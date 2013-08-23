using System;
using System.Data;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using C1.Win.C1Input;
using C1.Win.C1TrueDBGrid;
using PCSComUtils.Common;
using PCSComProduction.DCP.BO;
using PCSComUtils.PCSExc;
using PCSComUtils.Common.BO;
using PCSUtils.Log;
using PCSUtils.Utils;

namespace PCSProduction.DCP
{
	/// <summary>
	/// Summary description for DCPAdjustment.
	/// </summary>
	public class DCPAdjustment : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button btnClose;
		private System.Windows.Forms.Button btnHelp;
		private C1.Win.C1Input.C1NumericEdit numCrewSize;
		private System.Windows.Forms.CheckBox chkWorkingday;
		private System.Windows.Forms.Label lblMachine;
		private System.Windows.Forms.GroupBox grpAdjust;
		private C1.Win.C1Input.C1NumericEdit numFactor;
		private System.Windows.Forms.Label lblFactor;
		private System.Windows.Forms.Label lblCrewsize;
		private C1.Win.C1Input.C1NumericEdit numMachine;
		private System.Windows.Forms.Label lblShifts;
		private System.Windows.Forms.ComboBox cboType;
		private System.Windows.Forms.Label lblType;
		private C1.Win.C1Input.C1NumericEdit numCapacity;
		private System.Windows.Forms.Label lblCapacity;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.MonthCalendar monthCalendar;
		private System.Windows.Forms.TextBox txtWorkCenter;
		private System.Windows.Forms.Label lblWorkCenter;
		private System.Windows.Forms.GroupBox grpCalendar;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		public DataTable dtbWCCapacity = new DataTable();
		public DataTable dtbWCCapacityToReturn = new DataTable();
		private DataTable dtbWCCapacityByEachDay = new DataTable();
		//private DataTable dtbWCCapacity = new DataTable();
		public DataSet dstShift = new DataSet();
		public DataTable dtbShiftCapacity = new DataTable();
		public int intFakeWCCapacityID = 0;
		public ArrayList arlDeletedIDs = new ArrayList();
		private string strWorkCenter = string.Empty;
		private int intWorkCenterID = 0;
		private int intCycleID = 0;
		DateTime dtmMinBeginDate = new DateTime();
		DateTime dtmMaxEndDate = new DateTime();
		DateTime dtmMinBeginDateCycle = new DateTime();
		DateTime dtmMaxEndDateCycle = new DateTime();
		DateTime dtmMinCycle = new DateTime();
		DateTime dtmMaxCycle = new DateTime();
		private System.Windows.Forms.Label lblLabor;
		private System.Windows.Forms.Label lblMachineCaption;
		private const string THIS = "PCSProduction.DCP.DCPAdjustment";
		private const string SELECT = "Select";
		private const string SHIFT_PATTERN = "ShiftPattern";
		private const string SHIFT_DESCRIPTION = "Shift";
		private const string DAY_FLD = "Day";
		private const string NEWID_FLD = "NewID";
		DateTime dtmSelectedDate = new DateTime();
		DateTime dtmSelectedRow = new DateTime();
		string strType = string.Empty;
		bool blnIsWorkingDay = false;
		bool blnHasError = true;
		private C1.Win.C1TrueDBGrid.C1TrueDBGrid dgrdShift;
		bool blnIsChange = false;
		public DCPAdjustment()
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
		/// A new contructor method for WCCapacity Adjustment
		/// </summary>
		/// <param name="pdtbWCCapacity"></param>
		/// <author>Trada</author>
		/// <date>Thursday, January 5 2006</date>
		public DCPAdjustment(DataTable pdtbWCCapacity, DataTable pdtbShiftCapacity, string pstrWorkCenter, int pintWorkCenterID, int pintFakeWCCapacityID, ArrayList parlDeletedIDs, int pintCycleID, DateTime pdtmSelectedRow)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			dtbWCCapacity = pdtbWCCapacity.Copy();
			dtbShiftCapacity = pdtbShiftCapacity.Copy();
			strWorkCenter = pstrWorkCenter;
			intWorkCenterID = pintWorkCenterID;
			intFakeWCCapacityID = pintFakeWCCapacityID;
			arlDeletedIDs = parlDeletedIDs;
			intCycleID = pintCycleID;
			dtmSelectedRow = pdtmSelectedRow;
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(DCPAdjustment));
			this.btnClose = new System.Windows.Forms.Button();
			this.btnHelp = new System.Windows.Forms.Button();
			this.btnOK = new System.Windows.Forms.Button();
			this.monthCalendar = new System.Windows.Forms.MonthCalendar();
			this.numCapacity = new C1.Win.C1Input.C1NumericEdit();
			this.lblCapacity = new System.Windows.Forms.Label();
			this.grpAdjust = new System.Windows.Forms.GroupBox();
			this.dgrdShift = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
			this.lblType = new System.Windows.Forms.Label();
			this.cboType = new System.Windows.Forms.ComboBox();
			this.lblShifts = new System.Windows.Forms.Label();
			this.numFactor = new C1.Win.C1Input.C1NumericEdit();
			this.lblFactor = new System.Windows.Forms.Label();
			this.numCrewSize = new C1.Win.C1Input.C1NumericEdit();
			this.lblCrewsize = new System.Windows.Forms.Label();
			this.numMachine = new C1.Win.C1Input.C1NumericEdit();
			this.chkWorkingday = new System.Windows.Forms.CheckBox();
			this.lblMachine = new System.Windows.Forms.Label();
			this.grpCalendar = new System.Windows.Forms.GroupBox();
			this.txtWorkCenter = new System.Windows.Forms.TextBox();
			this.lblWorkCenter = new System.Windows.Forms.Label();
			this.lblLabor = new System.Windows.Forms.Label();
			this.lblMachineCaption = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.numCapacity)).BeginInit();
			this.grpAdjust.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgrdShift)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numFactor)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numCrewSize)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numMachine)).BeginInit();
			this.grpCalendar.SuspendLayout();
			this.SuspendLayout();
			// 
			// btnClose
			// 
			this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnClose.Location = new System.Drawing.Point(408, 265);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(63, 23);
			this.btnClose.TabIndex = 4;
			this.btnClose.Text = "&Close";
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			// 
			// btnHelp
			// 
			this.btnHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnHelp.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnHelp.Location = new System.Drawing.Point(344, 265);
			this.btnHelp.Name = "btnHelp";
			this.btnHelp.Size = new System.Drawing.Size(63, 23);
			this.btnHelp.TabIndex = 3;
			this.btnHelp.Text = "&Help";
			this.btnHelp.Click += new System.EventHandler(this.btnHelp_Click);
			// 
			// btnOK
			// 
			this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnOK.Location = new System.Drawing.Point(8, 265);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(63, 23);
			this.btnOK.TabIndex = 2;
			this.btnOK.Text = "&OK";
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// monthCalendar
			// 
			this.monthCalendar.Location = new System.Drawing.Point(6, 50);
			this.monthCalendar.MaxSelectionCount = 1;
			this.monthCalendar.Name = "monthCalendar";
			this.monthCalendar.TabIndex = 2;
			this.monthCalendar.DateSelected += new System.Windows.Forms.DateRangeEventHandler(this.monthCalendar_DateSelected);
			this.monthCalendar.DateChanged += new System.Windows.Forms.DateRangeEventHandler(this.monthCalendar_DateChanged);
			// 
			// numCapacity
			// 
			this.numCapacity.AccessibleDescription = "";
			this.numCapacity.AccessibleName = "";
			// 
			// numCapacity.Calculator
			// 
			this.numCapacity.Calculator.AccessibleDescription = "";
			this.numCapacity.Calculator.AccessibleName = "";
			this.numCapacity.CustomFormat = "###############,0.00";
			this.numCapacity.EmptyAsNull = true;
			this.numCapacity.ErrorInfo.ShowErrorMessage = false;
			this.numCapacity.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
			this.numCapacity.Location = new System.Drawing.Point(80, 218);
			this.numCapacity.Name = "numCapacity";
			this.numCapacity.ReadOnly = true;
			this.numCapacity.Size = new System.Drawing.Size(124, 20);
			this.numCapacity.TabIndex = 4;
			this.numCapacity.Tag = null;
			this.numCapacity.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.numCapacity.Value = new System.Decimal(new int[] {
																	  0,
																	  0,
																	  0,
																	  0});
			this.numCapacity.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.None;
			this.numCapacity.ValueChanged += new System.EventHandler(this.numCapacity_ValueChanged);
			// 
			// lblCapacity
			// 
			this.lblCapacity.Location = new System.Drawing.Point(8, 216);
			this.lblCapacity.Name = "lblCapacity";
			this.lblCapacity.Size = new System.Drawing.Size(68, 23);
			this.lblCapacity.TabIndex = 3;
			this.lblCapacity.Text = "Capacity (s)";
			this.lblCapacity.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// grpAdjust
			// 
			this.grpAdjust.Controls.Add(this.dgrdShift);
			this.grpAdjust.Controls.Add(this.lblType);
			this.grpAdjust.Controls.Add(this.cboType);
			this.grpAdjust.Controls.Add(this.lblShifts);
			this.grpAdjust.Controls.Add(this.numFactor);
			this.grpAdjust.Controls.Add(this.lblFactor);
			this.grpAdjust.Controls.Add(this.numCrewSize);
			this.grpAdjust.Controls.Add(this.lblCrewsize);
			this.grpAdjust.Controls.Add(this.numMachine);
			this.grpAdjust.Controls.Add(this.chkWorkingday);
			this.grpAdjust.Controls.Add(this.lblMachine);
			this.grpAdjust.Location = new System.Drawing.Point(228, 10);
			this.grpAdjust.Name = "grpAdjust";
			this.grpAdjust.Size = new System.Drawing.Size(244, 250);
			this.grpAdjust.TabIndex = 1;
			this.grpAdjust.TabStop = false;
			this.grpAdjust.Text = "Adjustment information";
			// 
			// dgrdShift
			// 
			this.dgrdShift.CaptionHeight = 17;
			this.dgrdShift.CollapseColor = System.Drawing.Color.Black;
			this.dgrdShift.ExpandColor = System.Drawing.Color.Black;
			this.dgrdShift.GroupByCaption = "Drag a column header here to group by that column";
			this.dgrdShift.Images.Add(((System.Drawing.Image)(resources.GetObject("resource"))));
			this.dgrdShift.Location = new System.Drawing.Point(8, 142);
			this.dgrdShift.MarqueeStyle = C1.Win.C1TrueDBGrid.MarqueeEnum.DottedCellBorder;
			this.dgrdShift.Name = "dgrdShift";
			this.dgrdShift.PreviewInfo.Location = new System.Drawing.Point(0, 0);
			this.dgrdShift.PreviewInfo.Size = new System.Drawing.Size(0, 0);
			this.dgrdShift.PreviewInfo.ZoomFactor = 75;
			this.dgrdShift.PrintInfo.ShowOptionsDialog = false;
			this.dgrdShift.RecordSelectorWidth = 17;
			this.dgrdShift.RowDivider.Color = System.Drawing.Color.DarkGray;
			this.dgrdShift.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.Single;
			this.dgrdShift.RowHeight = 15;
			this.dgrdShift.RowSubDividerColor = System.Drawing.Color.DarkGray;
			this.dgrdShift.Size = new System.Drawing.Size(228, 100);
			this.dgrdShift.TabIndex = 10;
			this.dgrdShift.BeforeColEdit += new C1.Win.C1TrueDBGrid.BeforeColEditEventHandler(this.dgrdShift_BeforeColEdit);
			this.dgrdShift.AfterColUpdate += new C1.Win.C1TrueDBGrid.ColEventHandler(this.dgrdShift_AfterColUpdate);
			this.dgrdShift.Leave += new System.EventHandler(this.dgrdShift_Leave);
			this.dgrdShift.PropBag = "<?xml version=\"1.0\"?><Blob><Styles type=\"C1.Win.C1TrueDBGrid.Design.ContextWrappe" +
				"r\"><Data>Group{BackColor:ControlDark;Border:None,,0, 0, 0, 0;AlignVert:Center;}E" +
				"ditor{}Style2{}Style5{}Style4{}Style7{}Style6{}EvenRow{BackColor:Aqua;}Selected{" +
				"ForeColor:HighlightText;BackColor:Highlight;}Style3{}Inactive{ForeColor:Inactive" +
				"CaptionText;BackColor:InactiveCaption;}FilterBar{}Footer{}Caption{AlignHorz:Cent" +
				"er;}Style9{}Normal{}HighlightRow{ForeColor:HighlightText;BackColor:Highlight;}St" +
				"yle14{}OddRow{}RecordSelector{AlignImage:Center;}Style15{}Heading{Wrap:True;Alig" +
				"nVert:Center;Border:Raised,,1, 1, 1, 1;ForeColor:ControlText;BackColor:Control;}" +
				"Style8{}Style10{AlignHorz:Near;}Style11{}Style12{}Style13{}Style1{}</Data></Styl" +
				"es><Splits><C1.Win.C1TrueDBGrid.MergeView Name=\"\" CaptionHeight=\"17\" ColumnCapti" +
				"onHeight=\"17\" ColumnFooterHeight=\"17\" MarqueeStyle=\"DottedCellBorder\" RecordSele" +
				"ctorWidth=\"17\" DefRecSelWidth=\"17\" VerticalScrollGroup=\"1\" HorizontalScrollGroup" +
				"=\"1\"><ClientRect>0, 0, 224, 96</ClientRect><BorderSide>0</BorderSide><CaptionSty" +
				"le parent=\"Style2\" me=\"Style10\" /><EditorStyle parent=\"Editor\" me=\"Style5\" /><Ev" +
				"enRowStyle parent=\"EvenRow\" me=\"Style8\" /><FilterBarStyle parent=\"FilterBar\" me=" +
				"\"Style13\" /><FooterStyle parent=\"Footer\" me=\"Style3\" /><GroupStyle parent=\"Group" +
				"\" me=\"Style12\" /><HeadingStyle parent=\"Heading\" me=\"Style2\" /><HighLightRowStyle" +
				" parent=\"HighlightRow\" me=\"Style7\" /><InactiveStyle parent=\"Inactive\" me=\"Style4" +
				"\" /><OddRowStyle parent=\"OddRow\" me=\"Style9\" /><RecordSelectorStyle parent=\"Reco" +
				"rdSelector\" me=\"Style11\" /><SelectedStyle parent=\"Selected\" me=\"Style6\" /><Style" +
				" parent=\"Normal\" me=\"Style1\" /></C1.Win.C1TrueDBGrid.MergeView></Splits><NamedSt" +
				"yles><Style parent=\"\" me=\"Normal\" /><Style parent=\"Normal\" me=\"Heading\" /><Style" +
				" parent=\"Heading\" me=\"Footer\" /><Style parent=\"Heading\" me=\"Caption\" /><Style pa" +
				"rent=\"Heading\" me=\"Inactive\" /><Style parent=\"Normal\" me=\"Selected\" /><Style par" +
				"ent=\"Normal\" me=\"Editor\" /><Style parent=\"Normal\" me=\"HighlightRow\" /><Style par" +
				"ent=\"Normal\" me=\"EvenRow\" /><Style parent=\"Normal\" me=\"OddRow\" /><Style parent=\"" +
				"Heading\" me=\"RecordSelector\" /><Style parent=\"Normal\" me=\"FilterBar\" /><Style pa" +
				"rent=\"Caption\" me=\"Group\" /></NamedStyles><vertSplits>1</vertSplits><horzSplits>" +
				"1</horzSplits><Layout>None</Layout><DefaultRecSelWidth>17</DefaultRecSelWidth><C" +
				"lientArea>0, 0, 224, 96</ClientArea><PrintPageHeaderStyle parent=\"\" me=\"Style14\"" +
				" /><PrintPageFooterStyle parent=\"\" me=\"Style15\" /></Blob>";
			// 
			// lblType
			// 
			this.lblType.Location = new System.Drawing.Point(8, 59);
			this.lblType.Name = "lblType";
			this.lblType.Size = new System.Drawing.Size(78, 20);
			this.lblType.TabIndex = 3;
			this.lblType.Text = "Type";
			this.lblType.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// cboType
			// 
			this.cboType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboType.Location = new System.Drawing.Point(108, 58);
			this.cboType.Name = "cboType";
			this.cboType.Size = new System.Drawing.Size(128, 21);
			this.cboType.TabIndex = 4;
			this.cboType.Validating += new System.ComponentModel.CancelEventHandler(this.numFactor_Validating);
			this.cboType.TextChanged += new System.EventHandler(this.cboType_TextChanged);
			this.cboType.SelectedIndexChanged += new System.EventHandler(this.cboType_SelectedIndexChanged);
			// 
			// lblShifts
			// 
			this.lblShifts.Location = new System.Drawing.Point(8, 126);
			this.lblShifts.Name = "lblShifts";
			this.lblShifts.Size = new System.Drawing.Size(74, 16);
			this.lblShifts.TabIndex = 9;
			this.lblShifts.Text = "Shifts";
			// 
			// numFactor
			// 
			this.numFactor.AccessibleDescription = "";
			this.numFactor.AccessibleName = "";
			// 
			// numFactor.Calculator
			// 
			this.numFactor.Calculator.AccessibleDescription = "";
			this.numFactor.Calculator.AccessibleName = "";
			this.numFactor.CustomFormat = "###############,0.00";
			this.numFactor.EmptyAsNull = true;
			this.numFactor.ErrorInfo.ShowErrorMessage = false;
			this.numFactor.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
			this.numFactor.Location = new System.Drawing.Point(108, 36);
			this.numFactor.Name = "numFactor";
			this.numFactor.PostValidation.Intervals.AddRange(new C1.Win.C1Input.ValueInterval[] {
																									new C1.Win.C1Input.ValueInterval(new System.Decimal(new int[] {
																																									  0,
																																									  0,
																																									  0,
																																									  0}), new System.Decimal(new int[] {
																																																			100,
																																																			0,
																																																			0,
																																																			0}), false, true)});
			this.numFactor.Size = new System.Drawing.Size(128, 20);
			this.numFactor.TabIndex = 2;
			this.numFactor.Tag = null;
			this.numFactor.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.numFactor.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.None;
			this.numFactor.Validating += new System.ComponentModel.CancelEventHandler(this.numFactor_Validating);
			this.numFactor.Leave += new System.EventHandler(this.numFactor_Leave);
			this.numFactor.ValidationError += new C1.Win.C1Input.ValidationErrorEventHandler(this.numFactor_ValidationError);
			// 
			// lblFactor
			// 
			this.lblFactor.Location = new System.Drawing.Point(10, 36);
			this.lblFactor.Name = "lblFactor";
			this.lblFactor.Size = new System.Drawing.Size(75, 20);
			this.lblFactor.TabIndex = 1;
			this.lblFactor.Text = "Factor (%)";
			this.lblFactor.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// numCrewSize
			// 
			this.numCrewSize.AccessibleDescription = "";
			this.numCrewSize.AccessibleName = "";
			// 
			// numCrewSize.Calculator
			// 
			this.numCrewSize.Calculator.AccessibleDescription = "";
			this.numCrewSize.Calculator.AccessibleName = "";
			this.numCrewSize.CustomFormat = "###############,0.00";
			this.numCrewSize.EmptyAsNull = true;
			this.numCrewSize.ErrorInfo.ShowErrorMessage = false;
			this.numCrewSize.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
			this.numCrewSize.Location = new System.Drawing.Point(108, 103);
			this.numCrewSize.MaxLength = 9;
			this.numCrewSize.Name = "numCrewSize";
			this.numCrewSize.Size = new System.Drawing.Size(128, 20);
			this.numCrewSize.TabIndex = 8;
			this.numCrewSize.Tag = null;
			this.numCrewSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.numCrewSize.Value = new System.Decimal(new int[] {
																	  0,
																	  0,
																	  0,
																	  0});
			this.numCrewSize.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.None;
			this.numCrewSize.Validating += new System.ComponentModel.CancelEventHandler(this.numFactor_Validating);
			this.numCrewSize.Leave += new System.EventHandler(this.numFactor_Leave);
			// 
			// lblCrewsize
			// 
			this.lblCrewsize.Location = new System.Drawing.Point(8, 104);
			this.lblCrewsize.Name = "lblCrewsize";
			this.lblCrewsize.Size = new System.Drawing.Size(75, 20);
			this.lblCrewsize.TabIndex = 7;
			this.lblCrewsize.Text = "Crew size (M)";
			this.lblCrewsize.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// numMachine
			// 
			this.numMachine.AccessibleDescription = "";
			this.numMachine.AccessibleName = "";
			// 
			// numMachine.Calculator
			// 
			this.numMachine.Calculator.AccessibleDescription = "";
			this.numMachine.Calculator.AccessibleName = "";
			this.numMachine.CustomFormat = "###############,0.00";
			this.numMachine.EmptyAsNull = true;
			this.numMachine.ErrorInfo.ShowErrorMessage = false;
			this.numMachine.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
			this.numMachine.Location = new System.Drawing.Point(108, 81);
			this.numMachine.MaxLength = 9;
			this.numMachine.Name = "numMachine";
			this.numMachine.Size = new System.Drawing.Size(128, 20);
			this.numMachine.TabIndex = 6;
			this.numMachine.Tag = null;
			this.numMachine.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.numMachine.Value = new System.Decimal(new int[] {
																	 0,
																	 0,
																	 0,
																	 0});
			this.numMachine.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.None;
			this.numMachine.Validating += new System.ComponentModel.CancelEventHandler(this.numFactor_Validating);
			this.numMachine.Leave += new System.EventHandler(this.numFactor_Leave);
			// 
			// chkWorkingday
			// 
			this.chkWorkingday.Location = new System.Drawing.Point(10, 16);
			this.chkWorkingday.Name = "chkWorkingday";
			this.chkWorkingday.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.chkWorkingday.Size = new System.Drawing.Size(112, 22);
			this.chkWorkingday.TabIndex = 0;
			this.chkWorkingday.Text = "Working day";
			this.chkWorkingday.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.chkWorkingday.Click += new System.EventHandler(this.chkWorkingday_Click);
			this.chkWorkingday.CheckedChanged += new System.EventHandler(this.chkWorkingday_CheckedChanged);
			// 
			// lblMachine
			// 
			this.lblMachine.Location = new System.Drawing.Point(8, 82);
			this.lblMachine.Name = "lblMachine";
			this.lblMachine.Size = new System.Drawing.Size(108, 20);
			this.lblMachine.TabIndex = 5;
			this.lblMachine.Text = "No. of Machine (M)";
			this.lblMachine.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// grpCalendar
			// 
			this.grpCalendar.Controls.Add(this.txtWorkCenter);
			this.grpCalendar.Controls.Add(this.monthCalendar);
			this.grpCalendar.Controls.Add(this.lblCapacity);
			this.grpCalendar.Controls.Add(this.numCapacity);
			this.grpCalendar.Controls.Add(this.lblWorkCenter);
			this.grpCalendar.Location = new System.Drawing.Point(8, 10);
			this.grpCalendar.Name = "grpCalendar";
			this.grpCalendar.Size = new System.Drawing.Size(212, 250);
			this.grpCalendar.TabIndex = 0;
			this.grpCalendar.TabStop = false;
			this.grpCalendar.Text = "Calendar";
			// 
			// txtWorkCenter
			// 
			this.txtWorkCenter.Location = new System.Drawing.Point(72, 20);
			this.txtWorkCenter.Name = "txtWorkCenter";
			this.txtWorkCenter.ReadOnly = true;
			this.txtWorkCenter.Size = new System.Drawing.Size(132, 20);
			this.txtWorkCenter.TabIndex = 1;
			this.txtWorkCenter.Text = "";
			// 
			// lblWorkCenter
			// 
			this.lblWorkCenter.Location = new System.Drawing.Point(6, 20);
			this.lblWorkCenter.Name = "lblWorkCenter";
			this.lblWorkCenter.Size = new System.Drawing.Size(70, 23);
			this.lblWorkCenter.TabIndex = 0;
			this.lblWorkCenter.Text = "Work center";
			// 
			// lblLabor
			// 
			this.lblLabor.Location = new System.Drawing.Point(100, 266);
			this.lblLabor.Name = "lblLabor";
			this.lblLabor.TabIndex = 5;
			this.lblLabor.Text = "Labor";
			this.lblLabor.Visible = false;
			// 
			// lblMachineCaption
			// 
			this.lblMachineCaption.Location = new System.Drawing.Point(228, 266);
			this.lblMachineCaption.Name = "lblMachineCaption";
			this.lblMachineCaption.TabIndex = 6;
			this.lblMachineCaption.Text = "Machine";
			this.lblMachineCaption.Visible = false;
			// 
			// DCPAdjustment
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.btnClose;
			this.ClientSize = new System.Drawing.Size(476, 294);
			this.Controls.Add(this.lblMachineCaption);
			this.Controls.Add(this.lblLabor);
			this.Controls.Add(this.grpCalendar);
			this.Controls.Add(this.grpAdjust);
			this.Controls.Add(this.btnClose);
			this.Controls.Add(this.btnHelp);
			this.Controls.Add(this.btnOK);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.KeyPreview = true;
			this.MaximizeBox = false;
			this.Name = "DCPAdjustment";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Detail Capacity Planning Adjustment";
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DCPAdjustment_KeyDown);
			this.Closing += new System.ComponentModel.CancelEventHandler(this.DCPAdjustment_Closing);
			this.Load += new System.EventHandler(this.DCPAdjustment_Load);
			((System.ComponentModel.ISupportInitialize)(this.numCapacity)).EndInit();
			this.grpAdjust.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dgrdShift)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numFactor)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numCrewSize)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numMachine)).EndInit();
			this.grpCalendar.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion
		/// <summary>
		/// DCPAdjustment_Load
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Thursday, January 5 2006</date>
		private void DCPAdjustment_Load(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".DCPAdjustment_Load()";
			const string MAX_DAYS ="MaxDays";
			try
			{
				//Set authorization for user
				Security objSecurity = new Security();
				this.Name = THIS;
				if(objSecurity.SetRightForUserOnForm(this, SystemProperty.UserName) == 0)
				{
					this.Close();
					// You don't have the right to view this item
					PCSMessageBox.Show(ErrorCode.MESSAGE_YOU_HAVE_NO_RIGHT_TO_VIEW);
					return;
				}
				//Set today is server date
				monthCalendar.TodayDate = (new UtilsBO()).GetDBDate();
				numMachine.FormatType = FormatTypeEnum.CustomFormat;
				numMachine.CustomFormat = Constants.INTERGER_NUMBERFORMAT;
				//Lock Form
				this.FormBorderStyle = FormBorderStyle.FixedSingle;
				this.MaximizeBox = false;
				//Load Type combo box
				cboType.Items.Clear();
				cboType.Items.Add(lblLabor.Text);// = 0
				cboType.Items.Add(lblMachineCaption.Text);   // = 1
				cboType.SelectedItem = null;
				txtWorkCenter.Text = strWorkCenter;
				DataRow[] adrowDateFromWCCapacity = dtbWCCapacity.Select(string.Empty, PRO_WCCapacityTable.BEGINDATE_FLD);
				if (adrowDateFromWCCapacity.Length > 0)
				{
					dtmMinBeginDateCycle = (DateTime) adrowDateFromWCCapacity[0][PRO_WCCapacityTable.BEGINDATE_FLD];
					dtmMaxEndDateCycle = (DateTime) adrowDateFromWCCapacity[adrowDateFromWCCapacity.Length - 1][PRO_WCCapacityTable.ENDDATE_FLD];	
				}
				//Creat a new WCCapacity table for each day
				dtbWCCapacityByEachDay = CreatWCCapactiyByDayTable(dtbWCCapacity);
				
				dstShift = (new WorkCenterCapacityBO()).GetShiftAndShiftPattern();
				dstShift = FormControlComponents.AddSelectColumnToTheFirstPositionOfGrid(dstShift);
				dgrdShift.DataSource = dstShift.Tables[0];
				//Config grid
				dgrdShift.Splits[0].DisplayColumns[SELECT].Width = 85;
				dgrdShift.Splits[0].DisplayColumns[PRO_ShiftTable.SHIFTDESC_FLD].DataColumn.Caption = SHIFT_DESCRIPTION;
				dgrdShift.Splits[0].DisplayColumns[PRO_ShiftTable.SHIFTDESC_FLD].HeadingStyle.HorizontalAlignment = AlignHorzEnum.Center;
				dgrdShift.Splits[0].DisplayColumns[PRO_ShiftTable.SHIFTDESC_FLD].Locked = true;
				dgrdShift.Splits[0].DisplayColumns[SHIFT_PATTERN].Visible = false;
				dgrdShift.Splits[0].DisplayColumns[PRO_ShiftTable.SHIFTID_FLD].Visible = false;

				//Initialize 
				DateTime dtmToday = new DateTime();
				dtmToday = GetDateOnly(monthCalendar.TodayDate);
				//Load WCCapacity from Database
				WorkCenterCapacityBO boWCCapacity = new WorkCenterCapacityBO();
				DataSet dstWCCapacityFromDatabase = boWCCapacity.GetWCCapacityByWorkCenter(SystemProperty.CCNID, intWorkCenterID);
				dtbShiftCapacity = boWCCapacity.GetShiftCapacityByWorkCenter(SystemProperty.CCNID, intWorkCenterID).Tables[0];
				dtbShiftCapacity.PrimaryKey = new DataColumn[]{dtbShiftCapacity.Columns[PRO_ShiftCapacityTable.SHIFTID_FLD], 
																  dtbShiftCapacity.Columns[PRO_ShiftCapacityTable.WCCAPACITYID_FLD]};
				dtbShiftCapacity.Columns.Add(NEWID_FLD, typeof(int));
				//Sort by Begin Date
				DataRow[] adrowDate = dstWCCapacityFromDatabase.Tables[0].Select(string.Empty, PRO_WCCapacityTable.BEGINDATE_FLD);
				if (adrowDate.Length > 0)
				{
					dtmMinBeginDate = (DateTime) adrowDate[0][PRO_WCCapacityTable.BEGINDATE_FLD];
					dtmMaxEndDate = (DateTime) adrowDate[adrowDate.Length - 1][PRO_WCCapacityTable.ENDDATE_FLD];	
				}
				//InitFormWithDate(dtmToday);
				dstShift.AcceptChanges();
				blnIsChange = false;
				//Get Cycle
				DCOptionsBO boDCOptions = new DCOptionsBO();
				if (intCycleID != 0)
				{
					DataRow drowCycle = boDCOptions.GetDCOptionMaster(intCycleID);
					dtmMinCycle = (DateTime)drowCycle[MTR_MPSCycleOptionMasterTable.ASOFDATE_FLD];
					dtmMaxCycle = dtmMinCycle.AddDays(int.Parse(drowCycle[PRO_DCOptionMasterTable.PLANHORIZON_FLD].ToString()));
				}
				else
				{
					dtmMinCycle = dtmMinBeginDate;
					dtmMaxCycle = dtmMaxEndDate;
				}
//				monthCalendar.SetDate(dtmMinCycle);
//				InitFormWithDate(dtmMinCycle);
				monthCalendar.SetDate(dtmSelectedRow);
				InitFormWithDate(dtmSelectedRow);
				//c1TrueDBGrid1.DataSource = dtbWCCapacityByEachDay;
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
		/// CreatWCCapactiyByDayTable
		/// </summary>
		/// <param name="pdtbWCCapacity"></param>
		/// <returns></returns>
		/// <author>Trada</author>
		/// <date>Friday, January 6 2006</date>
		private DataTable CreatWCCapactiyByDayTable(DataTable pdtbWCCapacity)
		{
			const string METHOD_NAME = THIS + ".CreatWCCapactiyByDayTable()";
			try
			{
				DataTable dtbOutput = new DataTable();
				//add Day columns
				dtbOutput.Columns.Add(DAY_FLD, typeof(DateTime));
				//add all fields of dtbWCCapacity in to dtbOutput, except StartDate and EndDate. 
				for (int i = 2; i < pdtbWCCapacity.Columns.Count; i++)
				{
					dtbOutput.Columns.Add(pdtbWCCapacity.Columns[i].ColumnName);
				}
				//add New ID
				dtbOutput.Columns.Add(NEWID_FLD, typeof(int));
				dtbOutput.Columns[NEWID_FLD].AutoIncrement = true;
				dtbOutput.Columns[NEWID_FLD].AutoIncrementSeed = 1;
				dtbOutput.Columns[NEWID_FLD].AutoIncrementStep = 1;
				//add Working Day 
				dtbOutput.Columns.Add(MST_WorkingDayDetailTable.OFFDAY_FLD, typeof(bool));
				foreach (DataRow drow in pdtbWCCapacity.Rows)
				{
					int intLoop = (GetDateOnly((DateTime)drow[PRO_WCCapacityTable.ENDDATE_FLD]) - GetDateOnly((DateTime)drow[PRO_WCCapacityTable.BEGINDATE_FLD])).Days;
					for (int j = 0; j <= intLoop; j++)
					{
						DataRow drowOutput = dtbOutput.NewRow();	
						drowOutput[DAY_FLD] = (GetDateOnly((DateTime)drow[PRO_WCCapacityTable.BEGINDATE_FLD])).AddDays(j);
						for (int i = 2; i < pdtbWCCapacity.Columns.Count; i++)
						{
							drowOutput[i-1] = drow[i];
						}		
						drowOutput[MST_WorkingDayDetailTable.OFFDAY_FLD] = false;
						dtbOutput.Rows.Add(drowOutput);
					}
				}
				return dtbOutput;
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
		/// Get Date Only
		/// </summary>
		/// <param name="pdtmInputDate"></param>
		/// <returns></returns>
		/// <author>Trada</author>
		/// <date>Friday, January 6 2006</date>
		private DateTime GetDateOnly(DateTime pdtmInputDate)
		{
			const string METHOD_NAME = THIS + ".GetDateOnly()";
			try
			{
				DateTime dtmOutputDate = new DateTime(pdtmInputDate.Year, pdtmInputDate.Month, pdtmInputDate.Day);
				return dtmOutputDate;
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
		/// This method used to add select column to the first position of a table
		/// </summary>
		/// <param name="pdtbInput"></param>
		/// <returns></returns>
		/// <author>Trada</author>
		/// <date>Monday, January 9 2006</date>
		private DataTable AddSelectColumnToTheFirstPositionOfTable(DataTable pdtbInput)
		{
			const string SELECT = "Select";
			const string METHOD_NAME = THIS + ".AddSelectColumnToTheFirstPositionOfTable()";
			try
			{
				DataTable dtbOutput = new DataTable();
				dtbOutput.Columns.Add(SELECT, typeof(bool));
				for (int i = 0; i < pdtbInput.Columns.Count; i++)
				{
					dtbOutput.Columns.Add(pdtbInput.Columns[i].ColumnName, pdtbInput.Columns[i].DataType);
				}
				foreach(DataRow drow in pdtbInput.Rows)
				{
					DataRow drowData = dtbOutput.NewRow(); 
					foreach (DataColumn dcol in pdtbInput.Columns)
					{
						drowData[dcol.ColumnName] = drow[dcol.ColumnName];
					}
					dtbOutput.Rows.Add(drowData);
				}
				foreach(DataRow drow in dtbOutput.Rows)
				{
					drow[SELECT] = false;
				}
				return dtbOutput;
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
		/// Check if information of the selected day was changed
		/// </summary>
		/// <param name="pdtmInputDate"></param>
		/// <returns></returns>
		private bool IsChange(DateTime pdtmInputDate)
		{
			const string METHOD_NAME = THIS + ".IsChange()";
			try
			{
				DataRow[] adrowResult = dtbWCCapacityByEachDay.Select(DAY_FLD + " = '" + pdtmInputDate.ToShortDateString() + "'");
				if (adrowResult.Length > 0)
				{
					if (!chkWorkingday.Checked)
						return true;
				}
				//check if shift was changed
				//				if (dstShift.HasChanges())
				//					return true;
				if (blnIsChange)
					return true;
				return false;
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
		/// RebuildWCCapacityTable
		/// </summary>
		/// <author>Trada</author>
		/// <date>Monday, January 16 2006</date>
		private void RebuildWCCapacityTable()
		{
			const string METHOD_NAME = THIS + ".RebuildWCCapacityTable()";
			try
			{
				if (ValidateData())
				{
					DateTime dtmOldEndDate = new DateTime();
					DateTime dtmOldBeginDate = new DateTime();
					bool blnAddRow = false;
					int intWCCapacityID = 0;
					DataRow drowWCCapacityByEachDay = null;
					if (CheckDateInPeriod(monthCalendar.SelectionStart)) 
					{
						#region Date is in period
						DataRow drowNew = null;
						
						if (IsChange(monthCalendar.SelectionStart))
						{
							DataRow[] adrowWCCapacityByEachDay = dtbWCCapacityByEachDay.Select(DAY_FLD + " = '" + monthCalendar.SelectionStart.ToShortDateString() + "'");
							if (adrowWCCapacityByEachDay.Length > 0) //Date is in period
							{
								intWCCapacityID = int.Parse(adrowWCCapacityByEachDay[0][PRO_WCCapacityTable.WCCAPACITYID_FLD].ToString());
								foreach (DataRow drowWCCapacity in dtbWCCapacity.Rows)
								{
									if (drowWCCapacity[PRO_WCCapacityTable.WCCAPACITYID_FLD].ToString() == intWCCapacityID.ToString())
									{
										blnAddRow = true;
										dtmOldEndDate = (DateTime)drowWCCapacity[PRO_WCCapacityTable.ENDDATE_FLD];
										dtmOldBeginDate = (DateTime)drowWCCapacity[PRO_WCCapacityTable.BEGINDATE_FLD];
										//Update case
										if ((dtmOldBeginDate == dtmOldEndDate)&&(monthCalendar.SelectionStart == dtmOldBeginDate))
										{
											drowWCCapacity[PRO_WCCapacityTable.FACTOR_FLD] = numFactor.Value;
											drowWCCapacity[PRO_WCCapacityTable.WCTYPE_FLD] = cboType.SelectedIndex;
											drowWCCapacity[PRO_WCCapacityTable.CREWSIZE_FLD] = numCrewSize.Value;
											drowWCCapacity[PRO_WCCapacityTable.MACHINENO_FLD] = numMachine.Value;
											drowWCCapacity[PRO_WCCapacityTable.CAPACITY_FLD] = numCapacity.Value;
										}
										else
										{
											//add new
											if (monthCalendar.SelectionStart == dtmOldBeginDate)
											{
												//Update Begin Date
												drowWCCapacity[PRO_WCCapacityTable.BEGINDATE_FLD] = monthCalendar.SelectionStart.AddDays(1);
											}
											else
											{
												//Update End Date
												drowWCCapacity[PRO_WCCapacityTable.ENDDATE_FLD] = monthCalendar.SelectionStart.AddDays(-1);
											}
										}
										//update WCCapacity by each day Table
										foreach (DataRow drowWCCByEachDay in dtbWCCapacityByEachDay.Rows)
										{
											if (monthCalendar.SelectionStart == (DateTime)drowWCCByEachDay[DAY_FLD])
											{
												drowWCCByEachDay[PRO_WCCapacityTable.FACTOR_FLD] = numFactor.Value;
												drowWCCByEachDay[PRO_WCCapacityTable.WCTYPE_FLD] = cboType.SelectedIndex;
												drowWCCByEachDay[PRO_WCCapacityTable.CREWSIZE_FLD] = numCrewSize.Value;
												drowWCCByEachDay[PRO_WCCapacityTable.MACHINENO_FLD] = numMachine.Value;
												drowWCCByEachDay[PRO_WCCapacityTable.CAPACITY_FLD] = numCapacity.Value;
												drowWCCByEachDay[PRO_WCCapacityTable.CCNID_FLD] = SystemProperty.CCNID;
												drowWCCByEachDay[PRO_WCCapacityTable.WORKCENTERID_FLD] = intWorkCenterID;
												drowWCCByEachDay[MST_WorkingDayDetailTable.OFFDAY_FLD] = false;
											}
										}
									}
								}
								if (blnAddRow)
								{
									if (!chkWorkingday.Checked) //off day
									{
											
									}
									else
									{
										//Add a new row to WCCapacity Table
										drowNew = dtbWCCapacity.NewRow();
										//Generate a fake WCCapacityId 
										drowNew[PRO_WCCapacityTable.WCCAPACITYID_FLD] = --intFakeWCCapacityID;
										drowNew[PRO_WCCapacityTable.BEGINDATE_FLD] = monthCalendar.SelectionStart;
										drowNew[PRO_WCCapacityTable.ENDDATE_FLD] = monthCalendar.SelectionStart;
										drowNew[PRO_WCCapacityTable.FACTOR_FLD] = numFactor.Value;
										drowNew[PRO_WCCapacityTable.WCTYPE_FLD] = cboType.SelectedIndex;
										drowNew[PRO_WCCapacityTable.CREWSIZE_FLD] = numCrewSize.Value;
										drowNew[PRO_WCCapacityTable.MACHINENO_FLD] = numMachine.Value;
										drowNew[PRO_WCCapacityTable.CAPACITY_FLD] = numCapacity.Value;
										drowNew[PRO_WCCapacityTable.CCNID_FLD] = SystemProperty.CCNID;
										drowNew[PRO_WCCapacityTable.WORKCENTERID_FLD] = intWorkCenterID;
										//update ShiftCapacity
										for (int i = 0; i < dgrdShift.RowCount; i++)
										{
											if (dgrdShift[i, SELECT].ToString() == true.ToString())
											{
												DataRow drowShiftCapacity = dtbShiftCapacity.NewRow();
												drowShiftCapacity[PRO_ShiftCapacityTable.WCCAPACITYID_FLD] = intFakeWCCapacityID;
												drowShiftCapacity[PRO_ShiftCapacityTable.SHIFTID_FLD] = dgrdShift[i, PRO_ShiftCapacityTable.SHIFTID_FLD];
												dtbShiftCapacity.Rows.Add(drowShiftCapacity);
											}
										}
										
									}
									if ((dtmOldEndDate == monthCalendar.SelectionStart)
										||(monthCalendar.SelectionStart == dtmOldBeginDate))
									{
											
									}
									else
									{
										//add the last row
										drowNew = dtbWCCapacity.NewRow();
										//Generate a fake WCCapacityId 
										drowNew[PRO_WCCapacityTable.WCCAPACITYID_FLD] = --intFakeWCCapacityID;
										drowNew[PRO_WCCapacityTable.BEGINDATE_FLD] = monthCalendar.SelectionStart.AddDays(1);
										drowNew[PRO_WCCapacityTable.ENDDATE_FLD] = dtmOldEndDate;
										drowNew[PRO_WCCapacityTable.FACTOR_FLD] = adrowWCCapacityByEachDay[0][PRO_WCCapacityTable.FACTOR_FLD];
										drowNew[PRO_WCCapacityTable.WCTYPE_FLD] = adrowWCCapacityByEachDay[0][PRO_WCCapacityTable.WCTYPE_FLD];
										drowNew[PRO_WCCapacityTable.CREWSIZE_FLD] = adrowWCCapacityByEachDay[0][PRO_WCCapacityTable.CREWSIZE_FLD];
										drowNew[PRO_WCCapacityTable.MACHINENO_FLD] = adrowWCCapacityByEachDay[0][PRO_WCCapacityTable.MACHINENO_FLD];
										drowNew[PRO_WCCapacityTable.CAPACITY_FLD] = adrowWCCapacityByEachDay[0][PRO_WCCapacityTable.CAPACITY_FLD];
										drowNew[PRO_WCCapacityTable.CCNID_FLD] = SystemProperty.CCNID;
										drowNew[PRO_WCCapacityTable.WORKCENTERID_FLD] = intWorkCenterID;
										dtbWCCapacity.Rows.Add(drowNew);
										//update ShiftCapacity
										DataRow[] arrSelectedShift = dtbShiftCapacity.Select(PRO_ShiftCapacityTable.WCCAPACITYID_FLD + " = " + intWCCapacityID);
										for (int i = 0; i < arrSelectedShift.Length; i++)
										{
											DataRow drowShiftCapacity = dtbShiftCapacity.NewRow();
											drowShiftCapacity[PRO_ShiftCapacityTable.WCCAPACITYID_FLD] = intFakeWCCapacityID;
											drowShiftCapacity[PRO_ShiftCapacityTable.SHIFTID_FLD] = arrSelectedShift[i][PRO_ShiftCapacityTable.SHIFTID_FLD];
											dtbShiftCapacity.Rows.Add(drowShiftCapacity);
												
										}
									}
								}
							}
							else
							{
								if (chkWorkingday.Checked)
								{
									drowNew = dtbWCCapacity.NewRow();
									drowNew[PRO_WCCapacityTable.BEGINDATE_FLD] = monthCalendar.SelectionStart;
									drowNew[PRO_WCCapacityTable.ENDDATE_FLD] = monthCalendar.SelectionStart;
									drowNew[PRO_WCCapacityTable.FACTOR_FLD] = numFactor.Value;
									drowNew[PRO_WCCapacityTable.WCTYPE_FLD] = cboType.SelectedIndex;
									drowNew[PRO_WCCapacityTable.CREWSIZE_FLD] = numCrewSize.Value;
									drowNew[PRO_WCCapacityTable.MACHINENO_FLD] = numMachine.Value;
									drowNew[PRO_WCCapacityTable.CAPACITY_FLD] = numCapacity.Value;
									drowNew[PRO_WCCapacityTable.CCNID_FLD] = SystemProperty.CCNID;
									drowNew[PRO_WCCapacityTable.WORKCENTERID_FLD] = intWorkCenterID;
									//add fake WCCapacityID 
									drowNew[PRO_WCCapacityTable.WCCAPACITYID_FLD] = --intFakeWCCapacityID;
									//update ShiftCapacity
									for (int i = 0; i < dgrdShift.RowCount; i++)
									{
										if (dgrdShift[i, SELECT].ToString() == true.ToString())
										{
											DataRow drowShiftCapacity = dtbShiftCapacity.NewRow();
											drowShiftCapacity[PRO_ShiftCapacityTable.WCCAPACITYID_FLD] = intFakeWCCapacityID;
											drowShiftCapacity[PRO_ShiftCapacityTable.SHIFTID_FLD] = dgrdShift[i, PRO_ShiftCapacityTable.SHIFTID_FLD];
											dtbShiftCapacity.Rows.Add(drowShiftCapacity);
										}
									}
									dtbWCCapacity.Rows.Add(drowNew);
									
									//add a new row to WCCapacity by each day Table
									drowWCCapacityByEachDay = dtbWCCapacityByEachDay.NewRow();
									drowWCCapacityByEachDay[PRO_WCCapacityTable.FACTOR_FLD] = numFactor.Value;
									drowWCCapacityByEachDay[PRO_WCCapacityTable.WCTYPE_FLD] = cboType.SelectedIndex;
									drowWCCapacityByEachDay[PRO_WCCapacityTable.CREWSIZE_FLD] = numCrewSize.Value;
									drowWCCapacityByEachDay[PRO_WCCapacityTable.MACHINENO_FLD] = numMachine.Value;
									drowWCCapacityByEachDay[PRO_WCCapacityTable.CAPACITY_FLD] = numCapacity.Value;
									drowWCCapacityByEachDay[PRO_WCCapacityTable.CCNID_FLD] = SystemProperty.CCNID;
									drowWCCapacityByEachDay[PRO_WCCapacityTable.WORKCENTERID_FLD] = intWorkCenterID;
									drowWCCapacityByEachDay[MST_WorkingDayDetailTable.OFFDAY_FLD] = false;
									drowWCCapacityByEachDay[DAY_FLD] = monthCalendar.SelectionStart;
									dtbWCCapacityByEachDay.Rows.Add(drowWCCapacityByEachDay);
								}
							}
							dtbWCCapacity.DefaultView.Sort = PRO_WCCapacityTable.BEGINDATE_FLD;
							//Group some rows which have the same information
						}
						#endregion	
					} 
					else
					{
						if (CheckDateInPeriodOfCycle(monthCalendar.SelectionStart))
						{
							if (chkWorkingday.Checked)
							{
								DataRow drowNew = null;
								//Add a new row
								drowNew = dtbWCCapacity.NewRow();
								//Generate a fake WCCapacityId 
								drowNew[PRO_WCCapacityTable.WCCAPACITYID_FLD] = --intFakeWCCapacityID;
								drowNew[PRO_WCCapacityTable.BEGINDATE_FLD] = monthCalendar.SelectionStart;
								drowNew[PRO_WCCapacityTable.ENDDATE_FLD] = monthCalendar.SelectionStart;
								drowNew[PRO_WCCapacityTable.FACTOR_FLD] = numFactor.Value;
								drowNew[PRO_WCCapacityTable.WCTYPE_FLD] = cboType.SelectedIndex;
								drowNew[PRO_WCCapacityTable.CREWSIZE_FLD] = numCrewSize.Value;
								drowNew[PRO_WCCapacityTable.MACHINENO_FLD] = numMachine.Value;
								drowNew[PRO_WCCapacityTable.CAPACITY_FLD] = numCapacity.Value;
								drowNew[PRO_WCCapacityTable.CCNID_FLD] = SystemProperty.CCNID;
								drowNew[PRO_WCCapacityTable.WORKCENTERID_FLD] = intWorkCenterID;
								//update ShiftCapacity
								for (int i = 0; i < dgrdShift.RowCount; i++)
								{
									if (dgrdShift[i, SELECT].ToString() == true.ToString())
									{
										DataRow drowShiftCapacity = dtbShiftCapacity.NewRow();
										drowShiftCapacity[PRO_ShiftCapacityTable.WCCAPACITYID_FLD] = intFakeWCCapacityID;
										drowShiftCapacity[PRO_ShiftCapacityTable.SHIFTID_FLD] = dgrdShift[i, PRO_ShiftCapacityTable.SHIFTID_FLD];
										dtbShiftCapacity.Rows.Add(drowShiftCapacity);
									}
								}
								dtbWCCapacity.Rows.Add(drowNew);
								//add a new row to WCCapacity by each day Table
								drowWCCapacityByEachDay = dtbWCCapacityByEachDay.NewRow();
								drowWCCapacityByEachDay[PRO_WCCapacityTable.FACTOR_FLD] = numFactor.Value;
								drowWCCapacityByEachDay[PRO_WCCapacityTable.WCTYPE_FLD] = cboType.SelectedIndex;
								drowWCCapacityByEachDay[PRO_WCCapacityTable.CREWSIZE_FLD] = numCrewSize.Value;
								drowWCCapacityByEachDay[PRO_WCCapacityTable.MACHINENO_FLD] = numMachine.Value;
								drowWCCapacityByEachDay[PRO_WCCapacityTable.CAPACITY_FLD] = numCapacity.Value;
								drowWCCapacityByEachDay[PRO_WCCapacityTable.CCNID_FLD] = SystemProperty.CCNID;
								drowWCCapacityByEachDay[PRO_WCCapacityTable.WORKCENTERID_FLD] = intWorkCenterID;
								drowWCCapacityByEachDay[MST_WorkingDayDetailTable.OFFDAY_FLD] = false;
								drowWCCapacityByEachDay[DAY_FLD] = monthCalendar.SelectionStart;
								dtbWCCapacityByEachDay.Rows.Add(drowWCCapacityByEachDay);	
							}
						}
						//add new
						if (chkWorkingday.Checked)
						{
							DataRow drowNew = null;
							//Add a new row
							drowNew = dtbWCCapacity.NewRow();
							//Generate a fake WCCapacityId 
							drowNew[PRO_WCCapacityTable.WCCAPACITYID_FLD] = --intFakeWCCapacityID;
							drowNew[PRO_WCCapacityTable.BEGINDATE_FLD] = monthCalendar.SelectionStart;
							drowNew[PRO_WCCapacityTable.ENDDATE_FLD] = monthCalendar.SelectionStart;
							drowNew[PRO_WCCapacityTable.FACTOR_FLD] = numFactor.Value;
							drowNew[PRO_WCCapacityTable.WCTYPE_FLD] = cboType.SelectedIndex;
							drowNew[PRO_WCCapacityTable.CREWSIZE_FLD] = numCrewSize.Value;
							drowNew[PRO_WCCapacityTable.MACHINENO_FLD] = numMachine.Value;
							drowNew[PRO_WCCapacityTable.CAPACITY_FLD] = numCapacity.Value;
							drowNew[PRO_WCCapacityTable.CCNID_FLD] = SystemProperty.CCNID;
							drowNew[PRO_WCCapacityTable.WORKCENTERID_FLD] = intWorkCenterID;
							//update ShiftCapacity
							for (int i = 0; i < dgrdShift.RowCount; i++)
							{
								if (dgrdShift[i, SELECT].ToString() == true.ToString())
								{
									DataRow drowShiftCapacity = dtbShiftCapacity.NewRow();
									drowShiftCapacity[PRO_ShiftCapacityTable.WCCAPACITYID_FLD] = intFakeWCCapacityID;
									drowShiftCapacity[PRO_ShiftCapacityTable.SHIFTID_FLD] = dgrdShift[i, PRO_ShiftCapacityTable.SHIFTID_FLD];
									dtbShiftCapacity.Rows.Add(drowShiftCapacity);
								}
							}
							dtbWCCapacity.Rows.Add(drowNew);
							//add a new row to WCCapacity by each day Table
							drowWCCapacityByEachDay = dtbWCCapacityByEachDay.NewRow();
							drowWCCapacityByEachDay[PRO_WCCapacityTable.FACTOR_FLD] = numFactor.Value;
							drowWCCapacityByEachDay[PRO_WCCapacityTable.WCTYPE_FLD] = cboType.SelectedIndex;
							drowWCCapacityByEachDay[PRO_WCCapacityTable.CREWSIZE_FLD] = numCrewSize.Value;
							drowWCCapacityByEachDay[PRO_WCCapacityTable.MACHINENO_FLD] = numMachine.Value;
							drowWCCapacityByEachDay[PRO_WCCapacityTable.CAPACITY_FLD] = numCapacity.Value;
							drowWCCapacityByEachDay[PRO_WCCapacityTable.CCNID_FLD] = SystemProperty.CCNID;
							drowWCCapacityByEachDay[PRO_WCCapacityTable.WORKCENTERID_FLD] = intWorkCenterID;
							drowWCCapacityByEachDay[MST_WorkingDayDetailTable.OFFDAY_FLD] = false;
							drowWCCapacityByEachDay[DAY_FLD] = monthCalendar.SelectionStart;
							dtbWCCapacityByEachDay.Rows.Add(drowWCCapacityByEachDay);
						}
					}
					GroupSameRows();
					GroupSameRows();
					
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
		}
		/// <summary>
		/// Rebuild WCCapacity by each day after a change
		/// </summary>
		/// <author>Trada</author>
		/// <date>Wednesday, January 18 2006</date>
		private void RebuildWCCapacityByEachDay()
		{
			const string METHOD_NAME = THIS + ".RebuildWCCapacityByEachDay()";
			const string SQL_DATETIME_FORMAT  = "yyyy-MM-dd";
			try
			{
				if ((numCapacity.Value != DBNull.Value)
					&& ((decimal) numCapacity.Value != 0))
				{
					bool blnDayExist = false;
					//update WCCapacity by each day Table

					DataRow[] adrowWCCByEachDay = dtbWCCapacityByEachDay.Select(DAY_FLD + " = '" + monthCalendar.SelectionStart.ToString(SQL_DATETIME_FORMAT) + "'");
					for (int i = 0; i < adrowWCCByEachDay.Length; i++)
					{
						//delete shift capacity 
						if (int.Parse(adrowWCCByEachDay[i][PRO_WCCapacityTable.WCCAPACITYID_FLD].ToString()) < 0)
						{
							//delete shiftcapacity
							DataRow[] arrSelectedShift = dtbShiftCapacity.Select(PRO_ShiftCapacityTable.WCCAPACITYID_FLD + " = " + adrowWCCByEachDay[i][PRO_WCCapacityTable.WCCAPACITYID_FLD].ToString());
							for (int p = 0; p < arrSelectedShift.Length; p++)
							{
								arrSelectedShift[p].Delete();
							} 
						}
						adrowWCCByEachDay[i].Delete();
					}
				
					//add new row to WCCapacity by each day Table
					DataRow drowWCCapacityByEachDay = null;
					drowWCCapacityByEachDay = dtbWCCapacityByEachDay.NewRow();
					drowWCCapacityByEachDay[PRO_WCCapacityTable.WCCAPACITYID_FLD] = --intFakeWCCapacityID;
					drowWCCapacityByEachDay[PRO_WCCapacityTable.FACTOR_FLD] = numFactor.Value;
					drowWCCapacityByEachDay[PRO_WCCapacityTable.WCTYPE_FLD] = cboType.SelectedIndex;
					drowWCCapacityByEachDay[PRO_WCCapacityTable.CREWSIZE_FLD] = numCrewSize.Value;
					drowWCCapacityByEachDay[PRO_WCCapacityTable.MACHINENO_FLD] = numMachine.Value;
					drowWCCapacityByEachDay[PRO_WCCapacityTable.CAPACITY_FLD] = numCapacity.Value;
					drowWCCapacityByEachDay[PRO_WCCapacityTable.CCNID_FLD] = SystemProperty.CCNID;
					drowWCCapacityByEachDay[PRO_WCCapacityTable.WORKCENTERID_FLD] = intWorkCenterID;
					drowWCCapacityByEachDay[MST_WorkingDayDetailTable.OFFDAY_FLD] = false;
					drowWCCapacityByEachDay[DAY_FLD] = monthCalendar.SelectionStart;
					dtbWCCapacityByEachDay.Rows.Add(drowWCCapacityByEachDay);
					//update ShiftCapacity
					for (int i = 0; i < dgrdShift.RowCount; i++)
					{
						if (dgrdShift[i, SELECT].ToString() == true.ToString())
						{
							DataRow drowShiftCapacity = dtbShiftCapacity.NewRow();
							drowShiftCapacity[PRO_ShiftCapacityTable.WCCAPACITYID_FLD] = intFakeWCCapacityID;
							//drowShiftCapacity[NEWID_FLD] = drowWCCapacityByEachDay[NEWID_FLD];
							drowShiftCapacity[PRO_ShiftCapacityTable.SHIFTID_FLD] = dgrdShift[i, PRO_ShiftCapacityTable.SHIFTID_FLD];
							dtbShiftCapacity.Rows.Add(drowShiftCapacity);
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
		}
		/// <summary>
		/// btnClose_Click
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Thursday, January 5 2006</date>
		private void btnClose_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}
		/// <summary>
		/// btnOK_Click
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Thursday, January 5 2006</date>
		private void btnOK_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnOK_Click()";
			try
			{
				blnHasError = true;
				if (ValidateData())
				{
					DataRow drwNewRow = null;
					dtbWCCapacityByEachDay.DefaultView.Sort = DAY_FLD;
					dtbWCCapacityToReturn = dtbWCCapacity.Clone();	
					//Sort by day
					DataRow[] adrowWCCapacityByEachDay = dtbWCCapacityByEachDay.Select(string.Empty, DAY_FLD);
					int i = 0;
					while (i < adrowWCCapacityByEachDay.Length)
					{
						int j = i;
						if (adrowWCCapacityByEachDay[j][MST_WorkingDayDetailTable.OFFDAY_FLD].ToString() == false.ToString())
						{
							while ((j < adrowWCCapacityByEachDay.Length - 1) 
								&& (adrowWCCapacityByEachDay[j][MST_WorkingDayDetailTable.OFFDAY_FLD].ToString() == false.ToString())
								&& (((DateTime)adrowWCCapacityByEachDay[j][DAY_FLD]).AddDays(1) ==  ((DateTime)adrowWCCapacityByEachDay[j + 1][DAY_FLD]))
								&& (IsRowsTheSame(adrowWCCapacityByEachDay[j], adrowWCCapacityByEachDay[j + 1])))
							{
								j++;
							}
							drwNewRow = dtbWCCapacityToReturn.NewRow();
							//Set date
							drwNewRow[PRO_WCCapacityTable.BEGINDATE_FLD] = adrowWCCapacityByEachDay[i][DAY_FLD];
							if (adrowWCCapacityByEachDay[j][MST_WorkingDayDetailTable.OFFDAY_FLD].ToString() == false.ToString())
							{
								drwNewRow[PRO_WCCapacityTable.ENDDATE_FLD] = adrowWCCapacityByEachDay[j][DAY_FLD];		
							}
							else
								drwNewRow[PRO_WCCapacityTable.ENDDATE_FLD] = adrowWCCapacityByEachDay[j - 1][DAY_FLD];	
							for (int k = 2; k < dtbWCCapacity.Columns.Count; k++)
							{
								drwNewRow[dtbWCCapacity.Columns[k].ColumnName] = adrowWCCapacityByEachDay[i][dtbWCCapacity.Columns[k].ColumnName];
							}
							dtbWCCapacityToReturn.Rows.Add(drwNewRow);

						}
						i = j + 1;
						//					else
						//					{
						//						i = j + 2;
						//					}
					}
					//Correct ID
					DataRow[] adrowPositiveID = dtbWCCapacityToReturn.Select(PRO_WCCapacityTable.WCCAPACITYID_FLD + " > 0");
					for (int k  = 0; k < adrowPositiveID.Length; k++)
					{
						int intWCCapacityLocalID = int.Parse(adrowPositiveID[k][PRO_WCCapacityTable.WCCAPACITYID_FLD].ToString());
						DataRow[] adrowWCCapacityToReturn = dtbWCCapacityToReturn.Select(PRO_WCCapacityTable.WCCAPACITYID_FLD + " = " + intWCCapacityLocalID.ToString());
						if (adrowWCCapacityToReturn.Length > 1)
						{
							for (int n = 1; n < adrowWCCapacityToReturn.Length; n++)
							{
								adrowWCCapacityToReturn[n][PRO_WCCapacityTable.WCCAPACITYID_FLD] = --intFakeWCCapacityID;
								//clone shiftcapacity
								DataRow[] arrSelectedShift = dtbShiftCapacity.Select(PRO_ShiftCapacityTable.WCCAPACITYID_FLD + " = " + intWCCapacityLocalID);
								for (int p = 0; p < arrSelectedShift.Length; p++)
								{
									DataRow drowShiftCapacity = dtbShiftCapacity.NewRow();
									drowShiftCapacity[PRO_ShiftCapacityTable.WCCAPACITYID_FLD] = intFakeWCCapacityID;
									drowShiftCapacity[PRO_ShiftCapacityTable.SHIFTID_FLD] = arrSelectedShift[p][PRO_ShiftCapacityTable.SHIFTID_FLD];
									dtbShiftCapacity.Rows.Add(drowShiftCapacity);
												
								}
							}
						}
					}
					//Remove WCCapacityID not use
					RemoveWCCapacityID();
					//Remove Deleted row
					DataRow[] adrowWCCapacityOld = dtbWCCapacity.Select(string.Empty, PRO_WCCapacityTable.WCCAPACITYID_FLD);
					if (adrowWCCapacityOld.Length > 0)
					{
						for (int k = 0; k < adrowWCCapacityOld.Length; k++)
						{
							DataRow[] adrowWCCapacityToReturn = dtbWCCapacityToReturn.Select(PRO_WCCapacityTable.WCCAPACITYID_FLD + " = " + adrowWCCapacityOld[k][PRO_WCCapacityTable.WCCAPACITYID_FLD].ToString());
							if(adrowWCCapacityToReturn.Length == 0)
							{
								arlDeletedIDs.Add(adrowWCCapacityOld[k][PRO_WCCapacityTable.WCCAPACITYID_FLD]);
							}
						}
					}
					blnHasError = false;
                    blnIsChange = false;
//					c1TrueDBGrid1.DataSource = dtbWCCapacityToReturn;
					//c1TrueDBGrid1.DataSource = dtbWCCapacityByEachDay;
					this.DialogResult = DialogResult.OK;
					this.Close();
				}
				else
				{
					blnHasError = false;
					blnIsChange = false;
					this.Close();
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
		/// Remove WCCapacityID is not use
		/// </summary>
		/// <author>Trada</author>
		/// <date>Thursday, January 26 2006</date>
		private void RemoveWCCapacityID()
		{
			const string METHOD_NAME = THIS + ".RemoveWCCapacityID()";
			try
			{
				DataRow[] adrowShiftCapacity = dtbShiftCapacity.Select(string.Empty, PRO_WCCapacityTable.WCCAPACITYID_FLD);
				if (adrowShiftCapacity.Length > 0)
				{
					for (int i = 0; i < adrowShiftCapacity.Length; i++)
					{
						DataRow[] adrowWCCapacityToReturn = dtbWCCapacityToReturn.Select(PRO_WCCapacityTable.WCCAPACITYID_FLD + " = " + adrowShiftCapacity[i][PRO_WCCapacityTable.WCCAPACITYID_FLD].ToString());
						if (adrowWCCapacityToReturn.Length == 0)
						{
							adrowShiftCapacity[i].Delete();
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
		}
		/// <summary>
		/// Check two rows are the same
		/// </summary>
		/// <param name="pdrowWCCapacityByEachDayOne"></param>
		/// <param name="pdrowWCCapacityByEachDayTwo"></param>
		/// <returns></returns>
		/// <author>Trada</author>
		/// <date>Friday, January 20 2006</date>
		private bool IsRowsTheSame(DataRow pdrowWCCapacityByEachDayOne, DataRow pdrowWCCapacityByEachDayTwo)
		{
			const string METHOD_NAME = THIS + ".IsRowsTheSame()";
			try
			{
				if ((int.Parse(pdrowWCCapacityByEachDayOne[PRO_WCCapacityTable.WCTYPE_FLD].ToString()) == int.Parse(pdrowWCCapacityByEachDayTwo[PRO_WCCapacityTable.WCTYPE_FLD].ToString()))
					&& (decimal.Parse(pdrowWCCapacityByEachDayOne[PRO_WCCapacityTable.CAPACITY_FLD].ToString()) == decimal.Parse(pdrowWCCapacityByEachDayTwo[PRO_WCCapacityTable.CAPACITY_FLD].ToString()))
					&& (decimal.Parse(pdrowWCCapacityByEachDayOne[PRO_WCCapacityTable.FACTOR_FLD].ToString()) == decimal.Parse(pdrowWCCapacityByEachDayTwo[PRO_WCCapacityTable.FACTOR_FLD].ToString()))
					&& (int.Parse(pdrowWCCapacityByEachDayOne[PRO_WCCapacityTable.CCNID_FLD].ToString()) == int.Parse(pdrowWCCapacityByEachDayTwo[PRO_WCCapacityTable.CCNID_FLD].ToString()))
					&& (int.Parse(pdrowWCCapacityByEachDayOne[PRO_WCCapacityTable.WORKCENTERID_FLD].ToString()) == int.Parse(pdrowWCCapacityByEachDayTwo[PRO_WCCapacityTable.WORKCENTERID_FLD].ToString())))
				{
					if ((pdrowWCCapacityByEachDayOne[PRO_WCCapacityTable.MACHINENO_FLD] == DBNull.Value)
						&& (pdrowWCCapacityByEachDayTwo[PRO_WCCapacityTable.MACHINENO_FLD] == DBNull.Value))
					{
						if ((pdrowWCCapacityByEachDayOne[PRO_WCCapacityTable.CREWSIZE_FLD] == DBNull.Value)
							&& (pdrowWCCapacityByEachDayTwo[PRO_WCCapacityTable.CREWSIZE_FLD] == DBNull.Value))
						{
							return true;
						}
						else
						{
							if ((pdrowWCCapacityByEachDayOne[PRO_WCCapacityTable.CREWSIZE_FLD] != DBNull.Value)
								&& (pdrowWCCapacityByEachDayTwo[PRO_WCCapacityTable.CREWSIZE_FLD] != DBNull.Value))
							{
								if ((decimal.Parse(pdrowWCCapacityByEachDayOne[PRO_WCCapacityTable.CREWSIZE_FLD].ToString()) == decimal.Parse(pdrowWCCapacityByEachDayTwo[PRO_WCCapacityTable.CREWSIZE_FLD].ToString())))
								{
									return true;
								}
							}
						}
					}
					else
					{
						if ((pdrowWCCapacityByEachDayOne[PRO_WCCapacityTable.MACHINENO_FLD] != DBNull.Value)
							&& (pdrowWCCapacityByEachDayTwo[PRO_WCCapacityTable.MACHINENO_FLD] != DBNull.Value))
						{
							if ((int.Parse(pdrowWCCapacityByEachDayOne[PRO_WCCapacityTable.MACHINENO_FLD].ToString()) == int.Parse(pdrowWCCapacityByEachDayTwo[PRO_WCCapacityTable.MACHINENO_FLD].ToString())))
							{
								if ((pdrowWCCapacityByEachDayOne[PRO_WCCapacityTable.CREWSIZE_FLD] == DBNull.Value)
									&& (pdrowWCCapacityByEachDayTwo[PRO_WCCapacityTable.CREWSIZE_FLD] == DBNull.Value))
								{
									return true;
								}
								else
								{
									if ((pdrowWCCapacityByEachDayOne[PRO_WCCapacityTable.CREWSIZE_FLD] != DBNull.Value)
										&& (pdrowWCCapacityByEachDayTwo[PRO_WCCapacityTable.CREWSIZE_FLD] != DBNull.Value))
									{
										if ((decimal.Parse(pdrowWCCapacityByEachDayOne[PRO_WCCapacityTable.CREWSIZE_FLD].ToString()) == decimal.Parse(pdrowWCCapacityByEachDayTwo[PRO_WCCapacityTable.CREWSIZE_FLD].ToString())))
										{
											return true;
										}
									}
								}
													
							}
						}
					}
				}
				return false;
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
		/// GroupSameRows
		/// </summary>
		/// <author>Trada</author>
		/// <date>Wednesday, January 11 2006</date>
		private void GroupSameRows()
		{
			const string METHOD_NAME = THIS + ".GroupSameRows()";
			try
			{
				ArrayList arrListWCCapacityIDToDelete = new ArrayList();
				for (int i = 0; i < dtbWCCapacity.Rows.Count; i++)
				{
					for (int j = i + 1; j < dtbWCCapacity.Rows.Count; j++)
					{
						//Check rows have the same information
						bool blnIdentical = false;
						if ((int.Parse(dtbWCCapacity.Rows[i][PRO_WCCapacityTable.WCTYPE_FLD].ToString()) == int.Parse(dtbWCCapacity.Rows[j][PRO_WCCapacityTable.WCTYPE_FLD].ToString()))
							&& (decimal.Parse(dtbWCCapacity.Rows[i][PRO_WCCapacityTable.CAPACITY_FLD].ToString()) == decimal.Parse(dtbWCCapacity.Rows[j][PRO_WCCapacityTable.CAPACITY_FLD].ToString()))
							&& (decimal.Parse(dtbWCCapacity.Rows[i][PRO_WCCapacityTable.FACTOR_FLD].ToString()) == decimal.Parse(dtbWCCapacity.Rows[j][PRO_WCCapacityTable.FACTOR_FLD].ToString()))
							&& (int.Parse(dtbWCCapacity.Rows[i][PRO_WCCapacityTable.CCNID_FLD].ToString()) == int.Parse(dtbWCCapacity.Rows[j][PRO_WCCapacityTable.CCNID_FLD].ToString()))
							&& (int.Parse(dtbWCCapacity.Rows[i][PRO_WCCapacityTable.WORKCENTERID_FLD].ToString()) == int.Parse(dtbWCCapacity.Rows[j][PRO_WCCapacityTable.WORKCENTERID_FLD].ToString())))
						{
							if ((dtbWCCapacity.Rows[i][PRO_WCCapacityTable.MACHINENO_FLD] == DBNull.Value)
								&& (dtbWCCapacity.Rows[j][PRO_WCCapacityTable.MACHINENO_FLD] == DBNull.Value))
							{
								if ((dtbWCCapacity.Rows[i][PRO_WCCapacityTable.CREWSIZE_FLD] == DBNull.Value)
									&& (dtbWCCapacity.Rows[j][PRO_WCCapacityTable.CREWSIZE_FLD] == DBNull.Value))
								{
									blnIdentical = true;
								}
								else
								{
									if ((dtbWCCapacity.Rows[i][PRO_WCCapacityTable.CREWSIZE_FLD] != DBNull.Value)
										&& (dtbWCCapacity.Rows[j][PRO_WCCapacityTable.CREWSIZE_FLD] != DBNull.Value))
									{
										if ((decimal.Parse(dtbWCCapacity.Rows[i][PRO_WCCapacityTable.CREWSIZE_FLD].ToString()) == decimal.Parse(dtbWCCapacity.Rows[j][PRO_WCCapacityTable.CREWSIZE_FLD].ToString())))
										{
											blnIdentical = true;
										}
									}
								}
							}
							else
							{
								if ((dtbWCCapacity.Rows[i][PRO_WCCapacityTable.MACHINENO_FLD] != DBNull.Value)
									&& (dtbWCCapacity.Rows[j][PRO_WCCapacityTable.MACHINENO_FLD] != DBNull.Value))
								{
									if ((int.Parse(dtbWCCapacity.Rows[i][PRO_WCCapacityTable.MACHINENO_FLD].ToString()) == int.Parse(dtbWCCapacity.Rows[j][PRO_WCCapacityTable.MACHINENO_FLD].ToString())))
									{
										if ((dtbWCCapacity.Rows[i][PRO_WCCapacityTable.CREWSIZE_FLD] == DBNull.Value)
											&& (dtbWCCapacity.Rows[j][PRO_WCCapacityTable.CREWSIZE_FLD] == DBNull.Value))
										{
											blnIdentical = true;
										}
										else
										{
											if ((dtbWCCapacity.Rows[i][PRO_WCCapacityTable.CREWSIZE_FLD] != DBNull.Value)
												&& (dtbWCCapacity.Rows[j][PRO_WCCapacityTable.CREWSIZE_FLD] != DBNull.Value))
											{
												if ((decimal.Parse(dtbWCCapacity.Rows[i][PRO_WCCapacityTable.CREWSIZE_FLD].ToString()) == decimal.Parse(dtbWCCapacity.Rows[j][PRO_WCCapacityTable.CREWSIZE_FLD].ToString())))
												{
													blnIdentical = true;
												}
											}
										}
													
									}
								}
							}
						}
						//Two rows are the same
						if (blnIdentical)
						{
							if (GetDateOnly((DateTime)dtbWCCapacity.Rows[i][PRO_WCCapacityTable.ENDDATE_FLD]).ToShortDateString() == GetDateOnly((DateTime) dtbWCCapacity.Rows[j][PRO_WCCapacityTable.BEGINDATE_FLD]).ToShortDateString())
							{
								dtbWCCapacity.Rows[i][PRO_WCCapacityTable.ENDDATE_FLD] = dtbWCCapacity.Rows[j][PRO_WCCapacityTable.ENDDATE_FLD];
								//Save ID of the second row
								if (arrListWCCapacityIDToDelete.Count > 0)
								{
									if (int.Parse(arrListWCCapacityIDToDelete[arrListWCCapacityIDToDelete.Count-1].ToString()) != int.Parse(dtbWCCapacity.Rows[j][PRO_WCCapacityTable.WCCAPACITYID_FLD].ToString()))
									{
										arrListWCCapacityIDToDelete.Add(int.Parse(dtbWCCapacity.Rows[j][PRO_WCCapacityTable.WCCAPACITYID_FLD].ToString()));
									}
								}
								else
								{
									arrListWCCapacityIDToDelete.Add(int.Parse(dtbWCCapacity.Rows[j][PRO_WCCapacityTable.WCCAPACITYID_FLD].ToString()));
								}
							}

							if (GetDateOnly((DateTime)dtbWCCapacity.Rows[i][PRO_WCCapacityTable.BEGINDATE_FLD]).ToShortDateString() == GetDateOnly((DateTime) dtbWCCapacity.Rows[j][PRO_WCCapacityTable.ENDDATE_FLD]).ToShortDateString())
							{
								dtbWCCapacity.Rows[i][PRO_WCCapacityTable.BEGINDATE_FLD] = dtbWCCapacity.Rows[j][PRO_WCCapacityTable.BEGINDATE_FLD];
								//Save ID of the second row
								if (arrListWCCapacityIDToDelete.Count > 0)
								{
									if (int.Parse(arrListWCCapacityIDToDelete[arrListWCCapacityIDToDelete.Count-1].ToString()) != int.Parse(dtbWCCapacity.Rows[j][PRO_WCCapacityTable.WCCAPACITYID_FLD].ToString()))
									{
										arrListWCCapacityIDToDelete.Add(int.Parse(dtbWCCapacity.Rows[j][PRO_WCCapacityTable.WCCAPACITYID_FLD].ToString()));
									}
								}
								else
								{
									arrListWCCapacityIDToDelete.Add(int.Parse(dtbWCCapacity.Rows[j][PRO_WCCapacityTable.WCCAPACITYID_FLD].ToString()));
								}
							}

							if (GetDateOnly((DateTime)dtbWCCapacity.Rows[i][PRO_WCCapacityTable.ENDDATE_FLD]).AddDays(1).ToShortDateString() == GetDateOnly((DateTime) dtbWCCapacity.Rows[j][PRO_WCCapacityTable.BEGINDATE_FLD]).ToShortDateString())
							{
								dtbWCCapacity.Rows[i][PRO_WCCapacityTable.ENDDATE_FLD] = dtbWCCapacity.Rows[j][PRO_WCCapacityTable.ENDDATE_FLD];
								//Save ID of the second row
								if (arrListWCCapacityIDToDelete.Count > 0)
								{
									if (int.Parse(arrListWCCapacityIDToDelete[arrListWCCapacityIDToDelete.Count-1].ToString()) != int.Parse(dtbWCCapacity.Rows[j][PRO_WCCapacityTable.WCCAPACITYID_FLD].ToString()))
									{
										arrListWCCapacityIDToDelete.Add(int.Parse(dtbWCCapacity.Rows[j][PRO_WCCapacityTable.WCCAPACITYID_FLD].ToString()));
									}
								}
								else
								{
									arrListWCCapacityIDToDelete.Add(int.Parse(dtbWCCapacity.Rows[j][PRO_WCCapacityTable.WCCAPACITYID_FLD].ToString()));
								}
							}
							else
							{
								if (GetDateOnly((DateTime)dtbWCCapacity.Rows[i][PRO_WCCapacityTable.BEGINDATE_FLD]).AddDays(-1).ToShortDateString() == GetDateOnly((DateTime) dtbWCCapacity.Rows[j][PRO_WCCapacityTable.ENDDATE_FLD]).ToShortDateString())
								{
									dtbWCCapacity.Rows[i][PRO_WCCapacityTable.BEGINDATE_FLD] = dtbWCCapacity.Rows[j][PRO_WCCapacityTable.BEGINDATE_FLD];
									//Save ID of the second row
									if (arrListWCCapacityIDToDelete.Count > 0)
									{
										if (int.Parse(arrListWCCapacityIDToDelete[arrListWCCapacityIDToDelete.Count-1].ToString()) != int.Parse(dtbWCCapacity.Rows[j][PRO_WCCapacityTable.WCCAPACITYID_FLD].ToString()))
										{
											arrListWCCapacityIDToDelete.Add(int.Parse(dtbWCCapacity.Rows[j][PRO_WCCapacityTable.WCCAPACITYID_FLD].ToString()));
										}
									}
									else
									{
										arrListWCCapacityIDToDelete.Add(int.Parse(dtbWCCapacity.Rows[j][PRO_WCCapacityTable.WCCAPACITYID_FLD].ToString()));
									}
								}
							}	
						}
					}
				}
				//Delete the second rows 
				int intCount = dtbWCCapacity.Rows.Count;
				for (int j = 0; j < intCount; j++)
				{
					if (arrListWCCapacityIDToDelete.Count > 0)
					{
						for (int i = 0; i < arrListWCCapacityIDToDelete.Count; i++)
						{
							try
							{
								if (dtbWCCapacity.Rows[j][PRO_WCCapacityTable.WCCAPACITYID_FLD].ToString() == arrListWCCapacityIDToDelete[i].ToString())
								{
									dtbWCCapacity.Rows[j].Delete();
								}
							}
							catch{}
							DataRow[] arrRows = dtbShiftCapacity.Select(PRO_ShiftCapacityTable.WCCAPACITYID_FLD + " = " + arrListWCCapacityIDToDelete[i].ToString());
							for(int k =0; k < arrRows.Length; k++)
							{					
								arrRows[k].Delete();
							}
							arlDeletedIDs.Add(arrListWCCapacityIDToDelete[i]);
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
		}
		/// <summary>
		/// CalculateCapacity
		/// </summary>
		/// <author>Trada</author>
		/// <date>Tuesday, January 10 2006</date>
		private void CalculateCapacity()
		{
			const string METHOD_NAME = THIS +  ".CalculateCapacity()";
			const string ZERO_STRING = "0";
			const string DELIMITER_CHAR = ",";
			
			try
			{
				WorkCenterCapacityBO boWorkCenterCapacity = new WorkCenterCapacityBO();
				//get selected shifts of WCCapacity row
				string strCheckedShiftIDs = ZERO_STRING;
				for (int i = 0; i < dgrdShift.RowCount; i++)
				{
					if (dgrdShift[i, SELECT].ToString() == true.ToString())
					{
						strCheckedShiftIDs += DELIMITER_CHAR + dgrdShift[i, PRO_ShiftTable.SHIFTID_FLD].ToString();
					}
				}
				//Get working time based-on selected shifts
				decimal decTotalWorkingTime = (decimal)boWorkCenterCapacity.GetTotalActualWorkingTime(strCheckedShiftIDs);	
				//parse values
				decimal decOptionValue = decimal.Zero;				
				decimal decFactor = 0;
				if ((numFactor.Value != null)&&(numFactor.Value != DBNull.Value))
				//if (numFactor.Value != DBNull.Value)
				{
					try
					{
						decFactor = Decimal.Parse(numFactor.Value.ToString());
					}
					catch
					{
						
					}
				}
				
				if(cboType.SelectedIndex == (int)WCType.Labor)
				{
					if ((numCrewSize.Value != null)&&(numCrewSize.Value != DBNull.Value))
					{
						decOptionValue = decimal.Parse(numCrewSize.Value.ToString());
					}
				}
				else
				{
					if ((numMachine.Value != null)&&(numMachine.Value != DBNull.Value))
					{
						decOptionValue = decimal.Parse(numMachine.Value.ToString());
					}					
				}
				numCapacity.Value = Math.Round((decTotalWorkingTime * decOptionValue * decFactor * (decimal)0.01), 2); 
				
			}
			catch(PCSException ex)
			{
				throw new PCSException(ex.mCode, METHOD_NAME, ex);
			}
			catch(Exception ex)
			{
				throw new Exception(ex.Message, ex);
			}			
		}
		/// <summary>
		/// ValidateData
		/// </summary>
		/// <author>Trada</author>
		/// <date>Tuesday, January 10 2006</date>
		private bool ValidateData()
		{
			const string METHOD_NAME = THIS + ".ValidateData()";
			try
			{
				//				if (!CheckDateInPeriod(monthCalendar.SelectionStart))
				//				{
				//					return false;
				//				}
				if (chkWorkingday.Checked)
				{
					if (numCapacity.Value == DBNull.Value)
					{
						PCSMessageBox.Show(ErrorCode.MESSAGE_PLZ_INSERT_DETAIL_INFORATION_TO_ADJUSTMENT_DCP, MessageBoxIcon.Warning);					
						return false;
					}
					else
					{
						if (decimal.Parse(numCapacity.Value.ToString()) == 0)
						{
							PCSMessageBox.Show(ErrorCode.MESSAGE_PLZ_INSERT_DETAIL_INFORATION_TO_ADJUSTMENT_DCP, MessageBoxIcon.Warning);					
							return false;
						}
					}
				}
				else
				{
					DataRow[] adrowWCCapacityByEachDay = dtbWCCapacityByEachDay.Select(DAY_FLD + " = '" + monthCalendar.SelectionStart.ToShortDateString() + "'"); 
					if (adrowWCCapacityByEachDay.Length == 0)
					{
						return false;
					}
					else
						return true;
				}
				
				//Check Shift
				for (int i = 0; i < dgrdShift.RowCount; i++)
				{
					if (dgrdShift[i, SELECT].ToString() == true.ToString())
					{
						return true;
					}
				}
				PCSMessageBox.Show(ErrorCode.MESSAGE_WCC_PLEASE_SELECT_SHIFT, MessageBoxIcon.Warning);					
				dgrdShift.Focus();
				return false;

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
		/// Lock Grid
		/// </summary>
		/// <param name="pblnLock"></param>
		/// <author>Trada</author>
		/// <date>Monday, January 9 2006</date>
		private void LockGird(bool pblnLock)
		{
			const string METHOD_NAME = THIS + ".LockGird()";
			try
			{
				for (int i = 0; i < dgrdShift.Columns.Count; i++)
				{
					dgrdShift.Splits[0].DisplayColumns[i].Locked = pblnLock;
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
		}
		private void btnHelp_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".FillDataToControls()";
			try
			{
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
		/// DCPAdjustment_KeyDown
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Thursday, January 5 2006</date>
		private void DCPAdjustment_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			const string METHOD_NAME = THIS + ".DCPAdjustment_KeyDown()";
			try
			{
				
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
		/// Fill Data From DataRow To Controls
		/// </summary>
		/// <param name="pdrowWCCapacity"></param>
		/// <author>Trada</author>
		/// <date>Thursday, January 5 2006</date>
		private void FillDataToControls(DataRow pdrowWCCapacity)

		{
			const string METHOD_NAME = THIS + ".FillDataToControls()";
			try
			{	
				
				//Load Shift 
				DCPAdjustmentBO boDCPAdjustment = new DCPAdjustmentBO();
				
				int intWCCapacityID = 0;
				if ((pdrowWCCapacity[PRO_WCCapacityTable.WCCAPACITYID_FLD] != null)
					&& (pdrowWCCapacity[PRO_WCCapacityTable.WCCAPACITYID_FLD] != DBNull.Value)
					&& (pdrowWCCapacity[PRO_WCCapacityTable.WCCAPACITYID_FLD].ToString() != string.Empty))
				{
					intWCCapacityID = int.Parse(pdrowWCCapacity[PRO_WCCapacityTable.WCCAPACITYID_FLD].ToString());
				}
				//DataTable dtbShiftCapacity = new DataTable();
				//				dtbShiftCapacity = boDCPAdjustment.GetShiftByWCCapacity(intWCCapacityID);
				//				dtbShiftCapacity.Columns.Add(NEWID_FLD, typeof(int));
				
				//Reset all check box
				ResetAllCheckbox();
				DataRow[] arrShiftCapacity = dtbShiftCapacity.Select(PRO_WCCapacityTable.WCCAPACITYID_FLD + " = " + pdrowWCCapacity[PRO_WCCapacityTable.WCCAPACITYID_FLD].ToString());
				//Check box
				foreach (DataRow drow in arrShiftCapacity)
				{
					for (int i = 0; i < dstShift.Tables[0].Rows.Count; i++)
					{
						if (drow[PRO_ShiftTable.SHIFTID_FLD].ToString() == dstShift.Tables[0].Rows[i][PRO_ShiftTable.SHIFTID_FLD].ToString())
						{
							dgrdShift[i, SELECT] = true;
						}
					}
				}
				dgrdShift.Splits[0].DisplayColumns[PRO_ShiftTable.SHIFTDESC_FLD].Locked = true;
				numCrewSize.Value = pdrowWCCapacity[PRO_WCCapacityTable.CREWSIZE_FLD];
				numFactor.Value = pdrowWCCapacity[PRO_WCCapacityTable.FACTOR_FLD];
				numMachine.Value = pdrowWCCapacity[PRO_WCCapacityTable.MACHINENO_FLD];
				numCapacity.Value = decimal.Parse(pdrowWCCapacity[PRO_WCCapacityTable.CAPACITY_FLD].ToString());
				//numCapacity.Value = 1.00000;
				cboType.SelectedIndex = int.Parse(pdrowWCCapacity[PRO_WCCapacityTable.WCTYPE_FLD].ToString());
				strType = cboType.Text.Trim();
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
		/// ResetAllCheckbox
		/// </summary>
		/// <author>Trada</author>
		/// <date>Friday, January 6 2006</date>
		private void ResetAllCheckbox()
		{
			const string METHOD_NAME = THIS + ".ResetAllCheckbox()";
			try
			{	
				for (int j = 0; j < dgrdShift.RowCount; j++)
				{
					dgrdShift[j, SELECT] = false;
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
		}
		/// <summary>
		/// Check if the selected date is in period of this work center
		/// </summary>
		/// <param name="pdtmSelectedDate"></param>
		/// <author>Trada</author>
		/// <date>Monday, January 9 2006</date>
		private bool CheckDateInPeriod(DateTime pdtmSelectedDate)
		{
			const string METHOD_NAME = THIS + ".CheckDateInPeriod()";
			try
			{	
				if ((pdtmSelectedDate <= dtmMaxEndDateCycle)
					&& (pdtmSelectedDate >= dtmMinBeginDateCycle))
				{
					return true;
				}
				else
					return false;
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
		/// Check if the selected date is in period of selected cycle of this work center
		/// </summary>
		/// <param name="pdtmSelectedDate"></param>
		/// <returns></returns>
		/// <author>Trada</author>
		/// <date>Wednesday, January 11 2006</date>
		private bool CheckDateInPeriodOfCycle(DateTime pdtmSelectedDate)
		{
			const string METHOD_NAME = THIS + ".CheckDateInPeriodOfCycle()";
			try
			{	
				if ((pdtmSelectedDate <= dtmMaxCycle)
					&& (pdtmSelectedDate >= dtmMinCycle))
				{
					if ((pdtmSelectedDate > dtmMaxEndDateCycle) || (pdtmSelectedDate < dtmMinBeginDateCycle))
					{
						return true;
					}
				}
				return false;
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
		/// DisableSomeControls
		/// </summary>
		/// <param name="pblnDisable"></param>
		/// <author>Trada</author>
		/// <date>Thursday, January 5 2006</date>
		private void DisableSomeControls(bool pblnDisable)
		{
			const string METHOD_NAME = THIS + ".DisableSomeControls()";
			try
			{
				numCapacity.Value = null;
				numCrewSize.Value = null;
				numFactor.Value = null;
				numMachine.Value = null;
				cboType.SelectedItem = null;
				if (pblnDisable) //Clear and disable some controls
				{
					numCrewSize.Enabled = false;
					numFactor.Enabled = false;
					numMachine.Enabled = false;
					cboType.SelectedValue = null;
					cboType.Enabled = false;
					LockGird(true);
					//Reset all check box
					for (int i = 0; i < dgrdShift.RowCount; i++)
					{
						dgrdShift[i, SELECT] = false;
					}
				}
				else
				{
					numCrewSize.Enabled = true;
					numFactor.Enabled = true;
					numMachine.Enabled = true;
					cboType.Enabled = true;
					LockGird(false);
					//chkWorkingday.Checked = true;
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
		}
		/// <summary>
		/// InitFormWithDate
		/// </summary>
		/// <param name="pdtmSelectedDate"></param>
		/// <author>Trada</author>
		/// <date>Friday, January 6 2006</date>
		private void InitFormWithDate(DateTime pdtmSelectedDate)
		{
			const string METHOD_NAME = THIS + ".InitFormWithDate()";
			try
			{
				blnIsWorkingDay = false;
				//				if ((dtmMinBeginDate <= pdtmSelectedDate)
				//					&& (dtmMaxEndDate) >= pdtmSelectedDate)
				//				{
				//Check if selected day is working day
				foreach (DataRow drow in dtbWCCapacityByEachDay.Rows)
				{
					if (GetDateOnly((DateTime)drow[DAY_FLD]) == GetDateOnly(pdtmSelectedDate))
					{
						if (drow[MST_WorkingDayDetailTable.OFFDAY_FLD].ToString() == true.ToString())
						{
							//Clear and disable some controls
							DisableSomeControls(true);
							chkWorkingday.Checked = false;
							numCapacity.Value = null;
							return;
						}
						blnIsWorkingDay = true;
						DisableSomeControls(false);
						chkWorkingday.Checked = true;
						drow[MST_WorkingDayDetailTable.OFFDAY_FLD] = false;
						//Fill data to controls
						FillDataToControls(drow);
					}
				}
				if (!blnIsWorkingDay)
				{
					//Clear and disable some controls
					DisableSomeControls(true);
					chkWorkingday.Checked = false;
					numCapacity.Value = null;
				}
				//				}
				//				else //Se update code khi quan tam den Cycle.
				//				{
				//					DisableSomeControls(true);
				//					chkWorkingday.Checked = false;
				//				}
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
		/// monthCalendar_DateChanged
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Thursday, January 5 2006</date>
		private void monthCalendar_DateChanged(object sender, System.Windows.Forms.DateRangeEventArgs e)
		{
			const string METHOD_NAME = THIS + ".monthCalendar_DateChanged()";
			try
			{
//				if (e.Start != dtmSelectedDate)
//				{
					if (ValidateData())
					{
						dtmSelectedDate = e.Start;
						InitFormWithDate(dtmSelectedDate);
					}
//				}
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

		private void monthCalendar_DateSelected(object sender, System.Windows.Forms.DateRangeEventArgs e)
		{
		}
		/// <summary>
		/// cboType_SelectedIndexChanged
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Monday, January 9 2006</date>
		private void cboType_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (cboType.Text != string.Empty
				&& cboType.Text != strType)
			{
				CalculateCapacity();
				blnIsChange = true;
				strType = cboType.Text.Trim();

			}
		}
		/// <summary>
		/// numFactor_Leave
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Tuesday, January 10 2006</date>
		private void numFactor_Leave(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".numFactor_Leave()";
			try
			{
				if (((C1NumericEdit)sender).Modified)
				{
					blnIsChange = true;
					//CalculateCapacity();
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
		/// dgrdShift_AfterColUpdate
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Tuesday, January 10 2006</date>
		private void dgrdShift_AfterColUpdate(object sender, C1.Win.C1TrueDBGrid.ColEventArgs e)
		{
			const string METHOD_NAME = THIS + ".dgrdShift_AfterColUpdate()";
			try
			{
				//Check if total working time exceed 24 hours
				string strCheckedShiftIDs = "0";
				for(int i =0; i< dgrdShift.RowCount; i++)
				{
					if(dgrdShift[i, SELECT].Equals(true))
					{
						strCheckedShiftIDs += Constants.COMMA + dgrdShift[i, PRO_ShiftTable.SHIFTID_FLD].ToString();
					}
				}
				WorkCenterCapacityBO boWCCapacity = new WorkCenterCapacityBO();	
				if(boWCCapacity.GetTotalWorkingTime(strCheckedShiftIDs) > 24 * 60)
				{
					if(dgrdShift.Bookmark >=0)
					{
						dgrdShift[dgrdShift.Bookmark, SELECT] = false;						
					}
					PCSMessageBox.Show(ErrorCode.MESSAGE_DCP_SHIFTPATTERN_WORKTIME_MUSTBE_SMALLER_24, MessageBoxIcon.Exclamation);
					dgrdShift.Focus();
					return;
				}

				CalculateCapacity();
				blnIsChange = true;
				//RebuildWCCapacityByEachDay();
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

		private void cboType_TextChanged(object sender, System.EventArgs e)
		{
			
		}
		/// <summary>
		/// numCapacity_ValueChanged
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Wednesday, January 18 2006</date>
		private void numCapacity_ValueChanged(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".numCapacity_ValueChanged()";
			try
			{
				//RebuildWCCapacityByEachDay();
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

		private void numFactor_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".dgrdShift_AfterColUpdate()";
			try
			{
				try
				{
					if (blnIsChange)
					{
						CalculateCapacity();
						RebuildWCCapacityByEachDay();
					}
				}
				catch
				{
					e.Cancel = true;
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

		private void dgrdShift_Leave(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".dgrdShift_AfterColUpdate()";
			try
			{
				try
				{
					if (blnIsChange)
					{
						RebuildWCCapacityByEachDay();
					}
				}
				catch
				{
					
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

		private void chkWorkingday_CheckedChanged(object sender, System.EventArgs e)
		{
		
		}
		/// <summary>
		/// chkWorkingday_Click
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Tuesday, January 24 2006</date>
		private void chkWorkingday_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".chkWorkingday_Click()";
			try
			{
				bool blnExist = false;
				foreach (DataRow drow in dtbWCCapacityByEachDay.Rows)
				{
					if (GetDateOnly((DateTime)drow[DAY_FLD]) == GetDateOnly(monthCalendar.SelectionStart))
					{
						blnExist = true;
						if (!chkWorkingday.Checked)
						{
							//Clear and disable some controls
							DisableSomeControls(true);
							drow[MST_WorkingDayDetailTable.OFFDAY_FLD] = true;
							numCapacity.Value = null;
						}
						else
						{
							DisableSomeControls(false);
							drow[MST_WorkingDayDetailTable.OFFDAY_FLD] = false;
							//Fill data to controls
							FillDataToControls(drow);
						}
					}
					
				}
				if (!blnExist) //add new
				{
					if (chkWorkingday.Checked)
					{
				
						DataRow drowWCCapacityByEachDay = null;
						drowWCCapacityByEachDay = dtbWCCapacityByEachDay.NewRow();
						DisableSomeControls(false);
						drowWCCapacityByEachDay[MST_WorkingDayDetailTable.OFFDAY_FLD] = false;
						drowWCCapacityByEachDay[PRO_WCCapacityTable.WCCAPACITYID_FLD] = --intFakeWCCapacityID;
						drowWCCapacityByEachDay[PRO_WCCapacityTable.FACTOR_FLD] = numFactor.Value;
						drowWCCapacityByEachDay[PRO_WCCapacityTable.WCTYPE_FLD] = cboType.SelectedIndex;
						drowWCCapacityByEachDay[PRO_WCCapacityTable.CREWSIZE_FLD] = numCrewSize.Value;
						drowWCCapacityByEachDay[PRO_WCCapacityTable.MACHINENO_FLD] = numMachine.Value;
						drowWCCapacityByEachDay[PRO_WCCapacityTable.CAPACITY_FLD] = numCapacity.Value;
						drowWCCapacityByEachDay[PRO_WCCapacityTable.CCNID_FLD] = SystemProperty.CCNID;
						drowWCCapacityByEachDay[PRO_WCCapacityTable.WORKCENTERID_FLD] = intWorkCenterID;
						drowWCCapacityByEachDay[DAY_FLD] = monthCalendar.SelectionStart;
						dtbWCCapacityByEachDay.Rows.Add(drowWCCapacityByEachDay);
						//update ShiftCapacity
						for (int i = 0; i < dgrdShift.RowCount; i++)
						{
							if (dgrdShift[i, SELECT].ToString() == true.ToString())
							{
								DataRow drowShiftCapacity = dtbShiftCapacity.NewRow();
								drowShiftCapacity[PRO_ShiftCapacityTable.WCCAPACITYID_FLD] = intFakeWCCapacityID;
								//drowShiftCapacity[NEWID_FLD] = drowWCCapacityByEachDay[NEWID_FLD];
								drowShiftCapacity[PRO_ShiftCapacityTable.SHIFTID_FLD] = dgrdShift[i, PRO_ShiftCapacityTable.SHIFTID_FLD];
								dtbShiftCapacity.Rows.Add(drowShiftCapacity);
							}
						}	
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

		private void DCPAdjustment_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".chkWorkingday_Click()";
			try
			{
				if (blnIsChange)
				{
					DialogResult confirmDialog = PCSMessageBox.Show(ErrorCode.MESSAGE_QUESTION_STORE_INTO_DATABASE, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
					switch (confirmDialog)
					{
						case DialogResult.Yes:
							//Save before exit
							btnOK_Click(null, new EventArgs());
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

		private void numFactor_ValidationError(object sender, C1.Win.C1Input.ValidationErrorEventArgs e)
		{
			
		}
		/// <summary>
		/// dgrdShift_BeforeColEdit
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Monday, Mar 6 2006</date>
		private void dgrdShift_BeforeColEdit(object sender, C1.Win.C1TrueDBGrid.BeforeColEditEventArgs e)
		{
			const string METHOD_NAME = THIS + ".dgrdShift_BeforeColEdit()";
			try
			{
				if(int.Parse(dgrdShift.Columns[SHIFT_PATTERN].Text) <= 0)
				{
					PCSMessageBox.Show(ErrorCode.MESSAGE_SHIFT_DOES_NOT_HAS_SHIFT_PATTERN, MessageBoxIcon.Exclamation);
					e.Cancel = true;
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
		
	}

}