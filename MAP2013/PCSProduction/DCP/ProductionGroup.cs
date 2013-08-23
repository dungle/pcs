using System;
using System.Data;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

//Using PCS's namespaces
using PCSComProduction.DCP.BO;
using PCSComUtils.Common;
using PCSComUtils.Common.BO;
using PCSComUtils.PCSExc;
using PCSUtils.Log;
using PCSUtils.Utils;

namespace PCSProduction.DCP
{
	/// <summary>
	/// Summary description for ProductGroup.
	/// </summary>
	public class ProductionGroup : System.Windows.Forms.Form
	{
		#region Windows Generation Declaration

		private C1.Win.C1List.C1Combo cboCCN;
		private System.Windows.Forms.Label lblCCN;
		private System.Windows.Forms.Label lbProductGroupList;				
		private C1.Win.C1TrueDBGrid.C1TrueDBGrid dgrdData;
		private System.Windows.Forms.Button btnSave;
		private System.Windows.Forms.Button btnHelp;
		private System.Windows.Forms.Button btnClose;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		#endregion Windows Generation Decalaration

		#region Declaration

		#region Constants
		
		private const string THIS = "PCSProduction.DCP.ProductGroup";		
		private const string ZERO_STRING = "0";	
		
		#endregion Constants
		
		private bool blnDataIsValid = false;		
		private int intCurrentRow = 0;
		private DataTable dtbGridLayOut;
		private DataTable dtbData;		
		private ProductionGroupBO boProductGroup;

		#endregion Declaration
		
		#region Constructor, Destructor
		public ProductionGroup()
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(ProductionGroup));
			this.dgrdData = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
			this.btnSave = new System.Windows.Forms.Button();
			this.btnHelp = new System.Windows.Forms.Button();
			this.btnClose = new System.Windows.Forms.Button();
			this.lbProductGroupList = new System.Windows.Forms.Label();
			this.cboCCN = new C1.Win.C1List.C1Combo();
			this.lblCCN = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.dgrdData)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.cboCCN)).BeginInit();
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
			this.dgrdData.MultiSelect = C1.Win.C1TrueDBGrid.MultiSelectEnum.None;
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
			this.dgrdData.KeyDown += new KeyEventHandler(dgrdData_KeyDown);
			this.dgrdData.ButtonClick += new C1.Win.C1TrueDBGrid.ColEventHandler(this.dgrdData_ButtonClick);
			this.dgrdData.AfterColUpdate += new C1.Win.C1TrueDBGrid.ColEventHandler(this.dgrdData_AfterColUpdate);
			this.dgrdData.BeforeColUpdate += new C1.Win.C1TrueDBGrid.BeforeColUpdateEventHandler(this.dgrdData_BeforeColUpdate);
			this.dgrdData.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgrdData_KeyDown);
			this.dgrdData.PropBag = resources.GetString("dgrdData.PropBag");
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
			// lbProductGroupList
			// 
			this.lbProductGroupList.AccessibleDescription = resources.GetString("lbProductGroupList.AccessibleDescription");
			this.lbProductGroupList.AccessibleName = resources.GetString("lbProductGroupList.AccessibleName");
			this.lbProductGroupList.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lbProductGroupList.Anchor")));
			this.lbProductGroupList.AutoSize = ((bool)(resources.GetObject("lbProductGroupList.AutoSize")));
			this.lbProductGroupList.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lbProductGroupList.Dock")));
			this.lbProductGroupList.Enabled = ((bool)(resources.GetObject("lbProductGroupList.Enabled")));
			this.lbProductGroupList.Font = ((System.Drawing.Font)(resources.GetObject("lbProductGroupList.Font")));
			this.lbProductGroupList.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lbProductGroupList.Image = ((System.Drawing.Image)(resources.GetObject("lbProductGroupList.Image")));
			this.lbProductGroupList.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lbProductGroupList.ImageAlign")));
			this.lbProductGroupList.ImageIndex = ((int)(resources.GetObject("lbProductGroupList.ImageIndex")));
			this.lbProductGroupList.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lbProductGroupList.ImeMode")));
			this.lbProductGroupList.Location = ((System.Drawing.Point)(resources.GetObject("lbProductGroupList.Location")));
			this.lbProductGroupList.Name = "lbProductGroupList";
			this.lbProductGroupList.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lbProductGroupList.RightToLeft")));
			this.lbProductGroupList.Size = ((System.Drawing.Size)(resources.GetObject("lbProductGroupList.Size")));
			this.lbProductGroupList.TabIndex = ((int)(resources.GetObject("lbProductGroupList.TabIndex")));
			this.lbProductGroupList.Text = resources.GetString("lbProductGroupList.Text");
			this.lbProductGroupList.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lbProductGroupList.TextAlign")));
			this.lbProductGroupList.Visible = ((bool)(resources.GetObject("lbProductGroupList.Visible")));
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
				" 116, 156</ClientRect><VScrollBar><Width>16</Width></VScrollBar><HScrollBar><Hei" +
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
				"th>16</DefaultRecSelWidth></Blob>";
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
			// ProductionGroup
			// 
			this.AccessibleDescription = resources.GetString("$this.AccessibleDescription");
			this.AccessibleName = resources.GetString("$this.AccessibleName");
			this.AutoScaleBaseSize = ((System.Drawing.Size)(resources.GetObject("$this.AutoScaleBaseSize")));
			this.AutoScroll = ((bool)(resources.GetObject("$this.AutoScroll")));
			this.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("$this.AutoScrollMargin")));
			this.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("$this.AutoScrollMinSize")));
			this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
			this.ClientSize = ((System.Drawing.Size)(resources.GetObject("$this.ClientSize")));
			this.Controls.Add(this.dgrdData);
			this.Controls.Add(this.btnSave);
			this.Controls.Add(this.btnHelp);
			this.Controls.Add(this.btnClose);
			this.Controls.Add(this.cboCCN);
			this.Controls.Add(this.lblCCN);
			this.Controls.Add(this.lbProductGroupList);
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
			this.Name = "ProductionGroup";
			this.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("$this.RightToLeft")));
			this.StartPosition = ((System.Windows.Forms.FormStartPosition)(resources.GetObject("$this.StartPosition")));
			this.Text = resources.GetString("$this.Text");
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ProductGroup_KeyDown);
			this.Closing += new System.ComponentModel.CancelEventHandler(this.ProductGroup_Closing);
			this.Load += new System.EventHandler(this.ProductionGroup_Load);
			((System.ComponentModel.ISupportInitialize)(this.dgrdData)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.cboCCN)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion		
		
		#region Methods
		
		/// <summary>
		/// Validate data before updating into database
		/// </summary>
		/// <returns></returns>
		/// <author> Tran Quoc Tuan </author>
		/// <created date> 
		/// 09 Nov 2005
		/// </created>
		private bool ValidateData()
		{
            if (cboCCN.SelectedIndex < 0)
            {
                PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                cboCCN.Focus();
                return false;
            }

            //Call update data to force grid update data
            dgrdData.UpdateData();

            //valid data in the grid
            for (int i = 0; i < dgrdData.RowCount; i++)
            {
                if (dgrdData[i, PRO_ProductionGroupTable.TABLE_NAME + PRO_ProductionGroupTable.DESCRIPTION_FLD].Equals(DBNull.Value)
                || dgrdData[i, PRO_ProductionGroupTable.TABLE_NAME + PRO_ProductionGroupTable.DESCRIPTION_FLD].ToString().Equals(string.Empty)
                  )
                {
                    PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Exclamation);
                    dgrdData.Row = i;
                    dgrdData.Col = dgrdData.Columns.IndexOf(dgrdData.Columns[PRO_ProductionGroupTable.TABLE_NAME + PRO_ProductionGroupTable.DESCRIPTION_FLD]);
                    dgrdData.Focus();
                    return false;
                }

                if (dgrdData[i, ITM_ProductTable.TABLE_NAME + ITM_ProductTable.CODE_FLD].Equals(DBNull.Value)
                    || dgrdData[i, ITM_ProductTable.TABLE_NAME + ITM_ProductTable.CODE_FLD].ToString().Equals(string.Empty)
                    )
                {
                    PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Exclamation);
                    dgrdData.Row = i;
                    dgrdData.Col = dgrdData.Columns.IndexOf(dgrdData.Columns[ITM_ProductTable.TABLE_NAME + ITM_ProductTable.CODE_FLD]);
                    dgrdData.Focus();
                    return false;
                }

                if (dgrdData[i, ITM_ProductTable.TABLE_NAME + ITM_ProductTable.DESCRIPTION_FLD].Equals(DBNull.Value)
                    || dgrdData[i, ITM_ProductTable.TABLE_NAME + ITM_ProductTable.DESCRIPTION_FLD].ToString().Equals(string.Empty)
                    )
                {
                    PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Exclamation);
                    dgrdData.Row = i;
                    dgrdData.Col = dgrdData.Columns.IndexOf(dgrdData.Columns[ITM_ProductTable.TABLE_NAME + ITM_ProductTable.DESCRIPTION_FLD]);
                    dgrdData.Focus();
                    return false;
                }
            }

            return true;
		}
		
		private void LockControl(bool pblnLockControl)
		{
            dgrdData.AllowAddNew = !pblnLockControl;
            dgrdData.AllowDelete = !pblnLockControl;
            dgrdData.AllowUpdate = !pblnLockControl;

            foreach (C1.Win.C1TrueDBGrid.C1DisplayColumn dspColum in dgrdData.Splits[0].DisplayColumns)
            {
                dspColum.Locked = !pblnLockControl;
            }

            //Unlock columns
            dgrdData.Splits[0].DisplayColumns[PRO_ProductionGroupTable.TABLE_NAME + PRO_ProductionGroupTable.DESCRIPTION_FLD].Locked = pblnLockControl;
            dgrdData.Splits[0].DisplayColumns[ITM_ProductTable.TABLE_NAME + ITM_ProductTable.CODE_FLD].Locked = pblnLockControl;
            dgrdData.Splits[0].DisplayColumns[ITM_ProductTable.TABLE_NAME + ITM_ProductTable.DESCRIPTION_FLD].Locked = pblnLockControl;

            //Set select buttons for grid				
            dgrdData.Splits[0].DisplayColumns[PRO_ProductionGroupTable.TABLE_NAME + PRO_ProductionGroupTable.DESCRIPTION_FLD].Button = !pblnLockControl;
            dgrdData.Splits[0].DisplayColumns[ITM_ProductTable.TABLE_NAME + ITM_ProductTable.CODE_FLD].Button = !pblnLockControl;
            dgrdData.Splits[0].DisplayColumns[ITM_ProductTable.TABLE_NAME + ITM_ProductTable.DESCRIPTION_FLD].Button = !pblnLockControl;

            //Lock controls
            btnSave.Enabled = !pblnLockControl;
		}

		private void FormatDataGrid()
		{
			//Restore layout
			FormControlComponents.RestoreGridLayout(dgrdData, dtbGridLayOut);

			//Setting filter bar
			dgrdData.FilterBar = true;
			dgrdData.AllowFilter = true;			

			dgrdData.MultiSelect = C1.Win.C1TrueDBGrid.MultiSelectEnum.Extended;
			//Change display format				
			dgrdData.Columns[PRO_ProductionGroupTable.GROUPPRODUCTIONMAX_FLD].NumberFormat = Constants.DECIMAL_NUMBERFORMAT;
		}

		/// <summary>
		/// Load data into grid based on a specific Production Line
		/// </summary>
		/// <param name="pintProductGroupId"></param>
		private void LoadData()
		{
			dtbData = boProductGroup.GetAllData();				

			//Bind to grid then format
			dgrdData.DataSource = dtbData;				
			FormatDataGrid();			
		}
		
		/// <summary>
		/// Reset value of controls
		/// </summary>		
		/// <author> Tran Quoc Tuan </author>
		/// <created date> 
		/// 09 November 2005
		/// </created>
		private void ResetControlValue(bool pblnClearProLine)
		{
			//Clear data grid
			LoadData();			
		}
		
		/// <summary>
		/// process data inputed into the grid
		/// </summary>
		/// <param name="pstrColumnName"></param>
		/// <param name="pstrColumValue"></param>
		/// <returns>If no error then return true, else return false</returns>
		private bool ProcessInputDataInGrid(string pstrColumnName, string pstrColumValue, bool pblnAlwaysShow)
		{
			const string METHOD_NAME = THIS +  ".ProcessInputDataInGrid()";
			const string PRODUCT_GROUP_VIEW = "v_ProductGroupInfo";
			const string PRODUCT_WITH_PRODUCTION_LINE_VIEW = "v_ProductWithProductionLineInfo";
			const string CATEGORY_FLD = "ITM_CategoryCode";

			try
			{	
				Hashtable htbCondition = new Hashtable();
				DataRowView drvResult = null;
				string[] arrMessage = new string[2];
				bool blnResult = true;

				//Check Cycle no
				if (cboCCN.SelectedIndex < 0)
				{
					PCSMessageBox.Show(ErrorCode.MESSAGE_MATERIALRECEIPT_SELECT_CNN, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					cboCCN.Focus();
					return false;
				}

				//Check for each column
				switch (pstrColumnName)
				{
					case PRO_ProductionGroupTable.TABLE_NAME + PRO_ProductionGroupTable.DESCRIPTION_FLD:
						//Clear hash table for new condition
						htbCondition.Clear();
						htbCondition.Add(MST_CCNTable.CCNID_FLD, cboCCN.SelectedValue);
												
						drvResult = FormControlComponents.OpenSearchForm(PRODUCT_GROUP_VIEW, PRO_ProductionGroupTable.DESCRIPTION_FLD, pstrColumValue, htbCondition, pblnAlwaysShow);

						// Call OpenSearchForm for Work Center selecting
						if(drvResult != null)
						{
							if(!dgrdData[dgrdData.Row, PRO_ProductionGroupTable.PRODUCTIONGROUPID_FLD].Equals(drvResult[PRO_ProductionGroupTable.PRODUCTIONGROUPID_FLD]))
							{
								if(pblnAlwaysShow)
								{
									dgrdData[dgrdData.Row, PRO_PGProductTable.PRODUCTID_FLD] = DBNull.Value;
									dgrdData[dgrdData.Row, ITM_ProductTable.TABLE_NAME + ITM_ProductTable.CODE_FLD] = DBNull.Value;
									dgrdData[dgrdData.Row, ITM_ProductTable.TABLE_NAME + ITM_ProductTable.DESCRIPTION_FLD] = DBNull.Value;
									dgrdData[dgrdData.Row, ITM_ProductTable.TABLE_NAME + ITM_ProductTable.REVISION_FLD] = DBNull.Value;
									dgrdData[dgrdData.Row, ITM_CategoryTable.TABLE_NAME + ITM_CategoryTable.CODE_FLD] = DBNull.Value;
								}
								else
								{
									dgrdData.Columns[PRO_PGProductTable.PRODUCTID_FLD].Value = DBNull.Value;
									dgrdData.Columns[ITM_ProductTable.TABLE_NAME + ITM_ProductTable.CODE_FLD].Value = DBNull.Value;
									dgrdData.Columns[ITM_ProductTable.TABLE_NAME + ITM_ProductTable.DESCRIPTION_FLD].Value = DBNull.Value;
									dgrdData.Columns[ITM_ProductTable.TABLE_NAME + ITM_ProductTable.REVISION_FLD].Value = DBNull.Value;
									dgrdData.Columns[ITM_CategoryTable.TABLE_NAME + ITM_CategoryTable.CODE_FLD].Value = DBNull.Value;
								}
							
								//Enable Save button
								btnSave.Enabled = true;								
							}

							//Fill selected data
							if(pblnAlwaysShow)
							{
								dgrdData[dgrdData.Row, PRO_ProductionGroupTable.PRODUCTIONGROUPID_FLD] = drvResult[PRO_ProductionGroupTable.PRODUCTIONGROUPID_FLD];
								dgrdData[dgrdData.Row, PRO_ProductionGroupTable.TABLE_NAME + PRO_ProductionGroupTable.DESCRIPTION_FLD] = drvResult[PRO_ProductionGroupTable.DESCRIPTION_FLD];
								dgrdData[dgrdData.Row, PRO_ProductionGroupTable.GROUPPRODUCTIONMAX_FLD] = drvResult[PRO_ProductionGroupTable.GROUPPRODUCTIONMAX_FLD];
								dgrdData[dgrdData.Row, PRO_ProductionGroupTable.PRODUCTIONLINEID_FLD] = drvResult[PRO_ProductionGroupTable.PRODUCTIONLINEID_FLD];
								dgrdData[dgrdData.Row, PRO_ProductionLineTable.TABLE_NAME + PRO_ProductionLineTable.CODE_FLD] = drvResult[PRO_ProductionLineTable.TABLE_NAME + PRO_ProductionLineTable.CODE_FLD];
							}
							else
							{
								dgrdData.Columns[PRO_ProductionGroupTable.PRODUCTIONGROUPID_FLD].Value = drvResult[PRO_ProductionGroupTable.PRODUCTIONGROUPID_FLD];
								dgrdData.Columns[PRO_ProductionGroupTable.TABLE_NAME + PRO_ProductionGroupTable.DESCRIPTION_FLD].Value = drvResult[PRO_ProductionGroupTable.DESCRIPTION_FLD];
								dgrdData.Columns[PRO_ProductionGroupTable.GROUPPRODUCTIONMAX_FLD].Value = drvResult[PRO_ProductionGroupTable.GROUPPRODUCTIONMAX_FLD];
								dgrdData.Columns[PRO_ProductionLineTable.TABLE_NAME + PRO_ProductionLineTable.CODE_FLD].Value = drvResult[PRO_ProductionLineTable.TABLE_NAME + PRO_ProductionLineTable.CODE_FLD];
								dgrdData.Columns[PRO_ProductionLineTable.PRODUCTIONLINEID_FLD].Value = drvResult[PRO_ProductionLineTable.PRODUCTIONLINEID_FLD];
							}
						}
						else
						{
							blnResult = pblnAlwaysShow;
						}
						break;
					
					case ITM_ProductTable.TABLE_NAME + ITM_ProductTable.CODE_FLD:
						if(dgrdData.Columns[PRO_ProductionGroupTable.PRODUCTIONLINEID_FLD].Value == DBNull.Value
						|| dgrdData.Columns[PRO_ProductionGroupTable.PRODUCTIONLINEID_FLD].Text.Trim() == string.Empty
						)
						{
							arrMessage[0] = dgrdData.Columns[PRO_ProductionGroupTable.TABLE_NAME + PRO_ProductionGroupTable.DESCRIPTION_FLD].Caption;
							arrMessage[1] = dgrdData.Columns[ITM_ProductTable.TABLE_NAME + ITM_ProductTable.CODE_FLD].Caption;

							PCSMessageBox.Show(ErrorCode.MESSAGE_SELECT_ONE_BEFORE_SELECT_ONE, MessageBoxIcon.Exclamation, arrMessage);
							dgrdData.Col = dgrdData.Columns.IndexOf(dgrdData.Columns[PRO_ProductionGroupTable.TABLE_NAME + PRO_ProductionGroupTable.DESCRIPTION_FLD]);
							dgrdData.Focus();
							return false;
						}

						//Clear hash table for new condition
						htbCondition.Clear();
						htbCondition.Add(ITM_ProductTable.MAKEITEM_FLD, 1);
						htbCondition.Add(ITM_ProductTable.PRODUCTIONLINEID_FLD, dgrdData.Columns[PRO_ProductionGroupTable.PRODUCTIONLINEID_FLD].Value);						
						
						// Call OpenSearchForm for Work Center selecting						
						drvResult = FormControlComponents.OpenSearchForm(PRODUCT_WITH_PRODUCTION_LINE_VIEW, ITM_ProductTable.CODE_FLD, pstrColumValue, htbCondition, pblnAlwaysShow);

						if(drvResult != null)
						{							
							if(!dgrdData[dgrdData.Row, PRO_PGProductTable.PRODUCTID_FLD].Equals(drvResult[PRO_PGProductTable.PRODUCTID_FLD]))
							{
								//Enable Save button
								btnSave.Enabled = true;								
							}

							//Fill selected data
							if(pblnAlwaysShow)
							{
								dgrdData[dgrdData.Row, PRO_PGProductTable.PRODUCTID_FLD] = drvResult[ITM_ProductTable.PRODUCTID_FLD];
								dgrdData[dgrdData.Row, ITM_ProductTable.TABLE_NAME + ITM_ProductTable.CODE_FLD] = drvResult[ITM_ProductTable.CODE_FLD];
								dgrdData[dgrdData.Row, ITM_ProductTable.TABLE_NAME + ITM_ProductTable.DESCRIPTION_FLD] = drvResult[ITM_ProductTable.DESCRIPTION_FLD];
								dgrdData[dgrdData.Row, ITM_ProductTable.TABLE_NAME + ITM_ProductTable.REVISION_FLD] = drvResult[ITM_ProductTable.REVISION_FLD];
								dgrdData[dgrdData.Row, ITM_CategoryTable.TABLE_NAME + ITM_CategoryTable.CODE_FLD] = drvResult[CATEGORY_FLD];
							}
							else
							{
								dgrdData.Columns[PRO_PGProductTable.PRODUCTID_FLD].Value = drvResult[ITM_ProductTable.PRODUCTID_FLD];
								dgrdData.Columns[ITM_ProductTable.TABLE_NAME + ITM_ProductTable.CODE_FLD].Value = drvResult[ITM_ProductTable.CODE_FLD];
								dgrdData.Columns[ITM_ProductTable.TABLE_NAME + ITM_ProductTable.DESCRIPTION_FLD].Value = drvResult[ITM_ProductTable.DESCRIPTION_FLD];
								dgrdData.Columns[ITM_ProductTable.TABLE_NAME + ITM_ProductTable.REVISION_FLD].Value = drvResult[ITM_ProductTable.REVISION_FLD];
								dgrdData.Columns[ITM_CategoryTable.TABLE_NAME + ITM_CategoryTable.CODE_FLD].Value = drvResult[CATEGORY_FLD];								
							}
						}
						else
						{
							blnResult = pblnAlwaysShow;
						}
						break;

					case ITM_ProductTable.TABLE_NAME + ITM_ProductTable.DESCRIPTION_FLD:
						if(dgrdData.Columns[PRO_ProductionGroupTable.PRODUCTIONLINEID_FLD].Value == DBNull.Value
							|| dgrdData.Columns[PRO_ProductionGroupTable.PRODUCTIONLINEID_FLD].Text.Trim() == string.Empty
							)
						{
							arrMessage[0] = dgrdData.Columns[PRO_ProductionGroupTable.TABLE_NAME + PRO_ProductionGroupTable.DESCRIPTION_FLD].Caption;
							arrMessage[1] = dgrdData.Columns[ITM_ProductTable.TABLE_NAME + ITM_ProductTable.DESCRIPTION_FLD].Caption;

							PCSMessageBox.Show(ErrorCode.MESSAGE_SELECT_ONE_BEFORE_SELECT_ONE, MessageBoxIcon.Exclamation, arrMessage);
							dgrdData.Col = dgrdData.Columns.IndexOf(dgrdData.Columns[PRO_ProductionGroupTable.TABLE_NAME + PRO_ProductionGroupTable.DESCRIPTION_FLD]);
							dgrdData.Focus();
							return false;
						}

						//Clear hash table for new condition
						htbCondition.Clear();
						htbCondition.Add(ITM_ProductTable.MAKEITEM_FLD, 1);
						htbCondition.Add(ITM_ProductTable.PRODUCTIONLINEID_FLD, dgrdData.Columns[PRO_ProductionGroupTable.PRODUCTIONLINEID_FLD].Value);						
						
						// Call OpenSearchForm for Work Center selecting						
						drvResult = FormControlComponents.OpenSearchForm(PRODUCT_WITH_PRODUCTION_LINE_VIEW, ITM_ProductTable.DESCRIPTION_FLD, pstrColumValue, htbCondition, pblnAlwaysShow);

						if(drvResult != null)
						{							
							if(!dgrdData[dgrdData.Row, PRO_PGProductTable.PRODUCTID_FLD].Equals(drvResult[PRO_PGProductTable.PRODUCTID_FLD]))
							{
								//Enable Save button
								btnSave.Enabled = true;								
							}

							//Fill selected data
							if(pblnAlwaysShow)
							{
								dgrdData[dgrdData.Row, PRO_PGProductTable.PRODUCTID_FLD] = drvResult[ITM_ProductTable.PRODUCTID_FLD];
								dgrdData[dgrdData.Row, ITM_ProductTable.TABLE_NAME + ITM_ProductTable.CODE_FLD] = drvResult[ITM_ProductTable.CODE_FLD];
								dgrdData[dgrdData.Row, ITM_ProductTable.TABLE_NAME + ITM_ProductTable.DESCRIPTION_FLD] = drvResult[ITM_ProductTable.DESCRIPTION_FLD];
								dgrdData[dgrdData.Row, ITM_ProductTable.TABLE_NAME + ITM_ProductTable.REVISION_FLD] = drvResult[ITM_ProductTable.REVISION_FLD];
								dgrdData[dgrdData.Row, ITM_CategoryTable.TABLE_NAME + ITM_CategoryTable.CODE_FLD] = drvResult[CATEGORY_FLD];
							}
							else
							{
								dgrdData.Columns[PRO_PGProductTable.PRODUCTID_FLD].Value = drvResult[ITM_ProductTable.PRODUCTID_FLD];
								dgrdData.Columns[ITM_ProductTable.TABLE_NAME + ITM_ProductTable.CODE_FLD].Value = drvResult[ITM_ProductTable.CODE_FLD];
								dgrdData.Columns[ITM_ProductTable.TABLE_NAME + ITM_ProductTable.DESCRIPTION_FLD].Value = drvResult[ITM_ProductTable.DESCRIPTION_FLD];
								dgrdData.Columns[ITM_ProductTable.TABLE_NAME + ITM_ProductTable.REVISION_FLD].Value = drvResult[ITM_ProductTable.REVISION_FLD];
								dgrdData.Columns[ITM_CategoryTable.TABLE_NAME + ITM_CategoryTable.CODE_FLD].Value = drvResult[CATEGORY_FLD];								
							}
						}
						else
						{
							blnResult = pblnAlwaysShow;
						}
						break;
				}

				return blnResult;
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
		
		#endregion Methods		
		
		#region Event Prcessing
		

		private void ProductionGroup_Load(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".ProductionGroup_Load()";
			try
			{
				this.Name = THIS;
				
				boProductGroup = new ProductionGroupBO();

				//Set form security
				Security objSecurity = new Security();		
				
				if (objSecurity.SetRightForUserOnForm(this, SystemProperty.UserName) == 0)
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
				if (SystemProperty.CCNID != 0)
				{
					cboCCN.SelectedValue = SystemProperty.CCNID;
				}
				
				//get grid layout
				dtbGridLayOut = FormControlComponents.StoreGridLayout(dgrdData);
				
				//Clear form
				ResetControlValue(true);
				LockControl(false);
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
			DataTable dtbBackup = null;
			try
			{
				// check data, if data is invalid then exit immediately
				blnDataIsValid = ValidateData() && !dgrdData.EditActive;;
				if(!blnDataIsValid) return;			

				dtbBackup = dtbData.Copy();

				//Build dataset object
				DataSet dtsData = dtbData.DataSet;
				if(dtsData == null)
				{
					dtsData = new DataSet();
					dtsData.Tables.Add(dtbData);
				}

				//update dataset object
				boProductGroup.UpdateDataSet(dtsData);				

				//Reload detail grid				
				LoadData();
				
				LockControl(false);

				//Show alert message
				PCSMessageBox.Show(ErrorCode.MESSAGE_AFTER_SAVE_DATA, MessageBoxButtons.OK, MessageBoxIcon.Information);				

				btnSave.Enabled = false;
			}
			catch (PCSException ex)
			{	
				blnDataIsValid = false;			

				try
				{
					if(ex.mCode == ErrorCode.DUPLICATE_KEY && dtbBackup != null)
					{
						int iErrorRow = 0;
						foreach(DataRow drow in dtbData.Rows)
						{	
							if(drow.RowError != string.Empty && drow.RowError != null)
							{
								break;
							}

							if(drow.RowState != DataRowState.Deleted) iErrorRow++;							
						}

						dtbData = dtbBackup.Copy();
						dgrdData.DataSource = dtbData;
						FormatDataGrid();
						LockControl(false);
						dgrdData.Row = iErrorRow;
					}
				}
				catch{}
				
				PCSMessageBox.Show(ex.mCode, MessageBoxIcon.Exclamation);
				try
				{
					Logger.LogMessage(ex.CauseException, METHOD_NAME, Level.ERROR);
				}
				catch
				{
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
				}				
			}
			catch (Exception ex)
			{
				blnDataIsValid = false;
				PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
				try
				{
					Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
				}
				catch
				{
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
			finally
			{				
				dgrdData.Focus();
			}
		}

		private void btnHelp_Click(object sender, System.EventArgs e)
		{
		
		}

		private void btnClose_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void ProductGroup_Closing(object sender, CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".DCOptions_Closing()";
			try
			{
				// if the form has been changed then ask to store database
				if(btnSave.Enabled)
				{
					DialogResult enumDialog = PCSMessageBox.Show(ErrorCode.MESSAGE_QUESTION_STORE_INTO_DATABASE, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
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

		private void dgrdData_ButtonClick(object sender, C1.Win.C1TrueDBGrid.ColEventArgs e)
		{
			const string METHOD_NAME = THIS + ".dgrdData_ButtonClick()";

			try
			{	
				//Set edit active				
				dgrdData.EditActive = true;
				
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
				//if column's value then exit immediately
				if(dgrdData.RowCount <= 0) return;
				
				switch (e.KeyCode)
				{
					case Keys.F4:							
						//Set edit active
						dgrdData.EditActive = true;
						
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

		private void dgrdData_BeforeColUpdate(object sender, C1.Win.C1TrueDBGrid.BeforeColUpdateEventArgs e)
		{
			const string METHOD_NAME = THIS + ".dgrdData_BeforeColUpdate()";

			try
			{
				//if column's value then exit immediately
				if(e.Column.DataColumn.Text.Trim().Length == 0)
				{	
					switch (e.Column.DataColumn.DataField)
					{
						case PRO_ProductionGroupTable.TABLE_NAME + PRO_ProductionGroupTable.DESCRIPTION_FLD:
							dgrdData.Columns[PRO_ProductionGroupTable.PRODUCTIONGROUPID_FLD].Value = DBNull.Value;
							dgrdData.Columns[PRO_ProductionGroupTable.TABLE_NAME + PRO_ProductionGroupTable.DESCRIPTION_FLD].Value = DBNull.Value;
							dgrdData.Columns[PRO_ProductionGroupTable.GROUPPRODUCTIONMAX_FLD].Value = DBNull.Value;
							dgrdData.Columns[PRO_ProductionLineTable.TABLE_NAME + PRO_ProductionLineTable.CODE_FLD].Value = DBNull.Value;
							dgrdData.Columns[PRO_ProductionLineTable.PRODUCTIONLINEID_FLD].Value = DBNull.Value;
							break;
					
						case ITM_ProductTable.TABLE_NAME + ITM_ProductTable.CODE_FLD:							
						case ITM_ProductTable.TABLE_NAME + ITM_ProductTable.DESCRIPTION_FLD:
							dgrdData.Columns[PRO_PGProductTable.PRODUCTID_FLD].Value = DBNull.Value;
							dgrdData.Columns[ITM_ProductTable.TABLE_NAME + ITM_ProductTable.CODE_FLD].Value = DBNull.Value;
							dgrdData.Columns[ITM_ProductTable.TABLE_NAME + ITM_ProductTable.DESCRIPTION_FLD].Value = DBNull.Value;
							dgrdData.Columns[ITM_ProductTable.TABLE_NAME + ITM_ProductTable.REVISION_FLD].Value = DBNull.Value;
							dgrdData.Columns[ITM_CategoryTable.TABLE_NAME + ITM_CategoryTable.CODE_FLD].Value = DBNull.Value;
							
							break;
					}

					return;
				}

				e.Cancel = !ProcessInputDataInGrid(e.Column.DataColumn.DataField, e.Column.DataColumn.Value.ToString().Trim(), false);
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

		private void dgrdData_AfterColUpdate(object sender, C1.Win.C1TrueDBGrid.ColEventArgs e)
		{
			const string METHOD_NAME = THIS + ".dgrdData_AfterColUpdate()";

			try
			{	
				btnSave.Enabled = true;
			}
			catch (Exception ex)
			{
				blnDataIsValid = false;
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
			btnSave.Enabled = true;
		}
		
		private void ProductGroup_KeyDown(object sender, KeyEventArgs e)
		{
			const string METHOD_NAME = THIS + ".ProductGroup_KeyDown()";

			try
			{
				//if column's value then exit immediately				
				switch (e.KeyCode)
				{
					case Keys.F12:
						dgrdData.Col = dgrdData.Columns.IndexOf(dgrdData.Columns[PRO_ProductionGroupTable.TABLE_NAME + PRO_ProductionGroupTable.DESCRIPTION_FLD]);
						dgrdData.Row = dgrdData.RowCount;
						dgrdData.Focus();
						break;

					case Keys.Delete:
						FormControlComponents.DeleteMultiRowsOnTrueDBGrid(dgrdData);
						btnSave.Enabled = true;
						break;
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

		#endregion Event Prcessing		
	}
}
