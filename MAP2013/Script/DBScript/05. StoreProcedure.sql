/*
StoreProcedure.sql
Created by: Dung Le Anh
Purpose: For all Store Procedure.
Last Modifed: Le Anh Dung Tuesday, July 08, 2008
*/


--------------------------------- STORE PROCEDUE PART ----------------------------------------------

-- spClosePeriod
IF EXISTS (SELECT id FROM sysobjects WHERE name = 'spClosePeriod')
 DROP PROCEDURE spClosePeriod
go
CREATE PROCEDURE spClosePeriod
	@EffectDate DATETIME,
	@NextMonth DATETIME,
	@PreviousMonth DATETIME
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets FROM
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	DELETE IV_BalanceBin WHERE EffectDate = @EffectDate
	INSERT INTO IV_BalanceBin (EffectDate, LocationID, BinID, ProductID, OHQuantity, CommitQuantity, StockUMID)
	SELECT  EffectDate, LocationID, BinID, ProductID, SUM(ISNULL(OHQuantity, 0)) OHQuantity,
         SUM(ISNULL(CommitQuantity, 0)) CommitQuantity, StockUMID
    FROM (SELECT    @EffectDate EffectDate, B.OHQuantity, B.CommitQuantity, B.ProductID,
                     B.LocationID, B.BinID, P.StockUMID
           FROM      IV_BalanceBin B JOIN ITM_Product P
           ON        B.ProductID = P.ProductID
           WHERE     B.EffectDate = @PreviousMonth
           UNION ALL
           SELECT    @EffectDate EffectDate,
                     SUM(CASE WHEN T.[Type] = 0 THEN -Quantity
                              WHEN T.[Type] = 1 THEN Quantity
                              WHEN T.[Type] = 2 THEN Quantity
                         END) AS OHQuantity,
                     NULL AS CommitQuantity, H.ProductID, H.LocationID, H.BinID, P.StockUMID
           FROM      MST_TransactionHistory H JOIN MST_TranType T ON H.TranTypeID = T.TranTypeID
           JOIN      ITM_Product P ON H.ProductID = P.ProductID
           WHERE     PostDate >= @EffectDate AND PostDate < @NextMonth AND T.[Type] IN ( 0, 1, 2 )
           GROUP BY  H.LocationID, H.BinID, H.ProductID, P.StockUMID
         ) A
	WHERE A.OHQuantity <> 0
	GROUP BY EffectDate, LocationID, BinID, ProductID, StockUMID
END
GO

-- spUpdateBeginStock
IF EXISTS (select id from sysobjects where name = 'spUpdateBeginStock')
 DROP PROCEDURE spUpdateBeginStock
go
CREATE PROCEDURE spUpdateBeginStock
	@PeriodId INT,
	@EffectDate DATETIME,
	@UserName NVARCHAR(400)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets FROM
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	UPDATE IV_BeginDCPReport SET
	Username = @UserName,
	Quantity =
	(SELECT ISNULL(SUM(Quantity),0)
	FROM IV_StockTaking JOIN IV_StockTakingMaster  ON IV_StockTaking.StockTakingMasterID = IV_StockTakingMaster.StockTakingMasterID
		JOIN IV_StockTakingPeriod ON IV_StockTakingPeriod.StockTakingPeriodID = IV_StockTakingMaster.StockTakingPeriodID
		JOIN MST_Bin ON MST_Bin.BinID = IV_StockTakingMaster.BinID
		JOIN ITM_Product ON ITM_Product.ProductID = IV_StockTaking.ProductID
		WHERE IV_StockTaking.ProductID = IV_BeginDCPReport.ProductID
		AND IV_StockTakingPeriod.StockTakingPeriodID = @PeriodId
		AND ISNULL(ITM_Product.MakeItem,0) = 1
		AND MST_Bin.BinTypeID IN (1,4))
	WHERE IV_BeginDCPReport.EffectDate = @EffectDate
END
GO

-- spCloseStockTakingPeriod
IF EXISTS (select id from sysobjects where name = 'spCloseStockTakingPeriod')
 DROP PROCEDURE spCloseStockTakingPeriod
go
CREATE PROCEDURE spCloseStockTakingPeriod
	@PeriodId INT,
	@EffectDate DATETIME,
	@NextMonth DATETIME
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets FROM
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	DELETE IV_BinCache;
	DELETE IV_LocationCache;
	DELETE IV_MasLocCache;
	DELETE IV_BalanceBin WHERE EffectDate = @EffectDate;
	DELETE IV_BalanceLocation WHERE EffectDate= @EffectDate;
	DELETE iv_BalanceMasterLocation WHERE EffectDate=@EffectDate;
	
	---Update Cache
	INSERT INTO IV_BinCache (CCNID,MasterLocationID,LocationID,BinID,ProductID,OHQuantity)
	(SELECT 1 AS CCNID,8 AS MasterLocationID,M.LocationID,M.BinID,D.ProductID,SUM(D.Quantity)
	FROM IV_StockTaking D
	INNER JOIN IV_StockTakingMaster M ON D.StockTakingMasterID=M.StockTakingMasterID
	INNER JOIN IV_StockTakingPeriod ON IV_StockTakingPeriod.StockTakingPeriodID = M.StockTakingPeriodID
	WHERE IV_StockTakingPeriod.StockTakingPeriodID = @PeriodId
	GROUP BY M.LocationID,M.BinID,D.ProductID);

	INSERT INTO IV_LocationCache (CCNID,MasterLocationID,LocationID,ProductID,OHQuantity)
	(SELECT CCNID,MasterLocationID,LocationID,ProductID,SUM(OHQuantity)
	FROM IV_BinCache
	GROUP BY CCNID,MasterLocationID,LocationID,ProductID);
	
	INSERT INTO IV_MasLocCache (CCNID,MasterLocationID,ProductID,OHQuantity)
	(SELECT CCNID,MasterLocationID,ProductID,SUM(OHQuantity)
	FROM IV_BinCache
	GROUP BY CCNID,MasterLocationID,ProductID);

	--Update Balance
	INSERT INTO IV_BalanceBin (EffectDate,LocationID,BinID,ProductID,OHQuantity,StockUMID)
	(SELECT @EffectDate AS EffectDate,M.LocationID,M.BinID,D.ProductID,SUM(D.Quantity),5 AS StockUMID
	FROM IV_StockTaking D INNER JOIN IV_StockTakingMaster M ON D.StockTakingMasterID=M.StockTakingMasterID
	WHERE M.StockTakingPeriodID = @PeriodId
	GROUP BY M.LocationID,M.BinID,D.ProductID);
	--
	INSERT INTO IV_Balancelocation (EffectDate,MasterLocationID,LocationID,ProductID,OHQuantity,StockUMID)
	(SELECT EffectDate, 8 AS MasterLocationID,LocationID,ProductID,SUM(OHQuantity),5 AS StockUMID
	FROM IV_BalanceBin 
	WHERE EffectDate=@EffectDate
	GROUP BY EffectDate, LocationID,ProductID);
	--
	INSERT INTO IV_BalanceMasterLocation (EffectDate,MasterLocationID,ProductID,OHQuantity,StockUMID)
	(SELECT EffectDate,8 AS MasterLocationID,ProductID,SUM(OHQuantity),5 AS StockUMID
	FROM IV_BalanceBin
	WHERE EffectDate=@EffectDate
	GROUP BY EffectDate,ProductID);
	
	-- Close stock taking period
	UPDATE IV_StockTakingPeriod
	SET Closed = 1
	WHERE StockTakingPeriodID = @PeriodId
	-- Close current working period
	UPDATE Sys_Period
	SET Activate = 0
	WHERE FromDate = @EffectDate
	-- Activate next working period
	UPDATE Sys_Period
	SET Activate = 1
	WHERE FromDate = @NextMonth
END
GO

-- GetAvailableQtyByPostDate
IF EXISTS (select id from sysobjects where name = 'GetAvailableQtyByPostDate')
 DROP PROCEDURE GetAvailableQtyByPostDate
go
CREATE PROCEDURE GetAvailableQtyByPostDate
	@postdate datetime
AS
SELECT A.MasterLocationID, A.LocationID, A.BinID, A.ProductID,
ISNULL(A.OHQuantity,0) - ISNULL(A.CommitQuantity,0) AS AVAILABLEQUANTITY
FROM 
((SELECT IV_BinCache.MasterLocationID, IV_BinCache.LocationID, IV_BinCache.BinID, IV_BinCache.ProductID,
SUM(IV_BinCache.OHQuantity)
- ISNULL((SELECT SUM(Quantity) AS OHQuantity
FROM MST_TransactionHistory TH JOIN MST_TranType TT ON TT.TranTypeID = TH.TranTypeID 
WHERE TT.Type IN (1,2)
AND TH.MasterLocationID = IV_BinCache.MasterLocationID
AND TH.LocationID = IV_BinCache.LocationID
AND TH.BinID = IV_BinCache.BinID
AND TH.ProductID = IV_BinCache.ProductID
AND TH.PostDate > @postdate
),0)
+ ISNULL((SELECT SUM(Quantity) AS OHQuantity
FROM MST_TransactionHistory TH JOIN MST_TranType TT ON TT.TranTypeID = TH.TranTypeID 
WHERE TT.Type = 0
AND TH.MasterLocationID = IV_BinCache.MasterLocationID
AND TH.LocationID = IV_BinCache.LocationID
AND TH.BinID = IV_BinCache.BinID
AND TH.ProductID = IV_BinCache.ProductID
AND TH.PostDate > @postdate
),0) AS OHQuantity, 0 AS CommitQuantity
FROM IV_BinCache
GROUP BY IV_BinCache.MasterLocationID, IV_BinCache.LocationID, IV_BinCache.BinID, IV_BinCache.ProductID)

UNION ALL

(SELECT IV_BinCache.MasterLocationID, IV_BinCache.LocationID, IV_BinCache.BinID, IV_BinCache.ProductID,
0 AS OHQuantity, SUM(ISNULL(IV_BinCache.CommitQuantity,0))
- ISNULL((SELECT SUM(Quantity) AS OHQuantity
FROM MST_TransactionHistory TH JOIN MST_TranType TT ON TT.TranTypeID = TH.TranTypeID 
WHERE TT.Code = 'SOCommitment'
AND TH.MasterLocationID = IV_BinCache.MasterLocationID
AND TH.LocationID = IV_BinCache.LocationID
AND TH.BinID = IV_BinCache.BinID
AND TH.ProductID = IV_BinCache.ProductID
AND TH.PostDate > @postdate
),0)
+ ISNULL((SELECT SUM(Quantity) AS OHQuantity
FROM MST_TransactionHistory TH JOIN MST_TranType TT ON TT.TranTypeID = TH.TranTypeID 
WHERE TT.Code = 'SOCancelCommitment'
AND TH.MasterLocationID = IV_BinCache.MasterLocationID
AND TH.LocationID = IV_BinCache.LocationID
AND TH.BinID = IV_BinCache.BinID
AND TH.ProductID = IV_BinCache.ProductID
AND TH.PostDate > @postdate
),0)
+ ISNULL((SELECT SUM(Quantity) AS OHQuantity
FROM MST_TransactionHistory TH JOIN MST_TranType TT ON TT.TranTypeID = TH.TranTypeID 
WHERE TT.Code = 'ShippingManagement'
AND TH.MasterLocationID = IV_BinCache.MasterLocationID
AND TH.LocationID = IV_BinCache.LocationID
AND TH.BinID = IV_BinCache.BinID
AND TH.ProductID = IV_BinCache.ProductID
AND TH.PostDate > @postdate
),0) AS CommitQuantity
FROM IV_BinCache
GROUP BY IV_BinCache.MasterLocationID, IV_BinCache.LocationID, IV_BinCache.BinID, IV_BinCache.ProductID) ) A
WHERE ISNULL(A.OHQuantity,0) - ISNULL(A.CommitQuantity,0) <> 0
go