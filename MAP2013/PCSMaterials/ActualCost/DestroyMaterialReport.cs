using System;
using System.IO;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

using C1.Win.C1TrueDBGrid;

//Using PCS's namespaces
using PCSComUtils.Common;
using PCSComUtils.Common.BO;
using PCSComUtils.Framework.ReportFrame.BO;
using PCSComUtils.MasterSetup.DS;
using PCSComUtils.PCSExc;
using PCSUtils.Log;
using PCSUtils.Utils;

namespace PCSMaterials.ActualCost
{
	/// <summary>
	/// Render Order Summary Report.
	/// </summary>
	public class DestroyMaterialReport : System.Windows.Forms.Form
	{
		#region Declaration
		
		#region Generated Declaration
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.Windows.Forms.Button btnClose;
		private C1.Win.C1List.C1Combo cboCCN;
		private System.Windows.Forms.ComboBox cboYear;
		private System.Windows.Forms.Label lblCCN;
		private System.Windows.Forms.Label lblYear;
		private System.Windows.Forms.Button btnExecute;
		private System.Windows.Forms.Button btnHelp;
		private System.Windows.Forms.Label lblCategory;
		private System.Windows.Forms.TextBox txtCategory;
		private System.Windows.Forms.TextBox txtModel;
		private System.Windows.Forms.Label lblModel;
		private System.Windows.Forms.Label lblDepartment;
		private System.Windows.Forms.ComboBox cboMonth;
		private System.Windows.Forms.Label lblMonth;
		private System.Windows.Forms.Label lblProductionLine;
		private System.Windows.Forms.Label lblPartNo;
		private System.Windows.Forms.Label lblPartName;
		private System.Windows.Forms.TextBox txtDepartment;
		private System.Windows.Forms.Button btnPartNo;
		private System.Windows.Forms.TextBox txtPartNo;
		private System.Windows.Forms.TextBox txtProductionLine;
		private System.Windows.Forms.Button btnProductionLine;
		private System.Windows.Forms.TextBox txtPartName;
		private System.Windows.Forms.Button btnPartName;
		private System.Windows.Forms.Button btnDepartment;
		private System.Windows.Forms.Button btnCategory;
		private System.ComponentModel.Container components = null;

		#endregion Generated Declaration
		
		#region Constants
		
		private const string THIS = "PCSMaterials.ActualCost.DestroyMaterialReport";
		
		private const string APPLICATION_PATH     = @"PCSMain\bin\Debug";
		private const string DESTROY_MATERIAL_REPORT = "DestroyMaterialReport.xml";
		private const string REPORT_NAME		  = "DestroyMaterialReport";

		#endregion Constants

		#endregion Declaration
		
		#region Constructor, Destructor
		
		public DestroyMaterialReport()
		{			
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(DestroyMaterialReport));
			this.btnExecute = new System.Windows.Forms.Button();
			this.btnClose = new System.Windows.Forms.Button();
			this.cboCCN = new C1.Win.C1List.C1Combo();
			this.lblCCN = new System.Windows.Forms.Label();
			this.cboYear = new System.Windows.Forms.ComboBox();
			this.cboMonth = new System.Windows.Forms.ComboBox();
			this.txtModel = new System.Windows.Forms.TextBox();
			this.lblModel = new System.Windows.Forms.Label();
			this.btnCategory = new System.Windows.Forms.Button();
			this.txtCategory = new System.Windows.Forms.TextBox();
			this.lblCategory = new System.Windows.Forms.Label();
			this.btnDepartment = new System.Windows.Forms.Button();
			this.txtDepartment = new System.Windows.Forms.TextBox();
			this.lblDepartment = new System.Windows.Forms.Label();
			this.btnPartNo = new System.Windows.Forms.Button();
			this.txtPartNo = new System.Windows.Forms.TextBox();
			this.lblPartNo = new System.Windows.Forms.Label();
			this.lblMonth = new System.Windows.Forms.Label();
			this.lblYear = new System.Windows.Forms.Label();
			this.btnHelp = new System.Windows.Forms.Button();
			this.txtProductionLine = new System.Windows.Forms.TextBox();
			this.lblProductionLine = new System.Windows.Forms.Label();
			this.btnProductionLine = new System.Windows.Forms.Button();
			this.txtPartName = new System.Windows.Forms.TextBox();
			this.lblPartName = new System.Windows.Forms.Label();
			this.btnPartName = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.cboCCN)).BeginInit();
			this.SuspendLayout();
			// 
			// btnExecute
			// 
			this.btnExecute.AccessibleDescription = resources.GetString("btnExecute.AccessibleDescription");
			this.btnExecute.AccessibleName = resources.GetString("btnExecute.AccessibleName");
			this.btnExecute.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnExecute.Anchor")));
			this.btnExecute.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnExecute.BackgroundImage")));
			this.btnExecute.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnExecute.Dock")));
			this.btnExecute.Enabled = ((bool)(resources.GetObject("btnExecute.Enabled")));
			this.btnExecute.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnExecute.FlatStyle")));
			this.btnExecute.Font = ((System.Drawing.Font)(resources.GetObject("btnExecute.Font")));
			this.btnExecute.Image = ((System.Drawing.Image)(resources.GetObject("btnExecute.Image")));
			this.btnExecute.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnExecute.ImageAlign")));
			this.btnExecute.ImageIndex = ((int)(resources.GetObject("btnExecute.ImageIndex")));
			this.btnExecute.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnExecute.ImeMode")));
			this.btnExecute.Location = ((System.Drawing.Point)(resources.GetObject("btnExecute.Location")));
			this.btnExecute.Name = "btnExecute";
			this.btnExecute.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnExecute.RightToLeft")));
			this.btnExecute.Size = ((System.Drawing.Size)(resources.GetObject("btnExecute.Size")));
			this.btnExecute.TabIndex = ((int)(resources.GetObject("btnExecute.TabIndex")));
			this.btnExecute.Text = resources.GetString("btnExecute.Text");
			this.btnExecute.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnExecute.TextAlign")));
			this.btnExecute.Visible = ((bool)(resources.GetObject("btnExecute.Visible")));
			this.btnExecute.Click += new System.EventHandler(this.btnExecute_Click);
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
			// cboYear
			// 
			this.cboYear.AccessibleDescription = resources.GetString("cboYear.AccessibleDescription");
			this.cboYear.AccessibleName = resources.GetString("cboYear.AccessibleName");
			this.cboYear.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("cboYear.Anchor")));
			this.cboYear.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cboYear.BackgroundImage")));
			this.cboYear.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("cboYear.Dock")));
			this.cboYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboYear.Enabled = ((bool)(resources.GetObject("cboYear.Enabled")));
			this.cboYear.Font = ((System.Drawing.Font)(resources.GetObject("cboYear.Font")));
			this.cboYear.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("cboYear.ImeMode")));
			this.cboYear.IntegralHeight = ((bool)(resources.GetObject("cboYear.IntegralHeight")));
			this.cboYear.ItemHeight = ((int)(resources.GetObject("cboYear.ItemHeight")));
			this.cboYear.Location = ((System.Drawing.Point)(resources.GetObject("cboYear.Location")));
			this.cboYear.MaxDropDownItems = ((int)(resources.GetObject("cboYear.MaxDropDownItems")));
			this.cboYear.MaxLength = ((int)(resources.GetObject("cboYear.MaxLength")));
			this.cboYear.Name = "cboYear";
			this.cboYear.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("cboYear.RightToLeft")));
			this.cboYear.Size = ((System.Drawing.Size)(resources.GetObject("cboYear.Size")));
			this.cboYear.TabIndex = ((int)(resources.GetObject("cboYear.TabIndex")));
			this.cboYear.Text = resources.GetString("cboYear.Text");
			this.cboYear.Visible = ((bool)(resources.GetObject("cboYear.Visible")));
			this.cboYear.SelectedIndexChanged += new System.EventHandler(this.cboYear_SelectedIndexChanged);
			// 
			// cboMonth
			// 
			this.cboMonth.AccessibleDescription = resources.GetString("cboMonth.AccessibleDescription");
			this.cboMonth.AccessibleName = resources.GetString("cboMonth.AccessibleName");
			this.cboMonth.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("cboMonth.Anchor")));
			this.cboMonth.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cboMonth.BackgroundImage")));
			this.cboMonth.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("cboMonth.Dock")));
			this.cboMonth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboMonth.Enabled = ((bool)(resources.GetObject("cboMonth.Enabled")));
			this.cboMonth.Font = ((System.Drawing.Font)(resources.GetObject("cboMonth.Font")));
			this.cboMonth.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("cboMonth.ImeMode")));
			this.cboMonth.IntegralHeight = ((bool)(resources.GetObject("cboMonth.IntegralHeight")));
			this.cboMonth.ItemHeight = ((int)(resources.GetObject("cboMonth.ItemHeight")));
			this.cboMonth.Location = ((System.Drawing.Point)(resources.GetObject("cboMonth.Location")));
			this.cboMonth.MaxDropDownItems = ((int)(resources.GetObject("cboMonth.MaxDropDownItems")));
			this.cboMonth.MaxLength = ((int)(resources.GetObject("cboMonth.MaxLength")));
			this.cboMonth.Name = "cboMonth";
			this.cboMonth.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("cboMonth.RightToLeft")));
			this.cboMonth.Size = ((System.Drawing.Size)(resources.GetObject("cboMonth.Size")));
			this.cboMonth.TabIndex = ((int)(resources.GetObject("cboMonth.TabIndex")));
			this.cboMonth.Text = resources.GetString("cboMonth.Text");
			this.cboMonth.Visible = ((bool)(resources.GetObject("cboMonth.Visible")));
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
			// lblModel
			// 
			this.lblModel.AccessibleDescription = resources.GetString("lblModel.AccessibleDescription");
			this.lblModel.AccessibleName = resources.GetString("lblModel.AccessibleName");
			this.lblModel.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblModel.Anchor")));
			this.lblModel.AutoSize = ((bool)(resources.GetObject("lblModel.AutoSize")));
			this.lblModel.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblModel.Dock")));
			this.lblModel.Enabled = ((bool)(resources.GetObject("lblModel.Enabled")));
			this.lblModel.Font = ((System.Drawing.Font)(resources.GetObject("lblModel.Font")));
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
			// btnCategory
			// 
			this.btnCategory.AccessibleDescription = resources.GetString("btnCategory.AccessibleDescription");
			this.btnCategory.AccessibleName = resources.GetString("btnCategory.AccessibleName");
			this.btnCategory.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnCategory.Anchor")));
			this.btnCategory.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCategory.BackgroundImage")));
			this.btnCategory.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnCategory.Dock")));
			this.btnCategory.Enabled = ((bool)(resources.GetObject("btnCategory.Enabled")));
			this.btnCategory.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnCategory.FlatStyle")));
			this.btnCategory.Font = ((System.Drawing.Font)(resources.GetObject("btnCategory.Font")));
			this.btnCategory.Image = ((System.Drawing.Image)(resources.GetObject("btnCategory.Image")));
			this.btnCategory.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnCategory.ImageAlign")));
			this.btnCategory.ImageIndex = ((int)(resources.GetObject("btnCategory.ImageIndex")));
			this.btnCategory.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnCategory.ImeMode")));
			this.btnCategory.Location = ((System.Drawing.Point)(resources.GetObject("btnCategory.Location")));
			this.btnCategory.Name = "btnCategory";
			this.btnCategory.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnCategory.RightToLeft")));
			this.btnCategory.Size = ((System.Drawing.Size)(resources.GetObject("btnCategory.Size")));
			this.btnCategory.TabIndex = ((int)(resources.GetObject("btnCategory.TabIndex")));
			this.btnCategory.Text = resources.GetString("btnCategory.Text");
			this.btnCategory.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnCategory.TextAlign")));
			this.btnCategory.Visible = ((bool)(resources.GetObject("btnCategory.Visible")));
			this.btnCategory.Click += new System.EventHandler(this.btnCategory_Click);
			// 
			// txtCategory
			// 
			this.txtCategory.AccessibleDescription = resources.GetString("txtCategory.AccessibleDescription");
			this.txtCategory.AccessibleName = resources.GetString("txtCategory.AccessibleName");
			this.txtCategory.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("txtCategory.Anchor")));
			this.txtCategory.AutoSize = ((bool)(resources.GetObject("txtCategory.AutoSize")));
			this.txtCategory.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("txtCategory.BackgroundImage")));
			this.txtCategory.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("txtCategory.Dock")));
			this.txtCategory.Enabled = ((bool)(resources.GetObject("txtCategory.Enabled")));
			this.txtCategory.Font = ((System.Drawing.Font)(resources.GetObject("txtCategory.Font")));
			this.txtCategory.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("txtCategory.ImeMode")));
			this.txtCategory.Location = ((System.Drawing.Point)(resources.GetObject("txtCategory.Location")));
			this.txtCategory.MaxLength = ((int)(resources.GetObject("txtCategory.MaxLength")));
			this.txtCategory.Multiline = ((bool)(resources.GetObject("txtCategory.Multiline")));
			this.txtCategory.Name = "txtCategory";
			this.txtCategory.PasswordChar = ((char)(resources.GetObject("txtCategory.PasswordChar")));
			this.txtCategory.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("txtCategory.RightToLeft")));
			this.txtCategory.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("txtCategory.ScrollBars")));
			this.txtCategory.Size = ((System.Drawing.Size)(resources.GetObject("txtCategory.Size")));
			this.txtCategory.TabIndex = ((int)(resources.GetObject("txtCategory.TabIndex")));
			this.txtCategory.Text = resources.GetString("txtCategory.Text");
			this.txtCategory.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("txtCategory.TextAlign")));
			this.txtCategory.Visible = ((bool)(resources.GetObject("txtCategory.Visible")));
			this.txtCategory.WordWrap = ((bool)(resources.GetObject("txtCategory.WordWrap")));
			this.txtCategory.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCategory_KeyDown);
			this.txtCategory.Validating += new System.ComponentModel.CancelEventHandler(this.txtCategory_Validating);
			// 
			// lblCategory
			// 
			this.lblCategory.AccessibleDescription = resources.GetString("lblCategory.AccessibleDescription");
			this.lblCategory.AccessibleName = resources.GetString("lblCategory.AccessibleName");
			this.lblCategory.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblCategory.Anchor")));
			this.lblCategory.AutoSize = ((bool)(resources.GetObject("lblCategory.AutoSize")));
			this.lblCategory.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblCategory.Dock")));
			this.lblCategory.Enabled = ((bool)(resources.GetObject("lblCategory.Enabled")));
			this.lblCategory.Font = ((System.Drawing.Font)(resources.GetObject("lblCategory.Font")));
			this.lblCategory.Image = ((System.Drawing.Image)(resources.GetObject("lblCategory.Image")));
			this.lblCategory.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblCategory.ImageAlign")));
			this.lblCategory.ImageIndex = ((int)(resources.GetObject("lblCategory.ImageIndex")));
			this.lblCategory.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblCategory.ImeMode")));
			this.lblCategory.Location = ((System.Drawing.Point)(resources.GetObject("lblCategory.Location")));
			this.lblCategory.Name = "lblCategory";
			this.lblCategory.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblCategory.RightToLeft")));
			this.lblCategory.Size = ((System.Drawing.Size)(resources.GetObject("lblCategory.Size")));
			this.lblCategory.TabIndex = ((int)(resources.GetObject("lblCategory.TabIndex")));
			this.lblCategory.Text = resources.GetString("lblCategory.Text");
			this.lblCategory.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblCategory.TextAlign")));
			this.lblCategory.Visible = ((bool)(resources.GetObject("lblCategory.Visible")));
			// 
			// btnDepartment
			// 
			this.btnDepartment.AccessibleDescription = resources.GetString("btnDepartment.AccessibleDescription");
			this.btnDepartment.AccessibleName = resources.GetString("btnDepartment.AccessibleName");
			this.btnDepartment.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnDepartment.Anchor")));
			this.btnDepartment.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnDepartment.BackgroundImage")));
			this.btnDepartment.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnDepartment.Dock")));
			this.btnDepartment.Enabled = ((bool)(resources.GetObject("btnDepartment.Enabled")));
			this.btnDepartment.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnDepartment.FlatStyle")));
			this.btnDepartment.Font = ((System.Drawing.Font)(resources.GetObject("btnDepartment.Font")));
			this.btnDepartment.Image = ((System.Drawing.Image)(resources.GetObject("btnDepartment.Image")));
			this.btnDepartment.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnDepartment.ImageAlign")));
			this.btnDepartment.ImageIndex = ((int)(resources.GetObject("btnDepartment.ImageIndex")));
			this.btnDepartment.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnDepartment.ImeMode")));
			this.btnDepartment.Location = ((System.Drawing.Point)(resources.GetObject("btnDepartment.Location")));
			this.btnDepartment.Name = "btnDepartment";
			this.btnDepartment.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnDepartment.RightToLeft")));
			this.btnDepartment.Size = ((System.Drawing.Size)(resources.GetObject("btnDepartment.Size")));
			this.btnDepartment.TabIndex = ((int)(resources.GetObject("btnDepartment.TabIndex")));
			this.btnDepartment.Text = resources.GetString("btnDepartment.Text");
			this.btnDepartment.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnDepartment.TextAlign")));
			this.btnDepartment.Visible = ((bool)(resources.GetObject("btnDepartment.Visible")));
			this.btnDepartment.Click += new System.EventHandler(this.btnDepartment_Click);
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
			// lblDepartment
			// 
			this.lblDepartment.AccessibleDescription = resources.GetString("lblDepartment.AccessibleDescription");
			this.lblDepartment.AccessibleName = resources.GetString("lblDepartment.AccessibleName");
			this.lblDepartment.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblDepartment.Anchor")));
			this.lblDepartment.AutoSize = ((bool)(resources.GetObject("lblDepartment.AutoSize")));
			this.lblDepartment.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblDepartment.Dock")));
			this.lblDepartment.Enabled = ((bool)(resources.GetObject("lblDepartment.Enabled")));
			this.lblDepartment.Font = ((System.Drawing.Font)(resources.GetObject("lblDepartment.Font")));
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
			// btnPartNo
			// 
			this.btnPartNo.AccessibleDescription = resources.GetString("btnPartNo.AccessibleDescription");
			this.btnPartNo.AccessibleName = resources.GetString("btnPartNo.AccessibleName");
			this.btnPartNo.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnPartNo.Anchor")));
			this.btnPartNo.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnPartNo.BackgroundImage")));
			this.btnPartNo.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnPartNo.Dock")));
			this.btnPartNo.Enabled = ((bool)(resources.GetObject("btnPartNo.Enabled")));
			this.btnPartNo.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnPartNo.FlatStyle")));
			this.btnPartNo.Font = ((System.Drawing.Font)(resources.GetObject("btnPartNo.Font")));
			this.btnPartNo.Image = ((System.Drawing.Image)(resources.GetObject("btnPartNo.Image")));
			this.btnPartNo.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnPartNo.ImageAlign")));
			this.btnPartNo.ImageIndex = ((int)(resources.GetObject("btnPartNo.ImageIndex")));
			this.btnPartNo.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnPartNo.ImeMode")));
			this.btnPartNo.Location = ((System.Drawing.Point)(resources.GetObject("btnPartNo.Location")));
			this.btnPartNo.Name = "btnPartNo";
			this.btnPartNo.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnPartNo.RightToLeft")));
			this.btnPartNo.Size = ((System.Drawing.Size)(resources.GetObject("btnPartNo.Size")));
			this.btnPartNo.TabIndex = ((int)(resources.GetObject("btnPartNo.TabIndex")));
			this.btnPartNo.Text = resources.GetString("btnPartNo.Text");
			this.btnPartNo.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnPartNo.TextAlign")));
			this.btnPartNo.Visible = ((bool)(resources.GetObject("btnPartNo.Visible")));
			this.btnPartNo.Click += new System.EventHandler(this.btnPartNo_Click);
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
			// lblPartNo
			// 
			this.lblPartNo.AccessibleDescription = resources.GetString("lblPartNo.AccessibleDescription");
			this.lblPartNo.AccessibleName = resources.GetString("lblPartNo.AccessibleName");
			this.lblPartNo.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblPartNo.Anchor")));
			this.lblPartNo.AutoSize = ((bool)(resources.GetObject("lblPartNo.AutoSize")));
			this.lblPartNo.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblPartNo.Dock")));
			this.lblPartNo.Enabled = ((bool)(resources.GetObject("lblPartNo.Enabled")));
			this.lblPartNo.Font = ((System.Drawing.Font)(resources.GetObject("lblPartNo.Font")));
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
			// lblMonth
			// 
			this.lblMonth.AccessibleDescription = resources.GetString("lblMonth.AccessibleDescription");
			this.lblMonth.AccessibleName = resources.GetString("lblMonth.AccessibleName");
			this.lblMonth.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblMonth.Anchor")));
			this.lblMonth.AutoSize = ((bool)(resources.GetObject("lblMonth.AutoSize")));
			this.lblMonth.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblMonth.Dock")));
			this.lblMonth.Enabled = ((bool)(resources.GetObject("lblMonth.Enabled")));
			this.lblMonth.Font = ((System.Drawing.Font)(resources.GetObject("lblMonth.Font")));
			this.lblMonth.ForeColor = System.Drawing.Color.Maroon;
			this.lblMonth.Image = ((System.Drawing.Image)(resources.GetObject("lblMonth.Image")));
			this.lblMonth.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblMonth.ImageAlign")));
			this.lblMonth.ImageIndex = ((int)(resources.GetObject("lblMonth.ImageIndex")));
			this.lblMonth.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblMonth.ImeMode")));
			this.lblMonth.Location = ((System.Drawing.Point)(resources.GetObject("lblMonth.Location")));
			this.lblMonth.Name = "lblMonth";
			this.lblMonth.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblMonth.RightToLeft")));
			this.lblMonth.Size = ((System.Drawing.Size)(resources.GetObject("lblMonth.Size")));
			this.lblMonth.TabIndex = ((int)(resources.GetObject("lblMonth.TabIndex")));
			this.lblMonth.Text = resources.GetString("lblMonth.Text");
			this.lblMonth.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblMonth.TextAlign")));
			this.lblMonth.Visible = ((bool)(resources.GetObject("lblMonth.Visible")));
			// 
			// lblYear
			// 
			this.lblYear.AccessibleDescription = resources.GetString("lblYear.AccessibleDescription");
			this.lblYear.AccessibleName = resources.GetString("lblYear.AccessibleName");
			this.lblYear.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblYear.Anchor")));
			this.lblYear.AutoSize = ((bool)(resources.GetObject("lblYear.AutoSize")));
			this.lblYear.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblYear.Dock")));
			this.lblYear.Enabled = ((bool)(resources.GetObject("lblYear.Enabled")));
			this.lblYear.Font = ((System.Drawing.Font)(resources.GetObject("lblYear.Font")));
			this.lblYear.ForeColor = System.Drawing.Color.Maroon;
			this.lblYear.Image = ((System.Drawing.Image)(resources.GetObject("lblYear.Image")));
			this.lblYear.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblYear.ImageAlign")));
			this.lblYear.ImageIndex = ((int)(resources.GetObject("lblYear.ImageIndex")));
			this.lblYear.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblYear.ImeMode")));
			this.lblYear.Location = ((System.Drawing.Point)(resources.GetObject("lblYear.Location")));
			this.lblYear.Name = "lblYear";
			this.lblYear.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblYear.RightToLeft")));
			this.lblYear.Size = ((System.Drawing.Size)(resources.GetObject("lblYear.Size")));
			this.lblYear.TabIndex = ((int)(resources.GetObject("lblYear.TabIndex")));
			this.lblYear.Text = resources.GetString("lblYear.Text");
			this.lblYear.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblYear.TextAlign")));
			this.lblYear.Visible = ((bool)(resources.GetObject("lblYear.Visible")));
			// 
			// btnHelp
			// 
			this.btnHelp.AccessibleDescription = resources.GetString("btnHelp.AccessibleDescription");
			this.btnHelp.AccessibleName = resources.GetString("btnHelp.AccessibleName");
			this.btnHelp.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnHelp.Anchor")));
			this.btnHelp.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnHelp.BackgroundImage")));
			this.btnHelp.DialogResult = System.Windows.Forms.DialogResult.Cancel;
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
			// lblProductionLine
			// 
			this.lblProductionLine.AccessibleDescription = resources.GetString("lblProductionLine.AccessibleDescription");
			this.lblProductionLine.AccessibleName = resources.GetString("lblProductionLine.AccessibleName");
			this.lblProductionLine.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblProductionLine.Anchor")));
			this.lblProductionLine.AutoSize = ((bool)(resources.GetObject("lblProductionLine.AutoSize")));
			this.lblProductionLine.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblProductionLine.Dock")));
			this.lblProductionLine.Enabled = ((bool)(resources.GetObject("lblProductionLine.Enabled")));
			this.lblProductionLine.Font = ((System.Drawing.Font)(resources.GetObject("lblProductionLine.Font")));
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
			// btnProductionLine
			// 
			this.btnProductionLine.AccessibleDescription = resources.GetString("btnProductionLine.AccessibleDescription");
			this.btnProductionLine.AccessibleName = resources.GetString("btnProductionLine.AccessibleName");
			this.btnProductionLine.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnProductionLine.Anchor")));
			this.btnProductionLine.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnProductionLine.BackgroundImage")));
			this.btnProductionLine.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnProductionLine.Dock")));
			this.btnProductionLine.Enabled = ((bool)(resources.GetObject("btnProductionLine.Enabled")));
			this.btnProductionLine.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnProductionLine.FlatStyle")));
			this.btnProductionLine.Font = ((System.Drawing.Font)(resources.GetObject("btnProductionLine.Font")));
			this.btnProductionLine.Image = ((System.Drawing.Image)(resources.GetObject("btnProductionLine.Image")));
			this.btnProductionLine.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnProductionLine.ImageAlign")));
			this.btnProductionLine.ImageIndex = ((int)(resources.GetObject("btnProductionLine.ImageIndex")));
			this.btnProductionLine.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnProductionLine.ImeMode")));
			this.btnProductionLine.Location = ((System.Drawing.Point)(resources.GetObject("btnProductionLine.Location")));
			this.btnProductionLine.Name = "btnProductionLine";
			this.btnProductionLine.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnProductionLine.RightToLeft")));
			this.btnProductionLine.Size = ((System.Drawing.Size)(resources.GetObject("btnProductionLine.Size")));
			this.btnProductionLine.TabIndex = ((int)(resources.GetObject("btnProductionLine.TabIndex")));
			this.btnProductionLine.Text = resources.GetString("btnProductionLine.Text");
			this.btnProductionLine.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnProductionLine.TextAlign")));
			this.btnProductionLine.Visible = ((bool)(resources.GetObject("btnProductionLine.Visible")));
			this.btnProductionLine.Click += new System.EventHandler(this.btnProductionLine_Click);
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
			// lblPartName
			// 
			this.lblPartName.AccessibleDescription = resources.GetString("lblPartName.AccessibleDescription");
			this.lblPartName.AccessibleName = resources.GetString("lblPartName.AccessibleName");
			this.lblPartName.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblPartName.Anchor")));
			this.lblPartName.AutoSize = ((bool)(resources.GetObject("lblPartName.AutoSize")));
			this.lblPartName.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblPartName.Dock")));
			this.lblPartName.Enabled = ((bool)(resources.GetObject("lblPartName.Enabled")));
			this.lblPartName.Font = ((System.Drawing.Font)(resources.GetObject("lblPartName.Font")));
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
			// btnPartName
			// 
			this.btnPartName.AccessibleDescription = resources.GetString("btnPartName.AccessibleDescription");
			this.btnPartName.AccessibleName = resources.GetString("btnPartName.AccessibleName");
			this.btnPartName.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnPartName.Anchor")));
			this.btnPartName.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnPartName.BackgroundImage")));
			this.btnPartName.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnPartName.Dock")));
			this.btnPartName.Enabled = ((bool)(resources.GetObject("btnPartName.Enabled")));
			this.btnPartName.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnPartName.FlatStyle")));
			this.btnPartName.Font = ((System.Drawing.Font)(resources.GetObject("btnPartName.Font")));
			this.btnPartName.Image = ((System.Drawing.Image)(resources.GetObject("btnPartName.Image")));
			this.btnPartName.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnPartName.ImageAlign")));
			this.btnPartName.ImageIndex = ((int)(resources.GetObject("btnPartName.ImageIndex")));
			this.btnPartName.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnPartName.ImeMode")));
			this.btnPartName.Location = ((System.Drawing.Point)(resources.GetObject("btnPartName.Location")));
			this.btnPartName.Name = "btnPartName";
			this.btnPartName.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnPartName.RightToLeft")));
			this.btnPartName.Size = ((System.Drawing.Size)(resources.GetObject("btnPartName.Size")));
			this.btnPartName.TabIndex = ((int)(resources.GetObject("btnPartName.TabIndex")));
			this.btnPartName.Text = resources.GetString("btnPartName.Text");
			this.btnPartName.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnPartName.TextAlign")));
			this.btnPartName.Visible = ((bool)(resources.GetObject("btnPartName.Visible")));
			this.btnPartName.Click += new System.EventHandler(this.btnPartName_Click);
			// 
			// DestroyMaterialReport
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
			this.Controls.Add(this.txtPartName);
			this.Controls.Add(this.txtProductionLine);
			this.Controls.Add(this.txtPartNo);
			this.Controls.Add(this.txtCategory);
			this.Controls.Add(this.txtModel);
			this.Controls.Add(this.txtDepartment);
			this.Controls.Add(this.lblPartName);
			this.Controls.Add(this.btnPartName);
			this.Controls.Add(this.lblProductionLine);
			this.Controls.Add(this.btnProductionLine);
			this.Controls.Add(this.cboYear);
			this.Controls.Add(this.cboMonth);
			this.Controls.Add(this.btnHelp);
			this.Controls.Add(this.btnClose);
			this.Controls.Add(this.btnExecute);
			this.Controls.Add(this.lblMonth);
			this.Controls.Add(this.lblCategory);
			this.Controls.Add(this.cboCCN);
			this.Controls.Add(this.lblCCN);
			this.Controls.Add(this.lblModel);
			this.Controls.Add(this.lblYear);
			this.Controls.Add(this.btnCategory);
			this.Controls.Add(this.lblPartNo);
			this.Controls.Add(this.btnDepartment);
			this.Controls.Add(this.lblDepartment);
			this.Controls.Add(this.btnPartNo);
			this.Enabled = ((bool)(resources.GetObject("$this.Enabled")));
			this.Font = ((System.Drawing.Font)(resources.GetObject("$this.Font")));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("$this.ImeMode")));
			this.Location = ((System.Drawing.Point)(resources.GetObject("$this.Location")));
			this.MaximizeBox = false;
			this.MaximumSize = ((System.Drawing.Size)(resources.GetObject("$this.MaximumSize")));
			this.MinimumSize = ((System.Drawing.Size)(resources.GetObject("$this.MinimumSize")));
			this.Name = "DestroyMaterialReport";
			this.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("$this.RightToLeft")));
			this.StartPosition = ((System.Windows.Forms.FormStartPosition)(resources.GetObject("$this.StartPosition")));
			this.Text = resources.GetString("$this.Text");
			this.Load += new System.EventHandler(this.DestroyMaterialReport_Load);
			((System.ComponentModel.ISupportInitialize)(this.cboCCN)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion
		
		#region Class's Methods
		
		private DataTable GetDestroyMaterialReportData()
		{
			const string EMPTY_STRING = "(0)";

			string strListDepartmentID = string.Empty;
			string strListProductionLineID = string.Empty;
			string strListCategoryID = string.Empty;
			string strListProductID = string.Empty;

			if(txtDepartment.Tag != null)
			{
				strListDepartmentID = "(0";
				foreach(DataRow drow in ((DataTable)txtDepartment.Tag).Rows)
				{
					strListDepartmentID += ", " + drow[MST_DepartmentTable.DEPARTMENTID_FLD].ToString();
				}
				strListDepartmentID += ")";
				
				//kill empty
				if(strListDepartmentID == EMPTY_STRING)
				{
					strListDepartmentID = string.Empty;
				}
			}

			if(txtProductionLine.Tag != null)
			{
				strListProductionLineID = "(0";
				foreach(DataRow drow in ((DataTable)txtProductionLine.Tag).Rows)
				{
					strListProductionLineID += ", " + drow[PRO_ProductionLineTable.PRODUCTIONLINEID_FLD].ToString();
				}
				strListProductionLineID += ")";

				//kill empty
				if(strListProductionLineID == EMPTY_STRING)
				{
					strListProductionLineID = string.Empty;
				}
			}

			if(txtCategory.Tag != null)
			{
				strListCategoryID = "(0";
				foreach(DataRow drow in ((DataTable)txtCategory.Tag).Rows)
				{
					strListCategoryID += ", " + drow[ITM_CategoryTable.CATEGORYID_FLD].ToString();
				}
				strListCategoryID += ")";

				//kill empty
				if(strListCategoryID == EMPTY_STRING)
				{
					strListCategoryID = string.Empty;
				}
			}

			if(txtPartNo.Tag != null)
			{
				strListProductID = "(0";
				foreach(DataRow drow in ((DataTable)txtPartNo.Tag).Rows)
				{
					strListProductID += ", " + drow[ITM_ProductTable.PRODUCTID_FLD].ToString();
				}
				strListProductID += ")";

				//kill empty
				if(strListProductID == EMPTY_STRING)
				{
					strListProductID = string.Empty;
				}
			}

			C1PrintPreviewDialogBO dbPrintPreview = new C1PrintPreviewDialogBO();

			return dbPrintPreview.GetDestroyMaterialData(cboCCN.SelectedValue.ToString(),
					cboYear.Text, cboMonth.Text, 
					strListDepartmentID, strListProductionLineID, 
					strListCategoryID, strListProductID);			
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="pstrTableName"></param>
		private void ClearValueInRelatedControl(string pstrTableName)
		{
			switch(pstrTableName)
			{
				case MST_DepartmentTable.TABLE_NAME:
					txtDepartment.Text = string.Empty;
					txtDepartment.Tag = null;

					txtProductionLine.Text = string.Empty;
					txtProductionLine.Tag = null;

					txtPartNo.Text = string.Empty;
					txtPartNo.Tag = null;

					txtPartName.Text = string.Empty;
					txtModel.Text = string.Empty;

					break;

				case PRO_ProductionLineTable.TABLE_NAME:
					txtProductionLine.Text = string.Empty;
					txtProductionLine.Tag = null;

					txtPartNo.Text = string.Empty;
					txtPartNo.Tag = null;

					txtPartName.Text = string.Empty;
					txtModel.Text = string.Empty;

					break;

				case ITM_CategoryTable.TABLE_NAME:
					txtCategory.Text = string.Empty;
					txtCategory.Tag = null;

					txtPartNo.Text = string.Empty;
					txtPartNo.Tag = null;

					txtPartName.Text = string.Empty;
					txtModel.Text = string.Empty;
					break;

				case ITM_ProductTable.TABLE_NAME:					
					txtPartNo.Text = string.Empty;
					txtPartNo.Tag = null;

					txtPartName.Text = string.Empty;
					txtModel.Text = string.Empty;
					break;
			}
		}

		/// <summary>
		/// Fill related data on controls when select Sale Department
		/// </summary>
		/// <param name="pblnAlwaysShowDialog"></param>
		/// <author> Tuan TQ, 01 Mar, 2006</author>
		private bool SelectProductionLine(bool pblnAlwaysShowDialog)
		{
			Hashtable htbCriteria = new Hashtable();

			if(txtDepartment.Tag == null)
			{
				string[] arrMessage = new string[2];
				arrMessage[0] = lblDepartment.Text;
				arrMessage[1] = lblProductionLine.Text;

				PCSMessageBox.Show(ErrorCode.MESSAGE_SELECT_ONE_BEFORE_SELECT_ONE, MessageBoxIcon.Warning, arrMessage);
				txtDepartment.Focus();
				return false;
			}

			string strWhereClause = PRO_ProductionLineTable.TABLE_NAME + "." + PRO_ProductionLineTable.DEPARTMENTID_FLD + " IN (0";

			foreach(DataRow drowCate in ((DataTable)txtDepartment.Tag).Rows)
			{
				strWhereClause += ", " + drowCate[MST_DepartmentTable.DEPARTMENTID_FLD].ToString();
			}

			strWhereClause += ")";

			//Call OpenSearchForm for selecting type
			DataTable dtbResult = FormControlComponents.OpenSearchFormForMultiSelectedRow(PRO_ProductionLineTable.TABLE_NAME, PRO_ProductionLineTable.CODE_FLD, txtProductionLine.Text, strWhereClause, pblnAlwaysShowDialog);
			
			// Fill values to form's controls
			if(dtbResult != null)
			{
				if(dtbResult.Rows.Count != 0)
				{
					string strSelectedValue = string.Empty;				
					foreach(DataRow drow in dtbResult.Rows)
					{
						strSelectedValue += drow[PRO_ProductionLineTable.CODE_FLD].ToString() + ", ";
					}
				
					txtProductionLine.Text = strSelectedValue.Substring(0, strSelectedValue.Length - 2);

					txtProductionLine.Tag = dtbResult;
				
					//Reset modify status
					txtProductionLine.Modified = false;				
				}
				else if(!pblnAlwaysShowDialog)
				{					
					txtProductionLine.Focus();
					return false;
				}				
			}
			else if(!pblnAlwaysShowDialog)
			{					
				txtProductionLine.Focus();
				return false;
			}

			return true;
		}


		/// <summary>
		/// Fill related data on controls when select Catagory
		/// </summary>
		/// <param name="pblnAlwaysShowDialog"></param>
		/// <author> Tuan TQ, 01 Mar, 2006</author>
		private bool SelectCategory(bool pblnAlwaysShowDialog)
		{
			//Call OpenSearchForm for selecting Master Location
			DataTable dtbResult = FormControlComponents.OpenSearchFormForMultiSelectedRow(ITM_CategoryTable.TABLE_NAME, ITM_CategoryTable.CODE_FLD, txtCategory.Text, string.Empty, pblnAlwaysShowDialog);
				
			// If has Master location matched searching condition, fill values to form's controls
			if(dtbResult != null)
			{
				if(dtbResult.Rows.Count != 0)
				{
					string strSelectedValue = string.Empty;				
					foreach(DataRow drow in dtbResult.Rows)
					{
						strSelectedValue += drow[ITM_CategoryTable.CODE_FLD].ToString() + ", ";
					}

					txtCategory.Text = strSelectedValue.Substring(0, strSelectedValue.Length - 2);

					txtCategory.Tag = dtbResult;
				
					//Reset modify status
					txtCategory.Modified = false;			
				}
				else if(!pblnAlwaysShowDialog)
				{					
					txtCategory.Focus();
					return false;
				}

			}
			else if(!pblnAlwaysShowDialog)
			{					
				txtCategory.Focus();
				return false;
			}

			return true;			
		}


		/// <summary>
		/// Fill related data on controls when select Model
		/// </summary>
		/// <param name="pblnAlwaysShowDialog"></param>
		/// <author> Tuan TQ, 01 Mar, 2006</author>
		private bool SelectDepartment(bool pblnAlwaysShowDialog)
		{			
			Hashtable htbCriteria = new Hashtable();
			DataTable dtbResult = null;
			
			if(cboCCN.SelectedValue == null)
			{
				string[] arrMessage = new string[2];
				arrMessage[0] = lblCCN.Text;
				arrMessage[1] = lblDepartment.Text;

				PCSMessageBox.Show(ErrorCode.MESSAGE_SELECT_ONE_BEFORE_SELECT_ONE, MessageBoxIcon.Warning, arrMessage);
				cboCCN.Focus();
				return false;
			}
			
			string strWhereClause = MST_DepartmentTable.TABLE_NAME + "." + MST_CCNTable.CCNID_FLD + "=" + cboCCN.SelectedValue.ToString();

			//Call OpenSearchForm for selecting MPS planning cycle
			dtbResult = FormControlComponents.OpenSearchFormForMultiSelectedRow(MST_DepartmentTable.TABLE_NAME, MST_DepartmentTable.CODE_FLD, txtDepartment.Text.Trim(), strWhereClause, pblnAlwaysShowDialog);
			
			// If has Master location matched searching condition, fill values to form's controls
			if (dtbResult != null)
			{
				if(dtbResult.Rows.Count != 0)
				{
					string strSelectedValue = string.Empty;				
					foreach(DataRow drow in dtbResult.Rows)
					{
						strSelectedValue += drow[MST_DepartmentTable.CODE_FLD].ToString() + ", ";
					}
				
					txtDepartment.Text = strSelectedValue.Substring(0, strSelectedValue.Length - 2);
					txtDepartment.Tag = dtbResult;
				
					//Reset modify status
					txtDepartment.Modified = false;						
				}
				else if(!pblnAlwaysShowDialog)
				{						
					txtDepartment.Focus();
					return false;
				}				
			}
			else if(!pblnAlwaysShowDialog)
			{						
				txtDepartment.Focus();
				return false;
			}		

			return true;
		}


		/// <summary>
		/// Fill related data on controls when select part no
		/// </summary>
		/// <param name="pblnAlwaysShowDialog"></param>
		/// <author> Tuan TQ, 01 Mar, 2006</author>
		private bool SelectPartNo(bool pblnAlwaysShowDialog)
		{			
			DataTable dtbResult = null;				
			
			if(txtProductionLine.Tag == null)
			{
				string[] arrMessage = new string[2];
				arrMessage[0] = lblProductionLine.Text;
				arrMessage[1] = lblPartNo.Text;

				PCSMessageBox.Show(ErrorCode.MESSAGE_SELECT_ONE_BEFORE_SELECT_ONE, MessageBoxIcon.Warning, arrMessage);
				txtProductionLine.Focus();
				return false;
			}
			
			string strWhereClause = string.Empty;

			if(txtCategory.Tag != null)
			{
				strWhereClause += ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.CATEGORYID_FLD + " IN (0";

				foreach(DataRow drowCate in ((DataTable)txtCategory.Tag).Rows)
				{
					strWhereClause += ", " + drowCate[ITM_CategoryTable.CATEGORYID_FLD].ToString();
				}
				strWhereClause += ")";				
			}

			if(txtProductionLine.Tag != null)
			{
				if(strWhereClause != string.Empty)
				{
					strWhereClause += " AND (" + ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.PRODUCTIONLINEID_FLD + " IN (0";
				}
				else
				{
					strWhereClause += " (" + ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.PRODUCTIONLINEID_FLD + " IN (0";
				}

				foreach(DataRow drowProductionLine in ((DataTable)txtProductionLine.Tag).Rows)
				{
					strWhereClause += ", " + drowProductionLine[PRO_ProductionLineTable.PRODUCTIONLINEID_FLD].ToString();
				}
				
				strWhereClause += "))";				
			}

			//Call OpenSearchForm for selecting MPS planning cycle
			dtbResult = FormControlComponents.OpenSearchFormForMultiSelectedRow(ITM_ProductTable.TABLE_NAME, ITM_ProductTable.CODE_FLD, txtPartNo.Text, strWhereClause, pblnAlwaysShowDialog);
			
			// Fill values to form's controls
			if (dtbResult != null)
			{	
				if(dtbResult.Rows.Count != 0)
				{
					string strPartNo = string.Empty;
					string strPartName = string.Empty;
					string strPartModel = string.Empty;

					foreach(DataRow drow in dtbResult.Rows)
					{
						strPartNo += drow[ITM_ProductTable.CODE_FLD].ToString() + ", ";
						strPartName += drow[ITM_ProductTable.DESCRIPTION_FLD].ToString() + ", ";
						strPartModel += drow[ITM_ProductTable.REVISION_FLD].ToString() + ", ";
					}

					txtPartNo.Text = strPartNo.Substring(0, strPartNo.Length - 2);
					txtPartName.Text = strPartName.Substring(0, strPartName.Length - 2);
					txtModel.Text = strPartModel.Substring(0, strPartModel.Length - 2);

					txtPartNo.Tag = dtbResult;

					//Reset modify status
					txtPartNo.Modified = false;
					txtPartName.Modified = false;
				}
				else if(!pblnAlwaysShowDialog)
				{			
					txtPartNo.Tag = null;
					txtPartNo.Focus();

					return false;			
				}

			}
			else if(!pblnAlwaysShowDialog)
			{			
				txtPartNo.Tag = null;
				txtPartNo.Focus();

				return false;			
			}

			return true;			
		}


		/// <summary>
		/// Fill related data on controls when select part no
		/// </summary>
		/// <param name="pblnAlwaysShowDialog"></param>
		/// <author> Tuan TQ, 01 Mar, 2006</author>
		private bool SelectPartName(bool pblnAlwaysShowDialog)
		{	
			string strWhereClause = string.Empty;
			
			DataTable dtbResult = null;
			
			if(txtProductionLine.Tag == null)
			{
				string[] arrMessage = new string[2];
				arrMessage[0] = lblProductionLine.Text;
				arrMessage[1] = lblPartNo.Text;

				PCSMessageBox.Show(ErrorCode.MESSAGE_SELECT_ONE_BEFORE_SELECT_ONE, MessageBoxIcon.Warning, arrMessage);
				txtProductionLine.Focus();
				return false;
			}
			
			if(txtCategory.Tag != null)
			{
				strWhereClause += ITM_ProductTable.TABLE_NAME + "." +  ITM_ProductTable.CATEGORYID_FLD + " IN (0";

				foreach(DataRow drowCate in ((DataTable)txtCategory.Tag).Rows)
				{
					strWhereClause += ", " + drowCate[ITM_CategoryTable.CATEGORYID_FLD].ToString();
				}

				strWhereClause += ")";				
			}

			if(txtProductionLine.Tag != null)
			{
				if(strWhereClause != string.Empty)
				{
					strWhereClause += " AND (" + ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.PRODUCTIONLINEID_FLD + " IN (0";
				}
				else
				{
					strWhereClause += " (" + ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.PRODUCTIONLINEID_FLD + " IN (0";
				}

				foreach(DataRow drowProductionLine in ((DataTable)txtProductionLine.Tag).Rows)
				{
					strWhereClause += ", " + drowProductionLine[PRO_ProductionLineTable.PRODUCTIONLINEID_FLD].ToString();
				}

				strWhereClause += "))";
			}

			//Call OpenSearchForm for selecting MPS planning cycle
			dtbResult = FormControlComponents.OpenSearchFormForMultiSelectedRow(ITM_ProductTable.TABLE_NAME, ITM_ProductTable.DESCRIPTION_FLD, txtPartName.Text, strWhereClause, pblnAlwaysShowDialog);
			
			// Fill values to form's controls
			if (dtbResult != null)
			{	
				if(dtbResult.Rows.Count != 0)
				{
					string strPartNo = string.Empty;
					string strPartName = string.Empty;
					string strPartModel = string.Empty;

					foreach(DataRow drow in dtbResult.Rows)
					{
						strPartNo += drow[ITM_ProductTable.CODE_FLD].ToString() + ", ";
						strPartName += drow[ITM_ProductTable.DESCRIPTION_FLD].ToString() + ", ";
						strPartModel += drow[ITM_ProductTable.REVISION_FLD].ToString() + ", ";
					}

					txtPartNo.Text = strPartNo.Substring(0, strPartNo.Length - 2);
					txtPartName.Text = strPartName.Substring(0, strPartName.Length - 2);
					txtModel.Text = strPartModel.Substring(0, strPartModel.Length - 2);

					txtPartNo.Tag = dtbResult;

					//Reset modify status
					txtPartNo.Modified = false;
					txtPartName.Modified = false;
				}
				else if(!pblnAlwaysShowDialog)
				{
					txtPartNo.Tag = null;
					txtPartName.Focus();

					return false;
				}			

			}
			else if(!pblnAlwaysShowDialog)
			{
				txtPartNo.Tag = null;
				txtPartName.Focus();

				return false;			
			}

			return true;			
		}

		#endregion Class's Methods		

		#region Event Processing		
		
		private void DestroyMaterialReport_Load(object sender, System.EventArgs e)
		{
			const int ADDED_YEAR = 2;
			const int SUBTRACTED_YEAR = 5;
			const string PAD_CHAR = "0";

			const string METHOD_NAME = THIS + ".DestroyMaterialReport_Load()";
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

				//Load CCN and set default CCN
				UtilsBO boUtils = new UtilsBO();
				DataSet dstCCN = boUtils.ListCCN();
				DateTime dtmServerDate = boUtils.GetDBDate();
				FormControlComponents.PutDataIntoC1ComboBox(cboCCN, dstCCN.Tables[MST_CCNTable.TABLE_NAME], MST_CCNTable.CODE_FLD, MST_CCNTable.CCNID_FLD, MST_CCNTable.TABLE_NAME);				
				//Set default CCN for CNN combobox
				if (SystemProperty.CCNID != 0)
				{
					cboCCN.SelectedValue = SystemProperty.CCNID;
				}
				else
				{
					cboCCN.SelectedIndex = 0;
				}

				FormControlComponents.InitMonthComboBox(cboMonth, true);				
				FormControlComponents.InitYearComboBox(cboYear, dtmServerDate.Year - SUBTRACTED_YEAR, dtmServerDate.Year + ADDED_YEAR, true);

				cboYear.Text  = dtmServerDate.Year.ToString();
				cboMonth.Text = (dtmServerDate.Month < 10)? PAD_CHAR + dtmServerDate.Month.ToString() : dtmServerDate.Month.ToString();

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

		private void btnDepartment_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnDepartment_Click()";

			try
			{
				SelectDepartment(true);
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
		
		private void btnPartName_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnPartName_Click()";

			try
			{
				SelectPartName(true);
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

		private void btnPartNo_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnPartNo_Click()";

			try
			{
				SelectPartNo(true);
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

		private void btnCategory_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnCategory_Click()";

			try
			{
				SelectCategory(true);
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

		private void btnProductionLine_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnProductionLine_Click()";

			try
			{
				SelectProductionLine(true);
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

		private void btnExecute_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnExecute_Click()";	

			const int MAX_LENGTH = 100;

			//Report field names
			const string RPT_YEAR	   = "Year";
			const string RPT_MONTH  = "Month";			
			const string RPT_CATEGORY    = "Category";
			const string RPT_DEPARTMENT      = "Department";			
			const string RPT_PRODUCTIONLINE   = "Production Line";
			const string RPT_PART_NO = "Part No.";
			const string RPT_PART_NAME = "Part Name";
			const string RPT_MODEL = "Model";
			const string RPT_COMPANY  = "fldCompany";
			const string RPT_HOME_CURRENCY  = "fldHomeCurrency";

			const string RPT_PAGE_HEADER = "PageHeader";			
			const string RPT_TITLE_FIELD = "fldTitle";

			try
			{				
				string strReportPath = Application.StartupPath;

				//Validate data; user must select CCN first
				if(cboCCN.SelectedValue == null || cboCCN.Text.Trim() == string.Empty)
				{
					this.Cursor = Cursors.Default;
					string[] arrParams = {lblCCN.Text};
					PCSMessageBox.Show(ErrorCode.MESSAGE_MANDATORY_FIELD_REQUIRED, MessageBoxIcon.Exclamation, arrParams);					
					cboCCN.Focus();
					return;
				}

				//Check if user does not select Year
				if(cboYear.Text.Trim() == string.Empty)
				{
					this.Cursor = Cursors.Default;
					string[] arrParams = {lblYear.Text};
					PCSMessageBox.Show(ErrorCode.MESSAGE_MANDATORY_FIELD_REQUIRED, MessageBoxIcon.Exclamation, arrParams);
					cboYear.Focus();					
					return;
				}
				
				if(cboMonth.Text.Trim() == string.Empty)
				{
					string[] arrParams = {lblMonth.Text};
					PCSMessageBox.Show(ErrorCode.MESSAGE_MANDATORY_FIELD_REQUIRED, MessageBoxIcon.Exclamation, arrParams);
					cboMonth.Focus();
					return;
				}
				
				//Change cursor to wait
				this.Cursor = Cursors.WaitCursor;
				
				int intIndex = strReportPath.IndexOf(APPLICATION_PATH);
				
				//Validate report layout path
				if(intIndex > -1)
				{
					strReportPath = strReportPath.Substring(0, intIndex);
				}

				if(strReportPath.Substring(strReportPath.Length -1) == @"\")
				{
					strReportPath += Constants.REPORT_DEFINITION_STORE_LOCATION;
				}
				else
				{
					strReportPath += @"\" + Constants.REPORT_DEFINITION_STORE_LOCATION;
				}				
				
				// check report layout file is exist or not
				if(!File.Exists(strReportPath + @"\" + DESTROY_MATERIAL_REPORT))
				{
					this.Cursor = Cursors.Default;
					PCSMessageBox.Show(ErrorCode.MESSAGE_REPORT_TEMPLATE_FILE_NOT_FOUND, MessageBoxIcon.Error);					
					return;
				}
				
				//create print preview object
				PCSComUtils.Framework.ReportFrame.BO.C1PrintPreviewDialogBO boPrintPreview = new PCSComUtils.Framework.ReportFrame.BO.C1PrintPreviewDialogBO();
				DataTable dtbData = GetDestroyMaterialReportData();

				//Create builder object
				ReportWithSubReportBuilder reportBuilder = new ReportWithSubReportBuilder();
				//Set report name
				reportBuilder.ReportName = REPORT_NAME;
				//Set Datasource
				reportBuilder.SourceDataTable = dtbData;
				//Set report layout location
				reportBuilder.ReportDefinitionFolder = strReportPath;
				reportBuilder.ReportLayoutFile = DESTROY_MATERIAL_REPORT;				
				
				reportBuilder.UseLayoutFile = true;
				reportBuilder.MakeDataTableForRender();			

				// and show it in preview dialog				
				PCSUtils.Framework.ReportFrame.C1PrintPreviewDialog	printPreview = new PCSUtils.Framework.ReportFrame.C1PrintPreviewDialog();
								
				//Attach report viewer
				reportBuilder.ReportViewer = printPreview.ReportViewer;				
				reportBuilder.RenderReport();				
				
				//Draw parameters				
				System.Collections.Specialized.NameValueCollection arrParamAndValue = new System.Collections.Specialized.NameValueCollection();
				arrParamAndValue.Add(RPT_YEAR, cboYear.Text);
				arrParamAndValue.Add(RPT_MONTH, cboMonth.Text);				

				if(txtDepartment.Text != string.Empty)
				{
					arrParamAndValue.Add(RPT_DEPARTMENT, (txtDepartment.Text.Length > MAX_LENGTH)?txtDepartment.Text.Substring(0, MAX_LENGTH) + "...":txtDepartment.Text);
				}

				if(txtProductionLine.Text != string.Empty)
				{
					arrParamAndValue.Add(RPT_PRODUCTIONLINE, (txtProductionLine.Text.Length > MAX_LENGTH)?txtProductionLine.Text.Substring(0, MAX_LENGTH) + "...":txtProductionLine.Text);
				}

				if(txtCategory.Text != string.Empty)
				{
					arrParamAndValue.Add(RPT_CATEGORY, (txtCategory.Text.Length > MAX_LENGTH)?txtCategory.Text.Substring(0, MAX_LENGTH) + "...":txtCategory.Text);
				}

				if(txtPartNo.Text != string.Empty)
				{
					arrParamAndValue.Add(RPT_PART_NO, (txtPartNo.Text.Length > MAX_LENGTH)?txtPartNo.Text.Substring(0, MAX_LENGTH) + "...":txtPartNo.Text);
				}

				if(txtPartName.Text != string.Empty)
				{
					arrParamAndValue.Add(RPT_PART_NAME, (txtPartName.Text.Length > MAX_LENGTH)?txtPartName.Text.Substring(0, MAX_LENGTH) + "...":txtPartName.Text);
				}

				if(txtModel.Text != string.Empty)
				{
					arrParamAndValue.Add(RPT_MODEL, (txtModel.Text.Length > MAX_LENGTH)?txtModel.Text.Substring(0, MAX_LENGTH) + "...":txtModel.Text);
				}
				
				//Anchor the Parameter drawing canvas cordinate to the fldTitle
				C1.C1Report.Field fldTitle = reportBuilder.GetFieldByName(RPT_TITLE_FIELD);
				double dblStartX = fldTitle.Left;
				double dblStartY = fldTitle.Top  + 1.3 * fldTitle.RenderHeight;
				reportBuilder.GetSectionByName(RPT_PAGE_HEADER).CanGrow = true;
				reportBuilder.DrawParameters(reportBuilder.GetSectionByName(RPT_PAGE_HEADER), dblStartX, dblStartY, arrParamAndValue, reportBuilder.Report.Font.Size);
				
				//Change form title
				try
				{
					printPreview.FormTitle = reportBuilder.GetFieldByName(RPT_TITLE_FIELD).Text + Constants.WHITE_SPACE + cboYear.Text;
				}
				catch{}
				
				//Company & Home Currency Info
				reportBuilder.DrawPredefinedField(RPT_COMPANY, SystemProperty.SytemParams.Get(SystemParam.COMPANY_FULL_NAME));
				reportBuilder.DrawPredefinedField(RPT_HOME_CURRENCY, SystemProperty.HomeCurrency);

				reportBuilder.RefreshReport();				
				
				printPreview.Show();
			}
			catch(Exception ex)
			{				
				//
				PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxIcon.Error);
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
			finally
			{
				this.Cursor = Cursors.Default;
			}
		}

		private void btnHelp_Click(object sender, System.EventArgs e)
		{
		
		}

		private void btnClose_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}
		
		private void txtDepartment_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtDepartment_Validating()";
			try
			{				
				//Exit immediately if empty or in default mode				
				if(txtDepartment.Text.Length == 0)
				{
					ClearValueInRelatedControl(MST_DepartmentTable.TABLE_NAME);
					return;
				}
				else if(!txtDepartment.Modified)
				{
					return;
				}
				
				e.Cancel = !SelectDepartment(false);
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

		private void txtPartNo_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtPartNo_Validating()";
			try
			{				
				//Exit immediately if empty or in default mode				
				if(txtPartNo.Text.Length == 0)
				{
					ClearValueInRelatedControl(ITM_ProductTable.TABLE_NAME);
					return;
				}
				else if(!txtPartNo.Modified)
				{
					return;
				}
				
				e.Cancel = !SelectPartNo(false);
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

		private void txtCategory_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtCategory_Validating()";
			try
			{				
				//Exit immediately if empty or in default mode				
				if(txtCategory.Text.Length == 0)
				{
					ClearValueInRelatedControl(ITM_CategoryTable.TABLE_NAME);
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

		private void txtProductionLine_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtProductionLine_Validating()";
			try
			{				
				//Exit immediately if empty or in default mode				
				if(txtProductionLine.Text.Length == 0)
				{
					ClearValueInRelatedControl(PRO_ProductionLineTable.TABLE_NAME);
					return;
				}
				else if(!txtProductionLine.Modified)
				{
					return;
				}
				
				e.Cancel = !SelectProductionLine(false);
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
		
		private void txtDepartment_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtDepartment_KeyDown()";

			try
			{
				if ((e.KeyCode == Keys.F4) && (btnDepartment.Enabled))
				{
					SelectDepartment(true);
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
		
		private void txtProductionLine_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtProductionLine_KeyDown()";

			try
			{
				if ((e.KeyCode == Keys.F4) && (btnProductionLine.Enabled))
				{
					SelectProductionLine(true);
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
		
		private void cboYear_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			cboMonth.SelectedIndex = 0;			
		}

			
		
		private void txtPartName_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtPartName_Validating()";
			try
			{				
				//Exit immediately if empty or in default mode				
				if(txtPartName.Text.Length == 0)
				{
					ClearValueInRelatedControl(ITM_ProductTable.TABLE_NAME);
					return;
				}
				else if(!txtPartName.Modified)
				{
					return;
				}
				
				e.Cancel = !SelectPartName(false);
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
		
		private void txtPartName_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtProductionLine_KeyDown()";

			try
			{
				if ((e.KeyCode == Keys.F4) && (btnPartName.Enabled))
				{
					SelectPartName(true);
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
		
		private void txtPartNo_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtPartNo_KeyDown()";

			try
			{
				if ((e.KeyCode == Keys.F4) && (btnPartNo.Enabled))
				{
					SelectPartNo(true);
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

		#endregion Event Processing	

		
		
	}
}
