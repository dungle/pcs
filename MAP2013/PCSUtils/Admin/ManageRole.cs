using System;
using System.Data;
using System.Windows.Forms;
using C1.Win.C1TrueDBGrid;
using PCSComUtils.Admin.BO;
using PCSComUtils.Common;
using PCSComUtils.PCSExc;
using PCSUtils.Log;
using PCSUtils.Utils;

namespace PCSUtils.Admin
{
	/// <summary>
	/// ManageUser Form. 
	/// Created by: Cuong NT
	/// Implemented by: Thien HD.	
	/// </summary>
	public class ManageRole : System.Windows.Forms.Form
	{
		#region Declaration

		#region System Generated
		
		private System.Windows.Forms.Button btnClose;
		private System.Windows.Forms.Button btnHelp;
		private System.Windows.Forms.Button btnDelete;
		private C1.Win.C1TrueDBGrid.C1TrueDBGrid tgridViewData;
		private System.Windows.Forms.Button btnSaveToDB;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		#endregion System Generated
		
		private const string THIS = "PCSUtils.Admin.ManageRole";
		private const char CHR_SEPARATOR = ';';

		private DataSet dstData; // this variable is used to store all data for this form;
		private bool blnEditUpdateData; //This variable is used to determine user action to edit or not
		private bool blnErrorOccurs;
		string strDeletedRole = string.Empty;	
		
		#endregion Declaration
		
		#region Constructor, Destructor
		public ManageRole()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
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
		
		#endregion Constructor, Destructor

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(ManageRole));
			this.btnClose = new System.Windows.Forms.Button();
			this.btnHelp = new System.Windows.Forms.Button();
			this.btnDelete = new System.Windows.Forms.Button();
			this.tgridViewData = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
			this.btnSaveToDB = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.tgridViewData)).BeginInit();
			this.SuspendLayout();
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
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
			// 
			// tgridViewData
			// 
			this.tgridViewData.AccessibleDescription = resources.GetString("tgridViewData.AccessibleDescription");
			this.tgridViewData.AccessibleName = resources.GetString("tgridViewData.AccessibleName");
			this.tgridViewData.AllowAddNew = ((bool)(resources.GetObject("tgridViewData.AllowAddNew")));
			this.tgridViewData.AllowArrows = ((bool)(resources.GetObject("tgridViewData.AllowArrows")));
			this.tgridViewData.AllowColMove = ((bool)(resources.GetObject("tgridViewData.AllowColMove")));
			this.tgridViewData.AllowColSelect = ((bool)(resources.GetObject("tgridViewData.AllowColSelect")));
			this.tgridViewData.AllowDelete = ((bool)(resources.GetObject("tgridViewData.AllowDelete")));
			this.tgridViewData.AllowDrag = ((bool)(resources.GetObject("tgridViewData.AllowDrag")));
			this.tgridViewData.AllowFilter = ((bool)(resources.GetObject("tgridViewData.AllowFilter")));
			this.tgridViewData.AllowHorizontalSplit = ((bool)(resources.GetObject("tgridViewData.AllowHorizontalSplit")));
			this.tgridViewData.AllowRowSelect = ((bool)(resources.GetObject("tgridViewData.AllowRowSelect")));
			this.tgridViewData.AllowSort = ((bool)(resources.GetObject("tgridViewData.AllowSort")));
			this.tgridViewData.AllowUpdate = ((bool)(resources.GetObject("tgridViewData.AllowUpdate")));
			this.tgridViewData.AllowUpdateOnBlur = ((bool)(resources.GetObject("tgridViewData.AllowUpdateOnBlur")));
			this.tgridViewData.AllowVerticalSplit = ((bool)(resources.GetObject("tgridViewData.AllowVerticalSplit")));
			this.tgridViewData.AlternatingRows = ((bool)(resources.GetObject("tgridViewData.AlternatingRows")));
			this.tgridViewData.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tgridViewData.Anchor")));
			this.tgridViewData.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tgridViewData.BackgroundImage")));
			this.tgridViewData.BorderStyle = ((System.Windows.Forms.BorderStyle)(resources.GetObject("tgridViewData.BorderStyle")));
			this.tgridViewData.Caption = resources.GetString("tgridViewData.Caption");
			this.tgridViewData.CaptionHeight = ((int)(resources.GetObject("tgridViewData.CaptionHeight")));
			this.tgridViewData.CellTipsDelay = ((int)(resources.GetObject("tgridViewData.CellTipsDelay")));
			this.tgridViewData.CellTipsWidth = ((int)(resources.GetObject("tgridViewData.CellTipsWidth")));
			this.tgridViewData.ChildGrid = ((C1.Win.C1TrueDBGrid.C1TrueDBGrid)(resources.GetObject("tgridViewData.ChildGrid")));
			this.tgridViewData.CollapseColor = ((System.Drawing.Color)(resources.GetObject("tgridViewData.CollapseColor")));
			this.tgridViewData.ColumnFooters = ((bool)(resources.GetObject("tgridViewData.ColumnFooters")));
			this.tgridViewData.ColumnHeaders = ((bool)(resources.GetObject("tgridViewData.ColumnHeaders")));
			this.tgridViewData.DefColWidth = ((int)(resources.GetObject("tgridViewData.DefColWidth")));
			this.tgridViewData.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tgridViewData.Dock")));
			this.tgridViewData.EditDropDown = ((bool)(resources.GetObject("tgridViewData.EditDropDown")));
			this.tgridViewData.EmptyRows = ((bool)(resources.GetObject("tgridViewData.EmptyRows")));
			this.tgridViewData.Enabled = ((bool)(resources.GetObject("tgridViewData.Enabled")));
			this.tgridViewData.ExpandColor = ((System.Drawing.Color)(resources.GetObject("tgridViewData.ExpandColor")));
			this.tgridViewData.ExposeCellMode = ((C1.Win.C1TrueDBGrid.ExposeCellModeEnum)(resources.GetObject("tgridViewData.ExposeCellMode")));
			this.tgridViewData.ExtendRightColumn = ((bool)(resources.GetObject("tgridViewData.ExtendRightColumn")));
			this.tgridViewData.FetchRowStyles = ((bool)(resources.GetObject("tgridViewData.FetchRowStyles")));
			this.tgridViewData.FilterBar = ((bool)(resources.GetObject("tgridViewData.FilterBar")));
			this.tgridViewData.FlatStyle = ((C1.Win.C1TrueDBGrid.FlatModeEnum)(resources.GetObject("tgridViewData.FlatStyle")));
			this.tgridViewData.Font = ((System.Drawing.Font)(resources.GetObject("tgridViewData.Font")));
			this.tgridViewData.GroupByAreaVisible = ((bool)(resources.GetObject("tgridViewData.GroupByAreaVisible")));
			this.tgridViewData.GroupByCaption = resources.GetString("tgridViewData.GroupByCaption");
			this.tgridViewData.Images.Add(((System.Drawing.Image)(resources.GetObject("resource"))));
			this.tgridViewData.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tgridViewData.ImeMode")));
			this.tgridViewData.LinesPerRow = ((int)(resources.GetObject("tgridViewData.LinesPerRow")));
			this.tgridViewData.Location = ((System.Drawing.Point)(resources.GetObject("tgridViewData.Location")));
			this.tgridViewData.MarqueeStyle = C1.Win.C1TrueDBGrid.MarqueeEnum.DottedCellBorder;
			this.tgridViewData.MultiSelect = C1.Win.C1TrueDBGrid.MultiSelectEnum.None;
			this.tgridViewData.Name = "tgridViewData";
			this.tgridViewData.PictureAddnewRow = ((System.Drawing.Image)(resources.GetObject("tgridViewData.PictureAddnewRow")));
			this.tgridViewData.PictureCurrentRow = ((System.Drawing.Image)(resources.GetObject("tgridViewData.PictureCurrentRow")));
			this.tgridViewData.PictureFilterBar = ((System.Drawing.Image)(resources.GetObject("tgridViewData.PictureFilterBar")));
			this.tgridViewData.PictureFooterRow = ((System.Drawing.Image)(resources.GetObject("tgridViewData.PictureFooterRow")));
			this.tgridViewData.PictureHeaderRow = ((System.Drawing.Image)(resources.GetObject("tgridViewData.PictureHeaderRow")));
			this.tgridViewData.PictureModifiedRow = ((System.Drawing.Image)(resources.GetObject("tgridViewData.PictureModifiedRow")));
			this.tgridViewData.PictureStandardRow = ((System.Drawing.Image)(resources.GetObject("tgridViewData.PictureStandardRow")));
			this.tgridViewData.PreviewInfo.AllowSizing = ((bool)(resources.GetObject("tgridViewData.PreviewInfo.AllowSizing")));
			this.tgridViewData.PreviewInfo.Caption = resources.GetString("tgridViewData.PreviewInfo.Caption");
			this.tgridViewData.PreviewInfo.Location = ((System.Drawing.Point)(resources.GetObject("tgridViewData.PreviewInfo.Location")));
			this.tgridViewData.PreviewInfo.Size = ((System.Drawing.Size)(resources.GetObject("tgridViewData.PreviewInfo.Size")));
			this.tgridViewData.PreviewInfo.ToolBars = ((bool)(resources.GetObject("tgridViewData.PreviewInfo.ToolBars")));
			this.tgridViewData.PreviewInfo.UIStrings.Content = ((string[])(resources.GetObject("tgridViewData.PreviewInfo.UIStrings.Content")));
			this.tgridViewData.PreviewInfo.ZoomFactor = ((System.Double)(resources.GetObject("tgridViewData.PreviewInfo.ZoomFactor")));
			this.tgridViewData.PrintInfo.MaxRowHeight = ((int)(resources.GetObject("tgridViewData.PrintInfo.MaxRowHeight")));
			this.tgridViewData.PrintInfo.OwnerDrawPageFooter = ((bool)(resources.GetObject("tgridViewData.PrintInfo.OwnerDrawPageFooter")));
			this.tgridViewData.PrintInfo.OwnerDrawPageHeader = ((bool)(resources.GetObject("tgridViewData.PrintInfo.OwnerDrawPageHeader")));
			this.tgridViewData.PrintInfo.PageFooter = resources.GetString("tgridViewData.PrintInfo.PageFooter");
			this.tgridViewData.PrintInfo.PageFooterHeight = ((int)(resources.GetObject("tgridViewData.PrintInfo.PageFooterHeight")));
			this.tgridViewData.PrintInfo.PageHeader = resources.GetString("tgridViewData.PrintInfo.PageHeader");
			this.tgridViewData.PrintInfo.PageHeaderHeight = ((int)(resources.GetObject("tgridViewData.PrintInfo.PageHeaderHeight")));
			this.tgridViewData.PrintInfo.PrintHorizontalSplits = ((bool)(resources.GetObject("tgridViewData.PrintInfo.PrintHorizontalSplits")));
			this.tgridViewData.PrintInfo.ProgressCaption = resources.GetString("tgridViewData.PrintInfo.ProgressCaption");
			this.tgridViewData.PrintInfo.RepeatColumnFooters = ((bool)(resources.GetObject("tgridViewData.PrintInfo.RepeatColumnFooters")));
			this.tgridViewData.PrintInfo.RepeatColumnHeaders = ((bool)(resources.GetObject("tgridViewData.PrintInfo.RepeatColumnHeaders")));
			this.tgridViewData.PrintInfo.RepeatGridHeader = ((bool)(resources.GetObject("tgridViewData.PrintInfo.RepeatGridHeader")));
			this.tgridViewData.PrintInfo.RepeatSplitHeaders = ((bool)(resources.GetObject("tgridViewData.PrintInfo.RepeatSplitHeaders")));
			this.tgridViewData.PrintInfo.ShowOptionsDialog = ((bool)(resources.GetObject("tgridViewData.PrintInfo.ShowOptionsDialog")));
			this.tgridViewData.PrintInfo.ShowProgressForm = ((bool)(resources.GetObject("tgridViewData.PrintInfo.ShowProgressForm")));
			this.tgridViewData.PrintInfo.ShowSelection = ((bool)(resources.GetObject("tgridViewData.PrintInfo.ShowSelection")));
			this.tgridViewData.PrintInfo.UseGridColors = ((bool)(resources.GetObject("tgridViewData.PrintInfo.UseGridColors")));
			this.tgridViewData.RecordSelectors = ((bool)(resources.GetObject("tgridViewData.RecordSelectors")));
			this.tgridViewData.RecordSelectorWidth = ((int)(resources.GetObject("tgridViewData.RecordSelectorWidth")));
			this.tgridViewData.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tgridViewData.RightToLeft")));
			this.tgridViewData.RowDivider.Color = ((System.Drawing.Color)(resources.GetObject("resource.Color")));
			this.tgridViewData.RowDivider.Style = ((C1.Win.C1TrueDBGrid.LineStyleEnum)(resources.GetObject("resource.Style")));
			this.tgridViewData.RowHeight = ((int)(resources.GetObject("tgridViewData.RowHeight")));
			this.tgridViewData.RowSubDividerColor = ((System.Drawing.Color)(resources.GetObject("tgridViewData.RowSubDividerColor")));
			this.tgridViewData.ScrollTips = ((bool)(resources.GetObject("tgridViewData.ScrollTips")));
			this.tgridViewData.ScrollTrack = ((bool)(resources.GetObject("tgridViewData.ScrollTrack")));
			this.tgridViewData.Size = ((System.Drawing.Size)(resources.GetObject("tgridViewData.Size")));
			this.tgridViewData.SpringMode = ((bool)(resources.GetObject("tgridViewData.SpringMode")));
			this.tgridViewData.TabAcrossSplits = ((bool)(resources.GetObject("tgridViewData.TabAcrossSplits")));
			this.tgridViewData.TabIndex = ((int)(resources.GetObject("tgridViewData.TabIndex")));
			this.tgridViewData.Text = resources.GetString("tgridViewData.Text");
			this.tgridViewData.ViewCaptionWidth = ((int)(resources.GetObject("tgridViewData.ViewCaptionWidth")));
			this.tgridViewData.ViewColumnWidth = ((int)(resources.GetObject("tgridViewData.ViewColumnWidth")));
			this.tgridViewData.Visible = ((bool)(resources.GetObject("tgridViewData.Visible")));
			this.tgridViewData.WrapCellPointer = ((bool)(resources.GetObject("tgridViewData.WrapCellPointer")));
			this.tgridViewData.AfterDelete += new System.EventHandler(this.tgridViewData_AfterDelete);
			this.tgridViewData.AfterUpdate += new System.EventHandler(this.tgridViewData_AfterUpdate);
			this.tgridViewData.RowColChange += new C1.Win.C1TrueDBGrid.RowColChangeEventHandler(this.tgridViewData_RowColChange);
			this.tgridViewData.BeforeColEdit += new C1.Win.C1TrueDBGrid.BeforeColEditEventHandler(this.tgridViewData_BeforeColEdit);
			this.tgridViewData.Click += new System.EventHandler(this.tgridViewData_Click);
			this.tgridViewData.AfterColEdit += new C1.Win.C1TrueDBGrid.ColEventHandler(this.tgridViewData_AfterColEdit);
			this.tgridViewData.BeforeUpdate += new C1.Win.C1TrueDBGrid.CancelEventHandler(this.tgridViewData_BeforeUpdate);
			this.tgridViewData.AfterColUpdate += new C1.Win.C1TrueDBGrid.ColEventHandler(this.tgridViewData_AfterColUpdate);
			this.tgridViewData.BeforeInsert += new C1.Win.C1TrueDBGrid.CancelEventHandler(this.tgridViewData_BeforeInsert);
			this.tgridViewData.BeforeColUpdate += new C1.Win.C1TrueDBGrid.BeforeColUpdateEventHandler(this.tgridViewData_BeforeColUpdate);
			this.tgridViewData.BeforeDelete += new C1.Win.C1TrueDBGrid.CancelEventHandler(this.tgridViewData_BeforeDelete);
			this.tgridViewData.BeforeRowColChange += new C1.Win.C1TrueDBGrid.CancelEventHandler(this.tgridViewData_BeforeRowColChange);
			this.tgridViewData.Leave += new System.EventHandler(this.tgridViewData_Leave);
			this.tgridViewData.AfterInsert += new System.EventHandler(this.tgridViewData_AfterInsert);
			this.tgridViewData.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tgridViewData_KeyUp);
			this.tgridViewData.PropBag = resources.GetString("tgridViewData.PropBag");
			// 
			// btnSaveToDB
			// 
			this.btnSaveToDB.AccessibleDescription = resources.GetString("btnSaveToDB.AccessibleDescription");
			this.btnSaveToDB.AccessibleName = resources.GetString("btnSaveToDB.AccessibleName");
			this.btnSaveToDB.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnSaveToDB.Anchor")));
			this.btnSaveToDB.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSaveToDB.BackgroundImage")));
			this.btnSaveToDB.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnSaveToDB.Dock")));
			this.btnSaveToDB.Enabled = ((bool)(resources.GetObject("btnSaveToDB.Enabled")));
			this.btnSaveToDB.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnSaveToDB.FlatStyle")));
			this.btnSaveToDB.Font = ((System.Drawing.Font)(resources.GetObject("btnSaveToDB.Font")));
			this.btnSaveToDB.Image = ((System.Drawing.Image)(resources.GetObject("btnSaveToDB.Image")));
			this.btnSaveToDB.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnSaveToDB.ImageAlign")));
			this.btnSaveToDB.ImageIndex = ((int)(resources.GetObject("btnSaveToDB.ImageIndex")));
			this.btnSaveToDB.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnSaveToDB.ImeMode")));
			this.btnSaveToDB.Location = ((System.Drawing.Point)(resources.GetObject("btnSaveToDB.Location")));
			this.btnSaveToDB.Name = "btnSaveToDB";
			this.btnSaveToDB.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnSaveToDB.RightToLeft")));
			this.btnSaveToDB.Size = ((System.Drawing.Size)(resources.GetObject("btnSaveToDB.Size")));
			this.btnSaveToDB.TabIndex = ((int)(resources.GetObject("btnSaveToDB.TabIndex")));
			this.btnSaveToDB.Text = resources.GetString("btnSaveToDB.Text");
			this.btnSaveToDB.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnSaveToDB.TextAlign")));
			this.btnSaveToDB.Visible = ((bool)(resources.GetObject("btnSaveToDB.Visible")));
			this.btnSaveToDB.Click += new System.EventHandler(this.btnSaveToDB_Click);
			// 
			// ManageRole
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
			this.Controls.Add(this.tgridViewData);
			this.Controls.Add(this.btnClose);
			this.Controls.Add(this.btnHelp);
			this.Controls.Add(this.btnDelete);
			this.Controls.Add(this.btnSaveToDB);
			this.Enabled = ((bool)(resources.GetObject("$this.Enabled")));
			this.Font = ((System.Drawing.Font)(resources.GetObject("$this.Font")));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("$this.ImeMode")));
			this.Location = ((System.Drawing.Point)(resources.GetObject("$this.Location")));
			this.MaximumSize = ((System.Drawing.Size)(resources.GetObject("$this.MaximumSize")));
			this.MinimumSize = ((System.Drawing.Size)(resources.GetObject("$this.MinimumSize")));
			this.Name = "ManageRole";
			this.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("$this.RightToLeft")));
			this.StartPosition = ((System.Windows.Forms.FormStartPosition)(resources.GetObject("$this.StartPosition")));
			this.Text = resources.GetString("$this.Text");
			this.Closing += new System.ComponentModel.CancelEventHandler(this.ManageRole_Closing);
			this.Load += new System.EventHandler(this.ManageRole_Load);
			((System.ComponentModel.ISupportInitialize)(this.tgridViewData)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		#region Class's Method		
				
		/// <summary>
		/// Enable or Disable button based on the user action
		/// </summary>
		/// <param name="blnStatus"></param>
		/// <Author> Thien HD, Jan 07, 2005</Author>
		private void ChangeEditFlag(bool blnStatus)
		{
			blnEditUpdateData = blnStatus;
			btnSaveToDB.Enabled = blnStatus;
			//btnCancel.Enabled = blnStatus;
		}
				
		/// <summary>
		/// Load data for this form
		/// </summary>
		/// <Author> Thien HD, Jan 07, 2005</Author>
		private void LoadForm()
		{
			const int ROLE_NAME_WIDTH = 200;
			const int ROLE_DESCRIPTION_WIDTH = 400;
			try
			{
				//Init the BO object
				ManageRoleBO objManageRoleBO = new ManageRoleBO();
				//Get the list of roles for this data and store into dstData variable
				dstData = objManageRoleBO.List();
				dstData.Tables[0].DefaultView.Sort = Sys_RoleTable.NAME_FLD;

				//get the field length for this table
				DataRow drFieldLength = objManageRoleBO.GetFieldLength();

				//Grant this data into the grid
				tgridViewData.DataSource = dstData.Tables[0];
				//tgridViewData.DataMember = dstData.Tables[0].TableName;

				//Set the role name to be unique
				//dstData.Tables[0].Columns[Sys_RoleTable.NAME_FLD].Unique = true;
				dstData.Tables[0].Columns[Sys_RoleTable.NAME_FLD].AllowDBNull = false;

				//set the ROLE ID to Identity column
				//dstData.Tables[0].Columns[Sys_RoleTable.ROLEID_FLD].AutoIncrement = true;

				//Get the highest value for this column
				/*
				DataView dvDataView = dstData.Tables[0].DefaultView;
				dvDataView.Sort = Sys_RoleTable.ROLEID_FLD + " DESC";
				if (dstData.Tables[0].Rows.Count == 0)
				{
					dstData.Tables[0].Columns[Sys_RoleTable.ROLEID_FLD].AutoIncrementSeed = 1;
				}
				else
				{
					dstData.Tables[0].Columns[Sys_RoleTable.ROLEID_FLD].AutoIncrementSeed = int.Parse(dvDataView[0][Sys_RoleTable.ROLEID_FLD].ToString()) + 1;
				}
				dstData.Tables[0].Columns[Sys_RoleTable.ROLEID_FLD].AutoIncrementStep = 1;
				dvDataView = null;
				*/


				//Center Heading
				for (int i = 0; i < tgridViewData.Splits[0].DisplayColumns.Count; i++)
				{
					tgridViewData.Splits[0].DisplayColumns[i].HeadingStyle.HorizontalAlignment = AlignHorzEnum.Center;
				}

				//Align the ID column
				tgridViewData.Splits[0].DisplayColumns[Sys_RoleTable.ROLEID_FLD].Style.HorizontalAlignment = AlignHorzEnum.Near;

				//Inivisble the ID column
				tgridViewData.Splits[0].DisplayColumns[Sys_RoleTable.ROLEID_FLD].Visible = false;


				//Set the column length for each column in grid
				tgridViewData.Columns[Sys_RoleTable.NAME_FLD].DataWidth = int.Parse(drFieldLength[Sys_RoleTable.NAME_FLD].ToString());
				tgridViewData.Columns[Sys_RoleTable.DESCRIPTION_FLD].DataWidth = int.Parse(drFieldLength[Sys_RoleTable.DESCRIPTION_FLD].ToString());

				//set the column width
				tgridViewData.Splits[0].DisplayColumns[Sys_RoleTable.NAME_FLD].Width = ROLE_NAME_WIDTH;
				tgridViewData.Splits[0].DisplayColumns[Sys_RoleTable.DESCRIPTION_FLD].Width = ROLE_DESCRIPTION_WIDTH;

				/// HACKED: Thachnn: fix bug 1652
				//set the Mandatory Column
				tgridViewData.Splits[0].DisplayColumns[Sys_RoleTable.NAME_FLD].HeadingStyle.ForeColor = System.Drawing.Color.Maroon;// = oHeadingStyle;
				/// ENDHACKED: Thachnn : fix bug 1652
				
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
		/// Save data into database
		/// </summary>
		/// <returns></returns>
		/// <Author> Thien HD, Jan 07, 2005</Author>
		private bool SaveToDatabase()
		{
			const int COL_NAME = 1;
			const string METHOD_NAME = THIS + ".btnSaveToDB_Click()";

			if (blnErrorOccurs)
			{
				PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID);
				tgridViewData.Col = COL_NAME;
				tgridViewData.Focus();
				return false;
			}
			if (!blnEditUpdateData)
			{
				return true;
			}

			if (dstData.Tables[0].Rows.Count == 0) 
			{
				PCSMessageBox.Show(ErrorCode.MESSAGE_MANAGEROLE_NOROW_TOSAVE,MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return true;
			}	

			try
			{
				ManageRoleBO objManageRoleBO = new ManageRoleBO();

				//Call the UpdateDataSet method 
				if (strDeletedRole.EndsWith(CHR_SEPARATOR.ToString()))
				{
					strDeletedRole = strDeletedRole.Substring(0,strDeletedRole.Length - 1);
				}
				objManageRoleBO.UpdateDataSetAndDelete(dstData,strDeletedRole);
				strDeletedRole = string.Empty;

				//After saving into database , refresh the data for the grid

				tgridViewData.Refresh();
				
				ChangeEditFlag(false);

				PCSMessageBox.Show(ErrorCode.MESSAGE_AFTER_SAVE_DATA, MessageBoxIcon.Information);
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
				//focus
				if (ex.mCode == ErrorCode.DUPLICATE_KEY)
				{
					//search for duplicate row
					int i,j;
					for (i = 0; i < tgridViewData.RowCount; i++)
					{
						for (j = i + 1; j < tgridViewData.RowCount; j++)
						{
							if (tgridViewData[i,COL_NAME].ToString().Equals(tgridViewData[j,COL_NAME].ToString()))
							{
								tgridViewData.Row = j;
								tgridViewData.Col = COL_NAME;
								tgridViewData.Focus();
								return false;
							}
						}
					}
				}
				return false;
			}
			catch (System.Runtime.InteropServices.COMException ex)
			{
				tgridViewData.Refresh();				
				ChangeEditFlag(false);
				PCSMessageBox.Show(ErrorCode.MESSAGE_COM_TRANSACTION, MessageBoxIcon.Error);
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
			finally
			{
			}			
		}		
		
		
		//**************************************************************************
		/// <summary>
		/// Validate data before saving
		/// </summary>
		/// <returns></returns>
		/// <Author> Thien HD, Jan 07, 2005</Author>
		private bool ValidateData()
		{
			return true;
		}
		
		
		//**************************************************************************
		/// <summary>
		/// Check mandatory fields
		/// </summary>
		/// <param name="pobjControl"></param>
		/// <returns></returns>
		/// <Author> Thien HD, Jan 07, 2005</Author>
		private bool CheckMandatory(Control pobjControl)
		{
			if (pobjControl.Text.Trim() == string.Empty)
			{
				return false;
			}
			return true;
		}

		
		#endregion Class's Method

		#region Event Processing
		
		private void ManageRole_Load(object sender, System.EventArgs e)
		{
			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.WaitCursor;

			#endregion Code Inserted Automatically

			
			const string METHOD_NAME = THIS + ".ManageRole_Load()";
			//Call the LoadForm method to load data for the first time
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
					PCSMessageBox.Show(ErrorCode.MESSAGE_YOU_HAVE_NO_RIGHT_TO_VIEW ,MessageBoxIcon.Warning);
					return;
				}
				#endregion

				LoadForm();
				ChangeEditFlag(false); // set the edit, update to false

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
		
				
		private void btnAdd_Click(object sender, System.EventArgs e)
		{
			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.WaitCursor;

			#endregion Code Inserted Automatically

			
			const string METHOD_NAME = THIS + ".bntAdd_Click()";
			try
			{
				//' This "Add New" button moves the cursor to the
				//' "new (blank) row" at the end so that user can start
				//' adding data to the new record.
				//' Move to last record so that "new row" will be visible

				//' Move the cursor to the "addnew row", and set focus to the grid
				
				CurrencyManager cm;

				//cm = (CurrencyManager) tgridViewTable.BindingContext[tgridViewTable.DataSource, tgridViewTable.DataMember];
				cm = (CurrencyManager) tgridViewData.BindingContext[tgridViewData.DataSource,tgridViewData.DataMember];
				cm.EndCurrentEdit();

				tgridViewData.Refresh();
				tgridViewData.MoveLast();
				tgridViewData.Row = tgridViewData.Row + 1;
				cm.AddNew();

				tgridViewData.Select();

				//Change the flag to Edit 
				ChangeEditFlag(true);
			}			
			catch (NoNullAllowedException ex) 
			{
				PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Error);
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
			tgridViewData.Select();

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

		
		private void ManageRole_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if (blnEditUpdateData)
			{
				System.Windows.Forms.DialogResult dlgResult = PCSMessageBox.Show(ErrorCode.MESSAGE_QUESTION_STORE_INTO_DATABASE, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
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

				/*
				if (PCSMessageBox.Show(ErrorCode.MESSAGE_QUESTION_STORE_INTO_DATABASE, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
				{
					if (!SaveToDatabase())
					{
						e.Cancel = true;	
					}
				}
				*/
			}
		}

		
		private void btnHelp_Click(object sender, System.EventArgs e)
		{// Code Inserted Automatically
#region Code Inserted Automatically
this.Cursor = Cursors.WaitCursor;
#endregion Code Inserted Automatically

// Code Inserted Automatically
#region Code Inserted Automatically
this.Cursor = Cursors.Default;
#endregion Code Inserted Automatically

		}

				
		/// <summary>
		/// Update the current row information into text boxes
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <Author> Thien HD, Jan 07, 2005</Author>
		private void tgridViewData_RowColChange(object sender, C1.Win.C1TrueDBGrid.RowColChangeEventArgs e)
		{
			const string METHOD_NAME = THIS + ".tgridViewData_RowColChange()";
			try
			{
				if (tgridViewData[tgridViewData.Row, Sys_RoleTable.NAME_FLD].ToString().ToLower() == Constants.ADMINISTRATORS.ToLower())
				{
					btnDelete.Enabled = false;
				}
				else
					btnDelete.Enabled = true;
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

		
		/// <summary>
		/// Delete the current row from True DbGrid
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <Author> Thien HD, Jan 07, 2005</Author>
		private void btnDelete_Click(object sender, System.EventArgs e)
		{
			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.WaitCursor;

			#endregion Code Inserted Automatically

			
			const int COL_ID = 0;
			if (tgridViewData.RowCount == 0 || tgridViewData.AddNewMode == AddNewModeEnum.AddNewCurrent)
			{
				// Code Inserted Automatically
                #region Code Inserted Automatically
this.Cursor = Cursors.Default;
#endregion Code Inserted Automatically

				return;
			}

			const string METHOD_NAME = THIS + ".btnDelete_Click()";
			DialogResult result;
			result = PCSMessageBox.Show(ErrorCode.MESSAGE_DELETE_RECORD, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
			if (result == DialogResult.Yes)
			{
				try
				{
					int nDeletedID;
					try
					{
						nDeletedID = Convert.ToInt32(tgridViewData[tgridViewData.Row,COL_ID]);
						strDeletedRole = strDeletedRole + nDeletedID + CHR_SEPARATOR;
					}
					catch
					{
					}
					tgridViewData.Delete();
					tgridViewData.UpdateData();
					tgridViewData.Refresh();
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

				
		/// <summary>
		/// Display a message to confirm delete
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <Author> Thien HD, Jan 07, 2005</Author>
		private void tgridViewData_BeforeDelete(object sender, C1.Win.C1TrueDBGrid.CancelEventArgs e)
		{
			if (tgridViewData[tgridViewData.Row, Sys_RoleTable.NAME_FLD].ToString().ToLower() == Constants.ADMINISTRATORS.ToLower())
			{
				e.Cancel = true;
			}
			else
			{
				if (PCSMessageBox.Show(ErrorCode.MESSAGE_DELETE_RECORD, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
				{
					e.Cancel = false;
				}
				else
				{

					e.Cancel = true;
				}
			}
		}

		
		private void tgridViewData_AfterDelete(object sender, System.EventArgs e)
		{
			ChangeEditFlag(true);
		}

		
		private void tgridViewData_AfterInsert(object sender, System.EventArgs e)
		{
			ChangeEditFlag(true);
		}

		
		private void tgridViewData_AfterUpdate(object sender, System.EventArgs e)
		{
			ChangeEditFlag(true);
		}

		
		/// <summary>
		/// Save data into the current row in grid
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <Author> Thien HD, Jan 07, 2005</Author>
		private void btnSave_Click(object sender, System.EventArgs e)
		{// Code Inserted Automatically
#region Code Inserted Automatically
this.Cursor = Cursors.WaitCursor;
#endregion Code Inserted Automatically

// Code Inserted Automatically
#region Code Inserted Automatically
this.Cursor = Cursors.Default;
#endregion Code Inserted Automatically

		}

		
		private void tgridViewData_Click(object sender, System.EventArgs e)
		{// Code Inserted Automatically
#region Code Inserted Automatically
this.Cursor = Cursors.WaitCursor;
#endregion Code Inserted Automatically

// Code Inserted Automatically
#region Code Inserted Automatically
this.Cursor = Cursors.Default;
#endregion Code Inserted Automatically

		}

				
		/// <summary>
		///  Cancel all changes
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <Author> Thien HD, Jan 07, 2005</Author>
		private void btnCancel_Click(object sender, System.EventArgs e)
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
						tgridViewData.RefreshRow();
						
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

				
		
		/// <summary>
		/// Save data into database
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author> Thien HD, Jan 07, 2005</author>
		private void btnSaveToDB_Click(object sender, System.EventArgs e)
		{
			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.WaitCursor;

			#endregion Code Inserted Automatically

			
			if (SaveToDatabase())
			{
				/// HACKED: Thachnn: fix bug 1638
				tgridViewData.MoveLast();
				tgridViewData.Row = tgridViewData.Row + 1;
				tgridViewData.Focus();
				/// ENDHACKED: Thachnn:
			}

			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.Default;

			#endregion Code Inserted Automatically

			
		}
		
		
		private void tgridViewData_AfterColUpdate(object sender, C1.Win.C1TrueDBGrid.ColEventArgs e)
		{
			e.Column.DataColumn.Value = e.Column.DataColumn.Value.ToString().Trim();
		}

		
		private void tgridViewData_BeforeColUpdate(object sender, C1.Win.C1TrueDBGrid.BeforeColUpdateEventArgs e)
		{
			if (e.Column.DataColumn.DataField==Sys_RoleTable.NAME_FLD && 
				e.Column.DataColumn.Value.ToString().Trim() == String.Empty)
			{
				e.Cancel = true;
				blnErrorOccurs = true;
				//PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID);
			}
			else 
			{
				e.Cancel = false;
			}
		}

		
		private void tgridViewData_AfterColEdit(object sender, C1.Win.C1TrueDBGrid.ColEventArgs e)
		{
			e.Column.DataColumn.Value = e.Column.DataColumn.Value.ToString().Trim();
		}

		
		private void tgridViewData_BeforeInsert(object sender, C1.Win.C1TrueDBGrid.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".tgridViewData_BeforeInsert()";
			try 
			{
				CurrencyManager cm;

				//cm = (CurrencyManager) tgridViewTable.BindingContext[tgridViewTable.DataSource, tgridViewTable.DataMember];
				cm = (CurrencyManager) tgridViewData.BindingContext[tgridViewData.DataSource,tgridViewData.DataMember];
				//End current edit to en-force constraint on the table
				cm.EndCurrentEdit();
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

		
		private void tgridViewData_BeforeUpdate(object sender, C1.Win.C1TrueDBGrid.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".tgridViewData_BeforeUpdate()";
			try 
			{
				CurrencyManager cm;

				//cm = (CurrencyManager) tgridViewTable.BindingContext[tgridViewTable.DataSource, tgridViewTable.DataMember];
				cm = (CurrencyManager) tgridViewData.BindingContext[tgridViewData.DataSource,tgridViewData.DataMember];
				
				// HACK: DuongNA 2005-10-13
				//Check ourselves before we can believe in Exception :((
				DataRowView obj = (DataRowView)cm.Current;
				if (obj.Row[Sys_RoleTable.NAME_FLD].ToString().Equals(string.Empty))
				{
					e.Cancel = true;
					blnErrorOccurs = true;
					tgridViewData.Focus();
					return;
				}
				// End DuongNA 2005-10-13

				//End current edit to en-force constraint on the table
				cm.EndCurrentEdit();

				e.Cancel = false;
				blnErrorOccurs = false;
			}
			catch (NoNullAllowedException ex) 
			{
				e.Cancel = true;
				blnErrorOccurs = true;
				tgridViewData.Focus();
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
				blnErrorOccurs = true;
				tgridViewData.Focus();
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
				blnErrorOccurs = true;
				tgridViewData.Focus();
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

		
		private void tgridViewData_BeforeRowColChange(object sender, C1.Win.C1TrueDBGrid.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".tgridViewData_BeforeRowColChange";
			try
			{
				if (tgridViewData.Columns[Sys_RoleTable.NAME_FLD].Value.ToString().Trim() == String.Empty)
				{
					//e.Cancel = true;
				}
				
				else
				{
					/// HACKED: Thachnn: fix bug
					if(blnEditUpdateData)				
					{
						ChangeEditFlag(true);				
					}					
					/// ENDHACKED: Thachnn
				
				e.Cancel = false;
				}
			}catch (Exception ex)
			{
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


		#region HACKED: Thachnn: fix bug: 1660	
			
		private void tgridViewData_Leave(object sender, System.EventArgs e)
		{		
			if(blnEditUpdateData)
			{
				btnSaveToDB.Enabled = true;
			}
		}

		private void tgridViewData_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
		{			
			if (e.KeyCode != Keys.Up &&
				e.KeyCode != Keys.Down &&
				e.KeyCode != Keys.Left &&
				e.KeyCode != Keys.Right &&
				e.KeyCode != Keys.Enter &&
				e.KeyCode != Keys.Escape &&
				e.KeyCode != Keys.Tab
				)
			{					
					ChangeEditFlag(true);		
			}	
		}

		#endregion

		private void tgridViewData_BeforeColEdit(object sender, C1.Win.C1TrueDBGrid.BeforeColEditEventArgs e)
		{
			if (tgridViewData[tgridViewData.Row, Sys_RoleTable.NAME_FLD].ToString().ToLower() == Constants.ADMINISTRATORS.ToLower())
			{
				e.Cancel = true;
			}
		}

		#endregion Event Processing
	}		
}
