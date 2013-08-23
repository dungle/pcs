using System;
using System.Collections;
using System.Data;
using PCSComUtils.PCSExc;
using PCSComUtils.Common;
using PCSComUtils.Framework.ReportFrame.DS;

namespace PCSComUtils.Framework.ReportFrame.BO
{
	public class FieldPropertiesBO
	{
		private const string THIS = "PCSComUtils.Framework.ReportFrame.BO.FieldPropertiesBO";		
		public void Add(object pobjObjectVO)
		{
			sys_ReportFieldsDS  templateDS = new sys_ReportFieldsDS();
			templateDS.Add(pobjObjectVO);
		}	
		public object GetObjectVO(int pintID,string VOclass)
		{
			const string METHOD_NAME = THIS + ".GetObjectVO()";
			throw new PCSException(ErrorCode.NOT_IMPLEMENT, METHOD_NAME,new Exception());
		}	

		public void Delete(object pObjectVO)
		{
			const string METHOD_NAME = THIS + ".Delete()";
			throw new PCSException(ErrorCode.NOT_IMPLEMENT, METHOD_NAME,new Exception());
		}	
		public void Delete(int pintID)
		{
			sys_ReportFieldsDS  templateDS = new sys_ReportFieldsDS();
			templateDS.Delete(pintID);
		}
	
		public void Delete(string pstrReportID)
		{
			sys_ReportFieldsDS  dsReportField = new sys_ReportFieldsDS();
			dsReportField.Delete(pstrReportID);
		}
		public object GetObjectVO(int pintID)
		{	
			sys_ReportFieldsDS  templateDS = new sys_ReportFieldsDS();
			return templateDS.GetObjectVO(pintID);
		}
	
		public void Update(object pobjObjecVO)
		{
			sys_ReportFieldsDS  templateDS = new sys_ReportFieldsDS();
			templateDS.UpdateByName(pobjObjecVO);
		}
	
		public DataSet List()
		{
			sys_ReportFieldsDS  templateDS = new sys_ReportFieldsDS();
			return templateDS.List();
		}
	
		public void UpdateDataSet(DataSet pData)
		{
			sys_ReportFieldsDS  templateDS = new sys_ReportFieldsDS();
			templateDS.UpdateDataSet(pData);
		}
	
		public ArrayList ListByReport(string pstrReportID)
		{	
			sys_ReportFieldsDS  dsReportFields = new sys_ReportFieldsDS();
			return dsReportFields.GetObjectVOs(pstrReportID);
		}
	
		public string ListByCommand(string pstrCommand, string pstrReportType)
		{
			const char DELIMITER = ' ';
			string strFields = string.Empty;
			if (pstrReportType.Equals(Constants.SQL_REPORT))
			{
				string[] arrCommand = pstrCommand.Split(DELIMITER);
				foreach (string strElement in arrCommand)
				{
					if ((!strElement.Trim().ToUpper().Equals(Constants.SELECT_STR)) &&
						(!strElement.Trim().ToUpper().Equals(Constants.FROM_STR.Trim())))
					{
						strFields += strElement;
					}
					else if (strElement.Trim().ToUpper().Equals(Constants.FROM_STR.Trim()))
					{
						break;
					}
				}
			}
			else // dll or C# file
			{
					
			}
			return strFields;
		}
	
		public int GetMaxFieldOrder(string pstrReportID)
		{
			sys_ReportFieldsDS dsField = new sys_ReportFieldsDS();
			return dsField.GetMaxFieldOrder(pstrReportID);
		}
	
		public bool MoveUpField(string pstrReportID, string pstrFieldName)
		{
			bool blnResult = true;
			sys_ReportFieldsDS dsReportField = new sys_ReportFieldsDS();
			sys_ReportFieldsVO voPreviousField = new sys_ReportFieldsVO();
			sys_ReportFieldsVO voCurrentField = new sys_ReportFieldsVO();
			ArrayList arrFields = new ArrayList();

			arrFields = dsReportField.GetObjectVOs(pstrReportID);
			for (int i = 0; i < arrFields.Count; i++)
			{
				sys_ReportFieldsVO voReportField = (sys_ReportFieldsVO)arrFields[i];
				string strFieldName = voReportField.FieldName;
				if (strFieldName.Equals(pstrFieldName))
				{
					int intCurrentOrder = voReportField.FieldOrder;
					// if current order reached the top of layout then return false
					if (intCurrentOrder <= 1)
					{
						blnResult = false;
					}
					else 
					{
						// get previous group
						voPreviousField = (sys_ReportFieldsVO)arrFields[i-1];
						// get current group
						voCurrentField = (sys_ReportFieldsVO)arrFields[i];
						// switch order between two groups
						voCurrentField.FieldOrder = voPreviousField.FieldOrder;
						voPreviousField.FieldOrder = intCurrentOrder;
							
						//update two rows into database
						dsReportField.UpdateByName(voPreviousField);
						dsReportField.UpdateByName(voCurrentField);
						blnResult = true;
					}
				}
			}
			return blnResult;
		}
	
		public bool MoveDownField(string pstrReportID, string pstrFieldName)
		{
			const string METHOD_NAME = THIS + ".MoveDownField()";
			bool blnResult = true;
			sys_ReportFieldsDS dsReportField = new sys_ReportFieldsDS();
			sys_ReportFieldsVO voPreviousField = new sys_ReportFieldsVO();
			sys_ReportFieldsVO voCurrentField = new sys_ReportFieldsVO();
			ArrayList arrFields = new ArrayList();

			arrFields = dsReportField.GetObjectVOs(pstrReportID);
			for (int i = 0; i < arrFields.Count; i++)
			{
				sys_ReportFieldsVO voReportField = (sys_ReportFieldsVO)arrFields[i];
				string strFieldName = voReportField.FieldName;
				if (strFieldName.Equals(pstrFieldName))
				{
					int intCurrentOrder = voReportField.FieldOrder;
					// if current order reached the bottom of layout then return false
					if (intCurrentOrder >= arrFields.Count)
					{
						blnResult = false;
					}
					else 
					{
						// get the previous group
						voPreviousField = (sys_ReportFieldsVO)arrFields[i+1];
						// get the current group
						voCurrentField = (sys_ReportFieldsVO)arrFields[i];
						// switch order
						voCurrentField.FieldOrder = voPreviousField.FieldOrder;
						voPreviousField.FieldOrder = intCurrentOrder;
							
						//update two rows into database
						dsReportField.UpdateByName(voPreviousField);
						dsReportField.UpdateByName(voCurrentField);
						blnResult = true;
					}
				}
			}
			return blnResult;
		}
	
		public ArrayList GetGroupByFields(string pstrReportID)
		{
			sys_ReportFieldsDS dsFields = new sys_ReportFieldsDS();
			return dsFields.GetGroupByFields(pstrReportID);
		}
	
		public ArrayList GetSortFields(string pstrReportID)
		{
			sys_ReportFieldsDS dsFields = new sys_ReportFieldsDS();
			return dsFields.GetSortFields(pstrReportID);
		}
		
		/// <summary>
		/// Update Report field by name
		/// </summary>
		/// <param name="pobjObjectVO">sys_ReportFieldsVO</param>
	
		public void UpdateByName(object pobjObjectVO)
		{
			sys_ReportFieldsDS dsReportField = new sys_ReportFieldsDS();
			dsReportField.UpdateByName(pobjObjectVO);
		}

		/// <summary>
		/// Switch two field order
		/// </summary>
		/// <param name="pobjSourceField">Source Field</param>
		/// <param name="pobjDestField">Destination Field</param>
	
		public void SwitchFields(object pobjSourceField, object pobjDestField)
		{
			sys_ReportFieldsDS dsReportField = new sys_ReportFieldsDS();
			dsReportField.UpdateByName(pobjSourceField);
			dsReportField.UpdateByName(pobjDestField);
		}
	}
}