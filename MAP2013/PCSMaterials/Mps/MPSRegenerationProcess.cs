using System;
using System.ComponentModel;
using System.Data;
using System.Collections;
//using System.Globalization;
using System.Threading;
using System.Windows.Forms;
using PCSComProduct.Items.DS;
//using PCSComProduction.DCP.BO;
using PCSComUtils.Common;
using PCSComUtils.Common.BO;
using PCSComUtils.PCSExc;
using PCSUtils.Log;
using PCSUtils.Utils;
using PCSComMaterials.Plan.BO;
using PCSComMaterials.Plan.DS;
using C1.Win.C1List;

namespace PCSMaterials.Mps
{
	/// <summary>
	/// Summary description for MPSRegenerationProcess.
	/// </summary>
	public class MPSRegenerationProcess : System.Windows.Forms.Form
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		private const string THIS = "PCSMaterials.Mps.MPSRegenerationProcess";
		private UtilsBO boUtils;
		Thread thrProcess = null;

		#region Members
		
		private MTR_MPSCycleOptionMasterVO voMPSCycleOptionMaster;
		private MPSRegenerationProcessBO boMPSRegeneration = null;
		private MPSCycleOptionBO boCycleOption = null;
		//const string MPS_CYCLE = "MPS Cycle";
		private const string DAY_TO_ADD_COL = "DayToAdd";
		private const string IS_SUPPLIED_COL = "IsSupplied";
		private const int DECIMAL_ROUND_NUMBER = 10;
		DateTime dtmCurrentDate = DateTime.Now;
		int intMPSCycleOtopnMasterID = 0;
		private System.Windows.Forms.Button btnCycle;
		private System.Windows.Forms.TextBox txtCycle;
		private C1.Win.C1List.C1Combo cboCCN;
		private System.Windows.Forms.Button btnClose;
		private System.Windows.Forms.Button btnHelp;
		private System.Windows.Forms.Label lblCycle;
		private System.Windows.Forms.Label lblCCN;
		private System.Windows.Forms.Button btnPreview;
		private System.Windows.Forms.Label lblProcessing;
		private System.Windows.Forms.PictureBox picProcessing;
		private System.Windows.Forms.Button btnProcess;
		#endregion

		public MPSRegenerationProcess()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
		}

		/// <summary>
		/// Initialize form with a specified MPS Cycle Option Master
		/// </summary>
		/// <param name="pintMPSCycleOptionMasterID">MPS Cycle Option Master</param>
		public MPSRegenerationProcess(int pintMPSCycleOptionMasterID)
		{
			InitializeComponent();
			intMPSCycleOtopnMasterID = pintMPSCycleOptionMasterID;
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(MPSRegenerationProcess));
			this.btnCycle = new System.Windows.Forms.Button();
			this.txtCycle = new System.Windows.Forms.TextBox();
			this.cboCCN = new C1.Win.C1List.C1Combo();
			this.btnClose = new System.Windows.Forms.Button();
			this.btnHelp = new System.Windows.Forms.Button();
			this.lblCycle = new System.Windows.Forms.Label();
			this.lblCCN = new System.Windows.Forms.Label();
			this.btnPreview = new System.Windows.Forms.Button();
			this.btnProcess = new System.Windows.Forms.Button();
			this.lblProcessing = new System.Windows.Forms.Label();
			this.picProcessing = new System.Windows.Forms.PictureBox();
			((System.ComponentModel.ISupportInitialize)(this.cboCCN)).BeginInit();
			this.SuspendLayout();
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
			this.txtCycle.Leave += new System.EventHandler(this.txtCycle_Leave);
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
				" 118, 158</ClientRect><VScrollBar><Width>15</Width></VScrollBar><HScrollBar><Hei" +
				"ght>15</Height></HScrollBar><CaptionStyle parent=\"Style2\" me=\"Style9\" /><EvenRow" +
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
			// lblCycle
			// 
			this.lblCycle.AccessibleDescription = resources.GetString("lblCycle.AccessibleDescription");
			this.lblCycle.AccessibleName = resources.GetString("lblCycle.AccessibleName");
			this.lblCycle.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblCycle.Anchor")));
			this.lblCycle.AutoSize = ((bool)(resources.GetObject("lblCycle.AutoSize")));
			this.lblCycle.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblCycle.Dock")));
			this.lblCycle.Enabled = ((bool)(resources.GetObject("lblCycle.Enabled")));
			this.lblCycle.Font = ((System.Drawing.Font)(resources.GetObject("lblCycle.Font")));
			this.lblCycle.ForeColor = System.Drawing.Color.Maroon;
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
			// btnPreview
			// 
			this.btnPreview.AccessibleDescription = resources.GetString("btnPreview.AccessibleDescription");
			this.btnPreview.AccessibleName = resources.GetString("btnPreview.AccessibleName");
			this.btnPreview.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnPreview.Anchor")));
			this.btnPreview.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnPreview.BackgroundImage")));
			this.btnPreview.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnPreview.Dock")));
			this.btnPreview.Enabled = ((bool)(resources.GetObject("btnPreview.Enabled")));
			this.btnPreview.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnPreview.FlatStyle")));
			this.btnPreview.Font = ((System.Drawing.Font)(resources.GetObject("btnPreview.Font")));
			this.btnPreview.Image = ((System.Drawing.Image)(resources.GetObject("btnPreview.Image")));
			this.btnPreview.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnPreview.ImageAlign")));
			this.btnPreview.ImageIndex = ((int)(resources.GetObject("btnPreview.ImageIndex")));
			this.btnPreview.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnPreview.ImeMode")));
			this.btnPreview.Location = ((System.Drawing.Point)(resources.GetObject("btnPreview.Location")));
			this.btnPreview.Name = "btnPreview";
			this.btnPreview.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnPreview.RightToLeft")));
			this.btnPreview.Size = ((System.Drawing.Size)(resources.GetObject("btnPreview.Size")));
			this.btnPreview.TabIndex = ((int)(resources.GetObject("btnPreview.TabIndex")));
			this.btnPreview.Text = resources.GetString("btnPreview.Text");
			this.btnPreview.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnPreview.TextAlign")));
			this.btnPreview.Visible = ((bool)(resources.GetObject("btnPreview.Visible")));
			this.btnPreview.Click += new System.EventHandler(this.btnPreview_Click);
			// 
			// btnProcess
			// 
			this.btnProcess.AccessibleDescription = resources.GetString("btnProcess.AccessibleDescription");
			this.btnProcess.AccessibleName = resources.GetString("btnProcess.AccessibleName");
			this.btnProcess.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnProcess.Anchor")));
			this.btnProcess.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnProcess.BackgroundImage")));
			this.btnProcess.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnProcess.Dock")));
			this.btnProcess.Enabled = ((bool)(resources.GetObject("btnProcess.Enabled")));
			this.btnProcess.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnProcess.FlatStyle")));
			this.btnProcess.Font = ((System.Drawing.Font)(resources.GetObject("btnProcess.Font")));
			this.btnProcess.Image = ((System.Drawing.Image)(resources.GetObject("btnProcess.Image")));
			this.btnProcess.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnProcess.ImageAlign")));
			this.btnProcess.ImageIndex = ((int)(resources.GetObject("btnProcess.ImageIndex")));
			this.btnProcess.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnProcess.ImeMode")));
			this.btnProcess.Location = ((System.Drawing.Point)(resources.GetObject("btnProcess.Location")));
			this.btnProcess.Name = "btnProcess";
			this.btnProcess.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnProcess.RightToLeft")));
			this.btnProcess.Size = ((System.Drawing.Size)(resources.GetObject("btnProcess.Size")));
			this.btnProcess.TabIndex = ((int)(resources.GetObject("btnProcess.TabIndex")));
			this.btnProcess.Text = resources.GetString("btnProcess.Text");
			this.btnProcess.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnProcess.TextAlign")));
			this.btnProcess.Visible = ((bool)(resources.GetObject("btnProcess.Visible")));
			this.btnProcess.Click += new System.EventHandler(this.btnProcess_Click);
			// 
			// lblProcessing
			// 
			this.lblProcessing.AccessibleDescription = resources.GetString("lblProcessing.AccessibleDescription");
			this.lblProcessing.AccessibleName = resources.GetString("lblProcessing.AccessibleName");
			this.lblProcessing.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblProcessing.Anchor")));
			this.lblProcessing.AutoSize = ((bool)(resources.GetObject("lblProcessing.AutoSize")));
			this.lblProcessing.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblProcessing.Dock")));
			this.lblProcessing.Enabled = ((bool)(resources.GetObject("lblProcessing.Enabled")));
			this.lblProcessing.Font = ((System.Drawing.Font)(resources.GetObject("lblProcessing.Font")));
			this.lblProcessing.Image = ((System.Drawing.Image)(resources.GetObject("lblProcessing.Image")));
			this.lblProcessing.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblProcessing.ImageAlign")));
			this.lblProcessing.ImageIndex = ((int)(resources.GetObject("lblProcessing.ImageIndex")));
			this.lblProcessing.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblProcessing.ImeMode")));
			this.lblProcessing.Location = ((System.Drawing.Point)(resources.GetObject("lblProcessing.Location")));
			this.lblProcessing.Name = "lblProcessing";
			this.lblProcessing.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblProcessing.RightToLeft")));
			this.lblProcessing.Size = ((System.Drawing.Size)(resources.GetObject("lblProcessing.Size")));
			this.lblProcessing.TabIndex = ((int)(resources.GetObject("lblProcessing.TabIndex")));
			this.lblProcessing.Text = resources.GetString("lblProcessing.Text");
			this.lblProcessing.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblProcessing.TextAlign")));
			this.lblProcessing.Visible = ((bool)(resources.GetObject("lblProcessing.Visible")));
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
			// MPSRegenerationProcess
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
			this.Controls.Add(this.lblProcessing);
			this.Controls.Add(this.picProcessing);
			this.Controls.Add(this.btnCycle);
			this.Controls.Add(this.txtCycle);
			this.Controls.Add(this.cboCCN);
			this.Controls.Add(this.btnClose);
			this.Controls.Add(this.btnHelp);
			this.Controls.Add(this.lblCycle);
			this.Controls.Add(this.lblCCN);
			this.Controls.Add(this.btnPreview);
			this.Controls.Add(this.btnProcess);
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
			this.Name = "MPSRegenerationProcess";
			this.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("$this.RightToLeft")));
			this.StartPosition = ((System.Windows.Forms.FormStartPosition)(resources.GetObject("$this.StartPosition")));
			this.Text = resources.GetString("$this.Text");
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MPSRegenerationProcess_KeyDown);
			this.Closing += new System.ComponentModel.CancelEventHandler(this.MPSRegenerationProcess_Closing);
			this.Load += new System.EventHandler(this.MPSRegenerationProcess_Load);
			((System.ComponentModel.ISupportInitialize)(this.cboCCN)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// Close the form
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnClose_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		/// <summary>
		/// First check user permission to this form. Then load CCN and set default value
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MPSRegenerationProcess_Load(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".MPSRegenerationProcess_Load()";
			try
			{
				#region check user permission

				Security objSecurity = new Security();
				this.Name = THIS;
				if (objSecurity.SetRightForUserOnForm(this, SystemProperty.UserName) == 0)
				{
					this.Close();
					return;
				}

				#endregion

				// initialize variable
				boUtils = new UtilsBO();
				boMPSRegeneration = new MPSRegenerationProcessBO();
				boCycleOption = new MPSCycleOptionBO();
				lblProcessing.Visible = false;
				picProcessing.Visible = false;
				// - Load CCN and set default value
				FormControlComponents.PutDataIntoC1ComboBox(cboCCN, boUtils.ListCCN().Tables[0], MST_CCNTable.CODE_FLD, MST_CCNTable.CCNID_FLD, MST_CCNTable.TABLE_NAME);
				cboCCN.SelectedValue = SystemProperty.CCNID;
				if (intMPSCycleOtopnMasterID > 0)
				{
					voMPSCycleOptionMaster = (MTR_MPSCycleOptionMasterVO)boCycleOption.GetCycleOptionMaster(intMPSCycleOtopnMasterID);
					txtCycle.Text = voMPSCycleOptionMaster.Cycle;
					btnPreview.Enabled = false;
					btnPreview.Focus();
				}
				else
				{
					voMPSCycleOptionMaster = new MTR_MPSCycleOptionMasterVO();
					btnPreview.Enabled = false;
					cboCCN.Focus();
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
		/// Preview the selected cycle option
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnPreview_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnPreview_Click()";
			try
			{
				if (!voMPSCycleOptionMaster.Equals(null) && voMPSCycleOptionMaster.MPSCycleOptionMasterID > 0)
				{
					// open MPSCycleOption form to show the selected Cycle infomation
					MPSCycleOption frmMPSCycleOption = new MPSCycleOption(voMPSCycleOptionMaster.MPSCycleOptionMasterID);
					frmMPSCycleOption.Show();
				}
				else
				{
					// display error message force user to select Cycle first
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
		/// Open search form allows user to select a MPS cycle option
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnCycle_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnCycle_Click()";
			try
			{
				DataRowView drwResult = null;
				Hashtable htbCondition = new Hashtable();
				if (cboCCN.SelectedValue != null)
					htbCondition.Add(MST_MasterLocationTable.CCNID_FLD, cboCCN.SelectedValue.ToString());
				else
				{
					PCSMessageBox.Show(ErrorCode.MESSAGE_RGA_CCN, MessageBoxIcon.Warning);
					cboCCN.Focus();
					return;
				}
				if (sender is TextBox && sender != null)
					drwResult = FormControlComponents.OpenSearchForm(MTR_MPSCycleOptionMasterTable.TABLE_NAME, MTR_MPSCycleOptionMasterTable.CYCLE_FLD, txtCycle.Text, htbCondition, false);
				else
					drwResult = FormControlComponents.OpenSearchForm(MTR_MPSCycleOptionMasterTable.TABLE_NAME, MTR_MPSCycleOptionMasterTable.CYCLE_FLD, txtCycle.Text, htbCondition, true);
				if (drwResult != null)
				{
					txtCycle.Text = drwResult[MTR_MPSCycleOptionMasterTable.CYCLE_FLD].ToString();
					voMPSCycleOptionMaster = (MTR_MPSCycleOptionMasterVO)boCycleOption.GetCycleOptionMaster(int.Parse(drwResult[MTR_MPSCycleOptionMasterTable.MPSCYCLEOPTIONMASTERID_FLD].ToString()));
					btnPreview.Enabled = true;
				}
				else
				{
					txtCycle.Focus();
					txtCycle.SelectAll();
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
		/// Run MPS process
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnProcess_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnProcess_Click()";
			try
			{
				if (cboCCN.SelectedValue.Equals(null))
				{
					PCSMessageBox.Show(ErrorCode.MESSAGE_RGA_CCN, MessageBoxIcon.Warning);
					cboCCN.Focus();
					return;
				}
				if (voMPSCycleOptionMaster == null || voMPSCycleOptionMaster.MPSCycleOptionMasterID <= 0)
				{
					//MessageBox.Show("You must select MPS Cycle Option before process MPS");
					PCSMessageBox.Show(ErrorCode.MESSAGE_MPS_MUST_SELECT_CYCLE, MessageBoxIcon.Warning);
                    txtCycle.Focus();
					return;
				}
				// try to get cycle
				try
				{
					voMPSCycleOptionMaster = (MTR_MPSCycleOptionMasterVO)boCycleOption.GetCycleOptionMaster(voMPSCycleOptionMaster.MPSCycleOptionMasterID);
					if (voMPSCycleOptionMaster.MPSCycleOptionMasterID <= 0)
						throw new Exception();
				}
				catch
				{
					string[] strMessage = new string[1];
					strMessage[0] = lblCycle.Text;
					//MessageBox.Show("The selected MPS Cycle is not exists in system. Please select another one");
					PCSMessageBox.Show(ErrorCode.MESSAGE_SELECTION_NOT_EXIST, MessageBoxIcon.Error, strMessage);
					// reset data
					txtCycle.Text = string.Empty;
					voMPSCycleOptionMaster = null;
					txtCycle.Focus();
					return;
				}
				// get current date from database server
				dtmCurrentDate = boUtils.GetDBDate();
				DateTime dtmAsOfDate = new DateTime(voMPSCycleOptionMaster.AsOfDate.Year, voMPSCycleOptionMaster.AsOfDate.Month, voMPSCycleOptionMaster.AsOfDate.Day);
				dtmCurrentDate = new DateTime(dtmCurrentDate.Year, dtmCurrentDate.Month, dtmCurrentDate.Day);
				if (dtmAsOfDate < dtmCurrentDate)
				{
					PCSMessageBox.Show(ErrorCode.MESSAGE_DCP_ASOFDATE_MUST_IN_FUTURE, MessageBoxIcon.Error);
					txtCycle.Focus();
					return;
				}
				
				lblCCN.Visible = false;
				cboCCN.Visible = false;
				lblCycle.Visible = false;
				txtCycle.Visible = false;
				btnCycle.Visible = false;
				btnPreview.Enabled = false;
				btnProcess.Enabled = false;
				lblProcessing.Visible = true;
				lblProcessing.Text = string.Format("{0} - {1}", lblProcessing.Text, this.Text);
				picProcessing.Visible = true;
				
				thrProcess = new Thread(new ThreadStart(ExecuteProcess));
				thrProcess.Start();
				if (thrProcess.ThreadState == ThreadState.Stopped || !thrProcess.IsAlive)
				{
					thrProcess = null;
				}
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
		/// Button Help pressed
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnHelp_Click(object sender, System.EventArgs e)
		{
			
		}
		/// <summary>
		/// When user change CCN, need to clear any MPS Cycle Option data on form
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void cboCCN_SelectedValueChanged(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".cboCCN_SelectedValueChanged()";
			try
			{
				voMPSCycleOptionMaster = new MTR_MPSCycleOptionMasterVO();
				txtCycle.Text = string.Empty;
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
		/// Form is closed.
		/// Stop thread if it is active.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MPSRegenerationProcess_Closing(object sender, CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".MPSRegenerationProcess_Closed()";
			try
			{
				// ask user to stop the thread
				if (thrProcess != null)
				{
					if (thrProcess.IsAlive || thrProcess.ThreadState == ThreadState.Running)
					{
						string[] strMsg = {this.Text};					
						DialogResult dlgResult = PCSMessageBox.Show(ErrorCode.MESSAGE_PROCESS_IS_RUNNING, MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2, strMsg);
						//DialogResult dlgResult = MessageBox.Show("Running. Close it?", "Close", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
						switch (dlgResult)
						{
							case System.Windows.Forms.DialogResult.OK:
								// try to stop the thread
								try
								{
									thrProcess.Abort();
								}
								catch (ThreadAbortException ex)
								{
									Logger.LogMessage(ex.Message, METHOD_NAME, Level.DEBUG);
									e.Cancel = false;
								}
								break;
							case System.Windows.Forms.DialogResult.Cancel:
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
		/// txtCycle lost focus
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void txtCycle_Leave(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtCycle_Leave()";
			try
			{
				if (txtCycle.Modified)
				{
					if (txtCycle.Text != string.Empty)
						btnCycle_Click(sender, e);
					else
					{
						voMPSCycleOptionMaster = new MTR_MPSCycleOptionMasterVO();
						btnPreview.Enabled = false;
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

		/// <summary>
		/// Focus on txtCycle
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void txtCycle_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtCycle_KeyDown()";
			try
			{
				if (e.KeyCode == Keys.F4)
					btnCycle_Click(null, null);
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

		private void MPSRegenerationProcess_KeyDown(object sender, KeyEventArgs e)
		{
			const string METHOD_NAME = THIS + ".MPSRegenerationProcess_KeyDown()";
			try
			{
				if (e.KeyCode == Keys.Escape)
					this.Close();
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

		#region Regenerate CPO in client
		/// <summary>
		/// 1. Retrieve all required data from database
		/// 2. Generate CPO based on demand and supply in client
		/// 3. Save all generated CPOs to database via BO class
		/// 4. Update MPSCycleOptionMaster generation date
		/// </summary>
		private void ExecuteProcess()
		{
			const string METHOD_NAME = THIS + ".ExecuteProcess()";
			//const string MPS_PROCESS = "MPS";
			const int DECIMAL_ROUND_NUMBER = 2;
			try
			{
				// checking workday calendar
				if (!boMPSRegeneration.CheckCalendarConfig(voMPSCycleOptionMaster))
					throw new PCSBOException(ErrorCode.MESSAGE_DCP_CONFIG_CALENDAR_FROM_X_TO_Y, METHOD_NAME, null);
				// get the cycle detail information
				DataTable dtbMPSCycleOptionDetail = boMPSRegeneration.GetCycleDetail(voMPSCycleOptionMaster.MPSCycleOptionMasterID);

				// due date of the cycle
				DateTime dtmDueDate = voMPSCycleOptionMaster.AsOfDate.AddDays(voMPSCycleOptionMaster.PlanHorizon);
				dtmDueDate = new DateTime(dtmDueDate.Year, dtmDueDate.Month, dtmDueDate.Day, 23, 59, 59);
				// get all product will be use in MPS
				DataTable dtbProducts = boMPSRegeneration.GetAllProducts(voMPSCycleOptionMaster.CCNID);

				#region create CPO table with schema only

				DataTable dtbCPOs = new DataTable(MTR_CPOTable.TABLE_NAME);
				dtbCPOs.Columns.Add(new DataColumn(MTR_CPOTable.CPOID_FLD, typeof (long)));
				dtbCPOs.Columns[MTR_CPOTable.CPOID_FLD].AllowDBNull = false;
				dtbCPOs.Columns[MTR_CPOTable.CPOID_FLD].AutoIncrement = true;
				dtbCPOs.Columns[MTR_CPOTable.CPOID_FLD].AutoIncrementSeed = 1;
				dtbCPOs.Columns[MTR_CPOTable.CPOID_FLD].AutoIncrementStep = 1;
				dtbCPOs.Columns.Add(new DataColumn(MTR_CPOTable.QUANTITY_FLD, typeof (decimal)));
				dtbCPOs.Columns[MTR_CPOTable.QUANTITY_FLD].AllowDBNull = true;
				//dtbCPOs.Columns[MTR_CPOTable.QUANTITY_FLD].MaxLength = int.MaxValue;
				dtbCPOs.Columns.Add(new DataColumn(MTR_CPOTable.STARTDATE_FLD, typeof (DateTime)));
				dtbCPOs.Columns[MTR_CPOTable.STARTDATE_FLD].AllowDBNull = true;
				dtbCPOs.Columns.Add(new DataColumn(MTR_CPOTable.DUEDATE_FLD, typeof (DateTime)));
				dtbCPOs.Columns[MTR_CPOTable.DUEDATE_FLD].AllowDBNull = true;
				dtbCPOs.Columns.Add(new DataColumn(MTR_CPOTable.REFMASTERID_FLD, typeof (int)));
				dtbCPOs.Columns[MTR_CPOTable.REFMASTERID_FLD].AllowDBNull = true;
				dtbCPOs.Columns.Add(new DataColumn(MTR_CPOTable.REFDETAILID_FLD, typeof (int)));
				dtbCPOs.Columns[MTR_CPOTable.REFDETAILID_FLD].AllowDBNull = true;
				dtbCPOs.Columns.Add(new DataColumn(MTR_CPOTable.REFTYPE_FLD, typeof (int)));
				dtbCPOs.Columns[MTR_CPOTable.REFTYPE_FLD].AllowDBNull = true;
				dtbCPOs.Columns.Add(new DataColumn(MTR_CPOTable.NETAVAILABLEQUANTITY_FLD, typeof (decimal)));
				dtbCPOs.Columns[MTR_CPOTable.NETAVAILABLEQUANTITY_FLD].AllowDBNull = true;
				//dtbCPOs.Columns[MTR_CPOTable.NETAVAILABLEQUANTITY_FLD].MaxLength = int.MaxValue;
				dtbCPOs.Columns[MTR_CPOTable.NETAVAILABLEQUANTITY_FLD].DefaultValue = decimal.Zero;
				dtbCPOs.Columns.Add(new DataColumn(MTR_CPOTable.CCNID_FLD, typeof (int)));
				dtbCPOs.Columns[MTR_CPOTable.CCNID_FLD].AllowDBNull = false;
				dtbCPOs.Columns[MTR_CPOTable.CCNID_FLD].DefaultValue = voMPSCycleOptionMaster.CCNID;
				dtbCPOs.Columns.Add(new DataColumn(MTR_CPOTable.PRODUCTID_FLD, typeof (int)));
				dtbCPOs.Columns[MTR_CPOTable.PRODUCTID_FLD].AllowDBNull = false;
				dtbCPOs.Columns.Add(new DataColumn(MTR_CPOTable.MASTERLOCATIONID_FLD, typeof (int)));
				dtbCPOs.Columns[MTR_CPOTable.MASTERLOCATIONID_FLD].AllowDBNull = false;
				dtbCPOs.Columns.Add(new DataColumn(MTR_CPOTable.STOCKUMID_FLD, typeof (int)));
				dtbCPOs.Columns[MTR_CPOTable.STOCKUMID_FLD].AllowDBNull = true;
				dtbCPOs.Columns.Add(new DataColumn(MTR_CPOTable.ISMPS_FLD, typeof (bool)));
				dtbCPOs.Columns[MTR_CPOTable.ISMPS_FLD].AllowDBNull = false;
				dtbCPOs.Columns[MTR_CPOTable.ISMPS_FLD].DefaultValue = true;
				dtbCPOs.Columns.Add(new DataColumn(MTR_CPOTable.CONVERTED_FLD, typeof (bool)));
				dtbCPOs.Columns[MTR_CPOTable.CONVERTED_FLD].AllowDBNull = false;
				dtbCPOs.Columns[MTR_CPOTable.CONVERTED_FLD].DefaultValue = false;
				dtbCPOs.Columns.Add(new DataColumn(MTR_CPOTable.POGENERATEDID_FLD, typeof (int)));
				dtbCPOs.Columns[MTR_CPOTable.POGENERATEDID_FLD].AllowDBNull = true;
				dtbCPOs.Columns.Add(new DataColumn(MTR_CPOTable.WOGENERATEDID_FLD, typeof (int)));
				dtbCPOs.Columns[MTR_CPOTable.WOGENERATEDID_FLD].AllowDBNull = true;
				dtbCPOs.Columns.Add(new DataColumn(MTR_CPOTable.MRPCYCLEOPTIONMASTERID_FLD, typeof (int)));
				dtbCPOs.Columns[MTR_CPOTable.MRPCYCLEOPTIONMASTERID_FLD].AllowDBNull = true;
				dtbCPOs.Columns.Add(new DataColumn(MTR_CPOTable.MPSCYCLEOPTIONMASTERID_FLD, typeof (int)));
				dtbCPOs.Columns[MTR_CPOTable.MPSCYCLEOPTIONMASTERID_FLD].AllowDBNull = true;
				dtbCPOs.Columns[MTR_CPOTable.MPSCYCLEOPTIONMASTERID_FLD].DefaultValue = voMPSCycleOptionMaster.MPSCycleOptionMasterID;
				dtbCPOs.Columns.Add(new DataColumn(MTR_CPOTable.PARENTCPOID_FLD, typeof (long)));
				dtbCPOs.Columns[MTR_CPOTable.PARENTCPOID_FLD].AllowDBNull = true;
				dtbCPOs.Columns.Add(new DataColumn(MTR_CPOTable.DEMANDQUANTITY_FLD, typeof (decimal)));
				dtbCPOs.Columns[MTR_CPOTable.DEMANDQUANTITY_FLD].AllowDBNull = true;
				//dtbCPOs.Columns[MTR_CPOTable.DEMANDQUANTITY_FLD].MaxLength = int.MaxValue;
				dtbCPOs.Columns[MTR_CPOTable.DEMANDQUANTITY_FLD].DefaultValue = decimal.Zero;
				dtbCPOs.Columns.Add(new DataColumn(MTR_CPOTable.SUPPLYQUANTITY_FLD, typeof (decimal)));
				dtbCPOs.Columns[MTR_CPOTable.SUPPLYQUANTITY_FLD].AllowDBNull = true;
				//dtbCPOs.Columns[MTR_CPOTable.SUPPLYQUANTITY_FLD].MaxLength = int.MaxValue;
				dtbCPOs.Columns[MTR_CPOTable.SUPPLYQUANTITY_FLD].DefaultValue = decimal.Zero;
				dtbCPOs.Columns.Add(new DataColumn(MTR_CPOTable.ISSAFETYSTOCK_FLD, typeof (bool)));
				dtbCPOs.Columns[MTR_CPOTable.ISSAFETYSTOCK_FLD].AllowDBNull = true;
				dtbCPOs.Columns[MTR_CPOTable.ISSAFETYSTOCK_FLD].DefaultValue = false;
				// number of day to add to child
				dtbCPOs.Columns.Add(new DataColumn(DAY_TO_ADD_COL, typeof (decimal)));
				dtbCPOs.Columns[DAY_TO_ADD_COL].DefaultValue = decimal.Zero;
				dtbCPOs.Columns[DAY_TO_ADD_COL].AllowDBNull = true;

				// leadtimeoffset from bom
				dtbCPOs.Columns.Add(new DataColumn(IS_SUPPLIED_COL, typeof (bool)));
				dtbCPOs.Columns[IS_SUPPLIED_COL].DefaultValue = false;
				dtbCPOs.Columns[IS_SUPPLIED_COL].AllowDBNull = true;

				#endregion

				// get all valid day of all configured year in system
				DataTable dtbDayOfWeek = boMPSRegeneration.GetDayOfWeek();
				// get all holidays in system.
				DataTable dtbHolidays = boMPSRegeneration.GetHolidays();
				// working time configure in Shift Pattern
				//DCPReportBO boDCP = new DCPReportBO();
				//DataTable dtbWorkingTime = boDCP.GetWorkingTime();

				DataTable dtbFutureSOs = null;
				DataTable dtbFuturePOs = null;
				DataTable dtbFutureSupplyWOs = null;
				DataTable dtbFutureDemandWOs = null;
				if (dtmCurrentDate < voMPSCycleOptionMaster.AsOfDate)
				{
					// get all SOs from current date to as of date
					dtbFutureSOs = boMPSRegeneration.GetTotalSO(voMPSCycleOptionMaster.CCNID, dtmCurrentDate, voMPSCycleOptionMaster.AsOfDate,0);
					// get all POs from current date to as of date
					dtbFuturePOs = boMPSRegeneration.GetTotalPO(voMPSCycleOptionMaster.CCNID, dtmCurrentDate, voMPSCycleOptionMaster.AsOfDate,0);
					// get all supply WOs from current date to as of date
					dtbFutureSupplyWOs = boMPSRegeneration.GetTotalWO(voMPSCycleOptionMaster.CCNID, dtmCurrentDate, voMPSCycleOptionMaster.AsOfDate,0);
					// get all demand WOs from current date to as of date
					dtbFutureDemandWOs = boMPSRegeneration.GetTotalDemandWO(voMPSCycleOptionMaster.CCNID, dtmCurrentDate, voMPSCycleOptionMaster.AsOfDate,0);
					foreach (DataRow drowWO in dtbFutureDemandWOs.Rows)
					{
						decimal decDemandQty = 0;
						decimal decShrink = 0;
						DateTime dtmStartDate = DateTime.MinValue;
						decimal decLeadTimeOffset = 0;

						#region demand start date
						try
						{
							dtmStartDate = (DateTime)drowWO[PRO_WorkOrderDetailTable.STARTDATE_FLD];
						}
						catch{}
						try
						{
							decLeadTimeOffset = decimal.Parse(drowWO[PRO_WorkOrderBomDetailTable.LEADTIMEOFFSET_FLD].ToString().Trim()) / 86400;
						}
						catch{}
						if (dtmStartDate != DateTime.MinValue && decLeadTimeOffset > 0)
						{
							// add lead time offset to start date
							dtmStartDate = ConvertWorkingDay(dtbDayOfWeek, dtbHolidays, dtmStartDate, decLeadTimeOffset);
							if (dtmStartDate > voMPSCycleOptionMaster.AsOfDate)
							{
								// if start date of parent work order is in the planning horizon
								// ignore it
								drowWO.Delete();
								continue;
							}
							// update row
							drowWO[PRO_WorkOrderDetailTable.STARTDATE_FLD] = dtmStartDate;
						}
						#endregion

						#region demand quantity

						try
						{
							decDemandQty = decimal.Parse(drowWO[Constants.DEMAND_QUANTITY_FLD].ToString().Trim());
						}
						catch{}
						try
						{
							decShrink = decimal.Parse(drowWO[PRO_WorkOrderBomDetailTable.SHRINK_FLD].ToString().Trim());
						}
						catch{}
						if (decShrink > 0)
							decDemandQty = decDemandQty/(1 - decShrink/100);

						drowWO[Constants.DEMAND_QUANTITY_FLD] = decDemandQty;

						#endregion
					}
					dtbFutureDemandWOs.AcceptChanges();
				}
				
				ArrayList arrDateTimes = new ArrayList();
				// add AsOfDate as the first day has demand
				//arrDateTimes.Add(voMPSCycleOptionMaster.AsOfDate);
				// clear old CPO first
				boMPSRegeneration.DeleteCPOs(voMPSCycleOptionMaster.CCNID, voMPSCycleOptionMaster.MPSCycleOptionMasterID);
				// start planning from top level items for each master location
				foreach (DataRow drowItem in dtbMPSCycleOptionDetail.Rows)
				{
					int intMasterLocationID = int.Parse(drowItem[MTR_MPSCycleOptionDetailTable.MASTERLOCATIONID_FLD].ToString());
					bool blnOnHand = Convert.ToBoolean(drowItem[MTR_MPSCycleOptionDetailTable.ONHAND_FLD].ToString());
					bool blnDemandWO = Convert.ToBoolean(drowItem[MTR_MPSCycleOptionDetailTable.DEMANDWO_FLD].ToString());
					bool blnSupplyWO = Convert.ToBoolean(drowItem[MTR_MPSCycleOptionDetailTable.SUPPLYWO_FLD].ToString());
					bool blnPO = Convert.ToBoolean(drowItem[MTR_MPSCycleOptionDetailTable.PURCHASEORDER_FLD].ToString());
					bool blnSO = Convert.ToBoolean(drowItem[MTR_MPSCycleOptionDetailTable.SALEORDER_FLD].ToString());
					bool blnSafetyStock = Convert.ToBoolean(drowItem[MTR_MPSCycleOptionDetailTable.SAFETYSTOCK_FLD].ToString());
					DataTable dtbSaleOrders = null;
					DataTable dtbDemandWorkOrders = null;
					DataTable dtbSupplyWorkOrders = null;
					DataTable dtbPurchaseOrders = null;
					DataTable dtbInventory = null;

					// if user did not select any option for this master location
					// go to next master location in list
					if (!blnOnHand && !blnDemandWO && !blnSupplyWO && !blnPO && !blnSO && !blnSafetyStock)
						continue;

					#region Prepare data

					// if Onhand is checked we calculate NetAvailableQuantity with Iventory Onhand
					if (blnOnHand)
						dtbInventory = boMPSRegeneration.GetAvailableQuantityForPlan(voMPSCycleOptionMaster.CCNID, intMasterLocationID);
					// retrieve all demand from sale order(s) which order current product
					// and have the schedule date in range of AsOfDate and DueDate
					if (blnSO)
						dtbSaleOrders = boMPSRegeneration.RetrieveSaleOrders(voMPSCycleOptionMaster.CCNID, intMasterLocationID, voMPSCycleOptionMaster.AsOfDate, dtmDueDate);
					// retrieve all demand from work order(s) of current product's parent(s)
					// which have the start date in range of AsOfDate and DueDate
					if (blnDemandWO)
					{
						dtbDemandWorkOrders = boMPSRegeneration.RetrieveParents(voMPSCycleOptionMaster.CCNID, intMasterLocationID, voMPSCycleOptionMaster.AsOfDate, dtmDueDate);
						// we need to re-calculate the start date of parent work order
						foreach (DataRow drowWO in dtbDemandWorkOrders.Rows)
						{
							DateTime dtmStartDate = DateTime.MinValue;
							decimal decLeadTimeOffset = 0;

							#region demand start date
							try
							{
								dtmStartDate = (DateTime)drowWO[PRO_WorkOrderDetailTable.STARTDATE_FLD];
							}
							catch{}
							try
							{
								decLeadTimeOffset = decimal.Parse(drowWO[PRO_WorkOrderBomDetailTable.LEADTIMEOFFSET_FLD].ToString().Trim()) / 86400;
							}
							catch{}
							if (dtmStartDate != DateTime.MinValue && decLeadTimeOffset > 0)
							{
								// add lead time offset to start date
								dtmStartDate = ConvertWorkingDay(dtbDayOfWeek, dtbHolidays, dtmStartDate, decLeadTimeOffset);
								// update row
								drowWO[PRO_WorkOrderDetailTable.STARTDATE_FLD] = dtmStartDate;
							}
							#endregion
						}
					}
					// retrieve all supply from work order(s) in process of current product
					// which have the due date in range of AsOfDate and DueDate
					if (blnSupplyWO)
						dtbSupplyWorkOrders = boMPSRegeneration.RetrieveSupplyFromWO(voMPSCycleOptionMaster.CCNID, intMasterLocationID, voMPSCycleOptionMaster.AsOfDate, dtmDueDate);
					// retrieve all supply from purchase order(s) which order current product
					// and have the delivery date in range of AsOfDate and DueDate
					if (blnPO)
						dtbPurchaseOrders = boMPSRegeneration.RetriveSupplyFromPO(voMPSCycleOptionMaster.CCNID, intMasterLocationID, voMPSCycleOptionMaster.AsOfDate, dtmDueDate);

					#endregion

					#region Refine date time based on cycle option

					// get the demand date from sale order
					DateTime dtmPlanDate = DateTime.MinValue;
					if (blnSO)
					{
						//DateTime dtmPreDate = DateTime.MaxValue;
						foreach (DataRow drowSO in dtbSaleOrders.Rows)
						{
							DateTime dtmScheduleDate = (DateTime)drowSO[SO_DeliveryScheduleTable.SCHEDULEDATE_FLD];
							// group by hour
							if (voMPSCycleOptionMaster.GroupBy == (int)MPSGroupByEnum.ByHour)
								dtmPlanDate = new DateTime(dtmScheduleDate.Year, dtmScheduleDate.Month, dtmScheduleDate.Day, dtmScheduleDate.Hour, 0, 0);
							else // group by day
							{
								// group due date to the first due date of the day
								if (dtmPlanDate < dtmScheduleDate)
								{
									if (dtmPlanDate.Year == dtmScheduleDate.Year && dtmPlanDate.Month == dtmScheduleDate.Month 
										&& dtmPlanDate.Day == dtmScheduleDate.Day)
										dtmPlanDate = dtmPlanDate;
									else
										dtmPlanDate = dtmScheduleDate;
								}
								//DateTime dtmStart = dtmScheduleDate;
								//DateTime dtmEnd = dtmStart;
								//GetStartAndEndTime(dtmScheduleDate, ref dtmStart, ref dtmEnd, dtbWorkingTime);
								//dtmPlanDate = new DateTime(dtmScheduleDate.Year, dtmScheduleDate.Month, dtmScheduleDate.Day, dtmStart.Hour, dtmStart.Minute, 0);
							}
							// get product id
							string strProductID = drowSO[SO_SaleOrderDetailTable.PRODUCTID_FLD].ToString().Trim();
							// get product information from the items table
							DataRow[] drowProduct = dtbProducts.Select(ITM_ProductTable.PRODUCTID_FLD + "='" + strProductID + "'");
							if (drowProduct != null && drowProduct.Length > 0)
							{
								ITM_ProductVO voProduct = GetProductInfo(drowProduct[0]);
								decimal decDayToAdd = voProduct.LTSalesATP / 86400;
								dtmPlanDate = ConvertWorkingDay(dtbDayOfWeek, dtbHolidays, dtmPlanDate, decDayToAdd);
							}
							// update to DataRow
							drowSO[SO_DeliveryScheduleTable.SCHEDULEDATE_FLD] = dtmPlanDate;
							// add leadtime to due date
							if (!arrDateTimes.Contains(dtmPlanDate))
								arrDateTimes.Add(dtmPlanDate);
						}
					}
					// reset plan date
					dtmPlanDate = DateTime.MinValue;
					// get the demand date from work order
					if (blnDemandWO)
					{
						foreach (DataRow drowWO in dtbDemandWorkOrders.Rows)
						{
							DateTime dtmScheduleDate = (DateTime)drowWO[PRO_WorkOrderDetailTable.STARTDATE_FLD];
							// group by hour
							if (voMPSCycleOptionMaster.GroupBy == (int)MPSGroupByEnum.ByHour)
								dtmPlanDate = new DateTime(dtmScheduleDate.Year, dtmScheduleDate.Month, dtmScheduleDate.Day, dtmScheduleDate.Hour, 0, 0);
							else // group by day
							{
								// group due date to the first due date of the day
								if (dtmPlanDate < dtmScheduleDate)
								{
									if (dtmPlanDate.Year == dtmScheduleDate.Year && dtmPlanDate.Month == dtmScheduleDate.Month 
										&& dtmPlanDate.Day == dtmScheduleDate.Day)
										dtmPlanDate = dtmPlanDate;
									else
										dtmPlanDate = dtmScheduleDate;
								}
//								DateTime dtmStart = dtmScheduleDate;
//								DateTime dtmEnd = dtmStart;
//								GetStartAndEndTime(dtmScheduleDate, ref dtmStart, ref dtmEnd, dtbWorkingTime);
//								dtmPlanDate = new DateTime(dtmScheduleDate.Year, dtmScheduleDate.Month, dtmScheduleDate.Day, dtmStart.Hour, dtmStart.Minute, 0);
							}
							// get product id
							string strProductID = drowWO[PRO_WorkOrderBomDetailTable.COMPONENTID_FLD].ToString().Trim();
							// get product information from the items table
							DataRow[] drowProduct = dtbProducts.Select(ITM_ProductTable.PRODUCTID_FLD + "='" + strProductID + "'");
							if (drowProduct != null && drowProduct.Length > 0)
							{
								ITM_ProductVO voProduct = GetProductInfo(drowProduct[0]);
								decimal decDayToAdd = voProduct.LTSalesATP/86400;
								dtmPlanDate = ConvertWorkingDay(dtbDayOfWeek, dtbHolidays, dtmPlanDate, decDayToAdd);
							}
							// update to DataRow
							drowWO[PRO_WorkOrderDetailTable.STARTDATE_FLD] = dtmPlanDate;
							if (!arrDateTimes.Contains(dtmPlanDate))
								arrDateTimes.Add(dtmPlanDate);
						}
					}
					if (!arrDateTimes.Contains(dtmDueDate))
						arrDateTimes.Add(dtmDueDate);
					arrDateTimes.Sort();
					arrDateTimes.TrimToSize();

					// reset Plan date
					dtmPlanDate = DateTime.MinValue;
					// remove milliseconds from Schedule date and due date of PO/WO
					if (blnPO)
					{
						foreach (DataRow drowPO in dtbPurchaseOrders.Rows)
						{
							DateTime dtmScheduleDate = (DateTime)drowPO[PO_DeliveryScheduleTable.SCHEDULEDATE_FLD];
							// group by hour
							if (voMPSCycleOptionMaster.GroupBy == (int)MPSGroupByEnum.ByHour)
								dtmPlanDate = new DateTime(dtmScheduleDate.Year, dtmScheduleDate.Month, dtmScheduleDate.Day, dtmScheduleDate.Hour, 0, 0);
							else // group by day
							{
								// group due date to the first due date of the day
								if (dtmPlanDate < dtmScheduleDate)
								{
									if (dtmPlanDate.Year == dtmScheduleDate.Year && dtmPlanDate.Month == dtmScheduleDate.Month 
										&& dtmPlanDate.Day == dtmScheduleDate.Day)
										dtmPlanDate = dtmPlanDate;
									else
										dtmPlanDate = dtmScheduleDate;
								}
//								DateTime dtmStart = dtmScheduleDate;
//								DateTime dtmEnd = dtmStart;
//								GetStartAndEndTime(dtmScheduleDate, ref dtmStart, ref dtmEnd, dtbWorkingTime);
//								dtmPlanDate = new DateTime(dtmScheduleDate.Year, dtmScheduleDate.Month, dtmScheduleDate.Day, dtmStart.Hour, dtmStart.Minute, 0);
							}
							// update to DataRow
							drowPO[PO_DeliveryScheduleTable.SCHEDULEDATE_FLD] = dtmPlanDate;
							if (!arrDateTimes.Contains(dtmPlanDate))
								arrDateTimes.Add(dtmPlanDate);
						}
					}
					// reset plan date
					dtmPlanDate = DateTime.MinValue;
					if (blnSupplyWO)
					{
						foreach (DataRow drowWO in dtbSupplyWorkOrders.Rows)
						{
							DateTime dtmScheduleDate = (DateTime)drowWO[PRO_WorkOrderDetailTable.DUEDATE_FLD];
							// group by hour
							if (voMPSCycleOptionMaster.GroupBy == (int)MPSGroupByEnum.ByHour)
								dtmPlanDate = new DateTime(dtmScheduleDate.Year, dtmScheduleDate.Month, dtmScheduleDate.Day, dtmScheduleDate.Hour, 0, 0);
							else // group by day
							{
								// group due date to the first due date of the day
								if (dtmPlanDate < dtmScheduleDate)
								{
									if (dtmPlanDate.Year == dtmScheduleDate.Year && dtmPlanDate.Month == dtmScheduleDate.Month 
										&& dtmPlanDate.Day == dtmScheduleDate.Day)
										dtmPlanDate = dtmPlanDate;
									else
										dtmPlanDate = dtmScheduleDate;
								}
//								DateTime dtmStart = dtmScheduleDate;
//								DateTime dtmEnd = dtmStart;
//								GetStartAndEndTime(dtmScheduleDate, ref dtmStart, ref dtmEnd, dtbWorkingTime);
//								dtmPlanDate = new DateTime(dtmScheduleDate.Year, dtmScheduleDate.Month, dtmScheduleDate.Day, dtmStart.Hour, dtmStart.Minute, 0);
							}
							// update to DataRow
							drowWO[PRO_WorkOrderDetailTable.DUEDATE_FLD] = dtmPlanDate;
							if (!arrDateTimes.Contains(dtmPlanDate))
								arrDateTimes.Add(dtmPlanDate);
						}
					}

					#endregion

					#region Calculate and generate CPO

					// select all top level item
					DataRow[] drowProducts = dtbProducts.Select("ParentID IS NULL");
					// clear all result of previous master location
					dtbCPOs.Clear();
					// reset CPO ID column
					dtbCPOs.Columns[MTR_CPOTable.CPOID_FLD].AllowDBNull = false;
					dtbCPOs.Columns[MTR_CPOTable.CPOID_FLD].AutoIncrement = true;
					dtbCPOs.Columns[MTR_CPOTable.CPOID_FLD].AutoIncrementSeed = 1;
					dtbCPOs.Columns[MTR_CPOTable.CPOID_FLD].AutoIncrementStep = 1;
					// assign default value for master location id field
					dtbCPOs.Columns[MTR_CPOTable.MASTERLOCATIONID_FLD].DefaultValue = intMasterLocationID;
					// start regeneration process
					DataTable dtbResult = GeneratePlanOffline(dtbProducts, drowProducts, intMasterLocationID, arrDateTimes, 
						voMPSCycleOptionMaster, 0, dtbInventory, dtbSaleOrders, dtbDemandWorkOrders, 
						dtbSupplyWorkOrders, dtbPurchaseOrders, dtbCPOs, dtbDayOfWeek, dtbHolidays,
						dtbFutureSOs, dtbFuturePOs, dtbFutureSupplyWOs, dtbFutureDemandWOs, null, blnSafetyStock);

					#endregion

					#region Save CPO to database

					// after generated all CPOs we need save to database 
					// and update reference of parent and child CPO
					// we need to turn off auto increment property of CPO ID in order to update child CPO
					dtbResult.Columns[MTR_CPOTable.CPOID_FLD].AutoIncrement = false;

					// now save new CPO to database
					foreach (DataRow drowCPO in dtbResult.Rows)
					{
						// store old id
						long lngOldID = long.Parse(drowCPO[MTR_CPOTable.CPOID_FLD].ToString());
						MTR_CPOVO voCPO = new MTR_CPOVO();
						// retrieve data from data row
						voCPO.CCNID = voMPSCycleOptionMaster.CCNID;
						voCPO.Converted = false;
						voCPO.DueDate = (DateTime) drowCPO[MTR_CPOTable.DUEDATE_FLD];
						voCPO.IsMPS = true;
						voCPO.MasterLocationID = intMasterLocationID;
						voCPO.MPSCycleOptionMasterID = voMPSCycleOptionMaster.MPSCycleOptionMasterID;
						voCPO.NetAvailableQuantity = decimal.Round(decimal.Parse(drowCPO[MTR_CPOTable.NETAVAILABLEQUANTITY_FLD].ToString().Trim()), DECIMAL_ROUND_NUMBER);
						if (drowCPO[MTR_CPOTable.PARENTCPOID_FLD] != null && drowCPO[MTR_CPOTable.PARENTCPOID_FLD] != DBNull.Value
							&& !drowCPO[MTR_CPOTable.PARENTCPOID_FLD].Equals(0))
							voCPO.ParentCPOID = int.Parse(drowCPO[MTR_CPOTable.PARENTCPOID_FLD].ToString().Trim());
						voCPO.ProductID = int.Parse(drowCPO[MTR_CPOTable.PRODUCTID_FLD].ToString().Trim());
						voCPO.StockUMID = int.Parse(drowCPO[MTR_CPOTable.STOCKUMID_FLD].ToString().Trim());
						try
						{
							voCPO.IsSafetyStock = bool.Parse(drowCPO[MTR_CPOTable.ISSAFETYSTOCK_FLD].ToString().Trim());
						}
						catch
						{
							voCPO.IsSafetyStock = false;
						}
						// total demand
						voCPO.DemandQuantity = decimal.Round(decimal.Parse(drowCPO[MTR_CPOTable.DEMANDQUANTITY_FLD].ToString().Trim()), DECIMAL_ROUND_NUMBER);
						// total supply
						voCPO.SupplyQuantity = decimal.Round(decimal.Parse(drowCPO[MTR_CPOTable.SUPPLYQUANTITY_FLD].ToString().Trim()), DECIMAL_ROUND_NUMBER);
						// quantity need to be produce
						voCPO.Quantity = decimal.Round(decimal.Parse(drowCPO[MTR_CPOTable.QUANTITY_FLD].ToString().Trim()), DECIMAL_ROUND_NUMBER);
						// start date
						voCPO.StartDate = (DateTime) drowCPO[MTR_CPOTable.STARTDATE_FLD];
						// save to database first and get new ID
						voCPO.CPOID = boMPSRegeneration.AddAndReturnID(voCPO);
						// select all child CPO of this CPO
						DataRow[] drowChildCPOs = dtbResult.Select(MTR_CPOTable.PARENTCPOID_FLD + "='" + lngOldID + "'");
						if (drowChildCPOs != null && drowChildCPOs.Length > 0)
						{
							foreach (DataRow drowChild in drowChildCPOs)
							{
								// ignore all modified row
								if (drowChild.RowState == DataRowState.Modified)
									continue;
								// update parent CPOID
								drowChild[MTR_CPOTable.PARENTCPOID_FLD] = voCPO.CPOID;
							}
						}
					}

					#endregion
				}

				// update cycle master object
				voMPSCycleOptionMaster.MPSGenDate = DateTime.Now;
				boMPSRegeneration.Update(voMPSCycleOptionMaster);

				//boMPSRegeneration.RunMPSProcessOffline(int.Parse(cboCCN.SelectedValue.ToString()), voMPSCycleOptionMaster.MPSCycleOptionMasterID);
				//PCSMessageBox.Show(ErrorCode.MESSAGE_AFTER_SAVE_DATA, MessageBoxButtons.OK, MessageBoxIcon.Information);
				string[] strParams = new string[1];
				strParams[0] = this.Text;
				PCSMessageBox.Show(ErrorCode.MESSAGE_GENERATED_SUCCESSFULLY, MessageBoxButtons.OK,
					MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, strParams);
			}
			catch (ThreadAbortException ex)
			{
				Logger.LogMessage(ex, METHOD_NAME, Level.DEBUG);
			}
			catch (PCSException ex)
			{
				if (ex.mCode == ErrorCode.MESSAGE_DCP_CONFIG_CALENDAR_FROM_X_TO_Y)
				{
					string[] strParams = new string[2];
					strParams[0] = voMPSCycleOptionMaster.AsOfDate.Year.ToString();
					strParams[1] = voMPSCycleOptionMaster.AsOfDate.AddDays(voMPSCycleOptionMaster.PlanHorizon).Year.ToString();
					PCSMessageBox.Show(ex.mCode, MessageBoxIcon.Error, strParams);
				}
				else if (ex.mCode == ErrorCode.CASCADE_DELETE_PREVENT)
				{
					// warn user to delete DCP result first
					PCSMessageBox.Show(ErrorCode.MESSAGE_MPS_ALREADY_USED_IN_DCP, MessageBoxIcon.Error);
				}
				else
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
			finally
			{
				lblCCN.Visible = true;
				cboCCN.Visible = true;
				lblCycle.Visible = true;
				txtCycle.Visible = true;
				btnCycle.Visible = true;
				btnPreview.Enabled = true;
				btnProcess.Enabled = true;
				lblProcessing.Visible = false;
				picProcessing.Visible = false;
			}
		}
		/// <summary>
		/// Scanning through the list of demand date and generated CPO for item if any.
		/// </summary>
		/// <param name="pdtbAllProducts">List of Item of selected CCN</param>
		/// <param name="pdrowProducts">All products to be planned</param>
		/// <param name="pintMasterLocationID">Master Location</param>
		/// <param name="parrDateTimes">List of all date which have demand</param>
		/// <param name="pobjMPSCycleOptionMaster">MPSCycleOptionMasterVO</param>
		/// <param name="pintParentID">Parent Product ID</param>
		/// <param name="pdtbInventory">Quantity from Inventory</param>
		/// <param name="pdtbSaleOrders">Demand from Sale Orders</param>
		/// <param name="pdtbDemandWOs">Demand from Work Orders</param>
		/// <param name="pdtbSupplyWOs">Supply from Work Orders</param>
		/// <param name="pdtbPOs">Supply from Purchase Orders</param>
		/// <param name="pdtbPOs">List of generated Parent CPOs</param>
		private DataTable GeneratePlanOffline(DataTable pdtbAllProducts, DataRow[] pdrowProducts,
			int pintMasterLocationID, ArrayList parrDateTimes,
			object pobjMPSCycleOptionMaster, int pintParentID, DataTable pdtbInventory,
			DataTable pdtbSaleOrders, DataTable pdtbDemandWOs, DataTable pdtbSupplyWOs, DataTable pdtbPOs,
			DataTable pdtbCPOs, DataTable pdtbDayOfWeek, DataTable pdtbHolidays,
			DataTable pdtbFutureSOs, DataTable pdtbFuturePOs, DataTable pdtbFutureSupplyWOs,
			DataTable pdtbFutureDemandWOs, DataTable pdtbParentCPOs, bool pblnSafetyStock)
		{
			MTR_MPSCycleOptionMasterVO voMPSCycleOptionMaster = (MTR_MPSCycleOptionMasterVO) pobjMPSCycleOptionMaster;
			// generate plan for each item in table
			foreach (DataRow drowItem in pdrowProducts)
			{
				ArrayList arrDateTimes = new ArrayList();
				arrDateTimes = (ArrayList)parrDateTimes.Clone();
				//arrDateTimes = parrDateTimes.GetRange(0, parrDateTimes.Count);
				decimal decNetAvailableQuantity = 0;
				decimal decYieldRate = 0;
				// remain quantity of previous day will be considered as available quantity of next day
				//decimal decRemainQuantity = 0;
				decimal decInventoryQuantity = 0;

				#region product information

				ITM_ProductVO voProduct = GetProductInfo(drowItem);
				
				#endregion

				// get all components of current product
				DataRow[] drowComponents = pdtbAllProducts.Select("ParentID = '" + voProduct.ProductID + "'");

				DataTable dtbParentCPOs = pdtbCPOs.Clone();
				// turn-off the auto increment of parent cpo table
				// cause we will import row with CPO id from CPO table
				dtbParentCPOs.Columns[MTR_CPOTable.CPOID_FLD].AutoIncrement = false;

				// if current product is MPS, start calculating plan data
				if (voProduct.PlanType.Equals((int) PlanTypeEnum.MPS))
				{
					decimal decShrink = 0;
					DateTime dtmPrevDate = voMPSCycleOptionMaster.AsOfDate;
					
					#region Supply quantity from inventory

					// supply quantity from inventory
					DataRow[] drowInventory = null;
					DataRow[] drowFutureSOs = null;
					DataRow[] drowFuturePOs = null;
					DataRow[] drowFutureSupplyWOs = null;
					DataRow[] drowFutureDemandWOs = null;

					if (pdtbInventory != null)
					{
						#region Onhand quantity

						drowInventory = pdtbInventory.Select(ITM_ProductTable.PRODUCTID_FLD + "='" + voProduct.ProductID + "'");
					
						if (drowInventory != null && drowInventory.Length > 0)
							foreach (DataRow drowInven in drowInventory)
							{
								try
								{
									decInventoryQuantity += decimal.Parse(drowInven[Constants.SUPPLY_QUANTITY_FLD].ToString());
								}
								catch{}
							}

						#endregion

						#region Subtract the future demand quantity from inventory
						if (pdtbFutureSOs != null)
							drowFutureSOs = pdtbFutureSOs.Select(ITM_ProductTable.PRODUCTID_FLD + "='" + voProduct.ProductID + "'"
								+ " AND " + SO_SaleOrderMasterTable.SHIPFROMLOCID_FLD + "='" + pintMasterLocationID + "'");
						if (drowFutureSOs != null && drowFutureSOs.Length > 0)
						{
							foreach (DataRow drowSOs in drowFutureSOs)
							{
								decimal decSOs = 0;
								try
								{
									decSOs = decimal.Parse(drowSOs[SO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD].ToString());
								}
								catch{}
								if (decSOs <= 0)
									continue;
								// subtract future demand quantity
								decInventoryQuantity -= decSOs;
								// remove demand quantity from data table
								drowSOs[SO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD] = decimal.Zero;
							}
							// update data table
							pdtbFutureSOs.AcceptChanges();
						}

						if (pdtbFutureDemandWOs != null)
							drowFutureDemandWOs = pdtbFutureDemandWOs.Select(ITM_BOMTable.COMPONENTID_FLD + "='" + voProduct.ProductID + "'"
								+ " AND " + PRO_WorkOrderMasterTable.MASTERLOCATIONID_FLD + "='" + pintMasterLocationID + "'");
						if (drowFutureDemandWOs != null && drowFutureDemandWOs.Length > 0)
						{
							foreach (DataRow drowWOs in drowFutureDemandWOs)
							{
								decimal decWOs = 0;
								try
								{
									decWOs = decimal.Parse(drowWOs[Constants.DEMAND_QUANTITY_FLD].ToString());
								}
								catch{}
								if (decWOs <= 0)
									continue;
								// substract demand quantity
								decInventoryQuantity -= decWOs;
								// remove demand quantity from data table
								drowWOs[Constants.DEMAND_QUANTITY_FLD] = decimal.Zero;
							}
							// update data table
							pdtbFutureDemandWOs.AcceptChanges();
						}
						#endregion

						#region Add the future supply quantity to inventory
						if (pdtbFuturePOs != null)
							drowFuturePOs = pdtbFuturePOs.Select(ITM_ProductTable.PRODUCTID_FLD + "='" + voProduct.ProductID + "'"
								+ " AND " + PO_PurchaseOrderMasterTable.MASTERLOCATIONID_FLD + "='" + pintMasterLocationID + "'");
						if (drowFuturePOs != null && drowFuturePOs.Length > 0)
						{
							foreach (DataRow drowPOs in drowFuturePOs)
							{
								decimal decPO = 0;
								try
								{
									decPO = decimal.Parse(drowPOs[PO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD].ToString());
								}
								catch{}
								if (decPO <= 0)
									continue;
								// add suppy quantity
								decInventoryQuantity += decPO;
								// remove used supply source
								drowPOs[PO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD] = decimal.Zero;
							}
							// update data table
							pdtbFuturePOs.AcceptChanges();
						}

						if (pdtbFutureSupplyWOs != null)
							drowFutureSupplyWOs = pdtbFutureSupplyWOs.Select(ITM_ProductTable.PRODUCTID_FLD + "='" + voProduct.ProductID + "'"
								+ " AND " + PRO_WorkOrderMasterTable.MASTERLOCATIONID_FLD + "='" + pintMasterLocationID + "'");
						if (drowFutureSupplyWOs != null && drowFutureSupplyWOs.Length > 0)
						{
							foreach (DataRow drowWOs in drowFutureSupplyWOs)
							{
								decimal decWOS = 0;
								try
								{
									decWOS = decimal.Parse(drowWOs[PRO_WorkOrderDetailTable.ORDERQUANTITY_FLD].ToString());
								}
								catch{}
								if (decWOS <= 0)
									continue;
								// add supply quantity
								decInventoryQuantity += (decWOS * decYieldRate);
								// remove used supply source
								drowWOs[PRO_WorkOrderDetailTable.ORDERQUANTITY_FLD] = decimal.Zero;
							}
							// update data table
							pdtbFutureSupplyWOs.AcceptChanges();
						}
						#endregion
					}

					// update new available quantity to inventory table
					if (drowInventory != null && drowInventory.Length > 0)
					{
						drowInventory[0][Constants.SUPPLY_QUANTITY_FLD] = decInventoryQuantity;
						pdtbInventory.AcceptChanges();
					}

					#endregion

					// calculate YieldRate of Product
					decYieldRate = decimal.Round((100 - voProduct.ScrapPercent)/100, DECIMAL_ROUND_NUMBER);
					if (pdtbInventory != null)
						decNetAvailableQuantity = decimal.Round(decInventoryQuantity, DECIMAL_ROUND_NUMBER);
					if (pblnSafetyStock)
					{
						decNetAvailableQuantity = decimal.Round(decInventoryQuantity - voProduct.SafetyStock, DECIMAL_ROUND_NUMBER);
						// remove safety stock of item in all bom item
						DataRow[] drowItems = pdtbAllProducts.Select(ITM_ProductTable.PRODUCTID_FLD + " = '" + voProduct.ProductID + "'");
						foreach (DataRow drowData in drowItems)
							drowData[ITM_ProductTable.SAFETYSTOCK_FLD] = decimal.Zero;
						pdtbAllProducts.AcceptChanges();
					}

					#region planning for each demand date

					// scan the Date Time array
					foreach (DateTime dtmDueDate in parrDateTimes)
					{
						// demand date out of the planning horizon
						// no need to generate CPO
						if (dtmDueDate < voMPSCycleOptionMaster.AsOfDate ||
							dtmDueDate > voMPSCycleOptionMaster.AsOfDate.AddDays(voMPSCycleOptionMaster.PlanHorizon))
							continue;
						MTR_CPOVO voParentCPO = null;
						DataRow[] drowSaleOrders = null;
						DataRow[] drowDemandWorkOrders = null;
						DataTable dtbTempDemandWOs = null;
						DataRow[] drowPurchaseOrders = null;
						DataRow[] drowSupplyWorkOrders = null;
						decimal decSaleOrder = 0;
						decimal decDemandWOs = 0;
						decimal decSupplyWOs = 0;
						decimal decSupplyPOs = 0;
						decimal decDemandFromParentCPO = 0;
						decimal decDemandOfDay = 0;
						decimal decParentLTOffset = 0;
						
						#region Calculate Demands

						string strExpression = string.Empty;

						#region Demand from Sale Orders

						if (pdtbSaleOrders != null && pdtbSaleOrders.Rows.Count > 0)
						{
//							strExpression = SO_DeliveryScheduleTable.SCHEDULEDATE_FLD
//								+ " >= '" + dtmPrevDate.ToString("G", DateTimeFormatInfo.CurrentInfo) + "' AND "
//								+ SO_DeliveryScheduleTable.SCHEDULEDATE_FLD + " <= '" + dtmDueDate.ToString("G", DateTimeFormatInfo.CurrentInfo) + "'"
//								+ " AND " + SO_SaleOrderDetailTable.PRODUCTID_FLD + "='" + voProduct.ProductID + "'";
							strExpression = SO_DeliveryScheduleTable.SCHEDULEDATE_FLD
								+ " >= '" + dtmPrevDate.ToString("G") + "' AND "
								+ SO_DeliveryScheduleTable.SCHEDULEDATE_FLD + " <= '" + dtmDueDate.ToString("G") + "'"
								+ " AND " + SO_SaleOrderDetailTable.PRODUCTID_FLD + "='" + voProduct.ProductID + "'";
							drowSaleOrders = pdtbSaleOrders.Select(strExpression);
						}
						if (drowSaleOrders != null && drowSaleOrders.Length > 0)
						{
							foreach (DataRow drowSO in drowSaleOrders)
							{
								try
								{
									decSaleOrder += decimal.Parse(drowSO[Constants.DEMAND_QUANTITY_FLD].ToString());
								}
								catch{}
								drowSO[Constants.DEMAND_QUANTITY_FLD] = decimal.Zero;
							}
							pdtbSaleOrders.AcceptChanges();
						}

						#endregion

						#region Demand from Parent Work Orders

						if (pdtbDemandWOs != null && pdtbDemandWOs.Rows.Count > 0)
						{
							dtbTempDemandWOs = pdtbDemandWOs.Clone();
//							strExpression = PRO_WorkOrderDetailTable.STARTDATE_FLD
//								+ " >= '" + dtmPrevDate.ToString("G", DateTimeFormatInfo.CurrentInfo) + "' AND "
//								+ PRO_WorkOrderDetailTable.STARTDATE_FLD + " <= '" + dtmDueDate.ToString("G", DateTimeFormatInfo.CurrentInfo) + "'"
//								+ " AND " + PRO_WorkOrderBomDetailTable.COMPONENTID_FLD + "='" + voProduct.ProductID + "'";
							strExpression = PRO_WorkOrderDetailTable.STARTDATE_FLD
								+ " >= '" + dtmPrevDate.ToString("G") + "' AND "
								+ PRO_WorkOrderDetailTable.STARTDATE_FLD + " <= '" + dtmDueDate.ToString("G") + "'"
								+ " AND " + PRO_WorkOrderBomDetailTable.COMPONENTID_FLD + "='" + voProduct.ProductID + "'";
							drowDemandWorkOrders = pdtbDemandWOs.Select(strExpression);
						}
						if (drowDemandWorkOrders != null && drowDemandWorkOrders.Length > 0)
						{
							foreach (DataRow drowWOs in drowDemandWorkOrders)
							{
								dtbTempDemandWOs.ImportRow(drowWOs);
								try
								{
									decDemandWOs += decimal.Parse(drowWOs[Constants.DEMAND_QUANTITY_FLD].ToString());
								}
								catch{}
								// update demand quantity
								drowWOs[Constants.DEMAND_QUANTITY_FLD] = decimal.Zero;
							}
							pdtbDemandWOs.AcceptChanges();
						}

						#endregion

						#region Demand from parent CPO

						// find the parent CPO which have start date in range of plan date
						if (pdtbParentCPOs != null)
						{
							foreach (DataRow drowCPO in pdtbParentCPOs.Rows)
							{
								int intParentID = 0;
								try
								{
									intParentID = int.Parse(drowCPO[MTR_CPOTable.PRODUCTID_FLD].ToString());
								}
								catch{}
								// found the parent CPO
								if (intParentID == pintParentID)
								{
									DateTime dtmStartDate = dtmDueDate;
									try
									{
										dtmStartDate = (DateTime) drowCPO[MTR_CPOTable.STARTDATE_FLD];
									}
									catch{}
									// now find extracly the parent CPO which have demand today
									if (dtmStartDate > dtmPrevDate && dtmStartDate <= dtmDueDate)
									{
										voParentCPO = new MTR_CPOVO();
										voParentCPO.ProductID = intParentID;
										voParentCPO.CPOID = long.Parse(drowCPO[MTR_CPOTable.CPOID_FLD].ToString());
										voParentCPO.StartDate = dtmStartDate;
										DataRow[] drowComponent = pdtbAllProducts.Select("ParentID = '" + pintParentID + "' AND ProductID = '" + voProduct.ProductID + "'");
										if (drowComponent.Length == 1)
										{
											// demand from parent's CPO
											decimal decRequiredQuantity = 0;
											try
											{
												decRequiredQuantity = decimal.Parse(drowComponent[0]["RequiredQuantity"].ToString());
											}
											catch{}
											try
											{
												decShrink = decimal.Parse(drowComponent[0][ITM_BOMTable.SHRINK_FLD].ToString());
											}
											catch{}
											try
											{
												decParentLTOffset = decimal.Parse(drowComponent[0][ITM_BOMTable.LEADTIMEOFFSET_FLD].ToString());
												decParentLTOffset = decParentLTOffset / 86400;
											}
											catch
											{
												decParentLTOffset = 0;
											}
											voParentCPO.Quantity = decimal.Parse(drowCPO[MTR_CPOTable.QUANTITY_FLD].ToString());
											if (decRequiredQuantity > 0)
												decDemandFromParentCPO += voParentCPO.Quantity * decRequiredQuantity;
											else
												decDemandFromParentCPO += voParentCPO.Quantity;
											if (decShrink > 0)
												decDemandFromParentCPO = decDemandFromParentCPO/(1 - decShrink/100);
										}
										else
											decDemandFromParentCPO += voParentCPO.Quantity;

										// mark this CPO is supplied
										//drowCPO[IS_SUPPLIED_COL] = true;
									}
								}
							} //for (int j = 0; j < parrCPOs.Count; j++)
						} //if (parrCPOs.Count > 0)

						#endregion

						// if today has no demand
						if (decSaleOrder + decDemandWOs + decDemandFromParentCPO <= 0)
						{
							// we wil generate CPO for demand from Safety Stock
							// if today is the last day of horizon
							if (!dtmDueDate.Equals(voMPSCycleOptionMaster.AsOfDate.AddDays(voMPSCycleOptionMaster.PlanHorizon)))
								continue;
						}

						#endregion

						#region Calculate Supplies

						// retrieve supply from work order of current day
						if (pdtbSupplyWOs != null && pdtbSupplyWOs.Rows.Count > 0)
						{
//							strExpression = PRO_WorkOrderDetailTable.DUEDATE_FLD
//								+ " >= '" + dtmPrevDate.ToString("G", DateTimeFormatInfo.CurrentInfo) + "' AND "
//								+ PRO_WorkOrderDetailTable.DUEDATE_FLD + " <= '" + dtmDueDate.ToString("G", DateTimeFormatInfo.CurrentInfo) + "'"
//								+ " AND " + PRO_WorkOrderDetailTable.PRODUCTID_FLD + "='" + voProduct.ProductID + "'";
							// get supply from all work order which have due date smaller than current date
							strExpression = PRO_WorkOrderDetailTable.DUEDATE_FLD + " < '" + dtmDueDate.ToString("G") + "'"
								+ " AND " + PRO_WorkOrderDetailTable.PRODUCTID_FLD + "='" + voProduct.ProductID + "'";
							drowSupplyWorkOrders = pdtbSupplyWOs.Select(strExpression);
						}
						// retrieve supply from purchase order of current day
						if (pdtbPOs != null && pdtbPOs.Rows.Count > 0)
						{
//							strExpression = PO_DeliveryScheduleTable.SCHEDULEDATE_FLD
//								+ " >= '" + dtmPrevDate.ToString("G", DateTimeFormatInfo.CurrentInfo) + "' AND "
//								+ PO_DeliveryScheduleTable.SCHEDULEDATE_FLD + " <= '" + dtmDueDate.ToString("G", DateTimeFormatInfo.CurrentInfo) + "'"
//								+ " AND " + PO_PurchaseOrderDetailTable.PRODUCTID_FLD + "='" + voProduct.ProductID + "'";
							// supply from purchase order which have due date smaller than current date
							strExpression = PO_DeliveryScheduleTable.SCHEDULEDATE_FLD + " < '" + dtmDueDate.ToString("G") + "'"
								+ " AND " + PO_PurchaseOrderDetailTable.PRODUCTID_FLD + "='" + voProduct.ProductID + "'";
							drowPurchaseOrders = pdtbPOs.Select(strExpression);
						}
						// total supply of day = Supply from WO + Supply from PO
						if (drowSupplyWorkOrders != null && drowSupplyWorkOrders.Length > 0)
						{
							foreach (DataRow drowWOs in drowSupplyWorkOrders)
							{
								try
								{
									decSupplyWOs += decimal.Parse(drowWOs[Constants.SUPPLY_QUANTITY_FLD].ToString());
								}
								catch{}
								// update supply quantity from data source
								//drowWOs[Constants.SUPPLY_QUANTITY_FLD] = decimal.Zero;
							}
							//pdtbSupplyWOs.AcceptChanges();
						}
						if (drowPurchaseOrders != null && drowPurchaseOrders.Length > 0)
						{
							foreach (DataRow drowPOs in drowPurchaseOrders)
							{
								try
								{
									decSupplyPOs += decimal.Parse(drowPOs[Constants.SUPPLY_QUANTITY_FLD].ToString());
								}
								catch{}
								// update supply quantity from data source
								//drowPOs[Constants.SUPPLY_QUANTITY_FLD] = decimal.Zero;
							}
							//pdtbPOs.AcceptChanges();
						}

						#endregion

						#region MPS Calculation
						// generated CPO for safety stock
						if (decNetAvailableQuantity < 0)
						{
							#region create new CPO for safety stock
							
							// create new CPO for safety stock
							DataRow drowNewCPO = pdtbCPOs.NewRow();
							drowNewCPO[MTR_CPOTable.PRODUCTID_FLD] = voProduct.ProductID;
							drowNewCPO[MTR_CPOTable.STOCKUMID_FLD] = voProduct.StockUMID;
							// Due date of child must equal to start date of parent
							drowNewCPO[MTR_CPOTable.DUEDATE_FLD] = dtmDueDate;
							// total demand of the day
							//Debug.WriteLine(decimal.Round(decSaleOrder + decDemandWOs, 5));
							drowNewCPO[MTR_CPOTable.DEMANDQUANTITY_FLD] = decimal.Round(voProduct.SafetyStock, DECIMAL_ROUND_NUMBER);
							// total supply of the day
							//drowNewCPO[MTR_CPOTable.SUPPLYQUANTITY_FLD] = decimal.Round(decSupplyPOs + decSupplyWOs + decRemainQuantity, 5);
							drowNewCPO[MTR_CPOTable.SUPPLYQUANTITY_FLD] = decimal.Round(decInventoryQuantity, DECIMAL_ROUND_NUMBER);
							// quantity need to be produce
							//decimal decQuantity = decimal.Round(Math.Abs(decNetAvailableQuantity/decYieldRate), DECIMAL_ROUND_NUMBER);
							decimal decQuantity = decimal.Round(Math.Abs(decNetAvailableQuantity), DECIMAL_ROUND_NUMBER);
							decQuantity = decQuantity / decYieldRate;
							if (decQuantity != decimal.Floor(decQuantity))
								decQuantity = decimal.Floor(decQuantity) + 1;
							drowNewCPO[MTR_CPOTable.QUANTITY_FLD] = decQuantity;
							// convert lead time from seconds to days
							// one day = 86400 seconds.
							decimal decDayToAdd = (voProduct.LTFixedTime + (voProduct.LTVariableTime * decQuantity) + voProduct.LTDocToStock
								+ voProduct.LTOrderPrepare + voProduct.LTShippingPrepare)/86400;
							//Debug.WriteLine(voProduct.Code + ":" + (decDayToAdd * 86400).ToString());
							// convert start date to valid working day
							DateTime dtmStartDate = ConvertWorkingDay(pdtbDayOfWeek, pdtbHolidays, dtmDueDate, decDayToAdd);
							drowNewCPO[MTR_CPOTable.STARTDATE_FLD] = dtmStartDate;
							drowNewCPO[MTR_CPOTable.ISSAFETYSTOCK_FLD] = true;
							// add start date to demand date array in order to use it if this CPO has child
							if (!arrDateTimes.Contains(dtmStartDate))
								arrDateTimes.Add(dtmStartDate);
							// put CPO to all generated CPO datatable
							pdtbCPOs.Rows.Add(drowNewCPO);
							// put CPO to parent CPO data table
							dtbParentCPOs.ImportRow(drowNewCPO);
							// reset variable
							decNetAvailableQuantity = 0;

							#endregion
						}

						// re-calculate NetAvailableQuantity of day = NetAvailableQuantity of previous day + supply - (demand / yield rate)
						decNetAvailableQuantity += (decSupplyPOs + decSupplyWOs * decYieldRate) - (decSaleOrder + decDemandWOs);
						// if we can supply
						if (decNetAvailableQuantity >= 0)
						{
							if (voParentCPO != null && voParentCPO.CPOID > 0
								&& (decNetAvailableQuantity - decDemandFromParentCPO) < 0)
							{
								#region Generate CPO for demand from parent CPO

								// add lead time offset of item in parent BOM to the due date of child
								DateTime dtmDueDateOfChild = voParentCPO.StartDate;
								dtmDueDateOfChild = ConvertWorkingDay(pdtbDayOfWeek, pdtbHolidays, dtmDueDateOfChild, decParentLTOffset);
								// create new row in CPO table
								DataRow drowNewCPO = pdtbCPOs.NewRow();
								drowNewCPO[MTR_CPOTable.PRODUCTID_FLD] = voProduct.ProductID;
								drowNewCPO[MTR_CPOTable.STOCKUMID_FLD] = voProduct.StockUMID;
								drowNewCPO[MTR_CPOTable.PARENTCPOID_FLD] = voParentCPO.CPOID;
								// Due date of child must equal to start date of parent
								drowNewCPO[MTR_CPOTable.DUEDATE_FLD] = dtmDueDateOfChild;
								// total demand of the day
								//Debug.WriteLine(decimal.Round(decDemandFromParentCPO + decSaleOrder + decDemandWOs, 5));
								drowNewCPO[MTR_CPOTable.DEMANDQUANTITY_FLD] = decimal.Round(decDemandFromParentCPO + decSaleOrder + decDemandWOs + voProduct.SafetyStock, DECIMAL_ROUND_NUMBER);
								// total supply of the day
								//drowNewCPO[MTR_CPOTable.SUPPLYQUANTITY_FLD] = decimal.Round(decSupplyPOs + decSupplyWOs + decRemainQuantity, 5);
								drowNewCPO[MTR_CPOTable.SUPPLYQUANTITY_FLD] = decimal.Round(decSupplyPOs + decSupplyWOs + decInventoryQuantity, DECIMAL_ROUND_NUMBER);
								// quantity need to be produce
								decimal decQuantity = decimal.Round(decDemandFromParentCPO - decNetAvailableQuantity, DECIMAL_ROUND_NUMBER);
								decQuantity = decQuantity / decYieldRate;
								if (decQuantity != decimal.Floor(decQuantity))
									decQuantity = decimal.Floor(decQuantity) + 1;
								drowNewCPO[MTR_CPOTable.QUANTITY_FLD] = decQuantity;
								// convert lead time from seconds to days
								// one day = 86400 seconds.
								decimal decDayToAdd = (voProduct.LTFixedTime + (voProduct.LTVariableTime * decQuantity) + voProduct.LTDocToStock
									+ voProduct.LTOrderPrepare + voProduct.LTShippingPrepare)/86400;
								//Debug.WriteLine(decDayToAdd);
								//Debug.WriteLine(voProduct.Code + ":" + (decDayToAdd * 86400).ToString());
								// number of day to add to the due date
								drowNewCPO[DAY_TO_ADD_COL] = decDayToAdd;
								// convert start date to valid working day
								DateTime dtmStartDate = ConvertWorkingDay(pdtbDayOfWeek, pdtbHolidays, dtmDueDateOfChild, decDayToAdd);
								drowNewCPO[MTR_CPOTable.STARTDATE_FLD] = dtmStartDate;
								// add start date to demand date array in order to use it if this CPO has child
								if (!arrDateTimes.Contains(dtmStartDate))
									arrDateTimes.Add(dtmStartDate);
								// put CPO to all generated CPO datatable
								pdtbCPOs.Rows.Add(drowNewCPO);
								// put CPO to parent CPO data table
								dtbParentCPOs.ImportRow(drowNewCPO);
								// reset available quantity
								decNetAvailableQuantity = 0;

								#endregion
							}
							else
							{
								decNetAvailableQuantity -= (decSupplyPOs + decSupplyWOs);
								decNetAvailableQuantity = (decNetAvailableQuantity < 0) ? decimal.Zero : decNetAvailableQuantity;
								dtmPrevDate = dtmDueDate;
							}
						}
						else // if NetAvailableQuantity < 0, we generate new CPO
						{
							decNetAvailableQuantity -= decDemandWOs;
							// we need to re-calculate the demand from work order
							// add shrink to demand
							if (dtbTempDemandWOs != null)
							{
								foreach (DataRow drowWO in dtbTempDemandWOs.Rows)
								{
									#region demand quantity
									decimal decShrinkWO = 0;
									try
									{
										decDemandWOs = decimal.Parse(drowWO[Constants.DEMAND_QUANTITY_FLD].ToString().Trim());
									}
									catch{}
									try
									{
										decShrinkWO = decimal.Parse(drowWO[PRO_WorkOrderBomDetailTable.SHRINK_FLD].ToString().Trim());
									}
									catch{}
									if (decShrinkWO > 0)
										decDemandWOs += decDemandWOs/(1 - decShrinkWO/100);
									#endregion
								}
							}
							decNetAvailableQuantity += decDemandWOs;
							#region Generate CPO for demand from SOs, WOs

							DataRow drowNewCPO = pdtbCPOs.NewRow();
							drowNewCPO[MTR_CPOTable.PRODUCTID_FLD] = voProduct.ProductID;
							drowNewCPO[MTR_CPOTable.STOCKUMID_FLD] = voProduct.StockUMID;
							// Due date of child must equal to start date of parent
							drowNewCPO[MTR_CPOTable.DUEDATE_FLD] = dtmDueDate;
							// total demand of the day
							//Debug.WriteLine(decimal.Round(decSaleOrder + decDemandWOs, 5));
							drowNewCPO[MTR_CPOTable.DEMANDQUANTITY_FLD] = decimal.Round(decSaleOrder + decDemandWOs, DECIMAL_ROUND_NUMBER);
							// total supply of the day
							//drowNewCPO[MTR_CPOTable.SUPPLYQUANTITY_FLD] = decimal.Round(decSupplyPOs + decSupplyWOs + decRemainQuantity, 5);
							drowNewCPO[MTR_CPOTable.SUPPLYQUANTITY_FLD] = decimal.Round(decSupplyPOs + decSupplyWOs + decInventoryQuantity, DECIMAL_ROUND_NUMBER);
							// quantity need to be produce
							//decimal decQuantity = decimal.Round(Math.Abs(decNetAvailableQuantity/decYieldRate), DECIMAL_ROUND_NUMBER);
							decimal decQuantity = decimal.Round(Math.Abs(decNetAvailableQuantity), DECIMAL_ROUND_NUMBER);
							decQuantity = decQuantity / decYieldRate;
							if (decQuantity != decimal.Floor(decQuantity))
								decQuantity = decimal.Floor(decQuantity) + 1;
							drowNewCPO[MTR_CPOTable.QUANTITY_FLD] = decQuantity;
							// convert lead time from seconds to days
							// one day = 86400 seconds.
							decimal decDayToAdd = (voProduct.LTFixedTime + (voProduct.LTVariableTime * decQuantity) + voProduct.LTDocToStock
								+ voProduct.LTOrderPrepare + voProduct.LTShippingPrepare)/86400;
							//Debug.WriteLine(voProduct.Code + ":" + (decDayToAdd * 86400).ToString());
							// convert start date to valid working day
							DateTime dtmStartDate = ConvertWorkingDay(pdtbDayOfWeek, pdtbHolidays, dtmDueDate, decDayToAdd);
							drowNewCPO[MTR_CPOTable.STARTDATE_FLD] = dtmStartDate;
							// add start date to demand date array in order to use it if this CPO has child
							if (!arrDateTimes.Contains(dtmStartDate))
								arrDateTimes.Add(dtmStartDate);
							// put CPO to all generated CPO datatable
							pdtbCPOs.Rows.Add(drowNewCPO);
							// put CPO to parent CPO data table
							dtbParentCPOs.ImportRow(drowNewCPO);

							#endregion

							#region Generate CPO for demand from parent CPO

							if (voParentCPO != null && voParentCPO.CPOID > 0
								&& decDemandFromParentCPO > decimal.Zero)
							{
								// add lead time offset of item in parent BOM to the due date of child
								DateTime dtmDueDateOfChild = voParentCPO.StartDate;
								dtmDueDateOfChild = ConvertWorkingDay(pdtbDayOfWeek, pdtbHolidays, dtmDueDateOfChild, decParentLTOffset);
								// create new row in CPO table
								DataRow drowNewCPOForParent = pdtbCPOs.NewRow();
								drowNewCPOForParent[MTR_CPOTable.PRODUCTID_FLD] = voProduct.ProductID;
								drowNewCPOForParent[MTR_CPOTable.STOCKUMID_FLD] = voProduct.StockUMID;
								drowNewCPOForParent[MTR_CPOTable.PARENTCPOID_FLD] = voParentCPO.CPOID;
								// Due date of child must equal to start date of parent
								drowNewCPOForParent[MTR_CPOTable.DUEDATE_FLD] = dtmDueDateOfChild;
								//Debug.WriteLine(decimal.Round(decDemandFromParentCPO, DECIMAL_ROUND_NUMBER));
								// total demand of the day
								drowNewCPOForParent[MTR_CPOTable.DEMANDQUANTITY_FLD] = decimal.Round(decDemandFromParentCPO, DECIMAL_ROUND_NUMBER);
								// total supply of the day
								drowNewCPOForParent[MTR_CPOTable.SUPPLYQUANTITY_FLD] = decimal.Zero;
								// quantity need to be produce
								decQuantity = decimal.Round(decDemandFromParentCPO, DECIMAL_ROUND_NUMBER);
								decQuantity = decQuantity / decYieldRate;
								if (decQuantity != decimal.Floor(decQuantity))
									decQuantity = decimal.Floor(decQuantity) + 1;
								drowNewCPOForParent[MTR_CPOTable.QUANTITY_FLD] = decQuantity;
								// convert lead time from seconds to days
								// one day = 86400 seconds.
								decDayToAdd = (voProduct.LTFixedTime + (voProduct.LTVariableTime * decQuantity) + voProduct.LTDocToStock
									+ voProduct.LTOrderPrepare + voProduct.LTShippingPrepare)/86400;
								//Debug.WriteLine(voProduct.Code + ":" + (decDayToAdd * 86400).ToString());
								drowNewCPO[DAY_TO_ADD_COL] = decDayToAdd;
								// convert start date to valid working day
								dtmStartDate = ConvertWorkingDay(pdtbDayOfWeek, pdtbHolidays, dtmDueDateOfChild, decDayToAdd);
								drowNewCPOForParent[MTR_CPOTable.STARTDATE_FLD] = dtmStartDate;
								// add start date to demand date array in order to use it if this CPO has child
								if (!arrDateTimes.Contains(dtmStartDate))
									arrDateTimes.Add(dtmStartDate);
								// put CPO to datatable in order to use as Parent CPO
								pdtbCPOs.Rows.Add(drowNewCPOForParent);
								// put CPO to parent CPO data table
								dtbParentCPOs.ImportRow(drowNewCPOForParent);
							}

							#endregion

							// reset remain quantity for next day
							//decRemainQuantity = 0;
							// reset available quantity
							decNetAvailableQuantity = 0;
						}
						decDemandOfDay += decSaleOrder + decDemandWOs + decDemandFromParentCPO;

						#endregion

						#region update supply quantity

						// get quantity from inventory to supply first
						if (decDemandOfDay > 0)
						{
							if (drowInventory != null)
							{
								foreach (DataRow drowNew in drowInventory)
								{
									decimal decQuantity = 0;
									try
									{
										decQuantity = decimal.Parse(drowNew[Constants.SUPPLY_QUANTITY_FLD].ToString());
									}
									catch{}
									if (decQuantity <= 0)
										continue;
									decDemandOfDay -= decQuantity;
									drowNew[Constants.SUPPLY_QUANTITY_FLD] = (decDemandOfDay > decimal.Zero) ? decimal.Zero : Math.Abs(decDemandOfDay);
								}
							}
						}
						// if quantity from inventory is not enough to supply
						// get quantity from work order to supply
						if (decDemandOfDay > 0)
						{
							if (drowSupplyWorkOrders != null)
							{
								foreach (DataRow drowNew in drowSupplyWorkOrders)
								{
									decimal decQuantity = 0;
									try
									{
										decQuantity = decimal.Parse(drowNew[Constants.SUPPLY_QUANTITY_FLD].ToString());
									}
									catch{}
									if (decQuantity <= 0)
										continue;
									decDemandOfDay -= decQuantity;
									drowNew[Constants.SUPPLY_QUANTITY_FLD] = (decDemandOfDay > decimal.Zero) ? decimal.Zero : Math.Abs(decDemandOfDay);
								}
							}
						}
						// if quantity from work order is not enough to supply
						// get quantity from purchase order to supply
						if (decDemandOfDay > 0)
						{
							if (drowPurchaseOrders != null)
							{
								foreach (DataRow drowNew in drowPurchaseOrders)
								{
									decimal decQuantity = 0;
									try
									{
										decQuantity = decimal.Parse(drowNew[Constants.SUPPLY_QUANTITY_FLD].ToString());
									}
									catch{}
									if (decQuantity <= 0)
										continue;
									decDemandOfDay -= decQuantity;
									drowNew[Constants.SUPPLY_QUANTITY_FLD] = (decDemandOfDay > decimal.Zero) ? decimal.Zero : Math.Abs(decDemandOfDay);
								}
							}
						}
						// update DataTable
						if (pdtbInventory != null)
							pdtbInventory.AcceptChanges();
						if (pdtbSupplyWOs != null)
							pdtbSupplyWOs.AcceptChanges();
						if (pdtbPOs != null)
							pdtbPOs.AcceptChanges();

						#endregion

						dtmPrevDate = dtmDueDate;
					} // foreach (DateTime dtmDueDate in parrDateTimes)

					#endregion

				} // if (voProduct.PlanType.Equals((int)PlanTypeEnum.MPS))
				// if current product has component(s)
				if (drowComponents.Length > 0)
				{
					arrDateTimes.Sort();
					arrDateTimes.TrimToSize();
					// we will generate plan for all components of current product
					// with new demand date time array which contain the demand from parent CPO
					GeneratePlanOffline(pdtbAllProducts, drowComponents, pintMasterLocationID, arrDateTimes,
						pobjMPSCycleOptionMaster, voProduct.ProductID, pdtbInventory, pdtbSaleOrders,
						pdtbDemandWOs, pdtbSupplyWOs, pdtbPOs, pdtbCPOs, pdtbDayOfWeek, pdtbHolidays,
						pdtbFutureSOs, pdtbFuturePOs, pdtbFutureSupplyWOs, pdtbFutureDemandWOs, dtbParentCPOs, pblnSafetyStock);
				}
			} // foreach (DataRow drowItem in pdtbItems.Rows)
			return pdtbCPOs;
		}

		/// <summary>
		/// Convert working day with offline method
		/// </summary>
		/// <param name="pdtbDayOfWeek">Valid work day of week</param>
		/// <param name="pdtbHolidays">Holidays in year</param>
		/// <param name="pdtmDate">Date to convert</param>
		/// <param name="pdecNumberOfDay">Number of day to add/subtract</param>
		/// <returns>Converted Date</returns>
		private DateTime ConvertWorkingDay(DataTable pdtbDayOfWeek, DataTable pdtbHolidays, 
			DateTime pdtmDate, decimal pdecNumberOfDay)
		{
			int intNumberOfDay = (int) decimal.Floor(pdecNumberOfDay);
			double dblRemainder = (double) (pdecNumberOfDay - (decimal) intNumberOfDay);

			ArrayList arrDayOfWeek = new ArrayList();
			if (pdtbDayOfWeek != null)
			{
				DataRow[] drowWorkingDay = pdtbDayOfWeek.Select(MST_WorkingDayMasterTable.YEAR_FLD + "='" + pdtmDate.Year + "'");
				if (drowWorkingDay.Length != 0)
				{
					DataRow drow = drowWorkingDay[0];

					if (!bool.Parse(drow[MST_WorkingDayMasterTable.MON_FLD].ToString()))
					{
						arrDayOfWeek.Add(DayOfWeek.Monday);
					}

					if (!bool.Parse(drow[MST_WorkingDayMasterTable.TUE_FLD].ToString()))
					{
						arrDayOfWeek.Add(DayOfWeek.Tuesday);
					}

					if (!bool.Parse(drow[MST_WorkingDayMasterTable.WED_FLD].ToString()))
					{
						arrDayOfWeek.Add(DayOfWeek.Wednesday);
					}

					if (!bool.Parse(drow[MST_WorkingDayMasterTable.THU_FLD].ToString()))
					{
						arrDayOfWeek.Add(DayOfWeek.Thursday);
					}

					if (!bool.Parse(drow[MST_WorkingDayMasterTable.FRI_FLD].ToString()))
					{
						arrDayOfWeek.Add(DayOfWeek.Friday);
					}

					if (!bool.Parse(drow[MST_WorkingDayMasterTable.SAT_FLD].ToString()))
					{
						arrDayOfWeek.Add(DayOfWeek.Saturday);
					}

					if (!bool.Parse(drow[MST_WorkingDayMasterTable.SUN_FLD].ToString()))
					{
						arrDayOfWeek.Add(DayOfWeek.Sunday);
					}
				}
			}

			ArrayList arrHolidays = new ArrayList();
			if (pdtbHolidays != null)
			{
				DataRow[] drowHoliday = pdtbHolidays.Select(MST_WorkingDayMasterTable.YEAR_FLD + "='" + pdtmDate.Year + "'");
				if (drowHoliday.Length != 0)
				{
					// have data--> create new array list to add items
					for (int i = 0; i < drowHoliday.Length; i++)
					{
						DateTime dtmTemp = DateTime.Parse(pdtbHolidays.Rows[i][MST_WorkingDayDetailTable.OFFDAY_FLD].ToString());
						// truncate hour, minute, second
						dtmTemp = new DateTime(dtmTemp.Year, dtmTemp.Month, dtmTemp.Day);
						arrHolidays.Add(dtmTemp);
					}
				}
			}

			DateTime dtmConvert = pdtmDate;
			for(int i =0; i < intNumberOfDay; i++)
			{							
				// 
				dtmConvert = dtmConvert.AddDays(-1);

				// goto next day if the day is holidayday
				while(arrHolidays.Contains(new DateTime(dtmConvert.Year, dtmConvert.Month, dtmConvert.Day)))
				{
					dtmConvert = dtmConvert.AddDays(-1);
				}

				// goto next day if the day is off day
				while(arrDayOfWeek.Contains( dtmConvert.DayOfWeek))
				{
					dtmConvert = dtmConvert.AddDays(-1);
					if(arrHolidays.Contains(new DateTime(dtmConvert.Year, dtmConvert.Month, dtmConvert.Day)))
					{
						dtmConvert = dtmConvert.AddDays(-1);
					}
				}

				// goto next day if the day is holiday
				while(arrHolidays.Contains(new DateTime(dtmConvert.Year, dtmConvert.Month, dtmConvert.Day)))
				{
					dtmConvert = dtmConvert.AddDays(-1);
				}
			}
						
			// Add remainder
			dtmConvert = dtmConvert.AddDays(-dblRemainder);

			// goto next day if the day is holidayday
			while(arrHolidays.Contains(new DateTime(dtmConvert.Year, dtmConvert.Month, dtmConvert.Day)))
			{
				dtmConvert = dtmConvert.AddDays(-1);
			}
						
			// goto next day if the day is off day
			while(arrDayOfWeek.Contains(dtmConvert.DayOfWeek))
			{
				dtmConvert = dtmConvert.AddDays(-1);
				if(arrHolidays.Contains(new DateTime(dtmConvert.Year, dtmConvert.Month, dtmConvert.Day)))
				{
					dtmConvert = dtmConvert.AddDays(-1);
				}
			}
						
			// goto next day if the day is holidayday
			while(arrHolidays.Contains(new DateTime(dtmConvert.Year, dtmConvert.Month, dtmConvert.Day)))
			{
				dtmConvert = dtmConvert.AddDays(-1);
			}

			return dtmConvert;
		}

		/// <summary>
		/// Puts product information from DataRow to VO
		/// </summary>
		/// <param name="pdrowProductData">Product Data</param>
		/// <returns>Product VO</returns>
		private ITM_ProductVO GetProductInfo(DataRow pdrowProductData)
		{
			ITM_ProductVO voProduct = new ITM_ProductVO();
			voProduct.ProductID = int.Parse(pdrowProductData[ITM_ProductTable.PRODUCTID_FLD].ToString());
			voProduct.StockUMID = int.Parse(pdrowProductData[ITM_ProductTable.STOCKUMID_FLD].ToString());
			try
			{
				voProduct.PlanType = int.Parse(pdrowProductData[ITM_ProductTable.PLANTYPE_FLD].ToString());
			}
			catch
			{
			}
			try
			{
				voProduct.ScrapPercent = decimal.Parse(pdrowProductData[ITM_ProductTable.SCRAPPERCENT_FLD].ToString());
				if (voProduct.ScrapPercent < 0)
					voProduct.ScrapPercent = decimal.Zero;
			}
			catch
			{
			}
			try
			{
				voProduct.SafetyStock = decimal.Parse(pdrowProductData[ITM_ProductTable.SAFETYSTOCK_FLD].ToString());
				if (voProduct.ScrapPercent < 0)
					voProduct.ScrapPercent = decimal.Zero;
			}
			catch
			{
			}
			try
			{
				voProduct.LTFixedTime = decimal.Parse(pdrowProductData[ITM_ProductTable.LTFIXEDTIME_FLD].ToString());
				if (voProduct.LTFixedTime < 0)
					voProduct.LTFixedTime = decimal.Zero;
			}
			catch
			{
			}
			try
			{
				voProduct.LTVariableTime = decimal.Parse(pdrowProductData[ITM_ProductTable.LTVARIABLETIME_FLD].ToString());
				if (voProduct.LTVariableTime < 0)
					voProduct.LTVariableTime = decimal.Zero;
			}
			catch
			{
			}
			try
			{
				voProduct.LTDocToStock = decimal.Parse(pdrowProductData[ITM_ProductTable.LTDOCKTOSTOCK_FLD].ToString());
				if (voProduct.LTDocToStock < 0)
					voProduct.LTDocToStock = decimal.Zero;
			}
			catch
			{
			}
			try
			{
				voProduct.LTOrderPrepare = decimal.Parse(pdrowProductData[ITM_ProductTable.LTORDERPREPARE_FLD].ToString());
				if (voProduct.LTOrderPrepare < 0)
					voProduct.LTOrderPrepare = decimal.Zero;
			}
			catch
			{
			}
			try
			{
				voProduct.LTShippingPrepare = decimal.Parse(pdrowProductData[ITM_ProductTable.LTSHIPPINGPREPARE_FLD].ToString());
				if (voProduct.LTShippingPrepare < 0)
					voProduct.LTShippingPrepare = decimal.Zero;
			}
			catch
			{
			}
			try
			{
				voProduct.LTSalesATP = decimal.Parse(pdrowProductData[ITM_ProductTable.LTSALESATP_FLD].ToString());
				if (voProduct.LTSalesATP < 0)
					voProduct.LTSalesATP = decimal.Zero;
			}
			catch
			{
			}
			return voProduct;
		}

		#endregion
	}
}