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
using PCSComUtils.MasterSetup.DS;
using PCSComUtils.PCSExc;
using PCSUtils.Log;
using PCSUtils.Utils;

namespace PCSSale.Order
{
	/// <summary>
	/// Render Order Summary Report.
	/// </summary>
	public class OrderSummaryReport : System.Windows.Forms.Form
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
		private System.Windows.Forms.Label lblFromMonth;
		private System.Windows.Forms.Label lblTo;
		private System.Windows.Forms.Label lblCustomer;
		private System.Windows.Forms.TextBox txtCustomer;
		private System.Windows.Forms.Button btnCustomer;
		private System.Windows.Forms.Label lblType;
		private System.Windows.Forms.Button btnType;
		private System.Windows.Forms.TextBox txtType;
		private System.Windows.Forms.Button btnExecute;
		private System.Windows.Forms.Button btnHelp;
		private System.Windows.Forms.Label lblCategory;
		private System.Windows.Forms.Button btnCategory;
		private System.Windows.Forms.TextBox txtCategory;
		private System.Windows.Forms.Button btnModel;
		private System.Windows.Forms.TextBox txtModel;
		private System.Windows.Forms.Label lblModel;
		private System.Windows.Forms.ComboBox cboFromMonth;
		private System.Windows.Forms.ComboBox cboToMonth;
		private System.Windows.Forms.TextBox txtCustomerName;		
		private System.ComponentModel.Container components = null;

		#endregion Generated Declaration
		
		#region Constants
		
		private const string THIS = "PCSProcurement.Purchase.OrderSummaryReport";

		const string REPORTFLD_TITLE = "fldTitle";

		private const string ZERO_STRING = "0";
		private const string REVISION_VIEW = "v_ProductRevision";
		private const string REVISION_COLUMN = "Revision";
		private const string VENDOR_CUSTOMER_VIEW = "V_VendorCustomer";		
		private const string CUSTOMER_COLUMN = "Customer";
		
		private const string APPLICATION_PATH     = @"PCSMain\bin\Debug";
		private const string ORDER_SUMMARY_REPORT = "OrderSummaryReport.xml";
		private const string REPORT_NAME		  = "OrderSummaryReport";
		
		private const string TRANSMONTH_FLD = "TransMonth";
		private const string TRANSYEAR_FLD = "TransYear";

		private const string CCNID_FLD = "CCNID";
		private const string SELLING_UM_FLD = "SellingUM";		
		private const string PARTY_CODE_FLD = "MST_PartyCode";
		private const string CATEGORY_CODE_FLD = "ITM_CategoryCode";
		private const string PRODUCT_CODE_FLD = "ITM_ProductCode";
		private const string PRODUCT_DESCRIPTION_FLD = "ITM_ProductDescription";
		private const string PRODUCT_REVISION_FLD = "ITM_ProductRevision";
		private const string SALE_TYPE_CODE_FLD = "SO_SaleTypeCode";
		private const string QUANTITY_FIELD_PREFIX = "MonthQuantity_";
		private const string TOTALSET_FLD = "TotalSet";
		private const string CAPACITY_FLD = "Capacity";
		private const string TOTAL_CAPACITY_FLD = "TotalCapacity";	

		#endregion Constants

		#endregion Declaration
		
		#region Constructor, Destructor
		
		public OrderSummaryReport()
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(OrderSummaryReport));
			this.btnExecute = new System.Windows.Forms.Button();
			this.btnClose = new System.Windows.Forms.Button();
			this.cboCCN = new C1.Win.C1List.C1Combo();
			this.lblCCN = new System.Windows.Forms.Label();
			this.cboYear = new System.Windows.Forms.ComboBox();
			this.cboToMonth = new System.Windows.Forms.ComboBox();
			this.cboFromMonth = new System.Windows.Forms.ComboBox();
			this.btnModel = new System.Windows.Forms.Button();
			this.txtModel = new System.Windows.Forms.TextBox();
			this.lblModel = new System.Windows.Forms.Label();
			this.btnCategory = new System.Windows.Forms.Button();
			this.txtCategory = new System.Windows.Forms.TextBox();
			this.lblCategory = new System.Windows.Forms.Label();
			this.btnType = new System.Windows.Forms.Button();
			this.txtType = new System.Windows.Forms.TextBox();
			this.lblType = new System.Windows.Forms.Label();
			this.btnCustomer = new System.Windows.Forms.Button();
			this.txtCustomerName = new System.Windows.Forms.TextBox();
			this.txtCustomer = new System.Windows.Forms.TextBox();
			this.lblCustomer = new System.Windows.Forms.Label();
			this.lblTo = new System.Windows.Forms.Label();
			this.lblFromMonth = new System.Windows.Forms.Label();
			this.lblYear = new System.Windows.Forms.Label();
			this.btnHelp = new System.Windows.Forms.Button();
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
			// cboToMonth
			// 
			this.cboToMonth.AccessibleDescription = resources.GetString("cboToMonth.AccessibleDescription");
			this.cboToMonth.AccessibleName = resources.GetString("cboToMonth.AccessibleName");
			this.cboToMonth.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("cboToMonth.Anchor")));
			this.cboToMonth.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cboToMonth.BackgroundImage")));
			this.cboToMonth.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("cboToMonth.Dock")));
			this.cboToMonth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboToMonth.Enabled = ((bool)(resources.GetObject("cboToMonth.Enabled")));
			this.cboToMonth.Font = ((System.Drawing.Font)(resources.GetObject("cboToMonth.Font")));
			this.cboToMonth.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("cboToMonth.ImeMode")));
			this.cboToMonth.IntegralHeight = ((bool)(resources.GetObject("cboToMonth.IntegralHeight")));
			this.cboToMonth.ItemHeight = ((int)(resources.GetObject("cboToMonth.ItemHeight")));
			this.cboToMonth.Location = ((System.Drawing.Point)(resources.GetObject("cboToMonth.Location")));
			this.cboToMonth.MaxDropDownItems = ((int)(resources.GetObject("cboToMonth.MaxDropDownItems")));
			this.cboToMonth.MaxLength = ((int)(resources.GetObject("cboToMonth.MaxLength")));
			this.cboToMonth.Name = "cboToMonth";
			this.cboToMonth.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("cboToMonth.RightToLeft")));
			this.cboToMonth.Size = ((System.Drawing.Size)(resources.GetObject("cboToMonth.Size")));
			this.cboToMonth.TabIndex = ((int)(resources.GetObject("cboToMonth.TabIndex")));
			this.cboToMonth.Text = resources.GetString("cboToMonth.Text");
			this.cboToMonth.Visible = ((bool)(resources.GetObject("cboToMonth.Visible")));
			// 
			// cboFromMonth
			// 
			this.cboFromMonth.AccessibleDescription = resources.GetString("cboFromMonth.AccessibleDescription");
			this.cboFromMonth.AccessibleName = resources.GetString("cboFromMonth.AccessibleName");
			this.cboFromMonth.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("cboFromMonth.Anchor")));
			this.cboFromMonth.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cboFromMonth.BackgroundImage")));
			this.cboFromMonth.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("cboFromMonth.Dock")));
			this.cboFromMonth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboFromMonth.Enabled = ((bool)(resources.GetObject("cboFromMonth.Enabled")));
			this.cboFromMonth.Font = ((System.Drawing.Font)(resources.GetObject("cboFromMonth.Font")));
			this.cboFromMonth.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("cboFromMonth.ImeMode")));
			this.cboFromMonth.IntegralHeight = ((bool)(resources.GetObject("cboFromMonth.IntegralHeight")));
			this.cboFromMonth.ItemHeight = ((int)(resources.GetObject("cboFromMonth.ItemHeight")));
			this.cboFromMonth.Location = ((System.Drawing.Point)(resources.GetObject("cboFromMonth.Location")));
			this.cboFromMonth.MaxDropDownItems = ((int)(resources.GetObject("cboFromMonth.MaxDropDownItems")));
			this.cboFromMonth.MaxLength = ((int)(resources.GetObject("cboFromMonth.MaxLength")));
			this.cboFromMonth.Name = "cboFromMonth";
			this.cboFromMonth.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("cboFromMonth.RightToLeft")));
			this.cboFromMonth.Size = ((System.Drawing.Size)(resources.GetObject("cboFromMonth.Size")));
			this.cboFromMonth.TabIndex = ((int)(resources.GetObject("cboFromMonth.TabIndex")));
			this.cboFromMonth.Text = resources.GetString("cboFromMonth.Text");
			this.cboFromMonth.Visible = ((bool)(resources.GetObject("cboFromMonth.Visible")));
			// 
			// btnModel
			// 
			this.btnModel.AccessibleDescription = resources.GetString("btnModel.AccessibleDescription");
			this.btnModel.AccessibleName = resources.GetString("btnModel.AccessibleName");
			this.btnModel.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnModel.Anchor")));
			this.btnModel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnModel.BackgroundImage")));
			this.btnModel.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnModel.Dock")));
			this.btnModel.Enabled = ((bool)(resources.GetObject("btnModel.Enabled")));
			this.btnModel.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnModel.FlatStyle")));
			this.btnModel.Font = ((System.Drawing.Font)(resources.GetObject("btnModel.Font")));
			this.btnModel.Image = ((System.Drawing.Image)(resources.GetObject("btnModel.Image")));
			this.btnModel.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnModel.ImageAlign")));
			this.btnModel.ImageIndex = ((int)(resources.GetObject("btnModel.ImageIndex")));
			this.btnModel.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnModel.ImeMode")));
			this.btnModel.Location = ((System.Drawing.Point)(resources.GetObject("btnModel.Location")));
			this.btnModel.Name = "btnModel";
			this.btnModel.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnModel.RightToLeft")));
			this.btnModel.Size = ((System.Drawing.Size)(resources.GetObject("btnModel.Size")));
			this.btnModel.TabIndex = ((int)(resources.GetObject("btnModel.TabIndex")));
			this.btnModel.Text = resources.GetString("btnModel.Text");
			this.btnModel.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnModel.TextAlign")));
			this.btnModel.Visible = ((bool)(resources.GetObject("btnModel.Visible")));
			this.btnModel.Click += new System.EventHandler(this.btnModel_Click);
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
			this.txtModel.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("txtModel.RightToLeft")));
			this.txtModel.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("txtModel.ScrollBars")));
			this.txtModel.Size = ((System.Drawing.Size)(resources.GetObject("txtModel.Size")));
			this.txtModel.TabIndex = ((int)(resources.GetObject("txtModel.TabIndex")));
			this.txtModel.Text = resources.GetString("txtModel.Text");
			this.txtModel.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("txtModel.TextAlign")));
			this.txtModel.Visible = ((bool)(resources.GetObject("txtModel.Visible")));
			this.txtModel.WordWrap = ((bool)(resources.GetObject("txtModel.WordWrap")));
			this.txtModel.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtModel_KeyDown);
			this.txtModel.Validating += new System.ComponentModel.CancelEventHandler(this.txtModel_Validating);
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
			// btnType
			// 
			this.btnType.AccessibleDescription = resources.GetString("btnType.AccessibleDescription");
			this.btnType.AccessibleName = resources.GetString("btnType.AccessibleName");
			this.btnType.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnType.Anchor")));
			this.btnType.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnType.BackgroundImage")));
			this.btnType.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnType.Dock")));
			this.btnType.Enabled = ((bool)(resources.GetObject("btnType.Enabled")));
			this.btnType.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnType.FlatStyle")));
			this.btnType.Font = ((System.Drawing.Font)(resources.GetObject("btnType.Font")));
			this.btnType.Image = ((System.Drawing.Image)(resources.GetObject("btnType.Image")));
			this.btnType.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnType.ImageAlign")));
			this.btnType.ImageIndex = ((int)(resources.GetObject("btnType.ImageIndex")));
			this.btnType.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnType.ImeMode")));
			this.btnType.Location = ((System.Drawing.Point)(resources.GetObject("btnType.Location")));
			this.btnType.Name = "btnType";
			this.btnType.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnType.RightToLeft")));
			this.btnType.Size = ((System.Drawing.Size)(resources.GetObject("btnType.Size")));
			this.btnType.TabIndex = ((int)(resources.GetObject("btnType.TabIndex")));
			this.btnType.Text = resources.GetString("btnType.Text");
			this.btnType.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnType.TextAlign")));
			this.btnType.Visible = ((bool)(resources.GetObject("btnType.Visible")));
			this.btnType.Click += new System.EventHandler(this.btnType_Click);
			// 
			// txtType
			// 
			this.txtType.AccessibleDescription = resources.GetString("txtType.AccessibleDescription");
			this.txtType.AccessibleName = resources.GetString("txtType.AccessibleName");
			this.txtType.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("txtType.Anchor")));
			this.txtType.AutoSize = ((bool)(resources.GetObject("txtType.AutoSize")));
			this.txtType.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("txtType.BackgroundImage")));
			this.txtType.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("txtType.Dock")));
			this.txtType.Enabled = ((bool)(resources.GetObject("txtType.Enabled")));
			this.txtType.Font = ((System.Drawing.Font)(resources.GetObject("txtType.Font")));
			this.txtType.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("txtType.ImeMode")));
			this.txtType.Location = ((System.Drawing.Point)(resources.GetObject("txtType.Location")));
			this.txtType.MaxLength = ((int)(resources.GetObject("txtType.MaxLength")));
			this.txtType.Multiline = ((bool)(resources.GetObject("txtType.Multiline")));
			this.txtType.Name = "txtType";
			this.txtType.PasswordChar = ((char)(resources.GetObject("txtType.PasswordChar")));
			this.txtType.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("txtType.RightToLeft")));
			this.txtType.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("txtType.ScrollBars")));
			this.txtType.Size = ((System.Drawing.Size)(resources.GetObject("txtType.Size")));
			this.txtType.TabIndex = ((int)(resources.GetObject("txtType.TabIndex")));
			this.txtType.Text = resources.GetString("txtType.Text");
			this.txtType.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("txtType.TextAlign")));
			this.txtType.Visible = ((bool)(resources.GetObject("txtType.Visible")));
			this.txtType.WordWrap = ((bool)(resources.GetObject("txtType.WordWrap")));
			this.txtType.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtType_KeyDown);
			this.txtType.Validating += new System.ComponentModel.CancelEventHandler(this.txtType_Validating);
			// 
			// lblType
			// 
			this.lblType.AccessibleDescription = resources.GetString("lblType.AccessibleDescription");
			this.lblType.AccessibleName = resources.GetString("lblType.AccessibleName");
			this.lblType.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblType.Anchor")));
			this.lblType.AutoSize = ((bool)(resources.GetObject("lblType.AutoSize")));
			this.lblType.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblType.Dock")));
			this.lblType.Enabled = ((bool)(resources.GetObject("lblType.Enabled")));
			this.lblType.Font = ((System.Drawing.Font)(resources.GetObject("lblType.Font")));
			this.lblType.Image = ((System.Drawing.Image)(resources.GetObject("lblType.Image")));
			this.lblType.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblType.ImageAlign")));
			this.lblType.ImageIndex = ((int)(resources.GetObject("lblType.ImageIndex")));
			this.lblType.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblType.ImeMode")));
			this.lblType.Location = ((System.Drawing.Point)(resources.GetObject("lblType.Location")));
			this.lblType.Name = "lblType";
			this.lblType.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblType.RightToLeft")));
			this.lblType.Size = ((System.Drawing.Size)(resources.GetObject("lblType.Size")));
			this.lblType.TabIndex = ((int)(resources.GetObject("lblType.TabIndex")));
			this.lblType.Text = resources.GetString("lblType.Text");
			this.lblType.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblType.TextAlign")));
			this.lblType.Visible = ((bool)(resources.GetObject("lblType.Visible")));
			// 
			// btnCustomer
			// 
			this.btnCustomer.AccessibleDescription = resources.GetString("btnCustomer.AccessibleDescription");
			this.btnCustomer.AccessibleName = resources.GetString("btnCustomer.AccessibleName");
			this.btnCustomer.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnCustomer.Anchor")));
			this.btnCustomer.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCustomer.BackgroundImage")));
			this.btnCustomer.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnCustomer.Dock")));
			this.btnCustomer.Enabled = ((bool)(resources.GetObject("btnCustomer.Enabled")));
			this.btnCustomer.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnCustomer.FlatStyle")));
			this.btnCustomer.Font = ((System.Drawing.Font)(resources.GetObject("btnCustomer.Font")));
			this.btnCustomer.Image = ((System.Drawing.Image)(resources.GetObject("btnCustomer.Image")));
			this.btnCustomer.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnCustomer.ImageAlign")));
			this.btnCustomer.ImageIndex = ((int)(resources.GetObject("btnCustomer.ImageIndex")));
			this.btnCustomer.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnCustomer.ImeMode")));
			this.btnCustomer.Location = ((System.Drawing.Point)(resources.GetObject("btnCustomer.Location")));
			this.btnCustomer.Name = "btnCustomer";
			this.btnCustomer.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnCustomer.RightToLeft")));
			this.btnCustomer.Size = ((System.Drawing.Size)(resources.GetObject("btnCustomer.Size")));
			this.btnCustomer.TabIndex = ((int)(resources.GetObject("btnCustomer.TabIndex")));
			this.btnCustomer.Text = resources.GetString("btnCustomer.Text");
			this.btnCustomer.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnCustomer.TextAlign")));
			this.btnCustomer.Visible = ((bool)(resources.GetObject("btnCustomer.Visible")));
			this.btnCustomer.Click += new System.EventHandler(this.btnCustomer_Click);
			// 
			// txtCustomerName
			// 
			this.txtCustomerName.AccessibleDescription = resources.GetString("txtCustomerName.AccessibleDescription");
			this.txtCustomerName.AccessibleName = resources.GetString("txtCustomerName.AccessibleName");
			this.txtCustomerName.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("txtCustomerName.Anchor")));
			this.txtCustomerName.AutoSize = ((bool)(resources.GetObject("txtCustomerName.AutoSize")));
			this.txtCustomerName.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("txtCustomerName.BackgroundImage")));
			this.txtCustomerName.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("txtCustomerName.Dock")));
			this.txtCustomerName.Enabled = ((bool)(resources.GetObject("txtCustomerName.Enabled")));
			this.txtCustomerName.Font = ((System.Drawing.Font)(resources.GetObject("txtCustomerName.Font")));
			this.txtCustomerName.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("txtCustomerName.ImeMode")));
			this.txtCustomerName.Location = ((System.Drawing.Point)(resources.GetObject("txtCustomerName.Location")));
			this.txtCustomerName.MaxLength = ((int)(resources.GetObject("txtCustomerName.MaxLength")));
			this.txtCustomerName.Multiline = ((bool)(resources.GetObject("txtCustomerName.Multiline")));
			this.txtCustomerName.Name = "txtCustomerName";
			this.txtCustomerName.PasswordChar = ((char)(resources.GetObject("txtCustomerName.PasswordChar")));
			this.txtCustomerName.ReadOnly = true;
			this.txtCustomerName.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("txtCustomerName.RightToLeft")));
			this.txtCustomerName.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("txtCustomerName.ScrollBars")));
			this.txtCustomerName.Size = ((System.Drawing.Size)(resources.GetObject("txtCustomerName.Size")));
			this.txtCustomerName.TabIndex = ((int)(resources.GetObject("txtCustomerName.TabIndex")));
			this.txtCustomerName.Text = resources.GetString("txtCustomerName.Text");
			this.txtCustomerName.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("txtCustomerName.TextAlign")));
			this.txtCustomerName.Visible = ((bool)(resources.GetObject("txtCustomerName.Visible")));
			this.txtCustomerName.WordWrap = ((bool)(resources.GetObject("txtCustomerName.WordWrap")));
			// 
			// txtCustomer
			// 
			this.txtCustomer.AccessibleDescription = resources.GetString("txtCustomer.AccessibleDescription");
			this.txtCustomer.AccessibleName = resources.GetString("txtCustomer.AccessibleName");
			this.txtCustomer.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("txtCustomer.Anchor")));
			this.txtCustomer.AutoSize = ((bool)(resources.GetObject("txtCustomer.AutoSize")));
			this.txtCustomer.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("txtCustomer.BackgroundImage")));
			this.txtCustomer.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("txtCustomer.Dock")));
			this.txtCustomer.Enabled = ((bool)(resources.GetObject("txtCustomer.Enabled")));
			this.txtCustomer.Font = ((System.Drawing.Font)(resources.GetObject("txtCustomer.Font")));
			this.txtCustomer.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("txtCustomer.ImeMode")));
			this.txtCustomer.Location = ((System.Drawing.Point)(resources.GetObject("txtCustomer.Location")));
			this.txtCustomer.MaxLength = ((int)(resources.GetObject("txtCustomer.MaxLength")));
			this.txtCustomer.Multiline = ((bool)(resources.GetObject("txtCustomer.Multiline")));
			this.txtCustomer.Name = "txtCustomer";
			this.txtCustomer.PasswordChar = ((char)(resources.GetObject("txtCustomer.PasswordChar")));
			this.txtCustomer.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("txtCustomer.RightToLeft")));
			this.txtCustomer.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("txtCustomer.ScrollBars")));
			this.txtCustomer.Size = ((System.Drawing.Size)(resources.GetObject("txtCustomer.Size")));
			this.txtCustomer.TabIndex = ((int)(resources.GetObject("txtCustomer.TabIndex")));
			this.txtCustomer.Text = resources.GetString("txtCustomer.Text");
			this.txtCustomer.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("txtCustomer.TextAlign")));
			this.txtCustomer.Visible = ((bool)(resources.GetObject("txtCustomer.Visible")));
			this.txtCustomer.WordWrap = ((bool)(resources.GetObject("txtCustomer.WordWrap")));
			this.txtCustomer.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCustomer_KeyDown);
			this.txtCustomer.Validating += new System.ComponentModel.CancelEventHandler(this.txtCustomer_Validating);
			// 
			// lblCustomer
			// 
			this.lblCustomer.AccessibleDescription = resources.GetString("lblCustomer.AccessibleDescription");
			this.lblCustomer.AccessibleName = resources.GetString("lblCustomer.AccessibleName");
			this.lblCustomer.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblCustomer.Anchor")));
			this.lblCustomer.AutoSize = ((bool)(resources.GetObject("lblCustomer.AutoSize")));
			this.lblCustomer.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblCustomer.Dock")));
			this.lblCustomer.Enabled = ((bool)(resources.GetObject("lblCustomer.Enabled")));
			this.lblCustomer.Font = ((System.Drawing.Font)(resources.GetObject("lblCustomer.Font")));
			this.lblCustomer.Image = ((System.Drawing.Image)(resources.GetObject("lblCustomer.Image")));
			this.lblCustomer.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblCustomer.ImageAlign")));
			this.lblCustomer.ImageIndex = ((int)(resources.GetObject("lblCustomer.ImageIndex")));
			this.lblCustomer.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblCustomer.ImeMode")));
			this.lblCustomer.Location = ((System.Drawing.Point)(resources.GetObject("lblCustomer.Location")));
			this.lblCustomer.Name = "lblCustomer";
			this.lblCustomer.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblCustomer.RightToLeft")));
			this.lblCustomer.Size = ((System.Drawing.Size)(resources.GetObject("lblCustomer.Size")));
			this.lblCustomer.TabIndex = ((int)(resources.GetObject("lblCustomer.TabIndex")));
			this.lblCustomer.Text = resources.GetString("lblCustomer.Text");
			this.lblCustomer.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblCustomer.TextAlign")));
			this.lblCustomer.Visible = ((bool)(resources.GetObject("lblCustomer.Visible")));
			// 
			// lblTo
			// 
			this.lblTo.AccessibleDescription = resources.GetString("lblTo.AccessibleDescription");
			this.lblTo.AccessibleName = resources.GetString("lblTo.AccessibleName");
			this.lblTo.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblTo.Anchor")));
			this.lblTo.AutoSize = ((bool)(resources.GetObject("lblTo.AutoSize")));
			this.lblTo.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblTo.Dock")));
			this.lblTo.Enabled = ((bool)(resources.GetObject("lblTo.Enabled")));
			this.lblTo.Font = ((System.Drawing.Font)(resources.GetObject("lblTo.Font")));
			this.lblTo.ForeColor = System.Drawing.SystemColors.WindowText;
			this.lblTo.Image = ((System.Drawing.Image)(resources.GetObject("lblTo.Image")));
			this.lblTo.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblTo.ImageAlign")));
			this.lblTo.ImageIndex = ((int)(resources.GetObject("lblTo.ImageIndex")));
			this.lblTo.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblTo.ImeMode")));
			this.lblTo.Location = ((System.Drawing.Point)(resources.GetObject("lblTo.Location")));
			this.lblTo.Name = "lblTo";
			this.lblTo.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblTo.RightToLeft")));
			this.lblTo.Size = ((System.Drawing.Size)(resources.GetObject("lblTo.Size")));
			this.lblTo.TabIndex = ((int)(resources.GetObject("lblTo.TabIndex")));
			this.lblTo.Text = resources.GetString("lblTo.Text");
			this.lblTo.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblTo.TextAlign")));
			this.lblTo.Visible = ((bool)(resources.GetObject("lblTo.Visible")));
			// 
			// lblFromMonth
			// 
			this.lblFromMonth.AccessibleDescription = resources.GetString("lblFromMonth.AccessibleDescription");
			this.lblFromMonth.AccessibleName = resources.GetString("lblFromMonth.AccessibleName");
			this.lblFromMonth.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblFromMonth.Anchor")));
			this.lblFromMonth.AutoSize = ((bool)(resources.GetObject("lblFromMonth.AutoSize")));
			this.lblFromMonth.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblFromMonth.Dock")));
			this.lblFromMonth.Enabled = ((bool)(resources.GetObject("lblFromMonth.Enabled")));
			this.lblFromMonth.Font = ((System.Drawing.Font)(resources.GetObject("lblFromMonth.Font")));
			this.lblFromMonth.ForeColor = System.Drawing.SystemColors.WindowText;
			this.lblFromMonth.Image = ((System.Drawing.Image)(resources.GetObject("lblFromMonth.Image")));
			this.lblFromMonth.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblFromMonth.ImageAlign")));
			this.lblFromMonth.ImageIndex = ((int)(resources.GetObject("lblFromMonth.ImageIndex")));
			this.lblFromMonth.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblFromMonth.ImeMode")));
			this.lblFromMonth.Location = ((System.Drawing.Point)(resources.GetObject("lblFromMonth.Location")));
			this.lblFromMonth.Name = "lblFromMonth";
			this.lblFromMonth.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblFromMonth.RightToLeft")));
			this.lblFromMonth.Size = ((System.Drawing.Size)(resources.GetObject("lblFromMonth.Size")));
			this.lblFromMonth.TabIndex = ((int)(resources.GetObject("lblFromMonth.TabIndex")));
			this.lblFromMonth.Text = resources.GetString("lblFromMonth.Text");
			this.lblFromMonth.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblFromMonth.TextAlign")));
			this.lblFromMonth.Visible = ((bool)(resources.GetObject("lblFromMonth.Visible")));
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
			// OrderSummaryReport
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
			this.Controls.Add(this.txtCustomer);
			this.Controls.Add(this.cboYear);
			this.Controls.Add(this.cboToMonth);
			this.Controls.Add(this.cboFromMonth);
			this.Controls.Add(this.txtCategory);
			this.Controls.Add(this.txtModel);
			this.Controls.Add(this.txtType);
			this.Controls.Add(this.btnHelp);
			this.Controls.Add(this.btnClose);
			this.Controls.Add(this.btnExecute);
			this.Controls.Add(this.lblFromMonth);
			this.Controls.Add(this.lblTo);
			this.Controls.Add(this.lblCategory);
			this.Controls.Add(this.cboCCN);
			this.Controls.Add(this.lblCCN);
			this.Controls.Add(this.lblModel);
			this.Controls.Add(this.lblYear);
			this.Controls.Add(this.btnCategory);
			this.Controls.Add(this.btnModel);
			this.Controls.Add(this.lblCustomer);
			this.Controls.Add(this.btnType);
			this.Controls.Add(this.lblType);
			this.Controls.Add(this.btnCustomer);
			this.Controls.Add(this.txtCustomerName);
			this.Enabled = ((bool)(resources.GetObject("$this.Enabled")));
			this.Font = ((System.Drawing.Font)(resources.GetObject("$this.Font")));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("$this.ImeMode")));
			this.Location = ((System.Drawing.Point)(resources.GetObject("$this.Location")));
			this.MaximizeBox = false;
			this.MaximumSize = ((System.Drawing.Size)(resources.GetObject("$this.MaximumSize")));
			this.MinimumSize = ((System.Drawing.Size)(resources.GetObject("$this.MinimumSize")));
			this.Name = "OrderSummaryReport";
			this.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("$this.RightToLeft")));
			this.StartPosition = ((System.Windows.Forms.FormStartPosition)(resources.GetObject("$this.StartPosition")));
			this.Text = resources.GetString("$this.Text");
			this.Load += new System.EventHandler(this.OrderSummaryReport_Load);
			((System.ComponentModel.ISupportInitialize)(this.cboCCN)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion
		
		#region Class's Methods		
	
		/// <summary>
		/// Get working day of 12 months in a specific year
		/// </summary>
		/// <param name="pintYear"></param>
		/// <returns>Array of working day of 12 months</returns>
		/// <author> Tuan TQ, 23 Nov, 2005</author>
		private Hashtable GetWorkingDayOfAllMonths(int pintYear)
		{
			UtilsBO boUtils = new UtilsBO();
			ArrayList arrHoliday = boUtils.GetHolidaysInYear(pintYear);
			ArrayList arrOffDay = boUtils.GetWorkingDayByYear(pintYear);
			
			Hashtable htbResult = new Hashtable();

			for(int iMonth = 1; iMonth <= 12; iMonth++)
			{
				int intDaysInMonth = DateTime.DaysInMonth(pintYear, iMonth);
				
				DateTime dtmTemp = new DateTime(pintYear, iMonth, 1);
				int intWorkingDays = intDaysInMonth;

				for(int i = 1; i <= intDaysInMonth; i++)
				{
					if(arrHoliday.Contains(dtmTemp))
					{
						intWorkingDays--;
					}

					if(arrOffDay.Contains(dtmTemp.DayOfWeek))
					{
						intWorkingDays--;
					}
					
					dtmTemp = dtmTemp.AddDays(1);
				}

				htbResult.Add(iMonth, intWorkingDays);
			}

			return htbResult;
		}

		/// <summary>
		/// Get actual working day in a specific month
		/// </summary>
		/// <param name="pintMonth"></param>
		/// <param name="pintYear"></param>
		/// <returns>Actual working day of month</returns>
		/// <author> Tuan TQ, 23 Nov, 2005</author>
		private int GetWorkingDayInMonth(int pintMonth, int pintYear)
		{
			int intDaysInMonth = DateTime.DaysInMonth(pintYear, pintMonth);
			
			UtilsBO boUtils = new UtilsBO();
			ArrayList arrHoliday = boUtils.GetHolidaysInYear(pintYear);
			ArrayList arrOffDay = boUtils.GetWorkingDayByYear(pintYear);
			
			DateTime dtmTemp = new DateTime(pintYear, pintMonth, 1);
			int intWorkingDays = intDaysInMonth;

			for(int i = 1; i <= intDaysInMonth; i++)
			{
				if(arrHoliday.Contains(dtmTemp))
				{
					intWorkingDays--;
				}

				if(arrOffDay.Contains(dtmTemp.DayOfWeek))
				{
					intWorkingDays--;
				}
				
				dtmTemp = dtmTemp.AddDays(1);
			}

			return intWorkingDays;
		}		
		
		/// <summary>
		/// Insert or update row into report data table
		/// </summary>
		/// <param name="ptdrSourceRow"></param>
		/// <param name="pdtbReporTable"></param>
		/// <param name="pblnFirstTime"></param>
		/// <author> Tuan TQ, 23 Nov, 2005</author>
		private void InsertRow2ReportTable(DataRow ptdrSourceRow, DataTable pdtbReporTable, bool pblnFirstTime, int pintTotalMonth)
		{
			DataRow dtrNewRow;

			//First time means insert new row
			if(pblnFirstTime)
			{
				dtrNewRow = pdtbReporTable.NewRow();

				dtrNewRow[CCNID_FLD] = ptdrSourceRow[CCNID_FLD];
				dtrNewRow[PARTY_CODE_FLD] = ptdrSourceRow[PARTY_CODE_FLD];
				dtrNewRow[SELLING_UM_FLD] = ptdrSourceRow[SELLING_UM_FLD];
				dtrNewRow[CATEGORY_CODE_FLD] = ptdrSourceRow[CATEGORY_CODE_FLD];
				dtrNewRow[PRODUCT_CODE_FLD] = ptdrSourceRow[PRODUCT_CODE_FLD];
				dtrNewRow[PRODUCT_DESCRIPTION_FLD] = ptdrSourceRow[PRODUCT_DESCRIPTION_FLD];
				dtrNewRow[PRODUCT_REVISION_FLD] = ptdrSourceRow[PRODUCT_REVISION_FLD];
				dtrNewRow[SALE_TYPE_CODE_FLD] = ptdrSourceRow[SALE_TYPE_CODE_FLD];

				//Set 0 to other quantity columns
				for(int i =1; i <= 12; i++)
				{
					dtrNewRow[QUANTITY_FIELD_PREFIX + i] = DBNull.Value;
				}

				//dtrNewRow[TOTAL_CAPACITY_FLD] = DBNull.Value;
				dtrNewRow[TOTALSET_FLD] = decimal.Zero;

				//Add to colection
				pdtbReporTable.Rows.Add(dtrNewRow);
			}
			else
			{
				//Update data of last row
				dtrNewRow = pdtbReporTable.Rows[pdtbReporTable.Rows.Count - 1];
			}

			int iMonth = int.Parse(ptdrSourceRow[TRANSMONTH_FLD].ToString());
			dtrNewRow[QUANTITY_FIELD_PREFIX + iMonth.ToString()] = ptdrSourceRow[SO_SaleOrderDetailTable.ORDERQUANTITY_FLD];

			if(!ptdrSourceRow[SO_SaleOrderDetailTable.ORDERQUANTITY_FLD].Equals(DBNull.Value)
				|| !ptdrSourceRow[SO_SaleOrderDetailTable.ORDERQUANTITY_FLD].ToString().Trim().Equals(string.Empty))
			{
				dtrNewRow[TOTALSET_FLD] = decimal.Parse(dtrNewRow[TOTALSET_FLD].ToString()) + decimal.Parse(ptdrSourceRow[SO_SaleOrderDetailTable.ORDERQUANTITY_FLD].ToString());
			}
		}

		/// <summary>
		/// Build Data table for Order Summary Report
		/// </summary>
		/// <param name="pdtbData">Source Data</param>
		/// <returns>Data with template as data template of report</returns>
		/// <author> Tuan TQ, 23 Nov, 2005</author>
		private DataTable BuildReportTable(DataTable pdtbData, int pintTotalMonth)
		{
			PCSComUtils.Framework.ReportFrame.BO.C1PrintPreviewDialogBO boPrintPreview = new PCSComUtils.Framework.ReportFrame.BO.C1PrintPreviewDialogBO();

			//Get data
			DataTable dtbCapacity;

			DataTable dtbTransform = BuildDataTemplateTable();
			if(pdtbData == null)
			{
				return dtbTransform;
			}
			
			//Collection of processed item
			ArrayList arlProduct = new ArrayList();
			Hashtable htbCategoryCapacity = new Hashtable();
			DataRow[] arrItem = null;

			string strProductList = string.Empty;
			//filter string
			string strFilter = string.Empty;			
			//keeps category code
			string strCategoryCode = string.Empty;
			string strCategoryID = string.Empty;
			//keeps Product & Sale Type
			string strProductSaleType = string.Empty;			
			//indicate data will be added or modified
			bool blnFirstTime = false;

			foreach(DataRow dtRow in pdtbData.Rows)
			{
				//Add category to collection (to keep capacity value)
				strCategoryCode = dtRow[CATEGORY_CODE_FLD].ToString();
				strCategoryID = dtRow[ITM_CategoryTable.CATEGORYID_FLD].ToString().Trim();
				
				//Processing CategoryID
				if(strCategoryID == string.Empty)
				{
					strCategoryID = "0";
				}

				if(!htbCategoryCapacity.ContainsKey(strCategoryCode))
				{
					htbCategoryCapacity.Add(strCategoryCode, string.Empty);

					//Collect products with the same category
					strFilter = ITM_CategoryTable.CATEGORYID_FLD  + "=" + strCategoryID;
					arrItem = pdtbData.Select(strFilter);

					strProductList = "(0 ";
					for(int i = 0; i < arrItem.Length; i++)
					{
						strProductList += ", " + arrItem[i][ITM_ProductTable.PRODUCTID_FLD].ToString();
					}
					strProductList += ")";

					dtbCapacity = boPrintPreview.GetProductCapacityOfCategory(strCategoryID, strProductList);
					if(dtbCapacity != null)
					{
						if(dtbCapacity.Rows.Count != 0)
						{
							htbCategoryCapacity[strCategoryCode] = dtbCapacity.Rows[0][CAPACITY_FLD];
						}
					}
				}				
				
				strProductSaleType = dtRow[PRODUCT_CODE_FLD].ToString() + dtRow[SALE_TYPE_CODE_FLD].ToString();

				if(!arlProduct.Contains(strProductSaleType))
				{
					strFilter = PRODUCT_CODE_FLD + "='" + dtRow[PRODUCT_CODE_FLD].ToString().Replace("'", "''") + "'";
					if(!dtRow[SALE_TYPE_CODE_FLD].Equals(DBNull.Value))
					{
						strFilter += " AND " + SALE_TYPE_CODE_FLD + "='" + dtRow[SALE_TYPE_CODE_FLD].ToString().Replace("'", "''") + "'";
					}
					else
					{
						strFilter += " AND " + SALE_TYPE_CODE_FLD + " IS NULL";
					}

					//Select data related to product
					arrItem = pdtbData.Select(strFilter);
					blnFirstTime = true;
					for(int i = 0; i < arrItem.Length; i++)
					{						
						InsertRow2ReportTable(arrItem[i], dtbTransform, blnFirstTime, pintTotalMonth);
						blnFirstTime = false;
					}

					//Add item to collection as processed
					arlProduct.Add(strProductSaleType);
				}
			}
			
			//Update value of capacity field of a category
			IDictionaryEnumerator myEnumerator = htbCategoryCapacity.GetEnumerator();
			while(myEnumerator.MoveNext())
			{
				if(myEnumerator.Value.ToString() != string.Empty)
				{
					strCategoryCode = CATEGORY_CODE_FLD  + "='" + myEnumerator.Key.ToString().Replace("'", "''")  + "'";
					arrItem = dtbTransform.Select(strCategoryCode);
					for(int i = 0; i < arrItem.Length; i++)
					{
						arrItem[i][CAPACITY_FLD] = myEnumerator.Value;
						arrItem[i][TOTAL_CAPACITY_FLD] = decimal.Parse(myEnumerator.Value.ToString()) * 12;
					}
				}
			}

			return dtbTransform;
		}		

		/// <summary>
		/// Create report data template
		/// </summary>
		/// <returns></returns>
		/// <author> Tuan TQ, 22 Nov, 2005</author>
		private DataTable BuildDataTemplateTable()
		{
			DataTable dtbReport = new DataTable();

			//Add column			
			dtbReport.Columns.Add(CCNID_FLD, typeof(System.Int32));
			dtbReport.Columns.Add(PARTY_CODE_FLD, typeof(System.String));
			dtbReport.Columns.Add(SELLING_UM_FLD, typeof(System.String));			
			dtbReport.Columns.Add(CATEGORY_CODE_FLD, typeof(System.String));
			dtbReport.Columns.Add(PRODUCT_CODE_FLD, typeof(System.String));
			dtbReport.Columns.Add(PRODUCT_DESCRIPTION_FLD, typeof(System.String));
			dtbReport.Columns.Add(PRODUCT_REVISION_FLD, typeof(System.String));
			dtbReport.Columns.Add(SALE_TYPE_CODE_FLD, typeof(System.String));
			
			for(int i = 1; i <= 12; i++)
			{
				dtbReport.Columns.Add(QUANTITY_FIELD_PREFIX + i.ToString(), typeof(System.Decimal));				
			}

			dtbReport.Columns.Add(TOTALSET_FLD, typeof(System.Decimal));
			dtbReport.Columns.Add(CAPACITY_FLD, typeof(System.Decimal));
			dtbReport.Columns.Add(TOTAL_CAPACITY_FLD, typeof(System.Decimal));

			return dtbReport;
		}
		
		/// <summary>
		/// Change header & column header of report
		/// </summary>
		/// <param name="pobjReportBuilder"></param>
		/// <param name="pintReportYear.ToString()"></param>
		/// <author> Tuan TQ, 22 Nov, 2005</author>
		private void ChangeReportDisplayInfo(ReportBuilder pobjReportBuilder, int pintReportYear, int pintFromMonth, int pintToMonth)
		{
			#region Constants
			
			const int DAYS_IN_YEAR  = 365;
			
			//Report Field's Name			
			const string RPT_COMPANY_FLD  = "fldCompany";
			const string RPT_YEAR_FLD = "fldYear";
			const string RPT_JAN_HEADER_FLD  = "hdrJan";
			const string RPT_FEB_HEADER_FLD  = "hdrFeb";
			const string RPT_MAR_HEADER_FLD  = "hdrMar";
			const string RPT_APR_HEADER_FLD  = "hdrApr";
			const string RPT_MAY_HEADER_FLD  = "hdrMay";
			const string RPT_JUN_HEADER_FLD  = "hdrJun";
			const string RPT_JUL_HEADER_FLD  = "hdrJul";
			const string RPT_AUG_HEADER_FLD  = "hdrAug";
			const string RPT_SEP_HEADER_FLD  = "hdrSep";
			const string RPT_OCT_HEADER_FLD  = "hdrOct";
			const string RPT_NOV_HEADER_FLD  = "hdrNov";
			const string RPT_DEC_HEADER_FLD  = "hdrDec";

			const string RPT_VERSION_FLD	  = "fldVersion";
			const string RPT_ACTIVE_DATE_FLD  = "fldActiveDate";
			const string RPT_DAYS_IN_YEAR_FLD  = "fldDaysInYear";
			const string RPT_WORKINGDAYS_IN_YEAR_FIELD_PREFIX   = "fldWorkingDaysInYear";
			
			const string RPT_MONTH_QTY_FIELD_PREFIX = "fldMonthQuantity_";
			const string RPT_PARTY_QTY_FIELD_PREFIX = "fldPartySumQty_";
			const string RPT_CATEGORY_QTY_FIELD_PREFIX    = "fldCategorySumQty_";
			const string RPT_MONTHLY_AVERAGE_FIELD_PREFIX = "fldMonthAverage_";
			const string RPT_CAPACITY_FIELD_PREFIX        = "fldCapacity_";
			const string RPT_WORKINGDAYS_IN_MONTH_FIELD_PREFIX   = "fldWorkingDaysInMonth_";
			const string RPT_DAYS_IN_JANUARY_FIELD_PREFIX = "fldDaysInMonth_2";

			#endregion Constants
						
			//Actual working day
			int iWorkingDayInYear = 0;
			Hashtable htbWorkingDays = GetWorkingDayOfAllMonths(pintReportYear);
			
			if(htbWorkingDays != null)
			{
				IDictionaryEnumerator myEnumerator = htbWorkingDays.GetEnumerator();
				while(myEnumerator.MoveNext())
				{
					pobjReportBuilder.DrawPredefinedField(RPT_WORKINGDAYS_IN_MONTH_FIELD_PREFIX + myEnumerator.Key.ToString(), myEnumerator.Value.ToString());
					iWorkingDayInYear += int.Parse(myEnumerator.Value.ToString());
				}
			
				pobjReportBuilder.DrawPredefinedField(RPT_WORKINGDAYS_IN_YEAR_FIELD_PREFIX, iWorkingDayInYear.ToString());
			}

			//Days in month: January
			int iDaysInJanuary = DateTime.DaysInMonth(pintReportYear, 2);
			pobjReportBuilder.DrawPredefinedField(RPT_DAYS_IN_JANUARY_FIELD_PREFIX, iDaysInJanuary.ToString());
			
			//Total days in year
			if(iDaysInJanuary == 28)
			{
				pobjReportBuilder.DrawPredefinedField(RPT_DAYS_IN_YEAR_FLD, DAYS_IN_YEAR.ToString());
			}
			else
			{
				pobjReportBuilder.DrawPredefinedField(RPT_DAYS_IN_YEAR_FLD, string.Format("{0}", DAYS_IN_YEAR + 1));
			}
			
			//Company Info
			pobjReportBuilder.DrawPredefinedField(RPT_COMPANY_FLD, SystemProperty.SytemParams.Get(SystemParam.COMPANY_FULL_NAME));
			
			//TOTO: Change to valid parameter name
			pobjReportBuilder.DrawPredefinedField(RPT_VERSION_FLD, SystemProperty.SytemParams.Get(SystemParam.OSR_VERSION));
			pobjReportBuilder.DrawPredefinedField(RPT_ACTIVE_DATE_FLD, SystemProperty.SytemParams.Get(SystemParam.OSR_ACTIVE_DATE));
			
			//Report PageHeader
			pobjReportBuilder.DrawPredefinedField(RPT_YEAR_FLD, pintReportYear.ToString());
			
			//Column PageHeader
			pobjReportBuilder.DrawPredefinedField(RPT_JAN_HEADER_FLD, RPT_JAN_HEADER_FLD.Substring(3,3) + "-" + pintReportYear.ToString().Substring(2, 2));
			pobjReportBuilder.DrawPredefinedField(RPT_FEB_HEADER_FLD, RPT_FEB_HEADER_FLD.Substring(3,3) + "-" + pintReportYear.ToString().Substring(2, 2));
			pobjReportBuilder.DrawPredefinedField(RPT_MAR_HEADER_FLD, RPT_MAR_HEADER_FLD.Substring(3,3) + "-" + pintReportYear.ToString().Substring(2, 2));
			pobjReportBuilder.DrawPredefinedField(RPT_APR_HEADER_FLD, RPT_APR_HEADER_FLD.Substring(3,3) + "-" + pintReportYear.ToString().Substring(2, 2));
			pobjReportBuilder.DrawPredefinedField(RPT_MAY_HEADER_FLD, RPT_MAY_HEADER_FLD.Substring(3,3) + "-" + pintReportYear.ToString().Substring(2, 2));
			pobjReportBuilder.DrawPredefinedField(RPT_JUN_HEADER_FLD, RPT_JUN_HEADER_FLD.Substring(3,3) + "-" + pintReportYear.ToString().Substring(2, 2));
			pobjReportBuilder.DrawPredefinedField(RPT_JUL_HEADER_FLD, RPT_JUL_HEADER_FLD.Substring(3,3) + "-" + pintReportYear.ToString().Substring(2, 2));
			pobjReportBuilder.DrawPredefinedField(RPT_AUG_HEADER_FLD, RPT_AUG_HEADER_FLD.Substring(3,3) + "-" + pintReportYear.ToString().Substring(2, 2));
			pobjReportBuilder.DrawPredefinedField(RPT_SEP_HEADER_FLD, RPT_SEP_HEADER_FLD.Substring(3,3) + "-" + pintReportYear.ToString().Substring(2, 2));
			pobjReportBuilder.DrawPredefinedField(RPT_OCT_HEADER_FLD, RPT_OCT_HEADER_FLD.Substring(3,3) + "-" + pintReportYear.ToString().Substring(2, 2));
			pobjReportBuilder.DrawPredefinedField(RPT_NOV_HEADER_FLD, RPT_NOV_HEADER_FLD.Substring(3,3) + "-" + pintReportYear.ToString().Substring(2, 2));
			pobjReportBuilder.DrawPredefinedField(RPT_DEC_HEADER_FLD, RPT_DEC_HEADER_FLD.Substring(3,3) + "-" + pintReportYear.ToString().Substring(2, 2));
			
			//Hide fields those are not displayed
			for(int i = 1; i< pintFromMonth; i++)
			{
				pobjReportBuilder.Report.Fields[RPT_MONTH_QTY_FIELD_PREFIX + i.ToString()].Visible = false;
				pobjReportBuilder.Report.Fields[RPT_PARTY_QTY_FIELD_PREFIX + i.ToString()].Visible = false;
				pobjReportBuilder.Report.Fields[RPT_CATEGORY_QTY_FIELD_PREFIX + i.ToString()].Visible = false;
				pobjReportBuilder.Report.Fields[RPT_MONTHLY_AVERAGE_FIELD_PREFIX + i.ToString()].Visible = false;				
				pobjReportBuilder.Report.Fields[RPT_CAPACITY_FIELD_PREFIX + i.ToString()].Visible = false;
			}

			for(int i = pintToMonth + 1; i<= 12; i++)
			{
				pobjReportBuilder.Report.Fields[RPT_MONTH_QTY_FIELD_PREFIX + i.ToString()].Visible = false;
				pobjReportBuilder.Report.Fields[RPT_PARTY_QTY_FIELD_PREFIX + i.ToString()].Visible = false;
				pobjReportBuilder.Report.Fields[RPT_CATEGORY_QTY_FIELD_PREFIX + i.ToString()].Visible = false;
				pobjReportBuilder.Report.Fields[RPT_MONTHLY_AVERAGE_FIELD_PREFIX + i.ToString()].Visible = false;				
				pobjReportBuilder.Report.Fields[RPT_CAPACITY_FIELD_PREFIX + i.ToString()].Visible = false;
			}			
		}

		/// <summary>
		/// Fill related data on controls when select Sale Type
		/// </summary>
		/// <param name="pblnAlwaysShowDialog"></param>
		/// <author> Tuan TQ, 21 Nov, 2005</author>
		private bool SelectType(bool pblnAlwaysShowDialog)
		{
			Hashtable htbCriteria = new Hashtable();		

			//Call OpenSearchForm for selecting type
			DataRowView drwResult = FormControlComponents.OpenSearchForm(SO_SaleTypeTable.TABLE_NAME, SO_SaleTypeTable.CODE_FLD, txtType.Text, htbCriteria, pblnAlwaysShowDialog);
			
			// Fill values to form's controls
			if(drwResult != null)
			{
				txtType.Text = drwResult[SO_SaleTypeTable.CODE_FLD].ToString();
				txtType.Tag = drwResult[SO_SaleTypeTable.SALETYPEID_FLD];
				
				//Reset modify status
				txtType.Modified = false;				
			}
			else if(!pblnAlwaysShowDialog)
			{					
				txtType.Focus();
				return false;
			}

			return true;
		}


		/// <summary>
		/// Fill related data on controls when select Catagory
		/// </summary>
		/// <param name="pblnAlwaysShowDialog"></param>
		/// <author> Tuan TQ, 21 Nov, 2005</author>
		private bool SelectCategory(bool pblnAlwaysShowDialog)
		{
			Hashtable htbCriteria = new Hashtable();		

			//Call OpenSearchForm for selecting Master Location
			DataRowView drwResult = FormControlComponents.OpenSearchForm(ITM_CategoryTable.TABLE_NAME, ITM_CategoryTable.CODE_FLD, txtCategory.Text, htbCriteria, pblnAlwaysShowDialog);
				
			// If has Master location matched searching condition, fill values to form's controls
			if(drwResult != null)
			{
				//Check if master location was changed then clear grid content
				txtCategory.Text = drwResult[ITM_CategoryTable.CODE_FLD].ToString();
				txtCategory.Tag = drwResult[ITM_CategoryTable.CATEGORYID_FLD];
				
				//Reset modify status
				txtCategory.Modified = false;			
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
		/// <author> Tuan TQ, 21 Nov, 2005</author>
		private bool SelectModel(bool pblnAlwaysShowDialog)
		{			
			Hashtable htbCriteria = new Hashtable();
			DataRowView drwResult = null;

			//Call OpenSearchForm for selecting MPS planning cycle
			drwResult = FormControlComponents.OpenSearchForm(REVISION_VIEW, REVISION_COLUMN, txtModel.Text.Trim(), htbCriteria, pblnAlwaysShowDialog);
			
			// If has Master location matched searching condition, fill values to form's controls
			if (drwResult != null)
			{
				//Check if data was changed then reassign
				txtModel.Text = drwResult[REVISION_COLUMN].ToString();					
				
				//Reset modify status
				txtModel.Modified = false;	
			}
			else
			{
				if(!pblnAlwaysShowDialog)
				{						
					txtModel.Focus();
					return false;
				}					
			}

			return true;
		}


		/// <summary>
		/// Fill related data on controls when select customer
		/// </summary>
		/// <param name="pblnAlwaysShowDialog"></param>
		/// <author> Tuan TQ, 21 Nov, 2005</author>
		private bool SelectCustomer(bool pblnAlwaysShowDialog)
		{
			Hashtable htbCriteria = new Hashtable();
			DataRowView drwResult = null;				
				
			htbCriteria.Add(CUSTOMER_COLUMN, 0);

			//Call OpenSearchForm for selecting MPS planning cycle
			drwResult = FormControlComponents.OpenSearchForm(VENDOR_CUSTOMER_VIEW, MST_PartyTable.CODE_FLD, txtCustomer.Text, htbCriteria, pblnAlwaysShowDialog);
			
			// Fill values to form's controls
			if (drwResult != null)
			{
				txtCustomer.Text = drwResult[MST_PartyTable.CODE_FLD].ToString();
				txtCustomerName.Text = drwResult[MST_PartyTable.NAME_FLD].ToString();
				txtCustomer.Tag = drwResult[MST_PartyTable.PARTYID_FLD];

				//Reset modify status
				txtCustomer.Modified = false;					
			}
			else
			{
				if(!pblnAlwaysShowDialog)
				{
					txtCustomer.Tag = ZERO_STRING;
					txtCustomer.Focus();

					return false;
				}
			}

			return true;			
		}

		#endregion Class's Methods		

		#region Event Processing		
		
		private void OrderSummaryReport_Load(object sender, System.EventArgs e)
		{
			const int ADDED_YEAR = 2;
			const int SUBTRACTED_YEAR = 5;

			const string METHOD_NAME = THIS + ".OrderSummaryReport_Load()";
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

				FormControlComponents.InitMonthComboBox(cboFromMonth, true);
				FormControlComponents.InitMonthComboBox(cboToMonth, true);
				FormControlComponents.InitYearComboBox(cboYear, dtmServerDate.Year - SUBTRACTED_YEAR, dtmServerDate.Year + ADDED_YEAR, true);

				cboYear.Text = dtmServerDate.Year.ToString();
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

		private void btnType_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnType_Click()";
			try
			{
				SelectType(true);
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

		private void btnModel_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnModel_Click()";
			try
			{
				SelectModel(true);
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

		private void btnCustomer_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnCustomer_Click()";
			try
			{
				SelectCustomer(true);
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

		private void btnExecute_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnExecute_Click()";	

			//Report field names
			const string RPT_YEAR	   = "Year";
			const string RPT_FROM_MONTH  = "From Month";
			const string RPT_TO_MONTH     = "To Month";
			const string RPT_TYPE      = "Type";
			const string RPT_CATEGORY    = "Category";
			const string RPT_MODEL = "Model";
			const string RPT_CUSTOMER    = "Customer";
			const string RPT_PAGE_HEADER = "PageHeader";			
			const string RPT_TITLE_FIELD = "fldTitle";

			try
			{	
				int intCCNID;
				int intYear;
				int intFromMonth = 1;
				int intToMonth = 12;
				string strReportPath = Application.StartupPath;
				string strOtherCondition = string.Empty;

				DataTable dtbResult = null;
				DataTable dtbTransform = null;				

				//Validate data; user must select CCN first
				if(cboCCN.SelectedValue == null || cboCCN.SelectedValue == DBNull.Value)
				{
					this.Cursor = Cursors.Default;
					string[] arrParams = {lblCCN.Text};
					PCSMessageBox.Show(ErrorCode.MESSAGE_MANDATORY_FIELD_REQUIRED, MessageBoxIcon.Error, arrParams);					
					cboCCN.Focus();
					return;
				}

				//Check if user does not select Year
				if((cboYear.Text == null) || (cboYear.Text == string.Empty))
				{
					this.Cursor = Cursors.Default;
					string[] arrParams = {lblYear.Text};
					PCSMessageBox.Show(ErrorCode.MESSAGE_MANDATORY_FIELD_REQUIRED, MessageBoxIcon.Error, arrParams);
					cboYear.Focus();					
					return;
				}
				
				if( (cboToMonth.Text.Trim() != string.Empty)
				 && (cboFromMonth.Text.Trim() == string.Empty)
				  )
				{
					string[] arrParams = {lblFromMonth.Text};
					PCSMessageBox.Show(ErrorCode.MESSAGE_MANDATORY_FIELD_REQUIRED, MessageBoxIcon.Error, arrParams);
					cboFromMonth.Focus();
					return;
				}

				//Check From Month, To Month
				if(cboFromMonth.Text.Trim() != string.Empty)
				{
					if(cboToMonth.Text.Trim() == string.Empty)
					{
						string[] arrParams = {lblTo.Text};
						PCSMessageBox.Show(ErrorCode.MESSAGE_MANDATORY_FIELD_REQUIRED, MessageBoxIcon.Error, arrParams);
						cboToMonth.Focus();
						return;
					}
					
					intFromMonth = int.Parse(cboFromMonth.Text.Trim());
					intToMonth = int.Parse(cboToMonth.Text.Trim());

					if(intFromMonth > intToMonth)
					{
						string[] arrParams = {lblTo.Text, lblFromMonth.Text};
						PCSMessageBox.Show(ErrorCode.MESSAGE_GREATER_THAN, MessageBoxIcon.Error, arrParams);
						cboToMonth.Focus();
						return;
					}
					
					//build other condition string
					strOtherCondition += " AND (Month(" + SO_DeliveryScheduleTable.TABLE_NAME + "." + SO_DeliveryScheduleTable.SCHEDULEDATE_FLD + ") BETWEEN " + cboFromMonth.Text;
					strOtherCondition += " AND " + cboToMonth.Text + ")";
				}
				
				//Change cursor to wait
				this.Cursor = Cursors.WaitCursor;				
				
				intCCNID = int.Parse(cboCCN.SelectedValue.ToString());				
				intYear = int.Parse(cboYear.Text);
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
				if(!File.Exists(strReportPath + @"\" + ORDER_SUMMARY_REPORT))
				{
					this.Cursor = Cursors.Default;
					PCSMessageBox.Show(ErrorCode.MESSAGE_REPORT_TEMPLATE_FILE_NOT_FOUND, MessageBoxIcon.Error);					
					return;
				}				

				//Build other condition string 
				//Category
				if(txtCategory.Text.Trim() != string.Empty)
				{
					strOtherCondition += " AND " + ITM_CategoryTable.TABLE_NAME + "." + ITM_CategoryTable.CODE_FLD  + "='" + txtCategory.Text.Trim().Replace("'", "''") + "'"; 
				}

				//Customer
				if(txtCustomer.Text.Trim() != string.Empty)
				{
					strOtherCondition += " AND " + MST_PartyTable.TABLE_NAME  + "." + MST_PartyTable.CODE_FLD  + "='" + txtCustomer.Text.Trim().Replace("'", "''") + "'"; 
				}

				//Model
				if(txtModel.Text.Trim() != string.Empty)
				{
					strOtherCondition += " AND " + ITM_ProductTable.TABLE_NAME + "." +  ITM_ProductTable.REVISION_FLD  + "='" + txtModel.Text.Trim().Replace("'", "''") + "'"; 
				}
				
				//Sale type
				if(txtType.Text.Trim() != string.Empty)
				{
					strOtherCondition += " AND " + SO_SaleTypeTable.TABLE_NAME + "." + SO_SaleTypeTable.CODE_FLD  + "='" + txtType.Text.Trim().Replace("'", "''") + "'"; 
				}
				
				//create print preview object
				PCSComUtils.Framework.ReportFrame.BO.C1PrintPreviewDialogBO boPrintPreview = new PCSComUtils.Framework.ReportFrame.BO.C1PrintPreviewDialogBO();

				//Get data
				dtbResult = boPrintPreview.GetOrderSummaryData(intCCNID, intYear, strOtherCondition);
				//Transform Data
				dtbTransform = BuildReportTable(dtbResult, (intToMonth - intFromMonth) + 1);

				//Create builder object
				ReportWithSubReportBuilder reportBuilder = new ReportWithSubReportBuilder();
				//Set report name
				reportBuilder.ReportName = REPORT_NAME;
				//Set Datasource
				reportBuilder.SourceDataTable = dtbTransform;
				//Set report layout location
				reportBuilder.ReportDefinitionFolder = strReportPath;
				reportBuilder.ReportLayoutFile = ORDER_SUMMARY_REPORT;				
				
				reportBuilder.UseLayoutFile = true;
				reportBuilder.MakeDataTableForRender();			

				// and show it in preview dialog				
				PCSUtils.Framework.ReportFrame.C1PrintPreviewDialog	printPreview = new PCSUtils.Framework.ReportFrame.C1PrintPreviewDialog();
				
				
				//Attach report viewer
				reportBuilder.ReportViewer = printPreview.ReportViewer;				
				reportBuilder.RenderReport();
				
				//Change report header & column header
				ChangeReportDisplayInfo(reportBuilder, int.Parse(cboYear.Text), intFromMonth, intToMonth);
				
				//Draw parameters				
				System.Collections.Specialized.NameValueCollection arrParamAndValue = new System.Collections.Specialized.NameValueCollection();
				arrParamAndValue.Add(RPT_YEAR, cboYear.Text);
				if(cboFromMonth.Text != string.Empty)
				{
					arrParamAndValue.Add(RPT_FROM_MONTH, cboFromMonth.Text);
				}

				if(cboToMonth.Text != string.Empty)
				{
					arrParamAndValue.Add(RPT_TO_MONTH, cboToMonth.Text);
				}

				if(txtType.Text != string.Empty)
				{
					arrParamAndValue.Add(RPT_TYPE, txtType.Text);
				}

				if(txtCategory.Text != string.Empty)
				{
					arrParamAndValue.Add(RPT_CATEGORY, txtCategory.Text);
				}

				if(txtModel.Text != string.Empty)
				{
					arrParamAndValue.Add(RPT_MODEL, txtModel.Text);
				}

				if(txtCustomer.Text != string.Empty)
				{
					arrParamAndValue.Add(RPT_CUSTOMER, txtCustomer.Text);
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
		
		private void txtType_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtType_Validating()";
			try
			{				
				//Exit immediately if empty or in default mode				
				if(txtType.Text.Length == 0)
				{
					return;
				}
				else if(!txtType.Modified)
				{
					return;
				}
				
				e.Cancel = !SelectType(false);
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

		private void txtModel_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtModel_Validating()";
			try
			{				
				//Exit immediately if empty or in default mode				
				if(txtModel.Text.Length == 0)
				{
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

		private void txtCustomer_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtCustomer_Validating()";
			try
			{				
				//Exit immediately if empty or in default mode				
				if(txtCustomer.Text.Length == 0)
				{
					txtCustomerName.Text = string.Empty;
					return;
				}
				else if(!txtCustomer.Modified)
				{
					return;
				}
				
				e.Cancel = !SelectCustomer(false);
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
		
		private void txtType_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtType_KeyDown()";

			try
			{
				if ((e.KeyCode == Keys.F4) && (btnType.Enabled))
				{
					SelectType(true);
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

		private void txtCustomer_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtCustomer_KeyDown()";

			try
			{
				if ((e.KeyCode == Keys.F4) && (btnCustomer.Enabled))
				{
					SelectCustomer(true);
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
			cboFromMonth.SelectedIndex = 0;
			cboToMonth.SelectedIndex = 0;
		}

		#endregion Event Processing						
	}
}
