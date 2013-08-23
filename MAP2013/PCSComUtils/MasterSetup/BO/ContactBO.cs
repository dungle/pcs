using System;

using PCSComUtils.MasterSetup.DS;
using PCSComUtils.PCSExc;

namespace PCSComUtils.MasterSetup.BO
{
	/// <summary>
	/// Summary description for ContactBO.
	/// </summary>
	public class ContactBO
	{
		public ContactBO()
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
		///       This method uses to add data to MST_PartyContact
		///    </Description>
		///    <Inputs>
		///        MST_PartyContactVO       
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
				MST_PartyContactDS dsParty = new MST_PartyContactDS();
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
		///       This method uses to delete MST_PartyContact
		///    </Description>
		///    <Inputs>
		///        MST_PartyContactVO       
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
				MST_PartyContactVO voParty = (MST_PartyContactVO)pObjectVO;
				// finally delete PartyContact
				MST_PartyContactDS dsParty = new MST_PartyContactDS();
				dsParty.Delete(voParty.PartyContactID);
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
		///       This method uses to get MST_PartyContactVO
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
				MST_PartyContactDS dsParty = new MST_PartyContactDS();
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
		///       This method uses to get MST_PartyContactVO by Code
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
	
		public object GetObjectVO(string pstrContactCode)
		{
			try
			{
				MST_PartyContactDS dsParty = new MST_PartyContactDS();
				return dsParty.GetObjectVO(pstrContactCode);
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
				MST_PartyContactDS dsParty = new MST_PartyContactDS();
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
		///       This method uses to update MST_PartyContactVO
		///    </Description>
		///    <Inputs>
		///        MST_PartyContactVO       
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
				MST_PartyContactDS dsParty = new MST_PartyContactDS();
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

		#endregion
	}
}
