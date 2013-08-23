using System;
using System.Collections;
using System.Data;
using PCSComProduct.Items.DS;
using PCSComUtils.MasterSetup.DS;
using PCSComUtils.PCSExc;


using PCSComUtils.Common;

using PCSComProcurement.Purchase.DS;

namespace PCSComProcurement.Purchase.BO
{
	public interface IPOItemVendorCrossReferenceBO 
	{
		DataSet List(int pintVendorID, int pintVedorLocID, int pintCCNID);
		object GetPartyInfo(int pintPartyID);
		DataTable GetAllLocation(int pintPartyID);
		DataTable GetRows(string pstrTableName, string pstrExpression);
		object GetProductInfo(int pintProductID);
		object GetObjectVO(int pintPartyID, int pintProductID);
		int AddAndReturnID(object pObjectDetail);
		void DeleteItemVendor(int pintPartyID, int pintProductID);
	}
	/// <summary>
	/// Summary description for .
	/// </summary>
	
	
	public class POItemVendorCrossReferenceBO :IPOItemVendorCrossReferenceBO
	{
		private const string THIS = "PCSComProcurement.Purchase.BO.POItemVendorCrossReferenceBO";
		public POItemVendorCrossReferenceBO()
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
	
		//**************************************************************************              
		///    <Description>
		///     update object
		///    </Description>
		///    <Inputs>
		///        object
		///    </Inputs>
		///    <Outputs>
		///    
		///    </Outputs>
		///    <Returns>
		///       N/A
		///    </Returns>
		///    <Authors>
		///       DungLA
		///    </Authors>
		///    <History>
		///    05-Apr-2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
	
		public void Update(object pObjectDetail)
		{
			try
			{
				PO_ItemVendorReferenceDS dsItemVendor = new PO_ItemVendorReferenceDS();
				dsItemVendor.Update(pObjectDetail);
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
	
		public void UpdateDataSet(DataSet dstData)
		{
			// TODO:  

		}

		//**************************************************************************              
		///    <Description>
		///     list all record in ItemVedorCrossReference table by VendorID, VendorLocationID
		///    </Description>
		///    <Inputs>
		///        VendorID, VendorLocID, CCNID
		///    </Inputs>
		///    <Outputs>
		///    
		///    </Outputs>
		///    <Returns>
		///       DataSet
		///    </Returns>
		///    <Authors>
		///       DungLA
		///    </Authors>
		///    <History>
		///    04-Apr-2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
	
		public DataSet List(int pintVendorID, int pintVedorLocID, int pintCCNID)
		{
			try
			{
				PO_ItemVendorReferenceDS dsVendorCross = new PO_ItemVendorReferenceDS();
				return dsVendorCross.List(pintVendorID, pintVedorLocID, pintCCNID);
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
		///     list Party Information
		///    </Description>
		///    <Inputs>
		///        VendorID
		///    </Inputs>
		///    <Outputs>
		///    
		///    </Outputs>
		///    <Returns>
		///       object
		///    </Returns>
		///    <Authors>
		///       DungLA
		///    </Authors>
		///    <History>
		///    04-Apr-2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
	
		public object GetPartyInfo(int pintPartyID)
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
		///     list all location of specified Party
		///    </Description>
		///    <Inputs>
		///        PartyID
		///    </Inputs>
		///    <Outputs>
		///    
		///    </Outputs>
		///    <Returns>
		///       DataTable
		///    </Returns>
		///    <Authors>
		///       DungLA
		///    </Authors>
		///    <History>
		///    04-Apr-2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
	
		public DataTable GetAllLocation(int pintPartyID)
		{
			try
			{
				MST_PartyLocationDS dsPartyLocation = new MST_PartyLocationDS();
				return dsPartyLocation.ListPartyLocation(pintPartyID).Tables[0];
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
		///       This method uses to get ITM_ProductVO object
		///    </Description>
		///    <Inputs>
		///        int Id
		///    </Inputs>
		///    <Outputs>
		///       
		///    </Outputs>
		///    <Returns>
		///       ITM_ProductVO
		///    </Returns>
		///    <Authors>
		///       DungLa
		///    </Authors>
		///    <History>
		///       10-Mar-2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
	
		public object GetProductInfo(int pintProductID)
		{
			try
			{
				ITM_ProductDS dsProduct = new ITM_ProductDS();
				return dsProduct.GetProductInfo(pintProductID);
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
		///       This method uses to get ItemVendorCrossReference object
		///    </Description>
		///    <Inputs>
		///        PartyID, ProductID
		///    </Inputs>
		///    <Outputs>
		///       
		///    </Outputs>
		///    <Returns>
		///       object
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
	
		public object GetObjectVO(int pintPartyID, int pintProductID)
		{
			try
			{
				PO_ItemVendorReferenceDS dsItemVendor = new PO_ItemVendorReferenceDS();
				return dsItemVendor.GetObjectVO(pintPartyID, pintProductID);
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
		///       This method uses to add new object and return new ID
		///    </Description>
		///    <Inputs>
		///        object
		///    </Inputs>
		///    <Outputs>
		///       
		///    </Outputs>
		///    <Returns>
		///       new ID
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
	
		public int AddAndReturnID(object pObjectDetail)
		{
			try
			{
				PO_ItemVendorReferenceDS dsItemVendor = new PO_ItemVendorReferenceDS();
				return dsItemVendor.AddAndReturnID(pObjectDetail);
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
		///       This method is used to get the DataTable result
		///       from a specific table with a specific search field key
		///       This function will be used on form that need to search for ID from Code, Name
		///       such as Product Code, or Product description
		///    </Description>
		///    <Inputs>
		///        TableName, Expression
		///    </Inputs>
		///    <Outputs>
		///       
		///    </Outputs>
		///    <Returns>
		///       DataTable
		///    </Returns>
		///    <Authors>
		///       DungLa
		///    </Authors>
		///    <History>
		///       07-Apr-2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
	
		public DataTable GetRows(string pstrTableName, string pstrExpression)
		{
			try
			{
				PO_ItemVendorReferenceDS dsItemVendor = new PO_ItemVendorReferenceDS();
				return dsItemVendor.GetRows(pstrTableName, pstrExpression);
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

	
		public void DeleteItemVendor(int pintPartyID, int pintProductID)
		{
			try 
			{
				PO_ItemVendorReferenceDS objPO_ItemVendorReferenceDS = new PO_ItemVendorReferenceDS();
				objPO_ItemVendorReferenceDS.DeleteItemVendor(pintPartyID,pintProductID);
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
