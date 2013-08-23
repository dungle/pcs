using System;
using System.Data;
//PCS namespaces
using PCSComUtils.Common;
using PCSComUtils.Admin.DS;
using PCSComUtils.PCSExc;

namespace PCSComUtils.Admin.BO
{
	/// <summary>
	/// Summary description for ManageRoleBO.
	/// </summary>
	public class ManageRoleBO
	{
		public ManageRoleBO()
		{			
		}
		
		/// <summary>
		/// Return a list of role
		/// </summary>
		/// <returns></returns>
		/// <Author>Thien HD, Jan-07-2005 </Author>
	
		public DataSet List()
		{
			try
			{
				Sys_RoleDS objSysRoleDS = new Sys_RoleDS();
				return objSysRoleDS.List();
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
		/// Return a list of Field Length
		/// </summary>
		/// <returns>DataRow</returns>
		/// <Author>Thien HD, Jan-07-2005 </Author>
	
		public DataRow GetFieldLength()
		{
			try
			{
				Sys_RoleDS objSysRoleDS = new Sys_RoleDS();
				return objSysRoleDS.GetFieldLength();
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

		#region IObjectBO Members

		public void Add(object pObjectDetail)
		{
			// TODO:  Add ManageRoleBO.Add implementation
		}

		public void Delete(object pObjectVO)
		{
			// TODO:  Add ManageRoleBO.Delete implementation
		}

		public object GetObjectVO(int pintID, string VOclass)
		{
			// TODO:  Add ManageRoleBO.GetObjectVO implementation
			return null;
		}
		
		/// <summary>
		/// Update Dataset of table Sys_Role
		/// </summary>
		/// <param name="dstData">DataSet</param>
		/// <Author>Thien HD, Jan-07-2005 </Author>
	
		public void UpdateDataSet(DataSet dstData)
		{
			// TODO:  Add ManageRoleBO.UpdateDataSet implementation
			try
			{
				//Init the DS object
				Sys_RoleDS objSysRoleDs = new Sys_RoleDS();
				objSysRoleDs.UpdateDataSet(dstData);				
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
		/// Update Dataset of table Sys_Role after deleting related rights
		/// </summary>
		/// <param name="dstData"></param>
		/// <Author>Duong NA, Oct-10-2005 </Author>
	
		public void UpdateDataSetAndDelete(DataSet dstData, string pstrDeletedRoleIDs)
		{			
			const char CHR_SEPARATOR = ',';
			//const int DEFAULT_PERMISSION = 1;
			string strAddedRole = string.Empty;
			
			//Begin edit by duongna 10-10-2005 to add default menus to new role and delete all menus assigned to role
			//mark added role
			
			foreach (DataRow drRole in dstData.Tables[0].Rows)
			{
				if (drRole.RowState == DataRowState.Added)
				{
					strAddedRole += "'" + drRole[Sys_RoleTable.NAME_FLD].ToString().Replace("'","''") + "' ,";
				}
			}

			if (strAddedRole.EndsWith(CHR_SEPARATOR.ToString()))
			{
				strAddedRole = strAddedRole.Substring(0, strAddedRole.Length - 1);
			}

			//Delete all menu entries assigned to this role
			
			Sys_RightDS objRightDS = new Sys_RightDS();
			if(pstrDeletedRoleIDs.Length > 0)
			{
				pstrDeletedRoleIDs = pstrDeletedRoleIDs.Replace(';',',');
				objRightDS.DeleteRightsOfRole(pstrDeletedRoleIDs);
			}
			
			//End edit by duongna 10-10-2005
			
			//Init the DS object
			Sys_RoleDS objSysRoleDs = new Sys_RoleDS();
			objSysRoleDs.UpdateDataSet(dstData);

			//
			if(strAddedRole.Length > 0)
			{
				Sys_VisibilityGroup_RoleDS dsVisibility = new Sys_VisibilityGroup_RoleDS();
				dsVisibility.InsertDefaultVisibility(strAddedRole);
				objRightDS.InsertDefaultMenu(strAddedRole);
			}
			//Begin edit by duongna 10-10-2005

			#region // HACK: DEL SonHT 2005-12-09

			//Add default menus for added role
//			foreach (string strIndex in strAddedRole.Split(CHR_SEPARATOR))
//			{
//				try
//				{
//					nIndex = Convert.ToInt32(strIndex);
//				}
//				catch
//				{
//					continue;
//				}
//				if (dstData.Tables[0].Rows[nIndex].RowState == DataRowState.Unchanged)
//				{
//					//Get all default menu entry
//					Sys_Menu_EntryDS objSysMenuDS = new Sys_Menu_EntryDS();
//					Sys_RightDS objSysRight = new Sys_RightDS();
//					ArrayList arrDefaultMenus = objSysMenuDS.GetAllDefaultMenus();
//					//Do add default rights here
//					foreach (int nMenuID in arrDefaultMenus)
//					{
//						Sys_RightVO voRight = new Sys_RightVO();
//						voRight.Menu_EntryID = nMenuID;
//						voRight.Permission = DEFAULT_PERMISSION;
//						try
//						{
//							voRight.RoleID = Convert.ToInt32(dstData.Tables[0].Rows[nIndex][Sys_RoleTable.ROLEID_FLD]);
//						}
//						catch
//						{
//							continue;
//						}
//						objSysRight.Add(voRight);
//					}
//				}
//			}
			//End edit by duongna
			#endregion // END: DEL SonHT 2005-12-09
		}


		public void Update(object pObjectDetail)
		{
			// TODO:  Add ManageRoleBO.Update implementation
		}

		#endregion
	}
}