using System;
using System.Data;

using PCSComUtils.MasterSetup.DS;
using PCSComUtils.PCSExc;

namespace PCSComUtils.MasterSetup.BO
{
	/// <summary>
	/// Summary description for PartyBO.
	/// </summary>
	public class PartyBO
	{
		public PartyBO()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		#region IObjectBO Members

		public void Add(object pObjectDetail)
		{
			// TODO:  Add PartyBO.Add implementation
		}

		//**************************************************************************              
		///    <Description>
		///       This method uses to add data to MST_Party
		///    </Description>
		///    <Inputs>
		///        MST_PartyVO       
		///    </Inputs>
		///    <Outputs>
		///       newly inserted primarkey value
		///    </Outputs>
		///    <Returns>
		///       int
		///    </Returns>
		///    <Authors>
		///       DungLa
		///    </Authors>
		///    <History>
		///       04-Mar-2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
	
		public int AddReturnID(object pObjectDetail)
		{
			try
			{
				MST_PartyDS dsParty = new MST_PartyDS();
				return dsParty.AddReturnID(pObjectDetail);
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
		///       This method uses to delete MST_Party
		///    </Description>
		///    <Inputs>
		///        MST_PartyVO       
		///    </Inputs>
		///    <Outputs>
		///       N/A
		///    </Outputs>
		///    <Returns>
		///       N/A
		///    </Returns>
		///    <Authors>
		///       DungLa
		///    </Authors>
		///    <History>
		///       04-Mar-2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
	
		public void Delete(object pObjectVO)
		{
			try
			{
				MST_PartyVO voParty = (MST_PartyVO)pObjectVO;
				// delete all contact first
				MST_PartyContactDS dsContact = new MST_PartyContactDS();
				dsContact.DeleteByParty(voParty.PartyID);
				// then delete all location
				MST_PartyLocationDS dsLocation = new MST_PartyLocationDS();
				dsLocation.DeleteByParty(voParty.PartyID);
				// finally delete Party
				MST_PartyDS dsParty = new MST_PartyDS();
				dsParty.Delete(voParty.PartyID);
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
		///       This method uses to get MST_PartyVO
		///    </Description>
		///    <Inputs>
		///        ID
		///    </Inputs>
		///    <Outputs>
		///       object
		///    </Outputs>
		///    <Returns>
		///       object
		///    </Returns>
		///    <Authors>
		///       DungLa
		///    </Authors>
		///    <History>
		///       04-Mar-2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
	
		public object GetObjectVO(int pintID, string VOclass)
		{
			try
			{
				MST_PartyDS dsParty = new MST_PartyDS();
				return dsParty.GetObjectVO(pintID);
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
		///       This method uses to get MST_PartyVO by Code
		///    </Description>
		///    <Inputs>
		///        ID
		///    </Inputs>
		///    <Outputs>
		///       object
		///    </Outputs>
		///    <Returns>
		///       object
		///    </Returns>
		///    <Authors>
		///       DungLa
		///    </Authors>
		///    <History>
		///       04-Mar-2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
	
		public object GetObjectVO(string pstrPartyCode)
		{
			try
			{
				MST_PartyDS dsParty = new MST_PartyDS();
				return dsParty.GetObjectVO(pstrPartyCode);
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
		///       This method uses to update a dataset
		///    </Description>
		///    <Inputs>
		///        DataSet to update
		///    </Inputs>
		///    <Outputs>
		///       N/A
		///    </Outputs>
		///    <Returns>
		///       N/A
		///    </Returns>
		///    <Authors>
		///       DungLa
		///    </Authors>
		///    <History>
		///       04-Mar-2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
	
		public void UpdateDataSet(System.Data.DataSet dstData)
		{
			try
			{
				MST_PartyDS dsParty = new MST_PartyDS();
				dsParty.UpdateDataSet(dstData);
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
		///       This method uses to update MST_PartyVO
		///    </Description>
		///    <Inputs>
		///        MST_PartyVO       
		///    </Inputs>
		///    <Outputs>
		///       N/A
		///    </Outputs>
		///    <Returns>
		///       N/A
		///    </Returns>
		///    <Authors>
		///       DungLa
		///    </Authors>
		///    <History>
		///       04-Mar-2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
	
		public void Update(object pObjectDetail)
		{
			try
			{
				MST_PartyDS dsParty = new MST_PartyDS();
				dsParty.Update(pObjectDetail);
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
		
	
		public DataTable GetPartyType()
		{
			MST_PartyDS dsParty = new MST_PartyDS();
			return dsParty.GetPartyType();
		}

		#endregion

	
		public DataTable ListVendor()
		{
			MST_PartyDS dsParty = new MST_PartyDS();
			return dsParty.ListVendor();
		}
	}
}
