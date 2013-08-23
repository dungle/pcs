using System;
using System.Collections;
using System.Data;
using PCSComUtils.PCSExc;
using PCSComUtils.Common;
using PCSComUtils.Framework.ReportFrame.DS;

namespace PCSComUtils.Framework.ReportFrame.BO
{
	public class ReportGroupBO
	{
		private const string THIS = "PCSComUtils.Framework.ReportFrame.BO.ReportGroupBO";
		
		public void Add(object pobjObjectVO)
		{
			sys_ReportGroupDS  dsReportGroup = new sys_ReportGroupDS();
			dsReportGroup.Add(pobjObjectVO);
		}	
	
		public object GetObjectVO(int pintID,string VOclass)
		{
			const string METHOD_NAME = THIS + ".GetObjectVO()";
			throw new PCSException(ErrorCode.NOT_IMPLEMENT, METHOD_NAME, new Exception());
		}	
	
		public void Delete(object pObjectVO)
		{
			const string METHOD_NAME = THIS + ".Delete()";
			throw new PCSException(ErrorCode.NOT_IMPLEMENT, METHOD_NAME, new Exception(ErrorCode.NOT_IMPLEMENT.ToString()));
		}	

		public void Delete(int pintID)
		{
			sys_ReportGroupDS  dsReportGroup = new sys_ReportGroupDS();
			dsReportGroup.Delete(pintID);
		}

		public object GetObjectVO(int pintID)
		{	
			sys_ReportGroupDS  dsReportGroup = new sys_ReportGroupDS();
			return dsReportGroup.GetObjectVO(pintID);
		}
	
		public void Update(object pobjObjecVO)
		{
			sys_ReportGroupDS  dsReportGroup = new sys_ReportGroupDS();
			dsReportGroup.Update(pobjObjecVO);
		}
	
		public void Update(object pobjObjecVO, string pstrOldID)
		{
			sys_ReportGroupDS  dsReportGroup = new sys_ReportGroupDS();
			dsReportGroup.Update(pobjObjecVO, pstrOldID);
		}
	
		public DataSet List()
		{
			sys_ReportGroupDS  dsReportGroup = new sys_ReportGroupDS();
			return dsReportGroup.List();
		}

		public void UpdateDataSet(DataSet pData)
		{
			sys_ReportGroupDS  dsReportGroup = new sys_ReportGroupDS();
			dsReportGroup.UpdateDataSet(pData);
		}

		public ArrayList GetAllGroups()
		{
			sys_ReportGroupDS dsReportGroup = new sys_ReportGroupDS();
			sys_ReportGroupVO voReportGroup;
			ArrayList arrGroups = new ArrayList();
			DataSet dset = new DataSet();
			dset = dsReportGroup.List();
			foreach (DataRow row in dset.Tables[0].Rows)
			{
				voReportGroup = new sys_ReportGroupVO();
				voReportGroup.GroupID = row[sys_ReportGroupTable.GROUPID_FLD].ToString();
				voReportGroup.GroupName = row[sys_ReportGroupTable.GROUPNAME_FLD].ToString();
				voReportGroup.GroupOrder = int.Parse(row[sys_ReportGroupTable.GROUPORDER_FLD].ToString());
					
				// add object sys_ReportGroupVO into array
				arrGroups.Add(voReportGroup);
			}
			return arrGroups;
		}

		public object GetReportGroup(int pintGroupID)
		{
			sys_ReportGroupDS dsReportGroup = new sys_ReportGroupDS();
			return dsReportGroup.GetObjectVO(pintGroupID);
		}

		public bool DeleteGroup(string pstrGroupID)
		{
			sys_ReportGroupDS dsReportGroup = new sys_ReportGroupDS();
			sys_ReportAndGroupDS dsReportAndGroup = new sys_ReportAndGroupDS();
			ArrayList arrObjects = dsReportAndGroup.GetObjectVOs(pstrGroupID);
			if (arrObjects.Count == 0)
			{
				// delete the group
				dsReportGroup.Delete(pstrGroupID);
				return true;
			}
			else
			{
				return false;
			}
		}

		public int GetMaxGroupOrder()
		{
			sys_ReportGroupDS dsReportGroup = new sys_ReportGroupDS();
			return dsReportGroup.GetMaxGroupOrder();
		}

		public bool MoveUpGroup(string pstrGroupID)
		{
			bool blnResult = true;
			sys_ReportGroupDS dsReportGroup = new sys_ReportGroupDS();
			sys_ReportGroupVO voPreviousGroup = new sys_ReportGroupVO();
			sys_ReportGroupVO voCurrentGroup = new sys_ReportGroupVO();
			ArrayList arrGroups = new ArrayList();

			arrGroups = dsReportGroup.GetObjectVOs();
			for (int i = 0; i < arrGroups.Count; i++)
			{
				sys_ReportGroupVO voReportGroup = (sys_ReportGroupVO)arrGroups[i];
				string strGroupID = voReportGroup.GroupID;
				if (strGroupID.Equals(pstrGroupID))
				{
					int intCurrentOrder = voReportGroup.GroupOrder;
					// if current order reached the top of layout then return false
					if (intCurrentOrder <= 1)
					{
						blnResult = false;
					}
					else 
					{
						// get previous group
						voPreviousGroup = (sys_ReportGroupVO)arrGroups[i-1];
						// get current group
						voCurrentGroup = (sys_ReportGroupVO)arrGroups[i];
						// switch order between two groups
						voCurrentGroup.GroupOrder = voPreviousGroup.GroupOrder;
						voPreviousGroup.GroupOrder = intCurrentOrder;
							
						//update two rows into database
						dsReportGroup.Update(voPreviousGroup);
						dsReportGroup.Update(voCurrentGroup);
						blnResult = true;
					}
				}
			}
			return blnResult;
		}
	
		public bool MoveDownGroup(string pstrGroupID)
		{
			const string METHOD_NAME = THIS + ".MoveDownGroup()";
			bool blnResult = true;
			sys_ReportGroupDS dsReportGroup = new sys_ReportGroupDS();
			sys_ReportGroupVO voPreviousGroup = new sys_ReportGroupVO();
			sys_ReportGroupVO voCurrentGroup = new sys_ReportGroupVO();
			ArrayList arrGroups = new ArrayList();

			arrGroups = dsReportGroup.GetObjectVOs();
			for (int i = 0; i < arrGroups.Count; i++)
			{
				sys_ReportGroupVO voReportGroup = (sys_ReportGroupVO)arrGroups[i];
				string strGroupID = voReportGroup.GroupID;
				if (strGroupID.Equals(pstrGroupID))
				{
					int intCurGroupOrder = voReportGroup.GroupOrder;
					// if current order reached the bottom of layout then return false
					if (intCurGroupOrder >= arrGroups.Count)
					{
						blnResult = false;
					}
					else 
					{
						// get the previous group
						voPreviousGroup = (sys_ReportGroupVO)arrGroups[i+1];
						// get the current group
						voCurrentGroup = (sys_ReportGroupVO)arrGroups[i];
						// switch order
						voCurrentGroup.GroupOrder = voPreviousGroup.GroupOrder;
						voPreviousGroup.GroupOrder = intCurGroupOrder;
							
						//update two rows into database
						dsReportGroup.Update(voPreviousGroup);
						dsReportGroup.Update(voCurrentGroup);
						blnResult = true;
					}
				}
			}
			return blnResult;
		}
		/// <summary>
		/// Get all group
		/// </summary>
		/// <returns>List of Group VO object</returns>
	
		public ArrayList GetAllGroup()
		{
			sys_ReportGroupDS dsReportGroupDS = new sys_ReportGroupDS();
			return dsReportGroupDS.GetObjectVOs();
		}
	}
}