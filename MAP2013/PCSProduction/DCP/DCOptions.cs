using System;
using System.Drawing;
using System.Data;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

using C1.Win.C1TrueDBGrid;

//Using PCS's namespaces
using PCSComProduction.DCP.BO;
using PCSComProduction.DCP.DS;
using PCSComUtils.Common;
using PCSComUtils.Common.BO;
using PCSComUtils.MasterSetup.DS;
using PCSComUtils.PCSExc;
using PCSUtils.Log;
using PCSUtils.Utils;

namespace PCSProduction.DCP
{
	/// <summary>
	/// Summary description for DCOptions.
	/// </summary>
	public class DCOptions : System.Windows.Forms.Form
	{
		#region Windows Generation Decalaration

		private System.Windows.Forms.Label lblMPSCycle;
		private System.Windows.Forms.Label lblCCN;
		private System.Windows.Forms.TextBox txtDescription;
		private System.Windows.Forms.Label lblDescription;
		private System.Windows.Forms.Label lblCycle;
		private System.Windows.Forms.Label lblMPS_Schedule_Code;
		private System.Windows.Forms.Label lblMaxDay;
		private System.Windows.Forms.Label lblAsOfDate;
		private System.Windows.Forms.Label lblLastUpdate;
		private System.Windows.Forms.Button btnEdit;
		private System.Windows.Forms.Button btnDelete;
		private System.Windows.Forms.Button btnSave;
		private System.Windows.Forms.Button btnAdd;
		private System.Windows.Forms.Button btnClose;
		private System.Windows.Forms.Button btnHelp;
		private C1.Win.C1List.C1Combo cboCCN;
		private System.Windows.Forms.TextBox txtCycle;
		private System.Windows.Forms.TextBox txtMPSCycle;
		private System.Windows.Forms.Button btnMPSCycle;
		private System.Windows.Forms.Button btnSearchCycle;
		private C1.Win.C1Input.C1DateEdit dtmLastUpdate;
		private System.Windows.Forms.CheckBox chkIgnoreMoveTime;
		private System.Windows.Forms.Label lblScheduleType;
		private System.Windows.Forms.Label lblInfiniteScheduling;
		private System.Windows.Forms.Label lblLoadAveraging;
		private System.Windows.Forms.Label lblFiniteScheduling;
		private C1.Win.C1TrueDBGrid.C1TrueDBGrid dgrdData;
		private C1.Win.C1Input.C1DateEdit dtmToDate;
		private System.Windows.Forms.CheckBox chkCheckPoint;
		private System.Windows.Forms.Button btnRemoveDCPResults;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.Button btnProcess;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		#endregion System Decalaration
		
		#region Declaration
		private const string THIS = "PCSProduction.DCP.DCOptions";		
		private const string ZERO_STRING = "0";
		private const string VALUE_FLD = "Value";
		private const string NAME_FLD = "Name";

		private EnumAction enuFormAction = EnumAction.Default;
		private int intMaxDays = 0;
		private bool blnDataIsValid = false;
		private DataTable dtbGridLayOut;
		private DataTable dtbDCOptionDetail;
		
		private DCOptionsBO boDCOptions = new DCOptionsBO();
		
		private PRO_DCOptionMasterVO voDCOptionMaster = new  PRO_DCOptionMasterVO();
		private C1.Win.C1List.C1Combo cboScheduleCode;
		private C1.Win.C1List.C1Combo cboScheduleType;
		private System.Windows.Forms.TextBox txtPlanHorizon;
		private System.Windows.Forms.Label lblPlanHorizon;
		private System.Windows.Forms.CheckBox chkSafetyStock;
		private System.Windows.Forms.CheckBox chkOnHand;
		private System.Windows.Forms.Label label1;
		private C1.Win.C1Input.C1DateEdit dtmFromDate;
		private System.Windows.Forms.Label lblByDay;
		private System.Windows.Forms.Label lblByHour;
		private System.Windows.Forms.ComboBox cboGroupBy;
		private System.Windows.Forms.Label lblYear;
		private System.Windows.Forms.Label lblMonth;
		private System.Windows.Forms.Label lblVersion;
		private System.Windows.Forms.ComboBox cboYear;
		private System.Windows.Forms.ComboBox cboMonth;
		private C1.Win.C1Input.C1NumericEdit txtVersion;
		private System.Windows.Forms.Label lblByShift;
		private System.Windows.Forms.CheckBox chkUseCacheAsBeginStock;		
		private const string MAXDAYS_FLD = "MaxDays";
		#endregion Decalaration
		
		#region Constructor, Destructor
		
		public DCOptions()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			mintDCOptionMasterID = 0;
			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}
		private int mintDCOptionMasterID = 0;
		public DCOptions(int pintDCOptionMasterID)
		{
			try
			{				
				// Required for Windows Form Designer support				
				InitializeComponent();
				mintDCOptionMasterID = pintDCOptionMasterID;				
			}
			catch
			{}
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
		
		#endregion

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(DCOptions));
			this.lblMPSCycle = new System.Windows.Forms.Label();
			this.btnEdit = new System.Windows.Forms.Button();
			this.btnDelete = new System.Windows.Forms.Button();
			this.btnSave = new System.Windows.Forms.Button();
			this.btnAdd = new System.Windows.Forms.Button();
			this.btnClose = new System.Windows.Forms.Button();
			this.btnHelp = new System.Windows.Forms.Button();
			this.lblCCN = new System.Windows.Forms.Label();
			this.txtDescription = new System.Windows.Forms.TextBox();
			this.lblDescription = new System.Windows.Forms.Label();
			this.lblCycle = new System.Windows.Forms.Label();
			this.lblMPS_Schedule_Code = new System.Windows.Forms.Label();
			this.lblMaxDay = new System.Windows.Forms.Label();
			this.lblAsOfDate = new System.Windows.Forms.Label();
			this.lblLastUpdate = new System.Windows.Forms.Label();
			this.dtmFromDate = new C1.Win.C1Input.C1DateEdit();
			this.cboCCN = new C1.Win.C1List.C1Combo();
			this.chkIgnoreMoveTime = new System.Windows.Forms.CheckBox();
			this.txtCycle = new System.Windows.Forms.TextBox();
			this.btnSearchCycle = new System.Windows.Forms.Button();
			this.btnMPSCycle = new System.Windows.Forms.Button();
			this.txtMPSCycle = new System.Windows.Forms.TextBox();
			this.dtmLastUpdate = new C1.Win.C1Input.C1DateEdit();
			this.lblScheduleType = new System.Windows.Forms.Label();
			this.lblInfiniteScheduling = new System.Windows.Forms.Label();
			this.lblFiniteScheduling = new System.Windows.Forms.Label();
			this.lblLoadAveraging = new System.Windows.Forms.Label();
			this.dgrdData = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
			this.dtmToDate = new C1.Win.C1Input.C1DateEdit();
			this.chkCheckPoint = new System.Windows.Forms.CheckBox();
			this.btnRemoveDCPResults = new System.Windows.Forms.Button();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.btnProcess = new System.Windows.Forms.Button();
			this.cboScheduleCode = new C1.Win.C1List.C1Combo();
			this.cboScheduleType = new C1.Win.C1List.C1Combo();
			this.txtPlanHorizon = new System.Windows.Forms.TextBox();
			this.lblPlanHorizon = new System.Windows.Forms.Label();
			this.chkSafetyStock = new System.Windows.Forms.CheckBox();
			this.chkOnHand = new System.Windows.Forms.CheckBox();
			this.label1 = new System.Windows.Forms.Label();
			this.lblByDay = new System.Windows.Forms.Label();
			this.lblByHour = new System.Windows.Forms.Label();
			this.cboGroupBy = new System.Windows.Forms.ComboBox();
			this.cboYear = new System.Windows.Forms.ComboBox();
			this.cboMonth = new System.Windows.Forms.ComboBox();
			this.lblYear = new System.Windows.Forms.Label();
			this.lblMonth = new System.Windows.Forms.Label();
			this.lblVersion = new System.Windows.Forms.Label();
			this.txtVersion = new C1.Win.C1Input.C1NumericEdit();
			this.lblByShift = new System.Windows.Forms.Label();
			this.chkUseCacheAsBeginStock = new System.Windows.Forms.CheckBox();
			((System.ComponentModel.ISupportInitialize)(this.dtmFromDate)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.cboCCN)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dtmLastUpdate)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dgrdData)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dtmToDate)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.cboScheduleCode)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.cboScheduleType)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.txtVersion)).BeginInit();
			this.SuspendLayout();
			// 
			// lblMPSCycle
			// 
			this.lblMPSCycle.AccessibleDescription = resources.GetString("lblMPSCycle.AccessibleDescription");
			this.lblMPSCycle.AccessibleName = resources.GetString("lblMPSCycle.AccessibleName");
			this.lblMPSCycle.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblMPSCycle.Anchor")));
			this.lblMPSCycle.AutoSize = ((bool)(resources.GetObject("lblMPSCycle.AutoSize")));
			this.lblMPSCycle.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblMPSCycle.Dock")));
			this.lblMPSCycle.Enabled = ((bool)(resources.GetObject("lblMPSCycle.Enabled")));
			this.lblMPSCycle.Font = ((System.Drawing.Font)(resources.GetObject("lblMPSCycle.Font")));
			this.lblMPSCycle.ForeColor = System.Drawing.Color.Maroon;
			this.lblMPSCycle.Image = ((System.Drawing.Image)(resources.GetObject("lblMPSCycle.Image")));
			this.lblMPSCycle.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblMPSCycle.ImageAlign")));
			this.lblMPSCycle.ImageIndex = ((int)(resources.GetObject("lblMPSCycle.ImageIndex")));
			this.lblMPSCycle.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblMPSCycle.ImeMode")));
			this.lblMPSCycle.Location = ((System.Drawing.Point)(resources.GetObject("lblMPSCycle.Location")));
			this.lblMPSCycle.Name = "lblMPSCycle";
			this.lblMPSCycle.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblMPSCycle.RightToLeft")));
			this.lblMPSCycle.Size = ((System.Drawing.Size)(resources.GetObject("lblMPSCycle.Size")));
			this.lblMPSCycle.TabIndex = ((int)(resources.GetObject("lblMPSCycle.TabIndex")));
			this.lblMPSCycle.Text = resources.GetString("lblMPSCycle.Text");
			this.lblMPSCycle.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblMPSCycle.TextAlign")));
			this.lblMPSCycle.Visible = ((bool)(resources.GetObject("lblMPSCycle.Visible")));
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
			// btnDelete
			// 
			this.btnDelete.AccessibleDescription = resources.GetString("btnDelete.AccessibleDescription");
			this.btnDelete.AccessibleName = resources.GetString("btnDelete.AccessibleName");
			this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnDelete.Anchor")));
			this.btnDelete.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnDelete.BackgroundImage")));
			this.btnDelete.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnDelete.Dock")));
			this.btnDelete.Enabled = ((bool)(resources.GetObject("btnDelete.Enabled")));
			this.btnDelete.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnDelete.FlatStyle")));
			this.btnDelete.Font = ((System.Drawing.Font)(resources.GetObject("btnDelete.Font")));
			this.btnDelete.Image = ((System.Drawing.Image)(resources.GetObject("btnDelete.Image")));
			this.btnDelete.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnDelete.ImageAlign")));
			this.btnDelete.ImageIndex = ((int)(resources.GetObject("btnDelete.ImageIndex")));
			this.btnDelete.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnDelete.ImeMode")));
			this.btnDelete.Location = ((System.Drawing.Point)(resources.GetObject("btnDelete.Location")));
			this.btnDelete.Name = "btnDelete";
			this.btnDelete.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnDelete.RightToLeft")));
			this.btnDelete.Size = ((System.Drawing.Size)(resources.GetObject("btnDelete.Size")));
			this.btnDelete.TabIndex = ((int)(resources.GetObject("btnDelete.TabIndex")));
			this.btnDelete.Text = resources.GetString("btnDelete.Text");
			this.btnDelete.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnDelete.TextAlign")));
			this.btnDelete.Visible = ((bool)(resources.GetObject("btnDelete.Visible")));
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
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
			// btnAdd
			// 
			this.btnAdd.AccessibleDescription = resources.GetString("btnAdd.AccessibleDescription");
			this.btnAdd.AccessibleName = resources.GetString("btnAdd.AccessibleName");
			this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnAdd.Anchor")));
			this.btnAdd.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnAdd.BackgroundImage")));
			this.btnAdd.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnAdd.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnAdd.Dock")));
			this.btnAdd.Enabled = ((bool)(resources.GetObject("btnAdd.Enabled")));
			this.btnAdd.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnAdd.FlatStyle")));
			this.btnAdd.Font = ((System.Drawing.Font)(resources.GetObject("btnAdd.Font")));
			this.btnAdd.Image = ((System.Drawing.Image)(resources.GetObject("btnAdd.Image")));
			this.btnAdd.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnAdd.ImageAlign")));
			this.btnAdd.ImageIndex = ((int)(resources.GetObject("btnAdd.ImageIndex")));
			this.btnAdd.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnAdd.ImeMode")));
			this.btnAdd.Location = ((System.Drawing.Point)(resources.GetObject("btnAdd.Location")));
			this.btnAdd.Name = "btnAdd";
			this.btnAdd.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnAdd.RightToLeft")));
			this.btnAdd.Size = ((System.Drawing.Size)(resources.GetObject("btnAdd.Size")));
			this.btnAdd.TabIndex = ((int)(resources.GetObject("btnAdd.TabIndex")));
			this.btnAdd.Text = resources.GetString("btnAdd.Text");
			this.btnAdd.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnAdd.TextAlign")));
			this.btnAdd.Visible = ((bool)(resources.GetObject("btnAdd.Visible")));
			this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
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
			this.btnHelp.Click += new System.EventHandler(this.btnHelp_Click);
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
			// txtDescription
			// 
			this.txtDescription.AccessibleDescription = resources.GetString("txtDescription.AccessibleDescription");
			this.txtDescription.AccessibleName = resources.GetString("txtDescription.AccessibleName");
			this.txtDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("txtDescription.Anchor")));
			this.txtDescription.AutoSize = ((bool)(resources.GetObject("txtDescription.AutoSize")));
			this.txtDescription.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("txtDescription.BackgroundImage")));
			this.txtDescription.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("txtDescription.Dock")));
			this.txtDescription.Enabled = ((bool)(resources.GetObject("txtDescription.Enabled")));
			this.txtDescription.Font = ((System.Drawing.Font)(resources.GetObject("txtDescription.Font")));
			this.txtDescription.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("txtDescription.ImeMode")));
			this.txtDescription.Location = ((System.Drawing.Point)(resources.GetObject("txtDescription.Location")));
			this.txtDescription.MaxLength = ((int)(resources.GetObject("txtDescription.MaxLength")));
			this.txtDescription.Multiline = ((bool)(resources.GetObject("txtDescription.Multiline")));
			this.txtDescription.Name = "txtDescription";
			this.txtDescription.PasswordChar = ((char)(resources.GetObject("txtDescription.PasswordChar")));
			this.txtDescription.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("txtDescription.RightToLeft")));
			this.txtDescription.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("txtDescription.ScrollBars")));
			this.txtDescription.Size = ((System.Drawing.Size)(resources.GetObject("txtDescription.Size")));
			this.txtDescription.TabIndex = ((int)(resources.GetObject("txtDescription.TabIndex")));
			this.txtDescription.Text = resources.GetString("txtDescription.Text");
			this.txtDescription.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("txtDescription.TextAlign")));
			this.txtDescription.Visible = ((bool)(resources.GetObject("txtDescription.Visible")));
			this.txtDescription.WordWrap = ((bool)(resources.GetObject("txtDescription.WordWrap")));
			// 
			// lblDescription
			// 
			this.lblDescription.AccessibleDescription = resources.GetString("lblDescription.AccessibleDescription");
			this.lblDescription.AccessibleName = resources.GetString("lblDescription.AccessibleName");
			this.lblDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblDescription.Anchor")));
			this.lblDescription.AutoSize = ((bool)(resources.GetObject("lblDescription.AutoSize")));
			this.lblDescription.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblDescription.Dock")));
			this.lblDescription.Enabled = ((bool)(resources.GetObject("lblDescription.Enabled")));
			this.lblDescription.Font = ((System.Drawing.Font)(resources.GetObject("lblDescription.Font")));
			this.lblDescription.Image = ((System.Drawing.Image)(resources.GetObject("lblDescription.Image")));
			this.lblDescription.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblDescription.ImageAlign")));
			this.lblDescription.ImageIndex = ((int)(resources.GetObject("lblDescription.ImageIndex")));
			this.lblDescription.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblDescription.ImeMode")));
			this.lblDescription.Location = ((System.Drawing.Point)(resources.GetObject("lblDescription.Location")));
			this.lblDescription.Name = "lblDescription";
			this.lblDescription.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblDescription.RightToLeft")));
			this.lblDescription.Size = ((System.Drawing.Size)(resources.GetObject("lblDescription.Size")));
			this.lblDescription.TabIndex = ((int)(resources.GetObject("lblDescription.TabIndex")));
			this.lblDescription.Text = resources.GetString("lblDescription.Text");
			this.lblDescription.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblDescription.TextAlign")));
			this.lblDescription.Visible = ((bool)(resources.GetObject("lblDescription.Visible")));
			// 
			// lblCycle
			// 
			this.lblCycle.AccessibleDescription = resources.GetString("lblCycle.AccessibleDescription");
			this.lblCycle.AccessibleName = resources.GetString("lblCycle.AccessibleName");
			this.lblCycle.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblCycle.Anchor")));
			this.lblCycle.AutoSize = ((bool)(resources.GetObject("lblCycle.AutoSize")));
			this.lblCycle.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblCycle.Dock")));
			this.lblCycle.Enabled = ((bool)(resources.GetObject("lblCycle.Enabled")));
			this.lblCycle.Font = ((System.Drawing.Font)(resources.GetObject("lblCycle.Font")));
			this.lblCycle.ForeColor = System.Drawing.Color.Maroon;
			this.lblCycle.Image = ((System.Drawing.Image)(resources.GetObject("lblCycle.Image")));
			this.lblCycle.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblCycle.ImageAlign")));
			this.lblCycle.ImageIndex = ((int)(resources.GetObject("lblCycle.ImageIndex")));
			this.lblCycle.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblCycle.ImeMode")));
			this.lblCycle.Location = ((System.Drawing.Point)(resources.GetObject("lblCycle.Location")));
			this.lblCycle.Name = "lblCycle";
			this.lblCycle.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblCycle.RightToLeft")));
			this.lblCycle.Size = ((System.Drawing.Size)(resources.GetObject("lblCycle.Size")));
			this.lblCycle.TabIndex = ((int)(resources.GetObject("lblCycle.TabIndex")));
			this.lblCycle.Text = resources.GetString("lblCycle.Text");
			this.lblCycle.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblCycle.TextAlign")));
			this.lblCycle.Visible = ((bool)(resources.GetObject("lblCycle.Visible")));
			// 
			// lblMPS_Schedule_Code
			// 
			this.lblMPS_Schedule_Code.AccessibleDescription = resources.GetString("lblMPS_Schedule_Code.AccessibleDescription");
			this.lblMPS_Schedule_Code.AccessibleName = resources.GetString("lblMPS_Schedule_Code.AccessibleName");
			this.lblMPS_Schedule_Code.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblMPS_Schedule_Code.Anchor")));
			this.lblMPS_Schedule_Code.AutoSize = ((bool)(resources.GetObject("lblMPS_Schedule_Code.AutoSize")));
			this.lblMPS_Schedule_Code.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblMPS_Schedule_Code.Dock")));
			this.lblMPS_Schedule_Code.Enabled = ((bool)(resources.GetObject("lblMPS_Schedule_Code.Enabled")));
			this.lblMPS_Schedule_Code.Font = ((System.Drawing.Font)(resources.GetObject("lblMPS_Schedule_Code.Font")));
			this.lblMPS_Schedule_Code.ForeColor = System.Drawing.Color.Maroon;
			this.lblMPS_Schedule_Code.Image = ((System.Drawing.Image)(resources.GetObject("lblMPS_Schedule_Code.Image")));
			this.lblMPS_Schedule_Code.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblMPS_Schedule_Code.ImageAlign")));
			this.lblMPS_Schedule_Code.ImageIndex = ((int)(resources.GetObject("lblMPS_Schedule_Code.ImageIndex")));
			this.lblMPS_Schedule_Code.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblMPS_Schedule_Code.ImeMode")));
			this.lblMPS_Schedule_Code.Location = ((System.Drawing.Point)(resources.GetObject("lblMPS_Schedule_Code.Location")));
			this.lblMPS_Schedule_Code.Name = "lblMPS_Schedule_Code";
			this.lblMPS_Schedule_Code.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblMPS_Schedule_Code.RightToLeft")));
			this.lblMPS_Schedule_Code.Size = ((System.Drawing.Size)(resources.GetObject("lblMPS_Schedule_Code.Size")));
			this.lblMPS_Schedule_Code.TabIndex = ((int)(resources.GetObject("lblMPS_Schedule_Code.TabIndex")));
			this.lblMPS_Schedule_Code.Text = resources.GetString("lblMPS_Schedule_Code.Text");
			this.lblMPS_Schedule_Code.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblMPS_Schedule_Code.TextAlign")));
			this.lblMPS_Schedule_Code.Visible = ((bool)(resources.GetObject("lblMPS_Schedule_Code.Visible")));
			// 
			// lblMaxDay
			// 
			this.lblMaxDay.AccessibleDescription = resources.GetString("lblMaxDay.AccessibleDescription");
			this.lblMaxDay.AccessibleName = resources.GetString("lblMaxDay.AccessibleName");
			this.lblMaxDay.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblMaxDay.Anchor")));
			this.lblMaxDay.AutoSize = ((bool)(resources.GetObject("lblMaxDay.AutoSize")));
			this.lblMaxDay.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblMaxDay.Dock")));
			this.lblMaxDay.Enabled = ((bool)(resources.GetObject("lblMaxDay.Enabled")));
			this.lblMaxDay.Font = ((System.Drawing.Font)(resources.GetObject("lblMaxDay.Font")));
			this.lblMaxDay.ForeColor = System.Drawing.Color.Maroon;
			this.lblMaxDay.Image = ((System.Drawing.Image)(resources.GetObject("lblMaxDay.Image")));
			this.lblMaxDay.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblMaxDay.ImageAlign")));
			this.lblMaxDay.ImageIndex = ((int)(resources.GetObject("lblMaxDay.ImageIndex")));
			this.lblMaxDay.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblMaxDay.ImeMode")));
			this.lblMaxDay.Location = ((System.Drawing.Point)(resources.GetObject("lblMaxDay.Location")));
			this.lblMaxDay.Name = "lblMaxDay";
			this.lblMaxDay.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblMaxDay.RightToLeft")));
			this.lblMaxDay.Size = ((System.Drawing.Size)(resources.GetObject("lblMaxDay.Size")));
			this.lblMaxDay.TabIndex = ((int)(resources.GetObject("lblMaxDay.TabIndex")));
			this.lblMaxDay.Text = resources.GetString("lblMaxDay.Text");
			this.lblMaxDay.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblMaxDay.TextAlign")));
			this.lblMaxDay.Visible = ((bool)(resources.GetObject("lblMaxDay.Visible")));
			// 
			// lblAsOfDate
			// 
			this.lblAsOfDate.AccessibleDescription = resources.GetString("lblAsOfDate.AccessibleDescription");
			this.lblAsOfDate.AccessibleName = resources.GetString("lblAsOfDate.AccessibleName");
			this.lblAsOfDate.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblAsOfDate.Anchor")));
			this.lblAsOfDate.AutoSize = ((bool)(resources.GetObject("lblAsOfDate.AutoSize")));
			this.lblAsOfDate.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblAsOfDate.Dock")));
			this.lblAsOfDate.Enabled = ((bool)(resources.GetObject("lblAsOfDate.Enabled")));
			this.lblAsOfDate.Font = ((System.Drawing.Font)(resources.GetObject("lblAsOfDate.Font")));
			this.lblAsOfDate.ForeColor = System.Drawing.Color.Maroon;
			this.lblAsOfDate.Image = ((System.Drawing.Image)(resources.GetObject("lblAsOfDate.Image")));
			this.lblAsOfDate.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblAsOfDate.ImageAlign")));
			this.lblAsOfDate.ImageIndex = ((int)(resources.GetObject("lblAsOfDate.ImageIndex")));
			this.lblAsOfDate.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblAsOfDate.ImeMode")));
			this.lblAsOfDate.Location = ((System.Drawing.Point)(resources.GetObject("lblAsOfDate.Location")));
			this.lblAsOfDate.Name = "lblAsOfDate";
			this.lblAsOfDate.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblAsOfDate.RightToLeft")));
			this.lblAsOfDate.Size = ((System.Drawing.Size)(resources.GetObject("lblAsOfDate.Size")));
			this.lblAsOfDate.TabIndex = ((int)(resources.GetObject("lblAsOfDate.TabIndex")));
			this.lblAsOfDate.Text = resources.GetString("lblAsOfDate.Text");
			this.lblAsOfDate.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblAsOfDate.TextAlign")));
			this.lblAsOfDate.Visible = ((bool)(resources.GetObject("lblAsOfDate.Visible")));
			// 
			// lblLastUpdate
			// 
			this.lblLastUpdate.AccessibleDescription = resources.GetString("lblLastUpdate.AccessibleDescription");
			this.lblLastUpdate.AccessibleName = resources.GetString("lblLastUpdate.AccessibleName");
			this.lblLastUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblLastUpdate.Anchor")));
			this.lblLastUpdate.AutoSize = ((bool)(resources.GetObject("lblLastUpdate.AutoSize")));
			this.lblLastUpdate.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblLastUpdate.Dock")));
			this.lblLastUpdate.Enabled = ((bool)(resources.GetObject("lblLastUpdate.Enabled")));
			this.lblLastUpdate.Font = ((System.Drawing.Font)(resources.GetObject("lblLastUpdate.Font")));
			this.lblLastUpdate.Image = ((System.Drawing.Image)(resources.GetObject("lblLastUpdate.Image")));
			this.lblLastUpdate.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblLastUpdate.ImageAlign")));
			this.lblLastUpdate.ImageIndex = ((int)(resources.GetObject("lblLastUpdate.ImageIndex")));
			this.lblLastUpdate.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblLastUpdate.ImeMode")));
			this.lblLastUpdate.Location = ((System.Drawing.Point)(resources.GetObject("lblLastUpdate.Location")));
			this.lblLastUpdate.Name = "lblLastUpdate";
			this.lblLastUpdate.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblLastUpdate.RightToLeft")));
			this.lblLastUpdate.Size = ((System.Drawing.Size)(resources.GetObject("lblLastUpdate.Size")));
			this.lblLastUpdate.TabIndex = ((int)(resources.GetObject("lblLastUpdate.TabIndex")));
			this.lblLastUpdate.Text = resources.GetString("lblLastUpdate.Text");
			this.lblLastUpdate.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblLastUpdate.TextAlign")));
			this.lblLastUpdate.Visible = ((bool)(resources.GetObject("lblLastUpdate.Visible")));
			// 
			// dtmFromDate
			// 
			this.dtmFromDate.AcceptsEscape = ((bool)(resources.GetObject("dtmFromDate.AcceptsEscape")));
			this.dtmFromDate.AccessibleDescription = resources.GetString("dtmFromDate.AccessibleDescription");
			this.dtmFromDate.AccessibleName = resources.GetString("dtmFromDate.AccessibleName");
			this.dtmFromDate.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("dtmFromDate.Anchor")));
			this.dtmFromDate.AutoSize = ((bool)(resources.GetObject("dtmFromDate.AutoSize")));
			this.dtmFromDate.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("dtmFromDate.BackgroundImage")));
			this.dtmFromDate.BorderStyle = ((System.Windows.Forms.BorderStyle)(resources.GetObject("dtmFromDate.BorderStyle")));
			// 
			// dtmFromDate.Calendar
			// 
			this.dtmFromDate.Calendar.AccessibleDescription = resources.GetString("dtmFromDate.Calendar.AccessibleDescription");
			this.dtmFromDate.Calendar.AccessibleName = resources.GetString("dtmFromDate.Calendar.AccessibleName");
			this.dtmFromDate.Calendar.AnnuallyBoldedDates = ((System.DateTime[])(resources.GetObject("dtmFromDate.Calendar.AnnuallyBoldedDates")));
			this.dtmFromDate.Calendar.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("dtmFromDate.Calendar.BackgroundImage")));
			this.dtmFromDate.Calendar.BoldedDates = ((System.DateTime[])(resources.GetObject("dtmFromDate.Calendar.BoldedDates")));
			this.dtmFromDate.Calendar.CalendarDimensions = ((System.Drawing.Size)(resources.GetObject("dtmFromDate.Calendar.CalendarDimensions")));
			this.dtmFromDate.Calendar.Enabled = ((bool)(resources.GetObject("dtmFromDate.Calendar.Enabled")));
			this.dtmFromDate.Calendar.FirstDayOfWeek = ((System.Windows.Forms.Day)(resources.GetObject("dtmFromDate.Calendar.FirstDayOfWeek")));
			this.dtmFromDate.Calendar.Font = ((System.Drawing.Font)(resources.GetObject("dtmFromDate.Calendar.Font")));
			this.dtmFromDate.Calendar.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("dtmFromDate.Calendar.ImeMode")));
			this.dtmFromDate.Calendar.MonthlyBoldedDates = ((System.DateTime[])(resources.GetObject("dtmFromDate.Calendar.MonthlyBoldedDates")));
			this.dtmFromDate.Calendar.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("dtmFromDate.Calendar.RightToLeft")));
			this.dtmFromDate.Calendar.ShowClearButton = ((bool)(resources.GetObject("dtmFromDate.Calendar.ShowClearButton")));
			this.dtmFromDate.Calendar.ShowTodayButton = ((bool)(resources.GetObject("dtmFromDate.Calendar.ShowTodayButton")));
			this.dtmFromDate.Calendar.ShowWeekNumbers = ((bool)(resources.GetObject("dtmFromDate.Calendar.ShowWeekNumbers")));
			this.dtmFromDate.CaseSensitive = ((bool)(resources.GetObject("dtmFromDate.CaseSensitive")));
			this.dtmFromDate.Culture = ((int)(resources.GetObject("dtmFromDate.Culture")));
			this.dtmFromDate.CurrentTimeZone = ((bool)(resources.GetObject("dtmFromDate.CurrentTimeZone")));
			this.dtmFromDate.CustomFormat = resources.GetString("dtmFromDate.CustomFormat");
			this.dtmFromDate.DaylightTimeAdjustment = ((C1.Win.C1Input.DaylightTimeAdjustmentEnum)(resources.GetObject("dtmFromDate.DaylightTimeAdjustment")));
			this.dtmFromDate.DisplayFormat.CustomFormat = resources.GetString("dtmFromDate.DisplayFormat.CustomFormat");
			this.dtmFromDate.DisplayFormat.FormatType = ((C1.Win.C1Input.FormatTypeEnum)(resources.GetObject("dtmFromDate.DisplayFormat.FormatType")));
			this.dtmFromDate.DisplayFormat.Inherit = ((C1.Win.C1Input.FormatInfoInheritFlags)(resources.GetObject("dtmFromDate.DisplayFormat.Inherit")));
			this.dtmFromDate.DisplayFormat.NullText = resources.GetString("dtmFromDate.DisplayFormat.NullText");
			this.dtmFromDate.DisplayFormat.TrimEnd = ((bool)(resources.GetObject("dtmFromDate.DisplayFormat.TrimEnd")));
			this.dtmFromDate.DisplayFormat.TrimStart = ((bool)(resources.GetObject("dtmFromDate.DisplayFormat.TrimStart")));
			this.dtmFromDate.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("dtmFromDate.Dock")));
			this.dtmFromDate.DropDownFormAlign = ((C1.Win.C1Input.DropDownFormAlignmentEnum)(resources.GetObject("dtmFromDate.DropDownFormAlign")));
			this.dtmFromDate.EditFormat.CustomFormat = resources.GetString("dtmFromDate.EditFormat.CustomFormat");
			this.dtmFromDate.EditFormat.FormatType = ((C1.Win.C1Input.FormatTypeEnum)(resources.GetObject("dtmFromDate.EditFormat.FormatType")));
			this.dtmFromDate.EditFormat.Inherit = ((C1.Win.C1Input.FormatInfoInheritFlags)(resources.GetObject("dtmFromDate.EditFormat.Inherit")));
			this.dtmFromDate.EditFormat.NullText = resources.GetString("dtmFromDate.EditFormat.NullText");
			this.dtmFromDate.EditFormat.TrimEnd = ((bool)(resources.GetObject("dtmFromDate.EditFormat.TrimEnd")));
			this.dtmFromDate.EditFormat.TrimStart = ((bool)(resources.GetObject("dtmFromDate.EditFormat.TrimStart")));
			this.dtmFromDate.EditMask = resources.GetString("dtmFromDate.EditMask");
			this.dtmFromDate.EmptyAsNull = ((bool)(resources.GetObject("dtmFromDate.EmptyAsNull")));
			this.dtmFromDate.Enabled = ((bool)(resources.GetObject("dtmFromDate.Enabled")));
			this.dtmFromDate.ErrorInfo.BeepOnError = ((bool)(resources.GetObject("dtmFromDate.ErrorInfo.BeepOnError")));
			this.dtmFromDate.ErrorInfo.ErrorMessage = resources.GetString("dtmFromDate.ErrorInfo.ErrorMessage");
			this.dtmFromDate.ErrorInfo.ErrorMessageCaption = resources.GetString("dtmFromDate.ErrorInfo.ErrorMessageCaption");
			this.dtmFromDate.ErrorInfo.ShowErrorMessage = ((bool)(resources.GetObject("dtmFromDate.ErrorInfo.ShowErrorMessage")));
			this.dtmFromDate.ErrorInfo.ValueOnError = ((object)(resources.GetObject("dtmFromDate.ErrorInfo.ValueOnError")));
			this.dtmFromDate.Font = ((System.Drawing.Font)(resources.GetObject("dtmFromDate.Font")));
			this.dtmFromDate.FormatType = ((C1.Win.C1Input.FormatTypeEnum)(resources.GetObject("dtmFromDate.FormatType")));
			this.dtmFromDate.GapHeight = ((int)(resources.GetObject("dtmFromDate.GapHeight")));
			this.dtmFromDate.GMTOffset = ((System.TimeSpan)(resources.GetObject("dtmFromDate.GMTOffset")));
			this.dtmFromDate.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("dtmFromDate.ImeMode")));
			this.dtmFromDate.InitialSelection = ((C1.Win.C1Input.InitialSelectionEnum)(resources.GetObject("dtmFromDate.InitialSelection")));
			this.dtmFromDate.Location = ((System.Drawing.Point)(resources.GetObject("dtmFromDate.Location")));
			this.dtmFromDate.MaskInfo.AutoTabWhenFilled = ((bool)(resources.GetObject("dtmFromDate.MaskInfo.AutoTabWhenFilled")));
			this.dtmFromDate.MaskInfo.CaseSensitive = ((bool)(resources.GetObject("dtmFromDate.MaskInfo.CaseSensitive")));
			this.dtmFromDate.MaskInfo.CopyWithLiterals = ((bool)(resources.GetObject("dtmFromDate.MaskInfo.CopyWithLiterals")));
			this.dtmFromDate.MaskInfo.EditMask = resources.GetString("dtmFromDate.MaskInfo.EditMask");
			this.dtmFromDate.MaskInfo.EmptyAsNull = ((bool)(resources.GetObject("dtmFromDate.MaskInfo.EmptyAsNull")));
			this.dtmFromDate.MaskInfo.ErrorMessage = resources.GetString("dtmFromDate.MaskInfo.ErrorMessage");
			this.dtmFromDate.MaskInfo.Inherit = ((C1.Win.C1Input.MaskInfoInheritFlags)(resources.GetObject("dtmFromDate.MaskInfo.Inherit")));
			this.dtmFromDate.MaskInfo.PromptChar = ((char)(resources.GetObject("dtmFromDate.MaskInfo.PromptChar")));
			this.dtmFromDate.MaskInfo.ShowLiterals = ((C1.Win.C1Input.ShowLiteralsEnum)(resources.GetObject("dtmFromDate.MaskInfo.ShowLiterals")));
			this.dtmFromDate.MaskInfo.StoredEmptyChar = ((char)(resources.GetObject("dtmFromDate.MaskInfo.StoredEmptyChar")));
			this.dtmFromDate.MaxLength = ((int)(resources.GetObject("dtmFromDate.MaxLength")));
			this.dtmFromDate.Name = "dtmFromDate";
			this.dtmFromDate.NullText = resources.GetString("dtmFromDate.NullText");
			this.dtmFromDate.ParseInfo.CaseSensitive = ((bool)(resources.GetObject("dtmFromDate.ParseInfo.CaseSensitive")));
			this.dtmFromDate.ParseInfo.CustomFormat = resources.GetString("dtmFromDate.ParseInfo.CustomFormat");
			this.dtmFromDate.ParseInfo.DateTimeStyle = ((C1.Win.C1Input.DateTimeStyleFlags)(resources.GetObject("dtmFromDate.ParseInfo.DateTimeStyle")));
			this.dtmFromDate.ParseInfo.EmptyAsNull = ((bool)(resources.GetObject("dtmFromDate.ParseInfo.EmptyAsNull")));
			this.dtmFromDate.ParseInfo.ErrorMessage = resources.GetString("dtmFromDate.ParseInfo.ErrorMessage");
			this.dtmFromDate.ParseInfo.FormatType = ((C1.Win.C1Input.FormatTypeEnum)(resources.GetObject("dtmFromDate.ParseInfo.FormatType")));
			this.dtmFromDate.ParseInfo.Inherit = ((C1.Win.C1Input.ParseInfoInheritFlags)(resources.GetObject("dtmFromDate.ParseInfo.Inherit")));
			this.dtmFromDate.ParseInfo.NullText = resources.GetString("dtmFromDate.ParseInfo.NullText");
			this.dtmFromDate.ParseInfo.TrimEnd = ((bool)(resources.GetObject("dtmFromDate.ParseInfo.TrimEnd")));
			this.dtmFromDate.ParseInfo.TrimStart = ((bool)(resources.GetObject("dtmFromDate.ParseInfo.TrimStart")));
			this.dtmFromDate.PasswordChar = ((char)(resources.GetObject("dtmFromDate.PasswordChar")));
			this.dtmFromDate.PostValidation.CaseSensitive = ((bool)(resources.GetObject("dtmFromDate.PostValidation.CaseSensitive")));
			this.dtmFromDate.PostValidation.ErrorMessage = resources.GetString("dtmFromDate.PostValidation.ErrorMessage");
			this.dtmFromDate.PostValidation.Inherit = ((C1.Win.C1Input.PostValidationInheritFlags)(resources.GetObject("dtmFromDate.PostValidation.Inherit")));
			this.dtmFromDate.PostValidation.Validation = ((C1.Win.C1Input.PostValidationTypeEnum)(resources.GetObject("dtmFromDate.PostValidation.Validation")));
			this.dtmFromDate.PostValidation.Values = ((System.Array)(resources.GetObject("dtmFromDate.PostValidation.Values")));
			this.dtmFromDate.PostValidation.ValuesExcluded = ((System.Array)(resources.GetObject("dtmFromDate.PostValidation.ValuesExcluded")));
			this.dtmFromDate.PreValidation.CaseSensitive = ((bool)(resources.GetObject("dtmFromDate.PreValidation.CaseSensitive")));
			this.dtmFromDate.PreValidation.ErrorMessage = resources.GetString("dtmFromDate.PreValidation.ErrorMessage");
			this.dtmFromDate.PreValidation.Inherit = ((C1.Win.C1Input.PreValidationInheritFlags)(resources.GetObject("dtmFromDate.PreValidation.Inherit")));
			this.dtmFromDate.PreValidation.ItemSeparator = resources.GetString("dtmFromDate.PreValidation.ItemSeparator");
			this.dtmFromDate.PreValidation.PatternString = resources.GetString("dtmFromDate.PreValidation.PatternString");
			this.dtmFromDate.PreValidation.RegexOptions = ((C1.Win.C1Input.RegexOptionFlags)(resources.GetObject("dtmFromDate.PreValidation.RegexOptions")));
			this.dtmFromDate.PreValidation.TrimEnd = ((bool)(resources.GetObject("dtmFromDate.PreValidation.TrimEnd")));
			this.dtmFromDate.PreValidation.TrimStart = ((bool)(resources.GetObject("dtmFromDate.PreValidation.TrimStart")));
			this.dtmFromDate.PreValidation.Validation = ((C1.Win.C1Input.PreValidationTypeEnum)(resources.GetObject("dtmFromDate.PreValidation.Validation")));
			this.dtmFromDate.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("dtmFromDate.RightToLeft")));
			this.dtmFromDate.ShowFocusRectangle = ((bool)(resources.GetObject("dtmFromDate.ShowFocusRectangle")));
			this.dtmFromDate.Size = ((System.Drawing.Size)(resources.GetObject("dtmFromDate.Size")));
			this.dtmFromDate.TabIndex = ((int)(resources.GetObject("dtmFromDate.TabIndex")));
			this.dtmFromDate.Tag = ((object)(resources.GetObject("dtmFromDate.Tag")));
			this.dtmFromDate.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("dtmFromDate.TextAlign")));
			this.dtmFromDate.TrimEnd = ((bool)(resources.GetObject("dtmFromDate.TrimEnd")));
			this.dtmFromDate.TrimStart = ((bool)(resources.GetObject("dtmFromDate.TrimStart")));
			this.dtmFromDate.UserCultureOverride = ((bool)(resources.GetObject("dtmFromDate.UserCultureOverride")));
			this.dtmFromDate.Value = ((object)(resources.GetObject("dtmFromDate.Value")));
			this.dtmFromDate.VerticalAlign = ((C1.Win.C1Input.VerticalAlignEnum)(resources.GetObject("dtmFromDate.VerticalAlign")));
			this.dtmFromDate.Visible = ((bool)(resources.GetObject("dtmFromDate.Visible")));
			this.dtmFromDate.VisibleButtons = ((C1.Win.C1Input.DropDownControlButtonFlags)(resources.GetObject("dtmFromDate.VisibleButtons")));
			this.dtmFromDate.Validating += new System.ComponentModel.CancelEventHandler(this.dtmFromDate_Validating);
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
			// chkIgnoreMoveTime
			// 
			this.chkIgnoreMoveTime.AccessibleDescription = resources.GetString("chkIgnoreMoveTime.AccessibleDescription");
			this.chkIgnoreMoveTime.AccessibleName = resources.GetString("chkIgnoreMoveTime.AccessibleName");
			this.chkIgnoreMoveTime.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("chkIgnoreMoveTime.Anchor")));
			this.chkIgnoreMoveTime.Appearance = ((System.Windows.Forms.Appearance)(resources.GetObject("chkIgnoreMoveTime.Appearance")));
			this.chkIgnoreMoveTime.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("chkIgnoreMoveTime.BackgroundImage")));
			this.chkIgnoreMoveTime.CheckAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("chkIgnoreMoveTime.CheckAlign")));
			this.chkIgnoreMoveTime.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("chkIgnoreMoveTime.Dock")));
			this.chkIgnoreMoveTime.Enabled = ((bool)(resources.GetObject("chkIgnoreMoveTime.Enabled")));
			this.chkIgnoreMoveTime.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("chkIgnoreMoveTime.FlatStyle")));
			this.chkIgnoreMoveTime.Font = ((System.Drawing.Font)(resources.GetObject("chkIgnoreMoveTime.Font")));
			this.chkIgnoreMoveTime.Image = ((System.Drawing.Image)(resources.GetObject("chkIgnoreMoveTime.Image")));
			this.chkIgnoreMoveTime.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("chkIgnoreMoveTime.ImageAlign")));
			this.chkIgnoreMoveTime.ImageIndex = ((int)(resources.GetObject("chkIgnoreMoveTime.ImageIndex")));
			this.chkIgnoreMoveTime.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("chkIgnoreMoveTime.ImeMode")));
			this.chkIgnoreMoveTime.Location = ((System.Drawing.Point)(resources.GetObject("chkIgnoreMoveTime.Location")));
			this.chkIgnoreMoveTime.Name = "chkIgnoreMoveTime";
			this.chkIgnoreMoveTime.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("chkIgnoreMoveTime.RightToLeft")));
			this.chkIgnoreMoveTime.Size = ((System.Drawing.Size)(resources.GetObject("chkIgnoreMoveTime.Size")));
			this.chkIgnoreMoveTime.TabIndex = ((int)(resources.GetObject("chkIgnoreMoveTime.TabIndex")));
			this.chkIgnoreMoveTime.Text = resources.GetString("chkIgnoreMoveTime.Text");
			this.chkIgnoreMoveTime.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("chkIgnoreMoveTime.TextAlign")));
			this.chkIgnoreMoveTime.Visible = ((bool)(resources.GetObject("chkIgnoreMoveTime.Visible")));
			// 
			// txtCycle
			// 
			this.txtCycle.AccessibleDescription = resources.GetString("txtCycle.AccessibleDescription");
			this.txtCycle.AccessibleName = resources.GetString("txtCycle.AccessibleName");
			this.txtCycle.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("txtCycle.Anchor")));
			this.txtCycle.AutoSize = ((bool)(resources.GetObject("txtCycle.AutoSize")));
			this.txtCycle.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("txtCycle.BackgroundImage")));
			this.txtCycle.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("txtCycle.Dock")));
			this.txtCycle.Enabled = ((bool)(resources.GetObject("txtCycle.Enabled")));
			this.txtCycle.Font = ((System.Drawing.Font)(resources.GetObject("txtCycle.Font")));
			this.txtCycle.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("txtCycle.ImeMode")));
			this.txtCycle.Location = ((System.Drawing.Point)(resources.GetObject("txtCycle.Location")));
			this.txtCycle.MaxLength = ((int)(resources.GetObject("txtCycle.MaxLength")));
			this.txtCycle.Multiline = ((bool)(resources.GetObject("txtCycle.Multiline")));
			this.txtCycle.Name = "txtCycle";
			this.txtCycle.PasswordChar = ((char)(resources.GetObject("txtCycle.PasswordChar")));
			this.txtCycle.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("txtCycle.RightToLeft")));
			this.txtCycle.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("txtCycle.ScrollBars")));
			this.txtCycle.Size = ((System.Drawing.Size)(resources.GetObject("txtCycle.Size")));
			this.txtCycle.TabIndex = ((int)(resources.GetObject("txtCycle.TabIndex")));
			this.txtCycle.Text = resources.GetString("txtCycle.Text");
			this.txtCycle.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("txtCycle.TextAlign")));
			this.txtCycle.Visible = ((bool)(resources.GetObject("txtCycle.Visible")));
			this.txtCycle.WordWrap = ((bool)(resources.GetObject("txtCycle.WordWrap")));
			this.txtCycle.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCycle_KeyDown);
			this.txtCycle.Validating += new System.ComponentModel.CancelEventHandler(this.txtCycle_Validating);
			// 
			// btnSearchCycle
			// 
			this.btnSearchCycle.AccessibleDescription = resources.GetString("btnSearchCycle.AccessibleDescription");
			this.btnSearchCycle.AccessibleName = resources.GetString("btnSearchCycle.AccessibleName");
			this.btnSearchCycle.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnSearchCycle.Anchor")));
			this.btnSearchCycle.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSearchCycle.BackgroundImage")));
			this.btnSearchCycle.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnSearchCycle.Dock")));
			this.btnSearchCycle.Enabled = ((bool)(resources.GetObject("btnSearchCycle.Enabled")));
			this.btnSearchCycle.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnSearchCycle.FlatStyle")));
			this.btnSearchCycle.Font = ((System.Drawing.Font)(resources.GetObject("btnSearchCycle.Font")));
			this.btnSearchCycle.Image = ((System.Drawing.Image)(resources.GetObject("btnSearchCycle.Image")));
			this.btnSearchCycle.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnSearchCycle.ImageAlign")));
			this.btnSearchCycle.ImageIndex = ((int)(resources.GetObject("btnSearchCycle.ImageIndex")));
			this.btnSearchCycle.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnSearchCycle.ImeMode")));
			this.btnSearchCycle.Location = ((System.Drawing.Point)(resources.GetObject("btnSearchCycle.Location")));
			this.btnSearchCycle.Name = "btnSearchCycle";
			this.btnSearchCycle.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnSearchCycle.RightToLeft")));
			this.btnSearchCycle.Size = ((System.Drawing.Size)(resources.GetObject("btnSearchCycle.Size")));
			this.btnSearchCycle.TabIndex = ((int)(resources.GetObject("btnSearchCycle.TabIndex")));
			this.btnSearchCycle.Text = resources.GetString("btnSearchCycle.Text");
			this.btnSearchCycle.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnSearchCycle.TextAlign")));
			this.btnSearchCycle.Visible = ((bool)(resources.GetObject("btnSearchCycle.Visible")));
			this.btnSearchCycle.Click += new System.EventHandler(this.btnSearchCycle_Click);
			// 
			// btnMPSCycle
			// 
			this.btnMPSCycle.AccessibleDescription = resources.GetString("btnMPSCycle.AccessibleDescription");
			this.btnMPSCycle.AccessibleName = resources.GetString("btnMPSCycle.AccessibleName");
			this.btnMPSCycle.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnMPSCycle.Anchor")));
			this.btnMPSCycle.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnMPSCycle.BackgroundImage")));
			this.btnMPSCycle.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnMPSCycle.Dock")));
			this.btnMPSCycle.Enabled = ((bool)(resources.GetObject("btnMPSCycle.Enabled")));
			this.btnMPSCycle.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnMPSCycle.FlatStyle")));
			this.btnMPSCycle.Font = ((System.Drawing.Font)(resources.GetObject("btnMPSCycle.Font")));
			this.btnMPSCycle.Image = ((System.Drawing.Image)(resources.GetObject("btnMPSCycle.Image")));
			this.btnMPSCycle.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnMPSCycle.ImageAlign")));
			this.btnMPSCycle.ImageIndex = ((int)(resources.GetObject("btnMPSCycle.ImageIndex")));
			this.btnMPSCycle.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnMPSCycle.ImeMode")));
			this.btnMPSCycle.Location = ((System.Drawing.Point)(resources.GetObject("btnMPSCycle.Location")));
			this.btnMPSCycle.Name = "btnMPSCycle";
			this.btnMPSCycle.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnMPSCycle.RightToLeft")));
			this.btnMPSCycle.Size = ((System.Drawing.Size)(resources.GetObject("btnMPSCycle.Size")));
			this.btnMPSCycle.TabIndex = ((int)(resources.GetObject("btnMPSCycle.TabIndex")));
			this.btnMPSCycle.Text = resources.GetString("btnMPSCycle.Text");
			this.btnMPSCycle.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnMPSCycle.TextAlign")));
			this.btnMPSCycle.Visible = ((bool)(resources.GetObject("btnMPSCycle.Visible")));
			this.btnMPSCycle.Click += new System.EventHandler(this.btnMPSCycle_Click);
			// 
			// txtMPSCycle
			// 
			this.txtMPSCycle.AccessibleDescription = resources.GetString("txtMPSCycle.AccessibleDescription");
			this.txtMPSCycle.AccessibleName = resources.GetString("txtMPSCycle.AccessibleName");
			this.txtMPSCycle.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("txtMPSCycle.Anchor")));
			this.txtMPSCycle.AutoSize = ((bool)(resources.GetObject("txtMPSCycle.AutoSize")));
			this.txtMPSCycle.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("txtMPSCycle.BackgroundImage")));
			this.txtMPSCycle.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("txtMPSCycle.Dock")));
			this.txtMPSCycle.Enabled = ((bool)(resources.GetObject("txtMPSCycle.Enabled")));
			this.txtMPSCycle.Font = ((System.Drawing.Font)(resources.GetObject("txtMPSCycle.Font")));
			this.txtMPSCycle.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("txtMPSCycle.ImeMode")));
			this.txtMPSCycle.Location = ((System.Drawing.Point)(resources.GetObject("txtMPSCycle.Location")));
			this.txtMPSCycle.MaxLength = ((int)(resources.GetObject("txtMPSCycle.MaxLength")));
			this.txtMPSCycle.Multiline = ((bool)(resources.GetObject("txtMPSCycle.Multiline")));
			this.txtMPSCycle.Name = "txtMPSCycle";
			this.txtMPSCycle.PasswordChar = ((char)(resources.GetObject("txtMPSCycle.PasswordChar")));
			this.txtMPSCycle.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("txtMPSCycle.RightToLeft")));
			this.txtMPSCycle.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("txtMPSCycle.ScrollBars")));
			this.txtMPSCycle.Size = ((System.Drawing.Size)(resources.GetObject("txtMPSCycle.Size")));
			this.txtMPSCycle.TabIndex = ((int)(resources.GetObject("txtMPSCycle.TabIndex")));
			this.txtMPSCycle.Text = resources.GetString("txtMPSCycle.Text");
			this.txtMPSCycle.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("txtMPSCycle.TextAlign")));
			this.txtMPSCycle.Visible = ((bool)(resources.GetObject("txtMPSCycle.Visible")));
			this.txtMPSCycle.WordWrap = ((bool)(resources.GetObject("txtMPSCycle.WordWrap")));
			this.txtMPSCycle.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtMPSCycle_KeyDown);
			this.txtMPSCycle.Validating += new System.ComponentModel.CancelEventHandler(this.txtMPSCycle_Validating);
			// 
			// dtmLastUpdate
			// 
			this.dtmLastUpdate.AcceptsEscape = ((bool)(resources.GetObject("dtmLastUpdate.AcceptsEscape")));
			this.dtmLastUpdate.AccessibleDescription = resources.GetString("dtmLastUpdate.AccessibleDescription");
			this.dtmLastUpdate.AccessibleName = resources.GetString("dtmLastUpdate.AccessibleName");
			this.dtmLastUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("dtmLastUpdate.Anchor")));
			this.dtmLastUpdate.AutoSize = ((bool)(resources.GetObject("dtmLastUpdate.AutoSize")));
			this.dtmLastUpdate.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("dtmLastUpdate.BackgroundImage")));
			this.dtmLastUpdate.BorderStyle = ((System.Windows.Forms.BorderStyle)(resources.GetObject("dtmLastUpdate.BorderStyle")));
			// 
			// dtmLastUpdate.Calendar
			// 
			this.dtmLastUpdate.Calendar.AccessibleDescription = resources.GetString("dtmLastUpdate.Calendar.AccessibleDescription");
			this.dtmLastUpdate.Calendar.AccessibleName = resources.GetString("dtmLastUpdate.Calendar.AccessibleName");
			this.dtmLastUpdate.Calendar.AnnuallyBoldedDates = ((System.DateTime[])(resources.GetObject("dtmLastUpdate.Calendar.AnnuallyBoldedDates")));
			this.dtmLastUpdate.Calendar.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("dtmLastUpdate.Calendar.BackgroundImage")));
			this.dtmLastUpdate.Calendar.BoldedDates = ((System.DateTime[])(resources.GetObject("dtmLastUpdate.Calendar.BoldedDates")));
			this.dtmLastUpdate.Calendar.CalendarDimensions = ((System.Drawing.Size)(resources.GetObject("dtmLastUpdate.Calendar.CalendarDimensions")));
			this.dtmLastUpdate.Calendar.Enabled = ((bool)(resources.GetObject("dtmLastUpdate.Calendar.Enabled")));
			this.dtmLastUpdate.Calendar.FirstDayOfWeek = ((System.Windows.Forms.Day)(resources.GetObject("dtmLastUpdate.Calendar.FirstDayOfWeek")));
			this.dtmLastUpdate.Calendar.Font = ((System.Drawing.Font)(resources.GetObject("dtmLastUpdate.Calendar.Font")));
			this.dtmLastUpdate.Calendar.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("dtmLastUpdate.Calendar.ImeMode")));
			this.dtmLastUpdate.Calendar.MonthlyBoldedDates = ((System.DateTime[])(resources.GetObject("dtmLastUpdate.Calendar.MonthlyBoldedDates")));
			this.dtmLastUpdate.Calendar.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("dtmLastUpdate.Calendar.RightToLeft")));
			this.dtmLastUpdate.Calendar.ShowClearButton = ((bool)(resources.GetObject("dtmLastUpdate.Calendar.ShowClearButton")));
			this.dtmLastUpdate.Calendar.ShowTodayButton = ((bool)(resources.GetObject("dtmLastUpdate.Calendar.ShowTodayButton")));
			this.dtmLastUpdate.Calendar.ShowWeekNumbers = ((bool)(resources.GetObject("dtmLastUpdate.Calendar.ShowWeekNumbers")));
			this.dtmLastUpdate.CaseSensitive = ((bool)(resources.GetObject("dtmLastUpdate.CaseSensitive")));
			this.dtmLastUpdate.Culture = ((int)(resources.GetObject("dtmLastUpdate.Culture")));
			this.dtmLastUpdate.CurrentTimeZone = ((bool)(resources.GetObject("dtmLastUpdate.CurrentTimeZone")));
			this.dtmLastUpdate.CustomFormat = resources.GetString("dtmLastUpdate.CustomFormat");
			this.dtmLastUpdate.DaylightTimeAdjustment = ((C1.Win.C1Input.DaylightTimeAdjustmentEnum)(resources.GetObject("dtmLastUpdate.DaylightTimeAdjustment")));
			this.dtmLastUpdate.DisplayFormat.CustomFormat = resources.GetString("dtmLastUpdate.DisplayFormat.CustomFormat");
			this.dtmLastUpdate.DisplayFormat.FormatType = ((C1.Win.C1Input.FormatTypeEnum)(resources.GetObject("dtmLastUpdate.DisplayFormat.FormatType")));
			this.dtmLastUpdate.DisplayFormat.Inherit = ((C1.Win.C1Input.FormatInfoInheritFlags)(resources.GetObject("dtmLastUpdate.DisplayFormat.Inherit")));
			this.dtmLastUpdate.DisplayFormat.NullText = resources.GetString("dtmLastUpdate.DisplayFormat.NullText");
			this.dtmLastUpdate.DisplayFormat.TrimEnd = ((bool)(resources.GetObject("dtmLastUpdate.DisplayFormat.TrimEnd")));
			this.dtmLastUpdate.DisplayFormat.TrimStart = ((bool)(resources.GetObject("dtmLastUpdate.DisplayFormat.TrimStart")));
			this.dtmLastUpdate.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("dtmLastUpdate.Dock")));
			this.dtmLastUpdate.DropDownFormAlign = ((C1.Win.C1Input.DropDownFormAlignmentEnum)(resources.GetObject("dtmLastUpdate.DropDownFormAlign")));
			this.dtmLastUpdate.EditFormat.CustomFormat = resources.GetString("dtmLastUpdate.EditFormat.CustomFormat");
			this.dtmLastUpdate.EditFormat.FormatType = ((C1.Win.C1Input.FormatTypeEnum)(resources.GetObject("dtmLastUpdate.EditFormat.FormatType")));
			this.dtmLastUpdate.EditFormat.Inherit = ((C1.Win.C1Input.FormatInfoInheritFlags)(resources.GetObject("dtmLastUpdate.EditFormat.Inherit")));
			this.dtmLastUpdate.EditFormat.NullText = resources.GetString("dtmLastUpdate.EditFormat.NullText");
			this.dtmLastUpdate.EditFormat.TrimEnd = ((bool)(resources.GetObject("dtmLastUpdate.EditFormat.TrimEnd")));
			this.dtmLastUpdate.EditFormat.TrimStart = ((bool)(resources.GetObject("dtmLastUpdate.EditFormat.TrimStart")));
			this.dtmLastUpdate.EditMask = resources.GetString("dtmLastUpdate.EditMask");
			this.dtmLastUpdate.EmptyAsNull = ((bool)(resources.GetObject("dtmLastUpdate.EmptyAsNull")));
			this.dtmLastUpdate.Enabled = ((bool)(resources.GetObject("dtmLastUpdate.Enabled")));
			this.dtmLastUpdate.ErrorInfo.BeepOnError = ((bool)(resources.GetObject("dtmLastUpdate.ErrorInfo.BeepOnError")));
			this.dtmLastUpdate.ErrorInfo.ErrorMessage = resources.GetString("dtmLastUpdate.ErrorInfo.ErrorMessage");
			this.dtmLastUpdate.ErrorInfo.ErrorMessageCaption = resources.GetString("dtmLastUpdate.ErrorInfo.ErrorMessageCaption");
			this.dtmLastUpdate.ErrorInfo.ShowErrorMessage = ((bool)(resources.GetObject("dtmLastUpdate.ErrorInfo.ShowErrorMessage")));
			this.dtmLastUpdate.ErrorInfo.ValueOnError = ((object)(resources.GetObject("dtmLastUpdate.ErrorInfo.ValueOnError")));
			this.dtmLastUpdate.Font = ((System.Drawing.Font)(resources.GetObject("dtmLastUpdate.Font")));
			this.dtmLastUpdate.FormatType = ((C1.Win.C1Input.FormatTypeEnum)(resources.GetObject("dtmLastUpdate.FormatType")));
			this.dtmLastUpdate.GapHeight = ((int)(resources.GetObject("dtmLastUpdate.GapHeight")));
			this.dtmLastUpdate.GMTOffset = ((System.TimeSpan)(resources.GetObject("dtmLastUpdate.GMTOffset")));
			this.dtmLastUpdate.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("dtmLastUpdate.ImeMode")));
			this.dtmLastUpdate.InitialSelection = ((C1.Win.C1Input.InitialSelectionEnum)(resources.GetObject("dtmLastUpdate.InitialSelection")));
			this.dtmLastUpdate.Location = ((System.Drawing.Point)(resources.GetObject("dtmLastUpdate.Location")));
			this.dtmLastUpdate.MaskInfo.AutoTabWhenFilled = ((bool)(resources.GetObject("dtmLastUpdate.MaskInfo.AutoTabWhenFilled")));
			this.dtmLastUpdate.MaskInfo.CaseSensitive = ((bool)(resources.GetObject("dtmLastUpdate.MaskInfo.CaseSensitive")));
			this.dtmLastUpdate.MaskInfo.CopyWithLiterals = ((bool)(resources.GetObject("dtmLastUpdate.MaskInfo.CopyWithLiterals")));
			this.dtmLastUpdate.MaskInfo.EditMask = resources.GetString("dtmLastUpdate.MaskInfo.EditMask");
			this.dtmLastUpdate.MaskInfo.EmptyAsNull = ((bool)(resources.GetObject("dtmLastUpdate.MaskInfo.EmptyAsNull")));
			this.dtmLastUpdate.MaskInfo.ErrorMessage = resources.GetString("dtmLastUpdate.MaskInfo.ErrorMessage");
			this.dtmLastUpdate.MaskInfo.Inherit = ((C1.Win.C1Input.MaskInfoInheritFlags)(resources.GetObject("dtmLastUpdate.MaskInfo.Inherit")));
			this.dtmLastUpdate.MaskInfo.PromptChar = ((char)(resources.GetObject("dtmLastUpdate.MaskInfo.PromptChar")));
			this.dtmLastUpdate.MaskInfo.ShowLiterals = ((C1.Win.C1Input.ShowLiteralsEnum)(resources.GetObject("dtmLastUpdate.MaskInfo.ShowLiterals")));
			this.dtmLastUpdate.MaskInfo.StoredEmptyChar = ((char)(resources.GetObject("dtmLastUpdate.MaskInfo.StoredEmptyChar")));
			this.dtmLastUpdate.MaxLength = ((int)(resources.GetObject("dtmLastUpdate.MaxLength")));
			this.dtmLastUpdate.Name = "dtmLastUpdate";
			this.dtmLastUpdate.NullText = resources.GetString("dtmLastUpdate.NullText");
			this.dtmLastUpdate.ParseInfo.CaseSensitive = ((bool)(resources.GetObject("dtmLastUpdate.ParseInfo.CaseSensitive")));
			this.dtmLastUpdate.ParseInfo.CustomFormat = resources.GetString("dtmLastUpdate.ParseInfo.CustomFormat");
			this.dtmLastUpdate.ParseInfo.DateTimeStyle = ((C1.Win.C1Input.DateTimeStyleFlags)(resources.GetObject("dtmLastUpdate.ParseInfo.DateTimeStyle")));
			this.dtmLastUpdate.ParseInfo.EmptyAsNull = ((bool)(resources.GetObject("dtmLastUpdate.ParseInfo.EmptyAsNull")));
			this.dtmLastUpdate.ParseInfo.ErrorMessage = resources.GetString("dtmLastUpdate.ParseInfo.ErrorMessage");
			this.dtmLastUpdate.ParseInfo.FormatType = ((C1.Win.C1Input.FormatTypeEnum)(resources.GetObject("dtmLastUpdate.ParseInfo.FormatType")));
			this.dtmLastUpdate.ParseInfo.Inherit = ((C1.Win.C1Input.ParseInfoInheritFlags)(resources.GetObject("dtmLastUpdate.ParseInfo.Inherit")));
			this.dtmLastUpdate.ParseInfo.NullText = resources.GetString("dtmLastUpdate.ParseInfo.NullText");
			this.dtmLastUpdate.ParseInfo.TrimEnd = ((bool)(resources.GetObject("dtmLastUpdate.ParseInfo.TrimEnd")));
			this.dtmLastUpdate.ParseInfo.TrimStart = ((bool)(resources.GetObject("dtmLastUpdate.ParseInfo.TrimStart")));
			this.dtmLastUpdate.PasswordChar = ((char)(resources.GetObject("dtmLastUpdate.PasswordChar")));
			this.dtmLastUpdate.PostValidation.CaseSensitive = ((bool)(resources.GetObject("dtmLastUpdate.PostValidation.CaseSensitive")));
			this.dtmLastUpdate.PostValidation.ErrorMessage = resources.GetString("dtmLastUpdate.PostValidation.ErrorMessage");
			this.dtmLastUpdate.PostValidation.Inherit = ((C1.Win.C1Input.PostValidationInheritFlags)(resources.GetObject("dtmLastUpdate.PostValidation.Inherit")));
			this.dtmLastUpdate.PostValidation.Validation = ((C1.Win.C1Input.PostValidationTypeEnum)(resources.GetObject("dtmLastUpdate.PostValidation.Validation")));
			this.dtmLastUpdate.PostValidation.Values = ((System.Array)(resources.GetObject("dtmLastUpdate.PostValidation.Values")));
			this.dtmLastUpdate.PostValidation.ValuesExcluded = ((System.Array)(resources.GetObject("dtmLastUpdate.PostValidation.ValuesExcluded")));
			this.dtmLastUpdate.PreValidation.CaseSensitive = ((bool)(resources.GetObject("dtmLastUpdate.PreValidation.CaseSensitive")));
			this.dtmLastUpdate.PreValidation.ErrorMessage = resources.GetString("dtmLastUpdate.PreValidation.ErrorMessage");
			this.dtmLastUpdate.PreValidation.Inherit = ((C1.Win.C1Input.PreValidationInheritFlags)(resources.GetObject("dtmLastUpdate.PreValidation.Inherit")));
			this.dtmLastUpdate.PreValidation.ItemSeparator = resources.GetString("dtmLastUpdate.PreValidation.ItemSeparator");
			this.dtmLastUpdate.PreValidation.PatternString = resources.GetString("dtmLastUpdate.PreValidation.PatternString");
			this.dtmLastUpdate.PreValidation.RegexOptions = ((C1.Win.C1Input.RegexOptionFlags)(resources.GetObject("dtmLastUpdate.PreValidation.RegexOptions")));
			this.dtmLastUpdate.PreValidation.TrimEnd = ((bool)(resources.GetObject("dtmLastUpdate.PreValidation.TrimEnd")));
			this.dtmLastUpdate.PreValidation.TrimStart = ((bool)(resources.GetObject("dtmLastUpdate.PreValidation.TrimStart")));
			this.dtmLastUpdate.PreValidation.Validation = ((C1.Win.C1Input.PreValidationTypeEnum)(resources.GetObject("dtmLastUpdate.PreValidation.Validation")));
			this.dtmLastUpdate.ReadOnly = true;
			this.dtmLastUpdate.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("dtmLastUpdate.RightToLeft")));
			this.dtmLastUpdate.ShowFocusRectangle = ((bool)(resources.GetObject("dtmLastUpdate.ShowFocusRectangle")));
			this.dtmLastUpdate.Size = ((System.Drawing.Size)(resources.GetObject("dtmLastUpdate.Size")));
			this.dtmLastUpdate.TabIndex = ((int)(resources.GetObject("dtmLastUpdate.TabIndex")));
			this.dtmLastUpdate.Tag = ((object)(resources.GetObject("dtmLastUpdate.Tag")));
			this.dtmLastUpdate.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("dtmLastUpdate.TextAlign")));
			this.dtmLastUpdate.TrimEnd = ((bool)(resources.GetObject("dtmLastUpdate.TrimEnd")));
			this.dtmLastUpdate.TrimStart = ((bool)(resources.GetObject("dtmLastUpdate.TrimStart")));
			this.dtmLastUpdate.UserCultureOverride = ((bool)(resources.GetObject("dtmLastUpdate.UserCultureOverride")));
			this.dtmLastUpdate.Value = ((object)(resources.GetObject("dtmLastUpdate.Value")));
			this.dtmLastUpdate.VerticalAlign = ((C1.Win.C1Input.VerticalAlignEnum)(resources.GetObject("dtmLastUpdate.VerticalAlign")));
			this.dtmLastUpdate.Visible = ((bool)(resources.GetObject("dtmLastUpdate.Visible")));
			this.dtmLastUpdate.VisibleButtons = ((C1.Win.C1Input.DropDownControlButtonFlags)(resources.GetObject("dtmLastUpdate.VisibleButtons")));
			// 
			// lblScheduleType
			// 
			this.lblScheduleType.AccessibleDescription = resources.GetString("lblScheduleType.AccessibleDescription");
			this.lblScheduleType.AccessibleName = resources.GetString("lblScheduleType.AccessibleName");
			this.lblScheduleType.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblScheduleType.Anchor")));
			this.lblScheduleType.AutoSize = ((bool)(resources.GetObject("lblScheduleType.AutoSize")));
			this.lblScheduleType.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblScheduleType.Dock")));
			this.lblScheduleType.Enabled = ((bool)(resources.GetObject("lblScheduleType.Enabled")));
			this.lblScheduleType.Font = ((System.Drawing.Font)(resources.GetObject("lblScheduleType.Font")));
			this.lblScheduleType.ForeColor = System.Drawing.Color.Maroon;
			this.lblScheduleType.Image = ((System.Drawing.Image)(resources.GetObject("lblScheduleType.Image")));
			this.lblScheduleType.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblScheduleType.ImageAlign")));
			this.lblScheduleType.ImageIndex = ((int)(resources.GetObject("lblScheduleType.ImageIndex")));
			this.lblScheduleType.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblScheduleType.ImeMode")));
			this.lblScheduleType.Location = ((System.Drawing.Point)(resources.GetObject("lblScheduleType.Location")));
			this.lblScheduleType.Name = "lblScheduleType";
			this.lblScheduleType.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblScheduleType.RightToLeft")));
			this.lblScheduleType.Size = ((System.Drawing.Size)(resources.GetObject("lblScheduleType.Size")));
			this.lblScheduleType.TabIndex = ((int)(resources.GetObject("lblScheduleType.TabIndex")));
			this.lblScheduleType.Text = resources.GetString("lblScheduleType.Text");
			this.lblScheduleType.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblScheduleType.TextAlign")));
			this.lblScheduleType.Visible = ((bool)(resources.GetObject("lblScheduleType.Visible")));
			// 
			// lblInfiniteScheduling
			// 
			this.lblInfiniteScheduling.AccessibleDescription = resources.GetString("lblInfiniteScheduling.AccessibleDescription");
			this.lblInfiniteScheduling.AccessibleName = resources.GetString("lblInfiniteScheduling.AccessibleName");
			this.lblInfiniteScheduling.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblInfiniteScheduling.Anchor")));
			this.lblInfiniteScheduling.AutoSize = ((bool)(resources.GetObject("lblInfiniteScheduling.AutoSize")));
			this.lblInfiniteScheduling.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblInfiniteScheduling.Dock")));
			this.lblInfiniteScheduling.Enabled = ((bool)(resources.GetObject("lblInfiniteScheduling.Enabled")));
			this.lblInfiniteScheduling.Font = ((System.Drawing.Font)(resources.GetObject("lblInfiniteScheduling.Font")));
			this.lblInfiniteScheduling.Image = ((System.Drawing.Image)(resources.GetObject("lblInfiniteScheduling.Image")));
			this.lblInfiniteScheduling.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblInfiniteScheduling.ImageAlign")));
			this.lblInfiniteScheduling.ImageIndex = ((int)(resources.GetObject("lblInfiniteScheduling.ImageIndex")));
			this.lblInfiniteScheduling.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblInfiniteScheduling.ImeMode")));
			this.lblInfiniteScheduling.Location = ((System.Drawing.Point)(resources.GetObject("lblInfiniteScheduling.Location")));
			this.lblInfiniteScheduling.Name = "lblInfiniteScheduling";
			this.lblInfiniteScheduling.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblInfiniteScheduling.RightToLeft")));
			this.lblInfiniteScheduling.Size = ((System.Drawing.Size)(resources.GetObject("lblInfiniteScheduling.Size")));
			this.lblInfiniteScheduling.TabIndex = ((int)(resources.GetObject("lblInfiniteScheduling.TabIndex")));
			this.lblInfiniteScheduling.Text = resources.GetString("lblInfiniteScheduling.Text");
			this.lblInfiniteScheduling.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblInfiniteScheduling.TextAlign")));
			this.lblInfiniteScheduling.Visible = ((bool)(resources.GetObject("lblInfiniteScheduling.Visible")));
			// 
			// lblFiniteScheduling
			// 
			this.lblFiniteScheduling.AccessibleDescription = resources.GetString("lblFiniteScheduling.AccessibleDescription");
			this.lblFiniteScheduling.AccessibleName = resources.GetString("lblFiniteScheduling.AccessibleName");
			this.lblFiniteScheduling.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblFiniteScheduling.Anchor")));
			this.lblFiniteScheduling.AutoSize = ((bool)(resources.GetObject("lblFiniteScheduling.AutoSize")));
			this.lblFiniteScheduling.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblFiniteScheduling.Dock")));
			this.lblFiniteScheduling.Enabled = ((bool)(resources.GetObject("lblFiniteScheduling.Enabled")));
			this.lblFiniteScheduling.Font = ((System.Drawing.Font)(resources.GetObject("lblFiniteScheduling.Font")));
			this.lblFiniteScheduling.Image = ((System.Drawing.Image)(resources.GetObject("lblFiniteScheduling.Image")));
			this.lblFiniteScheduling.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblFiniteScheduling.ImageAlign")));
			this.lblFiniteScheduling.ImageIndex = ((int)(resources.GetObject("lblFiniteScheduling.ImageIndex")));
			this.lblFiniteScheduling.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblFiniteScheduling.ImeMode")));
			this.lblFiniteScheduling.Location = ((System.Drawing.Point)(resources.GetObject("lblFiniteScheduling.Location")));
			this.lblFiniteScheduling.Name = "lblFiniteScheduling";
			this.lblFiniteScheduling.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblFiniteScheduling.RightToLeft")));
			this.lblFiniteScheduling.Size = ((System.Drawing.Size)(resources.GetObject("lblFiniteScheduling.Size")));
			this.lblFiniteScheduling.TabIndex = ((int)(resources.GetObject("lblFiniteScheduling.TabIndex")));
			this.lblFiniteScheduling.Text = resources.GetString("lblFiniteScheduling.Text");
			this.lblFiniteScheduling.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblFiniteScheduling.TextAlign")));
			this.lblFiniteScheduling.Visible = ((bool)(resources.GetObject("lblFiniteScheduling.Visible")));
			// 
			// lblLoadAveraging
			// 
			this.lblLoadAveraging.AccessibleDescription = resources.GetString("lblLoadAveraging.AccessibleDescription");
			this.lblLoadAveraging.AccessibleName = resources.GetString("lblLoadAveraging.AccessibleName");
			this.lblLoadAveraging.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblLoadAveraging.Anchor")));
			this.lblLoadAveraging.AutoSize = ((bool)(resources.GetObject("lblLoadAveraging.AutoSize")));
			this.lblLoadAveraging.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblLoadAveraging.Dock")));
			this.lblLoadAveraging.Enabled = ((bool)(resources.GetObject("lblLoadAveraging.Enabled")));
			this.lblLoadAveraging.Font = ((System.Drawing.Font)(resources.GetObject("lblLoadAveraging.Font")));
			this.lblLoadAveraging.Image = ((System.Drawing.Image)(resources.GetObject("lblLoadAveraging.Image")));
			this.lblLoadAveraging.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblLoadAveraging.ImageAlign")));
			this.lblLoadAveraging.ImageIndex = ((int)(resources.GetObject("lblLoadAveraging.ImageIndex")));
			this.lblLoadAveraging.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblLoadAveraging.ImeMode")));
			this.lblLoadAveraging.Location = ((System.Drawing.Point)(resources.GetObject("lblLoadAveraging.Location")));
			this.lblLoadAveraging.Name = "lblLoadAveraging";
			this.lblLoadAveraging.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblLoadAveraging.RightToLeft")));
			this.lblLoadAveraging.Size = ((System.Drawing.Size)(resources.GetObject("lblLoadAveraging.Size")));
			this.lblLoadAveraging.TabIndex = ((int)(resources.GetObject("lblLoadAveraging.TabIndex")));
			this.lblLoadAveraging.Text = resources.GetString("lblLoadAveraging.Text");
			this.lblLoadAveraging.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblLoadAveraging.TextAlign")));
			this.lblLoadAveraging.Visible = ((bool)(resources.GetObject("lblLoadAveraging.Visible")));
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
			this.dgrdData.ButtonClick += new C1.Win.C1TrueDBGrid.ColEventHandler(this.dgrdData_ButtonClick);
			this.dgrdData.BeforeColUpdate += new C1.Win.C1TrueDBGrid.BeforeColUpdateEventHandler(this.dgrdData_BeforeColUpdate);
			this.dgrdData.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgrdData_KeyDown);
			this.dgrdData.PropBag = resources.GetString("dgrdData.PropBag");
			// 
			// dtmToDate
			// 
			this.dtmToDate.AcceptsEscape = ((bool)(resources.GetObject("dtmToDate.AcceptsEscape")));
			this.dtmToDate.AccessibleDescription = resources.GetString("dtmToDate.AccessibleDescription");
			this.dtmToDate.AccessibleName = resources.GetString("dtmToDate.AccessibleName");
			this.dtmToDate.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("dtmToDate.Anchor")));
			this.dtmToDate.AutoSize = ((bool)(resources.GetObject("dtmToDate.AutoSize")));
			this.dtmToDate.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("dtmToDate.BackgroundImage")));
			this.dtmToDate.BorderStyle = ((System.Windows.Forms.BorderStyle)(resources.GetObject("dtmToDate.BorderStyle")));
			// 
			// dtmToDate.Calendar
			// 
			this.dtmToDate.Calendar.AccessibleDescription = resources.GetString("dtmToDate.Calendar.AccessibleDescription");
			this.dtmToDate.Calendar.AccessibleName = resources.GetString("dtmToDate.Calendar.AccessibleName");
			this.dtmToDate.Calendar.AnnuallyBoldedDates = ((System.DateTime[])(resources.GetObject("dtmToDate.Calendar.AnnuallyBoldedDates")));
			this.dtmToDate.Calendar.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("dtmToDate.Calendar.BackgroundImage")));
			this.dtmToDate.Calendar.BoldedDates = ((System.DateTime[])(resources.GetObject("dtmToDate.Calendar.BoldedDates")));
			this.dtmToDate.Calendar.CalendarDimensions = ((System.Drawing.Size)(resources.GetObject("dtmToDate.Calendar.CalendarDimensions")));
			this.dtmToDate.Calendar.Enabled = ((bool)(resources.GetObject("dtmToDate.Calendar.Enabled")));
			this.dtmToDate.Calendar.FirstDayOfWeek = ((System.Windows.Forms.Day)(resources.GetObject("dtmToDate.Calendar.FirstDayOfWeek")));
			this.dtmToDate.Calendar.Font = ((System.Drawing.Font)(resources.GetObject("dtmToDate.Calendar.Font")));
			this.dtmToDate.Calendar.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("dtmToDate.Calendar.ImeMode")));
			this.dtmToDate.Calendar.MonthlyBoldedDates = ((System.DateTime[])(resources.GetObject("dtmToDate.Calendar.MonthlyBoldedDates")));
			this.dtmToDate.Calendar.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("dtmToDate.Calendar.RightToLeft")));
			this.dtmToDate.Calendar.ShowClearButton = ((bool)(resources.GetObject("dtmToDate.Calendar.ShowClearButton")));
			this.dtmToDate.Calendar.ShowTodayButton = ((bool)(resources.GetObject("dtmToDate.Calendar.ShowTodayButton")));
			this.dtmToDate.Calendar.ShowWeekNumbers = ((bool)(resources.GetObject("dtmToDate.Calendar.ShowWeekNumbers")));
			this.dtmToDate.CaseSensitive = ((bool)(resources.GetObject("dtmToDate.CaseSensitive")));
			this.dtmToDate.Culture = ((int)(resources.GetObject("dtmToDate.Culture")));
			this.dtmToDate.CurrentTimeZone = ((bool)(resources.GetObject("dtmToDate.CurrentTimeZone")));
			this.dtmToDate.CustomFormat = resources.GetString("dtmToDate.CustomFormat");
			this.dtmToDate.DaylightTimeAdjustment = ((C1.Win.C1Input.DaylightTimeAdjustmentEnum)(resources.GetObject("dtmToDate.DaylightTimeAdjustment")));
			this.dtmToDate.DisplayFormat.CustomFormat = resources.GetString("dtmToDate.DisplayFormat.CustomFormat");
			this.dtmToDate.DisplayFormat.FormatType = ((C1.Win.C1Input.FormatTypeEnum)(resources.GetObject("dtmToDate.DisplayFormat.FormatType")));
			this.dtmToDate.DisplayFormat.Inherit = ((C1.Win.C1Input.FormatInfoInheritFlags)(resources.GetObject("dtmToDate.DisplayFormat.Inherit")));
			this.dtmToDate.DisplayFormat.NullText = resources.GetString("dtmToDate.DisplayFormat.NullText");
			this.dtmToDate.DisplayFormat.TrimEnd = ((bool)(resources.GetObject("dtmToDate.DisplayFormat.TrimEnd")));
			this.dtmToDate.DisplayFormat.TrimStart = ((bool)(resources.GetObject("dtmToDate.DisplayFormat.TrimStart")));
			this.dtmToDate.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("dtmToDate.Dock")));
			this.dtmToDate.DropDownFormAlign = ((C1.Win.C1Input.DropDownFormAlignmentEnum)(resources.GetObject("dtmToDate.DropDownFormAlign")));
			this.dtmToDate.EditFormat.CustomFormat = resources.GetString("dtmToDate.EditFormat.CustomFormat");
			this.dtmToDate.EditFormat.FormatType = ((C1.Win.C1Input.FormatTypeEnum)(resources.GetObject("dtmToDate.EditFormat.FormatType")));
			this.dtmToDate.EditFormat.Inherit = ((C1.Win.C1Input.FormatInfoInheritFlags)(resources.GetObject("dtmToDate.EditFormat.Inherit")));
			this.dtmToDate.EditFormat.NullText = resources.GetString("dtmToDate.EditFormat.NullText");
			this.dtmToDate.EditFormat.TrimEnd = ((bool)(resources.GetObject("dtmToDate.EditFormat.TrimEnd")));
			this.dtmToDate.EditFormat.TrimStart = ((bool)(resources.GetObject("dtmToDate.EditFormat.TrimStart")));
			this.dtmToDate.EditMask = resources.GetString("dtmToDate.EditMask");
			this.dtmToDate.EmptyAsNull = ((bool)(resources.GetObject("dtmToDate.EmptyAsNull")));
			this.dtmToDate.Enabled = ((bool)(resources.GetObject("dtmToDate.Enabled")));
			this.dtmToDate.ErrorInfo.BeepOnError = ((bool)(resources.GetObject("dtmToDate.ErrorInfo.BeepOnError")));
			this.dtmToDate.ErrorInfo.ErrorMessage = resources.GetString("dtmToDate.ErrorInfo.ErrorMessage");
			this.dtmToDate.ErrorInfo.ErrorMessageCaption = resources.GetString("dtmToDate.ErrorInfo.ErrorMessageCaption");
			this.dtmToDate.ErrorInfo.ShowErrorMessage = ((bool)(resources.GetObject("dtmToDate.ErrorInfo.ShowErrorMessage")));
			this.dtmToDate.ErrorInfo.ValueOnError = ((object)(resources.GetObject("dtmToDate.ErrorInfo.ValueOnError")));
			this.dtmToDate.Font = ((System.Drawing.Font)(resources.GetObject("dtmToDate.Font")));
			this.dtmToDate.FormatType = ((C1.Win.C1Input.FormatTypeEnum)(resources.GetObject("dtmToDate.FormatType")));
			this.dtmToDate.GapHeight = ((int)(resources.GetObject("dtmToDate.GapHeight")));
			this.dtmToDate.GMTOffset = ((System.TimeSpan)(resources.GetObject("dtmToDate.GMTOffset")));
			this.dtmToDate.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("dtmToDate.ImeMode")));
			this.dtmToDate.InitialSelection = ((C1.Win.C1Input.InitialSelectionEnum)(resources.GetObject("dtmToDate.InitialSelection")));
			this.dtmToDate.Location = ((System.Drawing.Point)(resources.GetObject("dtmToDate.Location")));
			this.dtmToDate.MaskInfo.AutoTabWhenFilled = ((bool)(resources.GetObject("dtmToDate.MaskInfo.AutoTabWhenFilled")));
			this.dtmToDate.MaskInfo.CaseSensitive = ((bool)(resources.GetObject("dtmToDate.MaskInfo.CaseSensitive")));
			this.dtmToDate.MaskInfo.CopyWithLiterals = ((bool)(resources.GetObject("dtmToDate.MaskInfo.CopyWithLiterals")));
			this.dtmToDate.MaskInfo.EditMask = resources.GetString("dtmToDate.MaskInfo.EditMask");
			this.dtmToDate.MaskInfo.EmptyAsNull = ((bool)(resources.GetObject("dtmToDate.MaskInfo.EmptyAsNull")));
			this.dtmToDate.MaskInfo.ErrorMessage = resources.GetString("dtmToDate.MaskInfo.ErrorMessage");
			this.dtmToDate.MaskInfo.Inherit = ((C1.Win.C1Input.MaskInfoInheritFlags)(resources.GetObject("dtmToDate.MaskInfo.Inherit")));
			this.dtmToDate.MaskInfo.PromptChar = ((char)(resources.GetObject("dtmToDate.MaskInfo.PromptChar")));
			this.dtmToDate.MaskInfo.ShowLiterals = ((C1.Win.C1Input.ShowLiteralsEnum)(resources.GetObject("dtmToDate.MaskInfo.ShowLiterals")));
			this.dtmToDate.MaskInfo.StoredEmptyChar = ((char)(resources.GetObject("dtmToDate.MaskInfo.StoredEmptyChar")));
			this.dtmToDate.MaxLength = ((int)(resources.GetObject("dtmToDate.MaxLength")));
			this.dtmToDate.Name = "dtmToDate";
			this.dtmToDate.NullText = resources.GetString("dtmToDate.NullText");
			this.dtmToDate.ParseInfo.CaseSensitive = ((bool)(resources.GetObject("dtmToDate.ParseInfo.CaseSensitive")));
			this.dtmToDate.ParseInfo.CustomFormat = resources.GetString("dtmToDate.ParseInfo.CustomFormat");
			this.dtmToDate.ParseInfo.DateTimeStyle = ((C1.Win.C1Input.DateTimeStyleFlags)(resources.GetObject("dtmToDate.ParseInfo.DateTimeStyle")));
			this.dtmToDate.ParseInfo.EmptyAsNull = ((bool)(resources.GetObject("dtmToDate.ParseInfo.EmptyAsNull")));
			this.dtmToDate.ParseInfo.ErrorMessage = resources.GetString("dtmToDate.ParseInfo.ErrorMessage");
			this.dtmToDate.ParseInfo.FormatType = ((C1.Win.C1Input.FormatTypeEnum)(resources.GetObject("dtmToDate.ParseInfo.FormatType")));
			this.dtmToDate.ParseInfo.Inherit = ((C1.Win.C1Input.ParseInfoInheritFlags)(resources.GetObject("dtmToDate.ParseInfo.Inherit")));
			this.dtmToDate.ParseInfo.NullText = resources.GetString("dtmToDate.ParseInfo.NullText");
			this.dtmToDate.ParseInfo.TrimEnd = ((bool)(resources.GetObject("dtmToDate.ParseInfo.TrimEnd")));
			this.dtmToDate.ParseInfo.TrimStart = ((bool)(resources.GetObject("dtmToDate.ParseInfo.TrimStart")));
			this.dtmToDate.PasswordChar = ((char)(resources.GetObject("dtmToDate.PasswordChar")));
			this.dtmToDate.PostValidation.CaseSensitive = ((bool)(resources.GetObject("dtmToDate.PostValidation.CaseSensitive")));
			this.dtmToDate.PostValidation.ErrorMessage = resources.GetString("dtmToDate.PostValidation.ErrorMessage");
			this.dtmToDate.PostValidation.Inherit = ((C1.Win.C1Input.PostValidationInheritFlags)(resources.GetObject("dtmToDate.PostValidation.Inherit")));
			this.dtmToDate.PostValidation.Validation = ((C1.Win.C1Input.PostValidationTypeEnum)(resources.GetObject("dtmToDate.PostValidation.Validation")));
			this.dtmToDate.PostValidation.Values = ((System.Array)(resources.GetObject("dtmToDate.PostValidation.Values")));
			this.dtmToDate.PostValidation.ValuesExcluded = ((System.Array)(resources.GetObject("dtmToDate.PostValidation.ValuesExcluded")));
			this.dtmToDate.PreValidation.CaseSensitive = ((bool)(resources.GetObject("dtmToDate.PreValidation.CaseSensitive")));
			this.dtmToDate.PreValidation.ErrorMessage = resources.GetString("dtmToDate.PreValidation.ErrorMessage");
			this.dtmToDate.PreValidation.Inherit = ((C1.Win.C1Input.PreValidationInheritFlags)(resources.GetObject("dtmToDate.PreValidation.Inherit")));
			this.dtmToDate.PreValidation.ItemSeparator = resources.GetString("dtmToDate.PreValidation.ItemSeparator");
			this.dtmToDate.PreValidation.PatternString = resources.GetString("dtmToDate.PreValidation.PatternString");
			this.dtmToDate.PreValidation.RegexOptions = ((C1.Win.C1Input.RegexOptionFlags)(resources.GetObject("dtmToDate.PreValidation.RegexOptions")));
			this.dtmToDate.PreValidation.TrimEnd = ((bool)(resources.GetObject("dtmToDate.PreValidation.TrimEnd")));
			this.dtmToDate.PreValidation.TrimStart = ((bool)(resources.GetObject("dtmToDate.PreValidation.TrimStart")));
			this.dtmToDate.PreValidation.Validation = ((C1.Win.C1Input.PreValidationTypeEnum)(resources.GetObject("dtmToDate.PreValidation.Validation")));
			this.dtmToDate.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("dtmToDate.RightToLeft")));
			this.dtmToDate.ShowFocusRectangle = ((bool)(resources.GetObject("dtmToDate.ShowFocusRectangle")));
			this.dtmToDate.Size = ((System.Drawing.Size)(resources.GetObject("dtmToDate.Size")));
			this.dtmToDate.TabIndex = ((int)(resources.GetObject("dtmToDate.TabIndex")));
			this.dtmToDate.Tag = ((object)(resources.GetObject("dtmToDate.Tag")));
			this.dtmToDate.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("dtmToDate.TextAlign")));
			this.dtmToDate.TrimEnd = ((bool)(resources.GetObject("dtmToDate.TrimEnd")));
			this.dtmToDate.TrimStart = ((bool)(resources.GetObject("dtmToDate.TrimStart")));
			this.dtmToDate.UserCultureOverride = ((bool)(resources.GetObject("dtmToDate.UserCultureOverride")));
			this.dtmToDate.Value = ((object)(resources.GetObject("dtmToDate.Value")));
			this.dtmToDate.VerticalAlign = ((C1.Win.C1Input.VerticalAlignEnum)(resources.GetObject("dtmToDate.VerticalAlign")));
			this.dtmToDate.Visible = ((bool)(resources.GetObject("dtmToDate.Visible")));
			this.dtmToDate.VisibleButtons = ((C1.Win.C1Input.DropDownControlButtonFlags)(resources.GetObject("dtmToDate.VisibleButtons")));
			this.dtmToDate.Validating += new System.ComponentModel.CancelEventHandler(this.dtmFromDate_Validating);
			// 
			// chkCheckPoint
			// 
			this.chkCheckPoint.AccessibleDescription = resources.GetString("chkCheckPoint.AccessibleDescription");
			this.chkCheckPoint.AccessibleName = resources.GetString("chkCheckPoint.AccessibleName");
			this.chkCheckPoint.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("chkCheckPoint.Anchor")));
			this.chkCheckPoint.Appearance = ((System.Windows.Forms.Appearance)(resources.GetObject("chkCheckPoint.Appearance")));
			this.chkCheckPoint.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("chkCheckPoint.BackgroundImage")));
			this.chkCheckPoint.CheckAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("chkCheckPoint.CheckAlign")));
			this.chkCheckPoint.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("chkCheckPoint.Dock")));
			this.chkCheckPoint.Enabled = ((bool)(resources.GetObject("chkCheckPoint.Enabled")));
			this.chkCheckPoint.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("chkCheckPoint.FlatStyle")));
			this.chkCheckPoint.Font = ((System.Drawing.Font)(resources.GetObject("chkCheckPoint.Font")));
			this.chkCheckPoint.Image = ((System.Drawing.Image)(resources.GetObject("chkCheckPoint.Image")));
			this.chkCheckPoint.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("chkCheckPoint.ImageAlign")));
			this.chkCheckPoint.ImageIndex = ((int)(resources.GetObject("chkCheckPoint.ImageIndex")));
			this.chkCheckPoint.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("chkCheckPoint.ImeMode")));
			this.chkCheckPoint.Location = ((System.Drawing.Point)(resources.GetObject("chkCheckPoint.Location")));
			this.chkCheckPoint.Name = "chkCheckPoint";
			this.chkCheckPoint.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("chkCheckPoint.RightToLeft")));
			this.chkCheckPoint.Size = ((System.Drawing.Size)(resources.GetObject("chkCheckPoint.Size")));
			this.chkCheckPoint.TabIndex = ((int)(resources.GetObject("chkCheckPoint.TabIndex")));
			this.chkCheckPoint.Text = resources.GetString("chkCheckPoint.Text");
			this.chkCheckPoint.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("chkCheckPoint.TextAlign")));
			this.chkCheckPoint.Visible = ((bool)(resources.GetObject("chkCheckPoint.Visible")));
			// 
			// btnRemoveDCPResults
			// 
			this.btnRemoveDCPResults.AccessibleDescription = resources.GetString("btnRemoveDCPResults.AccessibleDescription");
			this.btnRemoveDCPResults.AccessibleName = resources.GetString("btnRemoveDCPResults.AccessibleName");
			this.btnRemoveDCPResults.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnRemoveDCPResults.Anchor")));
			this.btnRemoveDCPResults.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnRemoveDCPResults.BackgroundImage")));
			this.btnRemoveDCPResults.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnRemoveDCPResults.Dock")));
			this.btnRemoveDCPResults.Enabled = ((bool)(resources.GetObject("btnRemoveDCPResults.Enabled")));
			this.btnRemoveDCPResults.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnRemoveDCPResults.FlatStyle")));
			this.btnRemoveDCPResults.Font = ((System.Drawing.Font)(resources.GetObject("btnRemoveDCPResults.Font")));
			this.btnRemoveDCPResults.Image = ((System.Drawing.Image)(resources.GetObject("btnRemoveDCPResults.Image")));
			this.btnRemoveDCPResults.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnRemoveDCPResults.ImageAlign")));
			this.btnRemoveDCPResults.ImageIndex = ((int)(resources.GetObject("btnRemoveDCPResults.ImageIndex")));
			this.btnRemoveDCPResults.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnRemoveDCPResults.ImeMode")));
			this.btnRemoveDCPResults.Location = ((System.Drawing.Point)(resources.GetObject("btnRemoveDCPResults.Location")));
			this.btnRemoveDCPResults.Name = "btnRemoveDCPResults";
			this.btnRemoveDCPResults.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnRemoveDCPResults.RightToLeft")));
			this.btnRemoveDCPResults.Size = ((System.Drawing.Size)(resources.GetObject("btnRemoveDCPResults.Size")));
			this.btnRemoveDCPResults.TabIndex = ((int)(resources.GetObject("btnRemoveDCPResults.TabIndex")));
			this.btnRemoveDCPResults.Text = resources.GetString("btnRemoveDCPResults.Text");
			this.btnRemoveDCPResults.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnRemoveDCPResults.TextAlign")));
			this.btnRemoveDCPResults.Visible = ((bool)(resources.GetObject("btnRemoveDCPResults.Visible")));
			this.btnRemoveDCPResults.Click += new System.EventHandler(this.btnRemoveDCPResults_Click);
			// 
			// textBox1
			// 
			this.textBox1.AccessibleDescription = resources.GetString("textBox1.AccessibleDescription");
			this.textBox1.AccessibleName = resources.GetString("textBox1.AccessibleName");
			this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("textBox1.Anchor")));
			this.textBox1.AutoSize = ((bool)(resources.GetObject("textBox1.AutoSize")));
			this.textBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("textBox1.BackgroundImage")));
			this.textBox1.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("textBox1.Dock")));
			this.textBox1.Enabled = ((bool)(resources.GetObject("textBox1.Enabled")));
			this.textBox1.Font = ((System.Drawing.Font)(resources.GetObject("textBox1.Font")));
			this.textBox1.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("textBox1.ImeMode")));
			this.textBox1.Location = ((System.Drawing.Point)(resources.GetObject("textBox1.Location")));
			this.textBox1.MaxLength = ((int)(resources.GetObject("textBox1.MaxLength")));
			this.textBox1.Multiline = ((bool)(resources.GetObject("textBox1.Multiline")));
			this.textBox1.Name = "textBox1";
			this.textBox1.PasswordChar = ((char)(resources.GetObject("textBox1.PasswordChar")));
			this.textBox1.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("textBox1.RightToLeft")));
			this.textBox1.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("textBox1.ScrollBars")));
			this.textBox1.Size = ((System.Drawing.Size)(resources.GetObject("textBox1.Size")));
			this.textBox1.TabIndex = ((int)(resources.GetObject("textBox1.TabIndex")));
			this.textBox1.Text = resources.GetString("textBox1.Text");
			this.textBox1.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("textBox1.TextAlign")));
			this.textBox1.Visible = ((bool)(resources.GetObject("textBox1.Visible")));
			this.textBox1.WordWrap = ((bool)(resources.GetObject("textBox1.WordWrap")));
			// 
			// btnProcess
			// 
			this.btnProcess.AccessibleDescription = resources.GetString("btnProcess.AccessibleDescription");
			this.btnProcess.AccessibleName = resources.GetString("btnProcess.AccessibleName");
			this.btnProcess.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnProcess.Anchor")));
			this.btnProcess.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnProcess.BackgroundImage")));
			this.btnProcess.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnProcess.Dock")));
			this.btnProcess.Enabled = ((bool)(resources.GetObject("btnProcess.Enabled")));
			this.btnProcess.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnProcess.FlatStyle")));
			this.btnProcess.Font = ((System.Drawing.Font)(resources.GetObject("btnProcess.Font")));
			this.btnProcess.Image = ((System.Drawing.Image)(resources.GetObject("btnProcess.Image")));
			this.btnProcess.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnProcess.ImageAlign")));
			this.btnProcess.ImageIndex = ((int)(resources.GetObject("btnProcess.ImageIndex")));
			this.btnProcess.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnProcess.ImeMode")));
			this.btnProcess.Location = ((System.Drawing.Point)(resources.GetObject("btnProcess.Location")));
			this.btnProcess.Name = "btnProcess";
			this.btnProcess.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnProcess.RightToLeft")));
			this.btnProcess.Size = ((System.Drawing.Size)(resources.GetObject("btnProcess.Size")));
			this.btnProcess.TabIndex = ((int)(resources.GetObject("btnProcess.TabIndex")));
			this.btnProcess.Text = resources.GetString("btnProcess.Text");
			this.btnProcess.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnProcess.TextAlign")));
			this.btnProcess.Visible = ((bool)(resources.GetObject("btnProcess.Visible")));
			this.btnProcess.Click += new System.EventHandler(this.btnProcess_Click);
			// 
			// cboScheduleCode
			// 
			this.cboScheduleCode.AccessibleDescription = resources.GetString("cboScheduleCode.AccessibleDescription");
			this.cboScheduleCode.AccessibleName = resources.GetString("cboScheduleCode.AccessibleName");
			this.cboScheduleCode.AddItemSeparator = ';';
			this.cboScheduleCode.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("cboScheduleCode.Anchor")));
			this.cboScheduleCode.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cboScheduleCode.BackgroundImage")));
			this.cboScheduleCode.Caption = "";
			this.cboScheduleCode.CaptionHeight = 17;
			this.cboScheduleCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
			this.cboScheduleCode.ColumnCaptionHeight = 17;
			this.cboScheduleCode.ColumnFooterHeight = 17;
			this.cboScheduleCode.ComboStyle = C1.Win.C1List.ComboStyleEnum.DropdownList;
			this.cboScheduleCode.ContentHeight = 15;
			this.cboScheduleCode.DeadAreaBackColor = System.Drawing.Color.Empty;
			this.cboScheduleCode.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("cboScheduleCode.Dock")));
			this.cboScheduleCode.EditorBackColor = System.Drawing.SystemColors.Window;
			this.cboScheduleCode.EditorFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.cboScheduleCode.EditorForeColor = System.Drawing.SystemColors.WindowText;
			this.cboScheduleCode.EditorHeight = 15;
			this.cboScheduleCode.Enabled = ((bool)(resources.GetObject("cboScheduleCode.Enabled")));
			this.cboScheduleCode.Font = ((System.Drawing.Font)(resources.GetObject("cboScheduleCode.Font")));
			this.cboScheduleCode.GapHeight = 2;
			this.cboScheduleCode.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("cboScheduleCode.ImeMode")));
			this.cboScheduleCode.ItemHeight = 15;
			this.cboScheduleCode.Location = ((System.Drawing.Point)(resources.GetObject("cboScheduleCode.Location")));
			this.cboScheduleCode.MatchEntryTimeout = ((long)(2000));
			this.cboScheduleCode.MaxDropDownItems = ((short)(5));
			this.cboScheduleCode.MaxLength = 32767;
			this.cboScheduleCode.MouseCursor = System.Windows.Forms.Cursors.Default;
			this.cboScheduleCode.Name = "cboScheduleCode";
			this.cboScheduleCode.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("cboScheduleCode.RightToLeft")));
			this.cboScheduleCode.RowDivider.Color = System.Drawing.Color.DarkGray;
			this.cboScheduleCode.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None;
			this.cboScheduleCode.RowSubDividerColor = System.Drawing.Color.DarkGray;
			this.cboScheduleCode.Size = ((System.Drawing.Size)(resources.GetObject("cboScheduleCode.Size")));
			this.cboScheduleCode.TabIndex = ((int)(resources.GetObject("cboScheduleCode.TabIndex")));
			this.cboScheduleCode.Text = resources.GetString("cboScheduleCode.Text");
			this.cboScheduleCode.Visible = ((bool)(resources.GetObject("cboScheduleCode.Visible")));
			this.cboScheduleCode.PropBag = "<?xml version=\"1.0\"?><Blob><Styles type=\"C1.Win.C1List.Design.ContextWrapper\"><Da" +
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
			// cboScheduleType
			// 
			this.cboScheduleType.AccessibleDescription = resources.GetString("cboScheduleType.AccessibleDescription");
			this.cboScheduleType.AccessibleName = resources.GetString("cboScheduleType.AccessibleName");
			this.cboScheduleType.AddItemSeparator = ';';
			this.cboScheduleType.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("cboScheduleType.Anchor")));
			this.cboScheduleType.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cboScheduleType.BackgroundImage")));
			this.cboScheduleType.Caption = "";
			this.cboScheduleType.CaptionHeight = 17;
			this.cboScheduleType.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
			this.cboScheduleType.ColumnCaptionHeight = 17;
			this.cboScheduleType.ColumnFooterHeight = 17;
			this.cboScheduleType.ComboStyle = C1.Win.C1List.ComboStyleEnum.DropdownList;
			this.cboScheduleType.ContentHeight = 15;
			this.cboScheduleType.DeadAreaBackColor = System.Drawing.Color.Empty;
			this.cboScheduleType.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("cboScheduleType.Dock")));
			this.cboScheduleType.EditorBackColor = System.Drawing.SystemColors.Window;
			this.cboScheduleType.EditorFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.cboScheduleType.EditorForeColor = System.Drawing.SystemColors.WindowText;
			this.cboScheduleType.EditorHeight = 15;
			this.cboScheduleType.Enabled = ((bool)(resources.GetObject("cboScheduleType.Enabled")));
			this.cboScheduleType.Font = ((System.Drawing.Font)(resources.GetObject("cboScheduleType.Font")));
			this.cboScheduleType.GapHeight = 2;
			this.cboScheduleType.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("cboScheduleType.ImeMode")));
			this.cboScheduleType.ItemHeight = 15;
			this.cboScheduleType.Location = ((System.Drawing.Point)(resources.GetObject("cboScheduleType.Location")));
			this.cboScheduleType.MatchEntryTimeout = ((long)(2000));
			this.cboScheduleType.MaxDropDownItems = ((short)(5));
			this.cboScheduleType.MaxLength = 32767;
			this.cboScheduleType.MouseCursor = System.Windows.Forms.Cursors.Default;
			this.cboScheduleType.Name = "cboScheduleType";
			this.cboScheduleType.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("cboScheduleType.RightToLeft")));
			this.cboScheduleType.RowDivider.Color = System.Drawing.Color.DarkGray;
			this.cboScheduleType.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None;
			this.cboScheduleType.RowSubDividerColor = System.Drawing.Color.DarkGray;
			this.cboScheduleType.Size = ((System.Drawing.Size)(resources.GetObject("cboScheduleType.Size")));
			this.cboScheduleType.TabIndex = ((int)(resources.GetObject("cboScheduleType.TabIndex")));
			this.cboScheduleType.Text = resources.GetString("cboScheduleType.Text");
			this.cboScheduleType.Visible = ((bool)(resources.GetObject("cboScheduleType.Visible")));
			this.cboScheduleType.PropBag = "<?xml version=\"1.0\"?><Blob><Styles type=\"C1.Win.C1List.Design.ContextWrapper\"><Da" +
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
			// txtPlanHorizon
			// 
			this.txtPlanHorizon.AccessibleDescription = resources.GetString("txtPlanHorizon.AccessibleDescription");
			this.txtPlanHorizon.AccessibleName = resources.GetString("txtPlanHorizon.AccessibleName");
			this.txtPlanHorizon.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("txtPlanHorizon.Anchor")));
			this.txtPlanHorizon.AutoSize = ((bool)(resources.GetObject("txtPlanHorizon.AutoSize")));
			this.txtPlanHorizon.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("txtPlanHorizon.BackgroundImage")));
			this.txtPlanHorizon.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("txtPlanHorizon.Dock")));
			this.txtPlanHorizon.Enabled = ((bool)(resources.GetObject("txtPlanHorizon.Enabled")));
			this.txtPlanHorizon.Font = ((System.Drawing.Font)(resources.GetObject("txtPlanHorizon.Font")));
			this.txtPlanHorizon.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("txtPlanHorizon.ImeMode")));
			this.txtPlanHorizon.Location = ((System.Drawing.Point)(resources.GetObject("txtPlanHorizon.Location")));
			this.txtPlanHorizon.MaxLength = ((int)(resources.GetObject("txtPlanHorizon.MaxLength")));
			this.txtPlanHorizon.Multiline = ((bool)(resources.GetObject("txtPlanHorizon.Multiline")));
			this.txtPlanHorizon.Name = "txtPlanHorizon";
			this.txtPlanHorizon.PasswordChar = ((char)(resources.GetObject("txtPlanHorizon.PasswordChar")));
			this.txtPlanHorizon.ReadOnly = true;
			this.txtPlanHorizon.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("txtPlanHorizon.RightToLeft")));
			this.txtPlanHorizon.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("txtPlanHorizon.ScrollBars")));
			this.txtPlanHorizon.Size = ((System.Drawing.Size)(resources.GetObject("txtPlanHorizon.Size")));
			this.txtPlanHorizon.TabIndex = ((int)(resources.GetObject("txtPlanHorizon.TabIndex")));
			this.txtPlanHorizon.Text = resources.GetString("txtPlanHorizon.Text");
			this.txtPlanHorizon.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("txtPlanHorizon.TextAlign")));
			this.txtPlanHorizon.Visible = ((bool)(resources.GetObject("txtPlanHorizon.Visible")));
			this.txtPlanHorizon.WordWrap = ((bool)(resources.GetObject("txtPlanHorizon.WordWrap")));
			// 
			// lblPlanHorizon
			// 
			this.lblPlanHorizon.AccessibleDescription = resources.GetString("lblPlanHorizon.AccessibleDescription");
			this.lblPlanHorizon.AccessibleName = resources.GetString("lblPlanHorizon.AccessibleName");
			this.lblPlanHorizon.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblPlanHorizon.Anchor")));
			this.lblPlanHorizon.AutoSize = ((bool)(resources.GetObject("lblPlanHorizon.AutoSize")));
			this.lblPlanHorizon.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblPlanHorizon.Dock")));
			this.lblPlanHorizon.Enabled = ((bool)(resources.GetObject("lblPlanHorizon.Enabled")));
			this.lblPlanHorizon.Font = ((System.Drawing.Font)(resources.GetObject("lblPlanHorizon.Font")));
			this.lblPlanHorizon.Image = ((System.Drawing.Image)(resources.GetObject("lblPlanHorizon.Image")));
			this.lblPlanHorizon.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblPlanHorizon.ImageAlign")));
			this.lblPlanHorizon.ImageIndex = ((int)(resources.GetObject("lblPlanHorizon.ImageIndex")));
			this.lblPlanHorizon.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblPlanHorizon.ImeMode")));
			this.lblPlanHorizon.Location = ((System.Drawing.Point)(resources.GetObject("lblPlanHorizon.Location")));
			this.lblPlanHorizon.Name = "lblPlanHorizon";
			this.lblPlanHorizon.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblPlanHorizon.RightToLeft")));
			this.lblPlanHorizon.Size = ((System.Drawing.Size)(resources.GetObject("lblPlanHorizon.Size")));
			this.lblPlanHorizon.TabIndex = ((int)(resources.GetObject("lblPlanHorizon.TabIndex")));
			this.lblPlanHorizon.Text = resources.GetString("lblPlanHorizon.Text");
			this.lblPlanHorizon.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblPlanHorizon.TextAlign")));
			this.lblPlanHorizon.Visible = ((bool)(resources.GetObject("lblPlanHorizon.Visible")));
			// 
			// chkSafetyStock
			// 
			this.chkSafetyStock.AccessibleDescription = resources.GetString("chkSafetyStock.AccessibleDescription");
			this.chkSafetyStock.AccessibleName = resources.GetString("chkSafetyStock.AccessibleName");
			this.chkSafetyStock.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("chkSafetyStock.Anchor")));
			this.chkSafetyStock.Appearance = ((System.Windows.Forms.Appearance)(resources.GetObject("chkSafetyStock.Appearance")));
			this.chkSafetyStock.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("chkSafetyStock.BackgroundImage")));
			this.chkSafetyStock.CheckAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("chkSafetyStock.CheckAlign")));
			this.chkSafetyStock.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("chkSafetyStock.Dock")));
			this.chkSafetyStock.Enabled = ((bool)(resources.GetObject("chkSafetyStock.Enabled")));
			this.chkSafetyStock.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("chkSafetyStock.FlatStyle")));
			this.chkSafetyStock.Font = ((System.Drawing.Font)(resources.GetObject("chkSafetyStock.Font")));
			this.chkSafetyStock.Image = ((System.Drawing.Image)(resources.GetObject("chkSafetyStock.Image")));
			this.chkSafetyStock.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("chkSafetyStock.ImageAlign")));
			this.chkSafetyStock.ImageIndex = ((int)(resources.GetObject("chkSafetyStock.ImageIndex")));
			this.chkSafetyStock.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("chkSafetyStock.ImeMode")));
			this.chkSafetyStock.Location = ((System.Drawing.Point)(resources.GetObject("chkSafetyStock.Location")));
			this.chkSafetyStock.Name = "chkSafetyStock";
			this.chkSafetyStock.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("chkSafetyStock.RightToLeft")));
			this.chkSafetyStock.Size = ((System.Drawing.Size)(resources.GetObject("chkSafetyStock.Size")));
			this.chkSafetyStock.TabIndex = ((int)(resources.GetObject("chkSafetyStock.TabIndex")));
			this.chkSafetyStock.Text = resources.GetString("chkSafetyStock.Text");
			this.chkSafetyStock.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("chkSafetyStock.TextAlign")));
			this.chkSafetyStock.Visible = ((bool)(resources.GetObject("chkSafetyStock.Visible")));
			// 
			// chkOnHand
			// 
			this.chkOnHand.AccessibleDescription = resources.GetString("chkOnHand.AccessibleDescription");
			this.chkOnHand.AccessibleName = resources.GetString("chkOnHand.AccessibleName");
			this.chkOnHand.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("chkOnHand.Anchor")));
			this.chkOnHand.Appearance = ((System.Windows.Forms.Appearance)(resources.GetObject("chkOnHand.Appearance")));
			this.chkOnHand.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("chkOnHand.BackgroundImage")));
			this.chkOnHand.CheckAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("chkOnHand.CheckAlign")));
			this.chkOnHand.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("chkOnHand.Dock")));
			this.chkOnHand.Enabled = ((bool)(resources.GetObject("chkOnHand.Enabled")));
			this.chkOnHand.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("chkOnHand.FlatStyle")));
			this.chkOnHand.Font = ((System.Drawing.Font)(resources.GetObject("chkOnHand.Font")));
			this.chkOnHand.Image = ((System.Drawing.Image)(resources.GetObject("chkOnHand.Image")));
			this.chkOnHand.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("chkOnHand.ImageAlign")));
			this.chkOnHand.ImageIndex = ((int)(resources.GetObject("chkOnHand.ImageIndex")));
			this.chkOnHand.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("chkOnHand.ImeMode")));
			this.chkOnHand.Location = ((System.Drawing.Point)(resources.GetObject("chkOnHand.Location")));
			this.chkOnHand.Name = "chkOnHand";
			this.chkOnHand.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("chkOnHand.RightToLeft")));
			this.chkOnHand.Size = ((System.Drawing.Size)(resources.GetObject("chkOnHand.Size")));
			this.chkOnHand.TabIndex = ((int)(resources.GetObject("chkOnHand.TabIndex")));
			this.chkOnHand.Text = resources.GetString("chkOnHand.Text");
			this.chkOnHand.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("chkOnHand.TextAlign")));
			this.chkOnHand.Visible = ((bool)(resources.GetObject("chkOnHand.Visible")));
			// 
			// label1
			// 
			this.label1.AccessibleDescription = resources.GetString("label1.AccessibleDescription");
			this.label1.AccessibleName = resources.GetString("label1.AccessibleName");
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("label1.Anchor")));
			this.label1.AutoSize = ((bool)(resources.GetObject("label1.AutoSize")));
			this.label1.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("label1.Dock")));
			this.label1.Enabled = ((bool)(resources.GetObject("label1.Enabled")));
			this.label1.Font = ((System.Drawing.Font)(resources.GetObject("label1.Font")));
			this.label1.ForeColor = System.Drawing.Color.Maroon;
			this.label1.Image = ((System.Drawing.Image)(resources.GetObject("label1.Image")));
			this.label1.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label1.ImageAlign")));
			this.label1.ImageIndex = ((int)(resources.GetObject("label1.ImageIndex")));
			this.label1.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("label1.ImeMode")));
			this.label1.Location = ((System.Drawing.Point)(resources.GetObject("label1.Location")));
			this.label1.Name = "label1";
			this.label1.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("label1.RightToLeft")));
			this.label1.Size = ((System.Drawing.Size)(resources.GetObject("label1.Size")));
			this.label1.TabIndex = ((int)(resources.GetObject("label1.TabIndex")));
			this.label1.Text = resources.GetString("label1.Text");
			this.label1.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label1.TextAlign")));
			this.label1.Visible = ((bool)(resources.GetObject("label1.Visible")));
			// 
			// lblByDay
			// 
			this.lblByDay.AccessibleDescription = resources.GetString("lblByDay.AccessibleDescription");
			this.lblByDay.AccessibleName = resources.GetString("lblByDay.AccessibleName");
			this.lblByDay.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblByDay.Anchor")));
			this.lblByDay.AutoSize = ((bool)(resources.GetObject("lblByDay.AutoSize")));
			this.lblByDay.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblByDay.Dock")));
			this.lblByDay.Enabled = ((bool)(resources.GetObject("lblByDay.Enabled")));
			this.lblByDay.Font = ((System.Drawing.Font)(resources.GetObject("lblByDay.Font")));
			this.lblByDay.Image = ((System.Drawing.Image)(resources.GetObject("lblByDay.Image")));
			this.lblByDay.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblByDay.ImageAlign")));
			this.lblByDay.ImageIndex = ((int)(resources.GetObject("lblByDay.ImageIndex")));
			this.lblByDay.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblByDay.ImeMode")));
			this.lblByDay.Location = ((System.Drawing.Point)(resources.GetObject("lblByDay.Location")));
			this.lblByDay.Name = "lblByDay";
			this.lblByDay.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblByDay.RightToLeft")));
			this.lblByDay.Size = ((System.Drawing.Size)(resources.GetObject("lblByDay.Size")));
			this.lblByDay.TabIndex = ((int)(resources.GetObject("lblByDay.TabIndex")));
			this.lblByDay.Text = resources.GetString("lblByDay.Text");
			this.lblByDay.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblByDay.TextAlign")));
			this.lblByDay.Visible = ((bool)(resources.GetObject("lblByDay.Visible")));
			// 
			// lblByHour
			// 
			this.lblByHour.AccessibleDescription = resources.GetString("lblByHour.AccessibleDescription");
			this.lblByHour.AccessibleName = resources.GetString("lblByHour.AccessibleName");
			this.lblByHour.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblByHour.Anchor")));
			this.lblByHour.AutoSize = ((bool)(resources.GetObject("lblByHour.AutoSize")));
			this.lblByHour.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblByHour.Dock")));
			this.lblByHour.Enabled = ((bool)(resources.GetObject("lblByHour.Enabled")));
			this.lblByHour.Font = ((System.Drawing.Font)(resources.GetObject("lblByHour.Font")));
			this.lblByHour.Image = ((System.Drawing.Image)(resources.GetObject("lblByHour.Image")));
			this.lblByHour.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblByHour.ImageAlign")));
			this.lblByHour.ImageIndex = ((int)(resources.GetObject("lblByHour.ImageIndex")));
			this.lblByHour.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblByHour.ImeMode")));
			this.lblByHour.Location = ((System.Drawing.Point)(resources.GetObject("lblByHour.Location")));
			this.lblByHour.Name = "lblByHour";
			this.lblByHour.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblByHour.RightToLeft")));
			this.lblByHour.Size = ((System.Drawing.Size)(resources.GetObject("lblByHour.Size")));
			this.lblByHour.TabIndex = ((int)(resources.GetObject("lblByHour.TabIndex")));
			this.lblByHour.Text = resources.GetString("lblByHour.Text");
			this.lblByHour.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblByHour.TextAlign")));
			this.lblByHour.Visible = ((bool)(resources.GetObject("lblByHour.Visible")));
			// 
			// cboGroupBy
			// 
			this.cboGroupBy.AccessibleDescription = resources.GetString("cboGroupBy.AccessibleDescription");
			this.cboGroupBy.AccessibleName = resources.GetString("cboGroupBy.AccessibleName");
			this.cboGroupBy.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("cboGroupBy.Anchor")));
			this.cboGroupBy.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cboGroupBy.BackgroundImage")));
			this.cboGroupBy.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("cboGroupBy.Dock")));
			this.cboGroupBy.Enabled = ((bool)(resources.GetObject("cboGroupBy.Enabled")));
			this.cboGroupBy.Font = ((System.Drawing.Font)(resources.GetObject("cboGroupBy.Font")));
			this.cboGroupBy.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("cboGroupBy.ImeMode")));
			this.cboGroupBy.IntegralHeight = ((bool)(resources.GetObject("cboGroupBy.IntegralHeight")));
			this.cboGroupBy.ItemHeight = ((int)(resources.GetObject("cboGroupBy.ItemHeight")));
			this.cboGroupBy.Location = ((System.Drawing.Point)(resources.GetObject("cboGroupBy.Location")));
			this.cboGroupBy.MaxDropDownItems = ((int)(resources.GetObject("cboGroupBy.MaxDropDownItems")));
			this.cboGroupBy.MaxLength = ((int)(resources.GetObject("cboGroupBy.MaxLength")));
			this.cboGroupBy.Name = "cboGroupBy";
			this.cboGroupBy.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("cboGroupBy.RightToLeft")));
			this.cboGroupBy.Size = ((System.Drawing.Size)(resources.GetObject("cboGroupBy.Size")));
			this.cboGroupBy.TabIndex = ((int)(resources.GetObject("cboGroupBy.TabIndex")));
			this.cboGroupBy.Text = resources.GetString("cboGroupBy.Text");
			this.cboGroupBy.Visible = ((bool)(resources.GetObject("cboGroupBy.Visible")));
			// 
			// cboYear
			// 
			this.cboYear.AccessibleDescription = resources.GetString("cboYear.AccessibleDescription");
			this.cboYear.AccessibleName = resources.GetString("cboYear.AccessibleName");
			this.cboYear.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("cboYear.Anchor")));
			this.cboYear.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cboYear.BackgroundImage")));
			this.cboYear.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("cboYear.Dock")));
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
			// 
			// cboMonth
			// 
			this.cboMonth.AccessibleDescription = resources.GetString("cboMonth.AccessibleDescription");
			this.cboMonth.AccessibleName = resources.GetString("cboMonth.AccessibleName");
			this.cboMonth.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("cboMonth.Anchor")));
			this.cboMonth.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cboMonth.BackgroundImage")));
			this.cboMonth.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("cboMonth.Dock")));
			this.cboMonth.Enabled = ((bool)(resources.GetObject("cboMonth.Enabled")));
			this.cboMonth.Font = ((System.Drawing.Font)(resources.GetObject("cboMonth.Font")));
			this.cboMonth.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("cboMonth.ImeMode")));
			this.cboMonth.IntegralHeight = ((bool)(resources.GetObject("cboMonth.IntegralHeight")));
			this.cboMonth.ItemHeight = ((int)(resources.GetObject("cboMonth.ItemHeight")));
			this.cboMonth.Location = ((System.Drawing.Point)(resources.GetObject("cboMonth.Location")));
			this.cboMonth.MaxDropDownItems = ((int)(resources.GetObject("cboMonth.MaxDropDownItems")));
			this.cboMonth.MaxLength = ((int)(resources.GetObject("cboMonth.MaxLength")));
			this.cboMonth.Name = "cboMonth";
			this.cboMonth.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("cboMonth.RightToLeft")));
			this.cboMonth.Size = ((System.Drawing.Size)(resources.GetObject("cboMonth.Size")));
			this.cboMonth.TabIndex = ((int)(resources.GetObject("cboMonth.TabIndex")));
			this.cboMonth.Text = resources.GetString("cboMonth.Text");
			this.cboMonth.Visible = ((bool)(resources.GetObject("cboMonth.Visible")));
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
			// lblMonth
			// 
			this.lblMonth.AccessibleDescription = resources.GetString("lblMonth.AccessibleDescription");
			this.lblMonth.AccessibleName = resources.GetString("lblMonth.AccessibleName");
			this.lblMonth.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblMonth.Anchor")));
			this.lblMonth.AutoSize = ((bool)(resources.GetObject("lblMonth.AutoSize")));
			this.lblMonth.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblMonth.Dock")));
			this.lblMonth.Enabled = ((bool)(resources.GetObject("lblMonth.Enabled")));
			this.lblMonth.Font = ((System.Drawing.Font)(resources.GetObject("lblMonth.Font")));
			this.lblMonth.ForeColor = System.Drawing.Color.Maroon;
			this.lblMonth.Image = ((System.Drawing.Image)(resources.GetObject("lblMonth.Image")));
			this.lblMonth.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblMonth.ImageAlign")));
			this.lblMonth.ImageIndex = ((int)(resources.GetObject("lblMonth.ImageIndex")));
			this.lblMonth.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblMonth.ImeMode")));
			this.lblMonth.Location = ((System.Drawing.Point)(resources.GetObject("lblMonth.Location")));
			this.lblMonth.Name = "lblMonth";
			this.lblMonth.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblMonth.RightToLeft")));
			this.lblMonth.Size = ((System.Drawing.Size)(resources.GetObject("lblMonth.Size")));
			this.lblMonth.TabIndex = ((int)(resources.GetObject("lblMonth.TabIndex")));
			this.lblMonth.Text = resources.GetString("lblMonth.Text");
			this.lblMonth.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblMonth.TextAlign")));
			this.lblMonth.Visible = ((bool)(resources.GetObject("lblMonth.Visible")));
			// 
			// lblVersion
			// 
			this.lblVersion.AccessibleDescription = resources.GetString("lblVersion.AccessibleDescription");
			this.lblVersion.AccessibleName = resources.GetString("lblVersion.AccessibleName");
			this.lblVersion.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblVersion.Anchor")));
			this.lblVersion.AutoSize = ((bool)(resources.GetObject("lblVersion.AutoSize")));
			this.lblVersion.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblVersion.Dock")));
			this.lblVersion.Enabled = ((bool)(resources.GetObject("lblVersion.Enabled")));
			this.lblVersion.Font = ((System.Drawing.Font)(resources.GetObject("lblVersion.Font")));
			this.lblVersion.ForeColor = System.Drawing.Color.Maroon;
			this.lblVersion.Image = ((System.Drawing.Image)(resources.GetObject("lblVersion.Image")));
			this.lblVersion.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblVersion.ImageAlign")));
			this.lblVersion.ImageIndex = ((int)(resources.GetObject("lblVersion.ImageIndex")));
			this.lblVersion.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblVersion.ImeMode")));
			this.lblVersion.Location = ((System.Drawing.Point)(resources.GetObject("lblVersion.Location")));
			this.lblVersion.Name = "lblVersion";
			this.lblVersion.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblVersion.RightToLeft")));
			this.lblVersion.Size = ((System.Drawing.Size)(resources.GetObject("lblVersion.Size")));
			this.lblVersion.TabIndex = ((int)(resources.GetObject("lblVersion.TabIndex")));
			this.lblVersion.Text = resources.GetString("lblVersion.Text");
			this.lblVersion.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblVersion.TextAlign")));
			this.lblVersion.Visible = ((bool)(resources.GetObject("lblVersion.Visible")));
			// 
			// txtVersion
			// 
			this.txtVersion.AcceptsEscape = ((bool)(resources.GetObject("txtVersion.AcceptsEscape")));
			this.txtVersion.AccessibleDescription = resources.GetString("txtVersion.AccessibleDescription");
			this.txtVersion.AccessibleName = resources.GetString("txtVersion.AccessibleName");
			this.txtVersion.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("txtVersion.Anchor")));
			this.txtVersion.AutoSize = ((bool)(resources.GetObject("txtVersion.AutoSize")));
			this.txtVersion.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("txtVersion.BackgroundImage")));
			this.txtVersion.BorderStyle = ((System.Windows.Forms.BorderStyle)(resources.GetObject("txtVersion.BorderStyle")));
			// 
			// txtVersion.Calculator
			// 
			this.txtVersion.Calculator.AccessibleDescription = resources.GetString("txtVersion.Calculator.AccessibleDescription");
			this.txtVersion.Calculator.AccessibleName = resources.GetString("txtVersion.Calculator.AccessibleName");
			this.txtVersion.Calculator.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("txtVersion.Calculator.BackgroundImage")));
			this.txtVersion.Calculator.ButtonFlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("txtVersion.Calculator.ButtonFlatStyle")));
			this.txtVersion.Calculator.DisplayFormat = resources.GetString("txtVersion.Calculator.DisplayFormat");
			this.txtVersion.Calculator.Font = ((System.Drawing.Font)(resources.GetObject("txtVersion.Calculator.Font")));
			this.txtVersion.Calculator.FormatOnClose = ((bool)(resources.GetObject("txtVersion.Calculator.FormatOnClose")));
			this.txtVersion.Calculator.StoredFormat = resources.GetString("txtVersion.Calculator.StoredFormat");
			this.txtVersion.Calculator.UIStrings.Content = ((string[])(resources.GetObject("txtVersion.Calculator.UIStrings.Content")));
			this.txtVersion.CaseSensitive = ((bool)(resources.GetObject("txtVersion.CaseSensitive")));
			this.txtVersion.Culture = ((int)(resources.GetObject("txtVersion.Culture")));
			this.txtVersion.CustomFormat = resources.GetString("txtVersion.CustomFormat");
			this.txtVersion.DataType = ((System.Type)(resources.GetObject("txtVersion.DataType")));
			this.txtVersion.DisplayFormat.CustomFormat = resources.GetString("txtVersion.DisplayFormat.CustomFormat");
			this.txtVersion.DisplayFormat.FormatType = ((C1.Win.C1Input.FormatTypeEnum)(resources.GetObject("txtVersion.DisplayFormat.FormatType")));
			this.txtVersion.DisplayFormat.Inherit = ((C1.Win.C1Input.FormatInfoInheritFlags)(resources.GetObject("txtVersion.DisplayFormat.Inherit")));
			this.txtVersion.DisplayFormat.NullText = resources.GetString("txtVersion.DisplayFormat.NullText");
			this.txtVersion.DisplayFormat.TrimEnd = ((bool)(resources.GetObject("txtVersion.DisplayFormat.TrimEnd")));
			this.txtVersion.DisplayFormat.TrimStart = ((bool)(resources.GetObject("txtVersion.DisplayFormat.TrimStart")));
			this.txtVersion.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("txtVersion.Dock")));
			this.txtVersion.DropDownFormAlign = ((C1.Win.C1Input.DropDownFormAlignmentEnum)(resources.GetObject("txtVersion.DropDownFormAlign")));
			this.txtVersion.EditFormat.CustomFormat = resources.GetString("txtVersion.EditFormat.CustomFormat");
			this.txtVersion.EditFormat.FormatType = ((C1.Win.C1Input.FormatTypeEnum)(resources.GetObject("txtVersion.EditFormat.FormatType")));
			this.txtVersion.EditFormat.Inherit = ((C1.Win.C1Input.FormatInfoInheritFlags)(resources.GetObject("txtVersion.EditFormat.Inherit")));
			this.txtVersion.EditFormat.NullText = resources.GetString("txtVersion.EditFormat.NullText");
			this.txtVersion.EditFormat.TrimEnd = ((bool)(resources.GetObject("txtVersion.EditFormat.TrimEnd")));
			this.txtVersion.EditFormat.TrimStart = ((bool)(resources.GetObject("txtVersion.EditFormat.TrimStart")));
			this.txtVersion.EditMask = resources.GetString("txtVersion.EditMask");
			this.txtVersion.EmptyAsNull = ((bool)(resources.GetObject("txtVersion.EmptyAsNull")));
			this.txtVersion.Enabled = ((bool)(resources.GetObject("txtVersion.Enabled")));
			this.txtVersion.ErrorInfo.BeepOnError = ((bool)(resources.GetObject("txtVersion.ErrorInfo.BeepOnError")));
			this.txtVersion.ErrorInfo.ErrorMessage = resources.GetString("txtVersion.ErrorInfo.ErrorMessage");
			this.txtVersion.ErrorInfo.ErrorMessageCaption = resources.GetString("txtVersion.ErrorInfo.ErrorMessageCaption");
			this.txtVersion.ErrorInfo.ShowErrorMessage = ((bool)(resources.GetObject("txtVersion.ErrorInfo.ShowErrorMessage")));
			this.txtVersion.ErrorInfo.ValueOnError = ((object)(resources.GetObject("txtVersion.ErrorInfo.ValueOnError")));
			this.txtVersion.Font = ((System.Drawing.Font)(resources.GetObject("txtVersion.Font")));
			this.txtVersion.FormatType = ((C1.Win.C1Input.FormatTypeEnum)(resources.GetObject("txtVersion.FormatType")));
			this.txtVersion.GapHeight = ((int)(resources.GetObject("txtVersion.GapHeight")));
			this.txtVersion.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("txtVersion.ImeMode")));
			this.txtVersion.Increment = ((object)(resources.GetObject("txtVersion.Increment")));
			this.txtVersion.InitialSelection = ((C1.Win.C1Input.InitialSelectionEnum)(resources.GetObject("txtVersion.InitialSelection")));
			this.txtVersion.Location = ((System.Drawing.Point)(resources.GetObject("txtVersion.Location")));
			this.txtVersion.MaskInfo.AutoTabWhenFilled = ((bool)(resources.GetObject("txtVersion.MaskInfo.AutoTabWhenFilled")));
			this.txtVersion.MaskInfo.CaseSensitive = ((bool)(resources.GetObject("txtVersion.MaskInfo.CaseSensitive")));
			this.txtVersion.MaskInfo.CopyWithLiterals = ((bool)(resources.GetObject("txtVersion.MaskInfo.CopyWithLiterals")));
			this.txtVersion.MaskInfo.EditMask = resources.GetString("txtVersion.MaskInfo.EditMask");
			this.txtVersion.MaskInfo.EmptyAsNull = ((bool)(resources.GetObject("txtVersion.MaskInfo.EmptyAsNull")));
			this.txtVersion.MaskInfo.ErrorMessage = resources.GetString("txtVersion.MaskInfo.ErrorMessage");
			this.txtVersion.MaskInfo.Inherit = ((C1.Win.C1Input.MaskInfoInheritFlags)(resources.GetObject("txtVersion.MaskInfo.Inherit")));
			this.txtVersion.MaskInfo.PromptChar = ((char)(resources.GetObject("txtVersion.MaskInfo.PromptChar")));
			this.txtVersion.MaskInfo.ShowLiterals = ((C1.Win.C1Input.ShowLiteralsEnum)(resources.GetObject("txtVersion.MaskInfo.ShowLiterals")));
			this.txtVersion.MaskInfo.StoredEmptyChar = ((char)(resources.GetObject("txtVersion.MaskInfo.StoredEmptyChar")));
			this.txtVersion.MaxLength = ((int)(resources.GetObject("txtVersion.MaxLength")));
			this.txtVersion.Name = "txtVersion";
			this.txtVersion.NullText = resources.GetString("txtVersion.NullText");
			this.txtVersion.ParseInfo.CaseSensitive = ((bool)(resources.GetObject("txtVersion.ParseInfo.CaseSensitive")));
			this.txtVersion.ParseInfo.CustomFormat = resources.GetString("txtVersion.ParseInfo.CustomFormat");
			this.txtVersion.ParseInfo.EmptyAsNull = ((bool)(resources.GetObject("txtVersion.ParseInfo.EmptyAsNull")));
			this.txtVersion.ParseInfo.ErrorMessage = resources.GetString("txtVersion.ParseInfo.ErrorMessage");
			this.txtVersion.ParseInfo.FormatType = ((C1.Win.C1Input.FormatTypeEnum)(resources.GetObject("txtVersion.ParseInfo.FormatType")));
			this.txtVersion.ParseInfo.Inherit = ((C1.Win.C1Input.ParseInfoInheritFlags)(resources.GetObject("txtVersion.ParseInfo.Inherit")));
			this.txtVersion.ParseInfo.NullText = resources.GetString("txtVersion.ParseInfo.NullText");
			this.txtVersion.ParseInfo.NumberStyle = ((C1.Win.C1Input.NumberStyleFlags)(resources.GetObject("txtVersion.ParseInfo.NumberStyle")));
			this.txtVersion.ParseInfo.TrimEnd = ((bool)(resources.GetObject("txtVersion.ParseInfo.TrimEnd")));
			this.txtVersion.ParseInfo.TrimStart = ((bool)(resources.GetObject("txtVersion.ParseInfo.TrimStart")));
			this.txtVersion.PasswordChar = ((char)(resources.GetObject("txtVersion.PasswordChar")));
			this.txtVersion.PostValidation.CaseSensitive = ((bool)(resources.GetObject("txtVersion.PostValidation.CaseSensitive")));
			this.txtVersion.PostValidation.ErrorMessage = resources.GetString("txtVersion.PostValidation.ErrorMessage");
			this.txtVersion.PostValidation.Inherit = ((C1.Win.C1Input.PostValidationInheritFlags)(resources.GetObject("txtVersion.PostValidation.Inherit")));
			this.txtVersion.PostValidation.Intervals.AddRange(new C1.Win.C1Input.ValueInterval[] {
																									 ((C1.Win.C1Input.ValueInterval)(resources.GetObject("txtVersion.PostValidation.Intervals")))});
			this.txtVersion.PostValidation.Validation = ((C1.Win.C1Input.PostValidationTypeEnum)(resources.GetObject("txtVersion.PostValidation.Validation")));
			this.txtVersion.PostValidation.Values = ((System.Array)(resources.GetObject("txtVersion.PostValidation.Values")));
			this.txtVersion.PostValidation.ValuesExcluded = ((System.Array)(resources.GetObject("txtVersion.PostValidation.ValuesExcluded")));
			this.txtVersion.PreValidation.CaseSensitive = ((bool)(resources.GetObject("txtVersion.PreValidation.CaseSensitive")));
			this.txtVersion.PreValidation.ErrorMessage = resources.GetString("txtVersion.PreValidation.ErrorMessage");
			this.txtVersion.PreValidation.Inherit = ((C1.Win.C1Input.PreValidationInheritFlags)(resources.GetObject("txtVersion.PreValidation.Inherit")));
			this.txtVersion.PreValidation.ItemSeparator = resources.GetString("txtVersion.PreValidation.ItemSeparator");
			this.txtVersion.PreValidation.PatternString = resources.GetString("txtVersion.PreValidation.PatternString");
			this.txtVersion.PreValidation.RegexOptions = ((C1.Win.C1Input.RegexOptionFlags)(resources.GetObject("txtVersion.PreValidation.RegexOptions")));
			this.txtVersion.PreValidation.TrimEnd = ((bool)(resources.GetObject("txtVersion.PreValidation.TrimEnd")));
			this.txtVersion.PreValidation.TrimStart = ((bool)(resources.GetObject("txtVersion.PreValidation.TrimStart")));
			this.txtVersion.PreValidation.Validation = ((C1.Win.C1Input.PreValidationTypeEnum)(resources.GetObject("txtVersion.PreValidation.Validation")));
			this.txtVersion.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("txtVersion.RightToLeft")));
			this.txtVersion.ShowFocusRectangle = ((bool)(resources.GetObject("txtVersion.ShowFocusRectangle")));
			this.txtVersion.Size = ((System.Drawing.Size)(resources.GetObject("txtVersion.Size")));
			this.txtVersion.TabIndex = ((int)(resources.GetObject("txtVersion.TabIndex")));
			this.txtVersion.Tag = ((object)(resources.GetObject("txtVersion.Tag")));
			this.txtVersion.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("txtVersion.TextAlign")));
			this.txtVersion.TrimEnd = ((bool)(resources.GetObject("txtVersion.TrimEnd")));
			this.txtVersion.TrimStart = ((bool)(resources.GetObject("txtVersion.TrimStart")));
			this.txtVersion.UserCultureOverride = ((bool)(resources.GetObject("txtVersion.UserCultureOverride")));
			this.txtVersion.Value = ((object)(resources.GetObject("txtVersion.Value")));
			this.txtVersion.VerticalAlign = ((C1.Win.C1Input.VerticalAlignEnum)(resources.GetObject("txtVersion.VerticalAlign")));
			this.txtVersion.Visible = ((bool)(resources.GetObject("txtVersion.Visible")));
			this.txtVersion.VisibleButtons = ((C1.Win.C1Input.DropDownControlButtonFlags)(resources.GetObject("txtVersion.VisibleButtons")));
			// 
			// lblByShift
			// 
			this.lblByShift.AccessibleDescription = resources.GetString("lblByShift.AccessibleDescription");
			this.lblByShift.AccessibleName = resources.GetString("lblByShift.AccessibleName");
			this.lblByShift.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblByShift.Anchor")));
			this.lblByShift.AutoSize = ((bool)(resources.GetObject("lblByShift.AutoSize")));
			this.lblByShift.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblByShift.Dock")));
			this.lblByShift.Enabled = ((bool)(resources.GetObject("lblByShift.Enabled")));
			this.lblByShift.Font = ((System.Drawing.Font)(resources.GetObject("lblByShift.Font")));
			this.lblByShift.Image = ((System.Drawing.Image)(resources.GetObject("lblByShift.Image")));
			this.lblByShift.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblByShift.ImageAlign")));
			this.lblByShift.ImageIndex = ((int)(resources.GetObject("lblByShift.ImageIndex")));
			this.lblByShift.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblByShift.ImeMode")));
			this.lblByShift.Location = ((System.Drawing.Point)(resources.GetObject("lblByShift.Location")));
			this.lblByShift.Name = "lblByShift";
			this.lblByShift.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblByShift.RightToLeft")));
			this.lblByShift.Size = ((System.Drawing.Size)(resources.GetObject("lblByShift.Size")));
			this.lblByShift.TabIndex = ((int)(resources.GetObject("lblByShift.TabIndex")));
			this.lblByShift.Text = resources.GetString("lblByShift.Text");
			this.lblByShift.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblByShift.TextAlign")));
			this.lblByShift.Visible = ((bool)(resources.GetObject("lblByShift.Visible")));
			// 
			// chkUseCacheAsBeginStock
			// 
			this.chkUseCacheAsBeginStock.AccessibleDescription = resources.GetString("chkUseCacheAsBeginStock.AccessibleDescription");
			this.chkUseCacheAsBeginStock.AccessibleName = resources.GetString("chkUseCacheAsBeginStock.AccessibleName");
			this.chkUseCacheAsBeginStock.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("chkUseCacheAsBeginStock.Anchor")));
			this.chkUseCacheAsBeginStock.Appearance = ((System.Windows.Forms.Appearance)(resources.GetObject("chkUseCacheAsBeginStock.Appearance")));
			this.chkUseCacheAsBeginStock.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("chkUseCacheAsBeginStock.BackgroundImage")));
			this.chkUseCacheAsBeginStock.CheckAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("chkUseCacheAsBeginStock.CheckAlign")));
			this.chkUseCacheAsBeginStock.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("chkUseCacheAsBeginStock.Dock")));
			this.chkUseCacheAsBeginStock.Enabled = ((bool)(resources.GetObject("chkUseCacheAsBeginStock.Enabled")));
			this.chkUseCacheAsBeginStock.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("chkUseCacheAsBeginStock.FlatStyle")));
			this.chkUseCacheAsBeginStock.Font = ((System.Drawing.Font)(resources.GetObject("chkUseCacheAsBeginStock.Font")));
			this.chkUseCacheAsBeginStock.Image = ((System.Drawing.Image)(resources.GetObject("chkUseCacheAsBeginStock.Image")));
			this.chkUseCacheAsBeginStock.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("chkUseCacheAsBeginStock.ImageAlign")));
			this.chkUseCacheAsBeginStock.ImageIndex = ((int)(resources.GetObject("chkUseCacheAsBeginStock.ImageIndex")));
			this.chkUseCacheAsBeginStock.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("chkUseCacheAsBeginStock.ImeMode")));
			this.chkUseCacheAsBeginStock.Location = ((System.Drawing.Point)(resources.GetObject("chkUseCacheAsBeginStock.Location")));
			this.chkUseCacheAsBeginStock.Name = "chkUseCacheAsBeginStock";
			this.chkUseCacheAsBeginStock.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("chkUseCacheAsBeginStock.RightToLeft")));
			this.chkUseCacheAsBeginStock.Size = ((System.Drawing.Size)(resources.GetObject("chkUseCacheAsBeginStock.Size")));
			this.chkUseCacheAsBeginStock.TabIndex = ((int)(resources.GetObject("chkUseCacheAsBeginStock.TabIndex")));
			this.chkUseCacheAsBeginStock.Text = resources.GetString("chkUseCacheAsBeginStock.Text");
			this.chkUseCacheAsBeginStock.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("chkUseCacheAsBeginStock.TextAlign")));
			this.chkUseCacheAsBeginStock.Visible = ((bool)(resources.GetObject("chkUseCacheAsBeginStock.Visible")));
			// 
			// DCOptions
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
			this.Controls.Add(this.chkUseCacheAsBeginStock);
			this.Controls.Add(this.lblByShift);
			this.Controls.Add(this.txtVersion);
			this.Controls.Add(this.lblVersion);
			this.Controls.Add(this.lblMonth);
			this.Controls.Add(this.lblYear);
			this.Controls.Add(this.cboMonth);
			this.Controls.Add(this.cboYear);
			this.Controls.Add(this.cboGroupBy);
			this.Controls.Add(this.lblByHour);
			this.Controls.Add(this.lblByDay);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.chkOnHand);
			this.Controls.Add(this.chkSafetyStock);
			this.Controls.Add(this.lblPlanHorizon);
			this.Controls.Add(this.txtPlanHorizon);
			this.Controls.Add(this.cboScheduleType);
			this.Controls.Add(this.cboScheduleCode);
			this.Controls.Add(this.btnProcess);
			this.Controls.Add(this.txtMPSCycle);
			this.Controls.Add(this.txtCycle);
			this.Controls.Add(this.txtDescription);
			this.Controls.Add(this.btnRemoveDCPResults);
			this.Controls.Add(this.chkCheckPoint);
			this.Controls.Add(this.dtmToDate);
			this.Controls.Add(this.dgrdData);
			this.Controls.Add(this.lblLoadAveraging);
			this.Controls.Add(this.lblFiniteScheduling);
			this.Controls.Add(this.lblInfiniteScheduling);
			this.Controls.Add(this.lblScheduleType);
			this.Controls.Add(this.dtmLastUpdate);
			this.Controls.Add(this.btnMPSCycle);
			this.Controls.Add(this.btnSearchCycle);
			this.Controls.Add(this.chkIgnoreMoveTime);
			this.Controls.Add(this.cboCCN);
			this.Controls.Add(this.dtmFromDate);
			this.Controls.Add(this.lblLastUpdate);
			this.Controls.Add(this.btnEdit);
			this.Controls.Add(this.btnDelete);
			this.Controls.Add(this.btnSave);
			this.Controls.Add(this.btnAdd);
			this.Controls.Add(this.btnClose);
			this.Controls.Add(this.btnHelp);
			this.Controls.Add(this.lblCCN);
			this.Controls.Add(this.lblDescription);
			this.Controls.Add(this.lblCycle);
			this.Controls.Add(this.lblMaxDay);
			this.Controls.Add(this.lblAsOfDate);
			this.Controls.Add(this.lblMPSCycle);
			this.Controls.Add(this.lblMPS_Schedule_Code);
			this.Controls.Add(this.textBox1);
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
			this.Name = "DCOptions";
			this.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("$this.RightToLeft")));
			this.StartPosition = ((System.Windows.Forms.FormStartPosition)(resources.GetObject("$this.StartPosition")));
			this.Text = resources.GetString("$this.Text");
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DCOptions_KeyDown);
			this.Closing += new System.ComponentModel.CancelEventHandler(this.DCOptions_Closing);
			this.Load += new System.EventHandler(this.DCOptions_Load);
			((System.ComponentModel.ISupportInitialize)(this.dtmFromDate)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.cboCCN)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dtmLastUpdate)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dgrdData)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dtmToDate)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.cboScheduleCode)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.cboScheduleType)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.txtVersion)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion
		
		#region Methods

		private void InitComboBoxes()
		{
			const string SCHEDULE_CODE_TABLE = "tblScheduleCode";
			const string SCHEDULE_TYPE_TABLE = "tblScheduleType";

			//============================================================
			//Schedule Code table
			//============================================================
			DataTable dtbScheduleCode = new DataTable(SCHEDULE_CODE_TABLE);
			dtbScheduleCode.Columns.Add(VALUE_FLD, typeof(System.Int32));
			dtbScheduleCode.Columns.Add(NAME_FLD, typeof(System.String));
			
			#region Hide forward: Proposal no. 3161 by ThuyPT.			
			/*
			//forward 
			DataRow drowNew1 = dtbScheduleCode.NewRow();
			drowNew1[VALUE_FLD] = (int)ScheduleMethodEnum.Forward;
			drowNew1[NAME_FLD] = ScheduleMethodEnum.Forward.ToString();			
			dtbScheduleCode.Rows.Add(drowNew1);
			*/
			#endregion

			//backward 
			DataRow drowNew2 = dtbScheduleCode.NewRow();
			drowNew2[VALUE_FLD] = (int)ScheduleMethodEnum.Backward;
			drowNew2[NAME_FLD] = ScheduleMethodEnum.Backward.ToString();			
			dtbScheduleCode.Rows.Add(drowNew2);

			//============================================================
			//Schedule Type table
			//============================================================
			DataTable dtbScheduleType = new DataTable(SCHEDULE_TYPE_TABLE);
			dtbScheduleType.Columns.Add(VALUE_FLD, typeof(System.Int32));
			dtbScheduleType.Columns.Add(NAME_FLD, typeof(System.String));			
			
			#region Hide Infinite Scheduling: Proposal no. 3161 by ThuyPT.			
			/*
			//Infinite Scheduling 
			DataRow drowInfiniteScheduling = dtbScheduleType.NewRow();
			drowInfiniteScheduling[VALUE_FLD] = (int)ScheduleType.InfiniteScheduling;
			drowInfiniteScheduling[NAME_FLD] = lblInfiniteScheduling.Text;
			dtbScheduleType.Rows.Add(drowInfiniteScheduling);
			*/
			#endregion

			#region Hide Load Averaging Scheduling: Proposal no. 3161 by ThuyPT.			
			/*
			//Load Averaging 
			DataRow drowLoadAveraging = dtbScheduleType.NewRow();
			drowLoadAveraging[VALUE_FLD] = (int)ScheduleType.LoadAveraging;
			drowLoadAveraging[NAME_FLD] = lblLoadAveraging.Text;
			dtbScheduleType.Rows.Add(drowLoadAveraging);
			*/
			#endregion

			//Finite Scheduling
			DataRow drowFiniteScheduling = dtbScheduleType.NewRow();
			drowFiniteScheduling[VALUE_FLD] = (int)ScheduleType.FiniteScheduling;
			drowFiniteScheduling[NAME_FLD] = lblFiniteScheduling.Text;
			dtbScheduleType.Rows.Add(drowFiniteScheduling);			

			//Bind to combobox
			FormControlComponents.PutDataIntoC1ComboBox(cboScheduleCode, dtbScheduleCode, NAME_FLD, VALUE_FLD, SCHEDULE_CODE_TABLE, true);
			FormControlComponents.PutDataIntoC1ComboBox(cboScheduleType, dtbScheduleType, NAME_FLD, VALUE_FLD, SCHEDULE_TYPE_TABLE, true);

			cboScheduleCode.SelectedIndex = -1;
			cboScheduleType.SelectedIndex = -1;
		}
		
		/// <summary>
		/// Assign data from DataRow to VO object and controls
		/// </summary>
		/// <param name="drowMaster"></param>
		private void DataRow2VOAndControls(DataRow drowMaster)
		{
			const string METHOD_NAME = THIS + ".DataRow2VOAndControls()";

			try
			{
				if(drowMaster != null)
				{	
					//Set value to VO object
//					if(!drowMaster[PRO_DCOptionMasterTable.MPSCYCLEOPTIONMASTERID_FLD].Equals(DBNull.Value))
//					{
//						voDCOptionMaster.MPSCycleOptionMasterID = int.Parse(drowMaster[PRO_DCOptionMasterTable.MPSCYCLEOPTIONMASTERID_FLD].ToString());
//					}
//					else
//					{
//						voDCOptionMaster.MPSCycleOptionMasterID = 0;
//					}

					voDCOptionMaster.DCOptionMasterID = int.Parse(drowMaster[PRO_DCOptionMasterTable.DCOPTIONMASTERID_FLD].ToString());
					voDCOptionMaster.Cycle = drowMaster[PRO_DCOptionMasterTable.CYCLE_FLD].ToString();
					voDCOptionMaster.CCNID = int.Parse(drowMaster[PRO_DCOptionMasterTable.CCNID_FLD].ToString());
					voDCOptionMaster.Description = drowMaster[PRO_DCOptionMasterTable.DESCRIPTION_FLD].ToString();
					 
					voDCOptionMaster.ScheduleType = int.Parse(drowMaster[PRO_DCOptionMasterTable.SCHEDULETYPE_FLD].ToString());
					voDCOptionMaster.ScheduleCode = int.Parse(drowMaster[PRO_DCOptionMasterTable.SCHEDULECODE_FLD].ToString());
					
					voDCOptionMaster.IgnoreMoveTime = bool.Parse(drowMaster[PRO_DCOptionMasterTable.IGNOREMOVETIME_FLD].ToString());
					voDCOptionMaster.IncludeCheckPoint = bool.Parse(drowMaster[PRO_DCOptionMasterTable.INCLUDECHECKPOINT_FLD].ToString());
					voDCOptionMaster.SafetyStock = bool.Parse(drowMaster[PRO_DCOptionMasterTable.SAFETYSTOCK_FLD].ToString());
					voDCOptionMaster.OnHand = bool.Parse(drowMaster[PRO_DCOptionMasterTable.ONHAND_FLD].ToString());
					if ((drowMaster[PRO_DCOptionMasterTable.VERSION_FLD] != null)
						&& (drowMaster[PRO_DCOptionMasterTable.VERSION_FLD] != DBNull.Value))
					{
						voDCOptionMaster.Version = int.Parse(drowMaster[PRO_DCOptionMasterTable.VERSION_FLD].ToString());
					}
					if ((drowMaster[PRO_DCOptionMasterTable.PLANNINGPERIOD_FLD] != DBNull.Value)
						&&(drowMaster[PRO_DCOptionMasterTable.PLANNINGPERIOD_FLD] != null))
					{
						voDCOptionMaster.PlanningPeriod = (DateTime) drowMaster[PRO_DCOptionMasterTable.PLANNINGPERIOD_FLD];	
					}
					else
						voDCOptionMaster.PlanningPeriod = (new DateTime(1,1,1));
					voDCOptionMaster.PlanHorizon = int.Parse(drowMaster[PRO_DCOptionMasterTable.PLANHORIZON_FLD].ToString());
					voDCOptionMaster.GroupBy = int.Parse(drowMaster[PRO_DCOptionMasterTable.GROUPBY_FLD].ToString());
					voDCOptionMaster.AsOfDate = (DateTime) drowMaster[PRO_DCOptionMasterTable.ASOFDATE_FLD];
//					if(!drowMaster[MAXDAYS_FLD].Equals(DBNull.Value))
//					{
//						intMaxDays = int.Parse(drowMaster[MAXDAYS_FLD].ToString());
//					}
//					else
//					{
//						intMaxDays = 0;
//					}
					
//					if(!drowMaster[PRO_DCOptionMasterTable.MPSCYCLEOPTIONMASTERID_FLD].Equals(DBNull.Value))
//					{
//						voDCOptionMaster.MPSCycleOptionMasterID = int.Parse(drowMaster[PRO_DCOptionMasterTable.MPSCYCLEOPTIONMASTERID_FLD].ToString());
//					}
//					else
//					{
//						voDCOptionMaster.MPSCycleOptionMasterID = 0;
//					}

					if(!drowMaster[PRO_DCOptionMasterTable.LASTUPDATE_FLD].Equals(DBNull.Value))
					{
						voDCOptionMaster.LastUpdate= DateTime.Parse(drowMaster[PRO_DCOptionMasterTable.LASTUPDATE_FLD].ToString());
						dtmLastUpdate.Value = voDCOptionMaster.LastUpdate;
					}
					else
					{						
						voDCOptionMaster.LastUpdate = DateTime.MinValue;
						dtmLastUpdate.Value = DBNull.Value;
					}

					if(!drowMaster[PRO_DCOptionMasterTable.USECACHE_ASBEGIN_FLD].Equals(DBNull.Value))
					{
						voDCOptionMaster.UseCacheAsBegin= Convert.ToBoolean(drowMaster[PRO_DCOptionMasterTable.USECACHE_ASBEGIN_FLD].ToString());
						chkUseCacheAsBeginStock.Checked = voDCOptionMaster.UseCacheAsBegin;
					}
					else
					{						
						voDCOptionMaster.UseCacheAsBegin = false;
						chkUseCacheAsBeginStock.Checked = false;
					}

					//Set to controls
					txtCycle.Text = voDCOptionMaster.Cycle;					
					txtCycle.Tag = voDCOptionMaster.DCOptionMasterID;
					txtDescription.Text = voDCOptionMaster.Description;
					//dtmAsOfDate.Value = DateTime.Parse(drowMaster[MTR_MPSCycleOptionMasterTable.ASOFDATE_FLD].ToString());
					dtmFromDate.Value = (DateTime) drowMaster[PRO_DCOptionMasterTable.ASOFDATE_FLD];
					txtPlanHorizon.Text = drowMaster[PRO_DCOptionMasterTable.PLANHORIZON_FLD].ToString();
					dtmToDate.Value = ((DateTime)dtmFromDate.Value).AddDays(int.Parse(txtPlanHorizon.Text));
					cboGroupBy.SelectedIndex = Byte.Parse(drowMaster[PRO_DCOptionMasterTable.GROUPBY_FLD].ToString());
					chkIgnoreMoveTime.Checked = voDCOptionMaster.IgnoreMoveTime;
					chkCheckPoint.Checked = voDCOptionMaster.IncludeCheckPoint;                           
					chkSafetyStock.Checked = voDCOptionMaster.SafetyStock;                           
					chkOnHand.Checked = voDCOptionMaster.OnHand;                          
					if (voDCOptionMaster.PlanningPeriod != (new DateTime(1,1,1)))
					{
						cboYear.SelectedIndex = voDCOptionMaster.PlanningPeriod.Year - 1999;
						cboMonth.SelectedIndex = voDCOptionMaster.PlanningPeriod.Month;
					}
					else
					{
						cboYear.SelectedIndex = -1;
						cboMonth.SelectedIndex = -1;
					}
					txtVersion.Value = voDCOptionMaster.Version;
//					txtMPSCycle.Text = drowMaster[MTR_MPSCycleOptionMasterTable.TABLE_NAME + MTR_MPSCycleOptionMasterTable.CYCLE_FLD].ToString();
//					txtMPSCycle.Tag = voDCOptionMaster.MPSCycleOptionMasterID;
					//dtmToDate.Value = ((DateTime) dtmAsOfDate.Value).AddDays(intMaxDays);
					cboCCN.SelectedValue = voDCOptionMaster.CCNID;

					cboScheduleCode.SelectedValue = voDCOptionMaster.ScheduleCode;
					cboScheduleType.SelectedValue = voDCOptionMaster.ScheduleType;					
				}
				else
				{					
					ResetControlValue(enuFormAction);
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
		/// This method will retrieve OutsideProcessing information to fill into controls
		/// in the form, including detail data for the grid
		/// </summary>
		/// <param name="pintMasterId"></param>
		/// <author> Tran Quoc Tuan </author>
		/// <created date> 
		/// 15 August 2005
		/// </created>		
		private void LoadDCOptionDetail(int pintMasterId)
		{
			const string METHOD_NAME = THIS + ".LoadDCOptionDetail()";
			try
			{   
				//Load blank data if master id is invalid id 
				if(pintMasterId <= 0)
				{
					ResetControlValue(enuFormAction);					
				}
				else
				{	
					//call bo's method tho retrieve data
					DataRow2VOAndControls(boDCOptions.GetDCOptionMaster(pintMasterId));	
//					dtbDCOptionDetail = boDCOptions.GetDetailByMaster(pintMasterId).Tables[0];
//					
//					// bind to grid & reformat grid
//					dgrdData.DataSource = dtbDCOptionDetail;				
//					FormatDataGrid();
				}

				LockControl(false);
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
		/// process data inputed into the grid
		/// </summary>
		/// <param name="pstrColumnName"></param>
		/// <param name="pstrColumValue"></param>
		/// <returns>If no error then return true, else return false</returns>
		private bool ProcessInputDataInGrid(string pstrColumnName, string pstrColumValue, bool pblnAlwaysShow)
		{
			const string METHOD_NAME = THIS +  ".ProcessInputDataInGrid()";
			try
			{
				if(pstrColumnName == PRO_DCOptionDetailTable.WORKORDER_FLD)
				{
					return true;
				}

				Hashtable htbCondition = new Hashtable(); 
				DataRowView drvResult = null;
				bool blnResult = true;				

				//Check for each column
				switch (pstrColumnName)
				{
					case MST_MasterLocationTable.TABLE_NAME + MST_MasterLocationTable.CODE_FLD:
						//clear hash table for new condition
						htbCondition.Clear();
						htbCondition.Add(MST_MasterLocationTable.CCNID_FLD, cboCCN.SelectedValue);
						
						// Call OpenSearchForm for Work Order selecting 
						drvResult = FormControlComponents.OpenSearchForm(MST_MasterLocationTable.TABLE_NAME, MST_MasterLocationTable.CODE_FLD, pstrColumValue, htbCondition, pblnAlwaysShow);
						if (drvResult != null)
						{							
							if(!dgrdData[dgrdData.Row, MST_MasterLocationTable.MASTERLOCATIONID_FLD].Equals(drvResult[PRO_DCOptionDetailTable.MASTERLOCATIONID_FLD]))
							{
								//Check duplicate key
								for(int i = 0; i < dgrdData.RowCount; i++)
								{
									if(dgrdData[i, MST_MasterLocationTable.MASTERLOCATIONID_FLD].Equals(drvResult[PRO_DCOptionDetailTable.MASTERLOCATIONID_FLD]) && (i != dgrdData.Row))
									{
										PCSMessageBox.Show(ErrorCode.DUPLICATE_KEY, MessageBoxIcon.Exclamation);
										return false;
									}
								}
								dgrdData[dgrdData.Row, PRO_DCOptionDetailTable.WORKORDER_FLD] = false;
							}

							//Fill data
							if(!pblnAlwaysShow)
							{
								dgrdData.Columns[PRO_DCOptionDetailTable.MASTERLOCATIONID_FLD].Value = drvResult[MST_MasterLocationTable.MASTERLOCATIONID_FLD];
								dgrdData.Columns[MST_MasterLocationTable.TABLE_NAME + MST_MasterLocationTable.CODE_FLD].Value = drvResult[MST_MasterLocationTable.CODE_FLD];								
							}
							else
							{
								dgrdData[dgrdData.Row, PRO_DCOptionDetailTable.MASTERLOCATIONID_FLD] = drvResult[MST_MasterLocationTable.MASTERLOCATIONID_FLD];
								dgrdData[dgrdData.Row, MST_MasterLocationTable.TABLE_NAME + MST_MasterLocationTable.CODE_FLD] = drvResult[MST_MasterLocationTable.CODE_FLD];								
							}
						}
						else
						{
							blnResult = pblnAlwaysShow;
						}
						
						break;				
				}			

				return blnResult;
			}
			catch(PCSException ex)
			{
				throw new PCSException(ex.mCode, METHOD_NAME, ex);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message, ex);
			}
		}

		/// <summary>
		/// Build structure of xx table for binding to grid
		/// </summary>
		/// <remarks>
		/// Structure of this table based on struct which be returned by calling
		/// xx.GetDetailByMaster() method.
		/// So we should keep them always are identical.
		/// </remarks>
		/// <returns>DataTable</returns>
		private DataTable BuildDetailTable()
		{
			try
			{
				//Create table
				DataTable dtbDetail = new DataTable(PRO_DCOptionDetailTable.TABLE_NAME);
				//Add columns
				dtbDetail.Columns.Add(PRO_DCOptionDetailTable.DCOPTIONMASTERID_FLD, typeof(System.Int32));
				dtbDetail.Columns.Add(PRO_DCOptionDetailTable.DCOPTIONDETAILID_FLD, typeof(System.Int32));
				dtbDetail.Columns.Add(PRO_DCOptionDetailTable.MASTERLOCATIONID_FLD, typeof(System.Int32));
				dtbDetail.Columns.Add(MST_MasterLocationTable.TABLE_NAME + MST_MasterLocationTable.CODE_FLD, typeof(System.String));
				dtbDetail.Columns.Add(PRO_DCOptionDetailTable.WORKORDER_FLD, typeof(System.Boolean));	

				dtbDetail.Columns[PRO_DCOptionDetailTable.WORKORDER_FLD].DefaultValue = false;

				return dtbDetail;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message, ex);
			}
		}

		/// <summary>
		/// Lock controls for updating
		/// </summary>
		/// <param name="pblnLock"></param>
		/// <author> Tran Quoc Tuan </author>
		/// <created date> 
		/// 15 June 2005
		/// </created>
		private void LockControl(bool pblnLock)
		{
            dgrdData.AllowAddNew = pblnLock;
            dgrdData.AllowDelete = pblnLock;
            dgrdData.AllowUpdate = pblnLock;

            //Set select buttons for grid				
            dgrdData.Splits[0].DisplayColumns[MST_MasterLocationTable.TABLE_NAME + MST_MasterLocationTable.CODE_FLD].Button = pblnLock;

            btnSave.Enabled = pblnLock;
            txtDescription.Enabled = pblnLock;
            txtMPSCycle.Enabled = pblnLock;
            btnMPSCycle.Enabled = pblnLock;
            cboScheduleCode.Enabled = false;//pblnLock;
            cboScheduleCode.SelectedIndex = 1;
            cboScheduleType.Enabled = false;//pblnLock;
            cboScheduleType.SelectedIndex = 1;
            cboGroupBy.Enabled = pblnLock;
            dtmFromDate.Enabled = pblnLock;
            dtmToDate.Enabled = pblnLock;
            //numMaxDays.Enabled = pblnLock;
            chkIgnoreMoveTime.Enabled = pblnLock;
            chkCheckPoint.Enabled = pblnLock;
            chkSafetyStock.Enabled = pblnLock;
            chkOnHand.Enabled = pblnLock;
            chkUseCacheAsBeginStock.Enabled = pblnLock;
            txtVersion.Enabled = pblnLock;
            cboMonth.Enabled = pblnLock;
            cboYear.Enabled = pblnLock;
            //
            btnAdd.Enabled = !pblnLock;
            btnEdit.Enabled = !pblnLock;
            btnDelete.Enabled = !pblnLock;
            btnSearchCycle.Enabled = !pblnLock;

            #region // HACK: DuongNA 2005-11-28
            btnRemoveDCPResults.Enabled = (voDCOptionMaster.DCOptionMasterID > 0) && (enuFormAction != EnumAction.Add);
            btnProcess.Enabled = (voDCOptionMaster.DCOptionMasterID > 0) && (enuFormAction != EnumAction.Add);
            #endregion // END: DuongNA 2005-11-28	
		}

		/// <summary>
		/// Fill related data on controls when select Cycle
		/// </summary>
		/// <param name="pblnAlwaysShowDialog"></param>
		private bool SelectCycle(string pstrMethodName, bool pblnAlwaysShowDialog)
		{
			try				
			{
				Hashtable htbCriteria = new Hashtable();
				DataRowView drwResult = null;
				//DataRow drowDCOption = null; 

				if(cboCCN.SelectedValue != null)
				{
					htbCriteria.Add(MST_MasterLocationTable.CCNID_FLD, cboCCN.SelectedValue);
				}
				else
				{
					htbCriteria.Add(MST_MasterLocationTable.CCNID_FLD, 0);
				}

				//Call OpenSearchForm for selecting MPS planning cycle
				drwResult = FormControlComponents.OpenSearchForm(PRO_DCOptionMasterTable.TABLE_NAME, PRO_DCOptionMasterTable.CYCLE_FLD, txtCycle.Text, htbCriteria, pblnAlwaysShowDialog);
				
				// If has Master location matched searching condition, fill values to form's controls
				if(drwResult != null)
				{
					voDCOptionMaster.DCOptionMasterID = int.Parse(drwResult[PRO_DCOptionMasterTable.DCOPTIONMASTERID_FLD].ToString());						
					LoadDCOptionDetail(voDCOptionMaster.DCOptionMasterID);
					
					//Reset modify status
					txtCycle.Modified = false;
				}
				else if(!pblnAlwaysShowDialog)
				{	
					txtCycle.Tag = ZERO_STRING;
					txtCycle.Focus();
					return false;
				}

				return true;
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
		/// Validate data before updating into database
		/// </summary>
		/// <returns></returns>
		/// <author> Tran Quoc Tuan </author>
		/// <created date> 
		/// 15 June 2005
		/// </created>
		private bool ValidateData()
		{
			const string METHOD_NAME = THIS + ".ValidateData()";
			try
			{
				UtilsBO boUtils = new UtilsBO();

				//Check Cycle no
				if (FormControlComponents.CheckMandatory(cboCCN) || (cboCCN.Text.Trim() == string.Empty))
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Exclamation);
					cboCCN.Focus();				
					return false;
				}

				//Check Cycle no
				if (FormControlComponents.CheckMandatory(txtCycle))
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Exclamation);
					txtCycle.Focus();				
					return false;
				}
				
				if( (boDCOptions.GetMasterVO(txtCycle.Text) != null) && !txtCycle.Text.Trim().ToUpper().Equals(voDCOptionMaster.Cycle.ToUpper()))
				{
					PCSMessageBox.Show(ErrorCode.DUPLICATE_KEY, MessageBoxIcon.Error);
					txtCycle.Focus();
					return false;
				}

				//Check MPS Cycle

				#region  DEL Trada 15-02-2006

//				if (FormControlComponents.CheckMandatory(txtMPSCycle))
//				{
//					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Exclamation);
//					txtMPSCycle.Focus();				
//					return false;
//				}

				#endregion		

				// HACK: Trada 15-02-2006
				if (FormControlComponents.CheckMandatory(dtmFromDate))
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Exclamation);
					dtmFromDate.Focus();				
					return false;
				}
				if (FormControlComponents.CheckMandatory(dtmToDate))
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Exclamation);
					dtmToDate.Focus();				
					return false;
				}
				if (FormControlComponents.CheckMandatory(txtVersion))
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Exclamation);
					txtVersion.Focus();				
					return false;
				}

				//Check Year
				if (cboYear.SelectedIndex == -1)
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Exclamation);
					cboYear.Focus();				
					return false;
				}
				//Check Month
				if (cboMonth.SelectedIndex == -1)
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Exclamation);
					cboMonth.Focus();				
					return false;
				}
				// END: Trada 15-02-2006

				#region Rem by Tuan TQ
				/*
				//Check As Of date
				if (FormControlComponents.CheckMandatory(dtmAsOfDate))
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Exclamation);
					dtmAsOfDate.Focus();				
					return false;
				}				
				
				DateTime dtmAsOfDateTemp = DateTime.Parse(dtmAsOfDate.Value.ToString());
				DateTime dtmDBDateTemp = boUtils.GetDBDate();

				dtmAsOfDateTemp = new DateTime(dtmAsOfDateTemp.Year, dtmAsOfDateTemp.Month, dtmAsOfDateTemp.Day);
				dtmDBDateTemp = new DateTime(dtmDBDateTemp.Year, dtmDBDateTemp.Month, dtmDBDateTemp.Day);
				
				//Compare as of date to DB date
				if(dtmAsOfDateTemp < dtmDBDateTemp)// && enuFormAction == EnumAction.Add)
				{
					PCSMessageBox.Show(ErrorCode.MESSAGE_DCP_ASOFDATE_MUST_IN_FUTURE, MessageBoxIcon.Exclamation);					
					dtmAsOfDate.Focus();
					return false;
				}
				*/
				#endregion

				//Check Group By
				if (cboGroupBy.SelectedIndex == -1)
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Exclamation);
					cboGroupBy.Focus();				
					return false;
				}
	
				//Check ScheduleCode
				if ( (cboScheduleCode.Text.Trim() == string.Empty) || (cboScheduleCode.SelectedIndex <= 0) )
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Exclamation);
					cboScheduleCode.Focus();				
					return false;
				}

				//Check ScheduleCode
				if ( (cboScheduleType.Text.Trim() == string.Empty) || (cboScheduleType.SelectedIndex <= 0) )
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Exclamation);
					cboScheduleType.Focus();			
					return false;
				}				
				
				//Call update data to force grid update data
				dgrdData.UpdateData();

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
		/// Fill related data on controls when select Cycle
		/// </summary>
		/// <param name="pblnAlwaysShowDialog"></param>
		private bool SelectMPSCycle(string pstrMethodName, bool pblnAlwaysShowDialog)
		{
			try				
			{
				Hashtable htbCriteria = new Hashtable();
				DataRowView drwResult = null;				

				if(cboCCN.SelectedValue != null)
				{
					htbCriteria.Add(MTR_MPSCycleOptionMasterTable.CCNID_FLD, cboCCN.SelectedValue);
				}
				else
				{
					htbCriteria.Add(MTR_MPSCycleOptionMasterTable.CCNID_FLD, 0);
				}

				//Call OpenSearchForm for selecting MPS planning cycle
				drwResult = FormControlComponents.OpenSearchForm(MTR_MPSCycleOptionMasterTable.TABLE_NAME, MTR_MPSCycleOptionMasterTable.CYCLE_FLD, txtMPSCycle.Text, htbCriteria, pblnAlwaysShowDialog);
				
				// If has Master location matched searching condition, fill values to form's controls
				if(drwResult != null)
				{
					txtMPSCycle.Text = drwResult[MTR_MPSCycleOptionMasterTable.CYCLE_FLD].ToString();
					txtMPSCycle.Tag = drwResult[MTR_MPSCycleOptionMasterTable.MPSCYCLEOPTIONMASTERID_FLD];
					
					//Hack: Trada 22-11-2005

					#region  DEL Trada 15-02-2006

//					dtmAsOfDate.Value = drwResult[MTR_MPSCycleOptionMasterTable.ASOFDATE_FLD];
//					if (drwResult[MTR_MPSCycleOptionMasterTable.PLANHORIZON_FLD].ToString() != string.Empty)
//					{
//						intMaxDays = int.Parse(drwResult[MTR_MPSCycleOptionMasterTable.PLANHORIZON_FLD].ToString());
//					} 
//					else
//					{
//						intMaxDays = 0;
//					}
//					
//					dtmToDate.Value = ((DateTime)dtmAsOfDate.Value).AddDays(intMaxDays);

					#endregion 

					// END: Trada 22-11-2005

					//Reset modify status
					txtMPSCycle.Modified = false;
				}
				else
				{
					if(!pblnAlwaysShowDialog)
					{
						txtMPSCycle.Tag = ZERO_STRING;
						txtMPSCycle.Focus();
						return false;
					}
				}
				return true;
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

		private void FormatDataGrid()
		{
			try
			{	
				//Restore layout
				FormControlComponents.RestoreGridLayout(dgrdData, dtbGridLayOut);			
				
				//Change display format				
				dgrdData.Columns[PRO_DCOptionDetailTable.WORKORDER_FLD].ValueItems.Presentation = C1.Win.C1TrueDBGrid.PresentationEnum.CheckBox;
				dgrdData.Columns[PRO_DCOptionDetailTable.WORKORDER_FLD].ValueItems.Translate = true;				
				dgrdData.Columns[PRO_DCOptionDetailTable.WORKORDER_FLD].DefaultValue = false.ToString();
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message, ex);
			}
		}
		
		/// <summary>
		/// Reset value of controls for updating
		/// </summary>
		/// <param name="penuFormAction"></param>
		/// <author> Tran Quoc Tuan </author>
		/// <created date> 
		/// 15 June 2005
		/// </created>
		private void ResetControlValue(EnumAction penuFormAction)
		{
			try
			{
				//if action id add then get default value for Post Date and TransNo
				if(penuFormAction == EnumAction.Add)
				{
					UtilsBO boUtil = new UtilsBO();
					//dtmAsOfDate.Value = boUtil.GetDBDate();
				}
				else
				{
					//dtmAsOfDate.Value = DBNull.Value;
				}				
				
				voDCOptionMaster.Cycle = string.Empty;
				voDCOptionMaster.DCOptionMasterID = 0;
				
				//Clear control's value
				txtCycle.Text = string.Empty;
				txtCycle.Tag = ZERO_STRING;
				txtDescription.Text = string.Empty;
				txtMPSCycle.Text = string.Empty;
				txtMPSCycle.Tag = ZERO_STRING;
				txtVersion.Value = DBNull.Value;
				cboScheduleCode.SelectedIndex = -1;
				cboGroupBy.SelectedIndex = -1;
				cboScheduleType.SelectedIndex = -1;
				dtmLastUpdate.Value = DBNull.Value;
				dtmFromDate.Value = DBNull.Value;
				dtmToDate.Value = DBNull.Value;
				txtPlanHorizon.Text = string.Empty;
				cboMonth.SelectedIndex = -1;
				cboYear.SelectedIndex = -1;
				txtVersion.Value = DBNull.Value;
				//numMaxDays.Value = DBNull.Value;			
				chkIgnoreMoveTime.Checked = false;
				chkCheckPoint.Checked = false;
				chkSafetyStock.Checked = false;
				chkOnHand.Checked = false;
				//create blank detail table
				dtbDCOptionDetail = BuildDetailTable();
				// bind to grid & reformat grid
				dgrdData.DataSource = dtbDCOptionDetail;
				FormatDataGrid();
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message, ex);
			}
		}

		#endregion Methods

		#region Event Prcessing

		private void DCOptions_Load(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".DCOptions_Load()";
			try
			{
				this.Name = THIS;
				// Create BO
				boDCOptions = new DCOptionsBO();
				enuFormAction = EnumAction.Default;
				
				//Set form security
				Security objSecurity = new Security();		
				
				if (objSecurity.SetRightForUserOnForm(this, SystemProperty.UserName) == 0)
				{
					// You don't have the right to view this item
					PCSMessageBox.Show(ErrorCode.MESSAGE_YOU_HAVE_NO_RIGHT_TO_VIEW, MessageBoxIcon.Warning);
					this.Close();
					return;
				}
				//Lock Form
				this.FormBorderStyle = FormBorderStyle.FixedSingle;
				this.MaximizeBox = false;
				//Load CCN and set default CCN
				UtilsBO boUtils = new UtilsBO();
				DataSet dstCCN = boUtils.ListCCN();				
				FormControlComponents.PutDataIntoC1ComboBox(cboCCN, dstCCN.Tables[MST_CCNTable.TABLE_NAME], MST_CCNTable.CODE_FLD, MST_CCNTable.CCNID_FLD, MST_CCNTable.TABLE_NAME);
				
				//Set default CCN for CNN combobox
				if (SystemProperty.CCNID != 0)
				{
					cboCCN.SelectedValue = SystemProperty.CCNID;
				}			
				
				//get grid layout
				dtbGridLayOut = FormControlComponents.StoreGridLayout(dgrdData);
				
				InitComboBoxes();
				//Init Group by Combo box
				cboGroupBy.Items.Clear();
				cboGroupBy.Items.Add(lblByDay.Text);// = 0
				cboGroupBy.Items.Add(lblByHour.Text);   // = 1
				cboGroupBy.Items.Add(lblByShift.Text);   // = 1
				cboGroupBy.SelectedIndex = -1;
				//Init Year, Month combo box
				cboMonth.Items.Clear();
				cboMonth.Items.Add(string.Empty);
				for (int i = 1; i < 13; i++)
				{
					cboMonth.Items.Add(i.ToString());
				}
				cboMonth.SelectedIndex = -1;

				cboYear.Items.Clear();
				cboYear.Items.Add(string.Empty);
				for (int i = 2000; i < 2051; i++)
				{
					cboYear.Items.Add(i.ToString());
				}
				cboYear.SelectedIndex = -1;
				if(mintDCOptionMasterID > 0)
				{					
					//Fill data from datarow to controls					
					LoadDCOptionDetail(mintDCOptionMasterID);				
				}
				else
				{
					//Clear controls value and lock for editing				
					ResetControlValue(enuFormAction);					
				}
				
				LockControl(false);
//				btnEdit.Enabled = false;
//				btnDelete.Enabled = false;
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

		private void btnSave_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnSave_Click()";

			try
			{
				// check data, if data is invalid then exit immediately
				blnDataIsValid = ValidateData() && !dgrdData.EditActive;
				if(!blnDataIsValid) return;
				
				//Data is ok, assign value to VO object
				voDCOptionMaster.CCNID = int.Parse(cboCCN.SelectedValue.ToString());
				voDCOptionMaster.Cycle = txtCycle.Text.Trim();				
				voDCOptionMaster.Description = txtDescription.Text.Trim();
				voDCOptionMaster.IgnoreMoveTime = chkIgnoreMoveTime.Checked;						
				voDCOptionMaster.SafetyStock = chkSafetyStock.Checked;
				voDCOptionMaster.OnHand = chkOnHand.Checked;
				//voDCOptionMaster.MPSCycleOptionMasterID = int.Parse(txtMPSCycle.Tag.ToString());
				voDCOptionMaster.ScheduleCode = int.Parse(cboScheduleCode.SelectedValue.ToString());
				voDCOptionMaster.ScheduleType = int.Parse(cboScheduleType.SelectedValue.ToString());				
				voDCOptionMaster.AsOfDate = GetDateOnly(DateTime.Parse(dtmFromDate.Value.ToString()));
				voDCOptionMaster.PlanHorizon = int.Parse(txtPlanHorizon.Text);
				//voDCOptionMaster.Version = int.Parse(txtVersion.Text);
				voDCOptionMaster.Version = (int)txtVersion.Value;
				if ((cboMonth.SelectedIndex != -1)
					&& (cboMonth.SelectedIndex != 0)
					&& (cboYear.SelectedIndex != 0)
					&& (cboYear.SelectedIndex != -1))
				{
					voDCOptionMaster.PlanningPeriod = (new DateTime(int.Parse(cboYear.SelectedItem.ToString()), int.Parse(cboMonth.SelectedItem.ToString()), 1));
				}
				if (cboGroupBy.SelectedIndex == 0)
				{
					voDCOptionMaster.GroupBy = (byte) PlanningGroupBy.ByDate;
				}
				else if (cboGroupBy.SelectedIndex == 1)
				{
					voDCOptionMaster.GroupBy = (byte) PlanningGroupBy.ByHour;
				}
				else
				{
					voDCOptionMaster.GroupBy = (byte) PlanningGroupBy.ByShift;
				}
				//voDCOptionMaster.AsOfDate = new DateTime(voDCOptionMaster.AsOfDate.Year, voDCOptionMaster.AsOfDate.Month, voDCOptionMaster.AsOfDate.Day);		
				
				#region Del by Trada 22-11-2005
				//if(numMaxDays.ValueIsDbNull || numMaxDays.Text.Trim().Length == 0)
				//{	
				//	voDCOptionMaster.MaxDays = Int32.MinValue;
				//}
				//else
				//{
				//	voDCOptionMaster.MaxDays = int.Parse(numMaxDays.Value.ToString());
				//}
				#endregion 
				
				//Hack: Trada 22-11-2005
				//voDCOptionMaster.MaxDays = intMaxDays;
				voDCOptionMaster.IncludeCheckPoint = chkCheckPoint.Checked;
				voDCOptionMaster.UseCacheAsBegin = chkUseCacheAsBeginStock.Checked;
				// END: Trada 22-11-2005				
				// check form action to save data
				DataSet dtsDC = new DataSet();
				switch(enuFormAction)
				{
					case EnumAction.Add:
						//check if version and planning period have existed
						if (boDCOptions.CheckUniqueVersion(voDCOptionMaster.PlanningPeriod, voDCOptionMaster.Version))
						{
							PCSMessageBox.Show(ErrorCode.MESSAGE_DCOPTION_VERSION_HAS_EXIST, MessageBoxIcon.Warning);
							txtVersion.Focus();
							return;
						}

						//Assign last update value	
						voDCOptionMaster.LastUpdate = DateTime.MinValue;

//						if(dtbDCOptionDetail.DataSet != null)
//						{
//							boDCOptions.AddDCOption(voDCOptionMaster, dtbDCOptionDetail.DataSet);							
//						}
//						else
//						{
//							DataSet dtsDC = new DataSet();
//							dtsDC.Tables.Add(dtbDCOptionDetail);
//							boDCOptions.AddDCOption(voDCOptionMaster, dtsDC);							
//						}
						dtsDC = new DataSet();
						boDCOptions.AddDCOption(voDCOptionMaster, dtsDC);							
						break;

					case EnumAction.Edit:
						//Assign last update value	
						//voDCOptionMaster.LastUpdate = DateTime.Now;

//						if(dtbDCOptionDetail.DataSet != null)
//						{
//							boDCOptions.UpdateDCOption(voDCOptionMaster, dtbDCOptionDetail.DataSet);
//						}
//						else
//						{
//							dtsDC = new DataSet();
//							dtsDC.Tables.Add(dtbDCOptionDetail);
//							boDCOptions.UpdateDCOption(voDCOptionMaster, dtsDC);
//						}	
						dtsDC = new DataSet();
						//dtsDC.Tables.Add(dtbDCOptionDetail);
						boDCOptions.UpdateDCOption(voDCOptionMaster, dtsDC);

						break;

				}
				
				LoadDCOptionDetail(voDCOptionMaster.DCOptionMasterID);
				PCSMessageBox.Show(ErrorCode.MESSAGE_AFTER_SAVE_DATA);

				enuFormAction = EnumAction.Default;
				LockControl(false);				
			}
			catch (PCSException ex)
			{
				blnDataIsValid = false;
				if (ex.mCode == ErrorCode.DUPLICATE_KEY)
				{
					PCSMessageBox.Show(ErrorCode.MESSAGE_OUTPROCESSING_DUPLICATE_TRANSNO, MessageBoxIcon.Exclamation);
					txtCycle.Focus();
				}
				else
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
			}
			catch (Exception ex)
			{
				blnDataIsValid = false;
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

		private void btnEdit_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnEdit_Click()";
			try
			{
				//return if user has not select DC Option
				if(voDCOptionMaster.DCOptionMasterID == 0 ) return;

				enuFormAction = EnumAction.Edit;
				LockControl(true);
				txtCycle.Focus();
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
				if(voDCOptionMaster.DCOptionMasterID == 0) return;

				if (PCSMessageBox.Show(ErrorCode.MESSAGE_DELETE_RECORD, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
				{
					boDCOptions.DeleteDCOption(voDCOptionMaster.DCOptionMasterID);
					LoadDCOptionDetail(0);
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

		private void btnAdd_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnAdd_Click()";

			try
			{				
				enuFormAction = EnumAction.Add;				
				ResetControlValue(enuFormAction);
				LockControl(true);
				txtCycle.Focus();
				//Fill Default Master Location 
				//FormControlComponents.SetDefaultMasterLocation(txtMasterLocation);
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

		private void btnHelp_Click(object sender, System.EventArgs e)
		{
		
		}

		private void btnClose_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void btnSearchCycle_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnCycleSearch_Click()";

			try
			{
				SelectCycle(METHOD_NAME, true);
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

		private void btnMRPCycle_Click(object sender, System.EventArgs e)
		{
		
		}

		private void btnMPSCycle_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnCycleSearch_Click()";

			try
			{
				SelectMPSCycle(METHOD_NAME, true);
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

		private void btnMRPScheduleCode_Click(object sender, System.EventArgs e)
		{
		
		}

		private void btnMPSScheduleCode_Click(object sender, System.EventArgs e)
		{
		
		}
		
		private void dgrdData_ButtonClick(object sender, ColEventArgs e)
		{
			const string METHOD_NAME = THIS + ".dgrdData_ButtonClick()";

			try
			{	
				if(e.Column.DataColumn == null)
				{
					return;
				}
				
				//Set edit active
				dgrdData.EditActive = true;

				ProcessInputDataInGrid(e.Column.DataColumn.DataField, e.Column.DataColumn.Value.ToString().Trim(), true);
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
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
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
		
		private void dgrdData_BeforeColUpdate(object sender, BeforeColUpdateEventArgs e)
		{
			const string METHOD_NAME = THIS + ".dgrdData_BeforeColUpdate()";

			try
			{
				//if column's value then exit immediately
				if(e.Column.DataColumn.Text.Trim().Length == 0)
				{
					if(e.Column.DataColumn.DataField == MST_MasterLocationTable.TABLE_NAME + MST_MasterLocationTable.CODE_FLD)
					{
						dgrdData.Columns[MST_MasterLocationTable.TABLE_NAME + MST_MasterLocationTable.CODE_FLD].Value = DBNull.Value;
						dgrdData.Columns[PRO_DCOptionDetailTable.MASTERLOCATIONID_FLD].Value = DBNull.Value;
						dgrdData.Columns[PRO_DCOptionDetailTable.WORKORDER_FLD].Value = false;
					}
					return;
				}

				e.Cancel = !ProcessInputDataInGrid(e.Column.DataColumn.DataField, e.Column.DataColumn.Text.Trim(), false);				
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
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
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

		
		private void dgrdData_KeyDown(object sender, KeyEventArgs e)
		{			
			const string METHOD_NAME = THIS + ".dgrdData_KeyDown()";

			try
			{
				//if column's value then exit immediately
				if(enuFormAction == EnumAction.Default) return;
				
				switch (e.KeyCode)
				{
					case Keys.F4:
						//Turn on EditActive status of grid
						dgrdData.EditActive = true;
						ProcessInputDataInGrid(dgrdData.Columns[dgrdData.Col].DataField, dgrdData.Columns[dgrdData.Col].Value.ToString(), true);
						break;
				}				
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
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
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

		private void txtCycle_KeyDown(object sender, KeyEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtCycle_KeyDown()";

			try
			{
				if ((e.KeyCode == Keys.F4) && (btnSearchCycle.Enabled))
				{
					SelectCycle(METHOD_NAME, true);
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

		
		private void txtMPSCycle_KeyDown(object sender, KeyEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtMPSCycle_KeyDown()";

			try
			{
				if ((e.KeyCode == Keys.F4) && (btnMPSCycle.Enabled))
				{
					SelectMPSCycle(METHOD_NAME, true);
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
		
		private void DCOptions_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".DCOptions_Closing()";
			try
			{
				// if the form has been changed then ask to store database
				if(enuFormAction != EnumAction.Default)
				{
					DialogResult enumDialog = PCSMessageBox.Show(ErrorCode.MESSAGE_QUESTION_STORE_INTO_DATABASE, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
					if( enumDialog == DialogResult.Yes)
					{
						btnSave_Click(sender, e);
						e.Cancel = !blnDataIsValid;
					}
					else if( enumDialog == DialogResult.Cancel)//click Cancel button
					{
						e.Cancel = true;
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

		private void DCOptions_KeyDown(object sender, KeyEventArgs e)
		{
			const string METHOD_NAME = THIS + ".DCOptions_KeyDown()";

			try
			{			
				switch (e.KeyCode)
				{
					case Keys.F12:
						//Goto bottommost row while update mode
						if(enuFormAction == EnumAction.Default) return;
						
						dgrdData.Col = dgrdData.Columns.IndexOf(dgrdData.Columns[MST_MasterLocationTable.TABLE_NAME + MST_MasterLocationTable.CODE_FLD]);
						dgrdData.Row = dgrdData.RowCount;
						dgrdData.Focus();
						break;					
				}
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
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
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
		
		private void txtMPSCycle_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtMPSCycle_Validating()";

			try
			{				
				//exit if not in add action or empty
				if(enuFormAction == EnumAction.Default) return;

				if(txtMPSCycle.Text.Trim().Length == 0)
				{
					txtMPSCycle.Tag = ZERO_STRING;
					return;
				}
				else if(!txtMPSCycle.Modified)
				{
					return;
				}

				e.Cancel = !SelectMPSCycle(METHOD_NAME, false);				
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
		/// Handle btnRemoveDCPResults event
		/// Author : DuongNA
		/// Date : 28-11-2005
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnRemoveDCPResults_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnRemoveDCPResults_Click()";

			try
			{				
				if (voDCOptionMaster.DCOptionMasterID <= 0)
				{
					return;
				}
				if (PCSMessageBox.Show(ErrorCode.MESSAGE_REMOVE_DCPRESULTS,MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes)
				{
					DCOptionsBO boDCOption = new DCOptionsBO();
					boDCOption.DeleteDCPResults(voDCOptionMaster.DCOptionMasterID);
					PCSMessageBox.Show(ErrorCode.MESSAGE_AFTER_SAVE_DATA, MessageBoxIcon.Information);
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
		

		private void btnProcess_Click(object sender, System.EventArgs e)
		{
			if (voDCOptionMaster.DCOptionMasterID != 0)
			{
				DCRegenerate frmDCRegenerate = new DCRegenerate(voDCOptionMaster.Cycle,voDCOptionMaster.DCOptionMasterID);
				frmDCRegenerate.ShowDialog(this);
			}
		}		

		#endregion Event Prcessing

		/// <summary>
		/// GetDateOnly
		/// </summary>
		/// <param name="pdtmInputDate"></param>
		/// <returns></returns>
		/// <author>Trada</author>
		/// <date>Thursday, January 12 2006</date>
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
		/// dtmFromDate_Validating
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Wednesday, Feb 15 2006</date>
		private void dtmFromDate_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".dtmFromDate_Validating()";
			try
			{	
				if ((dtmFromDate.Value != null)&&(dtmToDate.Value != null)
					&&(dtmFromDate.Value != DBNull.Value)&&(dtmToDate.Value != DBNull.Value))
				{
					if ((GetDateOnly((DateTime)dtmToDate.Value) - GetDateOnly((DateTime)dtmFromDate.Value)).Days <= 0)
					{
						PCSMessageBox.Show(ErrorCode.MESSAGE_MP_PERIODDATE, MessageBoxIcon.Warning);
						e.Cancel = true;
						return;
					}
					else
						txtPlanHorizon.Text = (GetDateOnly((DateTime)dtmToDate.Value) - GetDateOnly((DateTime)dtmFromDate.Value)).Days.ToString();
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

		private void txtCycle_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtMPSCycle_Validating()";
			try
			{				
				//exit if not in add action or empty
				if ((enuFormAction == EnumAction.Default)&& (txtCycle.Text.Trim() == string.Empty))
				{
					ResetControlValue(enuFormAction);
					return;
				}
				if(enuFormAction == EnumAction.Add) return;
				if(txtCycle.Text.Trim().Length == 0)
				{
					txtCycle.Tag = ZERO_STRING;
					return;
				}
				else if(!txtCycle.Modified)
				{
					return;
				}

				e.Cancel = !SelectCycle(METHOD_NAME, false);				
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