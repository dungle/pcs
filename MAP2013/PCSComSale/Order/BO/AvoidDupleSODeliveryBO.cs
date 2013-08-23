using System;
using System.Data;


using PCSComSale.Order.DS;

using PCSComUtils.Common;
using PCSComUtils.PCSExc;

namespace PCSComSale.Order.BO
{
	public interface IAvoidDupleSODeliveryBO
	{
		
	}
	/// <summary>
	/// Summary description for AvoidDupleSODeliveryBO.
	/// </summary>
	
	public class AvoidDupleSODeliveryBO : IAvoidDupleSODeliveryBO
	{
		DataTable dtbDupleSOToReturn = new DataTable();
		public AvoidDupleSODeliveryBO()
		{
			//
			// TODO: Add constructor logic here
			//
		}
	
		public void Add(object pObjectDetail)
		{
			throw new NotImplementedException();
		}
		/// <summary>
		/// Delete record by condition
		/// </summary>
	
		public void Delete(object pObjectVO)
		{
			return;			
		}
	
		public object GetObjectVO(int pintID, string VOclass)
		{
			return null;
		}
		/// <summary>
		/// Update into Database
		/// </summary>
	
		public void Update(object pObjectDetail)
		{
			throw new NotImplementedException();
		}
		/// <summary>
		/// Return the DataSet (list of record) by inputing the FieldList and Condition
		/// </summary>
	
		public void UpdateDataSet(DataSet pdstData)
		{
			throw new NotImplementedException();
		}
		/// <summary>
		/// DeleteDupleSODelivery
		/// </summary>
		/// <param name="pstrDeliveryID"></param>
		/// <author>Trada</author>
		/// <date>Friday, Feb 17 2006</date>
	
		public void DeleteDupleSODelivery(string pstrDeliveryID)
		{
			SO_DeliveryScheduleDS dsSO_DeliverySchedule = new SO_DeliveryScheduleDS();
			dsSO_DeliverySchedule.DeleteDupleSODelivery(pstrDeliveryID);
 		}
		/// <summary>
		/// Check if two rows are the same
		/// </summary>
		/// <param name="pdrowFirst"></param>
		/// <param name="pdrowSecond"></param>
		/// <returns></returns>
		/// <author>Trada</author>
		/// <date>Friday, Feb 17 2006</date>
	
		private bool TwoRowsAreTheSame(DataRow pdrowFirst, DataRow pdrowSecond)
		{
			if ((int.Parse(pdrowFirst[ITM_ProductTable.PRODUCTID_FLD].ToString()) == int.Parse(pdrowSecond[ITM_ProductTable.PRODUCTID_FLD].ToString()))
				&& ((DateTime)pdrowFirst[SO_DeliveryScheduleTable.SCHEDULEDATE_FLD] == (DateTime)pdrowSecond[SO_DeliveryScheduleTable.SCHEDULEDATE_FLD]))
			{
				return true;
			}
			return false;
		}
		/// <summary>
		/// Check if two rows are the same
		/// </summary>
		/// <param name="pdrowDataRow"></param>
		/// <returns></returns>
		/// <author>Trada</author>
		/// <date>Friday, Feb 17 2006</date>
	
		private bool RowIsExist(DataRow pdrowDataRow)
		{
			bool blnExist = false;
			if (dtbDupleSOToReturn.Rows.Count > 0)
			{
				foreach (DataRow drow in dtbDupleSOToReturn.Rows)
				{
					if (int.Parse(drow[SO_DeliveryScheduleTable.DELIVERYSCHEDULEID_FLD].ToString()) == int.Parse(pdrowDataRow[SO_DeliveryScheduleTable.DELIVERYSCHEDULEID_FLD].ToString()))
					{
						blnExist = true;
					}
				}
			}
			return blnExist;
		}
		/// <summary>
		/// SearchDupleSO
		/// </summary>
		/// <param name="pdtmDate"></param>
		/// <param name="pintPartyID"></param>
		/// <returns></returns>
		/// <author>Trada</author>
		/// <date>Friday, Feb 17 2006</date>
	
		public DataTable SearchDupleSO(DateTime pdtmDate, int pintPartyID)
		{
			DataSet dstDupleSO = new DataSet();
			SO_SaleOrderMasterDS dsSaleOrderMaster = new SO_SaleOrderMasterDS();
			dstDupleSO = dsSaleOrderMaster.GetDupleSODelivery(pdtmDate, pintPartyID);
			dtbDupleSOToReturn = dstDupleSO.Tables[0].Clone();
			for(int i = 0; i < dstDupleSO.Tables[0].Rows.Count; i++)
			{
				for (int j = i + 1; j < dstDupleSO.Tables[0].Rows.Count; j++)
				{
					if (TwoRowsAreTheSame(dstDupleSO.Tables[0].Rows[i], dstDupleSO.Tables[0].Rows[j]))
					{
						
						DataRow drowFirstNewRow = dtbDupleSOToReturn.NewRow();
						if (!RowIsExist(dstDupleSO.Tables[0].Rows[i]))
						{
							//add the first column into dtbDupleSOToReturn
							foreach (DataColumn dcol in  dtbDupleSOToReturn.Columns)
							{
								drowFirstNewRow[dcol.Caption] = dstDupleSO.Tables[0].Rows[i][dcol.Caption];
							}
							dtbDupleSOToReturn.Rows.Add(drowFirstNewRow);
						}
						DataRow drowSecondNewRow = dtbDupleSOToReturn.NewRow();
						if (!RowIsExist(dstDupleSO.Tables[0].Rows[j]))
						{
							//add the first column into dtbDupleSOToReturn
							foreach (DataColumn dcol in  dtbDupleSOToReturn.Columns)
							{
								drowSecondNewRow[dcol.Caption] = dstDupleSO.Tables[0].Rows[j][dcol.Caption];
							}
							dtbDupleSOToReturn.Rows.Add(drowSecondNewRow);
							//dtbDupleSOToReturn.Rows.Add(dstDupleSO.Tables[0].Rows[j]);
						}
					}
				}
			}
			return dtbDupleSOToReturn;
		}
	}
}
