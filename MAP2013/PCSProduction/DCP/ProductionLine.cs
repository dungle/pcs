using System;
using System.Data;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

//Using PCS's namespaces
using PCSComProduction.DCP.BO;
using PCSComUtils.Common;
using PCSComUtils.Common.BO;
using PCSComUtils.MasterSetup.DS;
using PCSComUtils.PCSExc;
using PCSUtils.Log;
using PCSUtils.Utils;

namespace PCSProduction.DCP
{
	/// <summary>
	/// Summary description for ProductionLine.
	/// </summary>
	public class ProductionLine : System.Windows.Forms.Form
	{
		#region Windows Generation Decalaration

		private C1.Win.C1List.C1Combo cboCCN;
		private System.Windows.Forms.Label lblCCN;
		private System.Windows.Forms.Label lblProductionLine;
		private System.Windows.Forms.Label lblWorkCenterList;
		private System.Windows.Forms.TextBox txtProductionLine;
		private System.Windows.Forms.Button btnProductionLineSearch;
		private C1.Win.C1TrueDBGrid.C1TrueDBGrid dgrdData;
		private System.Windows.Forms.Button btnSave;
		private System.Windows.Forms.Button btnHelp;
		private System.Windows.Forms.Button btnClose;
		private System.Windows.Forms.Button btnDelete;
		private System.Windows.Forms.Label lblDepartment;
		private System.Windows.Forms.TextBox txtDepartment;
		private System.Windows.Forms.Button btnDepartmentSearch;
		private System.Windows.Forms.TextBox txtDepartmentName;
		private System.Windows.Forms.TextBox txtProductionLineName;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		#endregion Windows Generation Decalaration

		#region Declaration

		#region Constants
		
		private const string THIS = "PCSProduction.DCP.ProductionLine";		
		private const string ZERO_STRING = "0";	
		
		#endregion Constants
		
		private bool blnDataIsValid = false;
		private bool blnGridDataIsValid = false;
		private int intCurrentRow = 0;
		private DataTable dtbGridLayOut;
		private DataTable dtbWorkCenter;				
		private ProductionLineBO boProductionLine;

		#endregion Declaration
		
		#region Constructor, Destructor
		public ProductionLine()
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(ProductionLine));
			this.txtProductionLine = new System.Windows.Forms.TextBox();
			this.btnProductionLineSearch = new System.Windows.Forms.Button();
			this.cboCCN = new C1.Win.C1List.C1Combo();
			this.lblCCN = new System.Windows.Forms.Label();
			this.lblProductionLine = new System.Windows.Forms.Label();
			this.dgrdData = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
			this.btnSave = new System.Windows.Forms.Button();
			this.btnHelp = new System.Windows.Forms.Button();
			this.btnClose = new System.Windows.Forms.Button();
			this.btnDelete = new System.Windows.Forms.Button();
			this.lblWorkCenterList = new System.Windows.Forms.Label();
			this.txtDepartment = new System.Windows.Forms.TextBox();
			this.btnDepartmentSearch = new System.Windows.Forms.Button();
			this.lblDepartment = new System.Windows.Forms.Label();
			this.txtDepartmentName = new System.Windows.Forms.TextBox();
			this.txtProductionLineName = new System.Windows.Forms.TextBox();
			((System.ComponentModel.ISupportInitialize)(this.cboCCN)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dgrdData)).BeginInit();
			this.SuspendLayout();
			// 
			// txtProductionLine
			// 
			this.txtProductionLine.AccessibleDescription = resources.GetString("txtProductionLine.AccessibleDescription");
			this.txtProductionLine.AccessibleName = resources.GetString("txtProductionLine.AccessibleName");
			this.txtProductionLine.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("txtProductionLine.Anchor")));
			this.txtProductionLine.AutoSize = ((bool)(resources.GetObject("txtProductionLine.AutoSize")));
			this.txtProductionLine.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("txtProductionLine.BackgroundImage")));
			this.txtProductionLine.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("txtProductionLine.Dock")));
			this.txtProductionLine.Enabled = ((bool)(resources.GetObject("txtProductionLine.Enabled")));
			this.txtProductionLine.Font = ((System.Drawing.Font)(resources.GetObject("txtProductionLine.Font")));
			this.txtProductionLine.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("txtProductionLine.ImeMode")));
			this.txtProductionLine.Location = ((System.Drawing.Point)(resources.GetObject("txtProductionLine.Location")));
			this.txtProductionLine.MaxLength = ((int)(resources.GetObject("txtProductionLine.MaxLength")));
			this.txtProductionLine.Multiline = ((bool)(resources.GetObject("txtProductionLine.Multiline")));
			this.txtProductionLine.Name = "txtProductionLine";
			this.txtProductionLine.PasswordChar = ((char)(resources.GetObject("txtProductionLine.PasswordChar")));
			this.txtProductionLine.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("txtProductionLine.RightToLeft")));
			this.txtProductionLine.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("txtProductionLine.ScrollBars")));
			this.txtProductionLine.Size = ((System.Drawing.Size)(resources.GetObject("txtProductionLine.Size")));
			this.txtProductionLine.TabIndex = ((int)(resources.GetObject("txtProductionLine.TabIndex")));
			this.txtProductionLine.Text = resources.GetString("txtProductionLine.Text");
			this.txtProductionLine.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("txtProductionLine.TextAlign")));
			this.txtProductionLine.Visible = ((bool)(resources.GetObject("txtProductionLine.Visible")));
			this.txtProductionLine.WordWrap = ((bool)(resources.GetObject("txtProductionLine.WordWrap")));
			this.txtProductionLine.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtProductionLine_KeyDown);
			this.txtProductionLine.Validating += new System.ComponentModel.CancelEventHandler(this.txtProductionLine_Validating);
			// 
			// btnProductionLineSearch
			// 
			this.btnProductionLineSearch.AccessibleDescription = resources.GetString("btnProductionLineSearch.AccessibleDescription");
			this.btnProductionLineSearch.AccessibleName = resources.GetString("btnProductionLineSearch.AccessibleName");
			this.btnProductionLineSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnProductionLineSearch.Anchor")));
			this.btnProductionLineSearch.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnProductionLineSearch.BackgroundImage")));
			this.btnProductionLineSearch.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnProductionLineSearch.Dock")));
			this.btnProductionLineSearch.Enabled = ((bool)(resources.GetObject("btnProductionLineSearch.Enabled")));
			this.btnProductionLineSearch.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnProductionLineSearch.FlatStyle")));
			this.btnProductionLineSearch.Font = ((System.Drawing.Font)(resources.GetObject("btnProductionLineSearch.Font")));
			this.btnProductionLineSearch.Image = ((System.Drawing.Image)(resources.GetObject("btnProductionLineSearch.Image")));
			this.btnProductionLineSearch.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnProductionLineSearch.ImageAlign")));
			this.btnProductionLineSearch.ImageIndex = ((int)(resources.GetObject("btnProductionLineSearch.ImageIndex")));
			this.btnProductionLineSearch.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnProductionLineSearch.ImeMode")));
			this.btnProductionLineSearch.Location = ((System.Drawing.Point)(resources.GetObject("btnProductionLineSearch.Location")));
			this.btnProductionLineSearch.Name = "btnProductionLineSearch";
			this.btnProductionLineSearch.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnProductionLineSearch.RightToLeft")));
			this.btnProductionLineSearch.Size = ((System.Drawing.Size)(resources.GetObject("btnProductionLineSearch.Size")));
			this.btnProductionLineSearch.TabIndex = ((int)(resources.GetObject("btnProductionLineSearch.TabIndex")));
			this.btnProductionLineSearch.Text = resources.GetString("btnProductionLineSearch.Text");
			this.btnProductionLineSearch.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnProductionLineSearch.TextAlign")));
			this.btnProductionLineSearch.Visible = ((bool)(resources.GetObject("btnProductionLineSearch.Visible")));
			this.btnProductionLineSearch.Click += new System.EventHandler(this.btnProductionLineSearch_Click);
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
			// lblProductionLine
			// 
			this.lblProductionLine.AccessibleDescription = resources.GetString("lblProductionLine.AccessibleDescription");
			this.lblProductionLine.AccessibleName = resources.GetString("lblProductionLine.AccessibleName");
			this.lblProductionLine.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblProductionLine.Anchor")));
			this.lblProductionLine.AutoSize = ((bool)(resources.GetObject("lblProductionLine.AutoSize")));
			this.lblProductionLine.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblProductionLine.Dock")));
			this.lblProductionLine.Enabled = ((bool)(resources.GetObject("lblProductionLine.Enabled")));
			this.lblProductionLine.Font = ((System.Drawing.Font)(resources.GetObject("lblProductionLine.Font")));
			this.lblProductionLine.ForeColor = System.Drawing.Color.Maroon;
			this.lblProductionLine.Image = ((System.Drawing.Image)(resources.GetObject("lblProductionLine.Image")));
			this.lblProductionLine.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblProductionLine.ImageAlign")));
			this.lblProductionLine.ImageIndex = ((int)(resources.GetObject("lblProductionLine.ImageIndex")));
			this.lblProductionLine.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblProductionLine.ImeMode")));
			this.lblProductionLine.Location = ((System.Drawing.Point)(resources.GetObject("lblProductionLine.Location")));
			this.lblProductionLine.Name = "lblProductionLine";
			this.lblProductionLine.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblProductionLine.RightToLeft")));
			this.lblProductionLine.Size = ((System.Drawing.Size)(resources.GetObject("lblProductionLine.Size")));
			this.lblProductionLine.TabIndex = ((int)(resources.GetObject("lblProductionLine.TabIndex")));
			this.lblProductionLine.Text = resources.GetString("lblProductionLine.Text");
			this.lblProductionLine.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblProductionLine.TextAlign")));
			this.lblProductionLine.Visible = ((bool)(resources.GetObject("lblProductionLine.Visible")));
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
			this.dgrdData.BeforeColEdit += new C1.Win.C1TrueDBGrid.BeforeColEditEventHandler(this.dgrdData_BeforeColEdit);
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
			// lblWorkCenterList
			// 
			this.lblWorkCenterList.AccessibleDescription = resources.GetString("lblWorkCenterList.AccessibleDescription");
			this.lblWorkCenterList.AccessibleName = resources.GetString("lblWorkCenterList.AccessibleName");
			this.lblWorkCenterList.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblWorkCenterList.Anchor")));
			this.lblWorkCenterList.AutoSize = ((bool)(resources.GetObject("lblWorkCenterList.AutoSize")));
			this.lblWorkCenterList.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblWorkCenterList.Dock")));
			this.lblWorkCenterList.Enabled = ((bool)(resources.GetObject("lblWorkCenterList.Enabled")));
			this.lblWorkCenterList.Font = ((System.Drawing.Font)(resources.GetObject("lblWorkCenterList.Font")));
			this.lblWorkCenterList.ForeColor = System.Drawing.Color.Maroon;
			this.lblWorkCenterList.Image = ((System.Drawing.Image)(resources.GetObject("lblWorkCenterList.Image")));
			this.lblWorkCenterList.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblWorkCenterList.ImageAlign")));
			this.lblWorkCenterList.ImageIndex = ((int)(resources.GetObject("lblWorkCenterList.ImageIndex")));
			this.lblWorkCenterList.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblWorkCenterList.ImeMode")));
			this.lblWorkCenterList.Location = ((System.Drawing.Point)(resources.GetObject("lblWorkCenterList.Location")));
			this.lblWorkCenterList.Name = "lblWorkCenterList";
			this.lblWorkCenterList.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblWorkCenterList.RightToLeft")));
			this.lblWorkCenterList.Size = ((System.Drawing.Size)(resources.GetObject("lblWorkCenterList.Size")));
			this.lblWorkCenterList.TabIndex = ((int)(resources.GetObject("lblWorkCenterList.TabIndex")));
			this.lblWorkCenterList.Text = resources.GetString("lblWorkCenterList.Text");
			this.lblWorkCenterList.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblWorkCenterList.TextAlign")));
			this.lblWorkCenterList.Visible = ((bool)(resources.GetObject("lblWorkCenterList.Visible")));
			// 
			// txtDepartment
			// 
			this.txtDepartment.AccessibleDescription = resources.GetString("txtDepartment.AccessibleDescription");
			this.txtDepartment.AccessibleName = resources.GetString("txtDepartment.AccessibleName");
			this.txtDepartment.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("txtDepartment.Anchor")));
			this.txtDepartment.AutoSize = ((bool)(resources.GetObject("txtDepartment.AutoSize")));
			this.txtDepartment.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("txtDepartment.BackgroundImage")));
			this.txtDepartment.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("txtDepartment.Dock")));
			this.txtDepartment.Enabled = ((bool)(resources.GetObject("txtDepartment.Enabled")));
			this.txtDepartment.Font = ((System.Drawing.Font)(resources.GetObject("txtDepartment.Font")));
			this.txtDepartment.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("txtDepartment.ImeMode")));
			this.txtDepartment.Location = ((System.Drawing.Point)(resources.GetObject("txtDepartment.Location")));
			this.txtDepartment.MaxLength = ((int)(resources.GetObject("txtDepartment.MaxLength")));
			this.txtDepartment.Multiline = ((bool)(resources.GetObject("txtDepartment.Multiline")));
			this.txtDepartment.Name = "txtDepartment";
			this.txtDepartment.PasswordChar = ((char)(resources.GetObject("txtDepartment.PasswordChar")));
			this.txtDepartment.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("txtDepartment.RightToLeft")));
			this.txtDepartment.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("txtDepartment.ScrollBars")));
			this.txtDepartment.Size = ((System.Drawing.Size)(resources.GetObject("txtDepartment.Size")));
			this.txtDepartment.TabIndex = ((int)(resources.GetObject("txtDepartment.TabIndex")));
			this.txtDepartment.Text = resources.GetString("txtDepartment.Text");
			this.txtDepartment.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("txtDepartment.TextAlign")));
			this.txtDepartment.Visible = ((bool)(resources.GetObject("txtDepartment.Visible")));
			this.txtDepartment.WordWrap = ((bool)(resources.GetObject("txtDepartment.WordWrap")));
			this.txtDepartment.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtDepartment_KeyDown);
			this.txtDepartment.Validating += new System.ComponentModel.CancelEventHandler(this.txtDepartment_Validating);
			// 
			// btnDepartmentSearch
			// 
			this.btnDepartmentSearch.AccessibleDescription = resources.GetString("btnDepartmentSearch.AccessibleDescription");
			this.btnDepartmentSearch.AccessibleName = resources.GetString("btnDepartmentSearch.AccessibleName");
			this.btnDepartmentSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnDepartmentSearch.Anchor")));
			this.btnDepartmentSearch.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnDepartmentSearch.BackgroundImage")));
			this.btnDepartmentSearch.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnDepartmentSearch.Dock")));
			this.btnDepartmentSearch.Enabled = ((bool)(resources.GetObject("btnDepartmentSearch.Enabled")));
			this.btnDepartmentSearch.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnDepartmentSearch.FlatStyle")));
			this.btnDepartmentSearch.Font = ((System.Drawing.Font)(resources.GetObject("btnDepartmentSearch.Font")));
			this.btnDepartmentSearch.Image = ((System.Drawing.Image)(resources.GetObject("btnDepartmentSearch.Image")));
			this.btnDepartmentSearch.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnDepartmentSearch.ImageAlign")));
			this.btnDepartmentSearch.ImageIndex = ((int)(resources.GetObject("btnDepartmentSearch.ImageIndex")));
			this.btnDepartmentSearch.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnDepartmentSearch.ImeMode")));
			this.btnDepartmentSearch.Location = ((System.Drawing.Point)(resources.GetObject("btnDepartmentSearch.Location")));
			this.btnDepartmentSearch.Name = "btnDepartmentSearch";
			this.btnDepartmentSearch.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnDepartmentSearch.RightToLeft")));
			this.btnDepartmentSearch.Size = ((System.Drawing.Size)(resources.GetObject("btnDepartmentSearch.Size")));
			this.btnDepartmentSearch.TabIndex = ((int)(resources.GetObject("btnDepartmentSearch.TabIndex")));
			this.btnDepartmentSearch.Text = resources.GetString("btnDepartmentSearch.Text");
			this.btnDepartmentSearch.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnDepartmentSearch.TextAlign")));
			this.btnDepartmentSearch.Visible = ((bool)(resources.GetObject("btnDepartmentSearch.Visible")));
			this.btnDepartmentSearch.Click += new System.EventHandler(this.btnDepartmentSearch_Click);
			// 
			// lblDepartment
			// 
			this.lblDepartment.AccessibleDescription = resources.GetString("lblDepartment.AccessibleDescription");
			this.lblDepartment.AccessibleName = resources.GetString("lblDepartment.AccessibleName");
			this.lblDepartment.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblDepartment.Anchor")));
			this.lblDepartment.AutoSize = ((bool)(resources.GetObject("lblDepartment.AutoSize")));
			this.lblDepartment.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblDepartment.Dock")));
			this.lblDepartment.Enabled = ((bool)(resources.GetObject("lblDepartment.Enabled")));
			this.lblDepartment.Font = ((System.Drawing.Font)(resources.GetObject("lblDepartment.Font")));
			this.lblDepartment.ForeColor = System.Drawing.Color.Maroon;
			this.lblDepartment.Image = ((System.Drawing.Image)(resources.GetObject("lblDepartment.Image")));
			this.lblDepartment.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblDepartment.ImageAlign")));
			this.lblDepartment.ImageIndex = ((int)(resources.GetObject("lblDepartment.ImageIndex")));
			this.lblDepartment.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblDepartment.ImeMode")));
			this.lblDepartment.Location = ((System.Drawing.Point)(resources.GetObject("lblDepartment.Location")));
			this.lblDepartment.Name = "lblDepartment";
			this.lblDepartment.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblDepartment.RightToLeft")));
			this.lblDepartment.Size = ((System.Drawing.Size)(resources.GetObject("lblDepartment.Size")));
			this.lblDepartment.TabIndex = ((int)(resources.GetObject("lblDepartment.TabIndex")));
			this.lblDepartment.Text = resources.GetString("lblDepartment.Text");
			this.lblDepartment.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblDepartment.TextAlign")));
			this.lblDepartment.Visible = ((bool)(resources.GetObject("lblDepartment.Visible")));
			// 
			// txtDepartmentName
			// 
			this.txtDepartmentName.AccessibleDescription = resources.GetString("txtDepartmentName.AccessibleDescription");
			this.txtDepartmentName.AccessibleName = resources.GetString("txtDepartmentName.AccessibleName");
			this.txtDepartmentName.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("txtDepartmentName.Anchor")));
			this.txtDepartmentName.AutoSize = ((bool)(resources.GetObject("txtDepartmentName.AutoSize")));
			this.txtDepartmentName.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("txtDepartmentName.BackgroundImage")));
			this.txtDepartmentName.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("txtDepartmentName.Dock")));
			this.txtDepartmentName.Enabled = ((bool)(resources.GetObject("txtDepartmentName.Enabled")));
			this.txtDepartmentName.Font = ((System.Drawing.Font)(resources.GetObject("txtDepartmentName.Font")));
			this.txtDepartmentName.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("txtDepartmentName.ImeMode")));
			this.txtDepartmentName.Location = ((System.Drawing.Point)(resources.GetObject("txtDepartmentName.Location")));
			this.txtDepartmentName.MaxLength = ((int)(resources.GetObject("txtDepartmentName.MaxLength")));
			this.txtDepartmentName.Multiline = ((bool)(resources.GetObject("txtDepartmentName.Multiline")));
			this.txtDepartmentName.Name = "txtDepartmentName";
			this.txtDepartmentName.PasswordChar = ((char)(resources.GetObject("txtDepartmentName.PasswordChar")));
			this.txtDepartmentName.ReadOnly = true;
			this.txtDepartmentName.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("txtDepartmentName.RightToLeft")));
			this.txtDepartmentName.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("txtDepartmentName.ScrollBars")));
			this.txtDepartmentName.Size = ((System.Drawing.Size)(resources.GetObject("txtDepartmentName.Size")));
			this.txtDepartmentName.TabIndex = ((int)(resources.GetObject("txtDepartmentName.TabIndex")));
			this.txtDepartmentName.Text = resources.GetString("txtDepartmentName.Text");
			this.txtDepartmentName.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("txtDepartmentName.TextAlign")));
			this.txtDepartmentName.Visible = ((bool)(resources.GetObject("txtDepartmentName.Visible")));
			this.txtDepartmentName.WordWrap = ((bool)(resources.GetObject("txtDepartmentName.WordWrap")));
			// 
			// txtProductionLineName
			// 
			this.txtProductionLineName.AccessibleDescription = resources.GetString("txtProductionLineName.AccessibleDescription");
			this.txtProductionLineName.AccessibleName = resources.GetString("txtProductionLineName.AccessibleName");
			this.txtProductionLineName.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("txtProductionLineName.Anchor")));
			this.txtProductionLineName.AutoSize = ((bool)(resources.GetObject("txtProductionLineName.AutoSize")));
			this.txtProductionLineName.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("txtProductionLineName.BackgroundImage")));
			this.txtProductionLineName.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("txtProductionLineName.Dock")));
			this.txtProductionLineName.Enabled = ((bool)(resources.GetObject("txtProductionLineName.Enabled")));
			this.txtProductionLineName.Font = ((System.Drawing.Font)(resources.GetObject("txtProductionLineName.Font")));
			this.txtProductionLineName.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("txtProductionLineName.ImeMode")));
			this.txtProductionLineName.Location = ((System.Drawing.Point)(resources.GetObject("txtProductionLineName.Location")));
			this.txtProductionLineName.MaxLength = ((int)(resources.GetObject("txtProductionLineName.MaxLength")));
			this.txtProductionLineName.Multiline = ((bool)(resources.GetObject("txtProductionLineName.Multiline")));
			this.txtProductionLineName.Name = "txtProductionLineName";
			this.txtProductionLineName.PasswordChar = ((char)(resources.GetObject("txtProductionLineName.PasswordChar")));
			this.txtProductionLineName.ReadOnly = true;
			this.txtProductionLineName.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("txtProductionLineName.RightToLeft")));
			this.txtProductionLineName.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("txtProductionLineName.ScrollBars")));
			this.txtProductionLineName.Size = ((System.Drawing.Size)(resources.GetObject("txtProductionLineName.Size")));
			this.txtProductionLineName.TabIndex = ((int)(resources.GetObject("txtProductionLineName.TabIndex")));
			this.txtProductionLineName.Text = resources.GetString("txtProductionLineName.Text");
			this.txtProductionLineName.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("txtProductionLineName.TextAlign")));
			this.txtProductionLineName.Visible = ((bool)(resources.GetObject("txtProductionLineName.Visible")));
			this.txtProductionLineName.WordWrap = ((bool)(resources.GetObject("txtProductionLineName.WordWrap")));
			// 
			// ProductionLine
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
			this.Controls.Add(this.txtProductionLineName);
			this.Controls.Add(this.txtDepartmentName);
			this.Controls.Add(this.txtDepartment);
			this.Controls.Add(this.txtProductionLine);
			this.Controls.Add(this.btnDepartmentSearch);
			this.Controls.Add(this.lblDepartment);
			this.Controls.Add(this.dgrdData);
			this.Controls.Add(this.btnSave);
			this.Controls.Add(this.btnHelp);
			this.Controls.Add(this.btnClose);
			this.Controls.Add(this.btnDelete);
			this.Controls.Add(this.btnProductionLineSearch);
			this.Controls.Add(this.cboCCN);
			this.Controls.Add(this.lblCCN);
			this.Controls.Add(this.lblProductionLine);
			this.Controls.Add(this.lblWorkCenterList);
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
			this.Name = "ProductionLine";
			this.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("$this.RightToLeft")));
			this.StartPosition = ((System.Windows.Forms.FormStartPosition)(resources.GetObject("$this.StartPosition")));
			this.Text = resources.GetString("$this.Text");
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ProductionLine_KeyDown);
			this.Closing += new System.ComponentModel.CancelEventHandler(this.ProductionLine_Closing);
			this.Load += new System.EventHandler(this.ProductionLine_Load);
			((System.ComponentModel.ISupportInitialize)(this.cboCCN)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dgrdData)).EndInit();
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

            //Check Department
            if (FormControlComponents.CheckMandatory(txtDepartment))
            {
                PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtDepartment.Focus();
                return false;
            }

            //Check Production Line
            if (FormControlComponents.CheckMandatory(txtProductionLine))
            {
                PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtProductionLine.Focus();
                return false;
            }

            //Call update data to force grid update data
            dgrdData.UpdateData();

            //Check data in the grid
            if (dgrdData.RowCount == 0)
            {
                PCSMessageBox.Show(ErrorCode.MESSAGE_PLEASE_ENTER_DETAIL_INFOR, MessageBoxIcon.Exclamation);
                dgrdData.Row = 0;
                dgrdData.Col = dgrdData.Columns.IndexOf(dgrdData.Columns[MST_WorkCenterTable.CODE_FLD]);
                dgrdData.Focus();
                return false;
            }

            bool blnHasMainWC = false;
            //valid data in the
            for (int i = 0; i < dgrdData.RowCount; i++)
            {
                if (dgrdData[i, MST_WorkCenterTable.WORKCENTERID_FLD].Equals(DBNull.Value)
                || dgrdData[i, MST_WorkCenterTable.WORKCENTERID_FLD].ToString().Equals(string.Empty)
                  )
                {
                    PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Exclamation);
                    dgrdData.Row = i;
                    dgrdData.Col = dgrdData.Columns.IndexOf(dgrdData.Columns[MST_WorkCenterTable.CODE_FLD]);
                    dgrdData.Focus();
                    return false;
                }
                if (!dgrdData[i, MST_WorkCenterTable.ISMAIN_FLD].ToString().Equals(string.Empty)
                && !dgrdData[i, MST_WorkCenterTable.ISMAIN_FLD].Equals(DBNull.Value))
                {
                    if (bool.Parse(dgrdData[i, MST_WorkCenterTable.ISMAIN_FLD].ToString()))
                    {
                        blnHasMainWC = true;
                    }
                }
            }

            if (!blnHasMainWC)
            {
                PCSMessageBox.Show(ErrorCode.MESSAGE_PRODUCTION_LINE_MUST_HAS_MAIN_WORK_CENTER, MessageBoxIcon.Exclamation);
                dgrdData.Col = dgrdData.Columns.IndexOf(dgrdData.Columns[MST_WorkCenterTable.ISMAIN_FLD]);
                dgrdData.Row = 0;
                dgrdData.Focus();
                return false;
            }

            return true;
		}
		
		private void LockControl(bool pblnLockControl)
		{
			try
			{				
				dgrdData.AllowAddNew = !pblnLockControl;
				dgrdData.AllowDelete = !pblnLockControl;
				dgrdData.AllowUpdate = !pblnLockControl;
				
				//Set select buttons for grid				
				dgrdData.Splits[0].DisplayColumns[MST_WorkCenterTable.CODE_FLD].Button = !pblnLockControl;
				dgrdData.Splits[0].DisplayColumns[MST_WorkCenterTable.NAME_FLD].Button = !pblnLockControl;

				//Lock controls
				btnSave.Enabled = false;
				btnDelete.Enabled = !pblnLockControl && (dgrdData.RowCount > 0);	
			}
			catch
			{
			}
		}

		private void FormatDataGrid()
		{
			try
			{	
				//Restore layout
				FormControlComponents.RestoreGridLayout(dgrdData, dtbGridLayOut);
				//Change display format				
				dgrdData.Columns[MST_WorkCenterTable.ISMAIN_FLD].ValueItems.Presentation = C1.Win.C1TrueDBGrid.PresentationEnum.CheckBox;				
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message, ex);
			}
		}

		/// <summary>
		/// Load data into grid based on a specific Production Line
		/// </summary>
		/// <param name="pintProductionLineId"></param>
		private void LoadData(int pintProductionLineId)
		{
			const string METHOD_NAME = THIS + "LoadData()";
			try
			{
				if(pintProductionLineId > 0)
				{
					//get work center belong to production line
					dtbWorkCenter = boProductionLine.GetWorkCenterByProductionLine(pintProductionLineId);
				}
				else
				{
					dtbWorkCenter = BuildDetailTable();
				}

				//Bind to grid then format
				dgrdData.DataSource = dtbWorkCenter;				
				FormatDataGrid();
                
				LockControl((pintProductionLineId <= 0));				
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
				DataTable dtbDetail = new DataTable(MST_WorkCenterTable.TABLE_NAME);
				//Add columns
				dtbDetail.Columns.Add(MST_WorkCenterTable.WORKCENTERID_FLD, typeof(System.Int32));
				dtbDetail.Columns.Add(MST_WorkCenterTable.PRODUCTIONLINEID_FLD, typeof(System.Int32));				
				dtbDetail.Columns.Add(MST_WorkCenterTable.CODE_FLD, typeof(System.String));
				dtbDetail.Columns.Add(MST_WorkCenterTable.NAME_FLD, typeof(System.String));
				dtbDetail.Columns.Add(MST_WorkCenterTable.ISMAIN_FLD, typeof(System.Boolean));

				dtbDetail.Columns[MST_WorkCenterTable.ISMAIN_FLD].DefaultValue = false;

				return dtbDetail;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message, ex);
			}
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
			try
			{
				txtDepartment.Text = string.Empty;
				txtDepartment.Tag = ZERO_STRING;
				txtDepartmentName.Text = string.Empty;
				
				if(pblnClearProLine)
				{
					//Clear control's value
					txtProductionLine.Text = string.Empty;
					txtProductionLine.Tag = ZERO_STRING;
					txtProductionLineName.Text = string.Empty;

					//Disable button
					btnProductionLineSearch.Enabled = false;
					txtProductionLine.Enabled = false;
				}

				//Clear data grid
				LoadData(0);
				//
				txtDepartment.Focus();
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message, ex);
			}
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
			try
			{	
				Hashtable htbCondition = new Hashtable();
				DataRowView drvResult = null;
				bool blnResult = true;

				//Check Cycle no
				if (cboCCN.SelectedIndex < 0)
				{
					PCSMessageBox.Show(ErrorCode.MESSAGE_MATERIALRECEIPT_SELECT_CNN, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					cboCCN.Focus();				
					return false;
				}

				if (FormControlComponents.CheckMandatory(txtProductionLine))
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					txtProductionLine.Focus();				
					return false;
				}

				if(pstrColumnName == MST_WorkCenterTable.ISMAIN_FLD)
				{
					return true;
				}				

				//Check for each column
				switch (pstrColumnName)
				{
					case MST_WorkCenterTable.CODE_FLD:						
						//Clear hash table for new condition
						htbCondition.Clear();
						htbCondition.Add(MST_WorkCenterTable.CCNID_FLD, cboCCN.SelectedValue);
						htbCondition.Add(MST_WorkCenterTable.PRODUCTIONLINEID_FLD, DBNull.Value);
						
						drvResult = FormControlComponents.OpenSearchForm(MST_WorkCenterTable.TABLE_NAME, MST_WorkCenterTable.CODE_FLD, pstrColumValue, htbCondition, pblnAlwaysShow);

						// Call OpenSearchForm for Work Center selecting						
						if(drvResult != null)
						{
							if(!dgrdData[dgrdData.Row, MST_WorkCenterTable.WORKCENTERID_FLD].Equals(drvResult[MST_WorkCenterTable.WORKCENTERID_FLD]))
							{
								//Check duplicate key
								for(int i = 0; i < dgrdData.RowCount; i++)
								{
									if(i != dgrdData.Row && dgrdData[i, MST_WorkCenterTable.WORKCENTERID_FLD].Equals(drvResult[MST_WorkCenterTable.WORKCENTERID_FLD]))
									{
										PCSMessageBox.Show(ErrorCode.DUPLICATE_KEY, MessageBoxIcon.Exclamation);
										return false;
									}
								}

								//Enable Save button
								btnSave.Enabled = true;
								dgrdData[dgrdData.Row, MST_WorkCenterTable.ISMAIN_FLD] = false;
							}

							//Fill selected data
							if(pblnAlwaysShow)
							{
								dgrdData[dgrdData.Row, MST_WorkCenterTable.WORKCENTERID_FLD] = drvResult[MST_WorkCenterTable.WORKCENTERID_FLD];
								dgrdData[dgrdData.Row, MST_WorkCenterTable.CODE_FLD] = drvResult[MST_WorkCenterTable.CODE_FLD];
								dgrdData[dgrdData.Row, MST_WorkCenterTable.NAME_FLD] = drvResult[MST_WorkCenterTable.NAME_FLD];
							}
							else
							{
								dgrdData.Columns[MST_WorkCenterTable.WORKCENTERID_FLD].Value = drvResult[MST_WorkCenterTable.WORKCENTERID_FLD];
								dgrdData.Columns[MST_WorkCenterTable.CODE_FLD].Value = drvResult[MST_WorkCenterTable.CODE_FLD];
								dgrdData.Columns[MST_WorkCenterTable.NAME_FLD].Value = drvResult[MST_WorkCenterTable.NAME_FLD];
							}
						}
						else
						{							
							blnResult = pblnAlwaysShow;
						}
						break;
					
					case MST_WorkCenterTable.NAME_FLD:
						//Clear hash table for new condition
						htbCondition.Clear();
						htbCondition.Add(MST_WorkCenterTable.CCNID_FLD, cboCCN.SelectedValue);
						htbCondition.Add(MST_WorkCenterTable.PRODUCTIONLINEID_FLD, DBNull.Value);						
						
						// Call OpenSearchForm for Work Center selecting						
						drvResult = FormControlComponents.OpenSearchForm(MST_WorkCenterTable.TABLE_NAME, MST_WorkCenterTable.NAME_FLD, pstrColumValue, htbCondition, pblnAlwaysShow);						

						if(drvResult != null)
						{							
							if(!dgrdData[dgrdData.Row, MST_WorkCenterTable.WORKCENTERID_FLD].Equals(drvResult[MST_WorkCenterTable.WORKCENTERID_FLD]))
							{
								//Check duplicate key
								for(int i = 0; i < dgrdData.RowCount && i != dgrdData.Row; i++)
								{
									if(dgrdData[i, MST_WorkCenterTable.WORKCENTERID_FLD].Equals(drvResult[MST_WorkCenterTable.WORKCENTERID_FLD]))
									{
										PCSMessageBox.Show(ErrorCode.DUPLICATE_KEY, MessageBoxIcon.Exclamation);
										return false;
									}
								}
								//Enable Save button
								btnSave.Enabled = true;
								dgrdData[dgrdData.Row, MST_WorkCenterTable.ISMAIN_FLD] = false;
							}

							//Fill selected data
							if(pblnAlwaysShow)
							{
								dgrdData[dgrdData.Row, MST_WorkCenterTable.WORKCENTERID_FLD] = drvResult[MST_WorkCenterTable.WORKCENTERID_FLD];
								dgrdData[dgrdData.Row, MST_WorkCenterTable.CODE_FLD] = drvResult[MST_WorkCenterTable.CODE_FLD];
								dgrdData[dgrdData.Row, MST_WorkCenterTable.NAME_FLD] = drvResult[MST_WorkCenterTable.NAME_FLD];
							}
							else
							{
								dgrdData.Columns[MST_WorkCenterTable.WORKCENTERID_FLD].Value = drvResult[MST_WorkCenterTable.WORKCENTERID_FLD];
								dgrdData.Columns[MST_WorkCenterTable.CODE_FLD].Value = drvResult[MST_WorkCenterTable.CODE_FLD];
								dgrdData.Columns[MST_WorkCenterTable.NAME_FLD].Value = drvResult[MST_WorkCenterTable.NAME_FLD];
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

		/// <summary>
		/// Fill related data on controls when select Department
		/// </summary>
		/// <param name="pblnAlwaysShowDialog"></param>
		private bool SelectDepartment(string pstrMethodName, bool pblnAlwaysShowDialog)
		{
			try
			{
				Hashtable htbCriteria = new Hashtable();
				DataRowView drwResult = null;			
				
				if(cboCCN.SelectedValue != null)
				{
					htbCriteria.Add(MST_DepartmentTable.CCNID_FLD, cboCCN.SelectedValue);
				}
				else
				{
					htbCriteria.Add(MST_DepartmentTable.CCNID_FLD, 0);
				}

				//Call OpenSearchForm for selecting MPS planning cycle
				drwResult = FormControlComponents.OpenSearchForm(MST_DepartmentTable.TABLE_NAME, MST_DepartmentTable.CODE_FLD, txtDepartment.Text.Trim(), htbCriteria, pblnAlwaysShowDialog);
				
				// If has Master location matched searching condition, fill values to form's controls
				if (drwResult != null)
				{
					//Clear production line if select new department
					if(!txtDepartment.Tag.Equals(drwResult[MST_DepartmentTable.DEPARTMENTID_FLD]))
					{
						txtProductionLine.Tag = ZERO_STRING;
						txtProductionLine.Text = string.Empty;						
						txtProductionLineName.Text = string.Empty;
						//Clear data grid
						LoadData(0);
					}

					//Check if data was changed then reassign
					txtDepartment.Text = drwResult[MST_DepartmentTable.CODE_FLD].ToString();
					txtDepartmentName.Text = drwResult[MST_DepartmentTable.NAME_FLD].ToString();
					txtDepartment.Tag = drwResult[MST_DepartmentTable.DEPARTMENTID_FLD];

					//Turn on
					txtProductionLine.Enabled = true;
					btnProductionLineSearch.Enabled = true;

					//Reset modify status
					txtDepartment.Modified = false;
				}
				else
				{
					if(!pblnAlwaysShowDialog)
					{
						txtProductionLine.Tag = ZERO_STRING;
						txtProductionLine.Text = string.Empty;						
						txtProductionLineName.Text = string.Empty;
						
						//Turn on
						txtProductionLine.Enabled = false;
						btnProductionLineSearch.Enabled = false;

						//Turn on
						txtDepartment.Tag = ZERO_STRING;
						txtDepartment.Text = string.Empty;
						txtDepartment.Focus();						
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
		/// Fill related data on controls when select Production Line
		/// </summary>
		/// <param name="pblnAlwaysShowDialog"></param>
		private bool SelectProductionLine(string pstrMethodName, bool pblnAlwaysShowDialog)
		{
			try
			{
				Hashtable htbCriteria = new Hashtable();
				DataRowView drwResult = null;			
				
				if(!txtDepartment.Tag.ToString().Equals(ZERO_STRING))
				{
					htbCriteria.Add(PRO_ProductionLineTable.DEPARTMENTID_FLD, txtDepartment.Tag);
				}
				else
				{
					htbCriteria.Add(PRO_ProductionLineTable.DEPARTMENTID_FLD, 0);
				}

				//Call OpenSearchForm for selecting MPS planning cycle
				drwResult = FormControlComponents.OpenSearchForm(PRO_ProductionLineTable.TABLE_NAME, PRO_ProductionLineTable.CODE_FLD, txtProductionLine.Text, htbCriteria, pblnAlwaysShowDialog);
				
				// If has Master location matched searching condition, fill values to form's controls
				if (drwResult != null)
				{
					if(!txtProductionLine.Tag.Equals(drwResult[PRO_ProductionLineTable.PRODUCTIONLINEID_FLD]))					
					{						
						LoadData(int.Parse(drwResult[PRO_ProductionLineTable.PRODUCTIONLINEID_FLD].ToString()));
					}

					//Check if data was changed then reassign
					txtProductionLine.Text = drwResult[PRO_ProductionLineTable.CODE_FLD].ToString();
					txtProductionLineName.Text = drwResult[PRO_ProductionLineTable.NAME_FLD].ToString();
					txtProductionLine.Tag = drwResult[PRO_ProductionLineTable.PRODUCTIONLINEID_FLD];
										
					//Reset modify status
					txtProductionLine.Modified = false;
				}
				else
				{
					if(!pblnAlwaysShowDialog)
					{
						txtProductionLine.Tag = ZERO_STRING;
						txtProductionLineName.Text = string.Empty;						
						txtProductionLine.Focus();

						//Clear data grid
						LoadData(0);
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

		#endregion Methods		
		
		#region Event Prcessing
		

		private void ProductionLine_Load(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".OutsideProcessing_Load()";
			try
			{
				this.Name = THIS;
				
				boProductionLine = new ProductionLineBO();

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
				LockControl(true);
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

		private void btnProductionLineSearch_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnSave_Click()";

			try
			{
				SelectProductionLine(METHOD_NAME, true);
			}
			catch (PCSException ex)
			{
				blnDataIsValid = false;
				if (ex.mCode == ErrorCode.DUPLICATE_KEY)
				{
					PCSMessageBox.Show(ErrorCode.MESSAGE_OUTPROCESSING_DUPLICATE_TRANSNO, MessageBoxIcon.Exclamation);
					txtProductionLine.Focus();
				}
				else
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

		private void btnSave_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnSave_Click()";

			try
			{
				// check data, if data is invalid then exit immediately
				blnDataIsValid = ValidateData() && !dgrdData.EditActive;;
				if(!blnDataIsValid) return;

				//Variable to store list of work center
				Hashtable htbWCList = new Hashtable();
				
				// Get Production Line
				int intProductionLine = int.Parse(txtProductionLine.Tag.ToString());
				
				//get Main Work Center info
				int intMainWorkCenter = 0;
				for(int i = 0; i < dgrdData.RowCount; i++)
				{
					htbWCList.Add(i, dgrdData[i, MST_WorkCenterTable.WORKCENTERID_FLD]);
					if(dgrdData[i, MST_WorkCenterTable.ISMAIN_FLD].ToString() != string.Empty)
					{
						if(bool.Parse(dgrdData[i, MST_WorkCenterTable.ISMAIN_FLD].ToString()))
						{
							intMainWorkCenter = int.Parse(dgrdData[i, MST_WorkCenterTable.WORKCENTERID_FLD].ToString());
						}
					}
				}

				//Update production line of work center				
				boProductionLine.SetProductionLine4WorkOrder(htbWCList, intProductionLine);
				
				//Update main work center
				MST_WorkCenterDS dsWorkCenter = new MST_WorkCenterDS();
				MST_WorkCenterVO voMainWorkCenter = (MST_WorkCenterVO)dsWorkCenter.GetObjectVO(intMainWorkCenter);
				if(voMainWorkCenter != null)
				{
					voMainWorkCenter.IsMain = true;
					dsWorkCenter.Update(voMainWorkCenter);
				}

				//Reload detail grid				
				LoadData(intProductionLine);				
				
				//Show alert message
				PCSMessageBox.Show(ErrorCode.MESSAGE_AFTER_SAVE_DATA, MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
			catch (PCSException ex)
			{
				blnDataIsValid = false;
				if (ex.mCode == ErrorCode.DUPLICATE_KEY)
				{
					PCSMessageBox.Show(ErrorCode.MESSAGE_OUTPROCESSING_DUPLICATE_TRANSNO, MessageBoxIcon.Exclamation);
					txtProductionLine.Focus();
				}
				else
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
		}

		private void btnDelete_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnSave_Click()";

			try
			{
				//exit if in grid is in edit mode
				if(dgrdData.EditActive) return;
				
				if (PCSMessageBox.Show(ErrorCode.MESSAGE_DELETE_RECORD, MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
				{
					return;
				}

				Hashtable htbWCList = new Hashtable();				
				int intProductionLine = int.Parse(txtProductionLine.Tag.ToString());
				
				//Update production line of work center
				boProductionLine.SetProductionLine4WorkOrder(htbWCList, intProductionLine);				

				LoadData(intProductionLine);				
			}
			catch (PCSException ex)
			{
				blnDataIsValid = false;
				if (ex.mCode == ErrorCode.DUPLICATE_KEY)
				{
					PCSMessageBox.Show(ErrorCode.MESSAGE_OUTPROCESSING_DUPLICATE_TRANSNO, MessageBoxIcon.Exclamation);
					txtProductionLine.Focus();
				}
				else
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

		private void btnHelp_Click(object sender, System.EventArgs e)
		{
		
		}

		private void btnClose_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void ProductionLine_Closing(object sender, CancelEventArgs e)
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
				blnGridDataIsValid = false;
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
						blnGridDataIsValid = false;
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
					dgrdData.Columns[MST_WorkCenterTable.WORKCENTERID_FLD].Value = DBNull.Value;
					dgrdData.Columns[MST_WorkCenterTable.CODE_FLD].Value = string.Empty;
					dgrdData.Columns[MST_WorkCenterTable.NAME_FLD].Value = string.Empty;
					dgrdData.Columns[MST_WorkCenterTable.ISMAIN_FLD].Value = false;

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
		
		private void txtProductionLine_Validating(object sender, CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtProductionLine_Validating()";
			try
			{
				//Exit immediately if empty or in default mode
				if(txtProductionLine.Text.Length == 0)
				{	
					txtProductionLine.Tag = ZERO_STRING;
					txtProductionLineName.Text = string.Empty;					

					//Clear data grid
					LoadData(0);
					return;
				}
				else if(!txtProductionLine.Modified)
				{
					return;
				}
				
				e.Cancel = !SelectProductionLine(METHOD_NAME, false);
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
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}		
		
		private void txtProductionLine_KeyDown(object sender, KeyEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtProductionLine_KeyDown()";

			try
			{
				if ((e.KeyCode == Keys.F4) && (btnProductionLineSearch.Enabled))
				{
					SelectProductionLine(METHOD_NAME, true);
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
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}

		private void txtDepartment_KeyDown(object sender, KeyEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtDepartment_KeyDown()";

			try
			{
				if ((e.KeyCode == Keys.F4) && (btnDepartmentSearch.Enabled))
				{
					SelectDepartment(METHOD_NAME, true);
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
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}	
		}

		private void txtDepartment_Validating(object sender, CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtDepartment_Validating()";
			try
			{
				if(txtDepartment.Text.Length == 0)
				{					
					txtDepartmentName.Text = string.Empty;
					txtDepartment.Tag = ZERO_STRING;
					
					txtProductionLine.Text = string.Empty;
					txtProductionLineName.Text = string.Empty;
					txtProductionLine.Tag = ZERO_STRING;

					//Clear data grid
					LoadData(0);

					txtProductionLine.Enabled = false;
					btnProductionLineSearch.Enabled = false;
					return;
				}
				else if(!txtDepartment.Modified)
				{
					return;
				}
				
				e.Cancel = !SelectDepartment(METHOD_NAME, false);
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
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}
		
		private void btnDepartmentSearch_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnDepartmentSearch_Click()";

			try
			{
				SelectDepartment(METHOD_NAME, true);
			}
			catch (PCSException ex)
			{
				blnDataIsValid = false;
				if (ex.mCode == ErrorCode.DUPLICATE_KEY)
				{
					PCSMessageBox.Show(ErrorCode.MESSAGE_OUTPROCESSING_DUPLICATE_TRANSNO, MessageBoxIcon.Exclamation);
					txtProductionLine.Focus();
				}
				else
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
		
		private void dgrdData_AfterColUpdate(object sender, C1.Win.C1TrueDBGrid.ColEventArgs e)
		{
			const string METHOD_NAME = THIS + ".dgrdData_AfterColUpdate()";

			try
			{				
				
				if( e.ColIndex != dgrdData.Columns.IndexOf(dgrdData.Columns[MST_WorkCenterTable.ISMAIN_FLD]))
				{
					return;
				}
				
				//Force main workcenter must be unique

				//Check if IsMain column is NULL
				if(dgrdData[intCurrentRow, MST_WorkCenterTable.ISMAIN_FLD].ToString().Equals(string.Empty)
					|| dgrdData[intCurrentRow, MST_WorkCenterTable.ISMAIN_FLD].Equals(DBNull.Value)
				)
				{
					return;
				}

				//update unique main work center
				if(bool.Parse(dgrdData[intCurrentRow, MST_WorkCenterTable.ISMAIN_FLD].ToString()))
				{
					for(int i = 0; i < dgrdData.RowCount; i++)
					{							
						dgrdData[i, MST_WorkCenterTable.ISMAIN_FLD] = (i == intCurrentRow);
					}
				}

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
		
		private void dgrdData_BeforeColEdit(object sender, C1.Win.C1TrueDBGrid.BeforeColEditEventArgs e)
		{
			intCurrentRow = dgrdData.Row;
		}
		
		private void dgrdData_AfterDelete(object sender, EventArgs e)
		{
			btnSave.Enabled = true;
		}
		
		private void ProductionLine_KeyDown(object sender, KeyEventArgs e)
		{
			const string METHOD_NAME = THIS + ".ProductionLine_KeyDown()";

			try
			{
				//if column's value then exit immediately				
				switch (e.KeyCode)
				{
					case Keys.F12:
						dgrdData.Col = dgrdData.Columns.IndexOf(dgrdData.Columns[MST_WorkCenterTable.CODE_FLD]);
						dgrdData.Row = dgrdData.RowCount;
						dgrdData.Focus();
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
