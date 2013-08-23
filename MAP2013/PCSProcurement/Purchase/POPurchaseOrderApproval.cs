using System;
using System.Data;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using PCSComUtils.PCSExc;
using PCSUtils.Utils;
using PCSUtils.Log;
using PCSUtils.MasterSetup;
using PCSComUtils.Common;
using PCSComUtils.Common.BO;
using PCSComProcurement.Purchase.BO;
using PCSComProcurement.Purchase.DS;
using C1.Win.C1TrueDBGrid;
namespace PCSProcurement.Purchase
{
	/// <summary>
	/// Summary description for POPurchaseOrderApproval.
	/// </summary>
	public class POPurchaseOrderApproval : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button btnClose;
		private System.Windows.Forms.Button btnHelp;
		private System.Windows.Forms.Button btnApprove;

		#region My variable
		const string THIS = "PCSProcurement.Purchase.POPurchaseOrderApproval";
		#endregion My variable

		private System.Windows.Forms.TextBox txtPONo;
		private System.Windows.Forms.Button btnSearch;
		private C1.Win.C1TrueDBGrid.C1TrueDBGrid gridApprove;
		private const string CHECK_APPROVE = "Approved";
		private const string PO_NO = "PONo";
		private const string OPEN_AMOUNT = "openAmount";
		private const string APPROVED_ID = "ApprovedID";
		private DataTable dtbGridLayOut;
		private DataSet dstGridData;
		private const string AVAILABLE_QUANTITY = "AvailableQty";
		private const string CURRENCY = "Currency";
		private const string TRUE = "True";
		private const string PO_APPROVE_TABLE = "POApprove_Table";
		private DataTable dtbSource = new DataTable(GRIDSOURCE);
		private const string GRIDSOURCE = "GridSource";
		private const string BUYINGUM = "BuyingUM";
		private const string V_PO_NOT_APPROVE = "V_PO_NOT_APPROVE";
		private const string V_PO_APPROVE = "V_PO_APPROVE";
		private bool blnStateOfCheck = false;
		private System.Windows.Forms.CheckBox chkSelectAll;
		private C1.Win.C1List.C1Combo cboCCN;
		private System.Windows.Forms.Button btnPONo;
		private System.Windows.Forms.Label lblCCN;
		private System.Windows.Forms.Label lblPONo;
		private System.Windows.Forms.Label lblApprover;
		private System.Windows.Forms.Label lblApprDate;
		private System.Windows.Forms.TextBox txtApprover;
		private C1.Win.C1Input.C1DateEdit dtmApprDate;
		private System.Windows.Forms.Button btnShowDetail;
		private System.Windows.Forms.ComboBox cboStatus;
		private System.Windows.Forms.Label lblApproved;
		private System.Windows.Forms.Label lblNotApproved;
		private System.Windows.Forms.Label lblCancelApprove;
		private System.Windows.Forms.Label lblCancelDate;
		private System.Windows.Forms.Label lblCanceler;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		string strlblApprovalDate, strbtnApprove, strlblApprover;
		private System.Windows.Forms.Label lblStatus;

		public POPurchaseOrderApproval()
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(POPurchaseOrderApproval));
			this.cboCCN = new C1.Win.C1List.C1Combo();
			this.lblCCN = new System.Windows.Forms.Label();
			this.txtPONo = new System.Windows.Forms.TextBox();
			this.btnPONo = new System.Windows.Forms.Button();
			this.lblPONo = new System.Windows.Forms.Label();
			this.lblApprover = new System.Windows.Forms.Label();
			this.chkSelectAll = new System.Windows.Forms.CheckBox();
			this.lblApprDate = new System.Windows.Forms.Label();
			this.btnApprove = new System.Windows.Forms.Button();
			this.btnClose = new System.Windows.Forms.Button();
			this.btnHelp = new System.Windows.Forms.Button();
			this.gridApprove = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
			this.btnSearch = new System.Windows.Forms.Button();
			this.txtApprover = new System.Windows.Forms.TextBox();
			this.dtmApprDate = new C1.Win.C1Input.C1DateEdit();
			this.btnShowDetail = new System.Windows.Forms.Button();
			this.lblStatus = new System.Windows.Forms.Label();
			this.cboStatus = new System.Windows.Forms.ComboBox();
			this.lblApproved = new System.Windows.Forms.Label();
			this.lblNotApproved = new System.Windows.Forms.Label();
			this.lblCancelApprove = new System.Windows.Forms.Label();
			this.lblCancelDate = new System.Windows.Forms.Label();
			this.lblCanceler = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.cboCCN)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gridApprove)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dtmApprDate)).BeginInit();
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
			this.cboCCN.Location = new System.Drawing.Point(550, 6);
			this.cboCCN.MatchEntryTimeout = ((long)(2000));
			this.cboCCN.MaxDropDownItems = ((short)(5));
			this.cboCCN.MaxLength = 32767;
			this.cboCCN.MouseCursor = System.Windows.Forms.Cursors.Default;
			this.cboCCN.Name = "cboCCN";
			this.cboCCN.RowDivider.Color = System.Drawing.Color.DarkGray;
			this.cboCCN.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None;
			this.cboCCN.RowSubDividerColor = System.Drawing.Color.DarkGray;
			this.cboCCN.Size = new System.Drawing.Size(80, 21);
			this.cboCCN.TabIndex = 1;
			this.cboCCN.Text = "CCN";
			this.cboCCN.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.cboCCN.Enter += new System.EventHandler(this.OnEnterControl);
			this.cboCCN.Leave += new System.EventHandler(this.OnLeaveControl);
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
				" 118, 158</ClientRect><VScrollBar><Width>16</Width></VScrollBar><HScrollBar><Hei" +
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
			this.lblCCN.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.lblCCN.ForeColor = System.Drawing.Color.Maroon;
			this.lblCCN.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lblCCN.Location = new System.Drawing.Point(517, 6);
			this.lblCCN.Name = "lblCCN";
			this.lblCCN.Size = new System.Drawing.Size(30, 19);
			this.lblCCN.TabIndex = 0;
			this.lblCCN.Text = "CCN";
			this.lblCCN.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// txtPONo
			// 
			this.txtPONo.Location = new System.Drawing.Point(84, 27);
			this.txtPONo.Name = "txtPONo";
			this.txtPONo.Size = new System.Drawing.Size(122, 20);
			this.txtPONo.TabIndex = 5;
			this.txtPONo.Text = "";
			this.txtPONo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPONo_KeyDown);
			this.txtPONo.Validating += new System.ComponentModel.CancelEventHandler(this.txtPONo_Validating);
			// 
			// btnPONo
			// 
			this.btnPONo.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnPONo.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnPONo.Location = new System.Drawing.Point(207, 27);
			this.btnPONo.Name = "btnPONo";
			this.btnPONo.Size = new System.Drawing.Size(21, 20);
			this.btnPONo.TabIndex = 6;
			this.btnPONo.Text = "...";
			this.btnPONo.Click += new System.EventHandler(this.btnPONo_Click);
			// 
			// lblPONo
			// 
			this.lblPONo.ForeColor = System.Drawing.Color.Black;
			this.lblPONo.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lblPONo.Location = new System.Drawing.Point(6, 27);
			this.lblPONo.Name = "lblPONo";
			this.lblPONo.Size = new System.Drawing.Size(76, 20);
			this.lblPONo.TabIndex = 4;
			this.lblPONo.Text = "PO No.";
			this.lblPONo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblApprover
			// 
			this.lblApprover.ForeColor = System.Drawing.Color.Maroon;
			this.lblApprover.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lblApprover.Location = new System.Drawing.Point(231, 50);
			this.lblApprover.Name = "lblApprover";
			this.lblApprover.Size = new System.Drawing.Size(52, 20);
			this.lblApprover.TabIndex = 9;
			this.lblApprover.Text = "Approver";
			this.lblApprover.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// chkSelectAll
			// 
			this.chkSelectAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.chkSelectAll.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.chkSelectAll.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.chkSelectAll.Location = new System.Drawing.Point(82, 431);
			this.chkSelectAll.Name = "chkSelectAll";
			this.chkSelectAll.Size = new System.Drawing.Size(70, 18);
			this.chkSelectAll.TabIndex = 13;
			this.chkSelectAll.Text = "Se&lect All";
			this.chkSelectAll.Enter += new System.EventHandler(this.chkSelectAll_Enter);
			this.chkSelectAll.Leave += new System.EventHandler(this.chkSelectAll_Leave);
			this.chkSelectAll.CheckedChanged += new System.EventHandler(this.chkSelectAll_CheckedChanged);
			// 
			// lblApprDate
			// 
			this.lblApprDate.ForeColor = System.Drawing.Color.Maroon;
			this.lblApprDate.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lblApprDate.Location = new System.Drawing.Point(6, 50);
			this.lblApprDate.Name = "lblApprDate";
			this.lblApprDate.Size = new System.Drawing.Size(76, 20);
			this.lblApprDate.TabIndex = 7;
			this.lblApprDate.Text = "Approval Date";
			this.lblApprDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// btnApprove
			// 
			this.btnApprove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnApprove.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnApprove.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnApprove.Location = new System.Drawing.Point(4, 428);
			this.btnApprove.Name = "btnApprove";
			this.btnApprove.Size = new System.Drawing.Size(76, 22);
			this.btnApprove.TabIndex = 12;
			this.btnApprove.Text = "&Approve";
			this.btnApprove.Click += new System.EventHandler(this.btnApprove_Click);
			// 
			// btnClose
			// 
			this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnClose.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnClose.Location = new System.Drawing.Point(566, 428);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(64, 22);
			this.btnClose.TabIndex = 16;
			this.btnClose.Text = "&Close";
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			// 
			// btnHelp
			// 
			this.btnHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnHelp.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnHelp.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnHelp.Location = new System.Drawing.Point(501, 428);
			this.btnHelp.Name = "btnHelp";
			this.btnHelp.Size = new System.Drawing.Size(64, 22);
			this.btnHelp.TabIndex = 15;
			this.btnHelp.Text = "&Help";
			// 
			// gridApprove
			// 
			this.gridApprove.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.gridApprove.CaptionHeight = 17;
			this.gridApprove.CollapseColor = System.Drawing.Color.Black;
			this.gridApprove.ExpandColor = System.Drawing.Color.Black;
			this.gridApprove.FlatStyle = C1.Win.C1TrueDBGrid.FlatModeEnum.System;
			this.gridApprove.GroupByCaption = "Drag a column header here to group by that column";
			this.gridApprove.Images.Add(((System.Drawing.Image)(resources.GetObject("resource"))));
			this.gridApprove.Location = new System.Drawing.Point(4, 76);
			this.gridApprove.MarqueeStyle = C1.Win.C1TrueDBGrid.MarqueeEnum.DottedCellBorder;
			this.gridApprove.Name = "gridApprove";
			this.gridApprove.PreviewInfo.Location = new System.Drawing.Point(0, 0);
			this.gridApprove.PreviewInfo.Size = new System.Drawing.Size(0, 0);
			this.gridApprove.PreviewInfo.ZoomFactor = 75;
			this.gridApprove.PrintInfo.ShowOptionsDialog = false;
			this.gridApprove.RecordSelectorWidth = 17;
			this.gridApprove.RowDivider.Color = System.Drawing.Color.DarkGray;
			this.gridApprove.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.Single;
			this.gridApprove.RowHeight = 14;
			this.gridApprove.RowSubDividerColor = System.Drawing.Color.DarkGray;
			this.gridApprove.Size = new System.Drawing.Size(626, 346);
			this.gridApprove.TabIndex = 10;
			this.gridApprove.Text = "c1TrueDBGrid1";
			this.gridApprove.AfterColEdit += new C1.Win.C1TrueDBGrid.ColEventHandler(this.gridApprove_AfterColEdit);
			this.gridApprove.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"PO No.\" Dat" +
				"aField=\"PONo\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" " +
				"Caption=\"Total Amount\" DataField=\"TotalAmount\"><ValueItems /><GroupInfo /></C1Da" +
				"taColumn><C1DataColumn Level=\"0\" Caption=\"Open Amount\" DataField=\"openAmount\"><V" +
				"alueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Approve" +
				"d\" DataField=\"Approved\"><ValueItems Presentation=\"CheckBox\" /><GroupInfo /></C1D" +
				"ataColumn><C1DataColumn Level=\"0\" Caption=\"PO Line\" DataField=\"Line\"><ValueItems" +
				" /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Category\" DataFi" +
				"eld=\"ITM_CategoryCode\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn L" +
				"evel=\"0\" Caption=\"Part Number\" DataField=\"Code\"><ValueItems /><GroupInfo /></C1D" +
				"ataColumn><C1DataColumn Level=\"0\" Caption=\"Part Name\" DataField=\"Description\"><V" +
				"alueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Model\" " +
				"DataField=\"Revision\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Lev" +
				"el=\"0\" Caption=\"Buying UM\" DataField=\"BuyingUM\"><ValueItems /><GroupInfo /></C1D" +
				"ataColumn><C1DataColumn Level=\"0\" Caption=\"Order Quantity \" DataField=\"OrderQuan" +
				"tity\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=" +
				"\"Currency\" DataField=\"Currency\"><ValueItems /><GroupInfo /></C1DataColumn><C1Dat" +
				"aColumn Level=\"0\" Caption=\"Available Qty\" DataField=\"AvailableQty\"><ValueItems /" +
				"><GroupInfo /></C1DataColumn></DataCols><Styles type=\"C1.Win.C1TrueDBGrid.Design" +
				".ContextWrapper\"><Data>HighlightRow{ForeColor:HighlightText;BackColor:Highlight;" +
				"}Style85{}Inactive{ForeColor:InactiveCaptionText;BackColor:InactiveCaption;}Styl" +
				"e78{}Style79{}Selected{ForeColor:HighlightText;BackColor:Highlight;}Editor{}Styl" +
				"e72{}Style73{}Style70{AlignHorz:Center;}Style71{AlignHorz:Near;}Style76{AlignHor" +
				"z:Center;}Style77{AlignHorz:Near;}Style74{}Style75{}Style84{}Style87{}Style86{}S" +
				"tyle81{}Style80{}Style83{AlignHorz:Near;}Style82{AlignHorz:Center;}Footer{}Style" +
				"89{AlignHorz:Near;}Style88{AlignHorz:Center;}Style94{AlignHorz:Center;}Style95{A" +
				"lignHorz:Near;}Style96{}Style97{}Style90{}Style91{}Style92{}Style93{}RecordSelec" +
				"tor{AlignImage:Center;}Style98{}Style99{}Heading{Wrap:True;AlignVert:Center;Bord" +
				"er:Raised,,1, 1, 1, 1;ForeColor:ControlText;BackColor:Control;}Style18{}Style19{" +
				"}Style14{}Style15{}Style16{AlignHorz:Center;}Style17{AlignHorz:Near;}Style10{Ali" +
				"gnHorz:Near;}Style11{}Style12{}Style13{}Style29{AlignHorz:Near;}Style28{AlignHor" +
				"z:Center;}Style27{}Style22{AlignHorz:Center;}Style9{}Style8{}Style26{}Style25{}S" +
				"tyle5{}Style4{}Style7{}Style6{}Style24{}Style23{AlignHorz:Near;}Style3{}Style2{}" +
				"Style21{}Style20{}OddRow{}Style49{}Style38{}Style39{}Style36{}FilterBar{}Style34" +
				"{AlignHorz:Center;}Style35{AlignHorz:Near;}Style32{}Style33{}Style30{}Style37{}S" +
				"tyle48{}Style31{}Normal{}Style41{AlignHorz:Near;}Style40{AlignHorz:Center;}Style" +
				"43{}Style42{}Style45{}Style44{}Style47{AlignHorz:Near;}Style46{AlignHorz:Center;" +
				"}EvenRow{BackColor:Aqua;}Style58{AlignHorz:Center;}Style59{AlignHorz:Near;}Style" +
				"50{}Style51{}Caption{AlignHorz:Center;}Style69{}Style68{}Style1{}Style63{}Style6" +
				"2{}Style61{}Style60{}Style67{}Style66{}Style65{AlignHorz:Near;}Style64{AlignHorz" +
				":Center;}Group{BackColor:ControlDark;Border:None,,0, 0, 0, 0;AlignVert:Center;}<" +
				"/Data></Styles><Splits><C1.Win.C1TrueDBGrid.MergeView Name=\"\" CaptionHeight=\"17\"" +
				" ColumnCaptionHeight=\"17\" ColumnFooterHeight=\"17\" MarqueeStyle=\"DottedCellBorder" +
				"\" RecordSelectorWidth=\"17\" DefRecSelWidth=\"17\" VerticalScrollGroup=\"1\" Horizonta" +
				"lScrollGroup=\"1\"><ClientRect>0, 0, 622, 342</ClientRect><BorderSide>0</BorderSid" +
				"e><CaptionStyle parent=\"Style2\" me=\"Style10\" /><EditorStyle parent=\"Editor\" me=\"" +
				"Style5\" /><EvenRowStyle parent=\"EvenRow\" me=\"Style8\" /><FilterBarStyle parent=\"F" +
				"ilterBar\" me=\"Style13\" /><FooterStyle parent=\"Footer\" me=\"Style3\" /><GroupStyle " +
				"parent=\"Group\" me=\"Style12\" /><HeadingStyle parent=\"Heading\" me=\"Style2\" /><High" +
				"LightRowStyle parent=\"HighlightRow\" me=\"Style7\" /><InactiveStyle parent=\"Inactiv" +
				"e\" me=\"Style4\" /><OddRowStyle parent=\"OddRow\" me=\"Style9\" /><RecordSelectorStyle" +
				" parent=\"RecordSelector\" me=\"Style11\" /><SelectedStyle parent=\"Selected\" me=\"Sty" +
				"le6\" /><Style parent=\"Normal\" me=\"Style1\" /><internalCols><C1DisplayColumn><Head" +
				"ingStyle parent=\"Style2\" me=\"Style16\" /><Style parent=\"Style1\" me=\"Style17\" /><F" +
				"ooterStyle parent=\"Style3\" me=\"Style18\" /><EditorStyle parent=\"Style5\" me=\"Style" +
				"19\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style21\" /><GroupFooterStyle parent=" +
				"\"Style1\" me=\"Style20\" /><Visible>True</Visible><ColumnDivider>DarkGray,Single</C" +
				"olumnDivider><Width>126</Width><Height>15</Height><DCIdx>0</DCIdx></C1DisplayCol" +
				"umn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style46\" /><Style parent=" +
				"\"Style1\" me=\"Style47\" /><FooterStyle parent=\"Style3\" me=\"Style48\" /><EditorStyle" +
				" parent=\"Style5\" me=\"Style49\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style51\" /" +
				"><GroupFooterStyle parent=\"Style1\" me=\"Style50\" /><Visible>True</Visible><Column" +
				"Divider>DarkGray,Single</ColumnDivider><Width>58</Width><Height>15</Height><DCId" +
				"x>4</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"" +
				"Style40\" /><Style parent=\"Style1\" me=\"Style41\" /><FooterStyle parent=\"Style3\" me" +
				"=\"Style42\" /><EditorStyle parent=\"Style5\" me=\"Style43\" /><GroupHeaderStyle paren" +
				"t=\"Style1\" me=\"Style45\" /><GroupFooterStyle parent=\"Style1\" me=\"Style44\" /><Visi" +
				"ble>True</Visible><ColumnDivider>DarkGray,Single</ColumnDivider><Width>84</Width" +
				"><Height>15</Height><DCIdx>5</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingS" +
				"tyle parent=\"Style2\" me=\"Style58\" /><Style parent=\"Style1\" me=\"Style59\" /><Foote" +
				"rStyle parent=\"Style3\" me=\"Style60\" /><EditorStyle parent=\"Style5\" me=\"Style61\" " +
				"/><GroupHeaderStyle parent=\"Style1\" me=\"Style63\" /><GroupFooterStyle parent=\"Sty" +
				"le1\" me=\"Style62\" /><Visible>True</Visible><ColumnDivider>DarkGray,Single</Colum" +
				"nDivider><Width>145</Width><Height>15</Height><DCIdx>6</DCIdx></C1DisplayColumn>" +
				"<C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style64\" /><Style parent=\"Sty" +
				"le1\" me=\"Style65\" /><FooterStyle parent=\"Style3\" me=\"Style66\" /><EditorStyle par" +
				"ent=\"Style5\" me=\"Style67\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style69\" /><Gr" +
				"oupFooterStyle parent=\"Style1\" me=\"Style68\" /><Visible>True</Visible><ColumnDivi" +
				"der>DarkGray,Single</ColumnDivider><Width>150</Width><Height>15</Height><DCIdx>7" +
				"</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Sty" +
				"le70\" /><Style parent=\"Style1\" me=\"Style71\" /><FooterStyle parent=\"Style3\" me=\"S" +
				"tyle72\" /><EditorStyle parent=\"Style5\" me=\"Style73\" /><GroupHeaderStyle parent=\"" +
				"Style1\" me=\"Style75\" /><GroupFooterStyle parent=\"Style1\" me=\"Style74\" /><Visible" +
				">True</Visible><ColumnDivider>DarkGray,Single</ColumnDivider><Width>96</Width><H" +
				"eight>15</Height><DCIdx>8</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyl" +
				"e parent=\"Style2\" me=\"Style76\" /><Style parent=\"Style1\" me=\"Style77\" /><FooterSt" +
				"yle parent=\"Style3\" me=\"Style78\" /><EditorStyle parent=\"Style5\" me=\"Style79\" /><" +
				"GroupHeaderStyle parent=\"Style1\" me=\"Style81\" /><GroupFooterStyle parent=\"Style1" +
				"\" me=\"Style80\" /><Visible>True</Visible><ColumnDivider>DarkGray,Single</ColumnDi" +
				"vider><Width>74</Width><Height>15</Height><DCIdx>9</DCIdx></C1DisplayColumn><C1D" +
				"isplayColumn><HeadingStyle parent=\"Style2\" me=\"Style82\" /><Style parent=\"Style1\"" +
				" me=\"Style83\" /><FooterStyle parent=\"Style3\" me=\"Style84\" /><EditorStyle parent=" +
				"\"Style5\" me=\"Style85\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style87\" /><GroupF" +
				"ooterStyle parent=\"Style1\" me=\"Style86\" /><Visible>True</Visible><ColumnDivider>" +
				"DarkGray,Single</ColumnDivider><Width>86</Width><Height>15</Height><DCIdx>10</DC" +
				"Idx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style94" +
				"\" /><Style parent=\"Style1\" me=\"Style95\" /><FooterStyle parent=\"Style3\" me=\"Style" +
				"96\" /><EditorStyle parent=\"Style5\" me=\"Style97\" /><GroupHeaderStyle parent=\"Styl" +
				"e1\" me=\"Style99\" /><GroupFooterStyle parent=\"Style1\" me=\"Style98\" /><Visible>Tru" +
				"e</Visible><ColumnDivider>DarkGray,Single</ColumnDivider><Height>15</Height><DCI" +
				"dx>12</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me" +
				"=\"Style88\" /><Style parent=\"Style1\" me=\"Style89\" /><FooterStyle parent=\"Style3\" " +
				"me=\"Style90\" /><EditorStyle parent=\"Style5\" me=\"Style91\" /><GroupHeaderStyle par" +
				"ent=\"Style1\" me=\"Style93\" /><GroupFooterStyle parent=\"Style1\" me=\"Style92\" /><Vi" +
				"sible>True</Visible><ColumnDivider>DarkGray,Single</ColumnDivider><Width>60</Wid" +
				"th><Height>15</Height><DCIdx>11</DCIdx></C1DisplayColumn><C1DisplayColumn><Headi" +
				"ngStyle parent=\"Style2\" me=\"Style22\" /><Style parent=\"Style1\" me=\"Style23\" /><Fo" +
				"oterStyle parent=\"Style3\" me=\"Style24\" /><EditorStyle parent=\"Style5\" me=\"Style2" +
				"5\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style27\" /><GroupFooterStyle parent=\"" +
				"Style1\" me=\"Style26\" /><Visible>True</Visible><ColumnDivider>DarkGray,Single</Co" +
				"lumnDivider><Width>112</Width><Height>15</Height><DCIdx>1</DCIdx></C1DisplayColu" +
				"mn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style28\" /><Style parent=\"" +
				"Style1\" me=\"Style29\" /><FooterStyle parent=\"Style3\" me=\"Style30\" /><EditorStyle " +
				"parent=\"Style5\" me=\"Style31\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style33\" />" +
				"<GroupFooterStyle parent=\"Style1\" me=\"Style32\" /><Visible>True</Visible><ColumnD" +
				"ivider>DarkGray,Single</ColumnDivider><Width>96</Width><Height>15</Height><DCIdx" +
				">2</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"S" +
				"tyle34\" /><Style parent=\"Style1\" me=\"Style35\" /><FooterStyle parent=\"Style3\" me=" +
				"\"Style36\" /><EditorStyle parent=\"Style5\" me=\"Style37\" /><GroupHeaderStyle parent" +
				"=\"Style1\" me=\"Style39\" /><GroupFooterStyle parent=\"Style1\" me=\"Style38\" /><Visib" +
				"le>True</Visible><ColumnDivider>DarkGray,Single</ColumnDivider><Width>57</Width>" +
				"<Height>15</Height><DCIdx>3</DCIdx></C1DisplayColumn></internalCols></C1.Win.C1T" +
				"rueDBGrid.MergeView></Splits><NamedStyles><Style parent=\"\" me=\"Normal\" /><Style " +
				"parent=\"Normal\" me=\"Heading\" /><Style parent=\"Heading\" me=\"Footer\" /><Style pare" +
				"nt=\"Heading\" me=\"Caption\" /><Style parent=\"Heading\" me=\"Inactive\" /><Style paren" +
				"t=\"Normal\" me=\"Selected\" /><Style parent=\"Normal\" me=\"Editor\" /><Style parent=\"N" +
				"ormal\" me=\"HighlightRow\" /><Style parent=\"Normal\" me=\"EvenRow\" /><Style parent=\"" +
				"Normal\" me=\"OddRow\" /><Style parent=\"Heading\" me=\"RecordSelector\" /><Style paren" +
				"t=\"Normal\" me=\"FilterBar\" /><Style parent=\"Caption\" me=\"Group\" /></NamedStyles><" +
				"vertSplits>1</vertSplits><horzSplits>1</horzSplits><Layout>Modified</Layout><Def" +
				"aultRecSelWidth>17</DefaultRecSelWidth><ClientArea>0, 0, 622, 342</ClientArea><P" +
				"rintPageHeaderStyle parent=\"\" me=\"Style14\" /><PrintPageFooterStyle parent=\"\" me=" +
				"\"Style15\" /></Blob>";
			// 
			// btnSearch
			// 
			this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnSearch.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnSearch.Location = new System.Drawing.Point(550, 49);
			this.btnSearch.Name = "btnSearch";
			this.btnSearch.Size = new System.Drawing.Size(80, 22);
			this.btnSearch.TabIndex = 11;
			this.btnSearch.Text = "&Search";
			this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
			// 
			// txtApprover
			// 
			this.txtApprover.Location = new System.Drawing.Point(285, 50);
			this.txtApprover.Name = "txtApprover";
			this.txtApprover.Size = new System.Drawing.Size(101, 20);
			this.txtApprover.TabIndex = 10;
			this.txtApprover.Text = "";
			// 
			// dtmApprDate
			// 
			this.dtmApprDate.EmptyAsNull = true;
			this.dtmApprDate.Location = new System.Drawing.Point(84, 50);
			this.dtmApprDate.Name = "dtmApprDate";
			this.dtmApprDate.Size = new System.Drawing.Size(122, 20);
			this.dtmApprDate.TabIndex = 8;
			this.dtmApprDate.Tag = null;
			this.dtmApprDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.dtmApprDate.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.DropDown;
			// 
			// btnShowDetail
			// 
			this.btnShowDetail.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnShowDetail.Location = new System.Drawing.Point(416, 428);
			this.btnShowDetail.Name = "btnShowDetail";
			this.btnShowDetail.Size = new System.Drawing.Size(84, 22);
			this.btnShowDetail.TabIndex = 14;
			this.btnShowDetail.Text = "Show D&etail";
			this.btnShowDetail.Click += new System.EventHandler(this.btnShowDetail_Click);
			// 
			// lblStatus
			// 
			this.lblStatus.ForeColor = System.Drawing.Color.Maroon;
			this.lblStatus.Location = new System.Drawing.Point(6, 5);
			this.lblStatus.Name = "lblStatus";
			this.lblStatus.Size = new System.Drawing.Size(76, 20);
			this.lblStatus.TabIndex = 2;
			this.lblStatus.Text = "Status";
			this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// cboStatus
			// 
			this.cboStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboStatus.Location = new System.Drawing.Point(84, 4);
			this.cboStatus.Name = "cboStatus";
			this.cboStatus.Size = new System.Drawing.Size(122, 21);
			this.cboStatus.TabIndex = 3;
			this.cboStatus.SelectedValueChanged += new System.EventHandler(this.cboStatus_SelectedValueChanged);
			// 
			// lblApproved
			// 
			this.lblApproved.Location = new System.Drawing.Point(414, 48);
			this.lblApproved.Name = "lblApproved";
			this.lblApproved.Size = new System.Drawing.Size(56, 20);
			this.lblApproved.TabIndex = 18;
			this.lblApproved.Text = "Approved";
			this.lblApproved.Visible = false;
			// 
			// lblNotApproved
			// 
			this.lblNotApproved.Location = new System.Drawing.Point(472, 48);
			this.lblNotApproved.Name = "lblNotApproved";
			this.lblNotApproved.Size = new System.Drawing.Size(78, 20);
			this.lblNotApproved.TabIndex = 19;
			this.lblNotApproved.Text = "Not Approved";
			this.lblNotApproved.Visible = false;
			// 
			// lblCancelApprove
			// 
			this.lblCancelApprove.Location = new System.Drawing.Point(414, 22);
			this.lblCancelApprove.Name = "lblCancelApprove";
			this.lblCancelApprove.Size = new System.Drawing.Size(88, 20);
			this.lblCancelApprove.TabIndex = 20;
			this.lblCancelApprove.Text = "&Cancel";
			this.lblCancelApprove.Visible = false;
			// 
			// lblCancelDate
			// 
			this.lblCancelDate.Location = new System.Drawing.Point(336, 22);
			this.lblCancelDate.Name = "lblCancelDate";
			this.lblCancelDate.Size = new System.Drawing.Size(76, 20);
			this.lblCancelDate.TabIndex = 21;
			this.lblCancelDate.Text = "Cancel Date";
			this.lblCancelDate.Visible = false;
			// 
			// lblCanceler
			// 
			this.lblCanceler.ForeColor = System.Drawing.Color.Maroon;
			this.lblCanceler.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lblCanceler.Location = new System.Drawing.Point(336, 1);
			this.lblCanceler.Name = "lblCanceler";
			this.lblCanceler.Size = new System.Drawing.Size(52, 20);
			this.lblCanceler.TabIndex = 22;
			this.lblCanceler.Text = "Canceler";
			this.lblCanceler.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.lblCanceler.Visible = false;
			// 
			// POPurchaseOrderApproval
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.btnClose;
			this.ClientSize = new System.Drawing.Size(634, 457);
			this.Controls.Add(this.lblCanceler);
			this.Controls.Add(this.lblCancelDate);
			this.Controls.Add(this.lblCancelApprove);
			this.Controls.Add(this.lblNotApproved);
			this.Controls.Add(this.lblApproved);
			this.Controls.Add(this.cboStatus);
			this.Controls.Add(this.lblStatus);
			this.Controls.Add(this.btnShowDetail);
			this.Controls.Add(this.dtmApprDate);
			this.Controls.Add(this.txtApprover);
			this.Controls.Add(this.txtPONo);
			this.Controls.Add(this.btnSearch);
			this.Controls.Add(this.btnHelp);
			this.Controls.Add(this.btnApprove);
			this.Controls.Add(this.btnClose);
			this.Controls.Add(this.gridApprove);
			this.Controls.Add(this.chkSelectAll);
			this.Controls.Add(this.lblApprDate);
			this.Controls.Add(this.cboCCN);
			this.Controls.Add(this.lblCCN);
			this.Controls.Add(this.btnPONo);
			this.Controls.Add(this.lblPONo);
			this.Controls.Add(this.lblApprover);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.KeyPreview = true;
			this.Name = "POPurchaseOrderApproval";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Approve/Cancel Purchase Order Approval";
			this.Load += new System.EventHandler(this.POPurchaseOrderApproval_Load);
			((System.ComponentModel.ISupportInitialize)(this.cboCCN)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gridApprove)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dtmApprDate)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion
		
		private void btnClose_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void POPurchaseOrderApproval_Load(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".POPurchaseOrderApproval_Load()";
			try
			{
				//Set authorization for user
				
				Security objSecurity = new Security();
				this.Name = THIS;
				if(objSecurity.SetRightForUserOnForm(this, SystemProperty.UserName) == 0)
				{
					this.Close();
					// You don't have the right to view this item
					PCSMessageBox.Show(ErrorCode.MESSAGE_YOU_HAVE_NO_RIGHT_TO_VIEW, MessageBoxIcon.Warning);
					return;
				}
				dtbGridLayOut = FormControlComponents.StoreGridLayout(gridApprove);
				// load form
				dtmApprDate.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
				dtmApprDate.CustomFormat = Constants.DATETIME_FORMAT;
				InitVariable();

				cboStatus.Items.Add(lblApproved.Text);
				cboStatus.Items.Add(lblNotApproved.Text);

				strlblApprovalDate = lblApprDate.Text.Trim();
				strbtnApprove = btnApprove.Text.Trim();
				strlblApprover = lblApprover.Text.Trim();

				txtApprover.ReadOnly = true;
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
		/// CreatDataSet
		/// </summary>
		/// <author>Trada</author>
		/// <date>Wednesday, October 12 2005</date>
		private void CreatDataSet()
		{
			const string METHOD_NAME = THIS + ".CreateDataSet()";
			try
			{
				dstGridData = new DataSet();
				dstGridData.Tables.Add(PO_APPROVE_TABLE);
				dstGridData.Tables[PO_APPROVE_TABLE].Columns.Add(PO_NO);
				dstGridData.Tables[PO_APPROVE_TABLE].Columns.Add(PO_PurchaseOrderDetailTable.LINE_FLD);
				dstGridData.Tables[PO_APPROVE_TABLE].Columns.Add(ITM_CategoryTable.TABLE_NAME + ITM_CategoryTable.CODE_FLD);
				dstGridData.Tables[PO_APPROVE_TABLE].Columns.Add(ITM_ProductTable.CODE_FLD);
				dstGridData.Tables[PO_APPROVE_TABLE].Columns.Add(ITM_ProductTable.DESCRIPTION_FLD);
				dstGridData.Tables[PO_APPROVE_TABLE].Columns.Add(ITM_ProductTable.REVISION_FLD);
				dstGridData.Tables[PO_APPROVE_TABLE].Columns.Add(BUYINGUM);
				dstGridData.Tables[PO_APPROVE_TABLE].Columns.Add(PO_PurchaseOrderDetailTable.ORDERQUANTITY_FLD);
				dstGridData.Tables[PO_APPROVE_TABLE].Columns.Add(AVAILABLE_QUANTITY);
				dstGridData.Tables[PO_APPROVE_TABLE].Columns.Add(CURRENCY);
				dstGridData.Tables[PO_APPROVE_TABLE].Columns.Add(PO_PurchaseOrderDetailTable.TOTALAMOUNT_FLD);
				dstGridData.Tables[PO_APPROVE_TABLE].Columns.Add(OPEN_AMOUNT);
				dstGridData.Tables[PO_APPROVE_TABLE].Columns.Add(CHECK_APPROVE, typeof(bool));
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
		private void InitVariable()
		{
			const string METHOD_NAME = THIS + ".InitVariable()";
			try
			{
				// Load combo box
				UtilsBO boUtil = new UtilsBO();
				DataSet dstCCN = boUtil.ListCCN();
				cboCCN.DataSource = dstCCN.Tables[MST_CCNTable.TABLE_NAME];
				cboCCN.DisplayMember = MST_CCNTable.CODE_FLD;
				cboCCN.ValueMember = MST_CCNTable.CCNID_FLD;
				FormControlComponents.PutDataIntoC1ComboBox(cboCCN,dstCCN.Tables[MST_CCNTable.TABLE_NAME],MST_CCNTable.CODE_FLD,MST_CCNTable.CCNID_FLD,MST_CCNTable.TABLE_NAME);
				if (SystemProperty.CCNID != 0)
				{
					cboCCN.SelectedValue = SystemProperty.CCNID;
				}

				// Load combo box displays approving date
				PO_PurchaseOrderMasterVO voMasterBlank = new PO_PurchaseOrderMasterVO();
				voMasterBlank.OrderDate = boUtil.GetDBDate();
				if((DateTime.MinValue < voMasterBlank.OrderDate) && (voMasterBlank.OrderDate < DateTime.MaxValue))
					dtmApprDate.Value = DateTime.Parse(voMasterBlank.OrderDate.ToString());
				else
					dtmApprDate.Value = DBNull.Value;
				// Load approver
				txtApprover.Text = SystemProperty.EmployeeName;
				if (SystemProperty.EmployeeID != 0)
				{
					txtApprover.Tag = SystemProperty.EmployeeID;
				}

				txtApprover.Enabled = false;
				btnShowDetail.Enabled = false;
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
		private void btnPONo_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnPONo_Click()";
			try 
			{
				Hashtable htbCriteria = new Hashtable();
				DataRowView drwResult = null;
				//User has selected CCN
				if (cboStatus.Text.Trim() == string.Empty)
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Warning);
					cboStatus.Focus();
					return;
				}

				if (cboCCN.SelectedIndex != -1 )
				{
					htbCriteria.Add(PO_PurchaseOrderMasterTable.CCNID_FLD, cboCCN.SelectedValue);	
				}
				//User has not selected CCN
				else
				{
					PCSMessageBox.Show(ErrorCode.MESSAGE_RGA_CCN, MessageBoxIcon.Warning);
					cboCCN.Focus();
					return;
				}
				if (cboStatus.Text.Trim() == lblNotApproved.Text.Trim())
					drwResult = FormControlComponents.OpenSearchForm(V_PO_NOT_APPROVE, PO_PurchaseOrderMasterTable.CODE_FLD, txtPONo.Text.Trim(), htbCriteria, true);
				else
					drwResult = FormControlComponents.OpenSearchForm(V_PO_APPROVE, PO_PurchaseOrderMasterTable.CODE_FLD, txtPONo.Text.Trim(), htbCriteria, true);
				if (drwResult != null)
				{
					if ((txtPONo.Tag != null) && (txtPONo.Tag != DBNull.Value))
					{
						if (drwResult[PO_PurchaseOrderMasterTable.PURCHASEORDERMASTERID_FLD].ToString() != txtPONo.Tag.ToString())
						{
							txtPONo.Text = drwResult[PO_PurchaseOrderMasterTable.CODE_FLD].ToString();
							txtPONo.Tag = drwResult[PO_PurchaseOrderMasterTable.PURCHASEORDERMASTERID_FLD];
							CreatDataSet();
							gridApprove.DataSource = dstGridData.Tables[0];
							FormControlComponents.RestoreGridLayout(gridApprove, dtbGridLayOut);
						}
					}
					txtPONo.Text = drwResult[PO_PurchaseOrderMasterTable.CODE_FLD].ToString();
					txtPONo.Tag = drwResult[PO_PurchaseOrderMasterTable.PURCHASEORDERMASTERID_FLD];
					
				}
				else
				{
					txtPONo.Focus();
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
				// displays the error message.
				PCSMessageBox.Show(ErrorCode.OTHER_ERROR,MessageBoxIcon.Error);
				// log message.
				try
				{
					Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
				}
				catch
				{
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION,MessageBoxIcon.Error);
				}
			}
		}
		private void btnSearch_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnSearch_Click()";
			try 
			{
				if (Validate_Data())
				{
					BindDataToGrid();
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
				// displays the error message.
				PCSMessageBox.Show(ErrorCode.OTHER_ERROR,MessageBoxIcon.Error);
				// log message.
				try
				{
					Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
				}
				catch
				{
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION,MessageBoxIcon.Error);
				}
			}

		}
		private bool Validate_Data()
		{
			const string METHOD_NAME = THIS + ".Validate_Data()";
			try
			{
				if (FormControlComponents.CheckMandatory(cboCCN))
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID);
					cboCCN.Focus();
					return false;
				}
				if (FormControlComponents.CheckMandatory(cboStatus))
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID);
					cboStatus.Focus();
					return false;
				}
				if (FormControlComponents.CheckMandatory(dtmApprDate))
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID);
					dtmApprDate.Focus();
					return false;
				}
				if (FormControlComponents.CheckMandatory(txtApprover))
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID);
					txtApprover.Focus();
					txtApprover.Select();
					return false;
				}
				return true;
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
		/// ConfigGrid
		/// </summary>
		/// <param name="pblnLock"></param>
		/// <author>Trada</author>
		/// <date>Tuesday, Nov 29 2005</date>
		private void ConfigGrid(bool pblnLock)
		{
			const string METHOD_NAME = THIS + ".ConfigGrid()";
			try
			{
				gridApprove.Enabled = true;
				for (int i =0; i <gridApprove.Splits[0].DisplayColumns.Count; i++)
				{
					gridApprove.Splits[0].DisplayColumns[i].Locked = true;
				}
				gridApprove.Splits[0].DisplayColumns[CHECK_APPROVE].Locked = pblnLock;
				gridApprove.Splits[0].DisplayColumns[PO_PurchaseOrderDetailTable.ORDERQUANTITY_FLD].DataColumn.NumberFormat = Constants.DECIMAL_NUMBERFORMAT;
				gridApprove.Splits[0].DisplayColumns[PO_PurchaseOrderDetailTable.ORDERQUANTITY_FLD].Style.HorizontalAlignment = AlignHorzEnum.Far;
				gridApprove.Splits[0].DisplayColumns[AVAILABLE_QUANTITY].DataColumn.NumberFormat = Constants.DECIMAL_NUMBERFORMAT;
				gridApprove.Splits[0].DisplayColumns[AVAILABLE_QUANTITY].Style.HorizontalAlignment = AlignHorzEnum.Far;
				gridApprove.Splits[0].DisplayColumns[PO_PurchaseOrderDetailTable.TOTALAMOUNT_FLD].DataColumn.NumberFormat = Constants.DECIMAL_NUMBERFORMAT;
				gridApprove.Splits[0].DisplayColumns[PO_PurchaseOrderDetailTable.TOTALAMOUNT_FLD].Style.HorizontalAlignment = AlignHorzEnum.Far;
				gridApprove.Splits[0].DisplayColumns[OPEN_AMOUNT].DataColumn.NumberFormat = Constants.DECIMAL_NUMBERFORMAT;
				gridApprove.Splits[0].DisplayColumns[OPEN_AMOUNT].Style.HorizontalAlignment = AlignHorzEnum.Far;
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

		public void BindDataToGrid()
		{
			const string METHOD_NAME = THIS + ".BindDataToGrid()";
			chkSelectAll.Checked = false;
			try
			{
				POPurchaseOrderApprovalBO boPurchase = new POPurchaseOrderApprovalBO();
				if (cboStatus.Text == lblApproved.Text.Trim())
				{
					if (txtPONo.Text.Trim() != string.Empty)
					{
						dtbSource = boPurchase.ListPODetailByPOMasterID(int.Parse(txtPONo.Tag.ToString()), int.Parse(cboCCN.SelectedValue.ToString()), true).Tables[1];
					}
					else
						dtbSource = boPurchase.ListPODetailByPOMasterID(-1, int.Parse(cboCCN.SelectedValue.ToString()), true).Tables[1];
				}
				else
				{
					if (txtPONo.Text.Trim() != string.Empty)
					{
						dtbSource = boPurchase.ListPODetailByPOMasterID(int.Parse(txtPONo.Tag.ToString()), int.Parse(cboCCN.SelectedValue.ToString()), false).Tables[1];
					}
					else
						dtbSource = boPurchase.ListPODetailByPOMasterID(-1, int.Parse(cboCCN.SelectedValue.ToString()), false).Tables[1];
				}
				dtbSource.Columns.Add(CHECK_APPROVE, typeof(bool));
				dtbSource.Columns.Add(APPROVED_ID);
				foreach (DataRow drow in dtbSource.Rows)
				{
					drow[CHECK_APPROVE] = false;
					if (txtApprover.Text == string.Empty)
					{
						PCSMessageBox.Show(ErrorCode.MESSAGE_RGV_PURCHASE_APPROVER, MessageBoxIcon.Error);
						return;
					}
					else
						drow[APPROVED_ID] = txtApprover.Tag.ToString();;
				}
				gridApprove.DataSource = dtbSource;
				FormControlComponents.RestoreGridLayout(gridApprove, dtbGridLayOut);
				gridApprove.Splits[0].DisplayColumns[CHECK_APPROVE].DataColumn.Caption = cboStatus.Text.Trim() == lblApproved.Text.Trim() ? lblCancelApprove.Text.Trim().Substring(1) : strbtnApprove.Substring(1);
				ConfigGrid(false);
				btnShowDetail.Enabled = gridApprove.RowCount > 0;
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
		private void btnApprove_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnApprove_Click()";
			try 
			{
				//Validate data 
				if (FormControlComponents.CheckMandatory(cboCCN))
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID);
					cboCCN.Focus();
					return;
				}

				if (FormControlComponents.CheckMandatory(dtmApprDate))
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID);
					dtmApprDate.Focus();
					return;
				}

				//check the PostDate in the current period
//				if (!FormControlComponents.CheckDateInCurrentPeriod((DateTime)dtmApprDate.Value))
//				{
//					//MessageBox.Show("The Post Date you input is not in the current period");
//					PCSMessageBox.Show(ErrorCode.MESSAGE_RTG_ENTRYDATE, MessageBoxIcon.Warning);
//					dtmApprDate.Focus();
//					return;
//				}


				if (!ValidateData()) return;
				if (cboStatus.Text.Trim() == lblApproved.Text)
				{
					new POPurchaseOrderApprovalBO().UpdateAllAfterApprove(dtbSource, DateTime.Parse(dtmApprDate.Value.ToString()), 0);
				}
				else
					new POPurchaseOrderApprovalBO().UpdateAllAfterApprove(dtbSource, DateTime.Parse(dtmApprDate.Value.ToString()), int.Parse(txtApprover.Tag.ToString().Trim()));
				BindDataToGrid();
				PCSMessageBox.Show(ErrorCode.MESSAGE_AFTER_SAVE_DATA);
			}
			catch (PCSException ex)
			{
				// displays the error message.
				PCSMessageBox.Show(ex.mCode, MessageBoxIcon.Error);
				// log message.
				try
				{
					Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
				}
				catch
				{
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION,MessageBoxIcon.Error);
				}
			}
			catch (Exception ex) 
			{
				// displays the error message.
				PCSMessageBox.Show(ErrorCode.OTHER_ERROR,MessageBoxIcon.Error);
				// log message.
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
		public bool ValidateData()
		{
			try
			{
				string METHOD_NAME = THIS + ".ValidateData()";
				if (gridApprove.RowCount == 0)
				{
					throw new PCSException(ErrorCode.MESSAGE_PO_APPROVE_NO_DATA_IN_GRID, METHOD_NAME, null);
				}

				int intCount = 0;
				for (int i = 0; i < gridApprove.RowCount; i++)
				{
					if (gridApprove[i, CHECK_APPROVE].ToString() == TRUE)
					{
						intCount++;
						break;
					}
				}
				if (intCount == 0)
				{
					PCSMessageBox.Show(ErrorCode.MESSAGE_RGV_PURCHASE_APPROVER_SELECT_LINE, MessageBoxIcon.Warning);	
					return false;
				}
				
				for (int i =0; i < gridApprove.RowCount; i++)
				{
					if (gridApprove[i, CHECK_APPROVE].ToString() == TRUE)
					{
						if (DateTime.Parse(((DateTime) dtmApprDate.Value).ToShortDateString()) < (DateTime)gridApprove[i, PO_PurchaseOrderMasterTable.ORDERDATE_FLD]
							&& cboStatus.Text.Trim() == lblNotApproved.Text.Trim())
						{
							PCSMessageBox.Show(ErrorCode.MESSAGE_RGV_APPROVE_DATE_MUST_OLDER_THAN_ORDER_DATE, MessageBoxIcon.Warning);
							dtmApprDate.Focus();
							return false;
						}

						if (DateTime.Parse(((DateTime) dtmApprDate.Value).ToShortDateString()) < (DateTime)gridApprove[i, PO_PurchaseOrderMasterTable.ORDERDATE_FLD] 
							&& cboStatus.Text.Trim() == lblApproved.Text.Trim())
						{
							PCSMessageBox.Show(ErrorCode.MESSAGE_RGV_CANCEL_APPROVE_DATE_MUST_OLDER_THAN_ORDER_DATE, MessageBoxIcon.Warning);
							dtmApprDate.Focus();
							return false;
						}
					}
				}

				//get all POMaster which was selected to approve
				DataRow[] drowIDs = dtbSource.Select(CHECK_APPROVE + "=1");
				ArrayList arlIDs = new ArrayList();
				foreach (DataRow drowData in drowIDs)
				{
					bool okExisted = false;
					for (int i = 0; i <arlIDs.Count; i++)
					{
						if (int.Parse(arlIDs[i].ToString()) == (int) drowData[PO_PurchaseOrderMasterTable.PURCHASEORDERMASTERID_FLD])
						{
							okExisted = true;
							break;
						}
					}
					if (!okExisted)
					{
						arlIDs.Add((int) drowData[PO_PurchaseOrderMasterTable.PURCHASEORDERMASTERID_FLD]);
					}
				}
				
				POPurchaseOrderApprovalBO boPurchase = new POPurchaseOrderApprovalBO();
				for (int i =0; i <arlIDs.Count; i++)
				{
					if (!(boPurchase.CheckLevelApproval(int.Parse(txtApprover.Tag.ToString().Trim()), int.Parse(arlIDs[i].ToString()), int.Parse(cboCCN.SelectedValue.ToString()))))
					{
						PCSMessageBox.Show(ErrorCode.MESSAGE_NO_RIGHT_TO_APPROVE, MessageBoxIcon.Stop);
					}
				}
				return true;
			}
			catch (PCSException ex)
			{
				throw ex;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		private void CheckOrNochkCheckAll()
		{
			for (int i =0; i <gridApprove.RowCount; i++)
			{
				if (gridApprove[i, CHECK_APPROVE].ToString().Trim() != TRUE)
				{
					chkSelectAll.Checked = false;
					return;
				}
			}
			chkSelectAll.Checked = true;
		}
		private void chkSelectAll_CheckedChanged(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".chkSelectAll_CheckedChanged()";
			try 
			{
			
				if (blnStateOfCheck)
				{
			
					if (chkSelectAll.Checked)
					{
						foreach (DataRow drow in dtbSource.Rows)
						{
							if (drow.RowState != DataRowState.Deleted)
							{
								drow[CHECK_APPROVE] = true;
							}
						}
					}
					else
					{
						foreach (DataRow drow in dtbSource.Rows)
						{
							if (drow.RowState != DataRowState.Deleted)
							{
								drow[CHECK_APPROVE] = false;
							}
						}
					}
				}
			}
			catch (PCSException ex)
			{
				// displays the error message.
				PCSMessageBox.Show(ex.mCode, MessageBoxIcon.Error);
				// log message.
				try
				{
					Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
				}
				catch
				{
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION,MessageBoxIcon.Error);
				}
			}
			catch (Exception ex) 
			{
				// displays the error message.
				PCSMessageBox.Show(ErrorCode.OTHER_ERROR,MessageBoxIcon.Error);
				// log message.
				try
				{
					Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
				}
				catch
				{
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION,MessageBoxIcon.Error);
				}
			}

		}
		private void gridApprove_AfterColEdit(object sender, C1.Win.C1TrueDBGrid.ColEventArgs e)
		{
			if (e.Column.DataColumn.DataField == CHECK_APPROVE)
			{
				CheckOrNochkCheckAll();
			}
		}
		#region ChangeStateOfCheck
		private void chkSelectAll_Enter(object sender, System.EventArgs e)
		{	
            blnStateOfCheck = true;			
		}

		private void chkSelectAll_Leave(object sender, System.EventArgs e)
		{
			blnStateOfCheck = false;
		}
		#endregion

		private void OnEnterControl(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".OnEnterControl()";
			try
			{
				FormControlComponents.OnEnterControl(sender, e);
				
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
		private void OnLeaveControl(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".OnLeaveControl()";
			try
			{
				FormControlComponents.OnLeaveControl(sender, e);
			}
			catch (PCSException ex)
			{
				// displays the error message.
				PCSMessageBox.Show(ex.mCode, MessageBoxIcon.Error);
				// log message.
				try
				{
					Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
				}
				catch
				{
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION,MessageBoxIcon.Error);
				}
			}
			catch (Exception ex) 
			{
				// displays the error message.
				PCSMessageBox.Show(ErrorCode.OTHER_ERROR,MessageBoxIcon.Error);
				// log message.
				try
				{
					Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
				}
				catch
				{
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION,MessageBoxIcon.Error);
				}
			}
		}

		private void txtPONo_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtPONo_KeyDown()";
			if (e.KeyCode == Keys.F4)
            {
				btnPONo_Click(sender, e);	
			}
		}
		/// <summary>
		/// btnShowDetail_Click
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Monday, August 15 2005</date>
		private void btnShowDetail_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnShowDetail_Click()";
			try
			{
				if (gridApprove.RowCount > 0)
				{
					PurchaseOrder frmPurchaseOrder = new PurchaseOrder();
					frmPurchaseOrder.Show();
					frmPurchaseOrder.LoadMaster((int) gridApprove[gridApprove.Row, PO_PurchaseOrderMasterTable.PURCHASEORDERMASTERID_FLD]);
				}
			}
			catch (PCSException ex)
			{
				// displays the error message.
				PCSMessageBox.Show(ex.mCode, MessageBoxIcon.Error);
				// log message.
				try
				{
					Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
				}
				catch
				{
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION,MessageBoxIcon.Error);
				}
			}
			catch (Exception ex) 
			{
				// displays the error message.
				PCSMessageBox.Show(ErrorCode.OTHER_ERROR,MessageBoxIcon.Error);
				// log message.
				try
				{
					Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
				}
				catch
				{
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION,MessageBoxIcon.Error);
				}
			}
		}

		private void cboStatus_SelectedValueChanged(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = ".cboStatus_SelectedValueChanged()";
			if (cboStatus.Text.Trim() == lblApproved.Text.Trim())
			{
				btnApprove.Text  = lblCancelApprove.Text;
				lblApprDate.Text = lblCancelDate.Text;
				lblApprover.Text = lblCanceler.Text;
				gridApprove.Splits[0].DisplayColumns[CHECK_APPROVE].DataColumn.Caption = lblCancelApprove.Text.Trim().Substring(1);
			}
			if (cboStatus.Text.Trim() == lblNotApproved.Text.Trim())
			{
				btnApprove.Text  = strbtnApprove;
				lblApprDate.Text = strlblApprovalDate;
				lblApprover.Text = strlblApprover;
				gridApprove.Splits[0].DisplayColumns[CHECK_APPROVE].DataColumn.Caption = strbtnApprove.Substring(1);
			}
			try
			{
				if (dtbSource.Columns.Count != 0)
				{
					dtbSource.Rows.Clear();
					dtbSource.AcceptChanges();
					gridApprove.Refresh();
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

		private void txtPONo_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtPONo_Validating()";
			try 
			{
				if (!txtPONo.Modified || txtPONo.Text.Trim() == string.Empty) return;

				Hashtable htbCriteria = new Hashtable();
				DataRowView drwResult = null;
				//User has selected CCN
				if (cboCCN.SelectedIndex != -1 )
					htbCriteria.Add(PO_PurchaseOrderMasterTable.CCNID_FLD, cboCCN.SelectedValue);	
				else
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Warning);
					cboCCN.Focus();
					e.Cancel = true;
					return;
				}
				// edited: 12-04-2006 dungla: fix bug 3702 for NganNT, dot not display search form when found only one record
				if (cboStatus.SelectedIndex == -1)
				{
					string[] strParam = new string[2];
					strParam[0] = lblStatus.Text;
					strParam[1] = lblPONo.Text;
					PCSMessageBox.Show(ErrorCode.MESSAGE_SELECT_ONE_BEFORE_SELECT_ONE, MessageBoxIcon.Warning, strParam);
					e.Cancel = true;
					return;
				}
				if (cboStatus.Text.Trim() == lblNotApproved.Text.Trim())
					drwResult = FormControlComponents.OpenSearchForm(V_PO_NOT_APPROVE, PO_PurchaseOrderMasterTable.CODE_FLD, txtPONo.Text.Trim(), htbCriteria, false);
				else
					drwResult = FormControlComponents.OpenSearchForm(V_PO_APPROVE, PO_PurchaseOrderMasterTable.CODE_FLD, txtPONo.Text.Trim(), htbCriteria, false);
				if (drwResult != null)
				{
					if ((txtPONo.Tag != null) && (txtPONo.Tag != DBNull.Value))
					{
						if (drwResult[PO_PurchaseOrderMasterTable.PURCHASEORDERMASTERID_FLD].ToString() != txtPONo.Tag.ToString())
						{
							txtPONo.Text = drwResult[PO_PurchaseOrderMasterTable.CODE_FLD].ToString();
							txtPONo.Tag = drwResult[PO_PurchaseOrderMasterTable.PURCHASEORDERMASTERID_FLD];
							CreatDataSet();
							gridApprove.DataSource = dstGridData.Tables[0];
							FormControlComponents.RestoreGridLayout(gridApprove, dtbGridLayOut);
						}
					}
					txtPONo.Text = drwResult[PO_PurchaseOrderMasterTable.CODE_FLD].ToString();
					txtPONo.Tag = drwResult[PO_PurchaseOrderMasterTable.PURCHASEORDERMASTERID_FLD];
				}
				else
					e.Cancel = true;
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
	}
}

