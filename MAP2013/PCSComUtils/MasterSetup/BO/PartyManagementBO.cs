using System.Data;

namespace PCSComUtils.MasterSetup.BO
{
	/// <summary>
	/// Summary description for LocationManagementBO.
	/// </summary>
	public class PartyManagementBO
	{
		public PartyManagementBO()
		{
			//
			// TODO: Add constructor logic here
			//
		}


		#region IObjectBO Members

		public void Add(object pObjectDetail)
		{
			// TODO:  Add PartyManagementBO.Add implementation
		}

		public void Delete(object pObjectVO)
		{
			// TODO:  Add PartyManagementBO.Delete implementation
		}

		public object GetObjectVO(int pintID, string VOclass)
		{
			// TODO:  Add PartyManagementBO.GetObjectVO implementation
			return null;
		}

		public void UpdateDataSet(DataSet dstData)
		{
			// TODO:  Add PartyManagementBO.UpdateDataSet implementation
		}

		public void Update(object pObjectDetail)
		{
			// TODO:  Add PartyManagementBO.Update implementation
		}
		
		public DataSet List()
		{
//			try 
//			{
//				MST_MasterLocationDS dsMasterLocation = new MST_MasterLocationDS();
//				return dsMasterLocation.List();
//			}
//			catch (PCSDBException ex) 
//			{
//				throw ex;
//			}
//			catch (Exception ex) 
//			{
//				throw ex;
//			}
			return null;
		}
		#endregion
	}
}
