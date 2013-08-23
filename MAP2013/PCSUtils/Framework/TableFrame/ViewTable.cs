using System;
using System.Collections;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using C1.Win.C1Input;
using C1.Win.C1TrueDBGrid;
using C1.Win.C1TrueDBGrid.BaseGrid;
using PCSComUtils.Admin.BO;
using PCSComUtils.Common;
using PCSComUtils.Common.BO;
using PCSComUtils.Framework.TableFrame.BO;
using PCSComUtils.Framework.TableFrame.DS;
using PCSComUtils.PCSExc;
using PCSUtils.Log;
using PCSUtils.Utils;
using System.Text;

namespace PCSUtils.Framework.TableFrame
{
	/// <summary>
	/// Summary description for frmTable.
	/// </summary>
	public class ViewTable : System.Windows.Forms.Form
	{
		const int SPACE_SCROLL = 60;

		private System.Windows.Forms.Label label1;
		private System.ComponentModel.IContainer components;
		private System.Windows.Forms.ToolBar tbarViewTable;
		private C1.Win.C1TrueDBGrid.C1TrueDBGrid tgridViewTable;

		private bool blnEditUpdateData; //Determine user changed data or not. This value will be true after each action on the true dbgrid
		private string strTableID; //TableID , this value will get from the Form Parameter
		private string strTableName; //Table Name, this value will get from the form Parameter
		private DataSet dstFieldList; //store all fields for this TableName
		private DataSet dstData; //Store all data for this TableName
		private System.Windows.Forms.ImageList imglstButtonImage; //Business Object Class
		private const string THIS = "PCSUtils.Framework.TableFrame.ViewTable";
		private const string SELECT = "Select";
		private bool blnStateOfCheck = false;	

		private bool blnViewOnly; //This variable is used to determine that this form is used only for searching 
		private bool blnGetData; // This variable is used to know when user use this form to get data
		private bool blnSelectMultiRows; // This variable is used to know when user use this form to get multi rows data
		private string strReturnFieldName; //The FieldName must be used to return the value to the master 
		private string strReturnFieldValue; //The value of the ReturnFiled , the field will be set when user press enter or doublick at a specific row

		private string strFilterFieldName1; //FilterFieldName is the FieldName will be used to return its value from Grid
		private string strFilterFieldValue1; //Filter Value

		private string strFilterFieldName2; //FilterFieldName is the FieldName will be used to return its value from Grid
		private string strFilterFieldValue2; //Filter Value

		private string strWhereClause = String.Empty; //store the where clause 
		private DataRowView drvReturnDataRowView = null; 
		DataTable dtbReturnTable;

		private string strSqlSelectCommand;
		private System.Windows.Forms.Label lblViewTable_ViewOnly;
		private System.Windows.Forms.Label lblViewTable_EditUpdate; //this SQL Statement is used to get data for this table
		private string strSqlSelectUpdateCommand; //This variable is used to store the SQL for Select and Update Data

		private string strFilterString = String.Empty;
		private string strPreviousFilterString = String.Empty;

		private bool blnRunRowFilter ; // this variable is used to determine if user has run the RowFilter function before or not

		private bool blnAllowEditDateTime; // this variable is used to allow edit date time or not

		private bool blnIgnoreChange;
		//tuandm
		public DataSet userDataSource = null;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Button btnDelete;
		private System.Windows.Forms.Button btnClose;
		private System.Windows.Forms.Button bntHelp;
		private System.Windows.Forms.Button btnAdd;
		private System.Windows.Forms.Button btnSave;
		private C1.Win.C1Input.C1DateEdit dtDateTimePicker;


		private const string CONVERT_DATE_TOSTRING = "d";
		//		private const string CONVERT_TIME_TOSTRING = "t";
		//		private const string DATE_FORMAT = "dd/MM/yyyy";


		private const int DEFAULT_WIDTH = 400;
		private const int INVISIBLE_COLUMN_WIDTH = 1;	// specify the value to hide the column in ViewTable

		//private const int INT_NORMAL = 0;
		private const int INT_UPPER = 1;
		private const int INT_LOWER = 2;
		
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Panel pnlToolbar;
		private System.Windows.Forms.ToolBarButton btnF2;
		private System.Windows.Forms.ToolBarButton btnF3;
		private System.Windows.Forms.ToolBarButton btnF4;
		private System.Windows.Forms.ToolBarButton btnF5;
		private System.Windows.Forms.ToolBarButton seperator1;
		private System.Windows.Forms.ToolBarButton btnF6;
		private System.Windows.Forms.ToolBarButton btnF8;
		private System.Windows.Forms.ToolBarButton seperator2;
		private System.Windows.Forms.ToolBarButton btnF9;
		private System.Windows.Forms.ToolBarButton btnF10;
		private System.Windows.Forms.ToolBarButton btnF1;
		private System.Windows.Forms.ToolBarButton btnF11;
		private System.Windows.Forms.CheckBox chkSelectAll;
		// added by DungLa: store the working object
		private sys_TableVO voSysTable = new sys_TableVO();

		//added by DuongNA to mark added state
		//private bool m_blnJustAddNew = false;

		private void ChangeFormWidth (string pstrTableName)
		{
			ViewTableBO objViewTableBO = new ViewTableBO();
			int intFormWidth = objViewTableBO.GetTotalColumnWidth(pstrTableName);
			intFormWidth += SPACE_SCROLL;
			if (intFormWidth > Screen.PrimaryScreen.WorkingArea.Width)
			{
				intFormWidth = Screen.PrimaryScreen.WorkingArea.Width;
			}
			if (intFormWidth < DEFAULT_WIDTH )
			{
				intFormWidth = DEFAULT_WIDTH;
			}

			if(intFormWidth > Screen.PrimaryScreen.WorkingArea.Width - 100)
			{
				intFormWidth = Screen.PrimaryScreen.WorkingArea.Width - 100;
			}				
			this.CenterToScreen();

			Size formSize= this.Size;
			formSize.Width = intFormWidth;
			this.Size = formSize;
			this.CenterToParent();
			
			tgridViewTable.RowHeight = Constants.DEFAULT_ROW_HEIGHT;
			tgridViewTable.MarqueeStyle = MarqueeEnum.HighlightCell;
			tgridViewTable.HighLightRowStyle.BackColor = Color.FromArgb((byte)Constants.BACKGROUND_COLOUR_R, (byte)Constants.BACKGROUND_COLOUR_G, (byte)Constants.BACKGROUND_COLOUR_B);
			tgridViewTable.HighLightRowStyle.ForeColor = Color.FromArgb((byte)Constants.FORE_COLOUR_R, (byte)Constants.FORE_COLOUR_R, (byte)Constants.FORE_COLOUR_R);

			//ViewTable_Load(this,null);
		}
		public ViewTable()
		{
			const string METHOD_NAME = THIS + ".ViewTable()";
			try
			{
				InitializeComponent();
				ChangeFormWidth (strTableName);
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

		public ViewTable(string pstrTableID, string pstrTableName)
		{
			const string METHOD_NAME = THIS + ".ViewTable()";
			try
			{
				strTableID = pstrTableID;
				strTableName = pstrTableName;
				InitializeComponent();
				ChangeFormWidth (strTableName);
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

		public ViewTable(string pstrTableName)
		{
			const string METHOD_NAME = THIS + ".ViewTable(string pstrTableName)";
			try 
			{
				strTableName = pstrTableName;
				InitializeComponent();
				ChangeFormWidth (strTableName);
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
				this.Close();
				return;
			}

		}
		/// <summary>
		/// ViewTable
		/// </summary>
		/// <param name="pstrTableName"></param>
		/// <param name="pblnSelectMultiRows"></param>
		/// <author>Trada</author>
		/// <date>Friday, December 23 2005</date>
		public ViewTable(string pstrTableName, bool pblnSelectMultiRows)
		{
			const string METHOD_NAME = THIS + ".ViewTable(string pstrTableName, bool pblnSelectMultiRows)";
			try 
			{
				strTableName = pstrTableName;
				blnSelectMultiRows = pblnSelectMultiRows;
				InitializeComponent();
				ChangeFormWidth (strTableName);
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
				this.Close();
				return;
			}

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

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(ViewTable));
			this.label1 = new System.Windows.Forms.Label();
			this.tgridViewTable = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
			this.tbarViewTable = new System.Windows.Forms.ToolBar();
			this.btnF1 = new System.Windows.Forms.ToolBarButton();
			this.btnF2 = new System.Windows.Forms.ToolBarButton();
			this.btnF3 = new System.Windows.Forms.ToolBarButton();
			this.btnF4 = new System.Windows.Forms.ToolBarButton();
			this.btnF5 = new System.Windows.Forms.ToolBarButton();
			this.seperator1 = new System.Windows.Forms.ToolBarButton();
			this.btnF6 = new System.Windows.Forms.ToolBarButton();
			this.btnF8 = new System.Windows.Forms.ToolBarButton();
			this.seperator2 = new System.Windows.Forms.ToolBarButton();
			this.btnF9 = new System.Windows.Forms.ToolBarButton();
			this.btnF10 = new System.Windows.Forms.ToolBarButton();
			this.btnF11 = new System.Windows.Forms.ToolBarButton();
			this.imglstButtonImage = new System.Windows.Forms.ImageList(this.components);
			this.lblViewTable_ViewOnly = new System.Windows.Forms.Label();
			this.lblViewTable_EditUpdate = new System.Windows.Forms.Label();
			this.btnOK = new System.Windows.Forms.Button();
			this.btnDelete = new System.Windows.Forms.Button();
			this.btnClose = new System.Windows.Forms.Button();
			this.bntHelp = new System.Windows.Forms.Button();
			this.btnAdd = new System.Windows.Forms.Button();
			this.btnSave = new System.Windows.Forms.Button();
			this.dtDateTimePicker = new C1.Win.C1Input.C1DateEdit();
			this.label2 = new System.Windows.Forms.Label();
			this.pnlToolbar = new System.Windows.Forms.Panel();
			this.chkSelectAll = new System.Windows.Forms.CheckBox();
			((System.ComponentModel.ISupportInitialize)(this.tgridViewTable)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dtDateTimePicker)).BeginInit();
			this.pnlToolbar.SuspendLayout();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AccessibleDescription = resources.GetString("label1.AccessibleDescription");
			this.label1.AccessibleName = resources.GetString("label1.AccessibleName");
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("label1.Anchor")));
			this.label1.AutoSize = ((bool)(resources.GetObject("label1.AutoSize")));
			this.label1.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("label1.Dock")));
			this.label1.Enabled = ((bool)(resources.GetObject("label1.Enabled")));
			this.label1.Font = ((System.Drawing.Font)(resources.GetObject("label1.Font")));
			this.label1.Image = ((System.Drawing.Image)(resources.GetObject("label1.Image")));
			this.label1.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label1.ImageAlign")));
			this.label1.ImageIndex = ((int)(resources.GetObject("label1.ImageIndex")));
			this.label1.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("label1.ImeMode")));
			this.label1.Location = ((System.Drawing.Point)(resources.GetObject("label1.Location")));
			this.label1.Name = "label1";
			this.label1.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("label1.RightToLeft")));
			this.label1.Size = ((System.Drawing.Size)(resources.GetObject("label1.Size")));
			this.label1.TabIndex = ((int)(resources.GetObject("label1.TabIndex")));
			this.label1.Text = resources.GetString("label1.Text");
			this.label1.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label1.TextAlign")));
			this.label1.Visible = ((bool)(resources.GetObject("label1.Visible")));
			// 
			// tgridViewTable
			// 
			this.tgridViewTable.AccessibleDescription = resources.GetString("tgridViewTable.AccessibleDescription");
			this.tgridViewTable.AccessibleName = resources.GetString("tgridViewTable.AccessibleName");
			this.tgridViewTable.AllowAddNew = ((bool)(resources.GetObject("tgridViewTable.AllowAddNew")));
			this.tgridViewTable.AllowArrows = ((bool)(resources.GetObject("tgridViewTable.AllowArrows")));
			this.tgridViewTable.AllowColMove = ((bool)(resources.GetObject("tgridViewTable.AllowColMove")));
			this.tgridViewTable.AllowColSelect = ((bool)(resources.GetObject("tgridViewTable.AllowColSelect")));
			this.tgridViewTable.AllowDelete = ((bool)(resources.GetObject("tgridViewTable.AllowDelete")));
			this.tgridViewTable.AllowDrag = ((bool)(resources.GetObject("tgridViewTable.AllowDrag")));
			this.tgridViewTable.AllowFilter = ((bool)(resources.GetObject("tgridViewTable.AllowFilter")));
			this.tgridViewTable.AllowHorizontalSplit = ((bool)(resources.GetObject("tgridViewTable.AllowHorizontalSplit")));
			this.tgridViewTable.AllowRowSelect = ((bool)(resources.GetObject("tgridViewTable.AllowRowSelect")));
			this.tgridViewTable.AllowSort = ((bool)(resources.GetObject("tgridViewTable.AllowSort")));
			this.tgridViewTable.AllowUpdate = ((bool)(resources.GetObject("tgridViewTable.AllowUpdate")));
			this.tgridViewTable.AllowUpdateOnBlur = ((bool)(resources.GetObject("tgridViewTable.AllowUpdateOnBlur")));
			this.tgridViewTable.AllowVerticalSplit = ((bool)(resources.GetObject("tgridViewTable.AllowVerticalSplit")));
			this.tgridViewTable.AlternatingRows = ((bool)(resources.GetObject("tgridViewTable.AlternatingRows")));
			this.tgridViewTable.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tgridViewTable.Anchor")));
			this.tgridViewTable.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tgridViewTable.BackgroundImage")));
			this.tgridViewTable.BorderStyle = ((System.Windows.Forms.BorderStyle)(resources.GetObject("tgridViewTable.BorderStyle")));
			this.tgridViewTable.Caption = resources.GetString("tgridViewTable.Caption");
			this.tgridViewTable.CaptionHeight = ((int)(resources.GetObject("tgridViewTable.CaptionHeight")));
			this.tgridViewTable.CausesValidation = false;
			this.tgridViewTable.CellTipsDelay = ((int)(resources.GetObject("tgridViewTable.CellTipsDelay")));
			this.tgridViewTable.CellTipsWidth = ((int)(resources.GetObject("tgridViewTable.CellTipsWidth")));
			this.tgridViewTable.ChildGrid = ((C1.Win.C1TrueDBGrid.C1TrueDBGrid)(resources.GetObject("tgridViewTable.ChildGrid")));
			this.tgridViewTable.CollapseColor = ((System.Drawing.Color)(resources.GetObject("tgridViewTable.CollapseColor")));
			this.tgridViewTable.ColumnFooters = ((bool)(resources.GetObject("tgridViewTable.ColumnFooters")));
			this.tgridViewTable.ColumnHeaders = ((bool)(resources.GetObject("tgridViewTable.ColumnHeaders")));
			this.tgridViewTable.DefColWidth = ((int)(resources.GetObject("tgridViewTable.DefColWidth")));
			this.tgridViewTable.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tgridViewTable.Dock")));
			this.tgridViewTable.EditDropDown = ((bool)(resources.GetObject("tgridViewTable.EditDropDown")));
			this.tgridViewTable.EmptyRows = ((bool)(resources.GetObject("tgridViewTable.EmptyRows")));
			this.tgridViewTable.Enabled = ((bool)(resources.GetObject("tgridViewTable.Enabled")));
			this.tgridViewTable.ExpandColor = ((System.Drawing.Color)(resources.GetObject("tgridViewTable.ExpandColor")));
			this.tgridViewTable.ExposeCellMode = ((C1.Win.C1TrueDBGrid.ExposeCellModeEnum)(resources.GetObject("tgridViewTable.ExposeCellMode")));
			this.tgridViewTable.ExtendRightColumn = ((bool)(resources.GetObject("tgridViewTable.ExtendRightColumn")));
			this.tgridViewTable.FetchRowStyles = ((bool)(resources.GetObject("tgridViewTable.FetchRowStyles")));
			this.tgridViewTable.FilterBar = ((bool)(resources.GetObject("tgridViewTable.FilterBar")));
			this.tgridViewTable.FlatStyle = ((C1.Win.C1TrueDBGrid.FlatModeEnum)(resources.GetObject("tgridViewTable.FlatStyle")));
			this.tgridViewTable.Font = ((System.Drawing.Font)(resources.GetObject("tgridViewTable.Font")));
			this.tgridViewTable.ForeColor = System.Drawing.Color.LemonChiffon;
			this.tgridViewTable.GroupByAreaVisible = ((bool)(resources.GetObject("tgridViewTable.GroupByAreaVisible")));
			this.tgridViewTable.GroupByCaption = resources.GetString("tgridViewTable.GroupByCaption");
			this.tgridViewTable.Images.Add(((System.Drawing.Image)(resources.GetObject("resource"))));
			this.tgridViewTable.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tgridViewTable.ImeMode")));
			this.tgridViewTable.LinesPerRow = ((int)(resources.GetObject("tgridViewTable.LinesPerRow")));
			this.tgridViewTable.Location = ((System.Drawing.Point)(resources.GetObject("tgridViewTable.Location")));
			this.tgridViewTable.MarqueeStyle = C1.Win.C1TrueDBGrid.MarqueeEnum.DottedCellBorder;
			this.tgridViewTable.Name = "tgridViewTable";
			this.tgridViewTable.PictureAddnewRow = ((System.Drawing.Image)(resources.GetObject("tgridViewTable.PictureAddnewRow")));
			this.tgridViewTable.PictureCurrentRow = ((System.Drawing.Image)(resources.GetObject("tgridViewTable.PictureCurrentRow")));
			this.tgridViewTable.PictureFilterBar = ((System.Drawing.Image)(resources.GetObject("tgridViewTable.PictureFilterBar")));
			this.tgridViewTable.PictureFooterRow = ((System.Drawing.Image)(resources.GetObject("tgridViewTable.PictureFooterRow")));
			this.tgridViewTable.PictureHeaderRow = ((System.Drawing.Image)(resources.GetObject("tgridViewTable.PictureHeaderRow")));
			this.tgridViewTable.PictureModifiedRow = ((System.Drawing.Image)(resources.GetObject("tgridViewTable.PictureModifiedRow")));
			this.tgridViewTable.PictureStandardRow = ((System.Drawing.Image)(resources.GetObject("tgridViewTable.PictureStandardRow")));
			this.tgridViewTable.PreviewInfo.AllowSizing = ((bool)(resources.GetObject("tgridViewTable.PreviewInfo.AllowSizing")));
			this.tgridViewTable.PreviewInfo.Caption = resources.GetString("tgridViewTable.PreviewInfo.Caption");
			this.tgridViewTable.PreviewInfo.Location = ((System.Drawing.Point)(resources.GetObject("tgridViewTable.PreviewInfo.Location")));
			this.tgridViewTable.PreviewInfo.Size = ((System.Drawing.Size)(resources.GetObject("tgridViewTable.PreviewInfo.Size")));
			this.tgridViewTable.PreviewInfo.ToolBars = ((bool)(resources.GetObject("tgridViewTable.PreviewInfo.ToolBars")));
			this.tgridViewTable.PreviewInfo.UIStrings.Content = ((string[])(resources.GetObject("tgridViewTable.PreviewInfo.UIStrings.Content")));
			this.tgridViewTable.PreviewInfo.ZoomFactor = ((System.Double)(resources.GetObject("tgridViewTable.PreviewInfo.ZoomFactor")));
			this.tgridViewTable.PrintInfo.MaxRowHeight = ((int)(resources.GetObject("tgridViewTable.PrintInfo.MaxRowHeight")));
			this.tgridViewTable.PrintInfo.OwnerDrawPageFooter = ((bool)(resources.GetObject("tgridViewTable.PrintInfo.OwnerDrawPageFooter")));
			this.tgridViewTable.PrintInfo.OwnerDrawPageHeader = ((bool)(resources.GetObject("tgridViewTable.PrintInfo.OwnerDrawPageHeader")));
			this.tgridViewTable.PrintInfo.PageFooter = resources.GetString("tgridViewTable.PrintInfo.PageFooter");
			this.tgridViewTable.PrintInfo.PageFooterHeight = ((int)(resources.GetObject("tgridViewTable.PrintInfo.PageFooterHeight")));
			this.tgridViewTable.PrintInfo.PageHeader = resources.GetString("tgridViewTable.PrintInfo.PageHeader");
			this.tgridViewTable.PrintInfo.PageHeaderHeight = ((int)(resources.GetObject("tgridViewTable.PrintInfo.PageHeaderHeight")));
			this.tgridViewTable.PrintInfo.PrintHorizontalSplits = ((bool)(resources.GetObject("tgridViewTable.PrintInfo.PrintHorizontalSplits")));
			this.tgridViewTable.PrintInfo.ProgressCaption = resources.GetString("tgridViewTable.PrintInfo.ProgressCaption");
			this.tgridViewTable.PrintInfo.RepeatColumnFooters = ((bool)(resources.GetObject("tgridViewTable.PrintInfo.RepeatColumnFooters")));
			this.tgridViewTable.PrintInfo.RepeatColumnHeaders = ((bool)(resources.GetObject("tgridViewTable.PrintInfo.RepeatColumnHeaders")));
			this.tgridViewTable.PrintInfo.RepeatGridHeader = ((bool)(resources.GetObject("tgridViewTable.PrintInfo.RepeatGridHeader")));
			this.tgridViewTable.PrintInfo.RepeatSplitHeaders = ((bool)(resources.GetObject("tgridViewTable.PrintInfo.RepeatSplitHeaders")));
			this.tgridViewTable.PrintInfo.ShowOptionsDialog = ((bool)(resources.GetObject("tgridViewTable.PrintInfo.ShowOptionsDialog")));
			this.tgridViewTable.PrintInfo.ShowProgressForm = ((bool)(resources.GetObject("tgridViewTable.PrintInfo.ShowProgressForm")));
			this.tgridViewTable.PrintInfo.ShowSelection = ((bool)(resources.GetObject("tgridViewTable.PrintInfo.ShowSelection")));
			this.tgridViewTable.PrintInfo.UseGridColors = ((bool)(resources.GetObject("tgridViewTable.PrintInfo.UseGridColors")));
			this.tgridViewTable.RecordSelectors = ((bool)(resources.GetObject("tgridViewTable.RecordSelectors")));
			this.tgridViewTable.RecordSelectorWidth = ((int)(resources.GetObject("tgridViewTable.RecordSelectorWidth")));
			this.tgridViewTable.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tgridViewTable.RightToLeft")));
			this.tgridViewTable.RowDivider.Color = ((System.Drawing.Color)(resources.GetObject("resource.Color")));
			this.tgridViewTable.RowDivider.Style = ((C1.Win.C1TrueDBGrid.LineStyleEnum)(resources.GetObject("resource.Style")));
			this.tgridViewTable.RowHeight = ((int)(resources.GetObject("tgridViewTable.RowHeight")));
			this.tgridViewTable.RowSubDividerColor = ((System.Drawing.Color)(resources.GetObject("tgridViewTable.RowSubDividerColor")));
			this.tgridViewTable.ScrollTips = ((bool)(resources.GetObject("tgridViewTable.ScrollTips")));
			this.tgridViewTable.ScrollTrack = ((bool)(resources.GetObject("tgridViewTable.ScrollTrack")));
			this.tgridViewTable.Size = ((System.Drawing.Size)(resources.GetObject("tgridViewTable.Size")));
			this.tgridViewTable.SpringMode = ((bool)(resources.GetObject("tgridViewTable.SpringMode")));
			this.tgridViewTable.TabAcrossSplits = ((bool)(resources.GetObject("tgridViewTable.TabAcrossSplits")));
			this.tgridViewTable.TabIndex = ((int)(resources.GetObject("tgridViewTable.TabIndex")));
			this.tgridViewTable.Text = resources.GetString("tgridViewTable.Text");
			this.tgridViewTable.ViewCaptionWidth = ((int)(resources.GetObject("tgridViewTable.ViewCaptionWidth")));
			this.tgridViewTable.ViewColumnWidth = ((int)(resources.GetObject("tgridViewTable.ViewColumnWidth")));
			this.tgridViewTable.Visible = ((bool)(resources.GetObject("tgridViewTable.Visible")));
			this.tgridViewTable.WrapCellPointer = ((bool)(resources.GetObject("tgridViewTable.WrapCellPointer")));
			this.tgridViewTable.AfterDelete += new System.EventHandler(this.tgridViewTable_AfterDelete);
			this.tgridViewTable.AfterUpdate += new System.EventHandler(this.tgridViewTable_AfterUpdate);
			this.tgridViewTable.RowColChange += new C1.Win.C1TrueDBGrid.RowColChangeEventHandler(this.tgridViewTable_RowColChange);
			this.tgridViewTable.Click += new System.EventHandler(this.tgridViewTable_Click);
			this.tgridViewTable.AfterColEdit += new C1.Win.C1TrueDBGrid.ColEventHandler(this.tgridViewTable_AfterColEdit);
			this.tgridViewTable.BeforeUpdate += new C1.Win.C1TrueDBGrid.CancelEventHandler(this.tgridViewTable_BeforeUpdate);
			this.tgridViewTable.ButtonClick += new C1.Win.C1TrueDBGrid.ColEventHandler(this.tgridViewTable_ButtonClick);
			this.tgridViewTable.AfterColUpdate += new C1.Win.C1TrueDBGrid.ColEventHandler(this.tgridViewTable_AfterColUpdate);
			this.tgridViewTable.BeforeInsert += new C1.Win.C1TrueDBGrid.CancelEventHandler(this.tgridViewTable_BeforeInsert);
			this.tgridViewTable.BeforeColUpdate += new C1.Win.C1TrueDBGrid.BeforeColUpdateEventHandler(this.tgridViewTable_BeforeColUpdate);
			this.tgridViewTable.BeforeDelete += new C1.Win.C1TrueDBGrid.CancelEventHandler(this.tgridViewTable_BeforeDelete);
			this.tgridViewTable.AfterInsert += new System.EventHandler(this.tgridViewTable_AfterInsert);
			this.tgridViewTable.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tgridViewTable_KeyDown_1);
			this.tgridViewTable.DoubleClick += new System.EventHandler(this.tgridViewTable_DoubleClick);
			this.tgridViewTable.FormatText += new C1.Win.C1TrueDBGrid.FormatTextEventHandler(this.tgridViewTable_FormatText);
			this.tgridViewTable.Error += new C1.Win.C1TrueDBGrid.ErrorEventHandler(this.tgridViewTable_Error);
			this.tgridViewTable.PropBag = resources.GetString("tgridViewTable.PropBag");
			// 
			// tbarViewTable
			// 
			this.tbarViewTable.AccessibleDescription = resources.GetString("tbarViewTable.AccessibleDescription");
			this.tbarViewTable.AccessibleName = resources.GetString("tbarViewTable.AccessibleName");
			this.tbarViewTable.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tbarViewTable.Anchor")));
			this.tbarViewTable.Appearance = ((System.Windows.Forms.ToolBarAppearance)(resources.GetObject("tbarViewTable.Appearance")));
			this.tbarViewTable.AutoSize = ((bool)(resources.GetObject("tbarViewTable.AutoSize")));
			this.tbarViewTable.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tbarViewTable.BackgroundImage")));
			this.tbarViewTable.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
																							 this.btnF1,
																							 this.btnF2,
																							 this.btnF3,
																							 this.btnF4,
																							 this.btnF5,
																							 this.seperator1,
																							 this.btnF6,
																							 this.btnF8,
																							 this.seperator2,
																							 this.btnF9,
																							 this.btnF10,
																							 this.btnF11});
			this.tbarViewTable.ButtonSize = ((System.Drawing.Size)(resources.GetObject("tbarViewTable.ButtonSize")));
			this.tbarViewTable.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tbarViewTable.Dock")));
			this.tbarViewTable.DropDownArrows = ((bool)(resources.GetObject("tbarViewTable.DropDownArrows")));
			this.tbarViewTable.Enabled = ((bool)(resources.GetObject("tbarViewTable.Enabled")));
			this.tbarViewTable.Font = ((System.Drawing.Font)(resources.GetObject("tbarViewTable.Font")));
			this.tbarViewTable.ImageList = this.imglstButtonImage;
			this.tbarViewTable.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tbarViewTable.ImeMode")));
			this.tbarViewTable.Location = ((System.Drawing.Point)(resources.GetObject("tbarViewTable.Location")));
			this.tbarViewTable.Name = "tbarViewTable";
			this.tbarViewTable.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tbarViewTable.RightToLeft")));
			this.tbarViewTable.ShowToolTips = ((bool)(resources.GetObject("tbarViewTable.ShowToolTips")));
			this.tbarViewTable.Size = ((System.Drawing.Size)(resources.GetObject("tbarViewTable.Size")));
			this.tbarViewTable.TabIndex = ((int)(resources.GetObject("tbarViewTable.TabIndex")));
			this.tbarViewTable.TextAlign = ((System.Windows.Forms.ToolBarTextAlign)(resources.GetObject("tbarViewTable.TextAlign")));
			this.tbarViewTable.Visible = ((bool)(resources.GetObject("tbarViewTable.Visible")));
			this.tbarViewTable.Wrappable = ((bool)(resources.GetObject("tbarViewTable.Wrappable")));
			this.tbarViewTable.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.tbarViewTable_ButtonClick);
			// 
			// btnF1
			// 
			this.btnF1.Enabled = ((bool)(resources.GetObject("btnF1.Enabled")));
			this.btnF1.ImageIndex = ((int)(resources.GetObject("btnF1.ImageIndex")));
			this.btnF1.Text = resources.GetString("btnF1.Text");
			this.btnF1.ToolTipText = resources.GetString("btnF1.ToolTipText");
			this.btnF1.Visible = ((bool)(resources.GetObject("btnF1.Visible")));
			// 
			// btnF2
			// 
			this.btnF2.Enabled = ((bool)(resources.GetObject("btnF2.Enabled")));
			this.btnF2.ImageIndex = ((int)(resources.GetObject("btnF2.ImageIndex")));
			this.btnF2.Text = resources.GetString("btnF2.Text");
			this.btnF2.ToolTipText = resources.GetString("btnF2.ToolTipText");
			this.btnF2.Visible = ((bool)(resources.GetObject("btnF2.Visible")));
			// 
			// btnF3
			// 
			this.btnF3.Enabled = ((bool)(resources.GetObject("btnF3.Enabled")));
			this.btnF3.ImageIndex = ((int)(resources.GetObject("btnF3.ImageIndex")));
			this.btnF3.Text = resources.GetString("btnF3.Text");
			this.btnF3.ToolTipText = resources.GetString("btnF3.ToolTipText");
			this.btnF3.Visible = ((bool)(resources.GetObject("btnF3.Visible")));
			// 
			// btnF4
			// 
			this.btnF4.Enabled = ((bool)(resources.GetObject("btnF4.Enabled")));
			this.btnF4.ImageIndex = ((int)(resources.GetObject("btnF4.ImageIndex")));
			this.btnF4.Text = resources.GetString("btnF4.Text");
			this.btnF4.ToolTipText = resources.GetString("btnF4.ToolTipText");
			this.btnF4.Visible = ((bool)(resources.GetObject("btnF4.Visible")));
			// 
			// btnF5
			// 
			this.btnF5.Enabled = ((bool)(resources.GetObject("btnF5.Enabled")));
			this.btnF5.ImageIndex = ((int)(resources.GetObject("btnF5.ImageIndex")));
			this.btnF5.Text = resources.GetString("btnF5.Text");
			this.btnF5.ToolTipText = resources.GetString("btnF5.ToolTipText");
			this.btnF5.Visible = ((bool)(resources.GetObject("btnF5.Visible")));
			// 
			// seperator1
			// 
			this.seperator1.Enabled = ((bool)(resources.GetObject("seperator1.Enabled")));
			this.seperator1.ImageIndex = ((int)(resources.GetObject("seperator1.ImageIndex")));
			this.seperator1.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
			this.seperator1.Text = resources.GetString("seperator1.Text");
			this.seperator1.ToolTipText = resources.GetString("seperator1.ToolTipText");
			this.seperator1.Visible = ((bool)(resources.GetObject("seperator1.Visible")));
			// 
			// btnF6
			// 
			this.btnF6.Enabled = ((bool)(resources.GetObject("btnF6.Enabled")));
			this.btnF6.ImageIndex = ((int)(resources.GetObject("btnF6.ImageIndex")));
			this.btnF6.Text = resources.GetString("btnF6.Text");
			this.btnF6.ToolTipText = resources.GetString("btnF6.ToolTipText");
			this.btnF6.Visible = ((bool)(resources.GetObject("btnF6.Visible")));
			// 
			// btnF8
			// 
			this.btnF8.Enabled = ((bool)(resources.GetObject("btnF8.Enabled")));
			this.btnF8.ImageIndex = ((int)(resources.GetObject("btnF8.ImageIndex")));
			this.btnF8.Text = resources.GetString("btnF8.Text");
			this.btnF8.ToolTipText = resources.GetString("btnF8.ToolTipText");
			this.btnF8.Visible = ((bool)(resources.GetObject("btnF8.Visible")));
			// 
			// seperator2
			// 
			this.seperator2.Enabled = ((bool)(resources.GetObject("seperator2.Enabled")));
			this.seperator2.ImageIndex = ((int)(resources.GetObject("seperator2.ImageIndex")));
			this.seperator2.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
			this.seperator2.Text = resources.GetString("seperator2.Text");
			this.seperator2.ToolTipText = resources.GetString("seperator2.ToolTipText");
			this.seperator2.Visible = ((bool)(resources.GetObject("seperator2.Visible")));
			// 
			// btnF9
			// 
			this.btnF9.Enabled = ((bool)(resources.GetObject("btnF9.Enabled")));
			this.btnF9.ImageIndex = ((int)(resources.GetObject("btnF9.ImageIndex")));
			this.btnF9.Text = resources.GetString("btnF9.Text");
			this.btnF9.ToolTipText = resources.GetString("btnF9.ToolTipText");
			this.btnF9.Visible = ((bool)(resources.GetObject("btnF9.Visible")));
			// 
			// btnF10
			// 
			this.btnF10.Enabled = ((bool)(resources.GetObject("btnF10.Enabled")));
			this.btnF10.ImageIndex = ((int)(resources.GetObject("btnF10.ImageIndex")));
			this.btnF10.Text = resources.GetString("btnF10.Text");
			this.btnF10.ToolTipText = resources.GetString("btnF10.ToolTipText");
			this.btnF10.Visible = ((bool)(resources.GetObject("btnF10.Visible")));
			// 
			// btnF11
			// 
			this.btnF11.Enabled = ((bool)(resources.GetObject("btnF11.Enabled")));
			this.btnF11.ImageIndex = ((int)(resources.GetObject("btnF11.ImageIndex")));
			this.btnF11.Text = resources.GetString("btnF11.Text");
			this.btnF11.ToolTipText = resources.GetString("btnF11.ToolTipText");
			this.btnF11.Visible = ((bool)(resources.GetObject("btnF11.Visible")));
			// 
			// imglstButtonImage
			// 
			this.imglstButtonImage.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
			this.imglstButtonImage.ImageSize = ((System.Drawing.Size)(resources.GetObject("imglstButtonImage.ImageSize")));
			this.imglstButtonImage.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imglstButtonImage.ImageStream")));
			this.imglstButtonImage.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// lblViewTable_ViewOnly
			// 
			this.lblViewTable_ViewOnly.AccessibleDescription = resources.GetString("lblViewTable_ViewOnly.AccessibleDescription");
			this.lblViewTable_ViewOnly.AccessibleName = resources.GetString("lblViewTable_ViewOnly.AccessibleName");
			this.lblViewTable_ViewOnly.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblViewTable_ViewOnly.Anchor")));
			this.lblViewTable_ViewOnly.AutoSize = ((bool)(resources.GetObject("lblViewTable_ViewOnly.AutoSize")));
			this.lblViewTable_ViewOnly.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblViewTable_ViewOnly.Dock")));
			this.lblViewTable_ViewOnly.Enabled = ((bool)(resources.GetObject("lblViewTable_ViewOnly.Enabled")));
			this.lblViewTable_ViewOnly.Font = ((System.Drawing.Font)(resources.GetObject("lblViewTable_ViewOnly.Font")));
			this.lblViewTable_ViewOnly.Image = ((System.Drawing.Image)(resources.GetObject("lblViewTable_ViewOnly.Image")));
			this.lblViewTable_ViewOnly.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblViewTable_ViewOnly.ImageAlign")));
			this.lblViewTable_ViewOnly.ImageIndex = ((int)(resources.GetObject("lblViewTable_ViewOnly.ImageIndex")));
			this.lblViewTable_ViewOnly.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblViewTable_ViewOnly.ImeMode")));
			this.lblViewTable_ViewOnly.Location = ((System.Drawing.Point)(resources.GetObject("lblViewTable_ViewOnly.Location")));
			this.lblViewTable_ViewOnly.Name = "lblViewTable_ViewOnly";
			this.lblViewTable_ViewOnly.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblViewTable_ViewOnly.RightToLeft")));
			this.lblViewTable_ViewOnly.Size = ((System.Drawing.Size)(resources.GetObject("lblViewTable_ViewOnly.Size")));
			this.lblViewTable_ViewOnly.TabIndex = ((int)(resources.GetObject("lblViewTable_ViewOnly.TabIndex")));
			this.lblViewTable_ViewOnly.Text = resources.GetString("lblViewTable_ViewOnly.Text");
			this.lblViewTable_ViewOnly.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblViewTable_ViewOnly.TextAlign")));
			this.lblViewTable_ViewOnly.Visible = ((bool)(resources.GetObject("lblViewTable_ViewOnly.Visible")));
			// 
			// lblViewTable_EditUpdate
			// 
			this.lblViewTable_EditUpdate.AccessibleDescription = resources.GetString("lblViewTable_EditUpdate.AccessibleDescription");
			this.lblViewTable_EditUpdate.AccessibleName = resources.GetString("lblViewTable_EditUpdate.AccessibleName");
			this.lblViewTable_EditUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblViewTable_EditUpdate.Anchor")));
			this.lblViewTable_EditUpdate.AutoSize = ((bool)(resources.GetObject("lblViewTable_EditUpdate.AutoSize")));
			this.lblViewTable_EditUpdate.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblViewTable_EditUpdate.Dock")));
			this.lblViewTable_EditUpdate.Enabled = ((bool)(resources.GetObject("lblViewTable_EditUpdate.Enabled")));
			this.lblViewTable_EditUpdate.Font = ((System.Drawing.Font)(resources.GetObject("lblViewTable_EditUpdate.Font")));
			this.lblViewTable_EditUpdate.Image = ((System.Drawing.Image)(resources.GetObject("lblViewTable_EditUpdate.Image")));
			this.lblViewTable_EditUpdate.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblViewTable_EditUpdate.ImageAlign")));
			this.lblViewTable_EditUpdate.ImageIndex = ((int)(resources.GetObject("lblViewTable_EditUpdate.ImageIndex")));
			this.lblViewTable_EditUpdate.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblViewTable_EditUpdate.ImeMode")));
			this.lblViewTable_EditUpdate.Location = ((System.Drawing.Point)(resources.GetObject("lblViewTable_EditUpdate.Location")));
			this.lblViewTable_EditUpdate.Name = "lblViewTable_EditUpdate";
			this.lblViewTable_EditUpdate.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblViewTable_EditUpdate.RightToLeft")));
			this.lblViewTable_EditUpdate.Size = ((System.Drawing.Size)(resources.GetObject("lblViewTable_EditUpdate.Size")));
			this.lblViewTable_EditUpdate.TabIndex = ((int)(resources.GetObject("lblViewTable_EditUpdate.TabIndex")));
			this.lblViewTable_EditUpdate.Text = resources.GetString("lblViewTable_EditUpdate.Text");
			this.lblViewTable_EditUpdate.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblViewTable_EditUpdate.TextAlign")));
			this.lblViewTable_EditUpdate.Visible = ((bool)(resources.GetObject("lblViewTable_EditUpdate.Visible")));
			// 
			// btnOK
			// 
			this.btnOK.AccessibleDescription = resources.GetString("btnOK.AccessibleDescription");
			this.btnOK.AccessibleName = resources.GetString("btnOK.AccessibleName");
			this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnOK.Anchor")));
			this.btnOK.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnOK.BackgroundImage")));
			this.btnOK.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnOK.Dock")));
			this.btnOK.Enabled = ((bool)(resources.GetObject("btnOK.Enabled")));
			this.btnOK.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnOK.FlatStyle")));
			this.btnOK.Font = ((System.Drawing.Font)(resources.GetObject("btnOK.Font")));
			this.btnOK.Image = ((System.Drawing.Image)(resources.GetObject("btnOK.Image")));
			this.btnOK.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnOK.ImageAlign")));
			this.btnOK.ImageIndex = ((int)(resources.GetObject("btnOK.ImageIndex")));
			this.btnOK.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnOK.ImeMode")));
			this.btnOK.Location = ((System.Drawing.Point)(resources.GetObject("btnOK.Location")));
			this.btnOK.Name = "btnOK";
			this.btnOK.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnOK.RightToLeft")));
			this.btnOK.Size = ((System.Drawing.Size)(resources.GetObject("btnOK.Size")));
			this.btnOK.TabIndex = ((int)(resources.GetObject("btnOK.TabIndex")));
			this.btnOK.Text = resources.GetString("btnOK.Text");
			this.btnOK.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnOK.TextAlign")));
			this.btnOK.Visible = ((bool)(resources.GetObject("btnOK.Visible")));
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// btnDelete
			// 
			this.btnDelete.AccessibleDescription = resources.GetString("btnDelete.AccessibleDescription");
			this.btnDelete.AccessibleName = resources.GetString("btnDelete.AccessibleName");
			this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnDelete.Anchor")));
			this.btnDelete.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnDelete.BackgroundImage")));
			this.btnDelete.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnDelete.Dock")));
			this.btnDelete.Enabled = ((bool)(resources.GetObject("btnDelete.Enabled")));
			this.btnDelete.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnDelete.FlatStyle")));
			this.btnDelete.Font = ((System.Drawing.Font)(resources.GetObject("btnDelete.Font")));
			this.btnDelete.Image = ((System.Drawing.Image)(resources.GetObject("btnDelete.Image")));
			this.btnDelete.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnDelete.ImageAlign")));
			this.btnDelete.ImageIndex = ((int)(resources.GetObject("btnDelete.ImageIndex")));
			this.btnDelete.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnDelete.ImeMode")));
			this.btnDelete.Location = ((System.Drawing.Point)(resources.GetObject("btnDelete.Location")));
			this.btnDelete.Name = "btnDelete";
			this.btnDelete.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnDelete.RightToLeft")));
			this.btnDelete.Size = ((System.Drawing.Size)(resources.GetObject("btnDelete.Size")));
			this.btnDelete.TabIndex = ((int)(resources.GetObject("btnDelete.TabIndex")));
			this.btnDelete.Text = resources.GetString("btnDelete.Text");
			this.btnDelete.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnDelete.TextAlign")));
			this.btnDelete.Visible = ((bool)(resources.GetObject("btnDelete.Visible")));
			this.btnDelete.Click += new System.EventHandler(this.btnDeleteInGrid_Click);
			// 
			// btnClose
			// 
			this.btnClose.AccessibleDescription = resources.GetString("btnClose.AccessibleDescription");
			this.btnClose.AccessibleName = resources.GetString("btnClose.AccessibleName");
			this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnClose.Anchor")));
			this.btnClose.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClose.BackgroundImage")));
			this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnClose.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnClose.Dock")));
			this.btnClose.Enabled = ((bool)(resources.GetObject("btnClose.Enabled")));
			this.btnClose.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnClose.FlatStyle")));
			this.btnClose.Font = ((System.Drawing.Font)(resources.GetObject("btnClose.Font")));
			this.btnClose.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.Image")));
			this.btnClose.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnClose.ImageAlign")));
			this.btnClose.ImageIndex = ((int)(resources.GetObject("btnClose.ImageIndex")));
			this.btnClose.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnClose.ImeMode")));
			this.btnClose.Location = ((System.Drawing.Point)(resources.GetObject("btnClose.Location")));
			this.btnClose.Name = "btnClose";
			this.btnClose.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnClose.RightToLeft")));
			this.btnClose.Size = ((System.Drawing.Size)(resources.GetObject("btnClose.Size")));
			this.btnClose.TabIndex = ((int)(resources.GetObject("btnClose.TabIndex")));
			this.btnClose.Text = resources.GetString("btnClose.Text");
			this.btnClose.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnClose.TextAlign")));
			this.btnClose.Visible = ((bool)(resources.GetObject("btnClose.Visible")));
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			// 
			// bntHelp
			// 
			this.bntHelp.AccessibleDescription = resources.GetString("bntHelp.AccessibleDescription");
			this.bntHelp.AccessibleName = resources.GetString("bntHelp.AccessibleName");
			this.bntHelp.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("bntHelp.Anchor")));
			this.bntHelp.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("bntHelp.BackgroundImage")));
			this.bntHelp.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("bntHelp.Dock")));
			this.bntHelp.Enabled = ((bool)(resources.GetObject("bntHelp.Enabled")));
			this.bntHelp.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("bntHelp.FlatStyle")));
			this.bntHelp.Font = ((System.Drawing.Font)(resources.GetObject("bntHelp.Font")));
			this.bntHelp.Image = ((System.Drawing.Image)(resources.GetObject("bntHelp.Image")));
			this.bntHelp.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("bntHelp.ImageAlign")));
			this.bntHelp.ImageIndex = ((int)(resources.GetObject("bntHelp.ImageIndex")));
			this.bntHelp.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("bntHelp.ImeMode")));
			this.bntHelp.Location = ((System.Drawing.Point)(resources.GetObject("bntHelp.Location")));
			this.bntHelp.Name = "bntHelp";
			this.bntHelp.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("bntHelp.RightToLeft")));
			this.bntHelp.Size = ((System.Drawing.Size)(resources.GetObject("bntHelp.Size")));
			this.bntHelp.TabIndex = ((int)(resources.GetObject("bntHelp.TabIndex")));
			this.bntHelp.Text = resources.GetString("bntHelp.Text");
			this.bntHelp.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("bntHelp.TextAlign")));
			this.bntHelp.Visible = ((bool)(resources.GetObject("bntHelp.Visible")));
			this.bntHelp.Click += new System.EventHandler(this.bntHelp_Click);
			// 
			// btnAdd
			// 
			this.btnAdd.AccessibleDescription = resources.GetString("btnAdd.AccessibleDescription");
			this.btnAdd.AccessibleName = resources.GetString("btnAdd.AccessibleName");
			this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnAdd.Anchor")));
			this.btnAdd.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnAdd.BackgroundImage")));
			this.btnAdd.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnAdd.Dock")));
			this.btnAdd.Enabled = ((bool)(resources.GetObject("btnAdd.Enabled")));
			this.btnAdd.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnAdd.FlatStyle")));
			this.btnAdd.Font = ((System.Drawing.Font)(resources.GetObject("btnAdd.Font")));
			this.btnAdd.Image = ((System.Drawing.Image)(resources.GetObject("btnAdd.Image")));
			this.btnAdd.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnAdd.ImageAlign")));
			this.btnAdd.ImageIndex = ((int)(resources.GetObject("btnAdd.ImageIndex")));
			this.btnAdd.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnAdd.ImeMode")));
			this.btnAdd.Location = ((System.Drawing.Point)(resources.GetObject("btnAdd.Location")));
			this.btnAdd.Name = "btnAdd";
			this.btnAdd.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnAdd.RightToLeft")));
			this.btnAdd.Size = ((System.Drawing.Size)(resources.GetObject("btnAdd.Size")));
			this.btnAdd.TabIndex = ((int)(resources.GetObject("btnAdd.TabIndex")));
			this.btnAdd.Text = resources.GetString("btnAdd.Text");
			this.btnAdd.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnAdd.TextAlign")));
			this.btnAdd.Visible = ((bool)(resources.GetObject("btnAdd.Visible")));
			this.btnAdd.Click += new System.EventHandler(this.bntAdd_Click);
			// 
			// btnSave
			// 
			this.btnSave.AccessibleDescription = resources.GetString("btnSave.AccessibleDescription");
			this.btnSave.AccessibleName = resources.GetString("btnSave.AccessibleName");
			this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnSave.Anchor")));
			this.btnSave.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSave.BackgroundImage")));
			this.btnSave.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnSave.Dock")));
			this.btnSave.Enabled = ((bool)(resources.GetObject("btnSave.Enabled")));
			this.btnSave.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnSave.FlatStyle")));
			this.btnSave.Font = ((System.Drawing.Font)(resources.GetObject("btnSave.Font")));
			this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
			this.btnSave.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnSave.ImageAlign")));
			this.btnSave.ImageIndex = ((int)(resources.GetObject("btnSave.ImageIndex")));
			this.btnSave.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnSave.ImeMode")));
			this.btnSave.Location = ((System.Drawing.Point)(resources.GetObject("btnSave.Location")));
			this.btnSave.Name = "btnSave";
			this.btnSave.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnSave.RightToLeft")));
			this.btnSave.Size = ((System.Drawing.Size)(resources.GetObject("btnSave.Size")));
			this.btnSave.TabIndex = ((int)(resources.GetObject("btnSave.TabIndex")));
			this.btnSave.Text = resources.GetString("btnSave.Text");
			this.btnSave.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnSave.TextAlign")));
			this.btnSave.Visible = ((bool)(resources.GetObject("btnSave.Visible")));
			this.btnSave.Click += new System.EventHandler(this.bntSave_Click);
			// 
			// dtDateTimePicker
			// 
			this.dtDateTimePicker.AcceptsEscape = ((bool)(resources.GetObject("dtDateTimePicker.AcceptsEscape")));
			this.dtDateTimePicker.AccessibleDescription = resources.GetString("dtDateTimePicker.AccessibleDescription");
			this.dtDateTimePicker.AccessibleName = resources.GetString("dtDateTimePicker.AccessibleName");
			this.dtDateTimePicker.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("dtDateTimePicker.Anchor")));
			this.dtDateTimePicker.AutoSize = ((bool)(resources.GetObject("dtDateTimePicker.AutoSize")));
			this.dtDateTimePicker.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("dtDateTimePicker.BackgroundImage")));
			this.dtDateTimePicker.BorderStyle = ((System.Windows.Forms.BorderStyle)(resources.GetObject("dtDateTimePicker.BorderStyle")));
			// 
			// dtDateTimePicker.Calendar
			// 
			this.dtDateTimePicker.Calendar.AccessibleDescription = resources.GetString("dtDateTimePicker.Calendar.AccessibleDescription");
			this.dtDateTimePicker.Calendar.AccessibleName = resources.GetString("dtDateTimePicker.Calendar.AccessibleName");
			this.dtDateTimePicker.Calendar.AnnuallyBoldedDates = ((System.DateTime[])(resources.GetObject("dtDateTimePicker.Calendar.AnnuallyBoldedDates")));
			this.dtDateTimePicker.Calendar.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("dtDateTimePicker.Calendar.BackgroundImage")));
			this.dtDateTimePicker.Calendar.BoldedDates = ((System.DateTime[])(resources.GetObject("dtDateTimePicker.Calendar.BoldedDates")));
			this.dtDateTimePicker.Calendar.CalendarDimensions = ((System.Drawing.Size)(resources.GetObject("dtDateTimePicker.Calendar.CalendarDimensions")));
			this.dtDateTimePicker.Calendar.Enabled = ((bool)(resources.GetObject("dtDateTimePicker.Calendar.Enabled")));
			this.dtDateTimePicker.Calendar.FirstDayOfWeek = ((System.Windows.Forms.Day)(resources.GetObject("dtDateTimePicker.Calendar.FirstDayOfWeek")));
			this.dtDateTimePicker.Calendar.Font = ((System.Drawing.Font)(resources.GetObject("dtDateTimePicker.Calendar.Font")));
			this.dtDateTimePicker.Calendar.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("dtDateTimePicker.Calendar.ImeMode")));
			this.dtDateTimePicker.Calendar.MonthlyBoldedDates = ((System.DateTime[])(resources.GetObject("dtDateTimePicker.Calendar.MonthlyBoldedDates")));
			this.dtDateTimePicker.Calendar.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("dtDateTimePicker.Calendar.RightToLeft")));
			this.dtDateTimePicker.Calendar.ShowClearButton = ((bool)(resources.GetObject("dtDateTimePicker.Calendar.ShowClearButton")));
			this.dtDateTimePicker.Calendar.ShowTodayButton = ((bool)(resources.GetObject("dtDateTimePicker.Calendar.ShowTodayButton")));
			this.dtDateTimePicker.Calendar.ShowWeekNumbers = ((bool)(resources.GetObject("dtDateTimePicker.Calendar.ShowWeekNumbers")));
			this.dtDateTimePicker.CaseSensitive = ((bool)(resources.GetObject("dtDateTimePicker.CaseSensitive")));
			this.dtDateTimePicker.Culture = ((int)(resources.GetObject("dtDateTimePicker.Culture")));
			this.dtDateTimePicker.CurrentTimeZone = ((bool)(resources.GetObject("dtDateTimePicker.CurrentTimeZone")));
			this.dtDateTimePicker.CustomFormat = resources.GetString("dtDateTimePicker.CustomFormat");
			this.dtDateTimePicker.DaylightTimeAdjustment = ((C1.Win.C1Input.DaylightTimeAdjustmentEnum)(resources.GetObject("dtDateTimePicker.DaylightTimeAdjustment")));
			this.dtDateTimePicker.DisplayFormat.CustomFormat = resources.GetString("dtDateTimePicker.DisplayFormat.CustomFormat");
			this.dtDateTimePicker.DisplayFormat.FormatType = ((C1.Win.C1Input.FormatTypeEnum)(resources.GetObject("dtDateTimePicker.DisplayFormat.FormatType")));
			this.dtDateTimePicker.DisplayFormat.Inherit = ((C1.Win.C1Input.FormatInfoInheritFlags)(resources.GetObject("dtDateTimePicker.DisplayFormat.Inherit")));
			this.dtDateTimePicker.DisplayFormat.NullText = resources.GetString("dtDateTimePicker.DisplayFormat.NullText");
			this.dtDateTimePicker.DisplayFormat.TrimEnd = ((bool)(resources.GetObject("dtDateTimePicker.DisplayFormat.TrimEnd")));
			this.dtDateTimePicker.DisplayFormat.TrimStart = ((bool)(resources.GetObject("dtDateTimePicker.DisplayFormat.TrimStart")));
			this.dtDateTimePicker.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("dtDateTimePicker.Dock")));
			this.dtDateTimePicker.DropDownFormAlign = ((C1.Win.C1Input.DropDownFormAlignmentEnum)(resources.GetObject("dtDateTimePicker.DropDownFormAlign")));
			this.dtDateTimePicker.EditFormat.CustomFormat = resources.GetString("dtDateTimePicker.EditFormat.CustomFormat");
			this.dtDateTimePicker.EditFormat.FormatType = ((C1.Win.C1Input.FormatTypeEnum)(resources.GetObject("dtDateTimePicker.EditFormat.FormatType")));
			this.dtDateTimePicker.EditFormat.Inherit = ((C1.Win.C1Input.FormatInfoInheritFlags)(resources.GetObject("dtDateTimePicker.EditFormat.Inherit")));
			this.dtDateTimePicker.EditFormat.NullText = resources.GetString("dtDateTimePicker.EditFormat.NullText");
			this.dtDateTimePicker.EditFormat.TrimEnd = ((bool)(resources.GetObject("dtDateTimePicker.EditFormat.TrimEnd")));
			this.dtDateTimePicker.EditFormat.TrimStart = ((bool)(resources.GetObject("dtDateTimePicker.EditFormat.TrimStart")));
			this.dtDateTimePicker.EditMask = resources.GetString("dtDateTimePicker.EditMask");
			this.dtDateTimePicker.EmptyAsNull = ((bool)(resources.GetObject("dtDateTimePicker.EmptyAsNull")));
			this.dtDateTimePicker.Enabled = ((bool)(resources.GetObject("dtDateTimePicker.Enabled")));
			this.dtDateTimePicker.ErrorInfo.BeepOnError = ((bool)(resources.GetObject("dtDateTimePicker.ErrorInfo.BeepOnError")));
			this.dtDateTimePicker.ErrorInfo.ErrorMessage = resources.GetString("dtDateTimePicker.ErrorInfo.ErrorMessage");
			this.dtDateTimePicker.ErrorInfo.ErrorMessageCaption = resources.GetString("dtDateTimePicker.ErrorInfo.ErrorMessageCaption");
			this.dtDateTimePicker.ErrorInfo.ShowErrorMessage = ((bool)(resources.GetObject("dtDateTimePicker.ErrorInfo.ShowErrorMessage")));
			this.dtDateTimePicker.ErrorInfo.ValueOnError = ((object)(resources.GetObject("dtDateTimePicker.ErrorInfo.ValueOnError")));
			this.dtDateTimePicker.Font = ((System.Drawing.Font)(resources.GetObject("dtDateTimePicker.Font")));
			this.dtDateTimePicker.FormatType = ((C1.Win.C1Input.FormatTypeEnum)(resources.GetObject("dtDateTimePicker.FormatType")));
			this.dtDateTimePicker.GapHeight = ((int)(resources.GetObject("dtDateTimePicker.GapHeight")));
			this.dtDateTimePicker.GMTOffset = ((System.TimeSpan)(resources.GetObject("dtDateTimePicker.GMTOffset")));
			this.dtDateTimePicker.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("dtDateTimePicker.ImeMode")));
			this.dtDateTimePicker.InitialSelection = ((C1.Win.C1Input.InitialSelectionEnum)(resources.GetObject("dtDateTimePicker.InitialSelection")));
			this.dtDateTimePicker.Location = ((System.Drawing.Point)(resources.GetObject("dtDateTimePicker.Location")));
			this.dtDateTimePicker.MaskInfo.AutoTabWhenFilled = ((bool)(resources.GetObject("dtDateTimePicker.MaskInfo.AutoTabWhenFilled")));
			this.dtDateTimePicker.MaskInfo.CaseSensitive = ((bool)(resources.GetObject("dtDateTimePicker.MaskInfo.CaseSensitive")));
			this.dtDateTimePicker.MaskInfo.CopyWithLiterals = ((bool)(resources.GetObject("dtDateTimePicker.MaskInfo.CopyWithLiterals")));
			this.dtDateTimePicker.MaskInfo.EditMask = resources.GetString("dtDateTimePicker.MaskInfo.EditMask");
			this.dtDateTimePicker.MaskInfo.EmptyAsNull = ((bool)(resources.GetObject("dtDateTimePicker.MaskInfo.EmptyAsNull")));
			this.dtDateTimePicker.MaskInfo.ErrorMessage = resources.GetString("dtDateTimePicker.MaskInfo.ErrorMessage");
			this.dtDateTimePicker.MaskInfo.Inherit = ((C1.Win.C1Input.MaskInfoInheritFlags)(resources.GetObject("dtDateTimePicker.MaskInfo.Inherit")));
			this.dtDateTimePicker.MaskInfo.PromptChar = ((char)(resources.GetObject("dtDateTimePicker.MaskInfo.PromptChar")));
			this.dtDateTimePicker.MaskInfo.ShowLiterals = ((C1.Win.C1Input.ShowLiteralsEnum)(resources.GetObject("dtDateTimePicker.MaskInfo.ShowLiterals")));
			this.dtDateTimePicker.MaskInfo.StoredEmptyChar = ((char)(resources.GetObject("dtDateTimePicker.MaskInfo.StoredEmptyChar")));
			this.dtDateTimePicker.MaxLength = ((int)(resources.GetObject("dtDateTimePicker.MaxLength")));
			this.dtDateTimePicker.Name = "dtDateTimePicker";
			this.dtDateTimePicker.NullText = resources.GetString("dtDateTimePicker.NullText");
			this.dtDateTimePicker.ParseInfo.CaseSensitive = ((bool)(resources.GetObject("dtDateTimePicker.ParseInfo.CaseSensitive")));
			this.dtDateTimePicker.ParseInfo.CustomFormat = resources.GetString("dtDateTimePicker.ParseInfo.CustomFormat");
			this.dtDateTimePicker.ParseInfo.DateTimeStyle = ((C1.Win.C1Input.DateTimeStyleFlags)(resources.GetObject("dtDateTimePicker.ParseInfo.DateTimeStyle")));
			this.dtDateTimePicker.ParseInfo.EmptyAsNull = ((bool)(resources.GetObject("dtDateTimePicker.ParseInfo.EmptyAsNull")));
			this.dtDateTimePicker.ParseInfo.ErrorMessage = resources.GetString("dtDateTimePicker.ParseInfo.ErrorMessage");
			this.dtDateTimePicker.ParseInfo.FormatType = ((C1.Win.C1Input.FormatTypeEnum)(resources.GetObject("dtDateTimePicker.ParseInfo.FormatType")));
			this.dtDateTimePicker.ParseInfo.Inherit = ((C1.Win.C1Input.ParseInfoInheritFlags)(resources.GetObject("dtDateTimePicker.ParseInfo.Inherit")));
			this.dtDateTimePicker.ParseInfo.NullText = resources.GetString("dtDateTimePicker.ParseInfo.NullText");
			this.dtDateTimePicker.ParseInfo.TrimEnd = ((bool)(resources.GetObject("dtDateTimePicker.ParseInfo.TrimEnd")));
			this.dtDateTimePicker.ParseInfo.TrimStart = ((bool)(resources.GetObject("dtDateTimePicker.ParseInfo.TrimStart")));
			this.dtDateTimePicker.PasswordChar = ((char)(resources.GetObject("dtDateTimePicker.PasswordChar")));
			this.dtDateTimePicker.PostValidation.CaseSensitive = ((bool)(resources.GetObject("dtDateTimePicker.PostValidation.CaseSensitive")));
			this.dtDateTimePicker.PostValidation.ErrorMessage = resources.GetString("dtDateTimePicker.PostValidation.ErrorMessage");
			this.dtDateTimePicker.PostValidation.Inherit = ((C1.Win.C1Input.PostValidationInheritFlags)(resources.GetObject("dtDateTimePicker.PostValidation.Inherit")));
			this.dtDateTimePicker.PostValidation.Validation = ((C1.Win.C1Input.PostValidationTypeEnum)(resources.GetObject("dtDateTimePicker.PostValidation.Validation")));
			this.dtDateTimePicker.PostValidation.Values = ((System.Array)(resources.GetObject("dtDateTimePicker.PostValidation.Values")));
			this.dtDateTimePicker.PostValidation.ValuesExcluded = ((System.Array)(resources.GetObject("dtDateTimePicker.PostValidation.ValuesExcluded")));
			this.dtDateTimePicker.PreValidation.CaseSensitive = ((bool)(resources.GetObject("dtDateTimePicker.PreValidation.CaseSensitive")));
			this.dtDateTimePicker.PreValidation.ErrorMessage = resources.GetString("dtDateTimePicker.PreValidation.ErrorMessage");
			this.dtDateTimePicker.PreValidation.Inherit = ((C1.Win.C1Input.PreValidationInheritFlags)(resources.GetObject("dtDateTimePicker.PreValidation.Inherit")));
			this.dtDateTimePicker.PreValidation.ItemSeparator = resources.GetString("dtDateTimePicker.PreValidation.ItemSeparator");
			this.dtDateTimePicker.PreValidation.PatternString = resources.GetString("dtDateTimePicker.PreValidation.PatternString");
			this.dtDateTimePicker.PreValidation.RegexOptions = ((C1.Win.C1Input.RegexOptionFlags)(resources.GetObject("dtDateTimePicker.PreValidation.RegexOptions")));
			this.dtDateTimePicker.PreValidation.TrimEnd = ((bool)(resources.GetObject("dtDateTimePicker.PreValidation.TrimEnd")));
			this.dtDateTimePicker.PreValidation.TrimStart = ((bool)(resources.GetObject("dtDateTimePicker.PreValidation.TrimStart")));
			this.dtDateTimePicker.PreValidation.Validation = ((C1.Win.C1Input.PreValidationTypeEnum)(resources.GetObject("dtDateTimePicker.PreValidation.Validation")));
			this.dtDateTimePicker.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("dtDateTimePicker.RightToLeft")));
			this.dtDateTimePicker.ShowFocusRectangle = ((bool)(resources.GetObject("dtDateTimePicker.ShowFocusRectangle")));
			this.dtDateTimePicker.Size = ((System.Drawing.Size)(resources.GetObject("dtDateTimePicker.Size")));
			this.dtDateTimePicker.TabIndex = ((int)(resources.GetObject("dtDateTimePicker.TabIndex")));
			this.dtDateTimePicker.Tag = ((object)(resources.GetObject("dtDateTimePicker.Tag")));
			this.dtDateTimePicker.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("dtDateTimePicker.TextAlign")));
			this.dtDateTimePicker.TrimEnd = ((bool)(resources.GetObject("dtDateTimePicker.TrimEnd")));
			this.dtDateTimePicker.TrimStart = ((bool)(resources.GetObject("dtDateTimePicker.TrimStart")));
			this.dtDateTimePicker.UserCultureOverride = ((bool)(resources.GetObject("dtDateTimePicker.UserCultureOverride")));
			this.dtDateTimePicker.Value = ((object)(resources.GetObject("dtDateTimePicker.Value")));
			this.dtDateTimePicker.VerticalAlign = ((C1.Win.C1Input.VerticalAlignEnum)(resources.GetObject("dtDateTimePicker.VerticalAlign")));
			this.dtDateTimePicker.Visible = ((bool)(resources.GetObject("dtDateTimePicker.Visible")));
			this.dtDateTimePicker.VisibleButtons = ((C1.Win.C1Input.DropDownControlButtonFlags)(resources.GetObject("dtDateTimePicker.VisibleButtons")));
			this.dtDateTimePicker.ValueChanged += new System.EventHandler(this.dtDateTimePicker_ValueChanged);
			// 
			// label2
			// 
			this.label2.AccessibleDescription = resources.GetString("label2.AccessibleDescription");
			this.label2.AccessibleName = resources.GetString("label2.AccessibleName");
			this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("label2.Anchor")));
			this.label2.AutoSize = ((bool)(resources.GetObject("label2.AutoSize")));
			this.label2.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("label2.Dock")));
			this.label2.Enabled = ((bool)(resources.GetObject("label2.Enabled")));
			this.label2.Font = ((System.Drawing.Font)(resources.GetObject("label2.Font")));
			this.label2.Image = ((System.Drawing.Image)(resources.GetObject("label2.Image")));
			this.label2.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label2.ImageAlign")));
			this.label2.ImageIndex = ((int)(resources.GetObject("label2.ImageIndex")));
			this.label2.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("label2.ImeMode")));
			this.label2.Location = ((System.Drawing.Point)(resources.GetObject("label2.Location")));
			this.label2.Name = "label2";
			this.label2.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("label2.RightToLeft")));
			this.label2.Size = ((System.Drawing.Size)(resources.GetObject("label2.Size")));
			this.label2.TabIndex = ((int)(resources.GetObject("label2.TabIndex")));
			this.label2.Text = resources.GetString("label2.Text");
			this.label2.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label2.TextAlign")));
			this.label2.Visible = ((bool)(resources.GetObject("label2.Visible")));
			// 
			// pnlToolbar
			// 
			this.pnlToolbar.AccessibleDescription = resources.GetString("pnlToolbar.AccessibleDescription");
			this.pnlToolbar.AccessibleName = resources.GetString("pnlToolbar.AccessibleName");
			this.pnlToolbar.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("pnlToolbar.Anchor")));
			this.pnlToolbar.AutoScroll = ((bool)(resources.GetObject("pnlToolbar.AutoScroll")));
			this.pnlToolbar.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("pnlToolbar.AutoScrollMargin")));
			this.pnlToolbar.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("pnlToolbar.AutoScrollMinSize")));
			this.pnlToolbar.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnlToolbar.BackgroundImage")));
			this.pnlToolbar.Controls.Add(this.tbarViewTable);
			this.pnlToolbar.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("pnlToolbar.Dock")));
			this.pnlToolbar.DockPadding.Left = 4;
			this.pnlToolbar.DockPadding.Right = 4;
			this.pnlToolbar.Enabled = ((bool)(resources.GetObject("pnlToolbar.Enabled")));
			this.pnlToolbar.Font = ((System.Drawing.Font)(resources.GetObject("pnlToolbar.Font")));
			this.pnlToolbar.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("pnlToolbar.ImeMode")));
			this.pnlToolbar.Location = ((System.Drawing.Point)(resources.GetObject("pnlToolbar.Location")));
			this.pnlToolbar.Name = "pnlToolbar";
			this.pnlToolbar.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("pnlToolbar.RightToLeft")));
			this.pnlToolbar.Size = ((System.Drawing.Size)(resources.GetObject("pnlToolbar.Size")));
			this.pnlToolbar.TabIndex = ((int)(resources.GetObject("pnlToolbar.TabIndex")));
			this.pnlToolbar.Text = resources.GetString("pnlToolbar.Text");
			this.pnlToolbar.Visible = ((bool)(resources.GetObject("pnlToolbar.Visible")));
			// 
			// chkSelectAll
			// 
			this.chkSelectAll.AccessibleDescription = resources.GetString("chkSelectAll.AccessibleDescription");
			this.chkSelectAll.AccessibleName = resources.GetString("chkSelectAll.AccessibleName");
			this.chkSelectAll.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("chkSelectAll.Anchor")));
			this.chkSelectAll.Appearance = ((System.Windows.Forms.Appearance)(resources.GetObject("chkSelectAll.Appearance")));
			this.chkSelectAll.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("chkSelectAll.BackgroundImage")));
			this.chkSelectAll.CheckAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("chkSelectAll.CheckAlign")));
			this.chkSelectAll.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("chkSelectAll.Dock")));
			this.chkSelectAll.Enabled = ((bool)(resources.GetObject("chkSelectAll.Enabled")));
			this.chkSelectAll.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("chkSelectAll.FlatStyle")));
			this.chkSelectAll.Font = ((System.Drawing.Font)(resources.GetObject("chkSelectAll.Font")));
			this.chkSelectAll.Image = ((System.Drawing.Image)(resources.GetObject("chkSelectAll.Image")));
			this.chkSelectAll.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("chkSelectAll.ImageAlign")));
			this.chkSelectAll.ImageIndex = ((int)(resources.GetObject("chkSelectAll.ImageIndex")));
			this.chkSelectAll.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("chkSelectAll.ImeMode")));
			this.chkSelectAll.Location = ((System.Drawing.Point)(resources.GetObject("chkSelectAll.Location")));
			this.chkSelectAll.Name = "chkSelectAll";
			this.chkSelectAll.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("chkSelectAll.RightToLeft")));
			this.chkSelectAll.Size = ((System.Drawing.Size)(resources.GetObject("chkSelectAll.Size")));
			this.chkSelectAll.TabIndex = ((int)(resources.GetObject("chkSelectAll.TabIndex")));
			this.chkSelectAll.Text = resources.GetString("chkSelectAll.Text");
			this.chkSelectAll.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("chkSelectAll.TextAlign")));
			this.chkSelectAll.Visible = ((bool)(resources.GetObject("chkSelectAll.Visible")));
			this.chkSelectAll.Enter += new System.EventHandler(this.chkSelectAll_Enter);
			this.chkSelectAll.Leave += new System.EventHandler(this.chkSelectAll_Leave);
			this.chkSelectAll.CheckedChanged += new System.EventHandler(this.chkSelectAll_CheckedChanged);
			// 
			// ViewTable
			// 
			this.AccessibleDescription = resources.GetString("$this.AccessibleDescription");
			this.AccessibleName = resources.GetString("$this.AccessibleName");
			this.AutoScaleBaseSize = ((System.Drawing.Size)(resources.GetObject("$this.AutoScaleBaseSize")));
			this.AutoScroll = ((bool)(resources.GetObject("$this.AutoScroll")));
			this.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("$this.AutoScrollMargin")));
			this.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("$this.AutoScrollMinSize")));
			this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
			this.CancelButton = this.btnClose;
			this.ClientSize = ((System.Drawing.Size)(resources.GetObject("$this.ClientSize")));
			this.Controls.Add(this.pnlToolbar);
			this.Controls.Add(this.tgridViewTable);
			this.Controls.Add(this.dtDateTimePicker);
			this.Controls.Add(this.lblViewTable_EditUpdate);
			this.Controls.Add(this.lblViewTable_ViewOnly);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.btnClose);
			this.Controls.Add(this.bntHelp);
			this.Controls.Add(this.btnAdd);
			this.Controls.Add(this.btnOK);
			this.Controls.Add(this.chkSelectAll);
			this.Controls.Add(this.btnSave);
			this.Controls.Add(this.btnDelete);
			this.Enabled = ((bool)(resources.GetObject("$this.Enabled")));
			this.Font = ((System.Drawing.Font)(resources.GetObject("$this.Font")));
			this.ForeColor = System.Drawing.SystemColors.ControlText;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("$this.ImeMode")));
			this.KeyPreview = true;
			this.Location = ((System.Drawing.Point)(resources.GetObject("$this.Location")));
			this.MaximumSize = ((System.Drawing.Size)(resources.GetObject("$this.MaximumSize")));
			this.MinimumSize = ((System.Drawing.Size)(resources.GetObject("$this.MinimumSize")));
			this.Name = "ViewTable";
			this.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("$this.RightToLeft")));
			this.StartPosition = ((System.Windows.Forms.FormStartPosition)(resources.GetObject("$this.StartPosition")));
			this.Text = resources.GetString("$this.Text");
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ViewTable_KeyDown);
			this.Closing += new System.ComponentModel.CancelEventHandler(this.frmTable_Closing);
			this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ViewTable_KeyPress);
			this.Load += new System.EventHandler(this.ViewTable_Load);
			((System.ComponentModel.ISupportInitialize)(this.tgridViewTable)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dtDateTimePicker)).EndInit();
			this.pnlToolbar.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		//**************************************************************************              
		///    <summary>
		///       Load data for this form
		///       - Build the SQL Select Statement to get data dynamically
		///       - Get The Data 
		///       - Set the Properties for the True DBGrid columns : Read Only Columns, Width, Caption , ect ...
		///       - Set the properties for the Data Set bind to the True DBGrid : Identity Column
		///    </summary>
		///    <Inputs>
		///       strTableID, strTableName
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
		private void ViewTable_Load(object sender, System.EventArgs e)
		{
			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.WaitCursor;

			#endregion Code Inserted Automatically

			
			const string METHOD_NAME = THIS + ".ViewTable_Load()";

			try
			{

				ViewTableBO objTableBO = new ViewTableBO();
				blnRunRowFilter = false;
				tgridViewTable.Splits[0].AllowColMove = false;

				#region Check if table was not configed

				if (strTableName == null || strTableName == String.Empty)
				{
					/// HACKED: Thachnn: make message display the tableName
					string[] arrstr = {strTableName};
					this.Close();
					PCSMessageBox.Show(ErrorCode.MESSAGE_CONFIGURED_TABLE,
						MessageBoxButtons.OK,
						MessageBoxIcon.Information,
						MessageBoxDefaultButton.Button1,
						arrstr);
					/// ENDHACKED: Thachnn
					// Code Inserted Automatically
					#region Code Inserted Automatically
					this.Cursor = Cursors.Default;
					#endregion Code Inserted Automatically

					return;
				}
				else 
				{
					//get the TableID from TableName
					int intViewTable_TableID = objTableBO.GetTableID(strTableName);
					if (intViewTable_TableID < 0)
					{
						//show message displaying that this table is not configured
						/// HACKED: Thachnn: make message display the tableName
						string[] arrstr = {strTableName};
						this.Close();
						PCSMessageBox.Show(ErrorCode.MESSAGE_CONFIGURED_TABLE,
							MessageBoxButtons.OK,
							MessageBoxIcon.Information,
							MessageBoxDefaultButton.Button1,
							arrstr);
						/// ENDHACKED: Thachnn
						// Code Inserted Automatically
						#region Code Inserted Automatically
						this.Cursor = Cursors.Default;
						#endregion Code Inserted Automatically

						return;
					}
					strTableID = intViewTable_TableID.ToString();
					// get tableVo
					voSysTable = (sys_TableVO) (objTableBO.GetObjectVO(intViewTable_TableID, string.Empty));
				}

				#endregion

				//Get the BO object for this form
				//ViewTableBO objTableBO = new ViewTableBO();

				#region Check if table view only or table input data

				if (blnViewOnly)
				{
					this.Text = voSysTable.TableName;
					blnAllowEditDateTime = false;
				}
				else
				{
					this.Text = voSysTable.TableName;
					blnAllowEditDateTime = true;
				}

				#endregion

				#region //Get the FieldList and store it into dataset variable
				dstFieldList = objTableBO.getFieldList(Convert.ToInt32(strTableID));

				if (dstFieldList.Tables[0].Rows.Count == 0)
				{
					PCSMessageBox.Show(ErrorCode.MESSAGE_CONFIGURED_TABLE, MessageBoxIcon.Error,new string[] {voSysTable.TableName});
					this.Close();
					// Code Inserted Automatically
					#region Code Inserted Automatically
					this.Cursor = Cursors.Default;
					#endregion Code Inserted Automatically

					return;
				}

				#endregion

				if (blnViewOnly)
				{
					#region //Set readonly for this True DbGrid in case we only use this form for searching values
					tgridViewTable.AllowAddNew = false;
					tgridViewTable.AllowDelete = false;
					tgridViewTable.AllowUpdate = false;
					btnOK.Visible = true;
					//btnAdd.Visible = false;
					btnSave.Visible = false;
					btnDelete.Visible = false;

					//Build the Select Statement command
					strSqlSelectCommand = objTableBO.BuildSQLSelect(dstFieldList, strTableName, false);
					strSqlSelectCommand += " " + strWhereClause;
					//objTableBO.Sql_Select_Command = strSqlSelectCommand; 

					//Build the Edit and Update command 
					strSqlSelectUpdateCommand = objTableBO.BuildSQLSelect(dstFieldList, strTableName, true);
					strSqlSelectUpdateCommand += " " + strWhereClause;

					//Get the data for this Table
					if (userDataSource == null)
					{
						//dstData = objTableBO.getDataList(strSqlSelectCommand, strTableName);
						dstData = objTableBO.getDataList(strSqlSelectUpdateCommand, strTableName);
					}
					else
					{
						dstData = userDataSource;
					}

					#endregion
				}
				else
				{
					#region Set inputable grid

					btnSave.Enabled = false;

					//Build the Select Statement command
					strSqlSelectCommand = objTableBO.BuildSQLSelect(dstFieldList, strTableName, false);
					strSqlSelectCommand += " " + strWhereClause;
					string strSortField = string.Empty;
					foreach(DataRow drRow in dstFieldList.Tables[0].Rows)
					{
						//Sort direction
						int intSortType;
						try
						{
							intSortType = int.Parse(drRow[sys_TableFieldTable.SORTTYPE_FLD].ToString());
						}
						catch 
						{
							intSortType = 0;
						}

						
						switch (intSortType)
						{
							case PCSSortType.NONE:
								continue;
							case PCSSortType.ASCENDING:
								if (strSortField != String.Empty) 
								{
									strSortField += "," + drRow[sys_TableFieldTable.FIELDNAME_FLD];
								}
								else
								{
									strSortField += drRow[sys_TableFieldTable.FIELDNAME_FLD];
								}
								continue;
							case PCSSortType.DESCENDING:
								if (strSortField != String.Empty) 
								{
									strSortField += "," + drRow[sys_TableFieldTable.FIELDNAME_FLD] + " DESC";
								}
								else
								{
									strSortField += drRow[sys_TableFieldTable.FIELDNAME_FLD] + " DESC";
								}
								continue;
						}
					}

					if(strSortField.Length > 0)
					{
						strSqlSelectCommand += " ORDER BY " + strSortField;
					}

					//Build the Edit and Update command 
					strSqlSelectUpdateCommand = objTableBO.BuildSQLSelect(dstFieldList, strTableName, true);
					
					//Get the Data for this table;
					dstData = objTableBO.getDataListForUpdate(strSqlSelectUpdateCommand + strWhereClause,strTableName);
					//dstData = objTableBO.getDataList(strSqlSelectCommand, strTableName);

					#endregion
				}

				#region //Check if the ReturnFieldName is already configured or not
				if (strReturnFieldName != null && strReturnFieldName != String.Empty)
				{
					if (!dstData.Tables[0].Columns.Contains(strReturnFieldName))
					{
						this.Close();
						PCSMessageBox.Show(ErrorCode.MESSAGE_CONFIGURED_TABLE, MessageBoxIcon.Error,new string[] {voSysTable.TableName});
						// Code Inserted Automatically
						#region Code Inserted Automatically
						this.Cursor = Cursors.Default;
						#endregion Code Inserted Automatically

						return;
					}
				}

				#endregion

				#region //Check if the strFilterFieldName1 is already configured or not
				if (strFilterFieldName1 != null && strFilterFieldName1 != String.Empty)
				{
					if (!dstData.Tables[0].Columns.Contains(strFilterFieldName1))
					{
						this.Close();
						PCSMessageBox.Show(ErrorCode.MESSAGE_CONFIGURED_TABLE, MessageBoxIcon.Error,new string[] {voSysTable.TableName});
						// Code Inserted Automatically
						#region Code Inserted Automatically
						this.Cursor = Cursors.Default;
						#endregion Code Inserted Automatically

						return;
					}
				}

				#endregion

				#region //Check if the strFilterFieldName2 is already configured or not
				if (strFilterFieldName2 != null && strFilterFieldName2 != String.Empty)
				{
					if (!dstData.Tables[0].Columns.Contains(strFilterFieldName2))
					{
						this.Close();
						PCSMessageBox.Show(ErrorCode.MESSAGE_CONFIGURED_TABLE, MessageBoxIcon.Error,new string[] {voSysTable.TableName});
						// Code Inserted Automatically
						#region Code Inserted Automatically
						this.Cursor = Cursors.Default;
						#endregion Code Inserted Automatically

						return;
					}
				}

				#endregion

				// HACK: Trada 03-01-2006

				#region //if Select Multi Rows then add 'select' column to the first position of the grid
				if (blnSelectMultiRows)
				{
					dstData = FormControlComponents.AddSelectColumnToTheFirstPositionOfGrid(dstData);
					chkSelectAll.Visible = true;
				}
				else
				{
					chkSelectAll.Visible = false;
				}

				#endregion

				// END: Trada 03-01-2006

				//Assign this table to the Grid
				tgridViewTable.DataSource = dstData.Tables[0];
				tgridViewTable.DataMember = strTableName;

				//Set the columm properties for both the Grid and DataSet
				SetColumnForTrueDBGrid();

				//Filter Data for the first time
				//This will happen when user calls this form from another form to get data
				FilterGridForTheFirstTime();

				#region //Un-lock 'select' column
				if (blnSelectMultiRows)
				{
					tgridViewTable.AllowUpdate = true;
					for (int i =0; i < tgridViewTable.Splits[0].DisplayColumns.Count; i++)
					{
						tgridViewTable.Splits[0].DisplayColumns[i].Locked = true;
					}
					tgridViewTable.Splits[0].DisplayColumns[SELECT].Locked = false;
				}

				#endregion
				//set the height of the row to be equal to the height of the date time picker
				tgridViewTable.RowHeight = dtDateTimePicker.Height;

				#region // Removed Set Height and Form Width
				//				int intFormWidth = 0;
				//				foreach(C1DisplayColumn colDisplay in tgridViewTable.Splits[0].DisplayColumns)
				//				{
				//					if(colDisplay.Visible)
				//					{
				//						intFormWidth += colDisplay.Width;
				//					}
				//				}
				//				intFormWidth += SPACE_SCROLL;
				//				if(intFormWidth < DEFAULT_WIDTH) intFormWidth = DEFAULT_WIDTH;
				//				this.Width = intFormWidth;
				/// HACKED: Thachnn: fix bug 1577
				//				if(this.Width > Screen.PrimaryScreen.WorkingArea.Width - 100)
				//				{
				//					this.Width = Screen.PrimaryScreen.WorkingArea.Width - 100;
				//				}				
				//				this.CenterToScreen();
				/// ENDHACKED:
				#endregion
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


			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.Default;

			#endregion Code Inserted Automatically

			
		}




		//**************************************************************************              
		///    <summary>
		///       Set the column properties for true dbgrid for example : Read Only, alignment, width, caption, ect
		///       Set the properties for the Identity column in DataSet
		///    </summary>
		///    <Inputs>
		///       DataSet FieldList
		///    </Inputs>
		///    <Outputs>
		///       The properties of the columns will be set
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
		private void SetColumnForTrueDBGrid()
		{
			ViewTableBO objViewTableBO = new ViewTableBO();

			#region //Set Center Heading
			for (int i = 0; i < tgridViewTable.Splits[0].DisplayColumns.Count; i++)
			{
				tgridViewTable.Splits[0].DisplayColumns[i].HeadingStyle.HorizontalAlignment = AlignHorzEnum.Center;
				// Header color
				if(!dstData.Tables[0].Columns[tgridViewTable.Splits[0].DisplayColumns[i].Name].AllowDBNull)
				{
					tgridViewTable.Splits[0].DisplayColumns[i].HeadingStyle.ForeColor = Color.Maroon;
				}

				#region Deleted

				// Auto Alignment
				//				if(dstData.Tables[0].Columns[tgridViewTable.Splits[0].DisplayColumns[i].DataColumn.DataField].DataType.Equals(typeof(int)))
				//				{
				//					tgridViewTable.Splits[0].DisplayColumns[i].Style.HorizontalAlignment = AlignHorzEnum.Far;
				//				}
				//				else if ((dstData.Tables[0].Columns[tgridViewTable.Splits[0].DisplayColumns[i].DataColumn.DataField].DataType.Equals(typeof(DateTime)))
				//						|| (dstData.Tables[0].Columns[tgridViewTable.Splits[0].DisplayColumns[i].DataColumn.DataField].DataType.Equals(typeof(Boolean))))
				//				{
				//					tgridViewTable.Splits[0].DisplayColumns[i].Style.HorizontalAlignment = AlignHorzEnum.Center;
				//				}

				#endregion
			}

			#endregion

			#region Setup default format

			int intFormWidth = 0;
			foreach (DataColumn dcolData in dstData.Tables[0].Columns)
			{
				// if data type is decimal then display only 2 digits 
				if (dcolData.DataType == typeof(decimal))
				{
					tgridViewTable.Columns[dcolData.ColumnName].NumberFormat = Constants.DECIMAL_NUMBERFORMAT;
				}
				else if (dcolData.DataType == typeof(int))
				{
					tgridViewTable.Columns[dcolData.ColumnName].NumberFormat = Constants.INTERGER_NUMBERFORMAT;
				}
				else if (dcolData.DataType == typeof(bool))
				{
					tgridViewTable.Columns[dcolData.ColumnName].ValueItems.Presentation = PresentationEnum.CheckBox;
					tgridViewTable.Columns[dcolData.ColumnName].DefaultValue = false.ToString();
					//tgridViewTable.Columns[dcolData.ColumnName].ValueItems.Translate = true;
					//tgridViewTable.Columns[dcolData.ColumnName].ValueItems.Validate = true;
					
					for (int a=0;a<dstData.Tables[0].Rows.Count;a++)
						if (dstData.Tables[0].Rows[a][dcolData.ColumnName] == DBNull.Value)
							dstData.Tables[0].Rows[a][dcolData.ColumnName] = false;
					
				}
				else if (dcolData.DataType == typeof(DateTime))
				{
					C1DateEdit dtmEditor = new C1DateEdit();
					dtmEditor.FormatType = FormatTypeEnum.CustomFormat;
					dtmEditor.VisibleButtons = DropDownControlButtonFlags.None;
					dtmEditor.CustomFormat = Constants.DATETIME_FORMAT_HOUR;
					tgridViewTable.Columns[dcolData.ColumnName].Tag = Constants.DATETIME_TYPE;
					tgridViewTable.Columns[dcolData.ColumnName].NumberFormat = Constants.DATETIME_FORMAT_HOUR;
					tgridViewTable.Columns[dcolData.ColumnName].Editor = dtmEditor;
				}
			}

			#endregion

			//Get the field length for this table from table
			DataRow drFieldLength = objViewTableBO.GetFieldLength(dstFieldList,strTableName);

			#region //loop in the Field List, for each field set its corresponding column in the grid to its properties
			foreach (DataRow drRow in dstFieldList.Tables[0].Rows)
			{
				//get the Field Name
				string strFieldName = ((string) drRow[sys_TableFieldTable.FIELDNAME_FLD]).Trim();

				//Set the True DBGrid , column caption 
				//Select language here 
				tgridViewTable.Columns[strFieldName].Caption = drRow[sys_TableFieldTable.CAPTION_FLD].ToString();

				//set the data width for this column
				tgridViewTable.Columns[strFieldName].DataWidth = int.Parse(drFieldLength[strFieldName].ToString());

				#region //Sort direction
				int intSortType;
				try
				{
					intSortType = int.Parse(drRow[sys_TableFieldTable.SORTTYPE_FLD].ToString());
				}
				catch 
				{
					intSortType = 0;
				}


				string strSortField = dstData.Tables[0].DefaultView.Sort;
				switch (intSortType)
				{
					case PCSSortType.NONE:
						
						tgridViewTable.Columns[strFieldName].SortDirection = SortDirEnum.None;
						break;
					case PCSSortType.ASCENDING:
						if (strSortField != String.Empty) 
						{
							strSortField += "," + strFieldName;
						}
						else
						{
							strSortField += strFieldName;
						}
						tgridViewTable.Columns[strFieldName].SortDirection = SortDirEnum.Ascending;
						break;
					case PCSSortType.DESCENDING:
						tgridViewTable.Columns[strFieldName].SortDirection = SortDirEnum.Descending;
						if (strSortField != String.Empty) 
						{
							strSortField += "," + strFieldName + " DESC";
						}
						else
						{
							strSortField += strFieldName + " DESC";
						}
						break;
				}
				dstData.Tables[0].DefaultView.Sort = strSortField;

				#endregion

				#region // Set Column Width
				tgridViewTable.Splits[0].DisplayColumns[strFieldName].Width = int.Parse(drRow[sys_TableFieldTable.WIDTH_FLD].ToString());

				intFormWidth += int.Parse(drRow[sys_TableFieldTable.WIDTH_FLD].ToString());

				if (int.Parse(drRow[sys_TableFieldTable.WIDTH_FLD].ToString()) <= INVISIBLE_COLUMN_WIDTH)
				{
					/// HACKED: Thachnn: Fix the bug 1590: when hide the column (by set Width = 1 in the Table Config/ Edit Detail), we still can resize column width.					
					tgridViewTable.Splits[0].DisplayColumns[strFieldName].Width = 0; //INVISIBLE_COLUMN_WIDTH;
					tgridViewTable.Splits[0].DisplayColumns[strFieldName].MinWidth = 0;		
					tgridViewTable.Splits[0].DisplayColumns[strFieldName].Visible = false;
					tgridViewTable.Splits[0].DisplayColumns[strFieldName].Locked = true;
					tgridViewTable.Splits[0].DisplayColumns[strFieldName].AllowSizing = false;
					/// ENDHACKED: Thachnn
				}

				#endregion

				//Set Invisible or not invisible column
				//tgridViewTable.Splits[0].DisplayColumns[strFieldName].Visible = !(bool) drRow[sys_TableFieldTable.INVISIBLE_FLD];

				#region //Set the alignment for a column
				int intAlign;
				try
				{
					intAlign = int.Parse(drRow[sys_TableFieldTable.ALIGN_FLD].ToString());
				}
				catch 
				{
					intAlign = 0;
				}
				switch (intAlign)
				{
					case PCSAligmentType.LEFT:
						tgridViewTable.Splits[0].DisplayColumns[strFieldName].Style.HorizontalAlignment = AlignHorzEnum.Near;
						break;
					case PCSAligmentType.CENTER:
						tgridViewTable.Splits[0].DisplayColumns[strFieldName].Style.HorizontalAlignment = AlignHorzEnum.Center;
						break;
					case PCSAligmentType.RIGHT:
						tgridViewTable.Splits[0].DisplayColumns[strFieldName].Style.HorizontalAlignment = AlignHorzEnum.Far;
						break;
				}

				#endregion

				#region //change the Character Case
				int nCase;
				try 
				{
					nCase = int.Parse(drRow[sys_TableFieldTable.CHARACTERCASE_FLD].ToString());
				}
				catch
				{
					nCase = 0;
				}
				if (nCase != 0)
				{
					tgridViewTable.Columns[strFieldName].NumberFormat = "FormatText Event";
					tgridViewTable.Columns[strFieldName].Tag = nCase;
				}

				#endregion

				#region //Set the Formats
				//if (drRow[sys_TableFieldTable.FORMATS_FLD] != null && drRow[sys_TableFieldTable.FORMATS_FLD].ToString().Trim() != String.Empty)
				if (drRow[sys_TableFieldTable.FORMATS_FLD].ToString().Trim() != String.Empty)
				{
					tgridViewTable.Columns[strFieldName].NumberFormat = drRow[sys_TableFieldTable.FORMATS_FLD].ToString();
					//tgridViewTable.Columns[strFieldName].EditMaskUpdate = true;
					//Column.EditMask = "00\.00\.0000"
					//tgridViewTable.Columns[strFieldName].NumberFormat = "dd/MM/yyyy";
				}

				#endregion

				//Set the property for each column in the Data Set
				//We only set this, when we need to edit this data
				if (!blnViewOnly)
				{
					//dstData.Tables[0].Columns[strFieldName].AllowDBNull = !(bool) drRow[sys_TableFieldTable.NOTALLOWNULL_FLD];

					//Set unique column
					//dstData.Tables[0].Columns[strFieldName].Unique = (bool) drRow[sys_TableFieldTable.UNIQUECOLUMN_FLD];

					//In order to change this into AutoIncrement 
					//This field must be Int or int32 (in the database)
					dstData.Tables[0].Columns[strFieldName].AutoIncrement = (bool) drRow[sys_TableFieldTable.IDENTITYCOLUMN_FLD];

					#region //If this field is an Identity column
					//We have to set its Seed and IncrementStep in the DataSet
					//When we insert a new row, this value will be automatically filed with next value
					if ((bool) drRow[sys_TableFieldTable.IDENTITYCOLUMN_FLD])
					{
						//First we have to get its DataView
						//DataView dvDataView = dstData.Tables[0].DefaultView;

						//Sort it to get the highest value for this field
						//dvDataView.Sort = strFieldName + " DESC";

						//Set this Identity Column to the highest value
						if (dstData.Tables[0].Rows.Count == 0)
						{
							dstData.Tables[0].Columns[strFieldName].AutoIncrementSeed = 1;
						}
						else
						{
							dstData.Tables[0].Columns[strFieldName].AutoIncrementSeed = objViewTableBO.GetMaxValue(dstData.Tables[0].TableName,strFieldName) ;//int.Parse(dvDataView[0][strFieldName].ToString()) + 1;
						}
						dstData.Tables[0].Columns[strFieldName].AutoIncrementStep = 1;

						//for the Auto Increase column, user is not allowed to input data
						tgridViewTable.Splits[0].DisplayColumns[strFieldName].Locked = true;

						//dispose this variable
						//dvDataView.Sort = "";
						//dvDataView = null;
					}

					#endregion

					#region //Set Readonly column
					if ((bool) drRow[sys_TableFieldTable.READONLY_FLD])
					{
						tgridViewTable.Splits[0].DisplayColumns[strFieldName].Locked = true;
					}

					#endregion

					//For a column that gets its values from a defined list values
					//First we define that column to be a combo box
					//We have initialize that combo box with the defined list value

					#region //Each value in this list is separated by "#" character
					if (drRow[sys_TableFieldTable.ITEMS_FLD] != null && drRow[sys_TableFieldTable.ITEMS_FLD].ToString().Trim() != String.Empty)
					{
						//Change this column to be a DropDown List box
						tgridViewTable.Splits[0].DisplayColumns[strFieldName].DropDownList = true;
						tgridViewTable.Splits[0].DisplayColumns[strFieldName].AutoDropDown = true;
						tgridViewTable.Splits[0].DisplayColumns[strFieldName].Button = true;

						tgridViewTable.Columns[strFieldName].ValueItems.Presentation = PresentationEnum.ComboBox;
						tgridViewTable.Columns[strFieldName].ValueItems.Translate = true;
						tgridViewTable.Columns[strFieldName].ValueItems.Validate = true;


						//Get a list values (separated by #)
						string strItemValues = drRow[sys_TableFieldTable.ITEMS_FLD].ToString();

						//turn it into an array
						string[] strItemValuesArray = strItemValues.Split(Constants.VIEW_TABLE_ITEM_SEPARATOR);

						//Init data for this combo box
						foreach (string strItemValue in strItemValuesArray)
						{
							ValueItem objItem = new ValueItem(strItemValue, strItemValue);
							tgridViewTable.Columns[strFieldName].ValueItems.Values.Add(objItem);
						}
					}

					#endregion

					//For a column that gets its value from another table. 
					//We will define this column to be a button

					#region //When a user clicks at this button it will display another form (another instance of this form)
					if (drRow[sys_TableFieldTable.FROMTABLE_FLD] != null && drRow[sys_TableFieldTable.FROMTABLE_FLD].ToString().Trim() != String.Empty)
					{
						//Invisible the original column 
						tgridViewTable.Splits[0].DisplayColumns[strFieldName].AllowSizing =
							tgridViewTable.Splits[0].DisplayColumns[strFieldName].Visible = false;

						//change this filter column to button enable
						string strFilterFieldName1 = strFieldName + Constants.VIEW_TABLE_FILTER_SEPARATOR + drRow[sys_TableFieldTable.FILTERFIELD1_FLD].ToString().Trim();

						//Create a string array to store temporay value for this column, so that later we can 
						//access this value to understand what we have to do with this column
						string[] strArrayValue1 = new string[6];
						strArrayValue1[0] = drRow[sys_TableFieldTable.FROMTABLE_FLD].ToString().Trim(); //From Table Name
						strArrayValue1[1] = drRow[sys_TableFieldTable.FROMFIELD_FLD].ToString().Trim(); //Return value column
						strArrayValue1[2] = drRow[sys_TableFieldTable.FILTERFIELD1_FLD].ToString().Trim(); //Display value column
						if (drRow[sys_TableFieldTable.FILTERFIELD2_FLD] != null && drRow[sys_TableFieldTable.FILTERFIELD2_FLD].ToString().Trim() != String.Empty)
						{
							strArrayValue1[3] = drRow[sys_TableFieldTable.FILTERFIELD2_FLD].ToString().Trim(); //Display value column
						}
						else
						{
							strArrayValue1[3] = String.Empty; //Display value column
						}
						strArrayValue1[4] = strFieldName; //The column name of this column
						strArrayValue1[5] = drRow[sys_TableFieldTable.FILTERFIELD1_FLD].ToString().Trim(); //the original filter column name

						//tgridViewTable.Columns[strFilterFieldName1].Caption = drRow[sys_TableFieldTable.CAPTION_FLD].ToString().Trim();

						tgridViewTable.Splits[0].DisplayColumns[strFilterFieldName1].Button = true;
						//tgridViewTable.Splits[0].DisplayColumns[strFilterFieldName1].ButtonText=true;
						//tgridViewTable.Splits[0].DisplayColumns[strFilterFieldName1].ButtonAlways =false;

						//Get the TableID, TableName, Return Field
						tgridViewTable.Splits[0].DisplayColumns[strFilterFieldName1].DataColumn.Tag = strArrayValue1;

						//if there is another filter column , we also have to set the same as the previous column is
						if (drRow[sys_TableFieldTable.FILTERFIELD2_FLD] != null && drRow[sys_TableFieldTable.FILTERFIELD2_FLD].ToString().Trim() != String.Empty)
						{
							//change this filter column to button enable
							string strFilterFieldName2 = strFieldName + Constants.VIEW_TABLE_FILTER_SEPARATOR + drRow[sys_TableFieldTable.FILTERFIELD2_FLD].ToString().Trim();

							//tgridViewTable.Columns[strFilterFieldName2].Caption = drRow[sys_TableFieldTable.CAPTION_FLD].ToString().Trim();
							tgridViewTable.Splits[0].DisplayColumns[strFilterFieldName2].Button = true;
							//tgridViewTable.Splits[0].DisplayColumns[strFilterFieldName2].ButtonText=true;
							//tgridViewTable.Splits[0].DisplayColumns[strFilterFieldName2].ButtonAlways =false;

							string[] strArrayValue2 = new string[6];
							strArrayValue2[0] = drRow[sys_TableFieldTable.FROMTABLE_FLD].ToString().Trim(); //From Table Name
							strArrayValue2[1] = drRow[sys_TableFieldTable.FROMFIELD_FLD].ToString().Trim(); //Return value column
							strArrayValue2[2] = drRow[sys_TableFieldTable.FILTERFIELD1_FLD].ToString().Trim(); //Display value column
							strArrayValue2[3] = drRow[sys_TableFieldTable.FILTERFIELD2_FLD].ToString().Trim(); //Display value column
							strArrayValue2[4] = strFieldName; //The column name of this column
							strArrayValue2[5] = drRow[sys_TableFieldTable.FILTERFIELD2_FLD].ToString().Trim(); //The original filter column name of this column

							//Get the TableID, TableName, Return Field
							tgridViewTable.Splits[0].DisplayColumns[strFilterFieldName2].DataColumn.Tag = strArrayValue2;
						}
					}

					#endregion
				} //end of If ViewOnly 

				#region //Change the caption of the Field that get data from another table
				if (drRow[sys_TableFieldTable.FROMTABLE_FLD] != DBNull.Value 
					&& drRow[sys_TableFieldTable.FROMTABLE_FLD].ToString().Trim() != String.Empty)
				{
					//Invisible the original column 
					tgridViewTable.Splits[0].DisplayColumns[strFieldName].AllowSizing =
						tgridViewTable.Splits[0].DisplayColumns[strFieldName].Visible = false;

					#region //if Display external field 1 column
					string strFilterFieldName11 = strFieldName + Constants.VIEW_TABLE_FILTER_SEPARATOR + drRow[sys_TableFieldTable.FILTERFIELD1_FLD].ToString().Trim();
					tgridViewTable.Columns[strFilterFieldName11].Caption = drRow[sys_TableFieldTable.FIELD1CAPTIONEN_FLD].ToString().Trim();
					tgridViewTable.Columns[strFilterFieldName11].NumberFormat = drRow[sys_TableFieldTable.FORMATFIELD1_FLD].ToString().Trim();
					tgridViewTable.Splits[0].DisplayColumns[strFilterFieldName11].Width = int.Parse( drRow[sys_TableFieldTable.WIDTHFIELD1_FLD].ToString());
					#region Alignment external field 1
					int intAlignField1;
					try
					{
						intAlignField1 = int.Parse(drRow[sys_TableFieldTable.ALIGNFIELD1_FLD].ToString());
					}
					catch 
					{
						intAlignField1 = 0;
					}
					switch (intAlignField1)
					{
						case PCSAligmentType.LEFT:
							tgridViewTable.Splits[0].DisplayColumns[strFilterFieldName11].Style.HorizontalAlignment = AlignHorzEnum.Near;
							break;
						case PCSAligmentType.CENTER:
							tgridViewTable.Splits[0].DisplayColumns[strFilterFieldName11].Style.HorizontalAlignment = AlignHorzEnum.Center;
							break;
						case PCSAligmentType.RIGHT:
							tgridViewTable.Splits[0].DisplayColumns[strFilterFieldName11].Style.HorizontalAlignment = AlignHorzEnum.Far;
							break;
					}
					#endregion
					if(tgridViewTable.Splits[0].DisplayColumns[strFilterFieldName11].Width <= 1)
					{
						tgridViewTable.Splits[0].DisplayColumns[strFilterFieldName11].AllowSizing =
							tgridViewTable.Splits[0].DisplayColumns[strFilterFieldName11].Visible = false;
					}
					#endregion

					#region //if Display external field 2 column
					if (drRow[sys_TableFieldTable.FILTERFIELD2_FLD] != null && drRow[sys_TableFieldTable.FILTERFIELD2_FLD].ToString().Trim() != String.Empty)
					{
						//change this filter column to button enable
						string strFilterFieldName22 = strFieldName + Constants.VIEW_TABLE_FILTER_SEPARATOR + drRow[sys_TableFieldTable.FILTERFIELD2_FLD].ToString().Trim();
						tgridViewTable.Columns[strFilterFieldName22].Caption = drRow[sys_TableFieldTable.FIELD2CAPTIONEN_FLD].ToString().Trim();
						tgridViewTable.Splits[0].DisplayColumns[strFilterFieldName22].Width = int.Parse( drRow[sys_TableFieldTable.WIDTHFIELD2_FLD].ToString());
						tgridViewTable.Columns[strFilterFieldName22].NumberFormat = drRow[sys_TableFieldTable.FORMATFIELD2_FLD].ToString().Trim();
						#region Alignment external field 2
						int intAlignField;
						try
						{
							intAlignField = int.Parse(drRow[sys_TableFieldTable.ALIGNFIELD2_FLD].ToString());
						}
						catch 
						{
							intAlignField = 0;
						}
						switch (intAlignField)
						{
							case PCSAligmentType.LEFT:
								tgridViewTable.Splits[0].DisplayColumns[strFilterFieldName22].Style.HorizontalAlignment = AlignHorzEnum.Near;
								break;
							case PCSAligmentType.CENTER:
								tgridViewTable.Splits[0].DisplayColumns[strFilterFieldName22].Style.HorizontalAlignment = AlignHorzEnum.Center;
								break;
							case PCSAligmentType.RIGHT:
								tgridViewTable.Splits[0].DisplayColumns[strFilterFieldName22].Style.HorizontalAlignment = AlignHorzEnum.Far;
								break;
						}
						#endregion
						if(tgridViewTable.Splits[0].DisplayColumns[strFilterFieldName22].Width <= 1)
						{
							tgridViewTable.Splits[0].DisplayColumns[strFilterFieldName22].AllowSizing =
								tgridViewTable.Splits[0].DisplayColumns[strFilterFieldName22].Visible = false;
						}
					}
					#endregion

					#region //if Display external field 3 column
					if (drRow[sys_TableFieldTable.FILTERFIELD3_FLD] != null && drRow[sys_TableFieldTable.FILTERFIELD3_FLD].ToString().Trim() != String.Empty)
					{
						// change this filter column to button enable
						string strFilterFieldName33 = strFieldName + Constants.VIEW_TABLE_FILTER_SEPARATOR + drRow[sys_TableFieldTable.FILTERFIELD3_FLD].ToString().Trim();
						tgridViewTable.Columns[strFilterFieldName33].Caption = drRow[sys_TableFieldTable.FIELD3CAPTIONEN_FLD].ToString().Trim();
						tgridViewTable.Splits[0].DisplayColumns[strFilterFieldName33].Width = int.Parse( drRow[sys_TableFieldTable.WIDTHFIELD3_FLD].ToString());
						tgridViewTable.Columns[strFilterFieldName33].NumberFormat = drRow[sys_TableFieldTable.FORMATFIELD3_FLD].ToString().Trim();
						#region Alignment external field 3
						int intAlignField;
						try
						{
							intAlignField = int.Parse(drRow[sys_TableFieldTable.ALIGNFIELD3_FLD].ToString());
						}
						catch 
						{
							intAlignField = 0;
						}
						switch (intAlignField)
						{
							case PCSAligmentType.LEFT:
								tgridViewTable.Splits[0].DisplayColumns[strFilterFieldName33].Style.HorizontalAlignment = AlignHorzEnum.Near;
								break;
							case PCSAligmentType.CENTER:
								tgridViewTable.Splits[0].DisplayColumns[strFilterFieldName33].Style.HorizontalAlignment = AlignHorzEnum.Center;
								break;
							case PCSAligmentType.RIGHT:
								tgridViewTable.Splits[0].DisplayColumns[strFilterFieldName33].Style.HorizontalAlignment = AlignHorzEnum.Far;
								break;
						}
						#endregion
						if(tgridViewTable.Splits[0].DisplayColumns[strFilterFieldName33].Width <= 1)
						{
							tgridViewTable.Splits[0].DisplayColumns[strFilterFieldName33].AllowSizing =
								tgridViewTable.Splits[0].DisplayColumns[strFilterFieldName33].Visible = false;
						}
					}
					#endregion
				}
				#endregion
			}

			#endregion
		}

		//**************************************************************************              
		///    <summary>
		///       Add a new row into the truedb grid
		///    </summary>
		///    <Inputs>
		///       
		///    </Inputs>
		///    <Outputs>
		///       A new row will be added to the end of the truedb grid
		///       a flag is raised to certify that user changed this record
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
		private void bntAdd_Click(object sender, System.EventArgs e)
		{
			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.WaitCursor;

			#endregion Code Inserted Automatically

			
			const string METHOD_NAME = THIS + ".bntAdd_Click()";
			// const int DEFAULT_SPLIT = 0;
			try
			{
				//HACKED : DuongNA 2005-10-13
				//check if viewonly mode
				if (blnViewOnly)
				{
					// Code Inserted Automatically
					#region Code Inserted Automatically
					this.Cursor = Cursors.Default;
					#endregion Code Inserted Automatically

					return;
				}
				//End DuongNA 2005-10-13

				//' check if have at least 1 blank line then return
				int i;
				bool blnBlank;
                for (i = 0; i < tgridViewTable.RowCount; i++)
				{
					blnBlank = true;
					foreach (C1DataColumn objCol in tgridViewTable.Columns)
					{
						if ((!dstData.Tables[0].Columns[objCol.DataField].AutoIncrement) && (!objCol.CellText(i).Trim().Equals(string.Empty)))
						{
							blnBlank = false;
							break;
						}
					}
					if (blnBlank)
					{
						tgridViewTable.Row = i;
						tgridViewTable.Col = 0;
						// Code Inserted Automatically
						#region Code Inserted Automatically
						this.Cursor = Cursors.Default;
						#endregion Code Inserted Automatically

						return;
					}

				}

				//' This "Add New" button moves the cursor to the
				//' "new (blank) row" at the end so that user can start
				//' adding data to the new record.
				//' Move to last record so that "new row" will be visible

				//' Move the cursor to the "addnew row", and set focus to the grid
				//
				//				CurrencyManager cm;

				//cm = (CurrencyManager) tgridViewTable.BindingContext[tgridViewTable.DataSource, tgridViewTable.DataMember];
				//				cm = (CurrencyManager) tgridViewTable.BindingContext[tgridViewTable.DataSource];
				//				cm.EndCurrentEdit();
				//				cm.AddNew();
				//				cm.EndCurrentEdit();
				//				tgridViewTable.Refresh();
				//				tgridViewTable.MoveLast();
				//m_blnJustAddNew = true;
				// tgridViewTable.Row = tgridViewTable.Row + 1;
				// move to first column
				int nFirstCol = 0;

				#region // HACK: DEL SonHT 2005-11-14
				//				while ((!tgridViewTable.Splits[DEFAULT_SPLIT].DisplayColumns[nFirstCol].Visible)
				//					|| (tgridViewTable.Splits[DEFAULT_SPLIT].DisplayColumns[nFirstCol].Width == 0))
				//				{
				//					nFirstCol++;
				//				}
				#endregion // END: DEL SonHT 2005-11-14

				// focus grid
				tgridViewTable.Focus();

				ChangeEditFlag(true);
				tgridViewTable.Col = nFirstCol;
                tgridViewTable.Row = tgridViewTable.RowCount;
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

			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.Default;

			#endregion Code Inserted Automatically

			
		}

		//**************************************************************************              
		///    <summary>
		///       Collect all data from Form and send back to BO layer for storing into database
		///    </summary>
		///    <Inputs>
		///       
		///    </Inputs>
		///    <Outputs>
		///       Data will be saved into database
		///       After saving, a new data will be refreshed 
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
		private void bntSave_Click(object sender, System.EventArgs e)
		{
			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.WaitCursor;

			#endregion Code Inserted Automatically

			 
			const string METHOD_NAME = THIS + ".bntSave_Click()";
	
			try
			{
				tgridViewTable.EditActive = false;
				if(CheckMandatory())
				{
					SaveDataToDatabase();
					/// HACKED: Thachnn: fix error
					btnClose.Focus();
					/// ENDHACKED:
				}
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

			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.Default;

			#endregion Code Inserted Automatically

			
		}
		private  bool SaveDataToDatabase()
		{
			const string METHOD_NAME = THIS + ".bntSave_Click()";
			DataSet dstBackup = new DataSet();
			try
			{
				tgridViewTable.UpdateData();
				//Call the BO layer object
				//DataSet dstSaveData =(DataSet) tgridViewTable.DataSource;
				ViewTableBO objTableBO = new ViewTableBO();

				dstBackup = dstData.Copy();
				objTableBO.UpdateDataSetViewTable(dstData, strSqlSelectCommand, !blnViewOnly, strSqlSelectUpdateCommand);

				//After saving into database , refresh the data for the grid
				//New Data
				//DataSet dstNewData = objTableBO.getDataList();

				tgridViewTable.Refresh();

				//blnEditUpdateData = false;
				ChangeEditFlag(false);

				//show message to inform
				PCSMessageBox.Show(ErrorCode.MESSAGE_AFTER_SAVE_DATA);
				return true;

			}
			catch (PCSException ex)
			{
				dstData = dstBackup.Copy();
				/*
				DataRow[] rowsInError;
				if(dstData.Tables[0].HasErrors)
				{
					// Get an array of all rows with errors.
					rowsInError = dstData.Tables[0].GetErrors();
					// Print the error of each column in each row.
					for(int i = 0; i < rowsInError.Length; i++)
					{
						foreach(DataColumn myCol in dstData.Tables[0].Columns)
						{
							Console.WriteLine(myCol.ColumnName + " " + 
								rowsInError[i].GetColumnError(myCol));
			
						}
						// Clear the row errors
						Console.WriteLine( "Error of row " + i.ToString() + "IS :" + rowsInError[i].RowError);
						//rowsInError[i].ClearErrors();
					}
				}
				*/

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


		/// <summary>
		/// Return true if all field mandatory were inputed
		/// else return false
		/// </summary>
		/// <returns></returns>
		private bool CheckMandatory()
		{
            for (int i = 0; i < tgridViewTable.RowCount; i++)
			{
				for(int j = 0; j < tgridViewTable.Splits[0].DisplayColumns.Count; j++)
				{
					if(dstData.Tables[0].Columns[j].AutoIncrement) continue;
					if(!dstData.Tables[0].Columns[j].AllowDBNull)
					{
						if(tgridViewTable[i,j].ToString().Trim().Length == 0)
						{
							PCSMessageBox.Show(ErrorCode.MESSAGE_MANDATORY_FIELD_REQUIRED, MessageBoxIcon.Warning, new string[1] {tgridViewTable.Splits[0].DisplayColumns[j].DataColumn.Caption});
							tgridViewTable.Focus();
							tgridViewTable.Row = i;
							tgridViewTable.Col = j;
							return false;
						}
					}
				}
			}
			return true;
		}
		private void bntHelp_Click(object sender, System.EventArgs e)
		{
			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.WaitCursor;

			#endregion Code Inserted Automatically

			
			MessageBox.Show("Help system is building,please be patient", "Help", MessageBoxButtons.OK, MessageBoxIcon.Information);

			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.Default;

			#endregion Code Inserted Automatically

			
		}



		//**************************************************************************              
		///    <summary>
		///       Close form. If this form is used to view data only, it will return the result of 
		///       the Dialog.Result to No signing that user doesn't select any row
		///       If the form is not viewing, and if the status is update or add new data, it will 
		///       display a message asking user to confirm before closing this form
		///    </summary>
		///    <Inputs>
		///       Form status (Viewing or not viewing data) and the status of the data
		///    </Inputs>
		///    <Outputs>
		///       Set the return value
		///    </Outputs>
		///    <Returns>
		///       
		///    </Returns>
		///    <Authors>
		///       THIENHD
		///    </Authors>
		///    <History>
		///       04-JAN-2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private void btnClose_Click(object sender, System.EventArgs e)
		{
			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.WaitCursor;

			#endregion Code Inserted Automatically

			
			if (blnViewOnly)
			{
				this.DialogResult = DialogResult.No;
			}
			this.Close();

			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.Default;

			#endregion Code Inserted Automatically

			
		}

		//**************************************************************************              
		///    <summary>
		///       Display a confirm message before deleting any row
		///    </summary>
		///    <Inputs>
		///       a current row
		///    </Inputs>
		///    <Outputs>
		///       a current row is deleted
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
		private void tgridViewTable_BeforeDelete(object sender, C1.Win.C1TrueDBGrid.CancelEventArgs e)
		{
			if (blnViewOnly) 
			{
				return;
			}
			if (PCSMessageBox.Show(ErrorCode.MESSAGE_DELETE_RECORD, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
			{
				e.Cancel = false;
				
			}
			else
			{
				e.Cancel = true;
			}
		}

		//**************************************************************************              
		///    <summary>
		///       This function will check to see if the user modified data, and if so, display a warning message
		///    </summary>
		///    <Inputs>
		///       status 
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
		private void frmTable_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if (blnEditUpdateData)
			{
				System.Windows.Forms.DialogResult dlgResult = PCSMessageBox.Show(ErrorCode.MESSAGE_QUESTION_STORE_INTO_DATABASE, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
				switch (dlgResult)
				{
					case DialogResult.Yes:
						if (!SaveDataToDatabase())
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
				/*
				if (PCSMessageBox.Show(ErrorCode.MESSAGE_QUESTION_STORE_INTO_DATABASE, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) == DialogResult.Yes)
				{
					if (!SaveDataToDatabase())
					{
						e.Cancel = true;
					}
				}
				*/
			}
		}

		//**************************************************************************              
		///    <summary>
		///       Set the EditUpdate flag to be true indicating that use has modified this data
		///       This flag will be used to identify if user edits data or not when closing this form
		///    </summary>
		///    <Inputs>
		///       True DbGrid
		///    </Inputs>
		///    <Outputs>
		///       The blnEditUpdateData will be set to true
		///    </Outputs>
		///    <Returns>
		///       
		///    </Returns>
		///    <Authors>
		///       THIENHD
		///    </Authors>
		///    <History>
		///       04-JAN-2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private void tgridViewTable_AfterUpdate(object sender, System.EventArgs e)
		{
			if (blnViewOnly) 
			{
				return;
			}
			//blnEditUpdateData = true;
			ChangeEditFlag(true);
			tgridViewTable.ColumnFooters = false;
		}

		//**************************************************************************              
		///    <summary>
		///       Set the EditUpdate flag to be true indicating that use has modified this data
		///       This flag will be used to identify if user edits data or not when closing this form
		///    </summary>
		///    <Inputs>
		///       True DbGrid
		///    </Inputs>
		///    <Outputs>
		///       The blnEditUpdateData will be set to true
		///    </Outputs>
		///    <Returns>
		///       
		///    </Returns>
		///    <Authors>
		///       THIENHD
		///    </Authors>
		///    <History>
		///       04-JAN-2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private void tgridViewTable_AfterInsert(object sender, System.EventArgs e)
		{
			if (blnViewOnly) 
			{
				return;
			}
			//blnEditUpdateData = true;
			ChangeEditFlag(true);
			tgridViewTable.ColumnFooters = false;
		}

		#region // HACK: DEL SonHT 2005-10-18 redundant code

		private void btnCancel_Click(object sender, EventArgs e)
		{
			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.WaitCursor;

			#endregion Code Inserted Automatically

			
			const string METHOD_NAME = THIS + ".btnCancel_Click()";
			try
			{
				if (blnEditUpdateData)
				{
					if (PCSMessageBox.Show(ErrorCode.MESSAGE_QUESTION_STORE_INTO_DATABASE, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
					{
						dstData.RejectChanges();
						tgridViewTable.RefreshRow();
						//blnEditUpdateData = false;
						ChangeEditFlag(false);
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

			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.Default;

			#endregion Code Inserted Automatically

			
		}

		//**************************************************************************              
		///    <summary>
		///       Printpreview data from the Truedbgrid
		///    </summary>
		///    <Inputs>
		///       True DBGrid
		///    </Inputs>
		///    <Outputs>
		///       a new form will be displayed for previewing and printing
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
		private void btnPrintPreview_Click(object sender, EventArgs e)
		{
			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.WaitCursor;

			#endregion Code Inserted Automatically

			
			//tgridViewTable.PrintInfo;
			const string METHOD_NAME = THIS + ".btnPrintPreview_Click()";
			try
			{
				Font fntFont;
				fntFont = new Font(tgridViewTable.PrintInfo.PageHeaderStyle.Font.Name, tgridViewTable.PrintInfo.PageHeaderStyle.Font.Size, FontStyle.Italic);
				tgridViewTable.PrintInfo.PageHeaderStyle.Font = fntFont;
				tgridViewTable.PrintInfo.PageHeader = "Composers Table";

				//column headers will be on every page
				tgridViewTable.PrintInfo.RepeatColumnHeaders = true;

				//'display page numbers (centered)
				tgridViewTable.PrintInfo.PageFooter = "Page: \\p";

				//'invoke print preview
				tgridViewTable.PrintInfo.UseGridColors = true;
				tgridViewTable.PrintInfo.PrintPreview();
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


			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.Default;

			#endregion Code Inserted Automatically

			
		}

		//**************************************************************************              
		///    <summary>
		///       Display or hide the Search Bar in the True DBGrid
		///    </summary>
		///    <Inputs>
		///       True DbGrid
		///    </Inputs>
		///    <Outputs>
		///       a FilterBar is displayed or hidden
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
		private void btnDisplayFilterBar_Click(object sender, EventArgs e)
		{
			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.WaitCursor;

			#endregion Code Inserted Automatically

			
			tgridViewTable.FilterBar = !tgridViewTable.FilterBar;

			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.Default;

			#endregion Code Inserted Automatically

			
		}


		private void cboChangeGridAppearance_SelectedIndexChanged(object sender, EventArgs e)
		{
			/*
			if (cboChangeGridAppearance.SelectedIndex < 0)
			{
				return;
			}
			switch (cboChangeGridAppearance.SelectedIndex)
			{
				case 0:
					tgridViewTable.DataView = C1.Win.C1TrueDBGrid.DataViewEnum.Normal;
					break;
				case 1:
					tgridViewTable.DataView = C1.Win.C1TrueDBGrid.DataViewEnum.Inverted;
					break;
				case 2:
					tgridViewTable.DataView = C1.Win.C1TrueDBGrid.DataViewEnum.Form;
					break;
				case 3:
					tgridViewTable.DataView = C1.Win.C1TrueDBGrid.DataViewEnum.GroupBy;
					break;
				case 4:
					tgridViewTable.DataView = C1.Win.C1TrueDBGrid.DataViewEnum.MultipleLines;
					break;
				case 5:
					tgridViewTable.DataView = C1.Win.C1TrueDBGrid.DataViewEnum.Hierarchical;
					break;
			}
			*/
			/*
			switch (cboChangeGridAppearance.SelectedText) {
				case "Normal":
					tgridViewTable.DataView = C1.Win.C1TrueDBGrid.DataViewEnum.Normal;
					break;
				case "Inverted":
					tgridViewTable.DataView = C1.Win.C1TrueDBGrid.DataViewEnum.Inverted;
					break;
				case "Form":
					tgridViewTable.DataView = C1.Win.C1TrueDBGrid.DataViewEnum.Form;
					break;
				case "GroupBy":
					tgridViewTable.DataView = C1.Win.C1TrueDBGrid.DataViewEnum.GroupBy;
					break;
				case "MultipleLines":
					tgridViewTable.DataView = C1.Win.C1TrueDBGrid.DataViewEnum.MultipleLines;
					break;
				case "Hierarchical":
					tgridViewTable.DataView = C1.Win.C1TrueDBGrid.DataViewEnum.Hierarchical;
					break;
			}
			*/
		}

		#endregion // END: DEL SonHT 2005-10-18

		//**************************************************************************              
		///    <summary>
		///       Call the appropriate function for each button on the toolbar when user clicks
		///    </summary>
		///    <Inputs>
		///       Tool Bar
		///    </Inputs>
		///    <Outputs>
		///       Appropriate function will be called
		///    </Outputs>
		///    <Returns>
		///       
		///    </Returns>
		///    <Authors>
		///       THIENHD
		///    </Authors>
		///    <History>
		///       05-Jan-2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private void tbarViewTable_ButtonClick(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e)
		{
			//			const string TOOLBAR_F1 = "btnF1";
			//			const string TOOLBAR_F2 = "btnF2";  //F0: Clear all filter
			//			const string TOOLBAR_F3 = "btnF3";  // Filter with current value
			//			const string TOOLBAR_F4 = "btnF4";  //Filter with except current value
			//			const string TOOLBAR_F5 = "btnF5";  //Return previous filter
			//			const string TOOLBAR_F6 = "btnF6";  //Row filter
			//			const string TOOLBAR_F7 = "btnF7";  // Single record
			//			const string TOOLBAR_F8 = "btnF8";  // Sum current column
			//			const string TOOLBAR_F9 = "btnF9";  //Export data to Excel
			//			const string TOOLBAR_F10 = "btnF10"; //Print data to printer
			//			const string TOOLBAR_F11 = "btnF11"; // Select data from table

			const string METHOD_NAME = THIS + ".tbarViewTable_ButtonClick()";

			/// HACKED: Thachnn: fix toolbar button click ---> wrong function
			//			try
			//			{
			// Evaluate the Button property to determine which button was clicked.
			if(e.Button.Equals(this.btnF1))
			{
				/// nothing to do
			}
			else if(e.Button.Equals(this.btnF2))
			{
				//clear all filter
				ClearAllFilter(); 
			}
			else if(e.Button.Equals(this.btnF3))
			{
				FilterWithCurrentValue(false);
			}
			else if(e.Button.Equals(this.btnF4))
			{
				// filter with except current value
				//FilterWithExceptCurrentValue();
				FilterWithCurrentValue(true);
			}
			else if(e.Button.Equals(this.btnF5))
			{
				// return to previous filter
				ReturnPreviousFilter();
			}
			else if(e.Button.Equals(this.btnF6))
			{
				RowFilter();
			}						
				/*else if(e.Button.Equals(this.btnF7))
				{
					// view single record
					this.ViewSingleRecord();
				}*/
			else if(e.Button.Equals(this.btnF8))
			{
				//Sum all value of the current column
				SumCurrentColumn();
			}
			else if(e.Button.Equals(this.btnF9))
			{
				//Export data to Excel
				ExportDataToExcel();
			}
			else if(e.Button.Equals(this.btnF10))
			{
				// Insert code to print the file.    
				PrintDataToPrinter();
			}
			else if(e.Button.Equals(this.btnF11))
			{
				// Open a new form to get data
				SelectDataFromTable();
			}
			//			}
			//			catch(Exception ex)
			//			{
			//				PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxIcon.Error);
			//			}
			/// ENDHACKED: Thachnn
		}

		//**************************************************************************              
		///    <summary>
		///       Display another form for searching value when user clicks at a button column in True DBGrid
		///    </summary>
		///    <Inputs>
		///       True DbGrid
		///    </Inputs>
		///    <Outputs>
		///       another instance of the ViewTable will be displayed
		///    </Outputs>
		///    <Returns>
		///       
		///    </Returns>
		///    <Authors>
		///       THIENHD
		///    </Authors>
		///    <History>
		///       04-Jan-2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private void tgridViewTable_ButtonClick(object sender, C1.Win.C1TrueDBGrid.ColEventArgs e)
		{
			const string METHOD_NAME = THIS + ".tgridViewTable_ButtonClick()";

			/*
			if (tgridViewTable.AddNewMode == AddNewModeEnum.AddNewCurrent) 
			{
				MessageBox.Show("This column is not yet added to the grid, we don't allow it add to the grid");
			}
			*/

			if (e.Column.DataColumn.Tag == null)
			{
				return;
			}
			int intColIndex = e.ColIndex;
			OpenFormToSelectValue(intColIndex);
			return;
		}


		private void OpenFormToSelectValue(int intColIndex)
		{
			const string METHOD_NAME = THIS + ".OpenFormToSelectValue()";

			try
			{
				//get the stored value for this button column
				//strArrayValue[0] - From TableName
				//strArrayValue[1] - Value Field
				//strArrayValue[2] - Display Field 1
				//strArrayValue[3] - Display Field 2
				//strArrayValue[4] - Ogininal Column Name
				string[] strArrayValue; // = (string[]) tgridViewTable.Columns[intColIndex].Tag;
				try
				{
					strArrayValue = (string[]) tgridViewTable.Columns[intColIndex].Tag;
				}
				catch
				{
					return;
				}
				//MessageBox.Show("TableName=" + strArrayValue[0] + ".Value Field =" + strArrayValue[1] + ".Display Field =" + strArrayValue[2] + ".Column Name =" + strArrayValue[3]);

				//get the TableID for this TableName
				//Call the ViewTable Form to get this value
				ViewTableBO objViewTableBO = new ViewTableBO();
				int intViewTable_TableID = objViewTableBO.GetTableID(strArrayValue[0]);
				if (intViewTable_TableID < 0)
				{
					//show message displaying that this table is not configured
					PCSMessageBox.Show(ErrorCode.MESSAGE_CONFIGURED_TABLE, MessageBoxIcon.Error,new string[] {voSysTable.TableName});
					return;
				}
				//if this table is configured.
				ViewTable objViewTableForm = new ViewTable(intViewTable_TableID.ToString(), strArrayValue[0]);
				objViewTableForm.ViewOnly = true;
				objViewTableForm.GetData = true;
				objViewTableForm.ReturnField = strArrayValue[1];
				objViewTableForm.FilterField1 = strArrayValue[2];
				objViewTableForm.FilterField2 = strArrayValue[3];

				//show this form and get the result
				if (objViewTableForm.ShowDialog() == DialogResult.OK)
				{
					//grant this result back to the True DBGrid
					//But first we need to check the status of add a new row 
					//into a grid
					//If the AddNewMode is AddNewCurrent means that
					//a new row is added but not yet added to the grid
					//for this type of new row we need to use DataRow and add it to the DataSet
					//and then bind it back to the grid

					//3. To the orginal column
					tgridViewTable.Columns[strArrayValue[4]].Value = objViewTableForm.ReturnField;

					//1. To the filter1 field column
					//tgridViewTable.Columns[strArrayValue[4] + Constants.VIEW_TABLE_FILTER_SEPARATOR + strArrayValue[2]].Value = objViewTableForm.FilterField1;
					tgridViewTable.Columns[strArrayValue[4] + Constants.VIEW_TABLE_FILTER_SEPARATOR + strArrayValue[2]].Value = objViewTableForm.FilterFieldValue1;

					//2. To the filter field2 column if having
					if (strArrayValue[3] != String.Empty)
					{
						//tgridViewTable.Columns[strArrayValue[4] + Constants.VIEW_TABLE_FILTER_SEPARATOR + strArrayValue[3]].Value = objViewTableForm.FilterField2;
						tgridViewTable.Columns[strArrayValue[4] + Constants.VIEW_TABLE_FILTER_SEPARATOR + strArrayValue[3]].Value = objViewTableForm.FilterFieldValue2;
					}

					/*
					if (tgridViewTable.AddNewMode == AddNewModeEnum.AddNewCurrent)
					{
						//1.We need to add a new Row
						DataRow drNewRow = dstData.Tables[0].NewRow();

						//2.Grant value to the first filter field
						drNewRow[strArrayValue[4] + Constants.VIEW_TABLE_FILTER_SEPARATOR + strArrayValue[2]] = objViewTableForm.FilterField1;

						//3. Grant value to the second filter field column 
						if (strArrayValue[3] != String.Empty)
						{
							drNewRow[strArrayValue[4] + Constants.VIEW_TABLE_FILTER_SEPARATOR + strArrayValue[3]] = objViewTableForm.FilterField2;
						}
						//4. To the orginal column
						drNewRow[strArrayValue[4]] = objViewTableForm.ReturnField;

						//5. Finally we need to add this new row into the the dataset
						dstData.Tables[0].Rows.Add(drNewRow);
					}
					else
					{
						//3. To the orginal column
						tgridViewTable[tgridViewTable.Row, strArrayValue[4]] = objViewTableForm.ReturnField;

						//1. To the filter1 field column
						tgridViewTable[tgridViewTable.Row, strArrayValue[4] + Constants.VIEW_TABLE_FILTER_SEPARATOR + strArrayValue[2]] = objViewTableForm.FilterField1;

						//2. To the filter field2 column if having
						if (strArrayValue[3] != String.Empty)
						{
							tgridViewTable[tgridViewTable.Row, strArrayValue[4] + Constants.VIEW_TABLE_FILTER_SEPARATOR + strArrayValue[3]] = objViewTableForm.FilterField2;
						}
					}
					*/
					ChangeEditFlag(true);
					tgridViewTable.ColumnFooters = false;
				}
				objViewTableForm.Close();

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
		//**************************************************************************              
		///    <summary>
		///       OK button, when user clicks this button , the current row
		///       value will return to the master form
		///    </summary>
		///    <Inputs>
		///       True DbGrid
		///    </Inputs>
		///    <Outputs>
		///       current row value
		///    </Outputs>
		///    <Returns>
		///       
		///    </Returns>
		///    <Authors>
		///       THIENHD
		///    </Authors>
		///    <History>
		///       04-Jan-2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private void btnOK_Click(object sender, System.EventArgs e)
		{
			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.WaitCursor;

			#endregion Code Inserted Automatically

			
			//Return the value to previous
			const string METHOD_NAME = THIS + ".btnOK_Click()";
			// HACK: Trada 27-12-2005
			if (blnSelectMultiRows)
			{
				dtbReturnTable = dstData.Tables[0].Clone();
				CommonBO boCommon = new CommonBO();
				string strPrimaryKeyColumnName = boCommon.GetPKColumnName(strTableName);
				try
				{
					tgridViewTable.UpdateData();
					tgridViewTable.AllowUpdate = true;

					#region  DEL Trada 04-01-2006

					//					SelectedRowCollection srcData = tgridViewTable.SelectedRows;
					//					int intCount = dstData.Tables[0].Rows.Count;
					//					for (int i = 0; i < srcData.Count; i++)
					//					{
					//						for (int k = 0; k < intCount; k++)
					//						{
					//							if (tgridViewTable[srcData[i], strPrimaryKeyColumnName].ToString() == dstData.Tables[0].Rows[k][strPrimaryKeyColumnName].ToString())
					//							{
					//								DataRow drwResult = dtbReturnTable.NewRow();
					//								for (int j = 0; j < dstData.Tables[0].Columns.Count; j++)
					//								{
					//									drwResult[j] = dstData.Tables[0].Rows[k][j];
					//								}
					//								dtbReturnTable.Rows.Add(drwResult);
					//							}
					//						}
					//					}

					#endregion 

					DataRow[] adrwResult = dstData.Tables[0].Select(SELECT + " = " + true.ToString());
					for (int i = 0; i < adrwResult.Length; i++)
					{
						dtbReturnTable.ImportRow(adrwResult[i]); 
					}
					this.DialogResult = DialogResult.OK;
					tgridViewTable.AllowUpdate = false;
					this.Close();
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
					this.DialogResult = DialogResult.No;

				}
			} 
			// END: Trada 27-12-2005

			if (blnViewOnly)
			{
				if (tgridViewTable.Row >= 0)
				{
					try
					{
						ReturnValueToMaster();
						this.DialogResult = DialogResult.OK;
						this.Close();
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
						this.DialogResult = DialogResult.No;

					}
				}
			}

			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.Default;

			#endregion Code Inserted Automatically

			
		}

		//**************************************************************************              
		///    <summary>
		///       OK button, when user double click this true dbgrid , the current row
		///       value will return to the master form
		///    </summary>
		///    <Inputs>
		///       True DbGrid
		///    </Inputs>
		///    <Outputs>
		///       current row value
		///    </Outputs>
		///    <Returns>
		///       
		///    </Returns>
		///    <Authors>
		///       THIENHD
		///    </Authors>
		///    <History>
		///       04-Jan-2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private void tgridViewTable_DoubleClick(object sender, System.EventArgs e)
		{
			//Return the value to previous
			if (blnSelectMultiRows)
				return;
			
			const string METHOD_NAME = THIS + ".tgridViewTable_DoubleClick()";
			//DataTable dtTmp = (DataTable)tgridViewTable.DataSource;
			//if (blnViewOnly)
			if (blnGetData)
			{
				if (blnEditUpdateData && !blnViewOnly) 
				{
					//Inform users that they have to save before selecting this value
					PCSMessageBox.Show(ErrorCode.MESSAGE_VIEWTABLE_SAVEBEFORE_SELECTVALUE,MessageBoxIcon.Warning);
					return;
				}

				
				int intRow = tgridViewTable.Row;
				/*
				if (tgridViewTable.AllowAddNew)
				{
					intRow = intRow - 1;
				}
				*/
				if (intRow >= 0)
				{
					try
					{
						ReturnValueToMaster();
						this.DialogResult = DialogResult.OK;
						this.Close();
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

		}

		//**************************************************************************              
		///    <summary>
		///       Set the return value to its coressponding variables
		///       There are three values "
		///       1. For the ReturnValue
		///       2. For the Filter Value 1
		///       3. For the Filter Value 2
		///    </summary>
		///    <Inputs>
		///       True DbGrid
		///    </Inputs>
		///    <Outputs>
		///       current row value
		///    </Outputs>
		///    <Returns>
		///       
		///    </Returns>
		///    <Authors>
		///       THIENHD
		///    </Authors>
		///    <History>
		///       04-Jan-2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private void ReturnValueToMaster()
		{
			//MessageBox.Show(tgridViewTable[tgridViewTable.Row,"Description"].ToString());
			//			try
			//			{
			DataTable dtTmp = (DataTable)tgridViewTable.DataSource;
			if(strReturnFieldName == null) return;
			if (dtTmp.DefaultView.Count > 0)
			{
				if (strReturnFieldName != String.Empty)
				{
					strReturnFieldValue = tgridViewTable[tgridViewTable.Row, strReturnFieldName].ToString();
				}
				if (strFilterFieldName1 != String.Empty)
				{
					strFilterFieldValue1 = tgridViewTable[tgridViewTable.Row, strFilterFieldName1].ToString();
				}
				if (strFilterFieldName2 != null && strFilterFieldName2 != String.Empty)
				{
					strFilterFieldValue2 = tgridViewTable[tgridViewTable.Row, strFilterFieldName2].ToString();
				}
				
				CurrencyManager cm;
				cm = (CurrencyManager) tgridViewTable.BindingContext[tgridViewTable.DataSource];
				drvReturnDataRowView = (DataRowView)cm.Current;
			}
			else
			{
				strReturnFieldValue = String.Empty;
				strFilterFieldValue1 = String.Empty;
				strFilterFieldValue2 = String.Empty;
				drvReturnDataRowView = null;
			}
			//			}
			//			catch (Exception ex)
			//			{
			//				throw ex;
			//			}
		}

		//**************************************************************************              
		///    <summary>
		///       Cancel the value of the edit column in case edit the Filter Column of the orginal column
		///    </summary>
		///    <Inputs>
		///       True DbGrid
		///    </Inputs>
		///    <Outputs>
		///       Null
		///    </Outputs>
		///    <Returns>
		///       
		///    </Returns>
		///    <Authors>
		///       THIENHD
		///    </Authors>
		///    <History>
		///       04-Jan-2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private void tgridViewTable_BeforeColUpdate(object sender, C1.Win.C1TrueDBGrid.BeforeColUpdateEventArgs e)
		{
			if (blnViewOnly) 
			{
				return;
			}
			const string METHOD_NAME = THIS + ".tgridViewTable_BeforeColUpdate()";
			//MessageBox.Show(e.Column.DataColumn.Text);
			//int intCurRow = tgridViewTable.Row;
			if (!blnViewOnly)
			{
				try
				{
					if (e.Column.Button && e.Column.DataColumn.Tag != null)
					{
						//get the stored value for this button column
						//strArrayValue[0] - From TableName
						//strArrayValue[1] - Value Field
						//strArrayValue[2] - Display Field 1
						//strArrayValue[3] - Display Field 2
						//strArrayValue[4] - Ogininal Column Name
						//strArrayValue[5] - Ogininal Filter Column Name
						string[] strArrayValue = (string[]) e.Column.DataColumn.Tag;

						
						if (e.OldValue == e.Column.DataColumn.Value)
						{
							return;
						}
						if (e.Column.DataColumn.Text.Trim() == String.Empty)
						{
							//if user change this text to empty
							//reset all the other colum to empty
							//							if (!dstData.Tables[0].Columns[strArrayValue[4]].AllowDBNull)
							//							{
							//								PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID);
							//								e.Cancel = true;
							//								return;
							//							}
							e.Cancel = false;
						}
						else
						{
							//incase if user enter data
							//use this data to search if this data is existed
							//otherwise, don't allow to update
							ViewTableBO objViewTableBO = new ViewTableBO();
							DataTable dtResult = objViewTableBO.SearchValueForButtonColumn(strArrayValue,e.Column.DataColumn.Value.ToString());
		
							//search this value
							if (dtResult.Rows.Count <=0)
							{
								PCSMessageBox.Show(ErrorCode.MESSAGE_USE_BUTTON_TO_SELECT_VALUE);
								e.Cancel = true;
							}
						}
						

						/*
						
						if (e.OldValue.ToString().Trim() == String.Empty)
						{
							PCSMessageBox.Show(ErrorCode.MESSAGE_USE_BUTTON_TO_SELECT_VALUE);
							e.Cancel = true;
						}
						else
						{
							if (e.Column.DataColumn.Text.Trim() == String.Empty)
							{
								if (!dstData.Tables[0].Columns[strArrayValue[4]].AllowDBNull)
								{
									PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID);
									e.Cancel = true;
									return;
								}

								tgridViewTable[tgridViewTable.Row, strArrayValue[4]] = "";
								if (e.Column.DataColumn.DataField != strArrayValue[4] + Constants.VIEW_TABLE_FILTER_SEPARATOR + strArrayValue[2])
								{
									tgridViewTable[tgridViewTable.Row, strArrayValue[4] + Constants.VIEW_TABLE_FILTER_SEPARATOR + strArrayValue[2]] = "";
								}
								if (strArrayValue[3] != String.Empty)
								{
									//if (e.Column.DataColumn.DataField != strArrayValue[4] + Constants.VIEW_TABLE_FILTER_SEPARATOR + strArrayValue[3])
									tgridViewTable[tgridViewTable.Row, strArrayValue[4] + Constants.VIEW_TABLE_FILTER_SEPARATOR + strArrayValue[3]] = "";
								}
								e.Cancel = false;
							}
							else
							{
								PCSMessageBox.Show(ErrorCode.MESSAGE_USE_BUTTON_TO_SELECT_VALUE);
								e.Cancel = true;
							}
						}
						*/
						

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
					e.Cancel = true;
				}

			}
		}

		//**************************************************************************              
		///    <summary>
		///       Update the filter column to null if the orginal column is null
		///    </summary>
		///    <Inputs>
		///       True DbGrid
		///    </Inputs>
		///    <Outputs>
		///       Null
		///    </Outputs>
		///    <Returns>
		///       
		///    </Returns>
		///    <Authors>
		///       THIENHD
		///    </Authors>
		///    <History>
		///       05-Jan-2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private void tgridViewTable_AfterColUpdate(object sender, C1.Win.C1TrueDBGrid.ColEventArgs e)
		{
			if (blnViewOnly) 
			{
				return;
			}
			//e.Column.DataColumn.Value = e.Column.DataColumn.Value.ToString().Trim();
			ChangeEditFlag(true);
			tgridViewTable.ColumnFooters = false;
			//m_blnJustAddNew = false;

			//MessageBox.Show(e.Column.DataColumn.Text);
			if (!blnViewOnly)
			{
				if (e.Column.Button && e.Column.DataColumn.Tag != null)
				{
					//get the stored value for this button column
					//strArrayValue[0] - From TableName
					//strArrayValue[1] - Value Field
					//strArrayValue[2] - Display Field 1
					//strArrayValue[3] - Display Field 2
					//strArrayValue[4] - Ogininal Column Name
					string[] strArrayValue = (string[]) e.Column.DataColumn.Tag;

					if (e.Column.DataColumn.Value.ToString().Trim() == String.Empty)
					{
						tgridViewTable[tgridViewTable.Row, strArrayValue[4]] = String.Empty;
						tgridViewTable[tgridViewTable.Row, strArrayValue[4] + Constants.VIEW_TABLE_FILTER_SEPARATOR + strArrayValue[2]] = "";
						if (strArrayValue[3] != String.Empty)
						{
							//if (e.Column.DataColumn.DataField != strArrayValue[4] + Constants.VIEW_TABLE_FILTER_SEPARATOR + strArrayValue[3])
							tgridViewTable[tgridViewTable.Row, strArrayValue[4] + Constants.VIEW_TABLE_FILTER_SEPARATOR + strArrayValue[3]] = "";
						}
					}
					else
					{
						//update the ID for this column
						ViewTableBO objViewTableBO = new ViewTableBO();
						DataTable dtResult = objViewTableBO.SearchValueForButtonColumn(strArrayValue,e.Column.DataColumn.Value.ToString());
						if (dtResult.Rows.Count > 0)
						{
							tgridViewTable.Columns[strArrayValue[4]].Value = dtResult.Rows[0][strArrayValue[1]];
							//tgridViewTable[tgridViewTable.Row, strArrayValue[4]] = dtResult.Rows[0][strArrayValue[1]];
							if (strArrayValue[4] + Constants.VIEW_TABLE_FILTER_SEPARATOR + strArrayValue[2] != e.Column.DataColumn.DataField)
							{
								tgridViewTable.Columns[strArrayValue[4] + Constants.VIEW_TABLE_FILTER_SEPARATOR + strArrayValue[2]].Value = dtResult.Rows[0][strArrayValue[2]];
								//tgridViewTable[tgridViewTable.Row, strArrayValue[4] + Constants.VIEW_TABLE_FILTER_SEPARATOR + strArrayValue[2]] = dtResult.Rows[0][strArrayValue[2]];
							}
							if (strArrayValue[3] != String.Empty && (strArrayValue[4] + Constants.VIEW_TABLE_FILTER_SEPARATOR + strArrayValue[3] != e.Column.DataColumn.DataField))
							{
								//if (e.Column.DataColumn.DataField != strArrayValue[4] + Constants.VIEW_TABLE_FILTER_SEPARATOR + strArrayValue[3])
								tgridViewTable.Columns[strArrayValue[4] + Constants.VIEW_TABLE_FILTER_SEPARATOR + strArrayValue[3]].Value = dtResult.Rows[0][strArrayValue[3]];
								//tgridViewTable[tgridViewTable.Row, strArrayValue[4] + Constants.VIEW_TABLE_FILTER_SEPARATOR + strArrayValue[3]] = dtResult.Rows[0][strArrayValue[3]];
							}
						}
					}
				}
			}
		}

		/// <summary>
		/// Change blnEditUpdateData and btnSave.Enabled
		/// </summary>
		/// <param name="blnStatus"></param>
		private void ChangeEditFlag(bool blnStatus)
		{
			blnEditUpdateData = blnStatus;
			btnSave.Enabled = blnStatus;

		}

		//**************************************************************************              
		///    <summary>
		///       Delete a row from True DbGrid
		///    </summary>
		///    <Inputs>
		///       a current row
		///    </Inputs>
		///    <Outputs>
		///       a current row is deleted
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
		private void btnDeleteInGrid_Click(object sender, System.EventArgs e)
		{
			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.WaitCursor;

			#endregion Code Inserted Automatically

			
			const string METHOD_NAME = THIS + ".btnDeleteInGrid_Click()";
			const int MIN_ROW_COUNT = 1;
            if (tgridViewTable.RowCount == 0)
			{
				//duongna : at least 1 row for new data
				// Code Inserted Automatically
				#region Code Inserted Automatically
				this.Cursor = Cursors.Default;
				#endregion Code Inserted Automatically

				return;
			}
			if (tgridViewTable.Row == tgridViewTable.Splits[0].Rows.Count - 1)
			{
				//duongna : in case user select last row (row for add new)
				// Code Inserted Automatically
				#region Code Inserted Automatically
				this.Cursor = Cursors.Default;
				#endregion Code Inserted Automatically

				return;
			}
			DialogResult result;
			result = PCSMessageBox.Show(ErrorCode.MESSAGE_DELETE_RECORD, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
			if (result == DialogResult.Yes)
			{
				try
				{
					tgridViewTable.Delete();
					ChangeEditFlag(true);
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

			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.Default;

			#endregion Code Inserted Automatically

			
		}

		//		private void tgridViewTable_FilterChange(object sender, System.EventArgs e)
		//		{
		//			try 
		//			{
		//				//	build our filter expression
		//				StringBuilder sbFilterString = new StringBuilder();		
		//
		//				foreach(C1DataColumn dc in tgridViewTable.Columns)
		//				{
		//					if (dc.FilterText.Length > 0)
		//					{
		//						if (sbFilterString.Length > 0 )
		//						{
		//							sbFilterString.Append(" AND ");
		//						}
		//						sbFilterString.Append(dc.DataField + " like " + "'" + dc.FilterText + "*'");
		//					}
		//				}
		//				// filter the data
		//				strFilterString = sbFilterString.ToString();
		//				//dtable.DefaultView.RowFilter = sb.ToString();
		//			}
		//			catch 	(Exception ex)
		//			{
		//				PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxIcon.Error);
		//
		//			}
		//		}

		//**************************************************************************              
		///    <summary>
		///       Clear all filter on this form
		///    </summary>
		///    <Inputs>
		///       True DBGrid
		///    </Inputs>
		///    <Outputs>
		///       All rows are displayed 
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
		private void ClearAllFilter()
		{
			const string METHOD_NAME = THIS + ".ClearAllFilter()";
			try 
			{
				this.dstData.Tables[0].DefaultView.RowFilter = String.Empty;
				strFilterString = String.Empty;
				strPreviousFilterString = String.Empty;

				tgridViewTable.FilterBar = false; 
				tgridViewTable.FilterActive = false;
				// btnClose.Focus();
				tgridViewTable.Focus();
				// tgridViewTable.Row = tgridViewTable.Col = 0;

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
		//**************************************************************************              
		///    <summary>
		///       Filter with the current value at the cell that user focuses on 
		///    </summary>
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

		private void FilterWithCurrentValue(bool blnExceptCurrentValue)
		{
			const string METHOD_NAME = THIS + ".FilterWithCurrentValue()";
			const string JOIN_FILTER_OPERATION = " and " ;
			const string FILTER_OPERATION_LIKE  = " like ";
			const string FILTER_OPERATION_EQUAL  = " = ";
			const string FILTER_OPERATION_EXCEPT  = " <> ";
			const string FILTER_WITH_NULL  = " is null ";
			const string FILTER_WITHOUT_NULL  = " is not null ";
            if (tgridViewTable.RowCount == 0) 
			{
				//if there is no row, this method will return without doing anything
				return;
			}

			//In order to use custom filter
			//we have to bind this True DbGrid to a Table not a DataSet
			try 
			{
				//get the column name
				string strColumnName = tgridViewTable.Splits[0].DisplayColumns[tgridViewTable.Col].DataColumn.DataField;
				//get the cel value
				string strCellValue;
				if (tgridViewTable[tgridViewTable.Row,tgridViewTable.Col] != DBNull.Value)
				{
					strCellValue = tgridViewTable[tgridViewTable.Row,tgridViewTable.Col].ToString().Trim();
				}
				else
				{
					strCellValue = null;
				}

				//this.tgridViewTable.Columns[strColumnName].FilterText = strCellValue;
				//this.tgridViewTable.Columns[strColumnName].FilterOperator 
				//return;


				//get the filter string
				
				string strFilterStringColumn = String.Empty;
				if (dstData.Tables[0].Columns[strColumnName].DataType == System.Type.GetType("System.String")
					|| dstData.Tables[0].Columns[strColumnName].DataType == System.Type.GetType("System.Char") )
				{
					if (!blnExceptCurrentValue) 
					{
						if (strCellValue != null)
						{
							strFilterStringColumn = strColumnName + FILTER_OPERATION_LIKE +  " '" +  strCellValue + "%'";
						}
						else
						{
							strFilterStringColumn = strColumnName + Constants.WHITE_SPACE + FILTER_WITH_NULL ;
						}
					}
					else 
					{
						if (strCellValue != null) 
						{
							strFilterStringColumn = strColumnName + " not " + FILTER_OPERATION_LIKE + " '" +  strCellValue + "%'";
						}
						else
						{
							strFilterStringColumn = strColumnName + Constants.WHITE_SPACE + FILTER_WITHOUT_NULL;
						}
					}
				}
				else 
				{
					if (!blnExceptCurrentValue)
					{
						if (strCellValue != null)  
						{
							strFilterStringColumn = strColumnName + FILTER_OPERATION_EQUAL + " '" +  strCellValue + "'";
						}
						else
						{
							strFilterStringColumn = strColumnName + Constants.WHITE_SPACE + FILTER_WITH_NULL ;
						}

					}
					else 
					{
						if (strCellValue != null)  
						{
							strFilterStringColumn = strColumnName + FILTER_OPERATION_EXCEPT + " '" +  strCellValue + "'";
						}
						else
						{
							strFilterStringColumn = strColumnName + Constants.WHITE_SPACE + FILTER_WITHOUT_NULL;
						}
					}
				}

				strPreviousFilterString = strFilterString; 
				if (strFilterString != String.Empty) 
				{
					strFilterString += JOIN_FILTER_OPERATION + strFilterStringColumn;
				}
				else 
				{
					strFilterString = strFilterStringColumn;
				}

				// HACK: SonHT 2005-10-18
				try
				{
					this.dstData.Tables[0].DefaultView.RowFilter = strFilterString;
				}
				catch
				{
					this.dstData.Tables[0].DefaultView.RowFilter = string.Empty;
				}
				// END: SonHT 2005-10-18
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
		
		
		
		private void ReturnPreviousFilter() 
		{
			const string METHOD_NAME = THIS + ".ReturnPreviousFilter()";
			try 
			{
				// HACK: SonHT 2005-10-18
				try
				{
					this.dstData.Tables[0].DefaultView.RowFilter = strPreviousFilterString;
				}
				catch
				{
					this.dstData.Tables[0].DefaultView.RowFilter = string.Empty;
				}
				// END: SonHT 2005-10-18
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
		//**************************************************************************              
		///    <summary>
		///       Filter data with current row value
		///    </summary>
		///    <Inputs>
		///    </Inputs>
		///    <Outputs>
		///       Filtered rows are displayed
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

		private void RowFilter() 
		{
			const string METHOD_NAME = THIS + ".RowFilter()";
			//Size sizeDropDown = new Size(200,200);
			tgridViewTable.FilterBar = !tgridViewTable.FilterBar;
			if (blnRunRowFilter) 
			{
				tgridViewTable.FilterActive = true;
				return;
			}
			blnRunRowFilter = true;
			
			try 
			{
				if (tgridViewTable.FilterBar)
				{
					DataTable dtData = dstData.Tables[0].Copy();

					foreach (DataColumn dtColumn in dtData.Columns)
					{
						string strFieldName = dtColumn.ColumnName;
						if (tgridViewTable.Columns[strFieldName].ValueItems.Presentation == PresentationEnum.CheckBox)
							continue;
						if (!tgridViewTable.Splits[0].DisplayColumns[strFieldName].Visible)
							continue;
						AddValueIntoComboBoxInTrueDBGrid(tgridViewTable.Columns[strFieldName],dtData,strFieldName);
						tgridViewTable.Splits[0].DisplayColumns[strFieldName].FilterButton = true;
						tgridViewTable.Splits[0].DisplayColumns[strFieldName].Button = false;
					}
					tgridViewTable.FilterActive = true;
					return;
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

		private void AddValueIntoComboBoxInTrueDBGrid(C1DataColumn c1dcDataColumn, DataTable dtData, string strFieldName) 
		{
			const string METHOD_NAME = THIS + ".AddValueIntoComboBoxInTrueDBGrid()";
			try 
			{
				c1dcDataColumn.ValueItems.Presentation = PresentationEnum.ComboBox;


				ValueItem vi = new ValueItem();

				int intFirstRow = c1dcDataColumn.ValueItems.Values.Add(vi);
				c1dcDataColumn.ValueItems.Values[intFirstRow].DisplayValue = "ALL";
				c1dcDataColumn.ValueItems.Values[intFirstRow].Value = "";
			
				ArrayList arValue = new ArrayList();

                int intTotalRows = tgridViewTable.RowCount;

                for (int i = 0; i < intTotalRows; i++)
                {
                    string strValue = String.Empty;

                    if (tgridViewTable.Columns[strFieldName].CellValue(i) != DBNull.Value && tgridViewTable.Columns[strFieldName].CellValue(i).ToString() != String.Empty)
                    {
                        strValue = tgridViewTable.Columns[strFieldName].CellText(i);
                    }

                    if (strValue == String.Empty)
                        continue;

                    if (arValue.IndexOf(strValue) < 0)
                    {
                        arValue.Add(strValue);
                    }
                }

				arValue.Sort();
				System.Collections.IEnumerator myEnumerator = arValue.GetEnumerator();
				while ( myEnumerator.MoveNext() )
				{
					vi = new ValueItem(myEnumerator.Current.ToString(), myEnumerator.Current);
					c1dcDataColumn.ValueItems.Values.Add(vi);
				}


				
				//set format for this column

				/*
				if (dcolData.DataType == typeof(decimal))
				{
					c1dcDataColumn.NumberFormat = Constants.CELL_NUMBER_FORMAT;
				}
				else if(dcolData.DataType == typeof(DateTime))
				{
					c1dcDataColumn.NumberFormat = Constants.DATETIME_FORMAT;
				}
				*/
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

		#region // HACK: DEL SonHT 2005-10-18 redundant code
		//		private void DisplayDropDownGridColumns(C1.Win.C1TrueDBGrid.C1TrueDBDropdown c1Grid, string[] pDisplayColumns, string[] pDisplayCaptions)
		//		{
		//			for (int i = 0; i < c1Grid.DisplayColumns.Count; i++)
		//			{
		//				c1Grid.DisplayColumns[i].Visible = false;
		//			}
		//			for (int i = 0; i < pDisplayColumns.Length; i++)
		//			{
		//				c1Grid.DisplayColumns[pDisplayColumns[i]].Visible = true;
		//				c1Grid.DisplayColumns[pDisplayColumns[i]].AutoSize();
		//				c1Grid.Columns[pDisplayColumns[i]].Caption = pDisplayCaptions[i];
		//			}
		//		}
		#endregion // END: DEL SonHT 2005-10-18

		//**************************************************************************              
		///    <summary>
		///       Sum the current column 
		///       For the number column it will display its total value
		///       For the un numeric column it will display zero
		///    </summary>
		///    <Inputs>
		///       a current column
		///    </Inputs>
		///    <Outputs>
		///       a message will display its total value 
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

		private void SumCurrentColumn()
		{
			if (tgridViewTable.ColumnFooters )
			{
				tgridViewTable.ColumnFooters = false;
				return;
			}
			const string METHOD_NAME = THIS + ".SumCurrentColumn()";
            if (tgridViewTable.RowCount == 0) 
			{
				return;
			}

			//In order to use custom filter
			//we have to bind this True DbGrid to a Table not a DataSet
			try 
			{
				//get the column name
				string strColumnName = tgridViewTable.Splits[0].DisplayColumns[tgridViewTable.Col].DataColumn.DataField;

				//check the data type for this column
				//dstData.Tables[0].Columns[strColumnName].DataType == 

                int intGridRows = this.tgridViewTable.RowCount;
			
				// now compute the number of unique values for the country and city columns
				double dblTotalValue = 0 ;
				for(int i = 0; i < intGridRows; i++)
				{
					try 
					{
						dblTotalValue += double.Parse(this.tgridViewTable[i,strColumnName].ToString());
					}
					catch
					{
						dblTotalValue += 0;
					}
				}
				tgridViewTable.ColumnFooters = true;
				this.tgridViewTable.Columns[strColumnName].FooterText = dblTotalValue.ToString();
				//viewing this value
				//PCSMessageBox.Show(ErrorCode.MESSAGE_VIEWTABLE_SUMCOLUMN 
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


		//**************************************************************************              
		///    <summary>
		///       Print data to printer
		///    </summary>
		///    <Inputs>
		///       True DBGrid
		///    </Inputs>
		///    <Outputs>
		///       a report is displayed before printing to printer
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

		private void PrintDataToPrinter()
		{
			const string METHOD_NAME = THIS + ".PrintDataToPrinter()";
			try
			{ //tgridViewTable.PrintInfo;
				Font fntFont;
				fntFont = new Font(tgridViewTable.PrintInfo.PageHeaderStyle.Font.Name, tgridViewTable.PrintInfo.PageHeaderStyle.Font.Size, FontStyle.Italic);
				tgridViewTable.PrintInfo.PageHeaderStyle.Font = fntFont;
				tgridViewTable.PrintInfo.PageHeader = "Composers Table";

				//column headers will be on every page
				tgridViewTable.PrintInfo.RepeatColumnHeaders = true;

				//'display page numbers (centered)
				tgridViewTable.PrintInfo.PageFooter = "Page: \\p";

				//'invoke print preview
				tgridViewTable.PrintInfo.UseGridColors = true;
				//tgridViewTable.PrintInfo.PrintPreview();
				try
				{
					tgridViewTable.PrintInfo.Print();
				}
				catch
				{
					// Not allow show error message if user cancel print
				}

				// Insert code to print the file.    
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

		//**************************************************************************              
		///    <summary>
		///       Export data to Excel
		///    </summary>
		///    <Inputs>
		///       True DBGrid
		///    </Inputs>
		///    <Outputs>
		///       a FileSaveDialog is displayed for user to select the location to save file
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

		private void ExportDataToExcel()
		{
			const string METHOD_NAME = THIS + ".ExportDataToExcel()";
			try 
			{
				SaveFileDialog saveFile = new SaveFileDialog();

				saveFile.DefaultExt = "*.xls";
				saveFile.Filter = "xls File|*.xls| All File|*.*";
				
				if(saveFile.ShowDialog() == System.Windows.Forms.DialogResult.OK &&	saveFile.FileName.Length > 0) 
				{
					try
					{	//tgridViewTable.ExportToExcel(saveFile.FileName);
						if(System.IO.File.Exists(saveFile.FileName))
							File.Delete(saveFile.FileName);
						tgridViewTable.ExportToDelimitedFile(saveFile.FileName,RowSelectorEnum.AllRows,"\t",""," ",true,"Unicode");
						PCSMessageBox.Show(ErrorCode.MESSAGE_TASK_COMPLETED,MessageBoxButtons.OK,MessageBoxIcon.Information,MessageBoxDefaultButton.Button1,new string[]{"Exporting"});
					}
					catch
					{
						PCSMessageBox.Show(ErrorCode.CAN_NOT_READ_EXCEL_FILE,MessageBoxButtons.OK,MessageBoxIcon.Error);
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
		//**************************************************************************              
		///    <summary>
		///       Display another form to select data for this column
		///    </summary>
		///    <Inputs>
		///       True DBGrid
		///    </Inputs>
		///    <Outputs>
		///       a new form is displayed 
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

		private void SelectDataFromTable() 
		{
			int intColIndex = tgridViewTable.Col;
			if (tgridViewTable.Splits[0].DisplayColumns[intColIndex].DataColumn.Tag == null)
			{
				return;
			}
			OpenFormToSelectValue(intColIndex);
		}

		//**************************************************************************              
		///    <summary>
		///       Implement Key on form
		///    </summary>
		///    <Inputs>
		///    </Inputs>
		///    <Outputs>
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
		private void ViewTable_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			const string METHOD_NAME = THIS + ".ViewTable_KeyDown()";
			try
			{
				// HACK: Trada 09-03-2006
				if ((e.KeyCode == Keys.Home) && (e.Modifiers == Keys.Control))
				{
					//Return to the first row
					tgridViewTable.Row = 0;
					tgridViewTable.Focus();
					
				} 
				if ((e.KeyCode == Keys.End) && (e.Modifiers == Keys.Control))
				{
					//Return to the first row
					tgridViewTable.Row = tgridViewTable.FirstRow;
					tgridViewTable.Focus();
					
				} 
				// END: Trada 09-03-2006

				// HACK: Trada 01-12-2005
				if ((e.KeyCode == Keys.L) && (e.Modifiers == Keys.Control))
				{
					tgridViewTable.Update();
					TableConfigDetailBO boTableConfigDetail = new TableConfigDetailBO();
					DataSet dstTableConfigDetail = boTableConfigDetail.List(int.Parse(strTableID));

					#region //Save the width of specify column
					if (tgridViewTable.Splits[0].DisplayColumns.Count != 0)
					{
						for (int i = 0; i < tgridViewTable.Splits[0].DisplayColumns.Count; i++)
						{
							foreach(DataRow drow in dstTableConfigDetail.Tables[0].Rows)
							{
								if (tgridViewTable.Columns[i].DataField == drow[sys_TableFieldTable.FIELDNAME_FLD].ToString())
								{
									if (drow[sys_TableFieldTable.FROMFIELD_FLD].ToString() != string.Empty)
									{
										//Set the width of the next column (foreign column) to hidden column
										drow[sys_TableFieldTable.WIDTH_FLD] = tgridViewTable.Splits[0].DisplayColumns[i].Width;
										drow[sys_TableFieldTable.WIDTHFIELD1_FLD] = tgridViewTable.Splits[0].DisplayColumns[i + 1].Width;
										if(drow[sys_TableFieldTable.FILTERFIELD2_FLD].ToString().Trim() != string.Empty)
										{
											drow[sys_TableFieldTable.WIDTHFIELD2_FLD] = tgridViewTable.Splits[0].DisplayColumns[i + 2].Width;
										}
										if(drow[sys_TableFieldTable.FILTERFIELD3_FLD].ToString().Trim() != string.Empty)
										{
											drow[sys_TableFieldTable.WIDTHFIELD3_FLD] = tgridViewTable.Splits[0].DisplayColumns[i + 3].Width;
										}
									}
									else
									{
										drow[sys_TableFieldTable.WIDTH_FLD] = tgridViewTable.Splits[0].DisplayColumns[i].Width;
									}
								}
							}
						}
					}
					// Update to Database
					boTableConfigDetail.UpdateDataSetByTableID(dstTableConfigDetail, int.Parse(strTableID));
					PCSMessageBox.Show(ErrorCode.MESSAGE_AFTER_SAVE_DATA);
					#endregion
				}
				else if ((e.KeyCode == Keys.H) && (e.Modifiers == Keys.Control))
				{
					#region Hide columns

					TableConfigDetailBO boTableConfigDetail = new TableConfigDetailBO();
					DataSet dstTableConfigDetail = boTableConfigDetail.List(int.Parse(strTableID));

					for(int i = 0; i < tgridViewTable.SelectedCols.Count; i++)
					{
						foreach(DataRow drow in dstTableConfigDetail.Tables[0].Rows)
						{
							//Check if columns or relate-columns has been selected 
							if ((tgridViewTable.SelectedCols[i].DataField == drow[sys_TableFieldTable.FIELDNAME_FLD].ToString()))
								//	|| (tgridViewTable.Columns[tgridViewTable.Columns.IndexOf(tgridViewTable.SelectedCols[i]) - 1].DataField  == drow[sys_TableFieldTable.FIELDNAME_FLD].ToString()))
							{
								drow[sys_TableFieldTable.WIDTH_FLD] = 0;
								tgridViewTable.Splits[0].DisplayColumns[tgridViewTable.SelectedCols[i].DataField].Visible = false;
								tgridViewTable.Splits[0].DisplayColumns[tgridViewTable.SelectedCols[i].DataField].AllowSizing = false;
							}

							#region // External field
							if(tgridViewTable.Columns.IndexOf(tgridViewTable.SelectedCols[i]) >= 1) 
							{
								if (tgridViewTable.Columns[tgridViewTable.Columns.IndexOf(tgridViewTable.SelectedCols[i]) - 1].DataField  == drow[sys_TableFieldTable.FIELDNAME_FLD].ToString())
								{
									// Field 1
									drow[sys_TableFieldTable.WIDTHFIELD1_FLD] = 0;
									tgridViewTable.Splits[0].DisplayColumns[tgridViewTable.SelectedCols[i].DataField].Visible = false;
									tgridViewTable.Splits[0].DisplayColumns[tgridViewTable.SelectedCols[i].DataField].AllowSizing = false;
								}
							}
							if(tgridViewTable.Columns.IndexOf(tgridViewTable.SelectedCols[i]) >= 2) 
							{
								if (tgridViewTable.Columns[tgridViewTable.Columns.IndexOf(tgridViewTable.SelectedCols[i]) - 2].DataField  == drow[sys_TableFieldTable.FIELDNAME_FLD].ToString())
								{
									// Field 2
									drow[sys_TableFieldTable.WIDTHFIELD2_FLD] = 0;
									tgridViewTable.Splits[0].DisplayColumns[tgridViewTable.SelectedCols[i].DataField].Visible = false;
									tgridViewTable.Splits[0].DisplayColumns[tgridViewTable.SelectedCols[i].DataField].AllowSizing = false;
								}
							}
							if(tgridViewTable.Columns.IndexOf(tgridViewTable.SelectedCols[i]) >= 3) 
							{
								if (tgridViewTable.Columns[tgridViewTable.Columns.IndexOf(tgridViewTable.SelectedCols[i]) - 3].DataField  == drow[sys_TableFieldTable.FIELDNAME_FLD].ToString())
								{
									// Field 3
									drow[sys_TableFieldTable.WIDTHFIELD3_FLD] = 0;
									tgridViewTable.Splits[0].DisplayColumns[tgridViewTable.SelectedCols[i].DataField].Visible = false;
									tgridViewTable.Splits[0].DisplayColumns[tgridViewTable.SelectedCols[i].DataField].AllowSizing = false;
								}
							}
							#endregion
						}
					}
					//Update to Database
					boTableConfigDetail.UpdateDataSetByTableID(dstTableConfigDetail, int.Parse(strTableID));
					// PCSMessageBox.Show(ErrorCode.MESSAGE_AFTER_SAVE_DATA);

					#endregion
				}
				else if ((e.KeyCode == Keys.G) && (e.Modifiers == Keys.Control))
				{
					TableConfigDetail frmConfig = new TableConfigDetail();
					frmConfig.TableOrView = strTableName;
					frmConfig.TableName = this.Text;
					frmConfig.TableCode = strTableName;
					frmConfig.TableID = Convert.ToInt32(strTableID);
					frmConfig.Show();
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
			// END: Trada 01-12-2005

			/*
			const int TOOLBAR_F1 = 0;
			const int TOOLBAR_F2 = 1;  //F0: Clear all filter
			const int TOOLBAR_F3 = 2;  // Filter with current value
			const int TOOLBAR_F4 = 3;  //Filter with except current value
			const int TOOLBAR_F5 = 4;  //Return previous filter
			const int TOOLBAR_F6 = 5;  //Row filter
			const int TOOLBAR_F7 = 6;  // Single record
			const int TOOLBAR_F8 = 7;  // Sum current column
			const int TOOLBAR_F9 = 8;  //Export data to Excel
			const int TOOLBAR_F10 = 9; //Print data to printer
			const int TOOLBAR_F11 = 10; // Select data from table
			*/
			
			//In order to use this we have to turn the property
			//KEYPREVIEW on form to true
			//otherwise it doesn't work
			switch (e.KeyCode) 
			{
				case Keys.F1:
					break;
				case Keys.F2:
					ClearAllFilter();
					break;
				case Keys.F3:
					FilterWithCurrentValue(false);
					break;
				case Keys.F4:
					FilterWithCurrentValue(true);
					break;
				case Keys.F5:
					ReturnPreviousFilter();
					break;
				case Keys.F6:
					RowFilter();
					break;
				case Keys.F7:
					// view single record
					#region // HACK: DEL SonHT 2005-12-08
					// this.ViewSingleRecord();
					#endregion // END: DEL SonHT 2005-12-08					
					break;
				case Keys.F8:
					SumCurrentColumn();
					break;
				case Keys.F9:
					ExportDataToExcel();
					break;
				case Keys.F10:
					PrintDataToPrinter();
					break;
				case Keys.F11:
					SelectDataFromTable();
					break;
				case Keys.F12:
					//add a new row
					bntAdd_Click(null,null);
					break;
				
			}
		}

		private void tgridViewTable_AfterColEdit(object sender, C1.Win.C1TrueDBGrid.ColEventArgs e)
		{
			if (!blnViewOnly)
			{
				//MessageBox.Show(tgridViewTable.Row.ToString());
				//PHAI XY LY TAI DAY
				if (e.Column.DataColumn.Value !=null && e.Column.DataColumn.Value.ToString() != String.Empty)
				{
					e.Column.DataColumn.Value = e.Column.DataColumn.Value.ToString().Trim();
					ChangeEditFlag(true);
					tgridViewTable.ColumnFooters = false;
				}
			}
			if (e.Column.DataColumn.DataField == SELECT)
			{
				CheckOrNochkCheckAll();
			}
		}

		private void tgridViewTable_BeforeInsert(object sender, C1.Win.C1TrueDBGrid.CancelEventArgs e)
		{
			if (blnViewOnly) 
			{
				return;
			}
			const string METHOD_NAME = THIS + ".tgridViewTable_BeforeInsert()";
			try 
			{
				CurrencyManager cm;

				//cm = (CurrencyManager) tgridViewTable.BindingContext[tgridViewTable.DataSource, tgridViewTable.DataMember];
				cm = (CurrencyManager) tgridViewTable.BindingContext[tgridViewTable.DataSource];
				//End current edit to en-force constraint on the table
				cm.EndCurrentEdit();
				
				// HACK: DuongNA 2005-10-13
				ChangeEditFlag(true);
				// End DuongNA 2005-10-13

				e.Cancel = false;
			}			
			catch (NoNullAllowedException ex) 
			{
				e.Cancel = true;
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
				e.Cancel = true;
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
				e.Cancel = true;
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

		private void tgridViewTable_BeforeUpdate(object sender, C1.Win.C1TrueDBGrid.CancelEventArgs e)
		{
			if (blnViewOnly) 
			{
				return;
			}
			const string METHOD_NAME = THIS + ".tgridViewTable_BeforeUpdate()";
			try 
			{
				CurrencyManager cm;

				//cm = (CurrencyManager) tgridViewTable.BindingContext[tgridViewTable.DataSource, tgridViewTable.DataMember];
				cm = (CurrencyManager) tgridViewTable.BindingContext[tgridViewTable.DataSource];
				//End current edit to en-force constraint on the table
				cm.EndCurrentEdit();
				e.Cancel = false;
			}
			catch (NoNullAllowedException ex) 
			{
				e.Cancel = true;
				//PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID);
				// log message.
				//				try
				//				{
				//					Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
				//				}
				//				catch
				//				{
				//					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
				//				}
			}
			catch (ConstraintException ex) 
			{
				e.Cancel = true;
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
				e.Cancel = true;
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
		//**************************************************************************              
		///    <summary>
		///       Filter the data for the first time to load this form
		///       This will happen when user calls this form from another form
		///       to get data
		///    </summary>
		///    <Inputs>
		///    </Inputs>
		///    <Outputs>
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

		private void FilterGridForTheFirstTime() 
		{
			const string METHOD_NAME = THIS + ".FilterGridForTheFirstTime()";
			const string JOIN_FILTER_OPERATION = " and " ;
			const string FILTER_OPERATION_LIKE  = " like ";
			const string FILTER_OPERATION_EQUAL  = " = ";

			try 
			{
				string strFilterStringColumn = String.Empty;
				if (strFilterFieldName1 !=null 
					&& strFilterFieldName1 != String.Empty 
					&& strFilterFieldValue1 != null 
					&& strFilterFieldValue1 != String.Empty)
				{
					if (dstData.Tables[0].Columns[strFilterFieldName1].DataType == System.Type.GetType("System.String")
						|| dstData.Tables[0].Columns[strFilterFieldName1].DataType == System.Type.GetType("System.Char") )
					{
						strFilterStringColumn = strFilterFieldName1 + FILTER_OPERATION_LIKE +  " '" +  strFilterFieldValue1 + "%'";
					}
					else 
					{
						strFilterStringColumn = strFilterFieldName1 + FILTER_OPERATION_EQUAL + " '" +  strFilterFieldValue1 + "'";
					}
				}

				if (strFilterFieldName2 !=null 
					&& strFilterFieldName2 != String.Empty 
					&& strFilterFieldValue2 != null 
					&& strFilterFieldValue2 != String.Empty)
				{
					if (dstData.Tables[0].Columns[strFilterFieldName2].DataType == System.Type.GetType("System.String")
						|| dstData.Tables[0].Columns[strFilterFieldName2].DataType == System.Type.GetType("System.Char") )
					{
						if (strFilterStringColumn != String.Empty)
						{
							strFilterStringColumn += JOIN_FILTER_OPERATION +  strFilterFieldName2 + FILTER_OPERATION_LIKE +  " '" +  strFilterFieldValue2 + "%'";
						}
						else 
						{
							strFilterStringColumn = strFilterFieldName2 + FILTER_OPERATION_LIKE +  " '" +  strFilterFieldValue2 + "%'";
						}
					}
					else 
					{
						if (strFilterStringColumn != String.Empty)
						{
							strFilterStringColumn += JOIN_FILTER_OPERATION + strFilterFieldName2 + FILTER_OPERATION_EQUAL + " '" +  strFilterFieldValue2 + "'";
						}
						else
						{
							strFilterStringColumn = strFilterFieldName2 + FILTER_OPERATION_EQUAL + " '" +  strFilterFieldValue2 + "'";
						}
					}
				}

				// HACK: SonHT 2005-10-18
				try
				{
					this.dstData.Tables[0].DefaultView.RowFilter = strFilterStringColumn;
					// strPreviousFilterString = strFilterStringColumn;
				}
				catch
				{
					this.dstData.Tables[0].DefaultView.RowFilter = string.Empty;
					// strPreviousFilterString = string.Empty;
				}
				// END: SonHT 2005-10-18

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


		//**************************************************************************              
		///    <summary>
		///       View single record of current record
		///    </summary>
		///    <Inputs>
		///    </Inputs>
		///    <Outputs>
		///    </Outputs>
		///    <Returns>
		///       
		///    </Returns>
		///    <Authors>
		///       DungLa
		///    </Authors>
		///    <History>
		///       02-Feb-2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private void ViewSingleRecord()
		{
			const string METHOD_NAME = THIS + ".ViewSingleRecord()";
			// View single record
			try
			{
				// view single record
				if (blnEditUpdateData)
				{
					PCSMessageBox.Show(ErrorCode.MESSAGE_SAVE_BEFORE_VIEW_RECORD, MessageBoxButtons.OK, MessageBoxIcon.Warning);
					return;
				}
				// get selected row
				DataRow drowSelected = this.dstData.Tables[0].Rows[this.tgridViewTable.Row];
				// convert fields list from DataSet to ArrayList
				ArrayList arrFieldList = new ArrayList();
				foreach (DataRow drowField in dstFieldList.Tables[0].Rows)
				{
					sys_TableFieldVO voTableField = new sys_TableFieldVO();
					voTableField.TableFieldID = int.Parse(drowField[sys_TableFieldTable.TABLEFIELDID_FLD].ToString());
					voTableField.TableID = int.Parse(drowField[sys_TableFieldTable.TABLEID_FLD].ToString());
					voTableField.FieldName = drowField[sys_TableFieldTable.FIELDNAME_FLD].ToString();
					voTableField.CaptionJP = drowField[sys_TableFieldTable.CAPTIONJP_FLD].ToString();
					voTableField.CaptionVN = drowField[sys_TableFieldTable.CAPTIONVN_FLD].ToString();
					voTableField.CaptionEN = drowField[sys_TableFieldTable.CAPTIONEN_FLD].ToString();
					voTableField.Caption = drowField[sys_TableFieldTable.CAPTION_FLD].ToString();
					voTableField.Invisible = bool.Parse(drowField[sys_TableFieldTable.INVISIBLE_FLD].ToString());
					voTableField.CharacterCase = int.Parse(drowField[sys_TableFieldTable.CHARACTERCASE_FLD].ToString());
					voTableField.Align = int.Parse(drowField[sys_TableFieldTable.ALIGN_FLD].ToString());
					voTableField.Width = int.Parse(drowField[sys_TableFieldTable.WIDTH_FLD].ToString());
					voTableField.SortType = int.Parse(drowField[sys_TableFieldTable.SORTTYPE_FLD].ToString());
					voTableField.Formats = drowField[sys_TableFieldTable.FORMATS_FLD].ToString();
					voTableField.ReadOnly = bool.Parse(drowField[sys_TableFieldTable.READONLY_FLD].ToString());
					voTableField.NotAllowNull = bool.Parse(drowField[sys_TableFieldTable.NOTALLOWNULL_FLD].ToString());
					voTableField.IdentityColumn = bool.Parse(drowField[sys_TableFieldTable.IDENTITYCOLUMN_FLD].ToString());
					voTableField.UniqueColumn = bool.Parse(drowField[sys_TableFieldTable.UNIQUECOLUMN_FLD].ToString());
					voTableField.Items = drowField[sys_TableFieldTable.ITEMS_FLD].ToString();
					voTableField.FromTable = drowField[sys_TableFieldTable.FROMTABLE_FLD].ToString();
					voTableField.FromField = drowField[sys_TableFieldTable.FROMFIELD_FLD].ToString();
					voTableField.FilterField1 = drowField[sys_TableFieldTable.FILTERFIELD1_FLD].ToString();
					voTableField.FilterField2 = drowField[sys_TableFieldTable.FILTERFIELD2_FLD].ToString();
					voTableField.FieldOrder = int.Parse(drowField[sys_TableFieldTable.FIELDORDER_FLD].ToString());
					// add to field list
					arrFieldList.Add(voTableField);
				}
				// trim to actual size
				arrFieldList.TrimToSize();
				ViewSingleRecord frmViewSingleRecord = new ViewSingleRecord(this.voSysTable.TableName);
				frmViewSingleRecord.TableData = this.dstData;
				frmViewSingleRecord.RecordData = drowSelected;
				frmViewSingleRecord.RecordFields = arrFieldList;
				frmViewSingleRecord.SelectCommand = this.strSqlSelectCommand;
				frmViewSingleRecord.UpdateCommand = this.strSqlSelectUpdateCommand;
				frmViewSingleRecord.ViewOnly = this.blnViewOnly;
				
				frmViewSingleRecord.ShowDialog();
				//				this.dstData = frmViewSingleRecord.TableData.GetChanges(DataRowState.Added | DataRowState.Deleted | DataRowState.Modified);
				//				this.dstData.AcceptChanges();
				
				if (frmViewSingleRecord.DeleteRow)
				{
					this.tgridViewTable.Delete();
					this.tgridViewTable.UpdateData();
					this.SaveDataToDatabase();
				}

				this.tgridViewTable.Refresh();
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
			catch (IndexOutOfRangeException ex)
			{
				// displays the error message.
				PCSMessageBox.Show(ErrorCode.MSG_VIEWTABLE_SELECT_ROW);
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

		//		private void tgridViewTable_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		//		{
		//			const string METHOD_NAME = THIS + ".tgridViewTable_KeyDown()";
		//			try 
		//			{
		//				if (e.KeyCode == Keys.F4)
		//				{
		//
		//					if (tgridViewTable.Columns[tgridViewTable.Col].Tag == null)
		//					{
		//						return;
		//					}
		//					if (!tgridViewTable.Splits[0].DisplayColumns[tgridViewTable.Col].Button)
		//					{
		//						return;
		//					}
		//					int intColIndex = tgridViewTable.Col;
		//					OpenFormToSelectValue(intColIndex);
		//				}
		//			}
		//			catch (Exception ex) 
		//			{
		//				// displays the error message.
		//				PCSMessageBox.Show(ErrorCode.OTHER_ERROR,MessageBoxIcon.Error);
		//				// log message.
		//				try
		//				{
		//					Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
		//				}
		//				catch
		//				{
		//					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION,MessageBoxIcon.Error);
		//				}
		//			}
		//		
		//		}

		private void tgridViewTable_AfterDelete(object sender, System.EventArgs e)
		{
			ChangeEditFlag(true);
		}

		private void tgridViewTable_KeyDown_1(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if (blnGetData) 
			{
				if (e.KeyCode == Keys.Enter)
				{
					btnOK_Click(null,null);
				}
			}
			
		}

		private void tgridViewTable_Click(object sender, System.EventArgs e)
		{
			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.WaitCursor;

			#endregion Code Inserted Automatically

		
		

			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.Default;

			#endregion Code Inserted Automatically

		
		}

		private void tgridViewTable_RowColChange(object sender, C1.Win.C1TrueDBGrid.RowColChangeEventArgs e)
		{
			const string METHOD_NAME = THIS + ".tgridViewTable_RowColChange()";
			try 
			{
				/// HACKED: Thachnn : by SonHT
				if (!blnAllowEditDateTime)
				{
					dtDateTimePicker.Visible = false;
					return;
				}

				if (this.tgridViewTable.Splits[0].DisplayColumns[this.tgridViewTable.Col].Button) 
				{
					//this is really not a date time column
					dtDateTimePicker.Visible = false;
					return;
				}

				if (this.tgridViewTable.Splits[0].DisplayColumns[this.tgridViewTable.Col].DataColumn.Tag == null)
				{
					dtDateTimePicker.Visible = false;
					return;
				}

				// position the datetime picker on the "BirthDate" column only
				C1.Win.C1TrueDBGrid.C1DataColumn col  = this.tgridViewTable.Splits[0].DisplayColumns[this.tgridViewTable.Col].DataColumn;
				//' get the area of the cell
				
				
				string strDateTimeCol = col.Tag.ToString();
				if (strDateTimeCol != Constants.DATETIME_TYPE)
				{
					return;
				}
			
				
				//this.dtRequiredDate.Visible = false;
				//this.dtPromiseDate.Visible = false;
				//this.dtScheduleDate.Visible = false;
				dtDateTimePicker.Visible = false;
				Rectangle r;
				r = this.tgridViewTable.Splits[0].GetCellBounds(this.tgridViewTable.Row, this.tgridViewTable.Col);
				//' change to screen coordiantes
				r = this.tgridViewTable.RectangleToScreen(r);
				dtDateTimePicker.Location = this.RectangleToClient(r).Location;
				dtDateTimePicker.Size = r.Size;
				blnIgnoreChange = true;
				if (col.CellValue(this.tgridViewTable.Row).ToString() != String.Empty)
				{
					//this.dtDateTimePicker.Value =DateTime.Parse(col.CellValue(this.tgridViewData.Row).ToString());
					this.dtDateTimePicker.Value =col.CellValue(this.tgridViewTable.Row).ToString();
				}
				else
				{
					dtDateTimePicker.Value = DBNull.Value;
				}
				blnIgnoreChange = false;
				//' display it
				dtDateTimePicker.Visible = true;
			}				
			catch (Exception ex)
			{
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

		private void UpdateDateTimeBackToGridColumn1(C1.Win.C1Input.C1DateEdit dtDateTimeControl)
		{
			//			try 
			//			{
			if (blnIgnoreChange)
			{
				return;
			}
			//' get the column
			C1.Win.C1TrueDBGrid.C1DataColumn col = tgridViewTable.Splits[0].DisplayColumns[tgridViewTable.Col].DataColumn;
			//' update the the grid
			if (dtDateTimeControl.Value != DBNull.Value)
			{
				DateTime dtmTmp = (DateTime)dtDateTimeControl.Value;
				col.Text = dtmTmp.ToString(CONVERT_DATE_TOSTRING) ; //+ Constants.WHITE_SPACE + dtmTmp.ToString(CONVERT_TIME_TOSTRING) ;
			}
			else
			{
				col.Text = String.Empty;
			}
			//			}
			//			catch(Exception ex)
			//			{
			//				throw ex;
			//			}

		}

		private void dtDateTimePicker_ValueChanged(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".dtDateTimePicker_ValueChanged()";
			try 
			{
				UpdateDateTimeBackToGridColumn1((C1.Win.C1Input.C1DateEdit)sender);			
			}			
			catch (Exception ex)
			{
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

		private void tgridViewTable_FormatText(object sender, C1.Win.C1TrueDBGrid.FormatTextEventArgs e)
		{			
			if (Convert.ToInt32(e.Column.Tag) == INT_UPPER)
			{
				e.Value = e.Value.ToUpper();
			}
			else if (Convert.ToInt32(e.Column.Tag) == INT_LOWER)
			{
				e.Value = e.Value.ToLower();
			}
		}

		private void tgridViewTable_Error(object sender, C1.Win.C1TrueDBGrid.ErrorEventArgs e)
		{
			e.Handled = true;
			e.Continue = true;
		}

		private void ViewTable_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			
		}
		/// <summary>
		/// chkSelectAll_CheckedChanged
		/// </summary>
		/// <author>Trada</author>
		/// <date>Monday, September 25 2006</date>
		/// <param name="sender"></param>
		/// <param name="e"></param>	
		private void chkSelectAll_CheckedChanged(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".chkSelectAll_CheckedChanged()";
			try
			{

                if ((blnStateOfCheck) && (tgridViewTable.RowCount != 0))
				{
					if (chkSelectAll.Checked)
					{
                        for (int i = 0; i < tgridViewTable.RowCount; i++)
						{
							tgridViewTable[i, SELECT] = true;
						}
					}
					else
					{
                        for (int i = 0; i < tgridViewTable.RowCount; i++)
						{
							tgridViewTable[i, SELECT] = false;
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
		/// CheckOrNochkCheckAll
		/// </summary>
		/// <author>Trada</author>
		/// <date>Friday, June 10 2005</date>
		private void CheckOrNochkCheckAll()
		{
            for (int i = 0; i < tgridViewTable.RowCount; i++)
			{
				if (tgridViewTable[i, SELECT].ToString().Trim() != true.ToString())
				{
					chkSelectAll.Checked = false;
					return;
				}
			}
			chkSelectAll.Checked = true;
		}
		private void chkSelectAll_Enter(object sender, System.EventArgs e)
		{
			blnStateOfCheck = true;
		}

		private void chkSelectAll_Leave(object sender, System.EventArgs e)
		{
			blnStateOfCheck = false;
		}


	
		public bool ViewOnly
		{
			set { blnViewOnly = value; }
		}

		public string TableID
		{
			set { strTableID = value; }
		}

		public string TableName
		{
			set 
			{ 
				strTableName = value.Trim(); 
			}
		}

		public string ReturnField
		{
			set 
			{ 
				strReturnFieldName = value; 
			}
			get 
			{ 
				return strReturnFieldValue; 
			}
		}

		public string FilterField1
		{
			get 
			{ 
				return strFilterFieldName1; 
			}
			set 
			{ 
				strFilterFieldName1 = value; 
			}
		}
		public string FilterFieldValue1 
		{
			get 
			{ 
				return strFilterFieldValue1; 
			}
			set 
			{ 
				strFilterFieldValue1 = value; 
			}

		}

		public string FilterField2
		{
			get 
			{ 
				return strFilterFieldValue2; 
			}
			set 
			{ strFilterFieldName2 = value; 
			}
		}
		public string FilterFieldValue2 
		{
			get 
			{ 
				return strFilterFieldValue2; 
			}
			set 
			{ 
				strFilterFieldValue2 = value; 
			}

		}

		public bool GetData
		{
			set 
			{
				blnGetData = value;
			}
			get 
			{
				return blnGetData;
			}
		}
		public string WhereClause 
		{
			set 
			{
				strWhereClause = value;
			}
			get 
			{
				return strWhereClause;
			}
		}
		public DataRowView ReturnDataRowView
		{
			get 
			{
				return drvReturnDataRowView;
			}
		}
		public DataTable ReturnTable
		{
			get 
			{
				return dtbReturnTable;
			}
		}

	}
}
