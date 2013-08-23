using System;
using System.Data;
using System.Collections;
using System.Windows.Forms;
using C1.Win.C1List;
using C1.Win.C1TrueDBGrid;
using PCSComUtils.Common;
using PCSComProduct.STDCost.BO;
using PCSComProduct.STDCost.DS;
using PCSComUtils.Common.BO;
using PCSComUtils.PCSExc;
using PCSUtils.Log;
using PCSUtils.Utils;
using BeforeColUpdateEventArgs = C1.Win.C1TrueDBGrid.BeforeColUpdateEventArgs;
using C1DisplayColumn = C1.Win.C1TrueDBGrid.C1DisplayColumn;
using CancelEventArgs = System.ComponentModel.CancelEventArgs;

namespace PCSProduct.STDCost
{
	/// <summary>
	/// Summary description for CostCenterRate.
	/// </summary>
	public class CostCenterRate : System.Windows.Forms.Form
	{
		#region controls

		private C1TrueDBGrid dgrdData;
		private C1Combo cboCCN;
		private Button btnHelp;
		private Button btnClose;
		private Button btnEdit;
		private Button btnAdd;
		private Button btnSave;
		private Button btnDelete;
		private Button btnCode;
		private TextBox txtCode;
		private Label lblCCN;
		private Label lblCode;
		private Button btnName;
		private TextBox txtName;
		private Label lblName;
		private TextBox txtCurrency;
		private Label lblCurrency;

		#endregion

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		#region variables

		const string THIS = "PCSProduct.STDCost.CostCenterRate";
		STD_CostCenterRateMasterVO voRateMaster = null;
		EnumAction FormMode = EnumAction.Default;
		DataTable dtbLayout = null;
		DataSet dstData = null;
		CostCenterRateBO boCostCenterRate = null;
		private System.Windows.Forms.Label lblTotal;
		private System.Windows.Forms.TextBox txtTotal;

		#endregion

		public CostCenterRate()
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

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(CostCenterRate));
			this.dgrdData = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
			this.btnCode = new System.Windows.Forms.Button();
			this.txtCode = new System.Windows.Forms.TextBox();
			this.cboCCN = new C1.Win.C1List.C1Combo();
			this.lblCCN = new System.Windows.Forms.Label();
			this.lblCode = new System.Windows.Forms.Label();
			this.btnName = new System.Windows.Forms.Button();
			this.txtName = new System.Windows.Forms.TextBox();
			this.lblName = new System.Windows.Forms.Label();
			this.btnHelp = new System.Windows.Forms.Button();
			this.btnClose = new System.Windows.Forms.Button();
			this.btnEdit = new System.Windows.Forms.Button();
			this.btnAdd = new System.Windows.Forms.Button();
			this.btnSave = new System.Windows.Forms.Button();
			this.btnDelete = new System.Windows.Forms.Button();
			this.txtCurrency = new System.Windows.Forms.TextBox();
			this.lblCurrency = new System.Windows.Forms.Label();
			this.lblTotal = new System.Windows.Forms.Label();
			this.txtTotal = new System.Windows.Forms.TextBox();
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
			this.dgrdData.AfterColUpdate += new C1.Win.C1TrueDBGrid.ColEventHandler(this.dgrdData_AfterColUpdate);
			this.dgrdData.BeforeColUpdate += new C1.Win.C1TrueDBGrid.BeforeColUpdateEventHandler(this.dgrdData_BeforeColUpdate);
			this.dgrdData.PropBag = resources.GetString("dgrdData.PropBag");
			// 
			// btnCode
			// 
			this.btnCode.AccessibleDescription = resources.GetString("btnCode.AccessibleDescription");
			this.btnCode.AccessibleName = resources.GetString("btnCode.AccessibleName");
			this.btnCode.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnCode.Anchor")));
			this.btnCode.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCode.BackgroundImage")));
			this.btnCode.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnCode.Dock")));
			this.btnCode.Enabled = ((bool)(resources.GetObject("btnCode.Enabled")));
			this.btnCode.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnCode.FlatStyle")));
			this.btnCode.Font = ((System.Drawing.Font)(resources.GetObject("btnCode.Font")));
			this.btnCode.Image = ((System.Drawing.Image)(resources.GetObject("btnCode.Image")));
			this.btnCode.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnCode.ImageAlign")));
			this.btnCode.ImageIndex = ((int)(resources.GetObject("btnCode.ImageIndex")));
			this.btnCode.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnCode.ImeMode")));
			this.btnCode.Location = ((System.Drawing.Point)(resources.GetObject("btnCode.Location")));
			this.btnCode.Name = "btnCode";
			this.btnCode.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnCode.RightToLeft")));
			this.btnCode.Size = ((System.Drawing.Size)(resources.GetObject("btnCode.Size")));
			this.btnCode.TabIndex = ((int)(resources.GetObject("btnCode.TabIndex")));
			this.btnCode.Text = resources.GetString("btnCode.Text");
			this.btnCode.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnCode.TextAlign")));
			this.btnCode.Visible = ((bool)(resources.GetObject("btnCode.Visible")));
			this.btnCode.Click += new System.EventHandler(this.btnCode_Click);
			// 
			// txtCode
			// 
			this.txtCode.AccessibleDescription = resources.GetString("txtCode.AccessibleDescription");
			this.txtCode.AccessibleName = resources.GetString("txtCode.AccessibleName");
			this.txtCode.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("txtCode.Anchor")));
			this.txtCode.AutoSize = ((bool)(resources.GetObject("txtCode.AutoSize")));
			this.txtCode.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("txtCode.BackgroundImage")));
			this.txtCode.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("txtCode.Dock")));
			this.txtCode.Enabled = ((bool)(resources.GetObject("txtCode.Enabled")));
			this.txtCode.Font = ((System.Drawing.Font)(resources.GetObject("txtCode.Font")));
			this.txtCode.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("txtCode.ImeMode")));
			this.txtCode.Location = ((System.Drawing.Point)(resources.GetObject("txtCode.Location")));
			this.txtCode.MaxLength = ((int)(resources.GetObject("txtCode.MaxLength")));
			this.txtCode.Multiline = ((bool)(resources.GetObject("txtCode.Multiline")));
			this.txtCode.Name = "txtCode";
			this.txtCode.PasswordChar = ((char)(resources.GetObject("txtCode.PasswordChar")));
			this.txtCode.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("txtCode.RightToLeft")));
			this.txtCode.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("txtCode.ScrollBars")));
			this.txtCode.Size = ((System.Drawing.Size)(resources.GetObject("txtCode.Size")));
			this.txtCode.TabIndex = ((int)(resources.GetObject("txtCode.TabIndex")));
			this.txtCode.Text = resources.GetString("txtCode.Text");
			this.txtCode.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("txtCode.TextAlign")));
			this.txtCode.Visible = ((bool)(resources.GetObject("txtCode.Visible")));
			this.txtCode.WordWrap = ((bool)(resources.GetObject("txtCode.WordWrap")));
			this.txtCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Control_KeyDown);
			this.txtCode.Validating += new System.ComponentModel.CancelEventHandler(this.txtCode_Validating);
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
			this.cboCCN.SelectedValueChanged += new System.EventHandler(this.cboCCN_SelectedValueChanged);
			this.cboCCN.PropBag = "<?xml version=\"1.0\"?><Blob><Styles type=\"C1.Win.C1List.Design.ContextWrapper\"><Da" +
				"ta>Group{BackColor:ControlDark;Border:None,,0, 0, 0, 0;AlignVert:Center;}Style2{" +
				"}Style5{}Style4{}Style7{}Style6{}EvenRow{BackColor:Aqua;}Selected{ForeColor:High" +
				"lightText;BackColor:Highlight;}Style3{}Inactive{ForeColor:InactiveCaptionText;Ba" +
				"ckColor:InactiveCaption;}Footer{}Caption{AlignHorz:Center;}Normal{BackColor:Wind" +
				"ow;}HighlightRow{ForeColor:HighlightText;BackColor:Highlight;}Style9{AlignHorz:N" +
				"ear;}OddRow{}RecordSelector{AlignImage:Center;}Heading{Wrap:True;AlignVert:Cente" +
				"r;Border:Raised,,1, 1, 1, 1;ForeColor:ControlText;BackColor:Control;}Style8{}Sty" +
				"le10{}Style11{}Style1{}</Data></Styles><Splits><C1.Win.C1List.ListBoxView AllowC" +
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
			// lblCode
			// 
			this.lblCode.AccessibleDescription = resources.GetString("lblCode.AccessibleDescription");
			this.lblCode.AccessibleName = resources.GetString("lblCode.AccessibleName");
			this.lblCode.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblCode.Anchor")));
			this.lblCode.AutoSize = ((bool)(resources.GetObject("lblCode.AutoSize")));
			this.lblCode.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblCode.Dock")));
			this.lblCode.Enabled = ((bool)(resources.GetObject("lblCode.Enabled")));
			this.lblCode.Font = ((System.Drawing.Font)(resources.GetObject("lblCode.Font")));
			this.lblCode.ForeColor = System.Drawing.Color.Maroon;
			this.lblCode.Image = ((System.Drawing.Image)(resources.GetObject("lblCode.Image")));
			this.lblCode.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblCode.ImageAlign")));
			this.lblCode.ImageIndex = ((int)(resources.GetObject("lblCode.ImageIndex")));
			this.lblCode.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblCode.ImeMode")));
			this.lblCode.Location = ((System.Drawing.Point)(resources.GetObject("lblCode.Location")));
			this.lblCode.Name = "lblCode";
			this.lblCode.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblCode.RightToLeft")));
			this.lblCode.Size = ((System.Drawing.Size)(resources.GetObject("lblCode.Size")));
			this.lblCode.TabIndex = ((int)(resources.GetObject("lblCode.TabIndex")));
			this.lblCode.Text = resources.GetString("lblCode.Text");
			this.lblCode.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblCode.TextAlign")));
			this.lblCode.Visible = ((bool)(resources.GetObject("lblCode.Visible")));
			// 
			// btnName
			// 
			this.btnName.AccessibleDescription = resources.GetString("btnName.AccessibleDescription");
			this.btnName.AccessibleName = resources.GetString("btnName.AccessibleName");
			this.btnName.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnName.Anchor")));
			this.btnName.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnName.BackgroundImage")));
			this.btnName.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnName.Dock")));
			this.btnName.Enabled = ((bool)(resources.GetObject("btnName.Enabled")));
			this.btnName.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnName.FlatStyle")));
			this.btnName.Font = ((System.Drawing.Font)(resources.GetObject("btnName.Font")));
			this.btnName.Image = ((System.Drawing.Image)(resources.GetObject("btnName.Image")));
			this.btnName.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnName.ImageAlign")));
			this.btnName.ImageIndex = ((int)(resources.GetObject("btnName.ImageIndex")));
			this.btnName.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnName.ImeMode")));
			this.btnName.Location = ((System.Drawing.Point)(resources.GetObject("btnName.Location")));
			this.btnName.Name = "btnName";
			this.btnName.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnName.RightToLeft")));
			this.btnName.Size = ((System.Drawing.Size)(resources.GetObject("btnName.Size")));
			this.btnName.TabIndex = ((int)(resources.GetObject("btnName.TabIndex")));
			this.btnName.Text = resources.GetString("btnName.Text");
			this.btnName.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnName.TextAlign")));
			this.btnName.Visible = ((bool)(resources.GetObject("btnName.Visible")));
			this.btnName.Click += new System.EventHandler(this.btnName_Click);
			// 
			// txtName
			// 
			this.txtName.AccessibleDescription = resources.GetString("txtName.AccessibleDescription");
			this.txtName.AccessibleName = resources.GetString("txtName.AccessibleName");
			this.txtName.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("txtName.Anchor")));
			this.txtName.AutoSize = ((bool)(resources.GetObject("txtName.AutoSize")));
			this.txtName.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("txtName.BackgroundImage")));
			this.txtName.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("txtName.Dock")));
			this.txtName.Enabled = ((bool)(resources.GetObject("txtName.Enabled")));
			this.txtName.Font = ((System.Drawing.Font)(resources.GetObject("txtName.Font")));
			this.txtName.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("txtName.ImeMode")));
			this.txtName.Location = ((System.Drawing.Point)(resources.GetObject("txtName.Location")));
			this.txtName.MaxLength = ((int)(resources.GetObject("txtName.MaxLength")));
			this.txtName.Multiline = ((bool)(resources.GetObject("txtName.Multiline")));
			this.txtName.Name = "txtName";
			this.txtName.PasswordChar = ((char)(resources.GetObject("txtName.PasswordChar")));
			this.txtName.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("txtName.RightToLeft")));
			this.txtName.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("txtName.ScrollBars")));
			this.txtName.Size = ((System.Drawing.Size)(resources.GetObject("txtName.Size")));
			this.txtName.TabIndex = ((int)(resources.GetObject("txtName.TabIndex")));
			this.txtName.Text = resources.GetString("txtName.Text");
			this.txtName.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("txtName.TextAlign")));
			this.txtName.Visible = ((bool)(resources.GetObject("txtName.Visible")));
			this.txtName.WordWrap = ((bool)(resources.GetObject("txtName.WordWrap")));
			this.txtName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Control_KeyDown);
			this.txtName.Validating += new System.ComponentModel.CancelEventHandler(this.txtCode_Validating);
			// 
			// lblName
			// 
			this.lblName.AccessibleDescription = resources.GetString("lblName.AccessibleDescription");
			this.lblName.AccessibleName = resources.GetString("lblName.AccessibleName");
			this.lblName.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblName.Anchor")));
			this.lblName.AutoSize = ((bool)(resources.GetObject("lblName.AutoSize")));
			this.lblName.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblName.Dock")));
			this.lblName.Enabled = ((bool)(resources.GetObject("lblName.Enabled")));
			this.lblName.Font = ((System.Drawing.Font)(resources.GetObject("lblName.Font")));
			this.lblName.ForeColor = System.Drawing.Color.Maroon;
			this.lblName.Image = ((System.Drawing.Image)(resources.GetObject("lblName.Image")));
			this.lblName.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblName.ImageAlign")));
			this.lblName.ImageIndex = ((int)(resources.GetObject("lblName.ImageIndex")));
			this.lblName.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblName.ImeMode")));
			this.lblName.Location = ((System.Drawing.Point)(resources.GetObject("lblName.Location")));
			this.lblName.Name = "lblName";
			this.lblName.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblName.RightToLeft")));
			this.lblName.Size = ((System.Drawing.Size)(resources.GetObject("lblName.Size")));
			this.lblName.TabIndex = ((int)(resources.GetObject("lblName.TabIndex")));
			this.lblName.Text = resources.GetString("lblName.Text");
			this.lblName.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblName.TextAlign")));
			this.lblName.Visible = ((bool)(resources.GetObject("lblName.Visible")));
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
			// btnEdit
			// 
			this.btnEdit.AccessibleDescription = resources.GetString("btnEdit.AccessibleDescription");
			this.btnEdit.AccessibleName = resources.GetString("btnEdit.AccessibleName");
			this.btnEdit.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnEdit.Anchor")));
			this.btnEdit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnEdit.BackgroundImage")));
			this.btnEdit.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnEdit.Dock")));
			this.btnEdit.Enabled = ((bool)(resources.GetObject("btnEdit.Enabled")));
			this.btnEdit.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnEdit.FlatStyle")));
			this.btnEdit.Font = ((System.Drawing.Font)(resources.GetObject("btnEdit.Font")));
			this.btnEdit.Image = ((System.Drawing.Image)(resources.GetObject("btnEdit.Image")));
			this.btnEdit.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnEdit.ImageAlign")));
			this.btnEdit.ImageIndex = ((int)(resources.GetObject("btnEdit.ImageIndex")));
			this.btnEdit.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnEdit.ImeMode")));
			this.btnEdit.Location = ((System.Drawing.Point)(resources.GetObject("btnEdit.Location")));
			this.btnEdit.Name = "btnEdit";
			this.btnEdit.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnEdit.RightToLeft")));
			this.btnEdit.Size = ((System.Drawing.Size)(resources.GetObject("btnEdit.Size")));
			this.btnEdit.TabIndex = ((int)(resources.GetObject("btnEdit.TabIndex")));
			this.btnEdit.Text = resources.GetString("btnEdit.Text");
			this.btnEdit.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnEdit.TextAlign")));
			this.btnEdit.Visible = ((bool)(resources.GetObject("btnEdit.Visible")));
			this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
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
			this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
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
			// CostCenterRate
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
			this.Controls.Add(this.txtTotal);
			this.Controls.Add(this.txtCurrency);
			this.Controls.Add(this.txtName);
			this.Controls.Add(this.txtCode);
			this.Controls.Add(this.lblTotal);
			this.Controls.Add(this.lblCurrency);
			this.Controls.Add(this.btnHelp);
			this.Controls.Add(this.btnClose);
			this.Controls.Add(this.btnEdit);
			this.Controls.Add(this.btnAdd);
			this.Controls.Add(this.btnSave);
			this.Controls.Add(this.btnDelete);
			this.Controls.Add(this.btnName);
			this.Controls.Add(this.lblName);
			this.Controls.Add(this.btnCode);
			this.Controls.Add(this.cboCCN);
			this.Controls.Add(this.lblCCN);
			this.Controls.Add(this.lblCode);
			this.Controls.Add(this.dgrdData);
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
			this.Name = "CostCenterRate";
			this.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("$this.RightToLeft")));
			this.StartPosition = ((System.Windows.Forms.FormStartPosition)(resources.GetObject("$this.StartPosition")));
			this.Text = resources.GetString("$this.Text");
			this.Closing += new System.ComponentModel.CancelEventHandler(this.CostCenterRate_Closing);
			this.Load += new System.EventHandler(this.CostCenterRate_Load);
			((System.ComponentModel.ISupportInitialize)(this.dgrdData)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.cboCCN)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		#region Events

		/// <summary>
		/// Check security, load data to ccn then set default value for CCN, currency.
		/// Switch form mode to Default.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void CostCenterRate_Load(object sender, EventArgs e)
		{
			const string METHOD_NAME = THIS + ".CostCenterRate_Load()";
			try
			{
				Security objSecurity = new Security();
				this.Name = THIS;
				if (objSecurity.SetRightForUserOnForm(this, SystemProperty.UserName) == 0)
				{
					this.Close();
					PCSMessageBox.Show(ErrorCode.MESSAGE_YOU_HAVE_NO_RIGHT_TO_VIEW, MessageBoxIcon.Warning);
					return;
				}

				this.MaximizeBox = false;
				this.FormBorderStyle = FormBorderStyle.FixedSingle;

				voRateMaster = new STD_CostCenterRateMasterVO();
				dstData = new DataSet();
				
				UtilsBO boUtils = new UtilsBO();
				// put data into CCN combo
				FormControlComponents.PutDataIntoC1ComboBox(cboCCN, boUtils.ListCCN().Tables[0], MST_CCNTable.CODE_FLD, MST_CCNTable.CCNID_FLD, MST_CCNTable.TABLE_NAME);
				// set default CCN
				cboCCN.SelectedValue = SystemProperty.CCNID;
				// home currency
				txtCurrency.Text = SystemProperty.HomeCurrency;
				// store grid layout
				dtbLayout = FormControlComponents.StoreGridLayout(dgrdData);
				txtTotal.TextAlign = HorizontalAlignment.Right;
				boCostCenterRate = new CostCenterRateBO();
				// switch form mode
				SwitchFormMode(FormMode);
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

		/// <summary>
		/// Open search form to select exist Cost Center Rate. Search by code
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnCode_Click(object sender, EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnCode_Click()";
			try
			{
				DataRowView drvResult = null;
				Hashtable htbCondition = new Hashtable();
				if (cboCCN.SelectedValue != null)
					htbCondition.Add(STD_CostCenterRateMasterTable.CCNID_FLD, cboCCN.SelectedValue.ToString());
				else
				{
					PCSMessageBox.Show(ErrorCode.MESSAGE_RGA_CCN, MessageBoxIcon.Warning);
					cboCCN.Focus();
					return;
				}
				if (sender is TextBox && sender != null)
					drvResult = FormControlComponents.OpenSearchForm(STD_CostCenterRateMasterTable.TABLE_NAME, STD_CostCenterRateMasterTable.CODE_FLD,
						txtCode.Text.Trim(), htbCondition, false);
				else
					drvResult = FormControlComponents.OpenSearchForm(STD_CostCenterRateMasterTable.TABLE_NAME, STD_CostCenterRateMasterTable.CODE_FLD,
						txtCode.Text.Trim(), htbCondition, true);
				if (drvResult != null)
					FillMasterData(drvResult.Row);
				else
					txtCode.Focus();
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

		/// <summary>
		/// Open search form to select exist cost center rate. Search by name
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnName_Click(object sender, EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnName_Click()";
			try
			{
				DataRowView drvResult = null;
				Hashtable htbCondition = new Hashtable();
				if (cboCCN.SelectedValue != null)
					htbCondition.Add(STD_CostCenterRateMasterTable.CCNID_FLD, cboCCN.SelectedValue.ToString());
				else
				{
					PCSMessageBox.Show(ErrorCode.MESSAGE_RGA_CCN, MessageBoxIcon.Warning);
					cboCCN.Focus();
					return;
				}
				if (sender is TextBox && sender != null)
					drvResult = FormControlComponents.OpenSearchForm(STD_CostCenterRateMasterTable.TABLE_NAME, STD_CostCenterRateMasterTable.NAME_FLD,
						txtName.Text.Trim(), htbCondition, false);
				else
					drvResult = FormControlComponents.OpenSearchForm(STD_CostCenterRateMasterTable.TABLE_NAME, STD_CostCenterRateMasterTable.NAME_FLD,
						txtName.Text.Trim(), htbCondition, true);
				if (drvResult != null)
					FillMasterData(drvResult.Row);
				else
					txtName.Focus();
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

		/// <summary>
		/// Turn form mode to Add
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnAdd_Click(object sender, EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnAdd_Click()";
			try
			{
				// clear form
				ClearForm();
				// popluate data from cost elements
				dstData = boCostCenterRate.GetDetailCost(0, EnumAction.Add);
				// bind to grid
				BindData(dstData);
				// turn form mode to Add
				FormMode = EnumAction.Add;
				// switch form mode
				SwitchFormMode(FormMode);
				// focus on Code
				txtCode.Focus();
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

		/// <summary>
		/// Validating data then Save data to database
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnSave_Click(object sender, EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnSave_Click()";
			DataSet dstCopy = new DataSet();
			try
			{
				// validate data first
				bool blnIsValid = ValidateData();
				if (!blnIsValid)
					return;
				// make a copy of detail data
				dstCopy = dstData.Copy();
				// save data
				bool blnSucceed = SaveData();
				if (blnSucceed)
				{
					// turn form mode to Default
					FormMode = EnumAction.Default;
					Security.UpdateUserNameModifyTransaction(this, STD_CostCenterRateMasterTable.COSTCENTERRATEMASTERID_FLD, voRateMaster.CostCenterRateMasterID);
					// get new data
					dstData = new DataSet();
					dstData = boCostCenterRate.GetDetailCost(voRateMaster.CostCenterRateMasterID, FormMode);
					// bind to grid
					BindData(dstData);
					// switch form mode
					SwitchFormMode(FormMode);
					// display successful message
					PCSMessageBox.Show(ErrorCode.MESSAGE_AFTER_SAVE_DATA, MessageBoxButtons.OK, MessageBoxIcon.Information);
					txtCode.Modified = false;
					txtName.Modified = false;
				}
				else
					PCSMessageBox.Show(ErrorCode.MESSAGE_CANNOT_SAVE_TO_DB, MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			catch (PCSException ex)
			{
				if (ex.mCode == ErrorCode.DUPLICATE_KEY)
				{
					PCSMessageBox.Show(ErrorCode.MESSAGE_CODE_NAME_UNIQUE, MessageBoxIcon.Error);
					txtCode.Focus();
				}
				else
					PCSMessageBox.Show(ex.mCode, MessageBoxIcon.Error);
				try
				{
					Logger.LogMessage(ex.CauseException, METHOD_NAME, Level.ERROR);
				}
				catch
				{
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION);
				}
				// restore the source
				dstData = new DataSet();
				dstData = dstCopy.Copy();
				BindData(dstData);
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
				// restore the source
				dstData = new DataSet();
				dstData = dstCopy.Copy();
				BindData(dstData);
			}
		}

		/// <summary>
		/// Turn form mode to Edit
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnEdit_Click(object sender, EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnEdit_Click()";
			try
			{
				// get detail data
				dstData = boCostCenterRate.GetDetailCost(voRateMaster.CostCenterRateMasterID, EnumAction.Edit);
				// bind data to grid
				BindData(dstData);
				// turn form mode to Edit
				FormMode = EnumAction.Edit;
				// switch form mode
				SwitchFormMode(FormMode);
				// focus on Code
				txtCode.Focus();
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

		/// <summary>
		/// Delete all data (master and details)
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnDelete_Click(object sender, EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnDelete_Click()";
			try
			{
				if(Security.NoRightToDeleteTransaction(this, STD_CostCenterRateMasterTable.COSTCENTERRATEMASTERID_FLD, voRateMaster.CostCenterRateMasterID))
					return;
				if (!dgrdData.EditActive)
				{
					if (PCSMessageBox.Show(ErrorCode.MESSAGE_DELETE_RECORD, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
					{
						// delete data
						boCostCenterRate.Delete(voRateMaster);
						// clear form
						ClearForm();
						// clear data in dataset
						dstData.Tables[0].Clear();
						// display successful message
						PCSMessageBox.Show(ErrorCode.MESSAGE_AFTER_SAVE_DATA, MessageBoxButtons.OK, MessageBoxIcon.Information);
						// turn form mode to default
						FormMode = EnumAction.Default;
						// switch form mode
						SwitchFormMode(FormMode);
						txtCode.Modified = false;
						txtName.Modified = false;
						// focus on Code
						txtCode.Focus();
					}
				}
				else
					PCSMessageBox.Show(ErrorCode.MESSAGE_CANNOT_DELETE_COST_CENTER_RATE, MessageBoxIcon.Error);
			}
			catch (PCSException ex)
			{
				if (ex.mCode == ErrorCode.CASCADE_DELETE_PREVENT)
					PCSMessageBox.Show(ErrorCode.MESSAGE_CANNOT_DELETE_COST_CENTER_RATE, MessageBoxIcon.Error);
				else
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

		/// <summary>
		/// Close the form
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnClose_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		/// <summary>
		/// Button help pressed
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnHelp_Click(object sender, EventArgs e)
		{
		
		}

		/// <summary>
		/// Validate all text box
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void txtCode_Validating(object sender, CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtCode_Validating()";
			try
			{
				if (sender.Equals(txtCode))
				{
					if (txtCode.Modified && btnCode.Enabled)
					{
						if (txtCode.Text.Trim() != string.Empty)
							btnCode_Click(sender, e);
						else
						{
							txtCode.Text = string.Empty;
							txtName.Text = string.Empty;
							if ((dstData != null) && (dstData.Tables.Count > 0))
								dstData.Tables[0].Clear();
							foreach (C1DisplayColumn dcolC1 in dgrdData.Splits[0].DisplayColumns)
								dcolC1.Locked = true;
						}
					}
				}
				else if (sender.Equals(txtName))
				{
					if (txtName.Modified && btnName.Enabled)
					{
						if (txtName.Text.Trim() != string.Empty)
							btnName_Click(sender, e);
						else
						{
							txtCode.Text = string.Empty;
							txtName.Text = string.Empty;
							if ((dstData != null) && (dstData.Tables.Count > 0))
								dstData.Tables[0].Clear();
							foreach (C1DisplayColumn dcolC1 in dgrdData.Splits[0].DisplayColumns)
								dcolC1.Locked = true;
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

		/// <summary>
		/// Handle keydown event of form and textbox
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Control_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			const string METHOD_NAME = THIS + ".Control_KeyDown()";
			try
			{
				if (sender.GetType().Equals(typeof(TextBox)))
				{
					// user press F4 on text box
					if (e.KeyCode == Keys.F4)
					{
						if (sender.Equals(txtCode) && btnCode.Enabled)
							btnCode_Click(null, null);
						else if (sender.Equals(txtName) && btnName.Enabled)
							btnName_Click(null, null);
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
		/// <summary>
		/// Before update data
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void dgrdData_BeforeColUpdate(object sender, BeforeColUpdateEventArgs e)
		{
			const string METHOD_NAME = THIS + ".dgrdData_KeyDown()";
			try
			{
				// check cost value
				if (e.Column.DataColumn.DataField == STD_CostCenterRateDetailTable.COST_FLD)
				{
					try
					{
						decimal decCost = decimal.Parse(dgrdData.Columns[STD_CostCenterRateDetailTable.COST_FLD].Text.Trim());

						//HACK: delete by Tuan TQ. 18 May, 2006: allow enter negative value
						//if (decCost < decimal.Zero)
						//{
						//	PCSMessageBox.Show(ErrorCode.POSITIVE_NUMBER, MessageBoxButtons.OK, MessageBoxIcon.Error);
						//	e.Cancel = true;
						//}
						//End hank
					}
					catch (OverflowException)
					{
						string[] strMsg = new string[2];
						strMsg[0] = "0";
						strMsg[1] = decimal.MaxValue.ToString(Constants.DECIMAL_NUMBERFORMAT);
						PCSMessageBox.Show(ErrorCode.MESSAGE_NUMBER_MUST_IN_SCOPE, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, strMsg);
						e.Cancel = true;
					}
					catch
					{
						PCSMessageBox.Show(ErrorCode.MESSAGE_INVALID_NUMERIC, MessageBoxButtons.OK, MessageBoxIcon.Error);
						e.Cancel = true;
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

		/// <summary>
		/// Update total cost
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void dgrdData_AfterColUpdate(object sender, C1.Win.C1TrueDBGrid.ColEventArgs e)
		{
			const string METHOD_NAME = THIS + ".dgrdData_AfterColUpdate()";
			try
			{
				decimal decTotal = 0;
				try
				{
					decTotal = CalculateTotal();
					txtTotal.Text = decTotal.ToString(Constants.DECIMAL_NUMBERFORMAT);
				}
				catch (OverflowException)
				{
					string[] strMsg = new string[2];
					strMsg[0] = "0";
					strMsg[1] = decimal.MaxValue.ToString(Constants.DECIMAL_NUMBERFORMAT);
					PCSMessageBox.Show(ErrorCode.MESSAGE_NUMBER_MUST_IN_SCOPE, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, strMsg);
					dgrdData.Focus();
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
		/// <summary>
		/// Check form mode to make sure user close form in right way. Else display confirm message
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void CostCenterRate_Closing(object sender, CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".CostCenterRate_Closing()";
			try
			{
				if (FormMode != EnumAction.Default)
				{
					// ask user to save change
					DialogResult dlgResult = PCSMessageBox.Show(ErrorCode.MESSAGE_QUESTION_STORE_INTO_DATABASE, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
					switch (dlgResult)
					{
						case DialogResult.Yes:
							try
							{
								if (!ValidateData())
								{
									e.Cancel = true;
									break;
								}
								if (!SaveData())
									e.Cancel = true;
								else
								{
									Security.UpdateUserNameModifyTransaction(this, STD_CostCenterRateMasterTable.COSTCENTERRATEMASTERID_FLD, voRateMaster.CostCenterRateMasterID);
									// display successful message
									PCSMessageBox.Show(ErrorCode.MESSAGE_AFTER_SAVE_DATA, MessageBoxButtons.OK, MessageBoxIcon.Information);
								}
							}
							catch
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

		/// <summary>
		/// When user change CCN, change the home currency
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void cboCCN_SelectedValueChanged(object sender, EventArgs e)
		{
			const string METHOD_NAME = THIS + ".cboCCN_SelectedValueChanged()";
			try
			{
				if (cboCCN.SelectedValue != null && cboCCN.SelectedValue != DBNull.Value)
				{
					UtilsBO boUtils = new UtilsBO();
					string strCurrency = boUtils.GetHomeCurrency(Convert.ToInt32(cboCCN.SelectedValue));
					txtCurrency.Text = strCurrency.Trim();
				}
				txtCode.Text = string.Empty;
				txtName.Text = string.Empty;
				txtTotal.Text = string.Empty;
				if (FormMode == EnumAction.Default)
				{
					if (dstData.Tables.Count > 0)
						dstData.Tables[0].Clear();
				}
				else
				{
					if (dstData.Tables.Count > 0)
					{
						for (int i = 0; i < dgrdData.RowCount; i++)
						{
							dgrdData.Row = i;
							dgrdData.Columns[STD_CostCenterRateDetailTable.COST_FLD].Text = string.Empty;
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
		#endregion

		#region Methods

		/// <summary>
		/// Validate data before save to database
		/// </summary>
		/// <returns></returns>
		private bool ValidateData()
		{
			// check mandatory field
			if (FormControlComponents.CheckMandatory(cboCCN))
			{
				PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxButtons.OK, MessageBoxIcon.Error);
				cboCCN.Focus();
				return false;
			}
			if (FormControlComponents.CheckMandatory(txtCode))
			{
				PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxButtons.OK, MessageBoxIcon.Error);
				txtCode.Focus();
				return false;
			}

			if (FormControlComponents.CheckMandatory(txtName))
			{
				PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxButtons.OK, MessageBoxIcon.Error);
				txtName.Focus();
				return false;
			}
			int intTotalRows = dgrdData.RowCount;
			
			if (intTotalRows <= 0)
			{
				PCSMessageBox.Show(ErrorCode.MESSAGE_PORECEIPT_INPUT_DETAIL, MessageBoxIcon.Error);
				return false;
			}
			for (int i = 0; i < intTotalRows; i++)
			{
				dgrdData.Row = i;

				#region Cost

				if (dgrdData[i, STD_CostCenterRateDetailTable.COST_FLD] == null 
					|| dgrdData[i, STD_CostCenterRateDetailTable.COST_FLD] == DBNull.Value)
				{
					continue;
				}
				else
				{				
					if (dgrdData[i, STD_CostCenterRateDetailTable.COST_FLD].ToString().Trim() == string.Empty)
					{
						continue;
					}

				}

				try
				{
					decimal decCost = decimal.Parse(dgrdData[i, STD_CostCenterRateDetailTable.COST_FLD].ToString());
					//HACK: delete by Tuan TQ. 18 May, 2006: allow enter negative value
					//if (decCost < decimal.Zero)
					//{
					//	PCSMessageBox.Show(ErrorCode.POSITIVE_NUMBER, MessageBoxButtons.OK, MessageBoxIcon.Error);
					//	dgrdData.Col = dgrdData.Columns.IndexOf(dgrdData.Columns[STD_CostCenterRateDetailTable.COST_FLD]);
					//	dgrdData.Select();
					//	return false;
					//}
					//End hack
				}
				catch (OverflowException)
				{
					string[] strMsg = new string[1];
					strMsg[0] = "0 - " + decimal.MaxValue.ToString(Constants.DECIMAL_NUMBERFORMAT);
					PCSMessageBox.Show(ErrorCode.MESSAGE_NUMBER_MUST_IN_SCOPE, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, strMsg);
					dgrdData.Col = dgrdData.Columns.IndexOf(dgrdData.Columns[STD_CostCenterRateDetailTable.COST_FLD]);
					dgrdData.Select();
					return false;
				}
				catch
				{
					PCSMessageBox.Show(ErrorCode.MESSAGE_INVALID_NUMERIC, MessageBoxButtons.OK, MessageBoxIcon.Error);
					dgrdData.Col = dgrdData.Columns.IndexOf(dgrdData.Columns[STD_CostCenterRateDetailTable.COST_FLD]);
					dgrdData.Select();
					return false;
				}

				#endregion
			}
			return true;
		}
		/// <summary>
		/// Save all data to database
		/// </summary>
		/// <returns>true if succeed. false if failure</returns>
		private bool SaveData()
		{
			voRateMaster.Code = txtCode.Text.Trim();
			voRateMaster.Name = txtName.Text.Trim();
			voRateMaster.CCNID = int.Parse(cboCCN.SelectedValue.ToString());
			switch (FormMode)
			{
				case EnumAction.Add:
					voRateMaster.CostCenterRateMasterID = boCostCenterRate.AddAndReturnID(voRateMaster, dstData);
					break;
				case EnumAction.Edit:
					boCostCenterRate.Update(voRateMaster, dstData);
					break;
			}
			return true;
		}

		/// <summary>
		/// Switch form mode based on EnumAction
		/// </summary>
		/// <param name="mMode">Mode</param>
		private void SwitchFormMode(EnumAction mMode)
		{
			switch (mMode)
			{
				case EnumAction.Add:
					btnAdd.Enabled = false;
					btnSave.Enabled = true;
					btnEdit.Enabled = false;
					btnDelete.Enabled = false;
					btnCode.Enabled = false;
					btnName.Enabled = false;
					// unlock the grid
					dgrdData.Splits[0].Locked = false;
					// unlock Cost column
					dgrdData.Splits[0].DisplayColumns[STD_CostCenterRateDetailTable.COST_FLD].Locked = false;
					// lock cost element code
					dgrdData.Splits[0].DisplayColumns[STD_CostElementTable.CODE_FLD].Locked = true;
					// lock cost element name
					dgrdData.Splits[0].DisplayColumns[STD_CostElementTable.NAME_FLD].Locked = true;
					break;
				case EnumAction.Edit:
					btnAdd.Enabled = false;
					btnSave.Enabled = true;
					btnEdit.Enabled = false;
					btnDelete.Enabled = false;
					btnCode.Enabled = false;
					btnName.Enabled = false;
					// unlock the grid
					dgrdData.Splits[0].Locked = false;
					// unlock Cost column
					dgrdData.Splits[0].DisplayColumns[STD_CostCenterRateDetailTable.COST_FLD].Locked = false;
					// lock cost element code
					dgrdData.Splits[0].DisplayColumns[STD_CostElementTable.CODE_FLD].Locked = true;
					// lock cost element name
					dgrdData.Splits[0].DisplayColumns[STD_CostElementTable.NAME_FLD].Locked = true;
					break;
				case EnumAction.Default:
					btnAdd.Enabled = true;
					btnSave.Enabled = false;
					btnEdit.Enabled = false;
					btnDelete.Enabled = false;
					btnCode.Enabled = true;
					btnName.Enabled = true;
					// lock the grid
					dgrdData.Splits[0].Locked = true;
					if (voRateMaster != null && voRateMaster.CostCenterRateMasterID > 0)
					{
						btnEdit.Enabled = true;
						btnDelete.Enabled = true;
					}
					break;
			}
		}

		/// <summary>
		/// Fill master rate from search result. List all detail and bind to grid
		/// </summary>
		/// <param name="pdrowData">Search Result</param>
		private void FillMasterData(DataRow pdrowData)
		{
			// master id
			voRateMaster.CostCenterRateMasterID = int.Parse(pdrowData[STD_CostCenterRateMasterTable.COSTCENTERRATEMASTERID_FLD].ToString());
			// code
			txtCode.Text = voRateMaster.Code = pdrowData[STD_CostCenterRateMasterTable.CODE_FLD].ToString();
			// name
			txtName.Text = voRateMaster.Name = pdrowData[STD_CostCenterRateMasterTable.NAME_FLD].ToString();
			// get detail data
			dstData = boCostCenterRate.GetDetailCost(voRateMaster.CostCenterRateMasterID, FormMode);
			// bind data to grid
			BindData(dstData);
			// calculate total cost
			decimal decTotal = CalculateTotal();
			txtTotal.Text = decTotal.ToString(Constants.DECIMAL_NUMBERFORMAT);
			SwitchFormMode(FormMode);
		}

		/// <summary>
		/// Binding data to grid
		/// </summary>
		/// <param name="pdstData">Data</param>
		private void BindData(DataSet pdstData)
		{
			// binding datasource
			dgrdData.DataSource = pdstData.Tables[0];
			// restore grid layout
			FormControlComponents.RestoreGridLayout(dgrdData, dtbLayout);
			// set number format for Cost column
			dgrdData.Columns[STD_CostCenterRateDetailTable.COST_FLD].NumberFormat = Constants.DECIMAL_NUMBERFORMAT;
		}

		/// <summary>
		/// Clear form data
		/// </summary>
		private void ClearForm()
		{
			voRateMaster = new STD_CostCenterRateMasterVO();
			if (dstData.Tables.Count > 0)
				dstData.Tables[0].Clear();
			txtCode.Text = string.Empty;
			txtName.Text = string.Empty;
			txtTotal.Text = string.Empty;
		}

		/// <summary>
		/// Calculate total cost in grid
		/// </summary>
		/// <returns>Total Cost</returns>
		private decimal CalculateTotal()
		{
			// sum cost column
			decimal decTotal = 0;
			foreach (DataRow drowdata in dstData.Tables[0].Rows)
			{
				try
				{
					decTotal += (decimal)drowdata[STD_CostCenterRateDetailTable.COST_FLD];
				}
				catch{}
			}
			return decTotal;
		}

		#endregion
	}
}
