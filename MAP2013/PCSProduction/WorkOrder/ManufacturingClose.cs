using System;
using System.Data;
using System.Collections;
using System.Windows.Forms;
using log4net;
using PCSComUtils.PCSExc;
using PCSUtils.Utils;
using PCSUtils.Log;
using PCSComUtils.Common;
using PCSComUtils.MasterSetup.DS;
using PCSComUtils.Common.BO;
using PCSComProduction.WorkOrder.BO;
using PCSComProduction.WorkOrder.DS;
using C1.Win.C1TrueDBGrid;


namespace PCSProduction.WorkOrder
{
	/// <summary>
	/// Summary description for ManufacturingClose.
	/// </summary>
	public partial class ManufacturingClose : Form
	{
		protected const string THIS = "PCSProduction.WorkOrder.ManufacturingClose";
	    private ILog _logger = LogManager.GetLogger(typeof (ManufacturingClose));

		/// <summary>
		/// Required designer variable.
		/// </summary>
		protected DataTable dtbGridLayOut;
		
		protected MST_MasterLocationVO voMasLoc = new MST_MasterLocationVO();
		protected DataSet dstReleaseWO = new DataSet();
		protected DataSet dstGridData;
		protected const string SELECT = "Selected";
		protected bool blnStateOfCheck = false;
		protected const string OPENQUANTITY = "OpenQuantity";
		private const string MANCLOSE_TABLE = "ManClose";
		protected const string PARTNUMBER = "PartNumber";
		protected const string CAPTION_UM = "UM";
		protected const string TRUE = "True";
		private const string CLOSE = "close";

		public ManufacturingClose()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
		}

		/// <summary>
		/// ManufacturingClose_Load
		/// </summary>
		/// <author>Trada</author>
		/// <date>Thursday, June 9 2005</date>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ManufacturingClose_Load(object sender, System.EventArgs e)
		{
			
			const string METHOD_NAME = THIS + ".ManufacturingClose_Load()";
			try
			{
				//Set authorization for user
				Security objSecurity = new Security();
				this.Name = THIS;
				if(objSecurity.SetRightForUserOnForm(this, SystemProperty.UserName) == 0)
				{
					this.Close();
					// You don't have the right to view this item
					PCSMessageBox.Show(ErrorCode.MESSAGE_YOU_HAVE_NO_RIGHT_TO_VIEW);
					return;
				}
				//StoreGrid();
				dtbGridLayOut = FormControlComponents.StoreGridLayout(gridWOClose);
				InitVariable();
				//Fill Default Master Location 
				FormControlComponents.SetDefaultMasterLocation(txtMasLoc);
				voMasLoc.MasterLocationID = SystemProperty.MasterLocationID;
				//Set focus to dtmPostDate
				dtmPostDate.Focus();
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
		/// <summary>
		/// Init variables 
		/// </summary>
		/// <author>Trada</author>
		/// <date>Friday, June 10 2005</date>
		private void InitVariable()
		{
            // Load combo box
            UtilsBO boUtil = new UtilsBO();
            DataSet dstCCN = boUtil.ListCCN();
            cboCCN.DataSource = dstCCN.Tables[MST_CCNTable.TABLE_NAME];
            cboCCN.DisplayMember = MST_CCNTable.CODE_FLD;
            cboCCN.ValueMember = MST_CCNTable.CCNID_FLD;
            FormControlComponents.PutDataIntoC1ComboBox(cboCCN, dstCCN.Tables[MST_CCNTable.TABLE_NAME], MST_CCNTable.CODE_FLD, MST_CCNTable.CCNID_FLD, MST_CCNTable.TABLE_NAME);
            if (SystemProperty.CCNID != 0)
            {
                cboCCN.SelectedValue = SystemProperty.CCNID;
            }
            // Load combo box displays approving date
            PRO_WorkOrderDetailVO voWorkOrderDetail = new PRO_WorkOrderDetailVO();
            voWorkOrderDetail.MfgCloseDate = boUtil.GetDBDate();
            if ((DateTime.MinValue < voWorkOrderDetail.MfgCloseDate) && (voWorkOrderDetail.MfgCloseDate < DateTime.MaxValue))
                dtmPostDate.Value = voWorkOrderDetail.MfgCloseDate;
            else
                dtmPostDate.Value = DBNull.Value;
		}
		/// <summary>
		/// btnSearchMasLoc_Click
		/// </summary>
		/// <author>Trada</author>
		/// <date>Friday, June 10 2005</date>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnSearchMasLoc_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnSearchMasLoc_Click()";
			try 
			{
				Hashtable htbCriteria = new Hashtable();
				DataRowView drwResult = null;
				//User has selected CCN
				if (cboCCN.SelectedIndex != -1)
				{
					htbCriteria.Add(MST_MasterLocationTable.CCNID_FLD, cboCCN.SelectedValue);	
				}
				else //User has not selected CCN
				{
					htbCriteria.Add(MST_MasterLocationTable.CCNID_FLD, 0);
				}
				drwResult = FormControlComponents.OpenSearchForm(MST_MasterLocationTable.TABLE_NAME, MST_MasterLocationTable.CODE_FLD, txtMasLoc.Text, htbCriteria, true);
				if (drwResult != null)
				{
					if (voMasLoc.MasterLocationID != int.Parse(drwResult[MST_MasterLocationTable.MASTERLOCATIONID_FLD].ToString()))
					{
						txtMasLoc.Text = drwResult[MST_MasterLocationTable.CODE_FLD].ToString();
						voMasLoc.MasterLocationID = int.Parse(drwResult[MST_MasterLocationTable.MASTERLOCATIONID_FLD].ToString());
						CreateDataSet();
						gridWOClose.DataSource = dstGridData.Tables[0];
						FormControlComponents.RestoreGridLayout(gridWOClose, dtbGridLayOut);
					}
					txtMasLoc.Text = drwResult[MST_MasterLocationTable.CODE_FLD].ToString();
					voMasLoc.MasterLocationID = int.Parse(drwResult[MST_MasterLocationTable.MASTERLOCATIONID_FLD].ToString());
				}
				else
				{
					txtMasLoc.Focus();
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
				// displays the error message.
				PCSMessageBox.Show(ErrorCode.OTHER_ERROR,MessageBoxIcon.Error);
				// log message.
				try
				{
					Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
				}
				catch
				{
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION,MessageBoxIcon.Error);
				}
			}
		}

		/// <summary>
		/// btnSearch_Click
		/// </summary>
		/// <author>Trada</author>
		/// <date>Thursday, June 9 2005</date>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected virtual void btnSearch_Click(object sender, System.EventArgs e)
		{
			try
            {
                if (dtmPostDate.Value.ToString() == string.Empty)
                {
                    PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Warning);
                    dtmPostDate.Focus();
                    dtmPostDate.Select();
                    return;
                }
                //Check date in period
                if (!FormControlComponents.CheckDateInCurrentPeriod(DateTime.Parse(dtmPostDate.Value.ToString())))
                {
                    PCSMessageBox.Show(ErrorCode.MESSAGE_RGV_POSTDATE_PERIOD, MessageBoxIcon.Warning);
                    dtmPostDate.Focus();
                    dtmPostDate.Select();
                    return;
                }
                DateTime pdtmFromDueDate = new DateTime();
                DateTime pdtmToDueDate = new DateTime();
                if (dtmFromDueDate.Value.ToString() != string.Empty)
                {
                    pdtmFromDueDate = DateTime.Parse(dtmFromDueDate.Value.ToString());
                }
                if (dtmToDueDate.Value.ToString() != string.Empty)
                {
                    pdtmToDueDate = DateTime.Parse(dtmToDueDate.Value.ToString());
                }
                if ((dtmFromDueDate.Value.ToString() != string.Empty) && (dtmToDueDate.Value.ToString() != string.Empty))
                {
                    if (pdtmFromDueDate > pdtmToDueDate)
                    {
                        PCSMessageBox.Show(ErrorCode.MESSAGE_MANUFACTURING_CLOSE_FROMDATE_SMALLER_TODATE,
                                           MessageBoxIcon.Warning);
                        dtmFromDueDate.Focus();
                        return;
                    }
                }
                if (txtMasLoc.Text == string.Empty)
                {
                    PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Warning);
                    txtMasLoc.Focus();
                    return;
                }

                //Bind data to grid
                BindDataToGrid();
            }
            catch (PCSException ex)
            {
                // Displays the error message if throwed from PCSException.
                PCSMessageBox.Show(ex.mCode, MessageBoxIcon.Error);
                _logger.Error(ex.CauseException.Message, ex.CauseException);
            }
            catch (Exception ex)
            {
                // displays the error message.
                PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxIcon.Error);
                _logger.Error(ex.Message, ex);
            }
		}

		/// <summary>
		/// chkSelectAllManuf_CheckedChanged
		/// </summary>
		/// <author>Trada</author>
		/// <date>Friday, June 10 2005</date>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void chkSelectAllManuf_CheckedChanged(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".chkSelectAllManuf_CheckedChanged()";
			try
			{
				
				if ((blnStateOfCheck)&&(gridWOClose.RowCount != 0))
				{
					if (chkSelectAllManuf.Checked)
					{
						foreach (DataRow drow in dstReleaseWO.Tables[0].Rows)
						{
							if (drow.RowState != DataRowState.Deleted)
							{
								drow[SELECT] = true;
							}
						}
					}
					else
					{
						foreach (DataRow drow in dstReleaseWO.Tables[0].Rows)
						{
							if (drow.RowState != DataRowState.Deleted)
							{
								drow[SELECT] = false;
							}
						}
					}
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

		/// <summary>
		/// Close WOs has been selected
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Friday, June 10 2005</date>
		protected virtual void btnCloseWO_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnCloseWO_Click()";
			try 
			{
				
				if (dtmPostDate.Value == null)
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Error);
					dtmPostDate.Focus();
					dtmPostDate.Select();
					return;
				}
				//Check date in period
				if (!FormControlComponents.CheckDateInCurrentPeriod(DateTime.Parse(dtmPostDate.Value.ToString())))
				{
					PCSMessageBox.Show(ErrorCode.MESSAGE_RGV_POSTDATE_PERIOD);
					dtmPostDate.Focus();
					dtmPostDate.Select();
					return;
				}
				if (dtmPostDate.Text == string.Empty)
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Error);
					dtmPostDate.Focus();
					return;
				}
				if (gridWOClose.RowCount == 0)
				{
					throw new PCSException(ErrorCode.MESSAGE_PO_APPROVE_NO_DATA_IN_GRID, METHOD_NAME, null);
				}
				if (CheckDataGridToRelease())
				{
					StoreSelectedLines();
                    ManufacturingCloseBO boManuf = new ManufacturingCloseBO();
                    boManuf.CloseWorkOrderLines(DateTime.Parse(dtmPostDate.Value.ToString()), arrSelectedLines);
					if (PCSMessageBox.Show(ErrorCode.MESSAGE_AFTER_SAVE_DATA) == DialogResult.OK)
					{
						BindDataToGrid();
					}
				}
				else 
					PCSMessageBox.Show(ErrorCode.MESSAGE_RELEASE_WO_SELECT_WOLINE, MessageBoxIcon.Error);

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
				// displays the error message.
				PCSMessageBox.Show(ErrorCode.OTHER_ERROR,MessageBoxIcon.Error);
				// log message.
				try
				{
					Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
				}
				catch
				{
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION,MessageBoxIcon.Error);
				}
			}
		}
		/// <summary>
		/// CheckDataGridToRelease
		/// </summary>
		/// <author>Trada</author>
		/// <date>Thursday, June 16 2005</date>
		/// <returns>bool</returns>
		private bool CheckDataGridToRelease()
		{
            int pintNumRow = 0;
            for (int i = 0; i < gridWOClose.RowCount; i++)
            {
                if (gridWOClose[i, SELECT].ToString().Trim() != TRUE)
                {
                    pintNumRow++;
                }
            }
            //there is not any rows checked
		    return pintNumRow != gridWOClose.RowCount;
		}

		/// <summary>
		/// btnHelp_Click
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnHelp_Click(object sender, System.EventArgs e)
		{
		
		}

		/// <summary>
		/// btnClose_Click
		/// </summary>
		/// <author>Trada</author>
		/// <date
		/// Thursday, June 9, 2005
		/// </date>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnClose_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}
		
        /// <summary>
		/// All selected work order line to be closed
		/// </summary>
		protected ArrayList arrSelectedLines;
		/// <summary>
		/// Bind data to grid
		/// </summary>
		/// <author>Trada</author>
		/// <date>Monday, June 13 2005</date>
		protected  virtual void BindDataToGrid()
		{
			int pintCCN = int.Parse(cboCCN.SelectedValue.ToString());
			int pintMasLoc = voMasLoc.MasterLocationID;
			DateTime pdtmFromDueDate = DateTime.MinValue;
            DateTime pdtmToDueDate = DateTime.MinValue;
            if (dtmFromDueDate.Value != null && !dtmFromDueDate.ValueIsDbNull)
            {
                pdtmFromDueDate = (DateTime)dtmFromDueDate.Value;
            }
            if (dtmToDueDate.Value != null && !dtmToDueDate.ValueIsDbNull)
            {
                pdtmToDueDate = (DateTime)dtmToDueDate.Value;
            }

            //List release WOs
            ManufacturingCloseBO boManufacturingClose = new ManufacturingCloseBO();
            dstReleaseWO = boManufacturingClose.SearchReleasedWO(pintCCN, pintMasLoc, pdtmFromDueDate, pdtmToDueDate);

            foreach (DataRow drow in dstReleaseWO.Tables[0].Rows)
            {
                drow[SELECT] = false;
            }
            gridWOClose.DataSource = dstReleaseWO.Tables[0];
            FormControlComponents.RestoreGridLayout(gridWOClose, dtbGridLayOut);
            gridWOClose.Splits[0].DisplayColumns[SELECT].DataColumn.ValueItems.Presentation = C1.Win.C1TrueDBGrid.PresentationEnum.CheckBox;
            gridWOClose.Splits[0].DisplayColumns[SELECT].Style.HorizontalAlignment = AlignHorzEnum.Center;

            //Lock all columns in grid
            gridWOClose.Splits[0].DisplayColumns[PRO_WorkOrderMasterTable.WORKORDERNO_FLD].Locked = true;
            gridWOClose.Splits[0].DisplayColumns[PRO_WorkOrderDetailTable.LINE_FLD].Locked = true;
            gridWOClose.Splits[0].DisplayColumns[OPENQUANTITY].Locked = true;
            gridWOClose.Splits[0].DisplayColumns[ITM_CategoryTable.TABLE_NAME + ITM_CategoryTable.CODE_FLD].Locked = true;
            gridWOClose.Columns[OPENQUANTITY].NumberFormat = Constants.EDIT_NUM_MASK;
            gridWOClose.Splits[0].DisplayColumns[PARTNUMBER].Locked = true;
            gridWOClose.Splits[0].DisplayColumns[CAPTION_UM].Locked = true;
            gridWOClose.Splits[0].DisplayColumns[PRO_WorkOrderDetailTable.STARTDATE_FLD].Locked = true;
            gridWOClose.Columns[PRO_WorkOrderDetailTable.STARTDATE_FLD].NumberFormat = Constants.DATETIME_FORMAT_HOUR;
            gridWOClose.Splits[0].DisplayColumns[PRO_WorkOrderDetailTable.DUEDATE_FLD].Locked = true;
            gridWOClose.Columns[PRO_WorkOrderDetailTable.DUEDATE_FLD].NumberFormat = Constants.DATETIME_FORMAT_HOUR;
            gridWOClose.Splits[0].DisplayColumns[ITM_ProductTable.DESCRIPTION_FLD].Locked = true;
            gridWOClose.Splits[0].DisplayColumns[ITM_ProductTable.REVISION_FLD].Locked = true;

            //Invisible some columns 
            gridWOClose.Splits[0].DisplayColumns[PRO_WorkOrderDetailTable.WORKORDERDETAILID_FLD].Visible = false;
            dstReleaseWO.AcceptChanges();
            //Set new status for chkSelectAll
            if (gridWOClose.RowCount == 0)
            {
                chkSelectAllManuf.Checked = false;
            }
            else
            {
                for (int i = 0; i < gridWOClose.RowCount; i++)
                {
                    if (gridWOClose[i, SELECT].ToString().Trim() != TRUE)
                    {
                        chkSelectAllManuf.Checked = false;
                        return;
                    }
                }
                chkSelectAllManuf.Checked = true;
            }
		}

		/// <summary>
		/// CreateDataSet
		/// </summary>
		/// <author>Trada</author>
		/// <Date>Thursday, October 13 2005</Date>
		private void CreateDataSet()
		{
            dstGridData = new DataSet();
            dstGridData.Tables.Add(MANCLOSE_TABLE);
            dstGridData.Tables[MANCLOSE_TABLE].Columns.Add(SELECT);
            dstGridData.Tables[MANCLOSE_TABLE].Columns.Add(PRO_WorkOrderMasterTable.WORKORDERNO_FLD);
            dstGridData.Tables[MANCLOSE_TABLE].Columns.Add(PRO_WorkOrderDetailTable.LINE_FLD);
            dstGridData.Tables[MANCLOSE_TABLE].Columns.Add(PARTNUMBER);
            dstGridData.Tables[MANCLOSE_TABLE].Columns.Add(ITM_ProductTable.DESCRIPTION_FLD);
            dstGridData.Tables[MANCLOSE_TABLE].Columns.Add(ITM_ProductTable.REVISION_FLD);
            dstGridData.Tables[MANCLOSE_TABLE].Columns.Add(OPENQUANTITY);
            dstGridData.Tables[MANCLOSE_TABLE].Columns.Add(CAPTION_UM);
            dstGridData.Tables[MANCLOSE_TABLE].Columns.Add(PRO_WorkOrderDetailTable.STARTDATE_FLD);
            dstGridData.Tables[MANCLOSE_TABLE].Columns.Add(PRO_WorkOrderDetailTable.DUEDATE_FLD);
		}
		/// <summary>
		/// foreach row in grid check:
		/// if (Select.Checked = true)
		/// {
		/// - get WorkOrderDetailID of current line
		/// - put WorkOrderDetailID into an ArrayList (arrSelectedLines)
		/// }
		/// </summary>
		/// <author>Trada</author>
		/// <date>Friday, June 10 2005</date>
		protected void StoreSelectedLines()
		{
            arrSelectedLines = new ArrayList();
            for (int i = 0; i < gridWOClose.RowCount; i++)
            {
                if (gridWOClose[i, SELECT].ToString().Trim() == TRUE)
                {
                    int pintWOId;
                    pintWOId = int.Parse(gridWOClose[i, PRO_WorkOrderDetailTable.WORKORDERDETAILID_FLD].ToString().Trim());
                    arrSelectedLines.Add(pintWOId);
                }

            }
		}
		/// <summary>
		/// txtMasLoc_Leave
		/// </summary>
		/// <author>Trada</author>
		/// <date>Friday, June 10 2005</date>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void txtMasLoc_Leave(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtMasLoc_Leave()";
			try
			{
				Hashtable htbCriteria = new Hashtable();
				DataRowView drwResult = null;
				//User has selected CCN
				if (txtMasLoc.Text.Trim() == string.Empty)
				{
					return;
				}
				if (cboCCN.SelectedIndex != -1)
				{
					htbCriteria.Add(MST_MasterLocationTable.CCNID_FLD, cboCCN.SelectedValue);	
				}
				else //User has not selected CCN
				{
					htbCriteria.Add(MST_MasterLocationTable.CCNID_FLD, 0);
				}
				drwResult = FormControlComponents.OpenSearchForm(MST_MasterLocationTable.TABLE_NAME, MST_MasterLocationTable.CODE_FLD, txtMasLoc.Text, htbCriteria, false);
				if (drwResult != null)
				{
					if (voMasLoc.MasterLocationID != int.Parse(drwResult[MST_MasterLocationTable.MASTERLOCATIONID_FLD].ToString()))
					{
						txtMasLoc.Text = drwResult[MST_MasterLocationTable.CODE_FLD].ToString();
						voMasLoc.MasterLocationID = int.Parse(drwResult[MST_MasterLocationTable.MASTERLOCATIONID_FLD].ToString());
						CreateDataSet();
						gridWOClose.DataSource = dstGridData.Tables[0];
						FormControlComponents.RestoreGridLayout(gridWOClose, dtbGridLayOut);
					}
					txtMasLoc.Text = drwResult[MST_MasterLocationTable.CODE_FLD].ToString();
					voMasLoc.MasterLocationID = int.Parse(drwResult[MST_MasterLocationTable.MASTERLOCATIONID_FLD].ToString());
				}
				else
				{
					txtMasLoc.Focus();
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
				// displays the error message.
				PCSMessageBox.Show(ErrorCode.OTHER_ERROR,MessageBoxIcon.Error);
				// log message.
				try
				{
					Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
				}
				catch
				{
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION,MessageBoxIcon.Error);
				}
			}
		}
		/// <summary>
		/// txtMasLoc_KeyDown
		/// </summary>
		/// <author>Trada</author>
		/// <date>Friday, June 10 2005</date>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void txtMasLoc_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if (e.KeyCode == Keys.F4)
			{
				btnSearchMasLoc_Click(sender, e);	
			}
		}
		/// <summary>
		/// CheckOrNochkCheckAll
		/// </summary>
		/// <author>Trada</author>
		/// <date>Friday, June 10 2005</date>
		private void CheckOrNochkCheckAll()
		{
			for (int i =0; i <gridWOClose.RowCount; i++)
			{
				if (gridWOClose[i, SELECT].ToString().Trim() != TRUE)
				{
					chkSelectAllManuf.Checked = false;
					return;
				}
			}
			chkSelectAllManuf.Checked = true;
		}
		/// <summary>
		/// gridWOClose_AfterColEdit
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Friday, June 10 2005</date>
		private void gridWOClose_AfterColEdit(object sender, C1.Win.C1TrueDBGrid.ColEventArgs e)
		{
			if (e.Column.DataColumn.DataField == SELECT)
			{
				CheckOrNochkCheckAll();
			}
		}
		/// <summary>
		/// ManufacturingClose_KeyDown
		/// </summary>
		/// <author>Trada</author>
		/// <date>Monday, June 13 2005</date>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ManufacturingClose_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			
		}

		/// <summary>
		/// chkSelectAllManuf_Enter
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Tuesday, June 13 2005</date>
		private void chkSelectAllManuf_Enter(object sender, System.EventArgs e)
		{
			blnStateOfCheck = true;
		}

		/// <summary>
		/// chkSelectAllManuf_Leave
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void chkSelectAllManuf_Leave(object sender, System.EventArgs e)
		{
			blnStateOfCheck = false;
		}
		
		/// <summary>
		/// ManufacturingClose_Closing
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Thursday, October 13 2005</date>
		private void ManufacturingClose_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".ManufacturingClose_Closing()";
			try 
			{
				if ((dstReleaseWO != null) && (dstReleaseWO.GetChanges() != null))
				{
					DialogResult confirmDialog = PCSMessageBox.Show(ErrorCode.MESSAGE_QUESTION_RELEASE_BEFORE_CLOSE, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, CLOSE);
					switch (confirmDialog)
					{
						case DialogResult.Yes:
							//Save before exit
							btnCloseWO_Click(sender, new EventArgs());
							break;
						case DialogResult.No:
							break;
						case DialogResult.Cancel:
							e.Cancel = true;
							break;
					}
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
				// displays the error message.
				PCSMessageBox.Show(ErrorCode.OTHER_ERROR,MessageBoxIcon.Error);
				// log message.
				try
				{
					Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
				}
				catch
				{
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION,MessageBoxIcon.Error);
				}
			}
		}
	}
}
