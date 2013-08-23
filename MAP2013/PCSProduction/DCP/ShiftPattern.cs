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
using PCSComUtils.MasterSetup.DS;
using PCSComUtils.Common.BO;
using PCSComProduction.DCP.BO;
using PCSComProduction.DCP.DS;

namespace PCSProduction.DCP
{
	/// <summary>
	/// Summary description for ShiftPattern.
	/// </summary>
	public class ShiftPattern : System.Windows.Forms.Form
	{
		private string strTotalTime;
		private string strWorkingTime;

		private System.Windows.Forms.Label lblCCN;
		private C1.Win.C1List.C1Combo cboCCN;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.GroupBox groupBox4;
		private System.Windows.Forms.GroupBox groupBox5;
		private System.Windows.Forms.Button btnDelete;
		private System.Windows.Forms.Button btnSave;
		private System.Windows.Forms.Button btnHelp;
		private System.Windows.Forms.Button btnClose;
		private System.Windows.Forms.Label lblWorkTime;
		private C1.Win.C1Input.C1DateEdit dtmWorkTimeTo;
		private C1.Win.C1Input.C1DateEdit dtmWorkTimeFrom;
		private System.Windows.Forms.Label lblWorkTimeTo;
		private System.Windows.Forms.Label lblWorkTimeFrom;
		private System.Windows.Forms.Label lblRegularStop;
		private C1.Win.C1Input.C1DateEdit dtmRegularTo;
		private C1.Win.C1Input.C1DateEdit dtmRegularStopFrom;
		private System.Windows.Forms.Label lblRegularStopTo;
		private System.Windows.Forms.Label lblRegularStopFrom;
		private System.Windows.Forms.Label lblRefresshing;
		private C1.Win.C1Input.C1DateEdit dtmRefresshingTo;
		private C1.Win.C1Input.C1DateEdit dtmRefresshingFrom;
		private System.Windows.Forms.Label lblRefresshingTo;
		private System.Windows.Forms.Label lblRefresshingFrom;
		private System.Windows.Forms.Label lblExtraStop;
		private C1.Win.C1Input.C1DateEdit dtmExtraStopTo;
		private C1.Win.C1Input.C1DateEdit dtmExtraStopFrom;
		private System.Windows.Forms.Label lblExtraStopTo;
		private System.Windows.Forms.Label lblExtraStopFrom;
		private System.Windows.Forms.Label lblShiftCode;
		private System.Windows.Forms.TextBox txtComment;
		private System.Windows.Forms.Label lblComment;
		private System.Windows.Forms.TextBox txtShiftCode;
		private System.Windows.Forms.Button btnShiftCode;
		private System.Windows.Forms.Label lblTotalTime;
		private System.Windows.Forms.Label lblWorkingTime;
		
		const string THIS = "PCSProduction.DCP.ShiftPattern";
		private int pintShiftID;
		EnumAction formMode;
		PRO_ShiftPatternVO voPRO_ShiftPattern;
		UtilsBO boUtil = new UtilsBO();
		private int pintShiftPatternID;
		private bool blnHasError = false;
		private DateTime dtmSpecialDay = new DateTime(1, 1, 1);
		
		private System.Windows.Forms.Button btnClearWorkTimeFrom;
		private System.Windows.Forms.Button btnClearWorkTimeTo;
		private System.Windows.Forms.Button btnClearRegularStopFrom;
		private System.Windows.Forms.Button btnClearRegularStopTo;
		private System.Windows.Forms.Button btnClearRefressingFrom;
		private System.Windows.Forms.Button btnClearRefressingTo;
		private System.Windows.Forms.Button btnClearExtraStopFrom;
		private System.Windows.Forms.Button btnClearExtraStopTo;
		
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public ShiftPattern()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
			strTotalTime = lblTotalTime.Text;
			strWorkingTime = lblWorkingTime.Text;
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
			this.lblCCN = new System.Windows.Forms.Label();
			this.cboCCN = new C1.Win.C1List.C1Combo();
			this.lblShiftCode = new System.Windows.Forms.Label();
			this.txtComment = new System.Windows.Forms.TextBox();
			this.lblComment = new System.Windows.Forms.Label();
			this.lblWorkTime = new System.Windows.Forms.Label();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.dtmWorkTimeTo = new C1.Win.C1Input.C1DateEdit();
			this.dtmWorkTimeFrom = new C1.Win.C1Input.C1DateEdit();
			this.lblWorkTimeTo = new System.Windows.Forms.Label();
			this.lblWorkTimeFrom = new System.Windows.Forms.Label();
			this.lblRegularStop = new System.Windows.Forms.Label();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.dtmRegularTo = new C1.Win.C1Input.C1DateEdit();
			this.dtmRegularStopFrom = new C1.Win.C1Input.C1DateEdit();
			this.lblRegularStopTo = new System.Windows.Forms.Label();
			this.lblRegularStopFrom = new System.Windows.Forms.Label();
			this.lblRefresshing = new System.Windows.Forms.Label();
			this.groupBox4 = new System.Windows.Forms.GroupBox();
			this.dtmRefresshingTo = new C1.Win.C1Input.C1DateEdit();
			this.dtmRefresshingFrom = new C1.Win.C1Input.C1DateEdit();
			this.lblRefresshingTo = new System.Windows.Forms.Label();
			this.lblRefresshingFrom = new System.Windows.Forms.Label();
			this.lblExtraStop = new System.Windows.Forms.Label();
			this.groupBox5 = new System.Windows.Forms.GroupBox();
			this.dtmExtraStopTo = new C1.Win.C1Input.C1DateEdit();
			this.dtmExtraStopFrom = new C1.Win.C1Input.C1DateEdit();
			this.lblExtraStopTo = new System.Windows.Forms.Label();
			this.lblExtraStopFrom = new System.Windows.Forms.Label();
			this.lblTotalTime = new System.Windows.Forms.Label();
			this.lblWorkingTime = new System.Windows.Forms.Label();
			this.btnDelete = new System.Windows.Forms.Button();
			this.btnSave = new System.Windows.Forms.Button();
			this.btnHelp = new System.Windows.Forms.Button();
			this.btnClose = new System.Windows.Forms.Button();
			this.txtShiftCode = new System.Windows.Forms.TextBox();
			this.btnShiftCode = new System.Windows.Forms.Button();
			this.btnClearWorkTimeFrom = new System.Windows.Forms.Button();
			this.btnClearWorkTimeTo = new System.Windows.Forms.Button();
			this.btnClearRegularStopFrom = new System.Windows.Forms.Button();
			this.btnClearRegularStopTo = new System.Windows.Forms.Button();
			this.btnClearRefressingFrom = new System.Windows.Forms.Button();
			this.btnClearRefressingTo = new System.Windows.Forms.Button();
			this.btnClearExtraStopFrom = new System.Windows.Forms.Button();
			this.btnClearExtraStopTo = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.cboCCN)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dtmWorkTimeTo)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dtmWorkTimeFrom)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dtmRegularTo)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dtmRegularStopFrom)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dtmRefresshingTo)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dtmRefresshingFrom)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dtmExtraStopTo)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dtmExtraStopFrom)).BeginInit();
			this.SuspendLayout();
			// 
			// lblCCN
			// 
			this.lblCCN.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.lblCCN.ForeColor = System.Drawing.Color.Maroon;
			this.lblCCN.Location = new System.Drawing.Point(324, 7);
			this.lblCCN.Name = "lblCCN";
			this.lblCCN.Size = new System.Drawing.Size(29, 21);
			this.lblCCN.TabIndex = 1;
			this.lblCCN.Text = "CCN";
			this.lblCCN.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
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
			this.cboCCN.Location = new System.Drawing.Point(354, 7);
			this.cboCCN.MatchEntryTimeout = ((long)(2000));
			this.cboCCN.MaxDropDownItems = ((short)(5));
			this.cboCCN.MaxLength = 32767;
			this.cboCCN.MouseCursor = System.Windows.Forms.Cursors.Default;
			this.cboCCN.Name = "cboCCN";
			this.cboCCN.RowDivider.Color = System.Drawing.Color.DarkGray;
			this.cboCCN.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None;
			this.cboCCN.RowSubDividerColor = System.Drawing.Color.DarkGray;
			this.cboCCN.Size = new System.Drawing.Size(96, 21);
			this.cboCCN.TabIndex = 0;
			this.cboCCN.Text = "CCN";
			this.cboCCN.PropBag = "<?xml version=\"1.0\"?><Blob><Styles type=\"C1.Win.C1List.Design.ContextWrapper\"><Da" +
				"ta>Group{AlignVert:Center;Border:None,,0, 0, 0, 0;BackColor:ControlDark;}Style2{" +
				"}Style5{}Style4{}Style7{}Style6{}EvenRow{BackColor:Aqua;}Selected{ForeColor:High" +
				"lightText;BackColor:Highlight;}Style3{}Inactive{ForeColor:InactiveCaptionText;Ba" +
				"ckColor:InactiveCaption;}Footer{}Caption{AlignHorz:Center;}Normal{}HighlightRow{" +
				"ForeColor:HighlightText;BackColor:Highlight;}Style1{}OddRow{}RecordSelector{Alig" +
				"nImage:Center;}Heading{Wrap:True;BackColor:Control;Border:Raised,,1, 1, 1, 1;For" +
				"eColor:ControlText;AlignVert:Center;}Style8{}Style10{}Style11{}Style9{AlignHorz:" +
				"Near;}</Data></Styles><Splits><C1.Win.C1List.ListBoxView AllowColSelect=\"False\" " +
				"Name=\"\" CaptionHeight=\"17\" ColumnCaptionHeight=\"17\" ColumnFooterHeight=\"17\" Vert" +
				"icalScrollGroup=\"1\" HorizontalScrollGroup=\"1\"><ClientRect>0, 0, 116, 156</Client" +
				"Rect><VScrollBar><Width>17</Width></VScrollBar><HScrollBar><Height>17</Height></" +
				"HScrollBar><CaptionStyle parent=\"Style2\" me=\"Style9\" /><EvenRowStyle parent=\"Eve" +
				"nRow\" me=\"Style7\" /><FooterStyle parent=\"Footer\" me=\"Style3\" /><GroupStyle paren" +
				"t=\"Group\" me=\"Style11\" /><HeadingStyle parent=\"Heading\" me=\"Style2\" /><HighLight" +
				"RowStyle parent=\"HighlightRow\" me=\"Style6\" /><InactiveStyle parent=\"Inactive\" me" +
				"=\"Style4\" /><OddRowStyle parent=\"OddRow\" me=\"Style8\" /><RecordSelectorStyle pare" +
				"nt=\"RecordSelector\" me=\"Style10\" /><SelectedStyle parent=\"Selected\" me=\"Style5\" " +
				"/><Style parent=\"Normal\" me=\"Style1\" /></C1.Win.C1List.ListBoxView></Splits><Nam" +
				"edStyles><Style parent=\"\" me=\"Normal\" /><Style parent=\"Normal\" me=\"Heading\" /><S" +
				"tyle parent=\"Heading\" me=\"Footer\" /><Style parent=\"Heading\" me=\"Caption\" /><Styl" +
				"e parent=\"Heading\" me=\"Inactive\" /><Style parent=\"Normal\" me=\"Selected\" /><Style" +
				" parent=\"Normal\" me=\"HighlightRow\" /><Style parent=\"Normal\" me=\"EvenRow\" /><Styl" +
				"e parent=\"Normal\" me=\"OddRow\" /><Style parent=\"Heading\" me=\"RecordSelector\" /><S" +
				"tyle parent=\"Caption\" me=\"Group\" /></NamedStyles><vertSplits>1</vertSplits><horz" +
				"Splits>1</horzSplits><Layout>Modified</Layout><DefaultRecSelWidth>16</DefaultRec" +
				"SelWidth></Blob>";
			// 
			// lblShiftCode
			// 
			this.lblShiftCode.ForeColor = System.Drawing.Color.Maroon;
			this.lblShiftCode.Location = new System.Drawing.Point(7, 7);
			this.lblShiftCode.Name = "lblShiftCode";
			this.lblShiftCode.Size = new System.Drawing.Size(57, 20);
			this.lblShiftCode.TabIndex = 2;
			this.lblShiftCode.Text = "Shift Code";
			this.lblShiftCode.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// txtComment
			// 
			this.txtComment.Location = new System.Drawing.Point(64, 32);
			this.txtComment.MaxLength = 200;
			this.txtComment.Name = "txtComment";
			this.txtComment.Size = new System.Drawing.Size(386, 20);
			this.txtComment.TabIndex = 6;
			this.txtComment.Text = "";
			// 
			// lblComment
			// 
			this.lblComment.Location = new System.Drawing.Point(7, 32);
			this.lblComment.Name = "lblComment";
			this.lblComment.Size = new System.Drawing.Size(78, 20);
			this.lblComment.TabIndex = 5;
			this.lblComment.Text = "Comment";
			this.lblComment.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblWorkTime
			// 
			this.lblWorkTime.ForeColor = System.Drawing.Color.Maroon;
			this.lblWorkTime.Location = new System.Drawing.Point(7, 65);
			this.lblWorkTime.Name = "lblWorkTime";
			this.lblWorkTime.Size = new System.Drawing.Size(78, 20);
			this.lblWorkTime.TabIndex = 13;
			this.lblWorkTime.Text = "Work Time:";
			this.lblWorkTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// groupBox1
			// 
			this.groupBox1.Location = new System.Drawing.Point(70, 75);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(379, 3);
			this.groupBox1.TabIndex = 14;
			this.groupBox1.TabStop = false;
			// 
			// dtmWorkTimeTo
			// 
			this.dtmWorkTimeTo.CustomFormat = "HH:mm";
			this.dtmWorkTimeTo.EmptyAsNull = true;
			this.dtmWorkTimeTo.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
			this.dtmWorkTimeTo.Location = new System.Drawing.Point(292, 87);
			this.dtmWorkTimeTo.Name = "dtmWorkTimeTo";
			this.dtmWorkTimeTo.Size = new System.Drawing.Size(61, 20);
			this.dtmWorkTimeTo.TabIndex = 18;
			this.dtmWorkTimeTo.Tag = null;
			this.dtmWorkTimeTo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.dtmWorkTimeTo.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.None;
			// 
			// dtmWorkTimeFrom
			// 
			this.dtmWorkTimeFrom.CustomFormat = "HH:mm";
			this.dtmWorkTimeFrom.EmptyAsNull = true;
			this.dtmWorkTimeFrom.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
			this.dtmWorkTimeFrom.Location = new System.Drawing.Point(115, 87);
			this.dtmWorkTimeFrom.Name = "dtmWorkTimeFrom";
			this.dtmWorkTimeFrom.Size = new System.Drawing.Size(61, 20);
			this.dtmWorkTimeFrom.TabIndex = 16;
			this.dtmWorkTimeFrom.Tag = null;
			this.dtmWorkTimeFrom.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.dtmWorkTimeFrom.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.None;
			// 
			// lblWorkTimeTo
			// 
			this.lblWorkTimeTo.ForeColor = System.Drawing.Color.Maroon;
			this.lblWorkTimeTo.Location = new System.Drawing.Point(272, 87);
			this.lblWorkTimeTo.Name = "lblWorkTimeTo";
			this.lblWorkTimeTo.Size = new System.Drawing.Size(24, 20);
			this.lblWorkTimeTo.TabIndex = 17;
			this.lblWorkTimeTo.Text = "To";
			this.lblWorkTimeTo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblWorkTimeFrom
			// 
			this.lblWorkTimeFrom.ForeColor = System.Drawing.Color.Maroon;
			this.lblWorkTimeFrom.Location = new System.Drawing.Point(85, 87);
			this.lblWorkTimeFrom.Name = "lblWorkTimeFrom";
			this.lblWorkTimeFrom.Size = new System.Drawing.Size(31, 20);
			this.lblWorkTimeFrom.TabIndex = 15;
			this.lblWorkTimeFrom.Text = "From";
			this.lblWorkTimeFrom.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblRegularStop
			// 
			this.lblRegularStop.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblRegularStop.Location = new System.Drawing.Point(7, 106);
			this.lblRegularStop.Name = "lblRegularStop";
			this.lblRegularStop.Size = new System.Drawing.Size(78, 20);
			this.lblRegularStop.TabIndex = 19;
			this.lblRegularStop.Text = "Regular Stop";
			this.lblRegularStop.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// groupBox3
			// 
			this.groupBox3.Location = new System.Drawing.Point(70, 116);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(379, 3);
			this.groupBox3.TabIndex = 20;
			this.groupBox3.TabStop = false;
			// 
			// dtmRegularTo
			// 
			this.dtmRegularTo.CustomFormat = "HH:mm";
			this.dtmRegularTo.EmptyAsNull = true;
			this.dtmRegularTo.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
			this.dtmRegularTo.Location = new System.Drawing.Point(292, 128);
			this.dtmRegularTo.Name = "dtmRegularTo";
			this.dtmRegularTo.Size = new System.Drawing.Size(61, 20);
			this.dtmRegularTo.TabIndex = 24;
			this.dtmRegularTo.Tag = null;
			this.dtmRegularTo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.dtmRegularTo.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.None;
			// 
			// dtmRegularStopFrom
			// 
			this.dtmRegularStopFrom.CustomFormat = "HH:mm";
			this.dtmRegularStopFrom.EmptyAsNull = true;
			this.dtmRegularStopFrom.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
			this.dtmRegularStopFrom.Location = new System.Drawing.Point(115, 128);
			this.dtmRegularStopFrom.Name = "dtmRegularStopFrom";
			this.dtmRegularStopFrom.Size = new System.Drawing.Size(61, 20);
			this.dtmRegularStopFrom.TabIndex = 22;
			this.dtmRegularStopFrom.Tag = null;
			this.dtmRegularStopFrom.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.dtmRegularStopFrom.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.None;
			// 
			// lblRegularStopTo
			// 
			this.lblRegularStopTo.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblRegularStopTo.Location = new System.Drawing.Point(272, 128);
			this.lblRegularStopTo.Name = "lblRegularStopTo";
			this.lblRegularStopTo.Size = new System.Drawing.Size(24, 20);
			this.lblRegularStopTo.TabIndex = 23;
			this.lblRegularStopTo.Text = "To";
			this.lblRegularStopTo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblRegularStopFrom
			// 
			this.lblRegularStopFrom.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblRegularStopFrom.Location = new System.Drawing.Point(85, 128);
			this.lblRegularStopFrom.Name = "lblRegularStopFrom";
			this.lblRegularStopFrom.Size = new System.Drawing.Size(31, 20);
			this.lblRegularStopFrom.TabIndex = 21;
			this.lblRegularStopFrom.Text = "From";
			this.lblRegularStopFrom.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblRefresshing
			// 
			this.lblRefresshing.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblRefresshing.Location = new System.Drawing.Point(7, 148);
			this.lblRefresshing.Name = "lblRefresshing";
			this.lblRefresshing.Size = new System.Drawing.Size(78, 20);
			this.lblRefresshing.TabIndex = 25;
			this.lblRefresshing.Text = "Refreshing";
			this.lblRefresshing.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// groupBox4
			// 
			this.groupBox4.Location = new System.Drawing.Point(70, 158);
			this.groupBox4.Name = "groupBox4";
			this.groupBox4.Size = new System.Drawing.Size(379, 3);
			this.groupBox4.TabIndex = 26;
			this.groupBox4.TabStop = false;
			// 
			// dtmRefresshingTo
			// 
			this.dtmRefresshingTo.CustomFormat = "HH:mm";
			this.dtmRefresshingTo.EmptyAsNull = true;
			this.dtmRefresshingTo.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
			this.dtmRefresshingTo.Location = new System.Drawing.Point(292, 170);
			this.dtmRefresshingTo.Name = "dtmRefresshingTo";
			this.dtmRefresshingTo.Size = new System.Drawing.Size(61, 20);
			this.dtmRefresshingTo.TabIndex = 30;
			this.dtmRefresshingTo.Tag = null;
			this.dtmRefresshingTo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.dtmRefresshingTo.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.None;
			// 
			// dtmRefresshingFrom
			// 
			this.dtmRefresshingFrom.CustomFormat = "HH:mm";
			this.dtmRefresshingFrom.EmptyAsNull = true;
			this.dtmRefresshingFrom.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
			this.dtmRefresshingFrom.Location = new System.Drawing.Point(115, 170);
			this.dtmRefresshingFrom.Name = "dtmRefresshingFrom";
			this.dtmRefresshingFrom.Size = new System.Drawing.Size(61, 20);
			this.dtmRefresshingFrom.TabIndex = 28;
			this.dtmRefresshingFrom.Tag = null;
			this.dtmRefresshingFrom.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.dtmRefresshingFrom.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.None;
			// 
			// lblRefresshingTo
			// 
			this.lblRefresshingTo.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblRefresshingTo.Location = new System.Drawing.Point(272, 170);
			this.lblRefresshingTo.Name = "lblRefresshingTo";
			this.lblRefresshingTo.Size = new System.Drawing.Size(24, 20);
			this.lblRefresshingTo.TabIndex = 29;
			this.lblRefresshingTo.Text = "To";
			this.lblRefresshingTo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblRefresshingFrom
			// 
			this.lblRefresshingFrom.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblRefresshingFrom.Location = new System.Drawing.Point(85, 170);
			this.lblRefresshingFrom.Name = "lblRefresshingFrom";
			this.lblRefresshingFrom.Size = new System.Drawing.Size(31, 20);
			this.lblRefresshingFrom.TabIndex = 27;
			this.lblRefresshingFrom.Text = "From";
			this.lblRefresshingFrom.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblExtraStop
			// 
			this.lblExtraStop.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblExtraStop.Location = new System.Drawing.Point(7, 188);
			this.lblExtraStop.Name = "lblExtraStop";
			this.lblExtraStop.Size = new System.Drawing.Size(78, 20);
			this.lblExtraStop.TabIndex = 31;
			this.lblExtraStop.Text = "Extra Stop";
			this.lblExtraStop.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// groupBox5
			// 
			this.groupBox5.Location = new System.Drawing.Point(70, 198);
			this.groupBox5.Name = "groupBox5";
			this.groupBox5.Size = new System.Drawing.Size(379, 3);
			this.groupBox5.TabIndex = 32;
			this.groupBox5.TabStop = false;
			// 
			// dtmExtraStopTo
			// 
			this.dtmExtraStopTo.CustomFormat = "HH:mm";
			this.dtmExtraStopTo.EmptyAsNull = true;
			this.dtmExtraStopTo.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
			this.dtmExtraStopTo.Location = new System.Drawing.Point(292, 210);
			this.dtmExtraStopTo.Name = "dtmExtraStopTo";
			this.dtmExtraStopTo.Size = new System.Drawing.Size(61, 20);
			this.dtmExtraStopTo.TabIndex = 36;
			this.dtmExtraStopTo.Tag = null;
			this.dtmExtraStopTo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.dtmExtraStopTo.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.None;
			// 
			// dtmExtraStopFrom
			// 
			this.dtmExtraStopFrom.CustomFormat = "HH:mm";
			this.dtmExtraStopFrom.EmptyAsNull = true;
			this.dtmExtraStopFrom.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
			this.dtmExtraStopFrom.Location = new System.Drawing.Point(115, 210);
			this.dtmExtraStopFrom.Name = "dtmExtraStopFrom";
			this.dtmExtraStopFrom.Size = new System.Drawing.Size(61, 20);
			this.dtmExtraStopFrom.TabIndex = 34;
			this.dtmExtraStopFrom.Tag = null;
			this.dtmExtraStopFrom.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.dtmExtraStopFrom.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.None;
			// 
			// lblExtraStopTo
			// 
			this.lblExtraStopTo.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblExtraStopTo.Location = new System.Drawing.Point(272, 210);
			this.lblExtraStopTo.Name = "lblExtraStopTo";
			this.lblExtraStopTo.Size = new System.Drawing.Size(24, 20);
			this.lblExtraStopTo.TabIndex = 35;
			this.lblExtraStopTo.Text = "To";
			this.lblExtraStopTo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblExtraStopFrom
			// 
			this.lblExtraStopFrom.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblExtraStopFrom.Location = new System.Drawing.Point(85, 210);
			this.lblExtraStopFrom.Name = "lblExtraStopFrom";
			this.lblExtraStopFrom.Size = new System.Drawing.Size(31, 20);
			this.lblExtraStopFrom.TabIndex = 33;
			this.lblExtraStopFrom.Text = "From";
			this.lblExtraStopFrom.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblTotalTime
			// 
			this.lblTotalTime.ForeColor = System.Drawing.Color.Red;
			this.lblTotalTime.Location = new System.Drawing.Point(40, 239);
			this.lblTotalTime.Name = "lblTotalTime";
			this.lblTotalTime.Size = new System.Drawing.Size(408, 20);
			this.lblTotalTime.TabIndex = 37;
			this.lblTotalTime.Text = "Total Time: 8 Hrs 00 Minutes or 480 Minutes or 28800 Seconds";
			this.lblTotalTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblWorkingTime
			// 
			this.lblWorkingTime.ForeColor = System.Drawing.Color.Red;
			this.lblWorkingTime.Location = new System.Drawing.Point(40, 268);
			this.lblWorkingTime.Name = "lblWorkingTime";
			this.lblWorkingTime.Size = new System.Drawing.Size(404, 20);
			this.lblWorkingTime.TabIndex = 38;
			this.lblWorkingTime.Text = "Working Time: 8 Hrs 00 Minutes or 480 Minutes or 28800 Seconds";
			this.lblWorkingTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// btnDelete
			// 
			this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnDelete.Location = new System.Drawing.Point(66, 302);
			this.btnDelete.Name = "btnDelete";
			this.btnDelete.Size = new System.Drawing.Size(60, 23);
			this.btnDelete.TabIndex = 40;
			this.btnDelete.Text = "&Delete";
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
			// 
			// btnSave
			// 
			this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnSave.Location = new System.Drawing.Point(6, 302);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(60, 23);
			this.btnSave.TabIndex = 39;
			this.btnSave.Text = "&Save";
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			// 
			// btnHelp
			// 
			this.btnHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnHelp.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnHelp.Location = new System.Drawing.Point(330, 302);
			this.btnHelp.Name = "btnHelp";
			this.btnHelp.Size = new System.Drawing.Size(60, 23);
			this.btnHelp.TabIndex = 41;
			this.btnHelp.Text = "&Help";
			// 
			// btnClose
			// 
			this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnClose.Location = new System.Drawing.Point(390, 302);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(60, 23);
			this.btnClose.TabIndex = 42;
			this.btnClose.Text = "&Close";
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			// 
			// txtShiftCode
			// 
			this.txtShiftCode.Location = new System.Drawing.Point(64, 8);
			this.txtShiftCode.MaxLength = 24;
			this.txtShiftCode.Name = "txtShiftCode";
			this.txtShiftCode.Size = new System.Drawing.Size(68, 20);
			this.txtShiftCode.TabIndex = 3;
			this.txtShiftCode.Text = "";
			this.txtShiftCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtShiftCode_KeyDown);
			this.txtShiftCode.Leave += new System.EventHandler(this.txtShiftCode_Leave);
			// 
			// btnShiftCode
			// 
			this.btnShiftCode.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnShiftCode.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btnShiftCode.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnShiftCode.Location = new System.Drawing.Point(132, 8);
			this.btnShiftCode.Name = "btnShiftCode";
			this.btnShiftCode.Size = new System.Drawing.Size(24, 20);
			this.btnShiftCode.TabIndex = 4;
			this.btnShiftCode.Text = "...";
			this.btnShiftCode.Click += new System.EventHandler(this.btnShiftCode_Click);
			// 
			// btnClearWorkTimeFrom
			// 
			this.btnClearWorkTimeFrom.Location = new System.Drawing.Point(177, 87);
			this.btnClearWorkTimeFrom.Name = "btnClearWorkTimeFrom";
			this.btnClearWorkTimeFrom.Size = new System.Drawing.Size(22, 20);
			this.btnClearWorkTimeFrom.TabIndex = 43;
			this.btnClearWorkTimeFrom.Text = "X";
			this.btnClearWorkTimeFrom.Click += new System.EventHandler(this.btnClearWorkTimeFrom_Click);
			// 
			// btnClearWorkTimeTo
			// 
			this.btnClearWorkTimeTo.Location = new System.Drawing.Point(354, 87);
			this.btnClearWorkTimeTo.Name = "btnClearWorkTimeTo";
			this.btnClearWorkTimeTo.Size = new System.Drawing.Size(22, 20);
			this.btnClearWorkTimeTo.TabIndex = 44;
			this.btnClearWorkTimeTo.Text = "X";
			this.btnClearWorkTimeTo.Click += new System.EventHandler(this.btnClearWorkTimeTo_Click);
			// 
			// btnClearRegularStopFrom
			// 
			this.btnClearRegularStopFrom.Location = new System.Drawing.Point(177, 128);
			this.btnClearRegularStopFrom.Name = "btnClearRegularStopFrom";
			this.btnClearRegularStopFrom.Size = new System.Drawing.Size(22, 20);
			this.btnClearRegularStopFrom.TabIndex = 45;
			this.btnClearRegularStopFrom.Text = "X";
			this.btnClearRegularStopFrom.Click += new System.EventHandler(this.btnClearRegularStopFrom_Click);
			// 
			// btnClearRegularStopTo
			// 
			this.btnClearRegularStopTo.Location = new System.Drawing.Point(354, 128);
			this.btnClearRegularStopTo.Name = "btnClearRegularStopTo";
			this.btnClearRegularStopTo.Size = new System.Drawing.Size(22, 20);
			this.btnClearRegularStopTo.TabIndex = 46;
			this.btnClearRegularStopTo.Text = "X";
			this.btnClearRegularStopTo.Click += new System.EventHandler(this.btnClearRegularStopTo_Click);
			// 
			// btnClearRefressingFrom
			// 
			this.btnClearRefressingFrom.Location = new System.Drawing.Point(177, 170);
			this.btnClearRefressingFrom.Name = "btnClearRefressingFrom";
			this.btnClearRefressingFrom.Size = new System.Drawing.Size(22, 20);
			this.btnClearRefressingFrom.TabIndex = 47;
			this.btnClearRefressingFrom.Text = "X";
			this.btnClearRefressingFrom.Click += new System.EventHandler(this.btnClearRefressingFrom_Click);
			// 
			// btnClearRefressingTo
			// 
			this.btnClearRefressingTo.Location = new System.Drawing.Point(354, 170);
			this.btnClearRefressingTo.Name = "btnClearRefressingTo";
			this.btnClearRefressingTo.Size = new System.Drawing.Size(22, 20);
			this.btnClearRefressingTo.TabIndex = 48;
			this.btnClearRefressingTo.Text = "X";
			this.btnClearRefressingTo.Click += new System.EventHandler(this.btnClearRefressingTo_Click);
			// 
			// btnClearExtraStopFrom
			// 
			this.btnClearExtraStopFrom.Location = new System.Drawing.Point(177, 210);
			this.btnClearExtraStopFrom.Name = "btnClearExtraStopFrom";
			this.btnClearExtraStopFrom.Size = new System.Drawing.Size(22, 20);
			this.btnClearExtraStopFrom.TabIndex = 49;
			this.btnClearExtraStopFrom.Text = "X";
			this.btnClearExtraStopFrom.Click += new System.EventHandler(this.btnClearExtraStopFrom_Click);
			// 
			// btnClearExtraStopTo
			// 
			this.btnClearExtraStopTo.Location = new System.Drawing.Point(354, 210);
			this.btnClearExtraStopTo.Name = "btnClearExtraStopTo";
			this.btnClearExtraStopTo.Size = new System.Drawing.Size(22, 20);
			this.btnClearExtraStopTo.TabIndex = 50;
			this.btnClearExtraStopTo.Text = "X";
			this.btnClearExtraStopTo.Click += new System.EventHandler(this.btnClearExtraStopTo_Click);
			// 
			// ShiftPattern
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.btnClose;
			this.ClientSize = new System.Drawing.Size(456, 329);
			this.Controls.Add(this.btnClearExtraStopTo);
			this.Controls.Add(this.btnClearExtraStopFrom);
			this.Controls.Add(this.btnClearRefressingTo);
			this.Controls.Add(this.btnClearRefressingFrom);
			this.Controls.Add(this.btnClearRegularStopTo);
			this.Controls.Add(this.btnClearRegularStopFrom);
			this.Controls.Add(this.btnClearWorkTimeTo);
			this.Controls.Add(this.btnClearWorkTimeFrom);
			this.Controls.Add(this.txtShiftCode);
			this.Controls.Add(this.txtComment);
			this.Controls.Add(this.btnShiftCode);
			this.Controls.Add(this.btnHelp);
			this.Controls.Add(this.btnClose);
			this.Controls.Add(this.btnDelete);
			this.Controls.Add(this.btnSave);
			this.Controls.Add(this.lblWorkingTime);
			this.Controls.Add(this.lblTotalTime);
			this.Controls.Add(this.lblExtraStop);
			this.Controls.Add(this.groupBox5);
			this.Controls.Add(this.dtmExtraStopTo);
			this.Controls.Add(this.dtmExtraStopFrom);
			this.Controls.Add(this.lblExtraStopTo);
			this.Controls.Add(this.lblExtraStopFrom);
			this.Controls.Add(this.lblRefresshing);
			this.Controls.Add(this.groupBox4);
			this.Controls.Add(this.dtmRefresshingTo);
			this.Controls.Add(this.dtmRefresshingFrom);
			this.Controls.Add(this.lblRefresshingTo);
			this.Controls.Add(this.lblRefresshingFrom);
			this.Controls.Add(this.lblRegularStop);
			this.Controls.Add(this.groupBox3);
			this.Controls.Add(this.dtmRegularTo);
			this.Controls.Add(this.dtmRegularStopFrom);
			this.Controls.Add(this.lblRegularStopTo);
			this.Controls.Add(this.lblRegularStopFrom);
			this.Controls.Add(this.lblWorkTime);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.dtmWorkTimeTo);
			this.Controls.Add(this.dtmWorkTimeFrom);
			this.Controls.Add(this.lblWorkTimeTo);
			this.Controls.Add(this.lblWorkTimeFrom);
			this.Controls.Add(this.lblComment);
			this.Controls.Add(this.lblShiftCode);
			this.Controls.Add(this.lblCCN);
			this.Controls.Add(this.cboCCN);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.KeyPreview = true;
			this.MaximizeBox = false;
			this.Name = "ShiftPattern";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Shift Pattern";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.ShiftPattern_Closing);
			this.Load += new System.EventHandler(this.ShiftPattern_Load);
			((System.ComponentModel.ISupportInitialize)(this.cboCCN)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dtmWorkTimeTo)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dtmWorkTimeFrom)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dtmRegularTo)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dtmRegularStopFrom)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dtmRefresshingTo)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dtmRefresshingFrom)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dtmExtraStopTo)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dtmExtraStopFrom)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion
		/// <summary>
		/// ShiftPattern_Load
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Friday, August 12 2005</date>
		private void ShiftPattern_Load(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".ShiftPattern_Load()";
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
				// Load combo box
				DataSet dstCCN = boUtil.ListCCN();
				cboCCN.DataSource = dstCCN.Tables[MST_CCNTable.TABLE_NAME];
				cboCCN.DisplayMember = MST_CCNTable.CODE_FLD;
				cboCCN.ValueMember = MST_CCNTable.CCNID_FLD;
				FormControlComponents.PutDataIntoC1ComboBox(cboCCN, dstCCN.Tables[MST_CCNTable.TABLE_NAME],MST_CCNTable.CODE_FLD,MST_CCNTable.CCNID_FLD,MST_CCNTable.TABLE_NAME);
				if (SystemProperty.CCNID != 0)
				{
					cboCCN.SelectedValue = SystemProperty.CCNID;
				}
				formMode = EnumAction.Default;
				ClearForm();
				SwitchFormMode();
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
		/// ClearForm
		/// </summary>
		/// <author>Trada</author>
		/// <date>Friday, August 12 2005</date>
		private void ClearForm()
		{
			const string METHOD_NAME = THIS + ".ClearForm()";
			try
			{
				txtShiftCode.Text = string.Empty;
				txtComment.Text = string.Empty;

				#region HACK: DEL Trada 10-17-2005

//				dtmEffectiveDateFrom.Value = null;
//				dtmEffectiveDateTo.Value = null;

				#endregion END: DEL Trada 10-17-2005

				dtmExtraStopFrom.Value = null;
				dtmExtraStopTo.Value = null;
				dtmRefresshingFrom.Value = null;
				dtmRefresshingTo.Value = null;
				dtmRegularStopFrom.Value = null;
				dtmRegularTo.Value = null;
				dtmWorkTimeFrom.Value = null;
				dtmWorkTimeTo.Value = null;
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
		/// Parse DateTime to DateTime for compare
		/// </summary>
		/// <param name="dtmInput"></param>
		/// <returns></returns>
		/// <author>Trada</author>
		/// <date>Monday, August 15 2005</date>
		private DateTime ParseDateTime(DateTime dtmInput)
		{
			const string METHOD_NAME = THIS + ".ParseDateTime()";
			try
			{
				//DateTime dtmTemp = DateTime.Parse(dtmEffectiveDateFrom.Value.ToString());				
				DateTime dtmTemp = new DateTime(2005, 1, 1);
				DateTime dtmOutput = new DateTime(dtmTemp.Year, dtmTemp.Month, dtmTemp.Day, dtmInput.Hour, dtmInput.Minute, 0, 0);
				return dtmOutput;
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
		/// SwitchFormMode
		/// </summary>
		/// <author>Trada</author>
		/// <date>Friday, August 12 2005</date>
		private void SwitchFormMode()
		{
			const string METHOD_NAME = THIS + ".SwitchFormMode()";
			try
			{
				switch (formMode)
				{
					case EnumAction.Default:
						txtComment.Enabled = false;
						txtShiftCode.Enabled = true;

						#region HACK: DEL Trada 10-17-2005

//						dtmEffectiveDateFrom.Enabled = false;
//						dtmEffectiveDateTo.Enabled = false;

						#endregion END: DEL Trada 10-17-2005

						dtmExtraStopFrom.Enabled = false;
						dtmExtraStopTo.Enabled = false;
						dtmRefresshingFrom.Enabled = false;
						dtmRefresshingTo.Enabled = false;
						dtmRegularStopFrom.Enabled = false;
						dtmRegularTo.Enabled = false;
						dtmWorkTimeFrom.Enabled = false;
						dtmWorkTimeTo.Enabled = false;
						btnSave.Enabled = false;
						btnDelete.Enabled = false;
						btnClearExtraStopFrom.Enabled = false;
						btnClearExtraStopTo.Enabled = false;
						btnClearRefressingFrom.Enabled = false;
						btnClearRefressingTo.Enabled = false;
						btnClearRegularStopFrom.Enabled = false;
						btnClearRegularStopTo.Enabled = false;
						btnClearWorkTimeFrom.Enabled = false;
						btnClearWorkTimeTo.Enabled = false;
						break;
					case EnumAction.Add:
						txtComment.Enabled = true;
						txtShiftCode.Enabled = true;

						#region HACK: DEL Trada 10-17-2005

//						dtmEffectiveDateFrom.Enabled = true;
//						dtmEffectiveDateTo.Enabled = true;

						#endregion END: DEL Trada 10-17-2005

						dtmExtraStopFrom.Enabled = true;
						dtmExtraStopTo.Enabled = true;
						dtmRefresshingFrom.Enabled = true;
						dtmRefresshingTo.Enabled = true;
						dtmRegularStopFrom.Enabled = true;
						dtmRegularTo.Enabled = true;
						dtmWorkTimeFrom.Enabled = true;
						dtmWorkTimeTo.Enabled = true;
						btnSave.Enabled = true;
						btnDelete.Enabled = false;
						btnClearExtraStopFrom.Enabled = true;
						btnClearExtraStopTo.Enabled = true;
						btnClearRefressingFrom.Enabled = true;
						btnClearRefressingTo.Enabled = true;
						btnClearRegularStopFrom.Enabled = true;
						btnClearRegularStopTo.Enabled = true;
						btnClearWorkTimeFrom.Enabled = true;
						btnClearWorkTimeTo.Enabled = true;
						break;
					case EnumAction.Edit:
						txtComment.Enabled = true;
						txtShiftCode.Enabled = true;

						#region HACK: DEL Trada 10-17-2005

//						dtmEffectiveDateFrom.Enabled = true;
//						dtmEffectiveDateTo.Enabled = true;

						#endregion END: DEL Trada 10-17-2005

						dtmExtraStopFrom.Enabled = true;
						dtmExtraStopTo.Enabled = true;
						dtmRefresshingFrom.Enabled = true;
						dtmRefresshingTo.Enabled = true;
						dtmRegularStopFrom.Enabled = true;
						dtmRegularTo.Enabled = true;
						dtmWorkTimeFrom.Enabled = true;
						dtmWorkTimeTo.Enabled = true;
						btnSave.Enabled = true;
						btnDelete.Enabled = true;
						btnClearExtraStopFrom.Enabled = true;
						btnClearExtraStopTo.Enabled = true;
						btnClearRefressingFrom.Enabled = true;
						btnClearRefressingTo.Enabled = true;
						btnClearRegularStopFrom.Enabled = true;
						btnClearRegularStopTo.Enabled = true;
						btnClearWorkTimeFrom.Enabled = true;
						btnClearWorkTimeTo.Enabled = true;
						break;
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
		/// FillDataToControls
		/// </summary>
		/// <param name="pobjShiftPatternVO"></param>
		/// <author>Trada</author>
		/// <date>Friday, August 12 2005</date>
		private void FillDataToControls(object pobjShiftPatternVO)
		{
			const string METHOD_NAME = THIS + ".FillDataToControls()";
			try
			{
				PRO_ShiftPatternVO voPRO_ShiftPattern = (PRO_ShiftPatternVO) pobjShiftPatternVO;
				txtComment.Text = voPRO_ShiftPattern.Comment;

				#region HACK: DEL Trada 10-17-2005

//				dtmEffectiveDateFrom.Value = voPRO_ShiftPattern.EffectDateFrom;
//				dtmEffectiveDateTo.Value = voPRO_ShiftPattern.EffectDateTo;

				#endregion END: DEL Trada 10-17-2005

				//Check if DateTime is null
				if (voPRO_ShiftPattern.ExtraStopFrom.ToShortDateString() != dtmSpecialDay.ToShortDateString())
				{
					dtmExtraStopFrom.Value = voPRO_ShiftPattern.ExtraStopFrom;
				}
				else
					dtmExtraStopFrom.Value = null;
				if (voPRO_ShiftPattern.ExtraStopTo.ToShortDateString() != dtmSpecialDay.ToShortDateString())
				{
					dtmExtraStopTo.Value = voPRO_ShiftPattern.ExtraStopTo;
				}
				else
					dtmExtraStopTo.Value = null;
				if (voPRO_ShiftPattern.RefreshingFrom.ToShortDateString() != dtmSpecialDay.ToShortDateString())
				{
					dtmRefresshingFrom.Value = voPRO_ShiftPattern.RefreshingFrom;
				}
				else
					dtmRefresshingFrom.Value = null;
				if (voPRO_ShiftPattern.RefreshingTo.ToShortDateString() != dtmSpecialDay.ToShortDateString())
				{
					dtmRefresshingTo.Value = voPRO_ShiftPattern.RefreshingTo;
				}
				else
					dtmRefresshingTo.Value = null;
				if (voPRO_ShiftPattern.RegularStopFrom.ToShortDateString() != dtmSpecialDay.ToShortDateString())
				{
					dtmRegularStopFrom.Value = voPRO_ShiftPattern.RegularStopFrom;
				}
				else
					dtmRegularStopFrom.Value = null;
				if (voPRO_ShiftPattern.RegularStopTo.ToShortDateString() != dtmSpecialDay.ToShortDateString())
				{
					dtmRegularTo.Value = voPRO_ShiftPattern.RegularStopTo;
				}
				else
					dtmRegularTo.Value = null;
				dtmWorkTimeFrom.Value = voPRO_ShiftPattern.WorkTimeFrom;
				dtmWorkTimeTo.Value = voPRO_ShiftPattern.WorkTimeTo;
				//Set text for Total Time and Working Time lable
				TimeSpan tsTotalTime = new TimeSpan();
				tsTotalTime = voPRO_ShiftPattern.WorkTimeTo - voPRO_ShiftPattern.WorkTimeFrom;

				#region HACK: DEL Trada 10-21-2005

//				double dblHour = ((DateTime)dtmWorkTimeTo.Value - (DateTime)dtmWorkTimeFrom.Value).Hours;
//				double dblMin = ((DateTime)dtmWorkTimeTo.Value - (DateTime)dtmWorkTimeFrom.Value).Minutes;
//				double dblSecond = ((DateTime)dtmWorkTimeTo.Value - (DateTime)dtmWorkTimeFrom.Value).Seconds;

				#endregion END: DEL Trada 10-21-2005
				double dblHour = Math.Floor(tsTotalTime.TotalHours);
				//Display time in lable
				lblTotalTime.Text = strTotalTime;
				lblTotalTime.Text = lblTotalTime.Text.Replace(" 8 ", Constants.WHITE_SPACE + dblHour.ToString() +  Constants.WHITE_SPACE);
				lblTotalTime.Text = lblTotalTime.Text.Replace(" 00 ", Constants.WHITE_SPACE + tsTotalTime.Minutes.ToString() + Constants.WHITE_SPACE);
				lblTotalTime.Text = lblTotalTime.Text.Replace(" 480 ", Constants.WHITE_SPACE + tsTotalTime.TotalMinutes.ToString() + Constants.WHITE_SPACE);
				lblTotalTime.Text = lblTotalTime.Text.Replace(" 28800 ", Constants.WHITE_SPACE + tsTotalTime.TotalSeconds.ToString() + Constants.WHITE_SPACE);
				TimeSpan tsRegularStop = new TimeSpan();
				TimeSpan tsRefreshing = new TimeSpan();
				TimeSpan tsExtraStop = new TimeSpan();
				if ((voPRO_ShiftPattern.RegularStopFrom.ToShortDateString() != dtmSpecialDay.ToShortDateString()) 
					&& (voPRO_ShiftPattern.RegularStopTo.ToShortDateString() != dtmSpecialDay.ToShortDateString()))
				{
					 tsRegularStop = voPRO_ShiftPattern.RegularStopTo - voPRO_ShiftPattern.RegularStopFrom;				
				}
				if ((voPRO_ShiftPattern.RefreshingFrom.ToShortDateString() != dtmSpecialDay.ToShortDateString()) 
					&& (voPRO_ShiftPattern.RefreshingTo.ToShortDateString() != dtmSpecialDay.ToShortDateString()))
				{
					tsRefreshing = voPRO_ShiftPattern.RefreshingTo - voPRO_ShiftPattern.RefreshingFrom;				
				}
				if ((voPRO_ShiftPattern.ExtraStopFrom.ToShortDateString() != dtmSpecialDay.ToShortDateString()) 
					&& (voPRO_ShiftPattern.ExtraStopTo.ToShortDateString() != dtmSpecialDay.ToShortDateString()))
				{
					tsExtraStop = voPRO_ShiftPattern.ExtraStopTo - voPRO_ShiftPattern.ExtraStopFrom;				
				}
				TimeSpan tsWorkingTime = new TimeSpan();
				tsWorkingTime = tsTotalTime - (tsRefreshing + tsRegularStop + tsExtraStop);
				dblHour = Math.Floor(tsWorkingTime.TotalHours);
				lblWorkingTime.Text = strWorkingTime;
				lblWorkingTime.Text = lblWorkingTime.Text.Replace(" 8 ", Constants.WHITE_SPACE + dblHour.ToString() +  Constants.WHITE_SPACE);
				lblWorkingTime.Text = lblWorkingTime.Text.Replace(" 00 ", Constants.WHITE_SPACE + tsWorkingTime.Minutes.ToString() + Constants.WHITE_SPACE);
				lblWorkingTime.Text = lblWorkingTime.Text.Replace(" 480 ", Constants.WHITE_SPACE + tsWorkingTime.TotalMinutes.ToString() + Constants.WHITE_SPACE);
				lblWorkingTime.Text = lblWorkingTime.Text.Replace(" 28800 ", Constants.WHITE_SPACE + tsWorkingTime.TotalSeconds.ToString() + Constants.WHITE_SPACE);
				
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
		/// btnShiftCode_Click
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Friday, August 12 2005</date>
		private void btnShiftCode_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnShiftCode_Click()";
			try 
			{
				//Hashtable htbCriteria = new Hashtable();
				DataRowView drwResult = null;
				drwResult = FormControlComponents.OpenSearchForm(PRO_ShiftTable.TABLE_NAME, PRO_ShiftTable.SHIFTDESC_FLD, txtShiftCode.Text.Trim(), null, true);
				if (drwResult != null)
				{
					
					//Keep valua of ShiftID 
					txtShiftCode.Tag = drwResult[PRO_ShiftTable.SHIFTID_FLD];
					pintShiftID = int.Parse(txtShiftCode.Tag.ToString());
					//Get ShiftPattern from PRO_ShiftPattern follow CCN and ShiftID
					ShiftPatternBO boShiftPattern = new ShiftPatternBO();
					if (cboCCN.SelectedIndex == -1)
					{
						PCSMessageBox.Show(ErrorCode.MESSAGE_RGA_CCN, MessageBoxIcon.Warning);
						cboCCN.Focus();
						return;
					}
					voPRO_ShiftPattern = (PRO_ShiftPatternVO) boShiftPattern.GetShiftParttern(pintShiftID, int.Parse(cboCCN.SelectedValue.ToString()));
					if (voPRO_ShiftPattern.ShiftPatternID > 0) 
					{
						formMode = EnumAction.Edit;
						SwitchFormMode();
						FillDataToControls(voPRO_ShiftPattern);
						pintShiftPatternID = voPRO_ShiftPattern.ShiftPatternID;
					}
					else
					{
						formMode = EnumAction.Add;
						voPRO_ShiftPattern = new PRO_ShiftPatternVO();
						lblTotalTime.Text = strTotalTime;
						lblWorkingTime.Text = strWorkingTime;
						ClearForm();
						SwitchFormMode();
					}
					txtShiftCode.Text = drwResult[PRO_ShiftTable.SHIFTDESC_FLD].ToString();
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

		/// <summary>
		/// btnClose_Click
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Monday, August 15 2005</date>
		private void btnClose_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}
		/// <summary>
		/// txtShiftCode_Leave
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Friday, August 12 2005</date>
		private void txtShiftCode_Leave(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtShiftCode_Leave()";
			try 
			{
				//OnLeaveControl(sender, e);
				if (!txtShiftCode.Modified) return;
				if (txtShiftCode.Text == string.Empty)
				{
					txtShiftCode.Tag = null;
					voPRO_ShiftPattern = new PRO_ShiftPatternVO();
					lblTotalTime.Text = strTotalTime;
					lblWorkingTime.Text = strWorkingTime;
					ClearForm();
					return;
				}
				//Hashtable htbCriteria = new Hashtable();
				DataRowView drwResult = null;
				drwResult = FormControlComponents.OpenSearchForm(PRO_ShiftTable.TABLE_NAME, PRO_ShiftTable.SHIFTDESC_FLD, txtShiftCode.Text.Trim(), null, false);
				if (drwResult != null)
				{
					
					//Keep valua of ShiftID 
					txtShiftCode.Tag = drwResult[PRO_ShiftTable.SHIFTID_FLD];
					pintShiftID = int.Parse(txtShiftCode.Tag.ToString());
					//Get ShiftPattern from PRO_ShiftPattern follow CCN and ShiftID
					ShiftPatternBO boShiftPattern = new ShiftPatternBO();
					if (cboCCN.SelectedIndex == -1)
					{
						PCSMessageBox.Show(ErrorCode.MESSAGE_RGA_CCN, MessageBoxIcon.Warning);
						cboCCN.Focus();
						return;
					}
					voPRO_ShiftPattern = (PRO_ShiftPatternVO) boShiftPattern.GetShiftParttern(pintShiftID, int.Parse(cboCCN.SelectedValue.ToString()));
					if (voPRO_ShiftPattern.ShiftPatternID > 0) 
					{
						formMode = EnumAction.Edit;
						SwitchFormMode();
						FillDataToControls(voPRO_ShiftPattern);
						pintShiftPatternID = voPRO_ShiftPattern.ShiftPatternID;
					}
					else
					{
						formMode = EnumAction.Add;
						voPRO_ShiftPattern = new PRO_ShiftPatternVO();
						lblTotalTime.Text = strTotalTime;
						lblWorkingTime.Text = strWorkingTime;
						ClearForm();
						SwitchFormMode();
					}
					txtShiftCode.Text = drwResult[PRO_ShiftTable.SHIFTDESC_FLD].ToString();
				}
				else
					txtShiftCode.Focus();
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
		/// <summary>
		/// txtShiftCode_KeyDown
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Friday, August 12 2005</date>
		private void txtShiftCode_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtShiftCode_KeyDown()";
			if (e.KeyCode == Keys.F4)
			{
				btnShiftCode_Click(sender, e);	
			}
		}
		/// <summary>
		/// Check if two couple DateTime controls are intersected
		/// </summary>
		/// <param name="pdtmFrom1"></param>
		/// <param name="pdtmTo1"></param>
		/// <param name="pdtmFrom2"></param>
		/// <param name="pdtmTo2"></param>
		/// <returns>true: if they are intersected
		/// false: if they are not</returns>
		/// <author>Trada</author>
		/// <date>Friday, October 21 2005</date>
		private bool IsIntersect(DateTime pdtmFrom1, DateTime pdtmTo1, DateTime pdtmFrom2, DateTime pdtmTo2)
		{
			const string METHOD_NAME = THIS + ".IsIntersect()";
			try
			{
				DateTime dtmTemp = new DateTime();
				if (pdtmFrom1 > pdtmFrom2)
				{
					//swap positions of datetimes
					dtmTemp = pdtmFrom2;
					pdtmFrom2 = pdtmFrom1;
					pdtmFrom1 = dtmTemp;

					dtmTemp = pdtmTo1;
					pdtmTo1 = pdtmTo2;
					pdtmTo2 = dtmTemp;
				}
				//Check if they are intersected
				if (pdtmTo1 > pdtmFrom2)
				{
					return true;
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
		/// IsValidateData
		/// </summary>
		/// <returns></returns>
		/// <author>Trada</author>
		/// <date>Friday, August 12 2005</date>
		private bool IsValidateData()
		{
			const string METHOD_NAME = THIS + ".IsValidateData()";
			try
			{
				string[] strParam = new string[2];
				string[] strParamOverlap = new string[1];
				//Check mandatory fields
				if (FormControlComponents.CheckMandatory(cboCCN))
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID);
					cboCCN.Focus();
					cboCCN.Select();
					return false;
				}
				if (FormControlComponents.CheckMandatory(txtShiftCode))
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID);
					txtShiftCode.Focus();
					txtShiftCode.Select();
					return false;
				}
				if (FormControlComponents.CheckMandatory(dtmWorkTimeFrom))
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID);
					dtmWorkTimeFrom.Focus();
					dtmWorkTimeFrom.Select();
					return false;
				}
				if (FormControlComponents.CheckMandatory(dtmWorkTimeTo))
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID);
					dtmWorkTimeTo.Focus();
					dtmWorkTimeTo.Select();
					return false;
				}
				if (ParseDateTime(DateTime.Parse(dtmWorkTimeFrom.Value.ToString())) >= ParseDateTime(DateTime.Parse(dtmWorkTimeTo.Value.ToString())))
				{
					voPRO_ShiftPattern.WorkTimeTo = ParseDateTime(DateTime.Parse(dtmWorkTimeTo.Value.ToString())).AddDays(1);
				}
				else
					voPRO_ShiftPattern.WorkTimeTo = ParseDateTime(DateTime.Parse(dtmWorkTimeTo.Value.ToString()));
				voPRO_ShiftPattern.WorkTimeFrom = ParseDateTime(DateTime.Parse(dtmWorkTimeFrom.Value.ToString()));
				//System will display error message if exist Regular Stop From and not exist Regular Stop To
				if ((dtmRegularStopFrom.Value.ToString() != string.Empty) && (dtmRegularTo.Value.ToString() == string.Empty))
				{
					PCSMessageBox.Show(ErrorCode.MESSAGE_DCP_SHIFTPATTERN_PLS_INPUT_REMAIN_CONTROLS, MessageBoxIcon.Warning);	
					dtmRegularTo.Focus();
					dtmRegularTo.Select();
					return false;
				}
				//System will display error message if not exist Regular Stop From and exist Regular Stop To
				if ((dtmRegularStopFrom.Value.ToString() == string.Empty) && (dtmRegularTo.Value.ToString() != string.Empty))
				{
					PCSMessageBox.Show(ErrorCode.MESSAGE_DCP_SHIFTPATTERN_PLS_INPUT_REMAIN_CONTROLS, MessageBoxIcon.Warning);	
					dtmRegularStopFrom.Focus();
					dtmRegularStopFrom.Select();
					return false;
				}
				//System will display error message if exist Refresshing Stop From and not exist Refresshing Stop To
				if ((dtmRefresshingFrom.Value.ToString() != string.Empty) && (dtmRefresshingTo.Value.ToString() == string.Empty))
				{
					PCSMessageBox.Show(ErrorCode.MESSAGE_DCP_SHIFTPATTERN_PLS_INPUT_REMAIN_CONTROLS, MessageBoxIcon.Warning);	
					dtmRefresshingTo.Focus();
					dtmRefresshingTo.Select();
					return false;
				}
				//System will display error message if not exist Refresshing Stop From and exist Refresshing Stop To
				if ((dtmRefresshingFrom.Value.ToString() == string.Empty) && (dtmRefresshingTo.Value.ToString() != string.Empty))
				{
					PCSMessageBox.Show(ErrorCode.MESSAGE_DCP_SHIFTPATTERN_PLS_INPUT_REMAIN_CONTROLS, MessageBoxIcon.Warning);	
					dtmRefresshingFrom.Focus();
					dtmRefresshingFrom.Select();
					return false;
				}
				//System will display error message if not exist Extra Stop From and exist Extra Stop To
				if ((dtmExtraStopFrom.Value.ToString() == string.Empty) && (dtmExtraStopTo.Value.ToString() != string.Empty))
				{
					PCSMessageBox.Show(ErrorCode.MESSAGE_DCP_SHIFTPATTERN_PLS_INPUT_REMAIN_CONTROLS, MessageBoxIcon.Warning);	
					dtmExtraStopFrom.Focus();
					dtmExtraStopFrom.Select();
					return false;
				}
				//System will display error message if exist Extra Stop From and not exist Extra Stop To
				if ((dtmExtraStopFrom.Value.ToString() != string.Empty) && (dtmExtraStopTo.Value.ToString() == string.Empty))
				{
					PCSMessageBox.Show(ErrorCode.MESSAGE_DCP_SHIFTPATTERN_PLS_INPUT_REMAIN_CONTROLS, MessageBoxIcon.Warning);	
					dtmExtraStopTo.Focus();
					dtmExtraStopTo.Select();
					return false;
				}
				//Check value of Regular Stop Time
				if ((dtmRegularStopFrom.Value.ToString() != string.Empty) && (dtmRegularTo.Value.ToString() != string.Empty))
				{
					if ((dtmRegularStopFrom.Value != DBNull.Value) && (dtmRegularTo.Value != DBNull.Value))
					{
						//System will display error message if Regular Stop From = Regular Stop To 
						if (ParseDateTime(DateTime.Parse(dtmRegularStopFrom.Value.ToString())) == ParseDateTime(DateTime.Parse(dtmRegularTo.Value.ToString())))
						{
							
							strParamOverlap[0] = lblRegularStop.Text;
							PCSMessageBox.Show(ErrorCode.MESSAGE_OVERLAP_NOT_ALLOWED, MessageBoxIcon.Warning, strParamOverlap);	
							dtmRegularTo.Focus();
							return false;
						}
						//System will increment value of Regular Stop To if Regular Stop From > Regular Stop
						if (ParseDateTime(DateTime.Parse(dtmRegularStopFrom.Value.ToString())) > ParseDateTime(DateTime.Parse(dtmRegularTo.Value.ToString())))
						{
							voPRO_ShiftPattern.RegularStopTo = ParseDateTime(DateTime.Parse(dtmRegularTo.Value.ToString())).AddDays(1);
						}
						else
							voPRO_ShiftPattern.RegularStopTo = ParseDateTime(DateTime.Parse(dtmRegularTo.Value.ToString()));
						voPRO_ShiftPattern.RegularStopFrom = ParseDateTime(DateTime.Parse(dtmRegularStopFrom.Value.ToString()));
						
						//Regular Stop not in Work Time

						#region HACK: DEL Trada 10-17-2005

//if (ParseDateTime(DateTime.Parse(dtmRegularStopFrom.Value.ToString())) < ParseDateTime(DateTime.Parse(dtmWorkTimeFrom.Value.ToString())))
						//Begin Del by SonHT 2005-10-09
//						if ((voPRO_ShiftPattern.RegularStopFrom < voPRO_ShiftPattern.WorkTimeFrom)
//							||(voPRO_ShiftPattern.RegularStopFrom > voPRO_ShiftPattern.WorkTimeTo))
//						{
//							PCSMessageBox.Show(ErrorCode.MESSAGE_DCP_SHIFTPATTERN_REGULAR_TIME_MUSTBE_IN_WORKTIME, MessageBoxIcon.Warning);	
//							dtmRegularStopFrom.Focus();
//							dtmRegularStopFrom.Select();
//							return false;	
//						}
//						if ((voPRO_ShiftPattern.RegularStopTo > voPRO_ShiftPattern.WorkTimeTo)
//							||(voPRO_ShiftPattern.RegularStopTo < voPRO_ShiftPattern.WorkTimeFrom))
//						{
//							PCSMessageBox.Show(ErrorCode.MESSAGE_DCP_SHIFTPATTERN_REGULAR_TIME_MUSTBE_IN_WORKTIME, MessageBoxIcon.Warning);	
//							dtmRegularTo.Focus();
//							dtmRegularTo.Select();
//							return false;
					}
					//End Del by SonHT 2005-10-09

					#endregion END: DEL Trada 10-17-2005

					//Add by Trada: Set values for VO
						if (voPRO_ShiftPattern.RegularStopFrom < voPRO_ShiftPattern.WorkTimeFrom)
						{
							voPRO_ShiftPattern.RegularStopFrom = voPRO_ShiftPattern.RegularStopFrom.AddDays(1);
							voPRO_ShiftPattern.RegularStopTo = voPRO_ShiftPattern.RegularStopTo.AddDays(1);
						}
						if ((voPRO_ShiftPattern.RegularStopFrom < voPRO_ShiftPattern.WorkTimeFrom)
							||(voPRO_ShiftPattern.RegularStopFrom > voPRO_ShiftPattern.WorkTimeTo))
						{
							PCSMessageBox.Show(ErrorCode.MESSAGE_DCP_SHIFTPATTERN_REGULAR_TIME_MUSTBE_IN_WORKTIME, MessageBoxIcon.Warning);	
							dtmRegularStopFrom.Focus();
							dtmRegularStopFrom.Select();
							return false;	
						}
						if ((voPRO_ShiftPattern.RegularStopTo > voPRO_ShiftPattern.WorkTimeTo)
							||(voPRO_ShiftPattern.RegularStopTo < voPRO_ShiftPattern.WorkTimeFrom))
						{
							PCSMessageBox.Show(ErrorCode.MESSAGE_DCP_SHIFTPATTERN_REGULAR_TIME_MUSTBE_IN_WORKTIME, MessageBoxIcon.Warning);	
							dtmRegularTo.Focus();
							dtmRegularTo.Select();
							return false;
					}
				}
				//Check value of Refresshing Time
				if ((dtmRefresshingFrom.Value.ToString() != string.Empty) && (dtmRefresshingTo.Value.ToString() != string.Empty))
				{
					if ((dtmRefresshingFrom.Value != DBNull.Value) && (dtmRefresshingTo.Value != DBNull.Value))
					{
						//System will display error message if Refresshing From = Refresshing To 
						if (ParseDateTime(DateTime.Parse(dtmRefresshingFrom.Value.ToString())) == ParseDateTime(DateTime.Parse(dtmRefresshingTo.Value.ToString())))
						{
							strParamOverlap[0] = lblRefresshing.Text;
							PCSMessageBox.Show(ErrorCode.MESSAGE_OVERLAP_NOT_ALLOWED, MessageBoxIcon.Warning, strParamOverlap);	
							dtmRefresshingTo.Focus();
							return false;
						}
						//System will increment value of Refresshing To if Refresshing From > Refresshing To 
						if (ParseDateTime(DateTime.Parse(dtmRefresshingFrom.Value.ToString())) > ParseDateTime(DateTime.Parse(dtmRefresshingTo.Value.ToString())))
						{
							voPRO_ShiftPattern.RefreshingTo = ParseDateTime(DateTime.Parse(dtmRefresshingTo.Value.ToString())).AddDays(1);
						}
						else
							voPRO_ShiftPattern.RefreshingTo = ParseDateTime(DateTime.Parse(dtmRefresshingTo.Value.ToString()));
						voPRO_ShiftPattern.RefreshingFrom = ParseDateTime(DateTime.Parse(dtmRefresshingFrom.Value.ToString()));

						#region HACK: DEL Trada 10-17-2005

// Begin Del by SonHT 2005-10-09
//						//Refresshing not in Work Time
//						if ((voPRO_ShiftPattern.RefreshingFrom < voPRO_ShiftPattern.WorkTimeFrom)
//							||(voPRO_ShiftPattern.RefreshingFrom > voPRO_ShiftPattern.WorkTimeTo))
//						{
//							PCSMessageBox.Show(ErrorCode.MESSAGE_DCP_SHIFTPATTERN_REFRESSHING_TIME_MUSTBE_IN_WORKTIME, MessageBoxIcon.Warning);	
//							dtmRefresshingFrom.Focus();
//							dtmRefresshingFrom.Select();
//							return false;	
//						}
//						if ((voPRO_ShiftPattern.RefreshingTo > voPRO_ShiftPattern.WorkTimeTo)
//							||(voPRO_ShiftPattern.RefreshingTo < voPRO_ShiftPattern.WorkTimeFrom))
//						{
//							PCSMessageBox.Show(ErrorCode.MESSAGE_DCP_SHIFTPATTERN_REFRESSHING_TIME_MUSTBE_IN_WORKTIME, MessageBoxIcon.Warning);	
//							dtmRegularTo.Focus();
//							dtmRegularTo.Select();
//							return false;
//						}
// End Del by SonHT 2005-10-09

						#endregion END: DEL Trada 10-17-2005

						if (voPRO_ShiftPattern.RefreshingFrom < voPRO_ShiftPattern.WorkTimeFrom)
						{
							voPRO_ShiftPattern.RefreshingFrom = voPRO_ShiftPattern.RefreshingFrom.AddDays(1);
							voPRO_ShiftPattern.RefreshingTo = voPRO_ShiftPattern.RefreshingTo.AddDays(1);
						}
						if ((voPRO_ShiftPattern.RefreshingFrom < voPRO_ShiftPattern.WorkTimeFrom)
							||(voPRO_ShiftPattern.RefreshingFrom > voPRO_ShiftPattern.WorkTimeTo))
						{
							PCSMessageBox.Show(ErrorCode.MESSAGE_DCP_SHIFTPATTERN_REFRESSHING_TIME_MUSTBE_IN_WORKTIME, MessageBoxIcon.Warning);	
							dtmRefresshingFrom.Focus();
							dtmRefresshingFrom.Select();
							return false;	
						}
						if ((voPRO_ShiftPattern.RefreshingTo > voPRO_ShiftPattern.WorkTimeTo)
							||(voPRO_ShiftPattern.RefreshingTo < voPRO_ShiftPattern.WorkTimeFrom))
						{
							PCSMessageBox.Show(ErrorCode.MESSAGE_DCP_SHIFTPATTERN_REFRESSHING_TIME_MUSTBE_IN_WORKTIME, MessageBoxIcon.Warning);	
							dtmRefresshingTo.Focus();
							dtmRefresshingTo.Select();
							return false;
						}

					}
				}
				//Check value of Extra Stop Time
				if ((dtmExtraStopFrom.Value.ToString() != string.Empty) && (dtmExtraStopTo.Value.ToString() != string.Empty))
				{
					if ((dtmExtraStopFrom.Value != DBNull.Value) && (dtmExtraStopTo.Value != DBNull.Value))
					{
						//System will display error message if Extra Stop From = Extra Stop To 
						if (ParseDateTime(DateTime.Parse(dtmExtraStopFrom.Value.ToString())) == ParseDateTime(DateTime.Parse(dtmExtraStopTo.Value.ToString())))
						{
							strParamOverlap[0] = lblExtraStop.Text;
							PCSMessageBox.Show(ErrorCode.MESSAGE_OVERLAP_NOT_ALLOWED, MessageBoxIcon.Warning, strParamOverlap);	
							dtmExtraStopTo.Focus();
							return false;
						}
						//System will increment value of Extra Stop To if Extra Stop From > Extra Stop To 
						if (ParseDateTime(DateTime.Parse(dtmExtraStopFrom.Value.ToString())) > ParseDateTime(DateTime.Parse(dtmExtraStopTo.Value.ToString())))
						{
							voPRO_ShiftPattern.ExtraStopTo = ParseDateTime(DateTime.Parse(dtmExtraStopTo.Value.ToString())).AddDays(1);
						}
						else
							voPRO_ShiftPattern.ExtraStopTo = ParseDateTime(DateTime.Parse(dtmExtraStopTo.Value.ToString()));
						voPRO_ShiftPattern.ExtraStopFrom = ParseDateTime(DateTime.Parse(dtmExtraStopFrom.Value.ToString()));
						//Extra Stop not in Work Time

						#region HACK: DEL Trada 10-17-2005

// Begin Del by SonHT 2005-10-09
//						if ((voPRO_ShiftPattern.ExtraStopFrom < voPRO_ShiftPattern.WorkTimeFrom)
//							||(voPRO_ShiftPattern.ExtraStopFrom > voPRO_ShiftPattern.WorkTimeTo))
//						{
//							PCSMessageBox.Show(ErrorCode.MESSAGE_DCP_SHIFTPATTERN_EXTRASTOP_TIME_MUSTBE_IN_WORKTIME, MessageBoxIcon.Warning);	
//							dtmExtraStopFrom.Focus();
//							dtmExtraStopFrom.Select();
//							return false;	
//						}
//						if ((voPRO_ShiftPattern.ExtraStopTo > voPRO_ShiftPattern.WorkTimeTo)
//							||(voPRO_ShiftPattern.ExtraStopTo < voPRO_ShiftPattern.WorkTimeFrom))
//						{
//							PCSMessageBox.Show(ErrorCode.MESSAGE_DCP_SHIFTPATTERN_EXTRASTOP_TIME_MUSTBE_IN_WORKTIME, MessageBoxIcon.Warning);	
//							dtmExtraStopTo.Focus();
//							dtmExtraStopTo.Select();
//							return false;
//						}
// End Del by SonHT 2005-10-09

						#endregion END: DEL Trada 10-17-2005

						if (voPRO_ShiftPattern.ExtraStopFrom < voPRO_ShiftPattern.WorkTimeFrom)
						{
							voPRO_ShiftPattern.ExtraStopFrom = voPRO_ShiftPattern.ExtraStopFrom.AddDays(1);
							voPRO_ShiftPattern.ExtraStopTo = voPRO_ShiftPattern.ExtraStopTo.AddDays(1);
						}
						if ((voPRO_ShiftPattern.ExtraStopFrom < voPRO_ShiftPattern.WorkTimeFrom)
							||(voPRO_ShiftPattern.ExtraStopFrom > voPRO_ShiftPattern.WorkTimeTo))
						{
							PCSMessageBox.Show(ErrorCode.MESSAGE_DCP_SHIFTPATTERN_EXTRASTOP_TIME_MUSTBE_IN_WORKTIME, MessageBoxIcon.Warning);	
							dtmExtraStopFrom.Focus();
							dtmExtraStopFrom.Select();
							return false;	
						}
						if ((voPRO_ShiftPattern.ExtraStopTo > voPRO_ShiftPattern.WorkTimeTo)
							||(voPRO_ShiftPattern.ExtraStopTo < voPRO_ShiftPattern.WorkTimeFrom))
						{
							PCSMessageBox.Show(ErrorCode.MESSAGE_DCP_SHIFTPATTERN_EXTRASTOP_TIME_MUSTBE_IN_WORKTIME, MessageBoxIcon.Warning);	
							dtmExtraStopTo.Focus();
							dtmExtraStopTo.Select();
							return false;
						}

					}
				}

				// HACK: Trada 10-21-2005
				//Check if Regular Stop, Refreshing and Extra Stop are intersected
				//Check if Regular Stop and Refreshing are intersected
				if ((dtmRegularStopFrom.Value != DBNull.Value) && (dtmRefresshingFrom.Value != DBNull.Value))
				{
					if (IsIntersect(voPRO_ShiftPattern.RegularStopFrom, voPRO_ShiftPattern.RegularStopTo, voPRO_ShiftPattern.RefreshingFrom, voPRO_ShiftPattern.RefreshingTo))
					{
						strParam[0] = lblRegularStop.Text;
						strParam[1] = lblRefresshing.Text;
						PCSMessageBox.Show(ErrorCode.MESSAGE_INTERSECT_NOT_ALLOWED, MessageBoxIcon.Warning, strParam);	
						dtmRefresshingTo.Focus();
						return false;
					}
				}
				//Check if Regular Stop and Extra Stop are intersected
				if ((dtmRegularStopFrom.Value != DBNull.Value) && (dtmExtraStopFrom.Value != DBNull.Value))
				{
					if (IsIntersect(voPRO_ShiftPattern.RegularStopFrom, voPRO_ShiftPattern.RegularStopTo, voPRO_ShiftPattern.ExtraStopFrom, voPRO_ShiftPattern.ExtraStopTo))
					{
						strParam[0] = lblRegularStop.Text;
						strParam[1] = lblExtraStop.Text;
						PCSMessageBox.Show(ErrorCode.MESSAGE_INTERSECT_NOT_ALLOWED, MessageBoxIcon.Warning, strParam);	
						dtmExtraStopTo.Focus();
						return false;
					}
				}
				//Check if Refreshing and Extra Stop are intersected
				if ((dtmRefresshingFrom.Value != DBNull.Value) && (dtmExtraStopFrom.Value != DBNull.Value))
				{
					if (IsIntersect(voPRO_ShiftPattern.RefreshingFrom, voPRO_ShiftPattern.RefreshingTo, voPRO_ShiftPattern.ExtraStopFrom, voPRO_ShiftPattern.ExtraStopTo))
					{
						strParam[0] = lblExtraStop.Text;
						strParam[1] = lblRefresshing.Text;
						PCSMessageBox.Show(ErrorCode.MESSAGE_INTERSECT_NOT_ALLOWED, MessageBoxIcon.Warning, strParam);	
						dtmExtraStopTo.Focus();
						return false;
					}
				} 
				// END: Trada 10-21-2005
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
		/// btnSave_Click
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Friday, August 12 2005</date>
		private void btnSave_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnSave_Click()";
			try
			{
				blnHasError = true;
				btnSave.Focus();
				//Correct data for all DateTime controls
				if (IsValidateData())
				{	
					//Set value for PRO_ShiftPatternVO
					voPRO_ShiftPattern.CCNID = int.Parse(cboCCN.SelectedValue.ToString());
					voPRO_ShiftPattern.Comment = txtComment.Text.Trim();
					voPRO_ShiftPattern.ShiftID = pintShiftID;
					voPRO_ShiftPattern.EffectDateFrom = new DateTime(2005, 1, 1);
					
					if ((dtmExtraStopFrom.Value == null) || (dtmExtraStopFrom.Value == DBNull.Value))
					{
						voPRO_ShiftPattern.ExtraStopFrom = dtmSpecialDay;
					}
						
					if ((dtmExtraStopTo.Value == null) || (dtmExtraStopTo.Value == DBNull.Value))
					{
						voPRO_ShiftPattern.ExtraStopTo = dtmSpecialDay;
					}
					
					if ((dtmRefresshingFrom.Value == null) || (dtmRefresshingFrom.Value == DBNull.Value))
					{
						voPRO_ShiftPattern.RefreshingFrom = dtmSpecialDay;
					}
						
					if ((dtmRefresshingTo.Value == null) || (dtmRefresshingTo.Value == DBNull.Value))
					{
						voPRO_ShiftPattern.RefreshingTo = dtmSpecialDay;
					}
					
					if ((dtmRegularStopFrom.Value == null) || (dtmRegularStopFrom.Value == DBNull.Value))
					{
						voPRO_ShiftPattern.RegularStopFrom = dtmSpecialDay;
					}
						
					if ((dtmRegularTo.Value == null) || (dtmRegularTo.Value == DBNull.Value))
					{
						voPRO_ShiftPattern.RegularStopTo = dtmSpecialDay;
					}
					ShiftPatternBO boShiftPattern = new ShiftPatternBO();
					if (formMode == EnumAction.Add)
					{
						pintShiftPatternID = boShiftPattern.SaveShiftParttern(voPRO_ShiftPattern);
					}
					if (formMode == EnumAction.Edit)
					{
						boShiftPattern.Update(voPRO_ShiftPattern);
					}
					PCSMessageBox.Show(ErrorCode.MESSAGE_AFTER_SAVE_DATA);
					FillDataToControls(voPRO_ShiftPattern);
					formMode = EnumAction.Default;
					SwitchFormMode();
					btnDelete.Enabled = true;
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
		/// <summary>
		/// btnDelete_Click
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Monday, August 15 2005</date>
		private void btnDelete_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnDelete_Click()";
			try
			{
				if (PCSMessageBox.Show(ErrorCode.MESSAGE_DELETE_RECORD, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
				{
					ShiftPatternBO boShiftPattern = new ShiftPatternBO();
					boShiftPattern.Delete(pintShiftPatternID);
					PCSMessageBox.Show(ErrorCode.MESSAGE_AFTER_SAVE_DATA);
					formMode = EnumAction.Default;
					ClearForm();
					SwitchFormMode();
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
		/// <summary>
		/// btnClearWorkTimeFrom_Click
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnClearWorkTimeFrom_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnClearWorkTimeFrom_Click()";
			try
			{
				dtmWorkTimeFrom.Value = null;
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
		/// btnClearWorkTimeTo_Click
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Monday, August 15 2005</date>
		private void btnClearWorkTimeTo_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnClearWorkTimeTo_Click()";
			try
			{
				dtmWorkTimeTo.Value = null;
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
		/// btnClearRegularStopFrom_Click
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Monday, August 15 2005</date>
		private void btnClearRegularStopFrom_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnClearRegularStopFrom_Click()";
			try
			{
				dtmRegularStopFrom.Value = null;
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
		/// btnClearRegularStopTo_Click
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Monday, August 15 2005</date>
		private void btnClearRegularStopTo_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnClearRegularStopTo_Click()";
			try
			{
				dtmRegularTo.Value = null;
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
		/// btnClearRefressingFrom_Click
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Monday, August 15 2005</date>
		private void btnClearRefressingFrom_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnClearRefressingFrom_Click()";
			try
			{
				dtmRefresshingFrom.Value = null;
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
		/// btnClearRefressingTo_Click
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Monday, August 15 2005</date>
		private void btnClearRefressingTo_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnClearRefressingTo_Click()";
			try
			{
				dtmRefresshingTo.Value = null;
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
		/// btnClearExtraStopFrom_Click
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Monday, August 15 2005</date>
		private void btnClearExtraStopFrom_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnClearExtraStopFrom_Click()";
			try
			{
				dtmExtraStopFrom.Value = null;
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
		/// btnClearExtraStopTo_Click
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Monday, August 15 2005</date>
		private void btnClearExtraStopTo_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnClearExtraStopTo_Click()";
			try
			{
				dtmExtraStopTo.Value = null;
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
		/// ShiftPattern_Closing
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Tuesday, August 16 2005</date>
		private void ShiftPattern_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".MPSCycleOption_Closing()";
			try
			{
				if (formMode != EnumAction.Default)
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
		/// OnEnterControl
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Monday, October 3 2005</date>
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
		//**************************************************************************              
		///    <Description>
		///       OnLeaveControl
		///    </Description>
		///    <Inputs>
		///    </Inputs>
		///    <Outputs>
		///    </Outputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <Authors>
		///       Dungla
		///    </Authors>
		///    <History>
		///       Tuesday, March 29, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
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

	}
}
