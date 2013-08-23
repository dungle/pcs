using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using C1.Win.C1Input;
using C1.Win.C1List;
using C1.Win.C1TrueDBGrid;
using PCSComMaterials.Plan.BO;
using PCSComMaterials.Plan.DS;
using PCSComUtils.Common;
using PCSComUtils.Common.BO;
using PCSComUtils.PCSExc;
using PCSUtils.Log;
using PCSUtils.Utils;
using AlignHorzEnum = C1.Win.C1TrueDBGrid.AlignHorzEnum;
using BeforeColUpdateEventArgs = C1.Win.C1TrueDBGrid.BeforeColUpdateEventArgs;
using CancelEventArgs = System.ComponentModel.CancelEventArgs;
using ColEventArgs = C1.Win.C1TrueDBGrid.ColEventArgs;
using PresentationEnum = C1.Win.C1TrueDBGrid.PresentationEnum;

namespace PCSMaterials.Mrp
{
	/// <summary>
	/// Summary description for MPRCycleOption.
	/// </summary>
	public class MRPCycleOption : Form
	{
		private Label label1;
		private Button btnEdit;
		private Button btnDelete;
		private Button btnSave;
		private Button btnAdd;
		private Button btnClose;
		private Button btnHelp;
		private Button btnPrint;
		private C1TrueDBGrid dgrdData;
		private TextBox txtCycleDecription;
		private Label lblCycle;
		private Label lblGenDate;
		private Label lblCycleDesc;
		private Label lblMPSCycle;
		private Button btnCycleSearch;
		private TextBox txtCycle;
		private Button btnMPSCycleSearch;
		private TextBox txtMPSCycle;
		private DataSet dstGridData;
		private DataTable dtbGridLayOut;
		private const string MASTERLOCATIONCODE = "MasterLocationCode";
		private const string MPSCYCLEOPTION = "MPSCycleOption";
		private const string REQUEST_WO_QTITY_FLD = "requestWOQtity";
		private const string REPLENISH_PO_QTITY_FLD = "ReplenishItemQtity";
		const string THIS = "PCSMaterials.Mrp.MRPCycleOption";
		EnumAction formMode;
		UtilsBO boUtil = new UtilsBO();
		private bool blnHasError = false;
		private int pintCycleOptionMasterID = 0;
		private string strCycleText = string.Empty;
		private int CCNID;
		private C1Combo cboCCN;
		private C1DateEdit dtmFromDate;
		private Label lblToDate;
		private Label lblFromDate;
		private C1DateEdit dtmToDate;
		DateTime dtmDateOnly;
		DateTime dtmSpecialDate = new DateTime(2005, 1, 1);
		private C1NumericEdit txtPlanHorizon;
		private Label lblPlanHorizon;
		private TextBox txtMPSDescription;
		private C1DateEdit txtGenDateTime;
		private Button btnProcess;
		private GroupBox grpCheck;
		private CheckBox chkReturnToVendor;
		private C1NumericEdit txtNumbersOfDays;
		private Label lblNumbersOfDays;
		private CheckBox chkPONotReceipt;
		private Button btnUpdate;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private Container components = null;
	
		public MRPCycleOption()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
	
			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		public MRPCycleOption(int pintMRPMasterID, int pintCCNID, string pstrCycleText)
		{
			InitializeComponent();
			pintCycleOptionMasterID = pintMRPMasterID;
			CCNID = pintCCNID;
			strCycleText = pstrCycleText;
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MRPCycleOption));
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnHelp = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.dgrdData = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
            this.label1 = new System.Windows.Forms.Label();
            this.txtCycleDecription = new System.Windows.Forms.TextBox();
            this.lblCycleDesc = new System.Windows.Forms.Label();
            this.lblCycle = new System.Windows.Forms.Label();
            this.lblGenDate = new System.Windows.Forms.Label();
            this.lblToDate = new System.Windows.Forms.Label();
            this.lblFromDate = new System.Windows.Forms.Label();
            this.lblMPSCycle = new System.Windows.Forms.Label();
            this.btnCycleSearch = new System.Windows.Forms.Button();
            this.txtCycle = new System.Windows.Forms.TextBox();
            this.btnMPSCycleSearch = new System.Windows.Forms.Button();
            this.txtMPSCycle = new System.Windows.Forms.TextBox();
            this.cboCCN = new C1.Win.C1List.C1Combo();
            this.dtmFromDate = new C1.Win.C1Input.C1DateEdit();
            this.dtmToDate = new C1.Win.C1Input.C1DateEdit();
            this.txtPlanHorizon = new C1.Win.C1Input.C1NumericEdit();
            this.lblPlanHorizon = new System.Windows.Forms.Label();
            this.txtMPSDescription = new System.Windows.Forms.TextBox();
            this.txtGenDateTime = new C1.Win.C1Input.C1DateEdit();
            this.btnProcess = new System.Windows.Forms.Button();
            this.grpCheck = new System.Windows.Forms.GroupBox();
            this.lblNumbersOfDays = new System.Windows.Forms.Label();
            this.txtNumbersOfDays = new C1.Win.C1Input.C1NumericEdit();
            this.chkReturnToVendor = new System.Windows.Forms.CheckBox();
            this.chkPONotReceipt = new System.Windows.Forms.CheckBox();
            this.btnUpdate = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgrdData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboCCN)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtmFromDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtmToDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPlanHorizon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtGenDateTime)).BeginInit();
            this.grpCheck.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtNumbersOfDays)).BeginInit();
            this.SuspendLayout();
            // 
            // btnEdit
            // 
            resources.ApplyResources(this.btnEdit, "btnEdit");
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnDelete
            // 
            resources.ApplyResources(this.btnDelete, "btnDelete");
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnSave
            // 
            resources.ApplyResources(this.btnSave, "btnSave");
            this.btnSave.Name = "btnSave";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnAdd
            // 
            resources.ApplyResources(this.btnAdd, "btnAdd");
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnClose
            // 
            resources.ApplyResources(this.btnClose, "btnClose");
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Name = "btnClose";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnHelp
            // 
            resources.ApplyResources(this.btnHelp, "btnHelp");
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Click += new System.EventHandler(this.btnHelp_Click);
            // 
            // btnPrint
            // 
            resources.ApplyResources(this.btnPrint, "btnPrint");
            this.btnPrint.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // dgrdData
            // 
            resources.ApplyResources(this.dgrdData, "dgrdData");
            this.dgrdData.Images.Add(((System.Drawing.Image)(resources.GetObject("dgrdData.Images"))));
            this.dgrdData.Name = "dgrdData";
            this.dgrdData.PreviewInfo.Location = new System.Drawing.Point(0, 0);
            this.dgrdData.PreviewInfo.Size = new System.Drawing.Size(0, 0);
            this.dgrdData.PreviewInfo.ZoomFactor = 75D;
            this.dgrdData.PrintInfo.PageSettings = ((System.Drawing.Printing.PageSettings)(resources.GetObject("dgrdData.PrintInfo.PageSettings")));
            this.dgrdData.VisualStyle = C1.Win.C1TrueDBGrid.VisualStyle.Office2007Blue;
            this.dgrdData.AfterColUpdate += new C1.Win.C1TrueDBGrid.ColEventHandler(this.dgrdData_AfterColUpdate);
            this.dgrdData.BeforeColUpdate += new C1.Win.C1TrueDBGrid.BeforeColUpdateEventHandler(this.dgrdData_BeforeColUpdate);
            this.dgrdData.ButtonClick += new C1.Win.C1TrueDBGrid.ColEventHandler(this.dgrdData_ButtonClick);
            this.dgrdData.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgrdData_KeyDown);
            this.dgrdData.PropBag = resources.GetString("dgrdData.PropBag");
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.ForeColor = System.Drawing.Color.Maroon;
            this.label1.Name = "label1";
            // 
            // txtCycleDecription
            // 
            resources.ApplyResources(this.txtCycleDecription, "txtCycleDecription");
            this.txtCycleDecription.Name = "txtCycleDecription";
            this.txtCycleDecription.Enter += new System.EventHandler(this.OnEnterControl);
            this.txtCycleDecription.Leave += new System.EventHandler(this.OnLeaveControl);
            // 
            // lblCycleDesc
            // 
            resources.ApplyResources(this.lblCycleDesc, "lblCycleDesc");
            this.lblCycleDesc.Name = "lblCycleDesc";
            // 
            // lblCycle
            // 
            this.lblCycle.ForeColor = System.Drawing.Color.Maroon;
            resources.ApplyResources(this.lblCycle, "lblCycle");
            this.lblCycle.Name = "lblCycle";
            // 
            // lblGenDate
            // 
            resources.ApplyResources(this.lblGenDate, "lblGenDate");
            this.lblGenDate.Name = "lblGenDate";
            // 
            // lblToDate
            // 
            this.lblToDate.ForeColor = System.Drawing.Color.Maroon;
            resources.ApplyResources(this.lblToDate, "lblToDate");
            this.lblToDate.Name = "lblToDate";
            // 
            // lblFromDate
            // 
            this.lblFromDate.ForeColor = System.Drawing.Color.Maroon;
            resources.ApplyResources(this.lblFromDate, "lblFromDate");
            this.lblFromDate.Name = "lblFromDate";
            // 
            // lblMPSCycle
            // 
            this.lblMPSCycle.ForeColor = System.Drawing.SystemColors.WindowText;
            resources.ApplyResources(this.lblMPSCycle, "lblMPSCycle");
            this.lblMPSCycle.Name = "lblMPSCycle";
            // 
            // btnCycleSearch
            // 
            resources.ApplyResources(this.btnCycleSearch, "btnCycleSearch");
            this.btnCycleSearch.Name = "btnCycleSearch";
            this.btnCycleSearch.Click += new System.EventHandler(this.btnCycleSearch_Click);
            // 
            // txtCycle
            // 
            resources.ApplyResources(this.txtCycle, "txtCycle");
            this.txtCycle.Name = "txtCycle";
            this.txtCycle.Enter += new System.EventHandler(this.OnEnterControl);
            this.txtCycle.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCycle_KeyDown);
            this.txtCycle.Leave += new System.EventHandler(this.txtCycle_Leave);
            // 
            // btnMPSCycleSearch
            // 
            resources.ApplyResources(this.btnMPSCycleSearch, "btnMPSCycleSearch");
            this.btnMPSCycleSearch.Name = "btnMPSCycleSearch";
            this.btnMPSCycleSearch.Click += new System.EventHandler(this.btnMPSCycleSearch_Click);
            // 
            // txtMPSCycle
            // 
            resources.ApplyResources(this.txtMPSCycle, "txtMPSCycle");
            this.txtMPSCycle.Name = "txtMPSCycle";
            this.txtMPSCycle.Enter += new System.EventHandler(this.OnEnterControl);
            this.txtMPSCycle.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtMPSCycle_KeyDown);
            this.txtMPSCycle.Leave += new System.EventHandler(this.txtMPSCycle_Leave);
            // 
            // cboCCN
            // 
            this.cboCCN.AddItemSeparator = ';';
            resources.ApplyResources(this.cboCCN, "cboCCN");
            this.cboCCN.CaptionHeight = 17;
            this.cboCCN.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.cboCCN.ColumnCaptionHeight = 17;
            this.cboCCN.ColumnFooterHeight = 17;
            this.cboCCN.ContentHeight = 15;
            this.cboCCN.DeadAreaBackColor = System.Drawing.Color.Empty;
            this.cboCCN.EditorBackColor = System.Drawing.SystemColors.Window;
            this.cboCCN.EditorFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboCCN.EditorForeColor = System.Drawing.SystemColors.WindowText;
            this.cboCCN.EditorHeight = 15;
            this.cboCCN.Images.Add(((System.Drawing.Image)(resources.GetObject("cboCCN.Images"))));
            this.cboCCN.ItemHeight = 15;
            this.cboCCN.MatchEntryTimeout = ((long)(2000));
            this.cboCCN.MaxDropDownItems = ((short)(5));
            this.cboCCN.MaxLength = 32767;
            this.cboCCN.MouseCursor = System.Windows.Forms.Cursors.Default;
            this.cboCCN.Name = "cboCCN";
            this.cboCCN.RowSubDividerColor = System.Drawing.Color.DarkGray;
            this.cboCCN.VisualStyle = C1.Win.C1List.VisualStyle.Office2007Blue;
            this.cboCCN.Enter += new System.EventHandler(this.OnEnterControl);
            this.cboCCN.Leave += new System.EventHandler(this.OnLeaveControl);
            this.cboCCN.PropBag = resources.GetString("cboCCN.PropBag");
            // 
            // dtmFromDate
            // 
            this.dtmFromDate.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(177)))), ((int)(((byte)(187)))), ((int)(((byte)(214)))));
            resources.ApplyResources(this.dtmFromDate, "dtmFromDate");
            // 
            // 
            // 
            this.dtmFromDate.Calendar.Font = ((System.Drawing.Font)(resources.GetObject("dtmFromDate.Calendar.Font")));
            this.dtmFromDate.Calendar.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("dtmFromDate.Calendar.ImeMode")));
            this.dtmFromDate.Calendar.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("dtmFromDate.Calendar.RightToLeft")));
            this.dtmFromDate.Calendar.VisualStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.dtmFromDate.Calendar.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.dtmFromDate.DisplayFormat.CustomFormat = resources.GetString("dtmFromDate.DisplayFormat.CustomFormat");
            this.dtmFromDate.DisplayFormat.Inherit = ((C1.Win.C1Input.FormatInfoInheritFlags)(resources.GetObject("dtmFromDate.DisplayFormat.Inherit")));
            this.dtmFromDate.EditFormat.CustomFormat = resources.GetString("dtmFromDate.EditFormat.CustomFormat");
            this.dtmFromDate.EditFormat.Inherit = ((C1.Win.C1Input.FormatInfoInheritFlags)(resources.GetObject("dtmFromDate.EditFormat.Inherit")));
            this.dtmFromDate.MaskInfo.Inherit = ((C1.Win.C1Input.MaskInfoInheritFlags)(resources.GetObject("dtmFromDate.MaskInfo.Inherit")));
            this.dtmFromDate.Name = "dtmFromDate";
            this.dtmFromDate.ParseInfo.CustomFormat = resources.GetString("dtmFromDate.ParseInfo.CustomFormat");
            this.dtmFromDate.ParseInfo.Inherit = ((C1.Win.C1Input.ParseInfoInheritFlags)(resources.GetObject("dtmFromDate.ParseInfo.Inherit")));
            this.dtmFromDate.PostValidation.Inherit = ((C1.Win.C1Input.PostValidationInheritFlags)(resources.GetObject("dtmFromDate.PostValidation.Inherit")));
            this.dtmFromDate.PreValidation.Inherit = ((C1.Win.C1Input.PreValidationInheritFlags)(resources.GetObject("dtmFromDate.PreValidation.Inherit")));
            this.dtmFromDate.VisualStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.dtmFromDate.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.dtmFromDate.TextChanged += new System.EventHandler(this.dtmFromDate_TextChanged);
            this.dtmFromDate.Leave += new System.EventHandler(this.dtmFromDate_TextChanged);
            // 
            // dtmToDate
            // 
            this.dtmToDate.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(177)))), ((int)(((byte)(187)))), ((int)(((byte)(214)))));
            resources.ApplyResources(this.dtmToDate, "dtmToDate");
            // 
            // 
            // 
            this.dtmToDate.Calendar.Font = ((System.Drawing.Font)(resources.GetObject("dtmToDate.Calendar.Font")));
            this.dtmToDate.Calendar.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("dtmToDate.Calendar.ImeMode")));
            this.dtmToDate.Calendar.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("dtmToDate.Calendar.RightToLeft")));
            this.dtmToDate.Calendar.VisualStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.dtmToDate.Calendar.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.dtmToDate.DisplayFormat.CustomFormat = resources.GetString("dtmToDate.DisplayFormat.CustomFormat");
            this.dtmToDate.DisplayFormat.Inherit = ((C1.Win.C1Input.FormatInfoInheritFlags)(resources.GetObject("dtmToDate.DisplayFormat.Inherit")));
            this.dtmToDate.EditFormat.CustomFormat = resources.GetString("dtmToDate.EditFormat.CustomFormat");
            this.dtmToDate.EditFormat.Inherit = ((C1.Win.C1Input.FormatInfoInheritFlags)(resources.GetObject("dtmToDate.EditFormat.Inherit")));
            this.dtmToDate.MaskInfo.Inherit = ((C1.Win.C1Input.MaskInfoInheritFlags)(resources.GetObject("dtmToDate.MaskInfo.Inherit")));
            this.dtmToDate.Name = "dtmToDate";
            this.dtmToDate.ParseInfo.CustomFormat = resources.GetString("dtmToDate.ParseInfo.CustomFormat");
            this.dtmToDate.ParseInfo.Inherit = ((C1.Win.C1Input.ParseInfoInheritFlags)(resources.GetObject("dtmToDate.ParseInfo.Inherit")));
            this.dtmToDate.PostValidation.Inherit = ((C1.Win.C1Input.PostValidationInheritFlags)(resources.GetObject("dtmToDate.PostValidation.Inherit")));
            this.dtmToDate.PreValidation.Inherit = ((C1.Win.C1Input.PreValidationInheritFlags)(resources.GetObject("dtmToDate.PreValidation.Inherit")));
            this.dtmToDate.VisualStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.dtmToDate.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.dtmToDate.TextChanged += new System.EventHandler(this.dtmFromDate_TextChanged);
            this.dtmToDate.Leave += new System.EventHandler(this.dtmFromDate_TextChanged);
            // 
            // txtPlanHorizon
            // 
            resources.ApplyResources(this.txtPlanHorizon, "txtPlanHorizon");
            this.txtPlanHorizon.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(177)))), ((int)(((byte)(187)))), ((int)(((byte)(214)))));
            // 
            // 
            // 
            this.txtPlanHorizon.Calculator.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(239)))), ((int)(((byte)(255)))));
            this.txtPlanHorizon.Calculator.VisualStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.txtPlanHorizon.Calculator.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.txtPlanHorizon.DisplayFormat.Inherit = ((C1.Win.C1Input.FormatInfoInheritFlags)(resources.GetObject("txtPlanHorizon.DisplayFormat.Inherit")));
            this.txtPlanHorizon.EditFormat.Inherit = ((C1.Win.C1Input.FormatInfoInheritFlags)(resources.GetObject("txtPlanHorizon.EditFormat.Inherit")));
            this.txtPlanHorizon.MaskInfo.Inherit = ((C1.Win.C1Input.MaskInfoInheritFlags)(resources.GetObject("txtPlanHorizon.MaskInfo.Inherit")));
            this.txtPlanHorizon.Name = "txtPlanHorizon";
            this.txtPlanHorizon.ParseInfo.Inherit = ((C1.Win.C1Input.ParseInfoInheritFlags)(resources.GetObject("txtPlanHorizon.ParseInfo.Inherit")));
            this.txtPlanHorizon.PostValidation.Inherit = ((C1.Win.C1Input.PostValidationInheritFlags)(resources.GetObject("txtPlanHorizon.PostValidation.Inherit")));
            this.txtPlanHorizon.PreValidation.Inherit = ((C1.Win.C1Input.PreValidationInheritFlags)(resources.GetObject("txtPlanHorizon.PreValidation.Inherit")));
            this.txtPlanHorizon.ReadOnly = true;
            this.txtPlanHorizon.VisualStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.txtPlanHorizon.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            // 
            // lblPlanHorizon
            // 
            resources.ApplyResources(this.lblPlanHorizon, "lblPlanHorizon");
            this.lblPlanHorizon.Name = "lblPlanHorizon";
            // 
            // txtMPSDescription
            // 
            resources.ApplyResources(this.txtMPSDescription, "txtMPSDescription");
            this.txtMPSDescription.Name = "txtMPSDescription";
            // 
            // txtGenDateTime
            // 
            resources.ApplyResources(this.txtGenDateTime, "txtGenDateTime");
            this.txtGenDateTime.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(177)))), ((int)(((byte)(187)))), ((int)(((byte)(214)))));
            // 
            // 
            // 
            this.txtGenDateTime.Calendar.Font = ((System.Drawing.Font)(resources.GetObject("txtGenDateTime.Calendar.Font")));
            this.txtGenDateTime.Calendar.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("txtGenDateTime.Calendar.ImeMode")));
            this.txtGenDateTime.Calendar.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("txtGenDateTime.Calendar.RightToLeft")));
            this.txtGenDateTime.Calendar.VisualStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.txtGenDateTime.Calendar.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.txtGenDateTime.DisplayFormat.Inherit = ((C1.Win.C1Input.FormatInfoInheritFlags)(resources.GetObject("txtGenDateTime.DisplayFormat.Inherit")));
            this.txtGenDateTime.EditFormat.Inherit = ((C1.Win.C1Input.FormatInfoInheritFlags)(resources.GetObject("txtGenDateTime.EditFormat.Inherit")));
            this.txtGenDateTime.MaskInfo.Inherit = ((C1.Win.C1Input.MaskInfoInheritFlags)(resources.GetObject("txtGenDateTime.MaskInfo.Inherit")));
            this.txtGenDateTime.Name = "txtGenDateTime";
            this.txtGenDateTime.ParseInfo.Inherit = ((C1.Win.C1Input.ParseInfoInheritFlags)(resources.GetObject("txtGenDateTime.ParseInfo.Inherit")));
            this.txtGenDateTime.PostValidation.Inherit = ((C1.Win.C1Input.PostValidationInheritFlags)(resources.GetObject("txtGenDateTime.PostValidation.Inherit")));
            this.txtGenDateTime.PreValidation.Inherit = ((C1.Win.C1Input.PreValidationInheritFlags)(resources.GetObject("txtGenDateTime.PreValidation.Inherit")));
            this.txtGenDateTime.ReadOnly = true;
            this.txtGenDateTime.VisualStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.txtGenDateTime.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            // 
            // btnProcess
            // 
            resources.ApplyResources(this.btnProcess, "btnProcess");
            this.btnProcess.Name = "btnProcess";
            this.btnProcess.Click += new System.EventHandler(this.btnProcess_Click);
            // 
            // grpCheck
            // 
            resources.ApplyResources(this.grpCheck, "grpCheck");
            this.grpCheck.Controls.Add(this.lblNumbersOfDays);
            this.grpCheck.Controls.Add(this.txtNumbersOfDays);
            this.grpCheck.Controls.Add(this.chkReturnToVendor);
            this.grpCheck.Controls.Add(this.chkPONotReceipt);
            this.grpCheck.Name = "grpCheck";
            this.grpCheck.TabStop = false;
            // 
            // lblNumbersOfDays
            // 
            resources.ApplyResources(this.lblNumbersOfDays, "lblNumbersOfDays");
            this.lblNumbersOfDays.Name = "lblNumbersOfDays";
            // 
            // txtNumbersOfDays
            // 
            this.txtNumbersOfDays.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(177)))), ((int)(((byte)(187)))), ((int)(((byte)(214)))));
            resources.ApplyResources(this.txtNumbersOfDays, "txtNumbersOfDays");
            // 
            // 
            // 
            this.txtNumbersOfDays.Calculator.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(239)))), ((int)(((byte)(255)))));
            this.txtNumbersOfDays.Calculator.ButtonFlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("txtNumbersOfDays.Calculator.ButtonFlatStyle")));
            this.txtNumbersOfDays.Calculator.FormatOnClose = ((bool)(resources.GetObject("txtNumbersOfDays.Calculator.FormatOnClose")));
            this.txtNumbersOfDays.Calculator.StoredFormat = resources.GetString("txtNumbersOfDays.Calculator.StoredFormat");
            this.txtNumbersOfDays.Calculator.VisualStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.txtNumbersOfDays.Calculator.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.txtNumbersOfDays.DisplayFormat.Inherit = ((C1.Win.C1Input.FormatInfoInheritFlags)(resources.GetObject("txtNumbersOfDays.DisplayFormat.Inherit")));
            this.txtNumbersOfDays.EditFormat.Inherit = ((C1.Win.C1Input.FormatInfoInheritFlags)(resources.GetObject("txtNumbersOfDays.EditFormat.Inherit")));
            this.txtNumbersOfDays.MaskInfo.Inherit = ((C1.Win.C1Input.MaskInfoInheritFlags)(resources.GetObject("txtNumbersOfDays.MaskInfo.Inherit")));
            this.txtNumbersOfDays.Name = "txtNumbersOfDays";
            this.txtNumbersOfDays.ParseInfo.Inherit = ((C1.Win.C1Input.ParseInfoInheritFlags)(resources.GetObject("txtNumbersOfDays.ParseInfo.Inherit")));
            this.txtNumbersOfDays.PostValidation.Inherit = ((C1.Win.C1Input.PostValidationInheritFlags)(resources.GetObject("txtNumbersOfDays.PostValidation.Inherit")));
            this.txtNumbersOfDays.PreValidation.Inherit = ((C1.Win.C1Input.PreValidationInheritFlags)(resources.GetObject("txtNumbersOfDays.PreValidation.Inherit")));
            this.txtNumbersOfDays.VisualStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.txtNumbersOfDays.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            // 
            // chkReturnToVendor
            // 
            resources.ApplyResources(this.chkReturnToVendor, "chkReturnToVendor");
            this.chkReturnToVendor.Name = "chkReturnToVendor";
            // 
            // chkPONotReceipt
            // 
            resources.ApplyResources(this.chkPONotReceipt, "chkPONotReceipt");
            this.chkPONotReceipt.Name = "chkPONotReceipt";
            this.chkPONotReceipt.CheckedChanged += new System.EventHandler(this.chkPONotReceipt_CheckedChanged);
            // 
            // btnUpdate
            // 
            resources.ApplyResources(this.btnUpdate, "btnUpdate");
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // MRPCycleOption
            // 
            resources.ApplyResources(this, "$this");
            this.CancelButton = this.btnClose;
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.grpCheck);
            this.Controls.Add(this.btnProcess);
            this.Controls.Add(this.txtGenDateTime);
            this.Controls.Add(this.txtMPSDescription);
            this.Controls.Add(this.txtMPSCycle);
            this.Controls.Add(this.txtCycle);
            this.Controls.Add(this.txtCycleDecription);
            this.Controls.Add(this.dgrdData);
            this.Controls.Add(this.txtPlanHorizon);
            this.Controls.Add(this.lblPlanHorizon);
            this.Controls.Add(this.dtmToDate);
            this.Controls.Add(this.dtmFromDate);
            this.Controls.Add(this.cboCCN);
            this.Controls.Add(this.btnMPSCycleSearch);
            this.Controls.Add(this.btnCycleSearch);
            this.Controls.Add(this.lblMPSCycle);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnHelp);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblCycleDesc);
            this.Controls.Add(this.lblCycle);
            this.Controls.Add(this.lblGenDate);
            this.Controls.Add(this.lblToDate);
            this.Controls.Add(this.lblFromDate);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "MRPCycleOption";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.MRPCycleOption_Closing);
            this.Load += new System.EventHandler(this.MRPCycleOption_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MRPCycleOption_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.dgrdData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboCCN)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtmFromDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtmToDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPlanHorizon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtGenDateTime)).EndInit();
            this.grpCheck.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtNumbersOfDays)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion
		/// <summary>
		/// MRPCycleOption_Load
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Thursday, August 11 2005</date>
		private void MRPCycleOption_Load(object sender, EventArgs e)
		{
			const string METHOD_NAME = THIS + ".MPSCycleOption_Load()";
			try
			{
				//Set authorization for user
				Security objSecurity = new Security();
				this.Name = THIS;
				if(objSecurity.SetRightForUserOnForm(this, SystemProperty.UserName) == 0)
				{
					this.Close();
					// You don't have the right to view this item
					PCSMessageBox.Show(ErrorCode.MESSAGE_YOU_HAVE_NO_RIGHT_TO_VIEW ,MessageBoxIcon.Warning);
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
				//Store grid layout
				dtbGridLayOut = FormControlComponents.StoreGridLayout(dgrdData);
				//Switch form Mode
				formMode = EnumAction.Default;
				SwitchFormMode();
				//format txtPlanHorizon
				txtPlanHorizon.FormatType = FormatTypeEnum.CustomFormat;
				txtPlanHorizon.CustomFormat = Constants.INTERGER_NUMBERFORMAT;
				txtNumbersOfDays.FormatType = FormatTypeEnum.CustomFormat;
				txtNumbersOfDays.CustomFormat = Constants.INTERGER_NUMBERFORMAT;
				//format Gendate
				txtGenDateTime.FormatType = FormatTypeEnum.CustomFormat;
				txtGenDateTime.CustomFormat = Constants.DATETIME_FORMAT_HOUR;
				if (pintCycleOptionMasterID != 0)
				{
					FillDataGrid(pintCycleOptionMasterID);
					FormControlComponents.RestoreGridLayout(dgrdData, dtbGridLayOut);
					cboCCN.SelectedValue = CCNID;
					txtCycle.Tag = pintCycleOptionMasterID;
					txtCycle.Text = strCycleText;
					btnEdit.Enabled = true;			
					btnDelete.Enabled = true;
				}
				FormBorderStyle = FormBorderStyle.FixedSingle;
				MaximizeBox = false;
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
		/// <date>Thursday, August 11 2005</date>
		private void ClearForm()
		{
			const string METHOD_NAME = THIS + ".ClearForm()";
			try
			{
				txtCycle.Text = string.Empty;
				txtCycleDecription.Text = string.Empty;
				txtGenDateTime.Value = null;
				dtmToDate.Value = null;
				dtmFromDate.Value = null;
				txtMPSCycle.Text = string.Empty;
				txtPlanHorizon.Value = null;
				txtMPSDescription.Text = string.Empty;
				chkPONotReceipt.Checked = true;
				chkReturnToVendor.Checked = false;
				txtNumbersOfDays.Value = null;
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
		/// <date>Thursday, August 11 2005</date>
		private void ConfigGrid(bool pblnLock)
		{
			const string METHOD_NAME = THIS + ".ConfigGrid()";
			try
			{
				dgrdData.Enabled = true;
				for (int i =0; i <dgrdData.Splits[0].DisplayColumns.Count; i++)
				{
					dgrdData.Splits[0].DisplayColumns[i].Locked = true;
				}
				dgrdData.Splits[0].DisplayColumns[MTR_MRPCycleOptionDetailTable.ONHAND_FLD].Locked = pblnLock;
				dgrdData.Splits[0].DisplayColumns[MTR_MRPCycleOptionDetailTable.PURCHASEORDER_FLD].Locked = pblnLock;
				dgrdData.Splits[0].DisplayColumns[MTR_MRPCycleOptionDetailTable.SALEORDER_FLD].Locked = pblnLock;
				dgrdData.Splits[0].DisplayColumns[MTR_MRPCycleOptionDetailTable.DEMANDWO_FLD].Locked = pblnLock;
				dgrdData.Splits[0].DisplayColumns[MASTERLOCATIONCODE].Locked = pblnLock;
				//Set check boxes to all columns
				dgrdData.Splits[0].DisplayColumns[MTR_MRPCycleOptionDetailTable.ONHAND_FLD].DataColumn.ValueItems.Presentation  = PresentationEnum.CheckBox;				
				dgrdData.Splits[0].DisplayColumns[MTR_MRPCycleOptionDetailTable.ONHAND_FLD].Style.HorizontalAlignment = AlignHorzEnum.Center;
				dgrdData.Splits[0].DisplayColumns[MTR_MRPCycleOptionDetailTable.DEMANDWO_FLD].DataColumn.ValueItems.Presentation  = PresentationEnum.CheckBox;
				dgrdData.Splits[0].DisplayColumns[MTR_MRPCycleOptionDetailTable.DEMANDWO_FLD].Style.HorizontalAlignment = AlignHorzEnum.Center;
				dgrdData.Splits[0].DisplayColumns[MTR_MRPCycleOptionDetailTable.PURCHASEORDER_FLD].DataColumn.ValueItems.Presentation  = PresentationEnum.CheckBox;
				dgrdData.Splits[0].DisplayColumns[MTR_MRPCycleOptionDetailTable.PURCHASEORDER_FLD].Style.HorizontalAlignment = AlignHorzEnum.Center;
				dgrdData.Splits[0].DisplayColumns[MTR_MRPCycleOptionDetailTable.SALEORDER_FLD].DataColumn.ValueItems.Presentation  = PresentationEnum.CheckBox;
				dgrdData.Splits[0].DisplayColumns[MTR_MRPCycleOptionDetailTable.SALEORDER_FLD].Style.HorizontalAlignment = AlignHorzEnum.Center;
				//Setting button click for MasterLocation column
				if (!pblnLock)
				{
					dgrdData.Splits[0].DisplayColumns[MASTERLOCATIONCODE].Button = true;
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
		/// CreateDataSet
		/// </summary>
		private void CreateDataSet()
		{
			const string METHOD_NAME = THIS + ".CreateDataSet()";
			try
			{
				dstGridData = new DataSet();
				dstGridData.Tables.Add(MTR_MRPCycleOptionDetailTable.TABLE_NAME);
				//insert columns which is invisible but use to update
				dstGridData.Tables[MTR_MRPCycleOptionDetailTable.TABLE_NAME].Columns.Add(MTR_MRPCycleOptionDetailTable.MRPCYCLEOPTIONDETAILID_FLD);
				dstGridData.Tables[MTR_MRPCycleOptionDetailTable.TABLE_NAME].Columns.Add(MTR_MRPCycleOptionDetailTable.MRPCYCLEOPTIONMASTERID_FLD);
				dstGridData.Tables[MTR_MRPCycleOptionDetailTable.TABLE_NAME].Columns.Add(MTR_MRPCycleOptionDetailTable.MASTERLOCATIONID_FLD);
				//insert display columns
				dstGridData.Tables[MTR_MRPCycleOptionDetailTable.TABLE_NAME].Columns.Add(MASTERLOCATIONCODE);
				dstGridData.Tables[MTR_MRPCycleOptionDetailTable.TABLE_NAME].Columns.Add(MTR_MRPCycleOptionDetailTable.ONHAND_FLD);
				dstGridData.Tables[MTR_MRPCycleOptionDetailTable.TABLE_NAME].Columns.Add(MTR_MRPCycleOptionDetailTable.PURCHASEORDER_FLD);
				dstGridData.Tables[MTR_MRPCycleOptionDetailTable.TABLE_NAME].Columns.Add(MTR_MRPCycleOptionDetailTable.DEMANDWO_FLD);
				dstGridData.Tables[MTR_MRPCycleOptionDetailTable.TABLE_NAME].Columns.Add(MTR_MRPCycleOptionDetailTable.SALEORDER_FLD);
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
		/// <date>Thursday, August 11 2005</date>
		private void SwitchFormMode()
		{
			const string METHOD_NAME = THIS + ".SwitchFormMode()";
			try
			{
				switch (formMode)
				{
					case EnumAction.Default:
						txtCycleDecription.Enabled = false;
						txtGenDateTime.Enabled = false;
						dtmToDate.Enabled = false;
						btnDelete.Enabled = false;
						btnEdit.Enabled = false;
						btnPrint.Enabled = false;
						btnSave.Enabled = false;
						btnCycleSearch.Enabled = true;
						dtmFromDate.Enabled = false;
						cboCCN.Enabled = false;
						txtMPSCycle.Enabled = false;
						btnMPSCycleSearch.Enabled = false;
						txtMPSDescription.Enabled = false;
						chkPONotReceipt.Enabled = false;
						chkReturnToVendor.Enabled = false;
						txtNumbersOfDays.Enabled = false;
						btnUpdate.Enabled = false;
						//Lock the grid
						ConfigGrid(true);	
						break;
					case EnumAction.Add:
						txtCycleDecription.Enabled = true;
						txtGenDateTime.Enabled = true;
						dtmToDate.Enabled = true;
						txtMPSCycle.Enabled = true;
						btnMPSCycleSearch.Enabled = true;
						btnDelete.Enabled = false;
						btnEdit.Enabled = false;
						btnPrint.Enabled = true;
						btnSave.Enabled = true;
						btnCycleSearch.Enabled = false;
						dtmFromDate.Enabled = true;
						cboCCN.Enabled = true;
						chkPONotReceipt.Enabled = true;
						chkReturnToVendor.Enabled = true;
						txtNumbersOfDays.Enabled = true;
						txtMPSDescription.Enabled = true;
						ConfigGrid(false);
						dgrdData.AllowDelete = true;
						break;
					case EnumAction.Edit:
						txtCycleDecription.Enabled = true;
						txtGenDateTime.Enabled = true;
						dtmToDate.Enabled = true;
						txtMPSCycle.Enabled = true;
						btnMPSCycleSearch.Enabled = true;
						btnDelete.Enabled = false;
						btnEdit.Enabled = false;
						btnPrint.Enabled = true;
						btnSave.Enabled = true;
						btnCycleSearch.Enabled = false;
						btnAdd.Enabled = false;
						dtmFromDate.Enabled = true;
						chkPONotReceipt.Enabled = true;
						chkReturnToVendor.Enabled = true;
						txtNumbersOfDays.Enabled = true;
						cboCCN.Enabled = true;
						txtMPSDescription.Enabled = true;
						ConfigGrid(false);
						dgrdData.AllowDelete = true;
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
		/// btnAdd_Click
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Thursday, August 11 2005</date>
		private void btnAdd_Click(object sender, EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnAdd_Click()";
			try
			{
				formMode = EnumAction.Add;
				ClearForm();
				SwitchFormMode();
				txtCycle.Focus();
				// Load PostDate
				MTR_MRPCycleOptionMasterVO voMTR_MRPCycleOptionMaster = new MTR_MRPCycleOptionMasterVO();
				voMTR_MRPCycleOptionMaster.AsOfDate = boUtil.GetDBDate();
				if((DateTime.MinValue < voMTR_MRPCycleOptionMaster.AsOfDate) && (voMTR_MRPCycleOptionMaster.AsOfDate < DateTime.MaxValue))
					dtmFromDate.Value = voMTR_MRPCycleOptionMaster.AsOfDate;
				else
					dtmFromDate.Value = DBNull.Value;
				//Load To Date
				DateTime dtmTempDate = boUtil.GetDBDate();
				if((DateTime.MinValue < dtmTempDate) && (dtmTempDate < DateTime.MaxValue))
					dtmToDate.Value = dtmTempDate;
				else
					dtmToDate.Value = DBNull.Value;
				//Disable Add button
				btnAdd.Enabled = false;
				//Fill data to controls
				CreateDataSet();
				dgrdData.DataSource = dstGridData.Tables[MTR_MRPCycleOptionDetailTable.TABLE_NAME];
				FormControlComponents.RestoreGridLayout(dgrdData, dtbGridLayOut);
				ConfigGrid(false);

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
		/// IsValidateData
		/// </summary>
		/// <returns></returns>
		/// <author>Trada</author>
		/// <date>Thursday, August 11 2005</date>
		private bool IsValidateData()
		{
			const string METHOD_NAME = THIS + ".IsValidateData()";
			try
			{
				//Check mandatory fields
				if (FormControlComponents.CheckMandatory(cboCCN))
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID);
					cboCCN.Focus();
					cboCCN.Select();
					return false;
				}
				if (FormControlComponents.CheckMandatory(txtCycle))
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID);
					txtCycle.Focus();
					txtCycle.Select();
					return false;
				}
				if (FormControlComponents.CheckMandatory(dtmFromDate))
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID);
					dtmFromDate.Focus();
					dtmFromDate.Select();
					return false;
				}
				if (FormControlComponents.CheckMandatory(dtmToDate))
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID);
					dtmToDate.Focus();
					dtmToDate.Select();
					return false;
				}
				if (chkPONotReceipt.Checked)
				{
					if (FormControlComponents.CheckMandatory(txtNumbersOfDays))
					{
						PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID);
						txtNumbersOfDays.Focus();
						txtNumbersOfDays.Select();
						return false;
					}	
				}
				// HACK: Trada 23-11-2005
				//Check if From Date > To Date
				dtmDateOnly = new DateTime(((DateTime)dtmFromDate.Value).Year, ((DateTime)dtmFromDate.Value).Month,((DateTime)dtmFromDate.Value).Day) ;
				DateTime dtmToDateOnly = new DateTime(((DateTime)dtmToDate.Value).Year, ((DateTime)dtmToDate.Value).Month,((DateTime)dtmToDate.Value).Day) ;
				if (dtmToDateOnly <= dtmDateOnly)
				{
					PCSMessageBox.Show(ErrorCode.MESSAGE_MP_PERIODDATE, MessageBoxIcon.Warning);
					dtmToDate.Focus();
					return false;
				} // END: Trada 23-11-2005

				//Check if Master Location rows are NOT unique, raise error message 
				for (int i = 0; i < dgrdData.RowCount; i++)
				{
					for (int j = i + 1; j < dgrdData.RowCount; j++)
					{
						if (dgrdData[i, MTR_MRPCycleOptionDetailTable.MASTERLOCATIONID_FLD].ToString().Trim() == dgrdData[j, MTR_MRPCycleOptionDetailTable.MASTERLOCATIONID_FLD].ToString().Trim())
						{
							PCSMessageBox.Show(ErrorCode.MESSAGE_MPS_CYCLE_OPTION_DUPLICATE_MASTERLOCATION);
							dgrdData.Row = i;
							dgrdData.Col = dgrdData.Splits[0].DisplayColumns.IndexOf(dgrdData.Splits[0].DisplayColumns[MASTERLOCATIONCODE]);
							dgrdData.Focus();
							return false;
						}
					}
				}
				//check if row in grid has data
				int intCountRow =0;
				for (int i =0; i < dgrdData.RowCount; i++)
				{
					if (dgrdData[i, MASTERLOCATIONCODE].ToString() != string.Empty)
					{
						intCountRow++;
					}
				}
				if (intCountRow ==0)
				{
					PCSMessageBox.Show(ErrorCode.MESSAGE_PO_APPROVE_NO_DATA_IN_GRID);
					dgrdData.Row = 0;
					dgrdData.Col = dgrdData.Splits[0].DisplayColumns.IndexOf(dgrdData.Splits[0].DisplayColumns[MASTERLOCATIONCODE]);
					dgrdData.Focus();
					return false;
				}
				//check mandatory field in grid
				for (int i =0; i < dgrdData.RowCount; i++)
				{
					//Check MasterLocation column
					if (dgrdData[i, MASTERLOCATIONCODE].ToString() == string.Empty)
					{
						PCSMessageBox.Show(ErrorCode.MESSAGE_MATERIALRECEIPT_SELECT_MASTERLOCATION, MessageBoxIcon.Error);
						dgrdData.Row = i;
						dgrdData.Col = dgrdData.Splits[0].DisplayColumns.IndexOf(dgrdData.Splits[0].DisplayColumns[MASTERLOCATIONCODE]);
						dgrdData.Focus();
						return false;
					}
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
		/// btnSave_Click
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Thursday, August 11 2005</date>
		private void btnSave_Click(object sender, EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnSave_Click()";
			try
			{
				blnHasError = true;
				if (IsValidateData())
				{
					MRPCycleOptionBO boMRPCycleOption = new MRPCycleOptionBO();
					//Make a new MRPCycleOptionMasterVO
					MTR_MRPCycleOptionMasterVO voMTR_MRPCycleOptionMaster = new MTR_MRPCycleOptionMasterVO();
					voMTR_MRPCycleOptionMaster.CCNID = int.Parse(cboCCN.SelectedValue.ToString());
					voMTR_MRPCycleOptionMaster.AsOfDate = dtmDateOnly;
					voMTR_MRPCycleOptionMaster.Cycle = txtCycle.Text.Trim();
					voMTR_MRPCycleOptionMaster.Description = txtCycleDecription.Text.Trim();
					voMTR_MRPCycleOptionMaster.IncludedRemainPO = chkPONotReceipt.Checked;
					voMTR_MRPCycleOptionMaster.IncludedReturnToVendor = chkReturnToVendor.Checked;
					if (chkPONotReceipt.Checked)
					{
						voMTR_MRPCycleOptionMaster.DaysBeforeAsOfDate = Convert.ToInt32(txtNumbersOfDays.Value);
					}
					else
						voMTR_MRPCycleOptionMaster.DaysBeforeAsOfDate = 0;
					if ((txtMPSCycle.Tag != null)&& (txtMPSCycle.Tag.ToString() != string.Empty))
					{
						voMTR_MRPCycleOptionMaster.MPSCycleOptionMasterID = int.Parse(txtMPSCycle.Tag.ToString());
					}
					else
						voMTR_MRPCycleOptionMaster.MPSCycleOptionMasterID = 0;
					//Plan Horizon
					voMTR_MRPCycleOptionMaster.PlanHorizon = ((DateTime)dtmToDate.Value - voMTR_MRPCycleOptionMaster.AsOfDate).Days;
					voMTR_MRPCycleOptionMaster.MPSGenDate = new DateTime(2005, 1, 1);
					//Add
					if (formMode == EnumAction.Add)
					{
						//Add this new VO to MTR_MPSCycleOptionMaster Table
						pintCycleOptionMasterID = boMRPCycleOption.Add(dstGridData, voMTR_MRPCycleOptionMaster);
						voMTR_MRPCycleOptionMaster.MRPCycleOptionMasterID = pintCycleOptionMasterID;
					}
					if (formMode == EnumAction.Edit)
					{
						voMTR_MRPCycleOptionMaster.MRPCycleOptionMasterID = pintCycleOptionMasterID;
						//Update MTR_MPSCycleOptionMaster and Detail
						boMRPCycleOption.UpdateMasterAndDetail(dstGridData, voMTR_MRPCycleOptionMaster);
					}
					PCSMessageBox.Show(ErrorCode.MESSAGE_AFTER_SAVE_DATA);
					//reload grid form database
					dstGridData = boMRPCycleOption.GetDetailByMasterID(pintCycleOptionMasterID);
					dgrdData.DataSource = dstGridData.Tables[0];
					FormControlComponents.RestoreGridLayout(dgrdData, dtbGridLayOut);
					formMode = EnumAction.Default;
					txtCycle.Tag = pintCycleOptionMasterID;
					SwitchFormMode();
					dgrdData.AllowDelete = false;
					btnAdd.Enabled = true;
					btnEdit.Enabled = true;
					btnDelete.Enabled = true;	
					btnUpdate.Enabled = true;
					blnHasError = false;
				}
			}
			catch (PCSException ex)
			{
				PCSMessageBox.Show(ex.mCode, MessageBoxIcon.Error);
				if (ex.mCode == ErrorCode.DUPLICATE_KEY)
				{
					txtCycle.Focus();
					txtCycle.Select();
				}
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
		/// btnEdit_Click
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Friday, August 12 2005</date>
		private void btnEdit_Click(object sender, EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnEdit_Click()";
			try
			{
				//Switch form Mode
				formMode = EnumAction.Edit;
				SwitchFormMode();
				//set some check box
				if (!chkPONotReceipt.Checked)
				{
					chkReturnToVendor.Checked = false;
					chkReturnToVendor.Enabled = false;
					txtNumbersOfDays.Value = null;
					txtNumbersOfDays.Enabled = false;
					lblNumbersOfDays.ForeColor = Color.Black;
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
		/// btnDelete_Click
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Friday, August 12 2005</date>
		private void btnDelete_Click(object sender, EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnDelete_Click()";
			try
			{
				//Delete Detail and Master
				if (PCSMessageBox.Show(ErrorCode.MESSAGE_DELETE_RECORD, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
				{
					// delete result only
					MRPCycleOptionBO boMRPCycleOption = new MRPCycleOptionBO();
					boMRPCycleOption.DeleteMRPResult(int.Parse(cboCCN.SelectedValue.ToString()), pintCycleOptionMasterID);
					PCSMessageBox.Show(ErrorCode.MESSAGE_AFTER_SAVE_DATA);
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

		private void btnPrint_Click(object sender, EventArgs e)
		{
		
		}

		private void btnHelp_Click(object sender, EventArgs e)
		{
		
		}
		/// <summary>
		/// btnClose_Click
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Thursday, August 11 2005</date>
		private void btnClose_Click(object sender, EventArgs e)
		{
			this.Close();
		}
		/// <summary>
		/// btnMPSCycleSearch_Click
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Thursday, August 11 2005</date>
		private void btnMPSCycleSearch_Click(object sender, EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnCycleSearch_Click()";
			try 
			{
				Hashtable htbCriteria = new Hashtable();
				DataRowView drwResult = null;
				//User has selected CCN
				if (cboCCN.SelectedIndex != -1)
				{
					htbCriteria.Add(MTR_MPSCycleOptionMasterTable.CCNID_FLD, cboCCN.SelectedValue);	
				}
				else //User has not selected CCN
				{
					htbCriteria.Add(MTR_MPSCycleOptionMasterTable.CCNID_FLD, 0);
				}
				drwResult = FormControlComponents.OpenSearchForm(MTR_MPSCycleOptionMasterTable.TABLE_NAME, MTR_MPSCycleOptionMasterTable.CYCLE_FLD, txtMPSCycle.Text.Trim(), htbCriteria, true);
				if (drwResult != null)
				{
					txtMPSCycle.Text = drwResult[MTR_MPSCycleOptionMasterTable.CYCLE_FLD].ToString();
					txtMPSDescription.Text = drwResult[MTR_MPSCycleOptionMasterTable.DESCRIPTION_FLD].ToString();
					//Keep valua of CycleOptionMasterID 
					txtMPSCycle.Tag = drwResult[MTR_MPSCycleOptionMasterTable.MPSCYCLEOPTIONMASTERID_FLD];
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
		/// MRPCycleOption_KeyDown
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Thursday, August 11 2005</date>
		private void MRPCycleOption_KeyDown(object sender, KeyEventArgs e)
		{
			const string METHOD_NAME = THIS + ".MRPCycleOption_KeyDown()";
			try
			{
				#region HACK: DEL Trada 10-21-2005

//				if (e.KeyCode == Keys.Escape)
//				{
//					btnClose_Click(sender, e);
//				}

				#endregion END: DEL Trada 10-21-2005

				if (e.KeyCode == Keys.F12)
				{
					if((formMode == EnumAction.Edit) || (formMode == EnumAction.Add)) 
					{
						DataRow drowNew = dstGridData.Tables[MTR_MRPCycleOptionDetailTable.TABLE_NAME].NewRow();
						dstGridData.Tables[MTR_MRPCycleOptionDetailTable.TABLE_NAME].Rows.Add(drowNew);
						dgrdData.Row = dgrdData.RowCount;
						dgrdData.Col = dgrdData.Columns.IndexOf(dgrdData.Columns[MASTERLOCATIONCODE]);
						dgrdData.Focus();
					}
					dgrdData.EditActive = false;
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
		/// dgrdData_ButtonClick
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Thursday, August 11 2005</date>
		private void dgrdData_ButtonClick(object sender, ColEventArgs e)
		{
			const string METHOD_NAME = THIS + ".dgrdData_ButtonClick()";
			try
			{
				Hashtable htbCriteria = new Hashtable();
				DataRowView drwResult = null;
				if (!btnSave.Enabled) return;
				//open the search form to select Master Location
				if (dgrdData.Col == dgrdData.Columns.IndexOf(dgrdData.Columns[MASTERLOCATIONCODE]))
				{
					if (cboCCN.SelectedIndex != -1)
					{
						htbCriteria.Add(MST_MasterLocationTable.CCNID_FLD, cboCCN.SelectedValue);	
					}
					else //User has not selected CCN
					{
						htbCriteria.Add(MST_MasterLocationTable.CCNID_FLD, 0);
					}
					drwResult = FormControlComponents.OpenSearchForm(MST_MasterLocationTable.TABLE_NAME, MST_MasterLocationTable.CODE_FLD, dgrdData[dgrdData.Row, MASTERLOCATIONCODE].ToString(), htbCriteria, true);
					if (drwResult != null)
					{
						dgrdData.EditActive = true;
						//dgrdData[pintRow, PRO_ComponentScrapDetailTable.LINE_FLD] = pintRow + 1;
						dgrdData[dgrdData.Row, MASTERLOCATIONCODE] = drwResult[MST_MasterLocationTable.CODE_FLD].ToString();
						if (dgrdData.Columns[MTR_MPSCycleOptionDetailTable.MASTERLOCATIONID_FLD].Text == string.Empty)
						{
							//Set false value for all check boxes
							dgrdData[dgrdData.Row, MTR_MRPCycleOptionDetailTable.ONHAND_FLD] = false;
							dgrdData[dgrdData.Row, MTR_MRPCycleOptionDetailTable.PURCHASEORDER_FLD] = false;
							dgrdData[dgrdData.Row, MTR_MRPCycleOptionDetailTable.SALEORDER_FLD] = false;
							dgrdData[dgrdData.Row, MTR_MRPCycleOptionDetailTable.DEMANDWO_FLD] = false;

						}
						dgrdData[dgrdData.Row, MTR_MPSCycleOptionDetailTable.MASTERLOCATIONID_FLD] = drwResult[MST_MasterLocationTable.MASTERLOCATIONID_FLD];
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
		/// dgrdData_BeforeColUpdate
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Thursday, August 11 2005</date>
		private void dgrdData_BeforeColUpdate(object sender, BeforeColUpdateEventArgs e)
		{
			const string METHOD_NAME = THIS + ".dgrdData_BeforeColUpdate()";
			try
			{
				Hashtable htbCriteria = new Hashtable();
				DataRowView drwResult = null;
				if (e.Column.DataColumn.DataField == MASTERLOCATIONCODE)
				{
					if (dgrdData.Columns[e.Column.DataColumn.DataField].Text.Trim() != string.Empty)
					{
						if (cboCCN.SelectedIndex != -1)
						{
							htbCriteria.Add(MST_MasterLocationTable.CCNID_FLD, cboCCN.SelectedValue);	
						}
						else //User has not selected CCN
						{
							htbCriteria.Add(MST_MasterLocationTable.CCNID_FLD, 0);
						}
						drwResult = FormControlComponents.OpenSearchForm(MST_MasterLocationTable.TABLE_NAME, MST_MasterLocationTable.CODE_FLD, dgrdData.Columns[e.Column.DataColumn.DataField].Text.Trim(), htbCriteria, false);
						if (drwResult != null)
						{
							e.Column.DataColumn.Tag = drwResult.Row;	
						}	
						else
						{
							e.Cancel = true;
						}
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
		/// dgrdData_AfterColUpdate
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Thursday, August 11 2005</date>
		private void dgrdData_AfterColUpdate(object sender, ColEventArgs e)
		{
			const string METHOD_NAME = THIS + ".dgrdData_AfterColUpdate()";
			try
			{
				DataRow drowResult = (DataRow) e.Column.DataColumn.Tag;
				//Fill Data to Master Location Column
				if (e.Column.DataColumn.DataField == MASTERLOCATIONCODE)
				{
					if ((e.Column.DataColumn.Tag == null) ||(dgrdData[dgrdData.Row, MASTERLOCATIONCODE].ToString() == string.Empty))
					{
						int row = dgrdData.Row;
						dgrdData[row, MTR_MPSCycleOptionDetailTable.MASTERLOCATIONID_FLD] = null;
						dgrdData[dgrdData.Row, MASTERLOCATIONCODE] = string.Empty;
					}
					else
					{
						dgrdData.EditActive = true;
						dgrdData[dgrdData.Row, MASTERLOCATIONCODE] = drowResult[MST_MasterLocationTable.CODE_FLD].ToString();
						if (dgrdData.Columns[MTR_MPSCycleOptionDetailTable.MASTERLOCATIONID_FLD].Text == string.Empty)
						{
							dgrdData[dgrdData.Row, MTR_MPSCycleOptionDetailTable.DEMANDWO_FLD] = false;
							dgrdData[dgrdData.Row, MTR_MPSCycleOptionDetailTable.ONHAND_FLD] = false;
							dgrdData[dgrdData.Row, MTR_MPSCycleOptionDetailTable.PURCHASEORDER_FLD] = false;
							dgrdData[dgrdData.Row, MTR_MPSCycleOptionDetailTable.SALEORDER_FLD] = false;
						}
						dgrdData[dgrdData.Row, MTR_MPSCycleOptionDetailTable.MASTERLOCATIONID_FLD] = drowResult[MST_MasterLocationTable.MASTERLOCATIONID_FLD];
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
		/// <summary>
		/// dgrdData_KeyDown
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Thursday, August 11 2005</date>
		private void dgrdData_KeyDown(object sender, KeyEventArgs e)
		{
			const string METHOD_NAME = THIS + ".dgrdData_KeyDown()";
			try
			{
				switch (e.KeyCode)
				{
					case Keys.F4:
						if (btnSave.Enabled)
						{
							dgrdData_ButtonClick(sender, null);
						}
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
		/// txtMPSCycle_Leave
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Thursday, August 11 2005</date>
		private void txtMPSCycle_Leave(object sender, EventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtMPSCycle_Leave()";
			try 
			{
				OnLeaveControl(sender, e);
				Hashtable htbCriteria = new Hashtable();
				DataRowView drwResult = null;
				if (!txtMPSCycle.Modified) return;
				//User has enter MasLoc
				if (cboCCN.SelectedIndex != -1)
				{
					htbCriteria.Add(MTR_MPSCycleOptionMasterTable.CCNID_FLD, cboCCN.SelectedValue);	
				}
				else //User has not selected CCN
				{
					htbCriteria.Add(MTR_MPSCycleOptionMasterTable.CCNID_FLD, 0);
				}
				if (txtMPSCycle.Text != string.Empty)
				{
					drwResult = FormControlComponents.OpenSearchForm(MTR_MPSCycleOptionMasterTable.TABLE_NAME, MTR_MPSCycleOptionMasterTable.CYCLE_FLD, txtMPSCycle.Text.Trim(), htbCriteria, false);
					if (drwResult != null)
					{
						txtMPSCycle.Text = drwResult[MTR_MPSCycleOptionMasterTable.CYCLE_FLD].ToString();
						//Keep valua of CycleOptionMasterID 
						txtMPSCycle.Tag = drwResult[MTR_MPSCycleOptionMasterTable.MPSCYCLEOPTIONMASTERID_FLD];
					}
					else
						txtCycle.Focus();
				}
				else
					txtMPSCycle.Tag = null;
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
		/// txtMPSCycle_KeyDown
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Thursday, August 11 2005</date>
		private void txtMPSCycle_KeyDown(object sender, KeyEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtMPSCycle_KeyDown()";
			if (e.KeyCode == Keys.F4)
			{
				btnMPSCycleSearch_Click(sender, e);	
			}
		}
		/// <summary>
		/// FillDataGrid
		/// </summary>
		/// <param name="pintCycleOptionMasterID"></param>
		/// <author>Trada</author>
		/// <date>Friday, August 12 2005</date>
		private void FillDataGrid(int pintCycleOptionMasterID)
		{
			const string METHOD_NAME = THIS + ".FillDataGrid()";
			try
			{
				int intPlanHorizon = 0;
				MRPCycleOptionBO boMRPCycleOption = new MRPCycleOptionBO();
				//Get data from CycleOptionMaster
				DataTable dtbMRPCycleOptionMaster = boMRPCycleOption.GetCycleOptionMaster(pintCycleOptionMasterID);
				//Fill Data to all controls
				if (dtbMRPCycleOptionMaster.Rows.Count > 0)
				{
					txtCycleDecription.Text = dtbMRPCycleOptionMaster.Rows[0][MTR_MRPCycleOptionMasterTable.DESCRIPTION_FLD].ToString();
					if (dtbMRPCycleOptionMaster.Rows[0][MTR_MRPCycleOptionMasterTable.INCLUDEDREMAINPO_FLD] != DBNull.Value)
					{
						chkPONotReceipt.Checked = Convert.ToBoolean(dtbMRPCycleOptionMaster.Rows[0][MTR_MRPCycleOptionMasterTable.INCLUDEDREMAINPO_FLD]);
					}
					if (chkPONotReceipt.Checked) lblNumbersOfDays.ForeColor = Color.Maroon;
					else lblNumbersOfDays.ForeColor = Color.Black;
					if (dtbMRPCycleOptionMaster.Rows[0][MTR_MRPCycleOptionMasterTable.INCLUDEDRETURNTOVENDOR_FLD] != DBNull.Value)
					{
						chkReturnToVendor.Checked = Convert.ToBoolean(dtbMRPCycleOptionMaster.Rows[0][MTR_MRPCycleOptionMasterTable.INCLUDEDRETURNTOVENDOR_FLD]);
					}
					if (dtbMRPCycleOptionMaster.Rows[0][MTR_MRPCycleOptionMasterTable.DAYBEFOREASOFDATE_FLD] != DBNull.Value)
					{
						txtNumbersOfDays.Value = Convert.ToInt32(dtbMRPCycleOptionMaster.Rows[0][MTR_MRPCycleOptionMasterTable.DAYBEFOREASOFDATE_FLD]);
					}
					if ((dtbMRPCycleOptionMaster.Rows[0][MTR_MRPCycleOptionMasterTable.MPSGENDATE_FLD].ToString() != string.Empty) 
						&& ((DateTime)dtbMRPCycleOptionMaster.Rows[0][MTR_MRPCycleOptionMasterTable.MPSGENDATE_FLD] != dtmSpecialDate))
					{
						txtGenDateTime.Value = dtbMRPCycleOptionMaster.Rows[0][MTR_MRPCycleOptionMasterTable.MPSGENDATE_FLD];
					}
					else txtGenDateTime.Value = null;
					txtMPSCycle.Text = dtbMRPCycleOptionMaster.Rows[0][MPSCYCLEOPTION].ToString();
					if ((dtbMRPCycleOptionMaster.Rows[0][MTR_MRPCycleOptionMasterTable.PLANHORIZON_FLD] != null)
						&& (dtbMRPCycleOptionMaster.Rows[0][MTR_MRPCycleOptionMasterTable.PLANHORIZON_FLD] != DBNull.Value))
					{
						intPlanHorizon = int.Parse(dtbMRPCycleOptionMaster.Rows[0][MTR_MRPCycleOptionMasterTable.PLANHORIZON_FLD].ToString());
					}
					else
						intPlanHorizon = 0;
				
					dtmFromDate.Value = dtbMRPCycleOptionMaster.Rows[0][MTR_MRPCycleOptionMasterTable.ASOFDATE_FLD];
					dtmToDate.Value = ((DateTime)dtmFromDate.Value).AddDays(intPlanHorizon);
					txtMPSCycle.Tag = dtbMRPCycleOptionMaster.Rows[0][MTR_MRPCycleOptionMasterTable.MPSCYCLEOPTIONMASTERID_FLD];
				}
				//Get data from CycleOptionDetail Table by CycleOptionMasterID
				dstGridData = boMRPCycleOption.GetDetailByMasterID(pintCycleOptionMasterID);
				dgrdData.DataSource = dstGridData.Tables[0];
				//Lock grid
				for (int i =0; i <dgrdData.Splits[0].DisplayColumns.Count; i++)
				{
					dgrdData.Splits[0].DisplayColumns[i].Locked = true;
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
		/// btnCycleSearch_Click
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Friday, August 12 2005</date>
		private void btnCycleSearch_Click(object sender, EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnCycleSearch_Click()";
			try 
			{
				Hashtable htbCriteria = new Hashtable();
				DataRowView drwResult = null;
				//User has selected CCN
				if (cboCCN.SelectedIndex != -1)
				{
					htbCriteria.Add(MTR_MRPCycleOptionMasterTable.CCNID_FLD, cboCCN.SelectedValue);	
				}
				else //User has not selected CCN
				{
					htbCriteria.Add(MTR_MRPCycleOptionMasterTable.CCNID_FLD, 0);
				}
				drwResult = FormControlComponents.OpenSearchForm(MTR_MRPCycleOptionMasterTable.TABLE_NAME, MTR_MRPCycleOptionMasterTable.CYCLE_FLD, txtCycle.Text.Trim(), htbCriteria, true);
				if (drwResult != null)
				{
					txtCycle.Text = drwResult[MTR_MRPCycleOptionMasterTable.CYCLE_FLD].ToString();
					//Keep valua of CycleOptionMasterID 
					txtCycle.Tag = drwResult[MTR_MRPCycleOptionMasterTable.MRPCYCLEOPTIONMASTERID_FLD];
					pintCycleOptionMasterID = int.Parse(txtCycle.Tag.ToString());
				}
				else
				{
					txtCycle.Focus();
					return;
				}
				FillDataGrid(pintCycleOptionMasterID);
				chkReturnToVendor.Enabled = false;
				txtNumbersOfDays.Enabled = false;
				FormControlComponents.RestoreGridLayout(dgrdData, dtbGridLayOut);
				btnEdit.Enabled = true;			
				btnDelete.Enabled = true;
				btnUpdate.Enabled = true;
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
		/// txtCycle_KeyDown
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Friday, August 12 2005</date>
		private void txtCycle_KeyDown(object sender, KeyEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtCycle_KeyDown()";
			if (!btnCycleSearch.Enabled)
			{
				return;
			}
			if (e.KeyCode == Keys.F4)
			{
				btnCycleSearch_Click(sender, e);	
			}
		}
		/// <summary>
		/// txtCycle_Leave
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Friday, August 12 2005</date>
		private void txtCycle_Leave(object sender, EventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtCycle_Leave()";
			try 
			{
				OnLeaveControl(sender, e);
				if (!txtCycle.Modified) return;
				if (btnCycleSearch.Enabled)
				{
					if (txtCycle.Text == string.Empty)
					{
						ClearForm();
						txtCycle.Tag = null;
						CreateDataSet();
						dgrdData.DataSource = dstGridData.Tables[MTR_MRPCycleOptionDetailTable.TABLE_NAME];
						FormControlComponents.RestoreGridLayout(dgrdData, dtbGridLayOut);
						ConfigGrid(false);
						btnUpdate.Enabled = false;
						return;
					}
					Hashtable htbCriteria = new Hashtable();
					DataRowView drwResult = null;
					//User has enter MasLoc
					if (cboCCN.SelectedIndex != -1)
					{
						htbCriteria.Add(MTR_MRPCycleOptionMasterTable.CCNID_FLD, cboCCN.SelectedValue);	
					}
					else //User has not selected CCN
					{
						htbCriteria.Add(MTR_MRPCycleOptionMasterTable.CCNID_FLD, 0);
					}
					if (txtCycle.Text != string.Empty)
					{
						drwResult = FormControlComponents.OpenSearchForm(MTR_MRPCycleOptionMasterTable.TABLE_NAME, MTR_MRPCycleOptionMasterTable.CYCLE_FLD, txtCycle.Text.Trim(), htbCriteria, false);
						if (drwResult != null)
						{
							txtCycle.Text = drwResult[MTR_MRPCycleOptionMasterTable.CYCLE_FLD].ToString();
							//Keep valua of CycleOptionMasterID 
							txtCycle.Tag = drwResult[MTR_MRPCycleOptionMasterTable.MRPCYCLEOPTIONMASTERID_FLD];
							pintCycleOptionMasterID = int.Parse(txtCycle.Tag.ToString());
						}
						else
						{
							txtCycle.Focus();
							return;
						}
						FillDataGrid(pintCycleOptionMasterID);
						FormControlComponents.RestoreGridLayout(dgrdData, dtbGridLayOut);
						btnEdit.Enabled = true;			
						btnDelete.Enabled = true;
						btnUpdate.Enabled = true;
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
		/// OnEnterControl
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Friday, August 12 2005</date>
		private void OnEnterControl(object sender, EventArgs e)
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
		private void OnLeaveControl(object sender, EventArgs e)
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
		/// <summary>
		/// MRPCycleOption_Closing
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Friday, August 12 2005</date>
		private void MRPCycleOption_Closing(object sender, CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".MRPCycleOption_Closing()";
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
		/// dtmFromDate_TextChanged
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Thursday, Nov 24 2005</date>
		private void dtmFromDate_TextChanged(object sender, EventArgs e)
		{
			const string METHOD_NAME = THIS + ".dtmFromDate_TextChanged()";
			try
			{
				
				if ((dtmFromDate.Value != null)&&(dtmToDate.Value != null)
					&&(dtmFromDate.Value != DBNull.Value)&&(dtmToDate.Value != DBNull.Value))
				{
					dtmDateOnly = new DateTime(((DateTime)dtmFromDate.Value).Year, ((DateTime)dtmFromDate.Value).Month,((DateTime)dtmFromDate.Value).Day) ;
					txtPlanHorizon.Value = ((DateTime)dtmToDate.Value - dtmDateOnly).Days;
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
		/// btnProcess_Click
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Tuesday, Feb 21 2006</date>
		private void btnProcess_Click(object sender, EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnProcess_Click()";
			try
			{
				if ((txtCycle.Tag != null) && (txtCycle.Tag != DBNull.Value))
				{
					MRPRegenerationProcess frmMRPRegenerationProcess = new MRPRegenerationProcess(int.Parse(txtCycle.Tag.ToString()));	
					frmMRPRegenerationProcess.Show();
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
		/// chkPONotReceipt_CheckedChanged
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Thursday, June 29 2006</date>
		private void chkPONotReceipt_CheckedChanged(object sender, EventArgs e)
		{
			const string METHOD_NAME = THIS + ".chkPONotReceipt_CheckedChanged()";
			try
			{
				if (chkPONotReceipt.Checked)
				{
					chkReturnToVendor.Enabled = true;
					txtNumbersOfDays.Enabled = true;
                    lblNumbersOfDays.ForeColor = Color.Maroon;
				}
				else
				{
					chkReturnToVendor.Enabled = false;
					chkReturnToVendor.Checked = false;
					txtNumbersOfDays.Enabled = false;
					txtNumbersOfDays.Value = null;
					lblNumbersOfDays.ForeColor = Color.Black;
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

		private void btnUpdate_Click(object sender, EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnUpdate_Click()";
			try
			{
				Cursor = Cursors.WaitCursor;
				DataTable dtbPONotReceipt = null;
				DataTable dtbPOReceipt = null;
				DataTable dtbReturnToVendor = null;
				DataTable dtbAvailable = null;
				// list of outside item
				MRPCycleOptionBO boCycle = new MRPCycleOptionBO();
				DataTable dtbItemList = boCycle.ListOutsideItem();
				DateTime dtmServerDate = boUtil.GetDBDate();
				DateTime dtmAsOfDate = (DateTime)dtmFromDate.Value;
				DateTime dtmBeginMonth = new DateTime(dtmServerDate.Year, dtmServerDate.Month, 1);
				int intNumOfDay = 0;
				try
				{
					intNumOfDay = Convert.ToInt32(txtNumbersOfDays.Value);
				}
				catch{}
				DateTime dtmBeginDate = dtmBeginMonth.AddDays(intNumOfDay);
				if (chkPONotReceipt.Checked)
				{
					MRPRegenerationProcessBO boMRP = new MRPRegenerationProcessBO();
					dtbPONotReceipt = boMRP.GetPONotReceipt(dtmBeginDate, dtmBeginMonth, CCNID);
					dtbPOReceipt = boMRP.GetPOReceipt(dtmBeginDate, dtmBeginMonth, CCNID);
					if (chkReturnToVendor.Checked)
						dtbReturnToVendor = boMRP.GetReturnToVendor(dtmBeginDate, dtmBeginMonth);
				}
				StringBuilder strSQLItems = BuildSQLIN(dtbItemList);
				DataTable dtbBeginForNIGURI = boCycle.GetBeginMRP(dtmAsOfDate);
				DataRow drowMasLocInfo = dstGridData.Tables[0].Rows[0];
				int intMasLocID = Convert.ToInt32(drowMasLocInfo[MST_MasterLocationTable.MASTERLOCATIONID_FLD]);
				if (Convert.ToBoolean(drowMasLocInfo[MTR_MRPCycleOptionDetailTable.ONHAND_FLD]))
					dtbAvailable = boCycle.GetAvailableQuantityForPlan(dtmBeginMonth);
				
				DataTable dtbItemsReplenishPrevious = boCycle.getAllReleasePOForItems(strSQLItems, dtmBeginMonth, dtmAsOfDate, intMasLocID);
				DataTable dtbSO = boCycle.getAllReleaseSOForItems(strSQLItems, dtmBeginMonth, dtmAsOfDate, intMasLocID);
				DataTable dtbCPO = boCycle.getAllCPOForItems(strSQLItems, dtmBeginMonth, dtmAsOfDate, intMasLocID);
				
				foreach (DataRow drowItem in dtbItemList.Rows)
				{
					string strProductID = drowItem[ITM_ProductTable.PRODUCTID_FLD].ToString();
					
					decimal decLocationQuantity = 0;
					try
					{
						decLocationQuantity += Convert.ToDecimal(dtbAvailable.Compute("SUM(" + Constants.SUPPLY_QUANTITY_FLD + ")",
							ITM_ProductTable.PRODUCTID_FLD + "=" + strProductID
							+ " AND " + ITM_ProductTable.LOCATIONID_FLD + "=" + drowItem[ITM_ProductTable.LOCATIONID_FLD].ToString()));
					}
					catch{}

					decimal decAvailable = 0, decAvailableNIGURY = 0;
					if (drowMasLocInfo[MTR_MRPCycleOptionDetailTable.ONHAND_FLD].ToString() == true.ToString())
					{
						decAvailable = GetAllRemainBeforeRun(Convert.ToInt32(strProductID), dtbSO, dtbCPO, dtbItemsReplenishPrevious);
						decAvailableNIGURY = decAvailable;

						try
						{
							decAvailable -= (decimal)drowItem[ITM_ProductTable.SAFETYSTOCK_FLD];
						}
						catch{}
							

						decLocationQuantity += decAvailableNIGURY;
						DataRow[] drowAvaiItems;
						if (dtbAvailable.Rows.Count > 0)
							drowAvaiItems = dtbAvailable.Select(ITM_ProductTable.PRODUCTID_FLD + "=" + strProductID);
						else
							drowAvaiItems = new DataRow[0];
						if (drowAvaiItems.Length > 0)
						{
							try
							{
								decAvailable += Convert.ToDecimal(dtbAvailable.Compute("SUM(" + Constants.SUPPLY_QUANTITY_FLD + ")",
									ITM_ProductTable.PRODUCTID_FLD + "=" + strProductID));
								decAvailableNIGURY += Convert.ToDecimal(dtbAvailable.Compute("SUM(" + Constants.SUPPLY_QUANTITY_FLD + ")",
									ITM_ProductTable.PRODUCTID_FLD + "=" + strProductID));
							}
							catch{}
						}
					}

					decimal decPONotReceipt = 0, decReturnToVendor = 0;
					if (chkPONotReceipt.Checked)
					{
						string strFilter = "MasterLocationID = " + intMasLocID + " AND ProductID = " + strProductID;
						try
						{
							decPONotReceipt = Convert.ToDecimal(dtbPOReceipt.Compute("SUM(Quantity)", strFilter));
						}
						catch{}
						try
						{
							decPONotReceipt -= Convert.ToDecimal(dtbPONotReceipt.Compute("SUM(Quantity)", strFilter));
						}
						catch{}
						if (chkReturnToVendor.Checked)
						{
							try
							{
								decReturnToVendor = Convert.ToDecimal(dtbReturnToVendor.Compute("SUM(Quantity)", strFilter));
							}
							catch{}
						}
					}
					// Begin OH = OH(AsofDate) + Sum(PO.Delivery - POReceipt) - Sum(ReturnToVendor)
					decAvailable = decAvailable - decPONotReceipt - decReturnToVendor;
					decAvailableNIGURY = decAvailableNIGURY - decPONotReceipt - decReturnToVendor;
					decLocationQuantity = decLocationQuantity - decPONotReceipt - decReturnToVendor;
					DataRow[] drowsBegin = dtbBeginForNIGURI.Select(ITM_ProductTable.PRODUCTID_FLD + "=" + strProductID
						+ " AND " + ITM_ProductTable.LOCATIONID_FLD + "=" + drowItem[ITM_ProductTable.LOCATIONID_FLD].ToString()
						+ " AND " + IV_BeginMRPTable.ASOFTDATE_FLD + "='" + dtmAsOfDate.ToString("G") + "'");
					if (drowsBegin.Length > 0) // already exits, need to update new quantity
					{
						drowsBegin[0][IV_BeginMRPTable.QUANTITY_FLD] = decLocationQuantity;
						drowsBegin[0][IV_BeginMRPTable.QUANTITYMAP_FLD] = decAvailableNIGURY;
					}
					else // make a new record
					{
						DataRow drowBegin = dtbBeginForNIGURI.NewRow();
						drowBegin[IV_BeginMRPTable.ASOFTDATE_FLD] = dtmAsOfDate;
						drowBegin[IV_BeginMRPTable.LOCATIONID_FLD] = drowItem[ITM_ProductTable.LOCATIONID_FLD];
						drowBegin[IV_BeginMRPTable.PRODUCTID_FLD] = drowItem[ITM_ProductTable.PRODUCTID_FLD];
						drowBegin[IV_BeginMRPTable.QUANTITY_FLD] = decLocationQuantity;
						drowBegin[IV_BeginMRPTable.QUANTITYMAP_FLD] = decAvailableNIGURY;
						dtbBeginForNIGURI.Rows.Add(drowBegin); 
					}
				}
				boCycle.UpdateBeginMRP(dtbBeginForNIGURI);
				PCSMessageBox.Show(ErrorCode.MESSAGE_AFTER_SAVE_DATA);
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
			finally
			{
				Cursor = Cursors.Default;
			}
		}
		private StringBuilder BuildSQLIN(DataTable pdtbItems)
		{
			StringBuilder strSQL = new StringBuilder();
			strSQL.Append("(");
			foreach (DataRow drowData in pdtbItems.Rows)
				strSQL.Append(drowData[ITM_ProductTable.PRODUCTID_FLD].ToString() + Constants.COMMA);
			strSQL = strSQL.Remove(strSQL.Length - 1, 1);
			strSQL.Append(")");

			return strSQL;
		}

		private decimal GetAllRemainBeforeRun(int pintProductID, DataTable pdtbSO, DataTable pdtbCPO, DataTable pdtbReplenish)
		{
			decimal decResult = 0;
			decimal decRequest = 0, decSupply = 0;
			try
			{
				decRequest = Convert.ToDecimal(pdtbSO.Compute("Sum(" + REQUEST_WO_QTITY_FLD + ")", ITM_ProductTable.PRODUCTID_FLD + "=" + pintProductID));
			}
			catch (Exception ex)
			{
				Console.Write(ex);
			}
			try
			{
				decRequest += Convert.ToDecimal(pdtbCPO.Compute("Sum(" + REQUEST_WO_QTITY_FLD + ")", ITM_ProductTable.PRODUCTID_FLD + "=" + pintProductID));
			}
			catch (Exception ex)
			{
				Console.Write(ex);
			}
			try
			{
				decSupply = Convert.ToDecimal(pdtbReplenish.Compute("Sum(" + REPLENISH_PO_QTITY_FLD + ")", ITM_ProductTable.PRODUCTID_FLD + "=" + pintProductID));
			}

			catch{}
			decResult = decSupply - decRequest;
		
			return decResult;
		}

	}
}
