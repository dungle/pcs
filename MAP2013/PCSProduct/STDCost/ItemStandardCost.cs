using System;
using System.Data;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

//using PCS's namespaces
using PCSUtils.Utils;
using PCSComUtils.PCSExc;
using PCSComUtils.Common;
using PCSComProduct.Items.BO;
using PCSComProduct.STDCost.BO;
using PCSComProduct.STDCost.DS;
using PCSUtils.Log;

namespace PCSProduct.STDCost
{
	/// <summary>
	/// Summary description for ItemStandardCost.
	/// </summary>
	public class ItemStandardCost : System.Windows.Forms.Form
	{
		#region Declaration
		
		#region Constants

		private const string THIS = "PCSProduct.STDCost.ItemStandardCost";

		private const string PRODUCT_INFOR_VIEW = "V_ProductInfor";
		private const string PART_NO_FLD = "PartNumber";
		private const string PART_NAME_FLD = "PartName";
		private const string PART_MODEL_FLD = "Model";
		private const string PART_MAKE_ITEM_FLD = "MakeItem";
		private const string STOCK_UM_FLD = "UM";

		private C1.Win.C1Input.C1NumericEdit numCostValue;

		#endregion Constants
		
		#region System Generated

		private System.Windows.Forms.Button btnHelp;
		private System.Windows.Forms.Button btnClose;
		private System.Windows.Forms.Button btnSave;
		public C1.Win.C1TrueDBGrid.C1TrueDBGrid dgrdData;
		private System.Windows.Forms.TextBox txtPartName;
		private System.Windows.Forms.TextBox txtModel;
		private System.Windows.Forms.TextBox txtStockUM;
		private System.Windows.Forms.CheckBox chkMakeItem;
		private System.Windows.Forms.Button btnSearchPartNo;
		private System.Windows.Forms.TextBox txtCurrency;
		private System.Windows.Forms.Button btnSearchPartName;
		private System.Windows.Forms.Button btnDelete;
		private System.Windows.Forms.Label lblPartName;
		private System.Windows.Forms.Label lblPartNo;
		private System.Windows.Forms.Label lblCurrency;
		private System.Windows.Forms.Label lblModel;
		private System.Windows.Forms.Label lblStockUM;
		private System.Windows.Forms.TextBox txtTotal;
		private System.Windows.Forms.Label lblTotal;	
		private System.Windows.Forms.Label lblMakeItem;
		private System.Windows.Forms.TextBox txtPartNo;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		#endregion System Generated

		#region Variables
	
		private bool blnDataIsValid;
		private DataTable dtbGridLayOut = new DataTable();
		private DataTable dtbItemCostDetail;
		private ItemStandardCostBO boItemStandardCost;		
		private int mintProductID;

		#endregion

		#endregion Declaration

		#region Constructor, Destructor

		public ItemStandardCost()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			mintProductID = 0;			
		}

		public ItemStandardCost(int pintProductID)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			mintProductID = pintProductID;			
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
		#endregion

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(ItemStandardCost));
			this.btnHelp = new System.Windows.Forms.Button();
			this.btnClose = new System.Windows.Forms.Button();
			this.btnSave = new System.Windows.Forms.Button();
			this.lblPartName = new System.Windows.Forms.Label();
			this.dgrdData = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
			this.lblPartNo = new System.Windows.Forms.Label();
			this.txtPartName = new System.Windows.Forms.TextBox();
			this.lblModel = new System.Windows.Forms.Label();
			this.txtModel = new System.Windows.Forms.TextBox();
			this.txtStockUM = new System.Windows.Forms.TextBox();
			this.lblStockUM = new System.Windows.Forms.Label();
			this.btnDelete = new System.Windows.Forms.Button();
			this.txtCurrency = new System.Windows.Forms.TextBox();
			this.lblCurrency = new System.Windows.Forms.Label();
			this.chkMakeItem = new System.Windows.Forms.CheckBox();
			this.btnSearchPartNo = new System.Windows.Forms.Button();
			this.btnSearchPartName = new System.Windows.Forms.Button();
			this.txtTotal = new System.Windows.Forms.TextBox();
			this.lblTotal = new System.Windows.Forms.Label();
			this.numCostValue = new C1.Win.C1Input.C1NumericEdit();
			this.lblMakeItem = new System.Windows.Forms.Label();
			this.txtPartNo = new System.Windows.Forms.TextBox();
			((System.ComponentModel.ISupportInitialize)(this.dgrdData)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numCostValue)).BeginInit();
			this.SuspendLayout();
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
			// lblPartName
			// 
			this.lblPartName.AccessibleDescription = resources.GetString("lblPartName.AccessibleDescription");
			this.lblPartName.AccessibleName = resources.GetString("lblPartName.AccessibleName");
			this.lblPartName.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblPartName.Anchor")));
			this.lblPartName.AutoSize = ((bool)(resources.GetObject("lblPartName.AutoSize")));
			this.lblPartName.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblPartName.Dock")));
			this.lblPartName.Enabled = ((bool)(resources.GetObject("lblPartName.Enabled")));
			this.lblPartName.Font = ((System.Drawing.Font)(resources.GetObject("lblPartName.Font")));
			this.lblPartName.ForeColor = System.Drawing.Color.Maroon;
			this.lblPartName.Image = ((System.Drawing.Image)(resources.GetObject("lblPartName.Image")));
			this.lblPartName.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblPartName.ImageAlign")));
			this.lblPartName.ImageIndex = ((int)(resources.GetObject("lblPartName.ImageIndex")));
			this.lblPartName.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblPartName.ImeMode")));
			this.lblPartName.Location = ((System.Drawing.Point)(resources.GetObject("lblPartName.Location")));
			this.lblPartName.Name = "lblPartName";
			this.lblPartName.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblPartName.RightToLeft")));
			this.lblPartName.Size = ((System.Drawing.Size)(resources.GetObject("lblPartName.Size")));
			this.lblPartName.TabIndex = ((int)(resources.GetObject("lblPartName.TabIndex")));
			this.lblPartName.Text = resources.GetString("lblPartName.Text");
			this.lblPartName.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblPartName.TextAlign")));
			this.lblPartName.Visible = ((bool)(resources.GetObject("lblPartName.Visible")));
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
			this.dgrdData.AfterColUpdate += new C1.Win.C1TrueDBGrid.ColEventHandler(this.dgrdData_AfterColUpdate);
			this.dgrdData.BeforeColUpdate += new C1.Win.C1TrueDBGrid.BeforeColUpdateEventHandler(this.dgrdData_BeforeColUpdate);
			this.dgrdData.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgrdData_KeyDown);
			this.dgrdData.PropBag = resources.GetString("dgrdData.PropBag");
			// 
			// lblPartNo
			// 
			this.lblPartNo.AccessibleDescription = resources.GetString("lblPartNo.AccessibleDescription");
			this.lblPartNo.AccessibleName = resources.GetString("lblPartNo.AccessibleName");
			this.lblPartNo.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblPartNo.Anchor")));
			this.lblPartNo.AutoSize = ((bool)(resources.GetObject("lblPartNo.AutoSize")));
			this.lblPartNo.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblPartNo.Dock")));
			this.lblPartNo.Enabled = ((bool)(resources.GetObject("lblPartNo.Enabled")));
			this.lblPartNo.Font = ((System.Drawing.Font)(resources.GetObject("lblPartNo.Font")));
			this.lblPartNo.ForeColor = System.Drawing.Color.Maroon;
			this.lblPartNo.Image = ((System.Drawing.Image)(resources.GetObject("lblPartNo.Image")));
			this.lblPartNo.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblPartNo.ImageAlign")));
			this.lblPartNo.ImageIndex = ((int)(resources.GetObject("lblPartNo.ImageIndex")));
			this.lblPartNo.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblPartNo.ImeMode")));
			this.lblPartNo.Location = ((System.Drawing.Point)(resources.GetObject("lblPartNo.Location")));
			this.lblPartNo.Name = "lblPartNo";
			this.lblPartNo.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblPartNo.RightToLeft")));
			this.lblPartNo.Size = ((System.Drawing.Size)(resources.GetObject("lblPartNo.Size")));
			this.lblPartNo.TabIndex = ((int)(resources.GetObject("lblPartNo.TabIndex")));
			this.lblPartNo.Text = resources.GetString("lblPartNo.Text");
			this.lblPartNo.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblPartNo.TextAlign")));
			this.lblPartNo.Visible = ((bool)(resources.GetObject("lblPartNo.Visible")));
			// 
			// txtPartName
			// 
			this.txtPartName.AccessibleDescription = resources.GetString("txtPartName.AccessibleDescription");
			this.txtPartName.AccessibleName = resources.GetString("txtPartName.AccessibleName");
			this.txtPartName.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("txtPartName.Anchor")));
			this.txtPartName.AutoSize = ((bool)(resources.GetObject("txtPartName.AutoSize")));
			this.txtPartName.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("txtPartName.BackgroundImage")));
			this.txtPartName.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("txtPartName.Dock")));
			this.txtPartName.Enabled = ((bool)(resources.GetObject("txtPartName.Enabled")));
			this.txtPartName.Font = ((System.Drawing.Font)(resources.GetObject("txtPartName.Font")));
			this.txtPartName.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("txtPartName.ImeMode")));
			this.txtPartName.Location = ((System.Drawing.Point)(resources.GetObject("txtPartName.Location")));
			this.txtPartName.MaxLength = ((int)(resources.GetObject("txtPartName.MaxLength")));
			this.txtPartName.Multiline = ((bool)(resources.GetObject("txtPartName.Multiline")));
			this.txtPartName.Name = "txtPartName";
			this.txtPartName.PasswordChar = ((char)(resources.GetObject("txtPartName.PasswordChar")));
			this.txtPartName.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("txtPartName.RightToLeft")));
			this.txtPartName.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("txtPartName.ScrollBars")));
			this.txtPartName.Size = ((System.Drawing.Size)(resources.GetObject("txtPartName.Size")));
			this.txtPartName.TabIndex = ((int)(resources.GetObject("txtPartName.TabIndex")));
			this.txtPartName.Text = resources.GetString("txtPartName.Text");
			this.txtPartName.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("txtPartName.TextAlign")));
			this.txtPartName.Visible = ((bool)(resources.GetObject("txtPartName.Visible")));
			this.txtPartName.WordWrap = ((bool)(resources.GetObject("txtPartName.WordWrap")));
			this.txtPartName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPartName_KeyDown);
			this.txtPartName.Validating += new System.ComponentModel.CancelEventHandler(this.txtPartName_Validating);
			// 
			// lblModel
			// 
			this.lblModel.AccessibleDescription = resources.GetString("lblModel.AccessibleDescription");
			this.lblModel.AccessibleName = resources.GetString("lblModel.AccessibleName");
			this.lblModel.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblModel.Anchor")));
			this.lblModel.AutoSize = ((bool)(resources.GetObject("lblModel.AutoSize")));
			this.lblModel.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblModel.Dock")));
			this.lblModel.Enabled = ((bool)(resources.GetObject("lblModel.Enabled")));
			this.lblModel.Font = ((System.Drawing.Font)(resources.GetObject("lblModel.Font")));
			this.lblModel.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblModel.Image = ((System.Drawing.Image)(resources.GetObject("lblModel.Image")));
			this.lblModel.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblModel.ImageAlign")));
			this.lblModel.ImageIndex = ((int)(resources.GetObject("lblModel.ImageIndex")));
			this.lblModel.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblModel.ImeMode")));
			this.lblModel.Location = ((System.Drawing.Point)(resources.GetObject("lblModel.Location")));
			this.lblModel.Name = "lblModel";
			this.lblModel.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblModel.RightToLeft")));
			this.lblModel.Size = ((System.Drawing.Size)(resources.GetObject("lblModel.Size")));
			this.lblModel.TabIndex = ((int)(resources.GetObject("lblModel.TabIndex")));
			this.lblModel.Text = resources.GetString("lblModel.Text");
			this.lblModel.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblModel.TextAlign")));
			this.lblModel.Visible = ((bool)(resources.GetObject("lblModel.Visible")));
			// 
			// txtModel
			// 
			this.txtModel.AccessibleDescription = resources.GetString("txtModel.AccessibleDescription");
			this.txtModel.AccessibleName = resources.GetString("txtModel.AccessibleName");
			this.txtModel.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("txtModel.Anchor")));
			this.txtModel.AutoSize = ((bool)(resources.GetObject("txtModel.AutoSize")));
			this.txtModel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("txtModel.BackgroundImage")));
			this.txtModel.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("txtModel.Dock")));
			this.txtModel.Enabled = ((bool)(resources.GetObject("txtModel.Enabled")));
			this.txtModel.Font = ((System.Drawing.Font)(resources.GetObject("txtModel.Font")));
			this.txtModel.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("txtModel.ImeMode")));
			this.txtModel.Location = ((System.Drawing.Point)(resources.GetObject("txtModel.Location")));
			this.txtModel.MaxLength = ((int)(resources.GetObject("txtModel.MaxLength")));
			this.txtModel.Multiline = ((bool)(resources.GetObject("txtModel.Multiline")));
			this.txtModel.Name = "txtModel";
			this.txtModel.PasswordChar = ((char)(resources.GetObject("txtModel.PasswordChar")));
			this.txtModel.ReadOnly = true;
			this.txtModel.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("txtModel.RightToLeft")));
			this.txtModel.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("txtModel.ScrollBars")));
			this.txtModel.Size = ((System.Drawing.Size)(resources.GetObject("txtModel.Size")));
			this.txtModel.TabIndex = ((int)(resources.GetObject("txtModel.TabIndex")));
			this.txtModel.Text = resources.GetString("txtModel.Text");
			this.txtModel.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("txtModel.TextAlign")));
			this.txtModel.Visible = ((bool)(resources.GetObject("txtModel.Visible")));
			this.txtModel.WordWrap = ((bool)(resources.GetObject("txtModel.WordWrap")));
			// 
			// txtStockUM
			// 
			this.txtStockUM.AccessibleDescription = resources.GetString("txtStockUM.AccessibleDescription");
			this.txtStockUM.AccessibleName = resources.GetString("txtStockUM.AccessibleName");
			this.txtStockUM.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("txtStockUM.Anchor")));
			this.txtStockUM.AutoSize = ((bool)(resources.GetObject("txtStockUM.AutoSize")));
			this.txtStockUM.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("txtStockUM.BackgroundImage")));
			this.txtStockUM.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("txtStockUM.Dock")));
			this.txtStockUM.Enabled = ((bool)(resources.GetObject("txtStockUM.Enabled")));
			this.txtStockUM.Font = ((System.Drawing.Font)(resources.GetObject("txtStockUM.Font")));
			this.txtStockUM.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("txtStockUM.ImeMode")));
			this.txtStockUM.Location = ((System.Drawing.Point)(resources.GetObject("txtStockUM.Location")));
			this.txtStockUM.MaxLength = ((int)(resources.GetObject("txtStockUM.MaxLength")));
			this.txtStockUM.Multiline = ((bool)(resources.GetObject("txtStockUM.Multiline")));
			this.txtStockUM.Name = "txtStockUM";
			this.txtStockUM.PasswordChar = ((char)(resources.GetObject("txtStockUM.PasswordChar")));
			this.txtStockUM.ReadOnly = true;
			this.txtStockUM.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("txtStockUM.RightToLeft")));
			this.txtStockUM.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("txtStockUM.ScrollBars")));
			this.txtStockUM.Size = ((System.Drawing.Size)(resources.GetObject("txtStockUM.Size")));
			this.txtStockUM.TabIndex = ((int)(resources.GetObject("txtStockUM.TabIndex")));
			this.txtStockUM.Text = resources.GetString("txtStockUM.Text");
			this.txtStockUM.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("txtStockUM.TextAlign")));
			this.txtStockUM.Visible = ((bool)(resources.GetObject("txtStockUM.Visible")));
			this.txtStockUM.WordWrap = ((bool)(resources.GetObject("txtStockUM.WordWrap")));
			// 
			// lblStockUM
			// 
			this.lblStockUM.AccessibleDescription = resources.GetString("lblStockUM.AccessibleDescription");
			this.lblStockUM.AccessibleName = resources.GetString("lblStockUM.AccessibleName");
			this.lblStockUM.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblStockUM.Anchor")));
			this.lblStockUM.AutoSize = ((bool)(resources.GetObject("lblStockUM.AutoSize")));
			this.lblStockUM.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblStockUM.Dock")));
			this.lblStockUM.Enabled = ((bool)(resources.GetObject("lblStockUM.Enabled")));
			this.lblStockUM.Font = ((System.Drawing.Font)(resources.GetObject("lblStockUM.Font")));
			this.lblStockUM.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblStockUM.Image = ((System.Drawing.Image)(resources.GetObject("lblStockUM.Image")));
			this.lblStockUM.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblStockUM.ImageAlign")));
			this.lblStockUM.ImageIndex = ((int)(resources.GetObject("lblStockUM.ImageIndex")));
			this.lblStockUM.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblStockUM.ImeMode")));
			this.lblStockUM.Location = ((System.Drawing.Point)(resources.GetObject("lblStockUM.Location")));
			this.lblStockUM.Name = "lblStockUM";
			this.lblStockUM.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblStockUM.RightToLeft")));
			this.lblStockUM.Size = ((System.Drawing.Size)(resources.GetObject("lblStockUM.Size")));
			this.lblStockUM.TabIndex = ((int)(resources.GetObject("lblStockUM.TabIndex")));
			this.lblStockUM.Text = resources.GetString("lblStockUM.Text");
			this.lblStockUM.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblStockUM.TextAlign")));
			this.lblStockUM.Visible = ((bool)(resources.GetObject("lblStockUM.Visible")));
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
			// txtCurrency
			// 
			this.txtCurrency.AccessibleDescription = resources.GetString("txtCurrency.AccessibleDescription");
			this.txtCurrency.AccessibleName = resources.GetString("txtCurrency.AccessibleName");
			this.txtCurrency.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("txtCurrency.Anchor")));
			this.txtCurrency.AutoSize = ((bool)(resources.GetObject("txtCurrency.AutoSize")));
			this.txtCurrency.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("txtCurrency.BackgroundImage")));
			this.txtCurrency.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("txtCurrency.Dock")));
			this.txtCurrency.Enabled = ((bool)(resources.GetObject("txtCurrency.Enabled")));
			this.txtCurrency.Font = ((System.Drawing.Font)(resources.GetObject("txtCurrency.Font")));
			this.txtCurrency.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("txtCurrency.ImeMode")));
			this.txtCurrency.Location = ((System.Drawing.Point)(resources.GetObject("txtCurrency.Location")));
			this.txtCurrency.MaxLength = ((int)(resources.GetObject("txtCurrency.MaxLength")));
			this.txtCurrency.Multiline = ((bool)(resources.GetObject("txtCurrency.Multiline")));
			this.txtCurrency.Name = "txtCurrency";
			this.txtCurrency.PasswordChar = ((char)(resources.GetObject("txtCurrency.PasswordChar")));
			this.txtCurrency.ReadOnly = true;
			this.txtCurrency.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("txtCurrency.RightToLeft")));
			this.txtCurrency.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("txtCurrency.ScrollBars")));
			this.txtCurrency.Size = ((System.Drawing.Size)(resources.GetObject("txtCurrency.Size")));
			this.txtCurrency.TabIndex = ((int)(resources.GetObject("txtCurrency.TabIndex")));
			this.txtCurrency.Text = resources.GetString("txtCurrency.Text");
			this.txtCurrency.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("txtCurrency.TextAlign")));
			this.txtCurrency.Visible = ((bool)(resources.GetObject("txtCurrency.Visible")));
			this.txtCurrency.WordWrap = ((bool)(resources.GetObject("txtCurrency.WordWrap")));
			// 
			// lblCurrency
			// 
			this.lblCurrency.AccessibleDescription = resources.GetString("lblCurrency.AccessibleDescription");
			this.lblCurrency.AccessibleName = resources.GetString("lblCurrency.AccessibleName");
			this.lblCurrency.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblCurrency.Anchor")));
			this.lblCurrency.AutoSize = ((bool)(resources.GetObject("lblCurrency.AutoSize")));
			this.lblCurrency.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblCurrency.Dock")));
			this.lblCurrency.Enabled = ((bool)(resources.GetObject("lblCurrency.Enabled")));
			this.lblCurrency.Font = ((System.Drawing.Font)(resources.GetObject("lblCurrency.Font")));
			this.lblCurrency.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblCurrency.Image = ((System.Drawing.Image)(resources.GetObject("lblCurrency.Image")));
			this.lblCurrency.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblCurrency.ImageAlign")));
			this.lblCurrency.ImageIndex = ((int)(resources.GetObject("lblCurrency.ImageIndex")));
			this.lblCurrency.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblCurrency.ImeMode")));
			this.lblCurrency.Location = ((System.Drawing.Point)(resources.GetObject("lblCurrency.Location")));
			this.lblCurrency.Name = "lblCurrency";
			this.lblCurrency.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblCurrency.RightToLeft")));
			this.lblCurrency.Size = ((System.Drawing.Size)(resources.GetObject("lblCurrency.Size")));
			this.lblCurrency.TabIndex = ((int)(resources.GetObject("lblCurrency.TabIndex")));
			this.lblCurrency.Text = resources.GetString("lblCurrency.Text");
			this.lblCurrency.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblCurrency.TextAlign")));
			this.lblCurrency.Visible = ((bool)(resources.GetObject("lblCurrency.Visible")));
			// 
			// chkMakeItem
			// 
			this.chkMakeItem.AccessibleDescription = resources.GetString("chkMakeItem.AccessibleDescription");
			this.chkMakeItem.AccessibleName = resources.GetString("chkMakeItem.AccessibleName");
			this.chkMakeItem.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("chkMakeItem.Anchor")));
			this.chkMakeItem.Appearance = ((System.Windows.Forms.Appearance)(resources.GetObject("chkMakeItem.Appearance")));
			this.chkMakeItem.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("chkMakeItem.BackgroundImage")));
			this.chkMakeItem.CheckAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("chkMakeItem.CheckAlign")));
			this.chkMakeItem.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("chkMakeItem.Dock")));
			this.chkMakeItem.Enabled = ((bool)(resources.GetObject("chkMakeItem.Enabled")));
			this.chkMakeItem.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("chkMakeItem.FlatStyle")));
			this.chkMakeItem.Font = ((System.Drawing.Font)(resources.GetObject("chkMakeItem.Font")));
			this.chkMakeItem.Image = ((System.Drawing.Image)(resources.GetObject("chkMakeItem.Image")));
			this.chkMakeItem.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("chkMakeItem.ImageAlign")));
			this.chkMakeItem.ImageIndex = ((int)(resources.GetObject("chkMakeItem.ImageIndex")));
			this.chkMakeItem.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("chkMakeItem.ImeMode")));
			this.chkMakeItem.Location = ((System.Drawing.Point)(resources.GetObject("chkMakeItem.Location")));
			this.chkMakeItem.Name = "chkMakeItem";
			this.chkMakeItem.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("chkMakeItem.RightToLeft")));
			this.chkMakeItem.Size = ((System.Drawing.Size)(resources.GetObject("chkMakeItem.Size")));
			this.chkMakeItem.TabIndex = ((int)(resources.GetObject("chkMakeItem.TabIndex")));
			this.chkMakeItem.Text = resources.GetString("chkMakeItem.Text");
			this.chkMakeItem.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("chkMakeItem.TextAlign")));
			this.chkMakeItem.Visible = ((bool)(resources.GetObject("chkMakeItem.Visible")));
			// 
			// btnSearchPartNo
			// 
			this.btnSearchPartNo.AccessibleDescription = resources.GetString("btnSearchPartNo.AccessibleDescription");
			this.btnSearchPartNo.AccessibleName = resources.GetString("btnSearchPartNo.AccessibleName");
			this.btnSearchPartNo.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnSearchPartNo.Anchor")));
			this.btnSearchPartNo.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSearchPartNo.BackgroundImage")));
			this.btnSearchPartNo.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnSearchPartNo.Dock")));
			this.btnSearchPartNo.Enabled = ((bool)(resources.GetObject("btnSearchPartNo.Enabled")));
			this.btnSearchPartNo.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnSearchPartNo.FlatStyle")));
			this.btnSearchPartNo.Font = ((System.Drawing.Font)(resources.GetObject("btnSearchPartNo.Font")));
			this.btnSearchPartNo.Image = ((System.Drawing.Image)(resources.GetObject("btnSearchPartNo.Image")));
			this.btnSearchPartNo.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnSearchPartNo.ImageAlign")));
			this.btnSearchPartNo.ImageIndex = ((int)(resources.GetObject("btnSearchPartNo.ImageIndex")));
			this.btnSearchPartNo.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnSearchPartNo.ImeMode")));
			this.btnSearchPartNo.Location = ((System.Drawing.Point)(resources.GetObject("btnSearchPartNo.Location")));
			this.btnSearchPartNo.Name = "btnSearchPartNo";
			this.btnSearchPartNo.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnSearchPartNo.RightToLeft")));
			this.btnSearchPartNo.Size = ((System.Drawing.Size)(resources.GetObject("btnSearchPartNo.Size")));
			this.btnSearchPartNo.TabIndex = ((int)(resources.GetObject("btnSearchPartNo.TabIndex")));
			this.btnSearchPartNo.Text = resources.GetString("btnSearchPartNo.Text");
			this.btnSearchPartNo.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnSearchPartNo.TextAlign")));
			this.btnSearchPartNo.Visible = ((bool)(resources.GetObject("btnSearchPartNo.Visible")));
			this.btnSearchPartNo.Click += new System.EventHandler(this.btnSearchPartNo_Click);
			// 
			// btnSearchPartName
			// 
			this.btnSearchPartName.AccessibleDescription = resources.GetString("btnSearchPartName.AccessibleDescription");
			this.btnSearchPartName.AccessibleName = resources.GetString("btnSearchPartName.AccessibleName");
			this.btnSearchPartName.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnSearchPartName.Anchor")));
			this.btnSearchPartName.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSearchPartName.BackgroundImage")));
			this.btnSearchPartName.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnSearchPartName.Dock")));
			this.btnSearchPartName.Enabled = ((bool)(resources.GetObject("btnSearchPartName.Enabled")));
			this.btnSearchPartName.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnSearchPartName.FlatStyle")));
			this.btnSearchPartName.Font = ((System.Drawing.Font)(resources.GetObject("btnSearchPartName.Font")));
			this.btnSearchPartName.Image = ((System.Drawing.Image)(resources.GetObject("btnSearchPartName.Image")));
			this.btnSearchPartName.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnSearchPartName.ImageAlign")));
			this.btnSearchPartName.ImageIndex = ((int)(resources.GetObject("btnSearchPartName.ImageIndex")));
			this.btnSearchPartName.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnSearchPartName.ImeMode")));
			this.btnSearchPartName.Location = ((System.Drawing.Point)(resources.GetObject("btnSearchPartName.Location")));
			this.btnSearchPartName.Name = "btnSearchPartName";
			this.btnSearchPartName.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnSearchPartName.RightToLeft")));
			this.btnSearchPartName.Size = ((System.Drawing.Size)(resources.GetObject("btnSearchPartName.Size")));
			this.btnSearchPartName.TabIndex = ((int)(resources.GetObject("btnSearchPartName.TabIndex")));
			this.btnSearchPartName.Text = resources.GetString("btnSearchPartName.Text");
			this.btnSearchPartName.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnSearchPartName.TextAlign")));
			this.btnSearchPartName.Visible = ((bool)(resources.GetObject("btnSearchPartName.Visible")));
			this.btnSearchPartName.Click += new System.EventHandler(this.btnSearchPartName_Click);
			// 
			// txtTotal
			// 
			this.txtTotal.AccessibleDescription = resources.GetString("txtTotal.AccessibleDescription");
			this.txtTotal.AccessibleName = resources.GetString("txtTotal.AccessibleName");
			this.txtTotal.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("txtTotal.Anchor")));
			this.txtTotal.AutoSize = ((bool)(resources.GetObject("txtTotal.AutoSize")));
			this.txtTotal.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("txtTotal.BackgroundImage")));
			this.txtTotal.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("txtTotal.Dock")));
			this.txtTotal.Enabled = ((bool)(resources.GetObject("txtTotal.Enabled")));
			this.txtTotal.Font = ((System.Drawing.Font)(resources.GetObject("txtTotal.Font")));
			this.txtTotal.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("txtTotal.ImeMode")));
			this.txtTotal.Location = ((System.Drawing.Point)(resources.GetObject("txtTotal.Location")));
			this.txtTotal.MaxLength = ((int)(resources.GetObject("txtTotal.MaxLength")));
			this.txtTotal.Multiline = ((bool)(resources.GetObject("txtTotal.Multiline")));
			this.txtTotal.Name = "txtTotal";
			this.txtTotal.PasswordChar = ((char)(resources.GetObject("txtTotal.PasswordChar")));
			this.txtTotal.ReadOnly = true;
			this.txtTotal.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("txtTotal.RightToLeft")));
			this.txtTotal.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("txtTotal.ScrollBars")));
			this.txtTotal.Size = ((System.Drawing.Size)(resources.GetObject("txtTotal.Size")));
			this.txtTotal.TabIndex = ((int)(resources.GetObject("txtTotal.TabIndex")));
			this.txtTotal.Text = resources.GetString("txtTotal.Text");
			this.txtTotal.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("txtTotal.TextAlign")));
			this.txtTotal.Visible = ((bool)(resources.GetObject("txtTotal.Visible")));
			this.txtTotal.WordWrap = ((bool)(resources.GetObject("txtTotal.WordWrap")));
			// 
			// lblTotal
			// 
			this.lblTotal.AccessibleDescription = resources.GetString("lblTotal.AccessibleDescription");
			this.lblTotal.AccessibleName = resources.GetString("lblTotal.AccessibleName");
			this.lblTotal.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblTotal.Anchor")));
			this.lblTotal.AutoSize = ((bool)(resources.GetObject("lblTotal.AutoSize")));
			this.lblTotal.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblTotal.Dock")));
			this.lblTotal.Enabled = ((bool)(resources.GetObject("lblTotal.Enabled")));
			this.lblTotal.Font = ((System.Drawing.Font)(resources.GetObject("lblTotal.Font")));
			this.lblTotal.Image = ((System.Drawing.Image)(resources.GetObject("lblTotal.Image")));
			this.lblTotal.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblTotal.ImageAlign")));
			this.lblTotal.ImageIndex = ((int)(resources.GetObject("lblTotal.ImageIndex")));
			this.lblTotal.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblTotal.ImeMode")));
			this.lblTotal.Location = ((System.Drawing.Point)(resources.GetObject("lblTotal.Location")));
			this.lblTotal.Name = "lblTotal";
			this.lblTotal.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblTotal.RightToLeft")));
			this.lblTotal.Size = ((System.Drawing.Size)(resources.GetObject("lblTotal.Size")));
			this.lblTotal.TabIndex = ((int)(resources.GetObject("lblTotal.TabIndex")));
			this.lblTotal.Text = resources.GetString("lblTotal.Text");
			this.lblTotal.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblTotal.TextAlign")));
			this.lblTotal.Visible = ((bool)(resources.GetObject("lblTotal.Visible")));
			// 
			// numCostValue
			// 
			this.numCostValue.AcceptsEscape = ((bool)(resources.GetObject("numCostValue.AcceptsEscape")));
			this.numCostValue.AccessibleDescription = resources.GetString("numCostValue.AccessibleDescription");
			this.numCostValue.AccessibleName = resources.GetString("numCostValue.AccessibleName");
			this.numCostValue.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("numCostValue.Anchor")));
			this.numCostValue.AutoSize = ((bool)(resources.GetObject("numCostValue.AutoSize")));
			this.numCostValue.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("numCostValue.BackgroundImage")));
			this.numCostValue.BorderStyle = ((System.Windows.Forms.BorderStyle)(resources.GetObject("numCostValue.BorderStyle")));
			// 
			// numCostValue.Calculator
			// 
			this.numCostValue.Calculator.AccessibleDescription = resources.GetString("numCostValue.Calculator.AccessibleDescription");
			this.numCostValue.Calculator.AccessibleName = resources.GetString("numCostValue.Calculator.AccessibleName");
			this.numCostValue.Calculator.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("numCostValue.Calculator.BackgroundImage")));
			this.numCostValue.Calculator.ButtonFlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("numCostValue.Calculator.ButtonFlatStyle")));
			this.numCostValue.Calculator.DisplayFormat = resources.GetString("numCostValue.Calculator.DisplayFormat");
			this.numCostValue.Calculator.Font = ((System.Drawing.Font)(resources.GetObject("numCostValue.Calculator.Font")));
			this.numCostValue.Calculator.FormatOnClose = ((bool)(resources.GetObject("numCostValue.Calculator.FormatOnClose")));
			this.numCostValue.Calculator.StoredFormat = resources.GetString("numCostValue.Calculator.StoredFormat");
			this.numCostValue.Calculator.UIStrings.Content = ((string[])(resources.GetObject("numCostValue.Calculator.UIStrings.Content")));
			this.numCostValue.CaseSensitive = ((bool)(resources.GetObject("numCostValue.CaseSensitive")));
			this.numCostValue.Culture = ((int)(resources.GetObject("numCostValue.Culture")));
			this.numCostValue.CustomFormat = resources.GetString("numCostValue.CustomFormat");
			this.numCostValue.DataType = ((System.Type)(resources.GetObject("numCostValue.DataType")));
			this.numCostValue.DisplayFormat.CustomFormat = resources.GetString("numCostValue.DisplayFormat.CustomFormat");
			this.numCostValue.DisplayFormat.FormatType = ((C1.Win.C1Input.FormatTypeEnum)(resources.GetObject("numCostValue.DisplayFormat.FormatType")));
			this.numCostValue.DisplayFormat.Inherit = ((C1.Win.C1Input.FormatInfoInheritFlags)(resources.GetObject("numCostValue.DisplayFormat.Inherit")));
			this.numCostValue.DisplayFormat.NullText = resources.GetString("numCostValue.DisplayFormat.NullText");
			this.numCostValue.DisplayFormat.TrimEnd = ((bool)(resources.GetObject("numCostValue.DisplayFormat.TrimEnd")));
			this.numCostValue.DisplayFormat.TrimStart = ((bool)(resources.GetObject("numCostValue.DisplayFormat.TrimStart")));
			this.numCostValue.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("numCostValue.Dock")));
			this.numCostValue.DropDownFormAlign = ((C1.Win.C1Input.DropDownFormAlignmentEnum)(resources.GetObject("numCostValue.DropDownFormAlign")));
			this.numCostValue.EditFormat.CustomFormat = resources.GetString("numCostValue.EditFormat.CustomFormat");
			this.numCostValue.EditFormat.FormatType = ((C1.Win.C1Input.FormatTypeEnum)(resources.GetObject("numCostValue.EditFormat.FormatType")));
			this.numCostValue.EditFormat.Inherit = ((C1.Win.C1Input.FormatInfoInheritFlags)(resources.GetObject("numCostValue.EditFormat.Inherit")));
			this.numCostValue.EditFormat.NullText = resources.GetString("numCostValue.EditFormat.NullText");
			this.numCostValue.EditFormat.TrimEnd = ((bool)(resources.GetObject("numCostValue.EditFormat.TrimEnd")));
			this.numCostValue.EditFormat.TrimStart = ((bool)(resources.GetObject("numCostValue.EditFormat.TrimStart")));
			this.numCostValue.EditMask = resources.GetString("numCostValue.EditMask");
			this.numCostValue.EmptyAsNull = ((bool)(resources.GetObject("numCostValue.EmptyAsNull")));
			this.numCostValue.Enabled = ((bool)(resources.GetObject("numCostValue.Enabled")));
			this.numCostValue.ErrorInfo.BeepOnError = ((bool)(resources.GetObject("numCostValue.ErrorInfo.BeepOnError")));
			this.numCostValue.ErrorInfo.ErrorMessage = resources.GetString("numCostValue.ErrorInfo.ErrorMessage");
			this.numCostValue.ErrorInfo.ErrorMessageCaption = resources.GetString("numCostValue.ErrorInfo.ErrorMessageCaption");
			this.numCostValue.ErrorInfo.ShowErrorMessage = ((bool)(resources.GetObject("numCostValue.ErrorInfo.ShowErrorMessage")));
			this.numCostValue.ErrorInfo.ValueOnError = ((object)(resources.GetObject("numCostValue.ErrorInfo.ValueOnError")));
			this.numCostValue.Font = ((System.Drawing.Font)(resources.GetObject("numCostValue.Font")));
			this.numCostValue.FormatType = ((C1.Win.C1Input.FormatTypeEnum)(resources.GetObject("numCostValue.FormatType")));
			this.numCostValue.GapHeight = ((int)(resources.GetObject("numCostValue.GapHeight")));
			this.numCostValue.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("numCostValue.ImeMode")));
			this.numCostValue.Increment = ((object)(resources.GetObject("numCostValue.Increment")));
			this.numCostValue.InitialSelection = ((C1.Win.C1Input.InitialSelectionEnum)(resources.GetObject("numCostValue.InitialSelection")));
			this.numCostValue.Location = ((System.Drawing.Point)(resources.GetObject("numCostValue.Location")));
			this.numCostValue.MaskInfo.AutoTabWhenFilled = ((bool)(resources.GetObject("numCostValue.MaskInfo.AutoTabWhenFilled")));
			this.numCostValue.MaskInfo.CaseSensitive = ((bool)(resources.GetObject("numCostValue.MaskInfo.CaseSensitive")));
			this.numCostValue.MaskInfo.CopyWithLiterals = ((bool)(resources.GetObject("numCostValue.MaskInfo.CopyWithLiterals")));
			this.numCostValue.MaskInfo.EditMask = resources.GetString("numCostValue.MaskInfo.EditMask");
			this.numCostValue.MaskInfo.EmptyAsNull = ((bool)(resources.GetObject("numCostValue.MaskInfo.EmptyAsNull")));
			this.numCostValue.MaskInfo.ErrorMessage = resources.GetString("numCostValue.MaskInfo.ErrorMessage");
			this.numCostValue.MaskInfo.Inherit = ((C1.Win.C1Input.MaskInfoInheritFlags)(resources.GetObject("numCostValue.MaskInfo.Inherit")));
			this.numCostValue.MaskInfo.PromptChar = ((char)(resources.GetObject("numCostValue.MaskInfo.PromptChar")));
			this.numCostValue.MaskInfo.ShowLiterals = ((C1.Win.C1Input.ShowLiteralsEnum)(resources.GetObject("numCostValue.MaskInfo.ShowLiterals")));
			this.numCostValue.MaskInfo.StoredEmptyChar = ((char)(resources.GetObject("numCostValue.MaskInfo.StoredEmptyChar")));
			this.numCostValue.MaxLength = ((int)(resources.GetObject("numCostValue.MaxLength")));
			this.numCostValue.Name = "numCostValue";
			this.numCostValue.NullText = resources.GetString("numCostValue.NullText");
			this.numCostValue.NumericInputKeys = ((C1.Win.C1Input.NumericInputKeyFlags)((((C1.Win.C1Input.NumericInputKeyFlags.F9 | C1.Win.C1Input.NumericInputKeyFlags.Plus) 
				| C1.Win.C1Input.NumericInputKeyFlags.Decimal) 
				| C1.Win.C1Input.NumericInputKeyFlags.X)));
			this.numCostValue.ParseInfo.CaseSensitive = ((bool)(resources.GetObject("numCostValue.ParseInfo.CaseSensitive")));
			this.numCostValue.ParseInfo.CustomFormat = resources.GetString("numCostValue.ParseInfo.CustomFormat");
			this.numCostValue.ParseInfo.EmptyAsNull = ((bool)(resources.GetObject("numCostValue.ParseInfo.EmptyAsNull")));
			this.numCostValue.ParseInfo.ErrorMessage = resources.GetString("numCostValue.ParseInfo.ErrorMessage");
			this.numCostValue.ParseInfo.FormatType = ((C1.Win.C1Input.FormatTypeEnum)(resources.GetObject("numCostValue.ParseInfo.FormatType")));
			this.numCostValue.ParseInfo.Inherit = ((C1.Win.C1Input.ParseInfoInheritFlags)(resources.GetObject("numCostValue.ParseInfo.Inherit")));
			this.numCostValue.ParseInfo.NullText = resources.GetString("numCostValue.ParseInfo.NullText");
			this.numCostValue.ParseInfo.NumberStyle = ((C1.Win.C1Input.NumberStyleFlags)(resources.GetObject("numCostValue.ParseInfo.NumberStyle")));
			this.numCostValue.ParseInfo.TrimEnd = ((bool)(resources.GetObject("numCostValue.ParseInfo.TrimEnd")));
			this.numCostValue.ParseInfo.TrimStart = ((bool)(resources.GetObject("numCostValue.ParseInfo.TrimStart")));
			this.numCostValue.PasswordChar = ((char)(resources.GetObject("numCostValue.PasswordChar")));
			this.numCostValue.PostValidation.CaseSensitive = ((bool)(resources.GetObject("numCostValue.PostValidation.CaseSensitive")));
			this.numCostValue.PostValidation.ErrorMessage = resources.GetString("numCostValue.PostValidation.ErrorMessage");
			this.numCostValue.PostValidation.Inherit = ((C1.Win.C1Input.PostValidationInheritFlags)(resources.GetObject("numCostValue.PostValidation.Inherit")));
			this.numCostValue.PostValidation.Validation = ((C1.Win.C1Input.PostValidationTypeEnum)(resources.GetObject("numCostValue.PostValidation.Validation")));
			this.numCostValue.PostValidation.Values = ((System.Array)(resources.GetObject("numCostValue.PostValidation.Values")));
			this.numCostValue.PostValidation.ValuesExcluded = ((System.Array)(resources.GetObject("numCostValue.PostValidation.ValuesExcluded")));
			this.numCostValue.PreValidation.CaseSensitive = ((bool)(resources.GetObject("numCostValue.PreValidation.CaseSensitive")));
			this.numCostValue.PreValidation.ErrorMessage = resources.GetString("numCostValue.PreValidation.ErrorMessage");
			this.numCostValue.PreValidation.Inherit = ((C1.Win.C1Input.PreValidationInheritFlags)(resources.GetObject("numCostValue.PreValidation.Inherit")));
			this.numCostValue.PreValidation.ItemSeparator = resources.GetString("numCostValue.PreValidation.ItemSeparator");
			this.numCostValue.PreValidation.PatternString = resources.GetString("numCostValue.PreValidation.PatternString");
			this.numCostValue.PreValidation.RegexOptions = ((C1.Win.C1Input.RegexOptionFlags)(resources.GetObject("numCostValue.PreValidation.RegexOptions")));
			this.numCostValue.PreValidation.TrimEnd = ((bool)(resources.GetObject("numCostValue.PreValidation.TrimEnd")));
			this.numCostValue.PreValidation.TrimStart = ((bool)(resources.GetObject("numCostValue.PreValidation.TrimStart")));
			this.numCostValue.PreValidation.Validation = ((C1.Win.C1Input.PreValidationTypeEnum)(resources.GetObject("numCostValue.PreValidation.Validation")));
			this.numCostValue.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("numCostValue.RightToLeft")));
			this.numCostValue.ShowFocusRectangle = ((bool)(resources.GetObject("numCostValue.ShowFocusRectangle")));
			this.numCostValue.Size = ((System.Drawing.Size)(resources.GetObject("numCostValue.Size")));
			this.numCostValue.TabIndex = ((int)(resources.GetObject("numCostValue.TabIndex")));
			this.numCostValue.Tag = ((object)(resources.GetObject("numCostValue.Tag")));
			this.numCostValue.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("numCostValue.TextAlign")));
			this.numCostValue.TrimEnd = ((bool)(resources.GetObject("numCostValue.TrimEnd")));
			this.numCostValue.TrimStart = ((bool)(resources.GetObject("numCostValue.TrimStart")));
			this.numCostValue.UserCultureOverride = ((bool)(resources.GetObject("numCostValue.UserCultureOverride")));
			this.numCostValue.Value = ((object)(resources.GetObject("numCostValue.Value")));
			this.numCostValue.VerticalAlign = ((C1.Win.C1Input.VerticalAlignEnum)(resources.GetObject("numCostValue.VerticalAlign")));
			this.numCostValue.Visible = ((bool)(resources.GetObject("numCostValue.Visible")));
			this.numCostValue.VisibleButtons = ((C1.Win.C1Input.DropDownControlButtonFlags)(resources.GetObject("numCostValue.VisibleButtons")));
			// 
			// lblMakeItem
			// 
			this.lblMakeItem.AccessibleDescription = resources.GetString("lblMakeItem.AccessibleDescription");
			this.lblMakeItem.AccessibleName = resources.GetString("lblMakeItem.AccessibleName");
			this.lblMakeItem.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblMakeItem.Anchor")));
			this.lblMakeItem.AutoSize = ((bool)(resources.GetObject("lblMakeItem.AutoSize")));
			this.lblMakeItem.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblMakeItem.Dock")));
			this.lblMakeItem.Enabled = ((bool)(resources.GetObject("lblMakeItem.Enabled")));
			this.lblMakeItem.Font = ((System.Drawing.Font)(resources.GetObject("lblMakeItem.Font")));
			this.lblMakeItem.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblMakeItem.Image = ((System.Drawing.Image)(resources.GetObject("lblMakeItem.Image")));
			this.lblMakeItem.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblMakeItem.ImageAlign")));
			this.lblMakeItem.ImageIndex = ((int)(resources.GetObject("lblMakeItem.ImageIndex")));
			this.lblMakeItem.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblMakeItem.ImeMode")));
			this.lblMakeItem.Location = ((System.Drawing.Point)(resources.GetObject("lblMakeItem.Location")));
			this.lblMakeItem.Name = "lblMakeItem";
			this.lblMakeItem.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblMakeItem.RightToLeft")));
			this.lblMakeItem.Size = ((System.Drawing.Size)(resources.GetObject("lblMakeItem.Size")));
			this.lblMakeItem.TabIndex = ((int)(resources.GetObject("lblMakeItem.TabIndex")));
			this.lblMakeItem.Text = resources.GetString("lblMakeItem.Text");
			this.lblMakeItem.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblMakeItem.TextAlign")));
			this.lblMakeItem.Visible = ((bool)(resources.GetObject("lblMakeItem.Visible")));
			// 
			// txtPartNo
			// 
			this.txtPartNo.AccessibleDescription = resources.GetString("txtPartNo.AccessibleDescription");
			this.txtPartNo.AccessibleName = resources.GetString("txtPartNo.AccessibleName");
			this.txtPartNo.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("txtPartNo.Anchor")));
			this.txtPartNo.AutoSize = ((bool)(resources.GetObject("txtPartNo.AutoSize")));
			this.txtPartNo.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("txtPartNo.BackgroundImage")));
			this.txtPartNo.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("txtPartNo.Dock")));
			this.txtPartNo.Enabled = ((bool)(resources.GetObject("txtPartNo.Enabled")));
			this.txtPartNo.Font = ((System.Drawing.Font)(resources.GetObject("txtPartNo.Font")));
			this.txtPartNo.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("txtPartNo.ImeMode")));
			this.txtPartNo.Location = ((System.Drawing.Point)(resources.GetObject("txtPartNo.Location")));
			this.txtPartNo.MaxLength = ((int)(resources.GetObject("txtPartNo.MaxLength")));
			this.txtPartNo.Multiline = ((bool)(resources.GetObject("txtPartNo.Multiline")));
			this.txtPartNo.Name = "txtPartNo";
			this.txtPartNo.PasswordChar = ((char)(resources.GetObject("txtPartNo.PasswordChar")));
			this.txtPartNo.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("txtPartNo.RightToLeft")));
			this.txtPartNo.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("txtPartNo.ScrollBars")));
			this.txtPartNo.Size = ((System.Drawing.Size)(resources.GetObject("txtPartNo.Size")));
			this.txtPartNo.TabIndex = ((int)(resources.GetObject("txtPartNo.TabIndex")));
			this.txtPartNo.Text = resources.GetString("txtPartNo.Text");
			this.txtPartNo.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("txtPartNo.TextAlign")));
			this.txtPartNo.Visible = ((bool)(resources.GetObject("txtPartNo.Visible")));
			this.txtPartNo.WordWrap = ((bool)(resources.GetObject("txtPartNo.WordWrap")));
			this.txtPartNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPartNo_KeyDown);
			this.txtPartNo.Validating += new System.ComponentModel.CancelEventHandler(this.txtPartNo_Validating);
			// 
			// ItemStandardCost
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
			this.Controls.Add(this.txtPartNo);
			this.Controls.Add(this.lblMakeItem);
			this.Controls.Add(this.numCostValue);
			this.Controls.Add(this.txtTotal);
			this.Controls.Add(this.txtCurrency);
			this.Controls.Add(this.txtStockUM);
			this.Controls.Add(this.txtModel);
			this.Controls.Add(this.txtPartName);
			this.Controls.Add(this.lblTotal);
			this.Controls.Add(this.btnSearchPartName);
			this.Controls.Add(this.btnSearchPartNo);
			this.Controls.Add(this.chkMakeItem);
			this.Controls.Add(this.lblCurrency);
			this.Controls.Add(this.btnDelete);
			this.Controls.Add(this.lblStockUM);
			this.Controls.Add(this.lblModel);
			this.Controls.Add(this.btnHelp);
			this.Controls.Add(this.btnClose);
			this.Controls.Add(this.btnSave);
			this.Controls.Add(this.lblPartName);
			this.Controls.Add(this.dgrdData);
			this.Controls.Add(this.lblPartNo);
			this.Enabled = ((bool)(resources.GetObject("$this.Enabled")));
			this.Font = ((System.Drawing.Font)(resources.GetObject("$this.Font")));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("$this.ImeMode")));
			this.Location = ((System.Drawing.Point)(resources.GetObject("$this.Location")));
			this.MaximumSize = ((System.Drawing.Size)(resources.GetObject("$this.MaximumSize")));
			this.MinimumSize = ((System.Drawing.Size)(resources.GetObject("$this.MinimumSize")));
			this.Name = "ItemStandardCost";
			this.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("$this.RightToLeft")));
			this.StartPosition = ((System.Windows.Forms.FormStartPosition)(resources.GetObject("$this.StartPosition")));
			this.Text = resources.GetString("$this.Text");
			this.Closing += new System.ComponentModel.CancelEventHandler(this.ItemStandardCost_Closing);
			this.Load += new System.EventHandler(this.ItemStandardCost_Load);
			((System.ComponentModel.ISupportInitialize)(this.dgrdData)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numCostValue)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion
		
		#region Methods		
		
		/// <summary>
		/// Validate data in controls
		/// </summary>
		/// <returns></returns>
		/// <author>Tuan TQ. 08 Jan, 2006</author>
		private bool ValidateData()
		{
			if (FormControlComponents.CheckMandatory(txtPartNo))
			{
				PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Exclamation);
				txtPartNo.Focus();
				return false;
			}

			if (FormControlComponents.CheckMandatory(txtPartName))
			{
				PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Exclamation);
				txtPartName.Focus();
				return false;
			}		

			if(dgrdData.RowCount == 0)
			{
				txtPartNo.Focus();
				return false;
			}

			for(int iRowIndex = 0; iRowIndex < dgrdData.RowCount; iRowIndex++)
			{
				string[] arrMessage = new string[2];
				arrMessage[0] = dgrdData.Columns[CST_STDItemCostTable.COST_FLD].Caption;
				arrMessage[1] = "or equal 0";
				
				if( dgrdData[iRowIndex, STD_CostElementTable.TABLE_NAME + STD_CostElementTable.CODE_FLD].Equals(DBNull.Value)
					|| (dgrdData[iRowIndex, STD_CostElementTable.TABLE_NAME + STD_CostElementTable.CODE_FLD].ToString().Trim() == string.Empty))
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Exclamation);

					dgrdData.Row = iRowIndex;
					dgrdData.Col = dgrdData.Columns.IndexOf(dgrdData.Columns[STD_CostElementTable.TABLE_NAME + STD_CostElementTable.CODE_FLD]);
					return false;
				}

				if( dgrdData[iRowIndex, STD_CostElementTable.TABLE_NAME + STD_CostElementTable.NAME_FLD].Equals(DBNull.Value)
					|| (dgrdData[iRowIndex, STD_CostElementTable.TABLE_NAME + STD_CostElementTable.NAME_FLD].ToString().Trim() == string.Empty))
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Exclamation);

					dgrdData.Row = iRowIndex;
					dgrdData.Col = dgrdData.Columns.IndexOf(dgrdData.Columns[STD_CostElementTable.TABLE_NAME + STD_CostElementTable.NAME_FLD]);
					return false;
				}

				if( dgrdData[iRowIndex, CST_STDItemCostTable.COST_FLD].Equals(DBNull.Value)
					|| (dgrdData[iRowIndex, CST_STDItemCostTable.COST_FLD].ToString().Trim() == string.Empty))
				{
					PCSMessageBox.Show(ErrorCode.MESSAGE_GREATER_THAN, MessageBoxIcon.Exclamation, arrMessage);

					dgrdData.Row = iRowIndex;
					dgrdData.Col = dgrdData.Columns.IndexOf(dgrdData.Columns[CST_STDItemCostTable.COST_FLD]);
					return false;
				}
				
				if( decimal.Parse(dgrdData[iRowIndex, CST_STDItemCostTable.COST_FLD].ToString()) < 0)
				{					
					PCSMessageBox.Show(ErrorCode.MESSAGE_GREATER_THAN, MessageBoxIcon.Exclamation, arrMessage);
					
					dgrdData.Row = iRowIndex;
					dgrdData.Col = dgrdData.Columns.IndexOf(dgrdData.Columns[CST_STDItemCostTable.COST_FLD]);
					return false;
				}
				
				//update product
				dgrdData[iRowIndex, CST_STDItemCostTable.PRODUCTID_FLD] = txtPartNo.Tag;
			}		

			return true;
		}


		private void ResetControlValue(bool pblnClearDataGrid)
		{
			//clear grid 
			if(pblnClearDataGrid)
			{
				dtbItemCostDetail = BuildItemCostTable();
				dgrdData.DataSource = dtbItemCostDetail;
				FormatDataGrid(true);
			}

			//reset other controls
			txtPartNo.Text   = string.Empty;
			txtPartName.Text = string.Empty;
			txtModel.Text    = string.Empty;
			txtPartNo.Tag    = null;
			txtStockUM.Text  = string.Empty;
			txtTotal.Text = string.Empty;

			txtCurrency.Text = SystemProperty.HomeCurrency;
			chkMakeItem.Checked = false;		
			
			btnSave.Enabled = false;
			btnDelete.Enabled = false;			
		}

		private DataTable BuildItemCostTable()
		{			
			DataTable dtbTable = new DataTable(CST_STDItemCostTable.TABLE_NAME);
			
			dtbTable.Columns.Add(CST_STDItemCostTable.STDITEMCOSTID_FLD, typeof(System.Int32));
			dtbTable.Columns.Add(CST_STDItemCostTable.COSTELEMENTID_FLD, typeof(System.Int32));
			dtbTable.Columns.Add(STD_CostElementTable.COSTELEMENTTYPEID_FLD, typeof(System.Int32));
			dtbTable.Columns.Add(CST_STDItemCostTable.PRODUCTID_FLD, typeof(System.Int32));

			dtbTable.Columns.Add(STD_CostElementTable.TABLE_NAME + STD_CostElementTable.CODE_FLD, typeof(System.String));
			dtbTable.Columns.Add(STD_CostElementTable.TABLE_NAME + STD_CostElementTable.NAME_FLD, typeof(System.String));

			dtbTable.Columns.Add(CST_STDItemCostTable.ROLLUPDATE_FLD, typeof(System.DateTime));
			dtbTable.Columns.Add(CST_STDItemCostTable.COST_FLD, typeof(System.Decimal));

			dtbTable.Columns.Add(STD_CostElementTable.ORDERNO_FLD, typeof(System.Int32));

			dtbTable.DefaultView.Sort = STD_CostElementTable.ORDERNO_FLD + " ASC";

			return dtbTable;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="pintProductID"></param>
		private bool InsertCostElement(int pintProductID, bool pblnMakeItem)
		{				
			if(!pblnMakeItem && dtbItemCostDetail.Rows.Count != 0)
			{
				return false;
			}

			//Insert cost element
			CostElementBO boCostElement = new CostElementBO();
			DataSet dtsLeafCostElement = boCostElement.ListLeafElements();

			bool blnInserted = false;
			
			if(dtsLeafCostElement != null)
			{
				string strCondition;
				DataRow[] arrFoundRow;

				foreach(DataRow drowSource in dtsLeafCostElement.Tables[0].Rows)
				{
					strCondition = CST_STDItemCostTable.COSTELEMENTID_FLD + "=" + drowSource[STD_CostElementTable.COSTELEMENTID_FLD].ToString();

					//don't add existing cost element
					arrFoundRow = dtbItemCostDetail.Select(strCondition);
					if(arrFoundRow.Length > 0)
					{
						continue;
					}
					
					//none make item: system only allows user input cost for Cost element have Type = Material
					if(!pblnMakeItem && drowSource[STD_CostElementTable.COSTELEMENTTYPEID_FLD].ToString() != ((int)CostElementType.Material).ToString())
					{
						continue;
					}

					blnInserted = true;

					//copy data then add to collection
					DataRow drowNew = dtbItemCostDetail.NewRow();								
					drowNew[CST_STDItemCostTable.COSTELEMENTID_FLD] = drowSource[STD_CostElementTable.COSTELEMENTID_FLD];
					drowNew[STD_CostElementTable.COSTELEMENTTYPEID_FLD] = drowSource[STD_CostElementTable.COSTELEMENTTYPEID_FLD];

					drowNew[STD_CostElementTable.TABLE_NAME + STD_CostElementTable.CODE_FLD] = drowSource[STD_CostElementTable.CODE_FLD];
					drowNew[STD_CostElementTable.TABLE_NAME + STD_CostElementTable.NAME_FLD] = drowSource[STD_CostElementTable.NAME_FLD];
					drowNew[STD_CostElementTable.ORDERNO_FLD] = drowSource[STD_CostElementTable.ORDERNO_FLD];

					drowNew[CST_STDItemCostTable.PRODUCTID_FLD] = pintProductID;

					drowNew[CST_STDItemCostTable.COST_FLD] = DBNull.Value;//default value is blank
					drowNew[CST_STDItemCostTable.ROLLUPDATE_FLD] = DBNull.Value;
								
					dtbItemCostDetail.Rows.Add(drowNew);
				}
			}

			return blnInserted;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="pintProductID"></param>
		private void LoadDetailGrid(int pintProductID, bool pblnMakeItem)
		{
			if(pintProductID > 0)
			{
				bool blnInserted = false;
				//Get data from database
				dtbItemCostDetail =  boItemStandardCost.GetItemCostDetail(pintProductID);
				dtbItemCostDetail.DefaultView.Sort = STD_CostElementTable.ORDERNO_FLD + " ASC";

				if(dtbItemCostDetail != null)
				{
					blnInserted = InsertCostElement(pintProductID, pblnMakeItem);
				}

				//Bind to grid
				dgrdData.DataSource = dtbItemCostDetail;

				//Sum total cost
				SumTotalCost();

				//Change grid representation
				FormatDataGrid(pblnMakeItem);

				//set save button state
				btnSave.Enabled = blnInserted && (decimal.Parse(txtTotal.Text) > 0);
			}
		}
		
		/// <summary>
		/// 
		/// </summary>
		private void FormatDataGrid(bool pblnMakeItem)
		{
			//Restore layout
			FormControlComponents.RestoreGridLayout(dgrdData, dtbGridLayOut);
			
			dgrdData.AllowAddNew = false;
			dgrdData.AllowDelete = false;
			dgrdData.AllowUpdate = true;

			//Lock columns
			for(int i = 0; i < dgrdData.Splits[0].DisplayColumns.Count; i++)			
			{
				dgrdData.Splits[0].DisplayColumns[i].Locked = true;
			}
			
			//only allow update cost when not make item and have selected product
			dgrdData.Splits[0].DisplayColumns[CST_STDItemCostTable.COST_FLD].Locked = pblnMakeItem || txtPartNo.Text.Trim().Equals(string.Empty);
		
			dgrdData.Columns[CST_STDItemCostTable.COST_FLD].NumberFormat = Constants.DECIMAL_NUMBERFORMAT;
			
			//format number editor
			numCostValue.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
			numCostValue.CustomFormat = Constants.DECIMAL_NUMBERFORMAT;

			dgrdData.Columns[CST_STDItemCostTable.COST_FLD].Editor = numCostValue;
			
			//Check if item could be deleted
			bool blnCanDelete = (dgrdData.RowCount > 0) && !dgrdData.Splits[0].DisplayColumns[CST_STDItemCostTable.COST_FLD].Locked;
			
			if(txtTotal.Text.Trim() != string.Empty)
			{
				blnCanDelete &= (decimal.Parse(txtTotal.Text) > 0);
			}

			//set delete button state
			btnDelete.Enabled = blnCanDelete;
			dgrdData.Splits[0].DisplayColumns[STD_CostElementTable.TABLE_NAME + STD_CostElementTable.CODE_FLD].Locked = pblnMakeItem;
			dgrdData.Splits[0].DisplayColumns[STD_CostElementTable.TABLE_NAME + STD_CostElementTable.NAME_FLD].Locked = pblnMakeItem;

			dgrdData.Splits[0].DisplayColumns[STD_CostElementTable.TABLE_NAME + STD_CostElementTable.CODE_FLD].Button = !pblnMakeItem;
			dgrdData.Splits[0].DisplayColumns[STD_CostElementTable.TABLE_NAME + STD_CostElementTable.NAME_FLD].Button = !pblnMakeItem;			
		}
		
		/// <summary>
		/// Fill related data on controls when select PartName
		/// </summary>
		/// <param name="blnOpenOnly"></param>
		private bool SelectPartName(bool pblnAlwaysShowDialog)
		{			
			//Call OpenSearchForm for selecting TransNo
			DataRowView drwResult = FormControlComponents.OpenSearchForm(PRODUCT_INFOR_VIEW, PART_NAME_FLD, txtPartName.Text, null, pblnAlwaysShowDialog);

			//If has TransNo matched searching condition, fill values to form's controls
			if(drwResult != null)
			{			
				txtPartNo.Text = drwResult[PART_NO_FLD].ToString();					
				txtPartName.Text = drwResult[PART_NAME_FLD].ToString();
				txtModel.Text = drwResult[PART_MODEL_FLD].ToString();				
				txtStockUM.Text = drwResult[STOCK_UM_FLD].ToString();
				txtPartNo.Tag  = drwResult[ITM_ProductTable.PRODUCTID_FLD];
				
				if(drwResult[ITM_ProductTable.MAKEITEM_FLD].Equals(DBNull.Value) 
					||(drwResult[ITM_ProductTable.MAKEITEM_FLD].ToString().Trim() == string.Empty))
				{
					chkMakeItem.Checked = false;
				}
				else
				{
					chkMakeItem.Checked = bool.Parse(drwResult[ITM_ProductTable.MAKEITEM_FLD].ToString());
				}

				mintProductID = int.Parse(drwResult[ITM_ProductTable.PRODUCTID_FLD].ToString());
				LoadDetailGrid(mintProductID, chkMakeItem.Checked);

				//reset modify status
				txtPartName.Modified = false;
				txtPartNo.Modified = false;
			}				
			else if(!pblnAlwaysShowDialog)
			{
				txtPartName.Focus();
				return false;
			}

			//Return true = ok
			return true;			
		}
		
		/// <summary>
		/// Fill related data on controls when select Part Name
		/// </summary>
		/// <param name="blnOpenOnly"></param>
		private bool SelectPartNo(bool pblnAlwaysShowDialog)
		{
			//Call OpenSearchForm for selecting TransNo
			DataRowView drwResult = FormControlComponents.OpenSearchForm(PRODUCT_INFOR_VIEW, PART_NO_FLD, txtPartNo.Text, null, pblnAlwaysShowDialog);
				
			//If has TransNo matched searching condition, fill values to form's controls
			if(drwResult != null)
			{
				txtPartNo.Text = drwResult[PART_NO_FLD].ToString();					
				txtPartName.Text = drwResult[PART_NAME_FLD].ToString();
				txtModel.Text = drwResult[PART_MODEL_FLD].ToString();				
				txtStockUM.Text = drwResult[STOCK_UM_FLD].ToString();
				txtPartNo.Tag  = drwResult[ITM_ProductTable.PRODUCTID_FLD];

				if(drwResult[ITM_ProductTable.MAKEITEM_FLD].Equals(DBNull.Value) 
					||(drwResult[ITM_ProductTable.MAKEITEM_FLD].ToString().Trim() == string.Empty))
				{
					chkMakeItem.Checked = false;
				}
				else
				{
					chkMakeItem.Checked = bool.Parse(drwResult[ITM_ProductTable.MAKEITEM_FLD].ToString());
				}

				mintProductID = int.Parse(drwResult[ITM_ProductTable.PRODUCTID_FLD].ToString());
				LoadDetailGrid(mintProductID, chkMakeItem.Checked);				

				//reset modify status
				txtPartName.Modified = false;
				txtPartNo.Modified = false;
			}				
			else if(!pblnAlwaysShowDialog)
			{	
				txtPartNo.Focus();
				return false;
			}

			//Return true = ok
			return true;			
		}
		
		/// <summary>
		/// process data inputed into the grid
		/// </summary>
		/// <param name="pstrColumnName"></param>
		/// <param name="pstrColumValue"></param>
		/// <returns>If no error then return true, else return false</returns>
		private bool ProcessInputDataInGrid(string pstrColumnName, string pstrColumValue, bool pblnAlwaysShow)
		{
			Hashtable htbCondition = new Hashtable(); 
			DataRowView drvResult = null;
			bool blnResult = true;			
			//clear hash table for new condition
			htbCondition.Clear();

			string strCondition = Convert.ToString((int)CostElementType.Material) + "' ";
			strCondition += " OR " + STD_CostElementTable.TABLE_NAME + "." + STD_CostElementTable.COSTELEMENTTYPEID_FLD + "='" + (int)CostElementType.SubMaterial;
			
			htbCondition.Add(STD_CostElementTable.ISLEAF_FLD, Constants.COST_ELEMENT_IS_LEAF);
			htbCondition.Add(STD_CostElementTable.COSTELEMENTTYPEID_FLD, strCondition);

			//Check for each column
			switch (pstrColumnName)
			{
				case STD_CostElementTable.TABLE_NAME + STD_CostElementTable.CODE_FLD:
					
					// Call OpenSearchForm for Cost Element selecting 
					drvResult = FormControlComponents.OpenSearchForm(STD_CostElementTable.TABLE_NAME, STD_CostElementTable.CODE_FLD, pstrColumValue, htbCondition, pblnAlwaysShow);
					if (drvResult != null)
					{
						if(pblnAlwaysShow)
						{
							dgrdData[dgrdData.Row, CST_STDItemCostTable.COSTELEMENTID_FLD] = drvResult[STD_CostElementTable.COSTELEMENTID_FLD];
							dgrdData[dgrdData.Row, STD_CostElementTable.TABLE_NAME + STD_CostElementTable.CODE_FLD] = drvResult[STD_CostElementTable.CODE_FLD];
							dgrdData[dgrdData.Row, STD_CostElementTable.TABLE_NAME + STD_CostElementTable.NAME_FLD] = drvResult[STD_CostElementTable.NAME_FLD];
						}
						else
						{
							dgrdData.Columns[CST_ActCostAllocationDetailTable.COSTELEMENTID_FLD].Value = drvResult[STD_CostElementTable.COSTELEMENTID_FLD];
							dgrdData.Columns[STD_CostElementTable.TABLE_NAME + STD_CostElementTable.CODE_FLD].Value = drvResult[STD_CostElementTable.CODE_FLD];
							dgrdData.Columns[STD_CostElementTable.TABLE_NAME + STD_CostElementTable.NAME_FLD].Value = drvResult[STD_CostElementTable.NAME_FLD];
						}
						
						btnSave.Enabled = true;
					}
					else
					{
						blnResult = pblnAlwaysShow;
					}
					break;

				case STD_CostElementTable.TABLE_NAME + STD_CostElementTable.NAME_FLD:
					
					// Call OpenSearchForm for Cost Element selecting 
					drvResult = FormControlComponents.OpenSearchForm(STD_CostElementTable.TABLE_NAME, STD_CostElementTable.NAME_FLD, pstrColumValue, htbCondition, pblnAlwaysShow);
					if (drvResult != null)
					{
						if(pblnAlwaysShow)
						{
							dgrdData[dgrdData.Row, CST_STDItemCostTable.COSTELEMENTID_FLD] = drvResult[STD_CostElementTable.COSTELEMENTID_FLD];
							dgrdData[dgrdData.Row, STD_CostElementTable.TABLE_NAME + STD_CostElementTable.CODE_FLD] = drvResult[STD_CostElementTable.CODE_FLD];
							dgrdData[dgrdData.Row, STD_CostElementTable.TABLE_NAME + STD_CostElementTable.NAME_FLD] = drvResult[STD_CostElementTable.NAME_FLD];
						}
						else
						{
							dgrdData.Columns[CST_ActCostAllocationDetailTable.COSTELEMENTID_FLD].Value = drvResult[STD_CostElementTable.COSTELEMENTID_FLD];
							dgrdData.Columns[STD_CostElementTable.TABLE_NAME + STD_CostElementTable.CODE_FLD].Value = drvResult[STD_CostElementTable.CODE_FLD];
							dgrdData.Columns[STD_CostElementTable.TABLE_NAME + STD_CostElementTable.NAME_FLD].Value = drvResult[STD_CostElementTable.NAME_FLD];
						}

						btnSave.Enabled = true;
					}
					else
					{
						blnResult = pblnAlwaysShow;
					}
					break;
			}

			return blnResult;
		}
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="pstrColumnName"></param>
		private void ClearRelatedColumns(string pstrColumnName, bool pblnUseColumnMode)
		{
			if(!pblnUseColumnMode)
			{
				switch (pstrColumnName)
				{
					case STD_CostElementTable.TABLE_NAME + STD_CostElementTable.CODE_FLD:
					case STD_CostElementTable.TABLE_NAME + STD_CostElementTable.NAME_FLD:
						//clear cost element columns
						dgrdData.Columns[CST_STDItemCostTable.COSTELEMENTID_FLD].Value = DBNull.Value;
						dgrdData.Columns[CST_STDItemCostTable.COST_FLD].Value = DBNull.Value;
						
						dgrdData.Columns[STD_CostElementTable.TABLE_NAME + STD_CostElementTable.CODE_FLD].Value = string.Empty;
						dgrdData.Columns[STD_CostElementTable.TABLE_NAME + STD_CostElementTable.NAME_FLD].Value = string.Empty;						
						break;
				}
			}
			else
			{
				switch (pstrColumnName)
				{
					case STD_CostElementTable.TABLE_NAME + STD_CostElementTable.CODE_FLD:
					case STD_CostElementTable.TABLE_NAME + STD_CostElementTable.NAME_FLD:
						
						//clear cost element columns
						dgrdData[dgrdData.Row, CST_STDItemCostTable.COST_FLD] = DBNull.Value;

						dgrdData[dgrdData.Row, STD_CostElementTable.TABLE_NAME + STD_CostElementTable.NAME_FLD] = string.Empty;
						dgrdData[dgrdData.Row, STD_CostElementTable.TABLE_NAME + STD_CostElementTable.CODE_FLD] = string.Empty;						
						break;						
				}
			}
		}

		/// <summary>
		/// Sum Total Cost
		/// </summary>
		private void SumTotalCost()
		{
			decimal decTotalCost = decimal.Zero;
			
			foreach(DataRow drow in dtbItemCostDetail.Rows)
			{
				if(drow.RowState == DataRowState.Deleted)
				{
					continue;
				}
				
				if(!drow[CST_STDItemCostTable.COST_FLD].Equals(DBNull.Value)
					&& !drow[CST_STDItemCostTable.COST_FLD].ToString().Trim().Equals(string.Empty))
				{
					decTotalCost += decimal.Parse(drow[CST_STDItemCostTable.COST_FLD].ToString());
				}
			}

			txtTotal.Text = decTotalCost.ToString(Constants.DECIMAL_NUMBERFORMAT);			
		}

		#endregion 

		#region Event Processing
		
		private void ItemStandardCost_Load(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".ItemStandardCost_Load()";
			try
			{				
				//Set form security
				Security objSecurity = new Security();
				this.Name = THIS;

				if (objSecurity.SetRightForUserOnForm(this, SystemProperty.UserName) == 0)
				{
					// You don't have the right to view this item
					PCSMessageBox.Show(ErrorCode.MESSAGE_YOU_HAVE_NO_RIGHT_TO_VIEW, MessageBoxIcon.Warning);
					this.Close();
					return;
				}
				
				//init variables				
				boItemStandardCost = new ItemStandardCostBO();
				dtbGridLayOut = FormControlComponents.StoreGridLayout(dgrdData);
				blnDataIsValid = true;				
				
				if(mintProductID > 0)
				{
					//Get information of default item					
					DataTable dtbProduct =  boItemStandardCost.GetProductItemInfo(mintProductID);
					if(dtbProduct != null)
					{
						if(dtbProduct.Rows.Count > 0)
						{
							txtModel.Text    = dtbProduct.Rows[0][ITM_ProductTable.REVISION_FLD].ToString();
							txtPartName.Text = dtbProduct.Rows[0][ITM_ProductTable.DESCRIPTION_FLD].ToString();
							txtPartNo.Text   = dtbProduct.Rows[0][ITM_ProductTable.CODE_FLD].ToString();
							txtPartNo.Tag    = dtbProduct.Rows[0][ITM_ProductTable.PRODUCTID_FLD];
							txtStockUM.Text  = dtbProduct.Rows[0][MST_UnitOfMeasureTable.TABLE_NAME + MST_UnitOfMeasureTable.CODE_FLD].ToString();
							chkMakeItem.Checked =  dtbProduct.Rows[0][ITM_ProductTable.MAKEITEM_FLD].Equals(DBNull.Value)? false : bool.Parse(dtbProduct.Rows[0][ITM_ProductTable.MAKEITEM_FLD].ToString());
						}
						else
						{
							txtModel.Text      = string.Empty;
							txtPartName.Text   = string.Empty;
							txtPartNo.Text     = string.Empty;
							txtPartNo.Tag      = string.Empty;
							txtStockUM.Text    = string.Empty;
							chkMakeItem.Checked = false;
						}
					}

					//load detail grid
					LoadDetailGrid(mintProductID, chkMakeItem.Checked);

					//reset modify status
					txtPartName.Modified = false;
					txtPartNo.Modified = false;
				}				
				else
				{
					ResetControlValue(true);
				}
				
				txtTotal.TextAlign = HorizontalAlignment.Right;

				//focus to Part No
				txtPartNo.Focus();
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
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
			catch (Exception ex)
			{
				PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
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
		
		private void ItemStandardCost_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".ItemStandardCost_Closing()";
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

		private void btnSearchPartNo_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnSearchPartNo_Click()";

			try
			{
				SelectPartNo(true);
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

		private void btnSearchPartName_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnSearchPartName_Click()";

			try
			{
				SelectPartName(true);
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
				blnDataIsValid = ValidateData();

				if(!blnDataIsValid)
				{					
					return;
				}

				if(dtbItemCostDetail.DataSet == null)
				{
					DataSet dtsTemp = new DataSet();
					dtsTemp.Tables.Add(dtbItemCostDetail);
					boItemStandardCost.UpdateDataSet(dtsTemp);
				}
				else
				{
					boItemStandardCost.UpdateDataSet(dtbItemCostDetail.DataSet);
				}
				
				//Reload Item Standard Code infor				
				LoadDetailGrid(mintProductID, chkMakeItem.Checked);
				
				//show successfull message
				PCSMessageBox.Show(ErrorCode.MESSAGE_AFTER_SAVE_DATA);
				btnSave.Enabled = false;
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

		private void btnDelete_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnDelete_Click()";

			try
			{
				if( (txtPartNo.Text.Trim() == string.Empty)
				 || (dgrdData.RowCount == 0)
				  )
				{
					return;
				}

				if (PCSMessageBox.Show(ErrorCode.MESSAGE_DELETE_RECORD, MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
				{
					return;
				}

				int iProductID = int.Parse(txtPartNo.Tag.ToString());
				boItemStandardCost.DeleteByProduct(iProductID);

				//reset controls's value
				ResetControlValue(true);

				//focus to Part No
				txtPartNo.Focus();
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

		private void btnHelp_Click(object sender, System.EventArgs e)
		{
		
		}

		private void txtPartNo_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtPartNo_Validating()";

			try
			{
				//exit if empty				
				if(txtPartNo.Text.Length == 0)
				{
					bool blnGridHasData = (dgrdData.RowCount > 0);
					ResetControlValue(blnGridHasData);
					return;
				}
				else if(!txtPartNo.Modified)
				{
					return;
				}

				e.Cancel = !SelectPartNo(false);				
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

		private void txtPartNo_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtPartNo_KeyDown()";

			try
			{
				if ((e.KeyCode == Keys.F4) && (btnSearchPartNo.Enabled))
				{
					SelectPartNo(true);
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

		private void txtPartName_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtPartName_Validating()";

			try
			{
				//exit if empty				
				if(txtPartName.Text.Length == 0)
				{
					bool blnGridHasData = (dgrdData.RowCount > 0);
					ResetControlValue(blnGridHasData);
					return;
				}
				else if(!txtPartName.Modified)
				{
					return;
				}				

				e.Cancel = !SelectPartName(false);				
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

		private void txtPartName_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtPartName_KeyDown()";

			try
			{
				if ((e.KeyCode == Keys.F4) && (btnSearchPartName.Enabled))
				{
					SelectPartName(true);
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

		private void dgrdData_AfterColUpdate(object sender, C1.Win.C1TrueDBGrid.ColEventArgs e)
		{
			const string METHOD_NAME = THIS + ".dgrdData_AfterColUpdate()";

			try
			{
				SumTotalCost();
				btnSave.Enabled = true;
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
		
		private void dgrdData_RowColChange(object sender, C1.Win.C1TrueDBGrid.RowColChangeEventArgs e)
		{
			
			const string METHOD_NAME = THIS + ".dgrdData_RowColChange()";

			try
			{
				if(dgrdData.RowCount == 0 || dgrdData.Row < 0)
				{
					return;
				}
				
				bool blnEnable = true;
				if(chkMakeItem.Checked)
				{
					//allows user input cost for Cost element which has been rollup && Type <> Material
					blnEnable  = (dgrdData[dgrdData.Row, STD_CostElementTable.COSTELEMENTTYPEID_FLD].ToString() != ((int)CostElementType.Material).ToString());
					blnEnable &= (dgrdData[dgrdData.Row, CST_STDItemCostTable.ROLLUPDATE_FLD].ToString() != string.Empty);					
				}

				numCostValue.Enabled = blnEnable;				
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
		
		private void dgrdData_ButtonClick(object sender, C1.Win.C1TrueDBGrid.ColEventArgs e)
		{
			const string METHOD_NAME = THIS + ".dgrdData_ButtonClick()";

			try
			{	
				//Set editactive status
				dgrdData.EditActive = true;

				ProcessInputDataInGrid(e.Column.DataColumn.DataField, dgrdData.Columns[e.Column.DataColumn.DataField].Value.ToString(), true);				
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
					ClearRelatedColumns(e.Column.DataColumn.DataField, false);

					return;
				}

				e.Cancel = !ProcessInputDataInGrid(e.Column.DataColumn.DataField, dgrdData.Columns[e.Column.DataColumn.DataField].Value.ToString(), false);
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
		
		private void dgrdData_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			const string METHOD_NAME = THIS + ".dgrdData_KeyDown()";

			try
			{
				//if column's value then exit immediately
				if(chkMakeItem.Checked) return;
				
				if(e.KeyCode == Keys.F4)
				{
					dgrdData.EditActive = true;
					ProcessInputDataInGrid(dgrdData.Columns[dgrdData.Col].DataField, dgrdData.Columns[dgrdData.Col].Value.ToString(), true);
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

		#endregion		

		
	}
}