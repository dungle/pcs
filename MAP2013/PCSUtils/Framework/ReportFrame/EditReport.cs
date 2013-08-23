using System;
using System.Collections;
using System.Drawing;
using System.IO;
using System.ComponentModel;
using System.Windows.Forms;
using PCSComUtils.Common;
using PCSComUtils.Framework.ReportFrame.BO;
using PCSComUtils.Framework.ReportFrame.DS;
using PCSComUtils.PCSExc;
using PCSUtils.Log;
using PCSUtils.Utils;
using C1.C1Report;

namespace PCSUtils.Framework.ReportFrame
{
	/// <summary>
	/// Summary description for frmEditReport.
	/// </summary>
	public class EditReport : Form
	{
		#region Components
		private Button btnHelp;
		private Button btnEdit;
		private Button btnSave;
		private Label lblReportCode;
		private TextBox txtReportCode;
		private TextBox txtReportName;
		private Label lblReportName;
		private TextBox txtCommand;
		private Label lblCommand;
		private TextBox txtReportFile;
		private Label lblReportFile;
		private TextBox txtISOCode;
		private Label lblReportType;
		private ComboBox cboReportType;
		private Button btnParameter;
		private Button btnDrillDownReport;
		private Button btnPageSetup;
		private Button btnClose;
		private TextBox txtDescription;
		private Label lblDescription;
		private Label lblGroupID;
		private Button btnDelete;
		private Label lblISO_Code;
		private GroupBox grbReportDetail;
		private Button btnFile;
		private System.Windows.Forms.OpenFileDialog dlgOpenFile;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private Container components = null;

		#endregion

		private const string THIS = "PCSUtils.Framework.ReportFrame.EditReport";
		private const string DLL_FILTER = "DLL File|*.dll";		
		private const string CSHARP_FILTER = "C# File|*.cs";
		private const string CUSTOM_REPORT_FILTER = "ComponentOne XML Report File|*.xml";

		private string mFormCaption = null;
		private bool blnSavedReport = true;
		private bool blnEditedReport = false;
		private bool blnReportFileChanged = false;
		private bool blnTemplateFileChanged = false;

		private EnumAction mEnumType;
		private sys_ReportVO mvoReport;
		private string mGroupID;
		private string mReportID;
		private int intReportOrder;
		string mstrReportDefFolder = Application.StartupPath + "\\" + Constants.REPORT_DEFINITION_STORE_LOCATION;
		//private string mstrReportFilePath = "";
		//private string mstrXMLTemplateFilePath = "";


		private System.Windows.Forms.Button btnFieldProperties;
		private System.Windows.Forms.Button btnFieldGroup;		
		private System.Windows.Forms.CheckBox chkUseTemplate;
		private System.Windows.Forms.TextBox txtTemplateFile;
		private System.Windows.Forms.Button btnTemplateFile;
		private System.Windows.Forms.Label lblTemplateFile;
		private System.Windows.Forms.OpenFileDialog dlgOpenTemplateFile;
		private System.Windows.Forms.Label lblDefaultParameterFont;
		private System.Windows.Forms.Label lblDefaultReportHeaderFont;
		private System.Windows.Forms.Label lblDefaultPageHeaderFont;
		private System.Windows.Forms.Label lblDefaultGroupByFont;
		private System.Windows.Forms.Label lblDefaultPageFooterFont;
		private System.Windows.Forms.Label lblDefaultReportFooterFont;
		private System.Windows.Forms.Label lblDefaultDetailFont;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox txtReportNameVN;
		private System.Windows.Forms.TextBox txtReportNameJP;	
		private System.Windows.Forms.Label lblAddNewReport;		


		#region Properties
		/// <summary>
		/// Report Code
		/// </summary>
		public string ReportID
		{
			get { return this.mReportID; }
			set { this.mReportID = value; }
		}

		/// <summary>
		/// Group Code
		/// </summary>
		public string GroupID
		{
			get { return this.mGroupID; }
			set { this.mGroupID = value; }
		}

		/// <summary>
		/// Form action
		/// </summary>
		public EnumAction EnumType
		{
			get { return this.mEnumType; }
			set { this.mEnumType = value; }
		}

		/// <summary>
		/// Edit Report Object
		/// </summary>
		public sys_ReportVO VOReport
		{
			get { return this.mvoReport; }
			set { this.mvoReport = value; }
		}

		/// <summary>
		/// Report Order in Group
		/// </summary>
		public int ReportOrder
		{
			get { return intReportOrder; }
			set { intReportOrder = value; }
		}

		#endregion

		#region Contructor
		public EditReport()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
		}

		public EditReport(object pobjEditObject)
		{
			const string METHOD_NAME = THIS + ".Constructor(object)";			
			InitializeComponent();

			try
			{
				mvoReport = (sys_ReportVO)(pobjEditObject);
			}
			catch (PCSException ex)
			{
				// displays the error message.
				PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxIcon.Error);

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
		}
		#endregion

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(EditReport));
			this.lblReportCode = new System.Windows.Forms.Label();
			this.txtReportCode = new System.Windows.Forms.TextBox();
			this.txtReportName = new System.Windows.Forms.TextBox();
			this.lblReportName = new System.Windows.Forms.Label();
			this.txtCommand = new System.Windows.Forms.TextBox();
			this.lblCommand = new System.Windows.Forms.Label();
			this.txtReportFile = new System.Windows.Forms.TextBox();
			this.lblReportFile = new System.Windows.Forms.Label();
			this.txtISOCode = new System.Windows.Forms.TextBox();
			this.lblISO_Code = new System.Windows.Forms.Label();
			this.btnFile = new System.Windows.Forms.Button();
			this.lblReportType = new System.Windows.Forms.Label();
			this.cboReportType = new System.Windows.Forms.ComboBox();
			this.btnParameter = new System.Windows.Forms.Button();
			this.btnDrillDownReport = new System.Windows.Forms.Button();
			this.btnPageSetup = new System.Windows.Forms.Button();
			this.grbReportDetail = new System.Windows.Forms.GroupBox();
			this.btnFieldProperties = new System.Windows.Forms.Button();
			this.btnFieldGroup = new System.Windows.Forms.Button();
			this.btnHelp = new System.Windows.Forms.Button();
			this.btnEdit = new System.Windows.Forms.Button();
			this.btnSave = new System.Windows.Forms.Button();
			this.btnDelete = new System.Windows.Forms.Button();
			this.btnClose = new System.Windows.Forms.Button();
			this.txtDescription = new System.Windows.Forms.TextBox();
			this.lblDescription = new System.Windows.Forms.Label();
			this.lblGroupID = new System.Windows.Forms.Label();
			this.dlgOpenFile = new System.Windows.Forms.OpenFileDialog();
			this.txtTemplateFile = new System.Windows.Forms.TextBox();
			this.btnTemplateFile = new System.Windows.Forms.Button();
			this.lblTemplateFile = new System.Windows.Forms.Label();
			this.chkUseTemplate = new System.Windows.Forms.CheckBox();
			this.dlgOpenTemplateFile = new System.Windows.Forms.OpenFileDialog();
			this.lblAddNewReport = new System.Windows.Forms.Label();
			this.lblDefaultParameterFont = new System.Windows.Forms.Label();
			this.lblDefaultReportHeaderFont = new System.Windows.Forms.Label();
			this.lblDefaultPageHeaderFont = new System.Windows.Forms.Label();
			this.lblDefaultGroupByFont = new System.Windows.Forms.Label();
			this.lblDefaultPageFooterFont = new System.Windows.Forms.Label();
			this.lblDefaultReportFooterFont = new System.Windows.Forms.Label();
			this.lblDefaultDetailFont = new System.Windows.Forms.Label();
			this.txtReportNameVN = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.txtReportNameJP = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.grbReportDetail.SuspendLayout();
			this.SuspendLayout();
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
			this.txtReportCode.ReadOnly = true;
			this.txtReportCode.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("txtReportCode.RightToLeft")));
			this.txtReportCode.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("txtReportCode.ScrollBars")));
			this.txtReportCode.Size = ((System.Drawing.Size)(resources.GetObject("txtReportCode.Size")));
			this.txtReportCode.TabIndex = ((int)(resources.GetObject("txtReportCode.TabIndex")));
			this.txtReportCode.Text = resources.GetString("txtReportCode.Text");
			this.txtReportCode.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("txtReportCode.TextAlign")));
			this.txtReportCode.Visible = ((bool)(resources.GetObject("txtReportCode.Visible")));
			this.txtReportCode.WordWrap = ((bool)(resources.GetObject("txtReportCode.WordWrap")));
			this.txtReportCode.Leave += new System.EventHandler(this.OnLeaveControl);
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
			this.txtReportName.ForeColor = System.Drawing.SystemColors.WindowText;
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
			this.txtReportName.Leave += new System.EventHandler(this.OnLeaveControl);
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
			this.lblReportName.ForeColor = System.Drawing.Color.Maroon;
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
			// txtCommand
			// 
			this.txtCommand.AccessibleDescription = resources.GetString("txtCommand.AccessibleDescription");
			this.txtCommand.AccessibleName = resources.GetString("txtCommand.AccessibleName");
			this.txtCommand.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("txtCommand.Anchor")));
			this.txtCommand.AutoSize = ((bool)(resources.GetObject("txtCommand.AutoSize")));
			this.txtCommand.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("txtCommand.BackgroundImage")));
			this.txtCommand.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("txtCommand.Dock")));
			this.txtCommand.Enabled = ((bool)(resources.GetObject("txtCommand.Enabled")));
			this.txtCommand.Font = ((System.Drawing.Font)(resources.GetObject("txtCommand.Font")));
			this.txtCommand.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("txtCommand.ImeMode")));
			this.txtCommand.Location = ((System.Drawing.Point)(resources.GetObject("txtCommand.Location")));
			this.txtCommand.MaxLength = ((int)(resources.GetObject("txtCommand.MaxLength")));
			this.txtCommand.Multiline = ((bool)(resources.GetObject("txtCommand.Multiline")));
			this.txtCommand.Name = "txtCommand";
			this.txtCommand.PasswordChar = ((char)(resources.GetObject("txtCommand.PasswordChar")));
			this.txtCommand.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("txtCommand.RightToLeft")));
			this.txtCommand.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("txtCommand.ScrollBars")));
			this.txtCommand.Size = ((System.Drawing.Size)(resources.GetObject("txtCommand.Size")));
			this.txtCommand.TabIndex = ((int)(resources.GetObject("txtCommand.TabIndex")));
			this.txtCommand.Text = resources.GetString("txtCommand.Text");
			this.txtCommand.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("txtCommand.TextAlign")));
			this.txtCommand.Visible = ((bool)(resources.GetObject("txtCommand.Visible")));
			this.txtCommand.WordWrap = ((bool)(resources.GetObject("txtCommand.WordWrap")));
			this.txtCommand.Leave += new System.EventHandler(this.OnLeaveControl);
			// 
			// lblCommand
			// 
			this.lblCommand.AccessibleDescription = resources.GetString("lblCommand.AccessibleDescription");
			this.lblCommand.AccessibleName = resources.GetString("lblCommand.AccessibleName");
			this.lblCommand.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblCommand.Anchor")));
			this.lblCommand.AutoSize = ((bool)(resources.GetObject("lblCommand.AutoSize")));
			this.lblCommand.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblCommand.Dock")));
			this.lblCommand.Enabled = ((bool)(resources.GetObject("lblCommand.Enabled")));
			this.lblCommand.Font = ((System.Drawing.Font)(resources.GetObject("lblCommand.Font")));
			this.lblCommand.Image = ((System.Drawing.Image)(resources.GetObject("lblCommand.Image")));
			this.lblCommand.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblCommand.ImageAlign")));
			this.lblCommand.ImageIndex = ((int)(resources.GetObject("lblCommand.ImageIndex")));
			this.lblCommand.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblCommand.ImeMode")));
			this.lblCommand.Location = ((System.Drawing.Point)(resources.GetObject("lblCommand.Location")));
			this.lblCommand.Name = "lblCommand";
			this.lblCommand.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblCommand.RightToLeft")));
			this.lblCommand.Size = ((System.Drawing.Size)(resources.GetObject("lblCommand.Size")));
			this.lblCommand.TabIndex = ((int)(resources.GetObject("lblCommand.TabIndex")));
			this.lblCommand.Text = resources.GetString("lblCommand.Text");
			this.lblCommand.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblCommand.TextAlign")));
			this.lblCommand.Visible = ((bool)(resources.GetObject("lblCommand.Visible")));
			// 
			// txtReportFile
			// 
			this.txtReportFile.AccessibleDescription = resources.GetString("txtReportFile.AccessibleDescription");
			this.txtReportFile.AccessibleName = resources.GetString("txtReportFile.AccessibleName");
			this.txtReportFile.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("txtReportFile.Anchor")));
			this.txtReportFile.AutoSize = ((bool)(resources.GetObject("txtReportFile.AutoSize")));
			this.txtReportFile.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("txtReportFile.BackgroundImage")));
			this.txtReportFile.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("txtReportFile.Dock")));
			this.txtReportFile.Enabled = ((bool)(resources.GetObject("txtReportFile.Enabled")));
			this.txtReportFile.Font = ((System.Drawing.Font)(resources.GetObject("txtReportFile.Font")));
			this.txtReportFile.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("txtReportFile.ImeMode")));
			this.txtReportFile.Location = ((System.Drawing.Point)(resources.GetObject("txtReportFile.Location")));
			this.txtReportFile.MaxLength = ((int)(resources.GetObject("txtReportFile.MaxLength")));
			this.txtReportFile.Multiline = ((bool)(resources.GetObject("txtReportFile.Multiline")));
			this.txtReportFile.Name = "txtReportFile";
			this.txtReportFile.PasswordChar = ((char)(resources.GetObject("txtReportFile.PasswordChar")));
			this.txtReportFile.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("txtReportFile.RightToLeft")));
			this.txtReportFile.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("txtReportFile.ScrollBars")));
			this.txtReportFile.Size = ((System.Drawing.Size)(resources.GetObject("txtReportFile.Size")));
			this.txtReportFile.TabIndex = ((int)(resources.GetObject("txtReportFile.TabIndex")));
			this.txtReportFile.Text = resources.GetString("txtReportFile.Text");
			this.txtReportFile.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("txtReportFile.TextAlign")));
			this.txtReportFile.Visible = ((bool)(resources.GetObject("txtReportFile.Visible")));
			this.txtReportFile.WordWrap = ((bool)(resources.GetObject("txtReportFile.WordWrap")));
			this.txtReportFile.EnabledChanged += new System.EventHandler(this.txtReportFile_EnabledChanged);
			this.txtReportFile.TextChanged += new System.EventHandler(this.txtReportFile_TextChanged);
			this.txtReportFile.Leave += new System.EventHandler(this.OnLeaveControl);
			// 
			// lblReportFile
			// 
			this.lblReportFile.AccessibleDescription = resources.GetString("lblReportFile.AccessibleDescription");
			this.lblReportFile.AccessibleName = resources.GetString("lblReportFile.AccessibleName");
			this.lblReportFile.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblReportFile.Anchor")));
			this.lblReportFile.AutoSize = ((bool)(resources.GetObject("lblReportFile.AutoSize")));
			this.lblReportFile.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblReportFile.Dock")));
			this.lblReportFile.Enabled = ((bool)(resources.GetObject("lblReportFile.Enabled")));
			this.lblReportFile.Font = ((System.Drawing.Font)(resources.GetObject("lblReportFile.Font")));
			this.lblReportFile.Image = ((System.Drawing.Image)(resources.GetObject("lblReportFile.Image")));
			this.lblReportFile.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblReportFile.ImageAlign")));
			this.lblReportFile.ImageIndex = ((int)(resources.GetObject("lblReportFile.ImageIndex")));
			this.lblReportFile.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblReportFile.ImeMode")));
			this.lblReportFile.Location = ((System.Drawing.Point)(resources.GetObject("lblReportFile.Location")));
			this.lblReportFile.Name = "lblReportFile";
			this.lblReportFile.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblReportFile.RightToLeft")));
			this.lblReportFile.Size = ((System.Drawing.Size)(resources.GetObject("lblReportFile.Size")));
			this.lblReportFile.TabIndex = ((int)(resources.GetObject("lblReportFile.TabIndex")));
			this.lblReportFile.Text = resources.GetString("lblReportFile.Text");
			this.lblReportFile.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblReportFile.TextAlign")));
			this.lblReportFile.Visible = ((bool)(resources.GetObject("lblReportFile.Visible")));
			// 
			// txtISOCode
			// 
			this.txtISOCode.AccessibleDescription = resources.GetString("txtISOCode.AccessibleDescription");
			this.txtISOCode.AccessibleName = resources.GetString("txtISOCode.AccessibleName");
			this.txtISOCode.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("txtISOCode.Anchor")));
			this.txtISOCode.AutoSize = ((bool)(resources.GetObject("txtISOCode.AutoSize")));
			this.txtISOCode.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("txtISOCode.BackgroundImage")));
			this.txtISOCode.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("txtISOCode.Dock")));
			this.txtISOCode.Enabled = ((bool)(resources.GetObject("txtISOCode.Enabled")));
			this.txtISOCode.Font = ((System.Drawing.Font)(resources.GetObject("txtISOCode.Font")));
			this.txtISOCode.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("txtISOCode.ImeMode")));
			this.txtISOCode.Location = ((System.Drawing.Point)(resources.GetObject("txtISOCode.Location")));
			this.txtISOCode.MaxLength = ((int)(resources.GetObject("txtISOCode.MaxLength")));
			this.txtISOCode.Multiline = ((bool)(resources.GetObject("txtISOCode.Multiline")));
			this.txtISOCode.Name = "txtISOCode";
			this.txtISOCode.PasswordChar = ((char)(resources.GetObject("txtISOCode.PasswordChar")));
			this.txtISOCode.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("txtISOCode.RightToLeft")));
			this.txtISOCode.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("txtISOCode.ScrollBars")));
			this.txtISOCode.Size = ((System.Drawing.Size)(resources.GetObject("txtISOCode.Size")));
			this.txtISOCode.TabIndex = ((int)(resources.GetObject("txtISOCode.TabIndex")));
			this.txtISOCode.Text = resources.GetString("txtISOCode.Text");
			this.txtISOCode.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("txtISOCode.TextAlign")));
			this.txtISOCode.Visible = ((bool)(resources.GetObject("txtISOCode.Visible")));
			this.txtISOCode.WordWrap = ((bool)(resources.GetObject("txtISOCode.WordWrap")));
			this.txtISOCode.Leave += new System.EventHandler(this.OnLeaveControl);
			// 
			// lblISO_Code
			// 
			this.lblISO_Code.AccessibleDescription = resources.GetString("lblISO_Code.AccessibleDescription");
			this.lblISO_Code.AccessibleName = resources.GetString("lblISO_Code.AccessibleName");
			this.lblISO_Code.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblISO_Code.Anchor")));
			this.lblISO_Code.AutoSize = ((bool)(resources.GetObject("lblISO_Code.AutoSize")));
			this.lblISO_Code.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblISO_Code.Dock")));
			this.lblISO_Code.Enabled = ((bool)(resources.GetObject("lblISO_Code.Enabled")));
			this.lblISO_Code.Font = ((System.Drawing.Font)(resources.GetObject("lblISO_Code.Font")));
			this.lblISO_Code.Image = ((System.Drawing.Image)(resources.GetObject("lblISO_Code.Image")));
			this.lblISO_Code.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblISO_Code.ImageAlign")));
			this.lblISO_Code.ImageIndex = ((int)(resources.GetObject("lblISO_Code.ImageIndex")));
			this.lblISO_Code.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblISO_Code.ImeMode")));
			this.lblISO_Code.Location = ((System.Drawing.Point)(resources.GetObject("lblISO_Code.Location")));
			this.lblISO_Code.Name = "lblISO_Code";
			this.lblISO_Code.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblISO_Code.RightToLeft")));
			this.lblISO_Code.Size = ((System.Drawing.Size)(resources.GetObject("lblISO_Code.Size")));
			this.lblISO_Code.TabIndex = ((int)(resources.GetObject("lblISO_Code.TabIndex")));
			this.lblISO_Code.Text = resources.GetString("lblISO_Code.Text");
			this.lblISO_Code.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblISO_Code.TextAlign")));
			this.lblISO_Code.Visible = ((bool)(resources.GetObject("lblISO_Code.Visible")));
			// 
			// btnFile
			// 
			this.btnFile.AccessibleDescription = resources.GetString("btnFile.AccessibleDescription");
			this.btnFile.AccessibleName = resources.GetString("btnFile.AccessibleName");
			this.btnFile.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnFile.Anchor")));
			this.btnFile.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnFile.BackgroundImage")));
			this.btnFile.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnFile.Dock")));
			this.btnFile.Enabled = ((bool)(resources.GetObject("btnFile.Enabled")));
			this.btnFile.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnFile.FlatStyle")));
			this.btnFile.Font = ((System.Drawing.Font)(resources.GetObject("btnFile.Font")));
			this.btnFile.Image = ((System.Drawing.Image)(resources.GetObject("btnFile.Image")));
			this.btnFile.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnFile.ImageAlign")));
			this.btnFile.ImageIndex = ((int)(resources.GetObject("btnFile.ImageIndex")));
			this.btnFile.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnFile.ImeMode")));
			this.btnFile.Location = ((System.Drawing.Point)(resources.GetObject("btnFile.Location")));
			this.btnFile.Name = "btnFile";
			this.btnFile.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnFile.RightToLeft")));
			this.btnFile.Size = ((System.Drawing.Size)(resources.GetObject("btnFile.Size")));
			this.btnFile.TabIndex = ((int)(resources.GetObject("btnFile.TabIndex")));
			this.btnFile.Text = resources.GetString("btnFile.Text");
			this.btnFile.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnFile.TextAlign")));
			this.btnFile.Visible = ((bool)(resources.GetObject("btnFile.Visible")));
			this.btnFile.Click += new System.EventHandler(this.btnFile_Click);
			this.btnFile.EnabledChanged += new System.EventHandler(this.btnFile_EnabledChanged);
			// 
			// lblReportType
			// 
			this.lblReportType.AccessibleDescription = resources.GetString("lblReportType.AccessibleDescription");
			this.lblReportType.AccessibleName = resources.GetString("lblReportType.AccessibleName");
			this.lblReportType.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblReportType.Anchor")));
			this.lblReportType.AutoSize = ((bool)(resources.GetObject("lblReportType.AutoSize")));
			this.lblReportType.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblReportType.Dock")));
			this.lblReportType.Enabled = ((bool)(resources.GetObject("lblReportType.Enabled")));
			this.lblReportType.Font = ((System.Drawing.Font)(resources.GetObject("lblReportType.Font")));
			this.lblReportType.ForeColor = System.Drawing.Color.Maroon;
			this.lblReportType.Image = ((System.Drawing.Image)(resources.GetObject("lblReportType.Image")));
			this.lblReportType.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblReportType.ImageAlign")));
			this.lblReportType.ImageIndex = ((int)(resources.GetObject("lblReportType.ImageIndex")));
			this.lblReportType.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblReportType.ImeMode")));
			this.lblReportType.Location = ((System.Drawing.Point)(resources.GetObject("lblReportType.Location")));
			this.lblReportType.Name = "lblReportType";
			this.lblReportType.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblReportType.RightToLeft")));
			this.lblReportType.Size = ((System.Drawing.Size)(resources.GetObject("lblReportType.Size")));
			this.lblReportType.TabIndex = ((int)(resources.GetObject("lblReportType.TabIndex")));
			this.lblReportType.Text = resources.GetString("lblReportType.Text");
			this.lblReportType.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblReportType.TextAlign")));
			this.lblReportType.Visible = ((bool)(resources.GetObject("lblReportType.Visible")));
			// 
			// cboReportType
			// 
			this.cboReportType.AccessibleDescription = resources.GetString("cboReportType.AccessibleDescription");
			this.cboReportType.AccessibleName = resources.GetString("cboReportType.AccessibleName");
			this.cboReportType.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("cboReportType.Anchor")));
			this.cboReportType.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cboReportType.BackgroundImage")));
			this.cboReportType.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("cboReportType.Dock")));
			this.cboReportType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboReportType.Enabled = ((bool)(resources.GetObject("cboReportType.Enabled")));
			this.cboReportType.Font = ((System.Drawing.Font)(resources.GetObject("cboReportType.Font")));
			this.cboReportType.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("cboReportType.ImeMode")));
			this.cboReportType.IntegralHeight = ((bool)(resources.GetObject("cboReportType.IntegralHeight")));
			this.cboReportType.ItemHeight = ((int)(resources.GetObject("cboReportType.ItemHeight")));
			this.cboReportType.Items.AddRange(new object[] {
															   resources.GetString("cboReportType.Items"),
															   resources.GetString("cboReportType.Items1"),
															   resources.GetString("cboReportType.Items2"),
															   resources.GetString("cboReportType.Items3")});
			this.cboReportType.Location = ((System.Drawing.Point)(resources.GetObject("cboReportType.Location")));
			this.cboReportType.MaxDropDownItems = ((int)(resources.GetObject("cboReportType.MaxDropDownItems")));
			this.cboReportType.MaxLength = ((int)(resources.GetObject("cboReportType.MaxLength")));
			this.cboReportType.Name = "cboReportType";
			this.cboReportType.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("cboReportType.RightToLeft")));
			this.cboReportType.Size = ((System.Drawing.Size)(resources.GetObject("cboReportType.Size")));
			this.cboReportType.TabIndex = ((int)(resources.GetObject("cboReportType.TabIndex")));
			this.cboReportType.Text = resources.GetString("cboReportType.Text");
			this.cboReportType.Visible = ((bool)(resources.GetObject("cboReportType.Visible")));
			this.cboReportType.SelectedIndexChanged += new System.EventHandler(this.cboReportType_SelectedIndexChanged);
			this.cboReportType.Leave += new System.EventHandler(this.OnLeaveControl);
			// 
			// btnParameter
			// 
			this.btnParameter.AccessibleDescription = resources.GetString("btnParameter.AccessibleDescription");
			this.btnParameter.AccessibleName = resources.GetString("btnParameter.AccessibleName");
			this.btnParameter.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnParameter.Anchor")));
			this.btnParameter.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnParameter.BackgroundImage")));
			this.btnParameter.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnParameter.Dock")));
			this.btnParameter.Enabled = ((bool)(resources.GetObject("btnParameter.Enabled")));
			this.btnParameter.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnParameter.FlatStyle")));
			this.btnParameter.Font = ((System.Drawing.Font)(resources.GetObject("btnParameter.Font")));
			this.btnParameter.Image = ((System.Drawing.Image)(resources.GetObject("btnParameter.Image")));
			this.btnParameter.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnParameter.ImageAlign")));
			this.btnParameter.ImageIndex = ((int)(resources.GetObject("btnParameter.ImageIndex")));
			this.btnParameter.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnParameter.ImeMode")));
			this.btnParameter.Location = ((System.Drawing.Point)(resources.GetObject("btnParameter.Location")));
			this.btnParameter.Name = "btnParameter";
			this.btnParameter.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnParameter.RightToLeft")));
			this.btnParameter.Size = ((System.Drawing.Size)(resources.GetObject("btnParameter.Size")));
			this.btnParameter.TabIndex = ((int)(resources.GetObject("btnParameter.TabIndex")));
			this.btnParameter.Text = resources.GetString("btnParameter.Text");
			this.btnParameter.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnParameter.TextAlign")));
			this.btnParameter.Visible = ((bool)(resources.GetObject("btnParameter.Visible")));
			this.btnParameter.Click += new System.EventHandler(this.btnParameter_Click);
			// 
			// btnDrillDownReport
			// 
			this.btnDrillDownReport.AccessibleDescription = resources.GetString("btnDrillDownReport.AccessibleDescription");
			this.btnDrillDownReport.AccessibleName = resources.GetString("btnDrillDownReport.AccessibleName");
			this.btnDrillDownReport.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnDrillDownReport.Anchor")));
			this.btnDrillDownReport.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnDrillDownReport.BackgroundImage")));
			this.btnDrillDownReport.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnDrillDownReport.Dock")));
			this.btnDrillDownReport.Enabled = ((bool)(resources.GetObject("btnDrillDownReport.Enabled")));
			this.btnDrillDownReport.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnDrillDownReport.FlatStyle")));
			this.btnDrillDownReport.Font = ((System.Drawing.Font)(resources.GetObject("btnDrillDownReport.Font")));
			this.btnDrillDownReport.Image = ((System.Drawing.Image)(resources.GetObject("btnDrillDownReport.Image")));
			this.btnDrillDownReport.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnDrillDownReport.ImageAlign")));
			this.btnDrillDownReport.ImageIndex = ((int)(resources.GetObject("btnDrillDownReport.ImageIndex")));
			this.btnDrillDownReport.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnDrillDownReport.ImeMode")));
			this.btnDrillDownReport.Location = ((System.Drawing.Point)(resources.GetObject("btnDrillDownReport.Location")));
			this.btnDrillDownReport.Name = "btnDrillDownReport";
			this.btnDrillDownReport.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnDrillDownReport.RightToLeft")));
			this.btnDrillDownReport.Size = ((System.Drawing.Size)(resources.GetObject("btnDrillDownReport.Size")));
			this.btnDrillDownReport.TabIndex = ((int)(resources.GetObject("btnDrillDownReport.TabIndex")));
			this.btnDrillDownReport.Text = resources.GetString("btnDrillDownReport.Text");
			this.btnDrillDownReport.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnDrillDownReport.TextAlign")));
			this.btnDrillDownReport.Visible = ((bool)(resources.GetObject("btnDrillDownReport.Visible")));
			this.btnDrillDownReport.Click += new System.EventHandler(this.btnDrillDownReport_Click);
			// 
			// btnPageSetup
			// 
			this.btnPageSetup.AccessibleDescription = resources.GetString("btnPageSetup.AccessibleDescription");
			this.btnPageSetup.AccessibleName = resources.GetString("btnPageSetup.AccessibleName");
			this.btnPageSetup.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnPageSetup.Anchor")));
			this.btnPageSetup.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnPageSetup.BackgroundImage")));
			this.btnPageSetup.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnPageSetup.Dock")));
			this.btnPageSetup.Enabled = ((bool)(resources.GetObject("btnPageSetup.Enabled")));
			this.btnPageSetup.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnPageSetup.FlatStyle")));
			this.btnPageSetup.Font = ((System.Drawing.Font)(resources.GetObject("btnPageSetup.Font")));
			this.btnPageSetup.Image = ((System.Drawing.Image)(resources.GetObject("btnPageSetup.Image")));
			this.btnPageSetup.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnPageSetup.ImageAlign")));
			this.btnPageSetup.ImageIndex = ((int)(resources.GetObject("btnPageSetup.ImageIndex")));
			this.btnPageSetup.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnPageSetup.ImeMode")));
			this.btnPageSetup.Location = ((System.Drawing.Point)(resources.GetObject("btnPageSetup.Location")));
			this.btnPageSetup.Name = "btnPageSetup";
			this.btnPageSetup.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnPageSetup.RightToLeft")));
			this.btnPageSetup.Size = ((System.Drawing.Size)(resources.GetObject("btnPageSetup.Size")));
			this.btnPageSetup.TabIndex = ((int)(resources.GetObject("btnPageSetup.TabIndex")));
			this.btnPageSetup.Text = resources.GetString("btnPageSetup.Text");
			this.btnPageSetup.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnPageSetup.TextAlign")));
			this.btnPageSetup.Visible = ((bool)(resources.GetObject("btnPageSetup.Visible")));
			this.btnPageSetup.Click += new System.EventHandler(this.btnPageSetup_Click);
			// 
			// grbReportDetail
			// 
			this.grbReportDetail.AccessibleDescription = resources.GetString("grbReportDetail.AccessibleDescription");
			this.grbReportDetail.AccessibleName = resources.GetString("grbReportDetail.AccessibleName");
			this.grbReportDetail.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("grbReportDetail.Anchor")));
			this.grbReportDetail.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("grbReportDetail.BackgroundImage")));
			this.grbReportDetail.Controls.Add(this.btnDrillDownReport);
			this.grbReportDetail.Controls.Add(this.btnParameter);
			this.grbReportDetail.Controls.Add(this.btnPageSetup);
			this.grbReportDetail.Controls.Add(this.btnFieldProperties);
			this.grbReportDetail.Controls.Add(this.btnFieldGroup);
			this.grbReportDetail.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("grbReportDetail.Dock")));
			this.grbReportDetail.Enabled = ((bool)(resources.GetObject("grbReportDetail.Enabled")));
			this.grbReportDetail.Font = ((System.Drawing.Font)(resources.GetObject("grbReportDetail.Font")));
			this.grbReportDetail.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("grbReportDetail.ImeMode")));
			this.grbReportDetail.Location = ((System.Drawing.Point)(resources.GetObject("grbReportDetail.Location")));
			this.grbReportDetail.Name = "grbReportDetail";
			this.grbReportDetail.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("grbReportDetail.RightToLeft")));
			this.grbReportDetail.Size = ((System.Drawing.Size)(resources.GetObject("grbReportDetail.Size")));
			this.grbReportDetail.TabIndex = ((int)(resources.GetObject("grbReportDetail.TabIndex")));
			this.grbReportDetail.TabStop = false;
			this.grbReportDetail.Text = resources.GetString("grbReportDetail.Text");
			this.grbReportDetail.Visible = ((bool)(resources.GetObject("grbReportDetail.Visible")));
			// 
			// btnFieldProperties
			// 
			this.btnFieldProperties.AccessibleDescription = resources.GetString("btnFieldProperties.AccessibleDescription");
			this.btnFieldProperties.AccessibleName = resources.GetString("btnFieldProperties.AccessibleName");
			this.btnFieldProperties.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnFieldProperties.Anchor")));
			this.btnFieldProperties.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnFieldProperties.BackgroundImage")));
			this.btnFieldProperties.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnFieldProperties.Dock")));
			this.btnFieldProperties.Enabled = ((bool)(resources.GetObject("btnFieldProperties.Enabled")));
			this.btnFieldProperties.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnFieldProperties.FlatStyle")));
			this.btnFieldProperties.Font = ((System.Drawing.Font)(resources.GetObject("btnFieldProperties.Font")));
			this.btnFieldProperties.Image = ((System.Drawing.Image)(resources.GetObject("btnFieldProperties.Image")));
			this.btnFieldProperties.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnFieldProperties.ImageAlign")));
			this.btnFieldProperties.ImageIndex = ((int)(resources.GetObject("btnFieldProperties.ImageIndex")));
			this.btnFieldProperties.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnFieldProperties.ImeMode")));
			this.btnFieldProperties.Location = ((System.Drawing.Point)(resources.GetObject("btnFieldProperties.Location")));
			this.btnFieldProperties.Name = "btnFieldProperties";
			this.btnFieldProperties.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnFieldProperties.RightToLeft")));
			this.btnFieldProperties.Size = ((System.Drawing.Size)(resources.GetObject("btnFieldProperties.Size")));
			this.btnFieldProperties.TabIndex = ((int)(resources.GetObject("btnFieldProperties.TabIndex")));
			this.btnFieldProperties.Text = resources.GetString("btnFieldProperties.Text");
			this.btnFieldProperties.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnFieldProperties.TextAlign")));
			this.btnFieldProperties.Visible = ((bool)(resources.GetObject("btnFieldProperties.Visible")));
			this.btnFieldProperties.Click += new System.EventHandler(this.btnFieldProperties_Click);
			// 
			// btnFieldGroup
			// 
			this.btnFieldGroup.AccessibleDescription = resources.GetString("btnFieldGroup.AccessibleDescription");
			this.btnFieldGroup.AccessibleName = resources.GetString("btnFieldGroup.AccessibleName");
			this.btnFieldGroup.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnFieldGroup.Anchor")));
			this.btnFieldGroup.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnFieldGroup.BackgroundImage")));
			this.btnFieldGroup.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnFieldGroup.Dock")));
			this.btnFieldGroup.Enabled = ((bool)(resources.GetObject("btnFieldGroup.Enabled")));
			this.btnFieldGroup.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnFieldGroup.FlatStyle")));
			this.btnFieldGroup.Font = ((System.Drawing.Font)(resources.GetObject("btnFieldGroup.Font")));
			this.btnFieldGroup.Image = ((System.Drawing.Image)(resources.GetObject("btnFieldGroup.Image")));
			this.btnFieldGroup.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnFieldGroup.ImageAlign")));
			this.btnFieldGroup.ImageIndex = ((int)(resources.GetObject("btnFieldGroup.ImageIndex")));
			this.btnFieldGroup.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnFieldGroup.ImeMode")));
			this.btnFieldGroup.Location = ((System.Drawing.Point)(resources.GetObject("btnFieldGroup.Location")));
			this.btnFieldGroup.Name = "btnFieldGroup";
			this.btnFieldGroup.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnFieldGroup.RightToLeft")));
			this.btnFieldGroup.Size = ((System.Drawing.Size)(resources.GetObject("btnFieldGroup.Size")));
			this.btnFieldGroup.TabIndex = ((int)(resources.GetObject("btnFieldGroup.TabIndex")));
			this.btnFieldGroup.Text = resources.GetString("btnFieldGroup.Text");
			this.btnFieldGroup.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnFieldGroup.TextAlign")));
			this.btnFieldGroup.Visible = ((bool)(resources.GetObject("btnFieldGroup.Visible")));
			this.btnFieldGroup.Click += new System.EventHandler(this.btnFieldGroup_Click);
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
			this.txtDescription.Leave += new System.EventHandler(this.OnLeaveControl);
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
			// lblGroupID
			// 
			this.lblGroupID.AccessibleDescription = resources.GetString("lblGroupID.AccessibleDescription");
			this.lblGroupID.AccessibleName = resources.GetString("lblGroupID.AccessibleName");
			this.lblGroupID.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblGroupID.Anchor")));
			this.lblGroupID.AutoSize = ((bool)(resources.GetObject("lblGroupID.AutoSize")));
			this.lblGroupID.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblGroupID.Dock")));
			this.lblGroupID.Enabled = ((bool)(resources.GetObject("lblGroupID.Enabled")));
			this.lblGroupID.Font = ((System.Drawing.Font)(resources.GetObject("lblGroupID.Font")));
			this.lblGroupID.Image = ((System.Drawing.Image)(resources.GetObject("lblGroupID.Image")));
			this.lblGroupID.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblGroupID.ImageAlign")));
			this.lblGroupID.ImageIndex = ((int)(resources.GetObject("lblGroupID.ImageIndex")));
			this.lblGroupID.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblGroupID.ImeMode")));
			this.lblGroupID.Location = ((System.Drawing.Point)(resources.GetObject("lblGroupID.Location")));
			this.lblGroupID.Name = "lblGroupID";
			this.lblGroupID.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblGroupID.RightToLeft")));
			this.lblGroupID.Size = ((System.Drawing.Size)(resources.GetObject("lblGroupID.Size")));
			this.lblGroupID.TabIndex = ((int)(resources.GetObject("lblGroupID.TabIndex")));
			this.lblGroupID.Text = resources.GetString("lblGroupID.Text");
			this.lblGroupID.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblGroupID.TextAlign")));
			this.lblGroupID.Visible = ((bool)(resources.GetObject("lblGroupID.Visible")));
			// 
			// dlgOpenFile
			// 
			this.dlgOpenFile.Filter = resources.GetString("dlgOpenFile.Filter");
			this.dlgOpenFile.Title = resources.GetString("dlgOpenFile.Title");
			// 
			// txtTemplateFile
			// 
			this.txtTemplateFile.AccessibleDescription = resources.GetString("txtTemplateFile.AccessibleDescription");
			this.txtTemplateFile.AccessibleName = resources.GetString("txtTemplateFile.AccessibleName");
			this.txtTemplateFile.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("txtTemplateFile.Anchor")));
			this.txtTemplateFile.AutoSize = ((bool)(resources.GetObject("txtTemplateFile.AutoSize")));
			this.txtTemplateFile.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("txtTemplateFile.BackgroundImage")));
			this.txtTemplateFile.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("txtTemplateFile.Dock")));
			this.txtTemplateFile.Enabled = ((bool)(resources.GetObject("txtTemplateFile.Enabled")));
			this.txtTemplateFile.Font = ((System.Drawing.Font)(resources.GetObject("txtTemplateFile.Font")));
			this.txtTemplateFile.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("txtTemplateFile.ImeMode")));
			this.txtTemplateFile.Location = ((System.Drawing.Point)(resources.GetObject("txtTemplateFile.Location")));
			this.txtTemplateFile.MaxLength = ((int)(resources.GetObject("txtTemplateFile.MaxLength")));
			this.txtTemplateFile.Multiline = ((bool)(resources.GetObject("txtTemplateFile.Multiline")));
			this.txtTemplateFile.Name = "txtTemplateFile";
			this.txtTemplateFile.PasswordChar = ((char)(resources.GetObject("txtTemplateFile.PasswordChar")));
			this.txtTemplateFile.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("txtTemplateFile.RightToLeft")));
			this.txtTemplateFile.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("txtTemplateFile.ScrollBars")));
			this.txtTemplateFile.Size = ((System.Drawing.Size)(resources.GetObject("txtTemplateFile.Size")));
			this.txtTemplateFile.TabIndex = ((int)(resources.GetObject("txtTemplateFile.TabIndex")));
			this.txtTemplateFile.Text = resources.GetString("txtTemplateFile.Text");
			this.txtTemplateFile.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("txtTemplateFile.TextAlign")));
			this.txtTemplateFile.Visible = ((bool)(resources.GetObject("txtTemplateFile.Visible")));
			this.txtTemplateFile.WordWrap = ((bool)(resources.GetObject("txtTemplateFile.WordWrap")));
			this.txtTemplateFile.EnabledChanged += new System.EventHandler(this.txtTemplateFile_EnabledChanged);
			this.txtTemplateFile.TextChanged += new System.EventHandler(this.txtTemplateFile_TextChanged);
			// 
			// btnTemplateFile
			// 
			this.btnTemplateFile.AccessibleDescription = resources.GetString("btnTemplateFile.AccessibleDescription");
			this.btnTemplateFile.AccessibleName = resources.GetString("btnTemplateFile.AccessibleName");
			this.btnTemplateFile.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnTemplateFile.Anchor")));
			this.btnTemplateFile.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnTemplateFile.BackgroundImage")));
			this.btnTemplateFile.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnTemplateFile.Dock")));
			this.btnTemplateFile.Enabled = ((bool)(resources.GetObject("btnTemplateFile.Enabled")));
			this.btnTemplateFile.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnTemplateFile.FlatStyle")));
			this.btnTemplateFile.Font = ((System.Drawing.Font)(resources.GetObject("btnTemplateFile.Font")));
			this.btnTemplateFile.Image = ((System.Drawing.Image)(resources.GetObject("btnTemplateFile.Image")));
			this.btnTemplateFile.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnTemplateFile.ImageAlign")));
			this.btnTemplateFile.ImageIndex = ((int)(resources.GetObject("btnTemplateFile.ImageIndex")));
			this.btnTemplateFile.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnTemplateFile.ImeMode")));
			this.btnTemplateFile.Location = ((System.Drawing.Point)(resources.GetObject("btnTemplateFile.Location")));
			this.btnTemplateFile.Name = "btnTemplateFile";
			this.btnTemplateFile.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnTemplateFile.RightToLeft")));
			this.btnTemplateFile.Size = ((System.Drawing.Size)(resources.GetObject("btnTemplateFile.Size")));
			this.btnTemplateFile.TabIndex = ((int)(resources.GetObject("btnTemplateFile.TabIndex")));
			this.btnTemplateFile.Text = resources.GetString("btnTemplateFile.Text");
			this.btnTemplateFile.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnTemplateFile.TextAlign")));
			this.btnTemplateFile.Visible = ((bool)(resources.GetObject("btnTemplateFile.Visible")));
			this.btnTemplateFile.Click += new System.EventHandler(this.btnTemplateFile_Click);
			this.btnTemplateFile.EnabledChanged += new System.EventHandler(this.btnTemplateFile_EnabledChanged);
			// 
			// lblTemplateFile
			// 
			this.lblTemplateFile.AccessibleDescription = resources.GetString("lblTemplateFile.AccessibleDescription");
			this.lblTemplateFile.AccessibleName = resources.GetString("lblTemplateFile.AccessibleName");
			this.lblTemplateFile.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblTemplateFile.Anchor")));
			this.lblTemplateFile.AutoSize = ((bool)(resources.GetObject("lblTemplateFile.AutoSize")));
			this.lblTemplateFile.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblTemplateFile.Dock")));
			this.lblTemplateFile.Enabled = ((bool)(resources.GetObject("lblTemplateFile.Enabled")));
			this.lblTemplateFile.Font = ((System.Drawing.Font)(resources.GetObject("lblTemplateFile.Font")));
			this.lblTemplateFile.Image = ((System.Drawing.Image)(resources.GetObject("lblTemplateFile.Image")));
			this.lblTemplateFile.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblTemplateFile.ImageAlign")));
			this.lblTemplateFile.ImageIndex = ((int)(resources.GetObject("lblTemplateFile.ImageIndex")));
			this.lblTemplateFile.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblTemplateFile.ImeMode")));
			this.lblTemplateFile.Location = ((System.Drawing.Point)(resources.GetObject("lblTemplateFile.Location")));
			this.lblTemplateFile.Name = "lblTemplateFile";
			this.lblTemplateFile.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblTemplateFile.RightToLeft")));
			this.lblTemplateFile.Size = ((System.Drawing.Size)(resources.GetObject("lblTemplateFile.Size")));
			this.lblTemplateFile.TabIndex = ((int)(resources.GetObject("lblTemplateFile.TabIndex")));
			this.lblTemplateFile.Text = resources.GetString("lblTemplateFile.Text");
			this.lblTemplateFile.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblTemplateFile.TextAlign")));
			this.lblTemplateFile.Visible = ((bool)(resources.GetObject("lblTemplateFile.Visible")));
			// 
			// chkUseTemplate
			// 
			this.chkUseTemplate.AccessibleDescription = resources.GetString("chkUseTemplate.AccessibleDescription");
			this.chkUseTemplate.AccessibleName = resources.GetString("chkUseTemplate.AccessibleName");
			this.chkUseTemplate.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("chkUseTemplate.Anchor")));
			this.chkUseTemplate.Appearance = ((System.Windows.Forms.Appearance)(resources.GetObject("chkUseTemplate.Appearance")));
			this.chkUseTemplate.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("chkUseTemplate.BackgroundImage")));
			this.chkUseTemplate.CheckAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("chkUseTemplate.CheckAlign")));
			this.chkUseTemplate.Checked = true;
			this.chkUseTemplate.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkUseTemplate.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("chkUseTemplate.Dock")));
			this.chkUseTemplate.Enabled = ((bool)(resources.GetObject("chkUseTemplate.Enabled")));
			this.chkUseTemplate.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("chkUseTemplate.FlatStyle")));
			this.chkUseTemplate.Font = ((System.Drawing.Font)(resources.GetObject("chkUseTemplate.Font")));
			this.chkUseTemplate.Image = ((System.Drawing.Image)(resources.GetObject("chkUseTemplate.Image")));
			this.chkUseTemplate.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("chkUseTemplate.ImageAlign")));
			this.chkUseTemplate.ImageIndex = ((int)(resources.GetObject("chkUseTemplate.ImageIndex")));
			this.chkUseTemplate.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("chkUseTemplate.ImeMode")));
			this.chkUseTemplate.Location = ((System.Drawing.Point)(resources.GetObject("chkUseTemplate.Location")));
			this.chkUseTemplate.Name = "chkUseTemplate";
			this.chkUseTemplate.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("chkUseTemplate.RightToLeft")));
			this.chkUseTemplate.Size = ((System.Drawing.Size)(resources.GetObject("chkUseTemplate.Size")));
			this.chkUseTemplate.TabIndex = ((int)(resources.GetObject("chkUseTemplate.TabIndex")));
			this.chkUseTemplate.Text = resources.GetString("chkUseTemplate.Text");
			this.chkUseTemplate.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("chkUseTemplate.TextAlign")));
			this.chkUseTemplate.Visible = ((bool)(resources.GetObject("chkUseTemplate.Visible")));
			this.chkUseTemplate.EnabledChanged += new System.EventHandler(this.chkUseTemplate_EnabledChanged);
			this.chkUseTemplate.CheckedChanged += new System.EventHandler(this.chkUseTemplate_CheckedChanged);
			// 
			// dlgOpenTemplateFile
			// 
			this.dlgOpenTemplateFile.Filter = resources.GetString("dlgOpenTemplateFile.Filter");
			this.dlgOpenTemplateFile.Title = resources.GetString("dlgOpenTemplateFile.Title");
			// 
			// lblAddNewReport
			// 
			this.lblAddNewReport.AccessibleDescription = resources.GetString("lblAddNewReport.AccessibleDescription");
			this.lblAddNewReport.AccessibleName = resources.GetString("lblAddNewReport.AccessibleName");
			this.lblAddNewReport.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblAddNewReport.Anchor")));
			this.lblAddNewReport.AutoSize = ((bool)(resources.GetObject("lblAddNewReport.AutoSize")));
			this.lblAddNewReport.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblAddNewReport.Dock")));
			this.lblAddNewReport.Enabled = ((bool)(resources.GetObject("lblAddNewReport.Enabled")));
			this.lblAddNewReport.Font = ((System.Drawing.Font)(resources.GetObject("lblAddNewReport.Font")));
			this.lblAddNewReport.Image = ((System.Drawing.Image)(resources.GetObject("lblAddNewReport.Image")));
			this.lblAddNewReport.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblAddNewReport.ImageAlign")));
			this.lblAddNewReport.ImageIndex = ((int)(resources.GetObject("lblAddNewReport.ImageIndex")));
			this.lblAddNewReport.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblAddNewReport.ImeMode")));
			this.lblAddNewReport.Location = ((System.Drawing.Point)(resources.GetObject("lblAddNewReport.Location")));
			this.lblAddNewReport.Name = "lblAddNewReport";
			this.lblAddNewReport.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblAddNewReport.RightToLeft")));
			this.lblAddNewReport.Size = ((System.Drawing.Size)(resources.GetObject("lblAddNewReport.Size")));
			this.lblAddNewReport.TabIndex = ((int)(resources.GetObject("lblAddNewReport.TabIndex")));
			this.lblAddNewReport.Text = resources.GetString("lblAddNewReport.Text");
			this.lblAddNewReport.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblAddNewReport.TextAlign")));
			this.lblAddNewReport.Visible = ((bool)(resources.GetObject("lblAddNewReport.Visible")));
			// 
			// lblDefaultParameterFont
			// 
			this.lblDefaultParameterFont.AccessibleDescription = resources.GetString("lblDefaultParameterFont.AccessibleDescription");
			this.lblDefaultParameterFont.AccessibleName = resources.GetString("lblDefaultParameterFont.AccessibleName");
			this.lblDefaultParameterFont.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblDefaultParameterFont.Anchor")));
			this.lblDefaultParameterFont.AutoSize = ((bool)(resources.GetObject("lblDefaultParameterFont.AutoSize")));
			this.lblDefaultParameterFont.BackColor = System.Drawing.SystemColors.ControlLightLight;
			this.lblDefaultParameterFont.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblDefaultParameterFont.Dock")));
			this.lblDefaultParameterFont.Enabled = ((bool)(resources.GetObject("lblDefaultParameterFont.Enabled")));
			this.lblDefaultParameterFont.Font = ((System.Drawing.Font)(resources.GetObject("lblDefaultParameterFont.Font")));
			this.lblDefaultParameterFont.Image = ((System.Drawing.Image)(resources.GetObject("lblDefaultParameterFont.Image")));
			this.lblDefaultParameterFont.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblDefaultParameterFont.ImageAlign")));
			this.lblDefaultParameterFont.ImageIndex = ((int)(resources.GetObject("lblDefaultParameterFont.ImageIndex")));
			this.lblDefaultParameterFont.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblDefaultParameterFont.ImeMode")));
			this.lblDefaultParameterFont.Location = ((System.Drawing.Point)(resources.GetObject("lblDefaultParameterFont.Location")));
			this.lblDefaultParameterFont.Name = "lblDefaultParameterFont";
			this.lblDefaultParameterFont.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblDefaultParameterFont.RightToLeft")));
			this.lblDefaultParameterFont.Size = ((System.Drawing.Size)(resources.GetObject("lblDefaultParameterFont.Size")));
			this.lblDefaultParameterFont.TabIndex = ((int)(resources.GetObject("lblDefaultParameterFont.TabIndex")));
			this.lblDefaultParameterFont.Text = resources.GetString("lblDefaultParameterFont.Text");
			this.lblDefaultParameterFont.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblDefaultParameterFont.TextAlign")));
			this.lblDefaultParameterFont.Visible = ((bool)(resources.GetObject("lblDefaultParameterFont.Visible")));
			// 
			// lblDefaultReportHeaderFont
			// 
			this.lblDefaultReportHeaderFont.AccessibleDescription = resources.GetString("lblDefaultReportHeaderFont.AccessibleDescription");
			this.lblDefaultReportHeaderFont.AccessibleName = resources.GetString("lblDefaultReportHeaderFont.AccessibleName");
			this.lblDefaultReportHeaderFont.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblDefaultReportHeaderFont.Anchor")));
			this.lblDefaultReportHeaderFont.AutoSize = ((bool)(resources.GetObject("lblDefaultReportHeaderFont.AutoSize")));
			this.lblDefaultReportHeaderFont.BackColor = System.Drawing.SystemColors.ControlLightLight;
			this.lblDefaultReportHeaderFont.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblDefaultReportHeaderFont.Dock")));
			this.lblDefaultReportHeaderFont.Enabled = ((bool)(resources.GetObject("lblDefaultReportHeaderFont.Enabled")));
			this.lblDefaultReportHeaderFont.Font = ((System.Drawing.Font)(resources.GetObject("lblDefaultReportHeaderFont.Font")));
			this.lblDefaultReportHeaderFont.Image = ((System.Drawing.Image)(resources.GetObject("lblDefaultReportHeaderFont.Image")));
			this.lblDefaultReportHeaderFont.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblDefaultReportHeaderFont.ImageAlign")));
			this.lblDefaultReportHeaderFont.ImageIndex = ((int)(resources.GetObject("lblDefaultReportHeaderFont.ImageIndex")));
			this.lblDefaultReportHeaderFont.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblDefaultReportHeaderFont.ImeMode")));
			this.lblDefaultReportHeaderFont.Location = ((System.Drawing.Point)(resources.GetObject("lblDefaultReportHeaderFont.Location")));
			this.lblDefaultReportHeaderFont.Name = "lblDefaultReportHeaderFont";
			this.lblDefaultReportHeaderFont.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblDefaultReportHeaderFont.RightToLeft")));
			this.lblDefaultReportHeaderFont.Size = ((System.Drawing.Size)(resources.GetObject("lblDefaultReportHeaderFont.Size")));
			this.lblDefaultReportHeaderFont.TabIndex = ((int)(resources.GetObject("lblDefaultReportHeaderFont.TabIndex")));
			this.lblDefaultReportHeaderFont.Text = resources.GetString("lblDefaultReportHeaderFont.Text");
			this.lblDefaultReportHeaderFont.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblDefaultReportHeaderFont.TextAlign")));
			this.lblDefaultReportHeaderFont.Visible = ((bool)(resources.GetObject("lblDefaultReportHeaderFont.Visible")));
			// 
			// lblDefaultPageHeaderFont
			// 
			this.lblDefaultPageHeaderFont.AccessibleDescription = resources.GetString("lblDefaultPageHeaderFont.AccessibleDescription");
			this.lblDefaultPageHeaderFont.AccessibleName = resources.GetString("lblDefaultPageHeaderFont.AccessibleName");
			this.lblDefaultPageHeaderFont.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblDefaultPageHeaderFont.Anchor")));
			this.lblDefaultPageHeaderFont.AutoSize = ((bool)(resources.GetObject("lblDefaultPageHeaderFont.AutoSize")));
			this.lblDefaultPageHeaderFont.BackColor = System.Drawing.SystemColors.ControlLightLight;
			this.lblDefaultPageHeaderFont.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblDefaultPageHeaderFont.Dock")));
			this.lblDefaultPageHeaderFont.Enabled = ((bool)(resources.GetObject("lblDefaultPageHeaderFont.Enabled")));
			this.lblDefaultPageHeaderFont.Font = ((System.Drawing.Font)(resources.GetObject("lblDefaultPageHeaderFont.Font")));
			this.lblDefaultPageHeaderFont.Image = ((System.Drawing.Image)(resources.GetObject("lblDefaultPageHeaderFont.Image")));
			this.lblDefaultPageHeaderFont.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblDefaultPageHeaderFont.ImageAlign")));
			this.lblDefaultPageHeaderFont.ImageIndex = ((int)(resources.GetObject("lblDefaultPageHeaderFont.ImageIndex")));
			this.lblDefaultPageHeaderFont.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblDefaultPageHeaderFont.ImeMode")));
			this.lblDefaultPageHeaderFont.Location = ((System.Drawing.Point)(resources.GetObject("lblDefaultPageHeaderFont.Location")));
			this.lblDefaultPageHeaderFont.Name = "lblDefaultPageHeaderFont";
			this.lblDefaultPageHeaderFont.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblDefaultPageHeaderFont.RightToLeft")));
			this.lblDefaultPageHeaderFont.Size = ((System.Drawing.Size)(resources.GetObject("lblDefaultPageHeaderFont.Size")));
			this.lblDefaultPageHeaderFont.TabIndex = ((int)(resources.GetObject("lblDefaultPageHeaderFont.TabIndex")));
			this.lblDefaultPageHeaderFont.Text = resources.GetString("lblDefaultPageHeaderFont.Text");
			this.lblDefaultPageHeaderFont.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblDefaultPageHeaderFont.TextAlign")));
			this.lblDefaultPageHeaderFont.Visible = ((bool)(resources.GetObject("lblDefaultPageHeaderFont.Visible")));
			// 
			// lblDefaultGroupByFont
			// 
			this.lblDefaultGroupByFont.AccessibleDescription = resources.GetString("lblDefaultGroupByFont.AccessibleDescription");
			this.lblDefaultGroupByFont.AccessibleName = resources.GetString("lblDefaultGroupByFont.AccessibleName");
			this.lblDefaultGroupByFont.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblDefaultGroupByFont.Anchor")));
			this.lblDefaultGroupByFont.AutoSize = ((bool)(resources.GetObject("lblDefaultGroupByFont.AutoSize")));
			this.lblDefaultGroupByFont.BackColor = System.Drawing.SystemColors.ControlLightLight;
			this.lblDefaultGroupByFont.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblDefaultGroupByFont.Dock")));
			this.lblDefaultGroupByFont.Enabled = ((bool)(resources.GetObject("lblDefaultGroupByFont.Enabled")));
			this.lblDefaultGroupByFont.Font = ((System.Drawing.Font)(resources.GetObject("lblDefaultGroupByFont.Font")));
			this.lblDefaultGroupByFont.Image = ((System.Drawing.Image)(resources.GetObject("lblDefaultGroupByFont.Image")));
			this.lblDefaultGroupByFont.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblDefaultGroupByFont.ImageAlign")));
			this.lblDefaultGroupByFont.ImageIndex = ((int)(resources.GetObject("lblDefaultGroupByFont.ImageIndex")));
			this.lblDefaultGroupByFont.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblDefaultGroupByFont.ImeMode")));
			this.lblDefaultGroupByFont.Location = ((System.Drawing.Point)(resources.GetObject("lblDefaultGroupByFont.Location")));
			this.lblDefaultGroupByFont.Name = "lblDefaultGroupByFont";
			this.lblDefaultGroupByFont.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblDefaultGroupByFont.RightToLeft")));
			this.lblDefaultGroupByFont.Size = ((System.Drawing.Size)(resources.GetObject("lblDefaultGroupByFont.Size")));
			this.lblDefaultGroupByFont.TabIndex = ((int)(resources.GetObject("lblDefaultGroupByFont.TabIndex")));
			this.lblDefaultGroupByFont.Text = resources.GetString("lblDefaultGroupByFont.Text");
			this.lblDefaultGroupByFont.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblDefaultGroupByFont.TextAlign")));
			this.lblDefaultGroupByFont.Visible = ((bool)(resources.GetObject("lblDefaultGroupByFont.Visible")));
			// 
			// lblDefaultPageFooterFont
			// 
			this.lblDefaultPageFooterFont.AccessibleDescription = resources.GetString("lblDefaultPageFooterFont.AccessibleDescription");
			this.lblDefaultPageFooterFont.AccessibleName = resources.GetString("lblDefaultPageFooterFont.AccessibleName");
			this.lblDefaultPageFooterFont.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblDefaultPageFooterFont.Anchor")));
			this.lblDefaultPageFooterFont.AutoSize = ((bool)(resources.GetObject("lblDefaultPageFooterFont.AutoSize")));
			this.lblDefaultPageFooterFont.BackColor = System.Drawing.SystemColors.ControlLightLight;
			this.lblDefaultPageFooterFont.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblDefaultPageFooterFont.Dock")));
			this.lblDefaultPageFooterFont.Enabled = ((bool)(resources.GetObject("lblDefaultPageFooterFont.Enabled")));
			this.lblDefaultPageFooterFont.Font = ((System.Drawing.Font)(resources.GetObject("lblDefaultPageFooterFont.Font")));
			this.lblDefaultPageFooterFont.Image = ((System.Drawing.Image)(resources.GetObject("lblDefaultPageFooterFont.Image")));
			this.lblDefaultPageFooterFont.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblDefaultPageFooterFont.ImageAlign")));
			this.lblDefaultPageFooterFont.ImageIndex = ((int)(resources.GetObject("lblDefaultPageFooterFont.ImageIndex")));
			this.lblDefaultPageFooterFont.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblDefaultPageFooterFont.ImeMode")));
			this.lblDefaultPageFooterFont.Location = ((System.Drawing.Point)(resources.GetObject("lblDefaultPageFooterFont.Location")));
			this.lblDefaultPageFooterFont.Name = "lblDefaultPageFooterFont";
			this.lblDefaultPageFooterFont.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblDefaultPageFooterFont.RightToLeft")));
			this.lblDefaultPageFooterFont.Size = ((System.Drawing.Size)(resources.GetObject("lblDefaultPageFooterFont.Size")));
			this.lblDefaultPageFooterFont.TabIndex = ((int)(resources.GetObject("lblDefaultPageFooterFont.TabIndex")));
			this.lblDefaultPageFooterFont.Text = resources.GetString("lblDefaultPageFooterFont.Text");
			this.lblDefaultPageFooterFont.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblDefaultPageFooterFont.TextAlign")));
			this.lblDefaultPageFooterFont.Visible = ((bool)(resources.GetObject("lblDefaultPageFooterFont.Visible")));
			// 
			// lblDefaultReportFooterFont
			// 
			this.lblDefaultReportFooterFont.AccessibleDescription = resources.GetString("lblDefaultReportFooterFont.AccessibleDescription");
			this.lblDefaultReportFooterFont.AccessibleName = resources.GetString("lblDefaultReportFooterFont.AccessibleName");
			this.lblDefaultReportFooterFont.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblDefaultReportFooterFont.Anchor")));
			this.lblDefaultReportFooterFont.AutoSize = ((bool)(resources.GetObject("lblDefaultReportFooterFont.AutoSize")));
			this.lblDefaultReportFooterFont.BackColor = System.Drawing.SystemColors.ControlLightLight;
			this.lblDefaultReportFooterFont.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblDefaultReportFooterFont.Dock")));
			this.lblDefaultReportFooterFont.Enabled = ((bool)(resources.GetObject("lblDefaultReportFooterFont.Enabled")));
			this.lblDefaultReportFooterFont.Font = ((System.Drawing.Font)(resources.GetObject("lblDefaultReportFooterFont.Font")));
			this.lblDefaultReportFooterFont.Image = ((System.Drawing.Image)(resources.GetObject("lblDefaultReportFooterFont.Image")));
			this.lblDefaultReportFooterFont.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblDefaultReportFooterFont.ImageAlign")));
			this.lblDefaultReportFooterFont.ImageIndex = ((int)(resources.GetObject("lblDefaultReportFooterFont.ImageIndex")));
			this.lblDefaultReportFooterFont.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblDefaultReportFooterFont.ImeMode")));
			this.lblDefaultReportFooterFont.Location = ((System.Drawing.Point)(resources.GetObject("lblDefaultReportFooterFont.Location")));
			this.lblDefaultReportFooterFont.Name = "lblDefaultReportFooterFont";
			this.lblDefaultReportFooterFont.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblDefaultReportFooterFont.RightToLeft")));
			this.lblDefaultReportFooterFont.Size = ((System.Drawing.Size)(resources.GetObject("lblDefaultReportFooterFont.Size")));
			this.lblDefaultReportFooterFont.TabIndex = ((int)(resources.GetObject("lblDefaultReportFooterFont.TabIndex")));
			this.lblDefaultReportFooterFont.Text = resources.GetString("lblDefaultReportFooterFont.Text");
			this.lblDefaultReportFooterFont.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblDefaultReportFooterFont.TextAlign")));
			this.lblDefaultReportFooterFont.Visible = ((bool)(resources.GetObject("lblDefaultReportFooterFont.Visible")));
			// 
			// lblDefaultDetailFont
			// 
			this.lblDefaultDetailFont.AccessibleDescription = resources.GetString("lblDefaultDetailFont.AccessibleDescription");
			this.lblDefaultDetailFont.AccessibleName = resources.GetString("lblDefaultDetailFont.AccessibleName");
			this.lblDefaultDetailFont.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblDefaultDetailFont.Anchor")));
			this.lblDefaultDetailFont.AutoSize = ((bool)(resources.GetObject("lblDefaultDetailFont.AutoSize")));
			this.lblDefaultDetailFont.BackColor = System.Drawing.SystemColors.ControlLightLight;
			this.lblDefaultDetailFont.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblDefaultDetailFont.Dock")));
			this.lblDefaultDetailFont.Enabled = ((bool)(resources.GetObject("lblDefaultDetailFont.Enabled")));
			this.lblDefaultDetailFont.Font = ((System.Drawing.Font)(resources.GetObject("lblDefaultDetailFont.Font")));
			this.lblDefaultDetailFont.Image = ((System.Drawing.Image)(resources.GetObject("lblDefaultDetailFont.Image")));
			this.lblDefaultDetailFont.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblDefaultDetailFont.ImageAlign")));
			this.lblDefaultDetailFont.ImageIndex = ((int)(resources.GetObject("lblDefaultDetailFont.ImageIndex")));
			this.lblDefaultDetailFont.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblDefaultDetailFont.ImeMode")));
			this.lblDefaultDetailFont.Location = ((System.Drawing.Point)(resources.GetObject("lblDefaultDetailFont.Location")));
			this.lblDefaultDetailFont.Name = "lblDefaultDetailFont";
			this.lblDefaultDetailFont.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblDefaultDetailFont.RightToLeft")));
			this.lblDefaultDetailFont.Size = ((System.Drawing.Size)(resources.GetObject("lblDefaultDetailFont.Size")));
			this.lblDefaultDetailFont.TabIndex = ((int)(resources.GetObject("lblDefaultDetailFont.TabIndex")));
			this.lblDefaultDetailFont.Text = resources.GetString("lblDefaultDetailFont.Text");
			this.lblDefaultDetailFont.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblDefaultDetailFont.TextAlign")));
			this.lblDefaultDetailFont.Visible = ((bool)(resources.GetObject("lblDefaultDetailFont.Visible")));
			// 
			// txtReportNameVN
			// 
			this.txtReportNameVN.AccessibleDescription = resources.GetString("txtReportNameVN.AccessibleDescription");
			this.txtReportNameVN.AccessibleName = resources.GetString("txtReportNameVN.AccessibleName");
			this.txtReportNameVN.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("txtReportNameVN.Anchor")));
			this.txtReportNameVN.AutoSize = ((bool)(resources.GetObject("txtReportNameVN.AutoSize")));
			this.txtReportNameVN.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("txtReportNameVN.BackgroundImage")));
			this.txtReportNameVN.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("txtReportNameVN.Dock")));
			this.txtReportNameVN.Enabled = ((bool)(resources.GetObject("txtReportNameVN.Enabled")));
			this.txtReportNameVN.Font = ((System.Drawing.Font)(resources.GetObject("txtReportNameVN.Font")));
			this.txtReportNameVN.ForeColor = System.Drawing.SystemColors.WindowText;
			this.txtReportNameVN.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("txtReportNameVN.ImeMode")));
			this.txtReportNameVN.Location = ((System.Drawing.Point)(resources.GetObject("txtReportNameVN.Location")));
			this.txtReportNameVN.MaxLength = ((int)(resources.GetObject("txtReportNameVN.MaxLength")));
			this.txtReportNameVN.Multiline = ((bool)(resources.GetObject("txtReportNameVN.Multiline")));
			this.txtReportNameVN.Name = "txtReportNameVN";
			this.txtReportNameVN.PasswordChar = ((char)(resources.GetObject("txtReportNameVN.PasswordChar")));
			this.txtReportNameVN.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("txtReportNameVN.RightToLeft")));
			this.txtReportNameVN.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("txtReportNameVN.ScrollBars")));
			this.txtReportNameVN.Size = ((System.Drawing.Size)(resources.GetObject("txtReportNameVN.Size")));
			this.txtReportNameVN.TabIndex = ((int)(resources.GetObject("txtReportNameVN.TabIndex")));
			this.txtReportNameVN.Text = resources.GetString("txtReportNameVN.Text");
			this.txtReportNameVN.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("txtReportNameVN.TextAlign")));
			this.txtReportNameVN.Visible = ((bool)(resources.GetObject("txtReportNameVN.Visible")));
			this.txtReportNameVN.WordWrap = ((bool)(resources.GetObject("txtReportNameVN.WordWrap")));
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
			// txtReportNameJP
			// 
			this.txtReportNameJP.AccessibleDescription = resources.GetString("txtReportNameJP.AccessibleDescription");
			this.txtReportNameJP.AccessibleName = resources.GetString("txtReportNameJP.AccessibleName");
			this.txtReportNameJP.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("txtReportNameJP.Anchor")));
			this.txtReportNameJP.AutoSize = ((bool)(resources.GetObject("txtReportNameJP.AutoSize")));
			this.txtReportNameJP.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("txtReportNameJP.BackgroundImage")));
			this.txtReportNameJP.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("txtReportNameJP.Dock")));
			this.txtReportNameJP.Enabled = ((bool)(resources.GetObject("txtReportNameJP.Enabled")));
			this.txtReportNameJP.Font = ((System.Drawing.Font)(resources.GetObject("txtReportNameJP.Font")));
			this.txtReportNameJP.ForeColor = System.Drawing.SystemColors.WindowText;
			this.txtReportNameJP.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("txtReportNameJP.ImeMode")));
			this.txtReportNameJP.Location = ((System.Drawing.Point)(resources.GetObject("txtReportNameJP.Location")));
			this.txtReportNameJP.MaxLength = ((int)(resources.GetObject("txtReportNameJP.MaxLength")));
			this.txtReportNameJP.Multiline = ((bool)(resources.GetObject("txtReportNameJP.Multiline")));
			this.txtReportNameJP.Name = "txtReportNameJP";
			this.txtReportNameJP.PasswordChar = ((char)(resources.GetObject("txtReportNameJP.PasswordChar")));
			this.txtReportNameJP.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("txtReportNameJP.RightToLeft")));
			this.txtReportNameJP.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("txtReportNameJP.ScrollBars")));
			this.txtReportNameJP.Size = ((System.Drawing.Size)(resources.GetObject("txtReportNameJP.Size")));
			this.txtReportNameJP.TabIndex = ((int)(resources.GetObject("txtReportNameJP.TabIndex")));
			this.txtReportNameJP.Text = resources.GetString("txtReportNameJP.Text");
			this.txtReportNameJP.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("txtReportNameJP.TextAlign")));
			this.txtReportNameJP.Visible = ((bool)(resources.GetObject("txtReportNameJP.Visible")));
			this.txtReportNameJP.WordWrap = ((bool)(resources.GetObject("txtReportNameJP.WordWrap")));
			// 
			// label2
			// 
			this.label2.AccessibleDescription = resources.GetString("label2.AccessibleDescription");
			this.label2.AccessibleName = resources.GetString("label2.AccessibleName");
			this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("label2.Anchor")));
			this.label2.AutoSize = ((bool)(resources.GetObject("label2.AutoSize")));
			this.label2.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("label2.Dock")));
			this.label2.Enabled = ((bool)(resources.GetObject("label2.Enabled")));
			this.label2.Font = ((System.Drawing.Font)(resources.GetObject("label2.Font")));
			this.label2.ForeColor = System.Drawing.Color.Maroon;
			this.label2.Image = ((System.Drawing.Image)(resources.GetObject("label2.Image")));
			this.label2.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label2.ImageAlign")));
			this.label2.ImageIndex = ((int)(resources.GetObject("label2.ImageIndex")));
			this.label2.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("label2.ImeMode")));
			this.label2.Location = ((System.Drawing.Point)(resources.GetObject("label2.Location")));
			this.label2.Name = "label2";
			this.label2.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("label2.RightToLeft")));
			this.label2.Size = ((System.Drawing.Size)(resources.GetObject("label2.Size")));
			this.label2.TabIndex = ((int)(resources.GetObject("label2.TabIndex")));
			this.label2.Text = resources.GetString("label2.Text");
			this.label2.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label2.TextAlign")));
			this.label2.Visible = ((bool)(resources.GetObject("label2.Visible")));
			// 
			// EditReport
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
			this.Controls.Add(this.txtReportNameJP);
			this.Controls.Add(this.txtReportNameVN);
			this.Controls.Add(this.chkUseTemplate);
			this.Controls.Add(this.txtTemplateFile);
			this.Controls.Add(this.txtDescription);
			this.Controls.Add(this.txtISOCode);
			this.Controls.Add(this.txtReportFile);
			this.Controls.Add(this.txtCommand);
			this.Controls.Add(this.txtReportName);
			this.Controls.Add(this.txtReportCode);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.lblDefaultParameterFont);
			this.Controls.Add(this.lblDefaultReportHeaderFont);
			this.Controls.Add(this.lblDefaultPageHeaderFont);
			this.Controls.Add(this.lblDefaultGroupByFont);
			this.Controls.Add(this.lblDefaultPageFooterFont);
			this.Controls.Add(this.lblDefaultReportFooterFont);
			this.Controls.Add(this.lblDefaultDetailFont);
			this.Controls.Add(this.lblTemplateFile);
			this.Controls.Add(this.btnTemplateFile);
			this.Controls.Add(this.lblGroupID);
			this.Controls.Add(this.lblDescription);
			this.Controls.Add(this.btnClose);
			this.Controls.Add(this.btnHelp);
			this.Controls.Add(this.btnEdit);
			this.Controls.Add(this.btnSave);
			this.Controls.Add(this.btnDelete);
			this.Controls.Add(this.grbReportDetail);
			this.Controls.Add(this.cboReportType);
			this.Controls.Add(this.lblReportType);
			this.Controls.Add(this.btnFile);
			this.Controls.Add(this.lblISO_Code);
			this.Controls.Add(this.lblReportFile);
			this.Controls.Add(this.lblCommand);
			this.Controls.Add(this.lblReportName);
			this.Controls.Add(this.lblReportCode);
			this.Controls.Add(this.lblAddNewReport);
			this.Enabled = ((bool)(resources.GetObject("$this.Enabled")));
			this.Font = ((System.Drawing.Font)(resources.GetObject("$this.Font")));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("$this.ImeMode")));
			this.Location = ((System.Drawing.Point)(resources.GetObject("$this.Location")));
			this.MaximizeBox = false;
			this.MaximumSize = ((System.Drawing.Size)(resources.GetObject("$this.MaximumSize")));
			this.MinimumSize = ((System.Drawing.Size)(resources.GetObject("$this.MinimumSize")));
			this.Name = "EditReport";
			this.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("$this.RightToLeft")));
			this.StartPosition = ((System.Windows.Forms.FormStartPosition)(resources.GetObject("$this.StartPosition")));
			this.Text = resources.GetString("$this.Text");
			this.Closing += new System.ComponentModel.CancelEventHandler(this.EditReport_Closing);
			this.Load += new System.EventHandler(this.EditReport_Load);
			this.grbReportDetail.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		#region Dispose
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

		#region Form Events
		private void EditReport_Load(object sender, EventArgs e)
		{
			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.WaitCursor;

			#endregion Code Inserted Automatically

			
			const string METHOD_NAME = THIS + ".EditReport_Load()";
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
				

				// store old caption
				mFormCaption = this.Text;
				lblGroupID.Text = mGroupID;
				txtReportCode.Text = mReportID;

				if (EnumType == EnumAction.Add)
				{					
					//TurnOnSaveReportButton();					
					cboReportType.SelectedItem = Constants.SQL_REPORT;
					/// HACKED: Thachnn: fix bug when OpenForm,
					/// selected index of cboReportType = SQL_REPORT,
					/// but state of form is not correct
					/// (chkUseTemplate is uncheck, but txtTemplateFile is still Enable)
					chkUseTemplate_CheckedChanged(chkUseTemplate,null);
					/// ENDHACKED: Thachnn: fix bug when OpenForm				
				}
				else
				{					
					LayoutOnFormValueInMvoObject();
				}
				SwitchFormMode();

				try	// get the report file and template file in the VOReport if any
				{
					//mstrReportFilePath = this.mvoReport.ReportFile;
					//mstrXMLTemplateFilePath = this.mvoReport.TemplateFile;
				}
				catch{}
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
		/// Open form to configure fields properties
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnFieldProperties_Click(object sender, System.EventArgs e)
		{
			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.WaitCursor;

			#endregion Code Inserted Automatically

			
			const string METHOD_NAME = THIS + ".btnFieldProperties_Click()";
			try
			{
				if (mvoReport.UseTemplate)
				{
					PCSMessageBox.Show(ErrorCode.MESSAGE_CANNOT_EDIT_FIELD_WHEN_USE_TEMPLATE, MessageBoxButtons.OK, MessageBoxIcon.Warning);
					// Code Inserted Automatically
#region Code Inserted Automatically
this.Cursor = Cursors.Default;
#endregion Code Inserted Automatically

					return;
				}
				// get all fields of report
				ArrayList arrFields = new FieldPropertiesBO().ListByReport(mvoReport.ReportID);
				if (arrFields.Count <= 0)
				{
					PCSMessageBox.Show(ErrorCode.MESSAGE_NO_RECORD, MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
					// Code Inserted Automatically
#region Code Inserted Automatically
this.Cursor = Cursors.Default;
#endregion Code Inserted Automatically

					return;
				}
				FieldProperties frmFieldProperties = new FieldProperties();
				frmFieldProperties.VoReport = this.mvoReport;
				frmFieldProperties.Fields = arrFields;
				frmFieldProperties.ShowDialog();
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
		/// Open form to configure field groups properties
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnFieldGroup_Click(object sender, System.EventArgs e)
		{
			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.WaitCursor;

			#endregion Code Inserted Automatically

			
			const string METHOD_NAME = THIS + ".btnFieldGroup_Click()";
			try
			{
				GroupProperties frmGroupProperties = new GroupProperties();
				frmGroupProperties.Report = this.mvoReport;
				frmGroupProperties.ShowDialog();
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
		private void btnParameter_Click(object sender, EventArgs e)
		{
			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.WaitCursor;

			#endregion Code Inserted Automatically

			
			const string METHOD_NAME = THIS + ".btnParameter_Click()";
			try
			{
				// get all params of report
				ArrayList arrParams = new ReportParameterBO().ListByReport(mvoReport.ReportID);
//				if (arrParams.Count <= 0)
//				{
//					PCSMessageBox.Show(ErrorCode.MESSAGE_NO_RECORD, MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);					
//				}								

				ReportParameter frmReportParameter = new ReportParameter(mvoReport);
				frmReportParameter.Params = arrParams;
				if(arrParams.Count <= 0)
				{
					//PCSMessageBox.Show(ErrorCode.MESSAGE_NO_RECORD, MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);	
					//frmReportParameter.EnumType = EnumAction.Add;
				}
				else
				{					                					
					frmReportParameter.EnumType = EnumAction.Default;
				}
				frmReportParameter.ShowDialog();				
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

		private void btnSave_Click(object sender, EventArgs e)
		{
			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.WaitCursor;

			#endregion Code Inserted Automatically

			
			const string METHOD_NAME = THIS + ".btnSave_Click()";
			try
			{
				// check mandatory fields
				if (!CheckMandatory(txtReportCode))
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Error);
					txtReportName.Focus();
					txtReportCode.Select();
					// Code Inserted Automatically
					#region Code Inserted Automatically
					this.Cursor = Cursors.Default;
					#endregion Code Inserted Automatically

					return;
				}
				if (!CheckMandatory(txtReportName))
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Error);
					txtReportName.Focus();
					txtReportName.Select();
					// Code Inserted Automatically
					#region Code Inserted Automatically
					this.Cursor = Cursors.Default;
					#endregion Code Inserted Automatically

					return;
				}
				if (cboReportType.SelectedItem == null)
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Error);
					cboReportType.Focus();
					cboReportType.Select();
					// Code Inserted Automatically
					#region Code Inserted Automatically
					this.Cursor = Cursors.Default;
					#endregion Code Inserted Automatically

					return;
				}
				else
				{
					if (cboReportType.SelectedItem.ToString() != Constants.CUSTOM_REPORT)
					{
						if (cboReportType.SelectedItem.ToString() != Constants.SQL_REPORT)
						{
							if (txtReportFile.Text.Trim() == string.Empty)
							{
								PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Error);
								txtReportFile.Focus();
								txtReportFile.Select();
								// Code Inserted Automatically
								#region Code Inserted Automatically
								this.Cursor = Cursors.Default;
								#endregion Code Inserted Automatically

								return;
							}
						}
						if (txtCommand.Text.Trim() == string.Empty)
						{
							PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Error);
							txtCommand.Focus();
							txtCommand.Select();
							// Code Inserted Automatically
							#region Code Inserted Automatically
							this.Cursor = Cursors.Default;
							#endregion Code Inserted Automatically

							return;
						}
					}
				}
				if(!Directory.Exists(mstrReportDefFolder))
				{
					Directory.CreateDirectory(mstrReportDefFolder);
				}

				if(!CheckMandatory(txtTemplateFile) && chkUseTemplate.Checked == true)
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Error);
					txtTemplateFile.Focus();
					txtTemplateFile.Select();
					// Code Inserted Automatically
					#region Code Inserted Automatically
					this.Cursor = Cursors.Default;
					#endregion Code Inserted Automatically

					return;
				}

				if (SaveData())
				{
					PCSMessageBox.Show(ErrorCode.MESSAGE_AFTER_SAVE_DATA, MessageBoxIcon.Information);
					// change form to default mode
					mEnumType = EnumAction.Default;
					//this.DialogResult = DialogResult.OK;
					// switch form mode
					SwitchFormMode();
				}
				else
				{
					PCSMessageBox.Show(ErrorCode.MESSAGE_CANNOT_SAVE_TO_DB, MessageBoxIcon.Error);
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
				txtReportName.Focus();
				txtReportName.Select();
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
				txtReportName.Focus();
				
			}

			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.Default;

			#endregion Code Inserted Automatically

			
		}

		private void btnEdit_Click(object sender, EventArgs e)
		{
			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.WaitCursor;

			#endregion Code Inserted Automatically

			
			const string METHOD_NAME = THIS + ".btnEdit_Click()";
			try
			{
				mEnumType = EnumAction.Edit;
				SwitchFormMode();
				//TurnOnSaveReportButton();
				LayoutOnFormValueInMvoObject();
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

		private void btnDelete_Click(object sender, EventArgs e)
		{
			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.WaitCursor;

			#endregion Code Inserted Automatically

			
			const string METHOD_NAME = THIS + ".btnDelete_Click()";
			try
			{
				// alert user to confirm delete selection
				DialogResult dlgResult = PCSMessageBox.Show(ErrorCode.MESSAGE_DELETE_RECORD, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
				if (dlgResult == DialogResult.Yes)
				{
					ReportManagementBO boReportManagement = new ReportManagementBO();
					boReportManagement.Delete(mvoReport);
					mvoReport = new sys_ReportVO();
					// close the form
					this.Close();
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

		private void btnDrillDownReport_Click(object sender, System.EventArgs e)
		{
			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.WaitCursor;

			#endregion Code Inserted Automatically

			
			const string METHOD_NAME = THIS + ".btnDrillDownReport_Click()";
			try
			{
				// display drill down report screen
				DrillDownReport frmDrillDownReport = new DrillDownReport();
				frmDrillDownReport.MasterReportID = mReportID;
				frmDrillDownReport.ShowDialog();
				// refresh form
				//EditReport_Load(this, e);
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

		private void btnPageSetup_Click(object sender, System.EventArgs e)
		{
			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.WaitCursor;

			#endregion Code Inserted Automatically

			
			const string METHOD_NAME = THIS + ".btnPageSetup_Click()";
			try
			{
				PageSetup frmPageSetup = new PageSetup();
				// assign report for setup
				frmPageSetup.VoSysReport = this.mvoReport;
				frmPageSetup.ShowDialog();
				// return new vo object
				this.mvoReport = frmPageSetup.VoSysReport;
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

		private void cboReportType_SelectedIndexChanged(object sender, System.EventArgs e)
		{			
			const string METHOD_NAME = THIS + ".cboReportType_SelectedIndexChanged()";
			try
			{				
				if (cboReportType.SelectedItem.Equals(Constants.CUSTOM_REPORT))
				{
					txtReportFile.Enabled = false;
					btnFile.Enabled = false;	
					chkUseTemplate.Checked = true;
					dlgOpenFile.Filter = CUSTOM_REPORT_FILTER;
					txtCommand.Enabled = true;
					lblCommand.ForeColor = DefaultForeColor;
					lblReportFile.ForeColor = DefaultForeColor;
				}
				else if(cboReportType.SelectedItem.Equals(Constants.SQL_REPORT))
				{					
					txtReportFile.Enabled = false;
					btnFile.Enabled = false;
					chkUseTemplate.Checked = false;
					dlgOpenFile.Filter = CUSTOM_REPORT_FILTER;
					txtCommand.Enabled = true;
					lblCommand.ForeColor = Color.Maroon;
					lblReportFile.ForeColor = DefaultForeColor;
				}					
				else if(cboReportType.SelectedItem.Equals(Constants.DLL_REPORT))
				{
					txtReportFile.Enabled = true;
					btnFile.Enabled = true;				
					chkUseTemplate.Checked = true;
					dlgOpenFile.Filter = DLL_FILTER;
					txtCommand.Enabled = true;
					lblCommand.ForeColor = Color.Maroon;
					lblReportFile.ForeColor = Color.Maroon;
				}
				else if (cboReportType.SelectedItem.Equals(Constants.CSHARP_FILE_REPORT))
				{
					txtReportFile.Enabled = true;
					btnFile.Enabled = true;				
					chkUseTemplate.Checked = true;
					dlgOpenFile.Filter = CSHARP_FILTER;
					txtCommand.Enabled = true;
					lblCommand.ForeColor = Color.Maroon;
					lblReportFile.ForeColor = Color.Maroon;
				}
				blnEditedReport = true;
				blnSavedReport = false;
			}
				#region catch Exception				
			catch (PCSException ex)
			{				
				PCSMessageBox.Show(ex.mCode, MessageBoxButtons.OK, MessageBoxIcon.Error);			
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
				PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);			
				try
				{
					Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
				}
				catch
				{
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
			#endregion catch Exception
		}

		/// <summary>
		/// User is closing the form.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void EditReport_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".EditReport_Closing()";
			try
			{
				if(txtReportName.Text == null || txtReportName.Text == string.Empty)
				{
					if (mEnumType == EnumAction.Add || mEnumType == EnumAction.Copy)
						mvoReport = null;
					this.DialogResult = DialogResult.Cancel;
					e.Cancel = false;
				}
				else
				{
					if(blnSavedReport == false && this.mEnumType != EnumAction.Default)	// if edited by not save, ask user
					{
						// display confirm message					
						DialogResult dlgResult = PCSMessageBox.Show(ErrorCode.MESSAGE_QUESTION_STORE_INTO_DATABASE,MessageBoxButtons.YesNoCancel,MessageBoxIcon.Question,MessageBoxDefaultButton.Button1);
						switch (dlgResult)
						{
							case DialogResult.Yes:
								//HACK: Thachnn : 31/10/2005. Fix bug 2426
//								try
//								{
//									if (!SaveData())
//									{
//										e.Cancel = true;
//										PCSMessageBox.Show(ErrorCode.MESSAGE_CANNOT_SAVE_TO_DB, MessageBoxIcon.Error);
//										this.DialogResult = DialogResult.Cancel;
//									}
//									else
//									{
//										this.DialogResult = DialogResult.OK;
//									}
//								}
//								catch
//								{
//									e.Cancel = true;
//									PCSMessageBox.Show(ErrorCode.DUPLICATE_KEY, MessageBoxIcon.Error);
//									txtReportName.Select();
//									this.DialogResult = DialogResult.Cancel;
//								}
								btnSave_Click(btnSave,null);
								if(mEnumType == EnumAction.Default)	// it means save OK
								{
								}
								else
								{
									e.Cancel = true;
								}
								// ENDHACK: Thachnn : 31/10/2005. Fix bug 2426
								break;

							case DialogResult.No:
								if (mEnumType == EnumAction.Add || mEnumType == EnumAction.Copy)
									mvoReport = null;
								e.Cancel = false;
								this.DialogResult = DialogResult.Cancel;
								break;

							case DialogResult.Cancel:
								e.Cancel = true;
								this.DialogResult = DialogResult.Cancel;
								break;
						}
					}
					else
					{
						this.DialogResult = DialogResult.OK;
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
		
		private void btnFile_Click(object sender, System.EventArgs e)
		{
			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.WaitCursor;

			#endregion Code Inserted Automatically

			
			const string METHOD_NAME = THIS + ".btnFile_Click()";
			try
			{
				if (dlgOpenFile.ShowDialog() == DialogResult.OK)
				{
					/// HACKED: Thachnn: get full path to mstrReportFilePath
					/// put only the filename in the txtTemplateFile
					//mstrReportFilePath = dlgOpenFile.FileName;
					txtReportFile.Text = Path.GetFileName(dlgOpenFile.FileName);
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
		
		private void txtReportFile_EnabledChanged(object sender, System.EventArgs e)
		{
			btnFile.Enabled = txtReportFile.Enabled;
		}
		
		private void btnFile_EnabledChanged(object sender, System.EventArgs e)
		{
			txtReportFile.Enabled = btnFile.Enabled;
		}

		/// <summary>
		/// When chkUseTemplate change, affect txtTemplateFile and btnTemplateFile Enabled state, too
		/// </summary>
		private void chkUseTemplate_CheckedChanged(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".chkUseTemplate_CheckedChanged()";
			try
			{
				// HACK: dungla 10-25-2005
				lblTemplateFile.ForeColor = (chkUseTemplate.Checked) ? Color.Maroon : DefaultForeColor;
				if(blnEditedReport == true)
					txtTemplateFile.Enabled = btnTemplateFile.Enabled = chkUseTemplate.Checked;
				// END: dungla 10-25-2005
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

		private void btnTemplateFile_Click(object sender, System.EventArgs e)
		{
			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.WaitCursor;

			#endregion Code Inserted Automatically

			
			const string METHOD_NAME = THIS + ".btnTemplateFile_Click()";
			try
			{
				if (dlgOpenTemplateFile.ShowDialog() == DialogResult.OK)
				{
					/// HACKED: Thachnn: get full path to mstrXMLTemplateFilePath
					/// put only the filename in the txtTemplateFile
					//mstrXMLTemplateFilePath = dlgOpenTemplateFile.FileName;
					txtTemplateFile.Text = Path.GetFileName(dlgOpenTemplateFile.FileName);
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

		private void btnTemplateFile_EnabledChanged(object sender, System.EventArgs e)
		{
			txtTemplateFile.Enabled = btnTemplateFile.Enabled;
		}

		private void txtTemplateFile_EnabledChanged(object sender, System.EventArgs e)
		{
			btnTemplateFile.Enabled = txtTemplateFile.Enabled;		
		}

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
		private void chkUseTemplate_EnabledChanged(object sender, System.EventArgs e)
		{
			if(blnEditedReport == true)// && ((CheckBox)sender).Enabled == true)
			{
				txtTemplateFile.Enabled = btnTemplateFile.Enabled = ((CheckBox)sender).Checked;
			}
		}

		private void txtTemplateFile_TextChanged(object sender, System.EventArgs e)
		{
			if(blnEditedReport && chkUseTemplate.Checked)
			{
				blnTemplateFileChanged = true;
			}
		}

		private void txtReportFile_TextChanged(object sender, System.EventArgs e)
		{
			if(blnEditedReport)// && chkUseTemplate.Checked)
			{
				blnReportFileChanged = true;
			}
		}


		#endregion

		#region Private Methods

		private bool SaveData()
		{
			// add new object or save the copied object
			if ((this.mEnumType == EnumAction.Add) || (this.mEnumType == EnumAction.Copy))
			{
				sys_ReportVO voReport = new sys_ReportVO();
				EditReportBO boEditReport = new EditReportBO();

				// prepare data for VO object
				voReport.ReportID = txtReportCode.Text.Trim();
				voReport.ReportName = txtReportName.Text.Trim();
				voReport.ReportType = cboReportType.SelectedItem.ToString().Trim();
				voReport.ISOCode = txtISOCode.Text.Trim();

				voReport.Description = txtDescription.Text.Trim();
				voReport.ReportFile = txtReportFile.Text.Trim();

				voReport.UseTemplate = chkUseTemplate.Checked;
				voReport.TemplateFile = txtTemplateFile.Text.Trim();
				voReport.Command =  txtCommand.Text.Trim();

				// REF: Thachnn: Command syntax for using the C# and DLL Reportfile
				// [Namespace]-[FullQualifyClassName]-[MethodName(parameter1, parameter2)]

				#region process the report file (C# file and DLL file)
				if((voReport.ReportType == Constants.CSHARP_FILE_REPORT || 
					voReport.ReportType == Constants.DLL_REPORT)
					&& blnReportFileChanged && txtReportFile.Text.Trim() != string.Empty)
				{
					// store in the database only the XML filename, exclude the path
					string strFileName = Path.GetFileName(txtReportFile.Text);
						
					if(!File.Exists(txtReportFile.Text.Trim()))
					{
						if(!File.Exists(mstrReportDefFolder + "\\" + strFileName))
						{
							PCSMessageBox.Show(ErrorCode.MESSAGE_REPORT_FILE_NOT_FOUND);
							return false;
						}
					}
					else
					{
						try
						{
							// Thachnn: if file existed in the ReportDefinition Folder, Set attribute file to archive
							if(File.Exists(mstrReportDefFolder + "\\" + strFileName))
								System.IO.File.SetAttributes(mstrReportDefFolder + "\\" + strFileName, FileAttributes.Archive);
							// COPY to PCS Working folder, overwrite if exist						
							System.IO.File.Copy(txtReportFile.Text,  mstrReportDefFolder + "\\" + strFileName,  true);
						}
						catch{}
					}
					voReport.ReportFile = strFileName.Trim();
				}
				#endregion
				
				#region process the template file				
				if(blnTemplateFileChanged && txtTemplateFile.Text.Trim() != string.Empty)
				{
					// store in the database only the XML filename, exclude the path
					string strXMLFileName = Path.GetFileName(txtTemplateFile.Text);
						
					if(!File.Exists(txtTemplateFile.Text.Trim()))
					{
						if(!File.Exists(mstrReportDefFolder + "\\" + strXMLFileName))
						{
							PCSMessageBox.Show(ErrorCode.MESSAGE_REPORT_TEMPLATE_FILE_NOT_FOUND);					
							return false;
						}
					}
					else
					{
						try
						{
							if(File.Exists(mstrReportDefFolder + "\\" + strXMLFileName))
							{
								// TODO: Thachnn: Set attribute file 							
								File.SetAttributes(mstrReportDefFolder + "\\" + strXMLFileName, FileAttributes.Archive);
							}
							// COPY to PCS Working folder, overwrite if exist						
							File.Copy(txtTemplateFile.Text,  mstrReportDefFolder + "\\" + strXMLFileName,  true);
						}
						catch{}
					}
					voReport.TemplateFile = strXMLFileName.Trim();
				}
				#endregion

					
				#region default margins, orient, paper size
				voReport.MarginTop = FormControlComponents.ConvertIncheToTwips(Constants.REPORT_DEFAULT_TOP / 2);
				voReport.MarginBottom = FormControlComponents.ConvertIncheToTwips(Constants.REPORT_DEFAULT_BOTTOM / 2);
				voReport.MarginLeft = FormControlComponents.ConvertIncheToTwips(Constants.REPORT_DEFAULT_LEFT / 2);
				voReport.MarginRight = FormControlComponents.ConvertIncheToTwips(Constants.REPORT_DEFAULT_RIGHT / 2);
				voReport.MarginGutter = 0;
				voReport.MarginGutterPos = Convert.ToBoolean(Constants.REPORT_DEFAULT_GUTTER_POSITION);

				/// TODO: Use value in Constant class
				voReport.Orientation = Constants.REPORT_DEFAULT_ORIENTATION;
				voReport.PaperSize = (int)System.Drawing.Printing.PaperKind.A4;	//Constants.REPORT_DEFAULT_PAPER_SIZE = 0;
				voReport.TableBorder = Constants.REPORT_DEFAULT_TABLE_BORDER;
				voReport.Signatures = string.Empty;//Constants.REPORT_DEFAULT_SIGNATURE;

				#endregion

				#region default fonts

				if (!chkUseTemplate.Checked)
				{
					voReport.FontReportHeader = FormControlComponents.GetReportFontString(lblDefaultReportHeaderFont.Font);
					voReport.FontReportFooter = FormControlComponents.GetReportFontString(lblDefaultReportFooterFont.Font);
					voReport.FontParameter = FormControlComponents.GetReportFontString(lblDefaultParameterFont.Font);
					voReport.FontPageHeader = FormControlComponents.GetReportFontString(lblDefaultPageHeaderFont.Font);
					voReport.FontGroupBy = FormControlComponents.GetReportFontString(lblDefaultGroupByFont.Font);
					voReport.FontDetail = FormControlComponents.GetReportFontString(lblDefaultDetailFont.Font);
					voReport.FontPageFooter = FormControlComponents.GetReportFontString(lblDefaultPageFooterFont.Font);
				}

				#endregion



				// add new object into database
				boEditReport.Add(voReport);

				#region update report treeview
					
				// add new report to selected group
				intReportOrder = boEditReport.AddReportToGroup(voReport.ReportID, mGroupID);
				// re-assign new report id
				mReportID = voReport.ReportID;
				// assign new object to form for use later
				mvoReport = voReport;

				#endregion
			}
				
				// edit object
			else if (this.mEnumType == EnumAction.Edit)
			{
				/// HACK: Thachnn : 24/10/2005
				/// If we use the OLDLINE, when update has problem (duplicate name), original mvoReport object will be affect and ReportManagement will display the wrong value (even it is not in the database)
				/// OLDLINE: sys_ReportVO voReport = this.mvoReport;
				sys_ReportVO voReport = new sys_ReportVO();
				/// ENDHACK: Thachnn : 24/10/2005

				EditReportBO boEditReport = new EditReportBO();
				// prepare data for VO object
				voReport.ReportID = txtReportCode.Text.Trim();
				voReport.ReportName = txtReportName.Text.Trim();
//				voReport.ReportNameVN = txtReportNameVN.Text.Trim();
//				voReport.ReportNameJP = txtReportNameJP.Text.Trim();
				voReport.ReportType = cboReportType.SelectedItem.ToString().Trim();
				voReport.ISOCode = txtISOCode.Text.Trim();
				voReport.Description = txtDescription.Text.Trim();
					
				voReport.ReportFile = txtReportFile.Text.Trim();					
				voReport.UseTemplate = chkUseTemplate.Checked;
				voReport.TemplateFile = txtTemplateFile.Text.Trim();				

				voReport.Command = mvoReport.Command;					
				voReport.FontDetail = mvoReport.FontDetail;
				voReport.FontGroupBy = mvoReport.FontGroupBy;
				voReport.FontPageFooter = mvoReport.FontPageFooter;
				voReport.FontPageHeader = mvoReport.FontPageHeader;
				voReport.FontParameter = mvoReport.FontParameter;
				voReport.FontReportFooter = mvoReport.FontReportFooter;
				voReport.FontReportHeader = mvoReport.FontReportHeader;					
				voReport.MarginBottom = mvoReport.MarginBottom;
				voReport.MarginGutter = mvoReport.MarginGutter;
				voReport.MarginGutterPos = mvoReport.MarginGutterPos;
				voReport.MarginLeft = mvoReport.MarginLeft;
				voReport.MarginRight = mvoReport.MarginRight;
				voReport.MarginTop = mvoReport.MarginTop;
				voReport.Orientation = mvoReport.Orientation;
				voReport.PaperSize = mvoReport.PaperSize;					
				voReport.Signatures = mvoReport.Signatures;
				voReport.TableBorder = mvoReport.TableBorder;


				#region process the report file (C# file and DLL file)
				if((voReport.ReportType == Constants.CSHARP_FILE_REPORT || 
					voReport.ReportType == Constants.DLL_REPORT)
					&& blnReportFileChanged && txtReportFile.Text.Trim() != string.Empty)
				{
					// store in the database only the XML filename, exclude the path
					string strFileName = Path.GetFileName(txtReportFile.Text);
						
					if(!File.Exists(txtReportFile.Text.Trim()))
					{
						if(!File.Exists(mstrReportDefFolder + "\\" + strFileName))
						{
							PCSMessageBox.Show(ErrorCode.MESSAGE_REPORT_FILE_NOT_FOUND);
							return false;
						}							
					}
					else
					{
						try
						{
							if(File.Exists(mstrReportDefFolder + "\\" + strFileName))
							{
								// Thachnn: if file existed, set attribute file to archive
								File.SetAttributes(mstrReportDefFolder + "\\" + strFileName, FileAttributes.Archive);
							}
							// COPY to PCS Working folder, overwrite if exist						
							File.Copy(txtReportFile.Text,  mstrReportDefFolder + "\\" + strFileName,  true);
						}
						catch{}
					}
					voReport.ReportFile = strFileName.Trim();
				}
				#endregion
				
				#region process the template file
				if(blnTemplateFileChanged && txtTemplateFile.Text.Trim() != string.Empty)
				{
					// store in the database only the XML filename, exclude the path
					string strXMLFileName = Path.GetFileName(txtTemplateFile.Text);
						
					if(!File.Exists(txtTemplateFile.Text.Trim()))
					{
						if(!File.Exists(mstrReportDefFolder + "\\" + strXMLFileName))
						{
							PCSMessageBox.Show(ErrorCode.MESSAGE_REPORT_TEMPLATE_FILE_NOT_FOUND);					
							return false;
						}
					}
					else
					{
						try
						{
							if(File.Exists(mstrReportDefFolder + "\\" + strXMLFileName))
							{
								// Thachnn: if file existed, set attribute file to archive
								File.SetAttributes(mstrReportDefFolder + "\\" + strXMLFileName, FileAttributes.Archive);
							}														
							// COPY to PCS Working folder, overwrite if exist
							File.Copy(txtTemplateFile.Text,  mstrReportDefFolder + "\\" + strXMLFileName,  true);
						}
						catch{}
					}
					voReport.TemplateFile = strXMLFileName.Trim();
				}
				#endregion

				voReport.Command = txtCommand.Text.Trim();

				// add new object into database
				boEditReport.Update(voReport);
				// assign new object to form for use later
				mvoReport = voReport;
			}			

			return true;
		}
	
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
		
		/// <summary>
		/// Allow user to edit information in form
		/// </summary>
		private void TurnOnEditableControls()
		{
			try
			{
				switch (mEnumType)
				{
					case EnumAction.Add:
						txtReportCode.Enabled = true;
						txtReportName.Enabled = true;
						txtReportNameVN.Enabled = true;
						txtReportNameJP.Enabled = true;
						cboReportType.Enabled = true;
						txtISOCode.Enabled = true;
						// txtReportFile.Enabled = ;
						// btnFile.Enabled = ;
						chkUseTemplate.Enabled = true;
						txtDescription.Enabled = true;
						txtCommand.Enabled = true;
						txtReportName.Focus();
						break;
					case EnumAction.Edit:
						txtReportName.Enabled = true;
						txtReportNameVN.Enabled = true;
						txtReportNameJP.Enabled = true;
						cboReportType.Enabled = true;
						txtISOCode.Enabled = true;

						if (cboReportType.SelectedItem != null)
						{
							if(cboReportType.SelectedItem.Equals(Constants.CSHARP_FILE_REPORT) ||
								cboReportType.SelectedItem.Equals(Constants.DLL_REPORT)   )
							{
								txtReportFile.Enabled = true;
								btnFile.Enabled = true;
							}
						}

						chkUseTemplate.Enabled = true;
						txtDescription.Enabled = true;
						txtCommand.Enabled = true;
						txtReportName.Focus();
						break;
					case EnumAction.Copy:
						txtReportCode.Enabled = true;
						txtReportName.Enabled = true;
						txtReportNameVN.Enabled = true;
						txtReportNameJP.Enabled = true;
						cboReportType.Enabled = true;
						txtISOCode.Enabled = true;
						// txtReportFile.Enabled = ;
						// btnFile.Enabled = ;
						chkUseTemplate.Enabled = true;
						txtDescription.Enabled = true;
						txtCommand.Enabled = true;

						if (mvoReport != null)
						{                    
							txtReportCode.Text = Constants.COPY_OF + mvoReport.ReportID;
							txtReportName.Text = Constants.COPY_OF + mvoReport.ReportName;
//							txtReportNameVN.Text = Constants.COPY_OF + mvoReport.ReportNameVN;
//							txtReportNameJP.Text = Constants.COPY_OF + mvoReport.ReportNameJP;
						}
						txtReportName.Focus();
						break;
				}
			}
			catch(Exception ex)
			{
				throw ex;
			}
		}
		
		/// <summary>
		/// Thachnn: 15/sep/2005
		/// Load data from value object to control in form
		/// </summary>
		private void LayoutOnFormValueInMvoObject()
		{
			try
			{			
				// load data to form
				if (mvoReport != null)
				{			
					cboReportType.SelectedItem = mvoReport.ReportType;			

					txtISOCode.Text = mvoReport.ISOCode;
					txtReportFile.Text = mvoReport.ReportFile;
					txtDescription.Text = mvoReport.Description;
					txtCommand.Text = mvoReport.Command;
					txtReportCode.Text = mvoReport.ReportID;						
					txtReportName.Text = mvoReport.ReportName;
//					txtReportNameVN.Text = mvoReport.ReportNameVN;
//					txtReportNameJP.Text = mvoReport.ReportNameJP;
					chkUseTemplate.Checked = mvoReport.UseTemplate;
					txtTemplateFile.Text = mvoReport.TemplateFile;
				}
			}
			catch(Exception ex)
			{
				throw ex;					 
			}
		}

		/// <summary>
		/// Call only from TurnOffSaveReportButton()
		/// </summary>		
		private void TurnOffEditableControls()
		{
			try
			{			
				txtReportCode.Enabled = false;
				txtReportName.Enabled = false;
				cboReportType.Enabled = false;
				txtISOCode.Enabled = false;
				txtReportFile.Enabled = false;
				btnFile.Enabled = false;
				//chkUseTemplate.Checked = false;
				chkUseTemplate.Enabled = false;
				txtTemplateFile.Enabled = false;
				btnTemplateFile.Enabled = false;

				txtDescription.Enabled = false;
				txtCommand.Enabled = false;
			}
			catch(Exception ex)
			{
				throw ex;
			}
		}

		/// <summary>
		/// Switch form mode based on enum action
		/// </summary>
		private void SwitchFormMode()
		{
			try
			{
				switch (mEnumType)
				{
					case EnumAction.Add:
						/// HACKED: Thachnn: fix bug 2118 when OpenForm, Title is not correct
						this.Text = lblAddNewReport.Text;
						/// ENDHACKED: Thachnn: fix bug 2118 when OpenForm, Title is not correct
						/// TurnOnEditableControls();
						btnEdit.Enabled = false;
						btnSave.Enabled = true;
						btnDelete.Enabled = false;
						btnDrillDownReport.Enabled = false;
						btnFieldGroup.Enabled = false;
						btnFieldProperties.Enabled = false;
						btnPageSetup.Enabled = false;
						btnParameter.Enabled = false;
						txtReportNameVN.Enabled = true;
						txtReportNameJP.Enabled = true;
						break;
					case EnumAction.Edit:
						TurnOnEditableControls();
						btnEdit.Enabled = false;
						btnSave.Enabled = true;
						btnDelete.Enabled = false;
						btnDrillDownReport.Enabled = false;
						btnFieldGroup.Enabled = false;
						btnFieldProperties.Enabled = false;
						btnPageSetup.Enabled = false;
						btnParameter.Enabled = false;
						txtReportNameVN.Enabled = true;
						txtReportNameJP.Enabled = true;
						break;
					case EnumAction.Default:
						TurnOffEditableControls();
						btnEdit.Enabled = true;
						btnSave.Enabled = false;
						btnDelete.Enabled = true;
						btnDrillDownReport.Enabled = true;
						btnFieldGroup.Enabled = true;
						btnFieldProperties.Enabled = true;
						btnPageSetup.Enabled = true;
						btnParameter.Enabled = true;
						txtReportNameVN.Enabled = false;
						txtReportNameJP.Enabled = false;

						/// HACKED: Thachnn: fix bug 2118 when OpenForm, Title is not correct
						this.Text = mFormCaption;
						/// ENDHACKED: Thachnn: fix bug 2118 when OpenForm, Title is not correct

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
		#endregion


	}
}
