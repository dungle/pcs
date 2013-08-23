using System;
using System.Data;


using PCSComSale.Order.DS;
using PCSComUtils.Common;

using PCSComUtils.PCSExc;

namespace PCSComSale.Order.BO
{
	public interface ICustomerAndItemReferenceBO 
	{
		DataSet ListDetailByMasterID(int pintPartyID, int pintCCNID);
		void DeleteMasterAndDetail(object pobjMaster);
		object GetObjectMasterByID(int pintPartyID, int pintCCNID);
		void UpdateMasterAndDetail(object pobjMaster, DataSet pdstDetail);
	}
	/// <summary>
	/// Summary description for .
	/// </summary>
	
	
	public class CustomerAndItemReferenceBO :ICustomerAndItemReferenceBO
	{
		private const string THIS = "PCSComSale.Order.BO.CustomerAndItemReferenceBO";
		public CustomerAndItemReferenceBO()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public void Add(object pObjectDetail)
		{
		
		}
	
		public void Delete(object pObjectVO)
		{
		
		}
	
		public object GetObjectVO(int pintID, string VOclass)
		{
			
			return null;
		}
	
		public void Update(object pObjectDetail)
		{
			
		}
	
		public void UpdateDataSet(DataSet dstData)
		{
			

		}

	
		public DataSet ListDetailByMasterID(int pintPartyID, int pintCCNID)
		{
			try
			{
				return new SO_CustomerItemRefDetailDS().List(pintPartyID, pintCCNID);
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
		
		/// <summary>
		/// Add Master & Detail Of Customer reference
		/// </summary>
		/// <param name="pobjMaster"></param>
		/// <param name="pdstDetail"></param>
	
		public void UpdateMasterAndDetail(object pobjMaster, DataSet pdstDetail)
		{
			try
			{
				SO_CustomerItemRefMasterDS dsMaster = new SO_CustomerItemRefMasterDS();
				SO_CustomerItemRefDetailDS dsDetail = new SO_CustomerItemRefDetailDS();
				int intMasterID = ((SO_CustomerItemRefMasterVO) pobjMaster).CustomerItemRefMasterID;
				if (intMasterID == 0)
				{
					//Add Master
					intMasterID = dsMaster.AddAndReturnID(pobjMaster);
				}
				else
				{
					//Update Master
					dsMaster.Update(pobjMaster);
				}
	
				//Add Detail
				for (int i =0; i < pdstDetail.Tables[0].Rows.Count; i++)
				{
					if (pdstDetail.Tables[0].Rows[i].RowState == DataRowState.Added)
					{
						pdstDetail.Tables[0].Rows[i][SO_CustomerItemRefDetailTable.CUSTOMERITEMREFMASTERID_FLD] = intMasterID;
					}
				}

				dsDetail.UpdateDataSet(pdstDetail);
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

		/// <summary>
		/// Delete the Master by ID : - Delete Detail 
		/// - Delete Master
		/// </summary>
		/// <param name="pobjMaster"></param>
	
		public void DeleteMasterAndDetail(object pobjMaster)
		{
			try
			{
				SO_CustomerItemRefMasterDS dsMaster = new SO_CustomerItemRefMasterDS();
				SO_CustomerItemRefDetailDS dsDetail = new SO_CustomerItemRefDetailDS();
				if (((SO_CustomerItemRefMasterVO) pobjMaster).CustomerItemRefMasterID != 0)
				{
					//Delete detail
					DataSet dstData = dsDetail.List(((SO_CustomerItemRefMasterVO) pobjMaster).PartyID, ((SO_CustomerItemRefMasterVO) pobjMaster).CCNID);
					foreach (DataRow drowDetail in dstData.Tables[0].Rows)
					{
						if (drowDetail.RowState != DataRowState.Deleted)
						{
							drowDetail.Delete();
						}
					}
					dsDetail.UpdateDataSet(dstData);
				}
				//Delete Master
				dsMaster.Delete(((SO_CustomerItemRefMasterVO) pobjMaster).CustomerItemRefMasterID);
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

	
		public object GetObjectMasterByID(int pintPartyID, int pintCCNID)
		{
			try
			{
				return new SO_CustomerItemRefMasterDS().GetObjectVO(pintPartyID, pintCCNID);
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
	} // end of class
}
