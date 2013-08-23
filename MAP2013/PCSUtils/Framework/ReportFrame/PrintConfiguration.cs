using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.IO;

using C1.Win.C1TrueDBGrid;
//Using PCS's namespaces
using PCSComUtils.Framework.ReportFrame.BO;
using PCSComUtils.Framework.ReportFrame.DS;
using PCSComUtils.Common;
using PCSComUtils.Common.BO;
using PCSComUtils.MasterSetup.DS;
using PCSComUtils.PCSExc;
using PCSUtils.Log;
using PCSUtils.Utils;

namespace PCSUtils.Utils
{
	/// <summary>
	/// Summary description for PrintConfigure.
	/// </summary>
	public class PrintConfiguration : System.Windows.Forms.Form
	{
		private const string THIS = "PCSUtils.Utils.PrintConfiguration";
		private const string ZERO_STRING = "0";
		private const string REPORT_LAYOUT_FILE_FILTER = "XML Report File|*.xml";

		#region Declaration
		private C1.Win.C1TrueDBGrid.C1TrueDBGrid dgrdData;
		private System.Windows.Forms.Button btnHelp;
		private System.Windows.Forms.Button btnSave;
		private System.Windows.Forms.Button btnClose;
		private C1.Win.C1Input.C1NumericEdit numValue;
		
		private bool blnDataIsValid = false;
		private DataTable dtbGridLayOut;
		private DataTable dtbPrinConfig;		
		private PrintConfigurationBO boPrintConfig = new PrintConfigurationBO();
		private string mstrReportDefFolder = Application.StartupPath + "\\" +Constants.REPORT_DEFINITION_STORE_LOCATION;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		
		#endregion Declaration
		private System.Windows.Forms.OpenFileDialog dlgOpenReportFile;

		#region Constructor, Destructor
		
		string mstrFormName;

		public PrintConfiguration()
		{			
			// Required for Windows Form Designer support		
			InitializeComponent();
			// assign form name's value to empty
			mstrFormName = string.Empty;
		}

		public PrintConfiguration(string pstrFormName)
		{
			InitializeComponent();
			// assign form name's value
			mstrFormName = pstrFormName;
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
					
		#endregion Constructor
			
		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>		
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(PrintConfiguration));
			this.dgrdData = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
			this.btnHelp = new System.Windows.Forms.Button();
			this.btnSave = new System.Windows.Forms.Button();
			this.btnClose = new System.Windows.Forms.Button();
			this.dlgOpenReportFile = new System.Windows.Forms.OpenFileDialog();
			this.numValue = new C1.Win.C1Input.C1NumericEdit();
			((System.ComponentModel.ISupportInitialize)(this.dgrdData)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numValue)).BeginInit();
			this.SuspendLayout();
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
			this.dgrdData.ButtonClick += new C1.Win.C1TrueDBGrid.ColEventHandler(this.dgrdData_ButtonClick);
			this.dgrdData.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgrdData_KeyDown);
			this.dgrdData.AfterDelete += new System.EventHandler(dgrdData_AfterDelete);
			this.dgrdData.Change += new System.EventHandler(this.dgrdData_Change);
			this.dgrdData.PropBag = resources.GetString("dgrdData.PropBag");
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
			this.btnHelp.Click += new System.EventHandler(this.btnHelp_Click);
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
			// dlgOpenReportFile
			// 
			this.dlgOpenReportFile.Filter = resources.GetString("dlgOpenReportFile.Filter");
			this.dlgOpenReportFile.RestoreDirectory = true;
			this.dlgOpenReportFile.Title = resources.GetString("dlgOpenReportFile.Title");
			// 
			// numValue
			// 
			this.numValue.AcceptsEscape = ((bool)(resources.GetObject("numValue.AcceptsEscape")));
			this.numValue.AccessibleDescription = resources.GetString("numValue.AccessibleDescription");
			this.numValue.AccessibleName = resources.GetString("numValue.AccessibleName");
			this.numValue.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("numValue.Anchor")));
			this.numValue.AutoSize = ((bool)(resources.GetObject("numValue.AutoSize")));
			this.numValue.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("numValue.BackgroundImage")));
			this.numValue.BorderStyle = ((System.Windows.Forms.BorderStyle)(resources.GetObject("numValue.BorderStyle")));
			// 
			// numValue.Calculator
			// 
			this.numValue.Calculator.AccessibleDescription = resources.GetString("numValue.Calculator.AccessibleDescription");
			this.numValue.Calculator.AccessibleName = resources.GetString("numValue.Calculator.AccessibleName");
			this.numValue.Calculator.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("numValue.Calculator.BackgroundImage")));
			this.numValue.Calculator.ButtonFlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("numValue.Calculator.ButtonFlatStyle")));
			this.numValue.Calculator.DisplayFormat = resources.GetString("numValue.Calculator.DisplayFormat");
			this.numValue.Calculator.Font = ((System.Drawing.Font)(resources.GetObject("numValue.Calculator.Font")));
			this.numValue.Calculator.FormatOnClose = ((bool)(resources.GetObject("numValue.Calculator.FormatOnClose")));
			this.numValue.Calculator.StoredFormat = resources.GetString("numValue.Calculator.StoredFormat");
			this.numValue.Calculator.UIStrings.Content = ((string[])(resources.GetObject("numValue.Calculator.UIStrings.Content")));
			this.numValue.CaseSensitive = ((bool)(resources.GetObject("numValue.CaseSensitive")));
			this.numValue.Culture = ((int)(resources.GetObject("numValue.Culture")));
			this.numValue.CustomFormat = resources.GetString("numValue.CustomFormat");
			this.numValue.DataType = ((System.Type)(resources.GetObject("numValue.DataType")));
			this.numValue.DisplayFormat.CustomFormat = resources.GetString("numValue.DisplayFormat.CustomFormat");
			this.numValue.DisplayFormat.FormatType = ((C1.Win.C1Input.FormatTypeEnum)(resources.GetObject("numValue.DisplayFormat.FormatType")));
			this.numValue.DisplayFormat.Inherit = ((C1.Win.C1Input.FormatInfoInheritFlags)(resources.GetObject("numValue.DisplayFormat.Inherit")));
			this.numValue.DisplayFormat.NullText = resources.GetString("numValue.DisplayFormat.NullText");
			this.numValue.DisplayFormat.TrimEnd = ((bool)(resources.GetObject("numValue.DisplayFormat.TrimEnd")));
			this.numValue.DisplayFormat.TrimStart = ((bool)(resources.GetObject("numValue.DisplayFormat.TrimStart")));
			this.numValue.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("numValue.Dock")));
			this.numValue.DropDownFormAlign = ((C1.Win.C1Input.DropDownFormAlignmentEnum)(resources.GetObject("numValue.DropDownFormAlign")));
			this.numValue.EditFormat.CustomFormat = resources.GetString("numValue.EditFormat.CustomFormat");
			this.numValue.EditFormat.FormatType = ((C1.Win.C1Input.FormatTypeEnum)(resources.GetObject("numValue.EditFormat.FormatType")));
			this.numValue.EditFormat.Inherit = ((C1.Win.C1Input.FormatInfoInheritFlags)(resources.GetObject("numValue.EditFormat.Inherit")));
			this.numValue.EditFormat.NullText = resources.GetString("numValue.EditFormat.NullText");
			this.numValue.EditFormat.TrimEnd = ((bool)(resources.GetObject("numValue.EditFormat.TrimEnd")));
			this.numValue.EditFormat.TrimStart = ((bool)(resources.GetObject("numValue.EditFormat.TrimStart")));
			this.numValue.EditMask = resources.GetString("numValue.EditMask");
			this.numValue.EmptyAsNull = ((bool)(resources.GetObject("numValue.EmptyAsNull")));
			this.numValue.Enabled = ((bool)(resources.GetObject("numValue.Enabled")));
			this.numValue.ErrorInfo.BeepOnError = ((bool)(resources.GetObject("numValue.ErrorInfo.BeepOnError")));
			this.numValue.ErrorInfo.ErrorMessage = resources.GetString("numValue.ErrorInfo.ErrorMessage");
			this.numValue.ErrorInfo.ErrorMessageCaption = resources.GetString("numValue.ErrorInfo.ErrorMessageCaption");
			this.numValue.ErrorInfo.ShowErrorMessage = ((bool)(resources.GetObject("numValue.ErrorInfo.ShowErrorMessage")));
			this.numValue.ErrorInfo.ValueOnError = ((object)(resources.GetObject("numValue.ErrorInfo.ValueOnError")));
			this.numValue.Font = ((System.Drawing.Font)(resources.GetObject("numValue.Font")));
			this.numValue.FormatType = ((C1.Win.C1Input.FormatTypeEnum)(resources.GetObject("numValue.FormatType")));
			this.numValue.GapHeight = ((int)(resources.GetObject("numValue.GapHeight")));
			this.numValue.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("numValue.ImeMode")));
			this.numValue.Increment = ((object)(resources.GetObject("numValue.Increment")));
			this.numValue.InitialSelection = ((C1.Win.C1Input.InitialSelectionEnum)(resources.GetObject("numValue.InitialSelection")));
			this.numValue.Location = ((System.Drawing.Point)(resources.GetObject("numValue.Location")));
			this.numValue.MaskInfo.AutoTabWhenFilled = ((bool)(resources.GetObject("numValue.MaskInfo.AutoTabWhenFilled")));
			this.numValue.MaskInfo.CaseSensitive = ((bool)(resources.GetObject("numValue.MaskInfo.CaseSensitive")));
			this.numValue.MaskInfo.CopyWithLiterals = ((bool)(resources.GetObject("numValue.MaskInfo.CopyWithLiterals")));
			this.numValue.MaskInfo.EditMask = resources.GetString("numValue.MaskInfo.EditMask");
			this.numValue.MaskInfo.EmptyAsNull = ((bool)(resources.GetObject("numValue.MaskInfo.EmptyAsNull")));
			this.numValue.MaskInfo.ErrorMessage = resources.GetString("numValue.MaskInfo.ErrorMessage");
			this.numValue.MaskInfo.Inherit = ((C1.Win.C1Input.MaskInfoInheritFlags)(resources.GetObject("numValue.MaskInfo.Inherit")));
			this.numValue.MaskInfo.PromptChar = ((char)(resources.GetObject("numValue.MaskInfo.PromptChar")));
			this.numValue.MaskInfo.ShowLiterals = ((C1.Win.C1Input.ShowLiteralsEnum)(resources.GetObject("numValue.MaskInfo.ShowLiterals")));
			this.numValue.MaskInfo.StoredEmptyChar = ((char)(resources.GetObject("numValue.MaskInfo.StoredEmptyChar")));
			this.numValue.MaxLength = ((int)(resources.GetObject("numValue.MaxLength")));
			this.numValue.Name = "numValue";
			this.numValue.NullText = resources.GetString("numValue.NullText");
			this.numValue.NumericInputKeys = ((C1.Win.C1Input.NumericInputKeyFlags)((((C1.Win.C1Input.NumericInputKeyFlags.F9 | C1.Win.C1Input.NumericInputKeyFlags.Plus) 
				| C1.Win.C1Input.NumericInputKeyFlags.Decimal) 
				| C1.Win.C1Input.NumericInputKeyFlags.X)));
			this.numValue.ParseInfo.CaseSensitive = ((bool)(resources.GetObject("numValue.ParseInfo.CaseSensitive")));
			this.numValue.ParseInfo.CustomFormat = resources.GetString("numValue.ParseInfo.CustomFormat");
			this.numValue.ParseInfo.EmptyAsNull = ((bool)(resources.GetObject("numValue.ParseInfo.EmptyAsNull")));
			this.numValue.ParseInfo.ErrorMessage = resources.GetString("numValue.ParseInfo.ErrorMessage");
			this.numValue.ParseInfo.FormatType = ((C1.Win.C1Input.FormatTypeEnum)(resources.GetObject("numValue.ParseInfo.FormatType")));
			this.numValue.ParseInfo.Inherit = ((C1.Win.C1Input.ParseInfoInheritFlags)(resources.GetObject("numValue.ParseInfo.Inherit")));
			this.numValue.ParseInfo.NullText = resources.GetString("numValue.ParseInfo.NullText");
			this.numValue.ParseInfo.NumberStyle = ((C1.Win.C1Input.NumberStyleFlags)(resources.GetObject("numValue.ParseInfo.NumberStyle")));
			this.numValue.ParseInfo.TrimEnd = ((bool)(resources.GetObject("numValue.ParseInfo.TrimEnd")));
			this.numValue.ParseInfo.TrimStart = ((bool)(resources.GetObject("numValue.ParseInfo.TrimStart")));
			this.numValue.PasswordChar = ((char)(resources.GetObject("numValue.PasswordChar")));
			this.numValue.PostValidation.CaseSensitive = ((bool)(resources.GetObject("numValue.PostValidation.CaseSensitive")));
			this.numValue.PostValidation.ErrorMessage = resources.GetString("numValue.PostValidation.ErrorMessage");
			this.numValue.PostValidation.Inherit = ((C1.Win.C1Input.PostValidationInheritFlags)(resources.GetObject("numValue.PostValidation.Inherit")));
			this.numValue.PostValidation.Validation = ((C1.Win.C1Input.PostValidationTypeEnum)(resources.GetObject("numValue.PostValidation.Validation")));
			this.numValue.PostValidation.Values = ((System.Array)(resources.GetObject("numValue.PostValidation.Values")));
			this.numValue.PostValidation.ValuesExcluded = ((System.Array)(resources.GetObject("numValue.PostValidation.ValuesExcluded")));
			this.numValue.PreValidation.CaseSensitive = ((bool)(resources.GetObject("numValue.PreValidation.CaseSensitive")));
			this.numValue.PreValidation.ErrorMessage = resources.GetString("numValue.PreValidation.ErrorMessage");
			this.numValue.PreValidation.Inherit = ((C1.Win.C1Input.PreValidationInheritFlags)(resources.GetObject("numValue.PreValidation.Inherit")));
			this.numValue.PreValidation.ItemSeparator = resources.GetString("numValue.PreValidation.ItemSeparator");
			this.numValue.PreValidation.PatternString = resources.GetString("numValue.PreValidation.PatternString");
			this.numValue.PreValidation.RegexOptions = ((C1.Win.C1Input.RegexOptionFlags)(resources.GetObject("numValue.PreValidation.RegexOptions")));
			this.numValue.PreValidation.TrimEnd = ((bool)(resources.GetObject("numValue.PreValidation.TrimEnd")));
			this.numValue.PreValidation.TrimStart = ((bool)(resources.GetObject("numValue.PreValidation.TrimStart")));
			this.numValue.PreValidation.Validation = ((C1.Win.C1Input.PreValidationTypeEnum)(resources.GetObject("numValue.PreValidation.Validation")));
			this.numValue.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("numValue.RightToLeft")));
			this.numValue.ShowFocusRectangle = ((bool)(resources.GetObject("numValue.ShowFocusRectangle")));
			this.numValue.Size = ((System.Drawing.Size)(resources.GetObject("numValue.Size")));
			this.numValue.TabIndex = ((int)(resources.GetObject("numValue.TabIndex")));
			this.numValue.Tag = ((object)(resources.GetObject("numValue.Tag")));
			this.numValue.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("numValue.TextAlign")));
			this.numValue.TrimEnd = ((bool)(resources.GetObject("numValue.TrimEnd")));
			this.numValue.TrimStart = ((bool)(resources.GetObject("numValue.TrimStart")));
			this.numValue.UserCultureOverride = ((bool)(resources.GetObject("numValue.UserCultureOverride")));
			this.numValue.Value = ((object)(resources.GetObject("numValue.Value")));
			this.numValue.VerticalAlign = ((C1.Win.C1Input.VerticalAlignEnum)(resources.GetObject("numValue.VerticalAlign")));
			this.numValue.Visible = ((bool)(resources.GetObject("numValue.Visible")));
			this.numValue.VisibleButtons = ((C1.Win.C1Input.DropDownControlButtonFlags)(resources.GetObject("numValue.VisibleButtons")));
			// 
			// PrintConfiguration
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
			this.Controls.Add(this.btnHelp);
			this.Controls.Add(this.btnSave);
			this.Controls.Add(this.btnClose);
			this.Controls.Add(this.dgrdData);
			this.Controls.Add(this.numValue);
			this.Enabled = ((bool)(resources.GetObject("$this.Enabled")));
			this.Font = ((System.Drawing.Font)(resources.GetObject("$this.Font")));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("$this.ImeMode")));
			this.Location = ((System.Drawing.Point)(resources.GetObject("$this.Location")));
			this.MaximizeBox = false;
			this.MaximumSize = ((System.Drawing.Size)(resources.GetObject("$this.MaximumSize")));
			this.MinimizeBox = false;
			this.MinimumSize = ((System.Drawing.Size)(resources.GetObject("$this.MinimumSize")));
			this.Name = "PrintConfiguration";
			this.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("$this.RightToLeft")));
			this.StartPosition = ((System.Windows.Forms.FormStartPosition)(resources.GetObject("$this.StartPosition")));
			this.Text = resources.GetString("$this.Text");
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.PrintConfiguration_KeyDown);
			this.Closing += new System.ComponentModel.CancelEventHandler(this.PrintConfiguration_Closing);
			this.Load += new System.EventHandler(this.PrintConfiguration_Load);
			((System.ComponentModel.ISupportInitialize)(this.dgrdData)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numValue)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion
		
		#region Methods
		
		/// <summary>
		/// process data inputed into the grid
		/// </summary>
		/// <param name="pstrColumnName"></param>
		/// <param name="pstrColumValue"></param>
		/// <returns>If no error then return true, else return false</returns>
		private bool ProcessInputDataInGrid(string pstrColumnName, string pstrColumValue, bool pblnAlwaysShow)
		{
			const string METHOD_NAME = THIS +  ".ProcessInputDataInGrid()";
			try
			{
				bool blnResult = true;
				//Check for each column
				switch (pstrColumnName)
				{
					case Sys_PrintConfigurationTable.FILENAME_FLD:				
						dlgOpenReportFile.Filter = REPORT_LAYOUT_FILE_FILTER;
						dlgOpenReportFile.InitialDirectory = mstrReportDefFolder;
                        dlgOpenReportFile.FileName = dgrdData.Columns[Sys_PrintConfigurationTable.FILENAME_FLD].Text;
						dlgOpenReportFile.ShowDialog();
						dgrdData.Columns[Sys_PrintConfigurationTable.FILENAME_FLD].Text = Path.GetFileName(dlgOpenReportFile.FileName);
						break;
				}
				return blnResult;
			}
			catch(PCSException ex)
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
				
				//Change display format
				dgrdData.Columns[Sys_PrintConfigurationTable.COPIES_FLD].NumberFormat = Constants.INT_DSP_NUM_MASK;
				dgrdData.Columns[Sys_PrintConfigurationTable.COPIES_FLD].Editor = numValue;

				dgrdData.Columns[Sys_PrintConfigurationTable.PRINTABLE_FLD].ValueItems.Presentation = PresentationEnum.CheckBox;
				dgrdData.Columns[Sys_PrintConfigurationTable.PRINTABLE_FLD].ValueItems.Translate = true;
				dgrdData.Columns[Sys_PrintConfigurationTable.PRINTABLE_FLD].DefaultValue = false.ToString();
				
				dgrdData.AllowRowSizing = RowSizingEnum.AllRows;
				dgrdData.RowHeight = numValue.Height;

				dgrdData.Splits[0].DisplayColumns[Sys_PrintConfigurationTable.FILENAME_FLD].Button = true;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message, ex);
			}
		}
		
		/// <summary>
		/// Validate data before update into batabase
		/// </summary>
		/// <returns></returns>		
		private bool ValidateData()
		{
			try
			{
				//variable to indicate grid's row index
				int intRowIndex = -1;
				foreach (DataRow drowRow in dtbPrinConfig.Rows)
				{
					intRowIndex++;
					if(drowRow.RowState == DataRowState.Deleted) continue;
					
					if(drowRow[Sys_PrintConfigurationTable.FILENAME_FLD].Equals(string.Empty))
					{
						// Please input Item field for each records.
						PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Exclamation);
						dgrdData.Row = intRowIndex;
						dgrdData.Col = dgrdData.Columns.IndexOf(dgrdData.Columns[Sys_PrintConfigurationTable.FILENAME_FLD]);
						dgrdData.Focus();
						return false;
					}					
					
					if(!File.Exists(mstrReportDefFolder + "\\" + drowRow[Sys_PrintConfigurationTable.FILENAME_FLD].ToString()))
					{
						// Please input Item field for each records.
						//PCSMessageBox.Show(ErrorCode.MESSAGE_PRINTCONFIG_FILE_NOT_EXIST, MessageBoxIcon.Exclamation);
						PCSMessageBox.Show(ErrorCode.MESSAGE_REPORT_TEMPLATE_FILE_NOT_FOUND, MessageBoxIcon.Exclamation);
						dgrdData.Row = intRowIndex;
						dgrdData.Col = dgrdData.Columns.IndexOf(dgrdData.Columns[Sys_PrintConfigurationTable.FILENAME_FLD]);
						dgrdData.Focus();
						return false;
					}
					
					if(!FormControlComponents.IsPositiveNumeric(drowRow[Sys_PrintConfigurationTable.COPIES_FLD].ToString())
						|| drowRow[Sys_PrintConfigurationTable.COPIES_FLD].Equals(ZERO_STRING))
					{
						// Please input Item field for each records.
						PCSMessageBox.Show(ErrorCode.MESSAGE_OUTPROCESSING_INVALID_NUMBER, MessageBoxIcon.Exclamation);
						dgrdData.Row = intRowIndex;
						dgrdData.Col = dgrdData.Columns.IndexOf(dgrdData.Columns[Sys_PrintConfigurationTable.COPIES_FLD]);
						dgrdData.Focus();
						return false;
					}

					drowRow[Sys_PrintConfigurationTable.FORMNAME_FLD] = mstrFormName;
				}
				
				return true;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message, ex);
			}
		}


		#endregion 

		#region Event Processing
		private void btnSave_Click(object sender, System.EventArgs e)
		{
			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.WaitCursor;

			#endregion Code Inserted Automatically

			
			const string METHOD_NAME = THIS + ".btnSave_Click()";

			try
			{
				// check data, if data is invalid then exit immediately
				blnDataIsValid = ValidateData() && !dgrdData.EditActive;
				if(!blnDataIsValid) 
{
// Code Inserted Automatically
#region Code Inserted Automatically
this.Cursor = Cursors.Default;
#endregion Code Inserted Automatically

																								return;
}
				
				// check form action to save data
				if(dtbPrinConfig.DataSet != null)
				{
					boPrintConfig.UpdateDataSet(dtbPrinConfig.DataSet);
				}
				else
				{
					DataSet dtsDC = new DataSet();
					dtsDC.Tables.Add(dtbPrinConfig);
					boPrintConfig.UpdateDataSet(dtbPrinConfig.DataSet);
				}
				
				//Get print config of selected form
				dtbPrinConfig = boPrintConfig.GetPrintConfigurationByFormName(mstrFormName);
				dgrdData.DataSource = dtbPrinConfig;	
				FormatDataGrid();
				PCSMessageBox.Show(ErrorCode.MESSAGE_AFTER_SAVE_DATA);
				btnSave.Enabled = false;
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

			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.Default;

			#endregion Code Inserted Automatically

			
		}		

		private void btnHelp_Click(object sender, System.EventArgs e)
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

		private void btnClose_Click(object sender, System.EventArgs e)
		{
			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.WaitCursor;

			#endregion Code Inserted Automatically

			
			this.Close();

			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.Default;

			#endregion Code Inserted Automatically

			
		}		

		private void PrintConfiguration_Load(object sender, System.EventArgs e)
		{
			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.WaitCursor;

			#endregion Code Inserted Automatically

			
			const string METHOD_NAME = THIS + ".PrintConfiguration_Load()";
			try
			{			
				#region Security
				//Set authorization for user
				Security objSecurity = new Security();
				this.Name = THIS;
				if(objSecurity.SetRightForUserOnForm(this, SystemProperty.UserName) == 0)
				{
					this.Close();
					// You don't have the right to view this item
					PCSMessageBox.Show(ErrorCode.MESSAGE_YOU_HAVE_NO_RIGHT_TO_VIEW);
					// Code Inserted Automatically
#region Code Inserted Automatically
this.Cursor = Cursors.Default;
#endregion Code Inserted Automatically

					return;
				}
				#endregion
				
				//store grid layout
				dtbGridLayOut = FormControlComponents.StoreGridLayout(dgrdData);
				//Get print config of selected form
				dtbPrinConfig = boPrintConfig.GetPrintConfigurationByFormName(mstrFormName);
				dgrdData.DataSource = dtbPrinConfig;
				
				//change grid representation
				FormatDataGrid();

				/// remove temporary files from the Report Definition Folder
				/// We put the cleanup process here to avoid it runs too frequently or rarely
				/// This form is rather small, so It performs quickly.
				/// TODO: Thachnn: Remove this line to clean all the ReportTemporaryFile
				/// FormControlComponents.DeletePCSTempReportFile(mstrReportDefFolder);
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

			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.Default;

			#endregion Code Inserted Automatically

			
		}
		
		private void dgrdData_ButtonClick(object sender, ColEventArgs e)
		{
			const string METHOD_NAME = THIS + ".dgrdData_ButtonClick()";

			try
			{					
				ProcessInputDataInGrid(e.Column.DataColumn.DataField, e.Column.DataColumn.Text.Trim(), true);				
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

		private void dgrdData_KeyDown(object sender, KeyEventArgs e)
		{
			const string METHOD_NAME = THIS + ".dgrdData_KeyDown()";

			try
			{
				switch (e.KeyCode)
				{
					case Keys.F4:
						ProcessInputDataInGrid(dgrdData.Columns[dgrdData.Col].DataField, dgrdData.Columns[dgrdData.Col].Value.ToString(), true);
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

		

		private void PrintConfiguration_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".PrintConfiguration_Closing()";
			try
			{
				// if the form has been changed then ask to store database
				if(btnSave.Enabled) 
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

		private void PrintConfiguration_KeyDown(object sender, KeyEventArgs e)
		{
			const string METHOD_NAME = THIS + ".dgrdData_KeyDown()";

			try
			{			
				switch (e.KeyCode)
				{
					case Keys.F12:
						dgrdData.Col = dgrdData.Columns.IndexOf(dgrdData.Columns[Sys_PrintConfigurationTable.PRINTABLE_FLD]);
                        dgrdData.Row = dgrdData.RowCount;
						dgrdData.Focus();
						break;

					case Keys.Escape:
						this.Close();
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
		
		private void dgrdData_Change(object sender, EventArgs e)
		{
			btnSave.Enabled = true;
		}		
		private void dgrdData_AfterDelete(object sender, EventArgs e)
		{
			btnSave.Enabled = true;
		}
		#endregion

		
	}
}
