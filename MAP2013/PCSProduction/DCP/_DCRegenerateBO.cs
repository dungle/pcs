using System;
using System.Collections;
using System.Data;
using System.Diagnostics;

using System.IO;
using System.Reflection;
using System.Windows.Forms;
using C1.Data;
using C1.Win.C1TrueDBGrid;
using PCSComMaterials.Plan.DS;
using PCSComUtils.Common;
//
using PCSComProduction.DCP.DS;
using PCSComUtils.PCSExc;

using PCSComProduction.WorkOrder.DS;
using PCSUtils.Log;

namespace PCSComProduction.DCP.BO
{
	public interface IDCRegenerateBO
	{
		void RunDCP(int pintCCNID, int pintDCOptionMasterID, DataTable pdtbOverCapacityWC);
		int GetLackOfYearInCalendar(int pintDCOptionMasterID);
		string[] GetFromYearToYear(int pintDCOptionMasterID);
		string[] GetWorkCenterNotConfig(int pintDCOptionMasterID);
		DataTable GetInvalidWOLineAndCPO(int pintDCOptionMasterID);
	}
	/// <summary>
	/// Summary description for DCRegenerateBO.
	/// </summary>
	//
	//
	public class DCRegenerateBO  //: System.EnterpriseServices.ServicedComponent, IDCRegenerateBO
	{
		#region Constants
		const string THIS = "PCSComProduction.DCP.BO.DCRegenerateBO";
		const string TOTALWORKTIME = "TotalWorkTime";
		const string DATE_FLD = "Date";
		const string WEEKDAY_FLD = "WeekDay";
		const string ISOFFDAY_FLD = "IsOffDay";
		const string DATEPARTDD_FLD = "DatePartDD";
		const double EPSILON = 0.00001;

		const string TBL_INDEX = "IndexTable";

		/// <summary>
		/// EXCEPTED_TIME will update to function
		/// </summary>
		const double EXCEPTED_TIME = 1;
		const string FROM_FLD = "From";
		const string TO_FLD = "To";
	
		const string MASTERID_FLD = "MasterID";
		const string DETAILID_FLD = "DetailID";
		const string LEADTIME_FLD = "LeadTime";
		const string OVERLEADTIME_FLD = "OverLeadTime";
		const string OVERDAYS_FLD = "OverDays";
		const string OVERPERCENT_FLD = "OverPercent";
		const string WORKCENTERCODE_FLD = "WorkCenterCode";
		const string DEPARTMENTCODE_FLD = "DepartmentCode";
		const string STOPTIMETABLE_FLD = "StopTimeTable";
		const string CHECKPOINTPERITEM_FLD = "CheckpointPerItem";
		
		//Free time datatable
		const string STARTFREETIME_FLD = "StartTime";
		const string ENDFREETIME_FLD = "EndTime";

		//remain capacity datatable
		const string WORKINGDAY_FLD = "WorkingDay";
		const string WORKINGDAYSTART_FLD = "WorkingDayStart";
		const string WORKINGDAYEND_FLD = "WorkingDayEnd";
		const string REMAINCAPACITY_FLD = "RemainCapacity";

		const string LEADTIME = "LeadTime";
		const string STARTDATETIME = "StartDateTime";
		const string DUEDATETIME = "DueDateTime";
		const string MASTERID = "MasterID";
		const string GETOUT = "GetOut";

		private decimal MINPRODUCE_SCALE = new decimal(1.0);

		private string PGPRIORITY_FLD = "PGPriority";

		private static bool m_blnRoundedQuantity = true;

		#endregion Constants

		#region Not implemented

		public DCRegenerateBO()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		/// <summary>
		/// Insert a new record into database
		/// </summary>
		public void Add(object pObjectDetail)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Delete record by condition
		/// </summary>
		public void Delete(object pObjectVO)
		{
			throw new NotImplementedException();
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

		#endregion

		#region Old version
		public DataTable ListDCOption()
		{
			PRO_DCOptionMasterDS dsDC = new PRO_DCOptionMasterDS();
			return dsDC.List().Tables[0];
		}
		
		private void AssignPGPriority(DataTable pdtbProduct, DataTable pdtbProductionGroup)
		{
			DataRow[] arrProducts = pdtbProduct.Select(string.Empty);
			foreach (DataRow drowProduct in arrProducts)
			{
				int intProductID = Convert.ToInt32(drowProduct[ITM_ProductTable.PRODUCTID_FLD]);
				//select group
				DataRow[] arrGroup = pdtbProductionGroup.Select(PRO_PGProductTable.PRODUCTID_FLD + "=" + intProductID,PRO_ProductionGroupTable.PRIORITY_FLD + " ASC");
				if (arrGroup.Length > 0)
				{
					drowProduct[PGPRIORITY_FLD] = arrGroup[0][PRO_ProductionGroupTable.PRIORITY_FLD];
				}
				else
				{
					drowProduct[PGPRIORITY_FLD] = 100;//Int32.MaxValue;
				}
			}
		}

		/// <summary>
		/// Processing DCP
		/// </summary>
		/// <param name="pintCCNID"></param>
		/// <param name="pintDCOptionMasterID"></param>
		/// <author>sonht</author>
	
		public int RunDCP(int pintCCNID, int pintDCOptionMasterID, DataTable pdtbOverCapacityWC)
		{
			#region Prepare datas
			PRO_DCOptionMasterDS dsDCOptionMaster = new PRO_DCOptionMasterDS();
			DataRow drowDCOption = dsDCOptionMaster.GetDCOption(pintDCOptionMasterID).Tables[0].Rows[0];
			
			DataTable dtbDelSchData = dsDCOptionMaster.GetDeliveryScheduleData(pintDCOptionMasterID);
			DataTable dtbDelSchTime = dsDCOptionMaster.GetDeliveryScheduleTime(pintDCOptionMasterID);
			DataTable dtbWCConfig = dsDCOptionMaster.GetWCConfigInCycle(pintDCOptionMasterID);
			DataTable dtbWCList = GetWorkCenterList(pintDCOptionMasterID);
			DataTable dtbChangeCategory = dsDCOptionMaster.GetChangeCategory(pintDCOptionMasterID);
			DataTable dtbBOMInfo = dsDCOptionMaster.GetBOMInfo(pintDCOptionMasterID);
			DataSet dstDCPResult = dsDCOptionMaster.GetResultDataSet(false);
			DataTable dtbProduct = dsDCOptionMaster.GetProductInfo(pintDCOptionMasterID);
			DataTable dtbAvailQty = dsDCOptionMaster.GetAvailQuantity();
			DataTable dtbRawResult = CreateRawResultTable();
			DataTable dtbProductionGroup = dsDCOptionMaster.GetProductionGroup();
			DataTable dtbProductPair = dsDCOptionMaster.GetProductPair();
			DataTable dtbPlanningStartDate = dsDCOptionMaster.SelectPlanningStartDate(pintDCOptionMasterID);
			// dungla 25-09-2006: begin data for other purpose
			DataTable dtbBeginData = dsDCOptionMaster.GetBeginData(pintDCOptionMasterID);

			ReCalculateLeadTime(dtbDelSchData,dtbBOMInfo,dtbProduct,Convert.ToInt32(drowDCOption[PRO_DCOptionMasterTable.INCLUDECHECKPOINT_FLD]) == 1);
			AssignPGPriority(dtbProduct,dtbProductionGroup);

			//close all released workorder
			//dsDCOptionMaster.CloseWorkOrderInCycle(pintDCOptionMasterID);
			#endregion

			#region Sort production line
			DataTable dtbTopLevelWC = dsDCOptionMaster.GetTopLevelWorkCenter();
			SortWorkCenterList(dtbTopLevelWC,dtbWCList,dtbBOMInfo);
			#endregion

			#region Feed bottles algorithm
			DataSet dstCapacityBottles = CreateCapacityBottles(dtbWCList,dtbWCConfig,drowDCOption,dtbDelSchTime);
			int intResult = AdjustDeliveryAndBottles(drowDCOption,dstCapacityBottles,dtbDelSchData,dtbWCList,
				dtbChangeCategory,dtbBOMInfo,dstDCPResult,dtbWCConfig,dtbProduct,dtbAvailQty,
				dtbRawResult,dtbProductionGroup,dtbProductPair, ref dtbBeginData);
			#endregion
			
			#region Collect over capacity
			CollectOverCapacity(pdtbOverCapacityWC,dtbDelSchData,drowDCOption,dtbWCConfig,dtbWCList,dtbRawResult,dtbBOMInfo,dtbProduct);
			#endregion

			#region Write final results
			WriteFinalResult(pintDCOptionMasterID,dtbRawResult,dstCapacityBottles,dtbWCList,dtbWCConfig,dstDCPResult,dtbChangeCategory,Convert.ToBoolean(drowDCOption[PRO_DCOptionMasterTable.INCLUDECHECKPOINT_FLD]),Convert.ToDateTime(drowDCOption[PRO_DCOptionMasterTable.ASOFDATE_FLD]));
			#endregion Write final results

			#region Update DCP result
			
			dsDCOptionMaster.DeleteDCPResult(pintDCOptionMasterID);
			dsDCOptionMaster.UpdateResultDataSet(dstDCPResult);
			dsDCOptionMaster.SetDCOptionLastUpdate(pintDCOptionMasterID);

			foreach (DataRow drowPlanningOfs in dtbPlanningStartDate.Rows)
			{
				//find workcenter
				DataRow drowWC;
				try
				{
					drowWC = dtbWCList.Select(PRO_PlanningOffsetTable.PLANNINGOFFSETID_FLD + "=" + drowPlanningOfs[PRO_PlanningOffsetTable.PLANNINGOFFSETID_FLD])[0];
					drowPlanningOfs[PRO_PlanningOffsetTable.PLANNINGSTARTDATE_FLD] = drowWC[PRO_PlanningOffsetTable.PLANNINGSTARTDATE_FLD];
					int intWorkCenterID = Convert.ToInt32(drowPlanningOfs[MST_WorkCenterTable.WORKCENTERID_FLD]);
					//close workorders
					dsDCOptionMaster.CloseWorkOrderInCycle(pintDCOptionMasterID,intWorkCenterID,Convert.ToDateTime(drowPlanningOfs[PRO_PlanningOffsetTable.PLANNINGSTARTDATE_FLD]));
				}
				catch (Exception ex)
				{
					ex.ToString();
				}
			}
			dsDCOptionMaster.UpdatePlanningStartDate(dtbPlanningStartDate);
			dsDCOptionMaster.UpdateBeginData(dtbBeginData);
			#endregion

			return intResult;
		}


		/// <summary>
		/// Build grid capacity to scan
		/// Return DataTable with:
		/// Date, WeekDay, IsOffDay, WC1, WC2, ..., WCn
		/// </summary>
		/// <param name="pdtmStartDate"></param>
		/// <param name="pintMaxDay"></param>
		/// <param name="pdtbWCCapacity"></param>
		/// <param name="pdstWDCalendar"></param>
		/// <returns></returns>
		private DataTable CreateInitCapacityByDate(DateTime pdtmStartDate,
			int pintMaxDay,
			DataTable pdtbWCCapacity,
			DataSet pdstWDCalendar)
		{
			// const string TOTALWORKINGTIME_FLD = "TotalWorkingTime";
			DataRow[] drowCalendars = pdstWDCalendar.Tables[0].Select("",MST_WorkingDayMasterTable.YEAR_FLD + " DESC");
			int intStartYear = pdtmStartDate.Year;
			int intEndYear = pdtmStartDate.AddDays(pintMaxDay).Year;
			if(drowCalendars.Length > 0)
			{
				intStartYear = int.Parse(drowCalendars[drowCalendars.Length-1][MST_WorkingDayMasterTable.YEAR_FLD].ToString());
				intEndYear = int.Parse(drowCalendars[0][MST_WorkingDayMasterTable.YEAR_FLD].ToString());
			}

			DataTable dtbData = new DataTable();
			dtbData.Columns.Add(DATE_FLD,typeof(DateTime));
			dtbData.Columns.Add(WEEKDAY_FLD,typeof(int));
			dtbData.Columns.Add(ISOFFDAY_FLD,typeof(bool));
			// Create table structure
			foreach(DataRow drow in pdtbWCCapacity.Rows)
			{
				dtbData.Columns.Add(drow[MST_WorkCenterTable.CODE_FLD].ToString(),typeof(decimal));
				dtbData.Columns[drow[MST_WorkCenterTable.CODE_FLD].ToString()].DefaultValue = 0;
			}

			InsertIntoCurrentCapacity(intStartYear,intEndYear,ref dtbData,pdstWDCalendar,pdtbWCCapacity);

			return dtbData;
		}

		/// <summary>
		/// Create table current capacity
		/// </summary>
		/// <param name="pintStartYear"></param>
		/// <param name="pintEndYear"></param>
		/// <param name="pdtbCurrentCapacity"></param>
		/// <param name="pdstWDCalendar"></param>
		private void InsertIntoCurrentCapacity(int pintStartYear,
			int pintEndYear,
			ref DataTable pdtbCurrentCapacity,
			DataSet pdstWDCalendar,
			DataTable pdtbWCCapacity)
		{
			const string DATE_FLD = "Date";
			const string WEEKDAY_FLD = "WeekDay";
			const string ISOFFDAY_FLD = "IsOffDay";
			const int FIRST_WC = 3;
			DateTime dtmCurrent = new DateTime(pintStartYear,1,1);
			DateTime dtmEndDate = new DateTime(pintEndYear,12,31);
			// Add date & other informations into table
			while (dtmCurrent < dtmEndDate)
			{
				DataRow drow = pdtbCurrentCapacity.NewRow();
				drow[DATE_FLD] = dtmCurrent;
				drow[WEEKDAY_FLD] = dtmCurrent.DayOfWeek;
				drow[ISOFFDAY_FLD] = IsOffDay(dtmCurrent,pdstWDCalendar);
				// fill init capacity
				for(int i = FIRST_WC; i < pdtbCurrentCapacity.Columns.Count; i++)
				{
					// Fill data for all col except 3 first columns
					DataColumn dcol = pdtbCurrentCapacity.Columns[i];
					DataRow[]drowWCCapacitys = pdtbWCCapacity.Select(PRO_WCCapacityTable.BEGINDATE_FLD + "<= '" + dtmCurrent + "'" +
						" AND " + PRO_WCCapacityTable.ENDDATE_FLD + ">= '" + dtmCurrent + "'" +
						" AND Code " + "= '" + dcol.ColumnName + "'");
					if(drowWCCapacitys.Length == 1)
					{
						drow[dcol.ColumnName] = drowWCCapacitys[0][PRO_WCCapacityTable.CAPACITY_FLD];
					}
					else if(drowWCCapacitys.Length > 1)
					{
						// throw: Overlap config WC
						throw new PCSBOException();
					}
				}
				
				pdtbCurrentCapacity.Rows.Add(drow);
				// Next row
				dtmCurrent = dtmCurrent.AddDays(1);
			}

		}
		/// <summary>
		/// Create 2 tables virtual Item and Routing
		/// </summary>
		/// <param name="pdstItemRoutingBasic"></param>
		/// <returns></returns>
		private DataSet CreateItemRouting(DataSet pdstItemRoutingBasic)
		{
			DataSet dstData = new DataSet();
			DataTable dtbItem = pdstItemRoutingBasic.Tables[0].Copy();
			dstData.Tables.Add(dtbItem);
			DataTable dtbRouting = new DataTable(ITM_RoutingTable.TABLE_NAME);
			// Add column
			foreach(DataColumn dcol in pdstItemRoutingBasic.Tables[ITM_RoutingTable.TABLE_NAME].Columns)
			{
				dtbRouting.Columns.Add(dcol.ColumnName, dcol.DataType);
			}
			int intMasterID = 0;
			foreach(DataRow drowItem in dtbItem.Rows)
			{
				// Update masterid
				drowItem[MASTERID_FLD] = ++intMasterID;
				string strWorkOrderDetailID = drowItem[PRO_WorkOrderDetailTable.WORKORDERDETAILID_FLD].ToString();
				string strProductID = drowItem[PRO_WorkOrderDetailTable.PRODUCTID_FLD].ToString();
				string strSelect = string.Empty;
				if(strWorkOrderDetailID != string.Empty)
				{
					strSelect = PRO_WORoutingTable.WORKORDERDETAILID_FLD + "=" + strWorkOrderDetailID ;
					// + " AND " + PRO_WorkOrderDetailTable.PRODUCTID_FLD + "=" + strProductID;
				}
				else
				{
					strSelect = PRO_WorkOrderDetailTable.WORKORDERDETAILID_FLD + " IS NULL "
						+ " AND " + PRO_WorkOrderDetailTable.PRODUCTID_FLD + "=" + strProductID;
				}
				DataRow[] drowRoutings = pdstItemRoutingBasic.Tables[ITM_RoutingTable.TABLE_NAME].Select(strSelect);
				foreach(DataRow drowRouting in drowRoutings)
				{
					drowRouting[MASTERID_FLD] = intMasterID;

					#region // HACK: DEL SonHT 2005-11-01

					//					drowRouting[CHECKPOINTPERITEM_FLD] = decimal.Parse(drowRouting[CHECKPOINTPERITEM_FLD].ToString()) * decimal.Parse(drowItem[MTR_CPOTable.QUANTITY_FLD].ToString());
					//					drowRouting[LEADTIME_FLD] = decimal.Parse(drowRouting[LEADTIME_FLD].ToString()) * decimal.Parse(drowItem[MTR_CPOTable.QUANTITY_FLD].ToString());
					//					if(drowRouting[PRO_CheckPointTable.SAMPLEPATTERN_FLD].ToString() == DistributionMethodEnum.ByQuantity.GetHashCode().ToString())
					//					{
					//						drowRouting[LEADTIME_FLD] = decimal.Parse(drowRouting[LEADTIME_FLD].ToString()) + decimal.Parse(drowRouting[CHECKPOINTPERITEM_FLD].ToString());
					//					}
					//					else if(drowRouting[PRO_CheckPointTable.SAMPLEPATTERN_FLD].ToString() == DistributionMethodEnum.ByLeadTime.GetHashCode().ToString())
					//					{
					//						drowRouting[LEADTIME_FLD] = decimal.Parse(drowRouting[LEADTIME_FLD].ToString()) 
					//											+ (decimal.Parse(drowRouting[LEADTIME_FLD].ToString())
					//											/decimal.Parse(drowItem[MTR_CPOTable.QUANTITY_FLD].ToString()) 
					//											* decimal.Parse(drowRouting[CHECKPOINTPERITEM_FLD].ToString()));
					//					}
					

					#endregion // END: DEL SonHT 2005-11-01

					DataRow drowNew = dtbRouting.NewRow();
					drowNew.ItemArray = drowRouting.ItemArray;

					/*drowNew[CHECKPOINTPERITEM_FLD] = decimal.Parse(drowRouting[CHECKPOINTPERITEM_FLD].ToString()) 
									* decimal.Parse(drowItem[MTR_CPOTable.QUANTITY_FLD].ToString());*/
					drowNew[LEADTIME_FLD] = decimal.Parse(drowNew[LEADTIME_FLD].ToString()) 
						* decimal.Parse(drowItem[MTR_CPOTable.QUANTITY_FLD].ToString());
					/*if(drowNew[PRO_CheckPointTable.SAMPLEPATTERN_FLD].ToString() == DistributionMethodEnum.ByQuantity.GetHashCode().ToString())
					{
						drowNew[LEADTIME_FLD] = decimal.Parse(drowNew[LEADTIME_FLD].ToString()) 
									+ decimal.Parse(drowNew[CHECKPOINTPERITEM_FLD].ToString());
					}
					else if(drowNew[PRO_CheckPointTable.SAMPLEPATTERN_FLD].ToString() == DistributionMethodEnum.ByLeadTime.GetHashCode().ToString())
					{
						drowNew[LEADTIME_FLD] = decimal.Parse(drowNew[LEADTIME_FLD].ToString()) 
							+ (decimal.Parse(drowNew[LEADTIME_FLD].ToString())
							/decimal.Parse(drowItem[MTR_CPOTable.QUANTITY_FLD].ToString()) 
							* decimal.Parse(drowNew[CHECKPOINTPERITEM_FLD].ToString()));
					}*/
					dtbRouting.Rows.Add(drowNew);
				}
			}
			// Set Detail ID
			int i = 0;
			foreach(DataRow drow in dtbRouting.Rows)
			{
				drow[DETAILID_FLD] = ++i;
			}
			dstData.Tables.Add(dtbRouting);
			return dstData;
		}
		
		/// <summary>
		/// Check the day is off day
		/// Return true if isoffday else return false
		/// </summary>
		/// <param name="pdtmDate"></param>
		/// <param name="pdstCalendar"></param>
		/// <returns></returns>
		private bool IsOffDay(DateTime pdtmDate, DataSet pdstCalendar)
		{
			const string METHOD_NAME = THIS + ".IsOffDay()";
			DayOfWeek objWeekDay = pdtmDate.DayOfWeek;
			DataRow[] drows = pdstCalendar.Tables[0].Select(MST_WorkingDayMasterTable.YEAR_FLD + " = " + pdtmDate.Year);
			DataRow[] drowDetails = pdstCalendar.Tables[MST_WorkingDayDetailTable.TABLE_NAME].Select(MST_WorkingDayDetailTable.OFFDAY_FLD + " = '" + pdtmDate.Date + "'");
			if(drowDetails.Length > 0)
			{
				return true;
			}
			if(drows.Length == 0) // return true;
			{
				throw new PCSBOException(ErrorCode.MESSAGE_DCP_SETTING_WORKING_CALENDAR,METHOD_NAME,null);
			}
			if(objWeekDay == DayOfWeek.Sunday)
			{
				if(bool.Parse(drows[0][MST_WorkingDayMasterTable.SUN_FLD].ToString()) == true) return false;
			}
			else if(objWeekDay == DayOfWeek.Saturday)
			{
				if(bool.Parse(drows[0][MST_WorkingDayMasterTable.SAT_FLD].ToString()) == true) return false;
			}
			else if(objWeekDay == DayOfWeek.Friday)
			{
				if(bool.Parse(drows[0][MST_WorkingDayMasterTable.FRI_FLD].ToString()) == true) return false;
			}
			else if(objWeekDay == DayOfWeek.Thursday)
			{
				if(bool.Parse(drows[0][MST_WorkingDayMasterTable.THU_FLD].ToString()) == true) return false;
			}
			else if(objWeekDay == DayOfWeek.Wednesday)
			{
				if(bool.Parse(drows[0][MST_WorkingDayMasterTable.WED_FLD].ToString()) == true) return false;
			}
			else if(objWeekDay == DayOfWeek.Tuesday)
			{
				if(bool.Parse(drows[0][MST_WorkingDayMasterTable.TUE_FLD].ToString()) == true) return false;
			}
			else if(objWeekDay == DayOfWeek.Monday)
			{
				if(bool.Parse(drows[0][MST_WorkingDayMasterTable.MON_FLD].ToString()) == true) return false;
			}
			return true;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="pdtmStartDate"></param>
		/// <param name="pdtmEndDate"></param>
		/// <param name="pdstCalendar"></param>
		/// <returns></returns>
		private int GetTotalWorkingDay(DateTime pdtmStartDate, DateTime pdtmEndDate, DataSet pdstCalendar)
		{			
			DataRow[] drows = pdstCalendar.Tables[0].Select(MST_WorkingDayMasterTable.YEAR_FLD + " = " + pdtmStartDate.Year);
			DataRow[] drowDetails = pdstCalendar.Tables[MST_WorkingDayDetailTable.TABLE_NAME].Select(MST_WorkingDayDetailTable.OFFDAY_FLD + " >= '" + pdtmStartDate.Date + "'" + " AND " + MST_WorkingDayDetailTable.OFFDAY_FLD + " < '" + pdtmEndDate.Date + "'");
			Hashtable htDOWCount = new Hashtable();
			for (DateTime dtm = pdtmStartDate; dtm < pdtmEndDate; dtm = dtm.AddDays(1))
			{
				DayOfWeek dow = dtm.DayOfWeek;
				string strDOW = string.Empty;
				switch (dow)
				{
					case DayOfWeek.Sunday:
						strDOW = MST_WorkingDayMasterTable.SUN_FLD;
						break;
					case DayOfWeek.Monday:
						strDOW = MST_WorkingDayMasterTable.MON_FLD;
						break;
					case DayOfWeek.Tuesday:
						strDOW = MST_WorkingDayMasterTable.TUE_FLD;
						break;
					case DayOfWeek.Wednesday:
						strDOW = MST_WorkingDayMasterTable.WED_FLD;
						break;
					case DayOfWeek.Thursday:
						strDOW = MST_WorkingDayMasterTable.THU_FLD;
						break;
					case DayOfWeek.Friday:
						strDOW = MST_WorkingDayMasterTable.FRI_FLD;
						break;
					case DayOfWeek.Saturday:
						strDOW = MST_WorkingDayMasterTable.SAT_FLD;
						break;
				}
				if (htDOWCount[strDOW] != null)
				{
					htDOWCount[strDOW] = Convert.ToInt32(htDOWCount[strDOW]) + 1;
				}
				else
				{
					htDOWCount.Add(strDOW,1);
				}
			}
			int intTotalDays = pdtmEndDate.Subtract(pdtmStartDate).Days;
			foreach (DictionaryEntry objDOW in htDOWCount)
			{
				if(Convert.ToBoolean(drows[0][objDOW.Key.ToString()]) == false)
				{
					intTotalDays -= Convert.ToInt32(objDOW.Value);
				}
			}
			return (intTotalDays - drowDetails.Length);
		}


		/// <summary>
		/// if t 
		/// </summary>
		/// <param name="pdtmTime"></param>
		/// <param name="pintWorkCenterID"></param>
		/// <param name="pdtbWCCapacityAndShift"></param>
		/// <param name="pdstCalendar"></param>
		/// <returns></returns>
		private bool IsWorkingTime(DateTime pdtmTime, 
			int pintWorkCenterID, 
			DataTable pdtbWCCapacityAndShift, 
			DataSet pdstCalendar)
		{	
			DateTime dtmDate = pdtmTime.Date;
			// Check working day calendar
			if(pdstCalendar.Tables[MST_WorkingDayMasterTable.TABLE_NAME].Select(MST_WorkingDayMasterTable.YEAR_FLD + "=" + dtmDate.Year).Length == 0)
			{
				// check year
				return false;
			}
			// Check BeginDate -> EndDate
			DataRow[] drowShifts = pdtbWCCapacityAndShift.Select(
				PRO_WCCapacityTable.BEGINDATE_FLD + " <= '" + dtmDate + "'"
				+ " AND '" + dtmDate + "' <= " + PRO_WCCapacityTable.ENDDATE_FLD
				+ " AND " + PRO_WCCapacityTable.WORKCENTERID_FLD + "=" + pintWorkCenterID);
			if(drowShifts.Length > 0)
			{
				foreach(DataRow drow in drowShifts)
				{
					// DateTime dtmEffFrom = (DateTime)drow[PRO_ShiftPatternTable.EFFECTDATEFROM_FLD];
					DateTime dtmTimeViaEffFrom = ChangeSameShift(pdtmTime, (DateTime)drow[PRO_ShiftPatternTable.WORKTIMEFROM_FLD]
						, (DateTime)drow[PRO_ShiftPatternTable.WORKTIMETO_FLD]);
					if((dtmTimeViaEffFrom >= (DateTime)drow[PRO_ShiftPatternTable.WORKTIMEFROM_FLD])
						&& (dtmTimeViaEffFrom <= (DateTime)drow[PRO_ShiftPatternTable.WORKTIMETO_FLD]))
					{

						// Check regular stop
						if((drow[PRO_ShiftPatternTable.REGULARSTOPFROM_FLD] != DBNull.Value)
							&& (drow[PRO_ShiftPatternTable.REGULARSTOPTO_FLD] != DBNull.Value))
							if((dtmTimeViaEffFrom >= (DateTime)drow[PRO_ShiftPatternTable.REGULARSTOPFROM_FLD])
								&& (dtmTimeViaEffFrom <= (DateTime)drow[PRO_ShiftPatternTable.REGULARSTOPTO_FLD]))
							{
								return false;
							}
						// Check refresh stop
						if((drow[PRO_ShiftPatternTable.REFRESHINGFROM_FLD] != DBNull.Value)
							&& (drow[PRO_ShiftPatternTable.REFRESHINGTO_FLD] != DBNull.Value))
							if((dtmTimeViaEffFrom >= (DateTime)drow[PRO_ShiftPatternTable.REFRESHINGFROM_FLD])
								&& (dtmTimeViaEffFrom <= (DateTime)drow[PRO_ShiftPatternTable.REFRESHINGTO_FLD]))
							{
								return false;
							}
						// Check extra stop
						if((drow[PRO_ShiftPatternTable.EXTRASTOPFROM_FLD] != DBNull.Value)
							&& (drow[PRO_ShiftPatternTable.EXTRASTOPTO_FLD] != DBNull.Value))
							if((dtmTimeViaEffFrom >= (DateTime)drow[PRO_ShiftPatternTable.EXTRASTOPFROM_FLD])
								&& (dtmTimeViaEffFrom <= (DateTime)drow[PRO_ShiftPatternTable.EXTRASTOPTO_FLD]))
							{
								return false;
							}

						// is working time
						return true;
					}
				}

				#region // HACK: DEL SonHT 2005-11-01

				// find if dtmTimeViaEffFrom add more 1 day
				//				foreach(DataRow drow in drowShifts)
				//				{
				//					DateTime dtmEffFrom = (DateTime)drow[PRO_ShiftPatternTable.EFFECTDATEFROM_FLD];
				//					DateTime dtmTimeViaEffFrom = ChangeSameDate(pdtmTime, dtmEffFrom);
				//					dtmTimeViaEffFrom = dtmTimeViaEffFrom.AddDays(1);
				//					if ((dtmTimeViaEffFrom >= (DateTime)drow[PRO_ShiftPatternTable.WORKTIMEFROM_FLD])
				//						&& (dtmTimeViaEffFrom <= (DateTime)drow[PRO_ShiftPatternTable.WORKTIMETO_FLD]))
				//					{
				//						// Check regular stop
				//						if((dtmTimeViaEffFrom >= (DateTime)drow[PRO_ShiftPatternTable.REGULARSTOPFROM_FLD])
				//							&& (dtmTimeViaEffFrom <= (DateTime)drow[PRO_ShiftPatternTable.REGULARSTOPTO_FLD]))
				//						{
				//							return false;
				//						}
				//						// Check refresh stop
				//						if((dtmTimeViaEffFrom >= (DateTime)drow[PRO_ShiftPatternTable.REFRESHINGFROM_FLD])
				//							&& (dtmTimeViaEffFrom <= (DateTime)drow[PRO_ShiftPatternTable.REFRESHINGTO_FLD]))
				//						{
				//							return false;
				//						}
				//						// Check extra stop
				//						if((dtmTimeViaEffFrom >= (DateTime)drow[PRO_ShiftPatternTable.EXTRASTOPFROM_FLD])
				//							&& (dtmTimeViaEffFrom <= (DateTime)drow[PRO_ShiftPatternTable.EXTRASTOPTO_FLD]))
				//						{
				//							return false;
				//						}
				//						// is working time
				//						return true;
				//					}
				//				}
				

				#endregion // END: DEL SonHT 2005-11-01
			}
			return false;
		}
		/// <summary>
		/// Get near prev working time
		/// </summary>
		/// <param name="pdtmTime"></param>
		/// <param name="pintWorkCenterID"></param>
		/// <param name="pdtbWCCapacityAndShift"></param>
		/// <param name="pdstCalendar"></param>
		/// <returns></returns>
		private DateTime GetPreviousWorkingTime(DateTime pdtmTime, 
			int pintWorkCenterID, 
			string pstrWorkCenter, 
			DataTable pdtbWCCapacityAndShift, 
			DataSet pdstCalendar, 
			ref DataTable pdtbCurrentCapacity)
		{
			const string METHOD_NAME = THIS + ".GetPreviousWorkingTime()";

			DateTime dtmDate = pdtmTime.Date;
			// Check working day calendar
			if(pdstCalendar.Tables[MST_WorkingDayMasterTable.TABLE_NAME].Select(MST_WorkingDayMasterTable.YEAR_FLD + "=" + dtmDate.Year).Length == 0)
			{
				// check year
				// Message: Setting working calendar for the year @, please
				throw new PCSBOException(ErrorCode.MESSAGE_DCP_SETTING_WORKING_CALENDAR,METHOD_NAME,null);
			}
			// Check BeginDate -> EndDate
			DataRow[] drows = pdtbWCCapacityAndShift.Select(
				"'" + dtmDate + "' >= " + PRO_WCCapacityTable.BEGINDATE_FLD 
				+ " AND '" + dtmDate + "' <= " + PRO_WCCapacityTable.ENDDATE_FLD 
				+ " AND " + PRO_WCCapacityTable.WORKCENTERID_FLD + "=" + pintWorkCenterID,
				PRO_ShiftPatternTable.WORKTIMEFROM_FLD + " DESC");
			if(drows.Length > 0)
			{
				DateTime dtmWorkTimeFrom = (DateTime)drows[drows.Length-1][PRO_ShiftPatternTable.WORKTIMEFROM_FLD];
				DateTime dtmWorkTimeTo = (DateTime)drows[0][PRO_ShiftPatternTable.WORKTIMETO_FLD];
				double dblTotalSecond = (dtmWorkTimeTo - dtmWorkTimeFrom).TotalSeconds;
				dtmDate = GetDateOnly(pdtmTime, dtmWorkTimeFrom, dtmWorkTimeTo);
				DateTime dtmFrom = new DateTime(dtmDate.Year, dtmDate.Month, dtmDate.Day,
					dtmWorkTimeFrom.Hour, dtmWorkTimeFrom.Minute, dtmWorkTimeFrom.Second);
				DateTime dtmTo = dtmFrom.AddSeconds(dblTotalSecond);
				
				DataTable dtbStopTime = GetStopTime(drows);

				#region // HACK: DEL SonHT 2005-11-01

				//				foreach(DataRow drow in dtbStopTime.Rows)
				//				{
				// DateTime dtmEffFrom = (DateTime)drow[PRO_ShiftPatternTable.EFFECTDATEFROM_FLD];
				//					DateTime dtmTimeViaEffFrom = ChangeSameShift(pdtmTime, (DateTime)drow[PRO_ShiftPatternTable.WORKTIMEFROM_FLD]
				//																, (DateTime)drow[PRO_ShiftPatternTable.WORKTIMETO_FLD]);
				//					DateTime dtmWorkTimeFrom = (DateTime)drow[PRO_ShiftPatternTable.WORKTIMEFROM_FLD];
				//					DateTime dtmWorkTimeTo = (DateTime)drow[PRO_ShiftPatternTable.WORKTIMETO_FLD];
				//					double dblTotalSecond = (dtmWorkTimeTo - dtmWorkTimeFrom).TotalSeconds;
				//					dtmDate = GetDateOnly(pdtmTime, dtmWorkTimeFrom, dtmWorkTimeTo);
				//					dtmWorkTimeFrom = new DateTime(dtmDate.Year, dtmDate.Month, dtmDate.Day,
				//										dtmWorkTimeFrom.Hour, dtmWorkTimeFrom.Minute, dtmWorkTimeFrom.Second);
				//					dtmWorkTimeTo = dtmWorkTimeFrom.AddSeconds(dblTotalSecond);
				// inside From -> To
				/*
									if((pdtmTime >= dtmWorkTimeFrom) && (pdtmTime <= dtmWorkTimeTo))
									{

										// Check regular stop
										if((drow[PRO_ShiftPatternTable.REGULARSTOPFROM_FLD] != DBNull.Value)
											&& (drow[PRO_ShiftPatternTable.REGULARSTOPTO_FLD] != DBNull.Value))
										{
											DateTime dtmRegularFrom = dtmWorkTimeFrom.AddSeconds(((DateTime)drow[PRO_ShiftPatternTable.REGULARSTOPFROM_FLD]
																		- (DateTime)drow[PRO_ShiftPatternTable.WORKTIMEFROM_FLD]).TotalSeconds);
											DateTime dtmRegularTo = dtmWorkTimeFrom.AddSeconds(((DateTime)drow[PRO_ShiftPatternTable.REGULARSTOPTO_FLD]
																		- (DateTime)drow[PRO_ShiftPatternTable.WORKTIMEFROM_FLD]).TotalSeconds);
								
											if((pdtmTime >= dtmRegularFrom)&& (pdtmTime <= dtmRegularTo))
											{
												return dtmRegularFrom;
											}
										}
										// Check refresh stop
										if ((drow[PRO_ShiftPatternTable.REFRESHINGFROM_FLD] != DBNull.Value)
											&& (drow[PRO_ShiftPatternTable.REFRESHINGTO_FLD] != DBNull.Value))
										{
											DateTime dtmRefreshFrom = dtmWorkTimeFrom.AddSeconds(((DateTime)drow[PRO_ShiftPatternTable.REFRESHINGFROM_FLD]
																			- (DateTime)drow[PRO_ShiftPatternTable.WORKTIMEFROM_FLD]).TotalSeconds);
											DateTime dtmRefreshTo = dtmWorkTimeFrom.AddSeconds(((DateTime)drow[PRO_ShiftPatternTable.REFRESHINGTO_FLD]
																			- (DateTime)drow[PRO_ShiftPatternTable.WORKTIMEFROM_FLD]).TotalSeconds);
											if((pdtmTime >= dtmRefreshFrom) && (pdtmTime <= dtmRefreshTo))
											{
												return dtmRefreshFrom;
											}
										}
										// Check extra stop
										if ((drow[PRO_ShiftPatternTable.EXTRASTOPFROM_FLD] != DBNull.Value)
											&& (drow[PRO_ShiftPatternTable.EXTRASTOPTO_FLD] != DBNull.Value))
										{
											DateTime dtmExtraFrom = dtmWorkTimeFrom.AddSeconds(((DateTime)drow[PRO_ShiftPatternTable.EXTRASTOPFROM_FLD]
																				- (DateTime)drow[PRO_ShiftPatternTable.WORKTIMEFROM_FLD]).TotalSeconds);
											DateTime dtmExtraTo = dtmWorkTimeFrom.AddSeconds(((DateTime)drow[PRO_ShiftPatternTable.EXTRASTOPTO_FLD]
																				- (DateTime)drow[PRO_ShiftPatternTable.WORKTIMEFROM_FLD]).TotalSeconds);
											if((pdtmTime >= dtmExtraFrom) && (pdtmTime <= dtmExtraTo))
											{
												return dtmExtraFrom;
											}
										}

									}
				*/			
				

				#endregion // END: DEL SonHT 2005-11-01

				if((pdtmTime >= dtmFrom) && (pdtmTime <= dtmTo))
				{
					foreach(DataRow drowStop in dtbStopTime.Rows)
					{
						DateTime dtmStopFrom = dtmFrom.AddSeconds(((DateTime)drowStop[FROM_FLD] - dtmWorkTimeFrom).TotalSeconds);
						DateTime dtmStopTo = dtmFrom.AddSeconds(((DateTime)drowStop[TO_FLD] - dtmWorkTimeFrom).TotalSeconds);
						if((pdtmTime >= dtmStopFrom) && (pdtmTime <= dtmStopTo))
						{
							return dtmStopFrom;
						}
					}
				}
					// pdtmTime < From
				else if(pdtmTime < dtmFrom)
				{
					return dtmTo.AddDays(-1);
				}
					// pdtmTime > To
				else if(pdtmTime > dtmTo)
				{
					return dtmTo;
				}

			}
			//			}
			// Message: configuration work center capacity
			Hashtable hshTable = new Hashtable();
			hshTable.Add(0,pstrWorkCenter);
			throw new PCSBOException(ErrorCode.MESSAGE_DCP_CONFIG_WORKCENTER,METHOD_NAME,null,hshTable);
		}

		#region // HACK: DEL SonHT 2005-11-16

		/*
				/// <summary>
				/// Get nearest next working time
				/// </summary>
				/// <param name="pdtmTime"></param>
				/// <param name="pintWorkCenterID"></param>
				/// <param name="pdtbWCCapacityAndShift"></param>
				/// <param name="pdstCalendar"></param>
				/// <returns></returns>
				/// <summary>
				/// GetNextWorkingTime
				/// </summary>
				/// <param name="pdtmTime"></param>
				/// <param name="pintWorkCenterID"></param>
				/// <param name="pstrWorkCenter"></param>
				/// <param name="pdtbWCCapacityAndShift"></param>
				/// <param name="pdstCalendar"></param>
				/// <param name="pdtbCurrentCapacity"></param>
				/// <returns></returns>
				/// <author>Trada</author>
				/// <date>Friday, November 4 2005</date>
				public DateTime GetNextWorkingTime(DateTime pdtmTime, 
					int pintWorkCenterID, 
					string pstrWorkCenter, 
					DataTable pdtbWCCapacityAndShift, 
					DataSet pdstCalendar, 
					ref DataTable pdtbCurrentCapacity)
				{

					const string METHOD_NAME = THIS + ".GetNextWorkingTime()";
					const string DATE_FLD = "Date";

					DateTime dtmDate = pdtmTime.Date;
					// Check working day calendar
					if(pdstCalendar.Tables[MST_WorkingDayMasterTable.TABLE_NAME].Select(MST_WorkingDayMasterTable.YEAR_FLD + "=" + dtmDate.Year).Length == 0)
					{
						// check year
						// Message: Setting working calendar for the year @, please
						throw new PCSBOException(ErrorCode.MESSAGE_DCP_SETTING_WORKING_CALENDAR,METHOD_NAME,null);
					}
					// Check BeginDate -> EndDate
					DataRow[] drows = pdtbWCCapacityAndShift.Select(
						"'" + dtmDate + "' >= " + PRO_WCCapacityTable.BEGINDATE_FLD 
						+ " AND '" + dtmDate + "' <= " + PRO_WCCapacityTable.ENDDATE_FLD 
						+ " AND " + PRO_WCCapacityTable.WORKCENTERID_FLD + " = " + pintWorkCenterID,
						PRO_ShiftPatternTable.WORKTIMEFROM_FLD + " DESC");
					if(drows.Length > 0)
					{
						DateTime dtmWorkTimeFrom = (DateTime)drows[drows.Length-1][PRO_ShiftPatternTable.WORKTIMEFROM_FLD];
						DateTime dtmWorkTimeTo = (DateTime)drows[0][PRO_ShiftPatternTable.WORKTIMETO_FLD];
						double dblTotalSecond = (dtmWorkTimeTo - dtmWorkTimeFrom).TotalSeconds;
						dtmDate = GetDateOnly(pdtmTime, dtmWorkTimeFrom, dtmWorkTimeTo);
						DataRow[] drowsWorkingDay = pdtbCurrentCapacity.Select("Date >= '"  + dtmDate.Date +  "' and IsOffDay = 0","Date ASC");
						if(drowsWorkingDay.Length == 0)
						{
							// TODO: throw message Config work day calendar after dtmDate 
							throw new PCSBOException();
						}
						//Re-Set dtmDate
						dtmDate = (DateTime) drowsWorkingDay[0][DATE_FLD];
						DateTime dtmFrom = new DateTime(dtmDate.Year, dtmDate.Month, dtmDate.Day,
							dtmWorkTimeFrom.Hour, dtmWorkTimeFrom.Minute, dtmWorkTimeFrom.Second);
						DateTime dtmTo = dtmFrom.AddSeconds(dblTotalSecond);
				
						DataTable dtbStopTime = GetStopTime(drows);

						if((pdtmTime >= dtmFrom) && (pdtmTime <= dtmTo))
						{
							foreach(DataRow drowStop in dtbStopTime.Rows)
							{
								DateTime dtmStopFrom = new DateTime();
								DateTime dtmStopTo = new DateTime();
								// TODO: Check again
								if (dtmFrom.AddSeconds(((DateTime)drowStop[TO_FLD] - dtmWorkTimeFrom).TotalSeconds) > dtmFrom)
								{
									dtmStopFrom = dtmFrom.AddSeconds(((DateTime)drowStop[FROM_FLD] - dtmWorkTimeFrom).TotalSeconds);
									dtmStopTo = dtmFrom.AddSeconds(((DateTime)drowStop[TO_FLD] - dtmWorkTimeFrom).TotalSeconds);
								}
								else
								{
									dtmStopFrom = dtmFrom.AddSeconds(((DateTime)drowStop[FROM_FLD] - dtmWorkTimeFrom).TotalSeconds).AddDays(1);
									dtmStopTo = dtmFrom.AddSeconds(((DateTime)drowStop[TO_FLD] - dtmWorkTimeFrom).TotalSeconds).AddDays(1);
								}
								if((pdtmTime >= dtmStopFrom) && (pdtmTime <= dtmStopTo))
								{
									return dtmStopTo;
								}
							}
							return pdtmTime;
						}
							// pdtmTime < From
						else if(pdtmTime < dtmFrom)
						{
							return dtmFrom;
						}
							// pdtmTime > To
						else if(pdtmTime > dtmTo)
						{
							//return dtmTo;
							return dtmFrom.AddDays(1);
						}

					}
					// Message: configuration work center capacity
					Hashtable hshTable = new Hashtable();
					hshTable.Add(0,pstrWorkCenter);
					throw new PCSBOException(ErrorCode.MESSAGE_DCP_CONFIG_WORKCENTER,METHOD_NAME,null,hshTable);
			
				}
		*/

		#endregion // END: DEL SonHT 2005-11-16

		/// <summary>
		/// Change Date but still keeping hour, minute and second
		/// </summary>
		/// <param name="pdtmTime"></param>
		/// <param name="pdtmWorkTimeFrom"></param>
		/// <returns></returns>
		private DateTime ChangeSameShift(DateTime pdtmTime, DateTime pdtmWorkTimeFrom, DateTime pdtmWorkTimeTo)
		{
			DateTime dtmTimeInShift = new DateTime(pdtmWorkTimeFrom.Year, pdtmWorkTimeFrom.Month, pdtmWorkTimeFrom.Day, pdtmTime.Hour, pdtmTime.Minute, pdtmTime.Second);
			// If out of range (before of 1 day)
			if(dtmTimeInShift < pdtmWorkTimeFrom) dtmTimeInShift = dtmTimeInShift.AddDays(1);
			else if(dtmTimeInShift > pdtmWorkTimeTo) dtmTimeInShift = dtmTimeInShift.AddDays(-1);
			return dtmTimeInShift;
		}

		/// <summary>
		/// Get total second available to fill capacity and return Starttime can be started
		/// </summary>
		/// <param name="pdtmStartTime"></param>
		/// <param name="pintWorkCenterID"></param>
		/// <param name="pdtbWCCapacityAndShift"></param>
		/// <returns></returns>
		private double GetTotalSecondForBackward(DateTime pdtmStartTime,
			DateTime pdtmDateOnly,
			int pintWorkCenterID, 
			DataTable pdtbWCCapacityAndShift,
			ref DataSet pdstDCPResultDetail)
		{//calculate avail time error here
			double dblToSecond = 0;
			DateTime dtmOldStartTime = pdtmStartTime;
			// Get all shift belong to pintWorkCenterID
			DataRow[] drowShifts = pdtbWCCapacityAndShift.Select(PRO_WCCapacityTable.WORKCENTERID_FLD + "=" + pintWorkCenterID, 
				PRO_ShiftPatternTable.WORKTIMEFROM_FLD + " DESC"); //pending begin, end
			DataTable dtbStopTime = GetStopTime(drowShifts);
			// Find Max(EndTime) of DCPResultDetail that EndTime < pdtmTime
			DateTime dtmFrom = new DateTime(pdtmDateOnly.Year,pdtmDateOnly.Month,pdtmDateOnly.Day,
				((DateTime)drowShifts[drowShifts.Length-1][PRO_ShiftPatternTable.WORKTIMEFROM_FLD]).Hour,
				((DateTime)drowShifts[drowShifts.Length-1][PRO_ShiftPatternTable.WORKTIMEFROM_FLD]).Minute,
				((DateTime)drowShifts[drowShifts.Length-1][PRO_ShiftPatternTable.WORKTIMEFROM_FLD]).Second);
			//			DateTime dtmTo = dtmFrom.AddSeconds(((DateTime)drowShifts[0][PRO_ShiftPatternTable.WORKTIMETO_FLD]
			//									- (DateTime)drowShifts[drowShifts.Length-1][PRO_ShiftPatternTable.WORKTIMEFROM_FLD]).TotalSeconds);
			// do not get time for this day
			if(dtmFrom > dtmOldStartTime) return 0;
			// Get list previous dcp results
			DataRow[] drowExistedResults = pdstDCPResultDetail.Tables[0].Select(DATE_FLD + "= '" + pdtmDateOnly.Date
				+ "' AND " + PRO_DCPResultDetailTable.ENDTIME_FLD + " < '" + dtmOldStartTime + "'",
				PRO_DCPResultDetailTable.ENDTIME_FLD + " DESC");
			// if found then get the end time of avial previous line
			if(drowExistedResults.Length > 0)
			{
				pdtmStartTime = (DateTime) drowExistedResults[0][PRO_DCPResultDetailTable.ENDTIME_FLD];
			}
			else
			{
				pdtmStartTime = dtmFrom;
			}

			dblToSecond = (dtmOldStartTime - pdtmStartTime).TotalSeconds;

			// remove stop time
			foreach(DataRow drowStop in dtbStopTime.Rows)
			{
				DateTime dtmStopFrom = new DateTime(pdtmDateOnly.Year, pdtmDateOnly.Month, pdtmDateOnly.Day,
					((DateTime)drowStop[FROM_FLD]).Hour, 
					((DateTime)drowStop[FROM_FLD]).Minute, 
					((DateTime)drowStop[FROM_FLD]).Second);
				DateTime dtmStopTo = dtmStopFrom.AddSeconds( ((DateTime)drowStop[TO_FLD]-(DateTime)drowStop[FROM_FLD]).TotalSeconds );
				if((pdtmStartTime <= dtmStopFrom) && (dtmStopTo <= dtmOldStartTime))
				{
					dblToSecond = dblToSecond - ( (dtmStopTo-dtmStopFrom).TotalSeconds );
				}
			}

			#region // HACK: DEL SonHT 2005-11-01

			/*
						DataRow drowPrevShift = null;
						foreach(DataRow drowShift in drowShifts)
						{
							// Find Max(EndTime) of DCPResultDetail that EndTime < pdtmTime
							DateTime dtmStartOfShift = new DateTime(pdtmDateOnly.Year,pdtmDateOnly.Month,pdtmDateOnly.Day,
												((DateTime)drowShift[PRO_ShiftPatternTable.WORKTIMEFROM_FLD]).Hour,
												((DateTime)drowShift[PRO_ShiftPatternTable.WORKTIMEFROM_FLD]).Minute,
												((DateTime)drowShift[PRO_ShiftPatternTable.WORKTIMEFROM_FLD]).Second);
							// do not get time for this day
							if(dtmStartOfShift > dtmOldStartTime) return 0;
				
							// Get list previous dcp results
							DataRow[] drowExistedResults = pdstDCPResultDetail.Tables[0].Select(DATE_FLD + "= '" + pdtmDateOnly.Date
												+ "' AND " + PRO_DCPResultDetailTable.ENDTIME_FLD + " < '" + dtmOldStartTime + "'",
												PRO_DCPResultDetailTable.ENDTIME_FLD + " DESC");
							// if found then get the end time of avial previous line
							if(drowExistedResults.Length > 0)
							{
								pdtmStartTime = (DateTime) drowExistedResults[0][PRO_DCPResultDetailTable.ENDTIME_FLD];

			//					foreach(DataRow drowResult in drowExistedResults)
			//					{
			//						dtmOldStartTime = (DateTime) drowResult[PRO_DCPResultDetailTable.ENDTIME_FLD];
			//						if(dtmMaxEndTime == dtmOldStartTime)
			//						{
			//							dtmOldStartTime = (DateTime) drowResult[PRO_DCPResultDetailTable.STARTTIME_FLD];
			//							if(dtmOldStartTime < dtmMaxEndTime)
			//							{
			//								dtmMaxEndTime = dtmEndOfShift;
			//							}
			//						}
			//						else
			//						{
			//							break;
			//						}
			//
			//					}
							}
							else
							{
								pdtmStartTime = dtmStartOfShift;
							}
							// return endtime
			//				pdtmStartTime = dtmMaxEndTime;
							// if pdtmTime < dtmMaxEndTime (unable to get time in this day)
			//				if(dtmOldStartTime == pdtmStartTime)
			//				{
			//					return 0;
			//				}

				
							DateTime dtmShiftEndTime = ChangeSameShift(dtmOldStartTime,(DateTime)drowShift[PRO_ShiftPatternTable.WORKTIMEFROM_FLD]
																		, (DateTime)drowShift[PRO_ShiftPatternTable.WORKTIMETO_FLD]);
							DateTime dtmShiftStartTime = ChangeSameShift(pdtmStartTime,(DateTime)drowShift[PRO_ShiftPatternTable.WORKTIMEFROM_FLD]
																		, (DateTime)drowShift[PRO_ShiftPatternTable.WORKTIMETO_FLD]);
							// if The time between Fromworktime and Toworktime
							if((dtmShiftEndTime > (DateTime)drowShift[PRO_ShiftPatternTable.WORKTIMEFROM_FLD])
								&& (dtmShiftEndTime <= (DateTime)drowShift[PRO_ShiftPatternTable.WORKTIMETO_FLD]))
							{
								// Get OK time
								dblToSecond = dblToSecond + (dtmOldStartTime - pdtmStartTime).TotalSeconds;
			//					pdtmTime = pdtmTime.AddSeconds((dtmStoreInputTime - (DateTime)drow[PRO_ShiftPatternTable.WORKTIMEFROM_FLD]).TotalSeconds);
								// Remove extra stop time
								if((drowShift[PRO_ShiftPatternTable.EXTRASTOPFROM_FLD] != DBNull.Value)
									&& (drowShift[PRO_ShiftPatternTable.EXTRASTOPTO_FLD] != DBNull.Value))
								{
									// if dtmShiftMaxEndTime <= ExtraTime <= dtmShiftTime then remove stoptime
									if(((DateTime)drowShift[PRO_ShiftPatternTable.EXTRASTOPTO_FLD] <= dtmShiftEndTime)
										&& ((DateTime)drowShift[PRO_ShiftPatternTable.EXTRASTOPTO_FLD] >= dtmShiftStartTime))
									{
										double dblStopSecond = ((DateTime)drowShift[PRO_ShiftPatternTable.EXTRASTOPTO_FLD]
																- (DateTime)drowShift[PRO_ShiftPatternTable.EXTRASTOPFROM_FLD]).TotalSeconds;
										// remove non working in total second
										dblToSecond = dblToSecond - dblStopSecond;
										// Add non working time
			//							pdtmTime = pdtmTime.AddSeconds( -dblStopSecond );
									}
								}
								// Remove regular stop time
								if((drowShift[PRO_ShiftPatternTable.REGULARSTOPFROM_FLD] != DBNull.Value)
									&& (drowShift[PRO_ShiftPatternTable.REGULARSTOPTO_FLD] != DBNull.Value))
								{
									if(((DateTime)drowShift[PRO_ShiftPatternTable.REGULARSTOPTO_FLD] < dtmShiftEndTime)
										&& ((DateTime)drowShift[PRO_ShiftPatternTable.REGULARSTOPTO_FLD] >= dtmShiftStartTime))
									{
										double dblStopSecond = ((DateTime)drowShift[PRO_ShiftPatternTable.REGULARSTOPTO_FLD]
																- (DateTime)drowShift[PRO_ShiftPatternTable.REGULARSTOPFROM_FLD]).TotalSeconds;
										// remove non working in total second
										dblToSecond = dblToSecond - dblStopSecond;
										// Add non working time
			//							pdtmTime = pdtmTime.AddSeconds( -dblStopSecond );
									}
								}
								// Remove refreshing stop time
								if((drowShift[PRO_ShiftPatternTable.REFRESHINGFROM_FLD] != DBNull.Value)
									&& (drowShift[PRO_ShiftPatternTable.REFRESHINGTO_FLD] != DBNull.Value))
								{
									if(((DateTime)drowShift[PRO_ShiftPatternTable.REFRESHINGTO_FLD] < dtmShiftEndTime)
										&& ((DateTime)drowShift[PRO_ShiftPatternTable.REFRESHINGTO_FLD] >= dtmShiftStartTime))
									{
										double dblStopSecond = ((DateTime)drowShift[PRO_ShiftPatternTable.REFRESHINGTO_FLD]
																-(DateTime)drowShift[PRO_ShiftPatternTable.REFRESHINGFROM_FLD]).TotalSeconds;
										// remove non working in total second
										dblToSecond = dblToSecond - dblStopSecond;
										// Add non working time
			//							pdtmTime = pdtmTime.AddSeconds( -dblStopSecond );
									}
								}
					
							}
								// Shift is before ptdmTime then get all time of this shift
							else if(dtmShiftEndTime >= (DateTime)drowShift[PRO_ShiftPatternTable.WORKTIMETO_FLD])
							{
								double dblNetWorkTime = double.Parse(drowShift[NETWORKTIME_FLD].ToString());
								dblToSecond = dblToSecond + dblNetWorkTime;
								// Get time is all time of shift StartTime = WorkTimeFrom
			//					pdtmTime = pdtmTime.AddSeconds(- ((DateTime)drow[PRO_ShiftPatternTable.WORKTIMETO_FLD] 
			//													- (DateTime)drow[PRO_ShiftPatternTable.WORKTIMEFROM_FLD]).TotalSeconds );
							}

							// Store prev shift
							drowPrevShift = drowShift;
						}
			*/	

			#endregion // END: DEL SonHT 2005-11-01

			return dblToSecond;
		}

		#region // HACK: DEL SonHT 2005-11-16 Redundal code

		/// <summary>
		/// Insert result data into master table pdstDCPResultMaster
		/// </summary>
		/// <param name="pdrowItem"></param>
		/// <param name="pdrowRouting"></param>
		/// <param name="pdstDCPResultMaster"></param>
		/// <param name="pintDCOptionMasterID"></param>
		/// <param name="pintDCPResultMasterID"></param>
		private void InsertResultMaster(DataRow pdrowItem,
			DataRow pdrowRouting,
			ref DataSet pdstDCPResultMaster,
			int pintDCOptionMasterID,
			int pintDCPResultMasterID)
		{
			DataRow drowInput = pdstDCPResultMaster.Tables[0].NewRow();
			drowInput[PRO_DCPResultMasterTable.STARTDATETIME_FLD] = pdrowRouting[PRO_DCPResultMasterTable.STARTDATETIME_FLD];
			drowInput[PRO_DCPResultMasterTable.DUEDATETIME_FLD] = pdrowRouting[PRO_DCPResultMasterTable.DUEDATETIME_FLD];
			drowInput[PRO_DCPResultMasterTable.QUANTITY_FLD] = pdrowItem[PRO_DCPResultMasterTable.QUANTITY_FLD];
			drowInput[PRO_DCPResultMasterTable.DCOPTIONMASTERID_FLD] = pintDCOptionMasterID;
			drowInput[PRO_DCPResultMasterTable.CPOID_FLD] = pdrowItem[PRO_DCPResultMasterTable.CPOID_FLD];
			drowInput[PRO_DCPResultMasterTable.WOROUTINGID_FLD] = pdrowRouting[PRO_DCPResultMasterTable.WOROUTINGID_FLD];
			drowInput[ITM_RoutingTable.ROUTINGID_FLD] = pdrowRouting[ITM_RoutingTable.ROUTINGID_FLD];
			drowInput[PRO_DCPResultMasterTable.WORKORDERDETAILID_FLD] = pdrowRouting[PRO_DCPResultMasterTable.WORKORDERDETAILID_FLD];
			drowInput[PRO_DCPResultMasterTable.PRODUCTID_FLD] = pdrowRouting[PRO_DCPResultMasterTable.PRODUCTID_FLD];
			drowInput[PRO_DCPResultMasterTable.WORKCENTERID_FLD] = pdrowRouting[PRO_DCPResultMasterTable.WORKCENTERID_FLD];
			// template ID
			drowInput[PRO_DCPResultMasterTable.DCPRESULTMASTERID_FLD] = pintDCPResultMasterID;

			pdstDCPResultMaster.Tables[0].Rows.Add(drowInput);
		}

		/// <summary>
		/// Return true if setting full calender else return false
		/// </summary>
		/// <param name="pintDCOptionMasterID"></param>
		/// <returns></returns>
		/// <author>SONHT</author>
		private bool CheckCalendarConfig(int pintDCOptionMasterID)
		{
			PRO_DCOptionMasterDS dsDC = new PRO_DCOptionMasterDS();
			DataSet dstDC = dsDC.GetDCOption(pintDCOptionMasterID);
			DataSet dstCalendar = dsDC.GetCalendar(pintDCOptionMasterID);
			if(dstDC.Tables[0].Rows.Count == 0)
			{
				return false;
			}
			DateTime dtmAsOfDate = (DateTime)dstDC.Tables[0].Rows[0][MTR_MPSCycleOptionMasterTable.ASOFDATE_FLD];
			int intStartYear = dtmAsOfDate.Year;
			int intEndYear = dtmAsOfDate.AddDays(int.Parse(dstDC.Tables[0].Rows[0][MTR_MPSCycleOptionMasterTable.PLANHORIZON_FLD].ToString())).Year;
			for(int i = intStartYear; i <= intEndYear; i++)
			{
				DataRow[] drowsFilter = dstCalendar.Tables[0].Select(MST_WorkingDayMasterTable.YEAR_FLD + " = " + i);
				if(drowsFilter.Length == 0) 
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>
		/// Get total second available to fill capacity and return Starttime can be started
		/// </summary>
		/// <param name="pdtmStartTime"></param>
		/// <param name="pintWorkCenterID"></param>
		/// <param name="pdtbWCCapacityAndShift"></param>
		/// <returns></returns>
		private double GetTotalSecondAvailToFillFromStartTime(DateTime pdtmStartTime,
			DateTime pdtmDateOnly,
			int pintWorkCenterID, 
			DataTable pdtbWCCapacityAndShift,
			ref DataSet pdstDCPResultDetail)
		{
			//calculate avail time error here
			double dblToSecond = 0;
			DateTime dtmOldStartTime = pdtmStartTime;
			// Get all shift belong to pintWorkCenterID
			DataRow[] drowShifts = pdtbWCCapacityAndShift.Select(PRO_WCCapacityTable.WORKCENTERID_FLD + "=" + pintWorkCenterID
				+ " AND '" + pdtmDateOnly + "' >= " + PRO_WCCapacityTable.BEGINDATE_FLD
				+ " AND " + pdtmDateOnly + "' <= " + PRO_WCCapacityTable.ENDDATE_FLD, 
				PRO_ShiftPatternTable.WORKTIMEFROM_FLD + " DESC"); 
			DataTable dtbStopTime = GetStopTime(drowShifts);
			
			DateTime dtmFrom = new DateTime(pdtmDateOnly.Year,pdtmDateOnly.Month,pdtmDateOnly.Day,
				((DateTime)drowShifts[drowShifts.Length-1][PRO_ShiftPatternTable.WORKTIMEFROM_FLD]).Hour,
				((DateTime)drowShifts[drowShifts.Length-1][PRO_ShiftPatternTable.WORKTIMEFROM_FLD]).Minute,
				((DateTime)drowShifts[drowShifts.Length-1][PRO_ShiftPatternTable.WORKTIMEFROM_FLD]).Second);
			// do not get time for this day
			//if(dtmFrom > dtmOldStartTime) return 0;
			// Get list previous dcp results
			DataRow[] drowExistedResults = pdstDCPResultDetail.Tables[0].Select(DATE_FLD + "= '" + pdtmDateOnly.Date
				+ "' AND " + PRO_DCPResultDetailTable.ENDTIME_FLD + " < '" + dtmOldStartTime + "'",
				PRO_DCPResultDetailTable.ENDTIME_FLD + " DESC");
			// if found then get the end time of avial previous line
			if(drowExistedResults.Length > 0)
			{
				pdtmStartTime = (DateTime) drowExistedResults[0][PRO_DCPResultDetailTable.ENDTIME_FLD];
			}
			else
			{
				pdtmStartTime = dtmFrom;
			}

			dblToSecond = (dtmOldStartTime - pdtmStartTime).TotalSeconds;

			// remove stop time
			foreach(DataRow drowStop in dtbStopTime.Rows)
			{
				DateTime dtmStopFrom = new DateTime(pdtmDateOnly.Year, pdtmDateOnly.Month, pdtmDateOnly.Day,
					((DateTime)drowStop[FROM_FLD]).Hour, 
					((DateTime)drowStop[FROM_FLD]).Minute, 
					((DateTime)drowStop[FROM_FLD]).Second);
				DateTime dtmStopTo = dtmStopFrom.AddSeconds( ((DateTime)drowStop[TO_FLD]-(DateTime)drowStop[FROM_FLD]).TotalSeconds );
				if((pdtmStartTime <= dtmStopFrom) && (dtmStopTo <= dtmOldStartTime))
				{
					dblToSecond = dblToSecond - ( (dtmStopTo-dtmStopFrom).TotalSeconds );
				}
			}

			return dblToSecond;
		}

		/*		/// <summary>
				/// 
				/// </summary>
				/// <param name="pstrWorkCenter"></param>
				/// <param name="pdtmStartTime"></param>
				/// <param name="pdtbCurrentCapacity"></param>
				/// <returns></returns>
				private DateTime FillBackwardCapacity(string pstrWorkCenter, 
													  decimal pdecCapacity, 
													  decimal pdecQuantity, 
													  DateTime pdtmStartTime,
													  ref DataTable pdtbCurrentCapacity,
													  ref DataSet pdstDCPResultDetail, 
													  decimal pdecLeadTime, 
													  DataTable pdtbWCCapacity,
													  int pintDCPResultMasterID,
													  int pintWorkCenterID, 
													  DataTable pdtbWCCapacityAndShift,
													  DataSet pdstCalendar)
				{
					const string METHOD_NAME = THIS + ".FillBackwardCapacity()";

					// Return Capacity calculate by second
					//decimal decTotalDay = pdecLeadTime/pdecCapacity;
					decimal decOldLeadTime = pdecLeadTime;
					DataRow[] drowAvailShifts = pdtbWCCapacityAndShift.Select(PRO_WCCapacityTable.WORKCENTERID_FLD 
						+ "=" + pintWorkCenterID,PRO_ShiftPatternTable.WORKTIMEFROM_FLD + " DESC");
					DateTime dtmBaseFrom = (DateTime)drowAvailShifts[drowAvailShifts.Length-1][PRO_ShiftPatternTable.WORKTIMEFROM_FLD];
					DateTime dtmBaseTo = (DateTime)drowAvailShifts[0][PRO_ShiftPatternTable.WORKTIMETO_FLD];
					DateTime dtmDate = GetDateOnly(pdtmStartTime,dtmBaseFrom,dtmBaseTo); //pdtmStartTime.Date;

					region // HACK: DEL SonHT 2005-11-01

					//			if( ((DateTime)drowAvailShifts[0][PRO_ShiftPatternTable.WORKTIMETO_FLD]).Date ==
					//				((DateTime)drowAvailShifts[drowAvailShifts.Length-1][PRO_ShiftPatternTable.WORKTIMEFROM_FLD]).Date.AddDays(1) )
					//				
					//			{
					//				DateTime dtmFrom = new DateTime(dtmDate.Year,dtmDate.Month,dtmDate.Day,
					//								((DateTime)drowAvailShifts[drowAvailShifts.Length-1][PRO_ShiftPatternTable.WORKTIMEFROM_FLD]).Hour,
					//								((DateTime)drowAvailShifts[drowAvailShifts.Length-1][PRO_ShiftPatternTable.WORKTIMEFROM_FLD]).Minute,
					//								((DateTime)drowAvailShifts[drowAvailShifts.Length-1][PRO_ShiftPatternTable.WORKTIMEFROM_FLD]).Second).AddDays(-1);
					//				DateTime dtmTo = dtmFrom.AddSeconds(((DateTime)drowAvailShifts[0][PRO_ShiftPatternTable.WORKTIMETO_FLD] 
					//								- (DateTime)drowAvailShifts[drowAvailShifts.Length-1][PRO_ShiftPatternTable.WORKTIMEFROM_FLD]).TotalSeconds);
					//				if(pdtmStartTime < dtmTo)
					//				{
					//					dtmDate = dtmFrom.Date;
					//					if(pdtmStartTime < dtmFrom.AddDays(1))
					//					{
					//						pdtmStartTime = dtmTo;
					//					}
					//				}
					//				else
					//				{
					//				}
					//			}
					endregion // END: DEL SonHT 2005-11-01

					DataRow[] drows = pdtbCurrentCapacity.Select(DATE_FLD + " <= '" + dtmDate + "'"
						+ " AND [" + pstrWorkCenter + "] <= " + ((double)pdecCapacity - 100*EPSILON).ToString(), 
																 DATE_FLD + " DESC");
					foreach(DataRow drow in drows)
					{
						if((double)pdecLeadTime <= EPSILON)
						{
							pdecLeadTime = 0;
							break;
						}
						// Get all shifts
						DataRow[] drowShifts = pdtbWCCapacityAndShift.Select(PRO_WCCapacityTable.WORKCENTERID_FLD 
							+ "=" + pintWorkCenterID,PRO_ShiftPatternTable.WORKTIMEFROM_FLD + " DESC");

						if(bool.Parse(drow[ISOFFDAY_FLD].ToString()))
						{
							// Set start time = end of shift
							pdtmStartTime = new DateTime(((DateTime)drow[DATE_FLD]).Year, ((DateTime)drow[DATE_FLD]).Month, ((DateTime)drow[DATE_FLD]).Day ,
														 ((DateTime)drowShifts[drowShifts.Length-1][PRO_ShiftPatternTable.WORKTIMEFROM_FLD]).Hour, 
														 ((DateTime)drowShifts[drowShifts.Length-1][PRO_ShiftPatternTable.WORKTIMEFROM_FLD]).Minute,
														 ((DateTime)drowShifts[drowShifts.Length-1][PRO_ShiftPatternTable.WORKTIMEFROM_FLD]).Second).AddDays(-1);
							pdtmStartTime = pdtmStartTime.AddSeconds(((DateTime)drowShifts[0][PRO_ShiftPatternTable.WORKTIMETO_FLD] -
								(DateTime)drowShifts[drowShifts.Length-1][PRO_ShiftPatternTable.WORKTIMEFROM_FLD]).TotalSeconds);
							continue;
						}
						else
						{
							DateTime dtmDateOnly = GetDateOnly(pdtmStartTime,(DateTime)drowShifts[drowShifts.Length-1][PRO_ShiftPatternTable.WORKTIMEFROM_FLD],
															   (DateTime)drowShifts[0][PRO_ShiftPatternTable.WORKTIMETO_FLD]);
							// the same day
							if(dtmDateOnly == pdtmStartTime.Date)
							{
								pdtmStartTime = new DateTime(((DateTime)drow[DATE_FLD]).Year, 
															 ((DateTime)drow[DATE_FLD]).Month,
															 ((DateTime)drow[DATE_FLD]).Day,
															 pdtmStartTime.Hour, pdtmStartTime.Minute, pdtmStartTime.Second);
								// add more one day if repeat next time
								if(dtmDateOnly > (DateTime)drow[DATE_FLD])
								{
									pdtmStartTime = pdtmStartTime.AddDays(1);
								}
							}
							else // pdtmStartTime.Date == dtmDateOnly + 1
							{
								pdtmStartTime = new DateTime(((DateTime)drow[DATE_FLD]).Year, 
															 ((DateTime)drow[DATE_FLD]).Month,
															 ((DateTime)drow[DATE_FLD]).Day,
															 pdtmStartTime.Hour, pdtmStartTime.Minute, pdtmStartTime.Second).AddDays(1);
							}

						}
						//DateTime dtmDateOfRow = (DateTime)drow[DATE_FLD];
						DateTime dtmOldStartTime = pdtmStartTime; //new DateTime(dtmDateOfRow.Year,dtmDateOfRow.Month,dtmDateOfRow.Day,pdtmStartTime.Hour,pdtmStartTime.Minute,pdtmStartTime.Second);
						// if(decOldLeadTime != pdecLeadTime) pdtmStartTime = pdtmStartTime.AddSeconds(-1);
						if(!IsWorkingTime(pdtmStartTime.AddSeconds(-1),pintWorkCenterID,pdtbWCCapacityAndShift,pdstCalendar))
						{
							pdtmStartTime = pdtmStartTime.AddSeconds(-1);
						}
						if(!IsWorkingTime(pdtmStartTime,pintWorkCenterID,pdtbWCCapacityAndShift,pdstCalendar))
						{
							pdtmStartTime = GetPreviousWorkingTime(pdtmStartTime,pintWorkCenterID,pstrWorkCenter, pdtbWCCapacityAndShift,pdstCalendar,ref pdtbCurrentCapacity);
						}
						//DateTime dtmOldStartTime = pdtmStartTime;
						// If not full capacity
						if(decimal.Parse(drow[pstrWorkCenter].ToString()) < pdecCapacity)
						{
							DataRow[] drowWCCs = pdtbWCCapacity.Select(MST_WorkCenterTable.CODE_FLD + " = '" + pstrWorkCenter + "'");
							if(drowWCCs.Length == 0)
							{
								// Message: Setting working calendar for the year @, please
								throw new PCSBOException(ErrorCode.MESSAGE_DCP_SETTING_WORKING_CALENDAR,METHOD_NAME,null);
							}
							// Sum all shift of day
							double dblTotalTime = 0;
							foreach(DataRow drowWCC in drowWCCs)
							{
								dblTotalTime = dblTotalTime + double.Parse(drowWCC[TOTALWORKTIME].ToString());
							}

							decimal decOldCapacity = decimal.Parse(drow[pstrWorkCenter].ToString());
							// Get time via capacity
							pdtmStartTime = GetTheTimeFreeCapacity(pdstDCPResultDetail,pdtmStartTime,pintWorkCenterID);

							// Store StartTime
							dtmOldStartTime = pdtmStartTime;
					
							// DateTime dtmEndTime = pdtmStartTime;
							double dblAvailTime = GetTotalSecondForBackward(pdtmStartTime,
																			(DateTime)drow[DATE_FLD],
																			pintWorkCenterID,
																			pdtbWCCapacityAndShift,
																			ref pdstDCPResultDetail );
							if(dblAvailTime == 0) continue;
							double dblToSecond = 0;
							if(pdecCapacity - decOldCapacity >= pdecLeadTime)
							{
								dblToSecond = (double)pdecLeadTime * dblTotalTime / (double)pdecCapacity;
								if(dblToSecond > dblAvailTime)
								{
									dblToSecond = dblAvailTime;
								}
								if(dblToSecond < EPSILON) dblToSecond = 0;
								drow[pstrWorkCenter] = decimal.Parse(drow[pstrWorkCenter].ToString()) 
									+ decimal.Parse(dblToSecond.ToString()) * pdecCapacity / decimal.Parse(dblTotalTime.ToString());
								//						DataRow[] drowShifts = pdtbWCCapacityAndShift.Select(PRO_WCCapacityTable.WORKCENTERID_FLD + "=" + pintWorkCenterID,
								//									PRO_ShiftPatternTable.WORKTIMEFROM_FLD + " DESC");
								pdtmStartTime = AddMoreSecondBackward(drowShifts,dtmOldStartTime,dblToSecond,(DateTime)drow[DATE_FLD]);
								// pdtmStartTime = pdtmStartTime.AddSeconds(-dblToSecond);
							}
							else
							{
								dblToSecond = ((double)pdecCapacity - (double)decOldCapacity) * dblTotalTime/(double)pdecCapacity;
								if(dblToSecond > dblAvailTime)
								{
									dblToSecond = dblAvailTime;
								}
								if(dblToSecond < EPSILON) dblToSecond = 0;
								drow[pstrWorkCenter] = decimal.Parse(drow[pstrWorkCenter].ToString()) + decimal.Parse(dblToSecond.ToString())*pdecCapacity/decimal.Parse(dblTotalTime.ToString());
								//						pdtmStartTime = pdtmStartTime.AddSeconds(-dblToSecond); 
								//						DataRow[] drowShifts = pdtbWCCapacityAndShift.Select(PRO_WCCapacityTable.WORKCENTERID_FLD + "=" + pintWorkCenterID, PRO_ShiftPatternTable.WORKTIMEFROM_FLD + " DESC");
								pdtmStartTime = AddMoreSecondBackward(drowShifts,dtmOldStartTime,dblToSecond, (DateTime)drow[DATE_FLD]);
							}
				
							decimal decFilled = decimal.Parse(drow[pstrWorkCenter].ToString()) - decOldCapacity;
							pdecLeadTime = pdecLeadTime - decFilled;
							// Insert into result detail
							DataRow drowResultDetail = pdstDCPResultDetail.Tables[0].NewRow();
							drowResultDetail[PRO_DCPResultDetailTable.ENDTIME_FLD] = dtmOldStartTime;
							drowResultDetail[PRO_DCPResultDetailTable.STARTTIME_FLD] = pdtmStartTime;
							drowResultDetail[DATE_FLD] = pdtmStartTime.Date;
							drowResultDetail[PRO_WCCapacityTable.WORKCENTERID_FLD] = pintWorkCenterID;
							drowResultDetail[PRO_DCPResultDetailTable.TOTALSECOND_FLD] = dblToSecond;
							drowResultDetail[PRO_DCPResultDetailTable.PERCENTAGE_FLD] = 100 * decFilled/decOldLeadTime;
							drowResultDetail[PRO_DCPResultDetailTable.QUANTITY_FLD] = pdecQuantity * decFilled/decOldLeadTime;
							drowResultDetail[PRO_DCPResultDetailTable.DCPRESULTMASTERID_FLD] = pintDCPResultMasterID;
					
							pdstDCPResultDetail.Tables[0].Rows.Add(drowResultDetail);
						}
						else // get previous day
						{
							//					DataRow[] drowShifts = pdtbWCCapacityAndShift.Select(PRO_WCCapacityTable.WORKCENTERID_FLD 
							//						+ "=" + pintWorkCenterID,PRO_ShiftPatternTable.WORKTIMEFROM_FLD + " DESC");
							// Set start time = end of shift
							pdtmStartTime = new DateTime(((DateTime)drow[DATE_FLD]).Year, ((DateTime)drow[DATE_FLD]).Month, ((DateTime)drow[DATE_FLD]).Day ,
														 ((DateTime)drowShifts[drowShifts.Length-1][PRO_ShiftPatternTable.WORKTIMEFROM_FLD]).Hour, 
														 ((DateTime)drowShifts[drowShifts.Length-1][PRO_ShiftPatternTable.WORKTIMEFROM_FLD]).Minute,
														 ((DateTime)drowShifts[drowShifts.Length-1][PRO_ShiftPatternTable.WORKTIMEFROM_FLD]).Second).AddDays(-1);
							pdtmStartTime = pdtmStartTime.AddSeconds(((DateTime)drowShifts[0][PRO_ShiftPatternTable.WORKTIMETO_FLD] -
								(DateTime)drowShifts[drowShifts.Length-1][PRO_ShiftPatternTable.WORKTIMEFROM_FLD]).TotalSeconds);
						}
					}
					// Message: Config calendar not enough to generate
					if(pdecLeadTime > 0)
					{
						// Message: Setting working calendar for the year @, please
						throw new PCSBOException(ErrorCode.MESSAGE_DCP_SETTING_WORKING_CALENDAR,METHOD_NAME,null);
					}
					return pdtmStartTime;
				}
		*/		
		

		#endregion // END: DEL SonHT 2005-11-16
		
		//		/// <summary>
		//		/// Get available time base on capacity
		//		/// </summary>
		//		/// <param name="pdstDCPResultDetail"></param>
		//		/// <param name="pdtmStartTime"></param>
		//		/// <param name="pintWorkCenterID"></param>
		//		/// <returns></returns>
		//		private DateTime GetTheTimeFreeCapacity(DataSet pdstDCPResultDetail,
		////												DateTime pdtmDateOnly,
		//												DateTime pdtmStartTime,
		//												int pintWorkCenterID)
		//		{
		//			DataRow[] drowResultDetails = pdstDCPResultDetail.Tables[0].Select(//DATE_FLD + " = '" + pdtmDateOnly.Date + "' AND " 
		//				 PRO_DCPResultDetailTable.STARTTIME_FLD + " <= '" + pdtmStartTime + "' AND " 
		//				+ PRO_DCPResultDetailTable.ENDTIME_FLD + " >= '" + pdtmStartTime + "' AND " 
		//				+ PRO_WCCapacityTable.WORKCENTERID_FLD + " = " + pintWorkCenterID, 
		//				PRO_DCPResultDetailTable.STARTTIME_FLD + " DESC");
		//			// if found a record were filled in capacity
		//			if(drowResultDetails.Length > 0)
		//			{
		//				// if starttime < pdtmStartTime <= endtime
		//				if((DateTime)drowResultDetails[0][PRO_DCPResultDetailTable.ENDTIME_FLD] >= pdtmStartTime)
		//				{
		//					pdtmStartTime = ((DateTime)drowResultDetails[0][PRO_DCPResultDetailTable.STARTTIME_FLD]).AddSeconds(-1);
		//				}
		//			}
		//			return pdtmStartTime;
		//		}





		/// <summary>
		/// Get year does not configs
		/// </summary>
		/// <param name="pintDCOptionMasterID"></param>
		/// <returns></returns>
	
		public int GetLackOfYearInCalendar(int pintDCOptionMasterID)
		{
			return (new PRO_DCOptionMasterDS()).GetLackOfYearInCalendar(pintDCOptionMasterID);
		}
		/// <summary>
		/// Get 2 year from ... to ... need to config 
		/// </summary>
		/// <param name="pintDCOptionMasterID"></param>
		/// <returns></returns>
	
		public string[] GetFromYearToYear(int pintDCOptionMasterID)
		{
			return (new PRO_DCOptionMasterDS()).GetFromYearToYear(pintDCOptionMasterID);
		}

		/// <summary>
		/// Get WC was not config
		/// </summary>
		/// <param name="pintDCOptionMasterID"></param>
		/// <returns></returns>
	
		public string[] GetWorkCenterNotConfig(int pintDCOptionMasterID)
		{
			return (new PRO_DCOptionMasterDS()).GetWorkCenterNotConfig(pintDCOptionMasterID);
		}
		
		//		/// <summary>
		//		/// Add more second from now
		//		/// </summary>
		//		/// <param name="pdrowShifts"></param>
		//		/// <param name="pdtmEndTime"></param>
		//		/// <param name="pdblToSecond"></param>
		//		/// <param name="pdtmDate"></param>
		//		/// <returns></returns>
		//		private DateTime AddMoreSecondBackward(DataRow[] pdrowShifts,
		//											DateTime pdtmEndTime, 
		//											double pdblToSecond, 
		//											DateTime pdtmDate)
		//		{
		//			const string METHOD_NAME = THIS + ".AddMoreSecondBackward()";
		//			DateTime dtmStartTime = pdtmEndTime.AddSeconds(-pdblToSecond);
		//			DateTime dtmFrom = new DateTime(pdtmDate.Year, pdtmDate.Month, pdtmDate.Day,
		//							((DateTime)pdrowShifts[pdrowShifts.Length-1][PRO_ShiftPatternTable.WORKTIMEFROM_FLD]).Hour,
		//							((DateTime)pdrowShifts[pdrowShifts.Length-1][PRO_ShiftPatternTable.WORKTIMEFROM_FLD]).Minute,
		//							((DateTime)pdrowShifts[pdrowShifts.Length-1][PRO_ShiftPatternTable.WORKTIMEFROM_FLD]).Second);
		//			DataTable dtbStopTime = GetStopTime(pdrowShifts);
		//			DataRow[] drowStops = dtbStopTime.Select("",FROM_FLD + " DESC");
		//			foreach(DataRow drowStop in drowStops)
		//			{
		//				DateTime dtmStopFrom = dtmFrom.AddSeconds(((DateTime)drowStop[FROM_FLD] 
		//							- (DateTime)pdrowShifts[pdrowShifts.Length-1][PRO_ShiftPatternTable.WORKTIMEFROM_FLD]).TotalSeconds); 
		//				DateTime dtmStopTo = dtmStopFrom.AddSeconds( ((DateTime)drowStop[TO_FLD] - (DateTime)drowStop[FROM_FLD]).TotalSeconds);
		//				if((dtmStopTo > dtmStartTime) && (dtmStopTo <= pdtmEndTime))
		//				{
		//					dtmStartTime = dtmStartTime.AddSeconds(-(dtmStopTo-dtmStopFrom).TotalSeconds );
		//				}
		//			}
		//
		//			#region // HACK: DEL SonHT 2005-11-01
		//
		///*			
		////			DateTime dtmShiftEndTimes = ChangeSameShift(pdtmEndTime, (DateTime)pdrowShifts[0][PRO_ShiftPatternTable.WORKTIMEFROM_FLD]
		////				, (DateTime)pdrowShifts[0][PRO_ShiftPatternTable.WORKTIMETO_FLD]);
		//			foreach(DataRow drowShift in pdrowShifts)
		//			{
		////				DateTime dtmShiftStartTime = ChangeSameShift(dtmStartTime, (DateTime)drowShift[PRO_ShiftPatternTable.WORKTIMEFROM_FLD]
		////													, (DateTime)drowShift[PRO_ShiftPatternTable.WORKTIMETO_FLD]);
		//				// Break between 2 shifts
		//				if (drowPrevShift != null)
		//				{
		//					DateTime dtmPrevWorkTimeFrom = (DateTime)drowPrevShift[PRO_ShiftPatternTable.WORKTIMEFROM_FLD];
		//					DateTime dtmWorkTimeTo = (DateTime)drowShift[PRO_ShiftPatternTable.WORKTIMETO_FLD];
		//					double dblStopTime = (dtmPrevWorkTimeFrom - dtmWorkTimeTo).TotalSeconds;
		//					if(dtmWorkTimeTo <= dtmPrevWorkTimeFrom)
		//					{
		//						//if((dtmPrevWorkTimeFrom > dtmShiftStartTime)) //(dtmPrevWorkTimeFrom < dtmShiftEndTime) 
		//						dtmPrevWorkTimeFrom = new DateTime(pdtmDate.Year, pdtmDate.Month, pdtmDate.Day,
		//													dtmPrevWorkTimeFrom.Hour, dtmPrevWorkTimeFrom.Minute, dtmPrevWorkTimeFrom.Second);
		//						if((dtmPrevWorkTimeFrom > dtmStartTime)&&(dtmPrevWorkTimeFrom < pdtmEndTime))
		//						{
		//							dtmStartTime = dtmStartTime.AddSeconds( -dblStopTime );
		////							dtmShiftStartTime = ChangeSameShift(dtmStartTime, (DateTime)drowShift[PRO_ShiftPatternTable.WORKTIMEFROM_FLD]
		////															, (DateTime)drowShift[PRO_ShiftPatternTable.WORKTIMETO_FLD]);
		//						}
		//					}
		//					else
		//					{
		//						// Message: Some shifts were overlaped time
		//						throw new PCSBOException(ErrorCode.MESSAGE_DCP_SHIFTS_OVERLAPED,METHOD_NAME,null);
		//					}
		//
		//				}
		//				// regular
		//				if ((drowShift[PRO_ShiftPatternTable.REGULARSTOPFROM_FLD] != DBNull.Value)
		//					&& (drowShift[PRO_ShiftPatternTable.REGULARSTOPTO_FLD] != DBNull.Value))
		//				{
		//					DateTime dtmStopFrom = (DateTime)drowShift[PRO_ShiftPatternTable.REGULARSTOPFROM_FLD];
		//					DateTime dtmStopTo = (DateTime)drowShift[PRO_ShiftPatternTable.REGULARSTOPTO_FLD];
		//					double dblStopTime = (dtmStopTo - dtmStopFrom).TotalSeconds;
		////					if( (dtmStopTo > dtmShiftStartTime)) //&& (dtmStopTo < dtmShiftEndTime)  )
		//					dtmStopTo = new DateTime( pdtmDate.Year, pdtmDate.Month, pdtmDate.Day,
		//								((DateTime)drowShift[PRO_ShiftPatternTable.WORKTIMEFROM_FLD]).Hour,
		//								((DateTime)drowShift[PRO_ShiftPatternTable.WORKTIMEFROM_FLD]).Minute,
		//								((DateTime)drowShift[PRO_ShiftPatternTable.WORKTIMEFROM_FLD]).Second);
		//					dtmStopTo = dtmStopTo.AddSeconds( ((DateTime)drowShift[PRO_ShiftPatternTable.REGULARSTOPTO_FLD] -
		//								(DateTime)drowShift[PRO_ShiftPatternTable.WORKTIMEFROM_FLD]).TotalSeconds );
		//					if( (dtmStopTo > dtmStartTime)&& (dtmStopTo < pdtmEndTime))
		//					{
		//						dtmStartTime = dtmStartTime.AddSeconds( -dblStopTime);
		////						dtmShiftStartTime = ChangeSameShift(dtmStartTime, (DateTime)drowShift[PRO_ShiftPatternTable.WORKTIMEFROM_FLD]
		////												, (DateTime)drowShift[PRO_ShiftPatternTable.WORKTIMETO_FLD]);
		//					}
		//				}
		//				// freshing
		//				if ((drowShift[PRO_ShiftPatternTable.REFRESHINGFROM_FLD] != DBNull.Value)
		//					&& (drowShift[PRO_ShiftPatternTable.REFRESHINGTO_FLD] != DBNull.Value))
		//				{
		//					DateTime dtmStopFrom = (DateTime)drowShift[PRO_ShiftPatternTable.REFRESHINGFROM_FLD];
		//					DateTime dtmStopTo = (DateTime)drowShift[PRO_ShiftPatternTable.REFRESHINGTO_FLD];
		//					double dblStopTime = (dtmStopTo - dtmStopFrom).TotalSeconds;
		//					dtmStopTo = new DateTime( pdtmDate.Year, pdtmDate.Month, pdtmDate.Day,
		//						((DateTime)drowShift[PRO_ShiftPatternTable.WORKTIMEFROM_FLD]).Hour,
		//						((DateTime)drowShift[PRO_ShiftPatternTable.WORKTIMEFROM_FLD]).Minute,
		//						((DateTime)drowShift[PRO_ShiftPatternTable.WORKTIMEFROM_FLD]).Second);
		//					dtmStopTo = dtmStopTo.AddSeconds( ((DateTime)drowShift[PRO_ShiftPatternTable.REFRESHINGTO_FLD] -
		//						(DateTime)drowShift[PRO_ShiftPatternTable.WORKTIMEFROM_FLD]).TotalSeconds );
		////					if( (dtmStopTo > dtmShiftStartTime)) //&& (dtmStopTo < dtmShiftEndTime) )
		//					if( (dtmStopTo > dtmStartTime)&& (dtmStopTo < pdtmEndTime))
		//					{
		//						dtmStartTime = dtmStartTime.AddSeconds( -dblStopTime );
		////						dtmShiftStartTime = ChangeSameShift(dtmStartTime, (DateTime)drowShift[PRO_ShiftPatternTable.WORKTIMEFROM_FLD]
		////												, (DateTime)drowShift[PRO_ShiftPatternTable.WORKTIMETO_FLD]);
		//					}
		//				}
		//				// extra
		//				if ((drowShift[PRO_ShiftPatternTable.EXTRASTOPFROM_FLD] != DBNull.Value)
		//					&& (drowShift[PRO_ShiftPatternTable.EXTRASTOPTO_FLD] != DBNull.Value))
		//				{
		//					DateTime dtmStopFrom = (DateTime)drowShift[PRO_ShiftPatternTable.EXTRASTOPFROM_FLD];
		//					DateTime dtmStopTo = (DateTime)drowShift[PRO_ShiftPatternTable.EXTRASTOPTO_FLD];
		//					double dblStopTime = (dtmStopTo - dtmStopFrom).TotalSeconds;
		//					dtmStopTo = new DateTime( pdtmDate.Year, pdtmDate.Month, pdtmDate.Day,
		//						((DateTime)drowShift[PRO_ShiftPatternTable.WORKTIMEFROM_FLD]).Hour,
		//						((DateTime)drowShift[PRO_ShiftPatternTable.WORKTIMEFROM_FLD]).Minute,
		//						((DateTime)drowShift[PRO_ShiftPatternTable.WORKTIMEFROM_FLD]).Second);
		//					dtmStopTo = dtmStopTo.AddSeconds( ((DateTime)drowShift[PRO_ShiftPatternTable.EXTRASTOPTO_FLD] -
		//						(DateTime)drowShift[PRO_ShiftPatternTable.WORKTIMEFROM_FLD]).TotalSeconds );
		////					if( (dtmStopTo > dtmShiftStartTime)) // && (dtmStopTo < dtmShiftEndTime) )
		//					if( (dtmStopTo > dtmStartTime)&& (dtmStopTo < pdtmEndTime))
		//					{
		//						dtmStartTime = dtmStartTime.AddSeconds( -dblStopTime );
		////						dtmShiftStartTime = ChangeSameShift(dtmStartTime, (DateTime)drowShift[PRO_ShiftPatternTable.WORKTIMEFROM_FLD]
		////												, (DateTime)drowShift[PRO_ShiftPatternTable.WORKTIMETO_FLD]);
		//					}
		//				}
		//				// Store prev shift
		//				drowPrevShift = drowShift;
		//			}
		//*/	
		//
		//			#endregion // END: DEL SonHT 2005-11-01
		//
		//			return dtmStartTime;
		//		}

		/// <summary>
		/// Get working date 
		/// </summary>
		/// <param name="pdtmTime"></param>
		/// <param name="pdtmBaseFrom"></param>
		/// <param name="pdtmBaseTo"></param>
		/// <returns></returns>
		/// <author>TraDA update</author>
		private DateTime GetDateOnly(DateTime pdtmTime,
			DateTime pdtmBaseFrom,
			DateTime pdtmBaseTo)
		{
			DateTime dtmDate = pdtmTime.Date;
			double dblTotalSecond = (pdtmBaseTo-pdtmBaseFrom).TotalSeconds;
			// if differ date
			if(pdtmBaseTo.Date == pdtmBaseFrom.Date.AddDays(1))
			{
				pdtmBaseFrom = new DateTime(dtmDate.Year,dtmDate.Month,dtmDate.Day,
					pdtmBaseFrom.Hour,pdtmBaseFrom.Minute,pdtmBaseFrom.Second);
				pdtmBaseTo = pdtmBaseFrom.AddSeconds(dblTotalSecond);
				if (pdtmTime >= pdtmBaseFrom)
				{
					dtmDate = pdtmBaseFrom.Date;
				}
				else if (pdtmTime > pdtmBaseTo.AddDays(-1))
				{
					dtmDate = pdtmBaseFrom.Date;
				} 
				else
				{
					dtmDate = pdtmBaseFrom.AddDays(-1);
				}

			}
			return dtmDate;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		private DataTable GetStopTime(DataSet pdstStopTime, DateTime pdtmWorkingDay)
		{
			DataRow[] arrIndex = pdstStopTime.Tables[TBL_INDEX].Select(
				PRO_WCCapacityTable.BEGINDATE_FLD + " <= '" + pdtmWorkingDay + "' AND " +
				PRO_WCCapacityTable.ENDDATE_FLD + " >= '" + pdtmWorkingDay + "'");
			if (arrIndex.Length > 0)
			{
				string strTableName = arrIndex[0][STOPTIMETABLE_FLD].ToString();
				return pdstStopTime.Tables[strTableName];
			}
			else
			{
				return null;
			}
		}

		/// <summary>
		/// Get stop working time via shifts
		/// </summary>
		/// <param name="pdrowShifts"></param>
		/// <returns></returns>
		private DataTable GetStopTime(DataRow[] pdrowShifts)
		{
			int t1 = Environment.TickCount;
			DataTable dtbStopTime = new DataTable();
			dtbStopTime.Columns.Add(FROM_FLD, typeof(DateTime));
			dtbStopTime.Columns.Add(TO_FLD, typeof(DateTime));
			DataRow drowPrev = null;
			foreach(DataRow drow in pdrowShifts)
			{
				DateTime dtmWorkTimeFrom = (DateTime)drow[PRO_ShiftPatternTable.WORKTIMEFROM_FLD];
				DateTime dtmWorkTimeTo = (DateTime)drow[PRO_ShiftPatternTable.WORKTIMETO_FLD];
				if(drowPrev != null)
				{
					if((DateTime)drowPrev[PRO_ShiftPatternTable.WORKTIMEFROM_FLD] > dtmWorkTimeTo)
					{
						DataRow drowStop = dtbStopTime.NewRow();
						drowStop[FROM_FLD] = dtmWorkTimeTo;
						drowStop[TO_FLD] = (DateTime)drowPrev[PRO_ShiftPatternTable.WORKTIMEFROM_FLD];
						dtbStopTime.Rows.Add(drowStop);
					}
				}
				// Check regular stop
				if((drow[PRO_ShiftPatternTable.REGULARSTOPFROM_FLD] != DBNull.Value)
					&& (drow[PRO_ShiftPatternTable.REGULARSTOPTO_FLD] != DBNull.Value))
				{
					DateTime dtmRegularFrom = dtmWorkTimeFrom.AddSeconds(((DateTime)drow[PRO_ShiftPatternTable.REGULARSTOPFROM_FLD]
						- (DateTime)drow[PRO_ShiftPatternTable.WORKTIMEFROM_FLD]).TotalSeconds);
					DateTime dtmRegularTo = dtmWorkTimeFrom.AddSeconds(((DateTime)drow[PRO_ShiftPatternTable.REGULARSTOPTO_FLD]
						- (DateTime)drow[PRO_ShiftPatternTable.WORKTIMEFROM_FLD]).TotalSeconds);
					DataRow drowStop = dtbStopTime.NewRow();
					drowStop[FROM_FLD] = dtmRegularFrom;
					drowStop[TO_FLD] = dtmRegularTo;
					dtbStopTime.Rows.Add(drowStop);
				}
				// Check refresh stop
				if ((drow[PRO_ShiftPatternTable.REFRESHINGFROM_FLD] != DBNull.Value)
					&& (drow[PRO_ShiftPatternTable.REFRESHINGTO_FLD] != DBNull.Value))
				{
					DateTime dtmRefreshFrom = dtmWorkTimeFrom.AddSeconds(((DateTime)drow[PRO_ShiftPatternTable.REFRESHINGFROM_FLD]
						- (DateTime)drow[PRO_ShiftPatternTable.WORKTIMEFROM_FLD]).TotalSeconds);
					DateTime dtmRefreshTo = dtmWorkTimeFrom.AddSeconds(((DateTime)drow[PRO_ShiftPatternTable.REFRESHINGTO_FLD]
						- (DateTime)drow[PRO_ShiftPatternTable.WORKTIMEFROM_FLD]).TotalSeconds);
					DataRow drowStop = dtbStopTime.NewRow();
					drowStop[FROM_FLD] = dtmRefreshFrom;
					drowStop[TO_FLD] = dtmRefreshTo;
					dtbStopTime.Rows.Add(drowStop);
				}
				// Check extra stop
				if ((drow[PRO_ShiftPatternTable.EXTRASTOPFROM_FLD] != DBNull.Value)
					&& (drow[PRO_ShiftPatternTable.EXTRASTOPTO_FLD] != DBNull.Value))
				{
					DateTime dtmExtraFrom = dtmWorkTimeFrom.AddSeconds(((DateTime)drow[PRO_ShiftPatternTable.EXTRASTOPFROM_FLD]
						- (DateTime)drow[PRO_ShiftPatternTable.WORKTIMEFROM_FLD]).TotalSeconds);
					DateTime dtmExtraTo = dtmWorkTimeFrom.AddSeconds(((DateTime)drow[PRO_ShiftPatternTable.EXTRASTOPTO_FLD]
						- (DateTime)drow[PRO_ShiftPatternTable.WORKTIMEFROM_FLD]).TotalSeconds);
					DataRow drowStop = dtbStopTime.NewRow();
					drowStop[FROM_FLD] = dtmExtraFrom;
					drowStop[TO_FLD] = dtmExtraTo;
					dtbStopTime.Rows.Add(drowStop);
				}
				drowPrev = drow;

			}
			int t2 = Environment.TickCount;
			return dtbStopTime;
		}

	

		/// <summary>
		/// Get each Routing find to fill, if fill all routing then success, else push to stack and search again.
		/// </summary>
		/// <param name="pdrowItem"></param>
		/// <param name="pdrowRoutings"></param>
		/// <param name="pstackRouting"></param>
		/// <param name="pdtbCurrentCapacity"></param>
		/// <param name="pdstDCPResultDetail"></param>
		/// <param name="pdtbWCCapacity"></param>
		/// <param name="pdtbWCCapacityAndShift"></param>
		/// <param name="pdstCalendar"></param>
		/// <returns>Start time of first routing</returns>
		private DateTime FindBackwardPlanning(DataRow pdrowItem, //int pintMasterID,
			ref DataRow[] pdrowRoutings,
			//ref DataSet pdstItemRouting,
			ref Stack pstackRouting,
			ref DataTable pdtbCurrentCapacity,
			ref DataSet pdstDCPResultMaster, 
			ref DataSet pdstDCPResultDetail, 
			// decimal pdecLeadTime, 
			ref DataTable pdtbWCCapacity,
			// int pintDCPResultMasterID,
			ref DataTable dtbChangeCategory, 
			ref DataTable pdtbWCCapacityAndShift,
			ref DataSet pdstCalendar,
			int pintDCOptionMasterID,
			DataTable pdtbChangeCategory)
		{
			//Stack stackRoutingOut = new Stack();
			DateTime dtmBaseStartTime = (DateTime)pdrowItem[MTR_CPOTable.STARTDATE_FLD];
			DateTime dtmStartTimeofFirstOperation = DateTime.MinValue;

			while (true)
			{
				#region // HACK: DEL SonHT 2005-11-07
				//				object objRouting = pstackRouting.Pop();
				//				stackRoutingOut.Push(objRouting);
				//				int intDetailID = int.Parse(objRouting.ToString());
				//				DataRow drowOperation = null;
				// Find Operation
				//				foreach(DataRow drowRouting in pdrowRoutings)
				//				{
				//					if(drowRouting[DETAILID_FLD].ToString() == intDetailID.ToString())
				//					{
				//						drowOperation = drowRouting;
				//						break;
				//					}
				//				}
				//
				//
				//				string strWorkCenter = drowOperation[WORKCENTERCODE].ToString();
				//				// decimal decCapacity = 0;
				//				decimal decQuantity = decimal.Parse(pdrowItem[MTR_CPOTable.QUANTITY_FLD].ToString());
				//				//DateTime dtmStartTime = GetActualStartTimeForCurrentOperation();
				//				decimal decLeadTime = decimal.Parse(drowOperation[LEADTIME_FLD].ToString());
				//				int intWorkCenterID = int.Parse(drowOperation[ITM_RoutingTable.WORKCENTERID_FLD].ToString());


				//				DateTime dtmEndTimeOfOperation = FindAvailTimeAndFillOperation( drowOperation,
				//													 strWorkCenter, 
				//													 // decCapacity, 
				//													 decQuantity, 
				//													 dtmStartTime,
				//													 ref pdtbCurrentCapacity,
				//													 ref pdstDCPResultMaster, 
				//													 ref pdstDCPResultDetail, 
				//													 decLeadTime, 
				//													 pdtbWCCapacity,
				//													 pintDCPResultMasterID,
				//													 intWorkCenterID, 
				//													 pdtbWCCapacityAndShift,
				//													 pdstCalendar);
				// if unable find a solution then rollback and find again
				//if(dtmEndTimeOfOperation == DateTime.MinValue)
				#endregion // END: DEL SonHT 2005-11-07
				DataTable dtbBackupChangeCategory = new DataTable();
				// Set new Start time for this item
				dtmStartTimeofFirstOperation = GetJumpingStep(
					dtmBaseStartTime,
					pdrowItem,
					pdrowRoutings,
					pdtbWCCapacity,
					pdtbWCCapacityAndShift,
					pdstCalendar,
					pdtbCurrentCapacity,
					pdstDCPResultMaster,
					pdstDCPResultDetail,
					pintDCOptionMasterID,
					pdtbChangeCategory,
					dtbBackupChangeCategory);

				if(dtmStartTimeofFirstOperation < dtmBaseStartTime)
				{
					dtmBaseStartTime = dtmStartTimeofFirstOperation;
				}
				else
				{
					break;
				}
			}
			pdrowItem[MTR_CPOTable.STARTDATE_FLD] = dtmStartTimeofFirstOperation;
			return dtmStartTimeofFirstOperation;
		}




		/*		private void CheckOverlapCapacity(int pintWorkCenterID, 
												ref DataTable pdtbWCCapacity)
				{
					DataRow[] drowWCCapacitys = pdtbWCCapacity.Select(MST_WorkCenterTable.WORKCENTERID_FLD + "=" + pintWorkCenterID,
						PRO_WCCapacityTable.BEGINDATE_FLD + " ASC" );
					if(drowWCCapacitys.Length == 0)
					{
						// Work center is empty capacity
						throw new PCSBOException();
					}
					if(drowWCCapacitys.Length == 1)
					{
						return;
					}
					// else
					DateTime dtmEndDate = (DateTime)drowWCCapacitys[0][PRO_WCCapacityTable.ENDDATE_FLD];
					for(int i = 1; i < drowWCCapacitys.Length; i++)
					{
						if(dtmEndDate > (DateTime)drowWCCapacitys[i][PRO_WCCapacityTable.BEGINDATE_FLD])
						{
							// Work center is overlap capacity
							throw new PCSBOException();
						}
						dtmEndDate = (DateTime)drowWCCapacitys[i][PRO_WCCapacityTable.ENDDATE_FLD];
					}
				}
		*/
	

		/// <summary>
		/// Get WorkTimeFrom and WorkTimeTo base on multi shift by WorkCenter,BeginDate and EndDate.
		/// Throw new exception if unsucessful
		/// </summary>
		/// <param name="pdtbWCCapacityAndShift"></param>
		/// <param name="pintWorkCenterID"></param>
		/// <param name="pdtmDateOnly"></param>
		/// <param name="pdtmBaseFrom">return BaseWorkTimeTo</param>
		/// <param name="pdtmBaseTo">return BaseWorkTimeTo</param>
		private void GetWorkTimeFromToByMultiShift(ref DataTable pdtbWCCapacityAndShift, 
			int pintWorkCenterID,
			DateTime pdtmDateOnly,
			ref DateTime pdtmBaseFrom,
			ref DateTime pdtmBaseTo)
		{
			DataRow[] drowShifts = pdtbWCCapacityAndShift.Select(
				MST_WorkCenterTable.WORKCENTERID_FLD + "=" + pintWorkCenterID
				+ " AND " + PRO_WCCapacityTable.BEGINDATE_FLD + "<= '" + pdtmDateOnly + "'" 
				+ " AND " + PRO_WCCapacityTable.ENDDATE_FLD + ">= '" + pdtmDateOnly + "'",
				PRO_ShiftPatternTable.WORKTIMEFROM_FLD + " ASC");
			// Check empty shift
			if(drowShifts.Length == 0)
			{
				// TODO: throw message Empty shift
				throw new PCSBOException();
			}
			else // Check overlap shift
			{
				if(drowShifts.Length > 1)
				{
					DateTime dtmWorkTimeToPrevShift = (DateTime)drowShifts[0][PRO_ShiftPatternTable.WORKTIMETO_FLD];
					for(int i = 1; i < drowShifts.Length; i++)
					{
						if((DateTime)drowShifts[i][PRO_ShiftPatternTable.WORKTIMEFROM_FLD] < dtmWorkTimeToPrevShift)
						{
							// TODO: throw message overlap shift
							throw new PCSBOException();
						}
					}
				}
			}
			pdtmBaseFrom = (DateTime)drowShifts[0][PRO_ShiftPatternTable.WORKTIMEFROM_FLD];

			pdtmBaseTo = (DateTime)drowShifts[drowShifts.Length-1][PRO_ShiftPatternTable.WORKTIMETO_FLD];
		}



		/// <summary>
		/// Insert into table current capacity, Calendar, WCCapacityAndShift and WCCapacity with new BeginDate,EndDate of Work center
		/// </summary>
		private void InsertNewNextYearSimilarToCurrentYear(ref DataTable pdtbCurrentCapacity, 
			ref DataTable pdtbWCCapacityAndShift, 
			ref DataSet pdstCalendar )
		{
			
		}

		#endregion Old version

		#region DUONGNA's codes
	
		#region Constants
		private const int LINEAR = 0;
		private const int PARALLEL = 1;
		private const int OVERLAPQTY = 2;
		private const int OVERLAPPERCENT = 3;

		const string CPOID_FLD = "CPOID";
		const string TOTALWORKTIME_FLD = "TotalWorkTime";
		const string CAPACITY_FLD = "Capacity";
		const string QUANTITY_FLD = "Quantity";

		//		const string OPEN_BRACKET = "(";
		//		const string CLOSE_BRACKET = ")";
		//		const string PARENTHESIS = "'";
		//		const string OR = " OR ";

		#endregion Constants

		#region Functions for backward algorithm

		/// <summary>
		/// GetListOfAvailCapacity base on current capacity in only one day is pdtmDateOnly.
		/// </summary>
		/// <param name="pdtmStartTime"></param>
		/// <param name="pdtmDateOnly"></param>
		/// <param name="pintWorkCenterID"></param>
		/// <param name="pdstDCPResultDetail"></param>
		/// <returns>if(pdtmFromTime == pdtmToTime) return false else if(pdtmFromTime small than pdtmToTime) return true;</returns>
		private bool GetAvailCapacityForOneDay(
			DateTime pdtmStartTime,
			ref DateTime pdtmFromTime,
			ref DateTime pdtmToTime,
			DateTime pdtmDateOnly,
			int pintWorkCenterID, 
			DataTable pdtbWCCapacityAndShift,
			DataSet pdstDCPResultMaster,
			DataSet pdstDCPResultDetail)
		{			
			//TODO: get over master
			DataRow[] arrResultMasters = pdstDCPResultMaster.Tables[0].Select(PRO_DCPResultMasterTable.WORKCENTERID_FLD + "=" + pintWorkCenterID.ToString());
			string strMasterIDs = "(0,";
			foreach (DataRow drowResultMaster in arrResultMasters)
			{
				strMasterIDs += drowResultMaster[PRO_DCPResultMasterTable.DCPRESULTMASTERID_FLD].ToString() + ",";
			}
			strMasterIDs = strMasterIDs.Substring(0,strMasterIDs.Length - 1) + ")";

			DataRow[] arrResultDetails = pdstDCPResultDetail.Tables[0].Select(
				PRO_DCPResultDetailTable.DCPRESULTMASTERID_FLD + " IN " + strMasterIDs
				//+ " AND " + DATE_FLD + "= '" + pdtmDateOnly + "'"
				+ " AND " + PRO_DCPResultDetailTable.ENDTIME_FLD + "> '" + pdtmStartTime + "'",
				PRO_DCPResultDetailTable.ENDTIME_FLD + " ASC");
			DateTime dtmBaseFrom = new DateTime();
			DateTime dtmBaseTo = new DateTime();
			GetWorkTimeFromToByMultiShift(
				ref pdtbWCCapacityAndShift,
				pintWorkCenterID,
				pdtmDateOnly,ref dtmBaseFrom,ref dtmBaseTo);
			
			double dblDiffDays = dtmBaseTo.Subtract(dtmBaseFrom).TotalDays;
			DateTime dtmRealFrom = new DateTime(pdtmDateOnly.Year,pdtmDateOnly.Month,pdtmDateOnly.Day,dtmBaseFrom.Hour,dtmBaseFrom.Minute,dtmBaseFrom.Second,dtmBaseFrom.Millisecond);
			DateTime dtmRealTo = new DateTime(pdtmDateOnly.Year,pdtmDateOnly.Month,pdtmDateOnly.Day,dtmBaseFrom.Hour,dtmBaseFrom.Minute,dtmBaseFrom.Second,dtmBaseFrom.Millisecond);
			dtmRealTo = dtmRealTo.AddDays(dblDiffDays);

			pdtmFromTime = dtmRealFrom;
			if (pdtmFromTime < pdtmStartTime)
			{
				pdtmFromTime = pdtmStartTime;
			}
			pdtmToTime = dtmRealTo;
			
			//pdtmFromTime = ChangeSameShift(pdtmDateOnly,dtmBaseFrom,dtmBaseTo);
			//pdtmToTime = ChangeSameShift(pdtmDateOnly,dtmBaseFrom,dtmBaseTo);
			if(arrResultDetails.Length == 0) // Get all from starttime to end of last shift in working day
			{
				if(pdtmFromTime == pdtmToTime)
				{
					return false;
				}
				else
				{
					return true;
				}
			}
			else // drowDetails.Length > 0
			{				
				DateTime dtmPartStart = DateTime.Parse(arrResultDetails[0][PRO_DCPResultDetailTable.STARTTIME_FLD].ToString());		
				DateTime dtmPartEnd = DateTime.Parse(arrResultDetails[0][PRO_DCPResultDetailTable.ENDTIME_FLD].ToString());

				if (dtmPartStart <= pdtmStartTime)
				{
					pdtmFromTime = dtmPartEnd;
					if (arrResultDetails.Length > 1)
					{
						DateTime dtmNextPartStart = DateTime.Parse(arrResultDetails[1][PRO_DCPResultDetailTable.STARTTIME_FLD].ToString());
						if (dtmNextPartStart < dtmRealTo)
						{
							pdtmToTime = dtmNextPartStart;
						}
						else
						{
							pdtmToTime = dtmRealTo;
						}
					}
					else
					{
						pdtmToTime = dtmRealTo;
					}
				}
				else
				{
					pdtmFromTime = pdtmStartTime;
					if (dtmPartStart < dtmRealTo)
					{
						pdtmToTime = dtmPartStart;
					}
					else
					{
						pdtmToTime = dtmRealTo;
					}
				}

				if(pdtmFromTime == pdtmToTime)
				{
					return false;
				}
				else
				{
					return true;
				}

				// Get from starttime if
				/*if(pdtmStartTime < (DateTime)drowNextDetails[0][PRO_DCPResultDetailTable.STARTTIME_FLD])
				{
					DataRow[] drowDetailPrevs = pdstDCPResultDetail.Tables[0].Select(
						MST_WorkCenterTable.WORKCENTERID_FLD + "=" + pintWorkCenterID
						+ " AND " + DATE_FLD + "= '" + pdtmDateOnly + "'"
						+ " AND " + PRO_DCPResultDetailTable.ENDTIME_FLD + "< '" + pdtmStartTime + "'",
						PRO_DCPResultDetailTable.ENDTIME_FLD + " DESC");
					pdtmToTime = (DateTime)drowNextDetails[0][PRO_DCPResultDetailTable.STARTTIME_FLD];
					// Don't found prev filled
					if(drowDetailPrevs.Length == 0)
					{
						if(pdtmFromTime == pdtmToTime) return false;
						return true;
					}
					else //drowDetailPrevs.Length > 0
					{
						pdtmFromTime = (DateTime)drowDetailPrevs[0][PRO_DCPResultDetailTable.ENDTIME_FLD];
						if(pdtmFromTime == pdtmToTime) return false;
						return true;
					}
				}
				else // Get from row[0][EndTime] if pdtmStartTime >= row[0][StartTime]
				{
					pdtmFromTime = (DateTime)drowNextDetails[0][PRO_DCPResultDetailTable.ENDTIME_FLD];
					if(drowNextDetails.Length > 1)
					{
						pdtmToTime = (DateTime)drowNextDetails[1][PRO_DCPResultDetailTable.STARTTIME_FLD];
						if(pdtmFromTime == pdtmToTime) return false;
						return true;
					}
					if(pdtmFromTime == pdtmToTime) return false;
					return true;
				}*/
			}
		}

	
		/// <summary>
		/// Get routing type of a routing record
		/// </summary>
		/// <param name="pdrowRouting">
		/// routing record
		/// </param>
		/// <returns>
		/// Routing type
		/// </returns>
		private int GetRoutingType(DataRow pdrowRouting)
		{
			int intType = LINEAR;
			// if parallel scheduling
			if (decimal.Parse(pdrowRouting[ITM_RoutingTable.SCHEDULESEQ_FLD].ToString()) > 0)
			{
				intType = PARALLEL;
			}
			else if (decimal.Parse(pdrowRouting[ITM_RoutingTable.OVERLAPPERCENT_FLD].ToString()) > 0)
				//if overlap % scheduling
			{
				intType = OVERLAPPERCENT;
			}
			else if (decimal.Parse(pdrowRouting[ITM_RoutingTable.OVERLAPQTY_FLD].ToString()) > 0)
				//if overlap quantity scheduling
			{
				intType = OVERLAPQTY;
			}
			return intType;
		}


		/// <summary>
		/// Get routing param of a routing record
		/// </summary>
		/// <param name="pdrowRouting">
		/// routing record
		/// </param>
		/// <returns>
		/// Routing param
		/// </returns>
		private decimal GetRoutingParam(DataRow pdrowRouting)
		{
			decimal decParam = 0;
			// if parallel scheduling
			if (decimal.Parse(pdrowRouting[ITM_RoutingTable.SCHEDULESEQ_FLD].ToString()) > 0)
			{
				return decimal.Parse(pdrowRouting[ITM_RoutingTable.SCHEDULESEQ_FLD].ToString());
			}
			else if (decimal.Parse(pdrowRouting[ITM_RoutingTable.OVERLAPPERCENT_FLD].ToString()) > 0)
				//if overlap % scheduling
			{
				return decimal.Parse(pdrowRouting[ITM_RoutingTable.OVERLAPPERCENT_FLD].ToString());
			}
			else if (decimal.Parse(pdrowRouting[ITM_RoutingTable.OVERLAPQTY_FLD].ToString()) > 0)
				//if overlap quantity scheduling
			{
				return decimal.Parse(pdrowRouting[ITM_RoutingTable.OVERLAPQTY_FLD].ToString());
			}
			return decParam;
		}


		/// <summary>
		/// convert rotuing type to operation type
		/// </summary>
		/// <param name="pintRoutingType">routing typy</param>
		/// <param name="pdrowRoutings">all routing records</param>
		/// <param name="pintCurrentOp">current operation</param>
		/// <param name="pdecQuantity">item quantity</param>
		/// <returns>operation type</returns>
		private int GetOperationTypeByRoutingType(
			int pintRoutingType, 
			DataRow[] pdrowRoutings, 
			int pintCurrentOp, 
			decimal pdecQuantity)
		{
			int intOpType = 0;
			DataRow drowCurrentRouting = pdrowRoutings[pintCurrentOp];

			switch (pintRoutingType)
			{
				case LINEAR:
					intOpType = LINEAR;
					break;
				case PARALLEL:
					decimal decSchSeq = GetRoutingParam(drowCurrentRouting);
					int intPrevRoutingIdx = pintCurrentOp - 1;
					bool blnSameSeqFound = false;
					while (intPrevRoutingIdx > 0)
					{
						DataRow drowPrevRouting = pdrowRoutings[intPrevRoutingIdx];
						int intPrevRoutingType = GetRoutingType(drowPrevRouting);
						if (intPrevRoutingType == PARALLEL)
						{
							decimal decPrevSchSeq = GetRoutingParam(drowPrevRouting);
							if (decPrevSchSeq.Equals(decSchSeq))
							{
								blnSameSeqFound = true;
								break;
							}
						}
						intPrevRoutingIdx--;
					}
					if (blnSameSeqFound)
					{
						intOpType = PARALLEL;
					}
					else
					{
						intOpType = LINEAR;
					}
					break;
				case OVERLAPPERCENT:
					decimal decOverlapPercent = GetRoutingParam(drowCurrentRouting);
					if (decOverlapPercent > 100)
					{
						intOpType = LINEAR;
					}
					else
					{
						intOpType = OVERLAPPERCENT;
					}
					break;
				case OVERLAPQTY:
					decimal decOverlapQty = GetRoutingParam(drowCurrentRouting);
					if (pdecQuantity < decOverlapQty)
					{
						intOpType = LINEAR;
					}
					else
					{
						intOpType = OVERLAPQTY;
					}
					break;
				default:
					break;
			}

			return intOpType;
		}


		/// <summary>
		/// Get master record of previous operation
		/// </summary>
		/// <param name="pdrowPrevRouting">routing record equivalent to previous operation</param>
		/// <returns>Routing type</returns>
		private DataRow GetPrevOpResultMaster(
			DataSet pdstDCPResultMaster, 
			DataRow pdrowPrevRouting)
		{
			//find previous operation, base on RoutingID or WORoutingID
			int intOpRoutingID = 0;
			int intItemRoutingDetailID = 0;
			DataRow[] arrPrevOpMasters;
			DataRow drowPrevOpMaster = null;

			intItemRoutingDetailID = int.Parse(pdrowPrevRouting[DETAILID_FLD].ToString());
			arrPrevOpMasters = pdstDCPResultMaster.Tables[0].Select(DETAILID_FLD + "=" + intItemRoutingDetailID.ToString());
			if (arrPrevOpMasters.Length > 0)
			{
				drowPrevOpMaster = arrPrevOpMasters[0];
			}
			return drowPrevOpMaster;
		}


		/// <summary>
		/// Get result detail record for previous part of operation
		/// </summary>
		/// <param name="pdstDCPResultDetail">datatable contains all result detail records</param>
		/// <param name="pintOpMasterID">result master record of current operation</param>
		/// <param name="pintCurrentPart">current part of operation</param>
		/// <returns>result detail record</returns>
		private DataRow GetPrevPartResultDetail(
			DataSet pdstDCPResultDetail, 
			int pintOpMasterID, 
			int pintCurrentPart)
		{
			if (pintCurrentPart <= 0)
			{
				return null;
			}

			DataRow[] arrPrevParts = pdstDCPResultDetail.Tables[0].Select(PRO_DCPResultDetailTable.DCPRESULTMASTERID_FLD + "=" + pintOpMasterID.ToString()
				+ " AND Type =" + ((int)DCPResultTypeEnum.Running).ToString());
			//Sure it has [intCurrentPart] rows in arrPrevParts
			DataRow drowPrevPart = arrPrevParts[pintCurrentPart - 1];
			return drowPrevPart;
		}


		/// <summary>
		/// Clear all results relate to item
		/// </summary>
		/// <param name="pintWODetailID">work order detail ID</param>
		/// <param name="pintCPOID">CPO ID</param>
		/// <param name="pblnIsWOLine">TRUE if item is a work order line</param>
		private void ClearItemDCPResult(
			ref DataSet pdstDCPResultMaster, 
			ref DataSet pdstDCPResultDetail, 
			int pintWODetailID, 
			int pintCPOID, 
			bool pblnIsWOLine)
		{			
			//Clear all related data
			DataRow[] arrMasterResults;
			DataRow[] arrDetailResults;
			if (pblnIsWOLine)
			{
				arrMasterResults = pdstDCPResultMaster.Tables[0].Select(PRO_DCPResultMasterTable.WORKORDERDETAILID_FLD + "=" + pintWODetailID.ToString());
			}
			else
			{				
				arrMasterResults = pdstDCPResultMaster.Tables[0].Select(PRO_DCPResultMasterTable.CPOID_FLD + "=" + pintCPOID.ToString());
			}
			if (arrMasterResults.Length > 0)
			{
				string strMasterIDs = "(0,";
				foreach (DataRow drowMasterResult in arrMasterResults)
				{
					strMasterIDs += drowMasterResult[PRO_DCPResultMasterTable.DCPRESULTMASTERID_FLD].ToString() + ",";
				}
				strMasterIDs = strMasterIDs.Substring(0,strMasterIDs.Length - 1) + ")";

				//int intMasterResultID = int.Parse(arrMasterResults[0][PRO_DCPResultMasterTable.DCPRESULTMASTERID_FLD].ToString());
				arrDetailResults = pdstDCPResultDetail.Tables[0].Select(PRO_DCPResultDetailTable.DCPRESULTMASTERID_FLD + " IN " + strMasterIDs);
				//Delete all detail
				foreach (DataRow drow in arrDetailResults)
				{
					drow.Delete();
				}
				//Delete master
				foreach (DataRow drow in arrMasterResults)
				{
					drow.Delete();
				}
			}
		}


		/// <summary>
		/// Get earliest time that current operation-part can fill
		/// Author : DuongNA
		/// </summary>
		/// <param name="pintOpType"></param>
		/// <param name="pintCurrentOp"></param>
		/// <param name="pintOpMasterID"></param>
		/// <param name="pintCurrentPart"></param>
		/// <param name="pdtmStartTime"></param>
		/// <param name="pdrowRoutings"></param>
		/// <param name="drowCurrentRouting"></param>
		/// <returns></returns>
		private DateTime GetOpPartAvailableStartTime(int pintOpType, int pintCurrentOp, int pintOpMasterID, int pintCurrentPart, decimal pdecQuantity, DateTime pdtmStartTime, DataRow[] pdrowRoutings, DataRow drowCurrentRouting, DataSet pdstDCPResultMaster, DataSet pdstDCPResultDetail, DataTable pdtbWCCapacity, DataTable pdtbWCCapacityAndShift, int pintWorkCenterID)
		
		{
			const decimal ONE_HUNDRED = 100;
			DateTime dtmPartAvailableStart = DateTime.MinValue;
			//Check RoutingType to get OpType
			switch (pintOpType)
			{
				case LINEAR:
					//Operation is linear with its previous operation
					//Then, if part is first part of operation, part can start after previous operation stopped only
					//In case current operation has no operation before, first part available to start at given start time
					//If part is not first part, it can start after previous part stopped only
					if (pintCurrentPart == 0)
					{
						if (pintCurrentOp == 0)
						{
							//no constraint here, avalaible to start freely
							dtmPartAvailableStart = pdtmStartTime;
						}
						else
						{
							DataRow drowPrevRouting = pdrowRoutings[pintCurrentOp - 1];
							DataRow drowPrevOpMaster = GetPrevOpResultMaster(pdstDCPResultMaster, drowPrevRouting);
							//Note : don't remember to fill StartTime and DueTime when make Master Record
							dtmPartAvailableStart = DateTime.Parse(drowPrevOpMaster[PRO_DCPResultMasterTable.DUEDATETIME_FLD].ToString());
						}
					}
					else
					{
						//Find previous Part
						DataRow drowPrevPart = GetPrevPartResultDetail(pdstDCPResultDetail, pintOpMasterID, pintCurrentPart);
						dtmPartAvailableStart = DateTime.Parse(drowPrevPart[PRO_DCPResultDetailTable.ENDTIME_FLD].ToString());
					}
					break;
				case PARALLEL:
					//Operation is parallel with an operation before it
					//Sure that exist an operation before it has same scheduling sequence !!!
					//Then, if part is first part of operation, part can start after its "paralleled" operation started !!!
					//If part is not first part, it can start after previous part stopped only
					if (pintCurrentPart == 0)
					{
						//find operation "parallel" with current operation
						decimal decSchSeq = GetRoutingParam(drowCurrentRouting);
						int intPrevRoutingIdx = pintCurrentOp - 1;
						while (intPrevRoutingIdx >= 0)
						{
							DataRow drowPrevRouting = pdrowRoutings[intPrevRoutingIdx];
							int intPrevRoutingType = GetRoutingType(drowPrevRouting);
							if (intPrevRoutingType == PARALLEL)
							{
								decimal decPrevSchSeq = GetRoutingParam(drowPrevRouting);
								if (decPrevSchSeq.Equals(decSchSeq))
								{
									//found
									DataRow drowPrevOpMaster = GetPrevOpResultMaster(pdstDCPResultMaster, drowPrevRouting);
									//Note : don't remember to fill StartTime and DueTime when make Master Record
									dtmPartAvailableStart = DateTime.Parse(drowPrevOpMaster[PRO_DCPResultMasterTable.STARTDATETIME_FLD].ToString());
									break;
								}
							}
							intPrevRoutingIdx--;
						}
					}
					else
					{
						//Find previous Part
						DataRow drowPrevPart = GetPrevPartResultDetail(pdstDCPResultDetail, pintOpMasterID, pintCurrentPart);
						dtmPartAvailableStart = DateTime.Parse(drowPrevPart[PRO_DCPResultDetailTable.ENDTIME_FLD].ToString());
					}
					break;
				case OVERLAPPERCENT:
					//Operation is overlap previous operation
					//If part is first part of operation, part can start after previous operation start "delta t" time
					//"delta t" base on overlap-percent and previous operation schedule
					//If part is not first part, it can start after previous part stopped only
					if (pintCurrentPart == 0)
					{
						if (pintCurrentOp == 0)
						{
							//no constraint here, avalaible to start freely
							dtmPartAvailableStart = pdtmStartTime;
						}
						else
						{
							DataRow drowPrevRouting = pdrowRoutings[pintCurrentOp - 1];
							int intPrevOpWorkCenterID = int.Parse(drowPrevRouting[PRO_WORoutingTable.WORKCENTERID_FLD].ToString());
							DataRow drowPrevOpMaster = GetPrevOpResultMaster(pdstDCPResultMaster, drowPrevRouting);
							int intPrevOpMasterID = int.Parse(drowPrevOpMaster[PRO_DCPResultMasterTable.DCPRESULTMASTERID_FLD].ToString());
							//calculate previous operation total quantity
							decimal decPrevTotalQty = pdecQuantity;//decimal.Parse(pdstDCPResultDetail.Tables[0].Compute("Sum(" + PRO_DCPResultDetailTable.QUANTITY_FLD + ")", PRO_DCPResultDetailTable.DCPRESULTMASTERID_FLD + "=" + intPrevOpMasterID.ToString()).ToString());


							decimal decOverlapPct = GetRoutingParam(drowCurrentRouting);
							decimal decPrevTotalLeadTime = decimal.Parse(drowPrevRouting[LEADTIME_FLD].ToString());

							//get all part of previous operation
							DataRow[] arrPrevOpDetails = pdstDCPResultDetail.Tables[0].Select(PRO_DCPResultDetailTable.DCPRESULTMASTERID_FLD + "=" + intPrevOpMasterID.ToString());
							decimal decTempPct = 0;
							decimal decLastPct = 0;
							int intPrevOpDetailIdx = 0;
							DateTime dtmTempPartStart = DateTime.MinValue;

							while ((intPrevOpDetailIdx < arrPrevOpDetails.Length) && (decTempPct < decOverlapPct))
							{
								DataRow drowPrevOpDetail = arrPrevOpDetails[intPrevOpDetailIdx];
								decLastPct = ONE_HUNDRED * decimal.Parse(drowPrevOpDetail[PRO_DCPResultDetailTable.QUANTITY_FLD].ToString())/decPrevTotalQty;
								decTempPct += decLastPct;
								dtmTempPartStart = DateTime.Parse(drowPrevOpDetail[PRO_DCPResultDetailTable.STARTTIME_FLD].ToString());
								intPrevOpDetailIdx++;
							}
							//decimal decGreaterQty = decTempQty - decOverlapQty;
							decimal decNeededToEqualOverlapPct = decOverlapPct - (decTempPct - decLastPct);
							decimal decNeededToEqualOverlapPctLeadTime = (decNeededToEqualOverlapPct / ONE_HUNDRED) * decPrevTotalLeadTime;
							DateTime dtmPrevOpWorkingDate = GetDateOnlyByWorkCenter(dtmTempPartStart,intPrevOpWorkCenterID,pdtbWCCapacityAndShift);
							decimal decNeededToEqualOverlapPctRealTime = ConvertToRealTime(dtmPrevOpWorkingDate,decNeededToEqualOverlapPctLeadTime, intPrevOpWorkCenterID,pdtbWCCapacity);
							
							DataRow[] drowShifts = pdtbWCCapacityAndShift.Select(PRO_WCCapacityTable.WORKCENTERID_FLD 
								+ "=" + intPrevOpWorkCenterID,PRO_ShiftPatternTable.WORKTIMEFROM_FLD + " ASC");
							//dtmPartAvailableStart = AddMoreSecondForwardOneDay(drowShifts,dtmTempPartStart,(double)decNeededToEqualOverlapPctRealTime,dtmPrevOpWorkingDate);
						}
					}
					else
					{
						//Find previous Part
						DataRow drowPrevPart = GetPrevPartResultDetail(pdstDCPResultDetail, pintOpMasterID, pintCurrentPart);
						decimal decPrevTotalQty = pdecQuantity;//decimal.Parse(pdstDCPResultDetail.Tables[0].Compute("Sum(" + PRO_DCPResultDetailTable.QUANTITY_FLD + ")", PRO_DCPResultDetailTable.DCPRESULTMASTERID_FLD + "=" + intPrevOpMasterID.ToString()).ToString());
						DateTime dtmPrevEnd = DateTime.Parse(drowPrevPart[PRO_DCPResultDetailTable.ENDTIME_FLD].ToString());
						if (pintCurrentOp == 0)
						{
							dtmPartAvailableStart = dtmPrevEnd;
						} 
						else
						{ 
							//calculate
							decimal decCompletedLeadTime = GetCurrentOpCompletedLeadTime(dtmPrevEnd,pdstDCPResultMaster,pdstDCPResultDetail,pdrowRoutings,pintCurrentOp,pdtbWCCapacity,pdtbWCCapacityAndShift,pintWorkCenterID);							
							decimal decTotalLeadTime = GetOperationTotalLeadTime(pdrowRoutings,pintCurrentOp);
							decimal decCompletedPct = (decCompletedLeadTime / decTotalLeadTime) * ONE_HUNDRED;
							decimal decOverlapPct = GetRoutingParam(drowCurrentRouting) + decCompletedPct;

							//find time where prev op completed decOverlapQty
							DataRow drowPrevRouting = pdrowRoutings[pintCurrentOp - 1];
							int intPrevOpWorkCenterID = int.Parse(drowPrevRouting[PRO_WORoutingTable.WORKCENTERID_FLD].ToString());
							DataRow drowPrevOpMaster = GetPrevOpResultMaster(pdstDCPResultMaster, drowPrevRouting);
							int intPrevOpMasterID = int.Parse(drowPrevOpMaster[PRO_DCPResultMasterTable.DCPRESULTMASTERID_FLD].ToString());
							//calculate previous operation total quantity
							decimal decPrevTotalLeadTime = decimal.Parse(drowPrevRouting[LEADTIME_FLD].ToString());

							//get all part of previous operation
							DataRow[] arrPrevOpDetails = pdstDCPResultDetail.Tables[0].Select(PRO_DCPResultDetailTable.DCPRESULTMASTERID_FLD + "=" + intPrevOpMasterID.ToString());
							decimal decTempPct = 0;
							decimal decLastPct = 0;
							int intPrevOpDetailIdx = 0;
							DateTime dtmTempPartStart = DateTime.MinValue;
							while ((intPrevOpDetailIdx < arrPrevOpDetails.Length) && (decTempPct < decOverlapPct))
							{
								DataRow drowPrevOpDetail = arrPrevOpDetails[intPrevOpDetailIdx];
								decLastPct = ONE_HUNDRED * decimal.Parse(drowPrevOpDetail[PRO_DCPResultDetailTable.QUANTITY_FLD].ToString())/decPrevTotalQty;
								decTempPct += decLastPct;
								dtmTempPartStart = DateTime.Parse(drowPrevOpDetail[PRO_DCPResultDetailTable.STARTTIME_FLD].ToString());
								intPrevOpDetailIdx++;
							}
							//decimal decGreaterQty = decTempQty - decOverlapQty;
							decimal decNeededToEqualOverlapPct = decOverlapPct - (decTempPct - decLastPct);
							decimal decNeededToEqualOverlapPctLeadTime = (decNeededToEqualOverlapPct / ONE_HUNDRED) * decPrevTotalLeadTime;
							DateTime dtmWorkingDate = GetDateOnlyByWorkCenter(dtmTempPartStart,intPrevOpWorkCenterID,pdtbWCCapacityAndShift);
							decimal decNeededToEqualOverlapQtyRealTime = ConvertToRealTime(dtmWorkingDate,decNeededToEqualOverlapPctLeadTime, intPrevOpWorkCenterID,pdtbWCCapacity);
							
							DataRow[] drowShifts = pdtbWCCapacityAndShift.Select(PRO_WCCapacityTable.WORKCENTERID_FLD 
								+ "=" + intPrevOpWorkCenterID,PRO_ShiftPatternTable.WORKTIMEFROM_FLD + " ASC");
							//dtmPartAvailableStart = AddMoreSecondForwardOneDay(drowShifts,dtmTempPartStart,(double)decNeededToEqualOverlapQtyRealTime,dtmWorkingDate);
							
							if (dtmPartAvailableStart < dtmPrevEnd)
							{
								dtmPartAvailableStart = dtmPrevEnd;
							}
						}
					}
					break;
				case OVERLAPQTY:
					//Operation is overlap previous operation
					//If part is first part of operation, part can start after previous operation start "delta t" time
					//"delta t" base on overlap-percent and previous operation schedule
					//If part is not first part, it can start after previous part stopped only
					if (pintCurrentPart == 0)
					{
						if (pintCurrentOp == 0)
						{
							//no constraint here, avalaible to start freely
							dtmPartAvailableStart = pdtmStartTime;
						}
						else
						{
							DataRow drowPrevRouting = pdrowRoutings[pintCurrentOp - 1];
							int intPrevOpWorkCenterID = int.Parse(drowPrevRouting[PRO_WORoutingTable.WORKCENTERID_FLD].ToString());
							DataRow drowPrevOpMaster = GetPrevOpResultMaster(pdstDCPResultMaster, drowPrevRouting);
							int intPrevOpMasterID = int.Parse(drowPrevOpMaster[PRO_DCPResultMasterTable.DCPRESULTMASTERID_FLD].ToString());
							//calculate previous operation total quantity
							decimal decPrevTotalQty = pdecQuantity;//decimal.Parse(pdstDCPResultDetail.Tables[0].Compute("Sum(" + PRO_DCPResultDetailTable.QUANTITY_FLD + ")", PRO_DCPResultDetailTable.DCPRESULTMASTERID_FLD + "=" + intPrevOpMasterID.ToString()).ToString());
							decimal decOverlapQty = GetRoutingParam(drowCurrentRouting);
							decimal decPrevTotalLeadTime = decimal.Parse(drowPrevRouting[LEADTIME_FLD].ToString());

							//get all part of previous operation
							DataRow[] arrPrevOpDetails = pdstDCPResultDetail.Tables[0].Select(PRO_DCPResultDetailTable.DCPRESULTMASTERID_FLD + "=" + intPrevOpMasterID.ToString());
							decimal decTempQty = 0;
							decimal decLastQty = 0;
							int intPrevOpDetailIdx = 0;
							DateTime dtmTempPartStart = DateTime.MinValue;

							while ((intPrevOpDetailIdx < arrPrevOpDetails.Length) && (decTempQty < decOverlapQty))
							{
								DataRow drowPrevOpDetail = arrPrevOpDetails[intPrevOpDetailIdx];
								decLastQty = decimal.Parse(drowPrevOpDetail[PRO_DCPResultDetailTable.QUANTITY_FLD].ToString());
								decTempQty += decLastQty;
								dtmTempPartStart = DateTime.Parse(drowPrevOpDetail[PRO_DCPResultDetailTable.STARTTIME_FLD].ToString());
								intPrevOpDetailIdx++;
							}
							//decimal decGreaterQty = decTempQty - decOverlapQty;
							decimal decNeededToEqualOverlapQty = decOverlapQty - (decTempQty - decLastQty);
							decimal decNeededToEqualOverlapQtyLeadTime = (decNeededToEqualOverlapQty / decPrevTotalQty) * decPrevTotalLeadTime;
							DateTime dtmWorkingDate = GetDateOnlyByWorkCenter(dtmTempPartStart,intPrevOpWorkCenterID,pdtbWCCapacityAndShift);
							decimal decNeededToEqualOverlapQtyRealTime = ConvertToRealTime(dtmWorkingDate,decNeededToEqualOverlapQtyLeadTime, intPrevOpWorkCenterID,pdtbWCCapacity);
							DataRow[] drowShifts = pdtbWCCapacityAndShift.Select(PRO_WCCapacityTable.WORKCENTERID_FLD 
								+ "=" + intPrevOpWorkCenterID,PRO_ShiftPatternTable.WORKTIMEFROM_FLD + " ASC");
							//dtmPartAvailableStart = AddMoreSecondForwardOneDay(drowShifts,dtmTempPartStart,(double)decNeededToEqualOverlapQtyRealTime,dtmWorkingDate);
						}
					}
					else
					{
						//Find previous Part
						DataRow drowPrevPart = GetPrevPartResultDetail(pdstDCPResultDetail, pintOpMasterID, pintCurrentPart);
						DateTime dtmPrevEnd = DateTime.Parse(drowPrevPart[PRO_DCPResultDetailTable.ENDTIME_FLD].ToString());							

						if (pintCurrentOp == 0)
						{
							dtmPartAvailableStart = dtmPrevEnd;
						} 
						else
						{ 
							//calculate
							decimal decCompletedLeadTime = GetCurrentOpCompletedLeadTime(dtmPrevEnd,pdstDCPResultMaster,pdstDCPResultDetail,pdrowRoutings,pintCurrentOp,pdtbWCCapacity,pdtbWCCapacityAndShift,pintWorkCenterID);							
							decimal decTotalLeadTime = GetOperationTotalLeadTime(pdrowRoutings,pintCurrentOp);
							decimal decCompletedQty = (decCompletedLeadTime / decTotalLeadTime) * pdecQuantity;
							decimal decOverlapQty = GetRoutingParam(drowCurrentRouting) + decCompletedQty;

							//find time where prev op completed decOverlapQty
							DataRow drowPrevRouting = pdrowRoutings[pintCurrentOp - 1];
							int intPrevOpWorkCenterID = int.Parse(drowPrevRouting[PRO_WORoutingTable.WORKCENTERID_FLD].ToString());
							DataRow drowPrevOpMaster = GetPrevOpResultMaster(pdstDCPResultMaster, drowPrevRouting);
							int intPrevOpMasterID = int.Parse(drowPrevOpMaster[PRO_DCPResultMasterTable.DCPRESULTMASTERID_FLD].ToString());
							//calculate previous operation total quantity
							decimal decPrevTotalQty = decimal.Parse(pdstDCPResultDetail.Tables[0].Compute("Sum(" + PRO_DCPResultDetailTable.QUANTITY_FLD + ")", PRO_DCPResultDetailTable.DCPRESULTMASTERID_FLD + "=" + intPrevOpMasterID.ToString()).ToString());
							decimal decPrevTotalLeadTime = decimal.Parse(drowPrevRouting[LEADTIME_FLD].ToString());

							//get all part of previous operation
							DataRow[] arrPrevOpDetails = pdstDCPResultDetail.Tables[0].Select(PRO_DCPResultDetailTable.DCPRESULTMASTERID_FLD + "=" + intPrevOpMasterID.ToString());
							decimal decTempQty = 0;
							decimal decLastQty = 0;
							int intPrevOpDetailIdx = 0;
							DateTime dtmTempPartStart = DateTime.MinValue;
							while ((intPrevOpDetailIdx < arrPrevOpDetails.Length) && (decTempQty < decOverlapQty))
							{
								DataRow drowPrevOpDetail = arrPrevOpDetails[intPrevOpDetailIdx];
								decLastQty = decimal.Parse(drowPrevOpDetail[PRO_DCPResultDetailTable.QUANTITY_FLD].ToString());
								decTempQty += decLastQty;
								dtmTempPartStart = DateTime.Parse(drowPrevOpDetail[PRO_DCPResultDetailTable.STARTTIME_FLD].ToString());
								intPrevOpDetailIdx++;
							}
							//decimal decGreaterQty = decTempQty - decOverlapQty;
							decimal decNeededToEqualOverlapQty = decOverlapQty - (decTempQty - decLastQty);
							decimal decNeededToEqualOverlapQtyLeadTime = (decNeededToEqualOverlapQty / decPrevTotalQty) * decPrevTotalLeadTime;
							DateTime dtmWorkingDate = GetDateOnlyByWorkCenter(dtmTempPartStart,intPrevOpWorkCenterID,pdtbWCCapacityAndShift);
							decimal decNeededToEqualOverlapQtyRealTime = ConvertToRealTime(dtmWorkingDate,decNeededToEqualOverlapQtyLeadTime, intPrevOpWorkCenterID,pdtbWCCapacity);
							DataRow[] drowShifts = pdtbWCCapacityAndShift.Select(PRO_WCCapacityTable.WORKCENTERID_FLD 
								+ "=" + intPrevOpWorkCenterID,PRO_ShiftPatternTable.WORKTIMEFROM_FLD + " ASC");
							//dtmPartAvailableStart = AddMoreSecondForwardOneDay(drowShifts,dtmTempPartStart,(double)decNeededToEqualOverlapQtyRealTime,dtmWorkingDate);
							if (dtmPartAvailableStart < dtmPrevEnd)
							{
								dtmPartAvailableStart = dtmPrevEnd;
							}
						}
					}
					break;
				default:
					break;
			}

			return dtmPartAvailableStart;
		}


		/// <summary>
		/// Check available start time to be sure that it is in the working day has remain capacity
		/// </summary>
		/// <param name="pdtmCurrentWorkingDay"></param>
		/// <param name="pdtmPartAvailableStart"></param>
		/// <param name="pintWorkCenterID"></param>
		/// <param name="pdtbWCCapacityAndShift"></param>
		/// <param name="pdtbCurrentCapacity"></param>
		/// <param name="pdstCalendar"></param>
		/// <returns></returns>
		private DateTime CorrectPartAvailableStart(ref DateTime pdtmCurrentWorkingDay, DateTime pdtmPartAvailableStart, int pintWorkCenterID, DataTable pdtbWCCapacityAndShift, DataTable pdtbCurrentCapacity, DataSet pdstCalendar)
		{
			const string METHOD_NAME = THIS + ".CorrectPartAvailableStart()";
			const string WORKCENTERCODE = "WorkCenterCode";

			//normalize workingday
			DataRow[] drowShifts = pdtbWCCapacityAndShift.Select(PRO_WCCapacityTable.WORKCENTERID_FLD 
				+ "=" + pintWorkCenterID
				+ " AND " + PRO_WCCapacityTable.BEGINDATE_FLD + " <= '" + pdtmCurrentWorkingDay + "'"
				+ " AND " + PRO_WCCapacityTable.ENDDATE_FLD + " >= '" + pdtmCurrentWorkingDay + "'",
				PRO_ShiftPatternTable.WORKTIMEFROM_FLD + " ASC");
			if (drowShifts.Length == 0)
			{
				//TODO : modify exception throw
				throw new PCSException(0,string.Empty,null);
			}

			pdtmCurrentWorkingDay = new DateTime(pdtmCurrentWorkingDay.Year,pdtmCurrentWorkingDay.Month,pdtmCurrentWorkingDay.Day,
				((DateTime)drowShifts[0][PRO_ShiftPatternTable.WORKTIMEFROM_FLD]).Hour,
				((DateTime)drowShifts[0][PRO_ShiftPatternTable.WORKTIMEFROM_FLD]).Minute,
				((DateTime)drowShifts[0][PRO_ShiftPatternTable.WORKTIMEFROM_FLD]).Second);

			//Check if current capacity is available at working day
			DataRow[] arrWCC = pdtbWCCapacityAndShift.Select(PRO_WCCapacityTable.WORKCENTERID_FLD + "=" + pintWorkCenterID.ToString());
			string strWorkCenter = arrWCC[0][WORKCENTERCODE].ToString();
			DateTime dtmWorkingDayBase = new DateTime(pdtmCurrentWorkingDay.Year,pdtmCurrentWorkingDay.Month,pdtmCurrentWorkingDay.Day,0,0,0);
			DataRow[] arrCurrentCapacity = pdtbCurrentCapacity.Select(DATE_FLD + " >= '" + dtmWorkingDayBase.ToString() + "'"

				+ " AND [" + strWorkCenter + "] >= " + ((double)100*EPSILON).ToString(), 
				DATE_FLD + " ASC");			
			
			if (arrCurrentCapacity.Length <= 0)
			{
				throw new PCSBOException(ErrorCode.MESSAGE_DCP_SETTING_WORKING_CALENDAR,METHOD_NAME,null);
			}
			
			//Move current working day to first day remain capacity
			pdtmCurrentWorkingDay = DateTime.Parse(arrCurrentCapacity[0][DATE_FLD].ToString());
			pdtmCurrentWorkingDay = new DateTime(pdtmCurrentWorkingDay.Year,pdtmCurrentWorkingDay.Month,pdtmCurrentWorkingDay.Day,
				((DateTime)drowShifts[0][PRO_ShiftPatternTable.WORKTIMEFROM_FLD]).Hour,
				((DateTime)drowShifts[0][PRO_ShiftPatternTable.WORKTIMEFROM_FLD]).Minute,
				((DateTime)drowShifts[0][PRO_ShiftPatternTable.WORKTIMEFROM_FLD]).Second);
			
			DateTime dtmPartAvailableStartWorkingDay = GetDateOnlyByWorkCenter(pdtmPartAvailableStart,pintWorkCenterID,pdtbWCCapacityAndShift);
			if (dtmPartAvailableStartWorkingDay > pdtmCurrentWorkingDay)
			{
				//Move Current working day to working day of AvailableStart date				
				pdtmCurrentWorkingDay = GetDateOnlyByWorkCenter(pdtmPartAvailableStart,pintWorkCenterID,pdtbWCCapacityAndShift);
				pdtmCurrentWorkingDay = new DateTime(pdtmCurrentWorkingDay.Year,pdtmCurrentWorkingDay.Month,pdtmCurrentWorkingDay.Day,
					((DateTime)drowShifts[0][PRO_ShiftPatternTable.WORKTIMEFROM_FLD]).Hour,
					((DateTime)drowShifts[0][PRO_ShiftPatternTable.WORKTIMEFROM_FLD]).Minute,
					((DateTime)drowShifts[0][PRO_ShiftPatternTable.WORKTIMEFROM_FLD]).Second);
			}
			else if (pdtmPartAvailableStart < pdtmCurrentWorkingDay)
			{
				//dtmPartAvailableStart = new DateTime(pdtmCurrentWorkingDay.Year,pdtmCurrentWorkingDay.Month,pdtmCurrentWorkingDay.Day,0,0,0);
				//move to first shift start
				pdtmPartAvailableStart = pdtmCurrentWorkingDay;
			}

			//check if available start time is in stoptime
			bool blnWorkingTime = IsWorkingTime(pdtmPartAvailableStart,pintWorkCenterID,pdtbWCCapacityAndShift,pdstCalendar);
			//move to next working time if needed
			if (!blnWorkingTime)
			{
				pdtmPartAvailableStart = GetNextWorkingTime(pdtmPartAvailableStart,pintWorkCenterID,string.Empty,pdtbWCCapacityAndShift,pdstCalendar,pdtbCurrentCapacity);
			}

			//round to seconds
			DateTime dtmReturn = new DateTime(pdtmPartAvailableStart.Year,pdtmPartAvailableStart.Month,pdtmPartAvailableStart.Day,pdtmPartAvailableStart.Hour,pdtmPartAvailableStart.Minute,pdtmPartAvailableStart.Second);
			if (pdtmPartAvailableStart.Millisecond > 0)
			{
				dtmReturn = dtmReturn.AddSeconds(1);
			}
			return dtmReturn;
		}
			
		/// <summary>
		/// Convert real time to lead time
		/// </summary>
		/// <param name="pdtmCurrentWorkingDay"></param>
		/// <param name="pintWorkCenterID"></param>
		/// <param name="pdtbWCCapacity"></param>
		/// <returns></returns>
		private decimal ConvertToLeadTime(
			DateTime pdtmCurrentWorkingDay, 
			decimal pdecRealTime,
			int pintWorkCenterID, 
			DataTable pdtbWCCapacity)
		{
			decimal decLeadTime = 0;
			DataRow[] arrWCC = pdtbWCCapacity.Select(PRO_WCCapacityTable.WORKCENTERID_FLD + "=" + pintWorkCenterID.ToString() +
				" AND " + PRO_WCCapacityTable.BEGINDATE_FLD + " <= '" + pdtmCurrentWorkingDay +
				"' AND " + PRO_WCCapacityTable.ENDDATE_FLD + " >= '" + pdtmCurrentWorkingDay + "'");
			if (arrWCC.Length != 1)
			{
				//TODO: Extend Work center capacity configuration
				throw new PCSException(0,string.Empty,null);
			}
			decimal decCapacity = decimal.Parse(arrWCC[0][CAPACITY_FLD].ToString());
			decimal decTotalWorkTime = decimal.Parse(arrWCC[0][TOTALWORKTIME_FLD].ToString());
			decLeadTime = (pdecRealTime *  decCapacity)/decTotalWorkTime;
			decLeadTime = decimal.Floor(decLeadTime);
			return decLeadTime;
		}
 
		/// <summary>
		/// Convert lead time to real time
		/// </summary>
		/// <param name="pdtmWorkingDate"></param>
		/// <param name="pdblLeadTime"></param>
		/// <param name="pintWorkCenterID"></param>
		/// <param name="pdtbWCCapacity"></param>
		/// <returns></returns>
		private decimal ConvertToRealTime(
			DateTime pdtmWorkingDate, 
			decimal pdblLeadTime, 
			int pintWorkCenterID, 
			DataTable pdtbWCCapacity)
		{
			decimal decRealTime = 0;
			DataRow[] arrWCC = pdtbWCCapacity.Select(PRO_WCCapacityTable.WORKCENTERID_FLD + "=" + pintWorkCenterID.ToString() +
				" AND " + PRO_WCCapacityTable.BEGINDATE_FLD + " <= '" + pdtmWorkingDate +
				"' AND " + PRO_WCCapacityTable.ENDDATE_FLD + " >= '" + pdtmWorkingDate + "'");
			if (arrWCC.Length != 1)
			{
				//TODO: Exception detail
				throw new PCSException(0,string.Empty,null);
			}
			decimal decCapacity = decimal.Parse(arrWCC[0][CAPACITY_FLD].ToString());
			decimal decTotalWorkTime = decimal.Parse(arrWCC[0][TOTALWORKTIME_FLD].ToString());
			decRealTime = (pdblLeadTime * decTotalWorkTime ) / decCapacity;
			if (decRealTime - decimal.Floor(decRealTime) > 0)
			{
				decRealTime = decimal.Floor(decRealTime) + 1;
			}
			else
			{
				decRealTime = decimal.Floor(decRealTime);
			}
			return decRealTime;
		}

		/// <summary>
		/// find and fill part, return arranged leadtime
		/// </summary>
		/// <param name="pintWorkCenterID"></param>
		/// <param name="pdtmPartAvailableStart"></param>
		/// <returns></returns>
		private decimal FindActualTimeAndFill(DataSet pdstDCPResultMaster, DataSet pdstDCPResultDetail, DataTable pdtbCurrentCapacity, int pintWorkCenterID, int pintCurrentOp, int pintOpType, int pintOpMasterID, DateTime pdtmPartAvailableStart, DataTable pdtbWCCapacity, DataTable pdtbWCCapacityAndShift, DateTime pdtmCurrentWorkingDay, decimal pdecQuantity, DataRow[] pdrowRoutings, DataTable pdtbChangeCategory, DataSet pdstCalendar)
		{
			DateTime dtm1 = DateTime.Now;
			DateTime dtmPartActualStart = DateTime.MinValue;
			DateTime dtmPartActualEnd = DateTime.MinValue;
			DateTime dtmFreeStart = DateTime.MinValue;
			DateTime dtmFreeEnd = DateTime.MinValue;

			decimal decArranged = 0;

			//determine free range
			if (GetAvailCapacityForOneDay(pdtmPartAvailableStart,ref dtmFreeStart,ref dtmFreeEnd,pdtmCurrentWorkingDay,pintWorkCenterID,pdtbWCCapacityAndShift,pdstDCPResultMaster,pdstDCPResultDetail))
			{
				//try to change category
				if (ArrangeChangeCategory(
					ref dtmFreeStart,
					ref dtmFreeEnd,
					pintWorkCenterID,
					string.Empty,
					pintOpMasterID,
					pdstDCPResultMaster,
					pdstDCPResultDetail,
					pdtbChangeCategory,
					pdtbWCCapacityAndShift,
					pdtmCurrentWorkingDay,
					pdstCalendar,DateTime.MinValue,null,null,null,null))
				{
					//calculate free time
					DataRow[] drowShifts = pdtbWCCapacityAndShift.Select(PRO_WCCapacityTable.WORKCENTERID_FLD 
						+ "=" + pintWorkCenterID,PRO_ShiftPatternTable.WORKTIMEFROM_FLD + " ASC");
					decimal decFreeRealTime = 0;//GetRealWorkingTimeInWorkDay(drowShifts,dtmFreeStart,dtmFreeEnd,pdtmCurrentWorkingDay);
					//(decimal)dtmFreeEnd.Subtract(dtmFreeStart).TotalSeconds;
				
					//convert to leadtime
					decimal decFreeLeadTime = ConvertToLeadTime(pdtmCurrentWorkingDay,decFreeRealTime,pintWorkCenterID,pdtbWCCapacity);

					//first , select start-free-time for actual-start-time and actual-end-time
					dtmPartActualStart = dtmFreeStart;
					dtmPartActualEnd = dtmFreeStart;

					//calculate max leadtime can produce at actual-end-time
					//decimal decOpRoutingParam = GetRoutingParam(pdrowRoutings[pintCurrentOp]);
					decimal decPrevOpCompletedPercent = GetPrevOpCompletedPercent(dtmPartActualEnd,pdstDCPResultMaster,pdstDCPResultDetail,pdrowRoutings,pintCurrentOp,pdtbWCCapacityAndShift,pintWorkCenterID);
					decimal decCurrentOpCompletedLeadTime = GetCurrentOpCompletedLeadTime(dtmPartActualEnd,pdstDCPResultMaster,pdstDCPResultDetail,pdrowRoutings,pintCurrentOp,pdtbWCCapacity,pdtbWCCapacityAndShift,pintWorkCenterID);
					decimal decCurrentOpTotalLeadTime = GetOperationTotalLeadTime(pdrowRoutings,pintCurrentOp);

					//calculate max-ready-leadtime base on prev-op completed percent,current completed leadtime, optype and real time free
					decimal decMaxReadyLeadTime = CalculateMaxReadyLeadTime(
						decPrevOpCompletedPercent,
						decCurrentOpCompletedLeadTime,
						decCurrentOpTotalLeadTime,
						decFreeLeadTime,
						pintOpType);
					if (decMaxReadyLeadTime < 0)
					{
						decMaxReadyLeadTime = 0;
					}

					//loop to find real actual endtime, until MaxReadyLeadTime = 0 or Actual-End-Time > Free-End-Time
					while ((dtmPartActualEnd < dtmFreeEnd) && (decMaxReadyLeadTime > 0))
					{
						//convert to real time
						decimal decMaxReadyRealTime = ConvertToRealTime(pdtmCurrentWorkingDay,decMaxReadyLeadTime,pintWorkCenterID,pdtbWCCapacity);
						//recalculate actual end
						decArranged += decMaxReadyLeadTime;

						//dtmPartActualEnd = AddMoreSecondForwardOneDay(drowShifts,dtmPartActualEnd,(double)decMaxReadyRealTime,pdtmCurrentWorkingDay);

						//recalculate completed and ready leadtime
						decPrevOpCompletedPercent = GetPrevOpCompletedPercent(dtmPartActualEnd,pdstDCPResultMaster,pdstDCPResultDetail,pdrowRoutings,pintCurrentOp,pdtbWCCapacityAndShift,pintWorkCenterID);
						decCurrentOpCompletedLeadTime += decMaxReadyLeadTime;
						decFreeLeadTime -= decMaxReadyLeadTime;
						decMaxReadyLeadTime = CalculateMaxReadyLeadTime(
							decPrevOpCompletedPercent,
							decCurrentOpCompletedLeadTime,
							decCurrentOpTotalLeadTime,
							decFreeLeadTime,
							pintOpType);
						if (decMaxReadyLeadTime < 0)
						{
							decMaxReadyLeadTime = 0;
						}
					}

					//decArranged;// = ConvertToLeadTime(pdtmCurrentWorkingDay,GetRealWorkingTimeInWorkDay(drowShifts,dtmPartActualStart,dtmPartActualEnd,pdtmCurrentWorkingDay),pintWorkCenterID,pdtbWCCapacity);

					//fill with actual-start & actual-end
					InsertResultDetail(
						pintWorkCenterID,
						"",
						pdstDCPResultDetail,
						pintOpMasterID,
						dtmPartActualStart,
						dtmPartActualEnd,
						decArranged,
						decCurrentOpTotalLeadTime,
						pdecQuantity,
						(int)DCPResultTypeEnum.Running,pdtmCurrentWorkingDay,
						pdtbWCCapacity);
					//fill current capacity
					DataRow[] arrCurrentCapacity = pdtbCurrentCapacity.Select(DATE_FLD + "='" + pdtmCurrentWorkingDay.Date + "'");
					if (arrCurrentCapacity.Length > 0)
					{
						DataRow drowCurrentCapacity = arrCurrentCapacity[0];
						string strWCCode = GetWorkCenterCodeByID(pdtbWCCapacity,pintWorkCenterID);
						drowCurrentCapacity[strWCCode] = decimal.Parse(drowCurrentCapacity[strWCCode].ToString()) - decArranged;
					}					
				}
			}

			DateTime dtm2 = DateTime.Now;

			return decArranged;
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="pdstDCPResultDetail"></param>
		/// <param name="pintOpMasterID"></param>
		/// <param name="pdtmStartTime"></param>
		/// <param name="pdtmEndTime"></param>
		/// <param name="pdecPartLeadTime"></param>
		/// <param name="pdecTotalLeadTime"></param>
		/// <param name="pdecQuantity"></param>
		/// <param name="pintType"></param>
		private void InsertResultDetail(int pintWorkCenterId, string pstrWorkCenterCode, DataSet pdstDCPResultDetail, int pintOpMasterID, DateTime pdtmStartTime, DateTime pdtmEndTime, decimal pdecPartLeadTime, decimal pdecTotalLeadTime, decimal pdecQuantity, int pintType, DateTime pdtmWorkingDay,DataTable pdtbWCCapacity)
		{
			const decimal ONE_HUNDRED = 100;
			if ((pdecPartLeadTime == 0) && (pdecTotalLeadTime != 0) && (pintType == 0))
			{
				return;
			}
			DataRow drowResultDetail = pdstDCPResultDetail.Tables[pstrWorkCenterCode].NewRow();
			drowResultDetail[PRO_DCPResultDetailTable.DCPRESULTMASTERID_FLD] = pintOpMasterID;
			drowResultDetail[PRO_DCPResultDetailTable.ENDTIME_FLD] = pdtmEndTime;

			decimal decTotalSecond = 0;
			if (pintType != 0) 
			{
				decimal decTotalSecondRealTime = (decimal)(pdtmEndTime.Subtract(pdtmStartTime)).TotalSeconds;
				decTotalSecond = ConvertToLeadTime(pdtmWorkingDay,decTotalSecondRealTime,pintWorkCenterId,pdtbWCCapacity);
			}
			else
			{
				decTotalSecond = pdecPartLeadTime;
			}

			if (pdecTotalLeadTime != 0)
			{
				drowResultDetail[PRO_DCPResultDetailTable.PERCENTAGE_FLD] = ONE_HUNDRED * pdecPartLeadTime / pdecTotalLeadTime > ONE_HUNDRED ? ONE_HUNDRED : (ONE_HUNDRED * pdecPartLeadTime / pdecTotalLeadTime);
				drowResultDetail[PRO_DCPResultDetailTable.QUANTITY_FLD] = (m_blnRoundedQuantity) ? Math.Round(pdecPartLeadTime * pdecQuantity / pdecTotalLeadTime) : pdecPartLeadTime * pdecQuantity / pdecTotalLeadTime;
			}
			else
			{
				drowResultDetail[PRO_DCPResultDetailTable.PERCENTAGE_FLD] = ONE_HUNDRED;
				drowResultDetail[PRO_DCPResultDetailTable.QUANTITY_FLD] = (m_blnRoundedQuantity) ? Math.Round(pdecQuantity) : pdecQuantity;
			}
			
			drowResultDetail[MST_WorkCenterTable.WORKCENTERID_FLD] = pintWorkCenterId;
			drowResultDetail[WORKCENTERCODE_FLD] = pstrWorkCenterCode;
			drowResultDetail[PRO_DCPResultDetailTable.STARTTIME_FLD] = pdtmStartTime;
			drowResultDetail[PRO_DCPResultDetailTable.TOTALSECOND_FLD] = decTotalSecond; //pdecPartLeadTime; //TODO: Calculate DCP Result Detail total second
			drowResultDetail[PRO_DCPResultDetailTable.TYPE_FLD] = pintType;
			drowResultDetail[PRO_DCPResultDetailTable.WORKINGDATE_FLD] = pdtmWorkingDay;
			drowResultDetail[PRO_DCPResultDetailTable.WOCONVERTED_FLD] = 0;
			pdstDCPResultDetail.Tables[pstrWorkCenterCode].Rows.Add(drowResultDetail);
		}

	
		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		/// <summary>
		/// Get working date of given time, by given work center
		/// </summary>
		/// <param name="pdtmWorkingTime"></param>
		/// <param name="pintWorkCenterID"></param>
		/// <param name="pdtbWCCapacityAndShift"></param>
		/// <returns></returns>
		private DateTime GetDateOnlyByWorkCenter(
			DateTime pdtmWorkingTime,
			int pintWorkCenterID,
			DataTable pdtbWCCapacityAndShift)
		{
			DateTime dtmBaseFrom;
			DateTime dtmBaseTo;
			
			DataRow[] arrShifts = pdtbWCCapacityAndShift.Select(PRO_WCCapacityTable.WORKCENTERID_FLD 
				+ "=" + pintWorkCenterID,PRO_ShiftPatternTable.WORKTIMEFROM_FLD + " ASC");
			
			if (arrShifts.Length > 0) 
			{
				dtmBaseFrom = DateTime.Parse(arrShifts[0][PRO_ShiftPatternTable.WORKTIMEFROM_FLD].ToString());
				dtmBaseTo = DateTime.Parse(arrShifts[arrShifts.Length - 1][PRO_ShiftPatternTable.WORKTIMETO_FLD].ToString());
				DateTime dtmWorkingDate = GetDateOnly(pdtmWorkingTime,dtmBaseFrom,dtmBaseTo);
				return dtmWorkingDate;
			}
			return pdtmWorkingTime;
		}

		private DateTime GetDateOnlyByWorkCenter(
			DateTime pdtmWorkingTime,
			string pstrWorkCenterCode,
			DataSet pdstRemainCapacity)
		{
			DataRow[] arrRemainCapacity = pdstRemainCapacity.Tables[pstrWorkCenterCode].Select(WORKINGDAYSTART_FLD + "<='" + pdtmWorkingTime + "'" + " AND " + REMAINCAPACITY_FLD + " <> -1", WORKINGDAY_FLD + " DESC");// AND " + WORKINGDAYEND_FLD + ">'" + pdtmWorkingTime + "'");
			if (arrRemainCapacity.Length >= 1)
			{
				return Convert.ToDateTime(arrRemainCapacity[0][WORKINGDAY_FLD]);
			}
			else
			{
				return pdtmWorkingTime.Date;
			}
		}
		

		/// <summary>
		/// Get previous operation completed percent at given time
		/// </summary>
		/// <returns></returns>
		private decimal GetPrevOpCompletedPercent(
			DateTime pdtmCurrentTime,
			DataSet pdstDCPResultMaster,
			DataSet pdstDCPResultDetail,
			DataRow[] pdrowRoutings,
			int pintCurrentOp,
			DataTable pdtbWCCapacityAndShift,
			int pintWorkCenterID)
		{
			const decimal ONE_HUNDRED = 100;
			if (pintCurrentOp < 1)
			{
				return ONE_HUNDRED;
			}
			//find prev-op master row
			DataRow drowPrevRouting = pdrowRoutings[pintCurrentOp - 1];
			DataRow drowResultMaster = GetPrevOpResultMaster(pdstDCPResultMaster,drowPrevRouting);
			int intResultMasterID = int.Parse(drowResultMaster[PRO_DCPResultMasterTable.DCPRESULTMASTERID_FLD].ToString());
			string strFilter = PRO_DCPResultDetailTable.DCPRESULTMASTERID_FLD + "=" + intResultMasterID.ToString()
				+ " AND " + PRO_DCPResultDetailTable.STARTTIME_FLD + "<='" + pdtmCurrentTime.ToString() + "'";
			//get all detail
			DataRow[] arrResultDetails = pdstDCPResultDetail.Tables[0].Select(strFilter,PRO_DCPResultDetailTable.STARTTIME_FLD);
			if (arrResultDetails.Length == 0)
			{
				return 0;
			}

			decimal decPrevOpLeadTime = GetOperationTotalLeadTime(pdrowRoutings,pintCurrentOp - 1);
			decimal decTempTotalLeadTime = 0;
			int i = 0;
			DateTime dtmPartStart = DateTime.MinValue;
			DateTime dtmPartEnd = DateTime.MinValue;

			DataRow[] drowShifts = pdtbWCCapacityAndShift.Select(PRO_WCCapacityTable.WORKCENTERID_FLD 
				+ "=" + pintWorkCenterID,PRO_ShiftPatternTable.WORKTIMEFROM_FLD + " ASC");
			
			for (i = 0; i <= arrResultDetails.Length - 1; i++)
			{
				DataRow drowResultDetail = arrResultDetails[i];
				dtmPartStart = DateTime.Parse(drowResultDetail[PRO_DCPResultDetailTable.STARTTIME_FLD].ToString());
				dtmPartEnd = DateTime.Parse(drowResultDetail[PRO_DCPResultDetailTable.ENDTIME_FLD].ToString());			
				decTempTotalLeadTime += decimal.Parse(drowResultDetail[PRO_DCPResultDetailTable.TOTALSECOND_FLD].ToString());
			}
			DateTime dtmWorkingDate = GetDateOnlyByWorkCenter(dtmPartStart,pintWorkCenterID,pdtbWCCapacityAndShift);
			decimal decGreaterLeadTime = 0;//GetRealWorkingTimeInWorkDay(drowShifts,pdtmCurrentTime,dtmPartEnd,dtmWorkingDate);
			if (decGreaterLeadTime > 0)
			{
				decTempTotalLeadTime -= decGreaterLeadTime;
			}
			else
			{
				//TODO : Do any thing here ???
			}

			return ONE_HUNDRED * decTempTotalLeadTime / decPrevOpLeadTime;
		}

		
		/// <summary>
		/// Get current operation completed percent at given time
		/// </summary>
		/// <returns></returns>
		private decimal GetCurrentOpCompletedLeadTime(			
			DateTime pdtmCurrentTime,
			DataSet pdtbDCPResultMaster,
			DataSet pdtbDCPResultDetail,
			DataRow[] pdrowRoutings,
			int pintCurrentOp,
			DataTable pdtbWCCapacity,
			DataTable pdtbWCCapacityAndShift,
			int pintWorkCenterID)
		{
			if (pintCurrentOp < 0)
			{
				return 0;
			}
			//find current-op master row
			DataRow drowCurrentRouting = pdrowRoutings[pintCurrentOp];
			DataRow drowResultMaster = GetPrevOpResultMaster(pdtbDCPResultMaster,drowCurrentRouting);
			if (drowResultMaster == null)
			{
				return 0;
			}
			int intResultMasterID = int.Parse(drowResultMaster[PRO_DCPResultMasterTable.DCPRESULTMASTERID_FLD].ToString());
			string strFilter = PRO_DCPResultDetailTable.DCPRESULTMASTERID_FLD + "=" + intResultMasterID.ToString()
				+ " AND " + PRO_DCPResultDetailTable.ENDTIME_FLD + "<='" + pdtmCurrentTime.ToString() + "'"
				+ " AND " + PRO_DCPResultDetailTable.TYPE_FLD + "=" + ((int)DCPResultTypeEnum.Running).ToString();
			//get all detail
			DataRow[] arrResultDetails = pdtbDCPResultDetail.Tables[0].Select(strFilter,PRO_DCPResultDetailTable.STARTTIME_FLD);
			if (arrResultDetails.Length == 0)
			{
				return 0;
			}

			decimal decTempTotalLeadTime = 0;
			int i = 0;
			DateTime dtmPartStart = DateTime.MinValue;
			DateTime dtmPartEnd = DateTime.MinValue;

			DataRow[] drowShifts = pdtbWCCapacityAndShift.Select(PRO_WCCapacityTable.WORKCENTERID_FLD 
				+ "=" + pintWorkCenterID,PRO_ShiftPatternTable.WORKTIMEFROM_FLD + " ASC");
			
			//sum all previous part leadtime
			for (i = 0; i <= arrResultDetails.Length - 1; i++)
			{
				DataRow drowResultDetail = arrResultDetails[i];
				dtmPartStart = DateTime.Parse(drowResultDetail[PRO_DCPResultDetailTable.STARTTIME_FLD].ToString());
				dtmPartEnd = DateTime.Parse(drowResultDetail[PRO_DCPResultDetailTable.ENDTIME_FLD].ToString());

				DateTime dtmCurrentWorkingDay = GetDateOnlyByWorkCenter(dtmPartStart,pintWorkCenterID,pdtbWCCapacityAndShift);

				//calculate effective working time
				decimal decEffectiveTime = 0; //GetRealWorkingTimeInWorkDay(drowShifts,dtmPartStart,dtmPartEnd,dtmCurrentWorkingDay);

				decTempTotalLeadTime += ConvertToLeadTime(dtmCurrentWorkingDay,decEffectiveTime,pintWorkCenterID,pdtbWCCapacity);
			}

			return decTempTotalLeadTime;
		}

	
		/// <summary>
		/// Get total lead time of given operation
		/// </summary>
		/// <returns></returns>
		private decimal GetOperationTotalLeadTime(
			DataRow[] pdrowRoutings, 
			int pintCurrentOp)
		{
			if ((pintCurrentOp < 0) || (pintCurrentOp > pdrowRoutings.Length))
			{
				return 0;
			}
			DataRow drowCurrentRouting = pdrowRoutings[pintCurrentOp];
			return decimal.Parse(drowCurrentRouting[LEADTIME_FLD].ToString());
		}

	
		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		private decimal CalculateMaxReadyLeadTime(decimal pdecPrevOpCompletedPercent, decimal pdecCurrentOpCompletedLeadTime, decimal pdecCurrentOpTotalLeadTime, decimal pdecFreeLeadTime, int pintOpType)
		{
			const decimal ONE_HUNDRED = 100;
			decimal decCurrentOpRemainingLeadTime = pdecCurrentOpTotalLeadTime - pdecCurrentOpCompletedLeadTime;
			switch (pintOpType)
			{
				case LINEAR:
					if (pdecPrevOpCompletedPercent < ONE_HUNDRED)
					{
						return 0;
					}
					else
					{
						if (pdecFreeLeadTime < decCurrentOpRemainingLeadTime)
						{
							return pdecFreeLeadTime;
						}
						else
						{
							return decCurrentOpRemainingLeadTime;
						}
					}
				case PARALLEL:
					if (pdecFreeLeadTime < decCurrentOpRemainingLeadTime)
					{
						return pdecFreeLeadTime;
					}
					else
					{
						return decCurrentOpRemainingLeadTime;
					}
				case OVERLAPPERCENT:
					decimal decCurrentOpCompletedPercent = (pdecCurrentOpCompletedLeadTime/pdecCurrentOpTotalLeadTime)*ONE_HUNDRED;
					decimal decMaxAvail = (pdecPrevOpCompletedPercent - decCurrentOpCompletedPercent) * pdecCurrentOpTotalLeadTime / ONE_HUNDRED;
					decMaxAvail = Convert.ToInt32(decMaxAvail);
					if (decMaxAvail > pdecFreeLeadTime)
					{
						return pdecFreeLeadTime;
					} 
					else
					{
						return decMaxAvail;
					}
				case OVERLAPQTY:
					//convert overlap quantity to percent
					decCurrentOpCompletedPercent = (pdecCurrentOpCompletedLeadTime/pdecCurrentOpTotalLeadTime)*ONE_HUNDRED;
					decMaxAvail = (pdecPrevOpCompletedPercent - decCurrentOpCompletedPercent) * pdecCurrentOpTotalLeadTime / ONE_HUNDRED;
					decMaxAvail = Convert.ToInt32(decMaxAvail);
					if (decMaxAvail > pdecFreeLeadTime)
					{
						return pdecFreeLeadTime;
					}
					else
					{
						return decMaxAvail;
					}
				default:
					break;
			}
			return 0;
		}

	
		/// <summary>
		/// Try to arrange more curren operation
		/// </summary>
		/// <param name="pintOpType"></param>
		/// <param name="pintCurrentOp"></param>
		/// <param name="pintOpMasterID"></param>
		/// <param name="pintCurrentPart"></param>
		/// <param name="pintWorkCenterID"></param>
		/// <param name="pdtmStartTime"></param>
		/// <param name="pdrowRoutings"></param>
		/// <returns>Arranged leadtime</returns>
		private Decimal TryToFillOpPart(	
			int pintOpType,
			int pintCurrentOp, 
			int pintOpMasterID, 
			int pintCurrentPart, 
			int pintWorkCenterID,
			DateTime pdtmStartTime, 
			DataRow[] pdrowRoutings, 
			ref DataSet pdstDCPResultMaster, 
			ref DataSet pdstDCPResultDetail,
			ref DataTable pdtbCurrentCapacity,
			DataTable pdtbWCCapacity,
			DataTable pdtbWCCapacityAndShift,
			DataSet pdstCalendar,
			ref DateTime pdtmCurrentWorkingDay,
			decimal pdecQuantity,
			DataTable pdtbChangeCategory)
		{
			DateTime dtmPartAvailableStart = DateTime.MinValue;

			DataRow drowCurrentRouting = pdrowRoutings[pintCurrentOp];

			//Find available start time for current part
			dtmPartAvailableStart = GetOpPartAvailableStartTime(
				pintOpType,
				pintCurrentOp,
				pintOpMasterID,
				pintCurrentPart,
				pdecQuantity,
				pdtmStartTime,
				pdrowRoutings,
				drowCurrentRouting, pdstDCPResultMaster, pdstDCPResultDetail,
				pdtbWCCapacity,
				pdtbWCCapacityAndShift,
				pintWorkCenterID);

			//correct to sure available start is in working time
			dtmPartAvailableStart = CorrectPartAvailableStart(ref pdtmCurrentWorkingDay,dtmPartAvailableStart,pintWorkCenterID,pdtbWCCapacityAndShift,pdtbCurrentCapacity,pdstCalendar);

			//Find actual start time for current part
			decimal decArranged = FindActualTimeAndFill(pdstDCPResultMaster, pdstDCPResultDetail, pdtbCurrentCapacity,pintWorkCenterID,pintCurrentOp,pintOpType,pintOpMasterID,dtmPartAvailableStart,pdtbWCCapacity,pdtbWCCapacityAndShift,pdtmCurrentWorkingDay,pdecQuantity,pdrowRoutings,pdtbChangeCategory,pdstCalendar);

			return decArranged;
		}
	
	
		/// <summary>
		/// Try with given StartTime, fill results, compare real DueTime to expected DueTime and adjust StartTime for next trying
		/// </summary>
		/// <param name="pdtmStartTime">given StartTime</param>
		/// <param name="pdrowItem">item to processing, maybe CPO or WOLine</param>
		/// <param name="pdrowRoutings">routing tables for given item</param>
		/// <param name="pdtbWCCapacity">work center capacity</param>
		/// <param name="pdtbCurrentCapacity">current used capacity</param>
		/// <returns>start datetime of Routing(1)</returns>		
		public DateTime GetJumpingStep(
			DateTime pdtmStartTime, 
			DataRow pdrowItem, 
			DataRow[] pdrowRoutings, 
			DataTable pdtbWCCapacity, 
			DataTable pdtbWCCapacityAndShift,
			DataSet pdstCalendar,
			DataTable pdtbCurrentCapacity, 
			DataSet pdstDCPResultMaster, 
			DataSet pdstDCPResultDetail,
			int pintDCOptionMasterID,
			DataTable pdtbChangeCategory,
			DataTable pdtbBackupChangeCategoryParts)
		{
			#region Common Declarations and Initial values;

			//Result
			DateTime dtmEarliestStart = DateTime.MinValue;

			//Scanning control index and count
			int intCurrentOp = 0;
			int intRoutingRecsCount = pdrowRoutings.Length;

			#endregion

			#region Preprocessing
			//CPO & WOLine
			bool blnIsWOLine = (pdrowItem[PRO_WorkOrderDetailTable.WORKORDERDETAILID_FLD] != DBNull.Value);

			int intWODetailID = -1;
			try
			{
				intWODetailID = int.Parse(pdrowItem[PRO_WorkOrderDetailTable.WORKORDERDETAILID_FLD].ToString());
			}
			catch
			{
			}

			int intCPOID = -1;
			try
			{
				intCPOID = int.Parse(pdrowItem[CPOID_FLD].ToString());
			}
			catch
			{
			}
			ClearItemDCPResult(ref pdstDCPResultMaster, ref pdstDCPResultDetail, intWODetailID, intCPOID, blnIsWOLine);
			#endregion

			#region Main Processing

			//Start with first routing
			DataRow drowCurrentRouting;
			DateTime dtmLatestOpEnd = DateTime.MinValue;

			//Walk through all routing records, each routing record equivalent to an operation
			while (intCurrentOp < intRoutingRecsCount)
			{
				#region Calculate initial values

				drowCurrentRouting = pdrowRoutings[intCurrentOp];
				DataRow drowOpMaster;
				int intOpMasterId;

				//Informations for current operation
				decimal decOpTotalLeadTime = 0; //Total needed time for operation
				decimal decOpRemainLeadTime = 0; //Total remain time
				decimal decQuantity = 0;
				int intWorkCenterID = -1; //Work center where operation take place

				//Total time = leadtime*totalworktime/capacity
				try
				{
					decOpTotalLeadTime = decimal.Parse(drowCurrentRouting[LEADTIME_FLD].ToString());
					decOpRemainLeadTime = decOpTotalLeadTime;
					decQuantity = decimal.Parse(pdrowItem[QUANTITY_FLD].ToString());
					intWorkCenterID = int.Parse(drowCurrentRouting[PRO_WORoutingTable.WORKCENTERID_FLD].ToString());
				}
				catch (Exception ex)
				{
					ex.ToString();
					//TODO: calculate times fail
				}

				//Master record - one and only one for a routing record
				//TODO: Using InsertResultMaster function
				//InsertResultMaster(pdrowItem,pdrowRoutings,ref pdstCalendar);
				DataRow[] arrExistedResultMasters = pdstDCPResultMaster.Tables[0].Select(string.Empty,PRO_DCPResultMasterTable.DCPRESULTMASTERID_FLD + " DESC");
				int intMaxMasterID = 0;
				if (arrExistedResultMasters.Length > 0)
				{
					intMaxMasterID = int.Parse(arrExistedResultMasters[0][PRO_DCPResultMasterTable.DCPRESULTMASTERID_FLD].ToString());
				}

				drowOpMaster = pdstDCPResultMaster.Tables[0].NewRow();
				drowOpMaster[PRO_DCPResultMasterTable.DCPRESULTMASTERID_FLD] = intMaxMasterID + 1;
				drowOpMaster[PRO_DCPResultMasterTable.WORKCENTERID_FLD] = intWorkCenterID;
				drowOpMaster[PRO_WORoutingTable.WOROUTINGID_FLD] = pdrowRoutings[intCurrentOp][PRO_WORoutingTable.WOROUTINGID_FLD];
				drowOpMaster[ITM_RoutingTable.ROUTINGID_FLD] = pdrowRoutings[intCurrentOp][ITM_RoutingTable.ROUTINGID_FLD];
				drowOpMaster[PRO_DCPResultMasterTable.QUANTITY_FLD] = decQuantity;
				drowOpMaster[PRO_DCPResultMasterTable.PRODUCTID_FLD] = pdrowRoutings[intCurrentOp][PRO_WORoutingTable.PRODUCTID_FLD];
				drowOpMaster[PRO_DCPResultMasterTable.DCOPTIONMASTERID_FLD] = pintDCOptionMasterID;
				drowOpMaster[PRO_DCPResultMasterTable.WORKORDERDETAILID_FLD] = intWODetailID;
				drowOpMaster[PRO_DCPResultMasterTable.CPOID_FLD] = intCPOID;
				drowOpMaster[DETAILID_FLD] = pdrowRoutings[intCurrentOp][DETAILID_FLD];
				pdstDCPResultMaster.Tables[0].Rows.Add(drowOpMaster);
				intOpMasterId = int.Parse(drowOpMaster[PRO_DCPResultMasterTable.DCPRESULTMASTERID_FLD].ToString());

				//Operation type and param
				int intRoutingType = GetRoutingType(drowCurrentRouting);

				#endregion Calculate initial values

				#region Try arrange operation until operation's remaining time is zero
				int intCurrentPart = 0;
				DateTime dtmCurrentWorkingDay = GetDateOnlyByWorkCenter(pdtmStartTime,intWorkCenterID,pdtbWCCapacityAndShift);

				while (decOpRemainLeadTime > 0)
				{
					//Try to arrange for a part of operation, just arrange in a working day, if cannot arrange in a day, move to next day
					int intOpType = GetOperationTypeByRoutingType(intRoutingType, pdrowRoutings, intCurrentOp, decQuantity);					
					decimal decOpArrangedLeadTime = TryToFillOpPart(intOpType,intCurrentOp,intOpMasterId,intCurrentPart,intWorkCenterID,pdtmStartTime,pdrowRoutings,ref pdstDCPResultMaster,ref pdstDCPResultDetail,ref pdtbCurrentCapacity,pdtbWCCapacity,pdtbWCCapacityAndShift,pdstCalendar,ref dtmCurrentWorkingDay,decQuantity,pdtbChangeCategory);
					if (decOpArrangedLeadTime > 0)
					{
						decOpRemainLeadTime -= decOpArrangedLeadTime;
						intCurrentPart++;
					}
					else
					{
						dtmCurrentWorkingDay = dtmCurrentWorkingDay.AddDays(1);
					}
				}

				//Update master information
				drowOpMaster = pdstDCPResultMaster.Tables[0].Select(PRO_DCPResultMasterTable.DCPRESULTMASTERID_FLD + "=" + intOpMasterId.ToString())[0];
				DataRow[] arrOpDetails = pdstDCPResultDetail.Tables[0].Select(PRO_DCPResultDetailTable.DCPRESULTMASTERID_FLD + "=" + intOpMasterId.ToString(),PRO_DCPResultDetailTable.STARTTIME_FLD + " ASC");
				if (arrOpDetails.Length > 0)
				{
					drowOpMaster[PRO_DCPResultMasterTable.STARTDATETIME_FLD] = arrOpDetails[0][PRO_DCPResultDetailTable.STARTTIME_FLD];
					DateTime dtmOpEnd = DateTime.Parse((drowOpMaster[PRO_DCPResultMasterTable.DUEDATETIME_FLD] = arrOpDetails[arrOpDetails.Length - 1][PRO_DCPResultDetailTable.ENDTIME_FLD]).ToString());
					if (dtmOpEnd > dtmLatestOpEnd)
					{
						dtmLatestOpEnd = dtmOpEnd;
					}
				}

				#endregion

				#region Switch to next routing records
				intCurrentOp++;
				#endregion
			}

			#endregion

			#region Post Processing
			DateTime dtmDueTime = DateTime.Parse(pdrowItem[PRO_WorkOrderDetailTable.DUEDATE_FLD].ToString());
			if (dtmDueTime < dtmLatestOpEnd)
			{
				TimeSpan tsDiff = dtmLatestOpEnd.Subtract(dtmDueTime);
				dtmEarliestStart = pdtmStartTime.Subtract(tsDiff);
			}
			else
			{
				dtmEarliestStart = pdtmStartTime;
			}
			return dtmEarliestStart;
			#endregion
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="pdtmStartTime"></param>
		/// <param name="pdtmEndTime"></param>
		/// <returns></returns>
		public decimal GetRealWorkingTimeInWorkDay(
			DataRow[] pdrowShifts,
			DateTime pdtmStartTime, 
			DateTime pdtmEndTime, 
			DateTime pdtmCurrentWorkingDay,
			string pstrWorkCenterCode,
			DataSet pdstStopTime)
		{			
			//add all			
			double dblSeconds = pdtmEndTime.Subtract(pdtmStartTime).TotalSeconds;

			//change shift configured day to working day
			DateTime dtmWorkingDayStart = new DateTime(pdtmCurrentWorkingDay.Year, pdtmCurrentWorkingDay.Month, pdtmCurrentWorkingDay.Day,
				((DateTime)pdrowShifts[0][PRO_ShiftPatternTable.WORKTIMEFROM_FLD]).Hour,
				((DateTime)pdrowShifts[0][PRO_ShiftPatternTable.WORKTIMEFROM_FLD]).Minute,
				((DateTime)pdrowShifts[0][PRO_ShiftPatternTable.WORKTIMEFROM_FLD]).Second);
			
			//Get all stoptime
			//DataTable dtbStopTime = GetStopTime(pdrowShifts);			
			//DataTable dtbStopTime = pdstStopTime.Tables[pstrWorkCenterCode].Copy();
			DataTable dtbStopTime = GetStopTime(pdstStopTime,pdtmCurrentWorkingDay);

			//DataRow[] drowStops = dtbStopTime.Select(string.Empty,FROM_FLD + " ASC");
			
			foreach(DataRow drowStop in dtbStopTime.Rows)
			{
				//change stop time to working day
				DateTime dtmStopFrom = dtmWorkingDayStart.AddSeconds(((DateTime)drowStop[FROM_FLD] 
					- (DateTime)pdrowShifts[0][PRO_ShiftPatternTable.WORKTIMEFROM_FLD]).TotalSeconds); 
				if (dtmStopFrom < dtmWorkingDayStart)
				{
					dtmStopFrom = dtmStopFrom.AddDays(1);
				}
				DateTime dtmStopTo = dtmStopFrom.AddSeconds( ((DateTime)drowStop[TO_FLD] - (DateTime)drowStop[FROM_FLD]).TotalSeconds);
				if (dtmStopTo < dtmWorkingDayStart)
				{
					dtmStopTo = dtmStopTo.AddDays(1);
				}

				//check to add more seconds
				if (dtmStopFrom <= pdtmStartTime)
				{
					if (dtmStopTo <= pdtmStartTime)
					{
						//do nothing	
					}
					else if ((dtmStopTo > pdtmStartTime) && (dtmStopTo <= pdtmEndTime))
					{
						dblSeconds -= (dtmStopTo - pdtmStartTime).TotalSeconds;
					}
					else if (dtmStopTo > pdtmEndTime)
					{
						dblSeconds -= (pdtmEndTime - pdtmStartTime).TotalSeconds;
					}
				}
				else if ((dtmStopFrom > pdtmStartTime) && (dtmStopFrom <= pdtmEndTime))
				{
					if (dtmStopTo <= pdtmEndTime)
					{
						dblSeconds -= (dtmStopTo - dtmStopFrom).TotalSeconds;	
					}
					else
					{
						dblSeconds -= (pdtmEndTime - dtmStopFrom).TotalSeconds;
					}
				}
			}

			if (dblSeconds < 0)
			{
				dblSeconds = 0;
			}
			return (decimal)dblSeconds;
		}


		
		private DateTime AddMoreSecondForwardOneDay(
			DataRow[] pdrowShifts,
			DateTime pdtmStartTime,
			double pdblTotalSecond, 
			DateTime pdtmWorkingDay)
		{
			//add all			
			DateTime dtmEndTime = pdtmStartTime.AddSeconds(pdblTotalSecond);
			
			//change shift configured day to working day
			DateTime dtmFrom = new DateTime(pdtmWorkingDay.Year, pdtmWorkingDay.Month, pdtmWorkingDay.Day,
				((DateTime)pdrowShifts[0][PRO_ShiftPatternTable.WORKTIMEFROM_FLD]).Hour,
				((DateTime)pdrowShifts[0][PRO_ShiftPatternTable.WORKTIMEFROM_FLD]).Minute,
				((DateTime)pdrowShifts[0][PRO_ShiftPatternTable.WORKTIMEFROM_FLD]).Second);
			
			//Get all stoptime
			DataTable dtbStopTime = GetStopTime(pdrowShifts);			
			//DataTable dtbStopTime = pdstStopTime.Tables[pstrWorkCenterCode].Copy();
			//DataTable dtbStopTime = GetStopTime(pdstStopTime,pdtmWorkingDay).Copy();

			//DataRow[] drowStops = dtbStopTime.Select(string.Empty,FROM_FLD + " ASC");
			
			foreach(DataRow drowStop in dtbStopTime.Rows)
			{
				//change stop time to working day
				DateTime dtmStopFrom = dtmFrom.AddSeconds(((DateTime)drowStop[FROM_FLD] 
					- (DateTime)pdrowShifts[0][PRO_ShiftPatternTable.WORKTIMEFROM_FLD]).TotalSeconds); 
				if (dtmStopFrom < dtmFrom)
				{
					dtmStopFrom = dtmStopFrom.AddDays(1);
				}
				DateTime dtmStopTo = dtmStopFrom.AddSeconds( ((DateTime)drowStop[TO_FLD] - (DateTime)drowStop[FROM_FLD]).TotalSeconds);
				if (dtmStopTo < dtmFrom)
				{
					dtmStopTo = dtmStopTo.AddDays(1);
				}

				//check to add more seconds
				if (dtmStopFrom < pdtmStartTime)
				{
					if (dtmStopTo < pdtmStartTime)
					{
						//do nothing	
					}
					else if ((dtmStopTo > pdtmStartTime) && (dtmStopTo < dtmEndTime))
					{
						dtmEndTime = dtmEndTime.AddSeconds((dtmStopTo - pdtmStartTime).TotalSeconds);
					}
					else if (dtmStopTo > dtmEndTime)
					{
						dtmEndTime = dtmEndTime.AddSeconds((dtmStopTo - pdtmStartTime).TotalSeconds);						
					}
				}
				else if ((dtmStopFrom > pdtmStartTime) && (dtmStopFrom < dtmEndTime))
				{
					if (dtmStopTo <= dtmEndTime)
					{
						dtmEndTime = dtmEndTime.AddSeconds((dtmStopTo - dtmStopFrom).TotalSeconds);	
					}
					else
					{
						dtmEndTime = dtmEndTime.AddSeconds((dtmStopTo - dtmStopFrom).TotalSeconds);												
					}
				}
			}
			return dtmEndTime;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="pdrowShifts"></param>
		/// <param name="pdtmStartTime"></param>
		/// <param name="pdblTotalSecond"></param>
		/// <param name="pdtmWorkingDay"></param>
		/// <returns></returns>
		private DateTime AddMoreSecondForwardOneDay(
			DataRow[] pdrowShifts,
			DateTime pdtmStartTime,
			double pdblTotalSecond, 
			DateTime pdtmWorkingDay,
			string pstrWorkCenterCode,
			DataSet pdstStopTime)
		{
			//add all			
			DateTime dtmEndTime = pdtmStartTime.AddSeconds(pdblTotalSecond);
			
			//change shift configured day to working day
			DateTime dtmFrom = new DateTime(pdtmWorkingDay.Year, pdtmWorkingDay.Month, pdtmWorkingDay.Day,
				((DateTime)pdrowShifts[0][PRO_ShiftPatternTable.WORKTIMEFROM_FLD]).Hour,
				((DateTime)pdrowShifts[0][PRO_ShiftPatternTable.WORKTIMEFROM_FLD]).Minute,
				((DateTime)pdrowShifts[0][PRO_ShiftPatternTable.WORKTIMEFROM_FLD]).Second);
			
			//Get all stoptime
			//DataTable dtbStopTime = GetStopTime(pdrowShifts);			
			//DataTable dtbStopTime = pdstStopTime.Tables[pstrWorkCenterCode].Copy();
			DataTable dtbStopTime = GetStopTime(pdstStopTime,pdtmWorkingDay).Copy();

			//DataRow[] drowStops = dtbStopTime.Select(string.Empty,FROM_FLD + " ASC");
			
			foreach(DataRow drowStop in dtbStopTime.Rows)
			{
				//change stop time to working day
				DateTime dtmStopFrom = dtmFrom.AddSeconds(((DateTime)drowStop[FROM_FLD] 
					- (DateTime)pdrowShifts[0][PRO_ShiftPatternTable.WORKTIMEFROM_FLD]).TotalSeconds); 
				if (dtmStopFrom < dtmFrom)
				{
					dtmStopFrom = dtmStopFrom.AddDays(1);
				}
				DateTime dtmStopTo = dtmStopFrom.AddSeconds( ((DateTime)drowStop[TO_FLD] - (DateTime)drowStop[FROM_FLD]).TotalSeconds);
				if (dtmStopTo < dtmFrom)
				{
					dtmStopTo = dtmStopTo.AddDays(1);
				}

				//check to add more seconds
				if (dtmStopFrom < pdtmStartTime)
				{
					if (dtmStopTo < pdtmStartTime)
					{
						//do nothing	
					}
					else if ((dtmStopTo > pdtmStartTime) && (dtmStopTo < dtmEndTime))
					{
						dtmEndTime = dtmEndTime.AddSeconds((dtmStopTo - pdtmStartTime).TotalSeconds);
					}
					else if (dtmStopTo > dtmEndTime)
					{
						dtmEndTime = dtmEndTime.AddSeconds((dtmStopTo - pdtmStartTime).TotalSeconds);						
					}
				}
				else if ((dtmStopFrom > pdtmStartTime) && (dtmStopFrom < dtmEndTime))
				{
					if (dtmStopTo <= dtmEndTime)
					{
						dtmEndTime = dtmEndTime.AddSeconds((dtmStopTo - dtmStopFrom).TotalSeconds);	
					}
					else
					{
						dtmEndTime = dtmEndTime.AddSeconds((dtmStopTo - dtmStopFrom).TotalSeconds);												
					}
				}
			}
			return dtmEndTime;
		}

		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="pdtmEndTime"></param>
		/// <param name="pdblTotalSeconds"></param>
		/// <param name="pdtmInitialWorkingDay"></param>
		/// <param name="pintWorkCenterID"></param>
		/// <param name="pdtbWCCapacityAndShift"></param>
		/// <param name="pdstCalendar"></param>
		/// <returns></returns>
		private DateTime AddMoreSecondBackwardMultiDay(
			DateTime pdtmEndTime,
			double pdblTotalSeconds, 
			DateTime pdtmInitialWorkingDay,
			int pintWorkCenterID,
			DataTable pdtbWCCapacityAndShift,
			DataSet pdstCalendar,
			DateTime pdtmAsOfDate,
			string pstrWorkCenterCode,
			DataSet pdstStopTime)
		{
			DateTime dtmTempStartTime = pdtmEndTime;
			DateTime dtmTempEndTime = pdtmEndTime;
			DateTime dtmCurrentWorkingDay = pdtmInitialWorkingDay;
			double dblRemainingSeconds = pdblTotalSeconds;

			DataRow[] drowShifts = pdtbWCCapacityAndShift.Select(PRO_WCCapacityTable.WORKCENTERID_FLD 
				+ "=" + pintWorkCenterID,PRO_ShiftPatternTable.WORKTIMEFROM_FLD + " ASC");

			while (dblRemainingSeconds > EXCEPTED_TIME)
			{
				//change shift configured day to working day
				DateTime dtmWorkingDayStart = DateTime.MinValue;
				DateTime dtmWorkingDayEnd = DateTime.MinValue;

				GetWorkingDayStartAndEndTime(dtmCurrentWorkingDay,ref dtmWorkingDayStart,ref dtmWorkingDayEnd,pdtbWCCapacityAndShift,pintWorkCenterID);
			
				if (dtmTempEndTime > dtmWorkingDayEnd)
				{
					dtmTempEndTime = dtmWorkingDayEnd;
				}

				if (dtmTempStartTime < dtmWorkingDayStart)
				{
					dtmTempStartTime = dtmWorkingDayStart;
				}

				//subtract all
				dtmTempStartTime = dtmTempEndTime.Subtract(new TimeSpan(0,0,0,(int)dblRemainingSeconds,0));
				if (dtmTempStartTime < dtmWorkingDayStart)
				{
					dtmTempStartTime = dtmWorkingDayStart;
				}
			
				double dblSubtractedSeconds = (double)GetRealWorkingTimeInWorkDay(drowShifts,dtmTempStartTime,dtmTempEndTime,dtmCurrentWorkingDay,pstrWorkCenterCode,pdstStopTime);

				dblRemainingSeconds -= dblSubtractedSeconds;
				if (dtmTempStartTime <= dtmWorkingDayStart)
				{
					dtmCurrentWorkingDay = GetPreviousWorkingDay(dtmCurrentWorkingDay,pdstCalendar);
					if (dtmCurrentWorkingDay < pdtmAsOfDate)
					{
						return pdtmAsOfDate;
					}
				}
				dtmTempEndTime = dtmTempStartTime;
			}
			return dtmTempStartTime;
		}

		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="pdtmStartTime"></param>
		/// <param name="pdblTotalSeconds"></param>
		/// <param name="pdtmInitialWorkingDay"></param>
		/// <param name="pintWorkCenterID"></param>
		/// <param name="pdtbWCCapacityAndShift"></param>
		/// <param name="pdstCalendar"></param>
		/// <returns></returns>
		private DateTime AddMoreSecondForwardMultiDay(
			DateTime pdtmStartTime,
			double pdblTotalSeconds, 
			DateTime pdtmInitialWorkingDay,
			int pintWorkCenterID,
			DataTable pdtbWCCapacityAndShift,
			DataSet pdstCalendar,
			string pstrWorkCenterCode,
			DataSet pdstStopTime)
		{
			DateTime dtmTempEndTime = pdtmStartTime;
			DateTime dtmTempStartTime = pdtmStartTime;
			DateTime dtmCurrentWorkingDay = pdtmInitialWorkingDay;
			double dblRemainingSeconds = pdblTotalSeconds;

			DataRow[] drowShifts = pdtbWCCapacityAndShift.Select(PRO_WCCapacityTable.WORKCENTERID_FLD 
				+ "=" + pintWorkCenterID,PRO_ShiftPatternTable.WORKTIMEFROM_FLD + " ASC");

			while (dblRemainingSeconds > EXCEPTED_TIME)
			{
				//subtract all
				dtmTempEndTime = dtmTempStartTime.AddSeconds(dblRemainingSeconds);
			
				//change shift configured day to working day
				DateTime dtmWorkingDayStart = new DateTime(dtmCurrentWorkingDay.Year, dtmCurrentWorkingDay.Month, dtmCurrentWorkingDay.Day,
					((DateTime)drowShifts[0][PRO_ShiftPatternTable.WORKTIMEFROM_FLD]).Hour,
					((DateTime)drowShifts[0][PRO_ShiftPatternTable.WORKTIMEFROM_FLD]).Minute,
					((DateTime)drowShifts[0][PRO_ShiftPatternTable.WORKTIMEFROM_FLD]).Second);
				DateTime dtmWorkingDayEnd = new DateTime(dtmCurrentWorkingDay.Year, dtmCurrentWorkingDay.Month, dtmCurrentWorkingDay.Day,
					((DateTime)drowShifts[drowShifts.Length - 1][PRO_ShiftPatternTable.WORKTIMETO_FLD]).Hour,
					((DateTime)drowShifts[drowShifts.Length - 1][PRO_ShiftPatternTable.WORKTIMETO_FLD]).Minute,
					((DateTime)drowShifts[drowShifts.Length - 1][PRO_ShiftPatternTable.WORKTIMETO_FLD]).Second);
				double dblDiff = ((DateTime)drowShifts[drowShifts.Length - 1][PRO_ShiftPatternTable.WORKTIMETO_FLD]).Subtract((DateTime)drowShifts[0][PRO_ShiftPatternTable.WORKTIMEFROM_FLD]).Days;
				dtmWorkingDayEnd = dtmWorkingDayEnd.AddDays(dblDiff);
			
				if (dtmTempEndTime > dtmWorkingDayEnd)
				{
					dtmTempEndTime = dtmWorkingDayEnd;
				}

				if (dtmTempStartTime < dtmWorkingDayStart)
				{
					dtmTempStartTime = dtmWorkingDayStart;
				}

				double dblAddedSeconds = (double)GetRealWorkingTimeInWorkDay(drowShifts,dtmTempStartTime,dtmTempEndTime,dtmCurrentWorkingDay,pstrWorkCenterCode,pdstStopTime);
				dblRemainingSeconds -= dblAddedSeconds;

				//				if (dblAddedSeconds > 0)
				//				{
				//					dblRemainingSeconds -= dblAddedSeconds;
				//				}
				//				else
				//				{
				//					dtmCurrentWorkingDay = dtmCurrentWorkingDay.AddDays(1);
				//					while (!IsWorkingTime(dtmCurrentWorkingDay,pintWorkCenterID,pdtbWCCapacityAndShift,pdstCalendar))
				//					{
				//						dtmCurrentWorkingDay = dtmCurrentWorkingDay.AddDays(1);
				//					}
				//				}
				//				dtmTempStartTime = dtmTempEndTime;
				if (dtmTempEndTime >= dtmWorkingDayEnd)
				{
					dtmCurrentWorkingDay = GetNextWorkingDay(dtmCurrentWorkingDay,pdstCalendar);
					DateTime pdtmEndOfCycle = new DateTime(2006,1,1,0,0,0,0);
					if (dtmCurrentWorkingDay > pdtmEndOfCycle)
					{
						return pdtmEndOfCycle;
					}
				}
				dtmTempStartTime = dtmTempEndTime;
			}
			return dtmTempEndTime;
		}


		/// <summary>
		/// Try arrange change category time
		/// </summary>
		/// <param name="pdtmFreeTimeStart"></param>
		/// <param name="pdtmFreeTimeEnd"></param>
		/// <param name="pintWorkCenterID"></param>
		/// <param name="pintOpMasterID"></param>
		/// <param name="pdstDCPResultMaster"></param>
		/// <param name="pdstDCPResultDetail"></param>
		/// <returns></returns>
		public bool ArrangeChangeCategory(
			ref DateTime pdtmFreeTimeStart, 
			ref DateTime pdtmFreeTimeEnd, 
			int pintWorkCenterID, 
			string pstrWorkCenterCode,
			int pintOpMasterID, 
			DataSet pdstDCPResultMaster, 
			DataSet pdstDCPResultDetail,
			DataTable pdtbChangeCategory,
			DataTable pdtbWCCapacityAndShift,
			DateTime pdtmCurrentWorkingDay,
			DataSet pdstCalendar,
			DateTime pdtmAsOfDate,
			DataSet pdstRemainCapacity,
			DataSet pdstStopTime,
			DataSet pdstFreeTime,
			DataTable pdtbWCCapacity)
		{
			#region initialize local variables
			int intCurrentProductID = 0;
			int intPrevProductID = 0;
			int intNextProductID = 0;
			int intNextPartOpMasterID = 0;
			DateTime dtmPrevPartEnd = DateTime.MinValue;
			DateTime dtmNextPartStart = DateTime.MinValue;
			DateTime dtmStartChangeCategoryBefore = DateTime.MinValue;
			DateTime dtmEndChangeCategoryBefore = DateTime.MinValue;
			DateTime dtmStartChangeCategoryAfter = DateTime.MinValue;
			DateTime dtmEndChangeCategoryAfter = DateTime.MinValue;
			decimal decChangeCategoryBeforeTime = 0;
			decimal decChangeCategoryAfterTime = 0;

			//find out current product id base on current master result record
			DataRow[] arrResultMasters = pdstDCPResultMaster.Tables[pstrWorkCenterCode].Select(PRO_DCPResultMasterTable.DCPRESULTMASTERID_FLD + "=" + pintOpMasterID.ToString());
			if (arrResultMasters.Length != 1)
			{
				return false;
			}
			intCurrentProductID = int.Parse(arrResultMasters[0][PRO_DCPResultMasterTable.PRODUCTID_FLD].ToString());

			//find out previous product id
			DataRow[] arrResultDetails = pdstDCPResultDetail.Tables[pstrWorkCenterCode].Select(
				PRO_DCPResultDetailTable.ENDTIME_FLD + " <= '" + pdtmFreeTimeStart + "'"
				+ " AND " + PRO_DCPResultDetailTable.TYPE_FLD + " = " + ((int)DCPResultTypeEnum.Running).ToString(),
				PRO_DCPResultDetailTable.ENDTIME_FLD + " DESC");

			if (arrResultDetails.Length > 0)
			{
				dtmPrevPartEnd = DateTime.Parse(arrResultDetails[0][PRO_DCPResultDetailTable.ENDTIME_FLD].ToString());
				arrResultMasters = pdstDCPResultMaster.Tables[pstrWorkCenterCode].Select(PRO_DCPResultMasterTable.DCPRESULTMASTERID_FLD + "=" + arrResultDetails[0][PRO_DCPResultDetailTable.DCPRESULTMASTERID_FLD].ToString());
				intPrevProductID = int.Parse(arrResultMasters[0][PRO_DCPResultMasterTable.PRODUCTID_FLD].ToString());
			}
		
			//find out next product id
			arrResultDetails = pdstDCPResultDetail.Tables[pstrWorkCenterCode].Select(
				PRO_DCPResultDetailTable.STARTTIME_FLD + " >= '" + pdtmFreeTimeEnd + "'"
				+ " AND " + PRO_DCPResultDetailTable.TYPE_FLD + " = " + ((int)DCPResultTypeEnum.Running).ToString(),
				PRO_DCPResultDetailTable.ENDTIME_FLD + " ASC");
			if (arrResultDetails.Length > 0)
			{
				dtmNextPartStart = DateTime.Parse(arrResultDetails[0][PRO_DCPResultDetailTable.STARTTIME_FLD].ToString());
				intNextPartOpMasterID = Convert.ToInt32(arrResultDetails[0][PRO_DCPResultDetailTable.DCPRESULTMASTERID_FLD].ToString());
				arrResultMasters = pdstDCPResultMaster.Tables[pstrWorkCenterCode].Select(PRO_DCPResultMasterTable.DCPRESULTMASTERID_FLD + "=" + arrResultDetails[0][PRO_DCPResultDetailTable.DCPRESULTMASTERID_FLD].ToString());
				if (arrResultMasters.Length == 0)
				{
					arrResultMasters.Length.ToString();
				}
				intNextProductID = int.Parse(arrResultMasters[0][PRO_DCPResultMasterTable.PRODUCTID_FLD].ToString());
			}
		
			#endregion

			#region process "before" change category
			//clear "before" change category if exist
			arrResultDetails = pdstDCPResultDetail.Tables[pstrWorkCenterCode].Select(
				PRO_DCPResultDetailTable.ENDTIME_FLD + " <= '" + pdtmFreeTimeStart + "'"
				+ " AND " + PRO_DCPResultDetailTable.STARTTIME_FLD + " >= '" + dtmPrevPartEnd + "'"
				+ " AND " + PRO_DCPResultDetailTable.TYPE_FLD + " = " + ((int)DCPResultTypeEnum.ChangeCategory).ToString(),
				PRO_DCPResultDetailTable.ENDTIME_FLD + " ASC");
			
			int intIdx = 0;
			foreach (DataRow drowResultDetail in arrResultDetails)
			{
				//adjust freetime				
				if (intIdx != arrResultDetails.Length - 1)
				{
					DataRow drowFreeTime = pdstFreeTime.Tables[pstrWorkCenterCode].NewRow();
					drowFreeTime[STARTFREETIME_FLD] = drowResultDetail[PRO_DCPResultDetailTable.STARTTIME_FLD];
					drowFreeTime[ENDFREETIME_FLD] = drowResultDetail[PRO_DCPResultDetailTable.ENDTIME_FLD];

					pdstFreeTime.Tables[pstrWorkCenterCode].Rows.Add(drowFreeTime);
					drowResultDetail.Delete();
				}
				else
				{
					try
					{
						DataRow drowFreeTime = pdstFreeTime.Tables[pstrWorkCenterCode].Select(STARTFREETIME_FLD + "='" + pdtmFreeTimeStart + "'")[0];
						drowFreeTime[STARTFREETIME_FLD] = drowResultDetail[PRO_DCPResultDetailTable.STARTTIME_FLD];
						pdtmFreeTimeStart = Convert.ToDateTime(drowFreeTime[STARTFREETIME_FLD]);
						drowResultDetail.Delete();
					}
					catch {}
				}
				intIdx ++;
			}

			if (dtmPrevPartEnd != DateTime.MinValue)
			{
				//determine change category time
				DataRow[] arrChangeCategoryTimes = pdtbChangeCategory.Select(PRO_ChangeCategoryMatrixTable.SOURCEPRODUCTID_FLD + "=" + intPrevProductID.ToString() 
					+ " AND " + PRO_ChangeCategoryMatrixTable.DESTPRODUCTID_FLD + "=" + intCurrentProductID.ToString()
					+ " AND " + PRO_ChangeCategoryMasterTable.WORKCENTERID_FLD + "=" + pintWorkCenterID.ToString());
				if (arrChangeCategoryTimes.Length > 0)
				{
					decChangeCategoryBeforeTime = decimal.Parse(arrChangeCategoryTimes[0][PRO_ChangeCategoryMatrixTable.CHANGETIME_FLD].ToString());
				}
				
				if (decChangeCategoryBeforeTime > 0)
				{
					DateTime dtmInitialWorkingDay = GetDateOnlyByWorkCenter(pdtmFreeTimeStart,pstrWorkCenterCode,pdstRemainCapacity);
					//GetDateOnlyByWorkCenter(pdtmFreeTimeStart,pintWorkCenterID,pdtbWCCapacityAndShift);
					//dtmStartChangeCategoryBefore = AddMoreSecondBackwardMultiDay(pdtmFreeTimeStart,(double)decChangeCategoryBeforeTime,pdtmCurrentWorkingDay,pintWorkCenterID,pdtbWCCapacityAndShift,pdstCalendar,pdtmAsOfDate,pstrWorkCenterCode,pdstStopTime);
					//if fail, try to put change category time forward and push FreeTimeStart to the right
					//if (dtmStartChangeCategoryBefore < dtmPrevPartEnd)
					//{
					//dtmStartChangeCategoryBefore = dtmPrevPartEnd;
					dtmStartChangeCategoryBefore = pdtmFreeTimeStart;
					dtmInitialWorkingDay = GetDateOnlyByWorkCenter(dtmStartChangeCategoryBefore,pstrWorkCenterCode,pdstRemainCapacity);
					//GetDateOnlyByWorkCenter(dtmStartChangeCategoryBefore,pintWorkCenterID,pdtbWCCapacityAndShift);
					dtmEndChangeCategoryBefore = AddMoreSecondForwardMultiDay(dtmStartChangeCategoryBefore,(double)decChangeCategoryBeforeTime,dtmInitialWorkingDay,pintWorkCenterID,pdtbWCCapacityAndShift,pdstCalendar,pstrWorkCenterCode,pdstStopTime);
					pdtmFreeTimeStart = dtmEndChangeCategoryBefore;
					//}	
					//else
					//{
					//dtmEndChangeCategoryBefore = pdtmFreeTimeStart;
					//}
				}
			}

			#endregion
				
			#region process "after" change category
			//clear "after" change category if exist
			arrResultDetails = pdstDCPResultDetail.Tables[pstrWorkCenterCode].Select(
				PRO_DCPResultDetailTable.STARTTIME_FLD + " >= '" + pdtmFreeTimeEnd + "'"
				+ " AND " + PRO_DCPResultDetailTable.ENDTIME_FLD + " <= '" + dtmNextPartStart + "'" 
				+ " AND " + PRO_DCPResultDetailTable.TYPE_FLD + " = " + ((int)DCPResultTypeEnum.ChangeCategory).ToString(),
				PRO_DCPResultDetailTable.ENDTIME_FLD + " DESC");
			intIdx = 0;
			foreach (DataRow drowResultDetail in arrResultDetails)
			{
				//adjust freetime				
				if (intIdx != arrResultDetails.Length - 1)
				{
					DataRow drowFreeTime = pdstFreeTime.Tables[pstrWorkCenterCode].NewRow();
					drowFreeTime[STARTFREETIME_FLD] = drowResultDetail[PRO_DCPResultDetailTable.STARTTIME_FLD];
					drowFreeTime[ENDFREETIME_FLD] = drowResultDetail[PRO_DCPResultDetailTable.ENDTIME_FLD];

					pdstFreeTime.Tables[pstrWorkCenterCode].Rows.Add(drowFreeTime);
					drowResultDetail.Delete();
				}
				else
				{
					try
					{
						DataRow drowFreeTime = pdstFreeTime.Tables[pstrWorkCenterCode].Select(ENDFREETIME_FLD + "='" + pdtmFreeTimeEnd + "'")[0];
						drowFreeTime[ENDFREETIME_FLD] = drowResultDetail[PRO_DCPResultDetailTable.ENDTIME_FLD];
						pdtmFreeTimeEnd = Convert.ToDateTime(drowFreeTime[ENDFREETIME_FLD]);
						drowResultDetail.Delete();
					}
					catch
					{				
					}
				}
				intIdx ++;
			}

			if (dtmNextPartStart != DateTime.MinValue)
			{
				//it has part take place after current part, need to change category
				DataRow[] arrChangeCategoryTimes = pdtbChangeCategory.Select(PRO_ChangeCategoryMatrixTable.SOURCEPRODUCTID_FLD + "=" + intCurrentProductID.ToString() 
					+ " AND " + PRO_ChangeCategoryMatrixTable.DESTPRODUCTID_FLD + "=" + intNextProductID.ToString()
					+ " AND " + PRO_ChangeCategoryMasterTable.WORKCENTERID_FLD + "=" + pintWorkCenterID.ToString());
				if (arrChangeCategoryTimes.Length > 0)
				{
					decChangeCategoryAfterTime = decimal.Parse(arrChangeCategoryTimes[0][PRO_ChangeCategoryMatrixTable.CHANGETIME_FLD].ToString());
				}
				//dtmEndChangeCategoryAfter = dtmNextPartStart;

				if (decChangeCategoryAfterTime > 0)
				{
					dtmEndChangeCategoryAfter = pdtmFreeTimeEnd;
					DateTime dtmInitialWorkingDay = GetDateOnlyByWorkCenter(dtmEndChangeCategoryAfter,pstrWorkCenterCode,pdstRemainCapacity);
					//GetDateOnlyByWorkCenter(dtmEndChangeCategoryAfter,pintWorkCenterID,pdtbWCCapacityAndShift);
					dtmStartChangeCategoryAfter = AddMoreSecondBackwardMultiDay(dtmEndChangeCategoryAfter,(double)decChangeCategoryAfterTime,dtmInitialWorkingDay,pintWorkCenterID,pdtbWCCapacityAndShift,pdstCalendar,pdtmAsOfDate,pstrWorkCenterCode,pdstStopTime);
					if (dtmStartChangeCategoryAfter < pdtmFreeTimeEnd)
					{
						pdtmFreeTimeEnd = dtmStartChangeCategoryAfter;
					}										
				}
			}

			#endregion

			#region post process

			//check against effective factor
			decimal decProduceTime = (decimal)pdtmFreeTimeEnd.Subtract(pdtmFreeTimeStart).TotalSeconds;
			decimal decChangeCategoryTime = decChangeCategoryAfterTime + decChangeCategoryBeforeTime;
			
			if ((decProduceTime > (decimal)EXCEPTED_TIME) && (decChangeCategoryTime/decProduceTime > 1))
			{
				return false;
			}

			if (pdtmFreeTimeStart.AddSeconds(EXCEPTED_TIME) < pdtmFreeTimeEnd)
			{
				//fill change category time
				if (decChangeCategoryBeforeTime > 0)
				{
					FillChangeCategoryTime(pdstDCPResultDetail,dtmStartChangeCategoryBefore,dtmEndChangeCategoryBefore,pintOpMasterID,pintWorkCenterID,pstrWorkCenterCode,pdtbWCCapacityAndShift,pdstCalendar,pdstRemainCapacity,pdstFreeTime,pdstStopTime,pdtbWCCapacity);
				}
				if (decChangeCategoryAfterTime > 0)
				{
					FillChangeCategoryTime(pdstDCPResultDetail,dtmStartChangeCategoryAfter,dtmEndChangeCategoryAfter,intNextPartOpMasterID,pintWorkCenterID,pstrWorkCenterCode,pdtbWCCapacityAndShift,pdstCalendar,pdstRemainCapacity,pdstFreeTime,pdstStopTime,pdtbWCCapacity);
				}
				return true;
			}
			else
			{
				return false;
			}

			#endregion
		}

	
		/// <summary>
		/// 
		/// </summary>
		/// <param name="pdstDCPResultDetail"></param>
		/// <param name="pdtmStartTime"></param>
		/// <param name="pdtmEndTime"></param>
		/// <param name="pintOpMasterID"></param>
		/// <param name="pintWorkCenterID"></param>
		/// <param name="pdtbWCCapacityAndShift"></param>
		/// <param name="pdstCalendar"></param>
		private void FillChangeCategoryTime(
			DataSet pdstDCPResultDetail, 
			DateTime pdtmStartTime, 
			DateTime pdtmEndTime, 
			int pintOpMasterID, 
			int pintWorkCenterID, 
			string pstrWorkCenterCode,
			DataTable pdtbWCCapacityAndShift, 
			DataSet pdstCalendar,
			DataSet pdstRemainCapacity,
			DataSet pdstFreeTime,
			DataSet pdstStopTime,
			DataTable pdtbWCCapacity)
		{
			DateTime dtmCurrentWorkingDay = DateTime.MinValue;
			DateTime dtmWorkingDayStart = DateTime.MinValue;
			DateTime dtmWorkingDayEnd = DateTime.MinValue;

			dtmCurrentWorkingDay = GetDateOnlyByWorkCenter(pdtmStartTime,pstrWorkCenterCode,pdstRemainCapacity);
			//GetDateOnlyByWorkCenter(pdtmStartTime,pintWorkCenterID,pdtbWCCapacityAndShift);

			//GetWorkingDayStartAndEndTime(dtmCurrentWorkingDay,ref dtmWorkingDayStart,ref dtmWorkingDayEnd,pdtbWCCapacityAndShift,pintWorkCenterID);
			GetWorkingDayStartAndEndTime(dtmCurrentWorkingDay,ref dtmWorkingDayStart,ref dtmWorkingDayEnd,pdstRemainCapacity,pstrWorkCenterCode);

			DataRow[] arrFreeTime;
			DataRow drowFreeTimeBefore;
			DateTime dtmFreeTimeBeforeStart;
			DataRow drowFreeTimeAfter;
			DateTime dtmFreeTimeAfterEnd;

			while (dtmWorkingDayEnd < pdtmEndTime)			
			{
				if (pdtmStartTime == pdtmEndTime)
				{
					break;
				}
				InsertResultDetail(pintWorkCenterID,pstrWorkCenterCode,pdstDCPResultDetail,pintOpMasterID,pdtmStartTime,dtmWorkingDayEnd,0,1,0,(int)DCPResultTypeEnum.ChangeCategory,dtmCurrentWorkingDay,pdtbWCCapacity);

				//adjust freetime
				arrFreeTime = pdstFreeTime.Tables[pstrWorkCenterCode].Select(
					STARTFREETIME_FLD + " <= '" + pdtmStartTime + "' AND " +
					ENDFREETIME_FLD + " >= '" + pdtmEndTime + "'");
			
				if (arrFreeTime.Length != 1)
				{
					FileStream objFileStream = new FileStream(@"C:\debug",FileMode.OpenOrCreate,FileAccess.Write);
					foreach (DataRow dr in pdstDCPResultDetail.Tables[pstrWorkCenterCode].Rows)
					{
						byte[] arr = System.Text.Encoding.ASCII.GetBytes(dr["StartTime"].ToString() + "->" + dr["EndTime"].ToString() + "(" + dr["Type"] + ")\r\n");
						objFileStream.Write(arr,0,arr.Length);
					}
					foreach (DataRow dr in pdstFreeTime.Tables[pstrWorkCenterCode].Rows)
					{
						byte[] arr = System.Text.Encoding.ASCII.GetBytes(dr["StartTime"].ToString() + "->" + dr["EndTime"].ToString() + "\r\n");
						objFileStream.Write(arr,0,arr.Length);
					}
					objFileStream.Close();
				}

				//add 2 new freetimes
				drowFreeTimeBefore = pdstFreeTime.Tables[pstrWorkCenterCode].NewRow();
				dtmFreeTimeBeforeStart = Convert.ToDateTime(arrFreeTime[0][STARTFREETIME_FLD]);
				NormalizeFreeTime(ref dtmFreeTimeBeforeStart,ref pdtmStartTime,pintWorkCenterID,pdtbWCCapacityAndShift,pstrWorkCenterCode,pdstRemainCapacity,pdstStopTime);
				if (dtmFreeTimeBeforeStart.AddSeconds(EXCEPTED_TIME) < pdtmStartTime)
				{
					drowFreeTimeBefore[STARTFREETIME_FLD] = dtmFreeTimeBeforeStart; 
					drowFreeTimeBefore[ENDFREETIME_FLD] = pdtmStartTime;
					pdstFreeTime.Tables[pstrWorkCenterCode].Rows.Add(drowFreeTimeBefore);
				}

				drowFreeTimeAfter = pdstFreeTime.Tables[pstrWorkCenterCode].NewRow();
				dtmFreeTimeAfterEnd = Convert.ToDateTime(arrFreeTime[0][ENDFREETIME_FLD]);
				NormalizeFreeTime(ref pdtmEndTime,ref dtmFreeTimeAfterEnd,pintWorkCenterID,pdtbWCCapacityAndShift,pstrWorkCenterCode,pdstRemainCapacity,pdstStopTime);
				if (pdtmEndTime.AddSeconds(EXCEPTED_TIME) < dtmFreeTimeAfterEnd)
				{
					drowFreeTimeAfter[STARTFREETIME_FLD] = pdtmEndTime;
					drowFreeTimeAfter[ENDFREETIME_FLD] = dtmFreeTimeAfterEnd;
					pdstFreeTime.Tables[pstrWorkCenterCode].Rows.Add(drowFreeTimeAfter);
				}

				//remove old freetime
				arrFreeTime[0].Delete();

				//next working day
				dtmCurrentWorkingDay = GetNextWorkingDay(dtmCurrentWorkingDay,pdstCalendar);
				 
				//change shift configured day to working day
				//GetWorkingDayStartAndEndTime(dtmCurrentWorkingDay,ref dtmWorkingDayStart,ref dtmWorkingDayEnd,pdtbWCCapacityAndShift,pintWorkCenterID);
				GetWorkingDayStartAndEndTime(dtmCurrentWorkingDay,ref dtmWorkingDayStart,ref dtmWorkingDayEnd,pdstRemainCapacity,pstrWorkCenterCode);

				pdtmStartTime = dtmWorkingDayStart;
			}	
			if (pdtmStartTime == pdtmEndTime)
			{
				return;
			}
			InsertResultDetail(pintWorkCenterID,pstrWorkCenterCode,pdstDCPResultDetail,pintOpMasterID,pdtmStartTime,pdtmEndTime,0,1,0,(int)DCPResultTypeEnum.ChangeCategory,dtmCurrentWorkingDay,pdtbWCCapacity);

			//adjust freetime
			arrFreeTime = pdstFreeTime.Tables[pstrWorkCenterCode].Select(
				STARTFREETIME_FLD + " <= '" + pdtmStartTime + "' AND " +
				ENDFREETIME_FLD + " >= '" + pdtmEndTime + "'");
			
			if (arrFreeTime.Length != 1)
			{
				FileStream objFileStream = new FileStream(@"C:\debug",FileMode.OpenOrCreate,FileAccess.Write);
				foreach (DataRow dr in pdstDCPResultDetail.Tables[pstrWorkCenterCode].Rows)
				{
					byte[] arr = System.Text.Encoding.ASCII.GetBytes(dr["StartTime"].ToString() + "->" + dr["EndTime"].ToString() + "(" + dr["Type"] + ")\r\n");
					objFileStream.Write(arr,0,arr.Length);
				}
				foreach (DataRow dr in pdstFreeTime.Tables[pstrWorkCenterCode].Rows)
				{
					byte[] arr = System.Text.Encoding.ASCII.GetBytes(dr["StartTime"].ToString() + "->" + dr["EndTime"].ToString() + "\r\n");
					objFileStream.Write(arr,0,arr.Length);
				}
				objFileStream.Close();
			}

			//add 2 new freetimes
			drowFreeTimeBefore = pdstFreeTime.Tables[pstrWorkCenterCode].NewRow();
			dtmFreeTimeBeforeStart = Convert.ToDateTime(arrFreeTime[0][STARTFREETIME_FLD]);
			NormalizeFreeTime(ref dtmFreeTimeBeforeStart,ref pdtmStartTime,pintWorkCenterID,pdtbWCCapacityAndShift,pstrWorkCenterCode,pdstRemainCapacity,pdstStopTime);
			if (dtmFreeTimeBeforeStart.AddSeconds(EXCEPTED_TIME) < pdtmStartTime)
			{
				drowFreeTimeBefore[STARTFREETIME_FLD] = dtmFreeTimeBeforeStart;
				drowFreeTimeBefore[ENDFREETIME_FLD] = pdtmStartTime;

				pdstFreeTime.Tables[pstrWorkCenterCode].Rows.Add(drowFreeTimeBefore);
			}

			drowFreeTimeAfter = pdstFreeTime.Tables[pstrWorkCenterCode].NewRow();
			dtmFreeTimeAfterEnd = Convert.ToDateTime(arrFreeTime[0][ENDFREETIME_FLD]);
			NormalizeFreeTime(ref pdtmEndTime,ref dtmFreeTimeAfterEnd,pintWorkCenterID,pdtbWCCapacityAndShift,pstrWorkCenterCode,pdstRemainCapacity,pdstStopTime);
			if (pdtmEndTime.AddSeconds(EXCEPTED_TIME) < dtmFreeTimeAfterEnd)
			{
				drowFreeTimeAfter[STARTFREETIME_FLD] = pdtmEndTime;
				drowFreeTimeAfter[ENDFREETIME_FLD] = dtmFreeTimeAfterEnd;
				
				
				pdstFreeTime.Tables[pstrWorkCenterCode].Rows.Add(drowFreeTimeAfter);
			}

			//remove old freetime
			arrFreeTime[0].Delete();
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="pdtbWCCapacity"></param>
		/// <param name="pintWorkCenterID"></param>
		/// <returns></returns>
		private string GetWorkCenterCodeByID(
			DataTable pdtbWCCapacity, 
			int pintWorkCenterID)
		{
			DataRow[] arrWorkCenter = pdtbWCCapacity.Select(MST_WorkCenterTable.WORKCENTERID_FLD + "=" + pintWorkCenterID.ToString());
			if (arrWorkCenter.Length > 0)
			{
				return arrWorkCenter[0][MST_WorkCenterTable.CODE_FLD].ToString();
			}
			else
			{
				return string.Empty;
			}
		}

		private void GetWorkingDayStartAndEndTime(
			DateTime pdtmCurrentWorkingDay, 
			ref DateTime pdtmWorkingDayStart, 
			ref DateTime pdtmWorkingDayEnd, 
			DataSet pdstRemainCapacity,
			string pstrWorkCenterCode)
		{
			DataRow[] arrRemainCapacity = pdstRemainCapacity.Tables[pstrWorkCenterCode].Select(WORKINGDAY_FLD + "='" + pdtmCurrentWorkingDay.Date + "'");
			if (arrRemainCapacity.Length > 0)
			{
				pdtmWorkingDayStart = Convert.ToDateTime(arrRemainCapacity[0][WORKINGDAYSTART_FLD]);
				pdtmWorkingDayEnd = Convert.ToDateTime(arrRemainCapacity[0][WORKINGDAYEND_FLD]);
			}
			else
			{
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="pdtmCurrentWorkingDay"></param>
		/// <param name="pdtmWorkingDayStart"></param>
		/// <param name="pdtmWorkingDayEnd"></param>
		/// <param name="pdtbWCCapacityAndShift"></param>
		/// <param name="pintWorkCenterID"></param>
		private void GetWorkingDayStartAndEndTime(
			DateTime pdtmCurrentWorkingDay, 
			ref DateTime pdtmWorkingDayStart, 
			ref DateTime pdtmWorkingDayEnd, 
			DataTable pdtbWCCapacityAndShift, 
			int pintWorkCenterID)
		{			
			DataRow[] drowShifts = pdtbWCCapacityAndShift.Select(PRO_WCCapacityTable.WORKCENTERID_FLD 
				+ "=" + pintWorkCenterID
				+ " AND " + PRO_WCCapacityTable.BEGINDATE_FLD + " <= '" + pdtmCurrentWorkingDay + "'"
				+ " AND " + PRO_WCCapacityTable.ENDDATE_FLD + " >= '" + pdtmCurrentWorkingDay + "'"
				,PRO_ShiftPatternTable.WORKTIMEFROM_FLD + " ASC");

			if (drowShifts.Length <= 0)
			{
				return;
			}
			//change shift configured day to working day
			pdtmWorkingDayStart = new DateTime(pdtmCurrentWorkingDay.Year, pdtmCurrentWorkingDay.Month, pdtmCurrentWorkingDay.Day,
				((DateTime)drowShifts[0][PRO_ShiftPatternTable.WORKTIMEFROM_FLD]).Hour,
				((DateTime)drowShifts[0][PRO_ShiftPatternTable.WORKTIMEFROM_FLD]).Minute,
				((DateTime)drowShifts[0][PRO_ShiftPatternTable.WORKTIMEFROM_FLD]).Second);
			pdtmWorkingDayEnd = new DateTime(pdtmCurrentWorkingDay.Year, pdtmCurrentWorkingDay.Month, pdtmCurrentWorkingDay.Day,
				((DateTime)drowShifts[drowShifts.Length - 1][PRO_ShiftPatternTable.WORKTIMETO_FLD]).Hour,
				((DateTime)drowShifts[drowShifts.Length - 1][PRO_ShiftPatternTable.WORKTIMETO_FLD]).Minute,
				((DateTime)drowShifts[drowShifts.Length - 1][PRO_ShiftPatternTable.WORKTIMETO_FLD]).Second);
			double dblDiff = ((DateTime)drowShifts[drowShifts.Length - 1][PRO_ShiftPatternTable.WORKTIMETO_FLD]).Subtract((DateTime)drowShifts[0][PRO_ShiftPatternTable.WORKTIMEFROM_FLD]).Days;
			pdtmWorkingDayEnd = pdtmWorkingDayEnd.AddDays(dblDiff);
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="pdtmCurrentWorkingDay"></param>
		/// <param name="pdtbCurrentCapacity"></param>
		/// <returns></returns>
		private DateTime GetNextAvailableWorkingDay(
			DateTime pdtmCurrentWorkingDay, 
			DataTable pdtbCurrentCapacity)
		{
			return DateTime.MinValue;
		}

		/// <summary>
		/// Adjust workorder start and due time when capaciry was changed
		/// </summary>
		/// <param name="pintWorkCenterID"></param>
		/// <param name="pintDCOptionMasterID"></param>
	
		private void AdjustWorkOrder(int pintWorkCenterID,int pintDCOptionMasterID)
		{
			PRO_WorkOrderDetailDS dsWODetail = new PRO_WorkOrderDetailDS();
			DataSet dstWODetail = dsWODetail.ListByWorkCenter(pintWorkCenterID);
			
			PRO_WCCapacityDS dsWCC = new PRO_WCCapacityDS();
			DataTable dtbWCCapacity = dsWCC.GetWCCapacity(pintDCOptionMasterID);
			DataTable dtbWCCapacityAndShift = dsWCC.GetWCCapacityAndShift(pintDCOptionMasterID);

			foreach (DataRow drowWO in dstWODetail.Tables[0].Rows)
			{
				decimal decQty = Convert.ToDecimal(drowWO[PRO_WorkOrderDetailTable.ORDERQUANTITY_FLD]);
				decimal decLeadTime = Convert.ToDecimal(drowWO[LEADTIME_FLD]);
				decimal decTotalLeadTime = decQty * decLeadTime;
				DateTime dtmStartTime = Convert.ToDateTime(drowWO[PRO_WorkOrderDetailTable.STARTDATE_FLD]);

				DateTime dtmWorkingDay = GetDateOnlyByWorkCenter(dtmStartTime,pintWorkCenterID,dtbWCCapacityAndShift);
				
				decimal decTotalRealTime = ConvertToRealTime(dtmWorkingDay,decTotalLeadTime,pintWorkCenterID,dtbWCCapacity);

				DataRow[] drowShifts = dtbWCCapacityAndShift.Select(
					"'" + dtmWorkingDay + "' >= " + PRO_WCCapacityTable.BEGINDATE_FLD 
					+ " AND '" + dtmWorkingDay + "' <= " + PRO_WCCapacityTable.ENDDATE_FLD 
					+ " AND " + PRO_WCCapacityTable.WORKCENTERID_FLD + "=" + pintWorkCenterID,
					PRO_ShiftPatternTable.WORKTIMEFROM_FLD + " DESC");
				
				DateTime dtmEndTime = AddMoreSecondForwardOneDay(drowShifts,dtmStartTime,(double)decTotalRealTime,dtmWorkingDay);
				drowWO[PRO_WorkOrderDetailTable.DUEDATE_FLD] = dtmEndTime;
			}

			dsWODetail.UpdateDataSet(dstWODetail);
		}

		
		#endregion Functions for backward algorithm

		#region Algorithm implementated for MAP only
		
		#region Constants

		const int CHECKPOINT_BY_TIME = 2;
		const int CHECKPOINT_BY_QTY = 1;
		#endregion

		#region Functions

		private void InsertDCPResultForWOLines(
			int pintDCOptionMasterID, 
			DataSet pdstDCPResultMaster, 
			DataSet pdstDCPResultDetail, 
			DataRow[] parrWOLineItems,
			DataSet pdstItemRouting, 
			DataSet pdstFreeTime,
			DataTable pdtbWCCapacity)
		{
			int intMaxMasterID = 0;
			foreach (DataRow drowWOLineItem in parrWOLineItems)
			{
				DateTime dtmStart = Convert.ToDateTime(drowWOLineItem[PRO_WorkOrderDetailTable.STARTDATE_FLD]);
				drowWOLineItem[PRO_WorkOrderDetailTable.STARTDATE_FLD] = new DateTime(dtmStart.Year,dtmStart.Month,dtmStart.Day,dtmStart.Hour,dtmStart.Minute,dtmStart.Second,0);

				DateTime dtmDue = Convert.ToDateTime(drowWOLineItem[PRO_WorkOrderDetailTable.DUEDATE_FLD]);
				drowWOLineItem[PRO_WorkOrderDetailTable.DUEDATE_FLD] = new DateTime(dtmDue.Year,dtmDue.Month,dtmDue.Day,dtmDue.Hour,dtmDue.Minute,dtmDue.Second,0);

				DataRow[] arrRoutings = pdstItemRouting.Tables[ITM_RoutingTable.TABLE_NAME].Select(MASTERID + "=" + drowWOLineItem[MASTERID], 
					ITM_RoutingTable.STEP_FLD + " DESC");
				DataRow pdrowMainRouting = arrRoutings[0];
				//insert master
				DataRow drowOpMaster = pdstDCPResultMaster.Tables[pdrowMainRouting[WORKCENTERCODE_FLD].ToString()].NewRow();
				drowOpMaster[PRO_DCPResultMasterTable.DCPRESULTMASTERID_FLD] = ++intMaxMasterID;
				drowOpMaster[PRO_DCPResultMasterTable.WORKCENTERID_FLD] = pdrowMainRouting[PRO_WORoutingTable.WORKCENTERID_FLD];
				drowOpMaster[PRO_WORoutingTable.WOROUTINGID_FLD] = pdrowMainRouting[PRO_WORoutingTable.WOROUTINGID_FLD];
				drowOpMaster[ITM_RoutingTable.ROUTINGID_FLD] = pdrowMainRouting[ITM_RoutingTable.ROUTINGID_FLD];
				drowOpMaster[PRO_DCPResultMasterTable.QUANTITY_FLD] = drowWOLineItem[QUANTITY_FLD];
				drowOpMaster[PRO_DCPResultMasterTable.PRODUCTID_FLD] = pdrowMainRouting[PRO_WORoutingTable.PRODUCTID_FLD];
				drowOpMaster[PRO_DCPResultMasterTable.DCOPTIONMASTERID_FLD] = pintDCOptionMasterID;
				drowOpMaster[PRO_DCPResultMasterTable.WORKORDERDETAILID_FLD] = drowWOLineItem[PRO_WorkOrderDetailTable.WORKORDERDETAILID_FLD];
				drowOpMaster[PRO_DCPResultMasterTable.CPOID_FLD] = drowWOLineItem[MTR_CPOTable.CPOID_FLD];
				drowOpMaster[PRO_DCPResultMasterTable.STARTDATETIME_FLD] = drowWOLineItem[PRO_WorkOrderDetailTable.STARTDATE_FLD];
				drowOpMaster[PRO_DCPResultMasterTable.DUEDATETIME_FLD] = drowWOLineItem[PRO_WorkOrderDetailTable.DUEDATE_FLD];

				drowOpMaster[DETAILID_FLD] = pdrowMainRouting[DETAILID_FLD];
				pdstDCPResultMaster.Tables[pdrowMainRouting[WORKCENTERCODE_FLD].ToString()].Rows.Add(drowOpMaster);
				int intDCPResultMasterID = Convert.ToInt32(drowOpMaster[PRO_DCPResultMasterTable.DCPRESULTMASTERID_FLD]);

				//insert detail
				InsertResultDetail(Convert.ToInt32(pdrowMainRouting[MST_WorkCenterTable.WORKCENTERID_FLD]),pdrowMainRouting[WORKCENTERCODE_FLD].ToString(),pdstDCPResultDetail,intDCPResultMasterID,Convert.ToDateTime(drowWOLineItem[PRO_WorkOrderDetailTable.STARTDATE_FLD]),Convert.ToDateTime(drowWOLineItem[PRO_WorkOrderDetailTable.DUEDATE_FLD]),Convert.ToDecimal(pdrowMainRouting[LEADTIME_FLD]),Convert.ToDecimal(pdrowMainRouting[LEADTIME_FLD]),Convert.ToDecimal(drowWOLineItem[QUANTITY_FLD]),(int)DCPResultTypeEnum.Running,dtmStart.Date,pdtbWCCapacity);
				
				//adjust freetime
				//find freetime
				DataRow[] arrFreeTime = pdstFreeTime.Tables[pdrowMainRouting[WORKCENTERCODE_FLD].ToString()].Select(
					"(" + STARTFREETIME_FLD + "<='" + drowWOLineItem[PRO_WorkOrderDetailTable.STARTDATE_FLD].ToString()
					+ "' AND " + ENDFREETIME_FLD + ">='" +  drowWOLineItem[PRO_WorkOrderDetailTable.STARTDATE_FLD].ToString() + "')"
					+ " OR "
					+ "(" + STARTFREETIME_FLD + "<='" + drowWOLineItem[PRO_WorkOrderDetailTable.DUEDATE_FLD].ToString()
					+ "' AND " + ENDFREETIME_FLD + ">='" +  drowWOLineItem[PRO_WorkOrderDetailTable.DUEDATE_FLD].ToString() + "')");
				foreach (DataRow drowFreeTime in arrFreeTime)
				{
					if (Convert.ToDateTime(drowFreeTime[STARTFREETIME_FLD]) < Convert.ToDateTime(drowWOLineItem[PRO_WorkOrderDetailTable.STARTDATE_FLD]))
					{
						DataRow drowNew = pdstFreeTime.Tables[pdrowMainRouting[WORKCENTERCODE_FLD].ToString()].NewRow();
						drowNew[STARTFREETIME_FLD] = drowFreeTime[STARTFREETIME_FLD];
						drowNew[ENDFREETIME_FLD] = drowWOLineItem[PRO_WorkOrderDetailTable.STARTDATE_FLD];
						pdstFreeTime.Tables[pdrowMainRouting[WORKCENTERCODE_FLD].ToString()].Rows.Add(drowNew);
					}
					if (Convert.ToDateTime(drowFreeTime[ENDFREETIME_FLD]) > Convert.ToDateTime(drowWOLineItem[PRO_WorkOrderDetailTable.DUEDATE_FLD]))
					{
						DataRow drowNew = pdstFreeTime.Tables[pdrowMainRouting[WORKCENTERCODE_FLD].ToString()].NewRow();
						drowNew[STARTFREETIME_FLD] = drowWOLineItem[PRO_WorkOrderDetailTable.DUEDATE_FLD];
						drowNew[ENDFREETIME_FLD] = drowFreeTime[ENDFREETIME_FLD];
						pdstFreeTime.Tables[pdrowMainRouting[WORKCENTERCODE_FLD].ToString()].Rows.Add(drowNew);
					}
					drowFreeTime.Delete();
				}
			}
		}

		
		private DateTime ArrangeCPOItem(
			DateTime pdtmCycleAsOfDate,
			int pintPlanHorizon,
			DataRow pdrowCPOItem, 
			DataRow pdrowRouting, 
			DataSet pdstDCPResultMaster, 
			DataSet pdstDCPResultDetail, 
			DataTable pdtbWCCapacityAndShift,
			DataTable pdtbChangeCategory,
			DataSet pdstCalendar,
			int pintDCOptionMasterID,
			DataTable pdtbWCCapacity,
			DataTable pdtbOverCapacityWC,
			DataSet pdstRemainCapacity,
			DataSet pdstFreeTime,
			int pintMaxMasterID,
			DataSet pdstStopTime,
			DataTable pdtbRelatedWC)
		{
			//initialize variables
			DateTime dtmDetailStartTime = DateTime.MinValue;
			DateTime dtmDetailDueTime = DateTime.MinValue;
			DateTime dtmMasterStartTime = DateTime.MaxValue;
			DateTime dtmMasterDueTime = DateTime.MinValue;

			//determine duetime
			dtmDetailDueTime = Convert.ToDateTime(pdrowCPOItem[MTR_CPOTable.DUEDATE_FLD]);

			//determine main operation and main work center
			int intMainWorkCenterID = 0;
			string strMainWorkCenterCode = string.Empty;

			//calculate total leadtime
			decimal decTotalLeadTime = 0;
			decimal decRemainLeadTime = 0;
			decimal decQuantity = 0;

			try
			{
				decTotalLeadTime = Convert.ToDecimal(pdrowRouting[LEADTIME_FLD]);
				decRemainLeadTime = decTotalLeadTime;
				decQuantity = Convert.ToDecimal(pdrowCPOItem[QUANTITY_FLD]);
				intMainWorkCenterID = Convert.ToInt32(pdrowRouting[PRO_WORoutingTable.WORKCENTERID_FLD]);
				strMainWorkCenterCode = pdrowRouting[WORKCENTERCODE_FLD].ToString();
			}
			catch {}

			//Insert master record
			DataRow drowOpMaster = pdstDCPResultMaster.Tables[strMainWorkCenterCode].NewRow();
			drowOpMaster[PRO_DCPResultMasterTable.DCPRESULTMASTERID_FLD] = pintMaxMasterID + 1;
			drowOpMaster[PRO_DCPResultMasterTable.WORKCENTERID_FLD] = intMainWorkCenterID;
			drowOpMaster[PRO_WORoutingTable.WOROUTINGID_FLD] = pdrowRouting[PRO_WORoutingTable.WOROUTINGID_FLD];
			drowOpMaster[ITM_RoutingTable.ROUTINGID_FLD] = pdrowRouting[ITM_RoutingTable.ROUTINGID_FLD];
			drowOpMaster[PRO_DCPResultMasterTable.QUANTITY_FLD] = decQuantity;
			drowOpMaster[PRO_DCPResultMasterTable.PRODUCTID_FLD] = pdrowRouting[PRO_WORoutingTable.PRODUCTID_FLD];
			drowOpMaster[PRO_DCPResultMasterTable.DCOPTIONMASTERID_FLD] = pintDCOptionMasterID;
			drowOpMaster[PRO_DCPResultMasterTable.WORKORDERDETAILID_FLD] = pdrowCPOItem[PRO_WorkOrderDetailTable.WORKORDERDETAILID_FLD];
			drowOpMaster[PRO_DCPResultMasterTable.CPOID_FLD] = pdrowCPOItem[MTR_CPOTable.CPOID_FLD];
			drowOpMaster[DETAILID_FLD] = pdrowRouting[DETAILID_FLD];
			pdstDCPResultMaster.Tables[strMainWorkCenterCode].Rows.Add(drowOpMaster);
			int intDCPResultMasterID = Convert.ToInt32(drowOpMaster[PRO_DCPResultMasterTable.DCPRESULTMASTERID_FLD]);
			DateTime dtmAsOfDateWorkingDay = GetDateOnlyByWorkCenter(pdtmCycleAsOfDate.AddHours(12),strMainWorkCenterCode,pdstRemainCapacity);
			//GetDateOnlyByWorkCenter(pdtmCycleAsOfDate.AddHours(12),intMainWorkCenterID,pdtbWCCapacityAndShift);

			DateTime dtmCurrentWorkingDay = GetDateOnlyByWorkCenter(dtmDetailDueTime,strMainWorkCenterCode,pdstRemainCapacity);
			//GetDateOnlyByWorkCenter(dtmDetailDueTime,intMainWorkCenterID,pdtbWCCapacityAndShift);
			DataRow[] arrRemainCapacities = pdstRemainCapacity.Tables[pdrowRouting[WORKCENTERCODE_FLD].ToString()].Select(WORKINGDAY_FLD + "<='" + dtmCurrentWorkingDay.Date + "' AND " + REMAINCAPACITY_FLD + " > " + EXCEPTED_TIME.ToString(),WORKINGDAY_FLD + " DESC");

			if (arrRemainCapacities.Length == 0)
			{
				InsertIntoOverCapacity(pdtbWCCapacity,pdtbOverCapacityWC,intMainWorkCenterID,dtmAsOfDateWorkingDay,pdrowRouting,decRemainLeadTime,pintPlanHorizon,pdtbRelatedWC);
				drowOpMaster.Delete();
				return dtmDetailStartTime;
			}
			dtmCurrentWorkingDay = Convert.ToDateTime(arrRemainCapacities[0][WORKINGDAY_FLD]);

			if (dtmCurrentWorkingDay < dtmAsOfDateWorkingDay)
			{
				//insert into overcapacity
				InsertIntoOverCapacity(pdtbWCCapacity,pdtbOverCapacityWC,intMainWorkCenterID,dtmAsOfDateWorkingDay,pdrowRouting,decRemainLeadTime,pintPlanHorizon,pdtbRelatedWC);
				drowOpMaster.Delete();
				return dtmDetailStartTime;
			}

			while ((double)decRemainLeadTime > EXCEPTED_TIME)
			{
				decimal decArranged = TryToArrangeCPOItem(
					ref dtmDetailStartTime,
					ref dtmDetailDueTime,
					intMainWorkCenterID,
					dtmCurrentWorkingDay,
					decRemainLeadTime,
					pdstDCPResultMaster,
					pdstDCPResultDetail,
					pdtbWCCapacityAndShift,
					intDCPResultMasterID,
					pdtbChangeCategory,
					pdstCalendar,
					pdrowCPOItem,
					pdrowRouting,
					pdtbWCCapacity,
					pdstRemainCapacity,
					pdstFreeTime,
					dtmAsOfDateWorkingDay,
					strMainWorkCenterCode,
					pdstStopTime);
				decRemainLeadTime -= decArranged;
								
				if (decArranged == 0)
				{
					//previous working day
					dtmCurrentWorkingDay = GetPreviousWorkingDay(dtmCurrentWorkingDay,pdstCalendar);
					arrRemainCapacities = pdstRemainCapacity.Tables[pdrowRouting[WORKCENTERCODE_FLD].ToString()].Select(WORKINGDAY_FLD + "<='" + dtmCurrentWorkingDay.Date + "' AND " + REMAINCAPACITY_FLD + " > " + EXCEPTED_TIME.ToString(),WORKINGDAY_FLD + " DESC");
					if (arrRemainCapacities.Length == 0)
					{
						dtmCurrentWorkingDay = dtmAsOfDateWorkingDay.Subtract(new TimeSpan(1,0,0,0));
					}
					else
					{
						dtmCurrentWorkingDay = Convert.ToDateTime(arrRemainCapacities[0][WORKINGDAY_FLD]);
					}
					if (dtmCurrentWorkingDay < dtmAsOfDateWorkingDay)
					{
						InsertIntoOverCapacity(pdtbWCCapacity,pdtbOverCapacityWC,intMainWorkCenterID,dtmAsOfDateWorkingDay,pdrowRouting,decRemainLeadTime,pintPlanHorizon,pdtbRelatedWC);
						break;
					}
				}
				else
				{
					if ((dtmDetailDueTime > dtmMasterDueTime) && (dtmDetailDueTime != DateTime.MinValue))
					{
						dtmMasterDueTime = dtmDetailDueTime;
					}
					if ((dtmDetailStartTime < dtmMasterStartTime) && (dtmDetailStartTime != DateTime.MinValue))
					{
						dtmMasterStartTime = dtmDetailStartTime;
					}
					dtmDetailDueTime = dtmDetailStartTime;	
				}
			}

			//update some information
			if ((dtmMasterStartTime != DateTime.MaxValue) && (dtmMasterDueTime != DateTime.MinValue))
			{	
				drowOpMaster[PRO_DCPResultMasterTable.STARTDATETIME_FLD] = dtmMasterStartTime;
				drowOpMaster[PRO_DCPResultMasterTable.DUEDATETIME_FLD] = dtmMasterDueTime;
			}
			else
			{
				if (decTotalLeadTime == 0)
				{
					InsertResultDetail(intMainWorkCenterID,strMainWorkCenterCode,pdstDCPResultDetail,intDCPResultMasterID,dtmDetailDueTime,dtmDetailDueTime,0,0,decQuantity,(int)DCPResultTypeEnum.Running,dtmCurrentWorkingDay,pdtbWCCapacity);
					drowOpMaster[PRO_DCPResultMasterTable.STARTDATETIME_FLD] = dtmDetailDueTime;
					drowOpMaster[PRO_DCPResultMasterTable.DUEDATETIME_FLD] = dtmDetailDueTime;
				}
				else
				{
					drowOpMaster.Delete();
				}
			}
			
			//return start-time
			return dtmDetailStartTime;
		}

		
		private decimal TryToArrangeCPOItem(
			ref DateTime pdtmStartTime,
			ref DateTime pdtmDueTime,
			int pintWorkCenterID,
			DateTime pdtmCurrentWorkingDay, 
			decimal pdecRemainLeadTime,
			DataSet pdstDCPResultMaster,
			DataSet pdstDCPResultDetail,
			DataTable pdtbWCCapacityAndShift,
			int pintDCPResultMasterID,
			DataTable pdtbChangeCategory,
			DataSet pdstCalendar,
			DataRow pdrowItem,
			DataRow pdrowMainRouting,
			DataTable pdtbWCCapacity,
			DataSet pdstRemainCapacity,
			DataSet pdstFreeTime,
			DateTime pdtmAsOfDate,
			string pstrWorkCenterCode,
			DataSet pdstStopTime)
		{
			decimal decArranged = 0;
			DateTime dtmFreeTimeStart = DateTime.MinValue;
			DateTime dtmFreeTimeEnd = DateTime.MinValue;
			if (GetFreeCapacityTime(pdtmDueTime,ref dtmFreeTimeStart,ref dtmFreeTimeEnd,pdtmCurrentWorkingDay,pintWorkCenterID,pdrowMainRouting[WORKCENTERCODE_FLD].ToString(),pdtbWCCapacityAndShift,pdstDCPResultMaster,pdstDCPResultDetail,pdstCalendar,pdstFreeTime,pdstRemainCapacity,pdstStopTime))
			{
				if (ArrangeChangeCategory(ref dtmFreeTimeStart,ref dtmFreeTimeEnd,pintWorkCenterID,pdrowMainRouting[WORKCENTERCODE_FLD].ToString(),pintDCPResultMasterID,pdstDCPResultMaster,pdstDCPResultDetail,pdtbChangeCategory,pdtbWCCapacityAndShift,pdtmCurrentWorkingDay,pdstCalendar,pdtmAsOfDate,pdstRemainCapacity,pdstStopTime,pdstFreeTime,pdtbWCCapacity))
				{
					NormalizeFreeTime(ref dtmFreeTimeStart,ref dtmFreeTimeEnd,pintWorkCenterID,pdtbWCCapacityAndShift,pstrWorkCenterCode,pdstRemainCapacity,pdstStopTime);
					//calculate needed real time to complete remain leadtime and earliest start time to complete
					decimal decNeededRealTime = CalculateNeededRealTime(pdecRemainLeadTime,pdrowItem,pdrowMainRouting,pdtmCurrentWorkingDay,pintWorkCenterID,pdtbWCCapacity);					
					DateTime dtmNeededStartTime = AddMoreSecondBackwardMultiDay(dtmFreeTimeEnd,(double)decNeededRealTime,pdtmCurrentWorkingDay,pintWorkCenterID,pdtbWCCapacityAndShift,pdstCalendar,pdtmAsOfDate,pstrWorkCenterCode,pdstStopTime);
					
					//fill as forward
					if (dtmNeededStartTime < dtmFreeTimeStart)
					{
						decArranged = FillCPOItemPartToFreeTime(pdecRemainLeadTime,pintDCPResultMasterID,pdstDCPResultDetail,dtmFreeTimeStart,dtmFreeTimeEnd,pdrowItem,pdrowMainRouting,pdtmCurrentWorkingDay,pintWorkCenterID,pdtbWCCapacity,pdtbWCCapacityAndShift,pdstRemainCapacity,pdstFreeTime,pstrWorkCenterCode,pdstStopTime);
						pdtmStartTime = dtmFreeTimeStart;
						pdtmDueTime = dtmFreeTimeEnd;
					}
					else
					{						
						decArranged = FillCPOItemPartToFreeTime(pdecRemainLeadTime,pintDCPResultMasterID,pdstDCPResultDetail,dtmNeededStartTime,dtmFreeTimeEnd,pdrowItem,pdrowMainRouting,pdtmCurrentWorkingDay,pintWorkCenterID,pdtbWCCapacity,pdtbWCCapacityAndShift,pdstRemainCapacity,pdstFreeTime,pstrWorkCenterCode,pdstStopTime);
						pdtmStartTime = dtmNeededStartTime;
						pdtmDueTime = dtmFreeTimeEnd;
					}
				}
			}
			
			return decArranged;
		}

		
		private void NormalizeFreeTime(
			ref DateTime pdtmFreeTimeStart, 
			ref DateTime pdtmFreeTimeEnd, 
			int pintWorkCenterID, 
			DataTable pdtbWCCapacityAndShift, string pstrWorkCenterCode, DataSet pdstRemainCapacity, DataSet pdstStopTime)
		{
			DateTime dtmFreeTimeStartWorkingDay = GetDateOnlyByWorkCenter(pdtmFreeTimeStart,pstrWorkCenterCode,pdstRemainCapacity);
			//GetDateOnlyByWorkCenter(pdtmFreeTimeStart,pintWorkCenterID,pdtbWCCapacityAndShift);
			DateTime dtmFreeTimeEndWorkingDay = GetDateOnlyByWorkCenter(pdtmFreeTimeEnd,pstrWorkCenterCode,pdstRemainCapacity);
			//GetDateOnlyByWorkCenter(pdtmFreeTimeEnd,pintWorkCenterID,pdtbWCCapacityAndShift);

			DataRow[] drowShifts;
			DataTable dtbStopTime;
			DataRow[] arrStops;
			
			//			drowShifts = pdtbWCCapacityAndShift.Select(
			//				"'" + dtmFreeTimeStartWorkingDay + "' >= " + PRO_WCCapacityTable.BEGINDATE_FLD 
			//				+ " AND '" + dtmFreeTimeStartWorkingDay + "' <= " + PRO_WCCapacityTable.ENDDATE_FLD 
			//				+ " AND " + PRO_WCCapacityTable.WORKCENTERID_FLD + "=" + pintWorkCenterID,
			//				PRO_ShiftPatternTable.WORKTIMEFROM_FLD + " DESC");
			//			dtbStopTime = GetStopTime(drowShifts);
			//			dtbStopTime = pdstStopTime.Tables[pstrWorkCenterCode].Copy();
			dtbStopTime = GetStopTime(pdstStopTime,dtmFreeTimeStartWorkingDay).Copy();
			ChangeStopTimeToWorkingDay(dtmFreeTimeStartWorkingDay,dtbStopTime,pintWorkCenterID,pdtbWCCapacityAndShift,pstrWorkCenterCode,pdstRemainCapacity);
			arrStops = dtbStopTime.Select(FROM_FLD + " < '" + pdtmFreeTimeStart + "' AND " + TO_FLD + " > '" + pdtmFreeTimeStart + "'");
			if (arrStops.Length > 0)
			{
				pdtmFreeTimeStart = Convert.ToDateTime(arrStops[0][TO_FLD]);
			}

			drowShifts = pdtbWCCapacityAndShift.Select(
				"'" + dtmFreeTimeEndWorkingDay + "' >= " + PRO_WCCapacityTable.BEGINDATE_FLD 
				+ " AND '" + dtmFreeTimeEndWorkingDay + "' <= " + PRO_WCCapacityTable.ENDDATE_FLD 
				+ " AND " + PRO_WCCapacityTable.WORKCENTERID_FLD + "=" + pintWorkCenterID,
				PRO_ShiftPatternTable.WORKTIMEFROM_FLD + " DESC");
			dtbStopTime = GetStopTime(drowShifts);
			ChangeStopTimeToWorkingDay(dtmFreeTimeEndWorkingDay,dtbStopTime,pintWorkCenterID,pdtbWCCapacityAndShift,pstrWorkCenterCode,pdstRemainCapacity);
			arrStops = dtbStopTime.Select(FROM_FLD + " < '" + pdtmFreeTimeEnd + "' AND " + TO_FLD + " > '" + pdtmFreeTimeEnd + "'");
			if (arrStops.Length > 0)
			{
				pdtmFreeTimeEnd = Convert.ToDateTime(arrStops[0][FROM_FLD]);
			}
		}

		
		private void ChangeStopTimeToWorkingDay(
			DateTime pdtmWorkingDay, 
			DataTable pdtbStopTimes, 
			int pintWorkCenterID, 
			DataTable pdtbWCCapacityAndShift, 
			string pstrWorkCenterCode, 
			DataSet pdstRemainCapacity)
		{
			if (pdtbStopTimes.Rows.Count == 0)
			{
				return;
			}

			DataRow[] arrStopTimes = pdtbStopTimes.Select(string.Empty,FROM_FLD + " ASC");
			
			DateTime dtmWorkingDayStart = DateTime.MinValue;
			DateTime dtmWorkingDayEnd = DateTime.MinValue;
			DateTime dtmFirstShiftConfigStart = Convert.ToDateTime(arrStopTimes[0][FROM_FLD]);
			
			//GetWorkingDayStartAndEndTime(pdtmWorkingDay,ref dtmWorkingDayStart,ref dtmWorkingDayEnd,pdtbWCCapacityAndShift,pintWorkCenterID);
			GetWorkingDayStartAndEndTime(pdtmWorkingDay,ref dtmWorkingDayStart,ref dtmWorkingDayEnd,pdstRemainCapacity,pstrWorkCenterCode);

			DateTime dtmConfigDayStart = new DateTime(dtmFirstShiftConfigStart.Year,dtmFirstShiftConfigStart.Month,dtmFirstShiftConfigStart.Day,dtmWorkingDayStart.Hour,dtmWorkingDayStart.Minute,dtmWorkingDayStart.Second);
			if (dtmFirstShiftConfigStart.Hour < dtmWorkingDayStart.Hour)
			{
				dtmConfigDayStart.Subtract(new TimeSpan(1,0,0,0));
			}

			foreach (DataRow drowStop in arrStopTimes)
			{
				drowStop[FROM_FLD] = dtmWorkingDayStart.Add(Convert.ToDateTime(drowStop[FROM_FLD]).Subtract(dtmConfigDayStart));
				drowStop[TO_FLD] = dtmWorkingDayStart.Add(Convert.ToDateTime(drowStop[TO_FLD]).Subtract(dtmConfigDayStart));
			}
		}

		
		private decimal FillCPOItemPartToFreeTime(
			decimal pdecRemainLeadTime,
			int pintDCPResultMasterID,
			DataSet pdstDCPResultDetail,
			DateTime pdtmStartTime, 
			DateTime pdtmEndTime, 
			DataRow pdrowItem, 
			DataRow pdrowMainRouting, 
			DateTime pdtmCurrentWorkingDay, 
			int pintWorkCenterID,
			DataTable pdtbWCCapacity,
			DataTable pdtbWCCapacityAndShift,
			DataSet pdstRemainCapacity,
			DataSet pdstFreeTime,
			string pstrWorkCenterCode,
			DataSet pdstStopTime)
		{			
			decimal decTotalQuantity = Convert.ToDecimal(pdrowItem[QUANTITY_FLD]);
			decimal decTotalLeadTime = Convert.ToDecimal(pdrowMainRouting[LEADTIME_FLD]);
			decimal decLeadTimePerQty =  decTotalLeadTime / decTotalQuantity;
			int intCheckpointSamplePattern = 0;
			try
			{
				intCheckpointSamplePattern = Convert.ToInt32(pdrowMainRouting[PRO_CheckPointTable.SAMPLEPATTERN_FLD]);
			}
			catch
			{}

			decimal decCheckpointSampleRate = 0;
			try
			{
				decCheckpointSampleRate = Convert.ToInt32(pdrowMainRouting[PRO_CheckPointTable.SAMPLERATE_FLD]);
			}
			catch
			{}

			decimal decCheckpointDelayTime = 0;
			try
			{
				decCheckpointDelayTime = Convert.ToInt32(pdrowMainRouting[PRO_CheckPointTable.DELAYTIME_FLD]);
			} 
			catch
			{}

			decimal decWorkingTimeBeforeCheckpoint = ConvertToRealTime(pdtmCurrentWorkingDay,pdecRemainLeadTime,pintWorkCenterID,pdtbWCCapacity);
			if (intCheckpointSamplePattern == CHECKPOINT_BY_QTY)
			{
				decWorkingTimeBeforeCheckpoint = ConvertToRealTime(pdtmCurrentWorkingDay,decCheckpointSampleRate * decLeadTimePerQty,pintWorkCenterID,pdtbWCCapacity);
			}
			else if (intCheckpointSamplePattern == CHECKPOINT_BY_TIME)
			{
				decWorkingTimeBeforeCheckpoint = decCheckpointSampleRate;
			}

			DataRow[] drowShifts = pdtbWCCapacityAndShift.Select(PRO_WCCapacityTable.WORKCENTERID_FLD 
				+ "=" + pintWorkCenterID,PRO_ShiftPatternTable.WORKTIMEFROM_FLD + " ASC");

			DateTime dtmTempStartTime = pdtmStartTime;
			DateTime dtmTempEndTime = AddMoreSecondForwardOneDay(drowShifts,dtmTempStartTime,(double)decWorkingTimeBeforeCheckpoint,pdtmCurrentWorkingDay,pstrWorkCenterCode,pdstStopTime);
			if (dtmTempEndTime > pdtmEndTime)
			{
				dtmTempEndTime = pdtmEndTime;
			}
			decimal decArranged = 0;
			while (dtmTempStartTime < pdtmEndTime)
			{
				//running
				decimal decPartRealTime = GetRealWorkingTimeInWorkDay(drowShifts, dtmTempStartTime,dtmTempEndTime,pdtmCurrentWorkingDay,pstrWorkCenterCode,pdstStopTime);
				decimal decPartLeadTime = ConvertToLeadTime(pdtmCurrentWorkingDay,decPartRealTime,pintWorkCenterID,pdtbWCCapacity);
				InsertResultDetail(Convert.ToInt32(pdrowMainRouting[MST_WorkCenterTable.WORKCENTERID_FLD]),pdrowMainRouting[WORKCENTERCODE_FLD].ToString(),pdstDCPResultDetail,pintDCPResultMasterID,dtmTempStartTime,dtmTempEndTime,decPartLeadTime,decTotalLeadTime,decTotalQuantity,(int)DCPResultTypeEnum.Running,pdtmCurrentWorkingDay,pdtbWCCapacity);
				decArranged += decPartLeadTime;

				//checkpoint
				if ((decPartRealTime >= decWorkingTimeBeforeCheckpoint) && (decCheckpointDelayTime > 0))
				{
					dtmTempStartTime = dtmTempEndTime;
					dtmTempEndTime = AddMoreSecondForwardOneDay(drowShifts,dtmTempStartTime,(double)decCheckpointDelayTime,pdtmCurrentWorkingDay,pstrWorkCenterCode,pdstStopTime);
					if (dtmTempEndTime <= pdtmEndTime)
					{
						InsertResultDetail(Convert.ToInt32(pdrowMainRouting[MST_WorkCenterTable.WORKCENTERID_FLD]),pdrowMainRouting[WORKCENTERCODE_FLD].ToString(),pdstDCPResultDetail,pintDCPResultMasterID,dtmTempStartTime,dtmTempEndTime,0,decTotalLeadTime,decTotalQuantity,(int)DCPResultTypeEnum.CheckPoint,pdtmCurrentWorkingDay,pdtbWCCapacity);
					}
					else
					{
						break;
					}					
				}
				//next
				dtmTempStartTime = dtmTempEndTime;
				dtmTempEndTime = AddMoreSecondForwardOneDay(drowShifts,dtmTempStartTime,(double)decWorkingTimeBeforeCheckpoint,pdtmCurrentWorkingDay,pstrWorkCenterCode,pdstStopTime);
				if (dtmTempEndTime > pdtmEndTime)
				{
					dtmTempEndTime = pdtmEndTime;
				}
			}

			decimal decTotalRealTime = GetRealWorkingTimeInWorkDay(drowShifts,pdtmStartTime,pdtmEndTime,pdtmCurrentWorkingDay,pstrWorkCenterCode,pdstStopTime);
			decimal decTotalCapacity = ConvertToLeadTime(pdtmCurrentWorkingDay,decTotalRealTime,pintWorkCenterID,pdtbWCCapacity);

			DataRow[] drowRemainCapacity = pdstRemainCapacity.Tables[pdrowMainRouting[WORKCENTERCODE_FLD].ToString()].Select(WORKINGDAY_FLD + "='" + pdtmCurrentWorkingDay.Date + "'");
			drowRemainCapacity[0][REMAINCAPACITY_FLD] = Convert.ToDecimal(drowRemainCapacity[0][REMAINCAPACITY_FLD]) - decTotalCapacity;

			//adjust freetime
			DataRow[] arrFreeTime = pdstFreeTime.Tables[pdrowMainRouting[WORKCENTERCODE_FLD].ToString()].Select(
				STARTFREETIME_FLD + " <= '" + pdtmStartTime + "' AND " +
				ENDFREETIME_FLD + " >= '" + pdtmEndTime + "'");
			
			if (arrFreeTime.Length != 1)
			{
				decArranged.ToString();
			}

			//add 2 new freetimes
			DataRow drowFreeTimeBefore = pdstFreeTime.Tables[pdrowMainRouting[WORKCENTERCODE_FLD].ToString()].NewRow();
			DateTime dtmFreeTimeBeforeStart = Convert.ToDateTime(arrFreeTime[0][STARTFREETIME_FLD]);
			if (dtmFreeTimeBeforeStart.AddSeconds(EXCEPTED_TIME) < pdtmStartTime)
			{
				NormalizeFreeTime(ref dtmFreeTimeBeforeStart,ref pdtmStartTime,pintWorkCenterID,pdtbWCCapacityAndShift,pstrWorkCenterCode,pdstRemainCapacity,pdstStopTime);
				drowFreeTimeBefore[STARTFREETIME_FLD] = dtmFreeTimeBeforeStart; 
				drowFreeTimeBefore[ENDFREETIME_FLD] = pdtmStartTime;
				pdstFreeTime.Tables[pdrowMainRouting[WORKCENTERCODE_FLD].ToString()].Rows.Add(drowFreeTimeBefore);
			}

			DataRow drowFreeTimeAfter = pdstFreeTime.Tables[pdrowMainRouting[WORKCENTERCODE_FLD].ToString()].NewRow();
			DateTime dtmFreeTimeAfterEnd = Convert.ToDateTime(arrFreeTime[0][ENDFREETIME_FLD]);
			if (pdtmEndTime.AddSeconds(EXCEPTED_TIME) < dtmFreeTimeAfterEnd)
			{
				NormalizeFreeTime(ref pdtmEndTime,ref dtmFreeTimeAfterEnd,pintWorkCenterID,pdtbWCCapacityAndShift,pstrWorkCenterCode,pdstRemainCapacity,pdstStopTime);
				drowFreeTimeAfter[STARTFREETIME_FLD] = pdtmEndTime;
				drowFreeTimeAfter[ENDFREETIME_FLD] = dtmFreeTimeAfterEnd;
				pdstFreeTime.Tables[pdrowMainRouting[WORKCENTERCODE_FLD].ToString()].Rows.Add(drowFreeTimeAfter);
			}

			//remove old freetime
			arrFreeTime[0].Delete();

			return decArranged;
		}

		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="pdecRemainLeadTime"></param>
		/// <param name="pdrowItem"></param>
		/// <param name="pdrowMainRouting"></param>
		/// <param name="pdtmCurrentWorkingDay"></param>
		/// <param name="pintWorkCenterID"></param>
		/// <param name="pdtbWCCapacity"></param>
		/// <returns></returns>
		private decimal CalculateNeededRealTime(
			decimal pdecRemainLeadTime, 
			DataRow pdrowItem, 
			DataRow pdrowMainRouting, 
			DateTime pdtmCurrentWorkingDay,
			int pintWorkCenterID,
			DataTable pdtbWCCapacity)
		{
			decimal decTotalQuantity = Convert.ToDecimal(pdrowItem[QUANTITY_FLD]);
			decimal decTotalLeadTime = Convert.ToDecimal(pdrowMainRouting[LEADTIME_FLD]);
			decimal decLeadTimePerQty =  decTotalLeadTime / decTotalQuantity;
			int intCheckpointSamplePattern = 0;
			try
			{
				intCheckpointSamplePattern = Convert.ToInt32(pdrowMainRouting[PRO_CheckPointTable.SAMPLEPATTERN_FLD]);
			}
			catch
			{}

			decimal decCheckpointSampleRate = 0;
			try
			{
				decCheckpointSampleRate = Convert.ToInt32(pdrowMainRouting[PRO_CheckPointTable.SAMPLERATE_FLD]);
			}
			catch
			{}

			decimal decCheckpointDelayTime = 0;
			try
			{
				decCheckpointDelayTime = Convert.ToInt32(pdrowMainRouting[PRO_CheckPointTable.DELAYTIME_FLD]);
			} 
			catch
			{}

			decimal decWorkingTimeBeforeCheckpoint = 0;
			if (intCheckpointSamplePattern == CHECKPOINT_BY_QTY)
			{
				decWorkingTimeBeforeCheckpoint = ConvertToRealTime(pdtmCurrentWorkingDay,decCheckpointSampleRate * decLeadTimePerQty,pintWorkCenterID,pdtbWCCapacity);
			}
			else if (intCheckpointSamplePattern == CHECKPOINT_BY_TIME)
			{
				decWorkingTimeBeforeCheckpoint = decCheckpointSampleRate;
			}

			//convert remaining leadtime to realtime
			decimal decRemainRealTime = ConvertToRealTime(pdtmCurrentWorkingDay,pdecRemainLeadTime,pintWorkCenterID,pdtbWCCapacity);
			int intCheckpointTimeCount = 0;
			if (decWorkingTimeBeforeCheckpoint != 0)
			{
				intCheckpointTimeCount = Convert.ToInt32(decimal.Floor(decRemainRealTime / decWorkingTimeBeforeCheckpoint));
			}

			decimal decNeededRealTime = decRemainRealTime + intCheckpointTimeCount * decCheckpointDelayTime;

			return decNeededRealTime;
		}

		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="pdrowItem"></param>
		/// <param name="pdrowMainRouting"></param>
		/// <returns></returns>
/*		private double CalculateCheckpointTime(
			DataRow pdrowItem, 
			DataRow pdrowMainRouting)
		{
			decimal decTotalQuantity = Convert.ToDecimal(pdrowItem[QUANTITY_FLD]);
			decimal decTotalLeadTime = Convert.ToDecimal(pdrowMainRouting[LEADTIME_FLD]);
			int intCheckpointSamplePattern = 0;
			try
			{
				intCheckpointSamplePattern = Convert.ToInt32(pdrowMainRouting[PRO_CheckPointTable.SAMPLEPATTERN_FLD]);
			}
			catch
			{}

			decimal decCheckpointSampleRate = 0;
			try
			{
				decCheckpointSampleRate = Convert.ToInt32(pdrowMainRouting[PRO_CheckPointTable.SAMPLERATE_FLD]);
			}
			catch
			{}

			decimal decCheckpointDelayTime = 0;
			try
			{
				decCheckpointDelayTime = Convert.ToInt32(pdrowMainRouting[PRO_CheckPointTable.DELAYTIME_FLD]);
			} 
			catch
			{}

			decimal decTotalCheckpointDelayTime = 0;
			if (intCheckpointSamplePattern == CHECKPOINT_BY_QTY)
			{
				decTotalCheckpointDelayTime = decimal.Floor((decTotalQuantity / decCheckpointSampleRate)) * decCheckpointDelayTime;
			}
			else if (intCheckpointSamplePattern == CHECKPOINT_BY_TIME)
			{
				decTotalCheckpointDelayTime = decimal.Floor((decTotalLeadTime / decCheckpointSampleRate)) * decCheckpointDelayTime;
			}			
			return (double)decTotalCheckpointDelayTime;
		}*/

		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="pdtmCurrentWorkingDay"></param>
		/// <param name="pdstCalendar"></param>
		/// <returns></returns>
		private DateTime GetPreviousWorkingDay(
			DateTime pdtmCurrentWorkingDay, 
			DataSet pdstCalendar)
		{
			DateTime dtmPreviousWorkingDay = pdtmCurrentWorkingDay.Subtract(new TimeSpan(1,0,0,0));
			
			while (true)
			{
				if (!IsOffDay(dtmPreviousWorkingDay,pdstCalendar))
				{
					break;
				}
				 
				dtmPreviousWorkingDay = dtmPreviousWorkingDay.Subtract(new TimeSpan(1,0,0,0));
			}
			return dtmPreviousWorkingDay;
		}

		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="pdtmCurrentWorkingDay"></param>
		/// <param name="pdstCalendar"></param>
		/// <returns></returns>
		private DateTime GetNextWorkingDay(
			DateTime pdtmCurrentWorkingDay, 
			DataSet pdstCalendar)
		{
			DateTime dtmNextWorkingDay = pdtmCurrentWorkingDay.Add(new TimeSpan(1,0,0,0));
			
			while (true)
			{
				if (!IsOffDay(dtmNextWorkingDay,pdstCalendar))
				{
					break;
				}
				 
				dtmNextWorkingDay = dtmNextWorkingDay.Add(new TimeSpan(1,0,0,0));
			}
			return dtmNextWorkingDay;
		}

		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="pdtmDueTime"></param>
		/// <param name="pdtmFreeTimeStart"></param>
		/// <param name="pdtmFreeTimeEnd"></param>
		/// <param name="pdtmWorkingDay"></param>
		/// <param name="pintWorkCenterID"></param>
		/// <param name="pdtbWCCapacityAndShift"></param>
		/// <param name="pdstDCPResultMaster"></param>
		/// <param name="pdstDCPResultDetail"></param>
		/// <param name="pdstCalendar"></param>
		/// <returns></returns>
		private bool GetFreeCapacityTime(
			DateTime pdtmDueTime, 
			ref DateTime pdtmFreeTimeStart, 
			ref DateTime pdtmFreeTimeEnd, 
			DateTime pdtmWorkingDay, 
			int pintWorkCenterID, 
			string pstrWorkCenterCode, 
			DataTable pdtbWCCapacityAndShift, 
			DataSet pdstDCPResultMaster, 
			DataSet pdstDCPResultDetail, 
			DataSet pdstCalendar,
			DataSet pdstFreeTime,
			DataSet pdstRemainCapacity,
			DataSet pdstStopTime)
		{						
			DateTime dtmWorkingDayStart = DateTime.MinValue;
			DateTime dtmWorkingDayEnd = DateTime.MinValue;
			
			//GetWorkingDayStartAndEndTime(pdtmWorkingDay,ref dtmWorkingDayStart,ref dtmWorkingDayEnd,pdtbWCCapacityAndShift,pintWorkCenterID);
			GetWorkingDayStartAndEndTime(pdtmWorkingDay,ref dtmWorkingDayStart,ref dtmWorkingDayEnd,pdstRemainCapacity,pstrWorkCenterCode);

			//			DataRow[] drowShifts = pdtbWCCapacityAndShift.Select(
			//				"'" + pdtmDueTime + "' >= " + PRO_WCCapacityTable.BEGINDATE_FLD 
			//				+ " AND '" + pdtmDueTime + "' <= " + PRO_WCCapacityTable.ENDDATE_FLD 
			//				+ " AND " + PRO_WCCapacityTable.WORKCENTERID_FLD + "=" + pintWorkCenterID,
			//				PRO_ShiftPatternTable.WORKTIMEFROM_FLD + " DESC");
			//			DataTable dtbStopTime = GetStopTime(drowShifts);
			//DataTable dtbStopTime = pdstStopTime.Tables[pstrWorkCenterCode].Copy();
			DataTable dtbStopTime = GetStopTime(pdstStopTime,pdtmWorkingDay).Copy();
			
			ChangeStopTimeToWorkingDay(pdtmWorkingDay,dtbStopTime,pintWorkCenterID,pdtbWCCapacityAndShift,pstrWorkCenterCode,pdstRemainCapacity);
			ChangeStopTimeToWorkingDay(pdtmWorkingDay,dtbStopTime,pintWorkCenterID,pdtbWCCapacityAndShift,pstrWorkCenterCode,pdstRemainCapacity);
			DataRow[] arrStops = dtbStopTime.Select(FROM_FLD + " < '" + pdtmDueTime + "' AND " + TO_FLD + " > '" + pdtmDueTime + "'");
			if (arrStops.Length > 0)
			{
				pdtmDueTime = Convert.ToDateTime(arrStops[0][FROM_FLD]);
			}


			if (dtmWorkingDayStart > pdtmDueTime)
			{
				return false;
			}

			DateTime dtmConditionStart = dtmWorkingDayStart;
			DateTime dtmConditionEnd = (pdtmDueTime < dtmWorkingDayEnd) ? pdtmDueTime : dtmWorkingDayEnd;
			DataRow[] arrFreeTimes = pdstFreeTime.Tables[pstrWorkCenterCode].Select(
				"(" + STARTFREETIME_FLD + " >= '" + dtmConditionStart + "'"
				+ " AND " + STARTFREETIME_FLD + " < '" + dtmConditionEnd + "')"
				+ " OR (" + ENDFREETIME_FLD + " >= '" + dtmConditionStart + "'"
				+ " AND " + ENDFREETIME_FLD + " < '" + dtmConditionEnd + "')",
				STARTFREETIME_FLD + " DESC"
				);
			if (arrFreeTimes.Length == 0)
			{
				return false;
			}
			pdtmFreeTimeStart = (dtmConditionStart < Convert.ToDateTime(arrFreeTimes[0][STARTFREETIME_FLD])) ? Convert.ToDateTime(arrFreeTimes[0][STARTFREETIME_FLD]) : dtmConditionStart;
			pdtmFreeTimeEnd = (dtmConditionEnd > Convert.ToDateTime(arrFreeTimes[0][ENDFREETIME_FLD])) ? Convert.ToDateTime(arrFreeTimes[0][ENDFREETIME_FLD]) : dtmConditionEnd;

			if (pdtmFreeTimeStart.AddSeconds(EXCEPTED_TIME) <= pdtmFreeTimeEnd)
			{
				return true;
			}
			else
			{
				return false;
			}
			/*DataRow[] arrResultDetails = pdstDCPResultDetail.Tables[pstrWorkCenterCode].Select(
				PRO_DCPResultDetailTable.STARTTIME_FLD + "< '" + dtmConditionEnd + "'"
				+ " AND " + PRO_DCPResultDetailTable.ENDTIME_FLD + "> '" + dtmConditionStart + "'",
				PRO_DCPResultDetailTable.ENDTIME_FLD + " DESC");
			
			if(arrResultDetails.Length == 0)
			{
				pdtmFreeTimeStart = dtmConditionStart;
				pdtmFreeTimeEnd = dtmConditionEnd;
				if(pdtmFreeTimeEnd.Subtract(pdtmFreeTimeStart).TotalSeconds < EXCEPTED_TIME )
				{
					return false;
				}
				else
				{
					return true;
				}
			}
			else //drowDetails.Length > 0
			{
				DateTime dtmFirstPartStart = Convert.ToDateTime(arrResultDetails[0][PRO_DCPResultDetailTable.STARTTIME_FLD]);				
				DateTime dtmFirstPartEnd = Convert.ToDateTime(arrResultDetails[0][PRO_DCPResultDetailTable.ENDTIME_FLD]);

				if (dtmFirstPartEnd >= dtmConditionEnd)
				{
					int i = 0;
					if (arrResultDetails.Length > 1) 
					{
						DateTime dtmSecondPartStart = DateTime.MinValue;
						DateTime dtmSecondPartEnd = DateTime.MinValue;
						while (i < arrResultDetails.Length - 1)
						{
							dtmFirstPartStart = Convert.ToDateTime(arrResultDetails[i][PRO_DCPResultDetailTable.STARTTIME_FLD]);				
							dtmFirstPartEnd = Convert.ToDateTime(arrResultDetails[i][PRO_DCPResultDetailTable.ENDTIME_FLD]);
							dtmSecondPartStart = Convert.ToDateTime(arrResultDetails[i + 1][PRO_DCPResultDetailTable.STARTTIME_FLD]);				
							dtmSecondPartEnd = Convert.ToDateTime(arrResultDetails[i + 1][PRO_DCPResultDetailTable.ENDTIME_FLD]);						
							if (dtmFirstPartStart > dtmSecondPartEnd)
							{
								break;
							}
							i++;
						}
						if (i < arrResultDetails.Length - 1)
						{
							pdtmFreeTimeStart = dtmSecondPartEnd;
							pdtmFreeTimeEnd = dtmFirstPartStart;
						}
						else
						{							
							pdtmFreeTimeStart = dtmConditionStart;
							pdtmFreeTimeEnd = dtmSecondPartStart;
						}
					}
					else
					{						
						pdtmFreeTimeStart = dtmConditionStart;
						pdtmFreeTimeEnd = dtmFirstPartStart;
					}
				}
				else
				{
					pdtmFreeTimeEnd = dtmConditionEnd;
					pdtmFreeTimeStart = dtmFirstPartEnd;
				}

				if(pdtmFreeTimeEnd.Subtract(pdtmFreeTimeStart).TotalSeconds < EXCEPTED_TIME )
				{
					return false;
				}
				else
				{
					return true;
				}
			}*/
		}

		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="pdstDCPResultMaster"></param>
		/// <param name="pdstDCPResultDetail"></param>
		/// <param name="pintWorkCenterID"></param>
		/// <returns></returns>
		private string GetDCPMasterIDsByWorkCenterID(
			DataSet pdstDCPResultMaster, 
			DataSet pdstDCPResultDetail, 
			int pintWorkCenterID)
		{
			DataRow[] arrResultMasters = pdstDCPResultMaster.Tables[0].Select(PRO_DCPResultMasterTable.WORKCENTERID_FLD + "=" + pintWorkCenterID.ToString());
			string strMasterIDs = "(";
			foreach (DataRow drowResultMaster in arrResultMasters)
			{
				strMasterIDs += drowResultMaster[PRO_DCPResultMasterTable.DCPRESULTMASTERID_FLD].ToString() + ",";
			}
			strMasterIDs = strMasterIDs.Substring(0,strMasterIDs.Length - 1) + ")";			
			return strMasterIDs;
		}

		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="pdtbOverCapacityWC"></param>
		private void CreateOverCapacityWCTable(
			DataTable pdtbOverCapacityWC)
		{
			pdtbOverCapacityWC.Columns.Add(MST_WorkCenterTable.WORKCENTERID_FLD,DbType.Int32.GetType());
			pdtbOverCapacityWC.Columns.Add(MST_WorkCenterTable.CODE_FLD,typeof(string));
			pdtbOverCapacityWC.Columns.Add(MST_WorkCenterTable.DESCRIPTION_FLD,typeof(string));
			pdtbOverCapacityWC.Columns.Add(OVERLEADTIME_FLD,typeof(decimal));
			pdtbOverCapacityWC.Columns.Add(CAPACITY_FLD,typeof(decimal));
			pdtbOverCapacityWC.Columns.Add(OVERDAYS_FLD,typeof(decimal));
			pdtbOverCapacityWC.Columns.Add(OVERPERCENT_FLD,typeof(decimal));

			DataColumn dcolOverLeadTime = pdtbOverCapacityWC.Columns[OVERLEADTIME_FLD];
			dcolOverLeadTime.DefaultValue = 0;
			DataColumn dcolWCID = pdtbOverCapacityWC.Columns[MST_WorkCenterTable.WORKCENTERID_FLD];
			pdtbOverCapacityWC.PrimaryKey = new DataColumn[1] {dcolWCID};
		}

		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="pdtmAsOfDate"></param>
		/// <param name="pintPlanHorizon"></param>
		/// <param name="pdtbWCCapacity"></param>
		/// <param name="pdstRemainCapacity"></param>
		/// <param name="pdstCalendar"></param>
		/// <returns></returns>
		private DataSet CreateFreeTimeDataSet(
			DateTime pdtmAsOfDate, 
			int pintPlanHorizon, 
			DataTable pdtbRelatedWC, 
			DataSet pdstRemainCapacity, 
			DataSet pdstCalendar)
		{
			DataSet dstFreeTime = new DataSet();
			foreach (DataRow drowWC in pdtbRelatedWC.Rows)
			{
				DataTable dtbFreeTimeInDay = new DataTable(drowWC[MST_WorkCenterTable.CODE_FLD].ToString());
				dtbFreeTimeInDay.Columns.Add(STARTFREETIME_FLD,typeof(DateTime));
				dtbFreeTimeInDay.Columns.Add(ENDFREETIME_FLD,typeof(DateTime));
				
				for (int i = 0; i < pintPlanHorizon; i++)
				{
					DateTime dtmCurrent = pdtmAsOfDate.AddDays(i);
					if (!IsOffDay(dtmCurrent,pdstCalendar))
					{
						DataRow drowRemainCapacity = pdstRemainCapacity.Tables[drowWC[MST_WorkCenterTable.CODE_FLD].ToString()].Select(WORKINGDAY_FLD + "='" + dtmCurrent + "'")[0];
						DataRow drowFreeCapacity = dtbFreeTimeInDay.NewRow();
						drowFreeCapacity[STARTFREETIME_FLD] = Convert.ToDateTime(drowRemainCapacity[WORKINGDAYSTART_FLD]);
						drowFreeCapacity[ENDFREETIME_FLD] = Convert.ToDateTime(drowRemainCapacity[WORKINGDAYEND_FLD]);

						dtbFreeTimeInDay.Rows.Add(drowFreeCapacity);
					}
				}					
				dstFreeTime.Tables.Add(dtbFreeTimeInDay);
			}
			return dstFreeTime;
		}

		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="pdtmAsOfDate"></param>
		/// <param name="pintPlanHorizon"></param>
		/// <param name="pdtbWCCapacity"></param>
		/// <param name="pdtbWCCapacityAndShift"></param>
		/// <param name="pdstCalendar"></param>
		/// <returns></returns>
		private DataSet CreateRemainCapacityDataSet(
			DateTime pdtmAsOfDate, 
			int pintPlanHorizon, 
			DataTable pdtbRelatedWC,
			DataTable pdtbWCCapacityAndShift, 
			DataSet pdstCalendar,
			DataTable pdtbWCCapacity)
		{
			DataSet dstRemainCapacity = new DataSet();
			foreach (DataRow drowWC in pdtbRelatedWC.Rows)
			{
				DataTable dtbRemainCapacity = new DataTable(drowWC[MST_WorkCenterTable.CODE_FLD].ToString());
				
				dtbRemainCapacity.Columns.Add(WORKINGDAY_FLD,typeof(DateTime));
				dtbRemainCapacity.Columns.Add(WORKINGDAYSTART_FLD,typeof(DateTime));
				dtbRemainCapacity.Columns.Add(WORKINGDAYEND_FLD,typeof(DateTime));
				dtbRemainCapacity.Columns.Add(REMAINCAPACITY_FLD,typeof(decimal));

				for (int i = 0; i < pintPlanHorizon; i++)
				{
					DateTime dtmCurrent = pdtmAsOfDate.AddDays(i);
					//get total capacity
					DataRow[] arrWCCapacity = pdtbWCCapacity.Select(
						PRO_WCCapacityTable.WORKCENTERID_FLD + "=" + drowWC[MST_WorkCenterTable.WORKCENTERID_FLD] + " AND " +
						PRO_WCCapacityTable.BEGINDATE_FLD + "<='" + dtmCurrent.Date + "' AND " +
						PRO_WCCapacityTable.ENDDATE_FLD + ">='" + dtmCurrent.Date + "'");
					
					decimal decTotalCapacity  = -1;
					if (arrWCCapacity.Length > 0)
					{
						decTotalCapacity = Convert.ToDecimal(arrWCCapacity[0][CAPACITY_FLD]);
					}
					else 
					{
					}
						
					if ((!IsOffDay(dtmCurrent,pdstCalendar)) || (decTotalCapacity == 0))
					{
						DateTime dtmWorkingDayStart =DateTime.MinValue;
						DateTime dtmWorkingDayEnd = DateTime.MinValue;
						GetWorkingDayStartAndEndTime(dtmCurrent,ref dtmWorkingDayStart, ref dtmWorkingDayEnd,pdtbWCCapacityAndShift,Convert.ToInt32(drowWC[MST_WorkCenterTable.WORKCENTERID_FLD]));
						
						//decimal decTotalCapacity = Convert.ToDecimal(drowWC[CAPACITY_FLD]);

						DataRow drowRemainCapacity = dtbRemainCapacity.NewRow();
						drowRemainCapacity[WORKINGDAY_FLD] = dtmCurrent.Date;
						drowRemainCapacity[WORKINGDAYSTART_FLD] = dtmWorkingDayStart;
						drowRemainCapacity[WORKINGDAYEND_FLD] = dtmWorkingDayEnd;
						drowRemainCapacity[REMAINCAPACITY_FLD] = decTotalCapacity;
						dtbRemainCapacity.Rows.Add(drowRemainCapacity);
					}
					else
					{						
						DateTime dtmWorkingDayStart =DateTime.MinValue;
						DateTime dtmWorkingDayEnd = DateTime.MinValue;
						GetWorkingDayStartAndEndTime(dtmCurrent,ref dtmWorkingDayStart, ref dtmWorkingDayEnd,pdtbWCCapacityAndShift,Convert.ToInt32(drowWC[MST_WorkCenterTable.WORKCENTERID_FLD]));
						DataRow drowRemainCapacity = dtbRemainCapacity.NewRow();
						drowRemainCapacity[WORKINGDAY_FLD] = dtmCurrent.Date;
						drowRemainCapacity[WORKINGDAYSTART_FLD] = dtmWorkingDayStart;
						drowRemainCapacity[WORKINGDAYEND_FLD] = dtmWorkingDayEnd;
						drowRemainCapacity[REMAINCAPACITY_FLD] = -1;
						dtbRemainCapacity.Rows.Add(drowRemainCapacity);
					}
				}

				dstRemainCapacity.Tables.Add(dtbRemainCapacity);
			}
			return dstRemainCapacity;
		}

		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="pdtbWCCapacity"></param>
		/// <param name="pdtbWCCapacityAndShift"></param>
		/// <param name="pdtmAsOfDate"></param>
		/// <returns></returns>
		private DataSet CreateStopTimeDataSet(
			DataTable pdtbRelatedWC, 
			DataTable pdtbWCCapacityAndShift, 
			DateTime pdtmAsOfDate,
			DataTable pdtbWCCapacity)
		{
			DataSet dstStopTime = new DataSet();
			
			DataTable dtbIndex = new DataTable(TBL_INDEX);
			dtbIndex.Columns.Add(PRO_WCCapacityTable.BEGINDATE_FLD,typeof(DateTime));
			dtbIndex.Columns.Add(PRO_WCCapacityTable.ENDDATE_FLD,typeof(DateTime));
			dtbIndex.Columns.Add(STOPTIMETABLE_FLD,typeof(string));

			foreach (DataRow drowWC in pdtbWCCapacity.Rows)
			{
				if (pdtbRelatedWC.Select(MST_WorkCenterTable.WORKCENTERID_FLD + "=" + drowWC[MST_WorkCenterTable.WORKCENTERID_FLD]).Length > 0)
				{
					string strCondition = "'" + drowWC[PRO_WCCapacityTable.BEGINDATE_FLD] + "' = " + PRO_WCCapacityTable.BEGINDATE_FLD 
						+ " AND '" + drowWC[PRO_WCCapacityTable.ENDDATE_FLD] + "' = " + PRO_WCCapacityTable.ENDDATE_FLD 
						+ " AND " + PRO_WCCapacityTable.WORKCENTERID_FLD + "=" + drowWC[MST_WorkCenterTable.WORKCENTERID_FLD];
					
					DataRow[] drowShifts = pdtbWCCapacityAndShift.Select(strCondition,PRO_ShiftPatternTable.WORKTIMEFROM_FLD + " DESC");
					DataTable dtbStopTime = GetStopTime(drowShifts);
					dtbStopTime.TableName = GetTableNameForWCAndTimeRange(drowWC[MST_WorkCenterTable.CODE_FLD].ToString(),drowWC[PRO_WCCapacityTable.BEGINDATE_FLD].ToString(),drowWC[PRO_WCCapacityTable.ENDDATE_FLD].ToString());
					dstStopTime.Tables.Add(dtbStopTime);	
					DataRow drowIndex = dtbIndex.NewRow();
					drowIndex[PRO_WCCapacityTable.BEGINDATE_FLD] = drowWC[PRO_WCCapacityTable.BEGINDATE_FLD];
					drowIndex[PRO_WCCapacityTable.ENDDATE_FLD] = drowWC[PRO_WCCapacityTable.ENDDATE_FLD];
					drowIndex[STOPTIMETABLE_FLD] = dtbStopTime.TableName;
					dtbIndex.Rows.Add(drowIndex);
				}
			}

			dstStopTime.Tables.Add(dtbIndex);

			return dstStopTime;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		private string GetTableNameForWCAndTimeRange(string pstrWorkCenterCode, string strBeginDate, string strEndDate)
		{
			const string STR_SEPARATOR = "#";
			return pstrWorkCenterCode + STR_SEPARATOR + strBeginDate + STR_SEPARATOR + strEndDate;
		}
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="pintDCOptionMasterID"></param>
		/// <returns></returns>
	
		public DataTable GetInvalidWOLineAndCPO(
			int pintDCOptionMasterID)
		{
			PRO_DCOptionMasterDS dsDCOptionMaster = new PRO_DCOptionMasterDS();
			return dsDCOptionMaster.GetInvalidWOLineAndCPO(pintDCOptionMasterID);
		}

		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="pintDCOptionMasterID"></param>
		/// <returns></returns>
	
		public DataTable GetNotConfiguredWC(
			int pintDCOptionMasterID)
		{			
			PRO_DCOptionMasterDS dsDCOptionMaster = new PRO_DCOptionMasterDS();
			return dsDCOptionMaster.GetNotConfiguredWC(pintDCOptionMasterID);
		}

		private DataTable GetRelatedWorkCenter(
			int pintDCOptionMasterID)
		{			
			PRO_DCOptionMasterDS dsDCOptionMaster = new PRO_DCOptionMasterDS();
			return dsDCOptionMaster.GetRelatedWorkCenter(pintDCOptionMasterID);
		}

	
		/// <summary>
		/// 
		/// </summary>
		/// <param name="pdtbWCCapacity"></param>
		/// <param name="pdtbOverCapacityWC"></param>
		/// <param name="pintMainWorkCenterID"></param>
		/// <param name="pdtmAsOfDateWorkingDay"></param>
		/// <param name="pdrowRouting"></param>
		/// <param name="pdecRemainLeadTime"></param>
		/// <param name="pintMaxDays"></param>
		private void InsertIntoOverCapacity(
			DataTable pdtbWCCapacity, 
			DataTable pdtbOverCapacityWC, 
			int pintMainWorkCenterID,
			DateTime pdtmAsOfDateWorkingDay,
			DataRow pdrowRouting,
			decimal pdecRemainLeadTime,
			int pintMaxDays,
			DataTable pdtbRelatedWC)
		{
			const decimal MINIMUM = 0.01M;
			DataRow[] arrOverCapacity = pdtbOverCapacityWC.Select(MST_WorkCenterTable.WORKCENTERID_FLD + "=" + pintMainWorkCenterID);
			DataRow drowOverCapacity = null;
			if (arrOverCapacity.Length > 0)
			{
				drowOverCapacity = arrOverCapacity[0];
			}
			else
			{
				drowOverCapacity = pdtbOverCapacityWC.NewRow();
				//drowOverCapacity[CAPACITY_FLD] = pdrowRouting[LEADTIME_FLD];
				DataRow[] arrWCC = pdtbWCCapacity.Select(PRO_WCCapacityTable.WORKCENTERID_FLD + "=" + pintMainWorkCenterID.ToString());// +
				//" AND " + PRO_WCCapacityTable.BEGINDATE_FLD + " <= '" + pdtmAsOfDateWorkingDay +
				//"' AND " + PRO_WCCapacityTable.ENDDATE_FLD + " >= '" + pdtmAsOfDateWorkingDay + "'");
				try
				{
					drowOverCapacity[CAPACITY_FLD] = decimal.Parse(arrWCC[0][CAPACITY_FLD].ToString());
				}
				catch
				{
					drowOverCapacity[CAPACITY_FLD] = 0;
				}
				if (Convert.ToDecimal(drowOverCapacity[CAPACITY_FLD]) == 0)
				{
					1.ToString();
				}
			}

			drowOverCapacity[OVERLEADTIME_FLD] = Convert.ToDecimal(drowOverCapacity[OVERLEADTIME_FLD]) + pdecRemainLeadTime;
			drowOverCapacity[MST_WorkCenterTable.WORKCENTERID_FLD] = pintMainWorkCenterID;
			drowOverCapacity[MST_WorkCenterTable.CODE_FLD] = pdrowRouting[WORKCENTERCODE_FLD];
			drowOverCapacity[MST_WorkCenterTable.DESCRIPTION_FLD] = pdrowRouting[MST_WorkCenterTable.DESCRIPTION_FLD];
			decimal decOverDays = Convert.ToDecimal(drowOverCapacity[OVERLEADTIME_FLD])/Convert.ToDecimal(drowOverCapacity[CAPACITY_FLD]);
			if (decOverDays < MINIMUM)
			{
				decOverDays = MINIMUM;
			}
			drowOverCapacity[OVERDAYS_FLD] = decOverDays;
			drowOverCapacity[OVERPERCENT_FLD] = 100 * (decOverDays) / pintMaxDays;
			if (arrOverCapacity.Length == 0)
			{
				pdtbOverCapacityWC.Rows.Add(drowOverCapacity);
			}
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="pdstDCPResultDetail"></param>
		/// <param name="pdtbWCCapacityAndShift"></param>
		/// <param name="pdstStopTime"></param>
		private void DivideDCPResultByShift(DataSet pdstDCPResultDetail, DataTable pdtbWCCapacityAndShift, DataSet pdstStopTime)
		{
			DataRow[] arrAllRow = pdstDCPResultDetail.Tables[0].Select();
			foreach (DataRow drowDCPResultDetail in arrAllRow)
			{
				DateTime dtmStartTime = Convert.ToDateTime(drowDCPResultDetail[PRO_DCPResultDetailTable.STARTTIME_FLD]);
				DateTime dtmEndTime = Convert.ToDateTime(drowDCPResultDetail[PRO_DCPResultDetailTable.ENDTIME_FLD]);
				DateTime dtmWorkingDate = Convert.ToDateTime(drowDCPResultDetail[PRO_DCPResultDetailTable.WORKINGDATE_FLD]);
				int intMainWorkCenterID = Convert.ToInt32(drowDCPResultDetail[MST_WorkCenterTable.WORKCENTERID_FLD]);
				int intDCPResultMasterID = Convert.ToInt32(drowDCPResultDetail[PRO_DCPResultDetailTable.DCPRESULTMASTERID_FLD]);
				int intType = Convert.ToInt32(drowDCPResultDetail[PRO_DCPResultDetailTable.TYPE_FLD]);
				decimal decTotalSecond = Convert.ToDecimal(drowDCPResultDetail[PRO_DCPResultDetailTable.TOTALSECOND_FLD]);
				decimal decQuantity = Convert.ToDecimal(drowDCPResultDetail[PRO_DCPResultDetailTable.QUANTITY_FLD]);
				decimal decPercentage = Convert.ToDecimal(drowDCPResultDetail[PRO_DCPResultDetailTable.PERCENTAGE_FLD]);
				string strMainWorkCenterCode = drowDCPResultDetail[WORKCENTERCODE_FLD].ToString();

				DataRow[] arrShifts = pdtbWCCapacityAndShift.Select(
					PRO_WCCapacityTable.BEGINDATE_FLD + "<='" + dtmWorkingDate.Date +
					"' AND " +
					PRO_WCCapacityTable.ENDDATE_FLD + ">='" + dtmWorkingDate.Date +
					"' AND " +
					PRO_WCCapacityTable.WORKCENTERID_FLD + "=" + intMainWorkCenterID,// +
					//" AND " +
					//PRO_ShiftPatternTable.WORKTIMEFROM_FLD + "<='" + dtmEndTime +
					//"' AND " +
					//PRO_ShiftPatternTable.WORKTIMETO_FLD + ">='" + dtmStartTime + "'",
					PRO_ShiftPatternTable.WORKTIMEFROM_FLD + " ASC"
					);

				//calculate effective seconds
				decimal decEffectiveSeconds = GetRealWorkingTimeInWorkDay(arrShifts,dtmStartTime,dtmEndTime,dtmWorkingDate,strMainWorkCenterCode,pdstStopTime);
				if (decEffectiveSeconds == 0)
				{
					continue;
				}
				drowDCPResultDetail.Delete();

				DateTime dtmTempStartTime = DateTime.MinValue;
				DateTime dtmTempEndTime = DateTime.MinValue;
				DateTime dtmShiftStartTime = DateTime.MinValue;
				DateTime dtmShiftEndTime= DateTime.MinValue;
				int intShiftID = 0;
				DateTime dtmShiftBase = Convert.ToDateTime(arrShifts[0][PRO_ShiftPatternTable.WORKTIMEFROM_FLD]).Date;
				//TODO : This can cause errors ???

				for (int intIdx = 0; intIdx < arrShifts.Length; intIdx++)
				{
					//intShiftPatternID = Convert.ToInt32(arrShifts[intIdx][PRO_ShiftPatternTable.SHIFTPATTERNID_FLD]);
					intShiftID = Convert.ToInt32(arrShifts[intIdx][PRO_ShiftTable.SHIFTID_FLD]);
					dtmShiftStartTime = Convert.ToDateTime(arrShifts[intIdx][PRO_ShiftPatternTable.WORKTIMEFROM_FLD]);
					dtmShiftEndTime = Convert.ToDateTime(arrShifts[intIdx][PRO_ShiftPatternTable.WORKTIMETO_FLD]);
					//Change shift pattern to workingday
					dtmShiftStartTime = dtmShiftStartTime.AddDays((dtmWorkingDate.Date.Subtract(dtmShiftBase)).TotalDays);
					dtmShiftEndTime = dtmShiftEndTime.AddDays((dtmWorkingDate.Date.Subtract(dtmShiftBase)).TotalDays);
					
					if (dtmShiftEndTime <= dtmStartTime)
					{
						continue;
					}

					dtmTempStartTime = (dtmShiftStartTime > dtmStartTime) ? dtmShiftStartTime : dtmStartTime;
					dtmTempEndTime = (dtmShiftEndTime < dtmEndTime) ? dtmShiftEndTime : dtmEndTime;
					
					if (dtmTempEndTime > dtmTempStartTime)
					{
						decimal decSeconds = GetRealWorkingTimeInWorkDay(arrShifts,dtmTempStartTime,dtmTempEndTime,dtmWorkingDate,strMainWorkCenterCode,pdstStopTime);
						DataRow drowNew = pdstDCPResultDetail.Tables[0].NewRow();
						drowNew[PRO_DCPResultDetailTable.DCPRESULTMASTERID_FLD] = intDCPResultMasterID;
						drowNew[PRO_DCPResultDetailTable.ENDTIME_FLD] = dtmTempEndTime;
						drowNew[PRO_DCPResultDetailTable.PERCENTAGE_FLD] = (decSeconds / decEffectiveSeconds) * decPercentage;
						drowNew[PRO_DCPResultDetailTable.QUANTITY_FLD] = m_blnRoundedQuantity ? Math.Round((decSeconds / decEffectiveSeconds) * decQuantity) : (decSeconds / decEffectiveSeconds) * decQuantity;
						drowNew[PRO_DCPResultDetailTable.STARTTIME_FLD] = dtmTempStartTime;
						drowNew[PRO_DCPResultDetailTable.TOTALSECOND_FLD] = (decSeconds / decEffectiveSeconds) * decTotalSecond;
						drowNew[PRO_DCPResultDetailTable.TYPE_FLD] = intType;
						drowNew[PRO_DCPResultDetailTable.WORKINGDATE_FLD] = dtmWorkingDate;
						drowNew["ShiftID"] = intShiftID;
						pdstDCPResultDetail.Tables[0].Rows.Add(drowNew);
					}
				}
			}
		}

		
		const string CPOLEVEL_FLD = "CPOLevel";
		const string HANDLED_FLD = "Handled";
		const int HANDLED_VALUE = 1;

		/// <summary>
		/// Sort CPO by order that have DCP run optimizedly
		/// </summary>
		/// <param name="pdstItemRouting"></param>
		/// <returns></returns>
		private DataRow[] SortCPOToOptimize(DataSet pdstItemRouting,DataSet pdstRemainCapacity)
		{
			DataColumn dcolLevel = new DataColumn(CPOLEVEL_FLD,typeof(int));
			pdstItemRouting.Tables[0].Columns.Add(dcolLevel);
			DataColumn dcolWorkingDay = new DataColumn(WORKINGDAY_FLD,typeof(DateTime));
			pdstItemRouting.Tables[0].Columns.Add(dcolWorkingDay);
			DataColumn dcolHandled = new DataColumn(HANDLED_FLD,typeof(int));
			pdstItemRouting.Tables[0].Columns.Add(dcolHandled);

			foreach (DataRow drowItem in pdstItemRouting.Tables[0].Rows)
			{
				DataRow drowRouting = pdstItemRouting.Tables[1].Select(MASTERID_FLD + "=" + drowItem[MASTERID_FLD])[0];
				drowItem[WORKINGDAY_FLD] = GetDateOnlyByWorkCenter(Convert.ToDateTime(drowItem[MTR_CPOTable.DUEDATE_FLD]),drowRouting[WORKCENTERCODE_FLD].ToString(),pdstRemainCapacity);
				drowItem[HANDLED_FLD] = 0;
				if (drowItem[MTR_CPOTable.PARENTCPOID_FLD] == DBNull.Value)
				{
					AssignCPOLevel(pdstItemRouting,drowItem,0);
				}
			}
			return pdstItemRouting.Tables[0].Select(string.Empty,CPOLEVEL_FLD + " ASC," + ITM_ProductTable.PRODUCTID_FLD + " ASC," + WORKINGDAY_FLD + " DESC");
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="pdstItemRouting"></param>
		/// <param name="pdrowItem"></param>
		/// <param name="pintLevel"></param>
		private void AssignCPOLevel(DataSet pdstItemRouting, DataRow pdrowItem, int pintLevel)
		{
			pdrowItem[CPOLEVEL_FLD] = pintLevel;
			if (pdrowItem[MTR_CPOTable.CPOID_FLD] == DBNull.Value)
			{
				return;
			}
			int intCPOID = Convert.ToInt32(pdrowItem[MTR_CPOTable.CPOID_FLD]);
			DataRow[] arrChildren = pdstItemRouting.Tables[0].Select(MTR_CPOTable.PARENTCPOID_FLD + "=" + intCPOID.ToString());
			foreach (DataRow drowChild in arrChildren)
			{
				AssignCPOLevel(pdstItemRouting,drowChild,pintLevel + 1);
			}
		}
	
		#endregion Functions

		#endregion Algorithm implementated for MAP only

		#endregion DUONGNA's codes

		#region TraDA's Codes
		/// <summary>
		/// Get StartDate from DueDate and Routing basic for an item
		/// </summary>
		/// <param name="pdstCalendar"></param>
		/// <param name="pdtbWCCapacity"></param>
		/// <param name="pdtbCurrentCapacity"></param>
		/// <param name="pdstItemRouting"></param>
		/// <param name="pdtbWCCapacityAndShift"></param>
		/// <param name="pdtmDueDate"></param>
		/// <returns>StartDate for WOLine</returns>
		/// <author>Trada</author>
		/// <date>Wednesday, November 2 2005</date>
		public void GetStartDateOfItem(DataSet pdstCalendar,
			DataTable pdtbWCCapacity,
			DataTable pdtbCurrentCapacity,
			ref DataSet pdstItemRouting, 
			DataTable pdtbWCCapacityAndShift,
			DateTime pdtmDueDate)
		{
			int intProductID = 0;
			string strSelect = string.Empty;
			double dblToTalSecondOfTheCurrentOperation;
			double dblToTalSecondOfTheNextOperation;
			double dblOverlapTimeOfTheCurrentOperation;
			double dblOverlapTimeOfTheNextOperation;
			decimal decOverlapQuantity;
			decimal decOverlapPercent;
			decimal decOrderQuantity; 
			decimal decSequenceNumber; 
			double dblTotalTimeFromStartToParallelOperation;
			double dblTotalTimeFromStartToCurrentOperation;
			double dblTotalTime;
			bool blnHasParallel = false;
			DateTime dtmNewStartDate;
			ArrayList arrDeltaTimeForEachStep = new ArrayList(); 
			//Query
			//double dblDeltaTime = 0;
			foreach (DataRow drow in pdstItemRouting.Tables[ITM_ProductTable.TABLE_NAME].Rows)
			{
				decOrderQuantity = decimal.Parse(drow[QUANTITY_FLD].ToString());
				intProductID = int.Parse(drow[ITM_ProductTable.PRODUCTID_FLD].ToString());
				//Select Routing by ProductID
				strSelect = ITM_ProductTable.PRODUCTID_FLD + " = '" + intProductID.ToString() + "'";
				DataRow[] drowRoutings = pdstItemRouting.Tables[ITM_RoutingTable.TABLE_NAME].Select(strSelect);
				//Get Time of the Start Operation
				dblToTalSecondOfTheCurrentOperation = GetTimeOfOperation(int.Parse(drowRoutings[0][MST_WorkCenterTable.WORKCENTERID_FLD].ToString()), 
					pdtbWCCapacity, drowRoutings[0]); 
				//Update DeltaTime
				arrDeltaTimeForEachStep.Add(dblToTalSecondOfTheCurrentOperation);
				//dblDeltaTime = dblDeltaTime + dblToTalSecondOfTheCurrentOperation;
				//Next to forward Operation
				for (int i = 1; i < drowRoutings.Length; i++)
				{
					//Check the type of the next Operation	

					#region Parallel Scheduling
					// if parallel scheduling
					if(decimal.Parse(drowRoutings[i][ITM_RoutingTable.SCHEDULESEQ_FLD].ToString()) > 0)
					{
						//Get Time of the next Operation
						dblToTalSecondOfTheNextOperation = GetTimeOfOperation(int.Parse(drowRoutings[i][MST_WorkCenterTable.WORKCENTERID_FLD].ToString()), 
							pdtbWCCapacity, drowRoutings[i]); 
						//Get Sequence number
						decSequenceNumber = decimal.Parse(drowRoutings[i][ITM_RoutingTable.SCHEDULESEQ_FLD].ToString());
						//Find the parallel Operation
						for (int j = 0; j < i; j++)
						{
							if ((!blnHasParallel) && (decimal.Parse(drowRoutings[j][ITM_RoutingTable.SCHEDULESEQ_FLD].ToString()) == decSequenceNumber))
							{
								blnHasParallel = true;
								//Calculate total of time from start operation to j operation
								dblTotalTimeFromStartToParallelOperation = 0;
								for (int k = 0; k < j; k++)
								{
									dblTotalTimeFromStartToParallelOperation += double.Parse(arrDeltaTimeForEachStep[k].ToString());
								}
								//Calculate total of time from start operation to i operation
								dblTotalTimeFromStartToCurrentOperation = 0;
								for (int k = 0; k < arrDeltaTimeForEachStep.Count; k++)
								{
									dblTotalTimeFromStartToCurrentOperation += double.Parse(arrDeltaTimeForEachStep[k].ToString());
								}
								//Compare dblTotalTimeFromStartToParallelOperation and dblTotalTimeFromStartToCurrentOperation
								if (dblTotalTimeFromStartToCurrentOperation > dblTotalTimeFromStartToParallelOperation + dblToTalSecondOfTheNextOperation)
								{
									continue;
								}
								else
								{
									dblOverlapTimeOfTheNextOperation = dblTotalTimeFromStartToParallelOperation + dblToTalSecondOfTheNextOperation
										- dblTotalTimeFromStartToCurrentOperation;
									//Update TotalTime
									arrDeltaTimeForEachStep.Add(dblOverlapTimeOfTheNextOperation);
								}
							}
						}
						if (!blnHasParallel) // linear scheduling 
						{
							//Update TotalTime
							arrDeltaTimeForEachStep.Add(dblToTalSecondOfTheNextOperation);
						}
					}
						#endregion 

						#region overlap % scheduling
					else if(decimal.Parse(drowRoutings[i][ITM_RoutingTable.OVERLAPPERCENT_FLD].ToString()) > 0) 
						//if overlap % scheduling
					{
						//Get Time of the next Operation
						dblToTalSecondOfTheNextOperation = GetTimeOfOperation(int.Parse(drowRoutings[i][MST_WorkCenterTable.WORKCENTERID_FLD].ToString()), 
							pdtbWCCapacity, drowRoutings[i]); 
						decOverlapPercent = decimal.Parse(drowRoutings[i][ITM_RoutingTable.OVERLAPPERCENT_FLD].ToString());
						//Compare OverlapQuantity and OrderQuantity
						if (decOverlapPercent >= decOrderQuantity) //Linear scheduling
						{
							//Update TotalTime
							arrDeltaTimeForEachStep.Add(dblToTalSecondOfTheNextOperation);
							//dblDeltaTime = dblDeltaTime + dblToTalSecondOfTheNextOperation;
						}
						else
						{
							//Calculate Overlap Time of the next Operation
							dblOverlapTimeOfTheNextOperation = (double)decOverlapPercent * dblToTalSecondOfTheNextOperation;
							//Calculate Overlap Time of the current Operation
							dblOverlapTimeOfTheCurrentOperation = (double)decOverlapPercent * dblToTalSecondOfTheCurrentOperation;
							//Compare dblToTalSecondOfTheCurrentOperation and dblToTalSecondOfTheNextOperation
							if (dblToTalSecondOfTheNextOperation - dblOverlapTimeOfTheNextOperation 
								+ dblOverlapTimeOfTheCurrentOperation > dblToTalSecondOfTheCurrentOperation)
							{
								//Update TotalTime
								arrDeltaTimeForEachStep.Add(dblOverlapTimeOfTheNextOperation);
								//dblDeltaTime = dblDeltaTime + dblOverlapTimeOfTheNextOperation;
							}
							else 
							{
								//Update TotalTime
								arrDeltaTimeForEachStep.Add((dblOverlapTimeOfTheCurrentOperation 
									+ dblToTalSecondOfTheNextOperation)
									- dblToTalSecondOfTheCurrentOperation);
								
								//								dblDeltaTime = dblDeltaTime + (dblOverlapTimeOfTheCurrentOperation 
								//									+ dblToTalSecondOfTheNextOperation)
								//									- dblToTalSecondOfTheCurrentOperation;
							}
						}
					}
						#endregion

						#region overlap quantity scheduling
					else if(decimal.Parse(drowRoutings[i][ITM_RoutingTable.OVERLAPQTY_FLD].ToString()) > 0) 
						//if overlap quantity scheduling
					{
						//Get Time of the next Operation
						dblToTalSecondOfTheNextOperation = GetTimeOfOperation(int.Parse(drowRoutings[i][MST_WorkCenterTable.WORKCENTERID_FLD].ToString()), 
							pdtbWCCapacity, drowRoutings[i]); 
						decOverlapQuantity = decimal.Parse(drowRoutings[i][ITM_RoutingTable.OVERLAPQTY_FLD].ToString());
						//Compare OverlapQuantity and OrderQuantity
						if (decOverlapQuantity >= decOrderQuantity) //Linear scheduling
						{
							//Update TotalTime
							arrDeltaTimeForEachStep.Add(dblToTalSecondOfTheNextOperation);
							//dblDeltaTime = dblDeltaTime + dblToTalSecondOfTheNextOperation;
						}
						else
						{
							//Calculate Overlap Time of the next Operation
							dblOverlapTimeOfTheNextOperation = (double)(decOverlapQuantity/decOrderQuantity) * dblToTalSecondOfTheNextOperation;
							//Calculate Overlap Time of the current Operation
							dblOverlapTimeOfTheCurrentOperation = (double)(decOverlapQuantity/decOrderQuantity) * dblToTalSecondOfTheCurrentOperation;
							//Compare dblToTalSecondOfTheCurrentOperation and dblToTalSecondOfTheNextOperation
							if (dblToTalSecondOfTheNextOperation - dblOverlapTimeOfTheNextOperation 
								+ dblOverlapTimeOfTheCurrentOperation > dblToTalSecondOfTheCurrentOperation)
							{
								//Update TotalTime
								arrDeltaTimeForEachStep.Add(dblOverlapTimeOfTheNextOperation);
								//dblDeltaTime = dblDeltaTime + dblOverlapTimeOfTheNextOperation;
							}
							else 
							{
								//Update TotalTime
								arrDeltaTimeForEachStep.Add((dblOverlapTimeOfTheCurrentOperation 
									+ dblToTalSecondOfTheNextOperation)
									- dblToTalSecondOfTheCurrentOperation);

								//								dblDeltaTime = dblDeltaTime + (dblOverlapTimeOfTheCurrentOperation 
								//									+ dblToTalSecondOfTheNextOperation)
								//									- dblToTalSecondOfTheCurrentOperation;
							}
						}
					}
						#endregion

						#region  linear scheduling 
					else // linear scheduling 
					{
						//Get Time of the next Operation
						dblToTalSecondOfTheNextOperation = GetTimeOfOperation(int.Parse(drowRoutings[i][MST_WorkCenterTable.WORKCENTERID_FLD].ToString()), 
							pdtbWCCapacity, drowRoutings[i]); 
						//Update TotalTime
						arrDeltaTimeForEachStep.Add(dblToTalSecondOfTheNextOperation);
						//dblDeltaTime = dblDeltaTime + dblToTalSecondOfTheNextOperation;
						
					}
					#endregion
				}
				//Calculate Total Time
				dblTotalTime = 0;
				for (int i = 0; i < arrDeltaTimeForEachStep.Count; i++)
				{
					dblTotalTime += double.Parse(arrDeltaTimeForEachStep[i].ToString());
				}
				//Calculate New StartDate
				dtmNewStartDate = ((DateTime)drow[PRO_WorkOrderDetailTable.DUEDATE_FLD]).AddSeconds(-dblTotalTime);
				drow[PRO_WorkOrderDetailTable.STARTDATE_FLD] = dtmNewStartDate;
			}
		}
		/// <summary>
		/// Get time of an Operation 
		/// </summary>
		/// <param name="pintWorkCenterID"></param>
		/// <param name="pdtbWCCapacity"></param>
		/// <param name="pdrowRouting"></param>
		/// <returns>Time of an Operation </returns>
		/// <author>Trada</author>
		/// <date>Thursday, November 3 2005</date>
		private double GetTimeOfOperation(int pintWorkCenterID, DataTable pdtbWCCapacity, DataRow pdrowRouting)
		{
			string strSelect;
			double dblTotalWorkTime = 0;
			decimal decLeadTime;
			decimal decCapacity;
			double dblToTalSecondForEachOperation;
			double dblDeltaTime= 0;
			//Calculate some parameters of the next Operation
			//Select Work Center Capacity by WorkCenterID
			strSelect = MST_WorkCenterTable.WORKCENTERID_FLD + " = '" + pintWorkCenterID.ToString() + "'";
			DataRow[] drowWCCs = pdtbWCCapacity.Select(strSelect);
			//Calculate Total Work Time
			foreach(DataRow drowWCC in drowWCCs)
			{
				// TODO: SonHT Why do you have to sum of total time
				dblTotalWorkTime = dblTotalWorkTime + double.Parse(drowWCC[TOTALWORKTIME].ToString());
			}
			//Calculate LeadTime
			decLeadTime = decimal.Parse(pdrowRouting[LEADTIME_FLD].ToString());
			//Calculate Capacity
			decCapacity = decimal.Parse(drowWCCs[0][PRO_WCCapacityTable.CAPACITY_FLD].ToString());
			//Calculate time of operations (dblToTalSecondForEachOperation)	
			dblToTalSecondForEachOperation = (double)decLeadTime * dblTotalWorkTime / (double)decCapacity;
			//Update DeltaTime
			dblDeltaTime = dblDeltaTime + dblToTalSecondForEachOperation;
			return dblDeltaTime;
		}
		
		/// <summary>
		/// GetNextWorkingTime
		/// </summary>
		/// <param name="pdtmTime"></param>
		/// <param name="pintWorkCenterID"></param>
		/// <param name="pstrWorkCenter"></param>
		/// <param name="pdtbWCCapacityAndShift"></param>
		/// <param name="pdstCalendar"></param>
		/// <param name="pdtbCurrentCapacity"></param>
		/// <returns></returns>
		/// <author>Trada</author>
		/// <date>Friday, November 4 2005</date>
		public DateTime GetNextWorkingTime(DateTime pdtmTime, 
			int pintWorkCenterID, 
			string pstrWorkCenter, 
			DataTable pdtbWCCapacityAndShift, 
			DataSet pdstCalendar, 
			DataTable pdtbCurrentCapacity)
		{

			const string METHOD_NAME = THIS + ".GetNextWorkingTime()";
			

			DateTime dtmDate = pdtmTime.Date;
			// Check working day calendar
			if(pdstCalendar.Tables[MST_WorkingDayMasterTable.TABLE_NAME].Select(MST_WorkingDayMasterTable.YEAR_FLD + "=" + dtmDate.Year).Length == 0)
			{
				// check year
				// Message: Setting working calendar for the year @, please
				throw new PCSBOException(ErrorCode.MESSAGE_DCP_SETTING_WORKING_CALENDAR,METHOD_NAME,null);
			}
			// Check BeginDate -> EndDate (shift sort desc by WorkTimeFrom)
			DataRow[] drows = pdtbWCCapacityAndShift.Select(
				"'" + dtmDate + "' >= " + PRO_WCCapacityTable.BEGINDATE_FLD 
				+ " AND '" + dtmDate + "' <= " + PRO_WCCapacityTable.ENDDATE_FLD 
				+ " AND " + PRO_WCCapacityTable.WORKCENTERID_FLD + " = " + pintWorkCenterID,
				PRO_ShiftPatternTable.WORKTIMEFROM_FLD + " DESC");
			if(drows.Length > 0)
			{
				DateTime dtmWorkTimeFrom = (DateTime)drows[drows.Length-1][PRO_ShiftPatternTable.WORKTIMEFROM_FLD];
				DateTime dtmWorkTimeTo = (DateTime)drows[0][PRO_ShiftPatternTable.WORKTIMETO_FLD];
				double dblTotalSecond = (dtmWorkTimeTo - dtmWorkTimeFrom).TotalSeconds;
				dtmDate = GetDateOnly(pdtmTime, dtmWorkTimeFrom, dtmWorkTimeTo);
				DataRow[] drowsWorkingDay = pdtbCurrentCapacity.Select("Date >= '"  + dtmDate.Date +  "' and IsOffDay = 0","Date ASC");
				//Check if drowsWorkingDay.Count = 0
				if(drowsWorkingDay.Length == 0)
				{
					InsertNewNextYearSimilarToCurrentYear(ref pdtbCurrentCapacity,ref pdtbWCCapacityAndShift, ref pdstCalendar);
					drowsWorkingDay = pdtbCurrentCapacity.Select("Date >= '"  + dtmDate.Date +  "' and IsOffDay = 0","Date ASC");
				}
				//Re-Set dtmDate
				dtmDate = (DateTime) drowsWorkingDay[0][DATE_FLD];
				DateTime dtmFrom = new DateTime(dtmDate.Year, dtmDate.Month, dtmDate.Day,
					dtmWorkTimeFrom.Hour, dtmWorkTimeFrom.Minute, dtmWorkTimeFrom.Second);
				DateTime dtmTo = dtmFrom.AddSeconds(dblTotalSecond);
				
				DataTable dtbStopTime = GetStopTime(drows);

				if((pdtmTime >= dtmFrom) && (pdtmTime <= dtmTo))
				{
					foreach(DataRow drowStop in dtbStopTime.Rows)
					{
						DateTime dtmStopFrom = new DateTime();
						DateTime dtmStopTo = new DateTime();
						dtmStopFrom = dtmFrom.AddSeconds(((DateTime)drowStop[FROM_FLD] - dtmWorkTimeFrom).TotalSeconds);
						dtmStopTo = dtmFrom.AddSeconds(((DateTime)drowStop[TO_FLD] - dtmWorkTimeFrom).TotalSeconds);
						if((pdtmTime >= dtmStopFrom) && (pdtmTime <= dtmStopTo))
						{
							return dtmStopTo;
						}
					}
					return pdtmTime;
				}
					// pdtmTime < From
				else if(pdtmTime < dtmFrom)
				{
					return dtmFrom;
				}
					// pdtmTime > To
				else if(pdtmTime > dtmTo)
				{
					//return dtmTo;
					return dtmFrom.AddDays(1);
				}

			}
			// TODO: Update Message: configuration work center 'x' capacity after 'date'
			Hashtable hshTable = new Hashtable();
			hshTable.Add(0,pstrWorkCenter);
			throw new PCSBOException(ErrorCode.MESSAGE_DCP_CONFIG_WORKCENTER,METHOD_NAME,null,hshTable);
			
		}
		#endregion

		#region Bottle feed algorithm

		private DataTable GetWorkCenterList(int pintDCOptionMasterID)
		{
			PRO_DCOptionMasterDS dsDCOptionMaster = new PRO_DCOptionMasterDS();
			DataTable dtbRelatedWC = dsDCOptionMaster.GetRelatedWorkCenter(pintDCOptionMasterID);

			DataTable dtbWorkCenterList = new DataTable();
			DataColumn dcolWorkCenterID = new DataColumn(MST_WorkCenterTable.WORKCENTERID_FLD,typeof(int));
			DataColumn dcolProductionLineID = new DataColumn(MST_WorkCenterTable.PRODUCTIONLINEID_FLD,typeof(int));
			DataColumn dcolWorkCenterCode = new DataColumn(WORKCENTERCODE_FLD,typeof(string));
			DataColumn dcolWorkCenterLevel = new DataColumn(WORKCENTERLEVEL_FLD,typeof(int));
			DataColumn dcolWorkCenterAncessors = new DataColumn(WORKCENTERANCESSORS_FLD,typeof(string));
			DataColumn dcolDepartmentCode = new DataColumn(DEPARTMENTCODE_FLD,typeof(string));
			DataColumn dcolBalancePlanning = new DataColumn(PRO_ProductionLineTable.BALANCEPLANNING_FLD,typeof(bool));
			DataColumn dcolRoundUpDaysException = new DataColumn(PRO_ProductionLineTable.ROUNDUPDAYSEXCEPTION_FLD,typeof(int));
			DataColumn dcolPlanningOffsetID = new DataColumn(PRO_PlanningOffsetTable.PLANNINGOFFSETID_FLD,typeof(int));
			DataColumn dcolOffset = new DataColumn(PRO_PlanningOffsetTable.OFFSET_FLD,typeof(decimal));
			DataColumn dcolPlanningStartDate = new DataColumn(PRO_PlanningOffsetTable.PLANNINGSTARTDATE_FLD,typeof(DateTime));
			DataColumn dcolSetMinProduce = new DataColumn("SetMinProduce",typeof(int));

			dtbWorkCenterList.Columns.Add(dcolWorkCenterID);
			dtbWorkCenterList.Columns.Add(dcolProductionLineID);
			dtbWorkCenterList.Columns.Add(dcolWorkCenterCode);
			dtbWorkCenterList.Columns.Add(dcolWorkCenterLevel);
			dtbWorkCenterList.Columns.Add(dcolWorkCenterAncessors);
			dtbWorkCenterList.Columns.Add(dcolDepartmentCode);
			dtbWorkCenterList.Columns.Add(dcolBalancePlanning);
			dtbWorkCenterList.Columns.Add(dcolRoundUpDaysException);
			dtbWorkCenterList.Columns.Add(dcolPlanningOffsetID);
			dtbWorkCenterList.Columns.Add(dcolOffset);
			dtbWorkCenterList.Columns.Add(dcolPlanningStartDate);
			dtbWorkCenterList.Columns.Add(dcolSetMinProduce);

			foreach (DataRow drowRelatedWC in dtbRelatedWC.Rows)
			{
				if (dtbWorkCenterList.Select(MST_WorkCenterTable.WORKCENTERID_FLD + "=" + drowRelatedWC[MST_WorkCenterTable.WORKCENTERID_FLD]).Length == 0)
				{
					DataRow drowWC = dtbWorkCenterList.NewRow();
					drowWC[MST_WorkCenterTable.WORKCENTERID_FLD] = drowRelatedWC[MST_WorkCenterTable.WORKCENTERID_FLD];
					drowWC[MST_WorkCenterTable.PRODUCTIONLINEID_FLD] = drowRelatedWC[MST_WorkCenterTable.PRODUCTIONLINEID_FLD];
					drowWC[WORKCENTERCODE_FLD] = drowRelatedWC[WORKCENTERCODE_FLD];
					drowWC[WORKCENTERLEVEL_FLD] = -1;
					drowWC[WORKCENTERANCESSORS_FLD] = string.Empty;
					drowWC[DEPARTMENTCODE_FLD] = drowRelatedWC[DEPARTMENTCODE_FLD];
					drowWC[PRO_ProductionLineTable.BALANCEPLANNING_FLD] = drowRelatedWC[PRO_ProductionLineTable.BALANCEPLANNING_FLD];
					drowWC[PRO_ProductionLineTable.ROUNDUPDAYSEXCEPTION_FLD] = drowRelatedWC[PRO_ProductionLineTable.ROUNDUPDAYSEXCEPTION_FLD];
					drowWC[PRO_PlanningOffsetTable.PLANNINGOFFSETID_FLD] = drowRelatedWC[PRO_PlanningOffsetTable.PLANNINGOFFSETID_FLD];
					drowWC[PRO_PlanningOffsetTable.OFFSET_FLD] = drowRelatedWC[PRO_PlanningOffsetTable.OFFSET_FLD];
					drowWC[PRO_PlanningOffsetTable.PLANNINGSTARTDATE_FLD] = drowRelatedWC[PRO_PlanningOffsetTable.PLANNINGSTARTDATE_FLD];
					drowWC["SetMinProduce"] = drowRelatedWC["SetMinProduce"];
					dtbWorkCenterList.Rows.Add(drowWC);
				}
			}
			return dtbWorkCenterList;
		}

		private const string CAPACITYBOTTLEID_FLD = "CapacityBottleID";
		private const string BOTTLEWORKINGDAY_FLD = "BottleWorkingDay";
		private const string BOTTLESTARTTIME_FLD = "BottleStartTime";
		private const string BOTTLEENDTIME_FLD = "BottleEndTime";
		private const string BOTTLETOTALCAPACITY_FLD = "BottleTotalCapacity";
		private const string BOTTLEREMAINCAPACITY_FLD = "BottleRemainCapacity";
		private const string BOTTLETOTALWORKTIME_FLD = "BottleTotalWorkTime";
		private const string BOTTLEFIRSTPRODUCEPRODUCTID_FLD = "FirstProduceProductID";

		private const string LEVEL_FLD = "Level";
		private const string HASPARENT_FLD = "HasParent";

		private const string OUTSIDE_FLD = "MAKER";

		//private decimal NOT_AVAILABLE_CAPACITY = -1;
		private decimal RESERVE_CAPACITY = 3600;

		private DataSet CreateCapacityBottles(DataTable pdtbWCList,DataTable pdtbWCConfig, DataRow pdrowDCOptionMaster, DataTable pdtbDelSch)
		{
			const int RESERVER_DAYS = 20;

			DateTime dtmAsOfDate = Convert.ToDateTime(pdrowDCOptionMaster[MTR_MPSCycleOptionMasterTable.ASOFDATE_FLD]);
			int intPlanHorizon = Convert.ToInt32(pdrowDCOptionMaster[MTR_MPSCycleOptionMasterTable.PLANHORIZON_FLD]);
			int intGroupBy = Convert.ToInt32(pdrowDCOptionMaster[MTR_MPSCycleOptionMasterTable.GROUPBY_FLD]);

			DataSet dstCapacityBottles = new DataSet();
			//Create structure
			DataTable dtbCapacityBottlesStructure = new DataTable();
			
			DataColumn dcolCapacityBottleID = new DataColumn(CAPACITYBOTTLEID_FLD,typeof(int));
			dcolCapacityBottleID.AllowDBNull = false;
			dcolCapacityBottleID.AutoIncrement = true;
			dcolCapacityBottleID.AutoIncrementSeed = 1;
			dcolCapacityBottleID.AutoIncrementStep = 1;
			dtbCapacityBottlesStructure.Columns.Add(dcolCapacityBottleID);

			DataColumn dcolBottleWorkingDay = new DataColumn(BOTTLEWORKINGDAY_FLD,typeof(DateTime));
			dtbCapacityBottlesStructure.Columns.Add(dcolBottleWorkingDay);

			DataColumn dcolBottleStartTime = new DataColumn(BOTTLESTARTTIME_FLD,typeof(DateTime));
			dtbCapacityBottlesStructure.Columns.Add(dcolBottleStartTime);

			DataColumn dcolBottleEndTime = new DataColumn(BOTTLEENDTIME_FLD,typeof(DateTime));
			dtbCapacityBottlesStructure.Columns.Add(dcolBottleEndTime);

			DataColumn dcolBottleTotalCapacity = new DataColumn(BOTTLETOTALCAPACITY_FLD,typeof(decimal));
			dtbCapacityBottlesStructure.Columns.Add(dcolBottleTotalCapacity);

			DataColumn dcolBottleRemainCapacity = new DataColumn(BOTTLEREMAINCAPACITY_FLD,typeof(decimal));
			dtbCapacityBottlesStructure.Columns.Add(dcolBottleRemainCapacity);

			DataColumn dcolBottleTotalWorkTime = new DataColumn(BOTTLETOTALWORKTIME_FLD,typeof(decimal));
			dtbCapacityBottlesStructure.Columns.Add(dcolBottleTotalWorkTime);

			DataColumn dcolBottleFirstProduceProductID = new DataColumn(BOTTLEFIRSTPRODUCEPRODUCTID_FLD,typeof(int));
			dtbCapacityBottlesStructure.Columns.Add(dcolBottleFirstProduceProductID);

			DataRow[] arrWCList = pdtbWCList.Select(string.Empty,WORKCENTERLEVEL_FLD + " ASC");
			foreach (DataRow drowWC in arrWCList)//pdtbWCList.Rows)
			{
				//determine parent 
				//check outsides
				string strDepartmentCode = drowWC[DEPARTMENTCODE_FLD].ToString();
				string strWorkCenterCode = drowWC[WORKCENTERCODE_FLD].ToString();
				int intWorkCenterId = Convert.ToInt32(drowWC[MST_WorkCenterTable.WORKCENTERID_FLD]);

				if (strDepartmentCode.StartsWith(OUTSIDE_FLD))
				{
					continue;
				}

				DataTable dtbCapacityBottles = dtbCapacityBottlesStructure.Clone();
				dtbCapacityBottles.TableName = strWorkCenterCode;
				
				DateTime dtmCurrentDay = dtmAsOfDate.Date.AddDays(-RESERVER_DAYS);

				//create bottle from before as of date N days, reserve for offset

				//First, create bottles per days				
				for (int intIdx = 0; intIdx <= intPlanHorizon + RESERVER_DAYS; intIdx++)
				{
					DataRow drowCapacityBottle = dtbCapacityBottles.NewRow();
					drowCapacityBottle[BOTTLEWORKINGDAY_FLD] = dtmCurrentDay;

					//all shifts
					DataRow[] arrShifts = pdtbWCConfig.Select(
						PRO_WCCapacityTable.WORKCENTERID_FLD + "=" + intWorkCenterId + " AND " +
						PRO_WCCapacityTable.BEGINDATE_FLD + "<='" + dtmCurrentDay + "'"  + " AND " +
						PRO_WCCapacityTable.ENDDATE_FLD + ">='" + dtmCurrentDay + "'" 
						,PRO_ShiftPatternTable.WORKTIMEFROM_FLD + " ASC");
					if (arrShifts.Length < 1)
					{
						drowCapacityBottle.Delete();
					}
					else
					{
						DateTime dtmBottleStart = Convert.ToDateTime(arrShifts[0][PRO_ShiftPatternTable.WORKTIMEFROM_FLD]);
						DateTime dtmBottleEnd = Convert.ToDateTime(arrShifts[arrShifts.Length - 1][PRO_ShiftPatternTable.WORKTIMETO_FLD]);
						int intDateDiff = dtmBottleEnd.Date.Subtract(dtmBottleStart.Date).Days;
						
						drowCapacityBottle[BOTTLESTARTTIME_FLD] = new DateTime(dtmCurrentDay.Year,dtmCurrentDay.Month,dtmCurrentDay.Day,dtmBottleStart.Hour,dtmBottleStart.Minute,dtmBottleStart.Second);
						drowCapacityBottle[BOTTLEENDTIME_FLD] = new DateTime(dtmCurrentDay.AddDays(intDateDiff).Year,dtmCurrentDay.AddDays(intDateDiff).Month,dtmCurrentDay.AddDays(intDateDiff).Day,dtmBottleEnd.Hour,dtmBottleEnd.Minute,dtmBottleEnd.Second);
						dtbCapacityBottles.Rows.Add(drowCapacityBottle);
					}
					dtmCurrentDay = dtmCurrentDay.AddDays(1);//dtmAsOfDate.Date.AddDays(intIdx);
				}
				//Second, if group by hour, divide bottles to times between 2 delivery time
				//else, normalize delivery time to start of each working day
				if (intGroupBy == (int)PlanningGroupBy.ByHour)
				{
					foreach (DataRow drowDelSch in pdtbDelSch.Rows)
					{
						DateTime dtmDelivery = Convert.ToDateTime(drowDelSch[SO_DeliveryScheduleTable.SCHEDULEDATE_FLD]);
						DataRow[] arrNearestBottles = dtbCapacityBottles.Select(BOTTLESTARTTIME_FLD + "<'" + dtmDelivery + "'",BOTTLESTARTTIME_FLD + " DESC");
						if (arrNearestBottles.Length > 0)
						{
							DataRow drowNearestBottle = arrNearestBottles[0];
							DateTime dtmBottleEndTime = Convert.ToDateTime(drowNearestBottle[BOTTLEENDTIME_FLD]);
							//if this time is inside a bottle
							//TODO: check bottle size > 1h
							if (dtmDelivery < dtmBottleEndTime)
							{
								//divide bottle
								DataRow drow1stNewBottle = dtbCapacityBottles.NewRow();
								DataRow drow2ndNewBottle = dtbCapacityBottles.NewRow();
								
								drow1stNewBottle[BOTTLESTARTTIME_FLD] = drowNearestBottle[BOTTLESTARTTIME_FLD];
								drow1stNewBottle[BOTTLEENDTIME_FLD] = dtmDelivery;
								drow1stNewBottle[BOTTLEWORKINGDAY_FLD] = drowNearestBottle[BOTTLEWORKINGDAY_FLD];

								drow2ndNewBottle[BOTTLESTARTTIME_FLD] = dtmDelivery;
								drow2ndNewBottle[BOTTLEENDTIME_FLD] = drowNearestBottle[BOTTLEENDTIME_FLD];
								drow2ndNewBottle[BOTTLEWORKINGDAY_FLD] = drowNearestBottle[BOTTLEWORKINGDAY_FLD];

								drowNearestBottle.Delete();
								dtbCapacityBottles.Rows.Add(drow1stNewBottle);
								dtbCapacityBottles.Rows.Add(drow2ndNewBottle);

								//drowDelSch[CAPACITYBOTTLEID_FLD] = drow1stNewBottle[CAPACITYBOTTLEID_FLD];
							}
							//if this time is boundary of a bottle
							if (dtmDelivery == dtmBottleEndTime)
							{
								//drowDelSch[CAPACITYBOTTLEID_FLD] = drowNearestBottle[CAPACITYBOTTLEID_FLD];
							}
							//if this time is outside a bottle
							if (dtmDelivery > dtmBottleEndTime)
							{
								//drowDelSch[SO_DeliveryScheduleTable.SCHEDULEDATE_FLD] = dtmBottleEndTime;
								//drowDelSch[CAPACITYBOTTLEID_FLD] = drowNearestBottle[CAPACITYBOTTLEID_FLD];
							}
						}
						else
						{
							//TODO : out of cycle
						}
					}
				}
				else if (intGroupBy == (int)PlanningGroupBy.ByShift)
				{
					DataRow[] arrBottles = dtbCapacityBottles.Select(string.Empty);
					foreach (DataRow drowBottle in arrBottles) 
					{
						DateTime dtmBottleStart = Convert.ToDateTime(drowBottle[BOTTLESTARTTIME_FLD]);
						DateTime dtmBottleEnd = Convert.ToDateTime(drowBottle[BOTTLESTARTTIME_FLD]);
						DateTime dtmBottleWorkingDay = Convert.ToDateTime(drowBottle[BOTTLEWORKINGDAY_FLD]);

						//select all shift configured
						DataRow[] arrShifts = pdtbWCConfig.Select(
							PRO_WCCapacityTable.WORKCENTERID_FLD + "=" + intWorkCenterId + " AND " +
							PRO_WCCapacityTable.BEGINDATE_FLD + "<='" + drowBottle[BOTTLEWORKINGDAY_FLD] + "'" + " AND " +
							PRO_WCCapacityTable.ENDDATE_FLD + ">='" + drowBottle[BOTTLEWORKINGDAY_FLD] + "'"
							,PRO_ShiftPatternTable.WORKTIMEFROM_FLD + " ASC");

                        foreach (DataRow drowShift in arrShifts)
						{
							DateTime dtmShiftStart = Convert.ToDateTime(drowShift[PRO_ShiftPatternTable.WORKTIMEFROM_FLD]);
							DateTime dtmShiftEnd = Convert.ToDateTime(drowShift[PRO_ShiftPatternTable.WORKTIMETO_FLD]);

							int intDateDiff = dtmBottleWorkingDay.Date.Subtract(dtmShiftStart.Date).Days;

							//add new bottle 
							DataRow drowNewBottle = dtbCapacityBottles.NewRow();
							drowNewBottle[BOTTLESTARTTIME_FLD] = dtmShiftStart.AddDays(intDateDiff);
							drowNewBottle[BOTTLEENDTIME_FLD] = dtmShiftEnd.AddDays(intDateDiff);
							drowNewBottle[BOTTLEWORKINGDAY_FLD] = dtmBottleWorkingDay;

							dtbCapacityBottles.Rows.Add(drowNewBottle);
						}
						//delete bottle
						drowBottle.Delete();
					}
				}
				else
				{
					//normalize delivery time
					foreach (DataRow drowDelSch in pdtbDelSch.Rows)
					{
						DateTime dtmDelivery = Convert.ToDateTime(drowDelSch[SO_DeliveryScheduleTable.SCHEDULEDATE_FLD]);
						DataRow[] arrNearestBottles = dtbCapacityBottles.Select(BOTTLEENDTIME_FLD + "<'" + dtmDelivery + "'",BOTTLESTARTTIME_FLD + " DESC");
						if (arrNearestBottles.Length > 0)
						{
							DataRow drowNearestBottle = arrNearestBottles[0];
							drowDelSch[SO_DeliveryScheduleTable.SCHEDULEDATE_FLD] = drowNearestBottle[BOTTLEENDTIME_FLD];
							//drowDelSch[CAPACITYBOTTLEID_FLD] = drowNearestBottle[CAPACITYBOTTLEID_FLD];
						}
						else
						{
							//TODO : out of cycle
						}
					}
				}

				//Calculate TotalWorkTime and TotalCapacity
				foreach (DataRow drowBottle in dtbCapacityBottles.Rows)
				{
					if (drowBottle.RowState == DataRowState.Deleted)
					{
						continue;
					}
					//all shifts
					DataRow[] arrShifts = pdtbWCConfig.Select(
						PRO_WCCapacityTable.WORKCENTERID_FLD + "=" + intWorkCenterId + " AND " +
						PRO_WCCapacityTable.BEGINDATE_FLD + "<='" + drowBottle[BOTTLEWORKINGDAY_FLD] + "'" + " AND " +
						PRO_WCCapacityTable.ENDDATE_FLD + ">='" + drowBottle[BOTTLEWORKINGDAY_FLD] + "'"
						,PRO_ShiftPatternTable.WORKTIMEFROM_FLD + " ASC");

					DateTime dtmBottleStart = Convert.ToDateTime(drowBottle[BOTTLESTARTTIME_FLD]);
					DateTime dtmBottleEnd = Convert.ToDateTime(drowBottle[BOTTLEENDTIME_FLD]);
					DateTime dtmBottleWorkingDay = Convert.ToDateTime(drowBottle[BOTTLEWORKINGDAY_FLD]);
						
					decimal decTotalWorkingTime = 0;
					foreach (DataRow drowShift in arrShifts)
					{
						DateTime dtmWorkTimeFrom = Convert.ToDateTime(drowShift[PRO_ShiftPatternTable.WORKTIMEFROM_FLD]);
						DateTime dtmWorkTimeTo = Convert.ToDateTime(drowShift[PRO_ShiftPatternTable.WORKTIMETO_FLD]);					
						int intDateDiff = dtmBottleWorkingDay.Date.Subtract(dtmWorkTimeFrom.Date).Days;

						DateTime dtmRefreshingFrom = DateTime.MinValue;
						try
						{
							dtmRefreshingFrom = Convert.ToDateTime(drowShift[PRO_ShiftPatternTable.REFRESHINGFROM_FLD]);
						}
						catch {}
						DateTime dtmRefreshingTo = DateTime.MinValue;
						try
						{
							dtmRefreshingTo = Convert.ToDateTime(drowShift[PRO_ShiftPatternTable.REFRESHINGTO_FLD]);
						}
						catch {}
						DateTime dtmRegularStopFrom = DateTime.MinValue;
						try
						{
							dtmRegularStopFrom = Convert.ToDateTime(drowShift[PRO_ShiftPatternTable.REGULARSTOPFROM_FLD]);
						}
						catch {}
						DateTime dtmRegularStopTo = DateTime.MinValue; 
						try
						{
							dtmRegularStopTo = Convert.ToDateTime(drowShift[PRO_ShiftPatternTable.REGULARSTOPTO_FLD]);
						}
						catch {}
						DateTime dtmExtraStopFrom = DateTime.MinValue; 
						try
						{
							dtmExtraStopFrom = Convert.ToDateTime(drowShift[PRO_ShiftPatternTable.EXTRASTOPFROM_FLD]);
						}
						catch {}
						DateTime dtmExtraStopTo = DateTime.MinValue; 
						try
						{
							dtmExtraStopTo = Convert.ToDateTime(drowShift[PRO_ShiftPatternTable.EXTRASTOPTO_FLD]);
						}
						catch {}
						
						//move to bottle workingday
						dtmWorkTimeFrom = dtmWorkTimeFrom.AddDays(intDateDiff);
						dtmWorkTimeTo = dtmWorkTimeTo.AddDays(intDateDiff);
						dtmRefreshingFrom = dtmRefreshingFrom.AddDays(intDateDiff);
						dtmRefreshingTo = dtmRefreshingTo.AddDays(intDateDiff);
						dtmRegularStopFrom = dtmRegularStopFrom.AddDays(intDateDiff);
						dtmRegularStopTo = dtmRegularStopTo.AddDays(intDateDiff);
						dtmExtraStopFrom = dtmExtraStopFrom.AddDays(intDateDiff);
						dtmExtraStopTo = dtmExtraStopTo.AddDays(intDateDiff);

						DateTime dtmStart;
						DateTime dtmEnd;
						decimal decTotalSeconds;
						
						//worktime
						dtmStart = (dtmWorkTimeFrom > dtmBottleStart) ? dtmWorkTimeFrom : dtmBottleStart;
						dtmEnd = (dtmWorkTimeTo < dtmBottleEnd) ? dtmWorkTimeTo : dtmBottleEnd;
						decTotalSeconds = Convert.ToDecimal(dtmEnd.Subtract(dtmStart).TotalSeconds);
						if (decTotalSeconds > 0)
						{
							decTotalWorkingTime += decTotalSeconds;
						}

						//refreshing
						dtmStart = (dtmRefreshingFrom > dtmBottleStart) ? dtmRefreshingFrom : dtmBottleStart;
						dtmEnd = (dtmRefreshingTo < dtmBottleEnd) ? dtmRefreshingTo : dtmBottleEnd;
						decTotalSeconds = Convert.ToDecimal(dtmEnd.Subtract(dtmStart).TotalSeconds);
						if (decTotalSeconds > 0)
						{
							decTotalWorkingTime -= decTotalSeconds;
						}

						//regular stop
						dtmStart = (dtmRegularStopFrom > dtmBottleStart) ? dtmRegularStopFrom : dtmBottleStart;
						dtmEnd = (dtmRegularStopTo < dtmBottleEnd) ? dtmRegularStopTo : dtmBottleEnd;
						decTotalSeconds = Convert.ToDecimal(dtmEnd.Subtract(dtmStart).TotalSeconds);
						if (decTotalSeconds > 0)
						{
							decTotalWorkingTime -= decTotalSeconds;
						}

						//extra stop
						dtmStart = (dtmExtraStopFrom > dtmBottleStart) ? dtmExtraStopFrom : dtmBottleStart;
						dtmEnd = (dtmExtraStopTo < dtmBottleEnd) ? dtmExtraStopTo : dtmBottleEnd;
						decTotalSeconds = Convert.ToDecimal(dtmEnd.Subtract(dtmStart).TotalSeconds);
						if (decTotalSeconds > 0)
						{
							decTotalWorkingTime -= decTotalSeconds;
						}						
					}
					decimal decTotalDayCapacity = Convert.ToDecimal(arrShifts[0][PRO_WCCapacityTable.CAPACITY_FLD]);
					int intWCType = Convert.ToInt32(arrShifts[0][PRO_WCCapacityTable.WCTYPE_FLD]);
					decimal decCrewSize = Convert.ToDecimal(arrShifts[0][PRO_WCCapacityTable.CREWSIZE_FLD]);
					decimal decMachineNo = Convert.ToDecimal(arrShifts[0][PRO_WCCapacityTable.MACHINENO_FLD]);
					decimal decFactor = Convert.ToDecimal(arrShifts[0][PRO_WCCapacityTable.FACTOR_FLD]);
					decimal decTotalDayWorkingTime = 0;
					if (intWCType == (int)WCType.Labor)
					{
						decTotalDayWorkingTime = decTotalDayCapacity / (decCrewSize * decFactor /100);
					}
					else if (intWCType == (int)WCType.Machine)
					{
						decTotalDayWorkingTime = decTotalDayCapacity / (decMachineNo * decFactor /100);
					}
					drowBottle[BOTTLETOTALWORKTIME_FLD] = decTotalWorkingTime;
					if (decTotalWorkingTime != 0)
					{
						drowBottle[BOTTLETOTALCAPACITY_FLD] = Math.Round((decTotalWorkingTime/decTotalDayWorkingTime)*decTotalDayCapacity);
					}
					else
					{
						drowBottle[BOTTLETOTALCAPACITY_FLD] = 0;
					}
					drowBottle[BOTTLEREMAINCAPACITY_FLD] = drowBottle[BOTTLETOTALCAPACITY_FLD];
				}

				dstCapacityBottles.Tables.Add(dtbCapacityBottles);
			}
			return dstCapacityBottles;
		}

		private int AdjustDeliveryAndBottles(DataRow pdrowDCOptionMaster, DataSet pdstCapacityBottles,DataTable pdtbDelSch,
			DataTable pdtbWCList,DataTable pdtbChangeCategory, DataTable pdtbBOM, DataSet pdstDCPResult,
			DataTable pdtbWCConfig, DataTable pdtbProduct, DataTable pdtbAvailQty, DataTable pdtbRawResult,
			DataTable pdtbProductionGroup, DataTable pdtbProductPair, ref DataTable pdtbBeginData)
		{
			//prepare datas
			#region Preparing
			bool blnIncludeCheckpoint = Convert.ToBoolean(pdrowDCOptionMaster[PRO_DCOptionMasterTable.INCLUDECHECKPOINT_FLD]);
			bool blnOnhand = Convert.ToBoolean(pdrowDCOptionMaster[PRO_DCOptionMasterTable.ONHAND_FLD]);
			bool blnSafetyStock = Convert.ToBoolean(pdrowDCOptionMaster[PRO_DCOptionMasterTable.SAFETYSTOCK_FLD]);
			int intDCOptionMasterID = Convert.ToInt32(pdrowDCOptionMaster[PRO_DCOptionMasterTable.DCOPTIONMASTERID_FLD]);
			int intCCNID = Convert.ToInt32(pdrowDCOptionMaster[PRO_DCOptionMasterTable.CCNID_FLD]);
			DateTime dtmAsOfDate = Convert.ToDateTime(pdrowDCOptionMaster[MTR_MPSCycleOptionMasterTable.ASOFDATE_FLD]);
			int intPlanHorizon = Convert.ToInt32(pdrowDCOptionMaster[MTR_MPSCycleOptionMasterTable.PLANHORIZON_FLD]);

			DateTime dtmDBDate = (new PCSComUtils.Common.BO.UtilsBO()).GetDBDate();//.Date;
			PCSComMaterials.Plan.BO.MPSRegenerationProcessBO boMPSRegeneration = new PCSComMaterials.Plan.BO.MPSRegenerationProcessBO();

			//DataTable dtbRawResult = CreateRawResultTable();

			DataSet dstFuturePOs = new DataSet();
			DataSet dstFutureSupplyWOs = new DataSet();
			DataSet dstFutureDemandWOs = new DataSet();
			DataSet dstFutureSOs = new DataSet();

			//cache all future WO & PO by workcenter
			DataTable dtbFutureSOs = new DataTable();
			DataTable dtbFuturePOs = new DataTable();
			DataTable dtbFutureSupplyWOs = new DataTable();		
			DataTable dtbFutureDemandWOs = new DataTable();
			
			/*GetDemandAndSupply(intCCNID,dtmDBDate,dtmAsOfDate,ref dtbFutureSOs,ref dtbFuturePOs,ref dtbFutureSupplyWOs,ref dtbFutureDemandWOs,boMPSRegeneration,0);
			
			dtbFuturePOs.TableName = dtmDBDate.ToString();
			dstFuturePOs.Tables.Add(dtbFuturePOs);
			dtbFutureSOs.TableName = dtmDBDate.ToString();
			dstFutureSOs.Tables.Add(dtbFutureSOs);
			dtbFutureSupplyWOs.TableName = dtmDBDate.ToString();
			dstFutureSupplyWOs.Tables.Add(dtbFutureSupplyWOs);
			dtbFutureDemandWOs.TableName = dtmDBDate.ToString();
			dstFutureDemandWOs.Tables.Add(dtbFutureDemandWOs);*/

			//Cache current available quantity
			DateTime dtmCurrentDay = dtmAsOfDate;
			/*for (int intIdx = 0; intIdx <= intPlanHorizon; intIdx++)
			{
				dtbFutureSOs = new DataTable();
				dtbFuturePOs = new DataTable();
				dtbFutureSupplyWOs = new DataTable();			
				dtbFutureDemandWOs = new DataTable();
				GetDemandAndSupply(intCCNID,dtmCurrentDay,dtmCurrentDay.AddDays(1).AddSeconds(-1),ref dtbFutureSOs,ref dtbFuturePOs,ref dtbFutureSupplyWOs,ref dtbFutureSupplyWOs,boMPSRegeneration);
				dtbFuturePOs.TableName = dtmCurrentDay.ToString();
				dstFuturePOs.Tables.Add(dtbFuturePOs);
				dtbFutureSupplyWOs.TableName = dtmCurrentDay.ToString();
				dstFutureSupplyWOs.Tables.Add(dtbFutureSupplyWOs);

				dtmCurrentDay = dtmCurrentDay.AddDays(1);
			}*/
			#endregion Preparing

			//walk through workcenter base on level
			DataRow[] arrWCList = pdtbWCList.Select(string.Empty,WORKCENTERLEVEL_FLD + " ASC");
			foreach (DataRow drowWC in arrWCList)
			{
				//prepare datas
				#region Preparing
				string strDepartmentCode = drowWC[DEPARTMENTCODE_FLD].ToString();
				string strWorkCenterCode = drowWC[WORKCENTERCODE_FLD].ToString();
				int intWorkCenterID = Convert.ToInt32(drowWC[MST_WorkCenterTable.WORKCENTERID_FLD]);
				bool blnOutside = (strDepartmentCode.StartsWith(OUTSIDE_FLD));

				bool blnBalancePlanning = Convert.ToBoolean(drowWC["BalancePlanning"]); //temporary hard code
				drowWC[PRO_PlanningOffsetTable.PLANNINGSTARTDATE_FLD] = DBNull.Value;//dtmAsOfDate.AddDays(1);

				//calculate actual AsOfDate
				string strParentWC = drowWC[WORKCENTERANCESSORS_FLD].ToString();
				if (strParentWC != string.Empty) 
				{
					//strParentWC.ToString();
					char[] arrSep = {','};
					string[] arrParentWC = strParentWC.Split(arrSep);
					foreach (string strWC in arrParentWC)
					{
						if (strWC != string.Empty)
						{
							DataRow drowParentWC = pdtbWCList.Select(MST_WorkCenterTable.WORKCENTERID_FLD + "=" + strWC)[0];
							if (blnOutside)// || (drowParentWC[DEPARTMENTCODE_FLD].ToString() == OUTSIDE_FLD)) 
							{
								//drowWC[PRO_PlanningOffsetTable.PLANNINGSTARTDATE_FLD] = dtmAsOfDate;
								if ((drowWC[PRO_PlanningOffsetTable.PLANNINGSTARTDATE_FLD] == DBNull.Value) || (Convert.ToDateTime(drowWC[PRO_PlanningOffsetTable.PLANNINGSTARTDATE_FLD]) > Convert.ToDateTime(drowParentWC[PRO_PlanningOffsetTable.PLANNINGSTARTDATE_FLD])))
								{
									drowWC[PRO_PlanningOffsetTable.PLANNINGSTARTDATE_FLD] = drowParentWC[PRO_PlanningOffsetTable.PLANNINGSTARTDATE_FLD];
								}
								continue;
							}
							DateTime dtmParentWCAsOfDate = Convert.ToDateTime(drowParentWC[PRO_PlanningOffsetTable.PLANNINGSTARTDATE_FLD]);

							try
							{
								int intOffset = Convert.ToInt32(drowWC[PRO_PlanningOffsetTable.OFFSET_FLD]);
								//determine base bottle
								DataRow[] arrBaseBottles = null;
								arrBaseBottles = pdstCapacityBottles.Tables[strWorkCenterCode].Select(BOTTLEENDTIME_FLD + " > '" + dtmParentWCAsOfDate + "'",BOTTLESTARTTIME_FLD + " ASC");
								DataRow[] arrAllBottles = pdstCapacityBottles.Tables[strWorkCenterCode].Select(string.Empty,BOTTLESTARTTIME_FLD + " ASC");
								if (arrBaseBottles.Length < 1) 
								{
									1.ToString();
									//error, never occurs
								}
								int intStartIdx = Array.IndexOf(arrAllBottles,arrBaseBottles[0]);
								if (intStartIdx - intOffset < 0)
								{
									intOffset = intStartIdx;
									return 1;
								}
								DataRow drowStart = pdstCapacityBottles.Tables[strWorkCenterCode].Rows[intStartIdx - intOffset];
								if ((drowWC[PRO_PlanningOffsetTable.PLANNINGSTARTDATE_FLD] == DBNull.Value) || (Convert.ToDateTime(drowWC[PRO_PlanningOffsetTable.PLANNINGSTARTDATE_FLD]) > Convert.ToDateTime(drowStart[BOTTLESTARTTIME_FLD])))
								{
									drowWC[PRO_PlanningOffsetTable.PLANNINGSTARTDATE_FLD] = drowStart[BOTTLESTARTTIME_FLD];
								}
							}
							catch
							{
								Logger.LogMessage("Error WorkCenter: " + strWorkCenterCode, string.Empty, Level.DEBUG);
								throw new PCSBOException(ErrorCode.MESSAGE_ERROR_WORKCENTER, strWorkCenterCode, new Exception());
							}
						}
					}
				}
				else
				{
					try
					{
						int intOffset = Convert.ToInt32(drowWC[PRO_PlanningOffsetTable.OFFSET_FLD]);
						//determine base bottle
						DataRow[] arrBaseBottles = pdstCapacityBottles.Tables[strWorkCenterCode].Select(BOTTLESTARTTIME_FLD + " >= '" + dtmAsOfDate + "'",BOTTLESTARTTIME_FLD + " ASC");
						DataRow[] arrAllBottles = pdstCapacityBottles.Tables[strWorkCenterCode].Select(string.Empty,BOTTLESTARTTIME_FLD + " ASC");
						if (arrBaseBottles.Length < 1) 
						{
							1.ToString();
							Debug.WriteLine(dtmAsOfDate.ToString());
							//error, never occurs
							// Neu say ra loi nay tuc la chua setup WCCapacity
                        }
                        #region cuonglv
                        if (arrAllBottles.Length > 0 && arrBaseBottles.Length > 0)
                        {
                            int intStartIdx = Array.IndexOf(arrAllBottles, arrBaseBottles[0]);
                            DataRow drowStart = pdstCapacityBottles.Tables[strWorkCenterCode].Rows[intStartIdx - intOffset];
                            drowWC[PRO_PlanningOffsetTable.PLANNINGSTARTDATE_FLD] = drowStart[BOTTLESTARTTIME_FLD];
                        }
                        #endregion
                        //int intStartIdx = Array.IndexOf(arrAllBottles, arrBaseBottles[0]);
                        //DataRow drowStart = pdstCapacityBottles.Tables[strWorkCenterCode].Rows[intStartIdx - intOffset];
                        //drowWC[PRO_PlanningOffsetTable.PLANNINGSTARTDATE_FLD] = drowStart[BOTTLESTARTTIME_FLD];
					}
					catch
					{
						Logger.LogMessage("Error WorkCenter: " + strWorkCenterCode, string.Empty, Level.DEBUG);
						throw new PCSBOException(ErrorCode.MESSAGE_ERROR_WORKCENTER, strWorkCenterCode, new Exception());
					}
				}
				
				DateTime dtmPlanningStartDate = new DateTime();
				if (!blnOutside)
				{
					try
					{
						dtmPlanningStartDate = Convert.ToDateTime(drowWC[PRO_PlanningOffsetTable.PLANNINGSTARTDATE_FLD]);
					}
					catch
					{
						dtmPlanningStartDate = dtmAsOfDate;
					}
				}
				else
				{
					dtmPlanningStartDate = dtmAsOfDate;
				}

				if (dtmDBDate >= dtmPlanningStartDate) 
				{
					return 1;
				}
				dtmPlanningStartDate = dtmPlanningStartDate.AddMinutes(375);

				dtbFutureSOs = new DataTable();
				dtbFuturePOs = new DataTable();
				dtbFutureSupplyWOs = new DataTable();		
				dtbFutureDemandWOs = new DataTable();			
				GetDemandAndSupply(intCCNID,dtmDBDate,dtmPlanningStartDate,ref dtbFutureSOs,ref dtbFuturePOs,ref dtbFutureSupplyWOs,ref dtbFutureDemandWOs,boMPSRegeneration,intWorkCenterID);
				
				if (dstFuturePOs.Tables[intWorkCenterID.ToString()] == null)
				{
					dtbFuturePOs.TableName = intWorkCenterID.ToString();
					dstFuturePOs.Tables.Add(dtbFuturePOs);
				}
				else
				{
					1.ToString();
				}
				if (dstFutureSOs.Tables[intWorkCenterID.ToString()] == null)
				{
					dtbFutureSOs.TableName = intWorkCenterID.ToString();
					dstFutureSOs.Tables.Add(dtbFutureSOs);
				}
				else
				{
					1.ToString();
				}
				if (dstFutureSupplyWOs.Tables[intWorkCenterID.ToString()] == null)
				{
					dtbFutureSupplyWOs.TableName = intWorkCenterID.ToString();
					dstFutureSupplyWOs.Tables.Add(dtbFutureSupplyWOs);
				}
				else
				{
					1.ToString();
				}
				if (dstFutureDemandWOs.Tables[intWorkCenterID.ToString()] == null)
				{
					dtbFutureDemandWOs.TableName = intWorkCenterID.ToString();
					dstFutureDemandWOs.Tables.Add(dtbFutureDemandWOs);
				}
				else
				{
					1.ToString();
				}

				#endregion Preparing

				//adjust delivery quantity base on inventory
				#region Onhand-Produce adjust
				DataRow[] arrDeliveries;// = pdtbDelSch.Select(MST_WorkCenterTable.WORKCENTERID_FLD + "=" + intWorkCenterID);
				DataRow[] arrProducts = pdtbProduct.Select(MST_WorkCenterTable.WORKCENTERID_FLD + "=" + intWorkCenterID);
				//foreach product
				if (blnOnhand) 
				{
					foreach (DataRow drowProduct in arrProducts)
					{
						//dtbFutureSOs = new DataTable();
						//dtbFuturePOs = new DataTable();
						//dtbFutureSupplyWOs = new DataTable();

						int intProductID = Convert.ToInt32(drowProduct[ITM_ProductTable.PRODUCTID_FLD]);

						//GetDemandAndSupply(intCCNID,dtmDBDate,dtmAsOfDate,ref dtbFutureSOs,ref dtbFuturePOs,ref dtbFutureSupplyWOs,boMPSRegeneration);
						System.Diagnostics.Debug.WriteLine(dtmPlanningStartDate);
						dtbFuturePOs = dstFuturePOs.Tables[intWorkCenterID.ToString()];
						dtbFutureSupplyWOs = dstFutureSupplyWOs.Tables[intWorkCenterID.ToString()];
						dtbFutureDemandWOs = dstFutureDemandWOs.Tables[intWorkCenterID.ToString()];
						dtbFutureSOs = dstFutureSOs.Tables[intWorkCenterID.ToString()];

						//TODO : change from asofdate to planning startdate
						bool blnUseCacheAsBegin = Convert.ToBoolean(pdrowDCOptionMaster[PRO_DCOptionMasterTable.USECACHE_ASBEGIN_FLD]);
						decimal decStock = GetOnHandAtAsOfDate(intProductID,dtbFutureSOs,dtbFuturePOs,dtbFutureSupplyWOs,dtbFutureDemandWOs,true,pdtbAvailQty,dtmPlanningStartDate,blnUseCacheAsBegin);

						//first of all, subtract for over quantity
						//select all parent
						DataRow[] arrParent = pdtbBOM.Select(ITM_BOMTable.COMPONENTID_FLD + " = " + intProductID.ToString());
						decimal decTotalParentOverQuantity = 0;
						foreach (DataRow drowParent in arrParent) 
						{
							int intParentProductID = Convert.ToInt32(drowParent[ITM_ProductTable.PRODUCTID_FLD]);
							decimal decShrink = Convert.ToDecimal(drowParent[ITM_BOMTable.SHRINK_FLD]);
							decimal decBOMQty = Convert.ToDecimal(drowParent[ITM_BOMTable.QUANTITY_FLD]);
							//get over quantity
							object objResult = pdtbDelSch.Compute("SUM(" + SO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD + ")",ITM_ProductTable.PRODUCTID_FLD + " = " + intParentProductID.ToString() + " AND " + CAPACITYBOTTLEID_FLD + " < 0");
							decimal decParentOverQuantity = Convert.ToDecimal(objResult == DBNull.Value ? 0 : objResult);
							//drowChildProduct[ITM_BOMTable.QUANTITY_FLD]) * decCurrentQuantity / (1-decShrink/100)
							decTotalParentOverQuantity += decParentOverQuantity * (1-decShrink / 100) / decBOMQty;
						}
						decStock -= decTotalParentOverQuantity;
						if (decStock < 0)
						{
							decStock = 0;
						}

						#region dungla 25-09-2006: save begin data for other purpose

						DataRow[] drowExist = pdtbBeginData.Select(ITM_ProductTable.PRODUCTID_FLD + "=" + intProductID
							+ " AND " + DCP_BeginQuantityTable.DCOPTIONMASTERID_FLD + "=" + pdrowDCOptionMaster[DCP_BeginQuantityTable.DCOPTIONMASTERID_FLD].ToString());
						// already in database, just need to update only
						if (drowExist != null && drowExist.Length > 0)
							drowExist[0][DCP_BeginQuantityTable.QUANTITY_FLD] = decStock;
						else
						{
							// make new record
							DataRow drowBeginData = pdtbBeginData.NewRow();
							drowBeginData[DCP_BeginQuantityTable.DCOPTIONMASTERID_FLD] = pdrowDCOptionMaster[DCP_BeginQuantityTable.DCOPTIONMASTERID_FLD];
							drowBeginData[DCP_BeginQuantityTable.PRODUCTID_FLD] = drowProduct[ITM_ProductTable.PRODUCTID_FLD];
							drowBeginData[DCP_BeginQuantityTable.QUANTITY_FLD] = decStock;
							pdtbBeginData.Rows.Add(drowBeginData);
						}

						#endregion

						//forward day by day
						dtmCurrentDay = dtmPlanningStartDate;//dtmAsOfDate;
						for (int intIdx = 0; intIdx <= intPlanHorizon + (dtmPlanningStartDate.Subtract(dtmAsOfDate)).TotalDays ; intIdx++)
						{
							//get all deliveries
							arrDeliveries = pdtbDelSch.Select(SO_SaleOrderDetailTable.PRODUCTID_FLD + "=" + intProductID + " AND " + MST_WorkCenterTable.WORKCENTERID_FLD + "=" + intWorkCenterID + " AND " + SO_DeliveryScheduleTable.SCHEDULEDATE_FLD + ">='" + dtmCurrentDay + "' AND " + SO_DeliveryScheduleTable.SCHEDULEDATE_FLD + "<='" +dtmCurrentDay.AddDays(1) + "'");
							string strFilter = MST_WorkCenterTable.WORKCENTERID_FLD + "=" + intWorkCenterID 
								+ " AND " + SO_DeliveryScheduleTable.SCHEDULEDATE_FLD + ">='" + dtmCurrentDay.ToString() 
								+ "' AND " + SO_DeliveryScheduleTable.SCHEDULEDATE_FLD + "<='" + dtmCurrentDay.AddDays(1).ToString() + "'";
							
							//object objResult = pdtbDelSch.Compute("SUM(DeliveryQuantity)",strFilter);
							//decimal decTotalDeliver = Convert.ToDecimal(objResult == DBNull.Value ? 0 : objResult);

							//decimal decTotalDeliver = Decimal.One;

							/*//GetDemandAndSupply(intCCNID,dtmCurrentDay,dtmCurrentDay.AddDays(1).AddSeconds(-1),ref dtbFutureSOs,ref dtbFuturePOs,ref dtbFutureSupplyWOs,boMPSRegeneration);
							dtbFuturePOs = dstFuturePOs.Tables[dtmCurrentDay.ToString()];
							dtbFutureSupplyWOs = dstFutureSupplyWOs.Tables[dtmCurrentDay.ToString()];

							objResult = dtbFuturePOs.Compute("SUM(" + PO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD + ")",PO_PurchaseOrderDetailTable.PRODUCTID_FLD + "=" + intProductID);
							decimal decTotalPurchase = Convert.ToDecimal(objResult == DBNull.Value ? 0 : objResult);
							//decimal decTotalPurchase = decimal.MinusOne;
							objResult = dtbFutureSupplyWOs.Compute("SUM(" + PRO_WorkOrderDetailTable.ORDERQUANTITY_FLD + ")",PRO_WorkOrderDetailTable.PRODUCTID_FLD + "=" + intProductID);
							decimal decTotalProduce =  Convert.ToDecimal(objResult == DBNull.Value ? 0 : objResult);
							//decimal decTotalProduce =  Decimal.One;*/

							decimal decTotalSupply = 0;//decTotalPurchase + decTotalProduce;
							decStock += decTotalSupply;
							if (decStock > 0)
							{
								//reduce delivery
								foreach (DataRow drowDelivery in arrDeliveries)
								{
									decimal decDeliveryQty = Convert.ToDecimal(drowDelivery[SO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD]);
									//decimal decDeliveryQty = decimal.One;

									if (decDeliveryQty < decStock)
									{
										drowDelivery[SO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD] = 0;
										//drowDelivery.Delete();
										decStock -= decDeliveryQty;
									}
									else
									{
										drowDelivery[SO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD] = decDeliveryQty - decStock;
										decStock = 0;
										break;
									}
								}
							}
							else
							{
								decStock = 0;
								break;
							}
							dtmCurrentDay = dtmCurrentDay.AddDays(1);
						}
						decimal decSafetyStock = Convert.ToDecimal(drowProduct[ITM_ProductTable.SAFETYSTOCK_FLD]);
						if (decStock > decSafetyStock)
						{
							drowProduct[ITM_ProductTable.SAFETYSTOCK_FLD] = 0;
						}
						else
						{
							drowProduct[ITM_ProductTable.SAFETYSTOCK_FLD] = decSafetyStock - decStock;
						}
					}
				}
				#endregion Onhand-Produce Adjust

				//feed bottles
				#region Assign deliveries to bottles
				if (!blnOutside)
				{
					#region Inside processing
					DataRow[] arrCapacityBottles = pdstCapacityBottles.Tables[strWorkCenterCode].Select(string.Empty,BOTTLESTARTTIME_FLD + " DESC");
					int intIdx = 0;
					foreach (DataRow drowBottle in arrCapacityBottles)
					{
						intIdx ++;
						int intCapacityBottleID = Convert.ToInt32(drowBottle[CAPACITYBOTTLEID_FLD]);
						decimal decBottleTotalCapacity = Convert.ToDecimal(drowBottle[BOTTLETOTALCAPACITY_FLD]);
						decimal decBottleTotalWorkingTime = Convert.ToDecimal(drowBottle[BOTTLETOTALWORKTIME_FLD]);
                        DateTime dtmBottleStartTime = Convert.ToDateTime(drowBottle[BOTTLESTARTTIME_FLD]);
                       
						DateTime dtmBottleEndTime = Convert.ToDateTime(drowBottle[BOTTLEENDTIME_FLD]);
						DateTime dtmBottleWorkingDay = Convert.ToDateTime(drowBottle[BOTTLEWORKINGDAY_FLD]);
						System.Diagnostics.Trace.WriteLine("Start : " + dtmBottleStartTime.ToString() + " | End : " + dtmBottleEndTime.ToString());

						//check bottle startdate with as of date
						if (dtmBottleStartTime < dtmPlanningStartDate)//dtmAsOfDate)
						{
							intIdx ++;
							continue;
						}

						//select all delivery
						arrDeliveries = pdtbDelSch.Select(CAPACITYBOTTLEID_FLD + " <= 0 " +
							" AND " + SO_DeliveryScheduleTable.SCHEDULEDATE_FLD + " >= '" + dtmBottleEndTime + "'" +
							" AND " + MST_WorkCenterTable.WORKCENTERID_FLD + " = " + intWorkCenterID +
							" AND " + SO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD + " > 0",
							//ITM_ProductTable.ORDERQUANTITYMULTIPLE_FLD + "," +
							ITM_ProductTable.REVISION_FLD + "," +
							//SO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD + "," +
							SO_SaleOrderDetailTable.PRODUCTID_FLD);

						#region Calculate produce quantity and order
						if (arrDeliveries.Length > 0)
						{
							//calculate require capacity
							decimal decRequiredCapacity = 0;
							int intSamplePattern = -1;
							decimal decSampleRate = 0;
							decimal decDelayTime = 0;
							foreach (DataRow drowDelivery in arrDeliveries)
							{
								drowDelivery[CAPACITYBOTTLEID_FLD] = intCapacityBottleID;
								decimal decTotalQuantity = Convert.ToDecimal(drowDelivery[SO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD]);
								
								//calculate scrapt percent
								decimal decScraptPercent = Convert.ToDecimal(drowDelivery[ITM_ProductTable.SCRAPPERCENT_FLD]);
								decTotalQuantity = Decimal.Round(decTotalQuantity / (1 - decScraptPercent/100),0);

								decimal decLeadTime = Convert.ToDecimal(drowDelivery[LEADTIME_FLD]);
								decimal decTotalLeadTime = decLeadTime * decTotalQuantity;
								decimal decTotalRealTime = decTotalLeadTime * (decBottleTotalWorkingTime / decBottleTotalCapacity);
								decimal decCheckpoint = Convert.ToDecimal(drowDelivery[CHECKPOINTPERITEM_FLD]);
								decimal decCheckpointTime = 0;
								if (blnIncludeCheckpoint)
								{
									decCheckpointTime = Math.Round(decTotalQuantity * decCheckpoint);
								}
								else
								{
									decCheckpointTime = 0;
								}
								decRequiredCapacity += decTotalLeadTime + decCheckpointTime;
							}

							//find optimized order
							//add calculate change category time
							ArrayList arrProductID = new ArrayList();
							ArrayList arrDeliveryQuantities = new ArrayList();
							ArrayList arrProduceQuantities = new ArrayList();
							ArrayList arrDeliveryRows = new ArrayList();
							ArrayList arrFinalOrder = new ArrayList();

							foreach (DataRow drowDelivery in arrDeliveries)
							{
								if (!arrProductID.Contains(drowDelivery[ITM_ProductTable.PRODUCTID_FLD]))
								{
									arrProductID.Add(drowDelivery[ITM_ProductTable.PRODUCTID_FLD]);
									arrDeliveryRows.Add(drowDelivery);
									arrDeliveryQuantities.Add(drowDelivery[SO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD]);
									arrProduceQuantities.Add(0);
								}
								else
								{
									int intArrayIdx = arrProductID.IndexOf(drowDelivery[ITM_ProductTable.PRODUCTID_FLD]);
									arrDeliveryQuantities[intArrayIdx] = Convert.ToDecimal(drowDelivery[SO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD]) + Convert.ToDecimal(arrDeliveryQuantities[intArrayIdx]);
								}
							}

							//build pair product delivery lookup table
							Hashtable htPairQuantity = new Hashtable();
							Hashtable htPair = new Hashtable();
							for (int i = 0; i < arrProductID.Count; i++)
							{
								int intProductID = Convert.ToInt32(arrProductID[i]);
								//find its pair
								DataRow[] arrPairProduct = pdtbProductPair.Select("ProductID1 = " + intProductID);
								if (arrPairProduct.Length > 0)
								{
									int intPairProductID = Convert.ToInt32(arrPairProduct[0]["ProductID2"]);
									htPairQuantity[intPairProductID] = arrDeliveryQuantities[i];
									htPair[intPairProductID] = intProductID;
								}
							}

							//Next bottle first produced item
							DataRow[] arrNextBottles = pdstCapacityBottles.Tables[strWorkCenterCode].Select(BOTTLESTARTTIME_FLD + ">='" + dtmBottleEndTime + "'",BOTTLESTARTTIME_FLD + " ASC");
							int intNextBottleFirstProducedProductID = -1;

							#region re-sort
							for (int i = 0; i < arrProductID.Count; i++)
							{
								DataRow drow1stProduct = pdtbProduct.Select(ITM_ProductTable.PRODUCTID_FLD + "=" + arrProductID[i])[0];
								decimal decSortPGPriority1st = Convert.ToDecimal(drow1stProduct[PGPRIORITY_FLD]);
								decimal decSortDeliveryQty1st = Convert.ToDecimal(arrDeliveryQuantities[i]);
								//compare to its pair
								decimal decPairDeliveryQty1st = Convert.ToDecimal(htPairQuantity[arrProductID[i]]);
								if (decPairDeliveryQty1st > decSortDeliveryQty1st)
								{
									decSortDeliveryQty1st = decPairDeliveryQty1st;
								}

								for (int j = i + 1; j < arrProductID.Count; j++)
								{
									DataRow drow2ndProduct = pdtbProduct.Select(ITM_ProductTable.PRODUCTID_FLD + "=" + arrProductID[j])[0];

									//sort value
									decimal decSortPGPriority2nd = Convert.ToDecimal(drow2ndProduct[PGPRIORITY_FLD]);
									decimal decSortDeliveryQty2nd = Convert.ToDecimal(arrDeliveryQuantities[j]);
									//compare to its pair
									decimal decPairDeliveryQty2nd = Convert.ToDecimal(htPairQuantity[arrProductID[j]]);
									if (decPairDeliveryQty2nd > decSortDeliveryQty2nd)
									{
										decSortDeliveryQty2nd = decPairDeliveryQty2nd;
									}


									//find delivery quantity of the product with same model
									//DataRow drowSameModel2nd = arrDeliveryRows.

									//first sort by group priority
									if (decSortPGPriority2nd > decSortPGPriority1st)
									{
										//swap
										int intTmp = Convert.ToInt32(arrProductID[i]);
										arrProductID[i] = arrProductID[j];
										arrProductID[j] = intTmp;

										DataRow drowTmp = (DataRow)arrDeliveryRows[i];
										arrDeliveryRows[i] = arrDeliveryRows[j];
										arrDeliveryRows[j] = drowTmp;

										decimal decTmp = Convert.ToDecimal(arrDeliveryQuantities[i]);
										arrDeliveryQuantities[i] = arrDeliveryQuantities[j];
										arrDeliveryQuantities[j] = decTmp;

										decSortPGPriority1st = decSortPGPriority2nd;
										decSortDeliveryQty1st = decSortDeliveryQty2nd;
									} 
									//second sort by delivery quantity
									else if ((decSortPGPriority2nd == decSortPGPriority1st) && (decSortDeliveryQty2nd < decSortDeliveryQty1st))
									{
										//swap
										int intTmp = Convert.ToInt32(arrProductID[i]);
										arrProductID[i] = arrProductID[j];
										arrProductID[j] = intTmp;

										DataRow drowTmp = (DataRow)arrDeliveryRows[i];
										arrDeliveryRows[i] = arrDeliveryRows[j];
										arrDeliveryRows[j] = drowTmp;

										decimal decTmp = Convert.ToDecimal(arrDeliveryQuantities[i]);
										arrDeliveryQuantities[i] = arrDeliveryQuantities[j];
										arrDeliveryQuantities[j] = decTmp;

										decSortPGPriority1st = decSortPGPriority2nd;
										decSortDeliveryQty1st = decSortDeliveryQty2nd;
									} 
									//and last sort by multiple quantity - not needed ???
									else if ((decSortPGPriority2nd == decSortPGPriority1st) && (decSortDeliveryQty2nd == decSortDeliveryQty1st) && (Convert.ToDecimal(((DataRow)arrDeliveryRows[j])[ITM_ProductTable.ORDERQUANTITYMULTIPLE_FLD]) < Convert.ToDecimal(((DataRow)arrDeliveryRows[i])[ITM_ProductTable.ORDERQUANTITYMULTIPLE_FLD])))
									{
										//swap
										int intTmp = Convert.ToInt32(arrProductID[i]);
										arrProductID[i] = arrProductID[j];
										arrProductID[j] = intTmp;

										DataRow drowTmp = (DataRow)arrDeliveryRows[i];
										arrDeliveryRows[i] = arrDeliveryRows[j];
										arrDeliveryRows[j] = drowTmp;

										decimal decTmp = Convert.ToDecimal(arrDeliveryQuantities[i]);
										arrDeliveryQuantities[i] = arrDeliveryQuantities[j];
										arrDeliveryQuantities[j] = decTmp;

										decSortPGPriority1st = decSortPGPriority2nd;
										decSortDeliveryQty1st = decSortDeliveryQty2nd;
									}
								}
							}
							#endregion re-sort

							if (arrNextBottles.Length > 0)
							{
								try
								{
									intNextBottleFirstProducedProductID = Convert.ToInt32(arrProductID[0]);							
									//intNextBottleFirstProducedProductID = Convert.ToInt32(arrNextBottles[0][BOTTLEFIRSTPRODUCEPRODUCTID_FLD]);
								}
								catch 
								{
									intNextBottleFirstProducedProductID = Convert.ToInt32(arrProductID[0]);							
								}
							}

							decimal decTotalChangeCategory = OptimizeOrder(arrProductID,intNextBottleFirstProducedProductID,pdtbChangeCategory,arrFinalOrder);
							arrFinalOrder.Reverse();
							//calculate change category time into leadtime
							decTotalChangeCategory = Decimal.Floor(decTotalChangeCategory * (Convert.ToDecimal(drowBottle[BOTTLETOTALCAPACITY_FLD]) / Convert.ToDecimal(drowBottle[BOTTLETOTALWORKTIME_FLD]))) + 1;

							if (arrFinalOrder.Count == 0)
							{
								continue;
							}
							drowBottle[BOTTLEFIRSTPRODUCEPRODUCTID_FLD] = arrFinalOrder[0];
							decimal decAvailableCapacity = decBottleTotalCapacity - decTotalChangeCategory;
							//calculate bottle remain capacity
							decimal decRemainCapacity = decBottleTotalCapacity - decRequiredCapacity - decTotalChangeCategory;;
							if (decRemainCapacity < 0)
							{
								decRemainCapacity = 0;
							}
							drowBottle[BOTTLEREMAINCAPACITY_FLD] = decRemainCapacity;

							//calculate produce quantity
							decimal decFactor = 1;
							if (blnBalancePlanning && (decAvailableCapacity < decRequiredCapacity))
							{
								decFactor = decAvailableCapacity / decRequiredCapacity;
							}

							decRemainCapacity = decAvailableCapacity;

							decimal decProduceQty;
							decimal decDeliveryQty;
							int intRoutingID;
							bool blnRecalculateFactor;
							decimal decProductLeadTime;
							decimal decCheckpointPerItem;
							decimal decMinProduce, decMaxProduce, decMultiple, decMaxRoundUpMin, decMaxRoundUpMultiple;
							int intProductIdx;
							int intFinalOrderIdx = 0;

							bool[] arrProcessed = new bool[arrFinalOrder.Count];
							for (int i = 0; i < arrProcessed.Length; i++) 
							{
								arrProcessed[i] = false;
							}

							//loop through all product
							foreach (int intProductID in arrFinalOrder)
							{
								#region Prepair
								intProductIdx = arrProductID.IndexOf(intProductID);								
								bool blnPair = false;
								if (arrProcessed[intProductIdx]) 
								{
									intFinalOrderIdx++;
									continue;
								}
								decDeliveryQty = Convert.ToDecimal(arrDeliveryQuantities[intProductIdx]);
								blnPair = (htPairQuantity[intProductID] != null);
								#endregion Prepair

								if (blnPair)
								{
									#region Pair processing
									#region Collect data
									int intLeftProductID = intProductID;
									int intRightProductID = Convert.ToInt32(htPair[intLeftProductID]);

									decimal decDeliveryLeft = decDeliveryQty;
									decimal decDeliveryRight = Convert.ToDecimal(htPairQuantity[intLeftProductID]);
									
									int intLeftIdx = arrProductID.IndexOf(intLeftProductID);
									int intRightIdx = arrProductID.IndexOf(intRightProductID);
									int intLeftProduceIdx = arrFinalOrder.IndexOf(intLeftProductID);
									int intRightProduceIdx = arrFinalOrder.IndexOf(intRightProductID);

									DataRow drowLeftData = (DataRow)arrDeliveryRows[intLeftIdx];
									DataRow drowRightData = (DataRow)arrDeliveryRows[intRightIdx];
									int intLeftRoutingID = Convert.ToInt32(drowLeftData[ITM_RoutingTable.ROUTINGID_FLD]);
									int intRightRoutingID = Convert.ToInt32(drowRightData[ITM_RoutingTable.ROUTINGID_FLD]);

									decimal decPairMinProduce = Convert.ToDecimal(drowLeftData[ITM_ProductTable.MINPRODUCE_FLD]);
									decimal decPairMaxProduce = Convert.ToDecimal(drowLeftData[ITM_ProductTable.MAXPRODUCE_FLD]);
									decimal decPairMultiple = Convert.ToDecimal(drowLeftData[ITM_ProductTable.ORDERQUANTITYMULTIPLE_FLD]);
									decimal decPairRoundUpMin = Convert.ToDecimal(drowLeftData[ITM_ProductTable.MAXROUNDUPTOMIN_FLD]);
									decimal decPairRoundUpMultiple = Convert.ToDecimal(drowLeftData[ITM_ProductTable.MAXROUNDUPTOMULTIPLE_FLD]);
									decimal decPairLeadTime = Convert.ToDecimal(drowLeftData[LEADTIME_FLD]);
									decimal decPairCheckpointPerItem = Convert.ToDecimal(drowLeftData[CHECKPOINTPERITEM_FLD]);

									decimal decProduceLeft = 0;
									decimal decProduceRight = 0;

									int intPairSamplePattern = 0;
									decimal decPairSampleRate = 0;
									decimal decPairDelayTime = 0;
									try
									{
										intPairSamplePattern = Convert.ToInt32(drowLeftData[PRO_CheckPointTable.SAMPLEPATTERN_FLD]);
										decPairSampleRate = Convert.ToDecimal(drowLeftData[PRO_CheckPointTable.SAMPLERATE_FLD]);
										decPairDelayTime = Convert.ToDecimal(drowLeftData[PRO_CheckPointTable.DELAYTIME_FLD]);
									}
									catch
									{										
									}

									if (Convert.ToDateTime(drowBottle[BOTTLESTARTTIME_FLD]) == dtmPlanningStartDate) 
									{
										decPairRoundUpMin = decPairMinProduce;
										decPairRoundUpMultiple = decPairMultiple;
									}


									#endregion Collect data

									#region Production group processing
									//first get all production group that contains current pair product
									DataRow[] arrPairProductionGroups = pdtbProductionGroup.Select(PRO_PGProductTable.PRODUCTID_FLD + "=" + intLeftProductID + " OR " + PRO_PGProductTable.PRODUCTID_FLD + "=" + intRightProductID);								
									decimal decPairMaxAllow = decimal.MaxValue;
									foreach (DataRow drowProductionGroup in arrPairProductionGroups) 
									{
										decimal decProducedInGroup = 0;
										decimal decGroupMaxProduce = Convert.ToDecimal(drowProductionGroup[PRO_ProductionGroupTable.GROUPPRODUCTIONMAX_FLD]);

										int intProductionGroupID = Convert.ToInt32(drowProductionGroup[PRO_PGProductTable.PRODUCTIONGROUPID_FLD]);
										//select all product in this group
										DataRow[] arrProductInPGs = pdtbProductionGroup.Select(PRO_PGProductTable.PRODUCTIONGROUPID_FLD + "=" + intProductionGroupID);
										//walk through all product and collect produce quantity in current bottle
										foreach (DataRow drowProductInPG in arrProductInPGs)
										{
											int intProductInPGID = Convert.ToInt32(drowProductInPG[PRO_PGProductTable.PRODUCTID_FLD]);
											//find rawresult
											string strFilter = RAWRESULTBOTTLEID_FLD + "=" + intCapacityBottleID + " AND " + RAWRESULTPRODUCTID_FLD + "=" + intProductInPGID;
											object objResult = pdtbRawResult.Compute("SUM(" + RAWRESULTPRODUCEQUANTITY_FLD + ")",strFilter);
											decProducedInGroup += (objResult == DBNull.Value) ? (0) : (Convert.ToDecimal(objResult));
										}

										if (decGroupMaxProduce - decProducedInGroup < decPairMaxAllow) 
										{
											decPairMaxAllow = decGroupMaxProduce - decProducedInGroup;
										}
										/*if (decPairMaxProduce > 0)
										{
											decPairMaxProduce = (decGroupMaxProduce - decProducedInGroup < decPairMaxProduce) ? decGroupMaxProduce - decProducedInGroup : decPairMaxProduce;
										}
										else
										{
											decPairMaxProduce = decGroupMaxProduce - decProducedInGroup;
										}
										if (decPairMaxProduce == 0) 
										{
											decPairMaxProduce = -1;
										}*/
									}

									#endregion Production group processing

									if (decPairMaxProduce < 0) 
									{
										#region Just turn recalculate factor flag on
										if (decPairMaxProduce != -1)
										{
											//serious fault, not allow any value except -1
											decPairMaxProduce.ToString();
										}
										//this pair will not be produced, recalculate factor
										blnRecalculateFactor = true;
										#endregion
									}
									else 
									{
										#region Normalize data
										decPairMultiple = (decPairMultiple == 0) ? 1 : decPairMultiple;
										decPairMaxProduce = (decPairMaxProduce % decPairMultiple == 0) ? decPairMaxProduce : (decPairMultiple * decimal.Floor(decPairMaxProduce / decPairMultiple));
										decPairMinProduce = (decPairMinProduce % decPairMultiple == 0) ? decPairMinProduce : (decPairMultiple * (decimal.Floor(decPairMinProduce / decPairMultiple) + 1));
										if (Convert.ToDateTime(drowBottle[BOTTLESTARTTIME_FLD]) <= dtmPlanningStartDate.AddDays(Convert.ToInt32(drowWC[PRO_ProductionLineTable.ROUNDUPDAYSEXCEPTION_FLD])))
										{
											decPairRoundUpMin = decPairMinProduce;
											decPairRoundUpMultiple = decPairMultiple;
										}
										#endregion
										
										if ((decPairMaxProduce > 0) && (decPairMinProduce > decPairMaxProduce))
										{
											#region Just turn recalculate factor flag on
											//this pair will not be produced, recalculate factor
											blnRecalculateFactor = true;
											#endregion
										}
										else
										{
											#region Calculate produce quantity
											decimal decDeliveryDiff = Math.Abs(decDeliveryLeft - decDeliveryRight);
											decimal decDiffInMultiple;
											if (decDeliveryDiff % decPairMultiple == 0)
											{
												decDiffInMultiple = decDeliveryDiff / decPairMultiple;
											}
											else
											{
												decDiffInMultiple = decimal.Floor(decDeliveryDiff / decPairMultiple);
											}
									
											decProduceLeft = decimal.Floor(decDeliveryLeft * decFactor);
											decProduceRight = decimal.Floor(decDeliveryRight * decFactor);

											decimal decPairUsedCapacity = 0;

											//if produce quantity greater than max available, reduce it to fit
											if ((decPairLeadTime + decPairCheckpointPerItem) * (decProduceLeft + decProduceRight) > decRemainCapacity) 
											{
												decimal decAvailForFewer = 0;
												decimal decAvailForGreater = 0;
												if (decProduceLeft < decProduceRight)
												{
													//first, reduce the fewer
													decAvailForFewer = decRemainCapacity - decProduceRight * (decPairLeadTime + decPairCheckpointPerItem);
													if (decAvailForFewer < 0)
													{
														decAvailForFewer = 0;
													}
													decProduceLeft = decimal.Floor(decAvailForFewer / (decPairLeadTime + decPairCheckpointPerItem));
													//then, reduce the greater
													decAvailForGreater = decRemainCapacity - decAvailForFewer;
													decProduceRight = decimal.Floor(decAvailForGreater / (decPairLeadTime + decPairCheckpointPerItem));
												}
												else if (decProduceLeft > decProduceRight)
												{
													//first, reduce the fewer
													decAvailForFewer = decRemainCapacity - decProduceLeft * (decPairLeadTime + decPairCheckpointPerItem);
													if (decAvailForFewer < 0)
													{
														decAvailForFewer = 0;
													}
													decProduceRight = decimal.Floor(decAvailForFewer / (decPairLeadTime + decPairCheckpointPerItem));
													//then, reduce the greater
													decAvailForGreater = decRemainCapacity - decAvailForFewer;
													decProduceLeft = decimal.Floor(decAvailForGreater / (decPairLeadTime + decPairCheckpointPerItem));
												}
												else
												{
													//reduce both
													decProduceLeft = decProduceRight = decimal.Floor(decRemainCapacity / (2 * (decPairLeadTime + decPairCheckpointPerItem)));
												}
												//decProduceLeft = decProduceRight = decimal.Floor(decRemainCapacity / (2 * (decPairLeadTime + decPairCheckpointPerItem)));
											}


											decimal decMaxRoundUpAvailable = decRemainCapacity / (decPairLeadTime + decPairCheckpointPerItem);
											#region calculate produce for left
											//check with min
											if (decProduceLeft < decPairMinProduce)
											{
												//check if can roundup
												if ((decProduceLeft + decPairRoundUpMin >= decPairMinProduce) && (decPairMinProduce < decMaxRoundUpAvailable / 2))
												{
													//check remain capacity
													if (decPairMinProduce * (decPairLeadTime + decPairCheckpointPerItem) <= decRemainCapacity - decPairUsedCapacity)
													{
														decProduceLeft = decPairMinProduce;
													}
													else
													{
														decProduceLeft = 0;
													}
												}
												else
												{
													decProduceLeft = 0;
												}
											}
											//check with max
											if ((decPairMaxProduce > 0) && (decProduceLeft > decPairMaxProduce))
											{
												//simply, cut to max
												decProduceLeft = decPairMaxProduce;
											}
											//round to fit multiple
											if (decProduceLeft % decPairMultiple != 0)
											{
												//check if can round up
												if ((decProduceLeft + decPairRoundUpMultiple >= (decimal.Floor(decProduceLeft / decPairMultiple) + 1) * decPairMultiple) && (decimal.Floor(decProduceLeft / decPairMultiple) + 1 < decMaxRoundUpAvailable / 2))
												{
													//check remain capacity
													if ((decimal.Floor(decProduceLeft / decPairMultiple) + 1) * decPairMultiple * (decPairLeadTime + decPairCheckpointPerItem) <= decRemainCapacity - decPairUsedCapacity)
													{
														decProduceLeft = (decimal.Floor(decProduceLeft / decPairMultiple) + 1) * decPairMultiple;
													}
													else
													{
														decProduceLeft = decimal.Floor(decProduceLeft / decPairMultiple) * decPairMultiple;
													}
												}
												else
												{
													decProduceLeft = decimal.Floor(decProduceLeft / decPairMultiple) * decPairMultiple;
												}
											}
											else
											{
												decProduceLeft = decimal.Floor(decProduceLeft / decPairMultiple) * decPairMultiple;
											}
											decPairUsedCapacity += decProduceLeft * (decPairLeadTime + decPairCheckpointPerItem);
											#endregion

											#region calculate produce for right
											//check with min
											if (decProduceRight < decPairMinProduce)
											{
												//check if can roundup
												if ((decProduceRight + decPairRoundUpMin >= decPairMinProduce) && (decPairMinProduce < decMaxRoundUpAvailable / 2))
												{
													//check remain capacity
													if (decPairMinProduce * (decPairLeadTime + decPairCheckpointPerItem) <= decRemainCapacity - decPairUsedCapacity)
													{
														decProduceRight = decPairMinProduce;
													}
													else
													{
														decProduceRight = 0;
													}
												}
												else
												{
													decProduceRight = 0;
												}
											}
											//check with max
											if ((decPairMaxProduce > 0) && (decProduceRight > decPairMaxProduce))
											{
												//simply, cut to max
												decProduceRight = decPairMaxProduce;
											}
											//round to fit multiple
											if (decProduceRight % decPairMultiple != 0)
											{
												//check if can round up
												if ((decProduceRight + decPairRoundUpMultiple >= (decimal.Floor(decProduceRight / decPairMultiple) + 1) * decPairMultiple) && (decimal.Floor(decProduceLeft / decPairMultiple) + 1 < decMaxRoundUpAvailable / 2))
												{
													//check remain capacity
													if ((decimal.Floor(decProduceRight / decPairMultiple) + 1) * decPairMultiple * (decPairLeadTime + decPairCheckpointPerItem) <= decRemainCapacity - decPairUsedCapacity)
													{
														decProduceRight = (decimal.Floor(decProduceRight / decPairMultiple) + 1) * decPairMultiple;
													}
													else
													{
														decProduceRight = decimal.Floor(decProduceRight / decPairMultiple) * decPairMultiple;
													}
												}
												else
												{
													decProduceRight = decimal.Floor(decProduceRight / decPairMultiple) * decPairMultiple;
												}
											}
											else
											{
												decProduceRight = decimal.Floor(decProduceRight / decPairMultiple) * decPairMultiple;
											}
											decPairUsedCapacity += decProduceRight * (decPairLeadTime + decPairCheckpointPerItem);
											#endregion

											#region adjust for pair balancing
											if (Math.Abs(decProduceRight - decProduceLeft) > decDiffInMultiple * decPairMultiple)
											{
												//increase the smaller or decrease the bigger
												if (decPairMultiple * (decPairLeadTime + decPairCheckpointPerItem) <= decRemainCapacity - decPairUsedCapacity)
												{
													if (decProduceLeft < decProduceRight)
													{
														if ((decProduceLeft + decPairMultiple >= decPairMinProduce) && (decProduceLeft + decPairMultiple <= decPairMinProduce))
														{
															decProduceLeft += decPairMultiple;
														}
														else
														{
															decProduceRight -= decPairMultiple;
														}
													}
													else
													{
														if ((decProduceRight + decPairMultiple >= decPairMinProduce) && (decProduceRight + decPairMultiple <= decPairMinProduce))
														{
															decProduceRight += decPairMultiple;
														}
														else
														{
															decProduceLeft -= decPairMultiple;
														}
													}
													decPairUsedCapacity += decPairMultiple * (decPairLeadTime + decPairCheckpointPerItem);
												}
												else
												{
													if (decProduceLeft < decProduceRight)
													{
														decProduceRight -= decPairMultiple;
													}
													else
													{
														decProduceLeft -= decPairMultiple;
													}
													decPairUsedCapacity -= decPairMultiple * (decPairLeadTime + decPairCheckpointPerItem);
												}
											}
											#endregion

											#region Adjust for production group constraints
											while (decProduceLeft + decProduceRight > decPairMaxAllow) 
											{
												//if not and right not equal, reduce maxer
												if (decProduceLeft > decProduceRight)
												{
													decProduceLeft -= decPairMultiple;
													decPairUsedCapacity -= decPairMultiple * (decPairLeadTime + decPairCheckpointPerItem);
												}
												else if (decProduceLeft < decProduceRight)
												{
													decProduceRight -= decPairMultiple;
													decPairUsedCapacity -= decPairMultiple * (decPairLeadTime + decPairCheckpointPerItem);
												}
												//else reduce all
												else
												{
													decProduceLeft -= decPairMultiple;
													decProduceRight -= decPairMultiple;
													decPairUsedCapacity -= 2 * decPairMultiple * (decPairLeadTime + decPairCheckpointPerItem);
												}
											}
											#endregion Adjust for production group constraints

											#endregion

											#region Write to raw result 
											decRemainCapacity -= decPairUsedCapacity;
											AddRawResult(pdtbRawResult,intLeftProductID,intCapacityBottleID,decProduceLeft,intLeftProduceIdx,decPairLeadTime,intLeftRoutingID,intWorkCenterID,strWorkCenterCode,intPairSamplePattern,decPairSampleRate,decPairDelayTime,DateTime.MinValue,decPairCheckpointPerItem,0);
											AddRawResult(pdtbRawResult,intRightProductID,intCapacityBottleID,decProduceRight,intRightProduceIdx,decPairLeadTime,intRightRoutingID, intWorkCenterID,strWorkCenterCode,intPairSamplePattern,decPairSampleRate,decPairDelayTime,DateTime.MinValue,decPairCheckpointPerItem,0);
											#endregion


											blnRecalculateFactor = true;
										}									
									}
			
									#region Generate over quantity and child quantity

									#region Left
									//generate over amount
									decimal decOverQtyLeft = decDeliveryLeft - decProduceLeft;
									if (decOverQtyLeft > 0)
									{
										DataRow drowOverDelivery = pdtbDelSch.NewRow();
										drowOverDelivery[SO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD] = decOverQtyLeft;
										drowOverDelivery[SO_DeliveryScheduleTable.SCHEDULEDATE_FLD] = dtmBottleStartTime;
										drowOverDelivery[ITM_RoutingTable.ROUTINGID_FLD] = drowLeftData[ITM_RoutingTable.ROUTINGID_FLD];
										drowOverDelivery[SO_SaleOrderDetailTable.PRODUCTID_FLD] = intLeftProductID;
										drowOverDelivery[MST_WorkCenterTable.WORKCENTERID_FLD] = drowLeftData[MST_WorkCenterTable.WORKCENTERID_FLD];
										drowOverDelivery[WORKCENTERCODE_FLD] = drowLeftData[WORKCENTERCODE_FLD];
										drowOverDelivery[PRO_CheckPointTable.SAMPLEPATTERN_FLD] = drowLeftData[PRO_CheckPointTable.SAMPLEPATTERN_FLD];
										drowOverDelivery[PRO_CheckPointTable.SAMPLERATE_FLD] = drowLeftData[PRO_CheckPointTable.SAMPLERATE_FLD];
										drowOverDelivery[PRO_CheckPointTable.DELAYTIME_FLD] = drowLeftData[PRO_CheckPointTable.DELAYTIME_FLD];
										drowOverDelivery[LEADTIME_FLD] = drowLeftData[LEADTIME_FLD];
										drowOverDelivery[ITM_RoutingTable.FIXLT_FLD] = drowLeftData[ITM_RoutingTable.FIXLT_FLD];
										drowOverDelivery[MINPRODUCE_FLD] = drowLeftData[MINPRODUCE_FLD];
										drowOverDelivery[MAXPRODUCE_FLD] = drowLeftData[MAXPRODUCE_FLD];
										drowOverDelivery[CHECKPOINTPERITEM_FLD] = drowLeftData[CHECKPOINTPERITEM_FLD];
										drowOverDelivery[ITM_ProductTable.SCRAPPERCENT_FLD] = drowLeftData[ITM_ProductTable.SCRAPPERCENT_FLD];
										drowOverDelivery[ITM_ProductTable.ORDERQUANTITYMULTIPLE_FLD] = drowLeftData[ITM_ProductTable.ORDERQUANTITYMULTIPLE_FLD];
										drowOverDelivery[ITM_ProductTable.REVISION_FLD] = drowLeftData[ITM_ProductTable.REVISION_FLD];
										drowOverDelivery[MAXROUNDUPTOMIN_FLD] = drowLeftData[MAXROUNDUPTOMIN_FLD];
										drowOverDelivery[MAXROUNDUPTOMULTIPLE_FLD] = drowLeftData[MAXROUNDUPTOMULTIPLE_FLD];
										drowOverDelivery[CAPACITYBOTTLEID_FLD] = -1;
										pdtbDelSch.Rows.Add(drowOverDelivery);
									}

									//generate child amount
									if (decProduceLeft > 0)
									{
										DataRow[] arrChildProducts = pdtbBOM.Select(ITM_BOMTable.PRODUCTID_FLD + "=" + intLeftProductID);
										foreach(DataRow drowChildProduct in arrChildProducts)
										{
											DataRow drowChildDelivery = pdtbDelSch.NewRow();
											decimal decShrink = Convert.ToDecimal(drowChildProduct[ITM_BOMTable.SHRINK_FLD]);
											drowChildDelivery[SO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD] = Decimal.Round(decProduceLeft * (Convert.ToDecimal(drowChildProduct[ITM_BOMTable.QUANTITY_FLD])) / (1 - decShrink / 100),0);
											drowChildDelivery[SO_DeliveryScheduleTable.SCHEDULEDATE_FLD] = dtmBottleStartTime.AddSeconds(-Convert.ToDouble(drowChildProduct[ITM_BOMTable.LEADTIMEOFFSET_FLD])) ;
											drowChildDelivery[SO_SaleOrderDetailTable.PRODUCTID_FLD] = drowChildProduct[ITM_BOMTable.COMPONENTID_FLD];
											drowChildDelivery[ITM_RoutingTable.ROUTINGID_FLD] = drowChildProduct[ITM_RoutingTable.ROUTINGID_FLD];
											drowChildDelivery[MST_WorkCenterTable.WORKCENTERID_FLD] = drowChildProduct[MST_WorkCenterTable.WORKCENTERID_FLD];
											drowChildDelivery[WORKCENTERCODE_FLD] = drowChildProduct[WORKCENTERCODE_FLD];
											drowChildDelivery[PRO_CheckPointTable.SAMPLEPATTERN_FLD] = drowChildProduct[PRO_CheckPointTable.SAMPLEPATTERN_FLD];
											drowChildDelivery[PRO_CheckPointTable.SAMPLERATE_FLD] = drowChildProduct[PRO_CheckPointTable.SAMPLERATE_FLD];
											drowChildDelivery[PRO_CheckPointTable.DELAYTIME_FLD] = drowChildProduct[PRO_CheckPointTable.DELAYTIME_FLD];
											drowChildDelivery[LEADTIME_FLD] = drowChildProduct[LEADTIME_FLD];
											drowChildDelivery[ITM_RoutingTable.FIXLT_FLD] = drowChildProduct[ITM_RoutingTable.FIXLT_FLD];
											drowChildDelivery[CAPACITYBOTTLEID_FLD] = -2;
											drowChildDelivery[MINPRODUCE_FLD] = drowChildProduct[MINPRODUCE_FLD];
											drowChildDelivery[MAXPRODUCE_FLD] = drowChildProduct[MAXPRODUCE_FLD];
											drowChildDelivery[CHECKPOINTPERITEM_FLD] = drowChildProduct[CHECKPOINTPERITEM_FLD];
											drowChildDelivery[ITM_ProductTable.SCRAPPERCENT_FLD] = drowChildProduct[ITM_ProductTable.SCRAPPERCENT_FLD];
											drowChildDelivery[ITM_ProductTable.ORDERQUANTITYMULTIPLE_FLD] = drowChildProduct[ITM_ProductTable.ORDERQUANTITYMULTIPLE_FLD];
											drowChildDelivery[ITM_ProductTable.REVISION_FLD] = drowChildProduct[ITM_ProductTable.REVISION_FLD];
											drowChildDelivery[MAXROUNDUPTOMIN_FLD] = drowChildProduct[MAXROUNDUPTOMIN_FLD];
											drowChildDelivery[MAXROUNDUPTOMULTIPLE_FLD] = drowChildProduct[MAXROUNDUPTOMULTIPLE_FLD];
											pdtbDelSch.Rows.Add(drowChildDelivery);
										}
									}
									#endregion Left
											
									#region Right
									//generate over amount
									decimal decOverQtyRight = decDeliveryRight - decProduceRight;
									if (decOverQtyRight > 0)
									{
										DataRow drowOverDelivery = pdtbDelSch.NewRow();
										drowOverDelivery[SO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD] = decOverQtyRight;
										drowOverDelivery[SO_DeliveryScheduleTable.SCHEDULEDATE_FLD] = dtmBottleStartTime;
										drowOverDelivery[ITM_RoutingTable.ROUTINGID_FLD] = drowRightData[ITM_RoutingTable.ROUTINGID_FLD];
										drowOverDelivery[SO_SaleOrderDetailTable.PRODUCTID_FLD] = intRightProductID;
										drowOverDelivery[MST_WorkCenterTable.WORKCENTERID_FLD] = drowRightData[MST_WorkCenterTable.WORKCENTERID_FLD];
										drowOverDelivery[WORKCENTERCODE_FLD] = drowLeftData[WORKCENTERCODE_FLD];
										drowOverDelivery[PRO_CheckPointTable.SAMPLEPATTERN_FLD] = drowRightData[PRO_CheckPointTable.SAMPLEPATTERN_FLD];
										drowOverDelivery[PRO_CheckPointTable.SAMPLERATE_FLD] = drowRightData[PRO_CheckPointTable.SAMPLERATE_FLD];
										drowOverDelivery[PRO_CheckPointTable.DELAYTIME_FLD] = drowRightData[PRO_CheckPointTable.DELAYTIME_FLD];
										drowOverDelivery[LEADTIME_FLD] = drowRightData[LEADTIME_FLD];
										drowOverDelivery[ITM_RoutingTable.FIXLT_FLD] = drowRightData[ITM_RoutingTable.FIXLT_FLD];
										drowOverDelivery[MINPRODUCE_FLD] = drowRightData[MINPRODUCE_FLD];
										drowOverDelivery[MAXPRODUCE_FLD] = drowRightData[MAXPRODUCE_FLD];
										drowOverDelivery[CHECKPOINTPERITEM_FLD] = drowRightData[CHECKPOINTPERITEM_FLD];
										drowOverDelivery[ITM_ProductTable.SCRAPPERCENT_FLD] = drowRightData[ITM_ProductTable.SCRAPPERCENT_FLD];
										drowOverDelivery[ITM_ProductTable.ORDERQUANTITYMULTIPLE_FLD] = drowRightData[ITM_ProductTable.ORDERQUANTITYMULTIPLE_FLD];
										drowOverDelivery[ITM_ProductTable.REVISION_FLD] = drowRightData[ITM_ProductTable.REVISION_FLD];
										drowOverDelivery[MAXROUNDUPTOMIN_FLD] = drowRightData[MAXROUNDUPTOMIN_FLD];
										drowOverDelivery[MAXROUNDUPTOMULTIPLE_FLD] = drowRightData[MAXROUNDUPTOMULTIPLE_FLD];
										drowOverDelivery[CAPACITYBOTTLEID_FLD] = -1;
										pdtbDelSch.Rows.Add(drowOverDelivery);
									}

									//generate child amount
									if (decProduceRight > 0)
									{
										DataRow[] arrChildProducts = pdtbBOM.Select(ITM_BOMTable.PRODUCTID_FLD + "=" + intRightProductID);
										foreach(DataRow drowChildProduct in arrChildProducts)
										{
											DataRow drowChildDelivery = pdtbDelSch.NewRow();
											decimal decShrink = Convert.ToDecimal(drowChildProduct[ITM_BOMTable.SHRINK_FLD]);
											drowChildDelivery[SO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD] = Decimal.Round(decProduceRight * (Convert.ToDecimal(drowChildProduct[ITM_BOMTable.QUANTITY_FLD])) / (1 - decShrink / 100),0);
											drowChildDelivery[SO_DeliveryScheduleTable.SCHEDULEDATE_FLD] = dtmBottleStartTime.AddSeconds(-Convert.ToDouble(drowChildProduct[ITM_BOMTable.LEADTIMEOFFSET_FLD])) ;
											drowChildDelivery[SO_SaleOrderDetailTable.PRODUCTID_FLD] = drowChildProduct[ITM_BOMTable.COMPONENTID_FLD];
											drowChildDelivery[ITM_RoutingTable.ROUTINGID_FLD] = drowChildProduct[ITM_RoutingTable.ROUTINGID_FLD];
											drowChildDelivery[MST_WorkCenterTable.WORKCENTERID_FLD] = drowChildProduct[MST_WorkCenterTable.WORKCENTERID_FLD];
											drowChildDelivery[WORKCENTERCODE_FLD] = drowChildProduct[WORKCENTERCODE_FLD];
											drowChildDelivery[PRO_CheckPointTable.SAMPLEPATTERN_FLD] = drowChildProduct[PRO_CheckPointTable.SAMPLEPATTERN_FLD];
											drowChildDelivery[PRO_CheckPointTable.SAMPLERATE_FLD] = drowChildProduct[PRO_CheckPointTable.SAMPLERATE_FLD];
											drowChildDelivery[PRO_CheckPointTable.DELAYTIME_FLD] = drowChildProduct[PRO_CheckPointTable.DELAYTIME_FLD];
											drowChildDelivery[LEADTIME_FLD] = drowChildProduct[LEADTIME_FLD];
											drowChildDelivery[ITM_RoutingTable.FIXLT_FLD] = drowChildProduct[ITM_RoutingTable.FIXLT_FLD];
											drowChildDelivery[CAPACITYBOTTLEID_FLD] = -2;
											drowChildDelivery[MINPRODUCE_FLD] = drowChildProduct[MINPRODUCE_FLD];
											drowChildDelivery[MAXPRODUCE_FLD] = drowChildProduct[MAXPRODUCE_FLD];
											drowChildDelivery[CHECKPOINTPERITEM_FLD] = drowChildProduct[CHECKPOINTPERITEM_FLD];
											drowChildDelivery[ITM_ProductTable.SCRAPPERCENT_FLD] = drowChildProduct[ITM_ProductTable.SCRAPPERCENT_FLD];
											drowChildDelivery[ITM_ProductTable.ORDERQUANTITYMULTIPLE_FLD] = drowChildProduct[ITM_ProductTable.ORDERQUANTITYMULTIPLE_FLD];
											drowChildDelivery[ITM_ProductTable.REVISION_FLD] = drowChildProduct[ITM_ProductTable.REVISION_FLD];
											drowChildDelivery[MAXROUNDUPTOMIN_FLD] = drowChildProduct[MAXROUNDUPTOMIN_FLD];
											drowChildDelivery[MAXROUNDUPTOMULTIPLE_FLD] = drowChildProduct[MAXROUNDUPTOMULTIPLE_FLD];
											pdtbDelSch.Rows.Add(drowChildDelivery);
										}
									}
									#endregion Right

									#endregion

									#region Complete processing
									arrProcessed[intLeftIdx] = arrProcessed[intRightIdx] = true;
									#endregion
									
									#endregion Pair processing
								}
								else 
								{
									#region Single processing
									decProduceQty = Decimal.Floor(decDeliveryQty * decFactor);
							
									DataRow drowDeliveryData = (DataRow)arrDeliveryRows[intProductIdx];
									decMinProduce = Convert.ToDecimal(drowDeliveryData[MINPRODUCE_FLD]);
									decMaxProduce = Convert.ToDecimal(drowDeliveryData[MAXPRODUCE_FLD]);
									decMultiple = Convert.ToDecimal(drowDeliveryData[ITM_ProductTable.ORDERQUANTITYMULTIPLE_FLD]);
									if (decMultiple == 0) 
									{
										decMultiple = 1;
									}
									decMaxRoundUpMin = Convert.ToDecimal(drowDeliveryData[MAXROUNDUPTOMIN_FLD]);
									decMaxRoundUpMultiple = Convert.ToDecimal(drowDeliveryData[MAXROUNDUPTOMULTIPLE_FLD]);

									decProductLeadTime = Convert.ToDecimal(drowDeliveryData[LEADTIME_FLD]);
									decCheckpointPerItem = Convert.ToDecimal(drowDeliveryData[CHECKPOINTPERITEM_FLD]);

									//if produce quantity greater than max available, reduce it to fit
									if ((decProductLeadTime + decCheckpointPerItem) * decProduceQty > decRemainCapacity) 
									{
										decProduceQty = decimal.Floor(decRemainCapacity / (decProductLeadTime + decCheckpointPerItem));
									}

									//first day exception
									if (Convert.ToDateTime(drowBottle[BOTTLESTARTTIME_FLD]) <= dtmPlanningStartDate.AddDays(Convert.ToInt32(drowWC[PRO_ProductionLineTable.ROUNDUPDAYSEXCEPTION_FLD])))
									{
										decMaxRoundUpMin = decMinProduce;
										decMaxRoundUpMultiple = decMultiple;
									}

									#region calculate actual max produce base on production group
									//first get all production group that contains current product
									DataRow[] arrProductionGroups = pdtbProductionGroup.Select(PRO_PGProductTable.PRODUCTID_FLD + "=" + intProductID);
							
									foreach (DataRow drowProductionGroup in arrProductionGroups) 
									{
										decimal decProducedInGroup = 0;
										decimal decGroupMaxProduce = Convert.ToDecimal(drowProductionGroup[PRO_ProductionGroupTable.GROUPPRODUCTIONMAX_FLD]);

										int intProductionGroupID = Convert.ToInt32(drowProductionGroup[PRO_PGProductTable.PRODUCTIONGROUPID_FLD]);
										//select all product in this group
										DataRow[] arrProductInPGs = pdtbProductionGroup.Select(PRO_PGProductTable.PRODUCTIONGROUPID_FLD + "=" + intProductionGroupID);
										//walk through all product and collect produce quantity in current bottle
										foreach (DataRow drowProductInPG in arrProductInPGs)
										{
											int intProductInPGID = Convert.ToInt32(drowProductInPG[PRO_PGProductTable.PRODUCTID_FLD]);
											//find rawresult
											string strFilter = RAWRESULTBOTTLEID_FLD + "=" + intCapacityBottleID + " AND " + RAWRESULTPRODUCTID_FLD + "=" + intProductInPGID;
											object objResult = pdtbRawResult.Compute("SUM(" + RAWRESULTPRODUCEQUANTITY_FLD + ")",strFilter);
											decProducedInGroup += (objResult == DBNull.Value) ? (0) : (Convert.ToDecimal(objResult));
										}
										if (decMaxProduce > 0)
										{
											decMaxProduce = (decGroupMaxProduce - decProducedInGroup < decMaxProduce) ? decGroupMaxProduce - decProducedInGroup : decMaxProduce;
										}
										else
										{
											decMaxProduce = decGroupMaxProduce - decProducedInGroup;
										}
										if (decMaxProduce == 0) 
										{
											decMaxProduce = -1;
										}
									}

									if (decMaxProduce < -1) 
									{
										//serious fault, not allow any value except -1
										decMaxProduce.ToString();
									}
									#endregion calculate actual max produce base on production group

									//calculate max produce base on model balance

									int intMultiple = Convert.ToInt32(decMultiple);
									int intMaxProduce = Convert.ToInt32(decMaxProduce);
									int intMinProduce = Convert.ToInt32(decMinProduce);

									//normalize max, min base on multiple
									if ((intMultiple != 0) && (decMaxProduce > 0))
									{
										decMaxProduce = (intMaxProduce / intMultiple) * intMultiple;
										if (decMaxProduce == 0)
										{
											decMaxProduce = -1;
										}
									}
									if ((intMultiple != 0) && (intMinProduce % intMultiple != 0))
									{
										decMinProduce = (intMinProduce / intMultiple + 1) * intMultiple;
									}

									if ((decMaxProduce == -1) || ((decMaxProduce > 0) && (decMaxProduce < decMinProduce)))
									{
										decProduceQty = 0;
										blnRecalculateFactor = true;
									}
									else
									{
										if ((decMinProduce > 0) && (decProduceQty < decMinProduce) && (decProduceQty > 0))
										{
											if (decMinProduce - decProduceQty <= decMaxRoundUpMin)
											{
												if (decProductLeadTime * (decMinProduce) + Math.Round((decMinProduce) * decCheckpointPerItem) <= decRemainCapacity)//Convert.ToDecimal(drowBottle[BOTTLEREMAINCAPACITY_FLD]))
												{
													//decRemainCapacity -= decProductLeadTime * (decMinProduce - decProduceQty);
													//drowBottle[BOTTLEREMAINCAPACITY_FLD] = Convert.ToDecimal(drowBottle[BOTTLEREMAINCAPACITY_FLD]) - decProductLeadTime * (decMinProduce - decProduceQty) - Math.Round(decCheckpointPerItem * (decMinProduce - decProduceQty));//decRemainCapacity;
													decProduceQty = decMinProduce;
												}
												else
												{
													//drowBottle[BOTTLEREMAINCAPACITY_FLD] = Convert.ToDecimal(drowBottle[BOTTLEREMAINCAPACITY_FLD]) + decProductLeadTime * (decProduceQty) + Math.Round(decCheckpointPerItem * (decProduceQty));//decRemainCapacity;
													decProduceQty = 0;
												}
												blnRecalculateFactor = true;
											}
											else
											{
												decProduceQty = 0;
												blnRecalculateFactor = true;
											}
										}
										else
										{
											blnRecalculateFactor = false;
										}

										int intProduceQty = Convert.ToInt32(decProduceQty);
										if (intProduceQty % intMultiple != 0)
										{
											if (decProductLeadTime * (intProduceQty / intMultiple + 1) * decMultiple + Math.Round(decCheckpointPerItem * (intProduceQty / intMultiple + 1) * decMultiple) < decRemainCapacity)
											{
												if (((intProduceQty / intMultiple + 1) * intMultiple) - decProduceQty <= decMaxRoundUpMultiple)
												{
													decProduceQty = (intProduceQty / intMultiple + 1) * intMultiple;
												}
												else
												{
													decProduceQty = (intProduceQty / intMultiple) * intMultiple;
												}
											}
											else
											{
												decProduceQty = (intProduceQty / intMultiple) * intMultiple;
											}
											blnRecalculateFactor = true;
										}

										if ((decMaxProduce > 0) && (decProduceQty > decMaxProduce))
										{
											decProduceQty = decMaxProduce;
											blnRecalculateFactor = true;
										}
									}
									//calculate real produce amount
									arrProduceQuantities[intProductIdx] = decProduceQty;
									decimal decCheckpointTime = 0;
									if (blnIncludeCheckpoint)
									{
										decCheckpointTime = Math.Round(decProduceQty * decCheckpointPerItem);//CalculateCheckpointTime(drowDeliveryData,decProduceQty,decProductLeadTime * decProduceQty * (decBottleTotalWorkingTime / decBottleTotalCapacity));
									}
									else
									{
										decCheckpointTime = 0;
									}
									decRemainCapacity -= decProductLeadTime * decProduceQty + decCheckpointTime;// * (decBottleTotalCapacity / decBottleTotalCapacity);
									//drowBottle[BOTTLEREMAINCAPACITY_FLD] = Convert.ToDecimal(drowBottle[BOTTLEREMAINCAPACITY_FLD]) - decProductLeadTime * decProduceQty + decCheckpointTime;
									//write to raw result
									try
									{
										intSamplePattern = Convert.ToInt32(drowDeliveryData[PRO_CheckPointTable.SAMPLEPATTERN_FLD]);									
										decSampleRate = Convert.ToDecimal(drowDeliveryData[PRO_CheckPointTable.SAMPLERATE_FLD]);
										decDelayTime = Convert.ToDecimal(drowDeliveryData[PRO_CheckPointTable.DELAYTIME_FLD]);							
									}
									catch 
									{
										intSamplePattern = 0;
									}
									//decimal decCheckpointPerItem = Convert.ToDecimal(drowDeliveryData[CHECKPOINTPERITEM_FLD]);
									intRoutingID = Convert.ToInt32(drowDeliveryData[ITM_RoutingTable.ROUTINGID_FLD]);
									AddRawResult(pdtbRawResult,intProductID,intCapacityBottleID,decProduceQty,arrFinalOrder.IndexOf(intProductID),decProductLeadTime,intRoutingID, intWorkCenterID,strWorkCenterCode,intSamplePattern,decSampleRate,decDelayTime,DateTime.MinValue,decCheckpointPerItem,0);

									//generate over amount
									decimal decOverQty = decDeliveryQty - decProduceQty;
									if (decOverQty > 0)
									{
										DataRow drowOverDelivery = pdtbDelSch.NewRow();
										drowOverDelivery[SO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD] = decOverQty;
										drowOverDelivery[SO_DeliveryScheduleTable.SCHEDULEDATE_FLD] = dtmBottleStartTime;
										drowOverDelivery[ITM_RoutingTable.ROUTINGID_FLD] = drowDeliveryData[ITM_RoutingTable.ROUTINGID_FLD];
										drowOverDelivery[SO_SaleOrderDetailTable.PRODUCTID_FLD] = intProductID;
										drowOverDelivery[MST_WorkCenterTable.WORKCENTERID_FLD] = drowDeliveryData[MST_WorkCenterTable.WORKCENTERID_FLD];
										drowOverDelivery[WORKCENTERCODE_FLD] = drowDeliveryData[WORKCENTERCODE_FLD];
										drowOverDelivery[PRO_CheckPointTable.SAMPLEPATTERN_FLD] = drowDeliveryData[PRO_CheckPointTable.SAMPLEPATTERN_FLD];
										drowOverDelivery[PRO_CheckPointTable.SAMPLERATE_FLD] = drowDeliveryData[PRO_CheckPointTable.SAMPLERATE_FLD];
										drowOverDelivery[PRO_CheckPointTable.DELAYTIME_FLD] = drowDeliveryData[PRO_CheckPointTable.DELAYTIME_FLD];
										drowOverDelivery[LEADTIME_FLD] = drowDeliveryData[LEADTIME_FLD];
										drowOverDelivery[ITM_RoutingTable.FIXLT_FLD] = drowDeliveryData[ITM_RoutingTable.FIXLT_FLD];
										drowOverDelivery[MINPRODUCE_FLD] = drowDeliveryData[MINPRODUCE_FLD];
										drowOverDelivery[MAXPRODUCE_FLD] = drowDeliveryData[MAXPRODUCE_FLD];
										drowOverDelivery[CHECKPOINTPERITEM_FLD] = drowDeliveryData[CHECKPOINTPERITEM_FLD];
										drowOverDelivery[ITM_ProductTable.SCRAPPERCENT_FLD] = drowDeliveryData[ITM_ProductTable.SCRAPPERCENT_FLD];
										drowOverDelivery[ITM_ProductTable.ORDERQUANTITYMULTIPLE_FLD] = drowDeliveryData[ITM_ProductTable.ORDERQUANTITYMULTIPLE_FLD];
										drowOverDelivery[ITM_ProductTable.REVISION_FLD] = drowDeliveryData[ITM_ProductTable.REVISION_FLD];
										drowOverDelivery[MAXROUNDUPTOMIN_FLD] = drowDeliveryData[MAXROUNDUPTOMIN_FLD];
										drowOverDelivery[MAXROUNDUPTOMULTIPLE_FLD] = drowDeliveryData[MAXROUNDUPTOMULTIPLE_FLD];
										drowOverDelivery[CAPACITYBOTTLEID_FLD] = -1;
										pdtbDelSch.Rows.Add(drowOverDelivery);
									}

									//generate child amount
									if (decProduceQty > 0)
									{
										DataRow[] arrChildProducts = pdtbBOM.Select(ITM_BOMTable.PRODUCTID_FLD + "=" + intProductID);
										foreach(DataRow drowChildProduct in arrChildProducts)
										{
											DataRow drowChildDelivery = pdtbDelSch.NewRow();
											decimal decShrink = Convert.ToDecimal(drowChildProduct[ITM_BOMTable.SHRINK_FLD]);
											drowChildDelivery[SO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD] = Decimal.Round(decProduceQty * (Convert.ToDecimal(drowChildProduct[ITM_BOMTable.QUANTITY_FLD])) / (1 - decShrink / 100),0);
											drowChildDelivery[SO_DeliveryScheduleTable.SCHEDULEDATE_FLD] = dtmBottleStartTime.AddSeconds(-Convert.ToDouble(drowChildProduct[ITM_BOMTable.LEADTIMEOFFSET_FLD])) ;
											drowChildDelivery[SO_SaleOrderDetailTable.PRODUCTID_FLD] = drowChildProduct[ITM_BOMTable.COMPONENTID_FLD];
											drowChildDelivery[ITM_RoutingTable.ROUTINGID_FLD] = drowChildProduct[ITM_RoutingTable.ROUTINGID_FLD];
											drowChildDelivery[MST_WorkCenterTable.WORKCENTERID_FLD] = drowChildProduct[MST_WorkCenterTable.WORKCENTERID_FLD];
											drowChildDelivery[WORKCENTERCODE_FLD] = drowChildProduct[WORKCENTERCODE_FLD];
											drowChildDelivery[PRO_CheckPointTable.SAMPLEPATTERN_FLD] = drowChildProduct[PRO_CheckPointTable.SAMPLEPATTERN_FLD];
											drowChildDelivery[PRO_CheckPointTable.SAMPLERATE_FLD] = drowChildProduct[PRO_CheckPointTable.SAMPLERATE_FLD];
											drowChildDelivery[PRO_CheckPointTable.DELAYTIME_FLD] = drowChildProduct[PRO_CheckPointTable.DELAYTIME_FLD];
											drowChildDelivery[LEADTIME_FLD] = drowChildProduct[LEADTIME_FLD];
											drowChildDelivery[ITM_RoutingTable.FIXLT_FLD] = drowChildProduct[ITM_RoutingTable.FIXLT_FLD];
											drowChildDelivery[CAPACITYBOTTLEID_FLD] = -2;
											drowChildDelivery[MINPRODUCE_FLD] = drowChildProduct[MINPRODUCE_FLD];
											drowChildDelivery[MAXPRODUCE_FLD] = drowChildProduct[MAXPRODUCE_FLD];
											drowChildDelivery[CHECKPOINTPERITEM_FLD] = drowChildProduct[CHECKPOINTPERITEM_FLD];
											drowChildDelivery[ITM_ProductTable.SCRAPPERCENT_FLD] = drowChildProduct[ITM_ProductTable.SCRAPPERCENT_FLD];
											drowChildDelivery[ITM_ProductTable.ORDERQUANTITYMULTIPLE_FLD] = drowChildProduct[ITM_ProductTable.ORDERQUANTITYMULTIPLE_FLD];
											drowChildDelivery[ITM_ProductTable.REVISION_FLD] = drowChildProduct[ITM_ProductTable.REVISION_FLD];
											drowChildDelivery[MAXROUNDUPTOMIN_FLD] = drowChildProduct[MAXROUNDUPTOMIN_FLD];
											drowChildDelivery[MAXROUNDUPTOMULTIPLE_FLD] = drowChildProduct[MAXROUNDUPTOMULTIPLE_FLD];
											pdtbDelSch.Rows.Add(drowChildDelivery);
										}
									}
									arrProcessed[arrProductID.IndexOf(intProductID)] = true;
									#endregion Single processing
								}
								
								#region Recalculate factor
								if (blnRecalculateFactor && blnBalancePlanning)
								{
									decRequiredCapacity = 0;
									for (int i = intFinalOrderIdx + 1; i < arrFinalOrder.Count; i++)
									{										
										if (arrProcessed[arrProductID.IndexOf(arrFinalOrder[i])])
										{
											continue;
										}
										int intProductID1 = Convert.ToInt32(arrFinalOrder[i]);
										int intIdx1 = arrProductID.IndexOf(intProductID1);
										DataRow drowDeliveryData1 = (DataRow)arrDeliveryRows[intIdx1];
										decimal decDeliveryQty1 = Convert.ToDecimal(arrDeliveryQuantities[intIdx1]);
										decimal decCheckpointPerItem1 = Convert.ToDecimal(drowDeliveryData1[CHECKPOINTPERITEM_FLD]);
										decimal decLeadTime1 = Convert.ToDecimal(drowDeliveryData1[LEADTIME_FLD]);
										decimal decTotalRealTime1 = (decDeliveryQty1 * decLeadTime1) * (decBottleTotalWorkingTime / decBottleTotalCapacity);
										decimal decCheckPointTime1 = 0;
										if (blnIncludeCheckpoint)
										{
											decCheckPointTime1 = Math.Round(decCheckpointPerItem1 * decDeliveryQty1); //CalculateCheckpointTime(drowDeliveryData1,decDeliveryQty1,decTotalRealTime1);
										}
										decRequiredCapacity += decLeadTime1 * decDeliveryQty1 + decCheckPointTime1;// * (decBottleTotalCapacity / decBottleTotalWorkingTime);
									}

									if ((decRemainCapacity < decRequiredCapacity) && (decRequiredCapacity != 0))
									{
										decFactor = decRemainCapacity / decRequiredCapacity;
									}
									else
									{
										decFactor = 1;
									}
								}
								#endregion recalculate factor								
								
								intFinalOrderIdx++;
							}
							
							if (decRemainCapacity < 0)
							{
								decRemainCapacity = 0;
							}
							drowBottle[BOTTLEREMAINCAPACITY_FLD] = decRemainCapacity;
						}
						#endregion Calculate produce quantity and order
					}
					#endregion
				}
				else
				{
					#region Outside processing
					
					//select all delivery
					arrDeliveries = pdtbDelSch.Select(MST_WorkCenterTable.WORKCENTERID_FLD + " = " + intWorkCenterID +
						" AND " + SO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD + " > 0",
						SO_SaleOrderDetailTable.PRODUCTID_FLD + "," + SO_DeliveryScheduleTable.SCHEDULEDATE_FLD + " DESC" );
					if (arrDeliveries.Length > 0)
					{
						int intGroupProductID = Convert.ToInt32(arrDeliveries[0][SO_SaleOrderDetailTable.PRODUCTID_FLD]);
						DateTime dtmGroupScheduleDate = Convert.ToDateTime(arrDeliveries[0][SO_DeliveryScheduleTable.SCHEDULEDATE_FLD]);
						decimal decGroupQuantity = 0;
						DataRow[] arrChildProducts;
						DateTime dtmGroupStartDate;
						decimal decFixLT;
						int intRoutingID;
						int intCurrentProductID;
						DataRow drowCurrentDelivery = null;
						DateTime dtmCurrentScheduleDate;
						for (int intIdx = 0; intIdx < arrDeliveries.Length; intIdx++)
						{
							drowCurrentDelivery = arrDeliveries[intIdx];
							drowCurrentDelivery[CAPACITYBOTTLEID_FLD] = 1;
							intCurrentProductID = Convert.ToInt32(drowCurrentDelivery[SO_SaleOrderDetailTable.PRODUCTID_FLD]);
							dtmCurrentScheduleDate = Convert.ToDateTime(drowCurrentDelivery[SO_DeliveryScheduleTable.SCHEDULEDATE_FLD]);
							if ((intCurrentProductID != intGroupProductID) || (dtmCurrentScheduleDate != dtmGroupScheduleDate))
							{
								//end current group								
								//add raw result
								decFixLT = Convert.ToDecimal(drowCurrentDelivery[ITM_RoutingTable.FIXLT_FLD]);
								intRoutingID = Convert.ToInt32(drowCurrentDelivery[ITM_RoutingTable.ROUTINGID_FLD]);
								dtmGroupStartDate = dtmGroupScheduleDate.AddSeconds(-Convert.ToDouble(decFixLT));
								AddRawResult(pdtbRawResult,intGroupProductID,0,decGroupQuantity,0,decFixLT,intRoutingID,intWorkCenterID,strWorkCenterCode,0,0,0,dtmGroupStartDate,0,0);

								//generate child
								arrChildProducts = pdtbBOM.Select(ITM_BOMTable.PRODUCTID_FLD + "=" + intGroupProductID);
								foreach(DataRow drowChildProduct in arrChildProducts)
								{
									DataRow drowChildDelivery = pdtbDelSch.NewRow();
									decimal decShrink = Convert.ToDecimal(drowChildProduct[ITM_BOMTable.SHRINK_FLD]);
									drowChildDelivery[SO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD] = Decimal.Round(decGroupQuantity * Convert.ToDecimal(drowChildProduct[ITM_BOMTable.QUANTITY_FLD]) / (1-decShrink/100),0);
									drowChildDelivery[SO_DeliveryScheduleTable.SCHEDULEDATE_FLD] = dtmGroupStartDate.AddSeconds(-Convert.ToDouble(drowChildProduct[ITM_BOMTable.LEADTIMEOFFSET_FLD]));
									drowChildDelivery[SO_SaleOrderDetailTable.PRODUCTID_FLD] = drowChildProduct[ITM_BOMTable.COMPONENTID_FLD];
									drowChildDelivery[ITM_RoutingTable.ROUTINGID_FLD] = drowChildProduct[ITM_RoutingTable.ROUTINGID_FLD];
									drowChildDelivery[MST_WorkCenterTable.WORKCENTERID_FLD] = drowChildProduct[MST_WorkCenterTable.WORKCENTERID_FLD];
									drowChildDelivery[WORKCENTERCODE_FLD] = drowChildProduct[WORKCENTERCODE_FLD];
									drowChildDelivery[PRO_CheckPointTable.SAMPLEPATTERN_FLD] = drowChildProduct[PRO_CheckPointTable.SAMPLEPATTERN_FLD];
									drowChildDelivery[PRO_CheckPointTable.SAMPLERATE_FLD] = drowChildProduct[PRO_CheckPointTable.SAMPLERATE_FLD];
									drowChildDelivery[PRO_CheckPointTable.DELAYTIME_FLD] = drowChildProduct[PRO_CheckPointTable.DELAYTIME_FLD];
									drowChildDelivery[LEADTIME_FLD] = drowChildProduct[LEADTIME_FLD];
									drowChildDelivery[ITM_RoutingTable.FIXLT_FLD] = drowChildProduct[ITM_RoutingTable.FIXLT_FLD];
									drowChildDelivery[MINPRODUCE_FLD] = drowChildProduct[MINPRODUCE_FLD];
									drowChildDelivery[MAXPRODUCE_FLD] = drowChildProduct[MAXPRODUCE_FLD];
									drowChildDelivery[CHECKPOINTPERITEM_FLD] = drowChildProduct[CHECKPOINTPERITEM_FLD];
									drowChildDelivery[ITM_ProductTable.SCRAPPERCENT_FLD] = drowChildProduct[ITM_ProductTable.SCRAPPERCENT_FLD];
									drowChildDelivery[ITM_ProductTable.ORDERQUANTITYMULTIPLE_FLD] = drowChildProduct[ITM_ProductTable.ORDERQUANTITYMULTIPLE_FLD];
									drowChildDelivery[ITM_ProductTable.REVISION_FLD] = drowChildProduct[ITM_ProductTable.REVISION_FLD];
									drowChildDelivery[MAXROUNDUPTOMIN_FLD] = drowChildProduct[MAXROUNDUPTOMIN_FLD];
									drowChildDelivery[MAXROUNDUPTOMULTIPLE_FLD] = drowChildProduct[MAXROUNDUPTOMULTIPLE_FLD];
									drowChildDelivery[CAPACITYBOTTLEID_FLD] = -2;
									pdtbDelSch.Rows.Add(drowChildDelivery);
								}

								//begin another group
								intGroupProductID = intCurrentProductID;
								dtmGroupScheduleDate = dtmCurrentScheduleDate;
								decGroupQuantity = 0;
							}
							decimal decCurrentQuantity = Convert.ToDecimal(drowCurrentDelivery[SO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD]);
							decimal decScraptPercent = Convert.ToDecimal(drowCurrentDelivery[ITM_ProductTable.SCRAPPERCENT_FLD]);
							//calculate scrapt percent
							decGroupQuantity += Decimal.Round((decCurrentQuantity / (1-decScraptPercent/100)),0);
						}
						//end last group								
						//add raw result
						decFixLT = Convert.ToDecimal(drowCurrentDelivery[ITM_RoutingTable.FIXLT_FLD]);
						intRoutingID = Convert.ToInt32(drowCurrentDelivery[ITM_RoutingTable.ROUTINGID_FLD]);
						dtmGroupStartDate = dtmGroupScheduleDate.AddSeconds(-Convert.ToDouble(decFixLT));
						AddRawResult(pdtbRawResult,intGroupProductID,0,decGroupQuantity,0,decFixLT,intRoutingID,intWorkCenterID,strWorkCenterCode,0,0,0,dtmGroupStartDate,0,0);

						//generate child
						arrChildProducts = pdtbBOM.Select(ITM_BOMTable.PRODUCTID_FLD + "=" + intGroupProductID);
						foreach(DataRow drowChildProduct in arrChildProducts)
						{
							DataRow drowChildDelivery = pdtbDelSch.NewRow();
							decimal decShrink = Convert.ToDecimal(drowChildProduct[ITM_BOMTable.SHRINK_FLD]);
							drowChildDelivery[SO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD] = Decimal.Round(decGroupQuantity * Convert.ToDecimal(drowChildProduct[ITM_BOMTable.QUANTITY_FLD]) / (1-decShrink/100),0);
							drowChildDelivery[SO_DeliveryScheduleTable.SCHEDULEDATE_FLD] = dtmGroupStartDate;
							drowChildDelivery[SO_SaleOrderDetailTable.PRODUCTID_FLD] = drowChildProduct[ITM_BOMTable.COMPONENTID_FLD];
							drowChildDelivery[ITM_RoutingTable.ROUTINGID_FLD] = drowChildProduct[ITM_RoutingTable.ROUTINGID_FLD];
							drowChildDelivery[MST_WorkCenterTable.WORKCENTERID_FLD] = drowChildProduct[MST_WorkCenterTable.WORKCENTERID_FLD];
							drowChildDelivery[WORKCENTERCODE_FLD] = drowChildProduct[WORKCENTERCODE_FLD];
							drowChildDelivery[PRO_CheckPointTable.SAMPLEPATTERN_FLD] = drowChildProduct[PRO_CheckPointTable.SAMPLEPATTERN_FLD];
							drowChildDelivery[PRO_CheckPointTable.SAMPLERATE_FLD] = drowChildProduct[PRO_CheckPointTable.SAMPLERATE_FLD];
							drowChildDelivery[PRO_CheckPointTable.DELAYTIME_FLD] = drowChildProduct[PRO_CheckPointTable.DELAYTIME_FLD];
							drowChildDelivery[LEADTIME_FLD] = drowChildProduct[LEADTIME_FLD];
							drowChildDelivery[ITM_RoutingTable.FIXLT_FLD] = drowChildProduct[ITM_RoutingTable.FIXLT_FLD];
							drowChildDelivery[MINPRODUCE_FLD] = drowChildProduct[MINPRODUCE_FLD];
							drowChildDelivery[MAXPRODUCE_FLD] = drowChildProduct[MAXPRODUCE_FLD];
							drowChildDelivery[CHECKPOINTPERITEM_FLD] = drowChildProduct[CHECKPOINTPERITEM_FLD];
							drowChildDelivery[ITM_ProductTable.SCRAPPERCENT_FLD] = drowChildProduct[ITM_ProductTable.SCRAPPERCENT_FLD];
							drowChildDelivery[ITM_ProductTable.ORDERQUANTITYMULTIPLE_FLD] = drowChildProduct[ITM_ProductTable.ORDERQUANTITYMULTIPLE_FLD];
							drowChildDelivery[MAXROUNDUPTOMIN_FLD] = drowChildProduct[MAXROUNDUPTOMIN_FLD];
							drowChildDelivery[MAXROUNDUPTOMULTIPLE_FLD] = drowChildProduct[MAXROUNDUPTOMULTIPLE_FLD];
							drowChildDelivery[CAPACITYBOTTLEID_FLD] = -2;
							pdtbDelSch.Rows.Add(drowChildDelivery);
						}
					}
					#endregion
				}
				#endregion Assign deliveries to bottles

				//adjust bottles for safetystock
				#region Adjust for safetystock
				if ((true) && (blnSafetyStock) && (!blnOutside))
				{
					#region	first, expand current produce				
					//walk through bottles
					DataRow[] arrCapacityBottles = pdstCapacityBottles.Tables[strWorkCenterCode].Select(string.Empty,BOTTLESTARTTIME_FLD + " ASC");
					foreach (DataRow drowBottle in arrCapacityBottles)
					{
						decimal decRemainCapacity = Convert.ToDecimal(drowBottle[BOTTLEREMAINCAPACITY_FLD]);

						if (decRemainCapacity <= 0)
						{
							continue;
						}
						int intCapacityBottleID = Convert.ToInt32(drowBottle[CAPACITYBOTTLEID_FLD]);
						decimal decBottleCapacity = Convert.ToDecimal(drowBottle[BOTTLETOTALCAPACITY_FLD]);
						decimal decBottleWorkTime = Convert.ToDecimal(drowBottle[BOTTLETOTALWORKTIME_FLD]);
						DateTime dtmBottleStartTime = Convert.ToDateTime(drowBottle[BOTTLESTARTTIME_FLD]);

						if (dtmBottleStartTime < dtmPlanningStartDate)
						{
							continue;
						}

						//products list
						DataRow[] arrRawResult = pdtbRawResult.Select(RAWRESULTBOTTLEID_FLD + "=" + intCapacityBottleID + " AND " + RAWRESULTWORKCENTERID_FLD + "=" + intWorkCenterID);
						//calculate require leadtime for all safetystock
						decimal decRequiredLeadTimeSS = 0;

						//build pair product delivery lookup table
						Hashtable htPair = new Hashtable();
						for (int i = 0; i < arrRawResult.Length; i++)
						{
							int intProductID = Convert.ToInt32(arrRawResult[i][RAWRESULTPRODUCTID_FLD]);
							//find its pair
							DataRow[] arrPairProduct = pdtbProductPair.Select("ProductID1 = " + intProductID);
							if (arrPairProduct.Length > 0)
							{
								int intPairProductID = Convert.ToInt32(arrPairProduct[0]["ProductID2"]);
								//check if pair product produced
								htPair[intPairProductID] = intProductID;
								htPair[intProductID] = intPairProductID;
							}
						}

						foreach (DataRow drowRawResult in arrRawResult)
						{
							int intProductID = Convert.ToInt32(drowRawResult[RAWRESULTPRODUCTID_FLD]);
							DataRow drowProduct = pdtbProduct.Select(ITM_ProductTable.PRODUCTID_FLD + "=" + intProductID)[0];
							decimal decSafetyStock = Convert.ToDecimal(drowProduct[ITM_ProductTable.SAFETYSTOCK_FLD]);

							int intPairProductID;
							DataRow drowPairProduct = null;
							decimal decPairSafetyStock = 0;

							//check pair
							bool blnPair = (htPair[intProductID] != null);
							if (blnPair)
							{
								intPairProductID = Convert.ToInt32(htPair[intProductID]);
								try
								{
									drowPairProduct = pdtbProduct.Select(ITM_ProductTable.PRODUCTID_FLD + "=" + intPairProductID)[0];
									decPairSafetyStock = Convert.ToDecimal(drowPairProduct[ITM_ProductTable.SAFETYSTOCK_FLD]);
								}
								catch (Exception ex) 
								{
									ex.ToString();
								}
								if (decPairSafetyStock > decSafetyStock) 
								{
									decSafetyStock = decPairSafetyStock;
								}
							}

							if (decSafetyStock <= 0)
							{
								continue;
							}
							decimal decLeadTime = Convert.ToDecimal(drowProduct[LEADTIME_FLD]);
							decimal decCheckpointPerItem = Convert.ToDecimal(drowProduct[CHECKPOINTPERITEM_FLD]);
							int intSamplePattern = 0;
							decimal decSampleRate = 0;
							decimal decDelayTime = 0;
							try
							{
								intSamplePattern = Convert.ToInt32(drowProduct[PRO_CheckPointTable.SAMPLEPATTERN_FLD]);
								decSampleRate = Convert.ToDecimal(drowProduct[PRO_CheckPointTable.SAMPLERATE_FLD]);
								decDelayTime = Convert.ToDecimal(drowProduct[PRO_CheckPointTable.DELAYTIME_FLD]);
							}
							catch 
							{
								intSamplePattern = 0;
							}
							decRequiredLeadTimeSS += decLeadTime * decSafetyStock;
							if (blnIncludeCheckpoint && (intSamplePattern != 0))
							{
								decRequiredLeadTimeSS += Math.Round(decSafetyStock * decCheckpointPerItem);
							}
						}
						if (decRequiredLeadTimeSS == 0)
						{
							continue;
						}
						decimal decFactor = 1;
						if (blnBalancePlanning)
						{
							decFactor = decRemainCapacity / decRequiredLeadTimeSS;
						}
						if (decFactor > 1)
						{
							decFactor = 1;
						}

						if (decRemainCapacity - decRequiredLeadTimeSS > 0)
						{
							drowBottle[BOTTLEREMAINCAPACITY_FLD] = decRemainCapacity - decRequiredLeadTimeSS;
						}
						else
						{
							drowBottle[BOTTLEREMAINCAPACITY_FLD] = 0;
						}

						bool blnRecalculateFactor = false;
						int intRawResultIdx = 0;

						bool[] arr1stSafetyStock = new bool[arrRawResult.Length];
						for (int i = 0; i < arr1stSafetyStock.Length; i++)
						{
							arr1stSafetyStock[i] = false;
						}
						foreach (DataRow drowRawResult in arrRawResult)
						{
							//check if processed, continue
							if (arr1stSafetyStock[intRawResultIdx]) 
							{
								intRawResultIdx++;
								continue;
							}

							int intProductID = Convert.ToInt32(drowRawResult[RAWRESULTPRODUCTID_FLD]);
							DataRow drowProduct = pdtbProduct.Select(ITM_ProductTable.PRODUCTID_FLD + "=" + intProductID)[0];
							decimal decSafetyStock = Convert.ToDecimal(drowProduct[ITM_ProductTable.SAFETYSTOCK_FLD]);

							int intPairProductID = 0;
							DataRow drowPairProduct = null;
							decimal decPairSafetyStock;
							DataRow drowPairRawResult = null;

							decimal decProduceQuantity = Convert.ToDecimal(drowRawResult[RAWRESULTPRODUCEQUANTITY_FLD]);
							decimal decPairProduceQuantity = 0;

							//check pair
                            bool blnPair = (htPair[intProductID] != null);
							if (blnPair)
							{
								try
								{
									intPairProductID = Convert.ToInt32(htPair[intProductID]);
									drowPairRawResult = pdtbRawResult.Select(RAWRESULTBOTTLEID_FLD + "=" + intCapacityBottleID + " AND " + RAWRESULTWORKCENTERID_FLD + "=" + intWorkCenterID + " AND " + RAWRESULTPRODUCTID_FLD + "=" + intPairProductID)[0];
									drowPairProduct = pdtbProduct.Select(ITM_ProductTable.PRODUCTID_FLD + "=" + intPairProductID)[0];
									decPairSafetyStock = Convert.ToDecimal(drowPairProduct[ITM_ProductTable.SAFETYSTOCK_FLD]);
									decPairProduceQuantity = Convert.ToDecimal(drowPairRawResult[RAWRESULTPRODUCEQUANTITY_FLD]);
									if (decPairSafetyStock > decSafetyStock) 
									{
										decSafetyStock = decPairSafetyStock;
									}
									//double quantity
									decSafetyStock *= 2;

								}
								catch  (Exception ex)
								{
									intRawResultIdx ++;
									continue;
								}
							}

							if (decSafetyStock == 0)
							{
								intRawResultIdx++;
								continue;
							}
							
							//increase quantity
							decimal decAdditionQuantity = Decimal.Floor(decSafetyStock * decFactor);
							decimal decMinProduce = Convert.ToDecimal(drowProduct[MINPRODUCE_FLD]);
							decimal decMaxProduce = Convert.ToDecimal(drowProduct[MAXPRODUCE_FLD]);
							decimal decMultiple = Convert.ToDecimal(drowProduct[ITM_ProductTable.ORDERQUANTITYMULTIPLE_FLD]);
							decimal decLeadTime = Convert.ToDecimal(drowProduct[LEADTIME_FLD]);
							decimal decCheckpointPerItem = Convert.ToDecimal(drowProduct[CHECKPOINTPERITEM_FLD]);
							if (decMultiple == 0) 
							{
								decMultiple = 1;
							}

							decimal decPairProduced = decProduceQuantity;
							if (blnPair) 
							{
								decMultiple *= 2; //pair
								decMinProduce *= 2;
								decMaxProduce *= 2;

								if (decProduceQuantity < decPairProduceQuantity)
								{
									decPairProduced = decPairProduceQuantity * 2;
								}
								else
								{
									decPairProduced = decProduceQuantity * 2;
								}
							}

							//if produce quantity greater than max available, reduce it to fit
							if ((decLeadTime + decCheckpointPerItem) * decAdditionQuantity > decRemainCapacity) 
							{
								decAdditionQuantity = decimal.Floor(decRemainCapacity / (decLeadTime + decCheckpointPerItem));
							}
						
							#region calculate actual max produce base on production group
							//first get all production group that contains current product
							DataRow[] arrProductionGroups = pdtbProductionGroup.Select(PRO_PGProductTable.PRODUCTID_FLD + "=" + intProductID);
								
							foreach (DataRow drowProductionGroup in arrProductionGroups) 
							{
								decimal decProducedInGroup = 0;
								decimal decGroupMaxProduce = Convert.ToDecimal(drowProductionGroup[PRO_ProductionGroupTable.GROUPPRODUCTIONMAX_FLD]);

								int intProductionGroupID = Convert.ToInt32(drowProductionGroup[PRO_PGProductTable.PRODUCTIONGROUPID_FLD]);
								//select all product in this group
								DataRow[] arrProductInPGs = pdtbProductionGroup.Select(PRO_PGProductTable.PRODUCTIONGROUPID_FLD + "=" + intProductionGroupID);
								//walk through all product and collect produce quantity in current bottle
								foreach (DataRow drowProductInPG in arrProductInPGs)
								{
									int intProductInPGID = Convert.ToInt32(drowProductInPG[PRO_PGProductTable.PRODUCTID_FLD]);
									/*if ((intProductInPGID == intProductID) || (intProductInPGID == intPairProductID))
									{
										continue;
									}*/
									//find rawresult
									string strFilter = RAWRESULTBOTTLEID_FLD + "=" + intCapacityBottleID + " AND " + RAWRESULTPRODUCTID_FLD + "=" + intProductInPGID;
									object objResult = pdtbRawResult.Compute("SUM(" + RAWRESULTPRODUCEQUANTITY_FLD + ")",strFilter);
									decProducedInGroup += (objResult == DBNull.Value) ? (0) : (Convert.ToDecimal(objResult));
								}
								if (decMaxProduce > 0)
								{
									decMaxProduce = (decGroupMaxProduce - decProducedInGroup < decMaxProduce) ? decGroupMaxProduce - decProducedInGroup : decMaxProduce;
								}
								else
								{
									decMaxProduce = decGroupMaxProduce - decProducedInGroup;
								}
								if (decMaxProduce == 0) 
								{
									decMaxProduce = -1;
								}
							}

							if (decMaxProduce < 0) 
							{
								//serious fault, not allow any value except -1
								decMaxProduce.ToString();
							}
							#endregion calculate actual max produce base on production group

							int intMultiple = Convert.ToInt32(decMultiple);
							int intMaxProduce = Convert.ToInt32(decMaxProduce);
							int intMinProduce = Convert.ToInt32(decMinProduce);

							//normalize max, min base on multiple
							if ((intMultiple != 0) && (decMaxProduce > 0))
							{
								decMaxProduce = (intMaxProduce / intMultiple) * intMultiple;
								if (decMaxProduce == 0)
								{
									decMaxProduce = -1;
								}
							}
							if ((intMultiple != 0) && (intMinProduce % intMultiple != 0))
							{
								decMinProduce = (intMinProduce / intMultiple + 1) * intMultiple;
							}
							
							if ((decMaxProduce == -1) || ((decMaxProduce > 0) && (decMaxProduce < decMinProduce)))
							{
								decAdditionQuantity = 0;
								blnRecalculateFactor = true;
							}
							else
							{
								if ((decMaxProduce > 0) && (decAdditionQuantity + decPairProduced > decMaxProduce))
								{
									decAdditionQuantity = decMaxProduce - decPairProduced;
									if (decAdditionQuantity < 0)
									{
										decAdditionQuantity = 0; //1.ToString();
									}
									blnRecalculateFactor = true;
								}

								int intProduceQty = Convert.ToInt32(decAdditionQuantity);
								if (intProduceQty % intMultiple != 0)
								{
									if (((intProduceQty / intMultiple + 1) * intMultiple + decProduceQuantity <= decMaxProduce) && (decLeadTime * (intProduceQty / intMultiple + 1) * decMultiple + Math.Round(decCheckpointPerItem * (intProduceQty / intMultiple + 1) * decMultiple) < decRemainCapacity))
									{
										decAdditionQuantity = (intProduceQty / intMultiple + 1) * intMultiple;
									}
									else
									{
										decAdditionQuantity = (intProduceQty / intMultiple) * intMultiple;
									}
									blnRecalculateFactor = true;
								}
							}
							decimal decCheckpointTime = 0;
							if (blnIncludeCheckpoint)
							{
								decCheckpointTime = Math.Round(decCheckpointPerItem * decAdditionQuantity); //CalculateCheckpointTime(drowProduct,decAdditionQuantity,decLeadTime * decAdditionQuantity * (decBottleWorkTime / decBottleCapacity));
							}
							else
							{
								decCheckpointTime = 0;
							}
						
							decRemainCapacity -= decAdditionQuantity * decLeadTime + decCheckpointTime;// * (decBottleCapacity / decBottleWorkTime);

							if (blnPair)
							{
								drowRawResult[RAWRESULTPRODUCEQUANTITY_FLD] = decProduceQuantity + decAdditionQuantity / 2;
								drowPairRawResult[RAWRESULTPRODUCEQUANTITY_FLD] = decPairProduceQuantity + decAdditionQuantity / 2;
								drowRawResult[RAWRESULTSAFETYSTOCKAMOUNT_FLD] = decAdditionQuantity / 2;
							}
							else
							{
								drowRawResult[RAWRESULTPRODUCEQUANTITY_FLD] = decProduceQuantity + decAdditionQuantity;
								drowRawResult[RAWRESULTSAFETYSTOCKAMOUNT_FLD] = decAdditionQuantity;
							}

							if (blnPair)
							{
								decAdditionQuantity /= 2;
							}

							if (Convert.ToDecimal(drowProduct[ITM_ProductTable.SAFETYSTOCK_FLD]) > decAdditionQuantity)
							{
								drowProduct[ITM_ProductTable.SAFETYSTOCK_FLD] = Convert.ToDecimal(drowProduct[ITM_ProductTable.SAFETYSTOCK_FLD]) - decAdditionQuantity;
							}
							else
							{
								drowProduct[ITM_ProductTable.SAFETYSTOCK_FLD] = 0;
							}
							if (blnPair)
							{
								if (Convert.ToDecimal(drowPairProduct[ITM_ProductTable.SAFETYSTOCK_FLD]) > decAdditionQuantity)
								{
									drowPairProduct[ITM_ProductTable.SAFETYSTOCK_FLD] = Convert.ToDecimal(drowPairProduct[ITM_ProductTable.SAFETYSTOCK_FLD]) - decAdditionQuantity;
								}
								else
								{
									drowPairProduct[ITM_ProductTable.SAFETYSTOCK_FLD] = 0;
								}
							}

							//generate child
							if (decAdditionQuantity > 0)
							{
								DataRow[] arrChildProducts = pdtbBOM.Select(ITM_BOMTable.PRODUCTID_FLD + "=" + intProductID);
								foreach(DataRow drowChildProduct in arrChildProducts)
								{
									DataRow drowChildDelivery = pdtbDelSch.NewRow();
									decimal decShrink = Convert.ToDecimal(drowChildProduct[ITM_BOMTable.SHRINK_FLD]);
									drowChildDelivery[SO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD] = Decimal.Round(Convert.ToDecimal(decAdditionQuantity * (Convert.ToDecimal(drowChildProduct[ITM_BOMTable.QUANTITY_FLD]))) / (1-decShrink/100) ,0);
									drowChildDelivery[SO_DeliveryScheduleTable.SCHEDULEDATE_FLD] = dtmBottleStartTime.AddSeconds(-Convert.ToDouble(drowChildProduct[ITM_BOMTable.LEADTIMEOFFSET_FLD])) ;
									drowChildDelivery[SO_SaleOrderDetailTable.PRODUCTID_FLD] = drowChildProduct[ITM_BOMTable.COMPONENTID_FLD];
									drowChildDelivery[ITM_RoutingTable.ROUTINGID_FLD] = drowChildProduct[ITM_RoutingTable.ROUTINGID_FLD];
									drowChildDelivery[MST_WorkCenterTable.WORKCENTERID_FLD] = drowChildProduct[MST_WorkCenterTable.WORKCENTERID_FLD];
									drowChildDelivery[WORKCENTERCODE_FLD] = drowChildProduct[WORKCENTERCODE_FLD];
									drowChildDelivery[PRO_CheckPointTable.SAMPLEPATTERN_FLD] = drowChildProduct[PRO_CheckPointTable.SAMPLEPATTERN_FLD];
									drowChildDelivery[PRO_CheckPointTable.SAMPLERATE_FLD] = drowChildProduct[PRO_CheckPointTable.SAMPLERATE_FLD];
									drowChildDelivery[PRO_CheckPointTable.DELAYTIME_FLD] = drowChildProduct[PRO_CheckPointTable.DELAYTIME_FLD];
									drowChildDelivery[LEADTIME_FLD] = drowChildProduct[LEADTIME_FLD];
									drowChildDelivery[ITM_RoutingTable.FIXLT_FLD] = drowChildProduct[ITM_RoutingTable.FIXLT_FLD];
									drowChildDelivery[MINPRODUCE_FLD] = drowChildProduct[MINPRODUCE_FLD];
									drowChildDelivery[MAXPRODUCE_FLD] = drowChildProduct[MAXPRODUCE_FLD];
									drowChildDelivery[CHECKPOINTPERITEM_FLD] = drowChildProduct[CHECKPOINTPERITEM_FLD];
									drowChildDelivery[ITM_ProductTable.SCRAPPERCENT_FLD] = drowChildProduct[ITM_ProductTable.SCRAPPERCENT_FLD];
									drowChildDelivery[ITM_ProductTable.ORDERQUANTITYMULTIPLE_FLD] = drowChildProduct[ITM_ProductTable.ORDERQUANTITYMULTIPLE_FLD];
									drowChildDelivery[ITM_ProductTable.REVISION_FLD] = drowChildProduct[ITM_ProductTable.REVISION_FLD];
									drowChildDelivery[MAXROUNDUPTOMIN_FLD] = drowChildProduct[MAXROUNDUPTOMIN_FLD];
									drowChildDelivery[MAXROUNDUPTOMULTIPLE_FLD] = drowChildProduct[MAXROUNDUPTOMULTIPLE_FLD];
									drowChildDelivery[CAPACITYBOTTLEID_FLD] = -2;
									pdtbDelSch.Rows.Add(drowChildDelivery);
								}
								if (blnPair)
								{
									arrChildProducts = pdtbBOM.Select(ITM_BOMTable.PRODUCTID_FLD + "=" + intPairProductID);
									foreach(DataRow drowChildProduct in arrChildProducts)
									{
										DataRow drowPairChildDelivery = pdtbDelSch.NewRow();
										decimal decShrink = Convert.ToDecimal(drowChildProduct[ITM_BOMTable.SHRINK_FLD]);
										drowPairChildDelivery[SO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD] = Decimal.Round(Convert.ToDecimal(decAdditionQuantity * (Convert.ToDecimal(drowChildProduct[ITM_BOMTable.QUANTITY_FLD]))) / (1-decShrink/100) ,0);
										drowPairChildDelivery[SO_DeliveryScheduleTable.SCHEDULEDATE_FLD] = dtmBottleStartTime.AddSeconds(-Convert.ToDouble(drowChildProduct[ITM_BOMTable.LEADTIMEOFFSET_FLD])) ;
										drowPairChildDelivery[SO_SaleOrderDetailTable.PRODUCTID_FLD] = drowChildProduct[ITM_BOMTable.COMPONENTID_FLD];
										drowPairChildDelivery[ITM_RoutingTable.ROUTINGID_FLD] = drowChildProduct[ITM_RoutingTable.ROUTINGID_FLD];
										drowPairChildDelivery[MST_WorkCenterTable.WORKCENTERID_FLD] = drowChildProduct[MST_WorkCenterTable.WORKCENTERID_FLD];
										drowPairChildDelivery[WORKCENTERCODE_FLD] = drowChildProduct[WORKCENTERCODE_FLD];
										drowPairChildDelivery[PRO_CheckPointTable.SAMPLEPATTERN_FLD] = drowChildProduct[PRO_CheckPointTable.SAMPLEPATTERN_FLD];
										drowPairChildDelivery[PRO_CheckPointTable.SAMPLERATE_FLD] = drowChildProduct[PRO_CheckPointTable.SAMPLERATE_FLD];
										drowPairChildDelivery[PRO_CheckPointTable.DELAYTIME_FLD] = drowChildProduct[PRO_CheckPointTable.DELAYTIME_FLD];
										drowPairChildDelivery[LEADTIME_FLD] = drowChildProduct[LEADTIME_FLD];
										drowPairChildDelivery[ITM_RoutingTable.FIXLT_FLD] = drowChildProduct[ITM_RoutingTable.FIXLT_FLD];
										drowPairChildDelivery[MINPRODUCE_FLD] = drowChildProduct[MINPRODUCE_FLD];
										drowPairChildDelivery[MAXPRODUCE_FLD] = drowChildProduct[MAXPRODUCE_FLD];
										drowPairChildDelivery[CHECKPOINTPERITEM_FLD] = drowChildProduct[CHECKPOINTPERITEM_FLD];
										drowPairChildDelivery[ITM_ProductTable.SCRAPPERCENT_FLD] = drowChildProduct[ITM_ProductTable.SCRAPPERCENT_FLD];
										drowPairChildDelivery[ITM_ProductTable.ORDERQUANTITYMULTIPLE_FLD] = drowChildProduct[ITM_ProductTable.ORDERQUANTITYMULTIPLE_FLD];
										drowPairChildDelivery[ITM_ProductTable.REVISION_FLD] = drowChildProduct[ITM_ProductTable.REVISION_FLD];
										drowPairChildDelivery[MAXROUNDUPTOMIN_FLD] = drowChildProduct[MAXROUNDUPTOMIN_FLD];
										drowPairChildDelivery[MAXROUNDUPTOMULTIPLE_FLD] = drowChildProduct[MAXROUNDUPTOMULTIPLE_FLD];
										drowPairChildDelivery[CAPACITYBOTTLEID_FLD] = -2;
										pdtbDelSch.Rows.Add(drowPairChildDelivery);
									}
								}

							}

							arr1stSafetyStock[intRawResultIdx] = true;
							if (blnPair) 
							{
								arr1stSafetyStock[Array.IndexOf(arrRawResult,drowPairRawResult)] = true;
							}
							//recalculate factor
							if (blnRecalculateFactor && blnBalancePlanning)
							{
								decimal decRequiredCapacity = 0;
								for (int i = intRawResultIdx + 1; i < arrRawResult.Length; i++)
								{
									if (arr1stSafetyStock[i])
									{
										continue;
									}

									int intProductID1 = Convert.ToInt32(arrRawResult[i][RAWRESULTPRODUCTID_FLD]);
									DataRow drowProduct1 = pdtbProduct.Select(ITM_ProductTable.PRODUCTID_FLD + "=" + intProductID1)[0];
									decimal decDeliveryQty1 = Convert.ToDecimal(drowProduct1[ITM_ProductTable.SAFETYSTOCK_FLD]);

									int intPairProductID1;
									DataRow drowPairProduct1;
									decimal decPairDeliveryQty1;

									//check pair
									blnPair = (htPair[intProductID] != null);
									if (blnPair)
									{
										intPairProductID1 = Convert.ToInt32(htPair[intProductID]);
										drowPairProduct1 = pdtbProduct.Select(ITM_ProductTable.PRODUCTID_FLD + "=" + intPairProductID1)[0];
										decPairDeliveryQty1 = Convert.ToDecimal(drowPairProduct1[ITM_ProductTable.SAFETYSTOCK_FLD]);
										if (decPairDeliveryQty1 > decDeliveryQty1) 
										{
											decDeliveryQty1 = decPairDeliveryQty1;
										}
									}

									if (decDeliveryQty1 == 0)
									{
										continue;
									}
									decimal decLeadTime1 = Convert.ToDecimal(drowProduct1[LEADTIME_FLD]);
									decimal decTotalRealTime1 = (decDeliveryQty1 * decLeadTime1) * (decBottleWorkTime / decBottleCapacity);
									decimal decCheckPointTime1 = 0;
									decimal decChecpointPerItem1 = Convert.ToDecimal(drowProduct1[CHECKPOINTPERITEM_FLD]);
									if (blnIncludeCheckpoint)
									{
										decCheckPointTime1 = Math.Round(decChecpointPerItem1 * decDeliveryQty1); //CalculateCheckpointTime(drowProduct1,decDeliveryQty1,decTotalRealTime1);
									}
									decRequiredCapacity += decLeadTime1 * decDeliveryQty1 + decCheckPointTime1;// * (decBottleCapacity / decBottleWorkTime);
								}
								if ((decRemainCapacity < decRequiredCapacity) && (decRequiredCapacity != 0))
								{
									decFactor = decRemainCapacity / decRequiredCapacity;
								}
								else
								{
									decFactor = 1;
								}
							}
							intRawResultIdx ++;
						}
						if (decRemainCapacity < 0)
						{
							1.ToString();
						}

						drowBottle[BOTTLEREMAINCAPACITY_FLD] = decRemainCapacity;
					}
					#endregion

					#region second, use last remaining capacity for remaining last safety stock
					arrProducts = pdtbProduct.Select(MST_WorkCenterTable.WORKCENTERID_FLD + "=" + intWorkCenterID,ITM_ProductTable.REVISION_FLD + " ASC, " + ITM_ProductTable.CATEGORYID_FLD + " ASC ");
					foreach (DataRow drowBottle in arrCapacityBottles)
					{
						//temp disable
						//continue;
						decimal decRemainCapacity = Convert.ToDecimal(drowBottle[BOTTLEREMAINCAPACITY_FLD]);
						int intCapacityBottleID = Convert.ToInt32(drowBottle[CAPACITYBOTTLEID_FLD]);
						decimal decBottleCapacity = Convert.ToDecimal(drowBottle[BOTTLETOTALCAPACITY_FLD]);
						decimal decBottleWorkTime = Convert.ToDecimal(drowBottle[BOTTLETOTALWORKTIME_FLD]);
						DateTime dtmBottleStartTime = Convert.ToDateTime(drowBottle[BOTTLESTARTTIME_FLD]);
						decimal decRequiredLeadTimeSS = 0;

						if (dtmBottleStartTime < dtmPlanningStartDate)
						{
							continue;
						}


						//build pair product delivery lookup table
						Hashtable htPair = new Hashtable();
						for (int i = 0; i < arrProducts.Length; i++)
						{
							int intProductID = Convert.ToInt32(arrProducts[i][ITM_ProductTable.PRODUCTID_FLD]);
							//find its pair
							DataRow[] arrPairProduct = pdtbProductPair.Select("ProductID1 = " + intProductID);
							if (arrPairProduct.Length > 0)
							{
								int intPairProductID = Convert.ToInt32(arrPairProduct[0]["ProductID2"]);
								htPair[intPairProductID] = intProductID;
							}
						}
						foreach (DataRow drowProduct in arrProducts)
						{
							decimal decSafetyStock = Convert.ToDecimal(drowProduct[ITM_ProductTable.SAFETYSTOCK_FLD]);

							int intPairProductID;
							DataRow drowPairProduct;
							decimal decPairSafetyStock;

							//check pair
							bool blnPair = (htPair[drowProduct[ITM_ProductTable.PRODUCTID_FLD]] != null);
							if (blnPair)
							{
								intPairProductID = Convert.ToInt32(htPair[drowProduct[ITM_ProductTable.PRODUCTID_FLD]]);
								drowPairProduct = pdtbProduct.Select(ITM_ProductTable.PRODUCTID_FLD + "=" + intPairProductID)[0];
								decPairSafetyStock = Convert.ToDecimal(drowPairProduct[ITM_ProductTable.SAFETYSTOCK_FLD]);
								if (decPairSafetyStock > decSafetyStock) 
								{
									decSafetyStock = decPairSafetyStock;
								}
							}

							if (decSafetyStock <= 0)
							{
								continue;
							}
							decimal decLeadTime = Convert.ToDecimal(drowProduct[LEADTIME_FLD]);
							decimal decCheckpointPerItem = Convert.ToDecimal(drowProduct[CHECKPOINTPERITEM_FLD]);
							decRequiredLeadTimeSS += decLeadTime * decSafetyStock;
							int intSamplePattern = 0;
							decimal decSampleRate = 0;
							decimal decDelayTime = 0;
							try
							{
								intSamplePattern = Convert.ToInt32(drowProduct[PRO_CheckPointTable.SAMPLEPATTERN_FLD]);
								decSampleRate = Convert.ToDecimal(drowProduct[PRO_CheckPointTable.SAMPLERATE_FLD]);
								decDelayTime = Convert.ToDecimal(drowProduct[PRO_CheckPointTable.DELAYTIME_FLD]);
							}
							catch 
							{
								intSamplePattern = 0;
							}
							if (blnIncludeCheckpoint && (intSamplePattern != 0))
							{
								decRequiredLeadTimeSS += Math.Round(decCheckpointPerItem * decSafetyStock);
							}
						}
						if (decRequiredLeadTimeSS == 0)
						{
							continue;
						}
						
						decimal decFactor = decRemainCapacity / decRequiredLeadTimeSS;
						
						if (decFactor > 1)
						{
							decFactor = 1;
						}
						if (!blnBalancePlanning) 
						{
							decFactor = 1;
						}

						if (decRemainCapacity - decRequiredLeadTimeSS > 0)
						{
							drowBottle[BOTTLEREMAINCAPACITY_FLD] = decRemainCapacity - decRequiredLeadTimeSS;
						}
						else
						{
							drowBottle[BOTTLEREMAINCAPACITY_FLD] = 0;
						}

						//increase produce or new item produce
						bool blnRecalculateFactor = false;
						int intProductIdx = 0;
						bool[] arr2ndSafetyStock = new bool[arrProducts.Length];
						for (int i = 0; i < arr2ndSafetyStock.Length; i++)
						{
							arr2ndSafetyStock[i] = false;
						}
						foreach (DataRow drowProduct in arrProducts)
						{
							//find raw result
							DataRow drowRawResult;
							int intProductID = Convert.ToInt32(drowProduct[ITM_ProductTable.PRODUCTID_FLD]);
							decimal decSafetyStock = Convert.ToDecimal(drowProduct[ITM_ProductTable.SAFETYSTOCK_FLD]);

							int intPairProductID = 0;
							DataRow drowPairProduct = null;
							decimal decPairSafetyStock;
							int intPairRoutingID = 0;

							//check pair
							bool blnPair = (htPair[drowProduct[ITM_ProductTable.PRODUCTID_FLD]] != null);
							if (blnPair)
							{
								intPairProductID = Convert.ToInt32(htPair[drowProduct[ITM_ProductTable.PRODUCTID_FLD]]);
								drowPairProduct = pdtbProduct.Select(ITM_ProductTable.PRODUCTID_FLD + "=" + intPairProductID)[0];
								intPairRoutingID = Convert.ToInt32(drowPairProduct[ITM_RoutingTable.ROUTINGID_FLD]);
								decPairSafetyStock = Convert.ToDecimal(drowPairProduct[ITM_ProductTable.SAFETYSTOCK_FLD]);
								if (decPairSafetyStock > decSafetyStock) 
								{
									decSafetyStock = decPairSafetyStock;
								}

								decSafetyStock *= 2;
							}

							if (decSafetyStock == 0)
							{
								intProductIdx ++;
								arr2ndSafetyStock[Array.IndexOf(arrProducts,drowProduct)] = true;
								if (blnPair)
								{
									arr2ndSafetyStock[Array.IndexOf(arrProducts,drowPairProduct)] = true;
								}
								continue;
							}
							decimal decLeadTime = Convert.ToDecimal(drowProduct[LEADTIME_FLD]);
							int intSamplePattern = Convert.ToInt32(drowProduct[PRO_CheckPointTable.SAMPLEPATTERN_FLD]);
							decimal decSampleRate =  Convert.ToDecimal(drowProduct[PRO_CheckPointTable.SAMPLERATE_FLD]);
							decimal decDelayTime = Convert.ToDecimal(drowProduct[PRO_CheckPointTable.DELAYTIME_FLD]);
							int intRoutingID = Convert.ToInt32(drowProduct[ITM_RoutingTable.ROUTINGID_FLD]);
							decimal decAdditionQuantity = Decimal.Floor(decFactor * decSafetyStock);
							decimal decMinProduce = Convert.ToDecimal(drowProduct[MINPRODUCE_FLD]);
							decimal decMaxProduce = Convert.ToDecimal(drowProduct[MAXPRODUCE_FLD]);
							decimal decMultiple = Convert.ToDecimal(drowProduct[ITM_ProductTable.ORDERQUANTITYMULTIPLE_FLD]);
							decimal decCheckpointPerItem = Convert.ToDecimal(drowProduct[CHECKPOINTPERITEM_FLD]);
							if (decMultiple == 0) 
							{
								decMultiple = 1;
							}

							if (blnPair)
							{
								decMinProduce *= 2;
								decMaxProduce *= 2;
								decMultiple *= 2;
							}

							//if produce quantity greater than max available, reduce it to fit
							if ((decLeadTime + decCheckpointPerItem) * decAdditionQuantity > decRemainCapacity) 
							{
								decAdditionQuantity = decimal.Floor(decRemainCapacity / (decLeadTime + decCheckpointPerItem));
							}


							#region calculate actual max produce base on production group
							//first get all production group that contains current product
							DataRow[] arrProductionGroups = pdtbProductionGroup.Select(PRO_PGProductTable.PRODUCTID_FLD + "=" + intProductID);
								
							foreach (DataRow drowProductionGroup in arrProductionGroups) 
							{
								decimal decProducedInGroup = 0;
								decimal decGroupMaxProduce = Convert.ToDecimal(drowProductionGroup[PRO_ProductionGroupTable.GROUPPRODUCTIONMAX_FLD]);

								int intProductionGroupID = Convert.ToInt32(drowProductionGroup[PRO_PGProductTable.PRODUCTIONGROUPID_FLD]);
								//select all product in this group
								DataRow[] arrProductInPGs = pdtbProductionGroup.Select(PRO_PGProductTable.PRODUCTIONGROUPID_FLD + "=" + intProductionGroupID);
								//walk through all product and collect produce quantity in current bottle
								foreach (DataRow drowProductInPG in arrProductInPGs)
								{
									int intProductInPGID = Convert.ToInt32(drowProductInPG[PRO_PGProductTable.PRODUCTID_FLD]);
									if (intProductInPGID == intProductID) 
									{
										continue;
									}
									//find rawresult
									string strFilter = RAWRESULTBOTTLEID_FLD + "=" + intCapacityBottleID + " AND " + RAWRESULTPRODUCTID_FLD + "=" + intProductInPGID;
									object objResult = pdtbRawResult.Compute("SUM(" + RAWRESULTPRODUCEQUANTITY_FLD + ")",strFilter);
									decProducedInGroup += (objResult == DBNull.Value) ? (0) : (Convert.ToDecimal(objResult));
								}
								if (decMaxProduce > 0)
								{
									decMaxProduce = (decGroupMaxProduce - decProducedInGroup < decMaxProduce) ? decGroupMaxProduce - decProducedInGroup : decMaxProduce;
								}
								else
								{
									decMaxProduce = decGroupMaxProduce - decProducedInGroup;
								}
								if (decMaxProduce == 0) 
								{
									decMaxProduce = -1;
								}
							}

							if (decMaxProduce < 0) 
							{
								//serious fault, not allow any value except -1
								decMaxProduce.ToString();
							}
							#endregion calculate actual max produce base on production group

							int intMultiple = Convert.ToInt32(decMultiple);
							int intMaxProduce = Convert.ToInt32(decMaxProduce);
							int intMinProduce = Convert.ToInt32(decMinProduce);

							//normalize max, min base on multiple
							if ((intMultiple != 0) && (decMaxProduce > 0))
							{
								decMaxProduce = (intMaxProduce / intMultiple) * intMultiple;
								if (decMaxProduce == 0)
								{
									decMaxProduce = -1;
								}
							}
							if ((intMultiple != 0) && (intMinProduce % intMultiple != 0))
							{
								decMinProduce = (intMinProduce / intMultiple + 1) * intMultiple;
							}

							try
							{
								blnRecalculateFactor = true;
								drowRawResult = pdtbRawResult.Select(RAWRESULTBOTTLEID_FLD + "=" + intCapacityBottleID + " AND " + RAWRESULTWORKCENTERID_FLD + "=" + intWorkCenterID + " AND " + RAWRESULTPRODUCTID_FLD + "=" + intProductID)[0];
								decAdditionQuantity	= 0;			
								/*
								if ((decMaxProduce != 0) && (decAdditionQuantity + decProduceQuantity > decMaxProduce))
								{
									decAdditionQuantity = decMaxProduce - decProduceQuantity;
									blnRecalculateFactor = true;
								}
								
								int intProduceQty = Convert.ToInt32(decAdditionQuantity);
								//int intMultiple = Convert.ToInt32(decMultiple);
								if (intProduceQty % intMultiple != 0)
								{
									if (decLeadTime * (intProduceQty / intMultiple + 1) * decMultiple + Math.Round(decCheckpointPerItem * (intProduceQty / intMultiple + 1) * decMultiple) < decRemainCapacity)
									{
										decAdditionQuantity = (intProduceQty / intMultiple + 1) * intMultiple;
									}
									else
									{
										decAdditionQuantity = (intProduceQty / intMultiple) * intMultiple;
									}
									blnRecalculateFactor = true;
								}

								
								drowRawResult[RAWRESULTPRODUCEQUANTITY_FLD] = decProduceQuantity + decAdditionQuantity;
								*/
							}
							catch 
							{
								if ((decMaxProduce == -1) || ((decMaxProduce > 0) && (decMaxProduce < decMinProduce)))
								{
									decAdditionQuantity = 0;
									blnRecalculateFactor = true;
								}
								else
								{
									if ((decMinProduce > 0) && (decAdditionQuantity < decMinProduce) && (decAdditionQuantity > 0))
									{
										if (decLeadTime * decMinProduce + Math.Round(decCheckpointPerItem * (decMinProduce)) <= decRemainCapacity)
										{
											//decRemainCapacity -= (decMinProduce - decAdditionQuantity) * decLeadTime + Math.Round((decMinProduce - decAdditionQuantity) * decCheckpointPerItem);
											//drowBottle[BOTTLEREMAINCAPACITY_FLD] = Convert.ToDecimal(drowBottle[BOTTLEREMAINCAPACITY_FLD]) - (decMinProduce - decAdditionQuantity) * decLeadTime - Math.Round((decMinProduce - decAdditionQuantity) * decCheckpointPerItem);//decRemainCapacity;
											decAdditionQuantity = decMinProduce;
										}
										else
										{
											//drowBottle[BOTTLEREMAINCAPACITY_FLD] = Convert.ToDecimal(drowBottle[BOTTLEREMAINCAPACITY_FLD]) + (decAdditionQuantity) * decLeadTime + Math.Round((decAdditionQuantity) * decCheckpointPerItem);//decRemainCapacity;
											decAdditionQuantity = 0;
										}
										blnRecalculateFactor = true;
									}
									else
									{
										blnRecalculateFactor = false;
									}
									if ((decMaxProduce != 0) && (decAdditionQuantity > decMaxProduce))
									{
										decAdditionQuantity = decMaxProduce;
										blnRecalculateFactor = true;
									}
								
									int intProduceQty = Convert.ToInt32(decAdditionQuantity);
									//int intMultiple = Convert.ToInt32(decMultiple);
									if (intProduceQty % intMultiple != 0)
									{
										if (((intProduceQty / intMultiple + 1) * intMultiple <= decMultiple) && (decLeadTime * (intProduceQty / intMultiple + 1) * decMultiple + Math.Round(decCheckpointPerItem * (intProduceQty / intMultiple + 1) * decMultiple) < decRemainCapacity))
										{
											decAdditionQuantity = (intProduceQty / intMultiple + 1) * intMultiple;
										}
										else
										{
											decAdditionQuantity = (intProduceQty / intMultiple) * intMultiple;
										}
										blnRecalculateFactor = true;
									}
								}

								if (blnPair)
								{
									decAdditionQuantity /= 2;
								}

								if (decAdditionQuantity > 0)
								{
									AddRawResult(pdtbRawResult,intProductID,intCapacityBottleID,decAdditionQuantity,int.MaxValue,decLeadTime,intRoutingID,intWorkCenterID,strWorkCenterCode,intSamplePattern,decSampleRate,decDelayTime,DateTime.MinValue,decCheckpointPerItem,decAdditionQuantity);
									if (blnPair)
									{
										AddRawResult(pdtbRawResult,intPairProductID,intCapacityBottleID,decAdditionQuantity,int.MaxValue,decLeadTime,intPairRoutingID,intWorkCenterID,strWorkCenterCode,intSamplePattern,decSampleRate,decDelayTime,DateTime.MinValue,decCheckpointPerItem,decAdditionQuantity);
									}
								}
							}
							
							arr2ndSafetyStock[Array.IndexOf(arrProducts,drowProduct)] = true;
							if (blnPair)
							{
								arr2ndSafetyStock[Array.IndexOf(arrProducts,drowPairProduct)] = true;
							}

							//update needed stock
							if (Convert.ToDecimal(drowProduct[ITM_ProductTable.SAFETYSTOCK_FLD]) > decAdditionQuantity)
							{
								drowProduct[ITM_ProductTable.SAFETYSTOCK_FLD] = Convert.ToDecimal(drowProduct[ITM_ProductTable.SAFETYSTOCK_FLD]) - decAdditionQuantity;
							}
							else
							{
								drowProduct[ITM_ProductTable.SAFETYSTOCK_FLD] = 0;
							}
							if (blnPair)
							{
								if (Convert.ToDecimal(drowPairProduct[ITM_ProductTable.SAFETYSTOCK_FLD]) > decAdditionQuantity)
								{
									drowPairProduct[ITM_ProductTable.SAFETYSTOCK_FLD] = Convert.ToDecimal(drowPairProduct[ITM_ProductTable.SAFETYSTOCK_FLD]) - decAdditionQuantity;
								}
								else
								{
									drowPairProduct[ITM_ProductTable.SAFETYSTOCK_FLD] = 0;
								}
							}

							if (decAdditionQuantity > 0)
							{

								decimal decCheckpointTime = 0;
								if (blnIncludeCheckpoint)
								{
									decCheckpointTime = decAdditionQuantity * decCheckpointPerItem;//CalculateCheckpointTime(drowProduct,decAdditionQuantity,decLeadTime * decAdditionQuantity * (decBottleWorkTime / decBottleCapacity));
								}
								else
								{
									decCheckpointTime = 0;
								}
								decRemainCapacity -= decAdditionQuantity * decLeadTime + decCheckpointTime;// * (decBottleCapacity / decBottleWorkTime);
								if (blnPair)
								{
									decRemainCapacity -= decAdditionQuantity * decLeadTime + decCheckpointTime;// * (decBottleCapacity / decBottleWorkTime);
								}
								//generate child
								if (decAdditionQuantity > 0)
								{
									DataRow[] arrChildProducts = pdtbBOM.Select(ITM_BOMTable.PRODUCTID_FLD + "=" + intProductID);
									foreach(DataRow drowChildProduct in arrChildProducts)
									{
										DataRow drowChildDelivery = pdtbDelSch.NewRow();
										decimal decShrink = Convert.ToDecimal(drowChildProduct[ITM_BOMTable.SHRINK_FLD]);
										drowChildDelivery[SO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD] = Decimal.Round(decAdditionQuantity * (Convert.ToDecimal(drowChildProduct[ITM_BOMTable.QUANTITY_FLD])) / (1-decShrink/100),0);
										drowChildDelivery[SO_DeliveryScheduleTable.SCHEDULEDATE_FLD] = dtmBottleStartTime.AddSeconds(-Convert.ToDouble(drowChildProduct[ITM_BOMTable.LEADTIMEOFFSET_FLD])) ;
										drowChildDelivery[SO_SaleOrderDetailTable.PRODUCTID_FLD] = drowChildProduct[ITM_BOMTable.COMPONENTID_FLD];
										drowChildDelivery[ITM_RoutingTable.ROUTINGID_FLD] = drowChildProduct[ITM_RoutingTable.ROUTINGID_FLD];
										drowChildDelivery[MST_WorkCenterTable.WORKCENTERID_FLD] = drowChildProduct[MST_WorkCenterTable.WORKCENTERID_FLD];
										drowChildDelivery[WORKCENTERCODE_FLD] = drowChildProduct[WORKCENTERCODE_FLD];
										drowChildDelivery[PRO_CheckPointTable.SAMPLEPATTERN_FLD] = drowChildProduct[PRO_CheckPointTable.SAMPLEPATTERN_FLD];
										drowChildDelivery[PRO_CheckPointTable.SAMPLERATE_FLD] = drowChildProduct[PRO_CheckPointTable.SAMPLERATE_FLD];
										drowChildDelivery[PRO_CheckPointTable.DELAYTIME_FLD] = drowChildProduct[PRO_CheckPointTable.DELAYTIME_FLD];
										drowChildDelivery[LEADTIME_FLD] = drowChildProduct[LEADTIME_FLD];
										drowChildDelivery[ITM_RoutingTable.FIXLT_FLD] = drowChildProduct[ITM_RoutingTable.FIXLT_FLD];
										drowChildDelivery[MINPRODUCE_FLD] = drowChildProduct[MINPRODUCE_FLD];
										drowChildDelivery[MAXPRODUCE_FLD] = drowChildProduct[MAXPRODUCE_FLD];
										drowChildDelivery[CHECKPOINTPERITEM_FLD] = drowChildProduct[CHECKPOINTPERITEM_FLD];
										drowChildDelivery[ITM_ProductTable.SCRAPPERCENT_FLD] = drowChildProduct[ITM_ProductTable.SCRAPPERCENT_FLD];
										drowChildDelivery[ITM_ProductTable.ORDERQUANTITYMULTIPLE_FLD] = drowChildProduct[ITM_ProductTable.ORDERQUANTITYMULTIPLE_FLD];
										drowChildDelivery[ITM_ProductTable.REVISION_FLD] = drowChildProduct[ITM_ProductTable.REVISION_FLD];
										drowChildDelivery[CAPACITYBOTTLEID_FLD] = -2;
										drowChildDelivery[MAXROUNDUPTOMIN_FLD] = drowChildProduct[MAXROUNDUPTOMIN_FLD];
										drowChildDelivery[MAXROUNDUPTOMULTIPLE_FLD] = drowChildProduct[MAXROUNDUPTOMULTIPLE_FLD];
										pdtbDelSch.Rows.Add(drowChildDelivery);
									}
									if (blnPair)
									{
										arrChildProducts = pdtbBOM.Select(ITM_BOMTable.PRODUCTID_FLD + "=" + intPairProductID);
										foreach(DataRow drowChildProduct in arrChildProducts)
										{
											DataRow drowChildDelivery = pdtbDelSch.NewRow();
											decimal decShrink = Convert.ToDecimal(drowChildProduct[ITM_BOMTable.SHRINK_FLD]);
											drowChildDelivery[SO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD] = Decimal.Round(decAdditionQuantity * (Convert.ToDecimal(drowChildProduct[ITM_BOMTable.QUANTITY_FLD])) / (1-decShrink/100),0);
											drowChildDelivery[SO_DeliveryScheduleTable.SCHEDULEDATE_FLD] = dtmBottleStartTime.AddSeconds(-Convert.ToDouble(drowChildProduct[ITM_BOMTable.LEADTIMEOFFSET_FLD])) ;
											drowChildDelivery[SO_SaleOrderDetailTable.PRODUCTID_FLD] = drowChildProduct[ITM_BOMTable.COMPONENTID_FLD];
											drowChildDelivery[ITM_RoutingTable.ROUTINGID_FLD] = drowChildProduct[ITM_RoutingTable.ROUTINGID_FLD];
											drowChildDelivery[MST_WorkCenterTable.WORKCENTERID_FLD] = drowChildProduct[MST_WorkCenterTable.WORKCENTERID_FLD];
											drowChildDelivery[WORKCENTERCODE_FLD] = drowChildProduct[WORKCENTERCODE_FLD];
											drowChildDelivery[PRO_CheckPointTable.SAMPLEPATTERN_FLD] = drowChildProduct[PRO_CheckPointTable.SAMPLEPATTERN_FLD];
											drowChildDelivery[PRO_CheckPointTable.SAMPLERATE_FLD] = drowChildProduct[PRO_CheckPointTable.SAMPLERATE_FLD];
											drowChildDelivery[PRO_CheckPointTable.DELAYTIME_FLD] = drowChildProduct[PRO_CheckPointTable.DELAYTIME_FLD];
											drowChildDelivery[LEADTIME_FLD] = drowChildProduct[LEADTIME_FLD];
											drowChildDelivery[ITM_RoutingTable.FIXLT_FLD] = drowChildProduct[ITM_RoutingTable.FIXLT_FLD];
											drowChildDelivery[MINPRODUCE_FLD] = drowChildProduct[MINPRODUCE_FLD];
											drowChildDelivery[MAXPRODUCE_FLD] = drowChildProduct[MAXPRODUCE_FLD];
											drowChildDelivery[CHECKPOINTPERITEM_FLD] = drowChildProduct[CHECKPOINTPERITEM_FLD];
											drowChildDelivery[ITM_ProductTable.SCRAPPERCENT_FLD] = drowChildProduct[ITM_ProductTable.SCRAPPERCENT_FLD];
											drowChildDelivery[ITM_ProductTable.ORDERQUANTITYMULTIPLE_FLD] = drowChildProduct[ITM_ProductTable.ORDERQUANTITYMULTIPLE_FLD];
											drowChildDelivery[ITM_ProductTable.REVISION_FLD] = drowChildProduct[ITM_ProductTable.REVISION_FLD];
											drowChildDelivery[CAPACITYBOTTLEID_FLD] = -2;
											drowChildDelivery[MAXROUNDUPTOMIN_FLD] = drowChildProduct[MAXROUNDUPTOMIN_FLD];
											drowChildDelivery[MAXROUNDUPTOMULTIPLE_FLD] = drowChildProduct[MAXROUNDUPTOMULTIPLE_FLD];
											pdtbDelSch.Rows.Add(drowChildDelivery);
										}
									}
								}
							}

							//recalculate factor
							if (blnRecalculateFactor && blnBalancePlanning)
							{
								decimal decRequiredCapacity = 0;
								for (int i = intProductIdx + 1; i < arrProducts.Length; i++)
								{
									if (arr2ndSafetyStock[i])
									{
										continue;
									}

									int intProductID1 = Convert.ToInt32(arrProducts[i][ITM_ProductTable.PRODUCTID_FLD]);
									DataRow drowProduct1 = pdtbProduct.Select(ITM_ProductTable.PRODUCTID_FLD + "=" + intProductID1)[0];
									decimal decDeliveryQty1 = Convert.ToDecimal(drowProduct1[ITM_ProductTable.SAFETYSTOCK_FLD]);

									int intPairProductID1;
									DataRow drowPairProduct1;
									decimal decPairDeliveryQty1;

									//check pair
									blnPair = (htPair[intProductID] != null);
									if (blnPair)
									{
										intPairProductID1 = Convert.ToInt32(htPair[intProductID]);
										drowPairProduct1 = pdtbProduct.Select(ITM_ProductTable.PRODUCTID_FLD + "=" + intPairProductID1)[0];
										decPairDeliveryQty1 = Convert.ToDecimal(drowPairProduct1[ITM_ProductTable.SAFETYSTOCK_FLD]);
										if (decPairDeliveryQty1 > decDeliveryQty1) 
										{
											decDeliveryQty1 = decPairDeliveryQty1;
										}
									}

									if (decDeliveryQty1 == 0)
									{
										continue;
									}
										
									decimal decLeadTime1 = Convert.ToDecimal(drowProduct1[LEADTIME_FLD]);
									decimal decCheckpointPerItem1 = Convert.ToDecimal(drowProduct1[CHECKPOINTPERITEM_FLD]);
									decimal decTotalRealTime1 = (decDeliveryQty1 * decLeadTime1) * (decBottleWorkTime / decBottleCapacity);
									decimal decCheckPointTime1 = 0;
									if (blnIncludeCheckpoint)
									{
										decCheckPointTime1 = Math.Round(decCheckpointPerItem1 * decDeliveryQty1); //CalculateCheckpointTime(drowProduct1,decDeliveryQty1,decTotalRealTime1);
									}
									decRequiredCapacity += decLeadTime1 * decDeliveryQty1 + decCheckPointTime1;// * (decBottleCapacity / decBottleWorkTime);
								}
								if ((decRemainCapacity < decRequiredCapacity) && (decRequiredCapacity != 0))
								{
									decFactor = decRemainCapacity / decRequiredCapacity;
								}
								else
								{
									decFactor = 1;
								}
							}
							intProductIdx ++;
						}
						if (decRemainCapacity < 0)
						{
							1.ToString();
						}
						drowBottle[BOTTLEREMAINCAPACITY_FLD] = decRemainCapacity;
					}
					#endregion
				}
				#endregion

			}
			return 0;
		}

		private void CreateBOMTree(DataTable pdtbProduct, DataTable pdtbBOM)
		{
			//Call assign for all top level product
			if (pdtbProduct.Rows.Count > 0)
			{
				int intIdx = 0;
				while (Convert.ToInt32(pdtbProduct.Rows[intIdx][HASPARENT_FLD]) == 0)
				{
					int intTopProductID = Convert.ToInt32(pdtbProduct.Rows[intIdx][ITM_ProductTable.PRODUCTID_FLD]);
					AssignProductLevel(intTopProductID,0,pdtbProduct,pdtbBOM);
					intIdx++;
				}
			}
		}

		private void AssignProductLevel(int pintProductID, int pintLevel, DataTable pdtbProduct, DataTable pdtbBOM)
		{
			DataRow[] arrProducts = pdtbProduct.Select(ITM_ProductTable.PRODUCTID_FLD + "=" + pintProductID);
			if (arrProducts.Length == 0)
			{
				return;
			}
			DataRow drowProduct = arrProducts[0];
			if ((drowProduct[LEVEL_FLD] != DBNull.Value) && (Convert.ToInt32(drowProduct[LEVEL_FLD]) > pintLevel))
			{
				return;
			}
			drowProduct[LEVEL_FLD] = pintLevel;
			DataRow[] arrChildren = pdtbBOM.Select(ITM_BOMTable.PRODUCTID_FLD + "=" + pintProductID);
			foreach (DataRow drowChild in arrChildren)
			{
				int intChildProductID = Convert.ToInt32(drowChild[ITM_BOMTable.COMPONENTID_FLD]);
				AssignProductLevel(intChildProductID,pintLevel + 1,pdtbProduct,pdtbBOM);
			}
		}

		private void AssignCPOToBottles(DataRow pdrowDCOptionMaster, DataTable pdtbCPO, DataTable pdtbProduct, DataTable pdtbBOM, DataSet pdstCapacityBottles, DataTable pdtbWCList, DataTable pdtbWCConfig, DataSet pdstDCPResult)
		{
			int intCurrentLevel = 0;
			DataRow[] arrSameLevelCPO;
			int intOverCapacity = 0;
			do
			{
				arrSameLevelCPO = pdtbCPO.Select(LEVEL_FLD + "=" + intCurrentLevel, MTR_CPOTable.DUEDATE_FLD + " DESC");
				//Loop through CPOs								
				foreach (DataRow drowCPO in arrSameLevelCPO)
				{
					int intWorkCenterID = Convert.ToInt32(drowCPO[MST_WorkCenterTable.WORKCENTERID_FLD]);
					string strWorkCenterCode = drowCPO[WORKCENTERCODE_FLD].ToString();
					int intProductID = Convert.ToInt32(drowCPO[MTR_CPOTable.PRODUCTID_FLD]);
					//first, find suitable bottle to fill
					DataRow[] arrCapacityBottles = pdstCapacityBottles.Tables[strWorkCenterCode].Select(BOTTLEENDTIME_FLD + " < '" + drowCPO[MTR_CPOTable.DUEDATE_FLD] + "' AND " + BOTTLEREMAINCAPACITY_FLD + " > " + RESERVE_CAPACITY, BOTTLESTARTTIME_FLD + " DESC");
					if (arrCapacityBottles.Length > 0)
					{
						drowCPO[CAPACITYBOTTLEID_FLD] = arrCapacityBottles[0][CAPACITYBOTTLEID_FLD];
						decimal decRemainCapacity = 0;
						decimal decTotalLeadtime = Convert.ToDecimal(drowCPO[LEADTIME_FLD])* Convert.ToDecimal(drowCPO[QUANTITY_FLD]);
						//calculate checkpoint time
						decimal decCheckPointTime = 0; //by leadtime
						int intPattern = Convert.ToInt32(drowCPO[PRO_CheckPointTable.SAMPLEPATTERN_FLD]);
						decimal decDelayTime = Convert.ToDecimal(drowCPO[PRO_CheckPointTable.DELAYTIME_FLD]);
						decimal decSampleRate = Convert.ToDecimal(drowCPO[PRO_CheckPointTable.SAMPLERATE_FLD]);
						switch (intPattern)
						{
							case CHECKPOINT_BY_QTY :
							{
								decCheckPointTime = 0;
								break;
							}
							case CHECKPOINT_BY_TIME :
							{
								decCheckPointTime = 0;
								break;
							}
						}
						decimal decQuantity = Convert.ToDecimal(drowCPO[MTR_CPOTable.QUANTITY_FLD])* Convert.ToDecimal(drowCPO[QUANTITY_FLD]);
						decimal decRemainLeadtime = decTotalLeadtime;
						int intBottleIdx = 0;
						
						//insert master result
						Int64 intDCPResultMasterID = AddDCPResultMaster(pdstDCPResult,pdrowDCOptionMaster,drowCPO);

						if (decRemainLeadtime == 0)
						{
							//TODO : outside process
						}
						while (decRemainLeadtime > 0)
						{
							if (intBottleIdx < arrCapacityBottles.Length)
							{
								decRemainCapacity = Convert.ToDecimal(arrCapacityBottles[intBottleIdx][BOTTLEREMAINCAPACITY_FLD]);
								int intCapacityBottleID = Convert.ToInt32(arrCapacityBottles[intBottleIdx][CAPACITYBOTTLEID_FLD]);
								if (decRemainCapacity - RESERVE_CAPACITY < decRemainLeadtime)
								{
									arrCapacityBottles[intBottleIdx][BOTTLEREMAINCAPACITY_FLD] = 0;
									decRemainLeadtime -= decRemainCapacity;
									//insert detail result
									AddDCPResultDetail(pdstDCPResult,intDCPResultMasterID,Convert.ToDateTime(arrCapacityBottles[intBottleIdx][BOTTLEWORKINGDAY_FLD]),Convert.ToDateTime(arrCapacityBottles[intBottleIdx][BOTTLESTARTTIME_FLD]),Convert.ToDateTime(arrCapacityBottles[intBottleIdx][BOTTLEENDTIME_FLD]),decRemainCapacity,(decRemainCapacity / decTotalLeadtime) * decQuantity,(decRemainCapacity / decTotalLeadtime) * 100,intCapacityBottleID,intProductID,strWorkCenterCode);
								}
								else
								{
									arrCapacityBottles[intBottleIdx][BOTTLEREMAINCAPACITY_FLD] = decRemainCapacity - decRemainLeadtime;
									decRemainLeadtime = 0;
									//insert detail result
									AddDCPResultDetail(pdstDCPResult,intDCPResultMasterID,Convert.ToDateTime(arrCapacityBottles[intBottleIdx][BOTTLEWORKINGDAY_FLD]),Convert.ToDateTime(arrCapacityBottles[intBottleIdx][BOTTLESTARTTIME_FLD]),Convert.ToDateTime(arrCapacityBottles[intBottleIdx][BOTTLEENDTIME_FLD]),decTotalLeadtime,decQuantity,100,intCapacityBottleID,intProductID,strWorkCenterCode);
								}
								intBottleIdx++;
							}
							else
							{
								intOverCapacity++;
								//TODO : Over capacity
							}
						}

						AddChildCPOs(drowCPO,pdtbBOM,pdtbCPO,pdtbProduct,Convert.ToDateTime(arrCapacityBottles[0][BOTTLEWORKINGDAY_FLD]));
						//generate child CPO
					}
					else
					{
						intOverCapacity++;
						//TODO : Over capacity
					}
				}
				intCurrentLevel++;
			} 
			while (arrSameLevelCPO.Length > 0);
		}

		private Int64 AddDCPResultMaster(DataSet pdstDCPResult, DataRow pdrowDCOptionMaster, DataRow pdrowCPO)
		{
			DataRow drowResultMaster = pdstDCPResult.Tables[PRO_DCPResultMasterTable.TABLE_NAME].NewRow();
			drowResultMaster[PRO_DCPResultMasterTable.CPOID_FLD] = pdrowCPO[MTR_CPOTable.CPOID_FLD];
			drowResultMaster[PRO_DCPResultMasterTable.DCOPTIONMASTERID_FLD] = pdrowDCOptionMaster[PRO_DCOptionMasterTable.DCOPTIONMASTERID_FLD];
			//drowResultMaster[PRO_DCPResultMasterTable.DUEDATETIME_FLD] = DateTime.Now; //TODO : Update after
			drowResultMaster[PRO_DCPResultMasterTable.PRODUCTID_FLD] = pdrowCPO[MTR_CPOTable.PRODUCTID_FLD]; //TODO : Update after
			drowResultMaster[PRO_DCPResultMasterTable.QUANTITY_FLD] = pdrowCPO[MTR_CPOTable.QUANTITY_FLD];
			drowResultMaster[PRO_DCPResultMasterTable.ROUTINGID_FLD] = pdrowCPO[ITM_RoutingTable.ROUTINGID_FLD];
			//drowResultMaster[PRO_DCPResultMasterTable.STARTDATETIME_FLD] = DateTime.Now; //TODO : Update after
			drowResultMaster[PRO_DCPResultMasterTable.WORKCENTERID_FLD] = pdrowCPO[MST_WorkCenterTable.WORKCENTERID_FLD];
			pdstDCPResult.Tables[PRO_DCPResultMasterTable.TABLE_NAME].Rows.Add(drowResultMaster);
			return Convert.ToInt64(drowResultMaster[PRO_DCPResultMasterTable.DCPRESULTMASTERID_FLD]);
		}

		private Int64 AddDCPResultDetail(DataSet pdstDCPResult, Int64 pintDCPResultMasterID, DateTime pdtmWorkingDate, DateTime pdtmStartTime, DateTime pdtmEndTime, decimal pdecTotalSecond, decimal pdecQuantity, decimal pdecPercentage, int pintCapacityBottleID, int pintProductID, string pstrWorkCenterCode)
		{
			DataRow drowResultDetail = pdstDCPResult.Tables[PRO_DCPResultDetailTable.TABLE_NAME].NewRow();
			drowResultDetail[PRO_DCPResultDetailTable.DCPRESULTMASTERID_FLD] = pintDCPResultMasterID;
			drowResultDetail[PRO_DCPResultDetailTable.ENDTIME_FLD] = pdtmEndTime;
			drowResultDetail[PRO_DCPResultDetailTable.PERCENTAGE_FLD] = pdecPercentage;
			drowResultDetail[PRO_DCPResultDetailTable.QUANTITY_FLD] = pdecQuantity;
			//drowResultDetail[PRO_DCPResultDetailTable.SHIFTID_FLD] = 1;
			drowResultDetail[PRO_DCPResultDetailTable.STARTTIME_FLD] = pdtmStartTime;
			drowResultDetail[PRO_DCPResultDetailTable.TOTALSECOND_FLD] = pdecTotalSecond;
			drowResultDetail[PRO_DCPResultDetailTable.TYPE_FLD] = (int)DCPResultTypeEnum.Running;
			drowResultDetail[PRO_DCPResultDetailTable.WOCONVERTED_FLD] = 0;
			drowResultDetail[PRO_DCPResultDetailTable.WOGENERATEDID_FLD] = 0;
			drowResultDetail[PRO_DCPResultDetailTable.WORKINGDATE_FLD] = pdtmWorkingDate;
			drowResultDetail[CAPACITYBOTTLEID_FLD] = pintCapacityBottleID;
			drowResultDetail[ITM_ProductTable.PRODUCTID_FLD] = pintProductID;
			drowResultDetail[WORKCENTERCODE_FLD] = pstrWorkCenterCode;
			pdstDCPResult.Tables[PRO_DCPResultDetailTable.TABLE_NAME].Rows.Add(drowResultDetail);
			return Convert.ToInt64(drowResultDetail[PRO_DCPResultDetailTable.DCPRESULTDETAILID_FLD]);
		}

		private void AddChildCPOs(DataRow pdrowCPO, DataTable pdtbBOM, DataTable pdtbCPO, DataTable pdtbProductInfo, DateTime pdtmDueDate)
		{
			DataRow[] arrChildItems = pdtbBOM.Select(ITM_BOMTable.PRODUCTID_FLD + "=" + pdrowCPO[MTR_CPOTable.PRODUCTID_FLD]);
			decimal decParentQuantity = Convert.ToDecimal(pdrowCPO[MTR_CPOTable.QUANTITY_FLD]);
			foreach (DataRow drowChildItem in arrChildItems)
			{
				DataRow drowChildCPO = pdtbCPO.NewRow();

				drowChildCPO[MTR_CPOTable.CPOID_FLD] = pdrowCPO[MTR_CPOTable.CPOID_FLD];

				drowChildCPO[MTR_CPOTable.DUEDATE_FLD] = pdtmDueDate;
				drowChildCPO[MTR_CPOTable.PRODUCTID_FLD] = drowChildItem[ITM_BOMTable.COMPONENTID_FLD];
				drowChildCPO[MTR_CPOTable.QUANTITY_FLD] = decParentQuantity * Convert.ToDecimal(drowChildItem[ITM_BOMTable.QUANTITY_FLD]);

				DataRow drowProductInfo = pdtbProductInfo.Select(MTR_CPOTable.PRODUCTID_FLD + "=" + drowChildItem[ITM_BOMTable.COMPONENTID_FLD])[0];
				drowChildCPO[LEADTIME_FLD] = drowProductInfo[LEADTIME_FLD];
				drowChildCPO[LEVEL_FLD] = drowProductInfo[LEVEL_FLD];
				
				drowChildCPO[WORKCENTERCODE_FLD] = drowProductInfo[WORKCENTERCODE_FLD];
				drowChildCPO[MST_WorkCenterTable.WORKCENTERID_FLD] = drowProductInfo[MST_WorkCenterTable.WORKCENTERID_FLD];
				drowChildCPO[ITM_RoutingTable.ROUTINGID_FLD] = drowProductInfo[ITM_RoutingTable.ROUTINGID_FLD];

				pdtbCPO.Rows.Add(drowChildCPO);
			}
		}

		private void BootleInsideProcess(DataSet pdstCapacityBottles, DataSet pdstDCPResult)
		{
			foreach (DataTable dtbBottles in pdstCapacityBottles.Tables)
			{
				string strWorkCenterCode = dtbBottles.TableName;
				foreach (DataRow drowBottle in dtbBottles.Rows)
				{
					int intCapacityBottle = Convert.ToInt32(drowBottle[CAPACITYBOTTLEID_FLD]);
					decimal decTotalCapacity = Convert.ToDecimal(drowBottle[BOTTLETOTALCAPACITY_FLD]);
					decimal decTotalWorkTime = Convert.ToDecimal(drowBottle[BOTTLETOTALWORKTIME_FLD]);
					//select all result in bottle
					DataRow[] arrResults = pdstDCPResult.Tables[PRO_DCPResultDetailTable.TABLE_NAME].Select(CAPACITYBOTTLEID_FLD + "=" + intCapacityBottle + " AND " + WORKCENTERCODE_FLD + "='" + strWorkCenterCode + "'",ITM_ProductTable.PRODUCTID_FLD);
					//re-arrange starttime and endtime
					DateTime dtmStartTime = Convert.ToDateTime(drowBottle[BOTTLESTARTTIME_FLD]);
					DateTime dtmEndTime;
					foreach (DataRow drowResult in arrResults)
					{
						decimal decLeadTime = Convert.ToDecimal(drowResult[PRO_DCPResultDetailTable.TOTALSECOND_FLD]);
						decimal decRealTime = (decLeadTime * (decTotalWorkTime / decTotalCapacity));
						dtmEndTime = dtmStartTime.AddSeconds(Convert.ToDouble(decRealTime));

						//rewrite
						drowResult[PRO_DCPResultDetailTable.STARTTIME_FLD] = dtmStartTime;
						drowResult[PRO_DCPResultDetailTable.ENDTIME_FLD] = dtmEndTime;

						dtmStartTime = dtmEndTime;
					}
				}
			}
		}

		private decimal OptimizeOrder(ArrayList parrProductID, int pintLastProduceProductID, DataTable pdtbChangeCategory,ArrayList parrFinalOrder)
		{
			int[] arrResult = new int[parrProductID.Count];
			bool[] arrUsed = new bool[parrProductID.Count];
			for (int intIdx = 0; intIdx < arrResult.Length; intIdx++)
			{
				arrResult[intIdx] = -1;
				arrUsed[intIdx] = false;
			}
			int intLastProduceProductIDIdx = parrProductID.IndexOf(pintLastProduceProductID);
			if (intLastProduceProductIDIdx == -1)
			{
				intLastProduceProductIDIdx = 0;
			}
			decimal decMin = Decimal.MaxValue;
			arrUsed[intLastProduceProductIDIdx] = true;
			arrResult[0] = Convert.ToInt32(parrProductID[intLastProduceProductIDIdx]);
			//calculate min
			TryToPut(parrProductID,ref arrResult,ref arrUsed,0,intLastProduceProductIDIdx,0,ref decMin,pdtbChangeCategory,parrFinalOrder);
			return decMin;
		}

		private decimal TryToPut(ArrayList parrProductID, ref int[] parrResult, ref bool[] parrUsed, int pintIndex, int pintValue, decimal pdecTotal, ref decimal pdecMin,DataTable pdtbChangeCategory,ArrayList parrFinalOrder)
		{
			DataRow[] arrChangeCategory;
			if (pintIndex > 0)
			{
				int int1stProductID = parrResult[pintIndex];
				int int2ndProductID = parrResult[pintIndex - 1];
				decimal decChangeCategory = 0;
				arrChangeCategory = pdtbChangeCategory.Select(PRO_ChangeCategoryMatrixTable.SOURCEPRODUCTID_FLD + "=" + int1stProductID + " AND " + PRO_ChangeCategoryMatrixTable.DESTPRODUCTID_FLD + "=" + int2ndProductID);
				if (arrChangeCategory.Length > 0)
				{
					decChangeCategory = Convert.ToDecimal(arrChangeCategory[0][PRO_ChangeCategoryMatrixTable.CHANGETIME_FLD]);
				}
				pdecTotal += decChangeCategory;
			}

			/*string strFilter = string.Empty;
			foreach (int intSourceProductID in parrProductID)
			{
				foreach (int intDestProductID in parrProductID)
				{
					strFilter += "(" + PRO_ChangeCategoryMatrixTable.SOURCEPRODUCTID_FLD + "=" + intSourceProductID
						+ " AND " + PRO_ChangeCategoryMatrixTable.DESTPRODUCTID_FLD + "=" + intDestProductID + ")" + " OR ";
				}
			}
			strFilter += " (1 > 1) ";
			arrChangeCategory = pdtbChangeCategory.Select(strFilter,PRO_ChangeCategoryMatrixTable.CHANGETIME_FLD + " ASC");
			*/
			//calculate min
			
			decimal decMinRemain = 0;
			int intDstProductID = (int)parrProductID[pintValue];
			for (int intIdx = 0; intIdx < parrProductID.Count; intIdx ++)
			{
				if (parrUsed[intIdx])
				{
					continue;
				}
				int intSrcProductID = (int)parrProductID[intIdx];
				//not arranged product
				decimal decChangeCategory = 0;
				arrChangeCategory = pdtbChangeCategory.Select(PRO_ChangeCategoryMatrixTable.SOURCEPRODUCTID_FLD + "=" + intSrcProductID + " AND " + PRO_ChangeCategoryMatrixTable.DESTPRODUCTID_FLD + "=" + intDstProductID);
				if (arrChangeCategory.Length > 0)
				{
					decChangeCategory = Convert.ToDecimal(arrChangeCategory[0][PRO_ChangeCategoryMatrixTable.CHANGETIME_FLD]);
				}
				decMinRemain += decChangeCategory;
				intDstProductID = intSrcProductID;
			}


			//short branch
			if (pdecTotal + decMinRemain >= pdecMin)
			{
				return pdecMin;
			}
			else
			{
				pdecMin = pdecTotal + decMinRemain + 1;
			}

			if (pintIndex == parrProductID.Count - 1)
			{
				//result
				pdecMin = pdecTotal;
				parrFinalOrder.Clear();
				foreach (int intProductID in parrResult)
				{
					parrFinalOrder.Add(intProductID);
				}
				return pdecTotal;
			}
			
			for (int i = 0; i < parrProductID.Count; i++)
			{
				if (!parrUsed[i])
				{
					parrUsed[i] = true;
					parrResult[pintIndex + 1] = Convert.ToInt32(parrProductID[i]);
					TryToPut(parrProductID,ref parrResult,ref parrUsed,pintIndex + 1,i,pdecTotal,ref pdecMin,pdtbChangeCategory,parrFinalOrder);
					parrResult[pintIndex + 1] = -1;
					parrUsed[i] = false;
				}
			}

			return pdecTotal;
		}

		private void MasterResultRefine(DataSet pdstDCPResult)
		{
			foreach (DataRow drowResultMaster in pdstDCPResult.Tables[PRO_DCPResultMasterTable.TABLE_NAME].Rows)
			{
				Int64 intDCPResultMasterID = Convert.ToInt64(drowResultMaster[PRO_DCPResultMasterTable.DCPRESULTMASTERID_FLD]);
				DataRow[] arrResultDetail = pdstDCPResult.Tables[PRO_DCPResultDetailTable.TABLE_NAME].Select(PRO_DCPResultDetailTable.DCPRESULTMASTERID_FLD + "=" + intDCPResultMasterID,PRO_DCPResultDetailTable.STARTTIME_FLD + " ASC");
				if (arrResultDetail.Length > 0)
				{
					drowResultMaster[PRO_DCPResultMasterTable.STARTDATETIME_FLD] = arrResultDetail[0][PRO_DCPResultDetailTable.STARTTIME_FLD];
					drowResultMaster[PRO_DCPResultMasterTable.DUEDATETIME_FLD] = arrResultDetail[arrResultDetail.Length - 1][PRO_DCPResultDetailTable.ENDTIME_FLD];
				}
			}
		}

		private class TimeMark
		{
			public TimeMark(DateTime pdtmTime, short pshtFlag,int pintShiftID,bool p_blnEndOfShift)
			{
				m_dtmTime = pdtmTime;
				m_shtFlag = pshtFlag;
				m_intShiftID = pintShiftID;
				m_blnEndOfShift = p_blnEndOfShift;
			}
			public DateTime m_dtmTime;
			public short m_shtFlag;
			public int m_intShiftID;
			public bool m_blnEndOfShift;
		}

		private class TimeMarkComparer : IComparer
		{
			public int Compare(object x, object y)
			{
				if (((TimeMark)x).m_dtmTime > ((TimeMark)y).m_dtmTime) 
				{
					return 1;
				}
				else if (((TimeMark)x).m_dtmTime < ((TimeMark)y).m_dtmTime) 
				{
					return -1;
				}
				else
				{
					if (((TimeMark)x).m_shtFlag > ((TimeMark)y).m_shtFlag)
					{
						return 1;
					}
					else if (((TimeMark)x).m_shtFlag < ((TimeMark)y).m_shtFlag)
					{
						return -1;
					}
					else
					{
						return 0;
					}
				}
			}
		}

		private DateTime CalculateEndTime(DateTime pdtmStartTime, ref decimal pdecTime, DataTable pdtbWCConfig, int pintWorkCenterID,ref int pintShiftID,DateTime pdtmBottleWorkingDay)
		{
			DataRow[] arrShifts = pdtbWCConfig.Select(MST_WorkCenterTable.WORKCENTERID_FLD + "=" + pintWorkCenterID + " AND " + PRO_WCCapacityTable.BEGINDATE_FLD + " <= '" + pdtmBottleWorkingDay.Date + "' AND " + PRO_WCCapacityTable.ENDDATE_FLD + " >= '" + pdtmBottleWorkingDay.Date + "'",PRO_ShiftPatternTable.WORKTIMEFROM_FLD + " ASC");
			ArrayList arrTimeMarks = new ArrayList();

			if (arrShifts.Length == 0)
			{
				return DateTime.MinValue;
			}
			foreach (DataRow drowShift in arrShifts)
			{
				int intShiftID = Convert.ToInt32(drowShift[PRO_ShiftPatternTable.SHIFTID_FLD]);
				DateTime dtmWorkTimeFrom = Convert.ToDateTime(drowShift[PRO_ShiftPatternTable.WORKTIMEFROM_FLD]);
				DateTime dtmWorkTimeTo = Convert.ToDateTime(drowShift[PRO_ShiftPatternTable.WORKTIMETO_FLD]);
				arrTimeMarks.Add(new TimeMark(dtmWorkTimeFrom,1,intShiftID,false));
				arrTimeMarks.Add(new TimeMark(dtmWorkTimeTo,-1,intShiftID,true));
				try
				{
					DateTime dtmRefreshingFrom = Convert.ToDateTime(drowShift[PRO_ShiftPatternTable.REFRESHINGFROM_FLD]);
					DateTime dtmRefreshingTo = Convert.ToDateTime(drowShift[PRO_ShiftPatternTable.REFRESHINGTO_FLD]);
					arrTimeMarks.Add(new TimeMark(dtmRefreshingFrom,-1,intShiftID,false));
					arrTimeMarks.Add(new TimeMark(dtmRefreshingTo,1,intShiftID,false));
				}
				catch {}
				try
				{
					DateTime dtmRegularStopFrom = Convert.ToDateTime(drowShift[PRO_ShiftPatternTable.REGULARSTOPFROM_FLD]);
					DateTime dtmRegularStopTo = Convert.ToDateTime(drowShift[PRO_ShiftPatternTable.REGULARSTOPTO_FLD]);
					arrTimeMarks.Add(new TimeMark(dtmRegularStopFrom,-1,intShiftID,false));
					arrTimeMarks.Add(new TimeMark(dtmRegularStopTo,1,intShiftID,false));
				}
				catch {}
				try
				{
					DateTime dtmExtraStopFrom = Convert.ToDateTime(drowShift[PRO_ShiftPatternTable.EXTRASTOPFROM_FLD]);
					DateTime dtmExtraStopTo = Convert.ToDateTime(drowShift[PRO_ShiftPatternTable.EXTRASTOPTO_FLD]);
					arrTimeMarks.Add(new TimeMark(dtmExtraStopFrom,-1,intShiftID,false));
					arrTimeMarks.Add(new TimeMark(dtmExtraStopTo,1,intShiftID,false));
				}
				catch {}
			}
			arrTimeMarks.Sort(new TimeMarkComparer());

			double dblNeededTime = Convert.ToDouble(pdecTime);
			double dblObtainedTime = 0;
			bool blnNote = false;
			DateTime dtmCheckPoint = pdtmStartTime.AddDays(-pdtmStartTime.Date.Subtract(Convert.ToDateTime(arrShifts[0][PRO_ShiftPatternTable.WORKTIMEFROM_FLD]).Date).Days);
			if (dtmCheckPoint < Convert.ToDateTime(arrShifts[0][PRO_ShiftPatternTable.WORKTIMEFROM_FLD])) 
			{
				dtmCheckPoint = dtmCheckPoint.AddDays(1);
				blnNote = true;
			}
			foreach (TimeMark objTimeMark in arrTimeMarks)
			{
				if ((objTimeMark.m_dtmTime < dtmCheckPoint) || ((objTimeMark.m_dtmTime == dtmCheckPoint) && (objTimeMark.m_blnEndOfShift)))
				{
					continue;
				}
				if (objTimeMark.m_shtFlag == -1)
				{
					dblObtainedTime += objTimeMark.m_dtmTime.Subtract(dtmCheckPoint).TotalSeconds;
					dtmCheckPoint = objTimeMark.m_dtmTime;
				}
				else if (objTimeMark.m_shtFlag == 1)
				{
					dtmCheckPoint = objTimeMark.m_dtmTime;
				}
				else
				{
					//Error, never occurs
				}
				if (dblObtainedTime >= dblNeededTime)
				{
					pintShiftID = objTimeMark.m_intShiftID;
					return dtmCheckPoint.AddSeconds(dblNeededTime - dblObtainedTime).AddDays(pdtmStartTime.Date.Subtract(Convert.ToDateTime(arrShifts[0][PRO_ShiftPatternTable.WORKTIMEFROM_FLD]).Date).Days);
				}
				else if (objTimeMark.m_blnEndOfShift)
				{
					pintShiftID = objTimeMark.m_intShiftID;
					pdecTime = Convert.ToDecimal(dblObtainedTime);
					return dtmCheckPoint.AddDays(pdtmStartTime.Date.Subtract(Convert.ToDateTime(arrShifts[0][PRO_ShiftPatternTable.WORKTIMEFROM_FLD]).Date).Days);
				}		
			}
			DateTime dtmReturn = dtmCheckPoint.AddSeconds(dblNeededTime - dblObtainedTime).AddDays(pdtmStartTime.Date.Subtract(Convert.ToDateTime(arrShifts[0][PRO_ShiftPatternTable.WORKTIMEFROM_FLD]).Date).Days);
			if (blnNote)
			{
				dtmReturn = dtmReturn.AddDays(-1);
			}
			return dtmReturn;
		}

		private const string AVAILQUANTITY_FLD = "AvailQuantity";
		private const string PRODUCEORDER_FLD = "ProduceOrder";
		private const string DEMANDQUANTITY_FLD = "DemandQuantity";

		public decimal GetOnHandAtAsOfDate(int pintProductID,
			DataTable pdtbFutureSOs,
			DataTable pdtbFuturePOs,
			DataTable pdtbFutureSupplyWOs,
			DataTable pdtbFutureDemandWOs,
			bool pblnIncludeSO,
			DataTable pdtbAvailQty,
			DateTime pdtmPlanningStartDate,
			bool pblnUseCacheAsBegin)
		{                  
			decimal decCurrentOH = 0; //TODO : get current onhand from all loc,bin
			DataRow[] drowSOs = null;
			DataRow[] drowPOs = null;
			DataRow[] drowSupplyWOs = null;
			DataRow[] drowDemandWOs = null;
			Decimal decQty = 0;
			try
			{
				DataRow[] arrAvailQty = pdtbAvailQty.Select(ITM_ProductTable.PRODUCTID_FLD + "=" + pintProductID);
				if (arrAvailQty.Length > 0)
				{
					decCurrentOH = Convert.ToDecimal(arrAvailQty[0][AVAILQUANTITY_FLD]);
				}
				if (pblnUseCacheAsBegin) 
				{
					return decCurrentOH;
				}
				drowSOs = pdtbFutureSOs.Select(ITM_ProductTable.PRODUCTID_FLD + "=" + pintProductID);
				drowPOs = pdtbFuturePOs.Select(ITM_ProductTable.PRODUCTID_FLD + "=" + pintProductID);
				drowSupplyWOs = pdtbFutureSupplyWOs.Select(ITM_ProductTable.PRODUCTID_FLD + "=" + pintProductID);
				drowDemandWOs = pdtbFutureDemandWOs.Select(ITM_ProductTable.PRODUCTID_FLD + "=" + pintProductID);

				decQty = decCurrentOH;
				foreach(DataRow drow in drowPOs)
				{
					decQty += Convert.ToDecimal(drow[PO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD]);
				}
				foreach(DataRow drow in drowSupplyWOs)
				{
					decQty += Convert.ToDecimal(drow[PRO_WorkOrderDetailTable.ORDERQUANTITY_FLD]);
				}
				if (pblnIncludeSO)
				{
					foreach(DataRow drow in drowSOs)
					{
						decQty -= Convert.ToDecimal(drow[PO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD]);
					}
				}
				foreach(DataRow drow in drowDemandWOs)
				{
					decQty -= Convert.ToDecimal(drow[PRO_WorkOrderDetailTable.ORDERQUANTITY_FLD]);
				}
			}
			catch (Exception ex)
			{
				ex.ToString();
			}
			return decQty;
		}

		private void GetDemandAndSupply(int pintCCNID,DateTime pdtmFromDate,DateTime pdtmToDate,
			ref DataTable pdtbFutureSOs,
			ref DataTable pdtbFuturePOs,
			ref DataTable pdtbFutureSupplyWOs,
			ref DataTable pdtbFutureDemandWOs,
			PCSComMaterials.Plan.BO.MPSRegenerationProcessBO boMPSRegeneration, int pintWorkCenterID)//,
		{
			//DateTime dtmCurrentDate = (new PCSComUtils.Common.BO.UtilsBO()).GetDBDate();
			//PCSComMaterials.Plan.BO.MPSRegenerationProcessBO boMPSRegeneration = new PCSComMaterials.Plan.BO.MPSRegenerationProcessBO();
			if (pdtmFromDate < pdtmToDate)
			{
				// get all SOs from current date to as of date
				pdtbFutureSOs = boMPSRegeneration.GetTotalSO(pintCCNID, pdtmFromDate, pdtmToDate, pintWorkCenterID);
				// get all POs from current date to as of date
				pdtbFuturePOs = boMPSRegeneration.GetTotalPO(pintCCNID, pdtmFromDate, pdtmToDate, pintWorkCenterID);
				// get all supply WOs from current date to as of date
				pdtbFutureSupplyWOs = boMPSRegeneration.GetTotalWO(pintCCNID, pdtmFromDate, pdtmToDate, pintWorkCenterID);
				// get all demand WOs from current date to as of date
				pdtbFutureDemandWOs = boMPSRegeneration.GetDemandWO(pintCCNID, pdtmFromDate, pdtmToDate, pintWorkCenterID);
				//pdtbFutureDemandWOs.DataSet.Tables.Remove(pdtbFutureDemandWOs.TableName);
			}
		}

		private const string WORKCENTERLEVEL_FLD = "WorkCenterLevel";
		private const string WORKCENTERANCESSORS_FLD = "WorkCenterAncessors";
		private const string MINPRODUCE_FLD = "MinProduce";
		private const string MAXPRODUCE_FLD = "MaxProduce";
		private const string MAXROUNDUPTOMIN_FLD = "MaxRoundUpToMin";
		private const string MAXROUNDUPTOMULTIPLE_FLD = "MaxRoundUpToMultiple";

		private void SortWorkCenterList(DataTable pdtbTopLevelWC,DataTable pdtbWCList,DataTable pdtbBOM)
		{
			Stack stkWC = new Stack();
			foreach (DataRow drowTopWC in pdtbTopLevelWC.Rows)
			{
				stkWC.Push(drowTopWC[MST_WorkCenterTable.WORKCENTERID_FLD]);
				DataRow drowWC = pdtbWCList.Select("WorkCenterID = " + drowTopWC["WorkCenterID"])[0];
				drowWC[WORKCENTERLEVEL_FLD] = 0;
			}
			
			int intWorkCenterID;
			int intWorkCenterLevel;
			string strWorkCenterAncessors;
			while (stkWC.Count > 0)
			{
				intWorkCenterID = (int)(stkWC.Pop());
				DataRow drowWC = pdtbWCList.Select("WorkCenterID = " + intWorkCenterID)[0];
				intWorkCenterLevel = (int)drowWC[WORKCENTERLEVEL_FLD];
				strWorkCenterAncessors = drowWC[WORKCENTERANCESSORS_FLD].ToString();
				
				//push all child
				DataRow[] arrChildWC = pdtbBOM.Select("ParentWCID = " + intWorkCenterID);
				foreach (DataRow drowChildWC in arrChildWC)
				{
					if (pdtbWCList.Select("WorkCenterID = " + drowChildWC["WorkCenterID"]).Length == 0)
						continue;
					DataRow drowWCInList = pdtbWCList.Select("WorkCenterID = " + drowChildWC["WorkCenterID"])[0];

					if ((drowWCInList[WORKCENTERLEVEL_FLD] != DBNull.Value) && (Convert.ToInt32(drowWCInList[WORKCENTERLEVEL_FLD]) >= intWorkCenterID + 1))
						continue;
					
					string[] arrAncessors = strWorkCenterAncessors.Split(',');
					bool blnCyclic = false;
					foreach (string strAncessors in arrAncessors)
					{
						if (strAncessors.Equals(drowChildWC[MST_WorkCenterTable.WORKCENTERID_FLD].ToString()))
						{
							blnCyclic = true;
							break;
						}
					}
					if (blnCyclic)
					{
						continue;
					}

					drowWCInList[WORKCENTERLEVEL_FLD] = intWorkCenterLevel + 1;
					drowWCInList[WORKCENTERANCESSORS_FLD] = strWorkCenterAncessors + "," + intWorkCenterID;
					if (!stkWC.Contains(drowChildWC["WorkCenterID"]))
					{
						stkWC.Push(drowChildWC[MST_WorkCenterTable.WORKCENTERID_FLD]);
					}
				}
			}
		}

		//raw result table
		private const string RAWRESULTPRODUCTID_FLD = "RawResultProductID";
		private const string RAWRESULTBOTTLEID_FLD = "RawResultBottleID";
		private const string RAWRESULTPRODUCEQUANTITY_FLD = "RawResultProduceQuantity";
		private const string RAWRESULTPRODUCEORDER_FLD = "RawResultProduceOrder";
		private const string RAWRESULTLEADTIME_FLD = "RawResultLeadTime";
		private const string RAWRESULTROUTINGID_FLD = "RawResultRoutingID";
		private const string RAWRESULTWORKCENTERCODE_FLD = "RawResultWorkCenterCode";
		private const string RAWRESULTWORKCENTERID_FLD = "RawResultWorkCenterID";
		private const string RAWRESULTSAMPLEPATTERN_FLD = "RawResultSamplePattern";
		private const string RAWRESULTSAMPLERATE_FLD = "RawResultSampleRate";
		private const string RAWRESULTDELAYTIME_FLD = "RawResultDelayTime";
		private const string RAWRESULTSTARTTIME_FLD = "RawResultStartTime";
		private const string RAWRESULTCHECKPOINTPERITEM_FLD = "RawResultCheckpointPerItem";
		private const string RAWRESULTSAFETYSTOCKAMOUNT_FLD = "RawResultSafetyStockAmount";

		/// <summary>
		/// Tao khuon mau bang ket qua
		/// </summary>
		/// <returns></returns>
		private DataTable CreateRawResultTable()
		{
			DataTable dtbRawResult = new DataTable();
			DataColumn dcolRawResultProductID = new DataColumn(RAWRESULTPRODUCTID_FLD,typeof(int));
			dtbRawResult.Columns.Add(dcolRawResultProductID);
			DataColumn dcolRawResultBottleID = new DataColumn(RAWRESULTBOTTLEID_FLD,typeof(int));
			dtbRawResult.Columns.Add(dcolRawResultBottleID);
			DataColumn dcolRawResultProduceQuantity = new DataColumn(RAWRESULTPRODUCEQUANTITY_FLD,typeof(decimal));
			dtbRawResult.Columns.Add(dcolRawResultProduceQuantity);
			DataColumn dcolRawResultProduceOrder = new DataColumn(RAWRESULTPRODUCEORDER_FLD,typeof(decimal));
			dtbRawResult.Columns.Add(dcolRawResultProduceOrder);
			DataColumn dcolRawResultLeadTime = new DataColumn(RAWRESULTLEADTIME_FLD,typeof(decimal));
			dtbRawResult.Columns.Add(dcolRawResultLeadTime);
			DataColumn dcolRawResultRoutingID = new DataColumn(RAWRESULTROUTINGID_FLD,typeof(int));
			dtbRawResult.Columns.Add(dcolRawResultRoutingID);
			DataColumn dcolRawResultWorkCenterCode = new DataColumn(RAWRESULTWORKCENTERCODE_FLD,typeof(string));
			dtbRawResult.Columns.Add(dcolRawResultWorkCenterCode);
			DataColumn dcolRawResultWorkCenterID = new DataColumn(RAWRESULTWORKCENTERID_FLD,typeof(int));
			dtbRawResult.Columns.Add(dcolRawResultWorkCenterID);
			DataColumn dcolRawResultSamplePattern = new DataColumn(RAWRESULTSAMPLEPATTERN_FLD,typeof(int));
			dtbRawResult.Columns.Add(dcolRawResultSamplePattern);
			DataColumn dcolRawResultSampleRate = new DataColumn(RAWRESULTSAMPLERATE_FLD,typeof(decimal));
			dtbRawResult.Columns.Add(dcolRawResultSampleRate);
			DataColumn dcolRawResultDelayTime = new DataColumn(RAWRESULTDELAYTIME_FLD,typeof(decimal));
			dtbRawResult.Columns.Add(dcolRawResultDelayTime);
			DataColumn dcolRawResultStartTime = new DataColumn(RAWRESULTSTARTTIME_FLD,typeof(DateTime));
			dtbRawResult.Columns.Add(dcolRawResultStartTime);
			DataColumn dcolRawResultCheckpointPerItem = new DataColumn(RAWRESULTCHECKPOINTPERITEM_FLD,typeof(decimal));
			dtbRawResult.Columns.Add(dcolRawResultCheckpointPerItem);
			DataColumn dcolRawResultSafetyStockAmount = new DataColumn(RAWRESULTSAFETYSTOCKAMOUNT_FLD,typeof(decimal));
			dtbRawResult.Columns.Add(dcolRawResultSafetyStockAmount);

			return dtbRawResult;
		}

		private void AddRawResult(DataTable pdtbRawResult, int pintProductID, int pintBottleID, decimal pdecProduceQuantity, int pintProduceOrder, decimal pdecLeadTime, int pintRoutingID, int pintWorkCenterID, string pstrWorkCenterCode, int pintSamplePattern, decimal pdecSampleRate, decimal pdecDelayTime, DateTime pdtmStartTime, decimal pdecCheckpointPerItem, decimal pdecSafetyStockAmount)
		{
			if (pdecProduceQuantity <= 0)
			{
				return;
			}
			DataRow drowRawResult = pdtbRawResult.NewRow();
			drowRawResult[RAWRESULTPRODUCTID_FLD] = pintProductID;
			drowRawResult[RAWRESULTBOTTLEID_FLD] = pintBottleID;
			drowRawResult[RAWRESULTPRODUCEQUANTITY_FLD] = pdecProduceQuantity;
			drowRawResult[RAWRESULTPRODUCEORDER_FLD] = pintProduceOrder;
			drowRawResult[RAWRESULTLEADTIME_FLD] = pdecLeadTime;
			drowRawResult[RAWRESULTROUTINGID_FLD] = pintRoutingID;
			drowRawResult[RAWRESULTWORKCENTERID_FLD] = pintWorkCenterID;
			drowRawResult[RAWRESULTWORKCENTERCODE_FLD] = pstrWorkCenterCode;
			
			drowRawResult[RAWRESULTSAMPLEPATTERN_FLD] = pintSamplePattern;
			drowRawResult[RAWRESULTSAMPLERATE_FLD] = pdecSampleRate;
			drowRawResult[RAWRESULTDELAYTIME_FLD] = pdecDelayTime;

			drowRawResult[RAWRESULTSTARTTIME_FLD] = pdtmStartTime;

			drowRawResult[RAWRESULTCHECKPOINTPERITEM_FLD] = pdecCheckpointPerItem;

			drowRawResult[RAWRESULTSAFETYSTOCKAMOUNT_FLD] = pdecSafetyStockAmount;

			pdtbRawResult.Rows.Add(drowRawResult);
		}

		private void CollectOverCapacity(DataTable pdtbOverCapacityWC, DataTable dtbDelSchData, DataRow drowDCOption, DataTable dtbWCConfig, DataTable dtbWCList, DataTable pdtbRawResult, DataTable pdtbBOM, DataTable pdtbProductInfo)
		{
			CreateOverCapacityWCTable(pdtbOverCapacityWC);
			foreach (DataRow drowDelSch in dtbDelSchData.Rows)
			{
				if (Convert.ToDecimal(drowDelSch[SO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD]) == 0)
				{
					continue;
				}
				if (Convert.ToInt32(drowDelSch[CAPACITYBOTTLEID_FLD]) <= 0)
				{
					int intProductID = Convert.ToInt32(drowDelSch[ITM_ProductTable.PRODUCTID_FLD]);
					decimal decQuantity = Convert.ToDecimal(drowDelSch[SO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD]);
//					decimal decLeadTime = Convert.ToDecimal(drowDelSch[LEADTIME_FLD]);
//					int intRoutingID = Convert.ToInt32(drowDelSch[ITM_RoutingTable.ROUTINGID_FLD]);
//					int intWorkCenterID = Convert.ToInt32(drowDelSch[MST_WorkCenterTable.WORKCENTERID_FLD]);
//					string strWorkCenterCode = drowDelSch[WORKCENTERCODE_FLD].ToString();
//
//					int intSamplePattern = 0;
//					decimal decSampleRate = 0;
//					decimal decDelayTime = 0;
//					try
//					{
//						intSamplePattern = Convert.ToInt32(drowDelSch[PRO_CheckPointTable.SAMPLEPATTERN_FLD]);
//						decSampleRate =  Convert.ToDecimal(drowDelSch[PRO_CheckPointTable.SAMPLERATE_FLD]);
//						decDelayTime = Convert.ToDecimal(drowDelSch[PRO_CheckPointTable.DELAYTIME_FLD]);
//					}
//					catch {}

					//Insert raw result
					//AddRawResult(pdtbRawResult,intProductID,-1,decQuantity,0,decLeadTime,intRoutingID,intWorkCenterID,strWorkCenterCode,intSamplePattern,decSampleRate,decDelayTime,DateTime.MinValue);

					//generate_child until last level
					Stack stkProductID = new Stack();
					Stack stkQuantity = new Stack();

					stkProductID.Push(intProductID);
					stkQuantity.Push(decQuantity);
					while (stkProductID.Count > 0)
					{
						int intCurrentProductID = Convert.ToInt32(stkProductID.Pop());
						decimal decCurrentQuantity = Convert.ToDecimal(stkQuantity.Pop());
						
						DataRow drowCurrentProduct = pdtbProductInfo.Select(ITM_ProductTable.PRODUCTID_FLD + "=" + intCurrentProductID)[0];
						
						decimal decCurrentLeadTime = Convert.ToDecimal(drowCurrentProduct[LEADTIME_FLD]);
						int intCurrentRoutingID = Convert.ToInt32(drowCurrentProduct[ITM_RoutingTable.ROUTINGID_FLD]);
						int intCurrentWorkCenterID = Convert.ToInt32(drowCurrentProduct[MST_WorkCenterTable.WORKCENTERID_FLD]);
						string strCurrentWorkCenterCode = drowCurrentProduct[WORKCENTERCODE_FLD].ToString();
						int intCurrentSamplePattern = 0;
						decimal decCurrentCheckpointPerItem = Convert.ToDecimal(drowCurrentProduct[CHECKPOINTPERITEM_FLD]);
						try
						{
							intCurrentSamplePattern = Convert.ToInt32(drowCurrentProduct[PRO_CheckPointTable.SAMPLEPATTERN_FLD]);
						}
						catch {}
						decimal decCurrentSampleRate = 0;
						try
						{
							decCurrentSampleRate = Convert.ToDecimal(drowCurrentProduct[PRO_CheckPointTable.SAMPLERATE_FLD]);
						}
						catch {}
						decimal decCurrentDelayTime = 0;
						try
						{
							decCurrentDelayTime = Convert.ToDecimal(drowCurrentProduct[PRO_CheckPointTable.DELAYTIME_FLD]);
						}
						catch {}
						AddRawResult(pdtbRawResult,intCurrentProductID,-1,decCurrentQuantity,0,decCurrentLeadTime,intCurrentRoutingID,intCurrentWorkCenterID,strCurrentWorkCenterCode,intCurrentSamplePattern,decCurrentSampleRate,decCurrentDelayTime,DateTime.MinValue,decCurrentCheckpointPerItem,0);

						//push child
						DataRow[] arrChildProducts = pdtbBOM.Select(ITM_BOMTable.PRODUCTID_FLD + "=" + intCurrentProductID);
						foreach(DataRow drowChildProduct in arrChildProducts)
						{
							stkProductID.Push(drowChildProduct[ITM_BOMTable.COMPONENTID_FLD]);
							decimal decShrink = Convert.ToDecimal(drowChildProduct[ITM_BOMTable.SHRINK_FLD]);
							stkQuantity.Push(Decimal.Round(Convert.ToDecimal(drowChildProduct[ITM_BOMTable.QUANTITY_FLD]) * decCurrentQuantity / (1-decShrink/100),0));
						}
					}
					
					DataRow[] arrOverCapacity = pdtbOverCapacityWC.Select(MST_WorkCenterTable.WORKCENTERID_FLD + "=" + drowDelSch[MST_WorkCenterTable.WORKCENTERID_FLD]);
					DataRow drowOverCapacity;
					if (arrOverCapacity.Length > 0)
					{
						drowOverCapacity = arrOverCapacity[0];
						drowOverCapacity[OVERLEADTIME_FLD] = Convert.ToDecimal(drowOverCapacity[OVERLEADTIME_FLD]) + Convert.ToDecimal(drowDelSch[SO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD]) * Convert.ToDecimal(drowDelSch[LEADTIME_FLD]);
						drowOverCapacity[OVERDAYS_FLD] = Convert.ToDecimal(drowOverCapacity[OVERLEADTIME_FLD]) / Convert.ToDecimal(drowOverCapacity[CAPACITY_FLD]) ;
						drowOverCapacity[OVERPERCENT_FLD] = 100 * Convert.ToDecimal(drowOverCapacity[OVERDAYS_FLD]) / Convert.ToInt32(drowDCOption[MTR_MPSCycleOptionMasterTable.PLANHORIZON_FLD]);
					}
					else
					{
						drowOverCapacity = pdtbOverCapacityWC.NewRow();
						drowOverCapacity[MST_WorkCenterTable.WORKCENTERID_FLD] = drowDelSch[MST_WorkCenterTable.WORKCENTERID_FLD];
						drowOverCapacity[MST_WorkCenterTable.CODE_FLD] = drowDelSch[WORKCENTERCODE_FLD];
						DataRow drowWCCapacity = null;

						DataRow drowWC = null;
						try
						{
							drowWC = dtbWCList.Select(MST_WorkCenterTable.WORKCENTERID_FLD + "=" + drowDelSch[MST_WorkCenterTable.WORKCENTERID_FLD])[0];
							drowWCCapacity = dtbWCConfig.Select(MST_WorkCenterTable.WORKCENTERID_FLD + "=" + drowDelSch[MST_WorkCenterTable.WORKCENTERID_FLD])[0];
						}
						catch
						{
							continue;
						}
						int intLevel = Convert.ToInt32(drowWC[WORKCENTERLEVEL_FLD]);

						drowOverCapacity[CAPACITY_FLD] = drowWCCapacity[PRO_WCCapacityTable.CAPACITY_FLD];
						drowOverCapacity[OVERLEADTIME_FLD] = Convert.ToDecimal(drowDelSch[SO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD]) * Convert.ToDecimal(drowDelSch[LEADTIME_FLD]);
						drowOverCapacity[OVERDAYS_FLD] = Convert.ToDecimal(drowOverCapacity[OVERLEADTIME_FLD]) / Convert.ToDecimal(drowOverCapacity[CAPACITY_FLD]) ;
						drowOverCapacity[OVERPERCENT_FLD] = 100 * Convert.ToDecimal(drowOverCapacity[OVERDAYS_FLD]) / Convert.ToInt32(drowDCOption[MTR_MPSCycleOptionMasterTable.PLANHORIZON_FLD]);
						pdtbOverCapacityWC.Rows.Add(drowOverCapacity);
					}
				}
			}
		}

		private void WriteFinalResult(int intDCOptionMasterID, DataTable pdtbRawResult, DataSet pdstCapacityBottles, DataTable pdtbWCList, DataTable pdtbWCConfig, DataSet pdstDCPResult, DataTable pdtbChangeCategory, bool pblnIncludeCheckpoint, DateTime pdtmAsOfDate)
		{
			foreach (DataRow drowWC in pdtbWCList.Rows)
			{
				int intWorkCenterID = Convert.ToInt32(drowWC[MST_WorkCenterTable.WORKCENTERID_FLD]);
				string strDepartmentCode = drowWC[DEPARTMENTCODE_FLD].ToString();
				string strWorkCenterCode = drowWC[WORKCENTERCODE_FLD].ToString();
				bool blnOutside = (strDepartmentCode.StartsWith(OUTSIDE_FLD));
				
				if (!blnOutside)
				{
					DataRow[] arrCapacityBottles = pdstCapacityBottles.Tables[strWorkCenterCode].Select(string.Empty,BOTTLESTARTTIME_FLD + " ASC");
					if (arrCapacityBottles.Length == 0)
					{
						continue;
					}

					#region Over capacity items
					DataRow[] arrOverRawResult = pdtbRawResult.Select(RAWRESULTBOTTLEID_FLD + "=-1" + " AND " + RAWRESULTWORKCENTERID_FLD + "=" + intWorkCenterID,RAWRESULTPRODUCEORDER_FLD + " ASC");
					DateTime dtmBottleStartTime = Convert.ToDateTime(arrCapacityBottles[0][BOTTLESTARTTIME_FLD]);
					DateTime dtmBottleWorkingDay = Convert.ToDateTime(arrCapacityBottles[0][BOTTLEWORKINGDAY_FLD]);
					int intCapacityBottleID = Convert.ToInt32(arrCapacityBottles[0][CAPACITYBOTTLEID_FLD]);
					decimal decBottleTotalCapacity = Convert.ToDecimal(arrCapacityBottles[0][BOTTLETOTALCAPACITY_FLD]);
					decimal decBottleTotalWorkingTime = Convert.ToDecimal(arrCapacityBottles[0][BOTTLETOTALWORKTIME_FLD]);
					
					DateTime dtmPlanningStartDate;
					try
					{
						dtmPlanningStartDate = Convert.ToDateTime(drowWC[PRO_PlanningOffsetTable.PLANNINGSTARTDATE_FLD]);	
					}
					catch
					{
						dtmPlanningStartDate = pdtmAsOfDate;
					}

					//DateTime dtmStartTime = new DateTime(dtmBottleWorkingDay.Year,dtmBottleWorkingDay.Month,dtmBottleWorkingDay.Day,dtmBottleStartTime.Hour,dtmBottleStartTime.Minute,dtmBottleStartTime.Second);
					DateTime dtmStartTime = dtmPlanningStartDate;//new DateTime(dtmPlanningStartDate.Year,dtmPlanningStartDate.Month,dtmPlanningStartDate.Day,dtmPlanningStartDate.Hour,dtmBottleStartTime.Minute,dtmBottleStartTime.Second);
					DateTime dtmEndTime = dtmStartTime;
					foreach (DataRow drowRawResult in arrOverRawResult)
					{					
						int intProductID = Convert.ToInt32(drowRawResult[RAWRESULTPRODUCTID_FLD]);
						try
						{
							int intSamplePattern = Convert.ToInt32(drowRawResult[PRO_CheckPointTable.SAMPLEPATTERN_FLD]);
								
							decimal decSampleRate = Convert.ToDecimal(drowRawResult[PRO_CheckPointTable.SAMPLERATE_FLD]);
							decimal decDelayTime = Convert.ToDecimal(drowRawResult[PRO_CheckPointTable.DELAYTIME_FLD]);							
						}
						catch {}

						//master
						DataRow drowMaster = pdstDCPResult.Tables[PRO_DCPResultMasterTable.TABLE_NAME].NewRow();
						decimal decDeliveryQuantity = Convert.ToDecimal(drowRawResult[RAWRESULTPRODUCEQUANTITY_FLD]);
						decimal decProductLeadTime = Convert.ToDecimal(drowRawResult[RAWRESULTLEADTIME_FLD]);
						drowMaster[PRO_DCPResultMasterTable.DCOPTIONMASTERID_FLD] = intDCOptionMasterID;
						drowMaster[PRO_DCPResultMasterTable.QUANTITY_FLD] = Math.Floor(Convert.ToDouble(decDeliveryQuantity));
						drowMaster[PRO_DCPResultMasterTable.STARTDATETIME_FLD] = dtmStartTime;//new DateTime(dtmStartTime.Year,dtmStartTime.Month,dtmStartTime.Day,dtmStartTime.Hour,dtmStartTime.Minute,dtmStartTime.Second);
						drowMaster[PRO_DCPResultMasterTable.WORKCENTERID_FLD] = intWorkCenterID;
						drowMaster[PRO_DCPResultMasterTable.PRODUCTID_FLD] = intProductID;
						drowMaster[PRO_DCPResultMasterTable.ROUTINGID_FLD] = drowRawResult[RAWRESULTROUTINGID_FLD];
						pdstDCPResult.Tables[PRO_DCPResultMasterTable.TABLE_NAME].Rows.Add(drowMaster);

						//detail - not divided by shift
						decimal decTotalLeadTime = decDeliveryQuantity * decProductLeadTime;
						decimal decTotalRealTime = decTotalLeadTime * (decBottleTotalWorkingTime / decBottleTotalCapacity);
						dtmEndTime = dtmStartTime;
						//Insert detail
						DataRow drowDetailRunning = pdstDCPResult.Tables[PRO_DCPResultDetailTable.TABLE_NAME].NewRow();
						drowDetailRunning[PRO_DCPResultDetailTable.DCPRESULTMASTERID_FLD] = drowMaster[PRO_DCPResultMasterTable.DCPRESULTMASTERID_FLD];
						drowDetailRunning[PRO_DCPResultDetailTable.STARTTIME_FLD] = dtmStartTime;
						drowDetailRunning[PRO_DCPResultDetailTable.ENDTIME_FLD] = dtmEndTime;
						drowDetailRunning[PRO_DCPResultDetailTable.QUANTITY_FLD] = decDeliveryQuantity;
						drowDetailRunning[PRO_DCPResultDetailTable.PERCENTAGE_FLD] = 100;
						drowDetailRunning[PRO_DCPResultDetailTable.SHIFTID_FLD] = DBNull.Value;
						drowDetailRunning[PRO_DCPResultDetailTable.TOTALSECOND_FLD] = Decimal.Floor(decTotalLeadTime);
						drowDetailRunning[PRO_DCPResultDetailTable.TYPE_FLD] = DCPResultTypeEnum.Running;
						drowDetailRunning[PRO_DCPResultDetailTable.WORKINGDATE_FLD] = dtmPlanningStartDate.Date;//dtmBottleWorkingDay;
						pdstDCPResult.Tables[PRO_DCPResultDetailTable.TABLE_NAME].Rows.Add(drowDetailRunning);

						drowMaster[PRO_DCPResultMasterTable.DUEDATETIME_FLD] = dtmEndTime; //new DateTime(dtmEndTime.Year,dtmEndTime.Month,dtmEndTime.Day,dtmEndTime.Hour,dtmEndTime.Minute,dtmEndTime.Second);
						dtmStartTime = dtmEndTime;
					}	
					#endregion
				
					#region Arranged items
					foreach (DataRow drowBottle in arrCapacityBottles)
					{
						dtmBottleStartTime = Convert.ToDateTime(drowBottle[BOTTLESTARTTIME_FLD]);
						dtmBottleWorkingDay = Convert.ToDateTime(drowBottle[BOTTLEWORKINGDAY_FLD]);
						intCapacityBottleID = Convert.ToInt32(drowBottle[CAPACITYBOTTLEID_FLD]);
						decBottleTotalCapacity = Convert.ToDecimal(drowBottle[BOTTLETOTALCAPACITY_FLD]);
						decBottleTotalWorkingTime = Convert.ToDecimal(drowBottle[BOTTLETOTALWORKTIME_FLD]);
					
						dtmStartTime = dtmBottleStartTime;
						dtmEndTime = dtmStartTime;

						//select rawresults
						DataRow[] arrRawResults = pdtbRawResult.Select(RAWRESULTBOTTLEID_FLD + "=" + intCapacityBottleID + " AND " + RAWRESULTWORKCENTERID_FLD + "=" + intWorkCenterID,RAWRESULTPRODUCEORDER_FLD + " ASC");

						foreach (DataRow drowRawResult in arrRawResults)
						{					
							int intProductID = Convert.ToInt32(drowRawResult[RAWRESULTPRODUCTID_FLD]);
							decimal decDeliveryQuantity = Convert.ToDecimal(drowRawResult[RAWRESULTPRODUCEQUANTITY_FLD]);
							decimal decProductLeadTime = Convert.ToDecimal(drowRawResult[RAWRESULTLEADTIME_FLD]);
							decimal decSSAmount = 0;
							if (drowRawResult[RAWRESULTSAFETYSTOCKAMOUNT_FLD] != DBNull.Value)
							{
								decSSAmount = Convert.ToDecimal(drowRawResult[RAWRESULTSAFETYSTOCKAMOUNT_FLD]);
							}
							decimal decCheckpointTime = 0;
							try
							{
								int intSamplePattern = Convert.ToInt32(drowRawResult[RAWRESULTSAMPLEPATTERN_FLD]);
								
								decimal decSampleRate = Convert.ToDecimal(drowRawResult[RAWRESULTSAMPLERATE_FLD]);
								decimal decDelayTime = Convert.ToDecimal(drowRawResult[RAWRESULTDELAYTIME_FLD]);							

								switch (intSamplePattern)
								{
									case CHECKPOINT_BY_QTY :
									{
										decCheckpointTime = Math.Round((decDeliveryQuantity / decSampleRate) * decDelayTime);
										break;
									}
									case CHECKPOINT_BY_TIME:
									{
										decCheckpointTime = Math.Round(((decDeliveryQuantity * decProductLeadTime) * (decBottleTotalWorkingTime / decBottleTotalCapacity) / decSampleRate) * decDelayTime);
										break;
									}
								}
							}
							catch {}
							if (!pblnIncludeCheckpoint)
							{
								decCheckpointTime = 0;
							}

							//master
							DataRow drowMaster = pdstDCPResult.Tables[PRO_DCPResultMasterTable.TABLE_NAME].NewRow();
							drowMaster[PRO_DCPResultMasterTable.DCOPTIONMASTERID_FLD] = intDCOptionMasterID;
							drowMaster[PRO_DCPResultMasterTable.QUANTITY_FLD] = decDeliveryQuantity;
							drowMaster[PRO_DCPResultMasterTable.STARTDATETIME_FLD] = dtmStartTime;//new DateTime(dtmStartTime.Year,dtmStartTime.Month,dtmStartTime.Day,dtmStartTime.Hour,dtmStartTime.Minute,dtmStartTime.Second);
							drowMaster[PRO_DCPResultMasterTable.WORKCENTERID_FLD] = intWorkCenterID;
							drowMaster[PRO_DCPResultMasterTable.PRODUCTID_FLD] = intProductID;
							drowMaster[PRO_DCPResultMasterTable.ROUTINGID_FLD] = drowRawResult[RAWRESULTROUTINGID_FLD];
							pdstDCPResult.Tables[PRO_DCPResultMasterTable.TABLE_NAME].Rows.Add(drowMaster);

							//detail - not divided by shift
							decimal decTotalLeadTime = decDeliveryQuantity * decProductLeadTime;
							decimal decTotalRealTime = decTotalLeadTime * (decBottleTotalWorkingTime / decBottleTotalCapacity) + decCheckpointTime;
							decTotalLeadTime += decCheckpointTime * (decBottleTotalCapacity / decBottleTotalWorkingTime);
						
							decimal decRemainRealTime = Decimal.Floor(decTotalRealTime);
							decimal decRemainQuantity = decDeliveryQuantity;
						
							int intShiftID = 0;
							while (decRemainRealTime > 0)
							{
								decimal decPartRealTime = decRemainRealTime;
								dtmEndTime = CalculateEndTime(dtmStartTime,ref decPartRealTime,pdtbWCConfig,intWorkCenterID,ref intShiftID,dtmBottleWorkingDay);
								decRemainRealTime -= decPartRealTime;
								decimal decPartQuantity = Math.Round(decDeliveryQuantity * (decPartRealTime / decTotalRealTime));
								decRemainQuantity -= decPartQuantity;
								if (decRemainQuantity == 1)
								{
									decPartQuantity++;
									decRemainQuantity--;
									decRemainRealTime = 0;
								}
								//Insert detail
								DataRow drowDetailRunning = pdstDCPResult.Tables[PRO_DCPResultDetailTable.TABLE_NAME].NewRow();
								drowDetailRunning[PRO_DCPResultDetailTable.DCPRESULTMASTERID_FLD] = drowMaster[PRO_DCPResultMasterTable.DCPRESULTMASTERID_FLD];
								drowDetailRunning[PRO_DCPResultDetailTable.STARTTIME_FLD] = dtmStartTime;
								drowDetailRunning[PRO_DCPResultDetailTable.ENDTIME_FLD] = dtmEndTime;
								drowDetailRunning[PRO_DCPResultDetailTable.QUANTITY_FLD] = decPartQuantity;
								drowDetailRunning[PRO_DCPResultDetailTable.SAFETYSTOCKAMOUNT] = decSSAmount;
								if (decTotalLeadTime == 0)
								{
									drowDetailRunning[PRO_DCPResultDetailTable.PERCENTAGE_FLD] = 100;
								}
								else
								{
									drowDetailRunning[PRO_DCPResultDetailTable.PERCENTAGE_FLD] = 100 * decPartRealTime / decTotalRealTime;
								}
								drowDetailRunning[PRO_DCPResultDetailTable.SHIFTID_FLD] = intShiftID; //TODO : divide by shift
								drowDetailRunning[PRO_DCPResultDetailTable.TOTALSECOND_FLD] = Decimal.Floor(decPartRealTime * (decBottleTotalCapacity / decBottleTotalWorkingTime));
								drowDetailRunning[PRO_DCPResultDetailTable.TYPE_FLD] = DCPResultTypeEnum.Running;
								drowDetailRunning[PRO_DCPResultDetailTable.WORKINGDATE_FLD] = dtmBottleWorkingDay;
								pdstDCPResult.Tables[PRO_DCPResultDetailTable.TABLE_NAME].Rows.Add(drowDetailRunning);
								dtmStartTime = dtmEndTime;
							}
							//change category
							int intNextProductID = intProductID;
							int intNextItemOrder = int.MaxValue;
							try
							{
								intNextProductID = Convert.ToInt32(((DataRow)arrRawResults[Array.IndexOf(arrRawResults,drowRawResult) + 1])[RAWRESULTPRODUCTID_FLD]);
								intNextItemOrder = Convert.ToInt32(((DataRow)arrRawResults[Array.IndexOf(arrRawResults,drowRawResult) + 1])[RAWRESULTPRODUCEORDER_FLD]);
							}
							catch {}
							if (intNextItemOrder == int.MaxValue) 
							{
								intNextProductID = intProductID;
							}
							DataRow[] arrChangeCategory = pdtbChangeCategory.Select(PRO_ChangeCategoryMatrixTable.SOURCEPRODUCTID_FLD + " = " + intProductID + " AND " + PRO_ChangeCategoryMatrixTable.DESTPRODUCTID_FLD + " = " + intNextProductID);
							if (arrChangeCategory.Length > 0)
							{
								decimal decChangeCategoryTime = 0;
								try 
								{
									decChangeCategoryTime = Convert.ToDecimal(arrChangeCategory[0][PRO_ChangeCategoryMatrixTable.CHANGETIME_FLD]);
								}
								catch {}
								if (decChangeCategoryTime > 0)
								{
									decRemainRealTime = decChangeCategoryTime;
									while (decRemainRealTime > 0)
									{
										decimal decPartRealTime = decRemainRealTime;
										dtmEndTime = CalculateEndTime(dtmStartTime,ref decPartRealTime,pdtbWCConfig,intWorkCenterID,ref intShiftID,dtmBottleWorkingDay);
										decRemainRealTime -= decPartRealTime;
										DataRow drowDetailChangeCategory = pdstDCPResult.Tables[PRO_DCPResultDetailTable.TABLE_NAME].NewRow();
										drowDetailChangeCategory[PRO_DCPResultDetailTable.DCPRESULTMASTERID_FLD] = drowMaster[PRO_DCPResultMasterTable.DCPRESULTMASTERID_FLD];
										drowDetailChangeCategory[PRO_DCPResultDetailTable.STARTTIME_FLD] = dtmStartTime;
										drowDetailChangeCategory[PRO_DCPResultDetailTable.ENDTIME_FLD] = dtmEndTime;
										drowDetailChangeCategory[PRO_DCPResultDetailTable.QUANTITY_FLD] = 0;
										drowDetailChangeCategory[PRO_DCPResultDetailTable.PERCENTAGE_FLD] = 0;
										drowDetailChangeCategory[PRO_DCPResultDetailTable.SHIFTID_FLD] = intShiftID; //TODO : divide by shift
										drowDetailChangeCategory[PRO_DCPResultDetailTable.TOTALSECOND_FLD] = Math.Round(decChangeCategoryTime * (decBottleTotalCapacity / decBottleTotalWorkingTime));
										drowDetailChangeCategory[PRO_DCPResultDetailTable.TYPE_FLD] = DCPResultTypeEnum.ChangeCategory;
										drowDetailChangeCategory[PRO_DCPResultDetailTable.WORKINGDATE_FLD] = dtmBottleWorkingDay;
										pdstDCPResult.Tables[PRO_DCPResultDetailTable.TABLE_NAME].Rows.Add(drowDetailChangeCategory);
										dtmStartTime = dtmEndTime;
									}
								}
							}

							drowMaster[PRO_DCPResultMasterTable.DUEDATETIME_FLD] = dtmEndTime; //new DateTime(dtmEndTime.Year,dtmEndTime.Month,dtmEndTime.Day,dtmEndTime.Hour,dtmEndTime.Minute,dtmEndTime.Second);
							dtmStartTime = dtmEndTime;
						}
					}
					#endregion
				}
				else
				{
					#region Outside Processing
					DataRow[] arrRawResults = pdtbRawResult.Select(RAWRESULTWORKCENTERID_FLD + "=" + intWorkCenterID,RAWRESULTSTARTTIME_FLD + " ASC");

					foreach (DataRow drowRawResult in arrRawResults)
					{			
						//master
						DataRow drowMaster = pdstDCPResult.Tables[PRO_DCPResultMasterTable.TABLE_NAME].NewRow();
						decimal decDeliveryQuantity = Convert.ToDecimal(drowRawResult[RAWRESULTPRODUCEQUANTITY_FLD]);
						decimal decProductLeadTime = Convert.ToDecimal(drowRawResult[RAWRESULTLEADTIME_FLD]);
						DateTime dtmStartTime = Convert.ToDateTime(drowRawResult[RAWRESULTSTARTTIME_FLD]);
						DateTime dtmEndTime = dtmStartTime.AddSeconds(Convert.ToDouble(decProductLeadTime));
						int intProductID = Convert.ToInt32(drowRawResult[RAWRESULTPRODUCTID_FLD]);

						drowMaster[PRO_DCPResultMasterTable.DCOPTIONMASTERID_FLD] = intDCOptionMasterID;
						drowMaster[PRO_DCPResultMasterTable.QUANTITY_FLD] = Decimal.Floor(decDeliveryQuantity);
						drowMaster[PRO_DCPResultMasterTable.STARTDATETIME_FLD] = dtmStartTime;
						drowMaster[PRO_DCPResultMasterTable.WORKCENTERID_FLD] = intWorkCenterID;
						drowMaster[PRO_DCPResultMasterTable.PRODUCTID_FLD] = intProductID;
						drowMaster[PRO_DCPResultMasterTable.ROUTINGID_FLD] = drowRawResult[RAWRESULTROUTINGID_FLD];
						pdstDCPResult.Tables[PRO_DCPResultMasterTable.TABLE_NAME].Rows.Add(drowMaster);

						//detail - not divided by shift
						//Insert detail
						DataRow drowDetailRunning = pdstDCPResult.Tables[PRO_DCPResultDetailTable.TABLE_NAME].NewRow();
						drowDetailRunning[PRO_DCPResultDetailTable.DCPRESULTMASTERID_FLD] = drowMaster[PRO_DCPResultMasterTable.DCPRESULTMASTERID_FLD];
						drowDetailRunning[PRO_DCPResultDetailTable.STARTTIME_FLD] = dtmStartTime;
						drowDetailRunning[PRO_DCPResultDetailTable.ENDTIME_FLD] = dtmEndTime;
						drowDetailRunning[PRO_DCPResultDetailTable.QUANTITY_FLD] = decDeliveryQuantity;
						drowDetailRunning[PRO_DCPResultDetailTable.PERCENTAGE_FLD] = 100;
						drowDetailRunning[PRO_DCPResultDetailTable.SHIFTID_FLD] = DBNull.Value;
						drowDetailRunning[PRO_DCPResultDetailTable.TOTALSECOND_FLD] = Decimal.Floor(decProductLeadTime);
						drowDetailRunning[PRO_DCPResultDetailTable.TYPE_FLD] = DCPResultTypeEnum.Running;
						drowDetailRunning[PRO_DCPResultDetailTable.WORKINGDATE_FLD] = dtmStartTime.Date;
						pdstDCPResult.Tables[PRO_DCPResultDetailTable.TABLE_NAME].Rows.Add(drowDetailRunning);

						drowMaster[PRO_DCPResultMasterTable.DUEDATETIME_FLD] = dtmEndTime; //new DateTime(dtmEndTime.Year,dtmEndTime.Month,dtmEndTime.Day,dtmEndTime.Hour,dtmEndTime.Minute,dtmEndTime.Second);
						dtmStartTime = dtmEndTime;
					}
					#endregion
				}
			}
		}

		private decimal CalculateCheckpointTime(DataRow pdrowDelivery, decimal pdecTotalQuantity, decimal pdecTotalRealTime)
		{
			try
			{
				int intSamplePattern = Convert.ToInt32(pdrowDelivery[PRO_CheckPointTable.SAMPLEPATTERN_FLD]);										
				decimal decSampleRate = Convert.ToDecimal(pdrowDelivery[PRO_CheckPointTable.SAMPLERATE_FLD]);
				decimal decDelayTime = Convert.ToDecimal(pdrowDelivery[PRO_CheckPointTable.DELAYTIME_FLD]);									
				switch (intSamplePattern)
				{
					case CHECKPOINT_BY_QTY :
					{
						decimal decCheckpointTime = Math.Round((pdecTotalQuantity / decSampleRate) * decDelayTime);//Convert.ToDecimal(Math.Floor(Convert.ToDouble((decTotalQuantity / decSampleRate))) + 1) * decDelayTime;
						return decCheckpointTime;
					}
					case CHECKPOINT_BY_TIME:
					{
						decimal decCheckpointTime = Math.Round((pdecTotalRealTime / decSampleRate) * decDelayTime);
						return decCheckpointTime;
					}
				}
			}
			catch 
			{
				return 0;
			}
			return 0;
		}

		private void ReCalculateLeadTime(DataTable pdtbDelSchData, DataTable pdtbBOMInfo, DataTable pdtbProduct, bool pblnIncludeCheckpoint)
		{
			pdtbDelSchData.Columns.Add(CHECKPOINTPERITEM_FLD,typeof(decimal));
			pdtbBOMInfo.Columns.Add(CHECKPOINTPERITEM_FLD,typeof(decimal));
			pdtbProduct.Columns.Add(CHECKPOINTPERITEM_FLD,typeof(decimal));
			
			foreach (DataRow drow in pdtbDelSchData.Rows)
			{
				try
				{
					int intSamplePattern = Convert.ToInt32(drow[PRO_CheckPointTable.SAMPLEPATTERN_FLD]);										
					decimal decSampleRate = Convert.ToDecimal(drow[PRO_CheckPointTable.SAMPLERATE_FLD]);
					decimal decDelayTime = Convert.ToDecimal(drow[PRO_CheckPointTable.DELAYTIME_FLD]);
					decimal decLeadTime = Convert.ToDecimal(drow[LEADTIME_FLD]);
					decimal decCheckpointTime = 0;
					switch (intSamplePattern)
					{
						case CHECKPOINT_BY_QTY :
						{
							decCheckpointTime = decDelayTime / decSampleRate;
							break;
						}
						case CHECKPOINT_BY_TIME:
						{
							decCheckpointTime = decDelayTime / (decSampleRate / decLeadTime);
							break;
						}
					}
					if (!pblnIncludeCheckpoint)
					{
						decCheckpointTime = 0;
					}
					drow[CHECKPOINTPERITEM_FLD] = decCheckpointTime;
				}
				catch 
				{
					drow[CHECKPOINTPERITEM_FLD] = 0;
				}
			}
			foreach (DataRow drow in pdtbBOMInfo.Rows)
			{
				try
				{
					int intSamplePattern = Convert.ToInt32(drow[PRO_CheckPointTable.SAMPLEPATTERN_FLD]);										
					decimal decSampleRate = Convert.ToDecimal(drow[PRO_CheckPointTable.SAMPLERATE_FLD]);
					decimal decDelayTime = Convert.ToDecimal(drow[PRO_CheckPointTable.DELAYTIME_FLD]);
					decimal decLeadTime = Convert.ToDecimal(drow[LEADTIME_FLD]);
					decimal decCheckpointTime = 0;
					switch (intSamplePattern)
					{
						case CHECKPOINT_BY_QTY :
						{
							decCheckpointTime = decDelayTime / decSampleRate;
							break;
						}
						case CHECKPOINT_BY_TIME:
						{
							decCheckpointTime = decDelayTime / (decSampleRate / decLeadTime);
							break;
						}
					}
					if (!pblnIncludeCheckpoint)
					{
						decCheckpointTime = 0;
					}
					drow[CHECKPOINTPERITEM_FLD] = decCheckpointTime;
				}
				catch 
				{
					drow[CHECKPOINTPERITEM_FLD] = 0;
				}
			}
			foreach (DataRow drow in pdtbProduct.Rows)
			{
				try
				{
					int intSamplePattern = Convert.ToInt32(drow[PRO_CheckPointTable.SAMPLEPATTERN_FLD]);										
					decimal decSampleRate = Convert.ToDecimal(drow[PRO_CheckPointTable.SAMPLERATE_FLD]);
					decimal decDelayTime = Convert.ToDecimal(drow[PRO_CheckPointTable.DELAYTIME_FLD]);
					decimal decLeadTime = Convert.ToDecimal(drow[LEADTIME_FLD]);
					decimal decCheckpointTime = 0;
					switch (intSamplePattern)
					{
						case CHECKPOINT_BY_QTY :
						{
							decCheckpointTime = decDelayTime / decSampleRate;
							break;
						}
						case CHECKPOINT_BY_TIME:
						{
							decCheckpointTime = decDelayTime / (decSampleRate / decLeadTime);
							break;
						}
					}
					if (!pblnIncludeCheckpoint)
					{
						decCheckpointTime = 0;
					}
					drow[CHECKPOINTPERITEM_FLD] = decCheckpointTime;
				}
				catch 
				{
					drow[CHECKPOINTPERITEM_FLD] = 0;
				}
			}
		}

		#endregion

		#region Manual Production Planning
		
		public DataSet GetDCPResultData(int pintProductionLineID, int pintDCOptionMasterID, bool pblnNoData)
		{
			PRO_DCOptionMasterDS dsDC = new PRO_DCOptionMasterDS();
			return dsDC.GetDCPData(pintProductionLineID,pintDCOptionMasterID,pblnNoData);
		}
		
		/// <summary>
		/// Get remain capacity per day, and working time
		/// </summary>
		/// <param name="pdtbWCList"></param>
		/// <param name="pdtbWCConfig"></param>
		/// <param name="pdrowDCOptionMaster"></param>
		/// <param name="pdtbDelSch"></param>
		/// <param name="p_intWorkCenterID"></param>
		/// <returns></returns>
		private DataTable CreateCapacityBottleForWC(DataTable pdtbWCList,DataTable pdtbWCConfig, DataRow pdrowDCOptionMaster, DataTable pdtbDelSch,int p_intWorkCenterID)
		{
			const int RESERVER_DAYS = 20;

			DateTime dtmAsOfDate = Convert.ToDateTime(pdrowDCOptionMaster[MTR_MPSCycleOptionMasterTable.ASOFDATE_FLD]);
			int intPlanHorizon = Convert.ToInt32(pdrowDCOptionMaster[MTR_MPSCycleOptionMasterTable.PLANHORIZON_FLD]);
			int intGroupBy = Convert.ToInt32(pdrowDCOptionMaster[MTR_MPSCycleOptionMasterTable.GROUPBY_FLD]);

			#region //Create structure dtbCapacityBottlesStructure
			DataTable dtbCapacityBottlesStructure = new DataTable();
			
			DataColumn dcolCapacityBottleID = new DataColumn(CAPACITYBOTTLEID_FLD,typeof(int));
			dcolCapacityBottleID.AllowDBNull = false;
			dcolCapacityBottleID.AutoIncrement = true;
			dcolCapacityBottleID.AutoIncrementSeed = 1;
			dcolCapacityBottleID.AutoIncrementStep = 1;
			dtbCapacityBottlesStructure.Columns.Add(dcolCapacityBottleID);

			DataColumn dcolBottleWorkingDay = new DataColumn(BOTTLEWORKINGDAY_FLD,typeof(DateTime));
			dtbCapacityBottlesStructure.Columns.Add(dcolBottleWorkingDay);

			DataColumn dcolBottleStartTime = new DataColumn(BOTTLESTARTTIME_FLD,typeof(DateTime));
			dtbCapacityBottlesStructure.Columns.Add(dcolBottleStartTime);

			DataColumn dcolBottleEndTime = new DataColumn(BOTTLEENDTIME_FLD,typeof(DateTime));
			dtbCapacityBottlesStructure.Columns.Add(dcolBottleEndTime);

			DataColumn dcolBottleTotalCapacity = new DataColumn(BOTTLETOTALCAPACITY_FLD,typeof(decimal));
			dtbCapacityBottlesStructure.Columns.Add(dcolBottleTotalCapacity);

			DataColumn dcolBottleRemainCapacity = new DataColumn(BOTTLEREMAINCAPACITY_FLD,typeof(decimal));
			dtbCapacityBottlesStructure.Columns.Add(dcolBottleRemainCapacity);

			DataColumn dcolBottleTotalWorkTime = new DataColumn(BOTTLETOTALWORKTIME_FLD,typeof(decimal));
			dtbCapacityBottlesStructure.Columns.Add(dcolBottleTotalWorkTime);

			DataColumn dcolBottleFirstProduceProductID = new DataColumn(BOTTLEFIRSTPRODUCEPRODUCTID_FLD,typeof(int));
			dtbCapacityBottlesStructure.Columns.Add(dcolBottleFirstProduceProductID);

			#endregion

			DataRow drowWC = pdtbWCList.Select(MST_WorkCenterTable.WORKCENTERID_FLD + "=" + p_intWorkCenterID)[0];
			//determine parent 
			//check outsides
			string strDepartmentCode = drowWC[DEPARTMENTCODE_FLD].ToString();
			string strWorkCenterCode = drowWC[WORKCENTERCODE_FLD].ToString();
			int intWorkCenterId = Convert.ToInt32(drowWC[MST_WorkCenterTable.WORKCENTERID_FLD]);

			if (strDepartmentCode.StartsWith(OUTSIDE_FLD))
			{
				return null;
			}

			DataTable dtbCapacityBottles = dtbCapacityBottlesStructure.Clone();
			dtbCapacityBottles.TableName = strWorkCenterCode;
			
			DateTime dtmCurrentDay = dtmAsOfDate.Date.AddDays(-RESERVER_DAYS);

			// create bottle from before AsOfDate N days, reserve for offset

			#region // First, create bottles per days				
			for (int intIdx = 0; intIdx <= intPlanHorizon + RESERVER_DAYS; intIdx++)
			{
				DataRow drowCapacityBottle = dtbCapacityBottles.NewRow();
				drowCapacityBottle[BOTTLEWORKINGDAY_FLD] = dtmCurrentDay;

				//all shifts
				DataRow[] arrShifts = pdtbWCConfig.Select(
					PRO_WCCapacityTable.WORKCENTERID_FLD + "=" + intWorkCenterId + " AND " +
						PRO_WCCapacityTable.BEGINDATE_FLD + "<='" + dtmCurrentDay + "'"  + " AND " +
						PRO_WCCapacityTable.ENDDATE_FLD + ">='" + dtmCurrentDay + "'" 
					,PRO_ShiftPatternTable.WORKTIMEFROM_FLD + " ASC");
				if (arrShifts.Length < 1)
				{
					drowCapacityBottle.Delete();
				}
				else
				{
					DateTime dtmBottleStart = Convert.ToDateTime(arrShifts[0][PRO_ShiftPatternTable.WORKTIMEFROM_FLD]);
					DateTime dtmBottleEnd = Convert.ToDateTime(arrShifts[arrShifts.Length - 1][PRO_ShiftPatternTable.WORKTIMETO_FLD]);
					int intDateDiff = dtmBottleEnd.Date.Subtract(dtmBottleStart.Date).Days;
					
					drowCapacityBottle[BOTTLESTARTTIME_FLD] = new DateTime(dtmCurrentDay.Year,dtmCurrentDay.Month,dtmCurrentDay.Day,dtmBottleStart.Hour,dtmBottleStart.Minute,dtmBottleStart.Second);
					drowCapacityBottle[BOTTLEENDTIME_FLD] = new DateTime(dtmCurrentDay.AddDays(intDateDiff).Year,dtmCurrentDay.AddDays(intDateDiff).Month,dtmCurrentDay.AddDays(intDateDiff).Day,dtmBottleEnd.Hour,dtmBottleEnd.Minute,dtmBottleEnd.Second);
					dtbCapacityBottles.Rows.Add(drowCapacityBottle);
				}
				dtmCurrentDay = dtmCurrentDay.AddDays(1);//dtmAsOfDate.Date.AddDays(intIdx);
			}

			#endregion

			//Second, if group by hour, divide bottles to times between 2 delivery time
			//else, normalize delivery time to start of each working day
			if (intGroupBy == (int)PlanningGroupBy.ByHour)
			{
				#region Group by Hours

				foreach (DataRow drowDelSch in pdtbDelSch.Rows)
				{
					DateTime dtmDelivery = Convert.ToDateTime(drowDelSch[SO_DeliveryScheduleTable.SCHEDULEDATE_FLD]);
					DataRow[] arrNearestBottles = dtbCapacityBottles.Select(BOTTLESTARTTIME_FLD + "<'" + dtmDelivery + "'",BOTTLESTARTTIME_FLD + " DESC");
					if (arrNearestBottles.Length > 0)
					{
						DataRow drowNearestBottle = arrNearestBottles[0];
						DateTime dtmBottleEndTime = Convert.ToDateTime(drowNearestBottle[BOTTLEENDTIME_FLD]);
						//if this time is inside a bottle
						//TODO: check bottle size > 1h
						if (dtmDelivery < dtmBottleEndTime)
						{
							//divide bottle
							DataRow drow1stNewBottle = dtbCapacityBottles.NewRow();
							DataRow drow2ndNewBottle = dtbCapacityBottles.NewRow();
							
							drow1stNewBottle[BOTTLESTARTTIME_FLD] = drowNearestBottle[BOTTLESTARTTIME_FLD];
							drow1stNewBottle[BOTTLEENDTIME_FLD] = dtmDelivery;
							drow1stNewBottle[BOTTLEWORKINGDAY_FLD] = drowNearestBottle[BOTTLEWORKINGDAY_FLD];

							drow2ndNewBottle[BOTTLESTARTTIME_FLD] = dtmDelivery;
							drow2ndNewBottle[BOTTLEENDTIME_FLD] = drowNearestBottle[BOTTLEENDTIME_FLD];
							drow2ndNewBottle[BOTTLEWORKINGDAY_FLD] = drowNearestBottle[BOTTLEWORKINGDAY_FLD];

							drowNearestBottle.Delete();
							dtbCapacityBottles.Rows.Add(drow1stNewBottle);
							dtbCapacityBottles.Rows.Add(drow2ndNewBottle);

							//drowDelSch[CAPACITYBOTTLEID_FLD] = drow1stNewBottle[CAPACITYBOTTLEID_FLD];
						}
						//if this time is boundary of a bottle
						if (dtmDelivery == dtmBottleEndTime)
						{
							//drowDelSch[CAPACITYBOTTLEID_FLD] = drowNearestBottle[CAPACITYBOTTLEID_FLD];
						}
						//if this time is outside a bottle
						if (dtmDelivery > dtmBottleEndTime)
						{
							//drowDelSch[SO_DeliveryScheduleTable.SCHEDULEDATE_FLD] = dtmBottleEndTime;
							//drowDelSch[CAPACITYBOTTLEID_FLD] = drowNearestBottle[CAPACITYBOTTLEID_FLD];
						}
					}
					else
					{
						//TODO : out of cycle
					}
				}

				#endregion
			}
			else if (intGroupBy == (int)PlanningGroupBy.ByShift)
			{
				#region Group by Shift

				DataRow[] arrBottles = dtbCapacityBottles.Select(string.Empty);
				foreach (DataRow drowBottle in arrBottles) 
				{
					DateTime dtmBottleStart = Convert.ToDateTime(drowBottle[BOTTLESTARTTIME_FLD]);
					DateTime dtmBottleEnd = Convert.ToDateTime(drowBottle[BOTTLESTARTTIME_FLD]);
					DateTime dtmBottleWorkingDay = Convert.ToDateTime(drowBottle[BOTTLEWORKINGDAY_FLD]);

					//select all shift configured
					DataRow[] arrShifts = pdtbWCConfig.Select(
						PRO_WCCapacityTable.WORKCENTERID_FLD + "=" + intWorkCenterId + " AND " +
							PRO_WCCapacityTable.BEGINDATE_FLD + "<='" + drowBottle[BOTTLEWORKINGDAY_FLD] + "'" + " AND " +
							PRO_WCCapacityTable.ENDDATE_FLD + ">='" + drowBottle[BOTTLEWORKINGDAY_FLD] + "'"
						,PRO_ShiftPatternTable.WORKTIMEFROM_FLD + " ASC");

					foreach (DataRow drowShift in arrShifts)
					{
						DateTime dtmShiftStart = Convert.ToDateTime(drowShift[PRO_ShiftPatternTable.WORKTIMEFROM_FLD]);
						DateTime dtmShiftEnd = Convert.ToDateTime(drowShift[PRO_ShiftPatternTable.WORKTIMETO_FLD]);

						int intDateDiff = dtmBottleWorkingDay.Date.Subtract(dtmShiftStart.Date).Days;

						//add new bottle 
						DataRow drowNewBottle = dtbCapacityBottles.NewRow();
						drowNewBottle[BOTTLESTARTTIME_FLD] = dtmShiftStart.AddDays(intDateDiff);
						drowNewBottle[BOTTLEENDTIME_FLD] = dtmShiftEnd.AddDays(intDateDiff);
						drowNewBottle[BOTTLEWORKINGDAY_FLD] = dtmBottleWorkingDay;

						dtbCapacityBottles.Rows.Add(drowNewBottle);
					}
					//delete bottle
					drowBottle.Delete();
				}

				#endregion
			}
			else
			{
				#region //normalize delivery time
				foreach (DataRow drowDelSch in pdtbDelSch.Rows)
				{
					DateTime dtmDelivery = Convert.ToDateTime(drowDelSch[SO_DeliveryScheduleTable.SCHEDULEDATE_FLD]);
					DataRow[] arrNearestBottles = dtbCapacityBottles.Select(BOTTLEENDTIME_FLD + "<'" + dtmDelivery + "'",BOTTLESTARTTIME_FLD + " DESC");
					if (arrNearestBottles.Length > 0)
					{
						DataRow drowNearestBottle = arrNearestBottles[0];
						drowDelSch[SO_DeliveryScheduleTable.SCHEDULEDATE_FLD] = drowNearestBottle[BOTTLEENDTIME_FLD];
						//drowDelSch[CAPACITYBOTTLEID_FLD] = drowNearestBottle[CAPACITYBOTTLEID_FLD];
					}
					else
					{
						//TODO : out of cycle
					}
				}

				#endregion
			}

			#region //Calculate TotalWorkTime and TotalCapacity
			foreach (DataRow drowBottle in dtbCapacityBottles.Rows)
			{
				if (drowBottle.RowState == DataRowState.Deleted)
				{
					continue;
				}
				//all shifts
				DataRow[] arrShifts = pdtbWCConfig.Select(
					PRO_WCCapacityTable.WORKCENTERID_FLD + "=" + intWorkCenterId + " AND " +
						PRO_WCCapacityTable.BEGINDATE_FLD + "<='" + drowBottle[BOTTLEWORKINGDAY_FLD] + "'" + " AND " +
						PRO_WCCapacityTable.ENDDATE_FLD + ">='" + drowBottle[BOTTLEWORKINGDAY_FLD] + "'"
					,PRO_ShiftPatternTable.WORKTIMEFROM_FLD + " ASC");

				DateTime dtmBottleStart = Convert.ToDateTime(drowBottle[BOTTLESTARTTIME_FLD]);
				DateTime dtmBottleEnd = Convert.ToDateTime(drowBottle[BOTTLEENDTIME_FLD]);
				DateTime dtmBottleWorkingDay = Convert.ToDateTime(drowBottle[BOTTLEWORKINGDAY_FLD]);
					
				decimal decTotalWorkingTime = 0;
				foreach (DataRow drowShift in arrShifts)
				{
					#region //move to bottle workingday

					DateTime dtmWorkTimeFrom = Convert.ToDateTime(drowShift[PRO_ShiftPatternTable.WORKTIMEFROM_FLD]);
					DateTime dtmWorkTimeTo = Convert.ToDateTime(drowShift[PRO_ShiftPatternTable.WORKTIMETO_FLD]);					
					int intDateDiff = dtmBottleWorkingDay.Date.Subtract(dtmWorkTimeFrom.Date).Days;

					DateTime dtmRefreshingFrom = DateTime.MinValue;
					try
					{
						dtmRefreshingFrom = Convert.ToDateTime(drowShift[PRO_ShiftPatternTable.REFRESHINGFROM_FLD]);
					}
					catch {}
					DateTime dtmRefreshingTo = DateTime.MinValue;
					try
					{
						dtmRefreshingTo = Convert.ToDateTime(drowShift[PRO_ShiftPatternTable.REFRESHINGTO_FLD]);
					}
					catch {}
					DateTime dtmRegularStopFrom = DateTime.MinValue;
					try
					{
						dtmRegularStopFrom = Convert.ToDateTime(drowShift[PRO_ShiftPatternTable.REGULARSTOPFROM_FLD]);
					}
					catch {}
					DateTime dtmRegularStopTo = DateTime.MinValue; 
					try
					{
						dtmRegularStopTo = Convert.ToDateTime(drowShift[PRO_ShiftPatternTable.REGULARSTOPTO_FLD]);
					}
					catch {}
					DateTime dtmExtraStopFrom = DateTime.MinValue; 
					try
					{
						dtmExtraStopFrom = Convert.ToDateTime(drowShift[PRO_ShiftPatternTable.EXTRASTOPFROM_FLD]);
					}
					catch {}
					DateTime dtmExtraStopTo = DateTime.MinValue; 
					try
					{
						dtmExtraStopTo = Convert.ToDateTime(drowShift[PRO_ShiftPatternTable.EXTRASTOPTO_FLD]);
					}
					catch {}
					
					//move to bottle workingday
					dtmWorkTimeFrom = dtmWorkTimeFrom.AddDays(intDateDiff);
					dtmWorkTimeTo = dtmWorkTimeTo.AddDays(intDateDiff);
					dtmRefreshingFrom = dtmRefreshingFrom.AddDays(intDateDiff);
					dtmRefreshingTo = dtmRefreshingTo.AddDays(intDateDiff);
					dtmRegularStopFrom = dtmRegularStopFrom.AddDays(intDateDiff);
					dtmRegularStopTo = dtmRegularStopTo.AddDays(intDateDiff);
					dtmExtraStopFrom = dtmExtraStopFrom.AddDays(intDateDiff);
					dtmExtraStopTo = dtmExtraStopTo.AddDays(intDateDiff);

					#endregion

					DateTime dtmStart;
					DateTime dtmEnd;
					decimal decTotalSeconds;

					#region Calculate total working time (to seconds)

					//worktime
					dtmStart = (dtmWorkTimeFrom > dtmBottleStart) ? dtmWorkTimeFrom : dtmBottleStart;
					dtmEnd = (dtmWorkTimeTo < dtmBottleEnd) ? dtmWorkTimeTo : dtmBottleEnd;
					decTotalSeconds = Convert.ToDecimal(dtmEnd.Subtract(dtmStart).TotalSeconds);
					if (decTotalSeconds > 0)
					{
						decTotalWorkingTime += decTotalSeconds;
					}

					//refreshing
					dtmStart = (dtmRefreshingFrom > dtmBottleStart) ? dtmRefreshingFrom : dtmBottleStart;
					dtmEnd = (dtmRefreshingTo < dtmBottleEnd) ? dtmRefreshingTo : dtmBottleEnd;
					decTotalSeconds = Convert.ToDecimal(dtmEnd.Subtract(dtmStart).TotalSeconds);
					if (decTotalSeconds > 0)
					{
						decTotalWorkingTime -= decTotalSeconds;
					}

					//regular stop
					dtmStart = (dtmRegularStopFrom > dtmBottleStart) ? dtmRegularStopFrom : dtmBottleStart;
					dtmEnd = (dtmRegularStopTo < dtmBottleEnd) ? dtmRegularStopTo : dtmBottleEnd;
					decTotalSeconds = Convert.ToDecimal(dtmEnd.Subtract(dtmStart).TotalSeconds);
					if (decTotalSeconds > 0)
					{
						decTotalWorkingTime -= decTotalSeconds;
					}

					//extra stop
					dtmStart = (dtmExtraStopFrom > dtmBottleStart) ? dtmExtraStopFrom : dtmBottleStart;
					dtmEnd = (dtmExtraStopTo < dtmBottleEnd) ? dtmExtraStopTo : dtmBottleEnd;
					decTotalSeconds = Convert.ToDecimal(dtmEnd.Subtract(dtmStart).TotalSeconds);
					if (decTotalSeconds > 0)
					{
						decTotalWorkingTime -= decTotalSeconds;
					}

					#endregion
				}
				decimal decTotalDayCapacity = Convert.ToDecimal(arrShifts[0][PRO_WCCapacityTable.CAPACITY_FLD]);
				int intWCType = Convert.ToInt32(arrShifts[0][PRO_WCCapacityTable.WCTYPE_FLD]);
				decimal decCrewSize = Convert.ToDecimal(arrShifts[0][PRO_WCCapacityTable.CREWSIZE_FLD]);
				decimal decMachineNo = Convert.ToDecimal(arrShifts[0][PRO_WCCapacityTable.MACHINENO_FLD]);
				decimal decFactor = Convert.ToDecimal(arrShifts[0][PRO_WCCapacityTable.FACTOR_FLD]);
				decimal decTotalDayWorkingTime = 0;
				if (intWCType == (int)WCType.Labor)
				{
					decTotalDayWorkingTime = decTotalDayCapacity / (decCrewSize * decFactor /100);
				}
				else if (intWCType == (int)WCType.Machine)
				{
					decTotalDayWorkingTime = decTotalDayCapacity / (decMachineNo * decFactor /100);
				}
				drowBottle[BOTTLETOTALWORKTIME_FLD] = decTotalWorkingTime;
				if (decTotalWorkingTime != 0)
				{
					drowBottle[BOTTLETOTALCAPACITY_FLD] = Math.Round((decTotalWorkingTime/decTotalDayWorkingTime)*decTotalDayCapacity);
				}
				else
				{
					drowBottle[BOTTLETOTALCAPACITY_FLD] = 0;
				}
				drowBottle[BOTTLEREMAINCAPACITY_FLD] = drowBottle[BOTTLETOTALCAPACITY_FLD];
			}

			#endregion

			return dtbCapacityBottles;
		}


		public void RunMPP(int pintDCOptionMasterID, int pintWorkCenterID, int pintProductionLineID,DataSet pdstDCPData)
		{
			#region Prepare datas
			PRO_DCOptionMasterDS dsDCOptionMaster = new PRO_DCOptionMasterDS();
			DataRow drowDCOptionMaster = dsDCOptionMaster.GetDCOption(pintDCOptionMasterID).Tables[0].Rows[0];
			
			DataTable dtbDelSchData = dsDCOptionMaster.GetDeliveryScheduleData(pintDCOptionMasterID);
			DataTable dtbDelSchTime = dsDCOptionMaster.GetDeliveryScheduleTime(pintDCOptionMasterID);
			DataTable dtbWCConfig = dsDCOptionMaster.GetWCConfigInCycle(pintDCOptionMasterID);
			DataTable dtbWCList = GetWorkCenterList(pintDCOptionMasterID);
			DataTable dtbChangeCategory = dsDCOptionMaster.GetChangeCategory(pintDCOptionMasterID);
			DataTable dtbBOMInfo = dsDCOptionMaster.GetBOMInfo(pintDCOptionMasterID);
			DataTable dtbProduct = dsDCOptionMaster.GetProductInfo(pintDCOptionMasterID);
			DataTable dtbAvailQty = dsDCOptionMaster.GetAvailQuantity();
			DataTable dtbRawResult = CreateRawResultTable();
			DataTable dtbProductionGroup = dsDCOptionMaster.GetProductionGroup();
			DataTable dtbProductPair = dsDCOptionMaster.GetProductPair();
			DataTable dtbPlanningStartDate = dsDCOptionMaster.SelectPlanningStartDate(pintDCOptionMasterID);
			#endregion
			


			#region deleted

			/*
			ReCalculateLeadTime(dtbDelSchData, dtbBOMInfo, dtbProduct, Convert.ToInt32(drowDCOptionMaster[PRO_DCOptionMasterTable.INCLUDECHECKPOINT_FLD]) == 1);
			AssignPGPriority(dtbProduct, dtbProductionGroup);

			//first, clear all data of this wc in cycle
			//dsDCOptionMaster.DeleteDCPResult(pintDCOptionMasterID,pintWorkCenterID);

			#endregion

			DataSet dstDCPResult = dsDCOptionMaster.GetResultDataSet(false);
			//DataRow[] newRow = pdstDCPData

			foreach(DataRow newRow in pdstDCPData.Tables[PRO_DCPResultMasterTable.TABLE_NAME].Select("","",DataViewRowState.Added))
			{
				dstDCPResult.Tables[PRO_DCPResultMasterTable.TABLE_NAME].ImportRow(newRow);
			}

			DataTable dtbCapacityBottles = CreateCapacityBottleForWC(dtbWCList,dtbWCConfig,drowDCOptionMaster,dtbDelSchData,pintWorkCenterID);
			DataRow drowWC = dtbWCList.Select(MST_WorkCenterTable.WORKCENTERID_FLD + "=" + pintWorkCenterID)[0];
			AdjustDeliveryAndBottlesSingleWC(dtbCapacityBottles,pintDCOptionMasterID,drowWC,dstDCPResult,dtbDelSchData,dtbProduct,dtbBOMInfo,dtbWCConfig);

			dsDCOptionMaster.UpdateResultDataSet(dstDCPResult);
			//SaveManualProductionPlanning(pintDCOptionMasterID,pintWorkCenterID,pintProductionLineID,dstDCPResult.Tables[1]);
			//(new PRO_DCPResultDetailDS()).UpdateDataSet(dstDCPResult);
			*/

			#endregion
		}
		

		public void RunMPPNew(int pintDCOptionMasterID, int pintWorkCenterID, int pintProductionLineID,DataSet pdstDCPData, DataTable pdtbIgnoreList, DataTable dtbBeginData)
		{
			#region Prepare datas
			PRO_DCOptionMasterDS dsDCOptionMaster = new PRO_DCOptionMasterDS();
			DataRow drowDCOptionMaster = dsDCOptionMaster.GetDCOption(pintDCOptionMasterID).Tables[0].Rows[0];
			//string strProductIDs = "(546)"; // List products need to re-generate
			DataTable dtbDelSchData = dsDCOptionMaster.GetDeliveryScheduleData(pintDCOptionMasterID);
			//DataTable dtbDelSchTime = dsDCOptionMaster.GetDeliveryScheduleTime(pintDCOptionMasterID);
			// Delete all product need to re-generate
			//dsDCOptionMaster.DeleteRelatedComponentInDCPResult(pintDCOptionMasterID,strProductIDs);
			//DataTable dtbShiftTime = CreateShiftTime()
			DataTable dtbRequiredProducts = dsDCOptionMaster.GetRequiredProducts(pintDCOptionMasterID,pintWorkCenterID); //,strProductIDs);

			//dtbDelSchData.DataSet.WriteXml("C:\\dtbDelSchData.xml");
			//dtbRequiredProducts.DataSet.WriteXml("C:\\ReqPro2.xml");

			DataTable dtbWCConfig = dsDCOptionMaster.GetWCConfigInCycle(pintDCOptionMasterID);
			//dtbWCConfig.DataSet.WriteXml("C:\\dtbWCConfig.xml");
			DataTable dtbWCList = GetWorkCenterList(pintDCOptionMasterID);
			DataTable dtbChangeCategory = dsDCOptionMaster.GetChangeCategory(pintDCOptionMasterID);
			DataTable dtbBOMInfo = dsDCOptionMaster.GetBOMInfo(pintDCOptionMasterID);
//			DataTable dtbProduct = dsDCOptionMaster.GetProductInfo(pintDCOptionMasterID);
			DataTable dtbAvailQty = dsDCOptionMaster.GetAvailQuantity();
			//dsDCOptionMaster.DCPAvailQuantity(pintDCOptionMasterID);//
//			DataTable dtbRawResult = CreateRawResultTable();
//			DataSet dstDCPResult = dsDCOptionMaster.GetResultDataSet(false);
//			DataTable dtbProductPair = dsDCOptionMaster.GetProductPair();

			DataTable dtbWCOutside = dsDCOptionMaster.GetWCOutside();
			DataTable dtbProductionGroup = dsDCOptionMaster.GetProductionGroup();
			DataTable dtbGroupCapacity = dsDCOptionMaster.GetGroupCapacity();
			DataSet dstProductPair = dsDCOptionMaster.GetProductSetupPair();
			//DataTable dtbBeginData = dsDCOptionMaster.GetBeginData(pintDCOptionMasterID);
			DataSet dst = new DataSet();
			dst.Tables.Add(dtbBeginData);
			dst.WriteXml("C:\\BeginData1.xml");

			//ReCalculateLeadTime(dtbDelSchData,dtbBOMInfo,dtbProduct,Convert.ToInt32(drowDCOptionMaster[PRO_DCOptionMasterTable.INCLUDECHECKPOINT_FLD]) == 1);
			//AssignPGPriority(dtbProduct,dtbProductionGroup);

			//DataTable dtbPlanningStartDate = dsDCOptionMaster.SelectPlanningStartDate(pintDCOptionMasterID);
			DataTable dtbShiftTime = CreateShiftTime(drowDCOptionMaster,dtbWCConfig);

			#region Sort production line
			//DataTable dtbTopLevelWC = dsDCOptionMaster.GetTopLevelWorkCenter();
			DataTable dtbRootWC = new DataTable();
			dtbRootWC.Columns.Add("WorkCenterID",typeof(int));
			dtbRootWC.Rows.Add(new object[]{ pintWorkCenterID });
			DataTable dtbNewWCList = dtbWCList.Copy();

			SortWorkCenterList(dtbRootWC,dtbNewWCList,dtbBOMInfo);
			for(int i = dtbNewWCList.Rows.Count - 1; i >= 0; i--)
			{
				DataRow row = dtbNewWCList.Rows[i];
				if(Convert.ToInt32(row[WORKCENTERLEVEL_FLD]) == -1) row.Delete();
			}
			dtbNewWCList.AcceptChanges();

			// update planning offset
			ModifyPlanningOffset(dtbNewWCList, drowDCOptionMaster, dtbBOMInfo);

			//			DataRow[] rowLevel1 = dtbNewWCList.Select(WORKCENTERLEVEL_FLD + " = 1");
			//			DataRow[] rowLevel2 = dtbNewWCList.Select(WORKCENTERLEVEL_FLD + " = 2");
			//			DataRow[] rowLevel3 = dtbNewWCList.Select(WORKCENTERLEVEL_FLD + " = 3");
			//			DataRow[] rowLevel4 = dtbNewWCList.Select(WORKCENTERLEVEL_FLD + " = 4");
			//			DataRow[] rowLevel5 = dtbNewWCList.Select(WORKCENTERLEVEL_FLD + " = 5");
			//			DataRow[] rowLevel6 = dtbNewWCList.Select(WORKCENTERLEVEL_FLD + " = 6");
			//			DataRow[] rowLevel7 = dtbNewWCList.Select(WORKCENTERLEVEL_FLD + " = 7");
			//			DataRow[] rowLevel8 = dtbNewWCList.Select(WORKCENTERLEVEL_FLD + " = 8");
			DataSet dst1 = new DataSet();
			dst1.Tables.Add(dtbNewWCList);
			dst1.WriteXml("C:\\dtbNewWCList.xml");

			
			DataTable dtbWorkCenter = dtbNewWCList.Clone();
			DataRow[] rowLevels = dtbNewWCList.Select(WORKCENTERLEVEL_FLD + " = 0");
			int level = 0;
			string strWorkCenterIDs = "("; // (3,4,5,0)
			while(rowLevels.Length > 0)
			{
				level++;
				foreach(DataRow row in rowLevels)
				{
					dtbWorkCenter.ImportRow(row);
					strWorkCenterIDs += row["workcenterID"] + ",";
				}
				rowLevels = dtbNewWCList.Select(WORKCENTERLEVEL_FLD + " = " + level);
			}
			strWorkCenterIDs += "0)";

			string strChildWCIDs = "(";
			level = 1;
			rowLevels = dtbNewWCList.Select(WORKCENTERLEVEL_FLD + " = 1");
			while(rowLevels.Length > 0)
			{
				level++;
				foreach(DataRow row in rowLevels)
				{
					dtbWorkCenter.ImportRow(row);
					if(pdtbIgnoreList.Select("WorkCenterID = " + row["workcenterID"]).Length == 0)
					{
						strChildWCIDs += row["workcenterID"] + ",";
					}
				}
				rowLevels = dtbNewWCList.Select(WORKCENTERLEVEL_FLD + " = " + level);
			}
			strChildWCIDs +="0)";
			#endregion

			DataTable dtbProductLists = dsDCOptionMaster.GetProductLists(pintDCOptionMasterID, strWorkCenterIDs);

			DataSet dstResultStruct = CreateResultStruct(dtbWorkCenter, dtbWCConfig, drowDCOptionMaster, dtbNewWCList); //dtbWCList

			//dsDCOptionMaster.CloseWorkOrderInCycle(pintDCOptionMasterID);
			//UseCacheAsBegin
//			if((Convert.ToInt32(drowDCOptionMaster["UseCacheAsBegin"]) == 0) && false)
//			{
//				#region Sort production line
//				DataTable dtbTopLevelWC = dsDCOptionMaster.GetTopLevelWorkCenter();
//				SortWorkCenterList(dtbTopLevelWC,dtbWCList,dtbBOMInfo);
//				#endregion
//
//				#region Feed bottles algorithm
//				DataSet dstCapacityBottles = CreateCapacityBottles(dtbWCList,dtbWCConfig,drowDCOptionMaster,dtbDelSchTime);
//				int intResult = AdjustDeliveryAndBottles(drowDCOptionMaster,dstCapacityBottles,dtbDelSchData,dtbWCList,
//					dtbChangeCategory,dtbBOMInfo,dstDCPResult,dtbWCConfig,dtbProduct,dtbAvailQty,
//					dtbRawResult,dtbProductionGroup,dtbProductPair, ref dtbBeginData);
//				#endregion
//				//GetBeginQuantity(drowDCOptionMaster,dstResultStruct,dtbBOMInfo,dtbDelSchData,dtbBeginData,dtbAvailQty,dtbProductLists,dtbWCList, dtbWCOutside);
//			}
				
			//dst.WriteXml("C:\\BeginData2.xml");
			// remove all row belong child workcenter in dtbRequiredProducts
			foreach(DataTable dtbWC in dstResultStruct.Tables)
			{
				if(dtbWC == dstResultStruct.Tables[0]) continue; // keep item in first wc
				DataRow[] rowRemoves = dtbRequiredProducts.Select("WorkCenterID = " + dtbWC.TableName);
				foreach(DataRow rowRemove in rowRemoves)
				{
					rowRemove.Delete();
				}
				// close workorders
				dsDCOptionMaster.CloseWorkOrderInCycleForDCPTool(pintDCOptionMasterID,Convert.ToInt32(dtbWC.TableName),Convert.ToDateTime(drowDCOptionMaster[PRO_DCOptionMasterTable.ASOFDATE_FLD]));

			}

			UpdateEndShiftTime(dstResultStruct, dtbShiftTime);

			//DataSet dstshift = new DataSet();
			//dstshift.Tables.Add(dtbShiftTime);
			//dstshift.WriteXml("C:\\ShiftTime.xml");
			//if(false)
			{
				#region // fill SOdelivery into dtbRequiredProducts data
				foreach(DataRow row in dtbDelSchData.Rows)
				{
					if(Convert.ToInt32(row["WorkCenterID"]) == pintWorkCenterID) continue;
					// khong tinh cac SO delivery cho nhung s/p khong lap ke hoach
					if(dtbWorkCenter.Select("WorkCenterID = " + row["WorkCenterID"]).Length == 0) continue;

					DataRow[] rowShifts = dtbShiftTime.Select("WorkCenterID = " + row["WorkCenterID"] + " AND StartShiftTime <= '" + row["ScheduleDate"] 
						+ "' AND EndShiftTime > '" + row["ScheduleDate"] + "'");
					if(rowShifts.Length > 0)
					{
						DataRow[] rowExisteds = dtbRequiredProducts.Select("WorkCenterID = " + row["WorkCenterID"] + " AND ProductID = " + row["ProductID"] + 
							" AND StartTime >= '" + rowShifts[0]["StartShiftTime"] + "' AND StartTime < '" + rowShifts[0]["EndShiftTime"] + "'"
							/* " AND ShiftID = " + rowShifts[0]["ShiftID"] + " AND WorkingDate = '" + ((DateTime)row["ScheduleDate"]).Date + "' " */ );
						if(rowExisteds.Length == 0)
						{
							// ProductID, WorkingDate, ShiftID, WorkCenterID
							DataRow rowNew = dtbRequiredProducts.NewRow();
							rowNew["ProductID"] = row["ProductID"];
							rowNew["WorkCenterID"] = row["WorkCenterID"];
							rowNew["WorkingDate"] = ((DateTime)row["ScheduleDate"]).Date;
							rowNew["StartTime"] = row["ScheduleDate"];
							rowNew["ShiftID"] = rowShifts[0]["ShiftID"];
							rowNew["Quantity"] = row["DeliveryQuantity"];
							dtbRequiredProducts.Rows.Add(rowNew);
						}
						else
						{
							rowExisteds[0]["Quantity"] = Convert.ToDecimal(rowExisteds[0]["Quantity"]) + Convert.ToDecimal(row["DeliveryQuantity"]);
						}
					}
				}
				// remove all item produce in first wc
				//			DataRow[] rowOnFirstWCNoShiftIDs = dtbRequiredProducts.Select("ShiftID is NULL AND WorkCenterID = " + pintWorkCenterID );
				//			foreach(DataRow row in rowOnFirstWCNoShiftIDs)
				//			{
				//				row.Delete();
				//			}
				//			dtbRequiredProducts.AcceptChanges();
				#endregion
			}
			//dtbRequiredProducts.DataSet.WriteXml("C:\\ReqPro1.xml");
			InsertExistedDataIntoResultStruct(dstResultStruct, dtbAvailQty, dtbShiftTime, dtbWCConfig, drowDCOptionMaster, dtbProductLists,pdstDCPData, 
				dstProductPair.Tables[0], dtbWCOutside, dtbBeginData, drowDCOptionMaster);
			//DataSet dst1= new DataSet();
			//dst1.Tables.Add(dstResultStruct.Tables["532"].Copy());
			//dst1.WriteXml("C:\\BeginQuantity532.xml");

			Hashtable hashProductLists = new Hashtable(dtbProductLists.Rows.Count);
			foreach(DataRow rowProduct in dtbProductLists.Rows)
			{
				hashProductLists.Add(rowProduct["ProductID"],rowProduct);
			}

			#endregion

			DataTable dtbDelivery = (new PRO_DCPResultMasterDS()).GetTableStruct();
				//dtbRequiredProducts.Clone();
			//MessageBox.Show("GetData: "+DateTime.Now.ToString("hh:mm:ss"));
			GenerateForwardDCP(dstResultStruct, dtbRequiredProducts, dtbShiftTime, 
				drowDCOptionMaster, dtbBOMInfo,dtbProductLists, dstProductPair, dtbChangeCategory, 
				dtbProductionGroup, dtbGroupCapacity, dtbWCOutside, dtbDelivery, dtbDelSchData, strChildWCIDs,
				hashProductLists);
			//MessageBox.Show("Generate: "+DateTime.Now.ToString("hh:mm:ss"));
			//dtbRequiredProducts.DataSet.WriteXml("C:\\ReqPro.xml");
			UpdateDataIntoDataBase(dtbRequiredProducts,dtbShiftTime,drowDCOptionMaster,pintDCOptionMasterID,strChildWCIDs, pintWorkCenterID,dstResultStruct,pdtbIgnoreList,hashProductLists);
			//MessageBox.Show("UpdateData: " + DateTime.Now.ToString("hh:mm:ss"));
			//UpdateDelivery(dtbDelivery, pintDCOptionMasterID);
			//if(Convert.ToInt32(drowDCOptionMaster["UseCacheAsBegin"]) == 0)
			//	dsDCOptionMaster.UpdateBeginData(dtbBeginData);
			UpdatePlanningStartDate(dtbNewWCList, pintDCOptionMasterID);
		}

		/// <summary>
		/// Adjust delivery and bottles to insert into dcp result
		/// </summary>
		/// <param name="pdtbCapacityBottles"></param>
		/// <param name="pintDCOptionMasterID"></param>
		/// <param name="pdrowWC"></param>
		/// <param name="pdstDCPResult"></param>
		/// <param name="pdtbDelSchData"></param>
		/// <param name="pdtbProducts"></param>
		/// <param name="pdtbBOMInfo"></param>
		/// <param name="pdtbWCConfig"></param>
		public void AdjustDeliveryAndBottlesSingleWC(DataTable pdtbCapacityBottles, int pintDCOptionMasterID, DataRow pdrowWC, DataSet pdstDCPResult, DataTable pdtbDelSchData, DataTable pdtbProducts, DataTable pdtbBOMInfo, DataTable pdtbWCConfig)
		{
			DataRow[] arrBottles = pdtbCapacityBottles.Select(string.Empty,BOTTLESTARTTIME_FLD + " DESC");

			#region //select all parent products
			DataRow[] arrProducts = pdtbProducts.Select(ITM_ProductTable.PRODUCTIONLINEID_FLD + "=" + pdrowWC[MST_WorkCenterTable.PRODUCTIONLINEID_FLD]);
			string strProductIDs = "(0,";
			foreach (DataRow drowProduct in arrProducts)
			{
				strProductIDs += drowProduct[ITM_ProductTable.PRODUCTID_FLD] + ",";
			}
			strProductIDs = strProductIDs.Substring(0,strProductIDs.Length - 1) + ")";
			DataRow[] arrParentProducts = pdtbBOMInfo.Select(ITM_BOMTable.COMPONENTID_FLD + " IN " + strProductIDs);
			string strParentProductIDs = "(0,";
			foreach (DataRow drowParentProduct in arrParentProducts)
			{
				strParentProductIDs += drowParentProduct[ITM_BOMTable.PRODUCTID_FLD] + ",";
			}
			strParentProductIDs = strParentProductIDs.Substring(0,strParentProductIDs.Length - 1) + ")";

			#endregion

			#region //select all parent DCP result
			DataRow[] arrParentDCPResultMaster = pdstDCPResult.Tables[PRO_DCPResultMasterTable.TABLE_NAME].Select(PRO_DCPResultMasterTable.PRODUCTID_FLD + " IN " + strParentProductIDs);
			string strParentMasterIDs = "(0,";
			foreach (DataRow drowMaster in arrParentDCPResultMaster)
			{
				strParentMasterIDs += drowMaster[PRO_DCPResultMasterTable.DCPRESULTMASTERID_FLD] + ",";
			}
			strParentMasterIDs = strParentMasterIDs.Substring(0,strParentMasterIDs.Length - 1) + ")";

			#endregion

			DataRow[] arrParentDCPResultDetail = pdstDCPResult.Tables[PRO_DCPResultDetailTable.TABLE_NAME].Select(PRO_DCPResultDetailTable.DCPRESULTMASTERID_FLD + " IN " + strParentMasterIDs + " AND " + PRO_DCPResultDetailTable.TYPE_FLD + " = 0");

			#region Scan all parent dcpresultdetail to insert delivery schedule

			foreach (DataRow drowDetail in arrParentDCPResultDetail)
			{
				DataRow drowMaster = pdstDCPResult.Tables[PRO_DCPResultMasterTable.TABLE_NAME].Select(PRO_DCPResultMasterTable.DCPRESULTMASTERID_FLD + "=" + drowDetail[PRO_DCPResultDetailTable.DCPRESULTMASTERID_FLD])[0];
				DataRow drowProduct = pdtbProducts.Select(ITM_ProductTable.PRODUCTID_FLD + "=" + drowMaster[PRO_DCPResultMasterTable.PRODUCTID_FLD])[0];
				DataRow[] arrChildProduct = pdtbBOMInfo.Select(ITM_BOMTable.PRODUCTID_FLD + "=" + drowMaster[PRO_DCPResultMasterTable.PRODUCTID_FLD]);

				#region Insert delivery row into pdtbDelSchData

				foreach (DataRow drowChildProduct in arrChildProduct)
				{
					DataRow drowDelivery = pdtbDelSchData.NewRow();
					drowDelivery[SO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD] = Convert.ToDecimal(drowDetail[PRO_DCPResultDetailTable.QUANTITY_FLD]) * Convert.ToDecimal(drowChildProduct[ITM_BOMTable.QUANTITY_FLD]);
					drowDelivery[SO_DeliveryScheduleTable.SCHEDULEDATE_FLD] = drowDetail[PRO_DCPResultDetailTable.STARTTIME_FLD];
					drowDelivery[SO_SaleOrderDetailTable.PRODUCTID_FLD] = drowChildProduct[ITM_BOMTable.COMPONENTID_FLD];
					drowDelivery[ITM_RoutingTable.ROUTINGID_FLD] = drowChildProduct[ITM_RoutingTable.ROUTINGID_FLD];
					drowDelivery[MST_WorkCenterTable.WORKCENTERID_FLD] = drowChildProduct[MST_WorkCenterTable.WORKCENTERID_FLD];
					drowDelivery[WORKCENTERCODE_FLD] = drowChildProduct[WORKCENTERCODE_FLD];
					drowDelivery[PRO_CheckPointTable.SAMPLEPATTERN_FLD] = drowChildProduct[PRO_CheckPointTable.SAMPLEPATTERN_FLD];
					drowDelivery[PRO_CheckPointTable.SAMPLERATE_FLD] = drowChildProduct[PRO_CheckPointTable.SAMPLERATE_FLD];
					drowDelivery[PRO_CheckPointTable.DELAYTIME_FLD] = drowChildProduct[PRO_CheckPointTable.DELAYTIME_FLD];
					drowDelivery[LEADTIME_FLD] = drowChildProduct[LEADTIME_FLD];
					drowDelivery[ITM_RoutingTable.FIXLT_FLD] = drowChildProduct[ITM_RoutingTable.FIXLT_FLD];
					drowDelivery[MINPRODUCE_FLD] = drowChildProduct[MINPRODUCE_FLD];
					drowDelivery[MAXPRODUCE_FLD] = drowChildProduct[MAXPRODUCE_FLD];
					drowDelivery[CHECKPOINTPERITEM_FLD] = drowChildProduct[CHECKPOINTPERITEM_FLD];
					drowDelivery[ITM_ProductTable.SCRAPPERCENT_FLD] = drowChildProduct[ITM_ProductTable.SCRAPPERCENT_FLD];
					drowDelivery[ITM_ProductTable.ORDERQUANTITYMULTIPLE_FLD] = drowChildProduct[ITM_ProductTable.ORDERQUANTITYMULTIPLE_FLD];
					drowDelivery[ITM_ProductTable.REVISION_FLD] = drowChildProduct[ITM_ProductTable.REVISION_FLD];
					drowDelivery[MAXROUNDUPTOMIN_FLD] = drowChildProduct[MAXROUNDUPTOMIN_FLD];
					drowDelivery[MAXROUNDUPTOMULTIPLE_FLD] = drowChildProduct[MAXROUNDUPTOMULTIPLE_FLD];
					drowDelivery[CAPACITYBOTTLEID_FLD] = -2;
					pdtbDelSchData.Rows.Add(drowDelivery);
				}

				#endregion
			}

			#endregion

			#region //walk through all bottle from future back to present
			foreach (DataRow drowBottle in arrBottles)
			{
				decimal decBottleTotalCapacity = Convert.ToDecimal(drowBottle[BOTTLETOTALCAPACITY_FLD]);
				decimal decBottleTotalWorkingTime = Convert.ToDecimal(drowBottle[BOTTLETOTALWORKTIME_FLD]);
				DateTime dtmBottleWorkingDay = Convert.ToDateTime(drowBottle[BOTTLEWORKINGDAY_FLD]);

				#region //select deliveries
				DataRow[] arrDeliveries = pdtbDelSchData.Select(SO_DeliveryScheduleTable.SCHEDULEDATE_FLD + ">='" + drowBottle[BOTTLEENDTIME_FLD] + "' AND " + CAPACITYBOTTLEID_FLD + " <= 0");

				Hashtable htDeliveryAmount = new Hashtable();
				Hashtable htProductInfo = new Hashtable();
				foreach (DataRow drowDelivery in arrDeliveries)
				{
					int intProductID = Convert.ToInt32(drowDelivery[SO_SaleOrderDetailTable.PRODUCTID_FLD]);
					if (htDeliveryAmount[intProductID] == null)
					{
						htDeliveryAmount[intProductID] = 0;
						DataRow drowProductInfo = pdtbProducts.Select(ITM_ProductTable.PRODUCTID_FLD + "=" + intProductID)[0];
						htProductInfo[intProductID] = drowProductInfo;
					}
					htDeliveryAmount[intProductID] = Convert.ToDecimal(htDeliveryAmount[intProductID]) + Convert.ToDecimal(drowDelivery[SO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD]);
				}

				#endregion

				decimal decFactor = 1;
				decimal decRemainCapacity = Convert.ToDecimal(drowBottle[BOTTLETOTALCAPACITY_FLD]);
				if (Convert.ToBoolean(pdrowWC[PRO_ProductionLineTable.BALANCEPLANNING_FLD]))
				{
					decimal decRequireCapacity = 0;
					foreach (DictionaryEntry objEntry in htDeliveryAmount)
					{
						DataRow drowProductInfo = (DataRow)htProductInfo[objEntry.Key];
						decRequireCapacity += Convert.ToDecimal(htDeliveryAmount[objEntry.Key]) * (Convert.ToDecimal(drowProductInfo[LEADTIME_FLD]) + Convert.ToDecimal(drowProductInfo[CHECKPOINTPERITEM_FLD]));
					}
					if (decRequireCapacity > 0)
					{
						decFactor = decRemainCapacity / decRequireCapacity;
					}
				}

				DateTime dtmStartTime = Convert.ToDateTime(drowBottle[BOTTLESTARTTIME_FLD]);
				DateTime dtmEndTime = dtmStartTime;

				#region Insert into result table (Master & Detail)

				foreach (DictionaryEntry objEntry in htDeliveryAmount)
				{
					DataRow drowProductInfo = (DataRow)htProductInfo[objEntry.Key];

					decimal decMaxProduce = Convert.ToDecimal(drowProductInfo[ITM_ProductTable.MAXPRODUCE_FLD]);
					decimal decMinProduce = Convert.ToDecimal(drowProductInfo[ITM_ProductTable.MINPRODUCE_FLD]);
					decimal decMultipleProduce = Convert.ToDecimal(drowProductInfo[ITM_ProductTable.ORDERQUANTITYMULTIPLE_FLD]);
					decimal decRoundUpToMin = Convert.ToDecimal(drowProductInfo[ITM_ProductTable.MAXROUNDUPTOMIN_FLD]);
					decimal decRoundUpToMultiple = Convert.ToDecimal(drowProductInfo[ITM_ProductTable.MAXROUNDUPTOMULTIPLE_FLD]);

					#region //normalize max and min
					decMultipleProduce = (decMultipleProduce == 0) ? 1 : decMultipleProduce;
					decMaxProduce = (decMaxProduce % decMultipleProduce == 0) ? decMaxProduce : (decMultipleProduce * decimal.Floor(decMaxProduce / decMultipleProduce));
					decMinProduce = (decMinProduce % decMultipleProduce == 0) ? decMinProduce : (decMultipleProduce * (decimal.Floor(decMinProduce / decMultipleProduce) + 1));

					decimal decDeliveryAmount = Convert.ToDecimal(htDeliveryAmount[objEntry.Key]);
					decimal decLeadTime = Convert.ToDecimal(drowProductInfo[LEADTIME_FLD]);
					decimal decCheckpointPerItem = Convert.ToDecimal(drowProductInfo[CHECKPOINTPERITEM_FLD]);

					//single process
					decimal decProduce = decimal.Floor(decDeliveryAmount);
					//compare to min
					if ((decMinProduce != 0) && (decProduce < decMinProduce))
					{
						if (decProduce + decRoundUpToMin >= decMinProduce)
						{
							if ((decMinProduce) * (decLeadTime + decCheckpointPerItem) < decRemainCapacity)
							{
								decProduce = decMinProduce;
							}
							else
							{
								decProduce = 0;
							}
						}
						else
						{
							decProduce = 0;
						}
					}
					//compare to multiple
					if ((decMultipleProduce != 0) && (decProduce % decMultipleProduce != 0))
					{
						if (decProduce + decRoundUpToMultiple >= (decimal.Floor(decProduce / decMultipleProduce) + 1) * decMultipleProduce)
						{
							if ((decimal.Floor(decProduce / decMultipleProduce) + 1) * decMultipleProduce * (decLeadTime + decCheckpointPerItem) < decRemainCapacity)
							{
								decProduce = (decimal.Floor(decProduce / decMultipleProduce) + 1) * decMultipleProduce;
							}
							else
							{
								decProduce = decimal.Floor(decProduce / decMultipleProduce) * decMultipleProduce;
							}
						}
						else
						{
							decProduce = decimal.Floor(decProduce / decMultipleProduce) * decMultipleProduce;
						}
					}
					//compare to max
					if ((decMaxProduce > 0) && (decProduce > decMaxProduce))
					{
						decProduce = decMaxProduce;
					}
					decRemainCapacity -= decProduce * (decLeadTime + decCheckpointPerItem);

					#endregion

					#region //calculate end time and write to dcp result master
					int intProductID = Convert.ToInt32(drowProductInfo[ITM_ProductTable.PRODUCTID_FLD]);
					//master
					DataRow drowMaster = pdstDCPResult.Tables[PRO_DCPResultMasterTable.TABLE_NAME].NewRow();
					drowMaster[PRO_DCPResultMasterTable.DCOPTIONMASTERID_FLD] = pintDCOptionMasterID;
					drowMaster[PRO_DCPResultMasterTable.QUANTITY_FLD] = decProduce;
					drowMaster[PRO_DCPResultMasterTable.STARTDATETIME_FLD] = dtmStartTime;//new DateTime(dtmStartTime.Year,dtmStartTime.Month,dtmStartTime.Day,dtmStartTime.Hour,dtmStartTime.Minute,dtmStartTime.Second);
					drowMaster[PRO_DCPResultMasterTable.WORKCENTERID_FLD] = Convert.ToInt32(pdrowWC[MST_WorkCenterTable.WORKCENTERID_FLD]);
					drowMaster[PRO_DCPResultMasterTable.PRODUCTID_FLD] = intProductID;
					drowMaster[PRO_DCPResultMasterTable.ROUTINGID_FLD] = 
						pdtbProducts.Select(ITM_BOMTable.PRODUCTID_FLD + "=" + intProductID)[0][PRO_DCPResultMasterTable.ROUTINGID_FLD];
						//pdrowWC[PRO_DCPResultMasterTableadmin.ROUTINGID_FLD];//0;
					pdstDCPResult.Tables[PRO_DCPResultMasterTable.TABLE_NAME].Rows.Add(drowMaster);

					#endregion

					//detail - not divided by shift
					decimal decTotalLeadTime = decProduce * (decLeadTime + decCheckpointPerItem);
					decimal decTotalRealTime = decTotalLeadTime * (decBottleTotalWorkingTime / decBottleTotalCapacity);
						
					decimal decRemainRealTime = Decimal.Floor(decTotalRealTime);
					decimal decRemainQuantity = decProduce;

					#region //Insert  StartTime,EndTime,PartQuantity,Percent .v.v into detail

					int intShiftID = 0;
					while (decRemainRealTime > 0)
					{
						decimal decPartRealTime = decRemainRealTime;
						dtmEndTime = CalculateEndTime(dtmStartTime,ref decPartRealTime,pdtbWCConfig,Convert.ToInt32(pdrowWC[MST_WorkCenterTable.WORKCENTERID_FLD]),ref intShiftID,dtmBottleWorkingDay);
						decRemainRealTime -= decPartRealTime;
						decimal decPartQuantity = Math.Round(decProduce * (decPartRealTime / decTotalRealTime));
						decRemainQuantity -= decPartQuantity;
						if (decRemainQuantity == 1)
						{
							decPartQuantity++;
							decRemainQuantity--;
							decRemainRealTime = 0;
						}
						//Insert detail
						DataRow drowDetailRunning = pdstDCPResult.Tables[PRO_DCPResultDetailTable.TABLE_NAME].NewRow();
						drowDetailRunning[PRO_DCPResultMasterTable.PRODUCTID_FLD] = intProductID;

						drowDetailRunning[PRO_DCPResultDetailTable.DCPRESULTMASTERID_FLD] = drowMaster[PRO_DCPResultMasterTable.DCPRESULTMASTERID_FLD];
						drowDetailRunning[PRO_DCPResultDetailTable.STARTTIME_FLD] = dtmStartTime;
						drowDetailRunning[PRO_DCPResultDetailTable.ENDTIME_FLD] = dtmEndTime;
						drowDetailRunning[PRO_DCPResultDetailTable.QUANTITY_FLD] = decPartQuantity;
						drowDetailRunning[PRO_DCPResultDetailTable.SAFETYSTOCKAMOUNT] = 0;
						if (decTotalLeadTime == 0)
						{
							drowDetailRunning[PRO_DCPResultDetailTable.PERCENTAGE_FLD] = 100;
						}
						else
						{
							drowDetailRunning[PRO_DCPResultDetailTable.PERCENTAGE_FLD] = 100 * decPartRealTime / decTotalRealTime;
						}
						drowDetailRunning[PRO_DCPResultDetailTable.SHIFTID_FLD] = intShiftID; //TODO : divide by shift
						drowDetailRunning[PRO_DCPResultDetailTable.TOTALSECOND_FLD] = Decimal.Floor(decPartRealTime * (decBottleTotalCapacity / decBottleTotalWorkingTime));
						drowDetailRunning[PRO_DCPResultDetailTable.TYPE_FLD] = DCPResultTypeEnum.Running;
						drowDetailRunning[PRO_DCPResultDetailTable.WORKINGDATE_FLD] = dtmBottleWorkingDay;
						pdstDCPResult.Tables[PRO_DCPResultDetailTable.TABLE_NAME].Rows.Add(drowDetailRunning);
						dtmStartTime = dtmEndTime;
					}

					#endregion

					drowMaster[PRO_DCPResultMasterTable.DUEDATETIME_FLD] = dtmEndTime; //new DateTime(dtmEndTime.Year,dtmEndTime.Month,dtmEndTime.Day,dtmEndTime.Hour,dtmEndTime.Minute,dtmEndTime.Second);
					dtmStartTime = dtmEndTime;
					

					//pair process

					//recalculate factor
				}

				#endregion
			}

			#endregion

			//update dcpresult
		}
	
		
		/// <summary>
		/// Get MainWorkCenterID base on pintProductionLineID
		/// </summary>
		/// <param name="pintProductionLineID"></param>
		/// <returns></returns>
		internal int GetMainWorkCenterID(int pintProductionLineID)
		{
			PRO_DCOptionMasterDS dsDCOptionMaster = new PRO_DCOptionMasterDS();
			return dsDCOptionMaster.GetMainWorkCenterID(pintProductionLineID);
		}


		public void SaveManualProductionPlanning(int pintDCOptionMasterID, int pintWorkCenterID, int pintProductionLineID,DataTable pdtbDCPResultDetail)
		{
			//(new PRO_DCOptionMasterDS()).UpdateResultDataSet(pdtbDCPResult);
			PRO_DCPResultMasterDS dsMaster = new PRO_DCPResultMasterDS();

			DataSet dstMaster = (new PRO_DCPResultMasterDS()).ListMasterStructTable();

			foreach(DataRow row in pdtbDCPResultDetail.Select("","",DataViewRowState.Added))
			{
				PRO_DCPResultMasterVO voMaster = new PRO_DCPResultMasterVO();
				voMaster.StartDateTime = (DateTime)row[PRO_DCPResultDetailTable.STARTTIME_FLD];
				voMaster.DueDateTime = (DateTime)row[PRO_DCPResultDetailTable.ENDTIME_FLD];
				voMaster.Quantity = Convert.ToInt32(row[PRO_DCPResultDetailTable.QUANTITY_FLD]);
				voMaster.DCOptionMasterID = pintDCOptionMasterID;
				voMaster.ProductID = Convert.ToInt32(row[ITM_PictureTable.PRODUCTID_FLD]);
				voMaster.WorkCenterID = pintWorkCenterID;
				voMaster.RoutingID = dsMaster.GetRoutingID(voMaster.ProductID, voMaster.WorkCenterID);

				row[PRO_DCPResultDetailTable.DCPRESULTMASTERID_FLD] = dsMaster.AddAndReturnID(voMaster);
				row[PRO_DCPResultDetailTable.PERCENTAGE_FLD] = 100;
				row[PRO_DCPResultDetailTable.TOTALSECOND_FLD] = voMaster.DueDateTime.Subtract(voMaster.StartDateTime).Seconds;

				row["Type"] = 0;
				row["IsManual"] = 1;
				row["Converted"] = 0;
				
			}
			//PRO_DCPResultMasterDS dsDCPMaster = new PRO_DCPResultMasterDS();
			//dsDCPMaster.UpdateDCPResultMaster(dstMaster);
			DataSet dstDCP = pdtbDCPResultDetail.DataSet.Copy();
			(new PRO_DCPResultDetailDS()).UpdateDataSet(dstDCP);
		}


		void GenerateForwardDCP(DataSet pdstResultStruct, DataTable pdtbRequiredProducts, DataTable pdtbShiftTime, DataRow pdrowDCPOption,
			DataTable pdtbBOM, DataTable pdtbProductLists, DataSet pdstProductPair, DataTable pdtbChangeCategory, 
			DataTable pdtbProductionGroup, DataTable pdtbGroupCapacity,
			DataTable pdtbWCOutside, DataTable pdtbDelivery, DataTable pdtbDelSchData, string strChildWCIDs,
			Hashtable hashProductLists)
		{


			// Generate by Shifts
			// Duyet tung shift trong thang fill san pham vao phan nang luc thua (chua su dung) theo thu tu uu tien cua san pham
			DataTable dtbOrderProduce = new DataTable();
			dtbOrderProduce.Columns.Add("WorkCenterID");
			//dtbOrderProduce.Columns.Add("WorkCenterCode");
			dtbOrderProduce.Columns.Add("ColumnName");
			dtbOrderProduce.Columns.Add("OrderNo");
			int OrderNo = 0;
			dtbOrderProduce.Columns.Add("OrderPlan");
			dtbOrderProduce.Columns.Add("ShiftID");
			dtbOrderProduce.Columns.Add("DCOptionMasterID");
			//dtbOrderProduce.Columns.Add("ProductCode");
			//dtbOrderProduce.Columns.Add("ProductName");
			//dtbOrderProduce.Columns.Add("ProductModel");

			foreach(DataTable dtbWC in pdstResultStruct.Tables)
			{
				Hashtable hashWC = new Hashtable(dtbWC.Rows.Count);
				foreach(DataRow rowProduct in dtbWC.Rows)
				{
					hashWC.Add(rowProduct["ProductID"] + "-" + rowProduct["Plan"],rowProduct);
				}

				DateTime dtmAsOfDate = (DateTime)pdrowDCPOption["AsOfDate"];
				DateTime dtmEndOfCycle = dtmAsOfDate.AddDays(Convert.ToDouble(pdrowDCPOption[PRO_DCOptionMasterTable.PLANHORIZON_FLD]));
				//DataTable dtbStandardTable = pdtbRequiredProducts.Clone();

				#region // Calculate DeliveryQty = SO(delivery) + Parent(produce) 
				if(dtbWC.TableName != pdstResultStruct.Tables[0].TableName)
				{
					foreach(DataRow row in dtbWC.Select("Plan = 'Delivery'"))
					{
						int intTotalDelivery = 0;
						int intTotalShiftHasDelivery = 0;
						int intBeginQuantity = dtbWC.Columns.IndexOf("BeginQuantity");
						// process for each shift
						foreach(DataColumn col in dtbWC.Columns)
						{
							if(col.ColumnName.Length >= 8)
							{
								string strDate = col.ColumnName.Substring(0,8);
								int intYear = 0, intMonth = 0, intDay = 0;
								try
								{
									intYear = Convert.ToInt32(strDate.Substring(0,4));
									intMonth = Convert.ToInt32(strDate.Substring(4,2));
									intDay = Convert.ToInt32(strDate.Substring(6,2));
								}
								catch{continue;}

								DateTime dtmDate;
								if(intYear > 0)
								{
									dtmDate = new DateTime(intYear, intMonth, intDay);
									strDate = col.ColumnName;
									int intSeparator = strDate.IndexOf('-');
									int intShiftID = 0;
									if(intSeparator > 0)
									{
										intShiftID = Convert.ToInt32(strDate.Substring(intSeparator + 1, strDate.Length-intSeparator-1));
									}
									
									if(intShiftID > 0)
									{

										int intDelivery = 0;
										//string strParentintMultipleIDs = "(";
										DataRow rowShift = pdtbShiftTime.Select("WorkCenterID = " + dtbWC.TableName + " AND WorkingDate = '" + dtmDate 
											+ "' AND ShiftID = " + intShiftID )[0];

										// OriginalScheduleDate
										if(col.Ordinal == intBeginQuantity + 1)
										{
											DataRow[] rowDeliveryOvers = pdtbDelSchData.Select("ProductID = " + row["ProductID"] + " AND ScheduleDate < '" + ((DateTime)rowShift["StartShiftTime"]) + "'");
											int intOver = 0;
											foreach(DataRow rowOver in rowDeliveryOvers)
											{
												intOver += Convert.ToInt32(rowOver["DeliveryQuantity"]);
											}
											DataRow overCicle = (DataRow)hashWC[row["ProductID"] + "-Over"];
											//dtbWC.Select("Plan = 'Over' AND ProductID = " + row["ProductID"])[0];
											overCicle["OverCircle"] = Convert.ToInt32(overCicle["OverCircle"]) + intOver;
											DataRow rowCongDon = (DataRow)hashWC[row["ProductID"] + "-CongDon"];
											rowCongDon["BeginQuantity"] = Convert.ToInt32(rowCongDon["BeginQuantity"]) + intOver;
											intTotalDelivery += intOver;
										}

										DataRow[] rowParents = pdtbBOM.Select("ComponentID = " + row["ProductID"]);
										double dblLTSafetyStock = Convert.ToDouble(((DataRow)hashProductLists[row["ProductID"]])["LTSafetyStock"]);
										foreach(DataRow rowParent in rowParents)
										{
											// dblLeadTimeOffset += dblLTSafetyStock update business
											double dblLeadTimeOffset = Convert.ToDouble(rowParent["LeadTimeOffset"]) + dblLTSafetyStock;

											DataRow[] rows = pdtbRequiredProducts.Select(" ProductID = " + rowParent["ProductID"] //+ " AND WorkingDate = '" + dtmDate 
												+ " AND StartTime >= '" + ((DateTime)rowShift["StartShiftTime"]).AddSeconds(dblLeadTimeOffset)
												+ "' AND StartTime < '" + ((DateTime)rowShift["EndShiftTime"]).AddSeconds(dblLeadTimeOffset) + "'" 
												/*"' AND ShiftID = " + intShiftID*/ );
											// Get quantity for the first shift of circle
											if(col.Ordinal == intBeginQuantity + 1)
											{
												DataRow[] rowOvers = pdtbRequiredProducts.Select(" ProductID = " + rowParent["ProductID"] //+ " AND WorkingDate = '" + dtmDate 
													+ " AND StartTime < '" + ((DateTime)rowShift["StartShiftTime"]).AddSeconds(dblLeadTimeOffset)
													+ "'");
												int intOver = 0;
												foreach(DataRow rowOver in rowOvers)
												{
													intOver += Convert.ToInt32(rowOver["Quantity"]) * Convert.ToInt32(rowParent["Quantity"]);
												}

												DataRow overCicle = (DataRow)hashWC[row["ProductID"] + "-Over"];
													//dtbWC.Select("Plan = 'Over' AND ProductID = " + row["ProductID"])[0];
												overCicle["OverCircle"] = Convert.ToInt32(overCicle["OverCircle"]) + intOver;

												DataRow rowCongDon = (DataRow)hashWC[row["ProductID"] + "-CongDon"];
												rowCongDon["BeginQuantity"] = Convert.ToInt32(rowCongDon["BeginQuantity"]) + intOver;
												intTotalDelivery += intOver;
											}

											foreach(DataRow rowc in rows)
											{
												intDelivery += Convert.ToInt32(rowc["Quantity"]) * Convert.ToInt32(rowParent["Quantity"]);
											}
										}

										if(col.Ordinal == intBeginQuantity + 1)
										{
											DataRow rowOverCicle = (DataRow)hashWC[row["ProductID"] + "-Over"];
											//if(Convert.ToInt32(rowStockCicle["BeginQuantity"]) > 0)
											//{
											DataRow rowStockCicle = (DataRow)hashWC[row["ProductID"] + "-Stock"];
											int intStock = Convert.ToInt32(rowStockCicle["BeginQuantity"]);
											//if(Convert.ToInt32(rowStockCicle["BeginQuantity"]) < intMinOverOrStock)
											//	intMinOverOrStock = Convert.ToInt32(rowStockCicle["BeginQuantity"]);
											rowStockCicle["BeginQuantity"] = Convert.ToInt32(rowStockCicle["BeginQuantity"]) - Convert.ToInt32(rowOverCicle["OverCircle"]);
											if(Convert.ToInt32(rowStockCicle["BeginQuantity"]) < 0) rowStockCicle["BeginQuantity"] = 0;
											if(Convert.ToInt32(rowOverCicle["OverCircle"]) - intStock > 0)
												rowOverCicle["OverCircle"] = Convert.ToInt32(rowOverCicle["OverCircle"]) - intStock;
											else rowOverCicle["OverCircle"] = 0;
											int intSafetyStock = 0; // Convert.ToInt32(((DataRow)hashProductLists[row["ProductID"]])["SafetyStock"]);
											DataRow rowCongDon = (DataRow)hashWC[row["ProductID"] + "-CongDon"];
											rowCongDon["BeginQuantity"] = Convert.ToInt32(rowOverCicle["OverCircle"])+Convert.ToInt32(rowStockCicle["BeginQuantity"]) /*intStock*/ - intSafetyStock;

											//}
										}

										// and SO delivery
										DataRow[] rowSOs = pdtbRequiredProducts.Select(" ProductID = " + row["ProductID"] //+ " AND WorkingDate = '" + dtmDate
											+ " AND StartTime >= '" + rowShift["StartShiftTime"] + "' AND StartTime < '" + rowShift["EndShiftTime"] + "'"  /*"' AND ShiftID = " + intShiftID*/ );
										foreach(DataRow rowSO in rowSOs)
										{
											intDelivery += Convert.ToInt32(rowSO["Quantity"]);
											rowSO.Delete();
										}

										//strParentIDs += "0)";
										row[col.ColumnName] = intDelivery;
										intTotalDelivery += intDelivery;
										if(intDelivery > 0) intTotalShiftHasDelivery ++;
									}
								}
							}
						}
						// Tinh tong luong giao hang va AVERAGE
						DataRow itemInfo = (DataRow)hashProductLists[row["ProductID"]]; //pdtbProductLists.Select("ProductID = " + row["ProductID"])[0];
						itemInfo["TotalDelivery"] = intTotalDelivery;
						if(intTotalShiftHasDelivery > 0)
						{
							DataRow rowBeginStock = (DataRow) hashWC[row["ProductID"] + "-Stock"];
							itemInfo["AverageDelivery"] = (intTotalDelivery-Convert.ToInt32(rowBeginStock["BeginQuantity"]) )/intTotalShiftHasDelivery;
							itemInfo["DeliveryTimes"] = intTotalShiftHasDelivery;
						}
					}
				}
				#endregion

				//pdtbProductLists.DataSet.WriteXml("C:\\pdtbProductLists.xml");

				DataRow rowCapacity = (DataRow)hashWC["0-Capacity"];
					
				int intPrevProductID = 0;

				int intIndexOfBeginQty = dtbWC.Columns.IndexOf("BeginQuantity");
				int indexCol = intIndexOfBeginQty + 1;

				#region // scan columns to planning
				while(indexCol < dtbWC.Columns.Count)
				{
					string strColumName = dtbWC.Columns[indexCol].ColumnName;

					DataTable dtbGroupCapacityCopy = pdtbGroupCapacity.Copy();
					#region // scan rows to fill Rate and ProduceC and Produce by Pair
					foreach(DataRow rowRate in dtbWC.Select("Plan = 'Rate'"))
					{
						decimal decDelQty = Convert.ToInt32(((DataRow)hashWC[rowRate["ProductID"] + "-Delivery"])[indexCol]);
						decimal decStocQty = Convert.ToInt32(((DataRow)hashWC[rowRate["ProductID"] + "-Stock"])[indexCol-1]);
						decimal decOverQty = Convert.ToInt32(((DataRow)hashWC[rowRate["ProductID"] + "-Over"])[indexCol-1]);

						DataRow rowProduceC = (DataRow)hashWC[rowRate["ProductID"] + "-ProduceC"];
						rowProduceC[indexCol] = decDelQty - decStocQty - decOverQty;

						// restore setup pair
						rowRate["SetupPair"] = rowProduceC["SetupPair"];

						if(decDelQty > 0)
							rowRate[indexCol] = (decStocQty+decOverQty)/decDelQty;
						else rowRate[indexCol] = decimal.MaxValue;

					}

					#region // s4: xem set co lam theo bo khong?
					if(pdstProductPair.Tables[0].Select("WorkCenterID = " + dtbWC.TableName).Length > 1)
					{	// 
						foreach(DataRow rowPair in pdstProductPair.Tables[1].Select("WorkCenterID = " + dtbWC.TableName))
						{
							DataRow[] rowProduceCPairs = dtbWC.Select("Plan = 'ProduceC' AND SetupPair = " + rowPair["SetupPair"], "[" + strColumName + "] DESC");
							if(rowProduceCPairs.Length > 1)
							{
								if(rowPair["SetupPair"].ToString() == "10" ) {int bug=0;}
								
								bool blnNotLamTheoBo = false;
								for(int i = 1; i < rowProduceCPairs.Length; i++)
								{
									int intMultiple = Convert.ToInt32(((DataRow)hashProductLists[rowProduceCPairs[0]["ProductID"]])["OrderQuantityMultiple"]); 
									//Convert.ToInt32(pdtbProductLists.Select("ProductID = " + rowProduceCPairs[0]["ProductID"])[0]["OrderQuantityMultiple"]);
									if( Math.Abs(Convert.ToInt32(rowProduceCPairs[0][indexCol]) - Convert.ToInt32(rowProduceCPairs[i][indexCol])) >= intMultiple)
									{
										blnNotLamTheoBo = true;
										break;
									}
								}

								bool blnAllDeliveryEqualZezo = true;
								foreach(DataRow rowProPair in rowProduceCPairs)
								{
									DataRow rowPairDelivery = (DataRow) hashWC[rowProPair["ProductID"]+"-Delivery"];
									if(Convert.ToInt32(rowPairDelivery[indexCol]) > 0)
									{
										blnAllDeliveryEqualZezo = false;
										break;
									}
								}

								if(blnAllDeliveryEqualZezo)
								{
									blnNotLamTheoBo = blnNotLamTheoBo && true;
								}

								if(blnNotLamTheoBo) // Neu ton tai mot cap co gia tri tuyet doi cua ProduceC_R-ProduceC_L >= R_Multiple thi se khong lam theo bo (gia su ProduceC_R la max trong setuppair)
								{
									foreach(DataRow rowProducePair in rowProduceCPairs)
									{
										DataRow rowRate = (DataRow)hashWC[rowProducePair["ProductID"] + "-Rate"];
										rowRate["SetupPair"] = 0;
									}
								}
								else // Nguoc lai thi se lam theo bo
								{
									foreach(DataRow rowProducePair in rowProduceCPairs)
									{
										DataRow rowDel = (DataRow)hashWC[rowProducePair["ProductID"] + "-Delivery"];
										rowProducePair["SetupPair"] = rowDel["SetupPair"];
									}

									// Thiet lap Rate cua nhom sp theo Min Rate
									DataRow rowMinRate = dtbWC.Select("Plan = 'Rate' AND SetupPair = " + rowProduceCPairs[0]["SetupPair"], "[" + strColumName + "] ASC")[0];
									decimal decMinRate = Convert.ToDecimal(rowMinRate[indexCol]);
									foreach(DataRow rowProducePair in rowProduceCPairs)
									{
										DataRow rowRate = (DataRow)hashWC[rowProducePair["ProductID"] + "-Rate"];
										//dtbWC.Select("Plan = 'Rate' AND ProductID = " + rowProducePair["ProductID"])[0];
										rowRate[indexCol] = decMinRate;
									}
								}
							}
						}
					}
					#endregion

					#endregion

					#region // fill produce quantity from min Rate to Max rate
					ArrayList arrProductFilled = new ArrayList();
					ArrayList arrPlannedProduct = new ArrayList();
					ArrayList arrProductSmallRate = new ArrayList();
					Stack arrStack = new Stack();

					#region // put all item that Rate < 1 into stack
					DataRow[] rowRateSmalls = dtbWC.Select("Plan = 'Rate'  AND [" + strColumName + "] < 1" , "[" + strColumName + "] ASC, Model ASC, ProductID ASC");
					if(rowRateSmalls.Length > 0)
					{
						arrStack.Push(rowRateSmalls[0]["ProductID"]);
						foreach(DataRow rowRateSmall in rowRateSmalls)
						{
							//arrStack.Push(rowRateSmall["ProductID"]);
							arrProductSmallRate.Add(rowRateSmall["ProductID"]);
						}
					}
//					else
//					{
//						DataRow[] rowRateGreaterThanOnes = dtbWC.Select("Plan = 'Rate' AND [" + strColumName + "] >= 1", "[" + strColumName + "] ASC, ProductID ASC");
//						if(rowRateGreaterThanOnes.Length > 0)
//						{
//							arrStack.Push(rowRateGreaterThanOnes[0]["ProductID"]);
//						}
//					}

					SelectProductToPlanning(arrStack,arrProductSmallRate,pdtbChangeCategory,dtbWC,0,
						(DataRow)hashWC[intPrevProductID + "-Rate"],indexCol,strColumName,arrPlannedProduct,hashWC,intPrevProductID,hashProductLists);

					#endregion

					//foreach(DataRow rowOrder in rowProductOrderRate)
					while(arrStack.Count > 0)
					{
						int intProductID = Convert.ToInt32(arrStack.Pop());
						DataRow rowOrder = (DataRow)hashWC[intProductID + "-Rate"];
						DataRow itemInfo = (DataRow) hashProductLists[rowOrder["ProductID"]]; 
						int intDeliveryTimes = Convert.ToInt32(itemInfo["DeliveryTimes"]);
						int intAverageDelivery = Convert.ToInt32(itemInfo["AverageDelivery"]);
						int intTotalDelivery = Convert.ToInt32(itemInfo["TotalDelivery"]);
						int intSafetyStock = Convert.ToInt32(itemInfo["SafetyStock"]);
						decimal decScrapPercent = Convert.ToInt32(itemInfo["ScrapPercent"]);
						//if (Convert.ToInt32(pdrowDCPOption["SafetyStock"]) == 0)
						{
							intSafetyStock = 0;
						}

						//dtbWC.Select("Plan = 'Rate' AND ProductID = " + intProductID)[0];
						if(arrProductSmallRate.Contains(intProductID))
						{
							arrProductSmallRate.Remove(intProductID);
						}
						OrderNo++;


						#region // Compute change category time
						int intChangeTime = 0;
						DataRow[] rowChangeCategorys = pdtbChangeCategory.Select("WorkCenterID = " + dtbWC.TableName + " AND SourceProductID = " + intPrevProductID + " AND DestProductID = " + rowOrder["ProductID"]);
						if(rowChangeCategorys.Length > 0)
						{
							intChangeTime = Convert.ToInt32(rowChangeCategorys[0]["CHANGETIME"]);
						}

						#endregion

						// update other info which planned by SetupPair
						if(arrProductFilled.Contains(rowOrder["ProductID"])) 
						{

							#region // modify produce quantity: if ProduceC <= 0 then Produce = 0
							DataRow rowStockFilled = (DataRow)hashWC[rowOrder["ProductID"] + "-Stock"];
							DataRow rowDeliveryFilled = (DataRow)hashWC[rowOrder["ProductID"] + "-Delivery"];
							DataRow rowProduceFilled = (DataRow)hashWC[rowOrder["ProductID"] + "-Produce"];
							DataRow rowProduceCFilled = (DataRow)hashWC[rowOrder["ProductID"] + "-ProduceC"];
							DataRow rowOverFilled = (DataRow)hashWC[rowOrder["ProductID"] + "-Over"];
							DataRow rowTotalSecondFilled = (DataRow)hashWC[rowOrder["ProductID"] + "-TotalSecond"];
							DataRow rowCongDonFilled = (DataRow)hashWC[rowOrder["ProductID"] + "-CongDon"];
							int intDeliveryFilled = 0;
							if(rowDeliveryFilled[indexCol] != DBNull.Value) intDeliveryFilled = Convert.ToInt32(rowDeliveryFilled[indexCol]);
							int intStockFilled = 0;
							if(rowStockFilled[indexCol - 1] != DBNull.Value)
								intStockFilled = Convert.ToInt32(rowStockFilled[indexCol - 1]);
					

							int intOverQtyFilled = Convert.ToInt32(rowOverFilled[indexCol-1]);
							int intProduceCFilled = Convert.ToInt32(rowProduceCFilled[indexCol]);
								// intDeliveryFilled - intStockFilled - intOverQtyFilled;
						
							#region // stock and over

							rowStockFilled[indexCol] = intStockFilled + intOverQtyFilled - intDeliveryFilled + Convert.ToInt32(rowProduceFilled[indexCol]);
							if(intProduceCFilled - Convert.ToInt32(rowProduceFilled[indexCol]) > 0)
							{
								rowOverFilled[indexCol] = //Convert.ToInt32(rowOverFilled[indexCol]) + [sua bi nhan doi]
									intProduceCFilled - Convert.ToInt32(rowProduceFilled[indexCol]);
							}

							if(Convert.ToInt32(rowOverFilled[indexCol]) < 0)
							{
								rowOverFilled[indexCol] = 0;
							}
							else if(intStockFilled + intOverQtyFilled - intDeliveryFilled + Convert.ToInt32(rowProduceFilled[indexCol]) >= 0)
							{
								rowOverFilled[indexCol] = 0;
							}

							#endregion

							rowCongDonFilled[indexCol] = Convert.ToInt32(rowCongDonFilled[indexCol-1]) + Convert.ToInt32(rowProduceFilled[indexCol])
								+ Convert.ToInt32(rowOverFilled[indexCol]);

						{
							//Neu ProduceC<=0 và (Tong Delivery+ SafetyStock )> (Cong don + SafetyStock) >= Tong Delivery
							// thi ProduceC= Tong Delivery-Cong don (Tong Delivery= Tong Delivery+OverCycle)
							// else
							if((intProduceCFilled <= 0) && ((intSafetyStock + intTotalDelivery)>(Convert.ToInt32(rowCongDonFilled[indexCol]) + intSafetyStock) ) 
								&& ((Convert.ToInt32(rowCongDonFilled[indexCol])+intSafetyStock ) >= intTotalDelivery))
							{
								intProduceCFilled = intTotalDelivery /*+ intSafetyStock*/ - Convert.ToInt32(rowCongDonFilled[indexCol-1]);
								//blnSuaOver = true;
								rowProduceCFilled[indexCol] = intProduceCFilled;
								if(intProduceCFilled > 0)
								{
									if(intProduceCFilled - Convert.ToInt32(rowProduceFilled[indexCol]) > 0)
										rowOverFilled[indexCol] = intProduceCFilled - Convert.ToInt32(rowProduceFilled[indexCol]);
									else
										rowOverFilled[indexCol] = 0;
								}
							}
						}
							// tinh lai cong dong
							rowCongDonFilled[indexCol] = Convert.ToInt32(rowCongDonFilled[indexCol-1]) + Convert.ToInt32(rowProduceFilled[indexCol])
								+ Convert.ToInt32(rowOverFilled[indexCol]);

							DataRow itemInfoxx = (DataRow)hashProductLists[rowOrder["ProductID"]]; 
							//Neu Cong don >= Sum( DeliveryQty) thi
							//Neu Cong don >= Tong Delivery va Stock(n-1)- Delivery(n)>=0 thi OverQty(m,n)=0
							if( (Convert.ToInt32(rowCongDonFilled[indexCol]) > Convert.ToInt32(itemInfoxx["TotalDelivery"]))
								&& (Convert.ToInt32(rowStockFilled[indexCol-1]) >= Convert.ToInt32(rowDeliveryFilled[indexCol])) )
							{
								rowOverFilled[indexCol] = 0;
							}
							#endregion

							if(Convert.ToInt32(((DataRow)hashWC[rowOrder["ProductID"]+"-Produce"])[indexCol]) > 0)
							{
								intPrevProductID = intProductID;
								// add order produce
								AddOrderProduce(intPrevProductID,intProductID,indexCol,dtbOrderProduce,dtbWC,pdrowDCPOption,OrderNo);
							}
	
							arrPlannedProduct.Add(intProductID);

							SelectProductToPlanning(arrStack, arrProductSmallRate, pdtbChangeCategory, dtbWC,
							                        intProductID, rowOrder, indexCol, strColumName, arrPlannedProduct,hashWC,
													intPrevProductID,hashProductLists);

							//arrPlannedProduct.Add(intPrevProductID);
							continue;
						}

						DataRow rowItemInfos = (DataRow) hashProductLists[rowOrder["ProductID"]]; //pdtbProductLists.Select("ProductID = " + rowOrder["ProductID"]);
						//if(rowItemInfos.Length == 0) continue;

						#region Tinh ProduceC 

						DataRow rowStock = (DataRow) hashWC[rowOrder["ProductID"] + "-Stock"];
						DataRow rowDelivery = (DataRow) hashWC[rowOrder["ProductID"] + "-Delivery"];
						DataRow rowProduce = (DataRow) hashWC[rowOrder["ProductID"] + "-Produce"];
						DataRow rowTotalSecond = (DataRow) hashWC[rowOrder["ProductID"] + "-TotalSecond"];
						// ProduceC(m,n)=DeliveryQty(m,n)- StockQty(m,n-1) - Over(m,n-1)
						int intDelivery = 0;
						if (rowDelivery[indexCol] != DBNull.Value) intDelivery = Convert.ToInt32(rowDelivery[indexCol]);
						int intStock = 0;
						if (rowStock[indexCol - 1] != DBNull.Value)
							intStock = Convert.ToInt32(rowStock[indexCol - 1]);

						int intOverQty = Convert.ToInt32(((DataRow) hashWC[rowOrder["ProductID"] + "-Over"])[indexCol - 1]);
						int intProduceC = intDelivery - intStock - intOverQty;
						
						#region // Update ProduceC

						DataRow rowCongDon = (DataRow) hashWC[rowOrder["ProductID"] + "-CongDon"];
						//dtbWC.Select("Plan = 'CongDon' AND ProductID = " + rowOrder["ProductID"])[0];
						int intCongDon = Convert.ToInt32(rowCongDon[indexCol - 1]);

						//Neu ProduceC(m,n)< AVEG(m) va Tong (SafetyStock+Produce+Stockdau)< Tong Delivery(m) thi ProduceC(m,n)= AVEG(m)
						if ((intProduceC < intAverageDelivery)
							&& (intCongDon < intTotalDelivery)
							//&& (intDeliveryTimes > 2)
							)
						{
							if (Convert.ToInt32(itemInfo["AVEG"]) == 1)
							{
								intProduceC = intAverageDelivery;
							}
						}
							//Neu ProduceC(m,n)< AVEG(m) va Tong (SafetyStock+Produce+Stockdau)>= Tong Delivery(m) thi ProduceC(m,n)=0
						else if ((intProduceC < intAverageDelivery)
							&& (intCongDon >= intTotalDelivery))
						{
							intProduceC = 0;
						}
						else if ((intProduceC >= intAverageDelivery)
							&& (intCongDon >= intTotalDelivery))
						{
							intProduceC = 0;
						}
						bool blnSuaOver = false; // theo nghiep vu moi chua dua vao excel
						//Neu ProduceC<=0 và (Tong Delivery+ SafetyStock )> (Cong don + SafetyStock) >= Tong Delivery
						// thi ProduceC= Tong Delivery-Cong don (Tong Delivery= Tong Delivery+OverCycle)
						// else
						if((intProduceC <= 0) && ((intSafetyStock + intTotalDelivery)>(intCongDon +intSafetyStock )) 
							&& ((intCongDon +intSafetyStock ) >= intTotalDelivery))
						{
							intProduceC = intTotalDelivery /*+ intSafetyStock*/ - intCongDon;
							blnSuaOver = true;
						}

						DataRow rowProduceC = (DataRow) hashWC[rowOrder["ProductID"] + "-ProduceC"];
						intProduceC = intProduceC + (int)(intProduceC*(decScrapPercent/100));
						rowProduceC[indexCol] = intProduceC;

						#endregion
						#endregion Tinh ProduceC 

						#region Tinh luong produce
						if(intProductID == 326 && strColumName == "20061201-2")
						{
							int bug=0;
						}
						int intProduce = intProduceC;

						if (intProduce < 0) intProduce = 0;
						int intMinProduce = Convert.ToInt32(rowItemInfos["MinProduce"]);
						int intMaxProduce = Convert.ToInt32(rowItemInfos["MaxProduce"]);
						int intMultiple = Convert.ToInt32(rowItemInfos["OrderQuantityMultiple"]);
						// Neu ProduceC(m,n)>0 va nho hon MinQty thi ProduceUp(m,n)=MinQty
						if (intProduce > 0 && intProduce < intMinProduce)
						{
							intProduce = intMinProduce;
						}
							// Neu ProduceC(n)>0 va lon hon MinQty va nho hon Max thi ProduceUp(n)=RoundUp(ProduceC(n),multiQty)
						else if (intProduce > intMinProduce && intProduce <= intMaxProduce)
						{
							if (intMultiple > 0)
							{
								if (intProduce%intMultiple > 0)
								{
									intProduce = intMultiple*((int) (intProduce/intMultiple) + 1);
								}
							}
						}
							// Neu ProduceC(n)>0 va lon hon MinQty va Max = 0 thi ProduceUp(n)=RoundUp(ProduceC(n),multiQty)
						else if ((intProduce > intMinProduce) && (intMaxProduce == 0))
						{
							if (intMultiple > 0)
							{
								if (intProduce%intMultiple > 0)
								{
									intProduce = intMultiple*((int) (intProduce/intMultiple) + 1);
								}
							}
						}
							// Neu ProduceC(n) >0 va lon hon MaxQty thi ProduceUp(n)=MaxQty. Con lai chenh lech ProduceC(n)-MaxQty luu lai voi ShiftID is Null
						else if ((intProduce > intMaxProduce) && (intMaxProduce > 0))
						{
							//if(intMaxProduce > 0)
							//{
							intProduce = intMaxProduce;
							//}
						}

						#endregion

						decimal decLeadTime = Convert.ToDecimal(rowItemInfos["LTVariableTime"]);
						int intLoad = (int) (intProduce*decLeadTime) + intChangeTime;
						int intRemainCapacity = Convert.ToInt32(rowCapacity[indexCol]);

						// check remain group
						DataRow[] rowProductGroups = pdtbProductionGroup.Select("ProductID = " + intProductID);
						int intRemainGroupCapacity = int.MaxValue;

						if (rowProductGroups.Length > 0)
						{
							DataRow rowGroupCapacity = dtbGroupCapacityCopy.Select("ProductionGroupID = " + rowProductGroups[0]["ProductionGroupID"])[0];
							intRemainGroupCapacity = Convert.ToInt32(rowGroupCapacity["CapacityOfGroup"]);
						}

						if ((intProduce > 0) && (intProduce < intMinProduce)) intProduce = intMinProduce;

						int intMinRemainCapacity = ((intRemainGroupCapacity < intRemainCapacity) ? intRemainGroupCapacity : intRemainCapacity);
						int k = 0;
						if (intMultiple > 0)
						{
							while (intMinRemainCapacity < (int) (decLeadTime*(intProduce - k*intMultiple)) + intChangeTime)
							{
								k++;
							}
							intProduce = intProduce - k*intMultiple;
							if(intProduce < intMinProduce) intProduce = 0;
						}
						else // No multiple
						{
							if (intMinRemainCapacity < (int) (intProduce*decLeadTime) + intChangeTime)
								intProduce = (int) ((intMinRemainCapacity - intChangeTime)/decLeadTime);
						}

						//if(intMinRemainCapacity < (int)(intProduce * decLeadTime) + intChangeTime) intProduce = 0;
						if (intProduce > 0)
						{
							intLoad = (int) (intProduce*decLeadTime) + intChangeTime;
						}
						else
						{
							intProduce = 0;
							intLoad = 0;
						}

						// Neu Load > Capacity thi Produce(n)= ProduceC(n) voi ShiftID Null va Capacity(m,n)=Capacity(m-1,n)
						if (intLoad > intMinRemainCapacity) // intRemainCapacity
						{
							#region intLoad >= intMinRemainCapacity

							int intQuantity = (int) ((Convert.ToInt32(rowCapacity[indexCol]) - intChangeTime)/decLeadTime);
							if (intQuantity < intMinProduce)
							{
								intProduce = 0;
							}
							else
							{
								if (intMultiple > 0)
								{
									if (intQuantity%intMultiple > 0)
									{
										intProduce = intMultiple*(intQuantity/intMultiple + 1);
									}
								}
							}

							rowProduce[indexCol] = intProduce;
							if (intProduce > 0)
							{
								rowCapacity[indexCol] = Convert.ToInt32(rowCapacity[indexCol]) - ((int) (intProduce*decLeadTime) + intChangeTime);
								rowTotalSecond[indexCol] = /*Convert.ToInt32(rowTotalSecond[indexCol]) + */ ((int) (intProduce*decLeadTime) + intChangeTime);
							}
							DataRow rowOver = (DataRow) hashWC[rowOrder["ProductID"] + "-Over"];
							//dtbWC.Select("Plan = 'Over' AND ProductID = " + rowOrder["ProductID"])[0];
							// if intProduceC - intProduce > 0
							if (intDelivery - intStock - intOverQty - intProduce > 0)
								rowOver[indexCol] = Convert.ToInt32(rowOver[indexCol]) + intDelivery - intStock - intOverQty - intProduce;
							//else rowOver[indexCol] = 0;

							// update group capacity
							if ((rowProductGroups.Length > 0) && (intProduce > 0))
							{
								DataRow rowGroupCapacity = dtbGroupCapacityCopy.Select("ProductionGroupID = " + rowProductGroups[0]["ProductionGroupID"])[0];
								intRemainGroupCapacity = Convert.ToInt32(rowGroupCapacity["CapacityOfGroup"]);
								rowGroupCapacity["CapacityOfGroup"] = intRemainGroupCapacity - ((int) (intProduce*decLeadTime)) - intChangeTime;
							}

							#endregion
						}
						else // Neu Load < Capacity thi Produce(n)= ProduceUp(n) và Capacity(m,n)=Capacity(m-1)- Load(m,n)
						{
							DataRow[] rowProductInPairs = dtbWC.Select("Plan='Rate' AND ProductID = " + rowOrder["ProductID"] + " AND SetupPair > 0");
							//pdstProductPair.Tables[0].Select("ProductID = " + rowOrder["ProductID"]);
							if (rowProductInPairs.Length > 0)
							{
								#region // setup pair

								DataRow[] rowProductPairs = pdstProductPair.Tables[0].Select("WorkCenterID = " + dtbWC.TableName + " AND SetUpPair = " + rowProductInPairs[0]["SetupPair"]);
								decimal decTotalLeadTime = 0;
								//int intMultiple = Convert.ToInt32(rowItemInfos[0]["OrderQuantityMultiple"]);
								foreach (DataRow row in rowProductPairs)
								{
									arrProductFilled.Add(row["ProductID"]);
									DataRow rowItemPair = (DataRow) hashProductLists[row["ProductID"]]; //pdtbProductLists.Select("ProductID = " + row["ProductID"])[0];
									decTotalLeadTime += Convert.ToDecimal(rowItemPair["LTVariableTime"]);
									if (Convert.ToInt32(row["ProductID"]) == intProductID)
										arrPlannedProduct.Add(row["ProductID"]);
								}
								int k1 = 0;
								if (intMultiple > 0)
								{
									while (/*intRemainCapacity*/ intMinRemainCapacity < decTotalLeadTime*(intProduce - k1*intMultiple) + intChangeTime)
									{
										k1++;
									}
									intProduce = intProduce - k1*intMultiple;
									if(intProduce < intMinProduce) intProduce = 0;
								}
								else
								{
									if (/*intRemainCapacity*/intMinRemainCapacity < decTotalLeadTime*(intProduce) + intChangeTime)
									{
										intProduce = (int) ((/*intRemainCapacity*/intMinRemainCapacity - intChangeTime)/decTotalLeadTime);
									}
								}

									foreach (DataRow row in rowProductPairs)
									{
										DataRow rowProduceOK = (DataRow) hashWC[row["ProductID"] + "-Produce"];
										//dtbWC.Select("Plan = 'Produce' AND ProductID = " + row["ProductID"])[0];
										rowProduceOK[indexCol] = Convert.ToInt32(rowProduceOK[indexCol]) + intProduce;
										//stock
										DataRow rowDeliveryPair = (DataRow) hashWC[row["ProductID"] + "-Delivery"];
										//dtbWC.Select("Plan = 'Delivery' AND ProductID = " + row["ProductID"])[0];
										DataRow rowOverPair = (DataRow) hashWC[row["ProductID"] + "-Over"];
										//dtbWC.Select("Plan = 'Over' AND ProductID = " + row["ProductID"])[0];
										DataRow rowStockPair = (DataRow) hashWC[row["ProductID"] + "-Stock"];
										//dtbWC.Select("Plan = 'Stock' AND ProductID = " + row["ProductID"])[0];
										rowStockPair[indexCol] = Convert.ToInt32(rowStockPair[indexCol - 1]) + Convert.ToInt32(rowOverPair[indexCol - 1])
											- Convert.ToInt32(rowDeliveryPair[indexCol]) + Convert.ToInt32(rowProduceOK[indexCol]);

										if (Convert.ToInt32(rowStockPair[indexCol]) < 0)
											rowOverPair[indexCol] = -Convert.ToInt32(rowStockPair[indexCol]);

										DataRow rowTotalSecondPair = (DataRow) hashWC[row["ProductID"] + "-TotalSecond"];
										//dtbWC.Select("Plan = 'TotalSecond' AND ProductID = " + row["ProductID"])[0];
										decimal decLeadTimePair = Convert.ToDecimal(((DataRow) hashProductLists[row["ProductID"]])["LTVariableTime"]);
										//Convert.ToDecimal(pdtbProductLists.Select("ProductID = " + row["ProductID"])[0]["LTVariableTime"]);
										if (Convert.ToInt32(row["ProductID"]) == intProductID)
											rowTotalSecondPair[indexCol] = (int) (Convert.ToInt32(rowProduceOK[indexCol])*decLeadTimePair) + intChangeTime;
										else
											rowTotalSecondPair[indexCol] = (int) (Convert.ToInt32(rowProduceOK[indexCol])*decLeadTimePair);

										rowCapacity[indexCol] = Convert.ToInt32(rowCapacity[indexCol]) - Convert.ToInt32(rowTotalSecondPair[indexCol]);
									}


									// update group capacity
									if ((rowProductGroups.Length > 0) && (intProduce > 0))
									{
										DataRow rowGroupCapacity = dtbGroupCapacityCopy.Select("ProductionGroupID = " + rowProductGroups[0]["ProductionGroupID"])[0];
										intRemainGroupCapacity = Convert.ToInt32(rowGroupCapacity["CapacityOfGroup"]);
										rowGroupCapacity["CapacityOfGroup"] = intRemainGroupCapacity - (intProduce*decTotalLeadTime) - intChangeTime;
									}
//								}

								#endregion
							}
							else
							{
								#region // no pair

								rowProduce[indexCol] = intProduce;
								if (intProduce > 0)
								{
									rowCapacity[indexCol] = /*intRemainCapacity*/intMinRemainCapacity - ((int) (intProduce*decLeadTime) + intChangeTime);
									rowTotalSecond[indexCol] = /*Convert.ToInt32(rowTotalSecond[indexCol]) + */ ((int) (intProduce*decLeadTime) + intChangeTime);
								}

								// update group capacity
								if ((rowProductGroups.Length > 0) && (intProduce > 0))
								{
									DataRow rowGroupCapacity = dtbGroupCapacityCopy.Select("ProductionGroupID = " + rowProductGroups[0]["ProductionGroupID"])[0];
									intRemainGroupCapacity = Convert.ToInt32(rowGroupCapacity["CapacityOfGroup"]);
									rowGroupCapacity["CapacityOfGroup"] = intRemainGroupCapacity - ((int) (intProduce*decLeadTime)) - intChangeTime;
								}

								#endregion
							}
						}
						// StockQty(m,n) = StockQty(m,n-1)+OverQty(m,n-1) - DeliveryQty(m,n)+Produce(m,n)
						DataRow rowOver1 = (DataRow) hashWC[rowOrder["ProductID"] + "-Over"];
						//dtbWC.Select("Plan = 'Over' AND ProductID = " + rowOrder["ProductID"])[0];

						// Cong don
						//DataRow rowCongDon = dtbWC.Select("Plan = 'CongDon' AND ProductID = " + rowOrder["ProductID"])[0];

						intStock = Convert.ToInt32(rowStock[indexCol - 1]);
						intOverQty = Convert.ToInt32(rowOver1[indexCol - 1]);
						intProduce = Convert.ToInt32(rowProduce[indexCol]);
						rowStock[indexCol] = intStock + intOverQty - intDelivery + intProduce;

						if (Convert.ToInt32(rowStock[indexCol]) < 0)
						{
//							if(indexCol != intIndexOfBeginQty + 1)
//							{
							rowOver1[indexCol] = - (intStock + intOverQty - intDelivery + intProduce);
//							}
						}

						// va loi over bi am
						if (Convert.ToInt32(rowOver1[indexCol]) < 0)
						{
							rowOver1[indexCol] = 0;
						}
						else if (intStock + intOverQty + intProduce - intDelivery >= 0)
						{
//							if(indexCol != intIndexOfBeginQty + 1)
							rowOver1[indexCol] = 0;
						}
						if(blnSuaOver && intProduceC > 0)
						{
							if(intProduceC - intProduce > 0)
								rowOver1[indexCol] = intProduceC - intProduce;
							else
								rowOver1[indexCol] = 0;
						}

						rowCongDon[indexCol] = Convert.ToInt32(rowCongDon[indexCol - 1]) + Convert.ToInt32(rowProduce[indexCol])
							+ Convert.ToInt32(rowOver1[indexCol]);

						//Neu Cong don >= Sum( DeliveryQty) thi
						//Neu Cong don>= Tong Delivery va Stock(n-1)- Delivery(n)>=0 thi OverQty(m,n)=0

						if ((Convert.ToInt32(rowCongDon[indexCol]) > intTotalDelivery)
							&& (Convert.ToInt32(rowStock[indexCol - 1]) >= Convert.ToInt32(rowDelivery[indexCol])))
						{
							rowOver1[indexCol] = 0;
						}

						if (Convert.ToInt32(((DataRow) hashWC[rowOrder["ProductID"] + "-Produce"])[indexCol]) > 0)
						{
							intPrevProductID = Convert.ToInt32(rowOrder["ProductID"]);
							// add order produce
							AddOrderProduce(intPrevProductID,intProductID,indexCol,dtbOrderProduce,dtbWC,pdrowDCPOption,OrderNo);

						}
						
						arrPlannedProduct.Add(rowOrder["ProductID"]);

						SelectProductToPlanning(arrStack, arrProductSmallRate, pdtbChangeCategory, dtbWC,
						                        intProductID, rowOrder, indexCol, strColumName, arrPlannedProduct,hashWC,
												intPrevProductID,hashProductLists);
					}

					#endregion

					indexCol++;
					if (dtbWC.Columns.Count <= indexCol) break;
				}

				#endregion

				#region // Convert template result table into pdtbRequiredProducts table

				if (dtbWC != pdstResultStruct.Tables[0])
				{
					if((dtbWC.TableName == "403")||(dtbWC.TableName == "532")||(dtbWC.TableName == "64")
						|| (dtbWC.TableName == "106")||(dtbWC.TableName == "51")
						|| (dtbWC.TableName == "110")||(dtbWC.TableName == "94"))
					{
						DataSet dst = new DataSet();
						dst.Tables.Add(dtbWC.Copy());
						dst.WriteXml("c:\\wc" + dtbWC.TableName + ".xml");
					}

					bool blnOutside = false;
					if (pdtbWCOutside.Select("WorkCenterID = " + dtbWC.TableName).Length > 0)
					{
						blnOutside = true;
					}
					// scan all productid to get produce and over
					DateTime dtmStartOfFirstShift = dtmAsOfDate;
					foreach (DataRow rowProduce in dtbWC.Select("Plan = 'Produce'"))
					{
						double dblLTFixedTime = 0;
						if (blnOutside)
						{
							dblLTFixedTime = Convert.ToDouble(((DataRow) hashProductLists[rowProduce["ProductID"]])["LTFixedTime"]);
							//Convert.ToDouble(pdtbProductLists.Select("ProductID = " + rowProduce["ProductID"])[0]["LTFixedTime"]);
						}

						DataRow rowDelivery = (DataRow) hashWC[rowProduce["ProductID"] + "-Delivery"];

						indexCol = intIndexOfBeginQty + 1;
						// scan columns to update into database
						while (indexCol < dtbWC.Columns.Count)
						{
							#region ShiftID & Date

							int intYear = 0, intMonth = 0, intDay = 0;
							string strDate = dtbWC.Columns[indexCol].ColumnName;
							try
							{
								intYear = Convert.ToInt32(strDate.Substring(0, 4));
								intMonth = Convert.ToInt32(strDate.Substring(4, 2));
								intDay = Convert.ToInt32(strDate.Substring(6, 2));
							}
							catch
							{
							}

							DateTime dtmDate = new DateTime(intYear, intMonth, intDay);

							int intShiftID = 0;
							int intSeparator = dtbWC.Columns[indexCol].ColumnName.IndexOf('-');
							if (intSeparator > 0)
							{
								intShiftID = Convert.ToInt32(dtbWC.Columns[indexCol].ColumnName.Substring(intSeparator + 1, strDate.Length - intSeparator - 1));
							}
							DataRow rowShift = pdtbShiftTime.Select("WorkCenterID = " + dtbWC.TableName + " AND ShiftID = " + intShiftID + " AND WorkingDate = '" + dtmDate + "'")[0];

							if (indexCol == intIndexOfBeginQty + 1)
							{
								dtmStartOfFirstShift = (DateTime) rowShift["StartShiftTime"];
							}

							#endregion

							#region Produce and Over

							if (Convert.ToInt32(rowProduce[indexCol]) > 0)
							{
								DataRow newRow = pdtbRequiredProducts.NewRow();
								newRow["ProductID"] = rowProduce["ProductID"];

								newRow["ShiftID"] = intShiftID;
								newRow["StartTime"] = rowShift["StartShiftTime"];
								newRow["EndTime"] = rowShift["REALEndShiftTime"];
								if (blnOutside)
								{
									newRow["StartTime"] = ((DateTime) rowShift["StartShiftTime"]).AddSeconds(-dblLTFixedTime);
									// bo xung nghiep vu cho outside
									if((DateTime)newRow["StartTime"] < dtmStartOfFirstShift) newRow["StartTime"] = dtmStartOfFirstShift;

									newRow["EndTime"] = rowShift["StartShiftTime"];
								}

								newRow["WorkCenterID"] = dtbWC.TableName;
								newRow["WorkingDate"] = dtmDate;
								newRow["Quantity"] = rowProduce[indexCol];
								newRow["TotalSecond"] = Convert.ToInt32(((DataRow) hashWC[rowProduce["ProductID"] + "-TotalSecond"])[indexCol]);
								//Convert.ToInt32(dtbWC.Select("Plan = 'TotalSecond' AND ProductID = " + rowProduce["ProductID"])[0][indexCol]);
								newRow["RoutingID"] = ((DataRow) hashProductLists[rowProduce["ProductID"]])["RoutingID"]; //pdtbProductLists.Select("ProductID = " + rowProduce["ProductID"])[0]["RoutingID"];

								pdtbRequiredProducts.Rows.Add(newRow);
							}
							DataRow rowOver = (DataRow) hashWC[rowProduce["ProductID"] + "-Over"];
							//dtbWC.Select("Plan = 'Over' AND ProductID = " + rowProduce["ProductID"])[0];
							if (indexCol == intIndexOfBeginQty + 1)
							{
								rowOver[indexCol] = Convert.ToInt32(rowOver[indexCol]) + Convert.ToInt32(rowOver["OverCircle"]);
							}

							if (Convert.ToInt32(rowOver[indexCol]) > 0)
							{
								DataRow newRow = pdtbRequiredProducts.NewRow();
								newRow["ProductID"] = rowProduce["ProductID"];

								newRow["StartTime"] = //rowShift["StartShiftTime"];
									newRow["EndTime"] = //dtmAsOfDate.Date.AddMinutes(375);//(6*60 + 15) rowShift["REALEndShiftTime"];
										dtmStartOfFirstShift;
//								if(blnOutside)
//								{
//									newRow["StartTime"] = ((DateTime)rowShift["StartShiftTime"]).AddSeconds(-dblLTFixedTime);
//									newRow["EndTime"] = rowShift["StartShiftTime"];
//								}

								newRow["Quantity"] = rowOver[indexCol];
								newRow["TotalSecond"] = Convert.ToInt32(((DataRow) hashProductLists[rowOver["ProductID"]])["LTVariableTime"])
									*Convert.ToInt32(newRow["Quantity"]);

								newRow["WorkCenterID"] = dtbWC.TableName;
								newRow["WorkingDate"] = dtmStartOfFirstShift.Date; //dtmDate;
								newRow["RoutingID"] = ((DataRow) hashProductLists[rowProduce["ProductID"]])["RoutingID"];
								pdtbRequiredProducts.Rows.Add(newRow);
							}

							#endregion

							#region // Delivery

							if (Convert.ToInt32(rowDelivery[indexCol]) > 0)
							{
								DataRow newRow = pdtbDelivery.NewRow();
								newRow["ProductID"] = rowDelivery["ProductID"];
								newRow["DCOptionMasterID"] = Convert.ToInt32(pdrowDCPOption["DCOptionMasterID"]);
								//newRow["ShiftID"] = intShiftID;
								//DataRow rowShift = pdtbShiftTime.Select("WorkCenterID = " + dtbWC.TableName + " AND ShiftID = " + intShiftID + " AND WorkingDate = '" + dtmDate + "'")[0];
								newRow["StartDateTime"] = rowShift["StartShiftTime"];
								newRow["DueDateTime"] = rowShift["REALEndShiftTime"];
//								if(blnOutside)
//								{
//									newRow["StartTime"] = ((DateTime)rowShift["StartShiftTime"]).AddSeconds(-dblLTFixedTime);
//									newRow["EndTime"] = rowShift["StartShiftTime"];
//								}

								newRow["WorkCenterID"] = dtbWC.TableName;
								//newRow["WorkingDate"] = dtmDate;
								newRow["Quantity"] = 0;
								newRow["DeliveryQuantity"] = rowDelivery[indexCol];
								//newRow["TotalSecond"] = 0;
								newRow["RoutingID"] = ((DataRow) hashProductLists[rowProduce["ProductID"]])["RoutingID"];
								//pdtbProductLists.Select("ProductID = " + rowProduce["ProductID"])[0]["RoutingID"];

								pdtbDelivery.Rows.Add(newRow);
							}

							#endregion

							indexCol++;
							if (dtbWC.Columns.Count <= indexCol) break;
						}
					}
					// sua theo Note2:
					SuaTheoNote2(dtbOrderProduce, dtbWC, pdrowDCPOption,dtmStartOfFirstShift,pdtbRequiredProducts,hashProductLists);
				}

				#endregion

				// Convert delivery 
			}
			// Generate by Hours

			//DataSet dstOrderProduce = new DataSet();
			//dstOrderProduce.Tables.Add(dtbOrderProduce);
			//dstOrderProduce.WriteXml("C:\\dtbOrderProduce.xml");
			(new PRO_DCPResultMasterDS()).InsertDCPOrderProduce(dtbOrderProduce,Convert.ToInt32(pdrowDCPOption["DCOptionMasterID"]),strChildWCIDs);

			//pdtbProductLists.DataSet.WriteXml("C:\\pdtbProductLists.xml");
		}

		void AddOrderProduce(int intPrevProductID, int intProductID, int indexCol, DataTable dtbOrderProduce,
			DataTable dtbWC, DataRow pdrowDCPOption,int OrderNo)
		{
			//if(intPrevProductID > 0)
			{
				if((dtbWC.TableName == "403")||(dtbWC.TableName == "532")||(dtbWC.TableName == "64")
					|| (dtbWC.TableName == "106")||(dtbWC.TableName == "51")
					|| (dtbWC.TableName == "110")||(dtbWC.TableName == "94"))
				{
					if(dtbOrderProduce.Rows.Count > 0)
					{
						if(Convert.ToInt32(dtbOrderProduce.Rows[dtbOrderProduce.Rows.Count-1]["OrderPlan"]) != intProductID)
						{
							DataRow newRowOrder = dtbOrderProduce.NewRow();
							newRowOrder["WorkCenterID"] = dtbWC.TableName;
							newRowOrder["ColumnName"] = dtbWC.Columns[indexCol].ColumnName;
							newRowOrder["OrderNo"] = OrderNo;
							newRowOrder["OrderPlan"] = intProductID;
							newRowOrder["ShiftID"] = Convert.ToInt32(dtbWC.Columns[indexCol].ColumnName.Substring(9)); //yyyymmdd-shiftid
							newRowOrder["DCOptionMasterID"] = Convert.ToInt32(pdrowDCPOption["DCOptionMasterID"]);
								
							dtbOrderProduce.Rows.Add(newRowOrder);
						}
					}
					else
					{
						DataRow newRowOrder = dtbOrderProduce.NewRow();
						newRowOrder["WorkCenterID"] = dtbWC.TableName;
						newRowOrder["ColumnName"] = dtbWC.Columns[indexCol].ColumnName;
						newRowOrder["OrderNo"] = OrderNo;
						newRowOrder["OrderPlan"] = intProductID;
						newRowOrder["ShiftID"] = Convert.ToInt32(dtbWC.Columns[indexCol].ColumnName.Substring(9)); //yyyymmdd-shiftid
						newRowOrder["DCOptionMasterID"] = Convert.ToInt32(pdrowDCPOption["DCOptionMasterID"]);
						dtbOrderProduce.Rows.Add(newRowOrder);
					}
				}
			}	
		}

		void SuaTheoNote2(DataTable dtbOrderProduce, DataTable dtbWC, DataRow pdrowDCPOption, DateTime dtmStartOfFirstShift,DataTable pdtbRequiredProducts, Hashtable hashProductLists)
		{
			//Sau do tinh luong SafetyStockPro= SafetyStock- EndStock (SafetyStockPro phai> MinProduce, chia het cho Multiple, va nho hon MaxProduce, neu SafetyStock- EndStock<0 thi gan =0
			foreach(DataRow rowProduce in dtbWC.Select("Plan = 'Stock'"))
			{
				int intLastColumn = dtbWC.Columns.Count - 1;
				DataRow itemInfo = (DataRow) hashProductLists[rowProduce["ProductID"]]; 
				int intMinProduce = Convert.ToInt32(itemInfo["MinProduce"]);
				int intMultiple = Convert.ToInt32(itemInfo["OrderQuantityMultiple"]);
				int intMaxProduce = Convert.ToInt32(itemInfo["MaxProduce"]);
				int intSafetyStock = Convert.ToInt32(itemInfo["SafetyStock"]);
				if (Convert.ToInt32(pdrowDCPOption["SafetyStock"]) == 0)
				{
					intSafetyStock = 0;
				}
				int SafetyStockPro = intSafetyStock-Convert.ToInt32(rowProduce[intLastColumn]);
				if (SafetyStockPro > 0)
				{
					//if(SafetyStockPro < intMinProduce) SafetyStockPro = intMinProduce;
					//if(intMaxProduce > 0)
					//	if(SafetyStockPro > intMaxProduce) SafetyStockPro = intMaxProduce;
					//if(intMultiple > 0)
					//if(SafetyStockPro % intMultiple > 0)
					//	SafetyStockPro = ((SafetyStockPro/intMultiple) + 1)* intMultiple;

					DataRow newRow = pdtbRequiredProducts.NewRow();
					newRow["ProductID"] = rowProduce["ProductID"];

					newRow["StartTime"] = //rowShift["StartShiftTime"];
						newRow["EndTime"] = //dtmAsOfDate.Date.AddMinutes(375);//(6*60 + 15) rowShift["REALEndShiftTime"];
							dtmStartOfFirstShift;

					newRow["Quantity"] = SafetyStockPro;
					newRow["TotalSecond"] = Convert.ToInt32(((DataRow) hashProductLists[rowProduce["ProductID"]])["LTVariableTime"])
						*SafetyStockPro;

					newRow["WorkCenterID"] = dtbWC.TableName;
					newRow["WorkingDate"] = dtmStartOfFirstShift.Date; //dtmDate;
					newRow["RoutingID"] = ((DataRow) hashProductLists[rowProduce["ProductID"]])["RoutingID"];
					pdtbRequiredProducts.Rows.Add(newRow);
				}
			}
		}

		void SelectProductToPlanning(Stack arrStack, ArrayList arrProductSmallRate, DataTable pdtbChangeCategory, DataTable dtbWC,
		                             int intProductID, DataRow rowOrder, int indexCol, string strColumName, ArrayList arrPlannedProduct,
									Hashtable hashWC,int intPrevProductID, Hashtable hashProductLists)
		{
			#region // push next item which is min change category
			if(dtbWC.TableName == "532"){int bug=0;}
			if ((arrStack.Count == 0)) //bo doan nay Rate >= 1
			{
				// find next item which has similar SetupPair
				if(hashWC[intPrevProductID+"-ProduceC"] != null)
				{
					if(((DataRow)hashWC[intPrevProductID+"-ProduceC"])["Model"].ToString().Trim().ToUpper() != "")
					{
						DataRow[] rowSimilarModels = dtbWC.Select("Plan='ProduceC' AND ProductID <> "+intPrevProductID
							+" AND Model = '" + ((DataRow)hashWC[intPrevProductID+"-ProduceC"])["Model"].ToString().Trim() + "'");
						if(intProductID == 0) // for the first item of the shift
						{
							rowSimilarModels = dtbWC.Select("Plan='ProduceC' AND ProductID <> "+intProductID
								+" AND Model = '" + ((DataRow)hashWC[intPrevProductID+"-ProduceC"])["Model"].ToString().Trim() + "'");
						}
						foreach(DataRow rowSimilarModel in rowSimilarModels)
						{
							if(!arrPlannedProduct.Contains(rowSimilarModel["ProductID"]))
							{
								arrStack.Push(Convert.ToInt32(rowSimilarModel["ProductID"]));
								return;
							}
						}
					}
				}

				if (arrProductSmallRate.Count > 0) // Rate < 1
				{
					string strNotPlannedProductIDs = "(";
					for (int i = 0; i < arrProductSmallRate.Count; i++)
					{
						strNotPlannedProductIDs += arrProductSmallRate[i] + ",";
					}
					strNotPlannedProductIDs += "0)";

					#region // insert product was not planned and has no change time

					DataTable dtbSmallTempCategory = pdtbChangeCategory.Clone();
					dtbSmallTempCategory.Columns.Add("Rate", typeof (decimal));
					dtbSmallTempCategory.Columns.Add("ProduceC", typeof (decimal));
					dtbSmallTempCategory.Columns.Add("Model", typeof (string));
					dtbSmallTempCategory.Columns.Add("SetupPair", typeof (string));

					DataRow[] drowSmallNotPlanneds = dtbWC.Select("Plan='Rate' AND ProductID IN " + strNotPlannedProductIDs);
					foreach (DataRow rowNotPlan in drowSmallNotPlanneds)
					{
						if (pdtbChangeCategory.Select("WorkCenterID = " + dtbWC.TableName + " AND SourceProductID = " + intPrevProductID + " AND DestProductID = " + rowNotPlan["ProductID"]).Length == 0)
						{
							DataRow newRow = dtbSmallTempCategory.NewRow();
							newRow["WorkCenterID"] = Convert.ToInt32(dtbWC.TableName);
							newRow["SourceProductID"] = intPrevProductID;
							newRow["DestProductID"] = rowNotPlan["ProductID"];
							newRow["CHANGETIME"] = 0;
							newRow["Rate"] = ((DataRow)hashWC[rowNotPlan["ProductID"] +"-Rate"])[indexCol];
							newRow["ProduceC"] = ((DataRow)hashWC[rowNotPlan["ProductID"] +"-ProduceC"])[indexCol];
							newRow["Model"] = ((DataRow)hashWC[rowNotPlan["ProductID"] +"-ProduceC"])["Model"];
							newRow["SetupPair"] = ((DataRow)hashWC[rowNotPlan["ProductID"] +"-ProduceC"])["SetupPair"];
							dtbSmallTempCategory.Rows.Add(newRow);
						}
					}

					DataRow[] rowSmallMinChangeCategorys = pdtbChangeCategory.Select("WorkCenterID = " + dtbWC.TableName + " AND SourceProductID = " + intPrevProductID + " AND DestProductID IN " + strNotPlannedProductIDs);
					foreach (DataRow rowMin in rowSmallMinChangeCategorys)
					{
						DataRow newRow = dtbSmallTempCategory.NewRow();
						newRow["WorkCenterID"] = rowMin["WorkCenterID"];
						newRow["SourceProductID"] = rowMin["SourceProductID"];
						newRow["DestProductID"] = rowMin["DestProductID"];
						newRow["ChangeTime"] = rowMin["ChangeTime"];
						newRow["Rate"] = ((DataRow)hashWC[rowMin["DestProductID"] +"-Rate"])[indexCol];
						newRow["ProduceC"] = ((DataRow)hashWC[rowMin["DestProductID"] +"-ProduceC"])[indexCol];
						newRow["Model"] = ((DataRow)hashWC[rowMin["DestProductID"] +"-ProduceC"])["Model"];
						newRow["SetupPair"] = ((DataRow)hashWC[rowMin["DestProductID"] +"-ProduceC"])["SetupPair"];
						dtbSmallTempCategory.Rows.Add(newRow);
					}

					#endregion

					#region // get product which is min change or min rate Push into stack
					// [Model] ASC,

					DataRow[] rowSmallMinChangeCategoryOrRate = dtbSmallTempCategory.Select("WorkCenterID = " + dtbWC.TableName
						+ " AND SourceProductID = " + intPrevProductID,
						"[Rate] ASC,[ChangeTime] ASC, [ProduceC] DESC, [DestProductID] ASC");
					if (rowSmallMinChangeCategoryOrRate.Length > 0)
					{
						// find next item equal [Model] with prev product that Rate, ChangeTime are similar
						int i = 0;
						bool blnFound = false;
						while(true)
						{
							if(rowSmallMinChangeCategoryOrRate.Length == i) break;
							if((DataRow)hashWC[intPrevProductID+"-Rate"] != null)
							{
								if((Convert.ToDecimal(rowSmallMinChangeCategoryOrRate[i]["Rate"])
									== Convert.ToDecimal(rowSmallMinChangeCategoryOrRate[0]["Rate"]))
									&& 
									(Convert.ToDecimal(rowSmallMinChangeCategoryOrRate[i]["ChangeTime"])
										== Convert.ToDecimal(rowSmallMinChangeCategoryOrRate[0]["ChangeTime"]))
									&& 
									(rowSmallMinChangeCategoryOrRate[i]["Model"].ToString().Trim().ToUpper()
										== ((DataRow)hashWC[intPrevProductID+"-Rate"])["Model"].ToString().Trim().ToUpper())
									)
								{
									blnFound = true;
									break;
								}
							}
							i++;
						}
						if(blnFound)
							arrStack.Push(Convert.ToInt32(rowSmallMinChangeCategoryOrRate[i]["DestProductID"]));
						else // Not found
						{
							//rowSmallMinChangeCategoryOrRate = dtbSmallTempCategory.Select("WorkCenterID = " + dtbWC.TableName
							//	+ " AND SourceProductID = " + intPrevProductID,
							//	"[Rate] ASC,[ChangeTime] ASC, [ProduceC] DESC, [DestProductID] ASC");
							arrStack.Push(Convert.ToInt32(rowSmallMinChangeCategoryOrRate[0]["DestProductID"]));
						}
					}

					#endregion
				}
				else // Rate >= 1
				{
					#region Create template table

					string strPlannedProductIDs = "(";
					for (int i = 0; i < arrPlannedProduct.Count; i++)
					{
						strPlannedProductIDs += arrPlannedProduct[i] + ",";
					}
					strPlannedProductIDs += "0)";

					#region // insert product was not planned and has no change time

					#endregion

					DataTable dtbTempCategory = pdtbChangeCategory.Clone();
					dtbTempCategory.Columns.Add("Rate", typeof (decimal));

					DataRow[] drowNotPlanneds = dtbWC.Select("Plan='Rate' AND ProductID NOT IN " + strPlannedProductIDs);
					foreach (DataRow rowNotPlan in drowNotPlanneds)
					{
						if (pdtbChangeCategory.Select("WorkCenterID = " + dtbWC.TableName + " AND SourceProductID = " + intPrevProductID + " AND DestProductID = " + rowNotPlan["ProductID"]).Length == 0)
						{
							DataRow newRow = dtbTempCategory.NewRow();
							newRow["WorkCenterID"] = Convert.ToInt32(dtbWC.TableName);
							newRow["SourceProductID"] = intPrevProductID;
							newRow["DestProductID"] = rowNotPlan["ProductID"];
							newRow["CHANGETIME"] = 0;
							newRow["Rate"] = rowNotPlan[indexCol];
							dtbTempCategory.Rows.Add(newRow);
						}
					}

					DataRow[] rowMinChangeCategorys = pdtbChangeCategory.Select("WorkCenterID = " + dtbWC.TableName + " AND SourceProductID = " + intPrevProductID + " AND DestProductID NOT IN " + strPlannedProductIDs, "CHANGETIME ASC, DestProductID ASC");
					foreach (DataRow rowMin in rowMinChangeCategorys)
					{
						DataRow newRow = dtbTempCategory.NewRow();
						newRow["WorkCenterID"] = rowMin["WorkCenterID"];
						newRow["SourceProductID"] = rowMin["SourceProductID"];
						newRow["DestProductID"] = rowMin["DestProductID"];
						newRow["ChangeTime"] = rowMin["ChangeTime"];
						newRow["Rate"] = ((DataRow)hashWC[rowMin["DestProductID"] +"-Rate"])[indexCol];
						dtbTempCategory.Rows.Add(newRow);
					}

					#endregion

					#region // get product which is min change or min rate Push into stack

					DataRow[] rowMinChangeCategoryOrRate = dtbTempCategory.Select("", "ChangeTime ASC, Rate ASC, DestProductID ASC");
					if (rowMinChangeCategoryOrRate.Length > 0)
					{
						arrStack.Push(Convert.ToInt32(rowMinChangeCategoryOrRate[0]["DestProductID"]));
					}
					else
					{
						DataRow[] rowNotPlanProductOrderRate = dtbWC.Select("Plan = 'Rate' AND (ProductID NOT IN " + strPlannedProductIDs 
							+ ") AND ProductID <> " + intProductID 
							+ " AND [" + strColumName + "] >= 1",
						    "[" + strColumName + "] ASC, ProductID ASC");
						if (rowNotPlanProductOrderRate.Length > 0)
						{
							arrStack.Push(Convert.ToInt32(rowNotPlanProductOrderRate[0]["ProductID"]));
						}
					}

					#endregion
				}
			}

			#endregion
		}


		DataSet CreateResultStruct(DataTable pdtbWorkCenter, DataTable pdtbWCConfig, DataRow pdrowDCPOption,DataTable dtbWCList)
		{
			DataSet dstResult = new DataSet();
			DateTime dtmAsOfDate = (DateTime) pdrowDCPOption[PRO_DCOptionMasterTable.ASOFDATE_FLD];
			DateTime dtmEndOfCycle = dtmAsOfDate.AddDays(Convert.ToDouble(pdrowDCPOption[PRO_DCOptionMasterTable.PLANHORIZON_FLD]));
			foreach (DataRow rowWC in pdtbWorkCenter.Rows)
			{
				if (dstResult.Tables.Contains(rowWC["WorkCenterID"].ToString())) continue;

				dstResult.Tables.Add(rowWC["WorkCenterID"].ToString());

				dstResult.Tables[rowWC["WorkCenterID"].ToString()].Columns.Add("SetupPair", typeof (int));
				dstResult.Tables[rowWC["WorkCenterID"].ToString()].Columns["SetupPair"].DefaultValue = 0;

				dstResult.Tables[rowWC["WorkCenterID"].ToString()].Columns.Add("Model", typeof (string));
				dstResult.Tables[rowWC["WorkCenterID"].ToString()].Columns["Model"].DefaultValue = string.Empty;

				dstResult.Tables[rowWC["WorkCenterID"].ToString()].Columns.Add("ProductID", typeof (int));
				//dstResult.Tables[rowWC["WorkCenterID"].ToString()].Columns.Add("Rate",typeof(decimal));
				dstResult.Tables[rowWC["WorkCenterID"].ToString()].Columns.Add("Plan"); //= {ProduceC, Produce, Stock, Capacity, Delivery}

				dstResult.Tables[rowWC["WorkCenterID"].ToString()].Columns.Add("OverCircle", typeof (int));
				dstResult.Tables[rowWC["WorkCenterID"].ToString()].Columns["OverCircle"].DefaultValue = 0;

				// fix position
				dstResult.Tables[rowWC["WorkCenterID"].ToString()].Columns.Add("BeginQuantity", typeof (int));
				dstResult.Tables[rowWC["WorkCenterID"].ToString()].Columns["BeginQuantity"].DefaultValue = 0;

				DataRow rowWCOffset = dtbWCList.Select("WorkCenterID = " + rowWC["WorkCenterID"])[0];
				dtmAsOfDate = (DateTime) rowWCOffset["PlanningStartDate"];
				//dtmAsOfDate =(DateTime) pdrowDCPOption[PRO_DCOptionMasterTable.ASOFDATE_FLD];

				while (dtmAsOfDate <= dtmEndOfCycle)
				{
					DataRow[] rowConfigs = pdtbWCConfig.Select("WorkCenterID=" + rowWC["WorkCenterID"] + " AND BeginDate <= '" + dtmAsOfDate + "' AND EndDate >= '" + dtmAsOfDate + "'", "WorkTimeFrom ASC");
					if (rowConfigs.Length > 0)
					{
						//dstResult.Tables[rowWC["WorkCenterID"].ToString()].Columns.Add(dtmAsOfDate.ToString("yyyyMMdd"),typeof(int)); //column no shift
						foreach (DataRow rowShift in rowConfigs)
						{
							dstResult.Tables[rowWC["WorkCenterID"].ToString()].Columns.Add(dtmAsOfDate.ToString("yyyyMMdd") + "-" + rowShift["ShiftID"], typeof (decimal));
							dstResult.Tables[rowWC["WorkCenterID"].ToString()].Columns[dtmAsOfDate.ToString("yyyyMMdd") + "-" + rowShift["ShiftID"]].DefaultValue = 0;
						}
					}
					dtmAsOfDate = dtmAsOfDate.AddDays(1);
				}
			}
			return dstResult;
		}


		/// <summary>
		/// 
		/// 1. Set up Delivery
		/// </summary>
		/// <param name="pdstResultStruct"></param>
		/// <param name="pdtbAvailQty"></param>
		void InsertExistedDataIntoResultStruct(DataSet pdstResultStruct, DataTable pdtbAvailQty, DataTable pdtbShiftTime, DataTable pdtbWCConfig, DataRow pdrowDCPOption, DataTable pdtbProductLists, DataSet pdstDCPData, DataTable pdtbProductPair, DataTable pdtbWCOutside,
		                                       DataTable pdtbBeginData, DataRow drowDCOptionMaster)
		{
			
			foreach (DataTable dtbWC in pdstResultStruct.Tables)
			{
				bool blnOutSide = false;
				if (pdtbWCOutside.Select("WorkCenterID = " + dtbWC.TableName).Length > 0) blnOutSide = true;

				#region Fill all WC Capacity on first row

				if (true)
				{
					DataRow newCapacityRow = dtbWC.NewRow();
					newCapacityRow["ProductID"] = 0;
					newCapacityRow["Plan"] = "Capacity";
					// Fill total capacity by shift pdtbWCConfig
					DateTime dtmAsOfDate, dtmEndOfCycle; // = 
					dtmAsOfDate = (DateTime) pdrowDCPOption[PRO_DCOptionMasterTable.ASOFDATE_FLD];
					dtmEndOfCycle = dtmAsOfDate.AddDays(Convert.ToDouble(pdrowDCPOption[PRO_DCOptionMasterTable.PLANHORIZON_FLD]));
					while (dtmAsOfDate <= dtmEndOfCycle)
					{
						DataRow[] rowConfigs = pdtbWCConfig.Select("WorkCenterID = " + dtbWC.TableName + " AND BeginDate <= '" + dtmAsOfDate + "' AND EndDate >= '" + dtmAsOfDate + "'", "WorkTimeFrom ASC");
						if (rowConfigs.Length > 0)
						{
							foreach (DataRow row in rowConfigs)
							{
								if (dtbWC.Columns.Contains(dtmAsOfDate.ToString("yyyyMMdd") + "-" + row["ShiftID"]))
								{
									if (blnOutSide)
									{
										newCapacityRow[dtmAsOfDate.ToString("yyyyMMdd") + "-" + row["ShiftID"]] = int.MaxValue;
									}
									else
									{
										newCapacityRow[dtmAsOfDate.ToString("yyyyMMdd") + "-" + row["ShiftID"]] = row["ShiftCapacity"];
									}
								}
							}
						}
						dtmAsOfDate = dtmAsOfDate.AddDays(1);
					}
					dtbWC.Rows.Add(newCapacityRow);
				}

				#endregion

				#region // Insert ProductID from pdtbProductLists

				foreach (DataRow row in pdtbProductLists.Select("WorkCenterID=" + dtbWC.TableName))
				{
					if (dtbWC.Select("ProductID=" + row["ProductID"]).Length == 0)
					{
						DataRow[] rowPairs = pdtbProductPair.Select("ProductID = " + row["ProductID"]);
						int intSetupPair = 0;
						if (rowPairs.Length > 0)
						{
							intSetupPair = Convert.ToInt32(rowPairs[0]["SetupPair"]);
						}
						// Delivery
						DataRow newRow = dtbWC.NewRow();
						newRow["Model"] = row["Revision"];
						newRow["ProductID"] = row["ProductID"];
						newRow["Plan"] = "Delivery";
						newRow["SetupPair"] = intSetupPair;
						dtbWC.Rows.Add(newRow);
						// ProduceC
						newRow = dtbWC.NewRow();
						newRow["Model"] = row["Revision"];
						newRow["ProductID"] = row["ProductID"];
						newRow["Plan"] = "ProduceC";
						newRow["SetupPair"] = intSetupPair;
						dtbWC.Rows.Add(newRow);
						// Produce
						newRow = dtbWC.NewRow();
						newRow["Model"] = row["Revision"];
						newRow["ProductID"] = row["ProductID"];
						newRow["Plan"] = "Produce";
						newRow["SetupPair"] = intSetupPair;
						dtbWC.Rows.Add(newRow);
						// Stock
						newRow = dtbWC.NewRow();
						newRow["Model"] = row["Revision"];
						newRow["ProductID"] = row["ProductID"];
						newRow["Plan"] = "Stock";
						newRow["SetupPair"] = intSetupPair;
						int intAvailQty = 0; // Begin stock
						if(Convert.ToInt32(drowDCOptionMaster["UseCacheAsBegin"]) == 0)
						{
							if (pdtbBeginData.Select("ProductID=" + row["ProductID"]).Length > 0)
							{
								intAvailQty = Convert.ToInt32(pdtbBeginData.Select("ProductID=" + row["ProductID"])[0]["Quantity"]);
							}
						}
						else
						{
							if (pdtbAvailQty.Select("ProductID=" + row["ProductID"]).Length > 0)
							{
								intAvailQty = Convert.ToInt32(pdtbAvailQty.Select("ProductID=" + row["ProductID"])[0]["AvailQuantity"]);
							}
						}
						newRow["BeginQuantity"] = intAvailQty;
						dtbWC.Rows.Add(newRow);
						// CongDon
						newRow = dtbWC.NewRow();
						newRow["Model"] = row["Revision"];
						newRow["ProductID"] = row["ProductID"];
						newRow["Plan"] = "CongDon";
						newRow["SetupPair"] = intSetupPair;
						if (Convert.ToInt32(pdrowDCPOption["SafetyStock"]) == 1)
							newRow["BeginQuantity"] = intAvailQty; // - Convert.ToInt32(row["SafetyStock"]);
						else
							newRow["BeginQuantity"] = intAvailQty;
						dtbWC.Rows.Add(newRow);
						// Over
						newRow = dtbWC.NewRow();
						newRow["Model"] = row["Revision"];
						newRow["ProductID"] = row["ProductID"];
						newRow["Plan"] = "Over";
						newRow["SetupPair"] = intSetupPair;
						dtbWC.Rows.Add(newRow);
						// Total second
						newRow = dtbWC.NewRow();
						newRow["Model"] = row["Revision"];
						newRow["ProductID"] = row["ProductID"];
						newRow["Plan"] = "TotalSecond";
						newRow["SetupPair"] = intSetupPair;
						dtbWC.Rows.Add(newRow);
						// rate
						newRow = dtbWC.NewRow();
						newRow["Model"] = row["Revision"];
						newRow["ProductID"] = row["ProductID"];
						newRow["Plan"] = "Rate";
						newRow["SetupPair"] = intSetupPair;
						dtbWC.Rows.Add(newRow);
					}
				}

				#endregion
			}

			#region DEL// Setup produce and begin stock for the first workcenter

//			if(true)
//			{
//				DataTable dtbWC = pdstResultStruct.Tables[0];
//				DateTime dtmAsOfDate, dtmEndOfCycle;// = 
//				dtmAsOfDate = (DateTime)pdrowDCPOption[PRO_DCOptionMasterTable.ASOFDATE_FLD];
//				dtmEndOfCycle = dtmAsOfDate.AddDays(Convert.ToDouble(pdrowDCPOption[PRO_DCOptionMasterTable.PLANHORIZON_FLD]));
//
//				foreach(DataRow rowDel in pdstDCPData.Tables[0].Rows) 
//				{
//					DateTime dtmWorkingDate = (DateTime) rowDel["WorkingDate"];
//					string strColName = dtmWorkingDate.ToString("yyyyMMdd");
//					string strShiftID = rowDel["ShiftID"].ToString();
//					if(!dtbWC.Columns.Contains(strColName + "-" + strShiftID)) continue;
//
//					DataRow rowProduce = dtbWC.Select("ProductID = " + rowDel["ProductID"] + " AND Plan='Produce'")[0];
//					DataRow rowStock = dtbWC.Select("ProductID = " + rowDel["ProductID"] + " AND Plan='Stock'")[0];
//					// Set produce quantity
//					rowProduce[strColName+"-"+strShiftID] = Convert.ToDecimal(rowProduce[strColName+"-"+strShiftID])
//						+ Convert.ToDecimal(rowDel["Quantity"]);
//
//					// Begin stock
//					int intAvailQty = 0;
//					if(pdtbBeginData.Select("ProductID="+rowDel["ProductID"]).Length > 0 )
//					{
//						intAvailQty = Convert.ToInt32(pdtbBeginData.Select("ProductID="+rowDel["ProductID"])[0]["AvailQuantity"]);
//					}
//					// rowStocks[0][dtbWC.Columns.IndexOf("BeginQuantity") + 1] = 
//					rowStock["BeginQuantity"] = intAvailQty;
//				}
//			}

			#endregion
		}


		DataTable CreateShiftTime(DataRow pdrowDCOptionMaster, DataTable pdtbWCConfig)
		{
			DataTable dtbShiftTime = new DataTable();
			dtbShiftTime.Columns.Add("WorkCenterID", typeof (int));
			dtbShiftTime.Columns.Add("ShiftID", typeof (int));
			dtbShiftTime.Columns.Add("WorkingDate", typeof (DateTime));
			dtbShiftTime.Columns.Add("StartShiftTime", typeof (DateTime));
			dtbShiftTime.Columns.Add("EndShiftTime", typeof (DateTime));
			dtbShiftTime.Columns.Add("REALEndShiftTime", typeof (DateTime));

			DateTime dtmAsOfDate = ((DateTime) pdrowDCOptionMaster["AsOfDate"]).AddDays(-31);
			DateTime dtmEndOfCycle = /*dtmAsOfDate*/
				((DateTime) pdrowDCOptionMaster["AsOfDate"]).AddDays(Convert.ToDouble(pdrowDCOptionMaster[PRO_DCOptionMasterTable.PLANHORIZON_FLD]));
			while (dtmAsOfDate <= dtmEndOfCycle)
			{
				DataRow[] rowConfigs = pdtbWCConfig.Select("BeginDate <= '" + dtmAsOfDate + "' AND EndDate >= '" + dtmAsOfDate + "'",
				                                           "WorkTimeFrom ASC");
				if (rowConfigs.Length > 0)
				{
					foreach (DataRow row in rowConfigs)
					{
						DataRow newShiftRow = dtbShiftTime.NewRow();
						DateTime dtmWorkTimeFrom = (DateTime) row["WorkTimeFrom"];
						DateTime dtmWorkTimeTo = (DateTime) row["WorkTimeTo"];
						newShiftRow["WorkCenterID"] = row["WorkCenterID"];
						newShiftRow["ShiftID"] = row["ShiftID"];
						newShiftRow["WorkingDate"] = dtmAsOfDate;
						newShiftRow["StartShiftTime"] = dtmWorkTimeFrom.AddDays(dtmAsOfDate.Subtract(dtmWorkTimeFrom).Days + 1);
						newShiftRow["REALEndShiftTime"] =
							newShiftRow["EndShiftTime"] = dtmWorkTimeTo.AddDays(dtmAsOfDate.Subtract(dtmWorkTimeTo).Days + 1);
						dtbShiftTime.Rows.Add(newShiftRow);
					}
				}
				dtmAsOfDate = dtmAsOfDate.AddDays(1);
			}
			return dtbShiftTime;
		}


		void UpdateDataIntoDataBase(DataTable pdtbRequiredProduct, DataTable pdtbShiftTime, DataRow pdrowDCOptionMaster, int pintDCOptionMasterID, string pstrChildWorkCenterIDs, int pintWorkCenterID, DataSet pdstResultStruct, DataTable pdtbIgnoreList,Hashtable hashProductLists)
		{
			PRO_DCOptionMasterDS dsMaster = new PRO_DCOptionMasterDS();
			dsMaster.DeleteDCPInChildWC(pintDCOptionMasterID, pstrChildWorkCenterIDs);
			//DataSet dstData = dsMaster.GetDCPData(0, pintDCOptionMasterID, true);
			PRO_DCPResultMasterDS dsResult = new PRO_DCPResultMasterDS();	
			DataTable dtbDCPResultMaster = dsResult.GetTableStruct();
			int intID = 0;
			foreach (DataTable dtbWC in pdstResultStruct.Tables)
			{
				if (Convert.ToInt32(dtbWC.TableName) == pintWorkCenterID) continue;
				if (pdtbIgnoreList.Select("WorkCenterID = " + dtbWC.TableName).Length > 0) continue;
				DataRow[] rowProduceInWCs = pdtbRequiredProduct.Select("WorkCenterID = " + dtbWC.TableName, "StartTime ASC", DataViewRowState.Added);
				foreach (DataRow row in rowProduceInWCs)
				{
					if (Convert.ToInt32(row[PRO_DCPResultMasterTable.WORKCENTERID_FLD]) == pintWorkCenterID) continue;
					if (Convert.ToDecimal(row[PRO_DCPResultMasterTable.QUANTITY_FLD]) == 0) continue;

					DataRow newRow = dtbDCPResultMaster.NewRow();
					newRow["DCOptionMasterID"] = pintDCOptionMasterID;
					newRow["ProductID"] = Convert.ToInt32(row[PRO_DCPResultMasterTable.PRODUCTID_FLD]);
					newRow["WorkCenterID"] = Convert.ToInt32(dtbWC.TableName); //Convert.ToInt32(row[PRO_DCPResultMasterTable.WORKCENTERID_FLD]);
					newRow["Quantity"] = Convert.ToDecimal(row[PRO_DCPResultMasterTable.QUANTITY_FLD]);
					newRow["StartDateTime"] = (DateTime) row["StartTime"];
					try
					{
						newRow["DueDateTime"] = (DateTime) row["EndTime"];
					}
					catch
					{
						newRow["DueDateTime"] = newRow["StartDateTime"];
					}
//					try
//					{
//						newRow["RoutingID"] = Convert.ToInt32(row["RoutingID"]);
//					}
//					catch
					{
						newRow["RoutingID"] = ((DataRow) hashProductLists[row["ProductID"]])["RoutingID"];
					}
					// for detail
					newRow["MasterShiftID"] = row["ShiftID"];
					newRow["MasterTotalSecond"] = (row["TotalSecond"] == DBNull.Value ? 0 : row["TotalSecond"]);
					dtbDCPResultMaster.Rows.Add(newRow);
				}
			}
			dsResult.UpdateDataTable(dtbDCPResultMaster);
			dsResult.InsertDCPResultDetail(pintDCOptionMasterID,pstrChildWorkCenterIDs);
			//(new PRO_DCPResultDetailDS()).UpdateDataSet(dstData);

		}

		void UpdateEndShiftTime(DataSet pdstWC, DataTable pdtbShiftTime)
		{
			foreach (DataTable dtbWC in pdstWC.Tables)
			{
				DataRow[] rowShifts = pdtbShiftTime.Select("WorkCenterID = " + dtbWC.TableName, "StartShiftTime ASC");
				if (rowShifts.Length > 0)
				{
					for (int i = 1; i < rowShifts.Length; i++)
					{
						rowShifts[i - 1]["EndShiftTime"] = rowShifts[i]["StartShiftTime"];
					}
				}
			}
		}


		void UpdateDelivery(DataTable pdtbDelivery, int pintDCOptionMasterID)
		{
			PRO_DCPResultMasterDS dsResMaster = new PRO_DCPResultMasterDS();
			dsResMaster.UpdateDataTable(pdtbDelivery);

		}

		void GetBeginQuantity(DataRow pdrowDCOptionMaster, DataSet pdstResultStruct, DataTable pdtbBOM, DataTable pdtbDelSch,
		                      DataTable pdtbBeginData, DataTable pdtbAvailQty, DataTable pdtbProductLists, DataTable pdtbWCList,
								DataTable pdtbWCOutside)
		{
			bool blnIncludeCheckpoint = Convert.ToBoolean(pdrowDCOptionMaster[PRO_DCOptionMasterTable.INCLUDECHECKPOINT_FLD]);
			bool blnOnhand = Convert.ToBoolean(pdrowDCOptionMaster[PRO_DCOptionMasterTable.ONHAND_FLD]);
			bool blnSafetyStock = Convert.ToBoolean(pdrowDCOptionMaster[PRO_DCOptionMasterTable.SAFETYSTOCK_FLD]);
			int intDCOptionMasterID = Convert.ToInt32(pdrowDCOptionMaster[PRO_DCOptionMasterTable.DCOPTIONMASTERID_FLD]);
			int intCCNID = Convert.ToInt32(pdrowDCOptionMaster[PRO_DCOptionMasterTable.CCNID_FLD]);
			DateTime dtmAsOfDate = Convert.ToDateTime(pdrowDCOptionMaster[MTR_MPSCycleOptionMasterTable.ASOFDATE_FLD]);
			int intPlanHorizon = Convert.ToInt32(pdrowDCOptionMaster[MTR_MPSCycleOptionMasterTable.PLANHORIZON_FLD]);
			DateTime dtmCurrentDay = dtmAsOfDate;

			DataSet dstFuturePOs = new DataSet();
			DataSet dstFutureSupplyWOs = new DataSet();
			DataSet dstFutureDemandWOs = new DataSet();
			DataSet dstFutureSOs = new DataSet();

			DateTime dtmDBDate = (new PCSComUtils.Common.BO.UtilsBO()).GetDBDate(); //.Date;
			PCSComMaterials.Plan.BO.MPSRegenerationProcessBO boMPSRegeneration = new PCSComMaterials.Plan.BO.MPSRegenerationProcessBO();

			foreach (DataTable dtbWC in pdstResultStruct.Tables)
			{
				if (dtbWC == pdstResultStruct.Tables[0]) continue;
				DateTime dtmPlanningStartDate = new DateTime();
				bool blnOutside = false;
				if(pdtbWCOutside.Select("WorkCenterID="+dtbWC.TableName).Length > 0)
				{
					blnOutside = true;
				}

				if (!blnOutside)
				{
					try
					{
						//calculate actual AsOfDate
						DataRow drowWC = pdtbWCList.Select("WorkCenterID = " + dtbWC.TableName)[0];
						dtmPlanningStartDate = Convert.ToDateTime(drowWC[PRO_PlanningOffsetTable.PLANNINGSTARTDATE_FLD]);
					}
					catch
					{
						dtmPlanningStartDate = dtmAsOfDate;
					}
				}
				else
				{
					dtmPlanningStartDate = dtmAsOfDate;
				}

				DataTable dtbFutureSOs = new DataTable();
				DataTable dtbFuturePOs = new DataTable();
				DataTable dtbFutureSupplyWOs = new DataTable();
				DataTable dtbFutureDemandWOs = new DataTable();
				GetDemandAndSupply(intCCNID, dtmDBDate, dtmPlanningStartDate, ref dtbFutureSOs, ref dtbFuturePOs, 
					ref dtbFutureSupplyWOs, ref dtbFutureDemandWOs, boMPSRegeneration, Convert.ToInt32(dtbWC.TableName));

				if (dstFuturePOs.Tables[dtbWC.TableName] == null)
				{
					dtbFuturePOs.TableName = dtbWC.TableName;
					dstFuturePOs.Tables.Add(dtbFuturePOs);
				}
				else
				{
					1.ToString();
				}
				if (dstFutureSOs.Tables[dtbWC.TableName] == null)
				{
					dtbFutureSOs.TableName = dtbWC.TableName;
					dstFutureSOs.Tables.Add(dtbFutureSOs);
				}
				else
				{
					1.ToString();
				}
				if (dstFutureSupplyWOs.Tables[dtbWC.TableName] == null)
				{
					dtbFutureSupplyWOs.TableName = dtbWC.TableName;
					dstFutureSupplyWOs.Tables.Add(dtbFutureSupplyWOs);
				}
				else
				{
					1.ToString();
				}
				if (dstFutureDemandWOs.Tables[dtbWC.TableName] == null)
				{
					dtbFutureDemandWOs.TableName = dtbWC.TableName;
					dstFutureDemandWOs.Tables.Add(dtbFutureDemandWOs);
				}
				else
				{
					1.ToString();
				}

				//#endregion Preparing

				//adjust delivery quantity base on inventory

				#region Onhand-Produce adjust

				DataRow[] arrDeliveries; // = pdtbDelSch.Select(MST_WorkCenterTable.WORKCENTERID_FLD + "=" + intWorkCenterID);
				DataRow[] arrProducts = pdtbProductLists.Select("WorkCenterID = " + dtbWC.TableName);
				//dtbWC.Select("Plan = 'Rate'");
				//foreach product
				if (blnOnhand)
				{
					foreach (DataRow drowProduct in arrProducts)
					{
						//dtbFutureSOs = new DataTable();
						//dtbFuturePOs = new DataTable();
						//dtbFutureSupplyWOs = new DataTable();

						int intProductID = Convert.ToInt32(drowProduct[ITM_ProductTable.PRODUCTID_FLD]);

						//GetDemandAndSupply(intCCNID,dtmDBDate,dtmAsOfDate,ref dtbFutureSOs,ref dtbFuturePOs,ref dtbFutureSupplyWOs,boMPSRegeneration);
						System.Diagnostics.Debug.WriteLine(dtmPlanningStartDate);
						dtbFuturePOs = dstFuturePOs.Tables[dtbWC.TableName];
						dtbFutureSupplyWOs = dstFutureSupplyWOs.Tables[dtbWC.TableName];
						dtbFutureDemandWOs = dstFutureDemandWOs.Tables[dtbWC.TableName];
						dtbFutureSOs = dstFutureSOs.Tables[dtbWC.TableName];

						//TODO : change from asofdate to planning startdate
						bool blnUseCacheAsBegin = Convert.ToBoolean(pdrowDCOptionMaster[PRO_DCOptionMasterTable.USECACHE_ASBEGIN_FLD]);
						decimal decStock = GetOnHandAtAsOfDate(intProductID, dtbFutureSOs, dtbFuturePOs, dtbFutureSupplyWOs, dtbFutureDemandWOs, true, pdtbAvailQty, dtmPlanningStartDate, blnUseCacheAsBegin);

						//first of all, subtract for over quantity
						//select all parent
						DataRow[] arrParent = pdtbBOM.Select(ITM_BOMTable.COMPONENTID_FLD + " = " + intProductID.ToString());
						decimal decTotalParentOverQuantity = 0;
						foreach (DataRow drowParent in arrParent)
						{
							int intParentProductID = Convert.ToInt32(drowParent[ITM_ProductTable.PRODUCTID_FLD]);
							decimal decShrink = Convert.ToDecimal(drowParent[ITM_BOMTable.SHRINK_FLD]);
							decimal decBOMQty = Convert.ToDecimal(drowParent[ITM_BOMTable.QUANTITY_FLD]);
							//get over quantity
							object objResult = pdtbDelSch.Compute("SUM(" + SO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD + ")", ITM_ProductTable.PRODUCTID_FLD + " = " + intParentProductID.ToString() + " AND " + CAPACITYBOTTLEID_FLD + " < 0");
							decimal decParentOverQuantity = Convert.ToDecimal(objResult == DBNull.Value ? 0 : objResult);
							//drowChildProduct[ITM_BOMTable.QUANTITY_FLD]) * decCurrentQuantity / (1-decShrink/100)
							decTotalParentOverQuantity += decParentOverQuantity*(1 - decShrink/100)/decBOMQty;
						}
						decStock -= decTotalParentOverQuantity;
						if (decStock < 0)
						{
							decStock = 0;
						}

						#region dungla 25-09-2006: save begin data for other purpose

						DataRow[] drowExist = pdtbBeginData.Select(ITM_ProductTable.PRODUCTID_FLD + "=" + intProductID
							+ " AND " + DCP_BeginQuantityTable.DCOPTIONMASTERID_FLD + "=" + pdrowDCOptionMaster[DCP_BeginQuantityTable.DCOPTIONMASTERID_FLD].ToString());
						// already in database, just need to update only
						if (drowExist != null && drowExist.Length > 0)
							drowExist[0][DCP_BeginQuantityTable.QUANTITY_FLD] = decStock;
						else
						{
							// make new record
							DataRow drowBeginData = pdtbBeginData.NewRow();
							drowBeginData[DCP_BeginQuantityTable.DCOPTIONMASTERID_FLD] = pdrowDCOptionMaster[DCP_BeginQuantityTable.DCOPTIONMASTERID_FLD];
							drowBeginData[DCP_BeginQuantityTable.PRODUCTID_FLD] = drowProduct[ITM_ProductTable.PRODUCTID_FLD];
							drowBeginData[DCP_BeginQuantityTable.QUANTITY_FLD] = decStock;
							pdtbBeginData.Rows.Add(drowBeginData);
						}

						#endregion

						//forward day by day
						dtmCurrentDay = dtmPlanningStartDate; //dtmAsOfDate;
						for (int intIdx = 0; intIdx <= intPlanHorizon + (dtmPlanningStartDate.Subtract(dtmAsOfDate)).TotalDays; intIdx++)
						{
							//get all deliveries
							arrDeliveries = pdtbDelSch.Select(SO_SaleOrderDetailTable.PRODUCTID_FLD + "=" + intProductID + " AND " + MST_WorkCenterTable.WORKCENTERID_FLD + "=" + dtbWC.TableName + " AND " + SO_DeliveryScheduleTable.SCHEDULEDATE_FLD + ">='" + dtmCurrentDay + "' AND " + SO_DeliveryScheduleTable.SCHEDULEDATE_FLD + "<='" + dtmCurrentDay.AddDays(1) + "'");

							decimal decTotalSupply = 0; //decTotalPurchase + decTotalProduce;
							decStock += decTotalSupply;
							if (decStock > 0)
							{
								//reduce delivery
								foreach (DataRow drowDelivery in arrDeliveries)
								{
									decimal decDeliveryQty = Convert.ToDecimal(drowDelivery[SO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD]);
									//decimal decDeliveryQty = decimal.One;

									if (decDeliveryQty < decStock)
									{
										drowDelivery[SO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD] = 0;
										//drowDelivery.Delete();
										decStock -= decDeliveryQty;
									}
									else
									{
										drowDelivery[SO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD] = decDeliveryQty - decStock;
										decStock = 0;
										break;
									}
								}
							}
							else
							{
								decStock = 0;
								break;
							}
							dtmCurrentDay = dtmCurrentDay.AddDays(1);
						}
						decimal decSafetyStock = Convert.ToDecimal(drowProduct[ITM_ProductTable.SAFETYSTOCK_FLD]);
						if (decStock > decSafetyStock)
						{
							drowProduct[ITM_ProductTable.SAFETYSTOCK_FLD] = 0;
						}
						else
						{
							drowProduct[ITM_ProductTable.SAFETYSTOCK_FLD] = decSafetyStock - decStock;
						}
					}
				}

				#endregion Onhand-Produce Adjust
			}
		}

		void ModifyPlanningOffset(DataTable dtbNewWCList, DataRow drowDCOptionMaster, DataTable pdtbBOM)
		{
			foreach(DataRow rowNewWCList in dtbNewWCList.Select("",WORKCENTERLEVEL_FLD + " ASC"))
			{
				double dblOffset = Convert.ToDouble(rowNewWCList["Offset"])*8;
				if(Convert.ToInt32(rowNewWCList[WORKCENTERLEVEL_FLD]) == 0)
				{
					if(rowNewWCList["PlanningStartDate"] == DBNull.Value)
						rowNewWCList["PlanningStartDate"] = ((DateTime)drowDCOptionMaster["AsOfDate"]).AddHours(-dblOffset);
				}
				else
				{
					DateTime dtmMinPlanningStartDate = DateTime.MaxValue;
					// Find min of PlanningStartDate
					int ParentLevel = Convert.ToInt32(rowNewWCList[WORKCENTERLEVEL_FLD]) - 1;
					foreach(DataRow rowParent in dtbNewWCList.Select(WORKCENTERLEVEL_FLD + "=" + ParentLevel))
					{
						DataRow[] arrChildWC = pdtbBOM.Select("ParentWCID = " + rowParent["WorkCenterID"] + " AND WorkCenterID = " + rowNewWCList["WorkCenterID"]);
						if(arrChildWC.Length > 0)
						{
							if(dtmMinPlanningStartDate > (DateTime)rowParent["PlanningStartDate"])
							{
								dtmMinPlanningStartDate = (DateTime)rowParent["PlanningStartDate"];
							}
						}
					}
					// Update planning start date
					if(dtmMinPlanningStartDate > ((DateTime)drowDCOptionMaster["AsOfDate"]))
						dtmMinPlanningStartDate = ((DateTime)drowDCOptionMaster["AsOfDate"]);
					rowNewWCList["PlanningStartDate"] = dtmMinPlanningStartDate.AddHours(-dblOffset);
				}
			}
		}

		void UpdatePlanningStartDate(DataTable dtbNewWCList, int pintDCOptionMasterID)
		{
			PRO_DCOptionMasterDS dsDCOptionMaster = new PRO_DCOptionMasterDS();
			DataTable dtbPlanningStartDate = dsDCOptionMaster.SelectPlanningStartDate(pintDCOptionMasterID);
            foreach(DataRow rowNewWCList in dtbNewWCList.Rows)
            {
				if(Convert.ToInt32(rowNewWCList["WorkCenterLevel"]) > 0)
				{
					DataRow rowPlanning;
					if(dtbPlanningStartDate.Select("WorkCenterID = " + rowNewWCList["WorkCenterID"]).Length > 0)
					{
						rowPlanning = dtbPlanningStartDate.Select("WorkCenterID=" + rowNewWCList["WorkCenterID"])[0];
						rowPlanning["PlanningStartDate"] = rowNewWCList["PlanningStartDate"];
					}
					else
					{
						rowPlanning = dtbPlanningStartDate.NewRow();
						rowPlanning["WorkCenterID"] = rowNewWCList["WorkCenterID"];
						rowPlanning["PlanningStartDate"] = rowNewWCList["PlanningStartDate"];
						rowPlanning["Offset"] = 0;
					}
				}
            }
			dsDCOptionMaster.UpdatePlanningStartDate(dtbPlanningStartDate);
		}

		#endregion Manual Production Planning
	}

	
}