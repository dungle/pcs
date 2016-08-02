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
        public DataSet Search(Hashtable phtbCriteria)
        {
            return (new MTR_CPODS()).Search(phtbCriteria);
        }

        public DataSet SearchForDCP(Hashtable phtbCriteria)
        {
            return (new MTR_CPODS()).SearchForDCP(phtbCriteria);
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

        public void UpdateWorkOrderDetail(int workOrderMasterId, int productId, decimal quantity, DateTime startDate,
            DateTime dueDate, DataRowView detailRow)
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
                            Status = (byte?) WOLineStatus.Unreleased,
                            Line = maxLine + 1,
                            StockUMID = (int) detailRow[MTR_CPOTable.STOCKUMID_FLD]
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

        public MTR_MPSCycleOptionMaster GetMpsCycle(int cycleId)
        {
            using (var db = new PCSDataContext(Utils.Instance.ConnectionString))
            {
                return db.MTR_MPSCycleOptionMasters.FirstOrDefault(c => c.MPSCycleOptionMasterID == cycleId);
            }
        }

        public MTR_MRPCycleOptionMaster GetMrpCycle(int cycleId)
        {
            using (var db = new PCSDataContext(Utils.Instance.ConnectionString))
            {
                return db.MTR_MRPCycleOptionMasters.FirstOrDefault(c => c.MRPCycleOptionMasterID == cycleId);
            }
        }

        public PRO_DCOptionMaster GetDcpOption(int cycleId)
        {
            using (var db = new PCSDataContext(Utils.Instance.ConnectionString))
            {
                return db.PRO_DCOptionMasters.FirstOrDefault(c => c.DCOptionMasterID == cycleId);
            }
        }

        /// <summary>
        /// Wipe wrong date item for current cycle
        /// </summary>
        /// <param name="cycleId"></param>
        public void WipeWrongItem(int cycleId)
        {
            MTR_CPODS dsCPO = new MTR_CPODS();
            dsCPO.WipeWrongItem(cycleId);
        }
        /// <summary>
        /// Log all item with wrong schedule date to database
        /// </summary>
        /// <param name="wrongItemTable"></param>
        public void LogWrongItem(DataTable wrongItemTable)
        {
            MTR_CPODS dsCPO = new MTR_CPODS();
            dsCPO.LogWrongItem(wrongItemTable);
        }
    }
}