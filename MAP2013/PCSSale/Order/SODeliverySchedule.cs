using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using C1.Win.C1Input;
using C1.Win.C1TrueDBGrid;
using PCSComSale.Order.BO;
using PCSComSale.Order.DS;
using PCSComUtils.Common;
using PCSComUtils.PCSExc;
using PCSUtils.Log;
using PCSUtils.Utils;
using CancelEventArgs = System.ComponentModel.CancelEventArgs;

namespace PCSSale.Order
{
    /// <summary>
    /// Summary description for SODeliverySchedule.
    /// </summary>
    public class SODeliverySchedule : Form
    {
        private const string THIS = "PCSSale.Order.SODeliverySchedule";
        private const string CONVERT_DATE_TOSTRING = "d";

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private readonly Container components;

        private readonly SaleOrderInformationVO objSaleOrderInformationVO;
        private bool blnAddNewByF12 = true;
        private Button btnClose;
        private Button btnDelete;
        private Button btnHelp;
        private Button btnPrint;
        private Button btnSOATP;
        private Button btnSave;
        private C1TrueDBGrid dgrdData;
        private DataSet dsDeliverySchedule;
        private DataTable dtbGridLayout;
        private C1DateEdit dtmDate;
        private EnumAction enumAction;
        private Label label1;
        private Label label10;

        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label label9;
        private C1NumericEdit lblOrderQuantity;
        private C1NumericEdit lblTotalDelivery;
        private TextBox txtCCN;

        private TextBox txtLine;
        private TextBox txtModel;
        private TextBox txtPartName;
        private TextBox txtPartNumber;
        private TextBox txtSONo;
        private TextBox txtUnit;

        public SODeliverySchedule()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
        }

        public SODeliverySchedule(object pobjSaleOrder)
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            objSaleOrderInformationVO = (SaleOrderInformationVO) pobjSaleOrder;
        }


        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        private void LoadDeliverySchedule(int intSaleOrderLineID)
        {
            var objSODeliveryScheduleBO = new SODeliveryScheduleBO();
            dsDeliverySchedule = objSODeliveryScheduleBO.GetDeliverySchedule(intSaleOrderLineID);
            dgrdData.DataSource = dsDeliverySchedule.Tables[0];
            FormControlComponents.RestoreGridLayout(dgrdData, dtbGridLayout);
            dgrdData.AllowUpdate = true;
            if (dsDeliverySchedule.Tables[0].Rows.Count != 0)
            {
                foreach (C1DisplayColumn dcol in dgrdData.Splits[0].DisplayColumns)
                {
                    dcol.Locked = true;
                }
            }

            //Set the read only for the LINE Column and Delivery No
            dgrdData.Splits[0].DisplayColumns[SO_DeliveryScheduleTable.LINE_FLD].Locked = true;
            dgrdData.Splits[0].DisplayColumns["SUMCommitQuantity"].Locked = true;

            //Align center for date
            dgrdData.Splits[0].DisplayColumns[SO_DeliveryScheduleTable.REQUIREDDATE_FLD].Style.HorizontalAlignment =
                AlignHorzEnum.Center;
            dgrdData.Splits[0].DisplayColumns[SO_DeliveryScheduleTable.PROMISEDATE_FLD].Style.HorizontalAlignment =
                AlignHorzEnum.Center;
            dgrdData.Splits[0].DisplayColumns[SO_DeliveryScheduleTable.SCHEDULEDATE_FLD].Style.HorizontalAlignment =
                AlignHorzEnum.Center;

            //align right for Line, Delivery Quantity, Delivery No
            dgrdData.Splits[0].DisplayColumns[SO_DeliveryScheduleTable.LINE_FLD].Style.HorizontalAlignment =
                AlignHorzEnum.Far;
            dgrdData.Splits[0].DisplayColumns[SO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD].Style.HorizontalAlignment =
                AlignHorzEnum.Far;
            dgrdData.Splits[0].DisplayColumns["SUMCommitQuantity"].Style.HorizontalAlignment = AlignHorzEnum.Far;
            dgrdData.Splits[0].DisplayColumns[SO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD].HeadingStyle.ForeColor =
                Color.Maroon;
            dgrdData.Splits[0].DisplayColumns[SO_DeliveryScheduleTable.SCHEDULEDATE_FLD].HeadingStyle.ForeColor =
                Color.Maroon;
            //add button
            dgrdData.Splits[0].DisplayColumns[SO_GateTable.CODE_FLD].Button = true;
            dgrdData.Columns["SUMCommitQuantity"].NumberFormat = Constants.DECIMAL_NUMBERFORMAT;
            dgrdData.Columns[SO_DeliveryScheduleTable.REQUIREDDATE_FLD].NumberFormat = Constants.DATETIME_FORMAT_HOUR;
            dgrdData.Columns[SO_DeliveryScheduleTable.PROMISEDATE_FLD].NumberFormat = Constants.DATETIME_FORMAT_HOUR;
            dgrdData.Splits[0].DisplayColumns[SO_DeliveryScheduleTable.SCHEDULEDATE_FLD].DataColumn.Editor = dtmDate;
            dgrdData.Columns[SO_DeliveryScheduleTable.SCHEDULEDATE_FLD].NumberFormat = Constants.DATETIME_FORMAT_HOUR;
            //Unlock some columns 
            dgrdData.Splits[0].DisplayColumns[SO_GateTable.CODE_FLD].Locked = false;
            dgrdData.Splits[0].DisplayColumns[SO_DeliveryScheduleTable.SCHEDULEDATE_FLD].Locked = false;
            dgrdData.Splits[0].DisplayColumns[SO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD].Locked = false;
            //Set the Delivery Quantity
            dgrdData.Columns[SO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD].NumberFormat =
                Constants.DECIMAL_NUMBERFORMAT;

            //Count the total of Delivery Quantity
            lblTotalDelivery.Value = SumTotalDeliveryQuantity().ToString();
        }

        private double SumTotalDeliveryQuantity()
        {
            const string METHOD_NAME = THIS + ".SumTotalDeliveryQuantity()";
            try
            {
                int intGridRows = dgrdData.RowCount;

                // now compute the number of unique values for the country and city columns
                double dblTotalValue = 0;
                for (int i = 0; i < intGridRows; i++)
                {
                    try
                    {
                        dblTotalValue +=
                            double.Parse(dgrdData[i, SO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD].ToString());
                    }
                    catch
                    {
                        dblTotalValue += 0;
                    }
                }
                return dblTotalValue;
            }
            catch (Exception ex)
            {
                // displays the error message.
                PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxIcon.Error);
                // log message.
                try
                {
                    Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
                }
                catch
                {
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
                }
                return 0;
            }
        }

        private bool IsDeleteAllSchedule()
        {
            //const string METHOD_NAME = THIS + ".CheckBeforeDeleteAllSchedule()";
//			try 
//			{
            int intGridRows = dgrdData.RowCount;

            for (int i = 0; i < intGridRows; i++)
            {
                string strCommit = dgrdData[i, "SUMCommitQuantity"].ToString().Trim();
                if (strCommit != String.Empty)
                {
                    try
                    {
                        if (int.Parse(strCommit) > 0)
                        {
                            return false;
                        }
                    }
                    catch
                    {
                    }
                }
            }
            return true;
//			}
//			catch (Exception ex)
//			{
//				throw ex;
//			}
        }

        private void DisplaySaleOrderInformation()
        {
            txtSONo.Text = objSaleOrderInformationVO.SaleOrderNo;
            txtLine.Text = objSaleOrderInformationVO.SaleOrderLine.ToString();
            txtPartNumber.Text = objSaleOrderInformationVO.ProductCode;
            txtModel.Text = objSaleOrderInformationVO.ProductRevision;
            txtPartName.Text = objSaleOrderInformationVO.ProductDescription;
            txtUnit.Text = objSaleOrderInformationVO.UnitCode;
            txtCCN.Text = objSaleOrderInformationVO.CCNCode;
            lblOrderQuantity.Value = objSaleOrderInformationVO.OrderQuantity.ToString();
        }

        private void SODeliverySchedule_Load(object sender, EventArgs e)
        {
            if (objSaleOrderInformationVO == null || objSaleOrderInformationVO.SaleOrderDetailID <= 0)
            {
                Close();
                return;
            }

            const string METHOD_NAME = THIS + ".SODeliverySchedule_Load()";
            try
            {
                //Set authorization for user
                var objSecurity = new Security();
                Name = THIS;

                if (objSecurity.SetRightForUserOnForm(this, SystemProperty.UserName) == 0)
                {
                    // You don't have the right to view this item
                    PCSMessageBox.Show(ErrorCode.MESSAGE_YOU_HAVE_NO_RIGHT_TO_VIEW, MessageBoxIcon.Warning);
                    Close();
                    return;
                }

                //added by duongna
                dtbGridLayout = FormControlComponents.StoreGridLayout(dgrdData);
                dtmDate.CustomFormat = Constants.DATETIME_FORMAT_HOUR;
                lblOrderQuantity.CustomFormat = Constants.DECIMAL_NUMBERFORMAT;
                lblTotalDelivery.CustomFormat = Constants.DECIMAL_NUMBERFORMAT;

                enumAction = new EnumAction();
                enumAction = EnumAction.Default;

                //Display Sale Order Information
                DisplaySaleOrderInformation();

                //Load the Delivery Detail data
                LoadDeliverySchedule(objSaleOrderInformationVO.SaleOrderDetailID);

                //Diable or Enable buttons
                //EnableDisableButtons();

                FormControlComponents.RestoreGridLayout(dgrdData, dtbGridLayout);
            }
            catch (PCSException ex)
            {
                // displays the error message.
                PCSMessageBox.Show(ex.mCode, MessageBoxIcon.Error);
                // log message.
                try
                {
                    Logger.LogMessage(ex.CauseException, METHOD_NAME, Level.ERROR);
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
                    Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
                }
                catch
                {
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
                }
            }
        }

        private bool SaveToDatabase()
        {
            const string METHOD_NAME = THIS + ".SaveToDatabase()";
            try
            {
                int intRowsCount = GetRowsCount();
                for (int i = 0; i < intRowsCount; i++)
                {
                    if (dgrdData[i, SO_DeliveryScheduleTable.SCHEDULEDATE_FLD].ToString() == string.Empty)
                    {
                        PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Warning);
                        dgrdData.Row = i;
                        dgrdData.Col =
                            dgrdData.Columns.IndexOf(dgrdData.Columns[PO_DeliveryScheduleTable.SCHEDULEDATE_FLD]);
                        dgrdData.Focus();
                        return false;
                    }
                    if (dgrdData[i, SO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD].ToString() == string.Empty)
                    {
                        PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Warning);
                        dgrdData.Row = i;
                        dgrdData.Col =
                            dgrdData.Columns.IndexOf(dgrdData.Columns[PO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD]);
                        dgrdData.Focus();
                        return false;
                    }
                }

                //Check the business 
                double dblTotalDeliveryQuantity = SumTotalDeliveryQuantity();
                lblTotalDelivery.Value = dblTotalDeliveryQuantity.ToString();
                if (dblTotalDeliveryQuantity > double.Parse(lblOrderQuantity.Text))
                {
                    //Display message here
                    //MessageBox.Show("The total delivery Quantity must be less than or equal to Order Quantity");
                    PCSMessageBox.Show(ErrorCode.MESSAGE_OVER_DELIVERYQTY, MessageBoxIcon.Error);
                    return false;
                }

                //Check promise date, schedule date, and require date
                var objSODeliveryScheduleBO = new SODeliveryScheduleBO();
                FormControlComponents.SynchronyGridData(dgrdData);
                objSODeliveryScheduleBO.UpdateDeliveryDataSet(dsDeliverySchedule,
                                                              objSaleOrderInformationVO.SaleOrderDetailID);
                enumAction = EnumAction.Default;
                dgrdData.Refresh();
                LoadDeliverySchedule(objSaleOrderInformationVO.SaleOrderDetailID);

                //re calculate the total Delivery quantity
                lblTotalDelivery.Value = SumTotalDeliveryQuantity().ToString();

                return true;
            }
            catch (PCSException ex)
            {
                // displays the error message.
                PCSMessageBox.Show(ex.mCode, MessageBoxIcon.Error);
                // log message.
                try
                {
                    Logger.LogMessage(ex.CauseException, METHOD_NAME, Level.ERROR);
                }
                catch
                {
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
                }
                return false;
            }
            catch (Exception ex)
            {
                // displays the error message.
                PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxIcon.Error);
                // log message.
                try
                {
                    Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
                }
                catch
                {
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
                }
                return false;
            }
        }

        private void SODeliverySchedule_Closing(object sender, CancelEventArgs e)
        {
            dgrdData.UpdateData();
            if (enumAction == EnumAction.Add || enumAction == EnumAction.Edit)
            {
                DialogResult dlgResult = PCSMessageBox.Show(ErrorCode.MESSAGE_QUESTION_STORE_INTO_DATABASE,
                                                            MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                switch (dlgResult)
                {
                    case DialogResult.Yes:
                        if (!SaveToDatabase())
                        {
                            e.Cancel = true;
                        }
                        break;
                    case DialogResult.No:
                        e.Cancel = false;
                        break;
                    case DialogResult.Cancel:
                        e.Cancel = true;
                        break;
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            const string METHOD_NAME = THIS + ".btnSave_Click()";
            try
            {
                if (dgrdData.EditActive) return;
                if (SaveToDatabase())
                {
                    enumAction = EnumAction.Default;
                    PCSMessageBox.Show(ErrorCode.MESSAGE_AFTER_SAVE_DATA, MessageBoxIcon.Information);
                }
            }
            catch (PCSDBException ex)
            {
                // displays the error message.
                PCSMessageBox.Show(ex.mCode, MessageBoxIcon.Error);
                // log message.
                try
                {
                    Logger.LogMessage(ex.CauseException, METHOD_NAME, Level.ERROR);
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
                    Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
                }
                catch
                {
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Check deleting in case selecting multiplies rows
        /// </summary>
        /// <returns></returns>
        private bool CheckBeforeDeleteAllRows()
        {
//			try 
//			{
            for (int i = 0; i < dgrdData.SelectedRows.Count; i++)
            {
                if (
                    dgrdData[
                        int.Parse(dgrdData.SelectedRows[i].ToString()),
                        SO_CommitInventoryDetailTable.COMMITINVENTORYDETAILID_FLD].ToString().Trim() != String.Empty)
                {
                    return false;
                }
            }

            return true;
//			}
//			catch (Exception ex)
//			{
//				throw ex;
//			}
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            const string METHOD_NAME = THIS + ".btnDelete_Click()";
            DialogResult result;
            if (dgrdData.EditActive) return;
            result = PCSMessageBox.Show(ErrorCode.MESSAGE_DELETE_RECORD, MessageBoxButtons.YesNo,
                                        MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                try
                {
                    if (!IsDeleteAllSchedule())
                    {
                        PCSMessageBox.Show(ErrorCode.MESSAGE_CANNOT_DELDELIVERY, MessageBoxIcon.Error);
                        return;
                    }
                    var objSODeliveryScheduleBO = new SODeliveryScheduleBO();
                    objSODeliveryScheduleBO.DeleteDeliveryDetail(objSaleOrderInformationVO.SaleOrderDetailID);
                    LoadDeliverySchedule(objSaleOrderInformationVO.SaleOrderDetailID);
                    enumAction = EnumAction.Default;
                    //lblTotalDelivery.Text = SumTotalDeliveryQuantity().ToString();
                }
                catch (PCSDBException ex)
                {
                    // displays the error message.
                    PCSMessageBox.Show(ex.mCode, MessageBoxIcon.Error);
                    // log message.
                    try
                    {
                        Logger.LogMessage(ex.CauseException, METHOD_NAME, Level.ERROR);
                    }
                    catch
                    {
                        PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
                    }
                }

                catch (Exception ex)
                {
                    PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxIcon.Error);
                    // log message.
                    try
                    {
                        Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
                    }
                    catch
                    {
                        PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            PCSMessageBox.Show(ErrorCode.NOT_IMPLEMENT);
        }

        private void btnSOATP_Click(object sender, EventArgs e)
        {
            PCSMessageBox.Show(ErrorCode.NOT_IMPLEMENT);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnHelp_Click(object sender, EventArgs e)
        {
        }

        private void dgrdData_BeforeDelete(object sender, C1.Win.C1TrueDBGrid.CancelEventArgs e)
        {
            const string METHOD_NAME = THIS + ".dgrdData_BeforeDelete()";
            try
            {
                if (!CheckBeforeDeleteAllRows())
                {
                    e.Cancel = true;
                    return;
                }
            }
            catch (PCSException ex)
            {
                // Displays the error message if throwed from PCSException.
                PCSMessageBox.Show(ex.mCode, MessageBoxIcon.Error);
                try
                {
                    // Log error message into log file.
                    Logger.LogMessage(ex.CauseException, METHOD_NAME, Level.ERROR);
                }
                catch
                {
                    // Show message if logger has an error.
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                // Displays the error message if throwed from system.
                PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxIcon.Error);
                try
                {
                    // Log error message into log file.
                    Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
                }
                catch
                {
                    // Show message if logger has an error.
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
                }
            }
        }

        private void dgrdData_BeforeColUpdate(object sender, BeforeColUpdateEventArgs e)
        {
            const string METHOD_NAME = THIS + ".dgrdData_BeforeColEdit()";
            try
            {
                //gate column
                if (e.Column.DataColumn.DataField == SO_GateTable.CODE_FLD)
                {
                    DataRowView drwResult = null;
                    if (dgrdData.Columns[e.Column.DataColumn.DataField].Text.Trim() != string.Empty)
                    {
                        drwResult = FormControlComponents.OpenSearchForm(SO_GateTable.TABLE_NAME, SO_GateTable.CODE_FLD,
                                                                         dgrdData.Columns[e.Column.DataColumn.DataField]
                                                                             .Text.Trim(), null, false);
                        if (drwResult != null)
                        {
                            e.Column.DataColumn.Tag = drwResult.Row;
                        }
                        else
                        {
                            e.Cancel = true;
                        }
                    }
                }
                //first check the Delivery Quantity 
                string strValue = String.Empty;
                if (e.Column.DataColumn.DataField == SO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD)
                {
                    //Delivery Quantity must be higher 0
                    double dblDeliveryQuantity = 0;
                    if (e.Column.DataColumn.Value.ToString().Trim() == String.Empty)
                    {
                        dblDeliveryQuantity = 0;
                    }
                    else
                    {
                        try
                        {
                            dblDeliveryQuantity = double.Parse(e.Column.DataColumn.Value.ToString());
                        }
                        catch
                        {
                            dblDeliveryQuantity = 0;
                        }
                    }

                    double dblOldValue;
                    if (e.OldValue.ToString().Trim() != String.Empty)
                    {
                        dblOldValue = double.Parse(e.OldValue.ToString());
                    }
                    else
                    {
                        dblOldValue = 0;
                    }

                    double dblRemainingQty = double.Parse(lblOrderQuantity.Text) + dblOldValue -
                                             double.Parse(lblTotalDelivery.Text);
                    if ((dblDeliveryQuantity == 0) && (e.Column.DataColumn.Value.ToString().Trim() == String.Empty))
                    {
                        return;
                    }
                    if (!(dblDeliveryQuantity > 0 && dblDeliveryQuantity <= dblRemainingQty))
                    {
                        //MessageBox.Show("The Delivery Quantity must be higher than 0");
                        PCSMessageBox.Show(ErrorCode.MESSAGE_MIN_DELIVERYQTY, MessageBoxIcon.Warning);
                        e.Cancel = true;
                        return;
                    }
//					if(dgrdData[dgrdData.Row,dgrdData.Col] != string.Empty)
//					{
//						PCSMessageBox.Show(ErrorCode.MESSAGE_SO_HAS_BEEN_RELEASED,MessageBoxIcon.Warning,new string[]{ROW});
//						e.Cancel = true;
//						return;
//					}
                }
                else
                {
                    if (e.Column.DataColumn.DataField == SO_DeliveryScheduleTable.SCHEDULEDATE_FLD)
                    {
                        if (dtmDate.Value.ToString() == string.Empty) return;
                        var dtOrderDate = new DateTime(objSaleOrderInformationVO.OrderDate.Year,
                                                       objSaleOrderInformationVO.OrderDate.Month,
                                                       objSaleOrderInformationVO.OrderDate.Day);
                        if ((DateTime) dtmDate.Value < dtOrderDate)
                        {
                            PCSMessageBox.Show(ErrorCode.MESSAGE_CHECK_ORDERDATE, MessageBoxIcon.Error);
                            e.Cancel = true;
                        }
                        else
                        {
                            //check the period
                            if (!FormControlComponents.CheckDateInCurrentPeriod((DateTime) dtmDate.Value))
                            {
                                e.Column.DataColumn.Value = dtmDate.Value;
                                PCSMessageBox.Show(ErrorCode.MESSAGE_RTG_ENTRYDATE, MessageBoxIcon.Warning);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxIcon.Error);
                // log message.
                try
                {
                    Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
                }
                catch
                {
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
                }
            }
        }

        private void dgrdData_BeforeColEdit(object sender, BeforeColEditEventArgs e)
        {
            const string METHOD_NAME = THIS + ".dgrdData_BeforeColEdit()";
            try
            {
                if (e.Column.DataColumn.DataField == SO_GateTable.CODE_FLD)
                {
                    if (dgrdData.Row == dgrdData.RowCount - 1)
                    {
                        if (lblTotalDelivery.Text.Trim() == lblOrderQuantity.Text.Trim()
                            || (decimal.Parse(lblOrderQuantity.Text) - decimal.Parse(lblTotalDelivery.Text) <= 0))
                        {
                            //MessageBox.Show("There are enought order quantity, you cannot add more row");
                            PCSMessageBox.Show(ErrorCode.MESSAGE_ENOUGH_DELIVERYQTY, MessageBoxIcon.Warning);
                            dtmDate.TextDetached = true;
                            e.Cancel = true;
                            return;
                        }
                        if (lblOrderQuantity.Text.Trim() == lblTotalDelivery.Text.Trim())
                        {
                            //MessageBox.Show("There are no enough quantiy for new order delivery ");
                            PCSMessageBox.Show(ErrorCode.MESSAGE_ENOUGH_DELIVERYQTY, MessageBoxIcon.Warning);
                            dtmDate.TextDetached = true;
                            e.Cancel = true;
                        }
                    }
                }
                string strCommit =
                    dgrdData[dgrdData.Row, SO_CommitInventoryDetailTable.COMMITINVENTORYDETAILID_FLD].ToString().Trim();

                if (strCommit == String.Empty || int.Parse(strCommit) == 0)
                {
                    e.Cancel = false;
                    return;
                }
//				else
//				{
//					e.Cancel = true;
//				}
            }
            catch (Exception ex)
            {
                PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxIcon.Error);
                // log message.
                try
                {
                    Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
                }
                catch
                {
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
                }
            }
        }

        private void dgrdData_OnAddNew(object sender, EventArgs e)
        {
            const string METHOD_NAME = THIS + ".dgrdData_OnAddNew()";
            try
            {
                CurrencyManager cm;

                //dgrdData.Refresh();
                dgrdData.MoveLast();
                //dgrdData.Row = dgrdData.Row + 1;

                cm = (CurrencyManager) dgrdData.BindingContext[dgrdData.DataSource];
                cm.EndCurrentEdit();
                cm.AddNew();
                dtmDate.TextDetached = false;
                AssignDefaultValue();
            }
            catch (PCSException ex)
            {
                // Displays the error message if throwed from PCSException.
                PCSMessageBox.Show(ex.mCode, MessageBoxIcon.Error);
                try
                {
                    // Log error message into log file.
                    Logger.LogMessage(ex.CauseException, METHOD_NAME, Level.ERROR);
                }
                catch
                {
                    // Show message if logger has an error.
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                // Displays the error message if throwed from system.
                PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxIcon.Error);
                try
                {
                    // Log error message into log file.
                    Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
                }
                catch
                {
                    // Show message if logger has an error.
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
                }
            }
        }

        private void AssignDefaultValue()
        {
            const string METHOD_NAME = THIS + ".AssignDefaultValue()";
            try
            {
                dgrdData[dgrdData.Row, SO_DeliveryScheduleTable.LINE_FLD] = GetRowsCount() + 1;
                dgrdData[dgrdData.Row, SO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD] =
                    decimal.Parse(lblOrderQuantity.Text) - decimal.Parse(lblTotalDelivery.Text);
                dgrdData[dgrdData.Row, SO_DeliveryScheduleTable.REQUIREDDATE_FLD] =
                    objSaleOrderInformationVO.OrderDate.ToString(CONVERT_DATE_TOSTRING);
                dgrdData[dgrdData.Row, SO_DeliveryScheduleTable.SCHEDULEDATE_FLD] =
                    objSaleOrderInformationVO.OrderDate.ToString(CONVERT_DATE_TOSTRING);
                dgrdData[dgrdData.Row, SO_DeliveryScheduleTable.SALEORDERDETAILID_FLD] =
                    objSaleOrderInformationVO.SaleOrderDetailID;
                lblTotalDelivery.Value = objSaleOrderInformationVO.OrderQuantity.ToString();
                enumAction = EnumAction.Edit;
            }
            catch (Exception ex)
            {
                PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxIcon.Error);
                // log message.
                try
                {
                    Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
                }
                catch
                {
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
                }
            }
        }

        private void dgrdData_AfterColUpdate(object sender, ColEventArgs e)
        {
            const string METHOD_NAME = THIS + ".dgrdData_AfterColUpdate()";
            try
            {
                var drwResult = (DataRow) e.Column.DataColumn.Tag;
                //Fill Data to ComNumber
                if (e.Column.DataColumn.DataField == SO_GateTable.CODE_FLD)
                {
                    if ((e.Column.DataColumn.Tag == null) ||
                        (dgrdData[dgrdData.Row, SO_GateTable.CODE_FLD].ToString() == string.Empty))
                    {
                        dgrdData.Columns[SO_GateTable.CODE_FLD].Value = string.Empty;
                        dgrdData.Columns[SO_GateTable.GATEID_FLD].Value = null;
                    }
                    else
                    {
                        dgrdData.EditActive = true;
                        FillItemDataToGrid(drwResult);
                    }
                }
                if (e.Column.DataColumn.DataField == SO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD)
                {
                    //Re calculate the total delivery quantity
                    lblTotalDelivery.Value = SumTotalDeliveryQuantity().ToString();
                }
                enumAction = EnumAction.Edit;
            }
            catch (Exception ex)
            {
                PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxIcon.Error);
                // log message.
                try
                {
                    Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
                }
                catch
                {
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
                }
            }
        }

        private void ChangeLineNumber()
        {
//			try 
//			{
            int intGridRows = GetRowsCount();
            for (int i = 0; i < intGridRows; i++)
            {
                dgrdData[i, SO_DeliveryScheduleTable.LINE_FLD] = i + 1;
            }
            dgrdData.Refresh();
//			}
//			catch (Exception ex)
//			{
//				throw ex;
//			}
        }

        private void dgrdData_AfterDelete(object sender, EventArgs e)
        {
            //re calculate the total delivery quantity
            const string METHOD_NAME = THIS + ".dgrdData_AfterDelete()";
            try
            {
                lblTotalDelivery.Value = SumTotalDeliveryQuantity().ToString();
                dgrdData.Update();
                ChangeLineNumber();
                enumAction = EnumAction.Edit;
            }
            catch (Exception ex)
            {
                PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxIcon.Error);
                // log message.
                try
                {
                    Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
                }
                catch
                {
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
                }
            }
        }

        private void dgrdData_AfterUpdate(object sender, EventArgs e)
        {
            const string METHOD_NAME = THIS + ".dgrdData_AfterUpdate()";
            try
            {
                lblTotalDelivery.Value = SumTotalDeliveryQuantity().ToString();
                enumAction = EnumAction.Edit;
            }
            catch (Exception ex)
            {
                PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxIcon.Error);
                // log message.
                try
                {
                    Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
                }
                catch
                {
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
                }
            }
        }

        private void SODeliverySchedule_KeyDown(object sender, KeyEventArgs e)
        {
            const string METHOD_NAME = THIS + ".SODeliverySchedule_KeyDown()";
            if (e.KeyCode == Keys.F4 && btnSave.Enabled)
            {
            }
            if (e.KeyCode == Keys.F12)
            {
                try
                {
                    //add a new row
                    dgrdData.UpdateData();
                    if (dgrdData.AllowAddNew)
                    {
                        if (lblTotalDelivery.Text.Trim() == lblOrderQuantity.Text.Trim()
                            || (decimal.Parse(lblOrderQuantity.Text) - decimal.Parse(lblTotalDelivery.Text) <= 0))
                        {
                            //MessageBox.Show("There are enought order quantity, you cannot add more row");
                            PCSMessageBox.Show(ErrorCode.MESSAGE_ENOUGH_DELIVERYQTY, MessageBoxIcon.Warning);
                            return;
                        }
                        CurrencyManager cm;

                        //dgrdData.Refresh();
                        dgrdData.MoveLast();
                        //dgrdData.Row = dgrdData.Row + 1;

                        cm = (CurrencyManager) dgrdData.BindingContext[dgrdData.DataSource];
                        cm.EndCurrentEdit();
                        cm.AddNew();
                        AssignDefaultValue();

                        //focus on the first cell
                        dgrdData.Col = 2;
                    }
                }
                catch (NoNullAllowedException ex)
                {
                    PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID);
                    // log message.
                    try
                    {
                        Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
                    }
                    catch
                    {
                        PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
                    }
                }
                catch (ConstraintException ex)
                {
                    PCSMessageBox.Show(ErrorCode.DUPLICATE_KEY, MessageBoxIcon.Error);
                    // log message.
                    try
                    {
                        Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
                    }
                    catch
                    {
                        PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
                    }
                }

                catch (Exception ex)
                {
                    PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxIcon.Error);
                    // log message.
                    try
                    {
                        Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
                    }
                    catch
                    {
                        PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void dgrdData_BeforeInsert(object sender, C1.Win.C1TrueDBGrid.CancelEventArgs e)
        {
            const string METHOD_NAME = THIS + ".dgrdData_BeforeInsert()";
            try
            {
                blnAddNewByF12 = false;
                if (lblTotalDelivery.Text.Trim() == lblOrderQuantity.Text.Trim()
                    || (decimal.Parse(lblOrderQuantity.Text) - decimal.Parse(lblTotalDelivery.Text) <= 0))
                {
                    //MessageBox.Show("There are enought order quantity, you cannot add more row");
                    PCSMessageBox.Show(ErrorCode.MESSAGE_ENOUGH_DELIVERYQTY, MessageBoxIcon.Warning);
                    dtmDate.TextDetached = true;
                    e.Cancel = true;
                    return;
                }
                if (lblOrderQuantity.Text.Trim() == lblTotalDelivery.Text.Trim())
                {
                    //MessageBox.Show("There are no enough quantiy for new order delivery ");
                    PCSMessageBox.Show(ErrorCode.MESSAGE_ENOUGH_DELIVERYQTY, MessageBoxIcon.Warning);
                    dtmDate.TextDetached = true;
                    e.Cancel = true;
                }
            }
            catch (PCSException ex)
            {
                // Displays the error message if throwed from PCSException.
                PCSMessageBox.Show(ex.mCode, MessageBoxIcon.Error);
                try
                {
                    // Log error message into log file.
                    Logger.LogMessage(ex.CauseException, METHOD_NAME, Level.ERROR);
                }
                catch
                {
                    // Show message if logger has an error.
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                // Displays the error message if throwed from system.
                PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxIcon.Error);
                try
                {
                    // Log error message into log file.
                    Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
                }
                catch
                {
                    // Show message if logger has an error.
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
                }
            }
        }

        private void dtmDate_DropDownClosed(object sender, EventArgs e)
        {
            const string METHOD_NAME = THIS + ".dtmTransDate_DropDownClosed()";
            try
            {
                if (dtmDate.Text != string.Empty)
                {
                    var dtmValue = new DateTime(DateTime.Parse(dtmDate.Value.ToString()).Year,
                                                DateTime.Parse(dtmDate.Value.ToString()).Month,
                                                DateTime.Parse(dtmDate.Value.ToString()).Day, DateTime.Now.Hour,
                                                DateTime.Now.Minute, DateTime.Now.Second);
                    dtmDate.Value = dtmValue;
                }
                else
                {
                    dgrdData[dgrdData.Row, dgrdData.Col] = DBNull.Value;
                }
            }
            catch (PCSException ex)
            {
                PCSMessageBox.Show(ex.mCode, MessageBoxIcon.Error);
                try
                {
                    Logger.LogMessage(ex.CauseException, METHOD_NAME, Level.ERROR);
                }
                catch
                {
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxIcon.Error);
                try
                {
                    Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
                }
                catch
                {
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
                }
            }
        }

        private int GetRowsCount()
        {
            if (!blnAddNewByF12)
            {
                blnAddNewByF12 = true;
                return dsDeliverySchedule.Tables[0].Select("").Length - 1;
            }
            else
            {
                blnAddNewByF12 = true;
                return dsDeliverySchedule.Tables[0].Select("").Length;
            }
        }

        private void dgrdData_KeyDown(object sender, KeyEventArgs e)
        {
            const string METHOD_NAME = THIS + "().dgrdData_KeyDown";
            try
            {
                if (e.KeyCode == Keys.F4 && btnSave.Enabled)
                {
                    dgrdData_ButtonClick(null, null);
                }

                if (e.KeyCode == Keys.Delete)
                {
                    if (CheckBeforeDeleteAllRows())
                    {
                        FormControlComponents.DeleteMultiRowsOnTrueDBGrid(dgrdData);
                        for (int i = 0; i < GetRowsCount(); i++)
                            dgrdData[i, SO_DeliveryScheduleTable.LINE_FLD] = i + 1;
                        lblTotalDelivery.Value = SumTotalDeliveryQuantity().ToString();
                        return;
                    }
                    else
                    {
                        PCSMessageBox.Show(ErrorCode.MESSAGE_SOLINE_HAS_DELIVERY, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (PCSException ex)
            {
                PCSMessageBox.Show(ex.mCode, MessageBoxIcon.Error);
                try
                {
                    Logger.LogMessage(ex.CauseException, METHOD_NAME, Level.ERROR);
                }
                catch
                {
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxIcon.Error);
                try
                {
                    Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
                }
                catch
                {
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// FillItemDataToGrid
        /// </summary>
        /// <param name="pdrowData"></param>
        /// <author>Trada</author>
        /// <date>Monday, May 29 2006</date>
        private void FillItemDataToGrid(DataRow pdrowData)
        {
            const string METHOD_NAME = THIS + ".FillItemDataToGrid()";
            try
            {
                dgrdData.EditActive = true;
//				dgrdData.Columns[SO_GateTable.GATEID_FLD].Value = int.Parse(pdrowData[SO_GateTable.GATEID_FLD].ToString());
//				dgrdData.Columns[SO_GateTable.CODE_FLD].Value = pdrowData[SO_GateTable.CODE_FLD];
                dgrdData[dgrdData.Row, SO_GateTable.GATEID_FLD] =
                    int.Parse(pdrowData[SO_GateTable.GATEID_FLD].ToString());
                dgrdData[dgrdData.Row, SO_GateTable.CODE_FLD] = pdrowData[SO_GateTable.CODE_FLD];
            }
            catch (PCSException ex)
            {
                throw new PCSException(ex.mCode, METHOD_NAME, ex);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        /// <summary>
        /// dgrdData_ButtonClick
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Trada</author>
        /// <date>Monday, May 29 2006</date>
        private void dgrdData_ButtonClick(object sender, ColEventArgs e)
        {
            const string METHOD_NAME = THIS + "().dgrdData_ButtonClick";
            try
            {
                //if (dgrdData.Row == dgrdData.RowCount - 1)
                if (dgrdData.Row == dsDeliverySchedule.Tables[0].Rows.Count
                    && dgrdData.Col == dgrdData.Columns.IndexOf(dgrdData.Columns[SO_GateTable.CODE_FLD]))
                {
                    if (lblTotalDelivery.Text.Trim() == lblOrderQuantity.Text.Trim()
                        || (decimal.Parse(lblOrderQuantity.Text) - decimal.Parse(lblTotalDelivery.Text) <= 0))
                    {
                        //MessageBox.Show("There are enought order quantity, you cannot add more row");
                        PCSMessageBox.Show(ErrorCode.MESSAGE_ENOUGH_DELIVERYQTY, MessageBoxIcon.Warning);
                        dtmDate.TextDetached = true;
                        return;
                    }
                    if (lblOrderQuantity.Text.Trim() == lblTotalDelivery.Text.Trim())
                    {
                        //MessageBox.Show("There are no enough quantiy for new order delivery ");
                        PCSMessageBox.Show(ErrorCode.MESSAGE_ENOUGH_DELIVERYQTY, MessageBoxIcon.Warning);
                        dtmDate.TextDetached = true;
                    }
                }
                DataRowView drwResult = null;
                var htbCondition = new Hashtable();

                if (!btnSave.Enabled) return;
                //Select Gate
                if (dgrdData.Col == dgrdData.Columns.IndexOf(dgrdData.Columns[SO_GateTable.CODE_FLD]))
                {
                    if (dgrdData.AddNewMode == AddNewModeEnum.AddNewCurrent)
                    {
                        drwResult = FormControlComponents.OpenSearchForm(SO_GateTable.TABLE_NAME, SO_GateTable.CODE_FLD,
                                                                         dgrdData[dgrdData.Row, SO_GateTable.CODE_FLD].
                                                                             ToString(), htbCondition, true);
                    }
                    else
                    {
                        drwResult = FormControlComponents.OpenSearchForm(SO_GateTable.TABLE_NAME, SO_GateTable.CODE_FLD,
                                                                         dgrdData.Columns[SO_GateTable.CODE_FLD].Text.
                                                                             Trim(), htbCondition, true);
                    }
                    if (drwResult != null)
                    {
                        dgrdData.EditActive = true;
                        FillItemDataToGrid(drwResult.Row);
                    }
                }
            }
            catch (PCSException ex)
            {
                PCSMessageBox.Show(ex.mCode, MessageBoxIcon.Error);
                try
                {
                    Logger.LogMessage(ex.CauseException, METHOD_NAME, Level.ERROR);
                }
                catch
                {
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxIcon.Error);
                try
                {
                    Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
                }
                catch
                {
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
                }
            }
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            var resources = new System.Resources.ResourceManager(typeof (SODeliverySchedule));
            this.dgrdData = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnHelp = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnSOATP = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lblOrderQuantity = new C1.Win.C1Input.C1NumericEdit();
            this.lblTotalDelivery = new C1.Win.C1Input.C1NumericEdit();
            this.txtSONo = new System.Windows.Forms.TextBox();
            this.txtLine = new System.Windows.Forms.TextBox();
            this.txtPartNumber = new System.Windows.Forms.TextBox();
            this.txtModel = new System.Windows.Forms.TextBox();
            this.txtPartName = new System.Windows.Forms.TextBox();
            this.txtUnit = new System.Windows.Forms.TextBox();
            this.txtCCN = new System.Windows.Forms.TextBox();
            this.dtmDate = new C1.Win.C1Input.C1DateEdit();
            ((System.ComponentModel.ISupportInitialize) (this.dgrdData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize) (this.lblOrderQuantity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize) (this.lblTotalDelivery)).BeginInit();
            ((System.ComponentModel.ISupportInitialize) (this.dtmDate)).BeginInit();
            this.SuspendLayout();
            // 
            // dgrdData
            // 
            this.dgrdData.AccessibleDescription = "";
            this.dgrdData.AccessibleName = "";
            this.dgrdData.AllowAddNew = true;
            this.dgrdData.AllowDelete = true;
            this.dgrdData.AllowFilter = false;
            this.dgrdData.AllowSort = false;
            this.dgrdData.Anchor =
                ((System.Windows.Forms.AnchorStyles)
                 ((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                    | System.Windows.Forms.AnchorStyles.Left)
                   | System.Windows.Forms.AnchorStyles.Right)));
            this.dgrdData.CaptionHeight = 17;
            this.dgrdData.CollapseColor = System.Drawing.Color.Black;
            this.dgrdData.ExpandColor = System.Drawing.Color.Black;
            this.dgrdData.GroupByCaption = "Drag a column header here to group by that column";
            this.dgrdData.Images.Add(((System.Drawing.Image) (resources.GetObject("resource"))));
            this.dgrdData.Location = new System.Drawing.Point(6, 100);
            this.dgrdData.MarqueeStyle = C1.Win.C1TrueDBGrid.MarqueeEnum.DottedCellBorder;
            this.dgrdData.Name = "dgrdData";
            this.dgrdData.PreviewInfo.AllowSizing = false;
            this.dgrdData.PreviewInfo.Caption = null;
            this.dgrdData.PreviewInfo.Location = new System.Drawing.Point(0, 0);
            this.dgrdData.PreviewInfo.Size = new System.Drawing.Size(0, 0);
            this.dgrdData.PreviewInfo.ToolBars = false;
            this.dgrdData.PreviewInfo.ZoomFactor = 0;
            this.dgrdData.PrintInfo.MaxRowHeight = 0;
            this.dgrdData.PrintInfo.PageFooter = null;
            this.dgrdData.PrintInfo.PageFooterHeight = 0;
            this.dgrdData.PrintInfo.PageHeader = null;
            this.dgrdData.PrintInfo.PageHeaderHeight = 0;
            this.dgrdData.PrintInfo.ProgressCaption = null;
            this.dgrdData.PrintInfo.RepeatColumnHeaders = false;
            this.dgrdData.PrintInfo.ShowOptionsDialog = false;
            this.dgrdData.PrintInfo.ShowProgressForm = false;
            this.dgrdData.PrintInfo.ShowSelection = false;
            this.dgrdData.PrintInfo.UseGridColors = false;
            this.dgrdData.RecordSelectorWidth = 17;
            this.dgrdData.RowDivider.Color = System.Drawing.Color.DarkGray;
            this.dgrdData.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.Single;
            this.dgrdData.RowHeight = 15;
            this.dgrdData.RowSubDividerColor = System.Drawing.Color.DarkGray;
            this.dgrdData.Size = new System.Drawing.Size(624, 318);
            this.dgrdData.TabAction = C1.Win.C1TrueDBGrid.TabActionEnum.ColumnNavigation;
            this.dgrdData.TabIndex = 18;
            this.dgrdData.Text = "c1TrueDBGrid1";
            this.dgrdData.AfterDelete += new System.EventHandler(this.dgrdData_AfterDelete);
            this.dgrdData.AfterUpdate += new System.EventHandler(this.dgrdData_AfterUpdate);
            this.dgrdData.BeforeColEdit += new C1.Win.C1TrueDBGrid.BeforeColEditEventHandler(this.dgrdData_BeforeColEdit);
            this.dgrdData.ButtonClick += new C1.Win.C1TrueDBGrid.ColEventHandler(this.dgrdData_ButtonClick);
            this.dgrdData.AfterColUpdate += new C1.Win.C1TrueDBGrid.ColEventHandler(this.dgrdData_AfterColUpdate);
            this.dgrdData.BeforeInsert += new C1.Win.C1TrueDBGrid.CancelEventHandler(this.dgrdData_BeforeInsert);
            this.dgrdData.BeforeColUpdate +=
                new C1.Win.C1TrueDBGrid.BeforeColUpdateEventHandler(this.dgrdData_BeforeColUpdate);
            this.dgrdData.BeforeDelete += new C1.Win.C1TrueDBGrid.CancelEventHandler(this.dgrdData_BeforeDelete);
            this.dgrdData.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgrdData_KeyDown);
            this.dgrdData.OnAddNew += new System.EventHandler(this.dgrdData_OnAddNew);
            this.dgrdData.PropBag =
                "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"Line\" DataF" +
                "ield=\"Line\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Ca" +
                "ption=\"Required Date\" DataField=\"RequiredDate\"><ValueItems /><GroupInfo /></C1Da" +
                "taColumn><C1DataColumn Level=\"0\" Caption=\"Promise Date\" DataField=\"PromiseDate\">" +
                "<ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Sched" +
                "ule Date\" DataField=\"ScheduleDate\"><ValueItems /><GroupInfo /></C1DataColumn><C1" +
                "DataColumn Level=\"0\" Caption=\"Delivery Quantity\" DataField=\"DeliveryQuantity\"><V" +
                "alueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Commit " +
                "Quantity\" DataField=\"SUMCommitQuantity\"><ValueItems /><GroupInfo /></C1DataColum" +
                "n><C1DataColumn Level=\"0\" Caption=\"Gate\" DataField=\"Code\"><ValueItems /><GroupIn" +
                "fo /></C1DataColumn></DataCols><Styles type=\"C1.Win.C1TrueDBGrid.Design.ContextW" +
                "rapper\"><Data>Style12{}RecordSelector{AlignImage:Center;}Style50{}Style51{}Style" +
                "52{AlignHorz:Near;}Style53{AlignHorz:Near;}Style54{}Caption{AlignHorz:Center;}St" +
                "yle56{}Normal{Font:Tahoma, 11world;}Selected{ForeColor:HighlightText;BackColor:H" +
                "ighlight;}Editor{}Style18{}Style19{}Style14{}Style15{}Style16{AlignHorz:Center;}" +
                "Style17{AlignHorz:Near;}Style10{AlignHorz:Near;}Style11{}OddRow{}Style13{}Style4" +
                "2{}Style47{AlignHorz:Near;}Heading{Wrap:True;AlignVert:Center;Border:Raised,,1, " +
                "1, 1, 1;ForeColor:ControlText;BackColor:Control;}Style39{}Style36{}Style29{Align" +
                "Horz:Near;}Style28{AlignHorz:Center;}HighlightRow{ForeColor:HighlightText;BackCo" +
                "lor:Highlight;}Style26{}Style25{}Footer{}Style23{AlignHorz:Near;}Style22{AlignHo" +
                "rz:Center;}Style21{}Style55{}Group{BackColor:ControlDark;Border:None,,0, 0, 0, 0" +
                ";AlignVert:Center;}Style57{}Inactive{ForeColor:InactiveCaptionText;BackColor:Ina" +
                "ctiveCaption;}EvenRow{BackColor:Aqua;}Style6{}Style27{}Style49{}Style48{}Style24" +
                "{}Style7{}Style8{}Style1{}Style20{}Style3{}Style41{AlignHorz:Near;}Style9{}Style" +
                "40{AlignHorz:Center;ForeColor:Maroon;}Style43{}Style45{}Style5{}Style4{}Style46{" +
                "AlignHorz:Center;}Style38{}Style44{}FilterBar{}Style37{}Style34{AlignHorz:Center" +
                ";ForeColor:Maroon;BackColor:Transparent;}Style35{AlignHorz:Near;}Style32{}Style3" +
                "3{}Style30{}Style31{}Style2{}</Data></Styles><Splits><C1.Win.C1TrueDBGrid.MergeV" +
                "iew Name=\"\" CaptionHeight=\"17\" ColumnCaptionHeight=\"17\" ColumnFooterHeight=\"17\" " +
                "MarqueeStyle=\"DottedCellBorder\" RecordSelectorWidth=\"17\" DefRecSelWidth=\"17\" Ver" +
                "ticalScrollGroup=\"1\" HorizontalScrollGroup=\"1\"><ClientRect>0, 0, 620, 314</Clien" +
                "tRect><BorderSide>0</BorderSide><CaptionStyle parent=\"Style2\" me=\"Style10\" /><Ed" +
                "itorStyle parent=\"Editor\" me=\"Style5\" /><EvenRowStyle parent=\"EvenRow\" me=\"Style" +
                "8\" /><FilterBarStyle parent=\"FilterBar\" me=\"Style13\" /><FooterStyle parent=\"Foot" +
                "er\" me=\"Style3\" /><GroupStyle parent=\"Group\" me=\"Style12\" /><HeadingStyle parent" +
                "=\"Heading\" me=\"Style2\" /><HighLightRowStyle parent=\"HighlightRow\" me=\"Style7\" />" +
                "<InactiveStyle parent=\"Inactive\" me=\"Style4\" /><OddRowStyle parent=\"OddRow\" me=\"" +
                "Style9\" /><RecordSelectorStyle parent=\"RecordSelector\" me=\"Style11\" /><SelectedS" +
                "tyle parent=\"Selected\" me=\"Style6\" /><Style parent=\"Normal\" me=\"Style1\" /><inter" +
                "nalCols><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style16\" /><Style par" +
                "ent=\"Style1\" me=\"Style17\" /><FooterStyle parent=\"Style3\" me=\"Style18\" /><EditorS" +
                "tyle parent=\"Style5\" me=\"Style19\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style2" +
                "1\" /><GroupFooterStyle parent=\"Style1\" me=\"Style20\" /><Visible>True</Visible><Co" +
                "lumnDivider>DarkGray,Single</ColumnDivider><Width>51</Width><Height>15</Height><" +
                "DCIdx>0</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" " +
                "me=\"Style22\" /><Style parent=\"Style1\" me=\"Style23\" /><FooterStyle parent=\"Style3" +
                "\" me=\"Style24\" /><EditorStyle parent=\"Style5\" me=\"Style25\" /><GroupHeaderStyle p" +
                "arent=\"Style1\" me=\"Style27\" /><GroupFooterStyle parent=\"Style1\" me=\"Style26\" /><" +
                "Visible>True</Visible><ColumnDivider>DarkGray,Single</ColumnDivider><Width>91</W" +
                "idth><Height>15</Height><DCIdx>1</DCIdx></C1DisplayColumn><C1DisplayColumn><Head" +
                "ingStyle parent=\"Style2\" me=\"Style28\" /><Style parent=\"Style1\" me=\"Style29\" /><F" +
                "ooterStyle parent=\"Style3\" me=\"Style30\" /><EditorStyle parent=\"Style5\" me=\"Style" +
                "31\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style33\" /><GroupFooterStyle parent=" +
                "\"Style1\" me=\"Style32\" /><Visible>True</Visible><ColumnDivider>DarkGray,Single</C" +
                "olumnDivider><Width>97</Width><Height>15</Height><DCIdx>2</DCIdx></C1DisplayColu" +
                "mn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style34\" /><Style parent=\"" +
                "Style1\" me=\"Style35\" /><FooterStyle parent=\"Style3\" me=\"Style36\" /><EditorStyle " +
                "parent=\"Style5\" me=\"Style37\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style39\" />" +
                "<GroupFooterStyle parent=\"Style1\" me=\"Style38\" /><Visible>True</Visible><ColumnD" +
                "ivider>DarkGray,Single</ColumnDivider><Width>145</Width><Height>15</Height><DCId" +
                "x>3</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"" +
                "Style40\" /><Style parent=\"Style1\" me=\"Style41\" /><FooterStyle parent=\"Style3\" me" +
                "=\"Style42\" /><EditorStyle parent=\"Style5\" me=\"Style43\" /><GroupHeaderStyle paren" +
                "t=\"Style1\" me=\"Style45\" /><GroupFooterStyle parent=\"Style1\" me=\"Style44\" /><Visi" +
                "ble>True</Visible><ColumnDivider>DarkGray,Single</ColumnDivider><Width>131</Widt" +
                "h><Height>15</Height><DCIdx>4</DCIdx></C1DisplayColumn><C1DisplayColumn><Heading" +
                "Style parent=\"Style2\" me=\"Style46\" /><Style parent=\"Style1\" me=\"Style47\" /><Foot" +
                "erStyle parent=\"Style3\" me=\"Style48\" /><EditorStyle parent=\"Style5\" me=\"Style49\"" +
                " /><GroupHeaderStyle parent=\"Style1\" me=\"Style51\" /><GroupFooterStyle parent=\"St" +
                "yle1\" me=\"Style50\" /><Visible>True</Visible><ColumnDivider>DarkGray,Single</Colu" +
                "mnDivider><Width>106</Width><Height>15</Height><DCIdx>5</DCIdx></C1DisplayColumn" +
                "><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style52\" /><Style parent=\"St" +
                "yle1\" me=\"Style53\" /><FooterStyle parent=\"Style3\" me=\"Style54\" /><EditorStyle pa" +
                "rent=\"Style5\" me=\"Style55\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style57\" /><G" +
                "roupFooterStyle parent=\"Style1\" me=\"Style56\" /><Visible>True</Visible><ColumnDiv" +
                "ider>DarkGray,Single</ColumnDivider><Width>58</Width><Height>15</Height><DCIdx>6" +
                "</DCIdx></C1DisplayColumn></internalCols></C1.Win.C1TrueDBGrid.MergeView></Split" +
                "s><NamedStyles><Style parent=\"\" me=\"Normal\" /><Style parent=\"Normal\" me=\"Heading" +
                "\" /><Style parent=\"Heading\" me=\"Footer\" /><Style parent=\"Heading\" me=\"Caption\" /" +
                "><Style parent=\"Heading\" me=\"Inactive\" /><Style parent=\"Normal\" me=\"Selected\" />" +
                "<Style parent=\"Normal\" me=\"Editor\" /><Style parent=\"Normal\" me=\"HighlightRow\" />" +
                "<Style parent=\"Normal\" me=\"EvenRow\" /><Style parent=\"Normal\" me=\"OddRow\" /><Styl" +
                "e parent=\"Heading\" me=\"RecordSelector\" /><Style parent=\"Normal\" me=\"FilterBar\" /" +
                "><Style parent=\"Caption\" me=\"Group\" /></NamedStyles><vertSplits>1</vertSplits><h" +
                "orzSplits>1</horzSplits><Layout>Modified</Layout><DefaultRecSelWidth>17</Default" +
                "RecSelWidth><ClientArea>0, 0, 620, 314</ClientArea><PrintPageHeaderStyle parent=" +
                "\"\" me=\"Style14\" /><PrintPageFooterStyle parent=\"\" me=\"Style15\" /></Blob>";
            // 
            // label9
            // 
            this.label9.AccessibleDescription = "";
            this.label9.AccessibleName = "";
            this.label9.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label9.ForeColor = System.Drawing.Color.Black;
            this.label9.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label9.Location = new System.Drawing.Point(6, 6);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(68, 20);
            this.label9.TabIndex = 2;
            this.label9.Text = "Order No.";
            // 
            // label10
            // 
            this.label10.AccessibleDescription = "";
            this.label10.AccessibleName = "";
            this.label10.Anchor =
                ((System.Windows.Forms.AnchorStyles)
                 ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label10.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label10.ForeColor = System.Drawing.Color.Black;
            this.label10.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label10.Location = new System.Drawing.Point(512, 6);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(36, 20);
            this.label10.TabIndex = 0;
            this.label10.Text = "CCN";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label7
            // 
            this.label7.AccessibleDescription = "";
            this.label7.AccessibleName = "";
            this.label7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label7.Location = new System.Drawing.Point(6, 72);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(68, 20);
            this.label7.TabIndex = 12;
            this.label7.Text = "Part Name";
            // 
            // label6
            // 
            this.label6.AccessibleDescription = "";
            this.label6.AccessibleName = "";
            this.label6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label6.Location = new System.Drawing.Point(294, 49);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(42, 20);
            this.label6.TabIndex = 8;
            this.label6.Text = "Model";
            // 
            // label5
            // 
            this.label5.AccessibleDescription = "";
            this.label5.AccessibleName = "";
            this.label5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label5.Location = new System.Drawing.Point(6, 50);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(68, 20);
            this.label5.TabIndex = 6;
            this.label5.Text = "Part Number";
            // 
            // label4
            // 
            this.label4.AccessibleDescription = "";
            this.label4.AccessibleName = "";
            this.label4.Anchor =
                ((System.Windows.Forms.AnchorStyles)
                 ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label4.Location = new System.Drawing.Point(476, 72);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(74, 20);
            this.label4.TabIndex = 16;
            this.label4.Text = "Total Delivery";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.AccessibleDescription = "";
            this.label3.AccessibleName = "";
            this.label3.Anchor =
                ((System.Windows.Forms.AnchorStyles)
                 ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label3.Location = new System.Drawing.Point(476, 50);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 20);
            this.label3.TabIndex = 10;
            this.label3.Text = "Order Quatity";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.AccessibleDescription = "";
            this.label2.AccessibleName = "";
            this.label2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label2.Location = new System.Drawing.Point(6, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 20);
            this.label2.TabIndex = 4;
            this.label2.Text = "Line";
            // 
            // btnClose
            // 
            this.btnClose.AccessibleDescription = "";
            this.btnClose.AccessibleName = "";
            this.btnClose.Anchor =
                ((System.Windows.Forms.AnchorStyles)
                 ((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnClose.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnClose.Location = new System.Drawing.Point(570, 422);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(60, 23);
            this.btnClose.TabIndex = 25;
            this.btnClose.Text = "&Close";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnHelp
            // 
            this.btnHelp.AccessibleDescription = "";
            this.btnHelp.AccessibleName = "";
            this.btnHelp.Anchor =
                ((System.Windows.Forms.AnchorStyles)
                 ((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnHelp.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnHelp.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnHelp.Location = new System.Drawing.Point(509, 422);
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Size = new System.Drawing.Size(60, 23);
            this.btnHelp.TabIndex = 24;
            this.btnHelp.Text = "&Help";
            this.btnHelp.Click += new System.EventHandler(this.btnHelp_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.AccessibleDescription = "";
            this.btnDelete.AccessibleName = "";
            this.btnDelete.Anchor =
                ((System.Windows.Forms.AnchorStyles)
                 ((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnDelete.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnDelete.Location = new System.Drawing.Point(67, 422);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(60, 23);
            this.btnDelete.TabIndex = 21;
            this.btnDelete.Text = "&Delete";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnSave
            // 
            this.btnSave.AccessibleDescription = "";
            this.btnSave.AccessibleName = "";
            this.btnSave.Anchor =
                ((System.Windows.Forms.AnchorStyles)
                 ((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnSave.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnSave.Location = new System.Drawing.Point(6, 422);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(60, 23);
            this.btnSave.TabIndex = 20;
            this.btnSave.Text = "&Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnSOATP
            // 
            this.btnSOATP.AccessibleDescription = "";
            this.btnSOATP.AccessibleName = "";
            this.btnSOATP.Anchor =
                ((System.Windows.Forms.AnchorStyles)
                 ((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSOATP.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnSOATP.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnSOATP.Location = new System.Drawing.Point(442, 422);
            this.btnSOATP.Name = "btnSOATP";
            this.btnSOATP.Size = new System.Drawing.Size(66, 23);
            this.btnSOATP.TabIndex = 23;
            this.btnSOATP.Text = "A&TP";
            this.btnSOATP.Visible = false;
            this.btnSOATP.Click += new System.EventHandler(this.btnSOATP_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.AccessibleDescription = "";
            this.btnPrint.AccessibleName = "";
            this.btnPrint.Anchor =
                ((System.Windows.Forms.AnchorStyles)
                 ((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnPrint.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnPrint.Location = new System.Drawing.Point(128, 422);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(60, 23);
            this.btnPrint.TabIndex = 22;
            this.btnPrint.Text = "&Print";
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // label1
            // 
            this.label1.AccessibleDescription = "";
            this.label1.AccessibleName = "";
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label1.Location = new System.Drawing.Point(377, 72);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(28, 20);
            this.label1.TabIndex = 14;
            this.label1.Text = "UM";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblOrderQuantity
            // 
            this.lblOrderQuantity.AccessibleDescription = "";
            this.lblOrderQuantity.AccessibleName = "";
            this.lblOrderQuantity.Anchor =
                ((System.Windows.Forms.AnchorStyles)
                 ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // lblOrderQuantity.Calculator
            // 
            this.lblOrderQuantity.Calculator.AccessibleDescription = "";
            this.lblOrderQuantity.Calculator.AccessibleName = "";
            this.lblOrderQuantity.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
            this.lblOrderQuantity.Location = new System.Drawing.Point(550, 50);
            this.lblOrderQuantity.Name = "lblOrderQuantity";
            this.lblOrderQuantity.ReadOnly = true;
            this.lblOrderQuantity.Size = new System.Drawing.Size(80, 20);
            this.lblOrderQuantity.TabIndex = 11;
            this.lblOrderQuantity.TabStop = false;
            this.lblOrderQuantity.Tag = null;
            this.lblOrderQuantity.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.lblOrderQuantity.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.None;
            // 
            // lblTotalDelivery
            // 
            this.lblTotalDelivery.AccessibleDescription = "";
            this.lblTotalDelivery.AccessibleName = "";
            this.lblTotalDelivery.Anchor =
                ((System.Windows.Forms.AnchorStyles)
                 ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // lblTotalDelivery.Calculator
            // 
            this.lblTotalDelivery.Calculator.AccessibleDescription = "";
            this.lblTotalDelivery.Calculator.AccessibleName = "";
            this.lblTotalDelivery.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
            this.lblTotalDelivery.Location = new System.Drawing.Point(550, 72);
            this.lblTotalDelivery.Name = "lblTotalDelivery";
            this.lblTotalDelivery.ReadOnly = true;
            this.lblTotalDelivery.Size = new System.Drawing.Size(80, 20);
            this.lblTotalDelivery.TabIndex = 17;
            this.lblTotalDelivery.TabStop = false;
            this.lblTotalDelivery.Tag = null;
            this.lblTotalDelivery.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.lblTotalDelivery.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.None;
            // 
            // txtSONo
            // 
            this.txtSONo.Location = new System.Drawing.Point(72, 4);
            this.txtSONo.Name = "txtSONo";
            this.txtSONo.ReadOnly = true;
            this.txtSONo.TabIndex = 3;
            this.txtSONo.TabStop = false;
            this.txtSONo.Text = "";
            // 
            // txtLine
            // 
            this.txtLine.Location = new System.Drawing.Point(72, 26);
            this.txtLine.Name = "txtLine";
            this.txtLine.ReadOnly = true;
            this.txtLine.TabIndex = 5;
            this.txtLine.TabStop = false;
            this.txtLine.Text = "";
            // 
            // txtPartNumber
            // 
            this.txtPartNumber.Location = new System.Drawing.Point(72, 49);
            this.txtPartNumber.Name = "txtPartNumber";
            this.txtPartNumber.ReadOnly = true;
            this.txtPartNumber.Size = new System.Drawing.Size(214, 20);
            this.txtPartNumber.TabIndex = 7;
            this.txtPartNumber.TabStop = false;
            this.txtPartNumber.Text = "";
            // 
            // txtModel
            // 
            this.txtModel.Location = new System.Drawing.Point(336, 48);
            this.txtModel.Name = "txtModel";
            this.txtModel.ReadOnly = true;
            this.txtModel.Size = new System.Drawing.Size(116, 20);
            this.txtModel.TabIndex = 9;
            this.txtModel.TabStop = false;
            this.txtModel.Text = "";
            // 
            // txtPartName
            // 
            this.txtPartName.Location = new System.Drawing.Point(72, 72);
            this.txtPartName.Name = "txtPartName";
            this.txtPartName.ReadOnly = true;
            this.txtPartName.Size = new System.Drawing.Size(302, 20);
            this.txtPartName.TabIndex = 13;
            this.txtPartName.TabStop = false;
            this.txtPartName.Text = "";
            // 
            // txtUnit
            // 
            this.txtUnit.Location = new System.Drawing.Point(407, 72);
            this.txtUnit.Name = "txtUnit";
            this.txtUnit.ReadOnly = true;
            this.txtUnit.Size = new System.Drawing.Size(45, 20);
            this.txtUnit.TabIndex = 15;
            this.txtUnit.TabStop = false;
            this.txtUnit.Text = "";
            // 
            // txtCCN
            // 
            this.txtCCN.Anchor =
                ((System.Windows.Forms.AnchorStyles)
                 ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCCN.Location = new System.Drawing.Point(550, 4);
            this.txtCCN.Name = "txtCCN";
            this.txtCCN.ReadOnly = true;
            this.txtCCN.Size = new System.Drawing.Size(80, 20);
            this.txtCCN.TabIndex = 1;
            this.txtCCN.TabStop = false;
            this.txtCCN.Text = "";
            // 
            // dtmDate
            // 
            this.dtmDate.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
            this.dtmDate.Location = new System.Drawing.Point(250, 215);
            this.dtmDate.Name = "dtmDate";
            this.dtmDate.Size = new System.Drawing.Size(134, 20);
            this.dtmDate.TabIndex = 27;
            this.dtmDate.Tag = null;
            this.dtmDate.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.DropDown;
            this.dtmDate.DropDownClosed += new DropDownClosedEventHandler(this.dtmDate_DropDownClosed);
            // 
            // SODeliverySchedule
            // 
            this.AccessibleDescription = "";
            this.AccessibleName = "";
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(634, 450);
            this.Controls.Add(this.txtCCN);
            this.Controls.Add(this.txtUnit);
            this.Controls.Add(this.txtPartName);
            this.Controls.Add(this.txtModel);
            this.Controls.Add(this.txtPartNumber);
            this.Controls.Add(this.txtLine);
            this.Controls.Add(this.txtSONo);
            this.Controls.Add(this.dgrdData);
            this.Controls.Add(this.lblTotalDelivery);
            this.Controls.Add(this.lblOrderQuantity);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.btnSOATP);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnHelp);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dtmDate);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.Name = "SODeliverySchedule";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sales Order Delivery Schedule";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SODeliverySchedule_KeyDown);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.SODeliverySchedule_Closing);
            this.Load += new System.EventHandler(this.SODeliverySchedule_Load);
            ((System.ComponentModel.ISupportInitialize) (this.dgrdData)).EndInit();
            ((System.ComponentModel.ISupportInitialize) (this.lblOrderQuantity)).EndInit();
            ((System.ComponentModel.ISupportInitialize) (this.lblTotalDelivery)).EndInit();
            ((System.ComponentModel.ISupportInitialize) (this.dtmDate)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion
    }
}