using System;
using System.Collections;
using System.Data;
using PCSComUtils.Common;
using PCSComUtils.PCSExc;
using PCSComUtils.Admin.DS;

namespace PCSComUtils.Admin.BO
{
	/// <summary>
	/// Summary description for CommonBO.
	/// </summary>
	public class ManageControlBO
	{
		private const string THIS = "PCSComUtils.Admin.BO.ManageControl";
		private const char SEPARATORCHAR = ';';

		public ManageControlBO()
		{
		}

		#region IObjectBO Members
		
	
		public void Add(object pobjObject)
		{}

		
		public void Delete(object pobjObject)
		{}

	
		public object GetObjectVO(int pintID)
		{
			return null;
		}

	
		public object GetObjectVO(int pintID, string pstrCode)
		{
			return null;
		}

	
		public void Update(object pobjObject)
		{
		}

	
		public void UpdateDataSet(DataSet dsData)
		{
		}
		
		#endregion IObjectBO Members

		#region ManageControlBO Members
		
		/// <summary>
		/// Get all control to build tree of control
		/// </summary>
		/// <returns></returns>
		/// <Author> Tuan DM. Apr 06, 2005</Author>
	
		public DataTable ListAllControl()
		{
			try
			{
				return new Sys_ControlGroupDS().ListControlGroup();	
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
		/// Detach the standard data to hierarchical data
		/// </summary>
		/// <param name="pdtbControl"></param>
		/// <returns></returns>
		/// <Author> Tuan DM. Apr 06, 2005</Author>
	
		public ArrayList DetachListControl(DataTable pdtbControl)
		{
			ArrayList arlResult = new ArrayList();			
			Sys_HiddenControlsVO voControl = new Sys_HiddenControlsVO();
			bool[] lbnCheck = new bool[pdtbControl.Rows.Count];
			for (int i=0; i <pdtbControl.Rows.Count; i++)
			{
				voControl = new Sys_HiddenControlsVO();
				voControl.FormName = pdtbControl.Rows[i][Sys_HiddenControlsTable.FORMNAME_FLD].ToString();
				if (!CheckExistControl(voControl, arlResult)) arlResult.Add(voControl);
				DataRow[] drowControls = pdtbControl.Select("FormName = '" + voControl.FormName + "'");
				i = i + drowControls.Length;
				for (int j =0; j <drowControls.Length; j++)
				{
					if ((drowControls[j][Sys_HiddenControlsTable.CONTROLNAME_FLD].ToString() != string.Empty) && (drowControls[j][Sys_HiddenControlsTable.CONTROLNAME_FLD] != null))
					{
						voControl = new Sys_HiddenControlsVO();
						voControl.FormName = drowControls[j] [Sys_HiddenControlsTable.FORMNAME_FLD].ToString();
						voControl.ControlName = drowControls[j][Sys_HiddenControlsTable.CONTROLNAME_FLD].ToString();
						if ((drowControls[j][Sys_HiddenControlsTable.SUBCONTROLNAME_FLD].ToString() != string.Empty) && (drowControls[j][Sys_HiddenControlsTable.SUBCONTROLNAME_FLD] != null))
						{
							if (!CheckExistControl(voControl, arlResult)) arlResult.Add(voControl);
							DataRow[] drowSubControls = pdtbControl.Select("FormName = '" + voControl.FormName + "' and ControlName = '" + drowControls[j][Sys_HiddenControlsTable.CONTROLNAME_FLD].ToString() + "' and SubControlName is not null and SubControlName <> ''");

							for (int k = 0; k <drowSubControls.Length; k++)
							{
								voControl = new Sys_HiddenControlsVO();	
								voControl.HiddenControlsID = int.Parse(drowSubControls[k][Sys_HiddenControlsTable.HIDDENCONTROLSID_FLD].ToString());
								voControl.FormName = drowSubControls[k][Sys_HiddenControlsTable.FORMNAME_FLD].ToString();
								voControl.ControlName = drowSubControls[k][Sys_HiddenControlsTable.CONTROLNAME_FLD].ToString();
								voControl.SubControlName = drowSubControls[k][Sys_HiddenControlsTable.SUBCONTROLNAME_FLD].ToString();
								if (!CheckExistControl(voControl, arlResult)) arlResult.Add(voControl);
							}
						}
						else
						{
							DataRow[] drowSubControls1 = pdtbControl.Select("FormName = '" + voControl.FormName + "' and ControlName = '" + drowControls[j][Sys_HiddenControlsTable.CONTROLNAME_FLD].ToString() + "'");

							for (int k = 0; k <drowSubControls1.Length; k++)
							{
								voControl.HiddenControlsID = int.Parse(drowControls[j][Sys_HiddenControlsTable.HIDDENCONTROLSID_FLD].ToString());
								if (!CheckExistControl(voControl, arlResult)) arlResult.Add(voControl);
							}
						}
					}
				}
			}
			
			return arlResult;
		}		
		
		/// <summary>
		/// Check the existing of control in ArrayList
		/// </summary>
		/// <param name="pvoControl"></param>
		/// <param name="parlControl"></param>
		/// <returns></returns>
		/// <Author> Tuan DM. Apr 06, 2005</Author>
	
		public bool CheckExistControl(Sys_HiddenControlsVO pvoControl, ArrayList parlControl)
		{
			for (int i =0; i <parlControl.Count; i++)
			{
				Sys_HiddenControlsVO voControl = (Sys_HiddenControlsVO) parlControl[i];
				if ((voControl.HiddenControlsID == pvoControl.HiddenControlsID)
					&& (voControl.FormName == pvoControl.FormName) 
					&& (voControl.ControlName == pvoControl.ControlName) 
					&& (voControl.SubControlName == pvoControl.SubControlName))
				{
					return true;
				}
			}
			return false;
		}
		
		/// <summary>
		/// Get a list of hidden control for a role; each control apart the SEPARATORCHAR 
		/// </summary>
		/// <param name="pintRoleID">Role identity</param>
		/// <returns></returns>
		/// <Author> Tuan DM. Apr 12, 2005</Author>
	
		public string ListControlForRole(int pintRoleID)
		{
			try
			{
				return new Sys_Role_ControlGroupDS().ListVisibleControlForRole(pintRoleID);
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
		/// Ccheck the deletion of control
		/// </summary>
		/// <param name="pstrID"></param>
		/// <param name="pstrListNewControl"></param>
		/// <returns></returns>
		/// <Author> Tuan DM. Apr 12, 2005</Author>		
		private bool ControlDeleted(string pstrID, string pstrListNewControl)
		{
			foreach (string newID in pstrListNewControl.Split(SEPARATORCHAR))
			{
				if (newID == pstrID)
				{
					return false;
				}
			}
			return true;
		}
		
		/// <summary>
		/// Check the addition of control
		/// </summary>
		/// <param name="pstrID"></param>
		/// <param name="pstrListOldControl"></param>
		/// <returns></returns>
		/// <Author> Tuan DM. Apr 12, 2005</Author>	
		private bool ControlAdded(string pstrID, string pstrListOldControl)
		{
			foreach (string newID in pstrListOldControl.Split(SEPARATORCHAR))
			{
				if (newID == pstrID)
				{
					return false;
				}
			}
			return true;
		}
		
		/// <summary>
		/// Reupdate all control for a role
		/// </summary>
		/// <param name="pintRoleID">RoleID</param>
		/// <param name="ListOldControl">iddenControlsID, apart by SEPARATORCHAR</param>
		/// <param name="ListNewControl">HiddenControlsID, apart by SEPARATORCHAR which checked on tree</param>
		/// <Author> Tuan DM. Apr 12, 2005</Author>	
	
		public void UpdateForRole(int pintRoleID, string ListOldControl, string ListNewControl)
		{
			Sys_Role_ControlGroupDS dsControl_Role = new Sys_Role_ControlGroupDS();
			foreach (string oldID in ListOldControl.Split(SEPARATORCHAR))
			{
				if ((oldID != string.Empty) &&(ControlDeleted(oldID, ListNewControl)))
				{
					dsControl_Role.Delete(pintRoleID, int.Parse(oldID));
				}
			}

			foreach (string newID in ListNewControl.Split(SEPARATORCHAR))
			{
				if ((newID != string.Empty) && (ControlAdded(newID, ListOldControl)))
				{
					Sys_Role_ControlGroupVO voObject = new Sys_Role_ControlGroupVO();
					voObject.ControlGroupID = int.Parse(newID);
					voObject.RoleID = pintRoleID;
					dsControl_Role.Add(voObject);
				}
			}
		}

		#endregion ManageControlBO Members
	}
}