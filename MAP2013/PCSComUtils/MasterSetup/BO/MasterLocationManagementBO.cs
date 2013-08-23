using System;
using System.Data;
using PCSComUtils.MasterSetup.DS;
using PCSComUtils.PCSExc;

namespace PCSComUtils.MasterSetup.BO
{
	/// <summary>
	/// Summary description for LocationManagementBO.
	/// </summary>
	public class MasterLocationManagementBO
	{
		public MasterLocationManagementBO()
		{
			//
			// TODO: Add constructor logic here
			//
		}


		#region IObjectBO Members

		public void Add(object pObjectDetail)
		{
			// TODO:  Add GrantRoleUserBO.Add implementation
		}

		public void Delete(object pObjectVO)
		{
			// TODO:  Add GrantRoleUserBO.Delete implementation
		}

		public object GetObjectVO(int pintID, string VOclass)
		{
			// TODO:  Add GrantRoleUserBO.GetObjectVO implementation
			return null;
		}

		public void UpdateDataSet(DataSet dstData)
		{
			// TODO:  Add GrantRoleUserBO.UpdateDataSet implementation
		}

		public void Update(object pObjectDetail)
		{
			// TODO:  Add GrantRoleUserBO.Update implementation
		}
		
		public DataSet List()
		{
			try 
			{
				MST_MasterLocationDS dsMasterLocation = new MST_MasterLocationDS();
				return dsMasterLocation.List();
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
		#endregion
	}
}
