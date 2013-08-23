using System;
using System.Data;


using PCSComProcurement.Purchase.DS;
using PCSComUtils.MasterSetup.DS;
using PCSComUtils.Common;


namespace PCSComProcurement.Purchase.BO
{
	public interface IPODeliveryByHourBO
	{
		DataSet GetVendor();
		DataSet GetVendorDeliverySchedule();
	}
	/// <summary>
	/// Summary description for PODeliverySlipBO.
	/// </summary>
	
	public class PODeliveryByHourBO : IPODeliveryByHourBO //ServicedComponent,
	{
		public PODeliveryByHourBO()
		{
			//
			// TODO: Add constructor logic here
			//
		}


		/// <summary>
		/// Insert a new record into database
		/// </summary>
		public void Add(object pObjectDetail)
		{
			throw new NotImplementedException();
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

		/// <summary>
		/// Return the DataSet (list of record) by inputing the FieldList and Condition
		/// </summary>
		public void UpdateDataSet(DataSet dstData)
		{
			(new PO_VendorDeliveryScheduleDS()).UpdateDataSet(dstData);
		}

		/// <summary>
		/// Update into Database
		/// </summary>
		public void Update(object pObjectDetail)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// GetVendor
		/// </summary>
		/// <returns></returns>
		/// <author>Trada</author>
		/// <date>Tuesday, Feb 7 2006</date>
	
		public DataSet GetVendor()
		{
			MST_PartyDS dsMST_Party = new MST_PartyDS();
			return dsMST_Party.List(" Type = "+(int)PartyTypeEnum.VENDOR
				+" OR Type = "+(int)PartyTypeEnum.BOTH
				+" OR Type = "+(int)PartyTypeEnum.OUTSIDE
				+ " ORDER BY " + MST_PartyTable.NAME_FLD); //dsMST_Party.GetVendor();
		}

		/// <summary>
		/// GetVendor
		/// </summary>
		/// <returns></returns>
		/// <author>Trada</author>
		/// <date>Tuesday, Feb 7 2006</date>
	
		public DataSet GetVendorDeliverySchedule()
		{
			const string TIME = "Time";
			PO_VendorDeliveryScheduleDS dsPO_VendorDeliverySchedule = new PO_VendorDeliveryScheduleDS();
			DataSet dstVendorDeliverySchedule = new DataSet();
			dstVendorDeliverySchedule = dsPO_VendorDeliverySchedule.GetVendorDeliverySchedule();
			//dstVendorDeliverySchedule.Tables[0].Columns.Add(TIME, typeof(DateTime));
			DateTime dtmTime = new DateTime();
			foreach (DataRow drow in dstVendorDeliverySchedule.Tables[0].Rows)
			{
				dtmTime = new DateTime(1,1,1, int.Parse(drow[PO_VendorDeliveryScheduleTable.DELHOUR_FLD].ToString()), int.Parse(drow[PO_VendorDeliveryScheduleTable.DELMIN_FLD].ToString()), 0);
				drow[TIME] = dtmTime;
			}
			return dstVendorDeliverySchedule;

		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="pdtbData"></param>
	
		public void SaveDeliveryHour(DataTable pdtbData)
		{
			(new PO_VendorDeliveryScheduleDS()).SaveDeliveryHour(pdtbData);
		}
	}
}
