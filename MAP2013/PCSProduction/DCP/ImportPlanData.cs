using System;
using System.Collections;
using System.Data;
using System.Windows.Forms;
using C1.Win.C1TrueDBGrid;
using PCSComProduction.DCP.BO;
using PCSComUtils.Common;
using PCSComUtils.PCSExc;
using PCSUtils.Log;
using PCSUtils.Utils;
using CancelEventArgs = System.ComponentModel.CancelEventArgs;

namespace PCSProduction.DCP
{
	/// <summary>
	/// Summary description for ImportPlanData.
	/// </summary>
	public partial class ImportPlanData : Form
	{
		const string THIS = "PCSProduction.DCP.ImportPlanData";
		
		private string strExcelFilename = "";
		private ExcelReader objExcelReader;
		private DataTable dtbData;
		private DataTable dtbGridLayout = new DataTable();
		ImportPlanDataBO boImport = new ImportPlanDataBO(); 
		
		public ImportPlanData()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
		}

		private void ImportPlanData_Load(object sender, EventArgs e)
		{
			const string METHOD_NAME = THIS + ".ImportPlanData_Load()";
			try
			{
				//Set authorization for user
				Security objSecurity = new Security();
				this.Name = THIS;
				if(objSecurity.SetRightForUserOnForm(this, SystemProperty.UserName) == 0)
				{
					this.Close();
					// You don't have the right to view this item
					PCSMessageBox.Show(ErrorCode.MESSAGE_YOU_HAVE_NO_RIGHT_TO_VIEW ,MessageBoxIcon.Warning);
					return;
				}
				foreach (C1DisplayColumn col in dgrdData.Splits[0].DisplayColumns)
					if (col.DataColumn.DataField != ITM_ProductTable.PRODUCTID_FLD)
						col.DataColumn.NumberFormat = Constants.DECIMAL_NUMBERFORMAT;
				// store grid layout
				dtbGridLayout = FormControlComponents.StoreGridLayout(dgrdData);
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

		private void btnSearchCycle_Click(object sender, EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnCycleSearch_Click()";
			try
			{
				DataRowView drwResult = null;
				Hashtable hshCondition = null;
				drwResult = FormControlComponents.OpenSearchForm(PRO_DCOptionMasterTable.TABLE_NAME,PRO_DCOptionMasterTable.CYCLE_FLD,txtCycle.Text,hshCondition);
				if (drwResult != null)
				{
					txtCycle.Text = drwResult[PRO_DCOptionMasterTable.CYCLE_FLD].ToString();
					txtCycle.Tag = drwResult[PRO_DCOptionMasterTable.DCOPTIONMASTERID_FLD].ToString();
				}
				else
				{
					txtCycle.Focus();
					txtCycle.SelectAll();
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

		private void txtCycle_Validating(object sender, CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtCycle_Validating()";
			try
			{
				if (!txtCycle.Modified)
					return;
				if (txtCycle.Text.Trim() == string.Empty)
				{
					txtCycle.Tag = null;
					return;
				}
				Hashtable htbCriterial = null;
				DataRowView drvResult = FormControlComponents.OpenSearchForm(PRO_DCOptionMasterTable.TABLE_NAME, PRO_DCOptionMasterTable.CYCLE_FLD, txtCycle.Text.Trim(), htbCriterial, false);
				if (drvResult != null)
				{
					txtCycle.Text = drvResult[PRO_DCOptionMasterTable.CYCLE_FLD].ToString();
					txtCycle.Tag = drvResult[PRO_DCOptionMasterTable.DCOPTIONMASTERID_FLD].ToString();
				}
				else
				{
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

		private void txtCycle_KeyDown(object sender, KeyEventArgs e)
		{
			if ((e.KeyCode == Keys.F4) && (btnSearchCycle.Enabled))
			{
				btnSearchCycle_Click(sender,e);
			}
		}

		private void btnProductionLine_Click(object sender, EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnProductionLine_Click()";
			try
			{
				DataRowView drwResult = null;
				Hashtable hshCondition = null;
				drwResult = FormControlComponents.OpenSearchForm(PRO_ProductionLineTable.TABLE_NAME,PRO_ProductionLineTable.CODE_FLD,txtProductionLine.Text,hshCondition);
				if (drwResult != null)
				{
					txtProductionLine.Text = drwResult[PRO_ProductionLineTable.CODE_FLD].ToString();
					txtProductionLine.Tag = drwResult[PRO_ProductionLineTable.PRODUCTIONLINEID_FLD].ToString();
				}
				else
				{
					txtProductionLine.Focus();
					txtProductionLine.SelectAll();
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

		private void txtProductionLine_Validating(object sender, CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnProductionLine_Click()";
			try
			{
				if (txtProductionLine.Modified)
				{
					if (txtProductionLine.Text.Trim().Length == 0)
					{
						txtProductionLine.Tag = null;
						return;
					}
					DataRowView drwResult = null;
					Hashtable hshCondition = null;
					drwResult = FormControlComponents.OpenSearchForm(PRO_ProductionLineTable.TABLE_NAME,PRO_ProductionLineTable.CODE_FLD,txtProductionLine.Text,hshCondition, false);
					if (drwResult != null)
					{
						txtProductionLine.Text = drwResult[PRO_ProductionLineTable.CODE_FLD].ToString();
						txtProductionLine.Tag = drwResult[PRO_ProductionLineTable.PRODUCTIONLINEID_FLD].ToString();
					}
					else
						e.Cancel = true;
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

		private void txtProductionLine_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.F4)
				btnProductionLine_Click(sender, e);
		}

		private void btnOpenFileDlg_Click(object sender, EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnOpenFileDlg_Click()";
			try
			{
				OpenFileDialog f = new OpenFileDialog(); 
				f.Filter ="Excel files | *.xls";
				f.InitialDirectory = Application.ExecutablePath;   
			
				if (f.ShowDialog()==DialogResult.OK)
					if (f.FileName != null && f.CheckFileExists==true )
					{
						strExcelFilename =f.FileName;
						txtFileName.Text = f.FileName;
						RetrieveSheetnames();
						if (this.cboSheetnames.Items.Count >0) 
							cboSheetnames.SelectedIndex =0;
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
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION);
				}
			}
			catch (Exception ex)
			{
				PCSMessageBox.Show(ErrorCode.OTHER_ERROR);
				try
				{
					Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
				}
				catch
				{
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION);
				}
			}
		}

		private void btnSave_Click(object sender, EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnSave_Click()";
			try
			{
				Cursor = Cursors.WaitCursor; 
				if (ValidateData())
				{
					if (dtbData == null)
					{
						InitExcel(ref objExcelReader);
						dtbData = objExcelReader.GetTable("A1");
						dtbData.DefaultView.Sort = "F1";
						dgrdData.DataSource=dtbData;
						FormControlComponents.RestoreGridLayout(dgrdData, dtbGridLayout);
						objExcelReader.Close();
						objExcelReader.Dispose();
						objExcelReader=null;
					}
					DataTable dtbImportData = new DataTable("A1");
					dtbImportData.Columns.Add(new DataColumn(MTR_CPOTable.PRODUCTID_FLD, typeof(int)));
					dtbImportData.Columns.Add(new DataColumn(MTR_CPOTable.WOGENERATEDID_FLD, typeof(int)));
				    for (int i = 1; i <= 31; i++)
				    {
				        dtbImportData.Columns.Add(new DataColumn("F" + i, typeof(decimal)));
				    }
					foreach (DataRow drowData in dtbData.Rows)
					{
						DataRow drowImported = dtbImportData.NewRow();
						drowImported[MTR_CPOTable.PRODUCTID_FLD] = drowData["F1"];
						drowImported[MTR_CPOTable.WOGENERATEDID_FLD] = drowData["F33"];
					    for (int i = 1; i <= 31; i++)
					    {
					        var colName = string.Format("F{0}", (i + 1));
                            drowImported["F" + i] = drowData[colName];
					    }
						dtbImportData.Rows.Add(drowImported);
					}
					int mainWorkCenterId = boImport.GetMainWorkCenter(Convert.ToInt32(txtProductionLine.Tag));
					boImport.ImportData(dtbImportData, Convert.ToInt32(txtCycle.Tag), mainWorkCenterId, Convert.ToInt32(txtShiftCode.Tag), Convert.ToDateTime(dtmMonth.Value));
					string[] strMsg = {Text};
					PCSMessageBox.Show(ErrorCode.MESSAGE_TASK_COMPLETED, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, strMsg);
				}
				Cursor = Cursors.Default;
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
			finally
			{
				Cursor = Cursors.Default;
			}
		}

		private void btnGetData_Click(object sender, EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnGetData_Click()";
			try
			{
				Cursor = Cursors.WaitCursor; 
				InitExcel(ref objExcelReader);
				dtbData = objExcelReader.GetTable("A1");
				dtbData.DefaultView.Sort = "F1";
				dgrdData.DataSource=dtbData;
				FormControlComponents.RestoreGridLayout(dgrdData, dtbGridLayout);
				
				objExcelReader.Close();
				objExcelReader.Dispose();
				objExcelReader=null;
				Cursor = Cursors.Default;
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
			finally
			{
				Cursor = Cursors.Default;
			}
		}

		private void btnClose_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void ImportPlanData_Closing(object sender, CancelEventArgs e)
		{
		
		}
		private void btnShiftCode_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnShiftCode_Click()";
			try
			{
				DataRowView drwResult = null;
				Hashtable hshCondition = null;
				drwResult = FormControlComponents.OpenSearchForm(PRO_ShiftTable.TABLE_NAME,PRO_ShiftTable.SHIFTDESC_FLD,txtShiftCode.Text,hshCondition);
				if (drwResult != null)
				{
					txtShiftCode.Text = drwResult[PRO_ShiftTable.SHIFTDESC_FLD].ToString();
					txtShiftCode.Tag = drwResult[PRO_ShiftTable.SHIFTID_FLD].ToString();
				}
				else
				{
					txtShiftCode.Focus();
					txtShiftCode.SelectAll();
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

		private void txtShiftCode_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtShiftCode_Validating()";
			try
			{
				if (!txtShiftCode.Modified)
					return;
				if (txtShiftCode.Text.Trim() == string.Empty)
				{
					txtShiftCode.Tag = null;
					return;
				}
				Hashtable htbCriterial = null;
				DataRowView drvResult = FormControlComponents.OpenSearchForm(PRO_ShiftTable.TABLE_NAME, PRO_ShiftTable.SHIFTDESC_FLD, txtShiftCode.Text.Trim(), htbCriterial, false);
				if (drvResult != null)
				{
					txtShiftCode.Text = drvResult[PRO_ShiftTable.SHIFTDESC_FLD].ToString();
					txtShiftCode.Tag = drvResult[PRO_ShiftTable.SHIFTID_FLD].ToString();
				}
				else
					e.Cancel = true;
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

		private void txtShiftCode_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if ((e.KeyCode == Keys.F4) && (btnShiftCode.Enabled))
			{
				btnShiftCode_Click(sender,e);
			}
		}
	
		private void RetrieveSheetnames()
		{
			this.cboSheetnames.Items.Clear();
			
			if (objExcelReader !=null)
			{
				objExcelReader.Dispose();
				objExcelReader=null;
			}
				
			objExcelReader = new ExcelReader();
			objExcelReader.ExcelFilename = strExcelFilename;
			objExcelReader.Headers =false;
			objExcelReader.MixedData =true;
			string[] sheetnames = this.objExcelReader.GetExcelSheetNames();
			this.cboSheetnames.Items.AddRange(sheetnames);
		}

		private void InitExcel(ref ExcelReader exr)
		{
			//Excel must be open
			if (exr == null)
			{
				exr = new ExcelReader();
				exr.ExcelFilename = strExcelFilename;
				exr.Headers =false;
				exr.MixedData =true;
			}
			if  (dtbData==null) dtbData = new DataTable("par");			
			exr.KeepConnectionOpen =true;
			
			//Check excel sheetname is selected
			if (this.cboSheetnames.SelectedIndex>-1) 
				exr.SheetName = this.cboSheetnames.Text; 
			else
				throw new PCSException(ErrorCode.MESSAGE_SELECT_SHEET, string.Empty, null);

			//Set excel sheet range
			exr.SheetRange = this.txtRange.Text; 
		}

		private bool ValidateData()
		{
			if (txtCycle.Tag == null)
			{
				PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxButtons.OK, MessageBoxIcon.Error);
				txtCycle.Focus();
				return false;
			}
			if (txtProductionLine.Tag == null)
			{
				PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxButtons.OK, MessageBoxIcon.Error);
				txtProductionLine.Focus();
				return false;
			}
			if (txtShiftCode.Tag == null)
			{
				PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxButtons.OK, MessageBoxIcon.Error);
				txtShiftCode.Focus();
				return false;
			}
			if (dtmMonth.Value == null)
			{
				PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxButtons.OK, MessageBoxIcon.Error);
				dtmMonth.Focus();
				return false;
			}
			if (txtFileName.Text.Trim() == string.Empty)
			{
				PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxButtons.OK, MessageBoxIcon.Error);
				txtFileName.Focus();
				return false;
			}
			return true;
		}
	}
}
