-- Script-02.sql 
-- Created by: HungLA

---------------------------------RELEASE PART----------------------------------------------

IF NOT EXISTS (SELECT ParamID FROM sys_Param WHERE Name = 'DBVersion')
 INSERT INTO sys_Param (Name, Value) VALUES('DBVersion', '28-Sep-05')
UPDATE sys_Param SET Value = '08-July-08' WHERE Name = 'DBVersion'
go

---------------------------------TABLE PART----------------------------------------------

-- create table DCP_BeginQuantity
IF NOT EXISTS(select id from dbo.sysobjects where id = object_id(N'DCP_BeginQuantity') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
Begin
CREATE TABLE [DCP_BeginQuantity] (
	[DCPBeginQuantityID] [int] IDENTITY (1, 1) NOT NULL ,
	[DCOptionMasterID] [int] NOT NULL ,
	[ProductID] [int] NOT NULL ,
	[Quantity] [NumberDec] NULL ,
	CONSTRAINT [PK_DCP_BeginQuantity] PRIMARY KEY  CLUSTERED 
	(
		[DCPBeginQuantityID]
	)  ON [PRIMARY] ,
	CONSTRAINT [FK_DCP_BeginQuantity_ITM_Product] FOREIGN KEY 
	(
		[ProductID]
	) REFERENCES [ITM_Product] (
		[ProductID]
	),
	CONSTRAINT [FK_DCP_BeginQuantity_PRO_DCOptionMaster] FOREIGN KEY 
	(
		[DCOptionMasterID]
	) REFERENCES [PRO_DCOptionMaster] (
		[DCOptionMasterID]
	)
) ON [PRIMARY]
End
GO

-- create table Sys_PostdateConfiguration
IF NOT EXISTS(select id from dbo.sysobjects where id = object_id(N'Sys_PostdateConfiguration') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
Begin
CREATE TABLE [Sys_PostdateConfiguration] (
	[PostdateConfigurationID] [int] IDENTITY (1,1) NOT NULL ,
	[DayBefore] [int] NOT NULL ,
	[LastUpdated] [datetime] NULL ,
	[Username] [nvarchar] (50),
	CONSTRAINT [PK_PostdateConfig] PRIMARY KEY  CLUSTERED 
	(
		[PostdateConfigurationID]
	)
)
End
GO

-- create table for print sale invoice
IF NOT EXISTS(select id from dbo.sysobjects where id = object_id(N'SO_InvoiceMaster') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
Begin
CREATE TABLE [SO_InvoiceMaster] (
	[InvoiceMasterID] [int] IDENTITY (1, 1) NOT NULL ,
	[ConfirmShipNo] [nvarchar] (20) NOT NULL ,
	[ShippedDate] [datetime] NOT NULL ,
	[SaleOrderMasterID] [int] NOT NULL ,
	[MasterLocationID] [int] NOT NULL ,
	[CCNID] [int] NOT NULL ,
	[UserName] [nvarchar] (40) NULL ,
	[LastChange] [datetime] NULL ,
	[ExchangeRate] [NumberDec] NOT NULL ,
	[CurrencyID] [int] NOT NULL ,
	[GateID] [int] NULL ,
	[Comment] [nvarchar] (100) NULL ,
	[FromPort] [nvarchar] (100) NULL ,
	[CNo] [nvarchar] (30) NULL ,
	[Measurement] [NumberDec] NULL ,
	[GrossWeight] [NumberDec] NULL ,
	[NetWeight] [NumberDec] NULL ,
	[IssuingBank] [nvarchar] (80) NULL ,
	[LCDate] [datetime] NULL ,
	[LCNo] [nvarchar] (30) NULL ,
	[VesselName] [nvarchar] (30) NULL ,
	[ShipCode] [nvarchar] (20) NOT NULL ,
	[OnBoardDate] [datetime] NULL ,
	[ReferenceNo] [nvarchar] (60) NULL ,
	[InvoiceNo] [nvarchar] (40) NULL ,
	[InvoiceDate] [datetime] NOT NULL ,
	 PRIMARY KEY  CLUSTERED 	(		[InvoiceMasterID]	)  ON [PRIMARY] ,
	 FOREIGN KEY 	(		[CCNID]	) REFERENCES [MST_CCN] (		[CCNID]	),
	 FOREIGN KEY 	(		[CurrencyID]	) REFERENCES [MST_Currency] (		[CurrencyID]	),
	 FOREIGN KEY 	(		[GateID]	) REFERENCES [SO_Gate] (		[GateID]	),
	 FOREIGN KEY 	(		[MasterLocationID]	) REFERENCES [MST_MasterLocation] (		[MasterLocationID]	),
	 FOREIGN KEY 	(		[SaleOrderMasterID]	) REFERENCES [SO_SaleOrderMaster] (		[SaleOrderMasterID]	)
)
CREATE UNIQUE INDEX XAK1SO_InvoiceMaster ON SO_InvoiceMaster
(
       ConfirmShipNo              ASC
)

CREATE TABLE [SO_InvoiceDetail] (
	[InvoiceDetailID] [int] IDENTITY (1, 1) NOT NULL ,
	[InvoiceMasterID] [int] NOT NULL ,
	[SaleOrderDetailID] [int] NOT NULL ,
	[DeliveryScheduleID] [int] NOT NULL ,
	[ProductID] [int] NOT NULL ,
	[Price] [NumberDec] NOT NULL ,
	[InvoiceQty] [NumberDec] NULL ,
	[VATPercent] [NumberDec] NULL ,
	[VATAmount] [NumberDec] NULL ,
	 PRIMARY KEY  CLUSTERED 	(		[InvoiceDetailID]	)  ON [PRIMARY] ,
	 FOREIGN KEY 	(		[InvoiceMasterID]	) REFERENCES [SO_InvoiceMaster] (		[InvoiceMasterID]	),
	 FOREIGN KEY 	(		[DeliveryScheduleID]	) REFERENCES [SO_DeliverySchedule] (		[DeliveryScheduleID]	),
	 FOREIGN KEY 	(		[ProductID]	) REFERENCES [ITM_Product] (		[ProductID]	),
	 FOREIGN KEY 	(		[SaleOrderDetailID]	) REFERENCES [SO_SaleOrderDetail] (		[SaleOrderDetailID]	)
)
END
GO

-- add some fields into PO_DeliverySchedule
IF NOT EXISTS (SELECT id FROM syscolumns WHERE object_name(id) = 'PO_DeliverySchedule'  AND name = 'StartDate')
Begin
	ALTER TABLE PO_DeliverySchedule ADD StartDate         datetime NULL
End
go
-- add some fields into ITM_Product
IF NOT EXISTS (SELECT id FROM syscolumns WHERE object_name(id) = 'ITM_Product'  AND name = 'SetUpPair')
Begin
	ALTER TABLE ITM_Product ADD SetUpPair         nvarchar(50) NULL
End
go
-- add some fields into PO_PurchaseOrderReceiptDetail
IF NOT EXISTS (SELECT id FROM syscolumns WHERE object_name(id) = 'PO_PurchaseOrderReceiptDetail'  AND name = 'InvoiceDetailID')
Begin
	ALTER TABLE PO_PurchaseOrderReceiptDetail ADD InvoiceDetailID         int NULL
	ALTER TABLE PO_PurchaseOrderReceiptDetail
	       ADD FOREIGN KEY (InvoiceDetailID)
	                             REFERENCES PO_InvoiceDetail
End
go
-- add some fields into cst_FreightDetail
IF NOT EXISTS (SELECT id FROM syscolumns WHERE object_name(id) = 'cst_FreightDetail'  AND name = 'InvoiceMasterID')
Begin
	ALTER TABLE cst_FreightDetail ADD InvoiceMasterID         int NULL
	ALTER TABLE cst_FreightDetail
	       ADD FOREIGN KEY (InvoiceMasterID)
	                             REFERENCES PO_InvoiceMaster
End
go

-- new begin cost for DCP table
IF NOT EXISTS(select id from dbo.sysobjects where id = object_id(N'IV_BeginDCPReport') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
Begin
	CREATE TABLE IV_BeginDCPReport (
	       BeginDCPReportID        bigint IDENTITY,
	       ProductID            int NOT NULL,
	       LastUpdate           datetime NULL,
	       EffectDate           datetime NOT NULL,
	       Quantity             NumberDec NULL,
	       Username           nvarchar(200) NULL
	)

	CREATE UNIQUE INDEX XAK1IV_BeginDCPReport ON IV_BeginDCPReport
	(
	       ProductID              ASC,
	       EffectDate             ASC
	)
	
	ALTER TABLE IV_BeginDCPReport
	       ADD PRIMARY KEY (BeginDCPReportID)
	
	ALTER TABLE IV_BeginDCPReport
	       ADD FOREIGN KEY (ProductID)
	                             REFERENCES ITM_Product
End
go

-- add some fields into CST_ActualCostHistory
IF NOT EXISTS (SELECT id FROM syscolumns WHERE object_name(id) = 'CST_ActualCostHistory'  AND name = 'BeginCost')
Begin
	ALTER TABLE CST_ActualCostHistory ADD BeginCost         NumberDec NULL
End
go
-- increase size of Command file in Sys_Report
IF EXISTS (SELECT id FROM syscolumns WHERE object_name(id) = 'Sys_Report'  AND name = 'Command')
Begin
	ALTER TABLE Sys_Report ALTER COLUMN Command         nvarchar(4000)
End
go

-- add some fields into PRO_ComponentScrapMaster
IF NOT EXISTS (SELECT id FROM syscolumns WHERE object_name(id) = 'PRO_ComponentScrapMaster'  AND name = 'ProductionLineID')
Begin
	ALTER TABLE PRO_ComponentScrapMaster ADD ProductionLineID         int NULL
	ALTER TABLE PRO_ComponentScrapMaster
	       ADD FOREIGN KEY (ProductionLineID)
	                             REFERENCES PRO_ProductionLine
End
go
-- add some fields into CST_ActualCostHistory
IF NOT EXISTS (SELECT id FROM syscolumns WHERE object_name(id) = 'CST_ActualCostHistory'  AND name = 'DSAmount')
Begin
	ALTER TABLE CST_ActualCostHistory ADD DS_OKAmount         NumberDec NULL,
										  DSAmount	NumberDec NULL,
										  AdjustAmount 	NumberDec NULL
End
go
-- CST_DSAndRecycleAllocation
IF NOT EXISTS(select id from dbo.sysobjects where id = object_id(N'CST_DSAndRecycleAllocation') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
Begin
	CREATE TABLE CST_DSAndRecycleAllocation (
	       DSADAllocactionID    int IDENTITY,
	       DSRate               NumberDec NULL,
	       DSAmount             NumberDec NULL,
	       RecycleRate          NumberDec NULL,
	       RecycleAmount        NumberDec NULL,
	       ShippingQty          NumberDec NULL,
	       ReturnGoodsReceiptQty NumberDec NULL,
	       ActCostAllocationMasterID            int NOT NULL,
	       ProductID            int NOT NULL,
	       CostElementID            int NOT NULL
	)
		
	CREATE UNIQUE INDEX XAK1CST_DSAndRecycleAllocation ON CST_DSAndRecycleAllocation
	(
	       ActCostAllocationMasterID              ASC,
	       ProductID                         ASC,
	       CostElementID                       ASC
	)
		
	ALTER TABLE CST_DSAndRecycleAllocation
	       ADD PRIMARY KEY (DSADAllocactionID)
	ALTER TABLE CST_DSAndRecycleAllocation
	       ADD FOREIGN KEY (ActCostAllocationMasterID)
	                             REFERENCES cst_ActCostAllocationMaster
	ALTER TABLE CST_DSAndRecycleAllocation
	       ADD FOREIGN KEY (ProductID)
	                             REFERENCES ITM_Product
	ALTER TABLE CST_DSAndRecycleAllocation
	       ADD FOREIGN KEY (CostElementID)
	                             REFERENCES STD_CostElement
End
go

-- add some fields into CST_DSAndRecycleAllocation
IF NOT EXISTS (SELECT id FROM syscolumns WHERE object_name(id) = 'CST_DSAndRecycleAllocation'  AND name = 'AdjustAmount')
Begin
	ALTER TABLE CST_DSAndRecycleAllocation ADD AdjustAmount         NumberDec NULL
End
go
-- remove recycle rate from CST_DSAndRecycleAllocation
IF EXISTS (SELECT id FROM syscolumns WHERE object_name(id) = 'CST_DSAndRecycleAllocation'  AND name = 'RecycleRate')
Begin
	ALTER TABLE CST_DSAndRecycleAllocation DROP COLUMN RecycleRate
End
go
-- add some fields into CST_DSAndRecycleAllocation
IF NOT EXISTS (SELECT id FROM syscolumns WHERE object_name(id) = 'CST_DSAndRecycleAllocation'  AND name = 'OH_RecycleAmount')
Begin
	ALTER TABLE CST_DSAndRecycleAllocation ADD OH_RecycleAmount         NumberDec NULL,
										  OH_DSAmount         NumberDec NULL,
										  OH_AdjustAmount         NumberDec NULL
End
go

-- add some fields into CST_ActualCostHistory
IF NOT EXISTS (SELECT id FROM syscolumns WHERE object_name(id) = 'CST_ActualCostHistory'  AND name = 'WOCompletionQty')
Begin
	ALTER TABLE CST_ActualCostHistory ADD WOCompletionQty         NumberDec NULL,
										  TransactionAmount	NumberDec NULL,
										  RecycleAmount 	NumberDec NULL,
										  RecoverableAmount 	NumberDec NULL,
										  FreightAmount	NumberDec NULL
End
go
-- add ComBeginCost into CST_ActualCostHistory
IF NOT EXISTS (SELECT id FROM syscolumns WHERE object_name(id) = 'CST_ActualCostHistory'  AND name = 'ComBeginCost')
Begin
	ALTER TABLE CST_ActualCostHistory ADD ComBeginCost         NumberDec NULL
End
go

-- Add some fields into PRO_ComponentScrapDetail
IF NOT EXISTS (SELECT id FROM syscolumns WHERE object_name(id) = 'PRO_ComponentScrapDetail'  AND name = 'FromLocationID')
Begin
	ALTER TABLE PRO_ComponentScrapDetail ADD AvailableQuantity         NumberDec NULL,
										 FromLocationID int NULL,
										 FromBinID int NULL,
										 ToLocationID int NULL,
										 ToBinID int NULL
	ALTER TABLE PRO_ComponentScrapDetail
	       ADD FOREIGN KEY (FromLocationID)
	                             REFERENCES MST_Location
	ALTER TABLE PRO_ComponentScrapDetail
	       ADD FOREIGN KEY (FromBinID)
	                             REFERENCES MST_Bin
	ALTER TABLE PRO_ComponentScrapDetail
	       ADD FOREIGN KEY (ToLocationID)
	                             REFERENCES MST_Location
	ALTER TABLE PRO_ComponentScrapDetail
	       ADD FOREIGN KEY (ToBinID)
	                             REFERENCES MST_Bin
End
go
-- alter some fields in PRO_ComponentScrapDetail to not null
IF EXISTS (SELECT id FROM syscolumns WHERE object_name(id) = 'PRO_ComponentScrapDetail'  AND name = 'FromLocationID' AND isnullable = 1)
Begin
--	UPDATE PRO_ComponentScrapDetail SET FromLocationID = SELECT TOP 1 LocationID FROM MST_Location
	ALTER TABLE PRO_ComponentScrapDetail ALTER COLUMN FromLocationID int NOT NULL
	ALTER TABLE PRO_ComponentScrapDetail ALTER COLUMN FromBinID int NOT NULL
	ALTER TABLE PRO_ComponentScrapDetail ALTER COLUMN ToLocationID int NOT NULL
	ALTER TABLE PRO_ComponentScrapDetail ALTER COLUMN ToBinID int NOT NULL
End
go

-- Add UseCacheAsBegin into PRO_DCOptionMaster
IF NOT EXISTS (SELECT id FROM syscolumns WHERE object_name(id) = 'PRO_DCOptionMaster'  AND name = 'UseCacheAsBegin')
Begin
	ALTER TABLE PRO_DCOptionMaster ADD UseCacheAsBegin         bit NULL
End
go

-- IV_BeginStockForDCPReport
IF NOT EXISTS(select id from dbo.sysobjects where id = object_id(N'IV_BeginStockForDCPReport') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
Begin
	CREATE TABLE IV_BeginStockForDCPReport (
	       BeginStockForDCPReportID        int IDENTITY,
	       ProductID            int NOT NULL,
	       LastUpdate            datetime NULL,
	       January             NumberDec NULL,
	       Febuary             NumberDec NULL,
	       March	           NumberDec NULL,
	       April    	         NumberDec NULL,
	       May             NumberDec NULL,
	       June             NumberDec NULL,
	       July             NumberDec NULL,
	       August             NumberDec NULL,
	       September             NumberDec NULL,
	       October             NumberDec NULL,
	       November             NumberDec NULL,
	       December             NumberDec NULL
	)
	
	ALTER TABLE IV_BeginStockForDCPReport
	       ADD PRIMARY KEY (BeginStockForDCPReportID)
	
	ALTER TABLE IV_BeginStockForDCPReport
	       ADD FOREIGN KEY (ProductID)
	                             REFERENCES ITM_Product
End
go

-- rename column in IV_BeginStockForDCPReport
IF EXISTS (SELECT id FROM syscolumns WHERE object_name(id) = 'IV_BeginStockForDCPReport'  AND name = 'Septemper')
Begin
	ALTER TABLE IV_BeginStockForDCPReport DROP COLUMN Septemper
	ALTER TABLE IV_BeginStockForDCPReport ADD September NumberDec NULL
End
go
-- add LastUpdate field
IF NOT EXISTS (SELECT id FROM syscolumns WHERE object_name(id) = 'IV_BeginStockForDCPReport'  AND name = 'LastUpdate')
Begin
	ALTER TABLE IV_BeginStockForDCPReport ADD LastUpdate datetime NULL
End
go

-- Add InvoiceDate into SO_ConfirmShipMaster
IF NOT EXISTS (SELECT id FROM syscolumns WHERE object_name(id) = 'SO_ConfirmShipMaster'  AND name = 'InvoiceDate')
Begin
	ALTER TABLE SO_ConfirmShipMaster ADD InvoiceDate         datetime NULL	
End
go
-- alter InvoiceDate to Not Null
IF EXISTS (SELECT id FROM syscolumns WHERE object_name(id) = 'SO_ConfirmShipMaster'  AND name = 'InvoiceDate' AND isnullable = 1)
Begin
	UPDATE SO_ConfirmShipMaster SET InvoiceDate = GETDATE()
	ALTER TABLE SO_ConfirmShipMaster ALTER COLUMN InvoiceDate datetime NOT NULL
End
go

-- Add ComponentDSAmount into CST_ActualCostHistory
IF NOT EXISTS (SELECT id FROM syscolumns WHERE object_name(id) = 'CST_ActualCostHistory'  AND name = 'ComponentDSAmount')
Begin
 ALTER TABLE CST_ActualCostHistory ADD ComponentDSAmount         decimal(20,5) NULL
End
go

-- Add ComponentValue into CST_ActualCostHistory
IF NOT EXISTS (SELECT id FROM syscolumns WHERE object_name(id) = 'CST_ActualCostHistory'  AND name = 'ComponentValue')
Begin
 ALTER TABLE CST_ActualCostHistory ADD ComponentValue         decimal(20,5) NULL
End
go

-- IV_StockTakingPeriod
IF NOT EXISTS(select id from dbo.sysobjects where id = object_id(N'IV_StockTakingPeriod') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
Begin
	CREATE TABLE IV_StockTakingPeriod (
	       StockTakingPeriodID  int IDENTITY,
	       Description          T_Description NOT NULL,
	       StockTakingDate      datetime NULL,
	       FromDate             datetime NOT NULL,
	       ToDate               datetime NOT NULL,
	       CCNID                int NOT NULL
	)
	
	ALTER TABLE IV_StockTakingPeriod
	       ADD PRIMARY KEY (StockTakingPeriodID)
End
go

-- IV_CoutingMethod
IF NOT EXISTS(select id from dbo.sysobjects where id = object_id(N'IV_CoutingMethod') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
Begin
	CREATE TABLE IV_CoutingMethod (
	       CountingMethodID     int IDENTITY,
	       Code                 T_Code,
	       Description          T_Description
	)
	
	ALTER TABLE IV_CoutingMethod
	       ADD PRIMARY KEY (CountingMethodID)
End
go

-- Add 3 fields into MTR_MRPCycleOptionMaster
IF NOT EXISTS (SELECT id FROM syscolumns WHERE object_name(id) = 'MTR_MRPCycleOptionMaster'  AND name = 'IncludedRemainPO')
Begin
 ALTER TABLE MTR_MRPCycleOptionMaster ADD IncludedRemainPO         bit NULL,
       DaysBeforeAsOfDate           int NULL,
       IncludedReturnToVendor          bit NULL
End
go

-- Add Quantity column into CST_ActualCostHistory
IF NOT EXISTS (SELECT id FROM syscolumns WHERE object_name(id) = 'CST_ActualCostHistory' AND name = 'Quantity')
 ALTER TABLE CST_ActualCostHistory ADD Quantity NumberDec NULL
go

-- Widen Name in sys_ReportParam
IF NOT EXISTS (SELECT id FROM syscolumns WHERE object_name(id) = 'sys_ReportPara' AND name = 'ParaName' AND length = 80)
 ALTER TABLE sys_ReportPara ALTER COLUMN ParaName nvarchar(40) NOT NULL
go

-- Move Oursourcing to MiscIssue
IF NOT EXISTS (SELECT * FROM PRO_IssuePurpose WHERE IssuePurposeID=7 AND TranTypeID = 24)
 UPDATE PRO_IssuePurpose Set TranTypeID = 24 where IssuePurposeID=7
go


-- enm_POReceiptType table
IF NOT EXISTS(select id from dbo.sysobjects where id = object_id(N'enm_POReceiptType') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
Begin
 CREATE TABLE enm_POReceiptType (
       POReceiptTypeID      int IDENTITY,
       POReceiptTypeCode    tinyint NOT NULL,
       Description          nvarchar(100) NOT NULL
 )

 CREATE UNIQUE INDEX XAK1enm_POReceiptType ON enm_POReceiptType
 (
       POReceiptTypeCode              ASC
 )

 ALTER TABLE enm_POReceiptType
       ADD PRIMARY KEY (POReceiptTypeID)
 
End
go


-- Set PlanningPeriod and Version is Unique 
IF NOT EXISTS (SELECT * FROM sysindexes WHERE Name = 'XAK2PRO_DCOptionMaster')
Begin
 CREATE UNIQUE INDEX XAK2PRO_DCOptionMaster ON PRO_DCOptionMaster
 (
       PlanningPeriod                 ASC,
       Version                        ASC
 )
End
go


-- enm_CostMethod
IF NOT EXISTS(select id from dbo.sysobjects where id = object_id(N'enm_CostMethod') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
Begin
 CREATE TABLE enm_CostMethod (
       CostMethodID         tinyint IDENTITY,
       Code                 tinyint NOT NULL,
       Description          nvarchar(40) NOT NULL
 )

 CREATE UNIQUE INDEX XAK1enm_CostMethod ON enm_CostMethod
 (
       Code                           ASC
 )

 ALTER TABLE enm_CostMethod
       ADD PRIMARY KEY (CostMethodID)
End
go

-- enm_WOLineStatus
IF NOT EXISTS(select id from dbo.sysobjects where id = object_id(N'enm_WOLineStatus') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
Begin
 CREATE TABLE enm_WOLineStatus (
       WOLineStatusID       tinyint IDENTITY,
       Code                 tinyint NOT NULL,
       Description          nvarchar(30) NOT NULL
 )

 CREATE UNIQUE INDEX XAK1enm_WOLineStatus ON enm_WOLineStatus
 (
       Code                           ASC
 )

 CREATE UNIQUE INDEX XAK2enm_WOLineStatus ON enm_WOLineStatus
 (
       Description                    ASC
 )

 ALTER TABLE enm_WOLineStatus
       ADD PRIMARY KEY (WOLineStatusID)
End
go

-- RoundUpDaysException
IF NOT EXISTS (SELECT id FROM syscolumns WHERE object_name(id) = 'PRO_ProductionLine' AND name = 'RoundUpDaysException')
Begin
 ALTER TABLE PRO_ProductionLine ADD RoundUpDaysException int NULL
 exec sp_bindefault One, 'PRO_ProductionLine.RoundUpDaysException'
End
go

-- Set default RoundUpDaysException to 1
IF EXISTS (SELECT id FROM syscolumns WHERE object_name(id) = 'PRO_ProductionLine' AND name = 'RoundUpDaysException')
 UPDATE PRO_ProductionLine SET RoundUpDaysException = 1 WHERE RoundUpDaysException IS NULL
go

-- Add AvailableQty
IF NOT EXISTS (SELECT id FROM syscolumns WHERE object_name(id) = 'CST_RecoverMaterialMaster' AND name = 'AvailableQty')
 ALTER TABLE CST_RecoverMaterialMaster ADD AvailableQty NumberDec NULL
go

-- Add AvailableQty into IV_MiscellaneousIssueDetail
IF NOT EXISTS (SELECT id FROM syscolumns WHERE object_name(id) = 'IV_MiscellaneousIssueDetail' AND name = 'AvailableQty')
 ALTER TABLE IV_MiscellaneousIssueDetail ADD AvailableQty NumberDec NULL
go


-- Add AvailableQty
IF NOT EXISTS (SELECT id FROM syscolumns WHERE object_name(id) = 'IV_Adjustment' AND name = 'AvailableQty')
 ALTER TABLE IV_Adjustment ADD AvailableQty NumberDec NULL
go


-- Add some fields into Sys_TableField
IF NOT EXISTS (SELECT id FROM syscolumns WHERE object_name(id) = 'Sys_TableField'  AND name = 'AlignField3')
Begin
 ALTER TABLE Sys_TableField ADD FilterField3         nvarchar(50) NULL,
       CaseField1           nvarchar(50) NULL,
       AlignField1          nvarchar(50) NULL,
       WidthField1          nvarchar(50) NULL,
       FormatField1         nvarchar(50) NULL,
       CaseField2           nvarchar(50) NULL,
       AlignField2          nvarchar(50) NULL,
       WidthField2          nvarchar(50) NULL,
       FormatField2         nvarchar(50) NULL,
       CaseField3           nvarchar(50) NULL,
       AlignField3          nvarchar(50) NULL,
       WidthField3          nvarchar(50) NULL,
       FormatField3         nvarchar(50) NULL
End
go


-- Add BelanceQty into SO_ReturnedGoodsDetail 
IF NOT EXISTS (SELECT id FROM syscolumns WHERE object_name(id) = 'SO_ReturnedGoodsDetail'  AND name = 'BalanceQty')
Begin
 ALTER TABLE SO_ReturnedGoodsDetail ADD BalanceQty NumberDec NULL
End 
go

IF NOT EXISTS (SELECT id FROM syscolumns WHERE object_name(id) = 'Sys_TableField'  AND name = 'Field1CaptionEN')
Begin
 ALTER TABLE Sys_TableField ALTER COLUMN AlignField1 tinyint NULL
 ALTER TABLE Sys_TableField ALTER COLUMN AlignField2 tinyint NULL
 ALTER TABLE Sys_TableField ALTER COLUMN AlignField3 tinyint NULL
 ALTER TABLE Sys_TableField ALTER COLUMN WidthField1 smallint NULL
 ALTER TABLE Sys_TableField ALTER COLUMN WidthField2 smallint NULL
 ALTER TABLE Sys_TableField ALTER COLUMN WidthField3 smallint NULL
 ALTER TABLE Sys_TableField DROP COLUMN CaseField3
 ALTER TABLE Sys_TableField DROP COLUMN CaseField2
 ALTER TABLE Sys_TableField DROP COLUMN CaseField1
 ALTER TABLE Sys_TableField ADD Field1CaptionVN nvarchar(50) NULL,
				Field1CaptionEN nvarchar(50) NULL,
				Field1CaptionJP nvarchar(50) NULL,
				Field2CaptionVN nvarchar(50) NULL,
				Field2CaptionEN nvarchar(50) NULL,
				Field2CaptionJP nvarchar(50) NULL,
				Field3CaptionVN nvarchar(50) NULL,
				Field3CaptionEN nvarchar(50) NULL,
				Field3CaptionJP nvarchar(50) NULL
End
go

IF EXISTS (SELECT c.name columnName FROM syscolumns c, systypes t
		WHERE object_name(c.id) = 'PO_PurchaseOrderMaster' AND c.name = 'PORevision' 
		AND c.xtype = t.xtype and t.Name = 'nvarchar')
Begin
 ALTER TABLE PO_PurchaseOrderMaster ALTER COLUMN PORevision tinyint NULL
End
go

-- SET PORevison is not null
IF EXISTS (SELECT PurchaseOrderMasterID FROM PO_PurchaseOrderMaster WHERE PORevision is null or PORevision = 0)
 UPDATE PO_PurchaseOrderMaster SET PORevision = 0 WHERE PORevision is null or PORevision = 0
go


-- Add Picture into ITM_Product
IF NOT EXISTS (SELECT c.name columnName FROM syscolumns c WHERE object_name(c.id) = 'ITM_Product' AND c.name = 'Picture')
Begin
 ALTER TABLE ITM_Product ADD Picture image NULL
End
go

-- Add Picture into ITM_Category
IF NOT EXISTS (SELECT c.name columnName FROM syscolumns c WHERE object_name(c.id) = 'ITM_Category' AND c.name = 'Picture')
Begin
 ALTER TABLE ITM_Category ADD Picture image NULL
End
go

-- Create Unique for Routing
IF NOT EXISTS (SELECT * from sysindexes WHERE Name = 'XAK2ITM_Routing')
Begin
 CREATE UNIQUE INDEX XAK2ITM_Routing ON ITM_Routing
 (
       ProductID                      ASC,
       WorkCenterID                   ASC
 )
End
go


-- SO_ConfirmShipMaster
IF NOT EXISTS (SELECT c.name columnName FROM syscolumns c WHERE object_name(c.id) = 'SO_ConfirmShipMaster' AND c.name = 'ExchangeRate')
 Begin
  ALTER TABLE SO_ConfirmShipMaster ADD ExchangeRate NumberDec NULL
  ALTER TABLE SO_ConfirmShipMaster ADD CurrencyID int NULL

  ALTER TABLE SO_ConfirmShipMaster
       ADD FOREIGN KEY (CurrencyID)
                            REFERENCES MST_Currency
 End
go

-- Set NOT NULL
IF EXISTS (SELECT ExchangeRate FROM SO_ConfirmShipMaster WHERE ExchangeRate IS NULL)
Begin
  UPDATE SO_ConfirmShipMaster SET CurrencyID = 22
  UPDATE SO_ConfirmShipMaster SET ExchangeRate = 1

  ALTER TABLE SO_ConfirmShipMaster ALTER COLUMN ExchangeRate NumberDec NOT NULL
  ALTER TABLE SO_ConfirmShipMaster ALTER COLUMN CurrencyID int NOT NULL
End
go


-- Set LocationType not null
IF EXISTS (SELECT * FROM MST_Location WHERE LocationTypeID is NULL)
Begin
 UPDATE MST_Location SET LocationTypeID = 2 where code = 'P2-CK-FP'
 UPDATE MST_Location SET LocationTypeID = 1 where LocationTypeID is NULL
 ALTER TABLE MST_Location ALTER COLUMN LocationTypeID int NOT NULL
End
go



-- Add DCOptionMasterID into PRO_WorkOrderMaster
IF NOT EXISTS (SELECT c.name columnName FROM syscolumns c WHERE object_name(c.id) = 'PRO_WorkOrderMaster' AND c.name = 'DCOptionMasterID')
Begin
 ALTER TABLE PRO_WorkOrderMaster ADD DCOptionMasterID int NULL
End
go


-- Expand Safety Stock
IF EXISTS (SELECT *  FROM syscolumns c
		WHERE object_name(c.id) = 'ITM_Product' AND c.name = 'SafetyStock' AND Prec = 10)
Begin
 ALTER TABLE ITM_Product ALTER COLUMN SafetyStock NumberDec NULL
End
go

-- cst_RecoverMaterialMaster
IF NOT EXISTS (SELECT *  FROM syscolumns c
		WHERE object_name(c.id) = 'cst_RecoverMaterialMaster' AND c.name = 'UserName')
Begin
 ALTER TABLE cst_RecoverMaterialMaster ADD UserName nvarchar(80) NULL
End
go

-- SO_CommitInventoryMaster
IF NOT EXISTS (SELECT *  FROM syscolumns c
		WHERE object_name(c.id) = 'SO_CommitInventoryMaster' AND c.name = 'UserName')
Begin
 ALTER TABLE SO_CommitInventoryMaster ADD UserName nvarchar(80) NULL
End
go

-- PRO_DCPResultDetail
IF NOT EXISTS (SELECT *  FROM syscolumns c
		WHERE object_name(c.id) = 'PRO_DCPResultDetail' AND c.name = 'IsManual')
Begin
 ALTER TABLE PRO_DCPResultDetail ADD IsManual bit NULL
 exec sp_bindefault Zero, 'PRO_DCPResultDetail.IsManual'
End
go


--enm_ACPurpose
IF NOT EXISTS(select id from dbo.sysobjects where id = object_id(N'enm_ACPurpose') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
Begin
 CREATE TABLE enm_ACPurpose (
       ACPurposeID          int IDENTITY,
       Description          nvarchar(80) NOT NULL,
       Code                 int NOT NULL
)

CREATE UNIQUE INDEX XAK1enm_ACPurpose ON enm_ACPurpose
(
       Code                           ASC
)

CREATE UNIQUE INDEX XAK2enm_ACPurpose ON enm_ACPurpose
(
       Description                    ASC
)

ALTER TABLE enm_ACPurpose
       ADD PRIMARY KEY (ACPurposeID)
End
go

--enm_ACObject
IF NOT EXISTS(select id from dbo.sysobjects where id = object_id(N'enm_ACObject') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
Begin
	CREATE TABLE enm_ACObject (
	       ACObjectID           int IDENTITY,
	       Code                 int NOT NULL,
	       Description          nvarchar(80) NOT NULL
	)
	
	CREATE UNIQUE INDEX XAK1enm_ACObject ON enm_ACObject
	(
	       Description                    ASC
	)
	
	CREATE UNIQUE INDEX XAK2enm_ACObject ON enm_ACObject
	(
	       Code                           ASC
	)
	
	ALTER TABLE enm_ACObject
	       ADD PRIMARY KEY (ACObjectID)
End
go



--cst_FreightMaster
IF NOT EXISTS (SELECT *  FROM syscolumns c
		WHERE object_name(c.id) = 'cst_FreightMaster' AND c.name = 'ACObjectID')
Begin
 ALTER TABLE cst_FreightMaster ADD ACObjectID int NULL
 ALTER TABLE cst_FreightMaster ADD ACPurposeID int NULL

 ALTER TABLE cst_FreightMaster
       ADD FOREIGN KEY (ACObjectID)
                            REFERENCES enm_ACObject
 ALTER TABLE cst_FreightMaster
       ADD FOREIGN KEY (ACPurposeID)
                             REFERENCES enm_ACPurpose
 ALTER TABLE cst_FreightMaster ALTER COLUMN TransporterID int NULL
 ALTER TABLE cst_FreightMaster ALTER COLUMN VendorID int NULL
--Drop Reference
 DECLARE @FKName varchar(100), @SQLStatement varchar(1000)
 DECLARE c CURSOR FOR SELECT object_name(constid) FROM sysreferences WHERE object_name(fkeyid) = 'cst_FreightMaster' AND object_name(rkeyid) = 'STD_CostElement'
 OPEN c
 FETCH NEXT FROM c INTO @FKName
 
 SELECT @SQLStatement = 'ALTER TABLE cst_FreightMaster DROP CONSTRAINT ' +  @FKName
 EXECUTE(@SQLStatement)
 CLOSE c
 DEALLOCATE c

 DROP INDEX cst_FreightMaster.XIE4cst_FreightMaster
 ALTER TABLE cst_FreightMaster DROP COLUMN CostElementID
End
go

--Add MakerID into cst_FreightMaster
IF NOT EXISTS (SELECT *  FROM syscolumns c
		WHERE object_name(c.id) = 'cst_FreightMaster' AND c.name = 'MakerID')
Begin
 ALTER TABLE cst_FreightMaster ADD MakerID int NULL

 ALTER TABLE cst_FreightMaster
       ADD FOREIGN KEY (MakerID)
                            REFERENCES MST_Party(PartyID)
End
go



--cst_FreightDetail
IF EXISTS (SELECT *  FROM syscolumns c
		WHERE object_name(c.id) = 'cst_FreightDetail' AND c.name = 'Line')
Begin
 ALTER TABLE cst_FreightDetail DROP COLUMN Line
 ALTER TABLE cst_FreightDetail ADD ImportTaxPercent NumberDec NULL
End
go


-- Add 2 fields into ITM_Product
IF NOT EXISTS (SELECT * FROM syscolumns c 
		WHERE object_name(c.id) = 'ITM_Product' AND c.name = 'MaxRoundUpToMin')
Begin
 ALTER TABLE ITM_Product ADD MaxRoundUpToMin NumberDec NULL
 ALTER TABLE ITM_Product ADD MaxRoundUpToMultiple NumberDec NULL
End
go

-- Add new 2 tables for DCP
IF NOT EXISTS(select id from dbo.sysobjects where id = object_id(N'PRO_PGProduct') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
Begin

CREATE TABLE PRO_ProductionGroup (
       ProductionGroupID    int IDENTITY,
       Description          nvarchar(80) NOT NULL,
       GroupProductionMax   NumberDec,
       ProductionLineID     int NOT NULL
)

CREATE UNIQUE INDEX XAK1PRO_ProductionGroup ON PRO_ProductionGroup
(
       Description                    ASC
)

ALTER TABLE PRO_ProductionGroup
       ADD PRIMARY KEY (ProductionGroupID)

ALTER TABLE PRO_ProductionGroup
       ADD FOREIGN KEY (ProductionLineID)
                             REFERENCES PRO_ProductionLine


CREATE TABLE PRO_PGProduct (
       PGProductID          int IDENTITY,
       ProductionGroupID    int NOT NULL,
       ProductID            int NOT NULL
)

CREATE UNIQUE INDEX XAK1PRO_PGProduct ON PRO_PGProduct
(
       ProductID                      ASC
)

ALTER TABLE PRO_PGProduct
       ADD PRIMARY KEY (PGProductID)

ALTER TABLE PRO_PGProduct
       ADD FOREIGN KEY (ProductID)
                             REFERENCES ITM_Product

ALTER TABLE PRO_PGProduct
       ADD FOREIGN KEY (ProductionGroupID)
                             REFERENCES PRO_ProductionGroup
End
go

IF EXISTS (SELECT * FROM syscolumns c 
		WHERE object_name(c.id) = 'cst_FreightDetail' AND c.name = 'ImportTaxPercent')
Begin
 ALTER TABLE cst_FreightDetail DROP COLUMN ImportTaxPercent
 ALTER TABLE cst_FreightDetail ADD ImportTax NumberDec NULL
End
go


-- Allow PurchaseOrderReceiptID NULL
--IF NOT EXISTS (SELECT id FROM syscolumns co WHERE co.id = object_id('cst_FreightMaster') AND name = 'PurchaseOrderReceiptID' AND IsNullable = 1) 
-- ALTER TABLE cst_FreightMaster ALTER COLUMN PurchaseOrderReceiptID int NULL
--go

-- Add Priority into PRO_ProductionGroup
IF NOT EXISTS (SELECT id FROM syscolumns co WHERE co.id = object_id('PRO_ProductionGroup') AND name = 'Priority') 
 ALTER TABLE PRO_ProductionGroup ADD Priority tinyint NULL
go

IF NOT EXISTS (SELECT id FROM syscolumns co WHERE co.id = object_id('PRO_ProductionGroup') AND name = 'Priority' AND IsNullAble = 1) 
Begin
 UPDATE PRO_ProductionGroup SET Priority = 1
 ALTER TABLE PRO_ProductionGroup ALTER COLUMN Priority tinyint NOT NULL
End

-- Add BalancePlanning into ProductionLine
IF NOT EXISTS (SELECT id FROM syscolumns co WHERE co.id = object_id('PRO_ProductionLine') AND name = 'BalancePlanning') 
Begin
 ALTER TABLE PRO_ProductionLine ADD BalancePlanning bit NULL
 exec sp_bindefault One, 'PRO_ProductionLine.BalancePlanning'
End
go


--IF EXISTS (SELECT id FROM syscolumns co WHERE co.id = object_id('PRO_ProductionLine') AND name = 'BalancePlanning') 
--Begin
-- UPDATE PRO_ProductionLine SET BalancePlanning = 1
--End
--go

-- cst_ACAdjustmentMaster
IF NOT EXISTS(select id from dbo.sysobjects where id = object_id(N'cst_ACAdjustmentMaster') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
Begin
CREATE TABLE cst_ACAdjustmentMaster (
       ACAdjustmentMasterID int IDENTITY,
       Code                 T_Code,
       Name                 nvarchar(100) NOT NULL,
       CCNID                int NOT NULL
)

CREATE UNIQUE INDEX XAK1cst_ACAdjustmentMaster ON cst_ACAdjustmentMaster
(
       Code                           ASC
)

CREATE UNIQUE INDEX XAK2cst_ACAdjustmentMaster ON cst_ACAdjustmentMaster
(
       Name                           ASC
)

ALTER TABLE cst_ACAdjustmentMaster
       ADD PRIMARY KEY (ACAdjustmentMasterID)

End
go


-- cst_ACAdjustmentDetail
IF NOT EXISTS(select id from dbo.sysobjects where id = object_id(N'cst_ACAdjustmentDetail') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
Begin
CREATE TABLE cst_ACAdjustmentDetail (
       ACAdjustmentDetail   int IDENTITY,
       Cost                 NumberDec,
       ACAdjustmentMasterID int NOT NULL,
       CostElementID        int NOT NULL
)


ALTER TABLE cst_ACAdjustmentDetail
       ADD PRIMARY KEY (ACAdjustmentDetail)

ALTER TABLE cst_ACAdjustmentDetail
       ADD FOREIGN KEY (CostElementID)
                             REFERENCES STD_CostElement

ALTER TABLE cst_ACAdjustmentDetail
       ADD FOREIGN KEY (ACAdjustmentMasterID)
                             REFERENCES cst_ACAdjustmentMaster

ALTER TABLE cst_ACAdjustmentMaster
       ADD FOREIGN KEY (CCNID)
                             REFERENCES MST_CCN
End
go

-- Add ACAdjustmentMaster
IF NOT EXISTS (SELECT id FROM syscolumns co WHERE co.id = object_id('ITM_Product') AND name = 'ACAdjustmentMasterID') 
Begin
ALTER TABLE ITM_Product ADD ACAdjustmentMasterID int NULL

ALTER TABLE ITM_Product
       ADD FOREIGN KEY (ACAdjustmentMasterID)
                             REFERENCES cst_ACAdjustmentMaster
End
go


-- Add Gate and related  
IF NOT EXISTS(select id from dbo.sysobjects where id = object_id(N'enm_GateType') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
Begin
 CREATE TABLE enm_GateType (
       GateTypeID           int IDENTITY,
       GateType             nvarchar(80) NOT NULL,
       Description          nvarchar(120) NULL
 )

 CREATE UNIQUE INDEX XAK1enm_GateType ON enm_GateType
 (
       GateType                       ASC
 )

 ALTER TABLE enm_GateType
       ADD PRIMARY KEY (GateTypeID)

 CREATE TABLE SO_Gate (
       GateID               int IDENTITY,
       Code                 nvarchar(40) NOT NULL,
       Description          nvarchar(80) NULL,
       GateTypeID           int NOT NULL
 )

 CREATE UNIQUE INDEX XAK1SO_Gate ON SO_Gate
 (
       Code                           ASC
 )

 ALTER TABLE SO_Gate
       ADD PRIMARY KEY (GateID)
 
 ALTER TABLE SO_Gate
       ADD FOREIGN KEY (GateTypeID)
                             REFERENCES enm_GateType


 ALTER TABLE SO_DeliverySchedule ADD GateID int NULL
 
 ALTER TABLE SO_DeliverySchedule
       ADD FOREIGN KEY (GateID)
                             REFERENCES SO_Gate
End
go

-- Add some fields into Shipping Management
IF NOT EXISTS (SELECT id FROM syscolumns co WHERE co.id = object_id('SO_ConfirmShipDetail') AND name = 'Price') 
Begin
 ALTER TABLE SO_ConfirmShipMaster ADD GateID int NULL
 ALTER TABLE SO_ConfirmShipMaster
       ADD FOREIGN KEY (GateID)
                             REFERENCES SO_Gate
 ALTER TABLE SO_ConfirmShipMaster ADD Comment nvarchar(100) NULL
 ALTER TABLE SO_ConfirmShipMaster ADD FromPort nvarchar(100) NULL
 ALTER TABLE SO_ConfirmShipMaster ADD CNo nvarchar(30) NULL
 ALTER TABLE SO_ConfirmShipMaster ADD Measurement NumberDec NULL
 ALTER TABLE SO_ConfirmShipMaster ADD GrossWeight NumberDec NULL
 ALTER TABLE SO_ConfirmShipMaster ADD NetWeight NumberDec NULL
 ALTER TABLE SO_ConfirmShipMaster ADD IssuingBank nvarchar(80) NULL
 ALTER TABLE SO_ConfirmShipMaster ADD LCDate DateTime NULL
 ALTER TABLE SO_ConfirmShipMaster ADD LCNo nvarchar(30) NULL
 ALTER TABLE SO_ConfirmShipMaster ADD VesselName nvarchar(30) NULL
 ALTER TABLE SO_ConfirmShipMaster ADD ShipCode nvarchar(20) NULL
 ALTER TABLE SO_ConfirmShipMaster ADD OnBoardDate DateTime NULL

 ALTER TABLE SO_ConfirmShipDetail ADD Price NumberDec NULL
End
go

IF EXISTS (SELECT id FROM syscolumns co WHERE co.id = object_id('SO_ConfirmShipDetail') AND name = 'Price' AND Isnullable = 1) 
Begin
-- UPDATE SO_ConfirmShipMaster SET FromPort = '', CNo = '', Measurement=0,GrossWeight=0,NetWeight=0, VesselName = '', ShipCode=''
 UPDATE SO_ConfirmShipMaster SET ShipCode=''

-- ALTER TABLE SO_ConfirmShipMaster ALTER COLUMN VesselName nvarchar(30) NOT NULL
 ALTER TABLE SO_ConfirmShipMaster ALTER COLUMN ShipCode nvarchar(20) NOT NULL
-- ALTER TABLE SO_ConfirmShipMaster ALTER COLUMN FromPort nvarchar(100) NOT NULL
-- ALTER TABLE SO_ConfirmShipMaster ALTER COLUMN CNo nvarchar(30) NOT NULL
-- ALTER TABLE SO_ConfirmShipMaster ALTER COLUMN Measurement NumberDec NOT NULL
-- ALTER TABLE SO_ConfirmShipMaster ALTER COLUMN GrossWeight NumberDec NOT NULL
-- ALTER TABLE SO_ConfirmShipMaster ALTER COLUMN NetWeight NumberDec NOT NULL

 UPDATE SO_ConfirmShipDetail SET Price = 0
 ALTER TABLE SO_ConfirmShipDetail ALTER COLUMN Price NumberDec NOT NULL

End
go

IF EXISTS (SELECT id FROM syscolumns co WHERE co.id = object_id('SO_ConfirmShipMaster') AND name = 'VesselName' AND Isnullable = 0) 
Begin
ALTER TABLE SO_ConfirmShipMaster ALTER COLUMN VesselName nvarchar(30) NULL
 ALTER TABLE SO_ConfirmShipMaster ALTER COLUMN FromPort nvarchar(100) NULL
 ALTER TABLE SO_ConfirmShipMaster ALTER COLUMN CNo nvarchar(30) NULL
 ALTER TABLE SO_ConfirmShipMaster ALTER COLUMN Measurement NumberDec NULL
 ALTER TABLE SO_ConfirmShipMaster ALTER COLUMN GrossWeight NumberDec NULL
 ALTER TABLE SO_ConfirmShipMaster ALTER COLUMN NetWeight NumberDec NULL
End
go


-- Modify Line to int
IF NOT EXISTS (SELECT * FROM syscolumns co WHERE co.id = object_id('cst_ActCostAllocationDetail') AND name = 'Line' AND Type = 56) 
 ALTER TABLE cst_ActCostAllocationDetail ALTER COLUMN Line int NOT NULL
go

-- Add PaymentTerm into MST_Party
IF NOT EXISTS (SELECT * FROM syscolumns co WHERE co.id = object_id('MST_Party') AND name = 'PaymentTermID') 
Begin
 ALTER TABLE MST_Party ADD PaymentTermID int NULL
 ALTER TABLE MST_Party
       ADD FOREIGN KEY (PaymentTermID)
                             REFERENCES MST_PaymentTerm
End
go


-- Create enm_PricingType table
IF NOT EXISTS(select id from dbo.sysobjects where id = object_id(N'enm_PricingType') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
Begin
CREATE TABLE enm_PricingType (
       PricingTypeID        int IDENTITY,
       Code                 int NOT NULL,
       Description          nvarchar(60) NOT NULL
)


CREATE UNIQUE INDEX XAK1enm_PricingType ON enm_PricingType
(
       Code                           ASC
)

CREATE UNIQUE INDEX XAK2enm_PricingType ON enm_PricingType
(
       Description                    ASC
)

ALTER TABLE enm_PricingType
       ADD PRIMARY KEY (PricingTypeID)

End

IF EXISTS (SELECT * FROM syscolumns co WHERE co.id = object_id('enm_PricingType') AND name = 'Code' and xtype = 231)
Begin
  DROP INDEX enm_PricingType.XAK1enm_PricingType
  ALTER TABLE enm_PricingType ALTER COLUMN Code nvarchar(60) NOT NULL
  CREATE UNIQUE INDEX XAK1enm_PricingType ON enm_PricingType
  (
       Code                           ASC
  )
End
go

-- Add some fields into PO_PurchaseOrderMaster
IF NOT EXISTS (SELECT * FROM syscolumns co WHERE co.id = object_id('PO_PurchaseOrderMaster') AND name = 'RequestDeliveryTime')
Begin
 ALTER TABLE PO_PurchaseOrderMaster ADD RequestDeliveryTime DateTime NULL
 ALTER TABLE PO_PurchaseOrderMaster ADD PricingTypeID int NULL
 ALTER TABLE PO_PurchaseOrderMaster ADD MakerID int NULL
 ALTER TABLE PO_PurchaseOrderMaster ADD MakerLocationID int NULL

 ALTER TABLE PO_PurchaseOrderMaster
       ADD FOREIGN KEY (PricingTypeID)
                             REFERENCES enm_PricingType

 ALTER TABLE PO_PurchaseOrderMaster
       ADD FOREIGN KEY (MakerID)
                             REFERENCES MST_Party

 ALTER TABLE PO_PurchaseOrderMaster
       ADD FOREIGN KEY (MakerLocationID)
                             REFERENCES MST_PartyLocation
End
go


-- Add Registered Code into ITM_Product
IF NOT EXISTS (SELECT * FROM syscolumns co WHERE co.id = object_id('ITM_Product') AND name = 'RegisteredCode')
 ALTER TABLE ITM_Product ADD RegisteredCode nvarchar(40) NULL


-- Create 
IF NOT EXISTS(select id from dbo.sysobjects where id = object_id(N'SO_Type') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
Begin
CREATE TABLE SO_Type (
       TypeID               int IDENTITY,
       Code                 nvarchar(3) NOT NULL,
       Description          nvarchar(40) NOT NULL
)

CREATE UNIQUE INDEX XAK1SO_Type ON SO_Type
(
       Code                           ASC
)

CREATE UNIQUE INDEX XAK2SO_Type ON SO_Type
(
       Description                    ASC
)

ALTER TABLE SO_Type
       ADD PRIMARY KEY (TypeID)
End
go

-- Add some fields into PO_PurchaseOrderMaster
IF NOT EXISTS (SELECT * FROM syscolumns co WHERE co.id = object_id('SO_SaleOrderMaster') AND name = 'TypeID')
Begin
 ALTER TABLE SO_SaleOrderMaster ADD TypeID int NULL
End
go

IF EXISTS (SELECT * FROM syscolumns co WHERE co.id = object_id('SO_SaleOrderMaster') AND name = 'TypeID' AND IsNullable = 1)
Begin
 UPDATE SO_SaleOrderMaster SET TypeID = 1 
 ALTER TABLE SO_SaleOrderMaster ALTER COLUMN TypeID int NOT NULL
End
go

IF NOT EXISTS (SELECT * FROM syscolumns co WHERE co.id = object_id('SO_ConfirmShipMaster') AND name = 'ReferenceNo')
 ALTER TABLE SO_ConfirmShipMaster ADD ReferenceNo nvarchar(60) NULL
go



IF NOT EXISTS (SELECT id FROM syscolumns co WHERE co.id = object_id('SO_ConfirmShipDetail') AND name = 'InvoiceQty')
Begin
 ALTER TABLE SO_ConfirmShipDetail ADD InvoiceQty NumberDec NULL
 ALTER TABLE SO_ConfirmShipDetail ADD VATPercent NumberDec NULL
 ALTER TABLE SO_ConfirmShipDetail ADD VATAmount NumberDec NULL
 ALTER TABLE SO_ConfirmShipMaster ADD InvoiceNo nvarchar(40) NULL
End
go

IF NOT EXISTS (SELECT id FROM syscolumns co WHERE co.id = object_id('SO_ConfirmShipDetail') AND name = 'InvoiceQty' AND IsNullable = 1)
Begin
 UPDATE SO_ConfirmShipDetail SET InvoiceQty = 0 where InvoiceQty is null
 ALTER TABLE SO_ConfirmShipDetail ALTER COLUMN InvoiceQty NumberDec NOT NULL 
End
go

IF EXISTS (SELECT id FROM syscolumns co WHERE co.id = object_id('SO_ConfirmShipDetail') AND name = 'AdjustQty')
Begin
 EXEC sp_unbindefault 'SO_ConfirmShipDetail.AdjustQty'
 ALTER TABLE SO_ConfirmShipDetail DROP COLUMN AdjustQty
 ALTER TABLE SO_ConfirmShipDetail DROP COLUMN OriginalQty
End
go


IF NOT EXISTS (SELECT id FROM syscolumns co WHERE co.id = object_id('CST_RecoverMaterialMaster') AND name = 'Comment')
 ALTER TABLE CST_RecoverMaterialMaster ADD Comment nvarchar(40) NULL
go

IF EXISTS (SELECT * FROM syscolumns co WHERE co.id = object_id('PO_PurchaseOrderMaster') AND name = 'RequestDeliveryTime' and xtype = 61)
Begin
 ALTER TABLE PO_PurchaseOrderMaster DROP COLUMN RequestDeliveryTime
 ALTER TABLE PO_PurchaseOrderMaster ADD RequestDeliveryTime int NULL
End
go

-- MST_Party add CurrencyID
IF NOT EXISTS (SELECT *  FROM syscolumns c
		WHERE object_name(c.id) = 'MST_Party' AND c.name = 'CurrencyID')
Begin
 ALTER TABLE MST_Party ADD CurrencyID int NULL
 ALTER TABLE MST_Party  WITH CHECK ADD FOREIGN KEY(CurrencyID)
REFERENCES MST_Currency (CurrencyID)
End
go

-- Add TABLE PRO_PlanningOffset
IF NOT EXISTS(select id from dbo.sysobjects where id = object_id(N'PRO_PlanningOffset') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
Begin

CREATE TABLE PRO_PlanningOffset (
       PlanningOffsetID     int IDENTITY,
       Offset               int NOT NULL,
       PlanningStartDate    datetime NULL,
       DCOptionMasterID     int NOT NULL,
       ProductionLineID     int NOT NULL
)

ALTER TABLE PRO_PlanningOffset
       ADD PRIMARY KEY (PlanningOffsetID)

CREATE UNIQUE INDEX XAK1PRO_PlanningOffset ON PRO_PlanningOffset
(
       DCOptionMasterID               ASC,
       ProductionLineID               ASC
)

exec sp_bindefault Zero, 'PRO_PlanningOffset.Offset'

ALTER TABLE PRO_PlanningOffset
       ADD FOREIGN KEY (ProductionLineID)
                             REFERENCES PRO_ProductionLine

ALTER TABLE PRO_PlanningOffset
       ADD FOREIGN KEY (DCOptionMasterID)
                             REFERENCES PRO_DCOptionMaster
end
go

-- MST_Party
IF NOT EXISTS (SELECT * FROM syscolumns co WHERE co.id = object_id('MST_Party') AND name = 'MAPBankAccountNo')
Begin
 ALTER TABLE MST_Party ADD MAPBankAccountNo nvarchar(100) NULL
 ALTER TABLE MST_Party ADD MAPBankAccountName nvarchar(100) NULL
End
go


-- IV_Adjustment
IF NOT EXISTS (SELECT * FROM syscolumns co WHERE co.id = object_id('IV_Adjustment') AND name = 'UsedByCosting')
Begin
 ALTER TABLE IV_Adjustment ADD UsedByCosting bit NULL
 exec sp_bindefault Zero, 'IV_Adjustment.UsedByCosting'
End
go


-- SO_ReturnedGoodsMaster
IF NOT EXISTS (SELECT * FROM syscolumns co WHERE co.id = object_id('SO_ReturnedGoodsMaster') AND name = 'CurrencyID')
Begin
 ALTER TABLE SO_ReturnedGoodsMaster ADD CurrencyID int NULL
 ALTER TABLE SO_ReturnedGoodsMaster
       ADD FOREIGN KEY (CurrencyID)
                             REFERENCES MST_Currency
 ALTER TABLE SO_ReturnedGoodsMaster ADD ExchangeRate decimal(20,5) NULL
End
go

-- SO_ReturnedGoodsDetail
IF NOT EXISTS (SELECT * FROM syscolumns co WHERE co.id = object_id('SO_ReturnedGoodsDetail') AND name = 'ConfirmShipMasterID')
Begin
 ALTER TABLE SO_ReturnedGoodsDetail ADD ConfirmShipMasterID int NULL
 ALTER TABLE SO_ReturnedGoodsDetail
       ADD FOREIGN KEY (ConfirmShipMasterID)
                             REFERENCES SO_ConfirmShipMaster
End
go

-- cst_FreightMaster
IF NOT EXISTS (SELECT * FROM syscolumns co WHERE co.id = object_id('cst_FreightMaster') AND name = 'ReturnToVendorMasterID')
Begin
 ALTER TABLE cst_FreightMaster ADD ReturnToVendorMasterID  int NULL
 ALTER TABLE cst_FreightMaster
       ADD FOREIGN KEY (ReturnToVendorMasterID)
                             REFERENCES po_ReturnToVendorMaster
 --ALTER TABLE cst_FreightMaster DROP FK__cst_Freig__Purch__1CDC41A7
 --ALTER TABLE cst_FreightMaster DROP COLUMN PurchaseOrderReceiptID
End
GO

-- po_ReturnToVendorMaster
IF NOT EXISTS (SELECT * FROM syscolumns co WHERE co.id = object_id('po_ReturnToVendorMaster') AND name = 'InvoiceMasterID')
Begin
 ALTER TABLE po_ReturnToVendorMaster ADD InvoiceMasterID  int NULL
 ALTER TABLE po_ReturnToVendorMaster
       ADD FOREIGN KEY (InvoiceMasterID)
                             REFERENCES po_InvoiceMaster
 ALTER TABLE po_ReturnToVendorMaster ADD ByPO bit NULL
 ALTER TABLE po_ReturnToVendorMaster ADD ByInvoice bit NULL
End
go

-- po_ReturnToVendorDetail
IF NOT EXISTS (SELECT * FROM syscolumns co WHERE co.id = object_id('po_ReturnToVendorDetail') AND name = 'UnitPrice')
Begin
 ALTER TABLE po_ReturnToVendorDetail ADD UnitPrice decimal(20,5) NULL
 ALTER TABLE po_ReturnToVendorDetail ADD Amount decimal(20,5) NULL
 ALTER TABLE po_ReturnToVendorDetail ADD VATPercent decimal(20,5) NULL
 ALTER TABLE po_ReturnToVendorDetail ADD VATAmount decimal(20,5) NULL
 ALTER TABLE po_ReturnToVendorDetail ADD TotalAmount decimal(20,5) NULL
End
go


-- CST_FreightDetail
IF NOT EXISTS (SELECT * FROM syscolumns co WHERE co.id = object_id('CST_FreightDetail') AND name = 'AdjustmentID')
Begin
 ALTER TABLE CST_FreightDetail ADD AdjustmentID int NULL
End
go

-- CST_ActualCostHistory
IF NOT EXISTS (SELECT * FROM syscolumns co WHERE co.id = object_id('CST_ActualCostHistory') AND name = 'BeginQuantity')
Begin
 ALTER TABLE CST_ActualCostHistory ADD BeginQuantity decimal(20,5) NULL
End
go

-- PRO_WorkOrderCompletion
IF NOT EXISTS (SELECT * FROM syscolumns co WHERE co.id = object_id('PRO_WorkOrderCompletion') AND name = 'CompletedDate')
Begin
 ALTER TABLE PRO_WorkOrderCompletion ADD CompletedDate DateTime NULL
End
go

--IF NOT EXISTS(select id from dbo.sysobjects where id = object_id(N'TwentyTwo'))
--Begin
--	CREATE DEFAULT dbo.TwentyTwo AS 24
--End
--Go

-- PRO_WorkingTime

IF NOT EXISTS(select id from dbo.sysobjects where id = object_id(N'PRO_WorkingTime') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
Begin
 CREATE TABLE PRO_WorkingTime (
		WorkingTimeID int IDENTITY,
		YearSetUp int,
		MonthSetUp int ,
		StartTime DateTime,
		EndTime DateTime,
		ProductionLineID int ,
		WorkingHours decimal
 )

 ALTER TABLE PRO_WorkingTime
       ADD PRIMARY KEY (WorkingTimeID)
	--exec sp_bindefault TwentyTwo, 'PRO_WorkingTime.WorkingHours'
End
go

-- Add new column to PRO_ProductionGroup
IF NOT EXISTS (SELECT * FROM syscolumns co WHERE co.id = object_id('PRO_ProductionGroup') AND name = 'CapacityOfGroup')
Begin
 ALTER TABLE PRO_ProductionGroup ADD CapacityOfGroup decimal(20,5) NULL
End
go

-- DROP unique for PRO_PGProduct
IF EXISTS(select * from dbo.sysindexes where name = N'XAK1PRO_PGProduct')
Begin
	DROP INDEX PRO_PGProduct.XAK1PRO_PGProduct
End
go

-- create unique for PRO_PGProduct
IF NOT EXISTS(select * from dbo.sysindexes where name = N'XAK1PRO_PGProduct')
Begin
	CREATE UNIQUE INDEX XAK1PRO_PGProduct ON PRO_PGProduct
	(
	       ProductID             ASC
	)
End
go

-- create unique for PRO_ProductionGroup
IF NOT EXISTS(select * from dbo.sysindexes where name = N'XAK1PRO_ProductionGroup')
Begin
	CREATE UNIQUE INDEX XAK1PRO_ProductionGroup ON PRO_ProductionGroup
	(
	       ProductionLineID	       ASC,
	       Description             ASC
	)
End
go

-- Add new column to PO_PurchaseOrderMaster
IF NOT EXISTS (SELECT * FROM syscolumns co WHERE co.id = object_id('PO_PurchaseOrderMaster') AND name = 'ReferenceNo')
Begin
	ALTER TABLE PO_PurchaseOrderMaster ADD ReferenceNo T_Code
End
go

-- IV_OnhandPeriod
IF NOT EXISTS(select id from dbo.sysobjects where id = object_id(N'IV_OnhandPeriod') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
BEGIN
	CREATE TABLE IV_OnhandPeriod (
	       OnhandPeriodID       int IDENTITY,
	       Code                 T_Code,
	       EffectDate           datetime NOT NULL,
	       Status               bit NULL
	)
	
	CREATE UNIQUE INDEX XAK1IV_OnhandPeriod ON IV_OnhandPeriod
	(
	       Code                           ASC
	)
	
	CREATE UNIQUE INDEX XAK2IV_OnhandPeriod ON IV_OnhandPeriod
	(
	       EffectDate                     ASC
	)
		
	ALTER TABLE IV_OnhandPeriod
	       ADD PRIMARY KEY (OnhandPeriodID)
END
go

-- IV_BalanceMasterLocation
IF NOT EXISTS(select id from dbo.sysobjects where id = object_id(N'IV_BalanceMasterLocation') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
BEGIN
	CREATE TABLE IV_BalanceMasterLocation (
	       BalanceMasterLocationID int IDENTITY,
	       EffectDate           datetime NULL,
	       OHQuantity           NumberDec,
	       CommitQuantity       NumberDec,
	       ProductID            int NOT NULL,
	       MasterLocationID     int NOT NULL,
	       StockUMID            int NULL
	)
	
	CREATE UNIQUE INDEX XAK1IV_BalanceMasterLocation ON IV_BalanceMasterLocation
	(
	       ProductID                      ASC,
	       MasterLocationID               ASC,
		   EffectDate					  ASC
	)
	
	ALTER TABLE IV_BalanceMasterLocation
	       ADD PRIMARY KEY (BalanceMasterLocationID)
	
	ALTER TABLE IV_BalanceMasterLocation
	       ADD FOREIGN KEY (StockUMID)
	                             REFERENCES MST_UnitOfMeasure
	
	ALTER TABLE IV_BalanceMasterLocation
	       ADD FOREIGN KEY (MasterLocationID)
	                             REFERENCES MST_MasterLocation
	
	ALTER TABLE IV_BalanceMasterLocation
	       ADD FOREIGN KEY (ProductID)
	                             REFERENCES ITM_Product
END
go

-- IV_BalanceLocation
IF NOT EXISTS(select id from dbo.sysobjects where id = object_id(N'IV_BalanceLocation') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
BEGIN
	CREATE TABLE IV_BalanceLocation (
	       BalanceLocationID    int IDENTITY,
	       EffectDate           datetime NULL,
	       OHQuantity           NumberDec,
	       CommitQuantity       NumberDec,
	       ProductID            int NOT NULL,
	       LocationID           int NOT NULL,
	       MasterLocationID     int NOT NULL,
	       StockUMID            int NULL
	)
	
	CREATE UNIQUE INDEX XAK1IV_BalanceLocation ON IV_BalanceLocation
	(
	       ProductID                      ASC,
	       MasterLocationID               ASC,
	       LocationID                     ASC,
		   EffectDate					  ASC
	)
	
	ALTER TABLE IV_BalanceLocation
	       ADD PRIMARY KEY (BalanceLocationID)
	ALTER TABLE IV_BalanceLocation
	       ADD FOREIGN KEY (StockUMID)
	                             REFERENCES MST_UnitOfMeasure
	ALTER TABLE IV_BalanceLocation
	       ADD FOREIGN KEY (MasterLocationID)
	                             REFERENCES MST_MasterLocation
	ALTER TABLE IV_BalanceLocation
	       ADD FOREIGN KEY (LocationID)
	                             REFERENCES MST_Location
	ALTER TABLE IV_BalanceLocation
	       ADD FOREIGN KEY (ProductID)
	                             REFERENCES ITM_Product
END
go

-- IV_BalanceBin
IF NOT EXISTS(select id from dbo.sysobjects where id = object_id(N'IV_BalanceBin') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
BEGIN
	CREATE TABLE IV_BalanceBin (
	       BalanceBinID         int IDENTITY,
	       EffectDate           datetime NULL,
	       OHQuantity           NumberDec,
	       CommitQuantity       NumberDec,
	       ProductID            int NOT NULL,
	       LocationID           int NOT NULL,
	       BinID                int NOT NULL,
	       StockUMID            int NULL
	)
	
	CREATE UNIQUE INDEX XAK1IV_BalanceBin ON IV_BalanceBin
	(
	       ProductID                      ASC,
	       LocationID                     ASC,
	       BinID                          ASC,
		   EffectDate					  ASC
	)
	
	ALTER TABLE IV_BalanceBin
	       ADD PRIMARY KEY (BalanceBinID)
	ALTER TABLE IV_BalanceBin
	       ADD FOREIGN KEY (StockUMID)
	                             REFERENCES MST_UnitOfMeasure
	ALTER TABLE IV_BalanceBin
	       ADD FOREIGN KEY (BinID)
	                             REFERENCES MST_BIN
	ALTER TABLE IV_BalanceBin
	       ADD FOREIGN KEY (LocationID)
	                             REFERENCES MST_Location
	ALTER TABLE IV_BalanceBin
	       ADD FOREIGN KEY (ProductID)
	                             REFERENCES ITM_Product
END
go

-- IV_BeginMRP
IF NOT EXISTS(select id from dbo.sysobjects where id = object_id(N'IV_BeginMRP') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
BEGIN
	CREATE TABLE IV_BeginMRP (
	       BeginMRPID         bigint IDENTITY,
	       AsOfDate           datetime NOT NULL,
	       Quantity           NumberDec NULL,
	       ProductID          int NOT NULL,
	       LocationID         int NOT NULL
	)
	
	CREATE UNIQUE INDEX XAK1IV_BeginMRP ON IV_BeginMRP
	(
	       ProductID                      ASC,
	       LocationID                     ASC,
	       AsOfDate                       ASC
	)
	
	ALTER TABLE IV_BeginMRP
	       ADD PRIMARY KEY (BeginMRPID)
	ALTER TABLE IV_BeginMRP
	       ADD FOREIGN KEY (LocationID)
	                             REFERENCES MST_Location
	ALTER TABLE IV_BeginMRP
	       ADD FOREIGN KEY (ProductID)
	                             REFERENCES ITM_Product
END
go

IF EXISTS(select id from dbo.sysobjects where id = object_id(N'IV_BeginStockForDCPReport') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
BEGIN
	DROP TABLE IV_BeginStockForDCPReport
END
go

-- add some fields into 'PRO_DCPResultMaster
IF NOT EXISTS (SELECT id FROM syscolumns WHERE object_name(id) = 'PRO_DCPResultMaster'  AND name = 'DeliveryQuantity')
Begin
	ALTER TABLE PRO_DCPResultMaster ADD DeliveryQuantity         NumberDec NULL
End
go

-- add some fields into 'MST_Location
IF NOT EXISTS (SELECT id FROM syscolumns WHERE object_name(id) = 'MST_Location'  AND name = 'DepartmentID')
Begin
	ALTER TABLE MST_Location ADD DepartmentID         int NULL
	ALTER TABLE MST_Location
	       ADD FOREIGN KEY (DepartmentID)
	                             REFERENCES MST_Department
End
go

-- add some fields into IV_BeginMRP
IF NOT EXISTS (SELECT id FROM syscolumns WHERE object_name(id) = 'IV_BeginMRP'  AND name = 'QuantityMAP')
Begin
	ALTER TABLE IV_BeginMRP ADD QuantityMAP         NumberDec NULL
End
go

-- add some fields into ITM_Product
IF NOT EXISTS (SELECT id FROM syscolumns WHERE object_name(id) = 'ITM_Product'  AND name = 'AVEG')
Begin
	ALTER TABLE ITM_Product ADD AVEG         bit NULL
	exec sp_bindefault One, 'ITM_Product.AVEG'
End
go

-- add some fields into MST_WorkCenter
IF NOT EXISTS (SELECT id FROM syscolumns WHERE object_name(id) = 'MST_WorkCenter'  AND name = 'SetMinProduce')
Begin
	ALTER TABLE MST_WorkCenter ADD SetMinProduce         bit NULL
	exec sp_bindefault One, 'MST_WorkCenter.SetMinProduce'
End
go

-- add some fields into PRO_DCPResultMaster
IF NOT EXISTS (SELECT id FROM syscolumns WHERE object_name(id) = 'PRO_DCPResultMaster'  AND name = 'MasterShiftID')
Begin
	ALTER TABLE PRO_DCPResultMaster ADD MasterShiftID         int NULL,
									MasterTotalSecond	NumberDec NULL
	ALTER TABLE PRO_DCPResultMaster
	       ADD FOREIGN KEY (MasterShiftID)
	                             REFERENCES PRO_Shift
End
go

-- DCP_OrderProduce
IF NOT EXISTS(select id from dbo.sysobjects where id = object_id(N'DCP_OrderProduce') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
BEGIN
	CREATE TABLE DCP_OrderProduce (
	       OrderProduceID         bigint IDENTITY,
	       WorkCenterID           int NULL,
	       ColumnName           T_Code NULL,
	       OrderNo          int NULL,
	       OrderPlan         int NULL
	)
	
	ALTER TABLE DCP_OrderProduce
	       ADD PRIMARY KEY (OrderProduceID)
	ALTER TABLE DCP_OrderProduce
	       ADD FOREIGN KEY (WorkCenterID)
	                             REFERENCES MST_WorkCenter
END
go

-- add some fields into DCP_OrderProduce
IF NOT EXISTS (SELECT id FROM syscolumns WHERE object_name(id) = 'DCP_OrderProduce'  AND name = 'ShiftID')
Begin
	ALTER TABLE DCP_OrderProduce ADD ShiftID         int NULL,
									 DCOptionMasterID int NULL
	ALTER TABLE DCP_OrderProduce
	       ADD FOREIGN KEY (ShiftID)
	                             REFERENCES PRO_Shift
	ALTER TABLE DCP_OrderProduce
	       ADD FOREIGN KEY (DCOptionMasterID)
	                             REFERENCES PRO_DCOptionMaster
End
go

-- add some fields into ITM_Product
IF NOT EXISTS (SELECT id FROM syscolumns WHERE object_name(id) = 'ITM_Product'  AND name = 'StockTakingCode')
Begin
	ALTER TABLE ITM_Product ADD StockTakingCode nvarchar(100) NULL
End
go

-- add some fields into PO_PurchaseOrderReceiptMaster
IF NOT EXISTS (SELECT id FROM syscolumns WHERE object_name(id) = 'PO_PurchaseOrderReceiptMaster'  AND name = 'Purpose')
Begin
	ALTER TABLE PO_PurchaseOrderReceiptMaster ADD Purpose int NULL
End
go

-- add some fields into IV_StockTakingPeriod
IF NOT EXISTS (SELECT id FROM syscolumns WHERE object_name(id) = 'IV_StockTakingPeriod'  AND name = 'Closed')
Begin
	ALTER TABLE IV_StockTakingPeriod ADD Closed bit NULL
End
go

-- add CancelDelivery field to PO_PurchaseOrderDetail
IF NOT EXISTS (SELECT id FROM syscolumns WHERE object_name(id) = 'PO_PurchaseOrderDetail'  AND name = 'CancelDelivery')
Begin
	ALTER TABLE PO_PurchaseOrderDetail ADD CancelDelivery bit NULL
End
ELSE
Begin
	ALTER TABLE PO_PurchaseOrderDetail DROP COLUMN CancelDelivery
End
go

-- add CancelDelivery field to PO_DeliverySchedule
IF NOT EXISTS (SELECT id FROM syscolumns WHERE object_name(id) = 'PO_DeliverySchedule'  AND name = 'CancelDelivery')
Begin
	ALTER TABLE PO_DeliverySchedule ADD CancelDelivery bit NULL
End

-- add MassOrder field to ITM_Product
IF NOT EXISTS (SELECT id FROM syscolumns WHERE object_name(id) = 'ITM_Product'  AND name = 'MassOrder')
Begin
	ALTER TABLE ITM_Product ADD MassOrder bit NULL
End

-- IV_StockTakingMaster
IF NOT EXISTS(select id from dbo.sysobjects where id = object_id(N'IV_StockTakingMaster') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
BEGIN
	CREATE TABLE IV_StockTakingMaster (
	       StockTakingMasterID  int IDENTITY,
	       StockTakingPeriodID  int NULL,
	       DepartmentID         int NULL,
	       ProductionLineID     int NULL,
	       LocationID           int NULL,
	       BinID                int NULL,
	       Code                 nvarchar(400) NOT NULL,
	       StockTakingDate      datetime NOT NULL
	)
	
	CREATE UNIQUE INDEX XAK1IV_StockTakingMaster ON IV_StockTakingMaster
	(
	       Code                           ASC
	)
	
	ALTER TABLE IV_StockTakingMaster
	       ADD PRIMARY KEY (StockTakingMasterID)
	ALTER TABLE IV_StockTakingMaster
	       ADD FOREIGN KEY (BinID)
	                             REFERENCES MST_BIN
	ALTER TABLE IV_StockTakingMaster
	       ADD FOREIGN KEY (LocationID)
	                             REFERENCES MST_Location
	ALTER TABLE IV_StockTakingMaster
	       ADD FOREIGN KEY (ProductionLineID)
	                             REFERENCES PRO_ProductionLine
	ALTER TABLE IV_StockTakingMaster
	       ADD FOREIGN KEY (DepartmentID)
	                             REFERENCES MST_Department
	ALTER TABLE IV_StockTakingMaster
	       ADD FOREIGN KEY (StockTakingPeriodID)
	                             REFERENCES IV_StockTakingPeriod
END
go


-- IV_StockTaking
IF NOT EXISTS(select id from dbo.sysobjects where id = object_id(N'IV_StockTaking') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
BEGIN
	CREATE TABLE IV_StockTaking (
	       StockTakingID        int IDENTITY,
	       Quantity             NumberDec NOT NULL,
	       SlipCode             T_Code,
	       Note                 T_Description,
	       BookQuantity         NumberDec,
	       ProductID            int NOT NULL,
	       StockUMID            int NULL,
	       CountingMethodID     int NOT NULL,
	       StockTakingMasterID  int NULL
	)
	
	CREATE UNIQUE INDEX XAK1IV_StockTaking ON IV_StockTaking
	(
	       StockTakingMasterID            ASC,
	       SlipCode                       ASC,
	       ProductID                      ASC
	)
	
	ALTER TABLE IV_StockTaking
	       ADD PRIMARY KEY (StockTakingID)
	ALTER TABLE IV_StockTaking
	       ADD FOREIGN KEY (StockTakingMasterID)
	                             REFERENCES IV_StockTakingMaster
	ALTER TABLE IV_StockTaking
	       ADD FOREIGN KEY (CountingMethodID)
	                             REFERENCES IV_CoutingMethod
	ALTER TABLE IV_StockTaking
	       ADD FOREIGN KEY (StockUMID)
	                             REFERENCES MST_UnitOfMeasure
	ALTER TABLE IV_StockTaking
	       ADD FOREIGN KEY (ProductID)
	                             REFERENCES ITM_Product
END
go

-- IV_StockTakingDifferent
IF NOT EXISTS(select id from dbo.sysobjects where id = object_id(N'IV_StockTakingDifferent') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
BEGIN
	CREATE TABLE IV_StockTakingDifferent (
	       StockTakingDifferentID bigint IDENTITY,
	       StockTakingDate      datetime NULL,
	       OHQuantity           NumberDec NULL,
	       ActualQuantity       NumberDec NULL,
	       DifferentQuantity    NumberDec NULL,
	       StockTakingPeriodID  int NULL,
	       ProductID            int NULL,
	       LocationID           int NULL,
	       BinID                int NULL
	)
	
	ALTER TABLE IV_StockTakingDifferent
	       ADD PRIMARY KEY (StockTakingDifferentID)
	ALTER TABLE IV_StockTakingDifferent
	       ADD FOREIGN KEY (BinID)
	                             REFERENCES MST_BIN
	ALTER TABLE IV_StockTakingDifferent
	       ADD FOREIGN KEY (LocationID)
	                             REFERENCES MST_Location
	ALTER TABLE IV_StockTakingDifferent
	       ADD FOREIGN KEY (ProductID)
	                             REFERENCES ITM_Product
	ALTER TABLE IV_StockTakingDifferent
	       ADD FOREIGN KEY (StockTakingPeriodID)
	                             REFERENCES IV_StockTakingPeriod
END
go

-- increase Code size in IV_StockTakingMaster
IF EXISTS (SELECT id FROM syscolumns WHERE object_name(id) = 'IV_StockTakingMaster'  AND name = 'Code')
Begin
	ALTER TABLE IV_StockTakingMaster ALTER COLUMN Code nvarchar(400)
End

-- A1 table for Import Plan Data
IF NOT EXISTS(select id from dbo.sysobjects where id = object_id(N'A1') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
BEGIN
	CREATE TABLE A1 (
	       ProductID int NULL,
	       F1 NumberDec NULL,
	       F2 NumberDec NULL,
	       F3 NumberDec NULL,
	       F4 NumberDec NULL,
	       F5 NumberDec NULL,
	       F6 NumberDec NULL,
	       F7 NumberDec NULL,
	       F8 NumberDec NULL,
	       F9 NumberDec NULL,
	       F10 NumberDec NULL,
	       F11 NumberDec NULL,
	       F12 NumberDec NULL,
	       F13 NumberDec NULL,
	       F14 NumberDec NULL,
	       F15 NumberDec NULL,
	       F16 NumberDec NULL,
	       F17 NumberDec NULL,
	       F18 NumberDec NULL,
	       F19 NumberDec NULL,
	       F20 NumberDec NULL,
	       F21 NumberDec NULL,
	       F22 NumberDec NULL,
	       F23 NumberDec NULL,
	       F24 NumberDec NULL,
	       F25 NumberDec NULL,
	       F26 NumberDec NULL,
	       F27 NumberDec NULL,
	       F28 NumberDec NULL,
	       F29 NumberDec NULL,
	       F30 NumberDec NULL,
	       F31 NumberDec NULL
	)
END
go

-- add ReturnToVendorDetailID field to CST_FreightDetail
IF NOT EXISTS (SELECT id FROM syscolumns WHERE object_name(id) = 'CST_FreightDetail'  AND name = 'ReturnToVendorDetailID')
Begin
	ALTER TABLE CST_FreightDetail ADD ReturnToVendorDetailID int NULL
	ALTER TABLE CST_FreightDetail
	       ADD FOREIGN KEY (ReturnToVendorDetailID)
	                             REFERENCES PO_ReturnToVendorDetail
End
go

-- add RunDCP field to MST_WorkCenter
IF NOT EXISTS (SELECT id FROM syscolumns WHERE object_name(id) = 'MST_WorkCenter'  AND name = 'RunDCP')
Begin
	ALTER TABLE MST_WorkCenter ADD RunDCP bit NULL
	UPDATE MST_WorkCenter SET RunDCP = 1
End
go


-- add DSOHRate field to CST_DSAndRecycleAllocation
IF NOT EXISTS (SELECT id FROM syscolumns WHERE object_name(id) = 'CST_DSAndRecycleAllocation'  AND name = 'DSOHRate')
Begin
	ALTER TABLE CST_DSAndRecycleAllocation ADD DSOHRate NumberDec NULL
End
go

-- add ProductID field to STD_CostCenterRateMaster
IF NOT EXISTS (SELECT id FROM syscolumns WHERE object_name(id) = 'STD_CostCenterRateMaster'  AND name = 'ProductID')
Begin
	ALTER TABLE STD_CostCenterRateMaster ADD ProductID int NULL
	ALTER TABLE STD_CostCenterRateMaster
	       ADD FOREIGN KEY (ProductID)
	                             REFERENCES ITM_Product
End
go

-- PRO_ProductProductionOrder
IF NOT EXISTS(select id from dbo.sysobjects where id = object_id(N'PRO_ProductProductionOrder') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
BEGIN
	CREATE TABLE PRO_ProductProductionOrder (
	       ProductProductionOrderID bigint IDENTITY,
	       ProductID            int NULL,
	       ProductionLineID           int NULL,
		   Seq int NULL
	)
	
	ALTER TABLE PRO_ProductProductionOrder
	       ADD PRIMARY KEY (ProductProductionOrderID)
	ALTER TABLE PRO_ProductProductionOrder
	       ADD FOREIGN KEY (ProductID)
	                             REFERENCES ITM_Product
	ALTER TABLE PRO_ProductProductionOrder
	       ADD FOREIGN KEY (ProductionLineID)
	                             REFERENCES PRO_ProductionLine

	CREATE UNIQUE INDEX XAK1PRO_ProductProductionOrder ON PRO_ProductProductionOrder
	(
	       ProductionLineID            ASC,
	       ProductID                      ASC
	)
END
go

-- add BomQuantity field to PRO_IssueMaterialDetail
IF NOT EXISTS (SELECT id FROM syscolumns WHERE object_name(id) = 'PRO_IssueMaterialDetail'  AND name = 'BomQuantity')
Begin
	ALTER TABLE PRO_IssueMaterialDetail ADD BomQuantity NumberDec NULL
End
go

-- DROP Table PRO_CompletionStdCost
IF EXISTS(select id from dbo.sysobjects where id = object_id(N'PRO_CompletionStdCost') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
Begin
	DROP TABLE PRO_CompletionStdCost
End
go

-- add ProductionLineID field to PO_ReturnToVendorMaster
IF NOT EXISTS (SELECT id FROM syscolumns WHERE object_name(id) = 'PO_ReturnToVendorMaster'  AND name = 'ProductionLineID')
Begin
	ALTER TABLE PO_ReturnToVendorMaster ADD ProductionLineID int NULL
	ALTER TABLE PO_ReturnToVendorMaster
	       ADD FOREIGN KEY (ProductionLineID)
	                             REFERENCES PRO_ProductionLine
End
go

-- add RefDetailID field to PO_ReturnToVendorDetail
IF NOT EXISTS (SELECT id FROM syscolumns WHERE object_name(id) = 'PO_ReturnToVendorDetail'  AND name = 'RefDetailID')
Begin
	ALTER TABLE PO_ReturnToVendorDetail ADD RefDetailID int NULL
End
go


-- add NetAmount field to SO_ConfirmShipDetail
IF NOT EXISTS (SELECT id FROM syscolumns WHERE object_name(id) = 'SO_ConfirmShipDetail'  AND name = 'NetAmount')
Begin
	ALTER TABLE SO_ConfirmShipDetail ADD NetAmount decimal(20,8)
End
go

-- add NetAmount field to dbo.SO_InvoiceDetail
IF NOT EXISTS (SELECT id FROM syscolumns WHERE object_name(id) = 'SO_InvoiceDetail'  AND name = 'NetAmount')
Begin
	ALTER TABLE SO_InvoiceDetail ADD NetAmount decimal(20,8)
End
go

-- add Purpose field to dbo.Sys_PostdateConfiguration
IF NOT EXISTS (SELECT id FROM syscolumns WHERE object_name(id) = 'Sys_PostdateConfiguration'  AND name = 'Purpose')
Begin
	ALTER TABLE Sys_PostdateConfiguration ADD Purpose nvarchar(50) NULL
End
go

-- add HistoryQuantity field to IV_StockTakingDifferent
IF NOT EXISTS (SELECT id FROM syscolumns WHERE object_name(id) = 'IV_StockTakingDifferent'  AND name = 'HistoryQuantity')
Begin
	ALTER TABLE IV_StockTakingDifferent ADD HistoryQuantity decimal(18,5) NULL
End
go

---------------------------------OTHERS PART----------------------------------------------

-- Record Permisson for IssueMaterial
IF EXISTS (SELECT * FROM sys_RelatedView WHERE TableOrView = 'v_RemainComponentForWOIssue')
 UPDATE sys_RelatedView SET TableOrView = 'v_RemainComponentForWOIssueWithParentInfo', MapField = 'ComponentID' WHERE TableOrView = 'v_RemainComponentForWOIssue'
go

-- Insert PO Receipt Type
IF NOT EXISTS (SELECT * FROM enm_POReceiptType WHERE POReceiptTypeCode = 0)
Begin
SET IDENTITY_INSERT enm_POReceiptType ON

 INSERT INTO [dbo].[enm_POReceiptType] ([POReceiptTypeID], [POReceiptTypeCode], [Description] )
 VALUES ( 1, 0, 'By PO' )

 INSERT INTO [dbo].[enm_POReceiptType] ([POReceiptTypeID], [POReceiptTypeCode], [Description] )
 VALUES ( 2, 1, 'By Item' )

 INSERT INTO [dbo].[enm_POReceiptType] ([POReceiptTypeID], [POReceiptTypeCode], [Description] )
 VALUES ( 3, 2, 'ByDeliverySlip' )

 INSERT INTO [dbo].[enm_POReceiptType] ([POReceiptTypeID], [POReceiptTypeCode], [Description] )
 VALUES ( 5, 3, 'ByInvoice' )

 INSERT INTO [dbo].[enm_POReceiptType] ([POReceiptTypeID], [POReceiptTypeCode], [Description] )
 VALUES ( 6, 4, 'ByOutside' )

SET IDENTITY_INSERT enm_POReceiptType OFF
End
go


-- Insert CostMethod
IF NOT EXISTS (SELECT * FROM enm_CostMethod WHERE Code = 1)
Begin
SET IDENTITY_INSERT enm_CostMethod ON
 INSERT INTO [dbo].[enm_CostMethod] ([CostMethodID], [Code], [Description] )
 VALUES ( 1, 0, 'ACT' ) 

 INSERT INTO [dbo].[enm_CostMethod] ([CostMethodID], [Code], [Description] )
 VALUES ( 2, 1, 'STD' ) 

 INSERT INTO [dbo].[enm_CostMethod] ([CostMethodID], [Code], [Description] )
 VALUES ( 3, 2, 'AVG' ) 

SET IDENTITY_INSERT enm_CostMethod OFF
End
go


-- enm_WOLineStatus
IF NOT EXISTS (SELECT * FROM enm_WOLineStatus WHERE Code = 1)
Begin
SET IDENTITY_INSERT enm_WOLineStatus ON

INSERT INTO [dbo].[enm_WOLineStatus] ([WOLineStatusID], [Code], [Description] )
 VALUES ( 1, 1, 'Un-Released' )

INSERT INTO [dbo].[enm_WOLineStatus] ([WOLineStatusID], [Code], [Description] )
 VALUES ( 2, 2, 'Released' )

INSERT INTO [dbo].[enm_WOLineStatus] ([WOLineStatusID], [Code], [Description] )
 VALUES ( 3, 3, 'Manufactoring Close' )

INSERT INTO [dbo].[enm_WOLineStatus] ([WOLineStatusID], [Code], [Description] )
 VALUES ( 4, 4, 'Finance Close' )

SET IDENTITY_INSERT enm_WOLineStatus OFF
End

go


IF NOT EXISTS(SELECT * FROM sys_RelatedView WHERE TableOrView = 'V_ProductInBinCache')
 INSERT INTO sys_RelatedView (TableOrView, RecordSecurityParamID, MapField)
	VALUES('V_ProductInBinCache',2, 'ProductID')
go


IF NOT EXISTS(SELECT * FROM sys_RelatedView WHERE TableOrView = 'V_ProductForCustomer')
 INSERT INTO sys_RelatedView (TableOrView, RecordSecurityParamID, MapField)
	VALUES('V_ProductForCustomer',2, 'ProductID')
go

-- Add 'Xu?t bù cho khách hàng'
IF NOT EXISTS (SELECT * FROM PRO_IssuePurpose WHERE TranTypeID = 24 AND Code = 20) 
 INSERT INTO PRO_IssuePurpose VALUES(N'Xu?t bù cho khách hàng',24,0,20)
go

-- Additional Charge Purpose
IF NOT EXISTS(SELECT * FROM enm_ACPurpose WHERE Code = 1)
Begin
 INSERT INTO enm_ACPurpose(Code, Description) VALUES(1,'Freight')
 INSERT INTO enm_ACPurpose(Code, Description) VALUES(2,'Import Tax')
 INSERT INTO enm_ACPurpose(Code, Description) VALUES(3,'Credit Note')
 INSERT INTO enm_ACPurpose(Code, Description) VALUES(4,'Debit Note')
End
go


-- Additional Charge Object
IF NOT EXISTS(SELECT * FROM enm_ACObject WHERE Code = 1)
Begin
 INSERT INTO enm_ACObject(Code, Description) VALUES(1,'ReceiptTransaction')
 INSERT INTO enm_ACObject(Code, Description) VALUES(2,'Item Inventory')
End
go

IF EXISTS(SELECT * FROM enm_ACObject WHERE Description = 'ReceiptTransaction')
 UPDATE enm_ACObject SET Description = 'Receipt Transaction' WHERE Description = 'ReceiptTransaction'
go

-- Constants for SO_Type
IF NOT EXISTS(SELECT * FROM SO_Type WHERE Code = '1')
Begin
SET IDENTITY_INSERT SO_Type ON
 INSERT INTO SO_Type(TypeID, Code, Description) VALUES(1,'1','Domestic')
 INSERT INTO SO_Type(TypeID, Code, Description) VALUES(2,'2','Export')
SET IDENTITY_INSERT SO_Type OFF
End
go

IF EXISTS(SELECT * FROM SO_Type WHERE Description = 'Dosmetic')
Begin
	UPDATE SO_Type SET Description = 'Domestic' WHERE Description = 'Dosmetic'
End

IF NOT EXISTS (SELECT * FROM sys_Param WHERE Name='BankAddr')
 INSERT INTO sys_Param (Name, Value) VALUES('BankAddr','17 Ngo Quyen, Hanoi, Viet nam')
go

-- MST_TranType
IF NOT EXISTS (SELECT * FROM MST_TranType WHERE TranTypeID = 26) 
Begin
 SET IDENTITY_INSERT MST_TranType ON
 INSERT INTO MST_TranType(TranTypeID,Code,Description,[Type]) 
	VALUES(26,'ShippingAdjustment','Shipping adjustment',2)
 SET IDENTITY_INSERT MST_TranType OFF
End
go

-- Update Maker = Vendor, Maker Location = Vendor Location 
Update PO_PurchaseOrderMaster Set MakerID=PartyID, MakerLocationID=VendorLocID 
WHERE MakerID IS NULL
go

-- MST_TranType update TransactionHistoryType cho PO Receipt thanh Both (= 2)
Update MST_TranType Set Type = 2
WHERE Code = 'POPurchaseOrderReceipts'
go

-- Constants for enm_PartyTypeEnumType
IF NOT EXISTS(SELECT * FROM enm_PartyTypeEnum WHERE Value = '3')
Begin
 INSERT INTO enm_PartyTypeEnum(Value, Name, Description) VALUES('3','Vendor-Outside','Vendor Outside')
End
go

-- Constants for PO_PurchaseType
IF NOT EXISTS(SELECT * FROM PO_PurchaseType WHERE Code = 'Domestic')
Begin
SET IDENTITY_INSERT PO_PurchaseType ON
 INSERT INTO PO_PurchaseType(PurchaseTypeID, Code, Description) VALUES(1,'Domestic','Domestic')
 INSERT INTO PO_PurchaseType(PurchaseTypeID, Code, Description) VALUES(2,'Import','Import')
 INSERT INTO PO_PurchaseType(PurchaseTypeID, Code, Description) VALUES(3,'Outside','Outside')
SET IDENTITY_INSERT PO_PurchaseType OFF
End
go

-- MST_TranType
IF NOT EXISTS (SELECT * FROM MST_TranType WHERE TranTypeID = 27) 
Begin
 SET IDENTITY_INSERT MST_TranType ON
 INSERT INTO MST_TranType(TranTypeID,Code,Description) 
	VALUES(27,'DeleteTransaction','DeleteTransaction')
 SET IDENTITY_INSERT MST_TranType OFF
End
go

---------------------------------VISIBILITY PART----------------------------------------------
-- Remove Convert To Existing buttons from CPO DataViewer
IF EXISTS (SELECT * FROM sys_VisibilityItem WHERE name like '%btnExisting%')
 DELETE FROM sys_VisibilityItem WHERE name like '%btnExisting%'
go
IF EXISTS (SELECT * FROM sys_VisibilityGroup WHERE GroupText like '%Convert To Existing%')
 DELETE FROM sys_VisibilityGroup WHERE GroupText like '%Convert To Existing%'
go
