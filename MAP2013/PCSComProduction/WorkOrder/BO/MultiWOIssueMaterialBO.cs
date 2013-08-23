using System;
using System.Collections;
using System.Data;
using PCSComProduction.DCP.DS;
using PCSComProduction.WorkOrder.DS;
using PCSComUtils.Common;
using PCSComUtils.MasterSetup.DS;
using PCSComUtils.DataContext;
using System.Linq;
using System.Transactions;
using PCSComUtils.PCSExc;
using PCSComUtils.DataAccess;
using IsolationLevel = System.Transactions.IsolationLevel;

namespace PCSComProduction.WorkOrder.BO
{
	/// <summary>
	/// Multi WO Issue Material BO
	/// </summary>
	public class MultiWOIssueMaterialBO
	{
        private const string This = "PCSComProduction.WorkOrder.BO.MultiWOIssueMaterialBO";

		#region IMultiWOIssueMaterialBO Members

		public DataSet GetWorkingTime()
		{
			var dsShiftPattern = new PRO_ShiftPatternDS();
			return 	dsShiftPattern.GetWorkingTime();
		}
        public decimal GetOHQuantity(int iCcnid, int iBinId, int iLocationId, int iProductId)
        {
            using (var db = new PCSDataContext(Utils.Instance.ConnectionString))
            {
                var objBin = db.IV_BinCaches.SingleOrDefault(e => e.LocationID == iLocationId && e.BinID == iBinId && e.ProductID == iProductId);
                if (objBin != null)
                {
                    if (objBin.OHQuantity != null) return Convert.ToDecimal(objBin.OHQuantity.ToString());
                }
            }
            return 0;
        }
		
        public int AddAndReturnId(object pobjMaster, DataSet pdstDetailData)
		{
            const string methodName = "PCSComProduction.WorkOrder.BO.AddAndReturnID()";
			try
            {
                using (var trans = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions {IsolationLevel = IsolationLevel.ReadCommitted, Timeout = new TimeSpan(0, 1, 0)}))
                {
                    using (var db = new PCSDataContext(Utils.Instance.ConnectionString))
                    {
                        var serverDate = db.GetServerDate();
                        var tranTypeId = db.MST_TranTypes.FirstOrDefault(t => t.Code == TransactionTypeEnum.PROIssueMaterial.ToString()).TranTypeID;

                        #region add the master first

                        //add the master first
                        var objMaster = (PRO_IssueMaterialMasterVO)pobjMaster;
                        var issueMaster = new PRO_IssueMaterialMaster
                        {
                            MasterLocationID = objMaster.MasterLocationID,
                            PostDate = objMaster.PostDate,
                            IssueNo = objMaster.IssueNo,
                            CCNID = objMaster.CCNID,
                            IssuePurposeID = objMaster.IssuePurposeID,
                            ToLocationID = objMaster.ToLocationID,
                            ToBinID = objMaster.ToBinID,
                            ShiftID = objMaster.ShiftID
                        };
                        if (objMaster.WorkOrderMasterID > 0)
                            issueMaster.WorkOrderMasterID = objMaster.WorkOrderMasterID;
                        if (objMaster.WorkOrderDetailID > 0)
                            issueMaster.WorkOrderDetailID = objMaster.WorkOrderDetailID;
                        
                        #endregion

                        #region insert issue detail
                        
                        //update the detail
                        foreach (var issueDetail in
                           from DataRow dr in pdstDetailData.Tables[0].Rows
                           where dr.RowState != DataRowState.Deleted
                           select CreateIssueDetail(dr))
                        {
                            issueMaster.PRO_IssueMaterialDetails.Add(issueDetail);
                        }
                        db.PRO_IssueMaterialMasters.InsertOnSubmit(issueMaster);
                        db.SubmitChanges();

                        #endregion

                        #region cache data

                        foreach (var issueDetail in issueMaster.PRO_IssueMaterialDetails)
                        {
                            var allowNegative = issueDetail.ITM_Product.AllowNegativeQty.GetValueOrDefault(false);

                            #region subtract from source cache

                            #region bin cache

                            var sourceBin = db.IV_BinCaches.FirstOrDefault(e => e.LocationID == issueDetail.LocationID && e.BinID == issueDetail.BinID && e.ProductID == issueDetail.ProductID);
                            if (!allowNegative && (sourceBin == null || sourceBin.OHQuantity.GetValueOrDefault(0) < issueDetail.CommitQuantity))
                            {
                                var productError = new Hashtable
                                                       {
                                                           { ITM_ProductTable.PRODUCTID_FLD, issueDetail.ProductID },
                                                           { IV_BinCacheTable.OHQUANTITY_FLD, sourceBin == null ? 0 : sourceBin.OHQuantity.GetValueOrDefault(0) }
                                                       };
                                throw new PCSBOException(ErrorCode.MESSAGE_NOT_ENOUGH_COMPONENT_TO_COMPLETE, issueDetail.ITM_Product.Code, new Exception(), productError);
                            }
                            if (sourceBin != null)
                            {
                                sourceBin.OHQuantity = sourceBin.OHQuantity.GetValueOrDefault(0) - issueDetail.CommitQuantity;
                            }
                            else
                            {
                                // create new record
                                sourceBin = new IV_BinCache
                                {
                                    BinID = issueDetail.BinID.GetValueOrDefault(0),
                                    CCNID = issueMaster.CCNID,
                                    LocationID = issueDetail.LocationID,
                                    MasterLocationID = issueMaster.MasterLocationID,
                                    ProductID = issueDetail.ProductID,
                                    OHQuantity = -issueDetail.CommitQuantity
                                };
                                db.IV_BinCaches.InsertOnSubmit(sourceBin);
                            }

                            #endregion

                            #region location cache

                            var sourceLocation = db.IV_LocationCaches.FirstOrDefault(e => e.MasterLocationID == issueMaster.MasterLocationID && e.LocationID == issueDetail.LocationID && e.ProductID == issueDetail.ProductID);
                            if (!allowNegative && (sourceLocation == null || sourceLocation.OHQuantity.GetValueOrDefault(0) < issueDetail.CommitQuantity))
                            {
                                var productError = new Hashtable
                                                       {
                                                           { ITM_ProductTable.PRODUCTID_FLD, issueDetail.ProductID },
                                                           { IV_BinCacheTable.OHQUANTITY_FLD, sourceLocation == null ? 0 : sourceLocation.OHQuantity.GetValueOrDefault(0) }
                                                       };
                                throw new PCSBOException(ErrorCode.MESSAGE_NOT_ENOUGH_COMPONENT_TO_COMPLETE, issueDetail.ITM_Product.Code, new Exception(), productError);
                            }
                            if (sourceLocation != null)
                            {
                                sourceLocation.OHQuantity = sourceLocation.OHQuantity.GetValueOrDefault(0) - issueDetail.CommitQuantity;
                            }
                            else
                            {
                                // create new record
                                sourceLocation = new IV_LocationCache
                                {
                                    CCNID = issueMaster.CCNID,
                                    LocationID = issueDetail.LocationID,
                                    MasterLocationID = issueMaster.MasterLocationID,
                                    ProductID = issueDetail.ProductID,
                                    OHQuantity = -issueDetail.CommitQuantity
                                };
                                db.IV_LocationCaches.InsertOnSubmit(sourceLocation);
                            }

                            #endregion

                            #region master location cache

                            var sourceMasLocation = db.IV_MasLocCaches.FirstOrDefault(e => e.MasterLocationID == issueMaster.MasterLocationID && e.ProductID == issueDetail.ProductID);
                            if (!allowNegative && (sourceMasLocation == null || sourceMasLocation.OHQuantity.GetValueOrDefault(0) < issueDetail.CommitQuantity))
                            {
                                var productError = new Hashtable
                                                       {
                                                           { ITM_ProductTable.PRODUCTID_FLD, issueDetail.ProductID },
                                                           { IV_BinCacheTable.OHQUANTITY_FLD, sourceMasLocation == null ? 0 : sourceMasLocation.OHQuantity.GetValueOrDefault(0) }
                                                       };
                                throw new PCSBOException(ErrorCode.MESSAGE_NOT_ENOUGH_COMPONENT_TO_COMPLETE, issueDetail.ITM_Product.Code, new Exception(), productError);
                            }
                            if (sourceMasLocation != null)
                            {
                                sourceMasLocation.OHQuantity = sourceMasLocation.OHQuantity.GetValueOrDefault(0) - issueDetail.CommitQuantity;
                            }
                            else
                            {
                                // create new record
                                sourceMasLocation = new IV_MasLocCache
                                {
                                    CCNID = issueMaster.CCNID,
                                    MasterLocationID = issueMaster.MasterLocationID,
                                    ProductID = issueDetail.ProductID,
                                    OHQuantity = -issueDetail.CommitQuantity
                                };
                                db.IV_MasLocCaches.InsertOnSubmit(sourceMasLocation);
                            }

                            #endregion

                            #region Transaction history

                            var sourceHistory = new MST_TransactionHistory
                            {
                                CCNID = issueMaster.CCNID,
                                StockUMID = issueDetail.StockUMID,
                                MasterLocationID = issueMaster.MasterLocationID,
                                ProductID = issueDetail.ProductID,
                                LocationID = issueDetail.LocationID,
                                BinID = issueDetail.BinID,
                                RefMasterID = issueMaster.IssueMaterialMasterID,
                                RefDetailID = issueDetail.IssueMaterialDetailID,
                                PostDate = issueMaster.PostDate,
                                TransDate = serverDate,
                                Quantity = -issueDetail.CommitQuantity,
                                UserName = SystemProperty.UserName,
                                TranTypeID = tranTypeId,
                                BinCommitQuantity = sourceBin.CommitQuantity,
                                BinOHQuantity = sourceBin.OHQuantity,
                                IssuePurposeID = issueMaster.IssuePurposeID,
                                LocationCommitQuantity = sourceLocation.CommitQuantity,
                                LocationOHQuantity = sourceLocation.OHQuantity,
                                MasLocCommitQuantity = sourceMasLocation.CommitQuantity,
                                MasLocOHQuantity = sourceMasLocation.OHQuantity
                            };
                            db.MST_TransactionHistories.InsertOnSubmit(sourceHistory);

                            #endregion

                            #endregion

                            #region add to destination cache

                            #region bin cache

                            var destBin = db.IV_BinCaches.FirstOrDefault(e => e.LocationID == issueMaster.ToLocationID && e.BinID == issueMaster.ToBinID && e.ProductID == issueDetail.ProductID);
                            if (destBin != null)
                            {
                                destBin.OHQuantity = destBin.OHQuantity.GetValueOrDefault(0) + issueDetail.CommitQuantity;
                            }
                            else
                            {
                                // create new record
                                destBin = new IV_BinCache
                                {
                                    BinID = issueMaster.ToBinID.GetValueOrDefault(0),
                                    CCNID = issueMaster.CCNID,
                                    LocationID = issueMaster.ToLocationID.GetValueOrDefault(0),
                                    MasterLocationID = issueMaster.MasterLocationID,
                                    ProductID = issueDetail.ProductID,
                                    OHQuantity = issueDetail.CommitQuantity
                                };
                                db.IV_BinCaches.InsertOnSubmit(destBin);
                            }

                            #endregion

                            #region location cache

                            var destLocation = db.IV_LocationCaches.FirstOrDefault(e => e.MasterLocationID == issueMaster.MasterLocationID && e.LocationID == issueMaster.ToLocationID && e.ProductID == issueDetail.ProductID);
                            if (destLocation != null)
                            {
                                destLocation.OHQuantity = destLocation.OHQuantity.GetValueOrDefault(0) + issueDetail.CommitQuantity;
                            }
                            else
                            {
                                // create new record
                                destLocation = new IV_LocationCache
                                {
                                    CCNID = issueMaster.CCNID,
                                    LocationID = issueMaster.ToLocationID.GetValueOrDefault(0),
                                    MasterLocationID = issueMaster.MasterLocationID,
                                    ProductID = issueDetail.ProductID,
                                    OHQuantity = issueDetail.CommitQuantity
                                };
                                db.IV_LocationCaches.InsertOnSubmit(destLocation);
                            }

                            #endregion

                            #region master location cache

                            var destMasLocation = db.IV_MasLocCaches.FirstOrDefault(e => e.MasterLocationID == issueMaster.MasterLocationID && e.ProductID == issueDetail.ProductID);
                            if (destMasLocation != null)
                            {
                                destMasLocation.OHQuantity = destMasLocation.OHQuantity.GetValueOrDefault(0) + issueDetail.CommitQuantity;
                            }
                            else
                            {
                                // create new record
                                destMasLocation = new IV_MasLocCache
                                {
                                    CCNID = issueMaster.CCNID,
                                    MasterLocationID = issueMaster.MasterLocationID,
                                    ProductID = issueDetail.ProductID,
                                    OHQuantity = issueDetail.CommitQuantity
                                };
                                db.IV_MasLocCaches.InsertOnSubmit(destMasLocation);
                            }

                            #endregion

                            #region Transaction history

                            var destHistory = new MST_TransactionHistory
                            {
                                CCNID = issueMaster.CCNID,
                                StockUMID = issueDetail.StockUMID,
                                MasterLocationID = issueMaster.MasterLocationID,
                                ProductID = issueDetail.ProductID,
                                LocationID = issueMaster.ToLocationID,
                                BinID = issueMaster.ToBinID,
                                RefMasterID = issueMaster.IssueMaterialMasterID,
                                RefDetailID = issueDetail.IssueMaterialDetailID,
                                PostDate = issueMaster.PostDate,
                                TransDate = serverDate,
                                Quantity = issueDetail.CommitQuantity,
                                UserName = SystemProperty.UserName,
                                TranTypeID = tranTypeId,
                                BinCommitQuantity = destBin.CommitQuantity,
                                BinOHQuantity = destBin.OHQuantity,
                                IssuePurposeID = issueMaster.IssuePurposeID,
                                LocationCommitQuantity = destLocation.CommitQuantity,
                                LocationOHQuantity = destLocation.OHQuantity,
                                MasLocCommitQuantity = destMasLocation.CommitQuantity,
                                MasLocOHQuantity = destMasLocation.OHQuantity
                            };
                            db.MST_TransactionHistories.InsertOnSubmit(destHistory);

                            #endregion

                            #endregion

                            db.SubmitChanges();
                        }
                        
                        #endregion

                        db.SubmitChanges();
                        trans.Complete();
                        return issueMaster.IssueMaterialMasterID;
                    }
                }
            }
            catch (PCSBOException ex)
            {
                if (ex.mCode == ErrorCode.SQLDUPLICATE_KEYCODE)
                    throw new PCSDBException(ErrorCode.DUPLICATE_KEY, methodName, ex);
                if (ex.mCode == ErrorCode.MESSAGE_NOT_ENOUGH_COMPONENT_TO_COMPLETE)
                    throw;
                throw new PCSDBException(ErrorCode.ERROR_DB, methodName, ex);
            }
		}

	    private static PRO_IssueMaterialDetail CreateIssueDetail(DataRow dr)
	    {
	        var objDetail = new PRO_IssueMaterialDetail();
	        if (dr[PRO_IssueMaterialDetailTable.LINE_FLD] != DBNull.Value)
	        {
	            objDetail.Line = Convert.ToInt32(dr[PRO_IssueMaterialDetailTable.LINE_FLD]);
	        }
	        if (dr[PRO_IssueMaterialDetailTable.COMMITQUANTITY_FLD] != DBNull.Value)
	        {
	            objDetail.CommitQuantity = Convert.ToDecimal(dr[PRO_IssueMaterialDetailTable.COMMITQUANTITY_FLD]);
	        }
	        if (dr[PRO_IssueMaterialDetailTable.PRODUCTID_FLD] != DBNull.Value)
	        {
	            objDetail.ProductID = Convert.ToInt32(dr[PRO_IssueMaterialDetailTable.PRODUCTID_FLD]);
	        }
	        if (dr[PRO_IssueMaterialDetailTable.LOCATIONID_FLD] != DBNull.Value)
	        {
	            objDetail.LocationID = Convert.ToInt32(dr[PRO_IssueMaterialDetailTable.LOCATIONID_FLD]);
	        }
	        if (dr[PRO_IssueMaterialDetailTable.BINID_FLD] != DBNull.Value)
	        {
	            objDetail.BinID = Convert.ToInt32(dr[PRO_IssueMaterialDetailTable.BINID_FLD]);
	        }
	        objDetail.Lot = dr[PRO_IssueMaterialDetailTable.LOT_FLD].ToString();
	        objDetail.Serial = dr[PRO_IssueMaterialDetailTable.SERIAL_FLD].ToString();
	        if (dr[PRO_IssueMaterialDetailTable.MASTERLOCATIONID_FLD] != DBNull.Value)
	        {
	            objDetail.MasterLocationID = Convert.ToInt32(dr[PRO_IssueMaterialDetailTable.MASTERLOCATIONID_FLD]);
	        }
	        if (dr[PRO_IssueMaterialDetailTable.STOCKUMID_FLD] != DBNull.Value)
	        {
	            objDetail.StockUMID = Convert.ToInt32(dr[PRO_IssueMaterialDetailTable.STOCKUMID_FLD]);
	        }
	        if (dr[PRO_IssueMaterialDetailTable.QASTATUS_FLD] != DBNull.Value)
	        {
	            objDetail.QAStatus = Convert.ToByte(dr[PRO_IssueMaterialDetailTable.QASTATUS_FLD]);
	        }
	        if (dr[PRO_IssueMaterialDetailTable.WORKORDERMASTERID_FLD] != DBNull.Value)
	        {
	            objDetail.WorkOrderMasterID = Convert.ToInt32(dr[PRO_IssueMaterialDetailTable.WORKORDERMASTERID_FLD]);
	        }
	        if (dr[PRO_IssueMaterialDetailTable.AVAILABLEQUANTITY_FLD] != DBNull.Value)
	        {
	            objDetail.AvailableQuantity = Convert.ToDecimal(dr[PRO_IssueMaterialDetailTable.AVAILABLEQUANTITY_FLD]);
	        }
	        if (dr[PRO_IssueMaterialDetailTable.WORKORDERDETAILID_FLD] != DBNull.Value)
	        {
	            objDetail.WorkOrderDetailID = Convert.ToInt32(dr[PRO_IssueMaterialDetailTable.WORKORDERDETAILID_FLD]);
	        }
	        if (dr[PRO_IssueMaterialDetailTable.BOMQUANTITY_FLD] != DBNull.Value)
	        {
	            objDetail.BomQuantity = Convert.ToDecimal(dr[PRO_IssueMaterialDetailTable.BOMQUANTITY_FLD]);
	        }
	        return objDetail;
	    }

		/// <summary>
		/// This method is used to get all the detail of the issue material
		/// Based on the Master ID of the table Issue Master Material
		/// </summary>
		/// <param name="pintMasterId"></param>
		/// <returns></returns>
		public DataSet GetDetailData(int pintMasterId)
		{
			var objProIssueMaterialDetailDS = new PRO_IssueMaterialDetailDS();
			DataSet dstDetailData = objProIssueMaterialDetailDS.GetDetailData(pintMasterId);
			return dstDetailData;
		}

		/// <summary>
		/// 
		/// </summary>		
		/// <param name="pstrLocationCode"></param>
		/// <returns>MST_LocationVO</returns>
		public string GetLocationNameByLocationCode(string pstrLocationCode)
		{
			var objDS = new MST_LocationDS();
			return objDS.GetNameFromCode(pstrLocationCode);
		}

		#endregion

	    /// <summary>
	    /// Delete transaction
	    /// </summary>
	    /// <param name="issueMasterId"></param>
        public void DeleteTransaction(int issueMasterId)
		{
            const string methodName = This + ".DeleteTransaction()";
            try
            {
                using (var trans = new TransactionScope())
                {
                    using (var db = new PCSDataContext(Utils.Instance.ConnectionString))
                    {
                        var tranTypeId = db.MST_TranTypes.FirstOrDefault(t => t.Code == TransactionTypeEnum.PROIssueMaterial.ToString()).TranTypeID;
                        var deleteTranTypeId = db.MST_TranTypes.FirstOrDefault(t => t.Code == TransactionTypeEnum.DeleteTransaction.ToString()).TranTypeID;
                        var serverDate = db.GetServerDate();

                        #region PRO_IssueMaterialMasters

                        var issueMaster = db.PRO_IssueMaterialMasters.FirstOrDefault(e => e.IssueMaterialMasterID == issueMasterId);
                        if (issueMaster == null)
                            return;

                        #endregion

                        #region old transaction history to be updated

                        var oldSourceHistory = db.MST_TransactionHistories.Where(e => e.RefMasterID == issueMasterId && e.TranTypeID == tranTypeId);
                        foreach (var history in oldSourceHistory)
                        {
                            // mark as delete transaction
                            history.TranTypeID = deleteTranTypeId;
                            history.UserName = SystemProperty.UserName;
                        }

                        #endregion

                        #region cache data

                        foreach (var issueDetail in issueMaster.PRO_IssueMaterialDetails)
                        {
                            var allowNegative = issueDetail.ITM_Product.AllowNegativeQty.GetValueOrDefault(false);

                            #region subtract from destionation cache

                            #region bin cache

                            var destBin = db.IV_BinCaches.FirstOrDefault(e => e.LocationID == issueMaster.ToLocationID && e.BinID == issueMaster.ToBinID && e.ProductID == issueDetail.ProductID);
                            if (!allowNegative && (destBin == null || destBin.OHQuantity.GetValueOrDefault(0) < issueDetail.CommitQuantity))
                            {
                                var productError = new Hashtable
                                                       {
                                                           { ITM_ProductTable.PRODUCTID_FLD, issueDetail.ProductID },
                                                           { IV_BinCacheTable.OHQUANTITY_FLD, destBin == null ? 0 : destBin.OHQuantity.GetValueOrDefault(0) }
                                                       };
                                throw new PCSBOException(ErrorCode.MESSAGE_NOT_ENOUGH_COMPONENT_TO_COMPLETE, issueDetail.ITM_Product.Code, new Exception(), productError);
                            }
                            if (destBin != null)
                            {
                                destBin.OHQuantity = destBin.OHQuantity.GetValueOrDefault(0) - issueDetail.CommitQuantity;
                            }
                            else
                            {
                                // create new record
                                destBin = new IV_BinCache
                                {
                                    BinID = issueMaster.ToBinID.GetValueOrDefault(0),
                                    CCNID = issueMaster.CCNID,
                                    LocationID = issueMaster.ToLocationID.GetValueOrDefault(0),
                                    MasterLocationID = issueMaster.MasterLocationID,
                                    ProductID = issueDetail.ProductID,
                                    OHQuantity = -issueDetail.CommitQuantity
                                };
                                db.IV_BinCaches.InsertOnSubmit(destBin);
                            }

                            #endregion

                            #region location cache

                            var destLocation = db.IV_LocationCaches.FirstOrDefault(e => e.MasterLocationID == issueMaster.MasterLocationID && e.LocationID == issueMaster.ToLocationID && e.ProductID == issueDetail.ProductID);
                            if (!allowNegative && (destLocation == null || destLocation.OHQuantity.GetValueOrDefault(0) < issueDetail.CommitQuantity))
                            {
                                var productError = new Hashtable
                                                       {
                                                           { ITM_ProductTable.PRODUCTID_FLD, issueDetail.ProductID },
                                                           { IV_BinCacheTable.OHQUANTITY_FLD, destLocation == null ? 0 : destLocation.OHQuantity.GetValueOrDefault(0) }
                                                       };
                                throw new PCSBOException(ErrorCode.MESSAGE_NOT_ENOUGH_COMPONENT_TO_COMPLETE, issueDetail.ITM_Product.Code, new Exception(), productError);
                            }
                            if (destLocation != null)
                            {
                                destLocation.OHQuantity = destLocation.OHQuantity.GetValueOrDefault(0) - issueDetail.CommitQuantity;
                            }
                            else
                            {
                                // create new record
                                destLocation = new IV_LocationCache
                                {
                                    CCNID = issueMaster.CCNID,
                                    LocationID = issueMaster.ToLocationID.GetValueOrDefault(0),
                                    MasterLocationID = issueMaster.MasterLocationID,
                                    ProductID = issueDetail.ProductID,
                                    OHQuantity = -issueDetail.CommitQuantity
                                };
                                db.IV_LocationCaches.InsertOnSubmit(destLocation);
                            }

                            #endregion

                            #region master location cache

                            var destMasLocation = db.IV_MasLocCaches.FirstOrDefault(e => e.MasterLocationID == issueMaster.MasterLocationID && e.ProductID == issueDetail.ProductID);
                            if (!allowNegative && (destMasLocation == null || destMasLocation.OHQuantity.GetValueOrDefault(0) < issueDetail.CommitQuantity))
                            {
                                var productError = new Hashtable
                                                       {
                                                           { ITM_ProductTable.PRODUCTID_FLD, issueDetail.ProductID },
                                                           { IV_BinCacheTable.OHQUANTITY_FLD, destMasLocation == null ? 0 : destMasLocation.OHQuantity.GetValueOrDefault(0) }
                                                       };
                                throw new PCSBOException(ErrorCode.MESSAGE_NOT_ENOUGH_COMPONENT_TO_COMPLETE, issueDetail.ITM_Product.Code, new Exception(), productError);
                            }
                            if (destMasLocation != null)
                            {
                                destMasLocation.OHQuantity = destMasLocation.OHQuantity.GetValueOrDefault(0) - issueDetail.CommitQuantity;
                            }
                            else
                            {
                                // create new record
                                destMasLocation = new IV_MasLocCache
                                {
                                    CCNID = issueMaster.CCNID,
                                    MasterLocationID = issueMaster.MasterLocationID,
                                    ProductID = issueDetail.ProductID,
                                    OHQuantity = -issueDetail.CommitQuantity
                                };
                                db.IV_MasLocCaches.InsertOnSubmit(destMasLocation);
                            }

                            #endregion

                            #region Transaction history

                            var destHistory = new MST_TransactionHistory
                            {
                                CCNID = issueMaster.CCNID,
                                StockUMID = issueDetail.StockUMID,
                                MasterLocationID = issueMaster.MasterLocationID,
                                ProductID = issueDetail.ProductID,
                                LocationID = issueMaster.ToLocationID,
                                BinID = issueMaster.ToBinID,
                                RefMasterID = issueMaster.IssueMaterialMasterID,
                                RefDetailID = issueDetail.IssueMaterialDetailID,
                                PostDate = issueMaster.PostDate,
                                TransDate = serverDate,
                                Quantity = -issueDetail.CommitQuantity,
                                UserName = SystemProperty.UserName,
                                TranTypeID = tranTypeId,
                                BinCommitQuantity = destBin.CommitQuantity,
                                BinOHQuantity = destBin.OHQuantity,
                                IssuePurposeID = issueMaster.IssuePurposeID,
                                LocationCommitQuantity = destLocation.CommitQuantity,
                                LocationOHQuantity = destLocation.OHQuantity,
                                MasLocCommitQuantity = destMasLocation.CommitQuantity,
                                MasLocOHQuantity = destMasLocation.OHQuantity
                            };
                            db.MST_TransactionHistories.InsertOnSubmit(destHistory);

                            #endregion

                            #endregion

                            #region add to source cache

                            #region bin cache

                            var sourceBin = db.IV_BinCaches.FirstOrDefault(e => e.LocationID == issueDetail.LocationID && e.BinID == issueDetail.BinID && e.ProductID == issueDetail.ProductID);
                            if (sourceBin != null)
                            {
                                sourceBin.OHQuantity = sourceBin.OHQuantity.GetValueOrDefault(0) + issueDetail.CommitQuantity;
                            }
                            else
                            {
                                // create new record
                                sourceBin = new IV_BinCache
                                {
                                    BinID = issueDetail.BinID.GetValueOrDefault(0),
                                    CCNID = issueMaster.CCNID,
                                    LocationID = issueDetail.LocationID,
                                    MasterLocationID = issueMaster.MasterLocationID,
                                    ProductID = issueDetail.ProductID,
                                    OHQuantity = issueDetail.CommitQuantity
                                };
                                db.IV_BinCaches.InsertOnSubmit(sourceBin);
                            }

                            #endregion

                            #region location cache

                            var sourceLocation = db.IV_LocationCaches.FirstOrDefault(e => e.MasterLocationID == issueMaster.MasterLocationID && e.LocationID == issueDetail.LocationID && e.ProductID == issueDetail.ProductID);
                            if (sourceLocation != null)
                            {
                                sourceLocation.OHQuantity = sourceLocation.OHQuantity.GetValueOrDefault(0) + issueDetail.CommitQuantity;
                            }
                            else
                            {
                                // create new record
                                sourceLocation = new IV_LocationCache
                                {
                                    CCNID = issueMaster.CCNID,
                                    LocationID = issueDetail.LocationID,
                                    MasterLocationID = issueMaster.MasterLocationID,
                                    ProductID = issueDetail.ProductID,
                                    OHQuantity = issueDetail.CommitQuantity
                                };
                                db.IV_LocationCaches.InsertOnSubmit(sourceLocation);
                            }

                            #endregion

                            #region master location cache

                            var sourceMasLocation = db.IV_MasLocCaches.FirstOrDefault(e => e.MasterLocationID == issueMaster.MasterLocationID && e.ProductID == issueDetail.ProductID);
                            if (sourceMasLocation != null)
                            {
                                sourceMasLocation.OHQuantity = sourceMasLocation.OHQuantity.GetValueOrDefault(0) + issueDetail.CommitQuantity;
                            }
                            else
                            {
                                // create new record
                                sourceMasLocation = new IV_MasLocCache
                                {
                                    CCNID = issueMaster.CCNID,
                                    MasterLocationID = issueMaster.MasterLocationID,
                                    ProductID = issueDetail.ProductID,
                                    OHQuantity = issueDetail.CommitQuantity
                                };
                                db.IV_MasLocCaches.InsertOnSubmit(sourceMasLocation);
                            }

                            #endregion

                            #region Transaction history

                            var sourceHistory = new MST_TransactionHistory
                            {
                                CCNID = issueMaster.CCNID,
                                StockUMID = issueDetail.StockUMID,
                                MasterLocationID = issueMaster.MasterLocationID,
                                ProductID = issueDetail.ProductID,
                                LocationID = issueDetail.LocationID,
                                BinID = issueDetail.BinID,
                                RefMasterID = issueMaster.IssueMaterialMasterID,
                                RefDetailID = issueDetail.IssueMaterialDetailID,
                                PostDate = issueMaster.PostDate,
                                TransDate = serverDate,
                                Quantity = issueDetail.CommitQuantity,
                                UserName = SystemProperty.UserName,
                                TranTypeID = tranTypeId,
                                BinCommitQuantity = sourceBin.CommitQuantity,
                                BinOHQuantity = sourceBin.OHQuantity,
                                IssuePurposeID = issueMaster.IssuePurposeID,
                                LocationCommitQuantity = sourceLocation.CommitQuantity,
                                LocationOHQuantity = sourceLocation.OHQuantity,
                                MasLocCommitQuantity = sourceMasLocation.CommitQuantity,
                                MasLocOHQuantity = sourceMasLocation.OHQuantity
                            };
                            db.MST_TransactionHistories.InsertOnSubmit(sourceHistory);

                            #endregion

                            #endregion

                            db.PRO_IssueMaterialDetails.DeleteOnSubmit(issueDetail);
                        }
                        #endregion

                        db.PRO_IssueMaterialMasters.DeleteOnSubmit(issueMaster);
                        db.SubmitChanges();
                        trans.Complete();
                    }
                }
            }
            catch (PCSBOException ex)
            {
                if (ex.mCode == ErrorCode.SQLDUPLICATE_KEYCODE)
                    throw new PCSDBException(ErrorCode.DUPLICATE_KEY, methodName, ex);
                if (ex.mCode == ErrorCode.MESSAGE_NOT_ENOUGH_COMPONENT_TO_COMPLETE)
                    throw;
                throw new PCSDBException(ErrorCode.ERROR_DB, methodName, ex);
            }
		}
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="pintIssueMasterId"></param>
		/// <returns></returns>
		public DataTable GetMasterIssue(int pintIssueMasterId)
		{
			return (new PRO_IssueMaterialMasterDS()).GetMasterIssue(pintIssueMasterId);
		}
	}
}
