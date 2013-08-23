/*
File:		ViewScript.sql
Created:	DungLA
Purpose:	All view in PCS
*/

/****** Object:  View [dbo].[V_MultiWOCompletion]    Script Date: 05/06/2010 12:53:52 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[V_MultiWOCompletion]'))
DROP VIEW [V_MultiWOCompletion]
GO

/****** Object:  View [dbo].[V_InOutStockReport_GroupByBinType]    Script Date: 05/06/2010 12:53:52 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[V_InOutStockReport_GroupByBinType]'))
DROP VIEW [V_InOutStockReport_GroupByBinType]
GO
/****** Object:  View [dbo].[v_POReceiptImport]    Script Date: 05/06/2010 12:53:52 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_POReceiptImport]'))
DROP VIEW [v_POReceiptImport]
GO
/****** Object:  View [dbo].[v_ConfirmShipByCustomer]    Script Date: 05/06/2010 12:53:52 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_ConfirmShipByCustomer]'))
DROP VIEW [v_ConfirmShipByCustomer]
GO
/****** Object:  View [dbo].[v_PO_ReturnToVendorMaster]    Script Date: 05/06/2010 12:53:52 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_PO_ReturnToVendorMaster]'))
DROP VIEW [v_PO_ReturnToVendorMaster]
GO
/****** Object:  View [dbo].[v_SOConfirmShipMaster]    Script Date: 05/06/2010 12:53:52 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_SOConfirmShipMaster]'))
DROP VIEW [v_SOConfirmShipMaster]
GO
/****** Object:  View [dbo].[v_SOInvoiceMaster]    Script Date: 05/06/2010 12:53:52 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_SOInvoiceMaster]'))
DROP VIEW [v_SOInvoiceMaster]
GO
/****** Object:  View [dbo].[v_ShippedSaleOrder]    Script Date: 05/06/2010 12:53:52 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_ShippedSaleOrder]'))
DROP VIEW [v_ShippedSaleOrder]
GO
/****** Object:  View [dbo].[v_SO_SaleOrderMasterHasCommit]    Script Date: 05/06/2010 12:53:52 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_SO_SaleOrderMasterHasCommit]'))
DROP VIEW [v_SO_SaleOrderMasterHasCommit]
GO
/****** Object:  View [dbo].[v_WorkCenterByActualCost]    Script Date: 05/06/2010 12:53:53 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_WorkCenterByActualCost]'))
DROP VIEW [v_WorkCenterByActualCost]
GO
/****** Object:  View [dbo].[v_OHDSRecycleAdjRpt]    Script Date: 05/06/2010 12:53:52 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_OHDSRecycleAdjRpt]'))
DROP VIEW [v_OHDSRecycleAdjRpt]
GO
/****** Object:  View [dbo].[v_CGS1]    Script Date: 05/06/2010 12:53:52 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_CGS1]'))
DROP VIEW [v_CGS1]
GO
/****** Object:  View [dbo].[v_TotalReturnedGoods]    Script Date: 05/06/2010 12:53:53 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_TotalReturnedGoods]'))
DROP VIEW [v_TotalReturnedGoods]
GO
/****** Object:  View [dbo].[V_InOutStockReport_GroupByBinType_1]    Script Date: 05/06/2010 12:53:52 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[V_InOutStockReport_GroupByBinType_1]'))
DROP VIEW [V_InOutStockReport_GroupByBinType_1]
GO
/****** Object:  View [dbo].[v_LocalReceive]    Script Date: 05/06/2010 12:53:52 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_LocalReceive]'))
DROP VIEW [v_LocalReceive]
GO
/****** Object:  View [dbo].[v_ReceiptByVendor]    Script Date: 05/06/2010 12:53:52 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_ReceiptByVendor]'))
DROP VIEW [v_ReceiptByVendor]
GO
/****** Object:  View [dbo].[v_SOCancelCommitment]    Script Date: 05/06/2010 12:53:52 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_SOCancelCommitment]'))
DROP VIEW [v_SOCancelCommitment]
GO
/****** Object:  View [dbo].[v_TotalCommitInventory]    Script Date: 05/06/2010 12:53:53 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_TotalCommitInventory]'))
DROP VIEW [v_TotalCommitInventory]
GO
/****** Object:  View [dbo].[v_ApprovedAndNotCompletedPO]    Script Date: 05/06/2010 12:53:52 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_ApprovedAndNotCompletedPO]'))
DROP VIEW [v_ApprovedAndNotCompletedPO]
GO
/****** Object:  View [dbo].[v_ApprovedAndNotCompletedPOLine]    Script Date: 05/06/2010 12:53:52 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_ApprovedAndNotCompletedPOLine]'))
DROP VIEW [v_ApprovedAndNotCompletedPOLine]
GO
/****** Object:  View [dbo].[v_CloseOrOpenPO]    Script Date: 05/06/2010 12:53:52 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_CloseOrOpenPO]'))
DROP VIEW [v_CloseOrOpenPO]
GO
/****** Object:  View [dbo].[v_IV_MaterialReceipt]    Script Date: 05/06/2010 12:53:52 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_IV_MaterialReceipt]'))
DROP VIEW [v_IV_MaterialReceipt]
GO
/****** Object:  View [dbo].[v_PO_Approve]    Script Date: 05/06/2010 12:53:52 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_PO_Approve]'))
DROP VIEW [v_PO_Approve]
GO
/****** Object:  View [dbo].[v_PO_NOT_Approve]    Script Date: 05/06/2010 12:53:52 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_PO_NOT_Approve]'))
DROP VIEW [v_PO_NOT_Approve]
GO
/****** Object:  View [dbo].[v_PO_PurchaseOrderMasterHasReceive]    Script Date: 05/06/2010 12:53:52 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_PO_PurchaseOrderMasterHasReceive]'))
DROP VIEW [v_PO_PurchaseOrderMasterHasReceive]
GO
/****** Object:  View [dbo].[v_PODeliver_Maker]    Script Date: 05/06/2010 12:53:52 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_PODeliver_Maker]'))
DROP VIEW [v_PODeliver_Maker]
GO
/****** Object:  View [dbo].[v_POReturnToVendor]    Script Date: 05/06/2010 12:53:52 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_POReturnToVendor]'))
DROP VIEW [v_POReturnToVendor]
GO
/****** Object:  View [dbo].[v_PurchaseOrderOfItem]    Script Date: 05/06/2010 12:53:52 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_PurchaseOrderOfItem]'))
DROP VIEW [v_PurchaseOrderOfItem]
GO
/****** Object:  View [dbo].[v_ReceiptBySchedule]    Script Date: 05/06/2010 12:53:52 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_ReceiptBySchedule]'))
DROP VIEW [v_ReceiptBySchedule]
GO
/****** Object:  View [dbo].[v_SelectPurchaseOrders]    Script Date: 05/06/2010 12:53:52 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_SelectPurchaseOrders]'))
DROP VIEW [v_SelectPurchaseOrders]
GO
/****** Object:  View [dbo].[v_SelectUnclosedPO4Invoice]    Script Date: 05/06/2010 12:53:52 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_SelectUnclosedPO4Invoice]'))
DROP VIEW [v_SelectUnclosedPO4Invoice]
GO
/****** Object:  View [dbo].[v_LeadTimeByMainWorkCenter]    Script Date: 05/06/2010 12:53:52 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_LeadTimeByMainWorkCenter]'))
DROP VIEW [v_LeadTimeByMainWorkCenter]
GO
/****** Object:  View [dbo].[v_WCCapacityByCategory]    Script Date: 05/06/2010 12:53:53 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_WCCapacityByCategory]'))
DROP VIEW [v_WCCapacityByCategory]
GO
/****** Object:  View [dbo].[v_DetailMaterialIssue]    Script Date: 05/06/2010 12:53:52 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_DetailMaterialIssue]'))
DROP VIEW [v_DetailMaterialIssue]
GO
/****** Object:  View [dbo].[v_ProductForCustomer]    Script Date: 05/06/2010 12:53:52 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_ProductForCustomer]'))
DROP VIEW [v_ProductForCustomer]
GO
/****** Object:  View [dbo].[v_SaleInvoice]    Script Date: 05/06/2010 12:53:52 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_SaleInvoice]'))
DROP VIEW [v_SaleInvoice]
GO
/****** Object:  View [dbo].[v_SaleOrderForWOLine]    Script Date: 05/06/2010 12:53:52 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_SaleOrderForWOLine]'))
DROP VIEW [v_SaleOrderForWOLine]
GO
/****** Object:  View [dbo].[v_SOMasterForShippingManagement]    Script Date: 05/06/2010 12:53:52 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_SOMasterForShippingManagement]'))
DROP VIEW [v_SOMasterForShippingManagement]
GO
/****** Object:  View [dbo].[v_SOMasterNotRelease]    Script Date: 05/06/2010 12:53:52 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_SOMasterNotRelease]'))
DROP VIEW [v_SOMasterNotRelease]
GO
/****** Object:  View [dbo].[v_SOMasterToCommit]    Script Date: 05/06/2010 12:53:52 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_SOMasterToCommit]'))
DROP VIEW [v_SOMasterToCommit]
GO
/****** Object:  View [dbo].[V_SONotCompletedShip]    Script Date: 05/06/2010 12:53:52 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[V_SONotCompletedShip]'))
DROP VIEW [V_SONotCompletedShip]
GO
/****** Object:  View [dbo].[v_SOToReturned]    Script Date: 05/06/2010 12:53:52 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_SOToReturned]'))
DROP VIEW [v_SOToReturned]
GO
/****** Object:  View [dbo].[v_ProductGroupInfo]    Script Date: 05/06/2010 12:53:52 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_ProductGroupInfo]'))
DROP VIEW [v_ProductGroupInfo]
GO
/****** Object:  View [dbo].[v_PRO_WORouting]    Script Date: 05/06/2010 12:53:52 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_PRO_WORouting]'))
DROP VIEW [v_PRO_WORouting]
GO
/****** Object:  View [dbo].[v_OutputInPeriod]    Script Date: 05/06/2010 12:53:52 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_OutputInPeriod]'))
DROP VIEW [v_OutputInPeriod]
GO
/****** Object:  View [dbo].[v_TransferLocation]    Script Date: 05/06/2010 12:53:53 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_TransferLocation]'))
DROP VIEW [v_TransferLocation]
GO
/****** Object:  View [dbo].[v_InvoiceToReturn]    Script Date: 05/06/2010 12:53:52 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_InvoiceToReturn]'))
DROP VIEW [v_InvoiceToReturn]
GO
/****** Object:  View [dbo].[V_BIN_OK_AND_NG]    Script Date: 05/06/2010 12:53:52 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[V_BIN_OK_AND_NG]'))
DROP VIEW [V_BIN_OK_AND_NG]
GO
/****** Object:  View [dbo].[v_BinExceptDestroy]    Script Date: 05/06/2010 12:53:52 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_BinExceptDestroy]'))
DROP VIEW [v_BinExceptDestroy]
GO
/****** Object:  View [dbo].[v_DetailedPOInvoiceMaster]    Script Date: 05/06/2010 12:53:52 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_DetailedPOInvoiceMaster]'))
DROP VIEW [v_DetailedPOInvoiceMaster]
GO
/****** Object:  View [dbo].[v_ProductInProductionLine]    Script Date: 05/06/2010 12:53:52 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_ProductInProductionLine]'))
DROP VIEW [v_ProductInProductionLine]
GO
/****** Object:  View [dbo].[V_ProductInWorkCenter]    Script Date: 05/06/2010 12:53:52 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[V_ProductInWorkCenter]'))
DROP VIEW [V_ProductInWorkCenter]
GO
/****** Object:  View [dbo].[V_SelectWOBaseProductionLine]    Script Date: 05/06/2010 12:53:52 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[V_SelectWOBaseProductionLine]'))
DROP VIEW [V_SelectWOBaseProductionLine]
GO
/****** Object:  View [dbo].[v_WOReleaseForCompletion]    Script Date: 05/06/2010 12:53:53 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_WOReleaseForCompletion]'))
DROP VIEW [v_WOReleaseForCompletion]
GO
/****** Object:  View [dbo].[v_AvailableForReversal]    Script Date: 05/06/2010 12:53:52 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_AvailableForReversal]'))
DROP VIEW [v_AvailableForReversal]
GO
/****** Object:  View [dbo].[v_PRO_DispatchDetail]    Script Date: 05/06/2010 12:53:52 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_PRO_DispatchDetail]'))
DROP VIEW [v_PRO_DispatchDetail]
GO
/****** Object:  View [dbo].[v_PRO_IssueMaterialDetail]    Script Date: 05/06/2010 12:53:52 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_PRO_IssueMaterialDetail]'))
DROP VIEW [v_PRO_IssueMaterialDetail]
GO
/****** Object:  View [dbo].[v_Pro_LaborTimeDetail]    Script Date: 05/06/2010 12:53:52 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_Pro_LaborTimeDetail]'))
DROP VIEW [v_Pro_LaborTimeDetail]
GO
/****** Object:  View [dbo].[v_Pro_MachineTimeDetail]    Script Date: 05/06/2010 12:53:52 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_Pro_MachineTimeDetail]'))
DROP VIEW [v_Pro_MachineTimeDetail]
GO
/****** Object:  View [dbo].[v_PRO_WorkOrderBomDetail]    Script Date: 05/06/2010 12:53:52 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_PRO_WorkOrderBomDetail]'))
DROP VIEW [v_PRO_WorkOrderBomDetail]
GO
/****** Object:  View [dbo].[v_ReleasedAndMFClosedWorkOrder]    Script Date: 05/06/2010 12:53:52 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_ReleasedAndMFClosedWorkOrder]'))
DROP VIEW [v_ReleasedAndMFClosedWorkOrder]
GO
/****** Object:  View [dbo].[v_ReleasedWO]    Script Date: 05/06/2010 12:53:52 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_ReleasedWO]'))
DROP VIEW [v_ReleasedWO]
GO
/****** Object:  View [dbo].[v_ReleasedWorkOrder]    Script Date: 05/06/2010 12:53:52 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_ReleasedWorkOrder]'))
DROP VIEW [v_ReleasedWorkOrder]
GO
/****** Object:  View [dbo].[v_ReleasedWorkOrderWithProductID]    Script Date: 05/06/2010 12:53:52 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_ReleasedWorkOrderWithProductID]'))
DROP VIEW [v_ReleasedWorkOrderWithProductID]
GO
/****** Object:  View [dbo].[v_ReleasedWOWithLocation]    Script Date: 05/06/2010 12:53:52 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_ReleasedWOWithLocation]'))
DROP VIEW [v_ReleasedWOWithLocation]
GO
/****** Object:  View [dbo].[v_ReleaseWorkOrder]    Script Date: 05/06/2010 12:53:52 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_ReleaseWorkOrder]'))
DROP VIEW [v_ReleaseWorkOrder]
GO
/****** Object:  View [dbo].[v_RemainComponentForWOIssue]    Script Date: 05/06/2010 12:53:52 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_RemainComponentForWOIssue]'))
DROP VIEW [v_RemainComponentForWOIssue]
GO
/****** Object:  View [dbo].[v_RemainComponentForWOIssueWithParentInfo]    Script Date: 05/06/2010 12:53:52 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_RemainComponentForWOIssueWithParentInfo]'))
DROP VIEW [v_RemainComponentForWOIssueWithParentInfo]
GO
/****** Object:  View [dbo].[v_RemainWOForIssue]    Script Date: 05/06/2010 12:53:52 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_RemainWOForIssue]'))
DROP VIEW [v_RemainWOForIssue]
GO
/****** Object:  View [dbo].[v_WO_BOM_Planning]    Script Date: 05/06/2010 12:53:53 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_WO_BOM_Planning]'))
DROP VIEW [v_WO_BOM_Planning]
GO
/****** Object:  View [dbo].[V_WODetailAndProductInfo]    Script Date: 05/06/2010 12:53:53 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[V_WODetailAndProductInfo]'))
DROP VIEW [V_WODetailAndProductInfo]
GO
/****** Object:  View [dbo].[v_WORelease]    Script Date: 05/06/2010 12:53:53 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_WORelease]'))
DROP VIEW [v_WORelease]
GO
/****** Object:  View [dbo].[v_WOReversal]    Script Date: 05/06/2010 12:53:53 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_WOReversal]'))
DROP VIEW [v_WOReversal]
GO
/****** Object:  View [dbo].[v_WorkOrderForIssueMaterial]    Script Date: 05/06/2010 12:53:53 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_WorkOrderForIssueMaterial]'))
DROP VIEW [v_WorkOrderForIssueMaterial]
GO
/****** Object:  View [dbo].[V_LocationAndProductionLine]    Script Date: 05/06/2010 12:53:52 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[V_LocationAndProductionLine]'))
DROP VIEW [V_LocationAndProductionLine]
GO
/****** Object:  View [dbo].[v_MSTParty]    Script Date: 05/06/2010 12:53:52 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_MSTParty]'))
DROP VIEW [v_MSTParty]
GO
/****** Object:  View [dbo].[v_PartyWithCCN]    Script Date: 05/06/2010 12:53:52 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_PartyWithCCN]'))
DROP VIEW [v_PartyWithCCN]
GO
/****** Object:  View [dbo].[v_vendor]    Script Date: 05/06/2010 12:53:53 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_vendor]'))
DROP VIEW [v_vendor]
GO
/****** Object:  View [dbo].[V_VendorCustomer]    Script Date: 05/06/2010 12:53:53 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[V_VendorCustomer]'))
DROP VIEW [V_VendorCustomer]
GO
/****** Object:  View [dbo].[v_CategoryOfProductionLine]    Script Date: 05/06/2010 12:53:52 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_CategoryOfProductionLine]'))
DROP VIEW [v_CategoryOfProductionLine]
GO
/****** Object:  View [dbo].[v_SOPackListManagement]    Script Date: 05/06/2010 12:53:52 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_SOPackListManagement]'))
DROP VIEW [v_SOPackListManagement]
GO
/****** Object:  View [dbo].[V_LocalVendor]    Script Date: 05/06/2010 12:53:52 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[V_LocalVendor]'))
DROP VIEW [V_LocalVendor]
GO
/****** Object:  View [dbo].[v_LocationNoScecurity]    Script Date: 05/06/2010 12:53:52 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_LocationNoScecurity]'))
DROP VIEW [v_LocationNoScecurity]
GO
/****** Object:  View [dbo].[V_BinItem]    Script Date: 05/06/2010 12:53:52 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[V_BinItem]'))
DROP VIEW [V_BinItem]
GO
/****** Object:  View [dbo].[V_BinItemSumAvail]    Script Date: 05/06/2010 12:53:52 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[V_BinItemSumAvail]'))
DROP VIEW [V_BinItemSumAvail]
GO
/****** Object:  View [dbo].[v_DetailMaterialScrap]    Script Date: 05/06/2010 12:53:52 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_DetailMaterialScrap]'))
DROP VIEW [v_DetailMaterialScrap]
GO
/****** Object:  View [dbo].[v_GetSaleOrderTotalInvCommit]    Script Date: 05/06/2010 12:53:52 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_GetSaleOrderTotalInvCommit]'))
DROP VIEW [v_GetSaleOrderTotalInvCommit]
GO
/****** Object:  View [dbo].[v_InOutStockForAccounting]    Script Date: 05/06/2010 12:53:52 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_InOutStockForAccounting]'))
DROP VIEW [v_InOutStockForAccounting]
GO
/****** Object:  View [dbo].[v_InputInPeriod]    Script Date: 05/06/2010 12:53:52 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_InputInPeriod]'))
DROP VIEW [v_InputInPeriod]
GO
/****** Object:  View [dbo].[v_IssueMaterialDetail]    Script Date: 05/06/2010 12:53:52 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_IssueMaterialDetail]'))
DROP VIEW [v_IssueMaterialDetail]
GO
/****** Object:  View [dbo].[v_IVAdjustmentAndProduct]    Script Date: 05/06/2010 12:53:52 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_IVAdjustmentAndProduct]'))
DROP VIEW [v_IVAdjustmentAndProduct]
GO
/****** Object:  View [dbo].[v_PO_PurchaseOrderDetailReceipt]    Script Date: 05/06/2010 12:53:52 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_PO_PurchaseOrderDetailReceipt]'))
DROP VIEW [v_PO_PurchaseOrderDetailReceipt]
GO
/****** Object:  View [dbo].[v_TotalQuantityReturnToVendor]    Script Date: 05/06/2010 12:53:53 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_TotalQuantityReturnToVendor]'))
DROP VIEW [v_TotalQuantityReturnToVendor]
GO
/****** Object:  View [dbo].[V_PRODUCT_AVAILABLE_IN_BIN_INCOMING]    Script Date: 05/06/2010 12:53:52 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[V_PRODUCT_AVAILABLE_IN_BIN_INCOMING]'))
DROP VIEW [V_PRODUCT_AVAILABLE_IN_BIN_INCOMING]
GO
/****** Object:  View [dbo].[V_ProductInBinCache]    Script Date: 05/06/2010 12:53:52 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[V_ProductInBinCache]'))
DROP VIEW [V_ProductInBinCache]
GO
/****** Object:  View [dbo].[V_ProductInforWithInventory]    Script Date: 05/06/2010 12:53:52 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[V_ProductInforWithInventory]'))
DROP VIEW [V_ProductInforWithInventory]
GO
/****** Object:  View [dbo].[v_ProductInventoryAdjustment]    Script Date: 05/06/2010 12:53:52 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_ProductInventoryAdjustment]'))
DROP VIEW [v_ProductInventoryAdjustment]
GO
/****** Object:  View [dbo].[v_SearchProductForMaterialReceipt]    Script Date: 05/06/2010 12:53:52 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_SearchProductForMaterialReceipt]'))
DROP VIEW [v_SearchProductForMaterialReceipt]
GO
/****** Object:  View [dbo].[v_WOCompletion]    Script Date: 05/06/2010 12:53:53 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_WOCompletion]'))
DROP VIEW [v_WOCompletion]
GO
/****** Object:  View [dbo].[v_Invoice]    Script Date: 05/06/2010 12:53:52 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_Invoice]'))
DROP VIEW [v_Invoice]
GO
/****** Object:  View [dbo].[V_InvoiceMasterNotReceiving]    Script Date: 05/06/2010 12:53:52 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[V_InvoiceMasterNotReceiving]'))
DROP VIEW [V_InvoiceMasterNotReceiving]
GO
/****** Object:  View [dbo].[V_ProductForStockTaking]    Script Date: 05/06/2010 12:53:52 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[V_ProductForStockTaking]'))
DROP VIEW [V_ProductForStockTaking]
GO
/****** Object:  View [dbo].[V_ProductInLocCache]    Script Date: 05/06/2010 12:53:52 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[V_ProductInLocCache]'))
DROP VIEW [V_ProductInLocCache]
GO
/****** Object:  View [dbo].[V_LocationItem]    Script Date: 05/06/2010 12:53:52 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[V_LocationItem]'))
DROP VIEW [V_LocationItem]
GO
/****** Object:  View [dbo].[V_LocationItemSumAvail]    Script Date: 05/06/2010 12:53:52 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[V_LocationItemSumAvail]'))
DROP VIEW [V_LocationItemSumAvail]
GO
/****** Object:  View [dbo].[v_ITMCostCenterRate]    Script Date: 05/06/2010 12:53:52 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_ITMCostCenterRate]'))
DROP VIEW [v_ITMCostCenterRate]
GO
/****** Object:  View [dbo].[V_MasLocItemSumAvail]    Script Date: 05/06/2010 12:53:52 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[V_MasLocItemSumAvail]'))
DROP VIEW [V_MasLocItemSumAvail]
GO
/****** Object:  View [dbo].[v_MasterLocationItem]    Script Date: 05/06/2010 12:53:52 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_MasterLocationItem]'))
DROP VIEW [v_MasterLocationItem]
GO
/****** Object:  View [dbo].[V_SumActCost]    Script Date: 05/06/2010 12:53:52 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[V_SumActCost]'))
DROP VIEW [V_SumActCost]
GO
/****** Object:  View [dbo].[V_SumWOCompletionCost]    Script Date: 05/06/2010 12:53:53 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[V_SumWOCompletionCost]'))
DROP VIEW [V_SumWOCompletionCost]
GO
/****** Object:  View [dbo].[v_Total_DS_Before_Allocation]    Script Date: 05/06/2010 12:53:53 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_Total_DS_Before_Allocation]'))
DROP VIEW [v_Total_DS_Before_Allocation]
GO
/****** Object:  View [dbo].[v_UnitOfActualCost]    Script Date: 05/06/2010 12:53:53 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_UnitOfActualCost]'))
DROP VIEW [v_UnitOfActualCost]
GO
/****** Object:  View [dbo].[v_UnitOfActualCost_NotDS]    Script Date: 05/06/2010 12:53:53 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_UnitOfActualCost_NotDS]'))
DROP VIEW [v_UnitOfActualCost_NotDS]
GO
/****** Object:  View [dbo].[v_UnitOfActualCost1]    Script Date: 05/06/2010 12:53:53 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_UnitOfActualCost1]'))
DROP VIEW [v_UnitOfActualCost1]
GO
/****** Object:  View [dbo].[v_DS_Recycle_AfterAllocate]    Script Date: 05/06/2010 12:53:52 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_DS_Recycle_AfterAllocate]'))
DROP VIEW [v_DS_Recycle_AfterAllocate]
GO
/****** Object:  View [dbo].[v_WOCompletion_Begin_Cost]    Script Date: 05/06/2010 12:53:53 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_WOCompletion_Begin_Cost]'))
DROP VIEW [v_WOCompletion_Begin_Cost]
GO
/****** Object:  View [dbo].[v_LeafCostElement]    Script Date: 05/06/2010 12:53:52 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_LeafCostElement]'))
DROP VIEW [v_LeafCostElement]
GO
/****** Object:  View [dbo].[v_ActualCostHistory_ElementType]    Script Date: 05/06/2010 12:53:52 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_ActualCostHistory_ElementType]'))
DROP VIEW [v_ActualCostHistory_ElementType]
GO
/****** Object:  View [dbo].[v_AllocatedCost]    Script Date: 05/06/2010 12:53:52 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_AllocatedCost]'))
DROP VIEW [v_AllocatedCost]
GO
/****** Object:  View [dbo].[v_ShippingInMonthByCostElement_Rpt]    Script Date: 05/06/2010 12:53:52 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_ShippingInMonthByCostElement_Rpt]'))
DROP VIEW [v_ShippingInMonthByCostElement_Rpt]
GO
/****** Object:  View [dbo].[v_ReturnGoodsInMonthByCostElement]    Script Date: 05/06/2010 12:53:52 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_ReturnGoodsInMonthByCostElement]'))
DROP VIEW [v_ReturnGoodsInMonthByCostElement]
GO
/****** Object:  View [dbo].[v_DS_Recycle_AfterAllocateByElement]    Script Date: 05/06/2010 12:53:52 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_DS_Recycle_AfterAllocateByElement]'))
DROP VIEW [v_DS_Recycle_AfterAllocateByElement]
GO
/****** Object:  View [dbo].[v_ShippingInMonthByCostElement]    Script Date: 05/06/2010 12:53:52 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_ShippingInMonthByCostElement]'))
DROP VIEW [v_ShippingInMonthByCostElement]
GO
/****** Object:  View [dbo].[v_UnitOfActualCost_ByCostElement]    Script Date: 05/06/2010 12:53:53 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_UnitOfActualCost_ByCostElement]'))
DROP VIEW [v_UnitOfActualCost_ByCostElement]
GO
/****** Object:  View [dbo].[v_SumAllocated_GroupByProduct]    Script Date: 05/06/2010 12:53:53 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_SumAllocated_GroupByProduct]'))
DROP VIEW [v_SumAllocated_GroupByProduct]
GO
/****** Object:  View [dbo].[v_ProductWithProductionLineInfo]    Script Date: 05/06/2010 12:53:52 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_ProductWithProductionLineInfo]'))
DROP VIEW [v_ProductWithProductionLineInfo]
GO
/****** Object:  View [dbo].[v_ComponentScrap]    Script Date: 05/06/2010 12:53:52 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_ComponentScrap]'))
DROP VIEW [v_ComponentScrap]
GO
/****** Object:  View [dbo].[v_IssueMaterialDetailByProduct]    Script Date: 05/06/2010 12:53:52 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_IssueMaterialDetailByProduct]'))
DROP VIEW [v_IssueMaterialDetailByProduct]
GO
/****** Object:  View [dbo].[V_ProductForFreight]    Script Date: 05/06/2010 12:53:52 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[V_ProductForFreight]'))
DROP VIEW [V_ProductForFreight]
GO
/****** Object:  View [dbo].[V_ProductInfor]    Script Date: 05/06/2010 12:53:52 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[V_ProductInfor]'))
DROP VIEW [V_ProductInfor]
GO
/****** Object:  View [dbo].[v_SOToCommit]    Script Date: 05/06/2010 12:53:52 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_SOToCommit]'))
DROP VIEW [v_SOToCommit]
GO
/****** Object:  View [dbo].[v_WODetailInfor]    Script Date: 05/06/2010 12:53:53 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_WODetailInfor]'))
DROP VIEW [v_WODetailInfor]
GO
/****** Object:  View [dbo].[v_WorkOrderItemDetail]    Script Date: 05/06/2010 12:53:53 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_WorkOrderItemDetail]'))
DROP VIEW [v_WorkOrderItemDetail]
GO
/****** Object:  View [dbo].[v_WorkOrderCompletion]    Script Date: 05/06/2010 12:53:53 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_WorkOrderCompletion]'))
DROP VIEW [v_WorkOrderCompletion]
GO
/****** Object:  View [dbo].[v_WorkOrderDetail]    Script Date: 05/06/2010 12:53:53 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_WorkOrderDetail]'))
DROP VIEW [v_WorkOrderDetail]
GO
/****** Object:  View [dbo].[v_WorkOrderDetailRemainQuantity]    Script Date: 05/06/2010 12:53:53 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_WorkOrderDetailRemainQuantity]'))
DROP VIEW [v_WorkOrderDetailRemainQuantity]
GO
/****** Object:  View [dbo].[v_WO_MaterialIssue_Planning]    Script Date: 05/06/2010 12:53:53 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_WO_MaterialIssue_Planning]'))
DROP VIEW [v_WO_MaterialIssue_Planning]
GO
/****** Object:  View [dbo].[V_WOBOM]    Script Date: 05/06/2010 12:53:53 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[V_WOBOM]'))
DROP VIEW [V_WOBOM]
GO
/****** Object:  View [dbo].[v_PO_PurchaseOrderTotalReceive]    Script Date: 05/06/2010 12:53:52 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_PO_PurchaseOrderTotalReceive]'))
DROP VIEW [v_PO_PurchaseOrderTotalReceive]
GO
/****** Object:  View [dbo].[v_NeededQtyToIssueMaterial]    Script Date: 05/06/2010 12:53:52 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_NeededQtyToIssueMaterial]'))
DROP VIEW [v_NeededQtyToIssueMaterial]
GO
/****** Object:  View [dbo].[v_TransactionHistory]    Script Date: 05/06/2010 12:53:53 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_TransactionHistory]'))
DROP VIEW [v_TransactionHistory]
GO
/****** Object:  View [dbo].[v_CostElement_Material]    Script Date: 05/06/2010 12:53:52 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_CostElement_Material]'))
DROP VIEW [v_CostElement_Material]
GO
/****** Object:  View [dbo].[v_GetAvgCommitCost]    Script Date: 05/06/2010 12:53:52 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_GetAvgCommitCost]'))
DROP VIEW [v_GetAvgCommitCost]
GO
/****** Object:  View [dbo].[v_GetSaleOrderTotalCommit]    Script Date: 05/06/2010 12:53:52 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_GetSaleOrderTotalCommit]'))
DROP VIEW [v_GetSaleOrderTotalCommit]
GO
/****** Object:  View [dbo].[v_InOutStock]    Script Date: 05/06/2010 12:53:52 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_InOutStock]'))
DROP VIEW [v_InOutStock]
GO
/****** Object:  View [dbo].[v_LotByBin]    Script Date: 05/06/2010 12:53:52 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_LotByBin]'))
DROP VIEW [v_LotByBin]
GO
/****** Object:  View [dbo].[v_LotByLoc]    Script Date: 05/06/2010 12:53:52 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_LotByLoc]'))
DROP VIEW [v_LotByLoc]
GO
/****** Object:  View [dbo].[v_LotByWODetailAndProduct]    Script Date: 05/06/2010 12:53:52 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_LotByWODetailAndProduct]'))
DROP VIEW [v_LotByWODetailAndProduct]
GO
/****** Object:  View [dbo].[v_ModelList]    Script Date: 05/06/2010 12:53:52 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_ModelList]'))
DROP VIEW [v_ModelList]
GO
/****** Object:  View [dbo].[v_ITM_BOM_Planning]    Script Date: 05/06/2010 12:53:52 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_ITM_BOM_Planning]'))
DROP VIEW [v_ITM_BOM_Planning]
GO
/****** Object:  View [dbo].[v_ITMBOM_Product]    Script Date: 05/06/2010 12:53:52 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_ITMBOM_Product]'))
DROP VIEW [v_ITMBOM_Product]
GO
/****** Object:  View [dbo].[v_PRO_WOROUTINGBORL]    Script Date: 05/06/2010 12:53:52 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_PRO_WOROUTINGBORL]'))
DROP VIEW [v_PRO_WOROUTINGBORL]
GO
/****** Object:  View [dbo].[v_PRO_WOROUTINGBORM]    Script Date: 05/06/2010 12:53:52 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_PRO_WOROUTINGBORM]'))
DROP VIEW [v_PRO_WOROUTINGBORM]
GO
/****** Object:  View [dbo].[v_ProductRevision]    Script Date: 05/06/2010 12:53:52 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_ProductRevision]'))
DROP VIEW [v_ProductRevision]
GO
/****** Object:  View [dbo].[v_RecoverMaterialMaster]    Script Date: 05/06/2010 12:53:52 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_RecoverMaterialMaster]'))
DROP VIEW [v_RecoverMaterialMaster]
GO
/****** Object:  View [dbo].[v_RemainWOLineForIssue]    Script Date: 05/06/2010 12:53:52 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_RemainWOLineForIssue]'))
DROP VIEW [v_RemainWOLineForIssue]
GO
/****** Object:  View [dbo].[v_ReleasedWOWithRemainNeededQty]    Script Date: 05/06/2010 12:53:52 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_ReleasedWOWithRemainNeededQty]'))
DROP VIEW [v_ReleasedWOWithRemainNeededQty]
GO
/****** Object:  View [dbo].[V_MultiWOCompletion]    Script Date: 05/06/2010 12:53:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[V_MultiWOCompletion]'))
EXEC dbo.sp_executesql @statement = N'CREATE  VIEW [V_MultiWOCompletion]
AS
SELECT DISTINCT MultiCompletionNo, CCNID
FROM PRO_WorkOrderCompletion
WHERE MultiCompletionNo IS NOT NULL AND MultiCompletionNo <> ''''''''
'
GO
/****** Object:  View [dbo].[v_ReleasedWOWithRemainNeededQty]    Script Date: 05/06/2010 12:53:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_ReleasedWOWithRemainNeededQty]'))
EXEC dbo.sp_executesql @statement = N'CREATE  VIEW [v_ReleasedWOWithRemainNeededQty]
AS
(SELECT DISTINCT dbo.PRO_WorkOrderDetail.WorkOrderMasterID, 
       dbo.PRO_WorkOrderDetail.WorkOrderDetailID, 
       bom.ComponentID AS ProductID, bom.Shrink,  bom.Quantity BomQuantity,
       ((bom.Quantity * dbo.PRO_WorkOrderDetail.OrderQuantity) / ((100 - ISNULL(bom.Shrink, 0)) / 100))  as NeededQuantity,
       ISNULL(
       (bom.Quantity * dbo.PRO_WorkOrderDetail.OrderQuantity) / ((100 - ISNULL(bom.Shrink, 0)) / 100) 
       -
       ISNULL(Issue.CommitQuantity, 0)
       , 0) AS RemainNeededQuantity,
	ISNULL(Issue.CommitQuantity, 0) CommitedQuantity 
FROM    ITM_BOM bom INNER JOIN
        PRO_WorkOrderDetail ON bom.ProductID = PRO_WorkOrderDetail.ProductID
LEFT JOIN (SELECT WorkOrderDetailID, ProductID, SUM(CommitQuantity) CommitQuantity
        FROM PRO_IssueMaterialDetail
	GROUP BY WorkOrderDetailID, ProductID
       ) Issue
ON PRO_WorkOrderDetail.WorkOrderDetailID = Issue.WorkOrderDetailID
AND Issue.ProductID = bom.componentID
WHERE   (dbo.PRO_WorkOrderDetail.Status = 2))
'
GO
/****** Object:  View [dbo].[v_RemainWOLineForIssue]    Script Date: 05/06/2010 12:53:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_RemainWOLineForIssue]'))
EXEC dbo.sp_executesql @statement = N'CREATE VIEW [v_RemainWOLineForIssue]
AS
SELECT  PRO_WorkOrderDetail.WorkOrderMasterID,
 PRO_WorkOrderDetail.WorkOrderDetailID, 
        PRO_WorkOrderDetail.Line,
 ITM_Product.Code,
 PRO_WorkOrderDetail.OrderQuantity,
        PRO_WorkOrderDetail.StartDate,
 PRO_WorkOrderDetail.DueDate
 
FROM   PRO_WorkOrderDetail
 INNER JOIN ITM_Product ON PRO_WorkOrderDetail.ProductID = ITM_Product.ProductID
 
WHERE PRO_WorkOrderDetail.WorkOrderDetailID IN
 (SELECT WorkOrderDetailID 
 FROM v_ReleasedWOWithRemainNeededQty 
 WHERE RemainNeededQuantity > 0)
'
GO
/****** Object:  View [dbo].[v_RecoverMaterialMaster]    Script Date: 05/06/2010 12:53:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_RecoverMaterialMaster]'))
EXEC dbo.sp_executesql @statement = N'CREATE VIEW [v_RecoverMaterialMaster]
AS
SELECT      CST_RecoverMaterialMaster.RecoverMaterialMasterID, CST_RecoverMaterialMaster.PostDate, CST_RecoverMaterialMaster.TransNo, 
            CST_RecoverMaterialMaster.CCNID, CST_RecoverMaterialMaster.FromLocationID, CST_RecoverMaterialMaster.FromBinID, 
            CST_RecoverMaterialMaster.Quantity, CST_RecoverMaterialMaster.ProductID, CST_RecoverMaterialMaster.AvailableQty,
            ITM_Product.CategoryID
 FROM CST_RecoverMaterialMaster JOIN ITM_Product
 ON CST_RecoverMaterialMaster.ProductID = ITM_Product.ProductID
'
GO
/****** Object:  View [dbo].[v_ProductRevision]    Script Date: 05/06/2010 12:53:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_ProductRevision]'))
EXEC dbo.sp_executesql @statement = N'CREATE VIEW [v_ProductRevision]
AS
 SELECT DISTINCT TOP 100 PERCENT [Revision]
 FROM ITM_Product
 ORDER BY Revision
'
GO
/****** Object:  View [dbo].[v_ITMBOM_Product]    Script Date: 05/06/2010 12:53:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_ITMBOM_Product]'))
EXEC dbo.sp_executesql @statement = N'CREATE VIEW [v_ITMBOM_Product]
AS
SELECT DISTINCT dbo.ITM_Product.ProductID, dbo.ITM_Product.Code, dbo.ITM_Product.Description, dbo.ITM_Product.Revision,  
 dbo.ITM_Product.BOMDescription, dbo.ITM_Product.BomIncrement, dbo.ITM_Product.CategoryID,dbo.ITM_Product.CCNID,  
 dbo.ITM_Product.MakeItem, ITM_Product.PrimaryVendorID,
 HasBOM = CASE
          ( SELECT     COUNT(*)
            FROM          ITM_Product A INNER JOIN
                                ITM_BOM ON ITM_Product.ProductID = ITM_BOM.ProductID
             WHERE  A.ProductID = ITM_Product.ProductID ) WHEN 0 THEN 0 ELSE 1 END
	FROM ITM_Product
'
GO
/****** Object:  View [dbo].[v_ITM_BOM_Planning]    Script Date: 05/06/2010 12:53:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_ITM_BOM_Planning]'))
EXEC dbo.sp_executesql @statement = N'CREATE VIEW [v_ITM_BOM_Planning] 
AS 
SELECT     b1.componentID, a1.ProductID, a1.Code, a1.Revision, a1.Description, a1.PartNumber, a1.LTFixedTime, a1.LTVariableTime, a1.LTOrderPrepare, 
                      a1.LTShippingPrepare, a1.LTSalesATP, b1.quantity, b1.EffectiveBeginDate, b1.EffectiveEndDate, b1.Shrink, b1.LeadTimeOffset
FROM         dbo.ITM_Product a1 INNER JOIN
                          (SELECT     c.ProductID, c.componentID, c.quantity, c.EffectiveBeginDate, c.EffectiveEndDate, c.Shrink, c.LeadTimeOffset
                            FROM          itm_Product a, ITM_BOM c
                            WHERE      a.ProductID = c.componentID) b1 ON a1.ProductID = b1.ProductID
'
GO
/****** Object:  View [dbo].[v_ModelList]    Script Date: 05/06/2010 12:53:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_ModelList]'))
EXEC dbo.sp_executesql @statement = N'CREATE VIEW [v_ModelList]
AS
 Select Distinct top 100 percent 
 ITM_Product.Revision from ITM_Product
 Order By ITM_Product.Revision
'
GO
/****** Object:  View [dbo].[v_LotByWODetailAndProduct]    Script Date: 05/06/2010 12:53:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_LotByWODetailAndProduct]'))
EXEC dbo.sp_executesql @statement = N'CREATE VIEW [v_LotByWODetailAndProduct]
AS 
SELECT D.Lot, SUM(D.CommitQuantity) as AvailableQuantity, D.ProductID, M.WorkOrderDetailID 
FROM PRO_IssueMaterialDetail D,  PRO_IssueMaterialMaster M 
WHERE M.IssueMaterialMasterID = D.IssueMaterialMasterID 
GROUP BY D.Lot, D.ProductID, M.WorkOrderDetailID 
HAVING SUM(D.CommitQuantity)>0
'
GO
/****** Object:  View [dbo].[v_LotByLoc]    Script Date: 05/06/2010 12:53:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_LotByLoc]'))
EXEC dbo.sp_executesql @statement = N'CREATE VIEW [v_LotByLoc] AS
 SELECT      LC.Lot, 
             P.LotControl,
             LC.LocationID,   
             (LC.OHQuantity - LC.CommitQuantity) AS AvailQuantity, 
             LC.ProductID, 
             LC.MasterLocationID
 FROM IV_LocationCache LC
             INNER JOIN ITM_Product p ON P.ProductID=LC.ProductID
 WHERE (LC.OHQuantity - LC.CommitQuantity) > 0
'
GO
/****** Object:  View [dbo].[v_LotByBin]    Script Date: 05/06/2010 12:53:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_LotByBin]'))
EXEC dbo.sp_executesql @statement = N'CREATE VIEW [v_LotByBin] AS
 SELECT      BC.Lot, 
             P.LotControl,
             BC.ProductID, 
             BC.MasterLocationID, 
             BC.LocationID, 
             BC.BinID, 
             (BC.OHQuantity - BC.CommitQuantity) AS IssueQuantity
 FROM IV_BinCache BC
             INNER JOIN ITM_Product p ON P.ProductID = BC.ProductID
 WHERE (BC.OHQuantity - BC.CommitQuantity) > 0
'
GO
/****** Object:  View [dbo].[v_InOutStock]    Script Date: 05/06/2010 12:53:52 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_InOutStock]'))
EXEC dbo.sp_executesql @statement = N'Create view [v_InOutStock] as
select DatePart(Year,PostDate)Years,DatePart(Month,PostDate)Months,CONVERT(VARCHAR(10), PostDate, 105) Days,
TrantypeID,LocationID,BinID,ProductID,Sum(Quantity)OUTQty,0 INQty,
0 as BeginQty
from MST_TransactionHistory
WHERE Quantity>=0
and TranTypeID IN (12,22)
Group by DatePart(Year,PostDate),DatePart(Month,PostDate),CONVERT(VARCHAR(10), PostDate, 105),
TrantypeID,LocationID,BinID,ProductID
UNION
select DatePart(Year,PostDate)Years,DatePart(Month,PostDate)Months,CONVERT(VARCHAR(10), PostDate, 105) Days,
TrantypeID,LocationID,BinID,ProductID,Sum(-Quantity)OUTQty,0 INQty,
0 as BeginQty
from MST_TransactionHistory
WHERE Quantity<0
and TranTypeID IN (17,19,21,24,25)
Group by DatePart(Year,PostDate),DatePart(Month,PostDate),CONVERT(VARCHAR(10), PostDate, 105),
TrantypeID,LocationID,BinID,ProductID
UNION
select DatePart(Year,PostDate)Years,DatePart(Month,PostDate)Months,CONVERT(VARCHAR(10), PostDate, 105) Days,
TrantypeID,LocationID,BinID,ProductID,0 OUTQty,Sum(Quantity)INQty,
0 as BeginQty
from MST_TransactionHistory
WHERE Quantity>=0
and TranTypeID IN (17,19,21,24,25)
Group by DatePart(Year,PostDate),DatePart(Month,PostDate),CONVERT(VARCHAR(10), PostDate, 105),
TrantypeID,LocationID,BinID,ProductID
'
GO
/****** Object:  View [dbo].[v_GetSaleOrderTotalCommit]    Script Date: 05/06/2010 12:53:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_GetSaleOrderTotalCommit]'))
EXEC dbo.sp_executesql @statement = N'CREATE VIEW [v_GetSaleOrderTotalCommit]
AS
SELECT     dbo.SO_SaleOrderDetail.SaleOrderDetailID, dbo.SO_SaleOrderDetail.ProductID, dbo.ITM_Product.Description, dbo.ITM_Product.Revision, 
                      dbo.SO_SaleOrderDetail.SellingUMID, dbo.SO_SaleOrderDetail.SaleOrderMasterID, SaleOrderCommit.TotalCommit, SaleOrderCommit.AVGCost, 
                      SaleOrderCommit.Lot, SaleOrderCommit.Serial, dbo.ITM_Product.Code AS ProductCode
FROM         dbo.SO_SaleOrderDetail INNER JOIN
                          (SELECT     a.SaleOrderDetailID, Lot, Serial, SUM(b.CommitQuantity) AS TotalCommit, AVG(b.CostOfGoodsSold * CommitQuantity) AS AVGCost
                            FROM          SO_DeliverySchedule a INNER JOIN
                                                   SO_CommitInventoryDetail b ON a.DeliveryScheduleID = b.DeliveryScheduleID
                            GROUP BY a.SaleOrderDetailID, b.Lot, b.Serial) SaleOrderCommit ON 
                      dbo.SO_SaleOrderDetail.SaleOrderDetailID = SaleOrderCommit.SaleOrderDetailID INNER JOIN
                      dbo.ITM_Product ON dbo.SO_SaleOrderDetail.ProductID = dbo.ITM_Product.ProductID
'
GO
/****** Object:  View [dbo].[v_GetAvgCommitCost]    Script Date: 05/06/2010 12:53:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_GetAvgCommitCost]'))
EXEC dbo.sp_executesql @statement = N'create view [v_GetAvgCommitCost]
as
(
SELECT SaleOrderMasterID, ProductID, AVG(AVGCost)AVGCost
FROM   v_GetSaleOrderTotalCommit 
GROUP BY SaleOrderMasterID, ProductID
)
'
GO
/****** Object:  View [dbo].[v_CostElement_Material]    Script Date: 05/06/2010 12:53:52 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_CostElement_Material]'))
EXEC dbo.sp_executesql @statement = N'Create view [v_CostElement_Material] as
select ProductID,ActCostAllocationMasterID,ISNULL(Quantity,0)-ISNULL(BeginQuantity,0)AS CompletedQty,
ISNULL(Sum(ISNULL(ComponentValue,0)),0)
+ ISNULL((Select ISNULL(Sum(Isnull(ActualCost,0)),0) from cst_ActualCostHistory where ProductID=A1.ProductID AND
ActCostAllocationMasterID=A1.ActCostAllocationMasterID AND
Quantity=A1.Quantity AND
BeginQuantity=A1.BeginQuantity AND
ProductID IN (select ProductID FROM ITM_Product where MakeItem=0)),0)
as Material,Quantity,BeginQuantity
from cst_ActualCostHistory as A1
Group by ActCostAllocationMasterID,ProductID,Quantity,BeginQuantity
'
GO
/****** Object:  View [dbo].[v_TransactionHistory]    Script Date: 05/06/2010 12:53:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_TransactionHistory]'))
EXEC dbo.sp_executesql @statement = N'CREATE VIEW [v_TransactionHistory]
AS
SELECT  CCNID,
            ProductID,
            MasterLocationID, 
            LocationID,
            BinID,
            TransDate,
            PostDate,
            TranTypeID,
           SUM(Quantity) as TransQuantity 
FROM    MST_TransactionHistory
GROUP BY CCNID,
            ProductID,
            MasterLocationID,
            LocationID,
            BinID,
            TransDate,
            PostDate,
            TranTypeID
'
GO
/****** Object:  View [dbo].[v_PO_PurchaseOrderTotalReceive]    Script Date: 05/06/2010 12:53:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_PO_PurchaseOrderTotalReceive]'))
EXEC dbo.sp_executesql @statement = N'CREATE VIEW [v_PO_PurchaseOrderTotalReceive]
AS
SELECT     PurchaseOrderMasterID, PurchaseOrderDetailID, ProductID, SUM(ReceiveQuantity) AS TotalReceive, Lot, Serial
FROM         dbo.PO_PurchaseOrderReceiptDetail
GROUP BY PurchaseOrderMasterID, PurchaseOrderDetailID, ProductID, Lot, Serial
'
GO
/****** Object:  View [dbo].[v_WO_MaterialIssue_Planning]    Script Date: 05/06/2010 12:53:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_WO_MaterialIssue_Planning]'))
EXEC dbo.sp_executesql @statement = N'CREATE VIEW [v_WO_MaterialIssue_Planning] 
AS
SELECT Sum(dbo.PRO_IssueMaterialDetail.CommitQuantity) as IssueQtity ,dbo.PRO_IssueMaterialDetail.ProductID,  
       dbo.PRO_IssueMaterialMaster.PostDate, dbo.PRO_IssueMaterialMaster.MasterLocationID , dbo.PRO_IssueMaterialMaster.IssueNo, 
       dbo.PRO_IssueMaterialMaster.WorkOrderDetailID
FROM   dbo.PRO_IssueMaterialDetail INNER JOIN
       dbo.PRO_IssueMaterialMaster ON 
       dbo.PRO_IssueMaterialDetail.IssueMaterialMasterID = dbo.PRO_IssueMaterialMaster.IssueMaterialMasterID
GROUP BY                  
	dbo.PRO_IssueMaterialDetail.ProductID,  
        dbo.PRO_IssueMaterialMaster.PostDate, dbo.PRO_IssueMaterialMaster.MasterLocationID , dbo.PRO_IssueMaterialMaster.IssueNo, 
        dbo.PRO_IssueMaterialMaster.WorkOrderDetailID
'
GO
/****** Object:  View [dbo].[v_WorkOrderDetailRemainQuantity]    Script Date: 05/06/2010 12:53:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_WorkOrderDetailRemainQuantity]'))
EXEC dbo.sp_executesql @statement = N'CREATE VIEW [v_WorkOrderDetailRemainQuantity]
AS
SELECT            Line, EstCst,ProductID, OrderQuantity, OrderQuantity - 
            isnull((SELECT SUM(CompletedQuantity) FROM PRO_WorkOrderCompletion
            WHERE WorkOrderDetailID = PRO_WorkOrderDetail.WorkOrderDetailID
            AND WorkOrderMasterID = PRO_WorkOrderDetail.WorkOrderMasterID),0) AS RemainQuantity,
            WorkOrderDetailID, WorkOrderMasterID
FROM   PRO_WorkOrderDetail
WHERE            Status = 2
'
GO
/****** Object:  View [dbo].[v_WorkOrderDetail]    Script Date: 05/06/2010 12:53:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_WorkOrderDetail]'))
EXEC dbo.sp_executesql @statement = N'CREATE VIEW [v_WorkOrderDetail]
AS
 SELECT            PRO_WorkOrderDetail.*, ITM_Product.CategoryID
 FROM   PRO_WorkOrderDetail INNER JOIN
	ITM_Product ON PRO_WorkOrderDetail.ProductID = ITM_Product.ProductID
'
GO
/****** Object:  View [dbo].[v_WorkOrderCompletion]    Script Date: 05/06/2010 12:53:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_WorkOrderCompletion]'))
EXEC dbo.sp_executesql @statement = N'CREATE VIEW [v_WorkOrderCompletion]
AS
 SELECT WOC.WorkOrderCompletionID, WOC.PostDate, WOC.WOCompletionNo, WOC.CompletedQuantity, WOC.Lot, WOC.Serial, WOC.LocationID, WOC.BinID, WOC.CCNID, WOC.MasterLocationID, WOC.ProductID, WOC.StockUMID, WOC.WorkOrderMasterID, WOC.WorkOrderDetailID, WOC.QAStatus, WOC.UserName, WOC.LastChange, WOC.IssuePurposeID, WOC.ShiftID, WOC.Remark, P.CategoryID
 FROM PRO_WorkOrderCompletion WOC
 INNER JOIN ITM_Product P ON WOC.ProductID=P.ProductID
'
GO
/****** Object:  View [dbo].[v_WorkOrderItemDetail]    Script Date: 05/06/2010 12:53:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_WorkOrderItemDetail]'))
EXEC dbo.sp_executesql @statement = N'CREATE VIEW [v_WorkOrderItemDetail] 
AS
SELECT PRO_WorkOrderDetail.WorkOrderMasterID,   
	 PRO_WorkOrderDetail.WorkOrderDetailID, 
	 PRO_WorkOrderDetail.Line as PRO_WorkOrderDetailLine, 
	 PRO_WorkOrderDetail.OrderQuantity, 
	 PRO_WorkOrderDetail.Status, 
	 PRO_WorkOrderDetail.StartDate, 
         PRO_WorkOrderDetail.DueDate,
	 ITM_Product.Code as ITM_ProductCode, 
         ITM_Product.Revision as ITM_ProductRevision,
	 ITM_Product.CostMethod,
	 ITM_Product.ProductID,
	 ITM_Product.PartNumber,
         MST_UnitOfMeasure.Code AS MST_UnitOfMeasureCode,
	 MST_UnitOfMeasure.Description as MST_UnitOfMeasureDescription,
	 PRO_WorkOrderDetail.StockUMID 
FROM    ITM_Product
	 INNER JOIN PRO_WorkOrderDetail ON ITM_Product.ProductID = PRO_WorkOrderDetail.ProductID 
	 LEFT JOIN  MST_UnitOfMeasure ON ITM_Product.StockUMID = MST_UnitOfMeasure.UnitOfMeasureID
'
GO
/****** Object:  View [dbo].[v_WODetailInfor]    Script Date: 05/06/2010 12:53:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_WODetailInfor]'))
EXEC dbo.sp_executesql @statement = N'CREATE VIEW [v_WODetailInfor] 
AS 
SELECT  wd.WorkOrderMasterID,
             wd.WorkOrderDetailID, 
              wd.Line, 
              wd.OrderQuantity, 
              wd.Status, 
              wd.StartDate, 
              wd.DueDate, 
              p.Code AS PartNumber, 
              p.Description AS PartName, 
              p.CostMethod, 
              p.ProductID, 
              p.Revision AS Model,
              p.StockUMID, 
  	      u.Code AS UM 
FROM   ITM_Product p, 
PRO_WorkOrderDetail wd, 
MST_UnitOfMeasure u 
WHERE p.ProductID = wd.ProductID AND p.StockUMID = u.UnitOfMeasureID 
'
GO
/****** Object:  View [dbo].[v_SOToCommit]    Script Date: 05/06/2010 12:53:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_SOToCommit]'))
EXEC dbo.sp_executesql @statement = N'CREATE VIEW [v_SOToCommit]
AS
SELECT            SO_SaleOrderDetail.SaleOrderDetailID, SaleOrderLine AS ''Line'', AutoCommit, 
            SO_DeliverySchedule.DeliveryQuantity - 
            (SELECT ISNULL(SUM(CommitQuantity), 0) FROM SO_CommitInventoryDetail
            WHERE SO_CommitInventoryDetail.DeliveryScheduleID = SO_DeliverySchedule.DeliveryScheduleID)
            AS ''CommitQuantity'',
            UnitPrice, VATAmount, ExportTaxAmount, SpecialTaxAmount, TotalAmount, 
            DiscountAmount, NetAmount, ItemCustomerCode, ItemCustomerRevision, 
            CancelReasonID, VATPercent, ExportTaxPercent, SpecialTaxPercent, 
            SO_SaleOrderDetail.UMRate, ShipQuantity, BackOrderQty, StockQuantity, SO_SaleOrderDetail.ProductID, 
            ConvertedQuantity, ReasonID, SaleOrderMasterID, 
            SO_SaleOrderDetail.SellingUMID, SO_SaleOrderDetail.StockUMID,
            SO_DeliverySchedule.DeliveryScheduleID, SO_DeliverySchedule.ScheduleDate,
            ITM_Product.Code AS ''ITM_ProductCode'', ITM_Product.Description, ITM_Product.Revision,
            MST_UnitOfMeasure.Code AS ''MST_UnitOfMeasureCode''
FROM
ITM_Product JOIN SO_SaleOrderDetail
            ON ITM_Product.ProductID = SO_SaleOrderDetail.ProductID
JOIN SO_DeliverySchedule
            ON SO_SaleOrderDetail.SaleOrderDetailID = SO_DeliverySchedule.SaleOrderDetailID
JOIN MST_UnitOfMeasure ON MST_UnitOfMeasure.UnitOfMeasureID = SO_SaleOrderDetail.SellingUMID
WHERE
SO_DeliverySchedule.DeliveryQuantity - 
           (SELECT ISNULL(SUM(CommitQuantity), 0) FROM SO_CommitInventoryDetail
         WHERE SO_CommitInventoryDetail.DeliveryScheduleID = SO_DeliverySchedule.DeliveryScheduleID) > 0


'
GO
/****** Object:  View [dbo].[V_ProductInfor]    Script Date: 05/06/2010 12:53:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[V_ProductInfor]'))
EXEC dbo.sp_executesql @statement = N'CREATE VIEW [V_ProductInfor]
AS
SELECT     p.Code AS PartNumber, p.Description AS PartName, p.MakeItem, 
p.CCNID, p.Revision AS Model, p.MasterLocationID, p.LocationID, p.BinID, 
p.StockUMID, u.Code AS UM, p.QAStatus, p.LotControl, p.ProductID, P.CategoryID
,BOM.ProductID ParentProductID 
FROM dbo.ITM_Product p 
INNER JOIN dbo.MST_UnitOfMeasure u ON p.StockUMID = u.UnitOfMeasureID and P.MakeItem=1
LEFT JOIN ITM_BOM BOM ON BOM.ComponentID = p.ProductID
--where BOM.ProductID is not null
union
SELECT     p.Code AS PartNumber, p.Description AS PartName, p.MakeItem, 
p.CCNID, p.Revision AS Model, p.MasterLocationID, p.LocationID, p.BinID, 
p.StockUMID, u.Code AS UM, p.QAStatus, p.LotControl, p.ProductID, P.CategoryID
,0 ParentProductID 
FROM dbo.ITM_Product p 
INNER JOIN dbo.MST_UnitOfMeasure u ON p.StockUMID = u.UnitOfMeasureID
AND P.MakeItem=0
'
GO
/****** Object:  View [dbo].[V_ProductForFreight]    Script Date: 05/06/2010 12:53:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[V_ProductForFreight]'))
EXEC dbo.sp_executesql @statement = N'CREATE VIEW [V_ProductForFreight]
AS
SELECT     P.ProductID, P.Code, P.Revision, P.Description, P.VAT, P.StockUMID, UM.Code AS MST_UnitOfMeasureCode
FROM       dbo.ITM_Product P INNER JOIN
	dbo.MST_UnitOfMeasure UM ON UM.UnitOfMeasureID = P.StockUMID
'
GO
/****** Object:  View [dbo].[v_IssueMaterialDetailByProduct]    Script Date: 05/06/2010 12:53:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_IssueMaterialDetailByProduct]'))
EXEC dbo.sp_executesql @statement = N'CREATE VIEW [v_IssueMaterialDetailByProduct]
AS
SELECT DISTINCT 
	ISD.ProductID, UOM.Code AS UM, P.Code, P.Description, P.Revision, ISD.WorkOrderDetailID, ISD.Lot, ISD.Serial, ISD.QAStatus, 
	ISD.IssueMaterialDetailID, ISD.StockUMID, ISM.IssueNo
FROM   	dbo.PRO_IssueMaterialDetail ISD INNER JOIN
	dbo.ITM_Product P ON ISD.ProductID = P.ProductID LEFT OUTER JOIN
	dbo.MST_UnitOfMeasure UOM ON ISD.StockUMID = UOM.UnitOfMeasureID INNER JOIN
	dbo.PRO_IssueMaterialMaster ISM ON ISM.IssueMaterialMasterID = ISD.IssueMaterialMasterID
'
GO
/****** Object:  View [dbo].[v_ComponentScrap]    Script Date: 05/06/2010 12:53:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_ComponentScrap]'))
EXEC dbo.sp_executesql @statement = N'CREATE  VIEW [v_ComponentScrap] AS
SELECT DISTINCT 
                      WOD.WorkOrderDetailID, BOMD.ComponentID, WOD.ProductID, ISD.CommitQuantity - ISNULL(CSD.ScrapQuantity, 0) 
                      - ISNULL(WOC.CompletedQuantity * BOMD.Quantity, 0) AS AvailableQuantity, 
                      P.Code AS ITM_ProductCode, P.Description AS ITM_ProductDescription, P.Revision AS ITM_ProductRevision, 
                      UOM.Code AS MST_UnitOfMeasureCode, P.StockUMID
FROM         ITM_BOM BOMD INNER JOIN
                      dbo.PRO_WorkOrderDetail WOD ON BOMD.ProductID = WOD.ProductID INNER JOIN
                      dbo.ITM_Product P ON P.ProductID = BOMD.ComponentID INNER JOIN
                      dbo.MST_UnitOfMeasure UOM ON UOM.UnitOfMeasureID = P.StockUMID LEFT OUTER JOIN
                          (SELECT     ISD.WorkOrderDetailID, ISD.ProductID ComponentID, SUM(CommitQuantity) CommitQuantity
                            FROM          dbo.PRO_IssueMaterialDetail ISD
                            GROUP BY ISD.WorkOrderDetailID, ISD.ProductID) ISD ON ISD.WorkOrderDetailID = WOD.WorkOrderDetailID AND 
                      ISD.ComponentID = BOMD.ComponentID LEFT OUTER JOIN
                          (SELECT     CSD.WorkOrderDetailID, CSD.ComponentID, SUM(ScrapQuantity) ScrapQuantity
                            FROM          dbo.PRO_ComponentScrapDetail CSD
                            GROUP BY CSD.WorkOrderDetailID, CSD.ComponentID) CSD ON CSD.WorkOrderDetailID = WOD.WorkOrderDetailID AND 
                      CSD.ComponentID = BOMD.ComponentID LEFT OUTER JOIN
                          (SELECT     WOC.WorkOrderDetailID, SUM(CompletedQuantity) CompletedQuantity
                            FROM          dbo.PRO_WorkOrderCompletion WOC
           GROUP BY WOC.WorkOrderDetailID) WOC ON WOC.WorkOrderDetailID = WOD.WorkOrderDetailID
'
GO
/****** Object:  View [dbo].[v_ProductWithProductionLineInfo]    Script Date: 05/06/2010 12:53:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_ProductWithProductionLineInfo]'))
EXEC dbo.sp_executesql @statement = N'CREATE VIEW [v_ProductWithProductionLineInfo]
AS
   SELECT  ITM_Product.ProductID,
   ITM_Product.Code,
   ITM_Product.Revision,
   ITM_Product.Description,
   ITM_Product.MakeItem,
   ITM_Category.Code AS ITM_CategoryCode,
   ITM_Product.ProductionLineID,
   ITM_Category.CategoryID
FROM    ITM_Product
   LEFT JOIN ITM_Category  ON ITM_Category.CategoryID = ITM_Product.CategoryID
WHERE ITM_Product.ProductionLineID IS NOT NULL
'
GO
/****** Object:  View [dbo].[v_SumAllocated_GroupByProduct]    Script Date: 05/06/2010 12:53:53 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_SumAllocated_GroupByProduct]'))
EXEC dbo.sp_executesql @statement = N'Create View  [v_SumAllocated_GroupByProduct] as
select CA.ActCostAllocationMasterID,CA.ProductID,
    SUM(CASE CT.CostElementTypeID WHEN 1 THEN Amount ELSE 0 END) AS Machine ,
    SUM(CASE CT.CostElementTypeID WHEN 2 THEN Amount ELSE 0 END) AS Labor,
    SUM(CASE CT.CostElementTypeID WHEN 3 THEN Amount ELSE 0 END) AS Material,
    SUM(CASE CT.CostElementTypeID WHEN 4 THEN Amount ELSE 0 END) AS SubMaterial,
    SUM(CASE CT.CostElementTypeID WHEN 5 THEN Amount ELSE 0 END) AS OverHead
from cst_AllocationResult CA
INNER JOIN STD_CostElement as CE ON CE.CostElementID=CA.CostElementID
INNER JOIN STD_CostElementType as CT ON CT.CostElementTypeid=CE.CostElementTypeid
Group by CA.ActCostAllocationMasterID,CA.ProductID
'
GO
/****** Object:  View [dbo].[v_UnitOfActualCost_ByCostElement]    Script Date: 05/06/2010 12:53:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_UnitOfActualCost_ByCostElement]'))
EXEC dbo.sp_executesql @statement = N'CREATE view [v_UnitOfActualCost_ByCostElement] as
select C.productID,I.CategoryID,C.CostElementID,C.ActualCost,C.ActCostAllocationMasterID,P.FromDate,P.ToDate,
C.RecycleAmount,C.DS_OKAmount,C.DSAmount,C.AdjustAmount
from CST_ActualCostHistory C
inner join cst_ActCostAllocationMaster P ON P.ActCostAllocationMasterID=C.ActCostAllocationMasterID
inner JOIN ITM_PRODUCT I ON I.Productid=c.productid
'
GO
/****** Object:  View [dbo].[v_ShippingInMonthByCostElement]    Script Date: 05/06/2010 12:53:52 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_ShippingInMonthByCostElement]'))
EXEC dbo.sp_executesql @statement = N'Create view [v_ShippingInMonthByCostElement] as
SELECT d.ProductID,SUM(ISNULL(D.InvoiceQty,0))as ShippedQty,
SUM(ISNULL(D.InvoiceQty,0)*ISNULL(D.Price,0)*ISNULL(M.ExchangeRate,0))AS ShippedAmount,
SUM(ISNULL(D.InvoiceQty,0)*ISNULL(C.ActualCost,0)) AS CGS1,
DATEPART(month,M.ShippedDate)as InMonth,DATEPART(Year,M.ShippedDate)as InYear,
SUM(C.ActualCost) ActualCost,C.FromDate,C.ToDate,ISNULL(C.CostElementID,0) CostElementID
FROM SO_ConfirmShipDetail D
INNER JOIN SO_ConfirmShipMaster M ON M.ConfirmShipMasterID=d.ConfirmShipMasterID
LEFT JOIN v_UnitOfActualCost_ByCostElement C
            ON C.ProductID=D.ProductID
            AND (M.ShippedDate BETWEEN C.FromDate AND (C.ToDate+1))
GROUP BY DATEPART(Year,M.ShippedDate), DATEPART(month, M.ShippedDate),
d.ProductID, C.FromDate,C.ToDate,C.CostElementID
'
GO
/****** Object:  View [dbo].[v_DS_Recycle_AfterAllocateByElement]    Script Date: 05/06/2010 12:53:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_DS_Recycle_AfterAllocateByElement]'))
EXEC dbo.sp_executesql @statement = N'Create view [v_DS_Recycle_AfterAllocateByElement] as
select ProductID,Sum(D.DSAmount+D.OH_DSAmount) AS DSAmount,
SUM(D.RecycleAmount+D.OH_RecycleAmount) as RecycleAmount,Sum(D.AdjustAmount+D.OH_AdjustAmount) as AdjustAmount,
D.ActCostAllocationMasterID,D.CostElementID,P.FromDate,P.ToDate
from CST_DSAndRecycleAllocation D
INNER JOIN cst_ActCostAllocationMaster P ON P.ActCostAllocationMasterID=D.ActCostAllocationMasterID
Group by D.ProductID,D.CostElementID,D.ActCostAllocationMasterID,P.FromDate,P.ToDate
'
GO
/****** Object:  View [dbo].[v_ReturnGoodsInMonthByCostElement]    Script Date: 05/06/2010 12:53:52 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_ReturnGoodsInMonthByCostElement]'))
EXEC dbo.sp_executesql @statement = N'Create view [v_ReturnGoodsInMonthByCostElement] as
Select Datepart(year,M.PostDate)as Year,Datepart(month,M.PostDate)as month,D.ProductID,Sum(D.ReceiveQuantity)as RTQuantity,
Sum(D.ReceiveQuantity*D.UnitPrice*M.ExchangeRate) as RTAmount,
Sum(D.ReceiveQuantity*C.ActualCost)as CGS1,
C.ActualCost,C.FromDate,C.ToDate,C.CostElementID
from SO_ReturnedGoodsDetail D
INNER JOIN SO_ReturnedGoodsMaster M ON D.ReturnedGoodsMasterID=M.ReturnedGoodsMasterID
LEFT JOIN v_UnitOfActualCost_ByCostElement C
	ON C.ProductID=D.ProductID
	AND (M.PostDate >= C.FromDate AND M.PostDate < C.ToDate+1)
Group by Datepart(year,M.PostDate),Datepart(month,M.PostDate),d.ProductID,C.ActualCost,C.FromDate,C.ToDate,C.CostElementID
'
GO
/****** Object:  View [dbo].[v_ShippingInMonthByCostElement_Rpt]    Script Date: 05/06/2010 12:53:52 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_ShippingInMonthByCostElement_Rpt]'))
EXEC dbo.sp_executesql @statement = N'CREATE View [v_ShippingInMonthByCostElement_Rpt] as
SELECT DISTINCT P.ProductID,P.Code as PartNo,P.Description as PartName,P.Revision as Model,P.StockUMID,
UM.Code as UM,P.CategoryID,CAT.Code as Category,CE.CostElementTypeID,
 (ISNULL(ShippedQty,0) - ISNULL(RTQuantity,0)) SellingQty,
(ISNULL(S.CGS1,0) - ISNULL(R.CGS1,0)) AS CGS1,
(AD.DSAmount-AD.RecycleAmount-AD.AdjustAmount)as Adjustment,
S.InMonth, S.InYear, S.CostElementID,
ISNULL(S.ShippedAmount,0)-ISNULL(RTAmount,0) as Revenue
FROM ITM_Product P
LEFT JOIN v_ShippingInMonthByCostElement S ON P.ProductID = S.ProductID
LEFT JOIN v_ReturnGoodsInMonthByCostElement R ON P.ProductID = R.ProductID
AND S.InMonth = R.[Month]
AND S.InYear = R.[Year]
AND S.CostElementID = R.CostElementID
LEFT JOIN v_DS_Recycle_AfterAllocateByElement AD
ON P.ProductID = AD.ProductID
AND S.CostElementID = AD.CostElementID
AND S.FromDate = AD.FromDate
AND S.ToDate = AD.ToDate
LEFT JOIN ITM_Category CAT on P.CategoryID=CAT.CategoryID
LEFT JOIN STD_CostElement CE ON S.CostElementID=CE.CostElementID
INNER JOIN MST_UnitOfMeasure UM ON UM.UnitOfMeasureID=P.StockUMID
WHERE S.CGS1 IS NOT NULL OR R.CGS1 IS NOT NULL
'
GO
/****** Object:  View [dbo].[v_AllocatedCost]    Script Date: 05/06/2010 12:53:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_AllocatedCost]'))
EXEC dbo.sp_executesql @statement = N'CREATE VIEW  [v_AllocatedCost] AS
(
SELECT C.ProductID,I.Code AS ProductCode,I.Description AS ProductName,I.Revision AS Model,
C.CostElementID,E.Name AS CostElement, Sum(C.Amount) AS AllocatedAmount,C.CompletedQuantity,
(Sum(C.Amount)/C.CompletedQuantity) AS UnitCost,ActCostAllocationMasterID
FROM CST_AllocationResult AS C
INNER JOIN ITM_Product I ON C.ProductID=I.ProductID
INNER JOIN STD_CostElement E ON C.CostElementID=E.CostElementID
GROUP BY C.CostElementID,E.Name,C.ProductID,I.Code,I.Description ,I.Revision,
C.CompletedQuantity,ActCostAllocationMasterID
)
'
GO
/****** Object:  View [dbo].[v_ActualCostHistory_ElementType]    Script Date: 05/06/2010 12:53:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_ActualCostHistory_ElementType]'))
EXEC dbo.sp_executesql @statement = N'Create View [v_ActualCostHistory_ElementType] as
Select C.*,T.CostElementTypeID from CST_ActualCostHistory C
INNER JOIN STD_CostElement T ON C.CostElementID=T.CostElementID
'
GO
/****** Object:  View [dbo].[v_LeafCostElement]    Script Date: 05/06/2010 12:53:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_LeafCostElement]'))
EXEC dbo.sp_executesql @statement = N'CREATE VIEW [v_LeafCostElement]
AS
SELECT TOP 100 PERCENT
 STD_CostElement.CostElementID, 
 STD_CostElement.Code, 
 STD_CostElement.Name, 
 STD_CostElement.OrderNo, 
 STD_CostElement.CostElementTypeID, 
 STD_CostElementType.Description as ElementType,
 STD_CostElement.ParentID, 
 STD_CostElement.IsLeaf 
FROM STD_CostElement
     INNER JOIN STD_CostElementType ON STD_CostElementType.CostElementTypeID = STD_CostElement.CostElementTypeID
WHERE STD_CostElement.IsLeaf = 1
ORDER BY STD_CostElement.OrderNo ASC
'
GO
/****** Object:  View [dbo].[v_WOCompletion_Begin_Cost]    Script Date: 05/06/2010 12:53:53 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_WOCompletion_Begin_Cost]'))
EXEC dbo.sp_executesql @statement = N'Create View [v_WOCompletion_Begin_Cost] as
Select AC.ProductID,AC.ActCostAllocationMasterID, AM.FromDate,AM.ToDate,Sum(isnull(AC.BeginCost,0)) BeginCost,
Sum(isnull(AC.ComponentValue,0)) WOCompletionCost
from cst_ActualCostHistory AC
INNER JOIN CST_ActCostAllocationMaster AM ON AM.ActCostAllocationMasterID=AC.ActCostAllocationMasterID
Group by AC.ActCostAllocationMasterID,AC.ProductID,AM.FromDate,AM.ToDate
'
GO
/****** Object:  View [dbo].[v_DS_Recycle_AfterAllocate]    Script Date: 05/06/2010 12:53:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_DS_Recycle_AfterAllocate]'))
EXEC dbo.sp_executesql @statement = N'CREATE VIEW [v_DS_Recycle_AfterAllocate] AS
select ProductID,Sum(D.DSAmount+D.OH_DSAmount) AS DSAmount,
SUM(D.RecycleAmount+D.OH_RecycleAmount) as RecycleAmount,Sum(D.AdjustAmount+D.OH_AdjustAmount) as AdjustAmount,
D.ActCostAllocationMasterID,P.FromDate,P.ToDate
from CST_DSAndRecycleAllocation D
INNER JOIN cst_ActCostAllocationMaster P ON P.ActCostAllocationMasterID=D.ActCostAllocationMasterID
Group by D.ProductID,D.ActCostAllocationMasterID,P.FromDate,P.ToDate
'
GO
/****** Object:  View [dbo].[v_UnitOfActualCost1]    Script Date: 05/06/2010 12:53:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_UnitOfActualCost1]'))
EXEC dbo.sp_executesql @statement = N'CREATE view [v_UnitOfActualCost1] as
select C.productID,I.CategoryID,Sum(C.ActualCost)AS UnitCost,C.ActCostAllocationMasterID,P.FromDate,P.ToDate,
DA.DSAmount,DA.RecycleAmount,DA.AdjustAmount
from CST_ActualCostHistory C
inner join cst_ActCostAllocationMaster P ON P.ActCostAllocationMasterID=C.ActCostAllocationMasterID
inner JOIN ITM_PRODUCT I ON I.Productid=c.productid
inner join v_DS_Recycle_AfterAllocate DA ON DA.ActCostAllocationMasterID=C.ActCostAllocationMasterID
AND DA.ProductID=C.ProductID
GROUP BY C.ProductID, C.ActCostAllocationMasterID,P.FromDate,P.ToDate,I.CategoryID,
DA.DSAmount,DA.RecycleAmount,DA.AdjustAmount

'
GO
/****** Object:  View [dbo].[v_UnitOfActualCost_NotDS]    Script Date: 05/06/2010 12:53:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_UnitOfActualCost_NotDS]'))
EXEC dbo.sp_executesql @statement = N'Create View [v_UnitOfActualCost_NotDS] AS
select C.productID,I.CategoryID,Sum(C.ActualCost)AS UnitCost,C.ActCostAllocationMasterID,P.FromDate,P.ToDate
from CST_ActualCostHistory C
inner join cst_ActCostAllocationMaster P ON P.ActCostAllocationMasterID=C.ActCostAllocationMasterID
inner JOIN ITM_PRODUCT I ON I.Productid=c.productid
GROUP BY C.ProductID, C.ActCostAllocationMasterID,P.FromDate,P.ToDate,I.CategoryID
'
GO
/****** Object:  View [dbo].[v_UnitOfActualCost]    Script Date: 05/06/2010 12:53:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_UnitOfActualCost]'))
EXEC dbo.sp_executesql @statement = N'Create view [v_UnitOfActualCost] as
select C.productID,I.CategoryID,Sum(C.ActualCost)AS UnitCost,C.ActCostAllocationMasterID,P.FromDate,P.ToDate
from CST_ActualCostHistory C
inner join cst_ActCostAllocationMaster P ON P.ActCostAllocationMasterID=C.ActCostAllocationMasterID
inner JOIN ITM_PRODUCT I ON I.Productid=c.productid
GROUP BY C.ProductID, C.ActCostAllocationMasterID,P.FromDate,P.ToDate,I.CategoryID
'
GO
/****** Object:  View [dbo].[v_Total_DS_Before_Allocation]    Script Date: 05/06/2010 12:53:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_Total_DS_Before_Allocation]'))
EXEC dbo.sp_executesql @statement = N'Create view [v_Total_DS_Before_Allocation] AS
select CM.ActCostAllocationMasterID,PM.FromDate,PM.ToDate,
CostElementID,Sum(OH_RecycleAmount)as RecycleAmount,
Sum(OH_DSAmount)as DSAmount,Sum(OH_AdjustAmount) as AdjustAmount
from CST_DSAndRecycleAllocation CM
INNER JOIN cst_ActCostAllocationMaster PM ON CM.ActCostAllocationMasterID=PM.ActCostAllocationMasterID
GROUP BY CM.ActCostAllocationMasterID,CostElementID,PM.FromDate,PM.ToDate
'
GO
/****** Object:  View [dbo].[V_SumWOCompletionCost]    Script Date: 05/06/2010 12:53:53 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[V_SumWOCompletionCost]'))
EXEC dbo.sp_executesql @statement = N'Create View [V_SumWOCompletionCost] as
select ACTM.ToDate,ProductID,
(Case 
when
(select WOCompletionQty from CST_ActualCostHistory where 
ProductID=ACT.ProductID and ActCostAllocationMasterID=ACT.ActCostAllocationMasterID
Group by act.ActCostAllocationMasterID,ProductID,WOCompletionQty )>0 
then
 Sum(ActualCost*Quantity-BeginCost*BeginQuantity)/ISNULL(WOCompletionQty,1)
else
 Sum(ActualCost*Quantity-BeginCost*BeginQuantity) end ) CompletionCost 
from CST_ActualCostHistory ACT
INNER JOIN cst_ActCostAllocationMaster ACTM ON ACTM.ActCostAllocationMasterID=ACT.ActCostAllocationMasterID
Group by ACT.ActCostAllocationMasterID,ACTM.ToDate,ProductID,WOCompletionQty
----
'
GO
/****** Object:  View [dbo].[V_SumActCost]    Script Date: 05/06/2010 12:53:52 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[V_SumActCost]'))
EXEC dbo.sp_executesql @statement = N'Create View [V_SumActCost] as
select ACTM.Todate,ACT.ProductID, Sum(ActualCost)Cost from CST_ActualCostHistory ACT
INNER JOIN cst_ActCostAllocationMaster ACTM ON ACTM.ActCostAllocationMasterID=ACT.ActCostAllocationMasterID
Group by ACTM.Todate,ACT.ProductID
'
GO
/****** Object:  View [dbo].[v_MasterLocationItem]    Script Date: 05/06/2010 12:53:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_MasterLocationItem]'))
EXEC dbo.sp_executesql @statement = N'CREATE VIEW [v_MasterLocationItem] AS
SELECT     ML.MasterLocationID, ML.Code, MLC.ProductID, itm.Code AS ProductCode, SUM(ISNULL(MLC.OHQuantity, 0) - ISNULL(MLC.CommitQuantity, 0)) 
                      AS AvailQuantity, MLC.InspStatus
FROM         dbo.MST_MasterLocation ML INNER JOIN
                      dbo.IV_MasLocCache MLC ON ML.MasterLocationID = MLC.MasterLocationID INNER JOIN
                      dbo.ITM_Product itm ON MLC.ProductID = itm.ProductID
GROUP BY ML.MasterLocationID, ML.Code, MLC.ProductID, itm.Code, MLC.InspStatus
'
GO
/****** Object:  View [dbo].[V_MasLocItemSumAvail]    Script Date: 05/06/2010 12:53:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[V_MasLocItemSumAvail]'))
EXEC dbo.sp_executesql @statement = N'CREATE VIEW [V_MasLocItemSumAvail] AS
SELECT ML.CCNID,ML.MasterLocationID, ML.Code, MLC.ProductID, MLC.InspStatus,
SUM(MLC.OHQuantity - ISNULL(MLC.CommitQuantity,0)) AS AvailQuantity
FROM MST_MasterLocation ML
INNER JOIN IV_MasLocCache MLC ON ML.MasterLocationID = MLC.MasterLocationID
AND (MLC.OHQuantity - ISNULL(MLC.CommitQuantity,0)) > 0
GROUP BY  ML.CCNID,ML.MasterLocationID, ML.Code, MLC.ProductID, MLC.InspStatus
'
GO
/****** Object:  View [dbo].[V_LocationItemSumAvail]    Script Date: 05/06/2010 12:53:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[V_LocationItemSumAvail]'))
EXEC dbo.sp_executesql @statement = N'CREATE VIEW [V_LocationItemSumAvail] AS
SELECT L.LocationID, L.Code, LC.ProductID, LC.MasterLocationID, L.Bin, LC.InspStatus, 
SUM(LC.OHQuantity - LC.CommitQuantity) AS AvailQuantity
FROM MST_Location L
INNER JOIN IV_LocationCache LC ON L.LocationID = LC.LocationID
AND (LC.OHQuantity - ISNULL(LC.CommitQuantity,0)) > 0
GROUP BY L.LocationID, L.Code, LC.ProductID, LC.MasterLocationID, L.Bin, LC.InspStatus
'
GO
/****** Object:  View [dbo].[V_LocationItem]    Script Date: 05/06/2010 12:53:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[V_LocationItem]'))
EXEC dbo.sp_executesql @statement = N'CREATE VIEW [V_LocationItem] AS
SELECT     LC.InspStatus, L.LocationID, L.Code, LC.ProductID, LC.MasterLocationID, itm.Code AS ProductCode, L.Bin, SUM(ISNULL(LC.OHQuantity, 0) - ISNULL(LC.CommitQuantity, 0)) AS AvailQuantity
FROM         dbo.MST_Location L INNER JOIN
             dbo.IV_LocationCache LC ON L.LocationID = LC.LocationID INNER JOIN
             dbo.ITM_Product itm ON LC.ProductID = itm.ProductID
GROUP BY LC.InspStatus, L.LocationID, L.Code, LC.ProductID, LC.MasterLocationID, itm.Code, L.Bin 
'
GO
/****** Object:  View [dbo].[V_ProductInLocCache]    Script Date: 05/06/2010 12:53:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[V_ProductInLocCache]'))
EXEC dbo.sp_executesql @statement = N'CREATE VIEW [V_ProductInLocCache]
AS
SELECT  Code, Description, Revision, StockUMID, Bin, LocationCode, LocationID,
        MasterLocationID, ProductID, MST_UnitOfMeasureCode, LotControl,
        PrimaryVendorID, MST_PartyCode, ITM_CategoryCode, AvailableQuantity,
        AllowNegativeQty
FROM    ( SELECT DISTINCT
                    p.Code, p.Description, p.Revision, p.StockUMID, loc.Bin,
                    loc.Code AS LocationCode, loc.LocationID,
                    loc.MasterLocationID, p.ProductID,
                    u.Code AS MST_UnitOfMeasureCode, p.LotControl,
                    p.PrimaryVendorID, V.Code AS MST_PartyCode,
                    C.Code AS ITM_CategoryCode,
                    ( SELECT    SUM(ISNULL(OHQuantity, 0)
                                    - ISNULL(CommitQuantity, 0))
                      FROM      iv_locationcache locC
                      WHERE     locC.ProductID = p.ProductID
                                AND locC.LocationID = loc.LocationID
                    ) AS AvailableQuantity,
                    ISNULL(P.AllowNegativeQty, 0) AllowNegativeQty
          FROM      dbo.ITM_Product p
                    INNER JOIN dbo.MST_UnitOfMeasure u ON p.StockUMID = u.UnitOfMeasureID
                    INNER JOIN dbo.IV_LocationCache lc ON p.ProductID = lc.ProductID
                    INNER JOIN dbo.MST_Location loc ON lc.LocationID = loc.LocationID
                    LEFT JOIN MST_Party V ON p.PrimaryVendorID = V.PartyID
                    LEFT JOIN ITM_Category C ON p.CategoryID = C.CategoryID
        ) B
WHERE   ISNULL(B.AvailableQuantity, 0) > 0
        OR B.AllowNegativeQty = 1
'
GO
/****** Object:  View [dbo].[V_ProductForStockTaking]    Script Date: 05/06/2010 12:53:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[V_ProductForStockTaking]'))
EXEC dbo.sp_executesql @statement = N'CREATE VIEW [V_ProductForStockTaking]
AS
SELECT P.Code, P.Description, P.Revision, P.StockUMID, P.ProductID,
CA.Code ITM_CategoryCode, UM.Code MST_UnitOfMeasureCode, S.Code Source,
PT.Code ProductType, V.Code Vendor,StockTakingCode
FROM ITM_Product P
LEFT JOIN ITM_Category CA ON CA.CategoryID = P.CategoryID
LEFT JOIN MST_UnitOfMeasure UM on UM.UnitOfMeasureID = P.StockUMID
LEFT JOIN ITM_Source S on S.SourceID = P.SourceID
LEFT JOIN ITM_ProductType PT on PT.ProductTypeID = P.ProductTypeID
LEFT JOIN MST_Party V on V.PartyID = P.PrimaryVendorID
'
GO
/****** Object:  View [dbo].[V_InvoiceMasterNotReceiving]    Script Date: 05/06/2010 12:53:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[V_InvoiceMasterNotReceiving]'))
EXEC dbo.sp_executesql @statement = N'CREATE VIEW [V_InvoiceMasterNotReceiving]
AS
SELECT DISTINCT IM.InvoiceMasterID, InvoiceNo, PostDate, ExchangeRate, BLDate,
        InformDate, DeclarationDate, BLNumber, TaxInformNumber,
        TaxDeclarationNumber, TotalInlandAmount, TotalCIPAmount,
        TotalCIFAmount, TotalImportTax, TotalBeforeVATAmount, TotalVATAmount,
        CCNID, IM.PartyID, CurrencyID, CarrierID, PaymentTermID, DeliveryTermID,
        UserName, LastChange
FROM    PO_InvoiceMaster IM
JOIN    PO_InvoiceDetail invoice ON IM.InvoiceMasterID = invoice.InvoiceMasterID
JOIN    PO_DeliverySchedule DS ON invoice.DeliveryScheduleID = DS.DeliveryScheduleID
'
GO
/****** Object:  View [dbo].[v_Invoice]    Script Date: 05/06/2010 12:53:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_Invoice]'))
EXEC dbo.sp_executesql @statement = N'CREATE VIEW [v_Invoice]
AS
SELECT  M.InvoiceMasterID, M.InvoiceNo, M.PostDate, M.PartyID, D.ProductID,
        D.InvoiceQuantity, D.UnitPrice, D.CIFAmount, D.CIPAmount, D.VATAmount,
        D.VAT, D.ImportTaxAmount, D.ImportTax, D.Inland
FROM    po_invoicemaster M
INNER JOIN po_invoicedetail D ON M.InvoiceMasterID = D.InvoiceMasterID
'
GO
/****** Object:  View [dbo].[v_WOCompletion]    Script Date: 05/06/2010 12:53:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_WOCompletion]'))
EXEC dbo.sp_executesql @statement = N'CREATE VIEW [v_WOCompletion]
AS
SELECT  Distinct wod.WorkOrderDetailID, wod.StartDate, wod.DueDate, wod.WorkOrderMasterID, 
		wod.ProductID, wod.OrderQuantity, wod.Line, wod.Status, 
        wod.StockUMID, UM.Code AS UM, l.Code AS LocationCode, b.Code AS BinCode, p.Code AS PartNumber, p.Description AS PartName, 
        p.Revision AS Model, p.QAStatus, p.MasterLocationID, p.LocationID, p.BinID, p.LotSize, p.LotControl, 
		SUM(ISNULL(PRO_WorkOrderCompletion.CompletedQuantity,0)) CompletedQuantity,
            0 ScrapQuantity, l.Bin, b.BinTypeID, P.CategoryID
FROM    dbo.PRO_WorkOrderDetail Wod 
Inner join dbo.ITM_Product p ON wod.ProductID = p.ProductID 
INNER JOIN
        dbo.MST_UnitOfMeasure UM ON UM.UnitOfMeasureID = wod.StockUMID 
LEFT  JOIN
        dbo.MST_Location l ON l.LocationID = p.LocationID 
LEFT  JOIN
        dbo.MST_BIN b ON b.BinID = p.BinID 
LEFT  JOIN
		PRO_WorkOrderCompletion ON wod.WorkOrderDetailID = PRO_WorkOrderCompletion.WorkOrderDetailID
WHERE   wod.Status = 2
AND DueDate BETWEEN DATEADD(day, -(SELECT TOP 1 DayBefore FROM Sys_PostdateConfiguration
WHERE Purpose = ''SearchCondition''), getdate()) AND DATEADD(day, (SELECT TOP 1 DayBefore FROM Sys_PostdateConfiguration
WHERE Purpose = ''SearchCondition''), getdate())
GROUP BY wod.WorkOrderDetailID, wod.StartDate, wod.DueDate, wod.WorkOrderMasterID, 
		wod.ProductID, wod.OrderQuantity, wod.Line, wod.Status, 
        wod.StockUMID, UM.Code, l.Code, b.Code, p.Code, p.Description, 
        p.Revision, p.QAStatus, p.MasterLocationID, p.LocationID, p.BinID, p.LotSize, p.LotControl,
		l.Bin, b.BinTypeID, P.CategoryID
HAVING SUM(ISNULL(PRO_WorkOrderCompletion.CompletedQuantity,0)) < wod.OrderQuantity
'
GO
/****** Object:  View [dbo].[v_SearchProductForMaterialReceipt]    Script Date: 05/06/2010 12:53:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_SearchProductForMaterialReceipt]'))
EXEC dbo.sp_executesql @statement = N'CREATE view [v_SearchProductForMaterialReceipt] 
AS
(
SELECT     
                      dbo.ITM_Product.ProductID, 
                      dbo.ITM_Product.Code, 
                      dbo.ITM_Product.Revision, 
                      dbo.ITM_Product.Description, 
                      dbo.ITM_Product.LotControl, 
                      dbo.ITM_Product.LotSize, 
                      dbo.ITM_Product.MasterLocationID, 
                      dbo.ITM_Product.LocationID, 
                      dbo.ITM_Product.BinID, 
                      dbo.ITM_Product.StockUMID, 
                      dbo.MST_MasterLocation.Code AS MST_MasterLocationCode, 
                      dbo.MST_Location.Code AS MST_LocationCode, 
                      dbo.MST_BIN.Code AS MST_BINCode, 
                      dbo.MST_UnitOfMeasure.Code AS MST_UnitOfMeasureCode
FROM         
                      dbo.ITM_Product 
                      LEFT OUTER JOIN
                      dbo.MST_UnitOfMeasure ON dbo.ITM_Product.StockUMID = dbo.MST_UnitOfMeasure.UnitOfMeasureID 
                      LEFT OUTER JOIN
                      dbo.MST_MasterLocation ON dbo.ITM_Product.MasterLocationID = dbo.MST_MasterLocation.MasterLocationID 
                      LEFT OUTER JOIN
                      dbo.MST_Location ON dbo.ITM_Product.LocationID = dbo.MST_Location.LocationID 
                      LEFT OUTER JOIN
                      dbo.MST_BIN ON dbo.ITM_Product.BinID = dbo.MST_BIN.BinID
)
'
GO
/****** Object:  View [dbo].[v_ProductInventoryAdjustment]    Script Date: 05/06/2010 12:53:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_ProductInventoryAdjustment]'))
EXEC dbo.sp_executesql @statement = N'CREATE VIEW [v_ProductInventoryAdjustment]
AS
SELECT     p.Code AS PartNumber, p.Description AS PartName, p.Revision AS Model, p.MasterLocationID, masloc.Code AS MasLocName, p.LocationID, 
                      loc.Code AS LocName, loc.Bin AS BinControl, p.BinID, bin.Code AS BinName, u.Code AS UM, p.LotControl, LC.InspStatus AS LocQAStatus, p.LotSize, 
                      BC.InspStatus AS BinQAStatus, p.ProductID, p.StockUMID, masloc.CCNID, SUM(ISNULL(LC.OHQuantity, 0) - ISNULL(LC.CommitQuantity, 0)) 
                      AS LocAvailableQuantity, SUM(ISNULL(BC.OHQuantity, 0) - ISNULL(BC.CommitQuantity, 0)) AS BinAvailableQuantity, P.CategoryID
            FROM dbo.ITM_Product p INNER JOIN
                      dbo.MST_UnitOfMeasure u ON p.StockUMID = u.UnitOfMeasureID LEFT OUTER JOIN
                      dbo.MST_MasterLocation masloc ON p.MasterLocationID = masloc.MasterLocationID LEFT OUTER JOIN
                      dbo.MST_Location loc ON p.LocationID = loc.LocationID LEFT OUTER JOIN
                      dbo.MST_BIN bin ON p.BinID = bin.BinID LEFT OUTER JOIN
                      dbo.IV_LocationCache LC ON p.ProductID = LC.ProductID AND loc.LocationID = LC.LocationID LEFT OUTER JOIN
                      dbo.IV_BinCache BC ON p.ProductID = BC.ProductID AND bin.BinID = BC.BinID
            GROUP BY p.Code, p.Description, p.Revision, p.MasterLocationID, masloc.Code, p.LocationID, loc.Code, loc.Bin, p.BinID, bin.Code, u.Code, p.LotControl, 
                      LC.InspStatus, p.LotSize, BC.InspStatus, p.ProductID, p.StockUMID, masloc.CCNID, P.CategoryID
'
GO
/****** Object:  View [dbo].[V_ProductInforWithInventory]    Script Date: 05/06/2010 12:53:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[V_ProductInforWithInventory]'))
EXEC dbo.sp_executesql @statement = N'CREATE View [V_ProductInforWithInventory] AS
SELECT p.Code AS PartNumber, p.Description AS PartName, p.Revision AS Model, mlc.MasterLocationID, masloc.Code AS MasLocName, lc.LocationID, 
                      loc.Code AS LocName, loc.Bin AS BinControl, p.BinID, bin.Code AS BinName, u.Code AS UM, p.LotControl, LC.InspStatus AS LocQAStatus, 
                      BC.InspStatus AS BinQAStatus, LC.OHQuantity - LC.CommitQuantity AS LocAvailableQuantity, u.UnitOfMeasureID, 
                      BC.OHQuantity - BC.CommitQuantity AS BinAvailableQuantity, p.ProductID, p.StockUMID, masloc.CCNID, p.LotSize, p.QAStatus 
FROM dbo.ITM_Product  p INNER JOIN
   dbo.MST_UnitOfMeasure u ON p.StockUMID = u.UnitOfMeasureID 
   inner join iv_masloccache mlc on p.Productid = mlc.ProductID
   inner join MST_MasterLocation masloc ON mlc.MasterLocationID = masloc.MasterLocationID 
   left join IV_LocationCache LC ON p.ProductID = LC.ProductID and lc.MasterLocationID = mlc.MasterLocationID
   left join MST_Location loc ON lc.LocationID = loc.LocationID
   left join IV_BinCache BC on BC.ProductID = p.ProductID and bc.LocationID = lc.LocationID
   left join MST_Bin Bin on Bin.BinID = bc.BinID
'
GO
/****** Object:  View [dbo].[V_ProductInBinCache]    Script Date: 05/06/2010 12:53:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[V_ProductInBinCache]'))
EXEC dbo.sp_executesql @statement = N'CREATE VIEW [V_ProductInBinCache]
AS
SELECT  Code, Description, Revision, StockUMID, MST_BinCode, LocationID,
        ProductID, CategoryID, MST_UnitOfMeasureCode, PrimaryVendorID,
        MST_PartyCode, ITM_CategoryCode, LotControl, BinID, MasterLocationID,
        AvailableQuantity, AllowNegativeQty
FROM    ( SELECT DISTINCT
                    p.Code, p.Description, p.Revision, p.StockUMID,
                    b.Code AS MST_BinCode, bc.LocationID, p.ProductID,
                    p.CategoryID, u.Code AS MST_UnitOfMeasureCode,
                    p.PrimaryVendorID, V.Code AS MST_PartyCode,
                    C.Code AS ITM_CategoryCode, p.LotControl, b.BinID,
                    bc.MasterLocationID,
                    ( SELECT    SUM(ISNULL(OHQuantity, 0)
                                    - ISNULL(CommitQuantity, 0))
                      FROM      iv_bincache binC
                      WHERE     binC.ProductID = p.ProductID
                                AND binC.BinID = bc.BinID
                    ) AS AvailableQuantity,
                    ISNULL(p.AllowNegativeQty, 0) AllowNegativeQty
          FROM      dbo.ITM_Product p
                    INNER JOIN dbo.MST_UnitOfMeasure u ON p.StockUMID = u.UnitOfMeasureID
                    INNER JOIN dbo.IV_BinCache bc ON bc.ProductID = p.ProductID
                    INNER JOIN dbo.MST_BIN b ON b.BinID = bc.BinID
                    LEFT JOIN MST_Party V ON p.PrimaryVendorID = V.PartyID
                    LEFT JOIN ITM_Category C ON p.CategoryID = C.CategoryID
        ) B
WHERE   ISNULL(B.AvailableQuantity, 0) > 0
        OR B.AllowNegativeQty = 1
'
GO
/****** Object:  View [dbo].[V_PRODUCT_AVAILABLE_IN_BIN_INCOMING]    Script Date: 05/06/2010 12:53:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[V_PRODUCT_AVAILABLE_IN_BIN_INCOMING]'))
EXEC dbo.sp_executesql @statement = N'CREATE VIEW [V_PRODUCT_AVAILABLE_IN_BIN_INCOMING]
AS
Select productid, IV_BINCache.Binid, mst_Location.locationid, mst_Location.masterlocationID, sum(isnull(ohquantity, 0) - isnull(commitquantity, 0)) as binavailable
FROM    IV_BINCache left join mst_Bin on IV_BINCache.BinID = mst_Bin.BinID 
           left join mst_Location on mst_Location.LocationID = mst_Bin.LocationID
           left join mst_MasterLocation on mst_MasterLocation.MasterLocationID = mst_Location.MasterLocationID
	Where  BinTypeID = 4
	Group  By productid, IV_BINCache.Binid, mst_Location.LocationID, mst_Location.MasterLocationID
'
GO
/****** Object:  View [dbo].[v_TotalQuantityReturnToVendor]    Script Date: 05/06/2010 12:53:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_TotalQuantityReturnToVendor]'))
EXEC dbo.sp_executesql @statement = N'CREATE VIEW [v_TotalQuantityReturnToVendor] 
AS
(
SELECT     
           dbo.PO_ReturnToVendorMaster.PurchaseOrderMasterID, 
           dbo.PO_ReturnToVendorDetail.ProductID, 
           dbo.PO_ReturnToVendorDetail.Lot,
           dbo.PO_ReturnToVendorDetail.Serial,
           sum(dbo.PO_ReturnToVendorDetail.Quantity) as TotalReturn
FROM         
           dbo.PO_ReturnToVendorDetail 
           INNER JOIN dbo.PO_ReturnToVendorMaster ON dbo.PO_ReturnToVendorDetail.ReturnToVendorMasterID = dbo.PO_ReturnToVendorMaster.ReturnToVendorMasterID
WHERE
           dbo.PO_ReturnToVendorMaster.PurchaseOrderMasterID is not null
GROUP BY 
           dbo.PO_ReturnToVendorMaster.PurchaseOrderMasterID,
           dbo.PO_ReturnToVendorDetail.ProductID,
           dbo.PO_ReturnToVendorDetail.Lot,
           dbo.PO_ReturnToVendorDetail.Serial
)
'
GO
/****** Object:  View [dbo].[v_PO_PurchaseOrderDetailReceipt]    Script Date: 05/06/2010 12:53:52 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_PO_PurchaseOrderDetailReceipt]'))
EXEC dbo.sp_executesql @statement = N'

CREATE   VIEW [v_PO_PurchaseOrderDetailReceipt]
AS
SELECT    a.PurchaseOrderDetailID, a.BuyingUMID, a.ProductID, a.Code, a.Revision, a.StockUMID, a.TotalReceive, a.Lot, a.Serial, 
a.PurchaseOrderMasterID, a.TotalRemain, P.LocationID, L.Code AS LocationCode, 
L.Bin, B.BinID, B.Code AS BinCode, a.LotControl, a.Description, a.UnitPrice, 
0.0 Amount, a.VATPercent, 0.0 VATAmount, 0.0 TotalAmount, a.UMRate, MST_UnitOfMeasure.Code AS MST_UnitOfMeasureCode
FROM         
(SELECT dbo.PO_PurchaseOrderDetail.PurchaseOrderDetailID, dbo.PO_PurchaseOrderDetail.BuyingUMID, dbo.PO_PurchaseOrderDetail.ProductID, 
dbo.ITM_Product.Code, dbo.ITM_Product.Revision, dbo.ITM_Product.Description, dbo.ITM_Product.StockUMID, dbo.ITM_Product.LotControl, 
dbo.v_PO_PurchaseOrderTotalReceive.TotalReceive, dbo.v_PO_PurchaseOrderTotalReceive.Lot, 
dbo.v_PO_PurchaseOrderTotalReceive.Serial, dbo.PO_PurchaseOrderDetail.UnitPrice,
dbo.PO_PurchaseOrderDetail.VAT VATPercent, ISNULL(PO_PurchaseOrderDetail.UMRate, 1) UMRate,
dbo.PO_PurchaseOrderDetail.PurchaseOrderMasterID, 
TotalRemain = v_PO_PurchaseOrderTotalReceive.TotalReceive
 - (isnull((SELECT     SUM(TotalReturn) FROM         v_TotalQuantityReturnToVendor
 WHERE     PurchaseOrderMasterID = dbo.PO_PurchaseOrderDetail.PurchaseOrderMasterID 
AND ProductID = dbo.PO_PurchaseOrderDetail.ProductID AND isnull(Lot, '''') = isnull(v_PO_PurchaseOrderTotalReceive.Lot, '''') AND 
  isnull(Serial, '''') = isnull(v_PO_PurchaseOrderTotalReceive.Serial, '''')), 0))
                       FROM          dbo.PO_PurchaseOrderDetail INNER JOIN
                                              dbo.ITM_Product ON dbo.PO_PurchaseOrderDetail.ProductID = dbo.ITM_Product.ProductID AND 
                                              dbo.PO_PurchaseOrderDetail.ProductID = dbo.ITM_Product.ProductID AND 
                                              dbo.PO_PurchaseOrderDetail.ProductID = dbo.ITM_Product.ProductID AND 
                                              dbo.PO_PurchaseOrderDetail.ProductID = dbo.ITM_Product.ProductID INNER JOIN
                                              dbo.v_PO_PurchaseOrderTotalReceive ON 
                                              dbo.PO_PurchaseOrderDetail.PurchaseOrderDetailID = dbo.v_PO_PurchaseOrderTotalReceive.PurchaseOrderDetailID AND 
                                              dbo.PO_PurchaseOrderDetail.ProductID = dbo.v_PO_PurchaseOrderTotalReceive.ProductID
                       WHERE      (dbo.PO_PurchaseOrderDetail.ProductID IN
                                                  (SELECT     ProductID
                                                     FROM          PO_PurchaseOrderReceiptDetail
                                                    WHERE      PurchaseOrderDetailID = PO_PurchaseOrderDetail.PurchaseOrderDetailID))) a 
            LEFT OUTER JOIN    dbo.ITM_Product P ON P.ProductID = a.ProductID LEFT OUTER JOIN
                      dbo.MST_Location L ON L.LocationID = P.LocationID LEFT OUTER JOIN
                      dbo.MST_BIN B ON B.BinID = P.BinID
LEFT JOIN MST_UnitOfMeasure ON a.BuyingUMID = MST_UnitOfMeasure.UnitOfMeasureID
            WHERE     (a.TotalRemain > 0)
'
GO
/****** Object:  View [dbo].[v_IVAdjustmentAndProduct]    Script Date: 05/06/2010 12:53:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_IVAdjustmentAndProduct]'))
EXEC dbo.sp_executesql @statement = N'CREATE VIEW [v_IVAdjustmentAndProduct]
AS
SELECT     dbo.IV_Adjustment.Comment, dbo.IV_Adjustment.TransNo, dbo.IV_Adjustment.Serial, dbo.IV_Adjustment.Lot, dbo.IV_Adjustment.AdjustQuantity, 
                      dbo.IV_Adjustment.BinID, dbo.IV_Adjustment.MasterLocationID, dbo.IV_Adjustment.LocationID, dbo.IV_Adjustment.AdjustmentID, 
                      dbo.IV_Adjustment.PostDate, dbo.IV_Adjustment.CCNID, dbo.IV_Adjustment.ProductID, dbo.IV_Adjustment.StockUMID, 
                      dbo.ITM_Product.Revision AS Model, dbo.ITM_Product.Description AS PartName, dbo.ITM_Product.Code AS PartNumber, 
                      dbo.MST_MasterLocation.Code AS MasLocName, dbo.MST_UnitOfMeasure.Code AS UM, dbo.MST_BIN.Code AS BinName, 
                      dbo.MST_Location.Code AS LocName, IV_Adjustment.AvailableQty, ITM_Product.CategoryID, dbo.IV_Adjustment.UsedByCosting

FROM         dbo.IV_Adjustment INNER JOIN
                      dbo.ITM_Product ON dbo.IV_Adjustment.ProductID = dbo.ITM_Product.ProductID INNER JOIN
                      dbo.MST_MasterLocation ON dbo.IV_Adjustment.MasterLocationID = dbo.MST_MasterLocation.MasterLocationID LEFT OUTER JOIN
                      dbo.MST_UnitOfMeasure ON dbo.IV_Adjustment.StockUMID = dbo.MST_UnitOfMeasure.UnitOfMeasureID LEFT OUTER JOIN
                      dbo.MST_BIN ON dbo.IV_Adjustment.BinID = dbo.MST_BIN.BinID INNER JOIN
                      dbo.MST_Location ON dbo.IV_Adjustment.LocationID = dbo.MST_Location.LocationID
'
GO
/****** Object:  View [dbo].[v_InputInPeriod]    Script Date: 05/06/2010 12:53:52 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_InputInPeriod]'))
EXEC dbo.sp_executesql @statement = N'Create view [v_InputInPeriod] as
select DatePart(Year,Trans.PostDate) Years ,Datepart(Month,Trans.PostDate)as Months,Trans.LocationID,Trans.BinID,Trans.ProductID,
sum(isnull(Trans.Quantity,0))AS Qty,
Isnull((select -Sum(isnull(Quantity,0)) from MST_TransactionHistory Trans4
WHERE TranTypeID=12 
AND Trans4.LocationID=Trans.LocationID 
AND Trans4.BinID=Trans.BinID
AND Trans4.ProductID=Trans.ProductID
AND Datepart(Year,Trans4.PostDate)=Datepart(Year,Trans.PostDate)
AND Datepart(Month,Trans4.PostDate)=Datepart(Month,Trans.PostDate)
Group by DatePart(Year,Trans4.PostDate),Datepart(Month,Trans4.PostDate),Trans4.LocationID,Trans4.BinID,Trans4.ProductID),0) AS Qty2,
isnull((Select Sum(isnull(ACT.ComponentValue,0)) from CST_ActualCostHistory ACT
INNER JOIN cst_ActCostAllocationMaster M ON ACT.ActCostAllocationMasterID=M.ActCostAllocationMasterID
Where ACT.ProductID=Trans.ProductID and DatePart(Year,M.FromDate)=DatePart(Year,Trans.PostDate)
AND DatePart(Month,M.FromDate)=Datepart(Month,Trans.PostDate)
Group by DatePart(Year,M.FromDate),DatePart(Month,M.FromDate)
),0) as Amount1,
ISNULL((select Sum(isnull(Trans1.Quantity,0))*
ISNULL((Select UnitCost from v_UnitOfActualCost UC
WHERE ProductID=Trans1.ProductID
AND DatePart(Year,Trans1.PostDate)=DatePart(Year,UC.FromDate)
AND DatePart(Month,Trans1.PostDate)=DatePart(Month,UC.FromDate)
),0)
From MST_TransactionHistory Trans1
INNER JOIN MST_Bin Bin1 ON BIN1.BinID=Trans1.BinID
where TranTypeID IN (8,14,17,21,24,25)
AND Quantity>=0
AND BIN1.BinTypeID<>3
AND Trans1.LocationID=Trans.LocationID 
AND Trans1.BinID=Trans.BinID
AND Trans1.ProductID=Trans.ProductID
AND Datepart(Year,Trans1.PostDate)=Datepart(Year,Trans.PostDate)
AND Datepart(Month,Trans1.PostDate)=Datepart(Month,Trans.PostDate)
Group by DatePart(Year,Trans1.PostDate),Datepart(Month,Trans1.PostDate),Trans1.LocationID,Trans1.BinID,Trans1.ProductID
),0) as Amount2
from MST_TransactionHistory Trans
INNER JOIN MST_Bin Bin ON BIN.BinID=Trans.BinID
where TranTypeID IN (8,11,13,14,17,19,21,24,25)
AND Quantity>=0
AND BIN.BinTypeID<>3
Group by DatePart(Year,Trans.PostDate),Datepart(Month,Trans.PostDate),Trans.LocationID,Trans.BinID,Trans.ProductID
'
GO
/****** Object:  View [dbo].[v_InOutStockForAccounting]    Script Date: 05/06/2010 12:53:52 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_InOutStockForAccounting]'))
EXEC dbo.sp_executesql @statement = N'CREATE VIEW [v_InOutStockForAccounting] AS
select IOS.Years,IOS.Months,IOS.TranTypeID,TranType.Code TranType,IOS.LocationID,Loc.Code Location, 
IOS.BinID,Bin.Code Bin,Bin.BinTypeID,IOS.ProductID,I.Code PartNo,I.Description PartName,
I.Revision Model,CAT.Code Category,UM.Code UM,
Sum(IOS.OUTQty) OUTQty,Sum(IOS.INQty) INQty,
            isnull((select isnull(BeginCost,0) from v_WOCompletion_Begin_Cost where 
            ProductID=IOS.ProductID
            AND Datepart(Year,FromDate)=IOS.Years
            AND Datepart(Month,FromDate)=IOS.Months),0)AS BeginCost,
            Isnull((select isnull(UnitCost,0) from v_UnitOfActualCost where 
            ProductID=IOS.ProductID
            AND Datepart(Year,FromDate)=IOS.Years
            AND Datepart(Month,FromDate)=IOS.Months),0)AS InCost,
            Isnull((select isnull(UnitCost,0) from v_UnitOfActualCost where 
            ProductID=IOS.ProductID
            AND Datepart(Year,FromDate)=IOS.Years
            AND Datepart(Month,FromDate)=IOS.Months),0)AS OUTCost
from v_InOutStock  IOS
INNER JOIN MST_TranType TranType on TranType.TranTypeID=IOS.TranTypeID
INNER JOIN ITM_Product I ON I.ProductID=IOS.ProductID
INNER JOIN MST_Location Loc on Loc.LocationID=IOS.LocationID
INNER JOIN MST_Bin Bin on Bin.BinID=IOS.Binid
inner join MST_UnitOfMeasure UM ON UM.UnitOfMeasureID=I.StockUMID
LEFT JOIN ITM_Category CAT ON CAT.CategoryID=I.CategoryID
where IOS.TranTypeID <> 19
Group by IOS.Years,IOS.Months,IOS.TranTypeID,TranType.Code,IOS.LocationID,Loc.Code,IOS.BinID,
Bin.Code,CAT.Code,IOS.ProductID,I.Code,I.Description,
I.Revision,UM.Code,Bin.BinTypeID
union all
select IOS.Years,IOS.Months,IOS.TranTypeID,TranType.Code TranType,IOS.LocationID,Loc.Code Location, 
IOS.BinID,Bin.Code Bin,Bin.BinTypeID,IOS.ProductID,I.Code PartNo,I.Description PartName,
I.Revision Model,CAT.Code Category,UM.Code UM,
Sum(IOS.OUTQty) OUTQty,Sum(IOS.INQty) INQty,
            isnull((select isnull(BeginCost,0) from v_WOCompletion_Begin_Cost where 
            ProductID=IOS.ProductID
            AND Datepart(Year,FromDate)=IOS.Years
            AND Datepart(Month,FromDate)=IOS.Months),0)AS BeginCost,
            Isnull((select isnull(WOCompletionCost,0) from v_WOCompletion_Begin_Cost where 
            ProductID=IOS.ProductID
            AND Datepart(Year,FromDate)=IOS.Years
            AND Datepart(Month,FromDate)=IOS.Months),0)AS INCost,
            (select UnitCost from v_UnitOfActualCost where 
            ProductID=IOS.ProductID
            AND Datepart(Year,FromDate)=IOS.Years
            AND Datepart(Month,FromDate)=IOS.Months)AS OUTCost
from v_InOutStock  IOS
INNER JOIN MST_TranType TranType on TranType.TranTypeID=IOS.TranTypeID
INNER JOIN ITM_Product I ON I.ProductID=IOS.ProductID
INNER JOIN MST_Location Loc on Loc.LocationID=IOS.LocationID
INNER JOIN MST_Bin Bin on Bin.BinID=IOS.Binid
inner join MST_UnitOfMeasure UM ON UM.UnitOfMeasureID=I.StockUMID
LEFT JOIN ITM_Category CAT ON CAT.CategoryID=I.CategoryID
where IOS.TranTypeID=19
Group by IOS.Years,IOS.Months,IOS.TranTypeID,TranType.Code,IOS.LocationID,Loc.Code,IOS.BinID,
Bin.Code,CAT.Code,IOS.ProductID,I.Code,I.Description,
I.Revision,UM.Code,Bin.BinTypeID
'
GO
/****** Object:  View [dbo].[v_GetSaleOrderTotalInvCommit]    Script Date: 05/06/2010 12:53:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_GetSaleOrderTotalInvCommit]'))
EXEC dbo.sp_executesql @statement = N'CREATE VIEW [v_GetSaleOrderTotalInvCommit]
AS
SELECT     SaleOrderCommit.SaleOrderDetailID, SaleOrderCommit.Lot, SaleOrderCommit.Serial, 
                      SaleOrderCommit.TotalCommit - ISNULL(ReturnedItem.TotalReceive, 0) AS BalanceQty, SaleOrderCommit.AVGCost, 
                      dbo.SO_SaleOrderDetail.SellingUMID, dbo.SO_SaleOrderDetail.ProductID, dbo.ITM_Product.Description, dbo.ITM_Product.Revision, 
                      dbo.ITM_Product.Code, dbo.ITM_Product.QAStatus, dbo.SO_SaleOrderDetail.SaleOrderMasterID, dbo.SO_SaleOrderDetail.UnitPrice, 
                      dbo.ITM_Product.MasterLocationID, dbo.ITM_Product.LocationID, dbo.ITM_Product.BinID, dbo.MST_MasterLocation.Code AS MST_MasterLocationCode,
                      dbo.MST_Location.Code AS MST_LocationCode, dbo.MST_Location.Bin, dbo.MST_BIN.Code AS MST_BinCode, 
                      dbo.MST_UnitOfMeasure.Code AS MST_UnitOfMeasureCode, ITM_Product.StockUMID, ITM_Product.CategoryID
FROM         (SELECT     a.SaleOrderDetailID, b.Lot, b.Serial, SUM(b.CommitQuantity) AS TotalCommit, AVG(b.CostOfGoodsSold * CommitQuantity) AS AVGCost
              FROM       SO_DeliverySchedule a INNER JOIN
                         SO_CommitInventoryDetail b ON a.DeliveryScheduleID = b.DeliveryScheduleID INNER JOIN
                         SO_SaleOrderDetail c ON c.SaleOrderDetailID = a.SaleOrderDetailID
              WHERE      b.Shipped = 1
GROUP BY a.SaleOrderDetailID, b.Lot, b.Serial) SaleOrderCommit LEFT OUTER JOIN
             (SELECT     c.SaleOrderDetailID, c.Lot, c.Serial, SUM(c.QuantityOfSelling) AS TotalReceive
              FROM          SO_ReturnedGoodsDetail c
              GROUP BY c.SaleOrderDetailID, c.Lot, c.Serial) ReturnedItem ON SaleOrderCommit.SaleOrderDetailID = ReturnedItem.SaleOrderDetailID INNER JOIN
              dbo.SO_SaleOrderDetail ON dbo.SO_SaleOrderDetail.SaleOrderDetailID = SaleOrderCommit.SaleOrderDetailID INNER JOIN
              dbo.ITM_Product ON dbo.SO_SaleOrderDetail.ProductID = dbo.ITM_Product.ProductID LEFT OUTER JOIN
              dbo.MST_MasterLocation ON dbo.MST_MasterLocation.MasterLocationID = dbo.ITM_Product.MasterLocationID LEFT OUTER JOIN
              dbo.MST_Location ON dbo.MST_Location.LocationID = dbo.ITM_Product.LocationID LEFT OUTER JOIN
              dbo.MST_BIN ON dbo.MST_BIN.BinID = dbo.ITM_Product.BinID LEFT OUTER JOIN
              dbo.MST_UnitOfMeasure ON dbo.MST_UnitOfMeasure.UnitOfMeasureID = dbo.SO_SaleOrderDetail.SellingUMID
'
GO
/****** Object:  View [dbo].[V_BinItemSumAvail]    Script Date: 05/06/2010 12:53:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[V_BinItemSumAvail]'))
EXEC dbo.sp_executesql @statement = N'CREATE VIEW [V_BinItemSumAvail]
AS
SELECT B.BinID, 
              B.Code, 
              BC.ProductID, 
              BC.LocationID, 
              BC. InspStatus,
             SUM(BC.OHQuantity - BC.CommitQuantity) AS AvailQuantity
FROM MST_Bin B
            INNER JOIN IV_BinCache BC ON B.BinID = BC.BinID AND (BC.OHQuantity - BC.CommitQuantity) > 0
GROUP BY B.BinID, B.Code, BC.ProductID, BC.LocationID, BC. InspStatus
'
GO
/****** Object:  View [dbo].[V_BinItem]    Script Date: 05/06/2010 12:53:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[V_BinItem]'))
EXEC dbo.sp_executesql @statement = N'CREATE VIEW [V_BinItem] AS
SELECT     BC.InspStatus, B.BinID, B.Code, BC.ProductID, BC.LocationID, itm.Code AS ProductCode, SUM(ISNULL(BC.OHQuantity, 0) - ISNULL(BC.CommitQuantity, 0)) AS AvailQuantity
FROM         dbo.IV_BinCache BC INNER JOIN
                      dbo.MST_BIN B ON BC.BinID = B.BinID INNER JOIN
                      dbo.ITM_Product itm ON BC.ProductID = itm.ProductID
GROUP BY BC.InspStatus, B.BinID, B.Code, BC.ProductID, BC.LocationID, itm.Code
'
GO
/****** Object:  View [dbo].[v_LocationNoScecurity]    Script Date: 05/06/2010 12:53:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_LocationNoScecurity]'))
EXEC dbo.sp_executesql @statement = N'CREATE VIEW [v_LocationNoScecurity]
AS
 SELECT LocationID, Code, Name, Type, ManufacturingAccess, SaleAccess, Bin, MasterLocationID, LocationTypeID 
 FROM mst_location
'
GO
/****** Object:  View [dbo].[V_LocalVendor]    Script Date: 05/06/2010 12:53:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[V_LocalVendor]'))
EXEC dbo.sp_executesql @statement = N'CREATE VIEW [V_LocalVendor] AS
	SELECT PartyID, Code, Name, Address, Website, Phone, Fax
	FROM MST_Party
	WHERE (Type = 1 OR Type = 2 OR Type = 3)
		AND (PartyID IN (SELECT PartyID
		FROM MST_Party P
		LEFT JOIN MST_CCN ON MST_CCN.CountryID = P.CountryID
		WHERE MST_CCN.Code = ''MAP''))
'
GO
/****** Object:  View [dbo].[v_CategoryOfProductionLine]    Script Date: 05/06/2010 12:53:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_CategoryOfProductionLine]'))
EXEC dbo.sp_executesql @statement = N'CREATE VIEW [v_CategoryOfProductionLine]
AS
SELECT DISTINCT ITM_Category.CategoryID, ITM_Category.Code, ITM_Category.Description,
 PRO_ProductionLine.ProductionLineID
 FROM ITM_Category LEFT JOIN ITM_Product
 ON ITM_Category.CategoryID = ITM_Product.CategoryID
 JOIN PRO_ProductionLine on ITM_Product.ProductionLineID = PRO_ProductionLine.ProductionLineID
'
GO
/****** Object:  View [dbo].[V_VendorCustomer]    Script Date: 05/06/2010 12:53:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[V_VendorCustomer]'))
EXEC dbo.sp_executesql @statement = N'CREATE VIEW [V_VendorCustomer] AS
SELECT  PartyID, P.Name, P.Code, Type,C.Code MST_PartyCurrency,
Case when ((Type = 0) OR (Type = 2)) then 0 end AS Customer,
CASE when  ((Type = 1) OR (Type = 2) OR (Type=3)) then 1 end AS Vendor
FROM dbo.MST_Party P
LEFT JOIN MST_Currency C ON P.CurrencyID=C.CurrencyID
WHERE (DeleteReason = 0) OR (DeleteReason IS NULL)

-- v_CloseOrOpenPO
'
GO
/****** Object:  View [dbo].[v_vendor]    Script Date: 05/06/2010 12:53:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_vendor]'))
EXEC dbo.sp_executesql @statement = N'CREATE View [v_vendor]
as
(SELECT PartyID,Code,Name,Address FROM MST_Party where Type <>0)
'
GO
/****** Object:  View [dbo].[v_PartyWithCCN]    Script Date: 05/06/2010 12:53:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_PartyWithCCN]'))
EXEC dbo.sp_executesql @statement = N'CREATE VIEW [v_PartyWithCCN]
AS
SELECT PartyID, MST_Party.Code, MST_Party.Name, MST_Party.Address, 
MST_Party.WebSite, MST_Party.State, MST_Party.DeleteReason, MST_Party.Type, 
MST_Party.ZipPost, MST_Party.VATCode, MST_Party.CountryID, MST_Party.CityID, 
MST_Party.Phone, MST_Party.Fax, MST_Party.BankAccount, MST_Party.PaymentTermID,
ISNULL(MST_CCN.CCNID,0) AS CCNID
FROM MST_Party
LEFT JOIN MST_CCN
ON MST_Party.CountryID = MST_CCN.CountryID

'
GO
/****** Object:  View [dbo].[v_MSTParty]    Script Date: 05/06/2010 12:53:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_MSTParty]'))
EXEC dbo.sp_executesql @statement = N'CREATE VIEW [v_MSTParty]
AS
SELECT     PartyID,Code, Name, Address, Type
FROM         dbo.MST_Party
WHERE     (DeleteReason = 0) OR
          (DeleteReason IS NULL)
'
GO
/****** Object:  View [dbo].[V_LocationAndProductionLine]    Script Date: 05/06/2010 12:53:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[V_LocationAndProductionLine]'))
EXEC dbo.sp_executesql @statement = N'CREATE VIEW [V_LocationAndProductionLine]
AS
	select L.LocationID, L.Code, L.Name, L.Type, L.ManufacturingAccess,
		L.SaleAccess,L.Bin, L.MasterLocationID, L.LocationTypeID,
		PL.ProductionLineID
	from MST_Location L 
	Left join Pro_ProductionLine PL on PL.LocationID = L.LocationID
'
GO
/****** Object:  View [dbo].[v_WorkOrderForIssueMaterial]    Script Date: 05/06/2010 12:53:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_WorkOrderForIssueMaterial]'))
EXEC dbo.sp_executesql @statement = N'CREATE VIEW [v_WorkOrderForIssueMaterial]
AS
SELECT  TOP 100 PERCENT CAST(0 AS bit) AS SELECTED, 
 PRO_WorkOrderDetail.WorkOrderMasterID, 
 PRO_WorkOrderMaster.WorkOrderNo AS PRO_WorkOrderMasterWorkOrderNo, 
 PRO_WorkOrderDetail.WorkOrderDetailID, 
 PRO_WorkOrderDetail.Line, 
 PRO_WorkOrderDetail.ProductID, 
 ITM_Product.Code AS ITM_ProductCode, 
 ITM_Product.Description AS ITM_ProductDescription, 
 ITM_Product.Revision AS ITM_ProductRevision, 
 PRO_WorkOrderDetail.StockUMID, 
 MST_UnitOfMeasure.Code AS MST_UnitOfMeasureCode, 
 PRO_WorkOrderDetail.OrderQuantity, 
 PRO_WorkOrderDetail.StartDate, 
 PRO_WorkOrderDetail.DueDate, 
 PRO_WorkOrderDetail.Status, 
 PRO_WorkOrderMaster.MasterLocationID,
 PRO_ProductionLine.LocationID
FROM   PRO_WorkOrderDetail 
       INNER JOIN  PRO_WorkOrderMaster ON PRO_WorkOrderDetail.WorkOrderMasterID = PRO_WorkOrderMaster.WorkOrderMasterID
       INNER JOIN  ITM_Product ON PRO_WorkOrderDetail.ProductID = ITM_Product.ProductID 
       INNER JOIN  MST_UnitOfMeasure ON PRO_WorkOrderDetail.StockUMID = MST_UnitOfMeasure.UnitOfMeasureID
       INNER JOIN PRO_ProductionLine ON PRO_WorkOrderMaster.ProductionLineID = PRO_ProductionLine.ProductionLineID
	ORDER BY PRO_WorkOrderDetail.StartDate
'
GO
/****** Object:  View [dbo].[v_WORelease]    Script Date: 05/06/2010 12:53:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_WORelease]'))
EXEC dbo.sp_executesql @statement = N'CREATE VIEW [v_WORelease]
AS
SELECT    DISTINCT M.WorkOrderMasterID, M.WorkOrderNo, M.TransDate, M.MasterLocationID, D.Status, M.ProductionLineID
FROM       dbo.PRO_WorkOrderMaster M INNER JOIN
                dbo.PRO_WorkOrderDetail D ON M.WorkOrderMasterID = D.WorkOrderMasterID
'
GO
/****** Object:  View [dbo].[V_WODetailAndProductInfo]    Script Date: 05/06/2010 12:53:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[V_WODetailAndProductInfo]'))
EXEC dbo.sp_executesql @statement = N'CREATE VIEW [V_WODetailAndProductInfo]
AS
	SELECT    dbo.PRO_WorkOrderMaster.WorkOrderMasterID , dbo.PRO_WorkOrderMaster.WorkOrderNo, 
		dbo.PRO_WorkOrderDetail.WorkOrderDetailID,dbo.PRO_WorkOrderDetail.Line,
		dbo.PRO_WorkOrderDetail.Status, dbo.MST_UnitOfMeasure.Code AS UM,
		dbo.PRO_WorkOrderDetail.ProductID, dbo.ITM_Product.Code, 
		dbo.ITM_Product.Revision, dbo.ITM_Product.Description,
		dbo.PRO_WorkOrderMaster.ProductionLineID
	FROM         dbo.PRO_WorkOrderMaster 
		INNER JOIN    dbo.PRO_WorkOrderDetail
		 ON dbo.PRO_WorkOrderMaster.WorkOrderMasterID = dbo.PRO_WorkOrderDetail.WorkOrderMasterID AND 
		   dbo.PRO_WorkOrderMaster.WorkOrderMasterID = dbo.PRO_WorkOrderDetail.WorkOrderMasterID 
		INNER JOIN	dbo.ITM_Product
		ON dbo.PRO_WorkOrderDetail.ProductID = dbo.ITM_Product.ProductID AND 
                  dbo.PRO_WorkOrderDetail.ProductID = dbo.ITM_Product.ProductID
		INNER JOIN	dbo.MST_UnitOfMeasure ON dbo.ITM_Product.StockUMID = dbo.MST_UnitOfMeasure.UnitOfMeasureID
'
GO
/****** Object:  View [dbo].[v_WO_BOM_Planning]    Script Date: 05/06/2010 12:53:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_WO_BOM_Planning]'))
EXEC dbo.sp_executesql @statement = N'CREATE view [v_WO_BOM_Planning] as
(SELECT dbo.PRO_WorkOrderMaster.WorkOrderNo, dbo.PRO_WorkOrderMaster.TransDate, dbo.PRO_WorkOrderMaster.MasterLocationID, 
                      dbo.PRO_WorkOrderDetail.WorkOrderDetailID, dbo.PRO_WorkOrderDetail.OrderQuantity, dbo.PRO_WorkOrderDetail.DueDate, 
                      dbo.PRO_WorkOrderDetail.StartDate, dbo.PRO_WorkOrderDetail.ProductID, dbo.ITM_BOM.ComponentID, 
                      dbo.ITM_BOM.Quantity RequiredQuantity, dbo.PRO_WorkOrderDetail.StockUMID, dbo.ITM_BOM.LeadTimeOffset, 
                      dbo.ITM_BOM.Shrink
FROM         dbo.PRO_WorkOrderMaster INNER JOIN
                      dbo.PRO_WorkOrderDetail ON dbo.PRO_WorkOrderMaster.WorkOrderMasterID = dbo.PRO_WorkOrderDetail.WorkOrderMasterID 
INNER JOIN              ITM_BOM ON PRO_WorkOrderDetail.ProductID = ITM_BOM.ProductID
WHERE     dbo.PRO_WorkOrderDetail.Status = 2
)
'
GO
/****** Object:  View [dbo].[v_RemainWOForIssue]    Script Date: 05/06/2010 12:53:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_RemainWOForIssue]'))
EXEC dbo.sp_executesql @statement = N'CREATE VIEW [v_RemainWOForIssue]
AS
SELECT  PRO_WorkOrderMaster.WorkOrderMasterID, 
        PRO_WorkOrderMaster.MasterLocationID,
        PRO_WorkOrderMaster.WorkOrderNo, 
 PRO_WorkOrderMaster.TransDate,
 PRO_ProductionLine.LocationID
FROM    PRO_WorkOrderMaster
        INNER JOIN PRO_ProductionLine ON PRO_WorkOrderMaster.ProductionLineID = PRO_ProductionLine.ProductionLineID 
WHERE PRO_WorkOrderMaster.WorkOrderMasterID IN
 (SELECT WorkOrderMasterID 
 FROM v_ReleasedWOWithRemainNeededQty 
 WHERE RemainNeededQuantity > 0)
'
GO
/****** Object:  View [dbo].[v_RemainComponentForWOIssueWithParentInfo]    Script Date: 05/06/2010 12:53:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_RemainComponentForWOIssueWithParentInfo]'))
EXEC dbo.sp_executesql @statement = N'CREATE VIEW [v_RemainComponentForWOIssueWithParentInfo]
AS
SELECT DISTINCT 
      PRO_WorkOrderMaster.WorkOrderMasterID,
      PRO_WorkOrderMaster.WorkOrderNo AS PRO_WorkOrderMasterWorkOrderNo,
      PRO_WorkOrderDetail.WorkOrderDetailID,       
      PRO_WorkOrderDetail.Line PRO_WorkOrderDetailLine,
      ITM_Product.Code AS ITM_ProductCode,
      ITM_Product.Description AS ITM_ProductDescription,
      ITM_Product.Revision AS ITM_ProductRevision,
      ISNULL(ITM_Product.AllowNegativeQty,0) AllowNegativeQty,
      ITM_Category.Code as ITM_CategoryCode, 
      PRO_WorkOrderDetail.StartDate,
      PRO_WorkOrderDetail.DueDate, 
      PRO_WorkOrderDetail.OrderQuantity,
      ITM_Product.MasterLocationID,
      PRO_ProductionLine.LocationID as ProductionLineLocationID,
      ITM_Product.LocationID, 
      MST_Location.Code MST_LocationCode, 
      ITM_Product.BinID,
      MST_Bin.Code MST_BinCode, 
      ITM_Product.ProductID,
      parent.Code as ParentCode,
      parent.Description as ParentName,
      parent.Revision as ParentRevision,   
      ITM_Product.LotSize,
      ITM_Product.StockUMID,
      ITM_Product.LotControl,
      MST_UnitOfMeasure.Code AS MST_UnitOfMeasureCode,
      v_ReleasedWOWithRemainNeededQty.Shrink,
      ISNULL(v_ReleasedWOWithRemainNeededQty.NeededQuantity,0) as RequiredQuantity,
      ISNULL(v_ReleasedWOWithRemainNeededQty.RemainNeededQuantity,0) CommitQuantity,
      ISNULL(v_ReleasedWOWithRemainNeededQty.CommitedQuantity,0) CommitedQuantity,
      ISNULL(v_ReleasedWOWithRemainNeededQty.BomQuantity,0) BomQuantity,
      ISNULL(BC.OHQuantity - ISNULL(BC.CommitQuantity,0),0) AvailableQuantity,
      v_ReleasedWOWithRemainNeededQty.ProductID as ComponentID,
      MST_Party.Code as MST_PartyCode,
      MST_Party.Name as MST_PartyName
 FROM  v_ReleasedWOWithRemainNeededQty
      INNER JOIN PRO_WorkOrderMaster ON v_ReleasedWOWithRemainNeededQty.WorkOrderMasterID = PRO_WorkOrderMaster.WorkOrderMasterID 
      INNER JOIN PRO_ProductionLine ON  PRO_ProductionLine.ProductionLineID = PRO_WorkOrderMaster.ProductionLineID
      INNER JOIN PRO_WorkOrderDetail ON v_ReleasedWOWithRemainNeededQty.WorkOrderDetailID = PRO_WorkOrderDetail.WorkOrderDetailID  
      INNER JOIN ITM_Product ON v_ReleasedWOWithRemainNeededQty.ProductID = ITM_Product.ProductID
      INNER JOIN ITM_Product parent ON PRO_WorkOrderDetail.ProductID = parent.ProductID       
      INNER JOIN MST_UnitOfMeasure ON ITM_Product.StockUMID = MST_UnitOfMeasure.UnitOfMeasureID
      JOIN MST_Location ON MST_Location.LocationID = ITM_Product.LocationID
      JOIN MST_Bin ON MST_Bin.BinID = ITM_Product.BinID
      JOIN IV_BinCache BC ON MST_Bin.BinID = BC.BinID AND ITM_Product.ProductID = BC.ProductID
      LEFT JOIN ITM_Category ON ITM_Product.CategoryID = ITM_Category.CategoryID
      LEFT JOIN MST_Party ON ITM_Product.PrimaryVendorID = MST_Party.PartyID
WHERE ISNULL(v_ReleasedWOWithRemainNeededQty.RemainNeededQuantity,0) > 0
'
GO
/****** Object:  View [dbo].[v_RemainComponentForWOIssue]    Script Date: 05/06/2010 12:53:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_RemainComponentForWOIssue]'))
EXEC dbo.sp_executesql @statement = N'CREATE VIEW [v_RemainComponentForWOIssue]
AS
SELECT
      PRO_WorkOrderMaster.WorkOrderMasterID,
      PRO_WorkOrderMaster.WorkOrderNo AS PRO_WorkOrderMasterWorkOrderNo,
      PRO_WorkOrderDetail.WorkOrderDetailID,
      PRO_WorkOrderDetail.Line,
      PRO_WorkOrderDetail.StartDate,
      PRO_WorkOrderDetail.DueDate, 
      PRO_WorkOrderDetail.OrderQuantity,
      ITM_Product.MasterLocationID,
      PRO_ProductionLine.LocationID as ProductionLineLocationID,
      ITM_Product.LocationID,	
      ITM_Product.BinID,
      ITM_Product.ProductID,
      ITM_Product.Code AS ITM_ProductCode,
      ITM_Product.Revision AS ITM_ProductRevision,
      ITM_Product.Description AS ITM_ProductDescription,
      ITM_Product.LotSize,
      ITM_Product.StockUMID,
      ITM_Product.LotControl,
      MST_UnitOfMeasure.Code AS MST_UnitOfMeasureCode,
      v_ReleasedWOWithRemainNeededQty.Shrink,
      v_ReleasedWOWithRemainNeededQty.NeededQuantity as RequiredQuantity,
      v_ReleasedWOWithRemainNeededQty.RemainNeededQuantity,
      v_ReleasedWOWithRemainNeededQty.ProductID as ComponentID
 
FROM  v_ReleasedWOWithRemainNeededQty
      INNER JOIN PRO_WorkOrderMaster ON v_ReleasedWOWithRemainNeededQty.WorkOrderMasterID = PRO_WorkOrderMaster.WorkOrderMasterID 
      INNER JOIN PRO_ProductionLine ON  PRO_ProductionLine.ProductionLineID = PRO_WorkOrderMaster.ProductionLineID
      INNER JOIN PRO_WorkOrderDetail ON v_ReleasedWOWithRemainNeededQty.WorkOrderDetailID = PRO_WorkOrderDetail.WorkOrderDetailID  
      INNER JOIN ITM_Product ON v_ReleasedWOWithRemainNeededQty.ProductID = ITM_Product.ProductID      
      INNER JOIN MST_UnitOfMeasure ON ITM_Product.StockUMID = MST_UnitOfMeasure.UnitOfMeasureID
 
WHERE v_ReleasedWOWithRemainNeededQty.RemainNeededQuantity > 0
'
GO
/****** Object:  View [dbo].[v_ReleaseWorkOrder]    Script Date: 05/06/2010 12:53:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_ReleaseWorkOrder]'))
EXEC dbo.sp_executesql @statement = N'CREATE VIEW [v_ReleaseWorkOrder] AS
SELECT  WOM.MasterLocationID, WOM.WorkOrderMasterID, WOM.WorkOrderNo, WOD.ProductID, WOD.Status
FROM    dbo.PRO_WorkOrderMaster WOM INNER JOIN
	dbo.PRO_WorkOrderDetail WOD ON WOD.WorkOrderMasterID = WOM.WorkOrderMasterID
'
GO
/****** Object:  View [dbo].[v_ReleasedWOWithLocation]    Script Date: 05/06/2010 12:53:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_ReleasedWOWithLocation]'))
EXEC dbo.sp_executesql @statement = N'CREATE VIEW [v_ReleasedWOWithLocation]
AS
SELECT  PRO_WorkOrderMaster.WorkOrderMasterID, 
        PRO_WorkOrderMaster.MasterLocationID,
        PRO_WorkOrderMaster.WorkOrderNo, 
	PRO_WorkOrderMaster.TransDate,
	PRO_ProductionLine.LocationID
FROM   PRO_WorkOrderMaster
       INNER JOIN PRO_ProductionLine ON PRO_WorkOrderMaster.ProductionLineID = PRO_ProductionLine.ProductionLineID
WHERE PRO_WorkOrderMaster.WorkOrderMasterID IN
	(SELECT PRO_WorkOrderDetail.WorkOrderMasterID 
	FROM PRO_WorkOrderDetail 
	WHERE PRO_WorkOrderDetail.Status = 2)
'
GO
/****** Object:  View [dbo].[v_ReleasedWorkOrderWithProductID]    Script Date: 05/06/2010 12:53:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_ReleasedWorkOrderWithProductID]'))
EXEC dbo.sp_executesql @statement = N'CREATE VIEW [v_ReleasedWorkOrderWithProductID] AS
SELECT DISTINCT WOM.WorkOrderMasterID, WOM.MasterLocationID,
            WOM.WorkOrderNo, WOM.TransDate, WOD.ProductID
FROM  PRO_WorkOrderMaster WOM
INNER JOIN PRO_WorkOrderDetail WOD ON WOD.WorkOrderMasterID=WOM.WorkOrderMasterID
            AND WOD.Status = 2
'
GO
/****** Object:  View [dbo].[v_ReleasedWorkOrder]    Script Date: 05/06/2010 12:53:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_ReleasedWorkOrder]'))
EXEC dbo.sp_executesql @statement = N'CREATE VIEW [v_ReleasedWorkOrder]
AS
(
SELECT            PRO_WorkOrderMaster.WorkOrderMasterID, PRO_WorkOrderMaster.MasterLocationID,
            PRO_WorkOrderMaster.WorkOrderNo, PRO_WorkOrderMaster.TransDate
FROM   dbo.PRO_WorkOrderMaster
WHERE PRO_WorkOrderMaster.WorkOrderMasterID IN
            (SELECT PRO_WorkOrderDetail.WorkOrderMasterID 
            FROM dbo.PRO_WorkOrderDetail WHERE PRO_WorkOrderDetail.Status = 2)
)
'
GO
/****** Object:  View [dbo].[v_ReleasedWO]    Script Date: 05/06/2010 12:53:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_ReleasedWO]'))
EXEC dbo.sp_executesql @statement = N'CREATE VIEW [v_ReleasedWO]
AS
 SELECT    DISTINCT M.WorkOrderMasterID, M.WorkOrderNo, M.TransDate, 
 M.MasterLocationID, D.Status, M.ProductionLineID
 FROM       dbo.PRO_WorkOrderMaster M 
 INNER JOIN dbo.PRO_WorkOrderDetail D ON M.WorkOrderMasterID = D.WorkOrderMasterID
 WHERE D.WorkOrderDetailID NOT IN (SELECT WorkOrderDetailID FROM PRO_WorkOrderCompletion)
'
GO
/****** Object:  View [dbo].[v_ReleasedAndMFClosedWorkOrder]    Script Date: 05/06/2010 12:53:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_ReleasedAndMFClosedWorkOrder]'))
EXEC dbo.sp_executesql @statement = N'CREATE VIEW [v_ReleasedAndMFClosedWorkOrder]
AS
(
SELECT            PRO_WorkOrderMaster.WorkOrderMasterID, PRO_WorkOrderMaster.MasterLocationID,
            PRO_WorkOrderMaster.WorkOrderNo, PRO_WorkOrderMaster.TransDate, PRO_WorkOrderMaster.ProductionLineID
FROM   dbo.PRO_WorkOrderMaster
WHERE PRO_WorkOrderMaster.WorkOrderMasterID IN
            (SELECT PRO_WorkOrderDetail.WorkOrderMasterID
            FROM dbo.PRO_WorkOrderDetail WHERE PRO_WorkOrderDetail.Status = 2 OR PRO_WorkOrderDetail.Status = 3)
)
'
GO
/****** Object:  View [dbo].[v_PRO_IssueMaterialDetail]    Script Date: 05/06/2010 12:53:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_PRO_IssueMaterialDetail]'))
EXEC dbo.sp_executesql @statement = N'CREATE VIEW [v_PRO_IssueMaterialDetail]
AS
SELECT     dbo.PRO_IssueMaterialDetail.IssueMaterialDetailID, dbo.PRO_IssueMaterialDetail.Line, dbo.PRO_IssueMaterialDetail.IssueMaterialMasterID, 
                      dbo.PRO_IssueMaterialDetail.WorkOrderMasterID, dbo.PRO_WorkOrderMaster.WorkOrderNo AS PRO_WorkOrderMasterWorkOrderNo, 
                      dbo.PRO_IssueMaterialDetail.WorkOrderDetailID, dbo.PRO_WorkOrderDetail.Line AS PRO_WorkOrderDetailLine, 
                      dbo.PRO_IssueMaterialDetail.LocationID, dbo.MST_Location.Code AS MST_LocationCode, dbo.PRO_IssueMaterialDetail.BinID, 
                      dbo.MST_BIN.Code AS MST_BinCode, dbo.PRO_IssueMaterialDetail.ProductID, dbo.ITM_Product.Code AS ITM_ProductCode, 
                      dbo.ITM_Product.Description AS ITM_ProductDescription, dbo.ITM_Product.Revision AS ITM_ProductRevision, 
                      dbo.ITM_Category.Code AS ITM_CategoryCode, dbo.PRO_IssueMaterialDetail.StockUMID, dbo.MST_UnitOfMeasure.Code AS MST_UnitOfMeasureCode,
                       dbo.PRO_IssueMaterialDetail.CommitQuantity, dbo.PRO_IssueMaterialDetail.AvailableQuantity, CAST(0 AS decimal) AS CommitedQuantity, 
                      CAST(0 AS decimal) AS RequiredQuantity, dbo.PRO_IssueMaterialDetail.Lot, dbo.PRO_IssueMaterialDetail.Serial, 
                      dbo.PRO_IssueMaterialDetail.MasterLocationID, dbo.PRO_IssueMaterialDetail.QAStatus, dbo.MST_Party.Code AS MST_PartyCode, 
                      dbo.MST_Party.Name AS MST_PartyName, dbo.PRO_IssueMaterialDetail.BomQuantity
FROM         dbo.PRO_IssueMaterialDetail INNER JOIN
                      dbo.MST_Location ON dbo.PRO_IssueMaterialDetail.LocationID = dbo.MST_Location.LocationID INNER JOIN
                      dbo.ITM_Product ON dbo.PRO_IssueMaterialDetail.ProductID = dbo.ITM_Product.ProductID INNER JOIN
                      dbo.PRO_WorkOrderMaster ON dbo.PRO_IssueMaterialDetail.WorkOrderMasterID = dbo.PRO_WorkOrderMaster.WorkOrderMasterID INNER JOIN
                      dbo.PRO_WorkOrderDetail ON dbo.PRO_IssueMaterialDetail.WorkOrderDetailID = dbo.PRO_WorkOrderDetail.WorkOrderDetailID INNER JOIN
                      dbo.MST_UnitOfMeasure ON dbo.PRO_IssueMaterialDetail.StockUMID = dbo.MST_UnitOfMeasure.UnitOfMeasureID LEFT OUTER JOIN
                      dbo.MST_BIN ON dbo.PRO_IssueMaterialDetail.BinID = dbo.MST_BIN.BinID LEFT OUTER JOIN
                      dbo.ITM_Category ON dbo.ITM_Product.CategoryID = dbo.ITM_Category.CategoryID LEFT OUTER JOIN
                      dbo.MST_Party ON dbo.ITM_Product.PrimaryVendorID = dbo.MST_Party.PartyID
'
GO
/****** Object:  View [dbo].[v_WOReleaseForCompletion]    Script Date: 05/06/2010 12:53:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_WOReleaseForCompletion]'))
EXEC dbo.sp_executesql @statement = N'CREATE VIEW [v_WOReleaseForCompletion]
AS
SELECT   DISTINCT M.WorkOrderMasterID, M.WorkOrderNo, M.TransDate, M.MasterLocationID, M.ProductionLineID, D.Status, w.WorkCenterID
 FROM     dbo.PRO_WorkOrderMaster M 
 INNER JOIN dbo.PRO_WorkOrderDetail D ON M.WorkOrderMasterID = D.WorkOrderMasterID and D.Status=2
 --(SonHT modified 28-Mar)
 Inner join MST_WorkCenter w on w.ProductionLineID = m.ProductionLineID And w.IsMain = 1
'
GO
/****** Object:  View [dbo].[V_ProductInWorkCenter]    Script Date: 05/06/2010 12:53:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[V_ProductInWorkCenter]'))
EXEC dbo.sp_executesql @statement = N'CREATE VIEW [V_ProductInWorkCenter]
AS
SELECT     p.ProductID, p.Code, p.Revision, p.Description, p.SetupDate, p.VAT, p.ImportTax, p.ExportTax, p.SpecialTax, p.MakeItem, p.PartNumber, p.OtherInfo1, 
                      p.OtherInfo2, p.Length, p.Width, p.Height, p.Weight, p.FinishedGoods, p.ShelfLife, p.LotControl, p.QAStatus, p.Stock, p.PlanType, p.AutoConversion, 
                      p.OrderQuantity, p.LTRequisition, p.LTSafetyStock, p.OrderQuantityMultiple, p.ScrapPercent, p.MinimumStock, p.MaximumStock, 
                      p.ConversionTolerance, p.VoucherTolerance, p.ReceiveTolerance, p.IssueSize, p.LTFixedTime, p.LTVariableTime, p.LTOrderPrepare, 
                      p.LTShippingPrepare, p.LTSalesATP, p.ShipToleranceID, p.BuyerID, p.BOMDescription, p.BomIncrement, p.RoutingDescription, p.CreateDateTime, 
                      p.UpdateDateTime, p.CostMethod, p.RoutingIncrement, p.CCNID, p.CategoryID, p.CostCenterID, p.DeleteReasonID, p.DeliveryPolicyID, p.FormatCodeID,
                      p.FreightClassID, p.HazardID, p.OrderPolicyID, p.OrderRuleID, p.SourceID, p.StockUMID, p.SellingUMID, p.HeightUMID, p.WidthUMID, p.LengthUMID, 
                      p.BuyingUMID, p.WeightUMID, p.LotSize, p.MasterLocationID, p.LocationID, p.BinID, p.PrimaryVendorID, p.VendorLocationID, p.OrderPoint, 
                      p.SafetyStock, p.AGCID, p.ParentProductID, p.LTDockToStock, p.PartNameVN, p.LicenseFee, p.InventorID, p.ProductTypeID, p.TaxCode, p.ListPrice, 
                      p.VendorCurrencyID, p.QuantitySet, p.ProductionLineID, p.CostCenterRateMasterID, p.ProductGroupID, p.MaxProduce, p.MinProduce, r.WorkCenterID, 
                      um.Code AS mst_unitofmeasurecode, Ca.Code AS ITM_CategoryCode, wc.Code AS mst_workcentercode, r.RoutingID
FROM         dbo.ITM_Product p INNER JOIN
                      dbo.ITM_Routing r ON p.ProductID = r.ProductID INNER JOIN
                      dbo.MST_WorkCenter wc ON r.WorkCenterID = wc.WorkCenterID AND wc.IsMain = 1 INNER JOIN
                      dbo.MST_UnitOfMeasure um ON p.StockUMID = um.UnitOfMeasureID Left JOIN
                      dbo.ITM_Category Ca ON p.CategoryID = Ca.CategoryID
'
GO
/****** Object:  View [dbo].[v_ProductInProductionLine]    Script Date: 05/06/2010 12:53:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_ProductInProductionLine]'))
EXEC dbo.sp_executesql @statement = N'CREATE VIEW [v_ProductInProductionLine]
AS
SELECT DISTINCT  PRO_ProductionLine.Code PLCode,
 ITM_Category.Code AS Category, 
 ITM_Product.ProductID, 
 ITM_Product.Code, 
 ITM_Product.Revision, 
ITM_Product.Description, 
ITM_Product.SetupDate, 
ITM_Product.VAT, 
ITM_Product.ImportTax, 
ITM_Product.ExportTax, 
ITM_Product.SpecialTax, 
ITM_Product.MakeItem, 
ITM_Product.PartNumber, 
ITM_Product.OtherInfo1, 
ITM_Product.OtherInfo2, 
ITM_Product.Length, 
ITM_Product.Width, 
ITM_Product.Height, 
ITM_Product.Weight, 
ITM_Product.FinishedGoods, 
ITM_Product.ShelfLife, 
ITM_Product.LotControl, 
ITM_Product.QAStatus, 
ITM_Product.Stock, 
ITM_Product.PlanType, 
ITM_Product.AutoConversion, 
ITM_Product.OrderQuantity, 
ITM_Product.LTRequisition, 
ITM_Product.LTSafetyStock, 
ITM_Product.OrderQuantityMultiple, 
ITM_Product.ScrapPercent, 
ITM_Product.MinimumStock, 
ITM_Product.MaximumStock, 
ITM_Product.ConversionTolerance, 
ITM_Product.VoucherTolerance, 
ITM_Product.ReceiveTolerance, 
ITM_Product.IssueSize, 
ITM_Product.LTFixedTime, 
ITM_Product.LTVariableTime, 
ITM_Product.LTOrderPrepare, 
ITM_Product.LTShippingPrepare, 
ITM_Product.LTSalesATP, 
ITM_Product.ShipToleranceID, 
ITM_Product.BuyerID, 
ITM_Product.BOMDescription, 
ITM_Product.BomIncrement, 
ITM_Product.RoutingDescription, 
ITM_Product.CreateDateTime, 
ITM_Product.UpdateDateTime, 
ITM_Product.CostMethod, 
ITM_Product.RoutingIncrement, 
ITM_Product.CCNID, 
ITM_Product.CategoryID, 
ITM_Product.CostCenterID, 
ITM_Product.DeleteReasonID, 
ITM_Product.DeliveryPolicyID, 
ITM_Product.FormatCodeID, 
ITM_Product.FreightClassID, 
ITM_Product.HazardID, 
ITM_Product.OrderPolicyID, 
ITM_Product.OrderRuleID, 
ITM_Product.SourceID, 
ITM_Product.StockUMID, 
ITM_Product.SellingUMID, 
 ITM_Product.HeightUMID, 
 ITM_Product.WidthUMID, 
 ITM_Product.LengthUMID, 
 ITM_Product.BuyingUMID, 
 ITM_Product.WeightUMID, 
 ITM_Product.LotSize, 
 ITM_Product.MasterLocationID, 
 ITM_Product.LocationID, 
 ITM_Product.BinID, 
 ITM_Product.PrimaryVendorID, 
 ITM_Product.VendorLocationID, 
 ITM_Product.OrderPoint, 
 ITM_Product.SafetyStock, 
 ITM_Product.AGCID, 
 ITM_Product.ParentProductID, 
 ITM_Product.LTDockToStock, 
 ITM_Product.PartNameVN, 
 ITM_Product.LicenseFee, 
 ITM_Product.InventorID, 
 ITM_Product.ProductTypeID, 
 ITM_Product.TaxCode, 
 ITM_Product.ListPrice, 
 ITM_Product.VendorCurrencyID, 
 ITM_Product.QuantitySet, 
 ITM_Product.ProductionLineID, 
 ITM_Product.CostCenterRateMasterID, 
 ITM_Product.ProductGroupID, 
 ITM_Product.MaxProduce, 
 ITM_Product.MinProduce
 FROM  ITM_Product
 join ITM_Routing on ITM_Product.ProductID = ITM_Routing.ProductID
 join MST_WorkCenter on MST_WorkCenter.WorkCenterID = ITM_Routing.WorkCenterID
 join PRO_ProductionLine on MST_WorkCenter.ProductionLineID = PRO_ProductionLine.ProductionLineID
 left join ITM_Category on ITM_Product.CategoryID = ITM_Category.CategoryID
'
GO
/****** Object:  View [dbo].[v_DetailedPOInvoiceMaster]    Script Date: 05/06/2010 12:53:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_DetailedPOInvoiceMaster]'))
EXEC dbo.sp_executesql @statement = N'CREATE VIEW [v_DetailedPOInvoiceMaster]
AS
SELECT  PO_InvoiceMaster.CCNID,
	PO_InvoiceMaster.InvoiceMasterID,
	PO_InvoiceMaster.InvoiceNo,
	PO_InvoiceMaster.PostDate,
	PO_InvoiceMaster.ExchangeRate,
	PO_InvoiceMaster.BLDate,
	PO_InvoiceMaster.InformDate,
	PO_InvoiceMaster.DeclarationDate,
        PO_InvoiceMaster.BLNumber,
	PO_InvoiceMaster.TaxInformNumber,
        PO_InvoiceMaster.TaxDeclarationNumber,
	PO_InvoiceMaster.TotalInlandAmount, 
	PO_InvoiceMaster.TotalCIFAmount, 
        PO_InvoiceMaster.TotalCIPAmount, 
	PO_InvoiceMaster.TotalImportTax, 
	PO_InvoiceMaster.TotalBeforeVATAmount, 
        PO_InvoiceMaster.TotalVATAmount,           
	PO_InvoiceMaster.PartyID, 
	PO_InvoiceMaster.CurrencyID, 
        PO_InvoiceMaster.CarrierID, 
	PO_InvoiceMaster.PaymentTermID, 
	PO_InvoiceMaster.DeliveryTermID,
	MST_Party.Code as MST_PartyCode, 
	MST_Party.Name as MST_PartyName,
	MST_PaymentTerm.Code AS MST_PaymentTermCode, 
	MST_DeliveryTerm.Code AS MST_DeliveryTermCode, 
	MST_Carrier.Code AS MST_CarrierCode,
        MST_Currency.Code AS MST_CurrencyCode
FROM    PO_InvoiceMaster 
	INNER JOIN MST_Party ON PO_InvoiceMaster.PartyID = MST_Party.PartyID 
	LEFT JOIN MST_DeliveryTerm ON PO_InvoiceMaster.DeliveryTermID = MST_DeliveryTerm.DeliveryTermID 
	LEFT JOIN MST_Carrier ON PO_InvoiceMaster.CarrierID = MST_Carrier.CarrierID 
	LEFT JOIN MST_PaymentTerm ON PO_InvoiceMaster.PaymentTermID = MST_PaymentTerm.PaymentTermID
	LEFT JOIN MST_Currency ON PO_InvoiceMaster.CurrencyID = MST_Currency.CurrencyID
'
GO
/****** Object:  View [dbo].[v_BinExceptDestroy]    Script Date: 05/06/2010 12:53:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_BinExceptDestroy]'))
EXEC dbo.sp_executesql @statement = N'CREATE VIEW [v_BinExceptDestroy]
AS
select BinID, Code, Name, LocationID, BinTypeID
from mst_bin where bintypeid != 3
'
GO
/****** Object:  View [dbo].[V_BIN_OK_AND_NG]    Script Date: 05/06/2010 12:53:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[V_BIN_OK_AND_NG]'))
EXEC dbo.sp_executesql @statement = N'CREATE VIEW [V_BIN_OK_AND_NG]
AS
SELECT mst_bin.* FROM mst_bin WHERE bintypeid = 1 or bintypeid = 2
'
GO
/****** Object:  View [dbo].[v_InvoiceToReturn]    Script Date: 05/06/2010 12:53:52 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_InvoiceToReturn]'))
EXEC dbo.sp_executesql @statement = N'
CREATE  VIEW [v_InvoiceToReturn]
AS
Select IVM.InvoiceNo, IVM.InvoiceMasterID, IVM.PostDate, IVM.ExchangeRate, 
IVM.TotalInlandAmount, IVM.TotalCIPAmount, IVM.TotalCIFAmount, IVM.TotalImportTax,
IVM.TotalBeforeVATAmount, IVM.TotalVATAmount, IVM.PartyID, PA.Code VendorCode,
PA.Name VendorName, IVM.CurrencyID,
CUR.Code MST_CurrencyCode, PL.Code MST_PartyLocationCode, PL.PartyLocationID
From PO_InvoiceMaster IVM
Inner Join MST_Party PA ON PA.PartyID = IVM.PartyID
Inner Join MST_Currency CUR ON CUR.CurrencyID = IVM.CurrencyID
Inner Join MST_PartyLocation PL ON PL.PartyID = IVM.PartyID
'
GO
/****** Object:  View [dbo].[v_TransferLocation]    Script Date: 05/06/2010 12:53:53 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_TransferLocation]'))
EXEC dbo.sp_executesql @statement = N'Create View [v_TransferLocation] as
Select Datepart(year,M.PostDate)years,Datepart(month,M.PostDate)months,D.ProductID,D.LocationID FromLocID,D.BinID FromBinID,M.ToLocationID ToLocID,M.ToBinID ToBinID,
Sum(D.CommitQuantity) Qty
From PRO_IssueMaterialDetail D
INNER JOIN PRO_IssueMaterialMaster M ON M.IssueMaterialMasterID=D.IssueMaterialMasterID
Group by Datepart(year,M.PostDate),Datepart(month,M.PostDate),D.LocationID,
D.BinID,M.ToLocationID,M.ToBinID,D.ProductID
Union all
select Datepart(year,M.PostDate)years,Datepart(month,M.PostDate)months,D.ProductID,
M.SourceLocationID FromLocID,M.SourceBinID FromBinID,M.DesLocationID ToLocID,M.DesBinID ToBinID,
Sum(Quantity) Qty 
From IV_MiscellaneousIssueDetail D
INNER JOIN IV_MiscellaneousIssueMaster M ON M.MiscellaneousIssueMasterID=D.MiscellaneousIssueMasterID
Group by Datepart(year,M.PostDate),Datepart(month,M.PostDate),
M.SourceLocationID,M.SourceBinID,M.DesLocationID,M.DesBinID,D.ProductID
Union all
select Datepart(year,M.PostDate)years,Datepart(month,M.PostDate)months,D.ComponentID ProductID,
D.FromLocationID FromLocID,D.FromBinID FromBinID,D.ToLocationID ToLocID,D.ToBinID ToBinID,
Sum(ScrapQuantity) Qty
From PRO_ComponentScrapDetail D
INNER JOIN PRO_ComponentScrapMaster M ON M.ComponentScrapMasterID=D.ComponentScrapMasterID
Group by Datepart(year,M.PostDate),Datepart(month,M.PostDate),
D.FromLocationID,D.FromBinID,D.ToLocationID,D.ToBinID,D.ComponentID
'
GO
/****** Object:  View [dbo].[v_OutputInPeriod]    Script Date: 05/06/2010 12:53:52 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_OutputInPeriod]'))
EXEC dbo.sp_executesql @statement = N'Create view [v_OutputInPeriod] as 
select DatePart(Year,Trans.PostDate) Years ,Datepart(Month,Trans.PostDate)as Months,
Trans.LocationID,Trans.BinID,Trans.ProductID,
(sum(isnull(-Trans.Quantity,0))
+ isnull((select sum(isnull(Trans2.Quantity,0))
from MST_TransactionHistory Trans2
INNER JOIN MST_Bin Bin ON BIN.BinID=Trans2.BinID
where TranTypeID IN (22)
AND BIN.BinTypeID<>3
AND Trans2.LocationID=Trans.LocationID 
AND Trans2.BinID=Trans.BinID
AND Trans2.ProductID=Trans.ProductID
AND Datepart(Year,Trans2.PostDate)=Datepart(Year,Trans.PostDate)
AND Datepart(Month,Trans2.PostDate)=Datepart(Month,Trans.PostDate)
Group by DatePart(Year,Trans2.PostDate),Datepart(Month,Trans2.PostDate),Trans2.LocationID,
Trans2.BinID,Trans2.ProductID),0))
+isnull((select sum(isnull(D.Quantity,0))
from IV_MiscellaneousIssueDetail D
INNER JOIN IV_MiscellaneousIssueMaster M ON M.MiscellaneousIssueMasterID=D.MiscellaneousIssueMasterID
INNER JOIN MST_Bin Bin1 ON BIN1.BinID=M.SourceBinID
INNER JOIN MST_Bin Bin2 ON BIN2.BinID=M.DesBinID
where BIN1.BinTypeID<>3
AND BIN2.BinTypeID=3
AND M.SourceLocationID=Trans.LocationID 
AND M.SourceBinID=Trans.BinID
AND D.ProductID=Trans.ProductID
AND Datepart(Year,M.PostDate)=Datepart(Year,Trans.PostDate)
AND Datepart(Month,M.PostDate)=Datepart(Month,Trans.PostDate)
Group by DatePart(Year,M.PostDate),Datepart(Month,M.PostDate),M.SourceLocationID,
M.SourceBinID,D.ProductID),0) AS QTY
from MST_TransactionHistory Trans
INNER JOIN MST_Bin Bin ON BIN.BinID=Trans.BinID
where TranTypeID IN (21,17,19)
AND Quantity<0
AND BIN.BinTypeID<>3
Group by DatePart(Year,Trans.PostDate),Datepart(Month,Trans.PostDate),Trans.LocationID,Trans.BinID,Trans.ProductID

'
GO
/****** Object:  View [dbo].[v_ProductGroupInfo]    Script Date: 05/06/2010 12:53:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_ProductGroupInfo]'))
EXEC dbo.sp_executesql @statement = N'CREATE VIEW [v_ProductGroupInfo]
AS
 SELECT DISTINCT PRO_ProductionGroup.ProductionGroupID, 
 PRO_ProductionGroup.Description, 
 PRO_ProductionGroup.GroupProductionMax, 
 PRO_ProductionLine.Code as PRO_ProductionLineCode,
        PRO_ProductionGroup.ProductionLineID, 
 MST_Department.CCNID, 
 MST_Department.DepartmentID
 FROM    PRO_ProductionGroup 
 INNER JOIN PRO_ProductionLine ON PRO_ProductionGroup.ProductionLineID = PRO_ProductionLine.ProductionLineID 
 INNER JOIN MST_Department ON PRO_ProductionLine.DepartmentID = MST_Department.DepartmentID
'
GO
/****** Object:  View [dbo].[v_SOToReturned]    Script Date: 05/06/2010 12:53:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_SOToReturned]'))
EXEC dbo.sp_executesql @statement = N'CREATE VIEW [v_SOToReturned]
AS
SELECT DISTINCT SOM.*
FROM         dbo.SO_SaleOrderMaster SOM INNER JOIN
             dbo.SO_SaleOrderDetail SOD ON SOM.SaleOrderMasterID = SOD.SaleOrderMasterID
WHERE     ((SELECT     isnull(SUM(isnull(b.CommitQuantity, 0)), 0)
                         FROM SO_DeliverySchedule a INNER JOIN
                              SO_CommitInventoryDetail b ON a.DeliveryScheduleID = b.DeliveryScheduleID INNER JOIN
                              SO_SaleOrderDetail c ON c.SaleOrderDetailID = a.SaleOrderDetailID
                         WHERE     a.SaleOrderDetailID = SOD.SaleOrderDetailID and b.Shipped = 1) -
                          (SELECT isnull(SUM(isnull(ReceiveQuantity, 0)), 0)
                           FROM SO_ReturnedGoodsDetail
                           WHERE SO_ReturnedGoodsDetail.SaleOrderDetailID = SOD.SaleOrderDetailID) > 0)
'
GO
/****** Object:  View [dbo].[V_SONotCompletedShip]    Script Date: 05/06/2010 12:53:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[V_SONotCompletedShip]'))
EXEC dbo.sp_executesql @statement = N'CREATE VIEW [V_SONotCompletedShip] AS
SELECT DISTINCT SOM.SaleOrderMasterID, SOM.Code, SOD.ProductID
FROM SO_SaleOrderDetail SOD
INNER JOIN SO_SaleOrderMaster SOM ON SOM.SaleOrderMasterID = SOD.SaleOrderMasterID
WHERE SOM.ShipCompleted = 0 OR SOM.ShipCompleted IS NULL
'
GO
/****** Object:  View [dbo].[v_SOMasterToCommit]    Script Date: 05/06/2010 12:53:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_SOMasterToCommit]'))
EXEC dbo.sp_executesql @statement = N'CREATE VIEW [v_SOMasterToCommit]
AS
SELECT            SaleOrderMasterID, Code, TransDate, CustomerPurchaseOrderNo, VAT, 
            VATRate, ExportTax, SpecialTax, ExportTaxRate, ShipCompleted, 
            SalesRepresentativeID, SpecialTaxRate, CCNID, CurrencyID, CarrierID, 
            PaymentTermsID, DeliveryTermsID, DiscountTermsID, PauseID, 
            TotalVATAmount, TotalExportAmount, TotalSpecialTaxAmount, TotalAmount, 
            TotalDiscountAmount, TotalNetAmount, PaymentMethodID, Priority, 
            BuyingLocID, ShipToLocID, BillToLocID, PartyContactID, SaleStatusID, 
            CancelReasonID, SaleTypeID, PartyID, LocationID, ExchangeRate, 
            ShipFromLocID
FROM SO_SaleOrderMaster WHERE SaleOrderMasterID IN
(SELECT           DISTINCT SaleOrderMasterID
FROM   SO_SaleOrderDetail JOIN SO_DeliverySchedule
            ON SO_SaleOrderDetail.SaleOrderDetailID = SO_DeliverySchedule.SaleOrderDetailID
WHERE
SO_DeliverySchedule.DeliveryQuantity - 
            (SELECT ISNULL(SUM(CommitQuantity), 0) FROM SO_CommitInventoryDetail
            WHERE SO_CommitInventoryDetail.DeliveryScheduleID = SO_DeliverySchedule.DeliveryScheduleID) > 0)
AND ISNULL(ShipCompleted, 0) = 0
'
GO
/****** Object:  View [dbo].[v_SOMasterNotRelease]    Script Date: 05/06/2010 12:53:52 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_SOMasterNotRelease]'))
EXEC dbo.sp_executesql @statement = N'CREATE  VIEW [v_SOMasterNotRelease]
AS
SELECT DISTINCT SOM.SaleOrderMasterID, SOM.Code, SOM.TransDate, C.Code Currency, SOM.TotalAmount, SOM.TotalNetAmount, Party.Code Party, SOM.CCNID
 FROM So_deliveryschedule DS 
 INNER JOIN SO_SaleOrderDetail SOD on SOD.Saleorderdetailid = DS.SaleOrderDetailID 
 INNER JOIN SO_SaleOrderMaster SOM on SOM.Saleordermasterid = SOD.SaleOrderMasterID  
 INNER JOIN MST_Currency C ON SOM.CurrencyID = C.CurrencyID
 INNER JOIN MST_Party Party ON SOM.PartyID = Party.PartyID
 INNER JOIN ITM_Product P ON SOD.ProductID = P.ProductID  
 INNER JOIN MST_UnitOfMeasure AS UM ON UM.UNITOFMEASUREID = P.SELLINGUMID
'
GO
/****** Object:  View [dbo].[v_SOMasterForShippingManagement]    Script Date: 05/06/2010 12:53:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_SOMasterForShippingManagement]'))
EXEC dbo.sp_executesql @statement = N'CREATE VIEW [v_SOMasterForShippingManagement]
AS
SELECT     dbo.SO_SaleOrderMaster.SaleOrderMasterID, dbo.SO_SaleOrderMaster.Code, dbo.SO_SaleOrderMaster.TransDate, dbo.SO_SaleOrderMaster.CCNID, 
                      dbo.SO_SaleOrderMaster.TotalAmount, dbo.SO_SaleOrderMaster.ShipFromLocID, dbo.SO_SaleOrderMaster.ShipCompleted, 
                      dbo.SO_SaleOrderMaster.TypeID, dbo.MST_MasterLocation.Code AS MasLoc, dbo.SO_SaleType.Code AS SaleType, 
                      dbo.MST_Party.Code AS CustomerCode, dbo.MST_Party.Name AS CustomerName
						, PT.Code AS MST_PaymentTermCode

FROM         dbo.SO_SaleOrderMaster INNER JOIN
                      dbo.MST_MasterLocation ON dbo.SO_SaleOrderMaster.ShipFromLocID = dbo.MST_MasterLocation.MasterLocationID INNER JOIN
                      dbo.MST_Party ON dbo.SO_SaleOrderMaster.PartyID = dbo.MST_Party.PartyID LEFT OUTER JOIN
                      dbo.SO_SaleType ON dbo.SO_SaleOrderMaster.SaleTypeID = dbo.SO_SaleType.SaleTypeID
			LEFT OUTER JOIN
                      dbo.MST_PaymentTerm PT ON dbo.MST_Party.PaymentTermID = PT.PaymentTermID

WHERE     (dbo.SO_SaleOrderMaster.SaleOrderMasterID IN
                          (SELECT DISTINCT SaleOrderMasterID
                            FROM          SO_SaleOrderDetail INNER JOIN
                                                  SO_DeliverySchedule ON SO_SaleOrderDetail.SaleOrderDetailID = SO_DeliverySchedule.SaleOrderDetailID
                            WHERE      (SELECT     SUM(IsNull(CommitQuantity, 0))
                                                    FROM          SO_CommitInventoryDetail
                                                    WHERE      SO_CommitInventoryDetail.DeliveryScheduleID = SO_DeliverySchedule.DeliveryScheduleID AND IsNull(Shipped, 0) = 0) > 0)) 
--                      AND (ISNULL(dbo.SO_SaleOrderMaster.ShipCompleted, 0) = 0)


'
GO
/****** Object:  View [dbo].[v_SaleOrderForWOLine]    Script Date: 05/06/2010 12:53:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_SaleOrderForWOLine]'))
EXEC dbo.sp_executesql @statement = N'CREATE VIEW [v_SaleOrderForWOLine]
AS
(
SELECT SO_SaleOrderMaster.SaleOrderMasterID, SO_SaleOrderMaster.Code as SaleOrderCode, 
 SO_SaleOrderDetail.SaleOrderLine, ITM_Product.Code, 
 SO_SaleOrderMaster.TransDate, SO_SaleOrderMaster.CustomerPurchaseOrderNo, 
 SO_SaleOrderMaster.VAT, SO_SaleOrderMaster.VATRate, 
 SO_SaleOrderMaster.ExportTax, SO_SaleOrderMaster.SpecialTax, 
 SO_SaleOrderMaster.ExportTaxRate, SO_SaleOrderMaster.ShipCompleted, 
 SO_SaleOrderMaster.SpecialTaxRate, SO_SaleOrderMaster.TotalVATAmount, 
 SO_SaleOrderMaster.TotalExportAmount, SO_SaleOrderMaster.TotalSpecialTaxAmount, 
 SO_SaleOrderMaster.TotalAmount, SO_SaleOrderMaster.TotalDiscountAmount, 
 SO_SaleOrderMaster.TotalNetAmount, SO_SaleOrderMaster.Priority, 
 SO_SaleOrderMaster.ExchangeRate, SO_SaleOrderDetail.SaleOrderDetailID, 
 SO_SaleOrderDetail.ProductID, SO_SaleOrderMaster.ShipFromLocID,
 SO_SaleOrderMaster.CCNID
FROM dbo.SO_SaleOrderMaster JOIN dbo.SO_SaleOrderDetail ON
  SO_SaleOrderMaster.SaleOrderMasterID = SO_SaleOrderDetail.SaleOrderMasterID
 JOIN ITM_Product ON
  SO_SaleOrderDetail.ProductID = ITM_Product.ProductID
)
'
GO
/****** Object:  View [dbo].[v_SaleInvoice]    Script Date: 05/06/2010 12:53:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_SaleInvoice]'))
EXEC dbo.sp_executesql @statement = N'CREATE VIEW [v_SaleInvoice] AS
SELECT DISTINCT SO_SaleOrderMaster.SaleOrderMasterID, SO_SaleOrderMaster.Code, SO_SaleOrderMaster.TransDate, SO_SaleOrderMaster.CCNID, 
SO_SaleOrderMaster.TotalAmount, SO_SaleOrderMaster.ShipFromLocID, SO_SaleOrderMaster.ShipCompleted, 
SO_SaleOrderMaster.TypeID, MST_MasterLocation.Code AS MasLoc, SO_SaleType.Code AS SaleType, 
MST_Party.Code AS CustomerCode, MST_Party.Name AS CustomerName, PT.Code AS MST_PaymentTermCode
FROM SO_SaleOrderMaster JOIN MST_MasterLocation ON SO_SaleOrderMaster.ShipFromLocID = MST_MasterLocation.MasterLocationID
JOIN MST_Party ON SO_SaleOrderMaster.PartyID = MST_Party.PartyID
LEFT JOIN SO_SaleType ON SO_SaleOrderMaster.SaleTypeID = SO_SaleType.SaleTypeID
LEFT JOIN MST_PaymentTerm PT ON MST_Party.PaymentTermID = PT.PaymentTermID
JOIN SO_SaleOrderDetail ON SO_SaleOrderMaster.SaleOrderMasterID = SO_SaleOrderDetail.SaleOrderMasterID
JOIN SO_DeliverySchedule ON SO_SaleOrderDetail.SaleOrderDetailID = SO_DeliverySchedule.SaleOrderDetailID
WHERE DeliveryScheduleID NOT IN (SELECT DISTINCT DeliveryScheduleID
	FROM SO_ConfirmshipDetail)
AND (ISNULL(SO_SaleOrderMaster.ShipCompleted, 0) = 0)
'
GO
/****** Object:  View [dbo].[v_ProductForCustomer]    Script Date: 05/06/2010 12:53:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_ProductForCustomer]'))
EXEC dbo.sp_executesql @statement = N'CREATE VIEW [v_ProductForCustomer]
AS
SELECT DISTINCT P.ProductID, P.Code, P.Description, P.Revision, P.SellingUMID,P.MasterLocationID,
            P.LocationID,P.BinID,ML.Code as MST_MasterLocationCode,
            L.Code as MST_LocationCode,
            B.Code as MST_BinCode, L.Bin,            
            U.Code as MST_UnitOfMeasureCode, p.StockUMID,
            SOM.PartyID, PartyContactID, ShipFromLocID, BuyingLocID, P.CategoryID
FROM         dbo.SO_SaleOrderDetail SOD 
inner join   dbo.SO_DeliverySchedule D on SOD.SaleOrderDetailID = D.SaleOrderDetailID
inner join   dbo.SO_CommitInventoryDetail  SOCM on SOCM.DeliveryScheduleID = D.DeliveryScheduleID
INNER JOIN   dbo.SO_SaleOrderMaster SOM ON SOD.SaleOrderMasterID = SOM.SaleOrderMasterID LEFT OUTER JOIN
             dbo.ITM_Product P ON P.ProductID = SOD.ProductID
 Left join MST_MasterLocation ML on ML.MasterLocationID = P.MasterLocationID
 Left join MST_Location L on L.LocationID = P.LocationID
 Left join MST_Bin B on B.BinID = P.BinID
 Left join MST_UnitOfMeasure U on U.UnitOfMeasureID = P.SellingUMID
 Where SOCM.Shipped = 1
'
GO
/****** Object:  View [dbo].[v_WCCapacityByCategory]    Script Date: 05/06/2010 12:53:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_WCCapacityByCategory]'))
EXEC dbo.sp_executesql @statement = N'CREATE VIEW [v_WCCapacityByCategory]
AS
SELECT  vCapacity.ProductID,
               vCapacity.CategoryID,
               vCapacity.WorkCenterID,
               vCapacity.IsMain,
               vCapacity.Code,
               Max(vCapacity.LeadTime) as LeadTime,
               vCapacity.QuantitySet,
               vCapacity.Capacity 
FROM (SELECT  TOP 100 PERCENT        
            ITM_Routing.ProductID,
            ITM_Category.CategoryID, 
            MST_WorkCenter.WorkCenterID,            
            MST_WorkCenter.IsMain, 
            MST_WorkCenter.Code,
            Case ITM_Routing.Pacer
              When ''L''   then ISNULL(ITM_Routing.LaborRunTime + ITM_Routing.LaborSetupTime, 0)
              When ''M''  then ISNULL(ITM_Routing.MachineRunTime + ITM_Routing.MachineSetupTime, 0)
              When ''B''  then ISNULL(ITM_Routing.LaborRunTime + ITM_Routing.LaborSetupTime + ITM_Routing.MachineRunTime + ITM_Routing.MachineSetupTime, 0)
            End as LeadTime,
                Case 
                When ITM_Product.QuantitySet IS NULL Then 1
                        When ITM_Product.QuantitySet = 0 Then 1
                        Else ITM_Product.QuantitySet
            End As QuantitySet,
            ISNULL(PRO_WCCapacity.Capacity, 1) as Capacity
       FROM    ITM_Product 
            INNER JOIN ITM_Routing ON ITM_Product.ProductID = ITM_Routing.ProductID 
            INNER JOIN MST_WorkCenter ON ITM_Routing.WorkCenterID = MST_WorkCenter.WorkCenterID 
            INNER JOIN PRO_ProductionLine ON MST_WorkCenter.ProductionLineID = PRO_ProductionLine.ProductionLineID 
            INNER JOIN PRO_WCCapacity ON MST_WorkCenter.WorkCenterID = PRO_WCCapacity.WorkCenterID 
            INNER JOIN ITM_Category ON ITM_Product.CategoryID = ITM_Category.CategoryID
       WHERE  MST_WorkCenter.IsMain = 1
       ORDER BY LeadTime DESC           
) vCapacity
GROUP BY vCapacity.ProductID,
            vCapacity.CategoryID,
            vCapacity.WorkCenterID,
            vCapacity.IsMain,
            vCapacity.Code,
            vCapacity.QuantitySet,
            vCapacity.Capacity
'
GO
/****** Object:  View [dbo].[v_LeadTimeByMainWorkCenter]    Script Date: 05/06/2010 12:53:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_LeadTimeByMainWorkCenter]'))
EXEC dbo.sp_executesql @statement = N'CREATE VIEW [v_LeadTimeByMainWorkCenter]
AS
SELECT  TOP 100 PERCENT        
    ITM_Routing.ProductID,
    ITM_Category.CategoryID, 
    MST_WorkCenter.WorkCenterID,            
    MST_WorkCenter.IsMain, 
    MST_WorkCenter.Code,
    Case ITM_Routing.Pacer
      When ''L''   then ISNULL(ITM_Routing.LaborRunTime, 0) + ISNULL(ITM_Routing.LaborSetupTime, 0)
      When ''M''  then ISNULL(ITM_Routing.MachineRunTime, 0) + ISNULL(ITM_Routing.MachineSetupTime, 0)
      When ''B''  then ISNULL(ITM_Routing.LaborRunTime, 0) + ISNULL(ITM_Routing.LaborSetupTime, 0) + ISNULL(ITM_Routing.MachineRunTime, 0) + ISNULL(ITM_Routing.MachineSetupTime, 0)
    End as LeadTime,
    Case 
        When ITM_Product.QuantitySet IS NULL Then 1
        When ITM_Product.QuantitySet = 0 Then 1
        Else ITM_Product.QuantitySet
    End As QuantitySet,
    ISNULL(PRO_WCCapacity.Capacity, 1) as Capacity
FROM    ITM_Product 
    INNER JOIN ITM_Routing ON ITM_Product.ProductID = ITM_Routing.ProductID 
    INNER JOIN MST_WorkCenter ON ITM_Routing.WorkCenterID = MST_WorkCenter.WorkCenterID 
    INNER JOIN PRO_WCCapacity ON MST_WorkCenter.WorkCenterID = PRO_WCCapacity.WorkCenterID 
    INNER JOIN ITM_Category ON ITM_Product.CategoryID = ITM_Category.CategoryID
WHERE  MST_WorkCenter.IsMain = 1
ORDER BY LeadTime DESC
'
GO
/****** Object:  View [dbo].[v_SelectUnclosedPO4Invoice]    Script Date: 05/06/2010 12:53:52 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_SelectUnclosedPO4Invoice]'))
EXEC dbo.sp_executesql @statement = N'CREATE VIEW [v_SelectUnclosedPO4Invoice]
AS
SELECT  DISTINCT 
            PO_PurchaseOrderMaster.PurchaseOrderMasterID, 
            PO_PurchaseOrderMaster.Code AS PO_PurchaseOrderMasterCode, 
            PO_PurchaseOrderMaster.OrderDate, 
            MST_Party.Name as MST_PartyName,
            MST_Currency.Code as MST_CurrencyCode,
            PO_PurchaseOrderMaster.TotalVAT,
            PO_PurchaseOrderMaster.TotalImportTax,
            PO_PurchaseOrderMaster.TotalAmount,
            PO_PurchaseOrderMaster.CCNID,
            PO_PurchaseOrderMaster.PartyID,
	    PO_PurchaseOrderMaster.MakerID
	
FROM    PO_PurchaseOrderDetail 
            INNER JOIN PO_PurchaseOrderMaster ON PO_PurchaseOrderDetail.PurchaseOrderMasterID = PO_PurchaseOrderMaster.PurchaseOrderMasterID
            INNER JOIN MST_Party ON MST_Party.PartyID = PO_PurchaseOrderMaster.PartyID
            LEFT JOIN MST_Currency ON MST_Currency.CurrencyID = PO_PurchaseOrderMaster.CurrencyID

WHERE (PO_PurchaseOrderDetail.Closed = 0 OR PO_PurchaseOrderDetail.Closed IS NULL)
   AND PO_PurchaseOrderDetail.ApproverID IS NOT NULL
'
GO
/****** Object:  View [dbo].[v_SelectPurchaseOrders]    Script Date: 05/06/2010 12:53:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_SelectPurchaseOrders]'))
EXEC dbo.sp_executesql @statement = N'CREATE VIEW [v_SelectPurchaseOrders] 
AS
SELECT  
            PO_PurchaseOrderMaster.PurchaseOrderMasterId,
            PO_PurchaseOrderMaster.Code as PO_PurchaseOrderMasterCode,
            PO_PurchaseOrderDetail.Line as PO_PurchaseOrderDetailLine,
            PO_PurchaseOrderMaster.OrderDate,
            PO_DeliverySchedule.DeliveryLine,
            ITM_Product.Code AS ITM_ProductCode,
            ITM_Product.Revision as ITM_ProductRevision,
            ITM_Product.Description as ITM_ProductDescription,
            ITM_Product.TaxCode,
            PO_PurchaseOrderMaster.DeliveryTermsID,
            PO_PurchaseOrderMaster.PaymentTermsID,
            PO_PurchaseOrderMaster.CarrierID,
            PO_PurchaseOrderMaster.PartyContactID,
            PO_DeliverySchedule.DeliveryQuantity, 
            PO_DeliverySchedule.ScheduleDate,
            PO_PurchaseOrderDetail.PurchaseOrderDetailId,
            PO_PurchaseOrderDetail.UnitPrice as PO_PurchaseOrderDetailUnitPrice,
            PO_PurchaseOrderDetail.VAT as PO_PurchaseOrderDetailVAT,
            PO_PurchaseOrderDetail.SpecialTax as PO_PurchaseOrderDetailSpecialTax,
            PO_PurchaseOrderDetail.ImportTax as PO_PurchaseOrderDetailImportTax,
            PO_PurchaseOrderDetail.BuyingUMID,
            MST_UnitOfMeasure.Code AS MST_UnitOfMeasureCode,
            ITM_Product.OtherInfo1,
            ITM_Product.PartNameVN,
            ITM_Product.ProductID,
            PO_PurchaseOrderMaster.CCNID,
            PO_PurchaseOrderMaster.PartyID,
            PO_PurchaseOrderDetail.Closed,
            PO_DeliverySchedule.DeliveryScheduleID,
     MST_Party.Code as MST_PartyCode,
                  ISNULL(PO_DeliverySchedule.ReceivedQuantity,0) ReceivedQuantity
FROM    PO_PurchaseOrderDetail
    INNER JOIN PO_PurchaseOrderMaster ON PO_PurchaseOrderDetail.PurchaseOrderMasterID = PO_PurchaseOrderMaster.PurchaseOrderMasterID 
    INNER JOIN PO_DeliverySchedule ON PO_PurchaseOrderDetail.PurchaseOrderDetailID = PO_DeliverySchedule.PurchaseOrderDetailID 
    INNER JOIN ITM_Product ON PO_PurchaseOrderDetail.ProductID = ITM_Product.ProductID 
    INNER JOIN MST_UnitOfMeasure ON PO_PurchaseOrderDetail.BuyingUMID = MST_UnitOfMeasure.UnitOfMeasureID
    LEFT JOIN MST_Party ON PO_PurchaseOrderMaster.MakerID = MST_Party.PartyID
WHERE ISNULL(PO_PurchaseOrderDetail.Closed,0) = 0
      AND PO_PurchaseOrderDetail.ApproverID IS NOT NULL
	AND ISNULL(PO_DeliverySchedule.ReceivedQuantity,0) < PO_DeliverySchedule.DeliveryQuantity
'
GO
/****** Object:  View [dbo].[v_ReceiptBySchedule]    Script Date: 05/06/2010 12:53:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_ReceiptBySchedule]'))
EXEC dbo.sp_executesql @statement = N'CREATE VIEW [v_ReceiptBySchedule]
AS
SELECT DISTINCT PO_PurchaseOrderMaster.Code AS PO_PurchaseOrderMasterCode, 
	    (ISNULL(PO_DeliverySchedule.DeliveryQuantity, 0) - 
	    ISNULL(PO_DeliverySchedule.ReceivedQuantity, 0)) AS ReceiveQuantity,
            PO_DeliverySchedule.ScheduleDate,
            DATEPART(year, PO_DeliverySchedule.ScheduleDate) ''Year'',
            DATEPART(month, PO_DeliverySchedule.ScheduleDate) ''Month'',
            DATEPART(day, PO_DeliverySchedule.ScheduleDate) ''Day'',
            DATEPART(hour, PO_DeliverySchedule.ScheduleDate) ''Hour'',
            PO_PurchaseOrderDetail.Line, ITM_Category.Code AS Category,
            ITM_Product.Code AS PartNumber, ITM_Product.Description AS PartName,
            ITM_Product.Revision AS Model,
            MST_Party.Code AS CustCode, MST_Party.Name AS CustName,
            MST_CCN.Code AS CCNCode, MST_CCN.Name AS CCNName,
            MST_UnitOfMeasure.Code AS MST_UnitOfMeasureCode,
            PO_PurchaseOrderDetail.StockUMID, PO_PurchaseOrderDetail.BuyingUMID,
            PO_DeliverySchedule.DeliveryScheduleID, ITM_Product.ProductID,
            PO_PurchaseOrderDetail.PurchaseOrderDetailID,
            PO_PurchaseOrderMaster.PurchaseOrderMasterID
FROM PO_DeliverySchedule
INNER JOIN PO_PurchaseOrderDetail
            ON PO_DeliverySchedule.PurchaseOrderDetailID = PO_PurchaseOrderDetail.PurchaseOrderDetailID
INNER JOIN PO_PurchaseOrderMaster
            ON PO_PurchaseOrderDetail.PurchaseOrderMasterID = PO_PurchaseOrderMaster.PurchaseOrderMasterID
INNER JOIN ITM_Product                                                    
            ON PO_PurchaseOrderDetail.ProductID = ITM_Product.ProductID
INNER JOIN MST_Party
            ON PO_PurchaseOrderMaster.PartyID = MST_Party.PartyID
INNER JOIN MST_CCN
            ON PO_PurchaseOrderMaster.CCNID = MST_CCN.CCNID
INNER JOIN MST_UnitOfMeasure
            ON PO_PurchaseOrderDetail.StockUMID = MST_UnitOfMeasure.UnitOfMeasureID
LEFT OUTER JOIN ITM_Category
            ON ITM_Product.CategoryID = ITM_Category.CategoryID
WHERE (ISNULL(PO_PurchaseOrderDetail.ApproverID, 0) > 0) 
AND (ISNULL(PO_PurchaseOrderDetail.Closed, 0) = 0)
AND (ISNULL(PO_DeliverySchedule.DeliveryQuantity, 0) - ISNULL(PO_DeliverySchedule.ReceivedQuantity, 0)) > 0
'
GO
/****** Object:  View [dbo].[v_PurchaseOrderOfItem]    Script Date: 05/06/2010 12:53:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_PurchaseOrderOfItem]'))
EXEC dbo.sp_executesql @statement = N'CREATE VIEW [v_PurchaseOrderOfItem]
AS
SELECT            DISTINCT ShipToLocID, InvToLocID, PO_PurchaseOrderMaster.MasterLocationID, 
            PO_PurchaseOrderMaster.Code AS ''PO_PurchaseOrderMasterCode'',
            OrderDate, PO_PurchaseOrderMaster.VAT, PO_PurchaseOrderMaster.ImportTax, 
            PO_PurchaseOrderMaster.SpecialTax, PO_PurchaseOrderMaster.PurchaseOrderMasterID, 
            CurrencyID, DeliveryTermsID, PaymentTermsID, 
            CarrierID, TotalImportTax, PO_PurchaseOrderMaster.BuyerID, TotalVAT, TotalSpecialTax, 
            PO_PurchaseOrderMaster.TotalAmount, TotalDiscount, TotalNetAmount, 
            PO_PurchaseOrderMaster.CCNID, PartyID, VendorLocID, PartyContactID, 
            PurchaseTypeID, DiscountTermID, PauseID, Priority, RecCompleted, Comment, 
            ExchangeRate, VendorSO, PORevision, Line, ITM_Product.ProductID,
            ITM_Product.Code AS ''ITM_ProductCode'', ITM_Product.Description, 
            ITM_Product.Revision, PO_PurchaseOrderDetail.UnitPrice, ISNULL(PO_PurchaseOrderDetail.ApproverID, 0) AS ''ApproverID'',
            PO_PurchaseOrderDetail.Closed, PO_PurchaseOrderDetail.PurchaseOrderDetailID,
            PO_PurchaseOrderDetail.OrderQuantity - ISNULL(PO_PurchaseOrderDetail.TotalDelivery, 0) AS ''ReceiveQuantity'',
            MST_UnitOfMeasure.Code AS ''MST_UnitOfMeasureCode'', PO_PurchaseOrderDetail.BuyingUMID,
            PO_PurchaseOrderDetail.StockUMID, PO_PurchaseOrderDetail.UMRate
FROM
PO_PurchaseOrderMaster JOIN PO_PurchaseOrderDetail
ON PO_PurchaseOrderMaster.PurchaseOrderMasterID = PO_PurchaseOrderDetail.PurchaseOrderMasterID
JOIN ITM_Product
ON PO_PurchaseOrderDetail.ProductID=ITM_Product.ProductID
JOIN MST_UnitOfMeasure
ON PO_PurchaseOrderDetail.BuyingUMID = MST_UnitOfMeasure.UnitOfMeasureID
WHERE  ApproverID > 0
AND Closed <> 1
'
GO
/****** Object:  View [dbo].[v_POReturnToVendor]    Script Date: 05/06/2010 12:53:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_POReturnToVendor]'))
EXEC dbo.sp_executesql @statement = N'CREATE VIEW [v_POReturnToVendor] AS
 SELECT DISTINCT POM.MasterLocationID, PORM.PurchaseOrderMasterID, POM.Code, POM.OrderDate, POM.PartyID, POM.VendorLocID
 FROM   dbo.PO_PurchaseOrderReceiptDetail PORM INNER JOIN
        dbo.PO_PurchaseOrderMaster POM ON POM.PurchaseOrderMasterID = PORM.PurchaseOrderMasterID
'
GO
/****** Object:  View [dbo].[v_PODeliver_Maker]    Script Date: 05/06/2010 12:53:52 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_PODeliver_Maker]'))
EXEC dbo.sp_executesql @statement = N'CREATE view [v_PODeliver_Maker] as
select M.PartyID,P1.Code as PartyCode,M.MakerID,P2.Code as MakerCode,D.ProductID,I.Code as PartNo,
I.Description as PartName,I.Revision as Model,UM.Code as UM,P1.CountryID,CAT.Code as Category,
Datepart(day,s.ScheduleDate)as DayinMonth,Datepart(month,s.ScheduleDate)as month,
Datepart(year,s.ScheduleDate) as year,
Sum(S.DeliveryQuantity)as OrderQty, I.CategoryID from PO_DeliverySchedule S
INNER JOIN PO_PurchaseOrderDetail D ON D.PurchaseOrderDetailID=S.PurchaseOrderDetailID
INNER JOIN PO_PurchaseOrderMaster M ON M.PurchaseOrderMasterID=D.PurchaseOrderMasterID
INNER JOIN MST_Party P1 ON M.PartyID=P1.PartyID
INNER JOIN MST_Party P2 ON M.MakerID=P2.PartyID
INNER JOIN ITM_Product I ON I.ProductID=D.ProductID
INNER JOIN MST_UnitOfMeasure UM ON UM.UnitOfMeasureID=I.StockUMID
LEFT JOIN ITM_Category CAT ON I.CategoryID=CAT.CategoryID
Group by M.MakerID, I.CategoryID, D.ProductID,Datepart(month,s.ScheduleDate),Datepart(day,s.ScheduleDate),M.PartyID,
P1.Code,P2.Code,I.Code,I.Description,I.Revision,UM.Code,P1.CountryID,Datepart(year,s.ScheduleDate),CAT.Code
'
GO
/****** Object:  View [dbo].[v_PO_PurchaseOrderMasterHasReceive]    Script Date: 05/06/2010 12:53:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_PO_PurchaseOrderMasterHasReceive]'))
EXEC dbo.sp_executesql @statement = N'Create view [v_PO_PurchaseOrderMasterHasReceive]
as
(
SELECT     MasterLocationID, Code, OrderDate, VAT, ImportTax, SpecialTax, PurchaseOrderMasterID, CurrencyID, DeliveryTermsID, PaymentTermsID, CarrierID, 
                      TotalImportTax, BuyerID, TotalVAT, TotalSpecialTax, TotalAmount, TotalDiscount, TotalNetAmount, CCNID, PartyID, VendorLocID, ShipToLocID, 
                      InvToLocID, PartyContactID, PurchaseTypeID, DiscountTermID, PauseID, Priority, RecCompleted, Comment, ExchangeRate, VendorSO
FROM         dbo.PO_PurchaseOrderMaster
WHERE     (PurchaseOrderMasterID IN
                          (SELECT     PurchaseOrderMasterID
                            FROM          PO_PurchaseOrderReceiptDetail))
)
'
GO
/****** Object:  View [dbo].[v_PO_NOT_Approve]    Script Date: 05/06/2010 12:53:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_PO_NOT_Approve]'))
EXEC dbo.sp_executesql @statement = N'CREATE VIEW [v_PO_NOT_Approve]
AS
SELECT DISTINCT a.PurchaseOrderMasterID, a.Code, a.CCNID, a.OrderDate, a.TotalAmount, a.RecCompleted
FROM         dbo.PO_PurchaseOrderMaster a INNER JOIN
                      dbo.PO_PurchaseOrderDetail b ON a.PurchaseOrderMasterID = b.PurchaseOrderMasterID
WHERE     (b.ApproverID IS NULL) AND (b.PurchaseOrderDetailID NOT IN
                          (SELECT     PurchaseOrderDetailID
                            FROM          PO_PurchaseOrderReceiptDetail))
'
GO
/****** Object:  View [dbo].[v_PO_Approve]    Script Date: 05/06/2010 12:53:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_PO_Approve]'))
EXEC dbo.sp_executesql @statement = N'CREATE VIEW [v_PO_Approve]
AS
SELECT DISTINCT a.PurchaseOrderMasterID, a.Code, a.CCNID, a.OrderDate, a.TotalAmount, a.RecCompleted

FROM         dbo.PO_PurchaseOrderMaster a INNER JOIN
             dbo.PO_PurchaseOrderDetail b ON a.PurchaseOrderMasterID = b.PurchaseOrderMasterID
WHERE     (b.ApproverID IS NOT NULL) AND (b.PurchaseOrderDetailID NOT IN
	(SELECT     PurchaseOrderDetailID
	FROM   PO_PurchaseOrderReceiptDetail))
'
GO
/****** Object:  View [dbo].[v_CloseOrOpenPO]    Script Date: 05/06/2010 12:53:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_CloseOrOpenPO]'))
EXEC dbo.sp_executesql @statement = N'CREATE  VIEW [v_CloseOrOpenPO]
AS
SELECT     '''' AS Sel, '''' AS Status, POM.Code AS PurchaseOrderNo, POD.PurchaseOrderDetailID, POD.Line, POD.Closed, P.Code, P.Description, P.Revision, 
                      UOM.Code AS UM, POD.OrderQuantity, PAR.Code AS VendorCode, PAR.Name AS VendorName, POM.MasterLocationID, POM.CCNID, 
                      POM.PurchaseOrderMasterID, DS.ScheduleDate
FROM         dbo.PO_PurchaseOrderDetail POD INNER JOIN
                      dbo.PO_PurchaseOrderMaster POM ON POM.PurchaseOrderMasterID = POD.PurchaseOrderMasterID LEFT JOIN
                      dbo.PO_DeliverySchedule DS ON DS.PurchaseOrderDetailID = POD.PurchaseOrderDetailID INNER JOIN
                      dbo.ITM_Product P ON P.ProductID = POD.ProductID INNER JOIN
                      dbo.MST_UnitOfMeasure UOM ON UOM.UnitOfMeasureID = POD.BuyingUMID INNER JOIN
                      dbo.MST_Party PAR ON PAR.PartyID = POM.PartyID
WHERE POD.ApproverID > 0 
'
GO
/****** Object:  View [dbo].[v_ApprovedAndNotCompletedPOLine]    Script Date: 05/06/2010 12:53:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_ApprovedAndNotCompletedPOLine]'))
EXEC dbo.sp_executesql @statement = N'CREATE VIEW [v_ApprovedAndNotCompletedPOLine] AS
SELECT     Master.Code, Detail.Line, dbo.MST_MasterLocation.Code AS MasterLocation, Detail.UnitPrice, Detail.ProductID, 
                      Detail.OrderQuantity - ISNULL(Detail.TotalDelivery, 0) AS AvailableQuantity, Master.PurchaseOrderMasterID, Master.MasterLocationID, 
                      Detail.PurchaseOrderDetailID
FROM         dbo.PO_PurchaseOrderDetail Detail INNER JOIN
                      dbo.PO_PurchaseOrderMaster Master ON Master.PurchaseOrderMasterID = Detail.PurchaseOrderMasterID LEFT OUTER JOIN
                      dbo.MST_MasterLocation ON Master.MasterLocationID = dbo.MST_MasterLocation.MasterLocationID
WHERE     (Detail.ApproverID > 0) AND (Detail.Closed = 0 OR
                      Detail.Closed IS NULL) AND (Master.RecCompleted = 0) AND (Detail.OrderQuantity - ISNULL(Detail.TotalDelivery, 0) > 0)
'
GO
/****** Object:  View [dbo].[v_ApprovedAndNotCompletedPO]    Script Date: 05/06/2010 12:53:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_ApprovedAndNotCompletedPO]'))
EXEC dbo.sp_executesql @statement = N'CREATE VIEW [v_ApprovedAndNotCompletedPO]
AS
SELECT            PO_PurchaseOrderMaster.Code, MST_MasterLocation.Code AS MasterLocation, 
            PurchaseOrderMasterID, MST_MasterLocation.MasterLocationID,
            ShipToLocID, InvToLocID, OrderDate, VAT, ImportTax, SpecialTax, 
            CurrencyID, DeliveryTermsID, PaymentTermsID, CarrierID, TotalImportTax, 
            BuyerID, TotalVAT, TotalSpecialTax, TotalAmount, TotalDiscount, 
            TotalNetAmount, PO_PurchaseOrderMaster.CCNID, PartyID, VendorLocID, PartyContactID, 
            PurchaseTypeID, DiscountTermID, PauseID, Priority, RecCompleted, 
            Comment, ExchangeRate, VendorSO, PORevision
FROM PO_PurchaseOrderMaster JOIN MST_MasterLocation
ON PO_PurchaseOrderMaster.MasterLocationID = MST_MasterLocation.MasterLocationID
WHERE PurchaseOrderMasterID IN 
            (SELECT Detail.PurchaseOrderMasterID
                        FROM PO_PurchaseOrderDetail AS Detail
                        WHERE Detail.ApproverID > 0 AND ISNULL(Detail.Closed,0) = 0
                                    AND (Detail.OrderQuantity - ISNULL(Detail.TotalDelivery,0)) > 0)
AND PO_PurchaseOrderMaster.RecCompleted = 0
'
GO
/****** Object:  View [dbo].[v_TotalCommitInventory]    Script Date: 05/06/2010 12:53:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_TotalCommitInventory]'))
EXEC dbo.sp_executesql @statement = N'CREATE View [v_TotalCommitInventory]
AS (
SELECT  SUM(A.CommitQuantity) AS CommitQuantity, A.ProductID, 
            B.SaleOrderMasterID, A.DeliveryScheduleID
FROM SO_CommitInventoryDetail A JOIN SO_CommitInventoryMaster B
ON A.CommitInventoryMasterID = B.CommitInventoryMasterID
GROUP BY B.SaleOrderMasterID, A.ProductID, A.DeliveryScheduleID
)
'
GO
/****** Object:  View [dbo].[v_SOCancelCommitment]    Script Date: 05/06/2010 12:53:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_SOCancelCommitment]'))
EXEC dbo.sp_executesql @statement = N'CREATE VIEW [v_SOCancelCommitment]
AS
SELECT  DISTINCT   dbo.SO_SaleOrderMaster.Code, dbo.SO_SaleOrderMaster.SaleOrderMasterID, 
dbo.SO_SaleOrderMaster.CCNID, dbo.SO_SaleOrderMaster.PartyID, dbo.SO_SaleOrderMaster.BuyingLocID
FROM         dbo.SO_CommitInventoryMaster INNER JOIN
                      dbo.SO_CommitInventoryDetail ON 
                      dbo.SO_CommitInventoryMaster.CommitInventoryMasterID = dbo.SO_CommitInventoryDetail.CommitInventoryMasterID INNER JOIN
                      dbo.SO_SaleOrderMaster ON dbo.SO_CommitInventoryMaster.SaleOrderMasterID = dbo.SO_SaleOrderMaster.SaleOrderMasterID
WHERE   (Select count (*)  From SO_SaleOrderMaster B inner join SO_CommitInventoryMaster C on B.SaleOrderMasterID = C.SaleOrderMasterID Inner Join SO_CommitInventoryDetail D on C.CommitInventoryMasterID = D.CommitInventoryMasterID Where SO_SaleOrderMaster.SaleOrderMasterID = B.SaleOrderMasterID and (D.Shipped = 0 or D.Shipped is null)) > 0
'
GO
/****** Object:  View [dbo].[v_ReceiptByVendor]    Script Date: 05/06/2010 12:53:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_ReceiptByVendor]'))
EXEC dbo.sp_executesql @statement = N'
CREATE VIEW [v_ReceiptByVendor]
AS
SELECT   DISTINCT  RM.PostDate, RM.ReceiveNo, RM.POSlipNo, RM.RefNo, RM.PurchaseOrderReceiptID,
                      PartyID = CASE RM.ReceiptType WHEN 3 THEN Invoice.PartyID WHEN 2 THEN PO.PartyID WHEN 4 THEN PO.PartyID  END, 
                      PurchaseOrderNO = CASE RM.ReceiptType WHEN 2 THEN PO.Code WHEN 4 THEN PO.Code END, 
                      VendorCode = CASE RM.ReceiptType WHEN 3 THEN Invoice.VendorCode WHEN 2 THEN PO.VendorCode WHEN 4 THEN PO.VendorCode END, 
                      VendorName = CASE RM.ReceiptType WHEN 3 THEN Invoice.VendorName WHEN 2 THEN PO.VendorName WHEN 4 THEN PO.VendorName END,
						POM.MakerID, PAM.Code MakerCode
FROM         PO_PurchaseOrderReceiptDetail RD INNER JOIN
                      PO_PurchaseOrderReceiptMaster RM ON RM.PurchaseOrderReceiptID = RD.PurchaseOrderReceiptID INNER JOIN
                      ITM_Product P ON P.ProductID = RD.ProductID INNER JOIN
                      MST_UnitOfMeasure UM ON UM.UnitOfMeasureID = RD.BuyingUMID LEFT JOIN
                          (SELECT     IVM.InvoiceNo, PA.Code VendorCode, PA.Name VendorName, IVM.PartyID, IVD.InvoiceMasterID, DeliveryScheduleID, CIFAmount
                            FROM          PO_InvoiceDetail IVD INNER JOIN
                                                   PO_InvoiceMaster IVM ON IVM.InvoiceMasterID = IVD.InvoiceMasterID INNER JOIN
                                                   MST_Party PA ON IVM.PartyID = PA.PartyID) Invoice ON RM.InvoiceMasterID = Invoice.InvoiceMasterID AND 
                      RD.DeliveryScheduleID = invoice.DeliveryScheduleID LEFT JOIN
                          (SELECT     PM.Code, PA.Code VendorCode, PA.Name VendorName, PM.PartyID, UnitPrice, DeliveryScheduleID, PD.PurchaseOrderMasterID
                            FROM          PO_PurchaseOrderDetail PD INNER JOIN
                                                   PO_PurchaseOrderMaster PM ON PD.PurchaseOrderMasterID = PM.PurchaseOrderMasterID INNER JOIN
                                                   PO_DeliverySchedule DS ON PD.PurchaseOrderDetailID = DS.PurchaseOrderDetailID INNER JOIN
                                                   MST_Party PA ON PM.PartyID = PA.PartyID) PO ON RM.PurchaseOrderMasterID = PO.PurchaseOrderMasterID AND 
                      RD.DeliveryScheduleID = PO.DeliveryScheduleID
						LEFT JOIN PO_PurchaseOrderMaster POM ON POM.PurchaseOrderMasterID = RD.PurchaseOrderMasterID
						LEFT JOIN MST_Party PAM ON PAM.PartyID = POM.MakerID

'
GO
/****** Object:  View [dbo].[v_LocalReceive]    Script Date: 05/06/2010 12:53:52 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_LocalReceive]'))
EXEC dbo.sp_executesql @statement = N'CREATE view [v_LocalReceive] as
select M.PurchaseOrderMasterID,M.ReceiveNo,M.PostDate,
D.ProductID,I.InventorID,
D.ReceiveQuantity,PD.UnitPrice,PM.ExchangeRate,PD.VAT,PM.PartyID,PM.MakerID
from PO_PurchaseOrderReceiptDetail D
INNER JOIN PO_PurchaseOrderDetail PD ON PD.PurchaseOrderDetailID=D.PurchaseOrderDetailID
INNER JOIN PO_PurchaseOrderReceiptMaster M ON M.PurchaseOrderReceiptID=D.PurchaseOrderReceiptID
INNER JOIN PO_PurchaseOrderMaster PM ON PM.PurchaseOrderMasterID=PD.PurchaseOrderMasterID
INNER JOIN ITM_Product I ON I.ProductID=D.ProductID
Where M.ReceiptType IN (2,4)
'
GO
/****** Object:  View [dbo].[V_InOutStockReport_GroupByBinType_1]    Script Date: 05/06/2010 12:53:52 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[V_InOutStockReport_GroupByBinType_1]'))
EXEC dbo.sp_executesql @statement = N'CREATE View [V_InOutStockReport_GroupByBinType_1] as
---Nhap SPHT
Select WO.PostDate,WO.BinID,WO.ProductID,WO.CompletedQuantity InputQty,
WO.CompletedQuantity *ISNULL(X.CompletionCost,0) InputAmount,0 OutputQty,0 OutputAmount,0 BeginQty,0 BeginCost
from PRO_WorkOrderCompletion WO 
LEFT JOIN V_SumWOCompletionCost X
ON X.productID= WO.ProductID AND Datepart(year,X.Todate)=Datepart(Year,WO.PostDate)
AND Datepart(Month,X.Todate)=Datepart(month,WO.PostDate)
UNION ALL
----Receipt By Invoice+ Slip
select M.PostDate,D.BinID,D.ProductID,D.ReceiveQuantity InputQty,(IND.BeforeVATAmount+IND.Inland)*INM.ExchangeRate InputAmount,
0 OutputQty, 0 OutputAmount,0 BeginQty,0 BeginCost
from PO_PurchaseOrderReceiptDetail D
INNER JOIN PO_PurchaseOrderReceiptMaster M ON D.PurchaseOrderReceiptID=M.PurchaseOrderReceiptID
INNER JOIN PO_InvoiceDetail IND ON IND.InvoiceDetailID=D.InvoiceDetailID
INNER JOIN PO_InvoiceMaster INM ON INM.InvoiceMasterID=IND.InvoiceMasterID
Where M.ReceiptType=3
Union All
select M.PostDate,D.BinID,D.ProductID,D.ReceiveQuantity InputQty,D.ReceiveQuantity*POD.UnitPrice InputAmount,
0 OutputQty, 0 OutputAmount,0 BeginQty,0 BeginCost
from PO_PurchaseOrderReceiptDetail D
INNER JOIN PO_PurchaseOrderReceiptMaster M ON D.PurchaseOrderReceiptID=M.PurchaseOrderReceiptID
INNER JOIN PO_PurchaseOrderDetail POD ON POD.PurchaseOrderDetailID=D.PurchaseOrderDetailID
WHERE M.ReceiptType in (2)
----
'
GO
/****** Object:  View [dbo].[v_TotalReturnedGoods]    Script Date: 05/06/2010 12:53:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_TotalReturnedGoods]'))
EXEC dbo.sp_executesql @statement = N'create view [v_TotalReturnedGoods] 
as 
(
select sum(a.ReceiveQuantity) ReceiveQuantity, 
      a.ProductID, 
      isnull(a.Lot,'''') Lot , 
      isnull(a.Serial,'''') Serial, 
      b.SaleOrderMasterID
From SO_ReturnedGoodsDetail a inner join SO_ReturnedGoodsMaster b
       on a.ReturnedGoodsMasterID = b.ReturnedGoodsMasterID 
       group by b.SaleOrderMasterID, 
         a.ProductID, 
         isnull(a.Lot,''''), 
         isnull(a.Serial,'''')
)
'
GO
/****** Object:  View [dbo].[v_CGS1]    Script Date: 05/06/2010 12:53:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_CGS1]'))
EXEC dbo.sp_executesql @statement = N'Create view [v_CGS1] AS
Select Sum(D.InvoiceQty*C.ActualCost)- ISNULL((Select Sum(D.ReceiveQuantity*C1.ActualCost)
as TotalCGS1
from SO_ReturnedGoodsDetail D
INNER JOIN SO_ReturnedGoodsMaster  M ON M.ReturnedGoodsMasterID=D.ReturnedGoodsMasterID
INNER JOIN v_UnitOfActualCost_ByCostElement AS C1 ON C1.ProductID=D.ProductID
WHERE M.PostDate>=C1.FromDate and M.PostDate<=C1.ToDate+1
AND C1.CostElementID = C.CostElementID
AND C1.ActCostAllocationMasterID = C.ActCostAllocationMasterID
),0)
as TotalCGS1,C.CostElementID,C.ActCostAllocationMasterID
from SO_ConfirmShipDetail D
INNER JOIN SO_ConfirmShipMaster  M ON M.ConfirmShipMasterID=D.ConfirmShipMasterID
INNER JOIN v_UnitOfActualCost_ByCostElement AS C ON C.ProductID=D.ProductID
WHERE M.ShippedDate>=C.FromDate and M.ShippedDate<=C.ToDate+1
group by C.ActCostAllocationMasterID,C.CostElementID
'
GO
/****** Object:  View [dbo].[v_OHDSRecycleAdjRpt]    Script Date: 05/06/2010 12:53:52 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_OHDSRecycleAdjRpt]'))
EXEC dbo.sp_executesql @statement = N'Create view [v_OHDSRecycleAdjRpt] as
Select D.ProductID,I.Code as PartNo,I.Description as PartName,I.Revision as Model,I.CategoryID,CAT.Code as Category,
Sum(D.InvoiceQty*C.ActualCost)
- ISNULL((Select Sum(D1.ReceiveQuantity*C1.ActualCost)
from SO_ReturnedGoodsDetail D1 
INNER JOIN SO_ReturnedGoodsMaster  M ON M.ReturnedGoodsMasterID=D1.ReturnedGoodsMasterID
INNER JOIN v_UnitOfActualCost_ByCostElement AS C1 ON C1.ProductID=D1.ProductID
WHERE M.PostDate>=C1.FromDate and M.PostDate<=C1.ToDate+1
AND C1.CostElementID = C.CostElementID
AND C1.ActCostAllocationMasterID = C.ActCostAllocationMasterID
AND D1.ProductID=D.ProductID
Group by D.ProductID),0)
AS CGS1,
CGS.TotalCGS1,100000*(Sum(D.InvoiceQty*C.ActualCost)
- ISNULL((Select Sum(D1.ReceiveQuantity*C1.ActualCost)
from SO_ReturnedGoodsDetail D1 
INNER JOIN SO_ReturnedGoodsMaster  M ON M.ReturnedGoodsMasterID=D1.ReturnedGoodsMasterID
INNER JOIN v_UnitOfActualCost_ByCostElement AS C1 ON C1.ProductID=D1.ProductID
WHERE M.PostDate>=C1.FromDate and M.PostDate<=C1.ToDate+1
AND C1.CostElementID = C.CostElementID
AND C1.ActCostAllocationMasterID = C.ActCostAllocationMasterID
AND D1.ProductID=D.ProductID
Group by D.ProductID),0))/CGS.TotalCGS1 AS Rate,
DS.DSAmount*(Sum(D.InvoiceQty*C.ActualCost)
- ISNULL((Select Sum(D1.ReceiveQuantity*C1.ActualCost)
from SO_ReturnedGoodsDetail D1 
INNER JOIN SO_ReturnedGoodsMaster  M ON M.ReturnedGoodsMasterID=D1.ReturnedGoodsMasterID
INNER JOIN v_UnitOfActualCost_ByCostElement AS C1 ON C1.ProductID=D1.ProductID
WHERE M.PostDate>=C1.FromDate and M.PostDate<=C1.ToDate+1
AND C1.CostElementID = C.CostElementID
AND C1.ActCostAllocationMasterID = C.ActCostAllocationMasterID
AND D1.ProductID=D.ProductID
Group by D.ProductID),0))
/CGS.TotalCGS1*100000/100000 AS DSAmount,
DS.RecycleAmount*(Sum(D.InvoiceQty*C.ActualCost)
- ISNULL((Select Sum(D1.ReceiveQuantity*C1.ActualCost)
from SO_ReturnedGoodsDetail D1 
INNER JOIN SO_ReturnedGoodsMaster  M ON M.ReturnedGoodsMasterID=D1.ReturnedGoodsMasterID
INNER JOIN v_UnitOfActualCost_ByCostElement AS C1 ON C1.ProductID=D1.ProductID
WHERE M.PostDate>=C1.FromDate and M.PostDate<=C1.ToDate+1
AND C1.CostElementID = C.CostElementID
AND C1.ActCostAllocationMasterID = C.ActCostAllocationMasterID
AND D1.ProductID=D.ProductID
Group by D.ProductID),0))
/CGS.TotalCGS1*100000/100000 AS RecycleAmount,
DS.AdjustAmount*(Sum(D.InvoiceQty*C.ActualCost)
- ISNULL((Select Sum(D1.ReceiveQuantity*C1.ActualCost)
from SO_ReturnedGoodsDetail D1 
INNER JOIN SO_ReturnedGoodsMaster  M ON M.ReturnedGoodsMasterID=D1.ReturnedGoodsMasterID
INNER JOIN v_UnitOfActualCost_ByCostElement AS C1 ON C1.ProductID=D1.ProductID
WHERE M.PostDate>=C1.FromDate and M.PostDate<=C1.ToDate+1
AND C1.CostElementID = C.CostElementID
AND C1.ActCostAllocationMasterID = C.ActCostAllocationMasterID
AND D1.ProductID=D.ProductID
Group by D.ProductID),0))
/CGS.TotalCGS1*100000/100000 AS AdjustAmount,
C.CostElementID,
C.ActCostAllocationMasterID
from SO_ConfirmShipDetail D
INNER JOIN ITM_Product I ON I.ProductID=D.ProductID
LEFT JOIN ITM_Category CAT ON CAT.CategoryID=I.CategoryID
INNER JOIN SO_ConfirmShipMaster  M ON M.ConfirmShipMasterID=D.ConfirmShipMasterID
INNER JOIN v_UnitOfActualCost_ByCostElement AS C ON C.ProductID=D.ProductID
INNER JOIN v_CGS1 CGS ON CGS.ActCostAllocationMasterID=C.ActCostAllocationMasterID AND CGS.CostElementID=C.CostElementID 
INNER JOIN v_Total_DS_Before_Allocation DS ON DS.ActCostAllocationMasterID=C.ActCostAllocationMasterID AND DS.CostElementID=C.CostElementID
INNER JOIN STD_CostElement CE ON CE.CostElementID=C.CostElementID
WHERE M.ShippedDate>=C.FromDate and M.ShippedDate<=C.ToDate+1
AND CGS.TotalCGS1<>0
group by d.ProductID,C.CostElementID, C.ActCostAllocationMasterID,CGS.TotalCGS1,DS.DSAmount,
DS.RecycleAmount,DS.AdjustAmount,I.ProductID,I.Code,I.Revision,I.Description,I.CategoryID,CAT.Code
'
GO
/****** Object:  View [dbo].[v_WorkCenterByActualCost]    Script Date: 05/06/2010 12:53:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_WorkCenterByActualCost]'))
EXEC dbo.sp_executesql @statement = N'Create view [v_WorkCenterByActualCost] as 
SELECT ACDSOptionMasterID, WC.WorkCenterID, Code, Name, CCNID FROM MST_WorkCenter WC, MTR_ACDSOptionDetail AC
WHERE 
WC.WorkCenterID = AC.WorkCenterID
'
GO
/****** Object:  View [dbo].[v_SO_SaleOrderMasterHasCommit]    Script Date: 05/06/2010 12:53:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_SO_SaleOrderMasterHasCommit]'))
EXEC dbo.sp_executesql @statement = N'CREATE view [v_SO_SaleOrderMasterHasCommit]
as 
(
SELECT     SaleOrderMasterID, Code, TransDate, CustomerPurchaseOrderNo, VAT, VATRate, ExportTax, SpecialTax, ExportTaxRate, ShipCompleted, 
                      SalesRepresentativeID, SpecialTaxRate, CCNID, CurrencyID, CarrierID, PaymentTermsID, DeliveryTermsID, DiscountTermsID, PauseID, 
                      TotalVATAmount, TotalExportAmount, TotalSpecialTaxAmount, TotalAmount, TotalDiscountAmount, TotalNetAmount, PaymentMethodID, Priority, 
                      BuyingLocID, ShipToLocID, BillToLocID, PartyContactID, SaleStatusID, CancelReasonID,  
                      SaleTypeID, PartyID, ExchangeRate, LocationID, ShipFromLocID
FROM         dbo.SO_SaleOrderMaster
WHERE     (SaleOrderMasterID IN
                          (SELECT     SaleOrderMasterID
                            FROM          SO_CommitInventoryMaster))
)
'
GO
/****** Object:  View [dbo].[v_ShippedSaleOrder]    Script Date: 05/06/2010 12:53:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_ShippedSaleOrder]'))
EXEC dbo.sp_executesql @statement = N'CREATE VIEW [v_ShippedSaleOrder]
AS
 SELECT DISTINCT SO_SaleOrderMaster.CCNID, SO_SaleOrderMaster.SaleOrderMasterID, SO_SaleOrderMaster.Code AS SONo, SO_ConfirmShipMaster.ShippedDate
 FROM SO_ConfirmShipMaster JOIN SO_SaleOrderMaster
 ON SO_ConfirmShipMaster.SaleOrderMasterID = SO_SaleOrderMaster.SaleOrderMasterID
'
GO
/****** Object:  View [dbo].[v_SOInvoiceMaster]    Script Date: 05/06/2010 12:53:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_SOInvoiceMaster]'))
EXEC dbo.sp_executesql @statement = N'CREATE  VIEW [v_SOInvoiceMaster]
AS
SELECT     SO_InvoiceMaster.InvoiceMasterID, SO_InvoiceMaster.ConfirmShipNo, SO_InvoiceMaster.ShippedDate, 
                      SO_InvoiceMaster.ExchangeRate, CUR.Code, SO_InvoiceMaster.CurrencyID, SO_InvoiceMaster.SaleOrderMasterID, 
                      SO_InvoiceMaster.MasterLocationID, SO_InvoiceMaster.CCNID, SO_SaleOrderMaster.Code AS SaleOrder, 
                      SO_SaleOrderMaster.SaleTypeID, SO_SaleOrderMaster.PartyID, MST_MasterLocation.Code AS MasLoc, 
                      SO_SaleType.Code AS SaleType, MST_Party.Name AS CustomerName, MST_Party.Code AS CustomerCode, TY.Description AS SO_TypeCode, 
                      TY.TypeID, GA.Code AS SO_GateCode, SO_InvoiceMaster.GateID, SO_InvoiceMaster.Comment, 
                      SO_InvoiceMaster.FromPort, SO_InvoiceMaster.CNo, SO_InvoiceMaster.Measurement, 
                      SO_InvoiceMaster.GrossWeight, SO_InvoiceMaster.NetWeight, SO_InvoiceMaster.IssuingBank, 
                      SO_InvoiceMaster.LCDate, SO_InvoiceMaster.LCNo, SO_InvoiceMaster.VesselName, 
                      SO_InvoiceMaster.ShipCode, SO_InvoiceMaster.OnBoardDate, SO_InvoiceMaster.ReferenceNo, 
                      SO_InvoiceMaster.InvoiceNo, SO_InvoiceMaster.InvoiceDate, PT.Code MST_PaymentTermCode

FROM         SO_InvoiceMaster INNER JOIN
                      SO_SaleOrderMaster INNER JOIN
                      MST_MasterLocation ON SO_SaleOrderMaster.ShipFromLocID = MST_MasterLocation.MasterLocationID INNER JOIN
                      MST_Party ON SO_SaleOrderMaster.PartyID = MST_Party.PartyID ON 
                      SO_InvoiceMaster.SaleOrderMasterID = SO_SaleOrderMaster.SaleOrderMasterID LEFT OUTER JOIN
                      SO_SaleType ON SO_SaleOrderMaster.SaleTypeID = SO_SaleType.SaleTypeID INNER JOIN
                      MST_Currency CUR ON CUR.CurrencyID = SO_InvoiceMaster.CurrencyID LEFT OUTER JOIN
                      SO_Type TY ON TY.TypeID = SO_SaleOrderMaster.TypeID LEFT OUTER JOIN
                      SO_Gate GA ON GA.GateID = SO_InvoiceMaster.GateID
						LEFT JOIN MST_PaymentTerm PT ON PT.PaymentTermID = MST_Party.PaymentTermID
'
GO
/****** Object:  View [dbo].[v_SOConfirmShipMaster]    Script Date: 05/06/2010 12:53:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_SOConfirmShipMaster]'))
EXEC dbo.sp_executesql @statement = N'CREATE VIEW [v_SOConfirmShipMaster]
AS
SELECT     dbo.SO_ConfirmShipMaster.ConfirmShipMasterID, dbo.SO_ConfirmShipMaster.ConfirmShipNo, dbo.SO_ConfirmShipMaster.ShippedDate, 
                      dbo.SO_ConfirmShipMaster.ExchangeRate, CUR.Code, dbo.SO_ConfirmShipMaster.CurrencyID, dbo.SO_ConfirmShipMaster.SaleOrderMasterID, 
                      dbo.SO_ConfirmShipMaster.MasterLocationID, dbo.SO_ConfirmShipMaster.CCNID, dbo.SO_SaleOrderMaster.Code AS SaleOrder, 
                      dbo.SO_SaleOrderMaster.SaleTypeID, dbo.SO_SaleOrderMaster.PartyID, dbo.MST_MasterLocation.Code AS MasLoc, 
                      dbo.SO_SaleType.Code AS SaleType, dbo.MST_Party.Name AS CustomerName, dbo.MST_Party.Code AS CustomerCode, TY.Description AS SO_TypeCode, 
                      TY.TypeID, GA.Code AS SO_GateCode, dbo.SO_ConfirmShipMaster.GateID, dbo.SO_ConfirmShipMaster.Comment, 
                      dbo.SO_ConfirmShipMaster.FromPort, dbo.SO_ConfirmShipMaster.CNo, dbo.SO_ConfirmShipMaster.Measurement, 
                      dbo.SO_ConfirmShipMaster.GrossWeight, dbo.SO_ConfirmShipMaster.NetWeight, dbo.SO_ConfirmShipMaster.IssuingBank, 
                      dbo.SO_ConfirmShipMaster.LCDate, dbo.SO_ConfirmShipMaster.LCNo, dbo.SO_ConfirmShipMaster.VesselName, 
                      dbo.SO_ConfirmShipMaster.ShipCode, dbo.SO_ConfirmShipMaster.OnBoardDate, dbo.SO_ConfirmShipMaster.ReferenceNo, 
                      dbo.SO_ConfirmShipMaster.InvoiceNo, dbo.SO_ConfirmShipMaster.InvoiceDate, PT.Code MST_PaymentTermCode

FROM         dbo.SO_ConfirmShipMaster INNER JOIN
                      dbo.SO_SaleOrderMaster INNER JOIN
                      dbo.MST_MasterLocation ON dbo.SO_SaleOrderMaster.ShipFromLocID = dbo.MST_MasterLocation.MasterLocationID INNER JOIN
                      dbo.MST_Party ON dbo.SO_SaleOrderMaster.PartyID = dbo.MST_Party.PartyID ON 
                      dbo.SO_ConfirmShipMaster.SaleOrderMasterID = dbo.SO_SaleOrderMaster.SaleOrderMasterID LEFT OUTER JOIN
                      dbo.SO_SaleType ON dbo.SO_SaleOrderMaster.SaleTypeID = dbo.SO_SaleType.SaleTypeID INNER JOIN
                      dbo.MST_Currency CUR ON CUR.CurrencyID = dbo.SO_ConfirmShipMaster.CurrencyID LEFT OUTER JOIN
                      dbo.SO_Type TY ON TY.TypeID = dbo.SO_SaleOrderMaster.TypeID LEFT OUTER JOIN
                      dbo.SO_Gate GA ON GA.GateID = dbo.SO_ConfirmShipMaster.GateID
						LEFT JOIN dbo.MST_PaymentTerm PT ON PT.PaymentTermID = dbo.MST_Party.PaymentTermID

'
GO
/****** Object:  View [dbo].[v_PO_ReturnToVendorMaster]    Script Date: 05/06/2010 12:53:52 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_PO_ReturnToVendorMaster]'))
EXEC dbo.sp_executesql @statement = N'
CREATE VIEW [v_PO_ReturnToVendorMaster]
AS 
SELECT     dbo.PO_ReturnToVendorMaster.PartyID, dbo.PO_ReturnToVendorMaster.PurchaseLocID, dbo.PO_ReturnToVendorMaster.PurchaseOrderMasterID, 
                      dbo.PO_ReturnToVendorMaster.MasterLocationID, dbo.PO_ReturnToVendorMaster.ReturnToVendorMasterID, dbo.PO_ReturnToVendorMaster.PostDate, 
                      dbo.PO_ReturnToVendorMaster.RTVNo, dbo.PO_ReturnToVendorMaster.CCNID, dbo.MST_Party.Code AS PartyCode, 
                      dbo.MST_Party.Name AS PartyName, PurchaseLoc.Code AS PurchaseLocCode, dbo.PO_PurchaseOrderMaster.Code AS PurchaseOrderCode, 
                      dbo.MST_MasterLocation.Code AS MasterLocCode, PO_ReturnToVendorMaster.InvoiceMasterID, PO_InvoiceMaster.InvoiceNo,
                                    ISNULL(PO_ReturnToVendorMaster.ByPO,0) ByPO, ISNULL(PO_ReturnToVendorMaster.ByInvoice,0) ByInvoice,
PO_ReturnToVendorMaster.ProductionLineID, Line.Code AS ProductionLine
FROM         dbo.PO_ReturnToVendorMaster INNER JOIN
                      dbo.MST_Party ON dbo.PO_ReturnToVendorMaster.PartyID = dbo.MST_Party.PartyID AND 
                      dbo.PO_ReturnToVendorMaster.PartyID = dbo.MST_Party.PartyID AND dbo.PO_ReturnToVendorMaster.PartyID = dbo.MST_Party.PartyID AND 
                      dbo.PO_ReturnToVendorMaster.PartyID = dbo.MST_Party.PartyID LEFT OUTER JOIN
                      dbo.MST_MasterLocation ON dbo.PO_ReturnToVendorMaster.MasterLocationID = dbo.MST_MasterLocation.MasterLocationID LEFT OUTER JOIN
                      dbo.MST_PartyLocation PurchaseLoc ON dbo.PO_ReturnToVendorMaster.PurchaseLocID = PurchaseLoc.PartyLocationID LEFT OUTER JOIN
                      dbo.PO_PurchaseOrderMaster ON dbo.PO_ReturnToVendorMaster.PurchaseOrderMasterID = dbo.PO_PurchaseOrderMaster.PurchaseOrderMasterID 
                                    Left Join PO_InvoiceMaster ON PO_ReturnToVendorMaster.InvoiceMasterID = PO_InvoiceMaster.InvoiceMasterID
LEFT JOIN PRO_ProductionLine Line ON PO_ReturnToVendorMaster.ProductionLineID = Line.ProductionLineID

'
GO
/****** Object:  View [dbo].[v_ConfirmShipByCustomer]    Script Date: 05/06/2010 12:53:52 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_ConfirmShipByCustomer]'))
EXEC dbo.sp_executesql @statement = N'
CREATE  VIEW [v_ConfirmShipByCustomer]
AS
SELECT COM.ConfirmShipNo, COM.ConfirmShipMasterID, SOM.PartyID, PA.Code
FROM SO_ConfirmShipMaster COM
INNER JOIN SO_SaleOrderMaster SOM 
ON SOM.SaleOrderMasterID = COM.SaleOrderMasterID
INNER JOIN mst_party PA ON PA.PartyID = SOM.PartyID
'
GO
/****** Object:  View [dbo].[v_POReceiptImport]    Script Date: 05/06/2010 12:53:52 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[v_POReceiptImport]'))
EXEC dbo.sp_executesql @statement = N'Create view [v_POReceiptImport] as
select DatePart(Year,M.PostDate)as Year,DatePart(Month,M.PostDate)as Month,D.InvoiceDetailID,
D.ProductID,I.InventorID,PM.PartyID,PM.MakerID,I.Code as PartNo,I.Revision as Model,I.Description as PartName,
PType.Code as Type,CAT.Code as Category,Vendor.Code as VendorCode,Vendor.Name VendorName,UM.Code as UM,
Maker.Code MakerCode,Maker.Name MakerName,
ISNULL(Sum(ISNULL(D.ReceiveQuantity,0)),0) 
-ISNULL((Select ISNULL(Sum(ISNULL(Quantity,0)),0) FROM PO_ReturnToVendorDetail RT
INNER JOIN PO_ReturnToVendorMaster RM ON RM.ReturnToVendorMasterID=RT.ReturnToVendorMasterID
WHERE RT.ProductID=D.ProductID
AND RM.InvoiceMasterID=M.InvoiceMasterID
AND DatePart(Year,RM.PostDate)=DatePart(Year,M.PostDate)
AND DatePart(Month,M.PostDate)=DatePart(Month,RM.PostDate)
Group by DatePart(Year,RM.PostDate),DatePart(Month,RM.PostDate),RT.ProductID
),0)
AS Quantity,
ISNULL((Select ID.UnitPrice*IM.ExchangeRate from PO_InvoiceDetail ID 
INNER JOIN PO_InvoiceMaster IM ON IM.InvoiceMasterID=ID.InvoiceMasterID
where InvoiceDetailID=D.InvoiceDetailID),0) AS CIFUnit,
ISNULL((Select ID.CIFAmount*IM.ExchangeRate From PO_InvoiceDetail ID
INNER JOIN PO_InvoiceMaster IM ON IM.InvoiceMasterID=ID.InvoiceMasterID
WHERE ID.InvoiceDetailID=D.InvoiceDetailID),0)as TotalCIF,
ISNULL((Select ID.InLand*IM.ExchangeRate From PO_InvoiceDetail ID
INNER JOIN PO_InvoiceMaster IM ON IM.InvoiceMasterID=ID.InvoiceMasterID
WHERE ID.InvoiceDetailID=D.InvoiceDetailID),0)as TotalInland,
ISNULL((Select ID.CIPAmount*IM.ExchangeRate From PO_InvoiceDetail ID
INNER JOIN PO_InvoiceMaster IM ON IM.InvoiceMasterID=ID.InvoiceMasterID
WHERE ID.InvoiceDetailID=D.InvoiceDetailID),0)as CIPAmount,
ISNULL((Select ID.ImportTaxAmount*IM.ExchangeRate From PO_InvoiceDetail ID
INNER JOIN PO_InvoiceMaster IM ON IM.InvoiceMasterID=ID.InvoiceMasterID
WHERE ID.InvoiceDetailID=D.InvoiceDetailID),0)as ImportTaxAmount,
ISNULL((Select Sum(ISNULL(Amount,0))FROM cst_FreightDetail AS FD
INNER JOIN cst_FreightMaster FM ON FD.FreightMasterID=FM.FreightMasterID
WHERE FD.ProductID=D.ProductID AND FD.InvoiceMasterID=M.InvoiceMasterID
Group by M.InvoiceMasterID,FD.ProductID
),0) AS OrtherAmount
From PO_PurchaseOrderReceiptDetail as D
INNER JOIN PO_PurchaseOrderDetail PD ON PD.PurchaseOrderDetailID=D.PurchaseOrderDetailID
INNER JOIN PO_PurchaseOrderReceiptMaster M ON M.PurchaseOrderReceiptID=D.PurchaseOrderReceiptID
INNER JOIN PO_PurchaseOrderMaster PM ON PM.PurchaseOrderMasterID=PD.PurchaseOrderMasterID
INNER JOIN ITM_Product I ON I.ProductID=D.ProductID
LEFT JOIN ITM_Category CAT ON CAT.CategoryID=I.CategoryID
INNER JOIN MST_Party VENDOR ON VENDOR.PartyID=PM.PartyID
INNER JOIN MST_Party Maker ON Maker.PartyID=PM.MakerID
LEFT JOIN ITM_ProductType PType on PType.ProductTypeID=I.ProductTypeID
INNER JOIN MST_UnitOfMeasure UM ON UM.UnitOfMeasureID=I.StockUMID
WHERE M.InvoiceMasterID IS NOT NULL and InvoiceDetailID IS NOT NULL
Group by DatePart(Year,M.PostDate),DatePart(Month,M.PostDate),M.InvoiceMasterID,D.InvoiceDetailID,
D.ProductID,I.InventorID,PM.PartyID,PM.MakerID,I.Code,I.Revision,I.Description,CAT.Code,Vendor.Code,
Maker.Code,Vendor.Name,Maker.Name,PType.Code,UM.Code
'
GO
/****** Object:  View [dbo].[V_InOutStockReport_GroupByBinType]    Script Date: 05/06/2010 12:53:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[V_InOutStockReport_GroupByBinType]'))
EXEC dbo.sp_executesql @statement = N'CREATE View [V_InOutStockReport_GroupByBinType] as
--V_SumWOCompletionCost
--V_SumActCost
--V_InOutStockReport_GroupByBinType_1
Select * from V_InOutStockReport_GroupByBinType_1
UNION ALL
---- Receipt By Outside
select m.PostDate,D.BinID,D.ProductID,D.ReceiveQuantity InputQty,
D.ReceiveQuantity*X.CompletionCost InputAmount,0  OutputQty,0 OutputAmount,0 BeginQty,0 BeginCost
from PO_PurchaseOrderReceiptDetail D
INNER JOIN PO_PurchaseOrderReceiptMaster M ON D.PurchaseOrderReceiptID=M.PurchaseOrderReceiptID
LEFT JOIN v_SumWOCompletionCost X
ON X.productID= d.ProductID AND Datepart(year,X.Todate)=Datepart(Year,M.PostDate)
AND Datepart(Month,X.Todate)=Datepart(month,M.PostDate)
WHERE M.ReceiptType in (4)
UNION ALL
--- Thu Hoi+ Dieu chinh tang+ Nhap hang ban bi tra lai
Select PostDate,BinID,x.ProductID,x.Qty InputQty,
x.Qty*Isnull(y.cost,0) InputAmount,0  OutputQty,0 OutputAmount,0 BeginQty,0 BeginCost
from (
select M.PostDate,D.ToBinID BinID ,D.ProductID,D.RecoverQuantity Qty
from CST_RecoverMaterialDetail D
INNER JOIN CST_RecoverMaterialMaster M ON M.RecoverMaterialMasterID=D.RecoverMaterialMasterID
Union all
select PostDate, BinID ,ProductID,AdjustQuantity Qty from IV_Adjustment
where AdjustQuantity>0
Union all
select M.PostDate,D.BinID ,D.ProductID,D.ReceiveQuantity Qty
from SO_ReturnedGoodsDetail d
INNER JOIN SO_ReturnedGoodsMaster M ON M.ReturnedGoodsMasterID=D.ReturnedGoodsMasterID
Union All
---Nhap do xuat chuyen kho MiscIssue
Select M.PostDate,M.DesBinID BinID, D.ProductID,
isnull(D.Quantity,0)Qty
from IV_MiscellaneousIssueDetail D
INNER JOIN IV_MiscellaneousIssueMaster M ON D.MiscellaneousIssueMasterID=M.MiscellaneousIssueMasterID
Union All
---Nhap do xuat chuyen kho ProIssue
Select M.PostDate,M.ToBinID BinID, D.ProductID,
isnull(D.CommitQuantity,0)Qty
from PRO_IssueMaterialDetail D
INNER JOIN PRO_IssueMaterialMaster M ON D.IssueMaterialMasterID=M.IssueMaterialMasterID
) x
Left join V_SumActCost y 
ON Y.ProductID=X.ProductID AND DatePart(Year,X.PostDate)=DatePart(Year,Y.Todate)
AND DatePart(Month,X.PostDate)=DatePart(Month,Y.Todate)


------ OUTPUT
UNION ALL
----- Rut vat lieu do WOCompletion va POReceipt+ Xuat chuyen kho
Select Y.PostDate,Y.BinID,Y.ProductID,0 InputQty,0 InputAmount,Qty OutQty,Qty*isnull(X.Cost,0) OutputAmount, 
0 BeginQty,0 BeginCost
from 
(
Select M.PostDate,M.SourceBinID BinID, D.ProductID,
isnull(D.Quantity,0)Qty
from IV_MiscellaneousIssueDetail D
INNER JOIN IV_MiscellaneousIssueMaster M ON D.MiscellaneousIssueMasterID=M.MiscellaneousIssueMasterID
Union All
---Nhap do xuat chuyen kho ProIssue
Select M.PostDate,D.BinID BinID, D.ProductID,
isnull(D.CommitQuantity,0) OutQty
from PRO_IssueMaterialDetail D
INNER JOIN PRO_IssueMaterialMaster M ON D.IssueMaterialMasterID=M.IssueMaterialMasterID
-- Rut vat lieu WO va POReceipt
Union all
Select M.PostDate,BinID,M.ProductID,-Quantity Qty
from MST_TransactionHistory M
Where TranTypeID IN (19,11)and Quantity<0

--- Dieu chinh giam
Union all
Select PostDate, BinID, ProductID,-AdjustQuantity Qty
from IV_Adjustment
where AdjustQuantity<0
) Y
LEFT JOIN V_SumActCost X
ON X.productID= Y.ProductID AND Datepart(year,X.Todate)=Datepart(year,Y.PostDate)
AND Datepart(Month,X.Todate)=Datepart(month,Y.PostDate)

UNION ALL
--- Tra lai Nha cung cap
select M.PostDate,D.BinID,D.ProductID,0 InputQty,0 InputAmount,
D.Quantity OutQty,ISNULL(FD.Amount,0)* ISNULL(FM.ExchangeRate,0) OutputAmount,0 BeginQty,0 BeginCost
from PO_ReturnToVendorDetail D
INNER JOIN PO_ReturnToVendorMaster M on M.ReturnToVendorMasterID=D.ReturnToVendorMasterID
LEFT JOIN cst_FreightDetail FD on FD.ReturnToVendorDetailID=D.ReturnToVendorDetailID
LEFT JOIN cst_FreightMaster FM on FM.FreightMasterID=FD.FreightMasterID

---- BEGIN 
UNION ALL
Select DateAdd(Month,+1,BB.EffectDate) PostDate,BinID,BB.ProductID,
0 InputQty,0 InputAmount,0 OutputQty,0 OutputAmount
,OHQuantity BeginQty,VSAC.Cost BeginCost
from IV_BalanceBin bb

LEFT JOIN V_SumActCost VSAC ON VSAC.ProductID=BB.ProductID
AND Datepart(Year,VSAC.Todate)=Datepart(Year,BB.EffectDate)
AND Datepart(month,VSAC.Todate)=Datepart(month,BB.EffectDate)
'
GO
