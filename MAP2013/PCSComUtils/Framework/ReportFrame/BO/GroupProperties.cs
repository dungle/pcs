using System;
using System.Collections;
using System.Data;
using PCSComUtils.PCSExc;
using PCSComUtils.Common;
using PCSComUtils.Framework.ReportFrame.DS;

namespace PCSComUtils.Framework.ReportFrame.BO
{
	public class GroupPropertiesBO
	{
		private const string THIS = "PCSComUtils.Framework.ReportFrame.BO.GroupPropertiesBO";		
		public GroupPropertiesBO()
		{
		}

		/// <summary>
		/// Insert a new record into database
		/// </summary>
	
		public void Add(object pObjectDetail)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Insert a new record into database
		/// </summary>
	
		public int AddAndReturnID(object pobjFieldGroup, ArrayList parrSelectedItem)
		{
			try
			{
				Sys_FieldGroupVO voFieldGroup = (Sys_FieldGroupVO)pobjFieldGroup;
				Sys_FieldGroupDS dsFieldGroup = new Sys_FieldGroupDS();
				sys_ReportFieldsDS dsReportField = new sys_ReportFieldsDS();
				Sys_FieldGroupDetailDS dsFieldGroupDetail = new Sys_FieldGroupDetailDS();
				// get current field group order
				voFieldGroup.GroupOrder = dsFieldGroup.GetFieldGroupOrder(voFieldGroup.ReportID, voFieldGroup.ParentFieldGroupID) + 1;
				// save new field group to database
				int intNewID = dsFieldGroup.AddAndReturnID(voFieldGroup);
				// save all selected sub group/field to database if any
				for (int i = 0; i < parrSelectedItem.Count; i++)
				{
					switch (voFieldGroup.GroupLevel)
					{
						case (int)GroupFieldLevel.One:
							// update sub group
							Sys_FieldGroupVO voSubGroup = (Sys_FieldGroupVO)parrSelectedItem[i];
							// parent field group id
							voSubGroup.ParentFieldGroupID = intNewID;
							// new order in group
							voSubGroup.GroupOrder = i + 1;
							// save to database
							dsFieldGroup.Update(voSubGroup);
							break;
						case (int)GroupFieldLevel.Two:
							// update report field
							sys_ReportFieldsVO voReportField = (sys_ReportFieldsVO)parrSelectedItem[i];
							// new field order in group
							voReportField.FieldOrder = i + 1;
							dsReportField.UpdateByName(voReportField);
							// create new Field Group Detail
							Sys_FieldGroupDetailVO voFieldGroupDetail = new Sys_FieldGroupDetailVO();
							voFieldGroupDetail.FieldGroupID = intNewID;
							voFieldGroupDetail.ReportID = voFieldGroup.ReportID;
							voFieldGroupDetail.FieldName = voReportField.FieldName;
							// save to database
							dsFieldGroupDetail.Add(voFieldGroupDetail);
							break;
					}
				}
				return intNewID;
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
		/// Delete record by condition
		/// </summary>
	
		public void Delete(object pObjectVO)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Delete record by condition
		/// </summary>
	
		public void Delete(object pObjectVO, ArrayList parrSelected)
		{
			try
			{
				Sys_FieldGroupVO voFieldGroup = (Sys_FieldGroupVO)pObjectVO;
				Sys_FieldGroupDS dsFieldGroup = new Sys_FieldGroupDS();
				sys_ReportFieldsDS dsReportField = new sys_ReportFieldsDS();
				Sys_FieldGroupDetailDS dsFieldGroupDetail = new Sys_FieldGroupDetailDS();
				int intCurrentGroupOrder = dsFieldGroup.GetFieldGroupOrder(voFieldGroup.ReportID, 0);
				int intCurrentFieldOrder = dsReportField.GetMaxFieldOrder(voFieldGroup.ReportID);
				for (int i = 0; i < parrSelected.Count; i++)
				{
					switch (voFieldGroup.GroupLevel)
					{
						case (int)GroupFieldLevel.One:
							// update sub group (order and parent id)
							Sys_FieldGroupVO voSubGroup = (Sys_FieldGroupVO)parrSelected[i];
							voSubGroup.ParentFieldGroupID = 0;
							voSubGroup.GroupOrder = intCurrentGroupOrder + i + 1;
							dsFieldGroup.Update(voSubGroup);
							break;
						case (int)GroupFieldLevel.Two:
							// update report field
							sys_ReportFieldsVO voReportField = (sys_ReportFieldsVO)parrSelected[i];
							// new field order in group
							voReportField.FieldOrder = intCurrentFieldOrder + i + 1;
							dsReportField.UpdateByName(voReportField);
							// delete field group detail records first
							dsFieldGroupDetail.Delete(voFieldGroup.FieldGroupID, voFieldGroup.ReportID);
							break;
					}
				}
				// delete field group
				dsFieldGroup.Delete(voFieldGroup.FieldGroupID);
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
		/// Get the object information by ID of VO class
		/// </summary>
	
		public object GetObjectVO(int pintID, string VOclass)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Return the DataSet (list of record) by inputing the FieldList and Condition
		/// </summary>
	
		public void UpdateDataSet(DataSet dstData)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Update into Database
		/// </summary>
	
		public void Update(object pObjectDetail)
		{
			throw new NotImplementedException();
		}
		
		/// <summary>
		/// Update into Database
		/// </summary>
	
		public void Update(object pobjFieldGroup, ArrayList parrSelectedItem, ArrayList parrAvailableItem)
		{
			try
			{
				Sys_FieldGroupVO voFieldGroup = (Sys_FieldGroupVO)pobjFieldGroup;
				Sys_FieldGroupDS dsFieldGroup = new Sys_FieldGroupDS();
				sys_ReportFieldsDS dsReportField = new sys_ReportFieldsDS();
				Sys_FieldGroupDetailDS dsFieldGroupDetail = new Sys_FieldGroupDetailDS();
				// update Field Group
				dsFieldGroup.Update(voFieldGroup);
				int intCurrentGroupOrder = dsFieldGroup.GetFieldGroupOrder(voFieldGroup.ReportID, 0);
				int intCurrentFieldOrder = dsReportField.GetMaxFieldOrder(voFieldGroup.ReportID);
				for (int i = 0; i < parrAvailableItem.Count; i++)
				{
					switch (voFieldGroup.GroupLevel)
					{
						case (int)GroupFieldLevel.One:
							Sys_FieldGroupVO voSubGroup = (Sys_FieldGroupVO)parrAvailableItem[i];
							// remove parent field group
							voSubGroup.ParentFieldGroupID = 0;
							voSubGroup.GroupOrder = intCurrentGroupOrder + 1 + i;
							dsFieldGroup.Update(voSubGroup);
							break;
						case (int)GroupFieldLevel.Two:
							// update report field
							sys_ReportFieldsVO voReportField = (sys_ReportFieldsVO)parrSelectedItem[i];
							// new field order in group
							voReportField.FieldOrder = intCurrentFieldOrder + i + 1;
							dsReportField.UpdateByName(voReportField);
							// create new Field Group Detail
							Sys_FieldGroupDetailVO voFieldGroupDetail = new Sys_FieldGroupDetailVO();
							voFieldGroupDetail.FieldGroupID = voFieldGroup.FieldGroupID;
							voFieldGroupDetail.ReportID = voFieldGroup.ReportID;
							voFieldGroupDetail.FieldName = voReportField.FieldName;
							// save to database
							dsFieldGroupDetail.Add(voFieldGroupDetail);
							break;
					}
				}
				// delete old field group detail
				dsFieldGroupDetail.Delete(voFieldGroup.FieldGroupID, voFieldGroup.ReportID);
				// update selected group/field
				for (int i = 0; i < parrSelectedItem.Count; i++)
				{
					switch (voFieldGroup.GroupLevel)
					{
						case (int)GroupFieldLevel.One:
							Sys_FieldGroupVO voSubGroup = (Sys_FieldGroupVO)parrSelectedItem[i];
							voSubGroup.ParentFieldGroupID = voFieldGroup.FieldGroupID;
							voSubGroup.GroupOrder = i + 1;
							dsFieldGroup.Update(voSubGroup);
							break;
						case (int)GroupFieldLevel.Two:
							// update report field
							sys_ReportFieldsVO voReportField = (sys_ReportFieldsVO)parrSelectedItem[i];
							// new field order in group
							voReportField.FieldOrder = i + 1;
							dsReportField.UpdateByName(voReportField);
							// create new Field Group Detail
							Sys_FieldGroupDetailVO voFieldGroupDetail = new Sys_FieldGroupDetailVO();
							voFieldGroupDetail.FieldGroupID = voFieldGroup.FieldGroupID;
							voFieldGroupDetail.ReportID = voFieldGroup.ReportID;
							voFieldGroupDetail.FieldName = voReportField.FieldName;
							// save to database
							dsFieldGroupDetail.Add(voFieldGroupDetail);
							break;
					}
				}
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
		/// List all Field Group of Report
		/// </summary>
		/// <param name="pstrReportID">Report ID</param>
		/// <returns>DataTable</returns>
	
		public DataTable List(string pstrReportID)
		{
			try
			{
				Sys_FieldGroupDS dsFieldGroup = new Sys_FieldGroupDS();
				return dsFieldGroup.List(pstrReportID);
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
		/// Get all lower group of a Group
		/// </summary>
		/// <param name="pintFieldGroupID">Field Group</param>
		/// <returns>DataTable</returns>
	
		public DataTable GetLowerGroup(int pintFieldGroupID)
		{
			try
			{
				Sys_FieldGroupDS dsFieldGroup = new Sys_FieldGroupDS();
				return dsFieldGroup.GetLowerGroup(pintFieldGroupID);
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
		/// Get all field belong to field group
		/// </summary>
		/// <param name="pintFieldGroupID">Field Group Id</param>
		/// <param name="pstrReportID">Report ID</param>
		/// <returns>DataTable</returns>
	
		public DataTable GetFields(int pintFieldGroupID, string pstrReportID)
		{
			try
			{
				Sys_FieldGroupDS dsFieldGroup = new Sys_FieldGroupDS();
				return dsFieldGroup.GetFields(pintFieldGroupID, pstrReportID);
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
		/// Get all field which not belong to any field group
		/// </summary>
		/// <param name="pstrReportID">Report ID</param>
		/// <returns>DataTable</returns>
	
		public DataTable GetAvailableFields(string pstrReportID)
		{
			try
			{
				Sys_FieldGroupDS dsFieldGroup = new Sys_FieldGroupDS();
				return dsFieldGroup.GetAvailableFields(pstrReportID);
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
		/// Switch two group order
		/// </summary>
		/// <param name="pobjGroup1">Field Group 1</param>
		/// <param name="pobjGroup2">Field Group 2</param>
	
		public void SwitchFieldGroup (object pobjGroup1, object pobjGroup2)
		{
			try
			{
				Sys_FieldGroupDS dsFieldGroup = new Sys_FieldGroupDS();
				dsFieldGroup.Update(pobjGroup1);
				dsFieldGroup.Update(pobjGroup2);
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
	}
}
