using System;
using System.Collections;
using System.Data;
using PCSComMaterials.Inventory.DS;
using PCSComUtils.Common;
using PCSComUtils.PCSExc;
using System.Transactions;
using PCSComUtils.DataContext;
using PCSComUtils.DataAccess;
using System.Linq;
namespace PCSComMaterials.Inventory.BO
{
	public class IVMiscellaneousIssueBO
	{
        private const string This = "PCSComMaterials.Inventory.BO.IVMiscellaneousIssueBO";

        /// <summary>
        /// Adds the and return id.
        /// </summary>
        /// <param name="pobjMaster">The pobj master.</param>
        /// <param name="pdstData">The PDST data.</param>
        /// <param name="serverDate">The server date.</param>
        /// <returns></returns>
        public int AddAndReturnId(object pobjMaster, DataSet pdstData, DateTime serverDate)
		{
            const string methodName = This + ".AddAndReturnId()";
            try
            {
                using (var trans = new TransactionScope())
                {
                    using (var db = new PCSDataContext(Utils.Instance.ConnectionString))
                    {
                        var miscTranTypeId = db.MST_TranTypes.FirstOrDefault(t => t.Code == TransactionTypeEnum.IVMiscellaneousIssue.ToString()).TranTypeID;

                        #region IV_MiscellaneousIssueMaster

                        var issueMaster = new IV_MiscellaneousIssueMaster();
                        var pobjMasterVO = (IV_MiscellaneousIssueMasterVO)pobjMaster;
                        issueMaster.CCNID = pobjMasterVO.CCNID;
                        issueMaster.Comment = pobjMasterVO.Comment;
                        if (pobjMasterVO.DesBinID > 0)
                        {
                            issueMaster.DesBinID = pobjMasterVO.DesBinID;
                        }
                        if (pobjMasterVO.DesLocationID > 0)
                        {
                            issueMaster.DesLocationID = pobjMasterVO.DesLocationID;
                        }
                        if (pobjMasterVO.DesMasLocationID > 0)
                        {
                            issueMaster.DesMasLocationID = pobjMasterVO.DesMasLocationID;
                        }

                        issueMaster.IssuePurposeID = pobjMasterVO.IssuePurposeID;

                        issueMaster.PartyID = null;
                        issueMaster.PostDate = pobjMasterVO.PostDate;

                        issueMaster.SourceBinID = pobjMasterVO.SourceBinID;
                        issueMaster.SourceLocationID = pobjMasterVO.SourceLocationID;
                        issueMaster.SourceMasLocationID = pobjMasterVO.SourceMasLocationID;

                        issueMaster.TransNo = pobjMasterVO.TransNo;
                        issueMaster.UserName = SystemProperty.UserName;
                        issueMaster.LastChange = serverDate;
                        
                        #endregion

                        #region Insert IV_MiscellaneousIssueDetail
                        
                        foreach (DataRow dr in pdstData.Tables[0].Rows)
                        {
                            if (dr.RowState == DataRowState.Deleted)
                                continue;
                            var objDetail = new IV_MiscellaneousIssueDetail
                                                {
                                                    ProductID = Convert.ToInt32(dr[IV_MiscellaneousIssueDetailTable.PRODUCTID_FLD]),
                                                    Quantity = Convert.ToDecimal(dr[IV_MiscellaneousIssueDetailTable.QUANTITY_FLD]),
                                                    StockUMID = Convert.ToInt32(dr[IV_MiscellaneousIssueDetailTable.STOCKUMID_FLD])
                                                };
                            if (dr[IV_MiscellaneousIssueDetailTable.LOT_FLD] != DBNull.Value)
                            {
                                objDetail.Lot = dr[IV_MiscellaneousIssueDetailTable.LOT_FLD].ToString();
                            }
                            if (dr[IV_MiscellaneousIssueDetailTable.AVAILABLEQTY_FLD] != DBNull.Value)
                            {
                                objDetail.AvailableQty = Convert.ToDecimal(dr[IV_MiscellaneousIssueDetailTable.AVAILABLEQTY_FLD]);
                            }
                            if (dr[IV_MiscellaneousIssueDetailTable.DEPARTMENTID_FLD] != DBNull.Value)
                            {
                                int departmentId;
                                int.TryParse(dr[IV_MiscellaneousIssueDetailTable.DEPARTMENTID_FLD].ToString(), out departmentId);
                                if (departmentId > 0)
                                {
                                    objDetail.DepartmentID = departmentId;
                                }
                            }
                            if (dr[IV_MiscellaneousIssueDetailTable.REASONID_FLD] != DBNull.Value
                                && dr[IV_MiscellaneousIssueDetailTable.REASONID_FLD] != null)
                            {
                                int reasonId;
                                int.TryParse(dr[IV_MiscellaneousIssueDetailTable.REASONID_FLD].ToString(), out reasonId);
                                if (reasonId > 0)
                                {
                                    objDetail.ReasonID = reasonId;
                                }
                            }
                            issueMaster.IV_MiscellaneousIssueDetails.Add(objDetail);
                        }
                        // temporary save master and detail to database
                        db.IV_MiscellaneousIssueMasters.InsertOnSubmit(issueMaster);
                        db.SubmitChanges();

                        #endregion

                        #region cache data, only update onhand if not Scrap issue (destroy)

                        if (issueMaster.IssuePurposeID != (int) PurposeEnum.Scrap)
                        {
                            foreach (var issueDetail in issueMaster.IV_MiscellaneousIssueDetails)
                            {
                                var allowNegative = issueDetail.ITM_Product.AllowNegativeQty.GetValueOrDefault(false);

                                #region subtract from source cache

                                #region bin cache

                                var sourceBin = db.IV_BinCaches.FirstOrDefault(e => e.LocationID == issueMaster.SourceLocationID && e.BinID == issueMaster.SourceBinID && e.ProductID == issueDetail.ProductID);
                                if (!allowNegative && (sourceBin == null || sourceBin.OHQuantity.GetValueOrDefault(0) < issueDetail.Quantity))
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
                                    sourceBin.OHQuantity = sourceBin.OHQuantity.GetValueOrDefault(0) - issueDetail.Quantity;
                                }
                                else
                                {
                                    // create new record
                                    sourceBin = new IV_BinCache
                                    {
                                        BinID = issueMaster.SourceBinID.GetValueOrDefault(0),
                                        CCNID = issueMaster.CCNID,
                                        LocationID = issueMaster.SourceLocationID,
                                        MasterLocationID = issueMaster.SourceMasLocationID,
                                        ProductID = issueDetail.ProductID,
                                        OHQuantity = -issueDetail.Quantity
                                    };
                                    db.IV_BinCaches.InsertOnSubmit(sourceBin);
                                }

                                #endregion

                                #region location cache

                                var sourceLocation = db.IV_LocationCaches.FirstOrDefault(e => e.MasterLocationID == issueMaster.SourceMasLocationID && e.LocationID == issueMaster.SourceLocationID && e.ProductID == issueDetail.ProductID);
                                if (!allowNegative && (sourceLocation == null || sourceLocation.OHQuantity.GetValueOrDefault(0) < issueDetail.Quantity))
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
                                    sourceLocation.OHQuantity = sourceLocation.OHQuantity.GetValueOrDefault(0) - issueDetail.Quantity;
                                }
                                else
                                {
                                    // create new record
                                    sourceLocation = new IV_LocationCache
                                    {
                                        CCNID = issueMaster.CCNID,
                                        LocationID = issueMaster.SourceLocationID,
                                        MasterLocationID = issueMaster.SourceMasLocationID,
                                        ProductID = issueDetail.ProductID,
                                        OHQuantity = -issueDetail.Quantity
                                    };
                                    db.IV_LocationCaches.InsertOnSubmit(sourceLocation);
                                }

                                #endregion

                                #region master location cache

                                var sourceMasLocation = db.IV_MasLocCaches.FirstOrDefault(e => e.MasterLocationID == issueMaster.SourceMasLocationID && e.ProductID == issueDetail.ProductID);
                                if (!allowNegative && (sourceMasLocation == null || sourceMasLocation.OHQuantity.GetValueOrDefault(0) < issueDetail.Quantity))
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
                                    sourceMasLocation.OHQuantity = sourceMasLocation.OHQuantity.GetValueOrDefault(0) - issueDetail.Quantity;
                                }
                                else
                                {
                                    // create new record
                                    sourceMasLocation = new IV_MasLocCache
                                    {
                                        CCNID = issueMaster.CCNID,
                                        MasterLocationID = issueMaster.SourceMasLocationID,
                                        ProductID = issueDetail.ProductID,
                                        OHQuantity = -issueDetail.Quantity
                                    };
                                    db.IV_MasLocCaches.InsertOnSubmit(sourceMasLocation);
                                }

                                #endregion

                                #region Transaction history

                                var sourceHistory = new MST_TransactionHistory
                                {
                                    CCNID = pobjMasterVO.CCNID,
                                    StockUMID = issueDetail.StockUMID,
                                    MasterLocationID = pobjMasterVO.SourceMasLocationID,
                                    ProductID = issueDetail.ProductID,
                                    LocationID = pobjMasterVO.SourceLocationID,
                                    BinID = pobjMasterVO.SourceBinID,
                                    RefMasterID = issueMaster.MiscellaneousIssueMasterID,
                                    RefDetailID = issueDetail.MiscellaneousIssueDetailID,
                                    PostDate = pobjMasterVO.PostDate,
                                    TransDate = serverDate,
                                    Quantity = -issueDetail.Quantity,
                                    UserName = SystemProperty.UserName,
                                    TranTypeID = miscTranTypeId,
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

                                if (issueMaster.DesMasLocationID.GetValueOrDefault(0) > 0)
                                {
                                    #region bin cache

                                    var destBin = db.IV_BinCaches.FirstOrDefault(e => e.LocationID == issueMaster.DesLocationID && e.BinID == issueMaster.DesBinID && e.ProductID == issueDetail.ProductID);
                                    if (destBin != null)
                                    {
                                        destBin.OHQuantity = destBin.OHQuantity.GetValueOrDefault(0) + issueDetail.Quantity;
                                    }
                                    else
                                    {
                                        // create new record
                                        destBin = new IV_BinCache
                                        {
                                            BinID = issueMaster.DesBinID.GetValueOrDefault(0),
                                            CCNID = issueMaster.CCNID,
                                            LocationID = issueMaster.DesLocationID.GetValueOrDefault(0),
                                            MasterLocationID = issueMaster.DesMasLocationID.GetValueOrDefault(0),
                                            ProductID = issueDetail.ProductID,
                                            OHQuantity = issueDetail.Quantity
                                        };
                                        db.IV_BinCaches.InsertOnSubmit(destBin);
                                    }

                                    #endregion

                                    #region location cache

                                    var destLocation = db.IV_LocationCaches.FirstOrDefault(e => e.MasterLocationID == issueMaster.DesMasLocationID && e.LocationID == issueMaster.DesLocationID && e.ProductID == issueDetail.ProductID);
                                    if (destLocation != null)
                                    {
                                        destLocation.OHQuantity = destLocation.OHQuantity.GetValueOrDefault(0) + issueDetail.Quantity;
                                    }
                                    else
                                    {
                                        // create new record
                                        destLocation = new IV_LocationCache
                                        {
                                            CCNID = issueMaster.CCNID,
                                            LocationID = issueMaster.DesLocationID.GetValueOrDefault(0),
                                            MasterLocationID = issueMaster.DesMasLocationID.GetValueOrDefault(0),
                                            ProductID = issueDetail.ProductID,
                                            OHQuantity = issueDetail.Quantity
                                        };
                                        db.IV_LocationCaches.InsertOnSubmit(destLocation);
                                    }

                                    #endregion

                                    #region master location cache

                                    var destMasLocation = db.IV_MasLocCaches.FirstOrDefault(e => e.MasterLocationID == issueMaster.DesMasLocationID && e.ProductID == issueDetail.ProductID);
                                    if (destMasLocation != null)
                                    {
                                        destMasLocation.OHQuantity = destMasLocation.OHQuantity.GetValueOrDefault(0) + issueDetail.Quantity;
                                    }
                                    else
                                    {
                                        // create new record
                                        destMasLocation = new IV_MasLocCache
                                        {
                                            CCNID = issueMaster.CCNID,
                                            MasterLocationID = issueMaster.DesMasLocationID.GetValueOrDefault(0),
                                            ProductID = issueDetail.ProductID,
                                            OHQuantity = issueDetail.Quantity
                                        };
                                        db.IV_MasLocCaches.InsertOnSubmit(destMasLocation);
                                    }

                                    #endregion

                                    #region Transaction history

                                    var destHistory = new MST_TransactionHistory
                                    {
                                        CCNID = pobjMasterVO.CCNID,
                                        StockUMID = issueDetail.StockUMID,
                                        MasterLocationID = pobjMasterVO.DesMasLocationID,
                                        ProductID = issueDetail.ProductID,
                                        LocationID = pobjMasterVO.DesLocationID,
                                        BinID = pobjMasterVO.DesBinID,
                                        RefMasterID = issueMaster.MiscellaneousIssueMasterID,
                                        RefDetailID = issueDetail.MiscellaneousIssueDetailID,
                                        PostDate = pobjMasterVO.PostDate,
                                        TransDate = serverDate,
                                        Quantity = issueDetail.Quantity,
                                        UserName = SystemProperty.UserName,
                                        TranTypeID = miscTranTypeId,
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
                                }

                                #endregion
                            }
                        }

                        #endregion

                        db.SubmitChanges();
                        trans.Complete();
                        return issueMaster.MiscellaneousIssueMasterID;
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

        public void UpdateCache(int issueMasterId, DateTime serverDate)
        {
            const string methodName = This + ".UpdateCache()";
            try
            {
                using (var trans = new TransactionScope())
                {
                    using (var db = new PCSDataContext(Utils.Instance.ConnectionString))
                    {
                        var miscTranTypeId = db.MST_TranTypes.FirstOrDefault(t => t.Code == TransactionTypeEnum.IVMiscellaneousIssue.ToString()).TranTypeID;

                        var issueMaster = db.IV_MiscellaneousIssueMasters.FirstOrDefault(m => m.MiscellaneousIssueMasterID == issueMasterId);
                        if (issueMaster == null)
                        {
                            return;
                        }

                        #region cache data, only update onhand if destroy transaction and not approved

                        if (issueMaster.IssuePurposeID == (int)PurposeEnum.Scrap && issueMaster.DestroyApproved != 1)
                        {
                            foreach (var issueDetail in issueMaster.IV_MiscellaneousIssueDetails)
                            {
                                var allowNegative = issueDetail.ITM_Product.AllowNegativeQty.GetValueOrDefault(false);

                                #region subtract from source cache

                                #region bin cache

                                var sourceBin = db.IV_BinCaches.FirstOrDefault(e => e.LocationID == issueMaster.SourceLocationID && e.BinID == issueMaster.SourceBinID && e.ProductID == issueDetail.ProductID);
                                if (!allowNegative && (sourceBin == null || sourceBin.OHQuantity.GetValueOrDefault(0) < issueDetail.Quantity))
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
                                    sourceBin.OHQuantity = sourceBin.OHQuantity.GetValueOrDefault(0) - issueDetail.Quantity;
                                }
                                else
                                {
                                    // create new record
                                    sourceBin = new IV_BinCache
                                    {
                                        BinID = issueMaster.SourceBinID.GetValueOrDefault(0),
                                        CCNID = issueMaster.CCNID,
                                        LocationID = issueMaster.SourceLocationID,
                                        MasterLocationID = issueMaster.SourceMasLocationID,
                                        ProductID = issueDetail.ProductID,
                                        OHQuantity = -issueDetail.Quantity
                                    };
                                    db.IV_BinCaches.InsertOnSubmit(sourceBin);
                                }

                                #endregion

                                #region location cache

                                var sourceLocation = db.IV_LocationCaches.FirstOrDefault(e => e.MasterLocationID == issueMaster.SourceMasLocationID && e.LocationID == issueMaster.SourceLocationID && e.ProductID == issueDetail.ProductID);
                                if (!allowNegative && (sourceLocation == null || sourceLocation.OHQuantity.GetValueOrDefault(0) < issueDetail.Quantity))
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
                                    sourceLocation.OHQuantity = sourceLocation.OHQuantity.GetValueOrDefault(0) - issueDetail.Quantity;
                                }
                                else
                                {
                                    // create new record
                                    sourceLocation = new IV_LocationCache
                                    {
                                        CCNID = issueMaster.CCNID,
                                        LocationID = issueMaster.SourceLocationID,
                                        MasterLocationID = issueMaster.SourceMasLocationID,
                                        ProductID = issueDetail.ProductID,
                                        OHQuantity = -issueDetail.Quantity
                                    };
                                    db.IV_LocationCaches.InsertOnSubmit(sourceLocation);
                                }

                                #endregion

                                #region master location cache

                                var sourceMasLocation = db.IV_MasLocCaches.FirstOrDefault(e => e.MasterLocationID == issueMaster.SourceMasLocationID && e.ProductID == issueDetail.ProductID);
                                if (!allowNegative && (sourceMasLocation == null || sourceMasLocation.OHQuantity.GetValueOrDefault(0) < issueDetail.Quantity))
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
                                    sourceMasLocation.OHQuantity = sourceMasLocation.OHQuantity.GetValueOrDefault(0) - issueDetail.Quantity;
                                }
                                else
                                {
                                    // create new record
                                    sourceMasLocation = new IV_MasLocCache
                                    {
                                        CCNID = issueMaster.CCNID,
                                        MasterLocationID = issueMaster.SourceMasLocationID,
                                        ProductID = issueDetail.ProductID,
                                        OHQuantity = -issueDetail.Quantity
                                    };
                                    db.IV_MasLocCaches.InsertOnSubmit(sourceMasLocation);
                                }

                                #endregion

                                #region Transaction history

                                var sourceHistory = new MST_TransactionHistory
                                {
                                    CCNID = issueMaster.CCNID,
                                    StockUMID = issueDetail.StockUMID,
                                    MasterLocationID = issueMaster.SourceMasLocationID,
                                    ProductID = issueDetail.ProductID,
                                    LocationID = issueMaster.SourceLocationID,
                                    BinID = issueMaster.SourceBinID,
                                    RefMasterID = issueMaster.MiscellaneousIssueMasterID,
                                    RefDetailID = issueDetail.MiscellaneousIssueDetailID,
                                    PostDate = issueMaster.PostDate,
                                    TransDate = serverDate,
                                    Quantity = -issueDetail.Quantity,
                                    UserName = SystemProperty.UserName,
                                    TranTypeID = miscTranTypeId,
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

                                if (issueMaster.DesMasLocationID.GetValueOrDefault(0) > 0 && issueMaster.DesLocationID.GetValueOrDefault(0) > 0)
                                {
                                    #region bin cache

                                    var destBin = db.IV_BinCaches.FirstOrDefault(e => e.LocationID == issueMaster.DesLocationID && e.BinID == issueMaster.DesBinID && e.ProductID == issueDetail.ProductID);
                                    if (destBin != null)
                                    {
                                        destBin.OHQuantity = destBin.OHQuantity.GetValueOrDefault(0) + issueDetail.Quantity;
                                    }
                                    else
                                    {
                                        // create new record
                                        destBin = new IV_BinCache
                                        {
                                            BinID = issueMaster.DesBinID.GetValueOrDefault(0),
                                            CCNID = issueMaster.CCNID,
                                            LocationID = issueMaster.DesLocationID.GetValueOrDefault(0),
                                            MasterLocationID = issueMaster.DesMasLocationID.GetValueOrDefault(0),
                                            ProductID = issueDetail.ProductID,
                                            OHQuantity = issueDetail.Quantity
                                        };
                                        db.IV_BinCaches.InsertOnSubmit(destBin);
                                    }

                                    #endregion

                                    #region location cache

                                    var destLocation = db.IV_LocationCaches.FirstOrDefault(e => e.MasterLocationID == issueMaster.DesMasLocationID && e.LocationID == issueMaster.DesLocationID && e.ProductID == issueDetail.ProductID);
                                    if (destLocation != null)
                                    {
                                        destLocation.OHQuantity = destLocation.OHQuantity.GetValueOrDefault(0) + issueDetail.Quantity;
                                    }
                                    else
                                    {
                                        // create new record
                                        destLocation = new IV_LocationCache
                                        {
                                            CCNID = issueMaster.CCNID,
                                            LocationID = issueMaster.DesLocationID.GetValueOrDefault(0),
                                            MasterLocationID = issueMaster.DesMasLocationID.GetValueOrDefault(0),
                                            ProductID = issueDetail.ProductID,
                                            OHQuantity = issueDetail.Quantity
                                        };
                                        db.IV_LocationCaches.InsertOnSubmit(destLocation);
                                    }

                                    #endregion

                                    #region master location cache

                                    var destMasLocation = db.IV_MasLocCaches.FirstOrDefault(e => e.MasterLocationID == issueMaster.DesMasLocationID && e.ProductID == issueDetail.ProductID);
                                    if (destMasLocation != null)
                                    {
                                        destMasLocation.OHQuantity = destMasLocation.OHQuantity.GetValueOrDefault(0) + issueDetail.Quantity;
                                    }
                                    else
                                    {
                                        // create new record
                                        destMasLocation = new IV_MasLocCache
                                        {
                                            CCNID = issueMaster.CCNID,
                                            MasterLocationID = issueMaster.DesMasLocationID.GetValueOrDefault(0),
                                            ProductID = issueDetail.ProductID,
                                            OHQuantity = issueDetail.Quantity
                                        };
                                        db.IV_MasLocCaches.InsertOnSubmit(destMasLocation);
                                    }

                                    #endregion

                                    #region Transaction history

                                    var destHistory = new MST_TransactionHistory
                                    {
                                        CCNID = issueMaster.CCNID,
                                        StockUMID = issueDetail.StockUMID,
                                        MasterLocationID = issueMaster.DesMasLocationID.Value,
                                        ProductID = issueDetail.ProductID,
                                        LocationID = issueMaster.DesLocationID,
                                        BinID = issueMaster.DesBinID,
                                        RefMasterID = issueMaster.MiscellaneousIssueMasterID,
                                        RefDetailID = issueDetail.MiscellaneousIssueDetailID,
                                        PostDate = issueMaster.PostDate,
                                        TransDate = serverDate,
                                        Quantity = issueDetail.Quantity,
                                        UserName = SystemProperty.UserName,
                                        TranTypeID = miscTranTypeId,
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
                                }

                                #endregion
                            }
                        }

                        #endregion

                        // update last change to master
                        issueMaster.UserName = SystemProperty.UserName;
                        issueMaster.LastChange = serverDate;
                        // approved
                        issueMaster.DestroyApproved = 1;
                        // submit change
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
        /// Get all LocToLoc detail by MasterID
        /// </summary>
        /// <param name="pintcMasterId"></param>
        /// <returns></returns>

        public DataSet GetMiscellaneousByMaster(int pintcMasterId)
		{
			return new IV_MiscellaneousIssueDetailDS().GetMiscellaneousDetailByMaster(pintcMasterId);
		}
		
		/// <summary>
		/// Get LocToLoc Master infor by ID
		/// </summary>
		/// <param name="pintMasterId"></param>
		/// <returns></returns>
	
		public DataRow GetMiscellaneousMasterInfor(int pintMasterId)
		{
			return new IV_MiscellaneousIssueMasterDS().GetMiscellaneousMasterInfor(pintMasterId);
		}

        /// <summary>
        /// Deletes the transaction.
        /// </summary>
        /// <param name="miscIssueMasterId">The misc issue master id.</param>
		public void DeleteTransaction(int miscIssueMasterId)
        {
            const string methodName = This + ".DeleteTransaction()";
            try
            {
                using (var trans = new TransactionScope())
                {
                    using (var db = new PCSDataContext(Utils.Instance.ConnectionString))
                    {
                        var miscTranTypeId = db.MST_TranTypes.FirstOrDefault(t => t.Code == TransactionTypeEnum.IVMiscellaneousIssue.ToString()).TranTypeID;
                        var deleteTranTypeId = db.MST_TranTypes.FirstOrDefault(t => t.Code == TransactionTypeEnum.DeleteTransaction.ToString()).TranTypeID;
                        var serverDate = db.GetServerDate();

                        #region IV_MiscellaneousIssueMaster

                        var issueMaster = db.IV_MiscellaneousIssueMasters.FirstOrDefault(e => e.MiscellaneousIssueMasterID == miscIssueMasterId);
                        if (issueMaster == null)
                            return;

                        #endregion

                        // only update transaction history and cache if transaction is not Scrap, or Approved Scrap transaction
                        if ((issueMaster.IssuePurposeID == (int)PurposeEnum.Scrap && issueMaster.DestroyApproved == 1)
                            || issueMaster.IssuePurposeID != (int)PurposeEnum.Scrap)
                        {
                            #region old transaction history to be updated

                            var oldSourceHistory = db.MST_TransactionHistories.Where(e => e.RefMasterID == miscIssueMasterId && e.TranTypeID == miscTranTypeId);
                            foreach (var history in oldSourceHistory)
                            {
                                // mark as delete transaction
                                history.TranTypeID = deleteTranTypeId;
                                history.UserName = SystemProperty.UserName;
                            }

                            #endregion

                            #region cache data

                            foreach (IV_MiscellaneousIssueDetail issueDetail in issueMaster.IV_MiscellaneousIssueDetails)
                            {
                                var allowNegative = issueDetail.ITM_Product.AllowNegativeQty.GetValueOrDefault(false);

                                #region subtract from destionation cache

                                if (issueMaster.DesMasLocationID.GetValueOrDefault(0) > 0 && issueMaster.DesLocationID.GetValueOrDefault(0) > 0)
                                {
                                    #region bin cache

                                    var destBin = db.IV_BinCaches.FirstOrDefault(e => e.LocationID == issueMaster.DesLocationID && e.BinID == issueMaster.DesBinID && e.ProductID == issueDetail.ProductID);
                                    if (!allowNegative && (destBin == null || destBin.OHQuantity.GetValueOrDefault(0) < issueDetail.Quantity))
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
                                        destBin.OHQuantity = destBin.OHQuantity.GetValueOrDefault(0) - issueDetail.Quantity;
                                    }
                                    else
                                    {
                                        // create new record
                                        destBin = new IV_BinCache
                                        {
                                            BinID = issueMaster.DesBinID.GetValueOrDefault(0),
                                            CCNID = issueMaster.CCNID,
                                            LocationID = issueMaster.DesLocationID.GetValueOrDefault(0),
                                            MasterLocationID = issueMaster.DesMasLocationID.GetValueOrDefault(0),
                                            ProductID = issueDetail.ProductID,
                                            OHQuantity = -issueDetail.Quantity
                                        };
                                        db.IV_BinCaches.InsertOnSubmit(destBin);
                                    }

                                    #endregion

                                    #region location cache

                                    var destLocation = db.IV_LocationCaches.FirstOrDefault(e => e.MasterLocationID == issueMaster.DesMasLocationID && e.LocationID == issueMaster.DesLocationID && e.ProductID == issueDetail.ProductID);
                                    if (!allowNegative && (destLocation == null || destLocation.OHQuantity.GetValueOrDefault(0) < issueDetail.Quantity))
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
                                        destLocation.OHQuantity = destLocation.OHQuantity.GetValueOrDefault(0) - issueDetail.Quantity;
                                    }
                                    else
                                    {
                                        // create new record
                                        destLocation = new IV_LocationCache
                                        {
                                            CCNID = issueMaster.CCNID,
                                            LocationID = issueMaster.DesLocationID.GetValueOrDefault(0),
                                            MasterLocationID = issueMaster.DesMasLocationID.GetValueOrDefault(0),
                                            ProductID = issueDetail.ProductID,
                                            OHQuantity = -issueDetail.Quantity
                                        };
                                        db.IV_LocationCaches.InsertOnSubmit(destLocation);
                                    }

                                    #endregion

                                    #region master location cache

                                    var destMasLocation = db.IV_MasLocCaches.FirstOrDefault(e => e.MasterLocationID == issueMaster.DesMasLocationID && e.ProductID == issueDetail.ProductID);
                                    if (!allowNegative && (destMasLocation == null || destMasLocation.OHQuantity.GetValueOrDefault(0) < issueDetail.Quantity))
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
                                        destMasLocation.OHQuantity = destMasLocation.OHQuantity.GetValueOrDefault(0) - issueDetail.Quantity;
                                    }
                                    else
                                    {
                                        // create new record
                                        destMasLocation = new IV_MasLocCache
                                        {
                                            CCNID = issueMaster.CCNID,
                                            MasterLocationID = issueMaster.DesMasLocationID.GetValueOrDefault(0),
                                            ProductID = issueDetail.ProductID,
                                            OHQuantity = -issueDetail.Quantity
                                        };
                                        db.IV_MasLocCaches.InsertOnSubmit(destMasLocation);
                                    }

                                    #endregion

                                    #region Transaction history

                                    var destHistory = new MST_TransactionHistory
                                    {
                                        CCNID = issueMaster.CCNID,
                                        StockUMID = issueDetail.StockUMID,
                                        MasterLocationID = issueMaster.DesMasLocationID.GetValueOrDefault(0),
                                        ProductID = issueDetail.ProductID,
                                        LocationID = issueMaster.DesLocationID,
                                        BinID = issueMaster.DesBinID,
                                        RefMasterID = issueMaster.MiscellaneousIssueMasterID,
                                        RefDetailID = issueDetail.MiscellaneousIssueDetailID,
                                        PostDate = issueMaster.PostDate,
                                        TransDate = serverDate,
                                        Quantity = -issueDetail.Quantity,
                                        UserName = SystemProperty.UserName,
                                        TranTypeID = miscTranTypeId,
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
                                }

                                #endregion

                                #region add to source cache

                                #region bin cache

                                var sourceBin = db.IV_BinCaches.FirstOrDefault(e => e.LocationID == issueMaster.SourceLocationID && e.BinID == issueMaster.SourceBinID && e.ProductID == issueDetail.ProductID);
                                if (sourceBin != null)
                                {
                                    sourceBin.OHQuantity = sourceBin.OHQuantity.GetValueOrDefault(0) + issueDetail.Quantity;
                                }
                                else
                                {
                                    // create new record
                                    sourceBin = new IV_BinCache
                                    {
                                        BinID = issueMaster.SourceBinID.GetValueOrDefault(0),
                                        CCNID = issueMaster.CCNID,
                                        LocationID = issueMaster.SourceLocationID,
                                        MasterLocationID = issueMaster.SourceMasLocationID,
                                        ProductID = issueDetail.ProductID,
                                        OHQuantity = issueDetail.Quantity
                                    };
                                    db.IV_BinCaches.InsertOnSubmit(sourceBin);
                                }

                                #endregion

                                #region location cache

                                var sourceLocation = db.IV_LocationCaches.FirstOrDefault(e => e.MasterLocationID == issueMaster.SourceMasLocationID && e.LocationID == issueMaster.SourceLocationID && e.ProductID == issueDetail.ProductID);
                                if (sourceLocation != null)
                                {
                                    sourceLocation.OHQuantity = sourceLocation.OHQuantity.GetValueOrDefault(0) + issueDetail.Quantity;
                                }
                                else
                                {
                                    // create new record
                                    sourceLocation = new IV_LocationCache
                                    {
                                        CCNID = issueMaster.CCNID,
                                        LocationID = issueMaster.SourceLocationID,
                                        MasterLocationID = issueMaster.SourceMasLocationID,
                                        ProductID = issueDetail.ProductID,
                                        OHQuantity = issueDetail.Quantity
                                    };
                                    db.IV_LocationCaches.InsertOnSubmit(sourceLocation);
                                }

                                #endregion

                                #region master location cache

                                var sourceMasLocation = db.IV_MasLocCaches.FirstOrDefault(e => e.MasterLocationID == issueMaster.SourceMasLocationID && e.ProductID == issueDetail.ProductID);
                                if (sourceMasLocation != null)
                                {
                                    sourceMasLocation.OHQuantity = sourceMasLocation.OHQuantity.GetValueOrDefault(0) + issueDetail.Quantity;
                                }
                                else
                                {
                                    // create new record
                                    sourceMasLocation = new IV_MasLocCache
                                    {
                                        CCNID = issueMaster.CCNID,
                                        MasterLocationID = issueMaster.SourceMasLocationID,
                                        ProductID = issueDetail.ProductID,
                                        OHQuantity = issueDetail.Quantity
                                    };
                                    db.IV_MasLocCaches.InsertOnSubmit(sourceMasLocation);
                                }

                                #endregion

                                #region Transaction history

                                var sourceHistory = new MST_TransactionHistory
                                {
                                    CCNID = issueMaster.CCNID,
                                    StockUMID = issueDetail.StockUMID,
                                    MasterLocationID = issueMaster.SourceMasLocationID,
                                    ProductID = issueDetail.ProductID,
                                    LocationID = issueMaster.SourceLocationID,
                                    BinID = issueMaster.SourceBinID,
                                    RefMasterID = issueMaster.MiscellaneousIssueMasterID,
                                    RefDetailID = issueDetail.MiscellaneousIssueDetailID,
                                    PostDate = issueMaster.PostDate,
                                    TransDate = serverDate,
                                    Quantity = issueDetail.Quantity,
                                    UserName = SystemProperty.UserName,
                                    TranTypeID = miscTranTypeId,
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

                                db.IV_MiscellaneousIssueDetails.DeleteOnSubmit(issueDetail);
                            }
                            #endregion
                        }

                        db.IV_MiscellaneousIssueMasters.DeleteOnSubmit(issueMaster);
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
	}
}

