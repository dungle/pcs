using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Linq;
using System.Data.SqlClient;
using System.Transactions;
using PCSComProduction.WorkOrder.DS;
using PCSComUtils.Common;
using PCSComUtils.PCSExc;
using PCSComUtils.DataContext;
using PCSComUtils.DataAccess;
using System.Linq;
using IsolationLevel = System.Transactions.IsolationLevel;

namespace PCSComProduction.WorkOrder.BO
{
    public class MultiCompletionBO
    {
        const string This = "PCSComProduction.WorkOrder.BO.MultiCompletionBO";

        /// <summary>
        /// Searches the work order line.
        /// </summary>
        /// <param name="fromDate">From date.</param>
        /// <param name="toDate">To date.</param>
        /// <param name="masterLocationId">The master location id.</param>
        /// <param name="workOrderMasterId">The work order master id.</param>
        /// <param name="productCondition"></param>
        /// <returns></returns>
        public DataTable SearchWorkOrderLine(DateTime fromDate, DateTime toDate, int masterLocationId, int workOrderMasterId, string productCondition)
        {
            var completionDS = new PRO_WorkOrderCompletionDS();
            var dataSource = completionDS.SearchWorkOrderLine(fromDate, toDate, masterLocationId, workOrderMasterId, productCondition);
            int counter = 1;
            foreach (DataRow row in dataSource.Rows)
            {
                row["No"] = counter;
                counter++;
            }
            return dataSource;
        }

        /// <summary>
        /// Searches the work order line.
        /// </summary>
        /// <param name="multiTransNo">The multi trans no.</param>
        /// <returns></returns>
        public DataTable SearchWorkOrderLine(string multiTransNo)
        {
            var completionDS = new PRO_WorkOrderCompletionDS();
            var dataSource = completionDS.SearchWorkOrderLine(multiTransNo);
            int counter = 1;
            foreach (DataRow row in dataSource.Rows)
            {
                row["No"] = counter;
                counter++;
            }
            return dataSource;
        }

        /// <summary>
        /// Gets the completion data.
        /// </summary>
        /// <param name="multiTransNo">The multi trans no.</param>
        /// <returns></returns>
        public PRO_WorkOrderCompletion GetCompletionData(string multiTransNo)
        {
            var dataContext = new PCSDataContext(Utils.Instance.ConnectionString);
            return dataContext.PRO_WorkOrderCompletions.FirstOrDefault(w => w.MultiCompletionNo == multiTransNo);
        }

        private static PRO_WorkOrderCompletion CreateNewCompletion(DataRow row, DateTime postDate, DateTime serverDate, string multiTransNo, int masterLocationId, int shiftId, int purposeId)
        {
            var completion = new PRO_WorkOrderCompletion
                                 {
                                     BinID = (int?) row[PRO_WorkOrderCompletionTable.BINID_FLD],
                                     CCNID = SystemProperty.CCNID,
                                     CompletedDate = (DateTime?) row[PRO_WorkOrderCompletionTable.COMPLETEDDATE_FLD],
                                     CompletedQuantity =
                                         (decimal) row[PRO_WorkOrderCompletionTable.COMPLETEDQUANTITY_FLD],
                                     IssuePurposeID = purposeId,
                                     LastChange = serverDate,
                                     LocationID = (int) row[PRO_WorkOrderCompletionTable.LOCATIONID_FLD],
                                     MasterLocationID = masterLocationId,
                                     MultiCompletionNo = multiTransNo,
                                     PostDate = postDate,
                                     ProductID = (int) row[PRO_WorkOrderCompletionTable.PRODUCTID_FLD],
                                     Remark = row[PRO_WorkOrderCompletionTable.REMARK_FLD].ToString(),
                                     ShiftID = shiftId,
                                     StockUMID = (int) row[PRO_WorkOrderCompletionTable.STOCKUMID_FLD],
                                     UserName = SystemProperty.UserName,
                                     WOCompletionNo = row[PRO_WorkOrderCompletionTable.WOCOMPLETIONNO_FLD].ToString(),
                                     WorkOrderDetailID = (int) row[PRO_WorkOrderCompletionTable.WORKORDERDETAILID_FLD],
                                     WorkOrderMasterID = (int) row[PRO_WorkOrderCompletionTable.WORKORDERMASTERID_FLD],
                                     QAStatus = (byte) QAStatusEnum.QUALITY_ASSURED
                                 };
            return completion;
        }

        public void SaveCompletion(DataTable dataSource, DateTime postDate, string multiTransNo, int masterLocationId, int shiftId, int purposeId)
        {
            const string methodName = This + ".SaveCompletion()";
            try
            {
                using (var trans = new TransactionScope(TransactionScopeOption.RequiresNew, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
                {
                    using (var dataContext = new PCSDataContext(Utils.Instance.ConnectionString))
                    {
                        int tranTypeId = dataContext.MST_TranTypes.FirstOrDefault(t => t.Code == TransactionType.WORK_ORDER_COMPLETION).TranTypeID;
                        var serverDate = dataContext.GetServerDate();
                        foreach (var woCompletion in from DataRow row in dataSource.Rows
                                                     where row.RowState != DataRowState.Deleted && row.RowState != DataRowState.Detached
                                                     select CreateNewCompletion(row, postDate, serverDate, multiTransNo, masterLocationId, shiftId, purposeId))
                        {
                            // save work order completion transaction first
                            dataContext.PRO_WorkOrderCompletions.InsertOnSubmit(woCompletion);
                            dataContext.SubmitChanges(ConflictMode.ContinueOnConflict);

                            #region get list of components and subtract quantity

                            var components = dataContext.ITM_BOMs.Where(b => b.ProductID == woCompletion.ProductID);
                            // DO NOT ALLOW USER COMPLETE THE WORK ORDER THAT NOT HAS COMPONENT
                            if (components.Count() == 0)
                                throw new PCSBOException(ErrorCode.MESSAGE_NOT_HAS_COMPONENT_TO_COMPLETE, methodName, new Exception());

                            var binCacheData = from binCache in dataContext.IV_BinCaches
                                               join bin in dataContext.MST_BINs on binCache.BinID equals bin.BinID
                                               where bin.LocationID == woCompletion.PRO_WorkOrderMaster.PRO_ProductionLine.LocationID.GetValueOrDefault(0)
                                                     && bin.BinTypeID == (int?)BinTypeEnum.IN
                                               select binCache;
                            int locationId = woCompletion.PRO_WorkOrderMaster.PRO_ProductionLine.LocationID.GetValueOrDefault(0);
                            //Subtract quantity in buffer location
                            foreach (var component in components)
                            {
                                var subtractQuantity = woCompletion.CompletedQuantity * component.Quantity;
                                var binCache = binCacheData.FirstOrDefault(b => b.ProductID == component.ComponentID);
                                // check onhand quantity in case component not allow negative onhand quantity
                                if (!component.ITM_Product.AllowNegativeQty.GetValueOrDefault(false))
                                {
                                    // not enough quantity to subtract
                                    if (binCache == null || subtractQuantity > binCache.OHQuantity.GetValueOrDefault(0))
                                    {
                                        var productError = new Hashtable{{PRO_WorkOrderDetailTable.WORKORDERDETAILID_FLD, woCompletion.WorkOrderDetailID}};
                                        throw new PCSBOException(ErrorCode.MESSAGE_NOT_ENOUGH_COMPONENT_TO_COMPLETE,
                                                                 component.ITM_Product.Code, new Exception(), productError);
                                    }
                                }

                                #region substract Master Location Cache

                                var masLocCache = dataContext.IV_MasLocCaches.FirstOrDefault(m => m.MasterLocationID == woCompletion.MasterLocationID && m.ProductID == component.ComponentID);
                                if (masLocCache != null)
                                {
                                    masLocCache.OHQuantity = masLocCache.OHQuantity.GetValueOrDefault(0) - subtractQuantity;
                                }
                                else
                                {
                                    masLocCache = new IV_MasLocCache
                                                      {
                                                          CCNID = woCompletion.CCNID,
                                                          MasterLocationID = woCompletion.MasterLocationID,
                                                          ProductID = component.ComponentID,
                                                          OHQuantity = -subtractQuantity,
                                                          Lot = woCompletion.Lot
                                                      };
                                    dataContext.IV_MasLocCaches.InsertOnSubmit(masLocCache);
                                }
                                decimal? masLocOnHand = masLocCache.OHQuantity;
                                decimal? masLocCommit = masLocCache.CommitQuantity;

                                #endregion

                                #region substract Location Cache

                                var locCache = dataContext.IV_LocationCaches.FirstOrDefault(m => m.MasterLocationID == woCompletion.MasterLocationID && m.LocationID == locationId && m.ProductID == component.ComponentID);
                                if (locCache != null)
                                {
                                    locCache.OHQuantity = locCache.OHQuantity.GetValueOrDefault(0) - subtractQuantity;
                                }
                                else
                                {
                                    locCache = new IV_LocationCache
                                                   {
                                                       CCNID = woCompletion.CCNID,
                                                       MasterLocationID = woCompletion.MasterLocationID,
                                                       LocationID = locationId,
                                                       ProductID = component.ComponentID,
                                                       OHQuantity = -subtractQuantity,
                                                       Lot = woCompletion.Lot
                                                   };
                                    dataContext.IV_LocationCaches.InsertOnSubmit(locCache);
                                }
                                decimal? locOnHand = locCache.OHQuantity;
                                decimal? locCommit = locCache.CommitQuantity;

                                #endregion

                                #region substract Bin Cache

                                var componentBinCache = dataContext.IV_BinCaches.FirstOrDefault(m => m.MasterLocationID == woCompletion.MasterLocationID && m.LocationID == locationId && m.BinID == binCache.BinID && m.ProductID == component.ComponentID);
                                if (componentBinCache != null)
                                {
                                    componentBinCache.OHQuantity = componentBinCache.OHQuantity.GetValueOrDefault(0) - subtractQuantity;
                                }
                                else
                                {
                                    componentBinCache = new IV_BinCache
                                                            {
                                                                CCNID = woCompletion.CCNID,
                                                                MasterLocationID = woCompletion.MasterLocationID,
                                                                LocationID = locationId,
                                                                BinID = binCache.BinID,
                                                                ProductID = component.ComponentID,
                                                                OHQuantity = -subtractQuantity,
                                                                Lot = woCompletion.Lot
                                                            };
                                    dataContext.IV_BinCaches.InsertOnSubmit(componentBinCache);
                                }
                                decimal? binOnHand = componentBinCache.OHQuantity;
                                decimal? binCommit = componentBinCache.CommitQuantity;

                                #endregion

                                #region transaction history

                                var transHistory = new MST_TransactionHistory
                                                       {
                                                           BinID = binCache.BinID,
                                                           TransDate = serverDate,
                                                           TranTypeID = tranTypeId,
                                                           LocationID = locationId,
                                                           ProductID = component.ComponentID,
                                                           CCNID = woCompletion.CCNID,
                                                           Lot = woCompletion.Lot,
                                                           MasterLocationID = woCompletion.MasterLocationID,
                                                           StockUMID = component.ITM_Product.StockUMID,
                                                           Serial = woCompletion.Serial,
                                                           PostDate = woCompletion.PostDate,
                                                           RefMasterID = woCompletion.WorkOrderCompletionID,
                                                           Quantity = -subtractQuantity,
                                                           MasLocCommitQuantity = masLocCommit,
                                                           BinCommitQuantity = binCommit,
                                                           LocationCommitQuantity = locCommit,
                                                           MasLocOHQuantity = masLocOnHand,
                                                           LocationOHQuantity = locOnHand,
                                                           BinOHQuantity = binOnHand,
                                                           UserName = SystemProperty.UserName,
                                                           IssuePurposeID = (int?)PurposeEnum.Completion
                                                       };
                                dataContext.MST_TransactionHistories.InsertOnSubmit(transHistory);

                                #endregion
                            }

                            #endregion

                            #region add completion quantity to cache and add transaction history

                            #region add Master Location Cache

                            var masLoc = dataContext.IV_MasLocCaches.FirstOrDefault(m => m.MasterLocationID == woCompletion.MasterLocationID && m.ProductID == woCompletion.ProductID);
                            if (masLoc != null)
                            {
                                masLoc.OHQuantity = masLoc.OHQuantity.GetValueOrDefault(0) + woCompletion.CompletedQuantity;
                            }
                            else
                            {
                                masLoc = new IV_MasLocCache
                                             {
                                                 CCNID = woCompletion.CCNID,
                                                 MasterLocationID = woCompletion.MasterLocationID,
                                                 ProductID = woCompletion.ProductID,
                                                 OHQuantity = woCompletion.CompletedQuantity,
                                                 Lot = woCompletion.Lot
                                             };
                                dataContext.IV_MasLocCaches.InsertOnSubmit(masLoc);
                            }
                            decimal? masOnHand = masLoc.OHQuantity;
                            decimal? masCommit = masLoc.CommitQuantity;

                            #endregion

                            #region add Location Cache

                            var lCache = dataContext.IV_LocationCaches.FirstOrDefault(m => m.MasterLocationID == woCompletion.MasterLocationID && m.LocationID == woCompletion.LocationID && m.ProductID == woCompletion.ProductID);
                            if (lCache != null)
                            {
                                lCache.OHQuantity = lCache.OHQuantity.GetValueOrDefault(0) + woCompletion.CompletedQuantity;
                            }
                            else
                            {
                                lCache = new IV_LocationCache
                                             {
                                                 CCNID = woCompletion.CCNID,
                                                 MasterLocationID = woCompletion.MasterLocationID,
                                                 LocationID = woCompletion.LocationID,
                                                 ProductID = woCompletion.ProductID,
                                                 OHQuantity = woCompletion.CompletedQuantity,
                                                 Lot = woCompletion.Lot
                                             };
                                dataContext.IV_LocationCaches.InsertOnSubmit(lCache);
                            }
                            decimal? lOnhand = lCache.OHQuantity;
                            decimal? lCommit = lCache.CommitQuantity;

                            #endregion

                            #region add Bin Cache

                            var bCache = dataContext.IV_BinCaches.FirstOrDefault(m => m.MasterLocationID == woCompletion.MasterLocationID && m.LocationID == woCompletion.LocationID && m.BinID == woCompletion.BinID && m.ProductID == woCompletion.ProductID);
                            if (bCache != null)
                            {
                                bCache.OHQuantity = bCache.OHQuantity.GetValueOrDefault(0) + woCompletion.CompletedQuantity;
                            }
                            else
                            {
                                bCache = new IV_BinCache
                                             {
                                                 CCNID = woCompletion.CCNID,
                                                 MasterLocationID = woCompletion.MasterLocationID,
                                                 LocationID = woCompletion.LocationID,
                                                 BinID = woCompletion.BinID.GetValueOrDefault(0),
                                                 ProductID = woCompletion.ProductID,
                                                 OHQuantity = woCompletion.CompletedQuantity,
                                                 Lot = woCompletion.Lot
                                             };
                                dataContext.IV_BinCaches.InsertOnSubmit(bCache);
                            }
                            decimal? bOnhand = bCache.OHQuantity;
                            decimal? bCommit = bCache.CommitQuantity;

                            #endregion

                            #region transaction history

                            var transHistory1 = new MST_TransactionHistory
                                                    {
                                                        BinID = woCompletion.BinID,
                                                        TransDate = serverDate,
                                                        TranTypeID = tranTypeId,
                                                        LocationID = woCompletion.LocationID,
                                                        ProductID = woCompletion.ProductID,
                                                        CCNID = woCompletion.CCNID,
                                                        Lot = woCompletion.Lot,
                                                        MasterLocationID = woCompletion.MasterLocationID,
                                                        StockUMID = woCompletion.StockUMID,
                                                        Serial = woCompletion.Serial,
                                                        PostDate = woCompletion.PostDate,
                                                        RefMasterID = woCompletion.WorkOrderCompletionID,
                                                        RefDetailID = woCompletion.WorkOrderCompletionID,
                                                        Quantity = woCompletion.CompletedQuantity,
                                                        MasLocCommitQuantity = masCommit,
                                                        BinCommitQuantity = bCommit,
                                                        LocationCommitQuantity = lCommit,
                                                        MasLocOHQuantity = masOnHand,
                                                        LocationOHQuantity = lOnhand,
                                                        BinOHQuantity = bOnhand,
                                                        UserName = SystemProperty.UserName,
                                                        IssuePurposeID = (int?)PurposeEnum.Completion
                                                    };
                            dataContext.MST_TransactionHistories.InsertOnSubmit(transHistory1);

                            #endregion

                            #endregion

                            #region update work order detail status if completed

                            var orderQuantity = woCompletion.PRO_WorkOrderDetail.OrderQuantity;
                            var totalCompleted = dataContext.PRO_WorkOrderCompletions.Where(
                                w => w.WorkOrderDetailID == woCompletion.WorkOrderDetailID).Sum(w => w.CompletedQuantity);
                            if (totalCompleted >= orderQuantity)
                                dataContext.PRO_WorkOrderDetails.FirstOrDefault(w => w.WorkOrderDetailID == woCompletion.WorkOrderDetailID).Status = (byte?)WOLineStatus.MfgClose;

                            #endregion

                            // submit changes for each completion transaction
                            dataContext.SubmitChanges(ConflictMode.ContinueOnConflict);

                            #region refresh all changes made since we already submit changes

                            var changeSet = dataContext.GetChangeSet();
                            var refreshedTables = new List<ITable>();
                            foreach (var item in changeSet.Updates)
                            {
                                var table = dataContext.GetTable(item.GetType());
                                if (refreshedTables.Contains(table))
                                {
                                    continue;
                                }
                                refreshedTables.Add(table);
                                dataContext.Refresh(RefreshMode.OverwriteCurrentValues, table);
                            }
                            foreach (var item in changeSet.Deletes)
                            {
                                var table = dataContext.GetTable(item.GetType());
                                if (refreshedTables.Contains(table))
                                {
                                    continue;
                                }
                                refreshedTables.Add(table);
                                dataContext.Refresh(RefreshMode.OverwriteCurrentValues, table);
                            }
                            foreach (var item in changeSet.Inserts)
                            {
                                var table = dataContext.GetTable(item.GetType());
                                if (refreshedTables.Contains(table))
                                {
                                    continue;
                                }
                                refreshedTables.Add(table);
                                dataContext.Refresh(RefreshMode.OverwriteCurrentValues, table);
                            }

                            #endregion

                        }
                    }
                    trans.Complete();
                }
            }
            catch (SqlException ex)
            {
                if (ex.Errors.Count > 1)
                {
                    if (ex.Number == ErrorCode.SQLDUPLICATE_KEYCODE)
                        throw new PCSDBException(ErrorCode.DUPLICATE_KEY, methodName, ex);
                    throw new PCSDBException(ErrorCode.ERROR_DB, methodName, ex);
                }
                throw new PCSDBException(ErrorCode.ERROR_DB, methodName, ex);
            }
        }
    }
}
