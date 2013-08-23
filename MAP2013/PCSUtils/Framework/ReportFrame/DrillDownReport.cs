using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using C1.Win.C1TrueDBGrid;
using PCSComUtils.Common;
using PCSComUtils.Framework.ReportFrame.BO;
using PCSComUtils.Framework.ReportFrame.DS;
using PCSComUtils.PCSExc;
using PCSUtils.Log;
using PCSUtils.Utils;
using CancelEventArgs = System.ComponentModel.CancelEventArgs;

namespace PCSUtils.Framework.ReportFrame
{
	/// <summary>
	/// Summary description for DrillDownReport.
	/// </summary>
	public class DrillDownReport : Form
	{
		private TextBox txtReportName;
		private TextBox txtReportCode;
		private Label lblReportName;
		private Label lblReportCode;
		private Button btnClose;
		private Button btnHelp;
		private Button btnEdit;
		private Button btnSave;
		private Button btnDelete;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private Container components = null;

		private const string THIS = "PCSUtils.Framework.ReportFrame.DrillDownReport";
		private const int ORDER_COLUMN_INDEX = 4;

		private string mMasterReportID;
		private EnumAction mEnumType;
		private DataTable tblDataSource = new DataTable(sys_ReportDrillDownTable.TABLE_NAME);
		private DataTable tblSourceForGrid = new DataTable(sys_ReportDrillDownTable.TABLE_NAME);
		private ArrayList arrMasterFields = new ArrayList();
		private ArrayList arrMasterParameters = new ArrayList();

		private DrillDownReportBO boDrillDownReport = new DrillDownReportBO();
		private C1TrueDBGrid gridDrillDownReport;

		private Hashtable htDataType = new Hashtable();
		private DataRow drowOldValue;

		public string MasterReportID
		{
			get { return this.mMasterReportID; }
			set { this.mMasterReportID = value; }
		}

		public EnumAction EnumType
		{
			get { return this.mEnumType; }
			set { this.mEnumType = value; }
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
		///    06-Jan-2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public DrillDownReport()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			#region init hashtable data type

			htDataType.Clear();

			htDataType.Add((int)TypeCode.Boolean, TypeCode.Boolean.ToString());
			htDataType.Add((int)TypeCode.Char, TypeCode.Char.ToString());
			htDataType.Add((int)TypeCode.DateTime, TypeCode.DateTime.ToString());
			htDataType.Add((int)TypeCode.Decimal, TypeCode.Decimal.ToString());
			htDataType.Add((int)TypeCode.Double, TypeCode.Double.ToString());
			htDataType.Add((int)TypeCode.Int16, TypeCode.Int16.ToString());
			htDataType.Add((int)TypeCode.Int32, TypeCode.Int32.ToString());
			htDataType.Add((int)TypeCode.Int64, TypeCode.Int64.ToString());
			htDataType.Add((int)TypeCode.Single, TypeCode.Single.ToString());
			htDataType.Add((int)TypeCode.String, TypeCode.String.ToString());

			#endregion
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(DrillDownReport));
			this.txtReportName = new System.Windows.Forms.TextBox();
			this.txtReportCode = new System.Windows.Forms.TextBox();
			this.lblReportName = new System.Windows.Forms.Label();
			this.lblReportCode = new System.Windows.Forms.Label();
			this.btnClose = new System.Windows.Forms.Button();
			this.btnHelp = new System.Windows.Forms.Button();
			this.btnEdit = new System.Windows.Forms.Button();
			this.btnSave = new System.Windows.Forms.Button();
			this.btnDelete = new System.Windows.Forms.Button();
			this.gridDrillDownReport = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
			((System.ComponentModel.ISupportInitialize)(this.gridDrillDownReport)).BeginInit();
			this.SuspendLayout();
			// 
			// txtReportName
			// 
			this.txtReportName.AccessibleDescription = resources.GetString("txtReportName.AccessibleDescription");
			this.txtReportName.AccessibleName = resources.GetString("txtReportName.AccessibleName");
			this.txtReportName.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("txtReportName.Anchor")));
			this.txtReportName.AutoSize = ((bool)(resources.GetObject("txtReportName.AutoSize")));
			this.txtReportName.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("txtReportName.BackgroundImage")));
			this.txtReportName.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("txtReportName.Dock")));
			this.txtReportName.Enabled = ((bool)(resources.GetObject("txtReportName.Enabled")));
			this.txtReportName.Font = ((System.Drawing.Font)(resources.GetObject("txtReportName.Font")));
			this.txtReportName.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("txtReportName.ImeMode")));
			this.txtReportName.Location = ((System.Drawing.Point)(resources.GetObject("txtReportName.Location")));
			this.txtReportName.MaxLength = ((int)(resources.GetObject("txtReportName.MaxLength")));
			this.txtReportName.Multiline = ((bool)(resources.GetObject("txtReportName.Multiline")));
			this.txtReportName.Name = "txtReportName";
			this.txtReportName.PasswordChar = ((char)(resources.GetObject("txtReportName.PasswordChar")));
			this.txtReportName.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("txtReportName.RightToLeft")));
			this.txtReportName.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("txtReportName.ScrollBars")));
			this.txtReportName.Size = ((System.Drawing.Size)(resources.GetObject("txtReportName.Size")));
			this.txtReportName.TabIndex = ((int)(resources.GetObject("txtReportName.TabIndex")));
			this.txtReportName.Text = resources.GetString("txtReportName.Text");
			this.txtReportName.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("txtReportName.TextAlign")));
			this.txtReportName.Visible = ((bool)(resources.GetObject("txtReportName.Visible")));
			this.txtReportName.WordWrap = ((bool)(resources.GetObject("txtReportName.WordWrap")));
			this.txtReportName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtReportName_KeyPress);
			// 
			// txtReportCode
			// 
			this.txtReportCode.AccessibleDescription = resources.GetString("txtReportCode.AccessibleDescription");
			this.txtReportCode.AccessibleName = resources.GetString("txtReportCode.AccessibleName");
			this.txtReportCode.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("txtReportCode.Anchor")));
			this.txtReportCode.AutoSize = ((bool)(resources.GetObject("txtReportCode.AutoSize")));
			this.txtReportCode.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("txtReportCode.BackgroundImage")));
			this.txtReportCode.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("txtReportCode.Dock")));
			this.txtReportCode.Enabled = ((bool)(resources.GetObject("txtReportCode.Enabled")));
			this.txtReportCode.Font = ((System.Drawing.Font)(resources.GetObject("txtReportCode.Font")));
			this.txtReportCode.ForeColor = System.Drawing.SystemColors.WindowText;
			this.txtReportCode.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("txtReportCode.ImeMode")));
			this.txtReportCode.Location = ((System.Drawing.Point)(resources.GetObject("txtReportCode.Location")));
			this.txtReportCode.MaxLength = ((int)(resources.GetObject("txtReportCode.MaxLength")));
			this.txtReportCode.Multiline = ((bool)(resources.GetObject("txtReportCode.Multiline")));
			this.txtReportCode.Name = "txtReportCode";
			this.txtReportCode.PasswordChar = ((char)(resources.GetObject("txtReportCode.PasswordChar")));
			this.txtReportCode.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("txtReportCode.RightToLeft")));
			this.txtReportCode.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("txtReportCode.ScrollBars")));
			this.txtReportCode.Size = ((System.Drawing.Size)(resources.GetObject("txtReportCode.Size")));
			this.txtReportCode.TabIndex = ((int)(resources.GetObject("txtReportCode.TabIndex")));
			this.txtReportCode.Text = resources.GetString("txtReportCode.Text");
			this.txtReportCode.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("txtReportCode.TextAlign")));
			this.txtReportCode.Visible = ((bool)(resources.GetObject("txtReportCode.Visible")));
			this.txtReportCode.WordWrap = ((bool)(resources.GetObject("txtReportCode.WordWrap")));
			this.txtReportCode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtReportCode_KeyPress);
			// 
			// lblReportName
			// 
			this.lblReportName.AccessibleDescription = resources.GetString("lblReportName.AccessibleDescription");
			this.lblReportName.AccessibleName = resources.GetString("lblReportName.AccessibleName");
			this.lblReportName.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblReportName.Anchor")));
			this.lblReportName.AutoSize = ((bool)(resources.GetObject("lblReportName.AutoSize")));
			this.lblReportName.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblReportName.Dock")));
			this.lblReportName.Enabled = ((bool)(resources.GetObject("lblReportName.Enabled")));
			this.lblReportName.Font = ((System.Drawing.Font)(resources.GetObject("lblReportName.Font")));
			this.lblReportName.Image = ((System.Drawing.Image)(resources.GetObject("lblReportName.Image")));
			this.lblReportName.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblReportName.ImageAlign")));
			this.lblReportName.ImageIndex = ((int)(resources.GetObject("lblReportName.ImageIndex")));
			this.lblReportName.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblReportName.ImeMode")));
			this.lblReportName.Location = ((System.Drawing.Point)(resources.GetObject("lblReportName.Location")));
			this.lblReportName.Name = "lblReportName";
			this.lblReportName.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblReportName.RightToLeft")));
			this.lblReportName.Size = ((System.Drawing.Size)(resources.GetObject("lblReportName.Size")));
			this.lblReportName.TabIndex = ((int)(resources.GetObject("lblReportName.TabIndex")));
			this.lblReportName.Text = resources.GetString("lblReportName.Text");
			this.lblReportName.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblReportName.TextAlign")));
			this.lblReportName.Visible = ((bool)(resources.GetObject("lblReportName.Visible")));
			// 
			// lblReportCode
			// 
			this.lblReportCode.AccessibleDescription = resources.GetString("lblReportCode.AccessibleDescription");
			this.lblReportCode.AccessibleName = resources.GetString("lblReportCode.AccessibleName");
			this.lblReportCode.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblReportCode.Anchor")));
			this.lblReportCode.AutoSize = ((bool)(resources.GetObject("lblReportCode.AutoSize")));
			this.lblReportCode.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblReportCode.Dock")));
			this.lblReportCode.Enabled = ((bool)(resources.GetObject("lblReportCode.Enabled")));
			this.lblReportCode.Font = ((System.Drawing.Font)(resources.GetObject("lblReportCode.Font")));
			this.lblReportCode.ForeColor = System.Drawing.Color.Maroon;
			this.lblReportCode.Image = ((System.Drawing.Image)(resources.GetObject("lblReportCode.Image")));
			this.lblReportCode.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblReportCode.ImageAlign")));
			this.lblReportCode.ImageIndex = ((int)(resources.GetObject("lblReportCode.ImageIndex")));
			this.lblReportCode.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblReportCode.ImeMode")));
			this.lblReportCode.Location = ((System.Drawing.Point)(resources.GetObject("lblReportCode.Location")));
			this.lblReportCode.Name = "lblReportCode";
			this.lblReportCode.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblReportCode.RightToLeft")));
			this.lblReportCode.Size = ((System.Drawing.Size)(resources.GetObject("lblReportCode.Size")));
			this.lblReportCode.TabIndex = ((int)(resources.GetObject("lblReportCode.TabIndex")));
			this.lblReportCode.Text = resources.GetString("lblReportCode.Text");
			this.lblReportCode.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblReportCode.TextAlign")));
			this.lblReportCode.Visible = ((bool)(resources.GetObject("lblReportCode.Visible")));
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
			// gridDrillDownReport
			// 
			this.gridDrillDownReport.AccessibleDescription = resources.GetString("gridDrillDownReport.AccessibleDescription");
			this.gridDrillDownReport.AccessibleName = resources.GetString("gridDrillDownReport.AccessibleName");
			this.gridDrillDownReport.AllowAddNew = ((bool)(resources.GetObject("gridDrillDownReport.AllowAddNew")));
			this.gridDrillDownReport.AllowArrows = ((bool)(resources.GetObject("gridDrillDownReport.AllowArrows")));
			this.gridDrillDownReport.AllowColMove = ((bool)(resources.GetObject("gridDrillDownReport.AllowColMove")));
			this.gridDrillDownReport.AllowColSelect = ((bool)(resources.GetObject("gridDrillDownReport.AllowColSelect")));
			this.gridDrillDownReport.AllowDelete = ((bool)(resources.GetObject("gridDrillDownReport.AllowDelete")));
			this.gridDrillDownReport.AllowDrag = ((bool)(resources.GetObject("gridDrillDownReport.AllowDrag")));
			this.gridDrillDownReport.AllowFilter = ((bool)(resources.GetObject("gridDrillDownReport.AllowFilter")));
			this.gridDrillDownReport.AllowHorizontalSplit = ((bool)(resources.GetObject("gridDrillDownReport.AllowHorizontalSplit")));
			this.gridDrillDownReport.AllowRowSelect = ((bool)(resources.GetObject("gridDrillDownReport.AllowRowSelect")));
			this.gridDrillDownReport.AllowSort = ((bool)(resources.GetObject("gridDrillDownReport.AllowSort")));
			this.gridDrillDownReport.AllowUpdate = ((bool)(resources.GetObject("gridDrillDownReport.AllowUpdate")));
			this.gridDrillDownReport.AllowUpdateOnBlur = ((bool)(resources.GetObject("gridDrillDownReport.AllowUpdateOnBlur")));
			this.gridDrillDownReport.AllowVerticalSplit = ((bool)(resources.GetObject("gridDrillDownReport.AllowVerticalSplit")));
			this.gridDrillDownReport.AlternatingRows = ((bool)(resources.GetObject("gridDrillDownReport.AlternatingRows")));
			this.gridDrillDownReport.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("gridDrillDownReport.Anchor")));
			this.gridDrillDownReport.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("gridDrillDownReport.BackgroundImage")));
			this.gridDrillDownReport.BorderStyle = ((System.Windows.Forms.BorderStyle)(resources.GetObject("gridDrillDownReport.BorderStyle")));
			this.gridDrillDownReport.Caption = resources.GetString("gridDrillDownReport.Caption");
			this.gridDrillDownReport.CaptionHeight = ((int)(resources.GetObject("gridDrillDownReport.CaptionHeight")));
			this.gridDrillDownReport.CellTipsDelay = ((int)(resources.GetObject("gridDrillDownReport.CellTipsDelay")));
			this.gridDrillDownReport.CellTipsWidth = ((int)(resources.GetObject("gridDrillDownReport.CellTipsWidth")));
			this.gridDrillDownReport.ChildGrid = ((C1.Win.C1TrueDBGrid.C1TrueDBGrid)(resources.GetObject("gridDrillDownReport.ChildGrid")));
			this.gridDrillDownReport.CollapseColor = ((System.Drawing.Color)(resources.GetObject("gridDrillDownReport.CollapseColor")));
			this.gridDrillDownReport.ColumnFooters = ((bool)(resources.GetObject("gridDrillDownReport.ColumnFooters")));
			this.gridDrillDownReport.ColumnHeaders = ((bool)(resources.GetObject("gridDrillDownReport.ColumnHeaders")));
			this.gridDrillDownReport.DefColWidth = ((int)(resources.GetObject("gridDrillDownReport.DefColWidth")));
			this.gridDrillDownReport.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("gridDrillDownReport.Dock")));
			this.gridDrillDownReport.EditDropDown = ((bool)(resources.GetObject("gridDrillDownReport.EditDropDown")));
			this.gridDrillDownReport.EmptyRows = ((bool)(resources.GetObject("gridDrillDownReport.EmptyRows")));
			this.gridDrillDownReport.Enabled = ((bool)(resources.GetObject("gridDrillDownReport.Enabled")));
			this.gridDrillDownReport.ExpandColor = ((System.Drawing.Color)(resources.GetObject("gridDrillDownReport.ExpandColor")));
			this.gridDrillDownReport.ExposeCellMode = ((C1.Win.C1TrueDBGrid.ExposeCellModeEnum)(resources.GetObject("gridDrillDownReport.ExposeCellMode")));
			this.gridDrillDownReport.ExtendRightColumn = ((bool)(resources.GetObject("gridDrillDownReport.ExtendRightColumn")));
			this.gridDrillDownReport.FetchRowStyles = ((bool)(resources.GetObject("gridDrillDownReport.FetchRowStyles")));
			this.gridDrillDownReport.FilterBar = ((bool)(resources.GetObject("gridDrillDownReport.FilterBar")));
			this.gridDrillDownReport.FlatStyle = ((C1.Win.C1TrueDBGrid.FlatModeEnum)(resources.GetObject("gridDrillDownReport.FlatStyle")));
			this.gridDrillDownReport.Font = ((System.Drawing.Font)(resources.GetObject("gridDrillDownReport.Font")));
			this.gridDrillDownReport.GroupByAreaVisible = ((bool)(resources.GetObject("gridDrillDownReport.GroupByAreaVisible")));
			this.gridDrillDownReport.GroupByCaption = resources.GetString("gridDrillDownReport.GroupByCaption");
			this.gridDrillDownReport.Images.Add(((System.Drawing.Image)(resources.GetObject("resource"))));
			this.gridDrillDownReport.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("gridDrillDownReport.ImeMode")));
			this.gridDrillDownReport.LinesPerRow = ((int)(resources.GetObject("gridDrillDownReport.LinesPerRow")));
			this.gridDrillDownReport.Location = ((System.Drawing.Point)(resources.GetObject("gridDrillDownReport.Location")));
			this.gridDrillDownReport.MarqueeStyle = C1.Win.C1TrueDBGrid.MarqueeEnum.DottedCellBorder;
			this.gridDrillDownReport.Name = "gridDrillDownReport";
			this.gridDrillDownReport.PictureAddnewRow = ((System.Drawing.Image)(resources.GetObject("gridDrillDownReport.PictureAddnewRow")));
			this.gridDrillDownReport.PictureCurrentRow = ((System.Drawing.Image)(resources.GetObject("gridDrillDownReport.PictureCurrentRow")));
			this.gridDrillDownReport.PictureFilterBar = ((System.Drawing.Image)(resources.GetObject("gridDrillDownReport.PictureFilterBar")));
			this.gridDrillDownReport.PictureFooterRow = ((System.Drawing.Image)(resources.GetObject("gridDrillDownReport.PictureFooterRow")));
			this.gridDrillDownReport.PictureHeaderRow = ((System.Drawing.Image)(resources.GetObject("gridDrillDownReport.PictureHeaderRow")));
			this.gridDrillDownReport.PictureModifiedRow = ((System.Drawing.Image)(resources.GetObject("gridDrillDownReport.PictureModifiedRow")));
			this.gridDrillDownReport.PictureStandardRow = ((System.Drawing.Image)(resources.GetObject("gridDrillDownReport.PictureStandardRow")));
			this.gridDrillDownReport.PreviewInfo.AllowSizing = ((bool)(resources.GetObject("gridDrillDownReport.PreviewInfo.AllowSizing")));
			this.gridDrillDownReport.PreviewInfo.Caption = resources.GetString("gridDrillDownReport.PreviewInfo.Caption");
			this.gridDrillDownReport.PreviewInfo.Location = ((System.Drawing.Point)(resources.GetObject("gridDrillDownReport.PreviewInfo.Location")));
			this.gridDrillDownReport.PreviewInfo.Size = ((System.Drawing.Size)(resources.GetObject("gridDrillDownReport.PreviewInfo.Size")));
			this.gridDrillDownReport.PreviewInfo.ToolBars = ((bool)(resources.GetObject("gridDrillDownReport.PreviewInfo.ToolBars")));
			this.gridDrillDownReport.PreviewInfo.UIStrings.Content = ((string[])(resources.GetObject("gridDrillDownReport.PreviewInfo.UIStrings.Content")));
			this.gridDrillDownReport.PreviewInfo.ZoomFactor = ((System.Double)(resources.GetObject("gridDrillDownReport.PreviewInfo.ZoomFactor")));
			this.gridDrillDownReport.PrintInfo.MaxRowHeight = ((int)(resources.GetObject("gridDrillDownReport.PrintInfo.MaxRowHeight")));
			this.gridDrillDownReport.PrintInfo.OwnerDrawPageFooter = ((bool)(resources.GetObject("gridDrillDownReport.PrintInfo.OwnerDrawPageFooter")));
			this.gridDrillDownReport.PrintInfo.OwnerDrawPageHeader = ((bool)(resources.GetObject("gridDrillDownReport.PrintInfo.OwnerDrawPageHeader")));
			this.gridDrillDownReport.PrintInfo.PageFooter = resources.GetString("gridDrillDownReport.PrintInfo.PageFooter");
			this.gridDrillDownReport.PrintInfo.PageFooterHeight = ((int)(resources.GetObject("gridDrillDownReport.PrintInfo.PageFooterHeight")));
			this.gridDrillDownReport.PrintInfo.PageHeader = resources.GetString("gridDrillDownReport.PrintInfo.PageHeader");
			this.gridDrillDownReport.PrintInfo.PageHeaderHeight = ((int)(resources.GetObject("gridDrillDownReport.PrintInfo.PageHeaderHeight")));
			this.gridDrillDownReport.PrintInfo.PrintHorizontalSplits = ((bool)(resources.GetObject("gridDrillDownReport.PrintInfo.PrintHorizontalSplits")));
			this.gridDrillDownReport.PrintInfo.ProgressCaption = resources.GetString("gridDrillDownReport.PrintInfo.ProgressCaption");
			this.gridDrillDownReport.PrintInfo.RepeatColumnFooters = ((bool)(resources.GetObject("gridDrillDownReport.PrintInfo.RepeatColumnFooters")));
			this.gridDrillDownReport.PrintInfo.RepeatColumnHeaders = ((bool)(resources.GetObject("gridDrillDownReport.PrintInfo.RepeatColumnHeaders")));
			this.gridDrillDownReport.PrintInfo.RepeatGridHeader = ((bool)(resources.GetObject("gridDrillDownReport.PrintInfo.RepeatGridHeader")));
			this.gridDrillDownReport.PrintInfo.RepeatSplitHeaders = ((bool)(resources.GetObject("gridDrillDownReport.PrintInfo.RepeatSplitHeaders")));
			this.gridDrillDownReport.PrintInfo.ShowOptionsDialog = ((bool)(resources.GetObject("gridDrillDownReport.PrintInfo.ShowOptionsDialog")));
			this.gridDrillDownReport.PrintInfo.ShowProgressForm = ((bool)(resources.GetObject("gridDrillDownReport.PrintInfo.ShowProgressForm")));
			this.gridDrillDownReport.PrintInfo.ShowSelection = ((bool)(resources.GetObject("gridDrillDownReport.PrintInfo.ShowSelection")));
			this.gridDrillDownReport.PrintInfo.UseGridColors = ((bool)(resources.GetObject("gridDrillDownReport.PrintInfo.UseGridColors")));
			this.gridDrillDownReport.RecordSelectors = ((bool)(resources.GetObject("gridDrillDownReport.RecordSelectors")));
			this.gridDrillDownReport.RecordSelectorWidth = ((int)(resources.GetObject("gridDrillDownReport.RecordSelectorWidth")));
			this.gridDrillDownReport.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("gridDrillDownReport.RightToLeft")));
			this.gridDrillDownReport.RowDivider.Color = ((System.Drawing.Color)(resources.GetObject("resource.Color")));
			this.gridDrillDownReport.RowDivider.Style = ((C1.Win.C1TrueDBGrid.LineStyleEnum)(resources.GetObject("resource.Style")));
			this.gridDrillDownReport.RowHeight = ((int)(resources.GetObject("gridDrillDownReport.RowHeight")));
			this.gridDrillDownReport.RowSubDividerColor = ((System.Drawing.Color)(resources.GetObject("gridDrillDownReport.RowSubDividerColor")));
			this.gridDrillDownReport.ScrollTips = ((bool)(resources.GetObject("gridDrillDownReport.ScrollTips")));
			this.gridDrillDownReport.ScrollTrack = ((bool)(resources.GetObject("gridDrillDownReport.ScrollTrack")));
			this.gridDrillDownReport.Size = ((System.Drawing.Size)(resources.GetObject("gridDrillDownReport.Size")));
			this.gridDrillDownReport.SpringMode = ((bool)(resources.GetObject("gridDrillDownReport.SpringMode")));
			this.gridDrillDownReport.TabAcrossSplits = ((bool)(resources.GetObject("gridDrillDownReport.TabAcrossSplits")));
			this.gridDrillDownReport.TabIndex = ((int)(resources.GetObject("gridDrillDownReport.TabIndex")));
			this.gridDrillDownReport.Text = resources.GetString("gridDrillDownReport.Text");
			this.gridDrillDownReport.ViewCaptionWidth = ((int)(resources.GetObject("gridDrillDownReport.ViewCaptionWidth")));
			this.gridDrillDownReport.ViewColumnWidth = ((int)(resources.GetObject("gridDrillDownReport.ViewColumnWidth")));
			this.gridDrillDownReport.Visible = ((bool)(resources.GetObject("gridDrillDownReport.Visible")));
			this.gridDrillDownReport.WrapCellPointer = ((bool)(resources.GetObject("gridDrillDownReport.WrapCellPointer")));
			this.gridDrillDownReport.AfterColUpdate += new C1.Win.C1TrueDBGrid.ColEventHandler(this.gridDrillDownReport_AfterColUpdate);
			this.gridDrillDownReport.PropBag = resources.GetString("gridDrillDownReport.PropBag");
			// 
			// DrillDownReport
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
			this.Controls.Add(this.gridDrillDownReport);
			this.Controls.Add(this.txtReportName);
			this.Controls.Add(this.txtReportCode);
			this.Controls.Add(this.btnClose);
			this.Controls.Add(this.btnHelp);
			this.Controls.Add(this.btnEdit);
			this.Controls.Add(this.btnSave);
			this.Controls.Add(this.btnDelete);
			this.Controls.Add(this.lblReportName);
			this.Controls.Add(this.lblReportCode);
			this.Enabled = ((bool)(resources.GetObject("$this.Enabled")));
			this.Font = ((System.Drawing.Font)(resources.GetObject("$this.Font")));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("$this.ImeMode")));
			this.Location = ((System.Drawing.Point)(resources.GetObject("$this.Location")));
			this.MaximumSize = ((System.Drawing.Size)(resources.GetObject("$this.MaximumSize")));
			this.MinimumSize = ((System.Drawing.Size)(resources.GetObject("$this.MinimumSize")));
			this.Name = "DrillDownReport";
			this.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("$this.RightToLeft")));
			this.StartPosition = ((System.Windows.Forms.FormStartPosition)(resources.GetObject("$this.StartPosition")));
			this.Text = resources.GetString("$this.Text");
			this.Closing += new System.ComponentModel.CancelEventHandler(this.DrillDownReport_Closing);
			this.Load += new System.EventHandler(this.DrillDownReport_Load);
			((System.ComponentModel.ISupportInitialize)(this.gridDrillDownReport)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		#region Form Events

		//**************************************************************************              
		///    <Description>
		///       Load event of form
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
		///    06-Jan-2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private void DrillDownReport_Load(object sender, EventArgs e)
		{
			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.WaitCursor;

			#endregion Code Inserted Automatically

			
			const string METHOD_NAME = THIS + ".DrillDownReport_Load()";
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
					PCSMessageBox.Show(ErrorCode.MESSAGE_YOU_HAVE_NO_RIGHT_TO_VIEW ,MessageBoxIcon.Warning);
					// Code Inserted Automatically
#region Code Inserted Automatically
this.Cursor = Cursors.Default;
#endregion Code Inserted Automatically

					return;
				}
				#endregion

				txtReportCode.Focus();

				// try to get existing drill down of selected report
				ReportManagementBO boReportManagement = new ReportManagementBO();
				DrillDownReportBO boDrillDownReport = new DrillDownReportBO();
				ArrayList arrDrillDown = boReportManagement.GetDrillDownReports(MasterReportID);
				if (arrDrillDown.Count > 0) // already has drill down
				{
					string strDetailReportID = ((sys_ReportDrillDownVO)arrDrillDown[0]).DetailReportID;
					bool blnIsEdit;
					// get data for C1TrueDBGrid
					tblDataSource = boDrillDownReport.GetDataForTrueDBGrid(MasterReportID, strDetailReportID, out blnIsEdit);
					tblSourceForGrid = tblDataSource.Copy();
					BindTrueDBGrid(tblSourceForGrid);
					EnableButtons(blnIsEdit);

					// set text for report code and report name
					txtReportCode.Text = strDetailReportID;
					txtReportName.Text = boReportManagement.GetReportName(strDetailReportID);
					// unable to edit report code and report name
					txtReportCode.Enabled = false;
					txtReportName.Enabled = false;
				}
				else
				{
					txtReportCode.Enabled = true;
					txtReportName.Enabled = true;
				}

				arrMasterFields = this.GetAllFieldsFromMaster(this.MasterReportID);
				arrMasterParameters = this.GetAllParametersFromMaster(this.MasterReportID);
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
		///       KeyPress Event, display the Report List form allows user select report
		///       then fill to report code and report name fields
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
		///    06-Jan-2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private void txtReportCode_KeyPress(object sender, KeyPressEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtReportCode_KeyPress()";
			if (e.KeyChar == (char)Constants.ENTER_KEY_CHAR)
			{
				try
				{
					ReportList frmReportList = new ReportList(mMasterReportID, sys_ReportTable.REPORTID_FLD);
					frmReportList.ShowDialog();
					if (frmReportList.SelectedReport == null)
					{
						return;
					}
					txtReportCode.Text = frmReportList.SelectedReport.ReportID;
					// fill the coresponding report name
					txtReportName.Text = frmReportList.SelectedReport.ReportName;

					// bind data to grid
					if (frmReportList.SelectedReport != null)
					{
						bool blnIsEdit;
						tblDataSource = boDrillDownReport.GetDataForTrueDBGrid(mMasterReportID, frmReportList.SelectedReport.ReportID, out blnIsEdit);
						BindTrueDBGrid(tblDataSource);
						EnableButtons(blnIsEdit);
					}
					// move to next field
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
		}

		//**************************************************************************              
		///    <Description>
		///       Keypress event, displays report list form allows user to select report
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
		///    06-Jan-2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private void txtReportName_KeyPress(object sender, KeyPressEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtReportName_KeyPress()";
			if (e.KeyChar == (char)Constants.ENTER_KEY_CHAR)
			{
				try
				{
					ReportList frmReportList = new ReportList(this.mMasterReportID, sys_ReportTable.REPORTNAME_FLD);
					frmReportList.ShowDialog();
					if (frmReportList.SelectedReport == null)
					{
						return;
					}
					txtReportCode.Text = frmReportList.SelectedReport.ReportID;
					// fill the coresponding report name
					txtReportName.Text = frmReportList.SelectedReport.ReportName;

					// bind data to grid
					if (frmReportList.SelectedReport != null)
					{
						bool blnIsEdit;
						tblDataSource = boDrillDownReport.GetDataForTrueDBGrid(this.mMasterReportID, frmReportList.SelectedReport.ReportID, out blnIsEdit);
						this.BindTrueDBGrid(tblDataSource);
						EnableButtons(blnIsEdit);
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
		}

		//**************************************************************************              
		///    <Description>
		///       Close the form
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
		///    06-Jan-2005
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

		//**************************************************************************              
		///    <Description>
		///       Check whether if form mode is not equal to Default then display confirm message
		///       Closing the form based on user selection.
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
		///    06-Jan-2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private void DrillDownReport_Closing(object sender, CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".DrillDownReport_Closing()";
			try
			{
				if (this.mEnumType != EnumAction.Default)
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
		//**************************************************************************              
		///    <Description>
		///       Click, Validate data and save new record
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
		///    06-Jan-2005
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
				this.SaveData();
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
		///       Click event, turn form to edit mode
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
		///    06-Jan-2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private void btnEdit_Click(object sender, EventArgs e)
		{
			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.WaitCursor;

			#endregion Code Inserted Automatically

			
			const string METHOD_NAME = THIS + ".btnEdit_Click()";
			try
			{
				this.gridDrillDownReport.AllowDelete = true;
				this.gridDrillDownReport.AllowUpdate = true;
				btnDelete.Enabled = true;
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
		///       Click event, delete parameter or display confirm message then delete selected record
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
		///    06-Jan-2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private void btnDelete_Click(object sender, EventArgs e)
		{
			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.WaitCursor;

			#endregion Code Inserted Automatically

			
			const string METHOD_NAME = THIS + ".btnDelete_Click()";
			try
			{
				// get selected row from C1TrueDBGrid
				SelectedRowCollection objSelectedRowCollection = gridDrillDownReport.SelectedRows;
				// delete parameter
				if (objSelectedRowCollection.Count > 0)
				{
					IEnumerator objEnum = objSelectedRowCollection.GetEnumerator();
					while (objEnum.MoveNext())
					{
						// remove the coresponding row in DataSource of grid with selected index
						tblDataSource.Rows.RemoveAt(int.Parse(objEnum.Current.ToString()));
					}
				}
				// if not select any parameter, display confirm message to delete drill down report
				else if (txtReportCode.Text != string.Empty)
				{
					DialogResult dlgResult = PCSMessageBox.Show(ErrorCode.MESSAGE_DELETE_RECORD, 
						MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
					if (dlgResult == DialogResult.Yes)
					{
						// delete drill down report
						if (boDrillDownReport.Delete(this.mMasterReportID, txtReportCode.Text) <= 0)
						{
							// TODO: using PCSMessageBox to display alert
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

			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.Default;

			#endregion Code Inserted Automatically

			
		}

		//**************************************************************************              
		///    <Description>
		///       When user check/uncheck FromColumn column then disable/enable MasterPara column
		///       If FromColumn is checked then MasterPara will display a combo box with all fields of master report
		///       Else MasterPara will display a combo box with all parameters of master report
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
		///    06-Jan-2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private void gridDrillDownReport_AfterColUpdate(object sender, ColEventArgs e)
		{
			const string METHOD_NAME = THIS + ".gridDrillDownReport_AfterColUpdate()";
			try
			{
				C1DataColumn dcolSelected = this.gridDrillDownReport.Columns[e.ColIndex];
				C1DataColumn dcolMasterPara = this.gridDrillDownReport.Columns[sys_ReportDrillDownTable.MASTERPARA_FLD];
				if (dcolSelected.DataField == sys_ReportDrillDownTable.FROMCOLUMN_FLD)
				{
					drowOldValue = tblDataSource.Rows[this.gridDrillDownReport.Row];
					string strOldValue = drowOldValue[sys_ReportDrillDownTable.MASTERPARA_FLD].ToString();
					bool blnChecked = bool.Parse(dcolSelected.Value.ToString());
					if (blnChecked)
					{
						dcolMasterPara.ValueItems.Presentation = PresentationEnum.ComboBox;
						// display master para as combo box with values is fields
						//this.gridDrillDownReport.Columns[sys_ReportDrillDownTable.MASTERPARA_FLD]
						dcolMasterPara.ValueItems.Values.Clear();
						for (int i = 0; i < arrMasterFields.Count; i++)
						{
							sys_ReportFieldsVO voReportField = (sys_ReportFieldsVO)arrMasterFields[i];
							ValueItem objValueItem = new ValueItem(voReportField.FieldName, voReportField.FieldName);
							dcolMasterPara.ValueItems.Values.Add(objValueItem);
						}
					}
					else
					{
						dcolMasterPara.ValueItems.Values.Clear();
						// display master para as combo box with values is parameters
						dcolMasterPara.ValueItems.Presentation = PresentationEnum.ComboBox;
						// display master para as combo box with values is fields
						for (int i = 0; i < arrMasterParameters.Count; i++)
						{
							sys_ReportParaVO voReportPara = (sys_ReportParaVO)arrMasterParameters[i];
							ValueItem objValueItem = new ValueItem(voReportPara.ParaName, voReportPara.ParaName);
							dcolMasterPara.ValueItems.Values.Add(objValueItem);
						}
						//this.gridDrillDownReport.Columns[sys_ReportDrillDownTable.MASTERPARA_FLD].ValueItems.Values.
					}
					if (drowOldValue[sys_ReportDrillDownTable.FROMCOLUMN_FLD].Equals(true.ToString()))
					{
						if (blnChecked) // return to default value
						{
							dcolMasterPara.Value = string.Empty;
							dcolMasterPara.Text = string.Empty;
							dcolMasterPara.Text = strOldValue;
							dcolMasterPara.Value = strOldValue;
							//this.gridDrillDownReport[this.gridDrillDownReport.Row, 0] = drowSelectedData[sys_ReportDrillDownTable.MASTERPARA_FLD].ToString();
						}
					}
					else
					{
						if (!blnChecked) // return to default value
						{
							dcolMasterPara.Value = string.Empty;
							dcolMasterPara.Text = string.Empty;
							dcolMasterPara.Text = strOldValue;
							dcolMasterPara.Value = strOldValue;
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
		}
		#endregion

		#region Private Methods

		//**************************************************************************              
		///    <Description>
		///       Get all fields from Master Report
		///    </Description>
		///    <Inputs>
		///       MasterReportID 
		///    </Inputs>
		///    <Outputs>
		///		  ArrayList
		///    </Outputs>
		///    <Returns>
		///		  List of fields
		///    </Returns>
		///    <Authors>
		///       DungLA
		///    </Authors>
		///    <History>
		///    01-Feb-2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private ArrayList GetAllFieldsFromMaster(string pstrMasterReportID)
		{
			try
			{
				// get all fields of master report
				FieldPropertiesBO boFieldProperties = new FieldPropertiesBO();
				return boFieldProperties.ListByReport(pstrMasterReportID);
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
		///       Get all parameters from Master Report
		///    </Description>
		///    <Inputs>
		///       DataTable 
		///    </Inputs>
		///    <Outputs>
		///       ArrayList
		///    </Outputs>
		///    <Returns>
		///       ArrayList
		///    </Returns>
		///    <Authors>
		///       DungLA
		///    </Authors>
		///    <History>
		///    01-Feb-2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private ArrayList GetAllParametersFromMaster(string pstrMasterReportID)
		{
			try
			{
				// get all parameters of master report
				ReportParameterBO boParameter = new ReportParameterBO();
				return boParameter.ListByReport(pstrMasterReportID);
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
		///       Bind the C1TrueDBGrid with specified datasource
		///    </Description>
		///    <Inputs>
		///       DataTable 
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
		///    06-Jan-2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private void BindTrueDBGrid(DataTable ptblSource)
		{
			try
			{ // clear the data source first
				this.gridDrillDownReport.DataSource = null;
				// bind to grid
				this.gridDrillDownReport.DataSource = this.ConvertDataType(ptblSource);
				// display FromColumn as check box
				this.gridDrillDownReport.Columns[sys_ReportDrillDownTable.FROMCOLUMN_FLD].ValueItems.Presentation = PresentationEnum.CheckBox;
				// do not display ParaOrder column
				this.gridDrillDownReport.Columns.RemoveAt(ORDER_COLUMN_INDEX);
				// lock data type and detail para column
				this.gridDrillDownReport.Splits[0].DisplayColumns[sys_ReportParaTable.DATATYPE_FLD].Locked = true;
				this.gridDrillDownReport.Splits[0].DisplayColumns[sys_ReportDrillDownTable.DETAILPARA_FLD].Locked = true;
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
		///       This method use to convert data type number to string, then display to user
		///    </Description>
		///    <Inputs>
		///       DataTable 
		///    </Inputs>
		///    <Outputs>
		///       Changed DataTable
		///    </Outputs>
		///    <Returns>
		///    
		///    </Returns>
		///    <Authors>
		///       DungLA
		///    </Authors>
		///    <History>
		///    06-Jan-2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private DataTable ConvertDataType(DataTable ptblSource)
		{
			try
			{
				IDictionaryEnumerator objEnum = htDataType.GetEnumerator();
				foreach (DataRow drow in ptblSource.Rows)
				{
					while (objEnum.MoveNext())
					{
						if (objEnum.Key.ToString().Equals(drow[sys_ReportParaTable.DATATYPE_FLD].ToString()))
						{
							drow[sys_ReportParaTable.DATATYPE_FLD] = objEnum.Value;
							break;
						}
					}
				}
				return ptblSource;
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
		///       Enable button based on selected item
		///    </Description>
		///    <Inputs>
		///       boolean: is editable?
		///    </Inputs>
		///    <Outputs>
		///       N/A
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
		private void EnableButtons(bool pblnIsEdit)
		{
			try
			{
				btnEdit.Enabled = (pblnIsEdit) ? true : false;
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
		///       This method use to check for mandatory field(s) in the form
		///    </Description>
		///    <Inputs>
		///       Control to check
		///    </Inputs>
		///    <Outputs>
		///       bool
		///    </Outputs>
		///    <Returns>
		///       true if validated, false if failed
		///    </Returns>
		///    <Authors>
		///       DungLA
		///    </Authors>
		///    <History>
		///       3-Jan-2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private bool CheckMandatory(Control pobjControl)
		{
			try
			{
				if (pobjControl.Text.Trim() == string.Empty)
				{
					return false;
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
				// validate data
				if (!CheckMandatory(txtReportCode))
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID);
					txtReportCode.Focus();
					txtReportCode.Select();
					return false;
				}
				if (!CheckMandatory(txtReportName))
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID);
					txtReportName.Focus();
					txtReportName.Select();
					return false;
				}
				// delete old drill down record first
				boDrillDownReport.Delete(this.MasterReportID, this.txtReportCode.Text);

				// for each param, we create new VO object and store to database
				DataTable dtblSource = (DataTable) (this.gridDrillDownReport.DataSource);
				int intOrder = 1;
				if (dtblSource.Rows.Count > 0)
				{
					foreach (DataRow drow in dtblSource.Rows)
					{
						sys_ReportDrillDownVO voDrillDown = new sys_ReportDrillDownVO();
						voDrillDown.MasterReportID = this.MasterReportID;
						voDrillDown.DetailReportID = txtReportCode.Text.Trim();
						voDrillDown.MasterPara = drow[sys_ReportDrillDownTable.MASTERPARA_FLD].ToString();
						voDrillDown.DetailPara = drow[sys_ReportDrillDownTable.DETAILPARA_FLD].ToString();
						voDrillDown.FromColumn = bool.Parse(drow[sys_ReportDrillDownTable.FROMCOLUMN_FLD].ToString());
						voDrillDown.ParaOrder = intOrder;
						boDrillDownReport.Add(voDrillDown);
						intOrder++;
					}
				}
				else
				{
					// alert user that there is no parameter
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID);
					return false;
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
	}
}