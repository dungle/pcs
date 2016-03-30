using System;
using System.Data;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;
using C1.C1Report;

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
    public partial class ProductItemInfo : Form
    {
        private bool blnFirstLoad;
        private string strProductReportQuery;
        private bool blnDataIsValid = false;
        private int intCopyFromProductID;
        private const string THIS = "PCSProduct.Items.ProductItemInfo";
        private Bitmap mPicture = null;
        private const string ZERO_STRING = "0";

        private ITM_ProductVO voProduct;
        EnumAction enumAction;
        private int intProductID;
        private C1Report rptBOM;
        private C1Report rptRouting;

        private const string V_CUSTOMER_VENDOR = "V_VendorCustomer";
        private const string DECIMAL_NUMBERFORMAT_SMALL = "##############,0.0000";

        public ProductItemInfo()
        {
            InitializeComponent();
        }

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
            numPurchasingPrice.CustomFormat = DECIMAL_NUMBERFORMAT_SMALL;

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

            // item group and product classified
            var itemGroupsAndClassified = objProductItemInfoBO.GetItemGroupAndClassified();
            if (itemGroupsAndClassified != null)
            {
                FormControlComponents.PutDataIntoC1ComboBox(cboItemGroup, itemGroupsAndClassified.Tables[ITM_ItemGroupTable.TABLE_NAME], ITM_ItemGroupTable.CODE_FLD, ITM_ItemGroupTable.ITEMGROUPID_FLD, ITM_ItemGroupTable.TABLE_NAME);
                FormControlComponents.PutDataIntoC1ComboBox(cboClassified, itemGroupsAndClassified.Tables[ITM_ProductClassifiedTable.TABLE_NAME], ITM_ProductClassifiedTable.CODE_FLD, ITM_ProductClassifiedTable.PRODUCTCLASSIFIEDID_FLD, ITM_ProductClassifiedTable.TABLE_NAME);
            }
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

            if (voProduct.ItemGroupID > 0)
            {
                cboItemGroup.SelectedValue = voProduct.ItemGroupID;
            }
            if (voProduct.ProductClassifiedID > 0)
            {
                cboClassified.SelectedValue = voProduct.ProductClassifiedID;
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
                cboItemGroup.Enabled = !blnLock;
                cboClassified.Enabled = !blnLock;

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
                txtRegisteredCode.Text = string.Empty;

                chkAllowNegativeQty.Checked = chkMakeItem.Checked = false;
                cboStockUMID.Text = string.Empty;
                cboStockUMID.SelectedIndex = -1;

                cboSellingUMID.Text = string.Empty;
                cboSellingUMID.SelectedIndex = -1;

                cboItemGroup.Text = string.Empty;
                cboItemGroup.SelectedIndex = -1;

                cboClassified.Text = string.Empty;
                cboClassified.SelectedIndex = -1;

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
            objITM_ProductVO.ItemGroupID = cboItemGroup.SelectedIndex >= 0 ? int.Parse(cboItemGroup.SelectedValue.ToString()) : ITM_ProductDS.NUMBER_EMPTY_VALUE;
            objITM_ProductVO.ProductClassifiedID = cboClassified.SelectedIndex >= 0 ? int.Parse(cboClassified.SelectedValue.ToString()) : ITM_ProductDS.NUMBER_EMPTY_VALUE;
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
        /// Report layout file constant
        const string REPORT_LAYOUT_FILE = "ItemInformation.xml";
        const string SUBREPORT_NAME = "SubReport";
        short COPIES = 1;
        const string FLD = "fld";

        /// <summary>
        /// Show the Item Information report
        /// This report uses the : ItemInformation.xml template
        /// PROGRESS IS:
        /// ## 1 ## Get the meta information (single value)
        /// ## 2 ## Get the data collection information (data source for sub report StockStatus,BOM,Routing,StandardCost)
        /// ## 3 ## Draw actual value to the report field			
        /// ## 4 ## Refresh, render report
        /// ## 5 ## Show report			
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