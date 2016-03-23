using System;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;
using C1.Win.C1Command;
using C1.Win.C1List;
using C1.C1Report;
using C1.Win.C1Input;

using PCSComProduct.Items.BO;
using PCSComProduct.Items.DS;
using PCSComUtils.PCSExc;
using PCSComUtils.Common;
using PCSUtils.Log;
using PCSUtils.Utils;
using PCSComUtils.Common.BO;
using PCSProduct.STDCost;

namespace PCSProduct.Items
{
    /// <summary>
    /// Summary description for ProductItemInfo.
    /// </summary>
    public class ProductItemInfo : System.Windows.Forms.Form
    {
        #region controls

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
        private IContainer components;
        private GroupBox grpWeightAndSize;
        private GroupBox grpLeadTime;
        private GroupBox grpReplenishment;
        private GroupBox grpPlantype;
        private Button btnCopy;

        private bool blnFirstLoad;
        private string strProductReportQuery;
        private bool blnDataIsValid = false;

        private C1Report rptBOM;
        private C1Report rptRouting;

        #region Define variable for this form
        private const string THIS = "PCSProduct.Items.ProductItemInfo";
        private string strPathImage = "";
        private Bitmap mPicture =null;
        private const string ZERO_STRING = "0";

        private ITM_ProductVO voProduct;
        EnumAction enumAction;

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
        private int intProductID;

        #endregion

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
        private int intCopyFromProductID;
        private Label lblPercentConv;
        private Label lblPercentVouc;

        private const string BOM_REPORT_FLD = "BOMReport";
        private const string ROUTING_REPORT_FLD = "RoutingReport";
        private const string V_CUSTOMER_VENDOR = "V_VendorCustomer";
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

        #endregion
        private System.Windows.Forms.OpenFileDialog dlgSelectPicture;
        private C1.Win.C1Input.C1NumericEdit numRoundUpToMultiple;
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
        private CheckBox chkAllowNegativeQty;
        private Button btnViewImage;
        private Label lblClassified;
        private C1Combo cboClassified;
        private C1Combo cboItemGroup;
        private Label lblItemGroup;
        private const string DECIMAL_NUMBERFORMAT_SMALL = "##############,0.0000";

        public ProductItemInfo()
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

        private void btnClose_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

        private void btnCost_Click(object sender, System.EventArgs e)
        {
            const string METHOD_NAME = THIS + ".btnCost_Click()";
            try
            {
                if (intProductID > 0)
                {
                    ItemStandardCost frmItemStandardCost = new ItemStandardCost(intProductID);
                    frmItemStandardCost.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                // displays the error message.
                PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxIcon.Error);
                // log message.
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

        private void SetEditMask()
        {
            //Quantity Set
            numQuantitySet.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
            numQuantitySet.CustomFormat = Constants.DECIMAL_NUMBERFORMAT;

            //List Price
            numPurchasingPrice.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
            //numPurchasingPrice.CustomFormat = Constants.DECIMAL_NUMBERFORMAT;
            const string DECIMAL_FORMAT = "##############,0.0000";
            numPurchasingPrice.CustomFormat = DECIMAL_FORMAT;

            //License Fee
            numLicenseFee.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
            numLicenseFee.CustomFormat = Constants.DECIMAL_NUMBERFORMAT;

            //Length 
            txtLength.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
            txtLength.CustomFormat = Constants.DECIMAL_NUMBERFORMAT;

            //Height
            txtHeight.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
            txtHeight.CustomFormat = Constants.DECIMAL_NUMBERFORMAT;

            //Width
            txtWidth.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
            txtWidth.CustomFormat = Constants.DECIMAL_NUMBERFORMAT;

            //Weight
            txtWeight.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
            txtWeight.CustomFormat = Constants.DECIMAL_NUMBERFORMAT;

            //Shelife
            txtShelfLife.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
            txtShelfLife.CustomFormat = Constants.DECIMAL_NUMBERFORMAT;

            //Lot Size
            txtLotSize.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
            txtLotSize.CustomFormat = Constants.INTERGER_NUMBERFORMAT;

            //Safety stock
            txtSafetyStock.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
            txtSafetyStock.CustomFormat = Constants.DECIMAL_NUMBERFORMAT;

            //Scrap percent
            txtScrapPercent.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
            txtScrapPercent.CustomFormat = DECIMAL_NUMBERFORMAT_SMALL;

            //Max stock
            txtMaximumStock.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
            txtMaximumStock.CustomFormat = Constants.DECIMAL_NUMBERFORMAT;

            //Min Stock
            txtMinimumStock.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
            txtMinimumStock.CustomFormat = Constants.DECIMAL_NUMBERFORMAT;

            //issue size
            txtIssueSize.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
            txtIssueSize.CustomFormat = Constants.DECIMAL_NUMBERFORMAT;


            //Voucher tolerance
            txtVoucherTolerance.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
            txtVoucherTolerance.CustomFormat = Constants.DECIMAL_NUMBERFORMAT;

            //Conversion tolerance
            txtConversionTolerance.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
            txtConversionTolerance.CustomFormat = Constants.DECIMAL_NUMBERFORMAT;

            //Lead Time Fixed
            txtLTFixedTime.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
            txtLTFixedTime.CustomFormat = Constants.DECIMAL_NUMBERFORMAT;

            //Lead Time Safety stock
            txtLTSafetyStock.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
            txtLTSafetyStock.CustomFormat = Constants.DECIMAL_NUMBERFORMAT;

            //Lead Time variable
            txtLTVariableTime.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
            txtLTVariableTime.CustomFormat = Constants.DECIMAL_NUMBERFORMAT;

            //Lead Time Order Prepare
            txtLTOrderPrepare.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
            txtLTOrderPrepare.CustomFormat = Constants.DECIMAL_NUMBERFORMAT;

            //Lead Time Dock To Stock
            txtLTDockToStock.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
            txtLTDockToStock.CustomFormat = Constants.DECIMAL_NUMBERFORMAT;

            //Lead Time Sale ATP
            txtLTSalesATP.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
            txtLTSalesATP.CustomFormat = Constants.DECIMAL_NUMBERFORMAT;

            //Lead Time Shipping Prepare
            txtLTShippingPrepare.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
            txtLTShippingPrepare.CustomFormat = Constants.DECIMAL_NUMBERFORMAT;

            //Lead Time Requisition
            txtLTRequisition.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
            txtLTRequisition.CustomFormat = Constants.DECIMAL_NUMBERFORMAT;

            //VAT Tax
            txtVAT.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
            txtVAT.CustomFormat = Constants.DECIMAL_NUMBERFORMAT;

            //Import Tax
            txtImportTax.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
            txtImportTax.CustomFormat = Constants.DECIMAL_NUMBERFORMAT;

            //Export Tax
            txtExportTax.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
            txtExportTax.CustomFormat = Constants.DECIMAL_NUMBERFORMAT;

            //Special Tax
            txtSpecialTax.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
            txtSpecialTax.CustomFormat = Constants.DECIMAL_NUMBERFORMAT;

            //Order
            txtOrderQuantity.CustomFormat = Constants.DECIMAL_NUMBERFORMAT;
            txtOrderQuantityMultiple.CustomFormat = Constants.DECIMAL_NUMBERFORMAT;
        }

        private void ProductItemInfo_Load(object sender, System.EventArgs e)
        {
            blnFirstLoad = true;
            const string METHOD_NAME = THIS + ".ProductItemInfo_Load()";
            try
            {
                //Set authorization for user
                Security secForm = new Security();
                this.Name = THIS;
                if (secForm.SetRightForUserOnForm(this, SystemProperty.UserName) == 0)
                {
                    // You don't have the right to view this item
                    PCSMessageBox.Show(ErrorCode.MESSAGE_YOU_HAVE_NO_RIGHT_TO_VIEW, MessageBoxIcon.Warning);
                    this.Close();
                    return;
                }

                enumAction = new EnumAction();
                enumAction = EnumAction.Default;
                intProductID = -1; //No product at the load time

                LoadDataForCombo();

                //clear all controls value on form
                ClearForm();

                //lock all controls
                LockForm(true);

                //Diable or Enable buttons
                EnableDisableButtons();

                //set the display and edit mask for C1numberEdit
                SetEditMask();

                //Allow user to input Product Code, and Product Description 
                //To find for a specific product
                AllowToSearchExistingProduct(true);

                //focus on the first tab
                tabProductInfo.SelectedIndex = 0;
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
            catch (Exception ex)
            {
                // displays the error message.
                PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxIcon.Error);
                // log message.
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
        private void LoadDataForCombo()
        {
            const string QASTATUS_CAPTION = "Status";
            const string QASTATUS_DESC = "Description";
            ProductItemInfoBO objProductItemInfoBO = new ProductItemInfoBO();
            DataTable dtTmp = new DataTable();

            //Load data for CNN combo box
            FormControlComponents.PutDataIntoC1ComboBox(cboCCN, objProductItemInfoBO.GetCCN(), MST_CCNTable.CODE_FLD, MST_CCNTable.CCNID_FLD, MST_CCNTable.TABLE_NAME);

            //Load Stock Unit of Measure
            dtTmp = objProductItemInfoBO.GetUnitOfMeasure();
            //PutDataIntoCombo(cboStockUMID,dtTmp.Copy(),MST_UnitOfMeasureTable.CODE_FLD,MST_UnitOfMeasureTable.UNITOFMEASUREID_FLD);
            FormControlComponents.PutDataIntoC1ComboBox(cboStockUMID, dtTmp.Copy(), MST_UnitOfMeasureTable.CODE_FLD, MST_UnitOfMeasureTable.UNITOFMEASUREID_FLD, MST_UnitOfMeasureTable.TABLE_NAME);

            //Buying UNT of Measure
            FormControlComponents.PutDataIntoC1ComboBox(cboBuyingUMID, dtTmp.Copy(), MST_UnitOfMeasureTable.CODE_FLD, MST_UnitOfMeasureTable.UNITOFMEASUREID_FLD, MST_UnitOfMeasureTable.TABLE_NAME);

            //Selling Unit Of measure
            FormControlComponents.PutDataIntoC1ComboBox(cboSellingUMID, dtTmp.Copy(), MST_UnitOfMeasureTable.CODE_FLD, MST_UnitOfMeasureTable.UNITOFMEASUREID_FLD, MST_UnitOfMeasureTable.TABLE_NAME);

            //Length Unit Of measure
            FormControlComponents.PutDataIntoC1ComboBox(cboLengthUMID, dtTmp.Copy(), MST_UnitOfMeasureTable.CODE_FLD, MST_UnitOfMeasureTable.UNITOFMEASUREID_FLD, MST_UnitOfMeasureTable.TABLE_NAME);

            //Width Unit of measure
            FormControlComponents.PutDataIntoC1ComboBox(cboWidthUMID, dtTmp.Copy(), MST_UnitOfMeasureTable.CODE_FLD, MST_UnitOfMeasureTable.UNITOFMEASUREID_FLD, MST_UnitOfMeasureTable.TABLE_NAME);

            //Height Unit of measure
            FormControlComponents.PutDataIntoC1ComboBox(cboWidthUMID, dtTmp.Copy(), MST_UnitOfMeasureTable.CODE_FLD, MST_UnitOfMeasureTable.UNITOFMEASUREID_FLD, MST_UnitOfMeasureTable.TABLE_NAME);

            //Height unit of measure
            FormControlComponents.PutDataIntoC1ComboBox(cboHeightUMID, dtTmp.Copy(), MST_UnitOfMeasureTable.CODE_FLD, MST_UnitOfMeasureTable.UNITOFMEASUREID_FLD, MST_UnitOfMeasureTable.TABLE_NAME);

            //Weight unit of measure
            FormControlComponents.PutDataIntoC1ComboBox(cboWeightUMID, dtTmp.Copy(), MST_UnitOfMeasureTable.CODE_FLD, MST_UnitOfMeasureTable.UNITOFMEASUREID_FLD, MST_UnitOfMeasureTable.TABLE_NAME);

            //HACK: modify by Tuan TQ. Bind Cost method from database				
            FormControlComponents.PutDataIntoC1ComboBox(cboCostMethod, objProductItemInfoBO.GetCostMethod().Copy(), enm_CostMethodTable.DESCRIPTION_FLD, enm_CostMethodTable.CODE_FLD, enm_CostMethodTable.TABLE_NAME);
            //End hack

            //Account Grouping Code
            FormControlComponents.PutDataIntoC1ComboBox(cboAccountCode, objProductItemInfoBO.GetAGC(), MST_AGCTable.CODE_FLD, MST_AGCTable.AGCID_FLD, MST_AGCTable.TABLE_NAME);
            //cboAccountCode.DataSource = objProductItemInfoBO.GetAGC();
            //cboAccountCode.DisplayMember =  Constants.VALUE_FIELD;
            //cboAccountCode.ValueMember = Constants.ID_FIELD;
            //cboAccountCode.SelectedIndex = -1;

            //QA Status
            cboQAStatus.DataSource = objProductItemInfoBO.GetQAStatus();
            cboQAStatus.DisplayMember = Constants.ID_FIELD;
            cboQAStatus.ValueMember = Constants.ID_FIELD;
            cboQAStatus.Splits[0].DisplayColumns[Constants.VALUE_FIELD].Width = 400;
            cboQAStatus.Columns[Constants.ID_FIELD].Caption = QASTATUS_CAPTION;
            cboQAStatus.Columns[Constants.VALUE_FIELD].Caption = QASTATUS_DESC;
            cboQAStatus.SelectedIndex = -1;

            //Product Type
            cboProductType.DataSource = objProductItemInfoBO.GetProductType();
            cboProductType.DisplayMember = ITM_ProductTypeTable.CODE_FLD;
            cboProductType.ValueMember = ITM_ProductTypeTable.PRODUCTTYPEID_FLD;
            cboQAStatus.SelectedIndex = -1;

            //Master Location, Bin, and Location
            DataSet dstLocation = objProductItemInfoBO.GetLocation();
            FormControlComponents.PutDataIntoC1ComboBox(cboMasterLocationID, dstLocation.Tables[MST_MasterLocationTable.TABLE_NAME], MST_MasterLocationTable.CODE_FLD, MST_MasterLocationTable.MASTERLOCATIONID_FLD, MST_MasterLocationTable.TABLE_NAME);
            FormControlComponents.PutDataIntoC1ComboBox(cboLocationID, dstLocation.Tables[MST_LocationTable.TABLE_NAME], MST_LocationTable.CODE_FLD, MST_LocationTable.LOCATIONID_FLD, MST_LocationTable.TABLE_NAME);
            FormControlComponents.PutDataIntoC1ComboBox(cboBinID, dstLocation.Tables[MST_BINTable.TABLE_NAME], MST_BINTable.CODE_FLD, MST_BINTable.BINID_FLD, MST_BINTable.TABLE_NAME);

            //Category
            FormControlComponents.PutDataIntoC1ComboBox(cboCategoryID, objProductItemInfoBO.GetCategory(), ITM_CategoryTable.CODE_FLD, ITM_CategoryTable.CATEGORYID_FLD, ITM_CategoryTable.TABLE_NAME);

            //Source combo box
            FormControlComponents.PutDataIntoC1ComboBox(cboSourceID, objProductItemInfoBO.GetSource(), ITM_SourceTable.CODE_FLD, ITM_SourceTable.SOURCEID_FLD, ITM_SourceTable.TABLE_NAME);

            //Harzard
            FormControlComponents.PutDataIntoC1ComboBox(cboHazardID, objProductItemInfoBO.GetHarzard(), ITM_HazardTable.CODE_FLD, ITM_HazardTable.HAZARDID_FLD, ITM_HazardTable.TABLE_NAME);

            //Freight Class
            FormControlComponents.PutDataIntoC1ComboBox(cboFreightClassID, objProductItemInfoBO.GetFreightClass(), ITM_FreightClassTable.CODE_FLD, ITM_FreightClassTable.FREIGHTCLASSID_FLD, ITM_FreightClassTable.TABLE_NAME);
            //Deleate Reason
            FormControlComponents.PutDataIntoC1ComboBox(cboReasonID, objProductItemInfoBO.GetDeleteReason(), ITM_DeleteReasonTable.CODE_FLD, ITM_DeleteReasonTable.DELETEREASONID_FLD, ITM_DeleteReasonTable.TABLE_NAME);
            //Format code
            FormControlComponents.PutDataIntoC1ComboBox(cboFormatCodeID, objProductItemInfoBO.GetFormatCodes(), ITM_FormatCodeTable.CODE_FLD, ITM_FormatCodeTable.FORMATCODEID_FLD, ITM_FormatCodeTable.TABLE_NAME);
            //Delivery policy

            FormControlComponents.PutDataIntoC1ComboBox(cboDeliveryPolicyID, objProductItemInfoBO.GetDeliveryPolicy(), ITM_DeliveryPolicyTable.CODE_FLD, ITM_DeliveryPolicyTable.DELIVERYPOLICYID_FLD, ITM_DeliveryPolicyTable.TABLE_NAME);
            //Order Policy
            FormControlComponents.PutDataIntoC1ComboBox(cboOrderPolicyID, objProductItemInfoBO.GetOrderPolicy(), ITM_OrderPolicyTable.CODE_FLD, ITM_OrderPolicyTable.ORDERPOLICYID_FLD, ITM_OrderPolicyTable.TABLE_NAME);
            //Ship Tolerence
            FormControlComponents.PutDataIntoC1ComboBox(cboShipToleranceID, objProductItemInfoBO.GetShipTolerence(), ITM_ShipToleranceTable.CODE_FLD, ITM_ShipToleranceTable.SHIPTOLERANCEID_FLD, ITM_ShipToleranceTable.TABLE_NAME);
            //Buyer combo box
            FormControlComponents.PutDataIntoC1ComboBox(cboBuyerID, objProductItemInfoBO.GetBuyer(), ITM_BuyerTable.CODE_FLD, ITM_BuyerTable.BUYERID_FLD, ITM_BuyerTable.TABLE_NAME);
            //Vendor Location
            txtPrimaryVendorID.Text = "0";
            FormControlComponents.PutDataIntoC1ComboBox(cboVendorLocationID, objProductItemInfoBO.GetVendorLocation(), MST_PartyLocationTable.CODE_FLD, MST_PartyLocationTable.PARTYLOCATIONID_FLD, MST_PartyLocationTable.TABLE_NAME);
            ChangePrimaryVendorID();

            //Order rule
            FormControlComponents.PutDataIntoC1ComboBox(cboOrderRuleID, objProductItemInfoBO.GetOrderRule(), ITM_OrderPolicyTable.CODE_FLD, ITM_OrderRuleTable.ORDERRULEID_FLD, ITM_OrderRuleTable.TABLE_NAME);
        }

        private void LoadProductInfor(int pintProductID)
        {
            const string SEPARATE_STRING = "##";

            int intPreviousTabIndex = tabProductInfo.SelectedIndex;

            ProductItemInfoBO objProductItemInfoBO = new ProductItemInfoBO();
            voProduct = (ITM_ProductVO)objProductItemInfoBO.GetProductInfo(pintProductID);

            //Update this value object on the screen
            txtCode.Text = voProduct.Code;
            txtDescription.Text = voProduct.Description;
            txtRevision.Text = voProduct.Revision;
            txtStockTakingCode.Text = voProduct.StockTakingCode;
            txtSetUpPair.Text = voProduct.SetUpPair;

            txtRegisteredCode.Text = voProduct.RegisteredCode;            
            chkAllowNegativeQty.Checked = voProduct.AllowNegativeQty;
            //display CCN combo
            if (voProduct.CCNID > 0)
                cboCCN.SelectedValue = voProduct.CCNID;
            else
                cboCCN.SelectedIndex = -1;
            //Make Item
            chkMakeItem.Checked = voProduct.MakeItem;
            //Category 
            if (voProduct.CategoryID > 0)
                cboCategoryID.SelectedValue = voProduct.CategoryID;
            else
                cboCategoryID.SelectedIndex = -1;
            //Lot Control
            chkLotControl.Checked = voProduct.LotControl;
            //Stock Unit Of measure
            if (voProduct.StockUMID > 0)
                cboStockUMID.SelectedValue = voProduct.StockUMID;
            else
                cboStockUMID.SelectedIndex = -1;
            //Buying Unit of measure
            if (voProduct.BuyingUMID > 0)
                cboBuyingUMID.SelectedValue = voProduct.BuyingUMID;
            else
                cboBuyingUMID.SelectedIndex = -1;
            //Selling Unit of measure
            if (voProduct.SellingUMID > 0)
                cboSellingUMID.SelectedValue = voProduct.SellingUMID;
            else
                cboSellingUMID.SelectedIndex = -1;
            //Cost method
            if (voProduct.CostMethod >= 0)
                cboCostMethod.SelectedValue = voProduct.CostMethod;
            else
                cboCostMethod.SelectedIndex = -1;
            //AGC
            if (voProduct.AGCID > 0)
                cboAccountCode.SelectedValue = voProduct.AGCID;
            else
                cboAccountCode.SelectedIndex = -1;
            //QA status
            if (voProduct.QAStatus > 0)
                cboQAStatus.SelectedValue = voProduct.QAStatus;
            else
                cboQAStatus.SelectedIndex = -1;
            //Master Location
            if (voProduct.MasterLocationID > 0)
                cboMasterLocationID.SelectedValue = voProduct.MasterLocationID;
            else
                cboMasterLocationID.SelectedIndex = -1;
            //Location
            if (voProduct.LocationID > 0)
                cboLocationID.SelectedValue = voProduct.LocationID;
            else
                cboLocationID.SelectedIndex = -1;
            //Bin
            if (voProduct.BinID > 0)
                cboBinID.SelectedValue = voProduct.BinID;
            else
                cboBinID.SelectedIndex = -1;
            //Shelf Life
            if (voProduct.ShelfLife > (decimal)ITM_ProductDS.NUMBER_EMPTY_VALUE)
                txtShelfLife.Value = voProduct.ShelfLife;
            else
                txtShelfLife.Value = DBNull.Value;
            //Source 
            if (voProduct.SourceID > 0)
                cboSourceID.SelectedValue = voProduct.SourceID;
            else
                cboSourceID.SelectedIndex = -1;
            //VAT
            if (voProduct.VAT > (float)ITM_ProductDS.NUMBER_EMPTY_VALUE)
                txtVAT.Value = voProduct.VAT;
            else
                txtVAT.Value = DBNull.Value;
            //Import Tax
            if (voProduct.ImportTax > (float)ITM_ProductDS.NUMBER_EMPTY_VALUE)
                txtImportTax.Value = voProduct.ImportTax;
            else
                txtImportTax.Value = DBNull.Value;
            //Export Tax
            if (voProduct.ExportTax > (float)ITM_ProductDS.NUMBER_EMPTY_VALUE)
                txtExportTax.Value = voProduct.ExportTax;
            else
                txtExportTax.Value = DBNull.Value;
            //Special Tax
            if (voProduct.SpecialTax > (float)ITM_ProductDS.NUMBER_EMPTY_VALUE)
                txtSpecialTax.Value = voProduct.SpecialTax;
            else
                txtSpecialTax.Value = DBNull.Value;
            //Hazard 
            if (voProduct.HazardID > 0)
                cboHazardID.SelectedValue = voProduct.HazardID;
            else
                cboHazardID.SelectedIndex = -1;
            //Freight Class 
            if (voProduct.FreightClassID > 0)
                cboFreightClassID.SelectedValue = voProduct.FreightClassID;
            else
                cboFreightClassID.SelectedIndex = -1;
            //Delete Reason 
            if (voProduct.DeleteReasonID > 0)
                cboReasonID.SelectedValue = voProduct.DeleteReasonID;
            else
                cboReasonID.SelectedIndex = -1;
            //Stock
            chkStock.Checked = voProduct.Stock;
            //Lot Size
            if (voProduct.LotSize > ITM_ProductDS.NUMBER_EMPTY_VALUE)
                txtLotSize.Value = voProduct.LotSize;
            else
                txtLotSize.Value = DBNull.Value;
            //Format code
            if (voProduct.FormatCodeID > 0)
                cboFormatCodeID.SelectedValue = voProduct.FormatCodeID;
            txtPartNumber.Value = voProduct.PartNumber;
            txtPartNumber.ReadOnly = true;
            //Other infor 1
            txtOtherInfo1.Text = voProduct.OtherInfo1;
            //Other infor 2
            txtOtherInfo2.Text = voProduct.OtherInfo2;
            //Length
            if (voProduct.Length > (decimal)ITM_ProductDS.NUMBER_EMPTY_VALUE)
                txtLength.Value = voProduct.Length;
            else
                txtLength.Value = DBNull.Value;
            if (voProduct.LengthUMID > 0)
                cboLengthUMID.SelectedValue = voProduct.LengthUMID;
            else
                cboLengthUMID.SelectedIndex = -1;
            //Width 
            if (voProduct.Width > (decimal)ITM_ProductDS.NUMBER_EMPTY_VALUE)
                txtWidth.Value = voProduct.Width;
            else
                txtWidth.Value = DBNull.Value;
            if (voProduct.WidthUMID > 0)
                cboWidthUMID.SelectedValue = voProduct.WidthUMID;
            else
                cboWidthUMID.SelectedIndex = -1;
            //Height 
            if (voProduct.Height > (decimal)ITM_ProductDS.NUMBER_EMPTY_VALUE)
                txtHeight.Value = voProduct.Height;
            else
                txtHeight.Value = DBNull.Value;
            if (voProduct.HeightUMID > 0)
                cboHeightUMID.SelectedValue = voProduct.HeightUMID;
            else
                cboHeightUMID.SelectedIndex = -1;
            //Weight
            if (voProduct.Weight > (decimal)ITM_ProductDS.NUMBER_EMPTY_VALUE)
                txtWeight.Value = voProduct.Weight;
            else
                txtWeight.Value = DBNull.Value;
            if (voProduct.WeightUMID > 0)
                cboWeightUMID.SelectedValue = voProduct.WeightUMID;
            else
                cboWeightUMID.SelectedIndex = -1;
            //Setup Date
            dtmSetupDate.Value = voProduct.SetupDate;
            //Plan Type
            if (voProduct.PlanType == (int)PlanTypeEnum.MPS)
                radPlanTypeMPS.Checked = true;
            if (voProduct.PlanType == (int)PlanTypeEnum.MRP)
                radPlanTypeMRP.Checked = true;
            //Delivery policy
            if (voProduct.DeliveryPolicyID > 0)
                cboDeliveryPolicyID.SelectedValue = voProduct.DeliveryPolicyID;
            else
                cboDeliveryPolicyID.SelectedIndex = -1;
            //Order Policy
            if (voProduct.OrderPolicyID > 0)
                cboOrderPolicyID.SelectedValue = voProduct.OrderPolicyID;
            else
                cboOrderPolicyID.SelectedIndex = -1;
            //Ship Tolerence 
            if (voProduct.ShipToleranceID > 0)
                cboShipToleranceID.SelectedValue = voProduct.ShipToleranceID;
            else
                cboShipToleranceID.SelectedIndex = -1;
            //Auto Conversion 
            chkAutoConversion.Checked = voProduct.AutoConversion;
            //Buyer
            if (voProduct.BuyerID > 0)
                cboBuyerID.SelectedValue = voProduct.BuyerID;
            else
                cboBuyerID.SelectedIndex = -1;
            if (voProduct.PrimaryVendorID > 0)
            {
                string strPartyCodeAndName = objProductItemInfoBO.GetVendorCodeAndName(voProduct.PrimaryVendorID);
                int iIndex = strPartyCodeAndName.IndexOf(SEPARATE_STRING);

                txtPrimaryVendor.Text = strPartyCodeAndName.Substring(0, iIndex);
                txtVendorName.Text = strPartyCodeAndName.Substring(iIndex + 2);
                txtPrimaryVendorID.Text = voProduct.PrimaryVendorID.ToString();
            }
            else
            {
                txtPrimaryVendorID.Text = string.Empty;
                txtPrimaryVendor.Text = string.Empty;
                txtVendorName.Text = string.Empty;
            }
            ChangePrimaryVendorID();
            //Vendor Location
            if (voProduct.VendorLocationID > 0)
                cboVendorLocationID.SelectedValue = voProduct.VendorLocationID;
            else
                cboVendorLocationID.SelectedIndex = -1;
            //Order Rule
            if (voProduct.OrderRuleID > 0)
                cboOrderRuleID.SelectedValue = voProduct.OrderRuleID;
            else
                cboOrderRuleID.SelectedIndex = -1;
            //Min Oder Quantity
            if (voProduct.OrderQuantity > (decimal)ITM_ProductDS.NUMBER_EMPTY_VALUE)
                txtOrderQuantity.Value = voProduct.OrderQuantity;
            else
                txtOrderQuantity.Value = DBNull.Value;
            //Order Multiple Quantity
            if (voProduct.OrderQuantityMultiple > (decimal)ITM_ProductDS.NUMBER_EMPTY_VALUE)
                txtOrderQuantityMultiple.Value = voProduct.OrderQuantityMultiple;
            else
                txtOrderQuantityMultiple.Value = DBNull.Value;
            //Order point
            if (voProduct.OrderPoint > (decimal)ITM_ProductDS.NUMBER_EMPTY_VALUE)
                txtOrderPoint.Value = voProduct.OrderPoint;
            else
                txtOrderPoint.Value = DBNull.Value;
            //Safety stock
            if (voProduct.SafetyStock > (decimal)ITM_ProductDS.NUMBER_EMPTY_VALUE)
                txtSafetyStock.Value = voProduct.SafetyStock;
            else
                txtSafetyStock.Value = DBNull.Value;
            //Scrapt Percent
            if (voProduct.ScrapPercent > (decimal)ITM_ProductDS.NUMBER_EMPTY_VALUE)
                txtScrapPercent.Value = voProduct.ScrapPercent;
            else
                txtScrapPercent.Value = DBNull.Value;
            //Maximum Stock
            if (voProduct.MaximumStock > (decimal)ITM_ProductDS.NUMBER_EMPTY_VALUE)
                txtMaximumStock.Value = voProduct.MaximumStock;
            else
                txtMaximumStock.Value = DBNull.Value;
            //Minimum stock
            if (voProduct.MinimumStock > (decimal)ITM_ProductDS.NUMBER_EMPTY_VALUE)
                txtMinimumStock.Value = voProduct.MinimumStock;
            else
                txtMinimumStock.Value = DBNull.Value;
            //Issue Size
            if (voProduct.IssueSize > (decimal)ITM_ProductDS.NUMBER_EMPTY_VALUE)
                txtIssueSize.Value = voProduct.IssueSize;
            else
                txtIssueSize.Value = DBNull.Value;
            //Voucher tolerence
            if (voProduct.VoucherTolerance > (decimal)ITM_ProductDS.NUMBER_EMPTY_VALUE)
                txtVoucherTolerance.Value = voProduct.VoucherTolerance;
            else
                txtVoucherTolerance.Value = DBNull.Value;
            //Conversion Tolerence
            if (voProduct.ConversionTolerance > (decimal)ITM_ProductDS.NUMBER_EMPTY_VALUE)
                txtConversionTolerance.Value = voProduct.ConversionTolerance;
            else
                txtConversionTolerance.Value = DBNull.Value;
            //Lead Time Fixed Time
            if (voProduct.LTFixedTime > (decimal)ITM_ProductDS.NUMBER_EMPTY_VALUE)
                txtLTFixedTime.Value = voProduct.LTFixedTime;
            else
                txtLTFixedTime.Value = DBNull.Value;
            //LeadTime Safety Stock
            if (voProduct.LTSafetyStock > (decimal)ITM_ProductDS.NUMBER_EMPTY_VALUE)
                txtLTSafetyStock.Value = voProduct.LTSafetyStock;
            else
                txtLTSafetyStock.Value = DBNull.Value;
            //LTVariable
            if (voProduct.LTVariableTime > (decimal)ITM_ProductDS.NUMBER_EMPTY_VALUE)
                txtLTVariableTime.Value = voProduct.LTVariableTime;
            else
                txtLTVariableTime.Value = DBNull.Value;
            //LT Order Prepare
            if (voProduct.LTOrderPrepare > (decimal)ITM_ProductDS.NUMBER_EMPTY_VALUE)
                txtLTOrderPrepare.Value = voProduct.LTOrderPrepare;
            else
                txtLTOrderPrepare.Value = DBNull.Value;
            //LTDocToStock
            if (voProduct.LTDocToStock > (decimal)ITM_ProductDS.NUMBER_EMPTY_VALUE)
                txtLTDockToStock.Value = voProduct.LTDocToStock;
            else
                txtLTDockToStock.Value = DBNull.Value;
            //LTSales ATP
            if (voProduct.LTSalesATP > (decimal)ITM_ProductDS.NUMBER_EMPTY_VALUE)
                txtLTSalesATP.Value = voProduct.LTSalesATP;
            else
                txtLTSalesATP.Value = DBNull.Value;
            //LTShip Prepare
            if (voProduct.LTShippingPrepare > (decimal)ITM_ProductDS.NUMBER_EMPTY_VALUE)
                txtLTShippingPrepare.Value = voProduct.LTShippingPrepare;
            else
                txtLTShippingPrepare.Value = DBNull.Value;
            //LT Requisition
            if (voProduct.LTRequisition > (decimal)ITM_ProductDS.NUMBER_EMPTY_VALUE)
                txtLTRequisition.Value = voProduct.LTRequisition;
            else
                txtLTRequisition.Value = DBNull.Value;
            //HACKED: Added by Tuan TQ -- 2005-09-22 
            txtPartNameVN.Text = voProduct.PartNameVN;

            cboProductType.SelectedValue = voProduct.ProductTypeId;

            txtTaxCode.Text = voProduct.TaxCode;
            //LicenseFee
            if (voProduct.LicenseFee > (decimal)ITM_ProductDS.NUMBER_EMPTY_VALUE)
                numLicenseFee.Value = voProduct.LicenseFee;
            else
                numLicenseFee.Value = DBNull.Value;
            //ListPrice
            if (voProduct.ListPrice > (decimal)ITM_ProductDS.NUMBER_EMPTY_VALUE)
                numPurchasingPrice.Value = voProduct.ListPrice;
            else
                numPurchasingPrice.Value = DBNull.Value;
            //Min Produce
            if (voProduct.MinProduce > (decimal)ITM_ProductDS.NUMBER_EMPTY_VALUE)
                numMinProduce.Value = voProduct.MinProduce;
            else
                numMinProduce.Value = DBNull.Value;
            //Max Produce
            if (voProduct.MaxProduce > (decimal)ITM_ProductDS.NUMBER_EMPTY_VALUE)
                numMaxProduce.Value = voProduct.MaxProduce;
            else
                numMaxProduce.Value = DBNull.Value;
            //QuantitySet
            if (voProduct.QuantitySet > (decimal)ITM_ProductDS.NUMBER_EMPTY_VALUE)
                numQuantitySet.Value = voProduct.QuantitySet;
            else
                numQuantitySet.Value = DBNull.Value;

            //Inventor
            if (voProduct.Inventor > 0)
            {
                txtInventor.Tag = voProduct.Inventor;
                txtInventor.Text = objProductItemInfoBO.GetInventorCode(voProduct.Inventor);
            }
            else
            {
                txtInventor.Text = string.Empty;
                txtInventor.Tag = null;
            }

            if (voProduct.VendorCurrencyID > 0)
            {
                txtCurrency.Tag = voProduct.VendorCurrencyID;
                txtCurrency.Text = objProductItemInfoBO.GetCurrencyCode(voProduct.VendorCurrencyID);
            }
            else
            {
                txtCurrency.Tag = null;
                txtCurrency.Text = string.Empty;
            }

            //HACK: added by Tuan TQ. 17 May, 2006
            //MaxRoundUpToMin
            if (voProduct.MaxRoundUpToMin > (decimal)ITM_ProductDS.NUMBER_EMPTY_VALUE)
            {
                numRoundUpToMin.Value = voProduct.MaxRoundUpToMin;
            }
            else
            {
                numRoundUpToMin.Value = DBNull.Value;
            }

            //MaxRoundUpToMultiple
            if (voProduct.MaxRoundUpToMultiple > (decimal)ITM_ProductDS.NUMBER_EMPTY_VALUE)
            {
                numRoundUpToMultiple.Value = voProduct.MaxRoundUpToMultiple;
            }
            else
            {
                numRoundUpToMultiple.Value = DBNull.Value;
            }

            //AC Adjustment Code			
            if (voProduct.ACAdjustmentMasterID > ITM_ProductDS.NUMBER_EMPTY_VALUE)
            {
                txtACAdjustment.Tag = voProduct.ACAdjustmentMasterID;
                txtACAdjustment.Text = objProductItemInfoBO.GetACAdjustCodeByID(voProduct.ACAdjustmentMasterID);
            }
            else
            {
                txtACAdjustment.Tag = null;
                txtACAdjustment.Text = string.Empty;
            }

            // item picture
            picCategory.Image = voProduct.Picture;
            mPicture = voProduct.Picture;
            // aveg
            chkAVEG.Checked = voProduct.AVEG;
            // mass order
            chkMassOrder.Checked = voProduct.MassOrder;
            //End_ Added by Tuan TQ -- 2005-09-22
            //restore to the previous tab index
            tabProductInfo.SelectedIndex = intPreviousTabIndex;
            //Change to Default
            enumAction = EnumAction.Default;
            //Enable and Disable Buttons
            EnableDisableButtons();
            //Don't allow to search for existing product
            AllowToSearchExistingProduct(false);
        }
        private void AllowToSearchExistingProduct(bool blnAllow)
        {
            //txtCode.ReadOnly = !blnAllow;
            //txtDescription.ReadOnly = !blnAllow;
        }
        private void btnAdd_Click(object sender, System.EventArgs e)
        {
            const string METHOD_NAME = THIS + ".btnAdd_Click()";
            UtilsBO objUtilsBO = new UtilsBO();
            try
            {
                //Turn to Add action
                enumAction = EnumAction.Add;

                //Unlock form
                LockForm(false);

                //clear controls
                ClearForm();

                //
                if (SystemProperty.CCNID > 0)
                {
                    cboCCN.SelectedValue = SystemProperty.CCNID;
                }

                //setup date
                dtmSetupDate.Value = objUtilsBO.GetDBDate();
                //Fill Default Master Location 
                cboMasterLocationID.SelectedText = SystemProperty.MasterLocationCode;
                cboMasterLocationID.SelectedValue = SystemProperty.MasterLocationID;
                //Enable Button
                EnableDisableButtons();

                //Don't allow to search for existing product
                radPlanTypeMRP.Checked = true;
                // default checked
                chkAVEG.Checked = true;
                // default false
                chkMassOrder.Checked = false;

                txtCode.Focus();
            }
            catch (Exception ex)
            {
                // displays the error message.
                PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxIcon.Error);
                // log message.
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

        private void EnableDisableButtons()
        {
            switch (enumAction)
            {
                case EnumAction.Add:
                    //Disable Buttons
                    btnAdd.Enabled = false;
                    btnEdit.Enabled = false;
                    btnDelete.Enabled = false;
                    btnCopy.Enabled = false;
                    btnBOM.Enabled = false;
                    btnRouting.Enabled = false;
                    btnCost.Enabled = false;
                    btnSearchProductCode.Enabled = false;
                    btnSearchProductDescription.Enabled = false;
                    btnTakingCode.Enabled = false;

                    //Enable Buttons
                    btnSave.Enabled = true;
                    btnSearchVendor.Enabled = true;

                    break;
                case EnumAction.Edit:
                    //Disable Buttons
                    btnAdd.Enabled = false;
                    btnEdit.Enabled = false;
                    btnDelete.Enabled = false;
                    btnCopy.Enabled = false;
                    btnBOM.Enabled = false;
                    btnRouting.Enabled = false;
                    btnCost.Enabled = false;
                    btnSearchProductCode.Enabled = false;
                    btnSearchProductDescription.Enabled = false;
                    btnTakingCode.Enabled = false;

                    //Enable Buttons
                    btnSave.Enabled = true;
                    btnSearchVendor.Enabled = true;
                    break;
                case EnumAction.Default:
                    //Disable Buttons
                    btnSave.Enabled = false;
                    btnSearchVendor.Enabled = false;

                    btnAdd.Enabled = true;
                    btnSearchProductCode.Enabled = true;
                    btnSearchProductDescription.Enabled = true;
                    btnTakingCode.Enabled = true;

                    if (intProductID > 0)
                    {
                        btnEdit.Enabled = true;
                        btnDelete.Enabled = true;
                        btnCopy.Enabled = true;
                        btnBOM.Enabled = chkMakeItem.Checked;
                        btnRouting.Enabled = chkMakeItem.Checked;
                        btnCost.Enabled = true;
                    }
                    else
                    {
                        btnEdit.Enabled = false;
                        btnDelete.Enabled = false;
                        btnCopy.Enabled = false;
                        btnBOM.Enabled = false;
                        btnRouting.Enabled = false;
                        btnCost.Enabled = false;
                    }
                    break;
            }
        }

        private void LockForm(bool blnLock)
        {
            const string METHOD_NAME = THIS + ".LockForm()";
            try
            {
                //general information
                txtRevision.Enabled = !blnLock;
                cboCCN.Enabled = !blnLock;

                //Tab Standard Information
                chkAllowNegativeQty.Enabled =chkMakeItem.Enabled = !blnLock;

                cboStockUMID.Enabled = !blnLock;
                cboBuyingUMID.Enabled = !blnLock;
                cboSellingUMID.Enabled = !blnLock;
                cboCostMethod.Enabled = !blnLock;
                cboAccountCode.Enabled = !blnLock;
                cboQAStatus.Enabled = !blnLock;
                cboMasterLocationID.Enabled = !blnLock;

                if (cboMasterLocationID.SelectedIndex >= 0)
                    cboLocationID.Enabled = !blnLock;
                else
                    cboLocationID.Enabled = false;

                if (cboLocationID.SelectedIndex >= 0)
                {
                    cboBinID.Enabled = !blnLock;
                    //HACK: Rem by Tuan TQ. 20 Mar, 2006: fix error informed by Cuong NT
                    //ChangeLocation();
                    //End hack
                }
                else
                {
                    cboBinID.Enabled = false;
                }

                cboDeliveryPolicyID.Enabled = !blnLock;
                cboOrderPolicyID.Enabled = !blnLock;
                cboShipToleranceID.Enabled = !blnLock;
                cboCategoryID.Enabled = !blnLock;
                cboSourceID.Enabled = !blnLock;
                cboHazardID.Enabled = !blnLock;
                cboFreightClassID.Enabled = !blnLock;
                cboReasonID.Enabled = !blnLock;
                cboProductType.Enabled = !blnLock;
                cboFormatCodeID.Enabled = !blnLock;

                chkStock.Enabled = !blnLock;
                chkLotControl.Enabled = !blnLock;
                txtShelfLife.Enabled = !blnLock;
                txtVAT.Enabled = !blnLock;
                txtImportTax.Enabled = !blnLock;
                txtExportTax.Enabled = !blnLock;
                txtSpecialTax.Enabled = !blnLock;
                txtLotSize.Enabled = !blnLock;
                txtSetUpPair.Enabled = !blnLock;
                txtOtherInfo1.Enabled = !blnLock;
                txtOtherInfo2.Enabled = !blnLock;
                dtmSetupDate.Enabled = !blnLock;
                btnChangePicture.Enabled = !blnLock;
                btnClearPicture.Enabled = !blnLock;

                if (cboFormatCodeID.SelectedIndex >= 0)
                {
                    txtPartNumber.Enabled = blnLock;
                }
                else
                {
                    txtPartNumber.Enabled = !blnLock;
                }

                //Weight and Size
                grpWeightAndSize.Enabled = !blnLock;

                //Tab Planning data
                // grpPlantype.Enabled = !blnLock;
                grpReplenishment.Enabled = !blnLock;
                grpLeadTime.Enabled = !blnLock;

                chkAutoConversion.Enabled = !blnLock;

                //Begin_ Added by Tuan TQ-- 2005-09-22
                txtTaxCode.Enabled = !blnLock;
                txtInventor.Enabled = !blnLock;
                btnInventor.Enabled = !blnLock;
                numLicenseFee.Enabled = !blnLock;
                cboProductType.Enabled = !blnLock;
                txtPartNameVN.Enabled = !blnLock;
                txtCurrency.Enabled = !blnLock;
                numPurchasingPrice.Enabled = !blnLock;
                numQuantitySet.Enabled = !blnLock;
                txtACAdjustment.Enabled = !blnLock;
                btnACAdjustment.Enabled = !blnLock;
                txtRegisteredCode.Enabled = !blnLock;
                chkAVEG.Enabled = !blnLock;
                chkMassOrder.Enabled = !blnLock;

                //End_ Added by Tuan TQ-- 2005-09-22				
            }
            catch (Exception ex)
            {
                // displays the error message.
                PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxIcon.Error);
                // log message.
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

        private void ClearForm()
        {
            const string METHOD_NAME = THIS + ".ClearForm()";
            int intPreviousTabIndex = tabProductInfo.SelectedIndex;
            try
            {
                //general information
                txtCode.Text = string.Empty;
                txtDescription.Text = string.Empty;
                txtRevision.Text = string.Empty;
                txtStockTakingCode.Text = string.Empty;
                cboCCN.Text = string.Empty;
                txtSetUpPair.Text = string.Empty;
                cboCCN.SelectedIndex = -1;
                //Tab Standard Information
                //Change to this Tab 
                //tabProductInfo.SelectedIndex = 0;

                txtRegisteredCode.Text = string.Empty;

                chkAllowNegativeQty.Checked = chkMakeItem.Checked = false;
                cboStockUMID.Text = string.Empty;
                cboStockUMID.SelectedIndex = -1;

                cboSellingUMID.Text = string.Empty;
                cboSellingUMID.SelectedIndex = -1;

                cboBuyingUMID.Text = string.Empty;
                cboBuyingUMID.SelectedIndex = -1;

                cboCostMethod.SelectedIndex = -1;
                cboCostMethod.Text = string.Empty;

                cboAccountCode.SelectedText = string.Empty;
                cboAccountCode.SelectedIndex = -1;

                cboQAStatus.Text = string.Empty;
                cboQAStatus.SelectedIndex = -1;

                cboMasterLocationID.Text = string.Empty;
                cboMasterLocationID.SelectedIndex = -1;
                cboLocationID.Text = string.Empty;
                cboLocationID.SelectedIndex = -1;
                cboBinID.Text = string.Empty;
                cboBinID.SelectedIndex = -1;
                txtShelfLife.Value = DBNull.Value;

                cboCategoryID.Text = string.Empty;
                cboCategoryID.SelectedIndex = -1;
                cboSourceID.Text = string.Empty;
                cboSourceID.SelectedIndex = -1;

                txtVAT.Value = DBNull.Value;
                txtImportTax.Value = DBNull.Value;
                txtExportTax.Value = DBNull.Value;
                txtSpecialTax.Value = DBNull.Value;

                cboHazardID.Text = string.Empty;
                cboHazardID.SelectedIndex = -1;
                cboFreightClassID.Text = string.Empty;
                cboFreightClassID.SelectedIndex = -1;
                cboReasonID.Text = string.Empty;
                cboReasonID.SelectedIndex = -1;

                chkStock.Checked = false;

                chkLotControl.Checked = false;
                txtLotSize.Value = DBNull.Value;
                cboFormatCodeID.Text = string.Empty;
                cboFormatCodeID.SelectedIndex = -1;
                //txtPartNumber.Text = string.Empty;
                //txtPartNumber.ReadOnly = true;
                txtOtherInfo1.Text = string.Empty;
                txtOtherInfo2.Text = string.Empty;

                //Weight and Size
                txtLength.Value = DBNull.Value;
                cboLengthUMID.Text = string.Empty;
                cboLengthUMID.SelectedIndex = -1;
                txtWidth.Value = DBNull.Value;
                cboWidthUMID.Text = string.Empty;
                cboWidthUMID.SelectedIndex = -1;
                txtHeight.Value = DBNull.Value;
                cboHeightUMID.Text = string.Empty;
                cboHeightUMID.SelectedIndex = -1;
                txtWeight.Value = DBNull.Value;
                cboWeightUMID.Text = string.Empty;
                cboWeightUMID.SelectedIndex = -1;

                //dtmSetupDate.Text = string.Empty;
                dtmSetupDate.Value = DBNull.Value;

                //Tab Planning data
                //tabProductInfo.SelectedIndex = 1;

                cboDeliveryPolicyID.Text = string.Empty;
                cboDeliveryPolicyID.SelectedIndex = -1;

                cboOrderPolicyID.Text = string.Empty;
                cboOrderPolicyID.SelectedIndex = -1;

                cboShipToleranceID.Text = string.Empty;
                cboShipToleranceID.SelectedIndex = -1;
                chkAutoConversion.Checked = false;

                //Replenishment
                cboBuyerID.Text = string.Empty;
                cboBuyerID.SelectedIndex = -1;
                txtPrimaryVendorID.Text = string.Empty;
                txtPrimaryVendor.Text = string.Empty;
                cboVendorLocationID.Text = string.Empty;
                cboVendorLocationID.SelectedIndex = -1;
                cboOrderRuleID.Text = string.Empty;
                cboOrderRuleID.SelectedIndex = -1;

                txtOrderQuantity.Value = DBNull.Value;
                txtOrderQuantityMultiple.Value = DBNull.Value;
                txtOrderPoint.Value = DBNull.Value;

                txtSafetyStock.Value = DBNull.Value;
                txtScrapPercent.Value = DBNull.Value;
                txtMaximumStock.Value = DBNull.Value;
                txtMinimumStock.Value = DBNull.Value;
                txtIssueSize.Value = DBNull.Value;
                txtVoucherTolerance.Value = DBNull.Value;
                txtConversionTolerance.Value = DBNull.Value;
                chkRequisition.Checked = false;

                //Lead Time
                txtLTFixedTime.Value = DBNull.Value;
                txtLTSafetyStock.Value = DBNull.Value;
                txtLTVariableTime.Value = DBNull.Value;
                txtLTOrderPrepare.Value = DBNull.Value;
                txtLTDockToStock.Value = DBNull.Value;
                txtLTSalesATP.Value = DBNull.Value;
                txtLTShippingPrepare.Value = DBNull.Value;
                txtLTRequisition.Value = DBNull.Value;

                //Begin_ Added by Tuan TQ -2005-09-22
                ChangePrimaryVendorID();

                txtTaxCode.Text = string.Empty;
                txtPartNameVN.Text = string.Empty;
                txtInventor.Text = string.Empty;
                txtInventor.Tag = null;
                numLicenseFee.Value = DBNull.Value;
                cboProductType.Text = string.Empty;
                cboProductType.SelectedIndex = -1;
                txtCurrency.Text = string.Empty;
                txtCurrency.Tag = null;
                numPurchasingPrice.Value = DBNull.Value;
                numQuantitySet.Value = DBNull.Value;
                picCategory.Image = null;

                numMinProduce.Value = DBNull.Value;
                numMaxProduce.Value = DBNull.Value;

                numRoundUpToMin.Value = DBNull.Value;
                numRoundUpToMultiple.Value = DBNull.Value;
                txtACAdjustment.Tag = null;
                txtACAdjustment.Text = string.Empty;

                //End_ Added by Tuan TQ -2005-09-22
                txtVendorName.Text = string.Empty;

                //tabProductInfo.SelectedIndex = intPreviousTabIndex;
                if (SystemProperty.CCNID > 0)
                {
                    cboCCN.SelectedValue = SystemProperty.CCNID;
                }
            }
            catch (Exception ex)
            {
                // displays the error message.
                PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxIcon.Error);
                // log message.
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
            if (SaveToDatabase())
            {
                //Turn to Add action
                enumAction = EnumAction.Default;

                //lock form
                LockForm(true);

                //Enable Button
                EnableDisableButtons();

                PCSMessageBox.Show(ErrorCode.MESSAGE_AFTER_SAVE_DATA, MessageBoxIcon.Information);

                txtCode.Modified = false;
                txtDescription.Modified = false;
                txtCode.Focus();
            }
        }
        private ITM_ProductVO AssignValueToVOClass()
        {
            //Init the VO class
            ITM_ProductVO objITM_ProductVO = new ITM_ProductVO();
            objITM_ProductVO.AllowNegativeQty = chkAllowNegativeQty.Checked;
            //Product ID
            objITM_ProductVO.ProductID = intProductID;
            //Code
            objITM_ProductVO.Code = txtCode.Text.Trim();
            objITM_ProductVO.Description = txtDescription.Text.Trim();
            objITM_ProductVO.Revision = txtRevision.Text.Trim();
            objITM_ProductVO.StockTakingCode = txtStockTakingCode.Text.Trim();
            objITM_ProductVO.SetUpPair = txtSetUpPair.Text;
            objITM_ProductVO.RegisteredCode = txtRegisteredCode.Text.Trim();

            //CCN
            if (cboCCN.SelectedIndex >= 0)
                objITM_ProductVO.CCNID = int.Parse(cboCCN.SelectedValue.ToString());
            //Make Item
            objITM_ProductVO.MakeItem = chkMakeItem.Checked;
            //Category 
            if (cboCategoryID.SelectedIndex >= 0 && cboCategoryID.SelectedValue.ToString().Trim() != string.Empty)
                objITM_ProductVO.CategoryID = int.Parse(cboCategoryID.SelectedValue.ToString());
            //Lot Control
            objITM_ProductVO.LotControl = chkLotControl.Checked;
            //Stock Unit Of measure
            if (cboStockUMID.SelectedIndex >= 0 && cboStockUMID.SelectedValue.ToString().Trim() != string.Empty)
                objITM_ProductVO.StockUMID = int.Parse(cboStockUMID.SelectedValue.ToString());
            //Buying Unit of measure
            if (cboBuyingUMID.SelectedIndex >= 0 && cboBuyingUMID.SelectedValue.ToString().Trim() != string.Empty)
                objITM_ProductVO.BuyingUMID = int.Parse(cboBuyingUMID.SelectedValue.ToString());
            //Selling Unit of measure
            if (cboSellingUMID.SelectedIndex >= 0 && cboSellingUMID.SelectedValue.ToString().Trim() != string.Empty)
                objITM_ProductVO.SellingUMID = int.Parse(cboSellingUMID.SelectedValue.ToString());
            //Cost method
            if (cboCostMethod.SelectedIndex >= 0 && cboCostMethod.SelectedValue.ToString().Trim() != string.Empty)
                objITM_ProductVO.CostMethod = int.Parse(cboCostMethod.SelectedValue.ToString());
            else
                objITM_ProductVO.CostMethod = ITM_ProductDS.NUMBER_EMPTY_VALUE;
            //AGC
            if (cboAccountCode.SelectedIndex >= 0 && cboAccountCode.SelectedValue.ToString().Trim() != string.Empty)
                objITM_ProductVO.AGCID = int.Parse(cboAccountCode.SelectedValue.ToString().Trim());
            //QA status
            if (cboQAStatus.SelectedIndex >= 0 && cboQAStatus.SelectedValue.ToString().Trim() != string.Empty)
                objITM_ProductVO.QAStatus = int.Parse(cboQAStatus.SelectedValue.ToString());
            //Master Location
            if (cboMasterLocationID.SelectedIndex >= 0 && cboMasterLocationID.SelectedValue.ToString().Trim() != string.Empty)
                objITM_ProductVO.MasterLocationID = int.Parse(cboMasterLocationID.SelectedValue.ToString());
            //Location
            if (cboLocationID.SelectedIndex >= 0 && cboLocationID.SelectedValue.ToString().Trim() != string.Empty)
                objITM_ProductVO.LocationID = int.Parse(cboLocationID.SelectedValue.ToString());
            //Bin
            if (cboBinID.SelectedIndex >= 0 && cboBinID.SelectedValue.ToString().Trim() != string.Empty)
                objITM_ProductVO.BinID = int.Parse(cboBinID.SelectedValue.ToString());
            //Shelf Life
            if (txtShelfLife.Text.Trim() != string.Empty && txtShelfLife.Value != DBNull.Value)
                objITM_ProductVO.ShelfLife = Decimal.Parse(txtShelfLife.Text);
            else
                objITM_ProductVO.ShelfLife = ITM_ProductDS.NUMBER_EMPTY_VALUE;
            //Source 
            if (cboSourceID.SelectedIndex >= 0 && cboSourceID.SelectedValue.ToString().Trim() != string.Empty)
                objITM_ProductVO.SourceID = int.Parse(cboSourceID.SelectedValue.ToString());
            //VAT
            if (txtVAT.Text.Trim() != string.Empty && txtVAT.Value != DBNull.Value)
                objITM_ProductVO.VAT = float.Parse(txtVAT.Text);
            else
                objITM_ProductVO.VAT = ITM_ProductDS.NUMBER_EMPTY_VALUE;
            //Import Tax
            if (txtImportTax.Text.Trim() != string.Empty && txtImportTax.Value != DBNull.Value)
                objITM_ProductVO.ImportTax = float.Parse(txtImportTax.Text);
            else
                objITM_ProductVO.ImportTax = ITM_ProductDS.NUMBER_EMPTY_VALUE;
            //Export Tax
            if (txtExportTax.Text.Trim() != string.Empty && txtExportTax.Value != DBNull.Value)
                objITM_ProductVO.ExportTax = float.Parse(txtExportTax.Text);
            else
                objITM_ProductVO.ExportTax = ITM_ProductDS.NUMBER_EMPTY_VALUE;
            //Special Tax
            if (txtSpecialTax.Text.Trim() != string.Empty && txtSpecialTax.Value != DBNull.Value)
                objITM_ProductVO.SpecialTax = float.Parse(txtSpecialTax.Text);
            else
                objITM_ProductVO.SpecialTax = ITM_ProductDS.NUMBER_EMPTY_VALUE;
            //Hazard 
            if (cboHazardID.SelectedIndex >= 0 && cboHazardID.SelectedValue.ToString().Trim() != string.Empty)
                objITM_ProductVO.HazardID = int.Parse(cboHazardID.SelectedValue.ToString());
            //Freight Class 
            if (cboFreightClassID.SelectedIndex >= 0 && cboFreightClassID.SelectedValue.ToString().Trim() != string.Empty)
                objITM_ProductVO.FreightClassID = int.Parse(cboFreightClassID.SelectedValue.ToString());
            //Delete Reason 
            if (cboReasonID.SelectedIndex >= 0 && cboReasonID.SelectedValue.ToString().Trim() != string.Empty)
                objITM_ProductVO.DeleteReasonID = int.Parse(cboReasonID.SelectedValue.ToString());
            //Stock
            objITM_ProductVO.Stock = chkStock.Checked;
            //Lot Size
            if (txtLotSize.Text.Trim() != string.Empty && txtLotSize.Value != DBNull.Value)
                objITM_ProductVO.LotSize = int.Parse(txtLotSize.Value.ToString());
            else
                objITM_ProductVO.LotSize = ITM_ProductDS.NUMBER_EMPTY_VALUE;
            //Format code
            if (cboFormatCodeID.SelectedIndex >= 0 && cboFormatCodeID.SelectedValue.ToString().Trim() != string.Empty)
                objITM_ProductVO.FormatCodeID = int.Parse(cboFormatCodeID.SelectedValue.ToString());
            //Part number
            objITM_ProductVO.PartNumber = txtPartNumber.Text.Trim();
            //Other infor 1
            objITM_ProductVO.OtherInfo1 = txtOtherInfo1.Text.Trim();
            //Other infor 2
            objITM_ProductVO.OtherInfo2 = txtOtherInfo2.Text.Trim();
            //Length
            if (txtLength.Text.Trim() != string.Empty && txtLength.Value != DBNull.Value)
                objITM_ProductVO.Length = Decimal.Parse(txtLength.Text);
            else
                objITM_ProductVO.Length = ITM_ProductDS.NUMBER_EMPTY_VALUE;
            if (cboLengthUMID.SelectedIndex >= 0 && cboLengthUMID.SelectedValue.ToString().Trim() != string.Empty)
                objITM_ProductVO.LengthUMID = int.Parse(cboLengthUMID.SelectedValue.ToString());
            //Width 
            if (txtWidth.Text.Trim() != string.Empty && txtWidth.Value != DBNull.Value)
                objITM_ProductVO.Width = Decimal.Parse(txtWidth.Text);
            else
                objITM_ProductVO.Width = ITM_ProductDS.NUMBER_EMPTY_VALUE;
            if (cboWidthUMID.SelectedIndex >= 0 && cboWidthUMID.SelectedValue.ToString().Trim() != string.Empty)
                objITM_ProductVO.WidthUMID = int.Parse(cboWidthUMID.SelectedValue.ToString());
            //Height 
            if (txtHeight.Text.Trim() != string.Empty && txtHeight.Value != DBNull.Value)
                objITM_ProductVO.Height = Decimal.Parse(txtHeight.Text);
            else
                objITM_ProductVO.Height = ITM_ProductDS.NUMBER_EMPTY_VALUE;
            if (cboHeightUMID.SelectedIndex >= 0 && cboHeightUMID.SelectedValue.ToString().Trim() != string.Empty)
                objITM_ProductVO.HeightUMID = int.Parse(cboHeightUMID.SelectedValue.ToString());
            //Weight
            if (txtWeight.Text.Trim() != string.Empty && txtWeight.Value != DBNull.Value)
                objITM_ProductVO.Weight = Decimal.Parse(txtWeight.Text);
            else
                objITM_ProductVO.Weight = ITM_ProductDS.NUMBER_EMPTY_VALUE;
            if (cboWeightUMID.SelectedIndex >= 0 && cboWeightUMID.SelectedValue.ToString().Trim() != string.Empty)
                objITM_ProductVO.WeightUMID = int.Parse(cboWeightUMID.SelectedValue.ToString());
            //Setup Date
            if (dtmSetupDate.Value != DBNull.Value && dtmSetupDate.Text.Trim() != string.Empty)
                objITM_ProductVO.SetupDate = (DateTime)dtmSetupDate.Value;
            else
                objITM_ProductVO.SetupDate = DateTime.MinValue;
            //Plan Type
            if (radPlanTypeMPS.Checked)
                objITM_ProductVO.PlanType = (int)PlanTypeEnum.MPS;
            if (radPlanTypeMRP.Checked)
                objITM_ProductVO.PlanType = (int)PlanTypeEnum.MRP;
            //Delivery policy
            if (cboDeliveryPolicyID.SelectedIndex >= 0 && cboDeliveryPolicyID.SelectedValue.ToString().Trim() != string.Empty)
                objITM_ProductVO.DeliveryPolicyID = int.Parse(cboDeliveryPolicyID.SelectedValue.ToString());
            //Order Policy
            if (cboOrderPolicyID.SelectedIndex >= 0 && cboOrderPolicyID.SelectedValue.ToString().Trim() != string.Empty)
                objITM_ProductVO.OrderPolicyID = int.Parse(cboOrderPolicyID.SelectedValue.ToString());
            //Ship Tolerence 
            if (cboShipToleranceID.SelectedIndex >= 0 && cboShipToleranceID.SelectedValue.ToString().Trim() != string.Empty)
                objITM_ProductVO.ShipToleranceID = int.Parse(cboShipToleranceID.SelectedValue.ToString());
            //Auto Conversion 
            objITM_ProductVO.AutoConversion = chkAutoConversion.Checked;
            //Buyer
            if (cboBuyerID.SelectedIndex >= 0 && cboBuyerID.SelectedValue.ToString().Trim() != string.Empty)
                objITM_ProductVO.BuyerID = int.Parse(cboBuyerID.SelectedValue.ToString());
            //Primary Vendor
            if (txtPrimaryVendorID.Text.Trim() != string.Empty)
                objITM_ProductVO.PrimaryVendorID = int.Parse(txtPrimaryVendorID.Text.Trim());
            //Vendor Location
            if (cboVendorLocationID.SelectedIndex >= 0 && cboVendorLocationID.SelectedValue.ToString().Trim() != string.Empty)
                objITM_ProductVO.VendorLocationID = int.Parse(cboVendorLocationID.SelectedValue.ToString());
            //Order Rule
            if (cboOrderRuleID.SelectedIndex >= 0 && cboOrderRuleID.SelectedValue.ToString().Trim() != string.Empty)
                objITM_ProductVO.OrderRuleID = int.Parse(cboOrderRuleID.SelectedValue.ToString());
            //Min Oder Quantity
            if (txtOrderQuantity.Text.Trim() != string.Empty)
                objITM_ProductVO.OrderQuantity = Decimal.Parse(txtOrderQuantity.Text);
            else
                objITM_ProductVO.OrderQuantity = ITM_ProductDS.NUMBER_EMPTY_VALUE;
            //Order Multiple Quantity
            if (txtOrderQuantityMultiple.Text.Trim() != string.Empty && txtOrderQuantityMultiple.Value != DBNull.Value)
                objITM_ProductVO.OrderQuantityMultiple = Decimal.Parse(txtOrderQuantityMultiple.Text);
            else
                objITM_ProductVO.OrderQuantityMultiple = ITM_ProductDS.NUMBER_EMPTY_VALUE;
            //Order point
            if (txtOrderPoint.Text.Trim() != string.Empty && txtOrderPoint.Value != DBNull.Value)
                objITM_ProductVO.OrderPoint = Decimal.Parse(txtOrderPoint.Text);
            else
                objITM_ProductVO.OrderPoint = ITM_ProductDS.NUMBER_EMPTY_VALUE;
            //Safety stock
            if (txtSafetyStock.Text.Trim() != string.Empty && txtSafetyStock.Value != DBNull.Value)
                objITM_ProductVO.SafetyStock = Decimal.Parse(txtSafetyStock.Text);
            else
                objITM_ProductVO.SafetyStock = ITM_ProductDS.NUMBER_EMPTY_VALUE;
            //Scrapt Percent
            if (txtScrapPercent.Text.Trim() != string.Empty && txtScrapPercent.Value != DBNull.Value)
                objITM_ProductVO.ScrapPercent = Decimal.Parse(txtScrapPercent.Text);
            else
                objITM_ProductVO.ScrapPercent = ITM_ProductDS.NUMBER_EMPTY_VALUE;
            //Maximum Stock
            if (txtMaximumStock.Text.Trim() != string.Empty && txtMaximumStock.Value != DBNull.Value)
                objITM_ProductVO.MaximumStock = Decimal.Parse(txtMaximumStock.Text);
            else
                objITM_ProductVO.MaximumStock = ITM_ProductDS.NUMBER_EMPTY_VALUE;
            //Minimum stock
            if (txtMinimumStock.Text.Trim() != string.Empty && txtMinimumStock.Value != DBNull.Value)
                objITM_ProductVO.MinimumStock = Decimal.Parse(txtMinimumStock.Text);
            else
                objITM_ProductVO.MinimumStock = ITM_ProductDS.NUMBER_EMPTY_VALUE;
            //Issue Size
            if (txtIssueSize.Text.Trim() != string.Empty && txtIssueSize.Value != DBNull.Value)
                objITM_ProductVO.IssueSize = Decimal.Parse(txtIssueSize.Text);
            else
                objITM_ProductVO.IssueSize = ITM_ProductDS.NUMBER_EMPTY_VALUE;
            //Voucher tolerence
            if (txtVoucherTolerance.Text.Trim() != string.Empty && txtVoucherTolerance.Value != DBNull.Value)
                objITM_ProductVO.VoucherTolerance = Decimal.Parse(txtVoucherTolerance.Text);
            else
                objITM_ProductVO.VoucherTolerance = ITM_ProductDS.NUMBER_EMPTY_VALUE;
            //Conversion Tolerence
            if (txtConversionTolerance.Text.Trim() != string.Empty && txtConversionTolerance.Value != DBNull.Value)
                objITM_ProductVO.ConversionTolerance = Decimal.Parse(txtConversionTolerance.Text);
            else
                objITM_ProductVO.ConversionTolerance = ITM_ProductDS.NUMBER_EMPTY_VALUE;
            //Lead Time Fixed Time
            if (txtLTFixedTime.Text.Trim() != string.Empty && txtLTFixedTime.Value != DBNull.Value)
                objITM_ProductVO.LTFixedTime = Decimal.Parse(txtLTFixedTime.Text);
            else
                objITM_ProductVO.LTFixedTime = ITM_ProductDS.NUMBER_EMPTY_VALUE;
            //LeadTime Safety Stock
            if (txtLTSafetyStock.Text.Trim() != string.Empty && txtLTSafetyStock.Value != DBNull.Value)
                objITM_ProductVO.LTSafetyStock = Decimal.Parse(txtLTSafetyStock.Text);
            else
                objITM_ProductVO.LTSafetyStock = ITM_ProductDS.NUMBER_EMPTY_VALUE;
            //LTVariable
            if (txtLTVariableTime.Text.Trim() != string.Empty && txtLTVariableTime.Value != DBNull.Value)
                objITM_ProductVO.LTVariableTime = Decimal.Parse(txtLTVariableTime.Text);
            else
                objITM_ProductVO.LTVariableTime = ITM_ProductDS.NUMBER_EMPTY_VALUE;
            //LT Order Prepare
            if (txtLTOrderPrepare.Text.Trim() != string.Empty && txtLTOrderPrepare.Value != DBNull.Value)
                objITM_ProductVO.LTOrderPrepare = Decimal.Parse(txtLTOrderPrepare.Text);
            else
                objITM_ProductVO.LTOrderPrepare = ITM_ProductDS.NUMBER_EMPTY_VALUE;
            //LTDocToStock
            if (txtLTDockToStock.Text.Trim() != string.Empty && txtLTDockToStock.Value != DBNull.Value)
                objITM_ProductVO.LTDocToStock = Decimal.Parse(txtLTDockToStock.Text);
            else
                objITM_ProductVO.LTDocToStock = ITM_ProductDS.NUMBER_EMPTY_VALUE;
            //LTSales ATP
            if (txtLTSalesATP.Text.Trim() != string.Empty && txtLTSalesATP.Value != DBNull.Value)
                objITM_ProductVO.LTSalesATP = Decimal.Parse(txtLTSalesATP.Text);
            else
                objITM_ProductVO.LTSalesATP = ITM_ProductDS.NUMBER_EMPTY_VALUE;
            //LTShip Prepare
            if (txtLTShippingPrepare.Text.Trim() != string.Empty && txtLTShippingPrepare.Value != DBNull.Value)
                objITM_ProductVO.LTShippingPrepare = Decimal.Parse(txtLTShippingPrepare.Text);
            else
                objITM_ProductVO.LTShippingPrepare = ITM_ProductDS.NUMBER_EMPTY_VALUE;
            //LT Requisition
            if (txtLTRequisition.Text.Trim() != string.Empty && txtLTRequisition.Value != DBNull.Value)
                objITM_ProductVO.LTRequisition = Decimal.Parse(txtLTRequisition.Text);
            else
                objITM_ProductVO.LTRequisition = ITM_ProductDS.NUMBER_EMPTY_VALUE;
            //Begin_ Added by Tuan TQ
            //Part name VN
            objITM_ProductVO.PartNameVN = txtPartNameVN.Text.Trim();
            //Tax code
            objITM_ProductVO.TaxCode = txtTaxCode.Text.Trim();
            // License Fee
            if (numLicenseFee.Text.Trim() != string.Empty && numLicenseFee.Value != DBNull.Value)
            {
                if (FormControlComponents.IsPositiveNumeric(numLicenseFee.Value.ToString()))
                    objITM_ProductVO.LicenseFee = decimal.Parse(numLicenseFee.Value.ToString());
                else
                    objITM_ProductVO.LicenseFee = ITM_ProductDS.NUMBER_EMPTY_VALUE;
            }
            else
                objITM_ProductVO.LicenseFee = ITM_ProductDS.NUMBER_EMPTY_VALUE;

            // List price
            if (numPurchasingPrice.Text.Trim() != string.Empty && numPurchasingPrice.Value != DBNull.Value)
                objITM_ProductVO.ListPrice = decimal.Parse(numPurchasingPrice.Value.ToString());
            else
                objITM_ProductVO.ListPrice = ITM_ProductDS.NUMBER_EMPTY_VALUE;
            // Min produce
            if (numMinProduce.Text.Trim() != string.Empty && numMinProduce.Value != DBNull.Value)
                objITM_ProductVO.MinProduce = decimal.Parse(numMinProduce.Value.ToString());
            else
                objITM_ProductVO.MinProduce = ITM_ProductDS.NUMBER_EMPTY_VALUE;
            // max produce
            if (numMaxProduce.Text.Trim() != string.Empty && numMaxProduce.Value != DBNull.Value)
                objITM_ProductVO.MaxProduce = decimal.Parse(numMaxProduce.Value.ToString());
            else
                objITM_ProductVO.MaxProduce = ITM_ProductDS.NUMBER_EMPTY_VALUE;
            // Quantity Set
            if (numQuantitySet.Text.Trim() != string.Empty && numQuantitySet.Value != DBNull.Value)
            {
                objITM_ProductVO.QuantitySet = decimal.Parse(numQuantitySet.Value.ToString());
            }
            else
            {
                objITM_ProductVO.QuantitySet = ITM_ProductDS.NUMBER_EMPTY_VALUE;
            }

            // Max Round Up To Min
            if (numRoundUpToMin.Text.Trim() != string.Empty && numRoundUpToMin.Value != DBNull.Value)
            {
                objITM_ProductVO.MaxRoundUpToMin = decimal.Parse(numRoundUpToMin.Value.ToString());
            }
            else
            {
                objITM_ProductVO.MaxRoundUpToMin = ITM_ProductDS.NUMBER_EMPTY_VALUE;
            }

            // Max Round Up To Multiple
            if (numRoundUpToMultiple.Text.Trim() != string.Empty && numRoundUpToMultiple.Value != DBNull.Value)
            {
                objITM_ProductVO.MaxRoundUpToMultiple = decimal.Parse(numRoundUpToMultiple.Value.ToString());
            }
            else
            {
                objITM_ProductVO.MaxRoundUpToMultiple = ITM_ProductDS.NUMBER_EMPTY_VALUE;
            }

            //CurrencyID
            if (txtCurrency.Text.Trim() != string.Empty)
            {
                objITM_ProductVO.VendorCurrencyID = int.Parse(txtCurrency.Tag.ToString());
            }
            else
            {
                objITM_ProductVO.VendorCurrencyID = ITM_ProductDS.NUMBER_EMPTY_VALUE;
            }

            //AC Adjustment ID
            if (txtACAdjustment.Text.Trim() != string.Empty)
            {
                objITM_ProductVO.ACAdjustmentMasterID = int.Parse(txtACAdjustment.Tag.ToString());
            }
            else
            {
                objITM_ProductVO.ACAdjustmentMasterID = ITM_ProductDS.NUMBER_EMPTY_VALUE;
            }

            //Inventor
            if (txtInventor.Text.Trim() != string.Empty)
            {
                objITM_ProductVO.Inventor = int.Parse(txtInventor.Tag.ToString());
            }
            else
            {
                objITM_ProductVO.Inventor = ITM_ProductDS.NUMBER_EMPTY_VALUE;
            }
            //Product Type
            if (cboProductType.SelectedIndex >= 0 && cboProductType.SelectedValue.ToString().Trim() != string.Empty)
                objITM_ProductVO.ProductTypeId = int.Parse(cboProductType.SelectedValue.ToString());
            else
                objITM_ProductVO.ProductTypeId = ITM_ProductDS.NUMBER_EMPTY_VALUE;
            //End_ Added by Tuan TQ
            // item picture
            if (picCategory.Image != null)
                objITM_ProductVO.Picture = new Bitmap(picCategory.Image);
            else
                objITM_ProductVO.Picture = null;
            objITM_ProductVO.AVEG = chkAVEG.Checked;
            objITM_ProductVO.MassOrder = chkMassOrder.Checked;
            return objITM_ProductVO;
        }

        private bool SaveToDatabase()
        {
            const string METHOD_NAME = THIS + ".SaveToDatabase()";
            try
            {
                blnDataIsValid = ValidateMandatoryControl();
                if (!blnDataIsValid)
                    return false;

                //Init the BO class
                ProductItemInfoBO objProductItemInfoBO = new ProductItemInfoBO();

                //HACK: added by Tuan TQ: 31 Mar, 2006
                voProduct = AssignValueToVOClass();
                //End hack

                switch (enumAction)
                {
                    case EnumAction.Add:
                        intProductID = objProductItemInfoBO.AddAndReturnID(voProduct, intCopyFromProductID);
                        voProduct.ProductID = intProductID;
                        break;

                    case EnumAction.Edit:
                        objProductItemInfoBO.Update(voProduct);
                        break;
                }

                intCopyFromProductID = 0;
                return true;
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
                txtCode.Focus();
                txtCode.SelectAll();
                return false;
            }
            catch (Exception ex)
            {
                // displays the error message.
                PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxIcon.Error);
                // log message.
                try
                {
                    Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
                }
                catch
                {
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
                }
                txtCode.Focus();
                return false;

            }

        }

        private bool ValidateMandatoryControl()
        {
            string[] arrMessage;

            ProductItemInfoBO objProductItemInfoBO = new ProductItemInfoBO();

            if (txtCode.Text.Trim() == string.Empty)
            {
                PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Warning);
                tabProductInfo.SelectedIndex = 0;
                txtCode.Focus();
                return false;
            }

            if (txtRevision.Text.Trim() == string.Empty)
            {
                PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Warning);
                tabProductInfo.SelectedIndex = 0;
                txtRevision.Focus();
                return false;
            }

            if (txtDescription.Text.Trim() == string.Empty)
            {
                PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Warning);
                tabProductInfo.SelectedIndex = 0;
                txtDescription.Focus();
                return false;
            }

            if (cboCCN.SelectedIndex < 0)
            {
                PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Warning);
                tabProductInfo.SelectedIndex = 0;
                cboCCN.Focus();
                return false;
            }

            // check for unique Stock Taking Code
            if (txtStockTakingCode.Text.Trim() != string.Empty)
            {
                int intProductID = 0;
                if (enumAction == EnumAction.Edit)
                    intProductID = voProduct.ProductID;
                if (!objProductItemInfoBO.CheckUniqueStockTakingCode(intProductID, txtStockTakingCode.Text.Trim()))
                {
                    PCSMessageBox.Show(ErrorCode.DUPLICATE_KEY, MessageBoxIcon.Warning);
                    txtStockTakingCode.Focus();
                    return false;
                }
            }

            if (cboStockUMID.SelectedIndex < 0 || cboStockUMID.SelectedValue.ToString().Trim() == string.Empty)
            {
                PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Warning);
                tabProductInfo.SelectedIndex = 0;
                cboStockUMID.Focus();
                return false;
            }

            if (cboBuyingUMID.SelectedIndex < 0 || cboBuyingUMID.SelectedValue.ToString().Trim() == string.Empty)
            {
                PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Warning);
                tabProductInfo.SelectedIndex = 0;
                cboBuyingUMID.Focus();
                return false;
            }

            if (cboSellingUMID.SelectedIndex < 0 || cboSellingUMID.SelectedValue.ToString().Trim() == string.Empty)
            {
                PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Warning);
                tabProductInfo.SelectedIndex = 0;
                cboSellingUMID.Focus();
                return false;
            }

            if (cboCostMethod.SelectedIndex < 0 || cboCostMethod.SelectedValue.ToString().Trim() == string.Empty)
            {
                PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Warning);
                tabProductInfo.SelectedIndex = 0;
                cboCostMethod.Focus();
                return false;
            }
            // HACK: Trada 09-02-2006
            if (cboMasterLocationID.SelectedIndex > 0)
            {
                if (cboLocationID.SelectedIndex < 0 || cboLocationID.SelectedValue.ToString().Trim() == string.Empty)
                {
                    arrMessage = new string[] { lblLocationID.Text };
                    PCSMessageBox.Show(ErrorCode.MESSAGE_MANDATORY_FIELD_REQUIRED, MessageBoxIcon.Warning, arrMessage);

                    tabProductInfo.SelectedIndex = 0;
                    cboLocationID.Focus();
                    return false;
                }

                if (cboBinID.SelectedIndex < 0 || cboBinID.SelectedValue.ToString().Trim() == string.Empty)
                {
                    arrMessage = new string[] { lblBinID.Text };

                    PCSMessageBox.Show(ErrorCode.MESSAGE_MANDATORY_FIELD_REQUIRED, MessageBoxIcon.Warning, arrMessage);
                    tabProductInfo.SelectedIndex = 0;
                    cboBinID.Focus();
                    return false;
                }
            }
            // END: Trada 09-02-2006

            //set up date
            if (dtmSetupDate.Value == DBNull.Value || dtmSetupDate.Text.Trim() == string.Empty)
            {
                PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Warning);
                tabProductInfo.SelectedIndex = 0;
                dtmSetupDate.Focus();
                return false;
            }
            string[] strParams = new string[2];

            //length
            if ((txtLength.Text.Trim() != string.Empty) && Decimal.Parse(txtLength.Text) > 0)
            {
                strParams[0] = lblLengthUMID.Text;
                strParams[1] = lblLength.Text;
                if (cboLengthUMID.SelectedIndex < 0 || cboLengthUMID.SelectedValue.ToString().Trim() == string.Empty)
                {
                    PCSMessageBox.Show(ErrorCode.MESSAGE_RELATION_REQUIRE, MessageBoxIcon.Error, strParams);
                    tabProductInfo.SelectedIndex = 0;
                    cboLengthUMID.Focus();
                    return false;
                }
            }
            // HACK : TuanDM 2005 - 13- 10
            if (cboLengthUMID.SelectedIndex > 0)
            {
                strParams[1] = lblLengthUMID.Text;
                strParams[0] = lblLength.Text;
                if (txtLength.Value.ToString() == string.Empty)
                {
                    PCSMessageBox.Show(ErrorCode.MESSAGE_RELATION_REQUIRE, MessageBoxIcon.Error, strParams);
                    tabProductInfo.SelectedIndex = 0;
                    txtLength.Focus();
                    return false;
                }
            }
            // End : TuanDM 2005 - 13- 10
            //height
            if ((txtHeight.Text.Trim() != string.Empty) && Decimal.Parse(txtHeight.Text) > 0)
            {
                strParams[0] = lblHeightUMID.Text;
                strParams[1] = lblHeight.Text;
                if (cboHeightUMID.SelectedIndex < 0 || cboHeightUMID.SelectedValue.ToString().Trim() == string.Empty)
                {
                    PCSMessageBox.Show(ErrorCode.MESSAGE_RELATION_REQUIRE, MessageBoxIcon.Error, strParams);
                    tabProductInfo.SelectedIndex = 0;
                    cboHeightUMID.Focus();
                    return false;
                }
            }
            // HACK : TuanDM 2005 - 13- 10
            if (cboHeightUMID.SelectedIndex > 0)
            {
                strParams[1] = lblHeightUMID.Text;
                strParams[0] = lblHeight.Text;
                if (txtHeight.Value.ToString() == string.Empty)
                {
                    PCSMessageBox.Show(ErrorCode.MESSAGE_RELATION_REQUIRE, MessageBoxIcon.Error, strParams);
                    tabProductInfo.SelectedIndex = 0;
                    txtHeight.Focus();
                    return false;
                }
            }
            // End : TuanDM 2005 - 13- 10
            //Width 
            if ((txtWidth.Text.Trim() != string.Empty) && Decimal.Parse(txtWidth.Text) > 0)
            {
                strParams[0] = lblWidthUMID.Text;
                strParams[1] = lblWidth.Text;
                if (cboWidthUMID.SelectedIndex < 0 || cboWidthUMID.SelectedValue.ToString().Trim() == string.Empty)
                {
                    PCSMessageBox.Show(ErrorCode.MESSAGE_RELATION_REQUIRE, MessageBoxIcon.Error, strParams);
                    tabProductInfo.SelectedIndex = 0;
                    cboWidthUMID.Focus();
                    return false;
                }
            }
            // HACK : TuanDM 2005 - 13- 10
            if (cboWidthUMID.SelectedIndex > 0)
            {
                strParams[1] = lblWidthUMID.Text;
                strParams[0] = lblWidth.Text;
                if (txtWidth.Value.ToString() == string.Empty)
                {
                    PCSMessageBox.Show(ErrorCode.MESSAGE_RELATION_REQUIRE, MessageBoxIcon.Error, strParams);
                    tabProductInfo.SelectedIndex = 0;
                    txtWidth.Focus();
                    return false;
                }
            }
            // End : TuanDM 2005 - 13- 10
            //Weight
            if ((txtWeight.Text.Trim() != string.Empty) && Decimal.Parse(txtWeight.Text) > 0)
            {
                strParams[0] = lblWeightUMID.Text;
                strParams[1] = lblWeight.Text;
                if (cboWeightUMID.SelectedIndex < 0 || cboWeightUMID.SelectedValue.ToString().Trim() == string.Empty)
                {
                    PCSMessageBox.Show(ErrorCode.MESSAGE_RELATION_REQUIRE, MessageBoxIcon.Error, strParams);
                    tabProductInfo.SelectedIndex = 0;
                    cboWeightUMID.Focus();
                    return false;
                }
            }
            // HACK : TuanDM 2005 - 13- 10
            if (cboWeightUMID.SelectedIndex > 0)
            {
                strParams[1] = lblWeightUMID.Text;
                strParams[0] = lblWeight.Text;
                if (txtWeight.Value.ToString() == string.Empty)
                {
                    PCSMessageBox.Show(ErrorCode.MESSAGE_RELATION_REQUIRE, MessageBoxIcon.Error, strParams);
                    tabProductInfo.SelectedIndex = 0;
                    txtWeight.Focus();
                    return false;
                }
            }
            // End : TuanDM 2005 - 13- 10
            //check MinStoc and MaxStock
            if (txtMinimumStock.Text.Trim() != string.Empty)
            {
                if (txtMaximumStock.Text.Trim() == string.Empty)
                {
                    PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Warning);
                    tabProductInfo.SelectedIndex = 1;
                    txtMaximumStock.Focus();
                    return false;
                }
                if (decimal.Parse(txtMinimumStock.Text.Trim()) > decimal.Parse(txtMaximumStock.Text.Trim()))
                {
                    PCSMessageBox.Show(ErrorCode.MESSAGE_INVALID_MINIMUMSTOCK, MessageBoxIcon.Warning);
                    tabProductInfo.SelectedIndex = 1;
                    txtMinimumStock.Focus();
                    return false;
                }
            }

            // HACK : TUANDM 2005 - 13 - 10 
            if (txtInventor.Text != string.Empty && txtInventor.Tag == null)
            {
                PCSMessageBox.Show(ErrorCode.MESSAGE_INVALID_INVENTOR, MessageBoxIcon.Warning);
                tabProductInfo.SelectedIndex = 0;
                txtInventor.Focus();
                return false;
            }

            // END : TUANDM 2005 - 13 - 10

            //Validate Primary Vendor
            if (txtPrimaryVendor.Text.Trim() != string.Empty)
            {
                int intPrimaryVendorID = objProductItemInfoBO.GetVendorID(txtPrimaryVendor.Text);
                if (intPrimaryVendorID == 0)
                {
                    PCSMessageBox.Show(ErrorCode.MESSAGE_INVALID_PARTYCODE, MessageBoxIcon.Warning);
                    tabProductInfo.SelectedIndex = 1;
                    txtPrimaryVendor.Focus();
                    return false;
                }

                //HACK: added by TuanTQ. 31 Mar, 2006: fix error no. 3618 by NgaHT
                if (enumAction == EnumAction.Edit && intPrimaryVendorID != voProduct.PrimaryVendorID)
                {
                    bool blnHasSchedule = objProductItemInfoBO.HasVendorDeliverySchedule(voProduct.ProductID);
                    if (blnHasSchedule)
                    {
                        PCSMessageBox.Show(ErrorCode.MESSAGE_PRODUCT_HAS_DELIVERY_SCHEDULE, MessageBoxIcon.Warning);
                        tabProductInfo.SelectedIndex = 1;
                        txtPrimaryVendor.Focus();
                        return false;
                    }
                }
                //End hack
            }
            else
            {
                //HACK: added by TuanTQ. 31 Mar, 2006: fix error no. 3618 by NgaHT
                if (enumAction == EnumAction.Edit)
                {
                    bool blnHasSchedule = objProductItemInfoBO.HasVendorDeliverySchedule(voProduct.ProductID);
                    if (blnHasSchedule)
                    {
                        PCSMessageBox.Show(ErrorCode.MESSAGE_PRODUCT_HAS_DELIVERY_SCHEDULE, MessageBoxIcon.Warning);
                        tabProductInfo.SelectedIndex = 1;
                        txtPrimaryVendor.Focus();
                        return false;
                    }
                }
                //End hack
            }

            //check location
            if (txtPrimaryVendorID.Text.Trim() != string.Empty && int.Parse(txtPrimaryVendorID.Text.Trim()) > 0)
            {
                if (cboVendorLocationID.SelectedIndex < 0)
                {
                    PCSMessageBox.Show(ErrorCode.MESSAGE_INVALID_PARTYLOCATION, MessageBoxIcon.Warning);
                    tabProductInfo.SelectedIndex = 1;
                    cboVendorLocationID.Focus();
                    return false;
                }
            }

            //Check Unit Buying and Stock 
            if (cboStockUMID.SelectedValue.ToString() != cboBuyingUMID.SelectedValue.ToString())
            {
                if (!objProductItemInfoBO.isTwoUnitOfMeasureScalled(int.Parse(cboBuyingUMID.SelectedValue.ToString()), int.Parse(cboStockUMID.SelectedValue.ToString())))
                {
                    PCSMessageBox.Show(ErrorCode.MESSAGE_INVALID_STOCKUNIT_AND_BUYINGUNIT, MessageBoxIcon.Warning);
                    tabProductInfo.SelectedIndex = 0;
                    cboBuyingUMID.Focus();
                    return false;
                }
            }

            //Check Unit Selling and Stock 
            if (cboStockUMID.SelectedValue.ToString() != cboSellingUMID.SelectedValue.ToString())
            {
                if (!objProductItemInfoBO.isTwoUnitOfMeasureScalled(int.Parse(cboSellingUMID.SelectedValue.ToString()), int.Parse(cboStockUMID.SelectedValue.ToString())))
                {
                    PCSMessageBox.Show(ErrorCode.MESSAGE_INVALID_STOCKUNIT_AND_SELLINGUNIT, MessageBoxIcon.Warning);
                    tabProductInfo.SelectedIndex = 0;
                    cboSellingUMID.Focus();
                    return false;
                }
            }

            if ((numPurchasingPrice.Text.Trim() != string.Empty) && (txtCurrency.Text.Trim() == string.Empty))
            {
                PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Warning);
                tabProductInfo.SelectedIndex = 1;
                txtCurrency.Focus();
                return false;
            }
            if (txtSetUpPair.Text.Trim() == 0.ToString())
            {
                PCSMessageBox.Show(ErrorCode.MESSAGE_DIFFERENT_ZERO, MessageBoxIcon.Warning);
                tabProductInfo.SelectedIndex = 0;
                txtSetUpPair.Focus();
                return false;
            }

            return true;
        }

        private void btnEdit_Click(object sender, System.EventArgs e)
        {
            const string METHOD_NAME = THIS + ".btnEdit_Click()";
            if (intProductID == -1)
            {
                PCSMessageBox.Show(ErrorCode.MESSAGE_PRODUCTINFO_SELECTPRODUCT, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                //Turn to Add action
                enumAction = EnumAction.Edit;

                //Unlock form
                LockForm(false);

                //Enable Button
                EnableDisableButtons();

                //focus
                txtCode.Focus();

            }
            catch (Exception ex)
            {
                // displays the error message.
                PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxIcon.Error);
                // log message.
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

        private void btnDelete_Click(object sender, System.EventArgs e)
        {
            const string METHOD_NAME = THIS + ".btnDelete_Click()";
            if (intProductID == -1)
            {
                PCSMessageBox.Show(ErrorCode.MESSAGE_PRODUCTINFO_SELECTPRODUCT, MessageBoxIcon.Warning);
                return;
            }
            if (PCSMessageBox.Show(ErrorCode.MESSAGE_DELETE_RECORD, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    //call the BO class to delete
                    ProductItemInfoBO objProductItemInfoBO = new ProductItemInfoBO();
                    objProductItemInfoBO.DeleteProduct(intProductID);


                    //After deleting, clean environment
                    intProductID = -1; //No product after deleting

                    //Turn to default action
                    enumAction = EnumAction.Default;

                    //unlock form
                    LockForm(false);

                    //clear controls
                    ClearForm();

                    //lock form
                    LockForm(true);

                    //Enable Button
                    EnableDisableButtons();

                    //allow to search for existing product
                    AllowToSearchExistingProduct(true);

                    //focus
                    txtCode.Focus();
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
                catch (Exception ex)
                {
                    // displays the error message.
                    PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxIcon.Error);
                    // log message.
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

        private void ProductItemInfo_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (enumAction == EnumAction.Add || enumAction == EnumAction.Edit)
            {
                System.Windows.Forms.DialogResult dlgResult = PCSMessageBox.Show(ErrorCode.MESSAGE_QUESTION_STORE_INTO_DATABASE, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                switch (dlgResult)
                {
                    case DialogResult.Yes:
                        SaveToDatabase();
                        e.Cancel = !blnDataIsValid;
                        break;

                    case DialogResult.Cancel:
                        e.Cancel = true;
                        break;
                }
            }
        }

        private void btnCopy_Click(object sender, System.EventArgs e)
        {
            const string METHOD_NAME = THIS + ".btnCopy_Click()";
            if (intProductID <= 0)
            {
                PCSMessageBox.Show(ErrorCode.MESSAGE_PRODUCTINFO_SELECTPRODUCT, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                //Unlock form
                LockForm(false);

                //Clear some properties
                txtCode.Text = string.Empty;
                txtDescription.Text = string.Empty;
                txtRevision.Text = string.Empty;
                txtStockTakingCode.Text = string.Empty;
                txtPartNameVN.Text = string.Empty;

                //Change the status of the form
                enumAction = EnumAction.Add;

                //Enable Buttons
                EnableDisableButtons();

                //store the current product ID , for later to copy it from
                intCopyFromProductID = intProductID;

                //HACK: Rem by Tuan TQ. 20 Mar, 2006: fix error informed by Cuong NT
                //if (cboLocationID.Text.Trim() != string.Empty)
                //{
                //	ChangeLocation();	
                //}				
                //End hack				
                txtCode.Focus();
            }
            catch (Exception ex)
            {
                // displays the error message.
                PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxIcon.Error);
                // log message.
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

        private void cboStockUMID_TextChanged(object sender, System.EventArgs e)
        {
            const string METHOD_NAME = THIS + ".cboBuyingUMID_TextChanged()";
            try
            {
                lblStockUM1.Text = "(" + cboStockUMID.Text + ")";
                lblStockUM2.Text = lblStockUM1.Text;
                lblStockUM3.Text = lblStockUM1.Text;
                lblStockUM4.Text = lblStockUM1.Text;
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

        private void ChangeMasterLocation()
        {
            const string METHOD_NAME = THIS + ".ChangeMasterLocation()";
            try
            {
                if (cboMasterLocationID.SelectedIndex < 0 || cboMasterLocationID.SelectedValue.ToString().Trim() == string.Empty)
                {
                    cboLocationID.SelectedIndex = -1;
                    cboBinID.SelectedIndex = -1;
                    cboLocationID.Enabled = false;
                    cboBinID.Enabled = false;
                }
                else
                {
                    if (enumAction != EnumAction.Default)
                    {
                        cboLocationID.Enabled = true;
                    }
                    else
                    {
                        cboBinID.Enabled = false;
                    }

                    //filter all rows from BIN
                    DataView dvLocation = ((DataTable)cboLocationID.DataSource).DefaultView;
                    dvLocation.RowFilter = MST_LocationTable.MASTERLOCATIONID_FLD + " = " + cboMasterLocationID.SelectedValue.ToString();
                    cboLocationID.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                // displays the error message.
                PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxIcon.Error);
                // log message.
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

        private void cboMasterLocationID_SelectedValueChanged(object sender, System.EventArgs e)
        {
            ChangeMasterLocation();
        }

        private void ChangeLocation()
        {
            const string METHOD_NAME = THIS + ".ChangeLocation()";
            try
            {
                if (cboLocationID.SelectedIndex < 0 || cboLocationID.SelectedValue.ToString().Trim() == string.Empty)
                {
                    cboBinID.SelectedIndex = -1;
                    cboBinID.Enabled = false;
                }
                else
                {
                    cboBinID.Enabled = true;
                    object objBinValue = ((DataTable)cboLocationID.DataSource).Rows[cboLocationID.SelectedIndex + 1][MST_LocationTable.BIN_FLD];
                    if (!objBinValue.Equals(DBNull.Value))
                    {
                        if (!bool.Parse(objBinValue.ToString()))
                        {
                            cboBinID.SelectedIndex = -1;
                            cboBinID.Enabled = false;
                            return;
                        }
                    }

                    if (enumAction != EnumAction.Default)
                    {
                        cboBinID.Enabled = true;
                    }
                    else
                    {
                        cboBinID.Enabled = false;
                    }

                    //filter all rows from BIN
                    DataView dvBin = ((DataTable)cboBinID.DataSource).DefaultView;
                    dvBin.RowFilter = MST_BINTable.LOCATIONID_FLD + " = " + cboLocationID.SelectedValue.ToString();
                    if (dvBin.Count > 0)
                    {
                        cboBinID.SelectedIndex = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                // displays the error message.
                PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxIcon.Error);
                // log message.
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
        private void cboLocationID_SelectedValueChanged(object sender, System.EventArgs e)
        {
            ChangeLocation();
        }

        private void btnBOM_Click(object sender, System.EventArgs e)
        {
            const string METHOD_NAME = THIS + ".btnBOM_Click()";
            try
            {
                if (intProductID > 0)
                {
                    Bom objBomForm = new Bom();
                    objBomForm.voProduct.ProductID = intProductID;
                    objBomForm.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                // displays the error message.
                PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxIcon.Error);
                // log message.
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

        private void cboMasterLocationID_ItemChanged(object sender, System.EventArgs e)
        {
            ChangeMasterLocation();
        }

        private void cboLocationID_ItemChanged(object sender, System.EventArgs e)
        {
            ChangeLocation();
        }

        private void btnSearchProductCode_Click(object sender, System.EventArgs e)
        {
            const string METHOD_NAME = THIS + ".btnSearchProductCode_Click()";
            try
            {
                DataRowView drwResult = null;
                Hashtable htbConditon = new Hashtable();
                if (cboCCN.SelectedValue != null)
                {
                    htbConditon.Add(ITM_ProductTable.CCNID_FLD, cboCCN.SelectedValue.ToString());
                }
                else
                {
                    PCSMessageBox.Show(ErrorCode.MESSAGE_RGA_CCN, MessageBoxIcon.Warning);
                    cboCCN.Focus();
                    return;
                }
                drwResult = FormControlComponents.OpenSearchForm(ITM_ProductTable.TABLE_NAME, ITM_ProductTable.CODE_FLD, txtCode.Text.Trim(), htbConditon, true);
                if (drwResult != null)
                {
                    intProductID = int.Parse(drwResult[ITM_ProductTable.PRODUCTID_FLD].ToString());
                    LoadProductInfor(intProductID);
                }
            }
            catch (Exception ex)
            {
                // displays the error message.
                PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxIcon.Error);
                // log message.
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

        private void ChangePrimaryVendorID()
        {
            const string METHOD_NAME = THIS + ".ChangePrimaryVendorID()";

            try
            {
                DataView drDataView = ((DataTable)cboVendorLocationID.DataSource).DefaultView;
                string strFilterString = MST_PartyLocationTable.PARTYID_FLD + "=" + txtPrimaryVendorID.Text;
                if (!drDataView.RowFilter.Equals(strFilterString))
                {
                    if (txtPrimaryVendorID.Text.Trim() == string.Empty || txtPrimaryVendorID.Text.Equals(ZERO_STRING))
                    {
                        drDataView.RowFilter = MST_PartyLocationTable.PARTYID_FLD + "=0";
                        cboVendorLocationID.Enabled = false;
                    }
                    else
                    {
                        drDataView.RowFilter = MST_PartyLocationTable.PARTYID_FLD + "=" + txtPrimaryVendorID.Text;
                        cboVendorLocationID.Enabled = true;
                    }
                    cboVendorLocationID.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                // displays the error message.
                PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxIcon.Error);
                // log message.
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
        private void btnSearchProductDescription_Click(object sender, System.EventArgs e)
        {
            const string METHOD_NAME = THIS + ".btnSearchProductDescription_Click()";
            try
            {
                DataRowView drwResult = null;
                Hashtable htbConditon = new Hashtable();
                if (cboCCN.SelectedValue != null)
                {
                    htbConditon.Add(ITM_ProductTable.CCNID_FLD, cboCCN.SelectedValue.ToString());
                }
                else
                {
                    PCSMessageBox.Show(ErrorCode.MESSAGE_RGA_CCN, MessageBoxIcon.Warning);
                    cboCCN.Focus();
                    return;
                }
                drwResult = FormControlComponents.OpenSearchForm(ITM_ProductTable.TABLE_NAME, ITM_ProductTable.DESCRIPTION_FLD, txtDescription.Text.Trim(), htbConditon, true);
                if (drwResult != null)
                {
                    intProductID = int.Parse(drwResult[ITM_ProductTable.PRODUCTID_FLD].ToString());
                    LoadProductInfor(intProductID);
                }
            }
            catch (Exception ex)
            {
                // displays the error message.
                PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxIcon.Error);
                // log message.
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

        private void txtPrimaryVendor_KeyDown(object sender, KeyEventArgs e)
        {
            const string METHOD_NAME = THIS + ".txtPrimaryVendor_KeyDown()";

            try
            {
                if ((e.KeyCode == Keys.F4) && (btnSearchVendor.Enabled))
                {
                    btnSearchVendor_Click(e, new EventArgs());
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

        private void btnSearchVendor_Click(object sender, System.EventArgs e)
        {
            const string METHOD_NAME = THIS + ".btnSearchVendor_Click()";
            const string VENDOR = "Vendor";
            try
            {
                DataRowView drwResult = null;
                Hashtable htbCondition = new Hashtable();

                htbCondition.Add(VENDOR, 1);
                drwResult = FormControlComponents.OpenSearchForm(V_CUSTOMER_VENDOR, MST_PartyTable.CODE_FLD, txtPrimaryVendor.Text, htbCondition, true);
                if (drwResult != null)
                {
                    txtPrimaryVendorID.Text = drwResult[MST_PartyTable.PARTYID_FLD].ToString();
                    txtPrimaryVendor.Text = drwResult[MST_PartyTable.CODE_FLD].ToString();
                    txtVendorName.Text = drwResult[MST_PartyTable.NAME_FLD].ToString();
                }
                ChangePrimaryVendorID();
            }
            catch (Exception ex)
            {
                // displays the error message.
                PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxIcon.Error);
                // log message.
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

        private void txtCode_Leave(object sender, System.EventArgs e)
        {
            const string METHOD_NAME = THIS + ".txtCode_Leave()";
            //Only use when users use this to search for existing product
            try
            {
                FormControlComponents.OnLeaveControl(sender, e);
                if (!btnSearchProductCode.Enabled || !txtCode.Modified)
                {
                    return;
                }
                if (txtCode.Text.Trim() == string.Empty)
                {
                    intProductID = -1;
                    ClearForm();
                    EnableDisableButtons();
                    return;
                }
                DataRowView drwResult = null;
                Hashtable htbConditon = new Hashtable();
                if (cboCCN.SelectedValue != null)
                {
                    htbConditon.Add(ITM_ProductTable.CCNID_FLD, cboCCN.SelectedValue.ToString());
                }
                else
                {
                    PCSMessageBox.Show(ErrorCode.MESSAGE_RGA_CCN, MessageBoxIcon.Warning);
                    cboCCN.Focus();
                    return;
                }
                drwResult = FormControlComponents.OpenSearchForm(ITM_ProductTable.TABLE_NAME, ITM_ProductTable.CODE_FLD, txtCode.Text.Trim(), htbConditon, false);
                if (drwResult != null)
                {
                    intProductID = int.Parse(drwResult[ITM_ProductTable.PRODUCTID_FLD].ToString());
                    LockForm(false);
                    ClearForm();
                    LockForm(true);
                    EnableDisableButtons();
                    LoadProductInfor(intProductID);
                }
                else
                {
                    txtCode.Focus();
                }
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
            catch (Exception ex)
            {
                // displays the error message.
                PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxIcon.Error);
                // log message.
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

        private void txtDescription_Leave(object sender, System.EventArgs e)
        {
            const string METHOD_NAME = THIS + ".txtDescription_Leave()";
            //Only use when users use this to search for existing product
            try
            {
                FormControlComponents.OnLeaveControl(sender, e);
                if (!btnSearchProductDescription.Enabled || !txtDescription.Modified)
                {
                    return;
                }
                if (txtDescription.Text.Trim() == string.Empty)
                {
                    intProductID = -1;
                    ClearForm();
                    EnableDisableButtons();
                    return;
                }
                DataRowView drwResult = null;
                Hashtable htbConditon = new Hashtable();
                if (cboCCN.SelectedValue != null)
                {
                    htbConditon.Add(ITM_ProductTable.CCNID_FLD, cboCCN.SelectedValue.ToString());
                }
                else
                {
                    PCSMessageBox.Show(ErrorCode.MESSAGE_RGA_CCN, MessageBoxIcon.Warning);
                    cboCCN.Focus();
                    return;
                }
                drwResult = FormControlComponents.OpenSearchForm(ITM_ProductTable.TABLE_NAME, ITM_ProductTable.DESCRIPTION_FLD, txtDescription.Text.Trim(), htbConditon, false);
                if (drwResult != null)
                {
                    intProductID = int.Parse(drwResult[ITM_ProductTable.PRODUCTID_FLD].ToString());
                    LockForm(false);
                    ClearForm();
                    LockForm(true);
                    EnableDisableButtons();
                    LoadProductInfor(intProductID);
                }
                else
                {
                    txtDescription.Focus();
                }
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
            catch (Exception ex)
            {
                // displays the error message.
                PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxIcon.Error);
                // log message.
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

        private void cboFormatCodeID_SelectedValueChanged(object sender, System.EventArgs e)
        {
            const string METHOD_NAME = THIS + ".cboFormatCodeID_SelectedValueChanged()";
            try
            {

                txtPartNumber.Value = DBNull.Value;
                CurrencyManager cm;
                cm = (CurrencyManager)cboFormatCodeID.BindingContext[cboFormatCodeID.DataSource];


                if (cboFormatCodeID.SelectedIndex < 0 || cboFormatCodeID.SelectedValue.ToString().Trim() == string.Empty)
                {
                    txtPartNumber.ReadOnly = true;
                }
                else
                {
                    //get the current row 
                    DataRowView drv = (DataRowView)cm.Current;
                    txtPartNumber.EditMask = drv[ITM_FormatCodeTable.CODE_FLD].ToString();
                    //C1.Win.C1Input.CustomPlaceholder cph = new C1.Win.C1Input.CustomPlaceholder();
                    //cph.LookupChars = "P1";
                    //txtPartNumber.MaskInfo.CustomPlaceholders.Add(cph);
                    txtPartNumber.ReadOnly = false;
                    txtPartNumber.Focus();
                }
            }
            catch (Exception ex)
            {
                // displays the error message.
                PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxIcon.Error);
                // log message.
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

        private void btnRouting_Click(object sender, System.EventArgs e)
        {
            const string METHOD_NAME = THIS + ".btnRouting_Click()";
            try
            {
                if (intProductID > 0 && chkMakeItem.Checked)
                {
                    Routing frmRoutingForm = new Routing();
                    frmRoutingForm.ProductID = intProductID;
                    frmRoutingForm.ShowDialog();

                    //HACKED by Tuan TQ. 09 Jan, 2005. Apply proposal number 3339					
                    ProductItemInfoBO boProductItemInfo = new ProductItemInfoBO();
                    decimal dVariableTime = boProductItemInfo.UpdateLTVariableTimeAndReturn(intProductID);

                    if (dVariableTime > 0)
                    {
                        txtLTVariableTime.Value = dVariableTime;
                    }
                    else
                    {
                        txtLTVariableTime.Value = DBNull.Value;
                    }
                    //End hacked
                }
            }
            catch (Exception ex)
            {
                // displays the error message.
                PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxIcon.Error);
                // log message.
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

        private void btnHelp_Click(object sender, System.EventArgs e)
        {
            PCSMessageBox.Show(ErrorCode.NOT_IMPLEMENT, MessageBoxIcon.Information);
        }

        private void ProductItemInfo_Activated(object sender, System.EventArgs e)
        {
            if (blnFirstLoad)
            {
                txtCode.Focus();
                blnFirstLoad = false;
            }
        }

        private void txtCode_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F4)
            {
                if (btnSearchProductCode.Enabled)
                {
                    btnSearchProductCode_Click(null, null);
                }
            }

        }

        private void txtDescription_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F4)
            {
                if (btnSearchProductDescription.Enabled)
                {
                    btnSearchProductDescription_Click(null, null);
                }
            }

        }

        private void txtVAT_TextChanged(object sender, System.EventArgs e)
        {

        }

        private void dtmSetupDate_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                tabProductInfo.TabIndex = 1;
            }
        }

        private void txtLTRequisition_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                tabProductInfo.TabIndex = 1;
            }

        }

        private void txtSpecialTax_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                tabProductInfo.TabIndex = 1;
            }
        }

        private void chkMakeItem_CheckedChanged(object sender, System.EventArgs e)
        {
            const string METHOD_NAME = THIS + ". chkMakeItem_CheckedChanged()";
            try
            {
                if (chkMakeItem.Checked)
                {
                    radPlanTypeMPS.Checked = true;
                }
                else
                {
                    radPlanTypeMRP.Checked = true;
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

        private void btnPrint_Click(object sender, System.EventArgs e)
        {
            const string METHOD_NAME = THIS + ".btnPrint_Click()";
            //return immediatly if none SO selected
            if (intProductID <= 0) return;

            try
            {
                this.Cursor = Cursors.WaitCursor;

                // HACKED: thachnn: 01/03/2006: add Item Information Report
                ShowItemInformationReport();
                // ENDHACKED: thachnn: 01/03/2006: add Item Information Report

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
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION);
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

        /// <summary>
        /// Select inventor information
        /// </summary>
        /// <param name="pstrMethodName"></param>
        /// <param name="pblnAlwaysShowDialog"></param>
        /// <param name="pblnFindById"></param>
        /// <returns></returns>
        /// <author> Tuan TQ</author>
        private bool SelectInventor(string pstrMethodName, bool pblnAlwaysShowDialog)
        {
            try
            {
                Hashtable htbCriteria = new Hashtable();
                DataRowView drwResult = null;
                //Call OpenSearchForm for selecting Master Location
                drwResult = FormControlComponents.OpenSearchForm(MST_PartyTable.TABLE_NAME, MST_PartyTable.CODE_FLD, txtInventor.Text, htbCriteria, pblnAlwaysShowDialog);

                // If has Party matched searching condition, fill values to form's controls
                if (drwResult != null)
                {
                    txtInventor.Text = drwResult[MST_PartyTable.CODE_FLD].ToString();
                    txtInventor.Tag = drwResult[MST_PartyTable.PARTYID_FLD];

                    //Reset modify status
                    txtInventor.Modified = false;
                }
                else if (!pblnAlwaysShowDialog)
                {
                    tabProductInfo.SelectedIndex = 0;
                    txtInventor.Focus();
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
        /// Fill related data on controls when select Currency
        /// </summary>
        /// <param name="pstrMethodName"></param>
        /// <param name="pblnFindByID"></param>
        /// <param name="pblnAlwaysShowDialog"></param>
        /// <returns></returns>
        /// <author> Tuan TQ</author>
        private bool SelectCurrency(string pstrMethodName, bool pblnAlwaysShowDialog)
        {
            try
            {
                Hashtable htbCriteria = new Hashtable();
                DataRowView drwResult = null;

                //Call OpenSearchForm for selecting Currency
                drwResult = FormControlComponents.OpenSearchForm(MST_CurrencyTable.TABLE_NAME, MST_CurrencyTable.CODE_FLD, txtCurrency.Text, htbCriteria, pblnAlwaysShowDialog);

                // If has currency matched searching condition, fill values to form's controls
                if (drwResult != null)
                {
                    //Check if master location was changed then clear grid content
                    txtCurrency.Text = drwResult[MST_CurrencyTable.CODE_FLD].ToString();
                    txtCurrency.Tag = drwResult[MST_CurrencyTable.CURRENCYID_FLD];

                    //Reset modify status
                    txtCurrency.Modified = false;
                }
                else if (!pblnAlwaysShowDialog)
                {
                    tabProductInfo.SelectedIndex = 1;
                    txtCurrency.Focus();
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

        private void btnInventor_Click(object sender, System.EventArgs e)
        {
            const string METHOD_NAME = THIS + ".btnInventor_Click()";
            try
            {
                SelectInventor(METHOD_NAME, true);
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

        private void OnEnterControl(object sender, System.EventArgs e)
        {
            const string METHOD_NAME = THIS + ". OnEnterControl()";
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

        private void cboBuyingUMID_TextChanged(object sender, System.EventArgs e)
        {
            const string METHOD_NAME = THIS + ".cboBuyingUMID_TextChanged()";
            try
            {
                lblBuyingUM1.Text = "(" + cboBuyingUMID.Text + ")";
                lblBuyingUM2.Text = lblBuyingUM1.Text;
                lblBuyingUM3.Text = lblBuyingUM1.Text;
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

        private void OnLeaveControl(object sender, System.EventArgs e)
        {
            const string METHOD_NAME = THIS + ".OnLeaveControl()";
            try
            {
                FormControlComponents.OnLeaveControl(sender, e);

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

        // HACK : Tuan DM 17 - 10 - 2005
        private void txtPrimaryVendor_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            const string METHOD_NAME = THIS + ".txtPrimaryVendor_Validating()";
            const string VENDOR = "Vendor";
            try
            {
                FormControlComponents.OnLeaveControl(sender, e);
                if (!btnSearchVendor.Enabled || !txtPrimaryVendor.Modified)
                {
                    return;
                }
                if (txtPrimaryVendor.Text.Trim() == string.Empty)
                {
                    txtPrimaryVendorID.Text = "0";
                    txtVendorName.Text = string.Empty;
                    ChangePrimaryVendorID();
                    return;
                }

                //Search for the Primary Vendor ID
                DataRowView drwResult = null;
                Hashtable htbCondition = new Hashtable();

                htbCondition.Add(VENDOR, 1);
                drwResult = FormControlComponents.OpenSearchForm(V_CUSTOMER_VENDOR, MST_PartyTable.CODE_FLD, txtPrimaryVendor.Text, htbCondition, false);
                if (drwResult != null)
                {
                    txtPrimaryVendorID.Text = drwResult[MST_PartyTable.PARTYID_FLD].ToString();
                    txtPrimaryVendor.Text = drwResult[MST_PartyTable.CODE_FLD].ToString();
                    txtVendorName.Text = drwResult[MST_PartyTable.NAME_FLD].ToString();
                    ChangePrimaryVendorID();
                }
                else
                {
                    e.Cancel = true;
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

        // END : Tuan DM 17 - 10 - 2005
        #region HACKED by Tuan TQ -- Nov 03, 2005: Change request, add new fields
        private void txtInventor_KeyDown(object sender, KeyEventArgs e)
        {
            const string METHOD_NAME = THIS + ".txtInventor_KeyDown()";

            try
            {
                if ((e.KeyCode == Keys.F4) && (btnInventor.Enabled))
                {
                    SelectCurrency(METHOD_NAME, true);
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

        private void btnSearchCurrency_Click(object sender, System.EventArgs e)
        {
            const string METHOD_NAME = THIS + ".btnSearchCurrency_Click()";
            try
            {
                SelectCurrency(METHOD_NAME, true);
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

        private void txtCurrency_KeyDown(object sender, KeyEventArgs e)
        {
            const string METHOD_NAME = THIS + ".txtCurrency_KeyDown()";
            try
            {
                if ((e.KeyCode == Keys.F4) && (btnSearchCurrency.Enabled))
                {
                    SelectCurrency(METHOD_NAME, true);
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

        private void txtCurrency_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            const string METHOD_NAME = THIS + ".txtCurrency_Validating()";

            try
            {
                //exit if not in add action or empty
                if (enumAction == EnumAction.Default) return;

                if (txtCurrency.Text.Trim().Length == 0)
                {
                    txtCurrency.Tag = null;
                    return;
                }
                else if (!txtCurrency.Modified)
                {
                    return;
                }

                e.Cancel = !SelectCurrency(METHOD_NAME, false);
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

        private void numPurchasingPrice_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                if (!numPurchasingPrice.Text.Trim().Equals(string.Empty))
                {
                    lblCurrency.ForeColor = System.Drawing.Color.Maroon;
                }
                else
                {
                    lblCurrency.ResetForeColor();
                }
            }
            catch
            {
            }
        }

        private void txtInventor_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            const string METHOD_NAME = THIS + ".txtCurrency_Validating()";

            try
            {
                //exit if not in add action or empty
                if (enumAction == EnumAction.Default) return;

                if (txtInventor.Text.Trim().Length == 0)
                {
                    txtInventor.Tag = null;
                    return;
                }
                else if (!txtInventor.Modified)
                {
                    return;
                }

                e.Cancel = !SelectInventor(METHOD_NAME, false);
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

        #endregion HACKED by Tuan TQ -- Nov 03, 2005: Change request, add new fields


        #region Thachnn: Item Information Report relate
        const string TABLE_NAME = "Table";
        const string REPORT_NAME = "ItemInformationReport";
        const string MAIN_META = "ItemInformation";
        const string STOCKSTATUS = "StockStatus";
        const string BOM = "BOM";
        const string ROUTING = "Routing";
        const string STANDARDCOST = "StandardCost";
        const string MONTH_DATE_FORMAT = "MMM";
        /// Report layout file constant
        const string REPORT_LAYOUT_FILE = "ItemInformation.xml";
        const string SUBREPORT_NAME = "SubReport";
        short COPIES = 1;
        const string PRODUCTID = "ProductID";
        const string CATEGORY = "Category";
        const string PARTNO = "Part No.";
        const string PARTNAME = "PartName";
        const string MODEL = "Model";
        const string FLD = "fld";
        const string LBL = "lbl";
        const string REPORTFIELD_PRODUCT_PICTURE = FLD + "Picture";

        /// <summary>
        /// Show the Item Information report
        /// This report uses the : ItemInformation.xml template
        /// PROGRESS IS:
        // ## 1 ## Get the meta information (single value)
        // ## 2 ## Get the data collection information (data source for sub report StockStatus,BOM,Routing,StandardCost)
        // ## 3 ## Draw actual value to the report field			
        // ## 4 ## Refresh, render report
        // ## 5 ## Show report			
        /// <author>thachnn: 01 03 2006</author>
        /// </summary>
        private void ShowItemInformationReport()
        {
            #region My variables

            const string METHOD_NAME = ".ShowItemInformationReport()";

            float fActualPageSize = 9000.0f;

            System.Data.DataTable dtbMainMeta;
            System.Data.DataTable dtbStockStatus;
            System.Data.DataTable dtbBOM;
            System.Data.DataTable dtbRouting;
            System.Data.DataTable dtbStandardCost;

            string mstrReportDefinitionFolder = Application.StartupPath + "\\" + Constants.REPORT_DEFINITION_STORE_LOCATION;

            #endregion


            #region	## 1 ## Get the meta information (single value)
            PCSComUtils.Common.BO.UtilsBO boUtil = new PCSComUtils.Common.BO.UtilsBO();
            PCSComUtils.Framework.ReportFrame.BO.C1PrintPreviewDialogBO objBO = new PCSComUtils.Framework.ReportFrame.BO.C1PrintPreviewDialogBO();

            ProductItemInfoBO objProductItemInfoBO = new ProductItemInfoBO();
            DataSet dstResultGetFromDatabase = objProductItemInfoBO.GetItemInformationData(intProductID,
                MAIN_META + TABLE_NAME, STOCKSTATUS + TABLE_NAME, BOM + TABLE_NAME, ROUTING + TABLE_NAME, STANDARDCOST + TABLE_NAME);

            dtbMainMeta = dstResultGetFromDatabase.Tables[MAIN_META + TABLE_NAME];
            dtbStockStatus = dstResultGetFromDatabase.Tables[STOCKSTATUS + TABLE_NAME];
            dtbBOM = dstResultGetFromDatabase.Tables[BOM + TABLE_NAME];
            dtbRouting = dstResultGetFromDatabase.Tables[ROUTING + TABLE_NAME];
            dtbStandardCost = dstResultGetFromDatabase.Tables[STANDARDCOST + TABLE_NAME];

            //			strCCN = boUtil.GetCCNCodeFromID(nCCNID);	
            //			strProductionLine = objBO.GetProductLineCodeFromID(nProductionLineID) + ": " + objBO.GetProductLineNameFromID(nProductionLineID);			
            //			strWorkOrder1 = objBO.GetWorkOrderMasterNoFromID(nWorkOrderID_1);
            //			strWorkOrder2 = objBO.GetWorkOrderMasterNoFromID(nWorkOrderID_2);

            #endregion

            #region ## 2 ## Get the data collection information (data source for sub report StockStatus,BOM,Routing,StandardCost)


            #endregion ## 2 ## Get the data collection information (data source for sub report StockStatus,BOM,Routing,StandardCost)

            #region ## 3 ## Draw actual value to the report field

            //			objRB.DrawPredefinedField(REPORTFLD_PARAMETER_CCN,strCCN);			
            //			objRB.DrawPredefinedField(REPORTFLD_PARAMETER_MONTH, pstrMonth);
            //			objRB.DrawPredefinedField(REPORTFLD_PARAMETER_YEAR, pstrYear);			
            //			objRB.DrawPredefinedField(REPORTFLD_PARAMETER_PRODUCTIONLINE, strProductionLine);			
            //			objRB.DrawPredefinedField(REPORTFLD_PARAMETER_WORKORDER1, strWorkOrder1);
            //			objRB.DrawPredefinedField(REPORTFLD_PARAMETER_WORKORDER2, strWorkOrder2);			
            //			string strProportionStandard = dblProportionStandard.ToString() ;
            //			objRB.DrawPredefinedField(REPORTFLD_PROPORTIONSTANDARDPERCENT, strProportionStandard );

            #endregion ## 3 ## Draw actual value to the report field


            #region ## 4 ## Refresh, render report

            ReportWithSubReportBuilder objRB = new ReportWithSubReportBuilder();
            objRB.ReportName = REPORT_NAME;
            objRB.SourceDataTable = dtbMainMeta;
            objRB.SubReportDataSources.Add(STOCKSTATUS + SUBREPORT_NAME, dtbStockStatus);
            objRB.SubReportDataSources.Add(BOM + SUBREPORT_NAME, dtbBOM);
            objRB.SubReportDataSources.Add(ROUTING + SUBREPORT_NAME, dtbRouting);
            objRB.SubReportDataSources.Add(STANDARDCOST + SUBREPORT_NAME, dtbStandardCost);

            #region INIT REPORT BUIDER OBJECT

            objRB.ReportDefinitionFolder = mstrReportDefinitionFolder;
            objRB.ReportLayoutFile = REPORT_LAYOUT_FILE;
            if (objRB.AnalyseLayoutFile() == false)
            {
                throw new PCSException(ErrorCode.MESSAGE_REPORT_TEMPLATE_FILE_NOT_FOUND, THIS + METHOD_NAME,
                    new System.IO.FileNotFoundException(THIS + METHOD_NAME, objRB.ReportLayoutFile));
            }
            objRB.UseLayoutFile = true;	// always use layout file


            C1.C1Report.Layout objLayout = objRB.Report.Layout;
            fActualPageSize = objLayout.PageSize.Width - (float)objLayout.MarginLeft - (float)objLayout.MarginRight;
            #endregion

            objRB.MakeDataTableForRender();

            // and show it in preview dialog				
            PCSUtils.Framework.ReportFrame.C1PrintPreviewDialog printPreview = new PCSUtils.Framework.ReportFrame.C1PrintPreviewDialog();
            printPreview.FormTitle = REPORT_NAME;
            objRB.ReportViewer = printPreview.ReportViewer;
            objRB.RenderReport();
            // TEST: Thachnn: ALTERNATIVE WAY: objRB.GetFieldByName(REPORTFIELD_PRODUCT_PICTURE).Picture = picCategory.Image;


            #endregion ## 4 ## Refresh, render report

            #region ## 5 ## Show report

            printPreview.FormTitle = objRB.GetFieldByName("fldTitle").Text;
            printPreview.Show();

            #endregion ## 5 ## Show report
        }

        #endregion Thachnn: Item Information Report relate

        private void btnChangePicture_Click(object sender, System.EventArgs e)
        {
            const string METHOD_NAME = THIS + ".btnChangePicture_Click()";
            try
            {
                if (System.Windows.Forms.DialogResult.Cancel == dlgSelectPicture.ShowDialog())
                    return;
                Image img = Image.FromFile(dlgSelectPicture.FileName);
                strPathImage = dlgSelectPicture.FileName;
                if (img.Size.Width > picCategory.Size.Width ||
                    img.Size.Height > picCategory.Size.Height)
                    picCategory.SizeMode = PictureBoxSizeMode.StretchImage;
                else
                    picCategory.SizeMode = PictureBoxSizeMode.CenterImage;
                picCategory.Image = Image.FromFile(dlgSelectPicture.FileName);
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

        private void btnClearPicture_Click(object sender, System.EventArgs e)
        {
            const string METHOD_NAME = THIS + ".btnClearPicture_Click()";
            try
            {
                picCategory.Image = null;
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

        #region Added by Tuan TQ - 29 May, 2006

        /// <summary>
        /// Fill related data on controls when select AC Adjustment Master Table
        /// </summary>
        /// <param name="pstrMethodName"></param>
        /// <param name="pblnFindByID"></param>
        /// <param name="pblnAlwaysShowDialog"></param>
        /// <returns></returns>
        /// <author> Tuan TQ</author>
        private bool SelectACAdjustment(bool pblnAlwaysShowDialog)
        {
            Hashtable htbCriteria = new Hashtable();
            DataRowView drwResult = null;

            //Call OpenSearchForm for selecting AC Adjustment Master Table
            drwResult = FormControlComponents.OpenSearchForm(cst_ACAdjustmentMasterTable.TABLE_NAME, cst_ACAdjustmentMasterTable.CODE_FLD, txtACAdjustment.Text, htbCriteria, pblnAlwaysShowDialog);

            // If has currency matched searching condition, fill values to form's controls
            if (drwResult != null)
            {
                txtACAdjustment.Text = drwResult[cst_ACAdjustmentMasterTable.CODE_FLD].ToString();
                txtACAdjustment.Tag = drwResult[cst_ACAdjustmentMasterTable.ACADJUSTMENTMASTERID_FLD];

                //Reset modify status
                txtACAdjustment.Modified = false;
            }
            else if (!pblnAlwaysShowDialog)
            {
                tabProductInfo.SelectedIndex = 2;
                txtACAdjustment.Focus();
                return false;
            }

            return true;
        }


        private void txtACAdjustment_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            const string METHOD_NAME = THIS + ".txtACAdjustment_Validating()";

            try
            {
                //exit if not in add action or empty
                if (enumAction == EnumAction.Default) return;

                if (txtACAdjustment.Text.Trim().Length == 0)
                {
                    txtACAdjustment.Tag = null;
                    return;
                }
                else if (!txtACAdjustment.Modified)
                {
                    return;
                }

                e.Cancel = !SelectACAdjustment(false);
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

        private void txtACAdjustment_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            const string METHOD_NAME = THIS + ".txtACAdjustment_KeyDown()";
            try
            {
                if ((e.KeyCode == Keys.F4) && (btnACAdjustment.Enabled))
                {
                    SelectACAdjustment(true);
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

        private void btnACAdjustment_Click(object sender, System.EventArgs e)
        {
            const string METHOD_NAME = THIS + ".btnACAdjustment_Click()";
            try
            {
                SelectACAdjustment(true);
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
        #endregion

        private void btnTakingCode_Click(object sender, System.EventArgs e)
        {
            const string METHOD_NAME = THIS + ".btnTakingCode_Click()";
            try
            {
                DataRowView drwResult = null;
                Hashtable htbConditon = new Hashtable();
                if (cboCCN.SelectedValue != null)
                {
                    htbConditon.Add(ITM_ProductTable.CCNID_FLD, cboCCN.SelectedValue.ToString());
                }
                else
                {
                    PCSMessageBox.Show(ErrorCode.MESSAGE_RGA_CCN, MessageBoxIcon.Warning);
                    cboCCN.Focus();
                    return;
                }
                drwResult = FormControlComponents.OpenSearchForm(ITM_ProductTable.TABLE_NAME, ITM_ProductTable.STOCKTAKINGCODE_FLD, txtStockTakingCode.Text, htbConditon, true);
                if (drwResult != null)
                {
                    intProductID = int.Parse(drwResult[ITM_ProductTable.PRODUCTID_FLD].ToString());
                    LoadProductInfor(intProductID);
                }
            }
            catch (Exception ex)
            {
                // displays the error message.
                PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxIcon.Error);
                // log message.
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

        private void txtStockTakingCode_Leave(object sender, EventArgs e)
        {
            const string METHOD_NAME = THIS + ".txtStockTakingCode_Leave()";
            //Only use when users use this to search for existing product
            try
            {
                FormControlComponents.OnLeaveControl(sender, e);
                if (!btnTakingCode.Enabled || !txtStockTakingCode.Modified)
                {
                    return;
                }
                if (txtStockTakingCode.Text.Trim() == string.Empty)
                {
                    intProductID = -1;
                    ClearForm();
                    EnableDisableButtons();
                    return;
                }
                DataRowView drwResult = null;
                Hashtable htbConditon = new Hashtable();
                if (cboCCN.SelectedValue != null)
                {
                    htbConditon.Add(ITM_ProductTable.CCNID_FLD, cboCCN.SelectedValue.ToString());
                }
                else
                {
                    PCSMessageBox.Show(ErrorCode.MESSAGE_RGA_CCN, MessageBoxIcon.Warning);
                    cboCCN.Focus();
                    return;
                }
                drwResult = FormControlComponents.OpenSearchForm(ITM_ProductTable.TABLE_NAME, ITM_ProductTable.STOCKTAKINGCODE_FLD, txtStockTakingCode.Text, htbConditon, false);
                if (drwResult != null)
                {
                    intProductID = int.Parse(drwResult[ITM_ProductTable.PRODUCTID_FLD].ToString());
                    LockForm(false);
                    ClearForm();
                    LockForm(true);
                    EnableDisableButtons();
                    LoadProductInfor(intProductID);
                }
                else
                    txtStockTakingCode.Focus();
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
            catch (Exception ex)
            {
                // displays the error message.
                PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxIcon.Error);
                // log message.
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

        private void txtStockTakingCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F4)
            {
                if (btnTakingCode.Enabled)
                {
                    btnTakingCode_Click(null, null);
                }
            }
        }

        private void btnViewImage_Click(object sender, EventArgs e)
        {
            if (mPicture == null && picCategory.Image != null)
                mPicture = new Bitmap(picCategory.Image);
            if (mPicture != null)
            {
                var viewImage = new ViewImage(mPicture);
                viewImage.Show();
            }
        }        
    }
}