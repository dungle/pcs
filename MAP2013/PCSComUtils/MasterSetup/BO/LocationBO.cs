using System;
using System.Data;

using PCSComUtils.MasterSetup.DS;
using PCSComUtils.PCSExc;

namespace PCSComUtils.MasterSetup.BO
{	
	public class LocationBO
	{
		public LocationBO()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		#region IObjectBO Members

		public void Add(object pObjectDetail)
		{
			// TODO:  Add LocationBO.Add implementation
		}

		//**************************************************************************              
		///    <Description>
		///       This method uses to add data to MST_PartyLocation
		///    </Description>
		///    <Inputs>
		///        MST_PartyLocationVO       
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
				MST_PartyLocationDS dsLocation = new MST_PartyLocationDS();
				return dsLocation.AddReturnID(pObjectDetail);
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
		///       This method uses to delete MST_PartyLocation
		///    </Description>
		///    <Inputs>
		///        MST_PartyLocationVO       
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
				MST_PartyLocationVO voLocation = (MST_PartyLocationVO)pObjectVO;
				// delete all contact first
				MST_PartyContactDS dsContact = new MST_PartyContactDS();
				dsContact.DeleteByLocation(voLocation.PartyLocationID);
				// finally delete Location
				MST_PartyLocationDS dsLocation = new MST_PartyLocationDS();
				dsLocation.Delete(voLocation.PartyLocationID);
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
		///       This method uses to get MST_PartyLocationVO
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
				MST_PartyLocationDS dsLocation = new MST_PartyLocationDS();
				return dsLocation.GetObjectVO(pintID);
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
		///       This method uses to get MST_PartyLocationVO by Code
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
	
		public object GetObjectVO(string pstrLocationCode)
		{
			try
			{
				MST_PartyLocationDS dsLocation = new MST_PartyLocationDS();
				return dsLocation.GetObjectVO(pstrLocationCode);
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
				MST_PartyLocationDS dsLocation = new MST_PartyLocationDS();
				dsLocation.UpdateDataSet(dstData);
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
		///       This method uses to update MST_PartyLocationVO
		///    </Description>
		///    <Inputs>
		///        MST_PartyLocationVO       
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
				MST_PartyLocationDS dsLocation = new MST_PartyLocationDS();
				dsLocation.Update(pObjectDetail);
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

		/// <summary>
		/// Get Location Info for selecting (in In-Out Stock Report)
		/// </summary>
		/// <param name="pintMasterLocationID"></param>
		/// <returns></returns>
		/// <author> Tuan TQ. 16 Jan, 2006</author>
	
		public DataTable GetByLocation4Selecting(int pintMasterLocationID, string pstrOtherCondition)
		{
			MST_LocationDS dsLocation = new MST_LocationDS();
			return dsLocation.GetByLocation4Selecting(pintMasterLocationID, pstrOtherCondition);			
		}

		#endregion
	}
}
