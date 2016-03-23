using System;
using System.Drawing;


namespace PCSComProduct.Items.DS
{
	
	[Serializable]
	public class ITM_ProductVO
	{
		private int mProductID;
		private string mCode;
		private string mRevision;
		private string mDescription;
		private DateTime mSetupDate;
		private float mVAT;
		private float mImportTax;
		private float mExportTax;
		private float mSpecialTax;
		private bool mMakeItem;
		private string mPartNumber;
		private string mOtherInfo1;
		private string mOtherInfo2;
		private Decimal mLength;
		private Decimal mWidth;
		private Decimal mHeight;
		private Decimal mWeight;
		private bool mFinishedGoods;
		private Decimal mShelfLife;
		private bool mLotControl;
		private int mQAStatus;
		private bool mStock;
		private int mPlanType;
		private bool mAutoConversion;
		private Decimal mOrderQuantity;
		private Decimal mLTRequisition;
		private Decimal mLTSafetyStock;
		private Decimal mOrderQuantityMultiple;
		private Decimal mScrapPercent;
		private Decimal mMinimumStock;
		private Decimal mMaximumStock;
		private Decimal mConversionTolerance;
		private Decimal mVoucherTolerance;
		private Decimal mReceiveTolerance;
		private Decimal mIssueSize;
		private Decimal mLTFixedTime;
		private Decimal mLTVariableTime;
		private Decimal mLTDocToStock;
		private Decimal mLTOrderPrepare;
		private Decimal mLTShippingPrepare;
		private Decimal mLTSalesATP;
		private int mShipToleranceID;
		private int mBuyerID;
		private string mBOMDescription;
		private int mBomIncrement;
		private string mRoutingDescription;
		private DateTime mCreateDateTime;
		private DateTime mUpdateDateTime;
		private int mCostMethod;
		private int mRoutingIncrement;
		private int mCCNID;
		private int mCategoryID;
		private int mCostCenterID;
		private int mDeleteReasonID;
		private int mDeliveryPolicyID;
		private int mFormatCodeID;
		private int mFreightClassID;
		private int mHazardID;
		private int mOrderPolicyID;
		private int mOrderRuleID;
		private int mSourceID;
		private int mStockUMID;
		private int mSellingUMID;
		private int mHeightUMID;
		private int mWidthUMID;
		private int mLengthUMID;
		private int mBuyingUMID;
		private int mWeightUMID;
		private int mLotSize;
		private int mMasterLocationID;
		private int mLocationID;
		private int mBinID;
		private int mPrimaryVendorID;
		private int mVendorLocationID;
		private decimal mOrderPoint;
		private decimal mSafetyStock;
		private string mTaxCode;
		private int mCostCenterRateMasterID;
		private int mProductionLineID;
		private int mProductGroupID;
		private Bitmap mPicture;
		private int mAGCID;
		private string mSetUpPair;
		private bool mAVEG;
		private string mStockTakingCode;
		private bool mMassOrder;
        private bool mAllowNegativeQty;

        public bool MassOrder
		{
            get { return mMassOrder; }
            set { mMassOrder = value; }
		}
        public bool AllowNegativeQty
        {
            get { return mAllowNegativeQty; }
            set { mAllowNegativeQty = value; }
        }
		public string StockTakingCode
		{
			get { return mStockTakingCode; }
			set { mStockTakingCode = value; }
		}

		public bool AVEG
		{
			get { return mAVEG; }
			set { mAVEG = value; }
		}

		#region HACKED by TUAN TQ -- Change request
		//7 added properties, added by Tuan TQ -- 2005-09-21 and 2005-11-(03-08)
		private decimal mLicenseFee;		
		private string mPartNameVN;
		private int mInventor;
		private int mProductTypeId;
		
		//Added 2005-11-03
		private decimal mListPrice;
		private int mVendorCurrencyID;
		
		//Added 2005-11-08
		private decimal mQuantitySet;

		//Added 01 March, 2006
		private decimal mMinProduce;
		private decimal mMaxProduce;

		//Added 17 May, 2006
		private decimal mMaxRoundUpToMin;
		private decimal mMaxRoundUpToMultiple;
		
		//Added on 29 May, 2006
		private int mACAdjustmentMasterID;

		//Added on 09 June, 2006
		private string mRegisteredCode;

		public string RegisteredCode
		{
			get{ return mRegisteredCode;}
			set{ mRegisteredCode = value;}
		}

		public int ACAdjustmentMasterID
		{
			get{ return mACAdjustmentMasterID;}
			set{ mACAdjustmentMasterID = value;}
		}

		public decimal MaxRoundUpToMin
		{
			get{ return mMaxRoundUpToMin;}
			set{ mMaxRoundUpToMin = value;}
		}

		public decimal MaxRoundUpToMultiple
		{
			get{ return mMaxRoundUpToMultiple;}
			set{ mMaxRoundUpToMultiple = value;}
		}

		public decimal MinProduce
		{
			get{ return mMinProduce;}
			set{ mMinProduce = value;}
		}

		public decimal MaxProduce
		{
			get{ return mMaxProduce;}
			set{ mMaxProduce = value;}
		}

		public int ProductTypeId
		{
			set { mProductTypeId = value; }
			get { return mProductTypeId; }
		}

		public decimal LicenseFee
		{
			set { mLicenseFee = value; }
			get { return mLicenseFee; }
		}
		
		public string PartNameVN
		{
			set { mPartNameVN = value; }
			get { return mPartNameVN; }
		}

		public int Inventor
		{
			set { mInventor = value; }
			get { return mInventor; }
		}

		public decimal ListPrice
		{
			get { return mListPrice;}
			set { mListPrice = value;}
		}

		public int VendorCurrencyID
		{
			get{ return mVendorCurrencyID;}
			set{ mVendorCurrencyID = value;}
		}
		
		public decimal QuantitySet
		{
			set { mQuantitySet = value; }
			get { return mQuantitySet; }
		}

		//End --- Added by Tuan TQ -- 2005-09-21
		#endregion


		public int ProductID
		{
			set { mProductID = value; }
			get { return mProductID; }
		}

		public string Code
		{
			set { mCode = value; }
			get { return mCode; }
		}
		
		public string Revision
		{
			set { mRevision = value; }
			get { return mRevision; }
		}
		
		public string Description
		{
			set { mDescription = value; }
			get { return mDescription; }
		}

		public DateTime SetupDate
		{
			set { mSetupDate = value; }
			get { return mSetupDate; }
		}
		
		public float VAT
		{
			set { mVAT = value; }
			get { return mVAT; }
		}
		
		public float ImportTax
		{
			set { mImportTax = value; }
			get { return mImportTax; }
		}
		
		public float ExportTax
		{
			set { mExportTax = value; }
			get { return mExportTax; }
		}
		
		public float SpecialTax
		{
			set { mSpecialTax = value; }
			get { return mSpecialTax; }
		}
		
		public bool MakeItem
		{
			set { mMakeItem = value; }
			get { return mMakeItem; }
		}
		
		public string PartNumber
		{
			set { mPartNumber = value; }
			get { return mPartNumber; }
		}
		
		public string OtherInfo1
		{
			set { mOtherInfo1 = value; }
			get { return mOtherInfo1; }
		}
		
		public string OtherInfo2
		{
			set { mOtherInfo2 = value; }
			get { return mOtherInfo2; }
		}
		
		public Decimal Length
		{
			set { mLength = value; }
			get { return mLength; }
		}
		
		public Decimal Width
		{
			set { mWidth = value; }
			get { return mWidth; }
		}
		
		public Decimal Height
		{
			set { mHeight = value; }
			get { return mHeight; }
		}
		
		public Decimal Weight
		{
			set { mWeight = value; }
			get { return mWeight; }
		}
		
		public bool FinishedGoods
		{
			set { mFinishedGoods = value; }
			get { return mFinishedGoods; }
		}
		
		public Decimal ShelfLife
		{
			set { mShelfLife = value; }
			get { return mShelfLife; }
		}
		
		public bool LotControl
		{
			set { mLotControl = value; }
			get { return mLotControl; }
		}
		
		public int QAStatus
		{
			set { mQAStatus = value; }
			get { return mQAStatus; }
		}
		
		public bool Stock
		{
			set { mStock = value; }
			get { return mStock; }
		}
		
		public int PlanType
		{
			set { mPlanType = value; }
			get { return mPlanType; }
		}
		
		public bool AutoConversion
		{
			set { mAutoConversion = value; }
			get { return mAutoConversion; }
		}
		
		public Decimal OrderQuantity
		{
			set { mOrderQuantity = value; }
			get { return mOrderQuantity; }
		}
		
		public Decimal LTRequisition
		{
			set { mLTRequisition = value; }
			get { return mLTRequisition; }
		}
		
		public Decimal LTSafetyStock
		{
			set { mLTSafetyStock = value; }
			get { return mLTSafetyStock; }
		}
		public Decimal OrderQuantityMultiple
		{
			set { mOrderQuantityMultiple = value; }
			get { return mOrderQuantityMultiple; }
		}
		public Decimal ScrapPercent
		{
			set { mScrapPercent = value; }
			get { return mScrapPercent; }
		}
		public Decimal MinimumStock
		{
			set { mMinimumStock = value; }
			get { return mMinimumStock; }
		}
		public Decimal MaximumStock
		{
			set { mMaximumStock = value; }
			get { return mMaximumStock; }
		}
		public Decimal ConversionTolerance
		{
			set { mConversionTolerance = value; }
			get { return mConversionTolerance; }
		}
		public Decimal VoucherTolerance
		{
			set { mVoucherTolerance = value; }
			get { return mVoucherTolerance; }
		}
		public Decimal ReceiveTolerance
		{
			set { mReceiveTolerance = value; }
			get { return mReceiveTolerance; }
		}
		public Decimal IssueSize
		{
			set { mIssueSize = value; }
			get { return mIssueSize; }
		}
		public Decimal LTFixedTime
		{
			set { mLTFixedTime = value; }
			get { return mLTFixedTime; }
		}
		public Decimal LTVariableTime
		{
			set { mLTVariableTime = value; }
			get { return mLTVariableTime; }
		}
		public Decimal LTDocToStock
		{
			set { mLTDocToStock = value; }
			get { return mLTDocToStock; }
		}
		public Decimal LTOrderPrepare
		{
			set { mLTOrderPrepare = value; }
			get { return mLTOrderPrepare; }
		}
		public Decimal LTShippingPrepare
		{
			set { mLTShippingPrepare = value; }
			get { return mLTShippingPrepare; }
		}
		public Decimal LTSalesATP
		{
			set { mLTSalesATP = value; }
			get { return mLTSalesATP; }
		}
		public int ShipToleranceID
		{
			set { mShipToleranceID = value; }
			get { return mShipToleranceID; }
		}
		public int BuyerID
		{
			set { mBuyerID = value; }
			get { return mBuyerID; }
		}
		public string BOMDescription
		{
			set { mBOMDescription = value; }
			get { return mBOMDescription; }
		}
		public int BomIncrement
		{
			set { mBomIncrement = value; }
			get { return mBomIncrement; }
		}
		public string RoutingDescription
		{
			set { mRoutingDescription = value; }
			get { return mRoutingDescription; }
		}
		public DateTime CreateDateTime
		{
			set { mCreateDateTime = value; }
			get { return mCreateDateTime; }
		}
		public DateTime UpdateDateTime
		{
			set { mUpdateDateTime = value; }
			get { return mUpdateDateTime; }
		}
		public int CostMethod
		{
			set { mCostMethod = value; }
			get { return mCostMethod; }
		}
		public int RoutingIncrement
		{
			set { mRoutingIncrement = value; }
			get { return mRoutingIncrement; }
		}
		public int CCNID
		{
			set { mCCNID = value; }
			get { return mCCNID; }
		}
		public int CategoryID
		{
			set { mCategoryID = value; }
			get { return mCategoryID; }
		}
		public int CostCenterID
		{
			set { mCostCenterID = value; }
			get { return mCostCenterID; }
		}
		public int DeleteReasonID
		{
			set { mDeleteReasonID = value; }
			get { return mDeleteReasonID; }
		}
		public int DeliveryPolicyID
		{
			set { mDeliveryPolicyID = value; }
			get { return mDeliveryPolicyID; }
		}
		public int FormatCodeID
		{
			set { mFormatCodeID = value; }
			get { return mFormatCodeID; }
		}
		public int FreightClassID
		{
			set { mFreightClassID = value; }
			get { return mFreightClassID; }
		}
		public int HazardID
		{
			set { mHazardID = value; }
			get { return mHazardID; }
		}
		public int OrderPolicyID
		{
			set { mOrderPolicyID = value; }
			get { return mOrderPolicyID; }
		}
		public int OrderRuleID
		{
			set { mOrderRuleID = value; }
			get { return mOrderRuleID; }
		}
		public int SourceID
		{
			set { mSourceID = value; }
			get { return mSourceID; }
		}
		public int StockUMID
		{
			set { mStockUMID = value; }
			get { return mStockUMID; }
		}
		public int SellingUMID
		{
			set { mSellingUMID = value; }
			get { return mSellingUMID; }
		}
		public int HeightUMID
		{
			set { mHeightUMID = value; }
			get { return mHeightUMID; }
		}
		public int WidthUMID
		{
			set { mWidthUMID = value; }
			get { return mWidthUMID; }
		}
		public int LengthUMID
		{
			set { mLengthUMID = value; }
			get { return mLengthUMID; }
		}
		public int BuyingUMID
		{
			set { mBuyingUMID = value; }
			get { return mBuyingUMID; }
		}
		public int WeightUMID
		{
			set { mWeightUMID = value; }
			get { return mWeightUMID; }
		}
		public int LotSize
		{
			set { mLotSize = value; }
			get { return mLotSize; }
		}
		public int MasterLocationID
		{
			set { mMasterLocationID = value; }
			get { return mMasterLocationID; }
		}
		public int LocationID
		{
			set { mLocationID = value; }
			get { return mLocationID; }
		}
		public int BinID
		{
			set { mBinID = value; }
			get { return mBinID; }
		}
		public int PrimaryVendorID
		{
			set { mPrimaryVendorID = value; }
			get { return mPrimaryVendorID; }
		}
		public int VendorLocationID
		{
			set { mVendorLocationID = value; }
			get { return mVendorLocationID; }
		}
		public Decimal OrderPoint
		{
			set { mOrderPoint = value; }
			get { return mOrderPoint; }
		}
		public Decimal SafetyStock
		{
			set {mSafetyStock = value;}
			get {return mSafetyStock;}
		}
		public int AGCID
		{
			set {mAGCID = value;}
			get {return mAGCID;}
		}
		public string TaxCode
		{
			set { mTaxCode = value; }
			get { return mTaxCode; }
		}

		public int CostCenterRateMasterID
		{
			get { return mCostCenterRateMasterID; }
			set { mCostCenterRateMasterID = value; }
		}

		public int ProductionLineID
		{
			get { return mProductionLineID; }
			set { mProductionLineID = value; }
		}

		public int ProductGroupID
		{
			get { return mProductGroupID; }
			set { mProductGroupID = value; }
		}

		public Bitmap Picture
		{
			get { return mPicture; }
			set { mPicture = value; }
		}
		public string SetUpPair
		{
			set { mSetUpPair = value; }
			get { return mSetUpPair; }
		}

        public int ItemGroupID { get; set; }
        public int ProductClassifiedID { get; set; }
	}
}
