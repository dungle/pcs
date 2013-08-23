using System;
using System.Data;
using System.Collections;
using System.Drawing.Printing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using C1.C1Report;
using C1.Win.C1TrueDBGrid;

//Using PCS's namespaces
using PCSComMaterials.ActualCost.BO;
using PCSComMaterials.ActualCost.DS;
using PCSComUtils.Common;
using PCSComUtils.Common.BO;
using PCSComUtils.DataAccess;
using PCSComUtils.PCSExc;
using PCSUtils.Framework.ReportFrame;
using PCSUtils.Log;
using PCSUtils.Utils;


namespace PCSMaterials.ActualCost
{
	/// <summary>
	/// Summary description for ActualCostDistributionSetup.
	/// </summary>
	public class ActualCostDistributionSetup : System.Windows.Forms.Form
	{
		#region Declaration 

		#region System Generated
		/// <summary>
		/// Required designer variables.
		/// </summary>
		private C1.Win.C1List.C1Combo cboCCN;
		private System.Windows.Forms.Label lblCCN;
		private System.Windows.Forms.Button btnHelp;
		private System.Windows.Forms.Button btnDelete;
		private System.Windows.Forms.Button btnSave;
		private System.Windows.Forms.Button btnAdd;
		private System.Windows.Forms.Button btnEdit;
		private System.Windows.Forms.Button btnClose;
		private C1.Win.C1TrueDBGrid.C1TrueDBGrid dgrdData;
		private System.Windows.Forms.TextBox txtPeriod;
		private System.Windows.Forms.Button btnPeriodSearch;
		private System.Windows.Forms.Label lblPeriod;
		private System.Windows.Forms.Label lblToDate;
		private System.Windows.Forms.Label lblFromDate;
		private C1.Win.C1Input.C1DateEdit dtmToDate;
		private C1.Win.C1Input.C1DateEdit dtmFromDate;
		private C1.Win.C1Input.C1NumericEdit numValue;
		private C1.Win.C1Input.C1DateEdit dtmRollupDate;
		private System.Windows.Forms.Button btnRollup;
		private System.Windows.Forms.Button btnAllocate;
		private System.Windows.Forms.Button btnPrint;
		private System.Windows.Forms.Button btnImport;
		private System.Windows.Forms.TextBox txtCurrency;
		private System.Windows.Forms.Label lblCurrency;		
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.Label lblRollupDate;
		
		#endregion System Generated

		#region Variables
		//======================================================
		private EnumAction enuFormAction = EnumAction.Default;
		
		private const string THIS = "PCSMaterials.ActualCost.ActualCostDistributionSetup";
				
		private DataTable dtbGridLayOut;
		private DataTable dtbDetail;		
		private bool blnDataIsValid = false;
		

		private ActualCostDistributionSetupBO boMaster = new ActualCostDistributionSetupBO();
		private System.Windows.Forms.Label lblAllocatingMessage;
		private System.Windows.Forms.Label lblEndDayOfMonth;
		private System.Windows.Forms.Label lblFirstDayOfMonth;
		private System.Windows.Forms.OpenFileDialog dlgOpenImpFile;
		private CST_ActCostAllocationMasterVO voMaster = new CST_ActCostAllocationMasterVO();

		#endregion Variables
		
		#region Constants for excel file format
		private const int DATA_SHT = 0;

		private const int PERIOD_COL = 1;
		private const int PERIOD_ROW = 0;

		private const int FROMDATE_COL = 1;
		private const int FROMDATE_ROW = 1;
		
		private const int TODATE_COL = 1;
		private const int TODATE_ROW = 2;

		private const int BEGINDATA_ROW = 5;
		private const int TOTALCOL_NUM = 9;

		private const int NO_COL = 0;
		private const int COSTELEMENT_COL = 1;
		private const int ALLOCATIONAMOUNT_COL = 2;
		private const int DEPARTMENT_COL = 3;
		private const int PRODUCTIONLINE_COL = 4;
		private const int GROUP_COL = 5;
		private const int PARTNO_COL = 6;
		private const int MODEL_COL = 7;
		private const int PARTNAME_COL = 8;

		private const int EXCEL_RED_COLOR = 3;

		private const int DATE_OFS = 2;
		private System.Windows.Forms.Button btnDelAllocation;
		private System.Windows.Forms.Label lblDelAllocatingMessage;
		private System.Windows.Forms.Button btnDelChargeAllocation;
		private System.Windows.Forms.TextBox txtTotalAmount;
		private System.Windows.Forms.Label lblTotalAmount;
		private DateTime EXCEL_SERIAL_DATE = new DateTime(1900,1,1,0,0,0);
		#endregion Constants for excel file format

		//======================================================
		#endregion Declaration 
		
		#region Constructor, Destructor
		
		public ActualCostDistributionSetup()
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
		
		#endregion Declaration 

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(ActualCostDistributionSetup));
			this.cboCCN = new C1.Win.C1List.C1Combo();
			this.lblCCN = new System.Windows.Forms.Label();
			this.txtPeriod = new System.Windows.Forms.TextBox();
			this.btnPeriodSearch = new System.Windows.Forms.Button();
			this.lblPeriod = new System.Windows.Forms.Label();
			this.btnHelp = new System.Windows.Forms.Button();
			this.btnDelete = new System.Windows.Forms.Button();
			this.btnSave = new System.Windows.Forms.Button();
			this.btnAdd = new System.Windows.Forms.Button();
			this.btnEdit = new System.Windows.Forms.Button();
			this.btnClose = new System.Windows.Forms.Button();
			this.btnRollup = new System.Windows.Forms.Button();
			this.dgrdData = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
			this.lblToDate = new System.Windows.Forms.Label();
			this.lblFromDate = new System.Windows.Forms.Label();
			this.dtmToDate = new C1.Win.C1Input.C1DateEdit();
			this.dtmFromDate = new C1.Win.C1Input.C1DateEdit();
			this.numValue = new C1.Win.C1Input.C1NumericEdit();
			this.btnAllocate = new System.Windows.Forms.Button();
			this.btnPrint = new System.Windows.Forms.Button();
			this.dtmRollupDate = new C1.Win.C1Input.C1DateEdit();
			this.lblRollupDate = new System.Windows.Forms.Label();
			this.btnImport = new System.Windows.Forms.Button();
			this.txtCurrency = new System.Windows.Forms.TextBox();
			this.lblCurrency = new System.Windows.Forms.Label();
			this.lblAllocatingMessage = new System.Windows.Forms.Label();
			this.lblFirstDayOfMonth = new System.Windows.Forms.Label();
			this.lblEndDayOfMonth = new System.Windows.Forms.Label();
			this.dlgOpenImpFile = new System.Windows.Forms.OpenFileDialog();
			this.btnDelAllocation = new System.Windows.Forms.Button();
			this.lblDelAllocatingMessage = new System.Windows.Forms.Label();
			this.btnDelChargeAllocation = new System.Windows.Forms.Button();
			this.txtTotalAmount = new System.Windows.Forms.TextBox();
			this.lblTotalAmount = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.cboCCN)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dgrdData)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dtmToDate)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dtmFromDate)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numValue)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dtmRollupDate)).BeginInit();
			this.SuspendLayout();
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
			// txtPeriod
			// 
			this.txtPeriod.AccessibleDescription = resources.GetString("txtPeriod.AccessibleDescription");
			this.txtPeriod.AccessibleName = resources.GetString("txtPeriod.AccessibleName");
			this.txtPeriod.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("txtPeriod.Anchor")));
			this.txtPeriod.AutoSize = ((bool)(resources.GetObject("txtPeriod.AutoSize")));
			this.txtPeriod.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("txtPeriod.BackgroundImage")));
			this.txtPeriod.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("txtPeriod.Dock")));
			this.txtPeriod.Enabled = ((bool)(resources.GetObject("txtPeriod.Enabled")));
			this.txtPeriod.Font = ((System.Drawing.Font)(resources.GetObject("txtPeriod.Font")));
			this.txtPeriod.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("txtPeriod.ImeMode")));
			this.txtPeriod.Location = ((System.Drawing.Point)(resources.GetObject("txtPeriod.Location")));
			this.txtPeriod.MaxLength = ((int)(resources.GetObject("txtPeriod.MaxLength")));
			this.txtPeriod.Multiline = ((bool)(resources.GetObject("txtPeriod.Multiline")));
			this.txtPeriod.Name = "txtPeriod";
			this.txtPeriod.PasswordChar = ((char)(resources.GetObject("txtPeriod.PasswordChar")));
			this.txtPeriod.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("txtPeriod.RightToLeft")));
			this.txtPeriod.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("txtPeriod.ScrollBars")));
			this.txtPeriod.Size = ((System.Drawing.Size)(resources.GetObject("txtPeriod.Size")));
			this.txtPeriod.TabIndex = ((int)(resources.GetObject("txtPeriod.TabIndex")));
			this.txtPeriod.Text = resources.GetString("txtPeriod.Text");
			this.txtPeriod.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("txtPeriod.TextAlign")));
			this.txtPeriod.Visible = ((bool)(resources.GetObject("txtPeriod.Visible")));
			this.txtPeriod.WordWrap = ((bool)(resources.GetObject("txtPeriod.WordWrap")));
			this.txtPeriod.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPeriod_KeyDown);
			this.txtPeriod.Validating += new System.ComponentModel.CancelEventHandler(this.txtPeriod_Validating);
			// 
			// btnPeriodSearch
			// 
			this.btnPeriodSearch.AccessibleDescription = resources.GetString("btnPeriodSearch.AccessibleDescription");
			this.btnPeriodSearch.AccessibleName = resources.GetString("btnPeriodSearch.AccessibleName");
			this.btnPeriodSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnPeriodSearch.Anchor")));
			this.btnPeriodSearch.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnPeriodSearch.BackgroundImage")));
			this.btnPeriodSearch.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnPeriodSearch.Dock")));
			this.btnPeriodSearch.Enabled = ((bool)(resources.GetObject("btnPeriodSearch.Enabled")));
			this.btnPeriodSearch.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnPeriodSearch.FlatStyle")));
			this.btnPeriodSearch.Font = ((System.Drawing.Font)(resources.GetObject("btnPeriodSearch.Font")));
			this.btnPeriodSearch.Image = ((System.Drawing.Image)(resources.GetObject("btnPeriodSearch.Image")));
			this.btnPeriodSearch.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnPeriodSearch.ImageAlign")));
			this.btnPeriodSearch.ImageIndex = ((int)(resources.GetObject("btnPeriodSearch.ImageIndex")));
			this.btnPeriodSearch.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnPeriodSearch.ImeMode")));
			this.btnPeriodSearch.Location = ((System.Drawing.Point)(resources.GetObject("btnPeriodSearch.Location")));
			this.btnPeriodSearch.Name = "btnPeriodSearch";
			this.btnPeriodSearch.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnPeriodSearch.RightToLeft")));
			this.btnPeriodSearch.Size = ((System.Drawing.Size)(resources.GetObject("btnPeriodSearch.Size")));
			this.btnPeriodSearch.TabIndex = ((int)(resources.GetObject("btnPeriodSearch.TabIndex")));
			this.btnPeriodSearch.Text = resources.GetString("btnPeriodSearch.Text");
			this.btnPeriodSearch.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnPeriodSearch.TextAlign")));
			this.btnPeriodSearch.Visible = ((bool)(resources.GetObject("btnPeriodSearch.Visible")));
			this.btnPeriodSearch.Click += new System.EventHandler(this.btnPeriodSearch_Click);
			// 
			// lblPeriod
			// 
			this.lblPeriod.AccessibleDescription = resources.GetString("lblPeriod.AccessibleDescription");
			this.lblPeriod.AccessibleName = resources.GetString("lblPeriod.AccessibleName");
			this.lblPeriod.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblPeriod.Anchor")));
			this.lblPeriod.AutoSize = ((bool)(resources.GetObject("lblPeriod.AutoSize")));
			this.lblPeriod.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblPeriod.Dock")));
			this.lblPeriod.Enabled = ((bool)(resources.GetObject("lblPeriod.Enabled")));
			this.lblPeriod.Font = ((System.Drawing.Font)(resources.GetObject("lblPeriod.Font")));
			this.lblPeriod.ForeColor = System.Drawing.Color.Maroon;
			this.lblPeriod.Image = ((System.Drawing.Image)(resources.GetObject("lblPeriod.Image")));
			this.lblPeriod.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblPeriod.ImageAlign")));
			this.lblPeriod.ImageIndex = ((int)(resources.GetObject("lblPeriod.ImageIndex")));
			this.lblPeriod.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblPeriod.ImeMode")));
			this.lblPeriod.Location = ((System.Drawing.Point)(resources.GetObject("lblPeriod.Location")));
			this.lblPeriod.Name = "lblPeriod";
			this.lblPeriod.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblPeriod.RightToLeft")));
			this.lblPeriod.Size = ((System.Drawing.Size)(resources.GetObject("lblPeriod.Size")));
			this.lblPeriod.TabIndex = ((int)(resources.GetObject("lblPeriod.TabIndex")));
			this.lblPeriod.Text = resources.GetString("lblPeriod.Text");
			this.lblPeriod.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblPeriod.TextAlign")));
			this.lblPeriod.Visible = ((bool)(resources.GetObject("lblPeriod.Visible")));
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
			// btnRollup
			// 
			this.btnRollup.AccessibleDescription = resources.GetString("btnRollup.AccessibleDescription");
			this.btnRollup.AccessibleName = resources.GetString("btnRollup.AccessibleName");
			this.btnRollup.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnRollup.Anchor")));
			this.btnRollup.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnRollup.BackgroundImage")));
			this.btnRollup.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnRollup.Dock")));
			this.btnRollup.Enabled = ((bool)(resources.GetObject("btnRollup.Enabled")));
			this.btnRollup.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnRollup.FlatStyle")));
			this.btnRollup.Font = ((System.Drawing.Font)(resources.GetObject("btnRollup.Font")));
			this.btnRollup.Image = ((System.Drawing.Image)(resources.GetObject("btnRollup.Image")));
			this.btnRollup.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnRollup.ImageAlign")));
			this.btnRollup.ImageIndex = ((int)(resources.GetObject("btnRollup.ImageIndex")));
			this.btnRollup.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnRollup.ImeMode")));
			this.btnRollup.Location = ((System.Drawing.Point)(resources.GetObject("btnRollup.Location")));
			this.btnRollup.Name = "btnRollup";
			this.btnRollup.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnRollup.RightToLeft")));
			this.btnRollup.Size = ((System.Drawing.Size)(resources.GetObject("btnRollup.Size")));
			this.btnRollup.TabIndex = ((int)(resources.GetObject("btnRollup.TabIndex")));
			this.btnRollup.Text = resources.GetString("btnRollup.Text");
			this.btnRollup.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnRollup.TextAlign")));
			this.btnRollup.Visible = ((bool)(resources.GetObject("btnRollup.Visible")));
			this.btnRollup.Click += new System.EventHandler(this.btnRollup_Click);
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
			this.dgrdData.TabAction = C1.Win.C1TrueDBGrid.TabActionEnum.ColumnNavigation;
			this.dgrdData.TabIndex = ((int)(resources.GetObject("dgrdData.TabIndex")));
			this.dgrdData.Text = resources.GetString("dgrdData.Text");
			this.dgrdData.ViewCaptionWidth = ((int)(resources.GetObject("dgrdData.ViewCaptionWidth")));
			this.dgrdData.ViewColumnWidth = ((int)(resources.GetObject("dgrdData.ViewColumnWidth")));
			this.dgrdData.Visible = ((bool)(resources.GetObject("dgrdData.Visible")));
			this.dgrdData.WrapCellPointer = ((bool)(resources.GetObject("dgrdData.WrapCellPointer")));
			this.dgrdData.AfterDelete += new System.EventHandler(this.dgrdData_AfterDelete);
			this.dgrdData.BeforeColEdit += new C1.Win.C1TrueDBGrid.BeforeColEditEventHandler(this.dgrdData_BeforeColEdit);
			this.dgrdData.Click += new System.EventHandler(this.dgrdData_Click);
			this.dgrdData.ButtonClick += new C1.Win.C1TrueDBGrid.ColEventHandler(this.dgrdData_ButtonClick);
			this.dgrdData.BeforeColUpdate += new C1.Win.C1TrueDBGrid.BeforeColUpdateEventHandler(this.dgrdData_BeforeColUpdate);
			this.dgrdData.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgrdData_KeyDown);
			this.dgrdData.OnAddNew += new System.EventHandler(this.dgrdData_OnAddNew);
			this.dgrdData.PropBag = resources.GetString("dgrdData.PropBag");
			// 
			// lblToDate
			// 
			this.lblToDate.AccessibleDescription = resources.GetString("lblToDate.AccessibleDescription");
			this.lblToDate.AccessibleName = resources.GetString("lblToDate.AccessibleName");
			this.lblToDate.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblToDate.Anchor")));
			this.lblToDate.AutoSize = ((bool)(resources.GetObject("lblToDate.AutoSize")));
			this.lblToDate.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblToDate.Dock")));
			this.lblToDate.Enabled = ((bool)(resources.GetObject("lblToDate.Enabled")));
			this.lblToDate.Font = ((System.Drawing.Font)(resources.GetObject("lblToDate.Font")));
			this.lblToDate.ForeColor = System.Drawing.Color.Maroon;
			this.lblToDate.Image = ((System.Drawing.Image)(resources.GetObject("lblToDate.Image")));
			this.lblToDate.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblToDate.ImageAlign")));
			this.lblToDate.ImageIndex = ((int)(resources.GetObject("lblToDate.ImageIndex")));
			this.lblToDate.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblToDate.ImeMode")));
			this.lblToDate.Location = ((System.Drawing.Point)(resources.GetObject("lblToDate.Location")));
			this.lblToDate.Name = "lblToDate";
			this.lblToDate.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblToDate.RightToLeft")));
			this.lblToDate.Size = ((System.Drawing.Size)(resources.GetObject("lblToDate.Size")));
			this.lblToDate.TabIndex = ((int)(resources.GetObject("lblToDate.TabIndex")));
			this.lblToDate.Text = resources.GetString("lblToDate.Text");
			this.lblToDate.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblToDate.TextAlign")));
			this.lblToDate.Visible = ((bool)(resources.GetObject("lblToDate.Visible")));
			// 
			// lblFromDate
			// 
			this.lblFromDate.AccessibleDescription = resources.GetString("lblFromDate.AccessibleDescription");
			this.lblFromDate.AccessibleName = resources.GetString("lblFromDate.AccessibleName");
			this.lblFromDate.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblFromDate.Anchor")));
			this.lblFromDate.AutoSize = ((bool)(resources.GetObject("lblFromDate.AutoSize")));
			this.lblFromDate.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblFromDate.Dock")));
			this.lblFromDate.Enabled = ((bool)(resources.GetObject("lblFromDate.Enabled")));
			this.lblFromDate.Font = ((System.Drawing.Font)(resources.GetObject("lblFromDate.Font")));
			this.lblFromDate.ForeColor = System.Drawing.Color.Maroon;
			this.lblFromDate.Image = ((System.Drawing.Image)(resources.GetObject("lblFromDate.Image")));
			this.lblFromDate.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblFromDate.ImageAlign")));
			this.lblFromDate.ImageIndex = ((int)(resources.GetObject("lblFromDate.ImageIndex")));
			this.lblFromDate.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblFromDate.ImeMode")));
			this.lblFromDate.Location = ((System.Drawing.Point)(resources.GetObject("lblFromDate.Location")));
			this.lblFromDate.Name = "lblFromDate";
			this.lblFromDate.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblFromDate.RightToLeft")));
			this.lblFromDate.Size = ((System.Drawing.Size)(resources.GetObject("lblFromDate.Size")));
			this.lblFromDate.TabIndex = ((int)(resources.GetObject("lblFromDate.TabIndex")));
			this.lblFromDate.Text = resources.GetString("lblFromDate.Text");
			this.lblFromDate.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblFromDate.TextAlign")));
			this.lblFromDate.Visible = ((bool)(resources.GetObject("lblFromDate.Visible")));
			// 
			// dtmToDate
			// 
			this.dtmToDate.AcceptsEscape = ((bool)(resources.GetObject("dtmToDate.AcceptsEscape")));
			this.dtmToDate.AccessibleDescription = resources.GetString("dtmToDate.AccessibleDescription");
			this.dtmToDate.AccessibleName = resources.GetString("dtmToDate.AccessibleName");
			this.dtmToDate.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("dtmToDate.Anchor")));
			this.dtmToDate.AutoSize = ((bool)(resources.GetObject("dtmToDate.AutoSize")));
			this.dtmToDate.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("dtmToDate.BackgroundImage")));
			this.dtmToDate.BorderStyle = ((System.Windows.Forms.BorderStyle)(resources.GetObject("dtmToDate.BorderStyle")));
			// 
			// dtmToDate.Calendar
			// 
			this.dtmToDate.Calendar.AccessibleDescription = resources.GetString("dtmToDate.Calendar.AccessibleDescription");
			this.dtmToDate.Calendar.AccessibleName = resources.GetString("dtmToDate.Calendar.AccessibleName");
			this.dtmToDate.Calendar.AnnuallyBoldedDates = ((System.DateTime[])(resources.GetObject("dtmToDate.Calendar.AnnuallyBoldedDates")));
			this.dtmToDate.Calendar.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("dtmToDate.Calendar.BackgroundImage")));
			this.dtmToDate.Calendar.BoldedDates = ((System.DateTime[])(resources.GetObject("dtmToDate.Calendar.BoldedDates")));
			this.dtmToDate.Calendar.CalendarDimensions = ((System.Drawing.Size)(resources.GetObject("dtmToDate.Calendar.CalendarDimensions")));
			this.dtmToDate.Calendar.Enabled = ((bool)(resources.GetObject("dtmToDate.Calendar.Enabled")));
			this.dtmToDate.Calendar.FirstDayOfWeek = ((System.Windows.Forms.Day)(resources.GetObject("dtmToDate.Calendar.FirstDayOfWeek")));
			this.dtmToDate.Calendar.Font = ((System.Drawing.Font)(resources.GetObject("dtmToDate.Calendar.Font")));
			this.dtmToDate.Calendar.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("dtmToDate.Calendar.ImeMode")));
			this.dtmToDate.Calendar.MonthlyBoldedDates = ((System.DateTime[])(resources.GetObject("dtmToDate.Calendar.MonthlyBoldedDates")));
			this.dtmToDate.Calendar.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("dtmToDate.Calendar.RightToLeft")));
			this.dtmToDate.Calendar.ShowClearButton = ((bool)(resources.GetObject("dtmToDate.Calendar.ShowClearButton")));
			this.dtmToDate.Calendar.ShowTodayButton = ((bool)(resources.GetObject("dtmToDate.Calendar.ShowTodayButton")));
			this.dtmToDate.Calendar.ShowWeekNumbers = ((bool)(resources.GetObject("dtmToDate.Calendar.ShowWeekNumbers")));
			this.dtmToDate.CaseSensitive = ((bool)(resources.GetObject("dtmToDate.CaseSensitive")));
			this.dtmToDate.Culture = ((int)(resources.GetObject("dtmToDate.Culture")));
			this.dtmToDate.CurrentTimeZone = ((bool)(resources.GetObject("dtmToDate.CurrentTimeZone")));
			this.dtmToDate.CustomFormat = resources.GetString("dtmToDate.CustomFormat");
			this.dtmToDate.DaylightTimeAdjustment = ((C1.Win.C1Input.DaylightTimeAdjustmentEnum)(resources.GetObject("dtmToDate.DaylightTimeAdjustment")));
			this.dtmToDate.DisplayFormat.CustomFormat = resources.GetString("dtmToDate.DisplayFormat.CustomFormat");
			this.dtmToDate.DisplayFormat.FormatType = ((C1.Win.C1Input.FormatTypeEnum)(resources.GetObject("dtmToDate.DisplayFormat.FormatType")));
			this.dtmToDate.DisplayFormat.Inherit = ((C1.Win.C1Input.FormatInfoInheritFlags)(resources.GetObject("dtmToDate.DisplayFormat.Inherit")));
			this.dtmToDate.DisplayFormat.NullText = resources.GetString("dtmToDate.DisplayFormat.NullText");
			this.dtmToDate.DisplayFormat.TrimEnd = ((bool)(resources.GetObject("dtmToDate.DisplayFormat.TrimEnd")));
			this.dtmToDate.DisplayFormat.TrimStart = ((bool)(resources.GetObject("dtmToDate.DisplayFormat.TrimStart")));
			this.dtmToDate.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("dtmToDate.Dock")));
			this.dtmToDate.DropDownFormAlign = ((C1.Win.C1Input.DropDownFormAlignmentEnum)(resources.GetObject("dtmToDate.DropDownFormAlign")));
			this.dtmToDate.EditFormat.CustomFormat = resources.GetString("dtmToDate.EditFormat.CustomFormat");
			this.dtmToDate.EditFormat.FormatType = ((C1.Win.C1Input.FormatTypeEnum)(resources.GetObject("dtmToDate.EditFormat.FormatType")));
			this.dtmToDate.EditFormat.Inherit = ((C1.Win.C1Input.FormatInfoInheritFlags)(resources.GetObject("dtmToDate.EditFormat.Inherit")));
			this.dtmToDate.EditFormat.NullText = resources.GetString("dtmToDate.EditFormat.NullText");
			this.dtmToDate.EditFormat.TrimEnd = ((bool)(resources.GetObject("dtmToDate.EditFormat.TrimEnd")));
			this.dtmToDate.EditFormat.TrimStart = ((bool)(resources.GetObject("dtmToDate.EditFormat.TrimStart")));
			this.dtmToDate.EditMask = resources.GetString("dtmToDate.EditMask");
			this.dtmToDate.EmptyAsNull = ((bool)(resources.GetObject("dtmToDate.EmptyAsNull")));
			this.dtmToDate.Enabled = ((bool)(resources.GetObject("dtmToDate.Enabled")));
			this.dtmToDate.ErrorInfo.BeepOnError = ((bool)(resources.GetObject("dtmToDate.ErrorInfo.BeepOnError")));
			this.dtmToDate.ErrorInfo.ErrorMessage = resources.GetString("dtmToDate.ErrorInfo.ErrorMessage");
			this.dtmToDate.ErrorInfo.ErrorMessageCaption = resources.GetString("dtmToDate.ErrorInfo.ErrorMessageCaption");
			this.dtmToDate.ErrorInfo.ShowErrorMessage = ((bool)(resources.GetObject("dtmToDate.ErrorInfo.ShowErrorMessage")));
			this.dtmToDate.ErrorInfo.ValueOnError = ((object)(resources.GetObject("dtmToDate.ErrorInfo.ValueOnError")));
			this.dtmToDate.Font = ((System.Drawing.Font)(resources.GetObject("dtmToDate.Font")));
			this.dtmToDate.FormatType = ((C1.Win.C1Input.FormatTypeEnum)(resources.GetObject("dtmToDate.FormatType")));
			this.dtmToDate.GapHeight = ((int)(resources.GetObject("dtmToDate.GapHeight")));
			this.dtmToDate.GMTOffset = ((System.TimeSpan)(resources.GetObject("dtmToDate.GMTOffset")));
			this.dtmToDate.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("dtmToDate.ImeMode")));
			this.dtmToDate.InitialSelection = ((C1.Win.C1Input.InitialSelectionEnum)(resources.GetObject("dtmToDate.InitialSelection")));
			this.dtmToDate.Location = ((System.Drawing.Point)(resources.GetObject("dtmToDate.Location")));
			this.dtmToDate.MaskInfo.AutoTabWhenFilled = ((bool)(resources.GetObject("dtmToDate.MaskInfo.AutoTabWhenFilled")));
			this.dtmToDate.MaskInfo.CaseSensitive = ((bool)(resources.GetObject("dtmToDate.MaskInfo.CaseSensitive")));
			this.dtmToDate.MaskInfo.CopyWithLiterals = ((bool)(resources.GetObject("dtmToDate.MaskInfo.CopyWithLiterals")));
			this.dtmToDate.MaskInfo.EditMask = resources.GetString("dtmToDate.MaskInfo.EditMask");
			this.dtmToDate.MaskInfo.EmptyAsNull = ((bool)(resources.GetObject("dtmToDate.MaskInfo.EmptyAsNull")));
			this.dtmToDate.MaskInfo.ErrorMessage = resources.GetString("dtmToDate.MaskInfo.ErrorMessage");
			this.dtmToDate.MaskInfo.Inherit = ((C1.Win.C1Input.MaskInfoInheritFlags)(resources.GetObject("dtmToDate.MaskInfo.Inherit")));
			this.dtmToDate.MaskInfo.PromptChar = ((char)(resources.GetObject("dtmToDate.MaskInfo.PromptChar")));
			this.dtmToDate.MaskInfo.ShowLiterals = ((C1.Win.C1Input.ShowLiteralsEnum)(resources.GetObject("dtmToDate.MaskInfo.ShowLiterals")));
			this.dtmToDate.MaskInfo.StoredEmptyChar = ((char)(resources.GetObject("dtmToDate.MaskInfo.StoredEmptyChar")));
			this.dtmToDate.MaxLength = ((int)(resources.GetObject("dtmToDate.MaxLength")));
			this.dtmToDate.Name = "dtmToDate";
			this.dtmToDate.NullText = resources.GetString("dtmToDate.NullText");
			this.dtmToDate.ParseInfo.CaseSensitive = ((bool)(resources.GetObject("dtmToDate.ParseInfo.CaseSensitive")));
			this.dtmToDate.ParseInfo.CustomFormat = resources.GetString("dtmToDate.ParseInfo.CustomFormat");
			this.dtmToDate.ParseInfo.DateTimeStyle = ((C1.Win.C1Input.DateTimeStyleFlags)(resources.GetObject("dtmToDate.ParseInfo.DateTimeStyle")));
			this.dtmToDate.ParseInfo.EmptyAsNull = ((bool)(resources.GetObject("dtmToDate.ParseInfo.EmptyAsNull")));
			this.dtmToDate.ParseInfo.ErrorMessage = resources.GetString("dtmToDate.ParseInfo.ErrorMessage");
			this.dtmToDate.ParseInfo.FormatType = ((C1.Win.C1Input.FormatTypeEnum)(resources.GetObject("dtmToDate.ParseInfo.FormatType")));
			this.dtmToDate.ParseInfo.Inherit = ((C1.Win.C1Input.ParseInfoInheritFlags)(resources.GetObject("dtmToDate.ParseInfo.Inherit")));
			this.dtmToDate.ParseInfo.NullText = resources.GetString("dtmToDate.ParseInfo.NullText");
			this.dtmToDate.ParseInfo.TrimEnd = ((bool)(resources.GetObject("dtmToDate.ParseInfo.TrimEnd")));
			this.dtmToDate.ParseInfo.TrimStart = ((bool)(resources.GetObject("dtmToDate.ParseInfo.TrimStart")));
			this.dtmToDate.PasswordChar = ((char)(resources.GetObject("dtmToDate.PasswordChar")));
			this.dtmToDate.PostValidation.CaseSensitive = ((bool)(resources.GetObject("dtmToDate.PostValidation.CaseSensitive")));
			this.dtmToDate.PostValidation.ErrorMessage = resources.GetString("dtmToDate.PostValidation.ErrorMessage");
			this.dtmToDate.PostValidation.Inherit = ((C1.Win.C1Input.PostValidationInheritFlags)(resources.GetObject("dtmToDate.PostValidation.Inherit")));
			this.dtmToDate.PostValidation.Validation = ((C1.Win.C1Input.PostValidationTypeEnum)(resources.GetObject("dtmToDate.PostValidation.Validation")));
			this.dtmToDate.PostValidation.Values = ((System.Array)(resources.GetObject("dtmToDate.PostValidation.Values")));
			this.dtmToDate.PostValidation.ValuesExcluded = ((System.Array)(resources.GetObject("dtmToDate.PostValidation.ValuesExcluded")));
			this.dtmToDate.PreValidation.CaseSensitive = ((bool)(resources.GetObject("dtmToDate.PreValidation.CaseSensitive")));
			this.dtmToDate.PreValidation.ErrorMessage = resources.GetString("dtmToDate.PreValidation.ErrorMessage");
			this.dtmToDate.PreValidation.Inherit = ((C1.Win.C1Input.PreValidationInheritFlags)(resources.GetObject("dtmToDate.PreValidation.Inherit")));
			this.dtmToDate.PreValidation.ItemSeparator = resources.GetString("dtmToDate.PreValidation.ItemSeparator");
			this.dtmToDate.PreValidation.PatternString = resources.GetString("dtmToDate.PreValidation.PatternString");
			this.dtmToDate.PreValidation.RegexOptions = ((C1.Win.C1Input.RegexOptionFlags)(resources.GetObject("dtmToDate.PreValidation.RegexOptions")));
			this.dtmToDate.PreValidation.TrimEnd = ((bool)(resources.GetObject("dtmToDate.PreValidation.TrimEnd")));
			this.dtmToDate.PreValidation.TrimStart = ((bool)(resources.GetObject("dtmToDate.PreValidation.TrimStart")));
			this.dtmToDate.PreValidation.Validation = ((C1.Win.C1Input.PreValidationTypeEnum)(resources.GetObject("dtmToDate.PreValidation.Validation")));
			this.dtmToDate.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("dtmToDate.RightToLeft")));
			this.dtmToDate.ShowFocusRectangle = ((bool)(resources.GetObject("dtmToDate.ShowFocusRectangle")));
			this.dtmToDate.Size = ((System.Drawing.Size)(resources.GetObject("dtmToDate.Size")));
			this.dtmToDate.TabIndex = ((int)(resources.GetObject("dtmToDate.TabIndex")));
			this.dtmToDate.Tag = ((object)(resources.GetObject("dtmToDate.Tag")));
			this.dtmToDate.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("dtmToDate.TextAlign")));
			this.dtmToDate.TrimEnd = ((bool)(resources.GetObject("dtmToDate.TrimEnd")));
			this.dtmToDate.TrimStart = ((bool)(resources.GetObject("dtmToDate.TrimStart")));
			this.dtmToDate.UserCultureOverride = ((bool)(resources.GetObject("dtmToDate.UserCultureOverride")));
			this.dtmToDate.Value = ((object)(resources.GetObject("dtmToDate.Value")));
			this.dtmToDate.VerticalAlign = ((C1.Win.C1Input.VerticalAlignEnum)(resources.GetObject("dtmToDate.VerticalAlign")));
			this.dtmToDate.Visible = ((bool)(resources.GetObject("dtmToDate.Visible")));
			this.dtmToDate.VisibleButtons = ((C1.Win.C1Input.DropDownControlButtonFlags)(resources.GetObject("dtmToDate.VisibleButtons")));
			// 
			// dtmFromDate
			// 
			this.dtmFromDate.AcceptsEscape = ((bool)(resources.GetObject("dtmFromDate.AcceptsEscape")));
			this.dtmFromDate.AccessibleDescription = resources.GetString("dtmFromDate.AccessibleDescription");
			this.dtmFromDate.AccessibleName = resources.GetString("dtmFromDate.AccessibleName");
			this.dtmFromDate.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("dtmFromDate.Anchor")));
			this.dtmFromDate.AutoSize = ((bool)(resources.GetObject("dtmFromDate.AutoSize")));
			this.dtmFromDate.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("dtmFromDate.BackgroundImage")));
			this.dtmFromDate.BorderStyle = ((System.Windows.Forms.BorderStyle)(resources.GetObject("dtmFromDate.BorderStyle")));
			// 
			// dtmFromDate.Calendar
			// 
			this.dtmFromDate.Calendar.AccessibleDescription = resources.GetString("dtmFromDate.Calendar.AccessibleDescription");
			this.dtmFromDate.Calendar.AccessibleName = resources.GetString("dtmFromDate.Calendar.AccessibleName");
			this.dtmFromDate.Calendar.AnnuallyBoldedDates = ((System.DateTime[])(resources.GetObject("dtmFromDate.Calendar.AnnuallyBoldedDates")));
			this.dtmFromDate.Calendar.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("dtmFromDate.Calendar.BackgroundImage")));
			this.dtmFromDate.Calendar.BoldedDates = ((System.DateTime[])(resources.GetObject("dtmFromDate.Calendar.BoldedDates")));
			this.dtmFromDate.Calendar.CalendarDimensions = ((System.Drawing.Size)(resources.GetObject("dtmFromDate.Calendar.CalendarDimensions")));
			this.dtmFromDate.Calendar.Enabled = ((bool)(resources.GetObject("dtmFromDate.Calendar.Enabled")));
			this.dtmFromDate.Calendar.FirstDayOfWeek = ((System.Windows.Forms.Day)(resources.GetObject("dtmFromDate.Calendar.FirstDayOfWeek")));
			this.dtmFromDate.Calendar.Font = ((System.Drawing.Font)(resources.GetObject("dtmFromDate.Calendar.Font")));
			this.dtmFromDate.Calendar.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("dtmFromDate.Calendar.ImeMode")));
			this.dtmFromDate.Calendar.MonthlyBoldedDates = ((System.DateTime[])(resources.GetObject("dtmFromDate.Calendar.MonthlyBoldedDates")));
			this.dtmFromDate.Calendar.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("dtmFromDate.Calendar.RightToLeft")));
			this.dtmFromDate.Calendar.ShowClearButton = ((bool)(resources.GetObject("dtmFromDate.Calendar.ShowClearButton")));
			this.dtmFromDate.Calendar.ShowTodayButton = ((bool)(resources.GetObject("dtmFromDate.Calendar.ShowTodayButton")));
			this.dtmFromDate.Calendar.ShowWeekNumbers = ((bool)(resources.GetObject("dtmFromDate.Calendar.ShowWeekNumbers")));
			this.dtmFromDate.CaseSensitive = ((bool)(resources.GetObject("dtmFromDate.CaseSensitive")));
			this.dtmFromDate.Culture = ((int)(resources.GetObject("dtmFromDate.Culture")));
			this.dtmFromDate.CurrentTimeZone = ((bool)(resources.GetObject("dtmFromDate.CurrentTimeZone")));
			this.dtmFromDate.CustomFormat = resources.GetString("dtmFromDate.CustomFormat");
			this.dtmFromDate.DaylightTimeAdjustment = ((C1.Win.C1Input.DaylightTimeAdjustmentEnum)(resources.GetObject("dtmFromDate.DaylightTimeAdjustment")));
			this.dtmFromDate.DisplayFormat.CustomFormat = resources.GetString("dtmFromDate.DisplayFormat.CustomFormat");
			this.dtmFromDate.DisplayFormat.FormatType = ((C1.Win.C1Input.FormatTypeEnum)(resources.GetObject("dtmFromDate.DisplayFormat.FormatType")));
			this.dtmFromDate.DisplayFormat.Inherit = ((C1.Win.C1Input.FormatInfoInheritFlags)(resources.GetObject("dtmFromDate.DisplayFormat.Inherit")));
			this.dtmFromDate.DisplayFormat.NullText = resources.GetString("dtmFromDate.DisplayFormat.NullText");
			this.dtmFromDate.DisplayFormat.TrimEnd = ((bool)(resources.GetObject("dtmFromDate.DisplayFormat.TrimEnd")));
			this.dtmFromDate.DisplayFormat.TrimStart = ((bool)(resources.GetObject("dtmFromDate.DisplayFormat.TrimStart")));
			this.dtmFromDate.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("dtmFromDate.Dock")));
			this.dtmFromDate.DropDownFormAlign = ((C1.Win.C1Input.DropDownFormAlignmentEnum)(resources.GetObject("dtmFromDate.DropDownFormAlign")));
			this.dtmFromDate.EditFormat.CustomFormat = resources.GetString("dtmFromDate.EditFormat.CustomFormat");
			this.dtmFromDate.EditFormat.FormatType = ((C1.Win.C1Input.FormatTypeEnum)(resources.GetObject("dtmFromDate.EditFormat.FormatType")));
			this.dtmFromDate.EditFormat.Inherit = ((C1.Win.C1Input.FormatInfoInheritFlags)(resources.GetObject("dtmFromDate.EditFormat.Inherit")));
			this.dtmFromDate.EditFormat.NullText = resources.GetString("dtmFromDate.EditFormat.NullText");
			this.dtmFromDate.EditFormat.TrimEnd = ((bool)(resources.GetObject("dtmFromDate.EditFormat.TrimEnd")));
			this.dtmFromDate.EditFormat.TrimStart = ((bool)(resources.GetObject("dtmFromDate.EditFormat.TrimStart")));
			this.dtmFromDate.EditMask = resources.GetString("dtmFromDate.EditMask");
			this.dtmFromDate.EmptyAsNull = ((bool)(resources.GetObject("dtmFromDate.EmptyAsNull")));
			this.dtmFromDate.Enabled = ((bool)(resources.GetObject("dtmFromDate.Enabled")));
			this.dtmFromDate.ErrorInfo.BeepOnError = ((bool)(resources.GetObject("dtmFromDate.ErrorInfo.BeepOnError")));
			this.dtmFromDate.ErrorInfo.ErrorMessage = resources.GetString("dtmFromDate.ErrorInfo.ErrorMessage");
			this.dtmFromDate.ErrorInfo.ErrorMessageCaption = resources.GetString("dtmFromDate.ErrorInfo.ErrorMessageCaption");
			this.dtmFromDate.ErrorInfo.ShowErrorMessage = ((bool)(resources.GetObject("dtmFromDate.ErrorInfo.ShowErrorMessage")));
			this.dtmFromDate.ErrorInfo.ValueOnError = ((object)(resources.GetObject("dtmFromDate.ErrorInfo.ValueOnError")));
			this.dtmFromDate.Font = ((System.Drawing.Font)(resources.GetObject("dtmFromDate.Font")));
			this.dtmFromDate.FormatType = ((C1.Win.C1Input.FormatTypeEnum)(resources.GetObject("dtmFromDate.FormatType")));
			this.dtmFromDate.GapHeight = ((int)(resources.GetObject("dtmFromDate.GapHeight")));
			this.dtmFromDate.GMTOffset = ((System.TimeSpan)(resources.GetObject("dtmFromDate.GMTOffset")));
			this.dtmFromDate.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("dtmFromDate.ImeMode")));
			this.dtmFromDate.InitialSelection = ((C1.Win.C1Input.InitialSelectionEnum)(resources.GetObject("dtmFromDate.InitialSelection")));
			this.dtmFromDate.Location = ((System.Drawing.Point)(resources.GetObject("dtmFromDate.Location")));
			this.dtmFromDate.MaskInfo.AutoTabWhenFilled = ((bool)(resources.GetObject("dtmFromDate.MaskInfo.AutoTabWhenFilled")));
			this.dtmFromDate.MaskInfo.CaseSensitive = ((bool)(resources.GetObject("dtmFromDate.MaskInfo.CaseSensitive")));
			this.dtmFromDate.MaskInfo.CopyWithLiterals = ((bool)(resources.GetObject("dtmFromDate.MaskInfo.CopyWithLiterals")));
			this.dtmFromDate.MaskInfo.EditMask = resources.GetString("dtmFromDate.MaskInfo.EditMask");
			this.dtmFromDate.MaskInfo.EmptyAsNull = ((bool)(resources.GetObject("dtmFromDate.MaskInfo.EmptyAsNull")));
			this.dtmFromDate.MaskInfo.ErrorMessage = resources.GetString("dtmFromDate.MaskInfo.ErrorMessage");
			this.dtmFromDate.MaskInfo.Inherit = ((C1.Win.C1Input.MaskInfoInheritFlags)(resources.GetObject("dtmFromDate.MaskInfo.Inherit")));
			this.dtmFromDate.MaskInfo.PromptChar = ((char)(resources.GetObject("dtmFromDate.MaskInfo.PromptChar")));
			this.dtmFromDate.MaskInfo.ShowLiterals = ((C1.Win.C1Input.ShowLiteralsEnum)(resources.GetObject("dtmFromDate.MaskInfo.ShowLiterals")));
			this.dtmFromDate.MaskInfo.StoredEmptyChar = ((char)(resources.GetObject("dtmFromDate.MaskInfo.StoredEmptyChar")));
			this.dtmFromDate.MaxLength = ((int)(resources.GetObject("dtmFromDate.MaxLength")));
			this.dtmFromDate.Name = "dtmFromDate";
			this.dtmFromDate.NullText = resources.GetString("dtmFromDate.NullText");
			this.dtmFromDate.ParseInfo.CaseSensitive = ((bool)(resources.GetObject("dtmFromDate.ParseInfo.CaseSensitive")));
			this.dtmFromDate.ParseInfo.CustomFormat = resources.GetString("dtmFromDate.ParseInfo.CustomFormat");
			this.dtmFromDate.ParseInfo.DateTimeStyle = ((C1.Win.C1Input.DateTimeStyleFlags)(resources.GetObject("dtmFromDate.ParseInfo.DateTimeStyle")));
			this.dtmFromDate.ParseInfo.EmptyAsNull = ((bool)(resources.GetObject("dtmFromDate.ParseInfo.EmptyAsNull")));
			this.dtmFromDate.ParseInfo.ErrorMessage = resources.GetString("dtmFromDate.ParseInfo.ErrorMessage");
			this.dtmFromDate.ParseInfo.FormatType = ((C1.Win.C1Input.FormatTypeEnum)(resources.GetObject("dtmFromDate.ParseInfo.FormatType")));
			this.dtmFromDate.ParseInfo.Inherit = ((C1.Win.C1Input.ParseInfoInheritFlags)(resources.GetObject("dtmFromDate.ParseInfo.Inherit")));
			this.dtmFromDate.ParseInfo.NullText = resources.GetString("dtmFromDate.ParseInfo.NullText");
			this.dtmFromDate.ParseInfo.TrimEnd = ((bool)(resources.GetObject("dtmFromDate.ParseInfo.TrimEnd")));
			this.dtmFromDate.ParseInfo.TrimStart = ((bool)(resources.GetObject("dtmFromDate.ParseInfo.TrimStart")));
			this.dtmFromDate.PasswordChar = ((char)(resources.GetObject("dtmFromDate.PasswordChar")));
			this.dtmFromDate.PostValidation.CaseSensitive = ((bool)(resources.GetObject("dtmFromDate.PostValidation.CaseSensitive")));
			this.dtmFromDate.PostValidation.ErrorMessage = resources.GetString("dtmFromDate.PostValidation.ErrorMessage");
			this.dtmFromDate.PostValidation.Inherit = ((C1.Win.C1Input.PostValidationInheritFlags)(resources.GetObject("dtmFromDate.PostValidation.Inherit")));
			this.dtmFromDate.PostValidation.Validation = ((C1.Win.C1Input.PostValidationTypeEnum)(resources.GetObject("dtmFromDate.PostValidation.Validation")));
			this.dtmFromDate.PostValidation.Values = ((System.Array)(resources.GetObject("dtmFromDate.PostValidation.Values")));
			this.dtmFromDate.PostValidation.ValuesExcluded = ((System.Array)(resources.GetObject("dtmFromDate.PostValidation.ValuesExcluded")));
			this.dtmFromDate.PreValidation.CaseSensitive = ((bool)(resources.GetObject("dtmFromDate.PreValidation.CaseSensitive")));
			this.dtmFromDate.PreValidation.ErrorMessage = resources.GetString("dtmFromDate.PreValidation.ErrorMessage");
			this.dtmFromDate.PreValidation.Inherit = ((C1.Win.C1Input.PreValidationInheritFlags)(resources.GetObject("dtmFromDate.PreValidation.Inherit")));
			this.dtmFromDate.PreValidation.ItemSeparator = resources.GetString("dtmFromDate.PreValidation.ItemSeparator");
			this.dtmFromDate.PreValidation.PatternString = resources.GetString("dtmFromDate.PreValidation.PatternString");
			this.dtmFromDate.PreValidation.RegexOptions = ((C1.Win.C1Input.RegexOptionFlags)(resources.GetObject("dtmFromDate.PreValidation.RegexOptions")));
			this.dtmFromDate.PreValidation.TrimEnd = ((bool)(resources.GetObject("dtmFromDate.PreValidation.TrimEnd")));
			this.dtmFromDate.PreValidation.TrimStart = ((bool)(resources.GetObject("dtmFromDate.PreValidation.TrimStart")));
			this.dtmFromDate.PreValidation.Validation = ((C1.Win.C1Input.PreValidationTypeEnum)(resources.GetObject("dtmFromDate.PreValidation.Validation")));
			this.dtmFromDate.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("dtmFromDate.RightToLeft")));
			this.dtmFromDate.ShowFocusRectangle = ((bool)(resources.GetObject("dtmFromDate.ShowFocusRectangle")));
			this.dtmFromDate.Size = ((System.Drawing.Size)(resources.GetObject("dtmFromDate.Size")));
			this.dtmFromDate.TabIndex = ((int)(resources.GetObject("dtmFromDate.TabIndex")));
			this.dtmFromDate.Tag = ((object)(resources.GetObject("dtmFromDate.Tag")));
			this.dtmFromDate.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("dtmFromDate.TextAlign")));
			this.dtmFromDate.TrimEnd = ((bool)(resources.GetObject("dtmFromDate.TrimEnd")));
			this.dtmFromDate.TrimStart = ((bool)(resources.GetObject("dtmFromDate.TrimStart")));
			this.dtmFromDate.UserCultureOverride = ((bool)(resources.GetObject("dtmFromDate.UserCultureOverride")));
			this.dtmFromDate.Value = ((object)(resources.GetObject("dtmFromDate.Value")));
			this.dtmFromDate.VerticalAlign = ((C1.Win.C1Input.VerticalAlignEnum)(resources.GetObject("dtmFromDate.VerticalAlign")));
			this.dtmFromDate.Visible = ((bool)(resources.GetObject("dtmFromDate.Visible")));
			this.dtmFromDate.VisibleButtons = ((C1.Win.C1Input.DropDownControlButtonFlags)(resources.GetObject("dtmFromDate.VisibleButtons")));
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
			// btnAllocate
			// 
			this.btnAllocate.AccessibleDescription = resources.GetString("btnAllocate.AccessibleDescription");
			this.btnAllocate.AccessibleName = resources.GetString("btnAllocate.AccessibleName");
			this.btnAllocate.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnAllocate.Anchor")));
			this.btnAllocate.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnAllocate.BackgroundImage")));
			this.btnAllocate.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnAllocate.Dock")));
			this.btnAllocate.Enabled = ((bool)(resources.GetObject("btnAllocate.Enabled")));
			this.btnAllocate.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnAllocate.FlatStyle")));
			this.btnAllocate.Font = ((System.Drawing.Font)(resources.GetObject("btnAllocate.Font")));
			this.btnAllocate.Image = ((System.Drawing.Image)(resources.GetObject("btnAllocate.Image")));
			this.btnAllocate.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnAllocate.ImageAlign")));
			this.btnAllocate.ImageIndex = ((int)(resources.GetObject("btnAllocate.ImageIndex")));
			this.btnAllocate.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnAllocate.ImeMode")));
			this.btnAllocate.Location = ((System.Drawing.Point)(resources.GetObject("btnAllocate.Location")));
			this.btnAllocate.Name = "btnAllocate";
			this.btnAllocate.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnAllocate.RightToLeft")));
			this.btnAllocate.Size = ((System.Drawing.Size)(resources.GetObject("btnAllocate.Size")));
			this.btnAllocate.TabIndex = ((int)(resources.GetObject("btnAllocate.TabIndex")));
			this.btnAllocate.Text = resources.GetString("btnAllocate.Text");
			this.btnAllocate.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnAllocate.TextAlign")));
			this.btnAllocate.Visible = ((bool)(resources.GetObject("btnAllocate.Visible")));
			this.btnAllocate.Click += new System.EventHandler(this.btnAllocate_Click);
			// 
			// btnPrint
			// 
			this.btnPrint.AccessibleDescription = resources.GetString("btnPrint.AccessibleDescription");
			this.btnPrint.AccessibleName = resources.GetString("btnPrint.AccessibleName");
			this.btnPrint.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnPrint.Anchor")));
			this.btnPrint.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnPrint.BackgroundImage")));
			this.btnPrint.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnPrint.Dock")));
			this.btnPrint.Enabled = ((bool)(resources.GetObject("btnPrint.Enabled")));
			this.btnPrint.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnPrint.FlatStyle")));
			this.btnPrint.Font = ((System.Drawing.Font)(resources.GetObject("btnPrint.Font")));
			this.btnPrint.Image = ((System.Drawing.Image)(resources.GetObject("btnPrint.Image")));
			this.btnPrint.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnPrint.ImageAlign")));
			this.btnPrint.ImageIndex = ((int)(resources.GetObject("btnPrint.ImageIndex")));
			this.btnPrint.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnPrint.ImeMode")));
			this.btnPrint.Location = ((System.Drawing.Point)(resources.GetObject("btnPrint.Location")));
			this.btnPrint.Name = "btnPrint";
			this.btnPrint.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnPrint.RightToLeft")));
			this.btnPrint.Size = ((System.Drawing.Size)(resources.GetObject("btnPrint.Size")));
			this.btnPrint.TabIndex = ((int)(resources.GetObject("btnPrint.TabIndex")));
			this.btnPrint.Text = resources.GetString("btnPrint.Text");
			this.btnPrint.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnPrint.TextAlign")));
			this.btnPrint.Visible = ((bool)(resources.GetObject("btnPrint.Visible")));
			this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
			// 
			// dtmRollupDate
			// 
			this.dtmRollupDate.AcceptsEscape = ((bool)(resources.GetObject("dtmRollupDate.AcceptsEscape")));
			this.dtmRollupDate.AccessibleDescription = resources.GetString("dtmRollupDate.AccessibleDescription");
			this.dtmRollupDate.AccessibleName = resources.GetString("dtmRollupDate.AccessibleName");
			this.dtmRollupDate.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("dtmRollupDate.Anchor")));
			this.dtmRollupDate.AutoSize = ((bool)(resources.GetObject("dtmRollupDate.AutoSize")));
			this.dtmRollupDate.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("dtmRollupDate.BackgroundImage")));
			this.dtmRollupDate.BorderStyle = ((System.Windows.Forms.BorderStyle)(resources.GetObject("dtmRollupDate.BorderStyle")));
			// 
			// dtmRollupDate.Calendar
			// 
			this.dtmRollupDate.Calendar.AccessibleDescription = resources.GetString("dtmRollupDate.Calendar.AccessibleDescription");
			this.dtmRollupDate.Calendar.AccessibleName = resources.GetString("dtmRollupDate.Calendar.AccessibleName");
			this.dtmRollupDate.Calendar.AnnuallyBoldedDates = ((System.DateTime[])(resources.GetObject("dtmRollupDate.Calendar.AnnuallyBoldedDates")));
			this.dtmRollupDate.Calendar.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("dtmRollupDate.Calendar.BackgroundImage")));
			this.dtmRollupDate.Calendar.BoldedDates = ((System.DateTime[])(resources.GetObject("dtmRollupDate.Calendar.BoldedDates")));
			this.dtmRollupDate.Calendar.CalendarDimensions = ((System.Drawing.Size)(resources.GetObject("dtmRollupDate.Calendar.CalendarDimensions")));
			this.dtmRollupDate.Calendar.Enabled = ((bool)(resources.GetObject("dtmRollupDate.Calendar.Enabled")));
			this.dtmRollupDate.Calendar.FirstDayOfWeek = ((System.Windows.Forms.Day)(resources.GetObject("dtmRollupDate.Calendar.FirstDayOfWeek")));
			this.dtmRollupDate.Calendar.Font = ((System.Drawing.Font)(resources.GetObject("dtmRollupDate.Calendar.Font")));
			this.dtmRollupDate.Calendar.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("dtmRollupDate.Calendar.ImeMode")));
			this.dtmRollupDate.Calendar.MonthlyBoldedDates = ((System.DateTime[])(resources.GetObject("dtmRollupDate.Calendar.MonthlyBoldedDates")));
			this.dtmRollupDate.Calendar.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("dtmRollupDate.Calendar.RightToLeft")));
			this.dtmRollupDate.Calendar.ShowClearButton = ((bool)(resources.GetObject("dtmRollupDate.Calendar.ShowClearButton")));
			this.dtmRollupDate.Calendar.ShowTodayButton = ((bool)(resources.GetObject("dtmRollupDate.Calendar.ShowTodayButton")));
			this.dtmRollupDate.Calendar.ShowWeekNumbers = ((bool)(resources.GetObject("dtmRollupDate.Calendar.ShowWeekNumbers")));
			this.dtmRollupDate.CaseSensitive = ((bool)(resources.GetObject("dtmRollupDate.CaseSensitive")));
			this.dtmRollupDate.Culture = ((int)(resources.GetObject("dtmRollupDate.Culture")));
			this.dtmRollupDate.CurrentTimeZone = ((bool)(resources.GetObject("dtmRollupDate.CurrentTimeZone")));
			this.dtmRollupDate.CustomFormat = resources.GetString("dtmRollupDate.CustomFormat");
			this.dtmRollupDate.DaylightTimeAdjustment = ((C1.Win.C1Input.DaylightTimeAdjustmentEnum)(resources.GetObject("dtmRollupDate.DaylightTimeAdjustment")));
			this.dtmRollupDate.DisplayFormat.CustomFormat = resources.GetString("dtmRollupDate.DisplayFormat.CustomFormat");
			this.dtmRollupDate.DisplayFormat.FormatType = ((C1.Win.C1Input.FormatTypeEnum)(resources.GetObject("dtmRollupDate.DisplayFormat.FormatType")));
			this.dtmRollupDate.DisplayFormat.Inherit = ((C1.Win.C1Input.FormatInfoInheritFlags)(resources.GetObject("dtmRollupDate.DisplayFormat.Inherit")));
			this.dtmRollupDate.DisplayFormat.NullText = resources.GetString("dtmRollupDate.DisplayFormat.NullText");
			this.dtmRollupDate.DisplayFormat.TrimEnd = ((bool)(resources.GetObject("dtmRollupDate.DisplayFormat.TrimEnd")));
			this.dtmRollupDate.DisplayFormat.TrimStart = ((bool)(resources.GetObject("dtmRollupDate.DisplayFormat.TrimStart")));
			this.dtmRollupDate.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("dtmRollupDate.Dock")));
			this.dtmRollupDate.DropDownFormAlign = ((C1.Win.C1Input.DropDownFormAlignmentEnum)(resources.GetObject("dtmRollupDate.DropDownFormAlign")));
			this.dtmRollupDate.EditFormat.CustomFormat = resources.GetString("dtmRollupDate.EditFormat.CustomFormat");
			this.dtmRollupDate.EditFormat.FormatType = ((C1.Win.C1Input.FormatTypeEnum)(resources.GetObject("dtmRollupDate.EditFormat.FormatType")));
			this.dtmRollupDate.EditFormat.Inherit = ((C1.Win.C1Input.FormatInfoInheritFlags)(resources.GetObject("dtmRollupDate.EditFormat.Inherit")));
			this.dtmRollupDate.EditFormat.NullText = resources.GetString("dtmRollupDate.EditFormat.NullText");
			this.dtmRollupDate.EditFormat.TrimEnd = ((bool)(resources.GetObject("dtmRollupDate.EditFormat.TrimEnd")));
			this.dtmRollupDate.EditFormat.TrimStart = ((bool)(resources.GetObject("dtmRollupDate.EditFormat.TrimStart")));
			this.dtmRollupDate.EditMask = resources.GetString("dtmRollupDate.EditMask");
			this.dtmRollupDate.EmptyAsNull = ((bool)(resources.GetObject("dtmRollupDate.EmptyAsNull")));
			this.dtmRollupDate.Enabled = ((bool)(resources.GetObject("dtmRollupDate.Enabled")));
			this.dtmRollupDate.ErrorInfo.BeepOnError = ((bool)(resources.GetObject("dtmRollupDate.ErrorInfo.BeepOnError")));
			this.dtmRollupDate.ErrorInfo.ErrorMessage = resources.GetString("dtmRollupDate.ErrorInfo.ErrorMessage");
			this.dtmRollupDate.ErrorInfo.ErrorMessageCaption = resources.GetString("dtmRollupDate.ErrorInfo.ErrorMessageCaption");
			this.dtmRollupDate.ErrorInfo.ShowErrorMessage = ((bool)(resources.GetObject("dtmRollupDate.ErrorInfo.ShowErrorMessage")));
			this.dtmRollupDate.ErrorInfo.ValueOnError = ((object)(resources.GetObject("dtmRollupDate.ErrorInfo.ValueOnError")));
			this.dtmRollupDate.Font = ((System.Drawing.Font)(resources.GetObject("dtmRollupDate.Font")));
			this.dtmRollupDate.FormatType = ((C1.Win.C1Input.FormatTypeEnum)(resources.GetObject("dtmRollupDate.FormatType")));
			this.dtmRollupDate.GapHeight = ((int)(resources.GetObject("dtmRollupDate.GapHeight")));
			this.dtmRollupDate.GMTOffset = ((System.TimeSpan)(resources.GetObject("dtmRollupDate.GMTOffset")));
			this.dtmRollupDate.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("dtmRollupDate.ImeMode")));
			this.dtmRollupDate.InitialSelection = ((C1.Win.C1Input.InitialSelectionEnum)(resources.GetObject("dtmRollupDate.InitialSelection")));
			this.dtmRollupDate.Location = ((System.Drawing.Point)(resources.GetObject("dtmRollupDate.Location")));
			this.dtmRollupDate.MaskInfo.AutoTabWhenFilled = ((bool)(resources.GetObject("dtmRollupDate.MaskInfo.AutoTabWhenFilled")));
			this.dtmRollupDate.MaskInfo.CaseSensitive = ((bool)(resources.GetObject("dtmRollupDate.MaskInfo.CaseSensitive")));
			this.dtmRollupDate.MaskInfo.CopyWithLiterals = ((bool)(resources.GetObject("dtmRollupDate.MaskInfo.CopyWithLiterals")));
			this.dtmRollupDate.MaskInfo.EditMask = resources.GetString("dtmRollupDate.MaskInfo.EditMask");
			this.dtmRollupDate.MaskInfo.EmptyAsNull = ((bool)(resources.GetObject("dtmRollupDate.MaskInfo.EmptyAsNull")));
			this.dtmRollupDate.MaskInfo.ErrorMessage = resources.GetString("dtmRollupDate.MaskInfo.ErrorMessage");
			this.dtmRollupDate.MaskInfo.Inherit = ((C1.Win.C1Input.MaskInfoInheritFlags)(resources.GetObject("dtmRollupDate.MaskInfo.Inherit")));
			this.dtmRollupDate.MaskInfo.PromptChar = ((char)(resources.GetObject("dtmRollupDate.MaskInfo.PromptChar")));
			this.dtmRollupDate.MaskInfo.ShowLiterals = ((C1.Win.C1Input.ShowLiteralsEnum)(resources.GetObject("dtmRollupDate.MaskInfo.ShowLiterals")));
			this.dtmRollupDate.MaskInfo.StoredEmptyChar = ((char)(resources.GetObject("dtmRollupDate.MaskInfo.StoredEmptyChar")));
			this.dtmRollupDate.MaxLength = ((int)(resources.GetObject("dtmRollupDate.MaxLength")));
			this.dtmRollupDate.Name = "dtmRollupDate";
			this.dtmRollupDate.NullText = resources.GetString("dtmRollupDate.NullText");
			this.dtmRollupDate.ParseInfo.CaseSensitive = ((bool)(resources.GetObject("dtmRollupDate.ParseInfo.CaseSensitive")));
			this.dtmRollupDate.ParseInfo.CustomFormat = resources.GetString("dtmRollupDate.ParseInfo.CustomFormat");
			this.dtmRollupDate.ParseInfo.DateTimeStyle = ((C1.Win.C1Input.DateTimeStyleFlags)(resources.GetObject("dtmRollupDate.ParseInfo.DateTimeStyle")));
			this.dtmRollupDate.ParseInfo.EmptyAsNull = ((bool)(resources.GetObject("dtmRollupDate.ParseInfo.EmptyAsNull")));
			this.dtmRollupDate.ParseInfo.ErrorMessage = resources.GetString("dtmRollupDate.ParseInfo.ErrorMessage");
			this.dtmRollupDate.ParseInfo.FormatType = ((C1.Win.C1Input.FormatTypeEnum)(resources.GetObject("dtmRollupDate.ParseInfo.FormatType")));
			this.dtmRollupDate.ParseInfo.Inherit = ((C1.Win.C1Input.ParseInfoInheritFlags)(resources.GetObject("dtmRollupDate.ParseInfo.Inherit")));
			this.dtmRollupDate.ParseInfo.NullText = resources.GetString("dtmRollupDate.ParseInfo.NullText");
			this.dtmRollupDate.ParseInfo.TrimEnd = ((bool)(resources.GetObject("dtmRollupDate.ParseInfo.TrimEnd")));
			this.dtmRollupDate.ParseInfo.TrimStart = ((bool)(resources.GetObject("dtmRollupDate.ParseInfo.TrimStart")));
			this.dtmRollupDate.PasswordChar = ((char)(resources.GetObject("dtmRollupDate.PasswordChar")));
			this.dtmRollupDate.PostValidation.CaseSensitive = ((bool)(resources.GetObject("dtmRollupDate.PostValidation.CaseSensitive")));
			this.dtmRollupDate.PostValidation.ErrorMessage = resources.GetString("dtmRollupDate.PostValidation.ErrorMessage");
			this.dtmRollupDate.PostValidation.Inherit = ((C1.Win.C1Input.PostValidationInheritFlags)(resources.GetObject("dtmRollupDate.PostValidation.Inherit")));
			this.dtmRollupDate.PostValidation.Validation = ((C1.Win.C1Input.PostValidationTypeEnum)(resources.GetObject("dtmRollupDate.PostValidation.Validation")));
			this.dtmRollupDate.PostValidation.Values = ((System.Array)(resources.GetObject("dtmRollupDate.PostValidation.Values")));
			this.dtmRollupDate.PostValidation.ValuesExcluded = ((System.Array)(resources.GetObject("dtmRollupDate.PostValidation.ValuesExcluded")));
			this.dtmRollupDate.PreValidation.CaseSensitive = ((bool)(resources.GetObject("dtmRollupDate.PreValidation.CaseSensitive")));
			this.dtmRollupDate.PreValidation.ErrorMessage = resources.GetString("dtmRollupDate.PreValidation.ErrorMessage");
			this.dtmRollupDate.PreValidation.Inherit = ((C1.Win.C1Input.PreValidationInheritFlags)(resources.GetObject("dtmRollupDate.PreValidation.Inherit")));
			this.dtmRollupDate.PreValidation.ItemSeparator = resources.GetString("dtmRollupDate.PreValidation.ItemSeparator");
			this.dtmRollupDate.PreValidation.PatternString = resources.GetString("dtmRollupDate.PreValidation.PatternString");
			this.dtmRollupDate.PreValidation.RegexOptions = ((C1.Win.C1Input.RegexOptionFlags)(resources.GetObject("dtmRollupDate.PreValidation.RegexOptions")));
			this.dtmRollupDate.PreValidation.TrimEnd = ((bool)(resources.GetObject("dtmRollupDate.PreValidation.TrimEnd")));
			this.dtmRollupDate.PreValidation.TrimStart = ((bool)(resources.GetObject("dtmRollupDate.PreValidation.TrimStart")));
			this.dtmRollupDate.PreValidation.Validation = ((C1.Win.C1Input.PreValidationTypeEnum)(resources.GetObject("dtmRollupDate.PreValidation.Validation")));
			this.dtmRollupDate.ReadOnly = true;
			this.dtmRollupDate.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("dtmRollupDate.RightToLeft")));
			this.dtmRollupDate.ShowFocusRectangle = ((bool)(resources.GetObject("dtmRollupDate.ShowFocusRectangle")));
			this.dtmRollupDate.Size = ((System.Drawing.Size)(resources.GetObject("dtmRollupDate.Size")));
			this.dtmRollupDate.TabIndex = ((int)(resources.GetObject("dtmRollupDate.TabIndex")));
			this.dtmRollupDate.TabStop = false;
			this.dtmRollupDate.Tag = ((object)(resources.GetObject("dtmRollupDate.Tag")));
			this.dtmRollupDate.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("dtmRollupDate.TextAlign")));
			this.dtmRollupDate.TrimEnd = ((bool)(resources.GetObject("dtmRollupDate.TrimEnd")));
			this.dtmRollupDate.TrimStart = ((bool)(resources.GetObject("dtmRollupDate.TrimStart")));
			this.dtmRollupDate.UserCultureOverride = ((bool)(resources.GetObject("dtmRollupDate.UserCultureOverride")));
			this.dtmRollupDate.Value = ((object)(resources.GetObject("dtmRollupDate.Value")));
			this.dtmRollupDate.VerticalAlign = ((C1.Win.C1Input.VerticalAlignEnum)(resources.GetObject("dtmRollupDate.VerticalAlign")));
			this.dtmRollupDate.Visible = ((bool)(resources.GetObject("dtmRollupDate.Visible")));
			this.dtmRollupDate.VisibleButtons = ((C1.Win.C1Input.DropDownControlButtonFlags)(resources.GetObject("dtmRollupDate.VisibleButtons")));
			// 
			// lblRollupDate
			// 
			this.lblRollupDate.AccessibleDescription = resources.GetString("lblRollupDate.AccessibleDescription");
			this.lblRollupDate.AccessibleName = resources.GetString("lblRollupDate.AccessibleName");
			this.lblRollupDate.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblRollupDate.Anchor")));
			this.lblRollupDate.AutoSize = ((bool)(resources.GetObject("lblRollupDate.AutoSize")));
			this.lblRollupDate.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblRollupDate.Dock")));
			this.lblRollupDate.Enabled = ((bool)(resources.GetObject("lblRollupDate.Enabled")));
			this.lblRollupDate.Font = ((System.Drawing.Font)(resources.GetObject("lblRollupDate.Font")));
			this.lblRollupDate.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblRollupDate.Image = ((System.Drawing.Image)(resources.GetObject("lblRollupDate.Image")));
			this.lblRollupDate.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblRollupDate.ImageAlign")));
			this.lblRollupDate.ImageIndex = ((int)(resources.GetObject("lblRollupDate.ImageIndex")));
			this.lblRollupDate.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblRollupDate.ImeMode")));
			this.lblRollupDate.Location = ((System.Drawing.Point)(resources.GetObject("lblRollupDate.Location")));
			this.lblRollupDate.Name = "lblRollupDate";
			this.lblRollupDate.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblRollupDate.RightToLeft")));
			this.lblRollupDate.Size = ((System.Drawing.Size)(resources.GetObject("lblRollupDate.Size")));
			this.lblRollupDate.TabIndex = ((int)(resources.GetObject("lblRollupDate.TabIndex")));
			this.lblRollupDate.Text = resources.GetString("lblRollupDate.Text");
			this.lblRollupDate.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblRollupDate.TextAlign")));
			this.lblRollupDate.Visible = ((bool)(resources.GetObject("lblRollupDate.Visible")));
			// 
			// btnImport
			// 
			this.btnImport.AccessibleDescription = resources.GetString("btnImport.AccessibleDescription");
			this.btnImport.AccessibleName = resources.GetString("btnImport.AccessibleName");
			this.btnImport.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnImport.Anchor")));
			this.btnImport.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnImport.BackgroundImage")));
			this.btnImport.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnImport.Dock")));
			this.btnImport.Enabled = ((bool)(resources.GetObject("btnImport.Enabled")));
			this.btnImport.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnImport.FlatStyle")));
			this.btnImport.Font = ((System.Drawing.Font)(resources.GetObject("btnImport.Font")));
			this.btnImport.Image = ((System.Drawing.Image)(resources.GetObject("btnImport.Image")));
			this.btnImport.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnImport.ImageAlign")));
			this.btnImport.ImageIndex = ((int)(resources.GetObject("btnImport.ImageIndex")));
			this.btnImport.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnImport.ImeMode")));
			this.btnImport.Location = ((System.Drawing.Point)(resources.GetObject("btnImport.Location")));
			this.btnImport.Name = "btnImport";
			this.btnImport.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnImport.RightToLeft")));
			this.btnImport.Size = ((System.Drawing.Size)(resources.GetObject("btnImport.Size")));
			this.btnImport.TabIndex = ((int)(resources.GetObject("btnImport.TabIndex")));
			this.btnImport.Text = resources.GetString("btnImport.Text");
			this.btnImport.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnImport.TextAlign")));
			this.btnImport.Visible = ((bool)(resources.GetObject("btnImport.Visible")));
			this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
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
			this.txtCurrency.TabStop = false;
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
			// lblAllocatingMessage
			// 
			this.lblAllocatingMessage.AccessibleDescription = resources.GetString("lblAllocatingMessage.AccessibleDescription");
			this.lblAllocatingMessage.AccessibleName = resources.GetString("lblAllocatingMessage.AccessibleName");
			this.lblAllocatingMessage.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblAllocatingMessage.Anchor")));
			this.lblAllocatingMessage.AutoSize = ((bool)(resources.GetObject("lblAllocatingMessage.AutoSize")));
			this.lblAllocatingMessage.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblAllocatingMessage.Dock")));
			this.lblAllocatingMessage.Enabled = ((bool)(resources.GetObject("lblAllocatingMessage.Enabled")));
			this.lblAllocatingMessage.Font = ((System.Drawing.Font)(resources.GetObject("lblAllocatingMessage.Font")));
			this.lblAllocatingMessage.Image = ((System.Drawing.Image)(resources.GetObject("lblAllocatingMessage.Image")));
			this.lblAllocatingMessage.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblAllocatingMessage.ImageAlign")));
			this.lblAllocatingMessage.ImageIndex = ((int)(resources.GetObject("lblAllocatingMessage.ImageIndex")));
			this.lblAllocatingMessage.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblAllocatingMessage.ImeMode")));
			this.lblAllocatingMessage.Location = ((System.Drawing.Point)(resources.GetObject("lblAllocatingMessage.Location")));
			this.lblAllocatingMessage.Name = "lblAllocatingMessage";
			this.lblAllocatingMessage.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblAllocatingMessage.RightToLeft")));
			this.lblAllocatingMessage.Size = ((System.Drawing.Size)(resources.GetObject("lblAllocatingMessage.Size")));
			this.lblAllocatingMessage.TabIndex = ((int)(resources.GetObject("lblAllocatingMessage.TabIndex")));
			this.lblAllocatingMessage.Text = resources.GetString("lblAllocatingMessage.Text");
			this.lblAllocatingMessage.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblAllocatingMessage.TextAlign")));
			this.lblAllocatingMessage.Visible = ((bool)(resources.GetObject("lblAllocatingMessage.Visible")));
			// 
			// lblFirstDayOfMonth
			// 
			this.lblFirstDayOfMonth.AccessibleDescription = resources.GetString("lblFirstDayOfMonth.AccessibleDescription");
			this.lblFirstDayOfMonth.AccessibleName = resources.GetString("lblFirstDayOfMonth.AccessibleName");
			this.lblFirstDayOfMonth.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblFirstDayOfMonth.Anchor")));
			this.lblFirstDayOfMonth.AutoSize = ((bool)(resources.GetObject("lblFirstDayOfMonth.AutoSize")));
			this.lblFirstDayOfMonth.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblFirstDayOfMonth.Dock")));
			this.lblFirstDayOfMonth.Enabled = ((bool)(resources.GetObject("lblFirstDayOfMonth.Enabled")));
			this.lblFirstDayOfMonth.Font = ((System.Drawing.Font)(resources.GetObject("lblFirstDayOfMonth.Font")));
			this.lblFirstDayOfMonth.Image = ((System.Drawing.Image)(resources.GetObject("lblFirstDayOfMonth.Image")));
			this.lblFirstDayOfMonth.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblFirstDayOfMonth.ImageAlign")));
			this.lblFirstDayOfMonth.ImageIndex = ((int)(resources.GetObject("lblFirstDayOfMonth.ImageIndex")));
			this.lblFirstDayOfMonth.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblFirstDayOfMonth.ImeMode")));
			this.lblFirstDayOfMonth.Location = ((System.Drawing.Point)(resources.GetObject("lblFirstDayOfMonth.Location")));
			this.lblFirstDayOfMonth.Name = "lblFirstDayOfMonth";
			this.lblFirstDayOfMonth.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblFirstDayOfMonth.RightToLeft")));
			this.lblFirstDayOfMonth.Size = ((System.Drawing.Size)(resources.GetObject("lblFirstDayOfMonth.Size")));
			this.lblFirstDayOfMonth.TabIndex = ((int)(resources.GetObject("lblFirstDayOfMonth.TabIndex")));
			this.lblFirstDayOfMonth.Text = resources.GetString("lblFirstDayOfMonth.Text");
			this.lblFirstDayOfMonth.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblFirstDayOfMonth.TextAlign")));
			this.lblFirstDayOfMonth.Visible = ((bool)(resources.GetObject("lblFirstDayOfMonth.Visible")));
			// 
			// lblEndDayOfMonth
			// 
			this.lblEndDayOfMonth.AccessibleDescription = resources.GetString("lblEndDayOfMonth.AccessibleDescription");
			this.lblEndDayOfMonth.AccessibleName = resources.GetString("lblEndDayOfMonth.AccessibleName");
			this.lblEndDayOfMonth.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblEndDayOfMonth.Anchor")));
			this.lblEndDayOfMonth.AutoSize = ((bool)(resources.GetObject("lblEndDayOfMonth.AutoSize")));
			this.lblEndDayOfMonth.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblEndDayOfMonth.Dock")));
			this.lblEndDayOfMonth.Enabled = ((bool)(resources.GetObject("lblEndDayOfMonth.Enabled")));
			this.lblEndDayOfMonth.Font = ((System.Drawing.Font)(resources.GetObject("lblEndDayOfMonth.Font")));
			this.lblEndDayOfMonth.Image = ((System.Drawing.Image)(resources.GetObject("lblEndDayOfMonth.Image")));
			this.lblEndDayOfMonth.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblEndDayOfMonth.ImageAlign")));
			this.lblEndDayOfMonth.ImageIndex = ((int)(resources.GetObject("lblEndDayOfMonth.ImageIndex")));
			this.lblEndDayOfMonth.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblEndDayOfMonth.ImeMode")));
			this.lblEndDayOfMonth.Location = ((System.Drawing.Point)(resources.GetObject("lblEndDayOfMonth.Location")));
			this.lblEndDayOfMonth.Name = "lblEndDayOfMonth";
			this.lblEndDayOfMonth.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblEndDayOfMonth.RightToLeft")));
			this.lblEndDayOfMonth.Size = ((System.Drawing.Size)(resources.GetObject("lblEndDayOfMonth.Size")));
			this.lblEndDayOfMonth.TabIndex = ((int)(resources.GetObject("lblEndDayOfMonth.TabIndex")));
			this.lblEndDayOfMonth.Text = resources.GetString("lblEndDayOfMonth.Text");
			this.lblEndDayOfMonth.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblEndDayOfMonth.TextAlign")));
			this.lblEndDayOfMonth.Visible = ((bool)(resources.GetObject("lblEndDayOfMonth.Visible")));
			// 
			// dlgOpenImpFile
			// 
			this.dlgOpenImpFile.DefaultExt = "xls";
			this.dlgOpenImpFile.Filter = resources.GetString("dlgOpenImpFile.Filter");
			this.dlgOpenImpFile.Title = resources.GetString("dlgOpenImpFile.Title");
			// 
			// btnDelAllocation
			// 
			this.btnDelAllocation.AccessibleDescription = resources.GetString("btnDelAllocation.AccessibleDescription");
			this.btnDelAllocation.AccessibleName = resources.GetString("btnDelAllocation.AccessibleName");
			this.btnDelAllocation.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnDelAllocation.Anchor")));
			this.btnDelAllocation.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnDelAllocation.BackgroundImage")));
			this.btnDelAllocation.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnDelAllocation.Dock")));
			this.btnDelAllocation.Enabled = ((bool)(resources.GetObject("btnDelAllocation.Enabled")));
			this.btnDelAllocation.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnDelAllocation.FlatStyle")));
			this.btnDelAllocation.Font = ((System.Drawing.Font)(resources.GetObject("btnDelAllocation.Font")));
			this.btnDelAllocation.Image = ((System.Drawing.Image)(resources.GetObject("btnDelAllocation.Image")));
			this.btnDelAllocation.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnDelAllocation.ImageAlign")));
			this.btnDelAllocation.ImageIndex = ((int)(resources.GetObject("btnDelAllocation.ImageIndex")));
			this.btnDelAllocation.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnDelAllocation.ImeMode")));
			this.btnDelAllocation.Location = ((System.Drawing.Point)(resources.GetObject("btnDelAllocation.Location")));
			this.btnDelAllocation.Name = "btnDelAllocation";
			this.btnDelAllocation.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnDelAllocation.RightToLeft")));
			this.btnDelAllocation.Size = ((System.Drawing.Size)(resources.GetObject("btnDelAllocation.Size")));
			this.btnDelAllocation.TabIndex = ((int)(resources.GetObject("btnDelAllocation.TabIndex")));
			this.btnDelAllocation.Text = resources.GetString("btnDelAllocation.Text");
			this.btnDelAllocation.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnDelAllocation.TextAlign")));
			this.btnDelAllocation.Visible = ((bool)(resources.GetObject("btnDelAllocation.Visible")));
			this.btnDelAllocation.Click += new System.EventHandler(this.btnDelAllocation_Click);
			// 
			// lblDelAllocatingMessage
			// 
			this.lblDelAllocatingMessage.AccessibleDescription = resources.GetString("lblDelAllocatingMessage.AccessibleDescription");
			this.lblDelAllocatingMessage.AccessibleName = resources.GetString("lblDelAllocatingMessage.AccessibleName");
			this.lblDelAllocatingMessage.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblDelAllocatingMessage.Anchor")));
			this.lblDelAllocatingMessage.AutoSize = ((bool)(resources.GetObject("lblDelAllocatingMessage.AutoSize")));
			this.lblDelAllocatingMessage.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblDelAllocatingMessage.Dock")));
			this.lblDelAllocatingMessage.Enabled = ((bool)(resources.GetObject("lblDelAllocatingMessage.Enabled")));
			this.lblDelAllocatingMessage.Font = ((System.Drawing.Font)(resources.GetObject("lblDelAllocatingMessage.Font")));
			this.lblDelAllocatingMessage.Image = ((System.Drawing.Image)(resources.GetObject("lblDelAllocatingMessage.Image")));
			this.lblDelAllocatingMessage.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblDelAllocatingMessage.ImageAlign")));
			this.lblDelAllocatingMessage.ImageIndex = ((int)(resources.GetObject("lblDelAllocatingMessage.ImageIndex")));
			this.lblDelAllocatingMessage.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblDelAllocatingMessage.ImeMode")));
			this.lblDelAllocatingMessage.Location = ((System.Drawing.Point)(resources.GetObject("lblDelAllocatingMessage.Location")));
			this.lblDelAllocatingMessage.Name = "lblDelAllocatingMessage";
			this.lblDelAllocatingMessage.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblDelAllocatingMessage.RightToLeft")));
			this.lblDelAllocatingMessage.Size = ((System.Drawing.Size)(resources.GetObject("lblDelAllocatingMessage.Size")));
			this.lblDelAllocatingMessage.TabIndex = ((int)(resources.GetObject("lblDelAllocatingMessage.TabIndex")));
			this.lblDelAllocatingMessage.Text = resources.GetString("lblDelAllocatingMessage.Text");
			this.lblDelAllocatingMessage.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblDelAllocatingMessage.TextAlign")));
			this.lblDelAllocatingMessage.Visible = ((bool)(resources.GetObject("lblDelAllocatingMessage.Visible")));
			// 
			// btnDelChargeAllocation
			// 
			this.btnDelChargeAllocation.AccessibleDescription = resources.GetString("btnDelChargeAllocation.AccessibleDescription");
			this.btnDelChargeAllocation.AccessibleName = resources.GetString("btnDelChargeAllocation.AccessibleName");
			this.btnDelChargeAllocation.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnDelChargeAllocation.Anchor")));
			this.btnDelChargeAllocation.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnDelChargeAllocation.BackgroundImage")));
			this.btnDelChargeAllocation.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnDelChargeAllocation.Dock")));
			this.btnDelChargeAllocation.Enabled = ((bool)(resources.GetObject("btnDelChargeAllocation.Enabled")));
			this.btnDelChargeAllocation.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnDelChargeAllocation.FlatStyle")));
			this.btnDelChargeAllocation.Font = ((System.Drawing.Font)(resources.GetObject("btnDelChargeAllocation.Font")));
			this.btnDelChargeAllocation.Image = ((System.Drawing.Image)(resources.GetObject("btnDelChargeAllocation.Image")));
			this.btnDelChargeAllocation.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnDelChargeAllocation.ImageAlign")));
			this.btnDelChargeAllocation.ImageIndex = ((int)(resources.GetObject("btnDelChargeAllocation.ImageIndex")));
			this.btnDelChargeAllocation.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnDelChargeAllocation.ImeMode")));
			this.btnDelChargeAllocation.Location = ((System.Drawing.Point)(resources.GetObject("btnDelChargeAllocation.Location")));
			this.btnDelChargeAllocation.Name = "btnDelChargeAllocation";
			this.btnDelChargeAllocation.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnDelChargeAllocation.RightToLeft")));
			this.btnDelChargeAllocation.Size = ((System.Drawing.Size)(resources.GetObject("btnDelChargeAllocation.Size")));
			this.btnDelChargeAllocation.TabIndex = ((int)(resources.GetObject("btnDelChargeAllocation.TabIndex")));
			this.btnDelChargeAllocation.Text = resources.GetString("btnDelChargeAllocation.Text");
			this.btnDelChargeAllocation.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnDelChargeAllocation.TextAlign")));
			this.btnDelChargeAllocation.Visible = ((bool)(resources.GetObject("btnDelChargeAllocation.Visible")));
			this.btnDelChargeAllocation.Click += new System.EventHandler(this.btnDelChargeAllocation_Click);
			// 
			// txtTotalAmount
			// 
			this.txtTotalAmount.AccessibleDescription = resources.GetString("txtTotalAmount.AccessibleDescription");
			this.txtTotalAmount.AccessibleName = resources.GetString("txtTotalAmount.AccessibleName");
			this.txtTotalAmount.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("txtTotalAmount.Anchor")));
			this.txtTotalAmount.AutoSize = ((bool)(resources.GetObject("txtTotalAmount.AutoSize")));
			this.txtTotalAmount.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("txtTotalAmount.BackgroundImage")));
			this.txtTotalAmount.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("txtTotalAmount.Dock")));
			this.txtTotalAmount.Enabled = ((bool)(resources.GetObject("txtTotalAmount.Enabled")));
			this.txtTotalAmount.Font = ((System.Drawing.Font)(resources.GetObject("txtTotalAmount.Font")));
			this.txtTotalAmount.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("txtTotalAmount.ImeMode")));
			this.txtTotalAmount.Location = ((System.Drawing.Point)(resources.GetObject("txtTotalAmount.Location")));
			this.txtTotalAmount.MaxLength = ((int)(resources.GetObject("txtTotalAmount.MaxLength")));
			this.txtTotalAmount.Multiline = ((bool)(resources.GetObject("txtTotalAmount.Multiline")));
			this.txtTotalAmount.Name = "txtTotalAmount";
			this.txtTotalAmount.PasswordChar = ((char)(resources.GetObject("txtTotalAmount.PasswordChar")));
			this.txtTotalAmount.ReadOnly = true;
			this.txtTotalAmount.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("txtTotalAmount.RightToLeft")));
			this.txtTotalAmount.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("txtTotalAmount.ScrollBars")));
			this.txtTotalAmount.Size = ((System.Drawing.Size)(resources.GetObject("txtTotalAmount.Size")));
			this.txtTotalAmount.TabIndex = ((int)(resources.GetObject("txtTotalAmount.TabIndex")));
			this.txtTotalAmount.TabStop = false;
			this.txtTotalAmount.Text = resources.GetString("txtTotalAmount.Text");
			this.txtTotalAmount.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("txtTotalAmount.TextAlign")));
			this.txtTotalAmount.Visible = ((bool)(resources.GetObject("txtTotalAmount.Visible")));
			this.txtTotalAmount.WordWrap = ((bool)(resources.GetObject("txtTotalAmount.WordWrap")));
			// 
			// lblTotalAmount
			// 
			this.lblTotalAmount.AccessibleDescription = resources.GetString("lblTotalAmount.AccessibleDescription");
			this.lblTotalAmount.AccessibleName = resources.GetString("lblTotalAmount.AccessibleName");
			this.lblTotalAmount.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblTotalAmount.Anchor")));
			this.lblTotalAmount.AutoSize = ((bool)(resources.GetObject("lblTotalAmount.AutoSize")));
			this.lblTotalAmount.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblTotalAmount.Dock")));
			this.lblTotalAmount.Enabled = ((bool)(resources.GetObject("lblTotalAmount.Enabled")));
			this.lblTotalAmount.Font = ((System.Drawing.Font)(resources.GetObject("lblTotalAmount.Font")));
			this.lblTotalAmount.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblTotalAmount.Image = ((System.Drawing.Image)(resources.GetObject("lblTotalAmount.Image")));
			this.lblTotalAmount.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblTotalAmount.ImageAlign")));
			this.lblTotalAmount.ImageIndex = ((int)(resources.GetObject("lblTotalAmount.ImageIndex")));
			this.lblTotalAmount.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblTotalAmount.ImeMode")));
			this.lblTotalAmount.Location = ((System.Drawing.Point)(resources.GetObject("lblTotalAmount.Location")));
			this.lblTotalAmount.Name = "lblTotalAmount";
			this.lblTotalAmount.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblTotalAmount.RightToLeft")));
			this.lblTotalAmount.Size = ((System.Drawing.Size)(resources.GetObject("lblTotalAmount.Size")));
			this.lblTotalAmount.TabIndex = ((int)(resources.GetObject("lblTotalAmount.TabIndex")));
			this.lblTotalAmount.Text = resources.GetString("lblTotalAmount.Text");
			this.lblTotalAmount.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblTotalAmount.TextAlign")));
			this.lblTotalAmount.Visible = ((bool)(resources.GetObject("lblTotalAmount.Visible")));
			// 
			// ActualCostDistributionSetup
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
			this.Controls.Add(this.txtTotalAmount);
			this.Controls.Add(this.lblTotalAmount);
			this.Controls.Add(this.btnDelChargeAllocation);
			this.Controls.Add(this.lblDelAllocatingMessage);
			this.Controls.Add(this.btnDelAllocation);
			this.Controls.Add(this.lblEndDayOfMonth);
			this.Controls.Add(this.lblFirstDayOfMonth);
			this.Controls.Add(this.lblAllocatingMessage);
			this.Controls.Add(this.txtCurrency);
			this.Controls.Add(this.txtPeriod);
			this.Controls.Add(this.dgrdData);
			this.Controls.Add(this.lblCurrency);
			this.Controls.Add(this.btnImport);
			this.Controls.Add(this.dtmRollupDate);
			this.Controls.Add(this.lblRollupDate);
			this.Controls.Add(this.btnPrint);
			this.Controls.Add(this.btnAllocate);
			this.Controls.Add(this.dtmToDate);
			this.Controls.Add(this.dtmFromDate);
			this.Controls.Add(this.lblToDate);
			this.Controls.Add(this.lblFromDate);
			this.Controls.Add(this.btnHelp);
			this.Controls.Add(this.btnDelete);
			this.Controls.Add(this.btnSave);
			this.Controls.Add(this.btnAdd);
			this.Controls.Add(this.btnEdit);
			this.Controls.Add(this.btnClose);
			this.Controls.Add(this.btnRollup);
			this.Controls.Add(this.btnPeriodSearch);
			this.Controls.Add(this.lblPeriod);
			this.Controls.Add(this.cboCCN);
			this.Controls.Add(this.lblCCN);
			this.Controls.Add(this.numValue);
			this.Enabled = ((bool)(resources.GetObject("$this.Enabled")));
			this.Font = ((System.Drawing.Font)(resources.GetObject("$this.Font")));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("$this.ImeMode")));
			this.KeyPreview = true;
			this.Location = ((System.Drawing.Point)(resources.GetObject("$this.Location")));
			this.MaximumSize = ((System.Drawing.Size)(resources.GetObject("$this.MaximumSize")));
			this.MinimumSize = ((System.Drawing.Size)(resources.GetObject("$this.MinimumSize")));
			this.Name = "ActualCostDistributionSetup";
			this.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("$this.RightToLeft")));
			this.StartPosition = ((System.Windows.Forms.FormStartPosition)(resources.GetObject("$this.StartPosition")));
			this.Text = resources.GetString("$this.Text");
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ActualCostDistributionSetup_KeyDown);
			this.Closing += new System.ComponentModel.CancelEventHandler(this.ActualCostDistributionSetup_Closing);
			this.Load += new System.EventHandler(this.ActualCostDistributionSetup_Load);
			((System.ComponentModel.ISupportInitialize)(this.cboCCN)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dgrdData)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dtmToDate)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dtmFromDate)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numValue)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dtmRollupDate)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion
		
		#region Methods	
		
		private void UpdateLineValue()
		{
			for(int i = 0; i < dgrdData.RowCount; i++)
			{
				dgrdData[i, CST_ActCostAllocationDetailTable.LINE_FLD] = i + 1;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="pintMasterId"></param>
		private void LoadDetailGrid(int pintMasterId)
		{			
			//Load blank data if master id is invalid id 
			if(pintMasterId <= 0)
			{
				//create blank detail table
				dtbDetail = BuildDetailTable();					
			}
			else
			{	
				//call bo's method tho retrieve data					
				dtbDetail = boMaster.GetDetailByMaster(pintMasterId);
			}

			// bind to grid & reformat grid
			dgrdData.DataSource = dtbDetail;				
			FormatDataGrid();
		}

		/// <summary>
		/// process data inputed into the grid
		/// </summary>
		/// <param name="pstrColumnName"></param>
		/// <param name="pstrColumValue"></param>
		/// <returns>If no error then return true, else return false</returns>
		private bool ProcessInputDataInGrid(string pstrColumnName, string pstrColumValue, bool pblnAlwaysShow)
		{			
			const string PRODUCT_IN_WORKCENTER_VIEW = "V_ProductInWorkCenter";

			Hashtable htbCondition = new Hashtable(); 
			DataRowView drvResult = null;
			bool blnResult = true;
			string[] arrMessage = new string[2];

			//Check for each column
			switch (pstrColumnName)
			{
				case STD_CostElementTable.TABLE_NAME + STD_CostElementTable.NAME_FLD:
					//clear hash table for new condition
					htbCondition.Clear();
					htbCondition.Add(STD_CostElementTable.ISLEAF_FLD, Constants.COST_ELEMENT_IS_LEAF);
					
					// Call OpenSearchForm for Work Center selecting 
					drvResult = FormControlComponents.OpenSearchForm(STD_CostElementTable.TABLE_NAME, STD_CostElementTable.NAME_FLD, pstrColumValue, htbCondition, pblnAlwaysShow);
					if (drvResult != null)
					{
						if(pblnAlwaysShow)
						{
							dgrdData[dgrdData.Row, CST_ActCostAllocationDetailTable.COSTELEMENTID_FLD] = drvResult[STD_CostElementTable.COSTELEMENTID_FLD];
							dgrdData[dgrdData.Row, STD_CostElementTable.TABLE_NAME + STD_CostElementTable.NAME_FLD] = drvResult[STD_CostElementTable.NAME_FLD];
						}
						else
						{
							dgrdData.Columns[CST_ActCostAllocationDetailTable.COSTELEMENTID_FLD].Value = drvResult[STD_CostElementTable.COSTELEMENTID_FLD];
							dgrdData.Columns[STD_CostElementTable.TABLE_NAME + STD_CostElementTable.NAME_FLD].Value = drvResult[STD_CostElementTable.NAME_FLD];								
						}
					}
					else
					{
						blnResult = pblnAlwaysShow;
					}
					break;

				case MST_DepartmentTable.TABLE_NAME + MST_DepartmentTable.CODE_FLD:
					//clear hash table for new condition
					htbCondition.Clear();
					htbCondition.Add(MST_DepartmentTable.CCNID_FLD, cboCCN.SelectedValue);
					
					// Call OpenSearchForm for Work Center selecting 
					drvResult = FormControlComponents.OpenSearchForm(MST_DepartmentTable.TABLE_NAME, MST_DepartmentTable.CODE_FLD, pstrColumValue, htbCondition, pblnAlwaysShow);
					if(drvResult != null)
					{
						//Clear related columns if other value has been selected
						if(dgrdData[dgrdData.Row, CST_ActCostAllocationDetailTable.DEPARTMENTID_FLD].ToString() != drvResult[MST_DepartmentTable.DEPARTMENTID_FLD].ToString()
						&& dgrdData.Columns[CST_ActCostAllocationDetailTable.DEPARTMENTID_FLD].Value.ToString() != drvResult[MST_DepartmentTable.DEPARTMENTID_FLD].ToString())
						{
							ClearRelatedColumns(pstrColumnName, pblnAlwaysShow);
						}

						if(pblnAlwaysShow)
						{
							dgrdData[dgrdData.Row, CST_ActCostAllocationDetailTable.DEPARTMENTID_FLD] = drvResult[MST_DepartmentTable.DEPARTMENTID_FLD];
							dgrdData[dgrdData.Row, MST_DepartmentTable.TABLE_NAME + MST_DepartmentTable.CODE_FLD] = drvResult[MST_DepartmentTable.CODE_FLD];								
						}
						else
						{
							dgrdData.Columns[CST_ActCostAllocationDetailTable.DEPARTMENTID_FLD].Value = drvResult[MST_DepartmentTable.DEPARTMENTID_FLD];
							dgrdData.Columns[MST_DepartmentTable.TABLE_NAME + MST_DepartmentTable.CODE_FLD].Value = drvResult[MST_DepartmentTable.CODE_FLD];								
						}
					}
					else
					{
						blnResult = pblnAlwaysShow;
					}
					break;
				
				case PRO_ProductionLineTable.TABLE_NAME + PRO_ProductionLineTable.CODE_FLD:
					//clear hash table for new condition
					htbCondition.Clear();

					if(!dgrdData[dgrdData.Row, MST_DepartmentTable.DEPARTMENTID_FLD].Equals(DBNull.Value))
					{
						htbCondition.Add(PRO_ProductionLineTable.DEPARTMENTID_FLD, dgrdData[dgrdData.Row, MST_DepartmentTable.DEPARTMENTID_FLD]);
					}
					else
					{
						arrMessage[0] = dgrdData.Columns[MST_DepartmentTable.TABLE_NAME + MST_DepartmentTable.CODE_FLD].Caption;
						arrMessage[1] = dgrdData.Columns[PRO_ProductionLineTable.TABLE_NAME + PRO_ProductionLineTable.CODE_FLD].Caption;

						PCSMessageBox.Show(ErrorCode.MESSAGE_SELECT_ONE_BEFORE_SELECT_ONE, MessageBoxIcon.Warning, arrMessage);
						dgrdData.Col = dgrdData.Columns.IndexOf(dgrdData.Columns[MST_DepartmentTable.TABLE_NAME + MST_DepartmentTable.CODE_FLD]);
						dgrdData.Focus();
						return false;
					}

					// Call OpenSearchForm for selecting 
					drvResult = FormControlComponents.OpenSearchForm(PRO_ProductionLineTable.TABLE_NAME, PRO_ProductionLineTable.CODE_FLD, pstrColumValue, htbCondition, pblnAlwaysShow);
					if (drvResult != null)
					{
						//Clear related columns if other value has been selected
						if(dgrdData[dgrdData.Row, CST_ActCostAllocationDetailTable.PRODUCTIONLINEID_FLD].ToString() != drvResult[PRO_ProductionLineTable.PRODUCTIONLINEID_FLD].ToString()
						&& dgrdData.Columns[CST_ActCostAllocationDetailTable.PRODUCTIONLINEID_FLD].Value.ToString() != drvResult[PRO_ProductionLineTable.PRODUCTIONLINEID_FLD].ToString())
						{
							ClearRelatedColumns(pstrColumnName, pblnAlwaysShow);
						}

						if(pblnAlwaysShow)
						{
							dgrdData[dgrdData.Row, CST_ActCostAllocationDetailTable.PRODUCTIONLINEID_FLD] = drvResult[PRO_ProductionLineTable.PRODUCTIONLINEID_FLD];
							dgrdData[dgrdData.Row, PRO_ProductionLineTable.TABLE_NAME + PRO_ProductionLineTable.CODE_FLD] = drvResult[PRO_ProductionLineTable.CODE_FLD];
						}
						else
						{
							dgrdData.Columns[CST_ActCostAllocationDetailTable.PRODUCTIONLINEID_FLD].Value = drvResult[PRO_ProductionLineTable.PRODUCTIONLINEID_FLD];
							dgrdData.Columns[PRO_ProductionLineTable.TABLE_NAME + PRO_ProductionLineTable.CODE_FLD].Value = drvResult[PRO_ProductionLineTable.CODE_FLD];
						}
					}
					else
					{
						blnResult = pblnAlwaysShow;
					}
					break;
				
				case CST_ProductGroupTable.TABLE_NAME + CST_ProductGroupTable.CODE_FLD:
					//clear hash table for new condition
					htbCondition.Clear();

					if(!dgrdData[dgrdData.Row, CST_ActCostAllocationDetailTable.PRODUCTIONLINEID_FLD].Equals(DBNull.Value))
					{
						htbCondition.Add(CST_ProductGroupTable.PRODUCTIONLINEID_FLD, dgrdData[dgrdData.Row, CST_ActCostAllocationDetailTable.PRODUCTIONLINEID_FLD]);
					}
					else
					{
						arrMessage[0] = dgrdData.Columns[PRO_ProductionLineTable.TABLE_NAME + PRO_ProductionLineTable.CODE_FLD].Caption;
						arrMessage[1] = dgrdData.Columns[CST_ProductGroupTable.TABLE_NAME + CST_ProductGroupTable.CODE_FLD].Caption;

						PCSMessageBox.Show(ErrorCode.MESSAGE_SELECT_ONE_BEFORE_SELECT_ONE, MessageBoxIcon.Warning, arrMessage);
						dgrdData.Col = dgrdData.Columns.IndexOf(dgrdData.Columns[PRO_ProductionLineTable.TABLE_NAME + PRO_ProductionLineTable.CODE_FLD]);
						dgrdData.Focus();
						return false;
					}

					// Call OpenSearchForm for selecting 
					drvResult = FormControlComponents.OpenSearchForm(CST_ProductGroupTable.TABLE_NAME, CST_ProductGroupTable.CODE_FLD, pstrColumValue, htbCondition, pblnAlwaysShow);
					if (drvResult != null)
					{
						//Clear related columns if other value has been selected
						if(dgrdData[dgrdData.Row, CST_ActCostAllocationDetailTable.PRODUCTGROUPID_FLD].ToString() != drvResult[CST_ProductGroupTable.PRODUCTGROUPID_FLD].ToString()
						&& dgrdData.Columns[CST_ActCostAllocationDetailTable.PRODUCTGROUPID_FLD].Value.ToString() != drvResult[CST_ProductGroupTable.PRODUCTGROUPID_FLD].ToString())
						{
							ClearRelatedColumns(pstrColumnName, pblnAlwaysShow);
						}

						if(pblnAlwaysShow)
						{
							dgrdData[dgrdData.Row, CST_ActCostAllocationDetailTable.PRODUCTGROUPID_FLD] = drvResult[CST_ProductGroupTable.PRODUCTGROUPID_FLD];
							dgrdData[dgrdData.Row, CST_ProductGroupTable.TABLE_NAME + CST_ProductGroupTable.CODE_FLD] = drvResult[CST_ProductGroupTable.CODE_FLD];
						}
						else
						{
							dgrdData.Columns[CST_ActCostAllocationDetailTable.PRODUCTGROUPID_FLD].Value = drvResult[CST_ProductGroupTable.PRODUCTGROUPID_FLD];
							dgrdData.Columns[CST_ProductGroupTable.TABLE_NAME + CST_ProductGroupTable.CODE_FLD].Value = drvResult[CST_ProductGroupTable.CODE_FLD];
						}
					}
					else
					{
						blnResult = pblnAlwaysShow;
					}
					break;

				case ITM_ProductTable.TABLE_NAME + ITM_ProductTable.CODE_FLD:
					//clear hash table for new condition
					htbCondition.Clear();

					//Item must belong selected group
					if(!dgrdData[dgrdData.Row, CST_ActCostAllocationDetailTable.PRODUCTGROUPID_FLD].Equals(DBNull.Value))
					{
						htbCondition.Add(ITM_ProductTable.PRODUCTGROUPID_FLD, dgrdData[dgrdData.Row, CST_ActCostAllocationDetailTable.PRODUCTGROUPID_FLD]);
					}
					else
					{
						arrMessage[0] = dgrdData.Columns[CST_ProductGroupTable.TABLE_NAME + CST_ProductGroupTable.CODE_FLD].Caption;
						arrMessage[1] = dgrdData.Columns[ITM_ProductTable.TABLE_NAME + ITM_ProductTable.CODE_FLD].Caption;

						PCSMessageBox.Show(ErrorCode.MESSAGE_SELECT_ONE_BEFORE_SELECT_ONE, MessageBoxIcon.Warning, arrMessage);
						dgrdData.Col = dgrdData.Columns.IndexOf(dgrdData.Columns[CST_ProductGroupTable.TABLE_NAME + CST_ProductGroupTable.CODE_FLD]);
						dgrdData.Focus();
						return false;
					}
					
					//show only make items
					htbCondition.Add(ITM_ProductTable.MAKEITEM_FLD, 1);

					// Call OpenSearchForm for selecting 
					drvResult = FormControlComponents.OpenSearchForm(PRODUCT_IN_WORKCENTER_VIEW, ITM_ProductTable.CODE_FLD, pstrColumValue, htbCondition, pblnAlwaysShow);
					if (drvResult != null)
					{
						if(pblnAlwaysShow)
						{
							dgrdData[dgrdData.Row, ITM_ProductTable.PRODUCTID_FLD] = drvResult[ITM_ProductTable.PRODUCTID_FLD];
							dgrdData[dgrdData.Row, ITM_ProductTable.TABLE_NAME + ITM_ProductTable.CODE_FLD] = drvResult[ITM_ProductTable.CODE_FLD];
							dgrdData[dgrdData.Row, ITM_ProductTable.TABLE_NAME + ITM_ProductTable.DESCRIPTION_FLD] = drvResult[ITM_ProductTable.DESCRIPTION_FLD];
							dgrdData[dgrdData.Row, ITM_ProductTable.TABLE_NAME + ITM_ProductTable.REVISION_FLD] = drvResult[ITM_ProductTable.REVISION_FLD];
							dgrdData[dgrdData.Row, MST_UnitOfMeasureTable.TABLE_NAME + MST_UnitOfMeasureTable.CODE_FLD] = drvResult[MST_UnitOfMeasureTable.TABLE_NAME + MST_UnitOfMeasureTable.CODE_FLD];
						}
						else
						{
							dgrdData.Columns[CST_ActCostAllocationDetailTable.PRODUCTID_FLD].Value = drvResult[ITM_ProductTable.PRODUCTID_FLD];
							dgrdData.Columns[ITM_ProductTable.TABLE_NAME + ITM_ProductTable.CODE_FLD].Value = drvResult[ITM_ProductTable.CODE_FLD];
							dgrdData.Columns[ITM_ProductTable.TABLE_NAME + ITM_ProductTable.DESCRIPTION_FLD].Value = drvResult[ITM_ProductTable.DESCRIPTION_FLD];
							dgrdData.Columns[ITM_ProductTable.TABLE_NAME + ITM_ProductTable.REVISION_FLD].Value = drvResult[ITM_ProductTable.REVISION_FLD];
							dgrdData.Columns[MST_UnitOfMeasureTable.TABLE_NAME + MST_UnitOfMeasureTable.CODE_FLD].Value = drvResult[MST_UnitOfMeasureTable.TABLE_NAME + MST_UnitOfMeasureTable.CODE_FLD];
						}
					}
					else
					{
						blnResult = pblnAlwaysShow;
					}
					break;

				case ITM_ProductTable.TABLE_NAME + ITM_ProductTable.DESCRIPTION_FLD:
					//clear hash table for new condition
					htbCondition.Clear();

					if(!dgrdData[dgrdData.Row, CST_ActCostAllocationDetailTable.PRODUCTGROUPID_FLD].Equals(DBNull.Value))
					{
						htbCondition.Add(CST_ProductGroupTable.PRODUCTGROUPID_FLD, dgrdData[dgrdData.Row, CST_ActCostAllocationDetailTable.PRODUCTGROUPID_FLD]);
					}
					else
					{
						arrMessage[0] = dgrdData.Columns[CST_ProductGroupTable.TABLE_NAME + CST_ProductGroupTable.CODE_FLD].Caption;
						arrMessage[1] = dgrdData.Columns[ITM_ProductTable.TABLE_NAME + ITM_ProductTable.DESCRIPTION_FLD].Caption;

						PCSMessageBox.Show(ErrorCode.MESSAGE_SELECT_ONE_BEFORE_SELECT_ONE, MessageBoxIcon.Warning, arrMessage);
						dgrdData.Col = dgrdData.Columns.IndexOf(dgrdData.Columns[CST_ProductGroupTable.TABLE_NAME + CST_ProductGroupTable.CODE_FLD]);
						dgrdData.Focus();
						return false;
					}
					
					//show only make items
					htbCondition.Add(ITM_ProductTable.MAKEITEM_FLD, 1);

					// Call OpenSearchForm for selecting 
					drvResult = FormControlComponents.OpenSearchForm(PRODUCT_IN_WORKCENTER_VIEW, ITM_ProductTable.DESCRIPTION_FLD, pstrColumValue, htbCondition, pblnAlwaysShow);
					if (drvResult != null)
					{
						if(pblnAlwaysShow)
						{
							dgrdData[dgrdData.Row, ITM_ProductTable.PRODUCTID_FLD] = drvResult[ITM_ProductTable.PRODUCTID_FLD];
							dgrdData[dgrdData.Row, ITM_ProductTable.TABLE_NAME + ITM_ProductTable.CODE_FLD] = drvResult[ITM_ProductTable.CODE_FLD];
							dgrdData[dgrdData.Row, ITM_ProductTable.TABLE_NAME + ITM_ProductTable.DESCRIPTION_FLD] = drvResult[ITM_ProductTable.DESCRIPTION_FLD];
							dgrdData[dgrdData.Row, ITM_ProductTable.TABLE_NAME + ITM_ProductTable.REVISION_FLD] = drvResult[ITM_ProductTable.REVISION_FLD];
							dgrdData[dgrdData.Row, MST_UnitOfMeasureTable.TABLE_NAME + MST_UnitOfMeasureTable.CODE_FLD] = drvResult[MST_UnitOfMeasureTable.TABLE_NAME + MST_UnitOfMeasureTable.CODE_FLD];
						}
						else
						{
							dgrdData.Columns[CST_ActCostAllocationDetailTable.PRODUCTID_FLD].Value = drvResult[ITM_ProductTable.PRODUCTID_FLD];
							dgrdData.Columns[ITM_ProductTable.TABLE_NAME + ITM_ProductTable.CODE_FLD].Value = drvResult[ITM_ProductTable.CODE_FLD];
							dgrdData.Columns[ITM_ProductTable.TABLE_NAME + ITM_ProductTable.DESCRIPTION_FLD].Value = drvResult[ITM_ProductTable.DESCRIPTION_FLD];
							dgrdData.Columns[ITM_ProductTable.TABLE_NAME + ITM_ProductTable.REVISION_FLD].Value = drvResult[ITM_ProductTable.REVISION_FLD];
							dgrdData.Columns[MST_UnitOfMeasureTable.TABLE_NAME + MST_UnitOfMeasureTable.CODE_FLD].Value = drvResult[MST_UnitOfMeasureTable.TABLE_NAME + MST_UnitOfMeasureTable.CODE_FLD];
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
			//Create table
			DataTable dtbDetail = new DataTable(CST_ActCostAllocationDetailTable.TABLE_NAME);
			//Add columns
			dtbDetail.Columns.Add(CST_ActCostAllocationDetailTable.ACTCOSTALLOCATIONDETAILID_FLD, typeof(System.Int32));
			dtbDetail.Columns.Add(CST_ActCostAllocationDetailTable.ACTCOSTALLOCATIONMASTERID_FLD, typeof(System.Int32));
			dtbDetail.Columns.Add(CST_ActCostAllocationDetailTable.COSTELEMENTID_FLD, typeof(System.Int32));
			dtbDetail.Columns.Add(CST_ActCostAllocationDetailTable.DEPARTMENTID_FLD, typeof(System.Int32));
			dtbDetail.Columns.Add(CST_ActCostAllocationDetailTable.PRODUCTIONLINEID_FLD, typeof(System.Int32));
			dtbDetail.Columns.Add(CST_ActCostAllocationDetailTable.PRODUCTGROUPID_FLD, typeof(System.Int32));
			dtbDetail.Columns.Add(CST_ActCostAllocationDetailTable.PRODUCTID_FLD, typeof(System.Int32));				
			
			dtbDetail.Columns.Add(CST_ActCostAllocationDetailTable.LINE_FLD, typeof(System.Int32));

			dtbDetail.Columns.Add(STD_CostElementTable.TABLE_NAME + STD_CostElementTable.NAME_FLD, typeof(System.String));
			dtbDetail.Columns.Add(CST_ActCostAllocationDetailTable.ALLOCATIONAMOUNT_FLD, typeof(System.Double));

			dtbDetail.Columns.Add(MST_DepartmentTable.TABLE_NAME + MST_DepartmentTable.CODE_FLD, typeof(System.String));
			dtbDetail.Columns.Add(PRO_ProductionLineTable.TABLE_NAME + PRO_ProductionLineTable.CODE_FLD, typeof(System.String));
			dtbDetail.Columns.Add(CST_ProductGroupTable.TABLE_NAME + CST_ProductGroupTable.CODE_FLD, typeof(System.String));

			
			dtbDetail.Columns.Add(ITM_ProductTable.TABLE_NAME + ITM_ProductTable.CODE_FLD, typeof(System.String));
			dtbDetail.Columns.Add(ITM_ProductTable.TABLE_NAME + ITM_ProductTable.DESCRIPTION_FLD, typeof(System.String));
			dtbDetail.Columns.Add(ITM_ProductTable.TABLE_NAME + ITM_ProductTable.REVISION_FLD, typeof(System.String));
			dtbDetail.Columns.Add(MST_UnitOfMeasureTable.TABLE_NAME + MST_UnitOfMeasureTable.CODE_FLD, typeof(System.String));
			
			return dtbDetail;			
		}

		/// <summary>
		/// Lock controls for updating
		/// </summary>
		/// <param name="pblnLock"></param>
		/// <author> Tran Quoc Tuan </author>
		/// <created date> 
		/// 21 Feb 2005
		/// </created>
		private void LockControl(bool pblnLock)
		{
			
			//Set select buttons for grid
			dgrdData.Splits[0].DisplayColumns[STD_CostElementTable.TABLE_NAME + STD_CostElementTable.NAME_FLD].Button = !pblnLock;
			dgrdData.Splits[0].DisplayColumns[MST_DepartmentTable.TABLE_NAME + MST_DepartmentTable.CODE_FLD].Button = !pblnLock;
			dgrdData.Splits[0].DisplayColumns[PRO_ProductionLineTable.TABLE_NAME + PRO_ProductionLineTable.CODE_FLD].Button = !pblnLock;
			dgrdData.Splits[0].DisplayColumns[CST_ProductGroupTable.TABLE_NAME + CST_ProductGroupTable.CODE_FLD].Button = !pblnLock;
			dgrdData.Splits[0].DisplayColumns[ITM_ProductTable.TABLE_NAME + ITM_ProductTable.CODE_FLD].Button = !pblnLock;
			dgrdData.Splits[0].DisplayColumns[ITM_ProductTable.TABLE_NAME + ITM_ProductTable.DESCRIPTION_FLD].Button = !pblnLock;
			
			//
			btnAdd.Enabled	= pblnLock;
			btnEdit.Enabled = pblnLock && (dgrdData.RowCount > 0);
			btnDelete.Enabled	= btnEdit.Enabled;
			btnRollup.Enabled	= btnEdit.Enabled;			
			btnPrint.Enabled	= btnEdit.Enabled;
			btnDelChargeAllocation.Enabled = btnDelAllocation.Enabled = 
			btnAllocate.Enabled = btnEdit.Enabled;

			
			btnPeriodSearch.Enabled = pblnLock;
			dgrdData.AllowAddNew = !pblnLock;
			dgrdData.AllowDelete = !pblnLock;
			dgrdData.AllowUpdate = !pblnLock;

			btnSave.Enabled = !pblnLock;
			dtmFromDate.Enabled = !pblnLock;
			dtmToDate.Enabled = !pblnLock;
			btnImport.Enabled = !pblnLock && (enuFormAction == EnumAction.Add);

			txtPeriod.Focus();			
		}
		
		private void VOToControls(CST_ActCostAllocationMasterVO pvoMaster)
		{			
			if (pvoMaster.ActCostAllocationMasterID <= 0)
			{
				ResetControlValue();
				return;
			}
			
			txtCurrency.Tag = pvoMaster.CurrencyID;
			if(pvoMaster.CurrencyID == SystemProperty.HomeCurrencyID)
			{
				txtCurrency.Text = SystemProperty.HomeCurrency;
			}
			else
			{
				txtCurrency.Text = boMaster.GetCurrencyCode(pvoMaster.CurrencyID);
			}

			txtPeriod.Text = pvoMaster.PeriodName;
			txtPeriod.Tag = pvoMaster.ActCostAllocationMasterID;

			dtmFromDate.Value = pvoMaster.FromDate;
			dtmToDate.Value = pvoMaster.ToDate;
			if(pvoMaster.RollupDate == DateTime.MinValue)
			{
				dtmRollupDate.Value = DBNull.Value;
			}
			else
			{
				dtmRollupDate.Value = pvoMaster.RollupDate;
			}
		}

		/// <summary>
		/// Fill related data on controls when select Cycle
		/// </summary>
		/// <param name="pblnAlwaysShowDialog"></param>
		private bool SelectPeriod(bool pblnAlwaysShowDialog)
		{
			Hashtable htbCriteria = new Hashtable();
			DataRowView drwResult = null;
			
			if(cboCCN.SelectedValue != null)
			{
				htbCriteria.Add(CST_ActCostAllocationMasterTable.CCNID_FLD, cboCCN.SelectedValue);
			}
			else
			{
				htbCriteria.Add(CST_ActCostAllocationMasterTable.CCNID_FLD, 0);
			}

			//Call OpenSearchForm for selecting cycle
			drwResult = FormControlComponents.OpenSearchForm(CST_ActCostAllocationMasterTable.TABLE_NAME, CST_ActCostAllocationMasterTable.PERIODNAME_FLD, txtPeriod.Text, htbCriteria, pblnAlwaysShowDialog);
			
			// If has Master location matched searching condition, fill values to form's controls
			if (drwResult != null)
			{
				voMaster.ActCostAllocationMasterID = int.Parse(drwResult[CST_ActCostAllocationMasterTable.ACTCOSTALLOCATIONMASTERID_FLD].ToString());
				voMaster.CCNID = int.Parse(drwResult[CST_ActCostAllocationMasterTable.CCNID_FLD].ToString());
				voMaster.CurrencyID = int.Parse(drwResult[CST_ActCostAllocationMasterTable.CURRENCYID_FLD].ToString());
				voMaster.FromDate = (DateTime)drwResult[CST_ActCostAllocationMasterTable.FROMDATE_FLD];
				voMaster.ToDate = (DateTime)drwResult[CST_ActCostAllocationMasterTable.TODATE_FLD];
				voMaster.PeriodName = drwResult[CST_ActCostAllocationMasterTable.PERIODNAME_FLD].ToString();

				if(drwResult[CST_ActCostAllocationMasterTable.ROLLUPDATE_FLD].Equals(DBNull.Value)
				|| drwResult[CST_ActCostAllocationMasterTable.ROLLUPDATE_FLD].ToString().Equals(string.Empty))
				{
					voMaster.RollupDate = DateTime.MinValue;
				}
				else
				{
					voMaster.RollupDate = (DateTime)drwResult[CST_ActCostAllocationMasterTable.ROLLUPDATE_FLD];
				}

				//Fill data from datarow to controls
				VOToControls(voMaster);

				//Fill data from datarow to controls
				LoadDetailGrid(voMaster.ActCostAllocationMasterID);

				try
				{
					txtTotalAmount.Text = Convert.ToDecimal(dtbDetail.Compute("SUM(AllocationAmount)", string.Empty)).ToString(Constants.DECIMAL_NUMBERFORMAT);
				}
				catch{}

				btnEdit.Enabled = true;
				btnDelete.Enabled = true;
				btnRollup.Enabled = true;
				btnAllocate.Enabled = true;
				// added by dungla: enable print button
				btnPrint.Enabled = true;
			}
			else
			{
				if(!pblnAlwaysShowDialog)
				{	
					btnEdit.Enabled = false;
					btnDelete.Enabled = false;
					btnRollup.Enabled = false;
					btnAllocate.Enabled = false;
					// added by dungla
					btnPrint.Enabled = false;
					
					txtPeriod.Focus();
					return false;
				}
			}

			return true;
		}

		/// <summary>
		/// Validate data before updating into database
		/// </summary>
		/// <returns></returns>
		/// <author> Tran Quoc Tuan </author>
		/// <created date> 
		/// 21 Feb 2005
		/// </created>
		private bool ValidateData()
		{
			if (FormControlComponents.CheckMandatory(txtPeriod))
			{
				PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Exclamation);
				txtPeriod.Focus();				
				return false;
			}
			
			if(boMaster.GetObjectVO(txtPeriod.Text) != null && !txtPeriod.Text.Trim().ToUpper().Equals(voMaster.PeriodName.ToUpper()))
			{
				PCSMessageBox.Show(ErrorCode.DUPLICATE_KEY, MessageBoxIcon.Exclamation);
				txtPeriod.Focus();
				return false;
			}

			//Check from date
			if(dtmFromDate.ValueIsDbNull  || dtmFromDate.Text.Trim() == string.Empty)
			{
				PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Exclamation);
				dtmFromDate.Focus();				
				return false;
			}				
			
			if(((DateTime)dtmFromDate.Value).Day != 1)
			{
				string[] arrMessage = new string[2];
				arrMessage[0] = lblFirstDayOfMonth.Text + " (1)";
				arrMessage[1] = lblFromDate.Text;

				PCSMessageBox.Show(ErrorCode.MESSAGE_RELATION_REQUIRE, MessageBoxIcon.Exclamation, arrMessage);
				dtmFromDate.Focus();				
				return false;
			}

			//Check to date
			if (dtmToDate.ValueIsDbNull || dtmToDate.Text.Trim() == string.Empty)
			{
				PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Exclamation);
				dtmToDate.Focus();				
				return false;
			}
			
			if(((DateTime)dtmToDate.Value).Day != DateTime.DaysInMonth(((DateTime)dtmToDate.Value).Year, ((DateTime)dtmToDate.Value).Month))
			{
				string[] arrMessage = new string[2];
				arrMessage[0] = lblEndDayOfMonth.Text + " (" + DateTime.DaysInMonth(((DateTime)dtmToDate.Value).Year, ((DateTime)dtmToDate.Value).Month) + ")";
				arrMessage[1] = lblToDate.Text;

				PCSMessageBox.Show(ErrorCode.MESSAGE_RELATION_REQUIRE, MessageBoxIcon.Exclamation, arrMessage);
				dtmToDate.Focus();				
				return false;
			}

			//Check if date is invalid
			if(DateTime.Parse(dtmFromDate.Value.ToString()) > DateTime.Parse(dtmToDate.Value.ToString()))
			{
				string[] arrMessage = new string[2];
				arrMessage[0] = lblToDate.Text;
				arrMessage[1] = lblFromDate.Text;

				PCSMessageBox.Show(ErrorCode.MESSAGE_GREATER_THAN, MessageBoxIcon.Exclamation, arrMessage);
				dtmToDate.Focus();				
				return false;
			}
			
			//assign value from controls to VO object
			voMaster.CCNID = int.Parse(cboCCN.SelectedValue.ToString());
			voMaster.PeriodName = txtPeriod.Text.Trim();
			voMaster.FromDate = (DateTime)dtmFromDate.Value;
			voMaster.ToDate = (DateTime)dtmToDate.Value;
			voMaster.CurrencyID = int.Parse(txtCurrency.Tag.ToString());

			if(dtmRollupDate.ValueIsDbNull || dtmRollupDate.Text.Trim() == string.Empty)
			{
				voMaster.RollupDate = DateTime.MinValue;
			}
			else
			{
				voMaster.RollupDate = (DateTime)dtmRollupDate.Value;
			}
			
			//Check overlap
			if(boMaster.IsPeriodOverlap(voMaster))
			{
				PCSMessageBox.Show(ErrorCode.MESSAGE_DATE_IS_OVERLAP, MessageBoxIcon.Exclamation);					
				dtmFromDate.Focus();				
				return false;
			}

			//Call update data to force grid update data
			dgrdData.UpdateData();
			
			//variable to indicate grid's row index
			int intRowIndex = -1;
			foreach (DataRow drowRow in dtbDetail.Rows)
			{
				if(drowRow.RowState == DataRowState.Deleted) continue;

				intRowIndex++;
				// check if omit Cost element
				if( drowRow[CST_ActCostAllocationDetailTable.COSTELEMENTID_FLD].Equals(string.Empty)
					|| drowRow[CST_ActCostAllocationDetailTable.COSTELEMENTID_FLD].Equals(DBNull.Value))
				{
					// Please input Item field for each records.
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Exclamation);
					dgrdData.Row = intRowIndex;
					dgrdData.Col = dgrdData.Columns.IndexOf(dgrdData.Columns[STD_CostElementTable.TABLE_NAME + STD_CostElementTable.NAME_FLD]);
					dgrdData.Focus();

					return false;
				}

				if(!FormControlComponents.IsPositiveNumeric(drowRow[CST_ActCostAllocationDetailTable.ALLOCATIONAMOUNT_FLD].ToString()))
				{
					// Please input Item field for each records.
					PCSMessageBox.Show(ErrorCode.MESSAGE_OUTPROCESSING_INVALID_NUMBER, MessageBoxIcon.Exclamation);
					dgrdData.Row = intRowIndex;
					dgrdData.Col = dgrdData.Columns.IndexOf(dgrdData.Columns[CST_ActCostAllocationDetailTable.ALLOCATIONAMOUNT_FLD]);
					dgrdData.Focus();

					return false;
				}

				if(decimal.Parse(drowRow[CST_ActCostAllocationDetailTable.ALLOCATIONAMOUNT_FLD].ToString()) == 0)
				{
					// Please input Item field for each records.
					PCSMessageBox.Show(ErrorCode.MESSAGE_OUTPROCESSING_INVALID_NUMBER, MessageBoxIcon.Exclamation);
					dgrdData.Row = intRowIndex;
					dgrdData.Col = dgrdData.Columns.IndexOf(dgrdData.Columns[CST_ActCostAllocationDetailTable.ALLOCATIONAMOUNT_FLD]);
					dgrdData.Focus();

					return false;
				}

				//Check duplicate row
				string strFilter = CST_ActCostAllocationDetailTable.COSTELEMENTID_FLD + "=" + drowRow[CST_ActCostAllocationDetailTable.COSTELEMENTID_FLD].ToString();
				strFilter += " AND " + CST_ActCostAllocationDetailTable.DEPARTMENTID_FLD + " IS NULL";
				strFilter += " AND " + CST_ActCostAllocationDetailTable.PRODUCTIONLINEID_FLD + " IS NULL";
				strFilter += " AND " + CST_ActCostAllocationDetailTable.PRODUCTGROUPID_FLD + " IS NULL";
				strFilter += " AND " + CST_ActCostAllocationDetailTable.PRODUCTID_FLD + " IS NULL";

				string strFilter1 = CST_ActCostAllocationDetailTable.COSTELEMENTID_FLD + "=" + drowRow[CST_ActCostAllocationDetailTable.COSTELEMENTID_FLD].ToString();
				strFilter1 += " AND " + CST_ActCostAllocationDetailTable.DEPARTMENTID_FLD + "=" + drowRow[CST_ActCostAllocationDetailTable.DEPARTMENTID_FLD].ToString();
				strFilter1 += " AND " + CST_ActCostAllocationDetailTable.PRODUCTIONLINEID_FLD + " IS NULL";
				strFilter1 += " AND " + CST_ActCostAllocationDetailTable.PRODUCTGROUPID_FLD + " IS NULL";
				strFilter1 += " AND " + CST_ActCostAllocationDetailTable.PRODUCTID_FLD + " IS NULL";

				string strFilter2 = CST_ActCostAllocationDetailTable.COSTELEMENTID_FLD + "=" + drowRow[CST_ActCostAllocationDetailTable.COSTELEMENTID_FLD].ToString();
				strFilter2 += " AND " + CST_ActCostAllocationDetailTable.DEPARTMENTID_FLD + "=" + drowRow[CST_ActCostAllocationDetailTable.DEPARTMENTID_FLD].ToString();
				strFilter2 += " AND " + CST_ActCostAllocationDetailTable.PRODUCTIONLINEID_FLD + "=" + drowRow[CST_ActCostAllocationDetailTable.PRODUCTIONLINEID_FLD].ToString();
				strFilter2 += " AND " + CST_ActCostAllocationDetailTable.PRODUCTGROUPID_FLD + " IS NULL";
				strFilter2 += " AND " + CST_ActCostAllocationDetailTable.PRODUCTID_FLD + " IS NULL";

				string strFilter3 = CST_ActCostAllocationDetailTable.COSTELEMENTID_FLD + "=" + drowRow[CST_ActCostAllocationDetailTable.COSTELEMENTID_FLD].ToString();
				strFilter3 += " AND " + CST_ActCostAllocationDetailTable.DEPARTMENTID_FLD + "=" + drowRow[CST_ActCostAllocationDetailTable.DEPARTMENTID_FLD].ToString();
				strFilter3 += " AND " + CST_ActCostAllocationDetailTable.PRODUCTIONLINEID_FLD + "=" + drowRow[CST_ActCostAllocationDetailTable.PRODUCTIONLINEID_FLD].ToString();
				strFilter3 += " AND " + CST_ActCostAllocationDetailTable.PRODUCTGROUPID_FLD + "=" + drowRow[CST_ActCostAllocationDetailTable.PRODUCTGROUPID_FLD].ToString();
				strFilter3 += " AND " + CST_ActCostAllocationDetailTable.PRODUCTID_FLD + " IS NULL";
				
				string strFilter4 = CST_ActCostAllocationDetailTable.COSTELEMENTID_FLD + "=" + drowRow[CST_ActCostAllocationDetailTable.COSTELEMENTID_FLD].ToString();
				strFilter4 += " AND " + CST_ActCostAllocationDetailTable.DEPARTMENTID_FLD + "=" + drowRow[CST_ActCostAllocationDetailTable.DEPARTMENTID_FLD].ToString();
				strFilter4 += " AND " + CST_ActCostAllocationDetailTable.PRODUCTIONLINEID_FLD + "=" + drowRow[CST_ActCostAllocationDetailTable.PRODUCTIONLINEID_FLD].ToString();
				strFilter4 += " AND " + CST_ActCostAllocationDetailTable.PRODUCTGROUPID_FLD + "=" + drowRow[CST_ActCostAllocationDetailTable.PRODUCTGROUPID_FLD].ToString();				
				strFilter4 += " AND " + CST_ActCostAllocationDetailTable.PRODUCTID_FLD + "=" + drowRow[CST_ActCostAllocationDetailTable.PRODUCTID_FLD].ToString();

				DataRow[] arrDuplicateRow;
				if(drowRow[CST_ActCostAllocationDetailTable.PRODUCTID_FLD].ToString().Trim() != string.Empty)
				{
					arrDuplicateRow = dtbDetail.Select(strFilter4);
					if(arrDuplicateRow.Length > 1)
					{
						PCSMessageBox.Show(ErrorCode.DUPLICATE_KEY, MessageBoxIcon.Exclamation);						
						dgrdData.Col = dgrdData.Columns.IndexOf(dgrdData.Columns[ITM_ProductTable.TABLE_NAME + ITM_ProductTable.CODE_FLD]);
						dgrdData.Row = intRowIndex;
						dgrdData.Focus();

						return false;
					}
				}
				else if(drowRow[CST_ActCostAllocationDetailTable.PRODUCTGROUPID_FLD].ToString().Trim() != string.Empty)
				{
					arrDuplicateRow = dtbDetail.Select(strFilter3);
					if(arrDuplicateRow.Length > 1)
					{
						PCSMessageBox.Show(ErrorCode.DUPLICATE_KEY, MessageBoxIcon.Exclamation);						
						dgrdData.Col = dgrdData.Columns.IndexOf(dgrdData.Columns[CST_ProductGroupTable.TABLE_NAME + CST_ProductGroupTable.CODE_FLD]);
						dgrdData.Row = intRowIndex;
						dgrdData.Focus();

						return false;
					}
				}
				else if(drowRow[CST_ActCostAllocationDetailTable.PRODUCTIONLINEID_FLD].ToString().Trim() != string.Empty)
				{
					arrDuplicateRow = dtbDetail.Select(strFilter2);
					if(arrDuplicateRow.Length > 1)
					{
						PCSMessageBox.Show(ErrorCode.DUPLICATE_KEY, MessageBoxIcon.Exclamation);						
						dgrdData.Col = dgrdData.Columns.IndexOf(dgrdData.Columns[PRO_ProductionLineTable.TABLE_NAME + PRO_ProductionLineTable.CODE_FLD]);
						dgrdData.Row = intRowIndex;
						dgrdData.Focus();

						return false;
					}
				}
				else if(drowRow[CST_ActCostAllocationDetailTable.DEPARTMENTID_FLD].ToString().Trim() != string.Empty)
				{
					arrDuplicateRow = dtbDetail.Select(strFilter1);
					if(arrDuplicateRow.Length > 1)
					{
						PCSMessageBox.Show(ErrorCode.DUPLICATE_KEY, MessageBoxIcon.Exclamation);						
						dgrdData.Col = dgrdData.Columns.IndexOf(dgrdData.Columns[MST_DepartmentTable.TABLE_NAME + MST_DepartmentTable.CODE_FLD]);
						dgrdData.Row = intRowIndex;
						dgrdData.Focus();

						return false;
					}
				}
				else if(drowRow[CST_ActCostAllocationDetailTable.COSTELEMENTID_FLD].ToString().Trim() != string.Empty)
				{
					arrDuplicateRow = dtbDetail.Select(strFilter);
					if(arrDuplicateRow.Length > 1)
					{
						PCSMessageBox.Show(ErrorCode.DUPLICATE_KEY, MessageBoxIcon.Exclamation);						
						dgrdData.Col = dgrdData.Columns.IndexOf(dgrdData.Columns[STD_CostElementTable.TABLE_NAME + STD_CostElementTable.NAME_FLD]);
						dgrdData.Row = intRowIndex;
						dgrdData.Focus();

						return false;
					}
				}
			}			

			return true;			
		}
		
		/// <summary>
		/// Format datagrid
		/// </summary>
		private void FormatDataGrid()
		{
			//Restore layout
			FormControlComponents.RestoreGridLayout(dgrdData, dtbGridLayOut);
				
			//Change display format
			dgrdData.Columns[CST_ActCostAllocationDetailTable.ALLOCATIONAMOUNT_FLD].NumberFormat = Constants.EDIT_NUM_MASK;
			dgrdData.Columns[CST_ActCostAllocationDetailTable.ALLOCATIONAMOUNT_FLD].Editor = numValue;

			//Lock columns
			dgrdData.Splits[0].DisplayColumns[CST_ActCostAllocationDetailTable.LINE_FLD].Locked = true;
			dgrdData.Splits[0].DisplayColumns[ITM_ProductTable.TABLE_NAME + ITM_ProductTable.REVISION_FLD].Locked = true;
			dgrdData.Splits[0].DisplayColumns[MST_UnitOfMeasureTable.TABLE_NAME + MST_UnitOfMeasureTable.CODE_FLD].Locked = true;
			
			//set resizing status & row height
			dgrdData.AllowRowSizing = RowSizingEnum.AllRows;
			dgrdData.RowHeight = numValue.Height;
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
					case MST_DepartmentTable.TABLE_NAME + MST_DepartmentTable.CODE_FLD:

						dgrdData.Columns[CST_ActCostAllocationDetailTable.DEPARTMENTID_FLD].Value = DBNull.Value;
						dgrdData.Columns[MST_DepartmentTable.TABLE_NAME + MST_DepartmentTable.CODE_FLD].Value = string.Empty;

						dgrdData.Columns[CST_ActCostAllocationDetailTable.PRODUCTIONLINEID_FLD].Value = DBNull.Value;
						dgrdData.Columns[PRO_ProductionLineTable.TABLE_NAME + PRO_ProductionLineTable.CODE_FLD].Value = string.Empty;

						//clear product group columns
						dgrdData.Columns[CST_ActCostAllocationDetailTable.PRODUCTGROUPID_FLD].Value = DBNull.Value;
						dgrdData.Columns[CST_ProductGroupTable.TABLE_NAME + CST_ProductGroupTable.CODE_FLD].Value = string.Empty;
							
						//clear product columns
						dgrdData.Columns[CST_ActCostAllocationDetailTable.PRODUCTID_FLD].Value = DBNull.Value;
						dgrdData.Columns[ITM_ProductTable.TABLE_NAME + ITM_ProductTable.CODE_FLD].Value = string.Empty;
						dgrdData.Columns[ITM_ProductTable.TABLE_NAME + ITM_ProductTable.DESCRIPTION_FLD].Value = string.Empty;
						dgrdData.Columns[ITM_ProductTable.TABLE_NAME + ITM_ProductTable.REVISION_FLD].Value = string.Empty;
						dgrdData.Columns[MST_UnitOfMeasureTable.TABLE_NAME + MST_UnitOfMeasureTable.CODE_FLD].Value = string.Empty;
						break;

					case PRO_ProductionLineTable.TABLE_NAME + PRO_ProductionLineTable.CODE_FLD:
						//clear production line columns
						dgrdData.Columns[CST_ActCostAllocationDetailTable.PRODUCTIONLINEID_FLD].Value = DBNull.Value;
						dgrdData.Columns[PRO_ProductionLineTable.TABLE_NAME + PRO_ProductionLineTable.CODE_FLD].Value = string.Empty;
						
						//clear product group columns
						dgrdData.Columns[CST_ActCostAllocationDetailTable.PRODUCTGROUPID_FLD].Value = DBNull.Value;
						dgrdData.Columns[CST_ProductGroupTable.TABLE_NAME + CST_ProductGroupTable.CODE_FLD].Value = string.Empty;
							
						//clear product columns
						dgrdData.Columns[CST_ActCostAllocationDetailTable.PRODUCTID_FLD].Value = DBNull.Value;
						dgrdData.Columns[ITM_ProductTable.TABLE_NAME + ITM_ProductTable.CODE_FLD].Value = string.Empty;
						dgrdData.Columns[ITM_ProductTable.TABLE_NAME + ITM_ProductTable.DESCRIPTION_FLD].Value = string.Empty;
						dgrdData.Columns[ITM_ProductTable.TABLE_NAME + ITM_ProductTable.REVISION_FLD].Value = string.Empty;
						dgrdData.Columns[MST_UnitOfMeasureTable.TABLE_NAME + MST_UnitOfMeasureTable.CODE_FLD].Value = string.Empty;
						break;

					case CST_ProductGroupTable.TABLE_NAME + CST_ProductGroupTable.CODE_FLD:
						//clear product group columns							
						dgrdData.Columns[CST_ActCostAllocationDetailTable.PRODUCTGROUPID_FLD].Value = DBNull.Value;
						dgrdData.Columns[CST_ProductGroupTable.TABLE_NAME + CST_ProductGroupTable.CODE_FLD].Value = string.Empty;
							
						//clear product columns
						dgrdData.Columns[CST_ActCostAllocationDetailTable.PRODUCTID_FLD].Value = DBNull.Value;
						dgrdData.Columns[ITM_ProductTable.TABLE_NAME + ITM_ProductTable.CODE_FLD].Value = string.Empty;
						dgrdData.Columns[ITM_ProductTable.TABLE_NAME + ITM_ProductTable.DESCRIPTION_FLD].Value = string.Empty;
						dgrdData.Columns[ITM_ProductTable.TABLE_NAME + ITM_ProductTable.REVISION_FLD].Value = string.Empty;
						dgrdData.Columns[MST_UnitOfMeasureTable.TABLE_NAME + MST_UnitOfMeasureTable.CODE_FLD].Value = string.Empty;
						break;

					case ITM_ProductTable.TABLE_NAME + ITM_ProductTable.CODE_FLD:
					case ITM_ProductTable.TABLE_NAME + ITM_ProductTable.DESCRIPTION_FLD:
						//clear product columns
						dgrdData.Columns[CST_ActCostAllocationDetailTable.PRODUCTID_FLD].Value = DBNull.Value;

						dgrdData.Columns[ITM_ProductTable.TABLE_NAME + ITM_ProductTable.CODE_FLD].Value = string.Empty;
						dgrdData.Columns[ITM_ProductTable.TABLE_NAME + ITM_ProductTable.DESCRIPTION_FLD].Value = string.Empty;
						dgrdData.Columns[ITM_ProductTable.TABLE_NAME + ITM_ProductTable.REVISION_FLD].Value = string.Empty;
						dgrdData.Columns[MST_UnitOfMeasureTable.TABLE_NAME + MST_UnitOfMeasureTable.CODE_FLD].Value = string.Empty;
						break;						
				}
			}
			else
			{
				switch (pstrColumnName)
				{
					case MST_DepartmentTable.TABLE_NAME + MST_DepartmentTable.CODE_FLD:

						dgrdData[dgrdData.Row, CST_ActCostAllocationDetailTable.DEPARTMENTID_FLD] = DBNull.Value;
						dgrdData[dgrdData.Row, MST_DepartmentTable.TABLE_NAME + MST_DepartmentTable.CODE_FLD] = string.Empty;

						dgrdData[dgrdData.Row, CST_ActCostAllocationDetailTable.PRODUCTIONLINEID_FLD] = DBNull.Value;
						dgrdData[dgrdData.Row, PRO_ProductionLineTable.TABLE_NAME + PRO_ProductionLineTable.CODE_FLD] = string.Empty;

						//clear product group columns
						dgrdData[dgrdData.Row, CST_ActCostAllocationDetailTable.PRODUCTGROUPID_FLD] = DBNull.Value;
						dgrdData[dgrdData.Row, CST_ProductGroupTable.TABLE_NAME + CST_ProductGroupTable.CODE_FLD] = string.Empty;
							
						//clear product columns
						dgrdData[dgrdData.Row, CST_ActCostAllocationDetailTable.PRODUCTID_FLD] = DBNull.Value;
						dgrdData[dgrdData.Row, ITM_ProductTable.TABLE_NAME + ITM_ProductTable.CODE_FLD] = string.Empty;
						dgrdData[dgrdData.Row, ITM_ProductTable.TABLE_NAME + ITM_ProductTable.DESCRIPTION_FLD] = string.Empty;
						dgrdData[dgrdData.Row, ITM_ProductTable.TABLE_NAME + ITM_ProductTable.REVISION_FLD] = string.Empty;
						dgrdData[dgrdData.Row, MST_UnitOfMeasureTable.TABLE_NAME + MST_UnitOfMeasureTable.CODE_FLD] = string.Empty;
						break;

					case PRO_ProductionLineTable.TABLE_NAME + PRO_ProductionLineTable.CODE_FLD:
						//clear production line
						dgrdData[dgrdData.Row, CST_ActCostAllocationDetailTable.PRODUCTIONLINEID_FLD] = DBNull.Value;
						dgrdData[dgrdData.Row, PRO_ProductionLineTable.TABLE_NAME + PRO_ProductionLineTable.CODE_FLD] = string.Empty;

						//clear product group
						dgrdData[dgrdData.Row, CST_ActCostAllocationDetailTable.PRODUCTGROUPID_FLD] = DBNull.Value;
						dgrdData[dgrdData.Row, CST_ProductGroupTable.TABLE_NAME + CST_ProductGroupTable.CODE_FLD] = string.Empty;
							
						//clear product columns
						dgrdData[dgrdData.Row, CST_ActCostAllocationDetailTable.PRODUCTID_FLD] = DBNull.Value;
						dgrdData[dgrdData.Row, ITM_ProductTable.TABLE_NAME + ITM_ProductTable.CODE_FLD] = string.Empty;
						dgrdData[dgrdData.Row, ITM_ProductTable.TABLE_NAME + ITM_ProductTable.DESCRIPTION_FLD] = string.Empty;
						dgrdData[dgrdData.Row, ITM_ProductTable.TABLE_NAME + ITM_ProductTable.REVISION_FLD] = string.Empty;
						dgrdData[dgrdData.Row, MST_UnitOfMeasureTable.TABLE_NAME + MST_UnitOfMeasureTable.CODE_FLD] = string.Empty;
						break;

					case CST_ProductGroupTable.TABLE_NAME + CST_ProductGroupTable.CODE_FLD:
						//clear product group columns							
						dgrdData[dgrdData.Row, CST_ActCostAllocationDetailTable.PRODUCTGROUPID_FLD] = DBNull.Value;							
						dgrdData[dgrdData.Row, CST_ProductGroupTable.TABLE_NAME + CST_ProductGroupTable.CODE_FLD] = string.Empty;

						//clear product columns
						dgrdData[dgrdData.Row, CST_ActCostAllocationDetailTable.PRODUCTID_FLD] = DBNull.Value;
						dgrdData[dgrdData.Row, ITM_ProductTable.TABLE_NAME + ITM_ProductTable.CODE_FLD] = string.Empty;
						dgrdData[dgrdData.Row, ITM_ProductTable.TABLE_NAME + ITM_ProductTable.DESCRIPTION_FLD] = string.Empty;
						dgrdData[dgrdData.Row, ITM_ProductTable.TABLE_NAME + ITM_ProductTable.REVISION_FLD] = string.Empty;
						dgrdData[dgrdData.Row, MST_UnitOfMeasureTable.TABLE_NAME + MST_UnitOfMeasureTable.CODE_FLD] = string.Empty;
						break;

					case ITM_ProductTable.TABLE_NAME + ITM_ProductTable.CODE_FLD:
					case ITM_ProductTable.TABLE_NAME + ITM_ProductTable.DESCRIPTION_FLD:
						
						//clear product columns
						dgrdData[dgrdData.Row, CST_ActCostAllocationDetailTable.PRODUCTID_FLD] = DBNull.Value;
						dgrdData[dgrdData.Row, ITM_ProductTable.TABLE_NAME + ITM_ProductTable.CODE_FLD] = string.Empty;
						dgrdData[dgrdData.Row, ITM_ProductTable.TABLE_NAME + ITM_ProductTable.DESCRIPTION_FLD] = string.Empty;
						dgrdData[dgrdData.Row, ITM_ProductTable.TABLE_NAME + ITM_ProductTable.REVISION_FLD] = string.Empty;
						dgrdData[dgrdData.Row, MST_UnitOfMeasureTable.TABLE_NAME + MST_UnitOfMeasureTable.CODE_FLD] = string.Empty;
						break;						
				}
			}
		}

		/// <summary>
		/// Reset value of controls for updating
		/// </summary>
		/// <param name="penuFormAction"></param>
		/// <author> Tran Quoc Tuan </author>
		/// <created date> 
		/// 21 Feb 2005
		/// </created>
		private void ResetControlValue()
		{			
			//Clear control's value
			txtPeriod.Text = string.Empty;
			txtPeriod.Tag = null;
			dtmFromDate.Value = DBNull.Value;
			dtmToDate.Value = DBNull.Value;
			dtmRollupDate.Value = DBNull.Value;

			voMaster = new CST_ActCostAllocationMasterVO();
			voMaster.PeriodName = string.Empty;

			txtCurrency.Text = SystemProperty.HomeCurrency;
			txtCurrency.Tag = SystemProperty.HomeCurrencyID;

			txtTotalAmount.Text = string.Empty;
			
			dtbDetail = BuildDetailTable();
			dgrdData.DataSource = dtbDetail;
			FormatDataGrid();
		}

		/// <summary>
		/// Load excel file into datatable
		/// </summary>
		/// <param name="pstrPath"></param>
		/// <returns></returns>
		public bool LoadExcelFile(string pstrPath)
		{
			ExcelReader objExcelReader = new ExcelReader(pstrPath);
			ActualCostDistributionSetupBO boActualCostDistributionSetup = new ActualCostDistributionSetupBO();
			

			//data cache
			DataTable dtbProduct = boActualCostDistributionSetup.GetAllProducts().Tables[0];
			DataTable dtbProductionLine = boActualCostDistributionSetup.GetAllProductionLine().Tables[0];
			DataTable dtbDepartment = boActualCostDistributionSetup.GetAllDepartment().Tables[0];
			DataTable dtbGroup = boActualCostDistributionSetup.GetAllGroup().Tables[0];
			DataSet dstCostElements = boActualCostDistributionSetup.GetAllCostElements();
			
			if (objExcelReader.Open())
			{
				//check file format
				if (objExcelReader.GetRowCount(DATA_SHT) <= 0)
				{
					PCSMessageBox.Show(ErrorCode.CAN_NOT_READ_EXCEL_FILE,MessageBoxIcon.Error);
					objExcelReader.Close();
					return false;
				}
				if ((objExcelReader.GetRowCount(DATA_SHT) <= BEGINDATA_ROW) || (objExcelReader.GetColCount(DATA_SHT) != TOTALCOL_NUM ))
				{
					objExcelReader.HighlightEntireRow(DATA_SHT,PERIOD_ROW,EXCEL_RED_COLOR);
					PCSMessageBox.Show(ErrorCode.ALLOCATION_EXCEL_FILE_NOT_CORRECT,MessageBoxIcon.Error);
					objExcelReader.Close();
					return false;
				}

				//read master information
				string strPeriodName = objExcelReader.ReadCell(DATA_SHT,PERIOD_ROW,PERIOD_COL).ToString();
				double dblFromDate = 0;
				double dblToDate = 0;
				try
				{
					dblFromDate = Convert.ToDouble(objExcelReader.ReadCell(DATA_SHT,FROMDATE_ROW,FROMDATE_COL)); 
				}
				catch
				{
					objExcelReader.HighlightEntireRow(DATA_SHT,FROMDATE_ROW,EXCEL_RED_COLOR);
					PCSMessageBox.Show(ErrorCode.ALLOCATION_EXCEL_FILE_NOT_CORRECT,MessageBoxIcon.Error);
					objExcelReader.Close();
					return false;
				}
				try
				{
					dblToDate = Convert.ToDouble(objExcelReader.ReadCell(DATA_SHT,TODATE_ROW,TODATE_COL)); 
				}
				catch
				{
					objExcelReader.HighlightEntireRow(DATA_SHT,TODATE_ROW,EXCEL_RED_COLOR);
					objExcelReader.Close();
					PCSMessageBox.Show(ErrorCode.ALLOCATION_EXCEL_FILE_NOT_CORRECT,MessageBoxIcon.Error);
					return false;
				}

				txtPeriod.Text = voMaster.PeriodName = strPeriodName;
				dtmFromDate.Value = voMaster.FromDate = EXCEL_SERIAL_DATE.AddDays(dblFromDate - DATE_OFS); //2 days for difference between excel and .net
				dtmToDate.Value = voMaster.ToDate = EXCEL_SERIAL_DATE.AddDays(dblToDate - DATE_OFS); //2 days for difference between excel and .net				

				//read detail lines
				int intRowCount = objExcelReader.GetRowCount(DATA_SHT);
				dtbDetail = BuildDetailTable();
				for (int intRow = BEGINDATA_ROW; intRow < intRowCount; intRow++)
				{
					string strCostElement = objExcelReader.ReadCell(DATA_SHT,intRow,COSTELEMENT_COL).ToString();
					decimal decAllocationAmount = 0;
					try
					{
						decAllocationAmount =  Convert.ToDecimal(objExcelReader.ReadCell(DATA_SHT,intRow,ALLOCATIONAMOUNT_COL));
					}
					catch
					{
						objExcelReader.HighlightEntireRow(DATA_SHT,intRow,EXCEL_RED_COLOR);
						PCSMessageBox.Show(ErrorCode.ALLOCATION_EXCEL_FILE_NOT_CORRECT,MessageBoxIcon.Error);
						objExcelReader.Close();
						return false;
					}
					string strDepartment = objExcelReader.ReadCell(DATA_SHT,intRow,DEPARTMENT_COL).ToString().Trim();
					string strProductionLine = objExcelReader.ReadCell(DATA_SHT,intRow,PRODUCTIONLINE_COL).ToString().Trim();
					string strGroup = objExcelReader.ReadCell(DATA_SHT,intRow,GROUP_COL).ToString().Trim();
					string strPartNo = objExcelReader.ReadCell(DATA_SHT,intRow,PARTNO_COL).ToString().Trim();
					string strModel = objExcelReader.ReadCell(DATA_SHT,intRow,MODEL_COL).ToString().Trim();
					string strPartName = objExcelReader.ReadCell(DATA_SHT,intRow,PARTNAME_COL).ToString().Trim();
					
					DataRow[] arrProducts;

					string strFilter = string.Empty;
					//department
					if (strDepartment != string.Empty)
					{
						strFilter += MST_DepartmentTable.TABLE_NAME + MST_DepartmentTable.CODE_FLD + "='" + strDepartment + "' AND ";
						//production line
						if (strProductionLine != string.Empty)
						{
							strFilter += PRO_ProductionLineTable.TABLE_NAME + PRO_ProductionLineTable.CODE_FLD + "='" + strProductionLine + "' AND ";
							//group
							if (strGroup != string.Empty)
							{
								strFilter += CST_ProductGroupTable.TABLE_NAME + CST_ProductGroupTable.CODE_FLD + "='" + strGroup + "' AND ";
								//product
								if (strPartNo != string.Empty)
								{
									strFilter += ITM_ProductTable.TABLE_NAME + ITM_ProductTable.CODE_FLD + "='" + strPartNo + "' AND ";
									strFilter += ITM_ProductTable.DESCRIPTION_FLD + "='" + strPartName + "' AND ";
									strFilter += ITM_ProductTable.REVISION_FLD + "='" + strModel + "' AND ";
								} 
								else
								{
									if ((strPartName != string.Empty) || (strModel != string.Empty))
									{
										objExcelReader.HighlightEntireRow(DATA_SHT,intRow,EXCEL_RED_COLOR);
										PCSMessageBox.Show(ErrorCode.PRODUCT_COST_ELEMENT_NOT_CORRECT,MessageBoxIcon.Error);
										objExcelReader.Close();
										return false;
									}
									else
									{
										//entire group
									}
								}
							} 
							else
							{
								if ((strPartNo != string.Empty) || (strPartName != string.Empty) || (strModel != string.Empty))
								{
									objExcelReader.HighlightEntireRow(DATA_SHT,intRow,EXCEL_RED_COLOR);
									PCSMessageBox.Show(ErrorCode.PRODUCT_COST_ELEMENT_NOT_CORRECT,MessageBoxIcon.Error);
									objExcelReader.Close();
									return false;
								}
								else
								{
									//entire production line
								}
							}
						} 
						else
						{
							if ((strGroup != string.Empty) || (strPartNo != string.Empty) || (strPartName != string.Empty) || (strModel != string.Empty))
							{
								objExcelReader.HighlightEntireRow(DATA_SHT,intRow,EXCEL_RED_COLOR);
								PCSMessageBox.Show(ErrorCode.PRODUCT_COST_ELEMENT_NOT_CORRECT,MessageBoxIcon.Error);
								objExcelReader.Close();
								return false;
							}
							else
							{
								//entire department
							}
						}
					} 
					else
					{
						if ((strProductionLine != string.Empty) || (strGroup != string.Empty) || (strPartNo != string.Empty) || (strPartName != string.Empty) || (strModel != string.Empty))
						{
							objExcelReader.HighlightEntireRow(DATA_SHT,intRow,EXCEL_RED_COLOR);
							PCSMessageBox.Show(ErrorCode.PRODUCT_COST_ELEMENT_NOT_CORRECT,MessageBoxIcon.Error);
							objExcelReader.Close();
							return false;
						}
						else
						{
							//entire factory
						}
					}


					strFilter += " TRUE";
					
					DataRow drowDetail = dtbDetail.NewRow();
					drowDetail[CST_ActCostAllocationDetailTable.LINE_FLD] = intRow - BEGINDATA_ROW + 1;
					drowDetail[CST_ActCostAllocationDetailTable.ALLOCATIONAMOUNT_FLD] = decAllocationAmount;

					if (strPartNo != string.Empty)
					{
						if (dtbProduct.Select(ITM_ProductTable.CODE_FLD + "='" + strPartNo + "'"
							+ " AND " + ITM_ProductTable.DESCRIPTION_FLD + "='" + strPartName + "'"
							+ " AND " + ITM_ProductTable.REVISION_FLD + "='" + strModel + "'").Length == 0)
						{
							drowDetail.Delete();
							objExcelReader.HighlightEntireRow(DATA_SHT,intRow,EXCEL_RED_COLOR);
							objExcelReader.Close();
							PCSMessageBox.Show(ErrorCode.PRODUCT_COST_ELEMENT_NOT_CORRECT, MessageBoxIcon.Error);
							return false;
						}
						DataRow drowProduct = dtbProduct.Select(ITM_ProductTable.CODE_FLD + "='" + strPartNo + "'"
							+ " AND " + ITM_ProductTable.DESCRIPTION_FLD + "='" + strPartName + "'"
							+ " AND " + ITM_ProductTable.REVISION_FLD + "='" + strModel + "'")[0];
						drowDetail[CST_ActCostAllocationDetailTable.PRODUCTID_FLD] = drowProduct[ITM_ProductTable.PRODUCTID_FLD];
						drowDetail[MST_UnitOfMeasureTable.TABLE_NAME + MST_UnitOfMeasureTable.CODE_FLD] = drowProduct[MST_UnitOfMeasureTable.TABLE_NAME + MST_UnitOfMeasureTable.CODE_FLD];
					}
						
					drowDetail[ITM_ProductTable.TABLE_NAME + ITM_ProductTable.CODE_FLD] = strPartNo;//drowProduct[ITM_ProductTable.TABLE_NAME + ITM_ProductTable.CODE_FLD];
					drowDetail[ITM_ProductTable.TABLE_NAME + ITM_ProductTable.REVISION_FLD] = strModel;//drowProduct[ITM_ProductTable.REVISION_FLD];
					drowDetail[ITM_ProductTable.TABLE_NAME + ITM_ProductTable.DESCRIPTION_FLD] = strPartName;//drowProduct[ITM_ProductTable.DESCRIPTION_FLD];
						
					if (strProductionLine != string.Empty)
					{
						if (dtbProductionLine.Select(PRO_ProductionLineTable.CODE_FLD + "='" + strProductionLine + "'").Length == 0)
						{
							drowDetail.Delete();
							objExcelReader.HighlightEntireRow(DATA_SHT,intRow,EXCEL_RED_COLOR);
							objExcelReader.Close();
							PCSMessageBox.Show(ErrorCode.PRODUCT_COST_ELEMENT_NOT_CORRECT, MessageBoxIcon.Error);
							return false;
						}
						DataRow drowInfo = dtbProductionLine.Select(PRO_ProductionLineTable.CODE_FLD + "='" + strProductionLine + "'")[0];
						drowDetail[CST_ActCostAllocationDetailTable.PRODUCTIONLINEID_FLD] = drowInfo[ITM_ProductTable.PRODUCTIONLINEID_FLD];
					}
					drowDetail[PRO_ProductionLineTable.TABLE_NAME + PRO_ProductionLineTable.CODE_FLD] = strProductionLine;//drowProduct[PRO_ProductionLineTable.TABLE_NAME + PRO_ProductionLineTable.CODE_FLD];
						
					if (strGroup != string.Empty)
					{
						if (dtbGroup.Select(CST_ProductGroupTable.CODE_FLD + "='" + strGroup + "'").Length == 0)
						{
							drowDetail.Delete();
							objExcelReader.HighlightEntireRow(DATA_SHT,intRow,EXCEL_RED_COLOR);
							objExcelReader.Close();
							PCSMessageBox.Show(ErrorCode.PRODUCT_COST_ELEMENT_NOT_CORRECT, MessageBoxIcon.Error);
							return false;
						}
						DataRow drowInfo = dtbGroup.Select(CST_ProductGroupTable.CODE_FLD + "='" + strGroup + "'")[0];
						drowDetail[CST_ActCostAllocationDetailTable.PRODUCTGROUPID_FLD] = drowInfo[ITM_ProductTable.PRODUCTGROUPID_FLD];
					}
					drowDetail[CST_ProductGroupTable.TABLE_NAME + CST_ProductGroupTable.CODE_FLD] = strGroup;//drowProduct[CST_ProductGroupTable.TABLE_NAME + CST_ProductGroupTable.CODE_FLD];

					if (strDepartment != string.Empty)
					{
						if (dtbDepartment.Select(MST_DepartmentTable.CODE_FLD + "='" + strDepartment + "'").Length == 0)
						{
							drowDetail.Delete();
							objExcelReader.HighlightEntireRow(DATA_SHT,intRow,EXCEL_RED_COLOR);
							objExcelReader.Close();
							PCSMessageBox.Show(ErrorCode.PRODUCT_COST_ELEMENT_NOT_CORRECT, MessageBoxIcon.Error);
							return false;
						}
						DataRow drowInfo = dtbDepartment.Select(MST_DepartmentTable.CODE_FLD + "='" + strDepartment + "'")[0];
						drowDetail[CST_ActCostAllocationDetailTable.DEPARTMENTID_FLD] = drowInfo[PRO_ProductionLineTable.DEPARTMENTID_FLD];
					}
					drowDetail[MST_DepartmentTable.TABLE_NAME + MST_DepartmentTable.CODE_FLD] = strDepartment;//drowProduct[MST_DepartmentTable.TABLE_NAME + MST_DepartmentTable.CODE_FLD];

					DataRow[] arrCostElements = dstCostElements.Tables[0].Select(STD_CostElementTable.NAME_FLD + "='" + strCostElement + "'");
					if (arrCostElements.Length == 1)
					{
						DataRow drowCostElement = arrCostElements[0];
						drowDetail[CST_ActCostAllocationDetailTable.COSTELEMENTID_FLD] = drowCostElement[STD_CostElementTable.COSTELEMENTID_FLD];
						drowDetail[STD_CostElementTable.TABLE_NAME + STD_CostElementTable.NAME_FLD] = drowCostElement[STD_CostElementTable.NAME_FLD];
					}
					else
					{
						drowDetail.Delete();
						objExcelReader.HighlightEntireRow(DATA_SHT,intRow,EXCEL_RED_COLOR);
						objExcelReader.Close();
						PCSMessageBox.Show(ErrorCode.PRODUCT_COST_ELEMENT_NOT_FOUND, MessageBoxIcon.Error);
						return false;
					}
					dtbDetail.Rows.Add(drowDetail);
				}

				//close excel application
				objExcelReader.Close();
				return true;
			}
			else
			{
				//TODO : messages cannot open excel file for import
				PCSMessageBox.Show(ErrorCode.CAN_NOT_READ_EXCEL_FILE,MessageBoxIcon.Error);
				return false;
			}
		}
		
		#endregion Methods

		#region Event Processing
		private void btnPeriodSearch_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnPeriodSearch_Click()";

			try
			{
				SelectPeriod(true);
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

		private void btnAdd_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnAdd_Click()";

			try
			{				
				enuFormAction = EnumAction.Add;				
				ResetControlValue();
				LockControl(false);				
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
				// check data, if data is invalid then exit immediately
				blnDataIsValid = ValidateData() && !dgrdData.EditActive;
				if(!blnDataIsValid) return;

				DataSet dtsDC = dtbDetail.DataSet;
				if (dtsDC == null)
				{
					dtsDC = new DataSet();
					dtsDC.Tables.Add(dtbDetail);
				}

				// check form action to save data
				switch(enuFormAction)
				{
					case EnumAction.Add:				
						boMaster.Add(voMaster, dtsDC);
						
						break;

					case EnumAction.Edit:
						
						boMaster.Update(voMaster, dtsDC);
						break;
				}

				//Reload detail grid
				LoadDetailGrid(voMaster.ActCostAllocationMasterID);
				enuFormAction = EnumAction.Default;				
				LockControl(true);

				PCSMessageBox.Show(ErrorCode.MESSAGE_AFTER_SAVE_DATA, MessageBoxIcon.Information);
			}
			catch (PCSException ex)
			{
				if (ex.mCode == ErrorCode.DUPLICATE_KEY)
				{
					PCSMessageBox.Show(ErrorCode.DUPLICATE_KEY, MessageBoxIcon.Exclamation);
					txtPeriod.Focus();
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
				blnDataIsValid = false;
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
				blnDataIsValid = false;
			}
		}

		private void btnEdit_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnEdit_Click()";
			try
			{
				//return if user has not select DC Option
				if(voMaster.ActCostAllocationMasterID <= 0 )
				{ 
					return;
				}

				enuFormAction = EnumAction.Edit;
				LockControl(false);
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
				if(voMaster.ActCostAllocationMasterID <= 0)
				{
					return;
				}

				if (PCSMessageBox.Show(ErrorCode.MESSAGE_DELETE_RECORD, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
				{
					boMaster.Delete(voMaster.ActCostAllocationMasterID);
					ResetControlValue();					
				}

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
	
		private void btnHelp_Click(object sender, System.EventArgs e)
		{
		
		}

		private void btnClose_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void ActualCostDistributionSetup_Load(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".ActualCostDistributionSetup_Load()";
			try
			{
				enuFormAction = EnumAction.Default;
				
				//Set form security
				Security objSecurity = new Security();
				this.Name = THIS;
				if (objSecurity.SetRightForUserOnForm(this, SystemProperty.UserName) == 0)
				{
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

				//Clear controls value and lock for editing				
				ResetControlValue();
				
				//format input controls
				numValue.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
				numValue.CustomFormat = Constants.DECIMAL_NUMBERFORMAT;

				dtmFromDate.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
				dtmFromDate.CustomFormat = Constants.DATETIME_FORMAT;

				dtmToDate.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
				dtmToDate.CustomFormat = Constants.DATETIME_FORMAT;

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
		
		
		private void txtPeriod_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtPeriod_Validating()";

			try
			{
				//exit if not in add action or empty
				if(enuFormAction != EnumAction.Default)
				{
					return;
				}

				if(txtPeriod.Text.Trim().Length == 0)
				{
					ResetControlValue();
					return;
				}
				else if(!txtPeriod.Modified)
				{
					return;
				}

				bool blnCancel = !SelectPeriod(false);

				e.Cancel = blnCancel;
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
		
		private void txtPeriod_KeyDown(object sender, KeyEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtPeriod_KeyDown()";

			try
			{
				if ((e.KeyCode == Keys.F4) && (btnPeriodSearch.Enabled))
				{
					SelectPeriod(true);
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

		private void dgrdData_BeforeColUpdate(object sender, BeforeColUpdateEventArgs e)
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

		private void dgrdData_ButtonClick(object sender, ColEventArgs e)
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

		private void dgrdData_KeyDown(object sender, KeyEventArgs e)
		{
			const string METHOD_NAME = THIS + ".dgrdData_KeyDown()";

			try
			{
				//if column's value then exit immediately
				if(enuFormAction == EnumAction.Default) return;
				
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

		private void ActualCostDistributionSetup_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".ActualCostDistributionSetup_Closing()";
			try
			{	
				// if the form has been changed then ask to store database
				if(enuFormAction != EnumAction.Default) 
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

		private void ActualCostDistributionSetup_KeyDown(object sender, KeyEventArgs e)
		{
			const string METHOD_NAME = THIS + ".ActualCostDistributionSetup_KeyDown()";

			try
			{
				
				switch (e.KeyCode)
				{
					case Keys.F12:					
						//if column's value then exit immediately
						if(enuFormAction == EnumAction.Default) return;
				
						dgrdData.Col = dgrdData.Columns.IndexOf(dgrdData.Columns[STD_CostElementTable.TABLE_NAME + STD_CostElementTable.NAME_FLD]);
						dgrdData.Row = dgrdData.RowCount;
						dgrdData.Focus();
						break;

					case Keys.Delete:
						if (dgrdData.SelectedRows.Count == 0) return;
						FormControlComponents.DeleteMultiRowsOnTrueDBGrid(dgrdData);
						UpdateLineValue();
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
		
		private void dgrdData_OnAddNew(object sender, System.EventArgs e)
		{
            const string METHOD_NAME = THIS + ".dgrdData_OnAddNew()";

			try
			{
				if(dgrdData.RowCount <= 0)
				{
					dgrdData[dgrdData.Row, CST_ActCostAllocationDetailTable.LINE_FLD] = 1;
				}
				else if(!dgrdData[dgrdData.Row - 1, CST_ActCostAllocationDetailTable.LINE_FLD].Equals(DBNull.Value))
				{
					dgrdData[dgrdData.Row, CST_ActCostAllocationDetailTable.LINE_FLD] = int.Parse(dgrdData[dgrdData.Row - 1, CST_ActCostAllocationDetailTable.LINE_FLD].ToString()) + 1;
				}
				else
				{
					UpdateLineValue();
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

		private void dgrdData_AfterDelete(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".dgrdData_AfterDelete()";

			try
			{				
				UpdateLineValue();
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

		private void btnAllocate_Click(object sender, System.EventArgs e)
		{	
			const string METHOD_NAME = THIS + ".btnAllocate_Click()";

			try
			{				
				if(voMaster.ActCostAllocationMasterID <= 0) return;
				
				if (PCSMessageBox.Show(ErrorCode.MESSAGE_ASK_FOR_ALLOCATE_COST, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
				{
					this.Cursor = Cursors.WaitCursor;
					//array of message content
					string[] arrMessage = new string[]{lblAllocatingMessage.Text};

					//call allocate from bo object
					boMaster.Allocate(voMaster, dtbDetail);
					
					//show successfully message
					PCSMessageBox.Show(ErrorCode.MESSAGE_TASK_COMPLETED, MessageBoxIcon.Information, arrMessage);
					
					this.Cursor = Cursors.Default;
				}
			}
			catch(PCSException ex)
			{
				this.Cursor = Cursors.Default;
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
				this.Cursor = Cursors.Default;
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
		
		private void btnRollup_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnRollup_Click()";

			try
			{				
				if(voMaster.ActCostAllocationMasterID <= 0) return;
				
				ActualCostRollUp frmActRollup = new ActualCostRollUp(voMaster);

				frmActRollup.Show();
			}
			catch(PCSException ex)
			{
				this.Cursor = Cursors.Default;
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
				this.Cursor = Cursors.Default;
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
			const string METHOD_NAME = THIS + ".dgrdData_BeforeColEdit()";

			try
			{
				string[] arrMessage = new string[2];
				
				//Check for each column
				switch (e.Column.DataColumn.DataField)
				{					
					case PRO_ProductionLineTable.TABLE_NAME + PRO_ProductionLineTable.CODE_FLD:
						if(dgrdData[dgrdData.Row, MST_DepartmentTable.DEPARTMENTID_FLD].Equals(DBNull.Value)
						|| dgrdData[dgrdData.Row, MST_DepartmentTable.DEPARTMENTID_FLD].ToString().Trim() == string.Empty)
						{
							arrMessage[0] = dgrdData.Columns[MST_DepartmentTable.TABLE_NAME + MST_DepartmentTable.CODE_FLD].Caption;
							arrMessage[1] = dgrdData.Columns[PRO_ProductionLineTable.TABLE_NAME + PRO_ProductionLineTable.CODE_FLD].Caption;

							PCSMessageBox.Show(ErrorCode.MESSAGE_SELECT_ONE_BEFORE_SELECT_ONE, MessageBoxIcon.Warning, arrMessage);
							dgrdData.Col = dgrdData.Columns.IndexOf(dgrdData.Columns[MST_DepartmentTable.TABLE_NAME + MST_DepartmentTable.CODE_FLD]);
							dgrdData.Focus();
							e.Cancel = true;
						}						
						break;
				
					case CST_ProductGroupTable.TABLE_NAME + CST_ProductGroupTable.CODE_FLD:
						
						if(dgrdData[dgrdData.Row, CST_ActCostAllocationDetailTable.PRODUCTIONLINEID_FLD].Equals(DBNull.Value)
						|| dgrdData[dgrdData.Row, CST_ActCostAllocationDetailTable.PRODUCTIONLINEID_FLD].ToString().Trim() == string.Empty)
						{
							arrMessage[0] = dgrdData.Columns[PRO_ProductionLineTable.TABLE_NAME + PRO_ProductionLineTable.CODE_FLD].Caption;
							arrMessage[1] = dgrdData.Columns[CST_ProductGroupTable.TABLE_NAME + CST_ProductGroupTable.CODE_FLD].Caption;

							PCSMessageBox.Show(ErrorCode.MESSAGE_SELECT_ONE_BEFORE_SELECT_ONE, MessageBoxIcon.Warning, arrMessage);
							dgrdData.Col = dgrdData.Columns.IndexOf(dgrdData.Columns[PRO_ProductionLineTable.TABLE_NAME + PRO_ProductionLineTable.CODE_FLD]);
							dgrdData.Focus();
							e.Cancel = true;
						}						
						break;

					case ITM_ProductTable.TABLE_NAME + ITM_ProductTable.CODE_FLD:
						
						//Item must belong selected group
						if(dgrdData[dgrdData.Row, CST_ActCostAllocationDetailTable.PRODUCTGROUPID_FLD].Equals(DBNull.Value)
						|| dgrdData[dgrdData.Row, CST_ActCostAllocationDetailTable.PRODUCTGROUPID_FLD].ToString().Trim() == string.Empty)
						{
							arrMessage[0] = dgrdData.Columns[CST_ProductGroupTable.TABLE_NAME + CST_ProductGroupTable.CODE_FLD].Caption;
							arrMessage[1] = dgrdData.Columns[ITM_ProductTable.TABLE_NAME + ITM_ProductTable.CODE_FLD].Caption;

							PCSMessageBox.Show(ErrorCode.MESSAGE_SELECT_ONE_BEFORE_SELECT_ONE, MessageBoxIcon.Warning, arrMessage);
							dgrdData.Col = dgrdData.Columns.IndexOf(dgrdData.Columns[CST_ProductGroupTable.TABLE_NAME + CST_ProductGroupTable.CODE_FLD]);
							dgrdData.Focus();
							e.Cancel = true;
						}						
						break;

					case ITM_ProductTable.TABLE_NAME + ITM_ProductTable.DESCRIPTION_FLD:
						
						if(dgrdData[dgrdData.Row, CST_ActCostAllocationDetailTable.PRODUCTGROUPID_FLD].Equals(DBNull.Value)
						|| dgrdData[dgrdData.Row, CST_ActCostAllocationDetailTable.PRODUCTGROUPID_FLD].ToString().Trim() == string.Empty)
						{
							arrMessage[0] = dgrdData.Columns[CST_ProductGroupTable.TABLE_NAME + CST_ProductGroupTable.CODE_FLD].Caption;
							arrMessage[1] = dgrdData.Columns[ITM_ProductTable.TABLE_NAME + ITM_ProductTable.DESCRIPTION_FLD].Caption;

							PCSMessageBox.Show(ErrorCode.MESSAGE_SELECT_ONE_BEFORE_SELECT_ONE, MessageBoxIcon.Warning, arrMessage);
							dgrdData.Col = dgrdData.Columns.IndexOf(dgrdData.Columns[CST_ProductGroupTable.TABLE_NAME + CST_ProductGroupTable.CODE_FLD]);
							dgrdData.Focus();
							e.Cancel = true;
						}					
						break;
				}
			}
			catch(PCSException ex)
			{
				this.Cursor = Cursors.Default;
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
				this.Cursor = Cursors.Default;
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
		/// Displays allocation by article report
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>DungLA</author>
		private void btnPrint_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnPrint_Click()";
			string REPORT_FILE = Application.StartupPath + @"\ReportDefinition\AllocationByArticle.xml";
			try
			{
				// verify report definition file
				if (!File.Exists(REPORT_FILE))
				{
					PCSMessageBox.Show(ErrorCode.MESSAGE_REPORT_FILE_NOT_FOUND);
					return;
				}
				if (voMaster == null)
					return;
				if (voMaster.ActCostAllocationMasterID <= 0)
					return;
				C1Report rptAllocation = new C1Report();
				string[] strInfo = rptAllocation.GetReportInfo(REPORT_FILE);
				rptAllocation.Load(REPORT_FILE, strInfo[0]);
				strInfo = null;
				rptAllocation.Layout.PaperSize = PaperKind.A4;
				Utils utils = new Utils();
				rptAllocation.DataSource.ConnectionString = Utils.Instance.OleDbConnectionString;
				StringBuilder sbRecordSource = new StringBuilder();
				sbRecordSource.Append("Select AllocationAmount, CE.Code CostElement, D.Code Department,");
				sbRecordSource.Append("PL.Code ProductionLine, PG.Code ProductGroup, P.Code PartNo, P.Description PartName,");
				sbRecordSource.Append("P.Revision Model, UM.Code AS StockUM");
				sbRecordSource.Append(" From  cst_ActCostAllocationDetail AC");
				sbRecordSource.Append(" Left Join STD_CostElement CE ON AC.CostElementID=CE.CostElementID");
				sbRecordSource.Append(" Left Join MST_Department D ON AC.DepartmentID=D.DepartmentID");
				sbRecordSource.Append(" Left Join CST_ProductGroup PG ON AC.ProductGroupID=PG.ProductGroupID");
				sbRecordSource.Append(" Left Join PRO_ProductionLine PL ON AC.ProductionLineID=PL.ProductionLineID");
				sbRecordSource.Append(" Left Join ITM_Product P ON AC.ProductID=P.ProductID");
				sbRecordSource.Append(" Left Join MST_UnitOfMeasure UM ON P.StockUMID = UM.UnitOfMeasureID");
				sbRecordSource.Append(" Where AC.ActCostAllocationMasterID = " + voMaster.ActCostAllocationMasterID);
				sbRecordSource.Append(" Order By D.Code , PL.Code , PG.Code , P.Code , P.Description , P.Revision ,  CE.OrderNo");
				rptAllocation.DataSource.RecordSource = sbRecordSource.ToString();
				rptAllocation.Fields["fldPeriod"].Text = voMaster.PeriodName;
				rptAllocation.Fields["fldFromDate"].Text = voMaster.FromDate.ToString(Constants.DATETIME_FORMAT);
				rptAllocation.Fields["fldToDate"].Text = voMaster.ToDate.ToString(Constants.DATETIME_FORMAT);
				rptAllocation.Render();
				C1PrintPreviewDialog ddlPreview = new C1PrintPreviewDialog();
				ddlPreview.FormTitle = rptAllocation.Fields["fldTitle"].Text;
				ddlPreview.ReportViewer.PreviewNavigationPanel.Visible = false;
				ddlPreview.ReportViewer.Document = rptAllocation.Document;
				ddlPreview.Show();
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
				PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxIcon.Error);
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

		private void btnImport_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnImport_Click()";
			try
			{			
				this.Cursor = Cursors.WaitCursor;
				//open dialog for select file
				if (dlgOpenImpFile.ShowDialog(this) == DialogResult.OK)
				{
					string strPath = dlgOpenImpFile.FileName;
					//load excel file
					ResetControlValue();

					if (LoadExcelFile(strPath))
					{
						//bind data
						dgrdData.DataSource = dtbDetail;
						// calculate total amount
						try
						{
							txtTotalAmount.Text = Convert.ToDecimal(dtbDetail.Compute("SUM(AllocationAmount)", string.Empty)).ToString(Constants.DECIMAL_NUMBERFORMAT);
						}
						catch{}
						FormatDataGrid();
						enuFormAction = EnumAction.Add;				
						LockControl(false);
					}
					else
					{
						ResetControlValue();
					}
				}
				this.Cursor = Cursors.Default;
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

		
		#endregion		

		private void btnDelAllocation_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnDelAllocation_Click()";

			try
			{				
				if(voMaster.ActCostAllocationMasterID <= 0) return;
				
				if (PCSMessageBox.Show(ErrorCode.MESSAGE_DELETE_RECORD, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
				{
					this.Cursor = Cursors.WaitCursor;
					//array of message content
					string[] arrMessage = new string[]{lblDelAllocatingMessage.Text};

					//call allocate from bo object
					boMaster.DeleteAllocation(voMaster.ActCostAllocationMasterID);
					
					//show successfully message
					PCSMessageBox.Show(ErrorCode.MESSAGE_TASK_COMPLETED, MessageBoxIcon.Information, arrMessage);
					
					this.Cursor = Cursors.Default;
				}
			}
			catch(PCSException ex)
			{
				this.Cursor = Cursors.Default;
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
				this.Cursor = Cursors.Default;
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

		private void dgrdData_Click(object sender, System.EventArgs e)
		{
		
		}

		private void btnDelChargeAllocation_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnDelChargeAllocation_Click()";
			try
			{
				if (voMaster.ActCostAllocationMasterID > 0)
				{
					if (PCSMessageBox.Show(ErrorCode.MESSAGE_DELETE_RECORD, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
					{
						boMaster.DelChargeAllocation(voMaster.ActCostAllocationMasterID);
						PCSMessageBox.Show(ErrorCode.MESSAGE_AFTER_SAVE_DATA, MessageBoxIcon.Information);
					}
				}
			}
			catch(PCSException ex)
			{
				this.Cursor = Cursors.Default;
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
				this.Cursor = Cursors.Default;
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

		#region ExcelReader nested class, for import funtion (by DuongNA)
		private class ExcelReader
		{
			string m_strFileName;
			Microsoft.Office.Interop.Excel.Application m_objExcelApp;
			Microsoft.Office.Interop.Excel.Workbook m_objWorkbook;
			int m_intCachingSheet = -1;			//Chi so cua sheet dang duoc cache du lieu
			object[,] m_arrDataCache;			//Du lieu cache
			int m_intRowCount = -1;
			int m_intColCount = -1;

			public ExcelReader(string pstrFileName)
			{
				m_strFileName = pstrFileName;
				m_objExcelApp = null;
				m_objWorkbook = null;
			}

			public bool Open()
			{
				try
				{
					m_objExcelApp = new Microsoft.Office.Interop.Excel.Application();
					m_objWorkbook = m_objExcelApp.Workbooks.Open(m_strFileName,Type.Missing,Type.Missing,Type.Missing,Type.Missing,Type.Missing,Type.Missing,Type.Missing,Type.Missing,Type.Missing,Type.Missing,Type.Missing,Type.Missing,Type.Missing,Type.Missing);
					if (m_objWorkbook.Worksheets == null)
					{
						return false;
					}
					else
					{
						return true;
					}
				}
				catch
				{
					return false;
				}
			}

			public void Close()
			{
				try
				{
					Boolean blnSaveChanges = false;
					m_objWorkbook.Save();
					m_objWorkbook.Close(blnSaveChanges,m_strFileName,Type.Missing);
					m_objExcelApp.Quit();
				}
				catch
				{}
			}

			public object ReadCell(int pintSheet, int pintRow, int pintCol)
			{
				try
				{
					//Cached read
					if (m_intCachingSheet != pintSheet)
					{
						m_arrDataCache = CacheBlock(pintSheet);
						m_intCachingSheet = pintSheet;
					}
					object objData = m_arrDataCache[pintRow + 1,pintCol + 1];
					if (objData != null)
					{
						return objData;
					}
					else
					{
						return string.Empty;
					}
				}
				catch 
				{
					return string.Empty;
				}
			}

			public void HighlightEntireRow(int pintSheet, int pintRow, int pintColor)
			{
				Microsoft.Office.Interop.Excel.Worksheet objWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)m_objWorkbook.Sheets[GetSheetName(pintSheet)];
				Microsoft.Office.Interop.Excel.Range objRange = objWorkSheet.get_Range(objWorkSheet.Cells[pintRow+1,1],objWorkSheet.Cells[pintRow+1,GetColCount(pintSheet)]);
				objRange.Interior.ColorIndex = pintColor;
			}

			public int GetRowCount(int pintSheet)
			{
				if (m_intRowCount != -1)
				{
					return m_intRowCount;
				}
				else
				{
					Microsoft.Office.Interop.Excel.Worksheet objWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)m_objWorkbook.Sheets[GetSheetName(pintSheet)];
					Microsoft.Office.Interop.Excel.Range objRange = objWorkSheet.UsedRange;
					return m_intRowCount = objRange.Rows.Count;
				}
			}

			public int GetColCount(int pintSheet)
			{
				if (m_intColCount != -1)
				{
					return m_intColCount;
				}
				else
				{
					Microsoft.Office.Interop.Excel.Worksheet objWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)m_objWorkbook.Sheets[GetSheetName(pintSheet)];
					Microsoft.Office.Interop.Excel.Range objRange = objWorkSheet.UsedRange;
					return m_intColCount = objRange.Columns.Count;
				}
			}

			private string GetSheetName(int pintSheet)
			{
				int i = 0;
				foreach (Microsoft.Office.Interop.Excel.Worksheet objWorksheet in m_objWorkbook.Sheets)
				{
					if (i == pintSheet)
					{
						return objWorksheet.Name;
					}
				}
				return string.Empty;
			}

			private string GetRowName(int pintRow)
			{
				return (pintRow + 1).ToString();
			}

			private string GetColName(int pintCol)
			{
				const int ALPHABETE_NUM = 25;
				//note : use for column A to ZZ only
				char chrCol1, chrCol2;
				if (pintCol < ALPHABETE_NUM)
				{
					chrCol1 = Convert.ToChar(pintCol + Convert.ToInt32('A'));
					return chrCol1.ToString();
				}
				else
				{
					chrCol1 = Convert.ToChar(pintCol / ALPHABETE_NUM + Convert.ToInt32('A'));
					chrCol2 = Convert.ToChar(pintCol % ALPHABETE_NUM + Convert.ToInt32('A'));
					return chrCol1.ToString() + chrCol2.ToString();
				}
			}
		
			private object[,] CacheBlock(int pintSheet)
			{
				object[,] arrResult;
				Microsoft.Office.Interop.Excel.Worksheet objWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)m_objWorkbook.Sheets[GetSheetName(pintSheet)];
				Microsoft.Office.Interop.Excel.Range objRange = objWorkSheet.UsedRange;
				arrResult = (object[,])objRange.Value2;
				return arrResult;
			}
		}
		#endregion ExcelReader nested class, for import funtion (by DuongNA)
	}
}