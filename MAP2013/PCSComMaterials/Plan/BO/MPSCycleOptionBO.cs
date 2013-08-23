using System;
using System.Collections;
using System.Data;
using PCSComUtils.PCSExc;


using PCSComUtils.Common;
using PCSComMaterials.Plan.DS;


namespace PCSComMaterials.Plan.BO
{
	public interface IMPSCycleOptionBO
	{
		int Add(DataSet pdstDetailData, object pobjMasterData);
		void UpdateMasterAndDetail(DataSet pdstDetailData, object pobjMasterData);
		DataSet GetDetailByMasterID(int pintCycleOptionMasterID);
		int DeleteCycleOptionMasterAndDetail(int pintCycleOptionMasterID, DataSet pdstData);
		object GetCycleOptionMaster(int pintMasterID);
	}
	/// <summary>
	/// Summary description for MPSCycleOptionBO.
	/// </summary>
	
	
	public class MPSCycleOptionBO : IMPSCycleOptionBO
	{
		public MPSCycleOptionBO()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		#region IObjectBO Members
	
		public void UpdateDataSet(System.Data.DataSet dstData)
		{
			// TODO:  Add MPSCycleOptionBO.UpdateDataSet implementation
		}
	
		public void Update(object pObjectDetail)
		{
			// TODO:  Add MPSCycleOptionBO.Update implementation
		}
		/// <summary>
		/// UpdateMasterAndDetail
		/// </summary>
		/// <param name="pdstDetailData"></param>
		/// <param name="pobjMasterData"></param>
		/// <author>Trada</author>
		/// <date>Wednesday, August 10 2005</date>
	
		public void UpdateMasterAndDetail(DataSet pdstDetailData, object pobjMasterData)
		{
			try
			{
				//Update Master
				MTR_MPSCycleOptionMasterVO objObject = (MTR_MPSCycleOptionMasterVO) pobjMasterData;
				MTR_MPSCycleOptionMasterDS dsMTR_MPSCycleOptionMaster = new MTR_MPSCycleOptionMasterDS();
				dsMTR_MPSCycleOptionMaster.Update(pobjMasterData);
				//Update Detail
				foreach (DataRow drow in pdstDetailData.Tables[0].Rows)
				{
					if (drow.RowState == DataRowState.Added)
					{
						drow[MTR_MPSCycleOptionDetailTable.MPSCYCLEOPTIONMASTERID_FLD] = objObject.MPSCycleOptionMasterID;
					}
				}
				MTR_MPSCycleOptionDetailDS dsMTR_MPSCycleOptionDetail = new MTR_MPSCycleOptionDetailDS();
				dsMTR_MPSCycleOptionDetail.UpdateDataSet(pdstDetailData);

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
		/// GetDetailByMasterID
		/// </summary>
		/// <param name="pintCycleOptionMasterID"></param>
		/// <author>Trada</author>
		/// <date>Wednesday, August 10 2005</date>
	
		public DataSet GetDetailByMasterID(int pintCycleOptionMasterID)
		{
			try
			{
				MTR_MPSCycleOptionDetailDS dsMTR_MPSCycleOptionDetail = new MTR_MPSCycleOptionDetailDS();
				DataSet dstMTR_MPSCycleOptionDetail = new DataSet();
				dstMTR_MPSCycleOptionDetail = dsMTR_MPSCycleOptionDetail.List(pintCycleOptionMasterID);
				return dstMTR_MPSCycleOptionDetail;
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
		/// Insert a new record into database
		/// </summary>
	
		public void Add(object pObjectDetail)
		{
			throw new NotImplementedException();
		}
	
		public void Delete(object pObjectVO)
		{
			// TODO:  Add MPSCycleOptionBO.Delete implementation
		}
		/// <summary>
		/// DeleteCycleOptionMasterAndDetail
		/// </summary>
		/// <param name="pintCycleOptionMasterID"></param>
		/// <param name="pdstData"></param>
		/// <author>Trada</author>
		/// <date>Thursday, August 11 2005</date>
	
		public int DeleteCycleOptionMasterAndDetail(int pintCycleOptionMasterID, DataSet pdstData)
		{
			try
			{
				const int ONE = 1;
				const int DCP = 1;
				const int MRP = 2;
				const int DCPOPTION = 3;
				int intRowCount;
				//Check MTR_CPO
				MTR_CPODS dsMTR_CPO = new MTR_CPODS();
				intRowCount = dsMTR_CPO.CheckDCPResult(pintCycleOptionMasterID, ONE);
				if (intRowCount != 0)
				{
					return DCP;
				}
				MTR_MPSCycleOptionMasterDS dsMTR_MPSCycleOptionMaster = new MTR_MPSCycleOptionMasterDS();
				//Check DCPOption Result
				intRowCount = dsMTR_MPSCycleOptionMaster.CheckDCPOption(pintCycleOptionMasterID);
				if (intRowCount != 0)
				{
					return DCPOPTION;
				}
				//Check MRP
				intRowCount = dsMTR_MPSCycleOptionMaster.CheckMRP(pintCycleOptionMasterID);
				if (intRowCount != 0)
				{
					return MRP;
				}
				else
				{
					//Delete MTR_CPO
					dsMTR_CPO.DeleteByMPSCycleOptionMasterID(pintCycleOptionMasterID);
					foreach (DataRow drow in pdstData.Tables[0].Rows)
					{
						if (drow.RowState != DataRowState.Deleted)
						{
							drow.Delete();
						}
					}
					//Delete Detail
					MTR_MPSCycleOptionDetailDS dsMTR_MPSCycleOptionDetail = new MTR_MPSCycleOptionDetailDS();
					dsMTR_MPSCycleOptionDetail.UpdateDataSet(pdstData);
					//Delete Master
					dsMTR_MPSCycleOptionMaster.Delete(pintCycleOptionMasterID);
				}
				
				return 0;
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
		/// Add
		/// </summary>
		/// <param name="pdstDetailData"></param>
		/// <param name="pobjMasterData"></param>
		/// <author>Trada</author>
		/// <date>Wednesday, August 10 2005</date>
	
		public int Add(DataSet pdstDetailData, object pobjMasterData)
		{
			try
			{
				//Add and Return ID
				MTR_MPSCycleOptionMasterDS dsMTR_MPSCycleOptionMaster = new MTR_MPSCycleOptionMasterDS();
				int pintMasterID = dsMTR_MPSCycleOptionMaster.AddAndReturnID(pobjMasterData);	
				foreach (DataRow drow in pdstDetailData.Tables[0].Rows)
				{
					drow[MTR_MPSCycleOptionDetailTable.MPSCYCLEOPTIONMASTERID_FLD] = pintMasterID;
				}
				//Update DataSet
				MTR_MPSCycleOptionDetailDS dsMTR_MPSCycleOptionDetail = new MTR_MPSCycleOptionDetailDS();
				dsMTR_MPSCycleOptionDetail.UpdateDataSet(pdstDetailData);
				return pintMasterID;
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
		/// GetCycleOptionMaster
		/// </summary>
		/// <param name="pintMasterID"></param>
		/// <returns></returns>
	
		public object GetCycleOptionMaster(int pintMasterID)
		{
			try
			{	
				MTR_MPSCycleOptionMasterDS dsMTR_MPSCycleOptionMaster = new MTR_MPSCycleOptionMasterDS();
				return dsMTR_MPSCycleOptionMaster.GetObjectVO(pintMasterID);
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

	
		public object GetObjectVO(int pintID, string VOclass)
		{
			// TODO:  Add MPSCycleOptionBO.GetObjectVO implementation
			return null;
		}
		
		#endregion
	}
}
