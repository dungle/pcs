using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using C1.Win.C1Input;
using C1.Win.C1List;
using PCSComUtils.Common;
using PCSComUtils.Common.BO;
using PCSComUtils.MasterSetup.BO;
using PCSComUtils.MasterSetup.DS;
using PCSComUtils.PCSExc;
using PCSUtils.Log;
using PCSUtils.Utils;
using PCSComUtils.DataContext;
using System.IO;
using System.Text;

namespace PCSUtils.MasterSetup.SystemTable
{
    public partial class ViewData : Form
    {
        public DataSet dset = new DataSet();
        public string strKeyWord = string.Empty;
        public string strTable = string.Empty;
        private int iTotalRecord = 0;//Total record
        private int iTotalPage = 0;//Total Page
        private List<PCSComUtils.Common.DS.ItemVO> list = new List<PCSComUtils.Common.DS.ItemVO>();
        public ArrayList arrID = new ArrayList();
        private int iCurrentPage = 1;
        public bool SelectMultiRow = false;
        private int iKeyOfTable = 0;
        private string strFiedValueSearch = string.Empty;
        private Hashtable mHashtable = null;
        private DataRowView drvReturnDataRowView;// Result of search
        private DataRow[] ReturnMutiRow = null;//Result return multi row
        private string strFiedName = string.Empty;
        private string strFiedValue = string.Empty;
        LikeCondition enmLike = LikeCondition.StartWith;
        private string LastCondition = string.Empty;
        public string strCondition = string.Empty;//Condition of search all       
        public string strOnlyHashTable = string.Empty;//Condition of search in HashTable
        string ValueFilter = string.Empty;
        int Position = 0;// Posittion within in list
        public int KeyofTable
        {
            set { iKeyOfTable = value; }
            get { return iKeyOfTable; }
        }
        public int TotalRecord
        {
            set { iTotalRecord = value; }
            get { return iTotalRecord; }
        }
        public DataRowView ReturnDataRowView
        {
            get { return drvReturnDataRowView; }
        }
        public DataRow[] SelectedRows
        {
            get { return ReturnMutiRow; }
        }
        public int GetTotalPage
        {
            set { iTotalPage = value; }
            get { return iTotalPage; }
        }
        public ViewData()
        {
            InitializeComponent();
        }
        //Cuonglv
        public ViewData(string strFieldName, string strFilterValue, string strKeyofTable, string strTableNameOrView, Hashtable htbOrtherFilterCondition)
        {
            InitializeComponent();
            strFiedName = strFieldName;
            strFiedValue = strFilterValue;
            strTable = strTableNameOrView;
            strKeyWord = strKeyofTable;
            mHashtable = htbOrtherFilterCondition;
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //Load form
        private void SearchParty_Load(object sender, EventArgs e)
        {
            if (dset != null && dset.Tables.Count > 0)
            {
                InitCombobox();
                BindData();
                return;
            }
            if (string.IsNullOrEmpty(strKeyWord.Trim()))
            {
                MessageBox.Show("Hệ thống bị lỗi  xin vui lòng chuyền khóa chính của bảng", SystemProperty.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            strCondition = GenerateFilterConditionToSQL(strFiedValue, strFiedName, mHashtable);
            MST_SearchPartyBO objBO = new MST_SearchPartyBO();
            TotalRecord = objBO.GetRowCount(strTable, strCondition);
            GetNumberOfPage();
            dset = objBO.GetList(strTable, strKeyWord, SelectMultiRow, strCondition, iCurrentPage, PCSComUtils.Common.Constants.CountPage);
            InitCombobox();
            BindData();
            this.Text = strTable;
        }
        // Get number of page
        private void GetNumberOfPage()
        {
            list.Clear();
            if (TotalRecord % PCSComUtils.Common.Constants.CountPage > 0)
                GetTotalPage = TotalRecord / PCSComUtils.Common.Constants.CountPage + 1;
            else
                GetTotalPage = TotalRecord / PCSComUtils.Common.Constants.CountPage;
        }

        private void InitCombobox()
        {
            if (GetTotalPage > 1)
            {
                lblPage.Visible = cmbPage.Visible = btnMove.Visible = true;
                list = new List<PCSComUtils.Common.DS.ItemVO>();
                for (int i = 1; i <= GetTotalPage; i++)
                {
                    PCSComUtils.Common.DS.ItemVO obj = new PCSComUtils.Common.DS.ItemVO(i, i.ToString());
                    list.Add(obj);
                }
                cmbPage.DataSource = list;
                cmbPage.ValueMember = "Values";
                cmbPage.DisplayMember = "Values";
            }
            else
            { lblPage.Visible = cmbPage.Visible = btnMove.Visible = false; }
        }
        private string GenerateFilterConditionToSQL(string strFilterFieldValue, string strFilterFieldName, Hashtable htbOrtherFilterCondition)
        {
            StringBuilder strFilterCondition = new StringBuilder();
            if (htbOrtherFilterCondition != null)
            {
                var myEnumerator = htbOrtherFilterCondition.GetEnumerator();
                while (myEnumerator.MoveNext())
                {
                    if (myEnumerator.Value == DBNull.Value)
                    {
                        if (strFilterCondition.Length > 0)
                            strFilterCondition.Append(" AND ");
                        strFilterCondition.Append(strTable + "." + myEnumerator.Key.ToString().Trim());
                        strFilterCondition.Append("IS NULL");
                    }
                    else if (myEnumerator.Value.ToString().ToUpper().Contains("IS NOT NULL"))
                    {
                        if (strFilterCondition.Length > 0)
                            strFilterCondition.Append(" AND ");
                        strFilterCondition.Append(strTable + "." + myEnumerator.Key.ToString().Trim());
                        strFilterCondition.Append("IS NOT NULL");
                    }
                    else
                    {
                        if (strFilterCondition.Length > 0)
                            strFilterCondition.Append(" AND ");
                        strFilterCondition.Append(strTable + "." + myEnumerator.Key.ToString().Trim());
                        strFilterCondition.Append("=N'");
                        strFilterCondition.Append(myEnumerator.Value);
                        strFilterCondition.Append("'");
                    }
                }
                strOnlyHashTable = strFilterCondition.ToString();
            }
            strFilterFieldValue = FormControlComponents.KillInjection(strFilterFieldValue);
            if (!string.IsNullOrEmpty(strFilterFieldName) && strFilterFieldValue != string.Empty)
            {
                if (strFilterCondition.Length > 0)
                    strFilterCondition.Append(" AND ");
                strFilterCondition.Append(strTable + "." + strFilterFieldName);
                strFilterCondition.Append(" LIKE N'%");
                strFilterCondition.Append(strFilterFieldValue.Replace("'", "''"));
                strFilterCondition.Append("%'");
            }
            #region /// HACKED: Thachnn: fix bug injection

            StringBuilder sql = new StringBuilder();
            sql.Append(FormControlComponents.KillInjectionInLikeClause(strFilterCondition.ToString()));

            #endregion /// ENDHACKED: Thachnn: fix bug injection

            //var strConditionByRecord = Utilities.Instance.GetConditionByRecord(SystemProperty.UserName, strTableNameOrView);
            //sql.Append(strConditionByRecord);
            return sql.ToString();
        }
        //Display data into gridview when DataSet!=null
        private void BindData()
        {
            //tgridViewTable.FilterActive = true;
            tgridViewTable.FilterBar = true;
            MST_SearchPartyBO objBO = new MST_SearchPartyBO();
            tgridViewTable.EditActive = true;
            tgridViewTable.DataChanged = true;
            tgridViewTable.AllowSort = true;
            tgridViewTable.ForeColor = System.Drawing.Color.LemonChiffon;
            tgridViewTable.DataSource = dset.Tables[0];
            for (int i = 0; i < tgridViewTable.Splits[0].DisplayColumns.Count; i++)
            {
                tgridViewTable.Splits[0].DisplayColumns[i].Locked = true;
                tgridViewTable.Splits[0].DisplayColumns[i].AutoSize();
                if (dset.Tables[0].Columns[i].DataType == typeof(DateTime))
                    tgridViewTable.Splits[0].DisplayColumns[i].DataColumn.NumberFormat = Constants.DATETIME_FORMAT;
            }
            tgridViewTable.Splits[0].DisplayColumns[0].Locked = !SelectMultiRow;
            tgridViewTable.AllowUpdate = SelectMultiRow;
            tgridViewTable.AllowAddNew = false;
            tgridViewTable.AllowDelete = false;
            tgridViewTable.Row = 0;
            tgridViewTable.Col = 1;
            tgridViewTable.Focus();
        }

        //Select row
        private void tgridViewTable_ButtonClick(object sender, C1.Win.C1TrueDBGrid.ColEventArgs e)
        {
            KeyofTable = Convert.ToInt32(tgridViewTable[tgridViewTable.Row, strKeyWord].ToString());
            this.Close();
        }
        private void tgridViewTable_DoubleClick(object sender, EventArgs e)
        {
            GetDataRow();
        }

        //Generate to Command SQL
        private void GetAll()
        {
            strCondition = strOnlyHashTable;
            MST_SearchPartyBO objBO = new MST_SearchPartyBO();
            iCurrentPage = 1;
            TotalRecord = objBO.GetRowCount(strTable, strCondition);
            GetNumberOfPage();
            dset = objBO.GetList(strTable, strKeyWord, SelectMultiRow, strCondition, iCurrentPage, PCSComUtils.Common.Constants.CountPage);
            BindData();
        }

        private void GetDataRow()
        {
            if (dset.Tables[0].Rows.Count <= 0) return;
            if (dset.Tables[0].Select(dset.Tables[0].DefaultView.RowFilter).Length < 1) return;
            if (tgridViewTable.Col.ToString() != "0")
            {
                try
                {
                    KeyofTable = Convert.ToInt32(tgridViewTable[tgridViewTable.Row, strKeyWord].ToString());
                }
                catch
                { }
                var cm = (CurrencyManager)tgridViewTable.BindingContext[tgridViewTable.DataSource];
                drvReturnDataRowView = (DataRowView)cm.Current;
                this.Close();
            }
            else
            {
                if (SelectMultiRow)
                {
                    ReturnMutiRow = dset.Tables[0].Select(" Checks='True' ");
                    this.Close();
                }
                else
                {
                    KeyofTable = Convert.ToInt32(tgridViewTable[tgridViewTable.Row, strKeyWord].ToString());
                    var cm = (CurrencyManager)tgridViewTable.BindingContext[tgridViewTable.DataSource];
                    drvReturnDataRowView = (DataRowView)cm.Current;
                    this.Close();
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            GetDataRow();
        }

        private void tgridViewTable_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F9)
            { ExportDataToExcel(); return; }
            if (e.KeyCode == Keys.F2)
            {
                iCurrentPage = 1;
                strCondition = strOnlyHashTable;
                MST_SearchPartyBO objBO = new MST_SearchPartyBO();
                TotalRecord = objBO.GetRowCount(strTable, strCondition);
                GetNumberOfPage();
                InitCombobox();
                dset = objBO.GetList(strTable, strKeyWord, SelectMultiRow, strCondition, iCurrentPage, PCSComUtils.Common.Constants.CountPage);
                BindData();
                ValueFilter = string.Empty; return;
            }
            if (GetTotalPage > 1 && (e.KeyCode == Keys.PageDown || e.KeyCode == Keys.PageUp || e.KeyCode == Keys.Home || e.KeyCode == Keys.End))
            {
                bool bContinue = false;
                if (e.KeyCode == Keys.PageDown && GetTotalPage > iCurrentPage) { iCurrentPage++; bContinue = true; }
                if (e.KeyCode == Keys.PageUp && iCurrentPage > 1) { iCurrentPage--; bContinue = true; }
                if (e.KeyCode == Keys.Home && iCurrentPage > 1) { iCurrentPage = 1; bContinue = true; }
                if (e.KeyCode == Keys.End && iCurrentPage < GetTotalPage) { iCurrentPage = GetTotalPage; bContinue = true; }
                if (bContinue)
                {
                    cmbPage.SelectedIndex = iCurrentPage - 1;
                    MST_SearchPartyBO objBO = new MST_SearchPartyBO();
                    dset = objBO.GetList(strTable, strKeyWord, SelectMultiRow, strCondition, iCurrentPage, PCSComUtils.Common.Constants.CountPage);
                    BindData();
                }
            }
            //MessageBox.Show(tgridViewTable.Row.ToString());
            //e.KeyCode == Keys.F4
            if (tgridViewTable.FilterBar == true && !string.IsNullOrEmpty(ValueFilter) && e.KeyCode == Keys.Enter)
            {
                const string METHOD_NAME = ".tgridViewTable_KeyDown()";
                try
                {
                    if (sender.Equals(mniContain))
                        enmLike = LikeCondition.Contain;
                    else enmLike = sender.Equals(mniEndWith) ? LikeCondition.EndWith : LikeCondition.StartWith;
                    string strColoumName = tgridViewTable.Columns[tgridViewTable.Col].Caption;
                    if (tgridViewTable.Col == 0) return;
                    string strColoumValue = string.Empty;
                    strCondition = strOnlyHashTable;
                    if (strCondition != null && strCondition.Length > 0) strCondition += " AND ";
                    strCondition += strColoumName + " like N'%" + ValueFilter + "%' ";
                    iCurrentPage = 1;
                    strCondition = FormControlComponents.KillInjectionInLikeClause(strCondition);
                    MST_SearchPartyBO objBO = new MST_SearchPartyBO();
                    TotalRecord = objBO.GetRowCount(strTable, strCondition);
                    GetNumberOfPage();
                    InitCombobox();
                    dset = objBO.GetList(strTable, strKeyWord, SelectMultiRow, strCondition, iCurrentPage, PCSComUtils.Common.Constants.CountPage);
                    BindData();
                    ValueFilter = string.Empty;
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
                return;
            }
            if (e.KeyCode == Keys.Enter)
            {
                GetDataRow();
            }
        }

        private void tgridViewTable_AfterFilter(object sender, C1.Win.C1TrueDBGrid.FilterEventArgs e)
        {
            int intPos = e.Condition.IndexOf("Like '");

            if (intPos > 0)
            {
                intPos += "Like '".Length;
                int intEndPos = e.Condition.IndexOf("'", intPos);
                string strValue = e.Condition.Substring(intPos, intEndPos - intPos);
                ValueFilter = strValue.Replace("*", string.Empty);
            }

        }
        private void ExportDataToExcel()
        {
            const string METHOD_NAME = ".ExportDataToExcel()";
            try
            {
                SaveFileDialog saveFile = new SaveFileDialog();

                saveFile.DefaultExt = "*.xls";
                saveFile.Filter = "xls File|*.xls| All File|*.*";

                if (saveFile.ShowDialog() == System.Windows.Forms.DialogResult.OK && saveFile.FileName.Length > 0)
                {
                    try
                    {	//tgridViewTable.ExportToExcel(saveFile.FileName);
                        if (System.IO.File.Exists(saveFile.FileName))
                            File.Delete(saveFile.FileName);
                        tgridViewTable.ExportToDelimitedFile(saveFile.FileName, C1.Win.C1TrueDBGrid.RowSelectorEnum.AllRows, "\t", string.Empty, " ", true, "Unicode");
                        PCSMessageBox.Show(ErrorCode.MESSAGE_TASK_COMPLETED, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, new string[] { "Exporting" });
                    }
                    catch
                    {
                        PCSMessageBox.Show(ErrorCode.CAN_NOT_READ_EXCEL_FILE, MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void btnMove_Click(object sender, EventArgs e)
        {
            if (iCurrentPage == cmbPage.SelectedIndex + 1) return;
            iCurrentPage = cmbPage.SelectedIndex + 1;
            MST_SearchPartyBO objBO = new MST_SearchPartyBO();
            dset = objBO.GetList(strTable, strKeyWord, SelectMultiRow, strCondition, iCurrentPage, PCSComUtils.Common.Constants.CountPage);
            BindData();
        }
    }
}
