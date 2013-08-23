using System;
using System.Collections;
using System.Data;
using System.Text;
using PCSComMaterials.Inventory.DS;
using PCSComProduct.Items.DS;
using PCSComUtils.PCSExc;


using PCSComUtils.Common;
using PCSComMaterials.Plan.DS;


namespace PCSComMaterials.Plan.BO
{
	public interface IMRPCycleOptionBO
	{
		int Add(DataSet pdstDetailData, object pobjMasterData);
		void UpdateMasterAndDetail(DataSet pdstDetailData, object pobjMasterData);
		DataSet GetDetailByMasterID(int pintCycleOptionMasterID);
		void DeleteCycleOptionMasterAndDetail(int pintCCNID, int pintCycleOptionMasterID, DataSet pdstData);
		DataTable GetCycleOptionMaster(int pintMasterID);
	}
	/// <summary>
	/// Summary description for MRPCycleOptionBO.
	/// </summary>
	
	
	public class MRPCycleOptionBO : IMRPCycleOptionBO
	{
		public MRPCycleOptionBO()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		/// <summary>
		/// Add
		/// </summary>
		/// <param name="pdstDetailData"></param>
		/// <param name="pobjMasterData"></param>
		/// <returns></returns>
		/// <author>Trada</author>
		/// <date>Thursday, August 11 2005</date>
	
		public int Add(DataSet pdstDetailData, object pobjMasterData)
		{
			try
			{
				//Add and Return ID
				MTR_MRPCycleOptionMasterDS dsMTR_MRPCycleOptionMaster = new MTR_MRPCycleOptionMasterDS();
				int pintMasterID = dsMTR_MRPCycleOptionMaster.AddAndReturnID(pobjMasterData);	
				foreach (DataRow drow in pdstDetailData.Tables[0].Rows)
				{
					drow[MTR_MRPCycleOptionDetailTable.MRPCYCLEOPTIONMASTERID_FLD] = pintMasterID;
				}
				//Update DataSet
				MTR_MRPCycleOptionDetailDS dsMTR_MRPCycleOptionDetail = new MTR_MRPCycleOptionDetailDS();
				dsMTR_MRPCycleOptionDetail.UpdateDataSet(pdstDetailData);
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
		/// UpdateMasterAndDetail
		/// </summary>
		/// <param name="pdstDetailData"></param>
		/// <param name="pobjMasterData"></param>
		/// <author>Trada</author>
		/// <date>Friday, August 12 2005</date>
	
		public void UpdateMasterAndDetail(DataSet pdstDetailData, object pobjMasterData)
		{
			try
			{
				//Update Master
				MTR_MRPCycleOptionMasterVO objObject = (MTR_MRPCycleOptionMasterVO) pobjMasterData;
				MTR_MRPCycleOptionMasterDS dsMTR_MRPCycleOptionMaster = new MTR_MRPCycleOptionMasterDS();
				dsMTR_MRPCycleOptionMaster.Update(pobjMasterData);
				//Update Detail
				foreach (DataRow drow in pdstDetailData.Tables[0].Rows)
				{
					if (drow.RowState == DataRowState.Added)
					{
						drow[MTR_MRPCycleOptionDetailTable.MRPCYCLEOPTIONMASTERID_FLD] = objObject.MRPCycleOptionMasterID;
					}
				}
				MTR_MRPCycleOptionDetailDS dsMTR_MRPCycleOptionDetail = new MTR_MRPCycleOptionDetailDS();
				dsMTR_MRPCycleOptionDetail.UpdateDataSet(pdstDetailData);

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
		/// <returns></returns>
		/// <author>Trada</author>
		/// <date>Thursday, August 11 2005</date>
	
		public DataSet GetDetailByMasterID(int pintCycleOptionMasterID)
		{
			try
			{
				MTR_MRPCycleOptionDetailDS dsMTR_MRPCycleOptionDetail = new MTR_MRPCycleOptionDetailDS();
				DataSet dstMTR_MRPCycleOptionDetail = new DataSet();
				dstMTR_MRPCycleOptionDetail = dsMTR_MRPCycleOptionDetail.List(pintCycleOptionMasterID);
				return dstMTR_MRPCycleOptionDetail;
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
		/// DeleteCycleOptionMasterAndDetail
		/// </summary>
		/// <param name="pintCycleOptionMasterID"></param>
		/// <param name="pdstData"></param>
		/// <author>Trada</author>
		/// <date>Thursday, August 11 2005</date>
	
		public void DeleteCycleOptionMasterAndDetail(int pintCCNID,int pintCycleOptionMasterID, DataSet pdstData)
		{
			foreach (DataRow drow in pdstData.Tables[0].Rows)
			{
				if (drow.RowState != DataRowState.Deleted)
				{
					drow.Delete();
				}
			}
			//Delete Detail
			MTR_MRPCycleOptionDetailDS dsMTR_MRPCycleOptionDetail = new MTR_MRPCycleOptionDetailDS();
			dsMTR_MRPCycleOptionDetail.UpdateDataSet(pdstData);
			//Delete MTR_CPO
			MTR_CPODS dsMTR_CPO = new MTR_CPODS();
			dsMTR_CPO.Delete(pintCCNID, pintCycleOptionMasterID);
			//Delete Master
			MTR_MRPCycleOptionMasterDS dsMTR_MRPCycleOptionMaster = new MTR_MRPCycleOptionMasterDS();
			dsMTR_MRPCycleOptionMaster.Delete(pintCycleOptionMasterID);
		}
	
		public void DeleteMRPResult(int pintCCNID, int pintCycleOptionID)
		{
			//Delete MTR_CPO
			MTR_CPODS dsMTR_CPO = new MTR_CPODS();
			dsMTR_CPO.Delete(pintCCNID, pintCycleOptionID);
		}
		/// <summary>
		/// GetCycleOptionMaster
		/// </summary>
		/// <param name="pintMasterID"></param>
		/// <returns></returns>
		/// <author>Trada</author>
		/// <date>Friday, August 12 2005</date>
	
		public DataTable GetCycleOptionMaster(int pintMasterID)
		{
			try
			{	
				MTR_MRPCycleOptionMasterDS dsMTR_MRPCycleOptionMaster = new MTR_MRPCycleOptionMasterDS();
				return dsMTR_MRPCycleOptionMaster.GetMRPCycleOptionMaster(pintMasterID);
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
			throw new NotImplementedException();
		}

		/// <summary>
		/// Update into Database
		/// </summary>
		public void Update(object pObjectDetail)
		{
			throw new NotImplementedException();
		}
	
		public DataTable ListOutsideItem()
		{
			ITM_ProductDS dsProduct = new ITM_ProductDS();
			return dsProduct.ListOutsideItem();
		}
	
		public DataTable GetBeginMRP(DateTime pdtmAsOfDate)
		{
			IV_MasLocCacheDS dsMasLoc = new IV_MasLocCacheDS();
			return dsMasLoc.GetBeginQuantityOfNiguri(pdtmAsOfDate);
		}
	
		public DataTable GetAvailableQuantityForPlan(DateTime dtmDate)
		{
			IV_BalanceBinDS dsBin = new IV_BalanceBinDS();
			return dsBin.GetAvailableQuantityForPlan(dtmDate);
		}
	
		public DataTable getAllReleasePOForItems(StringBuilder pstrItemsID, DateTime pdtmFromDate, DateTime pdtmToDate, int pintMasLocID)
		{
			MRPRegenerationProcessDS dsProcess = new MRPRegenerationProcessDS();
			return dsProcess.getAllReleasePOForItems(pstrItemsID, pdtmFromDate, pdtmToDate, pintMasLocID);
		}
	
		public DataTable getAllReleaseSOForItems(StringBuilder pstrItemsID, DateTime pdtmFromDate, DateTime pdtmToDate, int pintMasLocID)
		{
			MRPRegenerationProcessDS dsProcess = new MRPRegenerationProcessDS();
			return dsProcess.getAllReleaseSOForItems(pstrItemsID, pdtmFromDate, pdtmToDate, pintMasLocID);
		}
	
		public DataTable getAllCPOForItems(StringBuilder pstrItemsID, DateTime pdtmFromDate, DateTime pdtmToDate, int pintMasLocID)
		{
			MRPRegenerationProcessDS dsProcess = new MRPRegenerationProcessDS();
			return dsProcess.getAllCPOForItems(pstrItemsID, pdtmFromDate, pdtmToDate, pintMasLocID);
		}
	
		public void UpdateBeginMRP(DataTable pdtbData)
		{
			MRPRegenerationProcessDS dsMRP = new MRPRegenerationProcessDS();
			dsMRP.UpdateBeginMRP(pdtbData);
		}
	}
}
