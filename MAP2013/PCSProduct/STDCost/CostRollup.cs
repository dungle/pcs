using System;
using System.ComponentModel;
using System.Threading;
using System.Windows.Forms;
using C1.Win.C1Input;
using C1.Win.C1List;
using PCSComProduct.STDCost.BO;
using PCSComUtils.Common;
using PCSComUtils.Common.BO;
using PCSComUtils.PCSExc;
using PCSUtils.Log;
using PCSUtils.Utils;

namespace PCSProduct.STDCost
{
	/// <summary>
	/// Summary description for CostRollup.
	/// </summary>
	public class CostRollup : Form
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private Container components = null;

		const string THIS = "PCSProduct.STDCost.CostRollup";
		string OriginalMessage = string.Empty;
		private System.Windows.Forms.Label lblStandardCost;
		private System.Windows.Forms.Label lblProcessing;
		private System.Windows.Forms.PictureBox picProcessing;
		private C1.Win.C1Input.C1DateEdit dtmDate;
		private C1.Win.C1List.C1Combo cboCCN;
		private System.Windows.Forms.Label lblDate;
		private System.Windows.Forms.Label lblCCN;
		private System.Windows.Forms.Button btnHelp;
		private System.Windows.Forms.Button btnClose;
		private System.Windows.Forms.Button btnRollUp;
		Thread thrProcess = null;

		public CostRollup()
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(CostRollup));
			this.lblStandardCost = new System.Windows.Forms.Label();
			this.lblProcessing = new System.Windows.Forms.Label();
			this.picProcessing = new System.Windows.Forms.PictureBox();
			this.dtmDate = new C1.Win.C1Input.C1DateEdit();
			this.cboCCN = new C1.Win.C1List.C1Combo();
			this.lblDate = new System.Windows.Forms.Label();
			this.lblCCN = new System.Windows.Forms.Label();
			this.btnHelp = new System.Windows.Forms.Button();
			this.btnClose = new System.Windows.Forms.Button();
			this.btnRollUp = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.dtmDate)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.cboCCN)).BeginInit();
			this.SuspendLayout();
			// 
			// lblStandardCost
			// 
			this.lblStandardCost.AccessibleDescription = resources.GetString("lblStandardCost.AccessibleDescription");
			this.lblStandardCost.AccessibleName = resources.GetString("lblStandardCost.AccessibleName");
			this.lblStandardCost.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblStandardCost.Anchor")));
			this.lblStandardCost.AutoSize = ((bool)(resources.GetObject("lblStandardCost.AutoSize")));
			this.lblStandardCost.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblStandardCost.Dock")));
			this.lblStandardCost.Enabled = ((bool)(resources.GetObject("lblStandardCost.Enabled")));
			this.lblStandardCost.Font = ((System.Drawing.Font)(resources.GetObject("lblStandardCost.Font")));
			this.lblStandardCost.Image = ((System.Drawing.Image)(resources.GetObject("lblStandardCost.Image")));
			this.lblStandardCost.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblStandardCost.ImageAlign")));
			this.lblStandardCost.ImageIndex = ((int)(resources.GetObject("lblStandardCost.ImageIndex")));
			this.lblStandardCost.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblStandardCost.ImeMode")));
			this.lblStandardCost.Location = ((System.Drawing.Point)(resources.GetObject("lblStandardCost.Location")));
			this.lblStandardCost.Name = "lblStandardCost";
			this.lblStandardCost.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblStandardCost.RightToLeft")));
			this.lblStandardCost.Size = ((System.Drawing.Size)(resources.GetObject("lblStandardCost.Size")));
			this.lblStandardCost.TabIndex = ((int)(resources.GetObject("lblStandardCost.TabIndex")));
			this.lblStandardCost.Text = resources.GetString("lblStandardCost.Text");
			this.lblStandardCost.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblStandardCost.TextAlign")));
			this.lblStandardCost.Visible = ((bool)(resources.GetObject("lblStandardCost.Visible")));
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
			// dtmDate
			// 
			this.dtmDate.AcceptsEscape = ((bool)(resources.GetObject("dtmDate.AcceptsEscape")));
			this.dtmDate.AccessibleDescription = resources.GetString("dtmDate.AccessibleDescription");
			this.dtmDate.AccessibleName = resources.GetString("dtmDate.AccessibleName");
			this.dtmDate.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("dtmDate.Anchor")));
			this.dtmDate.AutoSize = ((bool)(resources.GetObject("dtmDate.AutoSize")));
			this.dtmDate.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("dtmDate.BackgroundImage")));
			this.dtmDate.BorderStyle = ((System.Windows.Forms.BorderStyle)(resources.GetObject("dtmDate.BorderStyle")));
			// 
			// dtmDate.Calendar
			// 
			this.dtmDate.Calendar.AccessibleDescription = resources.GetString("dtmDate.Calendar.AccessibleDescription");
			this.dtmDate.Calendar.AccessibleName = resources.GetString("dtmDate.Calendar.AccessibleName");
			this.dtmDate.Calendar.AnnuallyBoldedDates = ((System.DateTime[])(resources.GetObject("dtmDate.Calendar.AnnuallyBoldedDates")));
			this.dtmDate.Calendar.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("dtmDate.Calendar.BackgroundImage")));
			this.dtmDate.Calendar.BoldedDates = ((System.DateTime[])(resources.GetObject("dtmDate.Calendar.BoldedDates")));
			this.dtmDate.Calendar.CalendarDimensions = ((System.Drawing.Size)(resources.GetObject("dtmDate.Calendar.CalendarDimensions")));
			this.dtmDate.Calendar.Enabled = ((bool)(resources.GetObject("dtmDate.Calendar.Enabled")));
			this.dtmDate.Calendar.FirstDayOfWeek = ((System.Windows.Forms.Day)(resources.GetObject("dtmDate.Calendar.FirstDayOfWeek")));
			this.dtmDate.Calendar.Font = ((System.Drawing.Font)(resources.GetObject("dtmDate.Calendar.Font")));
			this.dtmDate.Calendar.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("dtmDate.Calendar.ImeMode")));
			this.dtmDate.Calendar.MonthlyBoldedDates = ((System.DateTime[])(resources.GetObject("dtmDate.Calendar.MonthlyBoldedDates")));
			this.dtmDate.Calendar.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("dtmDate.Calendar.RightToLeft")));
			this.dtmDate.Calendar.ShowClearButton = ((bool)(resources.GetObject("dtmDate.Calendar.ShowClearButton")));
			this.dtmDate.Calendar.ShowTodayButton = ((bool)(resources.GetObject("dtmDate.Calendar.ShowTodayButton")));
			this.dtmDate.Calendar.ShowWeekNumbers = ((bool)(resources.GetObject("dtmDate.Calendar.ShowWeekNumbers")));
			this.dtmDate.CaseSensitive = ((bool)(resources.GetObject("dtmDate.CaseSensitive")));
			this.dtmDate.Culture = ((int)(resources.GetObject("dtmDate.Culture")));
			this.dtmDate.CurrentTimeZone = ((bool)(resources.GetObject("dtmDate.CurrentTimeZone")));
			this.dtmDate.CustomFormat = resources.GetString("dtmDate.CustomFormat");
			this.dtmDate.DaylightTimeAdjustment = ((C1.Win.C1Input.DaylightTimeAdjustmentEnum)(resources.GetObject("dtmDate.DaylightTimeAdjustment")));
			this.dtmDate.DisableOnNoData = false;
			this.dtmDate.DisplayFormat.CustomFormat = resources.GetString("dtmDate.DisplayFormat.CustomFormat");
			this.dtmDate.DisplayFormat.FormatType = ((C1.Win.C1Input.FormatTypeEnum)(resources.GetObject("dtmDate.DisplayFormat.FormatType")));
			this.dtmDate.DisplayFormat.Inherit = ((C1.Win.C1Input.FormatInfoInheritFlags)(resources.GetObject("dtmDate.DisplayFormat.Inherit")));
			this.dtmDate.DisplayFormat.NullText = resources.GetString("dtmDate.DisplayFormat.NullText");
			this.dtmDate.DisplayFormat.TrimEnd = ((bool)(resources.GetObject("dtmDate.DisplayFormat.TrimEnd")));
			this.dtmDate.DisplayFormat.TrimStart = ((bool)(resources.GetObject("dtmDate.DisplayFormat.TrimStart")));
			this.dtmDate.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("dtmDate.Dock")));
			this.dtmDate.DropDownFormAlign = ((C1.Win.C1Input.DropDownFormAlignmentEnum)(resources.GetObject("dtmDate.DropDownFormAlign")));
			this.dtmDate.EditFormat.CustomFormat = resources.GetString("dtmDate.EditFormat.CustomFormat");
			this.dtmDate.EditFormat.FormatType = ((C1.Win.C1Input.FormatTypeEnum)(resources.GetObject("dtmDate.EditFormat.FormatType")));
			this.dtmDate.EditFormat.Inherit = ((C1.Win.C1Input.FormatInfoInheritFlags)(resources.GetObject("dtmDate.EditFormat.Inherit")));
			this.dtmDate.EditFormat.NullText = resources.GetString("dtmDate.EditFormat.NullText");
			this.dtmDate.EditFormat.TrimEnd = ((bool)(resources.GetObject("dtmDate.EditFormat.TrimEnd")));
			this.dtmDate.EditFormat.TrimStart = ((bool)(resources.GetObject("dtmDate.EditFormat.TrimStart")));
			this.dtmDate.EditMask = resources.GetString("dtmDate.EditMask");
			this.dtmDate.EmptyAsNull = ((bool)(resources.GetObject("dtmDate.EmptyAsNull")));
			this.dtmDate.Enabled = ((bool)(resources.GetObject("dtmDate.Enabled")));
			this.dtmDate.ErrorInfo.BeepOnError = ((bool)(resources.GetObject("dtmDate.ErrorInfo.BeepOnError")));
			this.dtmDate.ErrorInfo.ErrorMessage = resources.GetString("dtmDate.ErrorInfo.ErrorMessage");
			this.dtmDate.ErrorInfo.ErrorMessageCaption = resources.GetString("dtmDate.ErrorInfo.ErrorMessageCaption");
			this.dtmDate.ErrorInfo.ShowErrorMessage = ((bool)(resources.GetObject("dtmDate.ErrorInfo.ShowErrorMessage")));
			this.dtmDate.ErrorInfo.ValueOnError = ((object)(resources.GetObject("dtmDate.ErrorInfo.ValueOnError")));
			this.dtmDate.Font = ((System.Drawing.Font)(resources.GetObject("dtmDate.Font")));
			this.dtmDate.FormatType = ((C1.Win.C1Input.FormatTypeEnum)(resources.GetObject("dtmDate.FormatType")));
			this.dtmDate.GapHeight = ((int)(resources.GetObject("dtmDate.GapHeight")));
			this.dtmDate.GMTOffset = ((System.TimeSpan)(resources.GetObject("dtmDate.GMTOffset")));
			this.dtmDate.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("dtmDate.ImeMode")));
			this.dtmDate.InitialSelection = ((C1.Win.C1Input.InitialSelectionEnum)(resources.GetObject("dtmDate.InitialSelection")));
			this.dtmDate.Location = ((System.Drawing.Point)(resources.GetObject("dtmDate.Location")));
			this.dtmDate.MaskInfo.AutoTabWhenFilled = ((bool)(resources.GetObject("dtmDate.MaskInfo.AutoTabWhenFilled")));
			this.dtmDate.MaskInfo.CaseSensitive = ((bool)(resources.GetObject("dtmDate.MaskInfo.CaseSensitive")));
			this.dtmDate.MaskInfo.CopyWithLiterals = ((bool)(resources.GetObject("dtmDate.MaskInfo.CopyWithLiterals")));
			this.dtmDate.MaskInfo.EditMask = resources.GetString("dtmDate.MaskInfo.EditMask");
			this.dtmDate.MaskInfo.EmptyAsNull = ((bool)(resources.GetObject("dtmDate.MaskInfo.EmptyAsNull")));
			this.dtmDate.MaskInfo.ErrorMessage = resources.GetString("dtmDate.MaskInfo.ErrorMessage");
			this.dtmDate.MaskInfo.Inherit = ((C1.Win.C1Input.MaskInfoInheritFlags)(resources.GetObject("dtmDate.MaskInfo.Inherit")));
			this.dtmDate.MaskInfo.PromptChar = ((char)(resources.GetObject("dtmDate.MaskInfo.PromptChar")));
			this.dtmDate.MaskInfo.ShowLiterals = ((C1.Win.C1Input.ShowLiteralsEnum)(resources.GetObject("dtmDate.MaskInfo.ShowLiterals")));
			this.dtmDate.MaskInfo.StoredEmptyChar = ((char)(resources.GetObject("dtmDate.MaskInfo.StoredEmptyChar")));
			this.dtmDate.MaxLength = ((int)(resources.GetObject("dtmDate.MaxLength")));
			this.dtmDate.Name = "dtmDate";
			this.dtmDate.NullText = resources.GetString("dtmDate.NullText");
			this.dtmDate.ParseInfo.CaseSensitive = ((bool)(resources.GetObject("dtmDate.ParseInfo.CaseSensitive")));
			this.dtmDate.ParseInfo.CustomFormat = resources.GetString("dtmDate.ParseInfo.CustomFormat");
			this.dtmDate.ParseInfo.DateTimeStyle = ((C1.Win.C1Input.DateTimeStyleFlags)(resources.GetObject("dtmDate.ParseInfo.DateTimeStyle")));
			this.dtmDate.ParseInfo.EmptyAsNull = ((bool)(resources.GetObject("dtmDate.ParseInfo.EmptyAsNull")));
			this.dtmDate.ParseInfo.ErrorMessage = resources.GetString("dtmDate.ParseInfo.ErrorMessage");
			this.dtmDate.ParseInfo.FormatType = ((C1.Win.C1Input.FormatTypeEnum)(resources.GetObject("dtmDate.ParseInfo.FormatType")));
			this.dtmDate.ParseInfo.Inherit = ((C1.Win.C1Input.ParseInfoInheritFlags)(resources.GetObject("dtmDate.ParseInfo.Inherit")));
			this.dtmDate.ParseInfo.NullText = resources.GetString("dtmDate.ParseInfo.NullText");
			this.dtmDate.ParseInfo.TrimEnd = ((bool)(resources.GetObject("dtmDate.ParseInfo.TrimEnd")));
			this.dtmDate.ParseInfo.TrimStart = ((bool)(resources.GetObject("dtmDate.ParseInfo.TrimStart")));
			this.dtmDate.PasswordChar = ((char)(resources.GetObject("dtmDate.PasswordChar")));
			this.dtmDate.PostValidation.CaseSensitive = ((bool)(resources.GetObject("dtmDate.PostValidation.CaseSensitive")));
			this.dtmDate.PostValidation.ErrorMessage = resources.GetString("dtmDate.PostValidation.ErrorMessage");
			this.dtmDate.PostValidation.Inherit = ((C1.Win.C1Input.PostValidationInheritFlags)(resources.GetObject("dtmDate.PostValidation.Inherit")));
			this.dtmDate.PostValidation.Validation = ((C1.Win.C1Input.PostValidationTypeEnum)(resources.GetObject("dtmDate.PostValidation.Validation")));
			this.dtmDate.PostValidation.Values = ((System.Array)(resources.GetObject("dtmDate.PostValidation.Values")));
			this.dtmDate.PostValidation.ValuesExcluded = ((System.Array)(resources.GetObject("dtmDate.PostValidation.ValuesExcluded")));
			this.dtmDate.PreValidation.CaseSensitive = ((bool)(resources.GetObject("dtmDate.PreValidation.CaseSensitive")));
			this.dtmDate.PreValidation.ErrorMessage = resources.GetString("dtmDate.PreValidation.ErrorMessage");
			this.dtmDate.PreValidation.Inherit = ((C1.Win.C1Input.PreValidationInheritFlags)(resources.GetObject("dtmDate.PreValidation.Inherit")));
			this.dtmDate.PreValidation.ItemSeparator = resources.GetString("dtmDate.PreValidation.ItemSeparator");
			this.dtmDate.PreValidation.PatternString = resources.GetString("dtmDate.PreValidation.PatternString");
			this.dtmDate.PreValidation.RegexOptions = ((C1.Win.C1Input.RegexOptionFlags)(resources.GetObject("dtmDate.PreValidation.RegexOptions")));
			this.dtmDate.PreValidation.TrimEnd = ((bool)(resources.GetObject("dtmDate.PreValidation.TrimEnd")));
			this.dtmDate.PreValidation.TrimStart = ((bool)(resources.GetObject("dtmDate.PreValidation.TrimStart")));
			this.dtmDate.PreValidation.Validation = ((C1.Win.C1Input.PreValidationTypeEnum)(resources.GetObject("dtmDate.PreValidation.Validation")));
			this.dtmDate.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("dtmDate.RightToLeft")));
			this.dtmDate.ShowFocusRectangle = ((bool)(resources.GetObject("dtmDate.ShowFocusRectangle")));
			this.dtmDate.Size = ((System.Drawing.Size)(resources.GetObject("dtmDate.Size")));
			this.dtmDate.TabIndex = ((int)(resources.GetObject("dtmDate.TabIndex")));
			this.dtmDate.Tag = ((object)(resources.GetObject("dtmDate.Tag")));
			this.dtmDate.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("dtmDate.TextAlign")));
			this.dtmDate.TrimEnd = ((bool)(resources.GetObject("dtmDate.TrimEnd")));
			this.dtmDate.TrimStart = ((bool)(resources.GetObject("dtmDate.TrimStart")));
			this.dtmDate.UserCultureOverride = ((bool)(resources.GetObject("dtmDate.UserCultureOverride")));
			this.dtmDate.Value = ((object)(resources.GetObject("dtmDate.Value")));
			this.dtmDate.VerticalAlign = ((C1.Win.C1Input.VerticalAlignEnum)(resources.GetObject("dtmDate.VerticalAlign")));
			this.dtmDate.Visible = ((bool)(resources.GetObject("dtmDate.Visible")));
			this.dtmDate.VisibleButtons = ((C1.Win.C1Input.DropDownControlButtonFlags)(resources.GetObject("dtmDate.VisibleButtons")));
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
				" 118, 158</ClientRect><VScrollBar><Width>17</Width></VScrollBar><HScrollBar><Hei" +
				"ght>17</Height></HScrollBar><CaptionStyle parent=\"Style2\" me=\"Style9\" /><EvenRow" +
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
			// lblDate
			// 
			this.lblDate.AccessibleDescription = resources.GetString("lblDate.AccessibleDescription");
			this.lblDate.AccessibleName = resources.GetString("lblDate.AccessibleName");
			this.lblDate.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblDate.Anchor")));
			this.lblDate.AutoSize = ((bool)(resources.GetObject("lblDate.AutoSize")));
			this.lblDate.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblDate.Dock")));
			this.lblDate.Enabled = ((bool)(resources.GetObject("lblDate.Enabled")));
			this.lblDate.Font = ((System.Drawing.Font)(resources.GetObject("lblDate.Font")));
			this.lblDate.ForeColor = System.Drawing.Color.Maroon;
			this.lblDate.Image = ((System.Drawing.Image)(resources.GetObject("lblDate.Image")));
			this.lblDate.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblDate.ImageAlign")));
			this.lblDate.ImageIndex = ((int)(resources.GetObject("lblDate.ImageIndex")));
			this.lblDate.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblDate.ImeMode")));
			this.lblDate.Location = ((System.Drawing.Point)(resources.GetObject("lblDate.Location")));
			this.lblDate.Name = "lblDate";
			this.lblDate.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblDate.RightToLeft")));
			this.lblDate.Size = ((System.Drawing.Size)(resources.GetObject("lblDate.Size")));
			this.lblDate.TabIndex = ((int)(resources.GetObject("lblDate.TabIndex")));
			this.lblDate.Text = resources.GetString("lblDate.Text");
			this.lblDate.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblDate.TextAlign")));
			this.lblDate.Visible = ((bool)(resources.GetObject("lblDate.Visible")));
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
			// CostRollup
			// 
			this.AccessibleDescription = resources.GetString("$this.AccessibleDescription");
			this.AccessibleName = resources.GetString("$this.AccessibleName");
			this.AutoScaleBaseSize = ((System.Drawing.Size)(resources.GetObject("$this.AutoScaleBaseSize")));
			this.AutoScroll = ((bool)(resources.GetObject("$this.AutoScroll")));
			this.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("$this.AutoScrollMargin")));
			this.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("$this.AutoScrollMinSize")));
			this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
			this.ClientSize = ((System.Drawing.Size)(resources.GetObject("$this.ClientSize")));
			this.Controls.Add(this.lblProcessing);
			this.Controls.Add(this.picProcessing);
			this.Controls.Add(this.dtmDate);
			this.Controls.Add(this.cboCCN);
			this.Controls.Add(this.lblDate);
			this.Controls.Add(this.lblCCN);
			this.Controls.Add(this.btnHelp);
			this.Controls.Add(this.btnClose);
			this.Controls.Add(this.btnRollUp);
			this.Controls.Add(this.lblStandardCost);
			this.Enabled = ((bool)(resources.GetObject("$this.Enabled")));
			this.Font = ((System.Drawing.Font)(resources.GetObject("$this.Font")));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("$this.ImeMode")));
			this.Location = ((System.Drawing.Point)(resources.GetObject("$this.Location")));
			this.MaximizeBox = false;
			this.MaximumSize = ((System.Drawing.Size)(resources.GetObject("$this.MaximumSize")));
			this.MinimumSize = ((System.Drawing.Size)(resources.GetObject("$this.MinimumSize")));
			this.Name = "CostRollup";
			this.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("$this.RightToLeft")));
			this.StartPosition = ((System.Windows.Forms.FormStartPosition)(resources.GetObject("$this.StartPosition")));
			this.Text = resources.GetString("$this.Text");
			this.Closing += new System.ComponentModel.CancelEventHandler(this.CostRollup_Closing);
			this.Load += new System.EventHandler(this.CostRollup_Load);
			((System.ComponentModel.ISupportInitialize)(this.dtmDate)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.cboCCN)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// List CCN, fill default CCN
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void CostRollup_Load(object sender, EventArgs e)
		{
			const string METHOD_NAME = THIS + ".CostRollup_Load()";
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

				lblProcessing.Visible = false;
				picProcessing.Visible = false;

				UtilsBO boUtils = new UtilsBO();
				// put data into CCN combo
				FormControlComponents.PutDataIntoC1ComboBox(cboCCN, boUtils.ListCCN().Tables[0], MST_CCNTable.CODE_FLD, MST_CCNTable.CCNID_FLD, MST_CCNTable.TABLE_NAME);
				// set default CCN
				cboCCN.SelectedValue = SystemProperty.CCNID;
				// get current server date
				dtmDate.Value = boUtils.GetDBDate();
				OriginalMessage = this.Text;
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
		/// Rollup cost for all items in system
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnRollUp_Click(object sender, EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnRollUp_Click()";
			try
			{
				if (FormControlComponents.CheckMandatory(cboCCN))
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxButtons.OK, MessageBoxIcon.Error);
					cboCCN.Focus();
					cboCCN.Select();
					return;
				}
				if (FormControlComponents.CheckMandatory(dtmDate))
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxButtons.OK, MessageBoxIcon.Error);
					dtmDate.Focus();
					dtmDate.Select();
					return;
				}
				lblCCN.Enabled = false;
				lblDate.Enabled = false;
				cboCCN.ReadOnly = true;
				dtmDate.ReadOnly = true;
				lblProcessing.Visible = true;
				lblProcessing.Text = string.Format("{0} - {1}", lblProcessing.Text, this.Text);
				picProcessing.Visible = true;
				btnRollUp.Enabled = false;
				thrProcess = new Thread(new ThreadStart(RollUp));
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

		private void btnHelp_Click(object sender, EventArgs e)
		{
		
		}

		private void btnClose_Click(object sender, EventArgs e)
		{
			// close the form
			this.Close();
		}

		private void CostRollup_Closing(object sender, CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".CostRollup_Closing()";
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
								catch (ThreadAbortException ex)
								{
									Logger.LogMessage(ex.Message, METHOD_NAME, Level.DEBUG);
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
		/// Main function of roll up thread
		/// </summary>
		private void RollUp()
		{
			const string METHOD_NAME = THIS + ".RollUp()";
			try
			{
				CostRollupBO boCostRollUp = new CostRollupBO();
				DateTime dtmRollupDate = (DateTime)dtmDate.Value;
				int intCCNID = int.Parse(cboCCN.SelectedValue.ToString());
				boCostRollUp.RollUp(dtmRollupDate, intCCNID);
				string[] strMsg = new string[]{this.Text};
				PCSMessageBox.Show(ErrorCode.MESSAGE_TASK_COMPLETED, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, strMsg);
			}
			catch (ThreadAbortException ex)
			{
				Logger.LogMessage(ex, METHOD_NAME, Level.DEBUG);
			}
			catch (PCSException ex)
			{
				if (ex.mCode == ErrorCode.MESSAGE_CAN_NOT_DELETE)
				{
					string[] strMsg = new string[]{lblStandardCost.Text};
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
				lblCCN.Enabled = true;
				lblDate.Enabled = true;
				cboCCN.ReadOnly = false;
				dtmDate.ReadOnly = false;
				lblProcessing.Visible = false;
				lblProcessing.Text = OriginalMessage;
				picProcessing.Visible = false;
				btnRollUp.Enabled = true;
			}
		}
	}
}
