using System;
using System.Data;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

using C1.Win.C1TrueDBGrid;

//Using PCS's namespaces
using PCSComProduction.DCP.BO;
using PCSComProduction.DCP.DS;
using PCSComUtils.Common;
using PCSComUtils.Common.BO;
using PCSComUtils.MasterSetup.DS;
using PCSComUtils.PCSExc;
using PCSUtils.Log;
using PCSUtils.Utils;

namespace PCSProduction.DCP
{
	/// <summary>
	/// Summary description for WorkCenterCapacity.
	/// </summary>
	public class WorkCenterCapacity : System.Windows.Forms.Form
	{
		#region Declaration
		
		#region System Genarated Declaration

		private System.Windows.Forms.Label lblWorkCenter;
		private System.Windows.Forms.Button btnSave;
		private System.Windows.Forms.Button btnHelp;
		private System.Windows.Forms.Button btnClose;
		private System.Windows.Forms.Button btnDelete;
		private C1.Win.C1List.C1Combo cboCCN;
		private System.Windows.Forms.Label lblCCN;
		private System.Windows.Forms.TextBox txtWorkCenter;
		private C1.Win.C1TrueDBGrid.C1TrueDBGrid dgrdData;
		private System.Windows.Forms.Button btnWorkCenterSearch;
		private System.Windows.Forms.GroupBox grpShift;
		private C1.Win.C1TrueDBGrid.C1TrueDBGrid dgrdShift;
		private System.Windows.Forms.TextBox txtWCName;
		private C1.Win.C1Input.C1DateEdit dtmDate;
		private C1.Win.C1Input.C1NumericEdit numDecValue;
		private C1.Win.C1Input.C1NumericEdit numIntValue;
		private System.Windows.Forms.TextBox txtCycle;
		private System.Windows.Forms.Button btnCycle;
		private System.Windows.Forms.Label lblCycle;
		private System.Windows.Forms.Button btnAdjust;		
		private System.Windows.Forms.Label lblTo;
		private System.Windows.Forms.TextBox txtCycleRange;
		private System.Windows.Forms.Label lblActive;		
		private System.ComponentModel.Container components = null;

		#endregion

		#region Constants
		//========================================================
		private const string THIS = "PCSProduction.DCP.WorkCenterCapacity";		
		private const string ZERO_STRING = "0";				
		private const string SELECT_COLUMN = "Select_Col";
		private const string DELIMITER_CHAR = ",";
		private const string SHIFTPATTERN_COLUMN = "ShiftPattern";
		private const string PERCENT_VALUE_RANGE = "(0, 100]";
		private const string DAY_FLD = "Day";
		private const string NEWID_FLD = "NewID";
		private const string NEED_TO_DELETE = "NeedToDelete";
		
		
		#endregion Constants
		
		#region Variables
		private EnumAction enuFormAction = EnumAction.Default;
		private DataTable dtbGridLayOut;
		private DataTable dtbShiftLayOut;
		private DataTable dtbWCCapacity;
		private DataTable dtbWCCapacityToStore = null;
		private DataTable dtbShiftCapacity;
		private DataTable dtbShiftTable = new DataTable(PRO_ShiftTable.TABLE_NAME);
		private WorkCenterCapacityBO boWCCapacity = new WorkCenterCapacityBO();		
		private ArrayList arlDeletedIDs = new ArrayList();
		private int intWCCapacityFakeId = 0;		
		private int intWCCapacityIDNeedToDelete = 0;		
		private bool blnDataIsValid = false;		
		DataSet dstWorkingDayCalendar = new	DataSet();
		int intDCPCycleID = 0;
		private DateTime dtmBeginCycleDate = DateTime.MinValue;		
		private DateTime dtmEndCycleDate = DateTime.MinValue;
		private DateTime dtmBeginCycleDateToCheck = DateTime.MinValue;		
		private DateTime dtmEndCycleDateToCheck = DateTime.MinValue;
		private bool blnHasSeparate = false;		
		private bool blnHasAdjust = false;		
		#endregion Variables

		#endregion Declaration

		#region Constructor, Destructor

		private int mintWorkCenterID = 0;
		private System.Windows.Forms.Button btnSeparate;
		private int mintDCPCycleID = 0;

		public WorkCenterCapacity()
		{			
			InitializeComponent();

			//Assign default WC to 0
			mintWorkCenterID = 0;
			mintDCPCycleID = 0;
		}
		
		public WorkCenterCapacity(int pintWorkCenterID)
		{
			InitializeComponent();

			//Assign default WC to 0
			mintWorkCenterID = pintWorkCenterID;
			mintDCPCycleID = 0;
		}

		public WorkCenterCapacity(int pintWorkCenterID, int pintDCPCycleID)
		{
			InitializeComponent();

			//Assign default WC to 0
			mintWorkCenterID = pintWorkCenterID;
			mintDCPCycleID = pintDCPCycleID;
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}
		
		#endregion Constructor, Destructor

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(WorkCenterCapacity));
			this.lblWorkCenter = new System.Windows.Forms.Label();
			this.btnSave = new System.Windows.Forms.Button();
			this.btnHelp = new System.Windows.Forms.Button();
			this.btnClose = new System.Windows.Forms.Button();
			this.btnDelete = new System.Windows.Forms.Button();
			this.dgrdData = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
			this.cboCCN = new C1.Win.C1List.C1Combo();
			this.lblCCN = new System.Windows.Forms.Label();
			this.txtWorkCenter = new System.Windows.Forms.TextBox();
			this.btnWorkCenterSearch = new System.Windows.Forms.Button();
			this.grpShift = new System.Windows.Forms.GroupBox();
			this.dgrdShift = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
			this.numDecValue = new C1.Win.C1Input.C1NumericEdit();
			this.txtWCName = new System.Windows.Forms.TextBox();
			this.dtmDate = new C1.Win.C1Input.C1DateEdit();
			this.numIntValue = new C1.Win.C1Input.C1NumericEdit();
			this.txtCycle = new System.Windows.Forms.TextBox();
			this.btnCycle = new System.Windows.Forms.Button();
			this.lblCycle = new System.Windows.Forms.Label();
			this.btnAdjust = new System.Windows.Forms.Button();
			this.lblActive = new System.Windows.Forms.Label();
			this.lblTo = new System.Windows.Forms.Label();
			this.txtCycleRange = new System.Windows.Forms.TextBox();
			this.btnSeparate = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.dgrdData)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.cboCCN)).BeginInit();
			this.grpShift.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgrdShift)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numDecValue)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dtmDate)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numIntValue)).BeginInit();
			this.SuspendLayout();
			// 
			// lblWorkCenter
			// 
			this.lblWorkCenter.AccessibleDescription = resources.GetString("lblWorkCenter.AccessibleDescription");
			this.lblWorkCenter.AccessibleName = resources.GetString("lblWorkCenter.AccessibleName");
			this.lblWorkCenter.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblWorkCenter.Anchor")));
			this.lblWorkCenter.AutoSize = ((bool)(resources.GetObject("lblWorkCenter.AutoSize")));
			this.lblWorkCenter.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblWorkCenter.Dock")));
			this.lblWorkCenter.Enabled = ((bool)(resources.GetObject("lblWorkCenter.Enabled")));
			this.lblWorkCenter.Font = ((System.Drawing.Font)(resources.GetObject("lblWorkCenter.Font")));
			this.lblWorkCenter.ForeColor = System.Drawing.Color.Maroon;
			this.lblWorkCenter.Image = ((System.Drawing.Image)(resources.GetObject("lblWorkCenter.Image")));
			this.lblWorkCenter.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblWorkCenter.ImageAlign")));
			this.lblWorkCenter.ImageIndex = ((int)(resources.GetObject("lblWorkCenter.ImageIndex")));
			this.lblWorkCenter.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblWorkCenter.ImeMode")));
			this.lblWorkCenter.Location = ((System.Drawing.Point)(resources.GetObject("lblWorkCenter.Location")));
			this.lblWorkCenter.Name = "lblWorkCenter";
			this.lblWorkCenter.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblWorkCenter.RightToLeft")));
			this.lblWorkCenter.Size = ((System.Drawing.Size)(resources.GetObject("lblWorkCenter.Size")));
			this.lblWorkCenter.TabIndex = ((int)(resources.GetObject("lblWorkCenter.TabIndex")));
			this.lblWorkCenter.Text = resources.GetString("lblWorkCenter.Text");
			this.lblWorkCenter.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblWorkCenter.TextAlign")));
			this.lblWorkCenter.Visible = ((bool)(resources.GetObject("lblWorkCenter.Visible")));
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
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			this.btnSave.EnabledChanged += new System.EventHandler(this.btnSave_EnabledChanged);
			// 
			// btnHelp
			// 
			this.btnHelp.AccessibleDescription = resources.GetString("btnHelp.AccessibleDescription");
			this.btnHelp.AccessibleName = resources.GetString("btnHelp.AccessibleName");
			this.btnHelp.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnHelp.Anchor")));
			this.btnHelp.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnHelp.BackgroundImage")));
			this.btnHelp.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnHelp.Dock")));
			this.btnHelp.Enabled = ((bool)(resources.GetObject("btnHelp.Enabled")));
			this.btnHelp.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnHelp.FlatStyle")));
			this.btnHelp.Font = ((System.Drawing.Font)(resources.GetObject("btnHelp.Font")));
			this.btnHelp.Image = ((System.Drawing.Image)(resources.GetObject("btnHelp.Image")));
			this.btnHelp.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnHelp.ImageAlign")));
			this.btnHelp.ImageIndex = ((int)(resources.GetObject("btnHelp.ImageIndex")));
			this.btnHelp.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnHelp.ImeMode")));
			this.btnHelp.Location = ((System.Drawing.Point)(resources.GetObject("btnHelp.Location")));
			this.btnHelp.Name = "btnHelp";
			this.btnHelp.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnHelp.RightToLeft")));
			this.btnHelp.Size = ((System.Drawing.Size)(resources.GetObject("btnHelp.Size")));
			this.btnHelp.TabIndex = ((int)(resources.GetObject("btnHelp.TabIndex")));
			this.btnHelp.Text = resources.GetString("btnHelp.Text");
			this.btnHelp.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnHelp.TextAlign")));
			this.btnHelp.Visible = ((bool)(resources.GetObject("btnHelp.Visible")));
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
			// btnDelete
			// 
			this.btnDelete.AccessibleDescription = resources.GetString("btnDelete.AccessibleDescription");
			this.btnDelete.AccessibleName = resources.GetString("btnDelete.AccessibleName");
			this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnDelete.Anchor")));
			this.btnDelete.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnDelete.BackgroundImage")));
			this.btnDelete.DialogResult = System.Windows.Forms.DialogResult.Cancel;
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
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
			// 
			// dgrdData
			// 
			this.dgrdData.AccessibleDescription = resources.GetString("dgrdData.AccessibleDescription");
			this.dgrdData.AccessibleName = resources.GetString("dgrdData.AccessibleName");
			this.dgrdData.AllowAddNew = ((bool)(resources.GetObject("dgrdData.AllowAddNew")));
			this.dgrdData.AllowArrows = ((bool)(resources.GetObject("dgrdData.AllowArrows")));
			this.dgrdData.AllowColMove = ((bool)(resources.GetObject("dgrdData.AllowColMove")));
			this.dgrdData.AllowColSelect = ((bool)(resources.GetObject("dgrdData.AllowColSelect")));
			this.dgrdData.AllowDelete = ((bool)(resources.GetObject("dgrdData.AllowDelete")));
			this.dgrdData.AllowDrag = ((bool)(resources.GetObject("dgrdData.AllowDrag")));
			this.dgrdData.AllowFilter = ((bool)(resources.GetObject("dgrdData.AllowFilter")));
			this.dgrdData.AllowHorizontalSplit = ((bool)(resources.GetObject("dgrdData.AllowHorizontalSplit")));
			this.dgrdData.AllowRowSelect = ((bool)(resources.GetObject("dgrdData.AllowRowSelect")));
			this.dgrdData.AllowSort = ((bool)(resources.GetObject("dgrdData.AllowSort")));
			this.dgrdData.AllowUpdate = ((bool)(resources.GetObject("dgrdData.AllowUpdate")));
			this.dgrdData.AllowUpdateOnBlur = ((bool)(resources.GetObject("dgrdData.AllowUpdateOnBlur")));
			this.dgrdData.AllowVerticalSplit = ((bool)(resources.GetObject("dgrdData.AllowVerticalSplit")));
			this.dgrdData.AlternatingRows = ((bool)(resources.GetObject("dgrdData.AlternatingRows")));
			this.dgrdData.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("dgrdData.Anchor")));
			this.dgrdData.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("dgrdData.BackgroundImage")));
			this.dgrdData.BorderStyle = ((System.Windows.Forms.BorderStyle)(resources.GetObject("dgrdData.BorderStyle")));
			this.dgrdData.Caption = resources.GetString("dgrdData.Caption");
			this.dgrdData.CaptionHeight = ((int)(resources.GetObject("dgrdData.CaptionHeight")));
			this.dgrdData.CellTipsDelay = ((int)(resources.GetObject("dgrdData.CellTipsDelay")));
			this.dgrdData.CellTipsWidth = ((int)(resources.GetObject("dgrdData.CellTipsWidth")));
			this.dgrdData.ChildGrid = ((C1.Win.C1TrueDBGrid.C1TrueDBGrid)(resources.GetObject("dgrdData.ChildGrid")));
			this.dgrdData.CollapseColor = ((System.Drawing.Color)(resources.GetObject("dgrdData.CollapseColor")));
			this.dgrdData.ColumnFooters = ((bool)(resources.GetObject("dgrdData.ColumnFooters")));
			this.dgrdData.ColumnHeaders = ((bool)(resources.GetObject("dgrdData.ColumnHeaders")));
			this.dgrdData.DefColWidth = ((int)(resources.GetObject("dgrdData.DefColWidth")));
			this.dgrdData.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("dgrdData.Dock")));
			this.dgrdData.EditDropDown = ((bool)(resources.GetObject("dgrdData.EditDropDown")));
			this.dgrdData.EmptyRows = ((bool)(resources.GetObject("dgrdData.EmptyRows")));
			this.dgrdData.Enabled = ((bool)(resources.GetObject("dgrdData.Enabled")));
			this.dgrdData.ExpandColor = ((System.Drawing.Color)(resources.GetObject("dgrdData.ExpandColor")));
			this.dgrdData.ExposeCellMode = ((C1.Win.C1TrueDBGrid.ExposeCellModeEnum)(resources.GetObject("dgrdData.ExposeCellMode")));
			this.dgrdData.ExtendRightColumn = ((bool)(resources.GetObject("dgrdData.ExtendRightColumn")));
			this.dgrdData.FetchRowStyles = ((bool)(resources.GetObject("dgrdData.FetchRowStyles")));
			this.dgrdData.FilterBar = ((bool)(resources.GetObject("dgrdData.FilterBar")));
			this.dgrdData.FlatStyle = ((C1.Win.C1TrueDBGrid.FlatModeEnum)(resources.GetObject("dgrdData.FlatStyle")));
			this.dgrdData.Font = ((System.Drawing.Font)(resources.GetObject("dgrdData.Font")));
			this.dgrdData.GroupByAreaVisible = ((bool)(resources.GetObject("dgrdData.GroupByAreaVisible")));
			this.dgrdData.GroupByCaption = resources.GetString("dgrdData.GroupByCaption");
			this.dgrdData.Images.Add(((System.Drawing.Image)(resources.GetObject("resource"))));
			this.dgrdData.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("dgrdData.ImeMode")));
			this.dgrdData.LinesPerRow = ((int)(resources.GetObject("dgrdData.LinesPerRow")));
			this.dgrdData.Location = ((System.Drawing.Point)(resources.GetObject("dgrdData.Location")));
			this.dgrdData.MarqueeStyle = C1.Win.C1TrueDBGrid.MarqueeEnum.DottedCellBorder;
			this.dgrdData.Name = "dgrdData";
			this.dgrdData.PictureAddnewRow = ((System.Drawing.Image)(resources.GetObject("dgrdData.PictureAddnewRow")));
			this.dgrdData.PictureCurrentRow = ((System.Drawing.Image)(resources.GetObject("dgrdData.PictureCurrentRow")));
			this.dgrdData.PictureFilterBar = ((System.Drawing.Image)(resources.GetObject("dgrdData.PictureFilterBar")));
			this.dgrdData.PictureFooterRow = ((System.Drawing.Image)(resources.GetObject("dgrdData.PictureFooterRow")));
			this.dgrdData.PictureHeaderRow = ((System.Drawing.Image)(resources.GetObject("dgrdData.PictureHeaderRow")));
			this.dgrdData.PictureModifiedRow = ((System.Drawing.Image)(resources.GetObject("dgrdData.PictureModifiedRow")));
			this.dgrdData.PictureStandardRow = ((System.Drawing.Image)(resources.GetObject("dgrdData.PictureStandardRow")));
			this.dgrdData.PreviewInfo.AllowSizing = ((bool)(resources.GetObject("dgrdData.PreviewInfo.AllowSizing")));
			this.dgrdData.PreviewInfo.Caption = resources.GetString("dgrdData.PreviewInfo.Caption");
			this.dgrdData.PreviewInfo.Location = ((System.Drawing.Point)(resources.GetObject("dgrdData.PreviewInfo.Location")));
			this.dgrdData.PreviewInfo.Size = ((System.Drawing.Size)(resources.GetObject("dgrdData.PreviewInfo.Size")));
			this.dgrdData.PreviewInfo.ToolBars = ((bool)(resources.GetObject("dgrdData.PreviewInfo.ToolBars")));
			this.dgrdData.PreviewInfo.UIStrings.Content = ((string[])(resources.GetObject("dgrdData.PreviewInfo.UIStrings.Content")));
			this.dgrdData.PreviewInfo.ZoomFactor = ((System.Double)(resources.GetObject("dgrdData.PreviewInfo.ZoomFactor")));
			this.dgrdData.PrintInfo.MaxRowHeight = ((int)(resources.GetObject("dgrdData.PrintInfo.MaxRowHeight")));
			this.dgrdData.PrintInfo.OwnerDrawPageFooter = ((bool)(resources.GetObject("dgrdData.PrintInfo.OwnerDrawPageFooter")));
			this.dgrdData.PrintInfo.OwnerDrawPageHeader = ((bool)(resources.GetObject("dgrdData.PrintInfo.OwnerDrawPageHeader")));
			this.dgrdData.PrintInfo.PageFooter = resources.GetString("dgrdData.PrintInfo.PageFooter");
			this.dgrdData.PrintInfo.PageFooterHeight = ((int)(resources.GetObject("dgrdData.PrintInfo.PageFooterHeight")));
			this.dgrdData.PrintInfo.PageHeader = resources.GetString("dgrdData.PrintInfo.PageHeader");
			this.dgrdData.PrintInfo.PageHeaderHeight = ((int)(resources.GetObject("dgrdData.PrintInfo.PageHeaderHeight")));
			this.dgrdData.PrintInfo.PrintHorizontalSplits = ((bool)(resources.GetObject("dgrdData.PrintInfo.PrintHorizontalSplits")));
			this.dgrdData.PrintInfo.ProgressCaption = resources.GetString("dgrdData.PrintInfo.ProgressCaption");
			this.dgrdData.PrintInfo.RepeatColumnFooters = ((bool)(resources.GetObject("dgrdData.PrintInfo.RepeatColumnFooters")));
			this.dgrdData.PrintInfo.RepeatColumnHeaders = ((bool)(resources.GetObject("dgrdData.PrintInfo.RepeatColumnHeaders")));
			this.dgrdData.PrintInfo.RepeatGridHeader = ((bool)(resources.GetObject("dgrdData.PrintInfo.RepeatGridHeader")));
			this.dgrdData.PrintInfo.RepeatSplitHeaders = ((bool)(resources.GetObject("dgrdData.PrintInfo.RepeatSplitHeaders")));
			this.dgrdData.PrintInfo.ShowOptionsDialog = ((bool)(resources.GetObject("dgrdData.PrintInfo.ShowOptionsDialog")));
			this.dgrdData.PrintInfo.ShowProgressForm = ((bool)(resources.GetObject("dgrdData.PrintInfo.ShowProgressForm")));
			this.dgrdData.PrintInfo.ShowSelection = ((bool)(resources.GetObject("dgrdData.PrintInfo.ShowSelection")));
			this.dgrdData.PrintInfo.UseGridColors = ((bool)(resources.GetObject("dgrdData.PrintInfo.UseGridColors")));
			this.dgrdData.RecordSelectors = ((bool)(resources.GetObject("dgrdData.RecordSelectors")));
			this.dgrdData.RecordSelectorWidth = ((int)(resources.GetObject("dgrdData.RecordSelectorWidth")));
			this.dgrdData.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("dgrdData.RightToLeft")));
			this.dgrdData.RowDivider.Color = ((System.Drawing.Color)(resources.GetObject("resource.Color")));
			this.dgrdData.RowDivider.Style = ((C1.Win.C1TrueDBGrid.LineStyleEnum)(resources.GetObject("resource.Style")));
			this.dgrdData.RowHeight = ((int)(resources.GetObject("dgrdData.RowHeight")));
			this.dgrdData.RowSubDividerColor = ((System.Drawing.Color)(resources.GetObject("dgrdData.RowSubDividerColor")));
			this.dgrdData.ScrollTips = ((bool)(resources.GetObject("dgrdData.ScrollTips")));
			this.dgrdData.ScrollTrack = ((bool)(resources.GetObject("dgrdData.ScrollTrack")));
			this.dgrdData.Size = ((System.Drawing.Size)(resources.GetObject("dgrdData.Size")));
			this.dgrdData.SpringMode = ((bool)(resources.GetObject("dgrdData.SpringMode")));
			this.dgrdData.TabAcrossSplits = ((bool)(resources.GetObject("dgrdData.TabAcrossSplits")));
			this.dgrdData.TabIndex = ((int)(resources.GetObject("dgrdData.TabIndex")));
			this.dgrdData.Text = resources.GetString("dgrdData.Text");
			this.dgrdData.ViewCaptionWidth = ((int)(resources.GetObject("dgrdData.ViewCaptionWidth")));
			this.dgrdData.ViewColumnWidth = ((int)(resources.GetObject("dgrdData.ViewColumnWidth")));
			this.dgrdData.Visible = ((bool)(resources.GetObject("dgrdData.Visible")));
			this.dgrdData.WrapCellPointer = ((bool)(resources.GetObject("dgrdData.WrapCellPointer")));
			this.dgrdData.AfterDelete += new System.EventHandler(this.dgrdData_AfterDelete);
			this.dgrdData.RowColChange += new C1.Win.C1TrueDBGrid.RowColChangeEventHandler(this.dgrdData_RowColChange);
			this.dgrdData.AfterColUpdate += new C1.Win.C1TrueDBGrid.ColEventHandler(this.dgrdData_AfterColUpdate);
			this.dgrdData.BeforeColUpdate += new C1.Win.C1TrueDBGrid.BeforeColUpdateEventHandler(this.dgrdData_BeforeColUpdate);
			this.dgrdData.BeforeDelete += new C1.Win.C1TrueDBGrid.CancelEventHandler(this.dgrdData_BeforeDelete);
			this.dgrdData.Leave += new System.EventHandler(this.dgrdData_Leave);
			this.dgrdData.AfterInsert += new System.EventHandler(this.dgrdData_AfterInsert);
			this.dgrdData.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgrdData_KeyDown);
			this.dgrdData.OnAddNew += new System.EventHandler(this.dgrdData_OnAddNew);
			this.dgrdData.PropBag = resources.GetString("dgrdData.PropBag");
			// 
			// cboCCN
			// 
			this.cboCCN.AccessibleDescription = resources.GetString("cboCCN.AccessibleDescription");
			this.cboCCN.AccessibleName = resources.GetString("cboCCN.AccessibleName");
			this.cboCCN.AddItemSeparator = ';';
			this.cboCCN.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("cboCCN.Anchor")));
			this.cboCCN.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cboCCN.BackgroundImage")));
			this.cboCCN.Caption = "";
			this.cboCCN.CaptionHeight = 17;
			this.cboCCN.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
			this.cboCCN.ColumnCaptionHeight = 17;
			this.cboCCN.ColumnFooterHeight = 17;
			this.cboCCN.ComboStyle = C1.Win.C1List.ComboStyleEnum.DropdownList;
			this.cboCCN.ContentHeight = 15;
			this.cboCCN.DeadAreaBackColor = System.Drawing.Color.Empty;
			this.cboCCN.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("cboCCN.Dock")));
			this.cboCCN.DropdownPosition = C1.Win.C1List.DropdownPositionEnum.RightDown;
			this.cboCCN.DropDownWidth = 200;
			this.cboCCN.EditorBackColor = System.Drawing.SystemColors.Window;
			this.cboCCN.EditorFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.cboCCN.EditorForeColor = System.Drawing.SystemColors.WindowText;
			this.cboCCN.EditorHeight = 15;
			this.cboCCN.Enabled = ((bool)(resources.GetObject("cboCCN.Enabled")));
			this.cboCCN.FlatStyle = C1.Win.C1List.FlatModeEnum.System;
			this.cboCCN.Font = ((System.Drawing.Font)(resources.GetObject("cboCCN.Font")));
			this.cboCCN.GapHeight = 2;
			this.cboCCN.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("cboCCN.ImeMode")));
			this.cboCCN.ItemHeight = 15;
			this.cboCCN.Location = ((System.Drawing.Point)(resources.GetObject("cboCCN.Location")));
			this.cboCCN.MatchEntryTimeout = ((long)(2000));
			this.cboCCN.MaxDropDownItems = ((short)(5));
			this.cboCCN.MaxLength = 32767;
			this.cboCCN.MouseCursor = System.Windows.Forms.Cursors.Default;
			this.cboCCN.Name = "cboCCN";
			this.cboCCN.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("cboCCN.RightToLeft")));
			this.cboCCN.RowDivider.Color = System.Drawing.Color.DarkGray;
			this.cboCCN.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None;
			this.cboCCN.RowSubDividerColor = System.Drawing.Color.DarkGray;
			this.cboCCN.Size = ((System.Drawing.Size)(resources.GetObject("cboCCN.Size")));
			this.cboCCN.TabIndex = ((int)(resources.GetObject("cboCCN.TabIndex")));
			this.cboCCN.Text = resources.GetString("cboCCN.Text");
			this.cboCCN.Visible = ((bool)(resources.GetObject("cboCCN.Visible")));
			this.cboCCN.Enter += new System.EventHandler(this.OnEnterControl);
			this.cboCCN.Leave += new System.EventHandler(this.OnLeaveControl);
			this.cboCCN.PropBag = "<?xml version=\"1.0\"?><Blob><Styles type=\"C1.Win.C1List.Design.ContextWrapper\"><Da" +
				"ta>Group{AlignVert:Center;Border:None,,0, 0, 0, 0;BackColor:ControlDark;}Style2{" +
				"}Style5{}Style4{}Style7{}Style6{}EvenRow{BackColor:Aqua;}Selected{ForeColor:High" +
				"lightText;BackColor:Highlight;}Style3{}Inactive{ForeColor:InactiveCaptionText;Ba" +
				"ckColor:InactiveCaption;}Footer{}Caption{AlignHorz:Center;}Normal{BackColor:Wind" +
				"ow;}HighlightRow{ForeColor:HighlightText;BackColor:Highlight;}Style1{}OddRow{}Re" +
				"cordSelector{AlignImage:Center;}Heading{Wrap:True;BackColor:Control;Border:Raise" +
				"d,,1, 1, 1, 1;ForeColor:ControlText;AlignVert:Center;}Style8{}Style10{}Style11{}" +
				"Style9{AlignHorz:Near;}</Data></Styles><Splits><C1.Win.C1List.ListBoxView AllowC" +
				"olSelect=\"False\" Name=\"\" CaptionHeight=\"17\" ColumnCaptionHeight=\"17\" ColumnFoote" +
				"rHeight=\"17\" VerticalScrollGroup=\"1\" HorizontalScrollGroup=\"1\"><ClientRect>0, 0," +
				" 118, 158</ClientRect><VScrollBar><Width>16</Width></VScrollBar><HScrollBar><Hei" +
				"ght>16</Height></HScrollBar><CaptionStyle parent=\"Style2\" me=\"Style9\" /><EvenRow" +
				"Style parent=\"EvenRow\" me=\"Style7\" /><FooterStyle parent=\"Footer\" me=\"Style3\" />" +
				"<GroupStyle parent=\"Group\" me=\"Style11\" /><HeadingStyle parent=\"Heading\" me=\"Sty" +
				"le2\" /><HighLightRowStyle parent=\"HighlightRow\" me=\"Style6\" /><InactiveStyle par" +
				"ent=\"Inactive\" me=\"Style4\" /><OddRowStyle parent=\"OddRow\" me=\"Style8\" /><RecordS" +
				"electorStyle parent=\"RecordSelector\" me=\"Style10\" /><SelectedStyle parent=\"Selec" +
				"ted\" me=\"Style5\" /><Style parent=\"Normal\" me=\"Style1\" /></C1.Win.C1List.ListBoxV" +
				"iew></Splits><NamedStyles><Style parent=\"\" me=\"Normal\" /><Style parent=\"Normal\" " +
				"me=\"Heading\" /><Style parent=\"Heading\" me=\"Footer\" /><Style parent=\"Heading\" me=" +
				"\"Caption\" /><Style parent=\"Heading\" me=\"Inactive\" /><Style parent=\"Normal\" me=\"S" +
				"elected\" /><Style parent=\"Normal\" me=\"HighlightRow\" /><Style parent=\"Normal\" me=" +
				"\"EvenRow\" /><Style parent=\"Normal\" me=\"OddRow\" /><Style parent=\"Heading\" me=\"Rec" +
				"ordSelector\" /><Style parent=\"Caption\" me=\"Group\" /></NamedStyles><vertSplits>1<" +
				"/vertSplits><horzSplits>1</horzSplits><Layout>Modified</Layout><DefaultRecSelWid" +
				"th>17</DefaultRecSelWidth></Blob>";
			// 
			// lblCCN
			// 
			this.lblCCN.AccessibleDescription = resources.GetString("lblCCN.AccessibleDescription");
			this.lblCCN.AccessibleName = resources.GetString("lblCCN.AccessibleName");
			this.lblCCN.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblCCN.Anchor")));
			this.lblCCN.AutoSize = ((bool)(resources.GetObject("lblCCN.AutoSize")));
			this.lblCCN.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblCCN.Dock")));
			this.lblCCN.Enabled = ((bool)(resources.GetObject("lblCCN.Enabled")));
			this.lblCCN.Font = ((System.Drawing.Font)(resources.GetObject("lblCCN.Font")));
			this.lblCCN.ForeColor = System.Drawing.Color.Maroon;
			this.lblCCN.Image = ((System.Drawing.Image)(resources.GetObject("lblCCN.Image")));
			this.lblCCN.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblCCN.ImageAlign")));
			this.lblCCN.ImageIndex = ((int)(resources.GetObject("lblCCN.ImageIndex")));
			this.lblCCN.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblCCN.ImeMode")));
			this.lblCCN.Location = ((System.Drawing.Point)(resources.GetObject("lblCCN.Location")));
			this.lblCCN.Name = "lblCCN";
			this.lblCCN.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblCCN.RightToLeft")));
			this.lblCCN.Size = ((System.Drawing.Size)(resources.GetObject("lblCCN.Size")));
			this.lblCCN.TabIndex = ((int)(resources.GetObject("lblCCN.TabIndex")));
			this.lblCCN.Text = resources.GetString("lblCCN.Text");
			this.lblCCN.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblCCN.TextAlign")));
			this.lblCCN.Visible = ((bool)(resources.GetObject("lblCCN.Visible")));
			// 
			// txtWorkCenter
			// 
			this.txtWorkCenter.AccessibleDescription = resources.GetString("txtWorkCenter.AccessibleDescription");
			this.txtWorkCenter.AccessibleName = resources.GetString("txtWorkCenter.AccessibleName");
			this.txtWorkCenter.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("txtWorkCenter.Anchor")));
			this.txtWorkCenter.AutoSize = ((bool)(resources.GetObject("txtWorkCenter.AutoSize")));
			this.txtWorkCenter.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("txtWorkCenter.BackgroundImage")));
			this.txtWorkCenter.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("txtWorkCenter.Dock")));
			this.txtWorkCenter.Enabled = ((bool)(resources.GetObject("txtWorkCenter.Enabled")));
			this.txtWorkCenter.Font = ((System.Drawing.Font)(resources.GetObject("txtWorkCenter.Font")));
			this.txtWorkCenter.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("txtWorkCenter.ImeMode")));
			this.txtWorkCenter.Location = ((System.Drawing.Point)(resources.GetObject("txtWorkCenter.Location")));
			this.txtWorkCenter.MaxLength = ((int)(resources.GetObject("txtWorkCenter.MaxLength")));
			this.txtWorkCenter.Multiline = ((bool)(resources.GetObject("txtWorkCenter.Multiline")));
			this.txtWorkCenter.Name = "txtWorkCenter";
			this.txtWorkCenter.PasswordChar = ((char)(resources.GetObject("txtWorkCenter.PasswordChar")));
			this.txtWorkCenter.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("txtWorkCenter.RightToLeft")));
			this.txtWorkCenter.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("txtWorkCenter.ScrollBars")));
			this.txtWorkCenter.Size = ((System.Drawing.Size)(resources.GetObject("txtWorkCenter.Size")));
			this.txtWorkCenter.TabIndex = ((int)(resources.GetObject("txtWorkCenter.TabIndex")));
			this.txtWorkCenter.Text = resources.GetString("txtWorkCenter.Text");
			this.txtWorkCenter.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("txtWorkCenter.TextAlign")));
			this.txtWorkCenter.Visible = ((bool)(resources.GetObject("txtWorkCenter.Visible")));
			this.txtWorkCenter.WordWrap = ((bool)(resources.GetObject("txtWorkCenter.WordWrap")));
			this.txtWorkCenter.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtWorkCenter_KeyDown);
			this.txtWorkCenter.Validating += new System.ComponentModel.CancelEventHandler(this.txtWorkCenter_Validating);
			// 
			// btnWorkCenterSearch
			// 
			this.btnWorkCenterSearch.AccessibleDescription = resources.GetString("btnWorkCenterSearch.AccessibleDescription");
			this.btnWorkCenterSearch.AccessibleName = resources.GetString("btnWorkCenterSearch.AccessibleName");
			this.btnWorkCenterSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnWorkCenterSearch.Anchor")));
			this.btnWorkCenterSearch.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnWorkCenterSearch.BackgroundImage")));
			this.btnWorkCenterSearch.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnWorkCenterSearch.Dock")));
			this.btnWorkCenterSearch.Enabled = ((bool)(resources.GetObject("btnWorkCenterSearch.Enabled")));
			this.btnWorkCenterSearch.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnWorkCenterSearch.FlatStyle")));
			this.btnWorkCenterSearch.Font = ((System.Drawing.Font)(resources.GetObject("btnWorkCenterSearch.Font")));
			this.btnWorkCenterSearch.Image = ((System.Drawing.Image)(resources.GetObject("btnWorkCenterSearch.Image")));
			this.btnWorkCenterSearch.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnWorkCenterSearch.ImageAlign")));
			this.btnWorkCenterSearch.ImageIndex = ((int)(resources.GetObject("btnWorkCenterSearch.ImageIndex")));
			this.btnWorkCenterSearch.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnWorkCenterSearch.ImeMode")));
			this.btnWorkCenterSearch.Location = ((System.Drawing.Point)(resources.GetObject("btnWorkCenterSearch.Location")));
			this.btnWorkCenterSearch.Name = "btnWorkCenterSearch";
			this.btnWorkCenterSearch.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnWorkCenterSearch.RightToLeft")));
			this.btnWorkCenterSearch.Size = ((System.Drawing.Size)(resources.GetObject("btnWorkCenterSearch.Size")));
			this.btnWorkCenterSearch.TabIndex = ((int)(resources.GetObject("btnWorkCenterSearch.TabIndex")));
			this.btnWorkCenterSearch.Text = resources.GetString("btnWorkCenterSearch.Text");
			this.btnWorkCenterSearch.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnWorkCenterSearch.TextAlign")));
			this.btnWorkCenterSearch.Visible = ((bool)(resources.GetObject("btnWorkCenterSearch.Visible")));
			this.btnWorkCenterSearch.Click += new System.EventHandler(this.btnWorkCenter_Click);
			// 
			// grpShift
			// 
			this.grpShift.AccessibleDescription = resources.GetString("grpShift.AccessibleDescription");
			this.grpShift.AccessibleName = resources.GetString("grpShift.AccessibleName");
			this.grpShift.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("grpShift.Anchor")));
			this.grpShift.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("grpShift.BackgroundImage")));
			this.grpShift.Controls.Add(this.dgrdShift);
			this.grpShift.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("grpShift.Dock")));
			this.grpShift.Enabled = ((bool)(resources.GetObject("grpShift.Enabled")));
			this.grpShift.Font = ((System.Drawing.Font)(resources.GetObject("grpShift.Font")));
			this.grpShift.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("grpShift.ImeMode")));
			this.grpShift.Location = ((System.Drawing.Point)(resources.GetObject("grpShift.Location")));
			this.grpShift.Name = "grpShift";
			this.grpShift.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("grpShift.RightToLeft")));
			this.grpShift.Size = ((System.Drawing.Size)(resources.GetObject("grpShift.Size")));
			this.grpShift.TabIndex = ((int)(resources.GetObject("grpShift.TabIndex")));
			this.grpShift.TabStop = false;
			this.grpShift.Text = resources.GetString("grpShift.Text");
			this.grpShift.Visible = ((bool)(resources.GetObject("grpShift.Visible")));
			// 
			// dgrdShift
			// 
			this.dgrdShift.AccessibleDescription = resources.GetString("dgrdShift.AccessibleDescription");
			this.dgrdShift.AccessibleName = resources.GetString("dgrdShift.AccessibleName");
			this.dgrdShift.AllowAddNew = ((bool)(resources.GetObject("dgrdShift.AllowAddNew")));
			this.dgrdShift.AllowArrows = ((bool)(resources.GetObject("dgrdShift.AllowArrows")));
			this.dgrdShift.AllowColMove = ((bool)(resources.GetObject("dgrdShift.AllowColMove")));
			this.dgrdShift.AllowColSelect = ((bool)(resources.GetObject("dgrdShift.AllowColSelect")));
			this.dgrdShift.AllowDelete = ((bool)(resources.GetObject("dgrdShift.AllowDelete")));
			this.dgrdShift.AllowDrag = ((bool)(resources.GetObject("dgrdShift.AllowDrag")));
			this.dgrdShift.AllowFilter = ((bool)(resources.GetObject("dgrdShift.AllowFilter")));
			this.dgrdShift.AllowHorizontalSplit = ((bool)(resources.GetObject("dgrdShift.AllowHorizontalSplit")));
			this.dgrdShift.AllowRowSelect = ((bool)(resources.GetObject("dgrdShift.AllowRowSelect")));
			this.dgrdShift.AllowSort = ((bool)(resources.GetObject("dgrdShift.AllowSort")));
			this.dgrdShift.AllowUpdate = ((bool)(resources.GetObject("dgrdShift.AllowUpdate")));
			this.dgrdShift.AllowUpdateOnBlur = ((bool)(resources.GetObject("dgrdShift.AllowUpdateOnBlur")));
			this.dgrdShift.AllowVerticalSplit = ((bool)(resources.GetObject("dgrdShift.AllowVerticalSplit")));
			this.dgrdShift.AlternatingRows = ((bool)(resources.GetObject("dgrdShift.AlternatingRows")));
			this.dgrdShift.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("dgrdShift.Anchor")));
			this.dgrdShift.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("dgrdShift.BackgroundImage")));
			this.dgrdShift.BorderStyle = ((System.Windows.Forms.BorderStyle)(resources.GetObject("dgrdShift.BorderStyle")));
			this.dgrdShift.Caption = resources.GetString("dgrdShift.Caption");
			this.dgrdShift.CaptionHeight = ((int)(resources.GetObject("dgrdShift.CaptionHeight")));
			this.dgrdShift.CellTipsDelay = ((int)(resources.GetObject("dgrdShift.CellTipsDelay")));
			this.dgrdShift.CellTipsWidth = ((int)(resources.GetObject("dgrdShift.CellTipsWidth")));
			this.dgrdShift.ChildGrid = ((C1.Win.C1TrueDBGrid.C1TrueDBGrid)(resources.GetObject("dgrdShift.ChildGrid")));
			this.dgrdShift.CollapseColor = ((System.Drawing.Color)(resources.GetObject("dgrdShift.CollapseColor")));
			this.dgrdShift.ColumnFooters = ((bool)(resources.GetObject("dgrdShift.ColumnFooters")));
			this.dgrdShift.ColumnHeaders = ((bool)(resources.GetObject("dgrdShift.ColumnHeaders")));
			this.dgrdShift.DefColWidth = ((int)(resources.GetObject("dgrdShift.DefColWidth")));
			this.dgrdShift.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("dgrdShift.Dock")));
			this.dgrdShift.EditDropDown = ((bool)(resources.GetObject("dgrdShift.EditDropDown")));
			this.dgrdShift.EmptyRows = ((bool)(resources.GetObject("dgrdShift.EmptyRows")));
			this.dgrdShift.Enabled = ((bool)(resources.GetObject("dgrdShift.Enabled")));
			this.dgrdShift.ExpandColor = ((System.Drawing.Color)(resources.GetObject("dgrdShift.ExpandColor")));
			this.dgrdShift.ExposeCellMode = ((C1.Win.C1TrueDBGrid.ExposeCellModeEnum)(resources.GetObject("dgrdShift.ExposeCellMode")));
			this.dgrdShift.ExtendRightColumn = ((bool)(resources.GetObject("dgrdShift.ExtendRightColumn")));
			this.dgrdShift.FetchRowStyles = ((bool)(resources.GetObject("dgrdShift.FetchRowStyles")));
			this.dgrdShift.FilterBar = ((bool)(resources.GetObject("dgrdShift.FilterBar")));
			this.dgrdShift.FlatStyle = ((C1.Win.C1TrueDBGrid.FlatModeEnum)(resources.GetObject("dgrdShift.FlatStyle")));
			this.dgrdShift.Font = ((System.Drawing.Font)(resources.GetObject("dgrdShift.Font")));
			this.dgrdShift.GroupByAreaVisible = ((bool)(resources.GetObject("dgrdShift.GroupByAreaVisible")));
			this.dgrdShift.GroupByCaption = resources.GetString("dgrdShift.GroupByCaption");
			this.dgrdShift.Images.Add(((System.Drawing.Image)(resources.GetObject("resource1"))));
			this.dgrdShift.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("dgrdShift.ImeMode")));
			this.dgrdShift.LinesPerRow = ((int)(resources.GetObject("dgrdShift.LinesPerRow")));
			this.dgrdShift.Location = ((System.Drawing.Point)(resources.GetObject("dgrdShift.Location")));
			this.dgrdShift.MarqueeStyle = C1.Win.C1TrueDBGrid.MarqueeEnum.DottedCellBorder;
			this.dgrdShift.Name = "dgrdShift";
			this.dgrdShift.PictureAddnewRow = ((System.Drawing.Image)(resources.GetObject("dgrdShift.PictureAddnewRow")));
			this.dgrdShift.PictureCurrentRow = ((System.Drawing.Image)(resources.GetObject("dgrdShift.PictureCurrentRow")));
			this.dgrdShift.PictureFilterBar = ((System.Drawing.Image)(resources.GetObject("dgrdShift.PictureFilterBar")));
			this.dgrdShift.PictureFooterRow = ((System.Drawing.Image)(resources.GetObject("dgrdShift.PictureFooterRow")));
			this.dgrdShift.PictureHeaderRow = ((System.Drawing.Image)(resources.GetObject("dgrdShift.PictureHeaderRow")));
			this.dgrdShift.PictureModifiedRow = ((System.Drawing.Image)(resources.GetObject("dgrdShift.PictureModifiedRow")));
			this.dgrdShift.PictureStandardRow = ((System.Drawing.Image)(resources.GetObject("dgrdShift.PictureStandardRow")));
			this.dgrdShift.PreviewInfo.AllowSizing = ((bool)(resources.GetObject("dgrdShift.PreviewInfo.AllowSizing")));
			this.dgrdShift.PreviewInfo.Caption = resources.GetString("dgrdShift.PreviewInfo.Caption");
			this.dgrdShift.PreviewInfo.Location = ((System.Drawing.Point)(resources.GetObject("dgrdShift.PreviewInfo.Location")));
			this.dgrdShift.PreviewInfo.Size = ((System.Drawing.Size)(resources.GetObject("dgrdShift.PreviewInfo.Size")));
			this.dgrdShift.PreviewInfo.ToolBars = ((bool)(resources.GetObject("dgrdShift.PreviewInfo.ToolBars")));
			this.dgrdShift.PreviewInfo.UIStrings.Content = ((string[])(resources.GetObject("dgrdShift.PreviewInfo.UIStrings.Content")));
			this.dgrdShift.PreviewInfo.ZoomFactor = ((System.Double)(resources.GetObject("dgrdShift.PreviewInfo.ZoomFactor")));
			this.dgrdShift.PrintInfo.MaxRowHeight = ((int)(resources.GetObject("dgrdShift.PrintInfo.MaxRowHeight")));
			this.dgrdShift.PrintInfo.OwnerDrawPageFooter = ((bool)(resources.GetObject("dgrdShift.PrintInfo.OwnerDrawPageFooter")));
			this.dgrdShift.PrintInfo.OwnerDrawPageHeader = ((bool)(resources.GetObject("dgrdShift.PrintInfo.OwnerDrawPageHeader")));
			this.dgrdShift.PrintInfo.PageFooter = resources.GetString("dgrdShift.PrintInfo.PageFooter");
			this.dgrdShift.PrintInfo.PageFooterHeight = ((int)(resources.GetObject("dgrdShift.PrintInfo.PageFooterHeight")));
			this.dgrdShift.PrintInfo.PageHeader = resources.GetString("dgrdShift.PrintInfo.PageHeader");
			this.dgrdShift.PrintInfo.PageHeaderHeight = ((int)(resources.GetObject("dgrdShift.PrintInfo.PageHeaderHeight")));
			this.dgrdShift.PrintInfo.PrintHorizontalSplits = ((bool)(resources.GetObject("dgrdShift.PrintInfo.PrintHorizontalSplits")));
			this.dgrdShift.PrintInfo.ProgressCaption = resources.GetString("dgrdShift.PrintInfo.ProgressCaption");
			this.dgrdShift.PrintInfo.RepeatColumnFooters = ((bool)(resources.GetObject("dgrdShift.PrintInfo.RepeatColumnFooters")));
			this.dgrdShift.PrintInfo.RepeatColumnHeaders = ((bool)(resources.GetObject("dgrdShift.PrintInfo.RepeatColumnHeaders")));
			this.dgrdShift.PrintInfo.RepeatGridHeader = ((bool)(resources.GetObject("dgrdShift.PrintInfo.RepeatGridHeader")));
			this.dgrdShift.PrintInfo.RepeatSplitHeaders = ((bool)(resources.GetObject("dgrdShift.PrintInfo.RepeatSplitHeaders")));
			this.dgrdShift.PrintInfo.ShowOptionsDialog = ((bool)(resources.GetObject("dgrdShift.PrintInfo.ShowOptionsDialog")));
			this.dgrdShift.PrintInfo.ShowProgressForm = ((bool)(resources.GetObject("dgrdShift.PrintInfo.ShowProgressForm")));
			this.dgrdShift.PrintInfo.ShowSelection = ((bool)(resources.GetObject("dgrdShift.PrintInfo.ShowSelection")));
			this.dgrdShift.PrintInfo.UseGridColors = ((bool)(resources.GetObject("dgrdShift.PrintInfo.UseGridColors")));
			this.dgrdShift.RecordSelectors = ((bool)(resources.GetObject("dgrdShift.RecordSelectors")));
			this.dgrdShift.RecordSelectorWidth = ((int)(resources.GetObject("dgrdShift.RecordSelectorWidth")));
			this.dgrdShift.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("dgrdShift.RightToLeft")));
			this.dgrdShift.RowDivider.Color = ((System.Drawing.Color)(resources.GetObject("resource.Color1")));
			this.dgrdShift.RowDivider.Style = ((C1.Win.C1TrueDBGrid.LineStyleEnum)(resources.GetObject("resource.Style1")));
			this.dgrdShift.RowHeight = ((int)(resources.GetObject("dgrdShift.RowHeight")));
			this.dgrdShift.RowSubDividerColor = ((System.Drawing.Color)(resources.GetObject("dgrdShift.RowSubDividerColor")));
			this.dgrdShift.ScrollTips = ((bool)(resources.GetObject("dgrdShift.ScrollTips")));
			this.dgrdShift.ScrollTrack = ((bool)(resources.GetObject("dgrdShift.ScrollTrack")));
			this.dgrdShift.Size = ((System.Drawing.Size)(resources.GetObject("dgrdShift.Size")));
			this.dgrdShift.SpringMode = ((bool)(resources.GetObject("dgrdShift.SpringMode")));
			this.dgrdShift.TabAcrossSplits = ((bool)(resources.GetObject("dgrdShift.TabAcrossSplits")));
			this.dgrdShift.TabAction = C1.Win.C1TrueDBGrid.TabActionEnum.ColumnNavigation;
			this.dgrdShift.TabIndex = ((int)(resources.GetObject("dgrdShift.TabIndex")));
			this.dgrdShift.Text = resources.GetString("dgrdShift.Text");
			this.dgrdShift.ViewCaptionWidth = ((int)(resources.GetObject("dgrdShift.ViewCaptionWidth")));
			this.dgrdShift.ViewColumnWidth = ((int)(resources.GetObject("dgrdShift.ViewColumnWidth")));
			this.dgrdShift.Visible = ((bool)(resources.GetObject("dgrdShift.Visible")));
			this.dgrdShift.WrapCellPointer = ((bool)(resources.GetObject("dgrdShift.WrapCellPointer")));
			this.dgrdShift.BeforeColEdit += new C1.Win.C1TrueDBGrid.BeforeColEditEventHandler(this.dgrdShift_BeforeColEdit);
			this.dgrdShift.AfterColUpdate += new C1.Win.C1TrueDBGrid.ColEventHandler(this.dgrdShift_AfterColUpdate);
			this.dgrdShift.BeforeColUpdate += new C1.Win.C1TrueDBGrid.BeforeColUpdateEventHandler(this.dgrdShift_BeforeColUpdate);
			this.dgrdShift.Leave += new System.EventHandler(this.dgrdShift_Leave);
			this.dgrdShift.PropBag = resources.GetString("dgrdShift.PropBag");
			// 
			// numDecValue
			// 
			this.numDecValue.AcceptsEscape = ((bool)(resources.GetObject("numDecValue.AcceptsEscape")));
			this.numDecValue.AccessibleDescription = resources.GetString("numDecValue.AccessibleDescription");
			this.numDecValue.AccessibleName = resources.GetString("numDecValue.AccessibleName");
			this.numDecValue.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("numDecValue.Anchor")));
			this.numDecValue.AutoSize = ((bool)(resources.GetObject("numDecValue.AutoSize")));
			this.numDecValue.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("numDecValue.BackgroundImage")));
			this.numDecValue.BorderStyle = ((System.Windows.Forms.BorderStyle)(resources.GetObject("numDecValue.BorderStyle")));
			// 
			// numDecValue.Calculator
			// 
			this.numDecValue.Calculator.AccessibleDescription = resources.GetString("numDecValue.Calculator.AccessibleDescription");
			this.numDecValue.Calculator.AccessibleName = resources.GetString("numDecValue.Calculator.AccessibleName");
			this.numDecValue.Calculator.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("numDecValue.Calculator.BackgroundImage")));
			this.numDecValue.Calculator.ButtonFlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("numDecValue.Calculator.ButtonFlatStyle")));
			this.numDecValue.Calculator.DisplayFormat = resources.GetString("numDecValue.Calculator.DisplayFormat");
			this.numDecValue.Calculator.Font = ((System.Drawing.Font)(resources.GetObject("numDecValue.Calculator.Font")));
			this.numDecValue.Calculator.FormatOnClose = ((bool)(resources.GetObject("numDecValue.Calculator.FormatOnClose")));
			this.numDecValue.Calculator.StoredFormat = resources.GetString("numDecValue.Calculator.StoredFormat");
			this.numDecValue.Calculator.UIStrings.Content = ((string[])(resources.GetObject("numDecValue.Calculator.UIStrings.Content")));
			this.numDecValue.CaseSensitive = ((bool)(resources.GetObject("numDecValue.CaseSensitive")));
			this.numDecValue.Culture = ((int)(resources.GetObject("numDecValue.Culture")));
			this.numDecValue.CustomFormat = resources.GetString("numDecValue.CustomFormat");
			this.numDecValue.DataType = ((System.Type)(resources.GetObject("numDecValue.DataType")));
			this.numDecValue.DisplayFormat.CustomFormat = resources.GetString("numDecValue.DisplayFormat.CustomFormat");
			this.numDecValue.DisplayFormat.FormatType = ((C1.Win.C1Input.FormatTypeEnum)(resources.GetObject("numDecValue.DisplayFormat.FormatType")));
			this.numDecValue.DisplayFormat.Inherit = ((C1.Win.C1Input.FormatInfoInheritFlags)(resources.GetObject("numDecValue.DisplayFormat.Inherit")));
			this.numDecValue.DisplayFormat.NullText = resources.GetString("numDecValue.DisplayFormat.NullText");
			this.numDecValue.DisplayFormat.TrimEnd = ((bool)(resources.GetObject("numDecValue.DisplayFormat.TrimEnd")));
			this.numDecValue.DisplayFormat.TrimStart = ((bool)(resources.GetObject("numDecValue.DisplayFormat.TrimStart")));
			this.numDecValue.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("numDecValue.Dock")));
			this.numDecValue.DropDownFormAlign = ((C1.Win.C1Input.DropDownFormAlignmentEnum)(resources.GetObject("numDecValue.DropDownFormAlign")));
			this.numDecValue.EditFormat.CustomFormat = resources.GetString("numDecValue.EditFormat.CustomFormat");
			this.numDecValue.EditFormat.FormatType = ((C1.Win.C1Input.FormatTypeEnum)(resources.GetObject("numDecValue.EditFormat.FormatType")));
			this.numDecValue.EditFormat.Inherit = ((C1.Win.C1Input.FormatInfoInheritFlags)(resources.GetObject("numDecValue.EditFormat.Inherit")));
			this.numDecValue.EditFormat.NullText = resources.GetString("numDecValue.EditFormat.NullText");
			this.numDecValue.EditFormat.TrimEnd = ((bool)(resources.GetObject("numDecValue.EditFormat.TrimEnd")));
			this.numDecValue.EditFormat.TrimStart = ((bool)(resources.GetObject("numDecValue.EditFormat.TrimStart")));
			this.numDecValue.EditMask = resources.GetString("numDecValue.EditMask");
			this.numDecValue.EmptyAsNull = ((bool)(resources.GetObject("numDecValue.EmptyAsNull")));
			this.numDecValue.Enabled = ((bool)(resources.GetObject("numDecValue.Enabled")));
			this.numDecValue.ErrorInfo.BeepOnError = ((bool)(resources.GetObject("numDecValue.ErrorInfo.BeepOnError")));
			this.numDecValue.ErrorInfo.ErrorMessage = resources.GetString("numDecValue.ErrorInfo.ErrorMessage");
			this.numDecValue.ErrorInfo.ErrorMessageCaption = resources.GetString("numDecValue.ErrorInfo.ErrorMessageCaption");
			this.numDecValue.ErrorInfo.ShowErrorMessage = ((bool)(resources.GetObject("numDecValue.ErrorInfo.ShowErrorMessage")));
			this.numDecValue.ErrorInfo.ValueOnError = ((object)(resources.GetObject("numDecValue.ErrorInfo.ValueOnError")));
			this.numDecValue.Font = ((System.Drawing.Font)(resources.GetObject("numDecValue.Font")));
			this.numDecValue.FormatType = ((C1.Win.C1Input.FormatTypeEnum)(resources.GetObject("numDecValue.FormatType")));
			this.numDecValue.GapHeight = ((int)(resources.GetObject("numDecValue.GapHeight")));
			this.numDecValue.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("numDecValue.ImeMode")));
			this.numDecValue.Increment = ((object)(resources.GetObject("numDecValue.Increment")));
			this.numDecValue.InitialSelection = ((C1.Win.C1Input.InitialSelectionEnum)(resources.GetObject("numDecValue.InitialSelection")));
			this.numDecValue.Location = ((System.Drawing.Point)(resources.GetObject("numDecValue.Location")));
			this.numDecValue.MaskInfo.AutoTabWhenFilled = ((bool)(resources.GetObject("numDecValue.MaskInfo.AutoTabWhenFilled")));
			this.numDecValue.MaskInfo.CaseSensitive = ((bool)(resources.GetObject("numDecValue.MaskInfo.CaseSensitive")));
			this.numDecValue.MaskInfo.CopyWithLiterals = ((bool)(resources.GetObject("numDecValue.MaskInfo.CopyWithLiterals")));
			this.numDecValue.MaskInfo.EditMask = resources.GetString("numDecValue.MaskInfo.EditMask");
			this.numDecValue.MaskInfo.EmptyAsNull = ((bool)(resources.GetObject("numDecValue.MaskInfo.EmptyAsNull")));
			this.numDecValue.MaskInfo.ErrorMessage = resources.GetString("numDecValue.MaskInfo.ErrorMessage");
			this.numDecValue.MaskInfo.Inherit = ((C1.Win.C1Input.MaskInfoInheritFlags)(resources.GetObject("numDecValue.MaskInfo.Inherit")));
			this.numDecValue.MaskInfo.PromptChar = ((char)(resources.GetObject("numDecValue.MaskInfo.PromptChar")));
			this.numDecValue.MaskInfo.ShowLiterals = ((C1.Win.C1Input.ShowLiteralsEnum)(resources.GetObject("numDecValue.MaskInfo.ShowLiterals")));
			this.numDecValue.MaskInfo.StoredEmptyChar = ((char)(resources.GetObject("numDecValue.MaskInfo.StoredEmptyChar")));
			this.numDecValue.MaxLength = ((int)(resources.GetObject("numDecValue.MaxLength")));
			this.numDecValue.Name = "numDecValue";
			this.numDecValue.NullText = resources.GetString("numDecValue.NullText");
			this.numDecValue.NumericInputKeys = ((C1.Win.C1Input.NumericInputKeyFlags)((((C1.Win.C1Input.NumericInputKeyFlags.F9 | C1.Win.C1Input.NumericInputKeyFlags.Plus) 
				| C1.Win.C1Input.NumericInputKeyFlags.Decimal) 
				| C1.Win.C1Input.NumericInputKeyFlags.X)));
			this.numDecValue.ParseInfo.CaseSensitive = ((bool)(resources.GetObject("numDecValue.ParseInfo.CaseSensitive")));
			this.numDecValue.ParseInfo.CustomFormat = resources.GetString("numDecValue.ParseInfo.CustomFormat");
			this.numDecValue.ParseInfo.EmptyAsNull = ((bool)(resources.GetObject("numDecValue.ParseInfo.EmptyAsNull")));
			this.numDecValue.ParseInfo.ErrorMessage = resources.GetString("numDecValue.ParseInfo.ErrorMessage");
			this.numDecValue.ParseInfo.FormatType = ((C1.Win.C1Input.FormatTypeEnum)(resources.GetObject("numDecValue.ParseInfo.FormatType")));
			this.numDecValue.ParseInfo.Inherit = ((C1.Win.C1Input.ParseInfoInheritFlags)(resources.GetObject("numDecValue.ParseInfo.Inherit")));
			this.numDecValue.ParseInfo.NullText = resources.GetString("numDecValue.ParseInfo.NullText");
			this.numDecValue.ParseInfo.NumberStyle = ((C1.Win.C1Input.NumberStyleFlags)(resources.GetObject("numDecValue.ParseInfo.NumberStyle")));
			this.numDecValue.ParseInfo.TrimEnd = ((bool)(resources.GetObject("numDecValue.ParseInfo.TrimEnd")));
			this.numDecValue.ParseInfo.TrimStart = ((bool)(resources.GetObject("numDecValue.ParseInfo.TrimStart")));
			this.numDecValue.PasswordChar = ((char)(resources.GetObject("numDecValue.PasswordChar")));
			this.numDecValue.PostValidation.CaseSensitive = ((bool)(resources.GetObject("numDecValue.PostValidation.CaseSensitive")));
			this.numDecValue.PostValidation.ErrorMessage = resources.GetString("numDecValue.PostValidation.ErrorMessage");
			this.numDecValue.PostValidation.Inherit = ((C1.Win.C1Input.PostValidationInheritFlags)(resources.GetObject("numDecValue.PostValidation.Inherit")));
			this.numDecValue.PostValidation.Validation = ((C1.Win.C1Input.PostValidationTypeEnum)(resources.GetObject("numDecValue.PostValidation.Validation")));
			this.numDecValue.PostValidation.Values = ((System.Array)(resources.GetObject("numDecValue.PostValidation.Values")));
			this.numDecValue.PostValidation.ValuesExcluded = ((System.Array)(resources.GetObject("numDecValue.PostValidation.ValuesExcluded")));
			this.numDecValue.PreValidation.CaseSensitive = ((bool)(resources.GetObject("numDecValue.PreValidation.CaseSensitive")));
			this.numDecValue.PreValidation.ErrorMessage = resources.GetString("numDecValue.PreValidation.ErrorMessage");
			this.numDecValue.PreValidation.Inherit = ((C1.Win.C1Input.PreValidationInheritFlags)(resources.GetObject("numDecValue.PreValidation.Inherit")));
			this.numDecValue.PreValidation.ItemSeparator = resources.GetString("numDecValue.PreValidation.ItemSeparator");
			this.numDecValue.PreValidation.PatternString = resources.GetString("numDecValue.PreValidation.PatternString");
			this.numDecValue.PreValidation.RegexOptions = ((C1.Win.C1Input.RegexOptionFlags)(resources.GetObject("numDecValue.PreValidation.RegexOptions")));
			this.numDecValue.PreValidation.TrimEnd = ((bool)(resources.GetObject("numDecValue.PreValidation.TrimEnd")));
			this.numDecValue.PreValidation.TrimStart = ((bool)(resources.GetObject("numDecValue.PreValidation.TrimStart")));
			this.numDecValue.PreValidation.Validation = ((C1.Win.C1Input.PreValidationTypeEnum)(resources.GetObject("numDecValue.PreValidation.Validation")));
			this.numDecValue.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("numDecValue.RightToLeft")));
			this.numDecValue.ShowFocusRectangle = ((bool)(resources.GetObject("numDecValue.ShowFocusRectangle")));
			this.numDecValue.Size = ((System.Drawing.Size)(resources.GetObject("numDecValue.Size")));
			this.numDecValue.TabIndex = ((int)(resources.GetObject("numDecValue.TabIndex")));
			this.numDecValue.Tag = ((object)(resources.GetObject("numDecValue.Tag")));
			this.numDecValue.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("numDecValue.TextAlign")));
			this.numDecValue.TrimEnd = ((bool)(resources.GetObject("numDecValue.TrimEnd")));
			this.numDecValue.TrimStart = ((bool)(resources.GetObject("numDecValue.TrimStart")));
			this.numDecValue.UserCultureOverride = ((bool)(resources.GetObject("numDecValue.UserCultureOverride")));
			this.numDecValue.Value = ((object)(resources.GetObject("numDecValue.Value")));
			this.numDecValue.VerticalAlign = ((C1.Win.C1Input.VerticalAlignEnum)(resources.GetObject("numDecValue.VerticalAlign")));
			this.numDecValue.Visible = ((bool)(resources.GetObject("numDecValue.Visible")));
			this.numDecValue.VisibleButtons = ((C1.Win.C1Input.DropDownControlButtonFlags)(resources.GetObject("numDecValue.VisibleButtons")));
			// 
			// txtWCName
			// 
			this.txtWCName.AccessibleDescription = resources.GetString("txtWCName.AccessibleDescription");
			this.txtWCName.AccessibleName = resources.GetString("txtWCName.AccessibleName");
			this.txtWCName.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("txtWCName.Anchor")));
			this.txtWCName.AutoSize = ((bool)(resources.GetObject("txtWCName.AutoSize")));
			this.txtWCName.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("txtWCName.BackgroundImage")));
			this.txtWCName.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("txtWCName.Dock")));
			this.txtWCName.Enabled = ((bool)(resources.GetObject("txtWCName.Enabled")));
			this.txtWCName.Font = ((System.Drawing.Font)(resources.GetObject("txtWCName.Font")));
			this.txtWCName.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("txtWCName.ImeMode")));
			this.txtWCName.Location = ((System.Drawing.Point)(resources.GetObject("txtWCName.Location")));
			this.txtWCName.MaxLength = ((int)(resources.GetObject("txtWCName.MaxLength")));
			this.txtWCName.Multiline = ((bool)(resources.GetObject("txtWCName.Multiline")));
			this.txtWCName.Name = "txtWCName";
			this.txtWCName.PasswordChar = ((char)(resources.GetObject("txtWCName.PasswordChar")));
			this.txtWCName.ReadOnly = true;
			this.txtWCName.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("txtWCName.RightToLeft")));
			this.txtWCName.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("txtWCName.ScrollBars")));
			this.txtWCName.Size = ((System.Drawing.Size)(resources.GetObject("txtWCName.Size")));
			this.txtWCName.TabIndex = ((int)(resources.GetObject("txtWCName.TabIndex")));
			this.txtWCName.Text = resources.GetString("txtWCName.Text");
			this.txtWCName.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("txtWCName.TextAlign")));
			this.txtWCName.Visible = ((bool)(resources.GetObject("txtWCName.Visible")));
			this.txtWCName.WordWrap = ((bool)(resources.GetObject("txtWCName.WordWrap")));
			// 
			// dtmDate
			// 
			this.dtmDate.AcceptsEscape = ((bool)(resources.GetObject("dtmDate.AcceptsEscape")));
			this.dtmDate.AccessibleDescription = resources.GetString("dtmDate.AccessibleDescription");
			this.dtmDate.AccessibleName = resources.GetString("dtmDate.AccessibleName");
			this.dtmDate.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("dtmDate.Anchor")));
			this.dtmDate.AutoSize = ((bool)(resources.GetObject("dtmDate.AutoSize")));
			this.dtmDate.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("dtmDate.BackgroundImage")));
			this.dtmDate.BorderStyle = ((System.Windows.Forms.BorderStyle)(resources.GetObject("dtmDate.BorderStyle")));
			// 
			// dtmDate.Calendar
			// 
			this.dtmDate.Calendar.AccessibleDescription = resources.GetString("dtmDate.Calendar.AccessibleDescription");
			this.dtmDate.Calendar.AccessibleName = resources.GetString("dtmDate.Calendar.AccessibleName");
			this.dtmDate.Calendar.AnnuallyBoldedDates = ((System.DateTime[])(resources.GetObject("dtmDate.Calendar.AnnuallyBoldedDates")));
			this.dtmDate.Calendar.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("dtmDate.Calendar.BackgroundImage")));
			this.dtmDate.Calendar.BoldedDates = ((System.DateTime[])(resources.GetObject("dtmDate.Calendar.BoldedDates")));
			this.dtmDate.Calendar.CalendarDimensions = ((System.Drawing.Size)(resources.GetObject("dtmDate.Calendar.CalendarDimensions")));
			this.dtmDate.Calendar.Enabled = ((bool)(resources.GetObject("dtmDate.Calendar.Enabled")));
			this.dtmDate.Calendar.FirstDayOfWeek = ((System.Windows.Forms.Day)(resources.GetObject("dtmDate.Calendar.FirstDayOfWeek")));
			this.dtmDate.Calendar.Font = ((System.Drawing.Font)(resources.GetObject("dtmDate.Calendar.Font")));
			this.dtmDate.Calendar.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("dtmDate.Calendar.ImeMode")));
			this.dtmDate.Calendar.MonthlyBoldedDates = ((System.DateTime[])(resources.GetObject("dtmDate.Calendar.MonthlyBoldedDates")));
			this.dtmDate.Calendar.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("dtmDate.Calendar.RightToLeft")));
			this.dtmDate.Calendar.ShowClearButton = ((bool)(resources.GetObject("dtmDate.Calendar.ShowClearButton")));
			this.dtmDate.Calendar.ShowTodayButton = ((bool)(resources.GetObject("dtmDate.Calendar.ShowTodayButton")));
			this.dtmDate.Calendar.ShowWeekNumbers = ((bool)(resources.GetObject("dtmDate.Calendar.ShowWeekNumbers")));
			this.dtmDate.CaseSensitive = ((bool)(resources.GetObject("dtmDate.CaseSensitive")));
			this.dtmDate.Culture = ((int)(resources.GetObject("dtmDate.Culture")));
			this.dtmDate.CurrentTimeZone = ((bool)(resources.GetObject("dtmDate.CurrentTimeZone")));
			this.dtmDate.CustomFormat = resources.GetString("dtmDate.CustomFormat");
			this.dtmDate.DaylightTimeAdjustment = ((C1.Win.C1Input.DaylightTimeAdjustmentEnum)(resources.GetObject("dtmDate.DaylightTimeAdjustment")));
			this.dtmDate.DisplayFormat.CustomFormat = resources.GetString("dtmDate.DisplayFormat.CustomFormat");
			this.dtmDate.DisplayFormat.FormatType = ((C1.Win.C1Input.FormatTypeEnum)(resources.GetObject("dtmDate.DisplayFormat.FormatType")));
			this.dtmDate.DisplayFormat.Inherit = ((C1.Win.C1Input.FormatInfoInheritFlags)(resources.GetObject("dtmDate.DisplayFormat.Inherit")));
			this.dtmDate.DisplayFormat.NullText = resources.GetString("dtmDate.DisplayFormat.NullText");
			this.dtmDate.DisplayFormat.TrimEnd = ((bool)(resources.GetObject("dtmDate.DisplayFormat.TrimEnd")));
			this.dtmDate.DisplayFormat.TrimStart = ((bool)(resources.GetObject("dtmDate.DisplayFormat.TrimStart")));
			this.dtmDate.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("dtmDate.Dock")));
			this.dtmDate.DropDownFormAlign = ((C1.Win.C1Input.DropDownFormAlignmentEnum)(resources.GetObject("dtmDate.DropDownFormAlign")));
			this.dtmDate.EditFormat.CustomFormat = resources.GetString("dtmDate.EditFormat.CustomFormat");
			this.dtmDate.EditFormat.FormatType = ((C1.Win.C1Input.FormatTypeEnum)(resources.GetObject("dtmDate.EditFormat.FormatType")));
			this.dtmDate.EditFormat.Inherit = ((C1.Win.C1Input.FormatInfoInheritFlags)(resources.GetObject("dtmDate.EditFormat.Inherit")));
			this.dtmDate.EditFormat.NullText = resources.GetString("dtmDate.EditFormat.NullText");
			this.dtmDate.EditFormat.TrimEnd = ((bool)(resources.GetObject("dtmDate.EditFormat.TrimEnd")));
			this.dtmDate.EditFormat.TrimStart = ((bool)(resources.GetObject("dtmDate.EditFormat.TrimStart")));
			this.dtmDate.EditMask = resources.GetString("dtmDate.EditMask");
			this.dtmDate.EmptyAsNull = ((bool)(resources.GetObject("dtmDate.EmptyAsNull")));
			this.dtmDate.Enabled = ((bool)(resources.GetObject("dtmDate.Enabled")));
			this.dtmDate.ErrorInfo.BeepOnError = ((bool)(resources.GetObject("dtmDate.ErrorInfo.BeepOnError")));
			this.dtmDate.ErrorInfo.ErrorMessage = resources.GetString("dtmDate.ErrorInfo.ErrorMessage");
			this.dtmDate.ErrorInfo.ErrorMessageCaption = resources.GetString("dtmDate.ErrorInfo.ErrorMessageCaption");
			this.dtmDate.ErrorInfo.ShowErrorMessage = ((bool)(resources.GetObject("dtmDate.ErrorInfo.ShowErrorMessage")));
			this.dtmDate.ErrorInfo.ValueOnError = ((object)(resources.GetObject("dtmDate.ErrorInfo.ValueOnError")));
			this.dtmDate.Font = ((System.Drawing.Font)(resources.GetObject("dtmDate.Font")));
			this.dtmDate.FormatType = ((C1.Win.C1Input.FormatTypeEnum)(resources.GetObject("dtmDate.FormatType")));
			this.dtmDate.GapHeight = ((int)(resources.GetObject("dtmDate.GapHeight")));
			this.dtmDate.GMTOffset = ((System.TimeSpan)(resources.GetObject("dtmDate.GMTOffset")));
			this.dtmDate.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("dtmDate.ImeMode")));
			this.dtmDate.InitialSelection = ((C1.Win.C1Input.InitialSelectionEnum)(resources.GetObject("dtmDate.InitialSelection")));
			this.dtmDate.Location = ((System.Drawing.Point)(resources.GetObject("dtmDate.Location")));
			this.dtmDate.MaskInfo.AutoTabWhenFilled = ((bool)(resources.GetObject("dtmDate.MaskInfo.AutoTabWhenFilled")));
			this.dtmDate.MaskInfo.CaseSensitive = ((bool)(resources.GetObject("dtmDate.MaskInfo.CaseSensitive")));
			this.dtmDate.MaskInfo.CopyWithLiterals = ((bool)(resources.GetObject("dtmDate.MaskInfo.CopyWithLiterals")));
			this.dtmDate.MaskInfo.EditMask = resources.GetString("dtmDate.MaskInfo.EditMask");
			this.dtmDate.MaskInfo.EmptyAsNull = ((bool)(resources.GetObject("dtmDate.MaskInfo.EmptyAsNull")));
			this.dtmDate.MaskInfo.ErrorMessage = resources.GetString("dtmDate.MaskInfo.ErrorMessage");
			this.dtmDate.MaskInfo.Inherit = ((C1.Win.C1Input.MaskInfoInheritFlags)(resources.GetObject("dtmDate.MaskInfo.Inherit")));
			this.dtmDate.MaskInfo.PromptChar = ((char)(resources.GetObject("dtmDate.MaskInfo.PromptChar")));
			this.dtmDate.MaskInfo.ShowLiterals = ((C1.Win.C1Input.ShowLiteralsEnum)(resources.GetObject("dtmDate.MaskInfo.ShowLiterals")));
			this.dtmDate.MaskInfo.StoredEmptyChar = ((char)(resources.GetObject("dtmDate.MaskInfo.StoredEmptyChar")));
			this.dtmDate.MaxLength = ((int)(resources.GetObject("dtmDate.MaxLength")));
			this.dtmDate.Name = "dtmDate";
			this.dtmDate.NullText = resources.GetString("dtmDate.NullText");
			this.dtmDate.ParseInfo.CaseSensitive = ((bool)(resources.GetObject("dtmDate.ParseInfo.CaseSensitive")));
			this.dtmDate.ParseInfo.CustomFormat = resources.GetString("dtmDate.ParseInfo.CustomFormat");
			this.dtmDate.ParseInfo.DateTimeStyle = ((C1.Win.C1Input.DateTimeStyleFlags)(resources.GetObject("dtmDate.ParseInfo.DateTimeStyle")));
			this.dtmDate.ParseInfo.EmptyAsNull = ((bool)(resources.GetObject("dtmDate.ParseInfo.EmptyAsNull")));
			this.dtmDate.ParseInfo.ErrorMessage = resources.GetString("dtmDate.ParseInfo.ErrorMessage");
			this.dtmDate.ParseInfo.FormatType = ((C1.Win.C1Input.FormatTypeEnum)(resources.GetObject("dtmDate.ParseInfo.FormatType")));
			this.dtmDate.ParseInfo.Inherit = ((C1.Win.C1Input.ParseInfoInheritFlags)(resources.GetObject("dtmDate.ParseInfo.Inherit")));
			this.dtmDate.ParseInfo.NullText = resources.GetString("dtmDate.ParseInfo.NullText");
			this.dtmDate.ParseInfo.TrimEnd = ((bool)(resources.GetObject("dtmDate.ParseInfo.TrimEnd")));
			this.dtmDate.ParseInfo.TrimStart = ((bool)(resources.GetObject("dtmDate.ParseInfo.TrimStart")));
			this.dtmDate.PasswordChar = ((char)(resources.GetObject("dtmDate.PasswordChar")));
			this.dtmDate.PostValidation.CaseSensitive = ((bool)(resources.GetObject("dtmDate.PostValidation.CaseSensitive")));
			this.dtmDate.PostValidation.ErrorMessage = resources.GetString("dtmDate.PostValidation.ErrorMessage");
			this.dtmDate.PostValidation.Inherit = ((C1.Win.C1Input.PostValidationInheritFlags)(resources.GetObject("dtmDate.PostValidation.Inherit")));
			this.dtmDate.PostValidation.Validation = ((C1.Win.C1Input.PostValidationTypeEnum)(resources.GetObject("dtmDate.PostValidation.Validation")));
			this.dtmDate.PostValidation.Values = ((System.Array)(resources.GetObject("dtmDate.PostValidation.Values")));
			this.dtmDate.PostValidation.ValuesExcluded = ((System.Array)(resources.GetObject("dtmDate.PostValidation.ValuesExcluded")));
			this.dtmDate.PreValidation.CaseSensitive = ((bool)(resources.GetObject("dtmDate.PreValidation.CaseSensitive")));
			this.dtmDate.PreValidation.ErrorMessage = resources.GetString("dtmDate.PreValidation.ErrorMessage");
			this.dtmDate.PreValidation.Inherit = ((C1.Win.C1Input.PreValidationInheritFlags)(resources.GetObject("dtmDate.PreValidation.Inherit")));
			this.dtmDate.PreValidation.ItemSeparator = resources.GetString("dtmDate.PreValidation.ItemSeparator");
			this.dtmDate.PreValidation.PatternString = resources.GetString("dtmDate.PreValidation.PatternString");
			this.dtmDate.PreValidation.RegexOptions = ((C1.Win.C1Input.RegexOptionFlags)(resources.GetObject("dtmDate.PreValidation.RegexOptions")));
			this.dtmDate.PreValidation.TrimEnd = ((bool)(resources.GetObject("dtmDate.PreValidation.TrimEnd")));
			this.dtmDate.PreValidation.TrimStart = ((bool)(resources.GetObject("dtmDate.PreValidation.TrimStart")));
			this.dtmDate.PreValidation.Validation = ((C1.Win.C1Input.PreValidationTypeEnum)(resources.GetObject("dtmDate.PreValidation.Validation")));
			this.dtmDate.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("dtmDate.RightToLeft")));
			this.dtmDate.ShowFocusRectangle = ((bool)(resources.GetObject("dtmDate.ShowFocusRectangle")));
			this.dtmDate.Size = ((System.Drawing.Size)(resources.GetObject("dtmDate.Size")));
			this.dtmDate.TabIndex = ((int)(resources.GetObject("dtmDate.TabIndex")));
			this.dtmDate.Tag = ((object)(resources.GetObject("dtmDate.Tag")));
			this.dtmDate.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("dtmDate.TextAlign")));
			this.dtmDate.TrimEnd = ((bool)(resources.GetObject("dtmDate.TrimEnd")));
			this.dtmDate.TrimStart = ((bool)(resources.GetObject("dtmDate.TrimStart")));
			this.dtmDate.UserCultureOverride = ((bool)(resources.GetObject("dtmDate.UserCultureOverride")));
			this.dtmDate.Value = ((object)(resources.GetObject("dtmDate.Value")));
			this.dtmDate.VerticalAlign = ((C1.Win.C1Input.VerticalAlignEnum)(resources.GetObject("dtmDate.VerticalAlign")));
			this.dtmDate.Visible = ((bool)(resources.GetObject("dtmDate.Visible")));
			this.dtmDate.VisibleButtons = ((C1.Win.C1Input.DropDownControlButtonFlags)(resources.GetObject("dtmDate.VisibleButtons")));
			// 
			// numIntValue
			// 
			this.numIntValue.AcceptsEscape = ((bool)(resources.GetObject("numIntValue.AcceptsEscape")));
			this.numIntValue.AccessibleDescription = resources.GetString("numIntValue.AccessibleDescription");
			this.numIntValue.AccessibleName = resources.GetString("numIntValue.AccessibleName");
			this.numIntValue.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("numIntValue.Anchor")));
			this.numIntValue.AutoSize = ((bool)(resources.GetObject("numIntValue.AutoSize")));
			this.numIntValue.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("numIntValue.BackgroundImage")));
			this.numIntValue.BorderStyle = ((System.Windows.Forms.BorderStyle)(resources.GetObject("numIntValue.BorderStyle")));
			// 
			// numIntValue.Calculator
			// 
			this.numIntValue.Calculator.AccessibleDescription = resources.GetString("numIntValue.Calculator.AccessibleDescription");
			this.numIntValue.Calculator.AccessibleName = resources.GetString("numIntValue.Calculator.AccessibleName");
			this.numIntValue.Calculator.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("numIntValue.Calculator.BackgroundImage")));
			this.numIntValue.Calculator.ButtonFlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("numIntValue.Calculator.ButtonFlatStyle")));
			this.numIntValue.Calculator.DisplayFormat = resources.GetString("numIntValue.Calculator.DisplayFormat");
			this.numIntValue.Calculator.Font = ((System.Drawing.Font)(resources.GetObject("numIntValue.Calculator.Font")));
			this.numIntValue.Calculator.FormatOnClose = ((bool)(resources.GetObject("numIntValue.Calculator.FormatOnClose")));
			this.numIntValue.Calculator.StoredFormat = resources.GetString("numIntValue.Calculator.StoredFormat");
			this.numIntValue.Calculator.UIStrings.Content = ((string[])(resources.GetObject("numIntValue.Calculator.UIStrings.Content")));
			this.numIntValue.CaseSensitive = ((bool)(resources.GetObject("numIntValue.CaseSensitive")));
			this.numIntValue.Culture = ((int)(resources.GetObject("numIntValue.Culture")));
			this.numIntValue.CustomFormat = resources.GetString("numIntValue.CustomFormat");
			this.numIntValue.DataType = ((System.Type)(resources.GetObject("numIntValue.DataType")));
			this.numIntValue.DisplayFormat.CustomFormat = resources.GetString("numIntValue.DisplayFormat.CustomFormat");
			this.numIntValue.DisplayFormat.FormatType = ((C1.Win.C1Input.FormatTypeEnum)(resources.GetObject("numIntValue.DisplayFormat.FormatType")));
			this.numIntValue.DisplayFormat.Inherit = ((C1.Win.C1Input.FormatInfoInheritFlags)(resources.GetObject("numIntValue.DisplayFormat.Inherit")));
			this.numIntValue.DisplayFormat.NullText = resources.GetString("numIntValue.DisplayFormat.NullText");
			this.numIntValue.DisplayFormat.TrimEnd = ((bool)(resources.GetObject("numIntValue.DisplayFormat.TrimEnd")));
			this.numIntValue.DisplayFormat.TrimStart = ((bool)(resources.GetObject("numIntValue.DisplayFormat.TrimStart")));
			this.numIntValue.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("numIntValue.Dock")));
			this.numIntValue.DropDownFormAlign = ((C1.Win.C1Input.DropDownFormAlignmentEnum)(resources.GetObject("numIntValue.DropDownFormAlign")));
			this.numIntValue.EditFormat.CustomFormat = resources.GetString("numIntValue.EditFormat.CustomFormat");
			this.numIntValue.EditFormat.FormatType = ((C1.Win.C1Input.FormatTypeEnum)(resources.GetObject("numIntValue.EditFormat.FormatType")));
			this.numIntValue.EditFormat.Inherit = ((C1.Win.C1Input.FormatInfoInheritFlags)(resources.GetObject("numIntValue.EditFormat.Inherit")));
			this.numIntValue.EditFormat.NullText = resources.GetString("numIntValue.EditFormat.NullText");
			this.numIntValue.EditFormat.TrimEnd = ((bool)(resources.GetObject("numIntValue.EditFormat.TrimEnd")));
			this.numIntValue.EditFormat.TrimStart = ((bool)(resources.GetObject("numIntValue.EditFormat.TrimStart")));
			this.numIntValue.EditMask = resources.GetString("numIntValue.EditMask");
			this.numIntValue.EmptyAsNull = ((bool)(resources.GetObject("numIntValue.EmptyAsNull")));
			this.numIntValue.Enabled = ((bool)(resources.GetObject("numIntValue.Enabled")));
			this.numIntValue.ErrorInfo.BeepOnError = ((bool)(resources.GetObject("numIntValue.ErrorInfo.BeepOnError")));
			this.numIntValue.ErrorInfo.ErrorMessage = resources.GetString("numIntValue.ErrorInfo.ErrorMessage");
			this.numIntValue.ErrorInfo.ErrorMessageCaption = resources.GetString("numIntValue.ErrorInfo.ErrorMessageCaption");
			this.numIntValue.ErrorInfo.ShowErrorMessage = ((bool)(resources.GetObject("numIntValue.ErrorInfo.ShowErrorMessage")));
			this.numIntValue.ErrorInfo.ValueOnError = ((object)(resources.GetObject("numIntValue.ErrorInfo.ValueOnError")));
			this.numIntValue.Font = ((System.Drawing.Font)(resources.GetObject("numIntValue.Font")));
			this.numIntValue.FormatType = ((C1.Win.C1Input.FormatTypeEnum)(resources.GetObject("numIntValue.FormatType")));
			this.numIntValue.GapHeight = ((int)(resources.GetObject("numIntValue.GapHeight")));
			this.numIntValue.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("numIntValue.ImeMode")));
			this.numIntValue.Increment = ((object)(resources.GetObject("numIntValue.Increment")));
			this.numIntValue.InitialSelection = ((C1.Win.C1Input.InitialSelectionEnum)(resources.GetObject("numIntValue.InitialSelection")));
			this.numIntValue.Location = ((System.Drawing.Point)(resources.GetObject("numIntValue.Location")));
			this.numIntValue.MaskInfo.AutoTabWhenFilled = ((bool)(resources.GetObject("numIntValue.MaskInfo.AutoTabWhenFilled")));
			this.numIntValue.MaskInfo.CaseSensitive = ((bool)(resources.GetObject("numIntValue.MaskInfo.CaseSensitive")));
			this.numIntValue.MaskInfo.CopyWithLiterals = ((bool)(resources.GetObject("numIntValue.MaskInfo.CopyWithLiterals")));
			this.numIntValue.MaskInfo.EditMask = resources.GetString("numIntValue.MaskInfo.EditMask");
			this.numIntValue.MaskInfo.EmptyAsNull = ((bool)(resources.GetObject("numIntValue.MaskInfo.EmptyAsNull")));
			this.numIntValue.MaskInfo.ErrorMessage = resources.GetString("numIntValue.MaskInfo.ErrorMessage");
			this.numIntValue.MaskInfo.Inherit = ((C1.Win.C1Input.MaskInfoInheritFlags)(resources.GetObject("numIntValue.MaskInfo.Inherit")));
			this.numIntValue.MaskInfo.PromptChar = ((char)(resources.GetObject("numIntValue.MaskInfo.PromptChar")));
			this.numIntValue.MaskInfo.ShowLiterals = ((C1.Win.C1Input.ShowLiteralsEnum)(resources.GetObject("numIntValue.MaskInfo.ShowLiterals")));
			this.numIntValue.MaskInfo.StoredEmptyChar = ((char)(resources.GetObject("numIntValue.MaskInfo.StoredEmptyChar")));
			this.numIntValue.MaxLength = ((int)(resources.GetObject("numIntValue.MaxLength")));
			this.numIntValue.Name = "numIntValue";
			this.numIntValue.NullText = resources.GetString("numIntValue.NullText");
			this.numIntValue.NumericInputKeys = ((C1.Win.C1Input.NumericInputKeyFlags)((((C1.Win.C1Input.NumericInputKeyFlags.F9 | C1.Win.C1Input.NumericInputKeyFlags.Plus) 
				| C1.Win.C1Input.NumericInputKeyFlags.Decimal) 
				| C1.Win.C1Input.NumericInputKeyFlags.X)));
			this.numIntValue.ParseInfo.CaseSensitive = ((bool)(resources.GetObject("numIntValue.ParseInfo.CaseSensitive")));
			this.numIntValue.ParseInfo.CustomFormat = resources.GetString("numIntValue.ParseInfo.CustomFormat");
			this.numIntValue.ParseInfo.EmptyAsNull = ((bool)(resources.GetObject("numIntValue.ParseInfo.EmptyAsNull")));
			this.numIntValue.ParseInfo.ErrorMessage = resources.GetString("numIntValue.ParseInfo.ErrorMessage");
			this.numIntValue.ParseInfo.FormatType = ((C1.Win.C1Input.FormatTypeEnum)(resources.GetObject("numIntValue.ParseInfo.FormatType")));
			this.numIntValue.ParseInfo.Inherit = ((C1.Win.C1Input.ParseInfoInheritFlags)(resources.GetObject("numIntValue.ParseInfo.Inherit")));
			this.numIntValue.ParseInfo.NullText = resources.GetString("numIntValue.ParseInfo.NullText");
			this.numIntValue.ParseInfo.NumberStyle = ((C1.Win.C1Input.NumberStyleFlags)(resources.GetObject("numIntValue.ParseInfo.NumberStyle")));
			this.numIntValue.ParseInfo.TrimEnd = ((bool)(resources.GetObject("numIntValue.ParseInfo.TrimEnd")));
			this.numIntValue.ParseInfo.TrimStart = ((bool)(resources.GetObject("numIntValue.ParseInfo.TrimStart")));
			this.numIntValue.PasswordChar = ((char)(resources.GetObject("numIntValue.PasswordChar")));
			this.numIntValue.PostValidation.CaseSensitive = ((bool)(resources.GetObject("numIntValue.PostValidation.CaseSensitive")));
			this.numIntValue.PostValidation.ErrorMessage = resources.GetString("numIntValue.PostValidation.ErrorMessage");
			this.numIntValue.PostValidation.Inherit = ((C1.Win.C1Input.PostValidationInheritFlags)(resources.GetObject("numIntValue.PostValidation.Inherit")));
			this.numIntValue.PostValidation.Validation = ((C1.Win.C1Input.PostValidationTypeEnum)(resources.GetObject("numIntValue.PostValidation.Validation")));
			this.numIntValue.PostValidation.Values = ((System.Array)(resources.GetObject("numIntValue.PostValidation.Values")));
			this.numIntValue.PostValidation.ValuesExcluded = ((System.Array)(resources.GetObject("numIntValue.PostValidation.ValuesExcluded")));
			this.numIntValue.PreValidation.CaseSensitive = ((bool)(resources.GetObject("numIntValue.PreValidation.CaseSensitive")));
			this.numIntValue.PreValidation.ErrorMessage = resources.GetString("numIntValue.PreValidation.ErrorMessage");
			this.numIntValue.PreValidation.Inherit = ((C1.Win.C1Input.PreValidationInheritFlags)(resources.GetObject("numIntValue.PreValidation.Inherit")));
			this.numIntValue.PreValidation.ItemSeparator = resources.GetString("numIntValue.PreValidation.ItemSeparator");
			this.numIntValue.PreValidation.PatternString = resources.GetString("numIntValue.PreValidation.PatternString");
			this.numIntValue.PreValidation.RegexOptions = ((C1.Win.C1Input.RegexOptionFlags)(resources.GetObject("numIntValue.PreValidation.RegexOptions")));
			this.numIntValue.PreValidation.TrimEnd = ((bool)(resources.GetObject("numIntValue.PreValidation.TrimEnd")));
			this.numIntValue.PreValidation.TrimStart = ((bool)(resources.GetObject("numIntValue.PreValidation.TrimStart")));
			this.numIntValue.PreValidation.Validation = ((C1.Win.C1Input.PreValidationTypeEnum)(resources.GetObject("numIntValue.PreValidation.Validation")));
			this.numIntValue.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("numIntValue.RightToLeft")));
			this.numIntValue.ShowFocusRectangle = ((bool)(resources.GetObject("numIntValue.ShowFocusRectangle")));
			this.numIntValue.Size = ((System.Drawing.Size)(resources.GetObject("numIntValue.Size")));
			this.numIntValue.TabIndex = ((int)(resources.GetObject("numIntValue.TabIndex")));
			this.numIntValue.Tag = ((object)(resources.GetObject("numIntValue.Tag")));
			this.numIntValue.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("numIntValue.TextAlign")));
			this.numIntValue.TrimEnd = ((bool)(resources.GetObject("numIntValue.TrimEnd")));
			this.numIntValue.TrimStart = ((bool)(resources.GetObject("numIntValue.TrimStart")));
			this.numIntValue.UserCultureOverride = ((bool)(resources.GetObject("numIntValue.UserCultureOverride")));
			this.numIntValue.Value = ((object)(resources.GetObject("numIntValue.Value")));
			this.numIntValue.VerticalAlign = ((C1.Win.C1Input.VerticalAlignEnum)(resources.GetObject("numIntValue.VerticalAlign")));
			this.numIntValue.Visible = ((bool)(resources.GetObject("numIntValue.Visible")));
			this.numIntValue.VisibleButtons = ((C1.Win.C1Input.DropDownControlButtonFlags)(resources.GetObject("numIntValue.VisibleButtons")));
			// 
			// txtCycle
			// 
			this.txtCycle.AccessibleDescription = resources.GetString("txtCycle.AccessibleDescription");
			this.txtCycle.AccessibleName = resources.GetString("txtCycle.AccessibleName");
			this.txtCycle.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("txtCycle.Anchor")));
			this.txtCycle.AutoSize = ((bool)(resources.GetObject("txtCycle.AutoSize")));
			this.txtCycle.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("txtCycle.BackgroundImage")));
			this.txtCycle.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("txtCycle.Dock")));
			this.txtCycle.Enabled = ((bool)(resources.GetObject("txtCycle.Enabled")));
			this.txtCycle.Font = ((System.Drawing.Font)(resources.GetObject("txtCycle.Font")));
			this.txtCycle.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("txtCycle.ImeMode")));
			this.txtCycle.Location = ((System.Drawing.Point)(resources.GetObject("txtCycle.Location")));
			this.txtCycle.MaxLength = ((int)(resources.GetObject("txtCycle.MaxLength")));
			this.txtCycle.Multiline = ((bool)(resources.GetObject("txtCycle.Multiline")));
			this.txtCycle.Name = "txtCycle";
			this.txtCycle.PasswordChar = ((char)(resources.GetObject("txtCycle.PasswordChar")));
			this.txtCycle.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("txtCycle.RightToLeft")));
			this.txtCycle.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("txtCycle.ScrollBars")));
			this.txtCycle.Size = ((System.Drawing.Size)(resources.GetObject("txtCycle.Size")));
			this.txtCycle.TabIndex = ((int)(resources.GetObject("txtCycle.TabIndex")));
			this.txtCycle.Text = resources.GetString("txtCycle.Text");
			this.txtCycle.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("txtCycle.TextAlign")));
			this.txtCycle.Visible = ((bool)(resources.GetObject("txtCycle.Visible")));
			this.txtCycle.WordWrap = ((bool)(resources.GetObject("txtCycle.WordWrap")));
			this.txtCycle.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCycle_KeyDown);
			this.txtCycle.Validating += new System.ComponentModel.CancelEventHandler(this.txtCycle_Validating);
			// 
			// btnCycle
			// 
			this.btnCycle.AccessibleDescription = resources.GetString("btnCycle.AccessibleDescription");
			this.btnCycle.AccessibleName = resources.GetString("btnCycle.AccessibleName");
			this.btnCycle.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnCycle.Anchor")));
			this.btnCycle.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCycle.BackgroundImage")));
			this.btnCycle.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnCycle.Dock")));
			this.btnCycle.Enabled = ((bool)(resources.GetObject("btnCycle.Enabled")));
			this.btnCycle.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnCycle.FlatStyle")));
			this.btnCycle.Font = ((System.Drawing.Font)(resources.GetObject("btnCycle.Font")));
			this.btnCycle.Image = ((System.Drawing.Image)(resources.GetObject("btnCycle.Image")));
			this.btnCycle.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnCycle.ImageAlign")));
			this.btnCycle.ImageIndex = ((int)(resources.GetObject("btnCycle.ImageIndex")));
			this.btnCycle.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnCycle.ImeMode")));
			this.btnCycle.Location = ((System.Drawing.Point)(resources.GetObject("btnCycle.Location")));
			this.btnCycle.Name = "btnCycle";
			this.btnCycle.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnCycle.RightToLeft")));
			this.btnCycle.Size = ((System.Drawing.Size)(resources.GetObject("btnCycle.Size")));
			this.btnCycle.TabIndex = ((int)(resources.GetObject("btnCycle.TabIndex")));
			this.btnCycle.Text = resources.GetString("btnCycle.Text");
			this.btnCycle.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnCycle.TextAlign")));
			this.btnCycle.Visible = ((bool)(resources.GetObject("btnCycle.Visible")));
			this.btnCycle.Click += new System.EventHandler(this.btnCycle_Click);
			// 
			// lblCycle
			// 
			this.lblCycle.AccessibleDescription = resources.GetString("lblCycle.AccessibleDescription");
			this.lblCycle.AccessibleName = resources.GetString("lblCycle.AccessibleName");
			this.lblCycle.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblCycle.Anchor")));
			this.lblCycle.AutoSize = ((bool)(resources.GetObject("lblCycle.AutoSize")));
			this.lblCycle.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblCycle.Dock")));
			this.lblCycle.Enabled = ((bool)(resources.GetObject("lblCycle.Enabled")));
			this.lblCycle.Font = ((System.Drawing.Font)(resources.GetObject("lblCycle.Font")));
			this.lblCycle.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblCycle.Image = ((System.Drawing.Image)(resources.GetObject("lblCycle.Image")));
			this.lblCycle.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblCycle.ImageAlign")));
			this.lblCycle.ImageIndex = ((int)(resources.GetObject("lblCycle.ImageIndex")));
			this.lblCycle.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblCycle.ImeMode")));
			this.lblCycle.Location = ((System.Drawing.Point)(resources.GetObject("lblCycle.Location")));
			this.lblCycle.Name = "lblCycle";
			this.lblCycle.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblCycle.RightToLeft")));
			this.lblCycle.Size = ((System.Drawing.Size)(resources.GetObject("lblCycle.Size")));
			this.lblCycle.TabIndex = ((int)(resources.GetObject("lblCycle.TabIndex")));
			this.lblCycle.Text = resources.GetString("lblCycle.Text");
			this.lblCycle.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblCycle.TextAlign")));
			this.lblCycle.Visible = ((bool)(resources.GetObject("lblCycle.Visible")));
			// 
			// btnAdjust
			// 
			this.btnAdjust.AccessibleDescription = resources.GetString("btnAdjust.AccessibleDescription");
			this.btnAdjust.AccessibleName = resources.GetString("btnAdjust.AccessibleName");
			this.btnAdjust.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnAdjust.Anchor")));
			this.btnAdjust.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnAdjust.BackgroundImage")));
			this.btnAdjust.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnAdjust.Dock")));
			this.btnAdjust.Enabled = ((bool)(resources.GetObject("btnAdjust.Enabled")));
			this.btnAdjust.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnAdjust.FlatStyle")));
			this.btnAdjust.Font = ((System.Drawing.Font)(resources.GetObject("btnAdjust.Font")));
			this.btnAdjust.Image = ((System.Drawing.Image)(resources.GetObject("btnAdjust.Image")));
			this.btnAdjust.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnAdjust.ImageAlign")));
			this.btnAdjust.ImageIndex = ((int)(resources.GetObject("btnAdjust.ImageIndex")));
			this.btnAdjust.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnAdjust.ImeMode")));
			this.btnAdjust.Location = ((System.Drawing.Point)(resources.GetObject("btnAdjust.Location")));
			this.btnAdjust.Name = "btnAdjust";
			this.btnAdjust.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnAdjust.RightToLeft")));
			this.btnAdjust.Size = ((System.Drawing.Size)(resources.GetObject("btnAdjust.Size")));
			this.btnAdjust.TabIndex = ((int)(resources.GetObject("btnAdjust.TabIndex")));
			this.btnAdjust.Text = resources.GetString("btnAdjust.Text");
			this.btnAdjust.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnAdjust.TextAlign")));
			this.btnAdjust.Visible = ((bool)(resources.GetObject("btnAdjust.Visible")));
			this.btnAdjust.Click += new System.EventHandler(this.btnAdjust_Click);
			// 
			// lblActive
			// 
			this.lblActive.AccessibleDescription = resources.GetString("lblActive.AccessibleDescription");
			this.lblActive.AccessibleName = resources.GetString("lblActive.AccessibleName");
			this.lblActive.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblActive.Anchor")));
			this.lblActive.AutoSize = ((bool)(resources.GetObject("lblActive.AutoSize")));
			this.lblActive.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblActive.Dock")));
			this.lblActive.Enabled = ((bool)(resources.GetObject("lblActive.Enabled")));
			this.lblActive.Font = ((System.Drawing.Font)(resources.GetObject("lblActive.Font")));
			this.lblActive.Image = ((System.Drawing.Image)(resources.GetObject("lblActive.Image")));
			this.lblActive.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblActive.ImageAlign")));
			this.lblActive.ImageIndex = ((int)(resources.GetObject("lblActive.ImageIndex")));
			this.lblActive.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblActive.ImeMode")));
			this.lblActive.Location = ((System.Drawing.Point)(resources.GetObject("lblActive.Location")));
			this.lblActive.Name = "lblActive";
			this.lblActive.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblActive.RightToLeft")));
			this.lblActive.Size = ((System.Drawing.Size)(resources.GetObject("lblActive.Size")));
			this.lblActive.TabIndex = ((int)(resources.GetObject("lblActive.TabIndex")));
			this.lblActive.Text = resources.GetString("lblActive.Text");
			this.lblActive.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblActive.TextAlign")));
			this.lblActive.Visible = ((bool)(resources.GetObject("lblActive.Visible")));
			// 
			// lblTo
			// 
			this.lblTo.AccessibleDescription = resources.GetString("lblTo.AccessibleDescription");
			this.lblTo.AccessibleName = resources.GetString("lblTo.AccessibleName");
			this.lblTo.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblTo.Anchor")));
			this.lblTo.AutoSize = ((bool)(resources.GetObject("lblTo.AutoSize")));
			this.lblTo.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblTo.Dock")));
			this.lblTo.Enabled = ((bool)(resources.GetObject("lblTo.Enabled")));
			this.lblTo.Font = ((System.Drawing.Font)(resources.GetObject("lblTo.Font")));
			this.lblTo.Image = ((System.Drawing.Image)(resources.GetObject("lblTo.Image")));
			this.lblTo.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblTo.ImageAlign")));
			this.lblTo.ImageIndex = ((int)(resources.GetObject("lblTo.ImageIndex")));
			this.lblTo.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblTo.ImeMode")));
			this.lblTo.Location = ((System.Drawing.Point)(resources.GetObject("lblTo.Location")));
			this.lblTo.Name = "lblTo";
			this.lblTo.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblTo.RightToLeft")));
			this.lblTo.Size = ((System.Drawing.Size)(resources.GetObject("lblTo.Size")));
			this.lblTo.TabIndex = ((int)(resources.GetObject("lblTo.TabIndex")));
			this.lblTo.Text = resources.GetString("lblTo.Text");
			this.lblTo.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblTo.TextAlign")));
			this.lblTo.Visible = ((bool)(resources.GetObject("lblTo.Visible")));
			// 
			// txtCycleRange
			// 
			this.txtCycleRange.AccessibleDescription = resources.GetString("txtCycleRange.AccessibleDescription");
			this.txtCycleRange.AccessibleName = resources.GetString("txtCycleRange.AccessibleName");
			this.txtCycleRange.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("txtCycleRange.Anchor")));
			this.txtCycleRange.AutoSize = ((bool)(resources.GetObject("txtCycleRange.AutoSize")));
			this.txtCycleRange.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("txtCycleRange.BackgroundImage")));
			this.txtCycleRange.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("txtCycleRange.Dock")));
			this.txtCycleRange.Enabled = ((bool)(resources.GetObject("txtCycleRange.Enabled")));
			this.txtCycleRange.Font = ((System.Drawing.Font)(resources.GetObject("txtCycleRange.Font")));
			this.txtCycleRange.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("txtCycleRange.ImeMode")));
			this.txtCycleRange.Location = ((System.Drawing.Point)(resources.GetObject("txtCycleRange.Location")));
			this.txtCycleRange.MaxLength = ((int)(resources.GetObject("txtCycleRange.MaxLength")));
			this.txtCycleRange.Multiline = ((bool)(resources.GetObject("txtCycleRange.Multiline")));
			this.txtCycleRange.Name = "txtCycleRange";
			this.txtCycleRange.PasswordChar = ((char)(resources.GetObject("txtCycleRange.PasswordChar")));
			this.txtCycleRange.ReadOnly = true;
			this.txtCycleRange.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("txtCycleRange.RightToLeft")));
			this.txtCycleRange.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("txtCycleRange.ScrollBars")));
			this.txtCycleRange.Size = ((System.Drawing.Size)(resources.GetObject("txtCycleRange.Size")));
			this.txtCycleRange.TabIndex = ((int)(resources.GetObject("txtCycleRange.TabIndex")));
			this.txtCycleRange.Text = resources.GetString("txtCycleRange.Text");
			this.txtCycleRange.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("txtCycleRange.TextAlign")));
			this.txtCycleRange.Visible = ((bool)(resources.GetObject("txtCycleRange.Visible")));
			this.txtCycleRange.WordWrap = ((bool)(resources.GetObject("txtCycleRange.WordWrap")));
			// 
			// btnSeparate
			// 
			this.btnSeparate.AccessibleDescription = resources.GetString("btnSeparate.AccessibleDescription");
			this.btnSeparate.AccessibleName = resources.GetString("btnSeparate.AccessibleName");
			this.btnSeparate.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnSeparate.Anchor")));
			this.btnSeparate.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSeparate.BackgroundImage")));
			this.btnSeparate.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnSeparate.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnSeparate.Dock")));
			this.btnSeparate.Enabled = ((bool)(resources.GetObject("btnSeparate.Enabled")));
			this.btnSeparate.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnSeparate.FlatStyle")));
			this.btnSeparate.Font = ((System.Drawing.Font)(resources.GetObject("btnSeparate.Font")));
			this.btnSeparate.Image = ((System.Drawing.Image)(resources.GetObject("btnSeparate.Image")));
			this.btnSeparate.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnSeparate.ImageAlign")));
			this.btnSeparate.ImageIndex = ((int)(resources.GetObject("btnSeparate.ImageIndex")));
			this.btnSeparate.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnSeparate.ImeMode")));
			this.btnSeparate.Location = ((System.Drawing.Point)(resources.GetObject("btnSeparate.Location")));
			this.btnSeparate.Name = "btnSeparate";
			this.btnSeparate.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnSeparate.RightToLeft")));
			this.btnSeparate.Size = ((System.Drawing.Size)(resources.GetObject("btnSeparate.Size")));
			this.btnSeparate.TabIndex = ((int)(resources.GetObject("btnSeparate.TabIndex")));
			this.btnSeparate.Text = resources.GetString("btnSeparate.Text");
			this.btnSeparate.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnSeparate.TextAlign")));
			this.btnSeparate.Visible = ((bool)(resources.GetObject("btnSeparate.Visible")));
			this.btnSeparate.Click += new System.EventHandler(this.btnSeparate_Click);
			// 
			// WorkCenterCapacity
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
			this.Controls.Add(this.btnSeparate);
			this.Controls.Add(this.txtCycleRange);
			this.Controls.Add(this.lblTo);
			this.Controls.Add(this.lblActive);
			this.Controls.Add(this.btnAdjust);
			this.Controls.Add(this.txtCycle);
			this.Controls.Add(this.btnCycle);
			this.Controls.Add(this.txtWCName);
			this.Controls.Add(this.txtWorkCenter);
			this.Controls.Add(this.grpShift);
			this.Controls.Add(this.btnWorkCenterSearch);
			this.Controls.Add(this.cboCCN);
			this.Controls.Add(this.lblCCN);
			this.Controls.Add(this.lblWorkCenter);
			this.Controls.Add(this.btnSave);
			this.Controls.Add(this.btnHelp);
			this.Controls.Add(this.btnClose);
			this.Controls.Add(this.btnDelete);
			this.Controls.Add(this.dgrdData);
			this.Controls.Add(this.numIntValue);
			this.Controls.Add(this.numDecValue);
			this.Controls.Add(this.dtmDate);
			this.Controls.Add(this.lblCycle);
			this.Enabled = ((bool)(resources.GetObject("$this.Enabled")));
			this.Font = ((System.Drawing.Font)(resources.GetObject("$this.Font")));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("$this.ImeMode")));
			this.KeyPreview = true;
			this.Location = ((System.Drawing.Point)(resources.GetObject("$this.Location")));
			this.MaximizeBox = false;
			this.MaximumSize = ((System.Drawing.Size)(resources.GetObject("$this.MaximumSize")));
			this.MinimumSize = ((System.Drawing.Size)(resources.GetObject("$this.MinimumSize")));
			this.Name = "WorkCenterCapacity";
			this.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("$this.RightToLeft")));
			this.StartPosition = ((System.Windows.Forms.FormStartPosition)(resources.GetObject("$this.StartPosition")));
			this.Text = resources.GetString("$this.Text");
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.WorkCenterCapacity_KeyDown);
			this.Closing += new System.ComponentModel.CancelEventHandler(this.WorkCenterCapacity_Closing);
			this.Load += new System.EventHandler(this.WorkCenterCapacity_Load);
			((System.ComponentModel.ISupportInitialize)(this.dgrdData)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.cboCCN)).EndInit();
			this.grpShift.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dgrdShift)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numDecValue)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dtmDate)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numIntValue)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion
		
		#region Methods
		
		private void GridRowChange()
		{
			try
			{
				if (dtbWCCapacity == null)
				{					
					if (dtbShiftCapacity != null)
					{
						CheckShiftGrid(0);
					}
					return;
				}

				if(dgrdData.Row != dgrdData.Bookmark)
				{					
					CheckShiftGrid(0);
					return;
				}

				if(!dgrdData[dgrdData.Row, PRO_WCCapacityTable.WCCAPACITYID_FLD].Equals(DBNull.Value)
					&& !dgrdData[dgrdData.Row, PRO_WCCapacityTable.WCCAPACITYID_FLD].Equals(string.Empty))
				{					
					CheckShiftGrid(int.Parse(dgrdData[dgrdData.Row, PRO_WCCapacityTable.WCCAPACITYID_FLD].ToString()));
					return;				
				}
				else
				{
					CheckShiftGrid(0);
				}
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message, ex);
			}
		}

		private bool IsOverlap()
		{
			try
			{				
				//if row count is 1 then none of rows if overlap
				if(dtbWCCapacity.Rows.Count == 1)
				{
					return false;
				}
				
				DateTime dtmTempI;
				DateTime dtmBeginDateI;
				DateTime dtmEndDateI;

				for(int i =0; i < dgrdData.RowCount; i++)
				{
					dtmTempI = DateTime.Parse(dgrdData[i, PRO_WCCapacityTable.BEGINDATE_FLD].ToString());
					dtmBeginDateI = new DateTime(dtmTempI.Year, dtmTempI.Month, dtmTempI.Day);
					
					dtmTempI = DateTime.Parse(dgrdData[i, PRO_WCCapacityTable.ENDDATE_FLD].ToString());
					dtmEndDateI = new DateTime(dtmTempI.Year, dtmTempI.Month, dtmTempI.Day);

					for(int j= i+1; j < dgrdData.RowCount; j++)
					{
						DateTime dtmTempJ = DateTime.Parse(dgrdData[j, PRO_WCCapacityTable.BEGINDATE_FLD].ToString());
						DateTime dtmBeginDateJ = new DateTime(dtmTempJ.Year, dtmTempJ.Month, dtmTempJ.Day);
					
						dtmTempJ = DateTime.Parse(dgrdData[j, PRO_WCCapacityTable.ENDDATE_FLD].ToString());
						DateTime dtmEndDateJ = new DateTime(dtmTempJ.Year, dtmTempJ.Month, dtmTempJ.Day);

						if((dtmBeginDateI <= dtmBeginDateJ && dtmBeginDateJ <= dtmEndDateI)
							|| (dtmBeginDateI <= dtmEndDateJ && dtmEndDateJ <= dtmEndDateI)
							|| (dtmBeginDateJ <= dtmBeginDateI && dtmBeginDateI <= dtmEndDateJ)
							|| (dtmBeginDateJ <= dtmEndDateI && dtmEndDateI <= dtmEndDateJ)
						)
						{
							dgrdData.Row = j;
							dgrdData.Col = dgrdData.Columns.IndexOf(dgrdData.Columns[PRO_WCCapacityTable.BEGINDATE_FLD]);
							return true;
						}						
					}
				}

				return false;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message, ex);
			}
		}

		private void ResetFormValues(bool pblnClearWCCapacityGrid)
		{

			try
			{
				if(pblnClearWCCapacityGrid)
				{					
					//Clear WCCapctity grid					
					dtbWCCapacity = BuildDetailTable();
					dgrdData.DataSource = dtbWCCapacity;					
					FormatDataGrid();
					//dgrdData.Splits[0].DisplayColumns[PRO_WCCapacityTable.WCCAPACITYID_FLD].Visible = true;
				}
			
				//Clear shift capacity				
				dtbShiftCapacity = BuildShiftCapcityTable();
				arlDeletedIDs.Clear();
				intWCCapacityIDNeedToDelete = 0;
				CheckShiftGrid(0);
	
				//reset fake id
				intWCCapacityFakeId = 0;

				//Clear Work center information
				txtWorkCenter.Text = string.Empty;
				txtWorkCenter.Tag = ZERO_STRING;
				txtWCName.Text = string.Empty;

				//Clear Cycle's data information
				txtCycleRange.Text = string.Empty;
				dtmBeginCycleDate = DateTime.MinValue;
				dtmEndCycleDate = DateTime.MinValue;
                
				//Clear DCP Cycle info
				txtCycle.Text = string.Empty;

				txtCycle.Tag = ZERO_STRING;
				
				if(enuFormAction != EnumAction.Default)
				{
					txtWorkCenter.Focus();
				}

				LockControl(false);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message, ex);
			}
		}

		private void CalculateCapacity()
		{
			const string METHOD_NAME = THIS +  ".CalculateCapacity()";
			try
			{
//				if(dgrdData[dgrdData.Row, PRO_WCCapacityTable.WCCAPACITYID_FLD].Equals(DBNull.Value)
//				|| dgrdData[dgrdData.Row, PRO_WCCapacityTable.WCTYPE_FLD].Equals(DBNull.Value)
//				|| dgrdData[dgrdData.Row, PRO_WCCapacityTable.WCTYPE_FLD].ToString().Equals(string.Empty)
//				|| dgrdData[dgrdData.Row, PRO_WCCapacityTable.FACTOR_FLD].Equals(DBNull.Value)
//				|| dgrdData[dgrdData.Row, PRO_WCCapacityTable.FACTOR_FLD].ToString().Equals(string.Empty)
				if(dgrdData.Columns[PRO_WCCapacityTable.WCCAPACITYID_FLD].Value.Equals(DBNull.Value)
				|| dgrdData.Columns[PRO_WCCapacityTable.WCTYPE_FLD].Value.Equals(DBNull.Value)
				|| dgrdData.Columns[PRO_WCCapacityTable.WCTYPE_FLD].Value.ToString().Equals(string.Empty)
				|| dgrdData.Columns[PRO_WCCapacityTable.FACTOR_FLD].Value.Equals(DBNull.Value)
				|| dgrdData.Columns[PRO_WCCapacityTable.FACTOR_FLD].Value.ToString().Equals(string.Empty)
				)
				{
					dgrdData.Columns[PRO_WCCapacityTable.CAPACITY_FLD].Value = decimal.Zero;
					return;
				}

				//get selected shifts of WCCapacity row
				string strCheckedShiftIDs = ZERO_STRING;
				DataRow[] arrRows = dtbShiftCapacity.Select(PRO_ShiftCapacityTable.WCCAPACITYID_FLD + "=" + dgrdData[dgrdData.Row, PRO_WCCapacityTable.WCCAPACITYID_FLD].ToString());				
				for(int i = 0; i< arrRows.Length; i++)
				{
					strCheckedShiftIDs += DELIMITER_CHAR + arrRows[i][PRO_ShiftTable.SHIFTID_FLD].ToString();					
				}
				
				//Get working time based-on selected shifts
				decimal decTotalWorkingTime = (decimal)boWCCapacity.GetTotalActualWorkingTime(strCheckedShiftIDs);
				
				//parse values
				decimal decOptionValue = decimal.Zero;				
				decimal decFactor = Convert.ToDecimal(dgrdData.Columns[PRO_WCCapacityTable.FACTOR_FLD].Value);
				int intType = Convert.ToInt32(dgrdData.Columns[PRO_WCCapacityTable.WCTYPE_FLD].Value);
				
				if(intType == (int)WCType.Labor)
				{
					if(!dgrdData.Columns[PRO_WCCapacityTable.CREWSIZE_FLD].Value.Equals(DBNull.Value)
					&& !dgrdData.Columns[PRO_WCCapacityTable.CREWSIZE_FLD].Value.ToString().Equals(ZERO_STRING)) 
					{
						decOptionValue = Convert.ToDecimal(dgrdData.Columns[PRO_WCCapacityTable.CREWSIZE_FLD].Value);
					}
				}
				else
				{
					if(!dgrdData.Columns[PRO_WCCapacityTable.MACHINENO_FLD].Value.Equals(DBNull.Value)
					&& !dgrdData.Columns[PRO_WCCapacityTable.MACHINENO_FLD].Value.ToString().Equals(ZERO_STRING))
					{
						decOptionValue = Convert.ToDecimal(dgrdData.Columns[PRO_WCCapacityTable.MACHINENO_FLD].Value);
					}					
				}
				
				//dgrdData[dgrdData.Row, PRO_WCCapacityTable.CAPACITY_FLD] = Math.Round((decTotalWorkingTime * decOptionValue * decFactor * (decimal)0.01), 2); 
				dgrdData.Columns[PRO_WCCapacityTable.CAPACITY_FLD].Value = Math.Round((decTotalWorkingTime * decOptionValue * decFactor * (decimal)0.01), 2); 
				dgrdData.UpdateData();
				//Refresh grid
				//dgrdData.Refresh();
			}
			catch(PCSException ex)
			{
				throw new PCSException(ex.mCode, METHOD_NAME, ex);
			}
			catch(Exception ex)
			{
				throw new Exception(ex.Message, ex);
			}			
		}

		private bool IsChecked(object pobjShiftId, object pobjWCCapacityId, DataRow[] parrRows)
		{
			try
			{
				//return false if array is null or empty
				if(parrRows == null)
				{
					return false;
				}
				
				if(parrRows.Length == 0)
				{
					return false;
				}
				
				//loop in array, return true if match
				for(int i = 0; i < parrRows.Length; i++)
				{
					if(parrRows[i][PRO_ShiftCapacityTable.WCCAPACITYID_FLD].Equals(pobjWCCapacityId)
					   && parrRows[i][PRO_ShiftCapacityTable.SHIFTID_FLD].Equals(pobjShiftId))
						return true;
				}

				//return false when passing the loop(no matching)
				return false;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message, ex);
			}
		}
		
		/// <summary>
		/// Set checked status for Select column based on Workcenter Capacity
		/// </summary>
		/// <param name="pintWCCapacityId"></param>
		private void CheckShiftGrid(int pintWCCapacityId)
		{			
			try
			{
				DataRow[] arrRows = dtbShiftCapacity.Select(PRO_ShiftCapacityTable.WCCAPACITYID_FLD + "=" + pintWCCapacityId);

				for(int i =0; i < dgrdShift.RowCount; i++)
				{
					dgrdShift[i, SELECT_COLUMN] = IsChecked(dgrdShift[i, PRO_ShiftTable.SHIFTID_FLD], pintWCCapacityId, arrRows);
				}
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message, ex);
			}
		}
		
		/// <summary>
		/// Load data to Shift grid
		/// </summary>
		private void LoadShiftGrid()
		{
			const string METHOD_NAME = THIS +  ".LoadShiftGrid()";
			try
			{
				
				//Create Shift table struct
				dtbShiftTable.Columns.Add(SELECT_COLUMN, typeof(System.Boolean));
				dtbShiftTable.Columns.Add(PRO_ShiftTable.SHIFTID_FLD, typeof(System.Int32));
				dtbShiftTable.Columns.Add(PRO_ShiftTable.SHIFTDESC_FLD, typeof(System.String));
				dtbShiftTable.Columns.Add(SHIFTPATTERN_COLUMN, typeof(System.Int32));
								
				DataTable dtbTemp = boWCCapacity.GetShiftWithShiftPattern();

				foreach(DataRow drow in dtbTemp.Rows)
				{
					DataRow drowNew = dtbShiftTable.NewRow();
					drowNew[SELECT_COLUMN] = false;
					drowNew[PRO_ShiftTable.SHIFTID_FLD] = drow[PRO_ShiftTable.SHIFTID_FLD];
					drowNew[PRO_ShiftTable.SHIFTDESC_FLD] = drow[PRO_ShiftTable.SHIFTDESC_FLD];
					drowNew[SHIFTPATTERN_COLUMN] = drow[SHIFTPATTERN_COLUMN];

					dtbShiftTable.Rows.Add(drowNew);
				}
				
				dgrdShift.DataSource = dtbShiftTable;
				
				FormControlComponents.RestoreGridLayout(dgrdShift, dtbShiftLayOut);
				
				dgrdShift.Columns[SELECT_COLUMN].ValueItems.Presentation = C1.Win.C1TrueDBGrid.PresentationEnum.CheckBox;
				dgrdShift.Splits[0].DisplayColumns[PRO_ShiftTable.SHIFTDESC_FLD].Locked = true;
				//hide columns
				dgrdShift.Splits[0].DisplayColumns[PRO_ShiftTable.SHIFTID_FLD].Visible = false;
				dgrdShift.Splits[0].DisplayColumns[PRO_ShiftTable.SHIFTID_FLD].AllowSizing = false;
				dgrdShift.Splits[0].DisplayColumns[SHIFTPATTERN_COLUMN].Visible = false;
				dgrdShift.Splits[0].DisplayColumns[SHIFTPATTERN_COLUMN].AllowSizing = false;
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
		/// Processing control get focus event
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author> Tran Quoc Tuan </author>
		/// <created date> 
		/// 15 June 2005
		/// </created>
		private void OnEnterControl(object sender, EventArgs e)
		{
			try
			{
				FormControlComponents.OnEnterControl(sender, e);
			}
			catch
			{}
		}
		
		/// <summary>
		/// Processing control get focus event
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author> Tran Quoc Tuan </author>
		/// <created date> 
		/// 15 June 2005
		/// </created>
		private void OnLeaveControl(object sender, EventArgs e)
		{
			try
			{
				FormControlComponents.OnLeaveControl(sender, e);
			}
			catch
			{}
		}
		
		/// <summary>
		/// process data inputed into the grid
		/// </summary>
		/// <param name="pstrColumnName"></param>
		/// <param name="pstrColumValue"></param>
		/// <returns>If no error then return true, else return false</returns>
		private bool ProcessInputDataInGrid(string pstrColumnName, string pstrColumValue)
		{
			const string METHOD_NAME = THIS +  ".ProcessInputDataInGrid()";
			try
			{
				DateTime dtmBeginDateValue;
				DateTime dtmEndDateValue;

				//Check for each column
				switch (pstrColumnName)
				{
					case PRO_WCCapacityTable.BEGINDATE_FLD:

						//Have DCP Cycle, Dates must be in DCP cycle's range
						if(pstrColumValue != string.Empty 
							&& dtmDate.Text != string.Empty
							&& txtCycle.Text.Trim() != string.Empty)
						{
							dtmBeginDateValue =(DateTime)dtmDate.Value;
							dtmBeginDateValue = new DateTime(dtmBeginDateValue.Year, dtmBeginDateValue.Month, dtmBeginDateValue.Day);

							if(	dtmBeginDateValue < GetDateOnly(dtmBeginCycleDate)
								|| dtmBeginDateValue > GetDateOnly(dtmEndCycleDate)
							)
							{
								PCSMessageBox.Show(ErrorCode.MESSAGE_RTG_ENTRYDATE, MessageBoxIcon.Exclamation);
								dgrdData.Col = dgrdData.Columns.IndexOf(dgrdData.Columns[PRO_WCCapacityTable.BEGINDATE_FLD]);
								dgrdData.Focus();
								return false;
							}								
						}						

						if(pstrColumValue != string.Empty
							&& dtmDate.Text != string.Empty
							&& !dgrdData[dgrdData.Row, PRO_WCCapacityTable.ENDDATE_FLD].Equals(DBNull.Value) 
							&& !dgrdData[dgrdData.Row, PRO_WCCapacityTable.ENDDATE_FLD].ToString().Equals(string.Empty))
						{
							dtmBeginDateValue = (DateTime)dtmDate.Value;
							dtmBeginDateValue = new DateTime(dtmBeginDateValue.Year, dtmBeginDateValue.Month, dtmBeginDateValue.Day);

							dtmEndDateValue = (DateTime)dgrdData[dgrdData.Row, PRO_WCCapacityTable.ENDDATE_FLD];
							dtmEndDateValue = new DateTime(dtmEndDateValue.Year, dtmEndDateValue.Month, dtmEndDateValue.Day);

							if(dtmBeginDateValue > dtmEndDateValue)
							{
								PCSMessageBox.Show(ErrorCode.MESSAGE_DCP_SHIFTPATTERN_FROMDATE_MUST_BE_SMALLER_THAN_TODATE, MessageBoxIcon.Exclamation);
								dgrdData.Col = dgrdData.Columns.IndexOf(dgrdData.Columns[PRO_WCCapacityTable.BEGINDATE_FLD]);
								dgrdData.Focus();
								return false;
							}							
						}						
						break;

					case PRO_WCCapacityTable.ENDDATE_FLD:
						
						if(	pstrColumValue != string.Empty
							&& dtmDate.Text != string.Empty 
							&& txtCycle.Text.Trim() != string.Empty)
						{
							dtmEndDateValue = (DateTime)dtmDate.Value;
							dtmEndDateValue = new DateTime(dtmEndDateValue.Year, dtmEndDateValue.Month, dtmEndDateValue.Day);

							if(dtmEndDateValue < dtmBeginCycleDate
							|| dtmEndDateValue > dtmEndCycleDate
								)
							{
								PCSMessageBox.Show(ErrorCode.MESSAGE_RTG_ENTRYDATE, MessageBoxIcon.Exclamation);
								dgrdData.Col = dgrdData.Columns.IndexOf(dgrdData.Columns[PRO_WCCapacityTable.ENDDATE_FLD]);
								dgrdData.Focus();
								return false;
							}
						}

						if(pstrColumValue != string.Empty
							&& dtmDate.Text != string.Empty
							&& !dgrdData[dgrdData.Row, PRO_WCCapacityTable.BEGINDATE_FLD].Equals(DBNull.Value) 
							&& !dgrdData[dgrdData.Row, PRO_WCCapacityTable.BEGINDATE_FLD].ToString().Equals(string.Empty))
						{
							
							dtmBeginDateValue = (DateTime)dgrdData[dgrdData.Row, PRO_WCCapacityTable.BEGINDATE_FLD];
							dtmBeginDateValue = new DateTime(dtmBeginDateValue.Year, dtmBeginDateValue.Month, dtmBeginDateValue.Day);

							dtmEndDateValue = (DateTime)dtmDate.Value;
							dtmEndDateValue = new DateTime(dtmEndDateValue.Year, dtmEndDateValue.Month, dtmEndDateValue.Day);							

							if(dtmBeginDateValue > dtmEndDateValue)
							{
								PCSMessageBox.Show(ErrorCode.MESSAGE_DCP_SHIFTPATTERN_FROMDATE_MUST_BE_SMALLER_THAN_TODATE, MessageBoxIcon.Exclamation);								
								dgrdData.Col = dgrdData.Columns.IndexOf(dgrdData.Columns[PRO_WCCapacityTable.ENDDATE_FLD]);
								dgrdData.Focus();
								return false;
							}

							
						}
						
						break;

					case PRO_WCCapacityTable.FACTOR_FLD:
						
						if(pstrColumValue.Length != 0 && !FormControlComponents.IsValidPercent(pstrColumValue))
						{
							PCSMessageBox.Show(ErrorCode.MESSAGE_C1NUMBER_INPUT_VALUE, MessageBoxIcon.Exclamation, new string[]{PERCENT_VALUE_RANGE});
							dgrdData.Col = dgrdData.Columns.IndexOf(dgrdData.Columns[PRO_WCCapacityTable.FACTOR_FLD]);
							dgrdData.Focus();
							return false;
						}
						break;
				}

				return true;
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
		/// Build structure of xx table for binding to grid
		/// </summary>
		/// <remarks>
		/// Structure of this table based on struct which be returned by calling
		/// xx.GetDetailByMaster() method.
		/// So we should keep them always are identical.
		/// </remarks>
		/// <returns>DataTable</returns>
		private DataTable BuildDetailTable()
		{
			try
			{
				//Create table
				DataTable dtbtmpWCCapacity;
				dtbtmpWCCapacity = new DataTable(PRO_WCCapacityTable.TABLE_NAME);
				//Add columns
				dtbtmpWCCapacity.Columns.Add(PRO_WCCapacityTable.WCCAPACITYID_FLD, typeof(System.Int32));
				dtbtmpWCCapacity.Columns.Add(PRO_WCCapacityTable.CCNID_FLD, typeof(System.Int32));
				dtbtmpWCCapacity.Columns.Add(PRO_WCCapacityTable.WORKCENTERID_FLD, typeof(System.Int32));
				dtbtmpWCCapacity.Columns.Add(PRO_WCCapacityTable.BEGINDATE_FLD, typeof(System.DateTime));
				dtbtmpWCCapacity.Columns.Add(PRO_WCCapacityTable.ENDDATE_FLD, typeof(System.DateTime));
				dtbtmpWCCapacity.Columns.Add(PRO_WCCapacityTable.FACTOR_FLD, typeof(System.Decimal));
				dtbtmpWCCapacity.Columns.Add(PRO_WCCapacityTable.WCTYPE_FLD, typeof(System.Int32));
				dtbtmpWCCapacity.Columns.Add(PRO_WCCapacityTable.CREWSIZE_FLD, typeof(System.Decimal));
				dtbtmpWCCapacity.Columns.Add(PRO_WCCapacityTable.MACHINENO_FLD, typeof(System.Int32));
				dtbtmpWCCapacity.Columns.Add(PRO_WCCapacityTable.CAPACITY_FLD, typeof(System.Decimal));
				
				return dtbtmpWCCapacity;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message, ex);
			}
		}
		
		private DataTable BuildShiftCapcityTable()
		{
			try
			{
				//Create table
				DataTable dtbtmpShiftCapacity = new DataTable(PRO_ShiftCapacityTable.TABLE_NAME);
				//Add columns
				dtbtmpShiftCapacity.Columns.Add(PRO_ShiftCapacityTable.SHIFTCAPACITYID_FLD, typeof(System.Int32));
				dtbtmpShiftCapacity.Columns.Add(PRO_ShiftCapacityTable.WCCAPACITYID_FLD, typeof(System.Int32));
				dtbtmpShiftCapacity.Columns.Add(PRO_ShiftCapacityTable.SHIFTID_FLD, typeof(System.Int32));
				dtbtmpShiftCapacity.PrimaryKey = new DataColumn[]{dtbtmpShiftCapacity.Columns[PRO_ShiftCapacityTable.SHIFTID_FLD], 
																  dtbtmpShiftCapacity.Columns[PRO_ShiftCapacityTable.WCCAPACITYID_FLD]};
				return dtbtmpShiftCapacity;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message, ex);
			}
		}

		/// <summary>
		/// Lock controls for updating
		/// </summary>
		/// <param name="pblnLock"></param>
		/// <author> Tran Quoc Tuan </author>
		/// <created date> 
		/// 15 June 2005
		/// </created>
		private void LockControl(bool pblnLock)
		{
			dgrdData.AllowAddNew = pblnLock;
			dgrdData.AllowDelete = pblnLock;
			dgrdData.AllowUpdate = pblnLock;			
			btnAdjust.Enabled = pblnLock;
			btnDelete.Enabled = pblnLock;	
		
			dgrdData.AllowSort = !pblnLock;
			btnSave.Enabled = !pblnLock;
		}
		
		private void LoadFormByDefaultInfomation(int pinWorkCenterID, int pinDCPCycleID)
		{
			LoadFormByDefaultInfomation(pinWorkCenterID);
			FilterDataByCycle(pinDCPCycleID);
		}
		
		private void FilterDataByCycle(int pinDCPCycleID)
		{			
			const string SQL_DATE_TIME_FORMAT = "yyy-MM-dd HH:mm:ss";
			const string MAXDAYS_FLD = "MaxDays";

			//Get Cycle information
			DCOptionsBO boDCOptions = new DCOptionsBO();
			DataRow drowDCOptionMaster = boDCOptions.GetDCOptionMaster(pinDCPCycleID);
			
			if(drowDCOptionMaster == null) return;

			txtCycle.Text = drowDCOptionMaster[PRO_DCOptionMasterTable.CYCLE_FLD].ToString();
			txtCycle.Tag = drowDCOptionMaster[PRO_DCOptionMasterTable.DCOPTIONMASTERID_FLD].ToString();
			
			//Begin active date of cycle
			//dtmBeginCycleDate = DateTime.Parse(drowDCOptionMaster[MTR_MPSCycleOptionMasterTable.ASOFDATE_FLD].ToString());
			dtmBeginCycleDate = (DateTime)drowDCOptionMaster[MTR_MPSCycleOptionMasterTable.ASOFDATE_FLD];
				
			//Truncate hour, minute, second
			dtmBeginCycleDate = new DateTime(dtmBeginCycleDate.Year, dtmBeginCycleDate.Month, dtmBeginCycleDate.Day);

			//Calculate active end date
			int iPlanHoz = 0;				
//			if(!drowDCOptionMaster[MAXDAYS_FLD].Equals(DBNull.Value)
//				&& !drowDCOptionMaster[MAXDAYS_FLD].ToString().Equals(string.Empty))
//			{
//				iPlanHoz = int.Parse(drowDCOptionMaster[MAXDAYS_FLD].ToString());
//			}
			iPlanHoz = int.Parse(drowDCOptionMaster[PRO_DCOptionMasterTable.PLANHORIZON_FLD].ToString());
			//End active date of cycle
			dtmEndCycleDate = dtmBeginCycleDate.AddDays(iPlanHoz);
				
			//Build filter string 
			//Date in WC Capacity must be in range of [Begin Date - End date]
			string strFilter = "(" + PRO_WCCapacityTable.BEGINDATE_FLD + ">='" + dtmBeginCycleDate.ToString(SQL_DATE_TIME_FORMAT) + "'";
			strFilter += " AND " + PRO_WCCapacityTable.BEGINDATE_FLD + "<='" + dtmEndCycleDate.ToString(SQL_DATE_TIME_FORMAT) + "')";

			strFilter += " OR (" + PRO_WCCapacityTable.ENDDATE_FLD + ">='" + dtmBeginCycleDate.ToString(SQL_DATE_TIME_FORMAT) + "'";
			strFilter += " AND " + PRO_WCCapacityTable.ENDDATE_FLD + "<='" + dtmEndCycleDate.ToString(SQL_DATE_TIME_FORMAT) + "')";

			strFilter += " OR (" + PRO_WCCapacityTable.BEGINDATE_FLD + "<='" + dtmBeginCycleDate.ToString(SQL_DATE_TIME_FORMAT) + "'";
			strFilter += " AND " + PRO_WCCapacityTable.ENDDATE_FLD + ">='" + dtmEndCycleDate.ToString(SQL_DATE_TIME_FORMAT) + "')";

			strFilter += " OR ( (" + PRO_WCCapacityTable.BEGINDATE_FLD + " >= '" + dtmBeginCycleDate.ToString(SQL_DATE_TIME_FORMAT) + "' AND " + PRO_WCCapacityTable.BEGINDATE_FLD + " <= '" + dtmEndCycleDate.ToString(SQL_DATE_TIME_FORMAT) + "')";
			strFilter += " AND  (" + PRO_WCCapacityTable.ENDDATE_FLD + " >= '" + dtmBeginCycleDate.ToString(SQL_DATE_TIME_FORMAT) + "' AND " + PRO_WCCapacityTable.BEGINDATE_FLD + " <= '" + dtmEndCycleDate.ToString(SQL_DATE_TIME_FORMAT) + "') )";

			// Or is added rows
			strFilter += " OR (" + PRO_WCCapacityTable.WCCAPACITYID_FLD + " <= 0)";
			
			dtbWCCapacityToStore = ((DataTable)dgrdData.DataSource).Copy();

			dtbWCCapacityToStore.DefaultView.RowFilter = strFilter;

			//Assign to grid			
			dgrdData.DataSource = dtbWCCapacityToStore;
			FormatDataGrid();
			//dgrdData.Splits[0].DisplayColumns[PRO_WCCapacityTable.WCCAPACITYID_FLD].Visible = true;
			//Display Cycle's Active Date info
			txtCycleRange.Text = lblActive.Text + Constants.OPEN_SBRACKET + dtmBeginCycleDate.ToString(Constants.DATETIME_FORMAT) + Constants.WHITE_SPACE + lblTo.Text + Constants.WHITE_SPACE + dtmEndCycleDate.ToString(Constants.DATETIME_FORMAT) + Constants.CLOSE_SBRACKET;

			//Check grid
			DataRow[] arrMatchedRow = dtbWCCapacityToStore.Select(strFilter);

			if(arrMatchedRow.Length != 0)				
			{
				CheckShiftGrid(int.Parse(dgrdData[0, PRO_WCCapacityTable.WCCAPACITYID_FLD].ToString()));
			}
			else
			{
				CheckShiftGrid(0);
			}
			
			//Reset modify status
			txtCycle.Modified = false;

			//Unlock control for updating
			LockControl(true);
			if (dgrdData.RowCount == 0)
			{
				btnAdjust.Enabled = false;
			}
			if (dgrdData.RowCount > 0)
			{
				dtmBeginCycleDateToCheck = (DateTime) dgrdData[0, PRO_WCCapacityTable.BEGINDATE_FLD];
				dtmEndCycleDateToCheck = (DateTime) dgrdData[dgrdData.RowCount - 1, PRO_WCCapacityTable.ENDDATE_FLD];
			}
		}

		private void LoadFormByDefaultInfomation(int pinWorkCenterID)
		{
			MST_WorkCenterVO voWorkCenter = (MST_WorkCenterVO)boWCCapacity.GetWorkCenterVO(pinWorkCenterID);
			if(voWorkCenter == null) return;

			txtWorkCenter.Text = voWorkCenter.Code;
			txtWorkCenter.Tag = voWorkCenter.WorkCenterID;
			txtWCName.Text = voWorkCenter.Name;
			
			//Load Work-Center capacity
			DataSet dtsWCCapacity = boWCCapacity.GetWCCapacityByWorkCenter(int.Parse(cboCCN.SelectedValue.ToString()), pinWorkCenterID);
			if(dtsWCCapacity.Tables.Count != 0)
			{
				dtbWCCapacity = dtsWCCapacity.Tables[0];
			}
			else
			{
				dtbWCCapacity = BuildDetailTable();
			}
			dtbWCCapacityToStore = dtbWCCapacity.Copy();
			//Assign to grid			
			dgrdData.DataSource = dtbWCCapacityToStore;
			FormatDataGrid();
			//Load ShiftCapacity 
			DataSet dtsShiftCapacity = boWCCapacity.GetShiftCapacityByWorkCenter(int.Parse(cboCCN.SelectedValue.ToString()), pinWorkCenterID);
			if(dtsShiftCapacity.Tables.Count != 0)
			{
				dtbShiftCapacity = dtsShiftCapacity.Tables[0];
				dtbShiftCapacity.PrimaryKey = new DataColumn[]{dtbShiftCapacity.Columns[PRO_ShiftCapacityTable.SHIFTID_FLD], 
																  dtbShiftCapacity.Columns[PRO_ShiftCapacityTable.WCCAPACITYID_FLD]};
			}
			else
			{
				dtbShiftCapacity = BuildShiftCapcityTable();
			}

			//reset fake id 
			intWCCapacityFakeId = 0;
			//Reset array of delete items
			arlDeletedIDs.Clear();
			intWCCapacityIDNeedToDelete = 0;

			//Check grid					
            if (dgrdData.RowCount > 0)
			{
				CheckShiftGrid(int.Parse(dgrdData[0, PRO_WCCapacityTable.WCCAPACITYID_FLD].ToString()));
			}
			else
			{						
				CheckShiftGrid(0);
			}
			
			//Reset modify status
			txtWorkCenter.Modified = false;

			//Unlock control for updating
			LockControl(true);
			if (dgrdData.RowCount == 0)
			{
				btnAdjust.Enabled = false;
			}			
		}

		/// <summary>
		/// Fill related data on controls when select Cycle
		/// </summary>
		/// <param name="pblnAlwayShowDialog"></param>
		private bool SelectWorkCenter(string pstrMethodName, bool pblnAlwayShowDialog)
		{
			try				
			{
				Hashtable htbCriteria = new Hashtable();
				DataRowView drwResult = null;		

				if(cboCCN.SelectedValue != null)
				{
					htbCriteria.Add(MST_WorkCenterTable.CCNID_FLD, cboCCN.SelectedValue);
				}
				else
				{
					htbCriteria.Add(MST_WorkCenterTable.CCNID_FLD, 0);
				}

				//Call OpenSearchForm for selecting MPS planning cycle
				drwResult = FormControlComponents.OpenSearchForm(MST_WorkCenterTable.TABLE_NAME, MST_WorkCenterTable.CODE_FLD, txtWorkCenter.Text, htbCriteria, pblnAlwayShowDialog);
				
				// If has Master location matched searching condition, fill values to form's controls
				if (drwResult != null)
				{
					int iWorkCenterID = int.Parse(drwResult[MST_WorkCenterTable.WORKCENTERID_FLD].ToString());
					LoadFormByDefaultInfomation(iWorkCenterID);					
					arlDeletedIDs.Clear();
					intWCCapacityIDNeedToDelete = 0;
					//Clear cycle
					txtCycle.Text = string.Empty;
					txtCycleRange.Text = string.Empty;
					txtCycle.Tag = null;
				}
				else
				{
					if(!pblnAlwayShowDialog)
					{
						txtWorkCenter.Tag = ZERO_STRING;
						txtWorkCenter.Focus();
						return false;
					}
				}

				return true;
			}
			catch (PCSException ex)
			{
				throw new PCSException(ex.mCode, pstrMethodName, ex);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message, ex);
			}	
		}
		

		/// <summary>
		/// Fill related data on controls when select Cycle
		/// </summary>
		/// <param name="pblnAlwayShowDialog"></param>
		private bool SelectDCPCycle(string pstrMethodName, bool pblnAlwayShowDialog)
		{
			Hashtable htbCriteria = new Hashtable();
			DataRowView drwResult = null;		

			if(cboCCN.SelectedValue != null)
			{
				htbCriteria.Add(PRO_DCOptionMasterTable.CCNID_FLD, cboCCN.SelectedValue);
			}
			else
			{
				htbCriteria.Add(PRO_DCOptionMasterTable.CCNID_FLD, 0);
			}

			//Call OpenSearchForm for selecting MPS planning cycle
			drwResult = FormControlComponents.OpenSearchForm(PRO_DCOptionMasterTable.TABLE_NAME, PRO_DCOptionMasterTable.CYCLE_FLD, txtCycle.Text, htbCriteria, pblnAlwayShowDialog);
			
			// If has Master location matched searching condition, fill values to form's controls
			if (drwResult != null)
			{
				intDCPCycleID = int.Parse(drwResult[PRO_DCOptionMasterTable.DCOPTIONMASTERID_FLD].ToString());
				
				//Filter data by selected cycle
				FilterDataByCycle(intDCPCycleID);
			}
			else
			{
				if(!pblnAlwayShowDialog)
				{	
					//Reset cycle's info 
					txtCycleRange.Text = string.Empty;
					dtmBeginCycleDate = DateTime.MinValue;
					dtmEndCycleDate = DateTime.MinValue;
					dtmBeginCycleDateToCheck = DateTime.MinValue;
					dtmEndCycleDateToCheck = DateTime.MinValue;
					txtCycle.Tag = ZERO_STRING;
					txtCycle.Focus();
					return false;
				}				
			}

			return true;
			
		}

		private int GetActualTotalRow(DataTable pdtbTable)
		{
			if(pdtbTable == null) return 0;

			int iCount = 0;

			//loop and count undeleted rows
			foreach(DataRow drow in pdtbTable.Rows)
			{
				if(drow.RowState != DataRowState.Deleted)
				{
					iCount++;
				}
			}

			return iCount;

		}

		/// <summary>
		/// Validate data before updating into database
		/// </summary>
		/// <returns></returns>
		/// <author> Tran Quoc Tuan </author>
		/// <created date> 
		/// 15 June 2005
		/// </created>
		private bool ValidateData()
		{
			const string METHOD_NAME = THIS + ".ValidateData()";
			try
			{
				if(cboCCN.SelectedValue == null)
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Exclamation);
					cboCCN.Focus();
					return false;
				}

				//Check data in the grid
                if (dgrdData.RowCount == 0)
				{
					//If in filltering mode & has data
					if(txtCycle.Text.Trim() != string.Empty && GetActualTotalRow(dtbWCCapacity) > 0)
					{
						return true;
					}

					PCSMessageBox.Show(ErrorCode.MESSAGE_PLEASE_ENTER_DETAIL_INFOR, MessageBoxIcon.Exclamation);
					dgrdData.Col = dgrdData.Columns.IndexOf(dgrdData.Columns[PRO_WCCapacityTable.BEGINDATE_FLD]);
					dgrdData.Focus();
					return false;
				}

				//Update grid data 
				dgrdData.UpdateData();

				for(int intRowIndex = 0; intRowIndex < dgrdData.RowCount; intRowIndex++)
				{
					if(dgrdData[intRowIndex, PRO_WCCapacityTable.WCCAPACITYID_FLD].Equals(DBNull.Value)
						|| dgrdData[intRowIndex, PRO_WCCapacityTable.WCCAPACITYID_FLD].ToString().Equals(string.Empty))
					{
						PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Exclamation);
						dgrdData.Row = intRowIndex;
						dgrdData.Col = dgrdData.Columns.IndexOf(dgrdData.Columns[PRO_WCCapacityTable.BEGINDATE_FLD]);
						dgrdData.Focus();
						return false;
					}

					//PRO_WCCapacityTable.BEGINDATE_FLD:
					if(dgrdData[intRowIndex, PRO_WCCapacityTable.BEGINDATE_FLD].ToString().Length == 0)
					{
						PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Exclamation);							
						dgrdData.Row = intRowIndex;
						dgrdData.Col = dgrdData.Columns.IndexOf(dgrdData.Columns[PRO_WCCapacityTable.BEGINDATE_FLD]);
						dgrdData.Focus();
						return false;
					}					

					//PRO_WCCapacityTable.ENDDATE_FLD:
					if(dgrdData[intRowIndex, PRO_WCCapacityTable.ENDDATE_FLD].ToString().Length == 0)
					{
						PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Exclamation);							
						dgrdData.Row = intRowIndex;
						dgrdData.Col = dgrdData.Columns.IndexOf(dgrdData.Columns[PRO_WCCapacityTable.ENDDATE_FLD]);
						dgrdData.Focus();
						return false;
					}
					
					DateTime dtmBeginDateValue = (DateTime)dgrdData[intRowIndex, PRO_WCCapacityTable.BEGINDATE_FLD];
					dtmBeginDateValue = new DateTime(dtmBeginDateValue.Year, dtmBeginDateValue.Month, dtmBeginDateValue.Day);

					DateTime dtmEndDateValue = (DateTime)dgrdData[intRowIndex, PRO_WCCapacityTable.ENDDATE_FLD];
					dtmEndDateValue = new DateTime(dtmEndDateValue.Year, dtmEndDateValue.Month, dtmEndDateValue.Day);

					if(dtmBeginDateValue > dtmEndDateValue)
					{
						PCSMessageBox.Show(ErrorCode.MESSAGE_DCP_SHIFTPATTERN_FROMDATE_MUST_BE_SMALLER_THAN_TODATE, MessageBoxIcon.Exclamation);
						dgrdData.Row = intRowIndex;
						dgrdData.Col = dgrdData.Columns.IndexOf(dgrdData.Columns[PRO_WCCapacityTable.ENDDATE_FLD]);
						dgrdData.Focus();
						return false;
					}
					
					//Have DCP Cycle, Dates must be in DCP cycle's range
					if(txtCycle.Text.Trim() != string.Empty)
					{
						if ((dtmBeginCycleDateToCheck != DateTime.MinValue)
							&& (dtmEndCycleDateToCheck != DateTime.MinValue))
						{
							if(GetDateOnly((DateTime)dgrdData[intRowIndex, PRO_WCCapacityTable.BEGINDATE_FLD])	< GetDateOnly(dtmBeginCycleDateToCheck)
								|| GetDateOnly((DateTime)dgrdData[intRowIndex, PRO_WCCapacityTable.BEGINDATE_FLD])	> GetDateOnly(dtmEndCycleDateToCheck)
								)
							{
								PCSMessageBox.Show(ErrorCode.MESSAGE_RTG_ENTRYDATE, MessageBoxIcon.Exclamation);
								dgrdData.Row = intRowIndex;
								dgrdData.Col = dgrdData.Columns.IndexOf(dgrdData.Columns[PRO_WCCapacityTable.BEGINDATE_FLD]);
								dgrdData.Focus();
								return false;
							}		
						
							if(GetDateOnly((DateTime)dgrdData[intRowIndex, PRO_WCCapacityTable.ENDDATE_FLD]) <GetDateOnly(dtmBeginCycleDateToCheck)
								|| GetDateOnly((DateTime)dgrdData[intRowIndex, PRO_WCCapacityTable.ENDDATE_FLD]) > GetDateOnly(dtmEndCycleDateToCheck)
								)
							{
								PCSMessageBox.Show(ErrorCode.MESSAGE_RTG_ENTRYDATE, MessageBoxIcon.Exclamation);
								dgrdData.Row = intRowIndex;
								dgrdData.Col = dgrdData.Columns.IndexOf(dgrdData.Columns[PRO_WCCapacityTable.ENDDATE_FLD]);
								dgrdData.Focus();
								return false;
							}		
						}
					}
					
					//PRO_WCCapacityTable.FACTOR_FLD:
					if(dgrdData[intRowIndex, PRO_WCCapacityTable.FACTOR_FLD].ToString().Length == 0)
					{
						PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Exclamation);
						dgrdData.Col = dgrdData.Columns.IndexOf(dgrdData.Columns[PRO_WCCapacityTable.FACTOR_FLD]);
						dgrdData.Row = intRowIndex;
						dgrdData.Focus();
						return false;
					}
					
					if(!FormControlComponents.IsValidPercent(dgrdData[intRowIndex, PRO_WCCapacityTable.FACTOR_FLD].ToString()))
					{
						PCSMessageBox.Show(ErrorCode.MESSAGE_C1NUMBER_INPUT_VALUE, MessageBoxIcon.Exclamation, new string[]{PERCENT_VALUE_RANGE});
						dgrdData.Col = dgrdData.Columns.IndexOf(dgrdData.Columns[PRO_WCCapacityTable.FACTOR_FLD]);
						dgrdData.Row = intRowIndex;
						dgrdData.Focus();
						return false;
					}
					
					if(dgrdData[intRowIndex, PRO_WCCapacityTable.WCTYPE_FLD].ToString().Length == 0)
					{
						PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Exclamation);
						dgrdData.Col = dgrdData.Columns.IndexOf(dgrdData.Columns[PRO_WCCapacityTable.WCTYPE_FLD]);
						dgrdData.Row = intRowIndex;
						dgrdData.Focus();
						return false;
					}

					if(int.Parse(dgrdData[intRowIndex, PRO_WCCapacityTable.WCTYPE_FLD].ToString()) == (int)WCType.Labor)
					{
						//PRO_WCCapacityTable.CREWSIZE_FLD:
						if(dgrdData[intRowIndex, PRO_WCCapacityTable.CREWSIZE_FLD].Equals(DBNull.Value)
						   || dgrdData[intRowIndex, PRO_WCCapacityTable.CREWSIZE_FLD].ToString().Equals(ZERO_STRING))
						{
							PCSMessageBox.Show(ErrorCode.MESSAGE_MANDATORY_FIELD_REQUIRED, MessageBoxIcon.Exclamation, new string[]{dgrdData.Columns[PRO_WCCapacityTable.CREWSIZE_FLD].Caption});
							dgrdData.Col = dgrdData.Columns.IndexOf(dgrdData.Columns[PRO_WCCapacityTable.CREWSIZE_FLD]);
							dgrdData.Row = intRowIndex;
							dgrdData.Focus();
							return false;
						}
					}
					else
					{
						//PRO_WCCapacityTable.MACHINENO_FLD:
						if(dgrdData[intRowIndex, PRO_WCCapacityTable.MACHINENO_FLD].Equals(DBNull.Value)
							|| dgrdData[intRowIndex, PRO_WCCapacityTable.MACHINENO_FLD].ToString().Equals(ZERO_STRING))
						{
							PCSMessageBox.Show(ErrorCode.MESSAGE_MANDATORY_FIELD_REQUIRED, MessageBoxIcon.Exclamation, new string[]{dgrdData.Columns[PRO_WCCapacityTable.MACHINENO_FLD].Caption});
							dgrdData.Col = dgrdData.Columns.IndexOf(dgrdData.Columns[PRO_WCCapacityTable.MACHINENO_FLD]);
							dgrdData.Row = intRowIndex;
							dgrdData.Focus();
							return false;							
						}
					}
					
					DataRow[] arrTemp = dtbShiftCapacity.Select(PRO_ShiftCapacityTable.WCCAPACITYID_FLD + "=" + dgrdData[intRowIndex, PRO_WCCapacityTable.WCCAPACITYID_FLD].ToString());

					if(arrTemp.Length == 0)
					{
						PCSMessageBox.Show(ErrorCode.MESSAGE_WCC_PLEASE_SELECT_SHIFT, MessageBoxIcon.Exclamation);						
						dgrdShift.Focus();
						return false;
					}
				}

				//Check overlap date
				if(IsOverlap())
				{
					PCSMessageBox.Show(ErrorCode.MESSAGE_DATE_IS_OVERLAP, MessageBoxIcon.Exclamation);					
					dgrdData.Focus();
					return false;
				}

				return true;
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
		
		private void FormatDataGrid()
		{
			try
			{	
				//Restore layout
				FormControlComponents.RestoreGridLayout(dgrdData, dtbGridLayOut);				

				//Change representation
				dgrdData.Columns[PRO_WCCapacityTable.CAPACITY_FLD].NumberFormat = Constants.DECIMAL_NUMBERFORMAT;
				dgrdData.Splits[0].DisplayColumns[PRO_WCCapacityTable.CAPACITY_FLD].Locked = true;

				dgrdData.Columns[PRO_WCCapacityTable.BEGINDATE_FLD].NumberFormat = Constants.DATETIME_FORMAT;
				dgrdData.Columns[PRO_WCCapacityTable.ENDDATE_FLD].NumberFormat = Constants.DATETIME_FORMAT;
				
				dgrdData.Columns[PRO_WCCapacityTable.FACTOR_FLD].NumberFormat = Constants.DECIMAL_NUMBERFORMAT;
				dgrdData.Columns[PRO_WCCapacityTable.CREWSIZE_FLD].NumberFormat = Constants.DECIMAL_NUMBERFORMAT;
				dgrdData.Columns[PRO_WCCapacityTable.MACHINENO_FLD].NumberFormat = Constants.INTERGER_NUMBERFORMAT;

				dgrdData.Columns[PRO_WCCapacityTable.FACTOR_FLD].Editor = numDecValue;
				dgrdData.Columns[PRO_WCCapacityTable.CREWSIZE_FLD].Editor = numDecValue;
				dgrdData.Columns[PRO_WCCapacityTable.MACHINENO_FLD].Editor = numIntValue;

				dgrdData.Columns[PRO_WCCapacityTable.BEGINDATE_FLD].Editor = dtmDate;				
				dgrdData.Columns[PRO_WCCapacityTable.ENDDATE_FLD].Editor = dtmDate;
				
				//change format of Distrubiton Method
				dgrdData.Columns[PRO_WCCapacityTable.WCTYPE_FLD].ValueItems.Values.Clear();
				dgrdData.Columns[PRO_WCCapacityTable.WCTYPE_FLD].ValueItems.Values.Add(new ValueItem( ((int)WCType.Labor).ToString(), WCType.Labor.ToString()));
				dgrdData.Columns[PRO_WCCapacityTable.WCTYPE_FLD].ValueItems.Values.Add(new ValueItem( ((int)WCType.Machine).ToString(), WCType.Machine.ToString()));

				dgrdData.Columns[PRO_WCCapacityTable.WCTYPE_FLD].ValueItems.Presentation = C1.Win.C1TrueDBGrid.PresentationEnum.ComboBox;
				dgrdData.Columns[PRO_WCCapacityTable.WCTYPE_FLD].ValueItems.Translate = true;
				dgrdData.Splits[0].DisplayColumns[PRO_WCCapacityTable.WCTYPE_FLD].DropDownList = true;
				
				dgrdData.Refresh();
				//CheckShiftGrid();
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message, ex);
			}
		}
		
		#endregion Methods

		#region Event Processing
		
		private void WorkCenterCapacity_Load(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".WorkCenterCapacity_Load()";
			try
			{
				enuFormAction = EnumAction.Default;
				
				//Set form security
				Security objSecurity = new Security();
				this.Name = THIS;
				if(objSecurity.SetRightForUserOnForm(this, SystemProperty.UserName) == 0)
				{
					// You don't have the right to view this item
					PCSMessageBox.Show(ErrorCode.MESSAGE_YOU_HAVE_NO_RIGHT_TO_VIEW, MessageBoxIcon.Warning);
					this.Close();
					return;
				}

				//Load CCN and set default CCN
				UtilsBO boUtils = new UtilsBO();
				DataSet dstCCN = boUtils.ListCCN();				
				FormControlComponents.PutDataIntoC1ComboBox(cboCCN, dstCCN.Tables[MST_CCNTable.TABLE_NAME], MST_CCNTable.CODE_FLD, MST_CCNTable.CCNID_FLD, MST_CCNTable.TABLE_NAME);
				
				//Set default CCN for CNN combobox
				if(SystemProperty.CCNID != 0)
				{
					cboCCN.SelectedValue = SystemProperty.CCNID;
				}				
				
				//get grid layout
				dtbGridLayOut = FormControlComponents.StoreGridLayout(dgrdData);				
				dtbShiftLayOut = FormControlComponents.StoreGridLayout(dgrdShift);
				
				LoadShiftGrid();				

				//Format editor controls
				dtmDate.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
				dtmDate.CustomFormat = Constants.DATETIME_FORMAT;
				dtmDate.EmptyAsNull = true;
				
				numDecValue.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
				numDecValue.CustomFormat = Constants.DECIMAL_NUMBERFORMAT;
				numDecValue.EmptyAsNull = true;
				
				numIntValue.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
				numIntValue.CustomFormat = Constants.INTERGER_NUMBERFORMAT;
				numIntValue.EmptyAsNull = true;
				
				if(mintWorkCenterID > 0 &&  mintDCPCycleID > 0)
				{
					txtWorkCenter.Tag = mintWorkCenterID;
					txtCycle.Tag = mintDCPCycleID;
					intDCPCycleID = mintDCPCycleID;
					LoadFormByDefaultInfomation(mintWorkCenterID, mintDCPCycleID);
				}
				else if(mintWorkCenterID > 0)
				{
					LoadFormByDefaultInfomation(mintWorkCenterID);
				}
				else
				{
					ResetFormValues(true);
					//Clear controls value and lock for editing
					LockControl(false);
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

		private void btnWorkCenter_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnWorkCenter_Click()";

			try
			{
				SelectWorkCenter(METHOD_NAME, true);
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
		
		private void btnSave_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnSave_Click()";
			try
			{
				
				//validate data
				blnDataIsValid = ValidateData() && !dgrdData.EditActive;
				if(!blnDataIsValid) return;
				if (dtbWCCapacityToStore.Rows.Count == 0)
				{
					dtbWCCapacityToStore = ((DataTable)dgrdData.DataSource).Copy();
				}
				//remove WCCapacityID is not use
				RemoveWCCapacityID();

				//foreach(DataRow drow in dtbWCCapacity.Rows)
				foreach(DataRow drow in dtbWCCapacityToStore.Rows)
				{
					
					if( drow.RowState == DataRowState.Added || drow.RowState == DataRowState.Modified)
					{
						//Recalculate capacity value
						//CalculateCapacity();

						//Create VO bject
						PRO_WCCapacityVO voWCCapacity = new PRO_WCCapacityVO();

						voWCCapacity.BeginDate = DateTime.Parse(drow[PRO_WCCapacityTable.BEGINDATE_FLD].ToString());
						voWCCapacity.EndDate = DateTime.Parse(drow[PRO_WCCapacityTable.ENDDATE_FLD].ToString());
						voWCCapacity.Capacity = decimal.Parse(drow[PRO_WCCapacityTable.CAPACITY_FLD].ToString());
						voWCCapacity.CCNID = int.Parse(cboCCN.SelectedValue.ToString());						
						voWCCapacity.Factor = decimal.Parse(drow[PRO_WCCapacityTable.FACTOR_FLD].ToString());
						voWCCapacity.WorkCenterID = int.Parse(txtWorkCenter.Tag.ToString());
						voWCCapacity.WCCapacityID = int.Parse(drow[PRO_WCCapacityTable.WCCAPACITYID_FLD].ToString());
						voWCCapacity.WCType = int.Parse(drow[PRO_WCCapacityTable.WCTYPE_FLD].ToString());
                        
						if(!drow[PRO_WCCapacityTable.CREWSIZE_FLD].Equals(DBNull.Value))
						{
							voWCCapacity.CrewSize = decimal.Parse(drow[PRO_WCCapacityTable.CREWSIZE_FLD].ToString());
						}
						else
						{
							voWCCapacity.CrewSize = decimal.MinusOne;
						}
						
						if(!drow[PRO_WCCapacityTable.MACHINENO_FLD].Equals(DBNull.Value))
						{
							voWCCapacity.MachineNo = int.Parse(drow[PRO_WCCapacityTable.MACHINENO_FLD].ToString());						
						}
						else
						{
							voWCCapacity.MachineNo = Int32.MinValue;
						}
						
						//if(int.Parse(drow[PRO_WCCapacityTable.WCCAPACITYID_FLD].ToString()) < 0)
						if(voWCCapacity.WCCapacityID < 0)
						{							
							//WCCapacityId is negative means that it's new row --> call Add method	
							int intWCCapacityId = boWCCapacity.AddAndReturnId(voWCCapacity);                        
							
							//Reset WCCapacityID of ShiftCapacity
							DataRow[] arrRows = dtbShiftCapacity.Select(PRO_ShiftCapacityTable.WCCAPACITYID_FLD + "=" + voWCCapacity.WCCapacityID);
							for(int i = 0; i < arrRows.Length; i++)
							{					
								arrRows[i][PRO_ShiftCapacityTable.WCCAPACITYID_FLD] = intWCCapacityId;
							}
							
							//reset WCCapactityID, CCN, Work Center of current row
							drow[PRO_WCCapacityTable.WCCAPACITYID_FLD] = intWCCapacityId;
						}
						else
						{
							boWCCapacity.Update(voWCCapacity);
						}
					}
				}
				
				// update on ShiftCapacity table
				if(dtbShiftCapacity.DataSet != null)
				{
					boWCCapacity.UpdateShiftCapacityDataSet(dtbShiftCapacity.DataSet);
					dtbShiftCapacity.DataSet.AcceptChanges();
				}
				else
				{
					DataSet dtsTemp = new DataSet();
					dtsTemp.Tables.Add(dtbShiftCapacity);
					boWCCapacity.UpdateShiftCapacityDataSet(dtsTemp);
					dtsTemp.AcceptChanges();
				}

				//Actually delete deleted rows
				foreach(object objID in arlDeletedIDs)
				{
					boWCCapacity.Delete(int.Parse(objID.ToString()));
				}

//				//Commit change on dataset
//				if(dtbWCCapacity.DataSet != null)
//				{
//					//boWCCapacity.UpdateDataSet(dtbWCCapacity.DataSet);
//					dtbWCCapacity.DataSet.AcceptChanges();
//				}
//				else
//				{
//					DataSet dtsTemp = new DataSet();
//					dtsTemp.Tables.Add(dtbWCCapacity);
//					//boWCCapacity.UpdateDataSet(dtsTemp);
//					dtsTemp.AcceptChanges();
//				}		
				//Commit change on dataset
				if(dtbWCCapacityToStore.DataSet != null)
				{
					//boWCCapacity.UpdateDataSet(dtbWCCapacity.DataSet);
					dtbWCCapacityToStore.DataSet.AcceptChanges();
				}
				else
				{
					DataSet dtsTemp = new DataSet();
					dtsTemp.Tables.Add(dtbWCCapacityToStore);
					//boWCCapacity.UpdateDataSet(dtsTemp);
					dtsTemp.AcceptChanges();
				}		

				//delete row
				boWCCapacity.Delete(intWCCapacityIDNeedToDelete);
				//Load WCCapacity after saving
				if ((txtWorkCenter.Tag != null) && (txtWorkCenter.Tag != DBNull.Value))
				{
					LoadFormByDefaultInfomation(int.Parse(txtWorkCenter.Tag.ToString()));
					if (intDCPCycleID > 0)
					{
						FilterDataByCycle(intDCPCycleID);
					}
				}
				
				//Load ShiftCapacity 
				DataSet dtsShiftCapacity = boWCCapacity.GetShiftCapacityByWorkCenter(int.Parse(cboCCN.SelectedValue.ToString()), int.Parse(txtWorkCenter.Tag.ToString()));
				if(dtsShiftCapacity.Tables.Count != 0)
				{
					dtbShiftCapacity = dtsShiftCapacity.Tables[0];
					dtbShiftCapacity.PrimaryKey = new DataColumn[]{dtbShiftCapacity.Columns[PRO_ShiftCapacityTable.SHIFTID_FLD], 
																	  dtbShiftCapacity.Columns[PRO_ShiftCapacityTable.WCCAPACITYID_FLD]};
				}
				else
				{
					dtbShiftCapacity = BuildShiftCapcityTable();
				}
				
				//Reset array of delete items
				arlDeletedIDs.Clear();
				intWCCapacityIDNeedToDelete = 0;
				btnSave.Enabled = false;
				//btnSeparate.Enabled = false;
				//dgrdData.Refresh();
				//dgrdShift.Refresh();
				intWCCapacityIDNeedToDelete = 0;
				PCSMessageBox.Show(ErrorCode.MESSAGE_AFTER_SAVE_DATA);
				btnAdjust.Enabled = true;
				blnHasAdjust = false;
			}
			catch (PCSException ex)
			{
				blnDataIsValid = false;
				PCSMessageBox.Show(ex.mCode, MessageBoxIcon.Error);
				try
				{
					Logger.LogMessage(ex.CauseException, METHOD_NAME, Level.ERROR);
					if(dtbWCCapacity.DataSet != null)
					{
						dtbWCCapacity.DataSet.RejectChanges();
					}
					else
					{
						DataSet dtsTemp = new DataSet();
						dtsTemp.Tables.Add(dtbWCCapacity);
						dtsTemp.RejectChanges();
					}
				}
				catch
				{
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
				}				
			}
			catch (Exception ex)
			{
				blnDataIsValid = false;
				PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxIcon.Error);
				try
				{
					Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
					if(dtbWCCapacity.DataSet != null)
					{
						dtbWCCapacity.DataSet.RejectChanges();
					}
					else
					{
						DataSet dtsTemp = new DataSet();
						dtsTemp.Tables.Add(dtbWCCapacity);
						dtsTemp.RejectChanges();
					}
				}
				catch
				{
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
				}				
			}
		}

		private void btnDelete_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnDelete_Click()";

			try
			{
				if(txtWorkCenter.Tag.ToString().Equals(ZERO_STRING) || dtbWCCapacity.Rows.Count == 0 || dgrdData.EditActive)
				{
					return;
				}
		
				if (PCSMessageBox.Show(ErrorCode.MESSAGE_DELETE_RECORD, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
				{					
					boWCCapacity.DeleteByWorkCenter(int.Parse(cboCCN.SelectedValue.ToString()), int.Parse(txtWorkCenter.Tag.ToString()));					
					ResetFormValues(true);
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

		private void btnClose_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}
		
		private void txtWorkCenter_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtWorkCenter_Validating()";

			try
			{
				//exit if empty				
				if(txtWorkCenter.Text.Length == 0)
				{
					ResetFormValues(true);
					return;
				}
				else if(!txtWorkCenter.Modified)
				{
					return;
				}

				e.Cancel = !SelectWorkCenter(METHOD_NAME, false);
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

		private void dgrdData_RowColChange(object sender, RowColChangeEventArgs e)
		{
			const string METHOD_NAME = THIS + ".dgrdData_RowColChange()";

			try
			{
				if ((dgrdData.DataSource != null)&&(dgrdData.Row != dgrdData.RowCount)
					&&(((DataTable)dgrdData.DataSource).Rows.Count != 0))
				{
					if(dgrdData.Columns[PRO_WCCapacityTable.WCCAPACITYID_FLD].Value.Equals(DBNull.Value)
						|| dgrdData.Columns[PRO_WCCapacityTable.WCCAPACITYID_FLD].Value.ToString().Equals(string.Empty))
					{
						dgrdData.Columns[PRO_WCCapacityTable.WCCAPACITYID_FLD].Value = --intWCCapacityFakeId;
						dgrdData.Columns[PRO_WCCapacityTable.CAPACITY_FLD].Value = Decimal.Zero;
						dgrdData.Columns[PRO_WCCapacityTable.WORKCENTERID_FLD].Value = txtWorkCenter.Tag;
						dgrdData.Columns[PRO_WCCapacityTable.CCNID_FLD].Value = cboCCN.SelectedValue;	
					}
				}
				if(dgrdData.Row != e.LastRow) 
				{
					GridRowChange();
					
				}
				if (dgrdData.DataSource != null)
				{
					dtbWCCapacityToStore = ((DataTable) dgrdData.DataSource).Copy();
					if (dtbWCCapacityToStore.Rows.Count > 0)
					{
						if ((dgrdData.Row != dgrdData.RowCount)
							||(dgrdData.Row == 0))
						{
							if (dtbWCCapacityToStore.Rows[dgrdData.Row].RowState != DataRowState.Deleted)
							{
								if ((dgrdData[dgrdData.Row, PRO_WCCapacityTable.BEGINDATE_FLD].ToString() != string.Empty)
									&&(dgrdData[dgrdData.Row, PRO_WCCapacityTable.ENDDATE_FLD].ToString() != string.Empty))
								{
									if (CheckDateToSeparate(GetDateOnly((DateTime)dgrdData[dgrdData.Row, PRO_WCCapacityTable.BEGINDATE_FLD]), GetDateOnly((DateTime)dgrdData[dgrdData.Row, PRO_WCCapacityTable.ENDDATE_FLD])))
									{
										btnSeparate.Enabled = true;		
									}
									else
										btnSeparate.Enabled = false;		
								}
							}
						}
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

		private void dgrdData_AfterInsert(object sender, EventArgs e)
		{
			const string METHOD_NAME = THIS + ".dgrdData_AfterInsert()";

			try
			{
				btnSave.Enabled = true;
				//blnHasSeparate = false;
				btnAdjust.Enabled = true;
				//btnSeparate.Enabled = true;
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
		
		private void dgrdShift_BeforeColEdit(object sender, BeforeColEditEventArgs e)
		{
			const string METHOD_NAME = THIS + ".dgrdShift_BeforeColEdit()";

			try
			{
				if(dgrdData.Row < 0 || dgrdData.RowCount <= 0)
				{
					e.Cancel = true;
					return;
				}

				if(dgrdData[dgrdData.Row, PRO_WCCapacityTable.WCCAPACITYID_FLD].Equals(DBNull.Value)
					|| dgrdData[dgrdData.Row, PRO_WCCapacityTable.WCCAPACITYID_FLD].ToString().Equals(string.Empty)
					)
				{
					e.Cancel = true;
					return;
				}

				if(int.Parse(dgrdShift.Columns[SHIFTPATTERN_COLUMN].Text) <= 0)
				{
					PCSMessageBox.Show(ErrorCode.MESSAGE_SHIFT_DOES_NOT_HAS_SHIFT_PATTERN, MessageBoxIcon.Exclamation);
					e.Cancel = true;
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

		private void dgrdShift_AfterColUpdate(object sender, ColEventArgs e)
		{
			const string METHOD_NAME = THIS + ".dgrdShift_AfterColUpdate()";

			try
			{				
				//only can check if exists parent
				if(dgrdData.RowCount == 0)
				{
					dgrdShift[dgrdShift.Row, SELECT_COLUMN] = false;
					return;
				}
				
				//return if selected row is invalid
				if(dgrdData[dgrdData.Row, PRO_WCCapacityTable.WCCAPACITYID_FLD].Equals(DBNull.Value)
				 || dgrdData[dgrdData.Row, PRO_WCCapacityTable.WCCAPACITYID_FLD].ToString().Equals(string.Empty)
				)
				{
					dgrdShift[dgrdShift.Row, SELECT_COLUMN] = false;
					return;
				}

				//Check if total working time exceed 24 hours
				string strCheckedShiftIDs = ZERO_STRING;
				for(int i =0; i< dgrdShift.RowCount; i++)
				{
					if(dgrdShift[i, SELECT_COLUMN].Equals(true))
					{
						strCheckedShiftIDs += DELIMITER_CHAR + dgrdShift[i, PRO_ShiftTable.SHIFTID_FLD].ToString();
					}
				}

				if(boWCCapacity.GetTotalWorkingTime(strCheckedShiftIDs) > 24 * 60)
				{
					if(dgrdShift.Bookmark >=0)
					{
						dgrdShift[dgrdShift.Bookmark, SELECT_COLUMN] = false;						
					}
					PCSMessageBox.Show(ErrorCode.MESSAGE_DCP_SHIFTPATTERN_WORKTIME_MUSTBE_SMALLER_24, MessageBoxIcon.Exclamation);
					dgrdShift.Focus();
					return;
				}

				//if check was clear then remove row
				object[] objPrimaryKey = new object[2];
				objPrimaryKey[0] = dgrdShift[dgrdShift.Row, PRO_ShiftCapacityTable.SHIFTID_FLD];
				objPrimaryKey[1] = dgrdData.Columns[PRO_WCCapacityTable.WCCAPACITYID_FLD].Value;
				DataRow drowExist = dtbShiftCapacity.Rows.Find(objPrimaryKey); 

				if(dgrdShift[dgrdShift.Row, SELECT_COLUMN].Equals(false))
				{
					if(drowExist != null)	drowExist.Delete();					
				}
				else if(drowExist == null) 
				{
					DataRow drowAdd = dtbShiftCapacity.NewRow();
					drowAdd[PRO_ShiftCapacityTable.SHIFTID_FLD] = dgrdShift[dgrdShift.Row, PRO_ShiftCapacityTable.SHIFTID_FLD];
					drowAdd[PRO_ShiftCapacityTable.WCCAPACITYID_FLD] = dgrdData.Columns[PRO_WCCapacityTable.WCCAPACITYID_FLD].Value;

					dtbShiftCapacity.Rows.Add(drowAdd);
				}

				//Calculate capacity
				CalculateCapacity();
				if (dgrdData.RowCount > 0)
				{
					int intDataRow = dgrdData.Row;
					dgrdData.Focus();
					dgrdData.Row = dgrdData.RowCount;
					dgrdData.Row = intDataRow;
					dgrdShift.Focus();
				}
				btnSave.Enabled = true;	
				blnHasSeparate = false;
				//btnSeparate.Enabled = false;
				btnAdjust.Enabled = false;
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
		
		private void dgrdData_BeforeDelete(object sender, C1.Win.C1TrueDBGrid.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".dgrdData_BeforeDelete()";

			try
			{
				if(dgrdData.Columns[PRO_WCCapacityTable.WCCAPACITYID_FLD].Value.Equals(DBNull.Value))
				{
					return;
				}

				//Remove Shift capacity
				DataRow[] arrRows = dtbShiftCapacity.Select(PRO_ShiftCapacityTable.WCCAPACITYID_FLD + "=" + dgrdData.Columns[PRO_WCCapacityTable.WCCAPACITYID_FLD].Value.ToString());

				for(int i =0; i < arrRows.Length; i++)
				{					
					arrRows[i].Delete();
				}

				//Add ID to deleted collection
				arlDeletedIDs.Add(dgrdData.Columns[PRO_WCCapacityTable.WCCAPACITYID_FLD].Value);				
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
		
		private void dgrdData_AfterDelete(object sender, EventArgs e)
		{
			const string METHOD_NAME = THIS + ".dgrdData_AfterDelete()";

			try
			{
				//Check grid
                if (dgrdData.RowCount > 0)
				{
					CheckShiftGrid(int.Parse(dgrdData[0, PRO_WCCapacityTable.WCCAPACITYID_FLD].ToString()));
				}
				else
				{
					CheckShiftGrid(0);
				}

				//Enable Save button
				btnSave.Enabled = true;
				blnHasSeparate = false;
				//btnSeparate.Enabled = false;
				btnAdjust.Enabled = false;
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

		private void dgrdData_OnAddNew(object sender, EventArgs e)
		{
			const string METHOD_NAME = THIS + ".dgrdData_OnAddNew()";

			try
			{	
				CheckShiftGrid(0);
//				dgrdData[dgrdData.Bookmark, PRO_WCCapacityTable.WCCAPACITYID_FLD] = --intWCCapacityFakeId;
//				dgrdData[dgrdData.Bookmark, PRO_WCCapacityTable.CAPACITY_FLD] = Decimal.Zero;
//				dgrdData[dgrdData.Bookmark, PRO_WCCapacityTable.WORKCENTERID_FLD] = txtWorkCenter.Tag;
//				dgrdData[dgrdData.Bookmark, PRO_WCCapacityTable.CCNID_FLD] = cboCCN.SelectedValue;
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
		
		private void dgrdData_AfterColUpdate(object sender, ColEventArgs e)
		{
			const string METHOD_NAME = THIS + ".dgrdData_AfterColUpdate()";
			try
			{
				if(e.Column.DataColumn == null)
				{
					return;
				}				
				
				//Force update data in grid
				//dgrdData.UpdateData();

				switch(e.Column.DataColumn.DataField)
				{
					case PRO_WCCapacityTable.CREWSIZE_FLD:
					case PRO_WCCapacityTable.MACHINENO_FLD:
					case PRO_WCCapacityTable.FACTOR_FLD:
					case PRO_WCCapacityTable.WCTYPE_FLD:
						//Calculate capacity
						CalculateCapacity();
						break;				
				}
				btnSeparate.Enabled = false;
				btnSave.Enabled = true;
				//btnSeparate.Enabled = false;
				blnHasSeparate = false;
				btnAdjust.Enabled = false;
			}
			catch(PCSException ex)
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
			catch(Exception ex)
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

		private void WorkCenterCapacity_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".WorkCenterCapacity_Closing()";
			try
			{	
				// if the form has been changed then ask to store database
				if(btnSave.Enabled) 
				{
					if(!dgrdData.EditActive)
					{
						DialogResult enumDialog = PCSMessageBox.Show(ErrorCode.MESSAGE_QUESTION_STORE_INTO_DATABASE,MessageBoxButtons.YesNoCancel,MessageBoxIcon.Question);
						if( enumDialog == DialogResult.Yes)
						{
							btnSave_Click(sender, e);
							e.Cancel = !blnDataIsValid;
						}
						else if( enumDialog == DialogResult.Cancel)//click Cancel button
						{
							e.Cancel = true;
						}
					}
					else
					{
						e.Cancel = true;
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
		
		private void txtWorkCenter_KeyDown(object sender, KeyEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtWorkCenter_KeyDown()";

			try
			{
				if ((e.KeyCode == Keys.F4) && (btnWorkCenterSearch.Enabled))
				{
					SelectWorkCenter(METHOD_NAME, true);
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

		
		private void WorkCenterCapacity_KeyDown(object sender, KeyEventArgs e)
		{
			const string METHOD_NAME = THIS + ".WorkCenterCapacity_KeyDown()";

			try
			{	
				switch (e.KeyCode)
				{
					case Keys.F12:						
						dgrdData.Col = dgrdData.Columns.IndexOf( dgrdData.Columns[PRO_WCCapacityTable.BEGINDATE_FLD]);					
						dgrdData.Row = dgrdData.RowCount;					
						dgrdData.Focus();
						break;
				}				
			}
			catch(PCSException ex)
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
			catch(Exception ex)
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
		
		private void dgrdData_BeforeColUpdate(object sender, BeforeColUpdateEventArgs e)
		{
			const string METHOD_NAME = THIS + ".dgrdData_BeforeColUpdate()";

			try
			{
				if(e.Column.DataColumn == null) return;

				//Turn on EditActive status of grid
				//dgrdData.EditActive = true;

				e.Cancel = !ProcessInputDataInGrid(e.Column.DataColumn.DataField, e.Column.DataColumn.Text.Trim());				
			}
			catch(PCSException ex)
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
			catch(Exception ex)
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
		
		private void dgrdShift_BeforeColUpdate(object sender, BeforeColUpdateEventArgs e)
		{
			const string METHOD_NAME = THIS + ".dgrdShift_BeforeColUpdate()";
			try
			{
//				if(int.Parse(dgrdShift.Columns[SHIFTPATTERN_COLUMN].Text) <= 0)
//				{
//					PCSMessageBox.Show(ErrorCode.MESSAGE_SHIFT_DOES_NOT_HAS_SHIFT_PATTERN, MessageBoxIcon.Exclamation);
//					e.Cancel = true;
//				}
			}
			catch(PCSException ex)
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
			catch(Exception ex)
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
		/// btnAdjust_Click
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Thursday, January 5 2006</date>
		private void btnAdjust_Click(object sender, System.EventArgs e)
		{
		const string METHOD_NAME = THIS + ".btnAdjust_Click()";
			try
			{
				if ((dtbWCCapacity.Rows.Count == 0)
					|| (dtbShiftTable.Rows.Count == 0))
				{
					return;
				}
				if (dgrdData.Row == dgrdData.RowCount) return;
				int intCycleID = 0;
				intCycleID = txtCycle.Tag != null ? int.Parse(txtCycle.Tag.ToString()) : 0;
				DCPAdjustment frmDCPAdjustment = new DCPAdjustment(dtbWCCapacityToStore, dtbShiftCapacity, txtWorkCenter.Text, int.Parse(txtWorkCenter.Tag.ToString()), intWCCapacityFakeId, arlDeletedIDs, intCycleID, (DateTime)dgrdData[dgrdData.Row, PRO_WCCapacityTable.BEGINDATE_FLD]);
				if (frmDCPAdjustment.ShowDialog() == DialogResult.OK)
				{
					dtbShiftCapacity = frmDCPAdjustment.dtbShiftCapacity.Copy();
					
					DataSet dstShift = new DataSet();
					dstShift = frmDCPAdjustment.dstShift.Copy();
					dtbShiftTable = dstShift.Tables[0].Copy();
					dgrdShift.Refresh();
					dtbWCCapacityToStore = frmDCPAdjustment.dtbWCCapacityToReturn.Copy();
					//dtbWCCapacity.DefaultView.Sort = PRO_WCCapacityTable.BEGINDATE_FLD;
					//GroupSameRows();
					dgrdData.DataSource = dtbWCCapacityToStore;
					FormatDataGrid();
					arlDeletedIDs = frmDCPAdjustment.arlDeletedIDs;
//					if ((intWCCapacityFakeId != frmDCPAdjustment.intFakeWCCapacityID)
//						||())
//					{
//						btnSave.Enabled = true;
//					}
					if (intDCPCycleID > 0)
					{
						FilterDataByCycle(intDCPCycleID);
					}
					btnSave.Enabled = true;
					//btnSeparate.Enabled = false;
					blnHasSeparate = false;
					blnHasAdjust = true;
					btnAdjust.Enabled = false;
					//btnSeparate.Enabled = true;
					intWCCapacityFakeId = frmDCPAdjustment.intFakeWCCapacityID;
					
				}
				//c1TrueDBGrid1.DataSource = dtbShiftCapacity;
			}
			catch(PCSException ex)
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
			catch(Exception ex)
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
		/// GroupSameRows
		/// </summary>
		/// <author>Trada</author>
		/// <date>Thursday, January 12 2006</date>
		private void GroupSameRows()
		{
			const string METHOD_NAME = THIS + ".GroupSameRows()";
			try
			{
				ArrayList arrListWCCapacityIDToDelete = new ArrayList();
				for (int i = 0; i < dtbWCCapacity.Rows.Count; i++)
				{
					for (int j = i + 1; j < dtbWCCapacity.Rows.Count; j++)
					{
						//Check rows have the same information
						bool blnIdentical = false;
						if ((int.Parse(dtbWCCapacity.Rows[i][PRO_WCCapacityTable.WCTYPE_FLD].ToString()) == int.Parse(dtbWCCapacity.Rows[j][PRO_WCCapacityTable.WCTYPE_FLD].ToString()))
							&& (decimal.Parse(dtbWCCapacity.Rows[i][PRO_WCCapacityTable.CAPACITY_FLD].ToString()) == decimal.Parse(dtbWCCapacity.Rows[j][PRO_WCCapacityTable.CAPACITY_FLD].ToString()))
							&& (decimal.Parse(dtbWCCapacity.Rows[i][PRO_WCCapacityTable.FACTOR_FLD].ToString()) == decimal.Parse(dtbWCCapacity.Rows[j][PRO_WCCapacityTable.FACTOR_FLD].ToString()))
							&& (int.Parse(dtbWCCapacity.Rows[i][PRO_WCCapacityTable.CCNID_FLD].ToString()) == int.Parse(dtbWCCapacity.Rows[j][PRO_WCCapacityTable.CCNID_FLD].ToString()))
							&& (int.Parse(dtbWCCapacity.Rows[i][PRO_WCCapacityTable.WORKCENTERID_FLD].ToString()) == int.Parse(dtbWCCapacity.Rows[j][PRO_WCCapacityTable.WORKCENTERID_FLD].ToString())))
						{
							if ((dtbWCCapacity.Rows[i][PRO_WCCapacityTable.MACHINENO_FLD] == DBNull.Value)
								&& (dtbWCCapacity.Rows[j][PRO_WCCapacityTable.MACHINENO_FLD] == DBNull.Value))
							{
								if ((dtbWCCapacity.Rows[i][PRO_WCCapacityTable.CREWSIZE_FLD] == DBNull.Value)
									&& (dtbWCCapacity.Rows[j][PRO_WCCapacityTable.CREWSIZE_FLD] == DBNull.Value))
								{
									blnIdentical = true;
								}
								else
								{
									if ((dtbWCCapacity.Rows[i][PRO_WCCapacityTable.CREWSIZE_FLD] != DBNull.Value)
										&& (dtbWCCapacity.Rows[j][PRO_WCCapacityTable.CREWSIZE_FLD] != DBNull.Value))
									{
										if ((decimal.Parse(dtbWCCapacity.Rows[i][PRO_WCCapacityTable.CREWSIZE_FLD].ToString()) == decimal.Parse(dtbWCCapacity.Rows[j][PRO_WCCapacityTable.CREWSIZE_FLD].ToString())))
										{
											blnIdentical = true;
										}
									}
								}
							}
							else
							{
								if ((dtbWCCapacity.Rows[i][PRO_WCCapacityTable.MACHINENO_FLD] != DBNull.Value)
									&& (dtbWCCapacity.Rows[j][PRO_WCCapacityTable.MACHINENO_FLD] != DBNull.Value))
								{
									if ((int.Parse(dtbWCCapacity.Rows[i][PRO_WCCapacityTable.MACHINENO_FLD].ToString()) == int.Parse(dtbWCCapacity.Rows[j][PRO_WCCapacityTable.MACHINENO_FLD].ToString())))
									{
										if ((dtbWCCapacity.Rows[i][PRO_WCCapacityTable.CREWSIZE_FLD] == DBNull.Value)
											&& (dtbWCCapacity.Rows[j][PRO_WCCapacityTable.CREWSIZE_FLD] == DBNull.Value))
										{
											blnIdentical = true;
										}
										else
										{
											if ((dtbWCCapacity.Rows[i][PRO_WCCapacityTable.CREWSIZE_FLD] != DBNull.Value)
												&& (dtbWCCapacity.Rows[j][PRO_WCCapacityTable.CREWSIZE_FLD] != DBNull.Value))
											{
												if ((decimal.Parse(dtbWCCapacity.Rows[i][PRO_WCCapacityTable.CREWSIZE_FLD].ToString()) == decimal.Parse(dtbWCCapacity.Rows[j][PRO_WCCapacityTable.CREWSIZE_FLD].ToString())))
												{
													blnIdentical = true;
												}
											}
										}
													
									}
								}
							}
						}
						//Two rows are the same
						if (blnIdentical)
						{
							if (GetDateOnly((DateTime)dtbWCCapacity.Rows[i][PRO_WCCapacityTable.ENDDATE_FLD]).AddDays(1).ToShortDateString() == GetDateOnly((DateTime) dtbWCCapacity.Rows[j][PRO_WCCapacityTable.BEGINDATE_FLD]).ToShortDateString())
							{
								dtbWCCapacity.Rows[i][PRO_WCCapacityTable.ENDDATE_FLD] = dtbWCCapacity.Rows[j][PRO_WCCapacityTable.ENDDATE_FLD];
								//Save ID of the second row
								if (arrListWCCapacityIDToDelete.Count > 0)
								{
									if (int.Parse(arrListWCCapacityIDToDelete[arrListWCCapacityIDToDelete.Count-1].ToString()) != int.Parse(dtbWCCapacity.Rows[j][PRO_WCCapacityTable.WCCAPACITYID_FLD].ToString()))
									{
										arrListWCCapacityIDToDelete.Add(int.Parse(dtbWCCapacity.Rows[j][PRO_WCCapacityTable.WCCAPACITYID_FLD].ToString()));
									}
								}
								else
								{
									arrListWCCapacityIDToDelete.Add(int.Parse(dtbWCCapacity.Rows[j][PRO_WCCapacityTable.WCCAPACITYID_FLD].ToString()));
								}
							}
							else
							{
								if (GetDateOnly((DateTime)dtbWCCapacity.Rows[i][PRO_WCCapacityTable.BEGINDATE_FLD]).AddDays(-1).ToShortDateString() == GetDateOnly((DateTime) dtbWCCapacity.Rows[j][PRO_WCCapacityTable.ENDDATE_FLD]).ToShortDateString())
								{
									dtbWCCapacity.Rows[i][PRO_WCCapacityTable.BEGINDATE_FLD] = dtbWCCapacity.Rows[j][PRO_WCCapacityTable.BEGINDATE_FLD];
									//Save ID of the second row
									if (arrListWCCapacityIDToDelete.Count > 0)
									{
										if (int.Parse(arrListWCCapacityIDToDelete[arrListWCCapacityIDToDelete.Count-1].ToString()) != int.Parse(dtbWCCapacity.Rows[j][PRO_WCCapacityTable.WCCAPACITYID_FLD].ToString()))
										{
											arrListWCCapacityIDToDelete.Add(int.Parse(dtbWCCapacity.Rows[j][PRO_WCCapacityTable.WCCAPACITYID_FLD].ToString()));
										}
									}
									else
									{
										arrListWCCapacityIDToDelete.Add(int.Parse(dtbWCCapacity.Rows[j][PRO_WCCapacityTable.WCCAPACITYID_FLD].ToString()));
									}
								}
							}	
						}
					}
				}
				//Delete the second rows 
				int intCount = dtbWCCapacity.Rows.Count;
				for (int j = 0; j < intCount; j++)
				{
					if (arrListWCCapacityIDToDelete.Count > 0)
					{
						for (int i = 0; i < arrListWCCapacityIDToDelete.Count; i++)
						{
							if (dtbWCCapacity.Rows[j][PRO_WCCapacityTable.WCCAPACITYID_FLD].ToString() == arrListWCCapacityIDToDelete[i].ToString())
							{
								dtbWCCapacity.Rows[j].Delete();
							}
							DataRow[] arrRows = dtbShiftCapacity.Select(PRO_ShiftCapacityTable.WCCAPACITYID_FLD + " = " + arrListWCCapacityIDToDelete[i].ToString());
							for(int k =0; k < arrRows.Length; k++)
							{					
								arrRows[k].Delete();
							}
							arlDeletedIDs.Add(arrListWCCapacityIDToDelete[i]);
						}
					}
				}				
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
		/// GetDateOnly
		/// </summary>
		/// <param name="pdtmInputDate"></param>
		/// <returns></returns>
		/// <author>Trada</author>
		/// <date>Thursday, January 12 2006</date>
		private DateTime GetDateOnly(DateTime pdtmInputDate)
		{
			const string METHOD_NAME = THIS + ".GetDateOnly()";
			try
			{
				DateTime dtmOutputDate = new DateTime(pdtmInputDate.Year, pdtmInputDate.Month, pdtmInputDate.Day);
				return dtmOutputDate;
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
		private void btnCycle_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnCycle_Click()";

			try
			{
				if(txtWorkCenter.Text.Trim() == string.Empty)
				{
					PCSMessageBox.Show(ErrorCode.MESSAGE_WCDISPATCH_SELECT_WORKCENTER, MessageBoxIcon.Exclamation);
					txtWorkCenter.Focus();
					return;
				}

				SelectDCPCycle(METHOD_NAME, true);
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

		private void txtCycle_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtCycle_KeyDown()";

			try
			{
				if ((e.KeyCode == Keys.F4) && (btnCycle.Enabled))
				{
					if(txtWorkCenter.Text.Trim() == string.Empty)
					{						
						//Clear Cycle's data information
						txtCycleRange.Text = string.Empty;
						dtmBeginCycleDate = DateTime.MinValue;
						dtmEndCycleDate = DateTime.MinValue;

						PCSMessageBox.Show(ErrorCode.MESSAGE_WCDISPATCH_SELECT_WORKCENTER, MessageBoxIcon.Exclamation);
						txtWorkCenter.Focus();
						return;
					}

					SelectDCPCycle(METHOD_NAME, true);
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

		private void txtCycle_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtCycle_Validating()";

			try
			{
				//exit if empty				
				if(txtCycle.Text.Length == 0)
				{	
					bool blnSelectWorkCenter = (txtWorkCenter.Text.Trim() != string.Empty);
					if(blnSelectWorkCenter)
					{
						dtbWCCapacityToStore = (DataTable) dgrdData.DataSource;
						//dtbWCCapacity.DefaultView.RowFilter = string.Empty;
						dtbWCCapacityToStore.DefaultView.RowFilter = string.Empty;
						
						dgrdData.Refresh();
						dgrdData.AllowAddNew = true;
						dgrdData.AllowUpdate = true;
						dgrdData.AllowDelete = true;

						//Check grid
						if(GetActualTotalRow(dtbWCCapacity) > 0)
						{
							CheckShiftGrid(int.Parse(dgrdData[0, PRO_WCCapacityTable.WCCAPACITYID_FLD].ToString()));
						}
						else
						{
							CheckShiftGrid(0);
						}
					}
					
					//Clear Cycle's data information
					txtCycleRange.Text = string.Empty;
					intDCPCycleID = 0;
					dtmBeginCycleDate = DateTime.MinValue;
					dtmEndCycleDate = DateTime.MinValue;

					return;
				}
				else if(!txtCycle.Modified)
				{
					return;
				}
				
				if(txtWorkCenter.Text.Trim() == string.Empty)
				{
					//Clear Cycle's data information
					txtCycleRange.Text = string.Empty;
					dtmBeginCycleDate = DateTime.MinValue;
					dtmEndCycleDate = DateTime.MinValue;
					
					//Show error message
					PCSMessageBox.Show(ErrorCode.MESSAGE_WCDISPATCH_SELECT_WORKCENTER, MessageBoxIcon.Exclamation);
					txtWorkCenter.Focus();
					return;
				}

				e.Cancel = !SelectDCPCycle(METHOD_NAME, false);
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

		#endregion Event Processing	

		/// <summary>
		/// This method used to check period which user has entered contained no working day
		/// </summary>
		/// <param name="pdtmBeginDate"></param>
		/// <param name="pdtmEndDate"></param>
		/// <returns>true if needed, false if not</returns>
		/// <author>Trada</author>
		/// <date>Friday, Feb 10 2006</date>
		private bool CheckDateToSeparate(DateTime pdtmBeginDate, DateTime pdtmEndDate)
		{
			const string METHOD_NAME = THIS + ".CheckDateToSeparate()";
			try
			{
				WorkCenterCapacityBO boWorkCenterCapacity = new WorkCenterCapacityBO();
				dstWorkingDayCalendar = boWorkCenterCapacity.GetWorkingDayCalendar();
				foreach (DataRow drow in dstWorkingDayCalendar.Tables[0].Rows)
				{
					if((GetDateOnly((DateTime)drow[MST_WorkingDayDetailTable.OFFDAY_FLD]) >= pdtmBeginDate) 
						&& (GetDateOnly((DateTime)drow[MST_WorkingDayDetailTable.OFFDAY_FLD]) <= pdtmEndDate))
					{
						return true;
					}
				}
				DateTime dtmDate = new DateTime();
				dtmDate = new DateTime(pdtmBeginDate.Year, pdtmBeginDate.Month, pdtmBeginDate.Day);
				while (dtmDate <= pdtmEndDate)
				{
					if (!boWorkCenterCapacity.CheckIfWorkingday(dtmDate, dtmDate.Year))
					{
						return true;
					}
					dtmDate = dtmDate.AddDays(1);
				}
				return false;
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
		/// Check if the date is belong to no working day table
		/// </summary>
		/// <param name="pdtmDate"></param>
		/// <returns></returns>
		/// <author>Trada</author>
		/// <date>Friday, Feb 10 2006</date>
		private bool CheckDateIsOffDay(DateTime pdtmDate)
		{
			const string METHOD_NAME = THIS + ".CheckDateIsOffDay()";
			try
			{

				foreach (DataRow drow in dstWorkingDayCalendar.Tables[0].Rows)
				{
					if(GetDateOnly((DateTime)drow[MST_WorkingDayDetailTable.OFFDAY_FLD]) == pdtmDate) 
					{
						return true;
					}
				}
				//Check if the date is no working day
				WorkCenterCapacityBO boWorkCenterCapacity = new WorkCenterCapacityBO();
                if (!boWorkCenterCapacity.CheckIfWorkingday(pdtmDate, pdtmDate.Year))
                {
                	return true;
                }
				return false;
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
		/// Check two rows are the same
		/// </summary>
		/// <param name="pdrowWCCapacityByEachDayOne"></param>
		/// <param name="pdrowWCCapacityByEachDayTwo"></param>
		/// <returns></returns>
		/// <author>Trada</author>
		/// <date>Friday, January 20 2006</date>
		private bool IsRowsTheSame(DataRow pdrowWCCapacityByEachDayOne, DataRow pdrowWCCapacityByEachDayTwo)
		{
			const string METHOD_NAME = THIS + ".IsRowsTheSame()";
			try
			{
				if ((int.Parse(pdrowWCCapacityByEachDayOne[PRO_WCCapacityTable.WCTYPE_FLD].ToString()) == int.Parse(pdrowWCCapacityByEachDayTwo[PRO_WCCapacityTable.WCTYPE_FLD].ToString()))
					&& (decimal.Parse(pdrowWCCapacityByEachDayOne[PRO_WCCapacityTable.CAPACITY_FLD].ToString()) == decimal.Parse(pdrowWCCapacityByEachDayTwo[PRO_WCCapacityTable.CAPACITY_FLD].ToString()))
					&& (decimal.Parse(pdrowWCCapacityByEachDayOne[PRO_WCCapacityTable.FACTOR_FLD].ToString()) == decimal.Parse(pdrowWCCapacityByEachDayTwo[PRO_WCCapacityTable.FACTOR_FLD].ToString()))
					&& (int.Parse(pdrowWCCapacityByEachDayOne[PRO_WCCapacityTable.CCNID_FLD].ToString()) == int.Parse(pdrowWCCapacityByEachDayTwo[PRO_WCCapacityTable.CCNID_FLD].ToString()))
					&& (int.Parse(pdrowWCCapacityByEachDayOne[PRO_WCCapacityTable.WORKCENTERID_FLD].ToString()) == int.Parse(pdrowWCCapacityByEachDayTwo[PRO_WCCapacityTable.WORKCENTERID_FLD].ToString())))
				{
					if ((pdrowWCCapacityByEachDayOne[PRO_WCCapacityTable.MACHINENO_FLD] == DBNull.Value)
						&& (pdrowWCCapacityByEachDayTwo[PRO_WCCapacityTable.MACHINENO_FLD] == DBNull.Value))
					{
						if ((pdrowWCCapacityByEachDayOne[PRO_WCCapacityTable.CREWSIZE_FLD] == DBNull.Value)
							&& (pdrowWCCapacityByEachDayTwo[PRO_WCCapacityTable.CREWSIZE_FLD] == DBNull.Value))
						{
							return true;
						}
						else
						{
							if ((pdrowWCCapacityByEachDayOne[PRO_WCCapacityTable.CREWSIZE_FLD] != DBNull.Value)
								&& (pdrowWCCapacityByEachDayTwo[PRO_WCCapacityTable.CREWSIZE_FLD] != DBNull.Value))
							{
								if ((decimal.Parse(pdrowWCCapacityByEachDayOne[PRO_WCCapacityTable.CREWSIZE_FLD].ToString()) == decimal.Parse(pdrowWCCapacityByEachDayTwo[PRO_WCCapacityTable.CREWSIZE_FLD].ToString())))
								{
									return true;
								}
							}
						}
					}
					else
					{
						if ((pdrowWCCapacityByEachDayOne[PRO_WCCapacityTable.MACHINENO_FLD] != DBNull.Value)
							&& (pdrowWCCapacityByEachDayTwo[PRO_WCCapacityTable.MACHINENO_FLD] != DBNull.Value))
						{
							if ((int.Parse(pdrowWCCapacityByEachDayOne[PRO_WCCapacityTable.MACHINENO_FLD].ToString()) == int.Parse(pdrowWCCapacityByEachDayTwo[PRO_WCCapacityTable.MACHINENO_FLD].ToString())))
							{
								if ((pdrowWCCapacityByEachDayOne[PRO_WCCapacityTable.CREWSIZE_FLD] == DBNull.Value)
									&& (pdrowWCCapacityByEachDayTwo[PRO_WCCapacityTable.CREWSIZE_FLD] == DBNull.Value))
								{
									return true;
								}
								else
								{
									if ((pdrowWCCapacityByEachDayOne[PRO_WCCapacityTable.CREWSIZE_FLD] != DBNull.Value)
										&& (pdrowWCCapacityByEachDayTwo[PRO_WCCapacityTable.CREWSIZE_FLD] != DBNull.Value))
									{
										if ((decimal.Parse(pdrowWCCapacityByEachDayOne[PRO_WCCapacityTable.CREWSIZE_FLD].ToString()) == decimal.Parse(pdrowWCCapacityByEachDayTwo[PRO_WCCapacityTable.CREWSIZE_FLD].ToString())))
										{
											return true;
										}
									}
								}
													
							}
						}
					}
				}
				return false;
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
		/// Check if the date is out of work day calendar was configged.
		/// </summary>
		/// <param name="pdtmDateToCheck"></param>
		/// <returns></returns>
		/// <author>Trada</author>
		/// <date>Tuesday, Mar 7 2006</date>
		private bool DateIsOutOfWorkDayCalendar(DateTime pdtmDateToCheck)
		{
			const string METHOD_NAME = THIS + ".DateIsOutOfWorkDayCalendar()";
			try
			{
				boWCCapacity = new WorkCenterCapacityBO();
				return boWCCapacity.CheckIfDateIsOutOfPeriod(pdtmDateToCheck, pdtmDateToCheck.Year);
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
		/// Separate by Line
		/// </summary>
		/// <param name="pintRow"></param>
		/// <author>Trada</author>
		/// <date>Friday, Feb 17 2006</date>
		private void SeparateLine(int pintRow)
		{
			const string METHOD_NAME = THIS + ".SeparateLine()";
			try
			{
				bool blnHasIncrement = false;
				DataRow drwNewRow = null;
				WorkCenterCapacityBO boWorkCenterCapacity = new WorkCenterCapacityBO();
                dtbWCCapacityToStore = new DataTable();
				//dtbWCCapacityToStore = dtbWCCapacity.Clone();
				//dtbWCCapacityToStore = ((DataTable) dgrdData.DataSource).Clone();
				dtbWCCapacityToStore = ((DataTable) dgrdData.DataSource).Copy();
                DataTable dtbWCCapacityByEachDay = new DataTable();
				dtbWCCapacity = ((DataTable) dgrdData.DataSource).Copy();

				dtbWCCapacityByEachDay = CreatWCCapacityByDayTableRow(dtbWCCapacityToStore.Rows[pintRow]);
				//Sort by day
				DataRow[] adrowWCCapacityByEachDay = dtbWCCapacityByEachDay.Select(string.Empty, DAY_FLD);
				//Check if user didn't configged Work day calendar
				if (adrowWCCapacityByEachDay.Length > 0)
				{
					string[] strParam = new string[1];
					if (DateIsOutOfWorkDayCalendar((GetDateOnly((DateTime) adrowWCCapacityByEachDay[0][DAY_FLD]))))
					{
						strParam[0] = ((DateTime) adrowWCCapacityByEachDay[0][DAY_FLD]).Year.ToString();
						PCSMessageBox.Show(ErrorCode.MESSAGE_DCP_SETTING_WORKING_CALENDAR, MessageBoxIcon.Warning, strParam);
						return;
					}
					if (DateIsOutOfWorkDayCalendar((GetDateOnly((DateTime) adrowWCCapacityByEachDay[adrowWCCapacityByEachDay.Length - 1][DAY_FLD]))))
					{
						strParam[0] = ((DateTime) adrowWCCapacityByEachDay[adrowWCCapacityByEachDay.Length - 1][DAY_FLD]).Year.ToString();
						PCSMessageBox.Show(ErrorCode.MESSAGE_DCP_SETTING_WORKING_CALENDAR, MessageBoxIcon.Warning, strParam);
						return;
					}
				}
				int i = 0;
				while (i < adrowWCCapacityByEachDay.Length)
				{

					int j = i;
					//if (adrowWCCapacityByEachDay[j].RowState == DataRowState.Deleted) continue;
					blnHasIncrement = false;
					while ((j < adrowWCCapacityByEachDay.Length - 1)
						&& !CheckDateIsOffDay(GetDateOnly((DateTime)adrowWCCapacityByEachDay[j][DAY_FLD]))
						&& ((GetDateOnly((DateTime)adrowWCCapacityByEachDay[j][DAY_FLD])).AddDays(1) ==  (GetDateOnly((DateTime)adrowWCCapacityByEachDay[j + 1][DAY_FLD])))
						&& IsRowsTheSame(adrowWCCapacityByEachDay[j], adrowWCCapacityByEachDay[j + 1]))
					{
						blnHasIncrement = true;
						j++;
					}
					
					if ((j >= adrowWCCapacityByEachDay.Length - 1)
						||((GetDateOnly((DateTime)adrowWCCapacityByEachDay[j][DAY_FLD])).AddDays(1) !=  (GetDateOnly((DateTime)adrowWCCapacityByEachDay[j + 1][DAY_FLD]))))
					{
						
						if (blnHasIncrement)
						{
							blnHasIncrement = false;
						}	
					}
					if (j >= 0) 
					{
						if (j == 0) 
						{
							if (!CheckDateIsOffDay(GetDateOnly((DateTime)adrowWCCapacityByEachDay[j][DAY_FLD])))
							{
								//add new row into WCCapacity Table
								drwNewRow = dtbWCCapacityToStore.NewRow();
								//Set date
								drwNewRow[PRO_WCCapacityTable.BEGINDATE_FLD] = adrowWCCapacityByEachDay[i][DAY_FLD];
								if (blnHasIncrement)
								{
									
									drwNewRow[PRO_WCCapacityTable.ENDDATE_FLD] = adrowWCCapacityByEachDay[j-1][DAY_FLD];
								}	
								else
								{
									DateTime dtmDate = new DateTime();
									dtmDate = (DateTime)adrowWCCapacityByEachDay[j][DAY_FLD];
									dtmDate =  new DateTime(dtmDate.Year, dtmDate.Month, dtmDate.Day);
									while (CheckDateIsOffDay(dtmDate))
									{
										dtmDate = dtmDate.AddDays(-1);
									}
									//drwNewRow[PRO_WCCapacityTable.ENDDATE_FLD] = adrowWCCapacityByEachDay[j][DAY_FLD];
									drwNewRow[PRO_WCCapacityTable.ENDDATE_FLD] = dtmDate;
								}
									
						
								for (int k = 2; k < dtbWCCapacity.Columns.Count; k++)
								{
									if (dtbWCCapacity.Columns[k].ColumnName == PRO_WCCapacityTable.WCCAPACITYID_FLD)
									{
										if (int.Parse(adrowWCCapacityByEachDay[i][PRO_WCCapacityTable.WCCAPACITYID_FLD].ToString()) > 0)
										{
											DataRow[] adrowWCCapacityToStore = dtbWCCapacityToStore.Select(PRO_WCCapacityTable.WCCAPACITYID_FLD + " = " + adrowWCCapacityByEachDay[i][PRO_WCCapacityTable.WCCAPACITYID_FLD].ToString());
											if (adrowWCCapacityToStore.Length > 0)
											{
												drwNewRow[PRO_WCCapacityTable.WCCAPACITYID_FLD] = --intWCCapacityFakeId;
											}
											else
												drwNewRow[PRO_WCCapacityTable.WCCAPACITYID_FLD] = adrowWCCapacityByEachDay[i][PRO_WCCapacityTable.WCCAPACITYID_FLD];
										}
										else
											drwNewRow[PRO_WCCapacityTable.WCCAPACITYID_FLD] = --intWCCapacityFakeId;
							
									}
									else
										drwNewRow[dtbWCCapacity.Columns[k].ColumnName] = adrowWCCapacityByEachDay[i][dtbWCCapacity.Columns[k].ColumnName];
								}
								dtbWCCapacityToStore.Rows.Add(drwNewRow);
								if (int.Parse(drwNewRow[PRO_WCCapacityTable.WCCAPACITYID_FLD].ToString()) < 0)
								{
									//update ShiftCapacity
									DataRow[] adrowShiftCapacity = dtbShiftCapacity.Select(PRO_WCCapacityTable.WCCAPACITYID_FLD + " = " + adrowWCCapacityByEachDay[i][PRO_WCCapacityTable.WCCAPACITYID_FLD].ToString());
									if (adrowShiftCapacity.Length > 0)
									{
										for (int m = 0; m < adrowShiftCapacity.Length; m++)
										{
											DataRow drowNew = dtbShiftCapacity.NewRow();
											drowNew[PRO_ShiftCapacityTable.SHIFTID_FLD] = adrowShiftCapacity[m][PRO_ShiftCapacityTable.SHIFTID_FLD];
											drowNew[PRO_ShiftCapacityTable.WCCAPACITYID_FLD] = intWCCapacityFakeId;
											dtbShiftCapacity.Rows.Add(drowNew);
										}
									}
								}
							}
						}
						else
						{
//							if (j != adrowWCCapacityByEachDay.Length - 1)
//							{
								//add new row into WCCapacity Table
								drwNewRow = dtbWCCapacityToStore.NewRow();
								//Set date
								drwNewRow[PRO_WCCapacityTable.BEGINDATE_FLD] = adrowWCCapacityByEachDay[i][DAY_FLD];
								if (blnHasIncrement)
								{
									drwNewRow[PRO_WCCapacityTable.ENDDATE_FLD] = adrowWCCapacityByEachDay[j-1][DAY_FLD];
								}	
								else
								{
									DateTime dtmDate = new DateTime();
									dtmDate = (DateTime)adrowWCCapacityByEachDay[j][DAY_FLD];
									dtmDate =  new DateTime(dtmDate.Year, dtmDate.Month, dtmDate.Day);
									if ((j != adrowWCCapacityByEachDay.Length - 1)
										||(CheckDateIsOffDay(dtmDate)))
									{
										while (CheckDateIsOffDay(dtmDate))
										{
											dtmDate = dtmDate.AddDays(-1);
										}
									}
									//drwNewRow[PRO_WCCapacityTable.ENDDATE_FLD] = adrowWCCapacityByEachDay[j][DAY_FLD];
									drwNewRow[PRO_WCCapacityTable.ENDDATE_FLD] = dtmDate;
								}
					
								for (int k = 2; k < dtbWCCapacity.Columns.Count; k++)
								{
									if (dtbWCCapacity.Columns[k].ColumnName == PRO_WCCapacityTable.WCCAPACITYID_FLD)
									{
										if (int.Parse(adrowWCCapacityByEachDay[i][PRO_WCCapacityTable.WCCAPACITYID_FLD].ToString()) > 0)
										{
											DataRow[] adrowWCCapacityToStore = dtbWCCapacityToStore.Select(PRO_WCCapacityTable.WCCAPACITYID_FLD + " = " + adrowWCCapacityByEachDay[i][PRO_WCCapacityTable.WCCAPACITYID_FLD].ToString());
											if (adrowWCCapacityToStore.Length > 0)
											{
												drwNewRow[PRO_WCCapacityTable.WCCAPACITYID_FLD] = --intWCCapacityFakeId;
											}
											else
												drwNewRow[PRO_WCCapacityTable.WCCAPACITYID_FLD] = adrowWCCapacityByEachDay[i][PRO_WCCapacityTable.WCCAPACITYID_FLD];
										}
										else
											drwNewRow[PRO_WCCapacityTable.WCCAPACITYID_FLD] = --intWCCapacityFakeId;
							
									}
									else
										drwNewRow[dtbWCCapacity.Columns[k].ColumnName] = adrowWCCapacityByEachDay[i][dtbWCCapacity.Columns[k].ColumnName];
								}
								dtbWCCapacityToStore.Rows.Add(drwNewRow);
								if (int.Parse(drwNewRow[PRO_WCCapacityTable.WCCAPACITYID_FLD].ToString()) < 0)
								{
									//update ShiftCapacity
									DataRow[] adrowShiftCapacity = dtbShiftCapacity.Select(PRO_WCCapacityTable.WCCAPACITYID_FLD + " = " + adrowWCCapacityByEachDay[i][PRO_WCCapacityTable.WCCAPACITYID_FLD].ToString());
									if (adrowShiftCapacity.Length > 0)
									{
										for (int m = 0; m < adrowShiftCapacity.Length; m++)
										{
											DataRow drowNew = dtbShiftCapacity.NewRow();
											drowNew[PRO_ShiftCapacityTable.SHIFTID_FLD] = adrowShiftCapacity[m][PRO_ShiftCapacityTable.SHIFTID_FLD];
											drowNew[PRO_ShiftCapacityTable.WCCAPACITYID_FLD] = intWCCapacityFakeId;
											dtbShiftCapacity.Rows.Add(drowNew);
										}
									}
								}
							//}
						}
					}
					
					bool blnIncrement = false;
					//while ((j < adrowWCCapacityByEachDay.Length - 1)
					while ((j < adrowWCCapacityByEachDay.Length)
						&& CheckDateIsOffDay(GetDateOnly((DateTime)adrowWCCapacityByEachDay[j][DAY_FLD])))
					{
						blnIncrement = true;
						j++;
					}
					if (blnIncrement)
					{
						i = j;
					}
					else
						i = j + 1;
				}
				//remove this row
				//dtbWCCapacityToStore.Rows[pintRow].RowError = NEED_TO_DELETE; 
				intWCCapacityIDNeedToDelete = int.Parse(dtbWCCapacityToStore.Rows[pintRow][PRO_WCCapacityTable.WCCAPACITYID_FLD].ToString());
				dtbWCCapacityToStore.Rows[pintRow].Delete();
				
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
		/// This method used to separate date base on working day calendar
		/// </summary>
		/// <returns></returns>
		/// <author>Trada</author>
		/// <date>Friday, Feb 10 2006</date>
		private void SeparateDate()
		{
			const string METHOD_NAME = THIS + ".SeparateDate()";
			try
			{
				bool blnHasIncrement = false;
				DataRow drwNewRow = null;
				WorkCenterCapacityBO boWorkCenterCapacity = new WorkCenterCapacityBO();
				dtbWCCapacityToStore = new DataTable();
				//dtbWCCapacityToStore = dtbWCCapacity.Clone();
				dtbWCCapacityToStore = ((DataTable) dgrdData.DataSource).Clone();
				DataTable dtbWCCapacityByEachDay = new DataTable();
				dtbWCCapacity = ((DataTable) dgrdData.DataSource).Copy();
				dtbWCCapacityByEachDay = CreatWCCapacityByDayTable(dtbWCCapacity);
				//Sort by day
				DataRow[] adrowWCCapacityByEachDay = dtbWCCapacityByEachDay.Select(string.Empty, DAY_FLD);
				int i = 0;
				while (i < adrowWCCapacityByEachDay.Length)
				{
					int j = i;
					blnHasIncrement = false;
					while ((j < adrowWCCapacityByEachDay.Length - 1)
							&& !CheckDateIsOffDay(GetDateOnly((DateTime)adrowWCCapacityByEachDay[j][DAY_FLD]))
							&& (((DateTime)adrowWCCapacityByEachDay[j][DAY_FLD]).AddDays(1) ==  ((DateTime)adrowWCCapacityByEachDay[j + 1][DAY_FLD]))
							&& IsRowsTheSame(adrowWCCapacityByEachDay[j], adrowWCCapacityByEachDay[j + 1]))
					{
						blnHasIncrement = true;
						j++;
					}
					
					if ((j >= adrowWCCapacityByEachDay.Length - 1)
						|| ((DateTime)adrowWCCapacityByEachDay[j][DAY_FLD]).AddDays(1) !=  ((DateTime)adrowWCCapacityByEachDay[j + 1][DAY_FLD]))
					{
						if (blnHasIncrement)
						{
							blnHasIncrement = false;
						}	
					}
					if (j >= 0) 
					{
						if (j == 0) 
						{
							if (!CheckDateIsOffDay(GetDateOnly((DateTime)adrowWCCapacityByEachDay[j][DAY_FLD])))
							{
								//add new row into WCCapacity Table
								drwNewRow = dtbWCCapacityToStore.NewRow();
								//Set date
								drwNewRow[PRO_WCCapacityTable.BEGINDATE_FLD] = adrowWCCapacityByEachDay[i][DAY_FLD];
								if (blnHasIncrement)
								{
									
									drwNewRow[PRO_WCCapacityTable.ENDDATE_FLD] = adrowWCCapacityByEachDay[j-1][DAY_FLD];
								}	
								else
								{
									DateTime dtmDate = new DateTime();
									dtmDate = (DateTime)adrowWCCapacityByEachDay[j][DAY_FLD];
									dtmDate =  new DateTime(dtmDate.Year, dtmDate.Month, dtmDate.Day);
									while (CheckDateIsOffDay(dtmDate))
									{
										dtmDate = dtmDate.AddDays(-1);
									}
									//drwNewRow[PRO_WCCapacityTable.ENDDATE_FLD] = adrowWCCapacityByEachDay[j][DAY_FLD];
									drwNewRow[PRO_WCCapacityTable.ENDDATE_FLD] = dtmDate;
								}
									
						
								for (int k = 2; k < dtbWCCapacity.Columns.Count; k++)
								{
									if (dtbWCCapacity.Columns[k].ColumnName == PRO_WCCapacityTable.WCCAPACITYID_FLD)
									{
										if (int.Parse(adrowWCCapacityByEachDay[i][PRO_WCCapacityTable.WCCAPACITYID_FLD].ToString()) > 0)
										{
											DataRow[] adrowWCCapacityToStore = dtbWCCapacityToStore.Select(PRO_WCCapacityTable.WCCAPACITYID_FLD + " = " + adrowWCCapacityByEachDay[i][PRO_WCCapacityTable.WCCAPACITYID_FLD].ToString());
											if (adrowWCCapacityToStore.Length > 0)
											{
												drwNewRow[PRO_WCCapacityTable.WCCAPACITYID_FLD] = --intWCCapacityFakeId;
											}
											else
												drwNewRow[PRO_WCCapacityTable.WCCAPACITYID_FLD] = adrowWCCapacityByEachDay[i][PRO_WCCapacityTable.WCCAPACITYID_FLD];
										}
										else
											drwNewRow[PRO_WCCapacityTable.WCCAPACITYID_FLD] = --intWCCapacityFakeId;
							
									}
									else
										drwNewRow[dtbWCCapacity.Columns[k].ColumnName] = adrowWCCapacityByEachDay[i][dtbWCCapacity.Columns[k].ColumnName];
								}
								dtbWCCapacityToStore.Rows.Add(drwNewRow);
								if (int.Parse(drwNewRow[PRO_WCCapacityTable.WCCAPACITYID_FLD].ToString()) < 0)
								{
									//update ShiftCapacity
									DataRow[] adrowShiftCapacity = dtbShiftCapacity.Select(PRO_WCCapacityTable.WCCAPACITYID_FLD + " = " + adrowWCCapacityByEachDay[i][PRO_WCCapacityTable.WCCAPACITYID_FLD].ToString());
									if (adrowShiftCapacity.Length > 0)
									{
										for (int m = 0; m < adrowShiftCapacity.Length; m++)
										{
											DataRow drowNew = dtbShiftCapacity.NewRow();
											drowNew[PRO_ShiftCapacityTable.SHIFTID_FLD] = adrowShiftCapacity[m][PRO_ShiftCapacityTable.SHIFTID_FLD];
											drowNew[PRO_ShiftCapacityTable.WCCAPACITYID_FLD] = intWCCapacityFakeId;
											dtbShiftCapacity.Rows.Add(drowNew);
										}
									}
								}
							}
						}
						else
						{
							//add new row into WCCapacity Table
							drwNewRow = dtbWCCapacityToStore.NewRow();
							//Set date
							drwNewRow[PRO_WCCapacityTable.BEGINDATE_FLD] = adrowWCCapacityByEachDay[i][DAY_FLD];
							if (blnHasIncrement)
							{
								drwNewRow[PRO_WCCapacityTable.ENDDATE_FLD] = adrowWCCapacityByEachDay[j-1][DAY_FLD];
							}	
							else
							{
								DateTime dtmDate = new DateTime();
								dtmDate = (DateTime)adrowWCCapacityByEachDay[j][DAY_FLD];
								dtmDate =  new DateTime(dtmDate.Year, dtmDate.Month, dtmDate.Day);
								while (CheckDateIsOffDay(dtmDate))
								{
									dtmDate = dtmDate.AddDays(-1);
								}
								//drwNewRow[PRO_WCCapacityTable.ENDDATE_FLD] = adrowWCCapacityByEachDay[j][DAY_FLD];
								drwNewRow[PRO_WCCapacityTable.ENDDATE_FLD] = dtmDate;
							}
					
							for (int k = 2; k < dtbWCCapacity.Columns.Count; k++)
							{
								if (dtbWCCapacity.Columns[k].ColumnName == PRO_WCCapacityTable.WCCAPACITYID_FLD)
								{
									if (int.Parse(adrowWCCapacityByEachDay[i][PRO_WCCapacityTable.WCCAPACITYID_FLD].ToString()) > 0)
									{
										DataRow[] adrowWCCapacityToStore = dtbWCCapacityToStore.Select(PRO_WCCapacityTable.WCCAPACITYID_FLD + " = " + adrowWCCapacityByEachDay[i][PRO_WCCapacityTable.WCCAPACITYID_FLD].ToString());
										if (adrowWCCapacityToStore.Length > 0)
										{
											drwNewRow[PRO_WCCapacityTable.WCCAPACITYID_FLD] = --intWCCapacityFakeId;
										}
										else
											drwNewRow[PRO_WCCapacityTable.WCCAPACITYID_FLD] = adrowWCCapacityByEachDay[i][PRO_WCCapacityTable.WCCAPACITYID_FLD];
									}
									else
										drwNewRow[PRO_WCCapacityTable.WCCAPACITYID_FLD] = --intWCCapacityFakeId;
							
								}
								else
									drwNewRow[dtbWCCapacity.Columns[k].ColumnName] = adrowWCCapacityByEachDay[i][dtbWCCapacity.Columns[k].ColumnName];
							}
							dtbWCCapacityToStore.Rows.Add(drwNewRow);
							if (int.Parse(drwNewRow[PRO_WCCapacityTable.WCCAPACITYID_FLD].ToString()) < 0)
							{
								//update ShiftCapacity
								DataRow[] adrowShiftCapacity = dtbShiftCapacity.Select(PRO_WCCapacityTable.WCCAPACITYID_FLD + " = " + adrowWCCapacityByEachDay[i][PRO_WCCapacityTable.WCCAPACITYID_FLD].ToString());
								if (adrowShiftCapacity.Length > 0)
								{
									for (int m = 0; m < adrowShiftCapacity.Length; m++)
									{
										DataRow drowNew = dtbShiftCapacity.NewRow();
										drowNew[PRO_ShiftCapacityTable.SHIFTID_FLD] = adrowShiftCapacity[m][PRO_ShiftCapacityTable.SHIFTID_FLD];
										drowNew[PRO_ShiftCapacityTable.WCCAPACITYID_FLD] = intWCCapacityFakeId;
										dtbShiftCapacity.Rows.Add(drowNew);
									}
								}
							}
						}
					}
					
					bool blnIncrement = false;
					while ((j < adrowWCCapacityByEachDay.Length - 1)
						&& CheckDateIsOffDay(GetDateOnly((DateTime)adrowWCCapacityByEachDay[j][DAY_FLD])))
					{
						blnIncrement = true;
						j++;
					}
					if (blnIncrement)
					{
						i = j;
					}
					else
						i = j + 1;
				}
				
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
		/// CreatWCCapactiyByDayTable by one row
		/// </summary>
		/// <param name="pdrowWCCapacity"></param>
		/// <returns></returns>
		/// <author>Trada</author>
		/// <date>Friday, January 6 2006</date>
		private DataTable CreatWCCapacityByDayTableRow(DataRow pdrowWCCapacity)
		{
			const string METHOD_NAME = THIS + ".CreatWCCapactiyByDayTable()";
			try
			{
				DataTable dtbOutput = new DataTable();
				//add Day columns
				dtbOutput.Columns.Add(DAY_FLD, typeof(DateTime));
				//add all fields of dtbWCCapacity in to dtbOutput, except StartDate and EndDate. 
				for (int i = 2; i < dtbWCCapacity.Columns.Count; i++)
				{
					dtbOutput.Columns.Add(dtbWCCapacity.Columns[i].ColumnName);
				}
				//add New ID
				dtbOutput.Columns.Add(NEWID_FLD, typeof(int));
				dtbOutput.Columns[NEWID_FLD].AutoIncrement = true;
				dtbOutput.Columns[NEWID_FLD].AutoIncrementSeed = 1;
				dtbOutput.Columns[NEWID_FLD].AutoIncrementStep = 1;
				//add Working Day 
				dtbOutput.Columns.Add(MST_WorkingDayDetailTable.OFFDAY_FLD, typeof(bool));
				if (pdrowWCCapacity.RowState != DataRowState.Deleted)
				{
					int intLoop = ((DateTime)pdrowWCCapacity[PRO_WCCapacityTable.ENDDATE_FLD] - (DateTime)pdrowWCCapacity[PRO_WCCapacityTable.BEGINDATE_FLD]).Days;
					for (int j = 0; j <= intLoop; j++)
					{
						DataRow drowOutput = dtbOutput.NewRow();	
						drowOutput[DAY_FLD] = ((DateTime)pdrowWCCapacity[PRO_WCCapacityTable.BEGINDATE_FLD]).AddDays(j);
						for (int i = 2; i < dtbWCCapacity.Columns.Count; i++)
						{
							drowOutput[i-1] = pdrowWCCapacity[i];
						}		
						drowOutput[MST_WorkingDayDetailTable.OFFDAY_FLD] = false;
						dtbOutput.Rows.Add(drowOutput);
					}
				}
				return dtbOutput;
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
		/// CreatWCCapactiyByDayTable
		/// </summary>
		/// <param name="pdtbWCCapacity"></param>
		/// <returns></returns>
		/// <author>Trada</author>
		/// <date>Friday, January 6 2006</date>
		private DataTable CreatWCCapacityByDayTable(DataTable pdtbWCCapacity)
		{
			const string METHOD_NAME = THIS + ".CreatWCCapactiyByDayTable()";
			try
			{
				DataTable dtbOutput = new DataTable();
				//add Day columns
				dtbOutput.Columns.Add(DAY_FLD, typeof(DateTime));
				//add all fields of dtbWCCapacity in to dtbOutput, except StartDate and EndDate. 
				for (int i = 2; i < pdtbWCCapacity.Columns.Count; i++)
				{
					dtbOutput.Columns.Add(pdtbWCCapacity.Columns[i].ColumnName);
				}
				//add New ID
				dtbOutput.Columns.Add(NEWID_FLD, typeof(int));
				dtbOutput.Columns[NEWID_FLD].AutoIncrement = true;
				dtbOutput.Columns[NEWID_FLD].AutoIncrementSeed = 1;
				dtbOutput.Columns[NEWID_FLD].AutoIncrementStep = 1;
				//add Working Day 
				dtbOutput.Columns.Add(MST_WorkingDayDetailTable.OFFDAY_FLD, typeof(bool));
				foreach (DataRow drow in pdtbWCCapacity.Rows)
				{
					if (drow.RowState != DataRowState.Deleted)
					{
						int intLoop = ((DateTime)drow[PRO_WCCapacityTable.ENDDATE_FLD] - (DateTime)drow[PRO_WCCapacityTable.BEGINDATE_FLD]).Days;
						for (int j = 0; j <= intLoop; j++)
						{
							DataRow drowOutput = dtbOutput.NewRow();	
							drowOutput[DAY_FLD] = ((DateTime)drow[PRO_WCCapacityTable.BEGINDATE_FLD]).AddDays(j);
							for (int i = 2; i < pdtbWCCapacity.Columns.Count; i++)
							{
								drowOutput[i-1] = drow[i];
							}		
							drowOutput[MST_WorkingDayDetailTable.OFFDAY_FLD] = false;
							dtbOutput.Rows.Add(drowOutput);
						}
					}
				}
				return dtbOutput;
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
		/// Remove WCCapacityID is not use
		/// </summary>
		/// <author>Trada</author>
		/// <date>Thursday, January 26 2006</date>
		private void RemoveWCCapacityID()
		{
			const string METHOD_NAME = THIS + ".RemoveWCCapacityID()";
			try
			{
				DataRow[] adrowShiftCapacity = dtbShiftCapacity.Select(string.Empty, PRO_WCCapacityTable.WCCAPACITYID_FLD);
				if (adrowShiftCapacity.Length > 0)
				{
					for (int i = 0; i < adrowShiftCapacity.Length; i++)
					{
						DataRow[] adrowWCCapacityToReturn = dtbWCCapacityToStore.Select(PRO_WCCapacityTable.WCCAPACITYID_FLD + " = " + adrowShiftCapacity[i][PRO_WCCapacityTable.WCCAPACITYID_FLD].ToString());
						if (adrowWCCapacityToReturn.Length == 0)
						{
							adrowShiftCapacity[i].Delete();
						}
					}
				}
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
		/// btnSeparate_Click
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Friday, Feb 10 2006</date>
		private void btnSeparate_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnSeparate_Click()";
			try
			{	
				//validate data
				blnDataIsValid = ValidateData() && !dgrdData.EditActive;
				if(!blnDataIsValid) return;
				//Check if user doesn't need to separate
				bool blnNeedToSeparate = false;
				for (int i = 0; i < dgrdData.RowCount; i++)
				{
					if (CheckDateToSeparate(GetDateOnly((DateTime)dgrdData[i, PRO_WCCapacityTable.BEGINDATE_FLD]),GetDateOnly((DateTime)dgrdData[i, PRO_WCCapacityTable.ENDDATE_FLD])))
					{
						blnNeedToSeparate = true;
					}
				}
				if (!blnNeedToSeparate)
				{
					return;
				}
				if (dgrdData.RowCount > 0) 
				{
					WorkCenterCapacityBO boWorkCenterCapacity = new WorkCenterCapacityBO();
					dstWorkingDayCalendar = boWorkCenterCapacity.GetWorkingDayCalendar();
					if (dgrdData.Row == (dgrdData.RowCount))
						return;
					//check if this line doesn't need to separate
					if (!CheckDateToSeparate(GetDateOnly((DateTime)dgrdData[dgrdData.Row, PRO_WCCapacityTable.BEGINDATE_FLD]),GetDateOnly((DateTime)dgrdData[dgrdData.Row, PRO_WCCapacityTable.ENDDATE_FLD])))
					{
						return;
					}
					SeparateLine(dgrdData.Row);
					dgrdData.DataSource = dtbWCCapacityToStore;
					FormatDataGrid();
					btnSave.Enabled = true;
					blnHasSeparate = true;
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
		/// btnSave_EnabledChanged
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Thursday, Feb 16 2006</date>
		private void btnSave_EnabledChanged(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnSave_EnabledChanged()";
			try
			{
				btnAdjust.Enabled  = !btnSave.Enabled;
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
		/// dgrdData_KeyDown
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Tuesday, Feb 28 2006</date>
		private void dgrdData_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			const string METHOD_NAME = THIS + ".dgrdData_KeyDown()";
			try
			{
                if (e.KeyCode == Keys.Delete)
                {
                    //Before delete
                    //store the index of selectrows
                    for (int i = 0; i < dgrdData.SelectedRows.Count; i++)
                    {
                        if (dgrdData[int.Parse(dgrdData.SelectedRows[i].ToString()), PRO_ShiftCapacityTable.WCCAPACITYID_FLD].ToString() != string.Empty)
                        {
                            DataRow[] arrRows = dtbShiftCapacity.Select(PRO_ShiftCapacityTable.WCCAPACITYID_FLD + " = " +
                                                                        dgrdData[int.Parse(dgrdData.SelectedRows[i].ToString()),PRO_ShiftCapacityTable.WCCAPACITYID_FLD].ToString());

                            //Remove Shift capacity
                            for (int j = 0; j < arrRows.Length; j++)
                            {
                                arrRows[j].Delete();
                            }
                            //Add ID to deleted collection
                            arlDeletedIDs.Add(int.Parse(dgrdData[int.Parse(dgrdData.SelectedRows[i].ToString()),PRO_ShiftCapacityTable.WCCAPACITYID_FLD].ToString()));
                        }
                    }

                    FormControlComponents.DeleteMultiRowsOnTrueDBGrid(dgrdData);
                    dtbWCCapacityToStore = (DataTable) dgrdData.DataSource;
                    if (dgrdData.RowCount > 0)
                    {
                        CheckShiftGrid(int.Parse(dgrdData[0, PRO_WCCapacityTable.WCCAPACITYID_FLD].ToString()));
                    }
                    else
                    {
                        CheckShiftGrid(0);
                    }

                    //Enable Save button
                    btnSave.Enabled = true;
                    blnHasSeparate = false;
                    //btnSeparate.Enabled = false;
                    btnAdjust.Enabled = false;
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

		private void dgrdData_Leave(object sender, System.EventArgs e)
		{
		
		}

		private void dgrdShift_Leave(object sender, System.EventArgs e)
		{
			// dgrdData.UpdateData(); //.Update();

		}

		
	}
}