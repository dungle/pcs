using System;
using System.Collections;
using System.Data;
using PCSComUtils.PCSExc;


using PCSComUtils.Common;
using PCSComUtils.MasterSetup.DS;
using PCSComProcurement.Purchase.DS;

namespace PCSComProcurement.Purchase.BO
{
	public class POPurchaseOrderApprovalBO
	{
		private const string CHECK_APPROVE = "Approved";
		private const string OPEN_AMOUNT = "openAmount";
		private const string RECEIVE_AMOUNT = "ReceiveAmount";
		private const string TRUE = "True";

		public void Add(object pObjectDetail)
		{
			// TODO:  

		}
	
		public void Delete(object pObjectVO)
		{
			// TODO:  

		}
	
		public object GetObjectVO(int pintID, string VOclass)
		{
			// TODO:  
			return null;
		}
	
		public void Update(object pObjectDetail)
		{
			// TODO:  

		}
	

		public void UpdateDataSet(DataSet dstData)
		{
			// TODO:  

		}
		public DataSet ListEmployee()
		{
			try
			{
				MST_EmployeeDS dsEmployee = new MST_EmployeeDS();
				return dsEmployee.List();
			}
			catch (PCSDBException ex)
			{
				throw ex;
			}
			catch (Exception ex)
			{
				throw ex;
			}

		}
		public DataSet ListPODetailByPOMasterID(int pintPOMasterID, int pintCCNID, bool pblnApproved)
		{
			try
			{
				DataSet dstApprover = new DataSet();

				PO_PurchaseOrderMasterDS dsPurchaseOrderMaster = new PO_PurchaseOrderMasterDS();
				//Table[0] has got 2 fields PurchaseOrderDetailID, ReceiveAmount
				dstApprover.Tables.Add(dsPurchaseOrderMaster.ListPODetailByMasterIDOfPOReceiptDetail(pintPOMasterID, pblnApproved).Tables[0].Copy());	
				//Table[1] has got 12 fields PONo, PurchaseOrderDetailID, Line, Code, Description, Revision, BuyingUM, OrderQuantity, AvailableQty, Currency, TotalAmount and openAmount
				dstApprover.Tables.Add(dsPurchaseOrderMaster.ListPODetailByPOMasterID(pintPOMasterID, pintCCNID, pblnApproved).Tables[0].Copy());
				dstApprover.Tables[1].Columns.Add(OPEN_AMOUNT, typeof(decimal));
				//find rows to compare and insert value to openAmount field
				if (dstApprover.Tables[0].Rows.Count == 0)
				{
					foreach (DataRow drowTable1 in dstApprover.Tables[1].Rows)
					{
						drowTable1[OPEN_AMOUNT] = decimal.Parse(drowTable1[PO_PurchaseOrderDetailTable.TOTALAMOUNT_FLD].ToString());
					}
				}
				else
				{
					foreach (DataRow drowTable1 in dstApprover.Tables[1].Rows)
					{
						foreach (DataRow drowTable0 in dstApprover.Tables[0].Rows)
						{
						
							if (drowTable0[PO_PurchaseOrderDetailTable.PURCHASEORDERDETAILID_FLD].ToString() == drowTable1[PO_PurchaseOrderDetailTable.PURCHASEORDERDETAILID_FLD].ToString())
							{
								if (drowTable0[RECEIVE_AMOUNT].ToString() != string.Empty)
								{
									drowTable1[OPEN_AMOUNT] = decimal.Parse(drowTable1[PO_PurchaseOrderDetailTable.TOTALAMOUNT_FLD].ToString()) - decimal.Parse(drowTable0[RECEIVE_AMOUNT].ToString());
								}
								else 
									drowTable1[OPEN_AMOUNT] = decimal.Parse(drowTable1[PO_PurchaseOrderDetailTable.TOTALAMOUNT_FLD].ToString());

							}
						}
					}
				}

			return dstApprover;
			}
			catch (PCSDBException ex)
			{
				throw ex;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		public int GetPurchaseOrderMasterID(string pstrCode)
		{
			try
			{
				PO_PurchaseOrderMasterDS dsMasterBlank = new PO_PurchaseOrderMasterDS();
				PO_PurchaseOrderMasterVO voMasterBlank = new PO_PurchaseOrderMasterVO();
				voMasterBlank =	(PO_PurchaseOrderMasterVO)dsMasterBlank.GetObjectVO(pstrCode);
				return voMasterBlank.PurchaseOrderMasterID;
			}
			catch (PCSDBException ex)
			{
				throw ex;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		public DateTime GetOrderDateByPOMasterID(int pintPOMasterID)
		{
			try
			{
				PO_PurchaseOrderMasterDS dsPO_PurchaseOrderMaster = new PO_PurchaseOrderMasterDS();
				return dsPO_PurchaseOrderMaster.GetOrderDateByPOMasterID(pintPOMasterID);

			}
			catch (PCSDBException ex)
			{
				throw ex;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
	
		public bool CheckLevelApproval(int pintApproverID, int pintMasterID, int pintCCNID)
		{
            PO_PurchaseOrderDetailDS dsPurchaseOrderDetail = new PO_PurchaseOrderDetailDS();
            DataSet dstApprovalLevel = new DataSet();

            dstApprovalLevel = dsPurchaseOrderDetail.GetApprovalLevel(pintApproverID);
            PO_PurchaseOrderMasterDS dsPurchaseOrderMaster = new PO_PurchaseOrderMasterDS();

            dstApprovalLevel.Tables.Add(dsPurchaseOrderMaster.ListPODetailByPOMasterID(pintMasterID, pintCCNID, false).Tables[0].Copy());

            for (int i = 0; i < dstApprovalLevel.Tables[0].Rows.Count; i++)
            {
                decimal amount = 0, total = 0;
                if (dstApprovalLevel.Tables[0].Rows[i][MST_ApprovalLevelTable.AMOUNT_FLD].ToString().Trim() != String.Empty)
                {
                    amount = decimal.Parse(dstApprovalLevel.Tables[0].Rows[i][MST_ApprovalLevelTable.AMOUNT_FLD].ToString());

                }
                if (dstApprovalLevel.Tables[1].Rows[i][PO_PurchaseOrderDetailTable.TOTALAMOUNT_FLD].ToString().Trim() != String.Empty)
                {
                    total = decimal.Parse(dstApprovalLevel.Tables[1].Rows[i][PO_PurchaseOrderDetailTable.TOTALAMOUNT_FLD].ToString());
                }
                if (amount < total)
                {
                    return false;
                }
                else return true;
            }

            return true;
		}

		public void UpdateAllAfterApprove(DataTable pdtbDataApprove, DateTime pdtmApprovalDate, int pintApproveID)
		{
			try
			{
				PO_PurchaseOrderDetailDS dsPurchaseOrderDetail = new PO_PurchaseOrderDetailDS();

				//for approval
				if (pintApproveID > 0)
				{
					foreach (DataRow drow in pdtbDataApprove.Rows)
					{
						if (drow[CHECK_APPROVE].ToString().Trim() == TRUE)
						{
							drow[PO_PurchaseOrderDetailTable.APPROVERID_FLD] = pintApproveID;
							drow[PO_PurchaseOrderDetailTable.APPROVALDATE_FLD] = pdtmApprovalDate;
						}
					}
				}
				else
					foreach (DataRow drow in pdtbDataApprove.Rows)
					{
						if (drow[CHECK_APPROVE].ToString().Trim() == TRUE)
						{
							drow[PO_PurchaseOrderDetailTable.APPROVERID_FLD] = DBNull.Value;
							drow[PO_PurchaseOrderDetailTable.APPROVALDATE_FLD] = pdtmApprovalDate;
						}
					}

				//for cancel
				DataSet dstData = new DataSet();
				dstData.Tables.Add(pdtbDataApprove.Copy());
				dsPurchaseOrderDetail.UpdateDataSetForApproving(dstData);
			}
			catch (PCSDBException ex)
			{
				throw ex;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
	}
	
}
