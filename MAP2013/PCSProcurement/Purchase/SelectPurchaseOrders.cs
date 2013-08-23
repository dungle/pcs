using System;
using System.Data;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

//PCS's namespaces
using PCSComProcurement.Purchase.BO;
using PCSComProcurement.Purchase.DS;
using PCSUtils.Utils;
using PCSComUtils.Common.BO;
using PCSComUtils.Admin.BO;
using PCSComUtils.MasterSetup.BO;
using PCSComUtils.Common;
using PCSComUtils.PCSExc;
using PCSUtils.Log;
using PCSComUtils.MasterSetup.DS;

namespace PCSProcurement.Purchase
{
	/// <summary>
	/// Summary description for SelectPurchaseOrders.
	/// </summary>
	public class SelectPurchaseOrders : System.Windows.Forms.Form
	{
		#region Declarations

		private System.Windows.Forms.CheckBox chkSelectAll;
		private System.Windows.Forms.Button btnClose;
		private System.Windows.Forms.Button btnHelp;
		private System.Windows.Forms.Button btnSelect;
		private System.Windows.Forms.Button btnSearch;		
		private System.Windows.Forms.Label lblPO;
		private System.Windows.Forms.Button btnSearchBeginPO;
		private C1.Win.C1TrueDBGrid.C1TrueDBGrid dgrdData;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		/// 
		#region Constants
		
		private const string THIS = "PCSProcurement.Purchase.SelectPurchaseOrders";		
		private const string SELECTED_COL = "Selected";
		private const string ZERO_STRING = "0";		
		private const string UNCLOSE_PO_4_INVOICE_VIEW = "v_SelectUnclosedPO4Invoice";

		#endregion Constants
		
		private System.ComponentModel.Container components = null;		
		private DataTable dtbData;
		private DataTable dtStoreGridLayout;	
		
		#endregion Declarations		

		#region Properties
		
		private Hashtable mhtbCondition;
		private System.Windows.Forms.TextBox txtBeginPO;
		private C1.Win.C1Input.C1DateEdit dtmToDeliveryDate;
		private C1.Win.C1Input.C1DateEdit dtmFromDeliveryDate;
		private System.Windows.Forms.Label lblToDeliveryDate;
		private System.Windows.Forms.Label lblFromDeliveryDate;
	
		public Hashtable ConditionHashTable
		{
			set{ mhtbCondition = value;}
			get{ return mhtbCondition;}
		}
		
		private Hashtable mSelectedRows;
		public Hashtable SelectedRows
		{
			set{ mSelectedRows = value;}
			get{ return mSelectedRows;}
		}

		#endregion Properties
		
		#region Constructor, Destructor
		
		public SelectPurchaseOrders()
		{
			InitializeComponent();	
			mhtbCondition = new Hashtable();
		}

		public SelectPurchaseOrders(Hashtable phtbCondition)
		{
			InitializeComponent();
			if(phtbCondition != null)
			{
				mhtbCondition = phtbCondition;
			}
			else
			{
				mhtbCondition = new Hashtable();
			}
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
		
		#endregion Properties
	
		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(SelectPurchaseOrders));
			this.chkSelectAll = new System.Windows.Forms.CheckBox();
			this.btnClose = new System.Windows.Forms.Button();
			this.btnHelp = new System.Windows.Forms.Button();
			this.btnSelect = new System.Windows.Forms.Button();
			this.dtmToDeliveryDate = new C1.Win.C1Input.C1DateEdit();
			this.dtmFromDeliveryDate = new C1.Win.C1Input.C1DateEdit();
			this.lblToDeliveryDate = new System.Windows.Forms.Label();
			this.btnSearch = new System.Windows.Forms.Button();
			this.btnSearchBeginPO = new System.Windows.Forms.Button();
			this.txtBeginPO = new System.Windows.Forms.TextBox();
			this.lblFromDeliveryDate = new System.Windows.Forms.Label();
			this.lblPO = new System.Windows.Forms.Label();
			this.dgrdData = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
			((System.ComponentModel.ISupportInitialize)(this.dtmToDeliveryDate)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dtmFromDeliveryDate)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dgrdData)).BeginInit();
			this.SuspendLayout();
			// 
			// chkSelectAll
			// 
			this.chkSelectAll.AccessibleDescription = resources.GetString("chkSelectAll.AccessibleDescription");
			this.chkSelectAll.AccessibleName = resources.GetString("chkSelectAll.AccessibleName");
			this.chkSelectAll.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("chkSelectAll.Anchor")));
			this.chkSelectAll.Appearance = ((System.Windows.Forms.Appearance)(resources.GetObject("chkSelectAll.Appearance")));
			this.chkSelectAll.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("chkSelectAll.BackgroundImage")));
			this.chkSelectAll.CheckAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("chkSelectAll.CheckAlign")));
			this.chkSelectAll.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("chkSelectAll.Dock")));
			this.chkSelectAll.Enabled = ((bool)(resources.GetObject("chkSelectAll.Enabled")));
			this.chkSelectAll.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("chkSelectAll.FlatStyle")));
			this.chkSelectAll.Font = ((System.Drawing.Font)(resources.GetObject("chkSelectAll.Font")));
			this.chkSelectAll.Image = ((System.Drawing.Image)(resources.GetObject("chkSelectAll.Image")));
			this.chkSelectAll.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("chkSelectAll.ImageAlign")));
			this.chkSelectAll.ImageIndex = ((int)(resources.GetObject("chkSelectAll.ImageIndex")));
			this.chkSelectAll.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("chkSelectAll.ImeMode")));
			this.chkSelectAll.Location = ((System.Drawing.Point)(resources.GetObject("chkSelectAll.Location")));
			this.chkSelectAll.Name = "chkSelectAll";
			this.chkSelectAll.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("chkSelectAll.RightToLeft")));
			this.chkSelectAll.Size = ((System.Drawing.Size)(resources.GetObject("chkSelectAll.Size")));
			this.chkSelectAll.TabIndex = ((int)(resources.GetObject("chkSelectAll.TabIndex")));
			this.chkSelectAll.Text = resources.GetString("chkSelectAll.Text");
			this.chkSelectAll.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("chkSelectAll.TextAlign")));
			this.chkSelectAll.Visible = ((bool)(resources.GetObject("chkSelectAll.Visible")));
			this.chkSelectAll.Click += new System.EventHandler(this.chkSelectAll_Click);
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
			// 
			// btnSelect
			// 
			this.btnSelect.AccessibleDescription = resources.GetString("btnSelect.AccessibleDescription");
			this.btnSelect.AccessibleName = resources.GetString("btnSelect.AccessibleName");
			this.btnSelect.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnSelect.Anchor")));
			this.btnSelect.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSelect.BackgroundImage")));
			this.btnSelect.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnSelect.Dock")));
			this.btnSelect.Enabled = ((bool)(resources.GetObject("btnSelect.Enabled")));
			this.btnSelect.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnSelect.FlatStyle")));
			this.btnSelect.Font = ((System.Drawing.Font)(resources.GetObject("btnSelect.Font")));
			this.btnSelect.Image = ((System.Drawing.Image)(resources.GetObject("btnSelect.Image")));
			this.btnSelect.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnSelect.ImageAlign")));
			this.btnSelect.ImageIndex = ((int)(resources.GetObject("btnSelect.ImageIndex")));
			this.btnSelect.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnSelect.ImeMode")));
			this.btnSelect.Location = ((System.Drawing.Point)(resources.GetObject("btnSelect.Location")));
			this.btnSelect.Name = "btnSelect";
			this.btnSelect.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnSelect.RightToLeft")));
			this.btnSelect.Size = ((System.Drawing.Size)(resources.GetObject("btnSelect.Size")));
			this.btnSelect.TabIndex = ((int)(resources.GetObject("btnSelect.TabIndex")));
			this.btnSelect.Text = resources.GetString("btnSelect.Text");
			this.btnSelect.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnSelect.TextAlign")));
			this.btnSelect.Visible = ((bool)(resources.GetObject("btnSelect.Visible")));
			this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
			// 
			// dtmToDeliveryDate
			// 
			this.dtmToDeliveryDate.AcceptsEscape = ((bool)(resources.GetObject("dtmToDeliveryDate.AcceptsEscape")));
			this.dtmToDeliveryDate.AccessibleDescription = resources.GetString("dtmToDeliveryDate.AccessibleDescription");
			this.dtmToDeliveryDate.AccessibleName = resources.GetString("dtmToDeliveryDate.AccessibleName");
			this.dtmToDeliveryDate.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("dtmToDeliveryDate.Anchor")));
			this.dtmToDeliveryDate.AutoSize = ((bool)(resources.GetObject("dtmToDeliveryDate.AutoSize")));
			this.dtmToDeliveryDate.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("dtmToDeliveryDate.BackgroundImage")));
			this.dtmToDeliveryDate.BorderStyle = ((System.Windows.Forms.BorderStyle)(resources.GetObject("dtmToDeliveryDate.BorderStyle")));
			// 
			// dtmToDeliveryDate.Calendar
			// 
			this.dtmToDeliveryDate.Calendar.AccessibleDescription = resources.GetString("dtmToDeliveryDate.Calendar.AccessibleDescription");
			this.dtmToDeliveryDate.Calendar.AccessibleName = resources.GetString("dtmToDeliveryDate.Calendar.AccessibleName");
			this.dtmToDeliveryDate.Calendar.AnnuallyBoldedDates = ((System.DateTime[])(resources.GetObject("dtmToDeliveryDate.Calendar.AnnuallyBoldedDates")));
			this.dtmToDeliveryDate.Calendar.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("dtmToDeliveryDate.Calendar.BackgroundImage")));
			this.dtmToDeliveryDate.Calendar.BoldedDates = ((System.DateTime[])(resources.GetObject("dtmToDeliveryDate.Calendar.BoldedDates")));
			this.dtmToDeliveryDate.Calendar.CalendarDimensions = ((System.Drawing.Size)(resources.GetObject("dtmToDeliveryDate.Calendar.CalendarDimensions")));
			this.dtmToDeliveryDate.Calendar.Enabled = ((bool)(resources.GetObject("dtmToDeliveryDate.Calendar.Enabled")));
			this.dtmToDeliveryDate.Calendar.FirstDayOfWeek = ((System.Windows.Forms.Day)(resources.GetObject("dtmToDeliveryDate.Calendar.FirstDayOfWeek")));
			this.dtmToDeliveryDate.Calendar.Font = ((System.Drawing.Font)(resources.GetObject("dtmToDeliveryDate.Calendar.Font")));
			this.dtmToDeliveryDate.Calendar.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("dtmToDeliveryDate.Calendar.ImeMode")));
			this.dtmToDeliveryDate.Calendar.MonthlyBoldedDates = ((System.DateTime[])(resources.GetObject("dtmToDeliveryDate.Calendar.MonthlyBoldedDates")));
			this.dtmToDeliveryDate.Calendar.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("dtmToDeliveryDate.Calendar.RightToLeft")));
			this.dtmToDeliveryDate.Calendar.ShowClearButton = ((bool)(resources.GetObject("dtmToDeliveryDate.Calendar.ShowClearButton")));
			this.dtmToDeliveryDate.Calendar.ShowTodayButton = ((bool)(resources.GetObject("dtmToDeliveryDate.Calendar.ShowTodayButton")));
			this.dtmToDeliveryDate.Calendar.ShowWeekNumbers = ((bool)(resources.GetObject("dtmToDeliveryDate.Calendar.ShowWeekNumbers")));
			this.dtmToDeliveryDate.CaseSensitive = ((bool)(resources.GetObject("dtmToDeliveryDate.CaseSensitive")));
			this.dtmToDeliveryDate.Culture = ((int)(resources.GetObject("dtmToDeliveryDate.Culture")));
			this.dtmToDeliveryDate.CurrentTimeZone = ((bool)(resources.GetObject("dtmToDeliveryDate.CurrentTimeZone")));
			this.dtmToDeliveryDate.CustomFormat = resources.GetString("dtmToDeliveryDate.CustomFormat");
			this.dtmToDeliveryDate.DaylightTimeAdjustment = ((C1.Win.C1Input.DaylightTimeAdjustmentEnum)(resources.GetObject("dtmToDeliveryDate.DaylightTimeAdjustment")));
			this.dtmToDeliveryDate.DisplayFormat.CustomFormat = resources.GetString("dtmToDeliveryDate.DisplayFormat.CustomFormat");
			this.dtmToDeliveryDate.DisplayFormat.FormatType = ((C1.Win.C1Input.FormatTypeEnum)(resources.GetObject("dtmToDeliveryDate.DisplayFormat.FormatType")));
			this.dtmToDeliveryDate.DisplayFormat.Inherit = ((C1.Win.C1Input.FormatInfoInheritFlags)(resources.GetObject("dtmToDeliveryDate.DisplayFormat.Inherit")));
			this.dtmToDeliveryDate.DisplayFormat.NullText = resources.GetString("dtmToDeliveryDate.DisplayFormat.NullText");
			this.dtmToDeliveryDate.DisplayFormat.TrimEnd = ((bool)(resources.GetObject("dtmToDeliveryDate.DisplayFormat.TrimEnd")));
			this.dtmToDeliveryDate.DisplayFormat.TrimStart = ((bool)(resources.GetObject("dtmToDeliveryDate.DisplayFormat.TrimStart")));
			this.dtmToDeliveryDate.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("dtmToDeliveryDate.Dock")));
			this.dtmToDeliveryDate.DropDownFormAlign = ((C1.Win.C1Input.DropDownFormAlignmentEnum)(resources.GetObject("dtmToDeliveryDate.DropDownFormAlign")));
			this.dtmToDeliveryDate.EditFormat.CustomFormat = resources.GetString("dtmToDeliveryDate.EditFormat.CustomFormat");
			this.dtmToDeliveryDate.EditFormat.FormatType = ((C1.Win.C1Input.FormatTypeEnum)(resources.GetObject("dtmToDeliveryDate.EditFormat.FormatType")));
			this.dtmToDeliveryDate.EditFormat.Inherit = ((C1.Win.C1Input.FormatInfoInheritFlags)(resources.GetObject("dtmToDeliveryDate.EditFormat.Inherit")));
			this.dtmToDeliveryDate.EditFormat.NullText = resources.GetString("dtmToDeliveryDate.EditFormat.NullText");
			this.dtmToDeliveryDate.EditFormat.TrimEnd = ((bool)(resources.GetObject("dtmToDeliveryDate.EditFormat.TrimEnd")));
			this.dtmToDeliveryDate.EditFormat.TrimStart = ((bool)(resources.GetObject("dtmToDeliveryDate.EditFormat.TrimStart")));
			this.dtmToDeliveryDate.EditMask = resources.GetString("dtmToDeliveryDate.EditMask");
			this.dtmToDeliveryDate.EmptyAsNull = ((bool)(resources.GetObject("dtmToDeliveryDate.EmptyAsNull")));
			this.dtmToDeliveryDate.Enabled = ((bool)(resources.GetObject("dtmToDeliveryDate.Enabled")));
			this.dtmToDeliveryDate.ErrorInfo.BeepOnError = ((bool)(resources.GetObject("dtmToDeliveryDate.ErrorInfo.BeepOnError")));
			this.dtmToDeliveryDate.ErrorInfo.ErrorMessage = resources.GetString("dtmToDeliveryDate.ErrorInfo.ErrorMessage");
			this.dtmToDeliveryDate.ErrorInfo.ErrorMessageCaption = resources.GetString("dtmToDeliveryDate.ErrorInfo.ErrorMessageCaption");
			this.dtmToDeliveryDate.ErrorInfo.ShowErrorMessage = ((bool)(resources.GetObject("dtmToDeliveryDate.ErrorInfo.ShowErrorMessage")));
			this.dtmToDeliveryDate.ErrorInfo.ValueOnError = ((object)(resources.GetObject("dtmToDeliveryDate.ErrorInfo.ValueOnError")));
			this.dtmToDeliveryDate.Font = ((System.Drawing.Font)(resources.GetObject("dtmToDeliveryDate.Font")));
			this.dtmToDeliveryDate.FormatType = ((C1.Win.C1Input.FormatTypeEnum)(resources.GetObject("dtmToDeliveryDate.FormatType")));
			this.dtmToDeliveryDate.GapHeight = ((int)(resources.GetObject("dtmToDeliveryDate.GapHeight")));
			this.dtmToDeliveryDate.GMTOffset = ((System.TimeSpan)(resources.GetObject("dtmToDeliveryDate.GMTOffset")));
			this.dtmToDeliveryDate.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("dtmToDeliveryDate.ImeMode")));
			this.dtmToDeliveryDate.InitialSelection = ((C1.Win.C1Input.InitialSelectionEnum)(resources.GetObject("dtmToDeliveryDate.InitialSelection")));
			this.dtmToDeliveryDate.Location = ((System.Drawing.Point)(resources.GetObject("dtmToDeliveryDate.Location")));
			this.dtmToDeliveryDate.MaskInfo.AutoTabWhenFilled = ((bool)(resources.GetObject("dtmToDeliveryDate.MaskInfo.AutoTabWhenFilled")));
			this.dtmToDeliveryDate.MaskInfo.CaseSensitive = ((bool)(resources.GetObject("dtmToDeliveryDate.MaskInfo.CaseSensitive")));
			this.dtmToDeliveryDate.MaskInfo.CopyWithLiterals = ((bool)(resources.GetObject("dtmToDeliveryDate.MaskInfo.CopyWithLiterals")));
			this.dtmToDeliveryDate.MaskInfo.EditMask = resources.GetString("dtmToDeliveryDate.MaskInfo.EditMask");
			this.dtmToDeliveryDate.MaskInfo.EmptyAsNull = ((bool)(resources.GetObject("dtmToDeliveryDate.MaskInfo.EmptyAsNull")));
			this.dtmToDeliveryDate.MaskInfo.ErrorMessage = resources.GetString("dtmToDeliveryDate.MaskInfo.ErrorMessage");
			this.dtmToDeliveryDate.MaskInfo.Inherit = ((C1.Win.C1Input.MaskInfoInheritFlags)(resources.GetObject("dtmToDeliveryDate.MaskInfo.Inherit")));
			this.dtmToDeliveryDate.MaskInfo.PromptChar = ((char)(resources.GetObject("dtmToDeliveryDate.MaskInfo.PromptChar")));
			this.dtmToDeliveryDate.MaskInfo.ShowLiterals = ((C1.Win.C1Input.ShowLiteralsEnum)(resources.GetObject("dtmToDeliveryDate.MaskInfo.ShowLiterals")));
			this.dtmToDeliveryDate.MaskInfo.StoredEmptyChar = ((char)(resources.GetObject("dtmToDeliveryDate.MaskInfo.StoredEmptyChar")));
			this.dtmToDeliveryDate.MaxLength = ((int)(resources.GetObject("dtmToDeliveryDate.MaxLength")));
			this.dtmToDeliveryDate.Name = "dtmToDeliveryDate";
			this.dtmToDeliveryDate.NullText = resources.GetString("dtmToDeliveryDate.NullText");
			this.dtmToDeliveryDate.ParseInfo.CaseSensitive = ((bool)(resources.GetObject("dtmToDeliveryDate.ParseInfo.CaseSensitive")));
			this.dtmToDeliveryDate.ParseInfo.CustomFormat = resources.GetString("dtmToDeliveryDate.ParseInfo.CustomFormat");
			this.dtmToDeliveryDate.ParseInfo.DateTimeStyle = ((C1.Win.C1Input.DateTimeStyleFlags)(resources.GetObject("dtmToDeliveryDate.ParseInfo.DateTimeStyle")));
			this.dtmToDeliveryDate.ParseInfo.EmptyAsNull = ((bool)(resources.GetObject("dtmToDeliveryDate.ParseInfo.EmptyAsNull")));
			this.dtmToDeliveryDate.ParseInfo.ErrorMessage = resources.GetString("dtmToDeliveryDate.ParseInfo.ErrorMessage");
			this.dtmToDeliveryDate.ParseInfo.FormatType = ((C1.Win.C1Input.FormatTypeEnum)(resources.GetObject("dtmToDeliveryDate.ParseInfo.FormatType")));
			this.dtmToDeliveryDate.ParseInfo.Inherit = ((C1.Win.C1Input.ParseInfoInheritFlags)(resources.GetObject("dtmToDeliveryDate.ParseInfo.Inherit")));
			this.dtmToDeliveryDate.ParseInfo.NullText = resources.GetString("dtmToDeliveryDate.ParseInfo.NullText");
			this.dtmToDeliveryDate.ParseInfo.TrimEnd = ((bool)(resources.GetObject("dtmToDeliveryDate.ParseInfo.TrimEnd")));
			this.dtmToDeliveryDate.ParseInfo.TrimStart = ((bool)(resources.GetObject("dtmToDeliveryDate.ParseInfo.TrimStart")));
			this.dtmToDeliveryDate.PasswordChar = ((char)(resources.GetObject("dtmToDeliveryDate.PasswordChar")));
			this.dtmToDeliveryDate.PostValidation.CaseSensitive = ((bool)(resources.GetObject("dtmToDeliveryDate.PostValidation.CaseSensitive")));
			this.dtmToDeliveryDate.PostValidation.ErrorMessage = resources.GetString("dtmToDeliveryDate.PostValidation.ErrorMessage");
			this.dtmToDeliveryDate.PostValidation.Inherit = ((C1.Win.C1Input.PostValidationInheritFlags)(resources.GetObject("dtmToDeliveryDate.PostValidation.Inherit")));
			this.dtmToDeliveryDate.PostValidation.Validation = ((C1.Win.C1Input.PostValidationTypeEnum)(resources.GetObject("dtmToDeliveryDate.PostValidation.Validation")));
			this.dtmToDeliveryDate.PostValidation.Values = ((System.Array)(resources.GetObject("dtmToDeliveryDate.PostValidation.Values")));
			this.dtmToDeliveryDate.PostValidation.ValuesExcluded = ((System.Array)(resources.GetObject("dtmToDeliveryDate.PostValidation.ValuesExcluded")));
			this.dtmToDeliveryDate.PreValidation.CaseSensitive = ((bool)(resources.GetObject("dtmToDeliveryDate.PreValidation.CaseSensitive")));
			this.dtmToDeliveryDate.PreValidation.ErrorMessage = resources.GetString("dtmToDeliveryDate.PreValidation.ErrorMessage");
			this.dtmToDeliveryDate.PreValidation.Inherit = ((C1.Win.C1Input.PreValidationInheritFlags)(resources.GetObject("dtmToDeliveryDate.PreValidation.Inherit")));
			this.dtmToDeliveryDate.PreValidation.ItemSeparator = resources.GetString("dtmToDeliveryDate.PreValidation.ItemSeparator");
			this.dtmToDeliveryDate.PreValidation.PatternString = resources.GetString("dtmToDeliveryDate.PreValidation.PatternString");
			this.dtmToDeliveryDate.PreValidation.RegexOptions = ((C1.Win.C1Input.RegexOptionFlags)(resources.GetObject("dtmToDeliveryDate.PreValidation.RegexOptions")));
			this.dtmToDeliveryDate.PreValidation.TrimEnd = ((bool)(resources.GetObject("dtmToDeliveryDate.PreValidation.TrimEnd")));
			this.dtmToDeliveryDate.PreValidation.TrimStart = ((bool)(resources.GetObject("dtmToDeliveryDate.PreValidation.TrimStart")));
			this.dtmToDeliveryDate.PreValidation.Validation = ((C1.Win.C1Input.PreValidationTypeEnum)(resources.GetObject("dtmToDeliveryDate.PreValidation.Validation")));
			this.dtmToDeliveryDate.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("dtmToDeliveryDate.RightToLeft")));
			this.dtmToDeliveryDate.ShowFocusRectangle = ((bool)(resources.GetObject("dtmToDeliveryDate.ShowFocusRectangle")));
			this.dtmToDeliveryDate.Size = ((System.Drawing.Size)(resources.GetObject("dtmToDeliveryDate.Size")));
			this.dtmToDeliveryDate.TabIndex = ((int)(resources.GetObject("dtmToDeliveryDate.TabIndex")));
			this.dtmToDeliveryDate.Tag = ((object)(resources.GetObject("dtmToDeliveryDate.Tag")));
			this.dtmToDeliveryDate.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("dtmToDeliveryDate.TextAlign")));
			this.dtmToDeliveryDate.TrimEnd = ((bool)(resources.GetObject("dtmToDeliveryDate.TrimEnd")));
			this.dtmToDeliveryDate.TrimStart = ((bool)(resources.GetObject("dtmToDeliveryDate.TrimStart")));
			this.dtmToDeliveryDate.UserCultureOverride = ((bool)(resources.GetObject("dtmToDeliveryDate.UserCultureOverride")));
			this.dtmToDeliveryDate.Value = ((object)(resources.GetObject("dtmToDeliveryDate.Value")));
			this.dtmToDeliveryDate.VerticalAlign = ((C1.Win.C1Input.VerticalAlignEnum)(resources.GetObject("dtmToDeliveryDate.VerticalAlign")));
			this.dtmToDeliveryDate.Visible = ((bool)(resources.GetObject("dtmToDeliveryDate.Visible")));
			this.dtmToDeliveryDate.VisibleButtons = ((C1.Win.C1Input.DropDownControlButtonFlags)(resources.GetObject("dtmToDeliveryDate.VisibleButtons")));
			this.dtmToDeliveryDate.Validating += new System.ComponentModel.CancelEventHandler(this.dtmToPostDate_Validating);
			// 
			// dtmFromDeliveryDate
			// 
			this.dtmFromDeliveryDate.AcceptsEscape = ((bool)(resources.GetObject("dtmFromDeliveryDate.AcceptsEscape")));
			this.dtmFromDeliveryDate.AccessibleDescription = resources.GetString("dtmFromDeliveryDate.AccessibleDescription");
			this.dtmFromDeliveryDate.AccessibleName = resources.GetString("dtmFromDeliveryDate.AccessibleName");
			this.dtmFromDeliveryDate.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("dtmFromDeliveryDate.Anchor")));
			this.dtmFromDeliveryDate.AutoSize = ((bool)(resources.GetObject("dtmFromDeliveryDate.AutoSize")));
			this.dtmFromDeliveryDate.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("dtmFromDeliveryDate.BackgroundImage")));
			this.dtmFromDeliveryDate.BorderStyle = ((System.Windows.Forms.BorderStyle)(resources.GetObject("dtmFromDeliveryDate.BorderStyle")));
			// 
			// dtmFromDeliveryDate.Calendar
			// 
			this.dtmFromDeliveryDate.Calendar.AccessibleDescription = resources.GetString("dtmFromDeliveryDate.Calendar.AccessibleDescription");
			this.dtmFromDeliveryDate.Calendar.AccessibleName = resources.GetString("dtmFromDeliveryDate.Calendar.AccessibleName");
			this.dtmFromDeliveryDate.Calendar.AnnuallyBoldedDates = ((System.DateTime[])(resources.GetObject("dtmFromDeliveryDate.Calendar.AnnuallyBoldedDates")));
			this.dtmFromDeliveryDate.Calendar.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("dtmFromDeliveryDate.Calendar.BackgroundImage")));
			this.dtmFromDeliveryDate.Calendar.BoldedDates = ((System.DateTime[])(resources.GetObject("dtmFromDeliveryDate.Calendar.BoldedDates")));
			this.dtmFromDeliveryDate.Calendar.CalendarDimensions = ((System.Drawing.Size)(resources.GetObject("dtmFromDeliveryDate.Calendar.CalendarDimensions")));
			this.dtmFromDeliveryDate.Calendar.Enabled = ((bool)(resources.GetObject("dtmFromDeliveryDate.Calendar.Enabled")));
			this.dtmFromDeliveryDate.Calendar.FirstDayOfWeek = ((System.Windows.Forms.Day)(resources.GetObject("dtmFromDeliveryDate.Calendar.FirstDayOfWeek")));
			this.dtmFromDeliveryDate.Calendar.Font = ((System.Drawing.Font)(resources.GetObject("dtmFromDeliveryDate.Calendar.Font")));
			this.dtmFromDeliveryDate.Calendar.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("dtmFromDeliveryDate.Calendar.ImeMode")));
			this.dtmFromDeliveryDate.Calendar.MonthlyBoldedDates = ((System.DateTime[])(resources.GetObject("dtmFromDeliveryDate.Calendar.MonthlyBoldedDates")));
			this.dtmFromDeliveryDate.Calendar.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("dtmFromDeliveryDate.Calendar.RightToLeft")));
			this.dtmFromDeliveryDate.Calendar.ShowClearButton = ((bool)(resources.GetObject("dtmFromDeliveryDate.Calendar.ShowClearButton")));
			this.dtmFromDeliveryDate.Calendar.ShowTodayButton = ((bool)(resources.GetObject("dtmFromDeliveryDate.Calendar.ShowTodayButton")));
			this.dtmFromDeliveryDate.Calendar.ShowWeekNumbers = ((bool)(resources.GetObject("dtmFromDeliveryDate.Calendar.ShowWeekNumbers")));
			this.dtmFromDeliveryDate.CaseSensitive = ((bool)(resources.GetObject("dtmFromDeliveryDate.CaseSensitive")));
			this.dtmFromDeliveryDate.Culture = ((int)(resources.GetObject("dtmFromDeliveryDate.Culture")));
			this.dtmFromDeliveryDate.CurrentTimeZone = ((bool)(resources.GetObject("dtmFromDeliveryDate.CurrentTimeZone")));
			this.dtmFromDeliveryDate.CustomFormat = resources.GetString("dtmFromDeliveryDate.CustomFormat");
			this.dtmFromDeliveryDate.DaylightTimeAdjustment = ((C1.Win.C1Input.DaylightTimeAdjustmentEnum)(resources.GetObject("dtmFromDeliveryDate.DaylightTimeAdjustment")));
			this.dtmFromDeliveryDate.DisplayFormat.CustomFormat = resources.GetString("dtmFromDeliveryDate.DisplayFormat.CustomFormat");
			this.dtmFromDeliveryDate.DisplayFormat.FormatType = ((C1.Win.C1Input.FormatTypeEnum)(resources.GetObject("dtmFromDeliveryDate.DisplayFormat.FormatType")));
			this.dtmFromDeliveryDate.DisplayFormat.Inherit = ((C1.Win.C1Input.FormatInfoInheritFlags)(resources.GetObject("dtmFromDeliveryDate.DisplayFormat.Inherit")));
			this.dtmFromDeliveryDate.DisplayFormat.NullText = resources.GetString("dtmFromDeliveryDate.DisplayFormat.NullText");
			this.dtmFromDeliveryDate.DisplayFormat.TrimEnd = ((bool)(resources.GetObject("dtmFromDeliveryDate.DisplayFormat.TrimEnd")));
			this.dtmFromDeliveryDate.DisplayFormat.TrimStart = ((bool)(resources.GetObject("dtmFromDeliveryDate.DisplayFormat.TrimStart")));
			this.dtmFromDeliveryDate.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("dtmFromDeliveryDate.Dock")));
			this.dtmFromDeliveryDate.DropDownFormAlign = ((C1.Win.C1Input.DropDownFormAlignmentEnum)(resources.GetObject("dtmFromDeliveryDate.DropDownFormAlign")));
			this.dtmFromDeliveryDate.EditFormat.CustomFormat = resources.GetString("dtmFromDeliveryDate.EditFormat.CustomFormat");
			this.dtmFromDeliveryDate.EditFormat.FormatType = ((C1.Win.C1Input.FormatTypeEnum)(resources.GetObject("dtmFromDeliveryDate.EditFormat.FormatType")));
			this.dtmFromDeliveryDate.EditFormat.Inherit = ((C1.Win.C1Input.FormatInfoInheritFlags)(resources.GetObject("dtmFromDeliveryDate.EditFormat.Inherit")));
			this.dtmFromDeliveryDate.EditFormat.NullText = resources.GetString("dtmFromDeliveryDate.EditFormat.NullText");
			this.dtmFromDeliveryDate.EditFormat.TrimEnd = ((bool)(resources.GetObject("dtmFromDeliveryDate.EditFormat.TrimEnd")));
			this.dtmFromDeliveryDate.EditFormat.TrimStart = ((bool)(resources.GetObject("dtmFromDeliveryDate.EditFormat.TrimStart")));
			this.dtmFromDeliveryDate.EditMask = resources.GetString("dtmFromDeliveryDate.EditMask");
			this.dtmFromDeliveryDate.EmptyAsNull = ((bool)(resources.GetObject("dtmFromDeliveryDate.EmptyAsNull")));
			this.dtmFromDeliveryDate.Enabled = ((bool)(resources.GetObject("dtmFromDeliveryDate.Enabled")));
			this.dtmFromDeliveryDate.ErrorInfo.BeepOnError = ((bool)(resources.GetObject("dtmFromDeliveryDate.ErrorInfo.BeepOnError")));
			this.dtmFromDeliveryDate.ErrorInfo.ErrorMessage = resources.GetString("dtmFromDeliveryDate.ErrorInfo.ErrorMessage");
			this.dtmFromDeliveryDate.ErrorInfo.ErrorMessageCaption = resources.GetString("dtmFromDeliveryDate.ErrorInfo.ErrorMessageCaption");
			this.dtmFromDeliveryDate.ErrorInfo.ShowErrorMessage = ((bool)(resources.GetObject("dtmFromDeliveryDate.ErrorInfo.ShowErrorMessage")));
			this.dtmFromDeliveryDate.ErrorInfo.ValueOnError = ((object)(resources.GetObject("dtmFromDeliveryDate.ErrorInfo.ValueOnError")));
			this.dtmFromDeliveryDate.Font = ((System.Drawing.Font)(resources.GetObject("dtmFromDeliveryDate.Font")));
			this.dtmFromDeliveryDate.FormatType = ((C1.Win.C1Input.FormatTypeEnum)(resources.GetObject("dtmFromDeliveryDate.FormatType")));
			this.dtmFromDeliveryDate.GapHeight = ((int)(resources.GetObject("dtmFromDeliveryDate.GapHeight")));
			this.dtmFromDeliveryDate.GMTOffset = ((System.TimeSpan)(resources.GetObject("dtmFromDeliveryDate.GMTOffset")));
			this.dtmFromDeliveryDate.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("dtmFromDeliveryDate.ImeMode")));
			this.dtmFromDeliveryDate.InitialSelection = ((C1.Win.C1Input.InitialSelectionEnum)(resources.GetObject("dtmFromDeliveryDate.InitialSelection")));
			this.dtmFromDeliveryDate.Location = ((System.Drawing.Point)(resources.GetObject("dtmFromDeliveryDate.Location")));
			this.dtmFromDeliveryDate.MaskInfo.AutoTabWhenFilled = ((bool)(resources.GetObject("dtmFromDeliveryDate.MaskInfo.AutoTabWhenFilled")));
			this.dtmFromDeliveryDate.MaskInfo.CaseSensitive = ((bool)(resources.GetObject("dtmFromDeliveryDate.MaskInfo.CaseSensitive")));
			this.dtmFromDeliveryDate.MaskInfo.CopyWithLiterals = ((bool)(resources.GetObject("dtmFromDeliveryDate.MaskInfo.CopyWithLiterals")));
			this.dtmFromDeliveryDate.MaskInfo.EditMask = resources.GetString("dtmFromDeliveryDate.MaskInfo.EditMask");
			this.dtmFromDeliveryDate.MaskInfo.EmptyAsNull = ((bool)(resources.GetObject("dtmFromDeliveryDate.MaskInfo.EmptyAsNull")));
			this.dtmFromDeliveryDate.MaskInfo.ErrorMessage = resources.GetString("dtmFromDeliveryDate.MaskInfo.ErrorMessage");
			this.dtmFromDeliveryDate.MaskInfo.Inherit = ((C1.Win.C1Input.MaskInfoInheritFlags)(resources.GetObject("dtmFromDeliveryDate.MaskInfo.Inherit")));
			this.dtmFromDeliveryDate.MaskInfo.PromptChar = ((char)(resources.GetObject("dtmFromDeliveryDate.MaskInfo.PromptChar")));
			this.dtmFromDeliveryDate.MaskInfo.ShowLiterals = ((C1.Win.C1Input.ShowLiteralsEnum)(resources.GetObject("dtmFromDeliveryDate.MaskInfo.ShowLiterals")));
			this.dtmFromDeliveryDate.MaskInfo.StoredEmptyChar = ((char)(resources.GetObject("dtmFromDeliveryDate.MaskInfo.StoredEmptyChar")));
			this.dtmFromDeliveryDate.MaxLength = ((int)(resources.GetObject("dtmFromDeliveryDate.MaxLength")));
			this.dtmFromDeliveryDate.Name = "dtmFromDeliveryDate";
			this.dtmFromDeliveryDate.NullText = resources.GetString("dtmFromDeliveryDate.NullText");
			this.dtmFromDeliveryDate.ParseInfo.CaseSensitive = ((bool)(resources.GetObject("dtmFromDeliveryDate.ParseInfo.CaseSensitive")));
			this.dtmFromDeliveryDate.ParseInfo.CustomFormat = resources.GetString("dtmFromDeliveryDate.ParseInfo.CustomFormat");
			this.dtmFromDeliveryDate.ParseInfo.DateTimeStyle = ((C1.Win.C1Input.DateTimeStyleFlags)(resources.GetObject("dtmFromDeliveryDate.ParseInfo.DateTimeStyle")));
			this.dtmFromDeliveryDate.ParseInfo.EmptyAsNull = ((bool)(resources.GetObject("dtmFromDeliveryDate.ParseInfo.EmptyAsNull")));
			this.dtmFromDeliveryDate.ParseInfo.ErrorMessage = resources.GetString("dtmFromDeliveryDate.ParseInfo.ErrorMessage");
			this.dtmFromDeliveryDate.ParseInfo.FormatType = ((C1.Win.C1Input.FormatTypeEnum)(resources.GetObject("dtmFromDeliveryDate.ParseInfo.FormatType")));
			this.dtmFromDeliveryDate.ParseInfo.Inherit = ((C1.Win.C1Input.ParseInfoInheritFlags)(resources.GetObject("dtmFromDeliveryDate.ParseInfo.Inherit")));
			this.dtmFromDeliveryDate.ParseInfo.NullText = resources.GetString("dtmFromDeliveryDate.ParseInfo.NullText");
			this.dtmFromDeliveryDate.ParseInfo.TrimEnd = ((bool)(resources.GetObject("dtmFromDeliveryDate.ParseInfo.TrimEnd")));
			this.dtmFromDeliveryDate.ParseInfo.TrimStart = ((bool)(resources.GetObject("dtmFromDeliveryDate.ParseInfo.TrimStart")));
			this.dtmFromDeliveryDate.PasswordChar = ((char)(resources.GetObject("dtmFromDeliveryDate.PasswordChar")));
			this.dtmFromDeliveryDate.PostValidation.CaseSensitive = ((bool)(resources.GetObject("dtmFromDeliveryDate.PostValidation.CaseSensitive")));
			this.dtmFromDeliveryDate.PostValidation.ErrorMessage = resources.GetString("dtmFromDeliveryDate.PostValidation.ErrorMessage");
			this.dtmFromDeliveryDate.PostValidation.Inherit = ((C1.Win.C1Input.PostValidationInheritFlags)(resources.GetObject("dtmFromDeliveryDate.PostValidation.Inherit")));
			this.dtmFromDeliveryDate.PostValidation.Validation = ((C1.Win.C1Input.PostValidationTypeEnum)(resources.GetObject("dtmFromDeliveryDate.PostValidation.Validation")));
			this.dtmFromDeliveryDate.PostValidation.Values = ((System.Array)(resources.GetObject("dtmFromDeliveryDate.PostValidation.Values")));
			this.dtmFromDeliveryDate.PostValidation.ValuesExcluded = ((System.Array)(resources.GetObject("dtmFromDeliveryDate.PostValidation.ValuesExcluded")));
			this.dtmFromDeliveryDate.PreValidation.CaseSensitive = ((bool)(resources.GetObject("dtmFromDeliveryDate.PreValidation.CaseSensitive")));
			this.dtmFromDeliveryDate.PreValidation.ErrorMessage = resources.GetString("dtmFromDeliveryDate.PreValidation.ErrorMessage");
			this.dtmFromDeliveryDate.PreValidation.Inherit = ((C1.Win.C1Input.PreValidationInheritFlags)(resources.GetObject("dtmFromDeliveryDate.PreValidation.Inherit")));
			this.dtmFromDeliveryDate.PreValidation.ItemSeparator = resources.GetString("dtmFromDeliveryDate.PreValidation.ItemSeparator");
			this.dtmFromDeliveryDate.PreValidation.PatternString = resources.GetString("dtmFromDeliveryDate.PreValidation.PatternString");
			this.dtmFromDeliveryDate.PreValidation.RegexOptions = ((C1.Win.C1Input.RegexOptionFlags)(resources.GetObject("dtmFromDeliveryDate.PreValidation.RegexOptions")));
			this.dtmFromDeliveryDate.PreValidation.TrimEnd = ((bool)(resources.GetObject("dtmFromDeliveryDate.PreValidation.TrimEnd")));
			this.dtmFromDeliveryDate.PreValidation.TrimStart = ((bool)(resources.GetObject("dtmFromDeliveryDate.PreValidation.TrimStart")));
			this.dtmFromDeliveryDate.PreValidation.Validation = ((C1.Win.C1Input.PreValidationTypeEnum)(resources.GetObject("dtmFromDeliveryDate.PreValidation.Validation")));
			this.dtmFromDeliveryDate.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("dtmFromDeliveryDate.RightToLeft")));
			this.dtmFromDeliveryDate.ShowFocusRectangle = ((bool)(resources.GetObject("dtmFromDeliveryDate.ShowFocusRectangle")));
			this.dtmFromDeliveryDate.Size = ((System.Drawing.Size)(resources.GetObject("dtmFromDeliveryDate.Size")));
			this.dtmFromDeliveryDate.TabIndex = ((int)(resources.GetObject("dtmFromDeliveryDate.TabIndex")));
			this.dtmFromDeliveryDate.Tag = ((object)(resources.GetObject("dtmFromDeliveryDate.Tag")));
			this.dtmFromDeliveryDate.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("dtmFromDeliveryDate.TextAlign")));
			this.dtmFromDeliveryDate.TrimEnd = ((bool)(resources.GetObject("dtmFromDeliveryDate.TrimEnd")));
			this.dtmFromDeliveryDate.TrimStart = ((bool)(resources.GetObject("dtmFromDeliveryDate.TrimStart")));
			this.dtmFromDeliveryDate.UserCultureOverride = ((bool)(resources.GetObject("dtmFromDeliveryDate.UserCultureOverride")));
			this.dtmFromDeliveryDate.Value = ((object)(resources.GetObject("dtmFromDeliveryDate.Value")));
			this.dtmFromDeliveryDate.VerticalAlign = ((C1.Win.C1Input.VerticalAlignEnum)(resources.GetObject("dtmFromDeliveryDate.VerticalAlign")));
			this.dtmFromDeliveryDate.Visible = ((bool)(resources.GetObject("dtmFromDeliveryDate.Visible")));
			this.dtmFromDeliveryDate.VisibleButtons = ((C1.Win.C1Input.DropDownControlButtonFlags)(resources.GetObject("dtmFromDeliveryDate.VisibleButtons")));
			this.dtmFromDeliveryDate.Validating += new System.ComponentModel.CancelEventHandler(this.dtmFromPostDate_Validating);
			// 
			// lblToDeliveryDate
			// 
			this.lblToDeliveryDate.AccessibleDescription = resources.GetString("lblToDeliveryDate.AccessibleDescription");
			this.lblToDeliveryDate.AccessibleName = resources.GetString("lblToDeliveryDate.AccessibleName");
			this.lblToDeliveryDate.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblToDeliveryDate.Anchor")));
			this.lblToDeliveryDate.AutoSize = ((bool)(resources.GetObject("lblToDeliveryDate.AutoSize")));
			this.lblToDeliveryDate.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblToDeliveryDate.Dock")));
			this.lblToDeliveryDate.Enabled = ((bool)(resources.GetObject("lblToDeliveryDate.Enabled")));
			this.lblToDeliveryDate.Font = ((System.Drawing.Font)(resources.GetObject("lblToDeliveryDate.Font")));
			this.lblToDeliveryDate.Image = ((System.Drawing.Image)(resources.GetObject("lblToDeliveryDate.Image")));
			this.lblToDeliveryDate.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblToDeliveryDate.ImageAlign")));
			this.lblToDeliveryDate.ImageIndex = ((int)(resources.GetObject("lblToDeliveryDate.ImageIndex")));
			this.lblToDeliveryDate.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblToDeliveryDate.ImeMode")));
			this.lblToDeliveryDate.Location = ((System.Drawing.Point)(resources.GetObject("lblToDeliveryDate.Location")));
			this.lblToDeliveryDate.Name = "lblToDeliveryDate";
			this.lblToDeliveryDate.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblToDeliveryDate.RightToLeft")));
			this.lblToDeliveryDate.Size = ((System.Drawing.Size)(resources.GetObject("lblToDeliveryDate.Size")));
			this.lblToDeliveryDate.TabIndex = ((int)(resources.GetObject("lblToDeliveryDate.TabIndex")));
			this.lblToDeliveryDate.Text = resources.GetString("lblToDeliveryDate.Text");
			this.lblToDeliveryDate.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblToDeliveryDate.TextAlign")));
			this.lblToDeliveryDate.Visible = ((bool)(resources.GetObject("lblToDeliveryDate.Visible")));
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
			// btnSearchBeginPO
			// 
			this.btnSearchBeginPO.AccessibleDescription = resources.GetString("btnSearchBeginPO.AccessibleDescription");
			this.btnSearchBeginPO.AccessibleName = resources.GetString("btnSearchBeginPO.AccessibleName");
			this.btnSearchBeginPO.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnSearchBeginPO.Anchor")));
			this.btnSearchBeginPO.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSearchBeginPO.BackgroundImage")));
			this.btnSearchBeginPO.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnSearchBeginPO.Dock")));
			this.btnSearchBeginPO.Enabled = ((bool)(resources.GetObject("btnSearchBeginPO.Enabled")));
			this.btnSearchBeginPO.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnSearchBeginPO.FlatStyle")));
			this.btnSearchBeginPO.Font = ((System.Drawing.Font)(resources.GetObject("btnSearchBeginPO.Font")));
			this.btnSearchBeginPO.Image = ((System.Drawing.Image)(resources.GetObject("btnSearchBeginPO.Image")));
			this.btnSearchBeginPO.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnSearchBeginPO.ImageAlign")));
			this.btnSearchBeginPO.ImageIndex = ((int)(resources.GetObject("btnSearchBeginPO.ImageIndex")));
			this.btnSearchBeginPO.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnSearchBeginPO.ImeMode")));
			this.btnSearchBeginPO.Location = ((System.Drawing.Point)(resources.GetObject("btnSearchBeginPO.Location")));
			this.btnSearchBeginPO.Name = "btnSearchBeginPO";
			this.btnSearchBeginPO.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnSearchBeginPO.RightToLeft")));
			this.btnSearchBeginPO.Size = ((System.Drawing.Size)(resources.GetObject("btnSearchBeginPO.Size")));
			this.btnSearchBeginPO.TabIndex = ((int)(resources.GetObject("btnSearchBeginPO.TabIndex")));
			this.btnSearchBeginPO.Text = resources.GetString("btnSearchBeginPO.Text");
			this.btnSearchBeginPO.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnSearchBeginPO.TextAlign")));
			this.btnSearchBeginPO.Visible = ((bool)(resources.GetObject("btnSearchBeginPO.Visible")));
			this.btnSearchBeginPO.Click += new System.EventHandler(this.btnSearchBeginPO_Click);
			// 
			// txtBeginPO
			// 
			this.txtBeginPO.AccessibleDescription = resources.GetString("txtBeginPO.AccessibleDescription");
			this.txtBeginPO.AccessibleName = resources.GetString("txtBeginPO.AccessibleName");
			this.txtBeginPO.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("txtBeginPO.Anchor")));
			this.txtBeginPO.AutoSize = ((bool)(resources.GetObject("txtBeginPO.AutoSize")));
			this.txtBeginPO.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("txtBeginPO.BackgroundImage")));
			this.txtBeginPO.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("txtBeginPO.Dock")));
			this.txtBeginPO.Enabled = ((bool)(resources.GetObject("txtBeginPO.Enabled")));
			this.txtBeginPO.Font = ((System.Drawing.Font)(resources.GetObject("txtBeginPO.Font")));
			this.txtBeginPO.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("txtBeginPO.ImeMode")));
			this.txtBeginPO.Location = ((System.Drawing.Point)(resources.GetObject("txtBeginPO.Location")));
			this.txtBeginPO.MaxLength = ((int)(resources.GetObject("txtBeginPO.MaxLength")));
			this.txtBeginPO.Multiline = ((bool)(resources.GetObject("txtBeginPO.Multiline")));
			this.txtBeginPO.Name = "txtBeginPO";
			this.txtBeginPO.PasswordChar = ((char)(resources.GetObject("txtBeginPO.PasswordChar")));
			this.txtBeginPO.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("txtBeginPO.RightToLeft")));
			this.txtBeginPO.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("txtBeginPO.ScrollBars")));
			this.txtBeginPO.Size = ((System.Drawing.Size)(resources.GetObject("txtBeginPO.Size")));
			this.txtBeginPO.TabIndex = ((int)(resources.GetObject("txtBeginPO.TabIndex")));
			this.txtBeginPO.Text = resources.GetString("txtBeginPO.Text");
			this.txtBeginPO.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("txtBeginPO.TextAlign")));
			this.txtBeginPO.Visible = ((bool)(resources.GetObject("txtBeginPO.Visible")));
			this.txtBeginPO.WordWrap = ((bool)(resources.GetObject("txtBeginPO.WordWrap")));
			this.txtBeginPO.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBeginPO_KeyDown);
			this.txtBeginPO.Leave += new System.EventHandler(this.txtBeginPO_Leave);
			this.txtBeginPO.Enter += new System.EventHandler(this.OnEnterControl);
			// 
			// lblFromDeliveryDate
			// 
			this.lblFromDeliveryDate.AccessibleDescription = resources.GetString("lblFromDeliveryDate.AccessibleDescription");
			this.lblFromDeliveryDate.AccessibleName = resources.GetString("lblFromDeliveryDate.AccessibleName");
			this.lblFromDeliveryDate.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblFromDeliveryDate.Anchor")));
			this.lblFromDeliveryDate.AutoSize = ((bool)(resources.GetObject("lblFromDeliveryDate.AutoSize")));
			this.lblFromDeliveryDate.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblFromDeliveryDate.Dock")));
			this.lblFromDeliveryDate.Enabled = ((bool)(resources.GetObject("lblFromDeliveryDate.Enabled")));
			this.lblFromDeliveryDate.Font = ((System.Drawing.Font)(resources.GetObject("lblFromDeliveryDate.Font")));
			this.lblFromDeliveryDate.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblFromDeliveryDate.Image = ((System.Drawing.Image)(resources.GetObject("lblFromDeliveryDate.Image")));
			this.lblFromDeliveryDate.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblFromDeliveryDate.ImageAlign")));
			this.lblFromDeliveryDate.ImageIndex = ((int)(resources.GetObject("lblFromDeliveryDate.ImageIndex")));
			this.lblFromDeliveryDate.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblFromDeliveryDate.ImeMode")));
			this.lblFromDeliveryDate.Location = ((System.Drawing.Point)(resources.GetObject("lblFromDeliveryDate.Location")));
			this.lblFromDeliveryDate.Name = "lblFromDeliveryDate";
			this.lblFromDeliveryDate.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblFromDeliveryDate.RightToLeft")));
			this.lblFromDeliveryDate.Size = ((System.Drawing.Size)(resources.GetObject("lblFromDeliveryDate.Size")));
			this.lblFromDeliveryDate.TabIndex = ((int)(resources.GetObject("lblFromDeliveryDate.TabIndex")));
			this.lblFromDeliveryDate.Text = resources.GetString("lblFromDeliveryDate.Text");
			this.lblFromDeliveryDate.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblFromDeliveryDate.TextAlign")));
			this.lblFromDeliveryDate.Visible = ((bool)(resources.GetObject("lblFromDeliveryDate.Visible")));
			// 
			// lblPO
			// 
			this.lblPO.AccessibleDescription = resources.GetString("lblPO.AccessibleDescription");
			this.lblPO.AccessibleName = resources.GetString("lblPO.AccessibleName");
			this.lblPO.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblPO.Anchor")));
			this.lblPO.AutoSize = ((bool)(resources.GetObject("lblPO.AutoSize")));
			this.lblPO.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblPO.Dock")));
			this.lblPO.Enabled = ((bool)(resources.GetObject("lblPO.Enabled")));
			this.lblPO.Font = ((System.Drawing.Font)(resources.GetObject("lblPO.Font")));
			this.lblPO.Image = ((System.Drawing.Image)(resources.GetObject("lblPO.Image")));
			this.lblPO.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblPO.ImageAlign")));
			this.lblPO.ImageIndex = ((int)(resources.GetObject("lblPO.ImageIndex")));
			this.lblPO.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblPO.ImeMode")));
			this.lblPO.Location = ((System.Drawing.Point)(resources.GetObject("lblPO.Location")));
			this.lblPO.Name = "lblPO";
			this.lblPO.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblPO.RightToLeft")));
			this.lblPO.Size = ((System.Drawing.Size)(resources.GetObject("lblPO.Size")));
			this.lblPO.TabIndex = ((int)(resources.GetObject("lblPO.TabIndex")));
			this.lblPO.Text = resources.GetString("lblPO.Text");
			this.lblPO.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblPO.TextAlign")));
			this.lblPO.Visible = ((bool)(resources.GetObject("lblPO.Visible")));
			// 
			// dgrdData
			// 
			this.dgrdData.AccessibleDescription = resources.GetString("dgrdData.AccessibleDescription");
			this.dgrdData.AccessibleName = resources.GetString("dgrdData.AccessibleName");
			this.dgrdData.AllowAddNew = ((bool)(resources.GetObject("dgrdData.AllowAddNew")));
			this.dgrdData.AllowArrows = ((bool)(resources.GetObject("dgrdData.AllowArrows")));
			this.dgrdData.AllowColMove = ((bool)(resources.GetObject("dgrdData.AllowColMove")));
			this.dgrdData.AllowColSelect = ((bool)(resources.GetObject("dgrdData.AllowColSelect")));
			this.dgrdData.AllowDelete = ((bool)(resources.GetObject("dgrdData.AllowDelete")));
			this.dgrdData.AllowDrag = ((bool)(resources.GetObject("dgrdData.AllowDrag")));
			this.dgrdData.AllowFilter = ((bool)(resources.GetObject("dgrdData.AllowFilter")));
			this.dgrdData.AllowHorizontalSplit = ((bool)(resources.GetObject("dgrdData.AllowHorizontalSplit")));
			this.dgrdData.AllowRowSelect = ((bool)(resources.GetObject("dgrdData.AllowRowSelect")));
			this.dgrdData.AllowSort = ((bool)(resources.GetObject("dgrdData.AllowSort")));
			this.dgrdData.AllowUpdate = ((bool)(resources.GetObject("dgrdData.AllowUpdate")));
			this.dgrdData.AllowUpdateOnBlur = ((bool)(resources.GetObject("dgrdData.AllowUpdateOnBlur")));
			this.dgrdData.AllowVerticalSplit = ((bool)(resources.GetObject("dgrdData.AllowVerticalSplit")));
			this.dgrdData.AlternatingRows = ((bool)(resources.GetObject("dgrdData.AlternatingRows")));
			this.dgrdData.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("dgrdData.Anchor")));
			this.dgrdData.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("dgrdData.BackgroundImage")));
			this.dgrdData.BorderStyle = ((System.Windows.Forms.BorderStyle)(resources.GetObject("dgrdData.BorderStyle")));
			this.dgrdData.Caption = resources.GetString("dgrdData.Caption");
			this.dgrdData.CaptionHeight = ((int)(resources.GetObject("dgrdData.CaptionHeight")));
			this.dgrdData.CellTipsDelay = ((int)(resources.GetObject("dgrdData.CellTipsDelay")));
			this.dgrdData.CellTipsWidth = ((int)(resources.GetObject("dgrdData.CellTipsWidth")));
			this.dgrdData.ChildGrid = ((C1.Win.C1TrueDBGrid.C1TrueDBGrid)(resources.GetObject("dgrdData.ChildGrid")));
			this.dgrdData.CollapseColor = ((System.Drawing.Color)(resources.GetObject("dgrdData.CollapseColor")));
			this.dgrdData.ColumnFooters = ((bool)(resources.GetObject("dgrdData.ColumnFooters")));
			this.dgrdData.ColumnHeaders = ((bool)(resources.GetObject("dgrdData.ColumnHeaders")));
			this.dgrdData.DefColWidth = ((int)(resources.GetObject("dgrdData.DefColWidth")));
			this.dgrdData.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("dgrdData.Dock")));
			this.dgrdData.EditDropDown = ((bool)(resources.GetObject("dgrdData.EditDropDown")));
			this.dgrdData.EmptyRows = ((bool)(resources.GetObject("dgrdData.EmptyRows")));
			this.dgrdData.Enabled = ((bool)(resources.GetObject("dgrdData.Enabled")));
			this.dgrdData.ExpandColor = ((System.Drawing.Color)(resources.GetObject("dgrdData.ExpandColor")));
			this.dgrdData.ExposeCellMode = ((C1.Win.C1TrueDBGrid.ExposeCellModeEnum)(resources.GetObject("dgrdData.ExposeCellMode")));
			this.dgrdData.ExtendRightColumn = ((bool)(resources.GetObject("dgrdData.ExtendRightColumn")));
			this.dgrdData.FetchRowStyles = ((bool)(resources.GetObject("dgrdData.FetchRowStyles")));
			this.dgrdData.FilterBar = ((bool)(resources.GetObject("dgrdData.FilterBar")));
			this.dgrdData.FlatStyle = ((C1.Win.C1TrueDBGrid.FlatModeEnum)(resources.GetObject("dgrdData.FlatStyle")));
			this.dgrdData.Font = ((System.Drawing.Font)(resources.GetObject("dgrdData.Font")));
			this.dgrdData.GroupByAreaVisible = ((bool)(resources.GetObject("dgrdData.GroupByAreaVisible")));
			this.dgrdData.GroupByCaption = resources.GetString("dgrdData.GroupByCaption");
			this.dgrdData.Images.Add(((System.Drawing.Image)(resources.GetObject("resource"))));
			this.dgrdData.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("dgrdData.ImeMode")));
			this.dgrdData.LinesPerRow = ((int)(resources.GetObject("dgrdData.LinesPerRow")));
			this.dgrdData.Location = ((System.Drawing.Point)(resources.GetObject("dgrdData.Location")));
			this.dgrdData.MarqueeStyle = C1.Win.C1TrueDBGrid.MarqueeEnum.DottedCellBorder;
			this.dgrdData.Name = "dgrdData";
			this.dgrdData.PictureAddnewRow = ((System.Drawing.Image)(resources.GetObject("dgrdData.PictureAddnewRow")));
			this.dgrdData.PictureCurrentRow = ((System.Drawing.Image)(resources.GetObject("dgrdData.PictureCurrentRow")));
			this.dgrdData.PictureFilterBar = ((System.Drawing.Image)(resources.GetObject("dgrdData.PictureFilterBar")));
			this.dgrdData.PictureFooterRow = ((System.Drawing.Image)(resources.GetObject("dgrdData.PictureFooterRow")));
			this.dgrdData.PictureHeaderRow = ((System.Drawing.Image)(resources.GetObject("dgrdData.PictureHeaderRow")));
			this.dgrdData.PictureModifiedRow = ((System.Drawing.Image)(resources.GetObject("dgrdData.PictureModifiedRow")));
			this.dgrdData.PictureStandardRow = ((System.Drawing.Image)(resources.GetObject("dgrdData.PictureStandardRow")));
			this.dgrdData.PreviewInfo.AllowSizing = ((bool)(resources.GetObject("dgrdData.PreviewInfo.AllowSizing")));
			this.dgrdData.PreviewInfo.Caption = resources.GetString("dgrdData.PreviewInfo.Caption");
			this.dgrdData.PreviewInfo.Location = ((System.Drawing.Point)(resources.GetObject("dgrdData.PreviewInfo.Location")));
			this.dgrdData.PreviewInfo.Size = ((System.Drawing.Size)(resources.GetObject("dgrdData.PreviewInfo.Size")));
			this.dgrdData.PreviewInfo.ToolBars = ((bool)(resources.GetObject("dgrdData.PreviewInfo.ToolBars")));
			this.dgrdData.PreviewInfo.UIStrings.Content = ((string[])(resources.GetObject("dgrdData.PreviewInfo.UIStrings.Content")));
			this.dgrdData.PreviewInfo.ZoomFactor = ((System.Double)(resources.GetObject("dgrdData.PreviewInfo.ZoomFactor")));
			this.dgrdData.PrintInfo.MaxRowHeight = ((int)(resources.GetObject("dgrdData.PrintInfo.MaxRowHeight")));
			this.dgrdData.PrintInfo.OwnerDrawPageFooter = ((bool)(resources.GetObject("dgrdData.PrintInfo.OwnerDrawPageFooter")));
			this.dgrdData.PrintInfo.OwnerDrawPageHeader = ((bool)(resources.GetObject("dgrdData.PrintInfo.OwnerDrawPageHeader")));
			this.dgrdData.PrintInfo.PageFooter = resources.GetString("dgrdData.PrintInfo.PageFooter");
			this.dgrdData.PrintInfo.PageFooterHeight = ((int)(resources.GetObject("dgrdData.PrintInfo.PageFooterHeight")));
			this.dgrdData.PrintInfo.PageHeader = resources.GetString("dgrdData.PrintInfo.PageHeader");
			this.dgrdData.PrintInfo.PageHeaderHeight = ((int)(resources.GetObject("dgrdData.PrintInfo.PageHeaderHeight")));
			this.dgrdData.PrintInfo.PrintHorizontalSplits = ((bool)(resources.GetObject("dgrdData.PrintInfo.PrintHorizontalSplits")));
			this.dgrdData.PrintInfo.ProgressCaption = resources.GetString("dgrdData.PrintInfo.ProgressCaption");
			this.dgrdData.PrintInfo.RepeatColumnFooters = ((bool)(resources.GetObject("dgrdData.PrintInfo.RepeatColumnFooters")));
			this.dgrdData.PrintInfo.RepeatColumnHeaders = ((bool)(resources.GetObject("dgrdData.PrintInfo.RepeatColumnHeaders")));
			this.dgrdData.PrintInfo.RepeatGridHeader = ((bool)(resources.GetObject("dgrdData.PrintInfo.RepeatGridHeader")));
			this.dgrdData.PrintInfo.RepeatSplitHeaders = ((bool)(resources.GetObject("dgrdData.PrintInfo.RepeatSplitHeaders")));
			this.dgrdData.PrintInfo.ShowOptionsDialog = ((bool)(resources.GetObject("dgrdData.PrintInfo.ShowOptionsDialog")));
			this.dgrdData.PrintInfo.ShowProgressForm = ((bool)(resources.GetObject("dgrdData.PrintInfo.ShowProgressForm")));
			this.dgrdData.PrintInfo.ShowSelection = ((bool)(resources.GetObject("dgrdData.PrintInfo.ShowSelection")));
			this.dgrdData.PrintInfo.UseGridColors = ((bool)(resources.GetObject("dgrdData.PrintInfo.UseGridColors")));
			this.dgrdData.RecordSelectors = ((bool)(resources.GetObject("dgrdData.RecordSelectors")));
			this.dgrdData.RecordSelectorWidth = ((int)(resources.GetObject("dgrdData.RecordSelectorWidth")));
			this.dgrdData.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("dgrdData.RightToLeft")));
			this.dgrdData.RowDivider.Color = ((System.Drawing.Color)(resources.GetObject("resource.Color")));
			this.dgrdData.RowDivider.Style = ((C1.Win.C1TrueDBGrid.LineStyleEnum)(resources.GetObject("resource.Style")));
			this.dgrdData.RowHeight = ((int)(resources.GetObject("dgrdData.RowHeight")));
			this.dgrdData.RowSubDividerColor = ((System.Drawing.Color)(resources.GetObject("dgrdData.RowSubDividerColor")));
			this.dgrdData.ScrollTips = ((bool)(resources.GetObject("dgrdData.ScrollTips")));
			this.dgrdData.ScrollTrack = ((bool)(resources.GetObject("dgrdData.ScrollTrack")));
			this.dgrdData.Size = ((System.Drawing.Size)(resources.GetObject("dgrdData.Size")));
			this.dgrdData.SpringMode = ((bool)(resources.GetObject("dgrdData.SpringMode")));
			this.dgrdData.TabAcrossSplits = ((bool)(resources.GetObject("dgrdData.TabAcrossSplits")));
			this.dgrdData.TabIndex = ((int)(resources.GetObject("dgrdData.TabIndex")));
			this.dgrdData.Text = resources.GetString("dgrdData.Text");
			this.dgrdData.ViewCaptionWidth = ((int)(resources.GetObject("dgrdData.ViewCaptionWidth")));
			this.dgrdData.ViewColumnWidth = ((int)(resources.GetObject("dgrdData.ViewColumnWidth")));
			this.dgrdData.Visible = ((bool)(resources.GetObject("dgrdData.Visible")));
			this.dgrdData.WrapCellPointer = ((bool)(resources.GetObject("dgrdData.WrapCellPointer")));
			this.dgrdData.AfterColUpdate += new C1.Win.C1TrueDBGrid.ColEventHandler(this.dgrdData_AfterColUpdate);
			this.dgrdData.PropBag = resources.GetString("dgrdData.PropBag");
			// 
			// SelectPurchaseOrders
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
			this.Controls.Add(this.dgrdData);
			this.Controls.Add(this.txtBeginPO);
			this.Controls.Add(this.dtmToDeliveryDate);
			this.Controls.Add(this.dtmFromDeliveryDate);
			this.Controls.Add(this.lblToDeliveryDate);
			this.Controls.Add(this.btnSearch);
			this.Controls.Add(this.btnSearchBeginPO);
			this.Controls.Add(this.lblFromDeliveryDate);
			this.Controls.Add(this.lblPO);
			this.Controls.Add(this.btnClose);
			this.Controls.Add(this.btnHelp);
			this.Controls.Add(this.btnSelect);
			this.Controls.Add(this.chkSelectAll);
			this.Enabled = ((bool)(resources.GetObject("$this.Enabled")));
			this.Font = ((System.Drawing.Font)(resources.GetObject("$this.Font")));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("$this.ImeMode")));
			this.KeyPreview = true;
			this.Location = ((System.Drawing.Point)(resources.GetObject("$this.Location")));
			this.MaximumSize = ((System.Drawing.Size)(resources.GetObject("$this.MaximumSize")));
			this.MinimumSize = ((System.Drawing.Size)(resources.GetObject("$this.MinimumSize")));
			this.Name = "SelectPurchaseOrders";
			this.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("$this.RightToLeft")));
			this.StartPosition = ((System.Windows.Forms.FormStartPosition)(resources.GetObject("$this.StartPosition")));
			this.Text = resources.GetString("$this.Text");
			this.Closing += new System.ComponentModel.CancelEventHandler(this.SelectPurchaseOrders_Closing);
			this.Load += new System.EventHandler(this.SelectPurchaseOrders_Load);
			((System.ComponentModel.ISupportInitialize)(this.dtmToDeliveryDate)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dtmFromDeliveryDate)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dgrdData)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion
		
		#region Private Methods
		
		/// <summary>
		/// Fill related data on controls when select Payment Term
		/// </summary>
		/// <param name="pblnOpenOnly"></param>
		private void SelectPONo(string pstrMethodName, bool pblnOpenOnly)
		{
			try
			{
				Hashtable htbCriteria = new Hashtable();
				DataRowView drwResult = null;
				
				htbCriteria.Add(PO_PurchaseOrderMasterTable.CCNID_FLD, mhtbCondition[PO_PurchaseOrderMasterTable.CCNID_FLD]);
				htbCriteria.Add(PO_PurchaseOrderMasterTable.PARTYID_FLD, mhtbCondition[PO_PurchaseOrderMasterTable.PARTYID_FLD]);				
				
				//Call OpenSearchForm for selecting MPS planning cycle
				drwResult = FormControlComponents.OpenSearchForm(UNCLOSE_PO_4_INVOICE_VIEW, PO_PurchaseOrderMasterTable.TABLE_NAME + PO_PurchaseOrderMasterTable.CODE_FLD, txtBeginPO.Text, htbCriteria, pblnOpenOnly);
				
				// If has Master location matched searching condition, fill values to form's controls
				if (drwResult != null)
				{
					//Check if data was changed then reassign
					txtBeginPO.Text = drwResult[PO_PurchaseOrderMasterTable.TABLE_NAME + PO_PurchaseOrderMasterTable.CODE_FLD].ToString();
					//Reset modify status
					txtBeginPO.Modified = false;
				}
				else
				{
					if(!pblnOpenOnly)
					{				
						txtBeginPO.Focus();
					}					
				}				
			}
			catch (PCSException ex)
			{
				throw new PCSException(ex.mCode, pstrMethodName, ex);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message, ex);
			}	
		}

		/// <summary>
		/// Processign Enter event
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnEnterControl(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".OnEnterControl()";
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
		
		/// <summary>
		/// Processign Leave event
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnLeaveControl(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".OnLeaveControl()";
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
		
		#endregion Private Methods
		
		#region Event Processing

		private void SelectPurchaseOrders_Load(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".SelectPurchaseOrders_Load()";
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

				//store the gridlayout
				dtStoreGridLayout = FormControlComponents.StoreGridLayout(dgrdData);
				
				//Call the method in the BO Class to search for Work Order Line
				POInvoiceBO boInvoice = new POInvoiceBO();
				dtbData = boInvoice.SelectPO4Invoice(mhtbCondition);
				
				dgrdData.DataSource = dtbData;
				FormatDataGrid();
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
		/// Display the detail data after searching into the grid set layout for the grid
		/// </summary>
		/// <param name="dtData"></param>
		private void FormatDataGrid() 
		{
			try
			{
				//Restore the gridLayout
				FormControlComponents.RestoreGridLayout(dgrdData,dtStoreGridLayout);
				for(int i =0; i < dgrdData.Splits[0].DisplayColumns.Count; i++)
				{
					dgrdData.Splits[0].DisplayColumns[i].Locked = true;
				}

				dgrdData.Columns[PO_PurchaseOrderMasterTable.ORDERDATE_FLD].NumberFormat = Constants.DATETIME_FORMAT_HOUR;
				dgrdData.Columns[PO_DeliveryScheduleTable.SCHEDULEDATE_FLD].NumberFormat = Constants.DATETIME_FORMAT_HOUR;
				dgrdData.Columns[PO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD].NumberFormat = Constants.DECIMAL_NUMBERFORMAT;
				dgrdData.Columns[PO_PurchaseOrderDetailTable.TABLE_NAME + PO_PurchaseOrderDetailTable.UNITPRICE_FLD].NumberFormat = Constants.DECIMAL_NUMBERFORMAT;
				dgrdData.Columns[PO_PurchaseOrderDetailTable.TABLE_NAME + PO_PurchaseOrderDetailTable.VAT_FLD].NumberFormat = Constants.DECIMAL_NUMBERFORMAT;
				dgrdData.Columns[PO_PurchaseOrderDetailTable.TABLE_NAME + PO_PurchaseOrderDetailTable.SPECIALTAX_FLD].NumberFormat = Constants.DECIMAL_NUMBERFORMAT;
				dgrdData.Columns[PO_PurchaseOrderDetailTable.TABLE_NAME + PO_PurchaseOrderDetailTable.IMPORTTAX_FLD].NumberFormat = Constants.DECIMAL_NUMBERFORMAT;

				dgrdData.Splits[0].DisplayColumns[SELECTED_COL].Locked = false;				
				//set the selected to be the check box				
				dgrdData.Columns[SELECTED_COL].ValueItems.Presentation = C1.Win.C1TrueDBGrid.PresentationEnum.CheckBox;
				dgrdData.Columns[SELECTED_COL].DefaultValue = false.ToString();
				dgrdData.Columns[SELECTED_COL].ValueItems.Translate = true;				
			}
			catch (PCSException ex)
			{
				throw ex;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message, ex);
			}
		}

		public void btnSearch_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnSearch_Click()";
			const string SQL_DATE_FORMAT = "yyyy-MM-dd";
			const string END_TIME_OF_DAY = " 23:59:59";

			try 
			{
				string strFilter = string.Empty;
				bool blnHasFromPostDate = false;

				if(txtBeginPO.Text.Length > 0)
				{
					strFilter += PO_PurchaseOrderMasterTable.TABLE_NAME + PO_PurchaseOrderMasterTable.CODE_FLD;
					strFilter += " = '" + txtBeginPO.Text.Replace("'", "''") + "' ";
				}
				
				//if has From Post Date
				if(!dtmFromDeliveryDate.ValueIsDbNull && (dtmFromDeliveryDate.Text.Trim() != string.Empty))
				{
					if(strFilter.Length > 0)
					{
						strFilter += " AND " + PO_DeliveryScheduleTable.SCHEDULEDATE_FLD;
						strFilter += " >= '"  +  DateTime.Parse(dtmFromDeliveryDate.Value.ToString()).ToString(SQL_DATE_FORMAT) + "' ";
					}
					else
					{
						strFilter += PO_DeliveryScheduleTable.SCHEDULEDATE_FLD;
						strFilter += " >= '"  +  DateTime.Parse(dtmFromDeliveryDate.Value.ToString()).ToString(SQL_DATE_FORMAT) + "' ";
					}

					blnHasFromPostDate = true;
				}
				
				//if has To Post Date
				if(!dtmToDeliveryDate.ValueIsDbNull && (dtmToDeliveryDate.Text.Trim() != string.Empty))
				{
					if(blnHasFromPostDate)
					{
						DateTime dtmTmpFromDate = DateTime.Parse(((DateTime)dtmFromDeliveryDate.Value).ToString(SQL_DATE_FORMAT));
						DateTime dtmTmpToDate = DateTime.Parse(((DateTime)dtmToDeliveryDate.Value).ToString(SQL_DATE_FORMAT));
						if(dtmTmpFromDate > dtmTmpToDate)
						{							
							PCSMessageBox.Show(ErrorCode.MESSAGE_MP_PERIODDATE, MessageBoxIcon.Exclamation);
							dtmToDeliveryDate.Focus();
							return;
						}
					}

					if(strFilter.Length > 0)
					{
						strFilter += " AND " + PO_DeliveryScheduleTable.SCHEDULEDATE_FLD;
						strFilter += " <= '"  +  DateTime.Parse(dtmToDeliveryDate.Value.ToString()).ToString(SQL_DATE_FORMAT) + END_TIME_OF_DAY + "'";
					}
					else
					{
						strFilter += PO_DeliveryScheduleTable.SCHEDULEDATE_FLD;
						strFilter += " <= '"  +  DateTime.Parse(dtmToDeliveryDate.Value.ToString()).ToString(SQL_DATE_FORMAT) + END_TIME_OF_DAY + "'";
					}
				}

				//Filter data source by condition
				dtbData.DefaultView.RowFilter = strFilter;
				dgrdData.Refresh();				
			}
			catch (PCSException ex)
			{
				// displays the error message.
				PCSMessageBox.Show(ex.mCode,MessageBoxIcon.Error);
				// log message.
				try
				{
					Logger.LogMessage(ex.CauseException, METHOD_NAME, Level.ERROR);
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

		/// <summary>
		/// Search for specific WORK Order
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnSearchBeginPO_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnVendorName_Click()";
			try
			{
				SelectPONo(METHOD_NAME, true);
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
			const string METHOD_NAME = THIS + ".btnClose_Click";
			try
			{
				mSelectedRows = new Hashtable();
				this.DialogResult = DialogResult.No;
				this.Close();
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
		
		private void chkSelectAll_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".chkSelectAll_Click";
			try
			{
				
				for (int i=0 ; i < dgrdData.RowCount; i++) 
				{					
					dgrdData[i, SELECTED_COL] = chkSelectAll.Checked? 1: 0;
				}
				
				dgrdData.UpdateData();				
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

		private void dgrdData_BeforeColEdit(object sender, C1.Win.C1TrueDBGrid.BeforeColEditEventArgs e)
		{
			const string METHOD_NAME = THIS + ".dgrdData_BeforeColEdit";
			try
			{
				if (e.Column.DataColumn.DataField != SELECTED_COL)
				{
					e.Cancel = true;
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

		private void btnSelect_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnSelect_Click";			
			try
			{	
				int intIndex = 0;
				mSelectedRows = new Hashtable();
				for(int i =0; i< dtbData.Rows.Count; i++)
				{
					if(bool.Parse(dtbData.Rows[i][SELECTED_COL].ToString()))
					{
						mSelectedRows.Add(intIndex++, dtbData.Rows[i]);
					}
				}

				if(mSelectedRows.Count == 0)
				{
					PCSMessageBox.Show(ErrorCode.MESSAGE_RGV_MUST_SELECT_PURCHASE_ORDER, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return;
				}

				this.DialogResult = DialogResult.OK;
				this.Close();
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

		private void txtBeginPO_Leave(object sender, EventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtBeginPO_Leave()";
			try
			{
				OnLeaveControl(sender, e);
				//Exit immediately if BIN is empty				
				if(txtBeginPO.Text.Length == 0)
				{
					txtBeginPO.Tag = ZERO_STRING;
					return;
				}
				else if(!txtBeginPO.Modified)
				{
					return;
				}
				
				SelectPONo(METHOD_NAME, false);
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

		private void txtBeginPO_KeyDown(object sender, KeyEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtBeginPO_KeyDown()";

			try
			{
				if ((e.KeyCode == Keys.F4) && (btnSearchBeginPO.Enabled))
				{
					SelectPONo(METHOD_NAME, true);
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
		
		private void SelectPurchaseOrders_Closing(object sender, CancelEventArgs e)
		{
			try
			{
				if(this.DialogResult != DialogResult.OK)
				{
					mSelectedRows = new Hashtable();
				}
			}
			catch
			{}
		}
		
		private void dgrdData_AfterColUpdate(object sender, C1.Win.C1TrueDBGrid.ColEventArgs e)
		{
			const string METHOD_NAME = THIS + ".dgrdData_AfterColUpdate()";

			try
			{
				if(e.Column.DataColumn.DataField == SELECTED_COL)
				{
					bool blnCheckAll = true;
					for(int i =0; i < dgrdData.RowCount; i++)
					{
						blnCheckAll &= bool.Parse(dgrdData[i, SELECTED_COL].ToString());
					}
					chkSelectAll.Checked = blnCheckAll;
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
		

		private void dtmFromPostDate_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".dtmFromPostDate_Validating()";

			try
			{
				if(!dtmFromDeliveryDate.ValueIsDbNull
				&& !dtmFromDeliveryDate.Text.Trim().Equals(string.Empty)
				&& !dtmToDeliveryDate.ValueIsDbNull
				&& !dtmToDeliveryDate.Text.Trim().Equals(string.Empty)
				)
				{
					if(DateTime.Parse(dtmFromDeliveryDate.Value.ToString()) > DateTime.Parse(dtmToDeliveryDate.Value.ToString()))
					{
						PCSMessageBox.Show(ErrorCode.MESSAGE_MP_PERIODDATE, MessageBoxIcon.Exclamation);
						e.Cancel = true;
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

		private void dtmToPostDate_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".dtmToPostDate_Validating()";

			try
			{
				if(!dtmFromDeliveryDate.ValueIsDbNull
				&& !dtmFromDeliveryDate.Text.Trim().Equals(string.Empty)
				&& !dtmToDeliveryDate.ValueIsDbNull
				&& !dtmToDeliveryDate.Text.Trim().Equals(string.Empty)
				)
				{
					if(DateTime.Parse(dtmFromDeliveryDate.Value.ToString()) > DateTime.Parse(dtmToDeliveryDate.Value.ToString()))
					{
						PCSMessageBox.Show(ErrorCode.MESSAGE_MP_PERIODDATE, MessageBoxIcon.Exclamation);
						e.Cancel = true;
					}
				}
			}
			catch(Exception ex)
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
		
		#endregion Event Processing		
	}
}