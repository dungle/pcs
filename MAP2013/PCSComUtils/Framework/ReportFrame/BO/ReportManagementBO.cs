using System;
using System.Collections;
using System.Data;
using PCSComUtils.Admin.DS;
using PCSComUtils.Common;
using PCSComUtils.Common.BO;
using PCSComUtils.Framework.ReportFrame.DS;

using PCSComUtils.PCSExc;


namespace PCSComUtils.Framework.ReportFrame.BO
{
	public class ReportManagementBO
	{
		private const string THIS = "PCSComUtils.Framework.ReportFrame.BO.ReportManagementBO";
		
		/// <summary>
		/// Default constructor
		/// </summary>
		public ReportManagementBO()
		{
		}
		

		#region IObjectBO Members

		///    <Description>
		///       This method not implements yet 
		///    </Description>
		///    <Inputs>
		///       Value object    
		///    </Inputs>
		///    <Outputs>
		///      N/A   
		///    </Outputs>
		///    <Returns>
		///      void
		///    </Returns>
		///    <Authors>
		///       DungLA
		///    </Authors>
		///    <History>
		///     Created: 28-Dec-2004
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public void Add(object pObjectDetail)
		{
			const string METHOD_NAME = THIS + ".Add()";
			throw new PCSException(ErrorCode.NOT_IMPLEMENT, METHOD_NAME, new Exception());
		}

		//**************************************************************************              
		///    <Description>
		///       This method not implements yet
		///    </Description>
		///    <Inputs>
		///        
		///    </Inputs>
		///    <Outputs>
		///    
		///    </Outputs>
		///    <Returns>
		///    
		///    </Returns>
		///    <Authors>
		///       DungLA
		///    </Authors>
		///    <History>
		///		Created: 28-Dec-2004 
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
	
		public void Delete(object pObjectVO)
		{
			sys_ReportVO voReport = (sys_ReportVO)pObjectVO;

			sys_ReportAndGroupDS dsReportAndGroup = new sys_ReportAndGroupDS();
			// delete data in sys_ReportAndGroup
			dsReportAndGroup.DeleteByReportID(voReport.ReportID);

			// delete data in sys_ReportDrillDown where master id is selected report id
			sys_ReportDrillDownDS dsDrillDown = new sys_ReportDrillDownDS();
			dsDrillDown.Delete(voReport.ReportID);

			// delete data in sys_ReportFields
			sys_ReportFieldsDS dsReportFields = new sys_ReportFieldsDS();
			dsReportFields.Delete(voReport.ReportID);

			// delete data in sys_ReportPara
			sys_ReportParaDS dsReportPara = new sys_ReportParaDS();
			dsReportPara.Delete(voReport.ReportID);

			// retrieve history of this report
			sys_ReportHistoryVO voReportHistory = new sys_ReportHistoryVO();
			sys_ReportHistoryDS dsReportHistory = new sys_ReportHistoryDS();
			sys_ReportHistoryParaDS dsHistoryPara = new sys_ReportHistoryParaDS();
			ArrayList arrHistory = new ArrayList();
			arrHistory = dsReportHistory.ListByReport(voReport.ReportID);
			// delete all data in sys_ReportHistoryPara related to each history
			if (arrHistory.Count > 0)
			{
				for (int i = 0; i < arrHistory.Count - 1; i++)
				{
					voReportHistory = (sys_ReportHistoryVO)arrHistory[i];
					dsHistoryPara.Delete(voReportHistory.HistoryID);
				}
			}
			// delete data in sys_ReportHistory
			dsReportHistory.DeleteByReportID(voReport.ReportID);

			// delete data in sys_Report
			sys_ReportDS dsReport = new sys_ReportDS();
			dsReport.Delete(voReport.ReportID);
		}

		//**************************************************************************              
		///    <Description>
		///       This method not implements yet
		///    </Description>
		///    <Inputs>
		///        
		///    </Inputs>
		///    <Outputs>
		///    
		///    </Outputs>
		///    <Returns>
		///    
		///    </Returns>
		///    <Authors>
		///       DungLA
		///    </Authors>
		///    <History>
		///    Created: 28-Dec-2004 
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public object GetObjectVO(int pintID, string VOclass)
		{
			const string METHOD_NAME = THIS + ".GetObjectVO()";
			throw new PCSException(ErrorCode.NOT_IMPLEMENT, METHOD_NAME, new Exception());
		}

		//**************************************************************************              
		///    <Description>
		///       This method not implements yet
		///    </Description>
		///    <Inputs>
		///        
		///    </Inputs>
		///    <Outputs>
		///    
		///    </Outputs>
		///    <Returns>
		///    
		///    </Returns>
		///    <Authors>
		///       DungLA
		///    </Authors>
		///    <History>
		///		Created: 28-Dec-2004 
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public void UpdateDataSet(DataSet dstData)
		{
			const string METHOD_NAME = THIS + ".UpdateDataSet()";
			throw new PCSException(ErrorCode.NOT_IMPLEMENT, METHOD_NAME, new Exception());
		}

		//**************************************************************************              
		///    <Description>
		///       This method not implements yet
		///    </Description>
		///    <Inputs>
		///        
		///    </Inputs>
		///    <Outputs>
		///    
		///    </Outputs>
		///    <Returns>
		///    
		///    </Returns>
		///    <Authors>
		///       DungLA
		///    </Authors>
		///    <History>
		///		Created: 28-Dec-2004 
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public void Update(object pObjectDetail)
		{
			const string METHOD_NAME = THIS + ".Update()";
			throw new PCSException(ErrorCode.NOT_IMPLEMENT, METHOD_NAME, new Exception());
		}

		//**************************************************************************              
		///    <Description>
		///      This method uses to get all data
		///    </Description>
		///    <Inputs>
		///               
		///    </Inputs>
		///    <Outputs>
		///    
		///    </Outputs>
		///    <Returns>
		///       DataSet
		///    </Returns>
		///    <Authors>
		///       DungLA 
		///    </Authors>
		///    <History>
		///		Created: 28-Dec-2004
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
	
		public DataSet List()
		{
			sys_ReportDS dsSysReport = new sys_ReportDS();
			return dsSysReport.List();
		}

		//**************************************************************************              
		///    <Description>
		///      This method uses to get all report data of a group
		///    </Description>
		///    <Inputs>
		///		Group ID
		///    </Inputs>
		///    <Outputs>
		///		DataSet
		///    </Outputs>
		///    <Returns>
		///       DataSet
		///    </Returns>
		///    <Authors>
		///       DungLA 
		///    </Authors>
		///    <History>
		///		Created: 29-Dec-2004
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
	
		public DataSet List(string pstrGroupID)
		{
			sys_ReportDS dsSysReport = new sys_ReportDS();
			return dsSysReport.List(pstrGroupID);
		}

		//**************************************************************************              
		///    <Description>
		///      This method uses to get all report data of a group by user role
		///    </Description>
		///    <Inputs>
		///		Group ID
		///    </Inputs>
		///    <Outputs>
		///		DataSet
		///    </Outputs>
		///    <Returns>
		///       DataSet
		///    </Returns>
		///    <Authors>
		///       DungLA 
		///    </Authors>
		///    <History>
		///		Created: 29-Dec-2004
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
	
		public DataSet ListByUser(string pstrGroupID, string pstrUserName)
		{
			// get user id
			int intUserID = new Sys_UserDS().GetUserIDByUserName(pstrUserName);
			sys_ReportDS dsSysReport = new sys_ReportDS();
			return dsSysReport.ListByUser(pstrGroupID, intUserID);
		}
		//**************************************************************************              
		///    <Description>
		///      This method uses to get all report data of a group by user role
		///    </Description>
		///    <Inputs>
		///		Group ID
		///    </Inputs>
		///    <Outputs>
		///		DataSet
		///    </Outputs>
		///    <Returns>
		///       DataSet
		///    </Returns>
		///    <Authors>
		///       DungLA 
		///    </Authors>
		///    <History>
		///		Created: 29-Dec-2004
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
	
		public ArrayList GetAllReports(string pstrGroupID, string pstrUserName)
		{
			// get user id
			int intUserID = new Sys_UserDS().GetUserIDByUserName(pstrUserName);
			sys_ReportDS dsSysReport = new sys_ReportDS();
			return dsSysReport.GetAllReports(pstrGroupID, intUserID);
		}

		#endregion

		#region Public Methods

		//**************************************************************************              
		///    <Description>
		///       This method uses to check whether a report has drill down report or not
		///    </Description>
		///    <Inputs>
		///        Report ID
		///    </Inputs>
		///    <Outputs>
		///      bool
		///    </Outputs>
		///    <Returns>
		///       true: if has drill down | false: if has no drill down
		///    </Returns>
		///    <Authors>
		///       DungLA
		///    </Authors>
		///    <History>
		///       01-Feb-2004
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
	
		public ArrayList GetDrillDownReports(string pstrReportID)
		{
			sys_ReportDrillDownDS dsReportDrillDown = new sys_ReportDrillDownDS();
			return dsReportDrillDown.GetObjectVOs(pstrReportID);
		}
		//**************************************************************************              
		///    <Description>
		///       This method uses to check whether a report has drill down report or not
		///    </Description>
		///    <Inputs>
		///        Master Report ID, Detail Report ID
		///    </Inputs>
		///    <Outputs>
		///      bool
		///    </Outputs>
		///    <Returns>
		///       true: if has drill down | false: if has no drill down
		///    </Returns>
		///    <Authors>
		///       DungLA
		///    </Authors>
		///    <History>
		///       01-Feb-2004
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
	
		public ArrayList GetDrillDownReports(string pstrMasterReportID, string pstrDetailReportID)
		{
			sys_ReportDrillDownDS dsReportDrillDown = new sys_ReportDrillDownDS();
			return dsReportDrillDown.GetObjectVOs(pstrMasterReportID, pstrDetailReportID);
		}
		//**************************************************************************              
		///    <Description>
		///       This method uses to change up order of a row in sys_ReportAndGroup
		///    </Description>
		///    <Inputs>
		///        Report ID
		///    </Inputs>
		///    <Outputs>
		///      changes
		///    </Outputs>
		///    <Returns>
		///       bool
		///    </Returns>
		///    <Authors>
		///       DungLA
		///    </Authors>
		///    <History>
		///       Created: 30-Dec-2004
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
	
		public bool MoveUpReport(string pstrReportID)
		{
			bool blnResult = false;
			sys_ReportAndGroupDS dsReportAndGroup = new sys_ReportAndGroupDS();
			sys_ReportAndGroupVO voCurrentObject = new sys_ReportAndGroupVO();
			sys_ReportAndGroupVO voPreviousObject = new sys_ReportAndGroupVO();
			ArrayList arrReportAndGroup = new ArrayList();

			string strGroupID = dsReportAndGroup.GetGroupIDByReportID(pstrReportID);
			// get all record from sys_ReportAndGroup
			arrReportAndGroup = dsReportAndGroup.GetObjectVOs(strGroupID);
			for (int i = 0; i < arrReportAndGroup.Count; i++)
			{
				sys_ReportAndGroupVO voReportAndGroup = (sys_ReportAndGroupVO)arrReportAndGroup[i];
				if (voReportAndGroup.ReportID.Equals(pstrReportID))
				{
					int intCurrentOrder = voReportAndGroup.ReportOrder;
					// if current order is the top then cannot move up any more
					if (intCurrentOrder <= 1)
					{
						// return false
						blnResult = false;
					}
					else
					{
						// get previous report
						voPreviousObject = (sys_ReportAndGroupVO)arrReportAndGroup[i-1];
						// get current report
						voCurrentObject = (sys_ReportAndGroupVO)arrReportAndGroup[i];
						// switch order
						voCurrentObject.ReportOrder = voPreviousObject.ReportOrder;
						voPreviousObject.ReportOrder = intCurrentOrder;

						// update two rows in database
						dsReportAndGroup.Update(voPreviousObject);
						dsReportAndGroup.Update(voCurrentObject);
						// return true
						blnResult = true;
					}
				}
			}
			return blnResult;
		}

		//**************************************************************************              
		///    <Description>
		///       This method uses to change down of a row in sys_ReportAndGroup
		///    </Description>
		///    <Inputs>
		///        ReportID
		///    </Inputs>
		///    <Outputs>
		///      changes
		///    </Outputs>
		///    <Returns>
		///       bool
		///    </Returns>
		///    <Authors>
		///       DungLA
		///    </Authors>
		///    <History>
		///       30-Dec-2004
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
	
		public bool MoveDownReport(string pstrReportID)
		{
			bool blnResult = false;
			sys_ReportAndGroupDS dsReportAndGroup = new sys_ReportAndGroupDS();
			sys_ReportAndGroupVO voNextObject = new sys_ReportAndGroupVO();
			sys_ReportAndGroupVO voCurrentObject = new sys_ReportAndGroupVO();
			ArrayList arrReportAndGroup = new ArrayList();

			string strGroupID  = dsReportAndGroup.GetGroupIDByReportID(pstrReportID);
			arrReportAndGroup = dsReportAndGroup.GetObjectVOs(strGroupID);

			for (int i = 0; i < arrReportAndGroup.Count; i++)
			{
				sys_ReportAndGroupVO voReportAndGroup = (sys_ReportAndGroupVO)arrReportAndGroup[i];
				if (voReportAndGroup.ReportID.Equals(pstrReportID))
				{
					int intCurrentOrder = voReportAndGroup.ReportOrder;
					// if current order reach the bottom then return false
					if (intCurrentOrder >= arrReportAndGroup.Count)
					{
						blnResult = false;
					}
					else
					{
						// destination report
						voNextObject = (sys_ReportAndGroupVO)arrReportAndGroup[i+1];
						// current report
						voCurrentObject = (sys_ReportAndGroupVO)arrReportAndGroup[i];

						// switch order
						voCurrentObject.ReportOrder = voNextObject.ReportOrder;
						voNextObject.ReportOrder = intCurrentOrder;

						//update two rows into database
						dsReportAndGroup.Update(voNextObject);
						dsReportAndGroup.Update(voCurrentObject);
						blnResult = true;
					}
				}
			}
			return blnResult;
		}

		//**************************************************************************              
		///    <Description>
		///       This method uses to make a copy of specified report to another group,
		///       also copy all data relative to report (sys_ReportAndGroup, sys_ReportDrillDown,
		///       sys_ReportFields, sys_ReportPara).
		///    </Description>
		///    <Inputs>
		///        Source ReportID, Destination GroupID
		///    </Inputs>
		///    <Outputs>
		///      New report id
		///    </Outputs>
		///    <Returns>
		///       new report id
		///    </Returns>
		///    <Authors>
		///       DungLA
		///    </Authors>
		///    <History>
		///       03-Jan-2005
		///       11-Jan-2005
		///    </History>
		///    <Notes>
		///		Return newly report id
		///    </Notes>
		//**************************************************************************
	
		public object CopyReport(string pstrReportID, string pstrGroupID, out int ointReportOrder)
		{
			const string METHOD_NAME = THIS + ".CopyReport()";
			const int REPORT_ID_MAX_LENGTH = 20;
			const string CODE_DATE_FORMAT = "yyyyMMddHHmmssfff";
			UtilsBO boUtils = new UtilsBO();
			sys_ReportDS dsReport = new sys_ReportDS();
			sys_ReportVO voReport;
			
			// use to add new report to selected group
			sys_ReportAndGroupVO voReportAndGroup = new sys_ReportAndGroupVO();
			sys_ReportAndGroupDS dsReportAndGroup = new sys_ReportAndGroupDS();

			// use to copy report para
			sys_ReportParaDS dsReportPara = new sys_ReportParaDS();

			// use to copy report fields
			sys_ReportFieldsDS dsReportFields = new sys_ReportFieldsDS();

			// use to copy drill down report
			sys_ReportDrillDownDS dsReportDrillDown = new sys_ReportDrillDownDS();
			
			// get the data of selected object
			voReport = (sys_ReportVO) (dsReport.GetObjectVO(pstrReportID));

			#region Copy report
			// make a copy report
			sys_ReportVO voCopiedReport = new sys_ReportVO();
			voCopiedReport = voReport;
			// get database server date time
			DateTime dtmDB = boUtils.GetDBDate();
			// report ID = yyyyMMddHHmmssfff
			voCopiedReport.ReportID = dtmDB.ToString(CODE_DATE_FORMAT);
			if (voCopiedReport.ReportID.Length > REPORT_ID_MAX_LENGTH)
				throw new PCSBOException(ErrorCode.MESSAGE_VALUE_TOO_LONG, METHOD_NAME, new Exception());
			voCopiedReport.ReportName = Constants.COPY_OF + voCopiedReport.ReportName;
			// save new report to database
			dsReport.Add(voCopiedReport);
			#endregion

			#region Add new report to group
			voReportAndGroup.GroupID = pstrGroupID;
			voReportAndGroup.ReportID = voCopiedReport.ReportID;
			// increase report order by one in group.
			voReportAndGroup.ReportOrder = dsReportAndGroup.GetMaxReportOrder(pstrGroupID) + 1;
			// save data
			dsReportAndGroup.Add(voReportAndGroup);
			ointReportOrder = voReportAndGroup.ReportOrder;
			#endregion

			#region Copy all data relative from old report to new report
			
			#region ReportPara
			// get all parameter(s)
			sys_ReportParaVO voReportPara;
			ArrayList arrParas = dsReportPara.GetObjectVOs(pstrReportID);
			// make a copy of each parameter
			if (arrParas.Count > 0)
			{
				for (int i = 0; i < arrParas.Count; i++)
				{
					voReportPara = (sys_ReportParaVO)(arrParas[i]);
					// assign new report id
					voReportPara.ReportID = voCopiedReport.ReportID;
					// save new para
					dsReportPara.Add(voReportPara);
				}
			}
			#endregion
			
			#region ReportFields
			// get all report fields
			sys_ReportFieldsVO voReportFields;
			ArrayList arrFields = dsReportFields.GetObjectVOs(pstrReportID);
			// make a copy of each field
			if (arrFields.Count > 0)
			{
				for (int i = 0; i < arrFields.Count; i++)
				{
					voReportFields = (sys_ReportFieldsVO)arrFields[i];
					// assign new report id
					voReportFields.ReportID = voCopiedReport.ReportID;
					// save new field
					dsReportFields.Add(voReportFields);
				}
			}
			#endregion
			
			#region ReportDrillDown
			// get all drill down report
			sys_ReportDrillDownVO voReportDrillDown;
			ArrayList arrDrillDown = dsReportDrillDown.GetObjectVOs(pstrReportID);
			// make a copy each drill down report
			if (arrDrillDown.Count > 0)
			{
				for (int i = 0; i < arrDrillDown.Count; i++)
				{
					voReportDrillDown = (sys_ReportDrillDownVO)arrDrillDown[i];
					// assign new report id
					voReportDrillDown.MasterReportID = voCopiedReport.ReportID;
					// save new drill down
					dsReportDrillDown.Add(voReportDrillDown);
				}
			}
			#endregion
			
			#endregion 

			return voCopiedReport;
		}

		//**************************************************************************              
		///    <Description>
		///       This method uses to get max report order of specified group).
		///    </Description>
		///    <Inputs>
		///        GroupID
		///    </Inputs>
		///    <Outputs>
		///      Max order
		///    </Outputs>
		///    <Returns>
		///       int
		///    </Returns>
		///    <Authors>
		///       DungLA
		///    </Authors>
		///    <History>
		///       03-Jan-2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
	
		public int GetMaxReportOrder(string pstrGroupID)
		{
			sys_ReportAndGroupDS dsReportAndGroup = new sys_ReportAndGroupDS();
			return dsReportAndGroup.GetMaxReportOrder(pstrGroupID);
		}

		//**************************************************************************              
		///    <Description>
		///      This method uses to get all report
		///    </Description>
		///    <Inputs>
		///               
		///    </Inputs>
		///    <Outputs>
		///		ArrayList
		///    </Outputs>
		///    <Returns>
		///       ArrayList
		///    </Returns>
		///    <Authors>
		///       DungLA 
		///    </Authors>
		///    <History>
		///		06-Jan-2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
	
		public ArrayList GetAllReports()
		{
			sys_ReportDS dsSysReport = new sys_ReportDS();
			return dsSysReport.GetAllReports();
		}

		/// <summary>
		/// Get all report in a group
		/// </summary>
		/// <param name="pstrGroupID">Group ID</param>
		/// <returns>List of Report in a group</returns>
	
		public ArrayList GetAllReports(string pstrGroupID)
		{
			sys_ReportDS dsSysReport = new sys_ReportDS();
			return dsSysReport.GetAllReports(pstrGroupID);
		}

		//**************************************************************************              
		///    <Description>
		///       This method uses to get the ReportName of ReportID in sys_Report
		///    </Description>
		///    <Inputs>
		///        ReportID
		///    </Inputs>
		///    <Outputs>
		///        ReportName
		///    </Outputs>
		///    <Returns>
		///       string
		///    </Returns>
		///    <Authors>
		///       DungLA
		///    </Authors>
		///    <History>
		///       20-Jan-2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
	
		public string GetReportName(string pstrReportID)
		{
			sys_ReportDS dsReport = new sys_ReportDS();
			return dsReport.GetReportName(pstrReportID);
		}
		//**************************************************************************              
		///    <Description>
		///       This method uses to get all role of user
		///    </Description>
		///    <Inputs>
		///        user id
		///    </Inputs>
		///    <Outputs>
		///        array list
		///    </Outputs>
		///    <Returns>
		///       ArrayList
		///    </Returns>
		///    <Authors>
		///       DungLA
		///    </Authors>
		///    <History>
		///       06-Apr-2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
	
		public ArrayList GetRoles(string pstrUserName)
		{
			int intUserID = new Sys_UserDS().GetUserIDByUserName(pstrUserName);
			Sys_UserToRoleDS dsUserRole = new Sys_UserToRoleDS();
			return dsUserRole.ListRoleByUser(intUserID);
		}
		//**************************************************************************              
		///    <Description>
		///       This method uses to check if user has right to view this report or not
		///    </Description>
		///    <Inputs>
		///        ReportID
		///    </Inputs>
		///    <Outputs>
		///        bool
		///    </Outputs>
		///    <Returns>
		///       true if ok, false if not
		///    </Returns>
		///    <Authors>
		///       DungLA
		///    </Authors>
		///    <History>
		///       06-Apr-2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
	
		public bool HasRight(string pstrReportID, string pstrUserName)
		{
			Sys_Report_RoleDS dsReportRole = new Sys_Report_RoleDS();
			return dsReportRole.HasRight(pstrReportID, GetRoles(pstrUserName));
		}
		#endregion

		/// <summary>
		/// Get sys_ReportAndGroupVO object
		/// </summary>
		/// <param name="pstrReportID">ReportID</param>
		/// <param name="pstrGroupID">GroupID</param>
		/// <returns>sys_ReportAndGroupVO</returns>
	
		public object GetReportAndGroupObject(string pstrReportID, string pstrGroupID)
		{
			sys_ReportAndGroupDS dsRnG = new sys_ReportAndGroupDS();
			return dsRnG.GetObjectVO(pstrReportID, pstrGroupID);
		}
		/// <summary>
		/// Swap report order
		/// </summary>
		/// <param name="pobjSourceReport">Source Report</param>
		/// <param name="pobjDestReport">Destination Report</param>
	
		public void SwapReport(object pobjSourceReport, object pobjDestReport)
		{
			sys_ReportAndGroupDS dsReportAndGroup = new sys_ReportAndGroupDS();
			dsReportAndGroup.Update(pobjSourceReport);
			dsReportAndGroup.Update(pobjDestReport);
		}
		/// <summary>
		/// Swap group order
		/// </summary>
		/// <param name="pobjSourceGroup">Source Group</param>
		/// <param name="pobjDestGroup">Destination Group</param>
	
		public void SwapGroup(object pobjSourceGroup, object pobjDestGroup)
		{
			sys_ReportGroupDS dsReportGroup = new sys_ReportGroupDS();
			// update object
			dsReportGroup.Update(pobjSourceGroup);
			dsReportGroup.Update(pobjDestGroup);
		}
	}
}
