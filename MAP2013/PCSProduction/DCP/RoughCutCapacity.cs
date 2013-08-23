using System;
using System.ComponentModel;
using System.Data;
using System.Collections;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using PCSComProduction.DCP.BO;
using PCSComProduction.DCP.DS;
using PCSComUtils.Common;
using PCSComUtils.Common.BO;
using PCSComUtils.PCSExc;
using PCSUtils.Log;
using PCSUtils.Utils;
using ThreadState = System.Threading.ThreadState;

namespace PCSProduction.DCP
{
	/// <summary>
	/// Summary description for MPSRegenerationProcess.
	/// </summary>
	public class RoughCutCapacity : System.Windows.Forms.Form
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		private const string THIS = "PCSProduction.DCP.RoughCutCapacity";
		const string STOCK_FLD = "Stock";
		const string CAPACITY_FLD = "Capacity";
		const string RATE_FLD = "Rate";
		const string PLANQTY_FLD = "PlanQty";
		private UtilsBO boUtils;
		private RoughCutCapacityBO boRoughCut;
		Thread thrProcess = null;

		#region Members
		
		int intProductionLineID = 0;
		int intCCNID = 0; 
		int intDCOptionMasterID = 0;
		private C1.Win.C1List.C1Combo cboCCN;
		private System.Windows.Forms.Button btnClose;
		private System.Windows.Forms.Button btnHelp;
		private System.Windows.Forms.Label lblCCN;
		private System.Windows.Forms.Label lblProcessing;
		private System.Windows.Forms.PictureBox picProcessing;
		private System.Windows.Forms.Button btnSearch;
		private System.Windows.Forms.TextBox txtProductionLine;
		private System.Windows.Forms.Label lblProductionLine;
		private System.Windows.Forms.Button btnCycle;
		private System.Windows.Forms.TextBox txtCycle;
		private System.Windows.Forms.Label lblCycle;
		private System.Windows.Forms.Button btnProcess;
		#endregion

		public RoughCutCapacity()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
		}

		/// <summary>
		/// Initialize form with a specified MPS Cycle Option Master
		/// </summary>
		public RoughCutCapacity(int pintProductionLineID)
		{
			InitializeComponent();
			intProductionLineID = pintProductionLineID;
		}

		public RoughCutCapacity(int pintProductionLineID, int pintCycleID)
		{
			InitializeComponent();
			intProductionLineID = pintProductionLineID;
			intDCOptionMasterID = pintCycleID;
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(RoughCutCapacity));
			this.btnSearch = new System.Windows.Forms.Button();
			this.txtProductionLine = new System.Windows.Forms.TextBox();
			this.cboCCN = new C1.Win.C1List.C1Combo();
			this.btnClose = new System.Windows.Forms.Button();
			this.btnHelp = new System.Windows.Forms.Button();
			this.lblProductionLine = new System.Windows.Forms.Label();
			this.lblCCN = new System.Windows.Forms.Label();
			this.btnProcess = new System.Windows.Forms.Button();
			this.lblProcessing = new System.Windows.Forms.Label();
			this.picProcessing = new System.Windows.Forms.PictureBox();
			this.btnCycle = new System.Windows.Forms.Button();
			this.txtCycle = new System.Windows.Forms.TextBox();
			this.lblCycle = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.cboCCN)).BeginInit();
			this.SuspendLayout();
			// 
			// btnSearch
			// 
			this.btnSearch.AccessibleDescription = resources.GetString("btnSearch.AccessibleDescription");
			this.btnSearch.AccessibleName = resources.GetString("btnSearch.AccessibleName");
			this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnSearch.Anchor")));
			this.btnSearch.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSearch.BackgroundImage")));
			this.btnSearch.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnSearch.Dock")));
			this.btnSearch.Enabled = ((bool)(resources.GetObject("btnSearch.Enabled")));
			this.btnSearch.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnSearch.FlatStyle")));
			this.btnSearch.Font = ((System.Drawing.Font)(resources.GetObject("btnSearch.Font")));
			this.btnSearch.Image = ((System.Drawing.Image)(resources.GetObject("btnSearch.Image")));
			this.btnSearch.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnSearch.ImageAlign")));
			this.btnSearch.ImageIndex = ((int)(resources.GetObject("btnSearch.ImageIndex")));
			this.btnSearch.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnSearch.ImeMode")));
			this.btnSearch.Location = ((System.Drawing.Point)(resources.GetObject("btnSearch.Location")));
			this.btnSearch.Name = "btnSearch";
			this.btnSearch.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnSearch.RightToLeft")));
			this.btnSearch.Size = ((System.Drawing.Size)(resources.GetObject("btnSearch.Size")));
			this.btnSearch.TabIndex = ((int)(resources.GetObject("btnSearch.TabIndex")));
			this.btnSearch.Text = resources.GetString("btnSearch.Text");
			this.btnSearch.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnSearch.TextAlign")));
			this.btnSearch.Visible = ((bool)(resources.GetObject("btnSearch.Visible")));
			this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
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
			this.txtProductionLine.Leave += new System.EventHandler(this.txtProductionLine_Leave);
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
				" 116, 156</ClientRect><VScrollBar><Width>15</Width></VScrollBar><HScrollBar><Hei" +
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
				"th>16</DefaultRecSelWidth></Blob>";
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
			// RoughCutCapacity
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
			this.Controls.Add(this.btnSearch);
			this.Controls.Add(this.txtProductionLine);
			this.Controls.Add(this.txtCycle);
			this.Controls.Add(this.cboCCN);
			this.Controls.Add(this.btnClose);
			this.Controls.Add(this.btnHelp);
			this.Controls.Add(this.lblProductionLine);
			this.Controls.Add(this.lblCCN);
			this.Controls.Add(this.btnProcess);
			this.Controls.Add(this.lblProcessing);
			this.Controls.Add(this.picProcessing);
			this.Controls.Add(this.btnCycle);
			this.Controls.Add(this.lblCycle);
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
			this.Name = "RoughCutCapacity";
			this.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("$this.RightToLeft")));
			this.StartPosition = ((System.Windows.Forms.FormStartPosition)(resources.GetObject("$this.StartPosition")));
			this.Text = resources.GetString("$this.Text");
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.RoughCutCapacity_KeyDown);
			this.Closing += new System.ComponentModel.CancelEventHandler(this.RoughCutCapacity_Closing);
			this.Load += new System.EventHandler(this.RoughCutCapacity_Load);
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
		private void RoughCutCapacity_Load(object sender, System.EventArgs e)
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
				boRoughCut = new RoughCutCapacityBO();
				lblProcessing.Visible = false;
				picProcessing.Visible = false;
				// - Load CCN and set default value
				FormControlComponents.PutDataIntoC1ComboBox(cboCCN, boUtils.ListCCN().Tables[0], MST_CCNTable.CODE_FLD, MST_CCNTable.CCNID_FLD, MST_CCNTable.TABLE_NAME);
				cboCCN.SelectedValue = SystemProperty.CCNID;
				if (intProductionLineID > 0)
				{
					DataRowView drwResult = FormControlComponents.OpenSearchForm(PRO_ProductionLineTable.TABLE_NAME, PRO_ProductionLineTable.PRODUCTIONLINEID_FLD, intProductionLineID.ToString(), null, false);
					txtProductionLine.Text = drwResult[PRO_ProductionLineTable.CODE_FLD].ToString();
					ArrayList arrID = new ArrayList();
					arrID.Add(intProductionLineID);
					txtProductionLine.Tag = arrID;
				}
				if (intDCOptionMasterID > 0)
				{
					DataRowView drwResult = FormControlComponents.OpenSearchForm(PRO_DCOptionMasterTable.TABLE_NAME, PRO_DCOptionMasterTable.DCOPTIONMASTERID_FLD, intDCOptionMasterID.ToString(), null, false);
					txtCycle.Text = drwResult[PRO_DCOptionMasterTable.CYCLE_FLD].ToString();
					txtCycle.Tag = intDCOptionMasterID;
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
		private void btnSearch_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnCycle_Click()";
			try
			{
				DataTable dtbData = null;
				if (sender is TextBox && sender != null)
					dtbData = FormControlComponents.OpenSearchFormForMultiSelectedRow(PRO_ProductionLineTable.TABLE_NAME, PRO_ProductionLineTable.CODE_FLD, txtProductionLine.Text.Trim(), null, false);
				else
					dtbData = FormControlComponents.OpenSearchFormForMultiSelectedRow(PRO_ProductionLineTable.TABLE_NAME, PRO_ProductionLineTable.CODE_FLD, txtProductionLine.Text.Trim(), null, true);
				if (dtbData != null && dtbData.Rows.Count > 0)
				{
					StringBuilder sbCode = new StringBuilder();
					ArrayList arrID = new ArrayList();
					foreach (DataRow drowData in dtbData.Rows)
					{
						sbCode.Append(drowData[PRO_ProductionLineTable.CODE_FLD].ToString()).Append(",");
						arrID.Add(Convert.ToInt32(drowData[PRO_ProductionLineTable.PRODUCTIONLINEID_FLD]));
					}
					txtProductionLine.Text = sbCode.ToString(0, sbCode.Length - 1);
					txtProductionLine.Tag = arrID;
				}
				else
				{
					txtProductionLine.Focus();
					txtProductionLine.SelectAll();
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
				if (txtProductionLine.Tag == null || txtProductionLine.Text.Trim() == string.Empty)
				{
					string[] msg = new string[]{lblProductionLine.Text};
					PCSMessageBox.Show(ErrorCode.MESSAGE_MANDATORY_FIELD_REQUIRED, MessageBoxIcon.Warning, msg);
                    txtProductionLine.Focus();
					return;
				}
				if(txtCycle.Text.Trim() == string.Empty)
				{
					// Input cycle 
					PCSMessageBox.Show(ErrorCode.MESSAGE_DCP_SELECT_CYCLE);
					txtCycle.Focus();
					return;
				}
				if(txtCycle.Tag != null)
					intDCOptionMasterID = Convert.ToInt32(txtCycle.Tag);
				DateTime dtmCurrentDate = (new PCSComUtils.Common.BO.UtilsBO()).GetDBDate().Date ;
				DataRow drowOption = (new PCSComProduction.DCP.BO.DCOptionsBO()).GetDCOptionMaster(intDCOptionMasterID);
				DateTime dtmAsOfDate = Convert.ToDateTime(drowOption[MTR_MPSCycleOptionMasterTable.ASOFDATE_FLD]);
				if (dtmAsOfDate <= dtmCurrentDate)
				{
					PCSMessageBox.Show(ErrorCode.MESSAGE_DCP_ASOFDATE_MUST_IN_FUTURE, MessageBoxIcon.Error);
					txtCycle.Focus();
					return;
				}
				
				lblCCN.Visible = false;
				cboCCN.Visible = false;
				lblProductionLine.Visible = false;
				txtProductionLine.Visible = false;
				btnSearch.Visible = false;
				lblCycle.Visible = false;
				txtCycle.Visible = false;
				btnCycle.Visible = false;
				btnProcess.Enabled = false;
				lblProcessing.Visible = true;
				lblProcessing.Text = string.Format("{0} - {1}", lblProcessing.Text, this.Text);
				picProcessing.Visible = true;
				
				thrProcess = new Thread(new ThreadStart(RoughCut));
				thrProcess.Start();
				if (thrProcess.ThreadState == ThreadState.Stopped || !thrProcess.IsAlive)
					thrProcess = null;
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
				txtProductionLine.Text = string.Empty;
				txtProductionLine.Tag = null;
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
		private void RoughCutCapacity_Closing(object sender, CancelEventArgs e)
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
		private void txtProductionLine_Leave(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtCycle_Leave()";
			try
			{
				if (txtProductionLine.Modified)
				{
					if (txtProductionLine.Text != string.Empty)
						btnSearch_Click(sender, e);
					else
						txtProductionLine.Tag = null;
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
		private void txtProductionLine_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
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

		private void RoughCutCapacity_KeyDown(object sender, KeyEventArgs e)
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

		private void btnCycle_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnCycle_Click()";
			try
			{
				DataRowView drwResult = null;
				// Hashtable htbCondition = new Hashtable();
				if (cboCCN.SelectedValue != null)
				{
					intCCNID = int.Parse(cboCCN.SelectedValue.ToString());
				}
				else
				{
					PCSMessageBox.Show(ErrorCode.MESSAGE_RGA_CCN, MessageBoxIcon.Warning);
					cboCCN.Focus();
					return;
				}
				Hashtable hshCondition = new Hashtable();
				hshCondition.Add(MST_CCNTable.CCNID_FLD, intCCNID);
				drwResult = FormControlComponents.OpenSearchForm(PRO_DCOptionMasterTable.TABLE_NAME,PRO_DCOptionMasterTable.CYCLE_FLD,txtCycle.Text,hshCondition);
				if (drwResult != null)
				{
					txtCycle.Text = drwResult[PRO_DCOptionMasterTable.CYCLE_FLD].ToString();
					txtCycle.Tag = drwResult[PRO_DCOptionMasterTable.DCOPTIONMASTERID_FLD].ToString();
				}
				else
				{
					txtCycle.Focus();
					txtCycle.SelectAll();
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

		private void txtCycle_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if ((e.KeyCode == Keys.F4) && (btnCycle.Enabled))
			{
				btnCycle_Click(sender,e);
			}
		}

		private void txtCycle_Leave(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtCycle_Leave()";
			try
			{
				if (!txtCycle.Modified || txtCycle.Text.Trim() == string.Empty)
					return;
				Hashtable htbCriterial = new Hashtable();
				if (cboCCN.SelectedValue == null)
				{
					PCSMessageBox.Show(ErrorCode.MESSAGE_RGA_CCN, MessageBoxIcon.Warning);
					cboCCN.Focus();
					return;
				}
				else
					htbCriterial.Add(PRO_DCOptionMasterTable.CCNID_FLD, cboCCN.SelectedValue);
				DataRowView drvResult = FormControlComponents.OpenSearchForm(PRO_DCOptionMasterTable.TABLE_NAME, PRO_DCOptionMasterTable.CYCLE_FLD, txtCycle.Text.Trim(), htbCriterial, false);
				if (drvResult != null)
				{
					txtCycle.Text = drvResult[PRO_DCOptionMasterTable.CYCLE_FLD].ToString();
					txtCycle.Tag = drvResult[PRO_DCOptionMasterTable.DCOPTIONMASTERID_FLD].ToString();
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

		private void RoughCut()
		{
			const string METHOD_NAME = THIS + ".RoughCut()";
			try
			{
				this.Cursor = Cursors.WaitCursor;
				// get cycle information
				PRO_DCOptionMasterVO voCycle = (PRO_DCOptionMasterVO)boRoughCut.GetCycleInfo(Convert.ToInt32(txtCycle.Tag));
				// start rough cut
				StartRoughCut((ArrayList)(txtProductionLine.Tag), voCycle);
				string[] strMsg = new string[]{this.Text};
				PCSMessageBox.Show(ErrorCode.MESSAGE_TASK_COMPLETED, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, strMsg);
				if(thrProcess != null)
					thrProcess = null;
				this.Cursor = Cursors.Default;
			}
			catch (ThreadAbortException ex)
			{
				Logger.LogMessage(ex.Message, METHOD_NAME, Level.DEBUG);
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
				lblCCN.Visible = true;
				cboCCN.Visible = true;
				lblProductionLine.Visible = true;
				txtProductionLine.Visible = true;
				btnSearch.Visible = true;
				lblCycle.Visible = true;
				txtCycle.Visible = true;
				btnCycle.Visible = true;
				btnProcess.Enabled = true;
				lblProcessing.Visible = false;
				picProcessing.Visible = false;
			}
		}
		private void StartRoughCut(ArrayList parrProductionLineID, PRO_DCOptionMasterVO pvoCycle)
		{
			// getting planning offset in order to get as of date of cycle extractly
			DataTable dtbPlanningOffset = boRoughCut.GetPlanningOffset(intCCNID);

			#region table schema
			DataTable dtbDCPResult = boRoughCut.GetDCPResultSchema();
			dtbDCPResult.Columns[PRO_DCPResultDetailTable.TYPE_FLD].DefaultValue = 0;

			DataTable dtbAllOverData = boRoughCut.GetOverData(0, 0);

			StringBuilder sbOverMasterID = new StringBuilder();
			ArrayList arrOverMasterID = new ArrayList();

			// order produce
			DataTable dtbOrderProduce = boRoughCut.GetOrderProduce(pvoCycle.DCOptionMasterID);
			// change category time
			DataTable dtbChangeCategoryTime = boRoughCut.GetChangeCategoryTime();
			#endregion

			#region Cut over for each production line in list

			foreach (int intProductionLineID in parrProductionLineID)
			{
				#region validate condition before cut

				PRO_DCOptionMasterVO voCycle = pvoCycle;
				// getting list of over item from dcp result of selected production line in selected cycle
				DataTable dtbOverData = boRoughCut.GetOverData(intProductionLineID, voCycle.DCOptionMasterID);
				if (dtbOverData.Rows.Count == 0) // there is no over data for current production line
					continue;
				// refine cycle
				voCycle = RefineCycle(intProductionLineID, dtbPlanningOffset, voCycle);
				DateTime dtmToDate = voCycle.AsOfDate.AddDays(voCycle.PlanHorizon);

				#endregion

				#region Capacity

				// getting used capacity from dcp result
				DataTable dtbTotalCapacity = boRoughCut.GetTotalRequiredCapacity(intProductionLineID, voCycle.DCOptionMasterID,
				                                                                 voCycle.AsOfDate, dtmToDate);
				// getting shift capacity from shiftpattern
				DataTable dtbShiftCapacity = boRoughCut.GetShiftPattern(intProductionLineID);

				#endregion

				#region prepare data: begin stock, working time

				// working date of production line
				DataTable dtbValidWorkDay = boRoughCut.GetWorkingDateFromWCCapacity(intProductionLineID);
				// begin stock from dcp
				DataTable dtbBeginStock = boRoughCut.GetBeginStock(voCycle.DCOptionMasterID);
				dtbOverData.Columns.Add(new DataColumn(STOCK_FLD, typeof(decimal)));
				dtbOverData.Columns[STOCK_FLD].AllowDBNull = true;
				dtbOverData.Columns.Add(new DataColumn(RATE_FLD, typeof(decimal)));
				StringBuilder sbOverItems = new StringBuilder();
				// update begin stock to over data table
				foreach (DataRow drowData in dtbOverData.Rows)
				{
					sbOverItems.Append(drowData[ITM_ProductTable.PRODUCTID_FLD].ToString()).Append(",");
					sbOverMasterID.Append(drowData[PRO_DCPResultDetailTable.DCPRESULTMASTERID_FLD].ToString()).Append(",");
					arrOverMasterID.Add(drowData[PRO_DCPResultDetailTable.DCPRESULTMASTERID_FLD].ToString());
					decimal decBeginQuantity = 0;
					string strFilter = ITM_ProductTable.PRODUCTID_FLD + "=" + drowData[ITM_ProductTable.PRODUCTID_FLD].ToString()
						+ " AND " + PRO_DCOptionMasterTable.DCOPTIONMASTERID_FLD + "=" + voCycle.DCOptionMasterID;
					try
					{
						decBeginQuantity = Convert.ToDecimal(dtbBeginStock.Compute("SUM(Quantity)", strFilter));
					}
					catch{}
					drowData[STOCK_FLD] = decBeginQuantity;
				}
				sbOverItems.Append("0"); // avoid exception
				// product information
				DataTable dtbProduct = boRoughCut.ListProduct(intProductionLineID, sbOverItems.ToString());
				DCPReportBO boReport = new DCPReportBO();
				// working time of productionline
				DataTable dtbWorkingTime = boReport.GetWorkingTime(intProductionLineID);

				#endregion

				#region delivery for parent

				DataTable dtbDeliveryForParent = boRoughCut.GetDeliveryForParent(voCycle.DCOptionMasterID.ToString(), sbOverItems.ToString(), voCycle.AsOfDate, dtmToDate);
				// get first valid work day of current month
				DateTime dtmFirstValidDay = GetFirtValidWorkday(dtbValidWorkDay, voCycle);
				// refine the delivery date
				foreach (DataRow drowData in dtbDeliveryForParent.Rows)
				{
					DateTime dtmDate = (DateTime)drowData["StartTime"];
					// EndTime to check for over quantity from parent
					DateTime dtmEndTime = (DateTime)drowData["EndTime"];
					// do nothing with over quantity from parent
					if (dtmDate.Equals(dtmEndTime))
						continue;
					DateTime dtmTemp = new DateTime(dtmDate.Year, dtmDate.Month, dtmDate.Day);
					if (dtmTemp <= dtmFirstValidDay && dtmTemp >= voCycle.AsOfDate)
					{
						dtmDate = new DateTime(dtmFirstValidDay.Year, dtmFirstValidDay.Month, dtmFirstValidDay.Day,
						                       dtmDate.Hour, dtmDate.Minute, dtmDate.Second);
						drowData["StartTime"] = dtmDate;
						continue;
					}
					decimal decLeadTimeOffset = 0;
					try
					{
						decLeadTimeOffset = Convert.ToDecimal(drowData["LeadTimeOffSet"]);
					}
					catch{}
					decimal decNumOfDay = decLeadTimeOffset / 86400;
					// convert to valid work day
					dtmDate = ConvertWorkingDay(dtbWorkingTime, dtbValidWorkDay, dtmDate, decNumOfDay);
					drowData["StartTime"] = dtmDate;
				}

				#endregion

				#region prepare data: delivery (SO + for Parent), produce, production group

				DateTime dtmShiftFromDate = voCycle.AsOfDate;
				DateTime dtmShiftFromDateTemp = voCycle.AsOfDate;
				DateTime dtmShiftToDate = dtmShiftFromDate;
				DateTime dtmShiftToDateTemp = dtmShiftFromDate;
				GetStartAndEndTime(voCycle.AsOfDate, ref dtmShiftFromDate, ref dtmShiftFromDateTemp, dtbWorkingTime);
				GetStartAndEndTime(voCycle.AsOfDate.AddDays(voCycle.PlanHorizon), ref dtmShiftToDateTemp, ref dtmShiftToDate, dtbWorkingTime);
				// delivery from so
				DataTable dtbDeliverySO = boRoughCut.GetDeliveryForSO(sbOverItems.ToString(), dtmShiftFromDate, dtmShiftToDate);
				// produce
				DataTable dtbProduce = boRoughCut.GetProduce(voCycle.DCOptionMasterID.ToString(), intProductionLineID, sbOverItems.ToString(), voCycle.AsOfDate, dtmToDate);
				// production group of current production line
				DataTable dtbProductionGroup = boRoughCut.GetProductionGroup(intProductionLineID);

				#endregion

				#region table to store used capacity of each over item

				DataTable dtbUsedCapacity = new DataTable();
				dtbUsedCapacity.Columns.Add(new DataColumn(ITM_ProductTable.PRODUCTID_FLD, typeof(int)));
				dtbUsedCapacity.Columns.Add(new DataColumn(PRO_ShiftTable.SHIFTID_FLD, typeof(int)));
				dtbUsedCapacity.Columns.Add(new DataColumn(PRO_DCPResultDetailTable.WORKINGDATE_FLD, typeof(DateTime)));
				dtbUsedCapacity.Columns.Add(new DataColumn(CAPACITY_FLD, typeof(decimal)));
				dtbUsedCapacity.Columns.Add(new DataColumn(PLANQTY_FLD, typeof(decimal)));

				#endregion

				#region start rough cut for each day in cycle

				dtbDCPResult.Clear();
				for (DateTime dtmDay = voCycle.AsOfDate; dtmDay <= dtmToDate; dtmDay = dtmDay.AddDays(1))
				{
					#region variables

					string strStandardFilter = PRO_WCCapacityTable.BEGINDATE_FLD + "<='" + dtmDay.ToString("G") + "'"
						+ " AND " + PRO_WCCapacityTable.ENDDATE_FLD + ">='" + dtmDay.ToString("G") + "'";
					string strCapacityFilter = PRO_DCPResultDetailTable.WORKINGDATE_FLD + "='" + dtmDay.ToString("G") + "'";

					DataRow[] drowValidWorkDay = dtbValidWorkDay.Select(strStandardFilter);
					// today is offday
					if (drowValidWorkDay.Length == 0)
						continue;
				
					#endregion

					DataRow[] drowShifts = dtbShiftCapacity.Select(strStandardFilter);
					// calculate for each shift
					foreach (DataRow drowShift in drowShifts)
					{
						string strShiftFilter = PRO_ShiftTable.SHIFTID_FLD + "="
							+ drowShift[PRO_ShiftTable.SHIFTID_FLD].ToString();
						decimal decShiftRemainCap = 0, decTotalShiftCap = 0, decShiftStandardCap = 0;
						DateTime dtmStartTime = dtmDay, dtmEndTime = dtmDay;
						GetStartAndEndTime(dtmDay, ref dtmStartTime, ref dtmEndTime, dtbWorkingTime,
						                   drowShift[PRO_ShiftTable.SHIFTID_FLD].ToString());

						#region total capacity of current shift 

						// from dcp result
						try
						{
							decTotalShiftCap = Convert.ToDecimal(dtbTotalCapacity.Compute("SUM(" + PRO_DCPResultDetailTable.TOTALSECOND_FLD + ")",
							                                                              strCapacityFilter + " AND " + strShiftFilter));
						}
						catch{}
					
						#endregion

						#region standard capacity of shift

						try
						{
							decShiftStandardCap = Convert.ToDecimal(drowShift[PRO_WCCapacityTable.CAPACITY_FLD]);
						}
						catch{}

						#endregion

						// remain
						decShiftRemainCap = decShiftStandardCap - decTotalShiftCap;
						if (decShiftRemainCap > 0)
						{
							#region Calculate rate of over item and determine plan quantity
							// order item by rate
							dtbOverData = SortItemByRate(dtbOverData, dtbDeliveryForParent, dtbDeliverySO, dtmStartTime, dtmEndTime);
							string strLastOverItem = string.Empty;
							// now calculate quantity to produce for each item
							foreach (DataRow drowItem in dtbOverData.Rows)
							{
								#region prepare for item

								string strProductID = drowItem[ITM_ProductTable.PRODUCTID_FLD].ToString();
								if (strLastOverItem == strProductID)
									continue;
								strLastOverItem = strProductID;
								if (decShiftRemainCap <= 0)
									break;
								decimal decAvailableCap = decShiftRemainCap;
								DataRow drowProductInfo = GetProductInfo(strProductID, dtbProduct);
								if (IsInGroup(strProductID, dtbProductionGroup))
								{
									decimal decGroupStandardCap;
									// calculate total capacity of group
									decimal decGroupCap = CalculateGroupCapacity(strProductID, drowShift[PRO_ShiftTable.SHIFTID_FLD].ToString(),
									                                             dtmDay, dtbProductionGroup, dtbTotalCapacity, dtbUsedCapacity, out decGroupStandardCap);
									// available capacity for product is remain capacity of group
									decAvailableCap = (decAvailableCap < (decGroupStandardCap - decGroupCap)) ? decAvailableCap : (decGroupStandardCap - decGroupCap);
								}

								#endregion

								if (decAvailableCap > 0)
								{
									#region prepare data

									decimal decPlanQty = 0, decOverQty = 0, decVarLT = 0;
									decimal decAvailbleQuantity = 0, decProduceInShift = 0;
									decimal decMinQty = 0, decMaxQty = 0, decMultipleQty = 1;
									string strSetupPair = string.Empty;
									try
									{
										// total over quantity
										decOverQty = Convert.ToDecimal(dtbOverData.Compute("SUM(" + PRO_DCPResultDetailTable.QUANTITY_FLD + ")",
										                                                   ITM_ProductTable.PRODUCTID_FLD + "=" + strProductID));
									}
									catch{}
									try
									{
										decVarLT = Convert.ToDecimal(drowProductInfo[ITM_ProductTable.LTVARIABLETIME_FLD]);
									}
									catch{}
									try
									{
										decMinQty = Convert.ToDecimal(drowProductInfo[ITM_ProductTable.MINPRODUCE_FLD]);
									}
									catch{}
									try
									{
										decMaxQty = Convert.ToDecimal(drowProductInfo[ITM_ProductTable.MAXPRODUCE_FLD]);
									}
									catch
									{
										decMaxQty = 0;
									}
									try
									{
										decMultipleQty = Convert.ToDecimal(drowProductInfo[ITM_ProductTable.ORDERQUANTITYMULTIPLE_FLD]);
										decMultipleQty = (decMultipleQty == 0) ? 1 : decMultipleQty;
									}
									catch{}
									if (drowItem[ITM_ProductTable.SETUPPAIR_FLD] != DBNull.Value)
										strSetupPair = drowItem[ITM_ProductTable.SETUPPAIR_FLD].ToString().Trim();

									try
									{
										string strFilter = ITM_ProductTable.PRODUCTID_FLD + "=" + strProductID
											+ " AND " + PRO_DCPResultDetailTable.WORKINGDATE_FLD + " = '" + dtmDay.ToString("G") + "'"
											+ " AND " + strShiftFilter;
										decProduceInShift = Convert.ToDecimal(dtbProduce.Compute("SUM(" + PRO_DCPResultDetailTable.QUANTITY_FLD + ")",
										                                                         strFilter));
									}
									catch{}

									#region Apply order produce & change category time

									string strMaxOrderItem = string.Empty;
									decimal decChangeCategoryTime = 0;
									int intMaxOrderNo = 0;
									if (decProduceInShift == 0)
									{
										DataRow[] drowOrderOfDay = dtbOrderProduce.Select("WorkingDate = '" + dtmDay.ToString("G") + "'"
											+ " AND " + strShiftFilter, "OrderNo DESC");
										if (drowOrderOfDay.Length > 0)
										{
											strMaxOrderItem = drowOrderOfDay[0]["OrderPlan"].ToString();
											intMaxOrderNo = Convert.ToInt32(drowOrderOfDay[0]["OrderNo"]);
										}
										if (strMaxOrderItem != string.Empty)
										{
											try
											{
												decChangeCategoryTime = Convert.ToDecimal(dtbChangeCategoryTime.Select(PRO_ChangeCategoryMatrixTable.SOURCEPRODUCTID_FLD + "=" + strMaxOrderItem
													+ " AND " + PRO_ChangeCategoryMatrixTable.DESTPRODUCTID_FLD + "=" + strProductID)[0][PRO_ChangeCategoryMatrixTable.CHANGETIME_FLD]);
											}
											catch{}
										}

										// available quantity must subtract change category time
										decAvailableCap -= decChangeCategoryTime;
									}

									#endregion

									try
									{
										decAvailbleQuantity = decAvailableCap / decVarLT;
									}
									catch{}

									#endregion

									#region Calculate produce extra quantity

									if (decAvailbleQuantity >= decOverQty && decProduceInShift == 0)
									{
										decPlanQty = (decOverQty < decMinQty && decMinQty != 0) ? 0 : decOverQty;
										decPlanQty = (decPlanQty > decMaxQty && decMaxQty != 0) ? decMaxQty : decPlanQty;
									}
									else if (decAvailbleQuantity >= decOverQty && decProduceInShift != 0)
									{
										if (decMaxQty != 0)
											decPlanQty = ((decOverQty + decProduceInShift) > decMaxQty) ? 0 : decOverQty;
										else
											decPlanQty = decOverQty;
									}
									else if (decAvailbleQuantity < decOverQty && decProduceInShift != 0)
									{
										if (decAvailbleQuantity < decMinQty && decMinQty != 0)
											decPlanQty = 0;
										else
										{
											decPlanQty = decimal.Floor(decAvailbleQuantity / decMultipleQty)*decMultipleQty;
											decPlanQty = (decPlanQty < decMinQty && decMinQty != 0) ? 0 : decPlanQty;
											decPlanQty = ((decPlanQty + decProduceInShift) > decMaxQty && decMaxQty != 0) ? 0 : decPlanQty;
										}
									}
									else if (decAvailbleQuantity < decOverQty && decProduceInShift == 0)
									{
										if (decAvailbleQuantity < decMinQty && decMinQty != 0)
											decPlanQty = 0;
										else
										{
											decPlanQty = decimal.Floor(decAvailbleQuantity / decMultipleQty) * decMultipleQty;
											decPlanQty = (decPlanQty < decMinQty && decMinQty != 0) ? 0 : decPlanQty;
											decPlanQty = (decPlanQty > decMaxQty && decMaxQty != 0) ? decMaxQty : decPlanQty;
										}
									}

									#endregion

									if (decPlanQty != 0)
									{
										#region find pair product(s) of this product in over data
										DataRow[] drowPairs = null;
										ArrayList arrPairID = new ArrayList();
										if (strSetupPair.Trim() != string.Empty)
										{
											drowPairs = dtbOverData.Select(ITM_ProductTable.SETUPPAIR_FLD + "='" + strSetupPair + "'"
												+ " AND " + ITM_ProductTable.PRODUCTID_FLD + " <> " + strProductID,
												ITM_ProductTable.PRODUCTID_FLD + " ASC");
											string strLastPairID = string.Empty;
											int intPairCount = 0;
											ArrayList arrTempPair = new ArrayList();

											// count number of pair item first
											foreach (DataRow drowPair in drowPairs)
											{
												string strPairID = drowPair[ITM_ProductTable.PRODUCTID_FLD].ToString();
												if (strLastPairID == strPairID)
													continue;
												DataRow drowPairInfo = GetProductInfo(strPairID, dtbProduct);
												if (drowPairInfo == null) // same pair but not the same production line
													continue;
												intPairCount++;
												strLastPairID = strPairID;
												PairInfor objPair = new PairInfor(strPairID, drowPair[PRO_DCPResultDetailTable.DCPRESULTMASTERID_FLD].ToString());
												arrTempPair.Add(objPair);
											}
											if (intPairCount > 0)
											{
												// capacity for one item in pair
												decimal decCapForOne = decAvailableCap / (intPairCount + 1);
												decimal decAvailableCapPair = decCapForOne - (decPlanQty * decVarLT + decChangeCategoryTime);
												if (decAvailableCapPair < 0)
												{
													decPlanQty = decMinQty;
													decAvailableCapPair = decCapForOne - (decPlanQty * decVarLT + decChangeCategoryTime);
													if (decAvailableCapPair < 0) // not enough capacity for min produce
														decPlanQty = 0;
												}
												while (decAvailableCapPair > 0)
												{
													// try to plus multiple quantity to plan quantity
													if (decAvailableCapPair - ((decPlanQty + decMultipleQty)*decVarLT + decChangeCategoryTime) < 0)
														break;
													if (decPlanQty + decMultipleQty > decMaxQty)
														break;
													decPlanQty += decMultipleQty;
													decAvailableCapPair -= ((decPlanQty + decMultipleQty)*decVarLT + decChangeCategoryTime);
												}
											}

											if (decPlanQty != 0)
												arrPairID.AddRange(arrTempPair);

											#region old code
/*
											foreach (DataRow drowPair in drowPairs)
											{
												#region prepare data

												string strPairID = drowPair[ITM_ProductTable.PRODUCTID_FLD].ToString();
												if (strLastPairID == strPairID)
													continue;
												DataRow drowPairInfo = GetProductInfo(strPairID, dtbProduct);
												if (drowPairInfo == null) // same pair but not the same production line
													continue;
												intPairCount++;
												strLastPairID = strPairID;
												decimal decVarLTPair = 0;
												try
												{
													decVarLTPair = Convert.ToDecimal(drowPair[ITM_ProductTable.LTVARIABLETIME_FLD]);
												}
												catch{}

												#endregion

												#region produce extra quantity

												while (decAvailableCapPair < decPlanQty * decVarLTPair && decPlanQty != 0)
												{
													decPlanQty = decPlanQty - decMultipleQty;
													// if plan < min order then set plan to min order
													if ((decPlanQty < decMinQty && decMinQty != 0))
													{
														decPlanQty = decMinQty;
														decAvailableCapPair = decAvailableCap - (decPlanQty * decVarLT + decChangeCategoryTime);
														if (decAvailableCapPair < decPlanQty * decVarLTPair) // till not enough even set to min
															break;
													}
													decPlanQty = (decPlanQty > decMaxQty && decMaxQty != 0) ? decMaxQty : decPlanQty;
													decAvailableCapPair = decAvailableCap - (decPlanQty * decVarLT + decChangeCategoryTime);
												}
												if (decAvailableCapPair < decPlanQty * decVarLTPair)
													break;
												else
												{
													PairInfor objPair = new PairInfor(strPairID, drowPair[PRO_DCPResultDetailTable.DCPRESULTMASTERID_FLD].ToString());
													arrPairID.Add(objPair);
													decAvailableCapPair -= decPlanQty * decVarLTPair + decChangeCategoryTime;
												}

												#endregion
											}*/

											#endregion
										}
										#endregion

										if (decPlanQty != 0)
										{
											#region Mark as used
											DataRow drowUsed = dtbUsedCapacity.NewRow();
											drowUsed[ITM_ProductTable.PRODUCTID_FLD] = strProductID;
											drowUsed[PRO_ShiftTable.SHIFTID_FLD] = drowShift[PRO_ShiftTable.SHIFTID_FLD].ToString();
											drowUsed[PRO_DCPResultDetailTable.WORKINGDATE_FLD] = dtmDay;
											drowUsed[CAPACITY_FLD] = decPlanQty * decVarLT + decChangeCategoryTime;
											drowUsed[PLANQTY_FLD] = decPlanQty;
											dtbUsedCapacity.Rows.Add(drowUsed);
											decShiftRemainCap -= (decPlanQty * decVarLT + decChangeCategoryTime);
											foreach (PairInfor objPair in arrPairID)
											{
												drowUsed = dtbUsedCapacity.NewRow();
												drowUsed[ITM_ProductTable.PRODUCTID_FLD] = objPair.PairID;
												drowUsed[PRO_ShiftTable.SHIFTID_FLD] = drowShift[PRO_ShiftTable.SHIFTID_FLD].ToString();
												drowUsed[PRO_DCPResultDetailTable.WORKINGDATE_FLD] = dtmDay;
												drowUsed[CAPACITY_FLD] = decPlanQty * decVarLT + decChangeCategoryTime;
												drowUsed[PLANQTY_FLD] = decPlanQty;
												dtbUsedCapacity.Rows.Add(drowUsed);
												decShiftRemainCap -= (decPlanQty * decVarLT + decChangeCategoryTime);
											}
											#endregion

											#region insert to result table

											#region DCP Data

											DataRow drowDCP = dtbDCPResult.NewRow();
											drowDCP[PRO_DCPResultDetailTable.STARTTIME_FLD] = dtmStartTime;
											drowDCP[PRO_DCPResultDetailTable.ENDTIME_FLD] = dtmEndTime;
											drowDCP[PRO_DCPResultDetailTable.TOTALSECOND_FLD] = decPlanQty * decVarLT + decChangeCategoryTime;
											drowDCP[PRO_DCPResultDetailTable.QUANTITY_FLD] = decPlanQty;
											drowDCP[PRO_DCPResultDetailTable.PERCENTAGE_FLD] = ((decPlanQty * decVarLT)/decShiftStandardCap)*100;
											drowDCP[PRO_DCPResultDetailTable.DCPRESULTMASTERID_FLD] = drowItem[PRO_DCPResultDetailTable.DCPRESULTMASTERID_FLD];
											drowDCP[PRO_DCPResultDetailTable.WORKINGDATE_FLD] = dtmDay;
											drowDCP[PRO_DCPResultDetailTable.SHIFTID_FLD] = drowShift[PRO_ShiftTable.SHIFTID_FLD];
											drowDCP[PRO_DCPResultDetailTable.ISMANUAL_FLD] = true;
											dtbDCPResult.Rows.Add(drowDCP);

											#endregion

											#region Order Produce Data
											DataRow drowOrderProduce = dtbOrderProduce.NewRow();
											drowOrderProduce[MST_WorkCenterTable.WORKCENTERID_FLD] = drowItem[MST_WorkCenterTable.WORKCENTERID_FLD];
											drowOrderProduce["ColumnName"] = dtmDay.ToString("yyyyMMdd") + "-" + drowShift[PRO_ShiftTable.SHIFTID_FLD].ToString();
											drowOrderProduce["OrderNo"] = intMaxOrderNo++;
											drowOrderProduce["OrderPlan"] = strProductID;
											drowOrderProduce["ShiftID"] = drowShift[PRO_ShiftTable.SHIFTID_FLD];
											drowOrderProduce["DCOptionMasterID"] = pvoCycle.DCOptionMasterID;
											dtbOrderProduce.Rows.Add(drowOrderProduce);
											#endregion

											foreach (PairInfor objPair in arrPairID)
											{
												#region DCP Data

												drowDCP = dtbDCPResult.NewRow();
												drowDCP[PRO_DCPResultDetailTable.STARTTIME_FLD] = dtmStartTime;
												drowDCP[PRO_DCPResultDetailTable.ENDTIME_FLD] = dtmEndTime;
												drowDCP[PRO_DCPResultDetailTable.TOTALSECOND_FLD] = decPlanQty * decVarLT + decChangeCategoryTime;
												drowDCP[PRO_DCPResultDetailTable.QUANTITY_FLD] = decPlanQty;
												drowDCP[PRO_DCPResultDetailTable.PERCENTAGE_FLD] = ((decPlanQty * decVarLT)/decShiftStandardCap)*100;
												drowDCP[PRO_DCPResultDetailTable.DCPRESULTMASTERID_FLD] = objPair.DCPResultMasterID;
												drowDCP[PRO_DCPResultDetailTable.WORKINGDATE_FLD] = dtmDay;
												drowDCP[PRO_DCPResultDetailTable.SHIFTID_FLD] = drowShift[PRO_ShiftTable.SHIFTID_FLD];
												drowDCP[PRO_DCPResultDetailTable.ISMANUAL_FLD] = true;
												dtbDCPResult.Rows.Add(drowDCP);

												#endregion

												#region Order Produce Data

												drowOrderProduce = dtbOrderProduce.NewRow();
												drowOrderProduce[MST_WorkCenterTable.WORKCENTERID_FLD] = dtbOverData.Select("ProductID = " + objPair.PairID)[0][MST_WorkCenterTable.WORKCENTERID_FLD];
												drowOrderProduce["ColumnName"] = dtmDay.ToString("yyyyMMdd") + "-" + drowShift[PRO_ShiftTable.SHIFTID_FLD].ToString();
												drowOrderProduce["OrderNo"] = intMaxOrderNo ++;
												drowOrderProduce["OrderPlan"] = objPair.PairID;
												drowOrderProduce["ShiftID"] = drowShift[PRO_ShiftTable.SHIFTID_FLD];
												drowOrderProduce["DCOptionMasterID"] = pvoCycle.DCOptionMasterID;
												dtbOrderProduce.Rows.Add(drowOrderProduce);

												#endregion
											}
											#endregion

											#region decrease over quantity and capacity

											decimal decQuantityRemain = decPlanQty;
											foreach (DataRow drowOverItem in dtbOverData.Select(ITM_ProductTable.PRODUCTID_FLD
												+ "=" + strProductID, PRO_DCPResultDetailTable.QUANTITY_FLD + " ASC"))
											{
												decimal decQuantity = 0;
												try
												{
													decQuantity = Convert.ToDecimal(drowOverItem[PRO_DCPResultDetailTable.QUANTITY_FLD]);
												}
												catch{}
												if (decQuantity <= 0)
													continue;
												decQuantityRemain -= decQuantity;
												if (decQuantityRemain < 0)
												{
													drowOverItem[PRO_DCPResultDetailTable.QUANTITY_FLD] = Math.Abs(decQuantityRemain);
													try
													{
														drowOverItem[PRO_DCPResultDetailTable.TOTALSECOND_FLD] = Math.Abs(decQuantityRemain) * decVarLT;
													}
													catch{}
													break;
												}
												else
												{
													drowOverItem[PRO_DCPResultDetailTable.QUANTITY_FLD] = 0;
													drowOverItem[PRO_DCPResultDetailTable.TOTALSECOND_FLD] = 0;
												}
											}

											#region decrease over quantity & capacity for each pair item
											foreach (PairInfor objPair in arrPairID)
											{
												decimal decPairLT = 0;
												DataRow drowPair = GetProductInfo(objPair.PairID, dtbProduct);
												try
												{
													decPairLT = Convert.ToDecimal(drowPair[ITM_ProductTable.LTVARIABLETIME_FLD]);
												}
												catch{}
												decimal decRemain = decPlanQty;
												foreach (DataRow drowOverItem in dtbOverData.Select(ITM_ProductTable.PRODUCTID_FLD
													+ "=" + objPair.PairID, PRO_DCPResultDetailTable.QUANTITY_FLD + " ASC"))
												{
													decimal decQuantity = 0;
													try
													{
														decQuantity = Convert.ToDecimal(drowOverItem[PRO_DCPResultDetailTable.QUANTITY_FLD]);
													}
													catch{}
													if (decQuantity <= 0)
														continue;
													decRemain -= decQuantity;
													if (decRemain < 0)
													{
														drowOverItem[PRO_DCPResultDetailTable.QUANTITY_FLD] = Math.Abs(decRemain);
														try
														{
															drowOverItem[PRO_DCPResultDetailTable.TOTALSECOND_FLD] = Math.Abs(decRemain) * decPairLT;
														}
														catch{}
														break;
													}
													else
													{
														drowOverItem[PRO_DCPResultDetailTable.QUANTITY_FLD] = 0;
														drowOverItem[PRO_DCPResultDetailTable.TOTALSECOND_FLD] = 0;
													}
												}
											}
											#endregion

											#endregion
										}
									}
								}
							}
							#endregion
						}
					
						#region remove item which has no over data
						for (int i = dtbOverData.Rows.Count - 1; i >= 0; i--)
						{
							DataRow drowData = dtbOverData.Rows[i];
							if (drowData[PRO_DCPResultDetailTable.QUANTITY_FLD] == DBNull.Value ||
								Convert.ToDecimal(drowData[PRO_DCPResultDetailTable.QUANTITY_FLD]) <= 0)
								dtbOverData.Rows.RemoveAt(i);
						}
						#endregion

						#region calculate stock of shift for each over item
				
						foreach (DataRow drowData in dtbOverData.Rows)
						{
							decimal decStock = 0, decDelivery = 0, decProduce = 0;
							try
							{
								decStock = Convert.ToDecimal(drowData[STOCK_FLD]);
							}
							catch{}
							string strFilter = ITM_ProductTable.PRODUCTID_FLD + "=" + drowData[ITM_ProductTable.PRODUCTID_FLD].ToString()
								+ " AND " + PRO_DCPResultDetailTable.WORKINGDATE_FLD + " = '" + dtmDay.ToString("G") + "'"
								+ " AND " + strShiftFilter;
							string strFilterParent = ITM_ProductTable.PRODUCTID_FLD + "=" + drowData[ITM_ProductTable.PRODUCTID_FLD].ToString()
								+ " AND " + PRO_DCPResultDetailTable.STARTTIME_FLD + " >= '" + dtmStartTime.ToString("G") + "'"
								+ " AND " + PRO_DCPResultDetailTable.STARTTIME_FLD + " < '" + dtmEndTime.ToString("G") + "'";
							string strFilterSO = ITM_ProductTable.PRODUCTID_FLD + "=" + drowData[ITM_ProductTable.PRODUCTID_FLD].ToString()
								+ " AND " + SO_DeliveryScheduleTable.SCHEDULEDATE_FLD + " >= '" + dtmStartTime.ToString("G") + "'"
								+ " AND " + SO_DeliveryScheduleTable.SCHEDULEDATE_FLD + " < '" + dtmEndTime.ToString("G") + "'";
							try
							{
								decDelivery += Convert.ToDecimal(dtbDeliveryForParent.Compute("SUM(" + PRO_DCPResultDetailTable.QUANTITY_FLD + ")",
								                                                              strFilterParent));
							}
							catch{}
							try
							{
								decDelivery += Convert.ToDecimal(dtbDeliverySO.Compute("SUM(" + PRO_DCPResultDetailTable.QUANTITY_FLD + ")",
								                                                       strFilterSO));
							}
							catch{}
							try
							{
								decProduce = Convert.ToDecimal(dtbProduce.Compute("SUM(" + PRO_DCPResultDetailTable.QUANTITY_FLD + ")",
								                                                  strFilter));
							}
							catch{}
							// produce extra of current shift
							try
							{
								decProduce += Convert.ToDecimal(dtbUsedCapacity.Compute("SUM(" + PLANQTY_FLD + ")",
								                                                        strFilter));
							}
							catch{}
							decStock = decStock + decProduce - decDelivery;
							drowData[STOCK_FLD] = decStock;
						}

						#endregion
					} // end for each shift in day
				} // end for each day in cycle

				#endregion

				foreach (DataRow drowOver in dtbOverData.Rows)
					if (Convert.ToDecimal(drowOver[PRO_DCPResultDetailTable.QUANTITY_FLD]) != decimal.Zero)
						dtbAllOverData.ImportRow(drowOver);
			}

			#endregion

			sbOverMasterID.Append("0"); // avoid exception
			// update data to database
			boRoughCut.UpdateOver(dtbDCPResult, dtbAllOverData, arrOverMasterID, sbOverMasterID, pvoCycle);
		}
		private PRO_DCOptionMasterVO RefineCycle(int pintProductionLineID, DataTable pdtbPlanningOffset, PRO_DCOptionMasterVO voCycle)
		{
			string strFilter = PRO_DCOptionMasterTable.DCOPTIONMASTERID_FLD + "= " + voCycle.DCOptionMasterID
				+ " AND " + PRO_ProductionLineTable.PRODUCTIONLINEID_FLD + " = " + pintProductionLineID;
			DataRow[] drowOffset = pdtbPlanningOffset.Select(strFilter);
			// refine as of date of the cycle based on planning offset of current production line
			if (drowOffset.Length > 0)
			{
				if (drowOffset[0][PRO_PlanningOffsetTable.PLANNINGSTARTDATE_FLD] != null
					&& drowOffset[0][PRO_PlanningOffsetTable.PLANNINGSTARTDATE_FLD] != DBNull.Value)
				{
					DateTime dtmStartDate = (DateTime) drowOffset[0][PRO_PlanningOffsetTable.PLANNINGSTARTDATE_FLD];
					voCycle.AsOfDate = new DateTime(dtmStartDate.Year, dtmStartDate.Month, dtmStartDate.Day);
				}
			}
			return voCycle;
		}
		private void GetStartAndEndTime(DateTime pdtmCurrentDay, ref DateTime pdtmStartTime,
			ref DateTime pdtmEndTime, DataTable pdtmWorkingTime)
		{
			DataRow[] drowShifts = pdtmWorkingTime.Select(string.Empty, "WorkTimeFrom ASC");

			if (drowShifts.Length <= 0)
				return;
			//change shift configured day to working day
			pdtmStartTime = new DateTime(pdtmCurrentDay.Year, pdtmCurrentDay.Month, pdtmCurrentDay.Day,
				((DateTime)drowShifts[0]["WorkTimeFrom"]).Hour,
				((DateTime)drowShifts[0]["WorkTimeFrom"]).Minute,
				((DateTime)drowShifts[0]["WorkTimeFrom"]).Second);
			pdtmEndTime = new DateTime(pdtmCurrentDay.Year, pdtmCurrentDay.Month, pdtmCurrentDay.Day,
				((DateTime)drowShifts[drowShifts.Length - 1]["WorkTimeTo"]).Hour,
				((DateTime)drowShifts[drowShifts.Length - 1]["WorkTimeTo"]).Minute,
				((DateTime)drowShifts[drowShifts.Length - 1]["WorkTimeTo"]).Second);
			double dblDiff = ((DateTime)drowShifts[drowShifts.Length - 1]["WorkTimeTo"]).
				Subtract((DateTime)drowShifts[0]["WorkTimeFrom"]).Days;
			pdtmEndTime = pdtmEndTime.AddDays(dblDiff);
		}
		private void GetStartAndEndTime(DateTime pdtmCurrentDay, ref DateTime pdtmStartTime,
			ref DateTime pdtmEndTime, DataTable pdtmWorkingTime, string pstrShiftID)
		{
			DataRow[] drowShifts = pdtmWorkingTime.Select(PRO_ShiftTable.SHIFTID_FLD + "=" + pstrShiftID, "WorkTimeFrom ASC");

			if (drowShifts.Length <= 0)
				return;
			//change shift configured day to working day
			pdtmStartTime = new DateTime(pdtmCurrentDay.Year, pdtmCurrentDay.Month, pdtmCurrentDay.Day,
				((DateTime)drowShifts[0]["WorkTimeFrom"]).Hour,
				((DateTime)drowShifts[0]["WorkTimeFrom"]).Minute,
				((DateTime)drowShifts[0]["WorkTimeFrom"]).Second);
			pdtmEndTime = new DateTime(pdtmCurrentDay.Year, pdtmCurrentDay.Month, pdtmCurrentDay.Day,
				((DateTime)drowShifts[drowShifts.Length - 1]["WorkTimeTo"]).Hour,
				((DateTime)drowShifts[drowShifts.Length - 1]["WorkTimeTo"]).Minute,
				((DateTime)drowShifts[drowShifts.Length - 1]["WorkTimeTo"]).Second);
			double dblDiff = ((DateTime)drowShifts[drowShifts.Length - 1]["WorkTimeTo"]).
				Subtract((DateTime)drowShifts[0]["WorkTimeFrom"]).Days;
			pdtmEndTime = pdtmEndTime.AddDays(dblDiff);
		}
		private DateTime ConvertWorkingDay(DataTable pdtbWorkingTime, DataTable pdtbValidWorkDays, DateTime pdtmDate, decimal pdecNumberOfDay)
		{
			DateTime dtmConvert = pdtmDate;
			DataRow[] drowValidWorkDay = null;
			string strExpression = string.Empty;
			
			dtmConvert = dtmConvert.AddDays(-(double)pdecNumberOfDay);
			DateTime dtmWorkingDay = GetRealWorkingDay(dtmConvert, pdtbWorkingTime);
			bool blnIsOK = false;
			while (!blnIsOK)
			{
				DateTime dtmOld = dtmConvert;				
				//if(arrHolidays.Contains(new DateTime(dtmConvert.Year, dtmConvert.Month, dtmConvert.Day)))
				DateTime dtmConverted = new DateTime(dtmWorkingDay.Year, dtmWorkingDay.Month, dtmWorkingDay.Day);
				strExpression = "BeginDate <= '" + dtmConverted.ToString("G") + "'"
					+ " AND EndDate >='" + dtmConverted.ToString("G") + "'";
				drowValidWorkDay = pdtbValidWorkDays.Select(strExpression);
				if(drowValidWorkDay.Length == 0)
				{
					if(pdtbValidWorkDays.Select("BeginDate <= '"+ dtmConvert.ToString("G") + "'").Length == 0) 
					{
						dtmConvert = (DateTime)pdtbValidWorkDays.Select("","BeginDate ASC")[0]["BeginDate"];
						break;
					}
					dtmConvert = dtmConvert.AddDays(-1);
					dtmWorkingDay = GetRealWorkingDay(dtmConvert, pdtbWorkingTime);
				}
				if(dtmOld == dtmConvert) blnIsOK = true;
			}

			return dtmConvert;
		}
		private DateTime GetFirtValidWorkday(DataTable pdtbValidWorkdays, PRO_DCOptionMasterVO pvoCycle)
		{
			DateTime dtmFirstValidDay = DateTime.MinValue;
			DateTime dtmFromDate = pvoCycle.AsOfDate;
			DateTime dtmToDate = pvoCycle.AsOfDate.AddDays(pvoCycle.PlanHorizon);
			// loop thru all date to find the first valid work day
			string strExpression;
			for (DateTime dtmDate = dtmFromDate; dtmDate <= dtmToDate; dtmDate = dtmDate.AddDays(1))
			{
				strExpression = "BeginDate <= '" + dtmDate.ToString("G") + "'"
					+ " AND EndDate >='" + dtmDate.ToString("G") + "'";
				if (pdtbValidWorkdays.Select(strExpression).Length > 0)
				{
					dtmFirstValidDay = dtmDate;
					break;
				}
			}
			return dtmFirstValidDay;
		}
		private DateTime GetRealWorkingDay(DateTime pdtmNeedToResolve, DataTable pdtbWorkingTime)
		{
			DataRow[] drowShifts = pdtbWorkingTime.Select(string.Empty, "WorkTimeFrom ASC");

			if (drowShifts.Length <= 0)
				return DateTime.MinValue;

			DateTime dtmResolvedDate = pdtmNeedToResolve;
			//change shift configured day to working day
			DateTime dtmStartTime = new DateTime(pdtmNeedToResolve.Year, pdtmNeedToResolve.Month, pdtmNeedToResolve.Day,
				((DateTime)drowShifts[0]["WorkTimeFrom"]).Hour,
				((DateTime)drowShifts[0]["WorkTimeFrom"]).Minute,
				((DateTime)drowShifts[0]["WorkTimeFrom"]).Second);
			DateTime dtmEndTime = new DateTime(pdtmNeedToResolve.Year, pdtmNeedToResolve.Month, pdtmNeedToResolve.Day,
				((DateTime)drowShifts[drowShifts.Length - 1]["WorkTimeTo"]).Hour,
				((DateTime)drowShifts[drowShifts.Length - 1]["WorkTimeTo"]).Minute,
				((DateTime)drowShifts[drowShifts.Length - 1]["WorkTimeTo"]).Second);
			double dblDiff = ((DateTime)drowShifts[drowShifts.Length - 1]["WorkTimeTo"]).
				Subtract((DateTime)drowShifts[0]["WorkTimeFrom"]).Days;
			dtmEndTime = dtmEndTime.AddDays(dblDiff);

			while (dtmResolvedDate < dtmStartTime)
				dtmStartTime = dtmStartTime.AddDays(-1);

			return dtmStartTime;
		}
		private bool IsInGroup(string pstrProductID, DataTable pdtbProductionGroup)
		{
			return pdtbProductionGroup.Select(ITM_ProductTable.PRODUCTID_FLD + "=" + pstrProductID).Length > 0;
		}
		private DataTable SortItemByRate(DataTable pdtbOverItem, DataTable pdtbDeliveryForParent, DataTable pdtbDeliverySO, 
			DateTime pdtmStartTime, DateTime pdtmEndTime)
		{
			foreach (DataRow drowOver in pdtbOverItem.Rows)
			{
				decimal decStock = 0, decDelivery = 0, decRate = 0;
				try
				{
					decStock = Convert.ToDecimal(drowOver[STOCK_FLD]);
				}
				catch{}
				string strFilterParent = ITM_ProductTable.PRODUCTID_FLD + "=" + drowOver[ITM_ProductTable.PRODUCTID_FLD].ToString()
					+ " AND " + PRO_DCPResultDetailTable.STARTTIME_FLD + " >= '" + pdtmStartTime.ToString("G") + "'"
					+ " AND " + PRO_DCPResultDetailTable.STARTTIME_FLD + " < '" + pdtmEndTime.ToString("G") + "'";
				string strFilterSO = ITM_ProductTable.PRODUCTID_FLD + "=" + drowOver[ITM_ProductTable.PRODUCTID_FLD].ToString()
					+ " AND " + SO_DeliveryScheduleTable.SCHEDULEDATE_FLD + " >= '" + pdtmStartTime.ToString("G") + "'"
					+ " AND " + SO_DeliveryScheduleTable.SCHEDULEDATE_FLD + " < '" + pdtmEndTime.ToString("G") + "'";
				try
				{
					decDelivery += Convert.ToDecimal(pdtbDeliveryForParent.Compute("SUM(" + PRO_DCPResultDetailTable.QUANTITY_FLD + ")",
						strFilterParent));
				}
				catch{}
				try
				{
					decDelivery += Convert.ToDecimal(pdtbDeliverySO.Compute("SUM(" + PRO_DCPResultDetailTable.QUANTITY_FLD + ")",
						strFilterSO));
				}
				catch{}
				
				// rate = stock (n-1)/delivery
				if (decDelivery != 0)
					decRate = decStock / decDelivery;
				else
					decRate = decimal.MaxValue;
				drowOver[RATE_FLD] = decRate;
			}
			return SortItemByRate(pdtbOverItem);
		}
		private DataTable SortItemByRate(DataTable pdtbRatedItems)
		{
			DataTable dtbTemp = pdtbRatedItems.Clone();
			DataRow[] drowImports = pdtbRatedItems.Select(string.Empty, RATE_FLD + " ASC, "
				+ STOCK_FLD + " ASC, "
				+ ITM_ProductTable.PRODUCTID_FLD + " ASC");
			foreach (DataRow drowImp in drowImports)
				dtbTemp.ImportRow(drowImp);
			return dtbTemp;
		}
		private DataRow GetProductInfo(string pstrProductID, DataTable pdtbProducts)
		{
			if (pdtbProducts.Select(ITM_ProductTable.PRODUCTID_FLD + "=" + pstrProductID).Length > 0)
				return pdtbProducts.Select(ITM_ProductTable.PRODUCTID_FLD + "=" + pstrProductID)[0];
			else
				return null;
		}
		/// <summary>
		/// Calculate group used capacity in a shift of working day
		/// </summary>
		/// <param name="pstrProductID">ProductID</param>
		/// <param name="pstrShiftID">Shift to check</param>
		/// <param name="pdtmWorkingDay">Working day</param>
		/// <param name="pdtbProductionGroup">Production Group data</param>
		/// <param name="pdtbCapacity">Capacity from DCP</param>
		/// <param name="odecGroupStandardCap">Standard Capacity of Item in Group</param>
		/// <returns>Group Capacity</returns>
		private decimal CalculateGroupCapacity(string pstrProductID, string pstrShiftID, DateTime pdtmWorkingDay,
			DataTable pdtbProductionGroup, DataTable pdtbCapacity, DataTable pdtbUsedCapacity,
			out decimal odecGroupStandardCap)
		{
			odecGroupStandardCap = 0;
			string strGroupID = pdtbProductionGroup.Select(ITM_ProductTable.PRODUCTID_FLD + "="
				+ pstrProductID)[0][PRO_ProductionGroupTable.PRODUCTIONGROUPID_FLD].ToString();
			StringBuilder sbItemList = new StringBuilder();
			foreach (DataRow drowData in pdtbProductionGroup.Select(PRO_ProductionGroupTable.PRODUCTIONGROUPID_FLD + "=" + strGroupID))
				sbItemList.Append(drowData[ITM_ProductTable.PRODUCTID_FLD].ToString()).Append(",");
			sbItemList.Append("0"); // avoid exception
			decimal decGroupCap = 0;
			string strFilter = ITM_ProductTable.PRODUCTID_FLD + " IN (" + sbItemList.ToString() + ")"
				+ " AND " + PRO_ShiftTable.SHIFTID_FLD + "=" + pstrShiftID
				+ " AND " + PRO_DCPResultDetailTable.WORKINGDATE_FLD + "='" + pdtmWorkingDay.ToString("G") + "'";
			try
			{
				decGroupCap = Convert.ToDecimal(pdtbCapacity.Compute("SUM(" + PRO_DCPResultDetailTable.TOTALSECOND_FLD + ")",
					strFilter));
			}
			catch{}
			try
			{
				decGroupCap += Convert.ToDecimal(pdtbUsedCapacity.Compute("SUM(" + CAPACITY_FLD + ")", strFilter));
			}
			catch{}
			try
			{
				strFilter = ITM_ProductTable.PRODUCTID_FLD + "=" + pstrProductID
					+ " AND " + PRO_ProductionGroupTable.PRODUCTIONGROUPID_FLD + "=" + strGroupID;
				odecGroupStandardCap = Convert.ToDecimal(pdtbProductionGroup.Compute("SUM(" + PRO_ProductionGroupTable.CAPACITYOFGROUP_FLD + ")",
					strFilter));
			}
			catch{}
			return decGroupCap;
		}
	}
	public class PairInfor
	{
		private string strPairID;
		private string strDCPResultMasterID;

		public string PairID
		{
			get { return strPairID; }
		}

		public string DCPResultMasterID
		{
			get { return strDCPResultMasterID; }
		}
		public PairInfor(string pstrPairID, string pstrDCPResultMasterID)
		{
			strPairID = pstrPairID;
			strDCPResultMasterID = pstrDCPResultMasterID;
		}
	}
}