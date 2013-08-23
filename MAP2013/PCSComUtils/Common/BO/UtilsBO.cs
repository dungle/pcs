using System;
using System.Collections;
using System.Data;
using PCSComUtils.Admin.DS;
using PCSComUtils.Framework.TableFrame.DS;
using PCSComUtils.PCSExc;
using PCSComUtils.Common.DS;
using PCSComUtils.MasterSetup.DS;
using PCSComUtils.DataContext;
using PCSComUtils.DataAccess;
using System.Linq;

namespace PCSComUtils.Common.BO
{
    public class UtilsBO
    {
        #region IUtilsBO Members
        //**************************************************************************              
        ///    <Description>
        ///       Get list of QA status
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
        ///       THIENHD
        ///    </Authors>
        ///    <History>
        ///       15-Dec-2004
        ///    </History>
        ///    <Notes>
        ///    </Notes>
        //**************************************************************************


       
        public DataTable GetQAStatus()
        {
            const string ID_FIELD = "ID";
            const string VALUE_FIELD = "VALUE";

            // TODO:  Add ProductItemInfoBO.GetQAStatus implementation
            try
            {
                DataTable dtQAStatus = new DataTable();
                dtQAStatus.Columns.Add(ID_FIELD);
                dtQAStatus.Columns.Add(VALUE_FIELD);
                DataRow drNewRow = dtQAStatus.NewRow();

                drNewRow = dtQAStatus.NewRow();
                dtQAStatus.Rows.Add(drNewRow);

                drNewRow = dtQAStatus.NewRow();
                drNewRow[ID_FIELD] = "1";
                drNewRow[VALUE_FIELD] = "not source quality assured and requires inspection";
                dtQAStatus.Rows.Add(drNewRow);

                drNewRow = dtQAStatus.NewRow();
                drNewRow[ID_FIELD] = "2";
                drNewRow[VALUE_FIELD] = "not source quality assured but does not require inspection";
                dtQAStatus.Rows.Add(drNewRow);

                drNewRow = dtQAStatus.NewRow();

                drNewRow[ID_FIELD] = "3";
                drNewRow[VALUE_FIELD] = "source quality assured";
                dtQAStatus.Rows.Add(drNewRow);

                return dtQAStatus;
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


       
        public DataTable GetMaterialReceiptType()
        {
            // TODO:  Add ProductItemInfoBO.GetQAStatus implementation
            try
            {
                DataTable dtTable = new DataTable();
                dtTable.Columns.Add(Constants.ID_FIELD);
                dtTable.Columns.Add(Constants.VALUE_FIELD);
                DataRow drNewRow;

                drNewRow = dtTable.NewRow();
                drNewRow[Constants.ID_FIELD] = "1";
                drNewRow[Constants.VALUE_FIELD] = "PO";
                dtTable.Rows.Add(drNewRow);

                drNewRow = dtTable.NewRow();
                drNewRow[Constants.ID_FIELD] = "2";
                drNewRow[Constants.VALUE_FIELD] = "WO";
                dtTable.Rows.Add(drNewRow);

                return dtTable;
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
        //**************************************************************************              
        ///    <Description>
        ///       This method uses to get From Date, To Date of current period
        ///    </Description>
        ///    <Inputs></Inputs>
        ///    <Outputs> 
        ///			DateTime, DateTime 
        ///	   </Outputs>
        ///    <Returns></Returns>
        ///    <Authors>
        ///		  Tuan TQ, Isphere Software Co., Ltd 
        ///    </Authors>
        ///    <History>
        ///		  Created Date: 05-May-2005
        ///    </History>
        ///    <Notes></Notes>
        //**************************************************************************
        public void GetDateOfCurrentPeriod(out DateTime p_dtm_FromDate, out DateTime p_dtm_ToDate)
        {
            try
            {
                string str_Condition = Constants.WHERE_KEYWORD + " " + Sys_PeriodTable.ACTIVATE_FLD + " = " + Constants.TRUE_VALUE.ToString();

                DataTable table = (new UtilsDS()).GetRows(Sys_PeriodTable.TABLE_NAME, str_Condition);
                if (table != null)
                {
                    if (table.Rows.Count != 0)
                    {
                        p_dtm_FromDate = DateTime.Parse(table.Rows[0][Sys_PeriodTable.FROMDATE_FLD].ToString());
                        p_dtm_ToDate = DateTime.Parse(table.Rows[0][Sys_PeriodTable.TODATE_FLD].ToString());
                        return;
                    }
                }
                //otherwise, get current date on DB server
                p_dtm_FromDate = GetDBDate();
                p_dtm_ToDate = GetDBDate();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

       
        public DateTime GetDBDate()
        {
            // TODO:  Add UtilsBO.GetDBDate implementation
            try
            {
                UtilsDS objUtilsDS = new UtilsDS();
                return objUtilsDS.GetDBDate();
            }
            catch (PCSException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

       
        public string GetNoByMask(string strTableName, string strFieldName, DateTime dtEntryDate, string strFormat)
        {
            // TODO:  Add UtilsBO.GetDBDate implementation
            try
            {
                UtilsDS objUtilsDS = new UtilsDS();
                return objUtilsDS.GetNoByMask(strTableName, strFieldName, dtEntryDate, strFormat);
            }
            catch (PCSException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
       
        public string GetNoByMask(string pstrTableName, string pstrFieldName, string pstrPrefix, string pstrFormat)
        {
            UtilsDS objUtilsDS = new UtilsDS();
            return objUtilsDS.GetNoByMask(pstrTableName, pstrFieldName, pstrPrefix, pstrFormat);
        }

        public string GetNoByMask(string pstrTableName, string pstrFieldName, string pstrPrefix, string pstrFormat, DateTime entryDate, out int revision)
        {
            UtilsDS objUtilsDS = new UtilsDS();
            return objUtilsDS.GetNoByMask(pstrTableName, pstrFieldName, pstrPrefix, pstrFormat, entryDate, out revision);
        }
       
        public string GetNoByMask(string pstrUsername, string pstrTableName, string pstrFieldName, string pstrPrefix, string pstrFormat)
        {
            UtilsDS objUtilsDS = new UtilsDS();
            return objUtilsDS.GetNoByMask(pstrUsername, pstrTableName, pstrFieldName, pstrPrefix, pstrFormat);
        }

        #endregion

        /// <summary>
        /// Get comma-separated list of In stock tranaction type ID
        /// </summary>
        /// <returns></returns>
        /// <author> Tuan TQ. 28 Dec, 2005</author>		
        public string GetInStockTransTypeID()
        {
            return (new UtilsDS()).GetInStockTransTypeID();
        }
        public Sys_Menu_EntryVO GetMenuInfoForCK(string pstrFormName, string pstrTableName, string pstrTransNoFieldName, string pstrPrefix)
        {
            Sys_Menu_EntryDS dsMenu = new Sys_Menu_EntryDS();
            return dsMenu.GetMenuByFormLoadForCK(pstrFormName, pstrTableName, pstrTransNoFieldName, pstrPrefix);
        }
        /// <summary>
        /// Get comma-separated list of Out stock tranaction type ID
        /// </summary>
        /// <returns></returns>
        /// <author> Tuan TQ. 28 Dec, 2005</author>
        public string GetOutStockTransTypeID()
        {
            return (new UtilsDS()).GetOutStockTransTypeID();
        }

        public ArrayList GetWorkingDayByYear(int pintYear)
        {
            return (new UtilsDS()).GetWorkingDayByYear(pintYear);
        }

        /// <summary>
        /// Get list of Holiday in a specific year
        /// </summary>
        /// <param name="pdtmStartDate"></param>
        /// <param name="penuSchedule"></param>
        /// <returns></returns>
        public ArrayList GetHolidaysInYear(DateTime pdtmStartDate, ScheduleMethodEnum penuSchedule)
        {
            return (new UtilsDS()).GetHolidaysInYear(pdtmStartDate, penuSchedule);
        }

        public ArrayList GetHolidaysInYear(int pintYear)
        {
            return (new UtilsDS()).GetHolidaysInYear(pintYear);
        }

        /// <summary>
        /// Get working date after adding pintNumberOfDay
        /// </summary>
        /// <param name="pdtmDate"></param>
        /// <param name="pintNumberOfDay"></param>
        /// <param name="penuSchedule">
        /// 1: Forward
        /// 2: Backword
        /// </param>
        /// <returns></returns>
        public DateTime ConvertWorkingDay(DateTime pdtmDate, int pintNumberOfDay, ScheduleMethodEnum penuSchedule)
        {
            try
            {
                DateTime dtmConvert = pdtmDate;//new DateTime(pdtmDate.Year, pdtmDate.Month, pdtmDate.Day);				
                UtilsDS dsUtil = new UtilsDS();

                ArrayList arrDayOfWeek = dsUtil.GetWorkingDayByYear(pdtmDate.Year);
                ArrayList arrHolidays = dsUtil.GetHolidaysInYear(pdtmDate, penuSchedule);

                switch (penuSchedule)
                {
                    case ScheduleMethodEnum.Forward:
                        for (int i = 0; i < pintNumberOfDay; i++)
                        {
                            //Add day
                            dtmConvert = dtmConvert.AddDays(1);

                            //goto next day if the day is holidayday
                            while (arrHolidays.Contains(new DateTime(dtmConvert.Year, dtmConvert.Month, dtmConvert.Day)))
                            {
                                dtmConvert = dtmConvert.AddDays(1);
                            }

                            //goto next day if the day is off day
                            while (arrDayOfWeek.Contains(dtmConvert.DayOfWeek))
                            {
                                dtmConvert = dtmConvert.AddDays(1);
                                if (arrHolidays.Contains(new DateTime(dtmConvert.Year, dtmConvert.Month, dtmConvert.Day)))
                                {
                                    dtmConvert = dtmConvert.AddDays(1);
                                }
                            }

                            //goto next day if the day is holidayday
                            while (arrHolidays.Contains(new DateTime(dtmConvert.Year, dtmConvert.Month, dtmConvert.Day)))
                            {
                                dtmConvert = dtmConvert.AddDays(1);
                            }
                        }

                        //goto next day if the day is holidayday
                        while (arrHolidays.Contains(new DateTime(dtmConvert.Year, dtmConvert.Month, dtmConvert.Day)))
                        {
                            dtmConvert = dtmConvert.AddDays(1);
                        }

                        //goto next day if the day is off day
                        while (arrDayOfWeek.Contains(dtmConvert.DayOfWeek))
                        {
                            dtmConvert = dtmConvert.AddDays(1);
                            if (arrHolidays.Contains(new DateTime(dtmConvert.Year, dtmConvert.Month, dtmConvert.Day)))
                            {
                                dtmConvert = dtmConvert.AddDays(1);
                            }
                        }

                        //goto next day if the day is holidayday
                        while (arrHolidays.Contains(new DateTime(dtmConvert.Year, dtmConvert.Month, dtmConvert.Day)))
                        {
                            dtmConvert = dtmConvert.AddDays(1);
                        }

                        break;


                    case ScheduleMethodEnum.Backward:
                        for (int i = 0; i < pintNumberOfDay; i++)
                        {
                            //Add day
                            dtmConvert = dtmConvert.AddDays(-1);

                            //goto next day if the day is holidayday
                            while (arrHolidays.Contains(new DateTime(dtmConvert.Year, dtmConvert.Month, dtmConvert.Day)))
                            {
                                dtmConvert = dtmConvert.AddDays(-1);
                            }

                            //goto next day if the day is off day
                            while (arrDayOfWeek.Contains(dtmConvert.DayOfWeek))
                            {
                                dtmConvert = dtmConvert.AddDays(-1);
                                if (arrHolidays.Contains(new DateTime(dtmConvert.Year, dtmConvert.Month, dtmConvert.Day)))
                                {
                                    dtmConvert = dtmConvert.AddDays(-1);
                                }
                            }

                            //goto next day if the day is holidayday
                            while (arrHolidays.Contains(new DateTime(dtmConvert.Year, dtmConvert.Month, dtmConvert.Day)))
                            {
                                dtmConvert = dtmConvert.AddDays(-1);
                            }
                        }

                        //goto next day if the day is holidayday
                        while (arrHolidays.Contains(new DateTime(dtmConvert.Year, dtmConvert.Month, dtmConvert.Day)))
                        {
                            dtmConvert = dtmConvert.AddDays(-1);
                        }

                        //goto next day if the day is off day
                        while (arrDayOfWeek.Contains(dtmConvert.DayOfWeek))
                        {
                            dtmConvert = dtmConvert.AddDays(-1);
                            if (arrHolidays.Contains(new DateTime(dtmConvert.Year, dtmConvert.Month, dtmConvert.Day)))
                            {
                                dtmConvert = dtmConvert.AddDays(-1);
                            }
                        }

                        //goto next day if the day is holidayday
                        while (arrHolidays.Contains(new DateTime(dtmConvert.Year, dtmConvert.Month, dtmConvert.Day)))
                        {
                            dtmConvert = dtmConvert.AddDays(-1);
                        }

                        break;
                }

                return dtmConvert;
            }
            catch (PCSException ex)
            {
                throw ex.CauseException;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        /// <summary>
        /// Get working date after adding pintNumberOfDay
        /// </summary>
        /// <param name="pdtmDate"></param>
        /// <param name="pintNumberOfDay"></param>
        /// <param name="penuSchedule">
        /// 1: Forward
        /// 2: Backword
        /// </param>
        /// <returns></returns>
        public DateTime ConvertWorkingDay(DateTime pdtmDate, decimal pdecNumberOfDay, ScheduleMethodEnum penuSchedule)
        {
            try
            {
                int intNumberOfDay = (int)decimal.Floor(pdecNumberOfDay);
                double dblRemainder = (double)(pdecNumberOfDay - (decimal)intNumberOfDay);

                DateTime dtmConvert = pdtmDate;//new DateTime(pdtmDate.Year, pdtmDate.Month, pdtmDate.Day);				
                UtilsDS dsUtil = new UtilsDS();

                ArrayList arrDayOfWeek = dsUtil.GetWorkingDayByYear(pdtmDate.Year);
                ArrayList arrHolidays = dsUtil.GetHolidaysInYear(pdtmDate, penuSchedule);

                switch (penuSchedule)
                {
                    case ScheduleMethodEnum.Forward:
                        for (int i = 0; i < intNumberOfDay; i++)
                        {
                            //Add day
                            dtmConvert = dtmConvert.AddDays(1);

                            //goto next day if the day is holidayday
                            while (arrHolidays.Contains(new DateTime(dtmConvert.Year, dtmConvert.Month, dtmConvert.Day)))
                            {
                                dtmConvert = dtmConvert.AddDays(1);
                            }

                            //goto next day if the day is off day
                            while (arrDayOfWeek.Contains(dtmConvert.DayOfWeek))
                            {
                                dtmConvert = dtmConvert.AddDays(1);
                                if (arrHolidays.Contains(new DateTime(dtmConvert.Year, dtmConvert.Month, dtmConvert.Day)))
                                {
                                    dtmConvert = dtmConvert.AddDays(1);
                                }
                            }

                            //goto next day if the day is holidayday
                            while (arrHolidays.Contains(new DateTime(dtmConvert.Year, dtmConvert.Month, dtmConvert.Day)))
                            {
                                dtmConvert = dtmConvert.AddDays(1);
                            }
                        }
                        //Add remainder
                        dtmConvert = dtmConvert.AddDays(dblRemainder);

                        //goto next day if the day is holidayday
                        while (arrHolidays.Contains(new DateTime(dtmConvert.Year, dtmConvert.Month, dtmConvert.Day)))
                        {
                            dtmConvert = dtmConvert.AddDays(1);
                        }

                        //goto next day if the day is off day
                        while (arrDayOfWeek.Contains(dtmConvert.DayOfWeek))
                        {
                            dtmConvert = dtmConvert.AddDays(1);
                            if (arrHolidays.Contains(new DateTime(dtmConvert.Year, dtmConvert.Month, dtmConvert.Day)))
                            {
                                dtmConvert = dtmConvert.AddDays(1);
                            }
                        }

                        //goto next day if the day is holidayday
                        while (arrHolidays.Contains(new DateTime(dtmConvert.Year, dtmConvert.Month, dtmConvert.Day)))
                        {
                            dtmConvert = dtmConvert.AddDays(1);
                        }
                        break;


                    case ScheduleMethodEnum.Backward:
                        for (int i = 0; i < intNumberOfDay; i++)
                        {
                            //Add day
                            dtmConvert = dtmConvert.AddDays(-1);

                            //goto next day if the day is holidayday
                            while (arrHolidays.Contains(new DateTime(dtmConvert.Year, dtmConvert.Month, dtmConvert.Day)))
                            {
                                dtmConvert = dtmConvert.AddDays(-1);
                            }

                            //goto next day if the day is off day
                            while (arrDayOfWeek.Contains(dtmConvert.DayOfWeek))
                            {
                                dtmConvert = dtmConvert.AddDays(-1);
                                if (arrHolidays.Contains(new DateTime(dtmConvert.Year, dtmConvert.Month, dtmConvert.Day)))
                                {
                                    dtmConvert = dtmConvert.AddDays(-1);
                                }
                            }

                            //goto next day if the day is holidayday
                            while (arrHolidays.Contains(new DateTime(dtmConvert.Year, dtmConvert.Month, dtmConvert.Day)))
                            {
                                dtmConvert = dtmConvert.AddDays(-1);
                            }
                        }

                        //Add remainder
                        dtmConvert = dtmConvert.AddDays(-dblRemainder);

                        //goto next day if the day is holidayday
                        while (arrHolidays.Contains(new DateTime(dtmConvert.Year, dtmConvert.Month, dtmConvert.Day)))
                        {
                            dtmConvert = dtmConvert.AddDays(-1);
                        }

                        //goto next day if the day is off day
                        while (arrDayOfWeek.Contains(dtmConvert.DayOfWeek))
                        {
                            dtmConvert = dtmConvert.AddDays(-1);
                            if (arrHolidays.Contains(new DateTime(dtmConvert.Year, dtmConvert.Month, dtmConvert.Day)))
                            {
                                dtmConvert = dtmConvert.AddDays(-1);
                            }
                        }

                        //goto next day if the day is holidayday
                        while (arrHolidays.Contains(new DateTime(dtmConvert.Year, dtmConvert.Month, dtmConvert.Day)))
                        {
                            dtmConvert = dtmConvert.AddDays(-1);
                        }
                        break;
                }

                return dtmConvert;
            }
            catch (PCSException ex)
            {
                throw ex.CauseException;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public object GetUMInfor(int pintUMID)
        {
            try
            {
                MST_UnitOfMeasureDS dsUM = new MST_UnitOfMeasureDS();
                return dsUM.GetObjectVO(pintUMID);
            }
            catch (PCSException ex)
            {
                throw ex.CauseException;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public string GetCCNCodeFromID(int pintCCNID)
        {
            try
            {
                MST_CCNDS dsCCN = new MST_CCNDS();
                return dsCCN.GetCCNCodeFromID(pintCCNID);
            }
            catch (PCSException ex)
            {
                throw ex.CauseException;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable ListCarrier()
        {
            try
            {
                MST_CarrierDS dsCarrier = new MST_CarrierDS();
                return dsCarrier.List().Tables[0];
            }
            catch (PCSException ex)
            {
                throw ex.CauseException;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable ListUnitOfMeasure()
        {
            try
            {
                MST_UnitOfMeasureDS dsUnit = new MST_UnitOfMeasureDS();
                return dsUnit.List().Tables[0];
            }
            catch (PCSException ex)
            {
                throw ex.CauseException;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable ListCurrency()
        {
            try
            {
                MST_CurrencyDS dsCurrency = new MST_CurrencyDS();
                return dsCurrency.List().Tables[0];
            }
            catch (PCSException ex)
            {
                throw ex.CauseException;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet ListCCN()
        {
            try
            {
                MST_CCNDS dsCCN = new MST_CCNDS();
                return dsCCN.List();
            }
            catch (PCSException ex)
            {
                throw ex.CauseException;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable ListCCNForCheckListBox()
        {
            try
            {
                MST_CCNDS dsCCN = new MST_CCNDS();
                return dsCCN.ListAllCCN();
            }
            catch (PCSException ex)
            {
                throw ex.CauseException;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable ListUser()
        {
            try
            {
                Sys_UserDS dsSysUser = new Sys_UserDS();
                return dsSysUser.ListAllUsers();
            }
            catch (PCSException ex)
            {
                throw ex.CauseException;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable ListAllMenu()
        {
            Sys_Menu_EntryDS dsSysMenu = new Sys_Menu_EntryDS();
            return dsSysMenu.List().Tables[0];

        }
        public DataSet ListMasterLocationByCCNID(int pintCCNID)
        {
            try
            {
                MST_MasterLocationDS dsMasterLocation = new MST_MasterLocationDS();
                return dsMasterLocation.ListByCCNID(pintCCNID);
            }
            catch (PCSException ex)
            {
                throw ex.CauseException;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet ListLocationByMasterLocationID(int pintMasterLocationID)
        {
            try
            {
                MST_LocationDS dsLocation = new MST_LocationDS();
                return dsLocation.ListByMasterLocationID(pintMasterLocationID);
            }
            catch (PCSException ex)
            {
                throw ex.CauseException;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet ListBinByLocationID(int pintLocationID)
        {
            try
            {
                MST_BINDS dsBIN = new MST_BINDS();
                return dsBIN.ListByLocationID(pintLocationID);
            }
            catch (PCSException ex)
            {
                throw ex.CauseException;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet ListReasonByCCNID(int pintCCNID)
        {
            try
            {
                MST_ReasonDS dsReason = new MST_ReasonDS();
                return dsReason.ListByCCNID(pintCCNID);
            }
            catch (PCSException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable ListCountry()
        {
            try
            {
                MST_CountryDS dsCountry = new MST_CountryDS();
                return dsCountry.List().Tables[0];
            }
            catch (PCSException ex)
            {
                throw ex.CauseException;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable ListCityByCountry(int pintCountryID)
        {
            try
            {
                MST_CityDS dsCity = new MST_CityDS();
                return dsCity.ListByCountry(pintCountryID);
            }
            catch (PCSException ex)
            {
                throw ex.CauseException;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable ListAllCity()
        {
            try
            {
                MST_CityDS dsCity = new MST_CityDS();
                return dsCity.List().Tables[0];
            }
            catch (PCSException ex)
            {
                throw ex.CauseException;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable ListAddCharge()
        {
            try
            {
                MST_AddChargeDS dsAddCharge = new MST_AddChargeDS();
                return dsAddCharge.List().Tables[0];
            }
            catch (PCSException ex)
            {
                throw ex.CauseException;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable ListReasonByCCN(int pintCCNID)
        {
            MST_ReasonDS dsReason = new MST_ReasonDS();
            return dsReason.List(pintCCNID).Tables[0];
        }
        public DataTable GetRows(string pstrTableName, string pstrFieldName, string pstrFieldValue, Hashtable phashOtherConditions, string pstrConditionByRecord)
        {
            UtilsDS objUtilsDS = new UtilsDS();
            return objUtilsDS.IsTableOrViewConfigured(pstrTableName) ? objUtilsDS.GetRows(pstrTableName, pstrFieldName, pstrFieldValue, phashOtherConditions, pstrConditionByRecord) : null;
        }

        public DataTable GetRows(string pstrTableName, string pstrFieldName, string pstrFieldValue, Hashtable phashOtherConditions)
        {
            UtilsDS objUtilsDS = new UtilsDS();
            return objUtilsDS.IsTableOrViewConfigured(pstrTableName) ? objUtilsDS.GetRows(pstrTableName, pstrFieldName, pstrFieldValue, phashOtherConditions, string.Empty) : null;
        }

        public DataTable GetRows(string pstrTableName, string pstrExpression)
        {
            UtilsDS objUtilsDS = new UtilsDS();
            return objUtilsDS.IsTableOrViewConfigured(pstrTableName) ? objUtilsDS.GetRows(pstrTableName, pstrExpression) : null;
        }

        /// <summary>
        /// This method is used to get the DataTable result
        /// from a specific table with a specific search field key.
        /// This function will be used on form that need to search for ID from Code, Name
        /// such as Product Code, or Product description
        /// </summary>
        /// <param name="pstrTableName">Table Name</param>
        /// <param name="pstrFieldName">Filter Field</param>
        /// <param name="pstrFieldValue">Filter Value</param>
        /// <param name="pstrExpression">Expression or Where Clause</param>
        /// <returns>Result</returns>
        public DataTable GetRowsWithWhere(string pstrTableName, string pstrFieldName, string pstrFieldValue, string pstrExpression)
        {
            UtilsDS objUtilsDS = new UtilsDS();
            return objUtilsDS.IsTableOrViewConfigured(pstrTableName) ? objUtilsDS.GetRowsWithWhere(pstrTableName, pstrFieldName, pstrFieldValue, pstrExpression) : null;
        }

        public decimal GetUMRate(int pintInUMID, int pintOutUMID)
        {
            MST_UMRateDS dsUMRate = new MST_UMRateDS();
            return dsUMRate.GetUMRate(pintInUMID, pintOutUMID);
        }
        public decimal GetExchangeRate(int pintCurrencyID, DateTime pdtmPostDate)
        {
            MST_ExchangeRateDS dsMST = new MST_ExchangeRateDS();
            return dsMST.GetLastExchangeRate(pintCurrencyID, pdtmPostDate);
        }
        public bool IsSystemAdmin(string pstrUserName)
        {
            Sys_UserDS dsUser = new Sys_UserDS();
            return dsUser.IsSystemAdmin(pstrUserName);
        }

        /// <summary>
        /// This method will create a DataTable with two columns and hold following data:
        /// - R: Regular Time : 0
        /// - O: Over Time : 1
        /// - S: Sunday Time : 2
        /// </summary>
        /// <returns></returns>
        public DataTable GetHourCode()
        {
            // TODO:  Add ProductItemInfoBO.GetQAStatus implementation
            try
            {
                DataTable dtHourCode = new DataTable();
                dtHourCode.Columns.Add(Constants.ID_FIELD);
                dtHourCode.Columns.Add(Constants.VALUE_FIELD);

                DataRow drNewRow;

                drNewRow = dtHourCode.NewRow();
                drNewRow[Constants.ID_FIELD] = "0";
                drNewRow[Constants.VALUE_FIELD] = "Regular Time";
                dtHourCode.Rows.Add(drNewRow);

                drNewRow = dtHourCode.NewRow();
                drNewRow[Constants.ID_FIELD] = "1";
                drNewRow[Constants.VALUE_FIELD] = "Over Time";
                dtHourCode.Rows.Add(drNewRow);


                drNewRow = dtHourCode.NewRow();
                drNewRow[Constants.ID_FIELD] = "2";
                drNewRow[Constants.VALUE_FIELD] = "Sunday Time";
                dtHourCode.Rows.Add(drNewRow);

                return dtHourCode;
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
        /// This method will create a DataTable with two columns and hold following data:
        /// - S: Setup : 0
        /// - R: Run : 1
        /// </summary>
        /// <returns></returns>
        public DataTable GetSetupRunCode()
        {
            try
            {
                DataTable dtSetupRunCode = new DataTable();
                dtSetupRunCode.Columns.Add(Constants.ID_FIELD);
                dtSetupRunCode.Columns.Add(Constants.VALUE_FIELD);

                DataRow drNewRow;

                drNewRow = dtSetupRunCode.NewRow();
                drNewRow[Constants.ID_FIELD] = "0";
                drNewRow[Constants.VALUE_FIELD] = "Setup";
                dtSetupRunCode.Rows.Add(drNewRow);

                drNewRow = dtSetupRunCode.NewRow();
                drNewRow[Constants.ID_FIELD] = "1";
                drNewRow[Constants.VALUE_FIELD] = "Run";
                dtSetupRunCode.Rows.Add(drNewRow);

                return dtSetupRunCode;
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
        /// Get Machine and Labor setup code
        /// Get Machine and Labor load code
        /// </summary>
        /// <returns></returns>
        /// 
        public DataTable GetMLSetupLoadCode()
        {
            try
            {
                DataTable dtCode = new DataTable();
                dtCode.Columns.Add(Constants.ID_FIELD);
                dtCode.Columns.Add(Constants.VALUE_FIELD);

                DataRow drNewRow;

                drNewRow = dtCode.NewRow();
                drNewRow[Constants.ID_FIELD] = "COM";
                drNewRow[Constants.VALUE_FIELD] = "Operation Complete";
                dtCode.Rows.Add(drNewRow);


                drNewRow = dtCode.NewRow();
                drNewRow[Constants.ID_FIELD] = "EHL";
                drNewRow[Constants.VALUE_FIELD] = "Operation on engineering hold";
                dtCode.Rows.Add(drNewRow);

                drNewRow = dtCode.NewRow();
                drNewRow[Constants.ID_FIELD] = "IP";
                drNewRow[Constants.VALUE_FIELD] = "Operation in process/No Problems";
                dtCode.Rows.Add(drNewRow);

                drNewRow = dtCode.NewRow();
                drNewRow[Constants.ID_FIELD] = "MHL";
                drNewRow[Constants.VALUE_FIELD] = "Operation waiting materials";
                dtCode.Rows.Add(drNewRow);


                drNewRow = dtCode.NewRow();
                drNewRow[Constants.ID_FIELD] = "NS";
                drNewRow[Constants.VALUE_FIELD] = "Operation not started/No Problems";
                dtCode.Rows.Add(drNewRow);

                drNewRow = dtCode.NewRow();
                drNewRow[Constants.ID_FIELD] = "OHL";
                drNewRow[Constants.VALUE_FIELD] = "Operation waiting am operator";
                dtCode.Rows.Add(drNewRow);

                drNewRow = dtCode.NewRow();
                drNewRow[Constants.ID_FIELD] = "THL";
                drNewRow[Constants.VALUE_FIELD] = "Operation waiting tooling";
                dtCode.Rows.Add(drNewRow);

                return dtCode;
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
        /// Execute PO report and return data table
        /// </summary>
        /// <param name="pstrSql">Query string to be executed</param>
        /// <returns>Result DataTable</returns>
        public DataTable ExecutePOReport(ref string pstrSql, int pintPOMasterID)
        {
            try
            {
                UtilsDS dsUtils = new UtilsDS();
                return dsUtils.ExecutePOReport(ref pstrSql, pintPOMasterID);
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

        public object GetWorkOrderMasterInforForMaterialReceipt(int pintWorkOrderMasterID)
        {
            try
            {

                return null;
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

        public object GetWorkOrderDetailInforForMaterialReceipt(int pintWorkOrderDetailID)
        {
            try
            {
                return null;
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

        public object GetPurchaseOrderMasterInforForMaterialReceipt(int pintPurchaseOrderMasterID)
        {
            try
            {
                return null;
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

        public object GetPurchaseOrderDetailInforForMaterialReceipt(int pintPurchaseOrderDetailID)
        {
            try
            {
                return null;
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
        /// Get Master Location object by ID
        /// </summary>
        /// <param name="pintMasterLocID"></param>
        /// <returns></returns>
        /// <author>TuanDM</author>
        public MST_MasterLocationVO GetMasterLocByID(int pintMasterLocID)
        {
            try
            {
                return (MST_MasterLocationVO)new MST_MasterLocationDS().GetObjectVO(pintMasterLocID);
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
        /// Get database version
        /// </summary>
        /// <returns></returns>
        /// <author>DuongNA</author>
        public string GetDBVersion()
        {
            string strVersion = string.Empty;
            try
            {
                Sys_ParamDS dsSysParam = new Sys_ParamDS();
                // HACK: dungla 10-21-2005
                // use DB_VERSION from SystemParam instead of Constant
                strVersion = dsSysParam.GetNameValue(SystemParam.DB_VERSION);
                // END: dungla 10-21-2005
            }
            catch (PCSDBException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return strVersion;
        }

        public bool GetRoundedQuantity()
        {
            bool blnRoundedQuantity = false;
            const string ZERO = "0";
            const string ROUNDED_QUANTITY = "RoundedQuantity";
            try
            {
                Sys_ParamDS dsSysParam = new Sys_ParamDS();
                string strValue = dsSysParam.GetNameValue(ROUNDED_QUANTITY);
                blnRoundedQuantity = !strValue.Equals(ZERO);
            }
            catch (PCSDBException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return blnRoundedQuantity;
        }

        /// <summary>
        /// Get All system params
        /// </summary>
        /// <returns>DataTable of All System Params</returns>
        public DataTable GetSystemParams()
        {
            try
            {
                Sys_ParamDS dsSysParam = new Sys_ParamDS();
                return dsSysParam.List().Tables[0];
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
        /// Get MST_PartyVO object from ID
        /// </summary>
        /// <param name="pintPartyID">Party ID</param>
        /// <returns>MST_PartyVO</returns>
        /// <author>DungLA</author>
        public object GetPartyInfo(int pintPartyID)
        {
            MST_PartyDS dsParty = new MST_PartyDS();
            return dsParty.GetObjectVO(pintPartyID);
        }

        /// <summary>
        /// Get MST_CCNVO object from ID
        /// </summary>
        /// <param name="pintCCNID">CCNID</param>
        /// <returns>MST_CCNVO</returns>
        /// <author>DungLA</author>
        public object GetCCNInfo(int pintCCNID)
        {
            MST_CCNDS dsCCN = new MST_CCNDS();
            return dsCCN.GetObjectVO(pintCCNID);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pstrUserName"></param>
        /// <returns></returns>
        public bool UserNameBelongToAdministratorRole(string pstrUserName)
        {
            Sys_RoleDS dsRole = new Sys_RoleDS();
            return dsRole.UserNameBelongToAdministratorRole(pstrUserName);
        }

        public DataSet GetRightToModify(string pstrUserName, string pstrTableName, string pstrPrimaryKeyField, int pintMasterID)
        {
            Sys_RoleDS dsRole = new Sys_RoleDS();
            return dsRole.GetRightToModify(pstrUserName, pstrTableName, pstrPrimaryKeyField, pintMasterID);
        }

        public void UpdateUserNameModifyTransaction(string pstrUserName, string pstrTableName, string pstrPrimaryKeyField, int pintMasterID)
        {
            Sys_RoleDS dsRole = new Sys_RoleDS();
            dsRole.UpdateUserNameModifyTransaction(pstrUserName, pstrTableName, pstrPrimaryKeyField, pintMasterID);
        }

        public DataSet GetDefaultInfomation()
        {
            Sys_RoleDS dsRole = new Sys_RoleDS();
            return dsRole.GetDefaultInfomation();
        }

        public string GetConditionByRecord(string pstrUserName, string pstrTableName)
        {
            return (new sysGenerateTableFormDS()).GetConditionByRecord(pstrUserName, pstrTableName);
        }

        public DataTable GetPurposeByTransTypeID(int pintTransTypeID)
        {
            return new UtilsDS().GetPurPoseByTransType(pintTransTypeID);
        }

        public int GetTransTypeIDByCode(string pstrCode)
        {
            return new MST_TranTypeDS().GetTranTypeID(pstrCode);
        }
        /// <summary>
        /// Get home currency of CCN
        /// </summary>
        /// <param name="pintCCNID">CCN</param>
        /// <returns>Home Currency Code</returns>
        public string GetHomeCurrency(int pintCCNID)
        {
            MST_CurrencyDS dsCurrency = new MST_CurrencyDS();
            return dsCurrency.GetCurrencyByCCN(pintCCNID);
        }

        /// <summary>
        /// Get USD Currency
        /// </summary>
        /// <returns>USD MST_CurrencyVO object</returns>
        public object GetUSDCurrency()
        {
            MST_CurrencyDS dsCurrency = new MST_CurrencyDS();
            return dsCurrency.GetUSDCurrency();
        }


        #region HACKED: Thachnn: split function

        /// <summary>
        /// Split the input ArrayList of String to string element. Ex: 1 2 3 4 5 6 7 8 9  ----> (1,2,3) (4,5,6) (7,8,9)		
        /// </summary>
        /// <author>Thachnn 27/04/2006</author>
        /// <exception cref="">InvalidCastException</exception>
        /// <param name="parrNeedToSplit">ArrayList of string. If each element is not string, it will raise Invalid Cast Exception </param>
        /// <param name="pnSplitElementLength">Length of each output element, if this parameter is <=0, it will return empty ArrayList</param>
        /// <returns>ArrayList, each element is in format ( , , , ) </returns>
       
        public static ArrayList GetSplitList(ArrayList parrNeedToSplit, int pnSplitElementLength)
        {
            const string COMMA = ",";
            const string OPEN = "(";
            const string CLOSE = ")";

            #region exception case
            if (parrNeedToSplit.Count == 0 || pnSplitElementLength <= 0)
            {
                return new ArrayList();
            }

            if (parrNeedToSplit.Count == 1)
            {
                ArrayList arrRet = new ArrayList(1);
                arrRet.Add(OPEN + parrNeedToSplit[0] + CLOSE);
                return arrRet;
            }

            if (pnSplitElementLength == 1)		// improve speed
            {
                ArrayList arrRet = new ArrayList(parrNeedToSplit.Count);
                foreach (string str in parrNeedToSplit)
                {
                    arrRet.Add(OPEN + str + CLOSE);
                }
                return arrRet;
            }
            #endregion exception case

            int nCounter = 0;
            ArrayList arr = new ArrayList();

            //ArrayList arrTemp = parrNeedToSplit.GetRange(0, pnSplitElementLength);
            string strConcat = string.Empty;
            foreach (object obj in parrNeedToSplit)
            {
                if (obj != null)
                {
                    string str = obj.ToString();

                    strConcat = strConcat + str + COMMA;
                    nCounter++;

                    if (nCounter == pnSplitElementLength)
                    {
                        if (strConcat.EndsWith(COMMA))
                        {
                            strConcat = strConcat.Substring(0, strConcat.Length - 1);
                        }
                        arr.Add(OPEN + strConcat + CLOSE);
                        // reset
                        nCounter = 0;
                        strConcat = string.Empty;
                    }
                }
            }

            // add the last (not full element) to the return ArrayList
            if (0 < nCounter && nCounter <= pnSplitElementLength)
            {
                if (strConcat.EndsWith(COMMA))
                {
                    strConcat = strConcat.Substring(0, strConcat.Length - 1);
                }
                arr.Add(OPEN + strConcat + CLOSE);
                // reset
                nCounter = 0;
                strConcat = string.Empty;
            }

            return arr;
        }

        #endregion ENDHACKED: Thachnn: split function

        public void GetMenuInfo(string pstrFormName, out string strTableName, out string strTransNoFieldName, out string strPrefix, out string strFormat)
        {
            strTableName = string.Empty;
            strTransNoFieldName = string.Empty;
            strPrefix = string.Empty;
            strFormat = string.Empty;
            Sys_Menu_EntryDS dsMenu = new Sys_Menu_EntryDS();
            dsMenu.GetMenuByFormLoad(pstrFormName, out strTableName, out strTransNoFieldName, out strPrefix, out strFormat);
        }
        
        /// <summary>
        /// Update selected state for multi selection form
        /// </summary>
        /// <param name="pstrTableName">Temp Table Name</param>
        /// <param name="pstrFilter">Filter string</param>
        /// <param name="pblnSelected">Select state</param>
        /// <returns></returns>
        public DataSet UpdateSelected(string pstrTableName, string pstrFilter, bool pblnSelected)
        {
            UtilsDS dsUtils = new UtilsDS();
            return dsUtils.UpdateSelected(pstrTableName, pstrFilter, pblnSelected);
        }
        public DataSet UpdateSelectedRow(string pstrTableName, string pstrFilter, bool pblnSelected, int iProductID, int iWorkOrderMasterID, int iWorkOrderDetailID, int iComponentI)
        {
            UtilsDS dsUtils = new UtilsDS();
            return dsUtils.UpdateSelectedRow(pstrTableName, pstrFilter, pblnSelected, iProductID, iWorkOrderMasterID, iWorkOrderDetailID, iComponentI);
        }
        public void UpdateTempTable(DataSet pdtbData)
        {
            UtilsDS dsUtils = new UtilsDS();
            dsUtils.UpdateTempTable(pdtbData);
        }
        public PRO_Shift GetShiftDefault(string strShiftCode)
        {
            using (var db = new PCSDataContext(Utils.Instance.ConnectionString))
                return db.PRO_Shifts.SingleOrDefault(e => e.ShiftDesc == strShiftCode);
        }
    }
}