using System;
using System.Collections;
using System.Data;



//
using PCSComMaterials.Plan.DS;

using PCSComUtils.Common;
using PCSComProduction.DCP.DS;
using PCSComUtils.PCSExc;

namespace PCSComProduction.DCP.BO
{
	public interface IDCOptionsBO
	{
		int AddDCOption(object pobjMaster, DataSet pdtsDetail);
		object GetMasterVO(int pintMasterID);
		void UpdateDCOption(object pobjMaster, DataSet pdtsDetail);
		void DeleteDCOption(int pintMasterID);
		void DeleteDCPResults(int pintDCOptionMasterID);
		bool CheckUniqueVersion(DateTime pdtmPlanningPeriod, int pintVersion);
		void UpdateDataSetForDCP(DataSet dstData, string pstrMasterIDToDelete, ArrayList parrMasterIDToDelete, int pintDCOptionMasterID);
		void UpdateDataSetAfterSaving(DataSet pdstData);
	}

	/// <summary>
	/// Summary description for DCOptionsBO.
	/// </summary>	
	
	
	public class DCOptionsBO : IDCOptionsBO
	{
		public DCOptionsBO()
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
	
		public int AddDCOption(object pobjMaster, DataSet pdtsDetail)
		{
			try
			{				
				PRO_DCOptionMasterDS dsMaster = new PRO_DCOptionMasterDS();
				int intMasterId = dsMaster.AddAndReturnID(pobjMaster);
				
				//reset master id of voObject
				((PRO_DCOptionMasterVO)pobjMaster).DCOptionMasterID = intMasterId;

//				if(pdtsDetail != null)
//				{
//					foreach(DataRow row in pdtsDetail.Tables[0].Rows)
//					{
//						if(row.RowState == DataRowState.Deleted) continue;
//						row[PRO_DCOptionMasterTable.DCOPTIONMASTERID_FLD] = intMasterId;
//					}
//
//					PRO_DCOptionDetailDS dsDetail = new PRO_DCOptionDetailDS();
//					
//					//Check dataset to call UpdateDataSet method
//					dsDetail.UpdateDataSet(pdtsDetail);					
//				}

				return intMasterId;
			}
			catch(PCSDBException ex)
			{
				throw ex;
			}
			catch(Exception ex)
			{
				throw new Exception(ex.Message, ex);
			}
		}

		/// <summary>
		/// Delete record by condition
		/// </summary>
	
		public void Delete(object pObjectVO)
		{
			return;			
		}
		
		/// <summary>
		/// Delete record by condition
		/// </summary>
	
		public void DeleteDCOption(int pintMasterID)
		{
			try
			{
				//First, delete data on detail table
				PRO_DCOptionDetailDS dsDetail = new PRO_DCOptionDetailDS();
				PRO_DCOptionMasterDS dsMaster = new PRO_DCOptionMasterDS();

				DataSet dtsDetail = dsDetail.GetDetailByMaster(pintMasterID);
				foreach(DataRow drow in dtsDetail.Tables[0].Rows)
				{
					drow.Delete();
				}
				dsDetail.UpdateDataSet(dtsDetail);

				//Second, select all DCPResult
				dsMaster.DeleteRelatedInforOfDCOption(pintMasterID);
				
				//Then delete data on master table
				dsMaster.Delete(pintMasterID);
			}
			catch(PCSDBException ex)
			{
				throw ex;
			}
			catch(Exception ex)
			{
				throw new Exception(ex.Message, ex);
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="pintDCOptionMasterID"></param>
	
		public void DeleteDCPResults(int pintDCOptionMasterID)
		{
			PRO_DCPResultMasterDS dsDCPResultMaster = new PRO_DCPResultMasterDS();
			dsDCPResultMaster.DeleteOldResult(pintDCOptionMasterID);
		}

		/// <summary>
		/// Get the object information by ID of VO class
		/// </summary>
	
		public object GetObjectVO(int pintID, string VOclass)
		{
			return null;
		}

	
		public object GetMasterVO(int pintMasterID)
		{
			try
			{				
				return (new PRO_DCOptionMasterDS()).GetObjectVO(pintMasterID);
				}
			catch (PCSDBException ex)
			{
				throw ex;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message, ex);
			}
		}

	
		public object GetMasterVO(string pstrCycle)
		{
			try
			{				
				return (new PRO_DCOptionMasterDS()).GetObjectVO(pstrCycle);
			}
			catch (PCSDBException ex)
			{
				throw ex;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message, ex);
			}
		}

		/// <summary>
		/// UpdateDataSetAfterSaving
		/// </summary>
		/// <param name="pdstData"></param>
		/// <author>Trada</author>
		/// <date>Saturday, June 10 2006</date>
	
		public void UpdateDataSetAfterSaving(DataSet pdstData)
		{
			PRO_DCPResultDetailDS dsDCPResultDetail = new PRO_DCPResultDetailDS();
            dsDCPResultDetail.UpdateDataSetManual(pdstData);
		}
		/// <summary>
		/// Return the DataSet (list of record) by inputing the FieldList and Condition
		/// </summary>
	
		public void UpdateDataSet(DataSet pdstData)
		{
			try
			{
				PRO_DCOptionDetailDS dsDCOption = new PRO_DCOptionDetailDS();
				dsDCOption.UpdateDataSet(pdstData);
			}
			catch (PCSDBException ex)
			{
				throw ex;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message, ex);
			}
		}

		/// <summary>
		/// Update into Database
		/// </summary>
	
		public void Update(object pObjectDetail)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// GetDetailByMaster into Database
		/// </summary>
	
		public DataSet GetDetailByMaster(int pintMasterId)
		{
			try
			{
				return (new PRO_DCOptionDetailDS()).GetDetailByMaster(pintMasterId);
			}
			catch (PCSDBException ex)
			{
				throw ex;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message, ex);
			}
		}

		/// <summary>
		/// GetDetailByMaster into Database
		/// </summary>
	
		public DataRow GetDCOptionMaster(int pintMasterId)
		{
			try
			{
				return (new PRO_DCOptionMasterDS()).GetDCOptionMaster(pintMasterId);
			}
			catch (PCSDBException ex)
			{
				throw ex;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message, ex);
			}
		}

		/// <summary>
		/// CheckUniqueVersion
		/// </summary>
		/// <param name="pdtmPlanningPeriod"></param>
		/// <param name="pintVersion"></param>
		/// <returns></returns>
		/// <author>Trada</author>
		/// <date>Friday, Mar 17 2006</date>
	
		public bool CheckUniqueVersion(DateTime pdtmPlanningPeriod, int pintVersion)
		{
			PRO_DCOptionMasterDS dsPRO_DCOptionMaster = new PRO_DCOptionMasterDS();
			if (dsPRO_DCOptionMaster.CheckUniqueVersion(pdtmPlanningPeriod, pintVersion) != 0)
				return true;
			else
				return false;
		}
		/// <summary>
		/// Update outside processing
		/// </summary>
		/// <param name="pobjMaster"></param>
		/// <param name="pdtsDetail"></param>
	
		public void UpdateDCOption(object pobjMaster, DataSet pdtsDetail)
		{
//			try
//			{
				PRO_DCOptionMasterDS dsMaster = new PRO_DCOptionMasterDS();
				dsMaster.Update(pobjMaster);
				
//				//check and update master id for new added rows
//				if(pdtsDetail != null)
//				{
//					foreach (DataRow row in pdtsDetail.Tables[0].Rows)
//					{
//						if(row.RowState == DataRowState.Deleted) continue;
//						row[PRO_DCOptionDetailTable.DCOPTIONMASTERID_FLD] = ((PRO_DCOptionMasterVO)pobjMaster).DCOptionMasterID;
//					}
//				}
//				PRO_DCOptionDetailDS dsDetail = new PRO_DCOptionDetailDS();
//				//Check dataset to call UpdateDataSet method
//				dsDetail.UpdateDataSet(pdtsDetail);				
//			}
//			catch (PCSDBException ex)
//			{
//				throw ex;
//			}
//			catch (Exception ex)
//			{
//				throw new Exception(ex.Message, ex);
//			}
		}

		#region BO functions for CPO Data Viewer form
		/// <summary>
		/// UpdateDataSetForDCP
		/// </summary>
		/// <param name="dstData"></param>
		/// <param name="pstrMasterIDToUpdate"></param>
		/// <param name="parrMasterIDToUpdate"></param>
		/// <author>Trada</author>
		/// <date>Monday, April 24 2006</date>
	
		public void UpdateDataSetForDCP(DataSet dstData, string pstrMasterIDToUpdate, ArrayList parrMasterIDToUpdate, int pintDCOptionMasterID)
		{
			//Get ShiftPattern for refer information
			PRO_ShiftPatternDS dsShiftPattern = new PRO_ShiftPatternDS();
			DataSet dstShift = dsShiftPattern.List();
			PRO_DCPResultDetailDS dsDCPResultDetail = new PRO_DCPResultDetailDS();
			PRO_DCPResultMasterDS dsDCPResultMaster = new PRO_DCPResultMasterDS();
			MTR_CPODS dsCPO = new MTR_CPODS();
			//build a new dataset for new rows
			#region Clone dataset contain new rows
			DataSet dstNewDataSet = new DataSet();
			DataSet dstModifiedDataSet = new DataSet();
			dstNewDataSet = dstData.Clone();
			dstModifiedDataSet = dstData.Clone();
			foreach (DataRow drow in dstData.Tables[0].Rows)
			{
				if (drow.RowState == DataRowState.Added)
				{
					DataRow drowNew = dstNewDataSet.Tables[0].NewRow();
					for (int i = 0; i < dstData.Tables[0].Columns.Count; i++)
					{
						drowNew[i] = drow[i];
					}
					dstNewDataSet.Tables[0].Rows.Add(drowNew);
				}
				if (drow.RowState == DataRowState.Modified)
				{
					DataRow drowNew = dstModifiedDataSet.Tables[0].NewRow();
					for (int i = 0; i < dstData.Tables[0].Columns.Count; i++)
					{
						drowNew[i] = drow[i];
					}
					dstModifiedDataSet.Tables[0].Rows.Add(drowNew);
				}
			}
			#endregion
			//update data
			#region sort new dataset by productid and start time
			string strOrderby = ITM_ProductTable.PRODUCTID_FLD + "," + PRO_DCPResultDetailTable.STARTTIME_FLD;
			DataRow[] adrowNewDataSet = dstNewDataSet.Tables[0].Select(string.Empty, strOrderby);
			
			if (adrowNewDataSet.Length > 0)
			{
				#region Calculate and update Master Table
                int i = 0;
				while (i < adrowNewDataSet.Length)
				{
					int j = i;
					decimal decQuantity = decimal.Parse(adrowNewDataSet[0][PRO_DCPResultDetailTable.QUANTITY_FLD].ToString());
					DateTime dtmStartDateTime = new DateTime();
					dtmStartDateTime = (DateTime) adrowNewDataSet[i][MTR_CPOTable.STARTDATE_FLD]; 
					while ((j < adrowNewDataSet.Length - 1) &&(int.Parse(adrowNewDataSet[j][ITM_ProductTable.PRODUCTID_FLD].ToString()) == int.Parse(adrowNewDataSet[j + 1][ITM_ProductTable.PRODUCTID_FLD].ToString())))
					{
						decQuantity += decimal.Parse(adrowNewDataSet[j + 1][PRO_DCPResultDetailTable.QUANTITY_FLD].ToString());
						j = j + 1;				
					}
					//assign value to VO
					PRO_DCPResultMasterVO voDCPResultMaster = new PRO_DCPResultMasterVO();
					//voDCPResultMaster.DCOptionMasterID = intDCOptionMasterID;
					voDCPResultMaster.DCOptionMasterID = pintDCOptionMasterID;
					voDCPResultMaster.Quantity = decQuantity;
					voDCPResultMaster.StartDateTime = dtmStartDateTime;
					voDCPResultMaster.ProductID = int.Parse(adrowNewDataSet[i][ITM_ProductTable.PRODUCTID_FLD].ToString());
					voDCPResultMaster.DueDateTime = (DateTime) adrowNewDataSet[j][MTR_CPOTable.DUEDATE_FLD];
					voDCPResultMaster.RoutingID = int.Parse(adrowNewDataSet[i][ITM_RoutingTable.ROUTINGID_FLD].ToString());
					voDCPResultMaster.WorkCenterID = int.Parse(adrowNewDataSet[i][MST_WorkCenterTable.WORKCENTERID_FLD].ToString());
					//insert to database
					//insert to master table
					
					int intDCPResultMasterID = dsDCPResultMaster.AddAndReturnID(voDCPResultMaster);
					foreach (DataRow drowDetail in dstNewDataSet.Tables[0].Rows)
					{
						//set value of Converted column
						drowDetail[PRO_DCPResultDetailTable.WOCONVERTED_FLD] = false; 
						//Set value of Start Time and End Time
						drowDetail[PRO_DCPResultDetailTable.STARTTIME_FLD] = drowDetail[MTR_CPOTable.STARTDATE_FLD];
						drowDetail[PRO_DCPResultDetailTable.ENDTIME_FLD] = drowDetail[MTR_CPOTable.DUEDATE_FLD];
						//set value of Working date to detail table
						DataRow[] adrowShift = dstShift.Tables[0].Select(PRO_ShiftPatternTable.SHIFTID_FLD + " = " + drowDetail[PRO_DCPResultDetailTable.SHIFTID_FLD].ToString());
						if (adrowShift.Length > 0)
						{
							DateTime dtmWorkingDate = new DateTime();
							dtmWorkingDate = GetDateOnly((DateTime)drowDetail[MTR_CPOTable.STARTDATE_FLD], (DateTime)adrowShift[0][PRO_ShiftPatternTable.WORKTIMEFROM_FLD], (DateTime)adrowShift[0][PRO_ShiftPatternTable.WORKTIMETO_FLD]);
							drowDetail[PRO_DCPResultDetailTable.WORKINGDATE_FLD] = GetDateOnly(dtmWorkingDate);
						}
						//set value of totalsecond to detail table
						//drowDetail[PRO_DCPResultDetailTable.TOTALSECOND_FLD] = GetTotalSecond(int.Parse(drowDetail[PRO_DCPResultDetailTable.SHIFTID_FLD].ToString()), dstShift, (DateTime)drowDetail[PRO_DCPResultDetailTable.STARTTIME_FLD], (DateTime)drowDetail[PRO_DCPResultDetailTable.ENDTIME_FLD]);
						//set value of percentage to detail table
						drowDetail[PRO_DCPResultDetailTable.PERCENTAGE_FLD] = 100 * Decimal.Parse(drowDetail[PRO_DCPResultDetailTable.QUANTITY_FLD].ToString())/decQuantity;
						//set value of MasterID to Detail Table
						if (int.Parse(drowDetail[ITM_ProductTable.PRODUCTID_FLD].ToString()) == int.Parse(adrowNewDataSet[i][ITM_ProductTable.PRODUCTID_FLD].ToString()))
						{
							drowDetail[PRO_DCPResultMasterTable.DCPRESULTMASTERID_FLD] = intDCPResultMasterID;
						}
					}
					i = j + 1;
				}
				#endregion
				//update detail to database
				
				dsDCPResultDetail.UpdateDataSetManual(dstNewDataSet);
			}
			#endregion
			//Remove new rows
			//delete row has status is add new 
			#region delete row has status is add new 
			DataRow[] adrowToDelete = dstData.Tables[0].Select(string.Empty, string.Empty, DataViewRowState.Added);
			for(int i =0; i < adrowToDelete.Length; i++)
			{					
				adrowToDelete[i].Delete();
			}
			#endregion
			foreach (DataRow drow in dstModifiedDataSet.Tables[0].Rows)
			{
				Int64 intDCPResultMasterToEdit = 0;
				if (drow[PRO_DCPResultMasterTable.DCPRESULTMASTERID_FLD].ToString() != string.Empty)
				{
					intDCPResultMasterToEdit = Convert.ToInt64(drow[PRO_DCPResultDetailTable.DCPRESULTMASTERID_FLD]);
				}
				#region sort Modified dataset by productid and start time
				string strOrderbyCond = PRO_DCPResultDetailTable.DCPRESULTMASTERID_FLD + "," + PRO_DCPResultDetailTable.STARTTIME_FLD;
				DataRow[] adrowModifiedDataSet = dstData.Tables[0].Select(PRO_DCPResultDetailTable.DCPRESULTMASTERID_FLD + " = " + intDCPResultMasterToEdit.ToString(), strOrderbyCond);			
				if (adrowModifiedDataSet.Length > 0)
				{
					#region Calculate and update Master Table
					int i = 0;
					while (i < adrowModifiedDataSet.Length)
					{
						int j = i;
						decimal decQuantity = decimal.Parse(adrowModifiedDataSet[0][PRO_DCPResultDetailTable.QUANTITY_FLD].ToString());
						DateTime dtmStartDateTime = new DateTime();
						dtmStartDateTime = (DateTime) adrowModifiedDataSet[i][MTR_CPOTable.STARTDATE_FLD]; 
						while ((j < adrowModifiedDataSet.Length - 1) &&(int.Parse(adrowModifiedDataSet[j][PRO_DCPResultDetailTable.DCPRESULTMASTERID_FLD].ToString()) == int.Parse(adrowModifiedDataSet[j + 1][PRO_DCPResultDetailTable.DCPRESULTMASTERID_FLD].ToString())))
						{
							decQuantity += decimal.Parse(adrowModifiedDataSet[j + 1][PRO_DCPResultDetailTable.QUANTITY_FLD].ToString());
							j = j + 1;				
						}
						//assign value to VO
						PRO_DCPResultMasterVO voDCPResultMaster = new PRO_DCPResultMasterVO();
						//voDCPResultMaster.DCOptionMasterID = intDCOptionMasterID;
						//voDCPResultMaster.DCOptionMasterID = pintDCOptionMasterID;
						voDCPResultMaster.DCPResultMasterID = int.Parse(adrowModifiedDataSet[i][PRO_DCPResultDetailTable.DCPRESULTMASTERID_FLD].ToString());
						voDCPResultMaster.Quantity = decQuantity;
						voDCPResultMaster.StartDateTime = dtmStartDateTime;
						//voDCPResultMaster.ProductID = int.Parse(adrowNewDataSet[i][ITM_ProductTable.PRODUCTID_FLD].ToString());
						voDCPResultMaster.DueDateTime = (DateTime) adrowModifiedDataSet[j][MTR_CPOTable.DUEDATE_FLD];
						//insert to database
						//insert to master table
					
						dsDCPResultMaster.Update(voDCPResultMaster);
						foreach (DataRow drowDetail in adrowModifiedDataSet)
						{
							//set value of Converted column
							//drowDetail[PRO_DCPResultDetailTable.WOCONVERTED_FLD] = false; 
							//Set value of Start Time and End Time
							drowDetail[PRO_DCPResultDetailTable.STARTTIME_FLD] = drowDetail[MTR_CPOTable.STARTDATE_FLD];
							drowDetail[PRO_DCPResultDetailTable.ENDTIME_FLD] = drowDetail[MTR_CPOTable.DUEDATE_FLD];
							//set value of Working date to detail table
							DataRow[] adrowShift = dstShift.Tables[0].Select(PRO_ShiftPatternTable.SHIFTID_FLD + " = " + drowDetail[PRO_DCPResultDetailTable.SHIFTID_FLD].ToString());
							if (adrowShift.Length > 0)
							{
								DateTime dtmWorkingDate = new DateTime();
								dtmWorkingDate = GetDateOnly((DateTime)drowDetail[MTR_CPOTable.STARTDATE_FLD], (DateTime)adrowShift[0][PRO_ShiftPatternTable.WORKTIMEFROM_FLD], (DateTime)adrowShift[0][PRO_ShiftPatternTable.WORKTIMETO_FLD]);
								drowDetail[PRO_DCPResultDetailTable.WORKINGDATE_FLD] = GetDateOnly(dtmWorkingDate);
							}
							//set value of totalsecond to detail table
							//drowDetail[PRO_DCPResultDetailTable.TOTALSECOND_FLD] = GetTotalSecond(int.Parse(drowDetail[PRO_DCPResultDetailTable.SHIFTID_FLD].ToString()), dstShift, (DateTime)drowDetail[PRO_DCPResultDetailTable.STARTTIME_FLD], (DateTime)drowDetail[PRO_DCPResultDetailTable.ENDTIME_FLD]);
							//set value of percentage to detail table
							drowDetail[PRO_DCPResultDetailTable.PERCENTAGE_FLD] = 100 * Decimal.Parse(drowDetail[PRO_DCPResultDetailTable.QUANTITY_FLD].ToString())/decQuantity;
							
						}
						i = j + 1;
					}
					#endregion
					//update detail to database
				
					dsDCPResultDetail.UpdateDataSetManual(dstData);
				}
				#endregion
			}
			
			#region Update DataSet (old code)
			dsCPO.UpdateDataSetForDCP(dstData);
			#endregion

			#region Delete master Table for delete multi-rows event
			//Update Master Table 
			//First, we get detail and master table from database for re-calculating master information
			if (parrMasterIDToUpdate.Count > 0)
			{
				string strMasterIDToDelete = "0";
				DataSet dstDCPResultDetailAfterSaving = new DataSet();
				DataSet dstDCPResultMaster = new DataSet();
				//Master Table
				dstDCPResultMaster = dsDCPResultMaster.GetDCPResultMasterByArrayMasterID(pstrMasterIDToUpdate);
				//Detail table
				dstDCPResultDetailAfterSaving = dsDCPResultDetail.GetDCPResultDetailByMasterID(pstrMasterIDToUpdate);
				if (dstDCPResultDetailAfterSaving.Tables[0].Rows.Count > 0)
				{
					if (parrMasterIDToUpdate.Count > 0)
					{
						for (int i = 0; i < parrMasterIDToUpdate.Count; i++)
						{
							//Select Detail for each masterID
							DataRow[] adrowDetailByMasterID = dstDCPResultDetailAfterSaving.Tables[0].Select(PRO_DCPResultDetailTable.DCPRESULTMASTERID_FLD + " = " + parrMasterIDToUpdate[i].ToString(), PRO_DCPResultDetailTable.STARTTIME_FLD);
							//Select Master for each masterID
							DataRow[] adrowMasterByMasterID = dstDCPResultMaster.Tables[0].Select(PRO_DCPResultMasterTable.DCPRESULTMASTERID_FLD + " = " + parrMasterIDToUpdate[i].ToString());
							if (adrowDetailByMasterID.Length > 0) //Update Master
							{
								if (adrowMasterByMasterID.Length > 0)
								{
									decimal decQuantityMaster = 0;
									for (int j = 0; j < adrowDetailByMasterID.Length; j++)
									{
										if ((adrowDetailByMasterID[j][PRO_DCPResultDetailTable.QUANTITY_FLD] != null)
											&& (adrowDetailByMasterID[j][PRO_DCPResultDetailTable.QUANTITY_FLD] != DBNull.Value))
										{
											decQuantityMaster += decimal.Parse(adrowDetailByMasterID[j][PRO_DCPResultDetailTable.QUANTITY_FLD].ToString());
										} 
									}
									adrowMasterByMasterID[0][PRO_DCPResultMasterTable.QUANTITY_FLD] = decQuantityMaster;
									adrowMasterByMasterID[0][PRO_DCPResultMasterTable.STARTDATETIME_FLD] = (DateTime)adrowDetailByMasterID[0][PRO_DCPResultDetailTable.STARTTIME_FLD]; 
									adrowMasterByMasterID[0][PRO_DCPResultMasterTable.DUEDATETIME_FLD] = (DateTime)adrowDetailByMasterID[adrowDetailByMasterID.Length - 1][PRO_DCPResultDetailTable.ENDTIME_FLD]; 
								}
							}
							else
							{
								//Save MasterID to Delete
								strMasterIDToDelete += "," + parrMasterIDToUpdate[i].ToString();
							}
						}
					}
					//Update Master 
					dsDCPResultMaster.UpdateDataSetDeleteMultiRows(dstDCPResultMaster);
					//Delete Master
					dsDCPResultMaster.DeleteMultiRows(strMasterIDToDelete);	
				}
				else
				{
					//Delete Master
					dsDCPResultMaster.DeleteMultiRows(pstrMasterIDToUpdate);	
				}
				
			}
			#endregion

		}
		/// <summary>
		/// GetDateOnly
		/// </summary>
		/// <param name="pdtmInputDate"></param>
		/// <returns></returns>
		/// <author>Trada</author>
		/// <date>Thursday, April 27 2006</date>
		private DateTime GetDateOnly(DateTime pdtmInputDate)
		{
			DateTime dtmOutputDate = new DateTime(pdtmInputDate.Year, pdtmInputDate.Month, pdtmInputDate.Day);
			return dtmOutputDate;
		}
		/// <summary>
		/// GetTotalSecond
		/// </summary>
		/// <param name="pintShiftID"></param>
		/// <param name="pdstShift"></param>
		/// <returns></returns>
		/// <author>Trada</author>
		/// <date>Monday, April 24 2006</date>
		private decimal GetTotalSecond(int pintShiftID, DataSet pdstShift, DateTime pdtmStartTime, DateTime pdtmEndTime)
		{
			DataRow[] adrowShift = pdstShift.Tables[0].Select(PRO_ShiftPatternTable.SHIFTID_FLD + " = " + pintShiftID.ToString());
			decimal decTotalSecond = 0;
			if (adrowShift.Length  > 0)
			{
				TimeSpan tsTotalTime = new TimeSpan();
				TimeSpan tsRegularStop = new TimeSpan();
				TimeSpan tsRefreshing = new TimeSpan();
				TimeSpan tsExtraStop = new TimeSpan();
				tsTotalTime = pdtmEndTime - pdtmStartTime;
                if ((adrowShift[0][PRO_ShiftPatternTable.REGULARSTOPFROM_FLD] != DBNull.Value) 
					&& (adrowShift[0][PRO_ShiftPatternTable.REGULARSTOPTO_FLD] != DBNull.Value))
				{
					DateTime dtmRegurStopFrom = new DateTime(pdtmStartTime.Year, pdtmStartTime.Month, pdtmStartTime.Day, ((DateTime)adrowShift[0][PRO_ShiftPatternTable.REGULARSTOPFROM_FLD]).Hour, ((DateTime)adrowShift[0][PRO_ShiftPatternTable.REGULARSTOPFROM_FLD]).Minute, ((DateTime)adrowShift[0][PRO_ShiftPatternTable.REGULARSTOPFROM_FLD]).Second);
					DateTime dtmRegurStopTo = new DateTime(pdtmStartTime.Year, pdtmStartTime.Month, pdtmStartTime.Day, ((DateTime)adrowShift[0][PRO_ShiftPatternTable.REGULARSTOPTO_FLD]).Hour, ((DateTime)adrowShift[0][PRO_ShiftPatternTable.REGULARSTOPTO_FLD]).Minute, ((DateTime)adrowShift[0][PRO_ShiftPatternTable.REGULARSTOPTO_FLD]).Second);
					tsRegularStop = GetNoWorkingTime(pdtmStartTime, pdtmEndTime,  dtmRegurStopFrom, dtmRegurStopTo);
					if (tsRegularStop == TimeSpan.MinValue)
					{
						return 0;
					}
				}
				if ((adrowShift[0][PRO_ShiftPatternTable.REFRESHINGFROM_FLD] != DBNull.Value) 
					&& (adrowShift[0][PRO_ShiftPatternTable.REFRESHINGTO_FLD] != DBNull.Value))
				{
					DateTime dtmRefreshingFrom = new DateTime(pdtmStartTime.Year, pdtmStartTime.Month, pdtmStartTime.Day, ((DateTime)adrowShift[0][PRO_ShiftPatternTable.REFRESHINGFROM_FLD]).Hour, ((DateTime)adrowShift[0][PRO_ShiftPatternTable.REFRESHINGFROM_FLD]).Minute, ((DateTime)adrowShift[0][PRO_ShiftPatternTable.REFRESHINGFROM_FLD]).Second);
					DateTime dtmRefreshingTo = new DateTime(pdtmStartTime.Year, pdtmStartTime.Month, pdtmStartTime.Day, ((DateTime)adrowShift[0][PRO_ShiftPatternTable.REFRESHINGTO_FLD]).Hour, ((DateTime)adrowShift[0][PRO_ShiftPatternTable.REFRESHINGTO_FLD]).Minute, ((DateTime)adrowShift[0][PRO_ShiftPatternTable.REFRESHINGTO_FLD]).Second);
					tsRefreshing = GetNoWorkingTime(pdtmStartTime, pdtmEndTime,  dtmRefreshingFrom, dtmRefreshingTo);
					if (tsRefreshing == TimeSpan.MinValue)
					{
						return 0;
					}
				}
				if ((adrowShift[0][PRO_ShiftPatternTable.EXTRASTOPFROM_FLD] != DBNull.Value) 
					&& (adrowShift[0][PRO_ShiftPatternTable.EXTRASTOPTO_FLD] != DBNull.Value))
				{
					DateTime dtmExtraStopFrom = new DateTime(pdtmStartTime.Year, pdtmStartTime.Month, pdtmStartTime.Day, ((DateTime)adrowShift[0][PRO_ShiftPatternTable.EXTRASTOPFROM_FLD]).Hour, ((DateTime)adrowShift[0][PRO_ShiftPatternTable.EXTRASTOPFROM_FLD]).Minute, ((DateTime)adrowShift[0][PRO_ShiftPatternTable.EXTRASTOPFROM_FLD]).Second);
					DateTime dtmExtraStopTo = new DateTime(pdtmStartTime.Year, pdtmStartTime.Month, pdtmStartTime.Day, ((DateTime)adrowShift[0][PRO_ShiftPatternTable.EXTRASTOPTO_FLD]).Hour, ((DateTime)adrowShift[0][PRO_ShiftPatternTable.EXTRASTOPTO_FLD]).Minute, ((DateTime)adrowShift[0][PRO_ShiftPatternTable.EXTRASTOPTO_FLD]).Second);
					tsExtraStop = GetNoWorkingTime(pdtmStartTime, pdtmEndTime,  dtmExtraStopFrom, dtmExtraStopTo);
					if (tsExtraStop == TimeSpan.MinValue)
					{
						return 0;
					}
				}
				TimeSpan tsWorkingTime = new TimeSpan();
				tsWorkingTime = tsTotalTime - (tsRefreshing + tsRegularStop + tsExtraStop);
				decTotalSecond = Convert.ToDecimal(tsWorkingTime.TotalSeconds);
			}
			return decTotalSecond; 
		}	
		/// <summary>
		/// Get no-working time per shift
		/// </summary>
		/// <param name="pdtmStartTime"></param>
		/// <param name="pdtmEndTime"></param>
		/// <param name="pdtmStartOffTime"></param>
		/// <param name="pdtmEndOffTime"></param>
		/// <returns>time for no-working time, -1 when working time = 0</returns>
		/// <author>Trada</author>
		/// <date>Wednesday, May 3 2006</date>
		private TimeSpan GetNoWorkingTime(DateTime pdtmStartTime, DateTime pdtmEndTime, DateTime pdtmStartOffTime, DateTime pdtmEndOffTime)
		{
			TimeSpan tsNoWorkingTime = new TimeSpan();
			if ((pdtmStartTime >= pdtmEndOffTime) || (pdtmEndTime <= pdtmStartOffTime))
			{
				return tsNoWorkingTime = TimeSpan.Zero;	
			}
			//if working time is belong to no-working time then return min value
			if ((pdtmStartTime >= pdtmStartOffTime)	&& (pdtmEndTime <= pdtmEndOffTime))
			{
				return tsNoWorkingTime = TimeSpan.MinValue;
			}
			//if no-working time is belong to working time then return no-working time
			if ((pdtmStartOffTime >= pdtmStartTime) && (pdtmEndOffTime <= pdtmEndTime))
			{
				return tsNoWorkingTime = pdtmEndOffTime - pdtmStartOffTime;
			}
			if ((pdtmStartOffTime >= pdtmStartTime) && (pdtmEndOffTime >= pdtmEndTime))
			{
				return tsNoWorkingTime = pdtmEndTime - pdtmStartOffTime;
			}
			if ((pdtmStartOffTime <= pdtmStartTime) && (pdtmEndOffTime <= pdtmEndTime))
			{
				return tsNoWorkingTime = pdtmEndOffTime - pdtmStartTime;
			}
            return tsNoWorkingTime = TimeSpan.MinValue;
		}
		/// <summary>
		/// Get Working Day of Date
		/// </summary>
		/// <param name="pdtmTime"></param>
		/// <param name="pdtmBaseFrom"></param>
		/// <param name="pdtmBaseTo"></param>
		/// <returns></returns>
		/// <author>Trada</author>
		/// <date>Monday, April 24 2006</date>
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

		#endregion

	
		public DataSet GetProductSequence(int pintProductionLineID)
		{
			PRO_DCOptionMasterDS dsMaster = new PRO_DCOptionMasterDS();
			return dsMaster.GetProductSequence(pintProductionLineID);
		}
	
		public DataSet GetProductSequenceByDefault(int pintProductionLineID)
		{
			PRO_DCOptionMasterDS dsMaster = new PRO_DCOptionMasterDS();
			DataSet dstData = dsMaster.GetProductSequenceByDefault(pintProductionLineID);
			DataSet dstNew = dstData.Clone();
			dstNew.Tables[0].Columns.Add(new DataColumn("ProductProductionOrderID", typeof(int)));
			dstNew.Tables[0].Columns["ProductProductionOrderID"].AutoIncrement = true;
			dstNew.Tables[0].Columns["ProductProductionOrderID"].AutoIncrementSeed = 1;
			dstNew.Tables[0].Columns["ProductProductionOrderID"].AutoIncrementStep = 1;
			int intSequence = 1;
			foreach (DataRow drowData in dstData.Tables[0].Rows)
			{
				DataRow drowNew = dstNew.Tables[0].NewRow();
				foreach (DataColumn dcolData in dstData.Tables[0].Columns)
					drowNew[dcolData.ColumnName] = drowData[dcolData.ColumnName];
				drowNew["Seq"] = intSequence;
				dstNew.Tables[0].Rows.Add(drowNew);
				intSequence ++;
			}
			return dstData;
		}
	
		public void UpdateProductSequence(DataSet pdstData)
		{
			PRO_DCOptionMasterDS dsMaster = new PRO_DCOptionMasterDS();
			dsMaster.UpdateProductSequence(pdstData);
		}
	
		public DataTable GetWCConfig(int pintProductionLineID)
		{
			DCPReportDS dsReport = new DCPReportDS();
			return dsReport.GetWCConfig(pintProductionLineID);
		}
	
		public DataTable ListShift()
		{
			DCPReportDS dsReport = new DCPReportDS();
			return dsReport.GetShift();
		}
	}
}