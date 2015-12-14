/****** Object:  Add PO Number to SO_InvoiceMaster and SO_ConfirmShipMaster  Script Date: 12/14/2015 3:27:52 PM ******/
ALTER TABLE SO_InvoiceMaster ADD PONumber VARCHAR(500) NULL
GO
ALTER TABLE SO_ConfirmShipMaster ADD PONumber VARCHAR(500) NULL
GO

/****** Object:  View [dbo].[v_SOInvoiceMaster]    Script Date: 12/14/2015 3:27:52 PM ******/
ALTER  VIEW [v_SOInvoiceMaster]
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
                      SO_InvoiceMaster.InvoiceNo, SO_InvoiceMaster.InvoiceDate, PT.Code MST_PaymentTermCode, SO_InvoiceMaster.PONumber

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

GO

/****** Object:  View [dbo].[v_SOConfirmShipMaster]    Script Date: 12/14/2015 3:26:28 PM ******/
ALTER VIEW [v_SOConfirmShipMaster]
AS
SELECT     SO_ConfirmShipMaster.ConfirmShipMasterID, SO_ConfirmShipMaster.ConfirmShipNo, SO_ConfirmShipMaster.ShippedDate, 
                      SO_ConfirmShipMaster.ExchangeRate, CUR.Code, SO_ConfirmShipMaster.CurrencyID, SO_ConfirmShipMaster.SaleOrderMasterID, 
                      SO_ConfirmShipMaster.MasterLocationID, SO_ConfirmShipMaster.CCNID, SO_SaleOrderMaster.Code AS SaleOrder, 
                      SO_SaleOrderMaster.SaleTypeID, SO_SaleOrderMaster.PartyID, MST_MasterLocation.Code AS MasLoc, 
                      SO_SaleType.Code AS SaleType, MST_Party.Name AS CustomerName, MST_Party.Code AS CustomerCode, TY.Description AS SO_TypeCode, 
                      TY.TypeID, GA.Code AS SO_GateCode, SO_ConfirmShipMaster.GateID, SO_ConfirmShipMaster.Comment, 
                      SO_ConfirmShipMaster.FromPort, SO_ConfirmShipMaster.CNo, SO_ConfirmShipMaster.Measurement, 
                      SO_ConfirmShipMaster.GrossWeight, SO_ConfirmShipMaster.NetWeight, SO_ConfirmShipMaster.IssuingBank, 
                      SO_ConfirmShipMaster.LCDate, SO_ConfirmShipMaster.LCNo, SO_ConfirmShipMaster.VesselName, 
                      SO_ConfirmShipMaster.ShipCode, SO_ConfirmShipMaster.OnBoardDate, SO_ConfirmShipMaster.ReferenceNo, 
                      SO_ConfirmShipMaster.InvoiceNo, SO_ConfirmShipMaster.InvoiceDate, PT.Code MST_PaymentTermCode, SO_ConfirmShipMaster.PONumber

FROM         SO_ConfirmShipMaster INNER JOIN
                      SO_SaleOrderMaster INNER JOIN
                      MST_MasterLocation ON SO_SaleOrderMaster.ShipFromLocID = MST_MasterLocation.MasterLocationID INNER JOIN
                      MST_Party ON SO_SaleOrderMaster.PartyID = MST_Party.PartyID ON 
                      SO_ConfirmShipMaster.SaleOrderMasterID = SO_SaleOrderMaster.SaleOrderMasterID LEFT OUTER JOIN
                      SO_SaleType ON SO_SaleOrderMaster.SaleTypeID = SO_SaleType.SaleTypeID INNER JOIN
                      MST_Currency CUR ON CUR.CurrencyID = SO_ConfirmShipMaster.CurrencyID LEFT OUTER JOIN
                      SO_Type TY ON TY.TypeID = SO_SaleOrderMaster.TypeID LEFT OUTER JOIN
                      SO_Gate GA ON GA.GateID = SO_ConfirmShipMaster.GateID
						LEFT JOIN MST_PaymentTerm PT ON PT.PaymentTermID = MST_Party.PaymentTermID


GO

/* Shipping Management Transaction History Report update
SELECT DISTINCT 0 AS No, SO_ConfirmShipMaster.ShippedDate, SO_ConfirmShipMaster.OnBoardDate, SO_ConfirmShipMaster.ConfirmShipNo,
SO_ConfirmShipDetail.ProductID, ITM_Product.Code AS PartNo, ITM_Product.Description AS PartName, ITM_Product.Revision AS Model,
MST_UnitOfMeasure.Code AS SellingUM, MST_Currency.Code AS Currency, SO_ConfirmShipMaster.InvoiceNo,
SO_ConfirmShipDetail.InvoiceQty, SO_ConfirmShipDetail.Price, SO_ConfirmShipDetail.NetAmount, SO_ConfirmShipDetail.NetAmount * SO_ConfirmShipMaster.ExchangeRate AS NetAmountExchange,
SO_SaleType.Code AS SaleType, SO_SaleOrderMaster.Code AS SONo, SaleOrderLine, SO_DeliverySchedule.Line AS DelLine,
MST_Party.Code AS CustomerCode, MST_Party.Name AS CustomerName, SO_ConfirmShipMaster.Username
FROM SO_ConfirmShipMaster JOIN SO_ConfirmShipDetail
ON SO_ConfirmShipMaster.ConfirmShipMasterID = SO_ConfirmShipDetail.ConfirmShipMasterID
JOIN ITM_Product
ON SO_ConfirmShipDetail.ProductID = ITM_Product.ProductID
JOIN SO_DeliverySchedule
ON SO_ConfirmShipDetail.DeliveryScheduleID = SO_DeliverySchedule.DeliveryScheduleID
JOIN SO_SaleOrderMaster
ON SO_ConfirmShipMaster.SaleOrderMasterID = SO_SaleOrderMaster.SaleOrderMasterID
JOIN SO_SaleOrderDetail
ON SO_ConfirmShipDetail.SaleOrderDetailID = SO_SaleOrderDetail.SaleOrderDetailID
JOIN MST_UnitOfMeasure
ON SO_SaleOrderDetail.SellingUMID = MST_UnitOfMeasure.UnitOfMeasureID
JOIN MST_Party
ON SO_SaleOrderMaster.PartyID = MST_Party.PartyID
JOIN MST_Currency
ON SO_ConfirmShipMaster.CurrencyID = MST_Currency.CurrencyID
LEFT JOIN SO_SaleType
ON SO_SaleOrderMaster.SaleTypeiD = SO_SaleType.SaleTypeID
*/