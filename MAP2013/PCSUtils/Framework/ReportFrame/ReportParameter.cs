using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using C1.Win.C1Input;
using PCSComUtils.Common;
using PCSComUtils.Framework.ReportFrame.BO;
using PCSComUtils.Framework.ReportFrame.DS;
using PCSComUtils.Framework.TableFrame.BO;
using PCSComUtils.PCSExc;
using PCSUtils.Log;
using PCSUtils.Utils;

namespace PCSUtils.Framework.ReportFrame
{
	/// <summary>
	/// Summary description for Parameter.
	/// </summary>
	public class ReportParameter : Form
	{
		#region controls

		private Button btnHelp;
		private Button btnMoveDown;
		private Button btnMoveUp;
		private Button btnClose;		
		private Button btnSave;
		private Button btnEdit;
		private Button btnAdd;
		private Button btnDelete;

		private Container components = null;

		#endregion


		private string strReportID;
		private EnumAction mFormAction;
		private sys_ReportVO voReport;
		private ReportParameterBO boReportParameter = new ReportParameterBO();
		//private ViewTableBO boViewTable = new ViewTableBO();
		private string strSelectedPara = string.Empty;
		//private sys_ReportParaVO mvoReportPara;
		private sys_ReportParaVO mvoSelectedPara;

		//private TableConfigDetailBO boTableConfigDetail = new TableConfigDetailBO();

		private Hashtable htDataType = new Hashtable();
		private System.Windows.Forms.TreeView tvwParameterList;

		private ArrayList marrParams = new ArrayList();
		private System.Windows.Forms.GroupBox grpDetails;
		private C1.Win.C1Input.C1NumericEdit txtWidth2;
		private C1.Win.C1Input.C1NumericEdit txtWidth;
		private System.Windows.Forms.Label lblWidth2;
		private System.Windows.Forms.Label lblWidth1;
		private System.Windows.Forms.Label lblDataType;
		private System.Windows.Forms.Label lblFromTable;
		private System.Windows.Forms.ComboBox cboFromTable;
		private System.Windows.Forms.Label lblName;
		private System.Windows.Forms.Label lblFromField;
		private System.Windows.Forms.ComboBox cboFromField;
		private System.Windows.Forms.TextBox txtCaption;
		private System.Windows.Forms.Label lblDefaultValue;
		private System.Windows.Forms.Label lblWhereClause;
		private System.Windows.Forms.TextBox txtWhereClause;
		private System.Windows.Forms.CheckBox chkOptional;
		private System.Windows.Forms.CheckBox chkTagValue;
		private System.Windows.Forms.Label lblFilterField1;
		private System.Windows.Forms.ComboBox cboFilterField1;
		private System.Windows.Forms.TextBox txtSQL;
		private System.Windows.Forms.Label lblSQL;
		private System.Windows.Forms.Label lblItems;
		private System.Windows.Forms.TextBox txtItems;
		private System.Windows.Forms.Label lblFilterField2;
		private System.Windows.Forms.ComboBox cboFilterField2;
		private System.Windows.Forms.Label lblWidth;
		private System.Windows.Forms.CheckBox chkSameRow;
		private System.Windows.Forms.TextBox txtDefaultValue;
		private System.Windows.Forms.Label lblCaption;
		private System.Windows.Forms.ComboBox cboDataType;
		private System.Windows.Forms.TextBox txtParameterName;
		private C1.Win.C1Input.C1NumericEdit txtWidth1;
		private System.Windows.Forms.Label lblMW2;
		private System.Windows.Forms.Label lblMW1;
		private System.Windows.Forms.Label lblMW;
		private System.Windows.Forms.CheckBox chkMultiSelection;
	
		public ArrayList Params
		{
			get { return marrParams; }
			set { marrParams = value; }
		}

		public EnumAction EnumType
		{
			get { return this.mFormAction; }
			set { this.mFormAction = value; }
		}

		private const string THIS = "PCSUtils.Framework.ReportFrame.ReportParameter";
		//**************************************************************************              
		///    <Description>
		///       Default constructor with selected report object
		///    </Description>
		///    <Inputs>
		///       ReportNode object
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
		public ReportParameter(object pobjReport)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			#region init hashtable data type are define in C#

			htDataType.Clear();

			htDataType.Add((int)TypeCode.Boolean, TypeCode.Boolean.ToString());
			htDataType.Add((int)TypeCode.Char, TypeCode.Char.ToString());
			htDataType.Add((int)TypeCode.DateTime, TypeCode.DateTime.ToString());
			htDataType.Add((int)TypeCode.Decimal, TypeCode.Decimal.ToString());
			htDataType.Add((int)TypeCode.Double, TypeCode.Double.ToString());
			htDataType.Add((int)TypeCode.Int16, TypeCode.Int16.ToString());
			htDataType.Add((int)TypeCode.Int32, TypeCode.Int32.ToString());
			htDataType.Add((int)TypeCode.Int64, TypeCode.Int64.ToString());
			// HACKED: Thachnn to Bro.Dungla: remove to avoid ambiguous: htDataType.Add((int)TypeCode.Single, TypeCode.Single.ToString());
			htDataType.Add((int)TypeCode.String, TypeCode.String.ToString());

			#endregion

			try
			{
				voReport = (sys_ReportVO)(pobjReport);
				this.strReportID = voReport.ReportID;
				// set form caption
				this.Text = this.Text + voReport.ReportName + Constants.CLOSE_SBRACKET;
			}
			catch (InvalidCastException ex)
			{
				// display error message
				PCSMessageBox.Show(ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
				// log error message
				try
				{
					Logger.LogMessage(ex, THIS, Level.ERROR);
				}
				catch (Exception exLog)
				{
					PCSMessageBox.Show(exLog.Message, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
				}
			}
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(ReportParameter));
			this.btnHelp = new System.Windows.Forms.Button();
			this.btnClose = new System.Windows.Forms.Button();
			this.btnMoveDown = new System.Windows.Forms.Button();
			this.btnMoveUp = new System.Windows.Forms.Button();
			this.btnSave = new System.Windows.Forms.Button();
			this.btnEdit = new System.Windows.Forms.Button();
			this.btnAdd = new System.Windows.Forms.Button();
			this.btnDelete = new System.Windows.Forms.Button();
			this.tvwParameterList = new System.Windows.Forms.TreeView();
			this.grpDetails = new System.Windows.Forms.GroupBox();
			this.lblMW = new System.Windows.Forms.Label();
			this.lblMW2 = new System.Windows.Forms.Label();
			this.lblMW1 = new System.Windows.Forms.Label();
			this.txtWidth2 = new C1.Win.C1Input.C1NumericEdit();
			this.txtWidth = new C1.Win.C1Input.C1NumericEdit();
			this.lblWidth2 = new System.Windows.Forms.Label();
			this.lblWidth1 = new System.Windows.Forms.Label();
			this.lblDataType = new System.Windows.Forms.Label();
			this.lblFromTable = new System.Windows.Forms.Label();
			this.cboFromTable = new System.Windows.Forms.ComboBox();
			this.lblName = new System.Windows.Forms.Label();
			this.lblFromField = new System.Windows.Forms.Label();
			this.cboFromField = new System.Windows.Forms.ComboBox();
			this.txtCaption = new System.Windows.Forms.TextBox();
			this.lblDefaultValue = new System.Windows.Forms.Label();
			this.lblWhereClause = new System.Windows.Forms.Label();
			this.txtWhereClause = new System.Windows.Forms.TextBox();
			this.chkOptional = new System.Windows.Forms.CheckBox();
			this.chkTagValue = new System.Windows.Forms.CheckBox();
			this.lblFilterField1 = new System.Windows.Forms.Label();
			this.cboFilterField1 = new System.Windows.Forms.ComboBox();
			this.txtSQL = new System.Windows.Forms.TextBox();
			this.lblSQL = new System.Windows.Forms.Label();
			this.lblItems = new System.Windows.Forms.Label();
			this.txtItems = new System.Windows.Forms.TextBox();
			this.lblFilterField2 = new System.Windows.Forms.Label();
			this.cboFilterField2 = new System.Windows.Forms.ComboBox();
			this.lblWidth = new System.Windows.Forms.Label();
			this.chkSameRow = new System.Windows.Forms.CheckBox();
			this.txtDefaultValue = new System.Windows.Forms.TextBox();
			this.lblCaption = new System.Windows.Forms.Label();
			this.cboDataType = new System.Windows.Forms.ComboBox();
			this.txtParameterName = new System.Windows.Forms.TextBox();
			this.txtWidth1 = new C1.Win.C1Input.C1NumericEdit();
			this.chkMultiSelection = new System.Windows.Forms.CheckBox();
			this.grpDetails.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.txtWidth2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.txtWidth)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.txtWidth1)).BeginInit();
			this.SuspendLayout();
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
			// btnMoveDown
			// 
			this.btnMoveDown.AccessibleDescription = resources.GetString("btnMoveDown.AccessibleDescription");
			this.btnMoveDown.AccessibleName = resources.GetString("btnMoveDown.AccessibleName");
			this.btnMoveDown.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnMoveDown.Anchor")));
			this.btnMoveDown.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnMoveDown.BackgroundImage")));
			this.btnMoveDown.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnMoveDown.Dock")));
			this.btnMoveDown.Enabled = ((bool)(resources.GetObject("btnMoveDown.Enabled")));
			this.btnMoveDown.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnMoveDown.FlatStyle")));
			this.btnMoveDown.Font = ((System.Drawing.Font)(resources.GetObject("btnMoveDown.Font")));
			this.btnMoveDown.Image = ((System.Drawing.Image)(resources.GetObject("btnMoveDown.Image")));
			this.btnMoveDown.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnMoveDown.ImageAlign")));
			this.btnMoveDown.ImageIndex = ((int)(resources.GetObject("btnMoveDown.ImageIndex")));
			this.btnMoveDown.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnMoveDown.ImeMode")));
			this.btnMoveDown.Location = ((System.Drawing.Point)(resources.GetObject("btnMoveDown.Location")));
			this.btnMoveDown.Name = "btnMoveDown";
			this.btnMoveDown.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnMoveDown.RightToLeft")));
			this.btnMoveDown.Size = ((System.Drawing.Size)(resources.GetObject("btnMoveDown.Size")));
			this.btnMoveDown.TabIndex = ((int)(resources.GetObject("btnMoveDown.TabIndex")));
			this.btnMoveDown.Text = resources.GetString("btnMoveDown.Text");
			this.btnMoveDown.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnMoveDown.TextAlign")));
			this.btnMoveDown.Visible = ((bool)(resources.GetObject("btnMoveDown.Visible")));
			this.btnMoveDown.Click += new System.EventHandler(this.btnMoveDown_Click);
			// 
			// btnMoveUp
			// 
			this.btnMoveUp.AccessibleDescription = resources.GetString("btnMoveUp.AccessibleDescription");
			this.btnMoveUp.AccessibleName = resources.GetString("btnMoveUp.AccessibleName");
			this.btnMoveUp.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnMoveUp.Anchor")));
			this.btnMoveUp.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnMoveUp.BackgroundImage")));
			this.btnMoveUp.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnMoveUp.Dock")));
			this.btnMoveUp.Enabled = ((bool)(resources.GetObject("btnMoveUp.Enabled")));
			this.btnMoveUp.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnMoveUp.FlatStyle")));
			this.btnMoveUp.Font = ((System.Drawing.Font)(resources.GetObject("btnMoveUp.Font")));
			this.btnMoveUp.Image = ((System.Drawing.Image)(resources.GetObject("btnMoveUp.Image")));
			this.btnMoveUp.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnMoveUp.ImageAlign")));
			this.btnMoveUp.ImageIndex = ((int)(resources.GetObject("btnMoveUp.ImageIndex")));
			this.btnMoveUp.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnMoveUp.ImeMode")));
			this.btnMoveUp.Location = ((System.Drawing.Point)(resources.GetObject("btnMoveUp.Location")));
			this.btnMoveUp.Name = "btnMoveUp";
			this.btnMoveUp.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnMoveUp.RightToLeft")));
			this.btnMoveUp.Size = ((System.Drawing.Size)(resources.GetObject("btnMoveUp.Size")));
			this.btnMoveUp.TabIndex = ((int)(resources.GetObject("btnMoveUp.TabIndex")));
			this.btnMoveUp.Text = resources.GetString("btnMoveUp.Text");
			this.btnMoveUp.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnMoveUp.TextAlign")));
			this.btnMoveUp.Visible = ((bool)(resources.GetObject("btnMoveUp.Visible")));
			this.btnMoveUp.Click += new System.EventHandler(this.btnMoveUp_Click);
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
			// btnAdd
			// 
			this.btnAdd.AccessibleDescription = resources.GetString("btnAdd.AccessibleDescription");
			this.btnAdd.AccessibleName = resources.GetString("btnAdd.AccessibleName");
			this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnAdd.Anchor")));
			this.btnAdd.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnAdd.BackgroundImage")));
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
			// tvwParameterList
			// 
			this.tvwParameterList.AccessibleDescription = resources.GetString("tvwParameterList.AccessibleDescription");
			this.tvwParameterList.AccessibleName = resources.GetString("tvwParameterList.AccessibleName");
			this.tvwParameterList.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tvwParameterList.Anchor")));
			this.tvwParameterList.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tvwParameterList.BackgroundImage")));
			this.tvwParameterList.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tvwParameterList.Dock")));
			this.tvwParameterList.Enabled = ((bool)(resources.GetObject("tvwParameterList.Enabled")));
			this.tvwParameterList.Font = ((System.Drawing.Font)(resources.GetObject("tvwParameterList.Font")));
			this.tvwParameterList.FullRowSelect = true;
			this.tvwParameterList.HideSelection = false;
			this.tvwParameterList.ImageIndex = ((int)(resources.GetObject("tvwParameterList.ImageIndex")));
			this.tvwParameterList.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tvwParameterList.ImeMode")));
			this.tvwParameterList.Indent = ((int)(resources.GetObject("tvwParameterList.Indent")));
			this.tvwParameterList.ItemHeight = ((int)(resources.GetObject("tvwParameterList.ItemHeight")));
			this.tvwParameterList.Location = ((System.Drawing.Point)(resources.GetObject("tvwParameterList.Location")));
			this.tvwParameterList.Name = "tvwParameterList";
			this.tvwParameterList.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tvwParameterList.RightToLeft")));
			this.tvwParameterList.SelectedImageIndex = ((int)(resources.GetObject("tvwParameterList.SelectedImageIndex")));
			this.tvwParameterList.Size = ((System.Drawing.Size)(resources.GetObject("tvwParameterList.Size")));
			this.tvwParameterList.TabIndex = ((int)(resources.GetObject("tvwParameterList.TabIndex")));
			this.tvwParameterList.Text = resources.GetString("tvwParameterList.Text");
			this.tvwParameterList.Visible = ((bool)(resources.GetObject("tvwParameterList.Visible")));
			this.tvwParameterList.DoubleClick += new System.EventHandler(this.tvwParameterList_DoubleClick);
			this.tvwParameterList.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvwParameterList_AfterSelect);
			// 
			// grpDetails
			// 
			this.grpDetails.AccessibleDescription = resources.GetString("grpDetails.AccessibleDescription");
			this.grpDetails.AccessibleName = resources.GetString("grpDetails.AccessibleName");
			this.grpDetails.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("grpDetails.Anchor")));
			this.grpDetails.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("grpDetails.BackgroundImage")));
			this.grpDetails.Controls.Add(this.chkMultiSelection);
			this.grpDetails.Controls.Add(this.lblMW);
			this.grpDetails.Controls.Add(this.lblMW2);
			this.grpDetails.Controls.Add(this.lblMW1);
			this.grpDetails.Controls.Add(this.txtWidth2);
			this.grpDetails.Controls.Add(this.txtWidth);
			this.grpDetails.Controls.Add(this.lblWidth2);
			this.grpDetails.Controls.Add(this.lblWidth1);
			this.grpDetails.Controls.Add(this.lblDataType);
			this.grpDetails.Controls.Add(this.lblFromTable);
			this.grpDetails.Controls.Add(this.cboFromTable);
			this.grpDetails.Controls.Add(this.lblName);
			this.grpDetails.Controls.Add(this.lblFromField);
			this.grpDetails.Controls.Add(this.cboFromField);
			this.grpDetails.Controls.Add(this.txtCaption);
			this.grpDetails.Controls.Add(this.lblDefaultValue);
			this.grpDetails.Controls.Add(this.lblWhereClause);
			this.grpDetails.Controls.Add(this.txtWhereClause);
			this.grpDetails.Controls.Add(this.chkOptional);
			this.grpDetails.Controls.Add(this.chkTagValue);
			this.grpDetails.Controls.Add(this.lblFilterField1);
			this.grpDetails.Controls.Add(this.cboFilterField1);
			this.grpDetails.Controls.Add(this.txtSQL);
			this.grpDetails.Controls.Add(this.lblSQL);
			this.grpDetails.Controls.Add(this.lblItems);
			this.grpDetails.Controls.Add(this.txtItems);
			this.grpDetails.Controls.Add(this.lblFilterField2);
			this.grpDetails.Controls.Add(this.cboFilterField2);
			this.grpDetails.Controls.Add(this.lblWidth);
			this.grpDetails.Controls.Add(this.chkSameRow);
			this.grpDetails.Controls.Add(this.txtDefaultValue);
			this.grpDetails.Controls.Add(this.lblCaption);
			this.grpDetails.Controls.Add(this.cboDataType);
			this.grpDetails.Controls.Add(this.txtParameterName);
			this.grpDetails.Controls.Add(this.txtWidth1);
			this.grpDetails.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("grpDetails.Dock")));
			this.grpDetails.Enabled = ((bool)(resources.GetObject("grpDetails.Enabled")));
			this.grpDetails.Font = ((System.Drawing.Font)(resources.GetObject("grpDetails.Font")));
			this.grpDetails.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("grpDetails.ImeMode")));
			this.grpDetails.Location = ((System.Drawing.Point)(resources.GetObject("grpDetails.Location")));
			this.grpDetails.Name = "grpDetails";
			this.grpDetails.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("grpDetails.RightToLeft")));
			this.grpDetails.Size = ((System.Drawing.Size)(resources.GetObject("grpDetails.Size")));
			this.grpDetails.TabIndex = ((int)(resources.GetObject("grpDetails.TabIndex")));
			this.grpDetails.TabStop = false;
			this.grpDetails.Text = resources.GetString("grpDetails.Text");
			this.grpDetails.Visible = ((bool)(resources.GetObject("grpDetails.Visible")));
			// 
			// lblMW
			// 
			this.lblMW.AccessibleDescription = resources.GetString("lblMW.AccessibleDescription");
			this.lblMW.AccessibleName = resources.GetString("lblMW.AccessibleName");
			this.lblMW.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblMW.Anchor")));
			this.lblMW.AutoSize = ((bool)(resources.GetObject("lblMW.AutoSize")));
			this.lblMW.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblMW.Dock")));
			this.lblMW.Enabled = ((bool)(resources.GetObject("lblMW.Enabled")));
			this.lblMW.Font = ((System.Drawing.Font)(resources.GetObject("lblMW.Font")));
			this.lblMW.Image = ((System.Drawing.Image)(resources.GetObject("lblMW.Image")));
			this.lblMW.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblMW.ImageAlign")));
			this.lblMW.ImageIndex = ((int)(resources.GetObject("lblMW.ImageIndex")));
			this.lblMW.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblMW.ImeMode")));
			this.lblMW.Location = ((System.Drawing.Point)(resources.GetObject("lblMW.Location")));
			this.lblMW.Name = "lblMW";
			this.lblMW.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblMW.RightToLeft")));
			this.lblMW.Size = ((System.Drawing.Size)(resources.GetObject("lblMW.Size")));
			this.lblMW.TabIndex = ((int)(resources.GetObject("lblMW.TabIndex")));
			this.lblMW.Text = resources.GetString("lblMW.Text");
			this.lblMW.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblMW.TextAlign")));
			this.lblMW.Visible = ((bool)(resources.GetObject("lblMW.Visible")));
			// 
			// lblMW2
			// 
			this.lblMW2.AccessibleDescription = resources.GetString("lblMW2.AccessibleDescription");
			this.lblMW2.AccessibleName = resources.GetString("lblMW2.AccessibleName");
			this.lblMW2.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblMW2.Anchor")));
			this.lblMW2.AutoSize = ((bool)(resources.GetObject("lblMW2.AutoSize")));
			this.lblMW2.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblMW2.Dock")));
			this.lblMW2.Enabled = ((bool)(resources.GetObject("lblMW2.Enabled")));
			this.lblMW2.Font = ((System.Drawing.Font)(resources.GetObject("lblMW2.Font")));
			this.lblMW2.Image = ((System.Drawing.Image)(resources.GetObject("lblMW2.Image")));
			this.lblMW2.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblMW2.ImageAlign")));
			this.lblMW2.ImageIndex = ((int)(resources.GetObject("lblMW2.ImageIndex")));
			this.lblMW2.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblMW2.ImeMode")));
			this.lblMW2.Location = ((System.Drawing.Point)(resources.GetObject("lblMW2.Location")));
			this.lblMW2.Name = "lblMW2";
			this.lblMW2.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblMW2.RightToLeft")));
			this.lblMW2.Size = ((System.Drawing.Size)(resources.GetObject("lblMW2.Size")));
			this.lblMW2.TabIndex = ((int)(resources.GetObject("lblMW2.TabIndex")));
			this.lblMW2.Text = resources.GetString("lblMW2.Text");
			this.lblMW2.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblMW2.TextAlign")));
			this.lblMW2.Visible = ((bool)(resources.GetObject("lblMW2.Visible")));
			// 
			// lblMW1
			// 
			this.lblMW1.AccessibleDescription = resources.GetString("lblMW1.AccessibleDescription");
			this.lblMW1.AccessibleName = resources.GetString("lblMW1.AccessibleName");
			this.lblMW1.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblMW1.Anchor")));
			this.lblMW1.AutoSize = ((bool)(resources.GetObject("lblMW1.AutoSize")));
			this.lblMW1.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblMW1.Dock")));
			this.lblMW1.Enabled = ((bool)(resources.GetObject("lblMW1.Enabled")));
			this.lblMW1.Font = ((System.Drawing.Font)(resources.GetObject("lblMW1.Font")));
			this.lblMW1.Image = ((System.Drawing.Image)(resources.GetObject("lblMW1.Image")));
			this.lblMW1.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblMW1.ImageAlign")));
			this.lblMW1.ImageIndex = ((int)(resources.GetObject("lblMW1.ImageIndex")));
			this.lblMW1.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblMW1.ImeMode")));
			this.lblMW1.Location = ((System.Drawing.Point)(resources.GetObject("lblMW1.Location")));
			this.lblMW1.Name = "lblMW1";
			this.lblMW1.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblMW1.RightToLeft")));
			this.lblMW1.Size = ((System.Drawing.Size)(resources.GetObject("lblMW1.Size")));
			this.lblMW1.TabIndex = ((int)(resources.GetObject("lblMW1.TabIndex")));
			this.lblMW1.Text = resources.GetString("lblMW1.Text");
			this.lblMW1.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblMW1.TextAlign")));
			this.lblMW1.Visible = ((bool)(resources.GetObject("lblMW1.Visible")));
			// 
			// txtWidth2
			// 
			this.txtWidth2.AcceptsEscape = ((bool)(resources.GetObject("txtWidth2.AcceptsEscape")));
			this.txtWidth2.AccessibleDescription = resources.GetString("txtWidth2.AccessibleDescription");
			this.txtWidth2.AccessibleName = resources.GetString("txtWidth2.AccessibleName");
			this.txtWidth2.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("txtWidth2.Anchor")));
			this.txtWidth2.AutoSize = ((bool)(resources.GetObject("txtWidth2.AutoSize")));
			this.txtWidth2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("txtWidth2.BackgroundImage")));
			this.txtWidth2.BorderStyle = ((System.Windows.Forms.BorderStyle)(resources.GetObject("txtWidth2.BorderStyle")));
			// 
			// txtWidth2.Calculator
			// 
			this.txtWidth2.Calculator.AccessibleDescription = resources.GetString("txtWidth2.Calculator.AccessibleDescription");
			this.txtWidth2.Calculator.AccessibleName = resources.GetString("txtWidth2.Calculator.AccessibleName");
			this.txtWidth2.Calculator.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("txtWidth2.Calculator.BackgroundImage")));
			this.txtWidth2.Calculator.ButtonFlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("txtWidth2.Calculator.ButtonFlatStyle")));
			this.txtWidth2.Calculator.DisplayFormat = resources.GetString("txtWidth2.Calculator.DisplayFormat");
			this.txtWidth2.Calculator.Font = ((System.Drawing.Font)(resources.GetObject("txtWidth2.Calculator.Font")));
			this.txtWidth2.Calculator.FormatOnClose = ((bool)(resources.GetObject("txtWidth2.Calculator.FormatOnClose")));
			this.txtWidth2.Calculator.StoredFormat = resources.GetString("txtWidth2.Calculator.StoredFormat");
			this.txtWidth2.Calculator.UIStrings.Content = ((string[])(resources.GetObject("txtWidth2.Calculator.UIStrings.Content")));
			this.txtWidth2.CaseSensitive = ((bool)(resources.GetObject("txtWidth2.CaseSensitive")));
			this.txtWidth2.Culture = ((int)(resources.GetObject("txtWidth2.Culture")));
			this.txtWidth2.CustomFormat = resources.GetString("txtWidth2.CustomFormat");
			this.txtWidth2.DataType = ((System.Type)(resources.GetObject("txtWidth2.DataType")));
			this.txtWidth2.DisplayFormat.CustomFormat = resources.GetString("txtWidth2.DisplayFormat.CustomFormat");
			this.txtWidth2.DisplayFormat.FormatType = ((C1.Win.C1Input.FormatTypeEnum)(resources.GetObject("txtWidth2.DisplayFormat.FormatType")));
			this.txtWidth2.DisplayFormat.Inherit = ((C1.Win.C1Input.FormatInfoInheritFlags)(resources.GetObject("txtWidth2.DisplayFormat.Inherit")));
			this.txtWidth2.DisplayFormat.NullText = resources.GetString("txtWidth2.DisplayFormat.NullText");
			this.txtWidth2.DisplayFormat.TrimEnd = ((bool)(resources.GetObject("txtWidth2.DisplayFormat.TrimEnd")));
			this.txtWidth2.DisplayFormat.TrimStart = ((bool)(resources.GetObject("txtWidth2.DisplayFormat.TrimStart")));
			this.txtWidth2.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("txtWidth2.Dock")));
			this.txtWidth2.DropDownFormAlign = ((C1.Win.C1Input.DropDownFormAlignmentEnum)(resources.GetObject("txtWidth2.DropDownFormAlign")));
			this.txtWidth2.EditFormat.CustomFormat = resources.GetString("txtWidth2.EditFormat.CustomFormat");
			this.txtWidth2.EditFormat.FormatType = ((C1.Win.C1Input.FormatTypeEnum)(resources.GetObject("txtWidth2.EditFormat.FormatType")));
			this.txtWidth2.EditFormat.Inherit = ((C1.Win.C1Input.FormatInfoInheritFlags)(resources.GetObject("txtWidth2.EditFormat.Inherit")));
			this.txtWidth2.EditFormat.NullText = resources.GetString("txtWidth2.EditFormat.NullText");
			this.txtWidth2.EditFormat.TrimEnd = ((bool)(resources.GetObject("txtWidth2.EditFormat.TrimEnd")));
			this.txtWidth2.EditFormat.TrimStart = ((bool)(resources.GetObject("txtWidth2.EditFormat.TrimStart")));
			this.txtWidth2.EditMask = resources.GetString("txtWidth2.EditMask");
			this.txtWidth2.EmptyAsNull = ((bool)(resources.GetObject("txtWidth2.EmptyAsNull")));
			this.txtWidth2.Enabled = ((bool)(resources.GetObject("txtWidth2.Enabled")));
			this.txtWidth2.ErrorInfo.BeepOnError = ((bool)(resources.GetObject("txtWidth2.ErrorInfo.BeepOnError")));
			this.txtWidth2.ErrorInfo.ErrorMessage = resources.GetString("txtWidth2.ErrorInfo.ErrorMessage");
			this.txtWidth2.ErrorInfo.ErrorMessageCaption = resources.GetString("txtWidth2.ErrorInfo.ErrorMessageCaption");
			this.txtWidth2.ErrorInfo.ShowErrorMessage = ((bool)(resources.GetObject("txtWidth2.ErrorInfo.ShowErrorMessage")));
			this.txtWidth2.ErrorInfo.ValueOnError = ((object)(resources.GetObject("txtWidth2.ErrorInfo.ValueOnError")));
			this.txtWidth2.Font = ((System.Drawing.Font)(resources.GetObject("txtWidth2.Font")));
			this.txtWidth2.FormatType = ((C1.Win.C1Input.FormatTypeEnum)(resources.GetObject("txtWidth2.FormatType")));
			this.txtWidth2.GapHeight = ((int)(resources.GetObject("txtWidth2.GapHeight")));
			this.txtWidth2.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("txtWidth2.ImeMode")));
			this.txtWidth2.Increment = ((object)(resources.GetObject("txtWidth2.Increment")));
			this.txtWidth2.InitialSelection = ((C1.Win.C1Input.InitialSelectionEnum)(resources.GetObject("txtWidth2.InitialSelection")));
			this.txtWidth2.Location = ((System.Drawing.Point)(resources.GetObject("txtWidth2.Location")));
			this.txtWidth2.MaskInfo.AutoTabWhenFilled = ((bool)(resources.GetObject("txtWidth2.MaskInfo.AutoTabWhenFilled")));
			this.txtWidth2.MaskInfo.CaseSensitive = ((bool)(resources.GetObject("txtWidth2.MaskInfo.CaseSensitive")));
			this.txtWidth2.MaskInfo.CopyWithLiterals = ((bool)(resources.GetObject("txtWidth2.MaskInfo.CopyWithLiterals")));
			this.txtWidth2.MaskInfo.EditMask = resources.GetString("txtWidth2.MaskInfo.EditMask");
			this.txtWidth2.MaskInfo.EmptyAsNull = ((bool)(resources.GetObject("txtWidth2.MaskInfo.EmptyAsNull")));
			this.txtWidth2.MaskInfo.ErrorMessage = resources.GetString("txtWidth2.MaskInfo.ErrorMessage");
			this.txtWidth2.MaskInfo.Inherit = ((C1.Win.C1Input.MaskInfoInheritFlags)(resources.GetObject("txtWidth2.MaskInfo.Inherit")));
			this.txtWidth2.MaskInfo.PromptChar = ((char)(resources.GetObject("txtWidth2.MaskInfo.PromptChar")));
			this.txtWidth2.MaskInfo.ShowLiterals = ((C1.Win.C1Input.ShowLiteralsEnum)(resources.GetObject("txtWidth2.MaskInfo.ShowLiterals")));
			this.txtWidth2.MaskInfo.StoredEmptyChar = ((char)(resources.GetObject("txtWidth2.MaskInfo.StoredEmptyChar")));
			this.txtWidth2.MaxLength = ((int)(resources.GetObject("txtWidth2.MaxLength")));
			this.txtWidth2.Name = "txtWidth2";
			this.txtWidth2.NullText = resources.GetString("txtWidth2.NullText");
			this.txtWidth2.ParseInfo.CaseSensitive = ((bool)(resources.GetObject("txtWidth2.ParseInfo.CaseSensitive")));
			this.txtWidth2.ParseInfo.CustomFormat = resources.GetString("txtWidth2.ParseInfo.CustomFormat");
			this.txtWidth2.ParseInfo.EmptyAsNull = ((bool)(resources.GetObject("txtWidth2.ParseInfo.EmptyAsNull")));
			this.txtWidth2.ParseInfo.ErrorMessage = resources.GetString("txtWidth2.ParseInfo.ErrorMessage");
			this.txtWidth2.ParseInfo.FormatType = ((C1.Win.C1Input.FormatTypeEnum)(resources.GetObject("txtWidth2.ParseInfo.FormatType")));
			this.txtWidth2.ParseInfo.Inherit = ((C1.Win.C1Input.ParseInfoInheritFlags)(resources.GetObject("txtWidth2.ParseInfo.Inherit")));
			this.txtWidth2.ParseInfo.NullText = resources.GetString("txtWidth2.ParseInfo.NullText");
			this.txtWidth2.ParseInfo.NumberStyle = ((C1.Win.C1Input.NumberStyleFlags)(resources.GetObject("txtWidth2.ParseInfo.NumberStyle")));
			this.txtWidth2.ParseInfo.TrimEnd = ((bool)(resources.GetObject("txtWidth2.ParseInfo.TrimEnd")));
			this.txtWidth2.ParseInfo.TrimStart = ((bool)(resources.GetObject("txtWidth2.ParseInfo.TrimStart")));
			this.txtWidth2.PasswordChar = ((char)(resources.GetObject("txtWidth2.PasswordChar")));
			this.txtWidth2.PostValidation.CaseSensitive = ((bool)(resources.GetObject("txtWidth2.PostValidation.CaseSensitive")));
			this.txtWidth2.PostValidation.ErrorMessage = resources.GetString("txtWidth2.PostValidation.ErrorMessage");
			this.txtWidth2.PostValidation.Inherit = ((C1.Win.C1Input.PostValidationInheritFlags)(resources.GetObject("txtWidth2.PostValidation.Inherit")));
			this.txtWidth2.PostValidation.Intervals.AddRange(new C1.Win.C1Input.ValueInterval[] {
																									((C1.Win.C1Input.ValueInterval)(resources.GetObject("txtWidth2.PostValidation.Intervals")))});
			this.txtWidth2.PostValidation.Validation = ((C1.Win.C1Input.PostValidationTypeEnum)(resources.GetObject("txtWidth2.PostValidation.Validation")));
			this.txtWidth2.PostValidation.Values = ((System.Array)(resources.GetObject("txtWidth2.PostValidation.Values")));
			this.txtWidth2.PostValidation.ValuesExcluded = ((System.Array)(resources.GetObject("txtWidth2.PostValidation.ValuesExcluded")));
			this.txtWidth2.PreValidation.CaseSensitive = ((bool)(resources.GetObject("txtWidth2.PreValidation.CaseSensitive")));
			this.txtWidth2.PreValidation.ErrorMessage = resources.GetString("txtWidth2.PreValidation.ErrorMessage");
			this.txtWidth2.PreValidation.Inherit = ((C1.Win.C1Input.PreValidationInheritFlags)(resources.GetObject("txtWidth2.PreValidation.Inherit")));
			this.txtWidth2.PreValidation.ItemSeparator = resources.GetString("txtWidth2.PreValidation.ItemSeparator");
			this.txtWidth2.PreValidation.PatternString = resources.GetString("txtWidth2.PreValidation.PatternString");
			this.txtWidth2.PreValidation.RegexOptions = ((C1.Win.C1Input.RegexOptionFlags)(resources.GetObject("txtWidth2.PreValidation.RegexOptions")));
			this.txtWidth2.PreValidation.TrimEnd = ((bool)(resources.GetObject("txtWidth2.PreValidation.TrimEnd")));
			this.txtWidth2.PreValidation.TrimStart = ((bool)(resources.GetObject("txtWidth2.PreValidation.TrimStart")));
			this.txtWidth2.PreValidation.Validation = ((C1.Win.C1Input.PreValidationTypeEnum)(resources.GetObject("txtWidth2.PreValidation.Validation")));
			this.txtWidth2.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("txtWidth2.RightToLeft")));
			this.txtWidth2.ShowFocusRectangle = ((bool)(resources.GetObject("txtWidth2.ShowFocusRectangle")));
			this.txtWidth2.Size = ((System.Drawing.Size)(resources.GetObject("txtWidth2.Size")));
			this.txtWidth2.TabIndex = ((int)(resources.GetObject("txtWidth2.TabIndex")));
			this.txtWidth2.Tag = ((object)(resources.GetObject("txtWidth2.Tag")));
			this.txtWidth2.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("txtWidth2.TextAlign")));
			this.txtWidth2.TrimEnd = ((bool)(resources.GetObject("txtWidth2.TrimEnd")));
			this.txtWidth2.TrimStart = ((bool)(resources.GetObject("txtWidth2.TrimStart")));
			this.txtWidth2.UserCultureOverride = ((bool)(resources.GetObject("txtWidth2.UserCultureOverride")));
			this.txtWidth2.Value = ((object)(resources.GetObject("txtWidth2.Value")));
			this.txtWidth2.VerticalAlign = ((C1.Win.C1Input.VerticalAlignEnum)(resources.GetObject("txtWidth2.VerticalAlign")));
			this.txtWidth2.Visible = ((bool)(resources.GetObject("txtWidth2.Visible")));
			this.txtWidth2.VisibleButtons = ((C1.Win.C1Input.DropDownControlButtonFlags)(resources.GetObject("txtWidth2.VisibleButtons")));
			// 
			// txtWidth
			// 
			this.txtWidth.AcceptsEscape = ((bool)(resources.GetObject("txtWidth.AcceptsEscape")));
			this.txtWidth.AccessibleDescription = resources.GetString("txtWidth.AccessibleDescription");
			this.txtWidth.AccessibleName = resources.GetString("txtWidth.AccessibleName");
			this.txtWidth.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("txtWidth.Anchor")));
			this.txtWidth.AutoSize = ((bool)(resources.GetObject("txtWidth.AutoSize")));
			this.txtWidth.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("txtWidth.BackgroundImage")));
			this.txtWidth.BorderStyle = ((System.Windows.Forms.BorderStyle)(resources.GetObject("txtWidth.BorderStyle")));
			// 
			// txtWidth.Calculator
			// 
			this.txtWidth.Calculator.AccessibleDescription = resources.GetString("txtWidth.Calculator.AccessibleDescription");
			this.txtWidth.Calculator.AccessibleName = resources.GetString("txtWidth.Calculator.AccessibleName");
			this.txtWidth.Calculator.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("txtWidth.Calculator.BackgroundImage")));
			this.txtWidth.Calculator.ButtonFlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("txtWidth.Calculator.ButtonFlatStyle")));
			this.txtWidth.Calculator.DisplayFormat = resources.GetString("txtWidth.Calculator.DisplayFormat");
			this.txtWidth.Calculator.Font = ((System.Drawing.Font)(resources.GetObject("txtWidth.Calculator.Font")));
			this.txtWidth.Calculator.FormatOnClose = ((bool)(resources.GetObject("txtWidth.Calculator.FormatOnClose")));
			this.txtWidth.Calculator.StoredFormat = resources.GetString("txtWidth.Calculator.StoredFormat");
			this.txtWidth.Calculator.UIStrings.Content = ((string[])(resources.GetObject("txtWidth.Calculator.UIStrings.Content")));
			this.txtWidth.CaseSensitive = ((bool)(resources.GetObject("txtWidth.CaseSensitive")));
			this.txtWidth.Culture = ((int)(resources.GetObject("txtWidth.Culture")));
			this.txtWidth.CustomFormat = resources.GetString("txtWidth.CustomFormat");
			this.txtWidth.DataType = ((System.Type)(resources.GetObject("txtWidth.DataType")));
			this.txtWidth.DisplayFormat.CustomFormat = resources.GetString("txtWidth.DisplayFormat.CustomFormat");
			this.txtWidth.DisplayFormat.FormatType = ((C1.Win.C1Input.FormatTypeEnum)(resources.GetObject("txtWidth.DisplayFormat.FormatType")));
			this.txtWidth.DisplayFormat.Inherit = ((C1.Win.C1Input.FormatInfoInheritFlags)(resources.GetObject("txtWidth.DisplayFormat.Inherit")));
			this.txtWidth.DisplayFormat.NullText = resources.GetString("txtWidth.DisplayFormat.NullText");
			this.txtWidth.DisplayFormat.TrimEnd = ((bool)(resources.GetObject("txtWidth.DisplayFormat.TrimEnd")));
			this.txtWidth.DisplayFormat.TrimStart = ((bool)(resources.GetObject("txtWidth.DisplayFormat.TrimStart")));
			this.txtWidth.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("txtWidth.Dock")));
			this.txtWidth.DropDownFormAlign = ((C1.Win.C1Input.DropDownFormAlignmentEnum)(resources.GetObject("txtWidth.DropDownFormAlign")));
			this.txtWidth.EditFormat.CustomFormat = resources.GetString("txtWidth.EditFormat.CustomFormat");
			this.txtWidth.EditFormat.FormatType = ((C1.Win.C1Input.FormatTypeEnum)(resources.GetObject("txtWidth.EditFormat.FormatType")));
			this.txtWidth.EditFormat.Inherit = ((C1.Win.C1Input.FormatInfoInheritFlags)(resources.GetObject("txtWidth.EditFormat.Inherit")));
			this.txtWidth.EditFormat.NullText = resources.GetString("txtWidth.EditFormat.NullText");
			this.txtWidth.EditFormat.TrimEnd = ((bool)(resources.GetObject("txtWidth.EditFormat.TrimEnd")));
			this.txtWidth.EditFormat.TrimStart = ((bool)(resources.GetObject("txtWidth.EditFormat.TrimStart")));
			this.txtWidth.EditMask = resources.GetString("txtWidth.EditMask");
			this.txtWidth.EmptyAsNull = ((bool)(resources.GetObject("txtWidth.EmptyAsNull")));
			this.txtWidth.Enabled = ((bool)(resources.GetObject("txtWidth.Enabled")));
			this.txtWidth.ErrorInfo.BeepOnError = ((bool)(resources.GetObject("txtWidth.ErrorInfo.BeepOnError")));
			this.txtWidth.ErrorInfo.ErrorMessage = resources.GetString("txtWidth.ErrorInfo.ErrorMessage");
			this.txtWidth.ErrorInfo.ErrorMessageCaption = resources.GetString("txtWidth.ErrorInfo.ErrorMessageCaption");
			this.txtWidth.ErrorInfo.ShowErrorMessage = ((bool)(resources.GetObject("txtWidth.ErrorInfo.ShowErrorMessage")));
			this.txtWidth.ErrorInfo.ValueOnError = ((object)(resources.GetObject("txtWidth.ErrorInfo.ValueOnError")));
			this.txtWidth.Font = ((System.Drawing.Font)(resources.GetObject("txtWidth.Font")));
			this.txtWidth.FormatType = ((C1.Win.C1Input.FormatTypeEnum)(resources.GetObject("txtWidth.FormatType")));
			this.txtWidth.GapHeight = ((int)(resources.GetObject("txtWidth.GapHeight")));
			this.txtWidth.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("txtWidth.ImeMode")));
			this.txtWidth.Increment = ((object)(resources.GetObject("txtWidth.Increment")));
			this.txtWidth.InitialSelection = ((C1.Win.C1Input.InitialSelectionEnum)(resources.GetObject("txtWidth.InitialSelection")));
			this.txtWidth.Location = ((System.Drawing.Point)(resources.GetObject("txtWidth.Location")));
			this.txtWidth.MaskInfo.AutoTabWhenFilled = ((bool)(resources.GetObject("txtWidth.MaskInfo.AutoTabWhenFilled")));
			this.txtWidth.MaskInfo.CaseSensitive = ((bool)(resources.GetObject("txtWidth.MaskInfo.CaseSensitive")));
			this.txtWidth.MaskInfo.CopyWithLiterals = ((bool)(resources.GetObject("txtWidth.MaskInfo.CopyWithLiterals")));
			this.txtWidth.MaskInfo.EditMask = resources.GetString("txtWidth.MaskInfo.EditMask");
			this.txtWidth.MaskInfo.EmptyAsNull = ((bool)(resources.GetObject("txtWidth.MaskInfo.EmptyAsNull")));
			this.txtWidth.MaskInfo.ErrorMessage = resources.GetString("txtWidth.MaskInfo.ErrorMessage");
			this.txtWidth.MaskInfo.Inherit = ((C1.Win.C1Input.MaskInfoInheritFlags)(resources.GetObject("txtWidth.MaskInfo.Inherit")));
			this.txtWidth.MaskInfo.PromptChar = ((char)(resources.GetObject("txtWidth.MaskInfo.PromptChar")));
			this.txtWidth.MaskInfo.ShowLiterals = ((C1.Win.C1Input.ShowLiteralsEnum)(resources.GetObject("txtWidth.MaskInfo.ShowLiterals")));
			this.txtWidth.MaskInfo.StoredEmptyChar = ((char)(resources.GetObject("txtWidth.MaskInfo.StoredEmptyChar")));
			this.txtWidth.MaxLength = ((int)(resources.GetObject("txtWidth.MaxLength")));
			this.txtWidth.Name = "txtWidth";
			this.txtWidth.NullText = resources.GetString("txtWidth.NullText");
			this.txtWidth.ParseInfo.CaseSensitive = ((bool)(resources.GetObject("txtWidth.ParseInfo.CaseSensitive")));
			this.txtWidth.ParseInfo.CustomFormat = resources.GetString("txtWidth.ParseInfo.CustomFormat");
			this.txtWidth.ParseInfo.EmptyAsNull = ((bool)(resources.GetObject("txtWidth.ParseInfo.EmptyAsNull")));
			this.txtWidth.ParseInfo.ErrorMessage = resources.GetString("txtWidth.ParseInfo.ErrorMessage");
			this.txtWidth.ParseInfo.FormatType = ((C1.Win.C1Input.FormatTypeEnum)(resources.GetObject("txtWidth.ParseInfo.FormatType")));
			this.txtWidth.ParseInfo.Inherit = ((C1.Win.C1Input.ParseInfoInheritFlags)(resources.GetObject("txtWidth.ParseInfo.Inherit")));
			this.txtWidth.ParseInfo.NullText = resources.GetString("txtWidth.ParseInfo.NullText");
			this.txtWidth.ParseInfo.NumberStyle = ((C1.Win.C1Input.NumberStyleFlags)(resources.GetObject("txtWidth.ParseInfo.NumberStyle")));
			this.txtWidth.ParseInfo.TrimEnd = ((bool)(resources.GetObject("txtWidth.ParseInfo.TrimEnd")));
			this.txtWidth.ParseInfo.TrimStart = ((bool)(resources.GetObject("txtWidth.ParseInfo.TrimStart")));
			this.txtWidth.PasswordChar = ((char)(resources.GetObject("txtWidth.PasswordChar")));
			this.txtWidth.PostValidation.CaseSensitive = ((bool)(resources.GetObject("txtWidth.PostValidation.CaseSensitive")));
			this.txtWidth.PostValidation.ErrorMessage = resources.GetString("txtWidth.PostValidation.ErrorMessage");
			this.txtWidth.PostValidation.Inherit = ((C1.Win.C1Input.PostValidationInheritFlags)(resources.GetObject("txtWidth.PostValidation.Inherit")));
			this.txtWidth.PostValidation.Intervals.AddRange(new C1.Win.C1Input.ValueInterval[] {
																								   ((C1.Win.C1Input.ValueInterval)(resources.GetObject("txtWidth.PostValidation.Intervals")))});
			this.txtWidth.PostValidation.Validation = ((C1.Win.C1Input.PostValidationTypeEnum)(resources.GetObject("txtWidth.PostValidation.Validation")));
			this.txtWidth.PostValidation.Values = ((System.Array)(resources.GetObject("txtWidth.PostValidation.Values")));
			this.txtWidth.PostValidation.ValuesExcluded = ((System.Array)(resources.GetObject("txtWidth.PostValidation.ValuesExcluded")));
			this.txtWidth.PreValidation.CaseSensitive = ((bool)(resources.GetObject("txtWidth.PreValidation.CaseSensitive")));
			this.txtWidth.PreValidation.ErrorMessage = resources.GetString("txtWidth.PreValidation.ErrorMessage");
			this.txtWidth.PreValidation.Inherit = ((C1.Win.C1Input.PreValidationInheritFlags)(resources.GetObject("txtWidth.PreValidation.Inherit")));
			this.txtWidth.PreValidation.ItemSeparator = resources.GetString("txtWidth.PreValidation.ItemSeparator");
			this.txtWidth.PreValidation.PatternString = resources.GetString("txtWidth.PreValidation.PatternString");
			this.txtWidth.PreValidation.RegexOptions = ((C1.Win.C1Input.RegexOptionFlags)(resources.GetObject("txtWidth.PreValidation.RegexOptions")));
			this.txtWidth.PreValidation.TrimEnd = ((bool)(resources.GetObject("txtWidth.PreValidation.TrimEnd")));
			this.txtWidth.PreValidation.TrimStart = ((bool)(resources.GetObject("txtWidth.PreValidation.TrimStart")));
			this.txtWidth.PreValidation.Validation = ((C1.Win.C1Input.PreValidationTypeEnum)(resources.GetObject("txtWidth.PreValidation.Validation")));
			this.txtWidth.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("txtWidth.RightToLeft")));
			this.txtWidth.ShowFocusRectangle = ((bool)(resources.GetObject("txtWidth.ShowFocusRectangle")));
			this.txtWidth.Size = ((System.Drawing.Size)(resources.GetObject("txtWidth.Size")));
			this.txtWidth.TabIndex = ((int)(resources.GetObject("txtWidth.TabIndex")));
			this.txtWidth.Tag = ((object)(resources.GetObject("txtWidth.Tag")));
			this.txtWidth.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("txtWidth.TextAlign")));
			this.txtWidth.TrimEnd = ((bool)(resources.GetObject("txtWidth.TrimEnd")));
			this.txtWidth.TrimStart = ((bool)(resources.GetObject("txtWidth.TrimStart")));
			this.txtWidth.UserCultureOverride = ((bool)(resources.GetObject("txtWidth.UserCultureOverride")));
			this.txtWidth.Value = ((object)(resources.GetObject("txtWidth.Value")));
			this.txtWidth.VerticalAlign = ((C1.Win.C1Input.VerticalAlignEnum)(resources.GetObject("txtWidth.VerticalAlign")));
			this.txtWidth.Visible = ((bool)(resources.GetObject("txtWidth.Visible")));
			this.txtWidth.VisibleButtons = ((C1.Win.C1Input.DropDownControlButtonFlags)(resources.GetObject("txtWidth.VisibleButtons")));
			// 
			// lblWidth2
			// 
			this.lblWidth2.AccessibleDescription = resources.GetString("lblWidth2.AccessibleDescription");
			this.lblWidth2.AccessibleName = resources.GetString("lblWidth2.AccessibleName");
			this.lblWidth2.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblWidth2.Anchor")));
			this.lblWidth2.AutoSize = ((bool)(resources.GetObject("lblWidth2.AutoSize")));
			this.lblWidth2.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblWidth2.Dock")));
			this.lblWidth2.Enabled = ((bool)(resources.GetObject("lblWidth2.Enabled")));
			this.lblWidth2.Font = ((System.Drawing.Font)(resources.GetObject("lblWidth2.Font")));
			this.lblWidth2.Image = ((System.Drawing.Image)(resources.GetObject("lblWidth2.Image")));
			this.lblWidth2.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblWidth2.ImageAlign")));
			this.lblWidth2.ImageIndex = ((int)(resources.GetObject("lblWidth2.ImageIndex")));
			this.lblWidth2.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblWidth2.ImeMode")));
			this.lblWidth2.Location = ((System.Drawing.Point)(resources.GetObject("lblWidth2.Location")));
			this.lblWidth2.Name = "lblWidth2";
			this.lblWidth2.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblWidth2.RightToLeft")));
			this.lblWidth2.Size = ((System.Drawing.Size)(resources.GetObject("lblWidth2.Size")));
			this.lblWidth2.TabIndex = ((int)(resources.GetObject("lblWidth2.TabIndex")));
			this.lblWidth2.Text = resources.GetString("lblWidth2.Text");
			this.lblWidth2.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblWidth2.TextAlign")));
			this.lblWidth2.Visible = ((bool)(resources.GetObject("lblWidth2.Visible")));
			// 
			// lblWidth1
			// 
			this.lblWidth1.AccessibleDescription = resources.GetString("lblWidth1.AccessibleDescription");
			this.lblWidth1.AccessibleName = resources.GetString("lblWidth1.AccessibleName");
			this.lblWidth1.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblWidth1.Anchor")));
			this.lblWidth1.AutoSize = ((bool)(resources.GetObject("lblWidth1.AutoSize")));
			this.lblWidth1.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblWidth1.Dock")));
			this.lblWidth1.Enabled = ((bool)(resources.GetObject("lblWidth1.Enabled")));
			this.lblWidth1.Font = ((System.Drawing.Font)(resources.GetObject("lblWidth1.Font")));
			this.lblWidth1.Image = ((System.Drawing.Image)(resources.GetObject("lblWidth1.Image")));
			this.lblWidth1.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblWidth1.ImageAlign")));
			this.lblWidth1.ImageIndex = ((int)(resources.GetObject("lblWidth1.ImageIndex")));
			this.lblWidth1.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblWidth1.ImeMode")));
			this.lblWidth1.Location = ((System.Drawing.Point)(resources.GetObject("lblWidth1.Location")));
			this.lblWidth1.Name = "lblWidth1";
			this.lblWidth1.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblWidth1.RightToLeft")));
			this.lblWidth1.Size = ((System.Drawing.Size)(resources.GetObject("lblWidth1.Size")));
			this.lblWidth1.TabIndex = ((int)(resources.GetObject("lblWidth1.TabIndex")));
			this.lblWidth1.Text = resources.GetString("lblWidth1.Text");
			this.lblWidth1.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblWidth1.TextAlign")));
			this.lblWidth1.Visible = ((bool)(resources.GetObject("lblWidth1.Visible")));
			// 
			// lblDataType
			// 
			this.lblDataType.AccessibleDescription = resources.GetString("lblDataType.AccessibleDescription");
			this.lblDataType.AccessibleName = resources.GetString("lblDataType.AccessibleName");
			this.lblDataType.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblDataType.Anchor")));
			this.lblDataType.AutoSize = ((bool)(resources.GetObject("lblDataType.AutoSize")));
			this.lblDataType.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblDataType.Dock")));
			this.lblDataType.Enabled = ((bool)(resources.GetObject("lblDataType.Enabled")));
			this.lblDataType.Font = ((System.Drawing.Font)(resources.GetObject("lblDataType.Font")));
			this.lblDataType.ForeColor = System.Drawing.Color.Maroon;
			this.lblDataType.Image = ((System.Drawing.Image)(resources.GetObject("lblDataType.Image")));
			this.lblDataType.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblDataType.ImageAlign")));
			this.lblDataType.ImageIndex = ((int)(resources.GetObject("lblDataType.ImageIndex")));
			this.lblDataType.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblDataType.ImeMode")));
			this.lblDataType.Location = ((System.Drawing.Point)(resources.GetObject("lblDataType.Location")));
			this.lblDataType.Name = "lblDataType";
			this.lblDataType.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblDataType.RightToLeft")));
			this.lblDataType.Size = ((System.Drawing.Size)(resources.GetObject("lblDataType.Size")));
			this.lblDataType.TabIndex = ((int)(resources.GetObject("lblDataType.TabIndex")));
			this.lblDataType.Text = resources.GetString("lblDataType.Text");
			this.lblDataType.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblDataType.TextAlign")));
			this.lblDataType.Visible = ((bool)(resources.GetObject("lblDataType.Visible")));
			// 
			// lblFromTable
			// 
			this.lblFromTable.AccessibleDescription = resources.GetString("lblFromTable.AccessibleDescription");
			this.lblFromTable.AccessibleName = resources.GetString("lblFromTable.AccessibleName");
			this.lblFromTable.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblFromTable.Anchor")));
			this.lblFromTable.AutoSize = ((bool)(resources.GetObject("lblFromTable.AutoSize")));
			this.lblFromTable.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblFromTable.Dock")));
			this.lblFromTable.Enabled = ((bool)(resources.GetObject("lblFromTable.Enabled")));
			this.lblFromTable.Font = ((System.Drawing.Font)(resources.GetObject("lblFromTable.Font")));
			this.lblFromTable.Image = ((System.Drawing.Image)(resources.GetObject("lblFromTable.Image")));
			this.lblFromTable.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblFromTable.ImageAlign")));
			this.lblFromTable.ImageIndex = ((int)(resources.GetObject("lblFromTable.ImageIndex")));
			this.lblFromTable.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblFromTable.ImeMode")));
			this.lblFromTable.Location = ((System.Drawing.Point)(resources.GetObject("lblFromTable.Location")));
			this.lblFromTable.Name = "lblFromTable";
			this.lblFromTable.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblFromTable.RightToLeft")));
			this.lblFromTable.Size = ((System.Drawing.Size)(resources.GetObject("lblFromTable.Size")));
			this.lblFromTable.TabIndex = ((int)(resources.GetObject("lblFromTable.TabIndex")));
			this.lblFromTable.Text = resources.GetString("lblFromTable.Text");
			this.lblFromTable.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblFromTable.TextAlign")));
			this.lblFromTable.Visible = ((bool)(resources.GetObject("lblFromTable.Visible")));
			// 
			// cboFromTable
			// 
			this.cboFromTable.AccessibleDescription = resources.GetString("cboFromTable.AccessibleDescription");
			this.cboFromTable.AccessibleName = resources.GetString("cboFromTable.AccessibleName");
			this.cboFromTable.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("cboFromTable.Anchor")));
			this.cboFromTable.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cboFromTable.BackgroundImage")));
			this.cboFromTable.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("cboFromTable.Dock")));
			this.cboFromTable.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboFromTable.Enabled = ((bool)(resources.GetObject("cboFromTable.Enabled")));
			this.cboFromTable.Font = ((System.Drawing.Font)(resources.GetObject("cboFromTable.Font")));
			this.cboFromTable.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("cboFromTable.ImeMode")));
			this.cboFromTable.IntegralHeight = ((bool)(resources.GetObject("cboFromTable.IntegralHeight")));
			this.cboFromTable.ItemHeight = ((int)(resources.GetObject("cboFromTable.ItemHeight")));
			this.cboFromTable.Location = ((System.Drawing.Point)(resources.GetObject("cboFromTable.Location")));
			this.cboFromTable.MaxDropDownItems = ((int)(resources.GetObject("cboFromTable.MaxDropDownItems")));
			this.cboFromTable.MaxLength = ((int)(resources.GetObject("cboFromTable.MaxLength")));
			this.cboFromTable.Name = "cboFromTable";
			this.cboFromTable.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("cboFromTable.RightToLeft")));
			this.cboFromTable.Size = ((System.Drawing.Size)(resources.GetObject("cboFromTable.Size")));
			this.cboFromTable.TabIndex = ((int)(resources.GetObject("cboFromTable.TabIndex")));
			this.cboFromTable.Text = resources.GetString("cboFromTable.Text");
			this.cboFromTable.Visible = ((bool)(resources.GetObject("cboFromTable.Visible")));
			this.cboFromTable.SelectedIndexChanged += new System.EventHandler(this.cboFromTable_SelectedIndexChanged);
			// 
			// lblName
			// 
			this.lblName.AccessibleDescription = resources.GetString("lblName.AccessibleDescription");
			this.lblName.AccessibleName = resources.GetString("lblName.AccessibleName");
			this.lblName.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblName.Anchor")));
			this.lblName.AutoSize = ((bool)(resources.GetObject("lblName.AutoSize")));
			this.lblName.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblName.Dock")));
			this.lblName.Enabled = ((bool)(resources.GetObject("lblName.Enabled")));
			this.lblName.Font = ((System.Drawing.Font)(resources.GetObject("lblName.Font")));
			this.lblName.ForeColor = System.Drawing.Color.Maroon;
			this.lblName.Image = ((System.Drawing.Image)(resources.GetObject("lblName.Image")));
			this.lblName.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblName.ImageAlign")));
			this.lblName.ImageIndex = ((int)(resources.GetObject("lblName.ImageIndex")));
			this.lblName.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblName.ImeMode")));
			this.lblName.Location = ((System.Drawing.Point)(resources.GetObject("lblName.Location")));
			this.lblName.Name = "lblName";
			this.lblName.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblName.RightToLeft")));
			this.lblName.Size = ((System.Drawing.Size)(resources.GetObject("lblName.Size")));
			this.lblName.TabIndex = ((int)(resources.GetObject("lblName.TabIndex")));
			this.lblName.Text = resources.GetString("lblName.Text");
			this.lblName.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblName.TextAlign")));
			this.lblName.Visible = ((bool)(resources.GetObject("lblName.Visible")));
			// 
			// lblFromField
			// 
			this.lblFromField.AccessibleDescription = resources.GetString("lblFromField.AccessibleDescription");
			this.lblFromField.AccessibleName = resources.GetString("lblFromField.AccessibleName");
			this.lblFromField.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblFromField.Anchor")));
			this.lblFromField.AutoSize = ((bool)(resources.GetObject("lblFromField.AutoSize")));
			this.lblFromField.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblFromField.Dock")));
			this.lblFromField.Enabled = ((bool)(resources.GetObject("lblFromField.Enabled")));
			this.lblFromField.Font = ((System.Drawing.Font)(resources.GetObject("lblFromField.Font")));
			this.lblFromField.Image = ((System.Drawing.Image)(resources.GetObject("lblFromField.Image")));
			this.lblFromField.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblFromField.ImageAlign")));
			this.lblFromField.ImageIndex = ((int)(resources.GetObject("lblFromField.ImageIndex")));
			this.lblFromField.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblFromField.ImeMode")));
			this.lblFromField.Location = ((System.Drawing.Point)(resources.GetObject("lblFromField.Location")));
			this.lblFromField.Name = "lblFromField";
			this.lblFromField.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblFromField.RightToLeft")));
			this.lblFromField.Size = ((System.Drawing.Size)(resources.GetObject("lblFromField.Size")));
			this.lblFromField.TabIndex = ((int)(resources.GetObject("lblFromField.TabIndex")));
			this.lblFromField.Text = resources.GetString("lblFromField.Text");
			this.lblFromField.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblFromField.TextAlign")));
			this.lblFromField.Visible = ((bool)(resources.GetObject("lblFromField.Visible")));
			// 
			// cboFromField
			// 
			this.cboFromField.AccessibleDescription = resources.GetString("cboFromField.AccessibleDescription");
			this.cboFromField.AccessibleName = resources.GetString("cboFromField.AccessibleName");
			this.cboFromField.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("cboFromField.Anchor")));
			this.cboFromField.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cboFromField.BackgroundImage")));
			this.cboFromField.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("cboFromField.Dock")));
			this.cboFromField.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboFromField.Enabled = ((bool)(resources.GetObject("cboFromField.Enabled")));
			this.cboFromField.Font = ((System.Drawing.Font)(resources.GetObject("cboFromField.Font")));
			this.cboFromField.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("cboFromField.ImeMode")));
			this.cboFromField.IntegralHeight = ((bool)(resources.GetObject("cboFromField.IntegralHeight")));
			this.cboFromField.ItemHeight = ((int)(resources.GetObject("cboFromField.ItemHeight")));
			this.cboFromField.Location = ((System.Drawing.Point)(resources.GetObject("cboFromField.Location")));
			this.cboFromField.MaxDropDownItems = ((int)(resources.GetObject("cboFromField.MaxDropDownItems")));
			this.cboFromField.MaxLength = ((int)(resources.GetObject("cboFromField.MaxLength")));
			this.cboFromField.Name = "cboFromField";
			this.cboFromField.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("cboFromField.RightToLeft")));
			this.cboFromField.Size = ((System.Drawing.Size)(resources.GetObject("cboFromField.Size")));
			this.cboFromField.TabIndex = ((int)(resources.GetObject("cboFromField.TabIndex")));
			this.cboFromField.Text = resources.GetString("cboFromField.Text");
			this.cboFromField.Visible = ((bool)(resources.GetObject("cboFromField.Visible")));
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
			// lblDefaultValue
			// 
			this.lblDefaultValue.AccessibleDescription = resources.GetString("lblDefaultValue.AccessibleDescription");
			this.lblDefaultValue.AccessibleName = resources.GetString("lblDefaultValue.AccessibleName");
			this.lblDefaultValue.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblDefaultValue.Anchor")));
			this.lblDefaultValue.AutoSize = ((bool)(resources.GetObject("lblDefaultValue.AutoSize")));
			this.lblDefaultValue.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblDefaultValue.Dock")));
			this.lblDefaultValue.Enabled = ((bool)(resources.GetObject("lblDefaultValue.Enabled")));
			this.lblDefaultValue.Font = ((System.Drawing.Font)(resources.GetObject("lblDefaultValue.Font")));
			this.lblDefaultValue.Image = ((System.Drawing.Image)(resources.GetObject("lblDefaultValue.Image")));
			this.lblDefaultValue.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblDefaultValue.ImageAlign")));
			this.lblDefaultValue.ImageIndex = ((int)(resources.GetObject("lblDefaultValue.ImageIndex")));
			this.lblDefaultValue.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblDefaultValue.ImeMode")));
			this.lblDefaultValue.Location = ((System.Drawing.Point)(resources.GetObject("lblDefaultValue.Location")));
			this.lblDefaultValue.Name = "lblDefaultValue";
			this.lblDefaultValue.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblDefaultValue.RightToLeft")));
			this.lblDefaultValue.Size = ((System.Drawing.Size)(resources.GetObject("lblDefaultValue.Size")));
			this.lblDefaultValue.TabIndex = ((int)(resources.GetObject("lblDefaultValue.TabIndex")));
			this.lblDefaultValue.Text = resources.GetString("lblDefaultValue.Text");
			this.lblDefaultValue.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblDefaultValue.TextAlign")));
			this.lblDefaultValue.Visible = ((bool)(resources.GetObject("lblDefaultValue.Visible")));
			// 
			// lblWhereClause
			// 
			this.lblWhereClause.AccessibleDescription = resources.GetString("lblWhereClause.AccessibleDescription");
			this.lblWhereClause.AccessibleName = resources.GetString("lblWhereClause.AccessibleName");
			this.lblWhereClause.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblWhereClause.Anchor")));
			this.lblWhereClause.AutoSize = ((bool)(resources.GetObject("lblWhereClause.AutoSize")));
			this.lblWhereClause.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblWhereClause.Dock")));
			this.lblWhereClause.Enabled = ((bool)(resources.GetObject("lblWhereClause.Enabled")));
			this.lblWhereClause.Font = ((System.Drawing.Font)(resources.GetObject("lblWhereClause.Font")));
			this.lblWhereClause.Image = ((System.Drawing.Image)(resources.GetObject("lblWhereClause.Image")));
			this.lblWhereClause.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblWhereClause.ImageAlign")));
			this.lblWhereClause.ImageIndex = ((int)(resources.GetObject("lblWhereClause.ImageIndex")));
			this.lblWhereClause.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblWhereClause.ImeMode")));
			this.lblWhereClause.Location = ((System.Drawing.Point)(resources.GetObject("lblWhereClause.Location")));
			this.lblWhereClause.Name = "lblWhereClause";
			this.lblWhereClause.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblWhereClause.RightToLeft")));
			this.lblWhereClause.Size = ((System.Drawing.Size)(resources.GetObject("lblWhereClause.Size")));
			this.lblWhereClause.TabIndex = ((int)(resources.GetObject("lblWhereClause.TabIndex")));
			this.lblWhereClause.Text = resources.GetString("lblWhereClause.Text");
			this.lblWhereClause.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblWhereClause.TextAlign")));
			this.lblWhereClause.Visible = ((bool)(resources.GetObject("lblWhereClause.Visible")));
			// 
			// txtWhereClause
			// 
			this.txtWhereClause.AccessibleDescription = resources.GetString("txtWhereClause.AccessibleDescription");
			this.txtWhereClause.AccessibleName = resources.GetString("txtWhereClause.AccessibleName");
			this.txtWhereClause.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("txtWhereClause.Anchor")));
			this.txtWhereClause.AutoSize = ((bool)(resources.GetObject("txtWhereClause.AutoSize")));
			this.txtWhereClause.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("txtWhereClause.BackgroundImage")));
			this.txtWhereClause.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("txtWhereClause.Dock")));
			this.txtWhereClause.Enabled = ((bool)(resources.GetObject("txtWhereClause.Enabled")));
			this.txtWhereClause.Font = ((System.Drawing.Font)(resources.GetObject("txtWhereClause.Font")));
			this.txtWhereClause.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("txtWhereClause.ImeMode")));
			this.txtWhereClause.Location = ((System.Drawing.Point)(resources.GetObject("txtWhereClause.Location")));
			this.txtWhereClause.MaxLength = ((int)(resources.GetObject("txtWhereClause.MaxLength")));
			this.txtWhereClause.Multiline = ((bool)(resources.GetObject("txtWhereClause.Multiline")));
			this.txtWhereClause.Name = "txtWhereClause";
			this.txtWhereClause.PasswordChar = ((char)(resources.GetObject("txtWhereClause.PasswordChar")));
			this.txtWhereClause.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("txtWhereClause.RightToLeft")));
			this.txtWhereClause.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("txtWhereClause.ScrollBars")));
			this.txtWhereClause.Size = ((System.Drawing.Size)(resources.GetObject("txtWhereClause.Size")));
			this.txtWhereClause.TabIndex = ((int)(resources.GetObject("txtWhereClause.TabIndex")));
			this.txtWhereClause.Text = resources.GetString("txtWhereClause.Text");
			this.txtWhereClause.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("txtWhereClause.TextAlign")));
			this.txtWhereClause.Visible = ((bool)(resources.GetObject("txtWhereClause.Visible")));
			this.txtWhereClause.WordWrap = ((bool)(resources.GetObject("txtWhereClause.WordWrap")));
			// 
			// chkOptional
			// 
			this.chkOptional.AccessibleDescription = resources.GetString("chkOptional.AccessibleDescription");
			this.chkOptional.AccessibleName = resources.GetString("chkOptional.AccessibleName");
			this.chkOptional.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("chkOptional.Anchor")));
			this.chkOptional.Appearance = ((System.Windows.Forms.Appearance)(resources.GetObject("chkOptional.Appearance")));
			this.chkOptional.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("chkOptional.BackgroundImage")));
			this.chkOptional.CheckAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("chkOptional.CheckAlign")));
			this.chkOptional.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("chkOptional.Dock")));
			this.chkOptional.Enabled = ((bool)(resources.GetObject("chkOptional.Enabled")));
			this.chkOptional.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("chkOptional.FlatStyle")));
			this.chkOptional.Font = ((System.Drawing.Font)(resources.GetObject("chkOptional.Font")));
			this.chkOptional.Image = ((System.Drawing.Image)(resources.GetObject("chkOptional.Image")));
			this.chkOptional.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("chkOptional.ImageAlign")));
			this.chkOptional.ImageIndex = ((int)(resources.GetObject("chkOptional.ImageIndex")));
			this.chkOptional.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("chkOptional.ImeMode")));
			this.chkOptional.Location = ((System.Drawing.Point)(resources.GetObject("chkOptional.Location")));
			this.chkOptional.Name = "chkOptional";
			this.chkOptional.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("chkOptional.RightToLeft")));
			this.chkOptional.Size = ((System.Drawing.Size)(resources.GetObject("chkOptional.Size")));
			this.chkOptional.TabIndex = ((int)(resources.GetObject("chkOptional.TabIndex")));
			this.chkOptional.Text = resources.GetString("chkOptional.Text");
			this.chkOptional.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("chkOptional.TextAlign")));
			this.chkOptional.Visible = ((bool)(resources.GetObject("chkOptional.Visible")));
			// 
			// chkTagValue
			// 
			this.chkTagValue.AccessibleDescription = resources.GetString("chkTagValue.AccessibleDescription");
			this.chkTagValue.AccessibleName = resources.GetString("chkTagValue.AccessibleName");
			this.chkTagValue.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("chkTagValue.Anchor")));
			this.chkTagValue.Appearance = ((System.Windows.Forms.Appearance)(resources.GetObject("chkTagValue.Appearance")));
			this.chkTagValue.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("chkTagValue.BackgroundImage")));
			this.chkTagValue.CheckAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("chkTagValue.CheckAlign")));
			this.chkTagValue.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("chkTagValue.Dock")));
			this.chkTagValue.Enabled = ((bool)(resources.GetObject("chkTagValue.Enabled")));
			this.chkTagValue.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("chkTagValue.FlatStyle")));
			this.chkTagValue.Font = ((System.Drawing.Font)(resources.GetObject("chkTagValue.Font")));
			this.chkTagValue.Image = ((System.Drawing.Image)(resources.GetObject("chkTagValue.Image")));
			this.chkTagValue.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("chkTagValue.ImageAlign")));
			this.chkTagValue.ImageIndex = ((int)(resources.GetObject("chkTagValue.ImageIndex")));
			this.chkTagValue.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("chkTagValue.ImeMode")));
			this.chkTagValue.Location = ((System.Drawing.Point)(resources.GetObject("chkTagValue.Location")));
			this.chkTagValue.Name = "chkTagValue";
			this.chkTagValue.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("chkTagValue.RightToLeft")));
			this.chkTagValue.Size = ((System.Drawing.Size)(resources.GetObject("chkTagValue.Size")));
			this.chkTagValue.TabIndex = ((int)(resources.GetObject("chkTagValue.TabIndex")));
			this.chkTagValue.Text = resources.GetString("chkTagValue.Text");
			this.chkTagValue.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("chkTagValue.TextAlign")));
			this.chkTagValue.Visible = ((bool)(resources.GetObject("chkTagValue.Visible")));
			// 
			// lblFilterField1
			// 
			this.lblFilterField1.AccessibleDescription = resources.GetString("lblFilterField1.AccessibleDescription");
			this.lblFilterField1.AccessibleName = resources.GetString("lblFilterField1.AccessibleName");
			this.lblFilterField1.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblFilterField1.Anchor")));
			this.lblFilterField1.AutoSize = ((bool)(resources.GetObject("lblFilterField1.AutoSize")));
			this.lblFilterField1.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblFilterField1.Dock")));
			this.lblFilterField1.Enabled = ((bool)(resources.GetObject("lblFilterField1.Enabled")));
			this.lblFilterField1.Font = ((System.Drawing.Font)(resources.GetObject("lblFilterField1.Font")));
			this.lblFilterField1.Image = ((System.Drawing.Image)(resources.GetObject("lblFilterField1.Image")));
			this.lblFilterField1.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblFilterField1.ImageAlign")));
			this.lblFilterField1.ImageIndex = ((int)(resources.GetObject("lblFilterField1.ImageIndex")));
			this.lblFilterField1.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblFilterField1.ImeMode")));
			this.lblFilterField1.Location = ((System.Drawing.Point)(resources.GetObject("lblFilterField1.Location")));
			this.lblFilterField1.Name = "lblFilterField1";
			this.lblFilterField1.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblFilterField1.RightToLeft")));
			this.lblFilterField1.Size = ((System.Drawing.Size)(resources.GetObject("lblFilterField1.Size")));
			this.lblFilterField1.TabIndex = ((int)(resources.GetObject("lblFilterField1.TabIndex")));
			this.lblFilterField1.Text = resources.GetString("lblFilterField1.Text");
			this.lblFilterField1.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblFilterField1.TextAlign")));
			this.lblFilterField1.Visible = ((bool)(resources.GetObject("lblFilterField1.Visible")));
			// 
			// cboFilterField1
			// 
			this.cboFilterField1.AccessibleDescription = resources.GetString("cboFilterField1.AccessibleDescription");
			this.cboFilterField1.AccessibleName = resources.GetString("cboFilterField1.AccessibleName");
			this.cboFilterField1.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("cboFilterField1.Anchor")));
			this.cboFilterField1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cboFilterField1.BackgroundImage")));
			this.cboFilterField1.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("cboFilterField1.Dock")));
			this.cboFilterField1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboFilterField1.Enabled = ((bool)(resources.GetObject("cboFilterField1.Enabled")));
			this.cboFilterField1.Font = ((System.Drawing.Font)(resources.GetObject("cboFilterField1.Font")));
			this.cboFilterField1.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("cboFilterField1.ImeMode")));
			this.cboFilterField1.IntegralHeight = ((bool)(resources.GetObject("cboFilterField1.IntegralHeight")));
			this.cboFilterField1.ItemHeight = ((int)(resources.GetObject("cboFilterField1.ItemHeight")));
			this.cboFilterField1.Location = ((System.Drawing.Point)(resources.GetObject("cboFilterField1.Location")));
			this.cboFilterField1.MaxDropDownItems = ((int)(resources.GetObject("cboFilterField1.MaxDropDownItems")));
			this.cboFilterField1.MaxLength = ((int)(resources.GetObject("cboFilterField1.MaxLength")));
			this.cboFilterField1.Name = "cboFilterField1";
			this.cboFilterField1.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("cboFilterField1.RightToLeft")));
			this.cboFilterField1.Size = ((System.Drawing.Size)(resources.GetObject("cboFilterField1.Size")));
			this.cboFilterField1.TabIndex = ((int)(resources.GetObject("cboFilterField1.TabIndex")));
			this.cboFilterField1.Text = resources.GetString("cboFilterField1.Text");
			this.cboFilterField1.Visible = ((bool)(resources.GetObject("cboFilterField1.Visible")));
			// 
			// txtSQL
			// 
			this.txtSQL.AccessibleDescription = resources.GetString("txtSQL.AccessibleDescription");
			this.txtSQL.AccessibleName = resources.GetString("txtSQL.AccessibleName");
			this.txtSQL.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("txtSQL.Anchor")));
			this.txtSQL.AutoSize = ((bool)(resources.GetObject("txtSQL.AutoSize")));
			this.txtSQL.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("txtSQL.BackgroundImage")));
			this.txtSQL.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("txtSQL.Dock")));
			this.txtSQL.Enabled = ((bool)(resources.GetObject("txtSQL.Enabled")));
			this.txtSQL.Font = ((System.Drawing.Font)(resources.GetObject("txtSQL.Font")));
			this.txtSQL.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("txtSQL.ImeMode")));
			this.txtSQL.Location = ((System.Drawing.Point)(resources.GetObject("txtSQL.Location")));
			this.txtSQL.MaxLength = ((int)(resources.GetObject("txtSQL.MaxLength")));
			this.txtSQL.Multiline = ((bool)(resources.GetObject("txtSQL.Multiline")));
			this.txtSQL.Name = "txtSQL";
			this.txtSQL.PasswordChar = ((char)(resources.GetObject("txtSQL.PasswordChar")));
			this.txtSQL.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("txtSQL.RightToLeft")));
			this.txtSQL.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("txtSQL.ScrollBars")));
			this.txtSQL.Size = ((System.Drawing.Size)(resources.GetObject("txtSQL.Size")));
			this.txtSQL.TabIndex = ((int)(resources.GetObject("txtSQL.TabIndex")));
			this.txtSQL.Text = resources.GetString("txtSQL.Text");
			this.txtSQL.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("txtSQL.TextAlign")));
			this.txtSQL.Visible = ((bool)(resources.GetObject("txtSQL.Visible")));
			this.txtSQL.WordWrap = ((bool)(resources.GetObject("txtSQL.WordWrap")));
			// 
			// lblSQL
			// 
			this.lblSQL.AccessibleDescription = resources.GetString("lblSQL.AccessibleDescription");
			this.lblSQL.AccessibleName = resources.GetString("lblSQL.AccessibleName");
			this.lblSQL.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblSQL.Anchor")));
			this.lblSQL.AutoSize = ((bool)(resources.GetObject("lblSQL.AutoSize")));
			this.lblSQL.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblSQL.Dock")));
			this.lblSQL.Enabled = ((bool)(resources.GetObject("lblSQL.Enabled")));
			this.lblSQL.Font = ((System.Drawing.Font)(resources.GetObject("lblSQL.Font")));
			this.lblSQL.Image = ((System.Drawing.Image)(resources.GetObject("lblSQL.Image")));
			this.lblSQL.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblSQL.ImageAlign")));
			this.lblSQL.ImageIndex = ((int)(resources.GetObject("lblSQL.ImageIndex")));
			this.lblSQL.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblSQL.ImeMode")));
			this.lblSQL.Location = ((System.Drawing.Point)(resources.GetObject("lblSQL.Location")));
			this.lblSQL.Name = "lblSQL";
			this.lblSQL.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblSQL.RightToLeft")));
			this.lblSQL.Size = ((System.Drawing.Size)(resources.GetObject("lblSQL.Size")));
			this.lblSQL.TabIndex = ((int)(resources.GetObject("lblSQL.TabIndex")));
			this.lblSQL.Text = resources.GetString("lblSQL.Text");
			this.lblSQL.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblSQL.TextAlign")));
			this.lblSQL.Visible = ((bool)(resources.GetObject("lblSQL.Visible")));
			// 
			// lblItems
			// 
			this.lblItems.AccessibleDescription = resources.GetString("lblItems.AccessibleDescription");
			this.lblItems.AccessibleName = resources.GetString("lblItems.AccessibleName");
			this.lblItems.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblItems.Anchor")));
			this.lblItems.AutoSize = ((bool)(resources.GetObject("lblItems.AutoSize")));
			this.lblItems.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblItems.Dock")));
			this.lblItems.Enabled = ((bool)(resources.GetObject("lblItems.Enabled")));
			this.lblItems.Font = ((System.Drawing.Font)(resources.GetObject("lblItems.Font")));
			this.lblItems.Image = ((System.Drawing.Image)(resources.GetObject("lblItems.Image")));
			this.lblItems.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblItems.ImageAlign")));
			this.lblItems.ImageIndex = ((int)(resources.GetObject("lblItems.ImageIndex")));
			this.lblItems.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblItems.ImeMode")));
			this.lblItems.Location = ((System.Drawing.Point)(resources.GetObject("lblItems.Location")));
			this.lblItems.Name = "lblItems";
			this.lblItems.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblItems.RightToLeft")));
			this.lblItems.Size = ((System.Drawing.Size)(resources.GetObject("lblItems.Size")));
			this.lblItems.TabIndex = ((int)(resources.GetObject("lblItems.TabIndex")));
			this.lblItems.Text = resources.GetString("lblItems.Text");
			this.lblItems.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblItems.TextAlign")));
			this.lblItems.Visible = ((bool)(resources.GetObject("lblItems.Visible")));
			// 
			// txtItems
			// 
			this.txtItems.AccessibleDescription = resources.GetString("txtItems.AccessibleDescription");
			this.txtItems.AccessibleName = resources.GetString("txtItems.AccessibleName");
			this.txtItems.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("txtItems.Anchor")));
			this.txtItems.AutoSize = ((bool)(resources.GetObject("txtItems.AutoSize")));
			this.txtItems.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("txtItems.BackgroundImage")));
			this.txtItems.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("txtItems.Dock")));
			this.txtItems.Enabled = ((bool)(resources.GetObject("txtItems.Enabled")));
			this.txtItems.Font = ((System.Drawing.Font)(resources.GetObject("txtItems.Font")));
			this.txtItems.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("txtItems.ImeMode")));
			this.txtItems.Location = ((System.Drawing.Point)(resources.GetObject("txtItems.Location")));
			this.txtItems.MaxLength = ((int)(resources.GetObject("txtItems.MaxLength")));
			this.txtItems.Multiline = ((bool)(resources.GetObject("txtItems.Multiline")));
			this.txtItems.Name = "txtItems";
			this.txtItems.PasswordChar = ((char)(resources.GetObject("txtItems.PasswordChar")));
			this.txtItems.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("txtItems.RightToLeft")));
			this.txtItems.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("txtItems.ScrollBars")));
			this.txtItems.Size = ((System.Drawing.Size)(resources.GetObject("txtItems.Size")));
			this.txtItems.TabIndex = ((int)(resources.GetObject("txtItems.TabIndex")));
			this.txtItems.Text = resources.GetString("txtItems.Text");
			this.txtItems.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("txtItems.TextAlign")));
			this.txtItems.Visible = ((bool)(resources.GetObject("txtItems.Visible")));
			this.txtItems.WordWrap = ((bool)(resources.GetObject("txtItems.WordWrap")));
			// 
			// lblFilterField2
			// 
			this.lblFilterField2.AccessibleDescription = resources.GetString("lblFilterField2.AccessibleDescription");
			this.lblFilterField2.AccessibleName = resources.GetString("lblFilterField2.AccessibleName");
			this.lblFilterField2.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblFilterField2.Anchor")));
			this.lblFilterField2.AutoSize = ((bool)(resources.GetObject("lblFilterField2.AutoSize")));
			this.lblFilterField2.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblFilterField2.Dock")));
			this.lblFilterField2.Enabled = ((bool)(resources.GetObject("lblFilterField2.Enabled")));
			this.lblFilterField2.Font = ((System.Drawing.Font)(resources.GetObject("lblFilterField2.Font")));
			this.lblFilterField2.Image = ((System.Drawing.Image)(resources.GetObject("lblFilterField2.Image")));
			this.lblFilterField2.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblFilterField2.ImageAlign")));
			this.lblFilterField2.ImageIndex = ((int)(resources.GetObject("lblFilterField2.ImageIndex")));
			this.lblFilterField2.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblFilterField2.ImeMode")));
			this.lblFilterField2.Location = ((System.Drawing.Point)(resources.GetObject("lblFilterField2.Location")));
			this.lblFilterField2.Name = "lblFilterField2";
			this.lblFilterField2.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblFilterField2.RightToLeft")));
			this.lblFilterField2.Size = ((System.Drawing.Size)(resources.GetObject("lblFilterField2.Size")));
			this.lblFilterField2.TabIndex = ((int)(resources.GetObject("lblFilterField2.TabIndex")));
			this.lblFilterField2.Text = resources.GetString("lblFilterField2.Text");
			this.lblFilterField2.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblFilterField2.TextAlign")));
			this.lblFilterField2.Visible = ((bool)(resources.GetObject("lblFilterField2.Visible")));
			// 
			// cboFilterField2
			// 
			this.cboFilterField2.AccessibleDescription = resources.GetString("cboFilterField2.AccessibleDescription");
			this.cboFilterField2.AccessibleName = resources.GetString("cboFilterField2.AccessibleName");
			this.cboFilterField2.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("cboFilterField2.Anchor")));
			this.cboFilterField2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cboFilterField2.BackgroundImage")));
			this.cboFilterField2.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("cboFilterField2.Dock")));
			this.cboFilterField2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboFilterField2.Enabled = ((bool)(resources.GetObject("cboFilterField2.Enabled")));
			this.cboFilterField2.Font = ((System.Drawing.Font)(resources.GetObject("cboFilterField2.Font")));
			this.cboFilterField2.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("cboFilterField2.ImeMode")));
			this.cboFilterField2.IntegralHeight = ((bool)(resources.GetObject("cboFilterField2.IntegralHeight")));
			this.cboFilterField2.ItemHeight = ((int)(resources.GetObject("cboFilterField2.ItemHeight")));
			this.cboFilterField2.Location = ((System.Drawing.Point)(resources.GetObject("cboFilterField2.Location")));
			this.cboFilterField2.MaxDropDownItems = ((int)(resources.GetObject("cboFilterField2.MaxDropDownItems")));
			this.cboFilterField2.MaxLength = ((int)(resources.GetObject("cboFilterField2.MaxLength")));
			this.cboFilterField2.Name = "cboFilterField2";
			this.cboFilterField2.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("cboFilterField2.RightToLeft")));
			this.cboFilterField2.Size = ((System.Drawing.Size)(resources.GetObject("cboFilterField2.Size")));
			this.cboFilterField2.TabIndex = ((int)(resources.GetObject("cboFilterField2.TabIndex")));
			this.cboFilterField2.Text = resources.GetString("cboFilterField2.Text");
			this.cboFilterField2.Visible = ((bool)(resources.GetObject("cboFilterField2.Visible")));
			this.cboFilterField2.SelectedIndexChanged += new System.EventHandler(this.cboFilterField2_SelectedIndexChanged);
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
			// chkSameRow
			// 
			this.chkSameRow.AccessibleDescription = resources.GetString("chkSameRow.AccessibleDescription");
			this.chkSameRow.AccessibleName = resources.GetString("chkSameRow.AccessibleName");
			this.chkSameRow.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("chkSameRow.Anchor")));
			this.chkSameRow.Appearance = ((System.Windows.Forms.Appearance)(resources.GetObject("chkSameRow.Appearance")));
			this.chkSameRow.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("chkSameRow.BackgroundImage")));
			this.chkSameRow.CheckAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("chkSameRow.CheckAlign")));
			this.chkSameRow.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("chkSameRow.Dock")));
			this.chkSameRow.Enabled = ((bool)(resources.GetObject("chkSameRow.Enabled")));
			this.chkSameRow.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("chkSameRow.FlatStyle")));
			this.chkSameRow.Font = ((System.Drawing.Font)(resources.GetObject("chkSameRow.Font")));
			this.chkSameRow.Image = ((System.Drawing.Image)(resources.GetObject("chkSameRow.Image")));
			this.chkSameRow.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("chkSameRow.ImageAlign")));
			this.chkSameRow.ImageIndex = ((int)(resources.GetObject("chkSameRow.ImageIndex")));
			this.chkSameRow.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("chkSameRow.ImeMode")));
			this.chkSameRow.Location = ((System.Drawing.Point)(resources.GetObject("chkSameRow.Location")));
			this.chkSameRow.Name = "chkSameRow";
			this.chkSameRow.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("chkSameRow.RightToLeft")));
			this.chkSameRow.Size = ((System.Drawing.Size)(resources.GetObject("chkSameRow.Size")));
			this.chkSameRow.TabIndex = ((int)(resources.GetObject("chkSameRow.TabIndex")));
			this.chkSameRow.Text = resources.GetString("chkSameRow.Text");
			this.chkSameRow.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("chkSameRow.TextAlign")));
			this.chkSameRow.Visible = ((bool)(resources.GetObject("chkSameRow.Visible")));
			// 
			// txtDefaultValue
			// 
			this.txtDefaultValue.AccessibleDescription = resources.GetString("txtDefaultValue.AccessibleDescription");
			this.txtDefaultValue.AccessibleName = resources.GetString("txtDefaultValue.AccessibleName");
			this.txtDefaultValue.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("txtDefaultValue.Anchor")));
			this.txtDefaultValue.AutoSize = ((bool)(resources.GetObject("txtDefaultValue.AutoSize")));
			this.txtDefaultValue.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("txtDefaultValue.BackgroundImage")));
			this.txtDefaultValue.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("txtDefaultValue.Dock")));
			this.txtDefaultValue.Enabled = ((bool)(resources.GetObject("txtDefaultValue.Enabled")));
			this.txtDefaultValue.Font = ((System.Drawing.Font)(resources.GetObject("txtDefaultValue.Font")));
			this.txtDefaultValue.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("txtDefaultValue.ImeMode")));
			this.txtDefaultValue.Location = ((System.Drawing.Point)(resources.GetObject("txtDefaultValue.Location")));
			this.txtDefaultValue.MaxLength = ((int)(resources.GetObject("txtDefaultValue.MaxLength")));
			this.txtDefaultValue.Multiline = ((bool)(resources.GetObject("txtDefaultValue.Multiline")));
			this.txtDefaultValue.Name = "txtDefaultValue";
			this.txtDefaultValue.PasswordChar = ((char)(resources.GetObject("txtDefaultValue.PasswordChar")));
			this.txtDefaultValue.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("txtDefaultValue.RightToLeft")));
			this.txtDefaultValue.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("txtDefaultValue.ScrollBars")));
			this.txtDefaultValue.Size = ((System.Drawing.Size)(resources.GetObject("txtDefaultValue.Size")));
			this.txtDefaultValue.TabIndex = ((int)(resources.GetObject("txtDefaultValue.TabIndex")));
			this.txtDefaultValue.Text = resources.GetString("txtDefaultValue.Text");
			this.txtDefaultValue.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("txtDefaultValue.TextAlign")));
			this.txtDefaultValue.Visible = ((bool)(resources.GetObject("txtDefaultValue.Visible")));
			this.txtDefaultValue.WordWrap = ((bool)(resources.GetObject("txtDefaultValue.WordWrap")));
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
			// cboDataType
			// 
			this.cboDataType.AccessibleDescription = resources.GetString("cboDataType.AccessibleDescription");
			this.cboDataType.AccessibleName = resources.GetString("cboDataType.AccessibleName");
			this.cboDataType.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("cboDataType.Anchor")));
			this.cboDataType.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cboDataType.BackgroundImage")));
			this.cboDataType.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("cboDataType.Dock")));
			this.cboDataType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboDataType.Enabled = ((bool)(resources.GetObject("cboDataType.Enabled")));
			this.cboDataType.Font = ((System.Drawing.Font)(resources.GetObject("cboDataType.Font")));
			this.cboDataType.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("cboDataType.ImeMode")));
			this.cboDataType.IntegralHeight = ((bool)(resources.GetObject("cboDataType.IntegralHeight")));
			this.cboDataType.ItemHeight = ((int)(resources.GetObject("cboDataType.ItemHeight")));
			this.cboDataType.Location = ((System.Drawing.Point)(resources.GetObject("cboDataType.Location")));
			this.cboDataType.MaxDropDownItems = ((int)(resources.GetObject("cboDataType.MaxDropDownItems")));
			this.cboDataType.MaxLength = ((int)(resources.GetObject("cboDataType.MaxLength")));
			this.cboDataType.Name = "cboDataType";
			this.cboDataType.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("cboDataType.RightToLeft")));
			this.cboDataType.Size = ((System.Drawing.Size)(resources.GetObject("cboDataType.Size")));
			this.cboDataType.Sorted = true;
			this.cboDataType.TabIndex = ((int)(resources.GetObject("cboDataType.TabIndex")));
			this.cboDataType.Text = resources.GetString("cboDataType.Text");
			this.cboDataType.Visible = ((bool)(resources.GetObject("cboDataType.Visible")));
			// 
			// txtParameterName
			// 
			this.txtParameterName.AccessibleDescription = resources.GetString("txtParameterName.AccessibleDescription");
			this.txtParameterName.AccessibleName = resources.GetString("txtParameterName.AccessibleName");
			this.txtParameterName.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("txtParameterName.Anchor")));
			this.txtParameterName.AutoSize = ((bool)(resources.GetObject("txtParameterName.AutoSize")));
			this.txtParameterName.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("txtParameterName.BackgroundImage")));
			this.txtParameterName.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("txtParameterName.Dock")));
			this.txtParameterName.Enabled = ((bool)(resources.GetObject("txtParameterName.Enabled")));
			this.txtParameterName.Font = ((System.Drawing.Font)(resources.GetObject("txtParameterName.Font")));
			this.txtParameterName.ForeColor = System.Drawing.SystemColors.WindowText;
			this.txtParameterName.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("txtParameterName.ImeMode")));
			this.txtParameterName.Location = ((System.Drawing.Point)(resources.GetObject("txtParameterName.Location")));
			this.txtParameterName.MaxLength = ((int)(resources.GetObject("txtParameterName.MaxLength")));
			this.txtParameterName.Multiline = ((bool)(resources.GetObject("txtParameterName.Multiline")));
			this.txtParameterName.Name = "txtParameterName";
			this.txtParameterName.PasswordChar = ((char)(resources.GetObject("txtParameterName.PasswordChar")));
			this.txtParameterName.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("txtParameterName.RightToLeft")));
			this.txtParameterName.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("txtParameterName.ScrollBars")));
			this.txtParameterName.Size = ((System.Drawing.Size)(resources.GetObject("txtParameterName.Size")));
			this.txtParameterName.TabIndex = ((int)(resources.GetObject("txtParameterName.TabIndex")));
			this.txtParameterName.Text = resources.GetString("txtParameterName.Text");
			this.txtParameterName.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("txtParameterName.TextAlign")));
			this.txtParameterName.Visible = ((bool)(resources.GetObject("txtParameterName.Visible")));
			this.txtParameterName.WordWrap = ((bool)(resources.GetObject("txtParameterName.WordWrap")));
			// 
			// txtWidth1
			// 
			this.txtWidth1.AcceptsEscape = ((bool)(resources.GetObject("txtWidth1.AcceptsEscape")));
			this.txtWidth1.AccessibleDescription = resources.GetString("txtWidth1.AccessibleDescription");
			this.txtWidth1.AccessibleName = resources.GetString("txtWidth1.AccessibleName");
			this.txtWidth1.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("txtWidth1.Anchor")));
			this.txtWidth1.AutoSize = ((bool)(resources.GetObject("txtWidth1.AutoSize")));
			this.txtWidth1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("txtWidth1.BackgroundImage")));
			this.txtWidth1.BorderStyle = ((System.Windows.Forms.BorderStyle)(resources.GetObject("txtWidth1.BorderStyle")));
			// 
			// txtWidth1.Calculator
			// 
			this.txtWidth1.Calculator.AccessibleDescription = resources.GetString("txtWidth1.Calculator.AccessibleDescription");
			this.txtWidth1.Calculator.AccessibleName = resources.GetString("txtWidth1.Calculator.AccessibleName");
			this.txtWidth1.Calculator.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("txtWidth1.Calculator.BackgroundImage")));
			this.txtWidth1.Calculator.ButtonFlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("txtWidth1.Calculator.ButtonFlatStyle")));
			this.txtWidth1.Calculator.DisplayFormat = resources.GetString("txtWidth1.Calculator.DisplayFormat");
			this.txtWidth1.Calculator.Font = ((System.Drawing.Font)(resources.GetObject("txtWidth1.Calculator.Font")));
			this.txtWidth1.Calculator.FormatOnClose = ((bool)(resources.GetObject("txtWidth1.Calculator.FormatOnClose")));
			this.txtWidth1.Calculator.StoredFormat = resources.GetString("txtWidth1.Calculator.StoredFormat");
			this.txtWidth1.Calculator.UIStrings.Content = ((string[])(resources.GetObject("txtWidth1.Calculator.UIStrings.Content")));
			this.txtWidth1.CaseSensitive = ((bool)(resources.GetObject("txtWidth1.CaseSensitive")));
			this.txtWidth1.Culture = ((int)(resources.GetObject("txtWidth1.Culture")));
			this.txtWidth1.CustomFormat = resources.GetString("txtWidth1.CustomFormat");
			this.txtWidth1.DataType = ((System.Type)(resources.GetObject("txtWidth1.DataType")));
			this.txtWidth1.DisplayFormat.CustomFormat = resources.GetString("txtWidth1.DisplayFormat.CustomFormat");
			this.txtWidth1.DisplayFormat.FormatType = ((C1.Win.C1Input.FormatTypeEnum)(resources.GetObject("txtWidth1.DisplayFormat.FormatType")));
			this.txtWidth1.DisplayFormat.Inherit = ((C1.Win.C1Input.FormatInfoInheritFlags)(resources.GetObject("txtWidth1.DisplayFormat.Inherit")));
			this.txtWidth1.DisplayFormat.NullText = resources.GetString("txtWidth1.DisplayFormat.NullText");
			this.txtWidth1.DisplayFormat.TrimEnd = ((bool)(resources.GetObject("txtWidth1.DisplayFormat.TrimEnd")));
			this.txtWidth1.DisplayFormat.TrimStart = ((bool)(resources.GetObject("txtWidth1.DisplayFormat.TrimStart")));
			this.txtWidth1.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("txtWidth1.Dock")));
			this.txtWidth1.DropDownFormAlign = ((C1.Win.C1Input.DropDownFormAlignmentEnum)(resources.GetObject("txtWidth1.DropDownFormAlign")));
			this.txtWidth1.EditFormat.CustomFormat = resources.GetString("txtWidth1.EditFormat.CustomFormat");
			this.txtWidth1.EditFormat.FormatType = ((C1.Win.C1Input.FormatTypeEnum)(resources.GetObject("txtWidth1.EditFormat.FormatType")));
			this.txtWidth1.EditFormat.Inherit = ((C1.Win.C1Input.FormatInfoInheritFlags)(resources.GetObject("txtWidth1.EditFormat.Inherit")));
			this.txtWidth1.EditFormat.NullText = resources.GetString("txtWidth1.EditFormat.NullText");
			this.txtWidth1.EditFormat.TrimEnd = ((bool)(resources.GetObject("txtWidth1.EditFormat.TrimEnd")));
			this.txtWidth1.EditFormat.TrimStart = ((bool)(resources.GetObject("txtWidth1.EditFormat.TrimStart")));
			this.txtWidth1.EditMask = resources.GetString("txtWidth1.EditMask");
			this.txtWidth1.EmptyAsNull = ((bool)(resources.GetObject("txtWidth1.EmptyAsNull")));
			this.txtWidth1.Enabled = ((bool)(resources.GetObject("txtWidth1.Enabled")));
			this.txtWidth1.ErrorInfo.BeepOnError = ((bool)(resources.GetObject("txtWidth1.ErrorInfo.BeepOnError")));
			this.txtWidth1.ErrorInfo.ErrorMessage = resources.GetString("txtWidth1.ErrorInfo.ErrorMessage");
			this.txtWidth1.ErrorInfo.ErrorMessageCaption = resources.GetString("txtWidth1.ErrorInfo.ErrorMessageCaption");
			this.txtWidth1.ErrorInfo.ShowErrorMessage = ((bool)(resources.GetObject("txtWidth1.ErrorInfo.ShowErrorMessage")));
			this.txtWidth1.ErrorInfo.ValueOnError = ((object)(resources.GetObject("txtWidth1.ErrorInfo.ValueOnError")));
			this.txtWidth1.Font = ((System.Drawing.Font)(resources.GetObject("txtWidth1.Font")));
			this.txtWidth1.FormatType = ((C1.Win.C1Input.FormatTypeEnum)(resources.GetObject("txtWidth1.FormatType")));
			this.txtWidth1.GapHeight = ((int)(resources.GetObject("txtWidth1.GapHeight")));
			this.txtWidth1.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("txtWidth1.ImeMode")));
			this.txtWidth1.Increment = ((object)(resources.GetObject("txtWidth1.Increment")));
			this.txtWidth1.InitialSelection = ((C1.Win.C1Input.InitialSelectionEnum)(resources.GetObject("txtWidth1.InitialSelection")));
			this.txtWidth1.Location = ((System.Drawing.Point)(resources.GetObject("txtWidth1.Location")));
			this.txtWidth1.MaskInfo.AutoTabWhenFilled = ((bool)(resources.GetObject("txtWidth1.MaskInfo.AutoTabWhenFilled")));
			this.txtWidth1.MaskInfo.CaseSensitive = ((bool)(resources.GetObject("txtWidth1.MaskInfo.CaseSensitive")));
			this.txtWidth1.MaskInfo.CopyWithLiterals = ((bool)(resources.GetObject("txtWidth1.MaskInfo.CopyWithLiterals")));
			this.txtWidth1.MaskInfo.EditMask = resources.GetString("txtWidth1.MaskInfo.EditMask");
			this.txtWidth1.MaskInfo.EmptyAsNull = ((bool)(resources.GetObject("txtWidth1.MaskInfo.EmptyAsNull")));
			this.txtWidth1.MaskInfo.ErrorMessage = resources.GetString("txtWidth1.MaskInfo.ErrorMessage");
			this.txtWidth1.MaskInfo.Inherit = ((C1.Win.C1Input.MaskInfoInheritFlags)(resources.GetObject("txtWidth1.MaskInfo.Inherit")));
			this.txtWidth1.MaskInfo.PromptChar = ((char)(resources.GetObject("txtWidth1.MaskInfo.PromptChar")));
			this.txtWidth1.MaskInfo.ShowLiterals = ((C1.Win.C1Input.ShowLiteralsEnum)(resources.GetObject("txtWidth1.MaskInfo.ShowLiterals")));
			this.txtWidth1.MaskInfo.StoredEmptyChar = ((char)(resources.GetObject("txtWidth1.MaskInfo.StoredEmptyChar")));
			this.txtWidth1.MaxLength = ((int)(resources.GetObject("txtWidth1.MaxLength")));
			this.txtWidth1.Name = "txtWidth1";
			this.txtWidth1.NullText = resources.GetString("txtWidth1.NullText");
			this.txtWidth1.ParseInfo.CaseSensitive = ((bool)(resources.GetObject("txtWidth1.ParseInfo.CaseSensitive")));
			this.txtWidth1.ParseInfo.CustomFormat = resources.GetString("txtWidth1.ParseInfo.CustomFormat");
			this.txtWidth1.ParseInfo.EmptyAsNull = ((bool)(resources.GetObject("txtWidth1.ParseInfo.EmptyAsNull")));
			this.txtWidth1.ParseInfo.ErrorMessage = resources.GetString("txtWidth1.ParseInfo.ErrorMessage");
			this.txtWidth1.ParseInfo.FormatType = ((C1.Win.C1Input.FormatTypeEnum)(resources.GetObject("txtWidth1.ParseInfo.FormatType")));
			this.txtWidth1.ParseInfo.Inherit = ((C1.Win.C1Input.ParseInfoInheritFlags)(resources.GetObject("txtWidth1.ParseInfo.Inherit")));
			this.txtWidth1.ParseInfo.NullText = resources.GetString("txtWidth1.ParseInfo.NullText");
			this.txtWidth1.ParseInfo.NumberStyle = ((C1.Win.C1Input.NumberStyleFlags)(resources.GetObject("txtWidth1.ParseInfo.NumberStyle")));
			this.txtWidth1.ParseInfo.TrimEnd = ((bool)(resources.GetObject("txtWidth1.ParseInfo.TrimEnd")));
			this.txtWidth1.ParseInfo.TrimStart = ((bool)(resources.GetObject("txtWidth1.ParseInfo.TrimStart")));
			this.txtWidth1.PasswordChar = ((char)(resources.GetObject("txtWidth1.PasswordChar")));
			this.txtWidth1.PostValidation.CaseSensitive = ((bool)(resources.GetObject("txtWidth1.PostValidation.CaseSensitive")));
			this.txtWidth1.PostValidation.ErrorMessage = resources.GetString("txtWidth1.PostValidation.ErrorMessage");
			this.txtWidth1.PostValidation.Inherit = ((C1.Win.C1Input.PostValidationInheritFlags)(resources.GetObject("txtWidth1.PostValidation.Inherit")));
			this.txtWidth1.PostValidation.Intervals.AddRange(new C1.Win.C1Input.ValueInterval[] {
																									((C1.Win.C1Input.ValueInterval)(resources.GetObject("txtWidth1.PostValidation.Intervals")))});
			this.txtWidth1.PostValidation.Validation = ((C1.Win.C1Input.PostValidationTypeEnum)(resources.GetObject("txtWidth1.PostValidation.Validation")));
			this.txtWidth1.PostValidation.Values = ((System.Array)(resources.GetObject("txtWidth1.PostValidation.Values")));
			this.txtWidth1.PostValidation.ValuesExcluded = ((System.Array)(resources.GetObject("txtWidth1.PostValidation.ValuesExcluded")));
			this.txtWidth1.PreValidation.CaseSensitive = ((bool)(resources.GetObject("txtWidth1.PreValidation.CaseSensitive")));
			this.txtWidth1.PreValidation.ErrorMessage = resources.GetString("txtWidth1.PreValidation.ErrorMessage");
			this.txtWidth1.PreValidation.Inherit = ((C1.Win.C1Input.PreValidationInheritFlags)(resources.GetObject("txtWidth1.PreValidation.Inherit")));
			this.txtWidth1.PreValidation.ItemSeparator = resources.GetString("txtWidth1.PreValidation.ItemSeparator");
			this.txtWidth1.PreValidation.PatternString = resources.GetString("txtWidth1.PreValidation.PatternString");
			this.txtWidth1.PreValidation.RegexOptions = ((C1.Win.C1Input.RegexOptionFlags)(resources.GetObject("txtWidth1.PreValidation.RegexOptions")));
			this.txtWidth1.PreValidation.TrimEnd = ((bool)(resources.GetObject("txtWidth1.PreValidation.TrimEnd")));
			this.txtWidth1.PreValidation.TrimStart = ((bool)(resources.GetObject("txtWidth1.PreValidation.TrimStart")));
			this.txtWidth1.PreValidation.Validation = ((C1.Win.C1Input.PreValidationTypeEnum)(resources.GetObject("txtWidth1.PreValidation.Validation")));
			this.txtWidth1.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("txtWidth1.RightToLeft")));
			this.txtWidth1.ShowFocusRectangle = ((bool)(resources.GetObject("txtWidth1.ShowFocusRectangle")));
			this.txtWidth1.Size = ((System.Drawing.Size)(resources.GetObject("txtWidth1.Size")));
			this.txtWidth1.TabIndex = ((int)(resources.GetObject("txtWidth1.TabIndex")));
			this.txtWidth1.Tag = ((object)(resources.GetObject("txtWidth1.Tag")));
			this.txtWidth1.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("txtWidth1.TextAlign")));
			this.txtWidth1.TrimEnd = ((bool)(resources.GetObject("txtWidth1.TrimEnd")));
			this.txtWidth1.TrimStart = ((bool)(resources.GetObject("txtWidth1.TrimStart")));
			this.txtWidth1.UserCultureOverride = ((bool)(resources.GetObject("txtWidth1.UserCultureOverride")));
			this.txtWidth1.Value = ((object)(resources.GetObject("txtWidth1.Value")));
			this.txtWidth1.VerticalAlign = ((C1.Win.C1Input.VerticalAlignEnum)(resources.GetObject("txtWidth1.VerticalAlign")));
			this.txtWidth1.Visible = ((bool)(resources.GetObject("txtWidth1.Visible")));
			this.txtWidth1.VisibleButtons = ((C1.Win.C1Input.DropDownControlButtonFlags)(resources.GetObject("txtWidth1.VisibleButtons")));
			// 
			// chkMultiSelection
			// 
			this.chkMultiSelection.AccessibleDescription = resources.GetString("chkMultiSelection.AccessibleDescription");
			this.chkMultiSelection.AccessibleName = resources.GetString("chkMultiSelection.AccessibleName");
			this.chkMultiSelection.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("chkMultiSelection.Anchor")));
			this.chkMultiSelection.Appearance = ((System.Windows.Forms.Appearance)(resources.GetObject("chkMultiSelection.Appearance")));
			this.chkMultiSelection.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("chkMultiSelection.BackgroundImage")));
			this.chkMultiSelection.CheckAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("chkMultiSelection.CheckAlign")));
			this.chkMultiSelection.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("chkMultiSelection.Dock")));
			this.chkMultiSelection.Enabled = ((bool)(resources.GetObject("chkMultiSelection.Enabled")));
			this.chkMultiSelection.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("chkMultiSelection.FlatStyle")));
			this.chkMultiSelection.Font = ((System.Drawing.Font)(resources.GetObject("chkMultiSelection.Font")));
			this.chkMultiSelection.Image = ((System.Drawing.Image)(resources.GetObject("chkMultiSelection.Image")));
			this.chkMultiSelection.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("chkMultiSelection.ImageAlign")));
			this.chkMultiSelection.ImageIndex = ((int)(resources.GetObject("chkMultiSelection.ImageIndex")));
			this.chkMultiSelection.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("chkMultiSelection.ImeMode")));
			this.chkMultiSelection.Location = ((System.Drawing.Point)(resources.GetObject("chkMultiSelection.Location")));
			this.chkMultiSelection.Name = "chkMultiSelection";
			this.chkMultiSelection.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("chkMultiSelection.RightToLeft")));
			this.chkMultiSelection.Size = ((System.Drawing.Size)(resources.GetObject("chkMultiSelection.Size")));
			this.chkMultiSelection.TabIndex = ((int)(resources.GetObject("chkMultiSelection.TabIndex")));
			this.chkMultiSelection.Text = resources.GetString("chkMultiSelection.Text");
			this.chkMultiSelection.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("chkMultiSelection.TextAlign")));
			this.chkMultiSelection.Visible = ((bool)(resources.GetObject("chkMultiSelection.Visible")));
			// 
			// ReportParameter
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
			this.Controls.Add(this.tvwParameterList);
			this.Controls.Add(this.btnMoveDown);
			this.Controls.Add(this.btnMoveUp);
			this.Controls.Add(this.btnClose);
			this.Controls.Add(this.btnHelp);
			this.Controls.Add(this.btnEdit);
			this.Controls.Add(this.btnAdd);
			this.Controls.Add(this.btnDelete);
			this.Controls.Add(this.btnSave);
			this.Controls.Add(this.grpDetails);
			this.Enabled = ((bool)(resources.GetObject("$this.Enabled")));
			this.Font = ((System.Drawing.Font)(resources.GetObject("$this.Font")));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("$this.ImeMode")));
			this.Location = ((System.Drawing.Point)(resources.GetObject("$this.Location")));
			this.MaximizeBox = false;
			this.MaximumSize = ((System.Drawing.Size)(resources.GetObject("$this.MaximumSize")));
			this.MinimumSize = ((System.Drawing.Size)(resources.GetObject("$this.MinimumSize")));
			this.Name = "ReportParameter";
			this.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("$this.RightToLeft")));
			this.StartPosition = ((System.Windows.Forms.FormStartPosition)(resources.GetObject("$this.StartPosition")));
			this.Text = resources.GetString("$this.Text");
			this.Closing += new System.ComponentModel.CancelEventHandler(this.ReportParameter_Closing);
			this.Load += new System.EventHandler(this.ReportParameter_Load);
			this.grpDetails.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.txtWidth2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.txtWidth)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.txtWidth1)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		#region Load Form

		//**************************************************************************              
		///    <Description>
		///       Form load event, initialize every thing
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
		private void ReportParameter_Load(object sender, EventArgs e)
		{
			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.WaitCursor;

			#endregion Code Inserted Automatically

			
			const string METHOD_NAME = THIS + ".ReportParameter_Load()";
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
					PCSMessageBox.Show(ErrorCode.MESSAGE_YOU_HAVE_NO_RIGHT_TO_VIEW, MessageBoxIcon.Error);
					// Code Inserted Automatically
#region Code Inserted Automatically
this.Cursor = Cursors.Default;
#endregion Code Inserted Automatically

					return;
				}
				#endregion

				IDictionaryEnumerator objEnum = htDataType.GetEnumerator();
				while (objEnum.MoveNext())
				{
					cboDataType.Items.Add(objEnum.Value);
				}
				cboDataType.SelectedIndex = 0;
				
				// Load data into FromTable combo
				TableConfigBO boTableConfig = new TableConfigBO();
				//DataSet dstTableName = boTableConfig.ListTableOrView();
				//ArrayList arrTableName = GetArrayFromDataSet(dstTableName, SchemaTableTable.TABLE_NAME);
				DataSet dstTableName = boTableConfig.List();
				ArrayList arrTableName = GetArrayFromDataSet(dstTableName, sys_TableTable.TABLE_NAME);
				cboFromTable.Items.Clear();
				cboFromTable.Items.Add(string.Empty);
				for (int i = 0; i < arrTableName.Count; i++)
				{
					DataRow row = (DataRow)arrTableName[i];
					cboFromTable.Items.Add(row[sys_TableTable.TABLEORVIEW_FLD].ToString().Trim());
				}
				cboFromTable.Sorted = true;				
				
				// bind list box
				BuildTree(marrParams);				
				SwitchFormMode();
				tvwParameterList.Select();
				tvwParameterList.Focus();

				// formatting Width textbox
				txtWidth.FormatType = FormatTypeEnum.CustomFormat;
				txtWidth.CustomFormat = Constants.INTERGER_NUMBERFORMAT;
				txtWidth1.FormatType = FormatTypeEnum.CustomFormat;
				txtWidth1.CustomFormat = Constants.INTERGER_NUMBERFORMAT;
				txtWidth2.FormatType = FormatTypeEnum.CustomFormat;
				txtWidth2.CustomFormat = Constants.INTERGER_NUMBERFORMAT;

				if (cboFromTable.SelectedIndex > 0)
					chkMultiSelection.Visible = true;
				else
					chkMultiSelection.Visible = false;
				EnableButton();
			}
			catch (PCSException ex)
			{
				// displays the error message.
				PCSMessageBox.Show(ex.mCode, MessageBoxIcon.Error);
				// log message.
				try
				{
					Logger.LogMessage(ex.CauseException, METHOD_NAME, Level.ERROR);
				}
				catch
				{
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
				}
			}
			catch (Exception ex)
			{
				// displays the error message.
				PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxIcon.Error);
				// log message.
				try
				{
					Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
				}
				catch
				{
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
				}
			}

			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.Default;

			#endregion Code Inserted Automatically

			
		}

		#endregion

		#region Event Handler

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
		///       Clear value of all controls in form to add new record
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
		private void btnAdd_Click(object sender, EventArgs e)
		{
			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.WaitCursor;

			#endregion Code Inserted Automatically

			
			const string METHOD_NAME = THIS + "btnAdd_Click()";
			try
			{
				// txtParameterName.ReadOnly = false;
				mFormAction = EnumAction.Add;
				SwitchFormMode();
				// reset to default value
				ResetForm();
				txtParameterName.Focus();
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
		///       Turn form to edit mode
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
		///    Thachnn: 24/Oct/2005
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
				if (tvwParameterList.SelectedNode != null)
				{
					// turn to edit mode
					mFormAction = EnumAction.Edit;
					SwitchFormMode();
                    btnMoveUp.Enabled = btnMoveDown.Enabled = false;
					
					strSelectedPara = tvwParameterList.SelectedNode.Text;
					// load data of vo object
					mvoSelectedPara = (sys_ReportParaVO)(boReportParameter.GetObjectVO(strReportID, strSelectedPara));
					
					LayoutOnFormValueInParameterVoObject();					
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
		///       Validate data and add new record to database
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
		///    Thachnn: Oct/2005
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
				#region validate data

				// validate data
				if (!CheckMandatory(txtParameterName))
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Error);
					txtParameterName.Focus();
					txtParameterName.Select();
					// Code Inserted Automatically
#region Code Inserted Automatically
this.Cursor = Cursors.Default;
#endregion Code Inserted Automatically

					return;
				}
				if (!CheckMandatory(txtCaption))
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
				if (txtWidth.Value == DBNull.Value)
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Error);
					txtWidth.Focus();
					txtWidth.Select();
					// Code Inserted Automatically
#region Code Inserted Automatically
this.Cursor = Cursors.Default;
#endregion Code Inserted Automatically

					return;
				}

				if (cboFromTable.SelectedItem != null)
				{
					if (cboFromTable.SelectedItem.ToString().Trim() != string.Empty)
					{
						// user must select from field, filter 1 if select from table
						if (cboFromField.SelectedItem == null)
						{
							if (cboFromField.SelectedItem.ToString().Trim() == string.Empty)
							{
								PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Error);
								cboFromField.Focus();
								cboFromField.Select();
								// Code Inserted Automatically
#region Code Inserted Automatically
this.Cursor = Cursors.Default;
#endregion Code Inserted Automatically

								return;
							}
						}
						if (cboFilterField1.SelectedItem == null)
						{
							PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Error);
							cboFilterField1.Focus();
							cboFilterField1.Select();
							// Code Inserted Automatically
#region Code Inserted Automatically
this.Cursor = Cursors.Default;
#endregion Code Inserted Automatically

							return;
						}
						// check width if user select a filter field
						if (txtWidth1.Value == DBNull.Value || txtWidth1.Value.Equals(0))
						{
							PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Error);
							txtWidth1.Focus();
							txtWidth1.Select();
							// Code Inserted Automatically
#region Code Inserted Automatically
this.Cursor = Cursors.Default;
#endregion Code Inserted Automatically

							return;
						}
					}
				}
				
				if (cboFilterField2.SelectedItem != null) 
				{
					if(cboFilterField2.SelectedItem.ToString().Trim() != string.Empty)
					{
						// check width if user select a filter field 2
						if (txtWidth2.Value == DBNull.Value || txtWidth2.Value.Equals(0))
						{
							PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Error);
							txtWidth2.Focus();
							txtWidth2.Select();
							// Code Inserted Automatically
#region Code Inserted Automatically
this.Cursor = Cursors.Default;
#endregion Code Inserted Automatically

							return;
						}
					}
				}
				
				#region Check default value

				int nSelectedDataType = 0;
				IDictionaryEnumerator objEnum = htDataType.GetEnumerator();
				while (objEnum.MoveNext())
				{
					if (objEnum.Value.Equals(cboDataType.SelectedItem))
					{
						nSelectedDataType = (int) objEnum.Key;
						break;
					}
				}

				if (txtDefaultValue.Text.Trim() != string.Empty)
				{
					// we need to validate data again for default value 
					// based on data type selected by user
					if (nSelectedDataType.Equals((int)TypeCode.Boolean))
					{
						// user only allow to input 0/1 or true/false in default value
						try
						{
							Convert.ToBoolean(txtDefaultValue.Text.Trim());
						}
						catch
						{
							// TODO: Thachnn: fix the error code
							string[] arrstrErrorMessagePara = {" [" + txtDefaultValue.Text + "]",cboDataType.SelectedItem.ToString()};
							PCSMessageBox.Show(ErrorCode.MESSAGE_VALUE_AND_TYPE_NOTMATCH,MessageBoxIcon.Error, arrstrErrorMessagePara);
							txtDefaultValue.Focus();
							txtDefaultValue.Select();
							// Code Inserted Automatically
#region Code Inserted Automatically
this.Cursor = Cursors.Default;
#endregion Code Inserted Automatically

							return ;
						}
					}
					else if (nSelectedDataType.Equals((int)TypeCode.DateTime))
					{
						// user only allow to input valid date time format
						try
						{
							Convert.ToDateTime(txtDefaultValue.Text.Trim());
						}
						catch
						{
							// TODO: Thachnn: fix the error code
							string[] arrstrErrorMessagePara = {cboDataType.SelectedItem.ToString(),txtParameterName.Text};
							PCSMessageBox.Show(ErrorCode.MESSAGE_RELATION_REQUIRE ,MessageBoxIcon.Error, arrstrErrorMessagePara);
							txtDefaultValue.Focus();
							txtDefaultValue.Select();
							// Code Inserted Automatically
#region Code Inserted Automatically
this.Cursor = Cursors.Default;
#endregion Code Inserted Automatically

							return ;
						}
					}
					else if (nSelectedDataType.Equals((int)TypeCode.Char))
					{
						// only allow input a single character
						try
						{
							Convert.ToChar(txtDefaultValue.Text.Trim());
						}
						catch
						{
							// TODO: Thachnn: fix the error code
							string[] arrstrErrorMessagePara = {cboDataType.SelectedItem.ToString(),txtParameterName.Text};
							PCSMessageBox.Show(ErrorCode.MESSAGE_RELATION_REQUIRE ,MessageBoxIcon.Error, arrstrErrorMessagePara);
							txtDefaultValue.Focus();
							txtDefaultValue.Select();
							// Code Inserted Automatically
#region Code Inserted Automatically
this.Cursor = Cursors.Default;
#endregion Code Inserted Automatically

							return ;
						}
					}
					else if (!nSelectedDataType.Equals((int)TypeCode.String))
					{
						if (!FormControlComponents.IsNumeric(txtDefaultValue.Text.Trim()))
						{
							// TODO: Thachnn: fix the error code
							string[] arrstrErrorMessagePara = {cboDataType.SelectedItem.ToString(),txtParameterName.Text};
							PCSMessageBox.Show(ErrorCode.MESSAGE_RELATION_REQUIRE ,MessageBoxIcon.Error, arrstrErrorMessagePara);
							//PCSMessageBox.Show(ErrorCode.MESSAGE_INVALID_NUMERIC);
							txtDefaultValue.Focus();
							txtDefaultValue.Select();
							// Code Inserted Automatically
#region Code Inserted Automatically
this.Cursor = Cursors.Default;
#endregion Code Inserted Automatically

							return ;
						}
					}
				}
				#endregion


				#endregion

				if (SaveData())
				{
					switch (mFormAction)
					{
						case EnumAction.Add:
							marrParams.Add(mvoSelectedPara);
							// create new tree node
							TreeNode objNode = new TreeNode(mvoSelectedPara.ParaName);
							objNode.Tag = mvoSelectedPara;
							// add new node to tree view
							tvwParameterList.Nodes.Add(objNode);
							tvwParameterList.SelectedNode = objNode;
							break;
						case EnumAction.Edit:
							// update para name
							TreeNode objSelectedNode = tvwParameterList.SelectedNode;
							objSelectedNode.Text = mvoSelectedPara.ParaName;
							objSelectedNode.Tag = mvoSelectedPara;
							tvwParameterList.SelectedNode = objSelectedNode;
							break;
					}
					// unable to change parameter name
					// HACKED: Thachnn : 24/Oct/2005: allow to change parameter name (by mr HungLA suggest)
					// txtParameterName.ReadOnly = true;
					// ENDHACKED: Thachnn : 24/Oct/2005: allow to change parameter name (by mr HungLA suggest)

					mFormAction = EnumAction.Default;			    
					
					PCSMessageBox.Show(ErrorCode.MESSAGE_AFTER_SAVE_DATA, MessageBoxIcon.Information);
					tvwParameterList.Select();
					tvwParameterList.Focus();
					SwitchFormMode();
					EnableButton();
				}
				else
					PCSMessageBox.Show(ErrorCode.MESSAGE_CANNOT_SAVE_TO_DB, MessageBoxIcon.Error);
			}
			catch (PCSException ex)
			{
				// displays the error message.
				PCSMessageBox.Show(ex.mCode, MessageBoxIcon.Error);
				// log message.
				try
				{
					Logger.LogMessage(ex.CauseException, METHOD_NAME, Level.ERROR);
				}
				catch
				{
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
				}
			}
			catch (Exception ex)
			{
				// displays the error message.
				PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxIcon.Error);
				// log message.
				try
				{
					Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
				}
				catch
				{
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
				}
			}

			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.Default;

			#endregion Code Inserted Automatically

			
		}

		
		//**************************************************************************              
		///    <Description>
		///       Display confirm message, then delete selected record
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
				if (tvwParameterList.SelectedNode == null)
{
					// Code Inserted Automatically
#region Code Inserted Automatically
this.Cursor = Cursors.Default;
#endregion Code Inserted Automatically

					return;
}

				// alert user to confirm delete selected
				DialogResult dlgResult = PCSMessageBox.Show(ErrorCode.MESSAGE_DELETE_RECORD, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
				if (dlgResult == DialogResult.Yes)
				{
					TreeNode objSelectedNode = tvwParameterList.SelectedNode;
					// get value object of selected object
					//sys_ReportParaVO voReportPara = (sys_ReportParaVO)(boReportParameter.GetObjectVO(strReportID, mvoSelectedPara.ParaName));
					sys_ReportParaVO voReportPara = (sys_ReportParaVO)objSelectedNode.Tag;
					// delete it
					boReportParameter.Delete(voReportPara);
					// remove deleted node from the tree view
					tvwParameterList.Nodes.Remove(objSelectedNode);
					// remove deleted para from list
					marrParams.Remove(voReportPara);

					// clear all parameter information
					ClearParameterInfoControl();
					tvwParameterList.Select();
					tvwParameterList.Focus();
					EnableButton();

					//					if (marrParams.Count <= 0)
					//					{
					//						
					//						btnDelete.Enabled = false;
					//						btnEdit.Enabled = false;
					//					}
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
				catch (Exception exLog)
				{
					PCSMessageBox.Show(exLog.Message, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
				}
			}
			catch (Exception ex)
			{
				// displays the error message.
				PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxIcon.Error);
				// log message.
				try
				{
					Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
				}
				catch (Exception exLog)
				{
					PCSMessageBox.Show(exLog.Message, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
				}
			}

			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.Default;

			#endregion Code Inserted Automatically

			
		}

		//**************************************************************************              
		///    <Description>
		///       Move up the selected parameter
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
		///       Thachnn
		///    </Authors>
		///    <History>
		///    24-Oct-2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private void btnMoveUp_Click(object sender, EventArgs e)
		{
			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.WaitCursor;

			#endregion Code Inserted Automatically

			
			const string METHOD_NAME = THIS + ".btnMoveUp_Click()";						
			try
			{
				TreeNode objSelectedNode = tvwParameterList.SelectedNode;
				if (objSelectedNode != null)
				{
					if (objSelectedNode.PrevVisibleNode != null)
					{
						sys_ReportParaVO voCurrentParam = (sys_ReportParaVO)objSelectedNode.Tag;
						sys_ReportParaVO voPrevParam = (sys_ReportParaVO)objSelectedNode.PrevVisibleNode.Tag;
						// moving
						tvwParameterList.Nodes.Remove(objSelectedNode);
						tvwParameterList.Nodes.Insert(objSelectedNode.Index - 1, objSelectedNode);
						// now update field object
						voCurrentParam.ParaOrder = voPrevParam.ParaOrder;
						voPrevParam.ParaOrder = voCurrentParam.ParaOrder + 1;
						boReportParameter.SwitchParameters(voCurrentParam, voPrevParam);
					}
					// set focus on selected on
					tvwParameterList.Focus();
					tvwParameterList.Select();
					tvwParameterList.SelectedNode = objSelectedNode;
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
		///       Move down the selected parameter
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
		///       Thachnn
		///    </Authors>
		///    <History>
		///    24/Oct-2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private void btnMoveDown_Click(object sender, EventArgs e)
		{
			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.WaitCursor;

			#endregion Code Inserted Automatically

			
			const string METHOD_NAME = THIS + ".btnMoveDown_Click()";
			try
			{
				TreeNode objSelectedNode = tvwParameterList.SelectedNode;
				if (objSelectedNode != null)
				{
					if (objSelectedNode.NextVisibleNode != null)
					{
						sys_ReportParaVO voCurrentPara = (sys_ReportParaVO)objSelectedNode.Tag;
						sys_ReportParaVO voNextPara = (sys_ReportParaVO)objSelectedNode.NextVisibleNode.Tag;
						// moving
						tvwParameterList.Nodes.Remove(objSelectedNode);
						tvwParameterList.Nodes.Insert(objSelectedNode.Index + 1, objSelectedNode);
						// now update field object
						voCurrentPara.ParaOrder = voNextPara.ParaOrder;
						voNextPara.ParaOrder = voNextPara.ParaOrder - 1;
						boReportParameter.SwitchParameters(voCurrentPara, voNextPara);
					}
					// set focus on selected on
					tvwParameterList.Focus();
					tvwParameterList.Select();
					tvwParameterList.SelectedNode = objSelectedNode;
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
		///       Get the selected table name, 
		///       then fill its field to cboFromField, cboFilterField1, cboFilterField2
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
		private void cboFromTable_SelectedIndexChanged(object sender, EventArgs e)
		{
			const string METHOD_NAME = THIS + ".cboFromTable_SelectedIndexChanged()";
			try
			{
				// Load data into FromField combo
				//ComboBox cboFromTable = (ComboBox)sender;
				TableConfigDetailBO boTableConfigDetail = new TableConfigDetailBO();// .List(cboFromTable.Text.Trim());
				//DataSet dstFieldName = boTableConfigDetail.ListFieldName(cboFromTable.Text.Trim());
				//ArrayList arrFromField = GetArrayFromDataSet(dstFieldName, sys_TableFieldTable.TABLE_NAME);
				DataSet dstFieldName = boTableConfigDetail.List(cboFromTable.Text.Trim());
				ArrayList arrFromField = GetArrayFromDataSet(dstFieldName, sys_TableFieldTable.TABLE_NAME);
				cboFromField.Items.Clear();
				for (int i = 0; i < arrFromField.Count; i++)
				{
					DataRow row = (DataRow)arrFromField[i];
					cboFromField.Items.Add(row[sys_TableFieldTable.FIELDNAME_FLD].ToString().Trim());
				}
				cboFromField.Sorted = true;
				// Load data into FilterField1 combo
				cboFilterField1.Items.Clear();
				for (int i = 0; i < arrFromField.Count; i++)
				{
					DataRow row = (DataRow)arrFromField[i];
					cboFilterField1.Items.Add(row[sys_TableFieldTable.FIELDNAME_FLD].ToString().Trim());
				}
				cboFilterField1.Sorted = true;
				// Load data into FilterField2 combo
				cboFilterField2.Items.Clear();
				cboFilterField2.Items.Add(string.Empty);
				for (int i = 0; i < arrFromField.Count; i++)
				{
					DataRow row = (DataRow)arrFromField[i];
					cboFilterField2.Items.Add(row[sys_TableFieldTable.FIELDNAME_FLD].ToString().Trim());
				}
				cboFilterField2.Sorted = true;
				if (cboFromTable.SelectedIndex > 0) //(cboFromTable.SelectedItem != null && cboFromTable.SelectedItem.ToString() != string.Empty)
				{
					// change the color of key field and filter field 1 to required field
					lblFromField.ForeColor = Color.Maroon;
					lblFilterField1.ForeColor = Color.Maroon;
					lblWidth1.ForeColor = Color.Maroon;
					// show Multil selection checkbox
					chkMultiSelection.Visible = true;
				}
				else
				{
					// reset the color of key field and filter field 1 to default
					lblFromField.ResetForeColor();
					lblFilterField1.ResetForeColor();
					lblWidth1.ResetForeColor();
					lblWidth2.ResetForeColor();
					// clear the from field, filter field if any
					cboFromField.Items.Clear();
					cboFilterField1.Items.Clear();
					cboFilterField2.Items.Clear();
					try
					{
						txtWidth1.Value = txtWidth2.Value = DBNull.Value;
					}
					catch{}
					// hide Multil selection checkbox
					chkMultiSelection.Visible = false;
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
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
				}
			}
			catch (Exception ex)
			{
				// displays the error message.
				PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxIcon.Error);
				// log message.
				try
				{
					Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
				}
				catch
				{
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
				}
			}
		}

		
		
		private void ReportParameter_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".ReportParameter_Closing()";
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
		#endregion

		#region Private methods
				
		/// <summary>
		/// Thachnn: 24/Oct/2005
		/// Build the fields tree
		/// 
		/// </summary>
		/// <param name="parrSource">ArrayList contain ParameterVO objects</param>
		private void BuildTree(ArrayList parrSource)
		{
			try
			{
				this.tvwParameterList.Nodes.Clear();
				foreach (sys_ReportParaVO voReportPara in parrSource)
				{
					TreeNode objParaNode = new TreeNode(voReportPara.ParaName);
					objParaNode.Tag = voReportPara;
					tvwParameterList.Nodes.Add(objParaNode);
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
		///       This method use to reset value of all controls in form to default value
		///    </Description>
		///    <Inputs>
		///       Combo Box
		///    </Inputs>
		///    <Outputs>
		///       N/A
		///    </Outputs>
		///    <Returns>
		///       N/A
		///    </Returns>
		///    <Authors>
		///       DungLA
		///    </Authors>
		///    <History>
		///       05-Jan-2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private void ResetForm()
		{
			try
			{
				EnableEditParameterInfoControls();
				txtParameterName.Text = string.Empty;
				txtCaption.Text = string.Empty;
				cboDataType.SelectedIndex = -1;
				txtWidth.Value = DBNull.Value;
				chkOptional.Checked = false;
				chkTagValue.Checked = false;
				chkSameRow.Checked = false;
				txtDefaultValue.Text = string.Empty;
				txtItems.Text = string.Empty;
				txtWidth1.Value = DBNull.Value;
				txtWidth2.Value = DBNull.Value;
				txtSQL.Text = string.Empty;
				txtWhereClause.Text = string.Empty;
				cboFromTable.SelectedIndex = 0;
				//cboFromField.SelectedIndex = 0;

				btnAdd.Enabled = false;
				btnSave.Enabled = true;
				btnEdit.Enabled = false;
				btnDelete.Enabled = false;
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
		///       Reset all control on right panel to empty.
		///    </Description>
		///    <Inputs>
		
		///    </Inputs>
		///    <Outputs>
		///       N/A
		///    </Outputs>
		///    <Returns>
		///       N/A
		///    </Returns>
		///    <Authors>
		///       Thachnn
		///    </Authors>
		///    <History>
		///       24/Oct-2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private void ClearParameterInfoControl()
		{
			try
			{				
				txtParameterName.Text = string.Empty;
				txtCaption.Text = string.Empty;
				cboDataType.SelectedItem = DBNull.Value;
				txtWidth.Value = DBNull.Value;
				chkOptional.Checked = false;
				chkTagValue.Checked = false;
				chkSameRow.Checked = false;
				txtDefaultValue.Text = string.Empty;
				txtItems.Text = string.Empty;
				txtWidth1.Value = DBNull.Value;
				txtWidth2.Value = DBNull.Value;
				cboFromTable.SelectedItem = DBNull.Value;
				cboFromField.SelectedItem = DBNull.Value;
				cboFilterField1.SelectedItem = DBNull.Value;
				cboFilterField2.SelectedItem = DBNull.Value;
				//txtSQL.Text = string.Empty;
				//txtWhereClause.Text = string.Empty;
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
		///       This method use to enable all editable controls
		///    </Description>
		///    <Inputs>
		///       
		///    </Inputs>
		///    <Outputs>
		///       N/A
		///    </Outputs>
		///    <Returns>
		///       N/A
		///    </Returns>
		///    <Authors>
		///       DungLA
		///    </Authors>
		///    <History>
		///       05-Jan-2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private void EnableEditParameterInfoControls()
		{
			try
			{
				foreach (Control objControl in grpDetails.Controls)
				{
					if ((objControl is TextBox) || (objControl is ComboBox)	|| (objControl is CheckBox))
					{
						objControl.Enabled = true;
					}
				}
			txtParameterName.Enabled = true;
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
		///       Thachnn: Oct 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private bool SaveData()
		{
			//HACKED: Thachnn: store the old ParamName in this string, use to update Parameter
			string strOldParamName = string.Empty;
			try	// if Action = Add , so mvoSelectedPara will be null, but in that case, we also don't use strOldParamName
			{
				strOldParamName = mvoSelectedPara.ParaName;
			}
			catch{}			
			// ENDHACKED:

			try
			{
				#region prepare data

				// prepare data
				if (mFormAction == EnumAction.Add)
					mvoSelectedPara = new sys_ReportParaVO();
				mvoSelectedPara.ReportID = this.strReportID;
				mvoSelectedPara.ParaName = txtParameterName.Text.Trim();
				mvoSelectedPara.ParaCaption = txtCaption.Text.Trim();
				IDictionaryEnumerator objEnum = htDataType.GetEnumerator();
				while (objEnum.MoveNext())
				{
					if (objEnum.Value.Equals(cboDataType.SelectedItem))
					{
						mvoSelectedPara.DataType = (int) objEnum.Key;
						break;
					}
				}
				// convert width back to int
				mvoSelectedPara.Width = int.Parse(txtWidth.Value.ToString(), NumberStyles.AllowThousands);
				mvoSelectedPara.Optional = chkOptional.Checked;
				mvoSelectedPara.TagValue = chkTagValue.Checked;
				mvoSelectedPara.SameRow = chkSameRow.Checked;
				mvoSelectedPara.DefaultValue = txtDefaultValue.Text.Trim();
				mvoSelectedPara.Items = txtItems.Text.Trim();
				try
				{
					mvoSelectedPara.FromTable = cboFromTable.SelectedItem.ToString();
				}
				catch{}
				try
				{
					mvoSelectedPara.FromField = cboFromField.SelectedItem.ToString();
				}
				catch{}
				try
				{
					mvoSelectedPara.FilterField1 = cboFilterField1.SelectedItem.ToString();
				}
				catch{}
				try
				{
					mvoSelectedPara.FilterField2 = cboFilterField2.SelectedItem.ToString();
				}
				catch{}
				// convert width back to int
				/// HACKED: Thachnn: fix bug when user not input for width
				try
				{				
					mvoSelectedPara.FilterField1Width = int.Parse(txtWidth1.Value.ToString(), NumberStyles.AllowThousands);
				}
				catch
				{
					mvoSelectedPara.FilterField1Width = 0;
				}
				try
				{
					mvoSelectedPara.FilterField2Width = int.Parse(txtWidth2.Value.ToString(), NumberStyles.AllowThousands);
				}
				catch
				{
					mvoSelectedPara.FilterField2Width = 0;
				}
				mvoSelectedPara.SQLCLause = txtSQL.Text.Trim();
				mvoSelectedPara.WhereClause = txtWhereClause.Text.Trim();
				// for select mutil rows
				if (chkMultiSelection.Visible)
					mvoSelectedPara.MultiSelection = chkMultiSelection.Checked;

				#endregion

				#region Save the Parameter

				if (this.mFormAction == EnumAction.Add)
				{
					// get current max order
					// increase one for new Para
					mvoSelectedPara.ParaOrder = boReportParameter.GetMaxParaOrder(mvoSelectedPara.ReportID) + 1;
					boReportParameter.Add(mvoSelectedPara);
				}
				else
				{					
					boReportParameter.Update(mvoSelectedPara, strOldParamName);
				}

				#endregion
				
				return true;
			}
			catch (PCSException ex)
			{
				throw ex;
			}
			catch (NullReferenceException)
			{
				return false;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		//**************************************************************************              
		///    <Description>
		///       enable/disable button/textbox based on form action and security
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
		///    08-Mar-2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private void SwitchFormMode()
		{
			try
			{
				if (mFormAction == EnumAction.Add)
				{
					EnableEditParameterInfoControls();

					grpDetails.Enabled = true;					
					btnAdd.Enabled = false;
					btnSave.Enabled = true;
					btnEdit.Enabled = false;
					btnDelete.Enabled = false;
				}
				else if (mFormAction == EnumAction.Edit)
				{
					grpDetails.Enabled = true;					
					btnAdd.Enabled = false;					
					btnSave.Enabled = true;
					btnEdit.Enabled = false;
					btnDelete.Enabled = false;

					EnableEditParameterInfoControls();
//					foreach (Control objCtrl in grpDetails.Controls)
//					{						
//						objCtrl.Enabled = true;
//					}
				}
				else if (mFormAction == EnumAction.Default)
				{
					grpDetails.Enabled = false;
					btnAdd.Enabled = true;					
					btnSave.Enabled = false;
					btnEdit.Enabled = false;
					btnDelete.Enabled = false;

					if(tvwParameterList.SelectedNode != null)
					{
						if (tvwParameterList.SelectedNode.PrevVisibleNode == null || tvwParameterList.SelectedNode.PrevNode == null)
								btnMoveUp.Enabled = false;
							else
								btnMoveUp.Enabled = true;

						if (tvwParameterList.SelectedNode.NextVisibleNode == null || tvwParameterList.SelectedNode.NextNode == null)
							btnMoveDown.Enabled = false;
							else
								btnMoveDown.Enabled = true;
					}
					else
						btnMoveUp.Enabled = btnMoveDown.Enabled = false;
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
		///       Convert data form Dataset to ArrayList
		///    </Description>
		///    <Inputs>
		///               
		///    </Inputs>
		///    <Outputs>
		///       
		///    </Outputs>
		///    <Returns>
		///       return true if correct data, return false if incorrect 
		///    </Returns>
		///    <Authors>
		///       Hoang Trung Son - iSphere Software
		///    </Authors>
		///    <History>
		///       15-Dec-2004
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private ArrayList GetArrayFromDataSet(DataSet pdstDataSet,string pstrTableName)
		{
			const string METHOD_NAME = THIS + ".GetArrayFromDataSet()";
			ArrayList arrList = new ArrayList();
			try
			{				
				foreach (DataRow row in pdstDataSet.Tables[pstrTableName].Rows)
				{
					arrList.Add(row);
				}
			}
			catch (PCSException ex)
			{
				// displays the error message.
				PCSMessageBox.Show(ex.mCode, MessageBoxIcon.Error);
				// log message.
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
				// displays the error message.
				PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxIcon.Error);
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
			return arrList;
		}

		/// <summary>
		/// Thachnn: 15/oct/2005
		/// Load data from value object to control in form
				/// </summary>
		private void LayoutOnFormValueInParameterVoObject()
		{
			// load data to form
			if (mvoSelectedPara != null)
			{	
				// fill to the form with loaded data
				txtParameterName.Text = mvoSelectedPara.ParaName;
				txtCaption.Text = mvoSelectedPara.ParaCaption;
				
				IDictionaryEnumerator objEnum = htDataType.GetEnumerator();
				while (objEnum.MoveNext())
				{
					if (objEnum.Key.Equals(mvoSelectedPara.DataType))
					{
						cboDataType.SelectedItem = objEnum.Value;
						break;
					}
				}
				
				txtWidth.Value = mvoSelectedPara.Width;
				chkOptional.Checked = mvoSelectedPara.Optional;
				chkTagValue.Checked = mvoSelectedPara.TagValue;
				chkSameRow.Checked = mvoSelectedPara.SameRow;
				txtDefaultValue.Text = mvoSelectedPara.DefaultValue;
				txtItems.Text = mvoSelectedPara.Items;
				cboFromTable.SelectedItem = mvoSelectedPara.FromTable;
				cboFromField.SelectedItem = mvoSelectedPara.FromField;
				cboFilterField1.SelectedItem = mvoSelectedPara.FilterField1;
				txtWidth1.Value = mvoSelectedPara.FilterField1Width;
				cboFilterField2.SelectedItem = mvoSelectedPara.FilterField2;
				txtWidth2.Value = mvoSelectedPara.FilterField2Width;
				txtSQL.Text = mvoSelectedPara.SQLCLause;
				txtWhereClause.Text = mvoSelectedPara.WhereClause;
				chkMultiSelection.Checked = mvoSelectedPara.MultiSelection;
			}
		}


		/// <summary>
		/// Enable button after a node is selected
		/// </summary>
		private void EnableButton()
		{
			btnEdit.Enabled = (tvwParameterList.SelectedNode != null);
			btnDelete.Enabled = (tvwParameterList.SelectedNode != null);
		}

		#endregion

		private void tvwParameterList_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e)
		{
			const string METHOD_NAME = THIS + ".tvwParameterList_AfterSelect()";	
			
			try
			{
				mFormAction = EnumAction.Default;
				mvoSelectedPara = (sys_ReportParaVO)tvwParameterList.SelectedNode.Tag;
				// bind all properties
				LayoutOnFormValueInParameterVoObject();
				
				if (tvwParameterList.SelectedNode.PrevVisibleNode == null || tvwParameterList.SelectedNode.PrevNode == null)
					btnMoveUp.Enabled = false;
				else
					btnMoveUp.Enabled = true;

				if (tvwParameterList.SelectedNode.NextVisibleNode == null || tvwParameterList.SelectedNode.NextNode == null)
					btnMoveDown.Enabled = false;
				else
					btnMoveDown.Enabled = true;
				SwitchFormMode();
				EnableButton();
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

		private void tvwParameterList_DoubleClick(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".tvwParameterList_DoubleClick()";
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

		private void cboFilterField2_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".cboFilterField2_SelectedIndexChanged()";
			try
			{
				if (cboFilterField2.SelectedIndex > 0)
				{
					lblWidth2.ForeColor = Color.Maroon;
				}
				else
				{
					lblWidth2.ResetForeColor();
					txtWidth2.Value = DBNull.Value;
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
}