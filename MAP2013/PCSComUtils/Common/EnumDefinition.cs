using System;

namespace PCSComUtils.Common
{
	/// <summary>
	/// Summary description for Action, control on form. Default =0,Add = 1,Edit = 2,Copy = 3,Delete = 4	
	/// </summary>
	public enum EnumAction
	{
		/// <summary>
		/// 0
		/// </summary>
		Default =0, 
		/// <summary>
		/// 1
		/// </summary>
		Add = 1,
		/// <summary>
		/// 2
		/// </summary>
		Edit = 2,
		/// <summary>
		/// 3
		/// </summary>
		Copy = 3,
		/// <summary>
		/// 4
		/// </summary>
		Delete = 4	
	}
	/// <summary>
	/// sys_TableField.SoftType: Sort type using in table configuration: None = 0,Ascending = 1,Descending = 2
	/// </summary>
	public enum EnumSortType
	{
		/// <summary>
		/// 0
		/// </summary>
		None = 0,
		/// <summary>
		/// 1
		/// </summary>
		Ascending = 1,
		/// <summary>
		/// 2
		/// </summary>
		Descending = 2
	}

	/// <summary>
	/// MoveDirection: Up,Down
	/// </summary>
	public enum MoveDirection
	{
		Up,
		Down
	}

	/// <summary>
	/// GutterPosition: Top = 1,Left = 0
	/// </summary>
	public enum GutterPosition
	{
		Top = 1,
		Left = 0
	}

	/// <summary>
	/// ViewReportMode: Normal = 0,History = 1
	/// </summary>
	public enum ViewReportMode
	{
		Normal = 0,
		History = 1
	}

	/// <summary>
	/// CostMethodEnum: ACT = 0,STD = 1,AVG = 2
	/// </summary>
	public enum CostMethodEnum
	{
		/// <summary>
		/// 0
		/// </summary>
		ACT = 0,
		/// <summary>
		/// 1
		/// </summary>
		STD = 1,
		/// <summary>
		/// 2
		/// </summary>
		AVG = 2
	}

	/// <summary>
	/// QAStatusEnum: REQUIRE_INSPECTION = 1,NOT_REQUIRE_INSPECTION = 2,QUALITY_ASSURED = 3
	/// </summary>
	public enum QAStatusEnum
	{
		REQUIRE_INSPECTION = 1,
		NOT_REQUIRE_INSPECTION = 2,
		QUALITY_ASSURED = 3
	}
    public enum LikeCondition
    {
        /// <summary>
        /// Contain text
        /// </summary>
        Contain = 0,
        /// <summary>
        /// Start with text
        /// </summary>
        StartWith = 1,
        /// <summary>
        /// End with text
        /// </summary>
        EndWith = 2
    }
	/// <summary>
	/// PartyTypeEnum: CUSTOMER = 0,VENDOR = 1,BOTH = 2, VENDOR-OUTSIDE = 3
	/// </summary>
	public enum PartyTypeEnum
	{
		/// <summary>
		/// 0
		/// </summary>
		CUSTOMER = 0,
		/// <summary>
		/// 1
		/// </summary>
		VENDOR = 1,
		/// <summary>
		/// 2
		/// </summary>
		BOTH = 2,
		/// <summary>
		/// 3: Vendor-Outside
		/// </summary>
		OUTSIDE = 3,
	}

	/// <summary>
	/// CapacityPeriod: Yearly = 0,Monthly = 1,Weekly = 2,Daily = 3
	/// </summary>
	public enum CapacityPeriod
	{
		/// <summary>
		/// 0
		/// </summary>
		Yearly = 0,
		/// <summary>
		/// 1
		/// </summary>
		Monthly = 1,
		/// <summary>
		/// 2
		/// </summary>
		Weekly = 2,
		/// <summary>
		/// 3
		/// </summary>
		Daily = 3
	}

	/// <summary>
	/// WOLineStatus: Unreleased = 1,Released = 2,MfgClose = 3,FinClose = 4
	/// </summary>
	public enum WOLineStatus

	{
		/// <summary>
		/// 1
		/// </summary>
		Unreleased = 1,
		/// <summary>
		/// 2
		/// </summary>
		Released = 2,
		/// <summary>
		/// 3
		/// </summary>
		MfgClose = 3,
		/// <summary>
		/// 4
		/// </summary>
		FinClose = 4
	}

	/// <summary>
	/// WOScheduleCode: B = 1,F = 2,U = 3
	/// </summary>
	public enum WOScheduleCode
	{
		/// <summary>
		/// 1
		/// </summary>
		B = 1,
		/// <summary>
		/// 2
		/// </summary>
		F = 2,
		/// <summary>
		/// 3
		/// </summary>
		U = 3
	}
 
	/// <summary>
	/// Type of Operation: Inside = 0,Outside = 1
	/// </summary>
	public enum OperationType
	{
		/// <summary>
		/// 0
		/// </summary>
		Inside = 0,
		/// <summary>
		/// 1
		/// </summary>
		Outside = 1
	}

	/// <summary>
	/// InspectionStatus: Waiting = 1,OnHold = 2,Accepted = 3,Rejected = 4
	/// </summary>
	public enum InspectionStatus
	{
		/// <summary>
		/// 1
		/// </summary>
		Waiting = 1,
		/// <summary>
		/// 2
		/// </summary>
		OnHold = 2,
		/// <summary>
		/// 3
		/// </summary>
		Accepted = 3,
		/// <summary>
		/// 4
		/// </summary>
		Rejected = 4
	}

	/// <summary>
	/// Type of planning: MRP = 1,MPS = 2
	/// </summary>
	public enum PlanTypeEnum
	{
		/// <summary>
		/// 1
		/// </summary>
		MRP = 1,
		/// <summary>
		/// 2
		/// </summary>
		MPS = 2
	}

	/// <summary>
	/// Schedule Method: Forward = 1,Backward = 2
	/// </summary>
	public enum ScheduleMethodEnum
	{
		/// <summary>
		/// 1
		/// </summary>
		Forward = 1,
		/// <summary>
		/// 2
		/// </summary>
		Backward = 2
	}

	/// <summary>
	/// DistributionMethodEnum: ByQuantity =1, ByMaterial = 2, ByPercent = 3, ByLeadTime = 4
	/// </summary>
	public enum DistributionMethodEnum
	{
		/// <summary>
		/// 1
		/// </summary>
		ByQuantity =1, 
		/// <summary>
		/// 2
		/// </summary>
		ByMaterial = 2, 
		/// <summary>
		/// 3
		/// </summary>
		ByPercent = 3, 
		/// <summary>
		/// 4
		/// </summary>
		ByLeadTime = 4
	}

	/// <summary>
	/// Type of Schedule: InfiniteScheduling= 0,LoadAveraging = 1,FiniteScheduling =2
	/// </summary>
	public enum ScheduleType
	{
		/// <summary>
		/// 0
		/// </summary>
		InfiniteScheduling= 0,
		/// <summary>
		/// 1
		/// </summary>
		LoadAveraging = 1,
		/// <summary>
		/// 2
		/// </summary>
		FiniteScheduling =2
	}

	/// <summary>
	/// Type of WC: Labor= 0,Machine = 1
	/// </summary>
	public enum WCType
	{
		/// <summary>
		/// 0: WC using Labor
		/// </summary>
		Labor= 0,
		/// <summary>
		/// 0: WC using Machine
		/// </summary>
		Machine = 1
	}

	/// <summary>
	/// Routing.Pacer using in routing: B= 'B',M = 'M',L = 'L'
	/// </summary>
	public enum PacerEnum
	{
		/// <summary>
		/// 'B': Both Machine and Labor
		/// </summary>
		B = 'B',
		/// <summary>
		/// 'M': Machine only
		/// </summary>
		M = 'M',
		/// <summary>
		/// 'L': Labor only
		/// </summary>
		L = 'L'
	}

	/// <summary>
	/// Level group of field: One = 1,Two = 2
	/// </summary>
	public enum GroupFieldLevel
	{
		/// <summary>
		/// 1: Level one
		/// </summary>
		One = 1,
		/// <summary>
		/// 2: Level two
		/// </summary>
		Two = 2
	}
	
	/// <summary>
	/// Sys_Menu_Entry.Type column enum VisibleBoth = 0, InvisibleBoth = 1, InvisibleMenu = 2, VisibleMenu = 3
	/// </summary>
	public enum MenuTypeEnum
	{
		/// <summary>
		/// 0 : visible in both menu and right assignment
		/// </summary>
		VisibleBoth = 0,
		/// <summary>
		/// 1 : invisible in both menu and right assignment
		/// </summary>
		InvisibleBoth = 1,
		/// <summary>
		/// 2 : invisible in menu but visible in right assignment
		/// </summary>
		InvisibleMenu = 2,
		/// <summary>
		/// 3 : visible in menu but invisible in right assignment
		/// </summary>
		VisibleMenu = 3
	}

// HACK: SonHT 2005-10-13: using System.Drawing.Printing.PaperKind
//	/// <summary>
//	/// PaperSize enum: A3 = 1,A4 = 2,Legal = 3,Letter = 4
//	/// </summary>
//	public enum PaperSizeEnum
//	{
//		/// <summary>
//		/// 1 : A3 paper size
//		/// </summary>
//		A3 = 1,
//		/// <summary>
//		/// 2 : A4 paper size
//		/// </summary>
//		A4 = 2,
//		/// <summary>
//		/// 3 : Legal paper size
//		/// </summary>
//		Legal = 3,
//		/// <summary>
//		/// 4 : Letter paper size
//		/// </summary>
//		Letter = 4
//	}
// END: SonHT 2005-10-DD

	/// <summary>
	/// Sys_Menu_Entry.ParentChild enum: RootMenu = 1,NormalMenu = 2,LeafMenu = 3,SpecialLeafMenu = 10
	/// </summary>
	public enum MenuParentChildEnum
	{
		/// <summary>
		/// 1: Root menu
		/// </summary>
		RootMenu = 1,
		/// <summary>
		/// 2: Normal menu (middle menu)
		/// </summary>
		NormalMenu = 2,
		/// <summary>
		/// 3: Leaf menu
		/// </summary>
		LeafMenu = 3,
		/// <summary>
		/// 10: Special menu (hidden menu)
		/// </summary>
		SpecialLeafMenu = 10
	}
	
	/// <summary>
	/// Character-case format in table framework: Normal = 0, Upper = 1, Lower = 2
	/// </summary>
	public enum CharacterCaseEnum
	{
		/// <summary>
		/// 0: Normal character
		/// </summary>
		Normal = 0,
		/// <summary>
		/// 1: Upper character
		/// </summary>
		Upper = 1,
		/// <summary>
		/// 2: Lower character
		/// </summary>
		Lower = 2
	}

	/// <summary>
	/// DCPResultDetail.Type: 0 - Running, 1 - Change category, 2 - CheckPoint
	/// </summary>
	public enum DCPResultTypeEnum
	{
		/// <summary>
		/// 0: Running time
		/// </summary>
		Running = 0,
		/// <summary>
		/// 1: Change category time
		/// </summary>
		ChangeCategory = 1,
		/// <summary>
		/// 2: Check point time
		/// </summary>
		CheckPoint = 2
	}

	/// <summary>
	/// Sys_ReportField.Type - Display type of field: Default, Bar Chart or Record Counter
	/// </summary>
	public enum FieldTypeEnum
	{
		/// <summary>
		/// Default display type
		/// </summary>
		Default = 0,
		/// <summary>
		/// Display field as Bar Chart
		/// </summary>
		BarChart = 1,
		/// <summary>
		/// Display field and record counter
		/// </summary>
		RecordCounter = 2
	}

	/// <summary>
	/// Specifies the border style of a report
	/// </summary>
	public enum TableBoderEnum
	{
		/// <summary>
		/// No border
		/// </summary>
		None = 0,
		/// <summary>
		/// Vertical line border
		/// </summary>
		Vertical = 1,
		/// <summary>
		/// Horizontal line border
		/// </summary>
		Horizontal = 2,
		/// <summary>
		/// Vertical and Horizontal line border
		/// </summary>
		Both = 3
	}

	/// <summary>
	/// Visibility Group Type Enum
	/// </summary>
	public enum VisibilityGroupTypeEnum
	{
		/// <summary>
		/// Nature group: Container control or C1TrueDBGrid
		/// </summary>
		Container = 1,
		/// <summary>
		/// Define group: Group of control
		/// </summary>
		GroupNormal = 2,
	}

	/// <summary>
	/// Visibility Item Type Enum
	/// </summary>
	public enum VisibilityItemTypeEnum
	{
		/// <summary>
		/// Column of C1TrueDBGrid
		/// </summary>
		ColumnTrueDBGrid = 1,
		/// <summary>
		/// Special item
		/// </summary>
		SpecialItem = 2,
	}

	/// <summary>
	/// Type of transaction
	/// </summary>
	public enum TransactionTypeEnum
	{
		/// <summary>
		/// Sale order transaction
		/// </summary>
		SaleOrder = 0,
		/// <summary>
		/// Return goods receive transaction
		/// </summary>
		SOReturnGoodsReceive = 1,
		/// <summary>
		/// Cancel commitment transaction
		/// </summary>
		SOCancelCommitment = 2,
		/// <summary>
		/// Confirm shipment transaction
		/// </summary>
		SOConfirmShipment = 3,
		/// <summary>
		/// Purchase order transaction
		/// </summary>
		POPurchaseOrder = 4,
		/// <summary>
		///	Purchase order receipt transaction
		/// </summary>
		POPurchaseOrderReceipts = 5,
		/// <summary>
		///	Return to vendor transaction
		/// </summary>
		POReturnToVendor = 6,
		/// <summary>
		/// Material Receipt transaction
		/// </summary>
		IVMaterialReceipt = 7,
		/// <summary>
		///	Material Issue transaction
		/// </summary>
		IVMaterialIssue = 8,
		/// <summary>
		///	Material Scrap transaction
		/// </summary>
		IVMaterialScrap = 9,
		/// <summary>
		/// Loc to loc transfer
		/// </summary>
		IVLocToLocTransfer = 10,
		/// <summary>
		///	Inventory Adjusment transaction
		/// </summary>
		IVInventoryAdjustment = 11,
		/// <summary>
		///public const string INSPECTION_RESULT = "IVInspectionResult";
		/// </summary>
		IVInspectionResult = 12,
		/// <summary>
		///public const string MRB_RESULT = "IVMRBResult";
		/// </summary>
		IVMRBResult = 13,
		/// <summary>
		/// Work order completion transaction
		/// </summary>
		PROWorkOrderCompletion = 14,
		/// <summary>
		///public const string WO_REVERSAL ="WOReversal";
		/// </summary>
		WOReversal = 15,
		/// <summary>
		/// Issue material for work order transaction
		/// </summary>
		PROIssueMaterial = 16,
		/// <summary>
		/// Shipping management transaction
		/// </summary>
		ShippingManagement = 17,
		/// <summary>
		/// Sale order commitment transaction
		/// </summary>
		SOCommitment = 18,
		/// <summary>
		/// 19. Miscellaneous Issue transaction
		/// </summary>
		IVMiscellaneousIssue = 19,
		RecoverableMaterial = 20,
		/// <summary>
		/// Shipping adjustment transaction
		/// </summary>
		ShippingAdjustment = 26,
		/// <summary>
		/// 27.Delete transaction
		/// </summary>
		DeleteTransaction = 27
}

	public enum OrderPolicyEnum
	{
		Daily,
		Weekly,
		Monthly,
		Quarterly,
		Yearly
	}

	/// <summary>
	/// Specifies identifiers to indicate the grouping method when running MPS
	/// </summary>
	public enum MPSGroupByEnum
	{
		/// <summary>
		/// Group by Hour
		/// </summary>
		ByHour = 0,
		/// <summary>
		/// Group by Day
		/// </summary>
		ByDay = 1
	}

	public enum TransactionHistoryType
	{
		/// <summary>
		/// Out, xuat hang khoi kho
		/// </summary>
		Out = 0,
		/// <summary>
		/// In, nhap hang vao kho
		/// </summary>
		In = 1,
		/// <summary>
		/// Both, vu xuat vua nhap 
		/// </summary>
		Both = 2,
		/// <summary>
		/// Booking, commit kho
		/// </summary>
		Booking = 3
}

	/// <summary>
	/// Type of PO Receipt
	/// </summary>
	public enum POReceiptTypeEnum
	{
		/// <summary>
		/// Receipt By Purchase Order
		/// </summary>
		ByPO = 0,
		/// <summary>
		/// Receipt By Item
		/// </summary>
		ByItem = 1,
		/// <summary>
		/// Receipt By Delivery Slip
		/// </summary>
		ByDeliverySlip = 2,
		/// <summary>
		/// Receipt By Invoice
		/// </summary>
		ByInvoice = 3,
		/// <summary>
		/// Receipt By Outside
		/// </summary>
		ByOutside = 4

	}

	/// <summary>
	/// Purpose
	/// </summary>
	public enum PurposeEnum
	{
		/// <summary>
		/// Xuat Ke Hoach
		/// </summary>
		 Plan = 1,
		/// <summary>
		/// Xuat Bat Thuong
		/// </summary>
		Misc = 2,
		/// <summary>
		/// Xuat Bu Hong
		/// </summary>
		Compensation = 3,
		/// <summary>
		/// Xuat tra lai nha cung cap
		/// </summary>
		ReturnToVendor = 4,
		/// <summary>
		/// Xuat tra lai cong doan truoc
		/// </summary>
		ReturnPrevious = 5,
		/// <summary>
		/// QC
		/// </summary>
		QC = 6,
		/// <summary>
		/// Xuat hang gia cong ngoai
		/// </summary>
		Outside = 7,
		/// <summary>
		/// Xuat ban hang
		/// </summary>
		Ship = 8,
		/// <summary>
		/// Nhap hang tu nha cung cap
		/// </summary>
		Receipt = 9,
		/// <summary>
		/// Nhap san pham hoan thanh
		/// </summary>
		Completion = 10,
		/// <summary>
		/// Nhap hang tra lai tu khach hang
		/// </summary>
		ReturnGoodReceipt = 11,
		/// <summary>
		/// Xuat Chuyen Kho
		/// </summary>
		LocToLoc = 12,
		/// <summary>
		/// Chuyen tu kho NG den kho OK
		/// </summary>
		Transfer = 13,
		/// <summary>
		/// Xuat Huy
		/// </summary>
		Scrap = 14,
		/// <summary>
		/// Adjustment
		/// </summary>
		Adjustment = 15,
		/// <summary>
		/// Commit
		/// </summary>
		Commit = 16,
		/// <summary>
		/// Cancel Commitment
		/// </summary>
		CancelCommitment = 17,
		/// <summary>
		/// Nhap linh kien tan dung sau huy
		/// </summary>
		TanDungLinhKienSauHuy = 18,
		/// <summary>
		/// Thanh ly phe lieu sau huy
		/// </summary>
		ThanhLyLinhKienSauHuy = 19,
		/// <summary>
		/// In Compensation for Customer
		/// </summary>
		CompensationForCustomer = 20
	}

	/// <summary>
	/// Location Type (Base on row ID)
	/// </summary>
	public enum LocationTypeEnum
	{
		/// <summary>
		/// Warehouse
		/// </summary>
		WareHouse= 1,
		/// <summary>
		/// Manufacturing
		/// </summary>
		Manufacturing = 2
	}

	/// <summary>
	/// BIN Type (Base on row ID)
	/// </summary>
	public enum BinTypeEnum
	{
		/// <summary>
		/// OK 
		/// </summary>
		OK = 1,
		/// <summary>
		/// No Good
		/// </summary>
		NG = 2,
		/// <summary>
		/// Destroy bin
		/// </summary>
		LS = 3,
		/// <summary>
		/// Incomming (Buffer bin)
		/// </summary>
		IN = 4
	}
	/// <summary>
	/// Purchase Order Vendor Delivery Type
	/// </summary>
	public enum PODeliveryTypeEnum
	{
		/// <summary>
		/// Daily
		/// </summary>
		Daily = 0,
		/// <summary>
		/// Weekly
		/// </summary>
		Weekly = 1,
		/// <summary>
		/// Monthly
		/// </summary>
		Monthly = 2
	}

	/// <summary>
	/// Day Of Week
	/// </summary>
	public enum DayOfWeekEnum
	{
		/// <summary>
		/// Sunday
		/// </summary>
		Sunday = 0,
		/// <summary>
		/// Monday
		/// </summary>
		Monday = 1,
		/// <summary>
		/// Tuesday
		/// </summary>
		Tuesday = 2,
		/// <summary>
		/// Wednesday
		/// </summary>
		Wednesday = 3,
		/// <summary>
		/// Thusday
		/// </summary>
		Thusday = 4,
		/// <summary>
		/// Friday
		/// </summary>
		Friday = 5,
		/// <summary>
		/// Saturday
		/// </summary>
		Saturday = 6
	}

	/// <summary>
	/// Cost Element Type
	/// </summary>
	public enum CostElementType
	{
		/// <summary>
		/// Machine
		/// </summary>
		Machine = 1,
		/// <summary>
		/// Labor
		/// </summary>
		Labor = 2,
		/// <summary>
		/// Raw Material
		/// </summary>
		Material = 3,
		/// <summary>
		/// Sub Material
		/// </summary>
		SubMaterial = 4,
		/// <summary>
		/// Over Head
		/// </summary>
		OverHead = 5
	}

	/// <summary>
	/// Freight Type
	/// </summary>
	public enum FreightType
	{
		/// <summary>
		/// ForPO
		/// </summary>
		ForPO = 1,
		/// <summary>
		/// ForInvoice
		/// </summary>
		ForInvoice = 0,
	}

	/// <summary>
	/// Planning Group By
	/// </summary>
	public enum PlanningGroupBy
	{
		/// <summary>
		/// By Date
		/// </summary>
		ByDate = 0,
		/// <summary>
		/// By Hour
		/// </summary>
		ByHour = 1,
		/// <summary>
		/// By Shift
		/// </summary>
		ByShift = 2
}

	/// <summary>
	/// Additional Charge Purpose
	/// </summary>
	public enum ACPurpose
	{
		/// <summary>
		/// Freight
		/// </summary>
		Freight = 1,
		/// <summary>
		/// Import Tax
		/// </summary>
		 ImportTax= 2,
		/// <summary>
		/// Credit Note
		/// </summary>
		CreditNote= 3,
		/// <summary>
		/// Debit Note
		/// </summary>
		DebitNote= 4
	}

	/// <summary>
	/// Additional Charge Object
	/// </summary>
	public enum ACObject
	{
		/// <summary>
		/// Receipt Transaction
		/// </summary>
		ReceiptTransaction = 1,
		/// <summary>
		/// Item Inventory
		/// </summary>
		ItemInventory = 2
	}
	/// <summary>
	/// Type for Sale Order (Import/Export)
	/// </summary>
	public enum SOType
	{
		/// <summary>
		/// Import
		/// </summary>
		Import = 1,
		/// <summary>
		/// Export
		/// </summary>
		Export = 2
	}
	/// <summary>
	/// Type for Purchase Order (Domestic/Import/Outside)
	/// </summary>
	public enum POType
	{
		/// <summary>
		/// Domestic
		/// </summary>
		Domestic = 1,
		/// <summary>
		/// Import
		/// </summary>
		Import = 2,
		/// <summary>
		/// Outside
		/// </summary>
		Outside = 3
	}
	/// <summary>
	/// View Type for Shipping Management form
	/// </summary>
	public enum ShipViewType
	{
		/// <summary>
		/// Print Invoice
		/// </summary>
		PrintInvoice = 0,
		/// <summary>
		/// Shipping
		/// </summary>
		Shipping = 1
	}
	/// <summary>
	/// Receipt purose
	/// </summary>
	public enum ReceiptPurpose
	{
		/// <summary>
		/// Plan
		/// </summary>
		Plan = 0,
		/// <summary>
		/// Compensation
		/// </summary>
		Compensation = 1
	}
	/// <summary>
	/// Purpose for Post date configuration table
	/// </summary>
	public enum PostDateConfigPurpose
	{
		/// <summary>
		/// Config for postdate
		/// </summary>
		PostDate,
		/// <summary>
		/// Config for search form condition
		/// </summary>
		SearchCondition
	}

    /// <summary>
    /// Permission for menu of system
    /// </summary>
    public enum MenuPermission
    {
        /// <summary>
        ///     No permission
        /// </summary>
        None = 0,
        /// <summary>
        /// View
        /// </summary>
        View = 1,
        /// <summary>
        ///     Full permission
        /// </summary>
        All = 31,
    }
}