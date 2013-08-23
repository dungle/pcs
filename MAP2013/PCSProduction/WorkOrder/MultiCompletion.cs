using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using C1.Win.C1TrueDBGrid;
using log4net;
using PCSComMaterials.Inventory.BO;
using PCSComProduction.WorkOrder.BO;
using PCSComUtils.Common;
using PCSComUtils.Common.BO;
using PCSComUtils.Framework.ReportFrame.BO;
using PCSComUtils.PCSExc;
using PCSUtils.Log;
using PCSUtils.Utils;
using C1PrintPreviewDialog = PCSUtils.Framework.ReportFrame.C1PrintPreviewDialog;
using CancelEventArgs = System.ComponentModel.CancelEventArgs;

namespace PCSProduction.WorkOrder
{
    public partial class MultiCompletion : Form
    {
        public const string This = "PCSProduction.WorkOrder.MultiCompletion";
        private DataTable _gridLayout;
        private EnumAction _formMode = EnumAction.Default;
        private DataTable _dataSource;
        private DateTime _lastPostDate;
        private DateTime _serverDate;
        private ILog _logger = LogManager.GetLogger(typeof (MultiCompletion));

        public MultiCompletion()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Clears the form.
        /// </summary>
        private void ClearForm()
        {
            PostDatePicker.Value = DBNull.Value;
            FromDatePicker.Value = DBNull.Value;
            ToDatePicker.Value = DBNull.Value;
            MasterLocationText.Text = string.Empty;
            MasterLocationText.Tag = null;
            MultiCompletionNoText.Text = string.Empty;
            WorkOrderNoText.Text = string.Empty;
            WorkOrderNoText.Tag = null;
            ShiftText.Text = string.Empty;
            ShiftText.Tag = null;
            PurposeText.Text = string.Empty;
            PurposeText.Tag = null;

            if (_dataSource != null)
                _dataSource.Clear();
        }
        
        /// <summary>
        ///     Switching form mode, disable/enable related controls
        /// </summary>
        /// <param name="formMode"></param>
        private void SwitchFormMode(EnumAction formMode)
        {
            switch (formMode)
            {
                case EnumAction.Add:
                    PostDatePicker.ReadOnly = false;
                    MasterLocationText.ReadOnly = false;
                    MasterLocationButton.Enabled = true;
                    FromDatePicker.ReadOnly = false;
                    ToDatePicker.ReadOnly = false;
                    MultiNoButton.Enabled = false;
                    WorkOrderNoText.ReadOnly = false;
                    WorkOrderNoButton.Enabled = true;
                    ShiftText.ReadOnly = false;
                    ShiftButton.Enabled = true;
                    
                    ButtonSearch.Enabled = true;
                    AddButton.Enabled = false;
                    SaveButton.Enabled = true;
                    break;
                default:
                    PostDatePicker.ReadOnly = true;
                    MasterLocationText.ReadOnly = true;
                    MasterLocationButton.Enabled = false;
                    FromDatePicker.ReadOnly = true;
                    ToDatePicker.ReadOnly = true;
                    MultiNoButton.Enabled = true;
                    WorkOrderNoText.ReadOnly = true;
                    WorkOrderNoButton.Enabled = false;
                    ShiftText.ReadOnly = true;
                    ShiftButton.Enabled = false;

                    ButtonSearch.Enabled = false;
                    AddButton.Enabled = true;
                    SaveButton.Enabled = false;

                    DetailGrid.Splits[0].Locked = true;
                    break;
            }
        }

        /// <summary>
        /// Sets the default data when add new transaction
        /// </summary>
        private void SetDefaultData()
        {
            _serverDate = Utilities.Instance.GetServerDate();
            PostDatePicker.Value = _serverDate;
            var fromDate = new DateTime(_serverDate.Year, _serverDate.Month, 1);
            FromDatePicker.Value = fromDate;
            var toDate = new DateTime(_serverDate.Year, _serverDate.Month, _serverDate.Day, 23, 59, 59);
            ToDatePicker.Value = toDate;

            MasterLocationText.Text = SystemProperty.MasterLocationCode;
            MasterLocationText.Tag = SystemProperty.MasterLocationID;

            FillShift(null, true);
            FillPurpose(null, true);

            FillMultiCompletionNumber(_serverDate);
        }

        private void FillMultiCompletionNumber(DateTime postDate)
        {
            // fill new multi work order compltion number
            MultiCompletionNoText.Text = Utilities.Instance.GetMultiWorkOrderNumber(postDate);
        }

        private static string GetCompletionNo(string autoNumberFormat, string prefix, string postDateString, int maxValue)
        {
            var transNo = string.Format("{0}{1}", prefix, postDateString);
            transNo = string.Format("{0}{1}", transNo, maxValue.ToString().PadLeft(autoNumberFormat.Length, '0'));
            return transNo;
        }

        private void FillMultiWorkOrderData(DataRowView drowView)
        {
            if (drowView == null)
            {
                MultiCompletionNoText.Text = string.Empty;
                MultiCompletionNoText.Tag = null;
                ClearForm();
            }
            else
            {
                var multiNo = drowView[PRO_WorkOrderCompletionTable.MUlTICOMPLETIONNO_FLD].ToString();
                var boMulti = new MultiCompletionBO();
                var completion = boMulti.GetCompletionData(multiNo);
                // fill data
                PostDatePicker.Value = completion.PostDate;
                MasterLocationText.Text = completion.MST_MasterLocation.Code;
                MasterLocationText.Tag = completion.MasterLocationID;
                ShiftText.Text = completion.PRO_Shift.ShiftDesc;
                ShiftText.Tag = completion.ShiftID;
                PurposeText.Text = completion.PRO_IssuePurpose.Description;
                PurposeText.Tag = completion.IssuePurposeID;

                _dataSource = boMulti.SearchWorkOrderLine(multiNo);
                MultiCompletionNoText.Text = multiNo;
                FillDataGrid();
            }
        }

        private void FillWorkOrder(DataRowView drowView)
        {
            if (drowView == null)
            {
                WorkOrderNoText.Text = string.Empty;
                WorkOrderNoText.Tag = null;
            }
            else
            {
                WorkOrderNoText.Text = drowView[PRO_WorkOrderMasterTable.WORKORDERNO_FLD].ToString();
                WorkOrderNoText.Tag = drowView[PRO_WorkOrderMasterTable.WORKORDERMASTERID_FLD];
            }
        }

        private void FillMasterLocation(DataRowView drowView)
        {
            if (drowView == null)
            {
                MasterLocationText.Text = string.Empty;
                MasterLocationText.Tag = null;
            }
            else
            {
                MasterLocationText.Text = drowView[MST_MasterLocationTable.CODE_FLD].ToString();
                MasterLocationText.Tag = drowView[MST_MasterLocationTable.MASTERLOCATIONID_FLD];
            }
            // reset related fields
            FillWorkOrder(null);
            if (_dataSource != null)
                _dataSource.Clear();
        }

        private void FillDataGrid()
        {
            DetailGrid.DataSource = _dataSource;
            FormControlComponents.RestoreGridLayout(DetailGrid, _gridLayout);

            // format cell in grid
            DetailGrid.Columns[PRO_WorkOrderCompletionTable.COMPLETEDQUANTITY_FLD].NumberFormat = Constants.INTERGER_NUMBERFORMAT;
            DetailGrid.Columns[PRO_WorkOrderDetailTable.ORDERQUANTITY_FLD].NumberFormat = Constants.INTERGER_NUMBERFORMAT;
            DetailGrid.Columns[PRO_WorkOrderDetailTable.STARTDATE_FLD].NumberFormat = Constants.DATETIME_FORMAT_HOUR;
            DetailGrid.Columns[PRO_WorkOrderDetailTable.DUEDATE_FLD].NumberFormat = Constants.DATETIME_FORMAT_HOUR;
            DetailGrid.Splits[0].DisplayColumns[PRO_WorkOrderDetailTable.STARTDATE_FLD].Style.HorizontalAlignment =AlignHorzEnum.Center;
            DetailGrid.Splits[0].DisplayColumns[PRO_WorkOrderDetailTable.DUEDATE_FLD].Style.HorizontalAlignment = AlignHorzEnum.Center;
            if (_formMode == EnumAction.Add)
            {
                DetailGrid.Splits[0].DisplayColumns[PRO_WorkOrderCompletionTable.COMPLETEDQUANTITY_FLD].Locked = false;
                DetailGrid.Splits[0].DisplayColumns[PRO_WorkOrderCompletionTable.REMARK_FLD].Locked = false;
                DetailGrid.Splits[0].DisplayColumns[MST_LocationTable.TABLE_NAME + MST_LocationTable.CODE_FLD].Locked = false;
                DetailGrid.Splits[0].DisplayColumns[MST_LocationTable.TABLE_NAME + MST_LocationTable.CODE_FLD].Button = true;
                DetailGrid.Splits[0].DisplayColumns[MST_BINTable.TABLE_NAME + MST_BINTable.CODE_FLD].Locked = false;
                DetailGrid.Splits[0].DisplayColumns[MST_BINTable.TABLE_NAME + MST_BINTable.CODE_FLD].Button = true;
            }
        }

        private string GetPostDateString(DateTime postDate, string formatType)
        {
            const string dateStringShort = "D";
            const string dateStringFull = "DD";
            const string monthStringShort = "M";
            const string monthStringFull = "MM";
            const string yearStringShort = "YY";
            const string yearStringFull = "YYYY";

            //Replace the format_type with real value
            //1.Year
            if (formatType.IndexOf(yearStringFull) >= 0)
            {
                formatType = formatType.Replace(yearStringFull, postDate.Year.ToString());
            }
            else
            {
                if (formatType.IndexOf(yearStringShort) >= 0)
                {
                    formatType = formatType.Replace(yearStringShort, postDate.Year.ToString().Substring(2));
                }
            }
            //2.Month
            if (formatType.IndexOf(monthStringFull) >= 0)
            {
                formatType = formatType.Replace(monthStringFull, postDate.Month.ToString().PadLeft(2, '0'));
            }
            else
            {
                if (formatType.IndexOf(monthStringShort) >= 0)
                {
                    formatType = formatType.Replace(monthStringShort, postDate.Month.ToString());
                }
            }

            //3.Day
            if (formatType.IndexOf(dateStringFull) >= 0)
            {
                formatType = formatType.Replace(dateStringFull, postDate.Day.ToString().PadLeft(2, '0'));
            }
            else
            {
                if (formatType.IndexOf(dateStringShort) >= 0)
                {
                    formatType = formatType.Replace(dateStringShort, postDate.Day.ToString());
                }
            }

            return formatType;
        }

        private void ShowBOMShortageReport()
        {
            const string debugPath = @"PCSMain\bin\Debug";
            const string companyField = "fldCompany";
            
            const string reportFile = "MultiBomShortageReport.xml";
            try
            {
                if (DetailGrid.RowCount == 0)
                    return;
                
                //Change cursor type
                Cursor = Cursors.WaitCursor;

                var printPreview = new C1PrintPreviewDialog();
                var boDataReport = new C1PrintPreviewDialogBO();

                var workOrderDetailIdList = _dataSource.Rows.Cast<DataRow>().Where(row => row.RowState != DataRowState.Deleted).Select(row => row[PRO_WorkOrderCompletionTable.WORKORDERDETAILID_FLD].ToString());

                var dtbResult = boDataReport.GetMultiBomShortageData(workOrderDetailIdList.Distinct().ToList());
                // fill needed quantity based on bom quantity and completed quantity
                var sumResult = dtbResult.Clone();
                sumResult.Columns.Remove("WorkOrderDetailID");
                var binIdList = new List<string>();
                foreach (DataRow dataRow in _dataSource.Rows.Cast<DataRow>().Where(row => row.RowState != DataRowState.Deleted))
                {
                    var workOrderDetailId = dataRow[PRO_WorkOrderDetailTable.WORKORDERDETAILID_FLD].ToString();
                    var completedQuantity = Convert.ToDecimal(dataRow[PRO_WorkOrderCompletionTable.COMPLETEDQUANTITY_FLD]);
                    var children = dtbResult.Select(string.Format("WorkOrderDetailID = {0}", workOrderDetailId));
                    foreach (DataRow child in children)
                    {
                        var needQuantity = Convert.ToDecimal(child["NeedQuantity"]);
                        needQuantity = needQuantity*completedQuantity;
                        child["NeedQuantity"] = needQuantity;
                        // assign bin id to get available quantity
                        var location = InventoryUtilities.Instance.GetLocationInfo(Convert.ToInt32(dataRow[MST_LocationTable.LOCATIONID_FLD]));
                        var bufferBin = location.MST_BINs.FirstOrDefault(b => b.BinTypeID == (int?) BinTypeEnum.IN);
                        var binId = bufferBin != null
                                        ? bufferBin.BinID.ToString()
                                        : dataRow[MST_BINTable.BINID_FLD].ToString();
                        if (!binIdList.Contains(binId))
                        {
                            binIdList.Add(binId);
                        }
                        child[MST_BINTable.BINID_FLD] = binId;
                    }
                }
                var productIdList = new List<string>();
                var cache = new InventoryUtilsBO().ListAllBinCache(binIdList);
                foreach (DataRow row in dtbResult.Select(string.Empty, "Category ASC, Code ASC, Description ASC, Revision ASC"))
                {
                    var productId = row["ProductId"].ToString();
                    if (productIdList.Contains(productId))
                        continue;
                    productIdList.Add(productId);
                    var newRow = sumResult.NewRow();
                    // information
                    foreach (DataColumn column in sumResult.Columns)
                        newRow[column.ColumnName] = row[column.ColumnName];
                    // sum need quantity
                    var needQuantity = Convert.ToDecimal(dtbResult.Compute("SUM(NeedQuantity)", string.Format("ProductID = {0}", productId)));
                    var binId = row[MST_BINTable.BINID_FLD].ToString();
                    decimal availableQuantity;
                    try
                    {
                        availableQuantity = Convert.ToDecimal(cache.Compute(string.Format("SUM({0})", IV_BinCacheTable.OHQUANTITY_FLD),
                                                         string.Format("ProductID = {0} AND BinID = {1}", productId, binId)));
                    }
                    catch
                    {
                        availableQuantity = 0;
                    }
                    newRow["NeedQuantity"] = needQuantity;
                    newRow["OHQuantity"] = availableQuantity;
                    // only display component which not enough quantity
                    if (needQuantity > availableQuantity)
                        sumResult.Rows.Add(newRow);
                }
                // enoungh quantity to complete, no need to show report, focus on Save button
                if (sumResult.Rows.Count == 0)
                {
                    SaveButton.Focus();
                    return;
                }
                
                var reportBuilder = new ReportBuilder();

                //Get actual application path
                string strReportPath = Application.StartupPath;
                int intIndex = strReportPath.IndexOf(debugPath);
                if (intIndex > -1)
                {
                    strReportPath = strReportPath.Substring(0, intIndex);
                }

                strReportPath += strReportPath.Substring(strReportPath.Length - 1) == @"\"
                                     ? Constants.REPORT_DEFINITION_STORE_LOCATION
                                     : @"\" + Constants.REPORT_DEFINITION_STORE_LOCATION;

                //Set datasource and lay-out path for reports
                reportBuilder.SourceDataTable = sumResult;
                reportBuilder.ReportDefinitionFolder = strReportPath;

                reportBuilder.ReportLayoutFile = reportFile;

                //check if layout is valid
                if (reportBuilder.AnalyseLayoutFile())
                {
                    reportBuilder.UseLayoutFile = true;
                }
                else
                {
                    Cursor = Cursors.Default;
                    PCSMessageBox.Show(ErrorCode.MESSAGE_REPORT_TEMPLATE_FILE_NOT_FOUND, MessageBoxIcon.Error);
                    return;
                }

                reportBuilder.MakeDataTableForRender();

                // and show it in preview dialog								
                reportBuilder.ReportViewer = printPreview.ReportViewer;
                reportBuilder.RenderReport();

                //Header information get from system params				
                reportBuilder.DrawPredefinedField(companyField,
                                                  SystemProperty.SytemParams.Get(SystemParam.COMPANY_FULL_NAME));

                reportBuilder.RefreshReport();

                //Print report
                var field = reportBuilder.GetFieldByName("fldTitle");
                if (field != null)
                    printPreview.FormTitle = field.Text;

                printPreview.Show();
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private static DateTime GetCompletionDate(DateTime postDate, int productionLineId)
        {
            var boWorkOrderCompletion = new WorkOrderCompletionBO();
            int intYear = postDate.Year;
            int intMonth = postDate.Month;
            DateTime completedDate;
            DataSet dstWorkingTime = boWorkOrderCompletion.GetWorkingTimeByProductionLineAndYearMonth(productionLineId, intYear, intMonth);
            if (dstWorkingTime.Tables[0].Rows.Count == 1)
            {
                var dtmStartTimeFromDb = (DateTime)dstWorkingTime.Tables[0].Rows[0][PRO_WorkingTime.STARTTIME_FLD];
                //Build StartTime and EndTime 
                var dtmStartTime = new DateTime(postDate.Year, postDate.Month, postDate.Day,
                                                dtmStartTimeFromDb.Hour, dtmStartTimeFromDb.Minute, 0);
                DateTime dtmEndTime = dtmStartTime.AddHours(24);
                if (postDate >= dtmStartTime && postDate <= dtmEndTime)
                {
                    completedDate = new DateTime(postDate.Year, postDate.Month, postDate.Day, 0, 0, 0);
                }
                else
                {
                    completedDate = postDate.AddDays(-1);
                    completedDate = new DateTime(completedDate.Year, completedDate.Month, completedDate.Day);
                }
                return completedDate;
            }
            else
            {
                var dtmStartTimeFromDB = new DateTime(postDate.Year, postDate.Month, postDate.Day, 6, 15, 0);
                var dtmStartTime = new DateTime(postDate.Year, postDate.Month, postDate.Day,
                                                dtmStartTimeFromDB.Hour, dtmStartTimeFromDB.Minute, 0);
                if (postDate >= dtmStartTime)
                {
                    completedDate = new DateTime(postDate.Year, postDate.Month, postDate.Day, 0, 0, 0);
                }
                else
                {
                    completedDate = postDate.AddDays(-1);
                    completedDate = new DateTime(completedDate.Year, completedDate.Month, completedDate.Day);
                }
                
                return completedDate;
            }
        }

        /// <summary>
        /// Fills the shift.
        /// </summary>
        /// <param name="drowView">The drow view.</param>
        /// <param name="fillDefault">if set to <c>true</c> [fill default].</param>
        private void FillShift(DataRowView drowView, bool fillDefault)
        {
            if (fillDefault)
            {
                // fill default shift 'Full Day'
                var shift = FormControlComponents.OpenSearchForm(PRO_ShiftTable.TABLE_NAME,
                                                                   PRO_ShiftTable.SHIFTDESC_FLD, "Full Day",
                                                                   null, false);
                if (shift != null)
                {
                    ShiftText.Text = shift[PRO_ShiftTable.SHIFTDESC_FLD].ToString();
                    ShiftText.Tag = shift[PRO_ShiftTable.SHIFTID_FLD];
                }
            }
            else
            {
                if (drowView != null)
                {
                    ShiftText.Text = drowView[PRO_ShiftTable.SHIFTDESC_FLD].ToString();
                    ShiftText.Tag = drowView[PRO_ShiftTable.SHIFTID_FLD];
                }
                else
                {
                    ShiftText.Text = string.Empty;
                    ShiftText.Tag = null;
                }
            }
        }

        /// <summary>
        /// Fills the purpose.
        /// </summary>
        /// <param name="drowView">The drow view.</param>
        /// <param name="fillDefault">if set to <c>true</c> [fill default].</param>
        private void FillPurpose(DataRowView drowView, bool fillDefault)
        {
            if (fillDefault)
            {
                // fill default purpose
                var purpose = FormControlComponents.OpenSearchForm(PRO_IssuePurposeTable.TABLE_NAME,
                                                                   PRO_IssuePurposeTable.CODE_FLD, ((int)PurposeEnum.Completion).ToString(),
                                                                   null, false);
                if (purpose != null)
                {
                    PurposeText.Text = purpose[PRO_IssuePurposeTable.DESCRIPTION_FLD].ToString();
                    PurposeText.Tag = purpose[PRO_IssuePurposeTable.ISSUEPURPOSEID_FLD];
                }
            }
            else
            {
                if (drowView != null)
                {
                    PurposeText.Text = drowView[PRO_IssuePurposeTable.DESCRIPTION_FLD].ToString();
                    PurposeText.Tag = drowView[PRO_IssuePurposeTable.ISSUEPURPOSEID_FLD];
                }
                else
                {
                    PurposeText.Text = string.Empty;
                    PurposeText.Tag = null;
                }
            }
        }

        private void FillLocationData(DataRowView drowView)
        {
            if (drowView == null)
            {
                DetailGrid[DetailGrid.Row, MST_LocationTable.TABLE_NAME + MST_LocationTable.CODE_FLD] = string.Empty;
                DetailGrid[DetailGrid.Row, MST_LocationTable.LOCATIONID_FLD] = null;
            }
            else
            {
                DetailGrid[DetailGrid.Row, MST_LocationTable.TABLE_NAME + MST_LocationTable.CODE_FLD] = drowView[MST_LocationTable.CODE_FLD];
                DetailGrid[DetailGrid.Row, MST_LocationTable.LOCATIONID_FLD] = drowView[MST_LocationTable.LOCATIONID_FLD];
            }
            // reset bin
            FillBinData(null);
        }

        private void FillBinData(DataRowView drowView)
        {
            if (drowView == null)
            {
                DetailGrid[DetailGrid.Row, MST_BINTable.TABLE_NAME + MST_BINTable.CODE_FLD] = string.Empty;
                DetailGrid[DetailGrid.Row, MST_BINTable.BINID_FLD] = null;
            }
            else
            {
                DetailGrid[DetailGrid.Row, MST_BINTable.TABLE_NAME + MST_BINTable.CODE_FLD] = drowView[MST_BINTable.CODE_FLD];
                DetailGrid[DetailGrid.Row, MST_BINTable.BINID_FLD] = drowView[MST_BINTable.BINID_FLD];
            }
        }

        private void MultiCompletion_Load(object sender, EventArgs e)
        {
            const string methodName = This + ".MultiCompletion_Load()";
            try
            {
                //Set authorization for user
                
                var objSecurity = new Security();
                Name = This;

                if (objSecurity.SetRightForUserOnForm(this, SystemProperty.UserName) == 0)
                {
                    // You don't have the right to view this item
                    PCSMessageBox.Show(ErrorCode.MESSAGE_YOU_HAVE_NO_RIGHT_TO_VIEW, MessageBoxIcon.Warning);
                    Close();
                    return;
                }

                //store the gridlayout
                _gridLayout = FormControlComponents.StoreGridLayout(DetailGrid);
                _formMode = EnumAction.Default;
                ClearForm();
                SwitchFormMode(_formMode);
                MultiCompletionNoText.Focus();
            }
            catch (PCSException ex)
            {
                // displays the error message.
                PCSMessageBox.Show(ex.mCode, MessageBoxIcon.Error);
                // log message.
                try
                {
                    Logger.LogMessage(ex.CauseException, methodName, Level.ERROR);
                }
                catch
                {
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                // displays the error message.
                PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxIcon.Error);
                // log message.
                try
                {
                    Logger.LogMessage(ex, methodName, Level.ERROR);
                }
                catch
                {
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
                }
            }
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            const string methodName = This + ".AddButton_Click()";
            try
            {
                _formMode = EnumAction.Add;
                SwitchFormMode(_formMode);
                ClearForm();
                SetDefaultData();
                WorkOrderNoText.Focus();
            }
            catch (PCSException ex)
            {
                // displays the error message.
                PCSMessageBox.Show(ex.mCode, MessageBoxIcon.Error);
                // log message.
                try
                {
                    Logger.LogMessage(ex.CauseException, methodName, Level.ERROR);
                }
                catch
                {
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                // displays the error message.
                PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxIcon.Error);
                // log message.
                try
                {
                    Logger.LogMessage(ex, methodName, Level.ERROR);
                }
                catch
                {
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
                }
            }
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(MasterLocationText.Text.Trim()))
                {
                    PCSMessageBox.Show(ErrorCode.MESSAGE_RGA_MASTERLOCATION, MessageBoxIcon.Warning);
                    MasterLocationText.Focus();
                    return;
                }
                if (FormControlComponents.CheckMandatory(ShiftText))
                {
                    PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Warning);
                    ShiftText.Focus();
                    return;
                }
                if (FormControlComponents.CheckMandatory(PurposeText))
                {
                    PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Warning);
                    PurposeText.Focus();
                    return;
                }
                if (FromDatePicker.Value == null)
                {
                    PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Warning);
                    FromDatePicker.Focus();
                    return;
                }
                if (ToDatePicker.Value == null)
                {
                    PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Warning);
                    ToDatePicker.Focus();
                    return;
                }
                // to date cannot greater than post date
                var postDate = (DateTime)PostDatePicker.Value;
                //Check date in period
                if (!FormControlComponents.CheckDateInCurrentPeriod(postDate))
                {
                    PCSMessageBox.Show(ErrorCode.MESSAGE_RGV_POSTDATE_PERIOD, MessageBoxIcon.Exclamation);
                    PostDatePicker.Focus();
                    PostDatePicker.Select();
                    return;
                }
                //check the PostDate smaller than the current date
                if (postDate > Utilities.Instance.GetServerDate())
                {
                    PCSMessageBox.Show(ErrorCode.MESSAGE_INV_TRANSACTION_CANNOT_IN_FUTURE, MessageBoxIcon.Warning);
                    PostDatePicker.Focus();
                    return;
                }
                //Check postdate in configuration
                if (!FormControlComponents.CheckPostDateInConfiguration(postDate))
                {
                    return;
                }
                if (DetailGrid.RowCount == 0)
                {
                    PCSMessageBox.Show(ErrorCode.MESSAGE_INPUT_AT_LEAST_RECORD_IN_GRID, MessageBoxIcon.Warning);
                    DetailGrid.Focus();
                    return;
                }
                for (int i = 0; i < DetailGrid.RowCount; i++)
                {
                    #region check quantity
                    // check completed quantity
                    decimal completedQuantity;
                    if (!decimal.TryParse(DetailGrid[i, PRO_WorkOrderCompletionTable.COMPLETEDQUANTITY_FLD].ToString(), out completedQuantity))
                    {
                        PCSMessageBox.Show(ErrorCode.MESSAGE_INVALID_NUMERIC, MessageBoxIcon.Exclamation);
                        DetailGrid.Row = i;
                        DetailGrid.Col = DetailGrid.Splits[0].DisplayColumns.IndexOf(DetailGrid.Splits[0].DisplayColumns[PRO_WorkOrderCompletionTable.COMPLETEDQUANTITY_FLD]);
                        DetailGrid.Focus();
                        return;
                    }
                    if (completedQuantity <= 0)
                    {
                        PCSMessageBox.Show(ErrorCode.MESSAGE_WOCOMPLETION_COMPQTY_MUST_BE_GREATER_ZERO,
                                           MessageBoxIcon.Exclamation);
                        DetailGrid.Row = i;
                        DetailGrid.Col = DetailGrid.Splits[0].DisplayColumns.IndexOf(DetailGrid.Splits[0].DisplayColumns[PRO_WorkOrderCompletionTable.COMPLETEDQUANTITY_FLD]);
                        DetailGrid.Focus();
                        return;
                    }

                    #endregion

                    #region check location
                    if (DetailGrid[i, MST_LocationTable.TABLE_NAME + MST_LocationTable.CODE_FLD] == DBNull.Value || DetailGrid[i, MST_LocationTable.TABLE_NAME + MST_LocationTable.CODE_FLD].ToString() == string.Empty)
                    {
                        PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Exclamation);
                        DetailGrid.Row = i;
                        DetailGrid.Col = DetailGrid.Splits[0].DisplayColumns.IndexOf(DetailGrid.Splits[0].DisplayColumns[MST_LocationTable.TABLE_NAME + MST_LocationTable.CODE_FLD]);
                        DetailGrid.Focus();
                        return;
                    }
                    #endregion

                    #region bin
                    if (DetailGrid[i, MST_BINTable.TABLE_NAME + MST_BINTable.CODE_FLD] == DBNull.Value || DetailGrid[i, MST_BINTable.TABLE_NAME + MST_BINTable.CODE_FLD].ToString() == string.Empty)
                    {
                        PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Exclamation);
                        DetailGrid.Row = i;
                        DetailGrid.Col = DetailGrid.Splits[0].DisplayColumns.IndexOf(DetailGrid.Splits[0].DisplayColumns[MST_BINTable.TABLE_NAME + MST_BINTable.CODE_FLD]);
                        DetailGrid.Focus();
                        return;
                    }
                    #endregion
                }
                var boMulti = new MultiCompletionBO();
                boMulti.SaveCompletion(_dataSource, postDate, MultiCompletionNoText.Text, (int) MasterLocationText.Tag,
                                       (int) ShiftText.Tag, (int) PurposeText.Tag);
                PCSMessageBox.Show(ErrorCode.MESSAGE_AFTER_SAVE_DATA);
                _dataSource = boMulti.SearchWorkOrderLine(MultiCompletionNoText.Text);
                _formMode = EnumAction.Default;
                FillDataGrid();
                SwitchFormMode(_formMode);
                MultiCompletionNoText.Focus();
            }
            catch (PCSException ex)
            {
                if (ex.mCode == ErrorCode.MESSAGE_NOT_ENOUGH_COMPONENT_TO_COMPLETE)
                {
                    var strParam = new string[1];
                    strParam[0] = ex.mMethod;
                    //Show message
                    PCSMessageBox.Show(ErrorCode.MESSAGE_NOT_ENOUGH_QUANTITY_OF_COMPONENT_TO_COMPLETE,
                                       MessageBoxIcon.Warning, strParam);
                    var workOrderDetailId = Convert.ToInt32(ex.Hash[PRO_WorkOrderDetailTable.WORKORDERDETAILID_FLD]);
                    // find in the grid and set focus
                    for (int i = 0; i < DetailGrid.RowCount; i++)
                    {
                        var lineOrder = Convert.ToInt32(DetailGrid[i, PRO_WorkOrderDetailTable.WORKORDERDETAILID_FLD]);
                        if (workOrderDetailId == lineOrder)
                        {
                            DetailGrid.Row = i;
                            DetailGrid.Col = DetailGrid.Columns.IndexOf(DetailGrid.Columns[PRO_WorkOrderCompletionTable.WOCOMPLETIONNO_FLD]);
                            DetailGrid.Focus();
                        }
                    }
                }
                else
                {
                    PCSMessageBox.Show(ex.mCode, MessageBoxIcon.Error);
                }
                _logger.Error(ex.CauseException.Message, ex.CauseException);
            }
            catch (Exception ex)
            {
                // displays the error message.
                PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxIcon.Error);
                _logger.Error(ex.Message, ex);
            }
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void MasterLocationText_Validating(object sender, CancelEventArgs e)
        {
            const string methodName = This + ".MasterLocationText_Validating()";
            try
            {
                if (MasterLocationText.Text.Trim() == string.Empty)
                {
                    FillMasterLocation(null);
                    return;
                }
                if (!MasterLocationText.Modified)
                    return;
                var htCondition = new Hashtable {{MST_MasterLocationTable.CCNID_FLD, SystemProperty.CCNID}};
                var drvResult = FormControlComponents.OpenSearchForm(MST_MasterLocationTable.TABLE_NAME, MST_MasterLocationTable.CODE_FLD, MasterLocationText.Text.Trim(), htCondition, false);
                if (drvResult != null)
                    FillMasterLocation(drvResult);
                else
                    e.Cancel = true;
            }
            catch (PCSException ex)
            {
                // displays the error message.
                PCSMessageBox.Show(ex.mCode, MessageBoxIcon.Error);
                // log message.
                try
                {
                    Logger.LogMessage(ex.CauseException, methodName, Level.ERROR);
                }
                catch
                {
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                // displays the error message.
                PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxIcon.Error);
                // log message.
                try
                {
                    Logger.LogMessage(ex, methodName, Level.ERROR);
                }
                catch
                {
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
                }
            }
        }

        private void MasterLocationText_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F4 && MasterLocationButton.Enabled)
                MasterLocationButton_Click(null, null);
        }

        private void MasterLocationButton_Click(object sender, EventArgs e)
        {
            const string methodName = This + ".MasterLocationButton_Click()";
            try
            {
                var htCondition = new Hashtable { { MST_MasterLocationTable.CCNID_FLD, SystemProperty.CCNID } };
                var drvResult = FormControlComponents.OpenSearchForm(MST_MasterLocationTable.TABLE_NAME, MST_MasterLocationTable.CODE_FLD, MasterLocationText.Text.Trim(), htCondition, true);
                if (drvResult != null)
                    FillMasterLocation(drvResult);
                else
                    MasterLocationText.Focus();
            }
            catch (PCSException ex)
            {
                // displays the error message.
                PCSMessageBox.Show(ex.mCode, MessageBoxIcon.Error);
                // log message.
                try
                {
                    Logger.LogMessage(ex.CauseException, methodName, Level.ERROR);
                }
                catch
                {
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                // displays the error message.
                PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxIcon.Error);
                // log message.
                try
                {
                    Logger.LogMessage(ex, methodName, Level.ERROR);
                }
                catch
                {
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
                }
            }
        }

        private void MultiCompletionNoText_Validating(object sender, CancelEventArgs e)
        {
            const string methodName = This + ".MultiCompletionNoText_Validating()";
            try
            {
                if (!MultiNoButton.Enabled || !MultiCompletionNoText.Modified)
                {
                    return;
                }
                if (MultiCompletionNoText.Text.Trim() == string.Empty)
                {
                    FillMultiWorkOrderData(null);
                    return;
                }
                var htCondition = new Hashtable { { PRO_WorkOrderCompletionTable.CCNID_FLD, SystemProperty.CCNID } };
                var drvResult = FormControlComponents.OpenSearchForm("V_MultiWOCompletion", PRO_WorkOrderCompletionTable.MUlTICOMPLETIONNO_FLD, MultiCompletionNoText.Text.Trim(), htCondition, false);
                if (drvResult != null)
                    FillMultiWorkOrderData(drvResult);
                else
                    e.Cancel = true;
            }
            catch (PCSException ex)
            {
                // displays the error message.
                PCSMessageBox.Show(ex.mCode, MessageBoxIcon.Error);
                // log message.
                try
                {
                    Logger.LogMessage(ex.CauseException, methodName, Level.ERROR);
                }
                catch
                {
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                // displays the error message.
                PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxIcon.Error);
                // log message.
                try
                {
                    Logger.LogMessage(ex, methodName, Level.ERROR);
                }
                catch
                {
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
                }
            }
        }

        private void MultiCompletionNoText_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F4 && MultiNoButton.Enabled)
                MultiNoButton_Click(null, null);
        }

        private void MultiNoButton_Click(object sender, EventArgs e)
        {
            const string methodName = This + ".MultiNoButton_Click()";
            try
            {
                var htCondition = new Hashtable { { PRO_WorkOrderCompletionTable.CCNID_FLD, SystemProperty.CCNID } };
                var drvResult = FormControlComponents.OpenSearchForm("V_MultiWOCompletion", PRO_WorkOrderCompletionTable.MUlTICOMPLETIONNO_FLD, MultiCompletionNoText.Text.Trim(), htCondition, true);
                if (drvResult != null)
                    FillMultiWorkOrderData(drvResult);
                else
                    MasterLocationText.Focus();
            }
            catch (PCSException ex)
            {
                // displays the error message.
                PCSMessageBox.Show(ex.mCode, MessageBoxIcon.Error);
                // log message.
                try
                {
                    Logger.LogMessage(ex.CauseException, methodName, Level.ERROR);
                }
                catch
                {
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                // displays the error message.
                PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxIcon.Error);
                // log message.
                try
                {
                    Logger.LogMessage(ex, methodName, Level.ERROR);
                }
                catch
                {
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
                }
            }
        }

        private void WorkOrderNoText_Validating(object sender, CancelEventArgs e)
        {
            const string methodName = This + ".WorkOrderNoText_Validating()";
            try
            {
                if (WorkOrderNoText.Text.Trim() == string.Empty)
                {
                    FillWorkOrder(null);
                    return;
                }
                if (!WorkOrderNoText.Modified)
                    return;
                if (string.IsNullOrEmpty(MasterLocationText.Text.Trim()))
                {
                    PCSMessageBox.Show(ErrorCode.MESSAGE_SELECT_ONE_BEFORE_SELECT_ONE, MessageBoxIcon.Warning,
                                       new[] { MasterLocationLabel.Text, WorkOrderNoText.Text });
                    WorkOrderNoText.Text = string.Empty;
                    MasterLocationText.Focus();
                    return;
                }
                var htCondition = new Hashtable
                                      {
                                          {PRO_WorkOrderDetailTable.STATUS_FLD, (int) WOLineStatus.Released},
                                          {PRO_WorkOrderMasterTable.MASTERLOCATIONID_FLD, MasterLocationText.Tag}
                                      };
                var drvResult = FormControlComponents.OpenSearchForm("V_WOReleaseForCompletion", PRO_WorkOrderMasterTable.WORKORDERNO_FLD, WorkOrderNoText.Text.Trim(), htCondition, false);
                if (drvResult != null)
                    FillWorkOrder(drvResult);
                else
                    e.Cancel = true;
            }
            catch (PCSException ex)
            {
                // displays the error message.
                PCSMessageBox.Show(ex.mCode, MessageBoxIcon.Error);
                // log message.
                try
                {
                    Logger.LogMessage(ex.CauseException, methodName, Level.ERROR);
                }
                catch
                {
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                // displays the error message.
                PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxIcon.Error);
                // log message.
                try
                {
                    Logger.LogMessage(ex, methodName, Level.ERROR);
                }
                catch
                {
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
                }
            }
        }

        private void WorkOrderNoText_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F4 && WorkOrderNoButton.Enabled)
                WorkOrderNoButton_Click(null, null);
        }

        private void WorkOrderNoButton_Click(object sender, EventArgs e)
        {
            const string methodName = This + ".WorkOrderNoButton_Click()";
            try
            {
                if (string.IsNullOrEmpty(MasterLocationText.Text.Trim()))
                {
                    PCSMessageBox.Show(ErrorCode.MESSAGE_SELECT_ONE_BEFORE_SELECT_ONE, MessageBoxIcon.Warning,
                                       new[] {MasterLocationLabel.Text, WorkOrderNoText.Text});
                    MasterLocationText.Focus();
                    return;
                }
                var htCondition = new Hashtable
                                      {
                                          {PRO_WorkOrderDetailTable.STATUS_FLD, (int) WOLineStatus.Released},
                                          {PRO_WorkOrderMasterTable.MASTERLOCATIONID_FLD, MasterLocationText.Tag}
                                      };
                var drvResult = FormControlComponents.OpenSearchForm("V_WOReleaseForCompletion", PRO_WorkOrderMasterTable.WORKORDERNO_FLD, WorkOrderNoText.Text.Trim(), htCondition, true);
                if (drvResult != null)
                    FillWorkOrder(drvResult);
                else
                    WorkOrderNoText.Focus();
            }
            catch (PCSException ex)
            {
                // displays the error message.
                PCSMessageBox.Show(ex.mCode, MessageBoxIcon.Error);
                // log message.
                try
                {
                    Logger.LogMessage(ex.CauseException, methodName, Level.ERROR);
                }
                catch
                {
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                // displays the error message.
                PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxIcon.Error);
                // log message.
                try
                {
                    Logger.LogMessage(ex, methodName, Level.ERROR);
                }
                catch
                {
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
                }
            }
        }

        private void ShiftText_Validating(object sender, CancelEventArgs e)
        {
            const string methodName = This + ".ShiftText_Validating()";
            try
            {
                if (ShiftText.Text.Trim() == string.Empty)
                {
                    FillShift(null, false);
                    return;
                }
                if (!ShiftText.Modified)
                    return;
                var drvResult = FormControlComponents.OpenSearchForm(PRO_ShiftTable.TABLE_NAME, PRO_ShiftTable.SHIFTDESC_FLD, ShiftText.Text.Trim(), null, false);
                if (drvResult != null)
                    FillShift(drvResult, false);
                else
                    e.Cancel = true;
            }
            catch (PCSException ex)
            {
                // displays the error message.
                PCSMessageBox.Show(ex.mCode, MessageBoxIcon.Error);
                // log message.
                try
                {
                    Logger.LogMessage(ex.CauseException, methodName, Level.ERROR);
                }
                catch
                {
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                // displays the error message.
                PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxIcon.Error);
                // log message.
                try
                {
                    Logger.LogMessage(ex, methodName, Level.ERROR);
                }
                catch
                {
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
                }
            }
        }

        private void ShiftText_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F4 && ShiftButton.Enabled)
                ShiftButton_Click(null, null);
        }

        private void ShiftButton_Click(object sender, EventArgs e)
        {
            const string methodName = This + ".ShiftButton_Click()";
            try
            {
                var drvResult = FormControlComponents.OpenSearchForm(PRO_ShiftTable.TABLE_NAME, PRO_ShiftTable.SHIFTDESC_FLD, ShiftText.Text.Trim(), null, true);
                if (drvResult != null)
                    FillShift(drvResult, false);
                else
                    ShiftText.Focus();
            }
            catch (PCSException ex)
            {
                // displays the error message.
                PCSMessageBox.Show(ex.mCode, MessageBoxIcon.Error);
                // log message.
                try
                {
                    Logger.LogMessage(ex.CauseException, methodName, Level.ERROR);
                }
                catch
                {
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                // displays the error message.
                PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxIcon.Error);
                // log message.
                try
                {
                    Logger.LogMessage(ex, methodName, Level.ERROR);
                }
                catch
                {
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
                }
            }
        }

        private void SearchButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(MasterLocationText.Text.Trim()))
                {
                    PCSMessageBox.Show(ErrorCode.MESSAGE_RGA_MASTERLOCATION, MessageBoxIcon.Warning);
                    MasterLocationText.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(WorkOrderNoText.Text.Trim()))
                {
                    PCSMessageBox.Show(ErrorCode.MESSAGE_WOROUTING_INPUT_WORKORDER_FIRST, MessageBoxIcon.Warning);
                    WorkOrderNoText.Focus();
                    return;
                }
                if (FromDatePicker.Value == null)
                {
                    PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Warning);
                    FromDatePicker.Focus();
                    return;
                }
                if (ToDatePicker.Value == null)
                {
                    PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Warning);
                    ToDatePicker.Focus();
                    return;
                }
                var completionMenu = SystemProperty.TableMenuEntry.FirstOrDefault(menu => menu.FormLoad == "PCSProduction.WorkOrder.WorkOrderCompletion");
                if (completionMenu == null)
                {
                    PCSMessageBox.Show("User must have right to Work order completion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var postDate = (DateTime) PostDatePicker.Value;
                var toDate = (DateTime) ToDatePicker.Value;
                // to date cannot be next month
                var endMonth = new DateTime(_serverDate.Year, _serverDate.Month, 1).AddMonths(1).AddMilliseconds(-1);
                if (toDate > endMonth)
                {
                    PCSMessageBox.Show(string.Format("To Date must be smaller {0}", endMonth.ToString(Constants.DATETIME_FORMAT_HOUR)));
                    ToDatePicker.Focus();
                    return;
                }
                _lastPostDate = (DateTime)PostDatePicker.Value;
                toDate = toDate.AddDays(1).AddMilliseconds(-1);
                var boMultiCompletion = new MultiCompletionBO();
                string productCondition = new UtilsBO().GetConditionByRecord(SystemProperty.UserName, ITM_ProductTable.TABLE_NAME);
                _dataSource = boMultiCompletion.SearchWorkOrderLine((DateTime) FromDatePicker.Value,
                                                                    toDate,
                                                                    (int) MasterLocationText.Tag,
                                                                    (int) WorkOrderNoText.Tag,
                                                                    productCondition);
                // get the single completion number
                var completionNo = FormControlComponents.GetNoByMask(completionMenu.TableName, completionMenu.TransNoFieldName, completionMenu.Prefix, completionMenu.TransFormat, postDate);
                var transFormat = !string.IsNullOrEmpty(completionMenu.TransFormat)
                                 ? completionMenu.TransFormat
                                 : "YYYYMMDD####";
                var autoNumberFormat = transFormat.Substring(transFormat.IndexOf("#"));
                var prefix = !string.IsNullOrEmpty(completionMenu.Prefix)
                                 ? completionMenu.Prefix
                                 : string.Empty;
                var formatType = transFormat.Substring(0, transFormat.IndexOf("#"));
                var length = completionNo.IndexOf(prefix) >= 0 ? formatType.Length + prefix.Length : formatType.Length;
                var maxValue = Convert.ToInt32(completionNo.Substring(length));
                var postDateString = GetPostDateString(postDate, formatType.ToUpperInvariant());
                // fill work order completion number and completed date for each row
                int counter = 1;
                foreach (DataRow row in _dataSource.Rows)
                {
                    var productionLineId = 0;
                    if (row[PRO_WorkOrderMasterTable.PRODUCTIONLINEID_FLD] != DBNull.Value)
                    {
                        productionLineId = (int) row[PRO_WorkOrderMasterTable.PRODUCTIONLINEID_FLD];
                    }
                    var completionDate = GetCompletionDate(postDate, productionLineId);
                    row[PRO_WorkOrderCompletionTable.COMPLETEDDATE_FLD] = completionDate;
                    var transNo = GetCompletionNo(autoNumberFormat, prefix, postDateString, maxValue);
                    row[PRO_WorkOrderCompletionTable.WOCOMPLETIONNO_FLD] = transNo;
                    row["No"] = counter;
                    counter++;
                    maxValue++;
                }
                DetailGrid.Splits[0].Locked = false;
                FillDataGrid();
                DetailGrid.Focus();
            }
            catch (PCSException ex)
            {
                // displays the error message.
                PCSMessageBox.Show(ex.mCode, MessageBoxIcon.Error);
                _logger.Error(ex.CauseException.Message, ex.CauseException);
            }
            catch (Exception ex)
            {
                // displays the error message
                PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxIcon.Error);
                // log message.
                _logger.Error(ex.Message, ex);
            }
        }

        private void PostDatePicker_ValueChanged(object sender, EventArgs e)
        {
            // update from date and to date when post date changed
            if (PostDatePicker.Value != DBNull.Value)
            {
                var postDate = (DateTime)PostDatePicker.Value;
                if (postDate.Equals(_lastPostDate))
                {
                    return;
                }
                var fromDate = new DateTime(postDate.Year, postDate.Month, 1);
                FromDatePicker.Value = fromDate;
                var toDate = new DateTime(postDate.Year, postDate.Month, postDate.Day, 23, 59, 59);
                ToDatePicker.Value = toDate;
                FillWorkOrder(null);
                // clear data source of grid
                if (_dataSource != null)
                {
                    _dataSource.Clear();
                }
                _lastPostDate = (DateTime) PostDatePicker.Value;
                // refill the multi completion number based on new post date
                FillMultiCompletionNumber(_lastPostDate);
            }
        }

        private void BomShortageButton_Click(object sender, EventArgs e)
        {
            const string methodName = This + ".BomShortageButton_Click()";
            try
            {
                if (_formMode == EnumAction.Add && DetailGrid.RowCount > 0 && DetailGrid.Row >= 0 && DetailGrid.Row < DetailGrid.RowCount)
                    ShowBOMShortageReport();
            }
            catch (PCSException ex)
            {
                // displays the error message.
                PCSMessageBox.Show(ex.mCode, MessageBoxIcon.Error);
                // log message.
                try
                {
                    Logger.LogMessage(ex.CauseException, methodName, Level.ERROR);
                }
                catch
                {
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                // displays the error message.
                PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxIcon.Error);
                // log message.
                try
                {
                    Logger.LogMessage(ex, methodName, Level.ERROR);
                }
                catch
                {
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
                }
            }
        }

        private void DetailGrid_AfterColUpdate(object sender, ColEventArgs e)
        {
            const string methodName = This + ".DetailGrid_AfterColUpdate()";
            try
            {
                switch (e.Column.DataColumn.DataField)
                {
                    case MST_LocationTable.TABLE_NAME + MST_LocationTable.CODE_FLD:
                        FillLocationData(e.Column.DataColumn.Tag as DataRowView);
                        break;
                    case MST_BINTable.TABLE_NAME + MST_BINTable.CODE_FLD:
                        FillBinData(e.Column.DataColumn.Tag as DataRowView);
                        break;
                }
            }
            catch (PCSException ex)
            {
                // displays the error message.
                PCSMessageBox.Show(ex.mCode, MessageBoxIcon.Error);
                // log message.
                try
                {
                    Logger.LogMessage(ex.CauseException, methodName, Level.ERROR);
                }
                catch
                {
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                // displays the error message.
                PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxIcon.Error);
                // log message.
                try
                {
                    Logger.LogMessage(ex, methodName, Level.ERROR);
                }
                catch
                {
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
                }
            }
        }

        private void DetailGrid_BeforeColUpdate(object sender, BeforeColUpdateEventArgs e)
        {
            const string methodName = This + ".DetailGrid_BeforeColUpdate()";
            try
            {
                var filterValue = e.Column.DataColumn.Value.ToString();
                var condition = new Hashtable();
                DataRowView rowView;
                switch (e.Column.DataColumn.DataField)
                {
                    case MST_LocationTable.TABLE_NAME + MST_LocationTable.CODE_FLD:
                        if (string.IsNullOrEmpty(filterValue))
                        {
                            FillLocationData(null);
                            return;
                        }
                        if (MasterLocationText.Text.Trim() == string.Empty)
                        {
                            PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Warning);
                            MasterLocationText.Focus();
                            return;
                        }
                        condition.Add(MST_MasterLocationTable.MASTERLOCATIONID_FLD, MasterLocationText.Tag);
                        rowView = FormControlComponents.OpenSearchForm(MST_LocationTable.TABLE_NAME,
                                                                   MST_LocationTable.CODE_FLD, filterValue, condition,
                                                                   false);
                        if (rowView != null)
                            e.Column.DataColumn.Tag = rowView;
                        else
                            e.Cancel = true;
                        break;
                    case MST_BINTable.TABLE_NAME + MST_BINTable.CODE_FLD:
                        if (string.IsNullOrEmpty(filterValue))
                        {
                            FillBinData(null);
                        }
                        if (DetailGrid[DetailGrid.Row, MST_LocationTable.LOCATIONID_FLD] == null ||
                        DetailGrid[DetailGrid.Row, MST_LocationTable.LOCATIONID_FLD] == DBNull.Value ||
                        DetailGrid[DetailGrid.Row, MST_LocationTable.LOCATIONID_FLD].ToString() == string.Empty)
                        {
                            PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Warning);
                            FillBinData(null);
                            DetailGrid.Col = DetailGrid.Columns.IndexOf(DetailGrid.Columns[MST_LocationTable.TABLE_NAME + MST_LocationTable.CODE_FLD]);
                            DetailGrid.Focus();
                            return;
                        }
                        condition.Add(MST_LocationTable.LOCATIONID_FLD, DetailGrid[DetailGrid.Row, MST_LocationTable.LOCATIONID_FLD]);
                        rowView = FormControlComponents.OpenSearchForm(MST_BINTable.TABLE_NAME,
                                                                   MST_BINTable.CODE_FLD, filterValue, condition,
                                                                   false);
                        if (rowView != null)
                            e.Column.DataColumn.Tag = rowView;
                        else
                            e.Cancel = true;
                        break;
                    case PRO_WorkOrderCompletionTable.COMPLETEDQUANTITY_FLD:
                        decimal completedQuantity;
                        if (!decimal.TryParse(filterValue, out completedQuantity))
                        {
                            PCSMessageBox.Show(ErrorCode.MESSAGE_INVALID_NUMERIC, MessageBoxIcon.Exclamation);
                            DetailGrid.Col = DetailGrid.Splits[0].DisplayColumns.IndexOf(DetailGrid.Splits[0].DisplayColumns[PRO_WorkOrderCompletionTable.COMPLETEDQUANTITY_FLD]);
                            DetailGrid.Focus();
                            return;
                        }
                        if (completedQuantity == 0)
                        {
                            PCSMessageBox.Show(ErrorCode.MESSAGE_WOCOMPLETION_COMPQTY_MUST_BE_GREATER_ZERO,
                                           MessageBoxIcon.Exclamation);
                            DetailGrid.Col = DetailGrid.Splits[0].DisplayColumns.IndexOf(DetailGrid.Splits[0].DisplayColumns[PRO_WorkOrderCompletionTable.COMPLETEDQUANTITY_FLD]);
                            DetailGrid.Focus();
                            return;
                        }
                        break;
                }
            }
            catch (PCSException ex)
            {
                // displays the error message.
                PCSMessageBox.Show(ex.mCode, MessageBoxIcon.Error);
                // log message.
                try
                {
                    Logger.LogMessage(ex.CauseException, methodName, Level.ERROR);
                }
                catch
                {
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                // displays the error message.
                PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxIcon.Error);
                // log message.
                try
                {
                    Logger.LogMessage(ex, methodName, Level.ERROR);
                }
                catch
                {
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
                }
            }
        }

        private void DetailGrid_ButtonClick(object sender, ColEventArgs e)
        {
            const string methodName = This + ".DetailGrid_ButtonClick()";
            try
            {
                if (!DetailGrid.Splits[0].DisplayColumns[DetailGrid.Col].Button)
                    return;
                if (MasterLocationText.Text.Trim() == string.Empty)
                {
                    PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Warning);
                    DetailGrid.Columns[DetailGrid.Col].Text = string.Empty;
                    DetailGrid.Columns[DetailGrid.Col].Value = string.Empty;
                    MasterLocationText.Focus();
                    return;
                }
                var condition = new Hashtable();
                DataRowView rowView;
                var filterValue = DetailGrid[DetailGrid.Row, DetailGrid.Col].ToString();
                if (DetailGrid.Columns[DetailGrid.Col].DataField == MST_LocationTable.TABLE_NAME + MST_LocationTable.CODE_FLD)
                {
                    condition.Add(MST_LocationTable.MASTERLOCATIONID_FLD, MasterLocationText.Tag);
                    rowView = FormControlComponents.OpenSearchForm(MST_LocationTable.TABLE_NAME,
                                                                   MST_LocationTable.CODE_FLD, filterValue, condition,
                                                                   true);
                    if (rowView != null)
                        FillLocationData(rowView);
                    else
                    {
                        DetailGrid.Col = DetailGrid.Splits[0].DisplayColumns.IndexOf(DetailGrid.Splits[0].DisplayColumns[MST_LocationTable.TABLE_NAME + MST_LocationTable.CODE_FLD]);
                        DetailGrid.Focus();
                    }
                }
                if (DetailGrid.Columns[DetailGrid.Col].DataField == MST_BINTable.TABLE_NAME + MST_BINTable.CODE_FLD)
                {
                    if (DetailGrid[DetailGrid.Row, MST_LocationTable.LOCATIONID_FLD] == null ||
                        DetailGrid[DetailGrid.Row, MST_LocationTable.LOCATIONID_FLD] == DBNull.Value ||
                        DetailGrid[DetailGrid.Row, MST_LocationTable.LOCATIONID_FLD].ToString() == string.Empty)
                    {
                        PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Warning);
                        DetailGrid.Columns[DetailGrid.Col].Text = string.Empty;
                        DetailGrid.Columns[DetailGrid.Col].Value = string.Empty;
                        DetailGrid.Col = DetailGrid.Columns.IndexOf(DetailGrid.Columns[MST_LocationTable.TABLE_NAME + MST_LocationTable.CODE_FLD]);
                        DetailGrid.Focus();
                        return;
                    }
                    condition.Add(MST_LocationTable.LOCATIONID_FLD, DetailGrid[DetailGrid.Row, MST_LocationTable.LOCATIONID_FLD]);
                    rowView = FormControlComponents.OpenSearchForm(MST_BINTable.TABLE_NAME,
                                                                   MST_BINTable.CODE_FLD, filterValue, condition,
                                                                   true);
                    if (rowView != null)
                        FillBinData(rowView);
                    else
                    {
                        DetailGrid.Col = DetailGrid.Splits[0].DisplayColumns.IndexOf(DetailGrid.Splits[0].DisplayColumns[MST_BINTable.TABLE_NAME + MST_BINTable.CODE_FLD]);
                        DetailGrid.Focus();
                    }
                }
            }
            catch (PCSException ex)
            {
                // displays the error message.
                PCSMessageBox.Show(ex.mCode, MessageBoxIcon.Error);
                // log message.
                try
                {
                    Logger.LogMessage(ex.CauseException, methodName, Level.ERROR);
                }
                catch
                {
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                // displays the error message.
                PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxIcon.Error);
                // log message.
                try
                {
                    Logger.LogMessage(ex, methodName, Level.ERROR);
                }
                catch
                {
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
                }
            }
        }

        private void DetailGrid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F4 && DetailGrid.Splits[0].DisplayColumns[DetailGrid.Col].Button)
            {
                DetailGrid_ButtonClick(sender, null);
            }
            else if (e.KeyCode == Keys.Delete && _formMode == EnumAction.Add)
            {
                FormControlComponents.DeleteMultiRowsOnTrueDBGrid(DetailGrid);
            }
        }
    }
}