using System;
using System.Data;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using C1.Win.C1TrueDBGrid;
using PCSComMaterials.Plan.BO;
using PCSComUtils.Common;
using PCSComUtils.Common.BO;
using PCSComUtils.MasterSetup.DS;
using PCSComUtils.PCSExc;
using PCSUtils.Log;
using PCSUtils.Utils;

namespace PCSMaterials.Mps
{
	/// <summary>
	/// Summary description for WorkDayCalendar.
	/// </summary>
	public class WorkDayCalendar : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label3;


		private C1.Win.C1List.C1Combo cboCCN;
		private System.Windows.Forms.Label lblCCN;
		private System.Windows.Forms.Button btnDelete;
		private System.Windows.Forms.Button btnSave;
		private System.Windows.Forms.Button btnAdd;
		private System.Windows.Forms.Button btnEdit;
		private System.Windows.Forms.Button btnHelp;
		private System.Windows.Forms.Button btnClose;
		private C1.Win.C1TrueDBGrid.C1TrueDBGrid dgrdData;
		private System.Windows.Forms.CheckBox chkSunday;
		private System.Windows.Forms.CheckBox chkMonday;
		private System.Windows.Forms.CheckBox chkTuesday;
		private System.Windows.Forms.CheckBox chkWednesday;
		private System.Windows.Forms.CheckBox chkSaturday;
		private System.Windows.Forms.CheckBox chkThursday;
		private System.Windows.Forms.CheckBox chkFriday;
		private System.Windows.Forms.ComboBox cboYear;
		private System.Windows.Forms.GroupBox grpWorkDay;
		///// <summary>
		///// Required designer variable.
		///// </summary>
		private System.ComponentModel.Container components = null;


		private EnumAction mFormMode = EnumAction.Default;
		private const string THIS = "PCSMaterials.Mps.WorkDayCalendar";
		private const string DAYOFWEEK = "Day";
		private const string DAY_FORMAT= "dd-MM";
		private DataSet dstData;
		private C1.Win.C1Input.C1DateEdit dtmDate;
		private WorkDayCalendarBO boWorkDayCalendar = new WorkDayCalendarBO();
		private MST_WorkingDayMasterVO voWorkingMaster = new MST_WorkingDayMasterVO();
		private DataTable dtbGridLayOut;
		private bool blnHasError;

		public WorkDayCalendar()
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(WorkDayCalendar));
			this.dgrdData = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
			this.grpWorkDay = new System.Windows.Forms.GroupBox();
			this.chkSunday = new System.Windows.Forms.CheckBox();
			this.chkMonday = new System.Windows.Forms.CheckBox();
			this.chkTuesday = new System.Windows.Forms.CheckBox();
			this.chkWednesday = new System.Windows.Forms.CheckBox();
			this.chkSaturday = new System.Windows.Forms.CheckBox();
			this.chkThursday = new System.Windows.Forms.CheckBox();
			this.chkFriday = new System.Windows.Forms.CheckBox();
			this.cboYear = new System.Windows.Forms.ComboBox();
			this.label3 = new System.Windows.Forms.Label();
			this.cboCCN = new C1.Win.C1List.C1Combo();
			this.lblCCN = new System.Windows.Forms.Label();
			this.btnDelete = new System.Windows.Forms.Button();
			this.btnSave = new System.Windows.Forms.Button();
			this.btnAdd = new System.Windows.Forms.Button();
			this.btnEdit = new System.Windows.Forms.Button();
			this.btnHelp = new System.Windows.Forms.Button();
			this.btnClose = new System.Windows.Forms.Button();
			this.dtmDate = new C1.Win.C1Input.C1DateEdit();
			((System.ComponentModel.ISupportInitialize)(this.dgrdData)).BeginInit();
			this.grpWorkDay.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.cboCCN)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dtmDate)).BeginInit();
			this.SuspendLayout();
			// 
			// dgrdData
			// 
			this.dgrdData.AccessibleDescription = "";
			this.dgrdData.AccessibleName = "";
			this.dgrdData.AllowAddNew = true;
			this.dgrdData.AllowDelete = true;
			this.dgrdData.CaptionHeight = 17;
			this.dgrdData.CollapseColor = System.Drawing.Color.Black;
			this.dgrdData.ExpandColor = System.Drawing.Color.Black;
			this.dgrdData.FlatStyle = C1.Win.C1TrueDBGrid.FlatModeEnum.System;
			this.dgrdData.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.dgrdData.GroupByCaption = "Drag a column header here to group by that column";
			this.dgrdData.Images.Add(((System.Drawing.Image)(resources.GetObject("resource"))));
			this.dgrdData.Location = new System.Drawing.Point(5, 96);
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
			this.dgrdData.Size = new System.Drawing.Size(514, 224);
			this.dgrdData.TabIndex = 5;
			this.dgrdData.Text = "c1TrueDBGrid1";
			this.dgrdData.AfterColUpdate += new C1.Win.C1TrueDBGrid.ColEventHandler(this.dgrdData_AfterColUpdate);
			this.dgrdData.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"Date\" DataF" +
				"ield=\"OffDay\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" " +
				"Caption=\"Comment\" DataField=\"Comment\"><ValueItems /><GroupInfo /></C1DataColumn>" +
				"<C1DataColumn Level=\"0\" Caption=\"Day\" DataField=\"Day\"><ValueItems /><GroupInfo /" +
				"></C1DataColumn></DataCols><Styles type=\"C1.Win.C1TrueDBGrid.Design.ContextWrapp" +
				"er\"><Data>HighlightRow{ForeColor:HighlightText;BackColor:Highlight;}Caption{Alig" +
				"nHorz:Center;}Normal{Font:Microsoft Sans Serif, 8.25pt;}Style25{}Style24{}Editor" +
				"{}Style18{}Style19{}Style14{}Style15{}Style16{AlignHorz:Near;ForeColor:Maroon;}S" +
				"tyle17{AlignHorz:Near;}Style10{AlignHorz:Near;}Style11{}OddRow{}Style13{}Style32" +
				"{}Style31{}Style29{AlignHorz:Near;}Style28{AlignHorz:Near;}Style27{}Style26{}Rec" +
				"ordSelector{AlignImage:Center;}Footer{}Style23{AlignHorz:Near;}Style22{AlignHorz" +
				":Near;}Style21{}Style20{}Group{AlignVert:Center;Border:None,,0, 0, 0, 0;BackColo" +
				"r:ControlDark;}Inactive{ForeColor:InactiveCaptionText;BackColor:InactiveCaption;" +
				"}EvenRow{BackColor:Aqua;}Heading{Wrap:True;BackColor:Control;Border:Raised,,1, 1" +
				", 1, 1;ForeColor:ControlText;AlignVert:Center;}Style5{}FilterBar{}Selected{ForeC" +
				"olor:HighlightText;BackColor:Highlight;}Style9{}Style8{}Style1{}Style12{}Style4{" +
				"}Style7{}Style6{}Style33{}Style30{}Style3{}Style2{}</Data></Styles><Splits><C1.W" +
				"in.C1TrueDBGrid.MergeView Name=\"\" CaptionHeight=\"17\" ColumnCaptionHeight=\"17\" Co" +
				"lumnFooterHeight=\"17\" MarqueeStyle=\"DottedCellBorder\" RecordSelectorWidth=\"16\" D" +
				"efRecSelWidth=\"16\" VerticalScrollGroup=\"1\" HorizontalScrollGroup=\"1\"><ClientRect" +
				">0, 0, 510, 220</ClientRect><BorderSide>0</BorderSide><CaptionStyle parent=\"Styl" +
				"e2\" me=\"Style10\" /><EditorStyle parent=\"Editor\" me=\"Style5\" /><EvenRowStyle pare" +
				"nt=\"EvenRow\" me=\"Style8\" /><FilterBarStyle parent=\"FilterBar\" me=\"Style13\" /><Fo" +
				"oterStyle parent=\"Footer\" me=\"Style3\" /><GroupStyle parent=\"Group\" me=\"Style12\" " +
				"/><HeadingStyle parent=\"Heading\" me=\"Style2\" /><HighLightRowStyle parent=\"Highli" +
				"ghtRow\" me=\"Style7\" /><InactiveStyle parent=\"Inactive\" me=\"Style4\" /><OddRowStyl" +
				"e parent=\"OddRow\" me=\"Style9\" /><RecordSelectorStyle parent=\"RecordSelector\" me=" +
				"\"Style11\" /><SelectedStyle parent=\"Selected\" me=\"Style6\" /><Style parent=\"Normal" +
				"\" me=\"Style1\" /><internalCols><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=" +
				"\"Style16\" /><Style parent=\"Style1\" me=\"Style17\" /><FooterStyle parent=\"Style3\" m" +
				"e=\"Style18\" /><EditorStyle parent=\"Style5\" me=\"Style19\" /><GroupHeaderStyle pare" +
				"nt=\"Style1\" me=\"Style21\" /><GroupFooterStyle parent=\"Style1\" me=\"Style20\" /><Vis" +
				"ible>True</Visible><ColumnDivider>DarkGray,Single</ColumnDivider><Width>93</Widt" +
				"h><Height>15</Height><DCIdx>0</DCIdx></C1DisplayColumn><C1DisplayColumn><Heading" +
				"Style parent=\"Style2\" me=\"Style22\" /><Style parent=\"Style1\" me=\"Style23\" /><Foot" +
				"erStyle parent=\"Style3\" me=\"Style24\" /><EditorStyle parent=\"Style5\" me=\"Style25\"" +
				" /><GroupHeaderStyle parent=\"Style1\" me=\"Style27\" /><GroupFooterStyle parent=\"St" +
				"yle1\" me=\"Style26\" /><Visible>True</Visible><ColumnDivider>DarkGray,Single</Colu" +
				"mnDivider><Width>73</Width><Height>15</Height><DCIdx>2</DCIdx></C1DisplayColumn>" +
				"<C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style28\" /><Style parent=\"Sty" +
				"le1\" me=\"Style29\" /><FooterStyle parent=\"Style3\" me=\"Style30\" /><EditorStyle par" +
				"ent=\"Style5\" me=\"Style31\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style33\" /><Gr" +
				"oupFooterStyle parent=\"Style1\" me=\"Style32\" /><Visible>True</Visible><ColumnDivi" +
				"der>DarkGray,Single</ColumnDivider><Width>289</Width><Height>15</Height><DCIdx>1" +
				"</DCIdx></C1DisplayColumn></internalCols></C1.Win.C1TrueDBGrid.MergeView></Split" +
				"s><NamedStyles><Style parent=\"\" me=\"Normal\" /><Style parent=\"Normal\" me=\"Heading" +
				"\" /><Style parent=\"Heading\" me=\"Footer\" /><Style parent=\"Heading\" me=\"Caption\" /" +
				"><Style parent=\"Heading\" me=\"Inactive\" /><Style parent=\"Normal\" me=\"Selected\" />" +
				"<Style parent=\"Normal\" me=\"Editor\" /><Style parent=\"Normal\" me=\"HighlightRow\" />" +
				"<Style parent=\"Normal\" me=\"EvenRow\" /><Style parent=\"Normal\" me=\"OddRow\" /><Styl" +
				"e parent=\"Heading\" me=\"RecordSelector\" /><Style parent=\"Normal\" me=\"FilterBar\" /" +
				"><Style parent=\"Caption\" me=\"Group\" /></NamedStyles><vertSplits>1</vertSplits><h" +
				"orzSplits>1</horzSplits><Layout>Modified</Layout><DefaultRecSelWidth>16</Default" +
				"RecSelWidth><ClientArea>0, 0, 510, 220</ClientArea><PrintPageHeaderStyle parent=" +
				"\"\" me=\"Style14\" /><PrintPageFooterStyle parent=\"\" me=\"Style15\" /></Blob>";
			// 
			// grpWorkDay
			// 
			this.grpWorkDay.AccessibleDescription = "";
			this.grpWorkDay.AccessibleName = "";
			this.grpWorkDay.Controls.Add(this.chkSunday);
			this.grpWorkDay.Controls.Add(this.chkMonday);
			this.grpWorkDay.Controls.Add(this.chkTuesday);
			this.grpWorkDay.Controls.Add(this.chkWednesday);
			this.grpWorkDay.Controls.Add(this.chkSaturday);
			this.grpWorkDay.Controls.Add(this.chkThursday);
			this.grpWorkDay.Controls.Add(this.chkFriday);
			this.grpWorkDay.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.grpWorkDay.Location = new System.Drawing.Point(5, 32);
			this.grpWorkDay.Name = "grpWorkDay";
			this.grpWorkDay.Size = new System.Drawing.Size(514, 58);
			this.grpWorkDay.TabIndex = 4;
			this.grpWorkDay.TabStop = false;
			this.grpWorkDay.Text = "Valid Work Days";
			// 
			// chkSunday
			// 
			this.chkSunday.AccessibleDescription = "";
			this.chkSunday.AccessibleName = "";
			this.chkSunday.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.chkSunday.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.chkSunday.Location = new System.Drawing.Point(445, 22);
			this.chkSunday.Name = "chkSunday";
			this.chkSunday.Size = new System.Drawing.Size(62, 22);
			this.chkSunday.TabIndex = 6;
			this.chkSunday.Text = "Sunday";
			// 
			// chkMonday
			// 
			this.chkMonday.AccessibleDescription = "";
			this.chkMonday.AccessibleName = "";
			this.chkMonday.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.chkMonday.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.chkMonday.Location = new System.Drawing.Point(14, 22);
			this.chkMonday.Name = "chkMonday";
			this.chkMonday.Size = new System.Drawing.Size(63, 22);
			this.chkMonday.TabIndex = 0;
			this.chkMonday.Text = "Monday";
			// 
			// chkTuesday
			// 
			this.chkTuesday.AccessibleDescription = "";
			this.chkTuesday.AccessibleName = "";
			this.chkTuesday.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.chkTuesday.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.chkTuesday.Location = new System.Drawing.Point(81, 22);
			this.chkTuesday.Name = "chkTuesday";
			this.chkTuesday.Size = new System.Drawing.Size(67, 22);
			this.chkTuesday.TabIndex = 1;
			this.chkTuesday.Text = "Tuesday";
			// 
			// chkWednesday
			// 
			this.chkWednesday.AccessibleDescription = "";
			this.chkWednesday.AccessibleName = "";
			this.chkWednesday.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.chkWednesday.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.chkWednesday.Location = new System.Drawing.Point(152, 22);
			this.chkWednesday.Name = "chkWednesday";
			this.chkWednesday.Size = new System.Drawing.Size(83, 22);
			this.chkWednesday.TabIndex = 2;
			this.chkWednesday.Text = "Wednesday";
			// 
			// chkSaturday
			// 
			this.chkSaturday.AccessibleDescription = "";
			this.chkSaturday.AccessibleName = "";
			this.chkSaturday.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.chkSaturday.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.chkSaturday.Location = new System.Drawing.Point(372, 22);
			this.chkSaturday.Name = "chkSaturday";
			this.chkSaturday.Size = new System.Drawing.Size(69, 22);
			this.chkSaturday.TabIndex = 5;
			this.chkSaturday.Text = "Saturday";
			// 
			// chkThursday
			// 
			this.chkThursday.AccessibleDescription = "";
			this.chkThursday.AccessibleName = "";
			this.chkThursday.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.chkThursday.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.chkThursday.Location = new System.Drawing.Point(239, 22);
			this.chkThursday.Name = "chkThursday";
			this.chkThursday.Size = new System.Drawing.Size(70, 22);
			this.chkThursday.TabIndex = 3;
			this.chkThursday.Text = "Thursday";
			// 
			// chkFriday
			// 
			this.chkFriday.AccessibleDescription = "";
			this.chkFriday.AccessibleName = "";
			this.chkFriday.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.chkFriday.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.chkFriday.Location = new System.Drawing.Point(313, 22);
			this.chkFriday.Name = "chkFriday";
			this.chkFriday.Size = new System.Drawing.Size(55, 22);
			this.chkFriday.TabIndex = 4;
			this.chkFriday.Text = "Friday";
			// 
			// cboYear
			// 
			this.cboYear.AccessibleDescription = "";
			this.cboYear.AccessibleName = "";
			this.cboYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboYear.ItemHeight = 13;
			this.cboYear.Items.AddRange(new object[] {
														 "2001",
														 "2002",
														 "2003",
														 "2004",
														 "2005",
														 "2006",
														 "2007",
														 "2008",
														 "2009",
														 "2010",
														 "2011",
														 "2012",
														 "2013",
														 "2014",
														 "2015",
														 "2016",
														 "2017",
														 "2018",
														 "2019",
														 "2020",
														 "2021",
														 "2022",
														 "2023",
														 "2024",
														 "2025",
														 "2026",
														 "2027",
														 "2028",
														 "2029",
														 "2030"});
			this.cboYear.Location = new System.Drawing.Point(36, 8);
			this.cboYear.Name = "cboYear";
			this.cboYear.Size = new System.Drawing.Size(68, 21);
			this.cboYear.TabIndex = 3;
			this.cboYear.SelectedIndexChanged += new System.EventHandler(this.cboYear_SelectedIndexChanged);
			// 
			// label3
			// 
			this.label3.AccessibleDescription = "";
			this.label3.AccessibleName = "";
			this.label3.ForeColor = System.Drawing.Color.Maroon;
			this.label3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.label3.Location = new System.Drawing.Point(6, 8);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(31, 21);
			this.label3.TabIndex = 2;
			this.label3.Text = "Year";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// cboCCN
			// 
			this.cboCCN.AccessibleDescription = "";
			this.cboCCN.AccessibleName = "";
			this.cboCCN.AddItemSeparator = ';';
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
			this.cboCCN.Location = new System.Drawing.Point(439, 8);
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
			this.lblCCN.AccessibleDescription = "";
			this.lblCCN.AccessibleName = "";
			this.lblCCN.ForeColor = System.Drawing.Color.Maroon;
			this.lblCCN.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lblCCN.Location = new System.Drawing.Point(408, 8);
			this.lblCCN.Name = "lblCCN";
			this.lblCCN.Size = new System.Drawing.Size(30, 19);
			this.lblCCN.TabIndex = 0;
			this.lblCCN.Text = "CCN";
			this.lblCCN.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// btnDelete
			// 
			this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnDelete.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnDelete.Location = new System.Drawing.Point(188, 326);
			this.btnDelete.Name = "btnDelete";
			this.btnDelete.Size = new System.Drawing.Size(60, 23);
			this.btnDelete.TabIndex = 10;
			this.btnDelete.Text = "&Delete";
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
			// 
			// btnSave
			// 
			this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnSave.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnSave.Location = new System.Drawing.Point(66, 326);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(60, 23);
			this.btnSave.TabIndex = 8;
			this.btnSave.Text = "&Save";
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			// 
			// btnAdd
			// 
			this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnAdd.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnAdd.Location = new System.Drawing.Point(5, 326);
			this.btnAdd.Name = "btnAdd";
			this.btnAdd.Size = new System.Drawing.Size(60, 23);
			this.btnAdd.TabIndex = 7;
			this.btnAdd.Text = "&Add";
			this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
			// 
			// btnEdit
			// 
			this.btnEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnEdit.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnEdit.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnEdit.Location = new System.Drawing.Point(127, 326);
			this.btnEdit.Name = "btnEdit";
			this.btnEdit.Size = new System.Drawing.Size(60, 23);
			this.btnEdit.TabIndex = 9;
			this.btnEdit.Text = "&Edit";
			this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
			// 
			// btnHelp
			// 
			this.btnHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnHelp.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnHelp.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnHelp.Location = new System.Drawing.Point(398, 326);
			this.btnHelp.Name = "btnHelp";
			this.btnHelp.Size = new System.Drawing.Size(60, 23);
			this.btnHelp.TabIndex = 11;
			this.btnHelp.Text = "&Help";
			// 
			// btnClose
			// 
			this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnClose.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnClose.Location = new System.Drawing.Point(459, 326);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(60, 23);
			this.btnClose.TabIndex = 12;
			this.btnClose.Text = "&Close";
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			// 
			// dtmDate
			// 
			this.dtmDate.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
			this.dtmDate.Location = new System.Drawing.Point(166, 202);
			this.dtmDate.Name = "dtmDate";
			this.dtmDate.Size = new System.Drawing.Size(112, 20);
			this.dtmDate.TabIndex = 6;
			this.dtmDate.Tag = null;
			this.dtmDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.dtmDate.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.None;
			this.dtmDate.Enter += new System.EventHandler(this.dtmDate_Enter);
			this.dtmDate.Leave += new System.EventHandler(this.dtmDate_Leave);
			// 
			// WorkDayCalendar
			// 
			this.AccessibleDescription = "";
			this.AccessibleName = "";
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.btnClose;
			this.ClientSize = new System.Drawing.Size(524, 353);
			this.Controls.Add(this.btnHelp);
			this.Controls.Add(this.btnClose);
			this.Controls.Add(this.btnDelete);
			this.Controls.Add(this.btnSave);
			this.Controls.Add(this.btnAdd);
			this.Controls.Add(this.btnEdit);
			this.Controls.Add(this.cboCCN);
			this.Controls.Add(this.lblCCN);
			this.Controls.Add(this.grpWorkDay);
			this.Controls.Add(this.cboYear);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.dgrdData);
			this.Controls.Add(this.dtmDate);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.KeyPreview = true;
			this.MaximizeBox = false;
			this.Name = "WorkDayCalendar";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Setup Work Day Calendar";
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.WorkDayCalendar_KeyDown);
			this.Closing += new System.ComponentModel.CancelEventHandler(this.WorkDayCalendar_Closing);
			this.Load += new System.EventHandler(this.WorkDayCalendar_Load);
			((System.ComponentModel.ISupportInitialize)(this.dgrdData)).EndInit();
			this.grpWorkDay.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.cboCCN)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dtmDate)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// Form Load event:
		///		Clear form
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void WorkDayCalendar_Load(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".WorkOrder_Load()";
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
				dtmDate.CustomFormat = DAY_FORMAT;
				ResetForm();

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
				SwitchFormMode();
				btnAdd.Enabled = true;
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


		private void EnableControl(object pobjControl, bool pblnEnable)
		{
			const string METHOD_NAME = THIS + ".EnaleControl()";
			try
			{
				Control ctrControl = (Control) pobjControl;
				if (ctrControl.GetType() != typeof(Label))
					ctrControl.Enabled = pblnEnable;
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
		private void EnableControlParent(object pobjParentControl, bool pblnEnable)
		{
			const string METHOD_NAME = THIS + ".EnableControlParent()";
			try
			{
				foreach (Control ctrChild in ((Control) pobjParentControl).Controls)
				{
					if (ctrChild is System.Windows.Forms.GroupBox)
					{
						foreach (Control ctrInGrougp in ctrChild.Controls)
						{
							EnableControl(ctrInGrougp, pblnEnable);
						}
					}
					else
					{
						EnableControl(ctrChild, pblnEnable);
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
		/// create dataset which's template for Datagrid
		/// </summary>
		private void CreateDataSet()
		{
			const string METHOD_NAME = THIS + ".CreateDataSet()";
			try
			{
				dstData = new DataSet();
				dstData.Tables.Add(MST_WorkingDayDetailTable.TABLE_NAME);
				dstData.Tables[MST_WorkingDayDetailTable.TABLE_NAME].Columns.Add(MST_WorkingDayDetailTable.WORKINGDAYMASTERID_FLD);
				dstData.Tables[MST_WorkingDayDetailTable.TABLE_NAME].Columns.Add(MST_WorkingDayDetailTable.WORKINGDAYDETAILID_FLD);
				dstData.Tables[MST_WorkingDayDetailTable.TABLE_NAME].Columns.Add(MST_WorkingDayDetailTable.OFFDAY_FLD, typeof(DateTime));
				dstData.Tables[MST_WorkingDayDetailTable.TABLE_NAME].Columns.Add(DAYOFWEEK);
				dstData.Tables[MST_WorkingDayDetailTable.TABLE_NAME].Columns.Add(MST_WorkingDayDetailTable.COMMENT_FLD);
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
		/// set format for columns on the grid
		/// </summary>
		/// <param name="pblnLock"></param>
		public void ConfigGrid(bool pblnLock)
		{
			const string METHOD_NAME = THIS + ".ConfigGrid()";
			try
			{
				dgrdData.Splits[0].DisplayColumns[MST_WorkingDayDetailTable.OFFDAY_FLD].DataColumn.NumberFormat = DAY_FORMAT;
				for (int i =0; i < dgrdData.Splits[0].DisplayColumns.Count; i++)
				{
					dgrdData.Splits[0].DisplayColumns[i].Locked = pblnLock;
				}
				dgrdData.Splits[0].DisplayColumns[DAYOFWEEK].Locked = true;
				dgrdData.Splits[0].DisplayColumns[MST_WorkingDayDetailTable.OFFDAY_FLD].DataColumn.Editor = dtmDate;
				dgrdData.Splits[0].DisplayColumns[MST_WorkingDayDetailTable.OFFDAY_FLD].Style.HorizontalAlignment = AlignHorzEnum.Center;
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
		/// Switch form mode and set state for controls
		/// </summary>
		private void SwitchFormMode()
		{
			const string METHOD_NAME = THIS + ".SwitchFormMode()";
			try
			{
				switch (mFormMode)
				{
					case EnumAction.Default:
						EnableControlParent(this, false);
						btnAdd.Enabled = true;
						cboCCN.Enabled = true;
						dgrdData.Enabled = true;
						ConfigGrid(true);
						dgrdData.AllowDelete = false;
						dgrdData.AllowAddNew = false;
						cboYear.Enabled= true;
						break;
					case EnumAction.Add:
						EnableControlParent(this, true);
						btnAdd.Enabled = false;
						btnEdit.Enabled = false;
						btnDelete.Enabled = false;
						cboCCN.Enabled = true;
						dgrdData.Enabled = true;
						cboYear.Enabled= true;
						dgrdData.AllowDelete = true;
						dgrdData.AllowAddNew = true;
						break;
					case EnumAction.Edit:
						EnableControlParent(this, true);
						btnEdit.Enabled = false;
						btnDelete.Enabled = false;
						dgrdData.Enabled = true;
						cboCCN.Enabled = false;
						cboYear.Enabled= false;
						btnAdd.Enabled = false;
						dgrdData.AllowDelete = true;
						dgrdData.AllowAddNew = true;
						ConfigGrid(false);
						break;
				}
				btnHelp.Enabled = true;
				btnClose.Enabled = true;
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
		/// Clear controls's data
		/// </summary>
		private void ResetForm()
		{
			const string METHOD_NAME = THIS + ".ResetForm()";
			try
			{
				cboYear.SelectedIndex = -1;
				chkMonday.Checked = false;
				chkTuesday.Checked = false;
				chkWednesday.Checked = false;
				chkThursday.Checked = false;
				chkFriday.Checked = false;
				chkSaturday.Checked = false;
				chkSunday.Checked = false;

				CreateDataSet();
				dgrdData.DataSource = dstData.Tables[0];
				FormControlComponents.RestoreGridLayout(dgrdData, dtbGridLayOut);
				ConfigGrid(true);
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
		/// Setup some default days 
		/// </summary>
		private void CreateSomeDayNotWork()
		{
			const string METHOD_NAME = THIS + ".CreateSomeDayNotWork()";
			try
			{
				DataRow drowDay = dstData.Tables[0].NewRow();
				DateTime dtmPublicDay = new DateTime(int.Parse(cboYear.SelectedItem.ToString()), 5, 1);
				string strComment = "May Day";
				drowDay[MST_WorkingDayDetailTable.OFFDAY_FLD] = dtmPublicDay;
				drowDay[DAYOFWEEK] = dtmPublicDay.DayOfWeek;
				drowDay[MST_WorkingDayDetailTable.COMMENT_FLD]= strComment;

				dstData.Tables[0].Rows.Add(drowDay);
				
				drowDay = dstData.Tables[0].NewRow();
				dtmPublicDay = new DateTime(int.Parse(cboYear.SelectedItem.ToString()), 9, 2);
				strComment = "Independent Day";
				drowDay[MST_WorkingDayDetailTable.OFFDAY_FLD] = dtmPublicDay;
				drowDay[DAYOFWEEK] = dtmPublicDay.DayOfWeek;
				drowDay[MST_WorkingDayDetailTable.COMMENT_FLD]= strComment;

				dstData.Tables[0].Rows.Add(drowDay);
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
		/// Add event:
		///		- Clear form
		///		- Set default value for controls
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnAdd_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnAdd_Click()";
			try
			{
				ResetForm();
				mFormMode = EnumAction.Add;
				SwitchFormMode();
				cboYear.SelectedItem = (new UtilsBO().GetDBDate().Year + 1).ToString();
				
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
		/// Validate data
		///		- Check mandatory fields
		///		- Check some basic business rule
		/// </summary>
		/// <returns></returns>
		private bool ValidateData()
		{
			const string METHOD_NAME = THIS + ".ValidateData()";
			try
			{
				if (FormControlComponents.CheckMandatory(cboCCN))
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID);
					cboCCN.Focus();
					cboCCN.Select();
					return false;
				}
				
				//check the duplicate Date on the grid
				for (int i =0; i < dgrdData.RowCount; i++)
				{
					if (dgrdData[i, MST_WorkingDayDetailTable.COMMENT_FLD].ToString().Trim() != string.Empty
						&& dgrdData[i, MST_WorkingDayDetailTable.OFFDAY_FLD].ToString().Trim() == string.Empty)
					{
						PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID);
						dgrdData.Row = i;
						dgrdData.Col = dgrdData.Columns.IndexOf(dgrdData.Columns[MST_WorkingDayDetailTable.OFFDAY_FLD]);
						dgrdData.Focus();
						return false;
					}
					for (int j =i+1; j < dgrdData.RowCount; j++)
					{
						if (dgrdData[i, MST_WorkingDayDetailTable.OFFDAY_FLD].ToString().Trim() ==
							dgrdData[j, MST_WorkingDayDetailTable.OFFDAY_FLD].ToString().Trim())
						{
							PCSMessageBox.Show(ErrorCode.MESSAGE_MPS_WD_DUPLICATE_DAY, MessageBoxIcon.Error);
							dgrdData.Col = dgrdData.Columns.IndexOf(dgrdData.Columns[MST_WorkingDayDetailTable.OFFDAY_FLD]);
							dgrdData.Row = j;
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
		
		/// <summary>
		/// Fill data to Object VO
		/// </summary>
		private void GetDataForVOObject()
		{
			const string METHOD_NAME = THIS + ".GetDataForVOObject()";
			try
			{
				voWorkingMaster.Mon = chkMonday.Checked;
				voWorkingMaster.Tue = chkTuesday.Checked;
				voWorkingMaster.Wed = chkWednesday.Checked;
				voWorkingMaster.Thu = chkThursday.Checked;
				voWorkingMaster.Fri = chkFriday.Checked;
				voWorkingMaster.Sat = chkSaturday.Checked;
				voWorkingMaster.Sun = chkSunday.Checked;
				voWorkingMaster.CCNID = int.Parse(cboCCN.SelectedValue.ToString());
				voWorkingMaster.Year = int.Parse(cboYear.SelectedItem.ToString());
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
		/// Save event:
		///		- Validate Data
		///		- Call the method of BO class to update Data
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnSave_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnSave_Click()";
			try
			{
				blnHasError = true;
				if (ValidateData())
				{
					GetDataForVOObject();
					if (mFormMode == EnumAction.Add)
					{
						voWorkingMaster.WorkingDayMasterID = boWorkDayCalendar.AddWDCalendar(voWorkingMaster, dstData);
					}
					else
					{
						boWorkDayCalendar.UpdateWDCalendar(voWorkingMaster, dstData);
					}
					PCSMessageBox.Show(ErrorCode.MESSAGE_AFTER_SAVE_DATA);
					blnHasError = false;

					//Reload grid
					dstData = boWorkDayCalendar.GetDetailByMasterID(voWorkingMaster.WorkingDayMasterID);
					dgrdData.DataSource = dstData.Tables[0];
					FormControlComponents.RestoreGridLayout(dgrdData, dtbGridLayOut);
					ConfigGrid(true);
					
					mFormMode = EnumAction.Default;
					SwitchFormMode();
//					ExtendedProButton extend = (ExtendedProButton) btnDelete.Tag;
//					if (!extend.IsDisable)
//					{
					btnDelete.Enabled = true;
//					}
//					extend = (ExtendedProButton) btnEdit.Tag;
//					if (!extend.IsDisable)
//					{
					btnEdit.Enabled = true;
//					}
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
			this.Close();
		}
		
		/// <summary>
		/// Delete event:
		///		- Delete data
		///		- Clear form
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnDelete_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnDelete_Click()";
			try
			{
				//allow delete
				if (PCSMessageBox.Show(ErrorCode.MESSAGE_DELETE_RECORD, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
				{
					boWorkDayCalendar.Delete(voWorkingMaster);
					mFormMode = EnumAction.Default;
                    ResetForm();
					SwitchFormMode();
					PCSMessageBox.Show(ErrorCode.MESSAGE_AFTER_SAVE_DATA);
					cboYear.Focus();
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
		/// check existing of Working Day for a year
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void cboYear_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".cboYear_SelectedIndexChanged()";
			try
			{
				if (cboYear.Text.Trim() != string.Empty)
				{
					voWorkingMaster = (MST_WorkingDayMasterVO) boWorkDayCalendar.GetWDCalendarMaster(int.Parse(cboYear.SelectedItem.ToString()), int.Parse(cboCCN.SelectedValue.ToString()));

					//if exist
					if (voWorkingMaster.WorkingDayMasterID != 0)
					{
						//disable control
						mFormMode = EnumAction.Default;
						cboCCN.Enabled = false;
						dgrdData.AllowDelete = false;
						dgrdData.AllowAddNew = false;
						ConfigGrid(true);
						//load data and change formmode
//						ExtendedProButton extend = (ExtendedProButton) btnDelete.Tag;
//						if (!extend.IsDisable)
//						{
						btnDelete.Enabled = true;
//						}
//						extend = (ExtendedProButton) btnEdit.Tag;
//						if (!extend.IsDisable)
//						{
						btnEdit.Enabled = true;
//						}
//						extend = (ExtendedProButton) btnAdd.Tag;
//						if (!extend.IsDisable)
//						{
						btnAdd.Enabled = true;
//						}

						btnSave.Enabled = false;
						chkMonday.Checked = voWorkingMaster.Mon;
						chkTuesday.Checked = voWorkingMaster.Tue;
						chkWednesday.Checked = voWorkingMaster.Wed;
						chkThursday.Checked = voWorkingMaster.Thu;
						chkFriday.Checked = voWorkingMaster.Fri;
						chkSaturday.Checked = voWorkingMaster.Sat;
						chkSunday.Checked = voWorkingMaster.Sun;

						EnableControlParent(grpWorkDay, false);

						//get detail
                        dstData = boWorkDayCalendar.GetDetailByMasterID(voWorkingMaster.WorkingDayMasterID);
						dgrdData.DataSource = dstData.Tables[0];
						FormControlComponents.RestoreGridLayout(dgrdData, dtbGridLayOut);
						ConfigGrid(true);
					}
					else
					{
						mFormMode = EnumAction.Add;
						SwitchFormMode();
						CreateDataSet();
						CreateSomeDayNotWork();
						dgrdData.DataSource = dstData.Tables[0];
						FormControlComponents.RestoreGridLayout(dgrdData, dtbGridLayOut);
						ConfigGrid(false);
						chkMonday.Checked = true;
						chkTuesday.Checked = true;
						chkWednesday.Checked = true;
						chkThursday.Checked = true;
						chkFriday.Checked = true;
						chkSaturday.Checked = true;
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
		/// Edit event:
		///		- Change formode
		///		- Enable & Disable controls 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnEdit_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnEdit_Click()";
			try
			{
				mFormMode = EnumAction.Edit;
				SwitchFormMode();
				EnableControlParent(grpWorkDay, true);
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
		/// Keydown event:
		///		-F12 : add new row
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void WorkDayCalendar_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			const string METHOD_NAME = THIS + ".WorkDayCalendar_KeyDown()";
			try
			{
				switch (e.KeyCode)
				{
					case Keys.F12:
						dgrdData.Col = dgrdData.Columns.IndexOf(dgrdData.Columns[MST_WorkingDayDetailTable.OFFDAY_FLD]);
						dgrdData.Row = dgrdData.RowCount;
						dgrdData.Focus();
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

		/// <summary>
		/// after user input Date for Date columns
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void dgrdData_AfterColUpdate(object sender, C1.Win.C1TrueDBGrid.ColEventArgs e)
		{
			const string METHOD_NAME = THIS + ".dgrdData_AfterColUpdate()";
			try
			{
				if (e.Column.DataColumn.DataField == MST_WorkingDayDetailTable.OFFDAY_FLD)
				{
					if (e.Column.DataColumn.Value.ToString() == string.Empty)
					{
						e.Column.DataColumn.Value = DBNull.Value;
						dgrdData[dgrdData.Row, DAYOFWEEK] = string.Empty;
					}
					else
					{
						dgrdData[dgrdData.Row, DAYOFWEEK] = DateTime.Parse(e.Column.DataColumn.Value.ToString()).DayOfWeek;
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

		private void WorkDayCalendar_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".WorkOrder_Closing()";
			try
			{
				if (mFormMode != EnumAction.Default)
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

		private void dtmDate_Enter(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".dtmDate_Enter()";
			try
			{
				if (dgrdData[dgrdData.Row, dgrdData.Col].ToString() == string.Empty)
				{
					dtmDate.Value = new DateTime(int.Parse(cboYear.SelectedItem.ToString()), DateTime.Now.Month, DateTime.Now.Day);
				}
				else
				{
					dtmDate.Value = DateTime.Parse(dgrdData[dgrdData.Row, dgrdData.Col].ToString());
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

		private void dtmDate_Leave(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".dtmDate_Leave()";
			try
			{
				if (dtmDate.Text.ToString() == string.Empty)
				{
					dtmDate.Value = DBNull.Value;
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
	}
}

