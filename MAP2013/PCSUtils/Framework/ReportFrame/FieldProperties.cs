using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using PCSComUtils.Common;
using PCSComUtils.Framework.ReportFrame.BO;
using PCSComUtils.Framework.ReportFrame.DS;
using PCSComUtils.PCSExc;
using PCSUtils.Log;
using PCSUtils.Utils;

namespace PCSUtils.Framework.ReportFrame
{
	/// <summary>
	/// Summary description for FieldProperties.
	/// </summary>
	public class FieldProperties : Form
	{
		private GroupBox grpFieldProperties;
		private Label lblSort;
		private Label lblColumnSummary;
		private GroupBox grpSummary;
		private Label lblFormat;
		private Label lblType;
		private CheckBox chkSumBottomPage;
		private CheckBox chkSumTopPage;
		private CheckBox chkSumTopReport;
		private CheckBox chkSumBottomReport;
		private Label lblWidth;
		private Label lblCaption;
		private Label lblFieldName;
		private CheckBox chkDisplayBottom;
		private CheckBox chkGroupBy;
		private Label lblColumnGrouping;
		private GroupBox grpColumnGrouping;
		private Button btnHelp;
		private Button btnSave;
		private Button btnClose;
		private CheckBox chkPrint;
		private ComboBox cboSort;
		private TextBox txtFormat;
		private ComboBox cboType;
		private Button btnFont;
		private TextBox txtCaption;
		private TextBox txtFieldName;
		private FontDialog dlgFont;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private Container components = null;
		private sys_ReportVO mvoReport;
		private FieldPropertiesBO boFieldProperties = new FieldPropertiesBO();
		private Label lblCaptionEN;
		private TextBox txtCaptionJP;
		private Label lblCaptionJP;
		private TextBox txtCaptionEN;
		private TextBox txtCaptionVN;
		private Label lblCaptionVN;
		private const string THIS = "PCSUtils.Framework.ReportFrame.FieldProperties";

		private ArrayList arrFields = new ArrayList();
		private sys_ReportFieldsVO voSelectedField;
		private System.Windows.Forms.TreeView tvwFieldList;
		private System.Windows.Forms.Button btnEdit;

		private EnumAction mFormAction = EnumAction.Default;
		private C1.Win.C1Input.C1NumericEdit nudWidth;
		private System.Windows.Forms.Button btnUp;
		private System.Windows.Forms.Button btnDown;
		private System.Windows.Forms.Label lblAlign;
		private System.Windows.Forms.ComboBox cboAlign;
		private System.Windows.Forms.Label lblUM;
		private System.Windows.Forms.Label lblDefaultFont;
		private System.Windows.Forms.CheckBox chkVisible;
		private const string FONT_SEPARATOR = "|";

		public EnumAction FormAction
		{
			get { return mFormAction; }
			set { mFormAction = value; }
		}

		public sys_ReportVO VoReport
		{
			get { return this.mvoReport; }
			set { this.mvoReport = value; }
		}

		public ArrayList Fields
		{
			get { return arrFields; }
			set { arrFields = value; }
		}

		//**************************************************************************              
		///    <Description>
		///       Default constructor
		///    </Description>
		///    <Inputs>
		///       
		///    </Inputs>
		///    <Outputs>
		///       
		///    </Outputs>
		///    <Returns>
		///       
		///    </Returns>
		///    <Authors>
		///       DungLA
		///    </Authors>
		///    <History>
		///       06-Jan-2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public FieldProperties()
		{
			//
			// Required for Windows Form Designer support
			//
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

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FieldProperties));
			this.btnUp = new System.Windows.Forms.Button();
			this.btnDown = new System.Windows.Forms.Button();
			this.grpFieldProperties = new System.Windows.Forms.GroupBox();
			this.lblUM = new System.Windows.Forms.Label();
			this.nudWidth = new C1.Win.C1Input.C1NumericEdit();
			this.txtCaptionVN = new System.Windows.Forms.TextBox();
			this.lblCaptionVN = new System.Windows.Forms.Label();
			this.txtCaptionEN = new System.Windows.Forms.TextBox();
			this.lblCaptionEN = new System.Windows.Forms.Label();
			this.txtCaptionJP = new System.Windows.Forms.TextBox();
			this.lblCaptionJP = new System.Windows.Forms.Label();
			this.chkPrint = new System.Windows.Forms.CheckBox();
			this.cboSort = new System.Windows.Forms.ComboBox();
			this.lblSort = new System.Windows.Forms.Label();
			this.lblColumnSummary = new System.Windows.Forms.Label();
			this.grpSummary = new System.Windows.Forms.GroupBox();
			this.chkDisplayBottom = new System.Windows.Forms.CheckBox();
			this.lblColumnGrouping = new System.Windows.Forms.Label();
			this.txtFormat = new System.Windows.Forms.TextBox();
			this.lblFormat = new System.Windows.Forms.Label();
			this.cboType = new System.Windows.Forms.ComboBox();
			this.lblType = new System.Windows.Forms.Label();
			this.chkSumBottomPage = new System.Windows.Forms.CheckBox();
			this.chkSumTopPage = new System.Windows.Forms.CheckBox();
			this.chkSumTopReport = new System.Windows.Forms.CheckBox();
			this.chkSumBottomReport = new System.Windows.Forms.CheckBox();
			this.chkGroupBy = new System.Windows.Forms.CheckBox();
			this.lblWidth = new System.Windows.Forms.Label();
			this.btnFont = new System.Windows.Forms.Button();
			this.txtCaption = new System.Windows.Forms.TextBox();
			this.lblCaption = new System.Windows.Forms.Label();
			this.txtFieldName = new System.Windows.Forms.TextBox();
			this.lblFieldName = new System.Windows.Forms.Label();
			this.grpColumnGrouping = new System.Windows.Forms.GroupBox();
			this.lblAlign = new System.Windows.Forms.Label();
			this.cboAlign = new System.Windows.Forms.ComboBox();
			this.chkVisible = new System.Windows.Forms.CheckBox();
			this.btnHelp = new System.Windows.Forms.Button();
			this.btnSave = new System.Windows.Forms.Button();
			this.btnClose = new System.Windows.Forms.Button();
			this.dlgFont = new System.Windows.Forms.FontDialog();
			this.tvwFieldList = new System.Windows.Forms.TreeView();
			this.btnEdit = new System.Windows.Forms.Button();
			this.lblDefaultFont = new System.Windows.Forms.Label();
			this.grpFieldProperties.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.nudWidth)).BeginInit();
			this.SuspendLayout();
			// 
			// btnUp
			// 
			this.btnUp.AccessibleDescription = resources.GetString("btnUp.AccessibleDescription");
			this.btnUp.AccessibleName = resources.GetString("btnUp.AccessibleName");
			this.btnUp.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnUp.Anchor")));
			this.btnUp.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnUp.BackgroundImage")));
			this.btnUp.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnUp.Dock")));
			this.btnUp.Enabled = ((bool)(resources.GetObject("btnUp.Enabled")));
			this.btnUp.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnUp.FlatStyle")));
			this.btnUp.Font = ((System.Drawing.Font)(resources.GetObject("btnUp.Font")));
			this.btnUp.Image = ((System.Drawing.Image)(resources.GetObject("btnUp.Image")));
			this.btnUp.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnUp.ImageAlign")));
			this.btnUp.ImageIndex = ((int)(resources.GetObject("btnUp.ImageIndex")));
			this.btnUp.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnUp.ImeMode")));
			this.btnUp.Location = ((System.Drawing.Point)(resources.GetObject("btnUp.Location")));
			this.btnUp.Name = "btnUp";
			this.btnUp.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnUp.RightToLeft")));
			this.btnUp.Size = ((System.Drawing.Size)(resources.GetObject("btnUp.Size")));
			this.btnUp.TabIndex = ((int)(resources.GetObject("btnUp.TabIndex")));
			this.btnUp.Text = resources.GetString("btnUp.Text");
			this.btnUp.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnUp.TextAlign")));
			this.btnUp.Visible = ((bool)(resources.GetObject("btnUp.Visible")));
			this.btnUp.Click += new System.EventHandler(this.btnUp_Click);
			// 
			// btnDown
			// 
			this.btnDown.AccessibleDescription = resources.GetString("btnDown.AccessibleDescription");
			this.btnDown.AccessibleName = resources.GetString("btnDown.AccessibleName");
			this.btnDown.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnDown.Anchor")));
			this.btnDown.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnDown.BackgroundImage")));
			this.btnDown.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnDown.Dock")));
			this.btnDown.Enabled = ((bool)(resources.GetObject("btnDown.Enabled")));
			this.btnDown.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnDown.FlatStyle")));
			this.btnDown.Font = ((System.Drawing.Font)(resources.GetObject("btnDown.Font")));
			this.btnDown.Image = ((System.Drawing.Image)(resources.GetObject("btnDown.Image")));
			this.btnDown.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnDown.ImageAlign")));
			this.btnDown.ImageIndex = ((int)(resources.GetObject("btnDown.ImageIndex")));
			this.btnDown.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnDown.ImeMode")));
			this.btnDown.Location = ((System.Drawing.Point)(resources.GetObject("btnDown.Location")));
			this.btnDown.Name = "btnDown";
			this.btnDown.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnDown.RightToLeft")));
			this.btnDown.Size = ((System.Drawing.Size)(resources.GetObject("btnDown.Size")));
			this.btnDown.TabIndex = ((int)(resources.GetObject("btnDown.TabIndex")));
			this.btnDown.Text = resources.GetString("btnDown.Text");
			this.btnDown.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnDown.TextAlign")));
			this.btnDown.Visible = ((bool)(resources.GetObject("btnDown.Visible")));
			this.btnDown.Click += new System.EventHandler(this.btnDown_Click);
			// 
			// grpFieldProperties
			// 
			this.grpFieldProperties.AccessibleDescription = resources.GetString("grpFieldProperties.AccessibleDescription");
			this.grpFieldProperties.AccessibleName = resources.GetString("grpFieldProperties.AccessibleName");
			this.grpFieldProperties.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("grpFieldProperties.Anchor")));
			this.grpFieldProperties.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("grpFieldProperties.BackgroundImage")));
			this.grpFieldProperties.Controls.Add(this.lblUM);
			this.grpFieldProperties.Controls.Add(this.nudWidth);
			this.grpFieldProperties.Controls.Add(this.txtCaptionVN);
			this.grpFieldProperties.Controls.Add(this.lblCaptionVN);
			this.grpFieldProperties.Controls.Add(this.txtCaptionEN);
			this.grpFieldProperties.Controls.Add(this.lblCaptionEN);
			this.grpFieldProperties.Controls.Add(this.txtCaptionJP);
			this.grpFieldProperties.Controls.Add(this.lblCaptionJP);
			this.grpFieldProperties.Controls.Add(this.chkPrint);
			this.grpFieldProperties.Controls.Add(this.cboSort);
			this.grpFieldProperties.Controls.Add(this.lblSort);
			this.grpFieldProperties.Controls.Add(this.lblColumnSummary);
			this.grpFieldProperties.Controls.Add(this.grpSummary);
			this.grpFieldProperties.Controls.Add(this.chkDisplayBottom);
			this.grpFieldProperties.Controls.Add(this.lblColumnGrouping);
			this.grpFieldProperties.Controls.Add(this.txtFormat);
			this.grpFieldProperties.Controls.Add(this.lblFormat);
			this.grpFieldProperties.Controls.Add(this.cboType);
			this.grpFieldProperties.Controls.Add(this.lblType);
			this.grpFieldProperties.Controls.Add(this.chkSumBottomPage);
			this.grpFieldProperties.Controls.Add(this.chkSumTopPage);
			this.grpFieldProperties.Controls.Add(this.chkSumTopReport);
			this.grpFieldProperties.Controls.Add(this.chkSumBottomReport);
			this.grpFieldProperties.Controls.Add(this.chkGroupBy);
			this.grpFieldProperties.Controls.Add(this.lblWidth);
			this.grpFieldProperties.Controls.Add(this.btnFont);
			this.grpFieldProperties.Controls.Add(this.txtCaption);
			this.grpFieldProperties.Controls.Add(this.lblCaption);
			this.grpFieldProperties.Controls.Add(this.txtFieldName);
			this.grpFieldProperties.Controls.Add(this.lblFieldName);
			this.grpFieldProperties.Controls.Add(this.grpColumnGrouping);
			this.grpFieldProperties.Controls.Add(this.lblAlign);
			this.grpFieldProperties.Controls.Add(this.cboAlign);
			this.grpFieldProperties.Controls.Add(this.chkVisible);
			this.grpFieldProperties.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("grpFieldProperties.Dock")));
			this.grpFieldProperties.Enabled = ((bool)(resources.GetObject("grpFieldProperties.Enabled")));
			this.grpFieldProperties.Font = ((System.Drawing.Font)(resources.GetObject("grpFieldProperties.Font")));
			this.grpFieldProperties.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("grpFieldProperties.ImeMode")));
			this.grpFieldProperties.Location = ((System.Drawing.Point)(resources.GetObject("grpFieldProperties.Location")));
			this.grpFieldProperties.Name = "grpFieldProperties";
			this.grpFieldProperties.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("grpFieldProperties.RightToLeft")));
			this.grpFieldProperties.Size = ((System.Drawing.Size)(resources.GetObject("grpFieldProperties.Size")));
			this.grpFieldProperties.TabIndex = ((int)(resources.GetObject("grpFieldProperties.TabIndex")));
			this.grpFieldProperties.TabStop = false;
			this.grpFieldProperties.Text = resources.GetString("grpFieldProperties.Text");
			this.grpFieldProperties.Visible = ((bool)(resources.GetObject("grpFieldProperties.Visible")));
			// 
			// lblUM
			// 
			this.lblUM.AccessibleDescription = resources.GetString("lblUM.AccessibleDescription");
			this.lblUM.AccessibleName = resources.GetString("lblUM.AccessibleName");
			this.lblUM.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblUM.Anchor")));
			this.lblUM.AutoSize = ((bool)(resources.GetObject("lblUM.AutoSize")));
			this.lblUM.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblUM.Dock")));
			this.lblUM.Enabled = ((bool)(resources.GetObject("lblUM.Enabled")));
			this.lblUM.Font = ((System.Drawing.Font)(resources.GetObject("lblUM.Font")));
			this.lblUM.Image = ((System.Drawing.Image)(resources.GetObject("lblUM.Image")));
			this.lblUM.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblUM.ImageAlign")));
			this.lblUM.ImageIndex = ((int)(resources.GetObject("lblUM.ImageIndex")));
			this.lblUM.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblUM.ImeMode")));
			this.lblUM.Location = ((System.Drawing.Point)(resources.GetObject("lblUM.Location")));
			this.lblUM.Name = "lblUM";
			this.lblUM.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblUM.RightToLeft")));
			this.lblUM.Size = ((System.Drawing.Size)(resources.GetObject("lblUM.Size")));
			this.lblUM.TabIndex = ((int)(resources.GetObject("lblUM.TabIndex")));
			this.lblUM.Text = resources.GetString("lblUM.Text");
			this.lblUM.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblUM.TextAlign")));
			this.lblUM.Visible = ((bool)(resources.GetObject("lblUM.Visible")));
			// 
			// nudWidth
			// 
			this.nudWidth.AcceptsEscape = ((bool)(resources.GetObject("nudWidth.AcceptsEscape")));
			this.nudWidth.AccessibleDescription = resources.GetString("nudWidth.AccessibleDescription");
			this.nudWidth.AccessibleName = resources.GetString("nudWidth.AccessibleName");
			this.nudWidth.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("nudWidth.Anchor")));
			this.nudWidth.AutoSize = ((bool)(resources.GetObject("nudWidth.AutoSize")));
			this.nudWidth.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("nudWidth.BackgroundImage")));
			this.nudWidth.BorderStyle = ((System.Windows.Forms.BorderStyle)(resources.GetObject("nudWidth.BorderStyle")));
			// 
			// nudWidth.Calculator
			// 
			this.nudWidth.Calculator.AccessibleDescription = resources.GetString("nudWidth.Calculator.AccessibleDescription");
			this.nudWidth.Calculator.AccessibleName = resources.GetString("nudWidth.Calculator.AccessibleName");
			this.nudWidth.Calculator.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("nudWidth.Calculator.BackgroundImage")));
			this.nudWidth.Calculator.ButtonFlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("nudWidth.Calculator.ButtonFlatStyle")));
			this.nudWidth.Calculator.DisplayFormat = resources.GetString("nudWidth.Calculator.DisplayFormat");
			this.nudWidth.Calculator.Font = ((System.Drawing.Font)(resources.GetObject("nudWidth.Calculator.Font")));
			this.nudWidth.Calculator.FormatOnClose = ((bool)(resources.GetObject("nudWidth.Calculator.FormatOnClose")));
			this.nudWidth.Calculator.StoredFormat = resources.GetString("nudWidth.Calculator.StoredFormat");
			this.nudWidth.Calculator.UIStrings.Content = ((string[])(resources.GetObject("nudWidth.Calculator.UIStrings.Content")));
			this.nudWidth.CaseSensitive = ((bool)(resources.GetObject("nudWidth.CaseSensitive")));
			this.nudWidth.Culture = ((int)(resources.GetObject("nudWidth.Culture")));
			this.nudWidth.CustomFormat = resources.GetString("nudWidth.CustomFormat");
			this.nudWidth.DataType = ((System.Type)(resources.GetObject("nudWidth.DataType")));
			this.nudWidth.DisplayFormat.CustomFormat = resources.GetString("nudWidth.DisplayFormat.CustomFormat");
			this.nudWidth.DisplayFormat.FormatType = ((C1.Win.C1Input.FormatTypeEnum)(resources.GetObject("nudWidth.DisplayFormat.FormatType")));
			this.nudWidth.DisplayFormat.Inherit = ((C1.Win.C1Input.FormatInfoInheritFlags)(resources.GetObject("nudWidth.DisplayFormat.Inherit")));
			this.nudWidth.DisplayFormat.NullText = resources.GetString("nudWidth.DisplayFormat.NullText");
			this.nudWidth.DisplayFormat.TrimEnd = ((bool)(resources.GetObject("nudWidth.DisplayFormat.TrimEnd")));
			this.nudWidth.DisplayFormat.TrimStart = ((bool)(resources.GetObject("nudWidth.DisplayFormat.TrimStart")));
			this.nudWidth.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("nudWidth.Dock")));
			this.nudWidth.DropDownFormAlign = ((C1.Win.C1Input.DropDownFormAlignmentEnum)(resources.GetObject("nudWidth.DropDownFormAlign")));
			this.nudWidth.EditFormat.CustomFormat = resources.GetString("nudWidth.EditFormat.CustomFormat");
			this.nudWidth.EditFormat.FormatType = ((C1.Win.C1Input.FormatTypeEnum)(resources.GetObject("nudWidth.EditFormat.FormatType")));
			this.nudWidth.EditFormat.Inherit = ((C1.Win.C1Input.FormatInfoInheritFlags)(resources.GetObject("nudWidth.EditFormat.Inherit")));
			this.nudWidth.EditFormat.NullText = resources.GetString("nudWidth.EditFormat.NullText");
			this.nudWidth.EditFormat.TrimEnd = ((bool)(resources.GetObject("nudWidth.EditFormat.TrimEnd")));
			this.nudWidth.EditFormat.TrimStart = ((bool)(resources.GetObject("nudWidth.EditFormat.TrimStart")));
			this.nudWidth.EditMask = resources.GetString("nudWidth.EditMask");
			this.nudWidth.EmptyAsNull = ((bool)(resources.GetObject("nudWidth.EmptyAsNull")));
			this.nudWidth.Enabled = ((bool)(resources.GetObject("nudWidth.Enabled")));
			this.nudWidth.ErrorInfo.BeepOnError = ((bool)(resources.GetObject("nudWidth.ErrorInfo.BeepOnError")));
			this.nudWidth.ErrorInfo.ErrorMessage = resources.GetString("nudWidth.ErrorInfo.ErrorMessage");
			this.nudWidth.ErrorInfo.ErrorMessageCaption = resources.GetString("nudWidth.ErrorInfo.ErrorMessageCaption");
			this.nudWidth.ErrorInfo.ShowErrorMessage = ((bool)(resources.GetObject("nudWidth.ErrorInfo.ShowErrorMessage")));
			this.nudWidth.ErrorInfo.ValueOnError = ((object)(resources.GetObject("nudWidth.ErrorInfo.ValueOnError")));
			this.nudWidth.Font = ((System.Drawing.Font)(resources.GetObject("nudWidth.Font")));
			this.nudWidth.FormatType = ((C1.Win.C1Input.FormatTypeEnum)(resources.GetObject("nudWidth.FormatType")));
			this.nudWidth.GapHeight = ((int)(resources.GetObject("nudWidth.GapHeight")));
			this.nudWidth.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("nudWidth.ImeMode")));
			this.nudWidth.Increment = ((object)(resources.GetObject("nudWidth.Increment")));
			this.nudWidth.InitialSelection = ((C1.Win.C1Input.InitialSelectionEnum)(resources.GetObject("nudWidth.InitialSelection")));
			this.nudWidth.Location = ((System.Drawing.Point)(resources.GetObject("nudWidth.Location")));
			this.nudWidth.MaskInfo.AutoTabWhenFilled = ((bool)(resources.GetObject("nudWidth.MaskInfo.AutoTabWhenFilled")));
			this.nudWidth.MaskInfo.CaseSensitive = ((bool)(resources.GetObject("nudWidth.MaskInfo.CaseSensitive")));
			this.nudWidth.MaskInfo.CopyWithLiterals = ((bool)(resources.GetObject("nudWidth.MaskInfo.CopyWithLiterals")));
			this.nudWidth.MaskInfo.EditMask = resources.GetString("nudWidth.MaskInfo.EditMask");
			this.nudWidth.MaskInfo.EmptyAsNull = ((bool)(resources.GetObject("nudWidth.MaskInfo.EmptyAsNull")));
			this.nudWidth.MaskInfo.ErrorMessage = resources.GetString("nudWidth.MaskInfo.ErrorMessage");
			this.nudWidth.MaskInfo.Inherit = ((C1.Win.C1Input.MaskInfoInheritFlags)(resources.GetObject("nudWidth.MaskInfo.Inherit")));
			this.nudWidth.MaskInfo.PromptChar = ((char)(resources.GetObject("nudWidth.MaskInfo.PromptChar")));
			this.nudWidth.MaskInfo.ShowLiterals = ((C1.Win.C1Input.ShowLiteralsEnum)(resources.GetObject("nudWidth.MaskInfo.ShowLiterals")));
			this.nudWidth.MaskInfo.StoredEmptyChar = ((char)(resources.GetObject("nudWidth.MaskInfo.StoredEmptyChar")));
			this.nudWidth.MaxLength = ((int)(resources.GetObject("nudWidth.MaxLength")));
			this.nudWidth.Name = "nudWidth";
			this.nudWidth.NullText = resources.GetString("nudWidth.NullText");
			this.nudWidth.ParseInfo.CaseSensitive = ((bool)(resources.GetObject("nudWidth.ParseInfo.CaseSensitive")));
			this.nudWidth.ParseInfo.CustomFormat = resources.GetString("nudWidth.ParseInfo.CustomFormat");
			this.nudWidth.ParseInfo.EmptyAsNull = ((bool)(resources.GetObject("nudWidth.ParseInfo.EmptyAsNull")));
			this.nudWidth.ParseInfo.ErrorMessage = resources.GetString("nudWidth.ParseInfo.ErrorMessage");
			this.nudWidth.ParseInfo.FormatType = ((C1.Win.C1Input.FormatTypeEnum)(resources.GetObject("nudWidth.ParseInfo.FormatType")));
			this.nudWidth.ParseInfo.Inherit = ((C1.Win.C1Input.ParseInfoInheritFlags)(resources.GetObject("nudWidth.ParseInfo.Inherit")));
			this.nudWidth.ParseInfo.NullText = resources.GetString("nudWidth.ParseInfo.NullText");
			this.nudWidth.ParseInfo.NumberStyle = ((C1.Win.C1Input.NumberStyleFlags)(resources.GetObject("nudWidth.ParseInfo.NumberStyle")));
			this.nudWidth.ParseInfo.TrimEnd = ((bool)(resources.GetObject("nudWidth.ParseInfo.TrimEnd")));
			this.nudWidth.ParseInfo.TrimStart = ((bool)(resources.GetObject("nudWidth.ParseInfo.TrimStart")));
			this.nudWidth.PasswordChar = ((char)(resources.GetObject("nudWidth.PasswordChar")));
			this.nudWidth.PostValidation.CaseSensitive = ((bool)(resources.GetObject("nudWidth.PostValidation.CaseSensitive")));
			this.nudWidth.PostValidation.ErrorMessage = resources.GetString("nudWidth.PostValidation.ErrorMessage");
			this.nudWidth.PostValidation.Inherit = ((C1.Win.C1Input.PostValidationInheritFlags)(resources.GetObject("nudWidth.PostValidation.Inherit")));
			this.nudWidth.PostValidation.Intervals.AddRange(new C1.Win.C1Input.ValueInterval[] {
																								   ((C1.Win.C1Input.ValueInterval)(resources.GetObject("nudWidth.PostValidation.Intervals")))});
			this.nudWidth.PostValidation.Validation = ((C1.Win.C1Input.PostValidationTypeEnum)(resources.GetObject("nudWidth.PostValidation.Validation")));
			this.nudWidth.PostValidation.Values = ((System.Array)(resources.GetObject("nudWidth.PostValidation.Values")));
			this.nudWidth.PostValidation.ValuesExcluded = ((System.Array)(resources.GetObject("nudWidth.PostValidation.ValuesExcluded")));
			this.nudWidth.PreValidation.CaseSensitive = ((bool)(resources.GetObject("nudWidth.PreValidation.CaseSensitive")));
			this.nudWidth.PreValidation.ErrorMessage = resources.GetString("nudWidth.PreValidation.ErrorMessage");
			this.nudWidth.PreValidation.Inherit = ((C1.Win.C1Input.PreValidationInheritFlags)(resources.GetObject("nudWidth.PreValidation.Inherit")));
			this.nudWidth.PreValidation.ItemSeparator = resources.GetString("nudWidth.PreValidation.ItemSeparator");
			this.nudWidth.PreValidation.PatternString = resources.GetString("nudWidth.PreValidation.PatternString");
			this.nudWidth.PreValidation.RegexOptions = ((C1.Win.C1Input.RegexOptionFlags)(resources.GetObject("nudWidth.PreValidation.RegexOptions")));
			this.nudWidth.PreValidation.TrimEnd = ((bool)(resources.GetObject("nudWidth.PreValidation.TrimEnd")));
			this.nudWidth.PreValidation.TrimStart = ((bool)(resources.GetObject("nudWidth.PreValidation.TrimStart")));
			this.nudWidth.PreValidation.Validation = ((C1.Win.C1Input.PreValidationTypeEnum)(resources.GetObject("nudWidth.PreValidation.Validation")));
			this.nudWidth.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("nudWidth.RightToLeft")));
			this.nudWidth.ShowFocusRectangle = ((bool)(resources.GetObject("nudWidth.ShowFocusRectangle")));
			this.nudWidth.Size = ((System.Drawing.Size)(resources.GetObject("nudWidth.Size")));
			this.nudWidth.TabIndex = ((int)(resources.GetObject("nudWidth.TabIndex")));
			this.nudWidth.Tag = ((object)(resources.GetObject("nudWidth.Tag")));
			this.nudWidth.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("nudWidth.TextAlign")));
			this.nudWidth.TrimEnd = ((bool)(resources.GetObject("nudWidth.TrimEnd")));
			this.nudWidth.TrimStart = ((bool)(resources.GetObject("nudWidth.TrimStart")));
			this.nudWidth.UserCultureOverride = ((bool)(resources.GetObject("nudWidth.UserCultureOverride")));
			this.nudWidth.Value = ((object)(resources.GetObject("nudWidth.Value")));
			this.nudWidth.VerticalAlign = ((C1.Win.C1Input.VerticalAlignEnum)(resources.GetObject("nudWidth.VerticalAlign")));
			this.nudWidth.Visible = ((bool)(resources.GetObject("nudWidth.Visible")));
			this.nudWidth.VisibleButtons = ((C1.Win.C1Input.DropDownControlButtonFlags)(resources.GetObject("nudWidth.VisibleButtons")));
			// 
			// txtCaptionVN
			// 
			this.txtCaptionVN.AccessibleDescription = resources.GetString("txtCaptionVN.AccessibleDescription");
			this.txtCaptionVN.AccessibleName = resources.GetString("txtCaptionVN.AccessibleName");
			this.txtCaptionVN.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("txtCaptionVN.Anchor")));
			this.txtCaptionVN.AutoSize = ((bool)(resources.GetObject("txtCaptionVN.AutoSize")));
			this.txtCaptionVN.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("txtCaptionVN.BackgroundImage")));
			this.txtCaptionVN.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("txtCaptionVN.Dock")));
			this.txtCaptionVN.Enabled = ((bool)(resources.GetObject("txtCaptionVN.Enabled")));
			this.txtCaptionVN.Font = ((System.Drawing.Font)(resources.GetObject("txtCaptionVN.Font")));
			this.txtCaptionVN.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("txtCaptionVN.ImeMode")));
			this.txtCaptionVN.Location = ((System.Drawing.Point)(resources.GetObject("txtCaptionVN.Location")));
			this.txtCaptionVN.MaxLength = ((int)(resources.GetObject("txtCaptionVN.MaxLength")));
			this.txtCaptionVN.Multiline = ((bool)(resources.GetObject("txtCaptionVN.Multiline")));
			this.txtCaptionVN.Name = "txtCaptionVN";
			this.txtCaptionVN.PasswordChar = ((char)(resources.GetObject("txtCaptionVN.PasswordChar")));
			this.txtCaptionVN.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("txtCaptionVN.RightToLeft")));
			this.txtCaptionVN.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("txtCaptionVN.ScrollBars")));
			this.txtCaptionVN.Size = ((System.Drawing.Size)(resources.GetObject("txtCaptionVN.Size")));
			this.txtCaptionVN.TabIndex = ((int)(resources.GetObject("txtCaptionVN.TabIndex")));
			this.txtCaptionVN.Text = resources.GetString("txtCaptionVN.Text");
			this.txtCaptionVN.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("txtCaptionVN.TextAlign")));
			this.txtCaptionVN.Visible = ((bool)(resources.GetObject("txtCaptionVN.Visible")));
			this.txtCaptionVN.WordWrap = ((bool)(resources.GetObject("txtCaptionVN.WordWrap")));
			// 
			// lblCaptionVN
			// 
			this.lblCaptionVN.AccessibleDescription = resources.GetString("lblCaptionVN.AccessibleDescription");
			this.lblCaptionVN.AccessibleName = resources.GetString("lblCaptionVN.AccessibleName");
			this.lblCaptionVN.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblCaptionVN.Anchor")));
			this.lblCaptionVN.AutoSize = ((bool)(resources.GetObject("lblCaptionVN.AutoSize")));
			this.lblCaptionVN.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblCaptionVN.Dock")));
			this.lblCaptionVN.Enabled = ((bool)(resources.GetObject("lblCaptionVN.Enabled")));
			this.lblCaptionVN.Font = ((System.Drawing.Font)(resources.GetObject("lblCaptionVN.Font")));
			this.lblCaptionVN.ForeColor = System.Drawing.Color.Maroon;
			this.lblCaptionVN.Image = ((System.Drawing.Image)(resources.GetObject("lblCaptionVN.Image")));
			this.lblCaptionVN.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblCaptionVN.ImageAlign")));
			this.lblCaptionVN.ImageIndex = ((int)(resources.GetObject("lblCaptionVN.ImageIndex")));
			this.lblCaptionVN.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblCaptionVN.ImeMode")));
			this.lblCaptionVN.Location = ((System.Drawing.Point)(resources.GetObject("lblCaptionVN.Location")));
			this.lblCaptionVN.Name = "lblCaptionVN";
			this.lblCaptionVN.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblCaptionVN.RightToLeft")));
			this.lblCaptionVN.Size = ((System.Drawing.Size)(resources.GetObject("lblCaptionVN.Size")));
			this.lblCaptionVN.TabIndex = ((int)(resources.GetObject("lblCaptionVN.TabIndex")));
			this.lblCaptionVN.Text = resources.GetString("lblCaptionVN.Text");
			this.lblCaptionVN.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblCaptionVN.TextAlign")));
			this.lblCaptionVN.Visible = ((bool)(resources.GetObject("lblCaptionVN.Visible")));
			// 
			// txtCaptionEN
			// 
			this.txtCaptionEN.AccessibleDescription = resources.GetString("txtCaptionEN.AccessibleDescription");
			this.txtCaptionEN.AccessibleName = resources.GetString("txtCaptionEN.AccessibleName");
			this.txtCaptionEN.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("txtCaptionEN.Anchor")));
			this.txtCaptionEN.AutoSize = ((bool)(resources.GetObject("txtCaptionEN.AutoSize")));
			this.txtCaptionEN.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("txtCaptionEN.BackgroundImage")));
			this.txtCaptionEN.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("txtCaptionEN.Dock")));
			this.txtCaptionEN.Enabled = ((bool)(resources.GetObject("txtCaptionEN.Enabled")));
			this.txtCaptionEN.Font = ((System.Drawing.Font)(resources.GetObject("txtCaptionEN.Font")));
			this.txtCaptionEN.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("txtCaptionEN.ImeMode")));
			this.txtCaptionEN.Location = ((System.Drawing.Point)(resources.GetObject("txtCaptionEN.Location")));
			this.txtCaptionEN.MaxLength = ((int)(resources.GetObject("txtCaptionEN.MaxLength")));
			this.txtCaptionEN.Multiline = ((bool)(resources.GetObject("txtCaptionEN.Multiline")));
			this.txtCaptionEN.Name = "txtCaptionEN";
			this.txtCaptionEN.PasswordChar = ((char)(resources.GetObject("txtCaptionEN.PasswordChar")));
			this.txtCaptionEN.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("txtCaptionEN.RightToLeft")));
			this.txtCaptionEN.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("txtCaptionEN.ScrollBars")));
			this.txtCaptionEN.Size = ((System.Drawing.Size)(resources.GetObject("txtCaptionEN.Size")));
			this.txtCaptionEN.TabIndex = ((int)(resources.GetObject("txtCaptionEN.TabIndex")));
			this.txtCaptionEN.Text = resources.GetString("txtCaptionEN.Text");
			this.txtCaptionEN.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("txtCaptionEN.TextAlign")));
			this.txtCaptionEN.Visible = ((bool)(resources.GetObject("txtCaptionEN.Visible")));
			this.txtCaptionEN.WordWrap = ((bool)(resources.GetObject("txtCaptionEN.WordWrap")));
			// 
			// lblCaptionEN
			// 
			this.lblCaptionEN.AccessibleDescription = resources.GetString("lblCaptionEN.AccessibleDescription");
			this.lblCaptionEN.AccessibleName = resources.GetString("lblCaptionEN.AccessibleName");
			this.lblCaptionEN.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblCaptionEN.Anchor")));
			this.lblCaptionEN.AutoSize = ((bool)(resources.GetObject("lblCaptionEN.AutoSize")));
			this.lblCaptionEN.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblCaptionEN.Dock")));
			this.lblCaptionEN.Enabled = ((bool)(resources.GetObject("lblCaptionEN.Enabled")));
			this.lblCaptionEN.Font = ((System.Drawing.Font)(resources.GetObject("lblCaptionEN.Font")));
			this.lblCaptionEN.ForeColor = System.Drawing.Color.Maroon;
			this.lblCaptionEN.Image = ((System.Drawing.Image)(resources.GetObject("lblCaptionEN.Image")));
			this.lblCaptionEN.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblCaptionEN.ImageAlign")));
			this.lblCaptionEN.ImageIndex = ((int)(resources.GetObject("lblCaptionEN.ImageIndex")));
			this.lblCaptionEN.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblCaptionEN.ImeMode")));
			this.lblCaptionEN.Location = ((System.Drawing.Point)(resources.GetObject("lblCaptionEN.Location")));
			this.lblCaptionEN.Name = "lblCaptionEN";
			this.lblCaptionEN.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblCaptionEN.RightToLeft")));
			this.lblCaptionEN.Size = ((System.Drawing.Size)(resources.GetObject("lblCaptionEN.Size")));
			this.lblCaptionEN.TabIndex = ((int)(resources.GetObject("lblCaptionEN.TabIndex")));
			this.lblCaptionEN.Text = resources.GetString("lblCaptionEN.Text");
			this.lblCaptionEN.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblCaptionEN.TextAlign")));
			this.lblCaptionEN.Visible = ((bool)(resources.GetObject("lblCaptionEN.Visible")));
			// 
			// txtCaptionJP
			// 
			this.txtCaptionJP.AccessibleDescription = resources.GetString("txtCaptionJP.AccessibleDescription");
			this.txtCaptionJP.AccessibleName = resources.GetString("txtCaptionJP.AccessibleName");
			this.txtCaptionJP.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("txtCaptionJP.Anchor")));
			this.txtCaptionJP.AutoSize = ((bool)(resources.GetObject("txtCaptionJP.AutoSize")));
			this.txtCaptionJP.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("txtCaptionJP.BackgroundImage")));
			this.txtCaptionJP.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("txtCaptionJP.Dock")));
			this.txtCaptionJP.Enabled = ((bool)(resources.GetObject("txtCaptionJP.Enabled")));
			this.txtCaptionJP.Font = ((System.Drawing.Font)(resources.GetObject("txtCaptionJP.Font")));
			this.txtCaptionJP.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("txtCaptionJP.ImeMode")));
			this.txtCaptionJP.Location = ((System.Drawing.Point)(resources.GetObject("txtCaptionJP.Location")));
			this.txtCaptionJP.MaxLength = ((int)(resources.GetObject("txtCaptionJP.MaxLength")));
			this.txtCaptionJP.Multiline = ((bool)(resources.GetObject("txtCaptionJP.Multiline")));
			this.txtCaptionJP.Name = "txtCaptionJP";
			this.txtCaptionJP.PasswordChar = ((char)(resources.GetObject("txtCaptionJP.PasswordChar")));
			this.txtCaptionJP.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("txtCaptionJP.RightToLeft")));
			this.txtCaptionJP.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("txtCaptionJP.ScrollBars")));
			this.txtCaptionJP.Size = ((System.Drawing.Size)(resources.GetObject("txtCaptionJP.Size")));
			this.txtCaptionJP.TabIndex = ((int)(resources.GetObject("txtCaptionJP.TabIndex")));
			this.txtCaptionJP.Text = resources.GetString("txtCaptionJP.Text");
			this.txtCaptionJP.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("txtCaptionJP.TextAlign")));
			this.txtCaptionJP.Visible = ((bool)(resources.GetObject("txtCaptionJP.Visible")));
			this.txtCaptionJP.WordWrap = ((bool)(resources.GetObject("txtCaptionJP.WordWrap")));
			// 
			// lblCaptionJP
			// 
			this.lblCaptionJP.AccessibleDescription = resources.GetString("lblCaptionJP.AccessibleDescription");
			this.lblCaptionJP.AccessibleName = resources.GetString("lblCaptionJP.AccessibleName");
			this.lblCaptionJP.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblCaptionJP.Anchor")));
			this.lblCaptionJP.AutoSize = ((bool)(resources.GetObject("lblCaptionJP.AutoSize")));
			this.lblCaptionJP.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblCaptionJP.Dock")));
			this.lblCaptionJP.Enabled = ((bool)(resources.GetObject("lblCaptionJP.Enabled")));
			this.lblCaptionJP.Font = ((System.Drawing.Font)(resources.GetObject("lblCaptionJP.Font")));
			this.lblCaptionJP.ForeColor = System.Drawing.Color.Maroon;
			this.lblCaptionJP.Image = ((System.Drawing.Image)(resources.GetObject("lblCaptionJP.Image")));
			this.lblCaptionJP.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblCaptionJP.ImageAlign")));
			this.lblCaptionJP.ImageIndex = ((int)(resources.GetObject("lblCaptionJP.ImageIndex")));
			this.lblCaptionJP.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblCaptionJP.ImeMode")));
			this.lblCaptionJP.Location = ((System.Drawing.Point)(resources.GetObject("lblCaptionJP.Location")));
			this.lblCaptionJP.Name = "lblCaptionJP";
			this.lblCaptionJP.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblCaptionJP.RightToLeft")));
			this.lblCaptionJP.Size = ((System.Drawing.Size)(resources.GetObject("lblCaptionJP.Size")));
			this.lblCaptionJP.TabIndex = ((int)(resources.GetObject("lblCaptionJP.TabIndex")));
			this.lblCaptionJP.Text = resources.GetString("lblCaptionJP.Text");
			this.lblCaptionJP.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblCaptionJP.TextAlign")));
			this.lblCaptionJP.Visible = ((bool)(resources.GetObject("lblCaptionJP.Visible")));
			// 
			// chkPrint
			// 
			this.chkPrint.AccessibleDescription = resources.GetString("chkPrint.AccessibleDescription");
			this.chkPrint.AccessibleName = resources.GetString("chkPrint.AccessibleName");
			this.chkPrint.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("chkPrint.Anchor")));
			this.chkPrint.Appearance = ((System.Windows.Forms.Appearance)(resources.GetObject("chkPrint.Appearance")));
			this.chkPrint.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("chkPrint.BackgroundImage")));
			this.chkPrint.CheckAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("chkPrint.CheckAlign")));
			this.chkPrint.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("chkPrint.Dock")));
			this.chkPrint.Enabled = ((bool)(resources.GetObject("chkPrint.Enabled")));
			this.chkPrint.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("chkPrint.FlatStyle")));
			this.chkPrint.Font = ((System.Drawing.Font)(resources.GetObject("chkPrint.Font")));
			this.chkPrint.Image = ((System.Drawing.Image)(resources.GetObject("chkPrint.Image")));
			this.chkPrint.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("chkPrint.ImageAlign")));
			this.chkPrint.ImageIndex = ((int)(resources.GetObject("chkPrint.ImageIndex")));
			this.chkPrint.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("chkPrint.ImeMode")));
			this.chkPrint.Location = ((System.Drawing.Point)(resources.GetObject("chkPrint.Location")));
			this.chkPrint.Name = "chkPrint";
			this.chkPrint.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("chkPrint.RightToLeft")));
			this.chkPrint.Size = ((System.Drawing.Size)(resources.GetObject("chkPrint.Size")));
			this.chkPrint.TabIndex = ((int)(resources.GetObject("chkPrint.TabIndex")));
			this.chkPrint.Text = resources.GetString("chkPrint.Text");
			this.chkPrint.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("chkPrint.TextAlign")));
			this.chkPrint.Visible = ((bool)(resources.GetObject("chkPrint.Visible")));
			// 
			// cboSort
			// 
			this.cboSort.AccessibleDescription = resources.GetString("cboSort.AccessibleDescription");
			this.cboSort.AccessibleName = resources.GetString("cboSort.AccessibleName");
			this.cboSort.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("cboSort.Anchor")));
			this.cboSort.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cboSort.BackgroundImage")));
			this.cboSort.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("cboSort.Dock")));
			this.cboSort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboSort.Enabled = ((bool)(resources.GetObject("cboSort.Enabled")));
			this.cboSort.Font = ((System.Drawing.Font)(resources.GetObject("cboSort.Font")));
			this.cboSort.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("cboSort.ImeMode")));
			this.cboSort.IntegralHeight = ((bool)(resources.GetObject("cboSort.IntegralHeight")));
			this.cboSort.ItemHeight = ((int)(resources.GetObject("cboSort.ItemHeight")));
			this.cboSort.Items.AddRange(new object[] {
														 resources.GetString("cboSort.Items"),
														 resources.GetString("cboSort.Items1"),
														 resources.GetString("cboSort.Items2")});
			this.cboSort.Location = ((System.Drawing.Point)(resources.GetObject("cboSort.Location")));
			this.cboSort.MaxDropDownItems = ((int)(resources.GetObject("cboSort.MaxDropDownItems")));
			this.cboSort.MaxLength = ((int)(resources.GetObject("cboSort.MaxLength")));
			this.cboSort.Name = "cboSort";
			this.cboSort.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("cboSort.RightToLeft")));
			this.cboSort.Size = ((System.Drawing.Size)(resources.GetObject("cboSort.Size")));
			this.cboSort.TabIndex = ((int)(resources.GetObject("cboSort.TabIndex")));
			this.cboSort.Text = resources.GetString("cboSort.Text");
			this.cboSort.Visible = ((bool)(resources.GetObject("cboSort.Visible")));
			// 
			// lblSort
			// 
			this.lblSort.AccessibleDescription = resources.GetString("lblSort.AccessibleDescription");
			this.lblSort.AccessibleName = resources.GetString("lblSort.AccessibleName");
			this.lblSort.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblSort.Anchor")));
			this.lblSort.AutoSize = ((bool)(resources.GetObject("lblSort.AutoSize")));
			this.lblSort.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblSort.Dock")));
			this.lblSort.Enabled = ((bool)(resources.GetObject("lblSort.Enabled")));
			this.lblSort.Font = ((System.Drawing.Font)(resources.GetObject("lblSort.Font")));
			this.lblSort.Image = ((System.Drawing.Image)(resources.GetObject("lblSort.Image")));
			this.lblSort.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblSort.ImageAlign")));
			this.lblSort.ImageIndex = ((int)(resources.GetObject("lblSort.ImageIndex")));
			this.lblSort.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblSort.ImeMode")));
			this.lblSort.Location = ((System.Drawing.Point)(resources.GetObject("lblSort.Location")));
			this.lblSort.Name = "lblSort";
			this.lblSort.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblSort.RightToLeft")));
			this.lblSort.Size = ((System.Drawing.Size)(resources.GetObject("lblSort.Size")));
			this.lblSort.TabIndex = ((int)(resources.GetObject("lblSort.TabIndex")));
			this.lblSort.Text = resources.GetString("lblSort.Text");
			this.lblSort.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblSort.TextAlign")));
			this.lblSort.Visible = ((bool)(resources.GetObject("lblSort.Visible")));
			// 
			// lblColumnSummary
			// 
			this.lblColumnSummary.AccessibleDescription = resources.GetString("lblColumnSummary.AccessibleDescription");
			this.lblColumnSummary.AccessibleName = resources.GetString("lblColumnSummary.AccessibleName");
			this.lblColumnSummary.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblColumnSummary.Anchor")));
			this.lblColumnSummary.AutoSize = ((bool)(resources.GetObject("lblColumnSummary.AutoSize")));
			this.lblColumnSummary.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblColumnSummary.Dock")));
			this.lblColumnSummary.Enabled = ((bool)(resources.GetObject("lblColumnSummary.Enabled")));
			this.lblColumnSummary.Font = ((System.Drawing.Font)(resources.GetObject("lblColumnSummary.Font")));
			this.lblColumnSummary.Image = ((System.Drawing.Image)(resources.GetObject("lblColumnSummary.Image")));
			this.lblColumnSummary.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblColumnSummary.ImageAlign")));
			this.lblColumnSummary.ImageIndex = ((int)(resources.GetObject("lblColumnSummary.ImageIndex")));
			this.lblColumnSummary.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblColumnSummary.ImeMode")));
			this.lblColumnSummary.Location = ((System.Drawing.Point)(resources.GetObject("lblColumnSummary.Location")));
			this.lblColumnSummary.Name = "lblColumnSummary";
			this.lblColumnSummary.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblColumnSummary.RightToLeft")));
			this.lblColumnSummary.Size = ((System.Drawing.Size)(resources.GetObject("lblColumnSummary.Size")));
			this.lblColumnSummary.TabIndex = ((int)(resources.GetObject("lblColumnSummary.TabIndex")));
			this.lblColumnSummary.Text = resources.GetString("lblColumnSummary.Text");
			this.lblColumnSummary.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblColumnSummary.TextAlign")));
			this.lblColumnSummary.Visible = ((bool)(resources.GetObject("lblColumnSummary.Visible")));
			// 
			// grpSummary
			// 
			this.grpSummary.AccessibleDescription = resources.GetString("grpSummary.AccessibleDescription");
			this.grpSummary.AccessibleName = resources.GetString("grpSummary.AccessibleName");
			this.grpSummary.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("grpSummary.Anchor")));
			this.grpSummary.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("grpSummary.BackgroundImage")));
			this.grpSummary.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("grpSummary.Dock")));
			this.grpSummary.Enabled = ((bool)(resources.GetObject("grpSummary.Enabled")));
			this.grpSummary.Font = ((System.Drawing.Font)(resources.GetObject("grpSummary.Font")));
			this.grpSummary.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("grpSummary.ImeMode")));
			this.grpSummary.Location = ((System.Drawing.Point)(resources.GetObject("grpSummary.Location")));
			this.grpSummary.Name = "grpSummary";
			this.grpSummary.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("grpSummary.RightToLeft")));
			this.grpSummary.Size = ((System.Drawing.Size)(resources.GetObject("grpSummary.Size")));
			this.grpSummary.TabIndex = ((int)(resources.GetObject("grpSummary.TabIndex")));
			this.grpSummary.TabStop = false;
			this.grpSummary.Text = resources.GetString("grpSummary.Text");
			this.grpSummary.Visible = ((bool)(resources.GetObject("grpSummary.Visible")));
			// 
			// chkDisplayBottom
			// 
			this.chkDisplayBottom.AccessibleDescription = resources.GetString("chkDisplayBottom.AccessibleDescription");
			this.chkDisplayBottom.AccessibleName = resources.GetString("chkDisplayBottom.AccessibleName");
			this.chkDisplayBottom.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("chkDisplayBottom.Anchor")));
			this.chkDisplayBottom.Appearance = ((System.Windows.Forms.Appearance)(resources.GetObject("chkDisplayBottom.Appearance")));
			this.chkDisplayBottom.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("chkDisplayBottom.BackgroundImage")));
			this.chkDisplayBottom.CheckAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("chkDisplayBottom.CheckAlign")));
			this.chkDisplayBottom.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("chkDisplayBottom.Dock")));
			this.chkDisplayBottom.Enabled = ((bool)(resources.GetObject("chkDisplayBottom.Enabled")));
			this.chkDisplayBottom.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("chkDisplayBottom.FlatStyle")));
			this.chkDisplayBottom.Font = ((System.Drawing.Font)(resources.GetObject("chkDisplayBottom.Font")));
			this.chkDisplayBottom.Image = ((System.Drawing.Image)(resources.GetObject("chkDisplayBottom.Image")));
			this.chkDisplayBottom.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("chkDisplayBottom.ImageAlign")));
			this.chkDisplayBottom.ImageIndex = ((int)(resources.GetObject("chkDisplayBottom.ImageIndex")));
			this.chkDisplayBottom.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("chkDisplayBottom.ImeMode")));
			this.chkDisplayBottom.Location = ((System.Drawing.Point)(resources.GetObject("chkDisplayBottom.Location")));
			this.chkDisplayBottom.Name = "chkDisplayBottom";
			this.chkDisplayBottom.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("chkDisplayBottom.RightToLeft")));
			this.chkDisplayBottom.Size = ((System.Drawing.Size)(resources.GetObject("chkDisplayBottom.Size")));
			this.chkDisplayBottom.TabIndex = ((int)(resources.GetObject("chkDisplayBottom.TabIndex")));
			this.chkDisplayBottom.Text = resources.GetString("chkDisplayBottom.Text");
			this.chkDisplayBottom.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("chkDisplayBottom.TextAlign")));
			this.chkDisplayBottom.Visible = ((bool)(resources.GetObject("chkDisplayBottom.Visible")));
			// 
			// lblColumnGrouping
			// 
			this.lblColumnGrouping.AccessibleDescription = resources.GetString("lblColumnGrouping.AccessibleDescription");
			this.lblColumnGrouping.AccessibleName = resources.GetString("lblColumnGrouping.AccessibleName");
			this.lblColumnGrouping.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblColumnGrouping.Anchor")));
			this.lblColumnGrouping.AutoSize = ((bool)(resources.GetObject("lblColumnGrouping.AutoSize")));
			this.lblColumnGrouping.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblColumnGrouping.Dock")));
			this.lblColumnGrouping.Enabled = ((bool)(resources.GetObject("lblColumnGrouping.Enabled")));
			this.lblColumnGrouping.Font = ((System.Drawing.Font)(resources.GetObject("lblColumnGrouping.Font")));
			this.lblColumnGrouping.Image = ((System.Drawing.Image)(resources.GetObject("lblColumnGrouping.Image")));
			this.lblColumnGrouping.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblColumnGrouping.ImageAlign")));
			this.lblColumnGrouping.ImageIndex = ((int)(resources.GetObject("lblColumnGrouping.ImageIndex")));
			this.lblColumnGrouping.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblColumnGrouping.ImeMode")));
			this.lblColumnGrouping.Location = ((System.Drawing.Point)(resources.GetObject("lblColumnGrouping.Location")));
			this.lblColumnGrouping.Name = "lblColumnGrouping";
			this.lblColumnGrouping.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblColumnGrouping.RightToLeft")));
			this.lblColumnGrouping.Size = ((System.Drawing.Size)(resources.GetObject("lblColumnGrouping.Size")));
			this.lblColumnGrouping.TabIndex = ((int)(resources.GetObject("lblColumnGrouping.TabIndex")));
			this.lblColumnGrouping.Text = resources.GetString("lblColumnGrouping.Text");
			this.lblColumnGrouping.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblColumnGrouping.TextAlign")));
			this.lblColumnGrouping.Visible = ((bool)(resources.GetObject("lblColumnGrouping.Visible")));
			// 
			// txtFormat
			// 
			this.txtFormat.AccessibleDescription = resources.GetString("txtFormat.AccessibleDescription");
			this.txtFormat.AccessibleName = resources.GetString("txtFormat.AccessibleName");
			this.txtFormat.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("txtFormat.Anchor")));
			this.txtFormat.AutoSize = ((bool)(resources.GetObject("txtFormat.AutoSize")));
			this.txtFormat.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("txtFormat.BackgroundImage")));
			this.txtFormat.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("txtFormat.Dock")));
			this.txtFormat.Enabled = ((bool)(resources.GetObject("txtFormat.Enabled")));
			this.txtFormat.Font = ((System.Drawing.Font)(resources.GetObject("txtFormat.Font")));
			this.txtFormat.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("txtFormat.ImeMode")));
			this.txtFormat.Location = ((System.Drawing.Point)(resources.GetObject("txtFormat.Location")));
			this.txtFormat.MaxLength = ((int)(resources.GetObject("txtFormat.MaxLength")));
			this.txtFormat.Multiline = ((bool)(resources.GetObject("txtFormat.Multiline")));
			this.txtFormat.Name = "txtFormat";
			this.txtFormat.PasswordChar = ((char)(resources.GetObject("txtFormat.PasswordChar")));
			this.txtFormat.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("txtFormat.RightToLeft")));
			this.txtFormat.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("txtFormat.ScrollBars")));
			this.txtFormat.Size = ((System.Drawing.Size)(resources.GetObject("txtFormat.Size")));
			this.txtFormat.TabIndex = ((int)(resources.GetObject("txtFormat.TabIndex")));
			this.txtFormat.Text = resources.GetString("txtFormat.Text");
			this.txtFormat.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("txtFormat.TextAlign")));
			this.txtFormat.Visible = ((bool)(resources.GetObject("txtFormat.Visible")));
			this.txtFormat.WordWrap = ((bool)(resources.GetObject("txtFormat.WordWrap")));
			// 
			// lblFormat
			// 
			this.lblFormat.AccessibleDescription = resources.GetString("lblFormat.AccessibleDescription");
			this.lblFormat.AccessibleName = resources.GetString("lblFormat.AccessibleName");
			this.lblFormat.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblFormat.Anchor")));
			this.lblFormat.AutoSize = ((bool)(resources.GetObject("lblFormat.AutoSize")));
			this.lblFormat.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblFormat.Dock")));
			this.lblFormat.Enabled = ((bool)(resources.GetObject("lblFormat.Enabled")));
			this.lblFormat.Font = ((System.Drawing.Font)(resources.GetObject("lblFormat.Font")));
			this.lblFormat.Image = ((System.Drawing.Image)(resources.GetObject("lblFormat.Image")));
			this.lblFormat.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblFormat.ImageAlign")));
			this.lblFormat.ImageIndex = ((int)(resources.GetObject("lblFormat.ImageIndex")));
			this.lblFormat.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblFormat.ImeMode")));
			this.lblFormat.Location = ((System.Drawing.Point)(resources.GetObject("lblFormat.Location")));
			this.lblFormat.Name = "lblFormat";
			this.lblFormat.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblFormat.RightToLeft")));
			this.lblFormat.Size = ((System.Drawing.Size)(resources.GetObject("lblFormat.Size")));
			this.lblFormat.TabIndex = ((int)(resources.GetObject("lblFormat.TabIndex")));
			this.lblFormat.Text = resources.GetString("lblFormat.Text");
			this.lblFormat.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblFormat.TextAlign")));
			this.lblFormat.Visible = ((bool)(resources.GetObject("lblFormat.Visible")));
			// 
			// cboType
			// 
			this.cboType.AccessibleDescription = resources.GetString("cboType.AccessibleDescription");
			this.cboType.AccessibleName = resources.GetString("cboType.AccessibleName");
			this.cboType.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("cboType.Anchor")));
			this.cboType.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cboType.BackgroundImage")));
			this.cboType.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("cboType.Dock")));
			this.cboType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboType.Enabled = ((bool)(resources.GetObject("cboType.Enabled")));
			this.cboType.Font = ((System.Drawing.Font)(resources.GetObject("cboType.Font")));
			this.cboType.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("cboType.ImeMode")));
			this.cboType.IntegralHeight = ((bool)(resources.GetObject("cboType.IntegralHeight")));
			this.cboType.ItemHeight = ((int)(resources.GetObject("cboType.ItemHeight")));
			this.cboType.Items.AddRange(new object[] {
														 resources.GetString("cboType.Items"),
														 resources.GetString("cboType.Items1"),
														 resources.GetString("cboType.Items2")});
			this.cboType.Location = ((System.Drawing.Point)(resources.GetObject("cboType.Location")));
			this.cboType.MaxDropDownItems = ((int)(resources.GetObject("cboType.MaxDropDownItems")));
			this.cboType.MaxLength = ((int)(resources.GetObject("cboType.MaxLength")));
			this.cboType.Name = "cboType";
			this.cboType.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("cboType.RightToLeft")));
			this.cboType.Size = ((System.Drawing.Size)(resources.GetObject("cboType.Size")));
			this.cboType.TabIndex = ((int)(resources.GetObject("cboType.TabIndex")));
			this.cboType.Text = resources.GetString("cboType.Text");
			this.cboType.Visible = ((bool)(resources.GetObject("cboType.Visible")));
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
			// chkSumBottomPage
			// 
			this.chkSumBottomPage.AccessibleDescription = resources.GetString("chkSumBottomPage.AccessibleDescription");
			this.chkSumBottomPage.AccessibleName = resources.GetString("chkSumBottomPage.AccessibleName");
			this.chkSumBottomPage.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("chkSumBottomPage.Anchor")));
			this.chkSumBottomPage.Appearance = ((System.Windows.Forms.Appearance)(resources.GetObject("chkSumBottomPage.Appearance")));
			this.chkSumBottomPage.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("chkSumBottomPage.BackgroundImage")));
			this.chkSumBottomPage.CheckAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("chkSumBottomPage.CheckAlign")));
			this.chkSumBottomPage.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("chkSumBottomPage.Dock")));
			this.chkSumBottomPage.Enabled = ((bool)(resources.GetObject("chkSumBottomPage.Enabled")));
			this.chkSumBottomPage.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("chkSumBottomPage.FlatStyle")));
			this.chkSumBottomPage.Font = ((System.Drawing.Font)(resources.GetObject("chkSumBottomPage.Font")));
			this.chkSumBottomPage.Image = ((System.Drawing.Image)(resources.GetObject("chkSumBottomPage.Image")));
			this.chkSumBottomPage.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("chkSumBottomPage.ImageAlign")));
			this.chkSumBottomPage.ImageIndex = ((int)(resources.GetObject("chkSumBottomPage.ImageIndex")));
			this.chkSumBottomPage.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("chkSumBottomPage.ImeMode")));
			this.chkSumBottomPage.Location = ((System.Drawing.Point)(resources.GetObject("chkSumBottomPage.Location")));
			this.chkSumBottomPage.Name = "chkSumBottomPage";
			this.chkSumBottomPage.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("chkSumBottomPage.RightToLeft")));
			this.chkSumBottomPage.Size = ((System.Drawing.Size)(resources.GetObject("chkSumBottomPage.Size")));
			this.chkSumBottomPage.TabIndex = ((int)(resources.GetObject("chkSumBottomPage.TabIndex")));
			this.chkSumBottomPage.Text = resources.GetString("chkSumBottomPage.Text");
			this.chkSumBottomPage.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("chkSumBottomPage.TextAlign")));
			this.chkSumBottomPage.Visible = ((bool)(resources.GetObject("chkSumBottomPage.Visible")));
			// 
			// chkSumTopPage
			// 
			this.chkSumTopPage.AccessibleDescription = resources.GetString("chkSumTopPage.AccessibleDescription");
			this.chkSumTopPage.AccessibleName = resources.GetString("chkSumTopPage.AccessibleName");
			this.chkSumTopPage.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("chkSumTopPage.Anchor")));
			this.chkSumTopPage.Appearance = ((System.Windows.Forms.Appearance)(resources.GetObject("chkSumTopPage.Appearance")));
			this.chkSumTopPage.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("chkSumTopPage.BackgroundImage")));
			this.chkSumTopPage.CheckAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("chkSumTopPage.CheckAlign")));
			this.chkSumTopPage.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("chkSumTopPage.Dock")));
			this.chkSumTopPage.Enabled = ((bool)(resources.GetObject("chkSumTopPage.Enabled")));
			this.chkSumTopPage.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("chkSumTopPage.FlatStyle")));
			this.chkSumTopPage.Font = ((System.Drawing.Font)(resources.GetObject("chkSumTopPage.Font")));
			this.chkSumTopPage.Image = ((System.Drawing.Image)(resources.GetObject("chkSumTopPage.Image")));
			this.chkSumTopPage.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("chkSumTopPage.ImageAlign")));
			this.chkSumTopPage.ImageIndex = ((int)(resources.GetObject("chkSumTopPage.ImageIndex")));
			this.chkSumTopPage.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("chkSumTopPage.ImeMode")));
			this.chkSumTopPage.Location = ((System.Drawing.Point)(resources.GetObject("chkSumTopPage.Location")));
			this.chkSumTopPage.Name = "chkSumTopPage";
			this.chkSumTopPage.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("chkSumTopPage.RightToLeft")));
			this.chkSumTopPage.Size = ((System.Drawing.Size)(resources.GetObject("chkSumTopPage.Size")));
			this.chkSumTopPage.TabIndex = ((int)(resources.GetObject("chkSumTopPage.TabIndex")));
			this.chkSumTopPage.Text = resources.GetString("chkSumTopPage.Text");
			this.chkSumTopPage.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("chkSumTopPage.TextAlign")));
			this.chkSumTopPage.Visible = ((bool)(resources.GetObject("chkSumTopPage.Visible")));
			// 
			// chkSumTopReport
			// 
			this.chkSumTopReport.AccessibleDescription = resources.GetString("chkSumTopReport.AccessibleDescription");
			this.chkSumTopReport.AccessibleName = resources.GetString("chkSumTopReport.AccessibleName");
			this.chkSumTopReport.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("chkSumTopReport.Anchor")));
			this.chkSumTopReport.Appearance = ((System.Windows.Forms.Appearance)(resources.GetObject("chkSumTopReport.Appearance")));
			this.chkSumTopReport.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("chkSumTopReport.BackgroundImage")));
			this.chkSumTopReport.CheckAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("chkSumTopReport.CheckAlign")));
			this.chkSumTopReport.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("chkSumTopReport.Dock")));
			this.chkSumTopReport.Enabled = ((bool)(resources.GetObject("chkSumTopReport.Enabled")));
			this.chkSumTopReport.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("chkSumTopReport.FlatStyle")));
			this.chkSumTopReport.Font = ((System.Drawing.Font)(resources.GetObject("chkSumTopReport.Font")));
			this.chkSumTopReport.Image = ((System.Drawing.Image)(resources.GetObject("chkSumTopReport.Image")));
			this.chkSumTopReport.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("chkSumTopReport.ImageAlign")));
			this.chkSumTopReport.ImageIndex = ((int)(resources.GetObject("chkSumTopReport.ImageIndex")));
			this.chkSumTopReport.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("chkSumTopReport.ImeMode")));
			this.chkSumTopReport.Location = ((System.Drawing.Point)(resources.GetObject("chkSumTopReport.Location")));
			this.chkSumTopReport.Name = "chkSumTopReport";
			this.chkSumTopReport.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("chkSumTopReport.RightToLeft")));
			this.chkSumTopReport.Size = ((System.Drawing.Size)(resources.GetObject("chkSumTopReport.Size")));
			this.chkSumTopReport.TabIndex = ((int)(resources.GetObject("chkSumTopReport.TabIndex")));
			this.chkSumTopReport.Text = resources.GetString("chkSumTopReport.Text");
			this.chkSumTopReport.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("chkSumTopReport.TextAlign")));
			this.chkSumTopReport.Visible = ((bool)(resources.GetObject("chkSumTopReport.Visible")));
			// 
			// chkSumBottomReport
			// 
			this.chkSumBottomReport.AccessibleDescription = resources.GetString("chkSumBottomReport.AccessibleDescription");
			this.chkSumBottomReport.AccessibleName = resources.GetString("chkSumBottomReport.AccessibleName");
			this.chkSumBottomReport.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("chkSumBottomReport.Anchor")));
			this.chkSumBottomReport.Appearance = ((System.Windows.Forms.Appearance)(resources.GetObject("chkSumBottomReport.Appearance")));
			this.chkSumBottomReport.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("chkSumBottomReport.BackgroundImage")));
			this.chkSumBottomReport.CheckAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("chkSumBottomReport.CheckAlign")));
			this.chkSumBottomReport.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("chkSumBottomReport.Dock")));
			this.chkSumBottomReport.Enabled = ((bool)(resources.GetObject("chkSumBottomReport.Enabled")));
			this.chkSumBottomReport.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("chkSumBottomReport.FlatStyle")));
			this.chkSumBottomReport.Font = ((System.Drawing.Font)(resources.GetObject("chkSumBottomReport.Font")));
			this.chkSumBottomReport.Image = ((System.Drawing.Image)(resources.GetObject("chkSumBottomReport.Image")));
			this.chkSumBottomReport.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("chkSumBottomReport.ImageAlign")));
			this.chkSumBottomReport.ImageIndex = ((int)(resources.GetObject("chkSumBottomReport.ImageIndex")));
			this.chkSumBottomReport.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("chkSumBottomReport.ImeMode")));
			this.chkSumBottomReport.Location = ((System.Drawing.Point)(resources.GetObject("chkSumBottomReport.Location")));
			this.chkSumBottomReport.Name = "chkSumBottomReport";
			this.chkSumBottomReport.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("chkSumBottomReport.RightToLeft")));
			this.chkSumBottomReport.Size = ((System.Drawing.Size)(resources.GetObject("chkSumBottomReport.Size")));
			this.chkSumBottomReport.TabIndex = ((int)(resources.GetObject("chkSumBottomReport.TabIndex")));
			this.chkSumBottomReport.Text = resources.GetString("chkSumBottomReport.Text");
			this.chkSumBottomReport.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("chkSumBottomReport.TextAlign")));
			this.chkSumBottomReport.Visible = ((bool)(resources.GetObject("chkSumBottomReport.Visible")));
			// 
			// chkGroupBy
			// 
			this.chkGroupBy.AccessibleDescription = resources.GetString("chkGroupBy.AccessibleDescription");
			this.chkGroupBy.AccessibleName = resources.GetString("chkGroupBy.AccessibleName");
			this.chkGroupBy.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("chkGroupBy.Anchor")));
			this.chkGroupBy.Appearance = ((System.Windows.Forms.Appearance)(resources.GetObject("chkGroupBy.Appearance")));
			this.chkGroupBy.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("chkGroupBy.BackgroundImage")));
			this.chkGroupBy.CheckAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("chkGroupBy.CheckAlign")));
			this.chkGroupBy.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("chkGroupBy.Dock")));
			this.chkGroupBy.Enabled = ((bool)(resources.GetObject("chkGroupBy.Enabled")));
			this.chkGroupBy.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("chkGroupBy.FlatStyle")));
			this.chkGroupBy.Font = ((System.Drawing.Font)(resources.GetObject("chkGroupBy.Font")));
			this.chkGroupBy.Image = ((System.Drawing.Image)(resources.GetObject("chkGroupBy.Image")));
			this.chkGroupBy.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("chkGroupBy.ImageAlign")));
			this.chkGroupBy.ImageIndex = ((int)(resources.GetObject("chkGroupBy.ImageIndex")));
			this.chkGroupBy.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("chkGroupBy.ImeMode")));
			this.chkGroupBy.Location = ((System.Drawing.Point)(resources.GetObject("chkGroupBy.Location")));
			this.chkGroupBy.Name = "chkGroupBy";
			this.chkGroupBy.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("chkGroupBy.RightToLeft")));
			this.chkGroupBy.Size = ((System.Drawing.Size)(resources.GetObject("chkGroupBy.Size")));
			this.chkGroupBy.TabIndex = ((int)(resources.GetObject("chkGroupBy.TabIndex")));
			this.chkGroupBy.Text = resources.GetString("chkGroupBy.Text");
			this.chkGroupBy.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("chkGroupBy.TextAlign")));
			this.chkGroupBy.Visible = ((bool)(resources.GetObject("chkGroupBy.Visible")));
			this.chkGroupBy.CheckedChanged += new System.EventHandler(this.chkGroupBy_CheckedChanged);
			// 
			// lblWidth
			// 
			this.lblWidth.AccessibleDescription = resources.GetString("lblWidth.AccessibleDescription");
			this.lblWidth.AccessibleName = resources.GetString("lblWidth.AccessibleName");
			this.lblWidth.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblWidth.Anchor")));
			this.lblWidth.AutoSize = ((bool)(resources.GetObject("lblWidth.AutoSize")));
			this.lblWidth.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblWidth.Dock")));
			this.lblWidth.Enabled = ((bool)(resources.GetObject("lblWidth.Enabled")));
			this.lblWidth.Font = ((System.Drawing.Font)(resources.GetObject("lblWidth.Font")));
			this.lblWidth.ForeColor = System.Drawing.Color.Maroon;
			this.lblWidth.Image = ((System.Drawing.Image)(resources.GetObject("lblWidth.Image")));
			this.lblWidth.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblWidth.ImageAlign")));
			this.lblWidth.ImageIndex = ((int)(resources.GetObject("lblWidth.ImageIndex")));
			this.lblWidth.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblWidth.ImeMode")));
			this.lblWidth.Location = ((System.Drawing.Point)(resources.GetObject("lblWidth.Location")));
			this.lblWidth.Name = "lblWidth";
			this.lblWidth.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblWidth.RightToLeft")));
			this.lblWidth.Size = ((System.Drawing.Size)(resources.GetObject("lblWidth.Size")));
			this.lblWidth.TabIndex = ((int)(resources.GetObject("lblWidth.TabIndex")));
			this.lblWidth.Text = resources.GetString("lblWidth.Text");
			this.lblWidth.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblWidth.TextAlign")));
			this.lblWidth.Visible = ((bool)(resources.GetObject("lblWidth.Visible")));
			// 
			// btnFont
			// 
			this.btnFont.AccessibleDescription = resources.GetString("btnFont.AccessibleDescription");
			this.btnFont.AccessibleName = resources.GetString("btnFont.AccessibleName");
			this.btnFont.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnFont.Anchor")));
			this.btnFont.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnFont.BackgroundImage")));
			this.btnFont.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnFont.Dock")));
			this.btnFont.Enabled = ((bool)(resources.GetObject("btnFont.Enabled")));
			this.btnFont.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnFont.FlatStyle")));
			this.btnFont.Font = ((System.Drawing.Font)(resources.GetObject("btnFont.Font")));
			this.btnFont.Image = ((System.Drawing.Image)(resources.GetObject("btnFont.Image")));
			this.btnFont.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnFont.ImageAlign")));
			this.btnFont.ImageIndex = ((int)(resources.GetObject("btnFont.ImageIndex")));
			this.btnFont.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnFont.ImeMode")));
			this.btnFont.Location = ((System.Drawing.Point)(resources.GetObject("btnFont.Location")));
			this.btnFont.Name = "btnFont";
			this.btnFont.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnFont.RightToLeft")));
			this.btnFont.Size = ((System.Drawing.Size)(resources.GetObject("btnFont.Size")));
			this.btnFont.TabIndex = ((int)(resources.GetObject("btnFont.TabIndex")));
			this.btnFont.Text = resources.GetString("btnFont.Text");
			this.btnFont.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnFont.TextAlign")));
			this.btnFont.Visible = ((bool)(resources.GetObject("btnFont.Visible")));
			this.btnFont.Click += new System.EventHandler(this.btnFont_Click);
			// 
			// txtCaption
			// 
			this.txtCaption.AccessibleDescription = resources.GetString("txtCaption.AccessibleDescription");
			this.txtCaption.AccessibleName = resources.GetString("txtCaption.AccessibleName");
			this.txtCaption.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("txtCaption.Anchor")));
			this.txtCaption.AutoSize = ((bool)(resources.GetObject("txtCaption.AutoSize")));
			this.txtCaption.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("txtCaption.BackgroundImage")));
			this.txtCaption.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("txtCaption.Dock")));
			this.txtCaption.Enabled = ((bool)(resources.GetObject("txtCaption.Enabled")));
			this.txtCaption.Font = ((System.Drawing.Font)(resources.GetObject("txtCaption.Font")));
			this.txtCaption.ForeColor = System.Drawing.SystemColors.WindowText;
			this.txtCaption.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("txtCaption.ImeMode")));
			this.txtCaption.Location = ((System.Drawing.Point)(resources.GetObject("txtCaption.Location")));
			this.txtCaption.MaxLength = ((int)(resources.GetObject("txtCaption.MaxLength")));
			this.txtCaption.Multiline = ((bool)(resources.GetObject("txtCaption.Multiline")));
			this.txtCaption.Name = "txtCaption";
			this.txtCaption.PasswordChar = ((char)(resources.GetObject("txtCaption.PasswordChar")));
			this.txtCaption.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("txtCaption.RightToLeft")));
			this.txtCaption.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("txtCaption.ScrollBars")));
			this.txtCaption.Size = ((System.Drawing.Size)(resources.GetObject("txtCaption.Size")));
			this.txtCaption.TabIndex = ((int)(resources.GetObject("txtCaption.TabIndex")));
			this.txtCaption.Text = resources.GetString("txtCaption.Text");
			this.txtCaption.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("txtCaption.TextAlign")));
			this.txtCaption.Visible = ((bool)(resources.GetObject("txtCaption.Visible")));
			this.txtCaption.WordWrap = ((bool)(resources.GetObject("txtCaption.WordWrap")));
			// 
			// lblCaption
			// 
			this.lblCaption.AccessibleDescription = resources.GetString("lblCaption.AccessibleDescription");
			this.lblCaption.AccessibleName = resources.GetString("lblCaption.AccessibleName");
			this.lblCaption.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblCaption.Anchor")));
			this.lblCaption.AutoSize = ((bool)(resources.GetObject("lblCaption.AutoSize")));
			this.lblCaption.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblCaption.Dock")));
			this.lblCaption.Enabled = ((bool)(resources.GetObject("lblCaption.Enabled")));
			this.lblCaption.Font = ((System.Drawing.Font)(resources.GetObject("lblCaption.Font")));
			this.lblCaption.ForeColor = System.Drawing.Color.Maroon;
			this.lblCaption.Image = ((System.Drawing.Image)(resources.GetObject("lblCaption.Image")));
			this.lblCaption.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblCaption.ImageAlign")));
			this.lblCaption.ImageIndex = ((int)(resources.GetObject("lblCaption.ImageIndex")));
			this.lblCaption.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblCaption.ImeMode")));
			this.lblCaption.Location = ((System.Drawing.Point)(resources.GetObject("lblCaption.Location")));
			this.lblCaption.Name = "lblCaption";
			this.lblCaption.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblCaption.RightToLeft")));
			this.lblCaption.Size = ((System.Drawing.Size)(resources.GetObject("lblCaption.Size")));
			this.lblCaption.TabIndex = ((int)(resources.GetObject("lblCaption.TabIndex")));
			this.lblCaption.Text = resources.GetString("lblCaption.Text");
			this.lblCaption.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblCaption.TextAlign")));
			this.lblCaption.Visible = ((bool)(resources.GetObject("lblCaption.Visible")));
			// 
			// txtFieldName
			// 
			this.txtFieldName.AccessibleDescription = resources.GetString("txtFieldName.AccessibleDescription");
			this.txtFieldName.AccessibleName = resources.GetString("txtFieldName.AccessibleName");
			this.txtFieldName.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("txtFieldName.Anchor")));
			this.txtFieldName.AutoSize = ((bool)(resources.GetObject("txtFieldName.AutoSize")));
			this.txtFieldName.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("txtFieldName.BackgroundImage")));
			this.txtFieldName.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("txtFieldName.Dock")));
			this.txtFieldName.Enabled = ((bool)(resources.GetObject("txtFieldName.Enabled")));
			this.txtFieldName.Font = ((System.Drawing.Font)(resources.GetObject("txtFieldName.Font")));
			this.txtFieldName.ForeColor = System.Drawing.SystemColors.WindowText;
			this.txtFieldName.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("txtFieldName.ImeMode")));
			this.txtFieldName.Location = ((System.Drawing.Point)(resources.GetObject("txtFieldName.Location")));
			this.txtFieldName.MaxLength = ((int)(resources.GetObject("txtFieldName.MaxLength")));
			this.txtFieldName.Multiline = ((bool)(resources.GetObject("txtFieldName.Multiline")));
			this.txtFieldName.Name = "txtFieldName";
			this.txtFieldName.PasswordChar = ((char)(resources.GetObject("txtFieldName.PasswordChar")));
			this.txtFieldName.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("txtFieldName.RightToLeft")));
			this.txtFieldName.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("txtFieldName.ScrollBars")));
			this.txtFieldName.Size = ((System.Drawing.Size)(resources.GetObject("txtFieldName.Size")));
			this.txtFieldName.TabIndex = ((int)(resources.GetObject("txtFieldName.TabIndex")));
			this.txtFieldName.Text = resources.GetString("txtFieldName.Text");
			this.txtFieldName.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("txtFieldName.TextAlign")));
			this.txtFieldName.Visible = ((bool)(resources.GetObject("txtFieldName.Visible")));
			this.txtFieldName.WordWrap = ((bool)(resources.GetObject("txtFieldName.WordWrap")));
			// 
			// lblFieldName
			// 
			this.lblFieldName.AccessibleDescription = resources.GetString("lblFieldName.AccessibleDescription");
			this.lblFieldName.AccessibleName = resources.GetString("lblFieldName.AccessibleName");
			this.lblFieldName.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblFieldName.Anchor")));
			this.lblFieldName.AutoSize = ((bool)(resources.GetObject("lblFieldName.AutoSize")));
			this.lblFieldName.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblFieldName.Dock")));
			this.lblFieldName.Enabled = ((bool)(resources.GetObject("lblFieldName.Enabled")));
			this.lblFieldName.Font = ((System.Drawing.Font)(resources.GetObject("lblFieldName.Font")));
			this.lblFieldName.ForeColor = System.Drawing.Color.Maroon;
			this.lblFieldName.Image = ((System.Drawing.Image)(resources.GetObject("lblFieldName.Image")));
			this.lblFieldName.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblFieldName.ImageAlign")));
			this.lblFieldName.ImageIndex = ((int)(resources.GetObject("lblFieldName.ImageIndex")));
			this.lblFieldName.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblFieldName.ImeMode")));
			this.lblFieldName.Location = ((System.Drawing.Point)(resources.GetObject("lblFieldName.Location")));
			this.lblFieldName.Name = "lblFieldName";
			this.lblFieldName.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblFieldName.RightToLeft")));
			this.lblFieldName.Size = ((System.Drawing.Size)(resources.GetObject("lblFieldName.Size")));
			this.lblFieldName.TabIndex = ((int)(resources.GetObject("lblFieldName.TabIndex")));
			this.lblFieldName.Text = resources.GetString("lblFieldName.Text");
			this.lblFieldName.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblFieldName.TextAlign")));
			this.lblFieldName.Visible = ((bool)(resources.GetObject("lblFieldName.Visible")));
			// 
			// grpColumnGrouping
			// 
			this.grpColumnGrouping.AccessibleDescription = resources.GetString("grpColumnGrouping.AccessibleDescription");
			this.grpColumnGrouping.AccessibleName = resources.GetString("grpColumnGrouping.AccessibleName");
			this.grpColumnGrouping.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("grpColumnGrouping.Anchor")));
			this.grpColumnGrouping.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("grpColumnGrouping.BackgroundImage")));
			this.grpColumnGrouping.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("grpColumnGrouping.Dock")));
			this.grpColumnGrouping.Enabled = ((bool)(resources.GetObject("grpColumnGrouping.Enabled")));
			this.grpColumnGrouping.Font = ((System.Drawing.Font)(resources.GetObject("grpColumnGrouping.Font")));
			this.grpColumnGrouping.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("grpColumnGrouping.ImeMode")));
			this.grpColumnGrouping.Location = ((System.Drawing.Point)(resources.GetObject("grpColumnGrouping.Location")));
			this.grpColumnGrouping.Name = "grpColumnGrouping";
			this.grpColumnGrouping.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("grpColumnGrouping.RightToLeft")));
			this.grpColumnGrouping.Size = ((System.Drawing.Size)(resources.GetObject("grpColumnGrouping.Size")));
			this.grpColumnGrouping.TabIndex = ((int)(resources.GetObject("grpColumnGrouping.TabIndex")));
			this.grpColumnGrouping.TabStop = false;
			this.grpColumnGrouping.Text = resources.GetString("grpColumnGrouping.Text");
			this.grpColumnGrouping.Visible = ((bool)(resources.GetObject("grpColumnGrouping.Visible")));
			// 
			// lblAlign
			// 
			this.lblAlign.AccessibleDescription = resources.GetString("lblAlign.AccessibleDescription");
			this.lblAlign.AccessibleName = resources.GetString("lblAlign.AccessibleName");
			this.lblAlign.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblAlign.Anchor")));
			this.lblAlign.AutoSize = ((bool)(resources.GetObject("lblAlign.AutoSize")));
			this.lblAlign.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblAlign.Dock")));
			this.lblAlign.Enabled = ((bool)(resources.GetObject("lblAlign.Enabled")));
			this.lblAlign.Font = ((System.Drawing.Font)(resources.GetObject("lblAlign.Font")));
			this.lblAlign.Image = ((System.Drawing.Image)(resources.GetObject("lblAlign.Image")));
			this.lblAlign.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblAlign.ImageAlign")));
			this.lblAlign.ImageIndex = ((int)(resources.GetObject("lblAlign.ImageIndex")));
			this.lblAlign.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblAlign.ImeMode")));
			this.lblAlign.Location = ((System.Drawing.Point)(resources.GetObject("lblAlign.Location")));
			this.lblAlign.Name = "lblAlign";
			this.lblAlign.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblAlign.RightToLeft")));
			this.lblAlign.Size = ((System.Drawing.Size)(resources.GetObject("lblAlign.Size")));
			this.lblAlign.TabIndex = ((int)(resources.GetObject("lblAlign.TabIndex")));
			this.lblAlign.Text = resources.GetString("lblAlign.Text");
			this.lblAlign.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblAlign.TextAlign")));
			this.lblAlign.Visible = ((bool)(resources.GetObject("lblAlign.Visible")));
			// 
			// cboAlign
			// 
			this.cboAlign.AccessibleDescription = resources.GetString("cboAlign.AccessibleDescription");
			this.cboAlign.AccessibleName = resources.GetString("cboAlign.AccessibleName");
			this.cboAlign.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("cboAlign.Anchor")));
			this.cboAlign.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cboAlign.BackgroundImage")));
			this.cboAlign.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("cboAlign.Dock")));
			this.cboAlign.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboAlign.Enabled = ((bool)(resources.GetObject("cboAlign.Enabled")));
			this.cboAlign.Font = ((System.Drawing.Font)(resources.GetObject("cboAlign.Font")));
			this.cboAlign.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("cboAlign.ImeMode")));
			this.cboAlign.IntegralHeight = ((bool)(resources.GetObject("cboAlign.IntegralHeight")));
			this.cboAlign.ItemHeight = ((int)(resources.GetObject("cboAlign.ItemHeight")));
			this.cboAlign.Items.AddRange(new object[] {
														  resources.GetString("cboAlign.Items"),
														  resources.GetString("cboAlign.Items1"),
														  resources.GetString("cboAlign.Items2"),
														  resources.GetString("cboAlign.Items3")});
			this.cboAlign.Location = ((System.Drawing.Point)(resources.GetObject("cboAlign.Location")));
			this.cboAlign.MaxDropDownItems = ((int)(resources.GetObject("cboAlign.MaxDropDownItems")));
			this.cboAlign.MaxLength = ((int)(resources.GetObject("cboAlign.MaxLength")));
			this.cboAlign.Name = "cboAlign";
			this.cboAlign.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("cboAlign.RightToLeft")));
			this.cboAlign.Size = ((System.Drawing.Size)(resources.GetObject("cboAlign.Size")));
			this.cboAlign.TabIndex = ((int)(resources.GetObject("cboAlign.TabIndex")));
			this.cboAlign.Text = resources.GetString("cboAlign.Text");
			this.cboAlign.Visible = ((bool)(resources.GetObject("cboAlign.Visible")));
			// 
			// chkVisible
			// 
			this.chkVisible.AccessibleDescription = resources.GetString("chkVisible.AccessibleDescription");
			this.chkVisible.AccessibleName = resources.GetString("chkVisible.AccessibleName");
			this.chkVisible.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("chkVisible.Anchor")));
			this.chkVisible.Appearance = ((System.Windows.Forms.Appearance)(resources.GetObject("chkVisible.Appearance")));
			this.chkVisible.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("chkVisible.BackgroundImage")));
			this.chkVisible.CheckAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("chkVisible.CheckAlign")));
			this.chkVisible.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("chkVisible.Dock")));
			this.chkVisible.Enabled = ((bool)(resources.GetObject("chkVisible.Enabled")));
			this.chkVisible.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("chkVisible.FlatStyle")));
			this.chkVisible.Font = ((System.Drawing.Font)(resources.GetObject("chkVisible.Font")));
			this.chkVisible.Image = ((System.Drawing.Image)(resources.GetObject("chkVisible.Image")));
			this.chkVisible.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("chkVisible.ImageAlign")));
			this.chkVisible.ImageIndex = ((int)(resources.GetObject("chkVisible.ImageIndex")));
			this.chkVisible.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("chkVisible.ImeMode")));
			this.chkVisible.Location = ((System.Drawing.Point)(resources.GetObject("chkVisible.Location")));
			this.chkVisible.Name = "chkVisible";
			this.chkVisible.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("chkVisible.RightToLeft")));
			this.chkVisible.Size = ((System.Drawing.Size)(resources.GetObject("chkVisible.Size")));
			this.chkVisible.TabIndex = ((int)(resources.GetObject("chkVisible.TabIndex")));
			this.chkVisible.Text = resources.GetString("chkVisible.Text");
			this.chkVisible.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("chkVisible.TextAlign")));
			this.chkVisible.Visible = ((bool)(resources.GetObject("chkVisible.Visible")));
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
			// 
			// btnSave
			// 
			this.btnSave.AccessibleDescription = resources.GetString("btnSave.AccessibleDescription");
			this.btnSave.AccessibleName = resources.GetString("btnSave.AccessibleName");
			this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnSave.Anchor")));
			this.btnSave.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSave.BackgroundImage")));
			this.btnSave.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnSave.Dock")));
			this.btnSave.Enabled = ((bool)(resources.GetObject("btnSave.Enabled")));
			this.btnSave.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnSave.FlatStyle")));
			this.btnSave.Font = ((System.Drawing.Font)(resources.GetObject("btnSave.Font")));
			this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
			this.btnSave.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnSave.ImageAlign")));
			this.btnSave.ImageIndex = ((int)(resources.GetObject("btnSave.ImageIndex")));
			this.btnSave.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnSave.ImeMode")));
			this.btnSave.Location = ((System.Drawing.Point)(resources.GetObject("btnSave.Location")));
			this.btnSave.Name = "btnSave";
			this.btnSave.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnSave.RightToLeft")));
			this.btnSave.Size = ((System.Drawing.Size)(resources.GetObject("btnSave.Size")));
			this.btnSave.TabIndex = ((int)(resources.GetObject("btnSave.TabIndex")));
			this.btnSave.Text = resources.GetString("btnSave.Text");
			this.btnSave.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnSave.TextAlign")));
			this.btnSave.Visible = ((bool)(resources.GetObject("btnSave.Visible")));
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
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
			// dlgFont
			// 
			this.dlgFont.Font = new System.Drawing.Font("Arial Narrow", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.dlgFont.FontMustExist = true;
			// 
			// tvwFieldList
			// 
			this.tvwFieldList.AccessibleDescription = resources.GetString("tvwFieldList.AccessibleDescription");
			this.tvwFieldList.AccessibleName = resources.GetString("tvwFieldList.AccessibleName");
			this.tvwFieldList.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tvwFieldList.Anchor")));
			this.tvwFieldList.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tvwFieldList.BackgroundImage")));
			this.tvwFieldList.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tvwFieldList.Dock")));
			this.tvwFieldList.Enabled = ((bool)(resources.GetObject("tvwFieldList.Enabled")));
			this.tvwFieldList.Font = ((System.Drawing.Font)(resources.GetObject("tvwFieldList.Font")));
			this.tvwFieldList.FullRowSelect = true;
			this.tvwFieldList.HideSelection = false;
			this.tvwFieldList.ImageIndex = ((int)(resources.GetObject("tvwFieldList.ImageIndex")));
			this.tvwFieldList.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tvwFieldList.ImeMode")));
			this.tvwFieldList.Indent = ((int)(resources.GetObject("tvwFieldList.Indent")));
			this.tvwFieldList.ItemHeight = ((int)(resources.GetObject("tvwFieldList.ItemHeight")));
			this.tvwFieldList.Location = ((System.Drawing.Point)(resources.GetObject("tvwFieldList.Location")));
			this.tvwFieldList.Name = "tvwFieldList";
			this.tvwFieldList.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tvwFieldList.RightToLeft")));
			this.tvwFieldList.SelectedImageIndex = ((int)(resources.GetObject("tvwFieldList.SelectedImageIndex")));
			this.tvwFieldList.Size = ((System.Drawing.Size)(resources.GetObject("tvwFieldList.Size")));
			this.tvwFieldList.TabIndex = ((int)(resources.GetObject("tvwFieldList.TabIndex")));
			this.tvwFieldList.Text = resources.GetString("tvwFieldList.Text");
			this.tvwFieldList.Visible = ((bool)(resources.GetObject("tvwFieldList.Visible")));
			this.tvwFieldList.DoubleClick += new System.EventHandler(this.tvwFieldList_DoubleClick);
			this.tvwFieldList.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvwFieldList_AfterSelect);
			// 
			// btnEdit
			// 
			this.btnEdit.AccessibleDescription = resources.GetString("btnEdit.AccessibleDescription");
			this.btnEdit.AccessibleName = resources.GetString("btnEdit.AccessibleName");
			this.btnEdit.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnEdit.Anchor")));
			this.btnEdit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnEdit.BackgroundImage")));
			this.btnEdit.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnEdit.Dock")));
			this.btnEdit.Enabled = ((bool)(resources.GetObject("btnEdit.Enabled")));
			this.btnEdit.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnEdit.FlatStyle")));
			this.btnEdit.Font = ((System.Drawing.Font)(resources.GetObject("btnEdit.Font")));
			this.btnEdit.Image = ((System.Drawing.Image)(resources.GetObject("btnEdit.Image")));
			this.btnEdit.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnEdit.ImageAlign")));
			this.btnEdit.ImageIndex = ((int)(resources.GetObject("btnEdit.ImageIndex")));
			this.btnEdit.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnEdit.ImeMode")));
			this.btnEdit.Location = ((System.Drawing.Point)(resources.GetObject("btnEdit.Location")));
			this.btnEdit.Name = "btnEdit";
			this.btnEdit.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnEdit.RightToLeft")));
			this.btnEdit.Size = ((System.Drawing.Size)(resources.GetObject("btnEdit.Size")));
			this.btnEdit.TabIndex = ((int)(resources.GetObject("btnEdit.TabIndex")));
			this.btnEdit.Text = resources.GetString("btnEdit.Text");
			this.btnEdit.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnEdit.TextAlign")));
			this.btnEdit.Visible = ((bool)(resources.GetObject("btnEdit.Visible")));
			this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
			// 
			// lblDefaultFont
			// 
			this.lblDefaultFont.AccessibleDescription = resources.GetString("lblDefaultFont.AccessibleDescription");
			this.lblDefaultFont.AccessibleName = resources.GetString("lblDefaultFont.AccessibleName");
			this.lblDefaultFont.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblDefaultFont.Anchor")));
			this.lblDefaultFont.AutoSize = ((bool)(resources.GetObject("lblDefaultFont.AutoSize")));
			this.lblDefaultFont.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblDefaultFont.Dock")));
			this.lblDefaultFont.Enabled = ((bool)(resources.GetObject("lblDefaultFont.Enabled")));
			this.lblDefaultFont.Font = ((System.Drawing.Font)(resources.GetObject("lblDefaultFont.Font")));
			this.lblDefaultFont.Image = ((System.Drawing.Image)(resources.GetObject("lblDefaultFont.Image")));
			this.lblDefaultFont.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblDefaultFont.ImageAlign")));
			this.lblDefaultFont.ImageIndex = ((int)(resources.GetObject("lblDefaultFont.ImageIndex")));
			this.lblDefaultFont.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblDefaultFont.ImeMode")));
			this.lblDefaultFont.Location = ((System.Drawing.Point)(resources.GetObject("lblDefaultFont.Location")));
			this.lblDefaultFont.Name = "lblDefaultFont";
			this.lblDefaultFont.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblDefaultFont.RightToLeft")));
			this.lblDefaultFont.Size = ((System.Drawing.Size)(resources.GetObject("lblDefaultFont.Size")));
			this.lblDefaultFont.TabIndex = ((int)(resources.GetObject("lblDefaultFont.TabIndex")));
			this.lblDefaultFont.Text = resources.GetString("lblDefaultFont.Text");
			this.lblDefaultFont.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblDefaultFont.TextAlign")));
			this.lblDefaultFont.Visible = ((bool)(resources.GetObject("lblDefaultFont.Visible")));
			// 
			// FieldProperties
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
			this.Controls.Add(this.lblDefaultFont);
			this.Controls.Add(this.btnEdit);
			this.Controls.Add(this.tvwFieldList);
			this.Controls.Add(this.btnHelp);
			this.Controls.Add(this.btnSave);
			this.Controls.Add(this.btnClose);
			this.Controls.Add(this.grpFieldProperties);
			this.Controls.Add(this.btnDown);
			this.Controls.Add(this.btnUp);
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
			this.Name = "FieldProperties";
			this.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("$this.RightToLeft")));
			this.StartPosition = ((System.Windows.Forms.FormStartPosition)(resources.GetObject("$this.StartPosition")));
			this.Text = resources.GetString("$this.Text");
			this.Closing += new System.ComponentModel.CancelEventHandler(this.FieldProperties_Closing);
			this.Load += new System.EventHandler(this.FieldProperties_Load);
			this.grpFieldProperties.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.nudWidth)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		#region Form Events

		//**************************************************************************              
		///    <Description>
		///       Form load event, determide mode for form
		///    </Description>
		///    <Inputs>
		///       
		///    </Inputs>
		///    <Outputs>
		///       
		///    </Outputs>
		///    <Returns>
		///       
		///    </Returns>
		///    <Authors>
		///       DungLA
		///    </Authors>
		///    <History>
		///       06-Jan-2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private void FieldProperties_Load(object sender, EventArgs e)
		{
			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.WaitCursor;

			#endregion Code Inserted Automatically

			
			const string METHOD_NAME = THIS + ".FieldProperties_Load()";
			try
			{
				#region Security
				//Set authorization for user
				Security objSecurity = new Security();
				this.Name = THIS;
				if(objSecurity.SetRightForUserOnForm(this, SystemProperty.UserName) == 0)
				{
					this.Close();
					// You don't have the right to view this item
					PCSMessageBox.Show(ErrorCode.MESSAGE_YOU_HAVE_NO_RIGHT_TO_VIEW);
					// Code Inserted Automatically
#region Code Inserted Automatically
this.Cursor = Cursors.Default;
#endregion Code Inserted Automatically

					return;
				}
				#endregion

				// set title
				this.Text = this.Text + mvoReport.ReportName + Constants.CLOSE_SBRACKET;
				// bind list box
				BuildTree(arrFields);
				txtFieldName.Enabled = false;
				SwitchFormMode();
				// format Width number
				nudWidth.CustomFormat = Constants.DECIMAL_NUMBERFORMAT;
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

			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.Default;

			#endregion Code Inserted Automatically

			
		}

		//**************************************************************************              
		///    <Description>
		///       Display Font dialog allows user to select font
		///    </Description>
		///    <Inputs>
		///       
		///    </Inputs>
		///    <Outputs>
		///       
		///    </Outputs>
		///    <Returns>
		///       
		///    </Returns>
		///    <Authors>
		///       DungLA
		///    </Authors>
		///    <History>
		///       06-Jan-2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private void btnFont_Click(object sender, EventArgs e)
		{
			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.WaitCursor;

			#endregion Code Inserted Automatically

			
			const string METHOD_NAME = THIS + ".btnFont_Click()";
			try
			{
				dlgFont.Font = lblDefaultFont.Font; 
				if (dlgFont.ShowDialog() == DialogResult.OK)
				{
					if ((dlgFont.Font.Name != string.Empty) && (!dlgFont.FontMustExist))
					{
						// get selected font.
						voSelectedField.Font = GetSelectedFont(dlgFont.Font);
					}
				}
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

			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.Default;

			#endregion Code Inserted Automatically

			
		}

		//**************************************************************************              
		///    <Description>
		///       Move down item
		///    </Description>
		///    <Inputs>
		///       
		///    </Inputs>
		///    <Outputs>
		///       
		///    </Outputs>
		///    <Returns>
		///       
		///    </Returns>
		///    <Authors>
		///       DungLA
		///    </Authors>
		///    <History>
		///       06-Jan-2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private void btnDown_Click(object sender, EventArgs e)
		{
			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.WaitCursor;

			#endregion Code Inserted Automatically

			
			const string METHOD_NAME = THIS + ".btnDown_Click()";
			try
			{
				TreeNode objSelectedNode = tvwFieldList.SelectedNode;
				if (objSelectedNode != null)
				{
					if (objSelectedNode.NextVisibleNode != null)
					{
						sys_ReportFieldsVO voCurrentField = (sys_ReportFieldsVO)objSelectedNode.Tag;
						sys_ReportFieldsVO voNextField = (sys_ReportFieldsVO)objSelectedNode.NextVisibleNode.Tag;
						// moving
						tvwFieldList.Nodes.Remove(objSelectedNode);
						tvwFieldList.Nodes.Insert(objSelectedNode.Index + 1, objSelectedNode);
						// now update field object
						voCurrentField.FieldOrder = voNextField.FieldOrder;
						voNextField.FieldOrder = voNextField.FieldOrder - 1;
						boFieldProperties.SwitchFields(voCurrentField, voNextField);
					}
					// set focus on selected on
					tvwFieldList.Focus();
					tvwFieldList.Select();
					tvwFieldList.SelectedNode = objSelectedNode;
				}
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

			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.Default;

			#endregion Code Inserted Automatically

			
		}

		//**************************************************************************              
		///    <Description>
		///       Move up item
		///    </Description>
		///    <Inputs>
		///       
		///    </Inputs>
		///    <Outputs>
		///       
		///    </Outputs>
		///    <Returns>
		///       
		///    </Returns>
		///    <Authors>
		///       DungLA
		///    </Authors>
		///    <History>
		///       06-Jan-2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private void btnUp_Click(object sender, EventArgs e)
		{
			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.WaitCursor;

			#endregion Code Inserted Automatically

			
			const string METHOD_NAME = THIS + ".btnUp_Click()";
			try
			{
				TreeNode objSelectedNode = tvwFieldList.SelectedNode;
				if (objSelectedNode != null)
				{
					if (objSelectedNode.PrevVisibleNode != null)
					{
						sys_ReportFieldsVO voCurrentField = (sys_ReportFieldsVO)objSelectedNode.Tag;
						sys_ReportFieldsVO voPrevField = (sys_ReportFieldsVO)objSelectedNode.PrevVisibleNode.Tag;
						// moving
						tvwFieldList.Nodes.Remove(objSelectedNode);
						tvwFieldList.Nodes.Insert(objSelectedNode.Index - 1, objSelectedNode);
						// now update field object
						voCurrentField.FieldOrder = voPrevField.FieldOrder;
						voPrevField.FieldOrder = voPrevField.FieldOrder + 1;
						boFieldProperties.SwitchFields(voCurrentField, voPrevField);
					}
					// set focus on selected on
					tvwFieldList.Focus();
					tvwFieldList.Select();
					tvwFieldList.SelectedNode = objSelectedNode;
				}
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

			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.Default;

			#endregion Code Inserted Automatically

			
		}

		//**************************************************************************              
		///    <Description>
		///       Check mandatory field, check biz rules and save data
		///    </Description>
		///    <Inputs>
		///       
		///    </Inputs>
		///    <Outputs>
		///       
		///    </Outputs>
		///    <Returns>
		///       
		///    </Returns>
		///    <Authors>
		///       DungLA
		///    </Authors>
		///    <History>
		///       18-Jan-2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private void btnSave_Click(object sender, EventArgs e)
		{
			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.WaitCursor;

			#endregion Code Inserted Automatically

			
			const string METHOD_NAME = THIS + ".btnSave_Click()";
			try
			{
				#region Check mandatory fields

				// check mandatory field
				if (FormControlComponents.CheckMandatory(txtFieldName))
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Error);
					txtFieldName.Focus();
					txtFieldName.Select();
					// Code Inserted Automatically
#region Code Inserted Automatically
this.Cursor = Cursors.Default;
#endregion Code Inserted Automatically

					return;
				}
				if (FormControlComponents.CheckMandatory(txtCaption))
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Error);
					txtCaption.Focus();
					txtCaption.Select();
					// Code Inserted Automatically
#region Code Inserted Automatically
this.Cursor = Cursors.Default;
#endregion Code Inserted Automatically

					return;
				}
				if (FormControlComponents.CheckMandatory(txtCaptionEN))
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Error);
					txtCaptionEN.Focus();
					txtCaptionEN.Select();
					// Code Inserted Automatically
#region Code Inserted Automatically
this.Cursor = Cursors.Default;
#endregion Code Inserted Automatically

					return;
				}
				if (FormControlComponents.CheckMandatory(txtCaptionJP))
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Error);
					txtCaptionJP.Focus();
					txtCaptionJP.Select();
					// Code Inserted Automatically
#region Code Inserted Automatically
this.Cursor = Cursors.Default;
#endregion Code Inserted Automatically

					return;
				}
				if (FormControlComponents.CheckMandatory(txtCaptionVN))
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID,MessageBoxIcon.Error);
					txtCaptionVN.Focus();
					txtCaptionVN.Select();
					// Code Inserted Automatically
#region Code Inserted Automatically
this.Cursor = Cursors.Default;
#endregion Code Inserted Automatically

					return;
				}
				if (nudWidth.ValueIsDbNull)
				{
					PCSMessageBox.Show(ErrorCode.MESSAGE_INVALID_NUMERIC, MessageBoxIcon.Error);
					nudWidth.Focus();
					nudWidth.Select();
					// Code Inserted Automatically
#region Code Inserted Automatically
this.Cursor = Cursors.Default;
#endregion Code Inserted Automatically

					return;
				}
				else if (decimal.Parse(nudWidth.Value.ToString()) <= 0)
				{
					PCSMessageBox.Show(ErrorCode.POSITIVE_NUMBER, MessageBoxIcon.Error);
					nudWidth.Focus();
					nudWidth.Select();
					// Code Inserted Automatically
#region Code Inserted Automatically
this.Cursor = Cursors.Default;
#endregion Code Inserted Automatically

					return;
				}

				#endregion

				if (SaveData())
				{
					// turn off edit mode
					btnSave.Enabled = false;
					mFormAction = EnumAction.Default;
					PCSMessageBox.Show(ErrorCode.MESSAGE_AFTER_SAVE_DATA, MessageBoxIcon.Information);
					SwitchFormMode();					
					tvwFieldList.Select();
				}
				else
					PCSMessageBox.Show(ErrorCode.MESSAGE_CANNOT_SAVE_TO_DB, MessageBoxIcon.Error);
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

			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.Default;

			#endregion Code Inserted Automatically

			
		}

		//**************************************************************************              
		///    <Description>
		///       If current mode is Edit then display confirm message. Close the form
		///    </Description>
		///    <Inputs>
		///       
		///    </Inputs>
		///    <Outputs>
		///       
		///    </Outputs>
		///    <Returns>
		///       
		///    </Returns>
		///    <Authors>
		///       DungLA
		///    </Authors>
		///    <History>
		///       18-Jan-2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private void btnClose_Click(object sender, EventArgs e)
		{
			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.WaitCursor;

			#endregion Code Inserted Automatically

			
			this.Close();

			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.Default;

			#endregion Code Inserted Automatically

			
		}

		#region OLDCODE, use with txtWidth. Now we use C1NumericEdit for number input control
		
		//**************************************************************************              
		///    <Description>
		///       Change format (add thousands separator) of text
		///    </Description>
		///    <Inputs>
		///       
		///    </Inputs>
		///    <Outputs>
		///       
		///    </Outputs>
		///    <Returns>
		///       
		///    </Returns>
		///    <Authors>
		///       DungLA
		///    </Authors>
		///    <History>
		///       08-Jan-2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		/*private void txtWidth_TextChanged(object sender, EventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtWidth_TextChanged()";
			try
			{
				// if sender is TextBox receive numeric values, the add digit grouping
				// i.e: 123.456.789 based on culture
				if (sender.GetType().Equals(typeof(TextBox)))
				{
					// make sure that user input numeric values
					if (FormControlComponents.IsNumeric(txtWidth.Text))
					{
						// format input string
						if ((txtWidth.Text.Length > 3) && (txtWidth.Text.Length <= txtWidth.MaxLength))
						{
							txtWidth.Text = this.AddThousandsSeparator(txtWidth.Text);
						}
					}
				}
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
		}*/


		#endregion

		#endregion

		#region private methods
		
		/// <summary>
		/// Build the fields tree
		/// </summary>
		/// <param name="parrSource"></param>
		private void BuildTree(ArrayList parrSource)
		{
			try
			{
				foreach (sys_ReportFieldsVO voReportField in parrSource)
				{
					TreeNode objFieldNode = new TreeNode(voReportField.FieldName);
					objFieldNode.Tag = voReportField;
					tvwFieldList.Nodes.Add(objFieldNode);
				}
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

		//**************************************************************************              
		///    <Description>
		///       This method use to save data to data base
		///    </Description>
		///    <Inputs>
		///       
		///    </Inputs>
		///    <Outputs>
		///       bool
		///    </Outputs>
		///    <Returns>
		///       true if successful, false if failed
		///    </Returns>
		///    <Authors>
		///       DungLA
		///    </Authors>
		///    <History>
		///       03-Feb-2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private bool SaveData()
		{
			try
			{
				voSelectedField.ReportID = mvoReport.ReportID;
				voSelectedField.FieldName = txtFieldName.Text.Trim();
				voSelectedField.FieldCaption = txtCaption.Text.Trim();
				voSelectedField.FieldCaptionEN = txtCaptionEN.Text.Trim();
				voSelectedField.FieldCaptionJP = txtCaptionJP.Text.Trim();
				voSelectedField.FieldCaptionVN = txtCaptionVN.Text.Trim();
				voSelectedField.Type = (cboType.SelectedIndex > -1) ? cboType.SelectedIndex : 0;
				//voSelectedField.Width = int.Parse(txtWidth.Text.ToString(), NumberStyles.AllowThousands);
				voSelectedField.Width = FormControlComponents.ConvertIncheToTwips((decimal)nudWidth.Value);

				//voSelectedField.Font = btnFont.Tag.ToString();
				voSelectedField.Format = txtFormat.Text.Trim();
				voSelectedField.Sort = (cboSort.SelectedIndex > -1) ? cboSort.SelectedIndex : PCSSortType.NONE;
				voSelectedField.Align = (cboAlign.SelectedIndex > -1) ? cboAlign.SelectedIndex : PCSAligmentType.NONE;
				voSelectedField.PrintPreview = chkPrint.Checked;
				voSelectedField.Visisble = chkVisible.Checked;
				voSelectedField.GroupBy = chkGroupBy.Checked;
				voSelectedField.BottomGroup = chkDisplayBottom.Checked;
				voSelectedField.SumTopPage = chkSumTopPage.Checked;
				voSelectedField.SumTopReport = chkSumTopReport.Checked;
				voSelectedField.SumBottomPage = chkSumBottomPage.Checked;
				voSelectedField.SumBottomReport = chkSumBottomReport.Checked;
				if (this.mFormAction == EnumAction.Add)
				{
					// get new order
					int intFieldOrder = boFieldProperties.GetMaxFieldOrder(mvoReport.ReportID) + 1;
					voSelectedField.FieldOrder = intFieldOrder;
					boFieldProperties.Add(voSelectedField);
				}
				else if (this.mFormAction == EnumAction.Edit)
				{
					boFieldProperties.Update(voSelectedField);
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
		#endregion

		private void FieldProperties_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".FieldProperties_Closing()";
			try
			{
				if (this.mFormAction != EnumAction.Default)
				{
					// display confirm message					
					DialogResult dlgResult = PCSMessageBox.Show(ErrorCode.MESSAGE_QUESTION_STORE_INTO_DATABASE,MessageBoxButtons.YesNoCancel,MessageBoxIcon.Question,MessageBoxDefaultButton.Button1);
					switch (dlgResult)
					{
						case DialogResult.Yes:
							try
							{
								if (!this.SaveData())
								{
									e.Cancel = true;
								}
							}
							catch
							{
								e.Cancel = true;
							}
							break;
						case DialogResult.No:
							e.Cancel = false;
							break;
						case DialogResult.Cancel:
							e.Cancel = true;
							break;
					}
				}
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
		/// Load data of selected field to control
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void tvwFieldList_AfterSelect(object sender, TreeViewEventArgs e)
		{
			const string METHOD_NAME = THIS + ".tvwFieldList_AfterSelect()";
			try
			{
				mFormAction = EnumAction.Default;
				voSelectedField = (sys_ReportFieldsVO)tvwFieldList.SelectedNode.Tag;
				// bind all properties
				txtFieldName.Text = voSelectedField.FieldName;
				txtCaption.Text = voSelectedField.FieldCaption;
				txtCaptionEN.Text = voSelectedField.FieldCaptionEN;
				txtCaptionJP.Text = voSelectedField.FieldCaptionJP;
				txtCaptionVN.Text = voSelectedField.FieldCaptionVN;
				cboType.SelectedIndex = voSelectedField.Type;
				//txtWidth.Text = voSelectedField.Width.ToString();
				nudWidth.Value = FormControlComponents.ConvertTwipsToInches(voSelectedField.Width);
				txtFormat.Text = voSelectedField.Format;
				chkVisible.Checked = voSelectedField.Visisble;
				chkPrint.Checked = voSelectedField.PrintPreview;;
				cboSort.SelectedIndex = voSelectedField.Sort;
				cboAlign.SelectedIndex = voSelectedField.Align;
				chkGroupBy.Checked = voSelectedField.GroupBy;
				chkDisplayBottom.Checked = voSelectedField.BottomGroup;
				chkSumTopPage.Checked = voSelectedField.SumTopPage;
				chkSumBottomPage.Checked = voSelectedField.SumBottomPage;
				chkSumTopReport.Checked = voSelectedField.SumTopReport;
				chkSumBottomReport.Checked = voSelectedField.SumBottomReport;
				btnFont.Tag = voSelectedField.Font;
				if (tvwFieldList.SelectedNode.PrevVisibleNode == null || tvwFieldList.SelectedNode.PrevNode == null)
					btnUp.Enabled = false;
				else
					btnUp.Enabled = true;
				if (tvwFieldList.SelectedNode.NextVisibleNode == null || tvwFieldList.SelectedNode.NextNode == null)
					btnDown.Enabled = false;
				else
					btnDown.Enabled = true;
				SwitchFormMode();
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
		/// Edit selected field
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void tvwFieldList_DoubleClick(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".tvwFieldList_DoubleClick()";
			try
			{
				btnEdit_Click(null, null);
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
		/// Turn form to Edit mode
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnEdit_Click(object sender, EventArgs e)
		{
			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.WaitCursor;

			#endregion Code Inserted Automatically

			
			const string METHOD_NAME = THIS + ".btnEdit_Click()";
			try
			{
				// turn to edit mode
				mFormAction = EnumAction.Edit;
				SwitchFormMode();
				btnFont.Select();
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

			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.Default;

			#endregion Code Inserted Automatically

			
		}

		/// <summary>
		/// Enable buttons based on form mode
		/// </summary>
		private void SwitchFormMode()
		{
			try
			{
				switch (mFormAction)
				{
					case EnumAction.Default:
						btnEdit.Enabled = true;
						btnSave.Enabled = false;
						grpFieldProperties.Enabled = false;
						break;
					case EnumAction.Edit:
						btnEdit.Enabled = false;
						btnUp.Enabled = false;
						btnDown.Enabled = false;
						btnSave.Enabled = true;
						grpFieldProperties.Enabled = true;
						foreach (Control objCtrl in grpFieldProperties.Controls)
						{
							if (!objCtrl.Equals(txtFieldName))
								objCtrl.Enabled = true;
						}
						chkDisplayBottom.Enabled = true;
						chkDisplayBottom.Enabled = (chkGroupBy.Checked && chkGroupBy.Enabled);
						break;
				}
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
		/// <summary>
		/// This method used to convert selected font object to correct format string
		/// </summary>
		/// <param name="pobjFont">Font</param>
		/// <returns>Font in string format</returns>
		private string GetSelectedFont(Font pobjFont)
		{
			try
			{
				StringBuilder strSelectedFont = new StringBuilder();
				strSelectedFont.Append(pobjFont.Name).Append(FONT_SEPARATOR).Append(pobjFont.Size).Append(FONT_SEPARATOR);
				strSelectedFont.Append(pobjFont.Style).Append(FONT_SEPARATOR).Append(pobjFont.GdiCharSet.ToString()).Append(FONT_SEPARATOR);
				strSelectedFont.Append(pobjFont.GdiVerticalFont).Append(FONT_SEPARATOR).Append(pobjFont.Unit);
				return strSelectedFont.ToString();
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		/// <summary>
		/// Enable or disalbe option display result on bottom of group
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void chkGroupBy_CheckedChanged(object sender, EventArgs e)
		{
			const string METHOD_NAME = THIS + ".chkGroupBy_CheckedChanged()";
			try
			{
				chkDisplayBottom.Enabled = chkGroupBy.Checked;
				if (!chkGroupBy.Checked)
					chkDisplayBottom.Checked = false;
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
	}
}
