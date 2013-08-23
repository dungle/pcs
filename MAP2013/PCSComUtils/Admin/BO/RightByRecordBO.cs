using System;
using System.Data;
using PCSComUtils.Admin.DS;

namespace PCSComUtils.Admin.BO
{
	public class RightByRecordBO
	{
		/// <summary>
		/// UpdateSecurityTable
		/// </summary>
		/// <param name="pdstSecurityTable"></param>
		/// <param name="pstrSecurityTableName"></param>
		/// <author>Trada</author>
		/// <date>Friday, Nov 18 2005</date>
	
		public void UpdateSecurityTable(DataSet pdstSecurityTable, string pstrSecurityTableName, string pstrSourceTableName)
		{
			sys_RolePartyDS dsRoleParty = new sys_RolePartyDS();
			dsRoleParty.UpdateSecurityTable(pdstSecurityTable, pstrSecurityTableName, pstrSourceTableName);
		}
		public void Update(object pObjectDetail)
		{
			throw new NotImplementedException();
		}
		/// <summary>
		/// list all roles 
		/// </summary>
		/// <returns>DataSet</returns>
		/// <author>Trada</author>
		/// <date>Wednesday, Nov 16 2005</date>
	
		public DataSet ListRole()
		{
			Sys_RoleDS dsSys_Role = new Sys_RoleDS();
			return dsSys_Role.List();
		}
		/// <summary>
		/// List all Types
		/// </summary>
		/// <returns></returns>
		/// <author>Trada</author>
		/// <date>Wednesday, Nov 16 2005</date>
	
		public DataSet ListType()
		{
			sys_RecordSecurityParamDS dssys_RecordSecurityParam = new sys_RecordSecurityParamDS();
			return dssys_RecordSecurityParam.List();
		}
		/// <summary>
		/// Get data from RoleParty/RoleProduct/RoleWC 
		/// </summary>
		/// <param name="pintRoleID"></param>
		/// <param name="pstrSecurityTableName"></param>
		/// <returns></returns>
		/// <author>Trada</author>
		/// <date>Thursday, Nov 17 2005</date>
	
		public DataSet GetRightByRecord(int pintRoleID, string pstrSecurityTableName, string pstrSourceTableName)
		{
			DataSet dstSecurityTable = new DataSet();
			sys_RolePartyDS dsRoleParty = new sys_RolePartyDS();
			dstSecurityTable = dsRoleParty.GetDataByRoleID(pintRoleID, pstrSecurityTableName, pstrSourceTableName);
			return dstSecurityTable;
		}
	}
}