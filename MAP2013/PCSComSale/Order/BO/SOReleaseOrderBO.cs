using System;
using System.Collections;
using System.Data;


using PCSComMaterials.Inventory.DS;
using PCSComSale.Order.DS;
using PCSComUtils.Common;
using PCSComUtils.Common.BO;

using PCSComUtils.MasterSetup.DS;
using PCSComUtils.PCSExc;

namespace PCSComSale.Order.BO
{
	public interface ISOReleaseOrderBO 
	{
	}
	/// <summary>
	/// Summary description for .
	/// </summary>
	
	
	public class SOReleaseOrderBO : ISOReleaseOrderBO 
	{
		private const string THIS = "PCSComSale.Order.BO.SOReleaseOrderBO";
		public SOReleaseOrderBO()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public void Add(object pObjectDetail)
		{
			// TODO:  Add SOReleaseOrderBO.Add implementation

		}
	
		public void Delete(object pObjectVO)
		{
			// TODO:  Add SOReleaseOrderBO.Delete implementation

		}
	
		public object GetObjectVO(int pintID, string VOclass)
		{
			// TODO:  Add SOReleaseOrderBO.GetObjectVO implementation
			return null;
		}
	
		public void Update(object pObjectDetail)
		{
			// TODO:  Add SOReleaseOrderBO.Update implementation

		}
	
		public void UpdateDataSet(DataSet dstData)
		{
			// TODO:  Add SOReleaseOrderBO.UpdateDataSet implementation

		}

	
		public DataSet List(int MasterID)
		{
			SO_CommitInventoryDetailDS dsCommit = new SO_CommitInventoryDetailDS();
			return dsCommit.List(MasterID);
		}
		//**************************************************************************              
		///    <Description>
		///       This method uses to 
		///    </Description>
		///    <Inputs>
		///            
		///    </Inputs>
		///    <Outputs>
		///       
		///    </Outputs>
		///    <Returns>
		///       
		///    </Returns>
		///    <Authors>
		///       Hoang Trung Son - iSphere software
		///    </Authors>
		///    <History>
		///       Wednesday, March 09, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
	
		public DataTable Search(int pintCCNID, DateTime pdtmFromDeliveryDate, DateTime pdtmToDeliveryDate,
			string pstrOrderNo, string pstrItem,string pstrRevision,string pstrDescription,
			int pintLocationID, int pintBinID)
		{
			SO_SaleOrderDetailDS dsSale = new SO_SaleOrderDetailDS();
			return dsSale.Search(pintCCNID, pdtmFromDeliveryDate, pdtmToDeliveryDate,
				pstrOrderNo, pstrItem,pstrRevision, pstrDescription, pintLocationID, pintBinID);
		}

	
		public void UpdateRelease(ArrayList parrMaster, DataSet pdstCommitDetail, DataSet pdstMasLocCache,
			DataSet pdstLocCache, DataSet pdstBinCache, DataSet pdstTransaction)
		{
			const string METHOD_NAME = THIS + ".UpdateRelease()";
			
			SO_CommitInventoryMasterDS dsMaster = new SO_CommitInventoryMasterDS();
			DataSet dstCopyOfDetail = pdstCommitDetail.Clone();
			DataSet dstCopyOfTransaction = pdstTransaction.Clone();
			UtilsBO boUtils = new UtilsBO();
			foreach (SO_CommitInventoryMasterVO voMaster in parrMaster)
			{
				int intOldID = voMaster.CommitInventoryMasterID;
				voMaster.CommitmentNo = boUtils.GetNoByMask(SO_CommitInventoryMasterTable.TABLE_NAME,
					SO_CommitInventoryMasterTable.COMMITMENTNO_FLD, voMaster.CommitDate, Constants.YYYYMMDD0000);
				// add new master object and get new id
				voMaster.CommitInventoryMasterID = dsMaster.AddAndReturnID(voMaster);

				#region find all detail object to assign new master id

				DataRow[] drowDetails = pdstCommitDetail.Tables[0].Select(SO_CommitInventoryDetailTable.COMMITINVENTORYMASTERID_FLD + "=" + intOldID);
				foreach (DataRow drowDetail in drowDetails)
				{
					if (drowDetail.RowState != DataRowState.Deleted)
					{
						DataRow drowCommitDetail = dstCopyOfDetail.Tables[0].NewRow();
						foreach (DataColumn dcol in dstCopyOfDetail.Tables[0].Columns)
							drowCommitDetail[dcol.ColumnName] = drowDetail[dcol.ColumnName];
						drowCommitDetail[SO_CommitInventoryDetailTable.COMMITINVENTORYMASTERID_FLD] = voMaster.CommitInventoryMasterID;
						dstCopyOfDetail.Tables[0].Rows.Add(drowCommitDetail);

					}
				}

				#endregion

				#region find all transaction history object to assign new master id

				DataRow[] drowTransaction = pdstTransaction.Tables[0].Select(MST_TransactionHistoryTable.REFMASTERID_FLD + "=" + intOldID);
				foreach (DataRow drowDetail in drowTransaction)
				{
					if (drowDetail.RowState != DataRowState.Deleted)
					{
						DataRow drowNewTransaction = dstCopyOfTransaction.Tables[0].NewRow();
						foreach (DataColumn dcol in dstCopyOfTransaction.Tables[0].Columns)
							drowNewTransaction[dcol.ColumnName] = drowDetail[dcol.ColumnName];
						drowNewTransaction[MST_TransactionHistoryTable.REFMASTERID_FLD] = voMaster.CommitInventoryMasterID;
						dstCopyOfTransaction.Tables[0].Rows.Add(drowNewTransaction);

					}
				}

				#endregion
			}

			// update detail dataset
			SO_CommitInventoryDetailDS dsCommitDetail = new SO_CommitInventoryDetailDS();
			dsCommitDetail.UpdateDataSet(dstCopyOfDetail);
			// update transaction history
			MST_TransactionHistoryDS dsTransaction = new MST_TransactionHistoryDS();
			dsTransaction.UpdateDataSet(dstCopyOfTransaction);
			// update bin cache
			IV_BinCacheDS dsBinCache = new IV_BinCacheDS();
			dsBinCache.UpdateDataSet(pdstBinCache);
			// update location cache
			IV_LocationCacheDS dsLocCache = new IV_LocationCacheDS();
			dsLocCache.UpdateDataSet(pdstLocCache);
			// update master location cache
			IV_MasLocCacheDS dsMasLocCache = new IV_MasLocCacheDS();
			dsMasLocCache.UpdateDataSet(pdstMasLocCache);
		}

	} // end of class
}
