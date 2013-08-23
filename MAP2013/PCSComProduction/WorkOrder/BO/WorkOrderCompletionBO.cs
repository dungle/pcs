using System;
using System.Data;
using System.Data.Linq;
using System.Data.SqlClient;
using System.Transactions;
using PCSComProduct.Items.DS;
using PCSComProduction.WorkOrder.DS;
using PCSComUtils.Common;
using PCSComUtils.PCSExc;
using PCSComUtils.DataContext;
using PCSComUtils.DataAccess;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using PCSComProduct.Items.BO;
using IsolationLevel = System.Transactions.IsolationLevel;

namespace PCSComProduction.WorkOrder.BO
{
    public class WorkOrderCompletionBO
    {
        const string This = "PCSComProduction.WorkOrder.BO.WorkOrderCompletionBO";

        /// <summary>
        /// GetWorkingTimeByProductionLineAndYearMonth
        /// </summary>
        /// <param name="pintProductionLineID"></param>
        /// <param name="pintYear"></param>
        /// <param name="pintMonth"></param>
        /// <returns></returns>
        /// <author>Trada</author>
        /// <date>Wednesday, July 19 2006</date>
        public DataSet GetWorkingTimeByProductionLineAndYearMonth(int productionLineId, int pintYear, int pintMonth)
        {
            PRO_WorkOrderCompletionDS dsWorkOrderCompletion = new PRO_WorkOrderCompletionDS();
            return dsWorkOrderCompletion.GetWorkingTimeByProductionLineAndYearMonth(productionLineId, pintYear, pintMonth);
        }

        /// <summary>
        /// Deletes the completion.
        /// </summary>
        /// <param name="workOrderCompletionId">The work order completion id.</param>
        public void DeleteCompletion(int workOrderCompletionId)
        {
            const string methodName = This + ".EditWorkOrder()";
            try
            {
                using (var trans = new TransactionScope(TransactionScopeOption.RequiresNew, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
                {
                    using (var dataContext = new PCSDataContext(Utils.Instance.ConnectionString))
                    {
                        int tranTypeId = dataContext.MST_TranTypes.FirstOrDefault(t => t.Code == TransactionType.WORK_ORDER_COMPLETION).TranTypeID;
                        int deleteTranTypeId = dataContext.MST_TranTypes.FirstOrDefault(t => t.Code == TransactionTypeEnum.DeleteTransaction.ToString()).TranTypeID;
                        var oldCompletion = dataContext.PRO_WorkOrderCompletions.FirstOrDefault(w => w.WorkOrderCompletionID == workOrderCompletionId);
                        var serverDate = dataContext.GetServerDate();

                        #region subtract old completed quantity to cache

                        #region subtract Master Location Cache

                        var masLoc = dataContext.IV_MasLocCaches.FirstOrDefault(m => m.MasterLocationID == oldCompletion.MasterLocationID && m.ProductID == oldCompletion.ProductID);
                        if (masLoc != null)
                        {
                            masLoc.OHQuantity = masLoc.OHQuantity.GetValueOrDefault(0) - oldCompletion.CompletedQuantity;
                        }
                        else
                        {
                            masLoc = new IV_MasLocCache
                            {
                                CCNID = oldCompletion.CCNID,
                                MasterLocationID = oldCompletion.MasterLocationID,
                                ProductID = oldCompletion.ProductID,
                                OHQuantity = -oldCompletion.CompletedQuantity,
                                Lot = oldCompletion.Lot
                            };
                            dataContext.IV_MasLocCaches.InsertOnSubmit(masLoc);
                        }
                        var masOnHand = masLoc.OHQuantity;
                        var masCommit = masLoc.CommitQuantity;

                        #endregion

                        #region subtract Location Cache

                        var lCache = dataContext.IV_LocationCaches.FirstOrDefault(m => m.MasterLocationID == oldCompletion.MasterLocationID && m.LocationID == oldCompletion.LocationID && m.ProductID == oldCompletion.ProductID);
                        if (lCache != null)
                        {
                            lCache.OHQuantity = lCache.OHQuantity.GetValueOrDefault(0) - oldCompletion.CompletedQuantity;
                        }
                        else
                        {
                            lCache = new IV_LocationCache
                            {
                                CCNID = oldCompletion.CCNID,
                                MasterLocationID = oldCompletion.MasterLocationID,
                                LocationID = oldCompletion.LocationID,
                                ProductID = oldCompletion.ProductID,
                                OHQuantity = -oldCompletion.CompletedQuantity,
                                Lot = oldCompletion.Lot
                            };
                            dataContext.IV_LocationCaches.InsertOnSubmit(lCache);
                        }
                        var lOnhand = lCache.OHQuantity;
                        var lCommit = lCache.CommitQuantity;

                        #endregion

                        #region subtract Bin Cache

                        var bCache = dataContext.IV_BinCaches.FirstOrDefault(m => m.MasterLocationID == oldCompletion.MasterLocationID && m.LocationID == oldCompletion.LocationID && m.BinID == oldCompletion.BinID && m.ProductID == oldCompletion.ProductID);
                        if (bCache != null)
                        {
                            bCache.OHQuantity = bCache.OHQuantity.GetValueOrDefault(0) - oldCompletion.CompletedQuantity;
                        }
                        else
                        {
                            bCache = new IV_BinCache
                            {
                                CCNID = oldCompletion.CCNID,
                                MasterLocationID = oldCompletion.MasterLocationID,
                                LocationID = oldCompletion.LocationID,
                                BinID = oldCompletion.BinID.GetValueOrDefault(0),
                                ProductID = oldCompletion.ProductID,
                                OHQuantity = -oldCompletion.CompletedQuantity,
                                Lot = oldCompletion.Lot
                            };
                            dataContext.IV_BinCaches.InsertOnSubmit(bCache);
                        }
                        var bOnhand = bCache.OHQuantity;
                        var bCommit = bCache.CommitQuantity;

                        #endregion

                        #region transaction history

                        var transHistory1 = new MST_TransactionHistory
                        {
                            BinID = oldCompletion.BinID,
                            TransDate = serverDate,
                            TranTypeID = tranTypeId,
                            LocationID = oldCompletion.LocationID,
                            ProductID = oldCompletion.ProductID,
                            CCNID = oldCompletion.CCNID,
                            Lot = oldCompletion.Lot,
                            MasterLocationID = oldCompletion.MasterLocationID,
                            StockUMID = oldCompletion.StockUMID,
                            Serial = oldCompletion.Serial,
                            PostDate = oldCompletion.PostDate,
                            RefMasterID = oldCompletion.WorkOrderCompletionID,
                            RefDetailID = oldCompletion.WorkOrderCompletionID,
                            Quantity = -oldCompletion.CompletedQuantity,
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

                        #region update work order detail status

                        var workOrderDetail = dataContext.PRO_WorkOrderDetails.FirstOrDefault(w => w.WorkOrderDetailID == oldCompletion.WorkOrderDetailID);
                        workOrderDetail.Status = (byte?)WOLineStatus.Released;

                        #endregion

                        #region get list of components and add quantity

                        var components = dataContext.ITM_BOMs.Where(b => b.ProductID == oldCompletion.ProductID);
                        // DO NOT ALLOW USER COMPLETE THE WORK ORDER THAT NOT HAS COMPONENT
                        if (components.Count() == 0)
                            throw new PCSBOException(ErrorCode.MESSAGE_NOT_HAS_COMPONENT_TO_COMPLETE, methodName, new Exception());

                        int locationId = oldCompletion.PRO_WorkOrderMaster.PRO_ProductionLine.LocationID.GetValueOrDefault(0);
                        var binCacheData = from binCache in dataContext.IV_BinCaches
                                           join bin in dataContext.MST_BINs on binCache.BinID equals bin.BinID
                                           where bin.LocationID == locationId
                                           && bin.BinTypeID == (int?)BinTypeEnum.IN
                                           select binCache;
                        //Subtract quantity in buffer location
                        foreach (var component in components)
                        {
                            #region add old quantity

                            var addQuantity = oldCompletion.CompletedQuantity * component.Quantity;
                            var binCache = binCacheData.FirstOrDefault(b => b.ProductID == component.ComponentID);

                            #region add Master Location Cache

                            var comMasLocCache = dataContext.IV_MasLocCaches.FirstOrDefault(m => m.MasterLocationID == oldCompletion.MasterLocationID && m.ProductID == component.ComponentID);
                            if (comMasLocCache != null)
                            {
                                comMasLocCache.OHQuantity = comMasLocCache.OHQuantity.GetValueOrDefault(0) + addQuantity;
                            }
                            else
                            {
                                comMasLocCache = new IV_MasLocCache
                                {
                                    CCNID = oldCompletion.CCNID,
                                    MasterLocationID = oldCompletion.MasterLocationID,
                                    ProductID = component.ComponentID,
                                    OHQuantity = addQuantity,
                                    Lot = oldCompletion.Lot
                                };
                                dataContext.IV_MasLocCaches.InsertOnSubmit(comMasLocCache);
                            }
                            decimal? comMasLocOH = comMasLocCache.OHQuantity;
                            decimal? comMasLocCommit = comMasLocCache.CommitQuantity;

                            #endregion

                            #region add Location Cache

                            var comLocCache = dataContext.IV_LocationCaches.FirstOrDefault(m => m.MasterLocationID == oldCompletion.MasterLocationID && m.LocationID == locationId && m.ProductID == component.ComponentID);
                            if (comLocCache != null)
                            {
                                comLocCache.OHQuantity = comLocCache.OHQuantity.GetValueOrDefault(0) + addQuantity;
                            }
                            else
                            {
                                comLocCache = new IV_LocationCache
                                {
                                    CCNID = oldCompletion.CCNID,
                                    MasterLocationID = oldCompletion.MasterLocationID,
                                    LocationID = locationId,
                                    ProductID = component.ComponentID,
                                    OHQuantity = addQuantity,
                                    Lot = oldCompletion.Lot
                                };
                                dataContext.IV_LocationCaches.InsertOnSubmit(comLocCache);
                            }
                            decimal? comLocOnHand = comLocCache.OHQuantity;
                            decimal? comLocCommit = comLocCache.CommitQuantity;

                            #endregion

                            #region add Bin Cache

                            var comBinCache = dataContext.IV_BinCaches.FirstOrDefault(m => m.MasterLocationID == oldCompletion.MasterLocationID && m.LocationID == locationId && m.BinID == binCache.BinID && m.ProductID == component.ComponentID);
                            if (comBinCache != null)
                            {
                                comBinCache.OHQuantity = comBinCache.OHQuantity.GetValueOrDefault(0) + addQuantity;
                            }
                            else
                            {
                                comBinCache = new IV_BinCache
                                {
                                    CCNID = oldCompletion.CCNID,
                                    MasterLocationID = oldCompletion.MasterLocationID,
                                    LocationID = locationId,
                                    BinID = binCache.BinID,
                                    ProductID = component.ComponentID,
                                    OHQuantity = addQuantity,
                                    Lot = oldCompletion.Lot
                                };
                                dataContext.IV_BinCaches.InsertOnSubmit(comBinCache);
                            }
                            decimal? comBinOnHand = comBinCache.OHQuantity;
                            decimal? comBinCommit = comBinCache.CommitQuantity;

                            #endregion

                            #region transaction history

                            var transHistory2 = new MST_TransactionHistory
                            {
                                BinID = binCache.BinID,
                                TransDate = serverDate,
                                TranTypeID = tranTypeId,
                                LocationID = locationId,
                                ProductID = component.ComponentID,
                                CCNID = oldCompletion.CCNID,
                                Lot = oldCompletion.Lot,
                                MasterLocationID = oldCompletion.MasterLocationID,
                                StockUMID = component.ITM_Product.StockUMID,
                                Serial = oldCompletion.Serial,
                                PostDate = oldCompletion.PostDate,
                                RefMasterID = oldCompletion.WorkOrderCompletionID,
                                Quantity = addQuantity,
                                MasLocCommitQuantity = comMasLocCommit,
                                BinCommitQuantity = comBinCommit,
                                LocationCommitQuantity = comLocCommit,
                                MasLocOHQuantity = comMasLocOH,
                                LocationOHQuantity = comLocOnHand,
                                BinOHQuantity = comBinOnHand,
                                UserName = SystemProperty.UserName,
                                IssuePurposeID = (int?)PurposeEnum.Completion
                            };
                            dataContext.MST_TransactionHistories.InsertOnSubmit(transHistory2);

                            #endregion

                            #endregion
                        }

                        #endregion

                        var transHist = dataContext.MST_TransactionHistories.FirstOrDefault(t => t.RefMasterID == workOrderCompletionId && t.TranTypeID == tranTypeId);
                        transHist.InspStatus = 19;
                        transHist.TranTypeID = deleteTranTypeId;

                        dataContext.PRO_WorkOrderCompletions.DeleteOnSubmit(oldCompletion);
                        dataContext.SubmitChanges(ConflictMode.FailOnFirstConflict);
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

        /// <summary>
        /// Gets the work order completion.
        /// </summary>
        /// <param name="workOrderCompletionId">The work order completion id.</param>
        /// <returns></returns>
        public PRO_WorkOrderCompletion GetWorkOrderCompletion(int workOrderCompletionId)
        {
            var db = new PCSDataContext(Utils.Instance.ConnectionString);
            return db.PRO_WorkOrderCompletions.FirstOrDefault(e => e.WorkOrderCompletionID == workOrderCompletionId);
        }
        /// <summary>
        /// Gets the work order master.
        /// </summary>
        /// <param name="workOrderMasterId">The work order master id.</param>
        /// <returns></returns>
        public PRO_WorkOrderMaster GetWorkOrderMaster(int workOrderMasterId)
        {
            var db = new PCSDataContext(Utils.Instance.ConnectionString);
            return db.PRO_WorkOrderMasters.FirstOrDefault(e => e.WorkOrderMasterID == workOrderMasterId);
        }

        /// <summary>
        /// Edits the work order.
        /// </summary>
        /// <param name="woCompletion">The wo completion.</param>
        public void EditWorkOrder(PRO_WorkOrderCompletion woCompletion)
        {
            const string methodName = This + ".EditWorkOrder()";
            try
            {
                using (var trans = new TransactionScope(TransactionScopeOption.RequiresNew, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
                {
                    using (var dataContext = new PCSDataContext(Utils.Instance.ConnectionString))
                    {
                        int tranTypeId = dataContext.MST_TranTypes.FirstOrDefault(t => t.Code == TransactionType.WORK_ORDER_COMPLETION).TranTypeID;
                        var oldCompletion = dataContext.PRO_WorkOrderCompletions.FirstOrDefault(w => w.WorkOrderCompletionID == woCompletion.WorkOrderCompletionID);
                        var serverDate = dataContext.GetServerDate();

                        #region add new completed quantity to cache and add transaction history

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

                        #region subtract old completed quantity to cache

                        #region subtract Master Location Cache

                        masLoc = dataContext.IV_MasLocCaches.FirstOrDefault(m => m.MasterLocationID == oldCompletion.MasterLocationID && m.ProductID == oldCompletion.ProductID);
                        if (masLoc != null)
                        {
                            masLoc.OHQuantity = masLoc.OHQuantity.GetValueOrDefault(0) - oldCompletion.CompletedQuantity;
                        }
                        else
                        {
                            masLoc = new IV_MasLocCache
                            {
                                CCNID = woCompletion.CCNID,
                                MasterLocationID = oldCompletion.MasterLocationID,
                                ProductID = oldCompletion.ProductID,
                                OHQuantity = -oldCompletion.CompletedQuantity,
                                Lot = woCompletion.Lot
                            };
                            dataContext.IV_MasLocCaches.InsertOnSubmit(masLoc);
                        }
                        masOnHand = masLoc.OHQuantity;
                        masCommit = masLoc.CommitQuantity;

                        #endregion

                        #region subtract Location Cache

                        lCache = dataContext.IV_LocationCaches.FirstOrDefault(m => m.MasterLocationID == oldCompletion.MasterLocationID && m.LocationID == oldCompletion.LocationID && m.ProductID == oldCompletion.ProductID);
                        if (lCache != null)
                        {
                            lCache.OHQuantity = lCache.OHQuantity.GetValueOrDefault(0) - oldCompletion.CompletedQuantity;
                        }
                        else
                        {
                            lCache = new IV_LocationCache
                            {
                                CCNID = oldCompletion.CCNID,
                                MasterLocationID = oldCompletion.MasterLocationID,
                                LocationID = oldCompletion.LocationID,
                                ProductID = oldCompletion.ProductID,
                                OHQuantity = -oldCompletion.CompletedQuantity,
                                Lot = oldCompletion.Lot
                            };
                            dataContext.IV_LocationCaches.InsertOnSubmit(lCache);
                        }
                        lOnhand = lCache.OHQuantity;
                        lCommit = lCache.CommitQuantity;

                        #endregion

                        #region subtract Bin Cache

                        bCache = dataContext.IV_BinCaches.FirstOrDefault(m => m.MasterLocationID == oldCompletion.MasterLocationID && m.LocationID == oldCompletion.LocationID && m.BinID == oldCompletion.BinID && m.ProductID == oldCompletion.ProductID);
                        if (bCache != null)
                        {
                            bCache.OHQuantity = bCache.OHQuantity.GetValueOrDefault(0) - oldCompletion.CompletedQuantity;
                        }
                        else
                        {
                            bCache = new IV_BinCache
                            {
                                CCNID = oldCompletion.CCNID,
                                MasterLocationID = oldCompletion.MasterLocationID,
                                LocationID = oldCompletion.LocationID,
                                BinID = oldCompletion.BinID.GetValueOrDefault(0),
                                ProductID = oldCompletion.ProductID,
                                OHQuantity = -oldCompletion.CompletedQuantity,
                                Lot = oldCompletion.Lot
                            };
                            dataContext.IV_BinCaches.InsertOnSubmit(bCache);
                        }
                        bOnhand = bCache.OHQuantity;
                        bCommit = bCache.CommitQuantity;

                        #endregion

                        #region transaction history

                        transHistory1 = new MST_TransactionHistory
                        {
                            BinID = oldCompletion.BinID,
                            TransDate = serverDate,
                            TranTypeID = tranTypeId,
                            LocationID = oldCompletion.LocationID,
                            ProductID = oldCompletion.ProductID,
                            CCNID = oldCompletion.CCNID,
                            Lot = oldCompletion.Lot,
                            MasterLocationID = oldCompletion.MasterLocationID,
                            StockUMID = oldCompletion.StockUMID,
                            Serial = oldCompletion.Serial,
                            PostDate = oldCompletion.PostDate,
                            RefMasterID = oldCompletion.WorkOrderCompletionID,
                            RefDetailID = oldCompletion.WorkOrderCompletionID,
                            Quantity = -oldCompletion.CompletedQuantity,
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

                        #region update work order detail status

                        var orderQuantity = woCompletion.PRO_WorkOrderDetail.OrderQuantity;
                        var totalCompleted = dataContext.PRO_WorkOrderCompletions.Where(w => w.WorkOrderDetailID == woCompletion.WorkOrderDetailID && w.WorkOrderCompletionID != woCompletion.WorkOrderCompletionID).Sum(w => w.CompletedQuantity);
                        totalCompleted += woCompletion.CompletedQuantity;
                        var workOrderDetail = dataContext.PRO_WorkOrderDetails.FirstOrDefault(w => w.WorkOrderDetailID == woCompletion.WorkOrderDetailID);
                        workOrderDetail.Status = totalCompleted >= orderQuantity
                                                     ? (byte?) WOLineStatus.MfgClose
                                                     : (byte?) WOLineStatus.Released;

                        #endregion

                        #region get list of components and subtract quantity

                        var components = dataContext.ITM_BOMs.Where(b => b.ProductID == woCompletion.ProductID);
                        // DO NOT ALLOW USER COMPLETE THE WORK ORDER THAT NOT HAS COMPONENT
                        if (components.Count() == 0)
                            throw new PCSBOException(ErrorCode.MESSAGE_NOT_HAS_COMPONENT_TO_COMPLETE, methodName, new Exception());

                        int locationId = oldCompletion.PRO_WorkOrderMaster.PRO_ProductionLine.LocationID.GetValueOrDefault(0);
                        var binCacheData = from binCache in dataContext.IV_BinCaches
                                           join bin in dataContext.MST_BINs on binCache.BinID equals bin.BinID
                                           where bin.LocationID == locationId
                                           && bin.BinTypeID == (int?)BinTypeEnum.IN
                                           select binCache;
                        //Subtract quantity in buffer location
                        foreach (var component in components)
                        {
                            #region add old quantity

                            var addQuantity = oldCompletion.CompletedQuantity * component.Quantity;
                            var binCache = binCacheData.FirstOrDefault(b => b.ProductID == component.ComponentID);
                            
                            #region add Master Location Cache

                            var comMasLocCache = dataContext.IV_MasLocCaches.FirstOrDefault(m => m.MasterLocationID == oldCompletion.MasterLocationID && m.ProductID == component.ComponentID);
                            if (comMasLocCache != null)
                            {
                                comMasLocCache.OHQuantity = comMasLocCache.OHQuantity.GetValueOrDefault(0) + addQuantity;
                            }
                            else
                            {
                                comMasLocCache = new IV_MasLocCache
                                {
                                    CCNID = woCompletion.CCNID,
                                    MasterLocationID = woCompletion.MasterLocationID,
                                    ProductID = component.ComponentID,
                                    OHQuantity = addQuantity,
                                    Lot = woCompletion.Lot
                                };
                                dataContext.IV_MasLocCaches.InsertOnSubmit(comMasLocCache);
                            }
                            decimal? comMasLocOH = comMasLocCache.OHQuantity;
                            decimal? comMasLocCommit = comMasLocCache.CommitQuantity;

                            #endregion

                            #region add Location Cache

                            var comLocCache = dataContext.IV_LocationCaches.FirstOrDefault(m => m.MasterLocationID == oldCompletion.MasterLocationID && m.LocationID == locationId && m.ProductID == component.ComponentID);
                            if (comLocCache != null)
                            {
                                comLocCache.OHQuantity = comLocCache.OHQuantity.GetValueOrDefault(0) + addQuantity;
                            }
                            else
                            {
                                comLocCache = new IV_LocationCache
                                {
                                    CCNID = oldCompletion.CCNID,
                                    MasterLocationID = oldCompletion.MasterLocationID,
                                    LocationID = locationId,
                                    ProductID = component.ComponentID,
                                    OHQuantity = addQuantity,
                                    Lot = oldCompletion.Lot
                                };
                                dataContext.IV_LocationCaches.InsertOnSubmit(comLocCache);
                            }
                            decimal? comLocOnHand = comLocCache.OHQuantity;
                            decimal? comLocCommit = comLocCache.CommitQuantity;

                            #endregion

                            #region add Bin Cache

                            var comBinCache = dataContext.IV_BinCaches.FirstOrDefault(m => m.MasterLocationID == oldCompletion.MasterLocationID && m.LocationID == locationId && m.BinID == binCache.BinID && m.ProductID == component.ComponentID);
                            if (comBinCache != null)
                            {
                                comBinCache.OHQuantity = comBinCache.OHQuantity.GetValueOrDefault(0) + addQuantity;
                            }
                            else
                            {
                                comBinCache = new IV_BinCache
                                {
                                    CCNID = woCompletion.CCNID,
                                    MasterLocationID = woCompletion.MasterLocationID,
                                    LocationID = locationId,
                                    BinID = binCache.BinID,
                                    ProductID = component.ComponentID,
                                    OHQuantity = addQuantity,
                                    Lot = woCompletion.Lot
                                };
                                dataContext.IV_BinCaches.InsertOnSubmit(comBinCache);
                            }
                            decimal? comBinOnHand = comBinCache.OHQuantity;
                            decimal? comBinCommit = comBinCache.CommitQuantity;

                            #endregion

                            #region transaction history

                            var transHistory2 = new MST_TransactionHistory
                            {
                                BinID = binCache.BinID,
                                TransDate = serverDate,
                                TranTypeID = tranTypeId,
                                LocationID = locationId,
                                ProductID = component.ComponentID,
                                CCNID = oldCompletion.CCNID,
                                Lot = woCompletion.Lot,
                                MasterLocationID = woCompletion.MasterLocationID,
                                StockUMID = component.ITM_Product.StockUMID,
                                Serial = woCompletion.Serial,
                                PostDate = woCompletion.PostDate,
                                RefMasterID = woCompletion.WorkOrderCompletionID,
                                Quantity = addQuantity,
                                MasLocCommitQuantity = comMasLocCommit,
                                BinCommitQuantity = comBinCommit,
                                LocationCommitQuantity = comLocCommit,
                                MasLocOHQuantity = comMasLocOH,
                                LocationOHQuantity = comLocOnHand,
                                BinOHQuantity = comBinOnHand,
                                UserName = SystemProperty.UserName,
                                IssuePurposeID = (int?)PurposeEnum.Completion
                            };
                            dataContext.MST_TransactionHistories.InsertOnSubmit(transHistory2);

                            #endregion

                            #endregion

                            #region substract new quantity

                            var subtractQuantity = woCompletion.CompletedQuantity * component.Quantity;
                            binCache = binCacheData.FirstOrDefault(b => b.ProductID == component.ComponentID);
                            // check onhand quantity in case component not allow negative onhand quantity
                            if (!component.ITM_Product.AllowNegativeQty.GetValueOrDefault(false))
                            {
                                // not enough quantity to subtract
                                if (binCache == null || subtractQuantity > binCache.OHQuantity.GetValueOrDefault(0))
                                {
                                    throw new PCSBOException(ErrorCode.MESSAGE_NOT_ENOUGH_COMPONENT_TO_COMPLETE,
                                             component.ITM_Product.Code, new Exception());
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

                            #endregion
                        }

                        #endregion

                        #region update work order completion information
                        
                        oldCompletion.CompletedQuantity = woCompletion.CompletedQuantity;
                        oldCompletion.BinID = woCompletion.BinID;
                        oldCompletion.CCNID = woCompletion.CCNID;
                        oldCompletion.CompletedDate = woCompletion.CompletedDate;
                        oldCompletion.IssuePurposeID = woCompletion.IssuePurposeID;
                        oldCompletion.LastChange = serverDate;
                        oldCompletion.LocationID = woCompletion.LocationID;
                        oldCompletion.Lot = woCompletion.Lot;
                        oldCompletion.MasterLocationID = woCompletion.MasterLocationID;
                        oldCompletion.PostDate = woCompletion.PostDate;
                        oldCompletion.ProductID = woCompletion.ProductID;
                        oldCompletion.QAStatus = woCompletion.QAStatus;
                        oldCompletion.Remark = woCompletion.Remark;
                        oldCompletion.Serial = woCompletion.Serial;
                        oldCompletion.ShiftID = woCompletion.ShiftID;
                        oldCompletion.StockUMID = woCompletion.StockUMID;
                        oldCompletion.UserName = SystemProperty.UserName;
                        oldCompletion.WOCompletionNo = woCompletion.WOCompletionNo;

                        #endregion

                        dataContext.SubmitChanges(ConflictMode.FailOnFirstConflict);
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
        /// <summary>
        /// Adds the new.
        /// </summary>
        /// <param name="woCompletion">The wo completion.</param>
        /// <param name="productionLineId">The production line id.</param>
        /// <returns></returns>
        public int AddNew(PRO_WorkOrderCompletion woCompletion, int productionLineId)
        {
            const string methodName = This + ".AddNew()";
            try
            {
                using (var trans = new TransactionScope(TransactionScopeOption.RequiresNew, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
                {
                    using (var dataContext = new PCSDataContext(Utils.Instance.ConnectionString))
                    {
                        int tranTypeId = dataContext.MST_TranTypes.FirstOrDefault(t => t.Code == TransactionType.WORK_ORDER_COMPLETION).TranTypeID;
                        // save work order completion transaction first
                        dataContext.PRO_WorkOrderCompletions.InsertOnSubmit(woCompletion);
                        dataContext.SubmitChanges(ConflictMode.FailOnFirstConflict);

                        #region get list of components and subtract quantity

                        var components = dataContext.ITM_BOMs.Where(b => b.ProductID == woCompletion.ProductID);
                        // DO NOT ALLOW USER COMPLETE THE WORK ORDER THAT NOT HAS COMPONENT
                        if (components.Count() == 0)
                            throw new PCSBOException(ErrorCode.MESSAGE_NOT_HAS_COMPONENT_TO_COMPLETE, methodName, new Exception());

                        var serverDate = dataContext.GetServerDate();
                        var binCacheData = from binCache in dataContext.IV_BinCaches
                                           join bin in dataContext.MST_BINs on binCache.BinID equals bin.BinID
                                           where bin.LocationID == woCompletion.PRO_WorkOrderMaster.PRO_ProductionLine.LocationID.GetValueOrDefault(0)
                                           && bin.BinTypeID == (int?)BinTypeEnum.IN
                                           select binCache;
                        int locationId = woCompletion.PRO_WorkOrderMaster.PRO_ProductionLine.LocationID.GetValueOrDefault(0);
                        //Subtract quantity in buffer location
                        foreach (var component in components)
                        {
                            var subtractQuantity = woCompletion.CompletedQuantity*component.Quantity;
                            var binCache = binCacheData.FirstOrDefault(b => b.ProductID == component.ComponentID);
                            // check onhand quantity in case component not allow negative onhand quantity
                            if (!component.ITM_Product.AllowNegativeQty.GetValueOrDefault(false))
                            {
                                // not enough quantity to subtract
                                if (binCache == null || subtractQuantity > binCache.OHQuantity.GetValueOrDefault(0))
                                {
                                    throw new PCSBOException(ErrorCode.MESSAGE_NOT_ENOUGH_COMPONENT_TO_COMPLETE,
                                             component.ITM_Product.Code, new Exception());
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
                                                       IssuePurposeID = (int?) PurposeEnum.Completion
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

                        decimal? bOnhand, bCommit;
                        var bCache = dataContext.IV_BinCaches.FirstOrDefault(m => m.MasterLocationID == woCompletion.MasterLocationID && m.LocationID == woCompletion.LocationID && m.BinID == woCompletion.BinID && m.ProductID == woCompletion.ProductID);
                        if (bCache != null)
                        {
                            bCache.OHQuantity = bCache.OHQuantity.GetValueOrDefault(0) + woCompletion.CompletedQuantity;
                        }
                        else
                        {
                            bCache = new IV_BinCache()
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
                        bOnhand = bCache.OHQuantity;
                        bCommit = bCache.CommitQuantity;

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

                        dataContext.SubmitChanges(ConflictMode.FailOnFirstConflict);
                    }
                    trans.Complete();
                    return woCompletion.WorkOrderCompletionID;
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
