using System;
using System.Data;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using C1.Win.C1TrueDBGrid;
using PCSComProduction.DCP.BO;
using PCSComProduction.DCP.DS;
using PCSComUtils.Common;
using PCSComUtils.Common.BO;
using PCSComUtils.PCSExc;
using PCSUtils.Log;
using PCSUtils.Utils;

namespace PCSProduction.DCP
{
	/// <summary>
	/// Summary description for ChangeCategory.
	/// </summary>
	public class ChangeCategory : System.Windows.Forms.Form
	{
		private C1.Win.C1TrueDBGrid.C1TrueDBGrid dgrdData;
		private System.Windows.Forms.Button btnHelp;
		private System.Windows.Forms.Button btnDelete;
		private System.Windows.Forms.Button btnSave;
		private System.Windows.Forms.Button btnClose;
		private C1.Win.C1List.C1Combo cboCCN;
		private System.Windows.Forms.Label lblCCN;
		private System.Windows.Forms.TextBox txtWorkCenter;
		private System.Windows.Forms.Button btnWorkCenterSearch;
		private System.Windows.Forms.Label lblWorkCenter;
		private System.Windows.Forms.Button btnMatrix;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private const string THIS = "PCSProduction.DCP.ChangeCategory";
		private DataTable dtbGridLayOut;
		private DataSet dstData = null;
		private bool blnHasError = false;
		private ArrayList arlDeletedItems = new ArrayList();
		private PRO_ChangeCategoryMasterVO voMaster = new PRO_ChangeCategoryMasterVO();
		
		const string V_PRODUCT_IN_WO = "V_ProductInWorkCenter";
		public ChangeCategory()
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(ChangeCategory));
			this.txtWorkCenter = new System.Windows.Forms.TextBox();
			this.dgrdData = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
			this.btnHelp = new System.Windows.Forms.Button();
			this.btnDelete = new System.Windows.Forms.Button();
			this.btnSave = new System.Windows.Forms.Button();
			this.btnClose = new System.Windows.Forms.Button();
			this.btnWorkCenterSearch = new System.Windows.Forms.Button();
			this.lblWorkCenter = new System.Windows.Forms.Label();
			this.cboCCN = new C1.Win.C1List.C1Combo();
			this.lblCCN = new System.Windows.Forms.Label();
			this.btnMatrix = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.dgrdData)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.cboCCN)).BeginInit();
			this.SuspendLayout();
			// 
			// txtWorkCenter
			// 
			this.txtWorkCenter.AccessibleDescription = "";
			this.txtWorkCenter.AccessibleName = "";
			this.txtWorkCenter.Location = new System.Drawing.Point(72, 6);
			this.txtWorkCenter.Name = "txtWorkCenter";
			this.txtWorkCenter.Size = new System.Drawing.Size(119, 20);
			this.txtWorkCenter.TabIndex = 3;
			this.txtWorkCenter.Text = "";
			this.txtWorkCenter.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtWorkCenter_KeyDown);
			this.txtWorkCenter.Validating += new System.ComponentModel.CancelEventHandler(this.txtWorkCenter_Validating);
			this.txtWorkCenter.Enter += new System.EventHandler(this.OnEnterControl);
			// 
			// dgrdData
			// 
			this.dgrdData.AccessibleDescription = "";
			this.dgrdData.AccessibleName = "";
			this.dgrdData.AllowAddNew = true;
			this.dgrdData.AllowDelete = true;
			this.dgrdData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.dgrdData.CaptionHeight = 17;
			this.dgrdData.CollapseColor = System.Drawing.Color.Black;
			this.dgrdData.ExpandColor = System.Drawing.Color.Black;
			this.dgrdData.FlatStyle = C1.Win.C1TrueDBGrid.FlatModeEnum.System;
			this.dgrdData.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.dgrdData.GroupByCaption = "Drag a column header here to group by that column";
			this.dgrdData.Images.Add(((System.Drawing.Image)(resources.GetObject("resource"))));
			this.dgrdData.Location = new System.Drawing.Point(4, 31);
			this.dgrdData.MarqueeStyle = C1.Win.C1TrueDBGrid.MarqueeEnum.DottedCellBorder;
			this.dgrdData.Name = "dgrdData";
			this.dgrdData.PreviewInfo.Location = new System.Drawing.Point(0, 0);
			this.dgrdData.PreviewInfo.Size = new System.Drawing.Size(0, 0);
			this.dgrdData.PreviewInfo.ZoomFactor = 75;
			this.dgrdData.PrintInfo.ShowOptionsDialog = false;
			this.dgrdData.RecordSelectorWidth = 17;
			this.dgrdData.RowDivider.Color = System.Drawing.Color.DarkGray;
			this.dgrdData.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.Single;
			this.dgrdData.RowHeight = 15;
			this.dgrdData.RowSubDividerColor = System.Drawing.Color.DarkGray;
			this.dgrdData.Size = new System.Drawing.Size(536, 339);
			this.dgrdData.TabIndex = 5;
			this.dgrdData.Text = "c1TrueDBGrid1";
			this.dgrdData.ButtonClick += new C1.Win.C1TrueDBGrid.ColEventHandler(this.dgrdData_ButtonClick);
			this.dgrdData.AfterColUpdate += new C1.Win.C1TrueDBGrid.ColEventHandler(this.dgrdData_AfterColUpdate);
			this.dgrdData.BeforeColUpdate += new C1.Win.C1TrueDBGrid.BeforeColUpdateEventHandler(this.dgrdData_BeforeColUpdate);
			this.dgrdData.BeforeDelete += new C1.Win.C1TrueDBGrid.CancelEventHandler(this.dgrdData_BeforeDelete);
			this.dgrdData.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgrdData_KeyDown);
			this.dgrdData.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"Part Number" +
				"\" DataField=\"Code\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level" +
				"=\"0\" Caption=\"Part Name\" DataField=\"Description\"><ValueItems /><GroupInfo /></C1" +
				"DataColumn><C1DataColumn Level=\"0\" Caption=\"Model\" DataField=\"Revision\"><ValueIt" +
				"ems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Category\" Dat" +
				"aField=\"ITM_CategoryCode\"><ValueItems /><GroupInfo /></C1DataColumn></DataCols><" +
				"Styles type=\"C1.Win.C1TrueDBGrid.Design.ContextWrapper\"><Data>RecordSelector{Ali" +
				"gnImage:Center;}Caption{AlignHorz:Center;}Normal{Font:Microsoft Sans Serif, 8.25" +
				"pt;}Style24{}Editor{}Style18{}Style19{}Style14{}Style15{}Style16{AlignHorz:Cente" +
				"r;ForeColor:Maroon;}Style17{AlignHorz:Near;}Style10{AlignHorz:Near;}Style11{}Sty" +
				"le12{}Style13{}Style38{}Style37{}Style34{AlignHorz:Center;}Style35{AlignHorz:Nea" +
				"r;}OddRow{}Style29{AlignHorz:Near;}Style28{AlignHorz:Center;}Style27{}Style26{}S" +
				"tyle25{}Footer{}Style23{AlignHorz:Near;}Style22{AlignHorz:Center;ForeColor:Maroo" +
				"n;}Style21{AlignHorz:General;}Style20{}Inactive{ForeColor:InactiveCaptionText;Ba" +
				"ckColor:InactiveCaption;}EvenRow{BackColor:Aqua;}Heading{Wrap:True;BackColor:Con" +
				"trol;Border:Raised,,1, 1, 1, 1;ForeColor:ControlText;AlignVert:Center;}Style1{}S" +
				"tyle3{}Style2{}Style6{}FilterBar{}Selected{ForeColor:HighlightText;BackColor:Hig" +
				"hlight;}Style4{}Style9{}Style8{}Style39{}Style36{}Style5{}Group{AlignVert:Center" +
				";Border:None,,0, 0, 0, 0;BackColor:ControlDark;}Style7{}Style32{}Style33{}Style3" +
				"0{}Style31{}HighlightRow{ForeColor:HighlightText;BackColor:Highlight;}</Data></S" +
				"tyles><Splits><C1.Win.C1TrueDBGrid.MergeView Name=\"\" CaptionHeight=\"17\" ColumnCa" +
				"ptionHeight=\"17\" ColumnFooterHeight=\"17\" MarqueeStyle=\"DottedCellBorder\" RecordS" +
				"electorWidth=\"17\" DefRecSelWidth=\"17\" VerticalScrollGroup=\"1\" HorizontalScrollGr" +
				"oup=\"1\"><ClientRect>0, 0, 532, 335</ClientRect><BorderSide>0</BorderSide><Captio" +
				"nStyle parent=\"Style2\" me=\"Style10\" /><EditorStyle parent=\"Editor\" me=\"Style5\" /" +
				"><EvenRowStyle parent=\"EvenRow\" me=\"Style8\" /><FilterBarStyle parent=\"FilterBar\"" +
				" me=\"Style13\" /><FooterStyle parent=\"Footer\" me=\"Style3\" /><GroupStyle parent=\"G" +
				"roup\" me=\"Style12\" /><HeadingStyle parent=\"Heading\" me=\"Style2\" /><HighLightRowS" +
				"tyle parent=\"HighlightRow\" me=\"Style7\" /><InactiveStyle parent=\"Inactive\" me=\"St" +
				"yle4\" /><OddRowStyle parent=\"OddRow\" me=\"Style9\" /><RecordSelectorStyle parent=\"" +
				"RecordSelector\" me=\"Style11\" /><SelectedStyle parent=\"Selected\" me=\"Style6\" /><S" +
				"tyle parent=\"Normal\" me=\"Style1\" /><internalCols><C1DisplayColumn><HeadingStyle " +
				"parent=\"Style2\" me=\"Style34\" /><Style parent=\"Style1\" me=\"Style35\" /><FooterStyl" +
				"e parent=\"Style3\" me=\"Style36\" /><EditorStyle parent=\"Style5\" me=\"Style37\" /><Gr" +
				"oupHeaderStyle parent=\"Style1\" me=\"Style39\" /><GroupFooterStyle parent=\"Style1\" " +
				"me=\"Style38\" /><Visible>True</Visible><ColumnDivider>DarkGray,Single</ColumnDivi" +
				"der><Width>66</Width><Height>15</Height><Locked>True</Locked><DCIdx>3</DCIdx></C" +
				"1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style16\" /><St" +
				"yle parent=\"Style1\" me=\"Style17\" /><FooterStyle parent=\"Style3\" me=\"Style18\" /><" +
				"EditorStyle parent=\"Style5\" me=\"Style19\" /><GroupHeaderStyle parent=\"Style1\" me=" +
				"\"Style21\" /><GroupFooterStyle parent=\"Style1\" me=\"Style20\" /><Visible>True</Visi" +
				"ble><ColumnDivider>DarkGray,Single</ColumnDivider><Width>146</Width><Height>15</" +
				"Height><DCIdx>0</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"" +
				"Style2\" me=\"Style22\" /><Style parent=\"Style1\" me=\"Style23\" /><FooterStyle parent" +
				"=\"Style3\" me=\"Style24\" /><EditorStyle parent=\"Style5\" me=\"Style25\" /><GroupHeade" +
				"rStyle parent=\"Style1\" me=\"Style27\" /><GroupFooterStyle parent=\"Style1\" me=\"Styl" +
				"e26\" /><Visible>True</Visible><ColumnDivider>DarkGray,Single</ColumnDivider><Wid" +
				"th>200</Width><Height>15</Height><DCIdx>1</DCIdx></C1DisplayColumn><C1DisplayCol" +
				"umn><HeadingStyle parent=\"Style2\" me=\"Style28\" /><Style parent=\"Style1\" me=\"Styl" +
				"e29\" /><FooterStyle parent=\"Style3\" me=\"Style30\" /><EditorStyle parent=\"Style5\" " +
				"me=\"Style31\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style33\" /><GroupFooterStyl" +
				"e parent=\"Style1\" me=\"Style32\" /><Visible>True</Visible><ColumnDivider>DarkGray," +
				"Single</ColumnDivider><Width>97</Width><Height>15</Height><DCIdx>2</DCIdx></C1Di" +
				"splayColumn></internalCols></C1.Win.C1TrueDBGrid.MergeView></Splits><NamedStyles" +
				"><Style parent=\"\" me=\"Normal\" /><Style parent=\"Normal\" me=\"Heading\" /><Style par" +
				"ent=\"Heading\" me=\"Footer\" /><Style parent=\"Heading\" me=\"Caption\" /><Style parent" +
				"=\"Heading\" me=\"Inactive\" /><Style parent=\"Normal\" me=\"Selected\" /><Style parent=" +
				"\"Normal\" me=\"Editor\" /><Style parent=\"Normal\" me=\"HighlightRow\" /><Style parent=" +
				"\"Normal\" me=\"EvenRow\" /><Style parent=\"Normal\" me=\"OddRow\" /><Style parent=\"Head" +
				"ing\" me=\"RecordSelector\" /><Style parent=\"Normal\" me=\"FilterBar\" /><Style parent" +
				"=\"Caption\" me=\"Group\" /></NamedStyles><vertSplits>1</vertSplits><horzSplits>1</h" +
				"orzSplits><Layout>Modified</Layout><DefaultRecSelWidth>17</DefaultRecSelWidth><C" +
				"lientArea>0, 0, 532, 335</ClientArea><PrintPageHeaderStyle parent=\"\" me=\"Style14" +
				"\" /><PrintPageFooterStyle parent=\"\" me=\"Style15\" /></Blob>";
			// 
			// btnHelp
			// 
			this.btnHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnHelp.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnHelp.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnHelp.Location = new System.Drawing.Point(419, 375);
			this.btnHelp.Name = "btnHelp";
			this.btnHelp.Size = new System.Drawing.Size(60, 23);
			this.btnHelp.TabIndex = 9;
			this.btnHelp.Text = "&Help";
			// 
			// btnDelete
			// 
			this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnDelete.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnDelete.Location = new System.Drawing.Point(65, 375);
			this.btnDelete.Name = "btnDelete";
			this.btnDelete.Size = new System.Drawing.Size(60, 23);
			this.btnDelete.TabIndex = 7;
			this.btnDelete.Text = "&Delete";
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
			// 
			// btnSave
			// 
			this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnSave.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnSave.Location = new System.Drawing.Point(4, 375);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(60, 23);
			this.btnSave.TabIndex = 6;
			this.btnSave.Text = "&Save";
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			// 
			// btnClose
			// 
			this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnClose.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnClose.Location = new System.Drawing.Point(480, 375);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(60, 23);
			this.btnClose.TabIndex = 10;
			this.btnClose.Text = "&Close";
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			// 
			// btnWorkCenterSearch
			// 
			this.btnWorkCenterSearch.AccessibleDescription = "";
			this.btnWorkCenterSearch.AccessibleName = "";
			this.btnWorkCenterSearch.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnWorkCenterSearch.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnWorkCenterSearch.Location = new System.Drawing.Point(193, 6);
			this.btnWorkCenterSearch.Name = "btnWorkCenterSearch";
			this.btnWorkCenterSearch.Size = new System.Drawing.Size(24, 20);
			this.btnWorkCenterSearch.TabIndex = 4;
			this.btnWorkCenterSearch.Text = "...";
			this.btnWorkCenterSearch.Click += new System.EventHandler(this.btnWorkCenterSearch_Click);
			// 
			// lblWorkCenter
			// 
			this.lblWorkCenter.AccessibleDescription = "";
			this.lblWorkCenter.AccessibleName = "";
			this.lblWorkCenter.ForeColor = System.Drawing.Color.Maroon;
			this.lblWorkCenter.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lblWorkCenter.Location = new System.Drawing.Point(3, 6);
			this.lblWorkCenter.Name = "lblWorkCenter";
			this.lblWorkCenter.Size = new System.Drawing.Size(74, 20);
			this.lblWorkCenter.TabIndex = 2;
			this.lblWorkCenter.Text = "Work Center";
			this.lblWorkCenter.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// cboCCN
			// 
			this.cboCCN.AccessibleDescription = "";
			this.cboCCN.AccessibleName = "";
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
			this.cboCCN.GapHeight = 2;
			this.cboCCN.ItemHeight = 15;
			this.cboCCN.Location = new System.Drawing.Point(446, 6);
			this.cboCCN.MatchEntryTimeout = ((long)(2000));
			this.cboCCN.MaxDropDownItems = ((short)(5));
			this.cboCCN.MaxLength = 32767;
			this.cboCCN.MouseCursor = System.Windows.Forms.Cursors.Default;
			this.cboCCN.Name = "cboCCN";
			this.cboCCN.RowDivider.Color = System.Drawing.Color.DarkGray;
			this.cboCCN.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None;
			this.cboCCN.RowSubDividerColor = System.Drawing.Color.DarkGray;
			this.cboCCN.Size = new System.Drawing.Size(94, 21);
			this.cboCCN.TabIndex = 1;
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
				"th>17</DefaultRecSelWidth></Blob>";
			// 
			// lblCCN
			// 
			this.lblCCN.AccessibleDescription = "";
			this.lblCCN.AccessibleName = "";
			this.lblCCN.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.lblCCN.ForeColor = System.Drawing.Color.Maroon;
			this.lblCCN.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lblCCN.Location = new System.Drawing.Point(413, 6);
			this.lblCCN.Name = "lblCCN";
			this.lblCCN.Size = new System.Drawing.Size(32, 20);
			this.lblCCN.TabIndex = 0;
			this.lblCCN.Text = "CCN";
			this.lblCCN.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// btnMatrix
			// 
			this.btnMatrix.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnMatrix.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnMatrix.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnMatrix.Location = new System.Drawing.Point(358, 375);
			this.btnMatrix.Name = "btnMatrix";
			this.btnMatrix.Size = new System.Drawing.Size(60, 23);
			this.btnMatrix.TabIndex = 8;
			this.btnMatrix.Text = "&Matrix";
			this.btnMatrix.Click += new System.EventHandler(this.btnMatrix_Click);
			// 
			// ChangeCategory
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.btnClose;
			this.ClientSize = new System.Drawing.Size(546, 401);
			this.Controls.Add(this.btnMatrix);
			this.Controls.Add(this.txtWorkCenter);
			this.Controls.Add(this.dgrdData);
			this.Controls.Add(this.btnHelp);
			this.Controls.Add(this.btnDelete);
			this.Controls.Add(this.btnSave);
			this.Controls.Add(this.btnClose);
			this.Controls.Add(this.btnWorkCenterSearch);
			this.Controls.Add(this.cboCCN);
			this.Controls.Add(this.lblCCN);
			this.Controls.Add(this.lblWorkCenter);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.KeyPreview = true;
			this.Name = "ChangeCategory";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Change Category";
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ChangeCategory_KeyDown);
			this.Closing += new System.ComponentModel.CancelEventHandler(this.ChangeCategory_Closing);
			this.Load += new System.EventHandler(this.ChangeCategory_Load);
			((System.ComponentModel.ISupportInitialize)(this.dgrdData)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.cboCCN)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		private void ChangeCategory_Load(object sender, System.EventArgs e)
		{	
			const string METHOD_NAME = THIS + ".ChangeCategory_Load()";
			try
			{
				Security objSecurity = new Security();
				this.Name = THIS;
				if (objSecurity.SetRightForUserOnForm(this, SystemProperty.UserName) == 0)
				{
					this.Close();
					return;
				}

				//get grid layout
				dtbGridLayOut = FormControlComponents.StoreGridLayout(dgrdData);

				//Load CCN and set default CCN
				UtilsBO boUtils = new UtilsBO();
				DataSet dstCCN = boUtils.ListCCN();
				cboCCN.DataSource = dstCCN.Tables[MST_CCNTable.TABLE_NAME];
				cboCCN.DisplayMember	= MST_CCNTable.CODE_FLD;
				cboCCN.ValueMember		= MST_CCNTable.CCNID_FLD;
				FormControlComponents.PutDataIntoC1ComboBox(cboCCN,dstCCN.Tables[MST_CCNTable.TABLE_NAME],MST_CCNTable.CODE_FLD,MST_CCNTable.CCNID_FLD,MST_CCNTable.TABLE_NAME);
				
				
				if (SystemProperty.CCNID != 0)
				{
					cboCCN.SelectedValue = SystemProperty.CCNID;
				}

				FillGridData();
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
		/// Create template dataset
		/// </summary>
		private void CreateDataSet()
		{
            const string METHOD_NAME = ".CreateDataSet()";
			try
			{
				dstData = new DataSet();

				dstData.Tables.Add(new DataTable());
				dstData.Tables[0].Columns.Add(new DataColumn(ITM_ProductTable.PRODUCTID_FLD));
				dstData.Tables[0].Columns.Add(new DataColumn(ITM_ProductTable.CODE_FLD));
				dstData.Tables[0].Columns.Add(new DataColumn(ITM_ProductTable.DESCRIPTION_FLD));
				dstData.Tables[0].Columns.Add(new DataColumn(ITM_ProductTable.REVISION_FLD));
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
		/// Search Work center
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnWorkCenterSearch_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = ".btnWorkCenterSearch_Click()";
			try
			{
				DataRowView drwResult = null;
				Hashtable htbCondition = new Hashtable();
				if (cboCCN.SelectedValue != null)
				{
					htbCondition.Add(MST_MasterLocationTable.CCNID_FLD, cboCCN.SelectedValue.ToString());
				}
				else
				{
					PCSMessageBox.Show(ErrorCode.MESSAGE_RGA_CCN, MessageBoxIcon.Warning);
					cboCCN.Focus();
					return;
				}
				drwResult = FormControlComponents.OpenSearchForm(MST_WorkCenterTable.TABLE_NAME, MST_WorkCenterTable.CODE_FLD, txtWorkCenter.Text.Trim(), htbCondition, true); 
				if (drwResult != null)
				{
					txtWorkCenter.Text = drwResult[MST_WorkCenterTable.CODE_FLD].ToString();
					txtWorkCenter.Tag = drwResult[MST_WorkCenterTable.WORKCENTERID_FLD].ToString();

					FillGridData();
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
		/// Get all item to bind in the grid
		/// </summary>
		private void FillGridData()
		{
			const string METHOD_NAME = THIS + ".FillItemData()";
			try
			{
				if (txtWorkCenter.Text.Trim() != string.Empty && cboCCN.SelectedValue != null)
				{
					voMaster = (PRO_ChangeCategoryMasterVO) new PRO_ChangeCategoryMasterDS().GetObjectVO(int.Parse(cboCCN.SelectedValue.ToString()), int.Parse(txtWorkCenter.Tag.ToString()));
					dstData = new ChangeCategoryBO().List(voMaster.ChangeCategoryMasterID);
				}
				else
				{
					dstData = new ChangeCategoryBO().List(0);
					voMaster.ChangeCategoryMasterID = 0;
				}
				dgrdData.DataSource = dstData.Tables[0];
				FormControlComponents.RestoreGridLayout(dgrdData, dtbGridLayOut);
				dgrdData.Splits[0].DisplayColumns[ITM_ProductTable.CODE_FLD].Button = true;
				dgrdData.Splits[0].DisplayColumns[ITM_ProductTable.DESCRIPTION_FLD].Button = true;
				dgrdData.Splits[0].DisplayColumns[ITM_ProductTable.REVISION_FLD].Locked = true;
				// 27-04-2006 dungla: add Category column and cannot edit
				dgrdData.Splits[0].DisplayColumns[ITM_CategoryTable.TABLE_NAME + ITM_CategoryTable.CODE_FLD].Locked = true;
				// 27-04-2006 dungla: add Category column and cannot edit
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

		private void btnSave_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnSave_Click()";
			try
			{
				blnHasError = true;
				if (!dgrdData.EditActive && ValidateData())
				{
					voMaster.WorkCenterID = int.Parse(txtWorkCenter.Tag.ToString());
					voMaster.CCNID = int.Parse(cboCCN.SelectedValue.ToString());
					new ChangeCategoryBO().UpdateMasterAndDetail(voMaster, dstData, arlDeletedItems);
					PCSMessageBox.Show(ErrorCode.MESSAGE_AFTER_SAVE_DATA);

					//reload grid form database
					FillGridData();
					arlDeletedItems = new ArrayList();
					blnHasError = false;
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

		private bool ValidateData()
		{
			const string METHOD_NAME = THIS + ".ValidateData()";
			try
			{
				if (FormControlComponents.CheckMandatory(cboCCN))
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Warning);
					cboCCN.Focus();
					cboCCN.Select();
					return false;
				}

				if (FormControlComponents.CheckMandatory(txtWorkCenter))
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Warning);
					txtWorkCenter.Focus();
					txtWorkCenter.Select();
					return false;
				}
				if (dgrdData.RowCount < 2)
				{
					PCSMessageBox.Show(ErrorCode.MESSAGE_CHANGE_CATEGORY_AT_LEAST_TWOROW, MessageBoxIcon.Warning);
					dgrdData.Col = dgrdData.Columns.IndexOf(dgrdData.Columns[ITM_ProductTable.CODE_FLD]);
					dgrdData.Row = 0;
					dgrdData.Focus();
					return false;
				}
			    for (int i =0; i < dgrdData.RowCount; i++)
			    {
			        if (dgrdData[i, ITM_ProductTable.CODE_FLD].ToString() == string.Empty)
			        {
			            PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Warning);
			            dgrdData.Col = dgrdData.Columns.IndexOf(dgrdData.Columns[ITM_ProductTable.CODE_FLD]);
			            dgrdData.Row = i;
			            dgrdData.Focus();
			            return false;
			        }
			    }
			    for (int i =0; i < dgrdData.RowCount; i++)
				{
					for (int j =i + 1; j < dgrdData.RowCount; j++)
					{
						if (dgrdData[i, ITM_ProductTable.PRODUCTID_FLD].ToString() != string.Empty
							&& dgrdData[j, ITM_ProductTable.PRODUCTID_FLD].ToString() != string.Empty
							&& dgrdData[j, ITM_ProductTable.PRODUCTID_FLD].ToString() == dgrdData[i, ITM_ProductTable.PRODUCTID_FLD].ToString())
						{
							PCSMessageBox.Show(ErrorCode.DUPLICATE_KEY, MessageBoxIcon.Warning);
							dgrdData.Col = dgrdData.Columns.IndexOf(dgrdData.Columns[ITM_ProductTable.CODE_FLD]);
							dgrdData.Row = i;
							dgrdData.Focus();
							return false;
						}	
					}
				}
			}
			catch (PCSException ex)
			{
				throw new PCSException(ex.mCode, METHOD_NAME, ex);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message, ex);
			}
			return true;
		}

		private void dgrdData_ButtonClick(object sender, C1.Win.C1TrueDBGrid.ColEventArgs e)
		{
			const string METHOD_NAME = THIS + ".dgrdData_ButtonClick()";
			try
			{
				DataRowView drwResult = null;
				Hashtable htbCondition= new Hashtable();
				if (!btnSave.Enabled) return;
				if (dgrdData.Col == dgrdData.Columns.IndexOf(dgrdData.Columns[ITM_ProductTable.CODE_FLD]))
				{
					//open the search form to select Product
					if (txtWorkCenter.Text.Trim() != string.Empty)
					{
						htbCondition.Add(MST_WorkCenterTable.WORKCENTERID_FLD, txtWorkCenter.Tag);
						htbCondition.Add(ITM_ProductTable.MAKEITEM_FLD, 1);
					}
					else
					{
						PCSMessageBox.Show(ErrorCode.MESSAGE_WCDISPATCH_SELECT_WORKCENTER, MessageBoxIcon.Warning);
						txtWorkCenter.Focus();
						return;
					}
					if (dgrdData.AddNewMode == AddNewModeEnum.AddNewCurrent)
					{
						drwResult = FormControlComponents.OpenSearchForm(V_PRODUCT_IN_WO, ITM_ProductTable.CODE_FLD, dgrdData[dgrdData.Row, ITM_ProductTable.CODE_FLD].ToString(), htbCondition, true);
					}
					else
					{
						drwResult = FormControlComponents.OpenSearchForm(V_PRODUCT_IN_WO, ITM_ProductTable.CODE_FLD, dgrdData.Columns[ITM_ProductTable.CODE_FLD].Text.Trim(), htbCondition, true);
					}
					if (drwResult != null)
					{
						FillItemData(drwResult.Row);
					}
				}
				if (dgrdData.Col == dgrdData.Columns.IndexOf(dgrdData.Columns[ITM_ProductTable.DESCRIPTION_FLD]))
				{
					//open the search form to select Product
					if (cboCCN.SelectedValue != null)
					{
						htbCondition.Add(MST_WorkCenterTable.WORKCENTERID_FLD, txtWorkCenter.Tag);
						htbCondition.Add(ITM_ProductTable.MAKEITEM_FLD, 1);
					}
					else
					{
						PCSMessageBox.Show(ErrorCode.MESSAGE_RGA_CCN, MessageBoxIcon.Warning);
						cboCCN.Focus();
						return;
					}
					if (dgrdData.AddNewMode == AddNewModeEnum.AddNewCurrent)
					{
						drwResult = FormControlComponents.OpenSearchForm(V_PRODUCT_IN_WO, ITM_ProductTable.DESCRIPTION_FLD, dgrdData[dgrdData.Row, ITM_ProductTable.DESCRIPTION_FLD].ToString(), htbCondition, true);
					}
					else
					{
						drwResult = FormControlComponents.OpenSearchForm(V_PRODUCT_IN_WO, ITM_ProductTable.DESCRIPTION_FLD, dgrdData.Columns[ITM_ProductTable.DESCRIPTION_FLD].Text.Trim(), htbCondition, true);
					}
					if (drwResult != null)
					{
						FillItemData(drwResult.Row);
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

		/// <summary>
		/// Fill Item's infor
		/// </summary>
		/// <param name="pdrowData"></param>
		private void FillItemData(DataRow pdrowData)
		{
			const string METHOD_NAME = THIS + ".FillItemData()";
			try
			{
				dgrdData.EditActive = true;
				if (dgrdData[dgrdData.Row, ITM_ProductTable.PRODUCTID_FLD].ToString() != pdrowData[ITM_ProductTable.PRODUCTID_FLD].ToString())
				{
					dgrdData[dgrdData.Row, MST_WorkCenterTable.CODE_FLD] = string.Empty;
				}
				dgrdData[dgrdData.Row, ITM_ProductTable.CODE_FLD] = pdrowData[ITM_ProductTable.CODE_FLD].ToString();
				dgrdData[dgrdData.Row, ITM_ProductTable.DESCRIPTION_FLD] = pdrowData[ITM_ProductTable.DESCRIPTION_FLD].ToString();
				dgrdData[dgrdData.Row, ITM_ProductTable.REVISION_FLD] = pdrowData[ITM_ProductTable.REVISION_FLD].ToString();
				dgrdData[dgrdData.Row, PRO_WorkOrderDetailTable.PRODUCTID_FLD] = pdrowData[ITM_ProductTable.PRODUCTID_FLD].ToString();
				// 27-04-2006 dungla: add Category column
				int intCategoryID = 0;
				try
				{
					intCategoryID = Convert.ToInt32(pdrowData[ITM_ProductTable.CATEGORYID_FLD]);
				}
				catch{}
				if (intCategoryID > 0)
				{
					DataRowView drvCategory = FormControlComponents.OpenSearchForm(ITM_CategoryTable.TABLE_NAME, ITM_CategoryTable.CATEGORYID_FLD, intCategoryID.ToString(), null, false);
					dgrdData[dgrdData.Row, ITM_CategoryTable.TABLE_NAME + ITM_CategoryTable.CODE_FLD] = drvCategory[ITM_CategoryTable.CODE_FLD];
				}
				// 27-04-2006 dungla: add Category column
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

		private void dgrdData_BeforeColUpdate(object sender, C1.Win.C1TrueDBGrid.BeforeColUpdateEventArgs e)
		{
			const string METHOD_NAME = ".dgrdData_BeforeColUpdate()";
			try
			{
				Hashtable htbCriteria = new Hashtable();
				DataRowView drwResult = null;
				switch (e.Column.DataColumn.DataField)
				{
					case ITM_ProductTable.CODE_FLD:
						# region open Product search form 
						if (dgrdData.Columns[e.Column.DataColumn.DataField].Text.Trim() != string.Empty)
						{
							//open the search form to select Product
							if (txtWorkCenter.Text.Trim() != string.Empty)
							{
								htbCriteria.Add(MST_WorkCenterTable.WORKCENTERID_FLD, txtWorkCenter.Tag);
								htbCriteria.Add(ITM_ProductTable.MAKEITEM_FLD, 1);
							}
							else
							{
								PCSMessageBox.Show(ErrorCode.MESSAGE_WCDISPATCH_SELECT_WORKCENTER, MessageBoxIcon.Warning);
								e.Cancel = true;
								return;
							}
							drwResult = FormControlComponents.OpenSearchForm(V_PRODUCT_IN_WO, ITM_ProductTable.CODE_FLD, e.Column.DataColumn.Value.ToString(), htbCriteria, false);
							if (drwResult != null)
							{
								e.Column.DataColumn.Tag = drwResult.Row;
							}
							else
								e.Cancel = true;
						}
						#endregion
						break;
					case ITM_ProductTable.DESCRIPTION_FLD:
						# region open Product search form 
						if (dgrdData.Columns[e.Column.DataColumn.DataField].Text.Trim() != string.Empty)
						{
							//open the search form to select Product
							if (txtWorkCenter.Text.Trim() != string.Empty)
							{
								htbCriteria.Add(MST_WorkCenterTable.WORKCENTERID_FLD, txtWorkCenter.Tag);
								htbCriteria.Add(ITM_ProductTable.MAKEITEM_FLD, 1);
							}
							else
							{
								PCSMessageBox.Show(ErrorCode.MESSAGE_WCDISPATCH_SELECT_WORKCENTER, MessageBoxIcon.Warning);
								e.Cancel = true;
								return;
							}
							drwResult = FormControlComponents.OpenSearchForm(V_PRODUCT_IN_WO, ITM_ProductTable.DESCRIPTION_FLD, e.Column.DataColumn.Value.ToString(), htbCriteria, false);
							if (drwResult != null)
							{
								e.Column.DataColumn.Tag = drwResult.Row;
							}
							else
								e.Cancel = true;
						}
						#endregion
						break;
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
			const string METHOD_NAME = ".dgrdData_AfterUpdate()";
			try
			{
				if (e.Column.DataColumn.DataField== ITM_ProductTable.CODE_FLD)
				{
					if ((e.Column.DataColumn.Tag == null) ||(dgrdData[dgrdData.Row, ITM_ProductTable.CODE_FLD].ToString() == string.Empty))
					{
						int row = dgrdData.Row;
						dgrdData[row, ITM_ProductTable.CODE_FLD]= string.Empty;
						dgrdData[row, ITM_ProductTable.DESCRIPTION_FLD] = string.Empty;
						dgrdData[row, ITM_ProductTable.REVISION_FLD] = string.Empty;
					}
					else
					{
						FillItemData((DataRow) e.Column.DataColumn.Tag);
						return;
					}
				}

				if (e.Column.DataColumn.DataField== ITM_ProductTable.DESCRIPTION_FLD)
				{
					if ((e.Column.DataColumn.Tag == null) ||(dgrdData[dgrdData.Row, ITM_ProductTable.DESCRIPTION_FLD].ToString() == string.Empty))
					{
						int row = dgrdData.Row;
						dgrdData[row, ITM_ProductTable.CODE_FLD]= string.Empty;
						dgrdData[row, ITM_ProductTable.DESCRIPTION_FLD] = string.Empty;
						dgrdData[row, ITM_ProductTable.REVISION_FLD] = string.Empty;
						dgrdData[row, ITM_ProductTable.PRODUCTID_FLD] = null;
					}
					else
					{
						FillItemData((DataRow) e.Column.DataColumn.Tag);
						return;
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

		private void btnClose_Click(object sender, System.EventArgs e)
		{
			if (dgrdData.EditActive) return;
			this.Close();
		}

		private void ChangeCategory_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".ChangeCategory_Closing()";
			try
			{
				if (dstData != null)
					if (dstData.GetChanges() != null)
					{
						DialogResult confirmDialog = PCSMessageBox.Show(ErrorCode.MESSAGE_QUESTION_STORE_INTO_DATABASE, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
						switch (confirmDialog)
						{
							case DialogResult.Yes:
								//Save before exit
								btnSave_Click(btnSave, new EventArgs());
								if (blnHasError)
								{
									e.Cancel = true;
								}
								break;
							case DialogResult.No:
								break;
							case DialogResult.Cancel:
								e.Cancel = true;
								break;
						}
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

		private void cboCCN_SelectedValueChanged(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = ".cboCCN_SelectedValueChanged()";
			try
			{
				FillGridData();
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

		private void btnDelete_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnDelete_Click()";
			try
			{
				if (dgrdData.EditActive) return;
				if (PCSMessageBox.Show(ErrorCode.MESSAGE_DELETE_RECORD, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
				{
					new ChangeCategoryBO().DeleteMasterAndDetail(voMaster.ChangeCategoryMasterID);
					voMaster = new PRO_ChangeCategoryMasterVO();
					dstData = new ChangeCategoryBO().List(voMaster.ChangeCategoryMasterID);
					dgrdData.DataSource = dstData.Tables[0];
					FormControlComponents.RestoreGridLayout(dgrdData, dtbGridLayOut);
					dgrdData.Splits[0].DisplayColumns[ITM_ProductTable.CODE_FLD].Button = true;
					dgrdData.Splits[0].DisplayColumns[ITM_ProductTable.DESCRIPTION_FLD].Button = true;
					dgrdData.Splits[0].DisplayColumns[ITM_ProductTable.REVISION_FLD].Locked = true;
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

		private void dgrdData_BeforeDelete(object sender, C1.Win.C1TrueDBGrid.CancelEventArgs e)
		{
			const string METHOD_NAME = ".dgrdData_BeforeDelete()";
			try
			{
				arlDeletedItems.Add(dgrdData[dgrdData.Row, ITM_ProductTable.PRODUCTID_FLD]);
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


		#region Change backcolor when focus or lost focus
		private void OnEnterControl(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ". OnEnterControl()";
			try
			{
				FormControlComponents.OnEnterControl(sender, e);
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

		private void OnLeaveControl(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ". OnLeaveControl()";
			try
			{
				FormControlComponents.OnLeaveControl(sender, e);
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
		#endregion

		private void txtWorkCenter_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			const string METHOD_NAME = ".()";
			try
			{
				if (e.KeyCode == Keys.F4)
				{
					btnWorkCenterSearch_Click(btnWorkCenterSearch, new EventArgs());
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

		private void dgrdData_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			const string METHOD_NAME = ".dgrdData_KeyDown()";
			try
			{
				if (e.KeyCode == Keys.F4)
				{
					dgrdData_ButtonClick(sender, null);
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

		private void ChangeCategory_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			const string METHOD_NAME = ".ChangeCategory_KeyDown()";
			try
			{
				if (e.KeyCode == Keys.F12)
				{
					dgrdData.Row = dgrdData.RowCount;
					dgrdData.Col = dgrdData.Columns.IndexOf(dgrdData.Columns[ITM_ProductTable.CODE_FLD]);
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

		private void btnMatrix_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = ".btnMatrix_Click()";
			try
			{
				if (dstData.Tables[0].Rows.Count == 0 || dstData.GetChanges() != null) return;

				ChangeCategoryMatrix frmMatrix = new ChangeCategoryMatrix(txtWorkCenter.Text, cboCCN.Text, voMaster.ChangeCategoryMasterID);
				frmMatrix.ShowDialog();
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

		private void txtWorkCenter_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			const string METHOD_NAME = ".txtWorkCenter_Validating()";
			try
			{
				if (!txtWorkCenter.Modified) return;
				if (txtWorkCenter.Text.Trim() == string.Empty)
				{
					CreateDataSet();
					dgrdData.DataSource = dstData.Tables[0];
					FormControlComponents.RestoreGridLayout(dgrdData, dtbGridLayOut);
					return;
				}
				DataRowView drwResult = null;
				Hashtable htbCondition = new Hashtable();
				if (cboCCN.SelectedValue != null)
				{
					htbCondition.Add(MST_MasterLocationTable.CCNID_FLD, cboCCN.SelectedValue.ToString());
				}
				else
				{
					PCSMessageBox.Show(ErrorCode.MESSAGE_RGA_CCN, MessageBoxIcon.Warning);
					e.Cancel = true;
					return;
				}
				drwResult = FormControlComponents.OpenSearchForm(MST_WorkCenterTable.TABLE_NAME, MST_WorkCenterTable.CODE_FLD, txtWorkCenter.Text.Trim(), htbCondition, false); 
				if (drwResult != null)
				{
					txtWorkCenter.Text = drwResult[MST_WorkCenterTable.CODE_FLD].ToString();
					txtWorkCenter.Tag = drwResult[MST_WorkCenterTable.WORKCENTERID_FLD].ToString();

					FillGridData();
				}
				else
					e.Cancel = true;
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
