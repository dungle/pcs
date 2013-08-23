using System;
using System.Data;


using PCSComProcurement.Purchase.DS;
using PCSComUtils.Common;

using PCSComUtils.MasterSetup.DS;
using PCSComUtils.PCSExc;

namespace PCSComProcurement.Purchase.BO
{
	
	public interface IPOAdditionChargesBO 
	{
		DataSet ListByPOMaster(int pintPOMasterID);
		DataTable ListPODetailByPOMaster(int pintPOMasterID);
		object GetCustomerInfo(int pintPartyID);
		bool IsChargeByQuantity(int pintSOMasterID);
		decimal GetAddChargeVAT(int pintAddChargeID);
		object GetPurchaseOrderDetailVO(int pintPODetailID);
		object GetPOMasterVO(int pintPOMasterID);
		decimal GetOrderQuantity(int pintPODetailID);
		DataSet GetAdditionalChargeByPOMasterID(int pintPOMasterID);
		DataSet GetDataByPOMasterID(int pintPOMasterID);
		bool AlreadyCharged(int pintPOMasterID);
		object GetAddCharge(int pintAddChargeID);
		object GetReasonVO(int pintReasonID);
	}
	/// <summary>
	/// Summary description for .
	/// </summary>
	
	public class POAdditionChargesBO :IPOAdditionChargesBO
	{
		private const string THIS = "PCSComProcurement.Purchase.BO.POAdditionChargesBO";
		public POAdditionChargesBO()
		{
			//
			// TODO: Add constructor logic here
			//
		}

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
	
		//**************************************************************************              
		///    <Description>
		///       This method uses to Update a DataSet
		///    </Description>
		///    <Inputs>
		///        DataSet
		///    </Inputs>
		///    <Outputs>
		///       
		///    </Outputs>
		///    <Returns>
		///       
		///    </Returns>
		///    <Authors>
		///       DungLa
		///    </Authors>
		///    <History>
		///       07-Mar-2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
	
		public void UpdateDataSet(DataSet dstData)
		{
			try
			{
				PO_AdditionChargesDS dsPOAddCharge = new PO_AdditionChargesDS();
				dsPOAddCharge.UpdateDataSet(dstData);
			}
			catch (PCSException ex)
			{
				throw ex;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		//**************************************************************************              
		///    <Description>
		///       This method uses to get all POAdditionCharge by PO Master ID
		///    </Description>
		///    <Inputs>
		///        int
		///    </Inputs>
		///    <Outputs>
		///       DataSet
		///    </Outputs>
		///    <Returns>
		///       DataSet
		///    </Returns>
		///    <Authors>
		///       DungLa
		///    </Authors>
		///    <History>
		///       07-Mar-2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
	
		public DataSet ListByPOMaster(int pintPOMasterID)
		{
			try
			{
				PO_AdditionChargesDS dsPOAddCharge = new PO_AdditionChargesDS();
				return dsPOAddCharge.List(pintPOMasterID);
			}
			catch (PCSException ex)
			{
				throw ex;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		//**************************************************************************              
		///    <Description>
		///       This method uses to get all po detail by po master
		///    </Description>
		///    <Inputs>
		///        int
		///    </Inputs>
		///    <Outputs>
		///       DataTable
		///    </Outputs>
		///    <Returns>
		///       DataTable
		///    </Returns>
		///    <Authors>
		///       DungLa
		///    </Authors>
		///    <History>
		///       17-Feb-2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
	
		public DataTable ListPODetailByPOMaster(int pintPOMasterID)
		{
			try
			{
				PO_AdditionChargesDS dsAddCharge = new PO_AdditionChargesDS();
				return dsAddCharge.GetPODetailByPOMaster(pintPOMasterID);
			}
			catch (PCSException ex)
			{
				throw ex;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		//**************************************************************************              
		///    <Description>
		///       This method uses to get customer infor from Id
		///    </Description>
		///    <Inputs>
		///        int
		///    </Inputs>
		///    <Outputs>
		///       MST_PartyVO
		///    </Outputs>
		///    <Returns>
		///       MST_PartyVO
		///    </Returns>
		///    <Authors>
		///       DungLa
		///    </Authors>
		///    <History>
		///       07-Mar-2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
	
		public object GetCustomerInfo(int pintPartyID)
		{
			try
			{
				MST_PartyDS dsParty = new MST_PartyDS();
				return dsParty.GetObjectVO(pintPartyID);
			}
			catch (PCSException ex)
			{
				throw ex;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		//**************************************************************************              
		///    <Description>
		///       This method uses to check UM of all products
		///    </Description>
		///    <Inputs>
		///        Sale Order Master ID (int)
		///    </Inputs>
		///    <Outputs>
		///       return true if all of product have same UM
		///       else return false
		///    </Outputs>
		///    <Returns>
		///       bool
		///    </Returns>
		///    <Authors>
		///       DungLa
		///    </Authors>
		///    <History>
		///       07-Mar-2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
	
		public bool IsChargeByQuantity(int pintSOMasterID)
		{
			try
			{
				PO_AdditionChargesDS dsAddCharge = new PO_AdditionChargesDS();
				return dsAddCharge.IsChargeByQuantity(pintSOMasterID);
			}
			catch (PCSException ex)
			{
				throw ex;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		//**************************************************************************              
		///    <Description>
		///       This method uses to get VAT from AddCharge
		///    </Description>
		///    <Inputs>
		///        int
		///    </Inputs>
		///    <Outputs>
		///       decimal
		///    </Outputs>
		///    <Returns>
		///       
		///    </Returns>
		///    <Authors>
		///       DungLa
		///    </Authors>
		///    <History>
		///       07-Mar-2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
	
		public decimal GetAddChargeVAT(int pintAddChargeID)
		{
			try
			{
				MST_AddChargeDS dsAddCharge = new MST_AddChargeDS();
				return dsAddCharge.GetAddChargeVAT(pintAddChargeID);
			}
			catch (PCSException ex)
			{
				throw ex;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		//**************************************************************************              
		///    <Description>
		///       This method uses to get order quantity of a detail
		///    </Description>
		///    <Inputs>
		///        int
		///    </Inputs>
		///    <Outputs>
		///       decimal
		///    </Outputs>
		///    <Returns>
		///       
		///    </Returns>
		///    <Authors>
		///       DungLa
		///    </Authors>
		///    <History>
		///       05-Apr-2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
	
		public decimal GetOrderQuantity(int pintPODetailID)
		{
			try
			{
				PO_PurchaseOrderDetailDS dsDetail = new PO_PurchaseOrderDetailDS();
				return dsDetail.GetOrderQuantity(pintPODetailID);
			}
			catch (PCSException ex)
			{
				throw ex;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		//**************************************************************************              
		///    <Description>
		///       This method uses to get PODetailVO object from ID
		///    </Description>
		///    <Inputs>
		///        int
		///    </Inputs>
		///    <Outputs>
		///       PO_PurchaseOrderDetailVO
		///    </Outputs>
		///    <Returns>
		///       PO_PurchaseOrderDetailVO
		///    </Returns>
		///    <Authors>
		///       DungLa
		///    </Authors>
		///    <History>
		///       07-Mar-2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
	
		public object GetPurchaseOrderDetailVO(int pintPODetailID)
		{
			try
			{
				PO_PurchaseOrderDetailDS dsPODetail = new PO_PurchaseOrderDetailDS();
				return dsPODetail.GetObjectVO(pintPODetailID);
			}
			catch (PCSException ex)
			{
				throw ex;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		//**************************************************************************              
		///    <Description>
		///       This method uses to get PO_PurchaseOrderMasterVO object from ID
		///    </Description>
		///    <Inputs>
		///        int
		///    </Inputs>
		///    <Outputs>
		///       PO_PurchaseOrderMasterVO
		///    </Outputs>
		///    <Returns>
		///       PO_PurchaseOrderMasterVO
		///    </Returns>
		///    <Authors>
		///       DungLa
		///    </Authors>
		///    <History>
		///       07-Mar-2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
	
		public object GetPOMasterVO(int pintPOMasterID)
		{
			try
			{
				PO_PurchaseOrderMasterDS dsPOMaster = new PO_PurchaseOrderMasterDS();
				return dsPOMaster.GetObjectVO(pintPOMasterID);
			}
			catch (PCSException ex)
			{
				throw ex;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		//**************************************************************************              
		///    <Description>
		///       This method uses to get additional charge by PO Master
		///    </Description>
		///    <Inputs>
		///        int
		///    </Inputs>
		///    <Outputs>
		///       DataSet
		///    </Outputs>
		///    <Returns>
		///       DataSet
		///    </Returns>
		///    <Authors>
		///       DungLa
		///    </Authors>
		///    <History>
		///       17-Feb-2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
	
		public DataSet GetAdditionalChargeByPOMasterID(int pintPOMasterID)
		{
			try
			{
				PO_AdditionChargesDS dsAddCharge = new PO_AdditionChargesDS();
				MST_ReasonDS dsReason = new MST_ReasonDS();
				DataSet dstData = dsAddCharge.GetAdditionalChargeByPOMasterID(pintPOMasterID);
				// fill reason code if have id
				foreach (DataRow drowData in dstData.Tables[0].Rows)
				{
					if ((drowData[PO_AdditionChargesTable.REASONID_FLD] != null) && (drowData[PO_AdditionChargesTable.REASONID_FLD].ToString() != string.Empty)
						&& (drowData[PO_AdditionChargesTable.REASONID_FLD].ToString() != "0"))
					{
						string strReasonCode = dsReason.GetCodeFromID(int.Parse(drowData[SO_AdditionChargeTable.REASONID_FLD].ToString()));
						if ((strReasonCode != null) && (strReasonCode != string.Empty))
						{
							drowData[MST_ReasonTable.TABLE_NAME + MST_ReasonTable.CODE_FLD] = strReasonCode;
						}
					}
				}
				return dstData;
			}
			catch (PCSException ex)
			{
				throw ex;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		//**************************************************************************              
		///    <Description>
		///       This method uses to get data from PO detail by PO Master
		///    </Description>
		///    <Inputs>
		///        int
		///    </Inputs>
		///    <Outputs>
		///       DataSet
		///    </Outputs>
		///    <Returns>
		///       DataSet
		///    </Returns>
		///    <Authors>
		///       DungLa
		///    </Authors>
		///    <History>
		///       17-Feb-2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
	
		public DataSet GetDataByPOMasterID(int pintPOMasterID)
		{
			try
			{
				PO_AdditionChargesDS dsAddCharge = new PO_AdditionChargesDS();
				return dsAddCharge.GetDataByPOMasterID(pintPOMasterID);
			}
			catch (PCSException ex)
			{
				throw ex;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		//**************************************************************************              
		///    <Description>
		///       This method uses to check a PO Master was charged or not
		///    </Description>
		///    <Inputs>
		///        Purchase Order Master ID (int)
		///    </Inputs>
		///    <Outputs>
		///       return true if charged
		///       else return false
		///    </Outputs>
		///    <Returns>
		///       bool
		///    </Returns>
		///    <Authors>
		///       DungLa
		///    </Authors>
		///    <History>
		///       18-Feb-2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
	
		public bool AlreadyCharged(int pintPOMasterID)
		{
			try
			{
				PO_AdditionChargesDS dsAddCharge = new PO_AdditionChargesDS();
				return dsAddCharge.AlreadyCharged(pintPOMasterID);
			}
			catch (PCSException ex)
			{
				throw ex;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		//**************************************************************************              
		///    <Description>
		///       This method uses to get AddChargeVO
		///    </Description>
		///    <Inputs>
		///        int
		///    </Inputs>
		///    <Outputs>
		///       object
		///    </Outputs>
		///    <Returns>
		///       
		///    </Returns>
		///    <Authors>
		///       DungLa
		///    </Authors>
		///    <History>
		///       18-Feb-2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
	
		public object GetAddCharge(int pintAddChargeID)
		{
			try
			{
				MST_AddChargeDS dsAddCharge = new MST_AddChargeDS();
				return dsAddCharge.GetObjectVO(pintAddChargeID);
			}
			catch (PCSException ex)
			{
				throw ex;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		//**************************************************************************              
		///    <Description>
		///       This method uses to get ReasonVO
		///    </Description>
		///    <Inputs>
		///        int
		///    </Inputs>
		///    <Outputs>
		///       object
		///    </Outputs>
		///    <Returns>
		///       
		///    </Returns>
		///    <Authors>
		///       DungLa
		///    </Authors>
		///    <History>
		///       18-Feb-2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
	
		public object GetReasonVO(int pintReasonID)
		{
			try
			{
				MST_ReasonDS dsReason = new MST_ReasonDS();
				return dsReason.GetObjectVO(pintReasonID);
			}
			catch (PCSException ex)
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
