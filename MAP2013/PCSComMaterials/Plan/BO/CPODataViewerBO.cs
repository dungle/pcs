using System;
using System.Data;
using System.Collections;
using System.Linq;
using System.Transactions;
using PCSComUtils.Common;
using PCSComMaterials.Plan.DS;
using PCSComUtils.DataAccess;
using PCSComUtils.DataContext;

namespace PCSComMaterials.Plan.BO
{
    public class CPODataViewerBO
    {
        /// <summary>
        /// Insert a new record into database
        /// </summary>

        public void Add(object pObjectDetail)
        {
            throw new NotImplementedException();
        }

        public void ConvertToNewWO(object pobjCPO)
        {
        }

        /// <summary>
        /// Delete record by condition
        /// </summary>

        public void Delete(object pObjectVO)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get the object information by ID of VO class
        /// </summary>

        public object GetObjectVO(int pintID, string VOclass)
        {
            throw new NotImplementedException();
        }

        public void PrepareDataForPO()
        {
        }

        public DataSet Search(Hashtable phtbCriteria)
        {
            return (new MTR_CPODS()).Search(phtbCriteria);
        }

        public DataSet SearchForDCP(Hashtable phtbCriteria)
        {
            return (new MTR_CPODS()).SearchForDCP(phtbCriteria);
        }

        /// <summary>
        /// Return the DataSet (list of record) by inputing the FieldList and Condition
        /// </summary>

        public void UpdateDataSet(DataSet dstData)
        {
            MTR_CPODS dsCPO = new MTR_CPODS();
            dsCPO.UpdateDataSet(dstData);
        }

        /// <summary>
        /// Delete MRP result by list of CPO
        /// </summary>

        public void DeleteMRP(string pstrCPOIDs)
        {
            MTR_CPODS dsCPO = new MTR_CPODS();
            dsCPO.Delete(pstrCPOIDs);
        }

        /// <summary>
        /// UpdateDataSetForDCP
        /// </summary>
        /// <param name="dstData"></param>
        /// <author>Trada</author>
        /// <date>Monday, April 24 2006</date>

        public void UpdateDataSetForDCP(DataSet dstData)
        {
            MTR_CPODS dsCPO = new MTR_CPODS();
            dsCPO.UpdateDataSetForDCP(dstData);
        }

        /// <summary>
        /// Update into Database
        /// </summary>

        public void Update(object pObjectDetail)
        {
            throw new NotImplementedException();
        }

        public DataTable GetVendorDeliveryPolicyByParty(int pintPartyID)
        {
            return new MTR_CPODS().ListVendorDeliveryPolicy(pintPartyID);
        }

        public DataSet GetWorkDayCalendar()
        {
            return new MTR_CPODS().GetWorkDayCalendar();
        }

        public DateTime GetAsOfDate(int pintCycleID, bool pblnIsMPS)
        {
            MTR_MRPCycleOptionMasterDS dsMaster = new MTR_MRPCycleOptionMasterDS();
            return dsMaster.GetAsOfDate(pintCycleID, pblnIsMPS);
        }

        public void UpdateWorkOrderDetail(int workOrderMasterId, int productId, decimal quantity, DateTime startDate, DateTime dueDate, DataRowView detailRow)
        {
            using (var trans = new TransactionScope(TransactionScopeOption.Required, TimeSpan.FromHours(1)))
            {
                using (var db = new PCSDataContext(Utils.Instance.ConnectionString))
                {
                    var maxLine = db.PRO_WorkOrderDetails.Max(d => d.Line);
                    var workOrderDetail =
                        db.PRO_WorkOrderDetails.FirstOrDefault(w => w.WorkOrderMasterID == workOrderMasterId
                                                                    && w.ProductID == productId
                                                                    && w.StartDate.Equals(startDate)
                                                                    && w.DueDate.Equals(dueDate)
                                                                    && w.Status == 1); // unreleased work order
                    if (workOrderDetail == null)
                    {
                        // create new work order detail for new CPO
                        workOrderDetail = new PRO_WorkOrderDetail
                        {
                            WorkOrderMasterID = workOrderMasterId,
                            OrderQuantity = quantity,
                            ProductID = productId,
                            StartDate = startDate,
                            DueDate = dueDate,
                            Status = (byte?)WOLineStatus.Unreleased,
                            Line = maxLine + 1,
                            StockUMID = (int)detailRow[MTR_CPOTable.STOCKUMID_FLD]
                        };
                        db.PRO_WorkOrderDetails.InsertOnSubmit(workOrderDetail);
                    }
                    else
                    {
                        // if quantity is equal 0, then we remove work order line
                        if (quantity == 0)
                        {
                            db.PRO_WorkOrderDetails.DeleteOnSubmit(workOrderDetail);

                        }
                        else
                        {
                            // update quantity
                            workOrderDetail.OrderQuantity = quantity;
                        }
                    }
                    db.SubmitChanges();
                }
                trans.Complete();
            }
        }
    }
}