using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Threading;
using System.Windows.Forms;
using C1.Win.C1Input;
using C1.Win.C1List;
using PCSComMaterials.ActualCost.BO;
using PCSComMaterials.ActualCost.DS;
using PCSComUtils.Common;
using PCSComUtils.Common.BO;
using PCSComUtils.PCSExc;
using PCSUtils.Log;
using PCSUtils.Utils;
using ThreadState = System.Threading.ThreadState;

namespace PCSMaterials.ActualCost
{
	/// <summary>
	/// Summary description for ActualCostRollUp.
	/// </summary>
	public class ActualCostRollUp : Form
	{
		private Button btnHelp;
		private Button btnClose;
		private C1Combo cboCCN;
		private Label lblCCN;
		private Button btnRollUp;
		private Button btnPeriodSearch;
		private C1DateEdit dtmToDate;
		private C1DateEdit dtmFromDate;
		private Label lblToDate;
		private Label lblFromDate;
		private PictureBox picProcessing;
		private TextBox txtPeriodName;
		private Label lblPeriodName;
		private C1DateEdit dtmRollupDate;
		private Label lblRollupDate;
		private Button btnDeleteCost;
		private System.Windows.Forms.Label lblErrorReason;
		private System.Windows.Forms.Label lblActualCost;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private Container components = null;

		const string THIS = "PCSMaterials.ActualCost.ActualCostRollUp";
		Thread thrProcess = null;
		private Label lblRollup;
		private CheckBox chkRollup;
		CST_ActCostAllocationMasterVO voPeriod = null;
		CST_ActCostAllocationMasterVO voPeriodFromSetupForm = null;
		DataTable dtbErrorItem = null;
		ActualCostRollUpBO boActualCost = new ActualCostRollUpBO();
		
		#region constants

		const string PARENT_ID_FLD = "ParentID";
		const string TYPE_CODE_FLD = "TypeCode";
		const string REASON_FLD = "Reason";
		const string SOURCE_TYPE_FLD = "SourceBinType";
		const string DEST_TYPE_FLD = "DesBinType";
		const string TOP_ITEM = "TopItems";
		const string OHCACHE_TABLE = "dtbOnhandCache";
		const string BEGINCOST_TABLE = "dtbBeginCost";
		const string MISC_INPERIOD_TABLE = "dtbMiscIssueInPeriod";
		const string COMPLETE_COMP = "dtbCompletionComponent";
		const string COST_ADJUST = "dtbCostingAdjustment";
		const string WO_COMLETED_PERIOD = "dtbWOCompletedQuantityInPeriod";
		const string ADJUSTMENT_PERIOD = "dtbAdjustmentInPeriod";
		const string PO_RECEIPT_INPERIOD = "dtbPOReceiptInPeriod";
		const string RECEIPT_INVOICE_PERIOD = "dtbPOReceiptByInvoiceInPeriod";
		const string RETURN_GOODS_PERIOD = "dtbReturnGoodsReceiveInPeriod";
		const string RECOVER_PERIOD = "dtbRecoverableQuantityInPeriod";
		const string ADD_CHARGE = "dtbAdditionalCharge";
		const string COST_ELEMENT = "dtbCostElements";
		const string ALLOCATION = "dtbAllocationAmount";
		const string COMPONENT_SCRAP = "dtbComponentScrap";
		private System.Windows.Forms.Button btnChargeAllocation;
		private System.Windows.Forms.Label lblChargeAllocation;
		
		#endregion

		public ActualCostRollUp()
		{
			//// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		public ActualCostRollUp(object pobjActMasterVO)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			voPeriod = (CST_ActCostAllocationMasterVO)pobjActMasterVO;
			voPeriodFromSetupForm = voPeriod;
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(ActualCostRollUp));
			this.btnRollUp = new System.Windows.Forms.Button();
			this.txtPeriodName = new System.Windows.Forms.TextBox();
			this.lblPeriodName = new System.Windows.Forms.Label();
			this.btnHelp = new System.Windows.Forms.Button();
			this.btnClose = new System.Windows.Forms.Button();
			this.cboCCN = new C1.Win.C1List.C1Combo();
			this.lblCCN = new System.Windows.Forms.Label();
			this.dtmRollupDate = new C1.Win.C1Input.C1DateEdit();
			this.lblRollupDate = new System.Windows.Forms.Label();
			this.btnPeriodSearch = new System.Windows.Forms.Button();
			this.dtmToDate = new C1.Win.C1Input.C1DateEdit();
			this.dtmFromDate = new C1.Win.C1Input.C1DateEdit();
			this.lblToDate = new System.Windows.Forms.Label();
			this.lblFromDate = new System.Windows.Forms.Label();
			this.btnDeleteCost = new System.Windows.Forms.Button();
			this.picProcessing = new System.Windows.Forms.PictureBox();
			this.lblRollup = new System.Windows.Forms.Label();
			this.chkRollup = new System.Windows.Forms.CheckBox();
			this.lblErrorReason = new System.Windows.Forms.Label();
			this.lblActualCost = new System.Windows.Forms.Label();
			this.btnChargeAllocation = new System.Windows.Forms.Button();
			this.lblChargeAllocation = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.cboCCN)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dtmRollupDate)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dtmToDate)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dtmFromDate)).BeginInit();
			this.SuspendLayout();
			// 
			// btnRollUp
			// 
			this.btnRollUp.AccessibleDescription = resources.GetString("btnRollUp.AccessibleDescription");
			this.btnRollUp.AccessibleName = resources.GetString("btnRollUp.AccessibleName");
			this.btnRollUp.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnRollUp.Anchor")));
			this.btnRollUp.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnRollUp.BackgroundImage")));
			this.btnRollUp.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnRollUp.Dock")));
			this.btnRollUp.Enabled = ((bool)(resources.GetObject("btnRollUp.Enabled")));
			this.btnRollUp.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnRollUp.FlatStyle")));
			this.btnRollUp.Font = ((System.Drawing.Font)(resources.GetObject("btnRollUp.Font")));
			this.btnRollUp.Image = ((System.Drawing.Image)(resources.GetObject("btnRollUp.Image")));
			this.btnRollUp.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnRollUp.ImageAlign")));
			this.btnRollUp.ImageIndex = ((int)(resources.GetObject("btnRollUp.ImageIndex")));
			this.btnRollUp.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnRollUp.ImeMode")));
			this.btnRollUp.Location = ((System.Drawing.Point)(resources.GetObject("btnRollUp.Location")));
			this.btnRollUp.Name = "btnRollUp";
			this.btnRollUp.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnRollUp.RightToLeft")));
			this.btnRollUp.Size = ((System.Drawing.Size)(resources.GetObject("btnRollUp.Size")));
			this.btnRollUp.TabIndex = ((int)(resources.GetObject("btnRollUp.TabIndex")));
			this.btnRollUp.Text = resources.GetString("btnRollUp.Text");
			this.btnRollUp.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnRollUp.TextAlign")));
			this.btnRollUp.Visible = ((bool)(resources.GetObject("btnRollUp.Visible")));
			this.btnRollUp.Click += new System.EventHandler(this.btnRollUp_Click);
			// 
			// txtPeriodName
			// 
			this.txtPeriodName.AccessibleDescription = resources.GetString("txtPeriodName.AccessibleDescription");
			this.txtPeriodName.AccessibleName = resources.GetString("txtPeriodName.AccessibleName");
			this.txtPeriodName.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("txtPeriodName.Anchor")));
			this.txtPeriodName.AutoSize = ((bool)(resources.GetObject("txtPeriodName.AutoSize")));
			this.txtPeriodName.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("txtPeriodName.BackgroundImage")));
			this.txtPeriodName.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("txtPeriodName.Dock")));
			this.txtPeriodName.Enabled = ((bool)(resources.GetObject("txtPeriodName.Enabled")));
			this.txtPeriodName.Font = ((System.Drawing.Font)(resources.GetObject("txtPeriodName.Font")));
			this.txtPeriodName.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("txtPeriodName.ImeMode")));
			this.txtPeriodName.Location = ((System.Drawing.Point)(resources.GetObject("txtPeriodName.Location")));
			this.txtPeriodName.MaxLength = ((int)(resources.GetObject("txtPeriodName.MaxLength")));
			this.txtPeriodName.Multiline = ((bool)(resources.GetObject("txtPeriodName.Multiline")));
			this.txtPeriodName.Name = "txtPeriodName";
			this.txtPeriodName.PasswordChar = ((char)(resources.GetObject("txtPeriodName.PasswordChar")));
			this.txtPeriodName.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("txtPeriodName.RightToLeft")));
			this.txtPeriodName.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("txtPeriodName.ScrollBars")));
			this.txtPeriodName.Size = ((System.Drawing.Size)(resources.GetObject("txtPeriodName.Size")));
			this.txtPeriodName.TabIndex = ((int)(resources.GetObject("txtPeriodName.TabIndex")));
			this.txtPeriodName.Text = resources.GetString("txtPeriodName.Text");
			this.txtPeriodName.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("txtPeriodName.TextAlign")));
			this.txtPeriodName.Visible = ((bool)(resources.GetObject("txtPeriodName.Visible")));
			this.txtPeriodName.WordWrap = ((bool)(resources.GetObject("txtPeriodName.WordWrap")));
			this.txtPeriodName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPeriodName_KeyDown);
			this.txtPeriodName.Validating += new System.ComponentModel.CancelEventHandler(this.txtPeriodName_Validating);
			// 
			// lblPeriodName
			// 
			this.lblPeriodName.AccessibleDescription = resources.GetString("lblPeriodName.AccessibleDescription");
			this.lblPeriodName.AccessibleName = resources.GetString("lblPeriodName.AccessibleName");
			this.lblPeriodName.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblPeriodName.Anchor")));
			this.lblPeriodName.AutoSize = ((bool)(resources.GetObject("lblPeriodName.AutoSize")));
			this.lblPeriodName.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblPeriodName.Dock")));
			this.lblPeriodName.Enabled = ((bool)(resources.GetObject("lblPeriodName.Enabled")));
			this.lblPeriodName.Font = ((System.Drawing.Font)(resources.GetObject("lblPeriodName.Font")));
			this.lblPeriodName.ForeColor = System.Drawing.Color.Maroon;
			this.lblPeriodName.Image = ((System.Drawing.Image)(resources.GetObject("lblPeriodName.Image")));
			this.lblPeriodName.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblPeriodName.ImageAlign")));
			this.lblPeriodName.ImageIndex = ((int)(resources.GetObject("lblPeriodName.ImageIndex")));
			this.lblPeriodName.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblPeriodName.ImeMode")));
			this.lblPeriodName.Location = ((System.Drawing.Point)(resources.GetObject("lblPeriodName.Location")));
			this.lblPeriodName.Name = "lblPeriodName";
			this.lblPeriodName.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblPeriodName.RightToLeft")));
			this.lblPeriodName.Size = ((System.Drawing.Size)(resources.GetObject("lblPeriodName.Size")));
			this.lblPeriodName.TabIndex = ((int)(resources.GetObject("lblPeriodName.TabIndex")));
			this.lblPeriodName.Text = resources.GetString("lblPeriodName.Text");
			this.lblPeriodName.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblPeriodName.TextAlign")));
			this.lblPeriodName.Visible = ((bool)(resources.GetObject("lblPeriodName.Visible")));
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
			this.cboCCN.SelectedValueChanged += new System.EventHandler(this.cboCCN_SelectedValueChanged);
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
			this.dtmRollupDate.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("dtmRollupDate.RightToLeft")));
			this.dtmRollupDate.ShowFocusRectangle = ((bool)(resources.GetObject("dtmRollupDate.ShowFocusRectangle")));
			this.dtmRollupDate.Size = ((System.Drawing.Size)(resources.GetObject("dtmRollupDate.Size")));
			this.dtmRollupDate.TabIndex = ((int)(resources.GetObject("dtmRollupDate.TabIndex")));
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
			this.lblRollupDate.ForeColor = System.Drawing.Color.Maroon;
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
			this.dtmToDate.ReadOnly = true;
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
			this.dtmFromDate.ReadOnly = true;
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
			// lblToDate
			// 
			this.lblToDate.AccessibleDescription = resources.GetString("lblToDate.AccessibleDescription");
			this.lblToDate.AccessibleName = resources.GetString("lblToDate.AccessibleName");
			this.lblToDate.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblToDate.Anchor")));
			this.lblToDate.AutoSize = ((bool)(resources.GetObject("lblToDate.AutoSize")));
			this.lblToDate.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblToDate.Dock")));
			this.lblToDate.Enabled = ((bool)(resources.GetObject("lblToDate.Enabled")));
			this.lblToDate.Font = ((System.Drawing.Font)(resources.GetObject("lblToDate.Font")));
			this.lblToDate.ForeColor = System.Drawing.SystemColors.ControlText;
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
			this.lblFromDate.ForeColor = System.Drawing.SystemColors.ControlText;
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
			// btnDeleteCost
			// 
			this.btnDeleteCost.AccessibleDescription = resources.GetString("btnDeleteCost.AccessibleDescription");
			this.btnDeleteCost.AccessibleName = resources.GetString("btnDeleteCost.AccessibleName");
			this.btnDeleteCost.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnDeleteCost.Anchor")));
			this.btnDeleteCost.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnDeleteCost.BackgroundImage")));
			this.btnDeleteCost.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnDeleteCost.Dock")));
			this.btnDeleteCost.Enabled = ((bool)(resources.GetObject("btnDeleteCost.Enabled")));
			this.btnDeleteCost.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnDeleteCost.FlatStyle")));
			this.btnDeleteCost.Font = ((System.Drawing.Font)(resources.GetObject("btnDeleteCost.Font")));
			this.btnDeleteCost.Image = ((System.Drawing.Image)(resources.GetObject("btnDeleteCost.Image")));
			this.btnDeleteCost.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnDeleteCost.ImageAlign")));
			this.btnDeleteCost.ImageIndex = ((int)(resources.GetObject("btnDeleteCost.ImageIndex")));
			this.btnDeleteCost.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnDeleteCost.ImeMode")));
			this.btnDeleteCost.Location = ((System.Drawing.Point)(resources.GetObject("btnDeleteCost.Location")));
			this.btnDeleteCost.Name = "btnDeleteCost";
			this.btnDeleteCost.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnDeleteCost.RightToLeft")));
			this.btnDeleteCost.Size = ((System.Drawing.Size)(resources.GetObject("btnDeleteCost.Size")));
			this.btnDeleteCost.TabIndex = ((int)(resources.GetObject("btnDeleteCost.TabIndex")));
			this.btnDeleteCost.Text = resources.GetString("btnDeleteCost.Text");
			this.btnDeleteCost.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnDeleteCost.TextAlign")));
			this.btnDeleteCost.Visible = ((bool)(resources.GetObject("btnDeleteCost.Visible")));
			this.btnDeleteCost.Click += new System.EventHandler(this.btnDeleteCost_Click);
			// 
			// picProcessing
			// 
			this.picProcessing.AccessibleDescription = resources.GetString("picProcessing.AccessibleDescription");
			this.picProcessing.AccessibleName = resources.GetString("picProcessing.AccessibleName");
			this.picProcessing.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("picProcessing.Anchor")));
			this.picProcessing.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("picProcessing.BackgroundImage")));
			this.picProcessing.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("picProcessing.Dock")));
			this.picProcessing.Enabled = ((bool)(resources.GetObject("picProcessing.Enabled")));
			this.picProcessing.Font = ((System.Drawing.Font)(resources.GetObject("picProcessing.Font")));
			this.picProcessing.Image = ((System.Drawing.Image)(resources.GetObject("picProcessing.Image")));
			this.picProcessing.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("picProcessing.ImeMode")));
			this.picProcessing.Location = ((System.Drawing.Point)(resources.GetObject("picProcessing.Location")));
			this.picProcessing.Name = "picProcessing";
			this.picProcessing.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("picProcessing.RightToLeft")));
			this.picProcessing.Size = ((System.Drawing.Size)(resources.GetObject("picProcessing.Size")));
			this.picProcessing.SizeMode = ((System.Windows.Forms.PictureBoxSizeMode)(resources.GetObject("picProcessing.SizeMode")));
			this.picProcessing.TabIndex = ((int)(resources.GetObject("picProcessing.TabIndex")));
			this.picProcessing.TabStop = false;
			this.picProcessing.Text = resources.GetString("picProcessing.Text");
			this.picProcessing.Visible = ((bool)(resources.GetObject("picProcessing.Visible")));
			// 
			// lblRollup
			// 
			this.lblRollup.AccessibleDescription = resources.GetString("lblRollup.AccessibleDescription");
			this.lblRollup.AccessibleName = resources.GetString("lblRollup.AccessibleName");
			this.lblRollup.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblRollup.Anchor")));
			this.lblRollup.AutoSize = ((bool)(resources.GetObject("lblRollup.AutoSize")));
			this.lblRollup.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblRollup.Dock")));
			this.lblRollup.Enabled = ((bool)(resources.GetObject("lblRollup.Enabled")));
			this.lblRollup.Font = ((System.Drawing.Font)(resources.GetObject("lblRollup.Font")));
			this.lblRollup.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblRollup.Image = ((System.Drawing.Image)(resources.GetObject("lblRollup.Image")));
			this.lblRollup.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblRollup.ImageAlign")));
			this.lblRollup.ImageIndex = ((int)(resources.GetObject("lblRollup.ImageIndex")));
			this.lblRollup.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblRollup.ImeMode")));
			this.lblRollup.Location = ((System.Drawing.Point)(resources.GetObject("lblRollup.Location")));
			this.lblRollup.Name = "lblRollup";
			this.lblRollup.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblRollup.RightToLeft")));
			this.lblRollup.Size = ((System.Drawing.Size)(resources.GetObject("lblRollup.Size")));
			this.lblRollup.TabIndex = ((int)(resources.GetObject("lblRollup.TabIndex")));
			this.lblRollup.Text = resources.GetString("lblRollup.Text");
			this.lblRollup.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblRollup.TextAlign")));
			this.lblRollup.Visible = ((bool)(resources.GetObject("lblRollup.Visible")));
			// 
			// chkRollup
			// 
			this.chkRollup.AccessibleDescription = resources.GetString("chkRollup.AccessibleDescription");
			this.chkRollup.AccessibleName = resources.GetString("chkRollup.AccessibleName");
			this.chkRollup.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("chkRollup.Anchor")));
			this.chkRollup.Appearance = ((System.Windows.Forms.Appearance)(resources.GetObject("chkRollup.Appearance")));
			this.chkRollup.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("chkRollup.BackgroundImage")));
			this.chkRollup.CheckAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("chkRollup.CheckAlign")));
			this.chkRollup.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("chkRollup.Dock")));
			this.chkRollup.Enabled = ((bool)(resources.GetObject("chkRollup.Enabled")));
			this.chkRollup.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("chkRollup.FlatStyle")));
			this.chkRollup.Font = ((System.Drawing.Font)(resources.GetObject("chkRollup.Font")));
			this.chkRollup.Image = ((System.Drawing.Image)(resources.GetObject("chkRollup.Image")));
			this.chkRollup.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("chkRollup.ImageAlign")));
			this.chkRollup.ImageIndex = ((int)(resources.GetObject("chkRollup.ImageIndex")));
			this.chkRollup.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("chkRollup.ImeMode")));
			this.chkRollup.Location = ((System.Drawing.Point)(resources.GetObject("chkRollup.Location")));
			this.chkRollup.Name = "chkRollup";
			this.chkRollup.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("chkRollup.RightToLeft")));
			this.chkRollup.Size = ((System.Drawing.Size)(resources.GetObject("chkRollup.Size")));
			this.chkRollup.TabIndex = ((int)(resources.GetObject("chkRollup.TabIndex")));
			this.chkRollup.Text = resources.GetString("chkRollup.Text");
			this.chkRollup.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("chkRollup.TextAlign")));
			this.chkRollup.Visible = ((bool)(resources.GetObject("chkRollup.Visible")));
			// 
			// lblErrorReason
			// 
			this.lblErrorReason.AccessibleDescription = resources.GetString("lblErrorReason.AccessibleDescription");
			this.lblErrorReason.AccessibleName = resources.GetString("lblErrorReason.AccessibleName");
			this.lblErrorReason.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblErrorReason.Anchor")));
			this.lblErrorReason.AutoSize = ((bool)(resources.GetObject("lblErrorReason.AutoSize")));
			this.lblErrorReason.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblErrorReason.Dock")));
			this.lblErrorReason.Enabled = ((bool)(resources.GetObject("lblErrorReason.Enabled")));
			this.lblErrorReason.Font = ((System.Drawing.Font)(resources.GetObject("lblErrorReason.Font")));
			this.lblErrorReason.Image = ((System.Drawing.Image)(resources.GetObject("lblErrorReason.Image")));
			this.lblErrorReason.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblErrorReason.ImageAlign")));
			this.lblErrorReason.ImageIndex = ((int)(resources.GetObject("lblErrorReason.ImageIndex")));
			this.lblErrorReason.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblErrorReason.ImeMode")));
			this.lblErrorReason.Location = ((System.Drawing.Point)(resources.GetObject("lblErrorReason.Location")));
			this.lblErrorReason.Name = "lblErrorReason";
			this.lblErrorReason.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblErrorReason.RightToLeft")));
			this.lblErrorReason.Size = ((System.Drawing.Size)(resources.GetObject("lblErrorReason.Size")));
			this.lblErrorReason.TabIndex = ((int)(resources.GetObject("lblErrorReason.TabIndex")));
			this.lblErrorReason.Text = resources.GetString("lblErrorReason.Text");
			this.lblErrorReason.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblErrorReason.TextAlign")));
			this.lblErrorReason.Visible = ((bool)(resources.GetObject("lblErrorReason.Visible")));
			// 
			// lblActualCost
			// 
			this.lblActualCost.AccessibleDescription = resources.GetString("lblActualCost.AccessibleDescription");
			this.lblActualCost.AccessibleName = resources.GetString("lblActualCost.AccessibleName");
			this.lblActualCost.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblActualCost.Anchor")));
			this.lblActualCost.AutoSize = ((bool)(resources.GetObject("lblActualCost.AutoSize")));
			this.lblActualCost.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblActualCost.Dock")));
			this.lblActualCost.Enabled = ((bool)(resources.GetObject("lblActualCost.Enabled")));
			this.lblActualCost.Font = ((System.Drawing.Font)(resources.GetObject("lblActualCost.Font")));
			this.lblActualCost.Image = ((System.Drawing.Image)(resources.GetObject("lblActualCost.Image")));
			this.lblActualCost.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblActualCost.ImageAlign")));
			this.lblActualCost.ImageIndex = ((int)(resources.GetObject("lblActualCost.ImageIndex")));
			this.lblActualCost.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblActualCost.ImeMode")));
			this.lblActualCost.Location = ((System.Drawing.Point)(resources.GetObject("lblActualCost.Location")));
			this.lblActualCost.Name = "lblActualCost";
			this.lblActualCost.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblActualCost.RightToLeft")));
			this.lblActualCost.Size = ((System.Drawing.Size)(resources.GetObject("lblActualCost.Size")));
			this.lblActualCost.TabIndex = ((int)(resources.GetObject("lblActualCost.TabIndex")));
			this.lblActualCost.Text = resources.GetString("lblActualCost.Text");
			this.lblActualCost.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblActualCost.TextAlign")));
			this.lblActualCost.Visible = ((bool)(resources.GetObject("lblActualCost.Visible")));
			// 
			// btnChargeAllocation
			// 
			this.btnChargeAllocation.AccessibleDescription = resources.GetString("btnChargeAllocation.AccessibleDescription");
			this.btnChargeAllocation.AccessibleName = resources.GetString("btnChargeAllocation.AccessibleName");
			this.btnChargeAllocation.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnChargeAllocation.Anchor")));
			this.btnChargeAllocation.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnChargeAllocation.BackgroundImage")));
			this.btnChargeAllocation.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnChargeAllocation.Dock")));
			this.btnChargeAllocation.Enabled = ((bool)(resources.GetObject("btnChargeAllocation.Enabled")));
			this.btnChargeAllocation.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnChargeAllocation.FlatStyle")));
			this.btnChargeAllocation.Font = ((System.Drawing.Font)(resources.GetObject("btnChargeAllocation.Font")));
			this.btnChargeAllocation.Image = ((System.Drawing.Image)(resources.GetObject("btnChargeAllocation.Image")));
			this.btnChargeAllocation.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnChargeAllocation.ImageAlign")));
			this.btnChargeAllocation.ImageIndex = ((int)(resources.GetObject("btnChargeAllocation.ImageIndex")));
			this.btnChargeAllocation.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnChargeAllocation.ImeMode")));
			this.btnChargeAllocation.Location = ((System.Drawing.Point)(resources.GetObject("btnChargeAllocation.Location")));
			this.btnChargeAllocation.Name = "btnChargeAllocation";
			this.btnChargeAllocation.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnChargeAllocation.RightToLeft")));
			this.btnChargeAllocation.Size = ((System.Drawing.Size)(resources.GetObject("btnChargeAllocation.Size")));
			this.btnChargeAllocation.TabIndex = ((int)(resources.GetObject("btnChargeAllocation.TabIndex")));
			this.btnChargeAllocation.Text = resources.GetString("btnChargeAllocation.Text");
			this.btnChargeAllocation.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnChargeAllocation.TextAlign")));
			this.btnChargeAllocation.Visible = ((bool)(resources.GetObject("btnChargeAllocation.Visible")));
			this.btnChargeAllocation.Click += new System.EventHandler(this.btnChargeAllocation_Click);
			// 
			// lblChargeAllocation
			// 
			this.lblChargeAllocation.AccessibleDescription = resources.GetString("lblChargeAllocation.AccessibleDescription");
			this.lblChargeAllocation.AccessibleName = resources.GetString("lblChargeAllocation.AccessibleName");
			this.lblChargeAllocation.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblChargeAllocation.Anchor")));
			this.lblChargeAllocation.AutoSize = ((bool)(resources.GetObject("lblChargeAllocation.AutoSize")));
			this.lblChargeAllocation.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblChargeAllocation.Dock")));
			this.lblChargeAllocation.Enabled = ((bool)(resources.GetObject("lblChargeAllocation.Enabled")));
			this.lblChargeAllocation.Font = ((System.Drawing.Font)(resources.GetObject("lblChargeAllocation.Font")));
			this.lblChargeAllocation.Image = ((System.Drawing.Image)(resources.GetObject("lblChargeAllocation.Image")));
			this.lblChargeAllocation.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblChargeAllocation.ImageAlign")));
			this.lblChargeAllocation.ImageIndex = ((int)(resources.GetObject("lblChargeAllocation.ImageIndex")));
			this.lblChargeAllocation.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblChargeAllocation.ImeMode")));
			this.lblChargeAllocation.Location = ((System.Drawing.Point)(resources.GetObject("lblChargeAllocation.Location")));
			this.lblChargeAllocation.Name = "lblChargeAllocation";
			this.lblChargeAllocation.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblChargeAllocation.RightToLeft")));
			this.lblChargeAllocation.Size = ((System.Drawing.Size)(resources.GetObject("lblChargeAllocation.Size")));
			this.lblChargeAllocation.TabIndex = ((int)(resources.GetObject("lblChargeAllocation.TabIndex")));
			this.lblChargeAllocation.Text = resources.GetString("lblChargeAllocation.Text");
			this.lblChargeAllocation.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblChargeAllocation.TextAlign")));
			this.lblChargeAllocation.Visible = ((bool)(resources.GetObject("lblChargeAllocation.Visible")));
			// 
			// ActualCostRollUp
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
			this.Controls.Add(this.lblChargeAllocation);
			this.Controls.Add(this.lblActualCost);
			this.Controls.Add(this.lblErrorReason);
			this.Controls.Add(this.chkRollup);
			this.Controls.Add(this.picProcessing);
			this.Controls.Add(this.btnDeleteCost);
			this.Controls.Add(this.dtmToDate);
			this.Controls.Add(this.dtmFromDate);
			this.Controls.Add(this.lblToDate);
			this.Controls.Add(this.lblFromDate);
			this.Controls.Add(this.btnPeriodSearch);
			this.Controls.Add(this.dtmRollupDate);
			this.Controls.Add(this.lblRollupDate);
			this.Controls.Add(this.cboCCN);
			this.Controls.Add(this.lblCCN);
			this.Controls.Add(this.btnHelp);
			this.Controls.Add(this.btnClose);
			this.Controls.Add(this.txtPeriodName);
			this.Controls.Add(this.lblPeriodName);
			this.Controls.Add(this.btnRollUp);
			this.Controls.Add(this.lblRollup);
			this.Controls.Add(this.btnChargeAllocation);
			this.Enabled = ((bool)(resources.GetObject("$this.Enabled")));
			this.Font = ((System.Drawing.Font)(resources.GetObject("$this.Font")));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("$this.ImeMode")));
			this.Location = ((System.Drawing.Point)(resources.GetObject("$this.Location")));
			this.MaximizeBox = false;
			this.MaximumSize = ((System.Drawing.Size)(resources.GetObject("$this.MaximumSize")));
			this.MinimumSize = ((System.Drawing.Size)(resources.GetObject("$this.MinimumSize")));
			this.Name = "ActualCostRollUp";
			this.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("$this.RightToLeft")));
			this.StartPosition = ((System.Windows.Forms.FormStartPosition)(resources.GetObject("$this.StartPosition")));
			this.Text = resources.GetString("$this.Text");
			this.Closing += new System.ComponentModel.CancelEventHandler(this.ActualCostRollUp_Closing);
			this.Load += new System.EventHandler(this.ActualCostRollUp_Load);
			((System.ComponentModel.ISupportInitialize)(this.cboCCN)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dtmRollupDate)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dtmToDate)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dtmFromDate)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// Form is load
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ActualCostRollUp_Load(object sender, EventArgs e)
		{
			const string METHOD_NAME = THIS + ".ActualCostRollUp_Load()";
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
				UtilsBO boUtils = new UtilsBO();
				// put data into CCN combo
				FormControlComponents.PutDataIntoC1ComboBox(cboCCN, boUtils.ListCCN().Tables[0], MST_CCNTable.CODE_FLD, MST_CCNTable.CCNID_FLD, MST_CCNTable.TABLE_NAME);
				// set default CCN
				cboCCN.SelectedValue = SystemProperty.CCNID;
				// get current server date
				dtmRollupDate.Value = boUtils.GetDBDate();
				voPeriod = voPeriodFromSetupForm;
				if (voPeriod != null && voPeriod.ActCostAllocationMasterID > 0)
				{
					txtPeriodName.Text = voPeriod.PeriodName;
					dtmFromDate.Value = voPeriod.FromDate;
					dtmToDate.Value = voPeriod.ToDate;
					// determine whether period is rollup or not
					chkRollup.Checked = boActualCost.IsRollup(voPeriod.ActCostAllocationMasterID);
				}
				SwitchMode(false);
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
		/// User select another value from CCN combo
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void cboCCN_SelectedValueChanged(object sender, EventArgs e)
		{
			const string METHOD_NAME = THIS + ".cboCCN_SelectedValueChanged()";
			try
			{
				// clear form
				ClearForm();
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

		private void btnPeriodSearch_Click(object sender, EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnPeriodSearch_Click()";
			try
			{
				DataRowView drwResult = null;
				Hashtable htbCondition = new Hashtable();
				if (cboCCN.SelectedValue != null)
					htbCondition.Add(CST_ActCostAllocationMasterTable.CCNID_FLD, cboCCN.SelectedValue.ToString());
				else
				{
					PCSMessageBox.Show(ErrorCode.MESSAGE_RGA_CCN, MessageBoxIcon.Warning);
					cboCCN.Focus();
					return;
				}
				if (sender is TextBox && sender != null)
					drwResult = FormControlComponents.OpenSearchForm(CST_ActCostAllocationMasterTable.TABLE_NAME, CST_ActCostAllocationMasterTable.PERIODNAME_FLD, txtPeriodName.Text, htbCondition, false);
				else
					drwResult = FormControlComponents.OpenSearchForm(CST_ActCostAllocationMasterTable.TABLE_NAME, CST_ActCostAllocationMasterTable.PERIODNAME_FLD, txtPeriodName.Text, htbCondition, true);
				if (drwResult != null)
				{
					voPeriod = (CST_ActCostAllocationMasterVO)boActualCost.GetPeriod(int.Parse(drwResult[CST_ActCostAllocationMasterTable.ACTCOSTALLOCATIONMASTERID_FLD].ToString()));
					txtPeriodName.Text = voPeriod.PeriodName;
					btnRollUp.Enabled = true;
					if (voPeriod.RollupDate > DateTime.MinValue)
						chkRollup.Checked = true;
					else
						chkRollup.Checked = false;
					dtmFromDate.Value = voPeriod.FromDate;
					dtmToDate.Value = voPeriod.ToDate;
					// enable roll up and delete cost button
					btnRollUp.Enabled = true;
					btnDeleteCost.Enabled = true;
				}
				else
				{
					txtPeriodName.Focus();
					txtPeriodName.SelectAll();
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

		private void txtPeriodName_Validating(object sender, CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtPeriodName_Validating()";
			try
			{
				if (txtPeriodName.Modified)
				{
					if (txtPeriodName.Text != string.Empty)
						btnPeriodSearch_Click(sender, e);
					else
					{
						// clear data in form
						voPeriod = new CST_ActCostAllocationMasterVO();
						txtPeriodName.Text = string.Empty;
						dtmFromDate.Value = DBNull.Value;
						dtmToDate.Value = DBNull.Value;
						// disable roll up and delete cost button
						btnRollUp.Enabled = false;
						btnDeleteCost.Enabled = false;
					}
				}
				FormControlComponents.OnLeaveControl(sender, e);
			}
			catch (PCSException ex)
			{
				// displays the error message.
				PCSMessageBox.Show(ex.mCode, MessageBoxButtons.OK, MessageBoxIcon.Error);
				// log message.
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

		private void txtPeriodName_KeyDown(object sender, KeyEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtPeriodName_KeyDown()";
			try
			{
				if (e.KeyCode == Keys.F4)
					btnPeriodSearch_Click(null, null);
			}
			catch (PCSException ex)
			{
				// displays the error message.
				PCSMessageBox.Show(ex.mCode, MessageBoxButtons.OK, MessageBoxIcon.Error);
				// log message.
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

		/// <summary>
		/// btnRollUp pressed. Start roll up process
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnRollUp_Click(object sender, EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnRollUp_Click()";
			try
			{
				if (cboCCN.SelectedValue.Equals(null))
				{
					PCSMessageBox.Show(ErrorCode.MESSAGE_RGA_CCN, MessageBoxIcon.Warning);
					cboCCN.Focus();
					return;
				}
				if (voPeriod == null || voPeriod.ActCostAllocationMasterID <= 0)
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Warning);
					txtPeriodName.Focus();
					return;
				}
				// try to get period
				try
				{
					voPeriod = (CST_ActCostAllocationMasterVO)boActualCost.GetPeriod(voPeriod.ActCostAllocationMasterID);
					if (voPeriod.ActCostAllocationMasterID <= 0)
						throw new Exception();
				}
				catch
				{
					string[] strMessage = new string[1];
					strMessage[0] = lblPeriodName.Text;
					PCSMessageBox.Show(ErrorCode.MESSAGE_SELECTION_NOT_EXIST, MessageBoxIcon.Error, strMessage);
					// reset data
					txtPeriodName.Text = string.Empty;
					dtmFromDate.Value = DBNull.Value;
					dtmToDate.Value = DBNull.Value;
					btnRollUp.Enabled = false;
					btnDeleteCost.Enabled = false;
					voPeriod = null;
					txtPeriodName.Focus();
					return;
				}
				
				SwitchMode(true);
				
				if (dtbErrorItem != null)
					dtbErrorItem = null;
				
				thrProcess = new Thread(new ThreadStart(RollUp));
				thrProcess.Start();
				if (thrProcess.ThreadState == ThreadState.Stopped || !thrProcess.IsAlive)
					thrProcess.Abort();
			}
			catch (ThreadAbortException ex)
			{
				Logger.LogMessage(ex, METHOD_NAME, Level.DEBUG);
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
		/// Delete cost of selected period
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnDeleteCost_Click(object sender, EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnDeleteCost_Click()";
			try
			{
				// try to get period
				try
				{
					voPeriod = (CST_ActCostAllocationMasterVO)boActualCost.GetPeriod(voPeriod.ActCostAllocationMasterID);
					if (voPeriod.ActCostAllocationMasterID <= 0)
						throw new Exception();
				}
				catch
				{
					string[] strMessage = new string[]{lblPeriodName.Text};
					PCSMessageBox.Show(ErrorCode.MESSAGE_SELECTION_NOT_EXIST, MessageBoxIcon.Error, strMessage);
					// reset data
					txtPeriodName.Text = string.Empty;
					dtmFromDate.Value = DBNull.Value;
					dtmToDate.Value = DBNull.Value;
					btnRollUp.Enabled = false;
					btnDeleteCost.Enabled = false;
					voPeriod = null;
					txtPeriodName.Focus();
					return;
				}
				DialogResult dlgResult = PCSMessageBox.Show(ErrorCode.MESSAGE_DELETE_RECORD, MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
				if (dlgResult == DialogResult.OK)
				{
					try
					{
						SwitchMode(true);
						boActualCost.Delete(voPeriod);
						// actual cost was deleted succesfully
						PCSMessageBox.Show(ErrorCode.MESSAGE_AFTER_SAVE_DATA, MessageBoxButtons.OK, MessageBoxIcon.Information);
					}
					catch
					{
						string[] strMsg = new string[]{lblActualCost.Text};
						PCSMessageBox.Show(ErrorCode.MESSAGE_CAN_NOT_DELETE, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, strMsg);
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
			finally
			{
				chkRollup.Checked = false;
				SwitchMode(false);
			}
		}

		/// <summary>
		/// btnClose prcess. Close the form
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnClose_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		/// <summary>
		/// btnHelp pressed. Displays help
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnHelp_Click(object sender, EventArgs e)
		{
		
		}

		/// <summary>
		/// When user try to closing form while process till running, ask user to confirm action
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ActualCostRollUp_Closing(object sender, CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".ActualCostRollUp_Closing()";
			try
			{
				// ask user to stop the thread
				if (thrProcess != null)
				{
					if (thrProcess.IsAlive || thrProcess.ThreadState == ThreadState.Running)
					{
						string[] strMsg = {this.Text};					
						DialogResult dlgResult = PCSMessageBox.Show(ErrorCode.MESSAGE_PROCESS_IS_RUNNING, MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2, strMsg);
						switch (dlgResult)
						{
							case DialogResult.OK:
								// try to stop the thread
								try
								{
									thrProcess.Abort();
								}
								catch
								{
									e.Cancel = false;
								}
								break;
							case DialogResult.Cancel:
								e.Cancel = true;
								break;
						}
					}
				}
			}
			catch (ThreadAbortException ex)
			{
				Logger.LogMessage(ex.Message, METHOD_NAME, Level.DEBUG);
			}
			catch (PCSException ex)
			{
				// displays the error message.
				PCSMessageBox.Show(ex.mCode, MessageBoxButtons.OK, MessageBoxIcon.Error);
				// log message.
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
		/// <summary>
		/// Rollup actual cost
		/// </summary>
		private void RollUp()
		{
			const string METHOD_NAME = THIS + ".RollUp()";
			try
			{
				this.Cursor = Cursors.WaitCursor;
				dtbErrorItem = RollupActualCost(Convert.ToInt32(cboCCN.SelectedValue), voPeriod, (DateTime)dtmRollupDate.Value, lblErrorReason.Text);
				string[] strMsg = new string[]{this.Text};
				PCSMessageBox.Show(ErrorCode.MESSAGE_TASK_COMPLETED, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, strMsg);
				chkRollup.Checked = true;
				this.Cursor = Cursors.Default;
				SwitchMode(false);
				// if any item not yet setup standard cost, display it in error form
				if (dtbErrorItem != null && dtbErrorItem.Rows.Count > 0)
				{
					NotSetupSTDItems frmNotSetup = new NotSetupSTDItems(dtbErrorItem);
					frmNotSetup.ShowDialog(this);
				}
			}
			catch (ThreadAbortException ex)
			{
				Logger.LogMessage(ex.Message, METHOD_NAME, Level.DEBUG);
			}
			catch (PCSException ex)
			{
				if (ex.mCode == ErrorCode.MESSAGE_CAN_NOT_DELETE)
				{
					string[] strMsg = new string[]{lblActualCost.Text};
					PCSMessageBox.Show(ErrorCode.MESSAGE_CAN_NOT_DELETE, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, strMsg);
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
			}
			catch (Exception ex)
			{
				string[] strMsg = new string[]{this.Text};
				PCSMessageBox.Show(ErrorCode.MESSAGE_CANNOT_ROLL_UP, MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, strMsg);
				try
				{
					Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
				}
				catch
				{
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION);
				}
			}
			finally
			{
				this.Cursor = Cursors.Default;
				SwitchMode(false);
			}
		}
		/// <summary>
		/// Clears form
		/// </summary>
		private void ClearForm()
		{
			dtmRollupDate.Value = DBNull.Value;
			txtPeriodName.Text = string.Empty;
			voPeriod = new CST_ActCostAllocationMasterVO();
			dtmFromDate.Value = DBNull.Value;
			dtmToDate.Value = DBNull.Value;
		}
		/// <summary>
		/// Switchs form mode: processing or waiting
		/// </summary>
		/// <param name="pblnProcessing">True: processing, False: Normal</param>
		private void SwitchMode(bool pblnProcessing)
		{
			cboCCN.ReadOnly = pblnProcessing;
			dtmRollupDate.ReadOnly = pblnProcessing;
			txtPeriodName.Enabled = !pblnProcessing;
			btnPeriodSearch.Enabled = !pblnProcessing;
			btnRollUp.Enabled = !pblnProcessing;
			btnChargeAllocation.Enabled = !pblnProcessing;
			btnDeleteCost.Enabled = !pblnProcessing;
			picProcessing.Visible = pblnProcessing;
			if (!pblnProcessing && voPeriod != null && voPeriod.ActCostAllocationMasterID > 0)
			{
				btnRollUp.Enabled = true;
				btnDeleteCost.Enabled = true;
			}
		}


		private DataTable RollupActualCost(int pintCCNID, object pobjCurrentPeriod, DateTime pdtmRollupDate, string pstrErrorReason)
		{
			CST_ActCostAllocationMasterVO voPeriod = (CST_ActCostAllocationMasterVO)pobjCurrentPeriod;
			voPeriod.ToDate = new DateTime(voPeriod.ToDate.Year, voPeriod.ToDate.Month, voPeriod.ToDate.Day, 23,59,59);
			
			#region error items
			DataTable dtbErrorItems = new DataTable(ITM_ProductTable.TABLE_NAME);
			dtbErrorItems.Columns.Add(new DataColumn(ITM_ProductTable.PRODUCTID_FLD, typeof(int)));
			dtbErrorItems.Columns.Add(new DataColumn(ITM_ProductTable.CODE_FLD, typeof(string)));
			dtbErrorItems.Columns.Add(new DataColumn(ITM_ProductTable.DESCRIPTION_FLD, typeof(string)));
			dtbErrorItems.Columns.Add(new DataColumn(ITM_ProductTable.REVISION_FLD, typeof(string)));
			dtbErrorItems.Columns.Add(new DataColumn(REASON_FLD, typeof(string)));
			#endregion

			#region actual cost table
			DataTable dtbActualCost = new DataTable(CST_ActualCostHistoryTable.TABLE_NAME);
			dtbActualCost.Columns.Add(new DataColumn(CST_ActualCostHistoryTable.ACTCOSTALLOCATIONMASTERID_FLD, typeof(int)));
			dtbActualCost.Columns.Add(new DataColumn(CST_ActualCostHistoryTable.ACTUALCOST_FLD, typeof(decimal)));
			dtbActualCost.Columns[CST_ActualCostHistoryTable.ACTUALCOST_FLD].AllowDBNull = true;
			dtbActualCost.Columns.Add(new DataColumn(CST_ActualCostHistoryTable.ACTUALCOSTHISTORY_FLD, typeof(int)));
			dtbActualCost.Columns[CST_ActualCostHistoryTable.ACTUALCOSTHISTORY_FLD].AutoIncrement = true;
			dtbActualCost.Columns[CST_ActualCostHistoryTable.ACTUALCOSTHISTORY_FLD].AutoIncrementSeed = 1;
			dtbActualCost.Columns[CST_ActualCostHistoryTable.ACTUALCOSTHISTORY_FLD].AutoIncrementStep = 1;
			dtbActualCost.Columns.Add(new DataColumn(CST_ActualCostHistoryTable.COSTELEMENTID_FLD, typeof(int)));
			dtbActualCost.Columns.Add(new DataColumn(CST_ActualCostHistoryTable.PRODUCTID_FLD, typeof(int)));
			dtbActualCost.Columns.Add(new DataColumn(CST_ActualCostHistoryTable.STDCOST_FLD, typeof(decimal)));
			dtbActualCost.Columns[CST_ActualCostHistoryTable.STDCOST_FLD].AllowDBNull = true;
			dtbActualCost.Columns.Add(new DataColumn(CST_ActualCostHistoryTable.QUANTITY_FLD, typeof(decimal)));
			dtbActualCost.Columns[CST_ActualCostHistoryTable.QUANTITY_FLD].AllowDBNull = true;
			dtbActualCost.Columns.Add(new DataColumn(CST_ActualCostHistoryTable.BEGIN_QUANTITY_FLD, typeof(decimal)));
			dtbActualCost.Columns[CST_ActualCostHistoryTable.BEGIN_QUANTITY_FLD].AllowDBNull = true;
			dtbActualCost.Columns.Add(new DataColumn(CST_ActualCostHistoryTable.COMPONENTVALUE_FLD, typeof(decimal)));
			dtbActualCost.Columns[CST_ActualCostHistoryTable.COMPONENTVALUE_FLD].AllowDBNull = true;
			dtbActualCost.Columns.Add(new DataColumn(CST_ActualCostHistoryTable.COMPONENTDSAMOUNT_FLD, typeof(decimal)));
			dtbActualCost.Columns[CST_ActualCostHistoryTable.COMPONENTDSAMOUNT_FLD].AllowDBNull = true;
			dtbActualCost.Columns.Add(new DataColumn(CST_ActualCostHistoryTable.COMBEGINCOST_FLD, typeof(decimal)));
			dtbActualCost.Columns[CST_ActualCostHistoryTable.COMBEGINCOST_FLD].AllowDBNull = true;
			dtbActualCost.Columns.Add(new DataColumn(CST_ActualCostHistoryTable.WOCOMPLETIONQTY_FLD, typeof(decimal)));
			dtbActualCost.Columns[CST_ActualCostHistoryTable.WOCOMPLETIONQTY_FLD].AllowDBNull = true;
			dtbActualCost.Columns.Add(new DataColumn(CST_ActualCostHistoryTable.TRANSACTIONAMOUNT_FLD, typeof(decimal)));
			dtbActualCost.Columns[CST_ActualCostHistoryTable.TRANSACTIONAMOUNT_FLD].AllowDBNull = true;
			dtbActualCost.Columns.Add(new DataColumn(CST_ActualCostHistoryTable.RECYCLEAMOUNT_FLD, typeof(decimal)));
			dtbActualCost.Columns[CST_ActualCostHistoryTable.RECYCLEAMOUNT_FLD].AllowDBNull = true;
			dtbActualCost.Columns.Add(new DataColumn(CST_ActualCostHistoryTable.RECOVERABLEAMOUNT_FLD, typeof(decimal)));
			dtbActualCost.Columns[CST_ActualCostHistoryTable.RECOVERABLEAMOUNT_FLD].AllowDBNull = true;
			dtbActualCost.Columns.Add(new DataColumn(CST_ActualCostHistoryTable.FREIGHTAMOUNT_FLD, typeof(decimal)));
			dtbActualCost.Columns[CST_ActualCostHistoryTable.FREIGHTAMOUNT_FLD].AllowDBNull = true;
			dtbActualCost.Columns.Add(new DataColumn(CST_ActualCostHistoryTable.DSAMOUNT_FLD, typeof(decimal)));
			dtbActualCost.Columns[CST_ActualCostHistoryTable.DSAMOUNT_FLD].AllowDBNull = true;
			dtbActualCost.Columns.Add(new DataColumn(CST_ActualCostHistoryTable.DSOKAMOUNT_FLD, typeof(decimal)));
			dtbActualCost.Columns[CST_ActualCostHistoryTable.DSOKAMOUNT_FLD].AllowDBNull = true;
			dtbActualCost.Columns.Add(new DataColumn(CST_ActualCostHistoryTable.ADJUSTAMOUNT_FLD, typeof(decimal)));
			dtbActualCost.Columns[CST_ActualCostHistoryTable.ADJUSTAMOUNT_FLD].AllowDBNull = true;
			dtbActualCost.Columns.Add(new DataColumn(CST_ActualCostHistoryTable.BEGINCOST_FLD, typeof(decimal)));
			dtbActualCost.Columns[CST_ActualCostHistoryTable.BEGINCOST_FLD].AllowDBNull = true;
			#endregion

			#region prepare data for rollup
			DataSet dstData = new DataSet();
			DateTime dtmCurrentServerDate = new UtilsBO().GetDBDate();
			DataTable dtbTopItems = boActualCost.GetTopItems(pintCCNID);
			dtbTopItems.TableName = TOP_ITEM;
			dstData.Tables.Add(dtbTopItems);
			DataTable dtbCostElements = boActualCost.GetCostElements(pintCCNID);
			dtbCostElements.TableName = COST_ELEMENT;
			dstData.Tables.Add(dtbCostElements);
			DataTable dtbSTDCost = boActualCost.GetSTDCost(pintCCNID);
			dtbSTDCost.TableName = CST_STDItemCostTable.TABLE_NAME;
			dstData.Tables.Add(dtbSTDCost);
			DataTable dtbCostingAdjustment = boActualCost.GetCostFromCostCenterRate(pintCCNID);
			dtbCostingAdjustment.TableName = COST_ADJUST;
			dstData.Tables.Add(dtbCostingAdjustment);
			DataTable dtbBOM = boActualCost.GetBOM(pintCCNID);
			dtbBOM.TableName = ITM_BOMTable.TABLE_NAME;
			dstData.Tables.Add(dtbBOM);
			// onhand cache quantity
			DataTable dtbOnhandCache = boActualCost.GetBalanceQuantity(voPeriod.FromDate);
			dtbOnhandCache.TableName = OHCACHE_TABLE;
			dstData.Tables.Add(dtbOnhandCache);
			// begin cost
			DataTable dtbBeginCost = boActualCost.FindLastPeriodHasCost(voPeriod.FromDate);
			if (dtbBeginCost != null)
			{
				dtbBeginCost.TableName = BEGINCOST_TABLE;
				dstData.Tables.Add(dtbBeginCost);
			}
			// inventory adjustment
			DataTable dtbAdjustmentInPeriod = boActualCost.GetAdjusment(pintCCNID, voPeriod.FromDate, voPeriod.ToDate);
			dtbAdjustmentInPeriod.TableName = ADJUSTMENT_PERIOD;
			dstData.Tables.Add(dtbAdjustmentInPeriod);
			// recycle material
			DataTable dtbRecoverableQuantityInPeriod = boActualCost.GetRecoverableQuantity(pintCCNID, voPeriod.FromDate, voPeriod.ToDate);
			dtbRecoverableQuantityInPeriod.TableName = RECOVER_PERIOD;
			dstData.Tables.Add(dtbRecoverableQuantityInPeriod);
			// shipping management
			DataTable dtbShippingInPeriod = boActualCost.GetShipTransaction(pintCCNID, voPeriod.FromDate, voPeriod.ToDate);
			dtbShippingInPeriod.TableName = "dtbShippingInPeriod";
			dstData.Tables.Add(dtbShippingInPeriod);
			// shipping adjustment
			DataTable dtbShipAdjustment = boActualCost.GetShipAdjustment(pintCCNID, voPeriod.FromDate, dtmCurrentServerDate);
			dtbShipAdjustment.TableName = "dtbShipAdjustment";
			dstData.Tables.Add(dtbShipAdjustment);
			// return goods receive
			DataTable dtbReturnGoodsReceiveInPeriod = boActualCost.GetReturnGoodsReceipt(pintCCNID, voPeriod.FromDate, voPeriod.ToDate);
			dtbReturnGoodsReceiveInPeriod.TableName = RETURN_GOODS_PERIOD;
			dstData.Tables.Add(dtbReturnGoodsReceiveInPeriod);
			// data from misc. issue transaction
			DataTable dtbMiscIssueInPeriod = boActualCost.GetDataFromMiscIssue(pintCCNID, voPeriod.FromDate, voPeriod.ToDate);
			dtbMiscIssueInPeriod.TableName = MISC_INPERIOD_TABLE;
			dstData.Tables.Add(dtbMiscIssueInPeriod);
			// work order completion
			DataTable dtbWOCompletedQuantityInPeriod = boActualCost.GetWOCompletedQuantity(pintCCNID, voPeriod.FromDate, voPeriod.ToDate);
			dtbWOCompletedQuantityInPeriod.TableName = WO_COMLETED_PERIOD;
			dstData.Tables.Add(dtbWOCompletedQuantityInPeriod);
			// work order completion for component (work order completion & po receipt by outside
			DataTable dtbWOComponent = boActualCost.GetCompletionQuantityForComponent(pintCCNID, voPeriod.FromDate, dtmCurrentServerDate);
			dtbWOComponent.TableName = COMPLETE_COMP;
			dstData.Tables.Add(dtbWOComponent);
			// po receipt by slip and outside
			DataTable dtbPOReceiptInPeriod = boActualCost.GetPOReceipt(pintCCNID, voPeriod.FromDate, voPeriod.ToDate);
			dtbPOReceiptInPeriod.TableName = PO_RECEIPT_INPERIOD;
			dstData.Tables.Add(dtbPOReceiptInPeriod);
			// po receipt by invoice
			DataTable dtbPOReceiptByInvoiceInPeriod = boActualCost.GetPOReceiptByInvoice(pintCCNID, voPeriod.FromDate, voPeriod.ToDate);
			dtbPOReceiptByInvoiceInPeriod.TableName = RECEIPT_INVOICE_PERIOD;
			dstData.Tables.Add(dtbPOReceiptByInvoiceInPeriod);
			// return to vendor
			DataTable dtbReturnToVendorInPeriod = boActualCost.GetReturnToVendor(pintCCNID, voPeriod.FromDate, voPeriod.ToDate);
			dtbReturnToVendorInPeriod.TableName = "dtbReturnToVendorInPeriod";
			dstData.Tables.Add(dtbReturnToVendorInPeriod);
			// additional charge amount (freight, import tax, credit, debit)
			DataTable dtbAdditionalCharge = boActualCost.GetAdditionalCharge(pintCCNID, voPeriod.FromDate, voPeriod.ToDate);
			dtbAdditionalCharge.TableName = ADD_CHARGE;
			dstData.Tables.Add(dtbAdditionalCharge);
			// allocation amount
			DataTable dtbAllocationAmount = boActualCost.GetAllocationAmount(voPeriod.ActCostAllocationMasterID);
			dtbAllocationAmount.TableName = ALLOCATION;
			dstData.Tables.Add(dtbAllocationAmount);
			// component scrap
			DataTable dtbComponentScrap = boActualCost.GetComponentScrap(pintCCNID, voPeriod.FromDate, voPeriod.ToDate);
			dtbComponentScrap.TableName = COMPONENT_SCRAP;
			dstData.Tables.Add(dtbComponentScrap);
			
			#endregion

			// start roll up
			dtbActualCost = Rollup(pintCCNID, dtbActualCost, ref dtbErrorItems, dstData, null, pstrErrorReason);
			// update to database
			DataSet dstActualCost = new DataSet();
			dstActualCost.Tables.Add(dtbActualCost);

			// update rollup date of period
			voPeriod.RollupDate = pdtmRollupDate;
			
			// save result to database
			// refine ToDate to begin day
			voPeriod.ToDate = new DateTime(voPeriod.ToDate.Year, voPeriod.ToDate.Month, voPeriod.ToDate.Day);
			boActualCost.SaveData(dstActualCost, voPeriod);
			
			return dtbErrorItems;
		}
		
		/// <summary>
		/// Rollup actual cost from child to parent
		/// </summary>
		/// <param name="pintCCNID">CCN</param>
		/// <param name="pdtbActualCost">Rollup Result</param>
		/// <param name="pdtbErrorItems">Items which not setup standard cost yet</param>
		/// <param name="pdstData">Data for Rollup</param>
		/// <param name="pdrowItem">Current Item</param>
		/// <param name="pstrErrorReason"></param>
		/// <returns>Rollup result</returns>
		private DataTable Rollup(int pintCCNID, DataTable pdtbActualCost, ref DataTable pdtbErrorItems, DataSet pdstData,
			DataRow pdrowItem, string pstrErrorReason)
		{
			if (pdrowItem == null)
			{
				DataTable dtbTopItems = pdstData.Tables[TOP_ITEM];
				foreach (DataRow drowData in dtbTopItems.Rows)
					Rollup(pintCCNID, pdtbActualCost, ref pdtbErrorItems, pdstData, drowData, pstrErrorReason);
			}
			else
			{
				#region prepare variable

				string strProductID = pdrowItem[ITM_ProductTable.PRODUCTID_FLD].ToString();
				int intCostingAdjustmentID = 0;
				try
				{
					intCostingAdjustmentID = Convert.ToInt32(pdrowItem[ITM_ProductTable.ACADJUSTMENTMASTERID_FLD]);
				}
				catch{}
				DataTable dtbBOM = pdstData.Tables[ITM_BOMTable.TABLE_NAME];
				string strProductFilter = ITM_ProductTable.PRODUCTID_FLD + "='" + strProductID + "'";
				DataRow[] drowSubItem = dtbBOM.Select(PARENT_ID_FLD + "='" + strProductID + "'");
				foreach (DataRow drowData in drowSubItem)
					Rollup(pintCCNID, pdtbActualCost, ref pdtbErrorItems, pdstData, drowData, pstrErrorReason);;
				DataTable dtbSTDCost = pdstData.Tables[CST_STDItemCostTable.TABLE_NAME];

				#endregion

				// if item is not yet setup standard cost
				if (!IsSetupSTDCost(strProductID, dtbSTDCost))
				{
					#region add to error item

					DataRow drowError = pdtbErrorItems.NewRow();
					drowError[ITM_ProductTable.PRODUCTID_FLD] = strProductID;
					drowError[ITM_ProductTable.CODE_FLD] = pdrowItem[ITM_ProductTable.CODE_FLD];
					drowError[ITM_ProductTable.DESCRIPTION_FLD] = pdrowItem[ITM_ProductTable.DESCRIPTION_FLD];
					drowError[ITM_ProductTable.REVISION_FLD] = pdrowItem[ITM_ProductTable.REVISION_FLD];
					drowError[REASON_FLD] = pstrErrorReason;
					pdtbErrorItems.Rows.Add(drowError);

					#endregion
				}
				else
				{
					#region retrieve all table from data set

					DataTable dtbOnhandCache = pdstData.Tables[OHCACHE_TABLE];
					DataTable dtbBeginCost = null;
					if (pdstData.Tables.Contains(BEGINCOST_TABLE))
						dtbBeginCost = pdstData.Tables[BEGINCOST_TABLE];
					DataTable dtbMiscIssueInPeriod = pdstData.Tables[MISC_INPERIOD_TABLE];
					DataTable dtbCostingAdjustment = pdstData.Tables[COST_ADJUST];
					DataTable dtbReturnToVendorInPeriod = pdstData.Tables["dtbReturnToVendorInPeriod"];
					DataTable dtbWOCompletedQuantityInPeriod = pdstData.Tables[WO_COMLETED_PERIOD];
					DataTable dtbAdjustmentInPeriod = pdstData.Tables[ADJUSTMENT_PERIOD];
					DataTable dtbPOReceiptInPeriod = pdstData.Tables[PO_RECEIPT_INPERIOD];
					DataTable dtbPOReceiptByInvoiceInPeriod = pdstData.Tables[RECEIPT_INVOICE_PERIOD];
					DataTable dtbReturnGoodsReceiveInPeriod = pdstData.Tables[RETURN_GOODS_PERIOD];
					DataTable dtbRecoverableQuantityInPeriod = pdstData.Tables[RECOVER_PERIOD];
					DataTable dtbAdditionalCharge = pdstData.Tables[ADD_CHARGE];
					DataTable dtbCostElements = pdstData.Tables[COST_ELEMENT];
					DataTable dtbAllocationAmount = pdstData.Tables[ALLOCATION];
					DataTable dtbComponentScrap = pdstData.Tables[COMPONENT_SCRAP];

					#endregion

					#region •	Begin quantity
					
					decimal decBeginQuantity = 0, decReturnToVendorInPeriod = 0;
					
					try
					{
						decBeginQuantity = Convert.ToDecimal(dtbOnhandCache.Compute("SUM(OHQuantity)", strProductFilter));
					}
					catch{}

					string strExpression = strProductFilter;

					#endregion

					#region •	Freight Amount

					string strFreightFilter = strProductFilter + " AND "
						+ cst_FreightMasterTable.ACPURPOSEID_FLD + "='" + (int)ACPurpose.Freight + "'";
					decimal decFreightAmount = 0;
					try
					{
						decFreightAmount = Convert.ToDecimal(dtbAdditionalCharge.Compute("SUM(Amount)", strFreightFilter));
					}
					catch{}

					#endregion

					#region •	Import Tax Amount

					string strImportTaxFilter = strProductFilter + " AND "
						+ cst_FreightMasterTable.ACPURPOSEID_FLD + "='" + (int)ACPurpose.ImportTax + "'";
					decimal decImportTaxAmount = 0;
					try
					{
						decImportTaxAmount = Convert.ToDecimal(dtbAdditionalCharge.Compute("SUM(Amount)", strImportTaxFilter));
					}
					catch{}

					#endregion

					#region •	Debit Amount

					string strDebitFilter = strProductFilter + " AND "
						+ cst_FreightMasterTable.ACPURPOSEID_FLD + "=" + (int)ACPurpose.DebitNote;// + " AND "
//						+ cst_FreightMasterTable.ACOBJECTID_FLD + "=" + (int)ACObject.ItemInventory;
					decimal decDebitAmount = 0;
					try
					{
						decDebitAmount = Convert.ToDecimal(dtbAdditionalCharge.Compute("SUM(Amount)", strDebitFilter));
					}
					catch{}

					#endregion

					#region •	Credit Amount

					string strCreditFilter = strProductFilter + " AND "
						+ cst_FreightMasterTable.ACPURPOSEID_FLD + "=" + (int)ACPurpose.CreditNote;// + " AND "
//						+ cst_FreightMasterTable.ACOBJECTID_FLD + "=" + (int)ACObject.ItemInventory;
					decimal decCreditAmount = 0;
					try
					{
						decCreditAmount = Convert.ToDecimal(dtbAdditionalCharge.Compute("SUM(Amount)", strCreditFilter));
					}
					catch{}

					#endregion

					#region •	PO Receipt by slip and outside amount

					decimal decPOReceiptAmount = 0, decRate = 0, decPOReceiptQuantityInPeriod = 0;
					decimal decPOByOutsideInPeriod = 0;
					try
					{
						decPOReceiptQuantityInPeriod = Convert.ToDecimal(dtbPOReceiptInPeriod.Compute("SUM(Quantity)", strProductFilter));
					}
					catch{}
					try
					{
						string strOutsideFilter = strProductFilter + " AND " + PO_PurchaseOrderReceiptMasterTable.RECEIPTTYPE_FLD + "='" + ((int)POReceiptTypeEnum.ByOutside).ToString() + "'";
						decPOByOutsideInPeriod = Convert.ToDecimal(dtbPOReceiptInPeriod.Compute("SUM(Quantity)", strOutsideFilter));
						//decPOReceiptQuantityInPeriod += decPOByOutsideInPeriod;
					}
					catch{}
					DataRow[] drowsData = dtbPOReceiptInPeriod.Select(strProductFilter);
					foreach (DataRow drowData in drowsData)
					{
						decimal decQuantity = 0, decPrice = 0;
						try
						{
							decQuantity = Convert.ToDecimal(drowData["Quantity"]);
						}
						catch{}
						try
						{
							decPrice = Convert.ToDecimal(drowData[PO_PurchaseOrderDetailTable.UNITPRICE_FLD]);
						}
						catch{}
						try
						{
							decRate = Convert.ToDecimal(drowData[PO_PurchaseOrderMasterTable.EXCHANGERATE_FLD]);
						}
						catch{}
						decPOReceiptAmount += decQuantity * decPrice * decRate;
					}
					#endregion

					#region •	PO Receipt by invoice amount

					decimal decReceiveByInvoiceAmount = 0;
					drowsData = null;
					drowsData = dtbPOReceiptByInvoiceInPeriod.Select(strProductFilter);
					try
					{
						decPOReceiptQuantityInPeriod += Convert.ToDecimal(dtbPOReceiptByInvoiceInPeriod.Compute("SUM(Quantity)", strProductFilter));
					}
					catch{}
					try
					{
						decReceiveByInvoiceAmount = Convert.ToDecimal(dtbPOReceiptByInvoiceInPeriod.Compute("SUM(" + PO_InvoiceDetailTable.CIPAMOUNT_FLD + ")", strProductFilter));
					}
					catch{}
					
					#endregion

					#region •	Total Inpunt Quantity In Period

					#region •	Work Order Comleption In Period

					decimal decWOCompleteInPeriod = 0;
					try
					{
						decWOCompleteInPeriod = Convert.ToDecimal(dtbWOCompletedQuantityInPeriod.Compute("SUM(Quantity)", strProductFilter));
					}
					catch{}

					#endregion

					#region •	Return Goods Receive In Period

					decimal decRGRInPeriod = 0;
					try
					{
						decRGRInPeriod = Convert.ToDecimal(dtbReturnGoodsReceiveInPeriod.Compute("SUM(Quantity)", strProductFilter));
					}
					catch{}

					#endregion

					#region •	Recoverable Material In Period

					decimal decRecoverableMaterialInPeriod = 0;
					try
					{
						string strRecycleFilter = " ComponentID = '" + strProductID + "'";
						decRecoverableMaterialInPeriod = Convert.ToDecimal(dtbRecoverableQuantityInPeriod.Compute("SUM(RecoverQuantity)", strRecycleFilter));
					}
					catch{}
					
					#endregion

					#region •	Positive Inventory Adjustment In Period (Except Bin DS) (UsedByCosting = true)

					strExpression += " AND " + MST_BINTable.BINTYPEID_FLD + " <> '" + (int)BinTypeEnum.LS + "'"
						+ " AND Quantity > 0 AND UsedByCosting = 1";
					decimal decPositiveAdjustmentInPeriod = 0;
					try
					{
						decPositiveAdjustmentInPeriod = Convert.ToDecimal(dtbAdjustmentInPeriod.Compute("SUM(Quantity)", strExpression));
					}
					catch{}
					strExpression = strProductFilter;

					#endregion

					#region •	Inventory Adjustment In Period (Except Bin DS) (UsedByCosting = true)

					strExpression += " AND " + MST_BINTable.BINTYPEID_FLD + " <> '" + (int)BinTypeEnum.LS + "'";
					decimal decInventoryAdjustQuantityInPeriod = 0;
					try
					{
						decInventoryAdjustQuantityInPeriod = Convert.ToDecimal(dtbAdjustmentInPeriod.Compute("SUM(Quantity)", strExpression));
					}
					catch{}
					strExpression = strProductFilter;

					#endregion

					#region •	Misc. Issue From DS To Not DS In Period

					strExpression += " AND " + SOURCE_TYPE_FLD + " = '" + (int)BinTypeEnum.LS + "'"
						+ " AND " + DEST_TYPE_FLD + " <> '" + (int)BinTypeEnum.LS + "'";
					decimal decMiscIssueFromDS2NotDSInPeriod = 0;
					try
					{
						decMiscIssueFromDS2NotDSInPeriod = Convert.ToDecimal(dtbMiscIssueInPeriod.Compute("SUM(Quantity)", strExpression));
					}
					catch{}
					strExpression = strProductFilter;

					#endregion

					#region •	Return To Vendor

					try
					{
						decReturnToVendorInPeriod = Convert.ToDecimal(dtbReturnToVendorInPeriod.Compute("SUM(Quantity)", strExpression));
					}
					catch{}
					
					#endregion

//					decimal decTotalInputInPeriod = decPOReceiptQuantityInPeriod + decWOCompleteInPeriod
//						+ decRGRInPeriod + decRecoverableMaterialInPeriod + decPositiveAdjustmentInPeriod + decMiscIssueFromDS2NotDSInPeriod;
					decimal decTotalInputInPeriod = decPOReceiptQuantityInPeriod + decWOCompleteInPeriod - decReturnToVendorInPeriod;
						/*+ decRecoverableMaterialInPeriod + decPositiveAdjustmentInPeriod;*/// + decMiscIssueFromDS2NotDSInPeriod;

					#endregion

					#region •	Destroy Quantity In Period

					decimal decDestroyInPeriod = 0;

					#region •	Misc. Issue.Purpose= Xuat Huy with FromBin= not DS & ToBin= DS

					try
					{
						string strFilter = strProductFilter + " AND " + IV_MiscellaneousIssueMasterTable.ISSUEPURPOSEID_FLD
							+ " = '" + (int)PurposeEnum.Scrap + "'"
							+ " AND " + SOURCE_TYPE_FLD + " <> '" + (int)BinTypeEnum.LS + "'"
							+ " AND " + DEST_TYPE_FLD + " = '" + (int)BinTypeEnum.LS + "'";
						decDestroyInPeriod += Convert.ToDecimal(dtbMiscIssueInPeriod.Compute("SUM(Quantity)", strFilter));
					}
					catch{}

					#endregion

					#region •	PO Receipt with ToBin= DS
					
					try
					{
						string strFilter = strProductFilter + " AND " + MST_BINTable.BINTYPEID_FLD + " = '" + (int)BinTypeEnum.LS + "'";
						decDestroyInPeriod += Convert.ToDecimal(dtbPOReceiptInPeriod.Compute("SUM(Quantity)", strFilter));
					}
					catch{}
					try
					{
						string strFilter = strProductFilter + " AND " + MST_BINTable.BINTYPEID_FLD + " = '" + (int)BinTypeEnum.LS + "'";
						decDestroyInPeriod += Convert.ToDecimal(dtbPOReceiptByInvoiceInPeriod.Compute("SUM(Quantity)", strFilter));
					}
					catch{}

					#endregion

					#region •	WO Completion with ToBin= DS

					try
					{
						string strFilter = strProductFilter + " AND " + MST_BINTable.BINTYPEID_FLD + " = '" + (int)BinTypeEnum.LS + "'";
						decDestroyInPeriod += Convert.ToDecimal(dtbWOCompletedQuantityInPeriod.Compute("SUM(Quantity)", strFilter));
					}
					catch{}

					#endregion

					#region •	Return Goods Receive with ToBin=DS

					try
					{
						string strFilter = strProductFilter + " AND " + MST_BINTable.BINTYPEID_FLD + " = '" + (int)BinTypeEnum.LS + "'";
						decDestroyInPeriod += Convert.ToDecimal(dtbReturnGoodsReceiveInPeriod.Compute("SUM(Quantity)", strFilter));
					}
					catch{}

					#endregion

					#region •	Inventory Adjustment with Bin= DS

					try
					{
						string strFilter = strProductFilter + " AND " + MST_BINTable.BINTYPEID_FLD + " = '" + (int)BinTypeEnum.LS + "'"
							+ " AND Quantity > 0";
						decDestroyInPeriod += Convert.ToDecimal(dtbAdjustmentInPeriod.Compute("SUM(Quantity)", strFilter));
					}
					catch{}

					#endregion

					#endregion

					foreach (DataRow drowElement in dtbCostElements.Rows)
					{
						#region prepare variable

						int intElementType = Convert.ToInt32(drowElement[TYPE_CODE_FLD]);
						bool blnMakeItem = Convert.ToBoolean(pdrowItem[ITM_ProductTable.MAKEITEM_FLD]);
						// for none make item, only roll element have type is Material and Sub-Material
						if (!blnMakeItem && intElementType != (int)CostElementType.Material
							&& intElementType != (int)CostElementType.SubMaterial)
							continue;
						string strElementID = drowElement[STD_CostElementTable.COSTELEMENTID_FLD].ToString();
						string strElementFilter = strProductFilter + " AND "
							+ STD_CostElementTable.COSTELEMENTID_FLD + "='" + strElementID + "'";
						// check if already calculate, continue next element
						DataRow[] drowExist = pdtbActualCost.Select(strElementFilter);
						if (drowExist.Length > 0)
							continue;
						decimal decPeriodQuantity = 0, decWORawAmount = 0, decComponentValue = 0;
						decimal decOutsideRawAmount = 0, decItemCost = 0, decSTDCost = 0, decComponentScrapAmount = 0;
						decimal decBeginCost = 0, decCostingAdjust = 0, decComBeginCost = 0;

						#endregion

						#region •	Standard Cost of current item

						try
						{
							decSTDCost = Convert.ToDecimal(dtbSTDCost.Compute("SUM(Cost)", strElementFilter));
						}
						catch{}
						
						#endregion

						if (!blnMakeItem && decSTDCost == 0 && 
							(intElementType == (int)CostElementType.Material ||
							intElementType == (int)CostElementType.SubMaterial))
							continue;

						#region •	Begin Cost

						// use actual cost of previous period as begin cost
						if (dtbBeginCost != null)
						{
							try
							{
								decBeginCost = Convert.ToDecimal(dtbBeginCost.Compute("SUM(ActualCost)", strElementFilter));
							}
							catch{}
						}
						else // if current period is first period or there is no data from any previous period, use std cost as begin cost
						{
							try
							{
								decBeginCost = Convert.ToDecimal(dtbSTDCost.Compute("SUM(Cost)", strElementFilter));
							}
							catch{}
						}

						#endregion

						#region •	Component Begin Cost, scrap quantity, component scrap amount

						foreach (DataRow drowChild in drowSubItem)
						{
							decimal decBOMQty = Convert.ToDecimal(drowChild[ITM_BOMTable.QUANTITY_FLD]);
							decimal decChildCost = 0, decScrapQuantity = 0;
							string strComponentID = drowChild[ITM_ProductTable.PRODUCTID_FLD].ToString();
							string strChildFilter = ITM_ProductTable.PRODUCTID_FLD + "='" + strComponentID + "'";
							
							if (dtbBeginCost != null && dtbBeginCost.Select(strChildFilter).Length > 0)
							{
								strChildFilter += " AND " + STD_CostElementTable.COSTELEMENTID_FLD + " = " + strElementID;
								try
								{
									decChildCost = Convert.ToDecimal(dtbBeginCost.Compute("SUM(ActualCost)", strChildFilter));
								}
								catch{}
							}
							else
							{
								strChildFilter += " AND " + STD_CostElementTable.COSTELEMENTID_FLD + " = " + strElementID;
								try
								{
									decChildCost = Convert.ToDecimal(dtbSTDCost.Compute("SUM(Cost)", strChildFilter));
								}
								catch{}
							}
							string strScrapFilter = ITM_ProductTable.PRODUCTID_FLD + "=" + strProductID + " AND "
								+ ITM_BOMTable.COMPONENTID_FLD + "=" + strComponentID;
							try
							{
								decScrapQuantity = Convert.ToDecimal(dtbComponentScrap.Compute("SUM(" + PRO_ComponentScrapDetailTable.SCRAPQUANTITY_FLD + ")", strScrapFilter));
							}
							catch{}

							decComBeginCost += decChildCost * decBOMQty;
							decComponentScrapAmount += decChildCost * decScrapQuantity;
						}

						#endregion

						#region •	Costing Adjustment

						if (blnMakeItem && intCostingAdjustmentID > 0) // costing adjustment for make item already configured cost center rate only
						{
							try
							{
								string strCostingAdjustFilter = STD_CostElementTable.COSTELEMENTID_FLD + "='" + strElementID + "'"
									+ " AND " + cst_ACAdjustmentMasterTable.ACADJUSTMENTMASTERID_FLD + "='" + intCostingAdjustmentID + "'";
								// 26-05-2006: based on thuypt request
								decCostingAdjust = Convert.ToDecimal(dtbCostingAdjustment.Compute("SUM(Cost)", strCostingAdjustFilter));
							}
							catch{}
						}

						#endregion

						#region •	Allocation amount
						
						decimal decAllocationAmount = 0;
						try
						{
							decAllocationAmount = Convert.ToDecimal(dtbAllocationAmount.Compute("SUM(" + CST_AllocationResultTable.AMOUNT_FLD + ")", strElementFilter));
						}
						catch{}

						#endregion

						#region •	Recycle & Recoverable Amount

						// recover amount of all components of current item;
						decimal decRecoverableAmount = 0, decRecycleAmount = 0;
						drowsData = dtbRecoverableQuantityInPeriod.Select(strProductFilter);
						foreach (DataRow drowData in drowsData)
						{
							// recover item
							string strComponentID = drowData[ITM_BOMTable.COMPONENTID_FLD].ToString();
							// standard cost by element of recovered item
							string strRecoverFilter = ITM_ProductTable.PRODUCTID_FLD + "='" + strComponentID + "'"
								+ " AND " + STD_CostElementTable.COSTELEMENTID_FLD + " ='" + strElementID + "'";
							decimal decRecoverQuantity = 0, decCost = 0;
							try
							{
								// change from standard cost to Actual cost
								decCost = Convert.ToDecimal(pdtbActualCost.Compute("SUM(ActualCost)", strRecoverFilter));
							}
							catch{}
							try
							{
								decRecoverQuantity = Convert.ToDecimal(drowData[CST_RecoverMaterialDetailTable.RECOVERQUANTITY_FLD]);
							}
							catch{}
							// calculate salvaging amount based on detail
							// salvaging amount = recoverable quantity * cost from std cost
							decRecycleAmount += (decRecoverQuantity * decCost);
						}
						// recoverable value of current item
						decimal decRecoverableQuantity = 0;
						try
						{
							decRecoverableQuantity = Convert.ToDecimal(dtbRecoverableQuantityInPeriod.Compute("SUM(RecoverQuantity)", " ComponentID = '" + strProductID + "'"));
						}
						catch{}
						decRecoverableAmount = decRecoverableQuantity * decSTDCost;
						
						#endregion

						decimal decTransactionAmount = 0;
						// make item
						if (blnMakeItem)
						{
							#region calculate work order & po receipt amount (raw)

							if (decPOByOutsideInPeriod != 0 || decWOCompleteInPeriod != 0)
							{
								foreach (DataRow drowChild in drowSubItem)
								{
									decimal decBOMQty = Convert.ToDecimal(drowChild[ITM_BOMTable.QUANTITY_FLD]);
									decimal decChildCost = 0, decChildTransactionAmount = 0;
									string strComponentID = drowChild[ITM_ProductTable.PRODUCTID_FLD].ToString();
									string strChildFilter = ITM_ProductTable.PRODUCTID_FLD + "='" + strComponentID + "'";
									strChildFilter += " AND " + STD_CostElementTable.COSTELEMENTID_FLD + " = " + strElementID;

									try
									{
										decChildCost = Convert.ToDecimal(pdtbActualCost.Compute("SUM(ActualCost)", strChildFilter));
									}
									catch{}
									try
									{
										decChildTransactionAmount = Convert.ToDecimal(pdtbActualCost.Compute("SUM(TransactionAmount)", strChildFilter));
									}
									catch{}

									decimal decChildAllocationAmount = 0;
									try
									{
										decChildAllocationAmount = Convert.ToDecimal(dtbAllocationAmount.Compute("SUM(" + CST_AllocationResultTable.AMOUNT_FLD + ")", strChildFilter));
									}
									catch{}

									// work order amount
									decWORawAmount += decChildCost * decBOMQty * decWOCompleteInPeriod;
									// po receipt by outside amount
									decOutsideRawAmount += decChildCost * decBOMQty * decPOByOutsideInPeriod;
									// transaction amount
									decTransactionAmount += (decChildTransactionAmount + decChildAllocationAmount) * decBOMQty;
								}
							}
							#endregion

							#region Item actual cost

							switch (intElementType)
							{
								case (int)CostElementType.Material:
									// IF BeginQty=0, Sum(ReturnGoodsReceipt.Qty+ RecycleMaterialDetail.Qty
									// + Miscellaneous.Qty with FromBin=DS & ToBin=not DS)<>0 
									// and Sum(OrtherInputQty in the period)=0
									if (decBeginQuantity == 0 && 
										(decRGRInPeriod + decRecoverableMaterialInPeriod + decMiscIssueFromDS2NotDSInPeriod) != 0 &&
										(decPOReceiptQuantityInPeriod + decWOCompleteInPeriod + decPositiveAdjustmentInPeriod) == 0)
									{
										// MaterialCost(i)= LastPeriodHasItemCost.MaterialCost(i) + AllocationAmount(i)/ 
										// Sum(ReturnGoodsReceipt.Qty+ RecycleMaterialDetail.Qty
										// + Miscellaneous.Qty with FromBin=DS & ToBin=not DS)
										try
										{
											decPeriodQuantity = decRGRInPeriod + decRecoverableMaterialInPeriod + decMiscIssueFromDS2NotDSInPeriod;
											decItemCost = (decBeginCost + decAllocationAmount) / decPeriodQuantity;
										}
										catch{}
									}
									else
									{
										// MaterialCost(i) = ((BeginQty* MaterialUnitCost)+ UsedMaterial Amount
										// – RecycleAmount + AllocationAmount(i))/ (BeginQty + Total InputQty – DestroyQty)
										try
										{
											decPeriodQuantity = decBeginQuantity + decTotalInputInPeriod;// - decDestroyInPeriod;
											decimal decUsedMaterialAmount = decPOReceiptAmount + decReceiveByInvoiceAmount
												+ decFreightAmount + decImportTaxAmount + decCreditAmount - decDebitAmount
												+ decWORawAmount + decOutsideRawAmount + decCostingAdjust;
											decItemCost = ((decBeginCost * decBeginQuantity) + decUsedMaterialAmount
												/*- decRecycleAmount */+ decAllocationAmount) / decPeriodQuantity;
											decComponentValue = (decWORawAmount + decOutsideRawAmount + decPOReceiptAmount + decReceiveByInvoiceAmount)/decTotalInputInPeriod;
										}
										catch{}
									}
									break;
								case (int)CostElementType.SubMaterial:
									// IF BeginQty=0, Sum(ReturnGoodsReceipt.Qty+ RecycleMaterialDetail.Qty
									// + Miscellaneous.Qty with FromBin=DS & ToBin=not DS)<>0 
									// and Sum(OrtherInputQty in the period)=0
									if (decBeginQuantity == 0 && 
										(decRGRInPeriod + decRecoverableMaterialInPeriod + decMiscIssueFromDS2NotDSInPeriod) != 0 &&
										(decPOReceiptQuantityInPeriod + decWOCompleteInPeriod + decPositiveAdjustmentInPeriod) == 0)
									{
										// MaterialCost(i)= LastPeriodHasItemCost.MaterialCost(i) + AllocationAmount(i)/ 
										// Sum(ReturnGoodsReceipt.Qty+ RecycleMaterialDetail.Qty
										// + Miscellaneous.Qty with FromBin=DS & ToBin=not DS)
										try
										{
											decPeriodQuantity = decRGRInPeriod + decRecoverableMaterialInPeriod + decMiscIssueFromDS2NotDSInPeriod;
											decItemCost = (decBeginCost + decAllocationAmount) / decPeriodQuantity;
										}
										catch{}
									}
									else
									{
										// MaterialCost(i) = ((BeginQty* MaterialUnitCost)+ UsedMaterial Amount
										// – RecycleAmount + AllocationAmount(i))/ (BeginQty + Total InputQty – DestroyQty)
										try
										{
											// 10-06-2006: thuypt said: sub-material will calculate same as overhead
											decPeriodQuantity = decBeginQuantity + decTotalInputInPeriod;// - decDestroyInPeriod;
											decItemCost = ((decBeginCost * decBeginQuantity) /*- decRecycleAmount*/
												+ decWORawAmount + decOutsideRawAmount + decAllocationAmount) / decPeriodQuantity;
											decComponentValue = (decWORawAmount + decOutsideRawAmount) / decTotalInputInPeriod;
										}
										catch{}
									}
									break;
								default: // other element type
									// IF BeginQty=0, Sum(ReturnGoodsReceipt.Qty+ RecycleMaterialDetail.Qty
									// + Miscellaneous.Qty with FromBin=DS & ToBin=not DS)<>0 
									// and Sum(OrtherInputQty in the period)=0
									if (decBeginQuantity == 0 && 
										(decRGRInPeriod + decRecoverableMaterialInPeriod + decMiscIssueFromDS2NotDSInPeriod) != 0 &&
										(decPOReceiptQuantityInPeriod + decWOCompleteInPeriod + decPositiveAdjustmentInPeriod) == 0)
									{
										// MaterialCost(i)= LastPeriodHasItemCost.MaterialCost(i) + AllocationAmount(i)/ 
										// Sum(ReturnGoodsReceipt.Qty+ RecycleMaterialDetail.Qty
										// + Miscellaneous.Qty with FromBin=DS & ToBin=not DS)
										try
										{
											decPeriodQuantity = decRGRInPeriod + decRecoverableMaterialInPeriod + decMiscIssueFromDS2NotDSInPeriod;
											decItemCost = (decBeginCost + decAllocationAmount) / decPeriodQuantity;
										}
										catch{}
									}
									else
									{
										// MaterialCost(i) = ((BeginQty* BeginUnitCost) – RecycleAmount
										// + WorkOrderAmount + OutsideAmount + AllocationAmount(i))/ (BeginQty + Total InputQty – DestroyQty)
										try
										{
											decPeriodQuantity = decBeginQuantity + decTotalInputInPeriod;// - decDestroyInPeriod;
											decItemCost = ((decBeginCost * decBeginQuantity) /*- decRecycleAmount*/
												+ decWORawAmount + decOutsideRawAmount + decAllocationAmount) / decPeriodQuantity;
											decComponentValue = (decWORawAmount + decOutsideRawAmount) / decTotalInputInPeriod;										}
										catch{}
									}
									break;
							}

							#endregion
						}
						else // none make item
						{
							#region Item actual cost

							// IF BeginQty=0, Sum(ReturnGoodsReceipt.Qty+ RecycleMaterialDetail.Qty
							// + Miscellaneous.Qty with FromBin=DS & ToBin=not DS)<>0 
							// and Sum(OrtherInputQty in the period)=0
							if (decBeginQuantity == 0 && 
								(decRGRInPeriod + decRecoverableMaterialInPeriod + decMiscIssueFromDS2NotDSInPeriod) != 0 &&
								(decPOReceiptQuantityInPeriod + decWOCompleteInPeriod + decPositiveAdjustmentInPeriod) == 0)
							{
								// MaterialCost(i)= LastPeriodHasItemCost.MaterialCost(i) + AllocationAmount(i)/ 
								// Sum(ReturnGoodsReceipt.Qty+ RecycleMaterialDetail.Qty
								// + Miscellaneous.Qty with FromBin=DS & ToBin=not DS)
								try
								{
									decPeriodQuantity = decRGRInPeriod + decRecoverableMaterialInPeriod + decMiscIssueFromDS2NotDSInPeriod;
									decItemCost = (decBeginCost + decAllocationAmount) / decPeriodQuantity;
								}
								catch{}
							}
							else
							{
								// MaterialCost(i) = ((BeginQty* MaterialUnitCost)+ UsedMaterial Amount– RecycleAmount + RecoverableAmount
								// + AllocationAmount(i))/ (BeginQty + Total InputQty – DestroyQty)
								try
								{
									decPeriodQuantity = decBeginQuantity + decTotalInputInPeriod;// - decDestroyInPeriod;
									decimal decUsedMaterialAmount = decPOReceiptAmount + decReceiveByInvoiceAmount
										+ decFreightAmount + decImportTaxAmount + decCreditAmount - decDebitAmount
										+ decWORawAmount + decOutsideRawAmount + decCostingAdjust;
									decItemCost = ((decBeginCost * decBeginQuantity) + decUsedMaterialAmount
										/*- decRecycleAmount + decRecoverableAmount */+ decAllocationAmount) / decPeriodQuantity;
								}
								catch{}
							}

							decTransactionAmount = decItemCost;
							
							#endregion
						}

						#region finally add to result table

						DataRow drowItemCost = pdtbActualCost.NewRow();
						drowItemCost[CST_ActualCostHistoryTable.ACTCOSTALLOCATIONMASTERID_FLD] = voPeriod.ActCostAllocationMasterID;
						drowItemCost[CST_ActualCostHistoryTable.ACTUALCOST_FLD] = decItemCost;
						drowItemCost[CST_ActualCostHistoryTable.COSTELEMENTID_FLD] = strElementID;
						drowItemCost[CST_ActualCostHistoryTable.PRODUCTID_FLD] = strProductID;
						drowItemCost[CST_ActualCostHistoryTable.STDCOST_FLD] = decSTDCost;
						drowItemCost[CST_ActualCostHistoryTable.QUANTITY_FLD] = decPeriodQuantity;
						// store begin quantity as thuypt request
						drowItemCost[CST_ActualCostHistoryTable.BEGIN_QUANTITY_FLD] = decBeginQuantity;
						// store component value as thuypt request
						drowItemCost[CST_ActualCostHistoryTable.COMPONENTVALUE_FLD] = decComponentValue;
						// store component ds amount as thuypt request
						drowItemCost[CST_ActualCostHistoryTable.COMPONENTDSAMOUNT_FLD] = decComponentScrapAmount;
						// component begin cost
						drowItemCost[CST_ActualCostHistoryTable.COMBEGINCOST_FLD] = decComBeginCost;
						// work order completion quantity
						drowItemCost[CST_ActualCostHistoryTable.WOCOMPLETIONQTY_FLD] = decWOCompleteInPeriod + decPOReceiptQuantityInPeriod;
						// Transaction Amount
						drowItemCost[CST_ActualCostHistoryTable.TRANSACTIONAMOUNT_FLD] = decTransactionAmount;
						// Recycle Amount
						drowItemCost[CST_ActualCostHistoryTable.RECYCLEAMOUNT_FLD] = decRecycleAmount;
						// Recoverabl Amount
						drowItemCost[CST_ActualCostHistoryTable.RECOVERABLEAMOUNT_FLD] = decRecoverableAmount;
						// Freight Amount
						drowItemCost[CST_ActualCostHistoryTable.FREIGHTAMOUNT_FLD] = decFreightAmount;
						// DSAmount= DestroyQty*ActualCost
						drowItemCost[CST_ActualCostHistoryTable.DSAMOUNT_FLD] = decDestroyInPeriod * decItemCost;
						// DS_OKAmount=(Qty in Miscellaneous with From Bin=DS & To Bin= not DS)* ActualCost
						drowItemCost[CST_ActualCostHistoryTable.DSOKAMOUNT_FLD] = decMiscIssueFromDS2NotDSInPeriod * decItemCost;
						// AdjustAmount= Sum(AdjustQty)* ActualCost 
						drowItemCost[CST_ActualCostHistoryTable.ADJUSTAMOUNT_FLD] = decInventoryAdjustQuantityInPeriod * decItemCost;
						// begin cost
						drowItemCost[CST_ActualCostHistoryTable.BEGINCOST_FLD] = decBeginCost;
						pdtbActualCost.Rows.Add(drowItemCost);

						#endregion
					} // end loop elements
				} // end check setup standard cost
			}
			return pdtbActualCost;
		}
		/// <summary>
		/// Determine Product is setup standard cost yet
		/// </summary>
		/// <param name="pstrProductID">Product</param>
		/// <param name=CST_STDItemCostTable.TABLE_NAME>Standard cost of all products</param>
		/// <returns>true: already setup. false if not yet setup</returns>
		private bool IsSetupSTDCost(string pstrProductID, DataTable dtbSTDCost)
		{
			return dtbSTDCost.Select(ITM_ProductTable.PRODUCTID_FLD + "='" + pstrProductID + "'").Length > 0;
		}

		private void btnChargeAllocation_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnChargeAllocation_Click()";
			try
			{
				#region validate data

				if (cboCCN.SelectedValue.Equals(null))
				{
					PCSMessageBox.Show(ErrorCode.MESSAGE_RGA_CCN, MessageBoxIcon.Warning);
					cboCCN.Focus();
					return;
				}
				if (voPeriod == null || voPeriod.ActCostAllocationMasterID <= 0)
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Warning);
					txtPeriodName.Focus();
					return;
				}
				// try to get period
				try
				{
					voPeriod = (CST_ActCostAllocationMasterVO)boActualCost.GetPeriod(voPeriod.ActCostAllocationMasterID);
					if (voPeriod.ActCostAllocationMasterID <= 0)
						throw new Exception();
				}
				catch
				{
					string[] strMessage = new string[1];
					strMessage[0] = lblPeriodName.Text;
					PCSMessageBox.Show(ErrorCode.MESSAGE_SELECTION_NOT_EXIST, MessageBoxIcon.Error, strMessage);
					// reset data
					txtPeriodName.Text = string.Empty;
					dtmFromDate.Value = DBNull.Value;
					dtmToDate.Value = DBNull.Value;
					btnRollUp.Enabled = false;
					btnDeleteCost.Enabled = false;
					btnChargeAllocation.Enabled = false;
					voPeriod = null;
					txtPeriodName.Focus();
					return;
				}

				#endregion

				SwitchMode(true);
				
				if (dtbErrorItem != null)
					dtbErrorItem = null;
				
				thrProcess = new Thread(new ThreadStart(ChargeAllocation));
				thrProcess.Start();
				if (thrProcess.ThreadState == ThreadState.Stopped || !thrProcess.IsAlive)
					thrProcess.Abort();
			}
			catch (ThreadAbortException ex)
			{
				Logger.LogMessage(ex, METHOD_NAME, Level.DEBUG);
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
		private void ChargeAllocation()
		{
			const string METHOD_NAME = THIS + ".ChargeAllocation()";
			try
			{
				this.Cursor = Cursors.WaitCursor;
				ChargeAllocation(Convert.ToInt32(cboCCN.SelectedValue), voPeriod);
				string[] strMsg = new string[]{lblChargeAllocation.Text};
				PCSMessageBox.Show(ErrorCode.MESSAGE_TASK_COMPLETED, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, strMsg);
				chkRollup.Checked = true;
				this.Cursor = Cursors.Default;
				SwitchMode(false);
			}
			catch (ThreadAbortException ex)
			{
				Logger.LogMessage(ex.Message, METHOD_NAME, Level.DEBUG);
			}
			catch (PCSException ex)
			{
				if (ex.mCode == ErrorCode.MESSAGE_CAN_NOT_DELETE)
				{
					string[] strMsg = new string[]{lblActualCost.Text};
					PCSMessageBox.Show(ErrorCode.MESSAGE_CAN_NOT_DELETE, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, strMsg);
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
			}
			catch (Exception ex)
			{
				string[] strMsg = new string[]{this.Text};
				PCSMessageBox.Show(ErrorCode.MESSAGE_CANNOT_ROLL_UP, MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, strMsg);
				try
				{
					Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
				}
				catch
				{
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION);
				}
			}
			finally
			{
				this.Cursor = Cursors.Default;
				SwitchMode(false);
			}
		}
		private void ChargeAllocation(int pintCCNID, object pobjCurrentPeriod)
		{
			#region Charge Allocation table schema
			DataTable dtbChargeAllocation = new DataTable(CST_DSAndRecycleAllocationTable.TABLE_NAME);
			dtbChargeAllocation.Columns.Add(new DataColumn(CST_DSAndRecycleAllocationTable.ACTCOSTALLOCATIONMASTERID_FLD, typeof(int)));
			dtbChargeAllocation.Columns.Add(new DataColumn(CST_DSAndRecycleAllocationTable.COSTELEMENTID_FLD, typeof(int)));
			dtbChargeAllocation.Columns.Add(new DataColumn(CST_DSAndRecycleAllocationTable.DSADALLOCACTIONID_FLD, typeof(int)));
			dtbChargeAllocation.Columns[CST_DSAndRecycleAllocationTable.DSADALLOCACTIONID_FLD].AllowDBNull = false;
			dtbChargeAllocation.Columns[CST_DSAndRecycleAllocationTable.DSADALLOCACTIONID_FLD].AutoIncrement = true;
			dtbChargeAllocation.Columns[CST_DSAndRecycleAllocationTable.DSADALLOCACTIONID_FLD].AutoIncrementSeed = 1;
			dtbChargeAllocation.Columns[CST_DSAndRecycleAllocationTable.DSADALLOCACTIONID_FLD].AutoIncrementStep = 1;
			dtbChargeAllocation.Columns.Add(new DataColumn(CST_DSAndRecycleAllocationTable.DSAMOUNT_FLD, typeof(decimal)));
			dtbChargeAllocation.Columns[CST_DSAndRecycleAllocationTable.DSAMOUNT_FLD].AllowDBNull = true;
			dtbChargeAllocation.Columns.Add(new DataColumn(CST_DSAndRecycleAllocationTable.DSRATE_FLD, typeof(decimal)));
			dtbChargeAllocation.Columns[CST_DSAndRecycleAllocationTable.DSRATE_FLD].AllowDBNull = true;
			dtbChargeAllocation.Columns.Add(new DataColumn(CST_DSAndRecycleAllocationTable.PRODUCTID_FLD, typeof(int)));
			dtbChargeAllocation.Columns.Add(new DataColumn(CST_DSAndRecycleAllocationTable.RECYCLEAMOUNT_FLD, typeof(decimal)));
			dtbChargeAllocation.Columns[CST_DSAndRecycleAllocationTable.RECYCLEAMOUNT_FLD].AllowDBNull = true;
			dtbChargeAllocation.Columns.Add(new DataColumn(CST_DSAndRecycleAllocationTable.RECYCLEAMOUNT_FLD, typeof(decimal)));
			dtbChargeAllocation.Columns[CST_DSAndRecycleAllocationTable.RECYCLEAMOUNT_FLD].AllowDBNull = true;
			dtbChargeAllocation.Columns.Add(new DataColumn(CST_DSAndRecycleAllocationTable.ADJUSTAMOUNT_FLD, typeof(decimal)));
			dtbChargeAllocation.Columns[CST_DSAndRecycleAllocationTable.ADJUSTAMOUNT_FLD].AllowDBNull = true;
			dtbChargeAllocation.Columns.Add(new DataColumn(CST_DSAndRecycleAllocationTable.RETURNGOODSRECEIPTQTY_FLD, typeof(decimal)));
			dtbChargeAllocation.Columns[CST_DSAndRecycleAllocationTable.RETURNGOODSRECEIPTQTY_FLD].AllowDBNull = true;
			dtbChargeAllocation.Columns.Add(new DataColumn(CST_DSAndRecycleAllocationTable.SHIPPINGQTY_FLD, typeof(decimal)));
			dtbChargeAllocation.Columns[CST_DSAndRecycleAllocationTable.SHIPPINGQTY_FLD].AllowDBNull = true;
			dtbChargeAllocation.Columns.Add(new DataColumn(CST_DSAndRecycleAllocationTable.OH_ADJUSTAMOUNT_FLD, typeof(decimal)));
			dtbChargeAllocation.Columns[CST_DSAndRecycleAllocationTable.OH_ADJUSTAMOUNT_FLD].AllowDBNull = true;
			dtbChargeAllocation.Columns.Add(new DataColumn(CST_DSAndRecycleAllocationTable.OH_DSAMOUNT_FLD, typeof(decimal)));
			dtbChargeAllocation.Columns[CST_DSAndRecycleAllocationTable.OH_DSAMOUNT_FLD].AllowDBNull = true;
			dtbChargeAllocation.Columns.Add(new DataColumn(CST_DSAndRecycleAllocationTable.OH_RECYCLEAMOUNT_FLD, typeof(decimal)));
			dtbChargeAllocation.Columns[CST_DSAndRecycleAllocationTable.OH_RECYCLEAMOUNT_FLD].AllowDBNull = true;
			dtbChargeAllocation.Columns.Add(new DataColumn(CST_DSAndRecycleAllocationTable.DSOHRATE_FLD, typeof(decimal)));
			dtbChargeAllocation.Columns[CST_DSAndRecycleAllocationTable.DSOHRATE_FLD].AllowDBNull = true;
			#endregion

			#region prepare data
			CST_ActCostAllocationMasterVO voPeriod = (CST_ActCostAllocationMasterVO)pobjCurrentPeriod;
			voPeriod.ToDate = new DateTime(voPeriod.ToDate.Year, voPeriod.ToDate.Month, voPeriod.ToDate.Day, 23,59,59);
			// shipping data
			DataTable dtbShipping = boActualCost.GetShipTransaction(pintCCNID, voPeriod.FromDate, voPeriod.ToDate);
			// return goods receive data
			DataTable dtbReturnGoods = boActualCost.GetReturnGoodsReceipt(pintCCNID, voPeriod.FromDate, voPeriod.ToDate);
			// actual cost data
			DataTable dtbActualCost = boActualCost.GetCostOfPeriod(voPeriod.ActCostAllocationMasterID);
			// list of cost element
			DataTable dtbElement = boActualCost.GetCostElements(pintCCNID);
			// list of category which have item sold in period
			DataTable dtbCategory = boActualCost.GetCategorySoldInPeriod(pintCCNID, voPeriod.FromDate, voPeriod.ToDate);
			DataTable dtbChargedAmount = new DataTable();
			dtbChargedAmount.Columns.Add(new DataColumn(ITM_CategoryTable.CATEGORYID_FLD, typeof(int)));
			dtbChargedAmount.Columns.Add(new DataColumn(STD_CostElementTable.COSTELEMENTID_FLD, typeof(int)));
			dtbChargedAmount.Columns.Add(new DataColumn(CST_ActualCostHistoryTable.DSAMOUNT_FLD, typeof(decimal)));
			dtbChargedAmount.Columns.Add(new DataColumn(CST_ActualCostHistoryTable.DSOKAMOUNT_FLD, typeof(decimal)));
			dtbChargedAmount.Columns.Add(new DataColumn(CST_ActualCostHistoryTable.ADJUSTAMOUNT_FLD, typeof(decimal)));
			dtbChargedAmount.Columns.Add(new DataColumn(CST_ActualCostHistoryTable.RECYCLEAMOUNT_FLD, typeof(decimal)));
			// list of make item sold in month
			DataTable dtbItems = boActualCost.GetItemSoldInPeriod(pintCCNID, voPeriod.FromDate, voPeriod.ToDate);
			
			#endregion

			#region allocating
			// allocating for each category
			foreach (DataRow drowCat in dtbCategory.Rows)
			{
				string strCatID = drowCat[ITM_CategoryTable.CATEGORYID_FLD].ToString();
				// get all product of current category
				DataRow[] drowItems = dtbItems.Select(ITM_CategoryTable.CATEGORYID_FLD + "=" + strCatID, ITM_ProductTable.PRODUCTID_FLD + " ASC");

				#region allocating

				foreach (DataRow drowElement in dtbElement.Rows)
				{
					string strElementID = drowElement[STD_CostElementTable.COSTELEMENTID_FLD].ToString();
					DataRow drowCharged = dtbChargedAmount.NewRow();
					drowCharged[ITM_CategoryTable.CATEGORYID_FLD] = strCatID;
					drowCharged[STD_CostElementTable.COSTELEMENTID_FLD] = strElementID;
					decimal decChargedDS = 0, decChargedRec = 0, decChargedAdjust = 0, decChargedDSOK = 0;

					#region calculate total for category

					#region sum ds amount, recycle amount, adjust amount for all item in category

					decimal decSumDSAMount = 0, decSumForDSRate = 0, decSumRecycleAmount = 0;
					decimal decSumDSOKAmount = 0, decSumAdjustAmount = 0;
					string strSumFilter = ITM_ProductTable.CATEGORYID_FLD + "=" + strCatID
						+ " AND " + STD_CostElementTable.COSTELEMENTID_FLD + "=" + strElementID;
					try
					{
						decSumDSAMount = Convert.ToDecimal(dtbActualCost.Compute("SUM("
							+ CST_ActualCostHistoryTable.DSAMOUNT_FLD + ")", strSumFilter));
					}
					catch{}
					try
					{
						decSumRecycleAmount = Convert.ToDecimal(dtbActualCost.Compute("SUM("
							+ CST_ActualCostHistoryTable.RECYCLEAMOUNT_FLD + ")", strSumFilter));
					}
					catch{}
					try
					{
						decSumDSOKAmount = Convert.ToDecimal(dtbActualCost.Compute("SUM("
							+ CST_ActualCostHistoryTable.DSOKAMOUNT_FLD + ")", strSumFilter));
					}
					catch{}
					try
					{
						decSumAdjustAmount = Convert.ToDecimal(dtbActualCost.Compute("SUM("
							+ CST_ActualCostHistoryTable.ADJUSTAMOUNT_FLD + ")", strSumFilter));
					}
					catch{}

					#endregion

					#region sum for ds rate

					foreach (DataRow drowItem in drowItems)
					{
						string strProductID = drowItem[ITM_ProductTable.PRODUCTID_FLD].ToString();
						string strFilter = ITM_ProductTable.PRODUCTID_FLD + "=" + strProductID
							+ " AND " + STD_CostElementTable.COSTELEMENTID_FLD + "=" + strElementID;

						#region ship & return quantity

						decimal decShipQuantity = 0, decReturnGoodsQuantity = 0;
						try
						{
							decShipQuantity = Convert.ToDecimal(dtbShipping.Compute("SUM(" + SO_ConfirmShipDetailTable.INVOICEQTY_FLD + ")",
								ITM_ProductTable.PRODUCTID_FLD + "=" + strProductID));
						}
						catch{}
						try
						{
							decReturnGoodsQuantity = Convert.ToDecimal(dtbReturnGoods.Compute("SUM(" + SO_ReturnedGoodsDetailTable.RECEIVEQUANTITY_FLD + ")",
								ITM_ProductTable.PRODUCTID_FLD + "=" + strProductID));
						}
						catch{}

						#endregion

						#region actual cost
						decimal decActualCost = 0;
						try
						{
							decActualCost = Convert.ToDecimal(dtbActualCost.Compute("SUM("
								+ CST_ActualCostHistoryTable.ACTUALCOST_FLD + ")", strFilter));
						}
						catch{}
						#endregion

						decSumForDSRate += (decShipQuantity - decReturnGoodsQuantity) * decActualCost;
					}

					#endregion

					foreach (DataRow drowItem in drowItems)
					{
						string strProductID = drowItem[ITM_ProductTable.PRODUCTID_FLD].ToString();
						string strFilter = ITM_ProductTable.PRODUCTID_FLD + "=" + strProductID
							+ " AND " + STD_CostElementTable.COSTELEMENTID_FLD + "=" + strElementID;
						#region quantity
						decimal decShipQuantity = 0, decReturnGoodsQuantity = 0;
						try
						{
							decShipQuantity = Convert.ToDecimal(dtbShipping.Compute("SUM(" + SO_ConfirmShipDetailTable.INVOICEQTY_FLD + ")",
								ITM_ProductTable.PRODUCTID_FLD + "=" + strProductID));
						}
						catch{}
						try
						{
							decReturnGoodsQuantity = Convert.ToDecimal(dtbReturnGoods.Compute("SUM(" + SO_ReturnedGoodsDetailTable.RECEIVEQUANTITY_FLD + ")",
								ITM_ProductTable.PRODUCTID_FLD + "=" + strProductID));
						}
						catch{}

						#endregion

						#region actual cost
						decimal decActualCost = 0;
						try
						{
							decActualCost = Convert.ToDecimal(dtbActualCost.Compute("SUM("
								+ CST_ActualCostHistoryTable.ACTUALCOST_FLD + ")", strFilter));
						}
						catch{}
						#endregion

						// make a new row in result table;
						DataRow drowResult = dtbChargeAllocation.NewRow();
						decimal decDSRate = 0, decDSAmount = 0, decRecycleAmount = 0, decAdjustAmount = 0;
						// ds rate = ((ship - return) * actual cost) / sum of ds rate in category
						try
						{
							decDSRate = ((decShipQuantity - decReturnGoodsQuantity) * decActualCost) * Convert.ToDecimal(Math.Pow(10,5)) / decSumForDSRate;
						}
						catch{}

						// ds amount = sum of ds amount * ds rate
						decDSAmount = decSumDSAMount * decDSRate;
						decChargedDS += decDSAmount/Convert.ToDecimal(Math.Pow(10,5));
						// recycle amount = sum of recycle amount * ds rate
						decRecycleAmount = (decSumRecycleAmount + decSumDSOKAmount) * decDSRate;
						decChargedRec += (decSumRecycleAmount * decDSRate)/Convert.ToDecimal(Math.Pow(10,5));
						decChargedDSOK += (decSumDSOKAmount * decDSRate)/Convert.ToDecimal(Math.Pow(10,5));
						// adjust amount = sum of adjust amount * ds rate
						decAdjustAmount = decSumAdjustAmount * decDSRate;
						decChargedAdjust += decAdjustAmount/Convert.ToDecimal(Math.Pow(10,5));
						
						#region insert to result table
						drowResult[CST_DSAndRecycleAllocationTable.ACTCOSTALLOCATIONMASTERID_FLD] = voPeriod.ActCostAllocationMasterID;
						drowResult[CST_DSAndRecycleAllocationTable.COSTELEMENTID_FLD] = strElementID;
						drowResult[CST_DSAndRecycleAllocationTable.DSAMOUNT_FLD] = decDSAmount/Convert.ToDecimal(Math.Pow(10,5));
						drowResult[CST_DSAndRecycleAllocationTable.DSRATE_FLD] = decDSRate/Convert.ToDecimal(Math.Pow(10,5));
						drowResult[CST_DSAndRecycleAllocationTable.PRODUCTID_FLD] = strProductID;
						drowResult[CST_DSAndRecycleAllocationTable.RECYCLEAMOUNT_FLD] = decRecycleAmount/Convert.ToDecimal(Math.Pow(10,5));
						drowResult[CST_DSAndRecycleAllocationTable.ADJUSTAMOUNT_FLD] = decAdjustAmount/Convert.ToDecimal(Math.Pow(10,5));
						drowResult[CST_DSAndRecycleAllocationTable.RETURNGOODSRECEIPTQTY_FLD] = decReturnGoodsQuantity;
						drowResult[CST_DSAndRecycleAllocationTable.SHIPPINGQTY_FLD] = decShipQuantity;
						dtbChargeAllocation.Rows.Add(drowResult);
						#endregion
					}

					drowCharged[CST_ActualCostHistoryTable.DSAMOUNT_FLD] = decChargedDS;
					drowCharged[CST_ActualCostHistoryTable.RECYCLEAMOUNT_FLD] = decChargedRec;
					drowCharged[CST_ActualCostHistoryTable.ADJUSTAMOUNT_FLD] = decChargedAdjust;
					drowCharged[CST_ActualCostHistoryTable.DSOKAMOUNT_FLD] = decChargedDSOK;
					dtbChargedAmount.Rows.Add(drowCharged); 

					#endregion
				} // end loop element

				#endregion
			} // end loop category
			#endregion

			#region allocating overhead amount

			foreach (DataRow drowElement in dtbElement.Rows)
			{
				string strElementID = drowElement[STD_CostElementTable.COSTELEMENTID_FLD].ToString();

				#region Calculate Amount has not been charged
				decimal decTotalDS = 0, decTotalAdjust = 0, decTotalDSOK = 0, decTotalRec = 0;
				decimal decChargedDS = 0, decChargedAdjust = 0, decChargedRec = 0, decChargedDSOK = 0;
				try
				{
					decTotalDS = Convert.ToDecimal(dtbActualCost.Compute("SUM(" + CST_ActualCostHistoryTable.DSAMOUNT_FLD + ")",
						CST_ActualCostHistoryTable.COSTELEMENTID_FLD + "=" + strElementID));
				}
				catch{}
				try
				{
					decChargedDS = Convert.ToDecimal(dtbChargedAmount.Compute("SUM(" + CST_ActualCostHistoryTable.DSAMOUNT_FLD + ")",
						CST_ActualCostHistoryTable.COSTELEMENTID_FLD + "=" + strElementID));
				}
				catch{}
				try
				{
					decTotalAdjust = Convert.ToDecimal(dtbActualCost.Compute("SUM(" + CST_ActualCostHistoryTable.ADJUSTAMOUNT_FLD + ")",
						CST_ActualCostHistoryTable.COSTELEMENTID_FLD + "=" + strElementID));
				}
				catch{}
				try
				{
					decChargedAdjust = Convert.ToDecimal(dtbChargedAmount.Compute("SUM(" + CST_ActualCostHistoryTable.ADJUSTAMOUNT_FLD + ")",
						CST_ActualCostHistoryTable.COSTELEMENTID_FLD + "=" + strElementID));
				}
				catch{}
				try
				{
					decTotalRec = Convert.ToDecimal(dtbActualCost.Compute("SUM(" + CST_ActualCostHistoryTable.RECYCLEAMOUNT_FLD + ")",
						CST_ActualCostHistoryTable.COSTELEMENTID_FLD + "=" + strElementID));
				}
				catch{}
				try
				{
					decChargedRec = Convert.ToDecimal(dtbChargedAmount.Compute("SUM(" + CST_ActualCostHistoryTable.RECYCLEAMOUNT_FLD + ")",
						CST_ActualCostHistoryTable.COSTELEMENTID_FLD + "=" + strElementID));
				}
				catch{}
				try
				{
					decTotalDSOK = Convert.ToDecimal(dtbActualCost.Compute("SUM(" + CST_ActualCostHistoryTable.DSOKAMOUNT_FLD + ")",
						CST_ActualCostHistoryTable.COSTELEMENTID_FLD + "=" + strElementID));
				}
				catch{}
				try
				{
					decChargedDSOK = Convert.ToDecimal(dtbChargedAmount.Compute("SUM(" + CST_ActualCostHistoryTable.DSOKAMOUNT_FLD + ")",
						CST_ActualCostHistoryTable.COSTELEMENTID_FLD + "=" + strElementID));
				}
				catch{}
				#endregion

				decimal decSumForRate = 0;

				#region Sum for overhead
				foreach (DataRow drowItem in dtbItems.Rows)
				{
					string strProductID = drowItem[ITM_ProductTable.PRODUCTID_FLD].ToString();
					string strFilter = ITM_ProductTable.PRODUCTID_FLD + "=" + strProductID
						+ " AND " + STD_CostElementTable.COSTELEMENTID_FLD + "=" + strElementID;

					#region ship & return quantity

					decimal decShipQuantity = 0, decReturnGoodsQuantity = 0;
					try
					{
						decShipQuantity = Convert.ToDecimal(dtbShipping.Compute("SUM(" + SO_ConfirmShipDetailTable.INVOICEQTY_FLD + ")",
						                                                        ITM_ProductTable.PRODUCTID_FLD + "=" + strProductID));
					}
					catch{}
					try
					{
						decReturnGoodsQuantity = Convert.ToDecimal(dtbReturnGoods.Compute("SUM(" + SO_ReturnedGoodsDetailTable.RECEIVEQUANTITY_FLD + ")",
						                                                                  ITM_ProductTable.PRODUCTID_FLD + "=" + strProductID));
					}
					catch{}

					#endregion

					#region actual cost
					decimal decActualCost = 0;
					try
					{
						decActualCost = Convert.ToDecimal(dtbActualCost.Compute("SUM("
							+ CST_ActualCostHistoryTable.ACTUALCOST_FLD + ")", strFilter));
					}
					catch{}
					#endregion

					decSumForRate += (decShipQuantity - decReturnGoodsQuantity) * decActualCost;
				}
				#endregion

				#region Charge overhead
				foreach (DataRow drowData in dtbItems.Rows)
				{
					string strProductID = drowData[ITM_ProductTable.PRODUCTID_FLD].ToString();
					string strFilter = ITM_ProductTable.PRODUCTID_FLD + "=" + strProductID
						+ " AND " + STD_CostElementTable.COSTELEMENTID_FLD + "=" + strElementID;

					#region ship & return quantity

					decimal decShipQuantity = 0, decReturnGoodsQuantity = 0;
					try
					{
						decShipQuantity = Convert.ToDecimal(dtbShipping.Compute("SUM(" + SO_ConfirmShipDetailTable.INVOICEQTY_FLD + ")",
						                                                        ITM_ProductTable.PRODUCTID_FLD + "=" + strProductID));
					}
					catch{}
					try
					{
						decReturnGoodsQuantity = Convert.ToDecimal(dtbReturnGoods.Compute("SUM(" + SO_ReturnedGoodsDetailTable.RECEIVEQUANTITY_FLD + ")",
						                                                                  ITM_ProductTable.PRODUCTID_FLD + "=" + strProductID));
					}
					catch{}

					#endregion

					#region actual cost
					decimal decActualCost = 0;
					try
					{
						decActualCost = Convert.ToDecimal(dtbActualCost.Compute("SUM("
							+ CST_ActualCostHistoryTable.ACTUALCOST_FLD + ")", strFilter));
					}
					catch{}
					#endregion

					decimal decRate = 0;
					try
					{
						decRate = ((decShipQuantity - decReturnGoodsQuantity) * decActualCost) / decSumForRate;
						decRate = decRate * Convert.ToDecimal(Math.Pow(10,5));
					}
					catch{}
					DataRow[] drowExisted = IsCharged(strProductID, strElementID, voPeriod.ActCostAllocationMasterID.ToString(), dtbChargeAllocation);
					if (drowExisted.Length > 0)
					{
						DataRow drowCharged = drowExisted[0];
						drowCharged[CST_DSAndRecycleAllocationTable.OH_ADJUSTAMOUNT_FLD] = decRate * (decTotalAdjust - decChargedAdjust)/Convert.ToDecimal(Math.Pow(10,5));
						drowCharged[CST_DSAndRecycleAllocationTable.OH_DSAMOUNT_FLD] = decRate * (decTotalDS - decChargedDS)/Convert.ToDecimal(Math.Pow(10,5));
						drowCharged[CST_DSAndRecycleAllocationTable.OH_RECYCLEAMOUNT_FLD] = decRate * (((decTotalDSOK - decChargedDSOK) + (decTotalRec - decChargedRec)))/Convert.ToDecimal(Math.Pow(10,5));
						drowCharged[CST_DSAndRecycleAllocationTable.DSOHRATE_FLD] = decRate / Convert.ToDecimal(Math.Pow(10,5));
					}
					else
					{
						DataRow drowResult = dtbChargeAllocation.NewRow();
						drowResult[CST_DSAndRecycleAllocationTable.ACTCOSTALLOCATIONMASTERID_FLD] = voPeriod.ActCostAllocationMasterID;
						drowResult[CST_DSAndRecycleAllocationTable.COSTELEMENTID_FLD] = strElementID;
						drowResult[CST_DSAndRecycleAllocationTable.PRODUCTID_FLD] = strProductID;
						drowResult[CST_DSAndRecycleAllocationTable.OH_ADJUSTAMOUNT_FLD] = decRate * (decTotalAdjust - decChargedAdjust)/Convert.ToDecimal(Math.Pow(10,5));
						drowResult[CST_DSAndRecycleAllocationTable.OH_DSAMOUNT_FLD] = decRate * (decTotalDS - decChargedDS)/Convert.ToDecimal(Math.Pow(10,5));
						drowResult[CST_DSAndRecycleAllocationTable.OH_RECYCLEAMOUNT_FLD] = decRate * (((decTotalDSOK - decChargedDSOK) + (decTotalRec - decChargedRec)))/Convert.ToDecimal(Math.Pow(10,5));
						drowResult[CST_DSAndRecycleAllocationTable.DSOHRATE_FLD] = decRate / Convert.ToDecimal(Math.Pow(10,5));
						dtbChargeAllocation.Rows.Add(drowResult);
					}
				}
				#endregion
			}

			#endregion

			// update data to database
			DataSet dstData = new DataSet();
			dstData.Tables.Add(dtbChargeAllocation);
			boActualCost.ChargeAllocation(dstData, pobjCurrentPeriod);
		}
		private DataRow[] IsCharged(string pstrProductID, string pstrCostElementID, string pstrPeriodID, DataTable pdtbCharged)
		{
			return pdtbCharged.Select(ITM_ProductTable.PRODUCTID_FLD + "=" + pstrProductID
				+ " AND " + CST_ActualCostHistoryTable.COSTELEMENTID_FLD + "=" + pstrCostElementID
				+ " AND " + CST_ActualCostHistoryTable.ACTCOSTALLOCATIONMASTERID_FLD + "=" + pstrPeriodID);
		}
	}
}
