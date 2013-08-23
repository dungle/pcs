using System;
using System.Data;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Printing;
using System.Data.OleDb;
using System.Collections.Specialized;
using System.IO;
using C1.Win.C1Preview;

//Using PCS's namespaces
using C1.C1Report;
using PCSComUtils.Common;
using PCSComUtils.Common.BO;
using PCSComUtils.MasterSetup.BO;
using PCSComUtils.PCSExc;
using PCSUtils.Log;
using PCSUtils.Utils;
using PCSUtils.Framework.ReportFrame;
using PCSComUtils.Framework.ReportFrame.BO;
using C1PrintPreviewDialog = PCSUtils.Framework.ReportFrame.C1PrintPreviewDialog;

namespace PCSMaterials.Inventory
{
	

	/// <summary>
	/// Summary description for InventoryStatus.
	/// </summary>
	public class InOutStockReport : Form
	{        
		private enum EnumLocationType
		{
			MaterLocation = 0,
			Location = 1,
			Bin = 2
		}	

		#region Declaration
		
		#region System Generated

		private C1.Win.C1List.C1Combo cboCCN;
		private System.Windows.Forms.Label lblCCN;
		private System.Windows.Forms.Label lblMasLoc;
		private System.Windows.Forms.TextBox txtMasLoc;
		private System.Windows.Forms.TextBox txtLocation;
		private System.Windows.Forms.TextBox txtCategory;
		private System.Windows.Forms.Label lblLocation;
		private System.Windows.Forms.Label lblCategory;
		private System.Windows.Forms.Button btnMasLoc;
		private System.Windows.Forms.Button btnCategory;
		private System.Windows.Forms.Button btnHelp;
		private System.Windows.Forms.Button btnClose;
		private System.Windows.Forms.Button btnPrint;
		private System.Windows.Forms.Label lblFromDate;
		private C1.Win.C1Input.C1DateEdit dtmFromDate;
		private System.Windows.Forms.Label lblToDate;
		private C1.Win.C1Input.C1DateEdit dtmToDate;
		private System.Windows.Forms.Label lblBin;
		private System.Windows.Forms.Label lblSource;
		private System.Windows.Forms.TextBox txtBin;
		private System.Windows.Forms.TextBox txtSource;
		private System.Windows.Forms.Button btnBin;
		private System.Windows.Forms.Button btnSource;
		private C1.Win.C1TrueDBGrid.C1TrueDBGrid dgrdData;		
		private System.ComponentModel.Container components = null;

		#endregion System Generated
		
		#region Constants 
		
		private const string THIS = "PCSMaterials.Inventory.InOutStockReport";
		private const string SELECT_FLD = "Select_Fld";
		#endregion Constants 

		#region Variables		
		
		UtilsBO boUtil = new UtilsBO();
		private System.Windows.Forms.CheckBox chkSelectAll;
		private System.Windows.Forms.Label lblAllLocation;
		private System.Windows.Forms.TextBox txtBinType;
		private System.Windows.Forms.Button btnBinType;
		private System.Windows.Forms.Label lblModel;
		private System.Windows.Forms.Button btnModel;
		private System.Windows.Forms.TextBox txtModel;
		private System.Windows.Forms.Label lblBinType;
		private System.Windows.Forms.TextBox txtPartName;
		private System.Windows.Forms.TextBox txtPartNumber;
		private System.Windows.Forms.TextBox txtRevision;
		private System.Windows.Forms.Button btnPartNumberSearch;
		private System.Windows.Forms.Button btnPartNameSearch;
		private System.Windows.Forms.Label lblPartNumer;
		private System.Windows.Forms.Label lblPartName;
		private DataTable dtbGridLayOut;

		#endregion
		
		#endregion Declaration

		#region Constructor, Destructor
		public InOutStockReport()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(InOutStockReport));
			this.cboCCN = new C1.Win.C1List.C1Combo();
			this.lblCCN = new System.Windows.Forms.Label();
			this.txtMasLoc = new System.Windows.Forms.TextBox();
			this.txtLocation = new System.Windows.Forms.TextBox();
			this.txtCategory = new System.Windows.Forms.TextBox();
			this.lblMasLoc = new System.Windows.Forms.Label();
			this.lblLocation = new System.Windows.Forms.Label();
			this.lblCategory = new System.Windows.Forms.Label();
			this.btnMasLoc = new System.Windows.Forms.Button();
			this.btnCategory = new System.Windows.Forms.Button();
			this.btnClose = new System.Windows.Forms.Button();
			this.btnHelp = new System.Windows.Forms.Button();
			this.btnPrint = new System.Windows.Forms.Button();
			this.dtmFromDate = new C1.Win.C1Input.C1DateEdit();
			this.lblFromDate = new System.Windows.Forms.Label();
			this.dtmToDate = new C1.Win.C1Input.C1DateEdit();
			this.lblToDate = new System.Windows.Forms.Label();
			this.btnBin = new System.Windows.Forms.Button();
			this.txtBin = new System.Windows.Forms.TextBox();
			this.lblBin = new System.Windows.Forms.Label();
			this.btnSource = new System.Windows.Forms.Button();
			this.txtSource = new System.Windows.Forms.TextBox();
			this.lblSource = new System.Windows.Forms.Label();
			this.dgrdData = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
			this.chkSelectAll = new System.Windows.Forms.CheckBox();
			this.lblAllLocation = new System.Windows.Forms.Label();
			this.txtBinType = new System.Windows.Forms.TextBox();
			this.btnBinType = new System.Windows.Forms.Button();
			this.lblBinType = new System.Windows.Forms.Label();
			this.btnModel = new System.Windows.Forms.Button();
			this.txtModel = new System.Windows.Forms.TextBox();
			this.lblModel = new System.Windows.Forms.Label();
			this.txtPartName = new System.Windows.Forms.TextBox();
			this.txtPartNumber = new System.Windows.Forms.TextBox();
			this.txtRevision = new System.Windows.Forms.TextBox();
			this.btnPartNumberSearch = new System.Windows.Forms.Button();
			this.btnPartNameSearch = new System.Windows.Forms.Button();
			this.lblPartNumer = new System.Windows.Forms.Label();
			this.lblPartName = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.cboCCN)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dtmFromDate)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dtmToDate)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dgrdData)).BeginInit();
			this.SuspendLayout();
			// 
			// cboCCN
			// 
			this.cboCCN.AddItemSeparator = ';';
			this.cboCCN.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cboCCN.Caption = "";
			this.cboCCN.CaptionHeight = 17;
			this.cboCCN.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
			this.cboCCN.ColumnCaptionHeight = 17;
			this.cboCCN.ColumnFooterHeight = 17;
			this.cboCCN.ComboStyle = C1.Win.C1List.ComboStyleEnum.DropdownList;
			this.cboCCN.ContentHeight = 15;
			this.cboCCN.DeadAreaBackColor = System.Drawing.Color.Empty;
			this.cboCCN.EditorBackColor = System.Drawing.SystemColors.Window;
			this.cboCCN.EditorFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.cboCCN.EditorForeColor = System.Drawing.SystemColors.WindowText;
			this.cboCCN.EditorHeight = 15;
			this.cboCCN.FlatStyle = C1.Win.C1List.FlatModeEnum.System;
			this.cboCCN.GapHeight = 2;
			this.cboCCN.ItemHeight = 15;
			this.cboCCN.Location = new System.Drawing.Point(618, 7);
			this.cboCCN.MatchEntryTimeout = ((long)(2000));
			this.cboCCN.MaxDropDownItems = ((short)(5));
			this.cboCCN.MaxLength = 32767;
			this.cboCCN.MouseCursor = System.Windows.Forms.Cursors.Default;
			this.cboCCN.Name = "cboCCN";
			this.cboCCN.RowDivider.Color = System.Drawing.Color.DarkGray;
			this.cboCCN.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None;
			this.cboCCN.RowSubDividerColor = System.Drawing.Color.DarkGray;
			this.cboCCN.Size = new System.Drawing.Size(86, 21);
			this.cboCCN.TabIndex = 1;
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
			this.lblCCN.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.lblCCN.ForeColor = System.Drawing.Color.Maroon;
			this.lblCCN.Location = new System.Drawing.Point(586, 6);
			this.lblCCN.Name = "lblCCN";
			this.lblCCN.Size = new System.Drawing.Size(32, 20);
			this.lblCCN.TabIndex = 0;
			this.lblCCN.Text = "CCN";
			this.lblCCN.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// txtMasLoc
			// 
			this.txtMasLoc.Location = new System.Drawing.Point(100, 55);
			this.txtMasLoc.Name = "txtMasLoc";
			this.txtMasLoc.Size = new System.Drawing.Size(96, 20);
			this.txtMasLoc.TabIndex = 7;
			this.txtMasLoc.Text = "";
			this.txtMasLoc.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtMasLoc_KeyDown);
			this.txtMasLoc.Validating += new System.ComponentModel.CancelEventHandler(this.txtMasLoc_Validating);
			// 
			// txtLocation
			// 
			this.txtLocation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.txtLocation.Location = new System.Drawing.Point(100, 79);
			this.txtLocation.Name = "txtLocation";
			this.txtLocation.ReadOnly = true;
			this.txtLocation.Size = new System.Drawing.Size(602, 20);
			this.txtLocation.TabIndex = 25;
			this.txtLocation.Text = "";
			// 
			// txtCategory
			// 
			this.txtCategory.Location = new System.Drawing.Point(282, 55);
			this.txtCategory.Name = "txtCategory";
			this.txtCategory.Size = new System.Drawing.Size(96, 20);
			this.txtCategory.TabIndex = 16;
			this.txtCategory.Text = "";
			this.txtCategory.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCategory_KeyDown);
			this.txtCategory.Validating += new System.ComponentModel.CancelEventHandler(this.txtCategory_Validating);
			// 
			// lblMasLoc
			// 
			this.lblMasLoc.ForeColor = System.Drawing.Color.Maroon;
			this.lblMasLoc.Location = new System.Drawing.Point(6, 55);
			this.lblMasLoc.Name = "lblMasLoc";
			this.lblMasLoc.Size = new System.Drawing.Size(92, 20);
			this.lblMasLoc.TabIndex = 6;
			this.lblMasLoc.Text = "Master Location";
			this.lblMasLoc.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblLocation
			// 
			this.lblLocation.ForeColor = System.Drawing.Color.Maroon;
			this.lblLocation.Location = new System.Drawing.Point(6, 79);
			this.lblLocation.Name = "lblLocation";
			this.lblLocation.Size = new System.Drawing.Size(90, 20);
			this.lblLocation.TabIndex = 24;
			this.lblLocation.Text = "Location";
			this.lblLocation.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblCategory
			// 
			this.lblCategory.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblCategory.Location = new System.Drawing.Point(232, 55);
			this.lblCategory.Name = "lblCategory";
			this.lblCategory.Size = new System.Drawing.Size(58, 20);
			this.lblCategory.TabIndex = 15;
			this.lblCategory.Text = "Category";
			this.lblCategory.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// btnMasLoc
			// 
			this.btnMasLoc.Location = new System.Drawing.Point(197, 55);
			this.btnMasLoc.Name = "btnMasLoc";
			this.btnMasLoc.Size = new System.Drawing.Size(22, 20);
			this.btnMasLoc.TabIndex = 8;
			this.btnMasLoc.Text = "...";
			this.btnMasLoc.Click += new System.EventHandler(this.btnMasLoc_Click);
			// 
			// btnCategory
			// 
			this.btnCategory.Location = new System.Drawing.Point(380, 55);
			this.btnCategory.Name = "btnCategory";
			this.btnCategory.Size = new System.Drawing.Size(22, 20);
			this.btnCategory.TabIndex = 17;
			this.btnCategory.Text = "...";
			this.btnCategory.Click += new System.EventHandler(this.btnCategory_Click);
			// 
			// btnClose
			// 
			this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnClose.Location = new System.Drawing.Point(644, 436);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(60, 23);
			this.btnClose.TabIndex = 37;
			this.btnClose.Text = "&Close";
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			// 
			// btnHelp
			// 
			this.btnHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnHelp.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnHelp.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnHelp.Location = new System.Drawing.Point(583, 436);
			this.btnHelp.Name = "btnHelp";
			this.btnHelp.Size = new System.Drawing.Size(60, 23);
			this.btnHelp.TabIndex = 36;
			this.btnHelp.Text = "&Help";
			// 
			// btnPrint
			// 
			this.btnPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnPrint.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnPrint.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnPrint.Location = new System.Drawing.Point(8, 436);
			this.btnPrint.Name = "btnPrint";
			this.btnPrint.Size = new System.Drawing.Size(60, 23);
			this.btnPrint.TabIndex = 34;
			this.btnPrint.Text = "&Execute";
			this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
			// 
			// dtmFromDate
			// 
			// 
			// dtmFromDate.Calendar
			// 
			this.dtmFromDate.Calendar.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.dtmFromDate.CustomFormat = "dd-MM-yyyy HH:mm";
			this.dtmFromDate.EmptyAsNull = true;
			this.dtmFromDate.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
			this.dtmFromDate.Location = new System.Drawing.Point(100, 7);
			this.dtmFromDate.Name = "dtmFromDate";
			this.dtmFromDate.Size = new System.Drawing.Size(120, 20);
			this.dtmFromDate.TabIndex = 3;
			this.dtmFromDate.Tag = null;
			this.dtmFromDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.dtmFromDate.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.DropDown;
			this.dtmFromDate.Validating += new System.ComponentModel.CancelEventHandler(this.dtmFromDate_Validating);
			// 
			// lblFromDate
			// 
			this.lblFromDate.ForeColor = System.Drawing.Color.Maroon;
			this.lblFromDate.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lblFromDate.Location = new System.Drawing.Point(6, 7);
			this.lblFromDate.Name = "lblFromDate";
			this.lblFromDate.Size = new System.Drawing.Size(84, 20);
			this.lblFromDate.TabIndex = 2;
			this.lblFromDate.Text = "From Date";
			this.lblFromDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// dtmToDate
			// 
			// 
			// dtmToDate.Calendar
			// 
			this.dtmToDate.Calendar.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.dtmToDate.CustomFormat = "dd-MM-yyyy HH:mm";
			this.dtmToDate.EmptyAsNull = true;
			this.dtmToDate.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
			this.dtmToDate.Location = new System.Drawing.Point(100, 31);
			this.dtmToDate.Name = "dtmToDate";
			this.dtmToDate.Size = new System.Drawing.Size(120, 20);
			this.dtmToDate.TabIndex = 5;
			this.dtmToDate.Tag = null;
			this.dtmToDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.dtmToDate.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.DropDown;
			this.dtmToDate.Validating += new System.ComponentModel.CancelEventHandler(this.dtmToDate_Validating);
			// 
			// lblToDate
			// 
			this.lblToDate.ForeColor = System.Drawing.Color.Maroon;
			this.lblToDate.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lblToDate.Location = new System.Drawing.Point(6, 31);
			this.lblToDate.Name = "lblToDate";
			this.lblToDate.Size = new System.Drawing.Size(84, 20);
			this.lblToDate.TabIndex = 4;
			this.lblToDate.Text = "To Date";
			this.lblToDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// btnBin
			// 
			this.btnBin.Location = new System.Drawing.Point(380, 31);
			this.btnBin.Name = "btnBin";
			this.btnBin.Size = new System.Drawing.Size(22, 20);
			this.btnBin.TabIndex = 14;
			this.btnBin.Text = "...";
			this.btnBin.Click += new System.EventHandler(this.btnBin_Click);
			// 
			// txtBin
			// 
			this.txtBin.Location = new System.Drawing.Point(282, 31);
			this.txtBin.Name = "txtBin";
			this.txtBin.Size = new System.Drawing.Size(96, 20);
			this.txtBin.TabIndex = 13;
			this.txtBin.Text = "";
			this.txtBin.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBin_KeyDown);
			this.txtBin.Validating += new System.ComponentModel.CancelEventHandler(this.txtBin_Validating);
			// 
			// lblBin
			// 
			this.lblBin.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblBin.Location = new System.Drawing.Point(232, 31);
			this.lblBin.Name = "lblBin";
			this.lblBin.Size = new System.Drawing.Size(58, 20);
			this.lblBin.TabIndex = 12;
			this.lblBin.Text = "Bin";
			this.lblBin.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// btnSource
			// 
			this.btnSource.Location = new System.Drawing.Point(558, 31);
			this.btnSource.Name = "btnSource";
			this.btnSource.Size = new System.Drawing.Size(22, 20);
			this.btnSource.TabIndex = 23;
			this.btnSource.Text = "...";
			this.btnSource.Click += new System.EventHandler(this.btnSource_Click);
			// 
			// txtSource
			// 
			this.txtSource.Location = new System.Drawing.Point(460, 31);
			this.txtSource.Name = "txtSource";
			this.txtSource.Size = new System.Drawing.Size(96, 20);
			this.txtSource.TabIndex = 22;
			this.txtSource.Text = "";
			this.txtSource.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSource_KeyDown);
			this.txtSource.Validating += new System.ComponentModel.CancelEventHandler(this.txtSource_Validating);
			// 
			// lblSource
			// 
			this.lblSource.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblSource.Location = new System.Drawing.Point(410, 31);
			this.lblSource.Name = "lblSource";
			this.lblSource.Size = new System.Drawing.Size(48, 20);
			this.lblSource.TabIndex = 21;
			this.lblSource.Text = "Source";
			this.lblSource.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// dgrdData
			// 
			this.dgrdData.AllowAddNew = true;
			this.dgrdData.AllowDelete = true;
			this.dgrdData.AllowFilter = false;
			this.dgrdData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.dgrdData.CaptionHeight = 17;
			this.dgrdData.CollapseColor = System.Drawing.Color.Black;
			this.dgrdData.ExpandColor = System.Drawing.Color.Black;
			this.dgrdData.GroupByCaption = "Drag a column header here to group by that column";
			this.dgrdData.Images.Add(((System.Drawing.Image)(resources.GetObject("resource"))));
			this.dgrdData.Location = new System.Drawing.Point(8, 148);
			this.dgrdData.MarqueeStyle = C1.Win.C1TrueDBGrid.MarqueeEnum.DottedCellBorder;
			this.dgrdData.Name = "dgrdData";
			this.dgrdData.PreviewInfo.Location = new System.Drawing.Point(0, 0);
			this.dgrdData.PreviewInfo.Size = new System.Drawing.Size(0, 0);
			this.dgrdData.PreviewInfo.ZoomFactor = 75;
			this.dgrdData.PrintInfo.ShowOptionsDialog = false;
			this.dgrdData.RecordSelectorWidth = 16;
			this.dgrdData.RowDivider.Color = System.Drawing.Color.DarkGray;
			this.dgrdData.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.Single;
			this.dgrdData.RowHeight = 15;
			this.dgrdData.RowSubDividerColor = System.Drawing.Color.DarkGray;
			this.dgrdData.Size = new System.Drawing.Size(696, 282);
			this.dgrdData.TabIndex = 33;
			this.dgrdData.Text = "c1TrueDBGrid1";
			this.dgrdData.AfterColUpdate += new C1.Win.C1TrueDBGrid.ColEventHandler(this.dgrdData_AfterColUpdate);
			this.dgrdData.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"Select\" Dat" +
				"aField=\"Select_Fld\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Leve" +
				"l=\"0\" Caption=\"Location Code\" DataField=\"Code\"><ValueItems /><GroupInfo /></C1Da" +
				"taColumn><C1DataColumn Level=\"0\" Caption=\"Location Name\" DataField=\"Name\"><Value" +
				"Items /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Bin Control" +
				"\" DataField=\"BIN\"><ValueItems /><GroupInfo /></C1DataColumn></DataCols><Styles t" +
				"ype=\"C1.Win.C1TrueDBGrid.Design.ContextWrapper\"><Data>Style50{}Style51{}Caption{" +
				"AlignHorz:Center;}Normal{}Selected{ForeColor:HighlightText;BackColor:Highlight;}" +
				"Editor{}Style18{}Style19{}Style14{}Style15{}Style16{AlignHorz:Near;}Style17{Alig" +
				"nHorz:Near;}Style10{AlignHorz:Near;}Style11{}OddRow{}Style13{}Style46{AlignHorz:" +
				"Center;ForeColor:Maroon;}Heading{Wrap:True;BackColor:Control;Border:Raised,,1, 1" +
				", 1, 1;ForeColor:ControlText;AlignVert:Center;}Style12{}Style2{}Style85{}Style84" +
				"{}Style87{}Style86{}Style81{}Style80{}Style83{AlignHorz:Near;}Style82{AlignHorz:" +
				"Center;ForeColor:ControlText;}RecordSelector{AlignImage:Center;}Footer{}Style4{}" +
				"Style78{}Style21{}Style20{}Inactive{ForeColor:InactiveCaptionText;BackColor:Inac" +
				"tiveCaption;}EvenRow{BackColor:Aqua;}Style79{}Style49{}Style48{}Style77{AlignHor" +
				"z:Near;}Style76{AlignHorz:Center;ForeColor:ControlText;}FilterBar{}Style47{Align" +
				"Horz:Near;}Style9{}Style8{}Style5{}Group{AlignVert:Center;Border:None,,0, 0, 0, " +
				"0;BackColor:ControlDark;}Style7{}Style6{}Style1{}Style3{}HighlightRow{ForeColor:" +
				"HighlightText;BackColor:Highlight;}</Data></Styles><Splits><C1.Win.C1TrueDBGrid." +
				"MergeView Name=\"\" CaptionHeight=\"17\" ColumnCaptionHeight=\"17\" ColumnFooterHeight" +
				"=\"17\" MarqueeStyle=\"DottedCellBorder\" RecordSelectorWidth=\"16\" DefRecSelWidth=\"1" +
				"6\" VerticalScrollGroup=\"1\" HorizontalScrollGroup=\"1\"><ClientRect>0, 0, 692, 278<" +
				"/ClientRect><BorderSide>0</BorderSide><CaptionStyle parent=\"Style2\" me=\"Style10\"" +
				" /><EditorStyle parent=\"Editor\" me=\"Style5\" /><EvenRowStyle parent=\"EvenRow\" me=" +
				"\"Style8\" /><FilterBarStyle parent=\"FilterBar\" me=\"Style13\" /><FooterStyle parent" +
				"=\"Footer\" me=\"Style3\" /><GroupStyle parent=\"Group\" me=\"Style12\" /><HeadingStyle " +
				"parent=\"Heading\" me=\"Style2\" /><HighLightRowStyle parent=\"HighlightRow\" me=\"Styl" +
				"e7\" /><InactiveStyle parent=\"Inactive\" me=\"Style4\" /><OddRowStyle parent=\"OddRow" +
				"\" me=\"Style9\" /><RecordSelectorStyle parent=\"RecordSelector\" me=\"Style11\" /><Sel" +
				"ectedStyle parent=\"Selected\" me=\"Style6\" /><Style parent=\"Normal\" me=\"Style1\" />" +
				"<internalCols><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style46\" /><Sty" +
				"le parent=\"Style1\" me=\"Style47\" /><FooterStyle parent=\"Style3\" me=\"Style48\" /><E" +
				"ditorStyle parent=\"Style5\" me=\"Style49\" /><GroupHeaderStyle parent=\"Style1\" me=\"" +
				"Style51\" /><GroupFooterStyle parent=\"Style1\" me=\"Style50\" /><Visible>True</Visib" +
				"le><ColumnDivider>DarkGray,Single</ColumnDivider><Width>49</Width><Height>15</He" +
				"ight><DCIdx>0</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"St" +
				"yle2\" me=\"Style76\" /><Style parent=\"Style1\" me=\"Style77\" /><FooterStyle parent=\"" +
				"Style3\" me=\"Style78\" /><EditorStyle parent=\"Style5\" me=\"Style79\" /><GroupHeaderS" +
				"tyle parent=\"Style1\" me=\"Style81\" /><GroupFooterStyle parent=\"Style1\" me=\"Style8" +
				"0\" /><Visible>True</Visible><ColumnDivider>DarkGray,Single</ColumnDivider><Width" +
				">139</Width><Height>15</Height><DCIdx>1</DCIdx></C1DisplayColumn><C1DisplayColum" +
				"n><HeadingStyle parent=\"Style2\" me=\"Style82\" /><Style parent=\"Style1\" me=\"Style8" +
				"3\" /><FooterStyle parent=\"Style3\" me=\"Style84\" /><EditorStyle parent=\"Style5\" me" +
				"=\"Style85\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style87\" /><GroupFooterStyle " +
				"parent=\"Style1\" me=\"Style86\" /><Visible>True</Visible><ColumnDivider>DarkGray,Si" +
				"ngle</ColumnDivider><Width>275</Width><Height>15</Height><DCIdx>2</DCIdx></C1Dis" +
				"playColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style16\" /><Style " +
				"parent=\"Style1\" me=\"Style17\" /><FooterStyle parent=\"Style3\" me=\"Style18\" /><Edit" +
				"orStyle parent=\"Style5\" me=\"Style19\" /><GroupHeaderStyle parent=\"Style1\" me=\"Sty" +
				"le21\" /><GroupFooterStyle parent=\"Style1\" me=\"Style20\" /><Visible>True</Visible>" +
				"<ColumnDivider>DarkGray,Single</ColumnDivider><Width>73</Width><Height>15</Heigh" +
				"t><DCIdx>3</DCIdx></C1DisplayColumn></internalCols></C1.Win.C1TrueDBGrid.MergeVi" +
				"ew></Splits><NamedStyles><Style parent=\"\" me=\"Normal\" /><Style parent=\"Normal\" m" +
				"e=\"Heading\" /><Style parent=\"Heading\" me=\"Footer\" /><Style parent=\"Heading\" me=\"" +
				"Caption\" /><Style parent=\"Heading\" me=\"Inactive\" /><Style parent=\"Normal\" me=\"Se" +
				"lected\" /><Style parent=\"Normal\" me=\"Editor\" /><Style parent=\"Normal\" me=\"Highli" +
				"ghtRow\" /><Style parent=\"Normal\" me=\"EvenRow\" /><Style parent=\"Normal\" me=\"OddRo" +
				"w\" /><Style parent=\"Heading\" me=\"RecordSelector\" /><Style parent=\"Normal\" me=\"Fi" +
				"lterBar\" /><Style parent=\"Caption\" me=\"Group\" /></NamedStyles><vertSplits>1</ver" +
				"tSplits><horzSplits>1</horzSplits><Layout>Modified</Layout><DefaultRecSelWidth>1" +
				"6</DefaultRecSelWidth><ClientArea>0, 0, 692, 278</ClientArea><PrintPageHeaderSty" +
				"le parent=\"\" me=\"Style14\" /><PrintPageFooterStyle parent=\"\" me=\"Style15\" /></Blo" +
				"b>";
			// 
			// chkSelectAll
			// 
			this.chkSelectAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.chkSelectAll.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.chkSelectAll.Location = new System.Drawing.Point(78, 437);
			this.chkSelectAll.Name = "chkSelectAll";
			this.chkSelectAll.Size = new System.Drawing.Size(78, 22);
			this.chkSelectAll.TabIndex = 35;
			this.chkSelectAll.Text = "Select &All";
			this.chkSelectAll.Click += new System.EventHandler(this.chkSelectAll_Click);
			// 
			// lblAllLocation
			// 
			this.lblAllLocation.Location = new System.Drawing.Point(262, 376);
			this.lblAllLocation.Name = "lblAllLocation";
			this.lblAllLocation.TabIndex = 38;
			this.lblAllLocation.Text = "All Locations";
			this.lblAllLocation.Visible = false;
			// 
			// txtBinType
			// 
			this.txtBinType.Location = new System.Drawing.Point(282, 7);
			this.txtBinType.Name = "txtBinType";
			this.txtBinType.Size = new System.Drawing.Size(96, 20);
			this.txtBinType.TabIndex = 10;
			this.txtBinType.Text = "";
			this.txtBinType.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBinType_KeyDown);
			this.txtBinType.Validating += new System.ComponentModel.CancelEventHandler(this.txtBinType_Validating);
			// 
			// btnBinType
			// 
			this.btnBinType.Location = new System.Drawing.Point(380, 7);
			this.btnBinType.Name = "btnBinType";
			this.btnBinType.Size = new System.Drawing.Size(22, 20);
			this.btnBinType.TabIndex = 11;
			this.btnBinType.Text = "...";
			this.btnBinType.Click += new System.EventHandler(this.btnBinType_Click);
			// 
			// lblBinType
			// 
			this.lblBinType.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblBinType.Location = new System.Drawing.Point(232, 7);
			this.lblBinType.Name = "lblBinType";
			this.lblBinType.Size = new System.Drawing.Size(58, 20);
			this.lblBinType.TabIndex = 9;
			this.lblBinType.Text = "Bin Type";
			this.lblBinType.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// btnModel
			// 
			this.btnModel.Location = new System.Drawing.Point(558, 7);
			this.btnModel.Name = "btnModel";
			this.btnModel.Size = new System.Drawing.Size(22, 20);
			this.btnModel.TabIndex = 20;
			this.btnModel.Text = "...";
			this.btnModel.Click += new System.EventHandler(this.btnModel_Click);
			// 
			// txtModel
			// 
			this.txtModel.Location = new System.Drawing.Point(460, 7);
			this.txtModel.Name = "txtModel";
			this.txtModel.Size = new System.Drawing.Size(96, 20);
			this.txtModel.TabIndex = 19;
			this.txtModel.Text = "";
			this.txtModel.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtModel_KeyDown);
			this.txtModel.Validating += new System.ComponentModel.CancelEventHandler(this.txtModel_Validating);
			// 
			// lblModel
			// 
			this.lblModel.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblModel.Location = new System.Drawing.Point(410, 7);
			this.lblModel.Name = "lblModel";
			this.lblModel.Size = new System.Drawing.Size(48, 20);
			this.lblModel.TabIndex = 18;
			this.lblModel.Text = "Model";
			this.lblModel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// txtPartName
			// 
			this.txtPartName.Location = new System.Drawing.Point(100, 123);
			this.txtPartName.MaxLength = 200;
			this.txtPartName.Name = "txtPartName";
			this.txtPartName.Size = new System.Drawing.Size(284, 20);
			this.txtPartName.TabIndex = 31;
			this.txtPartName.Text = "";
			this.txtPartName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPartName_KeyDown);
			this.txtPartName.Validating += new System.ComponentModel.CancelEventHandler(this.txtPartName_Validating);
			// 
			// txtPartNumber
			// 
			this.txtPartNumber.Location = new System.Drawing.Point(100, 101);
			this.txtPartNumber.MaxLength = 24;
			this.txtPartNumber.Name = "txtPartNumber";
			this.txtPartNumber.Size = new System.Drawing.Size(180, 20);
			this.txtPartNumber.TabIndex = 27;
			this.txtPartNumber.Text = "";
			this.txtPartNumber.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPartNumber_KeyDown);
			this.txtPartNumber.Validating += new System.ComponentModel.CancelEventHandler(this.txtPartNumber_Validating);
			// 
			// txtRevision
			// 
			this.txtRevision.Enabled = false;
			this.txtRevision.Location = new System.Drawing.Point(308, 101);
			this.txtRevision.MaxLength = 20;
			this.txtRevision.Name = "txtRevision";
			this.txtRevision.ReadOnly = true;
			this.txtRevision.TabIndex = 29;
			this.txtRevision.Text = "";
			// 
			// btnPartNumberSearch
			// 
			this.btnPartNumberSearch.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnPartNumberSearch.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btnPartNumberSearch.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnPartNumberSearch.Location = new System.Drawing.Point(282, 101);
			this.btnPartNumberSearch.Name = "btnPartNumberSearch";
			this.btnPartNumberSearch.Size = new System.Drawing.Size(24, 20);
			this.btnPartNumberSearch.TabIndex = 28;
			this.btnPartNumberSearch.Text = "...";
			this.btnPartNumberSearch.Click += new System.EventHandler(this.btnPartNumberSearch_Click);
			// 
			// btnPartNameSearch
			// 
			this.btnPartNameSearch.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnPartNameSearch.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btnPartNameSearch.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnPartNameSearch.Location = new System.Drawing.Point(386, 123);
			this.btnPartNameSearch.Name = "btnPartNameSearch";
			this.btnPartNameSearch.Size = new System.Drawing.Size(24, 20);
			this.btnPartNameSearch.TabIndex = 32;
			this.btnPartNameSearch.Text = "...";
			this.btnPartNameSearch.Click += new System.EventHandler(this.btnPartNameSearch_Click);
			// 
			// lblPartNumer
			// 
			this.lblPartNumer.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblPartNumer.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.lblPartNumer.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lblPartNumer.Location = new System.Drawing.Point(6, 101);
			this.lblPartNumer.Name = "lblPartNumer";
			this.lblPartNumer.Size = new System.Drawing.Size(86, 21);
			this.lblPartNumer.TabIndex = 26;
			this.lblPartNumer.Text = "Part Number";
			this.lblPartNumer.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblPartName
			// 
			this.lblPartName.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblPartName.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lblPartName.Location = new System.Drawing.Point(6, 124);
			this.lblPartName.Name = "lblPartName";
			this.lblPartName.Size = new System.Drawing.Size(86, 21);
			this.lblPartName.TabIndex = 30;
			this.lblPartName.Text = "Part Name";
			this.lblPartName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// InOutStockReport
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.btnClose;
			this.ClientSize = new System.Drawing.Size(712, 465);
			this.Controls.Add(this.txtPartName);
			this.Controls.Add(this.txtPartNumber);
			this.Controls.Add(this.txtRevision);
			this.Controls.Add(this.btnPartNumberSearch);
			this.Controls.Add(this.btnPartNameSearch);
			this.Controls.Add(this.lblPartNumer);
			this.Controls.Add(this.lblPartName);
			this.Controls.Add(this.btnModel);
			this.Controls.Add(this.txtModel);
			this.Controls.Add(this.lblModel);
			this.Controls.Add(this.txtBinType);
			this.Controls.Add(this.btnBinType);
			this.Controls.Add(this.lblBinType);
			this.Controls.Add(this.lblAllLocation);
			this.Controls.Add(this.dgrdData);
			this.Controls.Add(this.btnSource);
			this.Controls.Add(this.txtSource);
			this.Controls.Add(this.txtBin);
			this.Controls.Add(this.txtCategory);
			this.Controls.Add(this.txtLocation);
			this.Controls.Add(this.txtMasLoc);
			this.Controls.Add(this.lblSource);
			this.Controls.Add(this.btnBin);
			this.Controls.Add(this.lblBin);
			this.Controls.Add(this.dtmToDate);
			this.Controls.Add(this.lblToDate);
			this.Controls.Add(this.dtmFromDate);
			this.Controls.Add(this.lblFromDate);
			this.Controls.Add(this.btnPrint);
			this.Controls.Add(this.btnHelp);
			this.Controls.Add(this.btnClose);
			this.Controls.Add(this.btnCategory);
			this.Controls.Add(this.btnMasLoc);
			this.Controls.Add(this.lblCCN);
			this.Controls.Add(this.cboCCN);
			this.Controls.Add(this.lblCategory);
			this.Controls.Add(this.lblLocation);
			this.Controls.Add(this.lblMasLoc);
			this.Controls.Add(this.chkSelectAll);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.Name = "InOutStockReport";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "In Out Stock Report";
			this.Load += new System.EventHandler(this.InventoryStatus_Load);
			((System.ComponentModel.ISupportInitialize)(this.cboCCN)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dtmFromDate)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dtmToDate)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dgrdData)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion
		
		#region Methods
		
		private void ChangeSelectedLocation()
		{
			string strSelectedNames = string.Empty;
			string strSelectedIDs = string.Empty;
			bool blnCheckAll = true;
			//bool blnBinControl = false;

			//Loop grid to get selected location
			for(int i = 0; i < dgrdData.RowCount; i++)
			{
				if(dgrdData[i, SELECT_FLD].Equals(DBNull.Value))
				{
					continue;
				}
				
				blnCheckAll &= bool.Parse(dgrdData[i, SELECT_FLD].ToString());

				if(dgrdData[i, SELECT_FLD].ToString().ToUpper().Equals(true.ToString().ToUpper()))
				{
					strSelectedNames += dgrdData[i, MST_LocationTable.CODE_FLD].ToString() + Constants.COMMA + Constants.WHITE_SPACE;
					strSelectedIDs   += dgrdData[i, MST_LocationTable.LOCATIONID_FLD].ToString() + Constants.COMMA + Constants.WHITE_SPACE;
				}
			}
				
			if(strSelectedNames.Length != 0)
			{
				txtLocation.Text = strSelectedNames.Trim().Substring(0, strSelectedNames.Trim().Length - 1);
				txtLocation.Tag = strSelectedIDs.Trim().Substring(0, strSelectedIDs.Trim().Length - 1);
			}
			else
			{
				txtLocation.Text = string.Empty;
				txtLocation.Tag = string.Empty;				
			}
			
			chkSelectAll.Checked = blnCheckAll;

			//btnBin.Enabled = blnBinControl && (txtLocation.Tag.ToString().IndexOf(Constants.COMMA) > 0);
			//txtBin.Enabled = btnBin.Enabled;			
		}

		/// <summary>
		/// Build and show In Out Stock Report
		/// </summary>
		/// <Author> Tuan TQ, 16 Jan, 2006</Author>
		private void ShowInOuStockReport()
		{
			const int MAX_LOCATION_LENGHT = 120;
			const string SQL_DATETIME_FORMAT = "yyyy-MM-dd HH:mm";
			const string APPLICATION_PATH  = @"PCSMain\bin\Debug";

			const string RPT_PAGE_HEADER = "PageHeader";

			const string RPT_COMPANY_FIELD = "fldCompany";
			const string RPT_TITLE_FIELD = "fldTitle";
						
			const string IN_OUT_STOCK_REPORT = "InOutStockReport.xml";

			#region report data

			C1PrintPreviewDialog   printPreview = new C1PrintPreviewDialog();
			C1PrintPreviewDialogBO boDataReport = new C1PrintPreviewDialogBO();			
			// build the category and model list for mutli option
			
			DataTable dtbResult = boDataReport.GetInOutStockData(cboCCN.SelectedValue.ToString(),
			                                                     txtMasLoc.Tag.ToString(), txtLocation.Tag.ToString(),
			                                                     txtBinType.Tag.ToString(), txtBin.Tag.ToString(),
			                                                     ((DateTime)dtmFromDate.Value), 
			                                                     ((DateTime)dtmToDate.Value), 
			                                                     txtCategory.Tag.ToString(), txtSource.Tag.ToString(),
			                                                     txtModel.Tag.ToString(), txtPartNumber.Tag.ToString());

			#endregion

			#region build report

			ReportBuilder reportBuilder = new ReportBuilder();
			
			//Get actual application path
			string strReportPath = Application.StartupPath;
			int intIndex = strReportPath.IndexOf(APPLICATION_PATH);
			if( intIndex > -1 ) 
				strReportPath = strReportPath.Substring(0, intIndex);

			if(strReportPath.Substring(strReportPath.Length -1) == @"\")
				strReportPath += Constants.REPORT_DEFINITION_STORE_LOCATION;
			else
				strReportPath += "\\" + Constants.REPORT_DEFINITION_STORE_LOCATION;
			
			//Set datasource and lay-out path for reports
			reportBuilder.SourceDataTable = dtbResult;
			reportBuilder.ReportDefinitionFolder = strReportPath;
			
			reportBuilder.ReportLayoutFile = IN_OUT_STOCK_REPORT;

			//check if layout is valid
			if(reportBuilder.AnalyseLayoutFile())
				reportBuilder.UseLayoutFile = true;
			else
			{
				PCSMessageBox.Show(ErrorCode.MESSAGE_REPORT_TEMPLATE_FILE_NOT_FOUND, MessageBoxIcon.Error);
				return;
			}

			reportBuilder.MakeDataTableForRender();
						
			// and show it in preview dialog
			printPreview.FormTitle = this.Text;				
			reportBuilder.ReportViewer = printPreview.ReportViewer;
			reportBuilder.RenderReport();
				
			//Header information get from system params				
			reportBuilder.DrawPredefinedField(RPT_COMPANY_FIELD, SystemProperty.SytemParams.Get(SystemParam.COMPANY_FULL_NAME));		
			
			//Draw parameters				
			NameValueCollection arrParamAndValue = new NameValueCollection();			

			arrParamAndValue.Add(lblMasLoc.Text, txtMasLoc.Text);
			if(chkSelectAll.Checked)
				arrParamAndValue.Add(lblLocation.Text, lblAllLocation.Text);
			else if(txtLocation.Text.Length <= MAX_LOCATION_LENGHT)
				arrParamAndValue.Add(lblLocation.Text, txtLocation.Text);
			else
				arrParamAndValue.Add(lblLocation.Text, txtLocation.Text.Substring(0, MAX_LOCATION_LENGHT) + "...");

			arrParamAndValue.Add(lblFromDate.Text, ((DateTime)dtmFromDate.Value).ToString(Constants.DATETIME_FORMAT_HOUR));
			arrParamAndValue.Add(lblToDate.Text, ((DateTime)dtmToDate.Value).ToString(Constants.DATETIME_FORMAT_HOUR));
			
			if(txtBinType.Text.Trim() != string.Empty)
				arrParamAndValue.Add(lblBinType.Text, txtBinType.Text);

			if(txtBin.Text.Trim() != string.Empty)
				arrParamAndValue.Add(lblBin.Text, txtBin.Text);

			if(txtCategory.Text.Trim() != string.Empty)
				arrParamAndValue.Add(lblCategory.Text, txtCategory.Text);

			if(txtModel.Text.Trim() != string.Empty)
				arrParamAndValue.Add(lblModel.Text, txtModel.Text);

			if(txtSource.Text.Trim() != string.Empty)
				arrParamAndValue.Add(lblSource.Text, txtSource.Text);

			if(txtPartNumber.Text.Trim() != string.Empty)
			{
				arrParamAndValue.Add(lblPartNumer.Text, lblPartNumer.Text);
				arrParamAndValue.Add(lblPartName.Text, txtPartName.Text);
			}
						
			//Anchor the Parameter drawing canvas cordinate to the fldTitle
			Field fldTitle = reportBuilder.GetFieldByName(RPT_TITLE_FIELD);
            double dblStartX = fldTitle.Left;
			double dblStartY = fldTitle.Top  + 1.3*fldTitle.RenderHeight;
			reportBuilder.GetSectionByName(RPT_PAGE_HEADER).CanGrow = true;
			reportBuilder.DrawParameters(reportBuilder.GetSectionByName(RPT_PAGE_HEADER), dblStartX, dblStartY, arrParamAndValue, reportBuilder.Report.Font.Size);		
			
			//Refresh report
			reportBuilder.RefreshReport();

			//Print report
			printPreview.Show();

			#endregion

			#region display report data on the form

			ReportData frmReportData = new ReportData();
			frmReportData.Data = dtbResult;
			frmReportData.Show();

			#endregion
		}
		/// <summary>
		/// Validate data before updating into database
		/// </summary>
		/// <returns></returns>
		/// <author> Tran Quoc Tuan </author>
		/// <created date> 
		/// 20 July 2005
		/// </created>
		private bool ValidateData()
		{
			if (FormControlComponents.CheckMandatory(dtmFromDate))
			{
				PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Exclamation);
				dtmFromDate.Focus();				
				return false;
			}

			if (FormControlComponents.CheckMandatory(dtmToDate))
			{
				PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Exclamation);
				dtmToDate.Focus();				
				return false;
			}			
			
			if (FormControlComponents.CheckMandatory(txtMasLoc))
			{
				PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Exclamation);
				txtMasLoc.Focus();				
				return false;
			}

			if (txtLocation.Text == string.Empty)
			{
				PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Exclamation);
				dgrdData.Focus();				
				return false;
			}

			return true;			
		}
		

		private DataTable BuildLocationTableTemplate()
		{

			DataTable dtbTable = new DataTable(MST_LocationTable.TABLE_NAME);
			dtbTable.Columns.Add(SELECT_FLD, typeof(System.Boolean));
			dtbTable.Columns.Add(MST_LocationTable.CODE_FLD, typeof(System.String));
			dtbTable.Columns.Add(MST_LocationTable.NAME_FLD, typeof(System.String));
			dtbTable.Columns.Add(MST_LocationTable.BIN_FLD, typeof(System.Boolean));
			dtbTable.Columns.Add(MST_LocationTable.LOCATIONID_FLD, typeof(System.Int32));

			return dtbTable;
		}

		private void FormatDataGrid()
		{
			
			//Restore layout
			FormControlComponents.RestoreGridLayout(dgrdData, dtbGridLayOut);
			
			//Set properties
			dgrdData.AllowAddNew = false;
			dgrdData.AllowDelete = false;
			dgrdData.AllowUpdate = true;

			//Loc columns
			for(int i =0; i < dgrdData.Splits[0].DisplayColumns.Count; i++)
			{
				dgrdData.Splits[0].DisplayColumns[i].Locked = true;
			}
			
			//Enable Select column for selecting
			dgrdData.Splits[0].DisplayColumns[SELECT_FLD].Locked = false;				

			//Change display format			
			dgrdData.Columns[SELECT_FLD].ValueItems.Presentation = C1.Win.C1TrueDBGrid.PresentationEnum.CheckBox;
			dgrdData.Columns[MST_LocationTable.BIN_FLD].ValueItems.Presentation = C1.Win.C1TrueDBGrid.PresentationEnum.CheckBox;
		}
		
		//
		private void LoadLocationGrid(int pintMasterLocationID)
		{
			DataTable dtbLocation;

			if(pintMasterLocationID > 0)
			{
				LocationBO boLocation = new LocationBO();

				//HACK: Added by Tuan TQ 03 Apr, 2006. Fix error no. 3688
				string strConditionByRecord = (new UtilsBO()).GetConditionByRecord(SystemProperty.UserName, MST_LocationTable.TABLE_NAME);
				
				dtbLocation = boLocation.GetByLocation4Selecting(pintMasterLocationID, strConditionByRecord);
				//End hack
			}
			else
			{
				dtbLocation = BuildLocationTableTemplate();
			}

			//Bind data to grid
			dgrdData.DataSource = dtbLocation;
			//Format grid
			FormatDataGrid();
			chkSelectAll.Checked = false;
			chkSelectAll.Enabled = (dgrdData.RowCount > 0);
		}

		/// <summary>
		/// Clear information related to MasLoc, Location or Bin
		/// </summary>
		/// <param name="penuLocationType"></param>
		private void ClearRelatedInfo(EnumLocationType penuLocationType)
		{			
			switch(penuLocationType)
			{
				case EnumLocationType.MaterLocation:

					txtMasLoc.Tag = string.Empty;
					txtLocation.Text = string.Empty;
					txtLocation.Tag = string.Empty;
					txtBin.Text = string.Empty;
					txtBin.Tag = string.Empty;					
					break;

				case EnumLocationType.Location:
					txtLocation.Tag = string.Empty;
					txtBin.Text = string.Empty;
					txtBin.Tag = string.Empty;					
					break;

				case EnumLocationType.Bin:
					txtBin.Text = string.Empty;
					txtBin.Tag = string.Empty;
					
					break;
			}			
		}	

		/// <summary>
		/// Fill related data on controls when select Master Location
		/// </summary>
		/// <param name="blnOpenOnly"></param>
		private bool SelectMasterLocation(bool blnAlwaysShowDialog)
		{
			Hashtable htbCriteria = new Hashtable();

			if(cboCCN.SelectedValue != null)
			{
				htbCriteria.Add(MST_MasterLocationTable.CCNID_FLD, cboCCN.SelectedValue);
			}
			else
			{
				htbCriteria.Add(MST_MasterLocationTable.CCNID_FLD, 0);
			}				

			//Call OpenSearchForm for selecting Master Location
			DataRowView drwResult = FormControlComponents.OpenSearchForm(MST_MasterLocationTable.TABLE_NAME, MST_MasterLocationTable.CODE_FLD, txtMasLoc.Text, htbCriteria, blnAlwaysShowDialog);
			
			// If has Master location matched searching condition, fill values to form's controls
			if(drwResult != null)
			{
				//Check if master location was changed then clear grid content
				if(!txtMasLoc.Tag.Equals(drwResult[MST_MasterLocationTable.MASTERLOCATIONID_FLD]))
				{
					LoadLocationGrid(int.Parse(drwResult[MST_MasterLocationTable.MASTERLOCATIONID_FLD].ToString()));
				}

				txtMasLoc.Text = drwResult[MST_MasterLocationTable.CODE_FLD].ToString();
				txtMasLoc.Tag = drwResult[MST_MasterLocationTable.MASTERLOCATIONID_FLD];
				
				//Reset modify status
				txtMasLoc.Modified = false;
			}
			else if(!blnAlwaysShowDialog)
			{
				txtMasLoc.Focus();
				return false;
			}
			
			//Return result
			return true;			
		}		
		
		/// <summary>
		/// Fill related data on controls when select BIN
		/// </summary>
		/// <param name="blnOpenOnly"></param>
		private bool SelectModel(bool blnAlwaysShowDialog)
		{
			const string PRODUCT_REVISION_VIEW = "v_ProductRevision";
			const string REVISION_FLD = "Revision";

			//Call OpenSearchForm for selecting Master Location
			DataTable dtbData = FormControlComponents.OpenSearchFormForMultiSelectedRow(PRODUCT_REVISION_VIEW, REVISION_FLD, txtModel.Text, string.Empty, blnAlwaysShowDialog);
			
			// If has Master location matched searching condition, fill values to form's controls
			if (dtbData != null && dtbData.Rows.Count > 0)
			{				
				StringBuilder sbCode = new StringBuilder();
				StringBuilder sbCodeTag = new StringBuilder();
				foreach (DataRow drowData in dtbData.Rows)
				{
					sbCode.Append(drowData[REVISION_FLD].ToString()).Append(",");
					sbCodeTag.Append("'" + drowData[REVISION_FLD].ToString() + "'").Append(",");
				}
				txtModel.Text = sbCode.ToString(0, sbCode.Length - 1);
				txtModel.Tag = sbCodeTag.ToString(0, sbCodeTag.Length - 1);
				
				//Reset modify status
				txtModel.Modified = false;
			}
			else if(!blnAlwaysShowDialog)
			{
				txtModel.Focus();
				return false;
			}
			
			return true;			
		}


		/// <summary>
		/// Fill related data on controls when select BIN Type
		/// </summary>
		private bool SelectBinType(bool blnAlwaysShowDialog)
		{
			DataTable dtbData = FormControlComponents.OpenSearchFormForMultiSelectedRow(enm_BINTypeTable.TABLE_NAME, enm_BINTypeTable.NAME_FLD, txtBinType.Text, string.Empty, blnAlwaysShowDialog);
			
			if (dtbData != null && dtbData.Rows.Count > 0)
			{				
				StringBuilder sbCode = new StringBuilder();
				StringBuilder sbCodeTag = new StringBuilder();
				foreach (DataRow drowData in dtbData.Rows)
				{
					sbCode.Append(drowData[enm_BINTypeTable.NAME_FLD].ToString()).Append(",");
					sbCodeTag.Append("'" + drowData[enm_BINTypeTable.BINTYPEID_FLD].ToString() + "'").Append(",");
				}
				txtBinType.Text = sbCode.ToString(0, sbCode.Length - 1);
				txtBinType.Tag = sbCodeTag.ToString(0, sbCodeTag.Length - 1);
				
				//Reset modify status
				txtBinType.Modified = false;
			}
			else if(!blnAlwaysShowDialog)
			{
				txtBinType.Focus();
				return false;
			}
			
			return true;
		}


		/// <summary>
		/// Fill related data on controls when select BIN
		/// </summary>
		/// <param name="blnOpenOnly"></param>
		private bool SelectBIN(bool blnAlwaysShowDialog)
		{
			string strFilter = "1=1";
				
			if(txtLocation.Tag != null && txtLocation.Tag.ToString() != string.Empty)
				strFilter += " AND " + MST_BINTable.TABLE_NAME + "." + MST_BINTable.LOCATIONID_FLD + " IN (" + txtLocation.Tag.ToString() + ")";
			if (txtBinType.Tag != null && txtBinType.Tag.ToString() != string.Empty)
				strFilter += " AND " + MST_BINTable.TABLE_NAME + "." + MST_BINTable.BINTYPEID_FLD + " IN (" + txtBinType.Tag.ToString() + ")";
			
			DataRowView drvResult = FormControlComponents.OpenSearchFormWithWhere(MST_BINTable.TABLE_NAME, MST_BINTable.CODE_FLD, txtBin.Text, strFilter, blnAlwaysShowDialog);

			// If has Master location matched searching condition, fill values to form's controls
			if (drvResult != null)
			{
				//Check if master location was changed then clear grid content
				if(!txtBin.Tag.Equals(drvResult[MST_BINTable.BINID_FLD]))
				{
					ClearRelatedInfo(EnumLocationType.Bin);
				}
				txtBin.Text = drvResult[MST_BINTable.CODE_FLD].ToString();
				txtBin.Tag = drvResult[MST_BINTable.BINID_FLD];
				
				//Reset modify status
				txtBin.Modified = false;
			}
			else if(!blnAlwaysShowDialog)
			{
				txtBin.Focus();
				return false;
			}				
			
			return true;			
		}


		/// <summary>
		/// Fill related data on controls when select Category
		/// </summary>
		/// <param name="blnOpenOnly"></param>
		private bool SelectCategory(bool blnAlwaysShowDialog)
		{
			//Call OpenSearchForm for selecting Category
			//DataRowView drwResult = FormControlComponents.OpenSearchForm(ITM_CategoryTable.TABLE_NAME, ITM_CategoryTable.CODE_FLD, txtCategory.Text, htbCriteria, blnAlwaysShowDialog);
			DataTable dtbData = FormControlComponents.OpenSearchFormForMultiSelectedRow(ITM_CategoryTable.TABLE_NAME, ITM_CategoryTable.CODE_FLD, txtCategory.Text, string.Empty, blnAlwaysShowDialog);
			
			// If has Master location matched searching condition, fill values to form's controls
			if (dtbData != null && dtbData.Rows.Count > 0)
			{
				StringBuilder sbCode = new StringBuilder();
				StringBuilder sbID = new StringBuilder();
				foreach (DataRow drowData in dtbData.Rows)
				{
					sbCode.Append(drowData[ITM_CategoryTable.CODE_FLD].ToString()).Append(",");
					sbID.Append(drowData[ITM_CategoryTable.CATEGORYID_FLD].ToString()).Append(",");
				}
				txtCategory.Text = sbCode.ToString(0, sbCode.Length - 1);
				txtCategory.Tag = sbID.ToString(0, sbID.Length - 1);
				
				//Reset modify status
				txtCategory.Modified = false;
			}
			else if(!blnAlwaysShowDialog)
			{
				txtCategory.Focus();
				return false;
			}				
			
			return true;			
		}


		/// <summary>
		/// Fill related data on controls when select Source
		/// </summary>
		/// <param name="blnOpenOnly"></param>
		private bool SelectSource(bool blnAlwaysShowDialog)
		{
			Hashtable htbCriteria = new Hashtable();
			
			//Call OpenSearchForm for selecting Master Location
			DataRowView drwResult = FormControlComponents.OpenSearchForm(ITM_SourceTable.TABLE_NAME, ITM_SourceTable.CODE_FLD, txtSource.Text, htbCriteria, blnAlwaysShowDialog);
			
			// If has Master location matched searching condition, fill values to form's controls
			if (drwResult != null)
			{				
				txtSource.Text = drwResult[ITM_SourceTable.CODE_FLD].ToString();
				txtSource.Tag = drwResult[ITM_SourceTable.SOURCEID_FLD];
				
				//Reset modify status
				txtSource.Modified = false;
			}
			else if(!blnAlwaysShowDialog)
			{
				txtSource.Focus();
				return false;
			}
			
			return true;			
		}


		private bool SelectItem(string pstrMethodName, string pstrFilterField, string pstrFilterValue, bool pblnAlwaysShowDialog)
		{
			try
			{
				string strFilter = ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.CCNID_FLD + "=" + cboCCN.SelectedValue.ToString();
				
				if(txtCategory.Tag != null && txtCategory.Tag.ToString() != string.Empty)
					strFilter += " AND " + ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.CATEGORYID_FLD + " IN (" + txtCategory.Tag.ToString() + ")";
				if (txtModel.Tag != null && txtModel.Tag.ToString() != string.Empty)
					strFilter += " AND " + ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.REVISION_FLD + " IN (" + txtModel.Tag.ToString() + ")";
				if (txtSource.Tag != null && txtSource.Tag.ToString() != string.Empty)
					strFilter += " AND " + ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.SOURCEID_FLD + " IN (" + txtSource.Tag.ToString() + ")";

				DataRowView drvResult = FormControlComponents.OpenSearchFormWithWhere(ITM_ProductTable.TABLE_NAME, pstrFilterField, pstrFilterValue, strFilter, pblnAlwaysShowDialog);
				
				if (drvResult != null)
				{
					txtPartNumber.Text = drvResult[ITM_ProductTable.CODE_FLD].ToString();
					txtPartName.Text = drvResult[ITM_ProductTable.DESCRIPTION_FLD].ToString();
					txtRevision.Text = drvResult[ITM_ProductTable.REVISION_FLD].ToString();
					txtPartNumber.Tag = drvResult[ITM_ProductTable.PRODUCTID_FLD].ToString();
					
					//Reset modify status
					txtCategory.Modified = false;
				}
				else if(!pblnAlwaysShowDialog)
				{
					switch (pstrFilterField)
					{
						case ITM_ProductTable.CODE_FLD:
							txtPartNumber.Focus();
							break;
						default:
							txtPartName.Focus();
							break;
					}
					return false;
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

		#region Event Processing

		/// <summary>
		/// InventoryStatus_Load
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Friday, October 28 2005</date>
		private void InventoryStatus_Load(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".InventoryStatus_Load()";
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

				// Load combo box
				DataSet dstCCN = boUtil.ListCCN();
				cboCCN.DataSource = dstCCN.Tables[MST_CCNTable.TABLE_NAME];
				cboCCN.DisplayMember = MST_CCNTable.CODE_FLD;
				cboCCN.ValueMember = MST_CCNTable.CCNID_FLD;
				FormControlComponents.PutDataIntoC1ComboBox(cboCCN, dstCCN.Tables[MST_CCNTable.TABLE_NAME],MST_CCNTable.CODE_FLD,MST_CCNTable.CCNID_FLD,MST_CCNTable.TABLE_NAME);
				
				if (SystemProperty.CCNID != 0)
				{
					cboCCN.SelectedValue = SystemProperty.CCNID;
				}
				
				// fill default master location
				txtMasLoc.Tag = SystemProperty.MasterLocationID;
				txtMasLoc.Text = SystemProperty.MasterLocationCode;
				txtLocation.Tag = string.Empty;
				txtBin.Tag = string.Empty;
				txtCategory.Tag = string.Empty;
				txtSource.Tag = string.Empty;
				txtBinType.Tag = string.Empty;
				txtModel.Tag = string.Empty;
				txtPartNumber.Tag = string.Empty;
				
				//btnBin.Enabled = false;
				//txtBin.Enabled = false;

				//Store grid layout
				dtbGridLayOut = FormControlComponents.StoreGridLayout(dgrdData);
				LoadLocationGrid(SystemProperty.MasterLocationID);
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
		
		private void btnClose_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}
		
		/// <summary>
		/// btnMasLoc_Click
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>		
		private void btnMasLoc_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnMasLoc_Click()";
			try
			{
				SelectMasterLocation(true);
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
		/// btnLocation_Click
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Friday, October 28 2005</date>
		private void btnLocation_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnLocation_Click()";
			try
			{
				DataRowView drwResult = null;
				Hashtable htbCriteria = new Hashtable();
				//User has selected Master Location
				if (txtMasLoc.Text.Trim() != string.Empty)
				{
					htbCriteria.Add(MST_LocationTable.MASTERLOCATIONID_FLD, int.Parse(txtMasLoc.Tag.ToString()));	
				}
				else //User has not selected Master Location
				{
					PCSMessageBox.Show(ErrorCode.MESSAGE_RGA_MASTERLOCATION, MessageBoxIcon.Warning);
					txtLocation.Text = string.Empty;
					txtMasLoc.Focus();
					return;
				}
				drwResult = FormControlComponents.OpenSearchForm(MST_LocationTable.TABLE_NAME, MST_LocationTable.CODE_FLD, txtLocation.Text.Trim(), htbCriteria, true);
				if (drwResult != null)
				{
					txtLocation.Text = drwResult[MST_LocationTable.CODE_FLD].ToString();
					txtLocation.Tag = drwResult[MST_LocationTable.LOCATIONID_FLD];
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
		/// btnCategory_Click
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>		
		private void btnCategory_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnCategory_Click()";
			try
			{
				SelectCategory(true);
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

		private void txtMasLoc_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtMasLoc_Validating()";
			try
			{				
				if(txtMasLoc.Text.Length == 0)
				{
					txtMasLoc.Tag = string.Empty;
					return;
				}
				else if(!txtMasLoc.Modified)
				{
					return;
				}

				e.Cancel = !SelectMasterLocation(false);
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
		/// txtMasLoc_KeyDown
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>		
		private void txtMasLoc_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtMasLoc_KeyDown()";

			try
			{
				if ((e.KeyCode == Keys.F4) && (btnMasLoc.Enabled))
				{
					SelectMasterLocation(true);
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
		/// txtCategory_KeyDown
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>		
		private void txtCategory_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtCategory_KeyDown()";

			try
			{
				if ((e.KeyCode == Keys.F4) && (btnCategory.Enabled))
				{
					SelectCategory(true);
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
		/// txtCategory_Validating
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>		
		private void txtCategory_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtCategory_Validating()";
			try
			{
				if(txtCategory.Text.Length == 0)
				{
					txtCategory.Tag = string.Empty;
					return;
				}
				else if(!txtCategory.Modified)
				{
					return;
				}

				e.Cancel = !SelectCategory(false);
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
		
		private void btnPrint_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnPrint_Click()";
			try
			{
				if(ValidateData())
				{
					this.Cursor = Cursors.WaitCursor;
					
					ShowInOuStockReport();					
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
			finally
			{
				this.Cursor = Cursors.Default;
			}			
		}
		
		private void btnBin_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnBin_Click()";
			try
			{
				if(txtLocation.Text.Trim() == string.Empty)
				{
					PCSMessageBox.Show(ErrorCode.MESSAGE_RGA_SELECTLOC, MessageBoxIcon.Exclamation);
					dgrdData.Focus();
					
					return;
				}

				SelectBIN(true);
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
		
		private void btnSource_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnSource_Click()";
			try
			{
				SelectSource(true);
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

		private void txtBin_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtBin_KeyDown()";

			try
			{
				if ((e.KeyCode == Keys.F4) && (btnBin.Enabled))
				{
					if(txtLocation.Text.Trim() == string.Empty)
					{
						PCSMessageBox.Show(ErrorCode.MESSAGE_RGA_SELECTLOC, MessageBoxIcon.Exclamation);
						dgrdData.Focus();
						return;
					}

					SelectBIN(true);
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

		private void txtBin_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtBin_Validating()";
			try
			{				
				if(txtBin.Text.Length == 0)
				{
					txtBin.Tag = string.Empty;
					return;
				}
				else if(!txtBin.Modified)
				{
					return;
				}
				
				if(txtLocation.Text.Trim() == string.Empty)
				{
					PCSMessageBox.Show(ErrorCode.MESSAGE_RGA_SELECTLOC, MessageBoxIcon.Exclamation);
					dgrdData.Focus();
					return;
				}

				e.Cancel = !SelectBIN(false);
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

		private void txtSource_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtSource_Validating()";
			try
			{				
				if(txtSource.Text.Length == 0)
				{
					txtSource.Tag = string.Empty;
					return;
				}
				else if(!txtSource.Modified)
				{
					return;
				}

				e.Cancel = !SelectSource(false);
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

		private void txtSource_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtSource_KeyDown()";

			try
			{
				if ((e.KeyCode == Keys.F4) && (btnSource.Enabled))
				{
					SelectSource(true);
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
		

		private void dtmFromDate_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".dtmFromDate_Validating()";

			try
			{
				if(dtmFromDate.Text.Trim() != string.Empty && dtmToDate.Text.Trim() != string.Empty)
				{
					if( ((DateTime)dtmFromDate.Value) >  ((DateTime)dtmToDate.Value))
					{
						PCSMessageBox.Show(ErrorCode.MESSAGE_MP_PERIODDATE, MessageBoxIcon.Error);
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

		private void dtmToDate_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtSource_KeyDown()";

			try
			{
				if(dtmFromDate.Text.Trim() != string.Empty && dtmToDate.Text.Trim() != string.Empty)
				{
					if( ((DateTime)dtmFromDate.Value) >  ((DateTime)dtmToDate.Value))
					{
						PCSMessageBox.Show(ErrorCode.MESSAGE_MP_PERIODDATE, MessageBoxIcon.Error);
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
		
		private void dgrdData_AfterColUpdate(object sender, C1.Win.C1TrueDBGrid.ColEventArgs e)
		{
			const string METHOD_NAME = THIS + ".dgrdData_AfterColUpdate()";

			try
			{
				if(e.Column.DataColumn.DataField != SELECT_FLD)
				{
					return;
				}

				//Show selected loaction
				ChangeSelectedLocation();
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
		
		private void chkSelectAll_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".chkSelectAll_Click";
			try
			{
				for (int i=0 ; i < dgrdData.RowCount; i++) 
				{
					dgrdData[i, SELECT_FLD] = chkSelectAll.Checked? 1: 0;
				}				
				dgrdData.UpdateData();

				//Show selected loaction
				ChangeSelectedLocation();
			}			
			catch (Exception ex)
			{
				// displays the error message.
				PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
				// log message.
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

		

		private void btnBinType_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnBinType_Click()";
			try
			{
				SelectBinType(true);
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
		
		private void btnModel_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnModel_Click()";
			try
			{
				SelectModel(true);
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

		

		private void txtBinType_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtBinType_KeyDown()";

			try
			{
				if ((e.KeyCode == Keys.F4) && (btnBinType.Enabled))
				{
					SelectBinType(true);
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

		private void txtBinType_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtBinType_Validating()";
			try
			{
				if(txtBinType.Text.Length == 0)
				{
					txtBinType.Tag = string.Empty;
					return;
				}
				else if(!txtBinType.Modified)
				{
					return;
				}

				e.Cancel = !SelectBinType(false);
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

		private void txtModel_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtModel_Validating()";
			try
			{
				if(txtModel.Text.Length == 0)
				{
					txtModel.Tag = string.Empty;
					return;
				}
				else if(!txtModel.Modified)
				{
					return;
				}

				e.Cancel = !SelectModel(false);
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

		private void txtModel_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtModel_KeyDown()";

			try
			{
				if ((e.KeyCode == Keys.F4) && (btnModel.Enabled))
				{
					SelectModel(true);
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

		#endregion Event Processing

		private void btnPartNumberSearch_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnPartNumberSearch_Click()";

			try
			{
				SelectItem(METHOD_NAME, ITM_ProductTable.CODE_FLD, txtPartNumber.Text, true);
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

		private void btnPartNameSearch_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnPartNameSearch_Click()";

			try
			{
				SelectItem(METHOD_NAME, ITM_ProductTable.DESCRIPTION_FLD, txtPartName.Text, true);
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

		private void txtPartNumber_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtPartNumber_Validating()";
			try
			{				
				if(txtPartNumber.Text.Trim().Length == 0)
				{
					txtPartNumber.Tag = string.Empty;
					txtPartName.Text = string.Empty;
					txtRevision.Text = string.Empty;
					return;
				}
				else if(!txtPartNumber.Modified)
					return;
				
				e.Cancel = !SelectItem(METHOD_NAME, ITM_ProductTable.CODE_FLD, txtPartNumber.Text, false);
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

		private void txtPartName_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtPartName_Validating()";
			try
			{				
				if(txtPartName.Text.Trim().Length == 0)
				{
					txtPartNumber.Tag = string.Empty;
					txtPartNumber.Text = string.Empty;
					txtRevision.Text = string.Empty;
					return;
				}
				else if(!txtPartName.Modified)
					return;
				
				e.Cancel = !SelectItem(METHOD_NAME, ITM_ProductTable.DESCRIPTION_FLD, txtPartName.Text, false);
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

		private void txtPartNumber_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtPartNumber_KeyDown()";

			try
			{
				if ((e.KeyCode == Keys.F4) && (btnPartNumberSearch.Enabled))
					SelectItem(METHOD_NAME, ITM_ProductTable.CODE_FLD, txtPartNumber.Text, true);
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

		private void txtPartName_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtPartName_KeyDown()";

			try
			{
				if ((e.KeyCode == Keys.F4) && (btnPartNameSearch.Enabled))
					SelectItem(METHOD_NAME, ITM_ProductTable.DESCRIPTION_FLD, txtPartName.Text, true);
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
	}
}
