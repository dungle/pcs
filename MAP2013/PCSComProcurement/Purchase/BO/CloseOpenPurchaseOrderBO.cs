using System;
using System.Collections;
using System.Data;

using PCSComUtils.Admin.BO;
using PCSComUtils.Admin.DS;
using PCSComUtils.Common;

using PCSComUtils.MasterSetup.DS;
using PCSComProcurement.Purchase.DS;
namespace PCSComProcurement.Purchase.BO
{

	#region Interface ICloseOpenPurchaseOrderBO
	
	public interface ICloseOpenPurchaseOrderBO
	{
		DataSet GetPurchaseOrderDetail(int pintCCNID, int pintMasterLocationID, int pintPurchaseOrderMasterID, bool pblnClose, DateTime pdtmFromScheduleDate, DateTime pdtmToScheduleDate);
	}

	#endregion

	/// <summary>
	/// RightByRecordBO
	/// </summary>
	
	public class CloseOpenPurchaseOrderBO : ICloseOpenPurchaseOrderBO
	{
		private const string THIS = "PCSComProcurement.Purchase.BO.CloseOpenPurchaseOrderBO";
		public CloseOpenPurchaseOrderBO()
		{
			
		}

		public void Add(object pObjectDetail)
		{
			throw new NotImplementedException();
		}

		public void Delete(object pObjectVO)
		{
			throw new NotImplementedException();
		}

		public object GetObjectVO(int pintID, string VOclass)
		{
			throw new NotImplementedException();
		}

		public void UpdateDataSet(DataSet dstData)
		{
			
			throw new NotImplementedException();
		}
		public void Update(object pobjObject)
		{
			
			throw new NotImplementedException();
		}
		/// <summary>
		/// Get PurchaseOrderDetail to Close/Open
		/// </summary>
		/// <param name="pintCCNID"></param>
		/// <param name="pintMasterLocationID"></param>
		/// <param name="pintPurchaseOrderMasterID"></param>
		/// <param name="pblnClose"></param>
		/// <param name="pdtmFromScheduleDate"></param>
		/// <param name="pdtmToScheduleDate"></param>
		/// <returns></returns>
		/// <author>Trada</author>
		/// <date>Friday, Nov 25 2005</date>
	
		public DataSet GetPurchaseOrderDetail(int pintCCNID, int pintMasterLocationID, int pintPurchaseOrderMasterID, bool pblnClose, DateTime pdtmFromScheduleDate, DateTime pdtmToScheduleDate)
		{
			DataSet dstPurchaseOrderDetail = new DataSet();
			PO_PurchaseOrderDetailDS dsPurchaseOrderDetail = new PO_PurchaseOrderDetailDS();
			return dstPurchaseOrderDetail = dsPurchaseOrderDetail.GetPurchaseOrderDetail(pintCCNID, pintMasterLocationID, pintPurchaseOrderMasterID, pblnClose, pdtmFromScheduleDate, pdtmToScheduleDate);
		}
		/// <summary>
		/// CloseOrOpenPOLines
		/// </summary>
		/// <param name="pblnPOClose"></param>
		/// <param name="parrSelectedLines"></param>
		/// <author>Trada</author>
		/// <date>Monday, Nov 29 2005</date>
	
		public void CloseOrOpenPOLines(bool pblnPOClose, ArrayList parrSelectedLines)
		{
			const string COLON = ",";
			string strListOfIds = string.Empty;
			for (int i = 0; i < parrSelectedLines.Count; i++)
			{
				strListOfIds += parrSelectedLines[i] + COLON;
			}
			// remove the last "," in string
			strListOfIds = strListOfIds.Remove(strListOfIds.Length - 1, 1);
			PO_PurchaseOrderDetailDS dsPurchaseOrderDetail = new PO_PurchaseOrderDetailDS();
			dsPurchaseOrderDetail.CloseOrOpenPOLines(pblnPOClose, strListOfIds);
		}
	}
}