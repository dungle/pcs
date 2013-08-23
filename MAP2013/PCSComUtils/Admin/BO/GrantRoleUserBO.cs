using System;
using System.Data;
using PCSComUtils.Admin.DS;
using PCSComUtils.PCSExc;
using PCSComUtils.Common;

namespace PCSComUtils.Admin.BO
{	
	/// <summary>
	/// Summary description for GrantRoleUserBO.
	/// </summary>
	/// 
	public class GrantRoleUserBO
	{
		public GrantRoleUserBO()
		{			
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

		#endregion

		#region IGrantRoleUserBO Members
		
		
		/// <summary>
		///  Get a list of users from database
		/// </summary>
		/// <returns></returns>
		/// <Author> Thien HD, Jan 07, 2005</Author>
	
		public DataSet ListUser()
		{			
			try 
			{
				Sys_UserDS objSysUserDs = new Sys_UserDS();
				return objSysUserDs.List();
			}
			catch (PCSDBException ex) 
			{
				throw ex;
			}
			catch (Exception ex) {
				throw ex;
			}
		}
		
		/// <summary>
		/// Get a list of all Role that doesn't grant to User
		/// </summary>
		/// <param name="intUserId"></param>
		/// <returns></returns>
		/// <Author> Thien HD, Jan 07, 2005</Author>
	
		public DataSet ListRoleNotGrantToUser(int intUserId)
		{			
			try 
			{
				Sys_RoleDS objSysRoleDs = new Sys_RoleDS();
				return objSysRoleDs.ListRoleNotGrantToUser(intUserId);
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
		/// Get a list of all Role that granted to User
		/// </summary>
		/// <param name="intUserId">User identity </param>
		/// <returns></returns>
		/// <Author> Thien HD, Jan 07, 2005</Author>
	
		public DataSet ListRoleGrantToUser(int intUserId)
		{			
			try 
			{
				Sys_RoleDS objSysRoleDs = new Sys_RoleDS();
				return objSysRoleDs.ListRoleGrantToUser(intUserId);
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

		#region IGrantRoleUserBO Members		
		
		/// <summary>
		/// Save a list of selected role into database
		/// Get a list of current role granted to this user from database in dataset
		/// and then use this dataset to input a new role 
		/// call the DS method to update this dataset
		/// </summary>
		/// <param name="pintUserID"></param>
		/// <param name="pdtListOfRole"></param>
		/// <Author> Thien HD, Jan 11, 2005</Author>
	
		public void SaveRoleToUser(int pintUserID, DataTable pdtListOfRole)
		{			
			try 
			{
				//Get the list of current granted role of this user.
				Sys_UserToRoleDS objSys_UserToRoleDS = new Sys_UserToRoleDS();
				DataSet dstCurrentRole = objSys_UserToRoleDS.ListRoleToUser(pintUserID);
				dstCurrentRole.Tables[0].Columns[Sys_UserToRoleTable.ID_FLD].AutoIncrement = true;

				//Get the highest value for this column
				dstCurrentRole.Tables[0].Columns[Sys_UserToRoleTable.ID_FLD].AutoIncrementSeed = objSys_UserToRoleDS.GetMaxID() +1;
				dstCurrentRole.Tables[0].Columns[Sys_UserToRoleTable.ID_FLD].AutoIncrementStep =1;


				for (int i=0; i<dstCurrentRole.Tables[0].Rows.Count;i++) 
				{
					dstCurrentRole.Tables[0].Rows[i].Delete();
				}
				pdtListOfRole.AcceptChanges();
				foreach (DataRow dr in pdtListOfRole.Rows) 
				{
					DataRow drNewRow = dstCurrentRole.Tables[0].NewRow();
					drNewRow[Sys_UserToRoleTable.ROLEID_FLD] = dr[Sys_RoleTable.ROLEID_FLD];
					drNewRow[Sys_UserToRoleTable.USERID_FLD] = pintUserID;
					dstCurrentRole.Tables[0].Rows.Add(drNewRow);	
				}
				
				//Update this data set into database
				objSys_UserToRoleDS.UpdateDataSet(dstCurrentRole);

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
