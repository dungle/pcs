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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditReport));
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
            this.lblReportCode.ForeColor = System.Drawing.Color.Maroon;
            resources.ApplyResources(this.lblReportCode, "lblReportCode");
            this.lblReportCode.Name = "lblReportCode";
            // 
            // txtReportCode
            // 
            this.txtReportCode.ForeColor = System.Drawing.SystemColors.WindowText;
            resources.ApplyResources(this.txtReportCode, "txtReportCode");
            this.txtReportCode.Name = "txtReportCode";
            this.txtReportCode.ReadOnly = true;
            this.txtReportCode.Leave += new System.EventHandler(this.OnLeaveControl);
            // 
            // txtReportName
            // 
            this.txtReportName.ForeColor = System.Drawing.SystemColors.WindowText;
            resources.ApplyResources(this.txtReportName, "txtReportName");
            this.txtReportName.Name = "txtReportName";
            this.txtReportName.Leave += new System.EventHandler(this.OnLeaveControl);
            // 
            // lblReportName
            // 
            this.lblReportName.ForeColor = System.Drawing.Color.Maroon;
            resources.ApplyResources(this.lblReportName, "lblReportName");
            this.lblReportName.Name = "lblReportName";
            // 
            // txtCommand
            // 
            resources.ApplyResources(this.txtCommand, "txtCommand");
            this.txtCommand.Name = "txtCommand";
            this.txtCommand.Leave += new System.EventHandler(this.OnLeaveControl);
            // 
            // lblCommand
            // 
            resources.ApplyResources(this.lblCommand, "lblCommand");
            this.lblCommand.Name = "lblCommand";
            // 
            // txtReportFile
            // 
            resources.ApplyResources(this.txtReportFile, "txtReportFile");
            this.txtReportFile.Name = "txtReportFile";
            this.txtReportFile.EnabledChanged += new System.EventHandler(this.txtReportFile_EnabledChanged);
            this.txtReportFile.TextChanged += new System.EventHandler(this.txtReportFile_TextChanged);
            this.txtReportFile.Leave += new System.EventHandler(this.OnLeaveControl);
            // 
            // lblReportFile
            // 
            resources.ApplyResources(this.lblReportFile, "lblReportFile");
            this.lblReportFile.Name = "lblReportFile";
            // 
            // txtISOCode
            // 
            resources.ApplyResources(this.txtISOCode, "txtISOCode");
            this.txtISOCode.Name = "txtISOCode";
            this.txtISOCode.Leave += new System.EventHandler(this.OnLeaveControl);
            // 
            // lblISO_Code
            // 
            resources.ApplyResources(this.lblISO_Code, "lblISO_Code");
            this.lblISO_Code.Name = "lblISO_Code";
            // 
            // btnFile
            // 
            resources.ApplyResources(this.btnFile, "btnFile");
            this.btnFile.Name = "btnFile";
            this.btnFile.EnabledChanged += new System.EventHandler(this.btnFile_EnabledChanged);
            this.btnFile.Click += new System.EventHandler(this.btnFile_Click);
            // 
            // lblReportType
            // 
            this.lblReportType.ForeColor = System.Drawing.Color.Maroon;
            resources.ApplyResources(this.lblReportType, "lblReportType");
            this.lblReportType.Name = "lblReportType";
            // 
            // cboReportType
            // 
            this.cboReportType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.cboReportType, "cboReportType");
            this.cboReportType.Items.AddRange(new object[] {
            resources.GetString("cboReportType.Items"),
            resources.GetString("cboReportType.Items1"),
            resources.GetString("cboReportType.Items2"),
            resources.GetString("cboReportType.Items3")});
            this.cboReportType.Name = "cboReportType";
            this.cboReportType.SelectedIndexChanged += new System.EventHandler(this.cboReportType_SelectedIndexChanged);
            this.cboReportType.Leave += new System.EventHandler(this.OnLeaveControl);
            // 
            // btnParameter
            // 
            resources.ApplyResources(this.btnParameter, "btnParameter");
            this.btnParameter.Name = "btnParameter";
            this.btnParameter.Click += new System.EventHandler(this.btnParameter_Click);
            // 
            // btnDrillDownReport
            // 
            resources.ApplyResources(this.btnDrillDownReport, "btnDrillDownReport");
            this.btnDrillDownReport.Name = "btnDrillDownReport";
            this.btnDrillDownReport.Click += new System.EventHandler(this.btnDrillDownReport_Click);
            // 
            // btnPageSetup
            // 
            resources.ApplyResources(this.btnPageSetup, "btnPageSetup");
            this.btnPageSetup.Name = "btnPageSetup";
            this.btnPageSetup.Click += new System.EventHandler(this.btnPageSetup_Click);
            // 
            // grbReportDetail
            // 
            this.grbReportDetail.Controls.Add(this.btnDrillDownReport);
            this.grbReportDetail.Controls.Add(this.btnParameter);
            this.grbReportDetail.Controls.Add(this.btnPageSetup);
            this.grbReportDetail.Controls.Add(this.btnFieldProperties);
            this.grbReportDetail.Controls.Add(this.btnFieldGroup);
            resources.ApplyResources(this.grbReportDetail, "grbReportDetail");
            this.grbReportDetail.Name = "grbReportDetail";
            this.grbReportDetail.TabStop = false;
            // 
            // btnFieldProperties
            // 
            resources.ApplyResources(this.btnFieldProperties, "btnFieldProperties");
            this.btnFieldProperties.Name = "btnFieldProperties";
            this.btnFieldProperties.Click += new System.EventHandler(this.btnFieldProperties_Click);
            // 
            // btnFieldGroup
            // 
            resources.ApplyResources(this.btnFieldGroup, "btnFieldGroup");
            this.btnFieldGroup.Name = "btnFieldGroup";
            this.btnFieldGroup.Click += new System.EventHandler(this.btnFieldGroup_Click);
            // 
            // btnHelp
            // 
            resources.ApplyResources(this.btnHelp, "btnHelp");
            this.btnHelp.Name = "btnHelp";
            // 
            // btnEdit
            // 
            resources.ApplyResources(this.btnEdit, "btnEdit");
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnSave
            // 
            resources.ApplyResources(this.btnSave, "btnSave");
            this.btnSave.Name = "btnSave";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnDelete
            // 
            resources.ApplyResources(this.btnDelete, "btnDelete");
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnClose
            // 
            resources.ApplyResources(this.btnClose, "btnClose");
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Name = "btnClose";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // txtDescription
            // 
            resources.ApplyResources(this.txtDescription, "txtDescription");
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Leave += new System.EventHandler(this.OnLeaveControl);
            // 
            // lblDescription
            // 
            resources.ApplyResources(this.lblDescription, "lblDescription");
            this.lblDescription.Name = "lblDescription";
            // 
            // lblGroupID
            // 
            resources.ApplyResources(this.lblGroupID, "lblGroupID");
            this.lblGroupID.Name = "lblGroupID";
            // 
            // txtTemplateFile
            // 
            resources.ApplyResources(this.txtTemplateFile, "txtTemplateFile");
            this.txtTemplateFile.Name = "txtTemplateFile";
            this.txtTemplateFile.EnabledChanged += new System.EventHandler(this.txtTemplateFile_EnabledChanged);
            this.txtTemplateFile.TextChanged += new System.EventHandler(this.txtTemplateFile_TextChanged);
            // 
            // btnTemplateFile
            // 
            resources.ApplyResources(this.btnTemplateFile, "btnTemplateFile");
            this.btnTemplateFile.Name = "btnTemplateFile";
            this.btnTemplateFile.EnabledChanged += new System.EventHandler(this.btnTemplateFile_EnabledChanged);
            this.btnTemplateFile.Click += new System.EventHandler(this.btnTemplateFile_Click);
            // 
            // lblTemplateFile
            // 
            resources.ApplyResources(this.lblTemplateFile, "lblTemplateFile");
            this.lblTemplateFile.Name = "lblTemplateFile";
            // 
            // chkUseTemplate
            // 
            this.chkUseTemplate.Checked = true;
            this.chkUseTemplate.CheckState = System.Windows.Forms.CheckState.Checked;
            resources.ApplyResources(this.chkUseTemplate, "chkUseTemplate");
            this.chkUseTemplate.Name = "chkUseTemplate";
            this.chkUseTemplate.CheckedChanged += new System.EventHandler(this.chkUseTemplate_CheckedChanged);
            this.chkUseTemplate.EnabledChanged += new System.EventHandler(this.chkUseTemplate_EnabledChanged);
            // 
            // dlgOpenTemplateFile
            // 
            resources.ApplyResources(this.dlgOpenTemplateFile, "dlgOpenTemplateFile");
            // 
            // lblAddNewReport
            // 
            resources.ApplyResources(this.lblAddNewReport, "lblAddNewReport");
            this.lblAddNewReport.Name = "lblAddNewReport";
            // 
            // lblDefaultParameterFont
            // 
            this.lblDefaultParameterFont.BackColor = System.Drawing.SystemColors.ControlLightLight;
            resources.ApplyResources(this.lblDefaultParameterFont, "lblDefaultParameterFont");
            this.lblDefaultParameterFont.Name = "lblDefaultParameterFont";
            // 
            // lblDefaultReportHeaderFont
            // 
            this.lblDefaultReportHeaderFont.BackColor = System.Drawing.SystemColors.ControlLightLight;
            resources.ApplyResources(this.lblDefaultReportHeaderFont, "lblDefaultReportHeaderFont");
            this.lblDefaultReportHeaderFont.Name = "lblDefaultReportHeaderFont";
            // 
            // lblDefaultPageHeaderFont
            // 
            this.lblDefaultPageHeaderFont.BackColor = System.Drawing.SystemColors.ControlLightLight;
            resources.ApplyResources(this.lblDefaultPageHeaderFont, "lblDefaultPageHeaderFont");
            this.lblDefaultPageHeaderFont.Name = "lblDefaultPageHeaderFont";
            // 
            // lblDefaultGroupByFont
            // 
            this.lblDefaultGroupByFont.BackColor = System.Drawing.SystemColors.ControlLightLight;
            resources.ApplyResources(this.lblDefaultGroupByFont, "lblDefaultGroupByFont");
            this.lblDefaultGroupByFont.Name = "lblDefaultGroupByFont";
            // 
            // lblDefaultPageFooterFont
            // 
            this.lblDefaultPageFooterFont.BackColor = System.Drawing.SystemColors.ControlLightLight;
            resources.ApplyResources(this.lblDefaultPageFooterFont, "lblDefaultPageFooterFont");
            this.lblDefaultPageFooterFont.Name = "lblDefaultPageFooterFont";
            // 
            // lblDefaultReportFooterFont
            // 
            this.lblDefaultReportFooterFont.BackColor = System.Drawing.SystemColors.ControlLightLight;
            resources.ApplyResources(this.lblDefaultReportFooterFont, "lblDefaultReportFooterFont");
            this.lblDefaultReportFooterFont.Name = "lblDefaultReportFooterFont";
            // 
            // lblDefaultDetailFont
            // 
            this.lblDefaultDetailFont.BackColor = System.Drawing.SystemColors.ControlLightLight;
            resources.ApplyResources(this.lblDefaultDetailFont, "lblDefaultDetailFont");
            this.lblDefaultDetailFont.Name = "lblDefaultDetailFont";
            // 
            // txtReportNameVN
            // 
            this.txtReportNameVN.ForeColor = System.Drawing.SystemColors.WindowText;
            resources.ApplyResources(this.txtReportNameVN, "txtReportNameVN");
            this.txtReportNameVN.Name = "txtReportNameVN";
            // 
            // label1
            // 
            this.label1.ForeColor = System.Drawing.Color.Maroon;
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // txtReportNameJP
            // 
            this.txtReportNameJP.ForeColor = System.Drawing.SystemColors.WindowText;
            resources.ApplyResources(this.txtReportNameJP, "txtReportNameJP");
            this.txtReportNameJP.Name = "txtReportNameJP";
            // 
            // label2
            // 
            this.label2.ForeColor = System.Drawing.Color.Maroon;
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // EditReport
            // 
            resources.ApplyResources(this, "$this");
            this.CancelButton = this.btnClose;
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
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "EditReport";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.EditReport_Closing);
            this.Load += new System.EventHandler(this.EditReport_Load);
            this.grbReportDetail.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

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
