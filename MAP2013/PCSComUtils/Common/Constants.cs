using System.Drawing.Printing;

// using System;

namespace PCSComUtils.Common
{
    /// <summary>
    /// Summary description for Constants.
    /// </summary>

    #region Constants
    public sealed class IV_DockToStockTable
    {
        public const string TABLE_NAME = "IV_DockToStock";
        public const string MASTERLOCATIONID_FLD = "MasterLocationID";
        public const string ACCEPTLOCID_FLD = "AcceptLocID";
        public const string REJECTLOCID_FLD = "RejectLocID";
        public const string REWORKMFGLOCID_FLD = "ReworkMfgLocID";
        public const string RTVLOCID_FLD = "RTVLocID";
        public const string RTVREWORKLOCID_FLD = "RTVReworkLocID";
        public const string RTVREPLACELOCID_FLD = "RTVReplaceLocID";
        public const string SCRAPLOCID_FLD = "ScrapLocID";
        public const string USEASISLOCID_FLD = "UseAsIsLocID";
        public const string PURSQALOCID_FLD = "PurSQALocID";
        public const string PURNONSQALOCID_FLD = "PurNonSQALocID";
        public const string PURNONSQAINSLOCID_FLD = "PurNonSQAInsLocID";
        public const string PURLINEINSLOCID_FLD = "PurLineInsLocID";
        public const string MFGSQALOCID_FLD = "MfgSQALocID";
        public const string MFGNONSQALOCID_FLD = "MfgNonSQALocID";
        public const string MFGNONSQAINSLOCID_FLD = "MfgNonSQAInsLocID";
        public const string MFGLINEINSLOCID_FLD = "MfgLineInsLocID";
        public const string PACKINGLOCID_FLD = "PackingLocID";
        public const string DOCKTOSTOCKID_FLD = "DockToStockID";
        public const string CCNID_FLD = "CCNID";
        public const string PACKINGBINID_FLD = "PackingBinID";
    }
    public sealed class ITM_CostTable
    {
        public const string TABLE_NAME = "ITM_Cost";
        public const string MANUAL_FLD = "Manual";
        public const string SINGLEROLLUPDATE_FLD = "SingleRollUpDate";
        public const string MULTIROLLUPDATE_FLD = "MultiRollUpDate";
        public const string COSTSETUPDATE_FLD = "CostSetupDate";
        public const string COMMATERIAL01_FLD = "ComMaterial01";
        public const string COMMATERIALOVERHEAD02_FLD = "ComMaterialOverHead02";
        public const string COMMACHINESETUP03_FLD = "ComMachineSetup03";
        public const string COMMACHINESETUPFIXED04_FLD = "ComMachineSetupFixed04";
        public const string COMMACHINESETUPVAR05_FLD = "ComMachineSetupVar05";
        public const string COMMACHINERUN06_FLD = "ComMachineRun06";
        public const string COMMACHINEFIXED07_FLD = "ComMachineFixed07";
        public const string COMMACHINEVARIABLE08_FLD = "ComMachineVariable08";
        public const string COMLABORSETUP09_FLD = "ComLaborSetup09";
        public const string COMLABORSETUPFIXED10_FLD = "ComLaborSetupFixed10";
        public const string COMLABORSETUPVARIABLE11_FLD = "ComLaborSetupVariable11";
        public const string COMLABORRUN12_FLD = "ComLaborRun12";
        public const string COMLABORFIXED13_FLD = "ComLaborFixed13";
        public const string COMLABORVARIABLE14_FLD = "ComLaborVariable14";
        public const string COMOUTSIDEPROC15_FLD = "ComOutsideProc15";
        public const string COMASSEMBLYSCRAP16_FLD = "ComAssemblyScrap16";
        public const string COMSHRINK17_FLD = "ComShrink17";
        public const string COMFREIGHT18_FLD = "ComFreight18";
        public const string COMUSERSTANDARD1_19_FLD = "ComUserStandard1_19";
        public const string COMUSERSTANDARD2_20_FLD = "ComUserStandard2_20";
        public const string COMTOTALAMOUNT21_FLD = "ComTotalAmount21";
        public const string VADDCOSTMATERIAL01_FLD = "VAddCostMaterial01";
        public const string VADDCOSTMATERIALOVERHEAD02_FLD = "VAddCostMaterialOverHead02";
        public const string VADDCOSTMACHINESETUP03_FLD = "VAddCostMachineSetup03";
        public const string VADDCOSTMACHINESETUPFIXED04_FLD = "VAddCostMachineSetupFixed04";
        public const string VADDCOSTMACHINESETUPVAR05_FLD = "VAddCostMachineSetupVar05";
        public const string VADDCOSTMACHINERUN06_FLD = "VAddCostMachineRun06";
        public const string VADDCOSTMACHINEFIXED07_FLD = "VAddCostMachineFixed07";
        public const string VADDCOSTMACHINEVARIABLE08_FLD = "VAddCostMachineVariable08";
        public const string VADDCOSTLABORSETUP09_FLD = "VAddCostLaborSetup09";
        public const string VADDCOSTLABORSETUPFIXED10_FLD = "VAddCostLaborSetupFixed10";
        public const string VADDCOSTLABORSETUPVARIABLE11_FLD = "VAddCostLaborSetupVariable11";
        public const string VADDCOSTLABORRUN12_FLD = "VAddCostLaborRun12";
        public const string VADDCOSTLABORFIXED13_FLD = "VAddCostLaborFixed13";
        public const string VADDCOSTLABORVARIABLE14_FLD = "VAddCostLaborVariable14";
        public const string VADDCOSTOUTSIDEPROC15_FLD = "VAddCostOutsideProc15";
        public const string VADDCOSTASSEMBLYSCRAP16_FLD = "VAddCostAssemblyScrap16";
        public const string VADDCOSTSHRINK17_FLD = "VAddCostShrink17";
        public const string VADDCOSTFREIGHT18_FLD = "VAddCostFreight18";
        public const string VADDCOSTUSERSTANDARD1_19_FLD = "VAddCostUserStandard1_19";
        public const string VADDCOSTUSERSTANDARD2_20_FLD = "VAddCostUserStandard2_20";
        public const string VADDCOSTTOTALAMOUNT21_FLD = "VAddCostTotalAmount21";
        public const string COMPONENTLABORRUNHOUR_FLD = "ComponentLaborRunHour";
        public const string COMPONENTLABORSETUPHOUR_FLD = "ComponentLaborSetupHour";
        public const string COMPONENTMACHINERUNHOUR_FLD = "ComponentMachineRunHour";
        public const string COMPONENTMACHINESETUPHOUR_FLD = "ComponentMachineSetupHour";
        public const string VALUEADDEDLABORRUNHOUR_FLD = "ValueAddedLaborRunHour";
        public const string VALUEADDEDLABORSETUPHOUR_FLD = "ValueAddedLaborSetupHour";
        public const string VALUEADDEDMACHINERUNHOUR_FLD = "ValueAddedMachineRunHour";
        public const string VALUEADDEDMACHINESETUPHOUR_FLD = "ValueAddedMachineSetupHour";
        public const string ITEMLABORRUNHOUR_FLD = "ItemLaborRunHour";
        public const string ITEMLABORSETUPHOUR_FLD = "ItemLaborSetupHour";
        public const string ITEMMACHINERUNHOUR_FLD = "ItemMachineRunHour";
        public const string ITEMMACHINESETUPHOUR_FLD = "ItemMachineSetupHour";
        public const string BCR_FLD = "BCR";
        public const string ITEMCOSTMATERIAL01_FLD = "ItemCostMaterial01";
        public const string ITEMCOSTMATERIALOVERHEAD02_FLD = "ItemCostMaterialOverHead02";
        public const string ITEMCOSTMACHINESETUP03_FLD = "ItemCostMachineSetup03";
        public const string ITEMCOSTMACHINESETUPFIXED04_FLD = "ItemCostMachineSetupFixed04";
        public const string ITEMCOSTMACHINESETUPVAR05_FLD = "ItemCostMachineSetupVar05";
        public const string ITEMCOSTMACHINERUN06_FLD = "ItemCostMachineRun06";
        public const string ITEMCOSTMACHINEFIXED07_FLD = "ItemCostMachineFixed07";
        public const string ITEMCOSTMACHINEVARIABLE08_FLD = "ItemCostMachineVariable08";
        public const string ITEMCOSTLABORSETUP09_FLD = "ItemCostLaborSetup09";
        public const string ITEMCOSTLABORSETUPFIXED10_FLD = "ItemCostLaborSetupFixed10";
        public const string ITEMCOSTLABORSETUPVARIABLE11_FLD = "ItemCostLaborSetupVariable11";
        public const string ITEMCOSTLABORRUN12_FLD = "ItemCostLaborRun12";
        public const string ITEMCOSTLABORFIXED13_FLD = "ItemCostLaborFixed13";
        public const string ITEMCOSTLABORVARIABLE14_FLD = "ItemCostLaborVariable14";
        public const string ITEMCOSTOUTSIDEPROC15_FLD = "ItemCostOutsideProc15";
        public const string ITEMCOSTASSEMBLYSCRAP16_FLD = "ItemCostAssemblyScrap16";
        public const string ITEMCOSTSHRINK17_FLD = "ItemCostShrink17";
        public const string ITEMCOSTFREIGHT18_FLD = "ItemCostFreight18";
        public const string ITEMCOSTUSERSTANDARD1_19_FLD = "ItemCostUserStandard1_19";
        public const string ITEMCOSTUSERSTANDARD2_20_FLD = "ItemCostUserStandard2_20";
        public const string ITEMCOSTTOTALAMOUNT21_FLD = "ItemCostTotalAmount21";
        public const string PRODUCTID_FLD = "ProductID";
        public const string COSTID_FLD = "CostID";
        public const string CCNID_FLD = "CCNID";
        public const string COSTDESCRIPTION_FLD = "CostDescription";
    }
    public sealed class IV_MaterialScrapTable
    {
        public const string TABLE_NAME = "IV_MaterialScrap";
        public const string MASTERLOCATIONID_FLD = "MasterLocationID";
        public const string MATERIALSCRAPID_FLD = "MaterialScrapID";
        public const string POSTDATE_FLD = "PostDate";
        public const string COMMENT_FLD = "Comment";
        public const string PRODUCTID_FLD = "ProductID";
        public const string BINID_FLD = "BinID";
        public const string LOCATIONID_FLD = "LocationID";
        public const string STOCKUMID_FLD = "StockUMID";
        public const string CCNID_FLD = "CCNID";
        public const string TRANSNO_FLD = "TransNo";
        public const string LOT_FLD = "Lot";
        public const string SERIAL_FLD = "Serial";
        public const string AVAILABLEQUANTITY_FLD = "AvailableQuantity";
        public const string SCRAPQUANTITY_FLD = "ScrapQuantity";
    }
    public sealed class PRO_WorkOrderBomDetailTable
    {
        public const string TABLE_NAME = "PRO_WorkOrderBomDetail";
        public const string WORKORDERBOMDETAILID_FLD = "WorkOrderBomDetailID";
        public const string COMPONENTID_FLD = "ComponentID";
        public const string REQUIREDQUANTITY_FLD = "RequiredQuantity";
        public const string SHRINK_FLD = "Shrink";
        public const string LEADTIMEOFFSET_FLD = "LeadTimeOffset";
        public const string OPERATIONID_FLD = "OperationID";
        public const string WORKORDERBOMMASTERID_FLD = "WorkOrderBomMasterID";
        public const string STOCKUMID_FLD = "StockUMID";
        public const string LINE_FLD = "Line";

    }
    public sealed class PO_PurchaseRequisitionMasterTable
    {
        public const string TABLE_NAME = "PO_PurchaseRequisitionMaster";
        public const string PURCHASEREQUISITIONMASTERID_FLD = "PurchaseRequisitionMasterID";
        public const string ORDERDATE_FLD = "OrderDate";
        public const string DELIVERYDATE_FLD = "DeliveryDate";
        public const string VAT_FLD = "VAT";
        public const string IMPORTTAX_FLD = "ImportTax";
        public const string SPECIALTAX_FLD = "SpecialTax";
        public const string CODE_FLD = "Code";
        public const string CCNID_FLD = "CCNID";
        public const string REQUESTORID_FLD = "RequestorID";
        public const string APPROVERID_FLD = "ApproverID";
        public const string EXCHANGERATEID_FLD = "ExchangeRateID";
        public const string DELIVERYTERMSID_FLD = "DeliveryTermsID";
        public const string PAYMENTTERMSID_FLD = "PaymentTermsID";
        public const string CARRIERID_FLD = "CarrierID";
        public const string CURRENCYID_FLD = "CurrencyID";
        public const string TOTALIMPORTTAX_FLD = "TotalImportTax";
        public const string TOTALVAT_FLD = "TotalVAT";
        public const string TOTALSPECIALTAX_FLD = "TotalSpecialTax";
        public const string TOTALAMOUNT_FLD = "TotalAmount";
        public const string TOTALDISCOUNT_FLD = "TotalDiscount";
        public const string TOTALNETAMOUNT_FLD = "TotalNetAmount";
        public const string APPROVALDATE_FLD = "ApprovalDate";
        public const string PARTYID_FLD = "PartyID";
        public const string PARTYCONTACTID_FLD = "PartyContactID";
        public const string VENDORLOCID_FLD = "VendorLocID";
        public const string SHIPTOLOCID_FLD = "ShipToLocID";
        public const string INVTOLOCID_FLD = "InvToLocID";
        public const string SOURCE_FLD = "Source";
    }
    public sealed class PO_ItemVendorReferenceDetailTable
    {
        public const string TABLE_NAME = "PO_ItemVendorReferenceDetail";
        public const string ITEMVENDORREFERENCEDETAILID_FLD = "ItemVendorReferenceDetailID";
        public const string ITEMVENDORREFERENCEID_FLD = "ItemVendorReferenceID";
        public const string ENDDATE_FLD = "EndDate";
        public const string UNITPRICE_FLD = "UnitPrice";
        public const string FROMQUANTITY_FLD = "FromQuantity";
        public const string TOQUANTITY_FLD = "ToQuantity";
        public const string FROMPRICE_FLD = "FromPrice";
        public const string TOPRICE_FLD = "ToPrice";
    }
    public sealed class PO_PRDeliveryScheduleTable
    {
        public const string TABLE_NAME = "PO_PRDeliverySchedule";
        public const string DELIVERYSCHEDULEID_FLD = "DeliveryScheduleID";
        public const string DELIVERYLINE_FLD = "DeliveryLine";
        public const string SCHEDULEDATE_FLD = "ScheduleDate";
        public const string DELIVERYQUANTITY_FLD = "DeliveryQuantity";
        public const string RECEIVEDQUANTITY_FLD = "ReceivedQuantity";
        public const string PURCHASEREQUISITIONLINESID_FLD = "PurchaseRequisitionLinesID";
    }
    public sealed class PRO_IssueStdCostTable
    {
        public const string TABLE_NAME = "PRO_IssueStdCost";
        public const string ISSUESTDCOSTID_FLD = "IssueStdCostID";
        public const string POSTDATE_FLD = "PostDate";
        public const string TRANSTYPE_FLD = "TransType";
        public const string COSTMATERIAL01_FLD = "CostMaterial01";
        public const string COSTMATERIALOVERHEAD02_FLD = "CostMaterialOverHead02";
        public const string COSTMACHINESETUP03_FLD = "CostMachineSetup03";
        public const string COSTMACHINESETUPFIXED04_FLD = "CostMachineSetupFixed04";
        public const string COSTMACHINESETUPVAR05_FLD = "CostMachineSetupVar05";
        public const string COSTMACHINERUN06_FLD = "CostMachineRun06";
        public const string COSTMACHINEFIXED07_FLD = "CostMachineFixed07";
        public const string COSTMACHINEVARIABLE08_FLD = "CostMachineVariable08";
        public const string COSTLABORSETUP09_FLD = "CostLaborSetup09";
        public const string COSTLABORSETUPFIXED10_FLD = "CostLaborSetupFixed10";
        public const string COSTLABORSETUPVARIABLE11_FLD = "CostLaborSetupVariable11";
        public const string COSTLABORRUN12_FLD = "CostLaborRun12";
        public const string COSTLABORFIXED13_FLD = "CostLaborFixed13";
        public const string COSTLABORVARIABLE14_FLD = "CostLaborVariable14";
        public const string COSTOUTSIDEPROC15_FLD = "CostOutsideProc15";
        public const string COSTASSEMBLYSCRAP16_FLD = "CostAssemblyScrap16";
        public const string COSTSHRINK17_FLD = "CostShrink17";
        public const string COSTFREIGHT18_FLD = "CostFreight18";
        public const string COSTUSERSTANDARD1_19_FLD = "CostUserStandard1_19";
        public const string COSTUSERSTANDARD2_20_FLD = "CostUserStandard2_20";
        public const string COSTTOTALAMOUNT21_FLD = "CostTotalAmount21";
        public const string ISSUEMATERIALDETAILID_FLD = "IssueMaterialDetailID";
    }
    public sealed class v_PurchaseOrderOfItem
    {
        public const string VIEW_NAME = "v_PurchaseOrderOfItem";
        public const string PO_CODE = "PO_PurchaseOrderMasterCode";
        public const string ITEM_CODE = "ITM_ProductCode";
    }
    public sealed class v_ReceiptBySchedule
    {
        public const string VIEW_NAME = "v_ReceiptBySchedule";
        public const string PARTNUMBER_FLD = "PartNumber";
        public const string PARTNAME_FLD = "PartNumber";
        public const string MODEL_FLD = "Model";
    }
    public sealed class v_SOCancelCommitment
    {
        public const string VIEW_NAME = "v_SOCancelCommitment";
    }
    public sealed class v_SelectPurchaseOrders
    {
        public const string VIEW_NAME = "v_SelectPurchaseOrders";
    }
    public sealed class Constants
    {
        public const string ADMINISTRATORS = "Administrators";
        public const string ADMINISTRATORS_ROLE = "Administrators";
        public const string ALL_ROLE = "(All)";
        public const int CountPage = 100;
        public const string SQL_REPORT = "SQL";
        public const string CUSTOM_REPORT = "Custom Report";
        /// <summary>
        /// Dynamic Link Library
        /// </summary>
        public const string DLL_REPORT = "Dynamic Link Library";
        /// <summary>
        /// C# File
        /// </summary>
        public const string CSHARP_FILE_REPORT = "C# File";
        /// <summary>
        ///  AND 
        /// </summary>
        public const string AND = " AND ";

        /// <summary>
        /// Production Control System
        /// </summary>
        public const string APPLICATION_NAME = "Production Control System";

        public const string AVAILABLE_QTY_COL = "AvailableQuantity";
        public const int BACKGROUND_COLOUR_B = 205;
        public const int BACKGROUND_COLOUR_G = 250;
        public const int BACKGROUND_COLOUR_R = 255;

        /// <summary>
        /// 0
        /// </summary>
        public const int BIT_DEFAULT_VALUE = 0;

        /// <summary>
        /// bit
        /// </summary>
        public const string BOOLEAN_TYPE = "bit";

        public const string BUTTON_ADD_NAME = "btnAdd";
        public const string BUTTON_DELETE_NAME = "btnDelete";
        public const string BUTTON_EDIT_NAME = "btnEdit";
        public const string BUTTON_PRINT_NAME = "btnPrint";
        public const string BUTTON_SAVE_NAME = "btnSave";

        /// <summary>
        /// BY
        /// </summary>
        public const string BY = "BY";

        /// <summary>
        /// 0.00
        /// </summary>
        public const string CELL_NUMBER_FORMAT = "0.00";

        /// <summary>
        /// ]
        /// </summary>
        public const string CLOSE_SBRACKET = "]";

        /// <summary>
        /// ,
        /// </summary>
        public const string COMMA = ",";

        public const string COMMITED_QTY_COL = "CommitedQuantity";


        /// <summary>
        /// ..\\..\\PCS.config
        /// </summary>
        public const string CONFIG_PATH = "..\\..\\PCS.config";

        /// <summary>
        /// ConnectionString
        /// </summary>
        public const string CONNECTION_STRING = "ConnectionString";

        /// <summary>
        /// Copy of 
        /// </summary>
        public const string COPY_OF = "Copy of ";

        public const int COST_ELEMENT_IS_LEAF = 1;

        /// <summary>
        /// en-US
        /// </summary>
        public const string CULTURE_EN = "en-US";

        /// <summary>
        /// ja-JP
        /// </summary>
        public const string CULTURE_JP = "ja-JP";

        /// <summary>
        /// vi-VN
        /// </summary>
        public const string CULTURE_VN = "vi-VN";

        /// <summary>
        /// dd-MM-yyyy
        /// </summary>
        public const string DATETIME_FORMAT = "dd-MM-yyyy";

        /// <summary>
        /// dd-MM-yyyy HH:mm:ss
        /// </summary>
        //public const string DATETIME_FORMAT_HOUR = "dd-MM-yyyy HH:mm:ss";
        public const string DATETIME_FORMAT_HOUR = "dd-MM-yyyy HH:mm";

        /// <summary>
        /// datetime
        /// </summary>
        public const string DATETIME_TYPE = "datetime";

        /// <summary>
        /// ##############,0.0000
        /// </summary>
        public const string DECIMAL_LONG_FORMAT = "##############,0.0000";

        /// <summary>
        /// ##############,0.00
        /// </summary>
        public const string DECIMAL_NUMBERFORMAT = "##############,0.00";

        /// <summary>
        /// 200
        /// </summary>
        public const int DEFAULT_C1COMBO_DROPDOWNWIDTH = 200;

        /// <summary>
        /// 21
        /// </summary>
        public const int DEFAULT_ROW_HEIGHT = 21;

        /// <summary>
        /// Delete
        /// </summary>
        public const string DELETE_STR = "Delete";

        public const string DEMAND_QUANTITY_FLD = "DemandQuantity";
        public const string DETAILID_FLD = "DetailID";

        /// <summary>
        /// .
        /// </summary>
        public const string DOT = ".";

        /// <summary>
        /// ###,###.00
        /// </summary>
        public const string EDIT_NUM_MASK = "###,###.00";

        /// <summary>
        /// 13
        /// </summary>
        public const int ENTER_KEY_CHAR = 13;

        /// <summary>
        /// =
        /// </summary>
        public const string EQUAL = "=";

        public const string ERP_CONNECTION = "PCSMain.Properties.Settings.ERPConnectionString";

        /// <summary>
        /// 0
        /// </summary>
        public const string ERROR_MSG_TYPE = "0";

        public const string EXCEL_REPORT_LOCATION = "ExcelReports";

        /// <summary>
        /// -1
        /// </summary>
        public const int FALSE_VALUE = -1;

        /// <summary>
        /// GdiCharSet=
        /// </summary>
        public const string FONT_CHARSET = "GdiCharSet=";


        /// <summary>
        /// Name=
        /// </summary>
        public const string FONT_NAME = "Name=";

        /// <summary>
        /// Size=
        /// </summary>
        public const string FONT_SIZE = "Size=";

        /// <summary>
        /// Style=
        /// </summary>
        public const string FONT_STYLE = "Style=";

        /// <summary>
        /// Units=
        /// </summary>
        public const string FONT_UNITS = "Units=";

        /// <summary>
        /// GdiVerticalFont=
        /// </summary>
        public const string FONT_VERTICAL_FONT = "GdiVerticalFont=";

        public const int FORE_COLOUR_B = 0;
        public const int FORE_COLOUR_G = 0;
        public const int FORE_COLOUR_R = 0;

        /// <summary>
        ///  FROM 
        /// </summary>
        public const string FROM_STR = " FROM ";

        /// <summary>
        /// PRECISION
        /// </summary>
        public const string GET_FIELD_LENGTH = "PRECISION";

        /// <summary>
        /// GROUP BY
        /// </summary>
        public const string GROUP_BY = "GROUP BY";

        /// <summary>
        /// HAVING
        /// </summary>
        public const string HAVING = "HAVING";

        // HACK: SonHT 2005-10-13 Using PlanTypeEnum
        //		/// <summary>
        //		/// 1
        //		/// </summary>
        //		public const int PLANTYPE_MRP = 1;
        //		/// <summary>
        //		/// 2
        //		/// </summary>
        //		public const int PLANTYPE_MPS = 2;
        // END: SonHT 2005-10-13

        /// <summary>
        /// ID
        /// </summary>
        public const string ID_FIELD = "ID";

        /// <summary>
        ///  IN 
        /// </summary>
        public const string IN_KEYWORD = " IN ";

        public const int INCHE_TWIPS_RATE = 1440; // DungLA correct

        /// <summary>
        /// 1
        /// </summary>
        public const string INFO_MSG_TYPE = "1";

        /// <summary>
        /// Insert
        /// </summary>
        public const string INSERT_STR = "Insert";

        /// <summary>
        /// ###,##0 for integer data type
        /// </summary>
        public const string INT_DSP_NUM_MASK = "###,##0";

        /// <summary>
        /// int
        /// </summary>
        public const string INTEGER_TYPE = "int";

        /// <summary>
        /// ##############,0
        /// </summary>
        public const string INTERGER_NUMBERFORMAT = "##############,0";

        /// <summary>
        ///  INTO 
        /// </summary>
        public const string INTO_STR = " INTO ";

        /// <summary>
        /// %'
        /// </summary>
        public const string LIKE_CLOSE = "%'";

        /// <summary>
        ///  LIKE '
        /// </summary>
        public const string LIKE_OPEN = " LIKE '";

        public const string MASTERID_FLD = "MasterID";

        /// <summary>
        /// 08-2005
        /// </summary>
        public const string MONTH_YEAR_FORMAT = "MM-yyyy";

        public const string NEEDED_QTY_COL = "RequiredQuantity";

        /// <summary>
        /// nvarchar
        /// </summary>
        public const string NVARCHAR_TYPE = "nvarchar";

        public const string OLE_CONNECTION = "PCSMain.Properties.Settings.OleDbConnectionString";

        /// <summary>
        /// [
        /// </summary>
        public const string OPEN_SBRACKET = "[";

        /// <summary>
        /// ORDER
        /// </summary>
        public const string ORDER = "ORDER";

        public const string OTHERDBB_CONNECTION = "PCSMain.Properties.Settings.OtherDbConnectionString";

        /// <summary>
        /// pwd
        /// </summary>
        public const string PASSWORD_STR = "pwd";

        /// <summary>
        /// PCS.log
        /// </summary>
        public const string PCS_LOG_FILE = "PCS.log";

        public const string PO_NUMBERFORMAT = "PO_NUMBERFORMAT";
        public const string POSTDATE_FLD = "PostDate";
        public const string QUANTITY_FLD = "Quantity";
        public const string RemainNeededQuantity_COL = "RemainNeededQuantity";
        public const decimal REPORT_DEFAULT_BOTTOM = 1;
        public const decimal REPORT_DEFAULT_GUTTER = 1;
        public const bool REPORT_DEFAULT_GUTTER_POSITION = false;
        public const decimal REPORT_DEFAULT_LEFT = 1;
        public const bool REPORT_DEFAULT_ORIENTATION = false;
        public const int REPORT_DEFAULT_PAPER_SIZE = (int) PaperKind.A4;
        public const decimal REPORT_DEFAULT_RIGHT = 1;
        public const int REPORT_DEFAULT_TABLE_BORDER = 0;

        /// <summary>
        /// Constant for report
        /// </summary>
        public const decimal REPORT_DEFAULT_TOP = 1;

        public const string REPORT_DEFINITION_STORE_LOCATION = "ReportDefinition";

        /// <summary>
        /// | 
        /// </summary>
        public const char REPORT_FONT_SEPARATOR = '|';

        /// <summary>
        /// ReportHistory_
        /// </summary>
        public const string REPORT_HISTORY_PREFIX = "ReportHistory_";

        /// <summary>
        /// Right to add
        /// </summary>
        public const int RIGHT_ADD = 2;

        public const int RIGHT_DELETE = 8;

        /// <summary>
        /// Right to edit
        /// </summary>
        public const int RIGHT_EDIT = 4;

        public const int RIGHT_PRINT = 16;

        /// <summary>
        /// Right to view
        /// </summary>
        public const int RIGHT_VIEW = 1;

        /// <summary>
        /// SELECT
        /// </summary>
        public const string SELECT_STR = "SELECT";

        public const string SELECTED_STR = "SELECTED";


        //BACKGROUND_COLOUR

        // New color is Color.LemonChiffon

        public const string SO_NUMBERFORMAT = "SO_NUMBERFORMAT";
        public const string SO_RG_NUMBERFORMAT = "SO_RG_NUMBERFORMAT";

        /// <summary>
        /// string
        /// </summary>
        public const string STRING_TYPE = "string";

        /// <summary>
        /// super
        /// </summary>
        public const string SUPER_ADMIN_USER = "super";

        public const string SUPPLY_QUANTITY_FLD = "SupplyQuantity";
        public const string TRANSNO_FLD = "TransNo";

        /// <summary>
        /// POPurchaseOrder
        /// </summary>
        public const string TRANTYPE_POPURCHASEORDER = "POPurchaseOrder";

        /// <summary>
        /// POPurchaseOrderReceipts
        /// </summary>
        public const string TRANTYPE_POPURCHASEORDERRECEIPTS = "POPurchaseOrderReceipts";

        /// <summary>
        /// POReturnToVendor
        /// </summary>
        public const string TRANTYPE_PORETURNTOVENDOR = "POReturnToVendor";

        /// <summary>
        /// SaleOrder
        /// </summary>
        public const string TRANTYPE_SALEORDER = "SaleOrder";

        /// <summary>
        /// SOReturnGoodsReceive
        /// </summary>
        public const string TRANTYPE_SOCANCELCOMMITMENT = "SOCancelCommitment";

        /// <summary>
        /// SOReturnGoodsReceive
        /// </summary>
        public const string TRANTYPE_SORETURNGOODSRECEIVE = "SOReturnGoodsReceive";

        /// <summary>
        /// 1
        /// </summary>
        public const int TRUE_VALUE = 1;

        /// <summary>
        /// U
        /// </summary>
        // public const string UPDATE_ACTION = "U";
        /// <summary>
        /// I
        /// </summary>
        // public const string INSERT_ACTION = "I";
        /// <summary>
        /// D
        /// </summary>
        // public const string DELETE_ACTION = "D";
        /// <summary>
        /// Update
        /// </summary>
        public const string UPDATE_STR = "Update";

        /// <summary>
        /// uid
        /// </summary>
        public const string USERNAME_STR = "uid";

        /// <summary>
        /// VALUE
        /// </summary>
        public const string VALUE_FIELD = "VALUE";

        /// <summary>
        /// varchar
        /// </summary>
        public const string VARCHAR_TYPE = "varchar";

        /// <summary>
        /// _
        /// </summary>
        public const char VIEW_TABLE_FILTER_SEPARATOR = '_';

        /// <summary>
        /// #
        /// </summary>
        public const char VIEW_TABLE_ITEM_SEPARATOR = '#';

        /// <summary>
        /// WHERE
        /// </summary>
        public const string WHERE_KEYWORD = "WHERE";

        /// <summary>
        /// " " (white space)
        /// </summary>
        public const string WHITE_SPACE = " ";

        /// <summary>
        /// YYYYMMDD0000
        /// </summary>
        public const string YYYYMMDD0000 = "YYYYMMDD0000";
    }

    #endregion

    #region May be change to Enum or remove

    //Constant for system parameters (sys_Param table)
    public sealed class SystemParam
    {
        public const string ACCOUNT = "Account";
        public const string ADDRESS = "Address";
        public const string BANK_ADDR = "BankAddr";
        public const string BANK_NAME = "BankName";
        public const string COMPANY_FULL_NAME = "CompanyFullName";
        public const string COMPANY_NAME = "CompanyName";
        public const string DB_VERSION = "DBVersion";
        public const string FAX = "Fax";
        public const string OSR_ACTIVE_DATE = "OrderSummaryReportActiveDate";
        public const string OSR_VERSION = "OrderSummaryReportVersion";
        public const string TEL = "Tel";
    }


    // TODO: Add constant for table name or view name	
    public sealed class PCSSortType
    {
        public const int ASCENDING = 1;
        public const int DESCENDING = 2;
        public const int NONE = 0;
    }

    public sealed class PCSAligmentType
    {
        /// <summary>
        /// 1: Center alignment
        /// </summary>
        public const int CENTER = 1;

        /// <summary>
        /// 0: Left alignment
        /// </summary>
        public const int LEFT = 0;

        /// <summary>
        /// 3 (DungLA order for Report Framework)
        /// </summary>
        public const int NONE = 3;

        /// <summary>
        /// 2: Right alignment
        /// </summary>
        public const int RIGHT = 2;
    }

    #endregion

    public sealed class ErrorCode
    {
        #region YES_NO_MESSAGE = 1000;

        /// <summary>
        /// The format of Allocation Excel file is not correct
        /// </summary>
        public const int ALLOCATION_EXCEL_FILE_NOT_CORRECT = YES_NO_MESSAGE + 507;

        /// <summary>
        /// Can not read excel file
        /// </summary>
        public const int CAN_NOT_READ_EXCEL_FILE = YES_NO_MESSAGE + 506;

        /// <summary>
        /// Unable to compile C# file. Please try again
        /// </summary>
        public const int COMPILE_ERROR = YES_NO_MESSAGE + 48; // Unable to compile C# file. Please try again	

        /// <summary>
        /// Converting has been completed successfully
        /// </summary>
        public const int CONVERTED_SUCCESSFULLY = YES_NO_MESSAGE + 494;

        /// <summary>
        /// Unable to create new application domain. Please try again
        /// </summary>
        public const int CREATE_DOMAIN_ERROR = YES_NO_MESSAGE + 49;
                         // Unable to create new application domain. Please try again

        /// <summary>
        /// Unable to create remote object. Please try again
        /// </summary>
        public const int CREATE_REMOTE_INSTANCE_ERROR = YES_NO_MESSAGE + 50;
                         // Unable to create remote object. Please try again

        /// <summary>
        /// Cannot delete a group that have report inside
        /// </summary>						
        public const int ERROR_CANNOT_DELETE_GROUP = YES_NO_MESSAGE + 64;
                         // Cannot delete a group that have report inside

        /// <summary>
        /// Unabled to export report to Excel. Please try again later.
        /// </summary>
        public const int ERROR_EXPORT_EXCEL = YES_NO_MESSAGE + 43;
                         // Unabled to export report to Excel. Please try again later.

        /// <summary>
        /// Please correct the numeric value
        /// </summary>						 
        public const int ERROR_NUMBER_OVERFLOW = YES_NO_MESSAGE + 65; // Please correct the numeric value

        /// <summary>
        /// Old Password is not correct
        /// </summary>
        public const int INVALID_PASSWORD = YES_NO_MESSAGE + 23; // Old Password is not correct

        /// <summary>
        /// Unable to invoke method. Please try again
        /// </summary>
        public const int INVOKE_METHOD_ERROR = YES_NO_MESSAGE + 51; // Unable to invoke method. Please try again

        /// <summary>
        /// Item serial not remain any more
        /// </summary>
        public const int ITEM_SERIAL_NO_REMAIN = YES_NO_MESSAGE + 190;

        /// <summary>
        /// Please enter user name and password 
        /// </summary>
        public const int LOGIN_MANDATORY_INVALID = YES_NO_MESSAGE + 423;

        /// <summary>
        /// Please enter data into Percentage column
        /// </summary>
        public const int MESSAGE_ACDS_PLS_INPUT_PERCENTAGE = YES_NO_MESSAGE + 411;
                         //Please enter information into Percentage column

        /// <summary>
        /// Total Percent must be smaller than 100
        /// </summary>
        public const int MESSAGE_ACDS_TOTAL_PERCENT_MUST_BE_SMALLER_THAN_ONE_HUNDRED = YES_NO_MESSAGE + 405;
                         //Total Percent must be smaller than 100

        /// <summary>
        /// Your account was expired, please contact the administrator
        /// </summary>
        public const int MESSAGE_ACOUNT_EXPIRE = YES_NO_MESSAGE + 55;
                         // Your account was expired, please contact the administrator

        /// <summary>
        /// Your account is inactive
        /// </summary>
        public const int MESSAGE_ACOUNT_NOTACTIVE = YES_NO_MESSAGE + 54; // Your account is inactive

        /// <summary>
        /// Do you really want to change Return No.?
        /// </summary>
        public const int MESSAGE_ADDITION_CHARGE_CHANGE_RETURN_NO = YES_NO_MESSAGE + 528;

        /// <summary>
        /// Your changes have been saved into database
        /// </summary>
        public const int MESSAGE_AFTER_SAVE_DATA = YES_NO_MESSAGE + 22; // Your changes have been saved into database

        /// <summary>
        ///  All selected rows have been converted successfully
        /// Exam: All selected rows have been converted successfully
        /// </summary>
        public const int MESSAGE_ALL_CPO_CONVERTED = YES_NO_MESSAGE + 484;

        /// <summary>
        /// Do you want to re-login to apply menu changes
        /// Exam: Do you want to re-login to apply menu changes
        /// </summary>
        public const int MESSAGE_APPLY_NEW_MENU = YES_NO_MESSAGE + 478;

        /// <summary>
        /// Do you really want to allocate Costing for this Period
        /// </summary>
        public const int MESSAGE_ASK_FOR_ALLOCATE_COST = YES_NO_MESSAGE + 499;

        /// <summary>
        /// Do you really want to generate accounting information
        /// </summary>
        public const int MESSAGE_ASK_GEN_ACCOUNTING = YES_NO_MESSAGE + 550;

        /// <summary>
        /// At least one selected Work Center does not belong to the selected Production Line
        /// </summary>
        public const int MESSAGE_AT_LEAST_WC_INVALID = YES_NO_MESSAGE + 515;

        /// <summary>
        /// Exist the at least item to pack
        /// </summary>
        public const int MESSAGE_ATLEAST_ITEMTOPACK = YES_NO_MESSAGE + 72; // Exist the at least item to pack

        /// <summary>
        /// Exist the at least pack to ship
        /// </summary>
        public const int MESSAGE_ATLEAST_PACKTOSHIP = YES_NO_MESSAGE + 95; // Exist the at least pack to ship

        /// <summary>
        /// This report will be printed as an attachment. Make sure that you loaded 1 Sales Order Invoice and enough paper for the attachment
        /// Exam: This report will be printed as an attachment. Make sure that you loaded 1 Sales Order Invoice and enough paper for the attachment
        /// </summary>
        public const int MESSAGE_ATTACHMENT_SOINVOICE_REPORT = YES_NO_MESSAGE + 458;

        /// <summary>
        /// There is no available quantity in inventory 
        /// </summary>
        public const int MESSAGE_AVAILABLE_QTY_MUST_GREATER_THAN_ZERO = YES_NO_MESSAGE + 401;
                         //There is no available quantity in inventory

        /// <summary>
        /// The available of item was used in some transaction
        /// </summary>
        public const int MESSAGE_AVAILABLE_WAS_USED_AFTER_POSTDATE = YES_NO_MESSAGE + 487;

        /// <summary>
        /// Deleting all Sale Orders is not allowed
        /// </summary>
        public const int MESSAGE_AVOID_SO_CANNOT_SELECT_ALL = YES_NO_MESSAGE + 497;

        /// <summary>
        /// You must select at least one line to cancel 
        /// </summary>
        public const int MESSAGE_AVOID_SO_SELECT_ATLEAST_ONE = YES_NO_MESSAGE + 496;

        /// <summary>
        /// SQL Query syntax is not correct 
        /// </summary>
        public const int MESSAGE_BAD_SQL_QUERY_IN_REPORT = YES_NO_MESSAGE + 422;

        /// <summary>
        /// Base Amount must be smaller than Distribution Amount 
        /// </summary>
        public const int MESSAGE_BASE_AMOUNT_GREATER_THAN_DISTRIBUTION = YES_NO_MESSAGE + 400;
                         //Base Amount cannot greater than Distribution Amount

        /// <summary>
        /// Eff.Begin day must be smaller than Eff.End day
        /// </summary>
        public const int MESSAGE_BOM_BEGIN_END_DAY = YES_NO_MESSAGE + 164;
                         //Eff.Begin day must be smaller than Eff.End day	

        /// <summary>
        /// You have to select a component or delete row's information
        /// </summary>
        public const int MESSAGE_BOM_COMPONENTNOTEXIST = YES_NO_MESSAGE + 161; //The component is not exist

        /// <summary>
        /// Can not insert duplicate component for the product's BOM
        /// </summary>
        public const int MESSAGE_BOM_DUPLICATECOMPONENT = YES_NO_MESSAGE + 191;

        /// <summary>
        /// Bill of material must have at least one component
        /// </summary>
        public const int MESSAGE_BOM_EXISTATLISTROW = YES_NO_MESSAGE + 158;
                         //Bill of material must have at least one component

        /// <summary>
        /// The product is not exist to setup BOM
        /// </summary>
        public const int MESSAGE_BOM_PRODUCTNOTEXIST = YES_NO_MESSAGE + 160; //The product is not exist to setup BOM

        /// <summary>
        /// Quantity of BOM must greater than zero
        /// </summary>
        public const int MESSAGE_BOM_QUANTITY = YES_NO_MESSAGE + 159; //Quantity of BOM must greater than zero

        /// <summary>
        /// The percent must be a number between 0 and 100
        /// </summary>
        public const int MESSAGE_BOM_SHRINK = YES_NO_MESSAGE + 162; //The shrink is a number between 1 and 100

        /// <summary>
        /// Can not create BOM from the item's parent component, looping BOM
        /// </summary>
        public const int MESSAGE_BOM_WRONGBUSINESS = YES_NO_MESSAGE + 163;
                         //Can not create BOM from the item's parent component, looping BOM

        /// <summary>
        /// The input value must be between
        /// </summary>
        public const int MESSAGE_C1NUMBER_INPUT_VALUE = YES_NO_MESSAGE + 435; //The input value must be in 

        /// <summary>
        /// or
        /// </summary>
        public const int MESSAGE_C1NUMBER_OR_MSG = YES_NO_MESSAGE + 436; //or meesage

        /// <summary>
        /// You should select smaller or equal than 400 rows per a commission times to avoid time out error. 
        /// </summary>
        public const int MESSAGE_CAN_NOT_COMMIT = YES_NO_MESSAGE + 526;

        /// <summary>
        /// Can not delete Actual Cost
        /// </summary>
        public const int MESSAGE_CAN_NOT_DELETE = YES_NO_MESSAGE + 501;

        /// <summary>
        /// The postdate must be greater than or equal @
        /// </summary>
        public const int MESSAGE_CAN_NOT_EDIT_POSTDATE = YES_NO_MESSAGE + 534;

        /// <summary>
        /// Are you sure to cancel commitment
        /// </summary>
        public const int MESSAGE_CANCELCOMIIT_AREYOURSURE = YES_NO_MESSAGE + 122; // Are you sure to cancel commitment

        /// <summary>
        /// At least one item checked before cancel commitment
        /// </summary>
        public const int MESSAGE_CANCELCOMMIT_ATLISTITEMCHECK = YES_NO_MESSAGE + 121;
                         // At least one item checked before cancel commitment

        /// <summary>
        /// Adding child for the selected Element is not allowed
        /// </summary>
        public const int MESSAGE_CANNOT_ADD_CHILD_IN_MATERIAL_COST_ELEMENT = YES_NO_MESSAGE + 498;

        /// <summary>
        /// Can not add child for this Cost Element because it was used in another transaction
        /// </summary>
        public const int MESSAGE_CANNOT_ADD_CHILDREN_IN_COST_ELEMENT = YES_NO_MESSAGE + 492;

        /// <summary>	
        /// Can not change Production Line because some Work Order Lines was released
        /// </summary>
        public const int MESSAGE_CANNOT_CHANGE_PRODUCTION_LINE = YES_NO_MESSAGE + 520;

        /// <summary>
        /// Cannot insert more than one record for Credit and Debit in same transaction
        /// </summary>
        public const int MESSAGE_CANNOT_CREDIT_DEBIT_ENTRIES = YES_NO_MESSAGE + 543;

        /// <summary>
        /// You cannot delete this row because it aleady has delivered
        /// </summary>
        public const int MESSAGE_CANNOT_DELDELIVERY = YES_NO_MESSAGE + 81;
                         // You cannot delete this row because it aleady has delivered

        /// <summary>
        /// Can not delete Cost Center Rate
        /// </summary>
        public const int MESSAGE_CANNOT_DELETE_COST_CENTER_RATE = YES_NO_MESSAGE + 490;

        /// <summary>
        /// Can not delete this Cost Element because it was used in another transaction
        /// </summary>
        public const int MESSAGE_CANNOT_DELETE_COST_ELEMENT = YES_NO_MESSAGE + 491;

        /// <summary>
        /// Cannot delete this transaction because product was returned to vendor (@)
        /// </summary>
        public const int MESSAGE_CANNOT_DELETE_RECEIPT = YES_NO_MESSAGE + 540;

        /// <summary>
        /// Can not delete the row which had Additional Charge already
        /// </summary>
        public const int MESSAGE_CANNOT_DELETE_SALE_ORDER_BECAUSE_ADDITIONAL_CHARGE = YES_NO_MESSAGE + 299;
                         // You cannot delete this row because it has already had additional charge

        /// <summary>
        /// Can not delete the row which had Delivery Schedule
        /// </summary>
        public const int MESSAGE_CANNOT_DELETE_SALE_ORDER_LINE_BECAUSE_DELIVERY_SCHEDULE = YES_NO_MESSAGE + 300;
                         // You cannot delete this row because it has already had delivery schedule

        /// <summary>
        /// You cannot delete this row because it aleady has been posted
        /// </summary>
        public const int MESSAGE_CANNOT_DELETE_SALE_ORDER_LINE_BECAUSE_POSTED = YES_NO_MESSAGE + 86;
                         // You cannot delete this row because it aleady has been posted

        /// <summary>
        /// You cannot delete this row because it aleady has been released
        /// </summary>
        public const int MESSAGE_CANNOT_DELETE_SALE_ORDER_LINE_BECAUSE_RELEASED = YES_NO_MESSAGE + 85;
                         // You cannot delete this row because it aleady has been released

        /// <summary>
        /// Cannot edit properties of fields when report is use template file
        /// </summary>
        public const int MESSAGE_CANNOT_EDIT_FIELD_WHEN_USE_TEMPLATE = YES_NO_MESSAGE + 510;

        /// <summary>
        /// You don't have right to edit this report
        /// </summary>
        public const int MESSAGE_CANNOT_EDIT_GROUP = YES_NO_MESSAGE + 180;

        /// <summary>
        /// You don't have right to edit this report
        /// </summary>
        public const int MESSAGE_CANNOT_EDIT_REPORT = YES_NO_MESSAGE + 179;

        /// <summary>
        /// Cannot input negative number
        /// </summary>
        public const int MESSAGE_CANNOT_INPUT_NEGATIVE_NUMBER = YES_NO_MESSAGE + 174;

        /// <summary>
        /// You have select the last record. Cannot move down
        /// </summary>
        public const int MESSAGE_CANNOT_MOVEDOWN = YES_NO_MESSAGE + 10;
                         // You have select the last record. Cannot move down

        /// <summary>
        /// You have select the top record. Cannot move up
        /// </summary>
        public const int MESSAGE_CANNOT_MOVEUP = YES_NO_MESSAGE + 9; // You have select the top record. Cannot move up

        /// <summary>
        /// Roll Up Actual Cost process is terminated by user and not finished successfully
        /// </summary>
        public const int MESSAGE_CANNOT_ROLL_UP = YES_NO_MESSAGE + 505;

        /// <summary>
        /// Couldn't save data. Please check your input information
        /// </summary>
        public const int MESSAGE_CANNOT_SAVE_TO_DB = YES_NO_MESSAGE + 99;
                         // Couldn't save data. Please check your input information

        /// <summary>
        /// Can not select Lot for this Item
        /// </summary>
        public const int MESSAGE_CANNOT_SELECT_LOT = YES_NO_MESSAGE + 398; // Cannot select Lot for this Item

        /// <summary>
        /// Cannot unrelease these work orders cause it already in used
        /// </summary>
        public const int MESSAGE_CANNOT_UNRELEASE_WO = YES_NO_MESSAGE + 559;

        /// <summary>
        /// You don't have right to view this report
        /// </summary>
        public const int MESSAGE_CANNOT_VIEW_REPORT = YES_NO_MESSAGE + 178;

        /// <summary>
        /// Can not create report history 
        /// </summary>
        public const int MESSAGE_CANT_CREATE_REPORT_HISTORY = YES_NO_MESSAGE + 426; //Can't create report history

        /// <summary>
        /// Can not reload report from history
        /// </summary>
        public const int MESSAGE_CANT_RELOAD_REPORT_FROM_HISTORY = YES_NO_MESSAGE + 429;
                         // Can't reload report from history, maybe report history table is missing or was not created

        /// <summary>
        /// The category is using in products, don’t add new child
        /// </summary>
        public const int MESSAGE_CATEGORY_NOADDCHILD = YES_NO_MESSAGE + 45;
                         // The category is using in products, don’t add new child

        /// <summary>
        /// The detail grid must has at least two Part Number 
        /// </summary>
        public const int MESSAGE_CHANGE_CATEGORY_AT_LEAST_TWOROW = YES_NO_MESSAGE + 417;
                         //The detail grid must have at least two part number

        /// <summary>
        /// The Change Category Time must be a positive integer
        /// </summary>
        public const int MESSAGE_CHANGECATEGORY_CHANGETIME = YES_NO_MESSAGE + 408; //The change time must be an interger

        /// <summary>
        /// Charge quantity must less than order quantity
        /// </summary>
        public const int MESSAGE_CHARGEQTY_MUST_LESS_THAN_ORDERQTY = YES_NO_MESSAGE + 175;

        /// <summary>
        /// The Date you entered is not correct
        /// </summary>
        public const int MESSAGE_CHECK_DATE = YES_NO_MESSAGE + 79; //The Date you entered is not correct

        /// <summary>
        /// Product information is invalid
        /// </summary>
        public const int MESSAGE_CHECK_ITEM_SETUP = YES_NO_MESSAGE + 558;

        /// <summary>
        /// The date must be higher than or equal Order Date
        /// </summary>
        public const int MESSAGE_CHECK_ORDERDATE = YES_NO_MESSAGE + 78;
                         // The date must be higher than or equal Order Date

        /// <summary>
        /// Total Debit must equals to Total Credit or Transacion must be a single transaction
        /// </summary>
        public const int MESSAGE_CHECK_TOTAL_SINGLE = YES_NO_MESSAGE + 544;

        /// <summary>
        /// Delay Time must be greater than 0
        /// </summary>
        public const int MESSAGE_CHECKPOINT_DELAY_TIME = YES_NO_MESSAGE + 390; //The delay time must be grater than zero

        /// <summary>
        /// Sample Rate must be greater than 0
        /// </summary>
        public const int MESSAGE_CHECKPOINT_SAMPLE_RATE = YES_NO_MESSAGE + 389;
                         //The sample rate must be grater than zero 

        /// <summary>
        /// Please enter Part Number before enter Operation
        /// </summary>
        public const int MESSAGE_CHECKPOINT_SLITEM_BEFORE = YES_NO_MESSAGE + 388;
                         //Pls input part number before input operation 

        /// <summary>
        /// Do you want to @ these Purchase Order Lines
        /// Exam: Do you want to open these Purchase Order Lines
        /// </summary>
        public const int MESSAGE_CLOSE_OR_OPEN_POLINE = YES_NO_MESSAGE + 469;

        /// <summary>
        /// Code or Name already existed in system. Please select another one
        /// </summary>
        public const int MESSAGE_CODE_NAME_UNIQUE = YES_NO_MESSAGE + 493;

        /// <summary>
        /// There is no MTS object context. You are not joined in the MTS transaction context
        /// </summary>
        public const int MESSAGE_COM_TRANSACTION = YES_NO_MESSAGE + 13;
                         // There is no MTS object context. You are not joined in the MTS transaction context

        /// <summary>
        /// Commit quantity greater than available quantity
        /// </summary>
        public const int MESSAGE_COMMITQUANTITY_GREATER_THAN_AVAILABLEQUANTITY = YES_NO_MESSAGE + 171;

        /// <summary>
        /// This Commit Quantity can not be higher than the Remaining Quantity of this Sale Order
        /// </summary>
        public const int MESSAGE_COMMITQUANTITY_GREATER_THAN_REMAINORDERQUANTITY = YES_NO_MESSAGE + 298;
                         //This commit quantity cannot be higher than the remaining quantity for this sale order

        /// <summary>
        /// Commit quantity must greater than zero
        /// </summary>
        public const int MESSAGE_COMMITQUANTITY_MUST_GREATER_THAN_ZERO = YES_NO_MESSAGE + 172;

        /// <summary>
        /// Total completion quantity cannot exceed Order Quantity
        /// </summary>
        public const int MESSAGE_COMPLETION_OVER_ORDER = YES_NO_MESSAGE + 561;

        /// <summary>
        /// Line can not be blank
        /// </summary>
        public const int MESSAGE_COMPONENT_SCRAP_ORDER_CANNOT_BE_NULL = YES_NO_MESSAGE + 281;
                         // Line column can not be null 

        /// <summary>
        /// Please enter Operation
        /// </summary>
        public const int MESSAGE_COMPONENT_SCRAP_PLS_INPUT_OPERATION = YES_NO_MESSAGE + 282; // Please input Operation

        /// <summary>
        /// Please enter Part Name
        /// </summary>
        public const int MESSAGE_COMPONENT_SCRAP_PLS_INPUT_PARTNAME = YES_NO_MESSAGE + 283; // Please input Part Name

        /// <summary>
        /// Please enter Scrap Quantity
        /// </summary>
        public const int MESSAGE_COMPONENT_SCRAP_PLS_INPUT_SCRAPQUANTITY = YES_NO_MESSAGE + 284;
                         // Please input Scrap Quantity

        /// <summary>
        /// Scrap Quantity must be smaller than Available Quantity
        /// </summary>
        public const int MESSAGE_COMPONENT_SCRAP_SCRAPQUANTITY_MUST_BE_SMALLER_AVAILABLEQTY = YES_NO_MESSAGE + 285;
                         // Scrap Quantity must be smaller than Available Quantity

        /// <summary>
        /// Please config Begin Period
        /// </summary>
        public const int MESSAGE_CONFIG_BEGINPERIOD = YES_NO_MESSAGE + 547;

        /// <summary>
        /// Please config Exchange Rate
        /// </summary>
        public const int MESSAGE_CONFIG_EXCHANGERATE = YES_NO_MESSAGE + 549;

        /// <summary>
        /// Please config Rate Type in Finance Parameter
        /// </summary>
        public const int MESSAGE_CONFIG_RATETYPE = YES_NO_MESSAGE + 548;

        /// <summary>
        /// The table [@] has not been not configured, please contact your administrator
        /// </summary>
        public const int MESSAGE_CONFIGURED_TABLE = YES_NO_MESSAGE + 4;
                         // The table [@] has not been not configured, please contact your administrator

        /// <summary>	
        /// Are you sure you want to save this transaction
        /// </summary>
        public const int MESSAGE_CONFIRM_BEFORE_SAVE_DATA = YES_NO_MESSAGE + 517;

        /// <summary>
        /// The Post Date is @. Are you sure want you want to save transaction
        /// </summary>
        public const int MESSAGE_CONFIRM_BEFORE_SAVE_WOCOMPLETION = YES_NO_MESSAGE + 531;

        /// <summary>
        /// Do you really want to change type of Receiving
        /// </summary>
        public const int MESSAGE_CONFIRM_CHANGE_TYPE_OF_RECEIVING = YES_NO_MESSAGE + 504;

        /// <summary>
        /// Set margin and font to default
        /// </summary>
        public const int MESSAGE_CONFIRM_SET_DEFAULT = YES_NO_MESSAGE + 16; // Set margin and font to default

        /// <summary>
        /// Can't ship some of saleorders of the pack list
        /// </summary>
        public const int MESSAGE_CONFIRMSHIP_CHECKINGERROR = YES_NO_MESSAGE + 96;
                         // Can't ship some of saleorders of the pack list

        /// <summary>
        /// Effective Beg. Date is bigger than Previous Effective End.Date
        /// </summary>
        public const int MESSAGE_COSTCENTERRATE_CHECKDATEEVERYROW = YES_NO_MESSAGE + 144;
                         // Effective Beg. Date is bigger than Previous Effective End.Date

        /// <summary>
        /// Effective Beg.Date is smaller than Effective End.Date
        /// </summary>
        public const int MESSAGE_COSTCENTERRATE_CHECKDATEONEROW = YES_NO_MESSAGE + 143;
                         //Effective Beg.Date is smaller than Effective End.Date

        /// <summary>
        /// The cost center is not exist
        /// </summary>
        public const int MESSAGE_COSTCENTERRATE_COSTRATENOTEXIST = YES_NO_MESSAGE + 145; // The cost center is not exist

        /// <summary>
        /// Effective Beg. Date is not null
        /// </summary>
        public const int MESSAGE_COSTCENTERRATE_EFFBEGINNOTNULL = YES_NO_MESSAGE + 146;
                         //Effective Beg. Date is not null

        /// <summary>
        /// Effective End. Date is not null
        /// </summary>
        public const int MESSAGE_COSTCENTERRATE_EFFENDNOTNULL = YES_NO_MESSAGE + 147; //Effective End. Date is not null

        /// <summary>
        /// Can not delete this menu because it was already used by another transaction
        /// Exam: Can not delete this menu because it was already used by another transaction
        /// </summary>
        public const int MESSAGE_COULD_NOT_DELETE_MENU = YES_NO_MESSAGE + 471;

        /// <summary>
        /// Could't charge by price
        /// </summary>
        public const int MESSAGE_COULDNOT_CHARGE_BY_PRICE = YES_NO_MESSAGE + 185;

        /// <summary>
        /// Could't charge by quantity
        /// </summary>
        public const int MESSAGE_COULDNOT_CHARGE_BY_QUANTITY = YES_NO_MESSAGE + 186; //Could't charge by quantity

        /// <summary>
        /// You can not convert this item, because it has not been saved in to database. 
        /// </summary>
        public const int MESSAGE_CPO_CAN_NOT_CONVERT = YES_NO_MESSAGE + 524;

        /// <summary>
        /// Purchasing Price, Currency, Primary Vendor, Vendor Location may not be configured
        /// Exam: Purchasing Price, Currency, Primary Vendor, Vendor Location may not be configured
        /// </summary>
        public const int MESSAGE_CPO_CAN_NOT_CONVERT_TO_PO = YES_NO_MESSAGE + 481;

        /// <summary>
        /// Production Line was not been configured correctly
        /// Exam: Production Line was not been configured correctly
        /// </summary>
        public const int MESSAGE_CPO_CAN_NOT_CONVERT_TO_WO = YES_NO_MESSAGE + 482;

        /// <summary>
        /// This CPO or Work Order Line is invalid. It has more than 1 main Work Center
        /// Exam: This CPO or Work Order Line is invalid. It has more than 1 main Work Center
        /// </summary>
        public const int MESSAGE_CPO_HAS_MULTI_MAIN_WC = YES_NO_MESSAGE + 476;

        /// <summary>
        /// Please select a CCN before continuing
        /// </summary>
        public const int MESSAGE_CPODATAVIEWER_PLEASE_SELECT_CCN = YES_NO_MESSAGE + 368;
                         //You dont select a CNN, please select a CCN in the list!

        /// <summary>
        /// Please select a Cycle for searching
        /// </summary>
        public const int MESSAGE_CPODATAVIEWER_PLEASE_SELECT_CYCLE = YES_NO_MESSAGE + 370;
                         //You dont select a Cycle, please select a Cycle for searching!

        /// <summary>
        /// Please select a Master Location
        /// </summary>
        public const int MESSAGE_CPODATAVIEWER_PLEASE_SELECT_MASLOC = YES_NO_MESSAGE + 371;
                         //You dont select a Master Location, please select for searching!

        /// <summary>
        /// Please enter Plan Type
        /// </summary>
        public const int MESSAGE_CPODATAVIEWER_PLEASE_SELECT_PLAN_TYPE = YES_NO_MESSAGE + 369;
                         //You dont select a Plan Type, please select for searching!

        /// <summary>
        /// This customer does not exist in database
        /// </summary>
        public const int MESSAGE_CUSTOMER_DOES_NOT_EXIST = YES_NO_MESSAGE + 83;
                         // This customer does not exist in database

        /// <summary>
        /// This (MPS) Cycle Option was used by (DCP). Please delete related (DCP) data before delete this Cycle Option
        /// </summary>
        public const int MESSAGE_CYCLE_OPTION_ALREADY_USED = YES_NO_MESSAGE + 445;

        /// <summary>
        /// Data casting is invalid
        /// </summary>
        public const int MESSAGE_DATA_CAST = YES_NO_MESSAGE + 14; // Data casting is invalid

        /// <summary>
        /// Date period can not be overlapped
        /// </summary>
        public const int MESSAGE_DATE_IS_OVERLAP = YES_NO_MESSAGE + 395; // The date is overlap, please enter new value!

        /// <summary>
        /// The Version already existed in this Planning Period, please enter another one
        /// </summary>
        public const int MESSAGE_DCOPTION_VERSION_HAS_EXIST = YES_NO_MESSAGE + 511;

        /// <summary>
        /// As Of Date must be latter than current date 
        /// </summary>
        public const int MESSAGE_DCP_ASOFDATE_MUST_IN_FUTURE = YES_NO_MESSAGE + 416;
                         //As of date must be latter than current date (must be in the future).

        /// <summary>
        /// Please configure configure Working Calendar from year @ to @ before running
        /// </summary>
        public const int MESSAGE_DCP_CONFIG_CALENDAR_FROM_X_TO_Y = YES_NO_MESSAGE + 412;
                         //You have to config Working Calendar from year @ to @	

        /// <summary>
        /// Please configure for Work Center '@' and Work Center Capacity before running
        /// </summary>
        public const int MESSAGE_DCP_CONFIG_WORKCENTER = YES_NO_MESSAGE + 413;
                         //Configuration for Work center '@' and Work center capacity, please

        /// <summary>
        /// Please select Detail Capacity Planning Cycle before continuing 
        /// </summary>
        public const int MESSAGE_DCP_SELECT_CYCLE = YES_NO_MESSAGE + 409; //Select a cycle before processing

        /// <summary>
        /// Please configure Working Calendar for the year @ before running
        /// </summary>
        public const int MESSAGE_DCP_SETTING_WORKING_CALENDAR = YES_NO_MESSAGE + 414;
                         //Setting working calendar for the year @, please

        /// <summary>
        /// Extra Stop From time must be smaller than Extra Stop To time
        /// </summary>
        public const int MESSAGE_DCP_SHIFTPATTERN_EXTRASTOP_FROM_MUSTBE_SMALLER_THAN_EXTRASTOP_TO_DATE =
            YES_NO_MESSAGE + 381; //The Extra Stop From Time must be smaller than Extra Stop To Time.

        /// <summary>
        /// Extra Stop From time must be in Work Time period
        /// </summary>
        public const int MESSAGE_DCP_SHIFTPATTERN_EXTRASTOP_TIME_MUSTBE_IN_WORKTIME = YES_NO_MESSAGE + 382;
                         //The Extra Stop From Time must be in the Work Time.

        /// <summary>
        /// The From Work Date must be smaller than To Work Date
        /// </summary>
        public const int MESSAGE_DCP_SHIFTPATTERN_FROM_WORKDATE_MUST_BE_SMALLER_THAN_TO_WORKDATE = YES_NO_MESSAGE + 384;
                         //The From Work Date must be smaller than To Work Date

        /// <summary>
        /// From Effective Date must be smaller than To Effective Date
        /// </summary>
        public const int MESSAGE_DCP_SHIFTPATTERN_FROMDATE_MUST_BE_SMALLER_THAN_TODATE = YES_NO_MESSAGE + 374;
                         //The From Effective Date must be smaller than To Effective Date

        /// <summary>
        /// Please input the other control
        /// </summary>
        public const int MESSAGE_DCP_SHIFTPATTERN_PLS_INPUT_REMAIN_CONTROLS = YES_NO_MESSAGE + 385;
                         //Please input the other control!

        /// <summary>
        /// The Refreshing From Time must be smaller than Refreshing To Time
        /// </summary>
        public const int MESSAGE_DCP_SHIFTPATTERN_REFRESSHING_FROM_MUSTBE_SMALLER_THAN_REFRESSHING_TO_DATE =
            YES_NO_MESSAGE + 379; //The Refresshing From must be smaller than Refresshing To Time.

        /// <summary>
        /// Refreshing Time must be in Work Time period
        /// </summary>
        public const int MESSAGE_DCP_SHIFTPATTERN_REFRESSHING_TIME_MUSTBE_IN_WORKTIME = YES_NO_MESSAGE + 380;
                         //The Refresshing Time must be in the Work Time.

        /// <summary>
        /// Regular Stop must be in Work Time period
        /// </summary>
        public const int MESSAGE_DCP_SHIFTPATTERN_REGULAR_TIME_MUSTBE_IN_WORKTIME = YES_NO_MESSAGE + 378;
                         //The Regular Stop must be in the Work Time.

        /// <summary>
        /// The Regular Stop From must be smaller than the Regular Stop To
        /// </summary>
        public const int MESSAGE_DCP_SHIFTPATTERN_REGULARFROM_MUSTBE_SMALLER_THAN_REGULARTO_DATE = YES_NO_MESSAGE + 386;
                         //The Regular Stop From must be smaller than the Regular Stop To.

        /// <summary>
        /// Work Time must be less than 24 hours
        /// </summary>
        public const int MESSAGE_DCP_SHIFTPATTERN_WORKTIME_MUSTBE_SMALLER_24 = YES_NO_MESSAGE + 383;
                         //The Work Time must be smaller than 24 hours

        /// <summary>
        /// Can not process because some shift periods are overlaped 
        /// </summary>
        public const int MESSAGE_DCP_SHIFTS_OVERLAPED = YES_NO_MESSAGE + 420; // Some shifts were overlaped time

        /// <summary>
        /// Cannot insert record Credit and Debit equal to Zero
        /// </summary>
        public const int MESSAGE_DEBIT_CREDIT_ZERO = YES_NO_MESSAGE + 546;

        /// <summary>
        /// Do you want to delete this group?
        /// </summary>
        public const int MESSAGE_DELETE_GROUP = YES_NO_MESSAGE + 2; // Do you want to delete this group?

        /// <summary>
        /// Do you really want to delete
        /// </summary>
        public const int MESSAGE_DELETE_RECORD = YES_NO_MESSAGE + 1; // Do you really want to delete?

        /// <summary>
        /// Do you want to delete this table?
        /// </summary>
        public const int MESSAGE_DELETE_TABLE = YES_NO_MESSAGE + 3; // Do you want to delete this table?

        /// <summary>
        /// Setup Pair must be different zero
        /// </summary>
        public const int MESSAGE_DIFFERENT_ZERO = YES_NO_MESSAGE + 533;

        /// <summary>
        /// Please insert commit detail to grid
        /// </summary>
        public const int MESSAGE_DIRECT_COMMIT_INPUT_DETAIL = YES_NO_MESSAGE + 391;
                         // Please add detail data to commit detail

        /// <summary>
        /// The Start Date or Due Date must be between Start Date and Due Date of Work Order Line
        /// </summary>
        public const int MESSAGE_DISPATCH_STARTDATE_DUEDATE = YES_NO_MESSAGE + 415;
                         //The start date or due date is not in the start Date, due Date of Work order line

        /// <summary>
        /// Distribution amount must greater than sum of amount
        /// </summary>
        public const int MESSAGE_DISTRIBUTE_AMOUNT_MUST_GREATER_THAN_SUM_AMOUNT = YES_NO_MESSAGE + 187;

        /// <summary>
        /// Distribute amount must greater than zero
        /// </summary>
        public const int MESSAGE_DISTRIBUTE_AMOUNT_MUST_GREATER_THAN_ZERO = YES_NO_MESSAGE + 173;

        /// <summary>
        /// The Report DDL version is not compatible with PCS version
        /// Exam: The Report DDL version is not compatible with PCS version
        /// </summary>
        public const int MESSAGE_DLL_VERSION_NOT_MATCH = YES_NO_MESSAGE + 468;

        /// <summary>
        /// Do you really want to exit
        /// Exam: Do you really want to exit
        /// </summary>
        public const int MESSAGE_DO_YOU_REALLY_WANT_TO_EXIT = YES_NO_MESSAGE + 462;

        /// <summary>
        /// You must correct all data before escape
        /// </summary>
        public const int MESSAGE_DOCK_TO_STOCK_YOU_MUST_CORRECT_DATA_BEFORE_ESCAPE = YES_NO_MESSAGE + 309;
                         //You must correct all data before you escape.

        /// <summary>
        /// You must select a Location before continuing
        /// </summary>
        public const int MESSAGE_DOCK_TO_STOCK_YOU_MUST_SELECT_ATLEAST_ONE_LOCATION = YES_NO_MESSAGE + 308;
                         //You must select at least one Location before continue.

        /// <summary>
        /// You must select a Packing Location before selecting Packing Bin
        /// </summary>
        public const int MESSAGE_DOCK_TO_STOCK_YOU_MUST_SELECT_PACKING_LOC_FIRST = YES_NO_MESSAGE + 307;
                         //You must select Packing Location before select Packing Bin.

        /// <summary>
        /// The pack list number is exist
        /// </summary>
        public const int MESSAGE_DUPLICATE_PACKLISTNO = YES_NO_MESSAGE + 71; //The pack list number is exist

        /// <summary>
        /// This @ is already selected, please select another one
        /// </summary>
        public const int MESSAGE_DUPLICATE_RECORD = YES_NO_MESSAGE + 541;

        /// <summary>
        /// Input end date greater than or equal begin date, please
        /// </summary>
        public const int MESSAGE_ENDDATE_GREATER_THAN_BEGINDATE = YES_NO_MESSAGE + 70;
                         // Input end date greater than or equal begin date, please

        /// <summary>
        /// The total delivery quantity is equal to the order quantity. You cannot add more
        /// </summary>
        public const int MESSAGE_ENOUGH_DELIVERYQTY = YES_NO_MESSAGE + 80;
                         // The total delivery quantity is equal to the order quantity. You cannot add more

        /// <summary>
        /// Error Work Center: @
        /// </summary>
        public const int MESSAGE_ERROR_WORKCENTER = YES_NO_MESSAGE + 538;

        /// <summary>
        /// Report was executed successfully. Click on tab "@" to view result
        /// </summary>
        public const int MESSAGE_EXECUTE_REPORT_SUCCEED = YES_NO_MESSAGE + 40;
                         // Report was executed successfully. Click on tab "Report Data" to view result

        /// <summary>
        /// Export report to Excel successful.
        /// </summary>
        public const int MESSAGE_EXPORT_EXCEL_SUCCEED = YES_NO_MESSAGE + 44; // Export report to Excel successful.

        /// <summary>
        /// Do you really want to change the Receipt No
        /// </summary>
        public const int MESSAGE_FREIGHT_CHANGE_RECEIVE_NO = YES_NO_MESSAGE + 502;

        /// <summary>
        /// Sum of Amount columns value must be equal Net Amount
        /// </summary>
        public const int MESSAGE_FREIGHT_SUMOFAMOUNT_MUST_BE_EQUAL_NETAMOUNT = YES_NO_MESSAGE + 500;

        /// <summary>
        /// The function is not exist
        /// </summary>
        public const int MESSAGE_FUNCTIONVENDOR_NOFUNCTION = YES_NO_MESSAGE + 183;

        /// <summary>
        /// The function vendor is not exist
        /// </summary>
        public const int MESSAGE_FUNCTIONVENDOR_NOFUNCTIONVENDOR = YES_NO_MESSAGE + 184;

        /// <summary>
        /// The vendor is not exist
        /// </summary>
        public const int MESSAGE_FUNCTIONVENDOR_NOVENDOR = YES_NO_MESSAGE + 182;

        /// <summary>
        /// @ generation has completed successfully
        /// </summary>
        public const int MESSAGE_GENERATED_SUCCESSFULLY = YES_NO_MESSAGE + 410; //Run DCP process successfully

        /// <summary>
        /// @ must be greater than @
        /// Exam: From Month must be greater than To Month
        /// </summary>
        public const int MESSAGE_GREATER_THAN = YES_NO_MESSAGE + 467;

        /// <summary>
        /// Format of Excel file is not correct
        /// </summary>
        public const int MESSAGE_IMPORT_ERROR_FILE_FORMAT = YES_NO_MESSAGE + 450;

        /// <summary>
        /// At least one Item Code in Excel file is not mapped to system Item Code
        /// </summary>
        public const int MESSAGE_IMPORT_ERROR_MAP_ITEM = YES_NO_MESSAGE + 451;

        /// <summary>
        /// Cannot read data from excel file
        /// </summary>
        public const int MESSAGE_IMPORT_ERROR_READ_FILE = YES_NO_MESSAGE + 449;

        /// <summary>
        /// Can not import Data because new Quatity is not equal to the Commited Quantity
        /// </summary>
        public const int MESSAGE_IMPORT_ERROR_UPDATE_COMMITTED = YES_NO_MESSAGE + 452;

        /// <summary>
        /// Incorrect Order No, input YYYYMMDD9999 correct please
        /// </summary>
        public const int MESSAGE_INCORRECT_ORDER_NO = YES_NO_MESSAGE + 82;
                         // Incorrect Order No, input YYYYMMDD9999 correct please

        /// <summary>
        /// Incorrect Post slip setup information
        /// </summary>
        public const int MESSAGE_INCORRECT_POSTSLIP = YES_NO_MESSAGE + 553;

        /// <summary>
        /// You have to input at least a record in grid detail
        /// </summary>
        public const int MESSAGE_INPUT_AT_LEAST_RECORD_IN_GRID = YES_NO_MESSAGE + 84;
                         // You have to input at least a record in grid detail

        /// <summary>
        /// Please enter data for mandatory columns 
        /// </summary>
        public const int MESSAGE_INPUT_FULL_ROW = YES_NO_MESSAGE + 418; //Please input complete information to this row.

        /// <summary>
        /// Please input Item field for each records.
        /// </summary>
        public const int MESSAGE_INPUT_ITEM_FIELD = YES_NO_MESSAGE + 90; // Please input Item field for each records. 

        /// <summary>
        /// Please input order quantity < on hand quantity at each records.
        /// </summary>
        public const int MESSAGE_INPUT_ON_HAND_QUANTITY_FIELD = YES_NO_MESSAGE + 94;
                         // Please input order quantity < on hand quantity at each records.

        /// <summary>
        /// Please input Order quantity field for each records.
        /// </summary>
        public const int MESSAGE_INPUT_ORDER_QUANTITY_FIELD = YES_NO_MESSAGE + 91;
                         // Please input Order quantity field for each records.

        /// <summary>
        /// Input Transaction date before execute this function
        /// </summary>
        public const int MESSAGE_INPUT_TRANSACTION_BEFORE = YES_NO_MESSAGE + 88;
                         // Input Transaction date before execute this function

        /// <summary>
        /// Please input Unit of measure field for each records.
        /// </summary>
        public const int MESSAGE_INPUT_UNIT_OF_MEASURE_FIELD = YES_NO_MESSAGE + 92;
                         // Please input Unit of measure field for each records.

        /// <summary>
        /// Please input Unit price field for each records.
        /// </summary>
        public const int MESSAGE_INPUT_UNIT_PRICE_FIELD = YES_NO_MESSAGE + 93;
                         // Please input Unit price field for each records.

        /// <summary>
        /// The sum of Accepted Quantity and Rejected Quantity must be equal to Sample Quantity
        /// </summary>
        public const int MESSAGE_INS_RESULT_ACCEPTED_REJECTED_SAMPLE = YES_NO_MESSAGE + 354;
                         //The Accepted Qty plus Rejected Qty must be equal Sample quantity

        /// <summary>
        /// Accepted Quantity must be equal or smaller than Sample Quantity
        /// </summary>
        public const int MESSAGE_INS_RESULT_ACCEPTED_SAMPLE = YES_NO_MESSAGE + 352;
                         //The Accepted quantity must be smaller or equal than Sample quantity

        /// <summary>
        /// Quantity must be greater than 0
        /// </summary>
        public const int MESSAGE_INS_RESULT_AVAILABLE = YES_NO_MESSAGE + 350; //The quantity must be greater than zere

        /// <summary>
        /// Available Quantity must be greater than 0
        /// </summary>
        public const int MESSAGE_INS_RESULT_AVAILABLE_QUANTITY = YES_NO_MESSAGE + 349;
                         //The Available Quantity must be greater than zere

        /// <summary>
        /// Sample Quantity must be equal or smaller than Available Quantity
        /// </summary>
        public const int MESSAGE_INS_RESULT_AVAILABLE_SAMPLE = YES_NO_MESSAGE + 351;
                         //The Sample quantity must be smaller or equal than Available quantity

        /// <summary>
        /// Rejected Quantity must be equal or smaller than Sample Quantity
        /// </summary>
        public const int MESSAGE_INS_RESULT_REJECTED_SAMPLE = YES_NO_MESSAGE + 353;
                         //The Rejected quantity must be smaller or equal than Sample quantity

        /// <summary>
        /// Insert MST_Currency table before execute this function
        /// </summary>
        public const int MESSAGE_INSERT_CURRENCY_BEFORE = YES_NO_MESSAGE + 87;
                         // Insert MST_Currency table before execute this function

        /// <summary>
        /// @ and @ can not be overlapped
        /// Exam: Regular Stop) and (Refreshing) can not be overlapped
        /// </summary>
        public const int MESSAGE_INTERSECT_NOT_ALLOWED = YES_NO_MESSAGE + 448;

        /// <summary>
        ///  The transaction date must be equal or smaller than current date
        /// </summary>
        public const int MESSAGE_INV_TRANSACTION_CANNOT_IN_FUTURE = YES_NO_MESSAGE + 488;

        /// <summary>
        /// Data is not a boolean
        /// </summary>
        public const int MESSAGE_INVALID_BOOLEAN = YES_NO_MESSAGE + 20; // Data is not a boolean

        /// <summary>
        /// Conversion Tolerence Percent must be less than 100
        /// </summary>        						
        public const int MESSAGE_INVALID_CTOLERENCEPERCENT = YES_NO_MESSAGE + 62;
                         // 	Conversion Tolerence Percent must be less than 100	        						

        /// <summary>
        /// Data is not a datetime
        /// </summary>
        public const int MESSAGE_INVALID_DATETIME = YES_NO_MESSAGE + 21; // Data is not a datetime

        /// <summary>
        /// Invalid e-mail address
        /// </summary>
        public const int MESSAGE_INVALID_EMAIL = YES_NO_MESSAGE + 102; // Invalid e-mail address

        /// <summary>
        /// Invalid fax number
        /// </summary>
        public const int MESSAGE_INVALID_FAX_NUMBER = YES_NO_MESSAGE + 101; //Invalid fax number

        /// <summary>
        /// @ format is invalid. The right format is @
        /// Exam: Slip Format format is invalid. The right format is PO-YYMMDD
        /// </summary>
        public const int MESSAGE_INVALID_FORMAT = YES_NO_MESSAGE + 455;

        /// <summary>
        /// End Date must be greater than Start Date
        /// </summary>
        public const int MESSAGE_INVALID_FROM_DATE_TODATE = YES_NO_MESSAGE + 396;
                         // From date must be earlier than To date, please enter again!

        /// <summary>
        /// Inventor is not valid, please select another one
        /// </summary>
        public const int MESSAGE_INVALID_INVENTOR = YES_NO_MESSAGE + 437; //Invalid inventor, please input again 

        /// <summary>
        /// Minimum stock must be less than maximum stock
        /// </summary>
        public const int MESSAGE_INVALID_MINIMUMSTOCK = YES_NO_MESSAGE + 66;
                         // Minimum stock must be less than maximum stock

        /// <summary>
        /// This value must be a number
        /// </summary>
        public const int MESSAGE_INVALID_NUMERIC = YES_NO_MESSAGE + 19; // This value must be a number

        /// <summary>
        /// The PrimaryVendor Code you selected does not exist
        /// </summary>
        public const int MESSAGE_INVALID_PARTYCODE = YES_NO_MESSAGE + 56;
                         // The PrimaryVendor Code you selected does not exist

        /// <summary>
        /// Please select the location for the PrimaryVendor
        /// </summary>
        public const int MESSAGE_INVALID_PARTYLOCATION = YES_NO_MESSAGE + 63;
                         // Please select the location for the PrimaryVendor

        /// <summary>
        /// Percentage must be between 0 and 100
        /// </summary>
        public const int MESSAGE_INVALID_PERCENT = YES_NO_MESSAGE + 392; // Percent must between 0 and 100

        /// <summary>
        /// Invalid phone number
        /// </summary>
        public const int MESSAGE_INVALID_PHONE_NUMBER = YES_NO_MESSAGE + 100; // Invalid phone number

        /// <summary>
        /// Invalid Purchase order number
        /// </summary>
        public const int MESSAGE_INVALID_PO_NUMBER = YES_NO_MESSAGE + 168; //Invalid PO number

        /// <summary>
        /// Invalid PO for receive @. Valid PO must have type is @
        /// Exam: Invalid PO for receive By Slip. Valid PO must have type is Domestic
        /// </summary>
        public const int MESSAGE_INVALID_POTYPE = YES_NO_MESSAGE + 539;

        /// <summary>
        /// Scrap percent must be less than 100
        /// </summary>
        public const int MESSAGE_INVALID_SCRAPPERCENT = YES_NO_MESSAGE + 60; // Scrap percent must be less than 100

        /// <summary>
        /// Stock Unit and Buying Unit do not have any scale
        /// </summary>
        public const int MESSAGE_INVALID_STOCKUNIT_AND_BUYINGUNIT = YES_NO_MESSAGE + 58;
                         // Stock Unit and Buying Unit do not have any scale

        /// <summary>
        /// Stock Unit and Selling Unit do not have scale
        /// </summary>
        public const int MESSAGE_INVALID_STOCKUNIT_AND_SELLINGUNIT = YES_NO_MESSAGE + 57;
                         // Stock Unit and Selling Unit do not have scale

        /// <summary>
        /// Voucher Tolerence Percent must be less than 100
        /// </summary>
        public const int MESSAGE_INVALID_VTOLERENCEPERCENT = YES_NO_MESSAGE + 61;
                         //Voucher Tolerence Percent must be less than 100

        /// <summary>
        /// You have to issue material to outside supplier before receiving finished goods
        /// </summary>
        public const int MESSAGE_ISSUE_MATERIAL_TO_OUTSIDE = YES_NO_MESSAGE + 503;

        /// <summary>
        /// One Item can belong to one Type. This Item did belong to another type already
        /// </summary>
        public const int MESSAGE_ITEM_BELONG_ANOTHER_TYPE = YES_NO_MESSAGE + 495;

        /// <summary>
        /// Please save data first before running this function
        /// </summary>
        public const int MESSAGE_ITEM_COST_SAVEDATAFIRST = YES_NO_MESSAGE + 157;
                         //Please save data first before running this function

        /// <summary>
        /// Cannot convert to work order from none make item
        /// </summary>
        public const int MESSAGE_ITEM_IS_NONE_MAKE = YES_NO_MESSAGE + 565;

        /// <summary>
        /// Adjustment Quantity must be smaller than Available Quantity
        /// </summary>
        public const int MESSAGE_IV_ADJUSTMENT_ADJUSTQTY_MUST_BE_SMALLER_THAN_AVAILABLEQTY = YES_NO_MESSAGE + 355;
                         //The Adjustment Quantity must be smaller than Available Quantity.

        /// <summary>
        /// Please enter Bin for this Location because it is controlled by Bin
        /// </summary>
        public const int MESSAGE_IV_ADJUSTMENT_PLS_INPUT_BIN = YES_NO_MESSAGE + 344;
                         //Please enter Bin because this Location is controlled by Bin.

        /// <summary>
        /// Please enter Lot for this Item because it is controlled by Lot
        /// </summary>
        public const int MESSAGE_IV_ADJUSTMENT_PLS_INPUT_LOT = YES_NO_MESSAGE + 343;
                         //Please enter Lot because this item is controlled by Lot.

        /// <summary>
        /// Please enter Part Number before continuing
        /// </summary>
        public const int MESSAGE_IV_ADJUSTMENT_PLS_INPUT_PATHNUMBER = YES_NO_MESSAGE + 363;
                         //You must input part number before continue.

        /// <summary>
        /// Missing Information when Generate Account. Please review the Post Slip Setup and its information
        /// </summary>
        public const int MESSAGE_LACK_INFO_GEN_ACCOUNT = YES_NO_MESSAGE + 589;

        /// <summary>
        /// Receive Location must belongs to the selected Master Location
        /// </summary>
        public const int MESSAGE_LOCATION_NOT_MATCH_WITH_MASLOC = YES_NO_MESSAGE + 394;
                         // Selected Location not in selected Master Location

        /// <summary>
        /// The Source Bin and Destination Bin must be diffirent
        /// </summary>
        public const int MESSAGE_LOCTOLOC_DIFFERENCE_BIN = YES_NO_MESSAGE + 328;
                         //The Source bin and Dest bin is not the same 

        /// <summary>
        /// The Source Location and Destination Location must be diffirent
        /// </summary>
        public const int MESSAGE_LOCTOLOC_DIFFERENCE_LOC = YES_NO_MESSAGE + 329;
                         //The Source location and Dest location is not the same 

        /// <summary>
        /// Can not dupplicate part number in the transaction
        /// </summary>
        public const int MESSAGE_LOCTOLOC_DUPLICATE_ITEM = YES_NO_MESSAGE + 334;
                         //The transaction must has at least the item

        /// <summary>
        /// Transaction contains at least a Part Number
        /// </summary>
        public const int MESSAGE_LOCTOLOC_HAS_ATLEAST_ONELINE = YES_NO_MESSAGE + 333;
                         //The transaction must has at least the item 

        /// <summary>
        /// There is not enough Item's Quantity for transferring
        /// </summary>
        public const int MESSAGE_LOCTOLOC_NOT_ENOUGH_QUANTITY_TO_TRANSFER = YES_NO_MESSAGE + 331;
                         //Not enough quantity of item to transfer 

        /// <summary>
        /// Please select the Destination bin because Location with Bin
        /// </summary>
        public const int MESSAGE_LOCTOLOC_SELECT_DEST_BIN = YES_NO_MESSAGE + 346;
                         //Please select the Destination bin because Location with Bin 

        /// <summary>
        /// Please select Part number before selecting Lot
        /// </summary>
        public const int MESSAGE_LOCTOLOC_SELECT_ITEM_BEFORE_SELECT_LOT = YES_NO_MESSAGE + 330;
                         //Please select the Location before select lot 

        /// <summary>
        /// Please select Location before selecting Part Number
        /// </summary>
        public const int MESSAGE_LOCTOLOC_SELECT_LOCATION = YES_NO_MESSAGE + 327;
                         //Please select the Location before select Part Number 

        /// <summary>
        /// Please select the Lot because the item control by lot
        /// </summary>
        public const int MESSAGE_LOCTOLOC_SELECT_LOT = YES_NO_MESSAGE + 348;
                         //Please select the Lot because the item control by lot

        /// <summary>
        /// Please select the Source bin because Location with Bin
        /// </summary>
        public const int MESSAGE_LOCTOLOC_SELECT_SOURCE_BIN = YES_NO_MESSAGE + 345;
                         //Please select the Source bin because Location with Bin 

        /// <summary>
        /// Transfer Quantity must be equal or smaller Available Quantity
        /// </summary>
        public const int MESSAGE_LOCTOLOC_TRANSFER_AVAILABLE = YES_NO_MESSAGE + 347;
                         //Please select the Destination bin because Location with Bin 

        /// <summary>
        /// Transfer Quantity must be greater than 0
        /// </summary>
        public const int MESSAGE_LOCTOLOC_TRANSFER_QUANTITY = YES_NO_MESSAGE + 332;
                         //The transfer quantity must greater than zero 

        /// <summary>
        /// Lot is fully received. Please select another Lot
        /// </summary>
        public const int MESSAGE_LOT_FULL = YES_NO_MESSAGE + 377; // Lot is fully receive. Please select another Lot

        /// <summary>
        /// There is no role to save, please add a new role
        /// </summary>
        public const int MESSAGE_MANAGEROLE_NOROW_TOSAVE = YES_NO_MESSAGE + 39;
                         // There is no role to save, please add a new role

        /// <summary>
        /// You have just added a new user, please use Save button before adding a new user
        /// </summary>
        public const int MESSAGE_MANAGEUSER_CLICKADD_AGAIN = YES_NO_MESSAGE + 38;
                         // You have just added a new user, please use Save button before adding a new user

        /// <summary>
        /// The Expired Date must be later than today
        /// </summary>
        public const int MESSAGE_MANAGEUSER_EXPIREDDATE = YES_NO_MESSAGE + 37;
                         // The Expired Date must be later than today

        /// <summary>
        /// Please re-type your password
        /// </summary>
        public const int MESSAGE_MANAGEUSER_PWD = YES_NO_MESSAGE + 15; // Please re-type your password

        /// <summary>
        /// Please select a user from grid
        /// </summary>
        public const int MESSAGE_MANAGEUSER_SELECT_USER = YES_NO_MESSAGE + 42; // Please select a user from grid

        /// <summary>
        /// Please enter data for @
        /// </summary>
        public const int MESSAGE_MANDATORY_FIELD_REQUIRED = YES_NO_MESSAGE + 440; //Please enter data for a field

        /// <summary>
        /// From Due Date must be smaller than To Due Date 
        /// </summary>
        public const int MESSAGE_MANUFACTURING_CLOSE_FROMDATE_SMALLER_TODATE = YES_NO_MESSAGE + 336;
                         //The from Date must be smaller than to Date.

        /// <summary>
        /// There is no Closed Work Order found
        /// </summary>
        public const int MESSAGE_MANUFACTURING_CLOSE_NOT_FOUND_ANY_WO = YES_NO_MESSAGE + 337;
                         //There is no Work Orders need to close.

        /// <summary>
        /// You must select or enter Lot because this Item is controlled by Lot 
        /// </summary>
        public const int MESSAGE_MATERIAL_RECEIPT_MUST_ENTER_LOT = YES_NO_MESSAGE + 406;
                         //You must select or enter Lot because this Item is controlled by Lot

        /// <summary>
        /// You must enter Serial because this Item is controlled by Serial
        /// </summary>
        public const int MESSAGE_MATERIAL_RECEIPT_MUST_ENTER_SERIAL = YES_NO_MESSAGE + 407;
                         //You must enter Serial because this Item is controlled by Serial

        /// <summary>
        /// This item is not available for issueing (Available qty = 0)! Please choose another!
        /// </summary>
        public const int MESSAGE_MATERIALISSUE_AVAILABLE_EQUAL_ZERO = YES_NO_MESSAGE + 365;
                         //This item is not available for issueing (Available qty = 0)! Please choose another!

        /// <summary>
        /// This Transaction Number already existed, please enter another value
        /// </summary>
        public const int MESSAGE_MATERIALISSUE_DUPLICATE_TRANSNO = YES_NO_MESSAGE + 364;
                         //This transaction no has already existed! Please choose another!

        /// <summary>
        /// Issue Quantity must be equal or smaller than Available Quantity
        /// </summary>
        public const int MESSAGE_MATERIALISSUE_INVALID_ISSUE_DATE = YES_NO_MESSAGE + 325;
                         //Issue date must be in current period

        /// <summary>
        /// This Quantity must be greater than 0 and greater than Available Quantity
        /// </summary>
        public const int MESSAGE_MATERIALISSUE_INVALID_ISSUE_QTY = YES_NO_MESSAGE + 326;

        /// <summary>
        /// This item is BIN controlled, please select it is BIN 
        /// </summary>
        public const int MESSAGE_MATERIALISSUE_SELECT_BIN = YES_NO_MESSAGE + 366;

        /// <summary>
        /// This Lot does not exist, do you want to create this lot
        /// </summary>
        public const int MESSAGE_MATERIALRECEIPT_ASK_CREATE_LOT = YES_NO_MESSAGE + 322;
                         //This lot does not exist, do you want to create this lot?

        /// <summary>
        /// Please enter Quantity
        /// </summary>
        public const int MESSAGE_MATERIALRECEIPT_INPUT_QUANTITY = YES_NO_MESSAGE + 316; //Please input the quantity

        /// <summary>
        /// In you select to receive product by serial, the quantity must be 1 
        /// </summary>
        public const int MESSAGE_MATERIALRECEIPT_QUANTITY_EQUAL_1 = YES_NO_MESSAGE + 318;
                         //In you select to receive product by serial, the quantity must be 1

        /// <summary>
        /// The Quantity must be greater than 0
        /// </summary>
        public const int MESSAGE_MATERIALRECEIPT_QUANTITY_HIGHER_0 = YES_NO_MESSAGE + 317;
                         //The quantity must be higher than zero

        /// <summary>
        /// Please select CCN before continuing
        /// </summary>
        public const int MESSAGE_MATERIALRECEIPT_SELECT_CNN = YES_NO_MESSAGE + 319; //Please select CCN

        /// <summary>
        /// Please select a Location
        /// </summary>
        public const int MESSAGE_MATERIALRECEIPT_SELECT_LOCATION = YES_NO_MESSAGE + 312; //Please select location

        /// <summary>
        /// Please select a Master Location
        /// </summary>
        public const int MESSAGE_MATERIALRECEIPT_SELECT_MASTERLOCATION = YES_NO_MESSAGE + 311;
                         //Please select master location

        /// <summary>
        /// Please select Purchase Order
        /// </summary>
        public const int MESSAGE_MATERIALRECEIPT_SELECT_PO = YES_NO_MESSAGE + 321; //Please select purchase order

        /// <summary>
        /// Please select a Product
        /// </summary>
        public const int MESSAGE_MATERIALRECEIPT_SELECT_PRODUCT = YES_NO_MESSAGE + 310; //Please select product

        /// <summary>
        /// Please select Receipt Type
        /// </summary>
        public const int MESSAGE_MATERIALRECEIPT_SELECT_RECTYPE = YES_NO_MESSAGE + 313; //Please select receipt type

        /// <summary>
        /// Please select Work Order
        /// </summary>
        public const int MESSAGE_MATERIALRECEIPT_SELECT_WO = YES_NO_MESSAGE + 320; //Please select work order

        /// <summary>
        /// Please select Work Order Line or Purchase Order Line
        /// </summary>
        public const int MESSAGE_MATERIALRECEIPT_SELECT_WOLINE_OR_POLINE = YES_NO_MESSAGE + 315;
                         //Please select work order line or purchase order line

        /// <summary>
        /// Please select Work Order or Purchase Order
        /// </summary>
        public const int MESSAGE_MATERIALRECEIPT_SELECT_WOORPO = YES_NO_MESSAGE + 314;
                         //Please select work order or purchase order

        /// <summary>
        /// The Delivery Quantity must be higher than 0 and less than the remaining Delivery Qty
        /// </summary>
        public const int MESSAGE_MIN_DELIVERYQTY = YES_NO_MESSAGE + 77;
                         // The Delivery Quantity must be higher than 0 and less than the remaining Delivery Qty

        /// <summary>
        /// The Request Amount must be higher than 0 and less than the remaining Amount
        /// </summary>
        public const int MESSAGE_MIN_REQUESTAMOUNT = YES_NO_MESSAGE + 563;

        /// <summary>
        /// You can not delete the Activate row
        /// </summary>
        public const int MESSAGE_MP_CAN_NOT_DEL_ROW = YES_NO_MESSAGE + 196;
                         //You can't delete the row which has Activate field is true

        /// <summary>
        /// The date you have entered is invalid
        /// </summary>
        public const int MESSAGE_MP_DATE_INVALID = YES_NO_MESSAGE + 195; //The date you entered is invalid

        /// <summary>
        /// The From Date must be smaller than To Date
        /// </summary>
        public const int MESSAGE_MP_PERIODDATE = YES_NO_MESSAGE + 194;

        /// <summary>
        /// This MPS Cycle Option was used by DCP. Please delete related DCP data before regenerating MPS
        /// </summary>
        public const int MESSAGE_MPS_ALREADY_USED_IN_DCP = YES_NO_MESSAGE + 441;
                         // This MPS Cycle Option was used by DCP. Please delete related DCP data before regenerating MPS

        /// <summary>
        /// Master Location in grid must be unique
        /// </summary>
        public const int MESSAGE_MPS_CYCLE_OPTION_DUPLICATE_MASTERLOCATION = YES_NO_MESSAGE + 373;
                         //Duplicate Master Location in the grid, please input again! 

        /// <summary>
        /// The Plan Horizon must be greater than 0
        /// </summary>
        public const int MESSAGE_MPS_CYCLE_OPTION_PLANHORIZON_MUST_BE_GREATER_ZERO = YES_NO_MESSAGE + 372;
                         //The PlanHorizon must be greater than zero

        /// <summary>
        /// You must select MPS Cycle Option before processing MPS
        /// </summary>
        public const int MESSAGE_MPS_MUST_SELECT_CYCLE = YES_NO_MESSAGE + 428;
                         // You must select MPS Cycle Option before processing MPS

        /// <summary>
        /// Day in list must be unique 
        /// </summary>
        public const int MESSAGE_MPS_WD_DUPLICATE_DAY = YES_NO_MESSAGE + 367;
                         //Duplicate the day in the list, pls input againt

        /// <summary>
        /// Available Quantity is not enough to assign
        /// </summary>
        public const int MESSAGE_MRB_NOT_ENOUGH = YES_NO_MESSAGE + 362; //Available quantity is not enough to assign

        /// <summary>
        /// Quantity must be equal or smaller than Available Quantity
        /// </summary>
        public const int MESSAGE_MRB_OTHER_QUANTITY = YES_NO_MESSAGE + 356;
                         //The Quantity must be smaller or equal than Available Quantity

        /// <summary>
        /// Total Quantity must be equal to Available Quantity
        /// </summary>
        public const int MESSAGE_MRB_OTHER_TOTAL_QUANTITY = YES_NO_MESSAGE + 357;
                         //The Total quantity must be equal Available quantity

        /// <summary>
        /// Please enter Bin because this Location is controlled by Bin
        /// </summary>
        public const int MESSAGE_MRB_SELECT_BIN = YES_NO_MESSAGE + 360;
                         //Have to input Bin because Location controled by Bin

        /// <summary>
        /// Please enter Location
        /// </summary>
        public const int MESSAGE_MRB_SELECT_LOCATION = YES_NO_MESSAGE + 358;
                         //Have to input Location if inputed Quantity

        /// <summary>
        /// Please enter Lot because this Item is controlled by Lot
        /// </summary>
        public const int MESSAGE_MRB_SELECT_LOT = YES_NO_MESSAGE + 361;
                         //Have to input Lot because Item controled by Bin

        /// <summary>
        /// Please enter Quantity
        /// </summary>
        public const int MESSAGE_MRB_SELECT_QUANTITY = YES_NO_MESSAGE + 359;
                         //Have to input Quantity if inputed Location

        /// <summary>
        /// This Work Order Line is invalid because it is produced in more than 1 day
        /// Exam: This Work Order Line is invalid because it is produced in more than 1 day
        /// </summary>
        public const int MESSAGE_MULTIDAY_WOLINE = YES_NO_MESSAGE + 477;

        /// <summary>
        /// @ must be unique
        /// </summary>
        public const int MESSAGE_MUST_BE_UNIQUE = YES_NO_MESSAGE + 523;

        /// <summary>
        /// Can not select Bin for this Location
        /// </summary>
        public const int MESSAGE_MUST_CANNOT_SELECT_BIN_FOR_LOCATION = YES_NO_MESSAGE + 397;
                         // Cannot select Bin for this Location

        /// <summary>
        /// Must enter Group number first
        /// </summary>
        public const int MESSAGE_MUST_ENTER_GROUPS = YES_NO_MESSAGE + 545;

        /// <summary>
        /// Please enter Lot
        /// </summary>
        public const int MESSAGE_MUST_ENTER_LOT = YES_NO_MESSAGE + 375;
                         // Item is controlled by Lot. Please enter or select Lot

        /// <summary>
        /// Please select Bin for this Location
        /// </summary>
        public const int MESSAGE_MUST_SELECT_BIN_FOR_LOCATION = YES_NO_MESSAGE + 387;
                         // You must select Bin for this Location

        /// <summary>
        /// You must select a Location before select Bin
        /// </summary>
        public const int MESSAGE_MUST_SELECT_LOCATION = YES_NO_MESSAGE + 142;
                         //You must select a Location before select Bin

        /// <summary>
        /// Please select a Lot for this line
        /// </summary>
        public const int MESSAGE_MUST_SELECT_LOT = YES_NO_MESSAGE + 155;

        /// <summary>
        /// Please select a Master Location for this line
        /// </summary>
        public const int MESSAGE_MUST_SELECT_MASTERLOCATION = YES_NO_MESSAGE + 154;

        /// <summary>
        /// You must select a Purchase Order
        /// </summary>
        public const int MESSAGE_MUST_SELECT_PO = YES_NO_MESSAGE + 126; // You must select a Purchase Order

        /// <summary>
        /// Please select a product for this line
        /// </summary>
        public const int MESSAGE_MUST_SELECT_PRODUCT = YES_NO_MESSAGE + 153;

        /// <summary>
        /// Must select sale order line first
        /// </summary>
        public const int MESSAGE_MUST_SELECT_SALEORDERLINE_FIRST = YES_NO_MESSAGE + 188;
                         //Must select sale order line first

        /// <summary>
        /// You must select a Vendor before select Vendor location
        /// </summary>
        public const int MESSAGE_MUST_SELECT_VENDOR = YES_NO_MESSAGE + 141;
                         //You must select a Vendor before select Vendor location

        /// <summary>
        /// You must set up Unit of measure rate first
        /// </summary>
        public const int MESSAGE_MUST_SET_UMRATE = YES_NO_MESSAGE + 176;

        /// <summary>
        /// There are no data for your selected item. Please try again
        /// </summary>
        public const int MESSAGE_NO_RECORD = YES_NO_MESSAGE + 59;
                         // There are no data for your selected item. Please try again

        /// <summary>
        /// You don't have right to approve
        /// </summary>
        public const int MESSAGE_NO_RIGHT_TO_APPROVE = YES_NO_MESSAGE + 167; //You don't have right to approve

        /// <summary>
        /// You do not have any right to access the system
        /// </summary>
        public const int MESSAGE_NORIGHT_LOGIN = YES_NO_MESSAGE + 431;
                         //You don't have any right to access the system, please ask your administrator for a right

        /// <summary>
        /// Please insert a new delivery schedule
        /// </summary>
        public const int MESSAGE_NOROW_TOINSERT = YES_NO_MESSAGE + 75; // Please insert a new delivery schedule

        /// <summary>
        /// There is enough available amount, you cannot request more
        /// </summary>
        public const int MESSAGE_NOT_ENOUGH_AMOUNT = YES_NO_MESSAGE + 562;

        /// <summary>
        /// There is not enough quantiy of component to complete
        /// Exam: There is not enough quantiy of component to complete
        /// </summary>
        public const int MESSAGE_NOT_ENOUGH_COMPONENT_TO_COMPLETE = YES_NO_MESSAGE + 456;

        public const int MESSAGE_NOT_ENOUGH_QTY_TO_RELEASE = YES_NO_MESSAGE + 556;

        /// <summary>
        /// On hand quantity subtract Commit quantity must greater than Delivery quantity
        /// </summary>
        public const int MESSAGE_NOT_ENOUGH_QUANTITY = YES_NO_MESSAGE + 125;
                         // On hand quantity subtract Commit quantity must greater than Delivery quantity

        /// <summary>
        /// There is not enough quantiy of component @ to complete
        /// Exam: There is not enough quantiy of component XYZ to complete
        /// </summary>
        public const int MESSAGE_NOT_ENOUGH_QUANTITY_OF_COMPONENT_TO_COMPLETE = YES_NO_MESSAGE + 527;

        /// <summary>
        /// Do not found any Exchange Rate records which (begin date <= current date <= end date)
        /// </summary>
        public const int MESSAGE_NOT_FOUND_EXCHANGE_RATE = YES_NO_MESSAGE + 89;
                         // Do not found any Exchange Rate records which (begin date <= current date <= end date)

        /// <summary>
        /// This Item does not has any component to completion. Please check BOM of Item
        /// </summary>
        public const int MESSAGE_NOT_HAS_COMPONENT_TO_COMPLETE = YES_NO_MESSAGE + 532;

        /// <summary>
        /// The value of this field must be beween @ and @
        /// </summary>
        public const int MESSAGE_NUMBER_MUST_IN_SCOPE = YES_NO_MESSAGE + 434; //The number must be in scope

        /// <summary>
        /// This Transaction Number already existed, please enter another value
        /// </summary>
        public const int MESSAGE_OUTPROCESSING_DUPLICATE_TRANSNO = YES_NO_MESSAGE + 341;
                         //This transaction number already exist! Please enter another value! 

        /// <summary>
        /// Please enter Outside processing detail information 
        /// </summary>
        public const int MESSAGE_OUTPROCESSING_INPUT_DETAIL_INFOR = YES_NO_MESSAGE + 338;
                         //Please enter Outside processing detail information. 

        /// <summary>
        /// Please enter Invoice Amount for this Operation
        /// </summary>
        public const int MESSAGE_OUTPROCESSING_INPUT_INVOICE_AMOUNT = YES_NO_MESSAGE + 243;
                         //Please enter invoice amount for this operation.

        /// <summary>
        /// Please enter Operation that is outside processed
        /// </summary>
        public const int MESSAGE_OUTPROCESSING_INPUT_OPERATION = YES_NO_MESSAGE + 244;
                         //Please enter operation whick be outside processed.

        /// <summary>
        /// Please enter Vendor
        /// </summary>
        public const int MESSAGE_OUTPROCESSING_INPUT_VENDOR = YES_NO_MESSAGE + 241; //Please enter vendor.

        /// <summary>
        /// Please enter Vendor Location
        /// </summary>
        public const int MESSAGE_OUTPROCESSING_INPUT_VENDOR_LOCATION = YES_NO_MESSAGE + 242;
                         //Please enter vendor location.

        /// <summary>
        /// Please enter Work Order No
        /// </summary>
        public const int MESSAGE_OUTPROCESSING_INPUT_WO = YES_NO_MESSAGE + 239; //Please enter Work order No.

        /// <summary>
        /// Please enter Work Order Line
        /// </summary>
        public const int MESSAGE_OUTPROCESSING_INPUT_WO_LINE = YES_NO_MESSAGE + 240; //Please enter Work order line.

        /// <summary>
        /// Invalid Completed Percent. Completed Percent must be between 1 and 100
        /// </summary>
        public const int MESSAGE_OUTPROCESSING_INVALID_COMPLETEDPERCENT = YES_NO_MESSAGE + 339;
                         //Invalid completed percent. Completed percent must be between 0 and 100! 

        /// <summary>
        /// The value for field must be greater than 0
        /// </summary>
        public const int MESSAGE_OUTPROCESSING_INVALID_NUMBER = YES_NO_MESSAGE + 340;
                         //Invalid value. Please enter a positive value for this field! 

        /// <summary>
        /// The total delivery Quantity must be less than or equal to Order Quantity
        /// </summary>
        public const int MESSAGE_OVER_DELIVERYQTY = YES_NO_MESSAGE + 76;
                         //The total delivery Quantity must be less than or equal to Order Quantity

        /// <summary>
        /// Quantity must be smaller than Lot size
        /// </summary>
        public const int MESSAGE_OVER_LOT_SIZE = YES_NO_MESSAGE + 376; // You have input quantity over the Lot size

        /// <summary>
        /// The (@) can not be overlapped
        /// Exam: The (regular stop) can not be overlapped
        /// </summary>
        public const int MESSAGE_OVERLAP_NOT_ALLOWED = YES_NO_MESSAGE + 447; // 

        /// <summary>
        /// Packed date, time are not smaller than PackList's entry date
        /// </summary>
        public const int MESSAGE_PACKLISTNO_ERRORDATE = YES_NO_MESSAGE + 73;
                         //Packed date, time are not smaller than PackList's entry date

        /// <summary>
        /// The pack list number is not exist
        /// </summary>
        public const int MESSAGE_PACKLISTNO_NOTEXIST = YES_NO_MESSAGE + 74; //The pack list number is not exist

        /// <summary>
        /// The Trans Date must be in the Current Period
        /// </summary>
        public const int MESSAGE_PKL_TRANSDATE_PERIOD = YES_NO_MESSAGE + 303;
                         //The trans date must be in the current period, please reinput it again

        /// <summary>
        /// Please enter data for grid
        /// </summary>
        public const int MESSAGE_PLEASE_ENTER_DETAIL_INFOR = YES_NO_MESSAGE + 402;
                         //Please enter information into detail grid! 

        /// <summary>	
        /// You must clear @ before clear @
        /// </summary>
        public const int MESSAGE_PLS_CLEAR_RELATE_FIRST = YES_NO_MESSAGE + 516;

        /// <summary>
        /// Total values of Commit Quantity column must be equal or smaller than Total Available Quantity 
        /// </summary>
        public const int MESSAGE_PLS_RE_INPUT_COMMITQTY = YES_NO_MESSAGE + 419;
                         //You can not input this value to Commit Quantity column, because total Available Quantity is now less than total Commit Quantity

        /// <summary>
        /// Capacity must be greater than zero, please input detail information to adjust DCP!
        /// </summary>
        public const int MESSAGE_PLZ_INSERT_DETAIL_INFORATION_TO_ADJUSTMENT_DCP = YES_NO_MESSAGE + 486;

        /// <summary>
        /// There  are no detail information
        /// </summary>
        public const int MESSAGE_PO_APPROVE_NO_DATA_IN_GRID = YES_NO_MESSAGE + 169; //The grid have no data

        /// <summary>
        /// Can not delete this Purchase Order Line because it was scheduled for delivery
        /// </summary>
        public const int MESSAGE_PO_POLINE_HAS_DELIVERY = YES_NO_MESSAGE + 442;
                         //Can not delete this Purchase Order Line because it was scheduled for delivery

        /// <summary>
        /// Unit Price field can not be 0
        /// </summary>
        public const int MESSAGE_PO_UP_CAN_NOT_BE_ZERO = YES_NO_MESSAGE + 198; // The Unit Price field can not be zero!

        /// <summary>
        /// The Purchase Order you have selected was deleted
        /// </summary>
        public const int MESSAGE_PO_WAS_DELETED = YES_NO_MESSAGE + 399; //The PO you have selected was deleted

        /// <summary>
        /// You cannot delete this delivery schedule because it already has received quantity
        /// </summary>
        public const int MESSAGE_PODELIVERY_CANNOTDELETE = YES_NO_MESSAGE + 123;
                         //You cannot delete this delivery schedule because it already has received quantity

        /// <summary>
        /// There is enough order delivery quantity, you cannot add more
        /// </summary>
        public const int MESSAGE_PODELIVERY_ENOUGHQTY = YES_NO_MESSAGE + 124;
                         //There is enough order delivery quantity, you cannot add more

        /// <summary>
        /// Please enter detail information for this transaction
        /// </summary>
        public const int MESSAGE_PORECEIPT_INPUT_DETAIL = YES_NO_MESSAGE + 306;
                         //Please input the detail for this transaction

        /// <summary>
        /// The Order quantity field must be higher than 0.
        /// </summary>
        public const int MESSAGE_POSITIVE_ORDER_QUANTITY = YES_NO_MESSAGE + 150;

        /// <summary>
        /// Can not receive this Line because its Post Date is less than Approval Date
        /// </summary>
        public const int MESSAGE_POST_DATE_LESS_THAN_APPROVAL_DATE = YES_NO_MESSAGE + 393;
                         //Cannot receive this line because of Post Date < ApprovalDate

        /// <summary>
        /// This file does not exist, please correct the path 
        /// </summary>
        public const int MESSAGE_PRINTCONFIG_FILE_NOT_EXIST = YES_NO_MESSAGE + 424;
                         //This file does not exist, please correct the path 

        /// <summary>
        /// @ Generation process is running. Do you want to stop
        /// Exam: MPS Generation process is running. Do you want to stop
        /// </summary>
        public const int MESSAGE_PROCESS_IS_RUNNING = YES_NO_MESSAGE + 461;

        /// <summary>
        /// This Primary Vendor was set Receiving Schedule, please re-set its Receiving Schedule before changing Primary Vendor
        /// </summary>
        public const int MESSAGE_PRODUCT_HAS_DELIVERY_SCHEDULE = YES_NO_MESSAGE + 512;

        /// <summary>
        /// @ of a Product are not identical in this Invoice. Please correct before continuing.
        /// </summary>
        public const int MESSAGE_PRODUCT_WITH_MULTIPLE_TAXRATE_IN_INVOICE = YES_NO_MESSAGE + 525;

        /// <summary>
        /// Please select an existing product item before doing this action
        /// </summary>
        public const int MESSAGE_PRODUCTINFO_SELECTPRODUCT = YES_NO_MESSAGE + 52;
                         // Please select an existing product item before doing this action

        /// <summary>
        /// Production Line must has one main Work Center
        /// Exam: Production Line must has one main Work Center
        /// </summary>
        public const int MESSAGE_PRODUCTION_LINE_MUST_HAS_MAIN_WORK_CENTER = YES_NO_MESSAGE + 464;

        /// <summary>
        /// The Purchase Order is approved, you can not modify or delete
        /// </summary>
        public const int MESSAGE_PURCHASE_ORDER_APPROVED = YES_NO_MESSAGE + 103;
                         //The Purchase Order is approved, you can not modify or delete

        /// <summary>
        /// Password is encrypting
        /// </summary>
        public const int MESSAGE_PWD_ENCRYPT = YES_NO_MESSAGE + 17; // Password is encrypting

        /// <summary>
        /// Do you want to @ before closing
        /// </summary>
        public const int MESSAGE_QUESTION_RELEASE_BEFORE_CLOSE = YES_NO_MESSAGE + 438; // Do you want to @ before close?

        /// <summary>
        /// Do you want to store data into database
        /// </summary>
        public const int MESSAGE_QUESTION_STORE_INTO_DATABASE = YES_NO_MESSAGE + 41;
                         // Do you want to store data into database?

        /// <summary>
        /// Receive quantity is greater than total delivery. do you want to continue
        /// </summary>
        public const int MESSAGE_RECEIVE_GREATER_THAN_TOTAL = YES_NO_MESSAGE + 170;

        /// <summary>
        /// Receive quantity must between zero and order quantity 
        /// </summary>
        public const int MESSAGE_RECEIVEQUANTITY_GREATERTHAN_ORDERQUANTITY = YES_NO_MESSAGE + 165;

        /// <summary>
        /// Can not view Recycled Material Slip cause this transaction has more than one destinations (To Location or To Bin)
        /// </summary>
        public const int MESSAGE_RECYCLED_HAS_MORE_THAN_ONE_DESTINATION = YES_NO_MESSAGE + 509;

        /// <summary>
        /// Please enter @ for @
        /// </summary>
        public const int MESSAGE_RELATION_REQUIRE = YES_NO_MESSAGE + 439; //Please insert @ for @

        /// <summary>
        /// Please select Work order line before continuing
        /// </summary>
        public const int MESSAGE_RELEASE_WO_SELECT_WOLINE = YES_NO_MESSAGE + 238;
                         //Please select Work order line before continue.

        /// <summary>
        /// From Start Date must be smaller than To Start Date
        /// </summary>
        public const int MESSAGE_RELEASE_WO_VALIDATETODATE = YES_NO_MESSAGE + 199;
                         // The From StartDate must be smaller than To StartDate!

        /// <summary>
        /// Do you want to remove all DCP results of this DCP Option
        /// Exam: Do you want to remove all DCP results of this DCP Option
        /// </summary>
        public const int MESSAGE_REMOVE_DCPRESULTS = YES_NO_MESSAGE + 470;

        /// <summary>
        /// Please remove granted role on the right before granting new role
        /// Exam: Please remove granted role on the right before granting new role 
        /// </summary>
        public const int MESSAGE_REMOVE_GRANTED_ROLE_ON_THE_RIGHT = YES_NO_MESSAGE + 475;

        /// <summary>
        /// Do you want to set margin to default values
        /// Exam: Do you want to set margin to default values
        /// </summary>
        public const int MESSAGE_REPORT_DEFAULT_MARGIN = YES_NO_MESSAGE + 460;

        /// <summary>
        /// Can not copy since a copy of this report already existed
        /// </summary>
        public const int MESSAGE_REPORT_EXISTED = YES_NO_MESSAGE + 432;
                         //A copy of this report already existed in system

        /// <summary>
        /// Report file is not found 
        /// </summary>
        public const int MESSAGE_REPORT_FILE_NOT_FOUND = YES_NO_MESSAGE + 421; // Report file is not found

        /// <summary>
        /// Do you want to set this font to default
        /// Exam: Do you want to set this font to default
        /// </summary>
        public const int MESSAGE_REPORT_SETUP_DEFAULT_FONT = YES_NO_MESSAGE + 459;

        /// <summary>
        /// Report Template file is not found 
        /// </summary>
        public const int MESSAGE_REPORT_TEMPLATE_FILE_NOT_FOUND = YES_NO_MESSAGE + 427;
                         // Report template file is not found

        /// <summary>
        /// Request Amount must less than Total Completion Amount
        /// </summary>
        public const int MESSAGE_REQUEST_AMOUNT_OVER_COMPLETE = YES_NO_MESSAGE + 555;

        /// <summary>
        /// @ Amount must less than Total Amount - Total Requested Amount
        /// </summary>
        public const int MESSAGE_REQUEST_AMOUNT_OVER_TOTAL = YES_NO_MESSAGE + 554;

        /// <summary>
        /// Total required quantity must less than estimated quantity
        /// </summary>
        public const int MESSAGE_REQUIRED_OVER_QTY = YES_NO_MESSAGE + 560;

        /// <summary>
        /// Require Date must be greater than Transaction Date
        /// </summary>
        public const int MESSAGE_REQUIREDATE_GREATER_THAN_TRANSDATE = YES_NO_MESSAGE + 197;
                         // Require date has to greater than transaction date?

        /// <summary>
        /// Please select CNN
        /// </summary>
        public const int MESSAGE_RGA_CCN = YES_NO_MESSAGE + 105; // Please select CNN

        /// <summary>
        /// Please select a customer
        /// </summary>
        public const int MESSAGE_RGA_CUSTOMER = YES_NO_MESSAGE + 108; //Please select a customer

        /// <summary>
        /// Please select a customer location
        /// </summary>
        public const int MESSAGE_RGA_CUSTOMERLOCATION = YES_NO_MESSAGE + 109; //Please select a customer location

        /// <summary>
        /// Please insert a returned goods detail
        /// </summary>
        public const int MESSAGE_RGA_DETAIL = YES_NO_MESSAGE + 110; // Please insert a returned goods detail

        /// <summary>
        /// This product is already selected, please select another one
        /// </summary>
        public const int MESSAGE_RGA_DUPLICATEPRO = YES_NO_MESSAGE + 118;
                         // This product is already selected, please select another one

        /// <summary>
        /// Please input the entry date for the returned goods transaction
        /// </summary>
        public const int MESSAGE_RGA_ENTRYDATE = YES_NO_MESSAGE + 106;
                         //Please input the entry date for the returned goods transaction

        /// <summary>
        /// Please select a location for storing this product
        /// </summary>
        /// 
        public const int MESSAGE_RGA_LOCATION = YES_NO_MESSAGE + 114;
        // Please select a location for storing this product

        /// <summary>
        /// Please select master location
        /// </summary>
        public const int MESSAGE_RGA_MASTERLOCATION = YES_NO_MESSAGE + 104; // Please select master location

        /// <summary>
        /// The quantity you want to return to vendor exceeds the quantity in inventory
        /// </summary>
        public const int MESSAGE_RGA_OVERONHANDQTY = YES_NO_MESSAGE + 177;
                         //The quantity you want to return to vendor exceed 

        /// <summary>
        /// Please select a product
        /// </summary>
        public const int MESSAGE_RGA_PRODUCT = YES_NO_MESSAGE + 111; // Please select a product

        /// <summary>
        /// Please input the receive quantity for the detail returned goods
        /// </summary>
        public const int MESSAGE_RGA_RECEIVEQTY = YES_NO_MESSAGE + 112;
                         //Please input the receive quantity for the detail returned goods

        /// <summary>
        /// Received quantity cannot greater than total commited quantity
        /// </summary>
        public const int MESSAGE_RGA_RECEIVEQTYTOCOMMIT = YES_NO_MESSAGE + 120;
                         //Received quantity cannot greater than total commited quantity

        /// <summary>
        /// The received quantity must be higher than zero
        /// </summary>
        public const int MESSAGE_RGA_RECEIVEQTYTOZERO = YES_NO_MESSAGE + 119;
                         // The received quantity must be higher than zero

        /// <summary>
        /// Please input the returned goods number
        /// </summary>
        public const int MESSAGE_RGA_RETURNEDNUMBER = YES_NO_MESSAGE + 107; //Please input the returned goods number

        /// <summary>
        /// Please select a location first
        /// </summary>
        public const int MESSAGE_RGA_SELECTLOC = YES_NO_MESSAGE + 117; // Please select a location first

        /// <summary>
        /// Please select a master location first
        /// </summary>
        public const int MESSAGE_RGA_SELECTMASLOC = YES_NO_MESSAGE + 116; // Please select a master location first

        /// <summary>
        /// Please select an extisting returned goods number first
        /// </summary>
        public const int MESSAGE_RGA_SELECTTOEDIT = YES_NO_MESSAGE + 115;
                         // Please select an extisting returned goods number first

        /// <summary>
        /// Please select a unit of measure for the product
        /// </summary>
        public const int MESSAGE_RGA_UNIT = YES_NO_MESSAGE + 113; // Please select a unit of measure for the product

        /// <summary>
        /// The Approve Date you have entered must be older than Order Date 
        /// </summary>
        public const int MESSAGE_RGV_APPROVE_DATE_MUST_OLDER_THAN_ORDER_DATE = YES_NO_MESSAGE + 305;
                         //The Approve Date you have entered must be older than Order Date

        /// <summary>
        /// Please input the buying unit for the product
        /// </summary>
        public const int MESSAGE_RGV_BUYINGUNIT = YES_NO_MESSAGE + 140; //Please input the buying unit for the product

        /// <summary>
        /// The Cancel Date you have entered must be older than Order Date 
        /// </summary>
        public const int MESSAGE_RGV_CANCEL_APPROVE_DATE_MUST_OLDER_THAN_ORDER_DATE = YES_NO_MESSAGE + 489;

        /// <summary>
        /// Please first select the CCN
        /// </summary>
        public const int MESSAGE_RGV_FIRSTSELECTCCN = YES_NO_MESSAGE + 138; //Please first select the CCN

        /// <summary>
        /// Please select select the product first
        /// </summary>
        public const int MESSAGE_RGV_FIRSTSELECTPRO = YES_NO_MESSAGE + 135; //Please select select the product first

        /// <summary>
        /// /Please first select the vendor 
        /// </summary>
        public const int MESSAGE_RGV_FIRSTSELECTVENDOR = YES_NO_MESSAGE + 137; //Please first select the vendor 

        /// <summary>
        /// Please select the master location
        /// </summary>
        public const int MESSAGE_RGV_MASTERLOCATION = YES_NO_MESSAGE + 127; //Please select the master location

        /// <summary>
        /// Please select a Purchase Order
        /// </summary>
        public const int MESSAGE_RGV_MUST_SELECT_PURCHASE_ORDER = YES_NO_MESSAGE + 301;
                         // Please select the purchase order

        /// <summary>
        /// Please insert a list of product to return to vendor
        /// </summary>
        public const int MESSAGE_RGV_NOPRODUCT = YES_NO_MESSAGE + 133;
                         //Please insert a list of product to return to vendor

        /// <summary>
        /// Do not have rate for these units
        /// </summary>
        public const int MESSAGE_RGV_NOUMRATE = YES_NO_MESSAGE + 136; //Do not have rate for these units

        /// <summary>
        /// Please input the return to vendor number
        /// </summary>
        public const int MESSAGE_RGV_NUMBER = YES_NO_MESSAGE + 130; //Please input the return to vendor number 

        /// <summary>
        /// Please input the post date
        /// </summary>
        public const int MESSAGE_RGV_POSTDATE = YES_NO_MESSAGE + 128; // Please input the post date

        /// <summary>
        /// The Post Date must be equal or higher than the Purchase Order Trans Date
        /// </summary>
        public const int MESSAGE_RGV_POSTDATE_MUST_HIGHER_THANPODATE = YES_NO_MESSAGE + 304;
                         //The post date must be higher than or equal to the trans date in Purchase Order

        /// <summary>
        /// The post date you entered is not in the current period, please enter it again 
        /// </summary>
        public const int MESSAGE_RGV_POSTDATE_PERIOD = YES_NO_MESSAGE + 193;
                         //The post date you entered is not in the current period, please reinput it again

        /// <summary>
        /// Please select the purchase approver first
        /// </summary>
        public const int MESSAGE_RGV_PURCHASE_APPROVER = YES_NO_MESSAGE + 166;
                         //Please select the purchase approve first

        /// <summary>
        /// Please select at least one line to approve
        /// </summary>
        public const int MESSAGE_RGV_PURCHASE_APPROVER_SELECT_LINE = YES_NO_MESSAGE + 302;
                         //Please select at least one line to approve.

        /// <summary>
        /// Please select the purchase location from the vendor
        /// </summary>
        public const int MESSAGE_RGV_PURLOC = YES_NO_MESSAGE + 131;
                         //Please select the purchase location from the vendor

        /// <summary>
        /// The return quantity must be between zero and received quantity
        /// </summary>
        public const int MESSAGE_RGV_RETURNQTY = YES_NO_MESSAGE + 134;
                         //The return quantity must be between zero and received quantity

        /// <summary>
        /// Please select the vendor to return goods to
        /// </summary>
        public const int MESSAGE_RGV_SELECTVENDOR = YES_NO_MESSAGE + 129; //Please select the vendor to return goods to

        /// <summary>
        /// Please select the shipping location from the vendor
        /// </summary>
        public const int MESSAGE_RGV_SHIPLOC = YES_NO_MESSAGE + 132;
                         //Please select the shipping location from the vendor

        /// <summary>
        /// Please input the stock unit for the product
        /// </summary>
        public const int MESSAGE_RGV_STOCKUNIT = YES_NO_MESSAGE + 139; //Please input the stock unit for the product

        /// <summary>
        /// Role name is duplicated, please input another name
        /// </summary>
        public const int MESSAGE_ROLE_NAME_DUPLICATE = YES_NO_MESSAGE + 12;
                         // Role name is duplicated, please input another name

        /// <summary>
        /// You have to select a role for modification
        /// </summary>
        public const int MESSAGE_ROLE_SELECT = YES_NO_MESSAGE + 11; // You have to select a role for modification

        /// <summary>
        /// The date you entered is not in the current period, please reinput it again
        /// </summary>
        public const int MESSAGE_RTG_ENTRYDATE = YES_NO_MESSAGE + 192;
                         //The date you entered is not in the current period, please reinput it again

        /// <summary>
        /// F.Goods Code is already converted to Work Order.
        /// </summary>
        public const int MESSAGE_SALE_IS_CONVERTED = YES_NO_MESSAGE + 557;

        /// <summary>
        /// Difference Account cannot be the same as Account
        /// </summary>
        public const int MESSAGE_SAME_DIFFACC = YES_NO_MESSAGE + 590;

        /// <summary>
        /// Importing requires all changes must be saved, do you want to save changes and import
        /// </summary>
        public const int MESSAGE_SAVE_BEFORE_IMPORT = YES_NO_MESSAGE + 453;

        /// <summary>
        /// You must save data before manage Location. Save now
        /// </summary>
        public const int MESSAGE_SAVE_BEFORE_MANAGE_CONTACT = YES_NO_MESSAGE + 98;
                         // You must save data before manage Location. Save now

        /// <summary>
        /// You must save data before manage Location. Save now
        /// </summary>
        public const int MESSAGE_SAVE_BEFORE_MANAGE_LOCATION = YES_NO_MESSAGE + 97;
                         // You must save data before manage Location. Save now

        /// <summary>
        /// Please save your change before view single record
        /// </summary>
        public const int MESSAGE_SAVE_BEFORE_VIEW_RECORD = YES_NO_MESSAGE + 68;
                         // Please save your change before view single record

        /// <summary>
        /// Please select Account(s) for this transaction
        /// </summary>
        public const int MESSAGE_SELECT_ACCOUNT = YES_NO_MESSAGE + 542;

        /// <summary>
        /// At least one line must be selected to commit
        /// Exam: At least one line must be selected to commit
        /// </summary>
        public const int MESSAGE_SELECT_AT_LEAST_LINE_TO_COMMIT = YES_NO_MESSAGE + 480;

        /// <summary>	
        /// Please select a Plan Type before converting data to Work Order
        /// </summary>
        public const int MESSAGE_SELECT_DCP_OR_MPS_BEFORE_CONVERT_WO = YES_NO_MESSAGE + 518;

        /// <summary>
        /// Please select @ before selecting @
        /// Exam: Please select Customer before selecting Buy Location
        /// </summary>
        public const int MESSAGE_SELECT_ONE_BEFORE_SELECT_ONE = YES_NO_MESSAGE + 463;

        /// <summary>
        /// Roll up code successful.
        /// </summary>
        ///public const int MESSAGE_ROLLUP_SUCCEED = YES_NO_MESSAGE + 151;
        /// <summary>
        /// Please select a product and CCN for this form
        /// </summary>
        public const int MESSAGE_SELECT_PRODUCT_CCN = YES_NO_MESSAGE + 152;
                         //Please select a product and CCN for this form

        /// <summary>
        /// Please select a production line
        /// </summary>
        public const int MESSAGE_SELECT_PRODUCTION_LINE = YES_NO_MESSAGE + 564;

        /// <summary>
        /// At least one row must be selected to convert
        /// Exam: At least one row must be selected to convert
        /// </summary>
        public const int MESSAGE_SELECT_ROW_TO_CONVERT = YES_NO_MESSAGE + 465;

        /// <summary>
        /// Please select a sheet first
        /// </summary>
        public const int MESSAGE_SELECT_SHEET = YES_NO_MESSAGE + 537;

        /// <summary>
        /// You have to select Vendor before select Vendor Location, please
        /// </summary>
        public const int MESSAGE_SELECT_VENDOR_BEFORE_VENDORLOC = YES_NO_MESSAGE + 149;

        /// <summary>
        /// The selected Work Center does not belong to the selected Production Line. Please select another one
        /// </summary>
        public const int MESSAGE_SELECTED_WC_NOT_IN_PRODUCTIONLINE = YES_NO_MESSAGE + 514;

        /// <summary>
        /// The selected (MPS Cycle) is not exists in system. Please select another one
        /// </summary>
        public const int MESSAGE_SELECTION_NOT_EXIST = YES_NO_MESSAGE + 446;

        /// <summary>
        /// Please setup Accounts in Account Payable Parameters
        /// </summary>
        public const int MESSAGE_SETUP_APPARAM = YES_NO_MESSAGE + 567;

        /// <summary>
        /// Please setup CGO Account in AP Parameters
        /// </summary>
        public const int MESSAGE_SETUP_CGO_ACCOUNT_AP = YES_NO_MESSAGE + 575;

        /// <summary>
        /// Please setup Cost Account for Department
        /// </summary>
        public const int MESSAGE_SETUP_COST_ACCOUNT_DEPARTMENT = YES_NO_MESSAGE + 573;

        /// <summary>
        /// Please setup Department for Fixed Asset
        /// </summary>
        public const int MESSAGE_SETUP_DEPARTMENT_FA = YES_NO_MESSAGE + 574;

        /// <summary>
        /// Please setup Deposit Account in AP Parameters
        /// </summary>
        public const int MESSAGE_SETUP_DEPOSIT_ACCOUNT_AP = YES_NO_MESSAGE + 583;

        /// <summary>
        /// Please setup Depreciation Account
        /// </summary>
        public const int MESSAGE_SETUP_DEPRECIATION_ACCOUNT = YES_NO_MESSAGE + 572;

        /// <summary>
        /// Please setup EAC Account in AP Parameters
        /// </summary>
        public const int MESSAGE_SETUP_EAC_ACCOUNT_AP = YES_NO_MESSAGE + 580;

        /// <summary>
        /// Please setup Employee Payable Account in AP Parameters
        /// </summary>
        public const int MESSAGE_SETUP_EMPLOYEE_PAYABLE_ACCOUNT_AP = YES_NO_MESSAGE + 582;

        /// <summary>
        /// Please setup FA Of Financial Credit Account in AP Parameters
        /// </summary>
        public const int MESSAGE_SETUP_FAOFFINANCIALCREDIT_ACCOUNT_AP = YES_NO_MESSAGE + 579;

        /// <summary>
        /// Please setup Accounts in Finance Parameters
        /// </summary>
        public const int MESSAGE_SETUP_FINANCEPARAM = YES_NO_MESSAGE + 586;

        /// <summary>
        /// Please setup Gain Exchange Account in Finance Parameters
        /// </summary>
        public const int MESSAGE_SETUP_GAINEXCHANGE_ACCOUNT_FIN = YES_NO_MESSAGE + 588;

        /// <summary>
        /// Please setup Import Tax Account in AP Parameters
        /// </summary>
        public const int MESSAGE_SETUP_IMPORTTAX_ACCOUNT_AP = YES_NO_MESSAGE + 584;

        /// <summary>
        /// Please setup Intangible FA Account in AP Parameters
        /// </summary>
        public const int MESSAGE_SETUP_INTANGIBLEFA_ACCOUNT_AP = YES_NO_MESSAGE + 578;

        /// <summary>
        /// Please setup Inventory Account for Item
        /// </summary>
        public const int MESSAGE_SETUP_INVENTORYACCOUNT = YES_NO_MESSAGE + 566;

        /// <summary>
        /// Please setup Loss Exchange Account in Finance Parameters
        /// </summary>
        public const int MESSAGE_SETUP_LOSSEXCHANGE_ACCOUNT_FIN = YES_NO_MESSAGE + 587;

        /// <summary>
        /// Please setup Payable Account in AP Parameters
        /// </summary>
        public const int MESSAGE_SETUP_PAYABLE_ACCOUNT_AP = YES_NO_MESSAGE + 581;

        /// <summary>
        /// Please setup Post Slip
        /// </summary>
        public const int MESSAGE_SETUP_POSTSLIP = YES_NO_MESSAGE + 552;

        /// <summary>
        /// Please setup Revenue Account for Item
        /// </summary>
        public const int MESSAGE_SETUP_RECEIVABLE_ACCOUNT_PARTY = YES_NO_MESSAGE + 570;

        /// <summary>
        /// Please setup Revenue Account for Item
        /// </summary>
        public const int MESSAGE_SETUP_REVENUEACCOUNT = YES_NO_MESSAGE + 568;

        /// <summary>
        /// Please setup Sale Costs Account in AP Parameters
        /// </summary>
        public const int MESSAGE_SETUP_SALECOSTS_ACCOUNT_AP = YES_NO_MESSAGE + 576;

        /// <summary>
        /// Please setup Tangible FA Account in AP Parameters
        /// </summary>
        public const int MESSAGE_SETUP_TANGIBLEFA_ACCOUNT_AP = YES_NO_MESSAGE + 577;

        /// <summary>
        /// Please setup Trade Discount Account for Item
        /// </summary>
        public const int MESSAGE_SETUP_TRADEDISCOUNT_ACCOUNT_ITEM = YES_NO_MESSAGE + 569;

        /// <summary>
        /// Please setup VAT Input Account in AP Parameters
        /// </summary>
        public const int MESSAGE_SETUP_VATINPUT_ACCOUNT_AP = YES_NO_MESSAGE + 585;

        /// <summary>
        /// Please setup VAT Output Account in AR parameter
        /// </summary>
        public const int MESSAGE_SETUP_VATOUPUT_ACCOUNT_ARSETUP = YES_NO_MESSAGE + 571;

        /// <summary>
        /// This shift has not been set pattern yet, please select another shift
        /// </summary>
        public const int MESSAGE_SHIFT_DOES_NOT_HAS_SHIFT_PATTERN = YES_NO_MESSAGE + 444;
                         //This shift has not been set pattern yet, please select another shift

        /// <summary>
        /// @ must be smaller than @
        /// Exam: From Month must be smaller than To Month
        /// </summary>
        public const int MESSAGE_SMALLER_THAN = YES_NO_MESSAGE + 551;

        /// <summary>	
        /// This Product can not be changed because its @ set Delivery Schedule
        /// </summary>
        public const int MESSAGE_SO_CAN_NOT_CHANGE_ITEM = YES_NO_MESSAGE + 519;

        /// <summary>
        /// This @ was released, you can not edit or delete it
        /// </summary>
        public const int MESSAGE_SO_HAS_BEEN_RELEASED = YES_NO_MESSAGE + 513;

        /// <summary>
        /// Can not delete this Sale Order Line because it was scheduled for delivery
        /// Exam: Can not delete this Sale Order Line because it was scheduled for delivery
        /// </summary>
        public const int MESSAGE_SOLINE_HAS_DELIVERY = YES_NO_MESSAGE + 457;

        /// <summary>
        ///  Some rows in RED have not been converted
        /// Exam: Some rows in RED have not been converted
        /// </summary>
        public const int MESSAGE_SOME_CPO_CAN_NOT_CONVERTED = YES_NO_MESSAGE + 485;

        /// <summary>
        /// Some data has been deleted, please re-input them
        /// Exam: Some data has been deleted, please re-input them
        /// </summary>
        public const int MESSAGE_SOME_DATA_DELETED = YES_NO_MESSAGE + 466;

        /// <summary>
        /// You are unable to change [@], because it was closed
        /// </summary>
        public const int MESSAGE_SOMETHING_CLOSED = YES_NO_MESSAGE + 536;

        /// <summary>
        /// Value must be betweeen previous step and less than next step in grid routing
        /// </summary>
        public const int MESSAGE_STEP_GREATER_THAN = YES_NO_MESSAGE + 67;
                         // Value must be betweeen previous step and less than next step in grid routing

        /// <summary>
        /// The Stock Taking Date must be between in [@] and [@]
        /// </summary>
        public const int MESSAGE_STOCK_TAKING_BETWEEN_IN = YES_NO_MESSAGE + 535;

        /// <summary>
        /// Sum of Delivery and Adjustment must be greater than @
        /// Exam: Sum of Delivery and Adjustment must be greater than 0
        /// </summary>
        public const int MESSAGE_SUM_OF_DELIVERY_ADJUSTMENT_GREATER_THAN = YES_NO_MESSAGE + 474;

        /// <summary>
        /// Your new table is added to group successfully.Click Detail Config to edit its fiels
        /// </summary>
        public const int MESSAGE_TABLEMANAGEMENT_AFTER_ADDTABLE = YES_NO_MESSAGE + 33;
                         // Your new table is added to group successfully.Click Detail Config to edit its fiels

        /// <summary>
        /// This group is copied to a new group successfully
        /// </summary>
        public const int MESSAGE_TABLEMANAGEMENT_AFTER_COPYGROUP = YES_NO_MESSAGE + 32;
                         // This group is copied to a new group successfully

        /// <summary>
        /// Your table information is saved to group successfully.Click Detail Config to edit its fields
        /// </summary>
        public const int MESSAGE_TABLEMANAGEMENT_AFTER_EDITTABLE = YES_NO_MESSAGE + 34;
                         // Your table information is saved to group successfully.Click Detail Config to edit its fields

        /// <summary>
        /// Your table is pasted to a new group successfully
        /// </summary>
        public const int MESSAGE_TABLEMANAGEMENT_AFTER_PASTETABLE = YES_NO_MESSAGE + 28;
                         // Your table is pasted to a new group successfully

        /// <summary>
        /// The table you want to paste is already existed in this group
        /// </summary>
        public const int MESSAGE_TABLEMANAGEMENT_DUPLICATE_TABLE = YES_NO_MESSAGE + 29;
                         // The table you want to paste is already existed in this group

        /// <summary>
        /// The table you want to paste is already existed. Do you want to cancel copy?
        /// </summary>
        public const int MESSAGE_TABLEMANAGEMENT_DUPLICATE_TABLE_ASK_YESNO = YES_NO_MESSAGE + 36;
                         // The table you want to paste is already existed. Do you want to cancel copy?

        /// <summary>
        /// Please select a node on tree
        /// </summary>
        public const int MESSAGE_TABLEMANAGEMENT_FOCUS = YES_NO_MESSAGE + 18; // Please select a node on the tree

        /// <summary>
        /// The height value must be numeric
        /// </summary>
        public const int MESSAGE_TABLEMANAGEMENT_HEIGH_NUMERIC = YES_NO_MESSAGE + 35;
                         // The height value must be numeric

        /// <summary>
        /// Please select a group to copy
        /// </summary>
        public const int MESSAGE_TABLEMANAGEMENT_SELECT_GROUP_TOCOPY = YES_NO_MESSAGE + 31;
                         // Please select a group to copy

        /// <summary>
        /// Please select a table to copy
        /// </summary>
        public const int MESSAGE_TABLEMANAGEMENT_SELECT_TABLE_TOCOPY = YES_NO_MESSAGE + 27;
                         // Please select a table to copy

        /// <summary>
        /// Please select a group to paste
        /// </summary>
        public const int MESSAGE_TABLEMANAGEMENT_SELECT_TABLE_TOPASTE = YES_NO_MESSAGE + 30;
                         // Please select a group to paste

        /// <summary>
        /// @ has been completed successfully
        /// Exam: Import has been completed successfully
        /// </summary>
        public const int MESSAGE_TASK_COMPLETED = YES_NO_MESSAGE + 454;

        /// <summary>
        /// Quantity of this Lot is over LotSize 
        /// </summary>
        public const int MESSAGE_THIS_LOT_IS_OVER_LOTSIZE = YES_NO_MESSAGE + 425; //Quantity of this Lot is over LotSize

        /// <summary>
        /// The prefix of @ must be @
        /// Exam: The prefix of Trans No must be PL
        /// </summary>
        public const int MESSAGE_TRANSACTION_HAS_TO_HAVE_PREFIX = YES_NO_MESSAGE + 479;

        /// <summary>
        /// Please configure Unit Of Measure Rate between @ and @ 
        /// Exam: Please configure Unit Of Measure Rate between m and cm
        /// </summary>
        public const int MESSAGE_UMRATE_IS_NOT_CONFIGURATED = YES_NO_MESSAGE + 483;

        /// <summary>
        /// You must set uncheck for all other columns before set uncheck for this column
        /// </summary>
        public const int MESSAGE_UNCHECK_OTHER_COLUMNS_BEFORE_VIEW = YES_NO_MESSAGE + 156;

        /// <summary>
        /// Successfull update begin stock for DCP Report
        /// </summary>
        public const int MESSAGE_UPDATE_BEGIN_FOR_REPORT_SUCCESS = YES_NO_MESSAGE + 530;

        /// <summary>
        /// You have to click at the button to select a new value
        /// </summary>
        public const int MESSAGE_USE_BUTTON_TO_SELECT_VALUE = YES_NO_MESSAGE + 6;
                         // You have to click at the button to select a new value

        /// <summary>
        /// Value @ you have entered does not match with data type @
        /// </summary>
        public const int MESSAGE_VALUE_AND_TYPE_NOTMATCH = YES_NO_MESSAGE + 443;
                         //Value 123 you have entered does not match with data type Boolean

        /// <summary>
        /// New Code is too long, please select another one
        /// </summary>
        public const int MESSAGE_VALUE_TOO_LONG = YES_NO_MESSAGE + 433; //New @ is too long, please select another one

        /// <summary>
        /// Vendor does not exist
        /// </summary>
        public const int MESSAGE_VENDOR_DONT_EXIST = YES_NO_MESSAGE + 69; // Vendor does not exist

        /// <summary>
        /// Please save data before selecting this row
        /// </summary>
        public const int MESSAGE_VIEWTABLE_SAVEBEFORE_SELECTVALUE = YES_NO_MESSAGE + 47;
                         // Please save data before selecting this row 

        /// <summary>
        /// Please select Shift
        /// </summary>
        public const int MESSAGE_WCC_PLEASE_SELECT_SHIFT = YES_NO_MESSAGE + 403;
                         //Please select shift in the list for this capacity!

        /// <summary>
        /// The date time you have entered is not between the Work Order Line Date Time period
        /// </summary>
        public const int MESSAGE_WCDISPATCH_DATETIME_ISNOT_WITHIN_WOLINEDATETIME = YES_NO_MESSAGE + 271;
                         // The date time you entered is not within the Work Order Line Date Time Period

        /// <summary>
        /// There is no row matches the search criteria
        /// </summary>
        public const int MESSAGE_WCDISPATCH_FOUND_NOROW = YES_NO_MESSAGE + 266;
                         // There is no row according to the search condition.Please Select another condition

        /// <summary>
        /// Please enter Completed Quantity
        /// </summary>
        public const int MESSAGE_WCDISPATCH_INPUT_COMPLETEDQTY = YES_NO_MESSAGE + 270;
                         // Please input the completed quantity

        /// <summary>
        /// Please enter End Date
        /// </summary>
        public const int MESSAGE_WCDISPATCH_INPUT_ENDDATE = YES_NO_MESSAGE + 269; // Please input end date

        /// <summary>
        /// Please enter Start Date
        /// </summary>
        public const int MESSAGE_WCDISPATCH_INPUT_STARTDATE = YES_NO_MESSAGE + 268; // Please input start date

        /// <summary>
        /// The value you have entered is different from the Completed Quantity in the previous step
        /// </summary>		
        public const int MESSAGE_WCDISPATCH_INQTY_ISNOT_THESAME_COMPLETEDQTY = YES_NO_MESSAGE + 272;
                         // The value you entered is different from the completed quantity in the previous step

        /// <summary>
        /// The previous operation step to this step is not yet finished 
        /// </summary>
        public const int MESSAGE_WCDISPATCH_PREVIOUS_OPERATION_ISNOTFINISHED = YES_NO_MESSAGE + 273;
                         // The previous operation step to this step is not yet finished

        /// <summary>
        /// Please select a Shift
        /// </summary>
        public const int MESSAGE_WCDISPATCH_SELECT_SHIFT = YES_NO_MESSAGE + 267; // Please select shift

        /// <summary>
        /// Please select a Work Center
        /// </summary>
        public const int MESSAGE_WCDISPATCH_SELECT_WORKCENTER = YES_NO_MESSAGE + 265; // Please select the work center

        /// <summary>
        /// Width must be less than 500 
        /// </summary>
        public const int MESSAGE_WIDTH_LESS_THAN_500 = YES_NO_MESSAGE + 189;

        /// <summary>
        /// Can not delete Work Order because it has some lines released
        /// </summary>
        public const int MESSAGE_WO_CANNOT_DELWO = YES_NO_MESSAGE + 204;
                         // Can not delete Work Order because it has some WO lines which released

        /// <summary>
        /// Can not delete Work Order line, that is released
        /// </summary>
        public const int MESSAGE_WO_CANNOT_DELWOLINE = YES_NO_MESSAGE + 208;
                         // Can not delete because WO Line was released

        /// <summary>
        /// Due Date must be greater than Trans Date
        /// </summary>
        public const int MESSAGE_WO_DUEDATE_TRANSDATE = YES_NO_MESSAGE + 203;
                         // Due date must be greater than Trans date

        /// <summary>
        /// Work Order has to have at least
        /// </summary>
        public const int MESSAGE_WO_HASWOLINE = YES_NO_MESSAGE + 209; // Work Order has to have at least 

        /// <summary>
        /// Quantity must be a number and greater than 0
        /// </summary>
        public const int MESSAGE_WO_ORDERQUANTITY = YES_NO_MESSAGE + 200;
                         // Quantity must be the number and greater than zero

        /// <summary>
        /// Please select Master Location before selecting Sale Order
        /// </summary>
        public const int MESSAGE_WO_SELECT_MASLOC = YES_NO_MESSAGE + 205;
                         // Please select Master Location before select Sale Order

        /// <summary>
        /// Please select a Part Number before selecting Sale Order
        /// </summary>
        public const int MESSAGE_WO_SELECT_PRODUCT = YES_NO_MESSAGE + 206;
                         // Please select Part Number before select Sale Order

        /// <summary>
        /// Please select a Sale Order before selecting Sale Order Line
        /// </summary>
        public const int MESSAGE_WO_SELECT_SALEORDER = YES_NO_MESSAGE + 207;
                         // Please select Sale Order before select Sale Order Line

        /// <summary>
        /// Due Date must be greater than Start Date
        /// </summary>
        public const int MESSAGE_WO_STARTDATE_DUEDATE = YES_NO_MESSAGE + 202;
                         // Due date must be greater than Start date

        /// <summary>
        /// Start Date must be greater than Trans Date
        /// </summary>
        public const int MESSAGE_WO_STARTDATE_TRANSDATE = YES_NO_MESSAGE + 201;
                         // Start date must be greater than Trans date

        /// <summary>
        /// Some of Work Order Lines you have selected was deleted, please re-load them
        /// </summary>
        public const int MESSAGE_WO_WAS_DELETED = YES_NO_MESSAGE + 430;
                         //Some of WOLines you have selected was deleted, you should re-load them

        /// <summary>
        /// Can not create Work order BOM has the product which's in WO Lines
        /// </summary>
        public const int MESSAGE_WOBOM_CANNOT_ITSELF = YES_NO_MESSAGE + 280;
                         // Can not create Work order BOM has the product which's in WO Lines

        /// <summary>
        /// Work Order BOM must have at least 1 line
        /// </summary>
        public const int MESSAGE_WOBOM_HASBOMLINE = YES_NO_MESSAGE + 279; // Work order BOM has to have at least line 

        /// <summary>
        /// Completed Quantity must be greater than 0
        /// </summary>
        public const int MESSAGE_WOCOMPLETION_COMPQTY_MUST_BE_GREATER_ZERO = YES_NO_MESSAGE + 248;
                         //Completed Quantity must be greater than Zero

        /// <summary>
        /// Completed Quantity must be 1
        /// </summary>
        public const int MESSAGE_WOCOMPLETION_COMPQTY_MUST_BE_ONE = YES_NO_MESSAGE + 246;
                         //Completed Quantity must be One

        /// <summary>
        /// Completed Quantity must be smaller than Lot Size
        /// </summary>
        public const int MESSAGE_WOCOMPLETION_COMPQTY_MUST_SMALLER_LOTSIZE = YES_NO_MESSAGE + 245;
                         //Completed Quantity must be smaller than Lotsize

        /// <summary>
        /// Serial number must be unique
        /// </summary>
        public const int MESSAGE_WOCOMPLETION_DUPLICATE_SERIAL = YES_NO_MESSAGE + 249; //Serial is duplicated

        /// <summary>
        /// Please enter Lot before continuing
        /// </summary>
        public const int MESSAGE_WOCOMPLETION_PLS_INPUT_LOT = YES_NO_MESSAGE + 247; //Please enter Lot before continue.

        /// <summary>
        /// Please enter Serial before continuing
        /// </summary>
        public const int MESSAGE_WOCOMPLETION_PLS_INPUT_SERIAL = YES_NO_MESSAGE + 297;
                         //Please enter Serial before continue.

        /// <summary>
        /// Completed Quantity must be a positive integer
        /// </summary>
        public const int MESSAGE_WODISPATCH_COMPLETED_QUANTITY = YES_NO_MESSAGE + 264;
                         // Completed quantity must be the number and greater than zero

        /// <summary>
        /// Do you want to select a Receiver
        /// </summary>
        public const int MESSAGE_WODISPATCH_CONFIRM_SELECT_RECEIVER = YES_NO_MESSAGE + 263;
                         //Do you want to select user for receiver

        /// <summary>
        /// Do you want to select a Transfer
        /// </summary>
        public const int MESSAGE_WODISPATCH_CONFIRM_SELECT_TRANSFER = YES_NO_MESSAGE + 262;
                         //Do you want to select user for transfer

        /// <summary>
        /// Completed Quantity must be equal to In Quantity 
        /// </summary>
        public const int MESSAGE_WODISPATCH_QUANTITY_INQUANTITY = YES_NO_MESSAGE + 342;
                         //Have the deffirent between Completed Quantity and In Quantity	

        /// <summary>
        /// Reversal Quantity is larger than Available Quantity
        /// </summary>
        public const int MESSAGE_WOREVERSAL_REVERSALQTY_LARGER_THAN_AVAILABLEQTY = YES_NO_MESSAGE + 404;
                         //Reversal Quantity is larger than Available Quantity

        /// <summary>
        /// Please add at least one Routing Step
        /// </summary>
        public const int MESSAGE_WOROUTING_ADD_ATLEAST_ONEROUTING = YES_NO_MESSAGE + 288;
                         // Please add at least one routing step

        /// <summary>
        /// Please input Routing Increment for each Routing Step
        /// </summary>
        public const int MESSAGE_WOROUTING_INPUT_INCREMENT = YES_NO_MESSAGE + 289;
                         // Please input the Routing increment for each routing step

        /// <summary>
        /// Please select Function for Routing Step
        /// </summary>
        public const int MESSAGE_WOROUTING_INPUT_ROUTINGFUNCTION = YES_NO_MESSAGE + 293;
                         // Please select the function for the wrouting step

        /// <summary>
        /// Please select Pacer for Routing Step
        /// </summary>
        public const int MESSAGE_WOROUTING_INPUT_ROUTINGPACER = YES_NO_MESSAGE + 294;
                         // Please select the pacer for the wrouting step

        /// <summary>
        /// Please enter Routing Step
        /// </summary>
        public const int MESSAGE_WOROUTING_INPUT_ROUTINGSTEP = YES_NO_MESSAGE + 290;
                         // Please input the step for the routing

        /// <summary>
        /// Please select Type for Routing Step
        /// </summary>
        public const int MESSAGE_WOROUTING_INPUT_ROUTINGTYPE = YES_NO_MESSAGE + 292;
                         // Please select the type for the routing step

        /// <summary>
        /// Please enter Work Center for Routing
        /// </summary>
        public const int MESSAGE_WOROUTING_INPUT_ROUTINGWORKCENTER = YES_NO_MESSAGE + 291;
                         // Please input the work center for the routing

        /// <summary>
        /// Please select a Work Order
        /// </summary>
        public const int MESSAGE_WOROUTING_INPUT_WORKORDER_FIRST = YES_NO_MESSAGE + 295;
                         // Please select the work order master first

        /// <summary>
        /// Please save Routing data before continuing
        /// </summary>
        public const int MESSAGE_WOROUTING_SAVE_ROUTING_FIRST = YES_NO_MESSAGE + 287;
                         // Please save the routing information first

        /// <summary>
        /// Please select a CCN before continuing
        /// </summary>
        public const int MESSAGE_WOROUTING_SELECT_CCN_FIRST = YES_NO_MESSAGE + 296; // Please select the CCN first

        /// <summary>
        /// Please select a Work Order Line to edit
        /// </summary>
        public const int MESSAGE_WOROUTING_SELECT_WOLINE = YES_NO_MESSAGE + 286;
                         // Please select the work order line to edit information

        /// <summary>
        /// Operation must be unique in a Work Center
        /// </summary>
        public const int MESSAGE_WOSCHE_DUPLICATE = YES_NO_MESSAGE + 237;
                         //Can not duplicate the operation for the Work center

        /// <summary>
        /// End Date must be between Start Date and End Date
        /// </summary>
        public const int MESSAGE_WOSCHE_END_END = YES_NO_MESSAGE + 234;
                         //The End date must be between StartDate and EndDate   

        /// <summary>
        /// End Date must be greater than Start Date
        /// </summary>
        public const int MESSAGE_WOSCHE_END_START = YES_NO_MESSAGE + 232; //The End date must be greater then Start date

        /// <summary>
        /// Please enter End Date
        /// </summary>
        public const int MESSAGE_WOSCHE_ENDDATE = YES_NO_MESSAGE + 230; //Please input the End date

        /// <summary>
        /// Please enter the Funtion Of Operation
        /// </summary>
        public const int MESSAGE_WOSCHE_FUNCTION = YES_NO_MESSAGE + 227; //Please input the Funtion of Operation 

        /// <summary>
        /// Schedule must have at least 1 row
        /// </summary>
        public const int MESSAGE_WOSCHE_HASDETAIL = YES_NO_MESSAGE + 224; //Schedule has to have at least row

        /// <summary>
        /// Please enter Operation
        /// </summary>
        public const int MESSAGE_WOSCHE_OPERATOIN = YES_NO_MESSAGE + 225; //Please enter Operation

        /// <summary>
        /// Please select a Work Order line before selecting Operation
        /// </summary>
        public const int MESSAGE_WOSCHE_SELECT_WOLINE = YES_NO_MESSAGE + 236;
                         //Please select Work order line before select Operation

        /// <summary>
        /// End Date must be greater than Start Date
        /// </summary>
        public const int MESSAGE_WOSCHE_START_END = YES_NO_MESSAGE + 231; //The Start date must be smaller than End date

        /// <summary>
        /// Start Date must be between Start Date and Due Date
        /// </summary>
        public const int MESSAGE_WOSCHE_START_START = YES_NO_MESSAGE + 233;
                         //The Start date must be between StartDate and EndDate  

        /// <summary>
        /// Please enter Start Date
        /// </summary>
        public const int MESSAGE_WOSCHE_STARTDATE = YES_NO_MESSAGE + 229; //Please input the Start date

        /// <summary>
        /// Please enter Type Of Operation
        /// </summary>
        public const int MESSAGE_WOSCHE_TYPE = YES_NO_MESSAGE + 226; //Please input the Type of Operation 

        /// <summary>
        /// Please enter Work Center Of Operation
        /// </summary>
        public const int MESSAGE_WOSCHE_WORKCENTER = YES_NO_MESSAGE + 228; //Please enter Work Center Of Operation

        /// <summary>
        /// You don not have right to delete, this transaction is created by @
        /// Exam: You don not have right to delete, this transaction is created by NamNT
        /// </summary>
        public const int MESSAGE_YOU_DONT_HAVE_RIGHT_TO_DELETE = YES_NO_MESSAGE + 473;

        /// <summary>
        /// You don not have right to edit, this transaction is created by @
        /// Exam: You don not have right to edit, this transaction is created by NamNT
        /// </summary>
        public const int MESSAGE_YOU_DONT_HAVE_RIGHT_TO_EDIT = YES_NO_MESSAGE + 472;

        /// <summary>
        /// You do not have right to access this function
        /// </summary>
        public const int MESSAGE_YOU_HAVE_NO_RIGHT_TO_VIEW = YES_NO_MESSAGE + 148;

        /// <summary>
        /// You must setup working time for this production line. 
        /// </summary>
        public const int MESSAGE_YOU_MUST_SETUP_WORKING_TIME = YES_NO_MESSAGE + 529;

        /// <summary>
        /// Scrap Quantity must be positive and equal or less than Available Quantity 
        /// </summary>
        public const int MSG_AOSCRAP_INVALID_SCRAP_QUANTITY = YES_NO_MESSAGE + 278;
                         //'Invalid quantity. Scrap quantity must be a positive number'

        /// <summary>
        /// Please enter or select Operation
        /// </summary>
        public const int MSG_AOSCRAP_PLS_INPUT_OPERATION = YES_NO_MESSAGE + 276; //'Please input or select operation'

        /// <summary>
        /// Please enter or select Scrap Reason
        /// </summary>
        public const int MSG_AOSCRAP_PLS_INPUT_REASON = YES_NO_MESSAGE + 277; //'Please input or select scrap reason'

        /// <summary>
        /// Please enter or select Work Order
        /// </summary>
        public const int MSG_AOSCRAP_PLS_INPUT_WO = YES_NO_MESSAGE + 274; //'Please input or select work order'

        /// <summary>
        /// Please enter or select Work Order Line
        /// </summary>
        public const int MSG_AOSCRAP_PLS_INPUT_WOITEM = YES_NO_MESSAGE + 275; //'Please input or select work order item'

        /// <summary>
        /// You cannot delete this transaction because there is one work order with is closed
        /// </summary>
        public const int MSG_LABORTIME_CANNOTDELETE = YES_NO_MESSAGE + 223;
                         // You cannot delete this transaction because there is one work order with is closed

        /// <summary>
        /// Completed Percent must be between 1 and 100
        /// </summary>
        public const int MSG_LABORTIME_COMPLETEPER = YES_NO_MESSAGE + 221;
                         // The complete percent must be within 1 and 100

        /// <summary>
        /// Please enter Labor Time Detail
        /// </summary>
        public const int MSG_LABORTIME_INPUTDETAIL = YES_NO_MESSAGE + 219; // Please input the labor time detail

        /// <summary>
        /// Please enter Total Hours
        /// </summary>
        public const int MSG_LABORTIME_INPUTHOUR = YES_NO_MESSAGE + 214; // Please input the total hours

        /// <summary>
        /// The value must be greater than 0
        /// </summary>
        public const int MSG_LABORTIME_QTY = YES_NO_MESSAGE + 220; // The value must be positive number

        /// <summary>
        /// Please select Hour Code
        /// </summary>
        public const int MSG_LABORTIME_SELECTHOURCODE = YES_NO_MESSAGE + 215; // Please select the hour code

        /// <summary>
        /// Please select the Labor Cost Center
        /// </summary>
        public const int MSG_LABORTIME_SELECTLABORCOSTCENTER = YES_NO_MESSAGE + 213;
                         // Please select the Labor Cost Center

        /// <summary>
        /// Please select Master Location
        /// </summary>
        public const int MSG_LABORTIME_SELECTMASLOC = YES_NO_MESSAGE + 210; // Please select the Master Location 

        /// <summary>
        /// Please select Operation Code
        /// </summary>
        public const int MSG_LABORTIME_SELECTOPERATION = YES_NO_MESSAGE + 217; // Please select the operation code

        /// <summary>
        /// Please select Setup/Run Code
        /// </summary>
        public const int MSG_LABORTIME_SELECTSETUPRUNCODE = YES_NO_MESSAGE + 216; // Please select the setup/run code

        /// <summary>
        /// Please select Work Order Line
        /// </summary>
        public const int MSG_LABORTIME_SELECTWODETAIL = YES_NO_MESSAGE + 212; // Please select the Work Order Line 

        /// <summary>
        /// Please select a Work Order
        /// </summary>
        public const int MSG_LABORTIME_SELECTWOMASTER = YES_NO_MESSAGE + 211; // Please select the Work Order Master 

        /// <summary>
        /// Issue Date must be in current period
        /// </summary>
        public const int MSG_LABORTIME_STARTDATE_HIGHER_THAN_POSTDATE = YES_NO_MESSAGE + 324;
                         // The startdate must be higher than post date	

        /// <summary>
        /// Start Date must be lower than the End Date
        /// </summary>
        public const int MSG_LABORTIME_STARTENDDATE = YES_NO_MESSAGE + 222;
                         // The Start Date must be lower than the end date

        /// <summary>
        /// Please enter Transaction Number
        /// </summary>
        public const int MSG_LABORTIME_TRANSNO = YES_NO_MESSAGE + 218; // Please input the Transaction number

        /// <summary>
        /// Please select Machine Name
        /// </summary>
        public const int MSG_MACHINETIME_SELECT_MACHINE = YES_NO_MESSAGE + 335; // Please select the machine name

        /// <summary>
        /// The Category you selected is not a leaf node, please select another category
        /// </summary>
        public const int MSG_PRODUCTINFO_CATEGORY_NOTALEAFNODE = YES_NO_MESSAGE + 53;
                         // The Category you selected is not a leaf node, please select another category

        /// <summary>
        /// Please select a row to view  
        /// </summary>
        public const int MSG_VIEWTABLE_SELECT_ROW = YES_NO_MESSAGE + 181; // Please select a row to view this record

        /// <summary>
        /// Commit Quantity exceeds the Available Auantity
        /// </summary>
        public const int MSG_WOISSUE_MATERIAL_COMMIT_QTY_DONOT_MEET_AVAILABLE_QTY = YES_NO_MESSAGE + 257;
                         //The commit quantity exceeds the available quantity in inventory

        /// <summary>
        /// The Commit Quantity does not meet the Lot Size
        /// </summary>
        public const int MSG_WOISSUE_MATERIAL_COMMIT_QTY_DONOT_MEET_LOTSIZE = YES_NO_MESSAGE + 256;
                         //The commit quantity doesn't meet the lot size quantity

        /// <summary>
        /// The Quantity for the Serial must be 1
        /// </summary>
        public const int MSG_WOISSUE_MATERIAL_COMMIT_QTY_DONOT_MEET_SERIAL_QTY = YES_NO_MESSAGE + 258;
                         //The quantity for the serial is only 1

        /// <summary>
        /// Commit Quantity must be greater than 0
        /// </summary>
        public const int MSG_WOISSUE_MATERIAL_COMMIT_QTY_GREATER_THAN_ZERO = YES_NO_MESSAGE + 251;
                         //The commit quantity must be greater than 0

        /// <summary>
        /// Please select at least one Uork Order Line to Issue Material
        /// </summary>
        public const int MSG_WOISSUE_MATERIAL_SELECT_ATLEAST_ONE_WOLINE = YES_NO_MESSAGE + 260;
                         //Please select the master location and CCN

        /// <summary>
        /// Please select a Component in this Work Order to Issue Material
        /// </summary>
        public const int MSG_WOISSUE_MATERIAL_SELECT_COMPONENT = YES_NO_MESSAGE + 250;
                         //Please select the component in this work order to issue material

        /// <summary>
        /// Please select a Component before continuing
        /// </summary>
        public const int MSG_WOISSUE_MATERIAL_SELECT_COMPONENT_FIRST = YES_NO_MESSAGE + 254;
                         //Please input the component first

        /// <summary>
        /// Please select a Location to issue material
        /// </summary>
        public const int MSG_WOISSUE_MATERIAL_SELECT_LOCATION = YES_NO_MESSAGE + 261;
                         //Please select the location to issue material

        /// <summary>
        /// Please select a Master Location before continuing
        /// </summary>
        public const int MSG_WOISSUE_MATERIAL_SELECT_LOCATION_FIRST = YES_NO_MESSAGE + 252;
                         //Please select the master location first

        /// <summary>
        /// Please enter Lot before continuing
        /// </summary>
        public const int MSG_WOISSUE_MATERIAL_SELECT_LOT_FIRST = YES_NO_MESSAGE + 255; //Please input the lot first

        /// <summary>
        /// Please select Master Location and CCN
        /// </summary>
        public const int MSG_WOISSUE_MATERIAL_SELECT_MASLOC_AND_CCN = YES_NO_MESSAGE + 259;
                         //Please select the master location and CCN

        /// <summary>
        /// Please select a Work Order Line before continuing
        /// </summary>
        public const int MSG_WOISSUE_MATERIAL_SELECT_WORKORDER_LINE_FIRST = YES_NO_MESSAGE + 253;
                         //Please select the work order line first

        /// <summary>
        /// New Password and Confirm Password must be identical
        /// </summary>
        public const int NEWPASSWORD_DIFFERENT_CONFIRMPASSWORD = YES_NO_MESSAGE + 24;
                         // New Password and Confirm Password must be identical

        /// <summary>	
        /// @ is not found in the system
        /// </summary>
        public const int NOT_FOUND = YES_NO_MESSAGE + 522;

        /// <summary>
        /// Your Password has changed successfully
        /// </summary>
        public const int PASSWORD_UPDATE_SUCCESSFUL = YES_NO_MESSAGE + 26; // Your Password has changed successfully

        /// <summary>
        /// This value must be positive
        /// </summary>
        public const int POSITIVE_NUMBER = YES_NO_MESSAGE + 7; // This value must be positive

        /// <summary>	
        /// The combination of Deparment, Production Line and Product Group is not correct. Please correct before continuing
        /// </summary>
        public const int PRODUCT_COST_ELEMENT_NOT_CORRECT = YES_NO_MESSAGE + 521;

        /// <summary>
        /// Some Products or Cost Elements not found
        /// </summary>
        public const int PRODUCT_COST_ELEMENT_NOT_FOUND = YES_NO_MESSAGE + 508;

        /// <summary>
        /// Do you want to save table config details
        /// </summary>
        public const int SAVE_BEFORE_OPEN_TABLE_CONFIG_DETAIL = YES_NO_MESSAGE + 8;
                         //Do you want to save table config details 

        /// <summary>
        /// User name or password is not correct
        /// </summary>
        public const int USERNAME_PASSWORD_INVALID = YES_NO_MESSAGE + 25; // User name or password is not correct

        /// <summary>
        /// Do you really want to delete
        /// </summary>
        public const int YES_NO_MESSAGE = 1000; //Do you really want to delete

        #region // HACK: DEL SonHT 2005-10-20

        /// <summary>
        /// Please enter data for grid
        /// </summary>
        public const int MSG_MULTIISSUE_MATERIAL_INPUTDETAIL = YES_NO_MESSAGE + 323;
                         // Please input the detail into the grid for this transaction.

        #endregion // END: DEL SonHT 2005-10-20

        #region // HACK: DEL SonHT 2005-10-14

        // public const int MESSAGE_QUESTION_BEFORE_CLOSE = YES_NO_MESSAGE + 46;   // 

        #endregion // END: DEL SonHT 2005-10-14

        #region // HACK: DEL SonHT 2005-10-20

        //		/// <summary>
        //		/// You have changed data but not yet saved to database, do you want to save all changed data
        //		/// </summary>
        //		public const int MESSAGE_BEFORE_CLOSE_FORM = YES_NO_MESSAGE + 5;  // You have changed data but not yet saved to database, do you want to save all changed data

        #endregion // END: DEL SonHT 2005-10-20

        #endregion

        #region ERROR_DB  = -1001;

        /// <summary>		
        /// The passed arguments is a null reference
        /// </summary>
        public const int ARGUMENTNULLEXCEPTION = ERROR_DB - 5; // The passed arguments is a null reference

        /// <summary>
        /// Can not delete because the data you delete was already used
        /// </summary>
        public const int CASCADE_DELETE_PREVENT = ERROR_DB - 8;
                         // Can not delete because the data you delete was already used

        /// <summary>
        /// The DataAdapter during the update operation if the number of rows affected equals zero
        /// </summary>
        public const int DBCONCURRENCYEXCEPTION = ERROR_DB - 6;
                         // The DataAdapter during the update operation if the number of rows affected equals zero

        /// <summary>
        /// You do not have permission to log file. Please try again
        /// </summary>
        public const int DBNULL_VIALATION = ERROR_DB - 14; //You do not have permission to log file. Please try again

        /// <summary>
        /// This field is duplicated
        /// </summary>
        public const int DUPLICATE_KEY = ERROR_DB - 7; //This field is duplicated

        /// <summary>
        /// Field Name or Table/View Name must be unique
        /// </summary>
        public const int DUPLICATE_TABLECONFIG = ERROR_DB - 15; //Field Name or Table/View Name must be unique

        /// <summary>
        /// Table must be unique
        /// </summary>
        public const int DUPLICATE_TABLECOPY = ERROR_DB - 17; // Table must be unique in the group

        /// <summary>
        /// Group Code and Group Name must be unique
        /// </summary>
        public const int DUPLICATE_TABLEGROUP = ERROR_DB - 16; //Group Code and Group Name must be unique

        /// <summary>
        /// This field is duplicated
        /// </summary>
        public const int DUPLICATEKEY = ERROR_DB - 1; //This field is duplicated

        /// <summary>
        /// The Database has an error
        /// </summary>	
        public const int ERROR_DB = -1001; //The Database has an error

        /// <summary>
        /// The exception is invalid
        /// </summary>
        public const int INVALIDEXCEPTION = ERROR_DB - 2; // The exception is invalid, try to restart application

        /// <summary>
        /// Unable to log exception. Please try again
        /// </summary>
        public const int LOG_EXCEPTION = ERROR_DB - 9; //Unable to log exception. Please try again

        /// <summary>
        /// Unable to log exception. Please try again
        /// </summary>	
        public const int LOG_IO_EXCEPTION = ERROR_DB - 11; //Unable to log exception. Please try again

        /// <summary>
        /// System did not support the file name. Please try another file name
        /// </summary>
        public const int LOG_NOT_SUPPORTED = ERROR_DB - 10;
                         //System did not support the file name. Please try another file name

        /// <summary>
        /// You do not have permission to log file. Please try again
        /// </summary>
        public const int LOG_SECURITY_EXCEPTION = ERROR_DB - 12;
                         //You do not have permission to log file. Please try again

        /// <summary>
        /// You do not have permission to log file. Please try again
        /// </summary>
        public const int LOG_UNAUTHORIZED = ERROR_DB - 13; // You do not have permission to log file. Please try again

        /// <summary>
        /// Unable to connect to Database
        /// </summary>
        public const int NOTCONNECTDB = ERROR_DB - 3; //Unable to connect to Database

        /// <summary>
        /// Unable to load form
        /// </summary>
        public const int TYPELOADEXCEPTION = ERROR_DB - 4; //Unable to load form

        #endregion

        #region OTHER_ERROR = -1100;

        /// <summary>
        /// Please enter data to required fields in red
        /// </summary>
        public const int MANDATORY_INVALID = OTHER_ERROR - 2; // Please enter data to required fields in red

        /// <summary>
        /// This feature has not been implemented yet
        /// </summary>
        public const int NOT_IMPLEMENT = OTHER_ERROR - 1; // This feature has not been implemented yet

        /// <summary>
        /// System has an error, please contact the administrator
        /// </summary>
        public const int OTHER_ERROR = -1100; // System has an error, please contact the administrator

        //Requested by TraDA
        public const int SQL_DATA_NOT_EXIST_KEYCODE = -2147217873;

        #endregion

        public const int SQL_ARITHMETRIC_OVERFLOW = 8115;
        public const int SQL_VIOLATION_CONSTRAINT = 2627;
        public const int SQLCASCADE_PREVENT_KEYCODE = 547;
        public const int SQLDBNULL_VIALATION_KEYCODE = 515;
        public const int SQLDUPLICATE_KEYCODE = 2601;
        public const int SQLDUPLICATE_UNIQUE_KEYCODE = 2627;
    }

    #region Table constants

    public sealed class AR_OtherReceivableDetailTable
    {
        public const string AMOUNT_FLD = "Amount";
        public const string ITEMDESCRIPTION_FLD = "ItemDescription";
        public const string OTHERRECEIVABLEDETAILID_FLD = "OtherReceivableDetailID";
        public const string OTHERRECEIVABLEMASTERID_FLD = "OtherReceivableMasterID";
        public const string PRICE_FLD = "Price";
        public const string QUANTITY_FLD = "Quantity";
        public const string TABLE_NAME = "AR_OtherReceivableDetail";
        public const string VATAMOUNT_FLD = "VATAmount";
        public const string VATPERCENT_FLD = "VATPercent";
    }

    public sealed class AR_OtherReceivableMasterTable
    {
        public const string BOOKSTATUS_FLD = "BookStatus";
        public const string CCNID_FLD = "CCNID";
        public const string CONTROLSTATUS_FLD = "ControlStatus";
        public const string CURRENCYID_FLD = "CurrencyID";
        public const string DESCRIPTION_FLD = "Description";
        public const string EMPLOYEEID_FLD = "EmployeeID";
        public const string EXCHANGERATE_FLD = "ExchangeRate";
        public const string LASTUPDATE_FLD = "LastUpdate";
        public const string ORIGININVOICEDATE_FLD = "OriginInvoiceDate";
        public const string ORIGININVOICENO_FLD = "OriginInvoiceNo";
        public const string OTHERRECEIVABLEMASTERID_FLD = "OtherReceivableMasterID";
        public const string PARTYID_FLD = "PartyID";
        public const string PAYMENTTERMID_FLD = "PaymentTermID";
        public const string POSTDATE_FLD = "PostDate";
        public const string TABLE_NAME = "AR_OtherReceivableMaster";
        public const string TOTALAMOUNT_FLD = "TotalAmount";
        public const string TRANSDATE_FLD = "TransDate";
        public const string TRANSNO_FLD = "TransNo";
        public const string USERID_FLD = "UserID";
    }

    public sealed class cst_ACAdjustmentDetailTable
    {
        public const string ACADJUSTMENTDETAIL_FLD = "ACAdjustmentDetail";
        public const string ACADJUSTMENTMASTERID_FLD = "ACAdjustmentMasterID";
        public const string COST_FLD = "Cost";
        public const string COSTELEMENTID_FLD = "CostElementID";
        public const string TABLE_NAME = "cst_ACAdjustmentDetail";
    }

    public sealed class cst_ACAdjustmentMasterTable
    {
        public const string ACADJUSTMENTMASTERID_FLD = "ACAdjustmentMasterID";
        public const string CCNID_FLD = "CCNID";
        public const string CODE_FLD = "Code";
        public const string NAME_FLD = "Name";
        public const string TABLE_NAME = "cst_ACAdjustmentMaster";
    }

    public sealed class CST_ActCostAllocationDetailTable
    {
        public const string ACTCOSTALLOCATIONDETAILID_FLD = "ActCostAllocationDetailID";
        public const string ACTCOSTALLOCATIONMASTERID_FLD = "ActCostAllocationMasterID";
        public const string ALLOCATIONAMOUNT_FLD = "AllocationAmount";
        public const string COSTELEMENTID_FLD = "CostElementID";
        public const string DEPARTMENTID_FLD = "DepartmentID";
        public const string LINE_FLD = "Line";
        public const string PRODUCTGROUPID_FLD = "ProductGroupID";
        public const string PRODUCTID_FLD = "ProductID";
        public const string PRODUCTIONLINEID_FLD = "ProductionLineID";
        public const string SALEORDERDETAILID_FLD = "SaleOrderDetailID";
        public const string SALEORDERMASTERID_FLD = "SaleOrderMasterID";
        public const string TABLE_NAME = "cst_ActCostAllocationDetail";
    }

    public sealed class CST_CollectionCostingSetupDetailTable
    {
        public const string CHARTOFACCOUNTID_FLD = "ChartOfAccountID";
        public const string COLLECTIONCOSTINGSETUPDETAILID_FLD = "CollectionCostingSetupDetailID";
        public const string COLLECTIONCOSTINGSETUPMASTERID_FLD = "CollectionCostingSetupMasterID";
        public const string CONTINGENTTYPE_FLD = "ContingentType";
        public const string COSTELEMENTID_FLD = "CostElementID";
        public const string TABLE_NAME = "CST_CollectionCostingSetupDetail";
    }

    public sealed class CST_CollectionCostingSetupMasterTable
    {
        public const string CCNID_FLD = "CCNID";
        public const string CODE_FLD = "Code";
        public const string COLLECTIONCOSTINGSETUPMASTERID_FLD = "CollectionCostingSetupMasterID";
        public const string DESCRIPTION_FLD = "Description";
        public const string FROMDATE_FLD = "FromDate";
        public const string LASTUPDATE_FLD = "LastUpdate";
        public const string TABLE_NAME = "CST_CollectionCostingSetupMaster";
        public const string TODATE_FLD = "ToDate";
        public const string USERID_FLD = "UserID";
    }

    public sealed class CST_ActCostAllocationMasterTable
    {
        public const string ACTCOSTALLOCATIONMASTERID_FLD = "ActCostAllocationMasterID";
        public const string CCNID_FLD = "CCNID";
        public const string COLLECTIONCOSTINGSETUPMASTERID_FLD = "CollectionCostingSetupMasterID";
        public const string CURRENCYID_FLD = "CurrencyID";
        public const string FROMDATE_FLD = "FromDate";
        public const string PERIODID_FLD = "PeriodID";
        public const string PERIODNAME_FLD = "PeriodName";
        public const string ROLLUPDATE_FLD = "RollupDate";
        public const string TABLE_NAME = "cst_ActCostAllocationMaster";
        public const string TODATE_FLD = "ToDate";
    }

    public sealed class CST_ActualCostHistoryTable
    {
        public const string ACTCOSTALLOCATIONMASTERID_FLD = "ActCostAllocationMasterID";
        public const string ACTUALCOST_FLD = "ActualCost";
        public const string ACTUALCOSTHISTORY_FLD = "ActualCostHistory";
        public const string ADJUSTAMOUNT_FLD = "AdjustAmount";
        public const string BEGINCOST_FLD = "BeginCost";
        public const string BEGINQUANTITY_FLD = "BeginQuantity";
        public const string COMBEGINCOST_FLD = "ComBeginCost";
        public const string COMPONENTDSAMOUNT_FLD = "ComponentDSAmount";
        public const string COMPONENTVALUE_FLD = "ComponentValue";
        public const string COSTELEMENTID_FLD = "CostElementID";
        public const string DS_OKAMOUNT_FLD = "DS_OKAmount";
        public const string DSAMOUNT_FLD = "DSAmount";
        public const string FREIGHTAMOUNT_FLD = "FreightAmount";
        public const string PRODUCTID_FLD = "ProductID";
        public const string QUANTITY_FLD = "Quantity";
        public const string RECOVERABLEAMOUNT_FLD = "RecoverableAmount";
        public const string RECYCLEAMOUNT_FLD = "RecycleAmount";
        public const string STDCOST_FLD = "StdCost";
        public const string TABLE_NAME = "CST_ActualCostHistory";
        public const string TRANSACTIONAMOUNT_FLD = "TransactionAmount";
        public const string WOCOMPLETIONQTY_FLD = "WOCompletionQty";
        public const string BEGIN_QUANTITY_FLD = "BeginQuantity";
        public const string DSOKAMOUNT_FLD = "DS_OKAmount";
    }
    public sealed class ITM_CostOperationTable
    {
        public const string TABLE_NAME = "ITM_CostOperation";
        public const string COSTOPERATIONID_FLD = "CostOperationID";
        public const string COMMATERIAL01_FLD = "ComMaterial01";
        public const string COMMATERIALOVERHEAD02_FLD = "ComMaterialOverHead02";
        public const string COMMACHINESETUP03_FLD = "ComMachineSetup03";
        public const string COMMACHINESETUPFIXED04_FLD = "ComMachineSetupFixed04";
        public const string COMMACHINESETUPVAR05_FLD = "ComMachineSetupVar05";
        public const string COMMACHINERUN06_FLD = "ComMachineRun06";
        public const string COMMACHINEFIXED07_FLD = "ComMachineFixed07";
        public const string COMMACHINEVARIABLE08_FLD = "ComMachineVariable08";
        public const string COMLABORSETUP09_FLD = "ComLaborSetup09";
        public const string COMLABORSETUPFIXED10_FLD = "ComLaborSetupFixed10";
        public const string COMLABORSETUPVARIABLE11_FLD = "ComLaborSetupVariable11";
        public const string COMLABORRUN12_FLD = "ComLaborRun12";
        public const string COMLABORFIXED13_FLD = "ComLaborFixed13";
        public const string COMLABORVARIABLE14_FLD = "ComLaborVariable14";
        public const string COMOUTSIDEPROC15_FLD = "ComOutsideProc15";
        public const string COMASSEMBLYSCRAP16_FLD = "ComAssemblyScrap16";
        public const string COMSHRINK17_FLD = "ComShrink17";
        public const string COMFREIGHT18_FLD = "ComFreight18";
        public const string COMUSERSTANDARD1_19_FLD = "ComUserStandard1_19";
        public const string COMUSERSTANDARD2_20_FLD = "ComUserStandard2_20";
        public const string COMTOTALAMOUNT21_FLD = "ComTotalAmount21";
        public const string VADDCOSTMATERIAL01_FLD = "VAddCostMaterial01";
        public const string VADDCOSTMATERIALOVERHEAD02_FLD = "VAddCostMaterialOverHead02";
        public const string VADDCOSTMACHINESETUP03_FLD = "VAddCostMachineSetup03";
        public const string VADDCOSTMACHINESETUPFIXED04_FLD = "VAddCostMachineSetupFixed04";
        public const string VADDCOSTMACHINESETUPVAR05_FLD = "VAddCostMachineSetupVar05";
        public const string VADDCOSTMACHINERUN06_FLD = "VAddCostMachineRun06";
        public const string VADDCOSTMACHINEFIXED07_FLD = "VAddCostMachineFixed07";
        public const string VADDCOSTMACHINEVARIABLE08_FLD = "VAddCostMachineVariable08";
        public const string VADDCOSTLABORSETUP09_FLD = "VAddCostLaborSetup09";
        public const string VADDCOSTLABORSETUPFIXED10_FLD = "VAddCostLaborSetupFixed10";
        public const string VADDCOSTLABORSETUPVARIABLE11_FLD = "VAddCostLaborSetupVariable11";
        public const string VADDCOSTLABORRUN12_FLD = "VAddCostLaborRun12";
        public const string VADDCOSTLABORFIXED13_FLD = "VAddCostLaborFixed13";
        public const string VADDCOSTLABORVARIABLE14_FLD = "VAddCostLaborVariable14";
        public const string VADDCOSTOUTSIDEPROC15_FLD = "VAddCostOutsideProc15";
        public const string VADDCOSTASSEMBLYSCRAP16_FLD = "VAddCostAssemblyScrap16";
        public const string VADDCOSTSHRINK17_FLD = "VAddCostShrink17";
        public const string VADDCOSTFREIGHT18_FLD = "VAddCostFreight18";
        public const string VADDCOSTUSERSTANDARD1_19_FLD = "VAddCostUserStandard1_19";
        public const string VADDCOSTUSERSTANDARD2_20_FLD = "VAddCostUserStandard2_20";
        public const string VADDCOSTTOTALAMOUNT21_FLD = "VAddCostTotalAmount21";
        public const string COMPONENTLABORRUNHOUR_FLD = "ComponentLaborRunHour";
        public const string COMPONENTLABORSETUPHOUR_FLD = "ComponentLaborSetupHour";
        public const string COMPONENTMACHINERUNHOUR_FLD = "ComponentMachineRunHour";
        public const string COMPONENTMACHINESETUPHOUR_FLD = "ComponentMachineSetupHour";
        public const string VALUEADDEDLABORRUNHOUR_FLD = "ValueAddedLaborRunHour";
        public const string VALUEADDEDLABORSETUPHOUR_FLD = "ValueAddedLaborSetupHour";
        public const string VALUEADDEDMACHINERUNHOUR_FLD = "ValueAddedMachineRunHour";
        public const string VALUEADDEDMACHINESETUPHOUR_FLD = "ValueAddedMachineSetupHour";
        public const string OPERATIONLABORRUNHOUR_FLD = "OperationLaborRunHour";
        public const string OPERATIONLABORSETUPHOUR_FLD = "OperationLaborSetupHour";
        public const string OPERATIONMACHINERUNHOUR_FLD = "OperationMachineRunHour";
        public const string OPERATIONMACHINESETUPHOUR_FLD = "OperationMachineSetupHour";
        public const string BCR_FLD = "BCR";
        public const string OPERCOSTMATERIAL01_FLD = "OperCostMaterial01";
        public const string OPERCOSTMATERIALOVERHEAD02_FLD = "OperCostMaterialOverHead02";
        public const string OPERCOSTMACHINESETUP03_FLD = "OperCostMachineSetup03";
        public const string OPERCOSTMACHINESETUPFIXED04_FLD = "OperCostMachineSetupFixed04";
        public const string OPERCOSTMACHINESETUPVAR05_FLD = "OperCostMachineSetupVar05";
        public const string OPERCOSTMACHINERUN06_FLD = "OperCostMachineRun06";
        public const string OPERCOSTMACHINEFIXED07_FLD = "OperCostMachineFixed07";
        public const string OPERCOSTMACHINEVARIABLE08_FLD = "OperCostMachineVariable08";
        public const string OPERCOSTLABORSETUP09_FLD = "OperCostLaborSetup09";
        public const string OPERCOSTLABORSETUPFIXED10_FLD = "OperCostLaborSetupFixed10";
        public const string OPERCOSTLABORSETUPVARIABLE11_FLD = "OperCostLaborSetupVariable11";
        public const string OPERCOSTLABORRUN12_FLD = "OperCostLaborRun12";
        public const string OPERCOSTLABORFIXED13_FLD = "OperCostLaborFixed13";
        public const string OPERCOSTLABORVARIABLE14_FLD = "OperCostLaborVariable14";
        public const string OPERCOSTOUTSIDEPROC15_FLD = "OperCostOutsideProc15";
        public const string OPERCOSTASSEMBLYSCRAP16_FLD = "OperCostAssemblyScrap16";
        public const string OPERCOSTSHRINK17_FLD = "OperCostShrink17";
        public const string OPERCOSTFREIGHT18_FLD = "OperCostFreight18";
        public const string OPERCOSTUSERSTANDARD1_19_FLD = "OperCostUserStandard1_19";
        public const string OPERCOSTUSERSTANDARD2_20_FLD = "OperCostUserStandard2_20";
        public const string OPERCOSTTOTALAMOUNT21_FLD = "OperCostTotalAmount21";
        public const string ROUTINGID_FLD = "RoutingID";
        public const string CCNID_FLD = "CCNID";
        public const string PRODUCTID_FLD = "ProductID";
    }
    public sealed class CST_AllocationResultTable
    {
        public const string ACTCOSTALLOCATIONMASTERID_FLD = "ActCostAllocationMasterID";
        public const string ALLOCATIONRESULTID_FLD = "AllocationResultID";
        public const string AMOUNT_FLD = "Amount";
        public const string COMPLETEDQUANTITY_FLD = "CompletedQuantity";
        public const string COSTELEMENTID_FLD = "CostElementID";
        public const string DEPARTMENTID_FLD = "DepartmentID";
        public const string PRODUCTGROUPID_FLD = "ProductGroupID";
        public const string PRODUCTID_FLD = "ProductID";
        public const string PRODUCTIONLINEID_FLD = "ProductionLineID";
        public const string RATE_FLD = "Rate";
        public const string TABLE_NAME = "cst_AllocationResult";
    }

    public sealed class CST_DSAndRecycleAllocationTable
    {
        public const string ACTCOSTALLOCATIONMASTERID_FLD = "ActCostAllocationMasterID";
        public const string ADJUSTAMOUNT_FLD = "AdjustAmount";
        public const string COSTELEMENTID_FLD = "CostElementID";
        public const string DSADALLOCACTIONID_FLD = "DSADAllocactionID";
        public const string DSAMOUNT_FLD = "DSAmount";
        public const string DSOHRATE_FLD = "DSOHRate";
        public const string DSRATE_FLD = "DSRate";
        public const string OH_ADJUSTAMOUNT_FLD = "OH_AdjustAmount";
        public const string OH_DSAMOUNT_FLD = "OH_DSAmount";
        public const string OH_RECYCLEAMOUNT_FLD = "OH_RecycleAmount";
        public const string PRODUCTID_FLD = "ProductID";
        public const string RECYCLEAMOUNT_FLD = "RecycleAmount";
        public const string RETURNGOODSRECEIPTQTY_FLD = "ReturnGoodsReceiptQty";
        public const string SHIPPINGQTY_FLD = "ShippingQty";
        public const string TABLE_NAME = "CST_DSAndRecycleAllocation";
    }

    public sealed class cst_FreightDetailTable
    {
        public const string ADJUSTMENTID_FLD = "AdjustmentID";
        public const string ALLOCATEDAMOUNT_FLD = "AllocatedAmount";
        public const string AMOUNT_FLD = "Amount";
        public const string BUYINGUMID_FLD = "BuyingUMID";
        public const string FREIGHTDETAILID_FLD = "FreightDetailID";
        public const string FREIGHTMASTERID_FLD = "FreightMasterID";
        public const string IMPORTTAX_FLD = "ImportTax";
        public const string INVOICEDETAILID_FLD = "InvoiceDetailID";
        public const string INVOICEMASTERID_FLD = "InvoiceMasterID";
        public const string PRODUCTID_FLD = "ProductID";
        public const string QUANTITY_FLD = "Quantity";
        public const string RETURNTOVENDORDETAILID_FLD = "ReturnToVendorDetailID";
        public const string TABLE_NAME = "cst_FreightDetail";
        public const string UNITPRICECIF_FLD = "UnitPriceCIF";
        public const string VATAMOUNT_FLD = "VATAmount";
        public const string IMPORTTAXPERCENT_FLD = "ImportTax";
    }
    public sealed class PRO_WOScheduleMasterTable
    {
        public const string TABLE_NAME = "PRO_WOScheduleMaster";
        public const string WOSCHEDULEMASTERID_FLD = "WOScheduleMasterID";
        public const string STARTDATE_FLD = "StartDate";
        public const string ENDDATE_FLD = "EndDate";
        public const string CCNID_FLD = "CCNID";
        public const string MASTERLOCATIONID_FLD = "MasterLocationID";
        public const string WORKORDERMASTERID_FLD = "WorkOrderMasterID";
        public const string WORKORDERDETAILID_FLD = "WorkOrderDetailID";
        public const string PRIORITY_FLD = "Priority";
        public const string PRODUCTID_FLD = "ProductID";
        public const string SCHEDULECODE_FLD = "ScheduleCode";
        public const string STOCKUMID_FLD = "StockUMID";
    }
    public sealed class PRO_DispatchDetailTable
    {
        public const string TABLE_NAME = "PRO_DispatchDetail";
        public const string DISPATCHDETAILID_FLD = "DispatchDetailID";
        public const string STARTDATETIME_FLD = "StartDateTime";
        public const string ENDDATETIME_FLD = "EndDateTime";
        public const string SHIFTID_FLD = "ShiftID";
        public const string COMPLETEDQUANTITY_FLD = "CompletedQuantity";
        public const string WOROUTINGID_FLD = "WORoutingID";
        public const string LABORSETUPID_FLD = "LaborSetupID";
        public const string MACHINESETUPID_FLD = "MachineSetupID";
        public const string LABORRUNID_FLD = "LaborRunID";
        public const string MACHINERUNID_FLD = "MachineRunID";
        public const string LABORLOADID_FLD = "LaborLoadID";
        public const string RECEIVERID_FLD = "ReceiverID";
        public const string TRANSFERID_FLD = "TransferID";
        public const string PRODUCTID_FLD = "ProductID";
        public const string DISPATCHMASTERID_FLD = "DispatchMasterID";
        public const string MACHINELOADID_FLD = "MachineLoadID";
        public const string WORKORDERDETAILID_FLD = "WorkOrderDetailID";
        public const string INQUANTITY_FLD = "InQuantity";
        public const string WORKCENTERID_FLD = "WorkCenterID";
    }
    public sealed class PRO_DCPResultTable
    {
        public const string TABLE_NAME = "PRO_DCPResult";
        public const string DCPRESULTID_FLD = "DCPResultID";
        public const string WOROUTINGID_FLD = "WORoutingID";
        public const string STARTDATETIME_FLD = "StartDateTime";
        public const string DUEDATETIME_FLD = "DueDateTime";
        public const string QUANTITY_FLD = "Quantity";
        public const string DCOPTIONMASTERID_FLD = "DCOptionMasterID";
        public const string CHECKPOINTID_FLD = "CheckPointID";
        public const string PRODUCTID_FLD = "ProductID";
        public const string CPOID_FLD = "CPOID";
        public const string WORKORDERDETAILID_FLD = "WorkOrderDetailID";
        public const string WORKCENTERID_FLD = "WorkCenterID";
    }
    public sealed class SO_PackListMasterTable
    {
        public const string TABLE_NAME = "SO_PackListMaster";
        public const string PACKLISTMASTERID_FLD = "PackListMasterID";
        public const string PACKLISTNO_FLD = "PackListNo";
        public const string PACKEDDATETIME_FLD = "PackedDateTime";
        public const string STATUS_FLD = "Status";
        public const string SHIPPED_FLD = "Shipped";
        public const string SHIPDATE_FLD = "ShipDate";
        public const string CARRIERID_FLD = "CarrierID";
        public const string WEIGHT_FLD = "Weight";
        public const string BOL_FLD = "BOL";
        public const string CONFIRMPACKDATE_FLD = "ConfirmPackDate";
        public const string EMPLOYEEID_FLD = "EmployeeID";
        public const string UNITOFMEASUREID_FLD = "UnitOfMeasureID";
    }
    public sealed class SO_PackListDetailTable
    {

        public const string TABLE_NAME = "SO_PackListDetail";
        public const string MASTERLOCATIONID_FLD = "MasterLocationID";
        public const string COMMITQUANTITY_FLD = "CommitQuantity";
        public const string PACKLISTDETAILID_FLD = "PackListDetailID";
        public const string SALEORDERDETAILID_FLD = "SaleOrderDetailId";
        public const string PACKLISTMASTERID_FLD = "PackListMasterID";
        public const string PRODUCTID_FLD = "ProductID";
        public const string BINID_FLD = "BinID";
        public const string LOCATIONID_FLD = "LocationID";
        public const string COMMITINVENTORYDETAILID_FLD = "CommitInventoryDetailID";
    }

    public sealed class cst_FreightMasterTable
    {
        public const string ACOBJECTID_FLD = "ACObjectID";
        public const string ACPURPOSEID_FLD = "ACPurposeID";
        public const string ADDITIONALTYPEID_FLD = "AdditionalTypeID";
        public const string ALLOCATIONAMOUNT_FLD = "AllocationAmount";
        public const string BOOKSTATUS_FLD = "BookStatus";
        public const string BYINVOICE_FLD = "ByInvoice";
        public const string BYPO_FLD = "ByPO";
        public const string CCNID_FLD = "CCNID";
        public const string CONTROLSTATUS_FLD = "ControlStatus";
        public const string CURRENCYID_FLD = "CurrencyID";
        public const string EXCHANGERATE_FLD = "ExchangeRate";
        public const string FREIGHTMASTERID_FLD = "FreightMasterID";
        public const string GRANDTOTAL_FLD = "GrandTotal";
        public const string INVOICEMASTERID_FLD = "InvoiceMasterID";
        public const string LASTUPDATE_FLD = "LastUpdate";
        public const string MAKERID_FLD = "MakerID";
        public const string NOTE_FLD = "Note";
        public const string ORIGINALINVOICEDATE_FLD = "OriginalInvoiceDate";
        public const string ORIGINALINVOICENO_FLD = "OriginalInvoiceNo";
        public const string PAYMENTAMOUNT_FLD = "PaymentAmount";
        public const string POSTDATE_FLD = "PostDate";
        public const string PURCHASEORDERRECEIPTID_FLD = "PurchaseOrderReceiptID";
        public const string RETURNTOVENDORMASTERID_FLD = "ReturnToVendorMasterID";
        public const string SUBTOTAL_FLD = "SubTotal";
        public const string TABLE_NAME = "cst_FreightMaster";
        public const string TOTALAMOUNT_FLD = "TotalAmount";
        public const string TOTALVAT_FLD = "TotalVAT";
        public const string TRANNO_FLD = "TranNo";
        public const string TRANSPORTERID_FLD = "TransporterID";
        public const string USERID_FLD = "UserID";
        public const string VATPERCENT_FLD = "VATPercent";
        public const string VENDORID_FLD = "VendorID";
        public const string RECEIPTMASTERID_FLD = "PurchaseOrderReceiptID";
    }
    public sealed class IV_LocToLocTransferMasterTable
    {
        public const string TABLE_NAME = "IV_LocToLocTransferMaster";
        public const string LOCTOLOCTRANSFERMASTERID_FLD = "LocToLocTransferMasterID";
        public const string POSTDATE_FLD = "PostDate";
        public const string COMMENT_FLD = "Comment";
        public const string CCNID_FLD = "CCNID";
        public const string DESBINID_FLD = "DesBinID";
        public const string SOURCEBINID_FLD = "SourceBinID";
        public const string SOURCELOCATIONID_FLD = "SourceLocationID";
        public const string DESLOCATIONID_FLD = "DesLocationID";
        public const string SOURCEMASLOCATIONID_FLD = "SourceMasLocationID";
        public const string DESMASLOCATIONID_FLD = "DesMasLocationID";
        public const string TRANSNO_FLD = "TransNo";
    }
    public sealed class IV_MaterialReceiptTable
    {
        public const string TABLE_NAME = "IV_MaterialReceipt";
        public const string MASTERLOCATIONID_FLD = "MasterLocationID";
        public const string MATERIALRECEIPTID_FLD = "MaterialReceiptID";
        public const string POSTDATE_FLD = "PostDate";
        public const string RECEIPTTYPE_FLD = "ReceiptType";
        public const string QUANTITY_FLD = "Quantity";
        public const string UNITCOST_FLD = "UnitCost";
        public const string TOTALAMOUNT_FLD = "TotalAmount";
        public const string LOT_FLD = "Lot";
        public const string SERIAL_FLD = "Serial";
        public const string CCNID_FLD = "CCNID";
        public const string BINID_FLD = "BinID";
        public const string LOCATIONID_FLD = "LocationID";
        public const string STOCKUMID_FLD = "StockUMID";
        public const string PRODUCTID_FLD = "ProductID";
        public const string TRANSNO_FLD = "TransNo";
        public const string DESCRIPTION_FLD = "Description";
        public const string QASTATUS_FLD = "QAStatus";
        public const string WORKORDERMASTERID_FLD = "WorkOrderMasterID";
        public const string WORKORDERDETAILID_FLD = "WorkOrderDetailID";
        public const string PURCHASEORDERMASTERID_FLD = "PurchaseOrderMasterID";
        public const string PURCHASEORDERDETAILID_FLD = "PurchaseOrderDetailID";
    }
    public sealed class IV_LotFIFOTable
    {
        public const string TABLE_NAME = "IV_LotFIFO";
        public const string LOTFIFOID_FLD = "LotFIFOID";
        public const string ACTUALCOST21_FLD = "ActualCost21";
        public const string COSTUPDATE_FLD = "CostUpdate";
        public const string LOT_FLD = "Lot";
        public const string LOTSEQUENCE_FLD = "LotSequence";
        public const string RECEIVEDATE_FLD = "ReceiveDate";
        public const string PRODUCTID_FLD = "ProductID";
        public const string CCNID_FLD = "CCNID";
    }
    public sealed class PRO_WorkOrderBomMasterTable
    {
        public const string TABLE_NAME = "PRO_WorkOrderBomMaster";
        public const string WORKORDERDETAILID_FLD = "WorkOrderDetailID";
        public const string WORKORDERBOMMASTERID_FLD = "WorkOrderBomMasterID";
        public const string CCNID_FLD = "CCNID";
        public const string MASTERLOCATIONID_FLD = "MasterLocationID";
        public const string WORKORDERMASTERID_FLD = "WorkOrderMasterID";
        public const string INCREMENT_FLD = "Increment";
        public const string DESCRIPTION_FLD = "Description";
    }
    public sealed class IV_MRBResultTable
    {
        public const string TABLE_NAME = "IV_MRBResult";
        public const string MASTERLOCATIONID_FLD = "MasterLocationID";
        public const string MRBRESULTID_FLD = "MRBResultID";
        public const string MRBNO_FLD = "MRBNo";
        public const string CCNID_FLD = "CCNID";
        public const string LOT_FLD = "Lot";
        public const string SERIAL_FLD = "Serial";
        public const string LOCATIONID_FLD = "LocationID";
        public const string BINID_FLD = "BinID";
        public const string POSTDATE_FLD = "PostDate";
        public const string COMMENT_FLD = "Comment";
        public const string USEASISQUANTITY_FLD = "UseAsIsQuantity";
        public const string RTVQUANTITY_FLD = "RTVQuantity";
        public const string RTVREPLACEQUANTITY_FLD = "RTVReplaceQuantity";
        public const string RTVREWORKQUANTITY_FLD = "RTVReworkQuantity";
        public const string REWORKPURQUANTITY_FLD = "ReworkPurQuantity";
        public const string REWORKMFGQUANTITY_FLD = "ReworkMfgQuantity";
        public const string SCRAPQUANTITY_FLD = "ScrapQuantity";
        public const string USEASISBINID_FLD = "UseAsIsBinID";
        public const string RTVBINID_FLD = "RTVBinID";
        public const string RTVREPLACEBINID_FLD = "RTVReplaceBinID";
        public const string RTVREWORKBINID_FLD = "RTVReworkBinID";
        public const string REWORKMFGBINID_FLD = "ReworkMfgBinID";
        public const string SCRAPBINID_FLD = "ScrapBinID";
        public const string REWORKPURBINID_FLD = "ReworkPurBinID";
        public const string USEASISLOCID_FLD = "UseAsIsLocID";
        public const string RTVLOCID_FLD = "RTVLocID";
        public const string RTVREPLACELOCID_FLD = "RTVReplaceLocID";
        public const string RTVREWORKLOCID_FLD = "RTVReworkLocID";
        public const string REWORKMFGLOCID_FLD = "ReworkMfgLocID";
        public const string SCRAPLOCID_FLD = "ScrapLocID";
        public const string REWORKPURLOCID_FLD = "ReworkPurLocID";
        public const string STOCKUMID_FLD = "StockUMID";
        public const string PRODUCTID_FLD = "ProductID";
    }
    public sealed class IV_MaterialIssueTable
    {
        public const string TABLE_NAME = "IV_MaterialIssue";
        public const string MASTERLOCATIONID_FLD = "MasterLocationID";
        public const string MATERIALISSUEMASTERID_FLD = "MaterialIssueMasterID";
        public const string POSTDATE_FLD = "PostDate";
        public const string BINID_FLD = "BinID";
        public const string LOCATIONID_FLD = "LocationID";
        public const string CCNID_FLD = "CCNID";
        public const string TRANSNO_FLD = "TransNo";
        public const string COMMENT_FLD = "Comment";
        public const string PRODUCTID_FLD = "ProductID";
        public const string STOCKUMID_FLD = "StockUMID";
        public const string LOT_FLD = "Lot";
        public const string SERIAL_FLD = "Serial";
        public const string QASTATUS_FLD = "QAStatus";
        public const string ISSUETYPE_FLD = "IssueType";
        public const string ISSUEQUANTITY_FLD = "IssueQuantity";
        public const string WORKORDERMASTERID_FLD = "WorkOrderMasterID";
        public const string WORKORDERDETAILID_FLD = "WorkOrderDetailID";
        public const string SALEORDERMASTERID_FLD = "SaleOrderMasterID";
        public const string SALEORDERDETAILID_FLD = "SaleOrderDetailID";
    }
    public sealed class TransactionType
    {
        public const string SALE_ORDER = "SaleOrder";
        public const string RETURN_GOODS_RECEIVE = "SOReturnGoodsReceive";
        public const string CANCEL_COMMITMENT = "SOCancelCommitment";
        public const string CONFIRM_SHIPMENT = "SOConfirmShipment";
        public const string PURCHASE_ORDER = "POPurchaseOrder";
        public const string PURCHASE_ORDER_RECEIPT = "POPurchaseOrderReceipts";
        public const string RETURN_TO_VENDOR = "POReturnToVendor";
        public const string MATERIAL_RECEIPT = "IVMaterialReceipt";
        public const string MATERIAL_ISSUE = "IVMaterialIssue";
        public const string MATERIAL_SCRAP = "IVMaterialScrap";
        public const string LOC_TO_LOC = "IVLocToLocTransfer";
        public const string INVENTORY_ADJUSTMENT = "IVInventoryAdjustment";
        public const string INSPECTION_RESULT = "IVInspectionResult";
        public const string MRB_RESULT = "IVMRBResult";
        public const string WORK_ORDER_COMPLETION = "PROWorkOrderCompletion";
        public const string WO_REVERSAL = "WOReversal";
        public const string PRO_ISSUE_MATERIAL = "PROIssueMaterial";
        public const string RECOVERABLE_MATERIAL = "RecoverableMaterial";
        public const string SHIPPING_ADJUSTMENT = "ShippingAdjustment";
    }
    public sealed class IV_MoveToInspectionDetailTable
    {
        public const string TABLE_NAME = "IV_MoveToInspectionDetail";
        public const string MOVETOINSPECTIONDETAILID_FLD = "MoveToInspectionDetailID";
        public const string INSPECTION_FLD = "Inspection";
        public const string MRP_FLD = "MRP";
        public const string TRANSFERQUANTITY_FLD = "TransferQuantity";
        public const string MOVETOINSPECTIONID_FLD = "MoveToInspectionID";
        public const string STOCKUMID_FLD = "StockUMID";
        public const string PRODUCTID_FLD = "ProductID";
    }
    public sealed class IV_LocToLocTransferDetailTable
    {
        public const string TABLE_NAME = "IV_LocToLocTransferDetail";
        public const string LOCTOLOCTRANSFERDETAILID_FLD = "LocToLocTransferDetailID";
        public const string TRANSFERQUANTITY_FLD = "TransferQuantity";
        public const string PRODUCTID_FLD = "ProductID";
        public const string LOT_FLD = "Lot";
        public const string SERIAL_FLD = "Serial";
        public const string QASTATUS_FLD = "QAStatus";
        public const string LOCTOLOCTRANSFERMASTERID_FLD = "LocToLocTransferMasterID";
        public const string STOCKUMID_FLD = "StockUMID";
    }
    public sealed class IV_ItemDetailTable
    {
        public const string TABLE_NAME = "IV_ItemDetail";
        public const string ITEMDETAILID_FLD = "ItemDetailID";
        public const string OHQUANTITY_FLD = "OHQuantity";
        public const string INSPSTATUS_FLD = "InspStatus";
        public const string MRBDISPLAY_FLD = "MRBDisplay";
        public const string CCNID_FLD = "CCNID";
        public const string PRODUCTID_FLD = "ProductID";
        public const string MASTERLOCATIONID_FLD = "MasterLocationID";
        public const string LOCATIONID_FLD = "LocationID";
        public const string BINID_FLD = "BinID";
        public const string LOT_FLD = "Lot";
        public const string SERIAL_FLD = "Serial";
    }
    public sealed class IV_INSResultTable
    {
        public const string TABLE_NAME = "IV_INSResult";
        public const string INSRESULTID_FLD = "InsResultID";
        public const string INSPECTNO_FLD = "InspectNo";
        public const string POSTDATE_FLD = "PostDate";
        public const string ACCEPTEDQUANTITY_FLD = "AcceptedQuantity";
        public const string REJECTEDQUANTITY_FLD = "RejectedQuantity";
        public const string SAMPLEQUANTITY_FLD = "SampleQuantity";
        public const string LOT_FLD = "Lot";
        public const string SERIAL_FLD = "Serial";
        public const string TRANTYPEID_FLD = "TranTypeID";
        public const string MASTERLOCATIONID_FLD = "MasterLocationID";
        public const string CCNID_FLD = "CCNID";
        public const string PRODUCTID_FLD = "ProductID";
        public const string INSPECTORID_FLD = "InspectorID";
        public const string ACCEPTEDLOCID_FLD = "AcceptedLocID";
        public const string REJECTEDLOCID_FLD = "RejectedLocID";
        public const string LOCATIONID_FLD = "LocationID";
        public const string ACCEPTEDBINID_FLD = "AcceptedBinID";
        public const string REJECTEDBINID_FLD = "RejectedBinID";
        public const string BINID_FLD = "BinID";
        public const string INSUMID_FLD = "InsUMID";
        public const string INSPECTIONSTATUS_FLD = "InspectionStatus";
    }
    public sealed class PRO_WOScheduleDetailTable
    {
        public const string TABLE_NAME = "PRO_WOScheduleDetail";
        public const string WOSCHEDULEMASTERID_FLD = "WOScheduleMasterID";
        public const string WOSCHEDULEDETAILID_FLD = "WOScheduleDetailID";
        public const string STARTDATETIME_FLD = "StartDateTime";
        public const string ENDDATETIME_FLD = "EndDateTime";
        public const string COMMENTS_FLD = "Comments";
        public const string SHIFTID_FLD = "ShiftID";
        public const string WOROUTINGID_FLD = "WORoutingID";
        public const string WORKCENTERID_FLD = "WorkCenterID";
        public const string TYPE_FLD = "Type";
        public const string FUNCTIONID_FLD = "FunctionID";
    }
    public sealed class PRO_DispatchMasterTable
    {
        public const string TABLE_NAME = "PRO_DispatchMaster";
        public const string DISPATCHMASTERID_FLD = "DispatchMasterID";
        public const string WORKORDERMASTERID_FLD = "WorkOrderMasterID";
        public const string WORKORDERDETAILID_FLD = "WorkOrderDetailID";
        public const string PRODUCTID_FLD = "ProductID";
        public const string CCNID_FLD = "CCNID";
        public const string MASTERLOCATIONID_FLD = "MasterLocationID";
    }
    public sealed class PRO_OutsideProcessingDetailTable
    {
        public const string TABLE_NAME = "PRO_OutsideProcessingDetail";
        public const string OUTSIDEPRCESSINGDETAILID_FLD = "OutsidePrcessingDetailID";
        public const string OUTSIDEPROCESSINGMASTERID_FLD = "OutsideProcessingMasterID";
        public const string WORKORDERDETAILID_FLD = "WorkOrderDetailID";
        public const string INVOICENO_FLD = "InvoiceNo";
        public const string INVOICEAMOUNT_FLD = "InvoiceAmount";
        public const string QUANTITY_FLD = "Quantity";
        public const string COMPLETEPERCENT_FLD = "CompletePercent";
        public const string WORKORDERMASTERID_FLD = "WorkOrderMasterID";
        public const string VENDORID_FLD = "VendorID";
        public const string PARTYLOCATIONID_FLD = "PartyLocationID";
        public const string WOROUTINGID_FLD = "WORoutingID";
        public const string COMPLETED_FLD = "Completed";
    }
    public sealed class PRO_WOReversalDetailTable
    {
        public const string TABLE_NAME = "PRO_WOReversalDetail";
        public const string WOREVERSALDETAILID_FLD = "WOReversalDetailID";
        public const string WOREVERSALMASTERID_FLD = "WOReversalMasterID";
        public const string QUANTITY_FLD = "Quantity";
        public const string PRODUCTID_FLD = "ProductID";
        public const string BINID_FLD = "BinID";
        public const string LOCATIONID_FLD = "LocationID";
        public const string ISSUEMATERIALDETAILID_FLD = "IssueMaterialDetailID";
        public const string LINE_FLD = "Line";

    }
    public sealed class PRO_CompletionStdCostTable
    {
        public const string TABLE_NAME = "PRO_CompletionStdCost";
        public const string COMPLETIONSTDCOSTID_FLD = "CompletionStdCostID";
        public const string POSTDATE_FLD = "PostDate";
        public const string TRANSTYPE_FLD = "TransType";
        public const string WORKORDERCOMPLETIONID_FLD = "WorkOrderCompletionID";
        public const string WORKORDERDETAILID_FLD = "WorkOrderDetailID";
        public const string WORKORDERMASTERID_FLD = "WorkOrderMasterID";
        public const string COSTMATERIAL01_FLD = "CostMaterial01";
        public const string COSTMATERIALOVERHEAD02_FLD = "CostMaterialOverHead02";
        public const string COSTMACHINESETUP03_FLD = "CostMachineSetup03";
        public const string COSTMACHINESETUPFIXED04_FLD = "CostMachineSetupFixed04";
        public const string COSTMACHINESETUPVAR05_FLD = "CostMachineSetupVar05";
        public const string COSTMACHINERUN06_FLD = "CostMachineRun06";
        public const string COSTMACHINEFIXED07_FLD = "CostMachineFixed07";
        public const string COSTMACHINEVARIABLE08_FLD = "CostMachineVariable08";
        public const string COSTLABORSETUP09_FLD = "CostLaborSetup09";
        public const string COSTLABORSETUPFIXED10_FLD = "CostLaborSetupFixed10";
        public const string COSTLABORSETUPVARIABLE11_FLD = "CostLaborSetupVariable11";
        public const string COSTLABORRUN12_FLD = "CostLaborRun12";
        public const string COSTLABORFIXED13_FLD = "CostLaborFixed13";
        public const string COSTLABORVARIABLE14_FLD = "CostLaborVariable14";
        public const string COSTOUTSIDEPROC15_FLD = "CostOutsideProc15";
        public const string COSTASSEMBLYSCRAP16_FLD = "CostAssemblyScrap16";
        public const string COSTSHRINK17_FLD = "CostShrink17";
        public const string COSTFREIGHT18_FLD = "CostFreight18";
        public const string COSTUSERSTANDARD1_19_FLD = "CostUserStandard1_19";
        public const string COSTUSERSTANDARD2_20_FLD = "CostUserStandard2_20";
        public const string COSTTOTALAMOUNT21_FLD = "CostTotalAmount21";
    }
    public sealed class v_ITMBOM_Product
    {
        public const string VIEW_NAME = "v_ITMBOM_Product";
        public const string HASBOM_FLD = "HasBom";
    }
    public sealed class ITM_CostDescriptionTable
    {
        public const string TABLE_NAME = "ITM_CostDescription";
        public const string COSTDESCRIPTIONID_FLD = "CostDescriptionID";
        public const string COSTDESC01_FLD = "CostDesc01";
        public const string COSTDESC02_FLD = "CostDesc02";
        public const string COSTDESC03_FLD = "CostDesc03";
        public const string COSTDESC04_FLD = "CostDesc04";
        public const string COSTDESC05_FLD = "CostDesc05";
        public const string COSTDESC06_FLD = "CostDesc06";
        public const string COSTDESC07_FLD = "CostDesc07";
        public const string COSTDESC08_FLD = "CostDesc08";
        public const string COSTDESC09_FLD = "CostDesc09";
        public const string COSTDESC10_FLD = "CostDesc10";
        public const string COSTDESC11_FLD = "CostDesc11";
        public const string COSTDESC12_FLD = "CostDesc12";
        public const string COSTDESC13_FLD = "CostDesc13";
        public const string COSTDESC14_FLD = "CostDesc14";
        public const string COSTDESC15_FLD = "CostDesc15";
        public const string COSTDESC16_FLD = "CostDesc16";
        public const string COSTDESC17_FLD = "CostDesc17";
        public const string COSTDESC18_FLD = "CostDesc18";
        public const string COSTDESC19_FLD = "CostDesc19";
        public const string COSTDESC20_FLD = "CostDesc20";
        public const string COSTDESC21_FLD = "CostDesc21";
        public const string MACHRUNHRSDESC_FLD = "MachRunHrsDesc";
        public const string MACHSETUPHRSDESC_FLD = "MachSetupHrsDesc";
        public const string LABORRUNHRSDESC_FLD = "LaborRunHrsDesc";
        public const string LABORSETUPHRSDESC_FLD = "LaborSetupHrsDesc";
        public const string CCNID_FLD = "CCNID";
    }
    public sealed class IV_LotItemTable
    {
        public const string TABLE_NAME = "IV_LotItem";
        public const string LOTITEMID_FLD = "LotItemID";
        public const string RECEIVETIMES_FLD = "ReceiveTimes";
        public const string RECEIVEQUANTITY_FLD = "ReceiveQuantity";
        public const string LOT_FLD = "Lot";
        public const string CCNID_FLD = "CCNID";
        public const string PRODUCTID_FLD = "ProductID";
        public const string RETURNQUANTITY_FLD = "ReturnQuantity";
    }
    public sealed class IV_MoveTicketTable
    {
        public const string TABLE_NAME = "IV_MoveTicket";
        public const string MOVETICKETID_FLD = "MoveTicketID";
        public const string TRANSACTIONNO_FLD = "TransactionNo";
        public const string LOT_FLD = "Lot";
        public const string INSPECTSTATUS_FLD = "InspectStatus";
        public const string MRBDISPLAY_FLD = "MRBDisplay";
        public const string QUANTITY_FLD = "Quantity";
        public const string CREATIONDATE_FLD = "CreationDate";
        public const string CCNID_FLD = "CCNID";
        public const string XFRCCNID_FLD = "XFRCCNID";
        public const string BINID_FLD = "BinID";
        public const string XFRBINID_FLD = "XFRBinID";
        public const string LOCATIONID_FLD = "LocationID";
        public const string XFRLOCATIONID_FLD = "XFRLocationID";
        public const string MASTERLOCATIONID_FLD = "MasterLocationID";
        public const string XFRMASTERLOCATIONID_FLD = "XFRMasterLocationID";
        public const string TRANTYPEID_FLD = "TranTypeID";
    }
    public sealed class IV_MoveToInspectionMasterTable
    {
        public const string TABLE_NAME = "IV_MoveToInspectionMaster";
        public const string MASTERLOCATIONID_FLD = "MasterLocationID";
        public const string MOVETOINSPECTIONMASTERID_FLD = "MoveToInspectionMasterID";
        public const string POSTDATE_FLD = "PostDate";
        public const string DESCRIPTION_FLD = "Description";
        public const string BINID_FLD = "BinID";
        public const string LOCATIONID_FLD = "LocationID";
    }
    public sealed class IV_MoveSerialTable
    {
        public const string TABLE_NAME = "IV_MoveSerial";
        public const string MOVESERIALID_FLD = "MoveSerialID";
        public const string SERIAL_FLD = "Serial";
        public const string CCNID_FLD = "CCNID";
        public const string MASTERLOCATIONID_FLD = "MasterLocationID";
        public const string MOVETICKETID_FLD = "MoveTicketID";
    }
    public sealed class CST_ProductGroupTable
    {
        public const string CODE_FLD = "Code";
        public const string DESCRIPTION_FLD = "Description";
        public const string PRODUCTGROUPID_FLD = "ProductGroupID";
        public const string PRODUCTIONLINEID_FLD = "ProductionLineID";
        public const string TABLE_NAME = "CST_ProductGroup";
    }

    public sealed class CST_RecoverMaterialDetailTable
    {
        public const string PARTYID_FLD = "PartyID";
        public const string PRODUCTID_FLD = "ProductID";
        public const string RECOVERMATERIALDETAILID_FLD = "RecoverMaterialDetailID";
        public const string RECOVERMATERIALMASTERID_FLD = "RecoverMaterialMasterID";
        public const string RECOVERQUANTITY_FLD = "RecoverQuantity";
        public const string TABLE_NAME = "CST_RecoverMaterialDetail";
        public const string TOBINID_FLD = "ToBinID";
        public const string TOLOCATIONID_FLD = "ToLocationID";
        public const string UNITOFMEASUREID_FLD = "UnitOfMeasureID";
    }

    public sealed class CST_RecoverMaterialMasterTable
    {
        public const string AVAILABLEQTY_FLD = "AvailableQty";
        public const string CCNID_FLD = "CCNID";
        public const string COMMENT_FLD = "Comment";
        public const string FROMBINID_FLD = "FromBinID";
        public const string FROMLOCATIONID_FLD = "FromLocationID";
        public const string POSTDATE_FLD = "PostDate";
        public const string PRODUCTID_FLD = "ProductID";
        public const string QUANTITY_FLD = "Quantity";
        public const string RECOVERMATERIALMASTERID_FLD = "RecoverMaterialMasterID";
        public const string TABLE_NAME = "CST_RecoverMaterialMaster";
        public const string TRANSNO_FLD = "TransNo";
        public const string USERNAME_FLD = "UserName";
    }

    public sealed class CST_STDItemCostTable
    {
        public const string COST_FLD = "Cost";
        public const string COSTELEMENTID_FLD = "CostElementID";
        public const string PRODUCTID_FLD = "ProductID";
        public const string ROLLUPDATE_FLD = "RollUpDate";
        public const string STDITEMCOSTID_FLD = "STDItemCostID";
        public const string TABLE_NAME = "CST_STDItemCost";
    }

    public sealed class DCP_BeginQuantityTable
    {
        public const string DCOPTIONMASTERID_FLD = "DCOptionMasterID";
        public const string DCPBEGINQUANTITYID_FLD = "DCPBeginQuantityID";
        public const string PRODUCTID_FLD = "ProductID";
        public const string QUANTITY_FLD = "Quantity";
        public const string TABLE_NAME = "DCP_BeginQuantity";
    }

    public sealed class DCP_OrderProduceTable
    {
        public const string COLUMNNAME_FLD = "ColumnName";
        public const string DCOPTIONMASTERID_FLD = "DCOptionMasterID";
        public const string ORDERNO_FLD = "OrderNo";
        public const string ORDERPLAN_FLD = "OrderPlan";
        public const string ORDERPRODUCEID_FLD = "OrderProduceID";
        public const string SHIFTID_FLD = "ShiftID";
        public const string TABLE_NAME = "DCP_OrderProduce";
        public const string WORKCENTERID_FLD = "WorkCenterID";
    }

    public sealed class dtpropertiesTable
    {
        public const string ID_FLD = "id";
        public const string LVALUE_FLD = "lvalue";
        public const string OBJECTID_FLD = "objectid";
        public const string PROPERTY_FLD = "property";
        public const string TABLE_NAME = "dtproperties";
        public const string UVALUE_FLD = "uvalue";
        public const string VALUE_FLD = "value";
        public const string VERSION_FLD = "version";
    }

    public sealed class enm_ACObjectTable
    {
        public const string ACOBJECTID_FLD = "ACObjectID";
        public const string CODE_FLD = "Code";
        public const string DESCRIPTION_FLD = "Description";
        public const string TABLE_NAME = "enm_ACObject";
    }

    public sealed class enm_ACPurposeTable
    {
        public const string ACPURPOSEID_FLD = "ACPurposeID";
        public const string CODE_FLD = "Code";
        public const string DESCRIPTION_FLD = "Description";
        public const string TABLE_NAME = "enm_ACPurpose";
    }

    public sealed class enm_AdditionalTypeTable
    {
        public const string CODE_FLD = "Code";
        public const string DESCRIPTION_FLD = "Description";
        public const string TABLE_NAME = "enm_AdditionalType";
        public const string TYPEID_FLD = "TypeID";
    }

    public sealed class enm_BINTypeTable
    {
        public const string BINTYPEID_FLD = "BINTypeID";
        public const string DESCRIPTION_FLD = "Description";
        public const string NAME_FLD = "Name";
        public const string TABLE_NAME = "enm_BINType";
    }

    public sealed class enm_CostMethodTable
    {
        public const string CODE_FLD = "Code";
        public const string COSTMETHODID_FLD = "CostMethodID";
        public const string DESCRIPTION_FLD = "Description";
        public const string TABLE_NAME = "enm_CostMethod";
    }

    public sealed class enm_GateTypeTable
    {
        public const string DESCRIPTION_FLD = "Description";
        public const string GATETYPE_FLD = "GateType";
        public const string GATETYPEID_FLD = "GateTypeID";
        public const string TABLE_NAME = "enm_GateType";
    }

    public sealed class enm_LocationTypeTable
    {
        public const string DESCRIPTION_FLD = "Description";
        public const string LOCATIONTYPEID_FLD = "LocationTypeID";
        public const string NAME_FLD = "Name";
        public const string TABLE_NAME = "enm_LocationType";
    }

    public sealed class enm_PartyTypeEnumTable
    {
        public const string DESCRIPTION_FLD = "Description";
        public const string NAME_FLD = "Name";
        public const string TABLE_NAME = "enm_PartyTypeEnum";
        public const string VALUE_FLD = "Value";
    }

    public sealed class enm_POReceiptTypeTable
    {
        public const string DESCRIPTION_FLD = "Description";
        public const string PORECEIPTTYPECODE_FLD = "POReceiptTypeCode";
        public const string PORECEIPTTYPEID_FLD = "POReceiptTypeID";
        public const string TABLE_NAME = "enm_POReceiptType";
    }

    public sealed class enm_PricingTypeTable
    {
        public const string CODE_FLD = "Code";
        public const string DESCRIPTION_FLD = "Description";
        public const string PRICINGTYPEID_FLD = "PricingTypeID";
        public const string TABLE_NAME = "enm_PricingType";
    }

    public sealed class enm_RequestTypeTable
    {
        public const string CODE_FLD = "Code";
        public const string DESCRIPTION_FLD = "Description";
        public const string NAME_FLD = "Name";
        public const string REQUESTTYPEID_FLD = "RequestTypeID";
        public const string TABLE_NAME = "enm_RequestType";
    }

    public sealed class enm_WOLineStatusTable
    {
        public const string CODE_FLD = "Code";
        public const string DESCRIPTION_FLD = "Description";
        public const string TABLE_NAME = "enm_WOLineStatus";
        public const string WOLINESTATUSID_FLD = "WOLineStatusID";
    }

    public sealed class FIN_AccountTypeTable
    {
        public const string ACCOUNTTYPEID_FLD = "AccountTypeID";
        public const string CODE_FLD = "Code";
        public const string NAME_FLD = "Name";
        public const string NAME_VN_FLD = "Name_VN";
        public const string TABLE_NAME = "FIN_AccountType";
    }

    public sealed class FIN_APPayableTable
    {
        public const string APPAYABLEID_FLD = "APPayableID";
        public const string CCNID_FLD = "CCNID";
        public const string CURRENCYID_FLD = "CurrencyID";
        public const string EMPLOYEEID_FLD = "EmployeeID";
        public const string LIABILITYAMOUNT_FLD = "LiabilityAmount";
        public const string PARTYID_FLD = "PartyID";
        public const string PAYMENTAMOUNT_FLD = "PaymentAmount";
        public const string RECEIVABLEAMOUNT_FLD = "ReceivableAmount";
        public const string RECEIVEDAMOUNT_FLD = "ReceivedAmount";
        public const string TABLE_NAME = "FIN_APPayable";
    }

    public sealed class FIN_APTaxDetailsTable
    {
        public const string AMOUNT_FLD = "Amount";
        public const string APTAXDETAILID_FLD = "APTaxDetailID";
        public const string APTAXMASTERID_FLD = "APTaxMasterID";
        public const string EXPENSEDETAILID_FLD = "ExpenseDetailID";
        public const string FREIGHTDETAILID_FLD = "FreightDetailID";
        public const string IMPTAX_FLD = "ImpTax";
        public const string IMPTAXAMOUNT_FLD = "ImpTaxAmount";
        public const string INVOICEDETAILID_FLD = "InvoiceDetailID";
        public const string PRODUCTID_FLD = "ProductID";
        public const string QUANTITY_FLD = "Quantity";
        public const string SPECIALTAX_FLD = "SpecialTax";
        public const string SPECIALTAXAMOUNT_FLD = "SpecialTaxAmount";
        public const string TABLE_NAME = "FIN_APTaxDetails";
        public const string UNITPRICE_FLD = "UnitPrice";
        public const string VAT_FLD = "VAT";
        public const string VATAMOUNT_FLD = "VATAmount";
    }

    public sealed class FIN_APTaxMasterTable
    {
        public const string APTAXMASTERID_FLD = "APTaxMasterID";
        public const string BOOKSTATUS_FLD = "BookStatus";
        public const string CCNID_FLD = "CCNID";
        public const string COMMENT_FLD = "Comment";
        public const string CONTROLSTATUS_FLD = "ControlStatus";
        public const string CURRENCYID_FLD = "CurrencyID";
        public const string EXCHANGERATE_FLD = "ExchangeRate";
        public const string EXPENSEMASTERID_FLD = "ExpenseMasterID";
        public const string FREIGHTMASTERID_FLD = "FreightMasterID";
        public const string INVOICEMASTERID_FLD = "InvoiceMasterID";
        public const string INVOICETYPE_FLD = "InvoiceType";
        public const string LASTUPDATE_FLD = "LastUpdate";
        public const string ORIGINALINVOICEDATE_FLD = "OriginalInvoiceDate";
        public const string ORIGINALINVOICENO_FLD = "OriginalInvoiceNo";
        public const string PARTYID_FLD = "PartyID";
        public const string POSTDATE_FLD = "Postdate";
        public const string TABLE_NAME = "FIN_APTaxMaster";
        public const string TRANSNO_FLD = "TransNo";
        public const string USERID_FLD = "UserID";
    }

    public sealed class FIN_ChartOfAccountTable
    {
        public const string ACCOUNTGL_FLD = "AccountGL";
        public const string ACCOUNTNAME_FLD = "AccountName";
        public const string ACCOUNTNAME_VN_FLD = "AccountName_VN";
        public const string ACCOUNTTYPEID_FLD = "AccountTypeID";
        public const string BALANCETYPE_FLD = "BalanceType";
        public const string BALANCEVALUE_FLD = "BalanceValue";
        public const string CHARTOFACCOUNTID_FLD = "ChartOfAccountID";
        public const string CHARTOFACCSTRUCTID_FLD = "ChartOfAccStructID";
        public const string CODE_FLD = "Code";
        public const string ISLEAF_FLD = "IsLeaf";
        public const string PARENTID_FLD = "ParentID";
        public const string SEGMENT1_FLD = "Segment1";
        public const string SEGMENT10_FLD = "Segment10";
        public const string SEGMENT2_FLD = "Segment2";
        public const string SEGMENT3_FLD = "Segment3";
        public const string SEGMENT4_FLD = "Segment4";
        public const string SEGMENT5_FLD = "Segment5";
        public const string SEGMENT6_FLD = "Segment6";
        public const string SEGMENT7_FLD = "Segment7";
        public const string SEGMENT8_FLD = "Segment8";
        public const string SEGMENT9_FLD = "Segment9";
        public const string TABLE_NAME = "FIN_ChartOfAccount";
    }

    public sealed class FIN_ChartOfAccStructTable
    {
        public const string CHARTOFACCSTRUCTID_FLD = "ChartOfAccStructID";
        public const string CODE_FLD = "Code";
        public const string NAME_FLD = "Name";
        public const string SEGMENT1_FLD = "Segment1";
        public const string SEGMENT10_FLD = "Segment10";
        public const string SEGMENT2_FLD = "Segment2";
        public const string SEGMENT3_FLD = "Segment3";
        public const string SEGMENT4_FLD = "Segment4";
        public const string SEGMENT5_FLD = "Segment5";
        public const string SEGMENT6_FLD = "Segment6";
        public const string SEGMENT7_FLD = "Segment7";
        public const string SEGMENT8_FLD = "Segment8";
        public const string SEGMENT9_FLD = "Segment9";
        public const string TABLE_NAME = "FIN_ChartOfAccStruct";
    }

    public sealed class FIN_CorrespondenceTable
    {
        public const string CHARTOFACCOUNTID_FLD = "ChartOfAccountID";
        public const string COR_ACCOUNTID_FLD = "Cor_AccountID";
        public const string CORRESPONDENCEID_FLD = "CorrespondenceID";
        public const string CREDIT_FLD = "Credit";
        public const string CREDITFOREIGN_FLD = "CreditForeign";
        public const string DEBIT_FLD = "Debit";
        public const string DEBITFOREIGN_FLD = "DebitForeign";
        public const string POSTDATE_FLD = "PostDate";
        public const string REFDETAILID_FLD = "RefDetailID";
        public const string REFMASTERID_FLD = "RefMasterID";
        public const string TABLE_NAME = "FIN_Correspondence";
        public const string TRANSTYPEID_FLD = "TransTypeID";
    }

    public sealed class FIN_DepositTable
    {
        public const string AMOUNT_FLD = "Amount";
        public const string APPLIEDAMOUNT_FLD = "AppliedAmount";
        public const string BANKID_FLD = "BankID";
        public const string BOOKSTATUS_FLD = "BookStatus";
        public const string CCNID_FLD = "CCNID";
        public const string CURRENCYID_FLD = "CurrencyID";
        public const string DEPOSITID_FLD = "DepositID";
        public const string DEPOSITTYPE_FLD = "DepositType";
        public const string DESCRIPTION_FLD = "Description";
        public const string EMPLOYEEID_FLD = "EmployeeID";
        public const string EXCHANGERATE_FLD = "ExchangeRate";
        public const string PARTYID_FLD = "PartyID";
        public const string POSTDATE_FLD = "PostDate";
        public const string PURCHASEORDERMASTERID_FLD = "PurchaseOrderMasterID";
        public const string TABLE_NAME = "FIN_Deposit";
        public const string TRANSDATE_FLD = "TransDate";
        public const string TRANSNO_FLD = "TransNo";
        public const string USERID_FLD = "UserID";
        public const string WORKORDERMASTERID_FLD = "WorkOrderMasterID";
    }

    public sealed class FIN_ExpenseDetailTable
    {
        public const string AMOUNT_FLD = "Amount";
        public const string CODE_FLD = "Code";
        public const string DEPARTMENTID_FLD = "DepartmentID";
        public const string DESCRIPTION_FLD = "Description";
        public const string EXPENSEDETAILID_FLD = "ExpenseDetailID";
        public const string EXPENSEMASTERID_FLD = "ExpenseMasterID";
        public const string LINE_FLD = "Line";
        public const string PRICE_FLD = "Price";
        public const string QUANTITY_FLD = "Quantity";
        public const string TABLE_NAME = "FIN_ExpenseDetail";
        public const string VAT_FLD = "VAT";
        public const string VATAMOUNT_FLD = "VATAmount";
    }

    public sealed class FIN_ExpenseMasterTable
    {
        public const string BOOKSTATUS_FLD = "BookStatus";
        public const string CCNID_FLD = "CCNID";
        public const string CURRENCYID_FLD = "CurrencyID";
        public const string DESCRIPTION_FLD = "Description";
        public const string EMPLOYEEID_FLD = "EmployeeID";
        public const string EXCHANGERATE_FLD = "ExchangeRate";
        public const string EXCHANGERATECC_FLD = "ExchangeRateCC";
        public const string EXPENSEMASTERID_FLD = "ExpenseMasterID";
        public const string EXPENSETYPE_FLD = "ExpenseType";
        public const string FIXASSETSTYPE_FLD = "FixAssetsType";
        public const string INVOICETYPE_FLD = "InvoiceType";
        public const string PARTYID_FLD = "PartyID";
        public const string PAYMENTAMOUNT_FLD = "PaymentAmount";
        public const string PAYMENTTERMID_FLD = "PaymentTermID";
        public const string POSTDATE_FLD = "PostDate";
        public const string SOURCETYPE_FLD = "SourceType";
        public const string TABLE_NAME = "FIN_ExpenseMaster";
        public const string TRANSDATE_FLD = "TransDate";
        public const string TRANSNO_FLD = "TransNo";
        public const string USERID_FLD = "UserID";
        public const string WORKORDERMASTERID_FLD = "WorkOrderMasterID";
    }

    public sealed class FIN_GLEntriesDetailTable
    {
        public const string CHARTOFACCOUNTID_FLD = "ChartOfAccountID";
        public const string COMMENT_FLD = "Comment";
        public const string CREDIT_FLD = "Credit";
        public const string CREDITCC_FLD = "CreditCC";
        public const string CREDITFOREIGN_FLD = "CreditForeign";
        public const string DEBIT_FLD = "Debit";
        public const string DEBITCC_FLD = "DebitCC";
        public const string DEBITFOREIGN_FLD = "DebitForeign";
        public const string DEPARTMENTID_FLD = "DepartmentID";
        public const string GLENTRIESDETAILID_FLD = "GLEntriesDetailID";
        public const string GLENTRIESMASTERID_FLD = "GLEntriesMasterID";
        public const string GROUPS_FLD = "Groups";
        public const string PRODUCTGROUPID_FLD = "ProductGroupID";
        public const string PRODUCTID_FLD = "ProductID";
        public const string PRODUCTIONLINEID_FLD = "ProductionLineID";
        public const string REFDETAILID_FLD = "RefDetailID";
        public const string REFMASTERID_FLD = "RefMasterID";
        public const string SALEORDERDETAILID_FLD = "SaleOrderDetailID";
        public const string SALEORDERMASTERID_FLD = "SaleOrderMasterID";
        public const string TABLE_NAME = "FIN_GLEntriesDetail";
    }

    public sealed class FIN_GLEntriesMasterTable
    {
        public const string BOOKSTATUS_FLD = "BookStatus";
        public const string CCNID_FLD = "CCNID";
        public const string CURRENCYID_FLD = "CurrencyID";
        public const string DESCRIPTION_FLD = "Description";
        public const string EMPLOYEEID_FLD = "EmployeeID";
        public const string EXCHANGERATE_FLD = "ExchangeRate";
        public const string EXCHANGERATECC_FLD = "ExchangeRateCC";
        public const string GLENTRIESMASTERID_FLD = "GLEntriesMasterID";
        public const string LASTUPDATE_FLD = "LastUpdate";
        public const string ORIGINDATE_FLD = "OriginDate";
        public const string ORIGININVOICENO_FLD = "OriginInvoiceNo";
        public const string PARTYID_FLD = "PartyID";
        public const string POSTDATE_FLD = "PostDate";
        public const string REFDETAILID_FLD = "RefDetailID";
        public const string REFMASTERID_FLD = "RefMasterID";
        public const string TABLE_NAME = "FIN_GLEntriesMaster";
        public const string TRANSDATE_FLD = "TransDate";
        public const string TRANSNO_FLD = "TransNo";
        public const string TRANSTYPEID_FLD = "TransTypeID";
        public const string USERID_FLD = "UserID";
        public const string USERNAME_FLD = "Username";
        public const string WORKORDERDETAILID_FLD = "WorkOrderDetailID";
        public const string WORKORDERMASTERID_FLD = "WorkOrderMasterID";
    }

    public sealed class FIN_OpenningBalanceTable
    {
        public const string CCNID_FLD = "CCNID";
        public const string CHARTOFACCOUNTID_FLD = "ChartOfAccountID";
        public const string CREDIT_FLD = "Credit";
        public const string CREDITFOREIGN_FLD = "CreditForeign";
        public const string CREDITFOREIGNYEAR_FLD = "CreditForeignYear";
        public const string CREDITYEAR_FLD = "CreditYear";
        public const string DEBIT_FLD = "Debit";
        public const string DEBITFOREIGN_FLD = "DebitForeign";
        public const string DEBITFOREIGNYEAR_FLD = "DebitForeignYear";
        public const string DEBITYEAR_FLD = "DebitYear";
        public const string EFFECTDATE_FLD = "EffectDate";
        public const string OPENNINGBALANCEID_FLD = "OpenningBalanceID";
        public const string TABLE_NAME = "FIN_OpenningBalance";
    }

    public sealed class FIN_PaymentDetailTable
    {
        public const string APPLYAMOUNT_FLD = "ApplyAmount";
        public const string BALANCEAMOUNT_FLD = "BalanceAmount";
        public const string BANKID_FLD = "BankID";
        public const string CONTENTS_FLD = "Contents";
        public const string DEPOSITAMOUNT_FLD = "DepositAmount";
        public const string DEPOSITID_FLD = "DepositID";
        public const string PAYMENTDETAILID_FLD = "PaymentDetailID";
        public const string PAYMENTMASTERID_FLD = "PaymentMasterID";
        public const string REMAINDEPOSITAMOUNT_FLD = "RemainDepositAmount";
        public const string TABLE_NAME = "FIN_PaymentDetail";
    }

    public sealed class FIN_PaymentForPartnerDetailTable
    {
        public const string EXPENSEMASTERID_FLD = "ExpenseMasterID";
        public const string FREIGHTMASTERID_FLD = "FreightMasterID";
        public const string INVOICEAMOUNT_FLD = "InvoiceAmount";
        public const string INVOICEMASTERID_FLD = "InvoiceMasterID";
        public const string INVOICETYPE_FLD = "InvoiceType";
        public const string PAYMENTAMOUNT_FLD = "PaymentAmount";
        public const string PAYMENTFORPARTNERDETAILID_FLD = "PaymentForPartnerDetailID";
        public const string PAYMENTFORPARTNERMASTERID_FLD = "PaymentForPartnerMasterID";
        public const string REMAINAMOUNT_FLD = "RemainAmount";
        public const string TABLE_NAME = "FIN_PaymentForPartnerDetail";
    }

    public sealed class FIN_PaymentForPartnerMasterTable
    {
        public const string BANKID_FLD = "BankID";
        public const string BOOKSTATUS_FLD = "BookStatus";
        public const string CCNID_FLD = "CCNID";
        public const string CONTROLSTATUS_FLD = "ControlStatus";
        public const string CURRENCYID_FLD = "CurrencyID";
        public const string EXCHANGERATE_FLD = "ExchangeRate";
        public const string LASTUPDATE_FLD = "LastUpdate";
        public const string LIABILITYAMOUNT_FLD = "LiabilityAmount";
        public const string PARTYID_FLD = "PartyID";
        public const string PAYMENTAMOUNT_FLD = "PaymentAmount";
        public const string PAYMENTFORPARTNERMASTERID_FLD = "PaymentForPartnerMasterID";
        public const string PAYMENTTYPE_FLD = "PaymentType";
        public const string POSTDATE_FLD = "PostDate";
        public const string REMAINAMOUNT_FLD = "RemainAmount";
        public const string TABLE_NAME = "FIN_PaymentForPartnerMaster";
        public const string TRANSNO_FLD = "TransNo";
        public const string USERID_FLD = "UserID";
    }

    public sealed class FIN_PaymentMasterTable
    {
        public const string ADDITIONALAMOUNT_FLD = "AdditionalAmount";
        public const string ADDITIONALREMAINAMOUNT_FLD = "AdditionalRemainAmount";
        public const string BOOKSTATUS_FLD = "BookStatus";
        public const string CCNID_FLD = "CCNID";
        public const string CONTROLSTATUS_FLD = "ControlStatus";
        public const string CURRENCYID_FLD = "CurrencyID";
        public const string EMPLOYEEID_FLD = "EmployeeID";
        public const string EXCHANGERATE_FLD = "ExchangeRate";
        public const string EXPENSEMASTERID_FLD = "ExpenseMasterID";
        public const string FREIGHTMASTERID_FLD = "FreightMasterID";
        public const string INVOICEAMOUNT_FLD = "InvoiceAmount";
        public const string INVOICEMASTERID_FLD = "InvoiceMasterID";
        public const string INVOICEPAYMENTTYPE_FLD = "InvoicePaymentType";
        public const string PARTYID_FLD = "PartyID";
        public const string PAYMENTEXCHANGERATE_FLD = "PaymentExchangeRate";
        public const string PAYMENTMASTERID_FLD = "PaymentMasterID";
        public const string PAYMENTTYPE_FLD = "PaymentType";
        public const string POSTDATE_FLD = "PostDate";
        public const string REMAINAMOUNT_FLD = "RemainAmount";
        public const string TABLE_NAME = "FIN_PaymentMaster";
        public const string TRANSDATE_FLD = "TransDate";
        public const string TRANSNO_FLD = "TransNo";
        public const string USERID_FLD = "UserID";
    }

    public sealed class FIN_PostSlipTable
    {
        public const string ACCOUNTCREDIT_FLD = "AccountCredit";
        public const string ACCOUNTDEBIT_FLD = "AccountDebit";
        public const string CCNID_FLD = "CCNID";
        public const string ISSUEPURPOSEID_FLD = "IssuePurposeID";
        public const string POSTSLIPID_FLD = "PostSlipID";
        public const string TABLE_NAME = "FIN_PostSlip";
        public const string TRANTYPEID_FLD = "TranTypeID";
    }

    public sealed class HR_CandidateTable
    {
        public const string ADDRESS1_FLD = "Address1";
        public const string ADDRESS2_FLD = "Address2";
        public const string CANDIDATEID_FLD = "CandidateID";
        public const string CODE_FLD = "Code";
        public const string DOB_FLD = "DOB";
        public const string EDUCATIONLEVEL_FLD = "EducationLevel";
        public const string EMAIL_FLD = "Email";
        public const string EXPYEARS_FLD = "ExpYears";
        public const string FAX_FLD = "Fax";
        public const string FULLNAME_FLD = "Fullname";
        public const string GUARANTOR_FLD = "Guarantor";
        public const string HEALTHSTATUS_FLD = "HealthStatus";
        public const string HEIGHT_FLD = "Height";
        public const string PRIORITYSEQ_FLD = "PrioritySeq";
        public const string PROFESSIONALDEGREE_FLD = "ProfessionalDegree";
        public const string PROFILEDATE_FLD = "ProfileDate";
        public const string PROFILESTATUS_FLD = "ProfileStatus";
        public const string RECRUITSTATUS_FLD = "RecruitStatus";
        public const string SEX_FLD = "Sex";
        public const string SPECIALITY_FLD = "Speciality";
        public const string TABLE_NAME = "HR_Candidate";
        public const string TEL_FLD = "Tel";
        public const string WEIGHT_FLD = "Weight";
        public const string WORKERRANK_FLD = "WorkerRank";
    }

    public sealed class HR_EmployeeProfileTable
    {
        public const string ACTIVE_FLD = "Active";
        public const string ALLOWANCE_FLD = "Allowance";
        public const string CANDIDATEID_FLD = "CandidateID";
        public const string CEL_FLD = "Cel";
        public const string CONTACTADDRESS_FLD = "ContactAddress";
        public const string COUNTRYID_FLD = "CountryID";
        public const string DOB_FLD = "DOB";
        public const string EDUCATIONLEVEL_FLD = "EducationLevel";
        public const string EMAIL_FLD = "Email";
        public const string EMPLOYEEID_FLD = "EmployeeID";
        public const string ETHNIC_FLD = "Ethnic";
        public const string FOREIGNLANGLEVEL_FLD = "ForeignLangLevel";
        public const string HEALTHLEVEL_FLD = "HealthLevel";
        public const string IDCARD_FLD = "IDCard";
        public const string IDDATEOFISSUE_FLD = "IDDateOfIssue";
        public const string IDPLACEOFISSUE_FLD = "IDPlaceOfIssue";
        public const string INFORMATICLEVEL_FLD = "InformaticLevel";
        public const string JOBTITLE_FLD = "JobTitle";
        public const string JOINDATE_FLD = "JoinDate";
        public const string LEAVEDATE_FLD = "LeaveDate";
        public const string LEAVEREASON_FLD = "LeaveReason";
        public const string LIVEADDRESS_FLD = "LiveAddress";
        public const string MARRIEDSTATUS_FLD = "MarriedStatus";
        public const string NATIVECOUNTRY_FLD = "NativeCountry";
        public const string NICKNAME_FLD = "Nickname";
        public const string PICTURE_FLD = "Picture";
        public const string POLICYREGULATION_FLD = "PolicyRegulation";
        public const string QUALIFICATIONS_FLD = "Qualifications";
        public const string RECRUITMENTSCHEMEMASTERID_FLD = "RecruitmentSchemeMasterID";
        public const string RECRUITTYPE_FLD = "RecruitType";
        public const string RELIGION_FLD = "Religion";
        public const string SEX_FLD = "Sex";
        public const string SPECIALITY_FLD = "Speciality";
        public const string TABLE_NAME = "HR_EmployeeProfile";
        public const string TEAM_FLD = "Team";
        public const string TEL_FLD = "Tel";
    }

    public sealed class HR_EmploymentHistoryTable
    {
        public const string COMPANYNAME_FLD = "CompanyName";
        public const string DESCRIPTION_FLD = "Description";
        public const string EMPLOYEEID_FLD = "EmployeeID";
        public const string EMPLOYMENTHISTORYID_FLD = "EmploymentHistoryID";
        public const string FROMDATE_FLD = "FromDate";
        public const string JOBTITLE_FLD = "JobTitle";
        public const string TABLE_NAME = "HR_EmploymentHistory";
        public const string TODATE_FLD = "ToDate";
    }

    public sealed class HR_InsuranceTable
    {
        public const string EMPLOYEEID_FLD = "EmployeeID";
        public const string FROMDATE_FLD = "FromDate";
        public const string INSURANCEID_FLD = "InsuranceID";
        public const string PAYPERCENT_FLD = "PayPercent";
        public const string SALARYINSURANCE_FLD = "SalaryInsurance";
        public const string TABLE_NAME = "HR_Insurance";
        public const string TODATE_FLD = "ToDate";
    }

    public sealed class HR_InternalReshuffleTable
    {
        public const string DEPARTMENTID_FLD = "DepartmentID";
        public const string EMPLOYEEID_FLD = "EmployeeID";
        public const string FROMDATE_FLD = "FromDate";
        public const string INTERNALRESHUFFLEID_FLD = "InternalReshuffleID";
        public const string JOBTITLE_FLD = "JobTitle";
        public const string TABLE_NAME = "HR_InternalReshuffle";
        public const string TEAM_FLD = "Team";
        public const string TODATE_FLD = "ToDate";
    }

    public sealed class HR_LaborContractTable
    {
        public const string CODE_FLD = "Code";
        public const string EMPLOYEEID_FLD = "EmployeeID";
        public const string ENDDATE_FLD = "EndDate";
        public const string LABORCONTRACTID_FLD = "LaborContractID";
        public const string LABORCONTRACTTYPEID_FLD = "LaborContractTypeID";
        public const string SIGNDATE_FLD = "SignDate";
        public const string STATUS_FLD = "Status";
        public const string TABLE_NAME = "HR_LaborContract";
    }

    public sealed class HR_LaborContractTypeTable
    {
        public const string CODE_FLD = "Code";
        public const string DESCRIPTION_FLD = "Description";
        public const string LABORCONTRACTTYPEID_FLD = "LaborContractTypeID";
        public const string TABLE_NAME = "HR_LaborContractType";
    }

    public sealed class HR_RecruitmentResultTable
    {
        public const string CANDIDATEID_FLD = "CandidateID";
        public const string EVALUATORID_FLD = "EvaluatorID";
        public const string FACTOR_FLD = "Factor";
        public const string RECRUITMENTRESULTID_FLD = "RecruitmentResultID";
        public const string RECRUITMENTSCHEMEMASTERID_FLD = "RecruitmentSchemeMasterID";
        public const string SCORE_FLD = "Score";
        public const string TABLE_NAME = "HR_RecruitmentResult";
        public const string TARGET_FLD = "Target";
    }

    public sealed class HR_RecruitmentSchemeDetailTable
    {
        public const string EVALUATORID_FLD = "EvaluatorID";
        public const string FACTOR_FLD = "Factor";
        public const string RECRUITMENTSCHEMEDETAILID_FLD = "RecruitmentSchemeDetailID";
        public const string RECRUITMENTSCHEMEMASTERID_FLD = "RecruitmentSchemeMasterID";
        public const string SCORE_FLD = "Score";
        public const string TABLE_NAME = "HR_RecruitmentSchemeDetail";
        public const string TARGET_FLD = "Target";
    }

    public sealed class HR_RecruitmentSchemeMasterTable
    {
        public const string CCNID_FLD = "CCNID";
        public const string DEPARTMENTID_FLD = "DepartmentID";
        public const string DESCRIPTION_FLD = "Description";
        public const string EMPLOYEEID_FLD = "EmployeeID";
        public const string FROMDATE_FLD = "FromDate";
        public const string POSITION_FLD = "Position";
        public const string RECRUITMENTSCHEMEMASTERID_FLD = "RecruitmentSchemeMasterID";
        public const string TABLE_NAME = "HR_RecruitmentSchemeMaster";
        public const string TODATE_FLD = "ToDate";
        public const string TOTALEMPLOYEE_FLD = "TotalEmployee";
        public const string TOTALFEMALE_FLD = "TotalFemale";
        public const string TOTALMALE_FLD = "TotalMale";
        public const string TRANSDATE_FLD = "TransDate";
        public const string TRANSNO_FLD = "TransNo";
        public const string USERID_FLD = "UserID";
    }

    public sealed class HR_RewardPunishTable
    {
        public const string CODE_FLD = "Code";
        public const string EMPLOYEEID_FLD = "EmployeeID";
        public const string REASON_FLD = "Reason";
        public const string REWARDDATE_FLD = "RewardDate";
        public const string REWARDPUNISHID_FLD = "RewardPunishID";
        public const string TABLE_NAME = "HR_RewardPunish";
        public const string TYPE_FLD = "Type";
    }

    public sealed class HR_TimesheetByProductDetailTable
    {
        public const string CONVERTEDQUANTITY_FLD = "ConvertedQuantity";
        public const string EMPLOYEEID_FLD = "EmployeeID";
        public const string TABLE_NAME = "HR_TimesheetByProductDetail";
        public const string TIMESHEETBYPRODUCTDETAILID_FLD = "TimesheetByProductDetailID";
        public const string TIMESHEETBYPRODUCTMASTERID_FLD = "TimesheetByProductMasterID";
    }

    public sealed class HR_TimesheetByProductMasterTable
    {
        public const string CCNID_FLD = "CCNID";
        public const string CHECKDATE_FLD = "CheckDate";
        public const string DEPARTMENTID_FLD = "DepartmentID";
        public const string PRODUCTIONLINEID_FLD = "ProductionLineID";
        public const string TABLE_NAME = "HR_TimesheetByProductMaster";
        public const string TIMESHEETBYPRODUCTMASTERID_FLD = "TimesheetByProductMasterID";
        public const string TOTALQTYAFTERCONVERTED_FLD = "TotalQtyAfterConverted";
        public const string TRANSDATE_FLD = "TransDate";
        public const string WORKORDERMASTERID_FLD = "WorkOrderMasterID";
    }

    public sealed class HR_TimesheetDetailTable
    {
        public const string D01C_FLD = "D01C";
        public const string D01S_FLD = "D01S";
        public const string D02C_FLD = "D02C";
        public const string D02S_FLD = "D02S";
        public const string D03C_FLD = "D03C";
        public const string D03S_FLD = "D03S";
        public const string D04C_FLD = "D04C";
        public const string D04S_FLD = "D04S";
        public const string D05C_FLD = "D05C";
        public const string D05S_FLD = "D05S";
        public const string D06C_FLD = "D06C";
        public const string D06S_FLD = "D06S";
        public const string D07C_FLD = "D07C";
        public const string D07S_FLD = "D07S";
        public const string D08C_FLD = "D08C";
        public const string D08S_FLD = "D08S";
        public const string D09C_FLD = "D09C";
        public const string D09S_FLD = "D09S";
        public const string D10C_FLD = "D10C";
        public const string D10S_FLD = "D10S";
        public const string D11C_FLD = "D11C";
        public const string D11S_FLD = "D11S";
        public const string D12C_FLD = "D12C";
        public const string D12S_FLD = "D12S";
        public const string D13C_FLD = "D13C";
        public const string D13S_FLD = "D13S";
        public const string D14C_FLD = "D14C";
        public const string D14S_FLD = "D14S";
        public const string D15C_FLD = "D15C";
        public const string D15S_FLD = "D15S";
        public const string D16C_FLD = "D16C";
        public const string D16S_FLD = "D16S";
        public const string D17C_FLD = "D17C";
        public const string D17S_FLD = "D17S";
        public const string D18C_FLD = "D18C";
        public const string D18S_FLD = "D18S";
        public const string D19C_FLD = "D19C";
        public const string D19S_FLD = "D19S";
        public const string D20C_FLD = "D20C";
        public const string D20S_FLD = "D20S";
        public const string D21C_FLD = "D21C";
        public const string D21S_FLD = "D21S";
        public const string D22C_FLD = "D22C";
        public const string D22S_FLD = "D22S";
        public const string D23C_FLD = "D23C";
        public const string D23S_FLD = "D23S";
        public const string D24C_FLD = "D24C";
        public const string D24S_FLD = "D24S";
        public const string D25C_FLD = "D25C";
        public const string D25S_FLD = "D25S";
        public const string D26C_FLD = "D26C";
        public const string D26S_FLD = "D26S";
        public const string D27C_FLD = "D27C";
        public const string D27S_FLD = "D27S";
        public const string D28C_FLD = "D28C";
        public const string D28S_FLD = "D28S";
        public const string D29C_FLD = "D29C";
        public const string D29S_FLD = "D29S";
        public const string D30C_FLD = "D30C";
        public const string D30S_FLD = "D30S";
        public const string D31C_FLD = "D31C";
        public const string D31S_FLD = "D31S";
        public const string EMPLOYEEID_FLD = "EmployeeID";
        public const string TABLE_NAME = "HR_TimesheetDetail";
        public const string TIMESHEETDETAILID_FLD = "TimesheetDetailID";
        public const string TIMESHEETMASTERID_FLD = "TimesheetMasterID";
        public const string TOTALDAYS_FLD = "TotalDays";
    }

    public sealed class HR_TimesheetMasterTable
    {
        public const string CCNID_FLD = "CCNID";
        public const string CHECKDATE_FLD = "CheckDate";
        public const string DEPARTMENTID_FLD = "DepartmentID";
        public const string PRODUCTIONLINEID_FLD = "ProductionLineID";
        public const string TABLE_NAME = "HR_TimesheetMaster";
        public const string TIMESHEETMASTERID_FLD = "TimesheetMasterID";
        public const string USERID_FLD = "UserID";
    }

    public sealed class HR_TrainingTable
    {
        public const string CERTIFICATE_FLD = "Certificate";
        public const string COURSENAME_FLD = "CourseName";
        public const string EMPLOYEEID_FLD = "EmployeeID";
        public const string FROMDATE_FLD = "FromDate";
        public const string TABLE_NAME = "HR_Training";
        public const string TODATE_FLD = "ToDate";
        public const string TRAININGID_FLD = "TrainingID";
        public const string TUITION_FLD = "Tuition";
    }

    public sealed class ITM_BuyerTable
    {
        public const string ADDRESS_FLD = "Address";
        public const string BUYERID_FLD = "BuyerID";
        public const string CODE_FLD = "Code";
        public const string DESCRIPTION_FLD = "Description";
        public const string NAME_FLD = "Name";
        public const string TABLE_NAME = "ITM_Buyer";
    }

    public sealed class ITM_CategoryTable
    {
        public const string CATALOGNAME_FLD = "CatalogName";
        public const string CATEGORYID_FLD = "CategoryID";
        public const string CODE_FLD = "Code";
        public const string DESCRIPTION_FLD = "Description";
        public const string NAME_FLD = "Name";
        public const string PARENTCATEGORYID_FLD = "ParentCategoryId";
        public const string PICTURE_FLD = "Picture";
        public const string TABLE_NAME = "ITM_Category";
    }

    public sealed class ITM_CostCenterTable
    {
        public const string CODE_FLD = "Code";
        public const string COSTCENTERID_FLD = "CostCenterID";
        public const string DESCRIPTION_FLD = "Description";
        public const string NAME_FLD = "Name";
        public const string TABLE_NAME = "ITM_CostCenter";
    }

    public sealed class ITM_CostCenterRateTable
    {
        public const string CCNID_FLD = "CCNID";
        public const string COSTCENTERID_FLD = "CostCenterID";
        public const string COSTCENTERRATEID_FLD = "CostCenterRateID";
        public const string EFFECTIVEBEGINDATE_FLD = "EffectiveBeginDate";
        public const string EFFECTIVEENDDATE_FLD = "EffectiveEndDate";
        public const string LABORRUN_FLD = "LaborRun";
        public const string LABORRUNFIXEDOVERHEAD_FLD = "LaborRunFixedOverhead";
        public const string LABORRUNVARIABLEOVERHEAD_FLD = "LaborRunVariableOverhead";
        public const string LABORSETUP_FLD = "LaborSetup";
        public const string LABORSETUPFIXEDOVERHEAD_FLD = "LaborSetupFixedOverhead";
        public const string LABORSETUPVARIABLEOVERHEAD_FLD = "LaborSetupVariableOverhead";
        public const string MACHINERUN_FLD = "MachineRun";
        public const string MACHINERUNFIXEDOVERHEAD_FLD = "MachineRunFixedOverhead";
        public const string MACHINERUNVARIABLEOVERHEAD_FLD = "MachineRunVariableOverhead";
        public const string MACHINESETUP_FLD = "MachineSetup";
        public const string MACHINESETUPFIXEDOVERHEAD_FLD = "MachineSetupFixedOverhead";
        public const string MACHINESETUPVARIABLEOVERHEAD_FLD = "MachineSetupVariableOverhead";
        public const string MATERIALOVERHEAD_FLD = "MaterialOverhead";
        public const string TABLE_NAME = "ITM_CostCenterRate";
    }

    public sealed class ITM_DeleteReasonTable
    {
        public const string CODE_FLD = "Code";
        public const string DELETEREASONID_FLD = "DeleteReasonID";
        public const string DESCRIPTION_FLD = "Description";
        public const string TABLE_NAME = "ITM_DeleteReason";
    }

    public sealed class ITM_DeliveryPolicyTable
    {
        public const string CODE_FLD = "Code";
        public const string DELIVERYPOLICYDAYS_FLD = "DeliveryPolicyDays";
        public const string DELIVERYPOLICYID_FLD = "DeliveryPolicyID";
        public const string DESCRIPTION_FLD = "Description";
        public const string TABLE_NAME = "ITM_DeliveryPolicy";
    }

    public sealed class ITM_FormatCodeTable
    {
        public const string CODE_FLD = "Code";
        public const string DESCRIPTION_FLD = "Description";
        public const string FORMATCODEID_FLD = "FormatCodeID";
        public const string TABLE_NAME = "ITM_FormatCode";
    }

    public sealed class ITM_FreightClassTable
    {
        public const string CODE_FLD = "Code";
        public const string DESCRIPTION_FLD = "Description";
        public const string FREIGHTCLASSID_FLD = "FreightClassID";
        public const string TABLE_NAME = "ITM_FreightClass";
    }

    public sealed class ITM_HazardTable
    {
        public const string CODE_FLD = "Code";
        public const string DESCRIPTION_FLD = "Description";
        public const string HAZARDID_FLD = "HazardID";
        public const string TABLE_NAME = "ITM_Hazard";
    }

    public sealed class ITM_HierarchyTable
    {
        public const string DESTINATION_FLD = "Destination";
        public const string HIERARCHYID_FLD = "HierarchyID";
        public const string SOURCE_FLD = "Source";
        public const string TABLE_NAME = "ITM_Hierarchy";
    }

    public sealed class ITM_OrderPolicyTable
    {
        public const string CODE_FLD = "Code";
        public const string DESCRIPTION_FLD = "Description";
        public const string ORDERPOLICYDAYS_FLD = "OrderPolicyDays";
        public const string ORDERPOLICYID_FLD = "OrderPolicyID";
        public const string TABLE_NAME = "ITM_OrderPolicy";
    }

    public sealed class ITM_OrderRuleTable
    {
        public const string CODE_FLD = "Code";
        public const string DESCRIPTION_FLD = "Description";
        public const string ORDERRULEID_FLD = "OrderRuleID";
        public const string TABLE_NAME = "ITM_OrderRule";
    }

    public sealed class ITM_PictureTable
    {
        public const string DESCRIPTION_FLD = "Description";
        public const string PICTUREID_FLD = "PictureID";
        public const string PICTUREIMAGE_FLD = "PictureImage";
        public const string PRODUCTID_FLD = "ProductID";
        public const string TABLE_NAME = "ITM_Picture";
    }

    public sealed class ITM_ProductTypeTable
    {
        public const string CODE_FLD = "Code";
        public const string DESCRIPTION_FLD = "Description";
        public const string PRODUCTTYPEID_FLD = "ProductTypeID";
        public const string TABLE_NAME = "ITM_ProductType";
    }

    public sealed class ITM_RevisionTable
    {
        public const string CODE_FLD = "Code";
        public const string DESCRIPTION_FLD = "Description";
        public const string REVISIONID_FLD = "RevisionID";
        public const string TABLE_NAME = "ITM_Revision";
    }

    public sealed class ITM_RoutingTable
    {
        public const string CALCULATETIME_FLD = "CalculateTime";
        public const string CREWSIZE_FLD = "CrewSize";
        public const string EFFECTBEGINDATE_FLD = "EffectBeginDate";
        public const string EFFECTENDDATE_FLD = "EffectEndDate";
        public const string FIXLT_FLD = "FixLT";
        public const string FUNCTIONID_FLD = "FunctionID";
        public const string LABORCOSTCENTERID_FLD = "LaborCostCenterID";
        public const string LABORRUNTIME_FLD = "LaborRunTime";
        public const string LABORSETUPTIME_FLD = "LaborSetupTime";
        public const string MACHINECOSTCENTERID_FLD = "MachineCostCenterID";
        public const string MACHINERUNTIME_FLD = "MachineRunTime";
        public const string MACHINES_FLD = "Machines";
        public const string MACHINESETUPTIME_FLD = "MachineSetupTime";
        public const string MOVETIME_FLD = "MoveTime";
        public const string OSCOST_FLD = "OSCost";
        public const string OSFIXLT_FLD = "OSFixLT";
        public const string OSOVERLAPPERCENT_FLD = "OSOverlapPercent";
        public const string OSOVERLAPQTY_FLD = "OSOverlapQty";
        public const string OSSCHEDULESEQ_FLD = "OSScheduleSeq";
        public const string OSVARLT_FLD = "OSVarLT";
        public const string OVERLAPPERCENT_FLD = "OverlapPercent";
        public const string OVERLAPQTY_FLD = "OverlapQty";
        public const string PACER_FLD = "Pacer";
        public const string PARTYID_FLD = "PartyID";
        public const string PRODUCTID_FLD = "ProductID";
        public const string ROUTINGID_FLD = "RoutingID";
        public const string ROUTINGSTATUSID_FLD = "RoutingStatusID";
        public const string RUNQUANTITY_FLD = "RunQuantity";
        public const string SCHEDULESEQ_FLD = "ScheduleSeq";
        public const string SETUPQUANTITY_FLD = "SetupQuantity";
        public const string STEP_FLD = "Step";
        public const string STUDYTIME_FLD = "StudyTime";
        public const string TABLE_NAME = "ITM_Routing";
        public const string TYPE_FLD = "Type";
        public const string VARLT_FLD = "VarLT";
        public const string WORKCENTERID_FLD = "WorkCenterID";
    }

    public sealed class ITM_RoutingStatusTable
    {
        public const string CODE_FLD = "Code";
        public const string DESCRIPTION_FLD = "Description";
        public const string ROUTINGSTATUSID_FLD = "RoutingStatusID";
        public const string TABLE_NAME = "ITM_RoutingStatus";
    }

    public sealed class ITM_ShipToleranceTable
    {
        public const string CODE_FLD = "Code";
        public const string DESCRIPTION_FLD = "Description";
        public const string OVERQTY_FLD = "OverQty";
        public const string SHIPTOLERANCEID_FLD = "ShipToleranceID";
        public const string TABLE_NAME = "ITM_ShipTolerance";
        public const string UNDERQTY_FLD = "UnderQty";
    }

    public sealed class ITM_SourceTable
    {
        public const string CODE_FLD = "Code";
        public const string DESCRIPTION_FLD = "Description";
        public const string PRIORITY_FLD = "Priority";
        public const string SOURCEID_FLD = "SourceID";
        public const string TABLE_NAME = "ITM_Source";
    }

    public sealed class IV_AdjustmentTable
    {
        public const string ADJUSTMENTID_FLD = "AdjustmentID";
        public const string ADJUSTQUANTITY_FLD = "AdjustQuantity";
        public const string AVAILABLEQTY_FLD = "AvailableQty";
        public const string BINID_FLD = "BinID";
        public const string CCNID_FLD = "CCNID";
        public const string COMMENT_FLD = "Comment";
        public const string LASTCHANGE_FLD = "LastChange";
        public const string LOCATIONID_FLD = "LocationID";
        public const string LOT_FLD = "Lot";
        public const string MASTERLOCATIONID_FLD = "MasterLocationID";
        public const string POSTDATE_FLD = "PostDate";
        public const string PRODUCTID_FLD = "ProductID";
        public const string SERIAL_FLD = "Serial";
        public const string STOCKUMID_FLD = "StockUMID";
        public const string TABLE_NAME = "IV_Adjustment";
        public const string TRANSNO_FLD = "TransNo";
        public const string USEDBYCOSTING_FLD = "UsedByCosting";
        public const string USERNAME_FLD = "UserName";
    }

    public sealed class IV_BalanceBinTable
    {
        public const string BALANCEBINID_FLD = "BalanceBinID";
        public const string BINID_FLD = "BinID";
        public const string COMMITQUANTITY_FLD = "CommitQuantity";
        public const string EFFECTDATE_FLD = "EffectDate";
        public const string LOCATIONID_FLD = "LocationID";
        public const string OHQUANTITY_FLD = "OHQuantity";
        public const string PRODUCTID_FLD = "ProductID";
        public const string STOCKUMID_FLD = "StockUMID";
        public const string TABLE_NAME = "IV_BalanceBin";
    }

    public sealed class IV_BalanceLocationTable
    {
        public const string BALANCELOCATIONID_FLD = "BalanceLocationID";
        public const string COMMITQUANTITY_FLD = "CommitQuantity";
        public const string EFFECTDATE_FLD = "EffectDate";
        public const string LOCATIONID_FLD = "LocationID";
        public const string MASTERLOCATIONID_FLD = "MasterLocationID";
        public const string OHQUANTITY_FLD = "OHQuantity";
        public const string PRODUCTID_FLD = "ProductID";
        public const string STOCKUMID_FLD = "StockUMID";
        public const string TABLE_NAME = "IV_BalanceLocation";
    }

    public sealed class IV_BalanceMasterLocationTable
    {
        public const string BALANCEMASTERLOCATIONID_FLD = "BalanceMasterLocationID";
        public const string COMMITQUANTITY_FLD = "CommitQuantity";
        public const string EFFECTDATE_FLD = "EffectDate";
        public const string MASTERLOCATIONID_FLD = "MasterLocationID";
        public const string OHQUANTITY_FLD = "OHQuantity";
        public const string PRODUCTID_FLD = "ProductID";
        public const string STOCKUMID_FLD = "StockUMID";
        public const string TABLE_NAME = "IV_BalanceMasterLocation";
    }

    public sealed class IV_BeginDCPReportTable
    {
        public const string BEGINDCPREPORTID_FLD = "BeginDCPReportID";
        public const string EFFECTDATE_FLD = "EffectDate";
        public const string LASTUPDATE_FLD = "LastUpdate";
        public const string PRODUCTID_FLD = "ProductID";
        public const string QUANTITY_FLD = "Quantity";
        public const string TABLE_NAME = "IV_BeginDCPReport";
        public const string USERNAME_FLD = "Username";
    }

    public sealed class IV_BeginMRPTable
    {
        public const string ASOFDATE_FLD = "AsOfDate";
        public const string BEGINMRPID_FLD = "BeginMRPID";
        public const string LOCATIONID_FLD = "LocationID";
        public const string PRODUCTID_FLD = "ProductID";
        public const string QUANTITY_FLD = "Quantity";
        public const string QUANTITYMAP_FLD = "QuantityMAP";
        public const string TABLE_NAME = "IV_BeginMRP";
        public const string ASOFTDATE_FLD = "AsOfDate";
    }

    public sealed class IV_BinCacheTable
    {
        public const string BINCACHEID_FLD = "BinCacheID";
        public const string BINID_FLD = "BinID";
        public const string CCNID_FLD = "CCNID";
        public const string COMMITQUANTITY_FLD = "CommitQuantity";
        public const string DEMANQUANTITY_FLD = "DemanQuantity";
        public const string INSPSTATUS_FLD = "InspStatus";
        public const string LOCATIONID_FLD = "LocationID";
        public const string LOT_FLD = "Lot";
        public const string MASTERLOCATIONID_FLD = "MasterLocationID";
        public const string OHQUANTITY_FLD = "OHQuantity";
        public const string PRODUCTID_FLD = "ProductID";
        public const string SUPPLYQUANTITY_FLD = "SupplyQuantity";
        public const string TABLE_NAME = "IV_BinCache";
    }

    public sealed class IV_CostHistoryTable
    {
        public const string CCNID_FLD = "CCNID";
        public const string COSTHISTORYID_FLD = "CostHistoryID";
        public const string COSTHISTORYSEQ_FLD = "CostHistorySeq";
        public const string ICDHITEMCOST21_FLD = "ICDHItemCost21";
        public const string MASTERLOCATIONID_FLD = "MasterLocationID";
        public const string PARTYID_FLD = "PartyID";
        public const string PARTYLOCATIONID_FLD = "PartyLocationID";
        public const string PRODUCTID_FLD = "ProductID";
        public const string QASTATUS_FLD = "QAStatus";
        public const string RECEIVEDATE_FLD = "ReceiveDate";
        public const string RECEIVEREF_FLD = "ReceiveRef";
        public const string RECEIVEREFLINE_FLD = "ReceiveRefLine";
        public const string STOCKUMID_FLD = "StockUMID";
        public const string TABLE_NAME = "IV_CostHistory";
        public const string TRANTYPEID_FLD = "TranTypeID";
    }

    public sealed class IV_CoutingMethodTable
    {
        public const string CODE_FLD = "Code";
        public const string COUNTINGMETHODID_FLD = "CountingMethodID";
        public const string DESCRIPTION_FLD = "Description";
        public const string TABLE_NAME = "IV_CoutingMethod";
    }

    public sealed class IV_LocationCacheTable
    {
        public const string CCNID_FLD = "CCNID";
        public const string COMMITQUANTITY_FLD = "CommitQuantity";
        public const string DEMANQUANTITY_FLD = "DemanQuantity";
        public const string INSPSTATUS_FLD = "InspStatus";
        public const string LOCATIONCACHEID_FLD = "LocationCacheID";
        public const string LOCATIONID_FLD = "LocationID";
        public const string LOT_FLD = "Lot";
        public const string MASTERLOCATIONID_FLD = "MasterLocationID";
        public const string OHQUANTITY_FLD = "OHQuantity";
        public const string PRODUCTID_FLD = "ProductID";
        public const string SUPPLYQUANTITY_FLD = "SupplyQuantity";
        public const string TABLE_NAME = "IV_LocationCache";
    }

    public sealed class IV_MasLocCacheTable
    {
        public const string AVGCOST_FLD = "AVGCost";
        public const string CCNID_FLD = "CCNID";
        public const string COMMITQUANTITY_FLD = "CommitQuantity";
        public const string DEMANQUANTITY_FLD = "DemanQuantity";
        public const string INSPSTATUS_FLD = "InspStatus";
        public const string LOT_FLD = "Lot";
        public const string MASLOCCACHEID_FLD = "MasLocCacheID";
        public const string MASTERLOCATIONID_FLD = "MasterLocationID";
        public const string OHQUANTITY_FLD = "OHQuantity";
        public const string PRODUCTID_FLD = "ProductID";
        public const string SUMMITEMCOST21_FLD = "SummItemCost21";
        public const string SUPPLYQUANTITY_FLD = "SupplyQuantity";
        public const string TABLE_NAME = "IV_MasLocCache";
    }

    public sealed class IV_MiscellaneousIssueDetailTable
    {
        public const string AVAILABLEQTY_FLD = "AvailableQty";
        public const string LOT_FLD = "Lot";
        public const string MISCELLANEOUSISSUEDETAILID_FLD = "MiscellaneousIssueDetailID";
        public const string MISCELLANEOUSISSUEMASTERID_FLD = "MiscellaneousIssueMasterID";
        public const string PRODUCTID_FLD = "ProductID";
        public const string QUANTITY_FLD = "Quantity";
        public const string REQUIREDMATERIALDETAILID_FLD = "RequiredMaterialDetailID";
        public const string STOCKUMID_FLD = "StockUMID";
        public const string TABLE_NAME = "IV_MiscellaneousIssueDetail";
    }

    public sealed class IV_MiscellaneousIssueMasterTable
    {
        public const string CCNID_FLD = "CCNID";
        public const string COMMENT_FLD = "Comment";
        public const string DESBINID_FLD = "DesBinID";
        public const string DESLOCATIONID_FLD = "DesLocationID";
        public const string DESMASLOCATIONID_FLD = "DesMasLocationID";
        public const string INVOICEMASTERID_FLD = "InvoiceMasterID";
        public const string ISSUEPURPOSEID_FLD = "IssuePurposeID";
        public const string LASTCHANGE_FLD = "LastChange";
        public const string MISCELLANEOUSISSUEMASTERID_FLD = "MiscellaneousIssueMasterID";
        public const string PARTYID_FLD = "PartyID";
        public const string POSTDATE_FLD = "PostDate";
        public const string REQUIREDMATERIALMASTERID_FLD = "RequiredMaterialMasterID";
        public const string SALEORDERDETAILID_FLD = "SaleOrderDetailID";
        public const string SOURCEBINID_FLD = "SourceBinID";
        public const string SOURCELOCATIONID_FLD = "SourceLocationID";
        public const string SOURCEMASLOCATIONID_FLD = "SourceMasLocationID";
        public const string TABLE_NAME = "IV_MiscellaneousIssueMaster";
        public const string TRANSNO_FLD = "TransNo";
        public const string USERNAME_FLD = "UserName";
    }

    public sealed class IV_OnhandPeriodTable
    {
        public const string CODE_FLD = "Code";
        public const string EFFECTDATE_FLD = "EffectDate";
        public const string ONHANDPERIODID_FLD = "OnhandPeriodID";
        public const string STATUS_FLD = "Status";
        public const string TABLE_NAME = "IV_OnhandPeriod";
    }

    public sealed class IV_StockTakingTable
    {
        public const string BOOKQUANTITY_FLD = "BookQuantity";
        public const string COUNTINGMETHODID_FLD = "CountingMethodID";
        public const string NOTE_FLD = "Note";
        public const string PRODUCTID_FLD = "ProductID";
        public const string QUANTITY_FLD = "Quantity";
        public const string SLIPCODE_FLD = "SlipCode";
        public const string STOCKTAKINGID_FLD = "StockTakingID";
        public const string STOCKTAKINGMASTERID_FLD = "StockTakingMasterID";
        public const string STOCKUMID_FLD = "StockUMID";
        public const string TABLE_NAME = "IV_StockTaking";
    }

    public sealed class IV_StockTakingDifferentTable
    {
        public const string ACTUALQUANTITY_FLD = "ActualQuantity";
        public const string BINID_FLD = "BinID";
        public const string DIFFERENTQUANTITY_FLD = "DifferentQuantity";
        public const string HISTORYQUANTITY_FLD = "HistoryQuantity";
        public const string LOCATIONID_FLD = "LocationID";
        public const string OHQUANTITY_FLD = "OHQuantity";
        public const string PRODUCTID_FLD = "ProductID";
        public const string STOCKTAKINGDATE_FLD = "StockTakingDate";
        public const string STOCKTAKINGDIFFERENTID_FLD = "StockTakingDifferentID";
        public const string STOCKTAKINGPERIODID_FLD = "StockTakingPeriodID";
        public const string TABLE_NAME = "IV_StockTakingDifferent";
    }

    public sealed class IV_StockTakingMasterTable
    {
        public const string BINID_FLD = "BinID";
        public const string CODE_FLD = "Code";
        public const string DEPARTMENTID_FLD = "DepartmentID";
        public const string LOCATIONID_FLD = "LocationID";
        public const string PRODUCTIONLINEID_FLD = "ProductionLineID";
        public const string STOCKTAKINGDATE_FLD = "StockTakingDate";
        public const string STOCKTAKINGMASTERID_FLD = "StockTakingMasterID";
        public const string STOCKTAKINGPERIODID_FLD = "StockTakingPeriodID";
        public const string TABLE_NAME = "IV_StockTakingMaster";
    }

    public sealed class IV_StockTakingPeriodTable
    {
        public const string CCNID_FLD = "CCNID";
        public const string CLOSED_FLD = "Closed";
        public const string DESCRIPTION_FLD = "Description";
        public const string FROMDATE_FLD = "FromDate";
        public const string STOCKTAKINGDATE_FLD = "StockTakingDate";
        public const string STOCKTAKINGPERIODID_FLD = "StockTakingPeriodID";
        public const string TABLE_NAME = "IV_StockTakingPeriod";
        public const string TODATE_FLD = "ToDate";
    }

    public sealed class MST_AbilityTable
    {
        public const string ABILITYID_FLD = "AbilityID";
        public const string CODE_FLD = "Code";
        public const string DESCRIPTION_FLD = "Description";
        public const string LEVEL_FLD = "Level";
        public const string NAME_FLD = "Name";
        public const string TABLE_NAME = "MST_Ability";
    }

    public sealed class MST_AddChargeTable
    {
        public const string ADDCHARGEID_FLD = "AddChargeID";
        public const string CODE_FLD = "Code";
        public const string DESCRIPTION_FLD = "Description";
        public const string TABLE_NAME = "MST_AddCharge";
        public const string VAT_FLD = "VAT";
    }

    public sealed class MST_AGCTable
    {
        public const string AGCID_FLD = "AGCID";
        public const string CCNID_FLD = "CCNID";
        public const string CODE_FLD = "Code";
        public const string DESCRIPTION_FLD = "Description";
        public const string TABLE_NAME = "MST_AGC";
    }

    public sealed class MST_ApprovalLevelTable
    {
        public const string AMOUNT_FLD = "Amount";
        public const string APPROVALLEVELID_FLD = "ApprovalLevelID";
        public const string CCNID_FLD = "CCNID";
        public const string DESCRIPTION_FLD = "Description";
        public const string LEVEL_FLD = "Level";
        public const string TABLE_NAME = "MST_ApprovalLevel";
    }

    public sealed class MST_BanksTable
    {
        public const string BALANCE_FLD = "Balance";
        public const string BANKID_FLD = "BankID";
        public const string BANKNAME_FLD = "BankName";
        public const string CCNID_FLD = "CCNID";
        public const string CHARTOFACCOUNTID_FLD = "ChartOfAccountID";
        public const string CODE_FLD = "Code";
        public const string CURRENCYID_FLD = "CurrencyID";
        public const string TABLE_NAME = "MST_Banks";
    }

    public sealed class MST_BINTable
    {
        public const string BINID_FLD = "BinID";
        public const string BINTYPEID_FLD = "BinTypeID";
        public const string CODE_FLD = "Code";
        public const string HEIGHT_FLD = "Height";
        public const string HEIGHTUNITID_FLD = "HeightUnitID";
        public const string LENGTH_FLD = "Length";
        public const string LENGTHUNITID_FLD = "LengthUnitID";
        public const string LOCATIONID_FLD = "LocationID";
        public const string LOCATIONTYPEID_FLD = "LocationTypeID";
        public const string NAME_FLD = "Name";
        public const string TABLE_NAME = "MST_BIN";
        public const string WIDTH_FLD = "Width";
        public const string WIDTHUNITID_FLD = "WidthUnitID";
    }

    public sealed class MST_CarrierTable
    {
        public const string ADDRESS_FLD = "Address";
        public const string CARRIERID_FLD = "CarrierID";
        public const string CODE_FLD = "Code";
        public const string EMAIL_FLD = "Email";
        public const string FAX_FLD = "Fax";
        public const string NAME_FLD = "Name";
        public const string PHONE_FLD = "Phone";
        public const string TABLE_NAME = "MST_Carrier";
        public const string WEBSITE_FLD = "WebSite";
    }

    public sealed class MST_CCNTable
    {
        public const string CCNID_FLD = "CCNID";
        public const string CHARTOFACCOUNTSTRUCTID_FLD = "ChartOfAccountStructID";
        public const string CITYID_FLD = "CityID";
        public const string CODE_FLD = "Code";
        public const string COUNTRYID_FLD = "CountryID";
        public const string DEFAULTCURRENCYID_FLD = "DefaultCurrencyID";
        public const string DESCRIPTION_FLD = "Description";
        public const string EMAIL_FLD = "Email";
        public const string EXCHANGERATE_FLD = "ExchangeRate";
        public const string EXCHANGERATEOPERATOR_FLD = "ExchangeRateOperator";
        public const string FAX_FLD = "Fax";
        public const string HOMECURRENCYID_FLD = "HomeCurrencyID";
        public const string NAME_FLD = "Name";
        public const string NAMEVN_FLD = "NameVN";
        public const string PHONE_FLD = "Phone";
        public const string STATE_FLD = "State";
        public const string TABLE_NAME = "MST_CCN";
        public const string VAT_FLD = "VAT";
        public const string WEBSITE_FLD = "WebSite";
        public const string ZIPCODE_FLD = "ZipCode";
    }

    public sealed class MST_CityTable
    {
        public const string CITYID_FLD = "CityID";
        public const string CODE_FLD = "Code";
        public const string COUNTRYID_FLD = "CountryID";
        public const string NAME_FLD = "Name";
        public const string TABLE_NAME = "MST_City";
    }

    public sealed class MST_CollegeTable
    {
        public const string CODE_FLD = "Code";
        public const string COLLEGEID_FLD = "CollegeID";
        public const string NAME_FLD = "Name";
        public const string TABLE_NAME = "MST_College";
    }

    public sealed class MST_CountryTable
    {
        public const string CODE_FLD = "Code";
        public const string COUNTRYID_FLD = "CountryID";
        public const string NAME_FLD = "Name";
        public const string TABLE_NAME = "MST_Country";
    }

    public sealed class MST_CurrencyTable
    {
        public const string CODE_FLD = "Code";
        public const string CURRENCYID_FLD = "CurrencyID";
        public const string MASK_FLD = "Mask";
        public const string NAME_FLD = "Name";
        public const string TABLE_NAME = "MST_Currency";
    }

    public sealed class MST_DegreeCertTable
    {
        public const string CODE_FLD = "Code";
        public const string COLLEGEID_FLD = "CollegeID";
        public const string COUNTRYID_FLD = "CountryID";
        public const string DEGREECERTID_FLD = "DegreeCertID";
        public const string DESCRIPTION_FLD = "Description";
        public const string FORMOFLEARNING_FLD = "FormOfLearning";
        public const string QUALIFICATION_FLD = "Qualification";
        public const string TABLE_NAME = "MST_DegreeCert";
    }

    public sealed class MST_DeliveryTermTable
    {
        public const string CODE_FLD = "Code";
        public const string DELIVERYTERMID_FLD = "DeliveryTermID";
        public const string DESCRIPTION_FLD = "Description";
        public const string TABLE_NAME = "MST_DeliveryTerm";
    }

    public sealed class MST_DepartmentTable
    {
        public const string CCNID_FLD = "CCNID";
        public const string CODE_FLD = "Code";
        public const string COSTACCOUNTID_FLD = "CostAccountID";
        public const string DEPARTMENTID_FLD = "DepartmentID";
        public const string NAME_FLD = "Name";
        public const string TABLE_NAME = "MST_Department";
    }

    public sealed class MST_DiscountTermTable
    {
        public const string CODE_FLD = "Code";
        public const string DESCRIPTION_FLD = "Description";
        public const string DISCOUNTTERMID_FLD = "DiscountTermID";
        public const string QUANTITY_FLD = "Quantity";
        public const string RATE_FLD = "Rate";
        public const string TABLE_NAME = "MST_DiscountTerm";
    }

    public sealed class MST_EmpAbilityTable
    {
        public const string ABILITYID_FLD = "AbilityID";
        public const string EMPLOYEEID_FLD = "EmployeeID";
        public const string RANK_FLD = "Rank";
        public const string TABLE_NAME = "MST_EmpAbility";
    }

    public sealed class MST_EmpDegreeTable
    {
        public const string DEGREECERTID_FLD = "DegreeCertID";
        public const string EMPLOYEEID_FLD = "EmployeeID";
        public const string GRADE_FLD = "Grade";
        public const string GRADUATEDATE_FLD = "GraduateDate";
        public const string TABLE_NAME = "MST_EmpDegree";
    }

    public sealed class MST_EmployeeTable
    {
        public const string CODE_FLD = "Code";
        public const string DEPARTMENTID_FLD = "DepartmentID";
        public const string EMPLOYEEID_FLD = "EmployeeID";
        public const string NAME_FLD = "Name";
        public const string SHIFT_FLD = "Shift";
        public const string TABLE_NAME = "MST_Employee";
    }

    public sealed class MST_EmployeeApprovalLevelTable
    {
        public const string APPROVALLEVELID_FLD = "ApprovalLevelID";
        public const string DESCRIPTION_FLD = "Description";
        public const string EMPLOYEEAPPROVALLEVELID_FLD = "EmployeeApprovalLevelID";
        public const string EMPLOYEEID_FLD = "EmployeeID";
        public const string TABLE_NAME = "MST_EmployeeApprovalLevel";
    }

    public sealed class MST_ExchangeRateTable
    {
        public const string APPROVALDATE_FLD = "ApprovalDate";
        public const string APPROVED_FLD = "Approved";
        public const string BEGINDATE_FLD = "BeginDate";
        public const string CCNID_FLD = "CCNID";
        public const string CODE_FLD = "Code";
        public const string CURRENCYID_FLD = "CurrencyID";
        public const string DESCRIPTION_FLD = "Description";
        public const string ENDDATE_FLD = "EndDate";
        public const string EXCHANGERATEID_FLD = "ExchangeRateID";
        public const string RATE_FLD = "Rate";
        public const string RATETYPE_FLD = "RateType";
        public const string TABLE_NAME = "MST_ExchangeRate";
    }

    public sealed class MST_FunctionTable
    {
        public const string CODE_FLD = "Code";
        public const string DESCRIPTION_FLD = "Description";
        public const string FUNCTIONID_FLD = "FunctionID";
        public const string TABLE_NAME = "MST_Function";
        public const string WORKCENTERID_FLD = "WorkCenterID";
    }

    public sealed class MST_LocationTable
    {
        public const string BIN_FLD = "Bin";
        public const string CODE_FLD = "Code";
        public const string DEPARTMENTID_FLD = "DepartmentID";
        public const string LOCATIONID_FLD = "LocationID";
        public const string LOCATIONTYPEID_FLD = "LocationTypeID";
        public const string MANUFACTURINGACCESS_FLD = "ManufacturingAccess";
        public const string MASTERLOCATIONID_FLD = "MasterLocationID";
        public const string NAME_FLD = "Name";
        public const string SALEACCESS_FLD = "SaleAccess";
        public const string TABLE_NAME = "MST_Location";
        public const string TYPE_FLD = "Type";
    }

    public sealed class MST_MasterLocationTable
    {
        public const string ADDRESS_FLD = "Address";
        public const string CCNID_FLD = "CCNID";
        public const string CITYID_FLD = "CityID";
        public const string CODE_FLD = "Code";
        public const string COUNTRYID_FLD = "CountryID";
        public const string MASTERLOCATIONID_FLD = "MasterLocationID";
        public const string NAME_FLD = "Name";
        public const string STATE_FLD = "State";
        public const string TABLE_NAME = "MST_MasterLocation";
        public const string ZIPPOST_FLD = "ZipPost";
    }

    public sealed class MST_AgentTypeTable
    {
        public const string AGENTTYPEID_FLD = "AgentTypeID";
        public const string CODE_FLD = "Code";
        public const string DESCRIPTION_FLD = "Description";
        public const string TABLE_NAME = "MST_AgentType";
    }

    public sealed class MST_CommunicationTable
    {
        public const string ASSESSMENT_FLD = "Assessment";
        public const string CODE_FLD = "Code";
        public const string COMMUNICATIONID_FLD = "CommunicationID";
        public const string FEEDBACKINFO_FLD = "FeedbackInfo";
        public const string NEXTDATE_FLD = "NextDate";
        public const string NEXTREQUIREMENT_FLD = "NextRequirement";
        public const string POSTDATE_FLD = "PostDate";
        public const string SALEEMPLOYEEID_FLD = "SaleEmployeeID";
        public const string TABLE_NAME = "MST_Communication";
        public const string TRANSACTIONCONTENT_FLD = "TransactionContent";
        public const string TRANSACTIONPERFORMER_FLD = "TransactionPerformer";
    }

    public sealed class MST_PartyTable
    {
        public const string ACCOUNTCODECHECK_FLD = "AccountCodeCheck";
        public const string ADDRESS_FLD = "Address";
        public const string AGENTTYPEID_FLD = "AgentTypeID";
        public const string APPLICATIONFORMCHECK_FLD = "ApplicationformCheck";
        public const string BANKACCOUNT_FLD = "BankAccount";
        public const string BIZCARDCHECK_FLD = "BizCardCheck";
        public const string BUSINESSREGISTRATION_FLD = "BusinessRegistration";
        public const string CHARTERCAPITAL_FLD = "CharterCapital";
        public const string CITYID_FLD = "CityID";
        public const string CODE_FLD = "Code";
        public const string COUNTRYID_FLD = "CountryID";
        public const string CURRENCYID_FLD = "CurrencyID";
        public const string DEBTLIMIT_FLD = "DebtLimit";
        public const string DELETEREASON_FLD = "DeleteReason";
        public const string DIRECTORNAME_FLD = "DirectorName";
        public const string FAX_FLD = "Fax";
        public const string FOUNDATIONDATE_FLD = "FoundationDate";
        public const string INDENTIFYCARDCHECK_FLD = "IndentifyCardCheck";
        public const string LANDLORDCARDCHECK_FLD = "LandLordCardCheck";
        public const string LASTUPDATE_FLD = "LastUpdate";
        public const string LIVINGCARDCHECK_FLD = "LivingCardCheck";
        public const string MAPBANKACCOUNTNAME_FLD = "MAPBankAccountName";
        public const string MAPBANKACCOUNTNO_FLD = "MAPBankAccountNo";
        public const string NAME_FLD = "Name";
        public const string PARTYID_FLD = "PartyID";
        public const string PARTYLEVEL_FLD = "PartyLevel";
        public const string PAYABLEACCOUNTID_FLD = "PayableAccountID";
        public const string PAYMENTTERMID_FLD = "PaymentTermID";
        public const string PHONE_FLD = "Phone";
        public const string RECEIVABLEACCOUNTID_FLD = "ReceivableAccountID";
        public const string STATE_FLD = "State";
        public const string TABLE_NAME = "MST_Party";
        public const string TAXCODECHECK_FLD = "TaxCodeCheck";
        public const string TYPE_FLD = "Type";
        public const string VATCODE_FLD = "VATCode";
        public const string WEBSITE_FLD = "WebSite";
        public const string ZIPPOST_FLD = "ZipPost";
    }

    // start duynt 23-07-2009
    // bang nay chi dung cho chuc nang import , khi lam viec voi db cua effect
    public sealed class Effect_KhangTable
    {
        public const string DIA_CHI_FLD = "DIA_CHI";
        public const string MA_FLD = "MA";
        public const string MA_GTGT_FLD = "Ma_GTGT";
        public const string PHONE_FLD = "PHONE";
        public const string TEN_FLD = "TEN";
        public const string TKHOAN_NH_FLD = "TKHOAN_NH";
        public const string TYPE_FLD = "Type";
    }

    // end duynt 23-07-2009
    public sealed class MST_RemindDetailTable
    {
        public const string CONFIRMJOB_FLD = "ConfirmJob";
        public const string CONFIRMTIME_FLD = "ConfirmTime";
        public const string DETAILDESCRIPTION_FLD = "DetailDescription";
        public const string EMPLOYEEID_FLD = "EmployeeID";
        public const string ENDTIME_FLD = "EndTime";
        public const string JOBTITLE_FLD = "JobTitle";
        public const string REMINDDETAILID_FLD = "RemindDetailID";
        public const string REMINDMASTERID_FLD = "RemindMasterID";
        public const string STARTTIME_FLD = "StartTime";
        public const string TABLE_NAME = "MST_RemindDetail";
    }

    public sealed class MST_RemindMasterTable
    {
        public const string CCNID_FLD = "CCNID";
        public const string CODE_FLD = "Code";
        public const string DEPARTMENTID_FLD = "DepartmentID";
        public const string EMPLOYEEID_FLD = "EmployeeID";
        public const string POSTDATE_FLD = "PostDate";
        public const string REMINDMASTERID_FLD = "RemindMasterID";
        public const string TABLE_NAME = "MST_RemindMaster";
        public const string USERID_FLD = "UserID";
    }

    public sealed class MST_PartyContactTable
    {
        public const string CODE_FLD = "Code";
        public const string DATEOFBIRTH_FLD = "DateOfBirth";
        public const string DEPARTMENT_FLD = "Department";
        public const string DEPARTMENTID_FLD = "DepartmentID";
        public const string EMAIL_FLD = "Email";
        public const string EXT_FLD = "Ext";
        public const string FAX_FLD = "Fax";
        public const string HOBBIES_FLD = "Hobbies";
        public const string JOBTITLE_FLD = "JobTitle";
        public const string MEMO_FLD = "Memo";
        public const string NAME_FLD = "Name";
        public const string PARTYCONTACTID_FLD = "PartyContactID";
        public const string PARTYID_FLD = "PartyID";
        public const string PARTYLOCATIONID_FLD = "PartyLocationID";
        public const string PERSONALITY_FLD = "Personality";
        public const string PHONE_FLD = "Phone";
        public const string SEX_FLD = "Sex";
        public const string TABLE_NAME = "MST_PartyContact";
        public const string TITLE_FLD = "Title";
    }

    public sealed class MST_PartyLocationTable
    {
        public const string ADDRESS_FLD = "Address";
        public const string CITYID_FLD = "CityID";
        public const string CODE_FLD = "Code";
        public const string COUNTRYID_FLD = "CountryID";
        public const string DELETEREASON_FLD = "DeleteReason";
        public const string DESCRIPTION_FLD = "Description";
        public const string PARTYID_FLD = "PartyID";
        public const string PARTYLOCATIONID_FLD = "PartyLocationID";
        public const string STATE_FLD = "State";
        public const string TABLE_NAME = "MST_PartyLocation";
        public const string ZIPPOST_FLD = "ZipPost";
    }

    public sealed class MST_PauseTable
    {
        public const string CODE_FLD = "Code";
        public const string DESCRIPTION_FLD = "Description";
        public const string PAUSEID_FLD = "PauseID";
        public const string TABLE_NAME = "MST_Pause";
    }

    public sealed class MST_PaymentTermTable
    {
        public const string CCNID_FLD = "CCNID";
        public const string CODE_FLD = "Code";
        public const string DESCRIPTION_FLD = "Description";
        public const string DISCOUNTDATE_FLD = "DiscountDate";
        public const string DISCOUNTPERCENT_FLD = "DiscountPercent";
        public const string NETDUEDATE_FLD = "NetDueDate";
        public const string PAYMENTTERMID_FLD = "PaymentTermID";
        public const string TABLE_NAME = "MST_PaymentTerm";
        public const string TYPE_FLD = "Type";
    }

    public sealed class MST_ReasonTable
    {
        public const string CCNID_FLD = "CCNID";
        public const string CODE_FLD = "Code";
        public const string DESCRIPTION_FLD = "Description";
        public const string REASONID_FLD = "ReasonID";
        public const string TABLE_NAME = "MST_Reason";
    }

    public sealed class MST_TransactionHistoryTable
    {
        public const string BINCOMMITQUANTITY_FLD = "BinCommitQuantity";
        public const string BINID_FLD = "BinID";
        public const string BINOHQUANTITY_FLD = "BinOHQuantity";
        public const string BUYSELLCOST_FLD = "BuySellCost";
        public const string CCNID_FLD = "CCNID";
        public const string COMMENT_FLD = "Comment";
        public const string COST_FLD = "StdCost";
        public const string CURRENCYID_FLD = "CurrencyID";
        public const string EXCHANGERATE_FLD = "ExchangeRate";
        public const string INSPSTATUS_FLD = "InspStatus";
        public const string ISSUEPUROSEID_FLD = "IssuePurposeID";
        public const string LOCATIONCOMMITQUANTITY_FLD = "LocationCommitQuantity";
        public const string LOCATIONID_FLD = "LocationID";
        public const string LOCATIONOHQUANTITY_FLD = "LocationOHQuantity";
        public const string LOT_FLD = "Lot";
        public const string MASLOCCOMMITQUANTITY_FLD = "MasLocCommitQuantity";
        public const string MASLOCOHQUANTITY_FLD = "MasLocOHQuantity";
        public const string MASTERLOCATIONID_FLD = "MasterLocationID";
        public const string NEWAVGCOST_FLD = "NewAvgCost";
        public const string OLDAVGCOST_FLD = "OldAvgCost";
        public const string PARTYID_FLD = "PartyID";
        public const string PARTYLOCATIONID_FLD = "PartyLocationID";
        public const string POSTDATE_FLD = "PostDate";
        public const string PRODUCTID_FLD = "ProductID";
        public const string QUANTITY_FLD = "Quantity";
        public const string REFDETAILID_FLD = "RefDetailID";
        public const string REFMASTERID_FLD = "RefMasterID";
        public const string SERIAL_FLD = "Serial";
        public const string STOCKUMID_FLD = "StockUMID";
        public const string TABLE_NAME = "MST_TransactionHistory";
        public const string TRANSACTIONHISTORYID_FLD = "TransactionHistoryID";
        public const string TRANSDATE_FLD = "TransDate";
        public const string TRANTYPEID_FLD = "TranTypeID";
        public const string USERNAME_FLD = "UserName";
    }

    public sealed class MST_TranTypeTable
    {
        public const string CODE_FLD = "Code";
        public const string DESCRIPTION_FLD = "Description";
        public const string TABLE_NAME = "MST_TranType";
        public const string TRANTYPEID_FLD = "TranTypeID";
        public const string TYPE_FLD = "Type";
    }

    public sealed class MST_UMRateTable
    {
        public const string DESCRIPTION_FLD = "Description";
        public const string SCALE_FLD = "Scale";
        public const string TABLE_NAME = "MST_UMRate";
        public const string UMRATEID_FLD = "UMRateID";
        public const string UNITOFMEASUREINID_FLD = "UnitOfMeasureInID";
        public const string UNITOFMEASUREOUTID_FLD = "UnitOfMeasureOutID";
    }

    public sealed class MST_UnitOfMeasureTable
    {
        public const string CODE_FLD = "Code";
        public const string DESCRIPTION_FLD = "Description";
        public const string TABLE_NAME = "MST_UnitOfMeasure";
        public const string UNITOFMEASUREID_FLD = "UnitOfMeasureID";
    }

    public sealed class MST_WorkCenterTable
    {
        public const string CCNID_FLD = "CCNID";
        public const string CODE_FLD = "Code";
        public const string DESCRIPTION_FLD = "Description";
        public const string ISMAIN_FLD = "IsMain";
        public const string NAME_FLD = "Name";
        public const string PRODUCTIONLINEID_FLD = "ProductionLineID";
        public const string RUNDCP_FLD = "RunDCP";
        public const string SETMINPRODUCE_FLD = "SetMinProduce";
        public const string TABLE_NAME = "MST_WorkCenter";
        public const string WORKCENTERID_FLD = "WorkCenterID";
    }

    public sealed class MST_WorkCenterCapacityTable
    {
        public const string CREWSIZE_FLD = "CrewSize";
        public const string EFFECTIVEBEGINDATE_FLD = "EffectiveBeginDate";
        public const string EFFECTIVEENDDATE_FLD = "EffectiveEndDate";
        public const string LABOREFFICIENCYFACTOR_FLD = "LaborEfficiencyFactor";
        public const string LABORHOURSPERDAY_FLD = "LaborHoursPerday";
        public const string LABORSHIFT_FLD = "LaborShift";
        public const string MACHINEEFFICIENCYFACTOR_FLD = "MachineEfficiencyFactor";
        public const string MACHINEHOURSPERDAY_FLD = "MachineHoursPerday";
        public const string MACHINEQTY_FLD = "MachineQty";
        public const string MACHINESHIFT_FLD = "MachineShift";
        public const string TABLE_NAME = "MST_WorkCenterCapacity";
        public const string WORKCENTERCAPACITYID_FLD = "WorkCenterCapacityID";
    }

    public sealed class MST_WorkingDayDetailTable
    {
        public const string COMMENT_FLD = "Comment";
        public const string OFFDAY_FLD = "OffDay";
        public const string TABLE_NAME = "MST_WorkingDayDetail";
        public const string WORKINGDAYDETAILID_FLD = "WorkingDayDetailID";
        public const string WORKINGDAYMASTERID_FLD = "WorkingDayMasterID";
    }

    public sealed class MST_WorkingDayMasterTable
    {
        public const string CCNID_FLD = "CCNID";
        public const string FRI_FLD = "Fri";
        public const string MON_FLD = "Mon";
        public const string SAT_FLD = "Sat";
        public const string SUN_FLD = "Sun";
        public const string TABLE_NAME = "MST_WorkingDayMaster";
        public const string THU_FLD = "Thu";
        public const string TUE_FLD = "Tue";
        public const string WED_FLD = "Wed";
        public const string WORKINGDAYMASTERID_FLD = "WorkingDayMasterID";
        public const string YEAR_FLD = "Year";
    }

    public sealed class MTR_ACDSDetailTable
    {
        public const string ACDSDETAILID_FLD = "ACDSDetailID";
        public const string ACDSOPTIONDETAILID_FLD = "ACDSOptionDetailID";
        public const string PERCENTAGE_FLD = "Percentage";
        public const string PRODUCTID_FLD = "ProductID";
        public const string TABLE_NAME = "MTR_ACDSDetail";
    }

    public sealed class MTR_ACDSOptionDetailTable
    {
        public const string ACDSOPTIONDETAILID_FLD = "ACDSOptionDetailID";
        public const string ACDSOPTIONMASTERID_FLD = "ACDSOptionMasterID";
        public const string COSTELEMENTID_FLD = "CostElementID";
        public const string DISTRIBUTIONMETHOD_FLD = "DistributionMethod";
        public const string TABLE_NAME = "MTR_ACDSOptionDetail";
        public const string VALUE_FLD = "Value";
        public const string WORKCENTERID_FLD = "WorkCenterID";
    }

    public sealed class MTR_ACDSOptionMasterTable
    {
        public const string ACDSOPTIONMASTERID_FLD = "ACDSOptionMasterID";
        public const string CCNID_FLD = "CCNID";
        public const string FROMDATE_FLD = "FromDate";
        public const string PERIOD_FLD = "Period";
        public const string ROLLUP_FLD = "RollUp";
        public const string TABLE_NAME = "MTR_ACDSOptionMaster";
        public const string TODATE_FLD = "ToDate";
    }

    public sealed class MTR_CPOTable
    {
        public const string CCNID_FLD = "CCNID";
        public const string CONVERTED_FLD = "Converted";
        public const string CPOID_FLD = "CPOID";
        public const string DCPUPDATED_FLD = "DCPUpdated";
        public const string DEMANDQUANTITY_FLD = "DemandQuantity";
        public const string DUEDATE_FLD = "DueDate";
        public const string ISMPS_FLD = "IsMPS";
        public const string ISSAFETYSTOCK_FLD = "IsSafetyStock";
        public const string MASTERLOCATIONID_FLD = "MasterLocationID";
        public const string MPSCYCLEOPTIONMASTERID_FLD = "MPSCycleOptionMasterID";
        public const string MRPCYCLEOPTIONMASTERID_FLD = "MRPCycleOptionMasterID";
        public const string NETAVAILABLEQUANTITY_FLD = "NetAvailableQuantity";
        public const string PARENTCPOID_FLD = "ParentCPOID";
        public const string POGENERATEDID_FLD = "POGeneratedID";
        public const string PRODUCTID_FLD = "ProductID";
        public const string QUANTITY_FLD = "Quantity";
        public const string REFDETAILID_FLD = "RefDetailID";
        public const string REFMASTERID_FLD = "RefMasterID";
        public const string REFTYPE_FLD = "RefType";
        public const string STARTDATE_FLD = "StartDate";
        public const string STOCKUMID_FLD = "StockUMID";
        public const string SUPPLYQUANTITY_FLD = "SupplyQuantity";
        public const string TABLE_NAME = "MTR_CPO";
        public const string WOGENERATEDID_FLD = "WOGeneratedID";
    }

    public sealed class MTR_ItemActualCostTable
    {
        public const string ACDSOPTIONMASTERID_FLD = "ACDSOptionMasterID";
        public const string ITEMACTUALCOSTID_FLD = "ItemActualCostID";
        public const string MATERIALCOST_FLD = "MaterialCost";
        public const string PRODUCTID_FLD = "ProductID";
        public const string QUANTITY_FLD = "Quantity";
        public const string TABLE_NAME = "MTR_ItemActualCost";
        public const string WORKCENTERID_FLD = "WorkCenterID";
    }

    public sealed class MTR_MPSCycleOptionDetailTable
    {
        public const string DEMANDWO_FLD = "DemandWO";
        public const string MASTERLOCATIONID_FLD = "MasterLocationID";
        public const string MPSCYCLEOPTIONDETAILID_FLD = "MPSCycleOptionDetailID";
        public const string MPSCYCLEOPTIONMASTERID_FLD = "MPSCycleOptionMasterID";
        public const string ONHAND_FLD = "OnHand";
        public const string PURCHASEORDER_FLD = "PurchaseOrder";
        public const string SAFETYSTOCK_FLD = "SafetyStock";
        public const string SALEORDER_FLD = "SaleOrder";
        public const string SUPPLYWO_FLD = "SupplyWO";
        public const string TABLE_NAME = "MTR_MPSCycleOptionDetail";
    }

    public sealed class MTR_MPSCycleOptionMasterTable
    {
        public const string ASOFDATE_FLD = "AsOfDate";
        public const string CCNID_FLD = "CCNID";
        public const string CYCLE_FLD = "Cycle";
        public const string DESCRIPTION_FLD = "Description";
        public const string GROUPBY_FLD = "GroupBy";
        public const string MPSCYCLEOPTIONMASTERID_FLD = "MPSCycleOptionMasterID";
        public const string MPSGENDATE_FLD = "MPSGenDate";
        public const string PLANHORIZON_FLD = "PlanHorizon";
        public const string TABLE_NAME = "MTR_MPSCycleOptionMaster";
    }

    public sealed class MTR_MRPCycleOptionDetailTable
    {
        public const string DEMANDWO_FLD = "DemandWO";
        public const string MASTERLOCATIONID_FLD = "MasterLocationID";
        public const string MRPCYCLEOPTIONDETAILID_FLD = "MRPCycleOptionDetailID";
        public const string MRPCYCLEOPTIONMASTERID_FLD = "MRPCycleOptionMasterID";
        public const string ONHAND_FLD = "OnHand";
        public const string PURCHASEORDER_FLD = "PurchaseOrder";
        public const string SALEORDER_FLD = "SaleOrder";
        public const string TABLE_NAME = "MTR_MRPCycleOptionDetail";
    }

    public sealed class MTR_MRPCycleOptionMasterTable
    {
        public const string ASOFDATE_FLD = "AsOfDate";
        public const string CCNID_FLD = "CCNID";
        public const string CYCLE_FLD = "Cycle";
        public const string DAYSBEFOREASOFDATE_FLD = "DaysBeforeAsOfDate";
        public const string DESCRIPTION_FLD = "Description";
        public const string INCLUDEDREMAINPO_FLD = "IncludedRemainPO";
        public const string INCLUDEDRETURNTOVENDOR_FLD = "IncludedReturnToVendor";
        public const string MPSCYCLEOPTIONMASTERID_FLD = "MPSCycleOptionMasterID";
        public const string MPSGENDATE_FLD = "MPSGenDate";
        public const string MRPCYCLEOPTIONMASTERID_FLD = "MRPCycleOptionMasterID";
        public const string PLANHORIZON_FLD = "PlanHorizon";
        public const string TABLE_NAME = "MTR_MRPCycleOptionMaster";
        public const string DAYBEFOREASOFDATE_FLD = "DaysBeforeAsOfDate";
    }

    public sealed class PAY_AdditionSalaryDetailTable
    {
        public const string ADDITIONSALARYDETAILID_FLD = "AdditionSalaryDetailID";
        public const string ADDITIONSALARYMASTERID_FLD = "AdditionSalaryMasterID";
        public const string APPROVALAMOUNT_FLD = "ApprovalAmount";
        public const string LINE_FLD = "Line";
        public const string PRODUCTID_FLD = "ProductID";
        public const string REASON_FLD = "Reason";
        public const string REQUESTAMOUNT_FLD = "RequestAmount";
        public const string REQUESTDATE_FLD = "RequestDate";
        public const string TABLE_NAME = "PAY_AdditionSalaryDetail";
    }

    public sealed class PAY_AdditionSalaryMasterTable
    {
        public const string ADDITIONSALARYMASTERID_FLD = "AdditionSalaryMasterID";
        public const string POSTDATE_FLD = "PostDate";
        public const string PRODUCTIONLINEID_FLD = "ProductionLineID";
        public const string SALEORDERDETAILID_FLD = "SaleOrderDetailID";
        public const string TABLE_NAME = "PAY_AdditionSalaryMaster";
        public const string TRANSNO_FLD = "TransNo";
    }

    public sealed class PAY_EstimateDetailTable
    {
        public const string ADVANCEAMOUNT_FLD = "AdvanceAmount";
        public const string ADVANCELIMIT_FLD = "AdvanceLimit";
        public const string AMOUNT_FLD = "Amount";
        public const string APPROVALAMOUNT_FLD = "ApprovalAmount";
        public const string APPROVEDADVANCEAMOUNT_FLD = "ApprovedAdvanceAmount";
        public const string APPROVEDREQUESTAMOUNT_FLD = "ApprovedRequestAmount";
        public const string AVAILABLEAMOUNT_FLD = "AvailableAmount";
        public const string ESTIMATEDETAILID_FLD = "EstimateDetailID";
        public const string ESTIMATEMASTERID_FLD = "EstimateMasterID";
        public const string PRODUCTIONLINEID_FLD = "ProductionLineID";
        public const string REQUESTAMOUNT_FLD = "RequestAmount";
        public const string REQUESTLIMIT_FLD = "RequestLimit";
        public const string TABLE_NAME = "PAY_EstimateDetail";
        public const string TOTALTIME_FLD = "TotalTime";
        public const string USERID_FLD = "UserID";
    }

    public sealed class PAY_EstimateMasterTable
    {
        public const string COMPLETED_FLD = "Completed";
        public const string ESTIMATEMASTERID_FLD = "EstimateMasterID";
        public const string SALEORDERDETAILID_FLD = "SaleOrderDetailID";
        public const string TABLE_NAME = "PAY_EstimateMaster";
        public const string TRANSDATE_FLD = "TransDate";
        public const string TRANSNO_FLD = "TransNo";
    }

    public sealed class PAY_PaymentRequestDetailTable
    {
        public const string APPROVALAMOUNT_FLD = "ApprovalAmount";
        public const string PAYMENTREQUESTDETAILID_FLD = "PaymentRequestDetailID";
        public const string PAYMENTREQUESTMASTERID_FLD = "PaymentRequestMasterID";
        public const string PRODUCTIONLINEID_FLD = "ProductionLineID";
        public const string REQUESTAMOUNT_FLD = "RequestAmount";
        public const string REQUESTDATE_FLD = "RequestDate";
        public const string TABLE_NAME = "PAY_PaymentRequestDetail";
        public const string USERID_FLD = "UserID";
    }

    public sealed class PAY_PaymentRequestMasterTable
    {
        public const string PAYMENTREQUESTMASTERID_FLD = "PaymentRequestMasterID";
        public const string PRODUCTIONLINEID_FLD = "ProductionLineID";
        public const string REQUESTTYPE_FLD = "RequestType";
        public const string SALEORDERDETAILID_FLD = "SaleOrderDetailID";
        public const string TABLE_NAME = "PAY_PaymentRequestMaster";
        public const string TRANSDATE_FLD = "TransDate";
        public const string TRANSNO_FLD = "TransNo";
    }

    public sealed class PAY_PayrollParamTable
    {
        public const string FROMDATE_FLD = "FromDate";
        public const string PAIDPERHOUR_FLD = "PaidPerHour";
        public const string PAYROLLPARAMID_FLD = "PayrollParamID";
        public const string STATUS_FLD = "Status";
        public const string TABLE_NAME = "PAY_PayrollParam";
        public const string TODATE_FLD = "ToDate";
    }

    public sealed class PAY_PersonalIncomeTariffTable
    {
        public const string CCNID_FLD = "CCNID";
        public const string INCOMEFROM_FLD = "IncomeFrom";
        public const string INCOMETO_FLD = "IncomeTo";
        public const string PERSONALINCOMETARIFFID_FLD = "PersonalIncomeTariffID";
        public const string PERSONALINCOMETAX_FLD = "PersonalIncomeTax";
        public const string TABLE_NAME = "PAY_PersonalIncomeTariff";
    }

    public sealed class PAY_ProductionNormTable
    {
        public const string COMPONENTID_FLD = "ComponentID";
        public const string FROMDATE_FLD = "FromDate";
        public const string PRODUCTID_FLD = "ProductID";
        public const string PRODUCTIONLINEID_FLD = "ProductionLineID";
        public const string PRODUCTIONNORMID_FLD = "ProductionNormID";
        public const string SALEORDERDETAILID_FLD = "SaleOrderDetailID";
        public const string TABLE_NAME = "PAY_ProductionNorm";
        public const string TODATE_FLD = "ToDate";
        public const string TOTALTIME_FLD = "TotalTime";
        public const string USERID_FLD = "UserID";
    }

    public sealed class PAY_SalaryCollectiveTable
    {
        public const string ALLOWANCE_FLD = "Allowance";
        public const string CCNID_FLD = "CCNID";
        public const string COLLECTIVEDATE_FLD = "CollectiveDate";
        public const string DEPARTMENTID_FLD = "DepartmentID";
        public const string EMPLOYEEID_FLD = "EmployeeID";
        public const string MONTHLYSALARY_FLD = "MonthlySalary";
        public const string OVERTIME_FLD = "Overtime";
        public const string PRODUCTIONLINEID_FLD = "ProductionLineID";
        public const string SALARYCOLLECTIVEID_FLD = "SalaryCollectiveID";
        public const string SALARYFACTOR_FLD = "SalaryFactor";
        public const string SALEBONUS_FLD = "SaleBonus";
        public const string TABLE_NAME = "PAY_SalaryCollective";
        public const string TOTALAMOUNT_FLD = "TotalAmount";
        public const string TOTALTAX_FLD = "TotalTax";
        public const string USERID_FLD = "UserID";
    }

    public sealed class PAY_SalaryProfileDetailTable
    {
        public const string BASICSALARY_FLD = "BasicSalary";
        public const string DAILYSALARY_FLD = "DailySalary";
        public const string EFFECTDATE_FLD = "EffectDate";
        public const string SALARYFACTOR_FLD = "SalaryFactor";
        public const string SALARYLEVEL_FLD = "SalaryLevel";
        public const string SALARYPROFILEDETAILID_FLD = "SalaryProfileDetailID";
        public const string SALARYPROFILEMASTERID_FLD = "SalaryProfileMasterID";
        public const string STATUS_FLD = "Status";
        public const string TABLE_NAME = "PAY_SalaryProfileDetail";
    }

    public sealed class PAY_SalaryProfileMasterTable
    {
        public const string CCNID_FLD = "CCNID";
        public const string CONTRACTTYPE_FLD = "ContractType";
        public const string DEPARTMENTID_FLD = "DepartmentID";
        public const string EMPLOYEEID_FLD = "EmployeeID";
        public const string PRODUCTIONLINEID_FLD = "ProductionLineID";
        public const string SALARYPROFILEMASTERID_FLD = "SalaryProfileMasterID";
        public const string TABLE_NAME = "PAY_SalaryProfileMaster";
        public const string USERID_FLD = "UserID";
    }

    public sealed class PAY_SpecialAllowanceTable
    {
        public const string ALLOWANCETYPE_FLD = "AllowanceType";
        public const string AMOUNT_FLD = "Amount";
        public const string EMPLOYEEID_FLD = "EmployeeID";
        public const string FROMDATE_FLD = "FromDate";
        public const string SPECIALALLOWANCEID_FLD = "SpecialAllowanceID";
        public const string TABLE_NAME = "PAY_SpecialAllowance";
        public const string TODATE_FLD = "ToDate";
    }

    public sealed class PAY_TaxDeductibleTable
    {
        public const string AMOUNT_FLD = "Amount";
        public const string CCNID_FLD = "CCNID";
        public const string DEDUCTIBLEITEM_FLD = "DeductibleItem";
        public const string DEPARTMENTID_FLD = "DepartmentID";
        public const string EMPLOYEEID_FLD = "EmployeeID";
        public const string PRODUCTIONLINEID_FLD = "ProductionLineID";
        public const string TABLE_NAME = "PAY_TaxDeductible";
        public const string TAXDEDUCTIBLEID_FLD = "TaxDeductibleID";
    }

    public sealed class PO_AdditionChargesTable
    {
        public const string ADDCHARGEID_FLD = "AddChargeID";
        public const string ADDITIONCHARGESID_FLD = "AdditionChargesID";
        public const string AMOUNT_FLD = "Amount";
        public const string PURCHASEORDERDETAILID_FLD = "PurchaseOrderDetailID";
        public const string PURCHASEORDERMASTERID_FLD = "PurchaseOrderMasterID";
        public const string QUANTITY_FLD = "Quantity";
        public const string REASONID_FLD = "ReasonID";
        public const string TABLE_NAME = "PO_AdditionCharges";
        public const string TOTALAMOUNT_FLD = "TotalAmount";
        public const string UNITPRICE_FLD = "UnitPrice";
        public const string VATAMOUNT_FLD = "VatAmount";
    }

    public sealed class PO_DeliveryScheduleTable
    {
        public const string ADJUSTMENT_FLD = "Adjustment";
        public const string CANCELDELIVERY_FLD = "CancelDelivery";
        public const string DELIVERYLINE_FLD = "DeliveryLine";
        public const string DELIVERYQUANTITY_FLD = "DeliveryQuantity";
        public const string DELIVERYSCHEDULEID_FLD = "DeliveryScheduleID";
        public const string PURCHASEORDERDETAILID_FLD = "PurchaseOrderDetailID";
        public const string RECEIVEDQUANTITY_FLD = "ReceivedQuantity";
        public const string SCHEDULEDATE_FLD = "ScheduleDate";
        public const string STARTDATE_FLD = "StartDate";
        public const string TABLE_NAME = "PO_DeliverySchedule";
    }

    public sealed class PO_FunctionVendorReferenceTable
    {
        public const string CAPACITY_FLD = "Capacity";
        public const string CAPACITYPERIOD_FLD = "CapacityPeriod";
        public const string CARRIERID_FLD = "CarrierID";
        public const string CCNID_FLD = "CCNID";
        public const string CURRENCYID_FLD = "CurrencyID";
        public const string ENDDATE_FLD = "EndDate";
        public const string FIXEDLEADTIME_FLD = "FixedLeadTime";
        public const string FUNCTIONID_FLD = "FunctionID";
        public const string FUNCTIONVENDORREFERENCEID_FLD = "FunctionVendorReferenceID";
        public const string MINORDERQUANTITY_FLD = "MinOrderQuantity";
        public const string PRICE_FLD = "Price";
        public const string QUANTITY_FLD = "Quantity";
        public const string TABLE_NAME = "PO_FunctionVendorReference";
        public const string VARIANCELEADTIME_FLD = "VarianceLeadTime";
        public const string VENDORFUNCTION_FLD = "VendorFunction";
        public const string VENDORID_FLD = "VendorID";
        public const string VENDORLOCID_FLD = "VendorLocID";
    }

    public sealed class PO_InvoiceDetailTable
    {
        public const string BEFOREVATAMOUNT_FLD = "BeforeVATAmount";
        public const string CIFAMOUNT_FLD = "CIFAmount";
        public const string CIPAMOUNT_FLD = "CIPAmount";
        public const string DELIVERYSCHEDULEID_FLD = "DeliveryScheduleID";
        public const string IMPORTTAX_FLD = "ImportTax";
        public const string IMPORTTAXAMOUNT_FLD = "ImportTaxAmount";
        public const string INLAND_FLD = "Inland";
        public const string INVOICEDETAILID_FLD = "InvoiceDetailID";
        public const string INVOICELINE_FLD = "InvoiceLine";
        public const string INVOICEMASTERID_FLD = "InvoiceMasterID";
        public const string INVOICEQUANTITY_FLD = "InvoiceQuantity";
        public const string INVOICEUMID_FLD = "InvoiceUMID";
        public const string NGQUANTITY_FLD = "NGQuantity";
        public const string NOTE_FLD = "Note";
        public const string PRODUCTID_FLD = "ProductID";
        public const string PURCHASEORDERDETAILID_FLD = "PurchaseOrderDetailID";
        public const string PURCHASEORDERMASTERID_FLD = "PurchaseOrderMasterID";
        public const string QASTATUS_FLD = "QAStatus";
        public const string TABLE_NAME = "PO_InvoiceDetail";
        public const string UNITPRICE_FLD = "UnitPrice";
        public const string VAT_FLD = "VAT";
        public const string VATAMOUNT_FLD = "VATAmount";
    }

    public sealed class PO_InvoiceMasterTable
    {
        public const string BLDATE_FLD = "BLDate";
        public const string BLNUMBER_FLD = "BLNumber";
        public const string CARRIERID_FLD = "CarrierID";
        public const string CCNID_FLD = "CCNID";
        public const string CURRENCYID_FLD = "CurrencyID";
        public const string DECLARATIONDATE_FLD = "DeclarationDate";
        public const string DELIVERYTERMID_FLD = "DeliveryTermID";
        public const string EXCHANGERATE_FLD = "ExchangeRate";
        public const string EXCHANGERATECC_FLD = "ExchangeRateCC";
        public const string EXPENSETYPE_FLD = "ExpenseType";
        public const string INFORMDATE_FLD = "InformDate";
        public const string INVOICEMASTERID_FLD = "InvoiceMasterID";
        public const string INVOICENO_FLD = "InvoiceNo";
        public const string INVOICETYPE_FLD = "InvoiceType";
        public const string LASTCHANGE_FLD = "LastChange";
        public const string PARTYID_FLD = "PartyID";
        public const string PAYMENTAMOUNT_FLD = "PaymentAmount";
        public const string PAYMENTTERMID_FLD = "PaymentTermID";
        public const string POSTDATE_FLD = "PostDate";
        public const string PURCHASETYPEID_FLD = "PurchaseTypeID";
        public const string TABLE_NAME = "PO_InvoiceMaster";
        public const string TAXDECLARATIONNUMBER_FLD = "TaxDeclarationNumber";
        public const string TAXINFORMNUMBER_FLD = "TaxInformNumber";
        public const string TOTALBEFOREVATAMOUNT_FLD = "TotalBeforeVATAmount";
        public const string TOTALCIFAMOUNT_FLD = "TotalCIFAmount";
        public const string TOTALCIPAMOUNT_FLD = "TotalCIPAmount";
        public const string TOTALIMPORTTAX_FLD = "TotalImportTax";
        public const string TOTALINLANDAMOUNT_FLD = "TotalInlandAmount";
        public const string TOTALVATAMOUNT_FLD = "TotalVATAmount";
        public const string USERNAME_FLD = "UserName";
    }

    public sealed class PO_ItemVendorReferenceTable
    {
        public const string BUYINGUM_FLD = "BuyingUM";
        public const string CAPACITY_FLD = "Capacity";
        public const string CAPACITYPERIOD_FLD = "CapacityPeriod";
        public const string CARRIERID_FLD = "CarrierID";
        public const string CCNID_FLD = "CCNID";
        public const string CURRENCYID_FLD = "CurrencyID";
        public const string ENDDATE_FLD = "EndDate";
        public const string FIXEDLEADTIME_FLD = "FixedLeadTime";
        public const string ITEMVENDORREFERENCEID_FLD = "ItemVendorReferenceID";
        public const string MINORDERQUANTITY_FLD = "MinOrderQuantity";
        public const string PARTYID_FLD = "PartyID";
        public const string PRICE_FLD = "Price";
        public const string PRODUCTID_FLD = "ProductID";
        public const string QASTATUS_FLD = "QAStatus";
        public const string QUANTITY_FLD = "Quantity";
        public const string TABLE_NAME = "PO_ItemVendorReference";
        public const string VARIANCELEADTIME_FLD = "VarianceLeadTime";
        public const string VENDORITEM_FLD = "VendorItem";
        public const string VENDORITEMDESCRIPTION_FLD = "VendorItemDescription";
        public const string VENDORITEMREVISION_FLD = "VendorItemRevision";
        public const string VENDORLOCID_FLD = "VendorLocID";
    }

    public sealed class PO_PurchaseOrderDetailTable
    {
        public const string ADJUSTMENT1_FLD = "Adjustment1";
        public const string ADJUSTMENT2_FLD = "Adjustment2";
        public const string APPROVALDATE_FLD = "ApprovalDate";
        public const string APPROVERID_FLD = "ApproverID";
        public const string BUYINGUMID_FLD = "BuyingUMID";
        public const string CLOSED_FLD = "Closed";
        public const string DISCOUNTAMOUNT_FLD = "DiscountAmount";
        public const string IMPORTTAX_FLD = "ImportTax";
        public const string IMPORTTAXAMOUNT_FLD = "ImportTaxAmount";
        public const string LINE_FLD = "Line";
        public const string NETAMOUNT_FLD = "NetAmount";
        public const string ORDERQUANTITY_FLD = "OrderQuantity";
        public const string PRODUCTID_FLD = "ProductID";
        public const string PURCHASEORDERDETAILID_FLD = "PurchaseOrderDetailID";
        public const string PURCHASEORDERMASTERID_FLD = "PurchaseOrderMasterID";
        public const string REOPEN_FLD = "ReOpen";
        public const string REQUIREDDATE_FLD = "RequiredDate";
        public const string SPECIALTAX_FLD = "SpecialTax";
        public const string SPECIALTAXAMOUNT_FLD = "SpecialTaxAmount";
        public const string STOCKUMID_FLD = "StockUMID";
        public const string TABLE_NAME = "PO_PurchaseOrderDetail";
        public const string TOTALAMOUNT_FLD = "TotalAmount";
        public const string TOTALDELIVERY_FLD = "TotalDelivery";
        public const string UMRATE_FLD = "UMRate";
        public const string UNITPRICE_FLD = "UnitPrice";
        public const string VAT_FLD = "VAT";
        public const string VATAMOUNT_FLD = "VATAmount";
        public const string VENDORITEM_FLD = "VendorItem";
        public const string VENDORREVISION_FLD = "VendorRevision";
        public const string L01_FLD = "L01";
        public const string L02_FLD = "L02";
        public const string L03_FLD = "L03";
        public const string DESCRIPTION_FLD = "DescriptionXH";
    }
    public sealed class IV_OutsideTransactionMasterTable
    {
        public const string TABLE_NAME = "IV_OutsideTransactionMaster";
        public const string OUTSIDETRANSACTIONMASTERID_FLD = "OutsideTransactionMasterID";
        public const string TRANNO_FLD = "TranNo";
        public const string TRANDATE_FLD = "TranDate";
        public const string TRANTYPEID_FLD = "TranTypeID";
        public const string LOCATIONID_FLD = "LocationID";
        public const string SALEORDERMASTERID_FLD = "SaleOrderMasterID";
        public const string PARTYID_FLD = "PartyID";
        public const string USERID_FLD = "UserID";
        public const string COMMENT_FLD = "Comment";
    }
    public sealed class IV_OutsideTransactionDetailTable
    {
        public const string TABLE_NAME = "IV_OutsideTransactionDetail";
        public const string OUTSIDETRANSACTIONMASTERID_FLD = "OutsideTransactionMasterID";
        public const string OUTSIDETRANSACTIONDETAILID_FLD = "OutsideTransactionDetailID";
        public const string PRODUCTID_FLD = "ProductID";
        public const string QTY_FLD = "Qty";
        public const string COMMENT = "Comment";
    }

    public sealed class PO_PurchaseOrderMasterTable
    {
        public const string BUYERID_FLD = "BuyerID";
        public const string CARRIERID_FLD = "CarrierID";
        public const string CCNID_FLD = "CCNID";
        public const string CODE_FLD = "Code";
        public const string COMMENT_FLD = "Comment";
        public const string CURRENCYID_FLD = "CurrencyID";
        public const string DELIVERYTERMSID_FLD = "DeliveryTermsID";
        public const string DISCOUNTTERMID_FLD = "DiscountTermID";
        public const string EXCHANGERATE_FLD = "ExchangeRate";
        public const string EXCHANGERATECC_FLD = "ExchangeRateCC";
        public const string IMPORTTAX_FLD = "ImportTax";
        public const string INVTOLOCID_FLD = "InvToLocID";
        public const string LASTCHANGE_FLD = "LastChange";
        public const string MAKERID_FLD = "MakerID";
        public const string MAKERLOCATIONID_FLD = "MakerLocationID";
        public const string MASTERLOCATIONID_FLD = "MasterLocationID";
        public const string ORDERDATE_FLD = "OrderDate";
        public const string PARTYCONTACTID_FLD = "PartyContactID";
        public const string PARTYID_FLD = "PartyID";
        public const string PAUSEID_FLD = "PauseID";
        public const string PAYMENTTERMSID_FLD = "PaymentTermsID";
        public const string POREVISION_FLD = "PORevision";
        public const string PRICINGTYPEID_FLD = "PricingTypeID";
        public const string PRIORITY_FLD = "Priority";
        public const string PURCHASEORDERMASTERID_FLD = "PurchaseOrderMasterID";
        public const string PURCHASETYPEID_FLD = "PurchaseTypeID";
        public const string RECCOMPLETED_FLD = "RecCompleted";
        public const string REFERENCENO_FLD = "ReferenceNo";
        public const string REQUESTDELIVERYTIME_FLD = "RequestDeliveryTime";
        public const string SHIPTOLOCID_FLD = "ShipToLocID";
        public const string SPECIALTAX_FLD = "SpecialTax";
        public const string TABLE_NAME = "PO_PurchaseOrderMaster";
        public const string TOTALAMOUNT_FLD = "TotalAmount";
        public const string TOTALDISCOUNT_FLD = "TotalDiscount";
        public const string TOTALIMPORTTAX_FLD = "TotalImportTax";
        public const string TOTALNETAMOUNT_FLD = "TotalNetAmount";
        public const string TOTALSPECIALTAX_FLD = "TotalSpecialTax";
        public const string TOTALVAT_FLD = "TotalVAT";
        public const string USERNAME_FLD = "UserName";
        public const string VAT_FLD = "VAT";
        public const string VENDORLOCID_FLD = "VendorLocID";
        public const string VENDORSO_FLD = "VendorSO";
        public const string LCDATE_FLD = "LCDate";
        public const string LCNO_FLD = "LCNo";
        public const string ORDERFORM_FLD = "Orderform";
        
    }

    public sealed class PO_PurchaseOrderReceiptDetailTable
    {
        public const string BINID_FLD = "BinID";
        public const string BUYINGUMID_FLD = "BuyingUMID";
        public const string DELIVERYSCHEDULEID_FLD = "DeliveryScheduleID";
        public const string INVOICEDETAILID_FLD = "InvoiceDetailID";
        public const string LOCATIONID_FLD = "LocationID";
        public const string LOT_FLD = "Lot";
        public const string PRODUCTID_FLD = "ProductID";
        public const string PURCHASEORDERDETAILID_FLD = "PurchaseOrderDetailID";
        public const string PURCHASEORDERMASTERID_FLD = "PurchaseOrderMasterID";
        public const string PURCHASEORDERRECEIPTDETAILID_FLD = "PurchaseOrderReceiptDetailID";
        public const string PURCHASEORDERRECEIPTID_FLD = "PurchaseOrderReceiptID";
        public const string QASTATUS_FLD = "QAStatus";
        public const string RECEIVEQUANTITY_FLD = "ReceiveQuantity";
        public const string SERIAL_FLD = "Serial";
        public const string STOCKUMID_FLD = "StockUMID";
        public const string TABLE_NAME = "PO_PurchaseOrderReceiptDetail";
        public const string UMRATE_FLD = "UMRate";
    }

    public sealed class PO_PurchaseOrderReceiptMasterTable
    {
        public const string CCNID_FLD = "CCNID";
        public const string INVOICEMASTERID_FLD = "InvoiceMasterID";
        public const string LASTCHANGE_FLD = "LastChange";
        public const string MASTERLOCATIONID_FLD = "MasterLocationID";
        public const string POSLIPNO_FLD = "POSlipNo";
        public const string POSTDATE_FLD = "PostDate";
        public const string PRODUCTIONLINEID_FLD = "ProductionLineID";
        public const string PURCHASEORDERMASTERID_FLD = "PurchaseOrderMasterID";
        public const string PURCHASEORDERRECEIPTID_FLD = "PurchaseOrderReceiptID";
        public const string PURPOSE_FLD = "Purpose";
        public const string RECEIPTTYPE_FLD = "ReceiptType";
        public const string RECEIVENO_FLD = "ReceiveNo";
        public const string REFNO_FLD = "RefNo";
        public const string TABLE_NAME = "PO_PurchaseOrderReceiptMaster";
        public const string USERNAME_FLD = "UserName";
    }

    public sealed class PO_PurchaseTypeTable
    {
        public const string CODE_FLD = "Code";
        public const string DESCRIPTION_FLD = "Description";
        public const string PURCHASETYPEID_FLD = "PurchaseTypeID";
        public const string TABLE_NAME = "PO_PurchaseType";
    }

    public sealed class PO_ReturnToVendorDetailTable
    {
        public const string AMOUNT_FLD = "Amount";
        public const string BINID_FLD = "BinID";
        public const string BUYINGUMID_FLD = "BuyingUMID";
        public const string LINE_FLD = "Line";
        public const string LOCATIONID_FLD = "LocationID";
        public const string LOT_FLD = "Lot";
        public const string MRB_FLD = "MRB";
        public const string PRODUCTID_FLD = "ProductID";
        public const string QUANTITY_FLD = "Quantity";
        public const string REFDETAILID_FLD = "RefDetailID";
        public const string RETURNTOVENDORDETAILID_FLD = "ReturnToVendorDetailID";
        public const string RETURNTOVENDORMASTERID_FLD = "ReturnToVendorMasterID";
        public const string SERIAL_FLD = "Serial";
        public const string STOCKUMID_FLD = "StockUMID";
        public const string TABLE_NAME = "PO_ReturnToVendorDetail";
        public const string TOTALAMOUNT_FLD = "TotalAmount";
        public const string UMRATE_FLD = "UMRate";
        public const string UNITPRICE_FLD = "UnitPrice";
        public const string VATAMOUNT_FLD = "VATAmount";
        public const string VATPERCENT_FLD = "VATPercent";
    }

    public sealed class PO_ReturnToVendorMasterTable
    {
        public const string BYINVOICE_FLD = "ByInvoice";
        public const string BYPO_FLD = "ByPO";
        public const string CCNID_FLD = "CCNID";
        public const string INVOICEMASTERID_FLD = "InvoiceMasterID";
        public const string LASTCHANGE_FLD = "LastChange";
        public const string MASTERLOCATIONID_FLD = "MasterLocationID";
        public const string PARTYID_FLD = "PartyID";
        public const string POSTDATE_FLD = "PostDate";
        public const string PRODUCTIONLINEID_FLD = "ProductionLineID";
        public const string PURCHASELOCID_FLD = "PurchaseLocID";
        public const string PURCHASEORDERMASTERID_FLD = "PurchaseOrderMasterID";
        public const string RETURNTOVENDORMASTERID_FLD = "ReturnToVendorMasterID";
        public const string RTVNO_FLD = "RTVNo";
        public const string TABLE_NAME = "PO_ReturnToVendorMaster";
        public const string USERNAME_FLD = "UserName";
        public const string PRODUCTIONLINE_FLD = "ProductionLineID";
    }

    public sealed class PO_VendorDeliveryScheduleTable
    {
        public const string CCNID_FLD = "CCNID";
        public const string COMMENT_FLD = "Comment";
        public const string DELHOUR_FLD = "DelHour";
        public const string DELIVERYTYPE_FLD = "DeliveryType";
        public const string DELMIN_FLD = "DelMin";
        public const string MONTHLYDATE_FLD = "MonthlyDate";
        public const string PARTYID_FLD = "PartyID";
        public const string PRODUCTID_FLD = "ProductID";
        public const string TABLE_NAME = "PO_VendorDeliverySchedule";
        public const string UNITPRICE_FLD = "UnitPrice";
        public const string VENDORDELIVERYSCHEDULE_FLD = "VendorDeliverySchedule";
        public const string WEEKLYDAY_FLD = "WeeklyDay";
    }

    public sealed class PRO_AOScrapCostTable
    {
        public const string AOCOST01_FLD = "AOCost01";
        public const string AOCOST02_FLD = "AOCost02";
        public const string AOCOST03_FLD = "AOCost03";
        public const string AOCOST04_FLD = "AOCost04";
        public const string AOCOST05_FLD = "AOCost05";
        public const string AOCOST06_FLD = "AOCost06";
        public const string AOCOST07_FLD = "AOCost07";
        public const string AOCOST08_FLD = "AOCost08";
        public const string AOCOST09_FLD = "AOCost09";
        public const string AOCOST10_FLD = "AOCost10";
        public const string AOCOST11_FLD = "AOCost11";
        public const string AOCOST12_FLD = "AOCost12";
        public const string AOCOST13_FLD = "AOCost13";
        public const string AOCOST14_FLD = "AOCost14";
        public const string AOCOST15_FLD = "AOCost15";
        public const string AOCOST16_FLD = "AOCost16";
        public const string AOCOST17_FLD = "AOCost17";
        public const string AOCOST18_FLD = "AOCost18";
        public const string AOCOST19_FLD = "AOCost19";
        public const string AOCOST20_FLD = "AOCost20";
        public const string AOCOST21_FLD = "AOCost21";
        public const string AOSCRAPCOSTID_FLD = "AOScrapCostID";
        public const string AOSCRAPDETAILID_FLD = "AOScrapDetailID";
        public const string TABLE_NAME = "PRO_AOScrapCost";
        public const string UNITCOST01_FLD = "UnitCost01";
        public const string UNITCOST02_FLD = "UnitCost02";
        public const string UNITCOST03_FLD = "UnitCost03";
        public const string UNITCOST04_FLD = "UnitCost04";
        public const string UNITCOST05_FLD = "UnitCost05";
        public const string UNITCOST06_FLD = "UnitCost06";
        public const string UNITCOST07_FLD = "UnitCost07";
        public const string UNITCOST08_FLD = "UnitCost08";
        public const string UNITCOST09_FLD = "UnitCost09";
        public const string UNITCOST10_FLD = "UnitCost10";
        public const string UNITCOST11_FLD = "UnitCost11";
        public const string UNITCOST12_FLD = "UnitCost12";
        public const string UNITCOST13_FLD = "UnitCost13";
        public const string UNITCOST14_FLD = "UnitCost14";
        public const string UNITCOST15_FLD = "UnitCost15";
        public const string UNITCOST16_FLD = "UnitCost16";
        public const string UNITCOST17_FLD = "UnitCost17";
        public const string UNITCOST18_FLD = "UnitCost18";
        public const string UNITCOST19_FLD = "UnitCost19";
        public const string UNITCOST20_FLD = "UnitCost20";
        public const string UNITCOST21_FLD = "UnitCost21";
    }

    public sealed class PRO_AOScrapDetailTable
    {
        public const string AOSCRAPDETAILID_FLD = "AOScrapDetailID";
        public const string AOSCRAPMASTERID_FLD = "AOScrapMasterID";
        public const string LINE_FLD = "Line";
        public const string PRODUCTID_FLD = "ProductID";
        public const string SCRAPQUANTITY_FLD = "ScrapQuantity";
        public const string SCRAPREASONID_FLD = "ScrapReasonID";
        public const string STOCKUMID_FLD = "StockUMID";
        public const string TABLE_NAME = "PRO_AOScrapDetail";
        public const string WORKORDERDETAILID_FLD = "WorkOrderDetailID";
        public const string WORKORDERMASTERID_FLD = "WorkOrderMasterID";
        public const string WOROUTINGID_FLD = "WORoutingID";
    }

    public sealed class PRO_AOScrapMasterTable
    {
        public const string AOSCRAPMASTERID_FLD = "AOScrapMasterID";
        public const string CCNID_FLD = "CCNID";
        public const string LASTCHANGE_FLD = "LastChange";
        public const string MASTERLOCATIONID_FLD = "MasterLocationID";
        public const string POSTDATE_FLD = "PostDate";
        public const string SCRAPNO_FLD = "ScrapNo";
        public const string TABLE_NAME = "PRO_AOScrapMaster";
        public const string USERNAME_FLD = "UserName";
    }

    public sealed class PRO_ChangeCategoryDetailTable
    {
        public const string CHANGECATEGORYDETAILID_FLD = "ChangeCategoryDetailID";
        public const string CHANGECATEGORYMASTERID_FLD = "ChangeCategoryMasterID";
        public const string PRODUCTID_FLD = "ProductID";
        public const string TABLE_NAME = "PRO_ChangeCategoryDetail";
    }

    public sealed class PRO_ChangeCategoryMasterTable
    {
        public const string CCNID_FLD = "CCNID";
        public const string CHANGECATEGORYMASTERID_FLD = "ChangeCategoryMasterID";
        public const string TABLE_NAME = "PRO_ChangeCategoryMaster";
        public const string WORKCENTERID_FLD = "WorkCenterID";
    }

    public sealed class PRO_ChangeCategoryMatrixTable
    {
        public const string CHANGECATEGORYMASTERID_FLD = "ChangeCategoryMasterID";
        public const string CHANGECATEGORYMATRIXID_FLD = "ChangeCategoryMatrixID";
        public const string CHANGETIME_FLD = "ChangeTime";
        public const string DESTPRODUCTID_FLD = "DestProductID";
        public const string SOURCEPRODUCTID_FLD = "SourceProductID";
        public const string TABLE_NAME = "PRO_ChangeCategoryMatrix";
    }

    public sealed class PRO_CheckPointTable
    {
        public const string CCNID_FLD = "CCNID";
        public const string CHECKPOINTID_FLD = "CheckPointID";
        public const string DELAYTIME_FLD = "DelayTime";
        public const string PRODUCTID_FLD = "ProductID";
        public const string SAMPLEPATTERN_FLD = "SamplePattern";
        public const string SAMPLERATE_FLD = "SampleRate";
        public const string TABLE_NAME = "PRO_CheckPoint";
        public const string WORKCENTERID_FLD = "WorkCenterID";
    }

    public sealed class PRO_ComponentScrapDetailTable
    {
        public const string AVAILABLEQUANTITY_FLD = "AvailableQuantity";
        public const string COMPONENTID_FLD = "ComponentID";
        public const string COMPONENTSCRAPDETAILID_FLD = "ComponentScrapDetailID";
        public const string COMPONENTSCRAPMASTERID_FLD = "ComponentScrapMasterID";
        public const string FROMBINID_FLD = "FromBinID";
        public const string FROMLOCATIONID_FLD = "FromLocationID";
        public const string LINE_FLD = "Line";
        public const string LOT_FLD = "Lot";
        public const string PRODUCTID_FLD = "ProductID";
        public const string SCRAPQUANTITY_FLD = "ScrapQuantity";
        public const string SCRAPREASONID_FLD = "ScrapReasonID";
        public const string SERIAL_FLD = "Serial";
        public const string TABLE_NAME = "PRO_ComponentScrapDetail";
        public const string TOBINID_FLD = "ToBinID";
        public const string TOLOCATIONID_FLD = "ToLocationID";
        public const string WORKORDERDETAILID_FLD = "WorkOrderDetailID";
        public const string WORKORDERMASTERID_FLD = "WorkOrderMasterID";
        public const string WOROUTINGID_FLD = "WORoutingID";
    }

    public sealed class PRO_ComponentScrapMasterTable
    {
        public const string CCNID_FLD = "CCNID";
        public const string COMPONENTSCRAPMASTERID_FLD = "ComponentScrapMasterID";
        public const string LASTCHANGE_FLD = "LastChange";
        public const string MASTERLOCATIONID_FLD = "MasterLocationID";
        public const string POSTDATE_FLD = "PostDate";
        public const string PRODUCTIONLINEID_FLD = "ProductionLineID";
        public const string SCRAPNO_FLD = "ScrapNo";
        public const string TABLE_NAME = "PRO_ComponentScrapMaster";
        public const string USERNAME_FLD = "UserName";
    }

    public sealed class PRO_CostElementTable
    {
        public const string CODE_FLD = "Code";
        public const string COSTELEMENTID_FLD = "CostElementID";
        public const string DESCRIPTION_FLD = "Description";
        public const string TABLE_NAME = "PRO_CostElement";
    }

    public sealed class PRO_DCOptionDetailTable
    {
        public const string DCOPTIONDETAILID_FLD = "DCOptionDetailID";
        public const string DCOPTIONMASTERID_FLD = "DCOptionMasterID";
        public const string MASTERLOCATIONID_FLD = "MasterLocationID";
        public const string TABLE_NAME = "PRO_DCOptionDetail";
        public const string WORKORDER_FLD = "WorkOrder";
    }

    public sealed class PRO_DCOptionMasterTable
    {
        public const string ASOFDATE_FLD = "AsOfDate";
        public const string CCNID_FLD = "CCNID";
        public const string CYCLE_FLD = "Cycle";
        public const string DCOPTIONMASTERID_FLD = "DCOptionMasterID";
        public const string DESCRIPTION_FLD = "Description";
        public const string GROUPBY_FLD = "GroupBy";
        public const string IGNOREMOVETIME_FLD = "IgnoreMoveTime";
        public const string INCLUDECHECKPOINT_FLD = "IncludeCheckPoint";
        public const string LASTUPDATE_FLD = "LastUpdate";
        public const string ONHAND_FLD = "OnHand";
        public const string PLANHORIZON_FLD = "PlanHorizon";
        public const string PLANNINGPERIOD_FLD = "PlanningPeriod";
        public const string SAFETYSTOCK_FLD = "SafetyStock";
        public const string SCHEDULECODE_FLD = "ScheduleCode";
        public const string SCHEDULETYPE_FLD = "ScheduleType";
        public const string TABLE_NAME = "PRO_DCOptionMaster";
        public const string USECACHEASBEGIN_FLD = "UseCacheAsBegin";
        public const string VERSION_FLD = "Version";
        public const string USECACHE_ASBEGIN_FLD = "UseCacheAsBegin";
    }

    public sealed class PRO_DCPCOptionTable
    {
        public const string ASOFDATE_FLD = "AsOfDate";
        public const string AVERAGESCHEDULE_FLD = "AverageSchedule";
        public const string CCNID_FLD = "CCNID";
        public const string CYCLE_FLD = "Cycle";
        public const string DCPCOPTIONID_FLD = "DCPCOptionID";
        public const string DESCRIPTION_FLD = "Description";
        public const string FINITESCHEDULE_FLD = "FiniteSchedule";
        public const string LASTUPDATE_FLD = "LastUpdate";
        public const string MASTERLOCATIONID_FLD = "MasterLocationID";
        public const string MPSCYCLEID_FLD = "MPSCycleID";
        public const string MRPCYCLEID_FLD = "MRPCycleID";
        public const string PLANHORIZON_FLD = "PlanHorizon";
        public const string TABLE_NAME = "PRO_DCPCOption";
        public const string WORKORDERID_FLD = "WorkOrderID";
    }
    public sealed class PRO_WOReversalMasterTable
    {
        public const string TABLE_NAME = "PRO_WOReversalMaster";
        public const string WOREVERSALMASTERID_FLD = "WOReversalMasterID";
        public const string CCNID_FLD = "CCNID";
        public const string POSTDATE_FLD = "PostDate";
        public const string MASTERLOCATIONID_FLD = "MasterLocationID";
        public const string REVERSALNO_FLD = "ReversalNo";
        public const string WORKORDERDETAILID_FLD = "WorkOrderDetailID";
        public const string WOROUTINGID_FLD = "WORoutingID";
    }
    public sealed class PRO_DCPResultDetailTable
    {
        public const string CONVERTED_FLD = "Converted";
        public const string DCPRESULTDETAILID_FLD = "DCPResultDetailID";
        public const string DCPRESULTMASTERID_FLD = "DCPResultMasterID";
        public const string ENDTIME_FLD = "EndTime";
        public const string ISMANUAL_FLD = "IsManual";
        public const string PERCENTAGE_FLD = "Percentage";
        public const string POGENERATEDID_FLD = "POGeneratedID";
        public const string QUANTITY_FLD = "Quantity";
        public const string SAFETYSTOCKAMOUNT = "SafetyStockAmount";
        public const string SHIFTID_FLD = "ShiftID";
        public const string STARTTIME_FLD = "StartTime";
        public const string TABLE_NAME = "PRO_DCPResultDetail";
        public const string TOTALSECOND_FLD = "TotalSecond";
        public const string TYPE_FLD = "Type";
        public const string WOGENERATEDID_FLD = "WOGeneratedID";
        public const string WORKINGDATE_FLD = "WorkingDate";
        public const string WOCONVERTED_FLD = "Converted";
    }
    public sealed class v_ApprovedAndNotCompletedPO
    {
        public const string VIEW_NAME = "v_ApprovedAndNotCompletedPO";
    }
    public sealed class PRO_DCPResultMasterTable
    {
        public const string CPOID_FLD = "CPOID";
        public const string DCOPTIONMASTERID_FLD = "DCOptionMasterID";
        public const string DCPRESULTMASTERID_FLD = "DCPResultMasterID";
        public const string DELIVERYQUANTITY_FLD = "DeliveryQuantity";
        public const string DUEDATETIME_FLD = "DueDateTime";
        public const string ID_FLD = "ID";
        public const string MASTERSHIFTID_FLD = "MasterShiftID";
        public const string MASTERTOTALSECOND_FLD = "MasterTotalSecond";
        public const string PRODUCTID_FLD = "ProductID";
        public const string QUANTITY_FLD = "Quantity";
        public const string ROUTINGID_FLD = "RoutingID";
        public const string STARTDATETIME_FLD = "StartDateTime";
        public const string TABLE_NAME = "PRO_DCPResultMaster";
        public const string WORKCENTERID_FLD = "WorkCenterID";
        public const string WORKORDERDETAILID_FLD = "WorkOrderDetailID";
        public const string WOROUTINGID_FLD = "WORoutingID";
        public const string CHECKPOINTID_FLD = "CheckPointID";
    }
    public sealed class PRO_OutsideProcessingMasterTable
    {
        public const string TABLE_NAME = "PRO_OutsideProcessingMaster";
        public const string OUTSIDEPROCESSINGMASTERID_FLD = "OutsideProcessingMasterID";
        public const string POSTDATE_FLD = "PostDate";
        public const string TRANSNO_FLD = "TransNo";
        public const string CCNID_FLD = "CCNID";
        public const string MASTERLOCATIONID_FLD = "MasterLocationID";
    }
    public sealed class PRO_FunctionTable
    {
        public const string CODE_FLD = "Code";
        public const string DESCRIPTION_FLD = "Description";
        public const string FUNCTIONID_FLD = "FunctionID";
        public const string TABLE_NAME = "PRO_Function";
    }

    public sealed class PRO_HourCodeTable
    {
        public const string CODE_FLD = "Code";
        public const string COMMENT_FLD = "Comment";
        public const string FACTOR_FLD = "Factor";
        public const string HOURCODE_FLD = "HourCode";
        public const string TABLE_NAME = "PRO_HourCode";
    }

    public sealed class PRO_IssueMaterialDetailTable
    {
        public const string AVAILABLEQUANTITY_FLD = "AvailableQuantity";
        public const string BINID_FLD = "BinID";
        public const string BOMQUANTITY_FLD = "BomQuantity";
        public const string COMMITQUANTITY_FLD = "CommitQuantity";
        public const string COMPLETEDQUANTITY_FLD = "CompletedQuantity";
        public const string ISSUEMATERIALDETAILID_FLD = "IssueMaterialDetailID";
        public const string ISSUEMATERIALMASTERID_FLD = "IssueMaterialMasterID";
        public const string LINE_FLD = "Line";
        public const string LOCATIONID_FLD = "LocationID";
        public const string LOT_FLD = "Lot";
        public const string MASTERLOCATIONID_FLD = "MasterLocationID";
        public const string PRODUCTID_FLD = "ProductID";
        public const string QASTATUS_FLD = "QAStatus";
        public const string SERIAL_FLD = "Serial";
        public const string STOCKUMID_FLD = "StockUMID";
        public const string TABLE_NAME = "PRO_IssueMaterialDetail";
        public const string WORKORDERDETAILID_FLD = "WorkOrderDetailID";
        public const string WORKORDERMASTERID_FLD = "WorkOrderMasterID";
    }

    public sealed class PRO_IssueMaterialMasterTable
    {
        public const string CCNID_FLD = "CCNID";
        public const string ISSUEMATERIALMASTERID_FLD = "IssueMaterialMasterID";
        public const string ISSUENO_FLD = "IssueNo";
        public const string ISSUEPURPOSEID_FLD = "IssuePurposeID";
        public const string LASTCHANGE_FLD = "LastChange";
        public const string MASTERLOCATIONID_FLD = "MasterLocationID";
        public const string POSTDATE_FLD = "PostDate";
        public const string PRODUCTID_FLD = "ProductID";
        public const string SHIFTID_FLD = "ShiftID";
        public const string TABLE_NAME = "PRO_IssueMaterialMaster";
        public const string TOBINID_FLD = "ToBinID";
        public const string TOLOCATIONID_FLD = "ToLocationID";
        public const string USERNAME_FLD = "UserName";
        public const string WORKORDERDETAILID_FLD = "WorkOrderDetailID";
        public const string WORKORDERMASTERID_FLD = "WorkOrderMasterID";
    }

    public sealed class PRO_IssuePurposeTable
    {
        public const string CODE_FLD = "Code";
        public const string DESCRIPTION_FLD = "Description";
        public const string HASDESTINATION_FLD = "HasDestination";
        public const string ISSUEPURPOSEID_FLD = "IssuePurposeID";
        public const string TABLE_NAME = "PRO_IssuePurpose";
        public const string TRANTYPEID_FLD = "TranTypeID";
    }

    public sealed class PRO_IssueSubMaterialDetailTable
    {
        public const string ISSUESUBMATERIALDETAILID_FLD = "IssueSubMaterialDetailID";
        public const string ISSUESUBMATERIALMASTERID_FLD = "IssueSubMaterialMasterID";
        public const string PRODUCTID_FLD = "ProductID";
        public const string QTY_FLD = "Qty";
        public const string TABLE_NAME = "PRO_IssueSubMaterialDetail";
        public const string UMID_FLD = "UMID";
    }

    public sealed class PRO_IssueSubMaterialMasterTable
    {
        public const string BINID_FLD = "BinID";
        public const string CCNID_FLD = "CCNID";
        public const string ISSUESUBMATERIALMASTERID_FLD = "IssueSubMaterialMasterID";
        public const string LOCATIONID_FLD = "LocationID";
        public const string PARENTWODETAILID_FLD = "ParentWODetailID";
        public const string POSTDATE_FLD = "PostDate";
        public const string PRODUCTIONLINEID_FLD = "ProductionLineID";
        public const string SALEORDERDETAILID_FLD = "SaleOrderDetailID";
        public const string TABLE_NAME = "PRO_IssueSubMaterialMaster";
        public const string TRANSNO_FLD = "TransNo";
        public const string WORKORDERDETAILID_FLD = "WorkOrderDetailID";
    }

    public sealed class PRO_LaborTimeDetailTable
    {
        public const string COMPLETED_FLD = "Completed";
        public const string COMPLETEPERCENT_FLD = "CompletePercent";
        public const string EMPLOYEEID_FLD = "EmployeeID";
        public const string ENDDATETIME_FLD = "EndDateTime";
        public const string HOUR_FLD = "Hour";
        public const string HOURCODE_FLD = "HourCode";
        public const string LABORCOSTCENTERID_FLD = "LaborCostCenterID";
        public const string LABORTIMEDETAILID_FLD = "LaborTimeDetailID";
        public const string LABORTIMEMASTERID_FLD = "LaborTimeMasterID";
        public const string QUANTITY_FLD = "Quantity";
        public const string SETUPRUN_FLD = "SetupRun";
        public const string SHIFTID_FLD = "ShiftID";
        public const string STARTDATETIME_FLD = "StartDateTime";
        public const string TABLE_NAME = "PRO_LaborTimeDetail";
        public const string WORKORDERDETAILID_FLD = "WorkOrderDetailID";
        public const string WORKORDERMASTERID_FLD = "WorkOrderMasterID";
        public const string WOROUTINGID_FLD = "WORoutingID";
    }

    public sealed class PRO_LaborTimeMasterTable
    {
        public const string CCNID_FLD = "CCNID";
        public const string LABORTIMEMASTERID_FLD = "LaborTimeMasterID";
        public const string MASTERLOCATIONID_FLD = "MasterLocationID";
        public const string POSTDATE_FLD = "PostDate";
        public const string TABLE_NAME = "PRO_LaborTimeMaster";
        public const string TRANSNO_FLD = "TransNo";
    }

    public sealed class PRO_MachineTable
    {
        public const string CODE_FLD = "Code";
        public const string MACHINEID_FLD = "MachineID";
        public const string MACHINENAME_FLD = "MachineName";
        public const string TABLE_NAME = "PRO_Machine";
    }

    public sealed class PRO_MachineTimeDetailTable
    {
        public const string COMPLETED_FLD = "Completed";
        public const string COMPLETEPERCENT_FLD = "CompletePercent";
        public const string ENDDATETIME_FLD = "EndDateTime";
        public const string HOUR_FLD = "Hour";
        public const string MACHINEID_FLD = "MachineID";
        public const string MACHINETIMEDETAILID_FLD = "MachineTimeDetailID";
        public const string MACHINETIMEMASTERID_FLD = "MachineTimeMasterID";
        public const string QUANTITY_FLD = "Quantity";
        public const string SETUPRUN_FLD = "SetupRun";
        public const string SHIFTID_FLD = "ShiftID";
        public const string STARTDATETIME_FLD = "StartDateTime";
        public const string TABLE_NAME = "PRO_MachineTimeDetail";
        public const string WORKORDERDETAILID_FLD = "WorkOrderDetailID";
        public const string WORKORDERMASTERID_FLD = "WorkOrderMasterID";
        public const string WOROUTINGID_FLD = "WORoutingID";
    }

    public sealed class PRO_MachineTimeMasterTable
    {
        public const string CCNID_FLD = "CCNID";
        public const string MACHINETIMEMASTERID_FLD = "MachineTimeMasterID";
        public const string MASTERLOCATIONID_FLD = "MasterLocationID";
        public const string POSTDATE_FLD = "PostDate";
        public const string TABLE_NAME = "PRO_MachineTimeMaster";
        public const string TRANSNO_FLD = "TransNo";
    }

    public sealed class PRO_OperationTable
    {
        public const string DESCRIPTION_FLD = "Description";
        public const string NAME_FLD = "Name";
        public const string OPERATIONID_FLD = "OperationID";
        public const string TABLE_NAME = "PRO_Operation";
        public const string WORKODERLINE_FLD = "WorkOderLine";
        public const string WORKORDER_FLD = "WorkOrder";
    }

    public sealed class PRO_OperationStatusTable
    {
        public const string CODE_FLD = "Code";
        public const string NAME_FLD = "Name";
        public const string OPERATIONSTATUSID_FLD = "OperationStatusID";
        public const string TABLE_NAME = "PRO_OperationStatus";
    }

    public sealed class PRO_PGProductTable
    {
        public const string PGPRODUCTID_FLD = "PGProductID";
        public const string PRODUCTID_FLD = "ProductID";
        public const string PRODUCTIONGROUPID_FLD = "ProductionGroupID";
        public const string TABLE_NAME = "PRO_PGProduct";
    }

    public sealed class PRO_PlanningOffsetTable
    {
        public const string DCOPTIONMASTERID_FLD = "DCOptionMasterID";
        public const string OFFSET_FLD = "Offset";
        public const string PLANNINGOFFSETID_FLD = "PlanningOffsetID";
        public const string PLANNINGSTARTDATE_FLD = "PlanningStartDate";
        public const string PRODUCTIONLINEID_FLD = "ProductionLineID";
        public const string TABLE_NAME = "PRO_PlanningOffset";
    }

    public sealed class PRO_ProduceReasonTable
    {
        public const string CODE_FLD = "Code";
        public const string DESCRIPTION_FLD = "Description";
        public const string PRODUCEREASONID_FLD = "ProduceReasonID";
        public const string TABLE_NAME = "PRO_ProduceReason";
    }

    public sealed class PRO_ProductionGroupTable
    {
        public const string CAPACITYOFGROUP_FLD = "CapacityOfGroup";
        public const string DESCRIPTION_FLD = "Description";
        public const string GROUPPRODUCTIONMAX_FLD = "GroupProductionMax";
        public const string PRIORITY_FLD = "Priority";
        public const string PRODUCTIONGROUPID_FLD = "ProductionGroupID";
        public const string PRODUCTIONLINEID_FLD = "ProductionLineID";
        public const string TABLE_NAME = "PRO_ProductionGroup";
    }

    public sealed class PRO_ProductionLineTable
    {
        public const string BALANCEPLANNING_FLD = "BalancePlanning";
        public const string CODE_FLD = "Code";
        public const string DEPARTMENTID_FLD = "DepartmentID";
        public const string JOBDESCRIPTION_FLD = "JobDescription";
        public const string LOCATIONID_FLD = "LocationID";
        public const string MANAGER_FLD = "Manager";
        public const string NAME_FLD = "Name";
        public const string PRODUCTIONLINEID_FLD = "ProductionLineID";
        public const string ROUNDUPDAYSEXCEPTION_FLD = "RoundUpDaysException";
        public const string TABLE_NAME = "PRO_ProductionLine";
    }

    public sealed class PRO_ProductProductionOrderTable
    {
        public const string PRODUCTID_FLD = "ProductID";
        public const string PRODUCTIONLINEID_FLD = "ProductionLineID";
        public const string PRODUCTPRODUCTIONORDERID_FLD = "ProductProductionOrderID";
        public const string SEQ_FLD = "Seq";
        public const string TABLE_NAME = "PRO_ProductProductionOrder";
    }

    public sealed class PRO_RequiredMaterialDetailTable
    {
        public const string ACTUALRECEIVED_FLD = "ActualReceived";
        public const string PRODUCTID_FLD = "ProductID";
        public const string QTY_FLD = "Qty";
        public const string REQUIREDMATERIALDETAILID_FLD = "RequiredMaterialDetailID";
        public const string REQUIREDMATERIALMASTERID_FLD = "RequiredMaterialMasterID";
        public const string REQUIREDQTY_FLD = "RequiredQty";
        public const string STOCKQTY_FLD = "StockQty";
        public const string TABLE_NAME = "PRO_RequiredMaterialDetail";
        public const string WORKORDERDETAILID_FLD = "WorkOrderDetailID";
    }

    public sealed class PRO_RequiredMaterialMasterTable
    {
        public const string CCNID_FLD = "CCNID";
        public const string ISSUEPURPOSEID_FLD = "IssuePurposeID";
        public const string LASTUPDATE_FLD = "LastUpdate";
        public const string PRODUCTIONLINEID_FLD = "ProductionLineID";
        public const string REQUIREDMATERIALMASTERID_FLD = "RequiredMaterialMasterID";
        public const string SALEORDERDETAILID_FLD = "SaleOrderDetailID";
        public const string TABLE_NAME = "PRO_RequiredMaterialMaster";
        public const string TRANSDATE_FLD = "TransDate";
        public const string TRANSNO_FLD = "TransNo";
        public const string USERAPPROVAL_FLD = "UserApproval";
        public const string USERNAME_FLD = "Username";
    }

    public sealed class PRO_ScrapReasonTable
    {
        public const string SCRAPREASONDESC_FLD = "ScrapReasonDesc";
        public const string SCRAPREASONID_FLD = "ScrapReasonID";
        public const string TABLE_NAME = "PRO_ScrapReason";
    }

    public sealed class PRO_ShiftTable
    {
        public const string SHIFTDESC_FLD = "ShiftDesc";
        public const string SHIFTID_FLD = "ShiftID";
        public const string TABLE_NAME = "PRO_Shift";
    }

    public sealed class PRO_ShiftCapacityTable
    {
        public const string SHIFTCAPACITYID_FLD = "ShiftCapacityID";
        public const string SHIFTID_FLD = "ShiftID";
        public const string TABLE_NAME = "PRO_ShiftCapacity";
        public const string WCCAPACITYID_FLD = "WCCapacityID";
    }

    public sealed class PRO_ShiftPatternTable
    {
        public const string CCNID_FLD = "CCNID";
        public const string COMMENT_FLD = "Comment";
        public const string EFFECTDATEFROM_FLD = "EffectDateFrom";
        public const string EFFECTDATETO_FLD = "EffectDateTo";
        public const string EXTRASTOPFROM_FLD = "ExtraStopFrom";
        public const string EXTRASTOPTO_FLD = "ExtraStopTo";
        public const string REFRESHINGFROM_FLD = "RefreshingFrom";
        public const string REFRESHINGTO_FLD = "RefreshingTo";
        public const string REGULARSTOPFROM_FLD = "RegularStopFrom";
        public const string REGULARSTOPTO_FLD = "RegularStopTo";
        public const string SHIFTID_FLD = "ShiftID";
        public const string SHIFTPATTERNID_FLD = "ShiftPatternID";
        public const string TABLE_NAME = "PRO_ShiftPattern";
        public const string WORKTIMEFROM_FLD = "WorkTimeFrom";
        public const string WORKTIMETO_FLD = "WorkTimeTo";
    }

    public sealed class PRO_WCCapacityTable
    {
        public const string BEGINDATE_FLD = "BeginDate";
        public const string CAPACITY_FLD = "Capacity";
        public const string CCNID_FLD = "CCNID";
        public const string CREWSIZE_FLD = "CrewSize";
        public const string ENDDATE_FLD = "EndDate";
        public const string FACTOR_FLD = "Factor";
        public const string MACHINENO_FLD = "MachineNo";
        public const string TABLE_NAME = "PRO_WCCapacity";
        public const string WCCAPACITYID_FLD = "WCCapacityID";
        public const string WCTYPE_FLD = "WCType";
        public const string WORKCENTERID_FLD = "WorkCenterID";
    }

    public sealed class PRO_WOBOMTable
    {
        public const string BOMUMID_FLD = "BomUMID";
        public const string COMPONENTID_FLD = "ComponentID";
        public const string LEADTIMEOFFSET_FLD = "LeadTimeOffset";
        public const string LINE_FLD = "Line";
        public const string PRODUCTID_FLD = "ProductID";
        public const string QUANTITY_FLD = "Quantity";
        public const string REQUIREDQUANTITY_FLD = "RequiredQuantity";
        public const string ROUTINGID_FLD = "RoutingID";
        public const string SHRINK_FLD = "Shrink";
        public const string TABLE_NAME = "PRO_WOBOM";
        public const string WOBOMID_FLD = "WOBomID";
        public const string WORKORDERDETAILID_FLD = "WorkOrderDetailID";
        public const string WORKORDERMASTERID_FLD = "WorkOrderMasterID";
    }

    public sealed class PRO_WorkingTimeTable
    {
        public const string ENDTIME_FLD = "EndTime";
        public const string MONTHSETUP_FLD = "MonthSetUp";
        public const string PRODUCTIONLINEID_FLD = "ProductionLineID";
        public const string STARTTIME_FLD = "StartTime";
        public const string TABLE_NAME = "PRO_WorkingTime";
        public const string WORKINGHOURS_FLD = "WorkingHours";
        public const string WORKINGTIMEID_FLD = "WorkingTimeID";
        public const string YEARSETUP_FLD = "YearSetUp";
    }

    public sealed class PRO_WorkOrderCompletionTable
    {
        public const string BINID_FLD = "BinID";
        public const string CCNID_FLD = "CCNID";
        public const string COMPLETEDDATE_FLD = "CompletedDate";
        public const string COMPLETEDQUANTITY_FLD = "CompletedQuantity";
        public const string ISSUEPURPOSEID_FLD = "IssuePurposeID";
        public const string KCSUSER_FLD = "KCSUser";
        public const string LASTCHANGE_FLD = "LastChange";
        public const string LOCATIONID_FLD = "LocationID";
        public const string LOT_FLD = "Lot";
        public const string MASTERLOCATIONID_FLD = "MasterLocationID";
        public const string NGQUANTITY_FLD = "NGQuantity";
        public const string POSTDATE_FLD = "PostDate";
        public const string PRODUCTID_FLD = "ProductID";
        public const string QASTATUS_FLD = "QAStatus";
        public const string REMARK_FLD = "Remark";
        public const string SERIAL_FLD = "Serial";
        public const string SHIFTID_FLD = "ShiftID";
        public const string STOCKUMID_FLD = "StockUMID";
        public const string TABLE_NAME = "PRO_WorkOrderCompletion";
        public const string USERNAME_FLD = "UserName";
        public const string WOCOMPLETIONNO_FLD = "WOCompletionNo";
        public const string WORKORDERCOMPLETIONID_FLD = "WorkOrderCompletionID";
        public const string WORKORDERDETAILID_FLD = "WorkOrderDetailID";
        public const string WORKORDERMASTERID_FLD = "WorkOrderMasterID";
        public const string WOCOMPLETIONDAT_FLD = "CompletedDate";
        public const string MUlTICOMPLETIONNO_FLD = "MultiCompletionNo";
    }
    public sealed class PRO_WorkingTime
    {
        public const string TABLE_NAME = "PRO_WorkingTime";
        public const string WORKINGTIMEID_FLD = "WorkingTimeID";
        public const string YEARSETUP_FLD = "YearSetUp";
        public const string MONTHSETUP_FLD = "MonthSetUp";
        public const string STARTTIME_FLD = "StartTime";
        public const string ENDTIME_FLD = "EndTime";
        public const string WORKINGHOURS_FLD = "WorkingHours";
        public const string PRODUCTIONLINEID_FLD = "ProductionLineID";
    }
    public sealed class PRO_WorkOrderDetailTable
    {
        public const string AGC_FLD = "AGC";
        public const string DESCRIPTION_FLD = "Description";
        public const string DUEDATE_FLD = "DueDate";
        public const string ESTCST_FLD = "EstCst";
        public const string FINCLOSEDATE_FLD = "FinCloseDate";
        public const string GROUP1_FLD = "Group1";
        public const string GROUP2_FLD = "Group2";
        public const string GROUPQUANTITY_FLD = "GroupQuantity";
        public const string INCREMENT_FLD = "Increment";
        public const string LINE_FLD = "Line";
        public const string MFGCLOSEDATE_FLD = "MfgCloseDate";
        public const string ORDERQUANTITY_FLD = "OrderQuantity";
        public const string PRIORITY_FLD = "Priority";
        public const string PRODUCTID_FLD = "ProductID";
        public const string PRODUCTIONLINEID_FLD = "ProductionLineID";
        public const string SALEORDERDETAILID_FLD = "SaleOrderDetailID";
        public const string SALEORDERMASTERID_FLD = "SaleOrderMasterID";
        public const string STARTDATE_FLD = "StartDate";
        public const string STATUS_FLD = "Status";
        public const string STOCKUMID_FLD = "StockUMID";
        public const string TABLE_NAME = "PRO_WorkOrderDetail";
        public const string USERID_FLD = "UserID";
        public const string WORKORDERDETAILID_FLD = "WorkOrderDetailID";
        public const string WORKORDERMASTERID_FLD = "WorkOrderMasterID";
    }

    public sealed class PRO_WorkOrderMasterTable
    {
        public const string CCNID_FLD = "CCNID";
        public const string DCOPTIONMASTERID_FLD = "DCOptionMasterID";
        public const string DESCRIPTION_FLD = "Description";
        public const string LASTCHANGE_FLD = "LastChange";
        public const string MASTERLOCATIONID_FLD = "MasterLocationID";
        public const string PARTYID_FLD = "PartyID";
        public const string PRODUCEREASONID_FLD = "ProduceReasonID";
        public const string PRODUCTIONLINEID_FLD = "ProductionLineID";
        public const string TABLE_NAME = "PRO_WorkOrderMaster";
        public const string TRANSDATE_FLD = "TransDate";
        public const string USERNAME_FLD = "UserName";
        public const string WORKORDERMASTERID_FLD = "WorkOrderMasterID";
        public const string WORKORDERNO_FLD = "WorkOrderNo";
    }

    public sealed class PRO_WORoutingTable
    {
        public const string CALCULATETIME_FLD = "CalculateTime";
        public const string CREWSIZE_FLD = "CrewSize";
        public const string EFFECTBEGINDATE_FLD = "EffectBeginDate";
        public const string EFFECTENDDATE_FLD = "EffectEndDate";
        public const string FIXLT_FLD = "FixLT";
        public const string FUNCTIONID_FLD = "FunctionID";
        public const string LABORCOSTCENTERID_FLD = "LaborCostCenterID";
        public const string LABORRUNTIME_FLD = "LaborRunTime";
        public const string LABORSETUPTIME_FLD = "LaborSetupTime";
        public const string MACHINECOSTCENTERID_FLD = "MachineCostCenterID";
        public const string MACHINERUNTIME_FLD = "MachineRunTime";
        public const string MACHINES_FLD = "Machines";
        public const string MACHINESETUPTIME_FLD = "MachineSetupTime";
        public const string MOVETIME_FLD = "MoveTime";
        public const string OSCOST_FLD = "OSCost";
        public const string OSFIXLT_FLD = "OSFixLT";
        public const string OSOVERLAPPERCENT_FLD = "OSOverlapPercent";
        public const string OSOVERLAPQTY_FLD = "OSOverlapQty";
        public const string OSSCHEDULESEQ_FLD = "OSScheduleSeq";
        public const string OSVARLT_FLD = "OSVarLT";
        public const string OVERLAPPERCENT_FLD = "OverlapPercent";
        public const string OVERLAPQTY_FLD = "OverlapQty";
        public const string PACER_FLD = "Pacer";
        public const string PARTYID_FLD = "PartyID";
        public const string PRODUCTID_FLD = "ProductID";
        public const string ROUTINGSTATUSID_FLD = "RoutingStatusID";
        public const string RUNQUANTITY_FLD = "RunQuantity";
        public const string SCHEDULESEQ_FLD = "ScheduleSeq";
        public const string SETUPQUANTITY_FLD = "SetupQuantity";
        public const string STEP_FLD = "Step";
        public const string STUDYTIME_FLD = "StudyTime";
        public const string TABLE_NAME = "PRO_WORouting";
        public const string TYPE_FLD = "Type";
        public const string VARLT_FLD = "VarLT";
        public const string WORKCENTERID_FLD = "WorkCenterID";
        public const string WORKORDERDETAILID_FLD = "WorkOrderDetailID";
        public const string WORKORDERMASTERID_FLD = "WorkOrderMasterID";
        public const string WOROUTINGID_FLD = "WORoutingID";
    }

    public sealed class SO_AdditionChargeTable
    {
        public const string ADDCHARGEID_FLD = "AddChargeID";
        public const string ADDITIONCHARGEID_FLD = "AdditionChargeID";
        public const string AMOUNT_FLD = "Amount";
        public const string QUANTITY_FLD = "Quantity";
        public const string REASONID_FLD = "ReasonID";
        public const string SALEORDERDETAILID_FLD = "SaleOrderDetailID";
        public const string SALEORDERMASTERID_FLD = "SaleOrderMasterID";
        public const string TABLE_NAME = "SO_AdditionCharge";
        public const string TOTALAMOUNT_FLD = "TotalAmount";
        public const string TYPE_FLD = "Type";
        public const string UNITPRICE_FLD = "UnitPrice";
        public const string VATAMOUNT_FLD = "VatAmount";
    }

    public sealed class SO_CancelReasonTable
    {
        public const string CANCELREASONID_FLD = "CancelReasonID";
        public const string CODE_FLD = "Code";
        public const string DESCRIPTION_FLD = "Description";
        public const string TABLE_NAME = "SO_CancelReason";
    }

    public sealed class SO_CommitInventoryDetailTable
    {
        public const string AVAILABLEQUANTITY_FLD = "AvailableQuantity";
        public const string BINID_FLD = "BinID";
        public const string CCNID_FLD = "CCNID";
        public const string COMMITINVENTORYDETAILID_FLD = "CommitInventoryDetailID";
        public const string COMMITINVENTORYMASTERID_FLD = "CommitInventoryMasterID";
        public const string COMMITQUANTITY_FLD = "CommitQuantity";
        public const string COSTOFGOODSSOLD_FLD = "CostOfGoodsSold";
        public const string DELIVERYSCHEDULEID_FLD = "DeliveryScheduleID";
        public const string DISCOUNTAMOUNT_FLD = "DiscountAmount";
        public const string DISCOUNTPERCENT_FLD = "DiscountPercent";
        public const string INSPECTIONSTATUS_FLD = "InspectionStatus";
        public const string LINE_FLD = "Line";
        public const string LOCATIONID_FLD = "LocationID";
        public const string LOT_FLD = "Lot";
        public const string MASTERLOCATIONID_FLD = "MasterLocationID";
        public const string NETAMOUNT_FLD = "NetAmount";
        public const string PACKED_FLD = "Packed";
        public const string PRICE_FLD = "Price";
        public const string PRODUCTID_FLD = "ProductID";
        public const string SELLINGUMID_FLD = "SellingUMID";
        public const string SERIAL_FLD = "Serial";
        public const string SHIPDATE_FLD = "ShipDate";
        public const string SHIPPED_FLD = "Shipped";
        public const string STDCOST_FLD = "STDCost";
        public const string STOCKUMID_FLD = "StockUMID";
        public const string TABLE_NAME = "SO_CommitInventoryDetail";
        public const string UMRATE_FLD = "UMRate";
        public const string VATAMOUNT_FLD = "VATAmount";
        public const string VATPERCENT_FLD = "VATPercent";
    }

    public sealed class SO_CommitInventoryMasterTable
    {
        public const string COMMENT_FLD = "Comment";
        public const string COMMITDATE_FLD = "CommitDate";
        public const string COMMITINVENTORYMASTERID_FLD = "CommitInventoryMasterID";
        public const string COMMITMENTNO_FLD = "CommitmentNo";
        public const string CONTACT_FLD = "Contact";
        public const string DELIVERYDATE_FLD = "DeliveryDate";
        public const string EMPLOYEEID_FLD = "EmployeeID";
        public const string LASTUPDATE_FLD = "LastUpdate";
        public const string PAYMENTTERMID_FLD = "PaymentTermID";
        public const string RECEIVEDAMOUNT_FLD = "ReceivedAmount";
        public const string SALEORDERMASTERID_FLD = "SaleOrderMasterID";
        public const string TABLE_NAME = "SO_CommitInventoryMaster";
        public const string TAXCODE_FLD = "TaxCode";
        public const string TOTALDISCOUNTAMOUNT_FLD = "TotalDiscountAmount";
        public const string TOTALRECEIVABLEAMOUNT_FLD = "TotalReceivableAmount";
        public const string TOTALREVENUEAMOUNT_FLD = "TotalRevenueAmount";
        public const string TOTALVATAMOUNT_FLD = "TotalVATAmount";
        public const string USERID_FLD = "UserID";
        public const string USERNAME_FLD = "UserName";
    }

    public sealed class SO_ConfirmShipDetailTable
    {
        public const string CONFIRMSHIPDETAILID_FLD = "ConfirmShipDetailID";
        public const string CONFIRMSHIPMASTERID_FLD = "ConfirmShipMasterID";
        public const string DELIVERYSCHEDULEID_FLD = "DeliveryScheduleID";
        public const string INVOICEQTY_FLD = "InvoiceQty";
        public const string NETAMOUNT_FLD = "NetAmount";
        public const string PRICE_FLD = "Price";
        public const string PRODUCTID_FLD = "ProductID";
        public const string SALEORDERDETAILID_FLD = "SaleOrderDetailID";
        public const string TABLE_NAME = "SO_ConfirmShipDetail";
        public const string VATAMOUNT_FLD = "VATAmount";
        public const string VATPERCENT_FLD = "VATPercent";
    }

    public sealed class SO_ConfirmShipMasterTable
    {
        public const string CCNID_FLD = "CCNID";
        public const string CNO_FLD = "CNo";
        public const string COMMENT_FLD = "Comment";
        public const string CONFIRMSHIPMASTERID_FLD = "ConfirmShipMasterID";
        public const string CONFIRMSHIPNO_FLD = "ConfirmShipNo";
        public const string CURRENCYID_FLD = "CurrencyID";
        public const string EXCHANGERATE_FLD = "ExchangeRate";
        public const string FROMPORT_FLD = "FromPort";
        public const string GATEID_FLD = "GateID";
        public const string GROSSWEIGHT_FLD = "GrossWeight";
        public const string INVOICEDATE_FLD = "InvoiceDate";
        public const string INVOICENO_FLD = "InvoiceNo";
        public const string ISSUINGBANK_FLD = "IssuingBank";
        public const string LASTCHANGE_FLD = "LastChange";
        public const string LCDATE_FLD = "LCDate";
        public const string LCNO_FLD = "LCNo";
        public const string MASTERLOCATIONID_FLD = "MasterLocationID";
        public const string MEASUREMENT_FLD = "Measurement";
        public const string NETWEIGHT_FLD = "NetWeight";
        public const string ONBOARDDATE_FLD = "OnBoardDate";
        public const string REFERENCENO_FLD = "ReferenceNo";
        public const string SALEORDERMASTERID_FLD = "SaleOrderMasterID";
        public const string SHIPCODE_FLD = "ShipCode";
        public const string SHIPPEDDATE_FLD = "ShippedDate";
        public const string TABLE_NAME = "SO_ConfirmShipMaster";
        public const string USERNAME_FLD = "UserName";
        public const string VESSELNAME_FLD = "VesselName";
    }

    public sealed class SO_CustomerItemRefDetailTable
    {
        public const string BYPRICE_FLD = "ByPrice";
        public const string CUSTOMERITEMCODE_FLD = "CustomerItemCode";
        public const string CUSTOMERITEMMODEL_FLD = "CustomerItemModel";
        public const string CUSTOMERITEMREFDETAILID_FLD = "CustomerItemRefDetailID";
        public const string CUSTOMERITEMREFMASTERID_FLD = "CustomerItemRefMasterID";
        public const string DISCOUNTAMOUNT_FLD = "DiscountAmount";
        public const string DISCOUNTPERCENT_FLD = "DiscountPercent";
        public const string DISCOUNTQTY_FLD = "DiscountQty";
        public const string PRODUCTID_FLD = "ProductID";
        public const string TABLE_NAME = "SO_CustomerItemRefDetail";
        public const string UNITOFMEASUREID_FLD = "UnitOfMeasureID";
        public const string UNITPRICE_FLD = "UnitPrice";
    }

    public sealed class SO_CustomerItemRefMasterTable
    {
        public const string ACTIVE_FLD = "Active";
        public const string CCNID_FLD = "CCNID";
        public const string CODE_FLD = "Code";
        public const string CUSTOMERITEMREFMASTERID_FLD = "CustomerItemRefMasterID";
        public const string EFFECTDATE_FLD = "EffectDate";
        public const string FROMDATE_FLD = "FromDate";
        public const string PARTYID_FLD = "PartyID";
        public const string TABLE_NAME = "SO_CustomerItemRefMaster";
        public const string TODATE_FLD = "ToDate";
    }

    public sealed class SO_DeliveryScheduleTable
    {
        public const string COMEDATE_FLD = "ComeDate";
        public const string COMEPORT_FLD = "ComePort";
        public const string LEAVEDATE_FLD = "LeaveDate";
        public const string LEAVEPORT_FLD = "LeavePort";

        public const string CARIER_FLD = "Carier";
        public const string DELIVERYNO_FLD = "DeliveryNo";
        public const string DELIVERYQUANTITY_FLD = "DeliveryQuantity";
        public const string DELIVERYSCHEDULEID_FLD = "DeliveryScheduleID";
        public const string GATEID_FLD = "GateID";
        public const string LINE_FLD = "Line";
        public const string PROMISEDATE_FLD = "PromiseDate";
        public const string REQUIREDDATE_FLD = "RequiredDate";
        public const string SALEORDERDETAILID_FLD = "SaleOrderDetailID";
        public const string SCHEDULEDATE_FLD = "ScheduleDate";
        public const string TABLE_NAME = "SO_DeliverySchedule";
    }

    public sealed class SO_GateTable
    {
        public const string CODE_FLD = "Code";
        public const string DESCRIPTION_FLD = "Description";
        public const string GATEID_FLD = "GateID";
        public const string GATETYPEID_FLD = "GateTypeID";
        public const string TABLE_NAME = "SO_Gate";
    }

    public sealed class SO_InvoiceDetailTable
    {
        public const string DELIVERYSCHEDULEID_FLD = "DeliveryScheduleID";
        public const string INVOICEDETAILID_FLD = "InvoiceDetailID";
        public const string INVOICEMASTERID_FLD = "InvoiceMasterID";
        public const string INVOICEQTY_FLD = "InvoiceQty";
        public const string NETAMOUNT_FLD = "NetAmount";
        public const string PRICE_FLD = "Price";
        public const string PRODUCTID_FLD = "ProductID";
        public const string SALEORDERDETAILID_FLD = "SaleOrderDetailID";
        public const string TABLE_NAME = "SO_InvoiceDetail";
        public const string VATAMOUNT_FLD = "VATAmount";
        public const string VATPERCENT_FLD = "VATPercent";
    }

    public sealed class SO_InvoiceMasterTable
    {
        public const string CCNID_FLD = "CCNID";
        public const string CNO_FLD = "CNo";
        public const string COMMENT_FLD = "Comment";
        public const string CONFIRMSHIPNO_FLD = "ConfirmShipNo";
        public const string CURRENCYID_FLD = "CurrencyID";
        public const string EXCHANGERATE_FLD = "ExchangeRate";
        public const string FROMPORT_FLD = "FromPort";
        public const string GATEID_FLD = "GateID";
        public const string GROSSWEIGHT_FLD = "GrossWeight";
        public const string INVOICEDATE_FLD = "InvoiceDate";
        public const string INVOICEMASTERID_FLD = "InvoiceMasterID";
        public const string INVOICENO_FLD = "InvoiceNo";
        public const string ISSUINGBANK_FLD = "IssuingBank";
        public const string LASTCHANGE_FLD = "LastChange";
        public const string LCDATE_FLD = "LCDate";
        public const string LCNO_FLD = "LCNo";
        public const string MASTERLOCATIONID_FLD = "MasterLocationID";
        public const string MEASUREMENT_FLD = "Measurement";
        public const string NETWEIGHT_FLD = "NetWeight";
        public const string ONBOARDDATE_FLD = "OnBoardDate";
        public const string REFERENCENO_FLD = "ReferenceNo";
        public const string SALEORDERMASTERID_FLD = "SaleOrderMasterID";
        public const string SHIPCODE_FLD = "ShipCode";
        public const string SHIPPEDDATE_FLD = "ShippedDate";
        public const string TABLE_NAME = "SO_InvoiceMaster";
        public const string USERNAME_FLD = "UserName";
        public const string VESSELNAME_FLD = "VesselName";
    }

    public sealed class SO_PaymentMethodTable
    {
        public const string CODE_FLD = "Code";
        public const string DESCRIPTION_FLD = "Description";
        public const string PAYMENTMETHODID_FLD = "PaymentMethodID";
        public const string TABLE_NAME = "SO_PaymentMethod";
    }

    public sealed class SO_ReturnedGoodsDetailTable
    {
        public const string BALANCEQTY_FLD = "BalanceQty";
        public const string BINID_FLD = "BinID";
        public const string CONFIRMSHIPMASTERID_FLD = "ConfirmShipMasterID";
        public const string LINE_FLD = "Line";
        public const string LOCATIONID_FLD = "LocationID";
        public const string LOT_FLD = "Lot";
        public const string MASTERLOCATIONID_FLD = "MasterLocationID";
        public const string PRODUCTID_FLD = "ProductID";
        public const string QASTATUS_FLD = "QAStatus";
        public const string QUANTITYOFSELLING_FLD = "QuantityOfSelling";
        public const string RECEIVEQUANTITY_FLD = "ReceiveQuantity";
        public const string RETURNEDGOODSDETAILID_FLD = "ReturnedGoodsDetailID";
        public const string RETURNEDGOODSMASTERID_FLD = "ReturnedGoodsMasterID";
        public const string SALEORDERDETAILID_FLD = "SaleOrderDetailID";
        public const string SERIAL_FLD = "Serial";
        public const string TABLE_NAME = "SO_ReturnedGoodsDetail";
        public const string UNITID_FLD = "UnitID";
        public const string UNITPRICE_FLD = "UnitPrice";
    }

    public sealed class SO_ReturnedGoodsMasterTable
    {
        public const string CCNID_FLD = "CCNID";
        public const string CURRENCYID_FLD = "CurrencyID";
        public const string DESCRIPTION_FLD = "Description";
        public const string EXCHANGERATE_FLD = "ExchangeRate";
        public const string LASTCHANGE_FLD = "LastChange";
        public const string MASTERLOCATIONID_FLD = "MasterLocationID";
        public const string PARTYCONTACTID_FLD = "PartyContactID";
        public const string PARTYID_FLD = "PartyID";
        public const string PARTYLOCATIONID_FLD = "PartyLocationID";
        public const string POSTDATE_FLD = "PostDate";
        public const string RECEIVERID_FLD = "ReceiverID";
        public const string RETURNEDGOODSMASTERID_FLD = "ReturnedGoodsMasterID";
        public const string RETURNEDGOODSNUMBER_FLD = "ReturnedGoodsNumber";
        public const string SALEORDERMASTERID_FLD = "SaleOrderMasterID";
        public const string TABLE_NAME = "SO_ReturnedGoodsMaster";
        public const string TRANSDATE_FLD = "TransDate";
        public const string USERNAME_FLD = "UserName";
    }

    public sealed class SO_SaleOrderDetailTable
    {
        public const string AUTOCOMMIT_FLD = "AutoCommit";
        public const string BACKORDERQTY_FLD = "BackOrderQty";
        public const string CANCELREASONID_FLD = "CancelReasonID";
        public const string CONVERTEDQUANTITY_FLD = "ConvertedQuantity";
        public const string DISCOUNTAMOUNT_FLD = "DiscountAmount";
        public const string DISCOUNTPERCENT_FLD = "DiscountPercent";
        public const string DISCOUNTQUANTITY_FLD = "DiscountQuantity";
        public const string DUEDATE_FLD = "DueDate";
        public const string EXPORTTAXAMOUNT_FLD = "ExportTaxAmount";
        public const string EXPORTTAXPERCENT_FLD = "ExportTaxPercent";
        public const string FGOODSCODE_FLD = "FGoodsCode";
        public const string ITEMCUSTOMERCODE_FLD = "ItemCustomerCode";
        public const string ITEMCUSTOMERREVISION_FLD = "ItemCustomerRevision";
        public const string ITEMDESCRIPTION_FLD = "ItemDescription";
        public const string NETAMOUNT_FLD = "NetAmount";
        public const string ORDERQUANTITY_FLD = "OrderQuantity";
        public const string PRODUCTID_FLD = "ProductID";
        public const string REASONID_FLD = "ReasonID";
        public const string SALEORDERDETAILID_FLD = "SaleOrderDetailID";
        public const string SALEORDERLINE_FLD = "SaleOrderLine";
        public const string SALEORDERMASTERID_FLD = "SaleOrderMasterID";
        public const string SELLINGUMID_FLD = "SellingUMID";
        public const string SHIPCOMPLETED_FLD = "ShipCompleted";
        public const string SHIPQUANTITY_FLD = "ShipQuantity";
        public const string SPECIALTAXAMOUNT_FLD = "SpecialTaxAmount";
        public const string SPECIALTAXPERCENT_FLD = "SpecialTaxPercent";
        public const string STOCKQUANTITY_FLD = "StockQuantity";
        public const string STOCKUMID_FLD = "StockUMID";
        public const string TABLE_NAME = "SO_SaleOrderDetail";
        public const string TOTALAMOUNT_FLD = "TotalAmount";
        public const string UMRATE_FLD = "UMRate";
        public const string UNITPRICE_FLD = "UnitPrice";
        public const string VATAMOUNT_FLD = "VATAmount";
        public const string VATPERCENT_FLD = "VATPercent";
        public const string WOQUANTITY_FLD = "WOQuantity";
        public const string REQUIREDDATE_FLD = "RequiredDate";
    }

    public sealed class SO_SaleOrderMasterTable
    {
        public const string BILLTOLOCID_FLD = "BillToLocID";
        public const string BUYINGLOCID_FLD = "BuyingLocID";
        public const string CANCELREASONID_FLD = "CancelReasonID";
        public const string CARRIERID_FLD = "CarrierID";
        public const string CCNID_FLD = "CCNID";
        public const string CODE_FLD = "Code";
        public const string CURRENCYID_FLD = "CurrencyID";
        public const string CUSTOMERITEMREFMASTERID_FLD = "CustomerItemRefMasterID";
        public const string CUSTOMERPURCHASEORDERNO_FLD = "CustomerPurchaseOrderNo";
        public const string DELIVERED_FLD = "Delivered";
        public const string DELIVERYTERMSID_FLD = "DeliveryTermsID";
        public const string DISCOUNTTERMSID_FLD = "DiscountTermsID";
        public const string EXCHANGERATE_FLD = "ExchangeRate";
        public const string EXCHANGERATECC_FLD = "ExchangeRateCC";
        public const string EXPORTTAX_FLD = "ExportTax";
        public const string EXPORTTAXRATE_FLD = "ExportTaxRate";
        public const string FINISHEDDATE_FLD = "FinishedDate";
        public const string LASTCHANGE_FLD = "LastChange";
        public const string LOCATIONID_FLD = "LocationID";
        public const string PARTYCONTACTID_FLD = "PartyContactID";
        public const string PARTYID_FLD = "PartyID";
        public const string PAUSEID_FLD = "PauseID";
        public const string PAYMENTMETHODID_FLD = "PaymentMethodID";
        public const string PAYMENTTERMSID_FLD = "PaymentTermsID";
        public const string PRICELISTMASTERID_FLD = "PriceListMasterID";
        public const string PRIORITY_FLD = "Priority";
        public const string PRODUCTIONVALIDATION_FLD = "Productionvalidation";
        public const string SALEORDERMASTERID_FLD = "SaleOrderMasterID";
        public const string SALESREPRESENTATIVEID_FLD = "SalesRepresentativeID";
        public const string SALESTATUSID_FLD = "SaleStatusID";
        public const string SALETYPEID_FLD = "SaleTypeID";
        public const string SHIPCOMPLETED_FLD = "ShipCompleted";
        public const string SHIPFROMLOCID_FLD = "ShipFromLocID";
        public const string SHIPTOLOCID_FLD = "ShipToLocID";
        public const string SPECIALTAX_FLD = "SpecialTax";
        public const string SPECIALTAXRATE_FLD = "SpecialTaxRate";
        public const string STATUS_FLD = "Status";
        public const string TABLE_NAME = "SO_SaleOrderMaster";
        public const string TOTALAMOUNT_FLD = "TotalAmount";
        public const string TOTALDISCOUNTAMOUNT_FLD = "TotalDiscountAmount";
        public const string TOTALEXPORTAMOUNT_FLD = "TotalExportAmount";
        public const string TOTALNETAMOUNT_FLD = "TotalNetAmount";
        public const string TOTALSPECIALTAXAMOUNT_FLD = "TotalSpecialTaxAmount";
        public const string TOTALVATAMOUNT_FLD = "TotalVATAmount";
        public const string TRANSDATE_FLD = "TransDate";
        public const string TYPEID_FLD = "TypeID";
        public const string USERNAME_FLD = "UserName";
        public const string VAT_FLD = "VAT";
        public const string VATRATE_FLD = "VATRate";

        public const string COMEDATE = "ComeDate";
        public const string FROMPORT = "FromPort";
        public const string LEAVEDATE = "LeaveDate";
        public const string TOPORT = "ToPort";
        public const string COMMENT_FLD = "Comment";

    }

    public sealed class SO_SaleStatusTable
    {
        public const string CODE_FLD = "Code";
        public const string DESCRIPTION_FLD = "Description";
        public const string SALESTATUSID_FLD = "SaleStatusID";
        public const string TABLE_NAME = "SO_SaleStatus";
    }

    public sealed class SO_SaleTypeTable
    {
        public const string CODE_FLD = "Code";
        public const string DESCRIPTION_FLD = "Description";
        public const string SALETYPEID_FLD = "SaleTypeID";
        public const string TABLE_NAME = "SO_SaleType";
    }

    public sealed class SO_TypeTable
    {
        public const string CODE_FLD = "Code";
        public const string DESCRIPTION_FLD = "Description";
        public const string TABLE_NAME = "SO_Type";
        public const string TYPEID_FLD = "TypeID";
    }

    public sealed class STD_CostCenterRateDetailTable
    {
        public const string COST_FLD = "Cost";
        public const string COSTCENTERRATEDETAILID_FLD = "CostCenterRateDetailID";
        public const string COSTCENTERRATEMASTERID_FLD = "CostCenterRateMasterID";
        public const string COSTELEMENTID_FLD = "CostElementID";
        public const string TABLE_NAME = "STD_CostCenterRateDetail";
    }

    public sealed class STD_CostCenterRateMasterTable
    {
        public const string CCNID_FLD = "CCNID";
        public const string CODE_FLD = "Code";
        public const string COSTCENTERRATEMASTERID_FLD = "CostCenterRateMasterID";
        public const string NAME_FLD = "Name";
        public const string PRODUCTID_FLD = "ProductID";
        public const string TABLE_NAME = "STD_CostCenterRateMaster";
    }

    public sealed class STD_CostElementTable
    {
        public const string CODE_FLD = "Code";
        public const string COSTELEMENTID_FLD = "CostElementID";
        public const string COSTELEMENTTYPEID_FLD = "CostElementTypeID";
        public const string ISLEAF_FLD = "IsLeaf";
        public const string NAME_FLD = "Name";
        public const string ORDERNO_FLD = "OrderNo";
        public const string PARENTID_FLD = "ParentID";
        public const string TABLE_NAME = "STD_CostElement";
    }

    public sealed class STD_CostElementTypeTable
    {
        public const string CODE_FLD = "Code";
        public const string COSTELEMENTTYPEID_FLD = "CostElementTypeID";
        public const string DESCRIPTION_FLD = "Description";
        public const string TABLE_NAME = "STD_CostElementType";
    }

    public sealed class Sys_APParametersTable
    {
        public const string CCNID_FLD = "CCNID";
        public const string CGOACCOUNTID_FLD = "CGOAccountID";
        public const string DEPOSITACCOUNTID_FLD = "DepositAccountID";
        public const string EACACCOUNTID_FLD = "EACAccountID";
        public const string EMPLOYEEPAYABLEACCOUNTID_FLD = "EmployeePayableAccountID";
        public const string FAOFFINANCIALCREDITACCOUNTID_FLD = "FAOfFinancialCreditAccountID";
        public const string IMPORTTAXACCOUNTID_FLD = "ImportTaxAccountID";
        public const string INTANGIBLEFAACCOUNTID_FLD = "IntangibleFAAccountID";
        public const string JOINTACCOUNTID_FLD = "JointAccountID";
        public const string PAYABLEACCOUNTID_FLD = "PayableAccountID";
        public const string SALECOSTSACCOUNTID_FLD = "SaleCostsAccountID";
        public const string TABLE_NAME = "Sys_APParameters";
        public const string TANGIBLEFAACCOUNTID_FLD = "TangibleFAAccountID";
        public const string VATINPUTACCOUNTID_FLD = "VATInputAccountID";
    }

    public sealed class Sys_ARParameterTable
    {
        public const string ARPARAMETERID_FLD = "ARParameterID";
        public const string CCNID_FLD = "CCNID";
        public const string RECEIVABLEACCOUNTID_FLD = "ReceivableAccountID";
        public const string TABLE_NAME = "Sys_ARParameter";
        public const string VATOUTPUTACCOUNTID_FLD = "VATOutputAccountID";
    }

    public sealed class Sys_ControlGroupTable
    {
        public const string CONTROLGROUPID_FLD = "ControlGroupID";
        public const string CONTROLGROUPTEXT_FLD = "ControlGroupText";
        public const string CONTROLGROUPTEXTJP_FLD = "ControlGroupTextJP";
        public const string CONTROLGROUPTEXTVN_FLD = "ControlGroupTextVN";
        public const string TABLE_NAME = "Sys_ControlGroup";
    }

    public sealed class Sys_Error_MsgTable
    {
        public const string CODE_FLD = "Code";
        public const string DESCRIPTION_FLD = "Description";
        public const string ERROR_MSGID_FLD = "Error_MsgID";
        public const string MSGDEFAULT_FLD = "MsgDefault";
        public const string MSGEN_FLD = "MsgEn";
        public const string MSGJP_FLD = "MsgJp";
        public const string MSGVN_FLD = "MsgVn";
        public const string TABLE_NAME = "Sys_Error_Msg";
    }

    public sealed class Sys_FieldGroupTable
    {
        public const string FIELDGROUPID_FLD = "FieldGroupID";
        public const string GROUPCODE_FLD = "GroupCode";
        public const string GROUPLEVEL_FLD = "GroupLevel";
        public const string GROUPNAMEEN_FLD = "GroupNameEN";
        public const string GROUPNAMEJP_FLD = "GroupNameJP";
        public const string GROUPNAMEVN_FLD = "GroupNameVN";
        public const string GROUPORDER_FLD = "GroupOrder";
        public const string PARENTFIELDGROUPID_FLD = "ParentFieldGroupID";
        public const string REPORTID_FLD = "ReportID";
        public const string TABLE_NAME = "Sys_FieldGroup";
    }

    public sealed class Sys_FieldGroupDetailTable
    {
        public const string FIELDGROUPDETAILID_FLD = "FieldGroupDetailID";
        public const string FIELDGROUPID_FLD = "FieldGroupID";
        public const string FIELDNAME_FLD = "FieldName";
        public const string REPORTID_FLD = "ReportID";
        public const string TABLE_NAME = "Sys_FieldGroupDetail";
    }

    public sealed class Sys_FinanceParamtersTable
    {
        public const string APPLYRATETYPE_FLD = "ApplyRateType";
        public const string CCNID_FLD = "CCNID";
        public const string EXCHANGEDEFFIRENCEACCOUNTID_FLD = "ExchangeDeffirenceAccountID";
        public const string GAINEXCHANGEACCOUNTID_FLD = "GainExchangeAccountID";
        public const string LOSSEXCHANGEACCOUNTID_FLD = "LossExchangeAccountID";
        public const string TABLE_NAME = "Sys_FinanceParamters";
    }

    public sealed class Sys_FinanceReportDetailTable
    {
        public const string ACCOUNTCODE_FLD = "AccountCode";
        public const string CODE_FLD = "Code";
        public const string CORACCOUNTCODE_FLD = "CorAccountCode";
        public const string CREDITACCOUNTCODE_FLD = "CreditAccountCode";
        public const string DEBITACCOUNTCODE_FLD = "DebitAccountCode";
        public const string DESCRIPTION_FLD = "Description";
        public const string FINANCEREPORTDETAILID_FLD = "FinanceReportDetailID";
        public const string FINANCEREPORTMASTERID_FLD = "FinanceReportMasterID";
        public const string FORMULA_FLD = "Formula";
        public const string ISBOLD_FLD = "IsBold";
        public const string LINE_FLD = "Line";
        public const string TABLE_NAME = "Sys_FinanceReportDetail";
        public const string TARGET_FLD = "Target";
    }

    public sealed class Sys_FinanceReportMasterTable
    {
        public const string CCNID_FLD = "CCNID";
        public const string CODE_FLD = "Code";
        public const string DECISIONNUMBER_FLD = "DecisionNumber";
        public const string FINANCEREPORTMASTERID_FLD = "FinanceReportMasterID";
        public const string FINANCEREPORTTYPEID_FLD = "FinanceReportTypeID";
        public const string PATTERNNUMBER_FLD = "PatternNumber";
        public const string REPORTNAMEEN_FLD = "ReportNameEN";
        public const string REPORTNAMEJP_FLD = "ReportNameJP";
        public const string REPORTNAMEVN_FLD = "ReportNameVN";
        public const string TABLE_NAME = "Sys_FinanceReportMaster";
        public const string USERID_FLD = "UserID";
    }

    public sealed class Sys_FinanceReportTypeTable
    {
        public const string CODE_FLD = "Code";
        public const string DESCRIPTION_FLD = "Description";
        public const string FINANCEREPORTTYPEID_FLD = "FinanceReportTypeID";
        public const string TABLE_NAME = "Sys_FinanceReportType";
    }

    public sealed class sys_InfoTable
    {
        public const string BISO_FLD = "BISO";
        public const string TABLE_NAME = "sys_Info";
    }

    public sealed class Sys_Menu_EntryTable
    {
        public const string BUTTON_CAPTION_FLD = "Button_Caption";
        public const string COLLAPSEDIMAGE_FLD = "CollapsedImage";
        public const string DESCRIPTION_FLD = "Description";
        public const string EXPANDEDIMAGE_FLD = "ExpandedImage";
        public const string FORMLOAD_FLD = "FormLoad";
        public const string ISTRANSACTION_FLD = "IsTransaction";
        public const string ISUSERCREATED_FLD = "IsUserCreated";
        public const string MENU_ENTRYID_FLD = "Menu_EntryID";
        public const string PARENT_CHILD_FLD = "Parent_Child";
        public const string PARENT_SHORTCUT_FLD = "Parent_Shortcut";
        public const string PREFIX_FLD = "Prefix";
        public const string REPORTID_FLD = "ReportID";
        public const string SHORTCUT_FLD = "Shortcut";
        public const string TABLE_NAME = "Sys_Menu_Entry";
        public const string TABLENAME_FLD = "TableName";
        public const string TEXT_CAPTION_EN_US_FLD = "Text_Caption_EN_US";
        public const string TEXT_CAPTION_JA_JP_FLD = "Text_Caption_JA_JP";
        public const string TEXT_CAPTION_LANGUAGE_DEFAULT_FLD = "Text_Caption_Language_Default";
        public const string TEXT_CAPTION_VI_VN_FLD = "Text_Caption_VI_VN";
        public const string TEXT_CAPTIONDEFAULT_FLD = "Text_CaptionDefault";
        public const string TRANSFORMAT_FLD = "TransFormat";
        public const string TRANSNOFIELDNAME_FLD = "TransNoFieldName";
        public const string TYPE_FLD = "Type";
    }

    public sealed class Sys_ParamTable
    {
        public const string NAME_FLD = "Name";
        public const string PARAMID_FLD = "ParamID";
        public const string TABLE_NAME = "Sys_Param";
        public const string VALUE_FLD = "Value";
    }

    public sealed class Sys_PayrollParamTable
    {
        public const string CCNID_FLD = "CCNID";
        public const string DEPARTMENTID_FLD = "DepartmentID";
        public const string EXPENSEACCOUNTID_FLD = "ExpenseAccountID";
        public const string FIXEDCOSTACCOUNTID_FLD = "FixedCostAccountID";
        public const string HEALTHINSURANCEACCOUNTID_FLD = "HealthInsuranceAccountID";
        public const string PAYFOREMPLOYEEACCOUNTID_FLD = "PayForEmployeeAccountID";
        public const string PERSONALINCOMETAXACCOUNTID_FLD = "PersonalIncomeTaxAccountID";
        public const string PRODUCTIONLINEID_FLD = "ProductionLineID";
        public const string SOCIALINSURANCEACCOUNTID_FLD = "SocialInsuranceAccountID";
        public const string TABLE_NAME = "Sys_PayrollParam";
    }

    public sealed class Sys_PCParamTable
    {
        public const string CCNID_FLD = "CCNID";
        public const string COSTFORRAWMATERIALACCOUNTID_FLD = "CostForRawMaterialAccountID";
        public const string COSTFORWIPACCOUNTID_FLD = "CostForWIPAccountID";
        public const string TABLE_NAME = "Sys_PCParam";
    }

    public sealed class Sys_PeriodTable
    {
        public const string ACTIVATE_FLD = "Activate";
        public const string BEGINPERIOD_FLD = "BeginPeriod";
        public const string CCNID_FLD = "CCNID";
        public const string FROMDATE_FLD = "FromDate";
        public const string PERIODID_FLD = "PeriodID";
        public const string TABLE_NAME = "Sys_Period";
        public const string TODATE_FLD = "ToDate";
    }

    public sealed class Sys_PostdateConfigurationTable
    {
        public const string DAYBEFORE_FLD = "DayBefore";
        public const string LASTUPDATED_FLD = "LastUpdated";
        public const string POSTDATECONFIGURATIONID_FLD = "PostdateConfigurationID";
        public const string PURPOSE_FLD = "Purpose";
        public const string TABLE_NAME = "Sys_PostdateConfiguration";
        public const string USERNAME_FLD = "Username";
    }

    public sealed class Sys_PrintConfigurationTable
    {
        public const string COPIES_FLD = "Copies";
        public const string DESCRIPTION_FLD = "Description";
        public const string FILENAME_FLD = "FileName";
        public const string FORMNAME_FLD = "FormName";
        public const string FUNCTIONNAME_FLD = "FunctionName";
        public const string PRINTABLE_FLD = "Printable";
        public const string PRINTCONFIGURATIONID_FLD = "PrintConfigurationID";
        public const string REPORTNAME_FLD = "ReportName";
        public const string REPORTNAMEJP_FLD = "ReportNameJP";
        public const string REPORTNAMEVN_FLD = "ReportNameVN";
        public const string TABLE_NAME = "Sys_PrintConfiguration";
    }

    public sealed class sys_RecordSecurityParamTable
    {
        public const string MENUNAME_FLD = "MenuName";
        public const string RECORDSECURITYPARAMID_FLD = "RecordSecurityParamID";
        public const string SECURITYTABLENAME_FLD = "SecurityTableName";
        public const string SOURCETABLENAME_FLD = "SourceTableName";
        public const string TABLE_NAME = "sys_RecordSecurityParam";
    }

    public sealed class sys_RelatedViewTable
    {
        public const string MAPFIELD_FLD = "MapField";
        public const string RECORDSECURITYPARAMID_FLD = "RecordSecurityParamID";
        public const string RELATEDVIEWID_FLD = "RelatedViewID";
        public const string TABLE_NAME = "sys_RelatedView";
        public const string TABLEORVIEW_FLD = "TableOrView";
    }

    public sealed class sys_ReportTable
    {
        public const string COMMAND_FLD = "Command";
        public const string CONVERTFONT_FLD = "ConvertFont";
        public const string DESCRIPTION_FLD = "Description";
        public const string FONTDETAIL_FLD = "FontDetail";
        public const string FONTGROUPBY_FLD = "FontGroupBy";
        public const string FONTPAGEFOOTER_FLD = "FontPageFooter";
        public const string FONTPAGEHEADER_FLD = "FontPageHeader";
        public const string FONTPARAMETER_FLD = "FontParameter";
        public const string FONTREPORTFOOTER_FLD = "FontReportFooter";
        public const string FONTREPORTHEADER_FLD = "FontReportHeader";
        public const string ISOCODE_FLD = "ISOCode";
        public const string MARGINBOTTOM_FLD = "MarginBottom";
        public const string MARGINGUTTER_FLD = "MarginGutter";
        public const string MARGINGUTTERPOS_FLD = "MarginGutterPos";
        public const string MARGINLEFT_FLD = "MarginLeft";
        public const string MARGINRIGHT_FLD = "MarginRight";
        public const string MARGINTOP_FLD = "MarginTop";
        public const string ORIENTATION_FLD = "Orientation";
        public const string PAPERSIZE_FLD = "PaperSize";
        public const string REPORTFILE_FLD = "ReportFile";
        public const string REPORTID_FLD = "ReportID";
        public const string REPORTNAME_FLD = "ReportName";
        public const string REPORTNAMEJP_FLD = "ReportNameJP";
        public const string REPORTNAMEVN_FLD = "ReportNameVN";
        public const string REPORTTYPE_FLD = "ReportType";
        public const string SIGNATURES_FLD = "Signatures";
        public const string TABLE_NAME = "sys_Report";
        public const string TABLEBORDER_FLD = "TableBorder";
        public const string TEMPLATEFILE_FLD = "TemplateFile";
        public const string USEEFFECTDB_FLD = "UseEffectDb";
        public const string USETEMPLATE_FLD = "UseTemplate";
    }

    public sealed class sys_ReportAndGroupTable
    {
        public const string GROUPID_FLD = "GroupID";
        public const string REPORTANDGROUPID_FLD = "ReportAndGroupID";
        public const string REPORTID_FLD = "ReportID";
        public const string REPORTORDER_FLD = "ReportOrder";
        public const string TABLE_NAME = "sys_ReportAndGroup";
    }

    public sealed class Sys_Report_RoleTable
    {
        public const string REPORT_ROLEID_FLD = "Report_RoleID";
        public const string REPORTID_FLD = "ReportID";
        public const string ROLEID_FLD = "RoleID";
        public const string TABLE_NAME = "Sys_Report_Role";
    }

    public sealed class SchemaTableTable
    {
        public const string TABLE_NAME = "INFORMATION_SCHEMA.TABLES";
        public const string TABLENAME_FLD = "TABLE_NAME";
        public const string TABLETYPE_FLD = "TABLE_TYPE";
    }

    public sealed class SchemaColumnTable
    {
        public const string COLUMN_NAME_FLD = "COLUMN_NAME";
        public const string ORDINAL_POSITION_FLD = "ORDINAL_POSITION";
        public const string TABLE_NAME = "INFORMATION_SCHEMA.COLUMNS";
        public const string TABLE_NAME_FLD = "TABLE_NAME";
    }

    public sealed class sys_ReportDrillDownTable
    {
        public const string DETAILPARA_FLD = "DetailPara";
        public const string DETAILREPORTID_FLD = "DetailReportID";
        public const string FROMCOLUMN_FLD = "FromColumn";
        public const string MASTERPARA_FLD = "MasterPara";
        public const string MASTERREPORTID_FLD = "MasterReportID";
        public const string PARAORDER_FLD = "ParaOrder";
        public const string REPORTDRILLDOWNID_FLD = "ReportDrillDownID";
        public const string TABLE_NAME = "sys_ReportDrillDown";
    }

    public sealed class sys_ReportFieldsTable
    {
        public const string ALIGN_FLD = "Align";
        public const string BOTTOMGROUP_FLD = "BottomGroup";
        public const string DATATYPE_FLD = "DataType";
        public const string FIELDCAPTION_FLD = "FieldCaption";
        public const string FIELDCAPTIONEN_FLD = "FieldCaptionEN";
        public const string FIELDCAPTIONJP_FLD = "FieldCaptionJP";
        public const string FIELDCAPTIONVN_FLD = "FieldCaptionVN";
        public const string FIELDNAME_FLD = "FieldName";
        public const string FIELDORDER_FLD = "FieldOrder";
        public const string FONT_FLD = "Font";
        public const string FORMAT_FLD = "Format";
        public const string GROUPBY_FLD = "GroupBy";
        public const string PRINTPREVIEW_FLD = "PrintPreview";
        public const string PRINTREASON_FLD = "PrintReason";
        public const string REPORTFORMAT_FLD = "ReportFormat";
        public const string REPORTID_FLD = "ReportID";
        public const string SORT_FLD = "Sort";
        public const string SUMBOTTOMPAGE_FLD = "SumBottomPage";
        public const string SUMBOTTOMREPORT_FLD = "SumBottomReport";
        public const string SUMTOPPAGE_FLD = "SumTopPage";
        public const string SUMTOPREPORT_FLD = "SumTopReport";
        public const string TABLE_NAME = "sys_ReportFields";
        public const string TYPE_FLD = "Type";
        public const string VISISBLE_FLD = "Visisble";
        public const string WIDTH_FLD = "Width";
    }

    public sealed class sys_ReportGroupTable
    {
        public const string GROUPID_FLD = "GroupID";
        public const string GROUPNAME_FLD = "GroupName";
        public const string GROUPNAMEJP_FLD = "GroupNameJP";
        public const string GROUPNAMEVN_FLD = "GroupNameVN";
        public const string GROUPORDER_FLD = "GroupOrder";
        public const string TABLE_NAME = "sys_ReportGroup";
    }

    public sealed class sys_ReportHistoryTable
    {
        public const string EXECDATETIME_FLD = "ExecDateTime";
        public const string HISTORYID_FLD = "HistoryID";
        public const string REPORTID_FLD = "ReportID";
        public const string TABLE_NAME = "sys_ReportHistory";
        public const string TABLENAME_FLD = "TableName";
        public const string USERID_FLD = "UserName";
    }

    public sealed class sys_ReportHistoryParaTable
    {
        public const string FILTERFIELD1VALUE_FLD = "FilterField1Value";
        public const string FILTERFIELD2VALUE_FLD = "FilterField2Value";
        public const string HISTORYID_FLD = "HistoryID";
        public const string PARANAME_FLD = "ParaName";
        public const string PARAVALUE_FLD = "ParaValue";
        public const string TABLE_NAME = "sys_ReportHistoryPara";
        public const string TAGVALUE_FLD = "TagValue";
    }

    public sealed class sys_ReportParaTable
    {
        public const string DATATYPE_FLD = "DataType";
        public const string DEFAULTVALUE_FLD = "DefaultValue";
        public const string FILTERFIELD1_FLD = "FilterField1";
        public const string FILTERFIELD1WIDTH_FLD = "FilterField1Width";
        public const string FILTERFIELD2_FLD = "FilterField2";
        public const string FILTERFIELD2WIDTH_FLD = "FilterField2Width";
        public const string FROMFIELD_FLD = "FromField";
        public const string FROMTABLE_FLD = "FromTable";
        public const string ITEMS_FLD = "Items";
        public const string MULTISELECTION_FLD = "MultiSelection";
        public const string OPTIONAL_FLD = "Optional";
        public const string PARACAPTION_FLD = "ParaCaption";
        public const string PARANAME_FLD = "ParaName";
        public const string PARANAMEJP_FLD = "ParaNameJP";
        public const string PARANAMEVN_FLD = "ParaNameVN";
        public const string PARAORDER_FLD = "ParaOrder";
        public const string REPORTID_FLD = "ReportID";
        public const string SAMEROW_FLD = "SameRow";
        public const string SQLCLAUSE_FLD = "SQLCLause";
        public const string TABLE_NAME = "sys_ReportPara";
        public const string TAGVALUE_FLD = "TagValue";
        public const string WHERECLAUSE_FLD = "WhereClause";
        public const string WIDTH_FLD = "Width";
    }

    public sealed class Sys_RightTable
    {
        public const string MENU_ENTRYID_FLD = "Menu_EntryID";
        public const string PERMISSION_FLD = "Permission";
        public const string RIGHTID_FLD = "RightID";
        public const string ROLEID_FLD = "RoleID";
        public const string TABLE_NAME = "Sys_Right";
    }

    public sealed class Sys_RoleTable
    {
        public const string DESCRIPTION_FLD = "Description";
        public const string NAME_FLD = "Name";
        public const string ROLEID_FLD = "RoleID";
        public const string TABLE_NAME = "Sys_Role";
    }

    public sealed class Sys_Role_ControlGroupTable
    {
        public const string CONTROLGROUPID_FLD = "ControlGroupID";
        public const string ROLE_CONTROLGROUPID_FLD = "Role_ControlGroupID";
        public const string ROLEID_FLD = "RoleID";
        public const string TABLE_NAME = "Sys_Role_ControlGroup";
    }

    public sealed class sys_RoleChartOfAccountTable
    {
        public const string CHARTOFACCOUNTID_FLD = "ChartOfAccountID";
        public const string ROLEID_FLD = "RoleID";
        public const string TABLE_NAME = "sys_RoleChartOfAccount";
    }

    public sealed class sys_RoleIssuePurposeTable
    {
        public const string ISSUEPURPOSEID_FLD = "IssuePurposeID";
        public const string ROLEID_FLD = "RoleID";
        public const string ROLEISSUEPURPOSEID_FLD = "RoleIssuePurposeID";
        public const string TABLE_NAME = "sys_RoleIssuePurpose";
    }

    public sealed class sys_RoleLocationTable
    {
        public const string LOCATIONID_FLD = "LocationID";
        public const string ROLEID_FLD = "RoleID";
        public const string ROLELOCATIONID_FLD = "RoleLocationID";
        public const string TABLE_NAME = "sys_RoleLocation";
    }

    public sealed class sys_RolePartyTable
    {
        public const string PARTYID_FLD = "PartyID";
        public const string ROLEID_FLD = "RoleID";
        public const string ROLEPARTYID_FLD = "RolePartyID";
        public const string TABLE_NAME = "sys_RoleParty";
    }

    public sealed class sys_RoleProductTable
    {
        public const string PRODUCTID_FLD = "ProductID";
        public const string ROLEID_FLD = "RoleID";
        public const string ROLEPRODUCTID_FLD = "RoleProductID";
        public const string TABLE_NAME = "sys_RoleProduct";
    }

    public sealed class sys_RoleProductionLineTable
    {
        public const string PRODUCTIONLINEID_FLD = "ProductionLineID";
        public const string ROLEID_FLD = "RoleID";
        public const string ROLEPRODUCTIONLINEID_FLD = "RoleProductionLineID";
        public const string TABLE_NAME = "sys_RoleProductionLine";
    }

    public sealed class sys_RoleWCTable
    {
        public const string ROLEID_FLD = "RoleID";
        public const string ROLEWCID_FLD = "RoleWCID";
        public const string TABLE_NAME = "sys_RoleWC";
        public const string WORKCENTERID_FLD = "WorkCenterID";
    }

    public sealed class sys_TableTable
    {
        public const string CODE_FLD = "Code";
        public const string HEIGHT_FLD = "Height";
        public const string ISVIEWONLY_FLD = "IsViewOnly";
        public const string TABLE_NAME = "Sys_Table";
        public const string TABLEID_FLD = "TableID";
        public const string TABLENAME_FLD = "TableName";
        public const string TABLENAMEJP_FLD = "TableNameJP";
        public const string TABLENAMEVN_FLD = "TableNameVN";
        public const string TABLEORVIEW_FLD = "TableOrView";
    }

    public sealed class sys_TableAndGroupTable
    {
        public const string TABLE_NAME = "Sys_TableAndGroup";
        public const string TABLEANDGROUPID_FLD = "TableAndGroupID";
        public const string TABLEGROUPID_FLD = "TableGroupID";
        public const string TABLEID_FLD = "TableID";
        public const string TABLEORDER_FLD = "TableOrder";
    }    
    public sealed class sys_TableFieldTable
    {
        public const string ALIGN_FLD = "Align";
        public const string ALIGNFIELD1_FLD = "AlignField1";
        public const string ALIGNFIELD2_FLD = "AlignField2";
        public const string ALIGNFIELD3_FLD = "AlignField3";
        public const string CAPTION_FLD = "Caption";
        public const string CAPTIONEN_FLD = "CaptionEN";
        public const string CAPTIONJP_FLD = "CaptionJP";
        public const string CAPTIONVN_FLD = "CaptionVN";
        public const string CHARACTERCASE_FLD = "CharacterCase";
        public const string FIELD1CAPTIONEN_FLD = "Field1CaptionEN";
        public const string FIELD1CAPTIONJP_FLD = "Field1CaptionJP";
        public const string FIELD1CAPTIONVN_FLD = "Field1CaptionVN";
        public const string FIELD2CAPTIONEN_FLD = "Field2CaptionEN";
        public const string FIELD2CAPTIONJP_FLD = "Field2CaptionJP";
        public const string FIELD2CAPTIONVN_FLD = "Field2CaptionVN";
        public const string FIELD3CAPTIONEN_FLD = "Field3CaptionEN";
        public const string FIELD3CAPTIONJP_FLD = "Field3CaptionJP";
        public const string FIELD3CAPTIONVN_FLD = "Field3CaptionVN";
        public const string FIELDNAME_FLD = "FieldName";
        public const string FIELDORDER_FLD = "FieldOrder";
        public const string FILTERFIELD1_FLD = "FilterField1";
        public const string FILTERFIELD2_FLD = "FilterField2";
        public const string FILTERFIELD3_FLD = "FilterField3";
        public const string FORMATFIELD1_FLD = "FormatField1";
        public const string FORMATFIELD2_FLD = "FormatField2";
        public const string FORMATFIELD3_FLD = "FormatField3";
        public const string FORMATS_FLD = "Formats";
        public const string FROMFIELD_FLD = "FromField";
        public const string FROMTABLE_FLD = "FromTable";
        public const string IDENTITYCOLUMN_FLD = "IdentityColumn";
        public const string INVISIBLE_FLD = "Invisible";
        public const string ITEMS_FLD = "Items";
        public const string NOTALLOWNULL_FLD = "NotAllowNull";
        public const string READONLY_FLD = "ReadOnly";
        public const string SORTTYPE_FLD = "SortType";
        public const string TABLE_NAME = "Sys_TableField";
        public const string TABLEFIELDID_FLD = "TableFieldID";
        public const string TABLEID_FLD = "TableID";
        public const string UNIQUECOLUMN_FLD = "UniqueColumn";
        public const string WIDTH_FLD = "Width";
        public const string WIDTHFIELD1_FLD = "WidthField1";
        public const string WIDTHFIELD2_FLD = "WidthField2";
        public const string WIDTHFIELD3_FLD = "WidthField3";
    }

    public sealed class sys_TableGroupTable
    {
        public const string CODE_FLD = "Code";
        public const string GROUPORDER_FLD = "GroupOrder";
        public const string TABLE_NAME = "Sys_TableGroup";
        public const string TABLEGROUPID_FLD = "TableGroupID";
        public const string TABLEGROUPNAME_FLD = "TableGroupName";
        public const string TABLEGROUPNAMEJP_FLD = "TableGroupNameJP";
        public const string TABLEGROUPNAMEVN_FLD = "TableGroupNameVN";
    }

    public sealed class Sys_UserTable
    {
        public const string ACTIVATE_FLD = "Activate";
        public const string CREATEDDATE_FLD = "CreatedDate";
        public const string DESCRIPTION_FLD = "Description";
        public const string EMPLOYEEID_FLD = "EmployeeID";
        public const string EXPIREDDATE_FLD = "ExpiredDate";
        public const string MASTERLOCATIONID_FLD = "MasterLocationID";
        public const string NAME_FLD = "Name";
        public const string PWD_FLD = "Pwd";
        public const string TABLE_NAME = "Sys_User";
        public const string USERID_FLD = "UserID";
        public const string USERNAME_FLD = "UserName";
        public const string CCNID_FLD = "CCNID";
    }

    public sealed class Sys_UserCCNTable
    {
        
        public const string TABLE_NAME = "Sys_UserCCN";
        public const string USERID_FLD = "UserID";
    }

    public sealed class Sys_UserToRoleTable
    {
        public const string ID_FLD = "id";
        public const string ROLEID_FLD = "RoleID";
        public const string TABLE_NAME = "Sys_UserToRole";
        public const string USERID_FLD = "UserID";
    }

    public sealed class Sys_VisibilityGroupTable
    {
        public const string CONTROLNAME_FLD = "ControlName";
        public const string FORMNAME_FLD = "FormName";
        public const string GROUPTEXT_FLD = "GroupText";
        public const string GROUPTEXTJP_FLD = "GroupTextJP";
        public const string GROUPTEXTVN_FLD = "GroupTextVN";
        public const string PARENTID_FLD = "ParentID";
        public const string TABLE_NAME = "sys_VisibilityGroup";
        public const string TYPE_FLD = "Type";
        public const string VISIBILITYGROUPID_FLD = "VisibilityGroupID";
    }

    public sealed class Sys_VisibilityGroup_RoleTable
    {
        public const string GROUPID_FLD = "GroupID";
        public const string ROLEID_FLD = "RoleID";
        public const string TABLE_NAME = "sys_VisibilityGroup_Role";
        public const string VISIBILITYGROUP_ROLEID_FLD = "VisibilityGroup_RoleID";
    }

    public sealed class Sys_VisibilityItemTable
    {
        public const string GROUPID_FLD = "GroupID";
        public const string NAME_FLD = "Name";
        public const string TABLE_NAME = "sys_VisibilityItem";
        public const string TYPE_FLD = "Type";
        public const string VISIBILITYITEMID_FLD = "VisibilityItemID";
    }

    public sealed class Sys_VisibleControlTable
    {
        public const string CONTROLGROUPID_FLD = "ControlGroupID";
        public const string CONTROLNAME_FLD = "ControlName";
        public const string FORMNAME_FLD = "FormName";
        public const string SUBCONTROLNAME_FLD = "SubControlName";
        public const string TABLE_NAME = "Sys_VisibleControl";
        public const string VISIBLECONTROLID_FLD = "VisibleControlID";
    }

    public sealed class sysdiagramsTable
    {
        public const string DEFINITION_FLD = "definition";
        public const string DIAGRAM_ID_FLD = "diagram_id";
        public const string NAME_FLD = "name";
        public const string PRINCIPAL_ID_FLD = "principal_id";
        public const string TABLE_NAME = "sysdiagrams";
        public const string VERSION_FLD = "version";
    }

    public sealed class sysFKeysTable
    {
        public const string FOREIGN_COLUMN_1_FLD = "foreign_column_1";
        public const string FOREIGN_COLUMN_2_FLD = "foreign_column_2";
        public const string FOREIGN_KEY_NAME_FLD = "foreign_key_name";
        public const string FOREIGN_TABLE_FLD = "foreign_table";
        public const string KEYCNT_FLD = "keycnt";
        public const string PRIMARY_COLUMN_1_FLD = "primary_column_1";
        public const string PRIMARY_COLUMN_2_FLD = "primary_column_2";
        public const string PRIMARY_TABLE_FLD = "primary_table";
        public const string TABLE_NAME = "sysFKeys";
    }

    public sealed class v_AccountInOpenTable
    {
        public const string ACCOUNTNAME_FLD = "AccountName";
        public const string ACCOUNTNAME_VN_FLD = "AccountName_VN";
        public const string CCNID_FLD = "CCNID";
        public const string CHARTOFACCOUNTID_FLD = "ChartOfAccountID";
        public const string CODE_FLD = "Code";
        public const string EFFECTDATE_FLD = "EffectDate";
        public const string OPENNINGBALANCEID_FLD = "OpenningBalanceID";
        public const string TABLE_NAME = "v_AccountInOpen";
    }

    public sealed class v_AccountNotInOpenTable
    {
        public const string ACCOUNTNAME_FLD = "AccountName";
        public const string ACCOUNTNAME_VN_FLD = "AccountName_VN";
        public const string CHARTOFACCOUNTID_FLD = "ChartOfAccountID";
        public const string CHARTOFACCSTRUCTID_FLD = "ChartOfAccStructID";
        public const string CODE_FLD = "Code";
        public const string TABLE_NAME = "v_AccountNotInOpen";
    }

    public sealed class v_ActualCostHistory_ElementTypeTable
    {
        public const string ACTCOSTALLOCATIONMASTERID_FLD = "ActCostAllocationMasterID";
        public const string ACTUALCOST_FLD = "ActualCost";
        public const string ACTUALCOSTHISTORY_FLD = "ActualCostHistory";
        public const string ADJUSTAMOUNT_FLD = "AdjustAmount";
        public const string BEGINCOST_FLD = "BeginCost";
        public const string BEGINQUANTITY_FLD = "BeginQuantity";
        public const string COMBEGINCOST_FLD = "ComBeginCost";
        public const string COMPONENTDSAMOUNT_FLD = "ComponentDSAmount";
        public const string COMPONENTVALUE_FLD = "ComponentValue";
        public const string COSTELEMENTID_FLD = "CostElementID";
        public const string COSTELEMENTTYPEID_FLD = "CostElementTypeID";
        public const string DS_OKAMOUNT_FLD = "DS_OKAmount";
        public const string DSAMOUNT_FLD = "DSAmount";
        public const string FREIGHTAMOUNT_FLD = "FreightAmount";
        public const string PRODUCTID_FLD = "ProductID";
        public const string QUANTITY_FLD = "Quantity";
        public const string RECOVERABLEAMOUNT_FLD = "RecoverableAmount";
        public const string RECYCLEAMOUNT_FLD = "RecycleAmount";
        public const string STDCOST_FLD = "StdCost";
        public const string TABLE_NAME = "v_ActualCostHistory_ElementType";
        public const string TRANSACTIONAMOUNT_FLD = "TransactionAmount";
        public const string WOCOMPLETIONQTY_FLD = "WOCompletionQty";
    }

    public sealed class v_AllocatedCostTable
    {
        public const string ACTCOSTALLOCATIONMASTERID_FLD = "ActCostAllocationMasterID";
        public const string ALLOCATEDAMOUNT_FLD = "AllocatedAmount";
        public const string COMPLETEDQUANTITY_FLD = "CompletedQuantity";
        public const string COSTELEMENT_FLD = "CostElement";
        public const string COSTELEMENTID_FLD = "CostElementID";
        public const string MODEL_FLD = "Model";
        public const string PRODUCTCODE_FLD = "ProductCode";
        public const string PRODUCTID_FLD = "ProductID";
        public const string PRODUCTNAME_FLD = "ProductName";
        public const string TABLE_NAME = "v_AllocatedCost";
        public const string UNITCOST_FLD = "UnitCost";
    }

    public sealed class v_ApprovedAndNotCompletedPOTable
    {
        public const string BUYERID_FLD = "BuyerID";
        public const string CARRIERID_FLD = "CarrierID";
        public const string CCNID_FLD = "CCNID";
        public const string CODE_FLD = "Code";
        public const string COMMENT_FLD = "Comment";
        public const string CURRENCYID_FLD = "CurrencyID";
        public const string DELIVERYTERMSID_FLD = "DeliveryTermsID";
        public const string DISCOUNTTERMID_FLD = "DiscountTermID";
        public const string EXCHANGERATE_FLD = "ExchangeRate";
        public const string IMPORTTAX_FLD = "ImportTax";
        public const string INVTOLOCID_FLD = "InvToLocID";
        public const string MASTERLOCATION_FLD = "MasterLocation";
        public const string MASTERLOCATIONID_FLD = "MasterLocationID";
        public const string ORDERDATE_FLD = "OrderDate";
        public const string PARTYCONTACTID_FLD = "PartyContactID";
        public const string PARTYID_FLD = "PartyID";
        public const string PAUSEID_FLD = "PauseID";
        public const string PAYMENTTERMSID_FLD = "PaymentTermsID";
        public const string POREVISION_FLD = "PORevision";
        public const string PRIORITY_FLD = "Priority";
        public const string PURCHASEORDERMASTERID_FLD = "PurchaseOrderMasterID";
        public const string PURCHASETYPEID_FLD = "PurchaseTypeID";
        public const string RECCOMPLETED_FLD = "RecCompleted";
        public const string SHIPTOLOCID_FLD = "ShipToLocID";
        public const string SPECIALTAX_FLD = "SpecialTax";
        public const string TABLE_NAME = "v_ApprovedAndNotCompletedPO";
        public const string TOTALAMOUNT_FLD = "TotalAmount";
        public const string TOTALDISCOUNT_FLD = "TotalDiscount";
        public const string TOTALIMPORTTAX_FLD = "TotalImportTax";
        public const string TOTALNETAMOUNT_FLD = "TotalNetAmount";
        public const string TOTALSPECIALTAX_FLD = "TotalSpecialTax";
        public const string TOTALVAT_FLD = "TotalVAT";
        public const string VAT_FLD = "VAT";
        public const string VENDORLOCID_FLD = "VendorLocID";
        public const string VENDORSO_FLD = "VendorSO";
    }

    public sealed class v_ApprovedAndNotCompletedPOLineTable
    {
        public const string AVAILABLEQUANTITY_FLD = "AvailableQuantity";
        public const string CODE_FLD = "Code";
        public const string LINE_FLD = "Line";
        public const string MASTERLOCATION_FLD = "MasterLocation";
        public const string MASTERLOCATIONID_FLD = "MasterLocationID";
        public const string PRODUCTID_FLD = "ProductID";
        public const string PURCHASEORDERDETAILID_FLD = "PurchaseOrderDetailID";
        public const string PURCHASEORDERMASTERID_FLD = "PurchaseOrderMasterID";
        public const string TABLE_NAME = "v_ApprovedAndNotCompletedPOLine";
        public const string UNITPRICE_FLD = "UnitPrice";
    }

    public sealed class v_BaoCaoQuanLyDonBanHangTable
    {
        public const string COMPLETEDQTY_FLD = "CompletedQty";
        public const string CONTACT_FLD = "Contact";
        public const string CUSTOMER_FLD = "Customer";
        public const string DUEDATE_FLD = "DueDate";
        public const string ORDERQTY_FLD = "OrderQty";
        public const string PARTNAME_FLD = "PartName";
        public const string PARTNO_FLD = "PartNo";
        public const string PARTYID_FLD = "PartyID";
        public const string PRODUCTID_FLD = "ProductID";
        public const string QUOTEQTY_FLD = "QuoteQty";
        public const string SALEORDERMASTERID_FLD = "SaleOrderMasterID";
        public const string SDH_FLD = "SDH";
        public const string SERVERDATE_FLD = "ServerDate";
        public const string STATUS_FLD = "Status";
        public const string TABLE_NAME = "v_BaoCaoQuanLyDonBanHang";
        public const string TRANSDATE_FLD = "TranSDate";
        public const string UM_FLD = "UM";
        public const string WOQTY_FLD = "WOQty";
    }

    public sealed class V_BIN_OK_AND_NGTable
    {
        public const string BINID_FLD = "BinID";
        public const string BINTYPEID_FLD = "BinTypeID";
        public const string CODE_FLD = "Code";
        public const string HEIGHT_FLD = "Height";
        public const string HEIGHTUNITID_FLD = "HeightUnitID";
        public const string LENGTH_FLD = "Length";
        public const string LENGTHUNITID_FLD = "LengthUnitID";
        public const string LOCATIONID_FLD = "LocationID";
        public const string LOCATIONTYPEID_FLD = "LocationTypeID";
        public const string NAME_FLD = "Name";
        public const string TABLE_NAME = "V_BIN_OK_AND_NG";
        public const string WIDTH_FLD = "Width";
        public const string WIDTHUNITID_FLD = "WidthUnitID";
    }

    public sealed class v_BinExceptDestroyTable
    {
        public const string BINID_FLD = "BinID";
        public const string BINTYPEID_FLD = "BinTypeID";
        public const string CODE_FLD = "Code";
        public const string LOCATIONID_FLD = "LocationID";
        public const string NAME_FLD = "Name";
        public const string TABLE_NAME = "v_BinExceptDestroy";
    }

    public sealed class V_BinItemTable
    {
        public const string AVAILQUANTITY_FLD = "AvailQuantity";
        public const string BINID_FLD = "BinID";
        public const string CODE_FLD = "Code";
        public const string INSPSTATUS_FLD = "InspStatus";
        public const string LOCATIONID_FLD = "LocationID";
        public const string PRODUCTCODE_FLD = "ProductCode";
        public const string PRODUCTID_FLD = "ProductID";
        public const string TABLE_NAME = "V_BinItem";
    }

    public sealed class V_BinItemSumAvailTable
    {
        public const string AVAILQUANTITY_FLD = "AvailQuantity";
        public const string BINID_FLD = "BinID";
        public const string CODE_FLD = "Code";
        public const string INSPSTATUS_FLD = "InspStatus";
        public const string LOCATIONID_FLD = "LocationID";
        public const string PRODUCTID_FLD = "ProductID";
        public const string TABLE_NAME = "V_BinItemSumAvail";
    }

    public sealed class v_BinNoSecurityTable
    {
        public const string BINID_FLD = "BinID";
        public const string BINTYPEID_FLD = "BinTypeID";
        public const string CODE_FLD = "Code";
        public const string HEIGHT_FLD = "Height";
        public const string HEIGHTUNITID_FLD = "HeightUnitID";
        public const string LENGTH_FLD = "Length";
        public const string LENGTHUNITID_FLD = "LengthUnitID";
        public const string LOCATIONID_FLD = "LocationID";
        public const string LOCATIONTYPEID_FLD = "LocationTypeID";
        public const string NAME_FLD = "Name";
        public const string TABLE_NAME = "v_BinNoSecurity";
        public const string WIDTH_FLD = "Width";
        public const string WIDTHUNITID_FLD = "WidthUnitID";
    }

    public sealed class v_CategoryOfProductionLineTable
    {
        public const string CATEGORYID_FLD = "CategoryID";
        public const string CODE_FLD = "Code";
        public const string DESCRIPTION_FLD = "Description";
        public const string PRODUCTIONLINEID_FLD = "ProductionLineID";
        public const string TABLE_NAME = "v_CategoryOfProductionLine";
    }

    public sealed class v_CGS1Table
    {
        public const string ACTCOSTALLOCATIONMASTERID_FLD = "ActCostAllocationMasterID";
        public const string COSTELEMENTID_FLD = "CostElementID";
        public const string TABLE_NAME = "v_CGS1";
        public const string TOTALCGS1_FLD = "TotalCGS1";
    }

    public sealed class v_CloseOrOpenPOTable
    {
        public const string CCNID_FLD = "CCNID";
        public const string CLOSED_FLD = "Closed";
        public const string CODE_FLD = "Code";
        public const string DESCRIPTION_FLD = "Description";
        public const string LINE_FLD = "Line";
        public const string MASTERLOCATIONID_FLD = "MasterLocationID";
        public const string ORDERQUANTITY_FLD = "OrderQuantity";
        public const string PURCHASEORDERDETAILID_FLD = "PurchaseOrderDetailID";
        public const string PURCHASEORDERMASTERID_FLD = "PurchaseOrderMasterID";
        public const string PURCHASEORDERNO_FLD = "PurchaseOrderNo";
        public const string REVISION_FLD = "Revision";
        public const string SCHEDULEDATE_FLD = "ScheduleDate";
        public const string SEL_FLD = "Sel";
        public const string STATUS_FLD = "Status";
        public const string TABLE_NAME = "v_CloseOrOpenPO";
        public const string UM_FLD = "UM";
        public const string VENDORCODE_FLD = "VendorCode";
        public const string VENDORNAME_FLD = "VendorName";
    }

    public sealed class v_CommitMasterToShippingTable
    {
        public const string COMMENT_FLD = "Comment";
        public const string COMMITDATE_FLD = "CommitDate";
        public const string COMMITINVENTORYMASTERID_FLD = "CommitInventoryMasterID";
        public const string COMMITMENTNO_FLD = "CommitmentNo";
        public const string CONTACT_FLD = "Contact";
        public const string DELIVERYDATE_FLD = "DeliveryDate";
        public const string EMPLOYEEID_FLD = "EmployeeID";
        public const string SALEORDERMASTERID_FLD = "SaleOrderMasterID";
        public const string TABLE_NAME = "v_CommitMasterToShipping";
        public const string USERNAME_FLD = "UserName";
    }

    public sealed class v_ConfirmShipByCustomerTable
    {
        public const string CODE_FLD = "Code";
        public const string CONFIRMSHIPMASTERID_FLD = "ConfirmShipMasterID";
        public const string CONFIRMSHIPNO_FLD = "ConfirmShipNo";
        public const string PARTYID_FLD = "PartyID";
        public const string TABLE_NAME = "v_ConfirmShipByCustomer";
    }

    public sealed class v_CostElement_MaterialTable
    {
        public const string ACTCOSTALLOCATIONMASTERID_FLD = "ActCostAllocationMasterID";
        public const string BEGINQUANTITY_FLD = "BeginQuantity";
        public const string COMPLETEDQTY_FLD = "CompletedQty";
        public const string MATERIAL_FLD = "Material";
        public const string PRODUCTID_FLD = "ProductID";
        public const string QUANTITY_FLD = "Quantity";
        public const string TABLE_NAME = "v_CostElement_Material";
    }

    public sealed class v_CustomerTable
    {
        public const string ADDRESS_FLD = "Address";
        public const string BANKACCOUNT_FLD = "BankAccount";
        public const string CITYID_FLD = "CityID";
        public const string CODE_FLD = "Code";
        public const string COUNTRYID_FLD = "CountryID";
        public const string CURRENCYID_FLD = "CurrencyID";
        public const string DEBTLIMIT_FLD = "DebtLimit";
        public const string DELETEREASON_FLD = "DeleteReason";
        public const string FAX_FLD = "Fax";
        public const string MAPBANKACCOUNTNAME_FLD = "MAPBankAccountName";
        public const string MAPBANKACCOUNTNO_FLD = "MAPBankAccountNo";
        public const string NAME_FLD = "Name";
        public const string PARTYID_FLD = "PartyID";
        public const string PAYMENTTERMID_FLD = "PaymentTermID";
        public const string PHONE_FLD = "Phone";
        public const string STATE_FLD = "State";
        public const string TABLE_NAME = "v_Customer";
        public const string TYPE_FLD = "Type";
        public const string VATCODE_FLD = "VATCode";
        public const string WEBSITE_FLD = "WebSite";
        public const string ZIPPOST_FLD = "ZipPost";
    }

    public sealed class v_DetailedPOInvoiceMasterTable
    {
        public const string BLDATE_FLD = "BLDate";
        public const string BLNUMBER_FLD = "BLNumber";
        public const string CARRIERID_FLD = "CarrierID";
        public const string CCNID_FLD = "CCNID";
        public const string CURRENCYID_FLD = "CurrencyID";
        public const string DECLARATIONDATE_FLD = "DeclarationDate";
        public const string DELIVERYTERMID_FLD = "DeliveryTermID";
        public const string EXCHANGERATE_FLD = "ExchangeRate";
        public const string INFORMDATE_FLD = "InformDate";
        public const string INVOICEMASTERID_FLD = "InvoiceMasterID";
        public const string INVOICENO_FLD = "InvoiceNo";
        public const string MST_CARRIERCODE_FLD = "MST_CarrierCode";
        public const string MST_CURRENCYCODE_FLD = "MST_CurrencyCode";
        public const string MST_DELIVERYTERMCODE_FLD = "MST_DeliveryTermCode";
        public const string MST_PARTYCODE_FLD = "MST_PartyCode";
        public const string MST_PARTYNAME_FLD = "MST_PartyName";
        public const string MST_PAYMENTTERMCODE_FLD = "MST_PaymentTermCode";
        public const string PARTNAME_FLD = "PartName";
        public const string PARTNO_FLD = "PartNo";
        public const string PARTYID_FLD = "PartyID";
        public const string PAYMENTTERMID_FLD = "PaymentTermID";
        public const string PO_PURCHASETYPECODE_FLD = "PO_PurchaseTypeCode";
        public const string POSTDATE_FLD = "PostDate";
        public const string QASTATUS_FLD = "QAStatus";
        public const string TABLE_NAME = "v_DetailedPOInvoiceMaster";
        public const string TAXDECLARATIONNUMBER_FLD = "TaxDeclarationNumber";
        public const string TAXINFORMNUMBER_FLD = "TaxInformNumber";
        public const string TOTALBEFOREVATAMOUNT_FLD = "TotalBeforeVATAmount";
        public const string TOTALCIFAMOUNT_FLD = "TotalCIFAmount";
        public const string TOTALCIPAMOUNT_FLD = "TotalCIPAmount";
        public const string TOTALIMPORTTAX_FLD = "TotalImportTax";
        public const string TOTALINLANDAMOUNT_FLD = "TotalInlandAmount";
        public const string TOTALVATAMOUNT_FLD = "TotalVATAmount";
    }

    public sealed class v_DS_Recycle_AfterAllocateTable
    {
        public const string ACTCOSTALLOCATIONMASTERID_FLD = "ActCostAllocationMasterID";
        public const string ADJUSTAMOUNT_FLD = "AdjustAmount";
        public const string DSAMOUNT_FLD = "DSAmount";
        public const string FROMDATE_FLD = "FromDate";
        public const string PRODUCTID_FLD = "ProductID";
        public const string RECYCLEAMOUNT_FLD = "RecycleAmount";
        public const string TABLE_NAME = "v_DS_Recycle_AfterAllocate";
        public const string TODATE_FLD = "ToDate";
    }

    public sealed class v_DS_Recycle_AfterAllocateByElementTable
    {
        public const string ACTCOSTALLOCATIONMASTERID_FLD = "ActCostAllocationMasterID";
        public const string ADJUSTAMOUNT_FLD = "AdjustAmount";
        public const string COSTELEMENTID_FLD = "CostElementID";
        public const string DSAMOUNT_FLD = "DSAmount";
        public const string FROMDATE_FLD = "FromDate";
        public const string PRODUCTID_FLD = "ProductID";
        public const string RECYCLEAMOUNT_FLD = "RecycleAmount";
        public const string TABLE_NAME = "v_DS_Recycle_AfterAllocateByElement";
        public const string TODATE_FLD = "ToDate";
    }

    public sealed class v_EmployeeHasProfileTable
    {
        public const string CODE_FLD = "Code";
        public const string DEPARTMENTID_FLD = "DepartmentID";
        public const string EMPLOYEEID_FLD = "EmployeeID";
        public const string NAME_FLD = "Name";
        public const string SHIFT_FLD = "Shift";
        public const string TABLE_NAME = "v_EmployeeHasProfile";
    }

    public sealed class v_EmployeeNoProfileTable
    {
        public const string CODE_FLD = "Code";
        public const string DEPARTMENTID_FLD = "DepartmentID";
        public const string EMPLOYEEID_FLD = "EmployeeID";
        public const string NAME_FLD = "Name";
        public const string SHIFT_FLD = "Shift";
        public const string TABLE_NAME = "v_EmployeeNoProfile";
    }

    public sealed class v_GetAvgCommitCostTable
    {
        public const string AVGCOST_FLD = "AVGCost";
        public const string PRODUCTID_FLD = "ProductID";
        public const string SALEORDERMASTERID_FLD = "SaleOrderMasterID";
        public const string TABLE_NAME = "v_GetAvgCommitCost";
    }

    public sealed class v_GetSaleOrderTotalCommitTable
    {
        public const string AVGCOST_FLD = "AVGCost";
        public const string DESCRIPTION_FLD = "Description";
        public const string LOT_FLD = "Lot";
        public const string PRODUCTCODE_FLD = "ProductCode";
        public const string PRODUCTID_FLD = "ProductID";
        public const string REVISION_FLD = "Revision";
        public const string SALEORDERDETAILID_FLD = "SaleOrderDetailID";
        public const string SALEORDERMASTERID_FLD = "SaleOrderMasterID";
        public const string SELLINGUMID_FLD = "SellingUMID";
        public const string SERIAL_FLD = "Serial";
        public const string TABLE_NAME = "v_GetSaleOrderTotalCommit";
        public const string TOTALCOMMIT_FLD = "TotalCommit";
    }

    public sealed class v_GetSaleOrderTotalInvCommitTable
    {
        public const string AVGCOST_FLD = "AVGCost";
        public const string BALANCEQTY_FLD = "BalanceQty";
        public const string BIN_FLD = "Bin";
        public const string BINID_FLD = "BinID";
        public const string CODE_FLD = "Code";
        public const string DESCRIPTION_FLD = "Description";
        public const string LOCATIONID_FLD = "LocationID";
        public const string LOT_FLD = "Lot";
        public const string MASTERLOCATIONID_FLD = "MasterLocationID";
        public const string MST_BINCODE_FLD = "MST_BinCode";
        public const string MST_LOCATIONCODE_FLD = "MST_LocationCode";
        public const string MST_MASTERLOCATIONCODE_FLD = "MST_MasterLocationCode";
        public const string MST_UNITOFMEASURECODE_FLD = "MST_UnitOfMeasureCode";
        public const string PRODUCTID_FLD = "ProductID";
        public const string QASTATUS_FLD = "QAStatus";
        public const string REVISION_FLD = "Revision";
        public const string SALEORDERDETAILID_FLD = "SaleOrderDetailID";
        public const string SALEORDERMASTERID_FLD = "SaleOrderMasterID";
        public const string SELLINGUMID_FLD = "SellingUMID";
        public const string SERIAL_FLD = "Serial";
        public const string STOCKUMID_FLD = "StockUMID";
        public const string TABLE_NAME = "v_GetSaleOrderTotalInvCommit";
        public const string UNITPRICE_FLD = "UnitPrice";
    }

    public sealed class v_InOutStockTable
    {
        public const string BEGINQTY_FLD = "BeginQty";
        public const string BINID_FLD = "BinID";
        public const string DAYS_FLD = "Days";
        public const string INQTY_FLD = "INQty";
        public const string LOCATIONID_FLD = "LocationID";
        public const string MONTHS_FLD = "Months";
        public const string OUTQTY_FLD = "OUTQty";
        public const string PRODUCTID_FLD = "ProductID";
        public const string TABLE_NAME = "v_InOutStock";
        public const string TRANTYPEID_FLD = "TrantypeID";
        public const string YEARS_FLD = "Years";
    }

    public sealed class v_InOutStockForAccountingTable
    {
        public const string BEGINCOST_FLD = "BeginCost";
        public const string BIN_FLD = "Bin";
        public const string BINID_FLD = "BinID";
        public const string BINTYPEID_FLD = "BinTypeID";
        public const string CATEGORY_FLD = "Category";
        public const string INCOST_FLD = "InCost";
        public const string INQTY_FLD = "INQty";
        public const string LOCATION_FLD = "Location";
        public const string LOCATIONID_FLD = "LocationID";
        public const string MODEL_FLD = "Model";
        public const string MONTHS_FLD = "Months";
        public const string OUTCOST_FLD = "OUTCost";
        public const string OUTQTY_FLD = "OUTQty";
        public const string PARTNAME_FLD = "PartName";
        public const string PARTNO_FLD = "PartNo";
        public const string PRODUCTID_FLD = "ProductID";
        public const string TABLE_NAME = "v_InOutStockForAccounting";
        public const string TRANTYPE_FLD = "TranType";
        public const string TRANTYPEID_FLD = "TranTypeID";
        public const string UM_FLD = "UM";
        public const string YEARS_FLD = "Years";
    }

    public sealed class V_InOutStockReport_GroupByBinTypeTable
    {
        public const string BEGINCOST_FLD = "BeginCost";
        public const string BEGINQTY_FLD = "BeginQty";
        public const string BINID_FLD = "BinID";
        public const string INPUTAMOUNT_FLD = "InputAmount";
        public const string INPUTQTY_FLD = "InputQty";
        public const string OUTPUTAMOUNT_FLD = "OutputAmount";
        public const string OUTPUTQTY_FLD = "OutputQty";
        public const string POSTDATE_FLD = "PostDate";
        public const string PRODUCTID_FLD = "ProductID";
        public const string TABLE_NAME = "V_InOutStockReport_GroupByBinType";
    }

    public sealed class V_InOutStockReport_GroupByBinType_1Table
    {
        public const string BEGINCOST_FLD = "BeginCost";
        public const string BEGINQTY_FLD = "BeginQty";
        public const string BINID_FLD = "BinID";
        public const string INPUTAMOUNT_FLD = "InputAmount";
        public const string INPUTQTY_FLD = "InputQty";
        public const string OUTPUTAMOUNT_FLD = "OutputAmount";
        public const string OUTPUTQTY_FLD = "OutputQty";
        public const string POSTDATE_FLD = "PostDate";
        public const string PRODUCTID_FLD = "ProductID";
        public const string TABLE_NAME = "V_InOutStockReport_GroupByBinType_1";
    }

    public sealed class v_InputInPeriodTable
    {
        public const string AMOUNT1_FLD = "Amount1";
        public const string AMOUNT2_FLD = "Amount2";
        public const string BINID_FLD = "BinID";
        public const string LOCATIONID_FLD = "LocationID";
        public const string MONTHS_FLD = "Months";
        public const string PRODUCTID_FLD = "ProductID";
        public const string QTY_FLD = "Qty";
        public const string QTY2_FLD = "Qty2";
        public const string TABLE_NAME = "v_InputInPeriod";
        public const string YEARS_FLD = "Years";
    }

    public sealed class v_InvoiceTable
    {
        public const string CIFAMOUNT_FLD = "CIFAmount";
        public const string CIPAMOUNT_FLD = "CIPAmount";
        public const string IMPORTTAX_FLD = "ImportTax";
        public const string IMPORTTAXAMOUNT_FLD = "ImportTaxAmount";
        public const string INLAND_FLD = "Inland";
        public const string INVOICEMASTERID_FLD = "InvoiceMasterID";
        public const string INVOICENO_FLD = "InvoiceNo";
        public const string INVOICEQUANTITY_FLD = "InvoiceQuantity";
        public const string PARTYID_FLD = "PartyID";
        public const string POSTDATE_FLD = "PostDate";
        public const string PRODUCTID_FLD = "ProductID";
        public const string TABLE_NAME = "v_Invoice";
        public const string UNITPRICE_FLD = "UnitPrice";
        public const string VAT_FLD = "VAT";
        public const string VATAMOUNT_FLD = "VATAmount";
    }

    public sealed class V_InvoiceMasterNotReceivingTable
    {
        public const string BLDATE_FLD = "BLDate";
        public const string BLNUMBER_FLD = "BLNumber";
        public const string CARRIERID_FLD = "CarrierID";
        public const string CCNID_FLD = "CCNID";
        public const string CURRENCYID_FLD = "CurrencyID";
        public const string DECLARATIONDATE_FLD = "DeclarationDate";
        public const string DELIVERYTERMID_FLD = "DeliveryTermID";
        public const string EXCHANGERATE_FLD = "ExchangeRate";
        public const string INFORMDATE_FLD = "InformDate";
        public const string INVOICEMASTERID_FLD = "InvoiceMasterID";
        public const string INVOICENO_FLD = "InvoiceNo";
        public const string LASTCHANGE_FLD = "LastChange";
        public const string PARTYID_FLD = "PartyID";
        public const string PAYMENTTERMID_FLD = "PaymentTermID";
        public const string POSTDATE_FLD = "PostDate";
        public const string TABLE_NAME = "V_InvoiceMasterNotReceiving";
        public const string TAXDECLARATIONNUMBER_FLD = "TaxDeclarationNumber";
        public const string TAXINFORMNUMBER_FLD = "TaxInformNumber";
        public const string TOTALBEFOREVATAMOUNT_FLD = "TotalBeforeVATAmount";
        public const string TOTALCIFAMOUNT_FLD = "TotalCIFAmount";
        public const string TOTALCIPAMOUNT_FLD = "TotalCIPAmount";
        public const string TOTALIMPORTTAX_FLD = "TotalImportTax";
        public const string TOTALINLANDAMOUNT_FLD = "TotalInlandAmount";
        public const string TOTALVATAMOUNT_FLD = "TotalVATAmount";
        public const string USERNAME_FLD = "UserName";
    }

    public sealed class v_InvoiceToReturnTable
    {
        public const string CURRENCYID_FLD = "CurrencyID";
        public const string EXCHANGERATE_FLD = "ExchangeRate";
        public const string INVOICEMASTERID_FLD = "InvoiceMasterID";
        public const string INVOICENO_FLD = "InvoiceNo";
        public const string MST_CURRENCYCODE_FLD = "MST_CurrencyCode";
        public const string MST_PARTYLOCATIONCODE_FLD = "MST_PartyLocationCode";
        public const string PARTYID_FLD = "PartyID";
        public const string PARTYLOCATIONID_FLD = "PartyLocationID";
        public const string POSTDATE_FLD = "PostDate";
        public const string TABLE_NAME = "v_InvoiceToReturn";
        public const string TOTALBEFOREVATAMOUNT_FLD = "TotalBeforeVATAmount";
        public const string TOTALCIFAMOUNT_FLD = "TotalCIFAmount";
        public const string TOTALCIPAMOUNT_FLD = "TotalCIPAmount";
        public const string TOTALIMPORTTAX_FLD = "TotalImportTax";
        public const string TOTALINLANDAMOUNT_FLD = "TotalInlandAmount";
        public const string TOTALVATAMOUNT_FLD = "TotalVATAmount";
        public const string VENDORCODE_FLD = "VendorCode";
        public const string VENDORNAME_FLD = "VendorName";
    }

    public sealed class v_IssueMaterialDetailByProductTable
    {
        public const string CODE_FLD = "Code";
        public const string DESCRIPTION_FLD = "Description";
        public const string ISSUEMATERIALDETAILID_FLD = "IssueMaterialDetailID";
        public const string ISSUENO_FLD = "IssueNo";
        public const string LOT_FLD = "Lot";
        public const string PRODUCTID_FLD = "ProductID";
        public const string QASTATUS_FLD = "QAStatus";
        public const string REVISION_FLD = "Revision";
        public const string SERIAL_FLD = "Serial";
        public const string STOCKUMID_FLD = "StockUMID";
        public const string TABLE_NAME = "v_IssueMaterialDetailByProduct";
        public const string UM_FLD = "UM";
        public const string WORKORDERDETAILID_FLD = "WorkOrderDetailID";
    }

    public sealed class v_ItemInSaleTable
    {
        public const string CODE_FLD = "Code";
        public const string DELIVERYQUANTITY_FLD = "DeliveryQuantity";
        public const string DELIVERYSCHEDULEID_FLD = "DeliveryScheduleID";
        public const string DESCRIPTION_FLD = "Description";
        public const string NETAMOUNT_FLD = "NetAmount";
        public const string PRODUCTID_FLD = "ProductID";
        public const string SALEORDERDETAILID_FLD = "SaleOrderDetailID";
        public const string SALEORDERMASTERID_FLD = "SaleOrderMasterID";
        public const string SCHEDULEDATE_FLD = "ScheduleDate";
        public const string STOCKUMID_FLD = "StockUMID";
        public const string TABLE_NAME = "v_ItemInSale";
        public const string UM_FLD = "UM";
        public const string UNITPRICE_FLD = "UnitPrice";
        public const string VATAMOUNT_FLD = "VATAmount";
        public const string VATPERCENT_FLD = "VATPercent";
    }

    public sealed class v_ItemIssueSubMaterialTable
    {
        public const string CATEGORYID_FLD = "CategoryID";
        public const string CODE_FLD = "Code";
        public const string DESCRIPTION_FLD = "Description";
        public const string PRODUCTID_FLD = "ProductID";
        public const string PRODUCTIONLINEID_FLD = "ProductionLineID";
        public const string REVISION_FLD = "Revision";
        public const string SALEORDERDETAILID_FLD = "SaleOrderDetailID";
        public const string STOCKUMID_FLD = "StockUMID";
        public const string TABLE_NAME = "v_ItemIssueSubMaterial";
    }

    public sealed class v_ItemPOTable
    {
        public const string CATEGORYID_FLD = "CategoryID";
        public const string CODE_FLD = "Code";
        public const string DESCRIPTION_FLD = "Description";
        public const string PARTYID_FLD = "PartyID";
        public const string PRODUCTID_FLD = "ProductID";
        public const string REVISION_FLD = "Revision";
        public const string TABLE_NAME = "v_ItemPO";
        public const string UNITPRICE_FLD = "UnitPrice";
    }

    public sealed class v_ITM_BOM_PlanningTable
    {
        public const string CODE_FLD = "Code";
        public const string COMPONENTID_FLD = "componentID";
        public const string DESCRIPTION_FLD = "Description";
        public const string EFFECTIVEBEGINDATE_FLD = "EffectiveBeginDate";
        public const string EFFECTIVEENDDATE_FLD = "EffectiveEndDate";
        public const string LEADTIMEOFFSET_FLD = "LeadTimeOffset";
        public const string LTFIXEDTIME_FLD = "LTFixedTime";
        public const string LTORDERPREPARE_FLD = "LTOrderPrepare";
        public const string LTSALESATP_FLD = "LTSalesATP";
        public const string LTSHIPPINGPREPARE_FLD = "LTShippingPrepare";
        public const string LTVARIABLETIME_FLD = "LTVariableTime";
        public const string PARTNUMBER_FLD = "PartNumber";
        public const string PRODUCTID_FLD = "ProductID";
        public const string QUANTITY_FLD = "quantity";
        public const string REVISION_FLD = "Revision";
        public const string SHRINK_FLD = "Shrink";
        public const string TABLE_NAME = "v_ITM_BOM_Planning";
    }

    public sealed class v_ITMBOM_ProductTable
    {
        public const string BOMDESCRIPTION_FLD = "BOMDescription";
        public const string BOMINCREMENT_FLD = "BomIncrement";
        public const string CATEGORYID_FLD = "CategoryID";
        public const string CCNID_FLD = "CCNID";
        public const string CODE_FLD = "Code";
        public const string DESCRIPTION_FLD = "Description";
        public const string HASBOM_FLD = "HasBOM";
        public const string MAKEITEM_FLD = "MakeItem";
        public const string PRIMARYVENDORID_FLD = "PrimaryVendorID";
        public const string PRODUCTID_FLD = "ProductID";
        public const string REVISION_FLD = "Revision";
        public const string TABLE_NAME = "v_ITMBOM_Product";
    }

    public sealed class v_ITMSaleOderDetailWithProductTable
    {
        public const string CCNID_FLD = "CCNID";
        public const string CODE_FLD = "Code";
        public const string DESCRIPTION_FLD = "Description";
        public const string FGOODSCODE_FLD = "FGoodsCode";
        public const string MAKEITEM_FLD = "MakeItem";
        public const string ORDERQUANTITY_FLD = "OrderQuantity";
        public const string PARTNUMBER_FLD = "PartNumber";
        public const string PRODUCTID_FLD = "ProductID";
        public const string PRODUCTIONLINEID_FLD = "ProductionLineID";
        public const string QASTATUS_FLD = "QAStatus";
        public const string REVISION_FLD = "Revision";
        public const string SALEORDERDETAILID_FLD = "SaleOrderDetailID";
        public const string SHIPCOMPLETED_FLD = "ShipCompleted";
        public const string STOCKUMID_FLD = "StockUMID";
        public const string TABLE_NAME = "v_ITMSaleOderDetailWithProduct";
        public const string WOQUANTITY_FLD = "WOQuantity";
    }

    public sealed class v_IVAdjustmentAndProductTable
    {
        public const string ADJUSTMENTID_FLD = "AdjustmentID";
        public const string ADJUSTQUANTITY_FLD = "AdjustQuantity";
        public const string AVAILABLEQTY_FLD = "AvailableQty";
        public const string BINID_FLD = "BinID";
        public const string BINNAME_FLD = "BinName";
        public const string CATEGORYID_FLD = "CategoryID";
        public const string CCNID_FLD = "CCNID";
        public const string COMMENT_FLD = "Comment";
        public const string LOCATIONID_FLD = "LocationID";
        public const string LOCNAME_FLD = "LocName";
        public const string LOT_FLD = "Lot";
        public const string MASLOCNAME_FLD = "MasLocName";
        public const string MASTERLOCATIONID_FLD = "MasterLocationID";
        public const string MODEL_FLD = "Model";
        public const string PARTNAME_FLD = "PartName";
        public const string PARTNUMBER_FLD = "PartNumber";
        public const string POSTDATE_FLD = "PostDate";
        public const string PRODUCTID_FLD = "ProductID";
        public const string SERIAL_FLD = "Serial";
        public const string STOCKUMID_FLD = "StockUMID";
        public const string TABLE_NAME = "v_IVAdjustmentAndProduct";
        public const string TRANSNO_FLD = "TransNo";
        public const string UM_FLD = "UM";
        public const string USEDBYCOSTING_FLD = "UsedByCosting";
    }

    public sealed class v_LeadTimeByMainWorkCenterTable
    {
        public const string CAPACITY_FLD = "Capacity";
        public const string CATEGORYID_FLD = "CategoryID";
        public const string CODE_FLD = "Code";
        public const string ISMAIN_FLD = "IsMain";
        public const string LEADTIME_FLD = "LeadTime";
        public const string PRODUCTID_FLD = "ProductID";
        public const string QUANTITYSET_FLD = "QuantitySet";
        public const string TABLE_NAME = "v_LeadTimeByMainWorkCenter";
        public const string WORKCENTERID_FLD = "WorkCenterID";
    }

    public sealed class v_LeafCostElementTable
    {
        public const string CODE_FLD = "Code";
        public const string COSTELEMENTID_FLD = "CostElementID";
        public const string COSTELEMENTTYPEID_FLD = "CostElementTypeID";
        public const string ELEMENTTYPE_FLD = "ElementType";
        public const string ISLEAF_FLD = "IsLeaf";
        public const string NAME_FLD = "Name";
        public const string ORDERNO_FLD = "OrderNo";
        public const string PARENTID_FLD = "ParentID";
        public const string TABLE_NAME = "v_LeafCostElement";
    }

    public sealed class v_LocalReceiveTable
    {
        public const string EXCHANGERATE_FLD = "ExchangeRate";
        public const string INVENTORID_FLD = "InventorID";
        public const string MAKERID_FLD = "MakerID";
        public const string PARTYID_FLD = "PartyID";
        public const string POSTDATE_FLD = "PostDate";
        public const string PRODUCTID_FLD = "ProductID";
        public const string PURCHASEORDERMASTERID_FLD = "PurchaseOrderMasterID";
        public const string RECEIVENO_FLD = "ReceiveNo";
        public const string RECEIVEQUANTITY_FLD = "ReceiveQuantity";
        public const string TABLE_NAME = "v_LocalReceive";
        public const string UNITPRICE_FLD = "UnitPrice";
        public const string VAT_FLD = "VAT";
    }

    public sealed class V_LocalVendorTable
    {
        public const string ADDRESS_FLD = "Address";
        public const string CODE_FLD = "Code";
        public const string FAX_FLD = "Fax";
        public const string NAME_FLD = "Name";
        public const string PARTYID_FLD = "PartyID";
        public const string PHONE_FLD = "Phone";
        public const string TABLE_NAME = "V_LocalVendor";
        public const string WEBSITE_FLD = "Website";
    }

    public sealed class V_LocationAndProductionLineTable
    {
        public const string BIN_FLD = "Bin";
        public const string CODE_FLD = "Code";
        public const string LOCATIONID_FLD = "LocationID";
        public const string LOCATIONTYPEID_FLD = "LocationTypeID";
        public const string MANUFACTURINGACCESS_FLD = "ManufacturingAccess";
        public const string MASTERLOCATIONID_FLD = "MasterLocationID";
        public const string NAME_FLD = "Name";
        public const string PRODUCTIONLINEID_FLD = "ProductionLineID";
        public const string SALEACCESS_FLD = "SaleAccess";
        public const string TABLE_NAME = "V_LocationAndProductionLine";
        public const string TYPE_FLD = "Type";
    }

    public sealed class V_LocationItemTable
    {
        public const string AVAILQUANTITY_FLD = "AvailQuantity";
        public const string BIN_FLD = "Bin";
        public const string CODE_FLD = "Code";
        public const string INSPSTATUS_FLD = "InspStatus";
        public const string LOCATIONID_FLD = "LocationID";
        public const string MASTERLOCATIONID_FLD = "MasterLocationID";
        public const string PRODUCTCODE_FLD = "ProductCode";
        public const string PRODUCTID_FLD = "ProductID";
        public const string TABLE_NAME = "V_LocationItem";
    }

    public sealed class V_LocationItemSumAvailTable
    {
        public const string AVAILQUANTITY_FLD = "AvailQuantity";
        public const string BIN_FLD = "Bin";
        public const string CODE_FLD = "Code";
        public const string INSPSTATUS_FLD = "InspStatus";
        public const string LOCATIONID_FLD = "LocationID";
        public const string MASTERLOCATIONID_FLD = "MasterLocationID";
        public const string PRODUCTID_FLD = "ProductID";
        public const string TABLE_NAME = "V_LocationItemSumAvail";
    }

    public sealed class v_LocationNoScecurityTable
    {
        public const string BIN_FLD = "Bin";
        public const string CODE_FLD = "Code";
        public const string LOCATIONID_FLD = "LocationID";
        public const string LOCATIONTYPEID_FLD = "LocationTypeID";
        public const string MANUFACTURINGACCESS_FLD = "ManufacturingAccess";
        public const string MASTERLOCATIONID_FLD = "MasterLocationID";
        public const string NAME_FLD = "Name";
        public const string SALEACCESS_FLD = "SaleAccess";
        public const string TABLE_NAME = "v_LocationNoScecurity";
        public const string TYPE_FLD = "Type";
    }

    public sealed class v_LotByBinTable
    {
        public const string BINID_FLD = "BinID";
        public const string ISSUEQUANTITY_FLD = "IssueQuantity";
        public const string LOCATIONID_FLD = "LocationID";
        public const string LOT_FLD = "Lot";
        public const string LOTCONTROL_FLD = "LotControl";
        public const string MASTERLOCATIONID_FLD = "MasterLocationID";
        public const string PRODUCTID_FLD = "ProductID";
        public const string TABLE_NAME = "v_LotByBin";
    }

    public sealed class v_LotByLocTable
    {
        public const string AVAILQUANTITY_FLD = "AvailQuantity";
        public const string LOCATIONID_FLD = "LocationID";
        public const string LOT_FLD = "Lot";
        public const string LOTCONTROL_FLD = "LotControl";
        public const string MASTERLOCATIONID_FLD = "MasterLocationID";
        public const string PRODUCTID_FLD = "ProductID";
        public const string TABLE_NAME = "v_LotByLoc";
    }

    public sealed class v_LotByWODetailAndProductTable
    {
        public const string AVAILABLEQUANTITY_FLD = "AvailableQuantity";
        public const string LOT_FLD = "Lot";
        public const string PRODUCTID_FLD = "ProductID";
        public const string TABLE_NAME = "v_LotByWODetailAndProduct";
        public const string WORKORDERDETAILID_FLD = "WorkOrderDetailID";
    }

    public sealed class V_MasLocItemSumAvailTable
    {
        public const string AVAILQUANTITY_FLD = "AvailQuantity";
        public const string CCNID_FLD = "CCNID";
        public const string CODE_FLD = "Code";
        public const string INSPSTATUS_FLD = "InspStatus";
        public const string MASTERLOCATIONID_FLD = "MasterLocationID";
        public const string PRODUCTID_FLD = "ProductID";
        public const string TABLE_NAME = "V_MasLocItemSumAvail";
    }

    public sealed class v_MasterLocationItemTable
    {
        public const string AVAILQUANTITY_FLD = "AvailQuantity";
        public const string CODE_FLD = "Code";
        public const string INSPSTATUS_FLD = "InspStatus";
        public const string MASTERLOCATIONID_FLD = "MasterLocationID";
        public const string PRODUCTCODE_FLD = "ProductCode";
        public const string PRODUCTID_FLD = "ProductID";
        public const string TABLE_NAME = "v_MasterLocationItem";
    }

    public sealed class v_MasterLocationNoSecurityTable
    {
        public const string ADDRESS_FLD = "Address";
        public const string CCNID_FLD = "CCNID";
        public const string CITYID_FLD = "CityID";
        public const string CODE_FLD = "Code";
        public const string COUNTRYID_FLD = "CountryID";
        public const string MASTERLOCATIONID_FLD = "MasterLocationID";
        public const string NAME_FLD = "Name";
        public const string STATE_FLD = "State";
        public const string TABLE_NAME = "v_MasterLocationNoSecurity";
        public const string ZIPPOST_FLD = "ZipPost";
    }

    public sealed class v_ModelListTable
    {
        public const string REVISION_FLD = "Revision";
        public const string TABLE_NAME = "v_ModelList";
    }

    public sealed class v_MSTPartyTable
    {
        public const string ADDRESS_FLD = "Address";
        public const string CODE_FLD = "Code";
        public const string NAME_FLD = "Name";
        public const string PARTYID_FLD = "PartyID";
        public const string TABLE_NAME = "v_MSTParty";
        public const string TYPE_FLD = "Type";
    }

    public sealed class v_OHDSRecycleAdjRptTable
    {
        public const string ACTCOSTALLOCATIONMASTERID_FLD = "ActCostAllocationMasterID";
        public const string ADJUSTAMOUNT_FLD = "AdjustAmount";
        public const string CATEGORY_FLD = "Category";
        public const string CATEGORYID_FLD = "CategoryID";
        public const string CGS1_FLD = "CGS1";
        public const string COSTELEMENTID_FLD = "CostElementID";
        public const string DSAMOUNT_FLD = "DSAmount";
        public const string MODEL_FLD = "Model";
        public const string PARTNAME_FLD = "PartName";
        public const string PARTNO_FLD = "PartNo";
        public const string PRODUCTID_FLD = "ProductID";
        public const string RATE_FLD = "Rate";
        public const string RECYCLEAMOUNT_FLD = "RecycleAmount";
        public const string TABLE_NAME = "v_OHDSRecycleAdjRpt";
        public const string TOTALCGS1_FLD = "TotalCGS1";
    }

    public sealed class v_OutputInPeriodTable
    {
        public const string BINID_FLD = "BinID";
        public const string LOCATIONID_FLD = "LocationID";
        public const string MONTHS_FLD = "Months";
        public const string PRODUCTID_FLD = "ProductID";
        public const string QTY_FLD = "QTY";
        public const string TABLE_NAME = "v_OutputInPeriod";
        public const string YEARS_FLD = "Years";
    }

    public sealed class v_PartyWithCCNTable
    {
        public const string ADDRESS_FLD = "Address";
        public const string BANKACCOUNT_FLD = "BankAccount";
        public const string CCNID_FLD = "CCNID";
        public const string CITYID_FLD = "CityID";
        public const string CODE_FLD = "Code";
        public const string COUNTRYID_FLD = "CountryID";
        public const string DELETEREASON_FLD = "DeleteReason";
        public const string FAX_FLD = "Fax";
        public const string NAME_FLD = "Name";
        public const string PARTYID_FLD = "PartyID";
        public const string PAYMENTTERMID_FLD = "PaymentTermID";
        public const string PHONE_FLD = "Phone";
        public const string STATE_FLD = "State";
        public const string TABLE_NAME = "v_PartyWithCCN";
        public const string TYPE_FLD = "Type";
        public const string VATCODE_FLD = "VATCode";
        public const string WEBSITE_FLD = "WebSite";
        public const string ZIPPOST_FLD = "ZipPost";
    }

    public sealed class v_PO_ApproveTable
    {
        public const string CCNID_FLD = "CCNID";
        public const string CODE_FLD = "Code";
        public const string MAKERID_FLD = "MakerID";
        public const string ORDERDATE_FLD = "OrderDate";
        public const string PARTYID_FLD = "PartyID";
        public const string PURCHASEORDERMASTERID_FLD = "PurchaseOrderMasterID";
        public const string RECCOMPLETED_FLD = "RecCompleted";
        public const string TABLE_NAME = "v_PO_Approve";
        public const string TOTALAMOUNT_FLD = "TotalAmount";
    }

    public sealed class v_PO_NOT_ApproveTable
    {
        public const string CCNID_FLD = "CCNID";
        public const string CODE_FLD = "Code";
        public const string ORDERDATE_FLD = "OrderDate";
        public const string PURCHASEORDERMASTERID_FLD = "PurchaseOrderMasterID";
        public const string RECCOMPLETED_FLD = "RecCompleted";
        public const string TABLE_NAME = "v_PO_NOT_Approve";
        public const string TOTALAMOUNT_FLD = "TotalAmount";
    }

    public sealed class v_PO_PurchaseOrderDetailReceiptTable
    {
        public const string AMOUNT_FLD = "Amount";
        public const string BIN_FLD = "Bin";
        public const string BINCODE_FLD = "BinCode";
        public const string BINID_FLD = "BinID";
        public const string BUYINGUMID_FLD = "BuyingUMID";
        public const string CODE_FLD = "Code";
        public const string DESCRIPTION_FLD = "Description";
        public const string LOCATIONCODE_FLD = "LocationCode";
        public const string LOCATIONID_FLD = "LocationID";
        public const string LOT_FLD = "Lot";
        public const string LOTCONTROL_FLD = "LotControl";
        public const string MST_UNITOFMEASURECODE_FLD = "MST_UnitOfMeasureCode";
        public const string PRODUCTID_FLD = "ProductID";
        public const string PURCHASEORDERDETAILID_FLD = "PurchaseOrderDetailID";
        public const string PURCHASEORDERMASTERID_FLD = "PurchaseOrderMasterID";
        public const string REVISION_FLD = "Revision";
        public const string SERIAL_FLD = "Serial";
        public const string STOCKUMID_FLD = "StockUMID";
        public const string TABLE_NAME = "v_PO_PurchaseOrderDetailReceipt";
        public const string TOTALAMOUNT_FLD = "TotalAmount";
        public const string TOTALRECEIVE_FLD = "TotalReceive";
        public const string TOTALREMAIN_FLD = "TotalRemain";
        public const string UMRATE_FLD = "UMRate";
        public const string UNITPRICE_FLD = "UnitPrice";
        public const string VATAMOUNT_FLD = "VATAmount";
        public const string VATPERCENT_FLD = "VATPercent";
    }

    public sealed class v_PO_PurchaseOrderMasterHasReceiveTable
    {
        public const string BUYERID_FLD = "BuyerID";
        public const string CARRIERID_FLD = "CarrierID";
        public const string CCNID_FLD = "CCNID";
        public const string CODE_FLD = "Code";
        public const string COMMENT_FLD = "Comment";
        public const string CURRENCYID_FLD = "CurrencyID";
        public const string DELIVERYTERMSID_FLD = "DeliveryTermsID";
        public const string DISCOUNTTERMID_FLD = "DiscountTermID";
        public const string EXCHANGERATE_FLD = "ExchangeRate";
        public const string IMPORTTAX_FLD = "ImportTax";
        public const string INVTOLOCID_FLD = "InvToLocID";
        public const string MASTERLOCATIONID_FLD = "MasterLocationID";
        public const string ORDERDATE_FLD = "OrderDate";
        public const string PARTYCONTACTID_FLD = "PartyContactID";
        public const string PARTYID_FLD = "PartyID";
        public const string PAUSEID_FLD = "PauseID";
        public const string PAYMENTTERMSID_FLD = "PaymentTermsID";
        public const string PRIORITY_FLD = "Priority";
        public const string PURCHASEORDERMASTERID_FLD = "PurchaseOrderMasterID";
        public const string PURCHASETYPEID_FLD = "PurchaseTypeID";
        public const string RECCOMPLETED_FLD = "RecCompleted";
        public const string SHIPTOLOCID_FLD = "ShipToLocID";
        public const string SPECIALTAX_FLD = "SpecialTax";
        public const string TABLE_NAME = "v_PO_PurchaseOrderMasterHasReceive";
        public const string TOTALAMOUNT_FLD = "TotalAmount";
        public const string TOTALDISCOUNT_FLD = "TotalDiscount";
        public const string TOTALIMPORTTAX_FLD = "TotalImportTax";
        public const string TOTALNETAMOUNT_FLD = "TotalNetAmount";
        public const string TOTALSPECIALTAX_FLD = "TotalSpecialTax";
        public const string TOTALVAT_FLD = "TotalVAT";
        public const string VAT_FLD = "VAT";
        public const string VENDORLOCID_FLD = "VendorLocID";
        public const string VENDORSO_FLD = "VendorSO";
    }

    public sealed class v_PO_PurchaseOrderTotalReceiveTable
    {
        public const string LOT_FLD = "Lot";
        public const string PRODUCTID_FLD = "ProductID";
        public const string PURCHASEORDERDETAILID_FLD = "PurchaseOrderDetailID";
        public const string PURCHASEORDERMASTERID_FLD = "PurchaseOrderMasterID";
        public const string SERIAL_FLD = "Serial";
        public const string TABLE_NAME = "v_PO_PurchaseOrderTotalReceive";
        public const string TOTALRECEIVE_FLD = "TotalReceive";
    }

    public sealed class v_PO_ReturnToVendorMasterTable
    {
        public const string BYINVOICE_FLD = "ByInvoice";
        public const string BYPO_FLD = "ByPO";
        public const string CCNID_FLD = "CCNID";
        public const string INVOICEMASTERID_FLD = "InvoiceMasterID";
        public const string INVOICENO_FLD = "InvoiceNo";
        public const string MASTERLOCATIONID_FLD = "MasterLocationID";
        public const string MASTERLOCCODE_FLD = "MasterLocCode";
        public const string PARTYCODE_FLD = "PartyCode";
        public const string PARTYID_FLD = "PartyID";
        public const string PARTYNAME_FLD = "PartyName";
        public const string POSTDATE_FLD = "PostDate";
        public const string PRODUCTIONLINE_FLD = "ProductionLine";
        public const string PRODUCTIONLINEID_FLD = "ProductionLineID";
        public const string PURCHASELOCCODE_FLD = "PurchaseLocCode";
        public const string PURCHASELOCID_FLD = "PurchaseLocID";
        public const string PURCHASEORDERCODE_FLD = "PurchaseOrderCode";
        public const string PURCHASEORDERMASTERID_FLD = "PurchaseOrderMasterID";
        public const string RETURNTOVENDORMASTERID_FLD = "ReturnToVendorMasterID";
        public const string RTVNO_FLD = "RTVNo";
        public const string TABLE_NAME = "v_PO_ReturnToVendorMaster";
    }

    public sealed class v_PODeliver_MakerTable
    {
        public const string CATEGORY_FLD = "Category";
        public const string CATEGORYID_FLD = "CategoryID";
        public const string COUNTRYID_FLD = "CountryID";
        public const string DAYINMONTH_FLD = "DayinMonth";
        public const string MAKERCODE_FLD = "MakerCode";
        public const string MAKERID_FLD = "MakerID";
        public const string MODEL_FLD = "Model";
        public const string MONTH_FLD = "month";
        public const string ORDERQTY_FLD = "OrderQty";
        public const string PARTNAME_FLD = "PartName";
        public const string PARTNO_FLD = "PartNo";
        public const string PARTYCODE_FLD = "PartyCode";
        public const string PARTYID_FLD = "PartyID";
        public const string PRODUCTID_FLD = "ProductID";
        public const string TABLE_NAME = "v_PODeliver_Maker";
        public const string UM_FLD = "UM";
        public const string YEAR_FLD = "year";
    }

    public sealed class v_POReceiptImportTable
    {
        public const string CATEGORY_FLD = "Category";
        public const string CIFUNIT_FLD = "CIFUnit";
        public const string CIPAMOUNT_FLD = "CIPAmount";
        public const string IMPORTTAXAMOUNT_FLD = "ImportTaxAmount";
        public const string INVENTORID_FLD = "InventorID";
        public const string INVOICEDETAILID_FLD = "InvoiceDetailID";
        public const string MAKERCODE_FLD = "MakerCode";
        public const string MAKERID_FLD = "MakerID";
        public const string MAKERNAME_FLD = "MakerName";
        public const string MODEL_FLD = "Model";
        public const string MONTH_FLD = "Month";
        public const string ORTHERAMOUNT_FLD = "OrtherAmount";
        public const string PARTNAME_FLD = "PartName";
        public const string PARTNO_FLD = "PartNo";
        public const string PARTYID_FLD = "PartyID";
        public const string PRODUCTID_FLD = "ProductID";
        public const string QUANTITY_FLD = "Quantity";
        public const string TABLE_NAME = "v_POReceiptImport";
        public const string TOTALCIF_FLD = "TotalCIF";
        public const string TOTALINLAND_FLD = "TotalInland";
        public const string TYPE_FLD = "Type";
        public const string UM_FLD = "UM";
        public const string VENDORCODE_FLD = "VendorCode";
        public const string VENDORNAME_FLD = "VendorName";
        public const string YEAR_FLD = "Year";
    }

    public sealed class v_POReturnToVendorTable
    {
        public const string CODE_FLD = "Code";
        public const string MASTERLOCATIONID_FLD = "MasterLocationID";
        public const string ORDERDATE_FLD = "OrderDate";
        public const string PARTYID_FLD = "PartyID";
        public const string PURCHASEORDERMASTERID_FLD = "PurchaseOrderMasterID";
        public const string TABLE_NAME = "v_POReturnToVendor";
        public const string VENDORLOCID_FLD = "VendorLocID";
    }

    public sealed class v_PRO_IssueMaterialDetailTable
    {
        public const string AVAILABLEQUANTITY_FLD = "AvailableQuantity";
        public const string BINID_FLD = "BinID";
        public const string BOMQUANTITY_FLD = "BomQuantity";
        public const string COMMITEDQUANTITY_FLD = "CommitedQuantity";
        public const string COMMITQUANTITY_FLD = "CommitQuantity";
        public const string FGOODSCODE_FLD = "FGoodsCode";
        public const string ISSUEMATERIALDETAILID_FLD = "IssueMaterialDetailID";
        public const string ISSUEMATERIALMASTERID_FLD = "IssueMaterialMasterID";
        public const string ITM_CATEGORYCODE_FLD = "ITM_CategoryCode";
        public const string ITM_PRODUCTCODE_FLD = "ITM_ProductCode";
        public const string ITM_PRODUCTDESCRIPTION_FLD = "ITM_ProductDescription";
        public const string ITM_PRODUCTREVISION_FLD = "ITM_ProductRevision";
        public const string LINE_FLD = "Line";
        public const string LOCATIONID_FLD = "LocationID";
        public const string LOT_FLD = "Lot";
        public const string MASTERLOCATIONID_FLD = "MasterLocationID";
        public const string MST_BINCODE_FLD = "MST_BinCode";
        public const string MST_LOCATIONCODE_FLD = "MST_LocationCode";
        public const string MST_PARTYCODE_FLD = "MST_PartyCode";
        public const string MST_PARTYNAME_FLD = "MST_PartyName";
        public const string MST_UNITOFMEASURECODE_FLD = "MST_UnitOfMeasureCode";
        public const string NEEDEDQUANTITY_FLD = "NeededQuantity";
        public const string PRO_WORKORDERDETAILLINE_FLD = "PRO_WorkOrderDetailLine";
        public const string PRO_WORKORDERMASTERWORKORDERNO_FLD = "PRO_WorkOrderMasterWorkOrderNo";
        public const string PRODUCTID_FLD = "ProductID";
        public const string QASTATUS_FLD = "QAStatus";
        public const string REMAINNEEDEDQUANTITY_FLD = "RemainNeededQuantity";
        public const string SERIAL_FLD = "Serial";
        public const string STOCKUMID_FLD = "StockUMID";
        public const string TABLE_NAME = "v_PRO_IssueMaterialDetail";
        public const string WORKORDERDETAILID_FLD = "WorkOrderDetailID";
        public const string WORKORDERMASTERID_FLD = "WorkOrderMasterID";
    }

    public sealed class v_Pro_LaborTimeDetailTable
    {
        public const string COMPLETED_FLD = "Completed";
        public const string COMPLETEPERCENT_FLD = "CompletePercent";
        public const string DUEDATE_FLD = "DueDate";
        public const string EMPLOYEEID_FLD = "EmployeeID";
        public const string ENDDATETIME_FLD = "EndDateTime";
        public const string HOUR_FLD = "Hour";
        public const string HOURCODE_FLD = "HourCode";
        public const string ITM_COSTCENTERCODE_FLD = "ITM_CostCenterCode";
        public const string LABORCOSTCENTERID_FLD = "LaborCostCenterID";
        public const string LABORTIMEDETAILID_FLD = "LaborTimeDetailID";
        public const string LABORTIMEMASTERID_FLD = "LaborTimeMasterID";
        public const string MST_EMPLOYEECODE_FLD = "MST_EmployeeCode";
        public const string PRO_SHIFTSHIFTDESC_FLD = "PRO_ShiftShiftDesc";
        public const string PRO_WORKORDERDETAILLINE_FLD = "PRO_WorkOrderDetailLine";
        public const string PRO_WORKORDERDETAILSTATUS_FLD = "PRO_WorkOrderDetailStatus";
        public const string PRO_WORKORDERMASTERWORKORDERNO_FLD = "PRO_WorkOrderMasterWorkOrderNo";
        public const string PRO_WOROUTINGSTEP_FLD = "PRO_WORoutingStep";
        public const string QUANTITY_FLD = "Quantity";
        public const string SETUPRUN_FLD = "SetupRun";
        public const string SHIFTID_FLD = "ShiftID";
        public const string STARTDATE_FLD = "StartDate";
        public const string STARTDATETIME_FLD = "StartDateTime";
        public const string TABLE_NAME = "v_Pro_LaborTimeDetail";
        public const string WORKORDERDETAILID_FLD = "WorkOrderDetailID";
        public const string WORKORDERMASTERID_FLD = "WorkOrderMasterID";
        public const string WOROUTINGID_FLD = "WORoutingID";
    }

    public sealed class v_Pro_MachineTimeDetailTable
    {
        public const string COMPLETED_FLD = "Completed";
        public const string COMPLETEPERCENT_FLD = "CompletePercent";
        public const string DUEDATE_FLD = "DueDate";
        public const string ENDDATETIME_FLD = "EndDateTime";
        public const string HOUR_FLD = "Hour";
        public const string MACHINEID_FLD = "MachineID";
        public const string MACHINETIMEDETAILID_FLD = "MachineTimeDetailID";
        public const string MACHINETIMEMASTERID_FLD = "MachineTimeMasterID";
        public const string PRO_MACHINECODE_FLD = "PRO_MachineCode";
        public const string PRO_SHIFTSHIFTDESC_FLD = "PRO_ShiftShiftDesc";
        public const string PRO_WORKORDERDETAILLINE_FLD = "PRO_WorkOrderDetailLine";
        public const string PRO_WORKORDERDETAILSTATUS_FLD = "PRO_WorkOrderDetailStatus";
        public const string PRO_WORKORDERMASTERWORKORDERNO_FLD = "PRO_WorkOrderMasterWorkOrderNo";
        public const string PRO_WOROUTINGSTEP_FLD = "PRO_WORoutingStep";
        public const string QUANTITY_FLD = "Quantity";
        public const string SETUPRUN_FLD = "SetupRun";
        public const string SHIFTID_FLD = "ShiftID";
        public const string STARTDATE_FLD = "StartDate";
        public const string STARTDATETIME_FLD = "StartDateTime";
        public const string TABLE_NAME = "v_Pro_MachineTimeDetail";
        public const string WORKORDERDETAILID_FLD = "WorkOrderDetailID";
        public const string WORKORDERMASTERID_FLD = "WorkOrderMasterID";
        public const string WOROUTINGID_FLD = "WORoutingID";
    }

    public sealed class v_PRO_RequiredMaterialTable
    {
        public const string ACTUALRECEIVED_FLD = "ActualReceived";
        public const string BOM_ITEM_FLD = "BOM_Item";
        public const string BOM_PARTNAME_FLD = "BOM_PartName";
        public const string CCNID_FLD = "CCNID";
        public const string DESIGNUMID_FLD = "DesignUMID";
        public const string FGOODSCODE_FLD = "FGoodsCode";
        public const string LINE_FLD = "Line";
        public const string MAKEITEM_FLD = "MakeItem";
        public const string PRODUCTID_FLD = "ProductID";
        public const string PRODUCTIONLINEID_FLD = "ProductionLineID";
        public const string QTY_FLD = "Qty";
        public const string RATE_FLD = "Rate";
        public const string REQUIREDMATERIALDETAILID_FLD = "RequiredMaterialDetailID";
        public const string REQUIREDMATERIALMASTERID_FLD = "RequiredMaterialMasterID";
        public const string REQUIREDQTY_FLD = "RequiredQty";
        public const string SALEORDERDETAILID_FLD = "SaleOrderDetailID";
        public const string SLDUTRU_FLD = "SLdutru";
        public const string STOCKQTY_FLD = "StockQty";
        public const string TABLE_NAME = "v_PRO_RequiredMaterial";
        public const string UNIT_FLD = "Unit";
        public const string WO_ITEM_FLD = "WO_item";
        public const string WO_PARTNAME_FLD = "WO_PartName";
        public const string WO_PRODUCTID_FLD = "WO_ProductID";
        public const string WO_QTY_FLD = "WO_Qty";
        public const string WORKORDERDETAILID_FLD = "WorkOrderDetailID";
        public const string WORKORDERNO_FLD = "WorkOrderNo";
    }

    public sealed class v_PRO_RequiredMaterialBuHongTable
    {
        public const string ACTUALRECEIVED_FLD = "ActualReceived";
        public const string BOM_ITEM_FLD = "BOM_Item";
        public const string BOM_PARTNAME_FLD = "BOM_PartName";
        public const string CCNID_FLD = "CCNID";
        public const string DESIGNUMID_FLD = "DesignUMID";
        public const string FGOODSCODE_FLD = "FGoodsCode";
        public const string LINE_FLD = "Line";
        public const string MAKEITEM_FLD = "MakeItem";
        public const string PRODUCTID_FLD = "ProductID";
        public const string PRODUCTIONLINEID_FLD = "ProductionLineID";
        public const string QTY_FLD = "Qty";
        public const string RATE_FLD = "Rate";
        public const string REQUIREDMATERIALDETAILID_FLD = "RequiredMaterialDetailID";
        public const string REQUIREDMATERIALMASTERID_FLD = "RequiredMaterialMasterID";
        public const string REQUIREDQTY_FLD = "RequiredQty";
        public const string SALEORDERDETAILID_FLD = "SaleOrderDetailID";
        public const string SLDUTRU_FLD = "SLdutru";
        public const string STOCKQTY_FLD = "StockQty";
        public const string TABLE_NAME = "v_PRO_RequiredMaterialBuHong";
        public const string UNIT_FLD = "Unit";
        public const string WO_ITEM_FLD = "WO_item";
        public const string WO_PARTNAME_FLD = "WO_PartName";
        public const string WO_PRODUCTID_FLD = "WO_ProductID";
        public const string WO_QTY_FLD = "WO_Qty";
        public const string WORKORDERDETAILID_FLD = "WorkOrderDetailID";
        public const string WORKORDERNO_FLD = "WorkOrderNo";
    }

    public sealed class v_PRO_RequiredMaterialDetailTable
    {
        public const string BOM_ITEM_FLD = "Bom_Item";
        public const string BOM_PARTNAME_FLD = "BOm_PartName";
        public const string DESIGNUMID_FLD = "DesignUMID";
        public const string EXPR1_FLD = "Expr1";
        public const string PRODUCTID_FLD = "ProductID";
        public const string PRODUCTIONLINEID_FLD = "ProductionLineID";
        public const string QTY_FLD = "Qty";
        public const string REQUIREDMATERIALDETAILID_FLD = "RequiredMaterialDetailID";
        public const string REQUIREDMATERIALMASTERID_FLD = "RequiredMaterialMasterID";
        public const string REQUIREDQTY_FLD = "RequiredQty";
        public const string STOCKQTY_FLD = "StockQty";
        public const string TABLE_NAME = "v_PRO_RequiredMaterialDetail";
        public const string UNIT_FLD = "Unit";
        public const string WO_ITEM_FLD = "WO_Item";
        public const string WO_PARTNAME_FLD = "WO_PartName";
        public const string WO_QTY_FLD = "WO_qty";
        public const string WORKORDERDETAILID_FLD = "WorkOrderDetailID";
        public const string WORKORDERNO_FLD = "WorkOrderNo";
    }

    public sealed class v_PRO_WORoutingTable
    {
        public const string CREWSIZE_FLD = "CrewSize";
        public const string EFFECTBEGINDATE_FLD = "EffectBeginDate";
        public const string EFFECTENDDATE_FLD = "EffectEndDate";
        public const string FIXLT_FLD = "FixLT";
        public const string FUNCTIONID_FLD = "FunctionID";
        public const string ITM_ROUTINGSTATUSCODE_FLD = "ITM_RoutingStatusCode";
        public const string LABORCOSTCENTERID_FLD = "LaborCostCenterID";
        public const string LABORITM_COSTCENTERCODE_FLD = "LABORITM_CostCenterCode";
        public const string LABORRUNTIME_FLD = "LaborRunTime";
        public const string LABORSETUPTIME_FLD = "LaborSetupTime";
        public const string MACHINECOSTCENTERID_FLD = "MachineCostCenterID";
        public const string MACHINEITM_COSTCENTERCODE_FLD = "MACHINEITM_CostCenterCode";
        public const string MACHINERUNTIME_FLD = "MachineRunTime";
        public const string MACHINES_FLD = "Machines";
        public const string MACHINESETUPTIME_FLD = "MachineSetupTime";
        public const string MOVETIME_FLD = "MoveTime";
        public const string MST_FUNCTIONCODE_FLD = "MST_FunctionCode";
        public const string MST_PARTYCODE_FLD = "MST_PartyCode";
        public const string MST_WORKCENTERCODE_FLD = "MST_WorkCenterCode";
        public const string OSCOST_FLD = "OSCost";
        public const string OSFIXLT_FLD = "OSFixLT";
        public const string OSOVERLAPPERCENT_FLD = "OSOverlapPercent";
        public const string OSOVERLAPQTY_FLD = "OSOverlapQty";
        public const string OSSCHEDULESEQ_FLD = "OSScheduleSeq";
        public const string OSVARLT_FLD = "OSVarLT";
        public const string OVERLAPPERCENT_FLD = "OverlapPercent";
        public const string OVERLAPQTY_FLD = "OverlapQty";
        public const string PACER_FLD = "Pacer";
        public const string PARTYID_FLD = "PartyID";
        public const string PRODUCTID_FLD = "ProductID";
        public const string ROUTINGSTATUSID_FLD = "RoutingStatusID";
        public const string RUNQUANTITY_FLD = "RunQuantity";
        public const string SCHEDULESEQ_FLD = "ScheduleSeq";
        public const string SETUPQUANTITY_FLD = "SetupQuantity";
        public const string STEP_FLD = "Step";
        public const string STUDYTIME_FLD = "StudyTime";
        public const string TABLE_NAME = "v_PRO_WORouting";
        public const string TYPE_FLD = "Type";
        public const string VARLT_FLD = "VarLT";
        public const string WORKCENTERID_FLD = "WorkCenterID";
        public const string WORKORDERDETAILID_FLD = "WorkOrderDetailID";
        public const string WORKORDERMASTERID_FLD = "WorkOrderMasterID";
        public const string WOROUTINGID_FLD = "WORoutingID";
    }

    public sealed class v_PRO_WOROUTINGBORLTable
    {
        public const string CREWSIZE_FLD = "CrewSize";
        public const string EFFECTBEGINDATE_FLD = "EffectBeginDate";
        public const string EFFECTENDDATE_FLD = "EffectEndDate";
        public const string FUNCTIONID_FLD = "FunctionID";
        public const string LABORCOSTCENTERID_FLD = "LaborCostCenterID";
        public const string LABORRUNTIME_FLD = "LaborRunTime";
        public const string LABORSETUPTIME_FLD = "LaborSetupTime";
        public const string MACHINECOSTCENTERID_FLD = "MachineCostCenterID";
        public const string MACHINERUNTIME_FLD = "MachineRunTime";
        public const string MACHINES_FLD = "Machines";
        public const string MACHINESETUPTIME_FLD = "MachineSetupTime";
        public const string MOVETIME_FLD = "MoveTime";
        public const string PACER_FLD = "Pacer";
        public const string PRODUCTID_FLD = "ProductID";
        public const string SETUPQUANTITY_FLD = "SetupQuantity";
        public const string STEP_FLD = "Step";
        public const string STUDYTIME_FLD = "StudyTime";
        public const string TABLE_NAME = "v_PRO_WOROUTINGBORL";
        public const string TYPE_FLD = "Type";
        public const string WORKCENTERID_FLD = "WorkCenterID";
        public const string WORKORDERDETAILID_FLD = "WorkOrderDetailID";
        public const string WOROUTINGID_FLD = "WORoutingID";
    }

    public sealed class v_PRO_WOROUTINGBORMTable
    {
        public const string CREWSIZE_FLD = "CrewSize";
        public const string EFFECTBEGINDATE_FLD = "EffectBeginDate";
        public const string EFFECTENDDATE_FLD = "EffectEndDate";
        public const string FUNCTIONID_FLD = "FunctionID";
        public const string LABORCOSTCENTERID_FLD = "LaborCostCenterID";
        public const string LABORRUNTIME_FLD = "LaborRunTime";
        public const string LABORSETUPTIME_FLD = "LaborSetupTime";
        public const string MACHINECOSTCENTERID_FLD = "MachineCostCenterID";
        public const string MACHINERUNTIME_FLD = "MachineRunTime";
        public const string MACHINES_FLD = "Machines";
        public const string MACHINESETUPTIME_FLD = "MachineSetupTime";
        public const string MOVETIME_FLD = "MoveTime";
        public const string PACER_FLD = "Pacer";
        public const string PRODUCTID_FLD = "ProductID";
        public const string SETUPQUANTITY_FLD = "SetupQuantity";
        public const string STEP_FLD = "Step";
        public const string STUDYTIME_FLD = "StudyTime";
        public const string TABLE_NAME = "v_PRO_WOROUTINGBORM";
        public const string TYPE_FLD = "Type";
        public const string WORKCENTERID_FLD = "WorkCenterID";
        public const string WORKORDERDETAILID_FLD = "WorkOrderDetailID";
        public const string WOROUTINGID_FLD = "WORoutingID";
    }

    public sealed class V_PRODUCT_AVAILABLE_IN_BIN_INCOMINGTable
    {
        public const string BINAVAILABLE_FLD = "binavailable";
        public const string BINID_FLD = "Binid";
        public const string LOCATIONID_FLD = "locationid";
        public const string MASTERLOCATIONID_FLD = "masterlocationID";
        public const string PRODUCTID_FLD = "productid";
        public const string TABLE_NAME = "V_PRODUCT_AVAILABLE_IN_BIN_INCOMING";
    }

    public sealed class v_ProductForCustomerTable
    {
        public const string BIN_FLD = "Bin";
        public const string BINID_FLD = "BinID";
        public const string BUYINGLOCID_FLD = "BuyingLocID";
        public const string CATEGORYID_FLD = "CategoryID";
        public const string CODE_FLD = "Code";
        public const string DESCRIPTION_FLD = "Description";
        public const string LOCATIONID_FLD = "LocationID";
        public const string MASTERLOCATIONID_FLD = "MasterLocationID";
        public const string MST_BINCODE_FLD = "MST_BinCode";
        public const string MST_LOCATIONCODE_FLD = "MST_LocationCode";
        public const string MST_MASTERLOCATIONCODE_FLD = "MST_MasterLocationCode";
        public const string MST_UNITOFMEASURECODE_FLD = "MST_UnitOfMeasureCode";
        public const string PARTYCONTACTID_FLD = "PartyContactID";
        public const string PARTYID_FLD = "PartyID";
        public const string PRODUCTID_FLD = "ProductID";
        public const string REVISION_FLD = "Revision";
        public const string SELLINGUMID_FLD = "SellingUMID";
        public const string SHIPFROMLOCID_FLD = "ShipFromLocID";
        public const string STOCKUMID_FLD = "StockUMID";
        public const string TABLE_NAME = "v_ProductForCustomer";
    }

    public sealed class V_ProductForFreightTable
    {
        public const string CODE_FLD = "Code";
        public const string DESCRIPTION_FLD = "Description";
        public const string MST_UNITOFMEASURECODE_FLD = "MST_UnitOfMeasureCode";
        public const string PRODUCTID_FLD = "ProductID";
        public const string REVISION_FLD = "Revision";
        public const string STOCKUMID_FLD = "StockUMID";
        public const string TABLE_NAME = "V_ProductForFreight";
        public const string VAT_FLD = "VAT";
    }

    public sealed class v_ProductForSalaryTable
    {
        public const string CODE_FLD = "Code";
        public const string DESCRIPTION_FLD = "Description";
        public const string PRODUCTID_FLD = "ProductID";
        public const string PRODUCTIONLINEID_FLD = "ProductionLineID";
        public const string REVISION_FLD = "Revision";
        public const string SALEORDERDETAILID_FLD = "SaleOrderDetailID";
        public const string TABLE_NAME = "v_ProductForSalary";
    }

    public sealed class V_ProductForStockTakingTable
    {
        public const string CODE_FLD = "Code";
        public const string DESCRIPTION_FLD = "Description";
        public const string ITM_CATEGORYCODE_FLD = "ITM_CategoryCode";
        public const string MST_UNITOFMEASURECODE_FLD = "MST_UnitOfMeasureCode";
        public const string PRODUCTID_FLD = "ProductID";
        public const string PRODUCTTYPE_FLD = "ProductType";
        public const string REVISION_FLD = "Revision";
        public const string SOURCE_FLD = "Source";
        public const string STOCKTAKINGCODE_FLD = "StockTakingCode";
        public const string STOCKUMID_FLD = "StockUMID";
        public const string TABLE_NAME = "V_ProductForStockTaking";
        public const string VENDOR_FLD = "Vendor";
    }

    public sealed class v_ProductGroupInfoTable
    {
        public const string CCNID_FLD = "CCNID";
        public const string DEPARTMENTID_FLD = "DepartmentID";
        public const string DESCRIPTION_FLD = "Description";
        public const string GROUPPRODUCTIONMAX_FLD = "GroupProductionMax";
        public const string PRO_PRODUCTIONLINECODE_FLD = "PRO_ProductionLineCode";
        public const string PRODUCTIONGROUPID_FLD = "ProductionGroupID";
        public const string PRODUCTIONLINEID_FLD = "ProductionLineID";
        public const string TABLE_NAME = "v_ProductGroupInfo";
    }

    public sealed class V_ProductInBinCacheTable
    {
        public const string AVAILABLEQUANTITY_FLD = "AvailableQuantity";
        public const string BINID_FLD = "BinID";
        public const string CATEGORYID_FLD = "CategoryID";
        public const string CODE_FLD = "Code";
        public const string DESCRIPTION_FLD = "Description";
        public const string ITM_CATEGORYCODE_FLD = "ITM_CategoryCode";
        public const string LOCATIONID_FLD = "LocationID";
        public const string LOTCONTROL_FLD = "LotControl";
        public const string MASTERLOCATIONID_FLD = "MasterLocationID";
        public const string MST_BINCODE_FLD = "MST_BinCode";
        public const string MST_PARTYCODE_FLD = "MST_PartyCode";
        public const string MST_UNITOFMEASURECODE_FLD = "MST_UnitOfMeasureCode";
        public const string PRIMARYVENDORID_FLD = "PrimaryVendorID";
        public const string PRODUCTID_FLD = "ProductID";
        public const string REVISION_FLD = "Revision";
        public const string STOCKUMID_FLD = "StockUMID";
        public const string TABLE_NAME = "V_ProductInBinCache";
    }

    public sealed class V_ProductInforTable
    {
        public const string BINID_FLD = "BinID";
        public const string CATEGORYID_FLD = "CategoryID";
        public const string CCNID_FLD = "CCNID";
        public const string LOCATIONID_FLD = "LocationID";
        public const string LOTCONTROL_FLD = "LotControl";
        public const string MAKEITEM_FLD = "MakeItem";
        public const string MASTERLOCATIONID_FLD = "MasterLocationID";
        public const string MODEL_FLD = "Model";
        public const string PARENTPRODUCTID_FLD = "ParentProductID";
        public const string PARTNAME_FLD = "PartName";
        public const string PARTNUMBER_FLD = "PartNumber";
        public const string PRODUCTID_FLD = "ProductID";
        public const string QASTATUS_FLD = "QAStatus";
        public const string STOCKUMID_FLD = "StockUMID";
        public const string TABLE_NAME = "V_ProductInfor";
        public const string UM_FLD = "UM";
    }

    public sealed class V_ProductInforWithInventoryTable
    {
        public const string BINAVAILABLEQUANTITY_FLD = "BinAvailableQuantity";
        public const string BINCONTROL_FLD = "BinControl";
        public const string BINID_FLD = "BinID";
        public const string BINNAME_FLD = "BinName";
        public const string BINQASTATUS_FLD = "BinQAStatus";
        public const string CCNID_FLD = "CCNID";
        public const string LOCATIONID_FLD = "LocationID";
        public const string LOCAVAILABLEQUANTITY_FLD = "LocAvailableQuantity";
        public const string LOCNAME_FLD = "LocName";
        public const string LOCQASTATUS_FLD = "LocQAStatus";
        public const string LOTCONTROL_FLD = "LotControl";
        public const string LOTSIZE_FLD = "LotSize";
        public const string MASLOCNAME_FLD = "MasLocName";
        public const string MASTERLOCATIONID_FLD = "MasterLocationID";
        public const string MODEL_FLD = "Model";
        public const string PARTNAME_FLD = "PartName";
        public const string PARTNUMBER_FLD = "PartNumber";
        public const string PRODUCTID_FLD = "ProductID";
        public const string QASTATUS_FLD = "QAStatus";
        public const string STOCKUMID_FLD = "StockUMID";
        public const string TABLE_NAME = "V_ProductInforWithInventory";
        public const string UM_FLD = "UM";
        public const string UNITOFMEASUREID_FLD = "UnitOfMeasureID";
    }

    public sealed class V_ProductInLocCacheTable
    {
        public const string AVAILABLEQUANTITY_FLD = "AvailableQuantity";
        public const string BIN_FLD = "Bin";
        public const string CODE_FLD = "Code";
        public const string DESCRIPTION_FLD = "Description";
        public const string ITM_CATEGORYCODE_FLD = "ITM_CategoryCode";
        public const string LOCATIONCODE_FLD = "LocationCode";
        public const string LOCATIONID_FLD = "LocationID";
        public const string LOTCONTROL_FLD = "LotControl";
        public const string MASTERLOCATIONID_FLD = "MasterLocationID";
        public const string MST_PARTYCODE_FLD = "MST_PartyCode";
        public const string MST_UNITOFMEASURECODE_FLD = "MST_UnitOfMeasureCode";
        public const string PRIMARYVENDORID_FLD = "PrimaryVendorID";
        public const string PRODUCTID_FLD = "ProductID";
        public const string REVISION_FLD = "Revision";
        public const string STOCKUMID_FLD = "StockUMID";
        public const string TABLE_NAME = "V_ProductInLocCache";
    }

    public sealed class v_ProductInProductionLineTable
    {
        public const string AGCID_FLD = "AGCID";
        public const string AUTOCONVERSION_FLD = "AutoConversion";
        public const string BINID_FLD = "BinID";
        public const string BOMDESCRIPTION_FLD = "BOMDescription";
        public const string BOMINCREMENT_FLD = "BomIncrement";
        public const string BUYERID_FLD = "BuyerID";
        public const string BUYINGUMID_FLD = "BuyingUMID";
        public const string CATEGORY_FLD = "Category";
        public const string CATEGORYID_FLD = "CategoryID";
        public const string CCNID_FLD = "CCNID";
        public const string CODE_FLD = "Code";
        public const string CONVERSIONTOLERANCE_FLD = "ConversionTolerance";
        public const string COSTCENTERID_FLD = "CostCenterID";
        public const string COSTCENTERRATEMASTERID_FLD = "CostCenterRateMasterID";
        public const string COSTMETHOD_FLD = "CostMethod";
        public const string CREATEDATETIME_FLD = "CreateDateTime";
        public const string DELETEREASONID_FLD = "DeleteReasonID";
        public const string DELIVERYPOLICYID_FLD = "DeliveryPolicyID";
        public const string DESCRIPTION_FLD = "Description";
        public const string EXPORTTAX_FLD = "ExportTax";
        public const string FINISHEDGOODS_FLD = "FinishedGoods";
        public const string FORMATCODEID_FLD = "FormatCodeID";
        public const string FREIGHTCLASSID_FLD = "FreightClassID";
        public const string HAZARDID_FLD = "HazardID";
        public const string HEIGHT_FLD = "Height";
        public const string HEIGHTUMID_FLD = "HeightUMID";
        public const string IMPORTTAX_FLD = "ImportTax";
        public const string INVENTORID_FLD = "InventorID";
        public const string ISSUESIZE_FLD = "IssueSize";
        public const string LENGTH_FLD = "Length";
        public const string LENGTHUMID_FLD = "LengthUMID";
        public const string LICENSEFEE_FLD = "LicenseFee";
        public const string LISTPRICE_FLD = "ListPrice";
        public const string LOCATIONID_FLD = "LocationID";
        public const string LOTCONTROL_FLD = "LotControl";
        public const string LOTSIZE_FLD = "LotSize";
        public const string LTDOCKTOSTOCK_FLD = "LTDockToStock";
        public const string LTFIXEDTIME_FLD = "LTFixedTime";
        public const string LTORDERPREPARE_FLD = "LTOrderPrepare";
        public const string LTREQUISITION_FLD = "LTRequisition";
        public const string LTSAFETYSTOCK_FLD = "LTSafetyStock";
        public const string LTSALESATP_FLD = "LTSalesATP";
        public const string LTSHIPPINGPREPARE_FLD = "LTShippingPrepare";
        public const string LTVARIABLETIME_FLD = "LTVariableTime";
        public const string MAKEITEM_FLD = "MakeItem";
        public const string MASTERLOCATIONID_FLD = "MasterLocationID";
        public const string MAXIMUMSTOCK_FLD = "MaximumStock";
        public const string MAXPRODUCE_FLD = "MaxProduce";
        public const string MINIMUMSTOCK_FLD = "MinimumStock";
        public const string MINPRODUCE_FLD = "MinProduce";
        public const string ORDERPOINT_FLD = "OrderPoint";
        public const string ORDERPOLICYID_FLD = "OrderPolicyID";
        public const string ORDERQUANTITY_FLD = "OrderQuantity";
        public const string ORDERQUANTITYMULTIPLE_FLD = "OrderQuantityMultiple";
        public const string ORDERRULEID_FLD = "OrderRuleID";
        public const string OTHERINFO1_FLD = "OtherInfo1";
        public const string OTHERINFO2_FLD = "OtherInfo2";
        public const string PARENTPRODUCTID_FLD = "ParentProductID";
        public const string PARTNAMEVN_FLD = "PartNameVN";
        public const string PARTNUMBER_FLD = "PartNumber";
        public const string PLANTYPE_FLD = "PlanType";
        public const string PLCODE_FLD = "PLCode";
        public const string PRIMARYVENDORID_FLD = "PrimaryVendorID";
        public const string PRODUCTGROUPID_FLD = "ProductGroupID";
        public const string PRODUCTID_FLD = "ProductID";
        public const string PRODUCTIONLINEID_FLD = "ProductionLineID";
        public const string PRODUCTTYPEID_FLD = "ProductTypeID";
        public const string QASTATUS_FLD = "QAStatus";
        public const string QUANTITYSET_FLD = "QuantitySet";
        public const string RECEIVETOLERANCE_FLD = "ReceiveTolerance";
        public const string REVISION_FLD = "Revision";
        public const string ROUTINGDESCRIPTION_FLD = "RoutingDescription";
        public const string ROUTINGINCREMENT_FLD = "RoutingIncrement";
        public const string SAFETYSTOCK_FLD = "SafetyStock";
        public const string SCRAPPERCENT_FLD = "ScrapPercent";
        public const string SELLINGUMID_FLD = "SellingUMID";
        public const string SETUPDATE_FLD = "SetupDate";
        public const string SHELFLIFE_FLD = "ShelfLife";
        public const string SHIPTOLERANCEID_FLD = "ShipToleranceID";
        public const string SOURCEID_FLD = "SourceID";
        public const string SPECIALTAX_FLD = "SpecialTax";
        public const string STOCK_FLD = "Stock";
        public const string STOCKUMID_FLD = "StockUMID";
        public const string TABLE_NAME = "v_ProductInProductionLine";
        public const string TAXCODE_FLD = "TaxCode";
        public const string UPDATEDATETIME_FLD = "UpdateDateTime";
        public const string VAT_FLD = "VAT";
        public const string VENDORCURRENCYID_FLD = "VendorCurrencyID";
        public const string VENDORLOCATIONID_FLD = "VendorLocationID";
        public const string VOUCHERTOLERANCE_FLD = "VoucherTolerance";
        public const string WEIGHT_FLD = "Weight";
        public const string WEIGHTUMID_FLD = "WeightUMID";
        public const string WIDTH_FLD = "Width";
        public const string WIDTHUMID_FLD = "WidthUMID";
    }

    public sealed class v_ProductInSaleTable
    {
        public const string CODE_FLD = "Code";
        public const string DELIVERYQUANTITY_FLD = "DeliveryQuantity";
        public const string DELIVERYSCHEDULEID_FLD = "DeliveryScheduleID";
        public const string DESCRIPTION_FLD = "Description";
        public const string NETAMOUNT_FLD = "NetAmount";
        public const string PRODUCTID_FLD = "ProductID";
        public const string SALEORDERDETAILID_FLD = "SaleOrderDetailID";
        public const string SALEORDERMASTERID_FLD = "SaleOrderMasterID";
        public const string SCHEDULEDATE_FLD = "ScheduleDate";
        public const string STOCKUMID_FLD = "StockUMID";
        public const string TABLE_NAME = "v_ProductInSale";
        public const string UM_FLD = "UM";
        public const string UNITPRICE_FLD = "UnitPrice";
        public const string VATAMOUNT_FLD = "VATAmount";
        public const string VATPERCENT_FLD = "VATPercent";
    }

    public sealed class v_ProductInventoryAdjustmentTable
    {
        public const string BINAVAILABLEQUANTITY_FLD = "BinAvailableQuantity";
        public const string BINCONTROL_FLD = "BinControl";
        public const string BINID_FLD = "BinID";
        public const string BINNAME_FLD = "BinName";
        public const string BINQASTATUS_FLD = "BinQAStatus";
        public const string CATEGORYID_FLD = "CategoryID";
        public const string CCNID_FLD = "CCNID";
        public const string LOCATIONID_FLD = "LocationID";
        public const string LOCAVAILABLEQUANTITY_FLD = "LocAvailableQuantity";
        public const string LOCNAME_FLD = "LocName";
        public const string LOCQASTATUS_FLD = "LocQAStatus";
        public const string LOTCONTROL_FLD = "LotControl";
        public const string LOTSIZE_FLD = "LotSize";
        public const string MASLOCNAME_FLD = "MasLocName";
        public const string MASTERLOCATIONID_FLD = "MasterLocationID";
        public const string MODEL_FLD = "Model";
        public const string PARTNAME_FLD = "PartName";
        public const string PARTNUMBER_FLD = "PartNumber";
        public const string PRODUCTID_FLD = "ProductID";
        public const string STOCKUMID_FLD = "StockUMID";
        public const string TABLE_NAME = "v_ProductInventoryAdjustment";
        public const string UM_FLD = "UM";
    }

    public sealed class V_ProductInWorkCenterTable
    {
        public const string AGCID_FLD = "AGCID";
        public const string AUTOCONVERSION_FLD = "AutoConversion";
        public const string BINID_FLD = "BinID";
        public const string BOMDESCRIPTION_FLD = "BOMDescription";
        public const string BOMINCREMENT_FLD = "BomIncrement";
        public const string BUYERID_FLD = "BuyerID";
        public const string BUYINGUMID_FLD = "BuyingUMID";
        public const string CATEGORYID_FLD = "CategoryID";
        public const string CCNID_FLD = "CCNID";
        public const string CODE_FLD = "Code";
        public const string CONVERSIONTOLERANCE_FLD = "ConversionTolerance";
        public const string COSTCENTERID_FLD = "CostCenterID";
        public const string COSTCENTERRATEMASTERID_FLD = "CostCenterRateMasterID";
        public const string COSTMETHOD_FLD = "CostMethod";
        public const string CREATEDATETIME_FLD = "CreateDateTime";
        public const string DELETEREASONID_FLD = "DeleteReasonID";
        public const string DELIVERYPOLICYID_FLD = "DeliveryPolicyID";
        public const string DESCRIPTION_FLD = "Description";
        public const string EXPORTTAX_FLD = "ExportTax";
        public const string FINISHEDGOODS_FLD = "FinishedGoods";
        public const string FORMATCODEID_FLD = "FormatCodeID";
        public const string FREIGHTCLASSID_FLD = "FreightClassID";
        public const string HAZARDID_FLD = "HazardID";
        public const string HEIGHT_FLD = "Height";
        public const string HEIGHTUMID_FLD = "HeightUMID";
        public const string IMPORTTAX_FLD = "ImportTax";
        public const string INVENTORID_FLD = "InventorID";
        public const string ISSUESIZE_FLD = "IssueSize";
        public const string ITM_CATEGORYCODE_FLD = "ITM_CategoryCode";
        public const string LENGTH_FLD = "Length";
        public const string LENGTHUMID_FLD = "LengthUMID";
        public const string LICENSEFEE_FLD = "LicenseFee";
        public const string LISTPRICE_FLD = "ListPrice";
        public const string LOCATIONID_FLD = "LocationID";
        public const string LOTCONTROL_FLD = "LotControl";
        public const string LOTSIZE_FLD = "LotSize";
        public const string LTDOCKTOSTOCK_FLD = "LTDockToStock";
        public const string LTFIXEDTIME_FLD = "LTFixedTime";
        public const string LTORDERPREPARE_FLD = "LTOrderPrepare";
        public const string LTREQUISITION_FLD = "LTRequisition";
        public const string LTSAFETYSTOCK_FLD = "LTSafetyStock";
        public const string LTSALESATP_FLD = "LTSalesATP";
        public const string LTSHIPPINGPREPARE_FLD = "LTShippingPrepare";
        public const string LTVARIABLETIME_FLD = "LTVariableTime";
        public const string MAKEITEM_FLD = "MakeItem";
        public const string MASTERLOCATIONID_FLD = "MasterLocationID";
        public const string MAXIMUMSTOCK_FLD = "MaximumStock";
        public const string MAXPRODUCE_FLD = "MaxProduce";
        public const string MINIMUMSTOCK_FLD = "MinimumStock";
        public const string MINPRODUCE_FLD = "MinProduce";
        public const string MST_UNITOFMEASURECODE_FLD = "mst_unitofmeasurecode";
        public const string MST_WORKCENTERCODE_FLD = "mst_workcentercode";
        public const string ORDERPOINT_FLD = "OrderPoint";
        public const string ORDERPOLICYID_FLD = "OrderPolicyID";
        public const string ORDERQUANTITY_FLD = "OrderQuantity";
        public const string ORDERQUANTITYMULTIPLE_FLD = "OrderQuantityMultiple";
        public const string ORDERRULEID_FLD = "OrderRuleID";
        public const string OTHERINFO1_FLD = "OtherInfo1";
        public const string OTHERINFO2_FLD = "OtherInfo2";
        public const string PARENTPRODUCTID_FLD = "ParentProductID";
        public const string PARTNAMEVN_FLD = "PartNameVN";
        public const string PARTNUMBER_FLD = "PartNumber";
        public const string PLANTYPE_FLD = "PlanType";
        public const string PRIMARYVENDORID_FLD = "PrimaryVendorID";
        public const string PRODUCTGROUPID_FLD = "ProductGroupID";
        public const string PRODUCTID_FLD = "ProductID";
        public const string PRODUCTIONLINEID_FLD = "ProductionLineID";
        public const string PRODUCTTYPEID_FLD = "ProductTypeID";
        public const string QASTATUS_FLD = "QAStatus";
        public const string QUANTITYSET_FLD = "QuantitySet";
        public const string RECEIVETOLERANCE_FLD = "ReceiveTolerance";
        public const string REVISION_FLD = "Revision";
        public const string ROUTINGDESCRIPTION_FLD = "RoutingDescription";
        public const string ROUTINGID_FLD = "RoutingID";
        public const string ROUTINGINCREMENT_FLD = "RoutingIncrement";
        public const string SAFETYSTOCK_FLD = "SafetyStock";
        public const string SCRAPPERCENT_FLD = "ScrapPercent";
        public const string SELLINGUMID_FLD = "SellingUMID";
        public const string SETUPDATE_FLD = "SetupDate";
        public const string SHELFLIFE_FLD = "ShelfLife";
        public const string SHIPTOLERANCEID_FLD = "ShipToleranceID";
        public const string SOURCEID_FLD = "SourceID";
        public const string SPECIALTAX_FLD = "SpecialTax";
        public const string STOCK_FLD = "Stock";
        public const string STOCKUMID_FLD = "StockUMID";
        public const string TABLE_NAME = "V_ProductInWorkCenter";
        public const string TAXCODE_FLD = "TaxCode";
        public const string UPDATEDATETIME_FLD = "UpdateDateTime";
        public const string VAT_FLD = "VAT";
        public const string VENDORCURRENCYID_FLD = "VendorCurrencyID";
        public const string VENDORLOCATIONID_FLD = "VendorLocationID";
        public const string VOUCHERTOLERANCE_FLD = "VoucherTolerance";
        public const string WEIGHT_FLD = "Weight";
        public const string WEIGHTUMID_FLD = "WeightUMID";
        public const string WIDTH_FLD = "Width";
        public const string WIDTHUMID_FLD = "WidthUMID";
        public const string WORKCENTERID_FLD = "WorkCenterID";
    }

    public sealed class v_ProductionLineFullLocationAndBinTable
    {
        public const string BINID_FLD = "BinID";
        public const string BINTYPEID_FLD = "BinTypeID";
        public const string LOCATIONID_FLD = "LocationID";
        public const string LOCATIONTYPEID_FLD = "LocationTypeID";
        public const string MST_BINCODE_FLD = "MST_BinCode";
        public const string MST_BINNAME_FLD = "MST_BinName";
        public const string MST_LOCATIONCODE_FLD = "MST_LocationCode";
        public const string MST_LOCATIONNAME_FLD = "MST_LocationName";
        public const string PRO_PRODUCTIONLINECODE_FLD = "PRO_ProductionLineCode";
        public const string PRO_PRODUCTIONLINENAME_FLD = "PRO_ProductionLineName";
        public const string PRODUCTIONLINEID_FLD = "ProductionLineID";
        public const string TABLE_NAME = "v_ProductionLineFullLocationAndBin";
    }

    public sealed class v_ProductionLineNoSecurityTable
    {
        public const string BALANCEPLANNING_FLD = "BalancePlanning";
        public const string CODE_FLD = "Code";
        public const string DEPARTMENTID_FLD = "DepartmentID";
        public const string LOCATIONID_FLD = "LocationID";
        public const string NAME_FLD = "Name";
        public const string PRODUCTIONLINEID_FLD = "ProductionLineID";
        public const string ROUNDUPDAYSEXCEPTION_FLD = "RoundUpDaysException";
        public const string TABLE_NAME = "v_ProductionLineNoSecurity";
    }

    public sealed class v_ProductRevisionTable
    {
        public const string REVISION_FLD = "Revision";
        public const string TABLE_NAME = "v_ProductRevision";
    }

    public sealed class v_ProductWithProductionLineInfoTable
    {
        public const string CATEGORYID_FLD = "CategoryID";
        public const string CODE_FLD = "Code";
        public const string DESCRIPTION_FLD = "Description";
        public const string ITM_CATEGORYCODE_FLD = "ITM_CategoryCode";
        public const string MAKEITEM_FLD = "MakeItem";
        public const string PRODUCTID_FLD = "ProductID";
        public const string PRODUCTIONLINEID_FLD = "ProductionLineID";
        public const string REVISION_FLD = "Revision";
        public const string TABLE_NAME = "v_ProductWithProductionLineInfo";
    }

    public sealed class v_PROIssueWithFGoodsCodeTable
    {
        public const string CCNID_FLD = "CCNID";
        public const string FGOODSCODE_FLD = "FGoodsCode";
        public const string ISSUEMATERIALMASTERID_FLD = "IssueMaterialMasterID";
        public const string ISSUENO_FLD = "IssueNo";
        public const string ISSUEPURPOSEID_FLD = "IssuePurposeID";
        public const string LASTCHANGE_FLD = "LastChange";
        public const string MASTERLOCATIONID_FLD = "MasterLocationID";
        public const string POSTDATE_FLD = "PostDate";
        public const string PRODUCTID_FLD = "ProductID";
        public const string SHIFTID_FLD = "ShiftID";
        public const string TABLE_NAME = "v_PROIssueWithFGoodsCode";
        public const string TOBINID_FLD = "ToBinID";
        public const string TOLOCATIONID_FLD = "ToLocationID";
        public const string USERNAME_FLD = "UserName";
        public const string WORKORDERDETAILID_FLD = "WorkOrderDetailID";
        public const string WORKORDERMASTERID_FLD = "WorkOrderMasterID";
    }

    public sealed class v_ProIssueWithProductionLineTable
    {
        public const string CCNID_FLD = "CCNID";
        public const string ISSUEMATERIALMASTERID_FLD = "IssueMaterialMasterID";
        public const string ISSUENO_FLD = "IssueNo";
        public const string ISSUEPURPOSEID_FLD = "IssuePurposeID";
        public const string LASTCHANGE_FLD = "LastChange";
        public const string MASTERLOCATIONID_FLD = "MasterLocationID";
        public const string POSTDATE_FLD = "PostDate";
        public const string PRODUCTID_FLD = "ProductID";
        public const string PRODUCTIONLINEID_FLD = "ProductionLineID";
        public const string SHIFTID_FLD = "ShiftID";
        public const string TABLE_NAME = "v_ProIssueWithProductionLine";
        public const string TOBINID_FLD = "ToBinID";
        public const string TOLOCATIONID_FLD = "ToLocationID";
        public const string USERNAME_FLD = "UserName";
        public const string WORKORDERDETAILID_FLD = "WorkOrderDetailID";
        public const string WORKORDERMASTERID_FLD = "WorkOrderMasterID";
    }

    public sealed class v_PROWorkOrderMasterJoinSaleOrderDetailTable
    {
        public const string CCNID_FLD = "CCNID";
        public const string DESCRIPTION_FLD = "Description";
        public const string FGOODSCODE_FLD = "FGoodsCode";
        public const string LASTCHANGE_FLD = "LastChange";
        public const string MASTERLOCATIONID_FLD = "MasterLocationID";
        public const string PARTYID_FLD = "PartyID";
        public const string PRODUCEREASONID_FLD = "ProduceReasonID";
        public const string PRODUCTID_FLD = "ProductID";
        public const string SALEORDERCODE_FLD = "SaleOrderCode";
        public const string SALEORDERDETAILID_FLD = "SaleOrderDetailID";
        public const string SALEORDERMASTERID_FLD = "SaleOrderMasterID";
        public const string TABLE_NAME = "v_PROWorkOrderMasterJoinSaleOrderDetail";
        public const string TRANSDATE_FLD = "TransDate";
        public const string USERNAME_FLD = "UserName";
        public const string WORKORDERMASTERID_FLD = "WorkOrderMasterID";
        public const string WORKORDERNO_FLD = "WorkOrderNo";
    }

    public sealed class v_PurchaseOrderOfItemTable
    {
        public const string APPROVERID_FLD = "ApproverID";
        public const string BUYERID_FLD = "BuyerID";
        public const string BUYINGUMID_FLD = "BuyingUMID";
        public const string CARRIERID_FLD = "CarrierID";
        public const string CCNID_FLD = "CCNID";
        public const string CLOSED_FLD = "Closed";
        public const string COMMENT_FLD = "Comment";
        public const string CURRENCYID_FLD = "CurrencyID";
        public const string DELIVERYTERMSID_FLD = "DeliveryTermsID";
        public const string DESCRIPTION_FLD = "Description";
        public const string DISCOUNTTERMID_FLD = "DiscountTermID";
        public const string EXCHANGERATE_FLD = "ExchangeRate";
        public const string IMPORTTAX_FLD = "ImportTax";
        public const string INVTOLOCID_FLD = "InvToLocID";
        public const string ITM_PRODUCTCODE_FLD = "ITM_ProductCode";
        public const string LINE_FLD = "Line";
        public const string MASTERLOCATIONID_FLD = "MasterLocationID";
        public const string MST_UNITOFMEASURECODE_FLD = "MST_UnitOfMeasureCode";
        public const string ORDERDATE_FLD = "OrderDate";
        public const string PARTYCONTACTID_FLD = "PartyContactID";
        public const string PARTYID_FLD = "PartyID";
        public const string PAUSEID_FLD = "PauseID";
        public const string PAYMENTTERMSID_FLD = "PaymentTermsID";
        public const string PO_PURCHASEORDERMASTERCODE_FLD = "PO_PurchaseOrderMasterCode";
        public const string POREVISION_FLD = "PORevision";
        public const string PRIORITY_FLD = "Priority";
        public const string PRODUCTID_FLD = "ProductID";
        public const string PURCHASEORDERDETAILID_FLD = "PurchaseOrderDetailID";
        public const string PURCHASEORDERMASTERID_FLD = "PurchaseOrderMasterID";
        public const string PURCHASETYPEID_FLD = "PurchaseTypeID";
        public const string RECCOMPLETED_FLD = "RecCompleted";
        public const string RECEIVEQUANTITY_FLD = "ReceiveQuantity";
        public const string REVISION_FLD = "Revision";
        public const string SHIPTOLOCID_FLD = "ShipToLocID";
        public const string SPECIALTAX_FLD = "SpecialTax";
        public const string STOCKUMID_FLD = "StockUMID";
        public const string TABLE_NAME = "v_PurchaseOrderOfItem";
        public const string TOTALAMOUNT_FLD = "TotalAmount";
        public const string TOTALDISCOUNT_FLD = "TotalDiscount";
        public const string TOTALIMPORTTAX_FLD = "TotalImportTax";
        public const string TOTALNETAMOUNT_FLD = "TotalNetAmount";
        public const string TOTALSPECIALTAX_FLD = "TotalSpecialTax";
        public const string TOTALVAT_FLD = "TotalVAT";
        public const string UMRATE_FLD = "UMRate";
        public const string UNITPRICE_FLD = "UnitPrice";
        public const string VAT_FLD = "VAT";
        public const string VENDORLOCID_FLD = "VendorLocID";
        public const string VENDORSO_FLD = "VendorSO";
    }

    public sealed class v_ReceiptByScheduleTable
    {
        public const string BUYINGUMID_FLD = "BuyingUMID";
        public const string CATEGORY_FLD = "Category";
        public const string CCNCODE_FLD = "CCNCode";
        public const string CCNNAME_FLD = "CCNName";
        public const string CUSTCODE_FLD = "CustCode";
        public const string CUSTNAME_FLD = "CustName";
        public const string DAY_FLD = "Day";
        public const string DELIVERYSCHEDULEID_FLD = "DeliveryScheduleID";
        public const string HOUR_FLD = "Hour";
        public const string LINE_FLD = "Line";
        public const string MODEL_FLD = "Model";
        public const string MONTH_FLD = "Month";
        public const string MST_UNITOFMEASURECODE_FLD = "MST_UnitOfMeasureCode";
        public const string PARTNAME_FLD = "PartName";
        public const string PARTNUMBER_FLD = "PartNumber";
        public const string PO_PURCHASEORDERMASTERCODE_FLD = "PO_PurchaseOrderMasterCode";
        public const string PRODUCTID_FLD = "ProductID";
        public const string PURCHASEORDERDETAILID_FLD = "PurchaseOrderDetailID";
        public const string PURCHASEORDERMASTERID_FLD = "PurchaseOrderMasterID";
        public const string RECEIVEQUANTITY_FLD = "ReceiveQuantity";
        public const string SCHEDULEDATE_FLD = "ScheduleDate";
        public const string STOCKUMID_FLD = "StockUMID";
        public const string TABLE_NAME = "v_ReceiptBySchedule";
        public const string YEAR_FLD = "Year";
    }

    public sealed class v_ReceiptByVendorTable
    {
        public const string MAKERCODE_FLD = "MakerCode";
        public const string MAKERID_FLD = "MakerID";
        public const string PARTYID_FLD = "PartyID";
        public const string POSLIPNO_FLD = "POSlipNo";
        public const string POSTDATE_FLD = "PostDate";
        public const string PURCHASEORDERNO_FLD = "PurchaseOrderNO";
        public const string PURCHASEORDERRECEIPTID_FLD = "PurchaseOrderReceiptID";
        public const string RECEIVENO_FLD = "ReceiveNo";
        public const string REFNO_FLD = "RefNo";
        public const string TABLE_NAME = "v_ReceiptByVendor";
        public const string VENDORCODE_FLD = "VendorCode";
        public const string VENDORNAME_FLD = "VendorName";
    }

    public sealed class v_RecoverMaterialMasterTable
    {
        public const string AVAILABLEQTY_FLD = "AvailableQty";
        public const string CATEGORYID_FLD = "CategoryID";
        public const string CCNID_FLD = "CCNID";
        public const string FROMBINID_FLD = "FromBinID";
        public const string FROMLOCATIONID_FLD = "FromLocationID";
        public const string POSTDATE_FLD = "PostDate";
        public const string PRODUCTID_FLD = "ProductID";
        public const string QUANTITY_FLD = "Quantity";
        public const string RECOVERMATERIALMASTERID_FLD = "RecoverMaterialMasterID";
        public const string TABLE_NAME = "v_RecoverMaterialMaster";
        public const string TRANSNO_FLD = "TransNo";
    }

    public sealed class v_ReleasedAndMFClosedWorkOrderTable
    {
        public const string MASTERLOCATIONID_FLD = "MasterLocationID";
        public const string PRODUCTIONLINEID_FLD = "ProductionLineID";
        public const string TABLE_NAME = "v_ReleasedAndMFClosedWorkOrder";
        public const string TRANSDATE_FLD = "TransDate";
        public const string WORKORDERMASTERID_FLD = "WorkOrderMasterID";
        public const string WORKORDERNO_FLD = "WorkOrderNo";
    }

    public sealed class v_ReleasedWOTable
    {
        public const string MASTERLOCATIONID_FLD = "MasterLocationID";
        public const string PRODUCTIONLINEID_FLD = "ProductionLineID";
        public const string STATUS_FLD = "Status";
        public const string TABLE_NAME = "v_ReleasedWO";
        public const string TRANSDATE_FLD = "TransDate";
        public const string WORKORDERMASTERID_FLD = "WorkOrderMasterID";
        public const string WORKORDERNO_FLD = "WorkOrderNo";
    }

    public sealed class v_ReleasedWOPDOTable
    {
        public const string MASTERLOCATIONID_FLD = "MasterLocationID";
        public const string PRODUCTIONLINEID_FLD = "ProductionLineID";
        public const string SALEORDERDETAILID_FLD = "SaleOrderDetailID";
        public const string STATUS_FLD = "Status";
        public const string TABLE_NAME = "v_ReleasedWOPDO";
        public const string TRANSDATE_FLD = "TransDate";
        public const string WORKORDERMASTERID_FLD = "WorkOrderMasterID";
        public const string WORKORDERNO_FLD = "WorkOrderNo";
    }

    public sealed class v_ReleasedWorkOrderTable
    {
        public const string MASTERLOCATIONID_FLD = "MasterLocationID";
        public const string TABLE_NAME = "v_ReleasedWorkOrder";
        public const string TRANSDATE_FLD = "TransDate";
        public const string WORKORDERMASTERID_FLD = "WorkOrderMasterID";
        public const string WORKORDERNO_FLD = "WorkOrderNo";
    }

    public sealed class v_ReleasedWorkOrderWithProductIDTable
    {
        public const string MASTERLOCATIONID_FLD = "MasterLocationID";
        public const string PRODUCTID_FLD = "ProductID";
        public const string TABLE_NAME = "v_ReleasedWorkOrderWithProductID";
        public const string TRANSDATE_FLD = "TransDate";
        public const string WORKORDERMASTERID_FLD = "WorkOrderMasterID";
        public const string WORKORDERNO_FLD = "WorkOrderNo";
    }

    public sealed class v_ReleasedWOWithLocationTable
    {
        public const string LOCATIONID_FLD = "LocationID";
        public const string MASTERLOCATIONID_FLD = "MasterLocationID";
        public const string TABLE_NAME = "v_ReleasedWOWithLocation";
        public const string TRANSDATE_FLD = "TransDate";
        public const string WORKORDERMASTERID_FLD = "WorkOrderMasterID";
        public const string WORKORDERNO_FLD = "WorkOrderNo";
    }

    public sealed class v_ReleasedWOWithLocationPDOTable
    {
        public const string LOCATIONID_FLD = "LocationID";
        public const string MASTERLOCATIONID_FLD = "MasterLocationID";
        public const string TABLE_NAME = "v_ReleasedWOWithLocationPDO";
        public const string TRANSDATE_FLD = "TransDate";
        public const string WORKORDERMASTERID_FLD = "WorkOrderMasterID";
        public const string WORKORDERNO_FLD = "WorkOrderNo";
    }

    public sealed class v_ReleasedWOWithRemainNeededQtyTable
    {
        public const string NEEDEDQUANTITY_FLD = "NeededQuantity";
        public const string PRODUCTID_FLD = "ProductID";
        public const string REMAINNEEDEDQUANTITY_FLD = "RemainNeededQuantity";
        public const string SHRINK_FLD = "Shrink";
        public const string TABLE_NAME = "v_ReleasedWOWithRemainNeededQty";
        public const string WORKORDERDETAILID_FLD = "WorkOrderDetailID";
        public const string WORKORDERMASTERID_FLD = "WorkOrderMasterID";
    }

    public sealed class v_ReleasedWOWithRemainNeededQtyAllPDOTable
    {
        public const string BOMQUANTITY_FLD = "BomQuantity";
        public const string MASTERLOCATIONID_FLD = "MasterLocationID";
        public const string NEEDEDQUANTITY_FLD = "NeededQuantity";
        public const string PRODUCTID_FLD = "ProductID";
        public const string REMAINNEEDEDQUANTITY_FLD = "RemainNeededQuantity";
        public const string SHRINK_FLD = "Shrink";
        public const string TABLE_NAME = "v_ReleasedWOWithRemainNeededQtyAllPDO";
        public const string WORKORDERDETAILID_FLD = "WorkOrderDetailID";
        public const string WORKORDERMASTERID_FLD = "WorkOrderMasterID";
    }

    public sealed class v_ReleasedWOWithRemainNeededQtyPDOTable
    {
        public const string BOMQUANTITY_FLD = "BomQuantity";
        public const string MASTERLOCATIONID_FLD = "MasterLocationID";
        public const string NEEDEDQUANTITY_FLD = "NeededQuantity";
        public const string PRODUCTID_FLD = "ProductID";
        public const string REMAINNEEDEDQUANTITY_FLD = "RemainNeededQuantity";
        public const string SHRINK_FLD = "Shrink";
        public const string TABLE_NAME = "v_ReleasedWOWithRemainNeededQtyPDO";
        public const string WORKORDERDETAILID_FLD = "WorkOrderDetailID";
        public const string WORKORDERMASTERID_FLD = "WorkOrderMasterID";
    }

    public sealed class v_ReleaseWorkOrderTable
    {
        public const string MASTERLOCATIONID_FLD = "MasterLocationID";
        public const string PRODUCTID_FLD = "ProductID";
        public const string STATUS_FLD = "Status";
        public const string TABLE_NAME = "v_ReleaseWorkOrder";
        public const string WORKORDERMASTERID_FLD = "WorkOrderMasterID";
        public const string WORKORDERNO_FLD = "WorkOrderNo";
    }

    public sealed class v_RemainComponentForWOIssueTable
    {
        public const string BINID_FLD = "BinID";
        public const string COMPONENTID_FLD = "ComponentID";
        public const string DUEDATE_FLD = "DueDate";
        public const string ITM_PRODUCTCODE_FLD = "ITM_ProductCode";
        public const string ITM_PRODUCTDESCRIPTION_FLD = "ITM_ProductDescription";
        public const string ITM_PRODUCTREVISION_FLD = "ITM_ProductRevision";
        public const string LINE_FLD = "Line";
        public const string LOCATIONID_FLD = "LocationID";
        public const string LOTCONTROL_FLD = "LotControl";
        public const string LOTSIZE_FLD = "LotSize";
        public const string MASTERLOCATIONID_FLD = "MasterLocationID";
        public const string MST_UNITOFMEASURECODE_FLD = "MST_UnitOfMeasureCode";
        public const string ORDERQUANTITY_FLD = "OrderQuantity";
        public const string PRO_WORKORDERMASTERWORKORDERNO_FLD = "PRO_WorkOrderMasterWorkOrderNo";
        public const string PRODUCTID_FLD = "ProductID";
        public const string PRODUCTIONLINELOCATIONID_FLD = "ProductionLineLocationID";
        public const string REMAINNEEDEDQUANTITY_FLD = "RemainNeededQuantity";
        public const string REQUIREDQUANTITY_FLD = "RequiredQuantity";
        public const string SHRINK_FLD = "Shrink";
        public const string STARTDATE_FLD = "StartDate";
        public const string STOCKUMID_FLD = "StockUMID";
        public const string TABLE_NAME = "v_RemainComponentForWOIssue";
        public const string WORKORDERDETAILID_FLD = "WorkOrderDetailID";
        public const string WORKORDERMASTERID_FLD = "WorkOrderMasterID";
    }

    public sealed class v_RemainComponentForWOIssuePDOTable
    {
        public const string BINID_FLD = "BinID";
        public const string COMPONENTID_FLD = "ComponentID";
        public const string DUEDATE_FLD = "DueDate";
        public const string ITM_PRODUCTCODE_FLD = "ITM_ProductCode";
        public const string ITM_PRODUCTDESCRIPTION_FLD = "ITM_ProductDescription";
        public const string ITM_PRODUCTREVISION_FLD = "ITM_ProductRevision";
        public const string LINE_FLD = "Line";
        public const string LOCATIONID_FLD = "LocationID";
        public const string LOTCONTROL_FLD = "LotControl";
        public const string LOTSIZE_FLD = "LotSize";
        public const string MASTERLOCATIONID_FLD = "MasterLocationID";
        public const string MST_UNITOFMEASURECODE_FLD = "MST_UnitOfMeasureCode";
        public const string ORDERQUANTITY_FLD = "OrderQuantity";
        public const string PRO_WORKORDERMASTERWORKORDERNO_FLD = "PRO_WorkOrderMasterWorkOrderNo";
        public const string PRODUCTID_FLD = "ProductID";
        public const string PRODUCTIONLINELOCATIONID_FLD = "ProductionLineLocationID";
        public const string REMAINNEEDEDQUANTITY_FLD = "RemainNeededQuantity";
        public const string REQUIREDQUANTITY_FLD = "RequiredQuantity";
        public const string SHRINK_FLD = "Shrink";
        public const string STARTDATE_FLD = "StartDate";
        public const string STOCKUMID_FLD = "StockUMID";
        public const string TABLE_NAME = "v_RemainComponentForWOIssuePDO";
        public const string WORKORDERDETAILID_FLD = "WorkOrderDetailID";
        public const string WORKORDERMASTERID_FLD = "WorkOrderMasterID";
    }

    public sealed class v_RemainComponentForWOIssueWithParentInfoTable
    {
        public const string BINID_FLD = "BinID";
        public const string COMPONENTID_FLD = "ComponentID";
        public const string DUEDATE_FLD = "DueDate";
        public const string ITM_CATEGORYCODE_FLD = "ITM_CategoryCode";
        public const string ITM_PRODUCTCODE_FLD = "ITM_ProductCode";
        public const string ITM_PRODUCTDESCRIPTION_FLD = "ITM_ProductDescription";
        public const string ITM_PRODUCTREVISION_FLD = "ITM_ProductRevision";
        public const string LOCATIONID_FLD = "LocationID";
        public const string LOTCONTROL_FLD = "LotControl";
        public const string LOTSIZE_FLD = "LotSize";
        public const string MASTERLOCATIONID_FLD = "MasterLocationID";
        public const string MST_PARTYCODE_FLD = "MST_PartyCode";
        public const string MST_PARTYNAME_FLD = "MST_PartyName";
        public const string MST_UNITOFMEASURECODE_FLD = "MST_UnitOfMeasureCode";
        public const string ORDERQUANTITY_FLD = "OrderQuantity";
        public const string PARENTCODE_FLD = "ParentCode";
        public const string PARENTNAME_FLD = "ParentName";
        public const string PARENTREVISION_FLD = "ParentRevision";
        public const string PRO_WORKORDERDETAILLINE_FLD = "PRO_WorkOrderDetailLine";
        public const string PRO_WORKORDERMASTERWORKORDERNO_FLD = "PRO_WorkOrderMasterWorkOrderNo";
        public const string PRODUCTID_FLD = "ProductID";
        public const string PRODUCTIONLINELOCATIONID_FLD = "ProductionLineLocationID";
        public const string REMAINNEEDEDQUANTITY_FLD = "RemainNeededQuantity";
        public const string REQUIREDQUANTITY_FLD = "RequiredQuantity";
        public const string SHRINK_FLD = "Shrink";
        public const string STARTDATE_FLD = "StartDate";
        public const string STOCKUMID_FLD = "StockUMID";
        public const string TABLE_NAME = "v_RemainComponentForWOIssueWithParentInfo";
        public const string WORKORDERDETAILID_FLD = "WorkOrderDetailID";
        public const string WORKORDERMASTERID_FLD = "WorkOrderMasterID";
    }

    public sealed class v_RemainComponentForWOIssueWithParentInfoAllPDOTable
    {
        public const string AVAILABLEQUANTITY_FLD = "AvailableQuantity";
        public const string BINID_FLD = "BinID";
        public const string BOMQUANTITY_FLD = "BomQuantity";
        public const string COMMITQUANTITY_FLD = "CommitQuantity";
        public const string COMPLETEDQUANTITY_FLD = "CompletedQuantity";
        public const string COMPONENTID_FLD = "ComponentID";
        public const string DESIGNUMID_FLD = "DesignUMID";
        public const string DUEDATE_FLD = "DueDate";
        public const string FGOODSCODE_FLD = "FGoodsCode";
        public const string ITM_CATEGORYCODE_FLD = "ITM_CategoryCode";
        public const string ITM_PRODUCTCODE_FLD = "ITM_ProductCode";
        public const string ITM_PRODUCTDESCRIPTION_FLD = "ITM_ProductDescription";
        public const string ITM_PRODUCTREVISION_FLD = "ITM_ProductRevision";
        public const string LOCATIONID_FLD = "LocationID";
        public const string LOTCONTROL_FLD = "LotControl";
        public const string LOTSIZE_FLD = "LotSize";
        public const string MASTERLOCATIONID_FLD = "MasterLocationID";
        public const string MST_BINCODE_FLD = "MST_BinCode";
        public const string MST_LOCATIONCODE_FLD = "MST_LocationCode";
        public const string MST_PARTYCODE_FLD = "MST_PartyCode";
        public const string MST_PARTYNAME_FLD = "MST_PartyName";
        public const string MST_UNITOFMEASURECODE_FLD = "MST_UnitOfMeasureCode";
        public const string NEEDEDQUANTITY_FLD = "NeededQuantity";
        public const string ORDERQUANTITY_FLD = "OrderQuantity";
        public const string PARENTCODE_FLD = "ParentCode";
        public const string PARENTNAME_FLD = "ParentName";
        public const string PARENTREVISION_FLD = "ParentRevision";
        public const string PRO_WORKORDERDETAILLINE_FLD = "PRO_WorkOrderDetailLine";
        public const string PRO_WORKORDERMASTERWORKORDERNO_FLD = "PRO_WorkOrderMasterWorkOrderNo";
        public const string PRODUCTID_FLD = "ProductID";
        public const string PRODUCTIONLINEID_FLD = "ProductionLineID";
        public const string PRODUCTIONLINELOCATIONID_FLD = "ProductionLineLocationID";
        public const string RATE_FLD = "Rate";
        public const string SHRINK_FLD = "Shrink";
        public const string STARTDATE_FLD = "StartDate";
        public const string STOCKUMID_FLD = "StockUMID";
        public const string TABLE_NAME = "v_RemainComponentForWOIssueWithParentInfoAllPDO";
        public const string WORKORDERDETAILID_FLD = "WorkOrderDetailID";
        public const string WORKORDERMASTERID_FLD = "WorkOrderMasterID";
    }

    public sealed class v_RemainComponentForWOIssueWithParentInfoPDOTable
    {
        public const string AVAILABLEQUANTITY_FLD = "AvailableQuantity";
        public const string BINID_FLD = "BinID";
        public const string BOMQUANTITY_FLD = "BomQuantity";
        public const string COMMITQUANTITY_FLD = "CommitQuantity";
        public const string COMPLETEDQUANTITY_FLD = "CompletedQuantity";
        public const string COMPONENTID_FLD = "ComponentID";
        public const string DESIGNUMID_FLD = "DesignUMID";
        public const string DUEDATE_FLD = "DueDate";
        public const string FGOODSCODE_FLD = "FGoodsCode";
        public const string ITM_CATEGORYCODE_FLD = "ITM_CategoryCode";
        public const string ITM_PRODUCTCODE_FLD = "ITM_ProductCode";
        public const string ITM_PRODUCTDESCRIPTION_FLD = "ITM_ProductDescription";
        public const string ITM_PRODUCTREVISION_FLD = "ITM_ProductRevision";
        public const string LOCATIONID_FLD = "LocationID";
        public const string LOTCONTROL_FLD = "LotControl";
        public const string LOTSIZE_FLD = "LotSize";
        public const string MASTERLOCATIONID_FLD = "MasterLocationID";
        public const string MST_BINCODE_FLD = "MST_BinCode";
        public const string MST_LOCATIONCODE_FLD = "MST_LocationCode";
        public const string MST_PARTYCODE_FLD = "MST_PartyCode";
        public const string MST_PARTYNAME_FLD = "MST_PartyName";
        public const string MST_UNITOFMEASURECODE_FLD = "MST_UnitOfMeasureCode";
        public const string NEEDEDQUANTITY_FLD = "NeededQuantity";
        public const string ORDERQUANTITY_FLD = "OrderQuantity";
        public const string PARENTCODE_FLD = "ParentCode";
        public const string PARENTNAME_FLD = "ParentName";
        public const string PARENTREVISION_FLD = "ParentRevision";
        public const string PRO_WORKORDERDETAILLINE_FLD = "PRO_WorkOrderDetailLine";
        public const string PRO_WORKORDERMASTERWORKORDERNO_FLD = "PRO_WorkOrderMasterWorkOrderNo";
        public const string PRODUCTID_FLD = "ProductID";
        public const string PRODUCTIONLINEID_FLD = "ProductionLineID";
        public const string PRODUCTIONLINELOCATIONID_FLD = "ProductionLineLocationID";
        public const string RATE_FLD = "Rate";
        public const string REMAINNEEDEDQUANTITY_FLD = "RemainNeededQuantity";
        public const string SHRINK_FLD = "Shrink";
        public const string STARTDATE_FLD = "StartDate";
        public const string STOCKUMID_FLD = "StockUMID";
        public const string TABLE_NAME = "v_RemainComponentForWOIssueWithParentInfoPDO";
        public const string WORKORDERDETAILID_FLD = "WorkOrderDetailID";
        public const string WORKORDERMASTERID_FLD = "WorkOrderMasterID";
    }

    public sealed class v_RemainDepositTable
    {
        public const string AMOUNT_FLD = "Amount";
        public const string APPLIEDAMOUNT_FLD = "AppliedAmount";
        public const string BANKID_FLD = "BankID";
        public const string BOOKSTATUS_FLD = "BookStatus";
        public const string CCNID_FLD = "CCNID";
        public const string CURRENCYID_FLD = "CurrencyID";
        public const string DEPOSITID_FLD = "DepositID";
        public const string DEPOSITTYPE_FLD = "DepositType";
        public const string DESCRIPTION_FLD = "Description";
        public const string EMPLOYEEID_FLD = "EmployeeID";
        public const string EXCHANGERATE_FLD = "ExchangeRate";
        public const string PARTYID_FLD = "PartyID";
        public const string POSTDATE_FLD = "PostDate";
        public const string PURCHASEORDERMASTERID_FLD = "PurchaseOrderMasterID";
        public const string REMAINDEPOSITAMOUNT_FLD = "RemainDepositAmount";
        public const string TABLE_NAME = "v_RemainDeposit";
        public const string TRANSDATE_FLD = "TransDate";
        public const string TRANSNO_FLD = "TransNo";
        public const string USERID_FLD = "UserID";
        public const string WORKORDERMASTERID_FLD = "WorkOrderMasterID";
    }

    public sealed class v_RemainExpenseTable
    {
        public const string BOOKSTATUS_FLD = "BookStatus";
        public const string CCNID_FLD = "CCNID";
        public const string CURRENCYID_FLD = "CurrencyID";
        public const string DESCRIPTION_FLD = "Description";
        public const string EMPLOYEEID_FLD = "EmployeeID";
        public const string EXCHANGERATE_FLD = "ExchangeRate";
        public const string EXPENSEMASTERID_FLD = "ExpenseMasterID";
        public const string EXPENSETYPE_FLD = "ExpenseType";
        public const string FIXASSETSTYPE_FLD = "FixAssetsType";
        public const string INVOICEAMOUNT_FLD = "InvoiceAmount";
        public const string INVOICETYPE_FLD = "InvoiceType";
        public const string PARTYID_FLD = "PartyID";
        public const string PAYMENTAMOUNT_FLD = "PaymentAmount";
        public const string PAYMENTTERMID_FLD = "PaymentTermID";
        public const string POSTDATE_FLD = "PostDate";
        public const string REMAINAMOUNT_FLD = "RemainAmount";
        public const string SOURCETYPE_FLD = "SourceType";
        public const string TABLE_NAME = "v_RemainExpense";
        public const string TRANSDATE_FLD = "TransDate";
        public const string TRANSNO_FLD = "TransNo";
        public const string USERID_FLD = "UserID";
        public const string WORKORDERMASTERID_FLD = "WorkOrderMasterID";
    }

    public sealed class v_RemainInvoiceTable
    {
        public const string BLDATE_FLD = "BLDate";
        public const string BLNUMBER_FLD = "BLNumber";
        public const string CARRIERID_FLD = "CarrierID";
        public const string CCNID_FLD = "CCNID";
        public const string CURRENCYID_FLD = "CurrencyID";
        public const string DECLARATIONDATE_FLD = "DeclarationDate";
        public const string DELIVERYTERMID_FLD = "DeliveryTermID";
        public const string EXCHANGERATE_FLD = "ExchangeRate";
        public const string EXPENSETYPE_FLD = "ExpenseType";
        public const string INFORMDATE_FLD = "InformDate";
        public const string INVOICEAMOUNT_FLD = "InvoiceAmount";
        public const string INVOICEMASTERID_FLD = "InvoiceMasterID";
        public const string INVOICENO_FLD = "InvoiceNo";
        public const string INVOICETYPE_FLD = "InvoiceType";
        public const string LASTCHANGE_FLD = "LastChange";
        public const string PARTYID_FLD = "PartyID";
        public const string PAYMENTAMOUNT_FLD = "PaymentAmount";
        public const string PAYMENTTERMID_FLD = "PaymentTermID";
        public const string POSTDATE_FLD = "PostDate";
        public const string PURCHASETYPEID_FLD = "PurchaseTypeID";
        public const string REMAINAMOUNT_FLD = "RemainAmount";
        public const string TABLE_NAME = "v_RemainInvoice";
        public const string TAXDECLARATIONNUMBER_FLD = "TaxDeclarationNumber";
        public const string TAXINFORMNUMBER_FLD = "TaxInformNumber";
        public const string TOTALCIFAMOUNT_FLD = "TotalCIFAmount";
        public const string TOTALCIPAMOUNT_FLD = "TotalCIPAmount";
        public const string TOTALIMPORTTAX_FLD = "TotalImportTax";
        public const string TOTALINLANDAMOUNT_FLD = "TotalInlandAmount";
        public const string TOTALVATAMOUNT_FLD = "TotalVATAmount";
        public const string USERNAME_FLD = "UserName";
    }

    public sealed class v_RemainWOForIssueTable
    {
        public const string LOCATIONID_FLD = "LocationID";
        public const string MASTERLOCATIONID_FLD = "MasterLocationID";
        public const string TABLE_NAME = "v_RemainWOForIssue";
        public const string TRANSDATE_FLD = "TransDate";
        public const string WORKORDERMASTERID_FLD = "WorkOrderMasterID";
        public const string WORKORDERNO_FLD = "WorkOrderNo";
    }

    public sealed class v_RemainWOForIssuePDOTable
    {
        public const string LOCATIONID_FLD = "LocationID";
        public const string MASTERLOCATIONID_FLD = "MasterLocationID";
        public const string SALEORDERDETAILID_FLD = "SaleOrderDetailID";
        public const string TABLE_NAME = "v_RemainWOForIssuePDO";
        public const string TRANSDATE_FLD = "TransDate";
        public const string WORKORDERMASTERID_FLD = "WorkOrderMasterID";
        public const string WORKORDERNO_FLD = "WorkOrderNo";
    }

    public sealed class v_RemainWOLineForIssueTable
    {
        public const string CODE_FLD = "Code";
        public const string DUEDATE_FLD = "DueDate";
        public const string LINE_FLD = "Line";
        public const string ORDERQUANTITY_FLD = "OrderQuantity";
        public const string STARTDATE_FLD = "StartDate";
        public const string TABLE_NAME = "v_RemainWOLineForIssue";
        public const string WORKORDERDETAILID_FLD = "WorkOrderDetailID";
        public const string WORKORDERMASTERID_FLD = "WorkOrderMasterID";
    }

    public sealed class v_RequiredForMiscIssueTable
    {
        public const string ISSUEPURPOSEID_FLD = "IssuePurposeID";
        public const string LASTUPDATE_FLD = "LastUpdate";
        public const string PRODUCTIONLINEID_FLD = "ProductionLineID";
        public const string REQUIREDMATERIALMASTERID_FLD = "RequiredMaterialMasterID";
        public const string SALEORDERDETAILID_FLD = "SaleOrderDetailID";
        public const string TABLE_NAME = "v_RequiredForMiscIssue";
        public const string TRANSDATE_FLD = "TransDate";
        public const string TRANSNO_FLD = "TransNo";
        public const string USERAPPROVAL_FLD = "UserApproval";
        public const string USERNAME_FLD = "Username";
    }

    public sealed class v_ReturnGoodsInMonthByCostElementTable
    {
        public const string ACTUALCOST_FLD = "ActualCost";
        public const string CGS1_FLD = "CGS1";
        public const string COSTELEMENTID_FLD = "CostElementID";
        public const string FROMDATE_FLD = "FromDate";
        public const string MONTH_FLD = "month";
        public const string PRODUCTID_FLD = "ProductID";
        public const string RTAMOUNT_FLD = "RTAmount";
        public const string RTQUANTITY_FLD = "RTQuantity";
        public const string TABLE_NAME = "v_ReturnGoodsInMonthByCostElement";
        public const string TODATE_FLD = "ToDate";
        public const string YEAR_FLD = "Year";
    }

    public sealed class v_SaleForEstimateTable
    {
        public const string AUTOCOMMIT_FLD = "AutoCommit";
        public const string BACKORDERQTY_FLD = "BackOrderQty";
        public const string CANCELREASONID_FLD = "CancelReasonID";
        public const string CONVERTEDQUANTITY_FLD = "ConvertedQuantity";
        public const string DISCOUNTAMOUNT_FLD = "DiscountAmount";
        public const string DUEDATE_FLD = "DueDate";
        public const string EXPORTTAXAMOUNT_FLD = "ExportTaxAmount";
        public const string EXPORTTAXPERCENT_FLD = "ExportTaxPercent";
        public const string FGOODSCODE_FLD = "FGoodsCode";
        public const string ITEMCUSTOMERCODE_FLD = "ItemCustomerCode";
        public const string ITEMCUSTOMERREVISION_FLD = "ItemCustomerRevision";
        public const string ITEMDESCRIPTION_FLD = "ItemDescription";
        public const string NETAMOUNT_FLD = "NetAmount";
        public const string ORDERQUANTITY_FLD = "OrderQuantity";
        public const string PRODUCTID_FLD = "ProductID";
        public const string REASONID_FLD = "ReasonID";
        public const string SALEORDERDETAILID_FLD = "SaleOrderDetailID";
        public const string SALEORDERLINE_FLD = "SaleOrderLine";
        public const string SALEORDERMASTERID_FLD = "SaleOrderMasterID";
        public const string SELLINGUMID_FLD = "SellingUMID";
        public const string SHIPCOMPLETED_FLD = "ShipCompleted";
        public const string SHIPQUANTITY_FLD = "ShipQuantity";
        public const string SPECIALTAXAMOUNT_FLD = "SpecialTaxAmount";
        public const string SPECIALTAXPERCENT_FLD = "SpecialTaxPercent";
        public const string STOCKQUANTITY_FLD = "StockQuantity";
        public const string STOCKUMID_FLD = "StockUMID";
        public const string TABLE_NAME = "v_SaleForEstimate";
        public const string TOTALAMOUNT_FLD = "TotalAmount";
        public const string UMRATE_FLD = "UMRate";
        public const string UNITPRICE_FLD = "UnitPrice";
        public const string VATAMOUNT_FLD = "VATAmount";
        public const string VATPERCENT_FLD = "VATPercent";
        public const string WOQUANTITY_FLD = "WOQuantity";
    }

    public sealed class v_SaleForRequestTable
    {
        public const string AUTOCOMMIT_FLD = "AutoCommit";
        public const string BACKORDERQTY_FLD = "BackOrderQty";
        public const string CANCELREASONID_FLD = "CancelReasonID";
        public const string CONVERTEDQUANTITY_FLD = "ConvertedQuantity";
        public const string DISCOUNTAMOUNT_FLD = "DiscountAmount";
        public const string DUEDATE_FLD = "DueDate";
        public const string ESTIMATEMASTERID_FLD = "EstimateMasterID";
        public const string EXPORTTAXAMOUNT_FLD = "ExportTaxAmount";
        public const string EXPORTTAXPERCENT_FLD = "ExportTaxPercent";
        public const string FGOODSCODE_FLD = "FGoodsCode";
        public const string ITEMCUSTOMERCODE_FLD = "ItemCustomerCode";
        public const string ITEMCUSTOMERREVISION_FLD = "ItemCustomerRevision";
        public const string ITEMDESCRIPTION_FLD = "ItemDescription";
        public const string NETAMOUNT_FLD = "NetAmount";
        public const string ORDERQUANTITY_FLD = "OrderQuantity";
        public const string PRODUCTID_FLD = "ProductID";
        public const string REASONID_FLD = "ReasonID";
        public const string SALEORDERDETAILID_FLD = "SaleOrderDetailID";
        public const string SALEORDERLINE_FLD = "SaleOrderLine";
        public const string SALEORDERMASTERID_FLD = "SaleOrderMasterID";
        public const string SELLINGUMID_FLD = "SellingUMID";
        public const string SHIPCOMPLETED_FLD = "ShipCompleted";
        public const string SHIPQUANTITY_FLD = "ShipQuantity";
        public const string SPECIALTAXAMOUNT_FLD = "SpecialTaxAmount";
        public const string SPECIALTAXPERCENT_FLD = "SpecialTaxPercent";
        public const string STOCKQUANTITY_FLD = "StockQuantity";
        public const string STOCKUMID_FLD = "StockUMID";
        public const string TABLE_NAME = "v_SaleForRequest";
        public const string TOTALAMOUNT_FLD = "TotalAmount";
        public const string UMRATE_FLD = "UMRate";
        public const string UNITPRICE_FLD = "UnitPrice";
        public const string VATAMOUNT_FLD = "VATAmount";
        public const string VATPERCENT_FLD = "VATPercent";
        public const string WOQUANTITY_FLD = "WOQuantity";
    }

    public sealed class v_SaleInvoiceTable
    {
        public const string CCNID_FLD = "CCNID";
        public const string CODE_FLD = "Code";
        public const string CUSTOMERCODE_FLD = "CustomerCode";
        public const string CUSTOMERNAME_FLD = "CustomerName";
        public const string MASLOC_FLD = "MasLoc";
        public const string MST_PAYMENTTERMCODE_FLD = "MST_PaymentTermCode";
        public const string SALEORDERMASTERID_FLD = "SaleOrderMasterID";
        public const string SALETYPE_FLD = "SaleType";
        public const string SHIPCOMPLETED_FLD = "ShipCompleted";
        public const string SHIPFROMLOCID_FLD = "ShipFromLocID";
        public const string TABLE_NAME = "v_SaleInvoice";
        public const string TOTALAMOUNT_FLD = "TotalAmount";
        public const string TRANSDATE_FLD = "TransDate";
        public const string TYPEID_FLD = "TypeID";
    }

    public sealed class v_SaleMasterWithFGoodsCodeTable
    {
        public const string BILLTOLOCID_FLD = "BillToLocID";
        public const string BUYINGLOCID_FLD = "BuyingLocID";
        public const string CANCELREASONID_FLD = "CancelReasonID";
        public const string CARRIERID_FLD = "CarrierID";
        public const string CCNID_FLD = "CCNID";
        public const string CODE_FLD = "Code";
        public const string CURRENCYID_FLD = "CurrencyID";
        public const string CUSTOMERPURCHASEORDERNO_FLD = "CustomerPurchaseOrderNo";
        public const string DELIVERYTERMSID_FLD = "DeliveryTermsID";
        public const string DISCOUNTTERMSID_FLD = "DiscountTermsID";
        public const string EXCHANGERATE_FLD = "ExchangeRate";
        public const string EXPORTTAX_FLD = "ExportTax";
        public const string EXPORTTAXRATE_FLD = "ExportTaxRate";
        public const string FGOODSCODE_FLD = "FGoodsCode";
        public const string FINISHEDDATE_FLD = "FinishedDate";
        public const string LASTCHANGE_FLD = "LastChange";
        public const string LOCATIONID_FLD = "LocationID";
        public const string PARTYCONTACTID_FLD = "PartyContactID";
        public const string PARTYID_FLD = "PartyID";
        public const string PAUSEID_FLD = "PauseID";
        public const string PAYMENTMETHODID_FLD = "PaymentMethodID";
        public const string PAYMENTTERMSID_FLD = "PaymentTermsID";
        public const string PRIORITY_FLD = "Priority";
        public const string PRODUCTID_FLD = "ProductID";
        public const string SALEORDERMASTERID_FLD = "SaleOrderMasterID";
        public const string SALESREPRESENTATIVEID_FLD = "SalesRepresentativeID";
        public const string SALESTATUSID_FLD = "SaleStatusID";
        public const string SALETYPEID_FLD = "SaleTypeID";
        public const string SHIPCOMPLETED_FLD = "ShipCompleted";
        public const string SHIPFROMLOCID_FLD = "ShipFromLocID";
        public const string SHIPTOLOCID_FLD = "ShipToLocID";
        public const string SPECIALTAX_FLD = "SpecialTax";
        public const string SPECIALTAXRATE_FLD = "SpecialTaxRate";
        public const string TABLE_NAME = "v_SaleMasterWithFGoodsCode";
        public const string TOTALAMOUNT_FLD = "TotalAmount";
        public const string TOTALDISCOUNTAMOUNT_FLD = "TotalDiscountAmount";
        public const string TOTALEXPORTAMOUNT_FLD = "TotalExportAmount";
        public const string TOTALNETAMOUNT_FLD = "TotalNetAmount";
        public const string TOTALSPECIALTAXAMOUNT_FLD = "TotalSpecialTaxAmount";
        public const string TOTALVATAMOUNT_FLD = "TotalVATAmount";
        public const string TRANSDATE_FLD = "TransDate";
        public const string TYPEID_FLD = "TypeID";
        public const string USERNAME_FLD = "UserName";
        public const string VAT_FLD = "VAT";
        public const string VATRATE_FLD = "VATRate";
    }

    public sealed class v_SaleOrderDetailWithCCNTable
    {
        public const string AUTOCOMMIT_FLD = "AutoCommit";
        public const string BACKORDERQTY_FLD = "BackOrderQty";
        public const string CANCELREASONID_FLD = "CancelReasonID";
        public const string CCNID_FLD = "CCNID";
        public const string CONVERTEDQUANTITY_FLD = "ConvertedQuantity";
        public const string DISCOUNTAMOUNT_FLD = "DiscountAmount";
        public const string DUEDATE_FLD = "DueDate";
        public const string EXPORTTAXAMOUNT_FLD = "ExportTaxAmount";
        public const string EXPORTTAXPERCENT_FLD = "ExportTaxPercent";
        public const string FGOODSCODE_FLD = "FGoodsCode";
        public const string ITEMCUSTOMERCODE_FLD = "ItemCustomerCode";
        public const string ITEMCUSTOMERREVISION_FLD = "ItemCustomerRevision";
        public const string ITEMDESCRIPTION_FLD = "ItemDescription";
        public const string NETAMOUNT_FLD = "NetAmount";
        public const string ORDERQUANTITY_FLD = "OrderQuantity";
        public const string PRODUCTID_FLD = "ProductID";
        public const string REASONID_FLD = "ReasonID";
        public const string SALEORDERDETAILID_FLD = "SaleOrderDetailID";
        public const string SALEORDERLINE_FLD = "SaleOrderLine";
        public const string SALEORDERMASTERID_FLD = "SaleOrderMasterID";
        public const string SELLINGUMID_FLD = "SellingUMID";
        public const string SHIPCOMPLETED_FLD = "ShipCompleted";
        public const string SHIPQUANTITY_FLD = "ShipQuantity";
        public const string SPECIALTAXAMOUNT_FLD = "SpecialTaxAmount";
        public const string SPECIALTAXPERCENT_FLD = "SpecialTaxPercent";
        public const string STOCKQUANTITY_FLD = "StockQuantity";
        public const string STOCKUMID_FLD = "StockUMID";
        public const string TABLE_NAME = "v_SaleOrderDetailWithCCN";
        public const string TOTALAMOUNT_FLD = "TotalAmount";
        public const string UMRATE_FLD = "UMRate";
        public const string UNITPRICE_FLD = "UnitPrice";
        public const string VATAMOUNT_FLD = "VATAmount";
        public const string VATPERCENT_FLD = "VATPercent";
        public const string WOQUANTITY_FLD = "WOQuantity";
    }

    public sealed class v_SaleOrderForWOLineTable
    {
        public const string CCNID_FLD = "CCNID";
        public const string CODE_FLD = "Code";
        public const string CUSTOMERPURCHASEORDERNO_FLD = "CustomerPurchaseOrderNo";
        public const string EXCHANGERATE_FLD = "ExchangeRate";
        public const string EXPORTTAX_FLD = "ExportTax";
        public const string EXPORTTAXRATE_FLD = "ExportTaxRate";
        public const string FGOODSCODE_FLD = "FGoodsCode";
        public const string PRIORITY_FLD = "Priority";
        public const string PRODUCTID_FLD = "ProductID";
        public const string SALEORDERCODE_FLD = "SaleOrderCode";
        public const string SALEORDERDETAILID_FLD = "SaleOrderDetailID";
        public const string SALEORDERLINE_FLD = "SaleOrderLine";
        public const string SALEORDERMASTERID_FLD = "SaleOrderMasterID";
        public const string SHIPCOMPLETED_FLD = "ShipCompleted";
        public const string SHIPFROMLOCID_FLD = "ShipFromLocID";
        public const string SPECIALTAX_FLD = "SpecialTax";
        public const string SPECIALTAXRATE_FLD = "SpecialTaxRate";
        public const string TABLE_NAME = "v_SaleOrderForWOLine";
        public const string TOTALAMOUNT_FLD = "TotalAmount";
        public const string TOTALDISCOUNTAMOUNT_FLD = "TotalDiscountAmount";
        public const string TOTALEXPORTAMOUNT_FLD = "TotalExportAmount";
        public const string TOTALNETAMOUNT_FLD = "TotalNetAmount";
        public const string TOTALSPECIALTAXAMOUNT_FLD = "TotalSpecialTaxAmount";
        public const string TOTALVATAMOUNT_FLD = "TotalVATAmount";
        public const string TRANSDATE_FLD = "TransDate";
        public const string VAT_FLD = "VAT";
        public const string VATRATE_FLD = "VATRate";
    }

    public sealed class v_SearchProductForMaterialReceiptTable
    {
        public const string BINID_FLD = "BinID";
        public const string CODE_FLD = "Code";
        public const string DESCRIPTION_FLD = "Description";
        public const string LOCATIONID_FLD = "LocationID";
        public const string LOTCONTROL_FLD = "LotControl";
        public const string LOTSIZE_FLD = "LotSize";
        public const string MASTERLOCATIONID_FLD = "MasterLocationID";
        public const string MST_BINCODE_FLD = "MST_BINCode";
        public const string MST_LOCATIONCODE_FLD = "MST_LocationCode";
        public const string MST_MASTERLOCATIONCODE_FLD = "MST_MasterLocationCode";
        public const string MST_UNITOFMEASURECODE_FLD = "MST_UnitOfMeasureCode";
        public const string PRODUCTID_FLD = "ProductID";
        public const string REVISION_FLD = "Revision";
        public const string STOCKUMID_FLD = "StockUMID";
        public const string TABLE_NAME = "v_SearchProductForMaterialReceipt";
    }

    public sealed class v_SelectPurchaseOrdersTable
    {
        public const string BUYINGUMID_FLD = "BuyingUMID";
        public const string CARRIERID_FLD = "CarrierID";
        public const string CCNID_FLD = "CCNID";
        public const string CLOSED_FLD = "Closed";
        public const string DELIVERYLINE_FLD = "DeliveryLine";
        public const string DELIVERYQUANTITY_FLD = "DeliveryQuantity";
        public const string DELIVERYSCHEDULEID_FLD = "DeliveryScheduleID";
        public const string DELIVERYTERMSID_FLD = "DeliveryTermsID";
        public const string INVENTORYACCOUNTID_FLD = "InventoryAccountID";
        public const string ITM_PRODUCTCODE_FLD = "ITM_ProductCode";
        public const string ITM_PRODUCTDESCRIPTION_FLD = "ITM_ProductDescription";
        public const string ITM_PRODUCTQASTATUS_FLD = "ITM_ProductQAStatus";
        public const string ITM_PRODUCTREVISION_FLD = "ITM_ProductRevision";
        public const string MST_PARTYCODE_FLD = "MST_PartyCode";
        public const string MST_UNITOFMEASURECODE_FLD = "MST_UnitOfMeasureCode";
        public const string ORDERDATE_FLD = "OrderDate";
        public const string OTHERINFO1_FLD = "OtherInfo1";
        public const string PARTNAMEVN_FLD = "PartNameVN";
        public const string PARTYCONTACTID_FLD = "PartyContactID";
        public const string PARTYID_FLD = "PartyID";
        public const string PAYMENTTERMSID_FLD = "PaymentTermsID";
        public const string PO_PURCHASEORDERDETAILIMPORTTAX_FLD = "PO_PurchaseOrderDetailImportTax";
        public const string PO_PURCHASEORDERDETAILLINE_FLD = "PO_PurchaseOrderDetailLine";
        public const string PO_PURCHASEORDERDETAILSPECIALTAX_FLD = "PO_PurchaseOrderDetailSpecialTax";
        public const string PO_PURCHASEORDERDETAILUNITPRICE_FLD = "PO_PurchaseOrderDetailUnitPrice";
        public const string PO_PURCHASEORDERDETAILVAT_FLD = "PO_PurchaseOrderDetailVAT";
        public const string PO_PURCHASEORDERMASTERCODE_FLD = "PO_PurchaseOrderMasterCode";
        public const string PRODUCTID_FLD = "ProductID";
        public const string PURCHASEORDERDETAILID_FLD = "PurchaseOrderDetailId";
        public const string PURCHASEORDERMASTERID_FLD = "PurchaseOrderMasterId";
        public const string RECEIVEDQUANTITY_FLD = "ReceivedQuantity";
        public const string SCHEDULEDATE_FLD = "ScheduleDate";
        public const string TABLE_NAME = "v_SelectPurchaseOrders";
        public const string TAXCODE_FLD = "TaxCode";
    }

    public sealed class v_SelectUnclosedPO4InvoiceTable
    {
        public const string CCNID_FLD = "CCNID";
        public const string MAKERID_FLD = "MakerID";
        public const string MST_CURRENCYCODE_FLD = "MST_CurrencyCode";
        public const string MST_PARTYNAME_FLD = "MST_PartyName";
        public const string ORDERDATE_FLD = "OrderDate";
        public const string PARTYID_FLD = "PartyID";
        public const string PO_PURCHASEORDERMASTERCODE_FLD = "PO_PurchaseOrderMasterCode";
        public const string PURCHASEORDERMASTERID_FLD = "PurchaseOrderMasterID";
        public const string TABLE_NAME = "v_SelectUnclosedPO4Invoice";
        public const string TOTALAMOUNT_FLD = "TotalAmount";
        public const string TOTALIMPORTTAX_FLD = "TotalImportTax";
        public const string TOTALVAT_FLD = "TotalVAT";
    }

    public sealed class V_SelectWOBaseProductionLineTable
    {
        public const string CCNID_FLD = "CCNID";
        public const string DCOPTIONMASTERID_FLD = "DCOptionMasterID";
        public const string DESCRIPTION_FLD = "Description";
        public const string LASTCHANGE_FLD = "LastChange";
        public const string MASTERLOCATIONID_FLD = "MasterLocationID";
        public const string PARTYID_FLD = "PartyID";
        public const string PRODUCEREASONID_FLD = "ProduceReasonID";
        public const string PRODUCTIONLINE_FLD = "ProductionLine";
        public const string PRODUCTIONLINEID_FLD = "ProductionLineID";
        public const string TABLE_NAME = "V_SelectWOBaseProductionLine";
        public const string TRANSDATE_FLD = "TransDate";
        public const string USERNAME_FLD = "UserName";
        public const string WORKORDERMASTERID_FLD = "WorkOrderMasterID";
        public const string WORKORDERNO_FLD = "WorkOrderNo";
    }

    public sealed class v_ShippedSaleOrderTable
    {
        public const string CCNID_FLD = "CCNID";
        public const string SALEORDERMASTERID_FLD = "SaleOrderMasterID";
        public const string SHIPPEDDATE_FLD = "ShippedDate";
        public const string SONO_FLD = "SONo";
        public const string TABLE_NAME = "v_ShippedSaleOrder";
    }

    public sealed class v_ShippingInMonthByCostElementTable
    {
        public const string ACTUALCOST_FLD = "ActualCost";
        public const string CGS1_FLD = "CGS1";
        public const string COSTELEMENTID_FLD = "CostElementID";
        public const string FROMDATE_FLD = "FromDate";
        public const string INMONTH_FLD = "InMonth";
        public const string INYEAR_FLD = "InYear";
        public const string PRODUCTID_FLD = "ProductID";
        public const string SHIPPEDAMOUNT_FLD = "ShippedAmount";
        public const string SHIPPEDQTY_FLD = "ShippedQty";
        public const string TABLE_NAME = "v_ShippingInMonthByCostElement";
        public const string TODATE_FLD = "ToDate";
    }

    public sealed class v_ShippingInMonthByCostElement_RptTable
    {
        public const string ADJUSTMENT_FLD = "Adjustment";
        public const string CATEGORY_FLD = "Category";
        public const string CATEGORYID_FLD = "CategoryID";
        public const string CGS1_FLD = "CGS1";
        public const string COSTELEMENTID_FLD = "CostElementID";
        public const string COSTELEMENTTYPEID_FLD = "CostElementTypeID";
        public const string INMONTH_FLD = "InMonth";
        public const string INYEAR_FLD = "InYear";
        public const string MODEL_FLD = "Model";
        public const string PARTNAME_FLD = "PartName";
        public const string PARTNO_FLD = "PartNo";
        public const string PRODUCTID_FLD = "ProductID";
        public const string REVENUE_FLD = "Revenue";
        public const string SELLINGQTY_FLD = "SellingQty";
        public const string STOCKUMID_FLD = "StockUMID";
        public const string TABLE_NAME = "v_ShippingInMonthByCostElement_Rpt";
        public const string UM_FLD = "UM";
    }

    public sealed class v_SO_SaleOrderMasterHasCommitTable
    {
        public const string BILLTOLOCID_FLD = "BillToLocID";
        public const string BUYINGLOCID_FLD = "BuyingLocID";
        public const string CANCELREASONID_FLD = "CancelReasonID";
        public const string CARRIERID_FLD = "CarrierID";
        public const string CCNID_FLD = "CCNID";
        public const string CODE_FLD = "Code";
        public const string CURRENCYID_FLD = "CurrencyID";
        public const string CUSTOMERPURCHASEORDERNO_FLD = "CustomerPurchaseOrderNo";
        public const string DELIVERYTERMSID_FLD = "DeliveryTermsID";
        public const string DISCOUNTTERMSID_FLD = "DiscountTermsID";
        public const string EXCHANGERATE_FLD = "ExchangeRate";
        public const string EXPORTTAX_FLD = "ExportTax";
        public const string EXPORTTAXRATE_FLD = "ExportTaxRate";
        public const string LOCATIONID_FLD = "LocationID";
        public const string PARTYCONTACTID_FLD = "PartyContactID";
        public const string PARTYID_FLD = "PartyID";
        public const string PAUSEID_FLD = "PauseID";
        public const string PAYMENTMETHODID_FLD = "PaymentMethodID";
        public const string PAYMENTTERMSID_FLD = "PaymentTermsID";
        public const string PRIORITY_FLD = "Priority";
        public const string SALEORDERMASTERID_FLD = "SaleOrderMasterID";
        public const string SALESREPRESENTATIVEID_FLD = "SalesRepresentativeID";
        public const string SALESTATUSID_FLD = "SaleStatusID";
        public const string SALETYPEID_FLD = "SaleTypeID";
        public const string SHIPCOMPLETED_FLD = "ShipCompleted";
        public const string SHIPFROMLOCID_FLD = "ShipFromLocID";
        public const string SHIPTOLOCID_FLD = "ShipToLocID";
        public const string SPECIALTAX_FLD = "SpecialTax";
        public const string SPECIALTAXRATE_FLD = "SpecialTaxRate";
        public const string TABLE_NAME = "v_SO_SaleOrderMasterHasCommit";
        public const string TOTALAMOUNT_FLD = "TotalAmount";
        public const string TOTALDISCOUNTAMOUNT_FLD = "TotalDiscountAmount";
        public const string TOTALEXPORTAMOUNT_FLD = "TotalExportAmount";
        public const string TOTALNETAMOUNT_FLD = "TotalNetAmount";
        public const string TOTALSPECIALTAXAMOUNT_FLD = "TotalSpecialTaxAmount";
        public const string TOTALVATAMOUNT_FLD = "TotalVATAmount";
        public const string TRANSDATE_FLD = "TransDate";
        public const string VAT_FLD = "VAT";
        public const string VATRATE_FLD = "VATRate";
    }

    public sealed class v_SOCancelCommitmentTable
    {
        public const string BUYINGLOCID_FLD = "BuyingLocID";
        public const string CCNID_FLD = "CCNID";
        public const string CODE_FLD = "Code";
        public const string PARTYID_FLD = "PartyID";
        public const string SALEORDERMASTERID_FLD = "SaleOrderMasterID";
        public const string TABLE_NAME = "v_SOCancelCommitment";
    }

    public sealed class v_SOConfirmShipMasterTable
    {
        public const string CCNID_FLD = "CCNID";
        public const string CNO_FLD = "CNo";
        public const string CODE_FLD = "Code";
        public const string COMMENT_FLD = "Comment";
        public const string CONFIRMSHIPMASTERID_FLD = "ConfirmShipMasterID";
        public const string CONFIRMSHIPNO_FLD = "ConfirmShipNo";
        public const string CURRENCYID_FLD = "CurrencyID";
        public const string CUSTOMERCODE_FLD = "CustomerCode";
        public const string CUSTOMERNAME_FLD = "CustomerName";
        public const string EXCHANGERATE_FLD = "ExchangeRate";
        public const string FROMPORT_FLD = "FromPort";
        public const string GATEID_FLD = "GateID";
        public const string GROSSWEIGHT_FLD = "GrossWeight";
        public const string INVOICEDATE_FLD = "InvoiceDate";
        public const string INVOICENO_FLD = "InvoiceNo";
        public const string ISSUINGBANK_FLD = "IssuingBank";
        public const string LCDATE_FLD = "LCDate";
        public const string LCNO_FLD = "LCNo";
        public const string MASLOC_FLD = "MasLoc";
        public const string MASTERLOCATIONID_FLD = "MasterLocationID";
        public const string MEASUREMENT_FLD = "Measurement";
        public const string MST_PAYMENTTERMCODE_FLD = "MST_PaymentTermCode";
        public const string NETWEIGHT_FLD = "NetWeight";
        public const string ONBOARDDATE_FLD = "OnBoardDate";
        public const string PARTYID_FLD = "PartyID";
        public const string REFERENCENO_FLD = "ReferenceNo";
        public const string SALEORDER_FLD = "SaleOrder";
        public const string SALEORDERMASTERID_FLD = "SaleOrderMasterID";
        public const string SALETYPE_FLD = "SaleType";
        public const string SALETYPEID_FLD = "SaleTypeID";
        public const string SHIPCODE_FLD = "ShipCode";
        public const string SHIPPEDDATE_FLD = "ShippedDate";
        public const string SO_GATECODE_FLD = "SO_GateCode";
        public const string SO_TYPECODE_FLD = "SO_TypeCode";
        public const string TABLE_NAME = "v_SOConfirmShipMaster";
        public const string TYPEID_FLD = "TypeID";
        public const string VESSELNAME_FLD = "VesselName";
    }

    public sealed class v_SOInvoiceMasterTable
    {
        public const string CCNID_FLD = "CCNID";
        public const string CNO_FLD = "CNo";
        public const string CODE_FLD = "Code";
        public const string COMMENT_FLD = "Comment";
        public const string CONFIRMSHIPNO_FLD = "ConfirmShipNo";
        public const string CURRENCYID_FLD = "CurrencyID";
        public const string CUSTOMERCODE_FLD = "CustomerCode";
        public const string CUSTOMERNAME_FLD = "CustomerName";
        public const string EXCHANGERATE_FLD = "ExchangeRate";
        public const string FROMPORT_FLD = "FromPort";
        public const string GATEID_FLD = "GateID";
        public const string GROSSWEIGHT_FLD = "GrossWeight";
        public const string INVOICEDATE_FLD = "InvoiceDate";
        public const string INVOICEMASTERID_FLD = "InvoiceMasterID";
        public const string INVOICENO_FLD = "InvoiceNo";
        public const string ISSUINGBANK_FLD = "IssuingBank";
        public const string LCDATE_FLD = "LCDate";
        public const string LCNO_FLD = "LCNo";
        public const string MASLOC_FLD = "MasLoc";
        public const string MASTERLOCATIONID_FLD = "MasterLocationID";
        public const string MEASUREMENT_FLD = "Measurement";
        public const string MST_PAYMENTTERMCODE_FLD = "MST_PaymentTermCode";
        public const string NETWEIGHT_FLD = "NetWeight";
        public const string ONBOARDDATE_FLD = "OnBoardDate";
        public const string PARTYID_FLD = "PartyID";
        public const string REFERENCENO_FLD = "ReferenceNo";
        public const string SALEORDER_FLD = "SaleOrder";
        public const string SALEORDERMASTERID_FLD = "SaleOrderMasterID";
        public const string SALETYPE_FLD = "SaleType";
        public const string SALETYPEID_FLD = "SaleTypeID";
        public const string SHIPCODE_FLD = "ShipCode";
        public const string SHIPPEDDATE_FLD = "ShippedDate";
        public const string SO_GATECODE_FLD = "SO_GateCode";
        public const string SO_TYPECODE_FLD = "SO_TypeCode";
        public const string TABLE_NAME = "v_SOInvoiceMaster";
        public const string TYPEID_FLD = "TypeID";
        public const string VESSELNAME_FLD = "VesselName";
    }

    public sealed class v_SOMasterForShippingManagementTable
    {
        public const string CCNID_FLD = "CCNID";
        public const string CODE_FLD = "Code";
        public const string CUSTOMERCODE_FLD = "CustomerCode";
        public const string CUSTOMERNAME_FLD = "CustomerName";
        public const string MASLOC_FLD = "MasLoc";
        public const string MST_PAYMENTTERMCODE_FLD = "MST_PaymentTermCode";
        public const string SALEORDERMASTERID_FLD = "SaleOrderMasterID";
        public const string SALETYPE_FLD = "SaleType";
        public const string SHIPCOMPLETED_FLD = "ShipCompleted";
        public const string SHIPFROMLOCID_FLD = "ShipFromLocID";
        public const string TABLE_NAME = "v_SOMasterForShippingManagement";
        public const string TOTALAMOUNT_FLD = "TotalAmount";
        public const string TRANSDATE_FLD = "TransDate";
        public const string TYPEID_FLD = "TypeID";
    }

    public sealed class v_SOMasterNotReleaseTable
    {
        public const string CCNID_FLD = "CCNID";
        public const string CODE_FLD = "Code";
        public const string CURRENCY_FLD = "Currency";
        public const string PARTY_FLD = "Party";
        public const string SALEORDERMASTERID_FLD = "SaleOrderMasterID";
        public const string TABLE_NAME = "v_SOMasterNotRelease";
        public const string TOTALAMOUNT_FLD = "TotalAmount";
        public const string TOTALNETAMOUNT_FLD = "TotalNetAmount";
        public const string TRANSDATE_FLD = "TransDate";
    }

    public sealed class v_SOMasterToCommitTable
    {
        public const string BILLTOLOCID_FLD = "BillToLocID";
        public const string BUYINGLOCID_FLD = "BuyingLocID";
        public const string CANCELREASONID_FLD = "CancelReasonID";
        public const string CARRIERID_FLD = "CarrierID";
        public const string CCNID_FLD = "CCNID";
        public const string CODE_FLD = "Code";
        public const string CURRENCYID_FLD = "CurrencyID";
        public const string CUSTOMERPURCHASEORDERNO_FLD = "CustomerPurchaseOrderNo";
        public const string DELIVERYTERMSID_FLD = "DeliveryTermsID";
        public const string DISCOUNTTERMSID_FLD = "DiscountTermsID";
        public const string EXCHANGERATE_FLD = "ExchangeRate";
        public const string EXPORTTAX_FLD = "ExportTax";
        public const string EXPORTTAXRATE_FLD = "ExportTaxRate";
        public const string LOCATIONID_FLD = "LocationID";
        public const string PARTYCONTACTID_FLD = "PartyContactID";
        public const string PARTYID_FLD = "PartyID";
        public const string PAUSEID_FLD = "PauseID";
        public const string PAYMENTMETHODID_FLD = "PaymentMethodID";
        public const string PAYMENTTERMSID_FLD = "PaymentTermsID";
        public const string PRIORITY_FLD = "Priority";
        public const string SALEORDERMASTERID_FLD = "SaleOrderMasterID";
        public const string SALESREPRESENTATIVEID_FLD = "SalesRepresentativeID";
        public const string SALESTATUSID_FLD = "SaleStatusID";
        public const string SALETYPEID_FLD = "SaleTypeID";
        public const string SHIPCOMPLETED_FLD = "ShipCompleted";
        public const string SHIPFROMLOCID_FLD = "ShipFromLocID";
        public const string SHIPTOLOCID_FLD = "ShipToLocID";
        public const string SPECIALTAX_FLD = "SpecialTax";
        public const string SPECIALTAXRATE_FLD = "SpecialTaxRate";
        public const string TABLE_NAME = "v_SOMasterToCommit";
        public const string TOTALAMOUNT_FLD = "TotalAmount";
        public const string TOTALDISCOUNTAMOUNT_FLD = "TotalDiscountAmount";
        public const string TOTALEXPORTAMOUNT_FLD = "TotalExportAmount";
        public const string TOTALNETAMOUNT_FLD = "TotalNetAmount";
        public const string TOTALSPECIALTAXAMOUNT_FLD = "TotalSpecialTaxAmount";
        public const string TOTALVATAMOUNT_FLD = "TotalVATAmount";
        public const string TRANSDATE_FLD = "TransDate";
        public const string VAT_FLD = "VAT";
        public const string VATRATE_FLD = "VATRate";
    }

    public sealed class V_SONotCompletedShipTable
    {
        public const string CODE_FLD = "Code";
        public const string PRODUCTID_FLD = "ProductID";
        public const string SALEORDERMASTERID_FLD = "SaleOrderMasterID";
        public const string TABLE_NAME = "V_SONotCompletedShip";
    }

    public sealed class v_SOToCommitTable
    {
        public const string AUTOCOMMIT_FLD = "AutoCommit";
        public const string BACKORDERQTY_FLD = "BackOrderQty";
        public const string CANCELREASONID_FLD = "CancelReasonID";
        public const string COMMITQUANTITY_FLD = "CommitQuantity";
        public const string CONVERTEDQUANTITY_FLD = "ConvertedQuantity";
        public const string DELIVERYSCHEDULEID_FLD = "DeliveryScheduleID";
        public const string DESCRIPTION_FLD = "Description";
        public const string DISCOUNTAMOUNT_FLD = "DiscountAmount";
        public const string EXPORTTAXAMOUNT_FLD = "ExportTaxAmount";
        public const string EXPORTTAXPERCENT_FLD = "ExportTaxPercent";
        public const string ITEMCUSTOMERCODE_FLD = "ItemCustomerCode";
        public const string ITEMCUSTOMERREVISION_FLD = "ItemCustomerRevision";
        public const string ITM_PRODUCTCODE_FLD = "ITM_ProductCode";
        public const string LINE_FLD = "Line";
        public const string MST_UNITOFMEASURECODE_FLD = "MST_UnitOfMeasureCode";
        public const string NETAMOUNT_FLD = "NetAmount";
        public const string PRODUCTID_FLD = "ProductID";
        public const string REASONID_FLD = "ReasonID";
        public const string REVISION_FLD = "Revision";
        public const string SALEORDERDETAILID_FLD = "SaleOrderDetailID";
        public const string SALEORDERMASTERID_FLD = "SaleOrderMasterID";
        public const string SCHEDULEDATE_FLD = "ScheduleDate";
        public const string SELLINGUMID_FLD = "SellingUMID";
        public const string SHIPQUANTITY_FLD = "ShipQuantity";
        public const string SPECIALTAXAMOUNT_FLD = "SpecialTaxAmount";
        public const string SPECIALTAXPERCENT_FLD = "SpecialTaxPercent";
        public const string STOCKQUANTITY_FLD = "StockQuantity";
        public const string STOCKUMID_FLD = "StockUMID";
        public const string TABLE_NAME = "v_SOToCommit";
        public const string TOTALAMOUNT_FLD = "TotalAmount";
        public const string UMRATE_FLD = "UMRate";
        public const string UNITPRICE_FLD = "UnitPrice";
        public const string VATAMOUNT_FLD = "VATAmount";
        public const string VATPERCENT_FLD = "VATPercent";
    }

    public sealed class v_SOToReturnedTable
    {
        public const string BILLTOLOCID_FLD = "BillToLocID";
        public const string BUYINGLOCID_FLD = "BuyingLocID";
        public const string CANCELREASONID_FLD = "CancelReasonID";
        public const string CARRIERID_FLD = "CarrierID";
        public const string CCNID_FLD = "CCNID";
        public const string CODE_FLD = "Code";
        public const string CURRENCYID_FLD = "CurrencyID";
        public const string CUSTOMERPURCHASEORDERNO_FLD = "CustomerPurchaseOrderNo";
        public const string DELIVERYTERMSID_FLD = "DeliveryTermsID";
        public const string DISCOUNTTERMSID_FLD = "DiscountTermsID";
        public const string EXCHANGERATE_FLD = "ExchangeRate";
        public const string EXPORTTAX_FLD = "ExportTax";
        public const string EXPORTTAXRATE_FLD = "ExportTaxRate";
        public const string LASTCHANGE_FLD = "LastChange";
        public const string LOCATIONID_FLD = "LocationID";
        public const string PARTYCONTACTID_FLD = "PartyContactID";
        public const string PARTYID_FLD = "PartyID";
        public const string PAUSEID_FLD = "PauseID";
        public const string PAYMENTMETHODID_FLD = "PaymentMethodID";
        public const string PAYMENTTERMSID_FLD = "PaymentTermsID";
        public const string PRIORITY_FLD = "Priority";
        public const string SALEORDERMASTERID_FLD = "SaleOrderMasterID";
        public const string SALESREPRESENTATIVEID_FLD = "SalesRepresentativeID";
        public const string SALESTATUSID_FLD = "SaleStatusID";
        public const string SALETYPEID_FLD = "SaleTypeID";
        public const string SHIPCOMPLETED_FLD = "ShipCompleted";
        public const string SHIPFROMLOCID_FLD = "ShipFromLocID";
        public const string SHIPTOLOCID_FLD = "ShipToLocID";
        public const string SPECIALTAX_FLD = "SpecialTax";
        public const string SPECIALTAXRATE_FLD = "SpecialTaxRate";
        public const string TABLE_NAME = "v_SOToReturned";
        public const string TOTALAMOUNT_FLD = "TotalAmount";
        public const string TOTALDISCOUNTAMOUNT_FLD = "TotalDiscountAmount";
        public const string TOTALEXPORTAMOUNT_FLD = "TotalExportAmount";
        public const string TOTALNETAMOUNT_FLD = "TotalNetAmount";
        public const string TOTALSPECIALTAXAMOUNT_FLD = "TotalSpecialTaxAmount";
        public const string TOTALVATAMOUNT_FLD = "TotalVATAmount";
        public const string TRANSDATE_FLD = "TransDate";
        public const string TYPEID_FLD = "TypeID";
        public const string USERNAME_FLD = "UserName";
        public const string VAT_FLD = "VAT";
        public const string VATRATE_FLD = "VATRate";
    }

    public sealed class V_SumActCostTable
    {
        public const string COST_FLD = "Cost";
        public const string PRODUCTID_FLD = "ProductID";
        public const string TABLE_NAME = "V_SumActCost";
        public const string TODATE_FLD = "Todate";
    }

    public sealed class v_SumAllocated_GroupByProductTable
    {
        public const string ACTCOSTALLOCATIONMASTERID_FLD = "ActCostAllocationMasterID";
        public const string LABOR_FLD = "Labor";
        public const string MACHINE_FLD = "Machine";
        public const string MATERIAL_FLD = "Material";
        public const string OVERHEAD_FLD = "OverHead";
        public const string PRODUCTID_FLD = "ProductID";
        public const string SUBMATERIAL_FLD = "SubMaterial";
        public const string TABLE_NAME = "v_SumAllocated_GroupByProduct";
    }

    public sealed class V_SumWOCompletionCostTable
    {
        public const string COMPLETIONCOST_FLD = "CompletionCost";
        public const string PRODUCTID_FLD = "ProductID";
        public const string TABLE_NAME = "V_SumWOCompletionCost";
        public const string TODATE_FLD = "ToDate";
    }

    public sealed class V_TheKhoTable
    {
        public const string BEGINSTOCK_FLD = "BeginStock";
        public const string BINID_FLD = "BinID";
        public const string COMMENT_FLD = "Comment";
        public const string INNO_FLD = "InNo";
        public const string INQTY_FLD = "InQty";
        public const string LOCID_FLD = "LocID";
        public const string OUTNO_FLD = "OutNo";
        public const string OUTQTY_FLD = "OutQty";
        public const string POSTDATE_FLD = "PostDate";
        public const string PRODUCTID_FLD = "ProductID";
        public const string SALEORDERDETAILID_FLD = "SaleOrderDetailID";
        public const string TABLE_NAME = "V_TheKho";
    }

    public sealed class v_Total_DS_Before_AllocationTable
    {
        public const string ACTCOSTALLOCATIONMASTERID_FLD = "ActCostAllocationMasterID";
        public const string ADJUSTAMOUNT_FLD = "AdjustAmount";
        public const string COSTELEMENTID_FLD = "CostElementID";
        public const string DSAMOUNT_FLD = "DSAmount";
        public const string FROMDATE_FLD = "FromDate";
        public const string RECYCLEAMOUNT_FLD = "RecycleAmount";
        public const string TABLE_NAME = "v_Total_DS_Before_Allocation";
        public const string TODATE_FLD = "ToDate";
    }

    public sealed class v_TotalCommitInventoryTable
    {
        public const string COMMITQUANTITY_FLD = "CommitQuantity";
        public const string DELIVERYSCHEDULEID_FLD = "DeliveryScheduleID";
        public const string PRODUCTID_FLD = "ProductID";
        public const string SALEORDERMASTERID_FLD = "SaleOrderMasterID";
        public const string TABLE_NAME = "v_TotalCommitInventory";
    }

    public sealed class v_TotalQuantityReturnToVendorTable
    {
        public const string LOT_FLD = "Lot";
        public const string PRODUCTID_FLD = "ProductID";
        public const string PURCHASEORDERMASTERID_FLD = "PurchaseOrderMasterID";
        public const string SERIAL_FLD = "Serial";
        public const string TABLE_NAME = "v_TotalQuantityReturnToVendor";
        public const string TOTALRETURN_FLD = "TotalReturn";
    }

    public sealed class v_TotalReturnedGoodsTable
    {
        public const string LOT_FLD = "Lot";
        public const string PRODUCTID_FLD = "ProductID";
        public const string RECEIVEQUANTITY_FLD = "ReceiveQuantity";
        public const string SALEORDERMASTERID_FLD = "SaleOrderMasterID";
        public const string SERIAL_FLD = "Serial";
        public const string TABLE_NAME = "v_TotalReturnedGoods";
    }

    public sealed class v_TransactionHistoryTable
    {
        public const string BINID_FLD = "BinID";
        public const string CCNID_FLD = "CCNID";
        public const string LOCATIONID_FLD = "LocationID";
        public const string MASTERLOCATIONID_FLD = "MasterLocationID";
        public const string POSTDATE_FLD = "PostDate";
        public const string PRODUCTID_FLD = "ProductID";
        public const string TABLE_NAME = "v_TransactionHistory";
        public const string TRANSDATE_FLD = "TransDate";
        public const string TRANSQUANTITY_FLD = "TransQuantity";
        public const string TRANTYPEID_FLD = "TranTypeID";
    }

    public sealed class v_TransferLocationTable
    {
        public const string FROMBINID_FLD = "FromBinID";
        public const string FROMLOCID_FLD = "FromLocID";
        public const string MONTHS_FLD = "months";
        public const string PRODUCTID_FLD = "ProductID";
        public const string QTY_FLD = "Qty";
        public const string TABLE_NAME = "v_TransferLocation";
        public const string TOBINID_FLD = "ToBinID";
        public const string TOLOCID_FLD = "ToLocID";
        public const string YEARS_FLD = "years";
    }

    public sealed class v_UnitOfActualCostTable
    {
        public const string ACTCOSTALLOCATIONMASTERID_FLD = "ActCostAllocationMasterID";
        public const string CATEGORYID_FLD = "CategoryID";
        public const string FROMDATE_FLD = "FromDate";
        public const string PRODUCTID_FLD = "productID";
        public const string TABLE_NAME = "v_UnitOfActualCost";
        public const string TODATE_FLD = "ToDate";
        public const string UNITCOST_FLD = "UnitCost";
    }

    public sealed class v_UnitOfActualCost_ByCostElementTable
    {
        public const string ACTCOSTALLOCATIONMASTERID_FLD = "ActCostAllocationMasterID";
        public const string ACTUALCOST_FLD = "ActualCost";
        public const string ADJUSTAMOUNT_FLD = "AdjustAmount";
        public const string CATEGORYID_FLD = "CategoryID";
        public const string COSTELEMENTID_FLD = "CostElementID";
        public const string DS_OKAMOUNT_FLD = "DS_OKAmount";
        public const string DSAMOUNT_FLD = "DSAmount";
        public const string FROMDATE_FLD = "FromDate";
        public const string PRODUCTID_FLD = "productID";
        public const string RECYCLEAMOUNT_FLD = "RecycleAmount";
        public const string TABLE_NAME = "v_UnitOfActualCost_ByCostElement";
        public const string TODATE_FLD = "ToDate";
    }

    public sealed class v_UnitOfActualCost_NotDSTable
    {
        public const string ACTCOSTALLOCATIONMASTERID_FLD = "ActCostAllocationMasterID";
        public const string CATEGORYID_FLD = "CategoryID";
        public const string FROMDATE_FLD = "FromDate";
        public const string PRODUCTID_FLD = "productID";
        public const string TABLE_NAME = "v_UnitOfActualCost_NotDS";
        public const string TODATE_FLD = "ToDate";
        public const string UNITCOST_FLD = "UnitCost";
    }

    public sealed class v_UnitOfActualCost1Table
    {
        public const string ACTCOSTALLOCATIONMASTERID_FLD = "ActCostAllocationMasterID";
        public const string ADJUSTAMOUNT_FLD = "AdjustAmount";
        public const string CATEGORYID_FLD = "CategoryID";
        public const string DSAMOUNT_FLD = "DSAmount";
        public const string FROMDATE_FLD = "FromDate";
        public const string PRODUCTID_FLD = "productID";
        public const string RECYCLEAMOUNT_FLD = "RecycleAmount";
        public const string TABLE_NAME = "v_UnitOfActualCost1";
        public const string TODATE_FLD = "ToDate";
        public const string UNITCOST_FLD = "UnitCost";
    }

    public sealed class v_vendorTable
    {
        public const string ADDRESS_FLD = "Address";
        public const string CODE_FLD = "Code";
        public const string NAME_FLD = "Name";
        public const string PARTYID_FLD = "PartyID";
        public const string TABLE_NAME = "v_vendor";
    }

    public sealed class V_VendorCustomerTable
    {
        public const string CODE_FLD = "Code";
        public const string CUSTOMER_FLD = "Customer";
        public const string MST_PARTYCURRENCY_FLD = "MST_PartyCurrency";
        public const string NAME_FLD = "Name";
        public const string PARTYID_FLD = "PartyID";
        public const string TABLE_NAME = "V_VendorCustomer";
        public const string TYPE_FLD = "Type";
        public const string VENDOR_FLD = "Vendor";
    }

    public sealed class v_WCCapacityByCategoryTable
    {
        public const string CAPACITY_FLD = "Capacity";
        public const string CATEGORYID_FLD = "CategoryID";
        public const string CODE_FLD = "Code";
        public const string ISMAIN_FLD = "IsMain";
        public const string LEADTIME_FLD = "LeadTime";
        public const string PRODUCTID_FLD = "ProductID";
        public const string QUANTITYSET_FLD = "QuantitySet";
        public const string TABLE_NAME = "v_WCCapacityByCategory";
        public const string WORKCENTERID_FLD = "WorkCenterID";
    }

    public sealed class v_WO_BOM_PlanningTable
    {
        public const string COMPONENTID_FLD = "ComponentID";
        public const string DUEDATE_FLD = "DueDate";
        public const string LEADTIMEOFFSET_FLD = "LeadTimeOffset";
        public const string MASTERLOCATIONID_FLD = "MasterLocationID";
        public const string ORDERQUANTITY_FLD = "OrderQuantity";
        public const string PRODUCTID_FLD = "ProductID";
        public const string REQUIREDQUANTITY_FLD = "RequiredQuantity";
        public const string SHRINK_FLD = "Shrink";
        public const string STARTDATE_FLD = "StartDate";
        public const string STOCKUMID_FLD = "StockUMID";
        public const string TABLE_NAME = "v_WO_BOM_Planning";
        public const string TRANSDATE_FLD = "TransDate";
        public const string WORKORDERDETAILID_FLD = "WorkOrderDetailID";
        public const string WORKORDERNO_FLD = "WorkOrderNo";
    }

    public sealed class v_WO_MaterialIssue_PlanningTable
    {
        public const string ISSUENO_FLD = "IssueNo";
        public const string ISSUEQTITY_FLD = "IssueQtity";
        public const string MASTERLOCATIONID_FLD = "MasterLocationID";
        public const string POSTDATE_FLD = "PostDate";
        public const string PRODUCTID_FLD = "ProductID";
        public const string TABLE_NAME = "v_WO_MaterialIssue_Planning";
        public const string WORKORDERDETAILID_FLD = "WorkOrderDetailID";
    }

    public sealed class v_WOCompletionTable
    {
        public const string BIN_FLD = "Bin";
        public const string BINCODE_FLD = "BinCode";
        public const string BINID_FLD = "BinID";
        public const string BINTYPEID_FLD = "BinTypeID";
        public const string CATEGORYID_FLD = "CategoryID";
        public const string COMPLETEDQUANTITY_FLD = "CompletedQuantity";
        public const string DUEDATE_FLD = "DueDate";
        public const string LINE_FLD = "Line";
        public const string LOCATIONCODE_FLD = "LocationCode";
        public const string LOCATIONID_FLD = "LocationID";
        public const string LOTCONTROL_FLD = "LotControl";
        public const string LOTSIZE_FLD = "LotSize";
        public const string MASTERLOCATIONID_FLD = "MasterLocationID";
        public const string MODEL_FLD = "Model";
        public const string ORDERQUANTITY_FLD = "OrderQuantity";
        public const string PARTNAME_FLD = "PartName";
        public const string PARTNUMBER_FLD = "PartNumber";
        public const string PRODUCTID_FLD = "ProductID";
        public const string QASTATUS_FLD = "QAStatus";
        public const string SCRAPQUANTITY_FLD = "ScrapQuantity";
        public const string STARTDATE_FLD = "StartDate";
        public const string STATUS_FLD = "Status";
        public const string STOCKUMID_FLD = "StockUMID";
        public const string TABLE_NAME = "v_WOCompletion";
        public const string UM_FLD = "UM";
        public const string WORKORDERDETAILID_FLD = "WorkOrderDetailID";
        public const string WORKORDERMASTERID_FLD = "WorkOrderMasterID";
    }

    public sealed class v_WOCompletion_Begin_CostTable
    {
        public const string ACTCOSTALLOCATIONMASTERID_FLD = "ActCostAllocationMasterID";
        public const string BEGINCOST_FLD = "BeginCost";
        public const string FROMDATE_FLD = "FromDate";
        public const string PRODUCTID_FLD = "ProductID";
        public const string TABLE_NAME = "v_WOCompletion_Begin_Cost";
        public const string TODATE_FLD = "ToDate";
        public const string WOCOMPLETIONCOST_FLD = "WOCompletionCost";
    }

    public sealed class v_WOCompletionPDOTable
    {
        public const string BIN_FLD = "Bin";
        public const string BINCODE_FLD = "BinCode";
        public const string BINID_FLD = "BinID";
        public const string BINTYPEID_FLD = "BinTypeID";
        public const string CATEGORYID_FLD = "CategoryID";
        public const string COMPLETEDQUANTITY_FLD = "CompletedQuantity";
        public const string DUEDATE_FLD = "DueDate";
        public const string LINE_FLD = "Line";
        public const string LOCATIONCODE_FLD = "LocationCode";
        public const string LOCATIONID_FLD = "LocationID";
        public const string LOTCONTROL_FLD = "LotControl";
        public const string LOTSIZE_FLD = "LotSize";
        public const string MASTERLOCATIONID_FLD = "MasterLocationID";
        public const string MODEL_FLD = "Model";
        public const string ORDERQUANTITY_FLD = "OrderQuantity";
        public const string PARTNAME_FLD = "PartName";
        public const string PARTNUMBER_FLD = "PartNumber";
        public const string PRODUCTID_FLD = "ProductID";
        public const string PRODUCTIONLINEID_FLD = "ProductionLineID";
        public const string PRODUCTTYPEID_FLD = "ProductTypeID";
        public const string QASTATUS_FLD = "QAStatus";
        public const string SALEORDERDETAILID_FLD = "SaleOrderDetailID";
        public const string SCRAPQUANTITY_FLD = "ScrapQuantity";
        public const string STARTDATE_FLD = "StartDate";
        public const string STATUS_FLD = "Status";
        public const string STOCKUMID_FLD = "StockUMID";
        public const string TABLE_NAME = "v_WOCompletionPDO";
        public const string UM_FLD = "UM";
        public const string WORKORDERDETAILID_FLD = "WorkOrderDetailID";
        public const string WORKORDERMASTERID_FLD = "WorkOrderMasterID";
    }

    public sealed class V_WODetailAndProductInfoTable
    {
        public const string CODE_FLD = "Code";
        public const string DESCRIPTION_FLD = "Description";
        public const string LINE_FLD = "Line";
        public const string PRODUCTID_FLD = "ProductID";
        public const string PRODUCTIONLINEID_FLD = "ProductionLineID";
        public const string REVISION_FLD = "Revision";
        public const string STATUS_FLD = "Status";
        public const string TABLE_NAME = "V_WODetailAndProductInfo";
        public const string UM_FLD = "UM";
        public const string WORKORDERDETAILID_FLD = "WorkOrderDetailID";
        public const string WORKORDERMASTERID_FLD = "WorkOrderMasterID";
        public const string WORKORDERNO_FLD = "WorkOrderNo";
    }

    public sealed class v_WODetailInforTable
    {
        public const string COSTMETHOD_FLD = "CostMethod";
        public const string DUEDATE_FLD = "DueDate";
        public const string LINE_FLD = "Line";
        public const string MODEL_FLD = "Model";
        public const string ORDERQUANTITY_FLD = "OrderQuantity";
        public const string PARTNAME_FLD = "PartName";
        public const string PARTNUMBER_FLD = "PartNumber";
        public const string PRODUCTID_FLD = "ProductID";
        public const string STARTDATE_FLD = "StartDate";
        public const string STATUS_FLD = "Status";
        public const string STOCKUMID_FLD = "StockUMID";
        public const string TABLE_NAME = "v_WODetailInfor";
        public const string UM_FLD = "UM";
        public const string WORKORDERDETAILID_FLD = "WorkOrderDetailID";
        public const string WORKORDERMASTERID_FLD = "WorkOrderMasterID";
    }

    public sealed class v_WOReleaseTable
    {
        public const string CCNID_FLD = "CCNID";
        public const string MASTERLOCATIONID_FLD = "MasterLocationID";
        public const string PRODUCTIONLINEID_FLD = "ProductionLineID";
        public const string STATUS_FLD = "Status";
        public const string TABLE_NAME = "v_WORelease";
        public const string TRANSDATE_FLD = "TransDate";
        public const string WORKORDERMASTERID_FLD = "WorkOrderMasterID";
        public const string WORKORDERNO_FLD = "WorkOrderNo";
    }

    public sealed class V_WOReleaseFGoodsCodeForCompletionTable
    {
        public const string CCNID_FLD = "CCNID";
        public const string FGOODSCODE_FLD = "FGoodsCode";
        public const string MASTERLOCATIONID_FLD = "MasterLocationID";
        public const string PRODUCTIONLINEID_FLD = "ProductionLineID";
        public const string STATUS_FLD = "Status";
        public const string TABLE_NAME = "V_WOReleaseFGoodsCodeForCompletion";
        public const string TRANSDATE_FLD = "TransDate";
        public const string WORKORDERMASTERID_FLD = "WorkOrderMasterID";
        public const string WORKORDERNO_FLD = "WorkOrderNo";
    }

    public sealed class v_WOReleaseForCompletionTable
    {
        public const string CCNID_FLD = "CCNID";
        public const string MASTERLOCATIONID_FLD = "MasterLocationID";
        public const string PRODUCTIONLINEID_FLD = "ProductionLineID";
        public const string STATUS_FLD = "Status";
        public const string TABLE_NAME = "v_WOReleaseForCompletion";
        public const string TRANSDATE_FLD = "TransDate";
        public const string WORKCENTERID_FLD = "WorkCenterID";
        public const string WORKORDERMASTERID_FLD = "WorkOrderMasterID";
        public const string WORKORDERNO_FLD = "WorkOrderNo";
    }

    public sealed class v_WOReleaseForCompletionPDOTable
    {
        public const string CCNID_FLD = "CCNID";
        public const string MASTERLOCATIONID_FLD = "MasterLocationID";
        public const string PRODUCTIONLINEID_FLD = "ProductionLineID";
        public const string STATUS_FLD = "Status";
        public const string TABLE_NAME = "v_WOReleaseForCompletionPDO";
        public const string TRANSDATE_FLD = "TransDate";
        public const string WORKORDERMASTERID_FLD = "WorkOrderMasterID";
        public const string WORKORDERNO_FLD = "WorkOrderNo";
    }

    public sealed class v_WOReleasePDOTable
    {
        public const string MASTERLOCATIONID_FLD = "MasterLocationID";
        public const string PRODUCTIONLINEID_FLD = "ProductionLineID";
        public const string SALEORDERDETAILID_FLD = "SaleOrderDetailID";
        public const string STATUS_FLD = "Status";
        public const string TABLE_NAME = "v_WOReleasePDO";
        public const string TRANSDATE_FLD = "TransDate";
        public const string WORKORDERMASTERID_FLD = "WorkOrderMasterID";
        public const string WORKORDERNO_FLD = "WorkOrderNo";
    }

    public sealed class v_WorkCenterByActualCostTable
    {
        public const string ACDSOPTIONMASTERID_FLD = "ACDSOptionMasterID";
        public const string CCNID_FLD = "CCNID";
        public const string CODE_FLD = "Code";
        public const string NAME_FLD = "Name";
        public const string TABLE_NAME = "v_WorkCenterByActualCost";
        public const string WORKCENTERID_FLD = "WorkCenterID";
    }

    public sealed class v_WorkOrderCompletionTable
    {
        public const string BINID_FLD = "BinID";
        public const string CATEGORYID_FLD = "CategoryID";
        public const string CCNID_FLD = "CCNID";
        public const string COMPLETEDQUANTITY_FLD = "CompletedQuantity";
        public const string ISSUEPURPOSEID_FLD = "IssuePurposeID";
        public const string LASTCHANGE_FLD = "LastChange";
        public const string LOCATIONID_FLD = "LocationID";
        public const string LOT_FLD = "Lot";
        public const string MASTERLOCATIONID_FLD = "MasterLocationID";
        public const string POSTDATE_FLD = "PostDate";
        public const string PRODUCTID_FLD = "ProductID";
        public const string QASTATUS_FLD = "QAStatus";
        public const string REMARK_FLD = "Remark";
        public const string SERIAL_FLD = "Serial";
        public const string SHIFTID_FLD = "ShiftID";
        public const string STOCKUMID_FLD = "StockUMID";
        public const string TABLE_NAME = "v_WorkOrderCompletion";
        public const string USERNAME_FLD = "UserName";
        public const string WOCOMPLETIONNO_FLD = "WOCompletionNo";
        public const string WORKORDERCOMPLETIONID_FLD = "WorkOrderCompletionID";
        public const string WORKORDERDETAILID_FLD = "WorkOrderDetailID";
        public const string WORKORDERMASTERID_FLD = "WorkOrderMasterID";
    }

    public sealed class v_WorkOrderCompletionPDOTable
    {
        public const string BINID_FLD = "BinID";
        public const string CATEGORYID_FLD = "CategoryID";
        public const string CCNID_FLD = "CCNID";
        public const string COMPLETEDQUANTITY_FLD = "CompletedQuantity";
        public const string FGOODSCODE_FLD = "FGoodsCode";
        public const string ISSUEPURPOSEID_FLD = "IssuePurposeID";
        public const string LASTCHANGE_FLD = "LastChange";
        public const string LOCATIONID_FLD = "LocationID";
        public const string LOT_FLD = "Lot";
        public const string MASTERLOCATIONID_FLD = "MasterLocationID";
        public const string POSTDATE_FLD = "PostDate";
        public const string PRODUCTID_FLD = "ProductID";
        public const string PRODUCTIONLINEID_FLD = "ProductionLineID";
        public const string QASTATUS_FLD = "QAStatus";
        public const string REMARK_FLD = "Remark";
        public const string SALEORDERDETAILID_FLD = "SaleOrderDetailID";
        public const string SERIAL_FLD = "Serial";
        public const string SHIFTID_FLD = "ShiftID";
        public const string STOCKUMID_FLD = "StockUMID";
        public const string TABLE_NAME = "v_WorkOrderCompletionPDO";
        public const string USERNAME_FLD = "UserName";
        public const string WOCOMPLETIONNO_FLD = "WOCompletionNo";
        public const string WORKORDERCOMPLETIONID_FLD = "WorkOrderCompletionID";
        public const string WORKORDERDETAILID_FLD = "WorkOrderDetailID";
        public const string WORKORDERMASTERID_FLD = "WorkOrderMasterID";
    }

    public sealed class v_WorkOrderDetailTable
    {
        public const string AGC_FLD = "AGC";
        public const string CATEGORYID_FLD = "CategoryID";
        public const string DESCRIPTION_FLD = "Description";
        public const string DUEDATE_FLD = "DueDate";
        public const string ESTCST_FLD = "EstCst";
        public const string FINCLOSEDATE_FLD = "FinCloseDate";
        public const string INCREMENT_FLD = "Increment";
        public const string LINE_FLD = "Line";
        public const string MFGCLOSEDATE_FLD = "MfgCloseDate";
        public const string ORDERQUANTITY_FLD = "OrderQuantity";
        public const string PRIORITY_FLD = "Priority";
        public const string PRODUCTID_FLD = "ProductID";
        public const string SALEORDERDETAILID_FLD = "SaleOrderDetailID";
        public const string SALEORDERMASTERID_FLD = "SaleOrderMasterID";
        public const string STARTDATE_FLD = "StartDate";
        public const string STATUS_FLD = "Status";
        public const string STOCKUMID_FLD = "StockUMID";
        public const string TABLE_NAME = "v_WorkOrderDetail";
        public const string WORKORDERDETAILID_FLD = "WorkOrderDetailID";
        public const string WORKORDERMASTERID_FLD = "WorkOrderMasterID";
    }

    public sealed class v_WorkOrderDetailRemainQuantityTable
    {
        public const string ESTCST_FLD = "EstCst";
        public const string LINE_FLD = "Line";
        public const string ORDERQUANTITY_FLD = "OrderQuantity";
        public const string PRODUCTID_FLD = "ProductID";
        public const string REMAINQUANTITY_FLD = "RemainQuantity";
        public const string TABLE_NAME = "v_WorkOrderDetailRemainQuantity";
        public const string WORKORDERDETAILID_FLD = "WorkOrderDetailID";
        public const string WORKORDERMASTERID_FLD = "WorkOrderMasterID";
    }

    public sealed class v_WorkOrderForIssueMaterialTable
    {
        public const string DUEDATE_FLD = "DueDate";
        public const string ITM_PRODUCTCODE_FLD = "ITM_ProductCode";
        public const string ITM_PRODUCTDESCRIPTION_FLD = "ITM_ProductDescription";
        public const string ITM_PRODUCTREVISION_FLD = "ITM_ProductRevision";
        public const string LINE_FLD = "Line";
        public const string LOCATIONID_FLD = "LocationID";
        public const string MASTERLOCATIONID_FLD = "MasterLocationID";
        public const string MST_UNITOFMEASURECODE_FLD = "MST_UnitOfMeasureCode";
        public const string ORDERQUANTITY_FLD = "OrderQuantity";
        public const string PRO_WORKORDERMASTERWORKORDERNO_FLD = "PRO_WorkOrderMasterWorkOrderNo";
        public const string PRODUCTID_FLD = "ProductID";
        public const string SELECTED_FLD = "SELECTED";
        public const string STARTDATE_FLD = "StartDate";
        public const string STATUS_FLD = "Status";
        public const string STOCKUMID_FLD = "StockUMID";
        public const string TABLE_NAME = "v_WorkOrderForIssueMaterial";
        public const string WORKORDERDETAILID_FLD = "WorkOrderDetailID";
        public const string WORKORDERMASTERID_FLD = "WorkOrderMasterID";
    }

    public sealed class v_WorkOrderItemDetailTable
    {
        public const string COSTMETHOD_FLD = "CostMethod";
        public const string DUEDATE_FLD = "DueDate";
        public const string ITM_PRODUCTCODE_FLD = "ITM_ProductCode";
        public const string ITM_PRODUCTREVISION_FLD = "ITM_ProductRevision";
        public const string MST_UNITOFMEASURECODE_FLD = "MST_UnitOfMeasureCode";
        public const string MST_UNITOFMEASUREDESCRIPTION_FLD = "MST_UnitOfMeasureDescription";
        public const string ORDERQUANTITY_FLD = "OrderQuantity";
        public const string PARTNUMBER_FLD = "PartNumber";
        public const string PRO_WORKORDERDETAILLINE_FLD = "PRO_WorkOrderDetailLine";
        public const string PRODUCTID_FLD = "ProductID";
        public const string STARTDATE_FLD = "StartDate";
        public const string STATUS_FLD = "Status";
        public const string STOCKUMID_FLD = "StockUMID";
        public const string TABLE_NAME = "v_WorkOrderItemDetail";
        public const string WORKORDERDETAILID_FLD = "WorkOrderDetailID";
        public const string WORKORDERMASTERID_FLD = "WorkOrderMasterID";
    }

    public sealed class Sys_HiddenControlsTable
    {
        public const string CONTROLNAME_FLD = "ControlName";
        public const string FORMNAME_FLD = "FormName";
        public const string HIDDENCONTROLSID_FLD = "HiddenControlsID";
        public const string SUBCONTROLNAME_FLD = "SubControlName";
        public const string TABLE_NAME = "Sys_HiddenControls";
    }

    public sealed class Sys_HiddenControls_RoleTable
    {
        public const string HIDDENCONTROLS_ROLEID_FLD = "HiddenControls_RoleID";
        public const string HIDDENCONTROLSID_FLD = "HiddenControlsID";
        public const string ROLEID_FLD = "RoleID";
        public const string TABLE_NAME = "Sys_HiddenControls_Role";
    }

    public sealed class Store_GridLayOutTable
    {
        public const string CAPTION_FLD = "Caption";
        public const string COL_NAME_FLD = "ColName";
        public const string COLOR_FLD = "Color";
        public const string TABLE_NAME = "Grid";
        public const string WIDTH_FLD = "Width";
        public const string LOCKED_FLD = "Locked";
    }

    public sealed class AR_ReceiveFromPartnerDetailTable
    {
        public const string COMMITINVENTORYMASTERID_FLD = "CommitInventoryMasterID";
        public const string INVOICEAMOUNT_FLD = "InvoiceAmount";
        public const string OTHERRECEIVABLEMASTERID_FLD = "OtherReceivableMasterID";
        public const string RECEIVEAMOUNT_FLD = "ReceiveAmount";
        public const string RECEIVEFROMPARTNERDETAILID_FLD = "ReceiveFromPartnerDetailID";
        public const string RECEIVEFROMPARTNERMASTERID_FLD = "ReceiveFromPartnerMasterID";
        public const string REMAINAMOUNT_FLD = "RemainAmount";
        public const string TABLE_NAME = "AR_ReceiveFromPartnerDetail";
        public const string TRANTYPEID_FLD = "TranTypeID";
    }

    public sealed class AR_ReceiveFromPartnerMasterTable
    {
        public const string BANKID_FLD = "BankID";
        public const string BOOKSTATUS_FLD = "BookStatus";
        public const string CCNID_FLD = "CCNID";
        public const string CONTROLSTATUS_FLD = "ControlStatus";
        public const string CURRENCYID_FLD = "CurrencyID";
        public const string EMPLOYEEID_FLD = "EmployeeID";
        public const string EXCHANGERATE_FLD = "ExchangeRate";
        public const string LASTUPDATE_FLD = "LastUpdate";
        public const string PARTYID_FLD = "PartyID";
        public const string PAYMENTTYPE_FLD = "PaymentType";
        public const string POSTDATE_FLD = "PostDate";
        public const string RECEIVABLEAMOUNT_FLD = "ReceivableAmount";
        public const string RECEIVEAMOUNT_FLD = "ReceiveAmount";
        public const string RECEIVEFROMPARTNERMASTERID_FLD = "ReceiveFromPartnerMasterID";
        public const string REMAINAMOUNT_FLD = "RemainAmount";
        public const string TABLE_NAME = "AR_ReceiveFromPartnerMaster";
        public const string TRANSNO_FLD = "TransNo";
        public const string USERID_FLD = "UserID";
    }

    public sealed class AR_CommissionForAgentDetailTable
    {
        public const string AGENTTYPEID_FLD = "AgentTypeID";
        public const string BONUSAMOUNT_FLD = "BonusAmount";
        public const string COMMISSIONFORAGENTDETAILID_FLD = "CommissionForAgentDetailID";
        public const string COMMISSIONFORAGENTMASTERID_FLD = "CommissionForAgentMasterID";
        public const string COMMISSIONPERCENT_FLD = "CommissionPercent";
        public const string TABLE_NAME = "AR_CommissionForAgentDetail";
        public const string TURNOVER_FLD = "Turnover";
    }

    public sealed class AR_CommissionForAgentMasterTable
    {
        public const string ACTIVE_FLD = "Active";
        public const string CCNID_FLD = "CCNID";
        public const string CODE_FLD = "Code";
        public const string COMMISSIONFORAGENTMASTERID_FLD = "CommissionForAgentMasterID";
        public const string FROMDATE_FLD = "FromDate";
        public const string TABLE_NAME = "AR_CommissionForAgentMaster";
        public const string TODATE_FLD = "ToDate";
        public const string USERID_FLD = "UserID";
    }

    public sealed class AR_CommissionForProductDetailTable
    {
        public const string BONUSAMOUNT_FLD = "BonusAmount";
        public const string COMMISIONPERCENT_FLD = "CommisionPercent";
        public const string COMMISSIONFORPRODUCTDETAILID_FLD = "CommissionForProductDetailID";
        public const string COMMISSIONFORPRODUCTMASTERID_FLD = "CommissionForProductMasterID";
        public const string PRODUCTID_FLD = "ProductID";
        public const string TABLE_NAME = "AR_CommissionForProductDetail";
        public const string TURNOVER_FLD = "Turnover";
    }

    public sealed class AR_CommissionForProductMasterTable
    {
        public const string ACTIVE_FLD = "Active";
        public const string AGENTTYPEID_FLD = "AgentTypeID";
        public const string CCNID_FLD = "CCNID";
        public const string CODE_FLD = "Code";
        public const string COMMISSIONFORPRODUCTMASTERID_FLD = "CommissionForProductMasterID";
        public const string FROMDATE_FLD = "FromDate";
        public const string TABLE_NAME = "AR_CommissionForProductMaster";
        public const string TODATE_FLD = "ToDate";
        public const string USERID_FLD = "UserID";
    }

    public sealed class AR_PriceForAgentLevelDetailTable
    {
        public const string DISCOUNTAMOUNT_FLD = "DiscountAmount";
        public const string DISCOUNTPERCENT_FLD = "DiscountPercent";
        public const string PRICE_FLD = "Price";
        public const string PRICEFORAGENTLEVELDETAILID_FLD = "PriceForAgentLevelDetailID";
        public const string PRICEFORAGENTLEVELMASTERID_FLD = "PriceForAgentLevelMasterID";
        public const string PRODUCTID_FLD = "ProductID";
        public const string SELLINGUMID_FLD = "SellingUMID";
        public const string TABLE_NAME = "AR_PriceForAgentLevelDetail";
    }

    public sealed class AR_PriceForAgentLevelMasterTable
    {
        public const string ACTIVE_FLD = "Active";
        public const string AGENTTYPEID_FLD = "AgentTypeID";
        public const string CCNID_FLD = "CCNID";
        public const string CODE_FLD = "Code";
        public const string FROMDATE_FLD = "FromDate";
        public const string PRICEFORAGENTLEVELMASTERID_FLD = "PriceForAgentLevelMasterID";
        public const string PRICELISTMASTERID_FLD = "PriceListMasterID";
        public const string TABLE_NAME = "AR_PriceForAgentLevelMaster";
        public const string TODATE_FLD = "ToDate";
        public const string USERID_FLD = "UserID";
    }

    public sealed class AR_PriceListDetailTable
    {
        public const string DISCOUNTPERCENT_FLD = "DiscountPercent";
        public const string DISCOUNTQUANTITY_FLD = "DiscountQuantity";
        public const string PRICE_FLD = "Price";
        public const string PRICELISTDETAILID_FLD = "PriceListDetailID";
        public const string PRICELISTMASTERID_FLD = "PriceListMasterID";
        public const string PRODUCTID_FLD = "ProductID";
        public const string SELLINGUMID_FLD = "SellingUMID";
        public const string TABLE_NAME = "AR_PriceListDetail";
    }

    public sealed class AR_PriceListMasterTable
    {
        public const string ACTIVE_FLD = "Active";
        public const string CCNID_FLD = "CCNID";
        public const string CODE_FLD = "Code";
        public const string FROMDATE_FLD = "FromDate";
        public const string PRICELISTMASTERID_FLD = "PriceListMasterID";
        public const string TABLE_NAME = "AR_PriceListMaster";
        public const string TODATE_FLD = "ToDate";
        public const string USERID_FLD = "UserID";
    }

    public sealed class v_ItemByAgentTable
    {
        public const string CCNID_FLD = "CCNID";
        public const string CODE_FLD = "Code";
        public const string CUSTOMERITEMCODE_FLD = "CustomerItemCode";
        public const string CUSTOMERITEMMODEL_FLD = "CustomerItemModel";
        public const string CUSTOMERITEMREFMASTERID_FLD = "CustomerItemRefMasterID";
        public const string DESCRIPTION_FLD = "Description";
        public const string DISCOUNTAMOUNT_FLD = "DiscountAmount";
        public const string DISCOUNTPERCENT_FLD = "DiscountPercent";
        public const string DISCOUNTQTY_FLD = "DiscountQty";
        public const string EXPORTTAX_FLD = "ExportTax";
        public const string ITM_CATEGORYCODE_FLD = "ITM_CategoryCode";
        public const string MST_UNITOFMEASURECODE_FLD = "MST_UnitOfMeasureCode";
        public const string PRODUCTID_FLD = "ProductID";
        public const string REVISION_FLD = "Revision";
        public const string SELLINGUMID_FLD = "SellingUMID";
        public const string SPECIALTAX_FLD = "SpecialTax";
        public const string STOCKUMID_FLD = "StockUMID";
        public const string TABLE_NAME = "v_ItemByAgent";
        public const string UNITPRICE_FLD = "UnitPrice";
        public const string VAT_FLD = "VAT";
    }

    public sealed class v_ItemByPriceListTable
    {
        public const string CCNID_FLD = "CCNID";
        public const string CODE_FLD = "Code";
        public const string CUSTOMERITEMCODE_FLD = "CustomerItemCode";
        public const string CUSTOMERITEMMODEL_FLD = "CustomerItemModel";
        public const string DESCRIPTION_FLD = "Description";
        public const string DISCOUNTAMOUNT_FLD = "DiscountAmount";
        public const string DISCOUNTPERCENT_FLD = "DiscountPercent";
        public const string DISCOUNTQUANTITY_FLD = "DiscountQuantity";
        public const string EXPORTTAX_FLD = "ExportTax";
        public const string ITM_CATEGORYCODE_FLD = "ITM_CategoryCode";
        public const string MST_UNITOFMEASURECODE_FLD = "MST_UnitOfMeasureCode";
        public const string PRICE_FLD = "Price";
        public const string PRICELISTMASTERID_FLD = "PriceListMasterID";
        public const string PRODUCTID_FLD = "ProductID";
        public const string REVISION_FLD = "Revision";
        public const string SELLINGUMID_FLD = "SellingUMID";
        public const string SPECIALTAX_FLD = "SpecialTax";
        public const string STOCKUMID_FLD = "StockUMID";
        public const string TABLE_NAME = "v_ItemByPriceList";
        public const string VAT_FLD = "VAT";
    }

    public sealed class FA_AdjustmentRatioTable
    {
        public const string ADJUSTMENTRATIOID_FLD = "AdjustmentRatioID";
        public const string CODE_FLD = "Code";
        public const string DESCRIPTION_FLD = "Description";
        public const string RATIO_FLD = "Ratio";
        public const string TABLE_NAME = "FA_AdjustmentRatio";
    }

    public sealed class FA_CategoryTable
    {
        public const string CATEGORYID_FLD = "CategoryID";
        public const string CODE_FLD = "Code";
        public const string DESCRIPTION_FLD = "Description";
        public const string PARENTID_FLD = "ParentID";
        public const string TABLE_NAME = "FA_Category";
    }

    public sealed class FA_DepreciationPlanTable
    {
        public const string ACCUMULATEDDEPRECIATION_FLD = "AccumulatedDepreciation";
        public const string DEPRECIATEDVALUE_FLD = "DepreciatedValue";
        public const string DEPRECIATIONPLANID_FLD = "DepreciationPlanID";
        public const string DEPRECIATIONRATE_FLD = "DepreciationRate";
        public const string FACARDMASTERID_FLD = "FACardMasterID";
        public const string FROMDATE_FLD = "FromDate";
        public const string REMAINVALUE_FLD = "RemainValue";
        public const string TABLE_NAME = "FA_DepreciationPlan";
        public const string TODATE_FLD = "ToDate";
        public const string YEARNO_FLD = "YearNo";
    }

    public sealed class FA_FACardDetailTable
    {
        public const string DEPRECIATIONVALUE_FLD = "DepreciationValue";
        public const string FACARDDETAILID_FLD = "FACardDetailID";
        public const string FACARDMASTERID_FLD = "FACardMasterID";
        public const string ORIGINALPRICE_FLD = "OriginalPrice";
        public const string SOURCE_FLD = "Source";
        public const string SOURCEOFCAPITALID_FLD = "SourceOfCapitalID";
        public const string TABLE_NAME = "FA_FACardDetail";
        public const string TRANSDATE_FLD = "TransDate";
        public const string TRANSNO_FLD = "TransNo";
        public const string TRANTYPEID_FLD = "TranTypeID";
    }

    public sealed class FA_FACardMasterTable
    {
        public const string ACTIVE_FLD = "Active";
        public const string ADDDATE_FLD = "AddDate";
        public const string ADDREASON_FLD = "AddReason";
        public const string ADJUSTMENTRATIOID_FLD = "AdjustmentRatioID";
        public const string CATEGORYID_FLD = "CategoryID";
        public const string CCNID_FLD = "CCNID";
        public const string CODE_FLD = "Code";
        public const string CODEBYMAKER_FLD = "CodeByMaker";
        public const string COUNTRYID_FLD = "CountryID";
        public const string DEPARTMENTID_FLD = "DepartmentID";
        public const string DEPRECIATIONACCOUNTID_FLD = "DepreciationAccountID";
        public const string DEPRECIATIONDATE_FLD = "DepreciationDate";
        public const string DEPRECIATIONDAYS_FLD = "DepreciationDays";
        public const string DEPRECIATIONMETHOD_FLD = "DepreciationMethod";
        public const string DEPRECIATIONMONTHS_FLD = "DepreciationMonths";
        public const string DEPRECIATIONVALUE_FLD = "DepreciationValue";
        public const string DEPRECIATIONYEARS_FLD = "DepreciationYears";
        public const string FACARDMASTERID_FLD = "FACardMasterID";
        public const string FAGROUP_FLD = "FAGroup";
        public const string FIXEDASSETACCOUNTID_FLD = "FixedAssetAccountID";
        public const string LASTUPDATE_FLD = "LastUpdate";
        public const string MFGDATE_FLD = "MfgDate";
        public const string MFGPLACE_FLD = "MfgPlace";
        public const string NAME_FLD = "Name";
        public const string NAMEBYMAKER_FLD = "NameByMaker";
        public const string NOTE_FLD = "Note";
        public const string ORIGINALPRICE_FLD = "OriginalPrice";
        public const string PRODUCTGROUPID_FLD = "ProductGroupID";
        public const string REMAINVALUE_FLD = "RemainValue";
        public const string SPENDINGACCOUNTID_FLD = "SpendingAccountID";
        public const string SUSPENDDATE_FLD = "SuspendDate";
        public const string SUSPENDREASON_FLD = "SuspendReason";
        public const string SUSPNEND_FLD = "Suspnend";
        public const string TABLE_NAME = "FA_FACardMaster";
        public const string TECHNICALSPECIFICATION_FLD = "TechnicalSpecification";
        public const string USERID_FLD = "UserID";
        public const string USINGDATE_FLD = "UsingDate";
    }

    public sealed class FA_FADepreciationDetailTable
    {
        public const string CURRENTDEPRECIATIONVALUE_FLD = "CurrentDepreciationValue";
        public const string FACARDMASTERID_FLD = "FACardMasterID";
        public const string FADEPRECIATIONDETAILID_FLD = "FADepreciationDetailID";
        public const string FADEPRECIATIONMASTERID_FLD = "FADepreciationMasterID";
        public const string LINE_FLD = "Line";
        public const string ORIGINALPRICE_FLD = "OriginalPrice";
        public const string REMAINVALUE_FLD = "RemainValue";
        public const string TABLE_NAME = "FA_FADepreciationDetail";
    }

    public sealed class FA_FADepreciationMasterTable
    {
        public const string CCNID_FLD = "CCNID";
        public const string DEPRECIATIONMETHOD_FLD = "DepreciationMethod";
        public const string FADEPRECIATIONMASTERID_FLD = "FADepreciationMasterID";
        public const string LASTUPDATE_FLD = "LastUpdate";
        public const string PERIODID_FLD = "PeriodID";
        public const string POSTDATE_FLD = "PostDate";
        public const string TABLE_NAME = "FA_FADepreciationMaster";
        public const string TRANSNO_FLD = "TransNo";
        public const string USERID_FLD = "UserID";
    }

    public sealed class FIN_SourceOfCapitalTable
    {
        public const string CODE_FLD = "Code";
        public const string NAME_FLD = "Name";
        public const string SOURCEOFCAPITALID_FLD = "SourceOfCapitalID";
        public const string TABLE_NAME = "FIN_SourceOfCapital";
    }

    public sealed class FA_AdjustDepreciationTable
    {
        public const string ADJUSTDEPRECIATIONID_FLD = "AdjustDepreciationID";
        public const string ADJUSTREASONID_FLD = "AdjustReasonID";
        public const string ADJUSTTYPE_FLD = "AdjustType";
        public const string ADJUSTVALUE_FLD = "AdjustValue";
        public const string CCNID_FLD = "CCNID";
        public const string CODE_FLD = "Code";
        public const string FACARDMASTERID_FLD = "FACardMasterID";
        public const string NOTE_FLD = "Note";
        public const string POSTDATE_FLD = "PostDate";
        public const string TABLE_NAME = "FA_AdjustDepreciation";
        public const string USERID_FLD = "UserID";
    }

    public sealed class FA_AdjustReasonTable
    {
        public const string ADJUSTREASONID_FLD = "AdjustReasonID";
        public const string CODE_FLD = "Code";
        public const string DESCRIPTION_FLD = "Description";
        public const string TABLE_NAME = "FA_AdjustReason";
    }

    public sealed class CST_AllocateSetupDetailTable
    {
        public const string ALLOCATESETUPDETAILID_FLD = "AllocateSetupDetailID";
        public const string ALLOCATESETUPMASTERID_FLD = "AllocateSetupMasterID";
        public const string COMMENT_FLD = "Comment";
        public const string GROUP_FLD = "Group";
        public const string PRODUCTGROUPID_FLD = "ProductGroupID";
        public const string PRODUCTID_FLD = "ProductID";
        public const string RATE_FLD = "Rate";
        public const string TABLE_NAME = "CST_AllocateSetupDetail";
    }

    public sealed class CST_AllocateSetupMasterTable
    {
        public const string ALLOCATESETUPMASTERID_FLD = "AllocateSetupMasterID";
        public const string CCNID_FLD = "CCNID";
        public const string CODE_FLD = "Code";
        public const string COSTACCOUNTID_FLD = "CostAccountID";
        public const string COSTELEMENTID_FLD = "CostElementID";
        public const string FROMDATE_FLD = "FromDate";
        public const string LASTUPDATE_FLD = "LastUpdate";
        public const string MATERIALTYPE_FLD = "MaterialType";
        public const string TABLE_NAME = "CST_AllocateSetupMaster";
        public const string TODATE_FLD = "ToDate";
        public const string USERID_FLD = "UserID";
    }

    public sealed class CST_CostAllocationDetailTable
    {
        public const string ALLOCATEDAMOUNT_FLD = "AllocatedAmount";
        public const string AMOUNT_FLD = "Amount";
        public const string BEGINWIP_FLD = "BeginWIP";
        public const string COSTALLOCATIONDETAILID_FLD = "CostAllocationDetailID";
        public const string COSTALLOCATIONMASTERID_FLD = "CostAllocationMasterID";
        public const string DIFWIP_FLD = "DifWIP";
        public const string ENDWIP_FLD = "EndWIP";
        public const string EXPENSEAMOUNT_FLD = "ExpenseAmount";
        public const string FINISHEDQTY_FLD = "FinishedQty";
        public const string PRODUCTGROUPID_FLD = "ProductGroupID";
        public const string PRODUCTID_FLD = "ProductID";
        public const string RATE_FLD = "Rate";
        public const string RATIO_FLD = "Ratio";
        public const string TABLE_NAME = "CST_CostAllocationDetail";
        public const string TOTALAMOUNT_FLD = "TotalAmount";
    }

    public sealed class CST_CostAllocationMasterTable
    {
        public const string ACTCOSTALLOCATIONMASTERID_FLD = "ActCostAllocationMasterID";
        public const string ALLOCATESETUPMASTERID_FLD = "AllocateSetupMasterID";
        public const string CCNID_FLD = "CCNID";
        public const string COSTALLOCATIONMASTERID_FLD = "CostAllocationMasterID";
        public const string LASTUPDATE_FLD = "LastUpdate";
        public const string TABLE_NAME = "CST_CostAllocationMaster";
        public const string USERID_FLD = "UserID";
    }

    public sealed class ITM_BOMTable
    {
        public const string ALTERNATIVE_FLD = "Alternative";
        public const string ANCESTOR_FLD = "Ancestor";
        public const string BOMID_FLD = "BomID";
        public const string BOMUMID_FLD = "BomUMID";
        public const string COMPONENTID_FLD = "ComponentID";
        public const string EFFECTIVEBEGINDATE_FLD = "EffectiveBeginDate";
        public const string EFFECTIVEBEGINDAY_FLD = "EffectiveBeginDay";
        public const string EFFECTIVEENDDATE_FLD = "EffectiveEndDate";
        public const string EFFECTIVEENDDAY_FLD = "EffectiveEndDay";
        public const string LEADTIMEOFFSET_FLD = "LeadTimeOffset";
        public const string LINE_FLD = "Line";
        public const string PRODUCTID_FLD = "ProductID";
        public const string QTYPERCENT_FLD = "QtyPercent";
        public const string QUANTITY_FLD = "Quantity";
        public const string ROUTINGID_FLD = "RoutingID";
        public const string SHRINK_FLD = "Shrink";
        public const string TABLE_NAME = "ITM_BOM";
        public const string VALUEPERCENT_FLD = "ValuePercent";
    }
    public sealed class IV_ItemSerialTable
    {
        public const string TABLE_NAME = "IV_ItemSerial";
        public const string ITEMSERIALID_FLD = "ItemSerialID";
        public const string LOT_FLD = "Lot";
        public const string QASTATUS_FLD = "QAStatus";
        public const string REFNO_FLD = "RefNo";
        public const string REFLINE_FLD = "RefLine";
        public const string INSPSTATUS_FLD = "InspStatus";
        public const string MRBDISPLAY_FLD = "MRBDisplay";
        public const string REMAIN_FLD = "Remain";
        public const string SERIAL_FLD = "Serial";
        public const string CCNID_FLD = "CCNID";
        public const string PRODUCTID_FLD = "ProductID";
        public const string TRANTYPEID_FLD = "TranTypeID";
        public const string LOCATIONID_FLD = "LocationID";
        public const string BINID_FLD = "BinID";
        public const string MASTERLOCATIONID_FLD = "MasterLocationID";
    }

    public sealed class ITM_ProductTable
    {
        public const string ALLOWNEGATIVEQTY_FLD = "AllowNegativeQty";
        public const string PRODUCTGROUPID_FLD = "ProductGroupID";
        public const string ACADJUSTMENTMASTERID_FLD = "ACAdjustmentMasterID";
        public const string AGCID_FLD = "AGCID";
        public const string AUTOCONVERSION_FLD = "AutoConversion";
        public const string AVEG_FLD = "AVEG";
        public const string BINID_FLD = "BinID";
        public const string BIZSTARTDATE_FLD = "BIZSTARTDATE";
        public const string BOMDESCRIPTION_FLD = "BOMDescription";
        public const string BOMINCREMENT_FLD = "BomIncrement";
        public const string BUYERID_FLD = "BuyerID";
        public const string BUYINGUMID_FLD = "BuyingUMID";
        public const string CATEGORYID_FLD = "CategoryID";
        public const string CCNID_FLD = "CCNID";
        public const string CODE_FLD = "Code";
        public const string COGSACCOUNTID_FLD = "COGSAccountID";
        public const string CONVERSIONTOLERANCE_FLD = "ConversionTolerance";
        public const string COSTCENTERID_FLD = "CostCenterID";
        public const string COSTCENTERRATEMASTERID_FLD = "CostCenterRateMasterID";
        public const string COSTMETHOD_FLD = "CostMethod";
        public const string CREATEDATETIME_FLD = "CreateDateTime";
        public const string DELETEREASONID_FLD = "DeleteReasonID";
        public const string DELIVERYPOLICYID_FLD = "DeliveryPolicyID";
        public const string DESCRIPTION_FLD = "Description";
        public const string DESIGNUMID_FLD = "DesignUMID";
        public const string EXPORTTAX_FLD = "ExportTax";
        public const string FINISHEDGOODS_FLD = "FinishedGoods";
        public const string FORMATCODEID_FLD = "FormatCodeID";
        public const string FREIGHTCLASSID_FLD = "FreightClassID";
        public const string GROUP1_FLD = "Group1";
        public const string GROUP2_FLD = "Group2";
        public const string GROUPQUANTITY_FLD = "GroupQuantity";
        public const string HAZARDID_FLD = "HazardID";
        public const string HEIGHT_FLD = "Height";
        public const string HEIGHTUMID_FLD = "HeightUMID";
        public const string IMPORTTAX_FLD = "ImportTax";
        public const string INVENTORID_FLD = "InventorID";
        public const string INVENTORYACCOUNTID_FLD = "InventoryAccountID";
        public const string ISSUESIZE_FLD = "IssueSize";
        public const string LENGTH_FLD = "Length";
        public const string LENGTHUMID_FLD = "LengthUMID";
        public const string LICENSEFEE_FLD = "LicenseFee";
        public const string LISTPRICE_FLD = "ListPrice";
        public const string LOCATIONID_FLD = "LocationID";
        public const string LOTCONTROL_FLD = "LotControl";
        public const string LOTSIZE_FLD = "LotSize";
        public const string LTDOCKTOSTOCK_FLD = "LTDockToStock";
        public const string LTFIXEDTIME_FLD = "LTFixedTime";
        public const string LTORDERPREPARE_FLD = "LTOrderPrepare";
        public const string LTREQUISITION_FLD = "LTRequisition";
        public const string LTSAFETYSTOCK_FLD = "LTSafetyStock";
        public const string LTSALESATP_FLD = "LTSalesATP";
        public const string LTSHIPPINGPREPARE_FLD = "LTShippingPrepare";
        public const string LTVARIABLETIME_FLD = "LTVariableTime";
        public const string MAKEITEM_FLD = "MakeItem";
        public const string MANUFACTURER_FLD = "Manufacturer";
        public const string MASSORDER_FLD = "MassOrder";
        public const string MASTERLOCATIONID_FLD = "MasterLocationID";
        public const string MAXIMUMSTOCK_FLD = "MaximumStock";
        public const string MAXPRODUCE_FLD = "MaxProduce";
        public const string MAXROUNDUPTOMIN_FLD = "MaxRoundUpToMin";
        public const string MAXROUNDUPTOMULTIPLE_FLD = "MaxRoundUpToMultiple";
        public const string MINIMUMSTOCK_FLD = "MinimumStock";
        public const string MINPRODUCE_FLD = "MinProduce";
        public const string ORDERPOINT_FLD = "OrderPoint";
        public const string ORDERPOLICYID_FLD = "OrderPolicyID";
        public const string ORDERQUANTITY_FLD = "OrderQuantity";
        public const string ORDERQUANTITYMULTIPLE_FLD = "OrderQuantityMultiple";
        public const string ORDERRULEID_FLD = "OrderRuleID";
        public const string OTHERINFO1_FLD = "OtherInfo1";
        public const string OTHERINFO2_FLD = "OtherInfo2";
        public const string PARENTPRODUCTID_FLD = "ParentProductID";
        public const string PARTNAMEVN_FLD = "PartNameVN";
        public const string PARTNUMBER_FLD = "PartNumber";
        public const string PICTURE_FLD = "Picture";
        public const string PLANTYPE_FLD = "PlanType";
        public const string PRIMARYVENDORID_FLD = "PrimaryVendorID";
        public const string PRODUCTID_FLD = "ProductID";
        public const string PRODUCTIONLINEID_FLD = "ProductionLineID";
        public const string PRODUCTTYPEID_FLD = "ProductTypeID";
        public const string QASTATUS_FLD = "QAStatus";
        public const string QUANTITYSET_FLD = "QuantitySet";
        public const string RATE_FLD = "Rate";
        public const string RECEIVETOLERANCE_FLD = "ReceiveTolerance";
        public const string REGISTEREDCODE_FLD = "RegisteredCode";
        public const string REVENUEACCOUNTID_FLD = "RevenueAccountID";
        public const string REVISION_FLD = "Revision";
        public const string ROUTINGDESCRIPTION_FLD = "RoutingDescription";
        public const string ROUTINGINCREMENT_FLD = "RoutingIncrement";
        public const string SAFETYSTOCK_FLD = "SafetyStock";
        public const string SCRAPPERCENT_FLD = "ScrapPercent";
        public const string SELLINGUMID_FLD = "SellingUMID";
        public const string SERIALNUMBER_FLD = "SerialNumber";
        public const string SETUPDATE_FLD = "SetupDate";
        public const string SETUPPAIR_FLD = "SetUpPair";
        public const string SHELFLIFE_FLD = "ShelfLife";
        public const string SHIPTOLERANCEID_FLD = "ShipToleranceID";
        public const string SOURCEID_FLD = "SourceID";
        public const string SPECIALTAX_FLD = "SpecialTax";
        public const string STOCK_FLD = "Stock";
        public const string STOCKTAKINGCODE_FLD = "StockTakingCode";
        public const string STOCKUMID_FLD = "StockUMID";
        public const string TABLE_NAME = "ITM_Product";
        public const string TAXCODE_FLD = "TaxCode";
        public const string TRADEDISCOUNTACCOUNTID_FLD = "TradeDiscountAccountID";
        public const string UPDATEDATETIME_FLD = "UpdateDateTime";
        public const string VAT_FLD = "VAT";
        public const string VENDORCURRENCYID_FLD = "VendorCurrencyID";
        public const string VENDORLOCATIONID_FLD = "VendorLocationID";
        public const string VOUCHERTOLERANCE_FLD = "VoucherTolerance";
        public const string WARRANTY_FLD = "Warranty";
        public const string WEIGHT_FLD = "Weight";
        public const string WEIGHTUMID_FLD = "WeightUMID";
        public const string WIDTH_FLD = "Width";
        public const string WIDTHUMID_FLD = "WidthUMID";
        public const string WORKCENTERID_FLD = "WorkCenterID";
        public const string MARKETING_CATEGORYID_FLD = "Marketing_CategoryID";
    }

    public sealed class ITM_ProductToGroupTable
    {
        public const string PRODUCTGROUPID_FLD = "ProductGroupID";
        public const string PRODUCTID_FLD = "ProductID";
        public const string TABLE_NAME = "ITM_ProductToGroup";
    }

    public sealed class ITM_UnfinishedConversionItemTable
    {
        public const string ACTCOSTALLOCATIONMASTERID_FLD = "ActCostAllocationMasterID";
        public const string COMPLETEDQTY_FLD = "CompletedQty";
        public const string COMPONENTID_FLD = "ComponentID";
        public const string FINISHEDQTY_FLD = "FinishedQty";
        public const string PRODUCTID_FLD = "ProductID";
        public const string QTYPERCENT_FLD = "QtyPercent";
        public const string TABLE_NAME = "ITM_UnfinishedConversionItem";
        public const string UNFINISHEDCONVERSIONITEMID_FLD = "UnfinishedConversionItemID";
    }

    public sealed class v_ProductInGroupTable
    {
        public const string CCNID_FLD = "CCNID";
        public const string CODE_FLD = "Code";
        public const string DESCRIPTION_FLD = "Description";
        public const string PRODUCTGROUPID_FLD = "ProductGroupID";
        public const string PRODUCTID_FLD = "ProductID";
        public const string REVISION_FLD = "Revision";
        public const string TABLE_NAME = "v_ProductInGroup";
    }

    public sealed class CST_DirectCostDetailTable
    {
        public const string AMOUNT_FLD = "Amount";
        public const string DIRECTCOSTDETAILID_FLD = "DirectCostDetailID";
        public const string DIRECTCOSTMASTERID_FLD = "DirectCostMasterID";
        public const string PRODUCTID_FLD = "ProductID";
        public const string TABLE_NAME = "CST_DirectCostDetail";
    }

    public sealed class CST_DirectCostMasterTable
    {
        public const string ACTCOSTALLOCATIONMASTERID_FLD = "ActCostAllocationMasterID";
        public const string CCNID_FLD = "CCNID";
        public const string COSTACCOUNTID_FLD = "CostAccountID";
        public const string COSTELEMENTID_FLD = "CostElementID";
        public const string DIRECTCOSTMASTERID_FLD = "DirectCostMasterID";
        public const string LASTUPDATE_FLD = "LastUpdate";
        public const string TABLE_NAME = "CST_DirectCostMaster";
        public const string USERID_FLD = "UserID";
    }

    public sealed class FIN_ExchangeDifferenceDetailTable
    {
        public const string ACCOUNTID_FLD = "AccountID";
        public const string DIFACCOUNTID_FLD = "DifAccountID";
        public const string DifCode = "DifCode";
        public const string DifName = "DifName";
        public const string EXCHANGEDIFFERENCEDETAILID_FLD = "ExchangeDifferenceDetailID";
        public const string EXCHANGEDIFFERENCEMASTERID_FLD = "ExchangeDifferenceMasterID";
        public const string TABLE_NAME = "FIN_ExchangeDifferenceDetail";
    }

    public sealed class FIN_ExchangeDifferenceMasterTable
    {
        public const string CCNID_FLD = "CCNID";
        public const string CODE_FLD = "Code";
        public const string CURRENCYID_FLD = "CurrencyID";
        public const string DESCRIPTION_FLD = "Description";
        public const string EXCHANGEDIFFERENCEMASTERID_FLD = "ExchangeDifferenceMasterID";
        public const string EXCHANGERATE_FLD = "ExchangeRate";
        public const string LASTUPDATE_FLD = "LastUpdate";
        public const string PERIODID_FLD = "PeriodID";
        public const string TABLE_NAME = "FIN_ExchangeDifferenceMaster";
        public const string USERID_FLD = "UserID";
    }

    //Begin 23-07-2009 -- khanhpq
    public sealed class SO_NegociationTable
    {
        public const string ARRANGEMENT_FLD = "Arrangement";
        public const string ASSESSMENT_FLD = "Assessment";
        public const string BUSINESSMANNAME_FLD = "BusinessmanName";
        public const string CONTENTS_FLD = "Contents";

        public const string FEEDBACKINFOR_FLD = "FeedbackInfor";
        public const string NEGOCIATIONID_FLD = "NegociationID";
        public const string NEGOCIATIONNO_FLD = "NegociationNo";
        public const string NEXTTIME_FLD = "NextTime";
        public const string PARTYID_FLD = "PartyID";
        public const string TABLE_NAME = "SO_Negociation";
        public const string TRANSDATE_FLD = "TransDate";
        public const string UPDATEDATE_FLD = "UpdateDate";
        public const string USERID_FLD = "UserID";
    }

    //End

    //Begin 27-07-2009 -- Khanhpq --
    public sealed class SO_AssignmentMasterTable
    {
        public const string ASSIGNMENTMASTERCODE_FLD = "AssignmentMasterCode";
        public const string ASSIGNMENTMASTERID_FLD = "AssignmentMasterID";
        public const string ASSIGNMENTTYPEID_FLD = "AssignmentTypeID";

        public const string CONTACTORID_FLD = "ContactorID";
        public const string CONTENTS_FLD = "Contents";
        public const string PARTYID_FLD = "PartyID";
        public const string STATUS_FLD = "Status";
        public const string TABLE_NAME = "SO_AssignmentMaster";
        public const string UPDATEDATE_FLD = "UpdateDate";
        public const string USERID_FLD = "UserID";
    }

    public sealed class SO_AssignmentDetailTable
    {
        public const string ASSESSMENT_FLD = "Assessment";
        public const string ASSIGNMENTDETAILID_FLD = "AssignmentDetailID";
        public const string ASSIGNMENTMASTERID_FLD = "AssignmentMasterID";
        public const string COMMENT_FLD = "Comment";
        public const string COMMITDATE_FLD = "CommitDate";
        public const string EMPLOYEEID_FLD = "EmployeeID";
        public const string ENDDATE_FLD = "EndDate";

        public const string RESULTS_FLD = "Results";
        public const string STARTDATE_FLD = "StartDate";
        public const string JOB_FLD = "Job";
        public const string ORTHER_FLD = "Orther";
        public const string TABLE_NAME = "SO_AssignmentDetail";
    }

    public sealed class SO_AssignmentTypeTable
    {
        public const string ASSIGMENTNAME_FLD = "AssigmentName";
        public const string ASSIGNMENTTYPEID_FLD = "AssignmentTypeID";
        public const string TABLE_NAME = "SO_AssignmentType";
    }

    //End

    //Begin 02-08-2009 -- Khanhpq ---
    public sealed class SO_ContactTable
    {
        public const string ADDRESS_FLD = "Address";
        public const string ADDRESSCOMPANY_FLD = "AddressCompany";
        public const string BANK_FLD = "Bank";
        public const string BANKACCOUNT_FLD = "BankAccount";
        public const string BIZCODE_FLD = "BizCode";
        public const string BIZDATE_FLD = "BizDate";
        public const string CODE_FLD = "Code";
        public const string CONTACTID_FLD = "ContactID";
        public const string CUSTITLE_FLD = "CusTitle";
        public const string CUSTOMERCONTACTOR_FLD = "CustomerContactor";
        public const string CUSTOMERNAME_FLD = "CustomerName";
        public const string FROMDATE_FLD = "FromDate";
        public const string IDENTIFYCARD_FLD = "IdentifyCard";
        public const string PHONE_FLD = "Phone";
        public const string RECENTLIVING_FLD = "RecentLiving";
        public const string REPRESENT_FLD = "Represent";
        public const string TABLE_NAME = "SO_Contact";
        public const string TAXCODE_FLD = "TaxCode";
        public const string TITLE_FLD = "Title";
        public const string TODATE_FLD = "ToDate";
        public const string TRANSDATE_FLD = "TransDate";
    }

    //End

    //Begin 04-08-2009 -- Khanhpq --
    public sealed class SO_PartyBeginAssessmentMasterTable
    {
        public const string AGENTTYPEID_FLD = "AgentTypeID";
        public const string CODE_FLD = "Code";
        public const string NUMBEROFMONTH_FLD = "NumberOfMonth";
        public const string PARTYBEGINASSESSMENTMASTERID_FLD = "PartyBeginAssessmentMasterID";
        public const string PARTYID_FLD = "PartyID";
        public const string STARTDATE_FLD = "StartDate";
        public const string TABLE_NAME = "SO_PartyBeginAssessmentMaster";
        public const string USERID_FLD = "UserID";
    }

    public sealed class SO_PartyBeginAssessmentDetailTable
    {
        public const string BALANCEDEBIT_FLD = "BalanceDebit";
        public const string MONTH_FLD = "Month";
        public const string PARTYBEGINASSESSMENTDETAILID_FLD = "PartyBeginAssessmentDetailID";
        public const string PARTYBEGINASSESSMENTMASTERID_FLD = "PartyBeginAssessmentMasterID";
        public const string REVENUE_FLD = "Revenue";
        public const string TABLE_NAME = "SO_PartyBeginAssessmentDetail";
    }

    //End
    // create duynt 19-08-2009
    public sealed class SO_PolicyForTypeAgentTable
    {
        public const string Code_FLD = "Code";
        public const string FromDate_FLD = "FromDate";
        public const string Note_FLD = "Note";
        public const string PercentPolicy_FLD = "PercentPolicy";
        public const string PolicyID_FLD = "PolicyID";
        public const string TABLE_NAME = "SO_PolicyForTypeAgent";
        public const string ToDate_FLD = "ToDate";
        public const string TypePolicy_FLD = "TypePolicy";
    }

    // created duynt 19-08-2009


    //Begin 14-08-2009 -- Khanhpq--
    public sealed class SO_DistributionPlanningTable
    {
        public const string Amount_FLD = "Amount";
        public const string Distribution_Code_FLD = "Distribution_Code";
        public const string Distribution_ID_FLD = "Distribution_ID";
        public const string Distribution_Type_ID_FLD = "Distribution_Type_ID";
        public const string End_Date_FLD = "End_Date";
        public const string Plan_Qty_FLD = "Plan_Qty";
        public const string Previous_Qty_FLD = "Previous_Qty";
        public const string Price_FLD = "Price";
        public const string Process1_FLD = "Process1";
        public const string Process2_FLD = "Process2";
        public const string Process3_FLD = "Process3";

        public const string Product_Name_FLD = "Product_Name";
        public const string ProductID_FLD = "ProductID";
        public const string Start_Date_FLD = "Start_Date";
        public const string TABLE_NAME = "SO_DistributionPlanning";
        public const string Trans_Date_FLD = "Trans_Date";
        public const string User_ID_FLD = "User_ID";
    }

    //End

    //Begin 19-08-2009 --- khanhpq ---
    public sealed class SO_PartyAssessmentMasterTable
    {
        public const string CODE_FLD = "Code";

        public const string ENDDATE_FLD = "EndDate";
        public const string PARTYASSESSMENTMASTERID_FLD = "PartyAssessmentMasterID";
        public const string STARTDATE_FLD = "StartDate";
        public const string TABLE_NAME = "SO_PartyAssessmentMaster";
        public const string USERID_FLD = "UserID";
    }

    public sealed class SO_PartyAssessmentDetailTable
    {
        public const string BALANCEDEBIT_FLD = "BalanceDebit";
        public const string COMMENT_FLD = "Comment";

        public const string DEBITPERREVENUE_FLD = "DebitPerRevenue";
        public const string EFFECT_MA_KH_FLD = "EFFECT_MA_KH";
        public const string EFFECT_TEN_FLD = "EFFECT_Ten";
        public const string LEVEL_FLD = "Level";
        public const string PARTYASSESSMENTDETAILID_FLD = "PartyAssessmentDetailID";

        public const string PARTYASSESSMENTMASTERID_FLD = "PartyAssessmentMasterID";
        public const string REVENUE_FLD = "Revenue";
        public const string TABLE_NAME = "SO_PartyAssessmentDetail";
    }

    //ENd

    //Begin 22 - 08-2009 -- Khanhpq--
    public sealed class SO_DeliveryTable
    {
        public const string COMMENT_FLD = "Comment";
        public const string DATE_FLD = "Date";
        public const string PARTYID_FLD = "PartyID";
        public const string PRODUCTID_FLD = "ProductID";
        public const string QTY_FLD = "Qty";
        public const string SALEORDERDETAILID_FLD = "SaleOrderDetailID";
        public const string SO_DELIVERYID_FLD = "SO_DeliveryID";
        public const string TABLE_NAME = "SO_Delivery";
        public const string USERID_FLD = "UserID";
    }

    //End
    public sealed class SO_Effect_InvoiceTable
    {
        public const string ID_FLD = "ID";
        public const string SOHD_FLD = "SOHD";
        public const string NGAY_GIAO_FLD = "NGAY_GIAO";
        public const string SALEORDERMASTERID_FLD = "SaleOrderMasterID";
        public const string TABLE_NAME = "SO_Effect_Invoice";
    }
    #endregion
}