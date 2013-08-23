using System;
using System.IO;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

using C1.Win.C1TrueDBGrid;

//Using PCS's namespaces
using PCSComUtils.Common;
using PCSComUtils.Common.BO;
using PCSComUtils.Framework.ReportFrame.BO;
using PCSComUtils.MasterSetup.DS;
using PCSComUtils.PCSExc;
using PCSUtils.Framework.ReportFrame;
using PCSUtils.Log;
using PCSUtils.Utils;

namespace PCSReport.Materials
{
	/// <summary>
	/// Render Order Summary Report.
	/// </summary>
	public class ActualCostRelatedReports : System.Windows.Forms.Form
	{
		#region Declaration
		
		#region Generated Declaration
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.Windows.Forms.Button btnClose;
		private C1.Win.C1List.C1Combo cboCCN;
		private System.Windows.Forms.ComboBox cboYear;
		private System.Windows.Forms.Label lblCCN;
		private System.Windows.Forms.Label lblYear;
		private System.Windows.Forms.Button btnHelp;
		private System.Windows.Forms.Label lblCategory;
		private System.Windows.Forms.TextBox txtCategory;
		private System.Windows.Forms.TextBox txtModel;
		private System.Windows.Forms.Label lblModel;
		private System.Windows.Forms.Label lblDepartment;
		private System.Windows.Forms.ComboBox cboMonth;
		private System.Windows.Forms.Label lblMonth;
		private System.Windows.Forms.Label lblProductionLine;
		private System.Windows.Forms.Label lblPartNo;
		private System.Windows.Forms.Label lblPartName;
		private System.Windows.Forms.TextBox txtDepartment;
		private System.Windows.Forms.Button btnPartNo;
		private System.Windows.Forms.TextBox txtPartNo;
		private System.Windows.Forms.TextBox txtProductionLine;
		private System.Windows.Forms.Button btnProductionLine;
		private System.Windows.Forms.TextBox txtPartName;
		private System.Windows.Forms.Button btnPartName;
		private System.Windows.Forms.Button btnDepartment;
		private System.Windows.Forms.Button btnCategory;
		private System.Windows.Forms.Button btnPrint;
		private System.Windows.Forms.Button btnPrintConfiguration;
		private System.ComponentModel.Container components = null;

		#endregion Generated Declaration
		
		#region Constants
		
		private const string THIS = "PCSMaterials.ActualCost.ActualCostRelatedReports";
		private const int MAX_LENGTH = 100;
		private const string APPLICATION_PATH     = @"PCSMain\bin\Debug";
        private System.Windows.Forms.CheckBox chkMakeItem;
        private C1.C1Report.C1Report c1Report1;
        private C1.Win.C1Preview.C1PrintPreviewControl c1PrintPreviewControl1;
		private const string EMPTY_STRING = "(0)";

		#endregion Constants

		#endregion Declaration
		
		#region Constructor, Destructor
		
		public ActualCostRelatedReports()
		{			
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
		
		#endregion Constructor, Destructor

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ActualCostRelatedReports));
            this.btnClose = new System.Windows.Forms.Button();
            this.cboCCN = new C1.Win.C1List.C1Combo();
            this.lblCCN = new System.Windows.Forms.Label();
            this.cboYear = new System.Windows.Forms.ComboBox();
            this.cboMonth = new System.Windows.Forms.ComboBox();
            this.txtModel = new System.Windows.Forms.TextBox();
            this.lblModel = new System.Windows.Forms.Label();
            this.btnCategory = new System.Windows.Forms.Button();
            this.txtCategory = new System.Windows.Forms.TextBox();
            this.lblCategory = new System.Windows.Forms.Label();
            this.btnDepartment = new System.Windows.Forms.Button();
            this.txtDepartment = new System.Windows.Forms.TextBox();
            this.lblDepartment = new System.Windows.Forms.Label();
            this.btnPartNo = new System.Windows.Forms.Button();
            this.txtPartNo = new System.Windows.Forms.TextBox();
            this.lblPartNo = new System.Windows.Forms.Label();
            this.lblMonth = new System.Windows.Forms.Label();
            this.lblYear = new System.Windows.Forms.Label();
            this.btnHelp = new System.Windows.Forms.Button();
            this.txtProductionLine = new System.Windows.Forms.TextBox();
            this.lblProductionLine = new System.Windows.Forms.Label();
            this.btnProductionLine = new System.Windows.Forms.Button();
            this.txtPartName = new System.Windows.Forms.TextBox();
            this.lblPartName = new System.Windows.Forms.Label();
            this.btnPartName = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnPrintConfiguration = new System.Windows.Forms.Button();
            this.chkMakeItem = new System.Windows.Forms.CheckBox();
            this.c1Report1 = new C1.C1Report.C1Report();
            this.c1PrintPreviewControl1 = new C1.Win.C1Preview.C1PrintPreviewControl();
            ((System.ComponentModel.ISupportInitialize)(this.cboCCN)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1Report1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1PrintPreviewControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1PrintPreviewControl1.PreviewPane)).BeginInit();
            this.c1PrintPreviewControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            resources.ApplyResources(this.btnClose, "btnClose");
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Name = "btnClose";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // cboCCN
            // 
            this.cboCCN.AddItemSeparator = ';';
            resources.ApplyResources(this.cboCCN, "cboCCN");
            this.cboCCN.CaptionHeight = 17;
            this.cboCCN.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.cboCCN.ColumnCaptionHeight = 17;
            this.cboCCN.ColumnFooterHeight = 17;
            this.cboCCN.ComboStyle = C1.Win.C1List.ComboStyleEnum.DropdownList;
            this.cboCCN.ContentHeight = 15;
            this.cboCCN.DeadAreaBackColor = System.Drawing.Color.Empty;
            this.cboCCN.DropDownWidth = 200;
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
            this.cboCCN.PropBag = resources.GetString("cboCCN.PropBag");
            // 
            // lblCCN
            // 
            resources.ApplyResources(this.lblCCN, "lblCCN");
            this.lblCCN.ForeColor = System.Drawing.Color.Maroon;
            this.lblCCN.Name = "lblCCN";
            // 
            // cboYear
            // 
            this.cboYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.cboYear, "cboYear");
            this.cboYear.Name = "cboYear";
            this.cboYear.SelectedIndexChanged += new System.EventHandler(this.cboYear_SelectedIndexChanged);
            // 
            // cboMonth
            // 
            this.cboMonth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.cboMonth, "cboMonth");
            this.cboMonth.Name = "cboMonth";
            // 
            // txtModel
            // 
            resources.ApplyResources(this.txtModel, "txtModel");
            this.txtModel.Name = "txtModel";
            this.txtModel.ReadOnly = true;
            // 
            // lblModel
            // 
            resources.ApplyResources(this.lblModel, "lblModel");
            this.lblModel.Name = "lblModel";
            // 
            // btnCategory
            // 
            resources.ApplyResources(this.btnCategory, "btnCategory");
            this.btnCategory.Name = "btnCategory";
            this.btnCategory.Click += new System.EventHandler(this.btnCategory_Click);
            // 
            // txtCategory
            // 
            resources.ApplyResources(this.txtCategory, "txtCategory");
            this.txtCategory.Name = "txtCategory";
            this.txtCategory.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCategory_KeyDown);
            this.txtCategory.Validating += new System.ComponentModel.CancelEventHandler(this.txtCategory_Validating);
            // 
            // lblCategory
            // 
            resources.ApplyResources(this.lblCategory, "lblCategory");
            this.lblCategory.Name = "lblCategory";
            // 
            // btnDepartment
            // 
            resources.ApplyResources(this.btnDepartment, "btnDepartment");
            this.btnDepartment.Name = "btnDepartment";
            this.btnDepartment.Click += new System.EventHandler(this.btnDepartment_Click);
            // 
            // txtDepartment
            // 
            resources.ApplyResources(this.txtDepartment, "txtDepartment");
            this.txtDepartment.Name = "txtDepartment";
            this.txtDepartment.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtDepartment_KeyDown);
            this.txtDepartment.Validating += new System.ComponentModel.CancelEventHandler(this.txtDepartment_Validating);
            // 
            // lblDepartment
            // 
            resources.ApplyResources(this.lblDepartment, "lblDepartment");
            this.lblDepartment.Name = "lblDepartment";
            // 
            // btnPartNo
            // 
            resources.ApplyResources(this.btnPartNo, "btnPartNo");
            this.btnPartNo.Name = "btnPartNo";
            this.btnPartNo.Click += new System.EventHandler(this.btnPartNo_Click);
            // 
            // txtPartNo
            // 
            resources.ApplyResources(this.txtPartNo, "txtPartNo");
            this.txtPartNo.Name = "txtPartNo";
            this.txtPartNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPartNo_KeyDown);
            this.txtPartNo.Validating += new System.ComponentModel.CancelEventHandler(this.txtPartNo_Validating);
            // 
            // lblPartNo
            // 
            resources.ApplyResources(this.lblPartNo, "lblPartNo");
            this.lblPartNo.Name = "lblPartNo";
            // 
            // lblMonth
            // 
            this.lblMonth.ForeColor = System.Drawing.SystemColors.ControlText;
            resources.ApplyResources(this.lblMonth, "lblMonth");
            this.lblMonth.Name = "lblMonth";
            // 
            // lblYear
            // 
            this.lblYear.ForeColor = System.Drawing.Color.Maroon;
            resources.ApplyResources(this.lblYear, "lblYear");
            this.lblYear.Name = "lblYear";
            // 
            // btnHelp
            // 
            resources.ApplyResources(this.btnHelp, "btnHelp");
            this.btnHelp.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Click += new System.EventHandler(this.btnHelp_Click);
            // 
            // txtProductionLine
            // 
            resources.ApplyResources(this.txtProductionLine, "txtProductionLine");
            this.txtProductionLine.Name = "txtProductionLine";
            this.txtProductionLine.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtProductionLine_KeyDown);
            this.txtProductionLine.Validating += new System.ComponentModel.CancelEventHandler(this.txtProductionLine_Validating);
            // 
            // lblProductionLine
            // 
            resources.ApplyResources(this.lblProductionLine, "lblProductionLine");
            this.lblProductionLine.Name = "lblProductionLine";
            // 
            // btnProductionLine
            // 
            resources.ApplyResources(this.btnProductionLine, "btnProductionLine");
            this.btnProductionLine.Name = "btnProductionLine";
            this.btnProductionLine.Click += new System.EventHandler(this.btnProductionLine_Click);
            // 
            // txtPartName
            // 
            resources.ApplyResources(this.txtPartName, "txtPartName");
            this.txtPartName.Name = "txtPartName";
            this.txtPartName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPartName_KeyDown);
            this.txtPartName.Validating += new System.ComponentModel.CancelEventHandler(this.txtPartName_Validating);
            // 
            // lblPartName
            // 
            resources.ApplyResources(this.lblPartName, "lblPartName");
            this.lblPartName.Name = "lblPartName";
            // 
            // btnPartName
            // 
            resources.ApplyResources(this.btnPartName, "btnPartName");
            this.btnPartName.Name = "btnPartName";
            this.btnPartName.Click += new System.EventHandler(this.btnPartName_Click);
            // 
            // btnPrint
            // 
            resources.ApplyResources(this.btnPrint, "btnPrint");
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.EnabledChanged += new System.EventHandler(this.btnPrint_EnabledChanged);
            // 
            // btnPrintConfiguration
            // 
            resources.ApplyResources(this.btnPrintConfiguration, "btnPrintConfiguration");
            this.btnPrintConfiguration.Name = "btnPrintConfiguration";
            // 
            // chkMakeItem
            // 
            resources.ApplyResources(this.chkMakeItem, "chkMakeItem");
            this.chkMakeItem.Name = "chkMakeItem";
            // 
            // c1Report1
            // 
            this.c1Report1.ReportDefinition = resources.GetString("c1Report1.ReportDefinition");
            this.c1Report1.ReportName = "c1Report1";
            // 
            // c1PrintPreviewControl1
            // 
            resources.ApplyResources(this.c1PrintPreviewControl1, "c1PrintPreviewControl1");
            this.c1PrintPreviewControl1.Name = "c1PrintPreviewControl1";
            // 
            // c1PrintPreviewControl1.OutlineView
            // 
            resources.ApplyResources(this.c1PrintPreviewControl1.PreviewOutlineView, "c1PrintPreviewControl1.OutlineView");
            this.c1PrintPreviewControl1.PreviewOutlineView.Name = "OutlineView";
            // 
            // c1PrintPreviewControl1.PreviewPane
            // 
            this.c1PrintPreviewControl1.PreviewPane.IntegrateExternalTools = true;
            resources.ApplyResources(this.c1PrintPreviewControl1.PreviewPane, "c1PrintPreviewControl1.PreviewPane");
            // 
            // c1PrintPreviewControl1.PreviewTextSearchPanel
            // 
            resources.ApplyResources(this.c1PrintPreviewControl1.PreviewTextSearchPanel, "c1PrintPreviewControl1.PreviewTextSearchPanel");
            this.c1PrintPreviewControl1.PreviewTextSearchPanel.MinimumSize = new System.Drawing.Size(200, 240);
            this.c1PrintPreviewControl1.PreviewTextSearchPanel.Name = "PreviewTextSearchPanel";
            // 
            // c1PrintPreviewControl1.ThumbnailView
            // 
            resources.ApplyResources(this.c1PrintPreviewControl1.PreviewThumbnailView, "c1PrintPreviewControl1.ThumbnailView");
            this.c1PrintPreviewControl1.PreviewThumbnailView.Name = "ThumbnailView";
            this.c1PrintPreviewControl1.PreviewThumbnailView.UseImageAsThumbnail = false;
            // 
            // ActualCostRelatedReports
            // 
            resources.ApplyResources(this, "$this");
            this.CancelButton = this.btnClose;
            this.Controls.Add(this.c1PrintPreviewControl1);
            this.Controls.Add(this.chkMakeItem);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.btnPrintConfiguration);
            this.Controls.Add(this.txtPartName);
            this.Controls.Add(this.txtProductionLine);
            this.Controls.Add(this.txtPartNo);
            this.Controls.Add(this.txtCategory);
            this.Controls.Add(this.txtModel);
            this.Controls.Add(this.txtDepartment);
            this.Controls.Add(this.lblPartName);
            this.Controls.Add(this.btnPartName);
            this.Controls.Add(this.lblProductionLine);
            this.Controls.Add(this.btnProductionLine);
            this.Controls.Add(this.cboYear);
            this.Controls.Add(this.cboMonth);
            this.Controls.Add(this.btnHelp);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.lblMonth);
            this.Controls.Add(this.lblCategory);
            this.Controls.Add(this.cboCCN);
            this.Controls.Add(this.lblCCN);
            this.Controls.Add(this.lblModel);
            this.Controls.Add(this.lblYear);
            this.Controls.Add(this.btnCategory);
            this.Controls.Add(this.lblPartNo);
            this.Controls.Add(this.btnDepartment);
            this.Controls.Add(this.lblDepartment);
            this.Controls.Add(this.btnPartNo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "ActualCostRelatedReports";
            this.Load += new System.EventHandler(this.ActualCostRelatedReports_Load);
            ((System.ComponentModel.ISupportInitialize)(this.cboCCN)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1Report1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1PrintPreviewControl1.PreviewPane)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1PrintPreviewControl1)).EndInit();
            this.c1PrintPreviewControl1.ResumeLayout(false);
            this.c1PrintPreviewControl1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion
		
		#region Class's Methods
		
		
		/// <summary>
		/// Create and show Salvaging Material Report
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ShowSalvagingMaterialReport(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".ShowSalvagingMaterialReport()";	

			const string REPORT_TEMPLATE_FILE = "SalvagingMaterialReport.xml";		
			const string REPORT_NAME		  = "SalvagingMaterialReport";

			//Report field names
			const string RPT_CCN = "CCN";
			const string RPT_YEAR	   = "Year";
			const string RPT_MONTH  = "Month";
			const string RPT_CATEGORY    = "Category";
			const string RPT_DEPARTMENT      = "Department";
			const string RPT_PRODUCTIONLINE   = "Production Line";
			const string RPT_PART_NO = "Part No.";
			const string RPT_PART_NAME = "Part Name";
			const string RPT_MODEL = "Model";
			const string RPT_COMPANY  = "fldCompany";
			const string RPT_HOME_CURRENCY  = "fldHomeCurrency";

			const string RPT_PAGE_HEADER = "PageHeader";			
			const string RPT_TITLE_FIELD = "fldTitle";

			try
			{				
				string strReportPath = Application.StartupPath;

				//Validate data; user must select CCN first
				if(cboCCN.SelectedValue == null || cboCCN.Text.Trim() == string.Empty)
				{					
					string[] arrParams = {lblCCN.Text};
					PCSMessageBox.Show(ErrorCode.MESSAGE_MANDATORY_FIELD_REQUIRED, MessageBoxIcon.Exclamation, arrParams);					
					cboCCN.Focus();
					return;
				}

				//Check if user does not select Year
				if(cboYear.Text.Trim() == string.Empty)
				{				
					string[] arrParams = {lblYear.Text};
					PCSMessageBox.Show(ErrorCode.MESSAGE_MANDATORY_FIELD_REQUIRED, MessageBoxIcon.Exclamation, arrParams);
					cboYear.Focus();					
					return;
				}				
				
				if(cboMonth.Text.Trim() == string.Empty)
				{
					string[] arrParams = {lblMonth.Text};
					PCSMessageBox.Show(ErrorCode.MESSAGE_MANDATORY_FIELD_REQUIRED, MessageBoxIcon.Exclamation, arrParams);
					cboMonth.Focus();
					return;
				}

				//Change cursor to wait
				this.Cursor = Cursors.WaitCursor;
				
				int intIndex = strReportPath.IndexOf(APPLICATION_PATH);
				
				//Validate report layout path
				if(intIndex > -1)
				{
					strReportPath = strReportPath.Substring(0, intIndex);
				}

				if(strReportPath.Substring(strReportPath.Length -1) == @"\")
				{
					strReportPath += Constants.REPORT_DEFINITION_STORE_LOCATION;
				}
				else
				{
					strReportPath += @"\" + Constants.REPORT_DEFINITION_STORE_LOCATION;
				}				
				
				// check report layout file is exist or not
				if(!File.Exists(strReportPath + @"\" + REPORT_TEMPLATE_FILE))
				{
					this.Cursor = Cursors.Default;
					PCSMessageBox.Show(ErrorCode.MESSAGE_REPORT_TEMPLATE_FILE_NOT_FOUND, MessageBoxIcon.Error);					
					return;
				}
				
				//Get Data based on supplied condition
				DataTable dtbData = GetSalvagingMaterial();

				//Create builder object
				ReportWithSubReportBuilder reportBuilder = new ReportWithSubReportBuilder();
				//Set report name
				reportBuilder.ReportName = REPORT_NAME;
				//Set Datasource
				reportBuilder.SourceDataTable = dtbData;
				//Set report layout location
				reportBuilder.ReportDefinitionFolder = strReportPath;
				reportBuilder.ReportLayoutFile = REPORT_TEMPLATE_FILE;				
				
				reportBuilder.UseLayoutFile = true;
				reportBuilder.MakeDataTableForRender();			

				// and show it in preview dialog				
				PCSUtils.Framework.ReportFrame.C1PrintPreviewDialog	printPreview = new PCSUtils.Framework.ReportFrame.C1PrintPreviewDialog();
								
				//Attach report viewer
				reportBuilder.ReportViewer = printPreview.ReportViewer;				
				reportBuilder.RenderReport();				
				
				//Draw parameters				
				System.Collections.Specialized.NameValueCollection arrParamAndValue = new System.Collections.Specialized.NameValueCollection();
				arrParamAndValue.Add(RPT_CCN, cboCCN.Text);
				arrParamAndValue.Add(RPT_YEAR, cboYear.Text);
				arrParamAndValue.Add(RPT_MONTH, cboMonth.Text);

				if(txtDepartment.Text != string.Empty)
				{
					arrParamAndValue.Add(RPT_DEPARTMENT, (txtDepartment.Text.Length > MAX_LENGTH)?txtDepartment.Text.Substring(0, MAX_LENGTH) + "...":txtDepartment.Text);
				}

				if(txtProductionLine.Text != string.Empty)
				{
					arrParamAndValue.Add(RPT_PRODUCTIONLINE, (txtProductionLine.Text.Length > MAX_LENGTH)?txtProductionLine.Text.Substring(0, MAX_LENGTH) + "...":txtProductionLine.Text);
				}

				if(txtCategory.Text != string.Empty)
				{
					arrParamAndValue.Add(RPT_CATEGORY, (txtCategory.Text.Length > MAX_LENGTH)?txtCategory.Text.Substring(0, MAX_LENGTH) + "...":txtCategory.Text);
				}

				if(txtPartNo.Text != string.Empty)
				{
					arrParamAndValue.Add(RPT_PART_NO, (txtPartNo.Text.Length > MAX_LENGTH)?txtPartNo.Text.Substring(0, MAX_LENGTH) + "...":txtPartNo.Text);
				}

				if(txtPartName.Text != string.Empty)
				{
					arrParamAndValue.Add(RPT_PART_NAME, (txtPartName.Text.Length > MAX_LENGTH)?txtPartName.Text.Substring(0, MAX_LENGTH) + "...":txtPartName.Text);
				}

				if(txtModel.Text != string.Empty)
				{
					arrParamAndValue.Add(RPT_MODEL, (txtModel.Text.Length > MAX_LENGTH)?txtModel.Text.Substring(0, MAX_LENGTH) + "...":txtModel.Text);
				}
				
				//Anchor the Parameter drawing canvas cordinate to the fldTitle
				C1.C1Report.Field fldTitle = reportBuilder.GetFieldByName(RPT_TITLE_FIELD);
				double dblStartX = fldTitle.Left;
				double dblStartY = fldTitle.Top  + 1.3 * fldTitle.RenderHeight;
				reportBuilder.GetSectionByName(RPT_PAGE_HEADER).CanGrow = true;
				reportBuilder.DrawParameters(reportBuilder.GetSectionByName(RPT_PAGE_HEADER), dblStartX, dblStartY, arrParamAndValue, reportBuilder.Report.Font.Size);
				
				//Change form title
				try
				{
					printPreview.FormTitle = reportBuilder.GetFieldByName(RPT_TITLE_FIELD).Text + Constants.WHITE_SPACE + cboYear.Text;
				}
				catch{}
				
				//Company & Home Currency Info
				reportBuilder.DrawPredefinedField(RPT_COMPANY, SystemProperty.SytemParams.Get(SystemParam.COMPANY_FULL_NAME));
				reportBuilder.DrawPredefinedField(RPT_HOME_CURRENCY, SystemProperty.HomeCurrency);

				reportBuilder.RefreshReport();				
				
				printPreview.Show();

				// display report data to a form
				ReportData frmReportData = new ReportData();
				frmReportData.Data = dtbData;
				frmReportData.Show();
			}
			catch(Exception ex)
			{				
				//
				PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxIcon.Error);
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
			finally
			{
				this.Cursor = Cursors.Default;
			}
		}
		
		
		/// <summary>
		/// Build condition & get Salvaging Material data
		/// </summary>
		/// <returns></returns>
		private DataTable GetSalvagingMaterial()
		{
			string strListDepartmentID = string.Empty;
			string strListProductionLineID = string.Empty;
			string strListCategoryID = string.Empty;
			string strListProductID = string.Empty;
			
			//Get ID of selected departments
			if(txtDepartment.Tag != null)
			{
				strListDepartmentID = "(0";
				foreach(DataRow drow in ((DataTable)txtDepartment.Tag).Rows)
				{
					strListDepartmentID += ", " + drow[MST_DepartmentTable.DEPARTMENTID_FLD].ToString();
				}
				strListDepartmentID += ")";
				
				//Validate empty value
				if(strListDepartmentID == EMPTY_STRING)
				{
					strListDepartmentID = string.Empty;
				}
			}
			
			//Get ID of selected production lines
			if(txtProductionLine.Tag != null)
			{
				strListProductionLineID = "(0";
				foreach(DataRow drow in ((DataTable)txtProductionLine.Tag).Rows)
				{
					strListProductionLineID += ", " + drow[PRO_ProductionLineTable.PRODUCTIONLINEID_FLD].ToString();
				}
				strListProductionLineID += ")";

				//Validate empty value
				if(strListProductionLineID == EMPTY_STRING)
				{
					strListProductionLineID = string.Empty;
				}
			}
			
			//Get ID of selected categories
			if(txtCategory.Tag != null)
			{
				strListCategoryID = "(0";
				foreach(DataRow drow in ((DataTable)txtCategory.Tag).Rows)
				{
					strListCategoryID += ", " + drow[ITM_CategoryTable.CATEGORYID_FLD].ToString();
				}
				strListCategoryID += ")";

				//Validate empty value
				if(strListCategoryID == EMPTY_STRING)
				{
					strListCategoryID = string.Empty;
				}
			}
			
			//Get ID of selected part no.
			if(txtPartNo.Tag != null)
			{
				strListProductID = "(0";
				foreach(DataRow drow in ((DataTable)txtPartNo.Tag).Rows)
				{
					strListProductID += ", " + drow[ITM_ProductTable.PRODUCTID_FLD].ToString();
				}
				strListProductID += ")";

				//Validate empty value
				if(strListProductID == EMPTY_STRING)
				{
					strListProductID = string.Empty;
				}
			}

			C1PrintPreviewDialogBO boPrintPreview = new C1PrintPreviewDialogBO();

			return boPrintPreview.GetSalvagingMaterialData(cboCCN.SelectedValue.ToString(),
				cboYear.Text, cboMonth.Text, strListDepartmentID, strListProductionLineID, 
				strListCategoryID, strListProductID);			
		}


		/// <summary>
		/// Item Cost Detailed By Element Report - BOD
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ShowItemCostDetailedByElementReport(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".ShowItemCostDetailedByElementReport()";	

			const string REPORT_TEMPLATE_FILE = "ItemCostDetailedByCostElementReport.xml";		
			const string REPORT_NAME		  = "ItemCostDetailedByCostElementReport";

			//Report field names
			const string RPT_CCN = "CCN";
			const string RPT_YEAR	   = "Year";	
			const string RPT_MONTH  = "Month";
			const string RPT_CATEGORY    = "Category";
			const string RPT_DEPARTMENT      = "Department";
			const string RPT_PRODUCTIONLINE   = "Production Line";
			const string RPT_PART_NO = "Part No.";
			const string RPT_PART_NAME = "Part Name";
			const string RPT_MODEL = "Model";
			const string RPT_COMPANY  = "fldCompany";
			const string RPT_HOME_CURRENCY  = "fldHomeCurrency";

			const string RPT_PAGE_HEADER = "PageHeader";			
			const string RPT_TITLE_FIELD = "fldTitle";

			try
			{				
				string strReportPath = Application.StartupPath;

				//Validate data; user must select CCN first
				if(cboCCN.SelectedValue == null || cboCCN.Text.Trim() == string.Empty)
				{					
					string[] arrParams = {lblCCN.Text};
					PCSMessageBox.Show(ErrorCode.MESSAGE_MANDATORY_FIELD_REQUIRED, MessageBoxIcon.Exclamation, arrParams);					
					cboCCN.Focus();
					return;
				}

				//Check if user does not select Year
				if(cboYear.Text.Trim() == string.Empty)
				{				
					string[] arrParams = {lblYear.Text};
					PCSMessageBox.Show(ErrorCode.MESSAGE_MANDATORY_FIELD_REQUIRED, MessageBoxIcon.Exclamation, arrParams);
					cboYear.Focus();					
					return;
				}

				if(cboMonth.Text.Trim() == string.Empty)
				{
					string[] arrParams = {lblMonth.Text};
					PCSMessageBox.Show(ErrorCode.MESSAGE_MANDATORY_FIELD_REQUIRED, MessageBoxIcon.Exclamation, arrParams);
					cboMonth.Focus();
					return;
				}

				//Change cursor to wait
				this.Cursor = Cursors.WaitCursor;
				
				int intIndex = strReportPath.IndexOf(APPLICATION_PATH);
				
				//Validate report layout path
				if(intIndex > -1)
				{
					strReportPath = strReportPath.Substring(0, intIndex);
				}

				if(strReportPath.Substring(strReportPath.Length -1) == @"\")
				{
					strReportPath += Constants.REPORT_DEFINITION_STORE_LOCATION;
				}
				else
				{
					strReportPath += @"\" + Constants.REPORT_DEFINITION_STORE_LOCATION;
				}				
				
				// check report layout file is exist or not
				if(!File.Exists(strReportPath + @"\" + REPORT_TEMPLATE_FILE))
				{
					this.Cursor = Cursors.Default;
					PCSMessageBox.Show(ErrorCode.MESSAGE_REPORT_TEMPLATE_FILE_NOT_FOUND, MessageBoxIcon.Error);					
					return;
				}
				
				//Get Data based on supplied condition
				DataTable dtbData = GetItemCostDetailedByElement();

				//Create builder object
				ReportWithSubReportBuilder reportBuilder = new ReportWithSubReportBuilder();
				//Set report name
				reportBuilder.ReportName = REPORT_NAME;
				//Set Datasource
				reportBuilder.SourceDataTable = dtbData;
				//Set report layout location
				reportBuilder.ReportDefinitionFolder = strReportPath;
				reportBuilder.ReportLayoutFile = REPORT_TEMPLATE_FILE;				
				
				reportBuilder.UseLayoutFile = true;
				reportBuilder.MakeDataTableForRender();			

				// and show it in preview dialog				
				PCSUtils.Framework.ReportFrame.C1PrintPreviewDialog	printPreview = new PCSUtils.Framework.ReportFrame.C1PrintPreviewDialog();
								
				//Attach report viewer
				reportBuilder.ReportViewer = printPreview.ReportViewer;				
				reportBuilder.RenderReport();				
				
				//Draw parameters				
				System.Collections.Specialized.NameValueCollection arrParamAndValue = new System.Collections.Specialized.NameValueCollection();
				arrParamAndValue.Add(RPT_CCN, cboCCN.Text);
				arrParamAndValue.Add(RPT_YEAR, cboYear.Text);	
				arrParamAndValue.Add(RPT_MONTH, cboMonth.Text);

				if(txtDepartment.Text != string.Empty)
				{
					arrParamAndValue.Add(RPT_DEPARTMENT, (txtDepartment.Text.Length > MAX_LENGTH)?txtDepartment.Text.Substring(0, MAX_LENGTH) + "...":txtDepartment.Text);
				}

				if(txtProductionLine.Text != string.Empty)
				{
					arrParamAndValue.Add(RPT_PRODUCTIONLINE, (txtProductionLine.Text.Length > MAX_LENGTH)?txtProductionLine.Text.Substring(0, MAX_LENGTH) + "...":txtProductionLine.Text);
				}

				if(txtCategory.Text != string.Empty)
				{
					arrParamAndValue.Add(RPT_CATEGORY, (txtCategory.Text.Length > MAX_LENGTH)?txtCategory.Text.Substring(0, MAX_LENGTH) + "...":txtCategory.Text);
				}

				if(txtPartNo.Text != string.Empty)
				{
					arrParamAndValue.Add(RPT_PART_NO, (txtPartNo.Text.Length > MAX_LENGTH)?txtPartNo.Text.Substring(0, MAX_LENGTH) + "...":txtPartNo.Text);
				}

				if(txtPartName.Text != string.Empty)
				{
					arrParamAndValue.Add(RPT_PART_NAME, (txtPartName.Text.Length > MAX_LENGTH)?txtPartName.Text.Substring(0, MAX_LENGTH) + "...":txtPartName.Text);
				}

				if(txtModel.Text != string.Empty)
				{
					arrParamAndValue.Add(RPT_MODEL, (txtModel.Text.Length > MAX_LENGTH)?txtModel.Text.Substring(0, MAX_LENGTH) + "...":txtModel.Text);
				}
				
				//Anchor the Parameter drawing canvas cordinate to the fldTitle
				C1.C1Report.Field fldTitle = reportBuilder.GetFieldByName(RPT_TITLE_FIELD);
				double dblStartX = fldTitle.Left;
				double dblStartY = fldTitle.Top  + 1.3 * fldTitle.RenderHeight;
				reportBuilder.GetSectionByName(RPT_PAGE_HEADER).CanGrow = true;
				reportBuilder.DrawParameters(reportBuilder.GetSectionByName(RPT_PAGE_HEADER), dblStartX, dblStartY, arrParamAndValue, reportBuilder.Report.Font.Size);
				
				//Change form title
				try
				{
					printPreview.FormTitle = reportBuilder.GetFieldByName(RPT_TITLE_FIELD).Text + Constants.WHITE_SPACE + cboYear.Text;
				}
				catch{}
				
				//Company & Home Currency Info
				reportBuilder.DrawPredefinedField(RPT_COMPANY, SystemProperty.SytemParams.Get(SystemParam.COMPANY_FULL_NAME));
				reportBuilder.DrawPredefinedField(RPT_HOME_CURRENCY, SystemProperty.HomeCurrency);

				reportBuilder.RefreshReport();				
				
				printPreview.Show();

				// display report data to a form
				ReportData frmReportData = new ReportData();
				frmReportData.Data = dtbData;
				frmReportData.Show();
			}
			catch(Exception ex)
			{				
				//
				PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxIcon.Error);
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
			finally
			{
				this.Cursor = Cursors.Default;
			}
		}
		

		/// <summary>
		/// Item Cost Detailed By Element Report
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ShowItemCostDetailedByElementAndProductionLineReport(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".ShowItemCostDetailedByElementAndProductionLineReport()";	

			const string REPORT_TEMPLATE_FILE = "ItemCostDetailedByCostElementReport_ProLine.xml";		
			const string REPORT_NAME		  = "ItemCostDetailedByCostElementReport";

			//Report field names
			const string RPT_CCN = "CCN";
			const string RPT_YEAR	   = "Year";	
			const string RPT_MONTH  = "Month";
			const string RPT_CATEGORY    = "Category";
			const string RPT_DEPARTMENT      = "Department";
			const string RPT_PRODUCTIONLINE   = "Production Line";
			const string RPT_PART_NO = "Part No.";
			const string RPT_PART_NAME = "Part Name";
			const string RPT_MODEL = "Model";
			const string RPT_COMPANY  = "fldCompany";
			const string RPT_HOME_CURRENCY  = "fldHomeCurrency";

			const string RPT_PAGE_HEADER = "PageHeader";			
			const string RPT_TITLE_FIELD = "fldTitle";

			try
			{				
				string strReportPath = Application.StartupPath;

				//Validate data; user must select CCN first
				if(cboCCN.SelectedValue == null || cboCCN.Text.Trim() == string.Empty)
				{					
					string[] arrParams = {lblCCN.Text};
					PCSMessageBox.Show(ErrorCode.MESSAGE_MANDATORY_FIELD_REQUIRED, MessageBoxIcon.Exclamation, arrParams);					
					cboCCN.Focus();
					return;
				}

				//Check if user does not select Year
				if(cboYear.Text.Trim() == string.Empty)
				{				
					string[] arrParams = {lblYear.Text};
					PCSMessageBox.Show(ErrorCode.MESSAGE_MANDATORY_FIELD_REQUIRED, MessageBoxIcon.Exclamation, arrParams);
					cboYear.Focus();					
					return;
				}

				if(cboMonth.Text.Trim() == string.Empty)
				{
					string[] arrParams = {lblMonth.Text};
					PCSMessageBox.Show(ErrorCode.MESSAGE_MANDATORY_FIELD_REQUIRED, MessageBoxIcon.Exclamation, arrParams);
					cboMonth.Focus();
					return;
				}

				//Change cursor to wait
				this.Cursor = Cursors.WaitCursor;
				
				int intIndex = strReportPath.IndexOf(APPLICATION_PATH);
				
				//Validate report layout path
				if(intIndex > -1)
				{
					strReportPath = strReportPath.Substring(0, intIndex);
				}

				if(strReportPath.Substring(strReportPath.Length -1) == @"\")
				{
					strReportPath += Constants.REPORT_DEFINITION_STORE_LOCATION;
				}
				else
				{
					strReportPath += @"\" + Constants.REPORT_DEFINITION_STORE_LOCATION;
				}				
				
				// check report layout file is exist or not
				if(!File.Exists(strReportPath + @"\" + REPORT_TEMPLATE_FILE))
				{
					this.Cursor = Cursors.Default;
					PCSMessageBox.Show(ErrorCode.MESSAGE_REPORT_TEMPLATE_FILE_NOT_FOUND, MessageBoxIcon.Error);					
					return;
				}
				
				//Get Data based on supplied condition
				DataTable dtbData = GetItemCostDetailedByElementAndProductionLine();

				//Create builder object
				ReportWithSubReportBuilder reportBuilder = new ReportWithSubReportBuilder();
				//Set report name
				reportBuilder.ReportName = REPORT_NAME;
				//Set Datasource
				reportBuilder.SourceDataTable = dtbData;
				//Set report layout location
				reportBuilder.ReportDefinitionFolder = strReportPath;
				reportBuilder.ReportLayoutFile = REPORT_TEMPLATE_FILE;				
				
				reportBuilder.UseLayoutFile = true;
				reportBuilder.MakeDataTableForRender();			

				// and show it in preview dialog				
				PCSUtils.Framework.ReportFrame.C1PrintPreviewDialog	printPreview = new PCSUtils.Framework.ReportFrame.C1PrintPreviewDialog();
								
				//Attach report viewer
				reportBuilder.ReportViewer = printPreview.ReportViewer;				
				reportBuilder.RenderReport();				
				
				//Draw parameters				
				System.Collections.Specialized.NameValueCollection arrParamAndValue = new System.Collections.Specialized.NameValueCollection();
				arrParamAndValue.Add(RPT_CCN, cboCCN.Text);
				arrParamAndValue.Add(RPT_YEAR, cboYear.Text);	
				arrParamAndValue.Add(RPT_MONTH, cboMonth.Text);

				if(txtDepartment.Text != string.Empty)
				{
					arrParamAndValue.Add(RPT_DEPARTMENT, (txtDepartment.Text.Length > MAX_LENGTH)?txtDepartment.Text.Substring(0, MAX_LENGTH) + "...":txtDepartment.Text);
				}

				if(txtProductionLine.Text != string.Empty)
				{
					arrParamAndValue.Add(RPT_PRODUCTIONLINE, (txtProductionLine.Text.Length > MAX_LENGTH)?txtProductionLine.Text.Substring(0, MAX_LENGTH) + "...":txtProductionLine.Text);
				}

				if(txtCategory.Text != string.Empty)
				{
					arrParamAndValue.Add(RPT_CATEGORY, (txtCategory.Text.Length > MAX_LENGTH)?txtCategory.Text.Substring(0, MAX_LENGTH) + "...":txtCategory.Text);
				}

				if(txtPartNo.Text != string.Empty)
				{
					arrParamAndValue.Add(RPT_PART_NO, (txtPartNo.Text.Length > MAX_LENGTH)?txtPartNo.Text.Substring(0, MAX_LENGTH) + "...":txtPartNo.Text);
				}

				if(txtPartName.Text != string.Empty)
				{
					arrParamAndValue.Add(RPT_PART_NAME, (txtPartName.Text.Length > MAX_LENGTH)?txtPartName.Text.Substring(0, MAX_LENGTH) + "...":txtPartName.Text);
				}

				if(txtModel.Text != string.Empty)
				{
					arrParamAndValue.Add(RPT_MODEL, (txtModel.Text.Length > MAX_LENGTH)?txtModel.Text.Substring(0, MAX_LENGTH) + "...":txtModel.Text);
				}
				
				//Anchor the Parameter drawing canvas cordinate to the fldTitle
				C1.C1Report.Field fldTitle = reportBuilder.GetFieldByName(RPT_TITLE_FIELD);
				double dblStartX = fldTitle.Left;
				double dblStartY = fldTitle.Top  + 1.3 * fldTitle.RenderHeight;
				reportBuilder.GetSectionByName(RPT_PAGE_HEADER).CanGrow = true;
				reportBuilder.DrawParameters(reportBuilder.GetSectionByName(RPT_PAGE_HEADER), dblStartX, dblStartY, arrParamAndValue, reportBuilder.Report.Font.Size);
				
				//Change form title
				try
				{
					printPreview.FormTitle = reportBuilder.GetFieldByName(RPT_TITLE_FIELD).Text + Constants.WHITE_SPACE + cboYear.Text;
				}
				catch{}
				
				//Company & Home Currency Info
				reportBuilder.DrawPredefinedField(RPT_COMPANY, SystemProperty.SytemParams.Get(SystemParam.COMPANY_FULL_NAME));
				reportBuilder.DrawPredefinedField(RPT_HOME_CURRENCY, SystemProperty.HomeCurrency);

				reportBuilder.RefreshReport();				
				
				printPreview.Show();

				// display report data to a form
				ReportData frmReportData = new ReportData();
				frmReportData.Data = dtbData;
				frmReportData.Show();
			}
			catch(Exception ex)
			{				
				//
				PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxIcon.Error);
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
			finally
			{
				this.Cursor = Cursors.Default;
			}
		}
		
		
		/// <summary>
		/// Build condition & get Item Cost Detailed data
		/// </summary>
		/// <returns></returns>
		private DataTable GetItemCostDetailedByElement()
		{
			string strListDepartmentID = string.Empty;
			string strListProductionLineID = string.Empty;
			string strListCategoryID = string.Empty;
			string strListProductID = string.Empty;
			
			//Get ID of selected departments
			if(txtDepartment.Tag != null)
			{
				strListDepartmentID = "(0";
				foreach(DataRow drow in ((DataTable)txtDepartment.Tag).Rows)
				{
					strListDepartmentID += ", " + drow[MST_DepartmentTable.DEPARTMENTID_FLD].ToString();
				}
				strListDepartmentID += ")";
				
				//Validate empty value
				if(strListDepartmentID == EMPTY_STRING)
				{
					strListDepartmentID = string.Empty;
				}
			}
			
			//Get ID of selected production lines
			if(txtProductionLine.Tag != null)
			{
				strListProductionLineID = "(0";
				foreach(DataRow drow in ((DataTable)txtProductionLine.Tag).Rows)
				{
					strListProductionLineID += ", " + drow[PRO_ProductionLineTable.PRODUCTIONLINEID_FLD].ToString();
				}
				strListProductionLineID += ")";

				//Validate empty value
				if(strListProductionLineID == EMPTY_STRING)
				{
					strListProductionLineID = string.Empty;
				}
			}
			
			//Get ID of selected categories
			if(txtCategory.Tag != null)
			{
				strListCategoryID = "(0";
				foreach(DataRow drow in ((DataTable)txtCategory.Tag).Rows)
				{
					strListCategoryID += ", " + drow[ITM_CategoryTable.CATEGORYID_FLD].ToString();
				}
				strListCategoryID += ")";

				//Validate empty value
				if(strListCategoryID == EMPTY_STRING)
				{
					strListCategoryID = string.Empty;
				}
			}
			
			//Get ID of selected part no.
			if(txtPartNo.Tag != null)
			{
				strListProductID = "(0";
				foreach(DataRow drow in ((DataTable)txtPartNo.Tag).Rows)
				{
					strListProductID += ", " + drow[ITM_ProductTable.PRODUCTID_FLD].ToString();
				}
				strListProductID += ")";

				//Validate empty value
				if(strListProductID == EMPTY_STRING)
				{
					strListProductID = string.Empty;
				}
			}

			int intMakeItem = -1;
			if (chkMakeItem.CheckState != CheckState.Indeterminate)
				intMakeItem = Convert.ToInt32(chkMakeItem.Checked);
			C1PrintPreviewDialogBO boPrintPreview = new C1PrintPreviewDialogBO();

			return boPrintPreview.GetItemCostDetailedByElementData(cboCCN.SelectedValue.ToString(),
				cboYear.Text, cboMonth.Text, strListDepartmentID, strListProductionLineID, 
				strListCategoryID, strListProductID, intMakeItem);			
		}

		
		/// <summary>
		/// Build condition & get Item Cost Detailed data
		/// </summary>
		/// <returns></returns>
		private DataTable GetItemCostDetailedByElementAndProductionLine()
		{
			string strListDepartmentID = string.Empty;
			string strListProductionLineID = string.Empty;
			string strListCategoryID = string.Empty;
			string strListProductID = string.Empty;
			
			//Get ID of selected departments
			if(txtDepartment.Tag != null)
			{
				strListDepartmentID = "(0";
				foreach(DataRow drow in ((DataTable)txtDepartment.Tag).Rows)
				{
					strListDepartmentID += ", " + drow[MST_DepartmentTable.DEPARTMENTID_FLD].ToString();
				}
				strListDepartmentID += ")";
				
				//Validate empty value
				if(strListDepartmentID == EMPTY_STRING)
				{
					strListDepartmentID = string.Empty;
				}
			}
			
			//Get ID of selected production lines
			if(txtProductionLine.Tag != null)
			{
				strListProductionLineID = "(0";
				foreach(DataRow drow in ((DataTable)txtProductionLine.Tag).Rows)
				{
					strListProductionLineID += ", " + drow[PRO_ProductionLineTable.PRODUCTIONLINEID_FLD].ToString();
				}
				strListProductionLineID += ")";

				//Validate empty value
				if(strListProductionLineID == EMPTY_STRING)
				{
					strListProductionLineID = string.Empty;
				}
			}
			
			//Get ID of selected categories
			if(txtCategory.Tag != null)
			{
				strListCategoryID = "(0";
				foreach(DataRow drow in ((DataTable)txtCategory.Tag).Rows)
				{
					strListCategoryID += ", " + drow[ITM_CategoryTable.CATEGORYID_FLD].ToString();
				}
				strListCategoryID += ")";

				//Validate empty value
				if(strListCategoryID == EMPTY_STRING)
				{
					strListCategoryID = string.Empty;
				}
			}
			
			//Get ID of selected part no.
			if(txtPartNo.Tag != null)
			{
				strListProductID = "(0";
				foreach(DataRow drow in ((DataTable)txtPartNo.Tag).Rows)
				{
					strListProductID += ", " + drow[ITM_ProductTable.PRODUCTID_FLD].ToString();
				}
				strListProductID += ")";

				//Validate empty value
				if(strListProductID == EMPTY_STRING)
				{
					strListProductID = string.Empty;
				}
			}
			int intMakeItem = -1;
			if (chkMakeItem.CheckState != CheckState.Indeterminate)
				intMakeItem = Convert.ToInt32(chkMakeItem.Checked);

			C1PrintPreviewDialogBO boPrintPreview = new C1PrintPreviewDialogBO();

			return boPrintPreview.GetItemCostDetailedByElementAndProductionLineData(cboCCN.SelectedValue.ToString(),
				cboYear.Text, cboMonth.Text, strListDepartmentID, strListProductionLineID, 
				strListCategoryID, strListProductID, intMakeItem);

		}

		/// <summary>
		/// Create and show Item Cost By Month Report
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ShowItemCostByMonthReport(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".ShowItemCostByMonthReport()";	

			const string REPORT_TEMPLATE_FILE = "ItemCostByMonthReport.xml";		
			const string REPORT_NAME		  = "ItemCostByMonthReport";

			//Report field names
			const string RPT_CCN = "CCN";
			const string RPT_YEAR	   = "Year";			
			const string RPT_CATEGORY    = "Category";
			const string RPT_DEPARTMENT      = "Department";			
			const string RPT_PRODUCTIONLINE   = "Production Line";
			const string RPT_PART_NO = "Part No.";
			const string RPT_PART_NAME = "Part Name";
			const string RPT_MODEL = "Model";
			const string RPT_COMPANY  = "fldCompany";
			const string RPT_HOME_CURRENCY  = "fldHomeCurrency";

			const string RPT_PAGE_HEADER = "PageHeader";			
			const string RPT_TITLE_FIELD = "fldTitle";

			try
			{				
				string strReportPath = Application.StartupPath;

				//Validate data; user must select CCN first
				if(cboCCN.SelectedValue == null || cboCCN.Text.Trim() == string.Empty)
				{
					this.Cursor = Cursors.Default;
					string[] arrParams = {lblCCN.Text};
					PCSMessageBox.Show(ErrorCode.MESSAGE_MANDATORY_FIELD_REQUIRED, MessageBoxIcon.Exclamation, arrParams);					
					cboCCN.Focus();
					return;
				}

				//Check if user does not select Year
				if(cboYear.Text.Trim() == string.Empty)
				{
					this.Cursor = Cursors.Default;
					string[] arrParams = {lblYear.Text};
					PCSMessageBox.Show(ErrorCode.MESSAGE_MANDATORY_FIELD_REQUIRED, MessageBoxIcon.Exclamation, arrParams);
					cboYear.Focus();					
					return;
				}				
				
				//Change cursor to wait
				this.Cursor = Cursors.WaitCursor;
				
				int intIndex = strReportPath.IndexOf(APPLICATION_PATH);
				
				//Validate report layout path
				if(intIndex > -1)
				{
					strReportPath = strReportPath.Substring(0, intIndex);
				}

				if(strReportPath.Substring(strReportPath.Length -1) == @"\")
				{
					strReportPath += Constants.REPORT_DEFINITION_STORE_LOCATION;
				}
				else
				{
					strReportPath += @"\" + Constants.REPORT_DEFINITION_STORE_LOCATION;
				}				
				
				// check report layout file is exist or not
				if(!File.Exists(strReportPath + @"\" + REPORT_TEMPLATE_FILE))
				{
					this.Cursor = Cursors.Default;
					PCSMessageBox.Show(ErrorCode.MESSAGE_REPORT_TEMPLATE_FILE_NOT_FOUND, MessageBoxIcon.Error);					
					return;
				}
				
				//Get Data based on supplied condition
				DataTable dtbData = GetItemCostByMonthData();

				//Create builder object
				ReportWithSubReportBuilder reportBuilder = new ReportWithSubReportBuilder();
				//Set report name
				reportBuilder.ReportName = REPORT_NAME;
				//Set Datasource
				reportBuilder.SourceDataTable = dtbData;
				//Set report layout location
				reportBuilder.ReportDefinitionFolder = strReportPath;
				reportBuilder.ReportLayoutFile = REPORT_TEMPLATE_FILE;				
				
				reportBuilder.UseLayoutFile = true;
				reportBuilder.MakeDataTableForRender();			

				// and show it in preview dialog				
				PCSUtils.Framework.ReportFrame.C1PrintPreviewDialog	printPreview = new PCSUtils.Framework.ReportFrame.C1PrintPreviewDialog();
								
				//Attach report viewer
				reportBuilder.ReportViewer = printPreview.ReportViewer;				
				reportBuilder.RenderReport();				
				
				//Draw parameters				
				System.Collections.Specialized.NameValueCollection arrParamAndValue = new System.Collections.Specialized.NameValueCollection();
				arrParamAndValue.Add(RPT_CCN, cboCCN.Text);
				arrParamAndValue.Add(RPT_YEAR, cboYear.Text);				

				if(txtDepartment.Text != string.Empty)
				{
					arrParamAndValue.Add(RPT_DEPARTMENT, (txtDepartment.Text.Length > MAX_LENGTH)?txtDepartment.Text.Substring(0, MAX_LENGTH) + "...":txtDepartment.Text);
				}

				if(txtProductionLine.Text != string.Empty)
				{
					arrParamAndValue.Add(RPT_PRODUCTIONLINE, (txtProductionLine.Text.Length > MAX_LENGTH)?txtProductionLine.Text.Substring(0, MAX_LENGTH) + "...":txtProductionLine.Text);
				}

				if(txtCategory.Text != string.Empty)
				{
					arrParamAndValue.Add(RPT_CATEGORY, (txtCategory.Text.Length > MAX_LENGTH)?txtCategory.Text.Substring(0, MAX_LENGTH) + "...":txtCategory.Text);
				}

				if(txtPartNo.Text != string.Empty)
				{
					arrParamAndValue.Add(RPT_PART_NO, (txtPartNo.Text.Length > MAX_LENGTH)?txtPartNo.Text.Substring(0, MAX_LENGTH) + "...":txtPartNo.Text);
				}

				if(txtPartName.Text != string.Empty)
				{
					arrParamAndValue.Add(RPT_PART_NAME, (txtPartName.Text.Length > MAX_LENGTH)?txtPartName.Text.Substring(0, MAX_LENGTH) + "...":txtPartName.Text);
				}

				if(txtModel.Text != string.Empty)
				{
					arrParamAndValue.Add(RPT_MODEL, (txtModel.Text.Length > MAX_LENGTH)?txtModel.Text.Substring(0, MAX_LENGTH) + "...":txtModel.Text);
				}
				
				//Anchor the Parameter drawing canvas cordinate to the fldTitle
				C1.C1Report.Field fldTitle = reportBuilder.GetFieldByName(RPT_TITLE_FIELD);
				double dblStartX = fldTitle.Left;
				double dblStartY = fldTitle.Top  + 1.3 * fldTitle.RenderHeight;
				reportBuilder.GetSectionByName(RPT_PAGE_HEADER).CanGrow = true;
				reportBuilder.DrawParameters(reportBuilder.GetSectionByName(RPT_PAGE_HEADER), dblStartX, dblStartY, arrParamAndValue, reportBuilder.Report.Font.Size);
				
				//Change form title
				try
				{
					printPreview.FormTitle = reportBuilder.GetFieldByName(RPT_TITLE_FIELD).Text + Constants.WHITE_SPACE + cboYear.Text;
				}
				catch{}
				
				//Company & Home Currency Info
				reportBuilder.DrawPredefinedField(RPT_COMPANY, SystemProperty.SytemParams.Get(SystemParam.COMPANY_FULL_NAME));
				reportBuilder.DrawPredefinedField(RPT_HOME_CURRENCY, SystemProperty.HomeCurrency);

				reportBuilder.RefreshReport();				
				
				printPreview.Show();

				// display report data to a form
				ReportData frmReportData = new ReportData();
				frmReportData.Data = dtbData;
				frmReportData.Show();
			}
			catch(Exception ex)
			{				
				//
				PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxIcon.Error);
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
			finally
			{
				this.Cursor = Cursors.Default;
			}
		}
		
		
		/// <summary>
		/// Build condition & get Item Cost By Month data
		/// </summary>
		/// <returns></returns>
		private DataTable GetItemCostByMonthData()
		{			
			string strListDepartmentID = string.Empty;
			string strListProductionLineID = string.Empty;
			string strListCategoryID = string.Empty;
			string strListProductID = string.Empty;
			
			//Get ID of selected departments
			if(txtDepartment.Tag != null)
			{
				strListDepartmentID = "(0";
				foreach(DataRow drow in ((DataTable)txtDepartment.Tag).Rows)
				{
					strListDepartmentID += ", " + drow[MST_DepartmentTable.DEPARTMENTID_FLD].ToString();
				}
				strListDepartmentID += ")";
				
				//Validate empty value
				if(strListDepartmentID == EMPTY_STRING)
				{
					strListDepartmentID = string.Empty;
				}
			}
			
			//Get ID of selected production lines
			if(txtProductionLine.Tag != null)
			{
				strListProductionLineID = "(0";
				foreach(DataRow drow in ((DataTable)txtProductionLine.Tag).Rows)
				{
					strListProductionLineID += ", " + drow[PRO_ProductionLineTable.PRODUCTIONLINEID_FLD].ToString();
				}
				strListProductionLineID += ")";

				//Validate empty value
				if(strListProductionLineID == EMPTY_STRING)
				{
					strListProductionLineID = string.Empty;
				}
			}
			
			//Get ID of selected categories
			if(txtCategory.Tag != null)
			{
				strListCategoryID = "(0";
				foreach(DataRow drow in ((DataTable)txtCategory.Tag).Rows)
				{
					strListCategoryID += ", " + drow[ITM_CategoryTable.CATEGORYID_FLD].ToString();
				}
				strListCategoryID += ")";

				//Validate empty value
				if(strListCategoryID == EMPTY_STRING)
				{
					strListCategoryID = string.Empty;
				}
			}
			
			//Get ID of selected part no.
			if(txtPartNo.Tag != null)
			{
				strListProductID = "(0";
				foreach(DataRow drow in ((DataTable)txtPartNo.Tag).Rows)
				{
					strListProductID += ", " + drow[ITM_ProductTable.PRODUCTID_FLD].ToString();
				}
				strListProductID += ")";

				//Validate empty value
				if(strListProductID == EMPTY_STRING)
				{
					strListProductID = string.Empty;
				}
			}

			C1PrintPreviewDialogBO boPrintPreview = new C1PrintPreviewDialogBO();

			return boPrintPreview.GetItemCostByMonthData(cboCCN.SelectedValue.ToString(),
				cboYear.Text, strListDepartmentID, strListProductionLineID, 
				strListCategoryID, strListProductID);
			
		}
		
		
		/// <summary>
		/// Create and show Detailed Item Cost By Month Report
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ShowDetailedItemCostByMonthReport(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".ShowDetailedItemCostByMonthReport()";	

			const string REPORT_TEMPLATE_FILE = "DetailedItemCostByMonthReport.xml";		
			const string REPORT_NAME		  = "DetailedItemCostByMonthReport";

			//Report field names
			const string RPT_CCN = "CCN";
			const string RPT_YEAR	   = "Year";			
			const string RPT_CATEGORY    = "Category";
			const string RPT_DEPARTMENT      = "Department";			
			const string RPT_PRODUCTIONLINE   = "Production Line";
			const string RPT_PART_NO = "Part No.";
			const string RPT_PART_NAME = "Part Name";
			const string RPT_MODEL = "Model";
			const string RPT_COMPANY  = "fldCompany";
			const string RPT_HOME_CURRENCY  = "fldHomeCurrency";

			const string RPT_PAGE_HEADER = "PageHeader";			
			const string RPT_TITLE_FIELD = "fldTitle";

			try
			{				
				string strReportPath = Application.StartupPath;

				//Validate data; user must select CCN first
				if(cboCCN.SelectedValue == null || cboCCN.Text.Trim() == string.Empty)
				{
					this.Cursor = Cursors.Default;
					string[] arrParams = {lblCCN.Text};
					PCSMessageBox.Show(ErrorCode.MESSAGE_MANDATORY_FIELD_REQUIRED, MessageBoxIcon.Exclamation, arrParams);					
					cboCCN.Focus();
					return;
				}

				//Check if user does not select Year
				if(cboYear.Text.Trim() == string.Empty)
				{
					this.Cursor = Cursors.Default;
					string[] arrParams = {lblYear.Text};
					PCSMessageBox.Show(ErrorCode.MESSAGE_MANDATORY_FIELD_REQUIRED, MessageBoxIcon.Exclamation, arrParams);
					cboYear.Focus();					
					return;
				}				
				
				//Change cursor to wait
				this.Cursor = Cursors.WaitCursor;
				
				int intIndex = strReportPath.IndexOf(APPLICATION_PATH);
				
				//Validate report layout path
				if(intIndex > -1)
				{
					strReportPath = strReportPath.Substring(0, intIndex);
				}

				if(strReportPath.Substring(strReportPath.Length -1) == @"\")
				{
					strReportPath += Constants.REPORT_DEFINITION_STORE_LOCATION;
				}
				else
				{
					strReportPath += @"\" + Constants.REPORT_DEFINITION_STORE_LOCATION;
				}				
				
				// check report layout file is exist or not
				if(!File.Exists(strReportPath + @"\" + REPORT_TEMPLATE_FILE))
				{
					this.Cursor = Cursors.Default;
					PCSMessageBox.Show(ErrorCode.MESSAGE_REPORT_TEMPLATE_FILE_NOT_FOUND, MessageBoxIcon.Error);					
					return;
				}
				
				//Get Data based on supplied condition
				DataTable dtbData = GetDetailedItemCostByMonthData();

				//Create builder object
				ReportWithSubReportBuilder reportBuilder = new ReportWithSubReportBuilder();
				//Set report name
				reportBuilder.ReportName = REPORT_NAME;
				//Set Datasource
				reportBuilder.SourceDataTable = dtbData;
				//Set report layout location
				reportBuilder.ReportDefinitionFolder = strReportPath;
				reportBuilder.ReportLayoutFile = REPORT_TEMPLATE_FILE;				
				
				reportBuilder.UseLayoutFile = true;
				reportBuilder.MakeDataTableForRender();			

				// and show it in preview dialog				
				PCSUtils.Framework.ReportFrame.C1PrintPreviewDialog	printPreview = new PCSUtils.Framework.ReportFrame.C1PrintPreviewDialog();
								
				//Attach report viewer
				reportBuilder.ReportViewer = printPreview.ReportViewer;				
				reportBuilder.RenderReport();				
				
				//Draw parameters				
				System.Collections.Specialized.NameValueCollection arrParamAndValue = new System.Collections.Specialized.NameValueCollection();
				arrParamAndValue.Add(RPT_CCN, cboCCN.Text);
				arrParamAndValue.Add(RPT_YEAR, cboYear.Text);				

				if(txtDepartment.Text != string.Empty)
				{
					arrParamAndValue.Add(RPT_DEPARTMENT, (txtDepartment.Text.Length > MAX_LENGTH)?txtDepartment.Text.Substring(0, MAX_LENGTH) + "...":txtDepartment.Text);
				}

				if(txtProductionLine.Text != string.Empty)
				{
					arrParamAndValue.Add(RPT_PRODUCTIONLINE, (txtProductionLine.Text.Length > MAX_LENGTH)?txtProductionLine.Text.Substring(0, MAX_LENGTH) + "...":txtProductionLine.Text);
				}

				if(txtCategory.Text != string.Empty)
				{
					arrParamAndValue.Add(RPT_CATEGORY, (txtCategory.Text.Length > MAX_LENGTH)?txtCategory.Text.Substring(0, MAX_LENGTH) + "...":txtCategory.Text);
				}

				if(txtPartNo.Text != string.Empty)
				{
					arrParamAndValue.Add(RPT_PART_NO, (txtPartNo.Text.Length > MAX_LENGTH)?txtPartNo.Text.Substring(0, MAX_LENGTH) + "...":txtPartNo.Text);
				}

				if(txtPartName.Text != string.Empty)
				{
					arrParamAndValue.Add(RPT_PART_NAME, (txtPartName.Text.Length > MAX_LENGTH)?txtPartName.Text.Substring(0, MAX_LENGTH) + "...":txtPartName.Text);
				}

				if(txtModel.Text != string.Empty)
				{
					arrParamAndValue.Add(RPT_MODEL, (txtModel.Text.Length > MAX_LENGTH)?txtModel.Text.Substring(0, MAX_LENGTH) + "...":txtModel.Text);
				}
				
				//Anchor the Parameter drawing canvas cordinate to the fldTitle
				C1.C1Report.Field fldTitle = reportBuilder.GetFieldByName(RPT_TITLE_FIELD);
				double dblStartX = fldTitle.Left;
				double dblStartY = fldTitle.Top  + 1.3 * fldTitle.RenderHeight;
				reportBuilder.GetSectionByName(RPT_PAGE_HEADER).CanGrow = true;
				reportBuilder.DrawParameters(reportBuilder.GetSectionByName(RPT_PAGE_HEADER), dblStartX, dblStartY, arrParamAndValue, reportBuilder.Report.Font.Size);
				
				//Change form title
				try
				{
					printPreview.FormTitle = reportBuilder.GetFieldByName(RPT_TITLE_FIELD).Text + Constants.WHITE_SPACE + cboYear.Text;
				}
				catch{}
				
				//Company & Home Currency Info
				reportBuilder.DrawPredefinedField(RPT_COMPANY, SystemProperty.SytemParams.Get(SystemParam.COMPANY_FULL_NAME));
				reportBuilder.DrawPredefinedField(RPT_HOME_CURRENCY, SystemProperty.HomeCurrency);

				reportBuilder.RefreshReport();				
				
				printPreview.Show();

				// display report data to a form
				ReportData frmReportData = new ReportData();
				frmReportData.Data = dtbData;
				frmReportData.Show();
			}
			catch(Exception ex)
			{				
				//
				PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxIcon.Error);
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
			finally
			{
				this.Cursor = Cursors.Default;
			}
		}
		
		
		/// <summary>
		/// Build condition & get Detaile Item Cost By Month data
		/// </summary>
		/// <returns></returns>
		private DataTable GetDetailedItemCostByMonthData()
		{			
			string strListDepartmentID = string.Empty;
			string strListProductionLineID = string.Empty;
			string strListCategoryID = string.Empty;
			string strListProductID = string.Empty;
			
			//Get ID of selected departments
			if(txtDepartment.Tag != null)
			{
				strListDepartmentID = "(0";
				foreach(DataRow drow in ((DataTable)txtDepartment.Tag).Rows)
				{
					strListDepartmentID += ", " + drow[MST_DepartmentTable.DEPARTMENTID_FLD].ToString();
				}
				strListDepartmentID += ")";
				
				//Validate empty value
				if(strListDepartmentID == EMPTY_STRING)
				{
					strListDepartmentID = string.Empty;
				}
			}
			
			//Get ID of selected production lines
			if(txtProductionLine.Tag != null)
			{
				strListProductionLineID = "(0";
				foreach(DataRow drow in ((DataTable)txtProductionLine.Tag).Rows)
				{
					strListProductionLineID += ", " + drow[PRO_ProductionLineTable.PRODUCTIONLINEID_FLD].ToString();
				}
				strListProductionLineID += ")";

				//Validate empty value
				if(strListProductionLineID == EMPTY_STRING)
				{
					strListProductionLineID = string.Empty;
				}
			}
			
			//Get ID of selected categories
			if(txtCategory.Tag != null)
			{
				strListCategoryID = "(0";
				foreach(DataRow drow in ((DataTable)txtCategory.Tag).Rows)
				{
					strListCategoryID += ", " + drow[ITM_CategoryTable.CATEGORYID_FLD].ToString();
				}
				strListCategoryID += ")";

				//Validate empty value
				if(strListCategoryID == EMPTY_STRING)
				{
					strListCategoryID = string.Empty;
				}
			}
			
			//Get ID of selected part no.
			if(txtPartNo.Tag != null)
			{
				strListProductID = "(0";
				foreach(DataRow drow in ((DataTable)txtPartNo.Tag).Rows)
				{
					strListProductID += ", " + drow[ITM_ProductTable.PRODUCTID_FLD].ToString();
				}
				strListProductID += ")";

				//Validate empty value
				if(strListProductID == EMPTY_STRING)
				{
					strListProductID = string.Empty;
				}
			}

			C1PrintPreviewDialogBO boPrintPreview = new C1PrintPreviewDialogBO();

			return boPrintPreview.GetDetailedItemCostByMonthData(cboCCN.SelectedValue.ToString(),
				cboYear.Text, strListDepartmentID, strListProductionLineID, 
				strListCategoryID, strListProductID);			
		}
				
		/// <summary>
		/// Create and show Destroy Material Report
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ShowDestroyMeterialReport(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".ShowDestroyMeterialReport()";	

			const string DESTROY_MATERIAL_REPORT = "DestroyMaterialReport.xml";		
			const string REPORT_NAME		  = "DestroyMaterialReport";

			//Report field names
			const string RPT_CCN	   = "CCN";
			const string RPT_YEAR	   = "Year";
			const string RPT_MONTH  = "Month";
			const string RPT_CATEGORY    = "Category";
			const string RPT_DEPARTMENT      = "Department";			
			const string RPT_PRODUCTIONLINE   = "Production Line";
			const string RPT_PART_NO = "Part No.";
			const string RPT_PART_NAME = "Part Name";
			const string RPT_MODEL = "Model";
			const string RPT_COMPANY  = "fldCompany";
			const string RPT_HOME_CURRENCY  = "fldHomeCurrency";

			const string RPT_PAGE_HEADER = "PageHeader";			
			const string RPT_TITLE_FIELD = "fldTitle";

			try
			{				
				string strReportPath = Application.StartupPath;

				//Validate data; user must select CCN first
				if(cboCCN.SelectedValue == null || cboCCN.Text.Trim() == string.Empty)
				{					
					string[] arrParams = {lblCCN.Text};
					PCSMessageBox.Show(ErrorCode.MESSAGE_MANDATORY_FIELD_REQUIRED, MessageBoxIcon.Exclamation, arrParams);					
					cboCCN.Focus();
					return;
				}

				//Check if user does not select Year
				if(cboYear.Text.Trim() == string.Empty)
				{					
					string[] arrParams = {lblYear.Text};
					PCSMessageBox.Show(ErrorCode.MESSAGE_MANDATORY_FIELD_REQUIRED, MessageBoxIcon.Exclamation, arrParams);
					cboYear.Focus();					
					return;
				}
				
				if(cboMonth.Text.Trim() == string.Empty)
				{
					string[] arrParams = {lblMonth.Text};
					PCSMessageBox.Show(ErrorCode.MESSAGE_MANDATORY_FIELD_REQUIRED, MessageBoxIcon.Exclamation, arrParams);
					cboMonth.Focus();
					return;
				}
				
				//Change cursor to wait
				this.Cursor = Cursors.WaitCursor;
				
				int intIndex = strReportPath.IndexOf(APPLICATION_PATH);
				
				//Validate report layout path
				if(intIndex > -1)
				{
					strReportPath = strReportPath.Substring(0, intIndex);
				}

				if(strReportPath.Substring(strReportPath.Length -1) == @"\")
				{
					strReportPath += Constants.REPORT_DEFINITION_STORE_LOCATION;
				}
				else
				{
					strReportPath += @"\" + Constants.REPORT_DEFINITION_STORE_LOCATION;
				}				
				
				// check report layout file is exist or not
				if(!File.Exists(strReportPath + @"\" + DESTROY_MATERIAL_REPORT))
				{
					this.Cursor = Cursors.Default;
					PCSMessageBox.Show(ErrorCode.MESSAGE_REPORT_TEMPLATE_FILE_NOT_FOUND, MessageBoxIcon.Error);					
					return;
				}
				
				//Get Data based on supplied condition
				DataTable dtbData = GetDestroyMaterialData();

				//Create builder object
				ReportWithSubReportBuilder reportBuilder = new ReportWithSubReportBuilder();
				//Set report name
				reportBuilder.ReportName = REPORT_NAME;
				//Set Datasource
				reportBuilder.SourceDataTable = dtbData;
				//Set report layout location
				reportBuilder.ReportDefinitionFolder = strReportPath;
				reportBuilder.ReportLayoutFile = DESTROY_MATERIAL_REPORT;				
				
				reportBuilder.UseLayoutFile = true;
				reportBuilder.MakeDataTableForRender();			

				// and show it in preview dialog				
				PCSUtils.Framework.ReportFrame.C1PrintPreviewDialog	printPreview = new PCSUtils.Framework.ReportFrame.C1PrintPreviewDialog();
								
				//Attach report viewer
				reportBuilder.ReportViewer = printPreview.ReportViewer;				
				reportBuilder.RenderReport();				
				
				//Draw parameters				
				System.Collections.Specialized.NameValueCollection arrParamAndValue = new System.Collections.Specialized.NameValueCollection();
				arrParamAndValue.Add(RPT_CCN, cboCCN.Text);
				arrParamAndValue.Add(RPT_YEAR, cboYear.Text);
				arrParamAndValue.Add(RPT_MONTH, cboMonth.Text);

				if(txtDepartment.Text != string.Empty)
				{
					arrParamAndValue.Add(RPT_DEPARTMENT, (txtDepartment.Text.Length > MAX_LENGTH)?txtDepartment.Text.Substring(0, MAX_LENGTH) + "...":txtDepartment.Text);
				}

				if(txtProductionLine.Text != string.Empty)
				{
					arrParamAndValue.Add(RPT_PRODUCTIONLINE, (txtProductionLine.Text.Length > MAX_LENGTH)?txtProductionLine.Text.Substring(0, MAX_LENGTH) + "...":txtProductionLine.Text);
				}

				if(txtCategory.Text != string.Empty)
				{
					arrParamAndValue.Add(RPT_CATEGORY, (txtCategory.Text.Length > MAX_LENGTH)?txtCategory.Text.Substring(0, MAX_LENGTH) + "...":txtCategory.Text);
				}

				if(txtPartNo.Text != string.Empty)
				{
					arrParamAndValue.Add(RPT_PART_NO, (txtPartNo.Text.Length > MAX_LENGTH)?txtPartNo.Text.Substring(0, MAX_LENGTH) + "...":txtPartNo.Text);
				}

				if(txtPartName.Text != string.Empty)
				{
					arrParamAndValue.Add(RPT_PART_NAME, (txtPartName.Text.Length > MAX_LENGTH)?txtPartName.Text.Substring(0, MAX_LENGTH) + "...":txtPartName.Text);
				}

				if(txtModel.Text != string.Empty)
				{
					arrParamAndValue.Add(RPT_MODEL, (txtModel.Text.Length > MAX_LENGTH)?txtModel.Text.Substring(0, MAX_LENGTH) + "...":txtModel.Text);
				}
				
				//Anchor the Parameter drawing canvas cordinate to the fldTitle
				C1.C1Report.Field fldTitle = reportBuilder.GetFieldByName(RPT_TITLE_FIELD);
				double dblStartX = fldTitle.Left;
				double dblStartY = fldTitle.Top  + 1.3 * fldTitle.RenderHeight;
				reportBuilder.GetSectionByName(RPT_PAGE_HEADER).CanGrow = true;
				reportBuilder.DrawParameters(reportBuilder.GetSectionByName(RPT_PAGE_HEADER), dblStartX, dblStartY, arrParamAndValue, reportBuilder.Report.Font.Size);
				
				//Change form title
				try
				{
					printPreview.FormTitle = reportBuilder.GetFieldByName(RPT_TITLE_FIELD).Text + Constants.WHITE_SPACE + cboYear.Text;
				}
				catch{}
				
				//Company & Home Currency Info
				reportBuilder.DrawPredefinedField(RPT_COMPANY, SystemProperty.SytemParams.Get(SystemParam.COMPANY_FULL_NAME));
				reportBuilder.DrawPredefinedField(RPT_HOME_CURRENCY, SystemProperty.HomeCurrency);

				reportBuilder.RefreshReport();				
				
				printPreview.Show();

				// display report data to a form
				ReportData frmReportData = new ReportData();
				frmReportData.Data = dtbData;
				frmReportData.Show();
			}
			catch(Exception ex)
			{				
				//
				PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxIcon.Error);
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
			finally
			{
				this.Cursor = Cursors.Default;
			}
		}

		
		/// <summary>
		/// Build condition & get Destroy Material data
		/// </summary>
		/// <returns></returns>
		private DataTable GetDestroyMaterialData()
		{
			string strListDepartmentID = string.Empty;
			string strListProductionLineID = string.Empty;
			string strListCategoryID = string.Empty;
			string strListProductID = string.Empty;
			
			//Get ID of selected departments
			if(txtDepartment.Tag != null)
			{
				strListDepartmentID = "(0";
				foreach(DataRow drow in ((DataTable)txtDepartment.Tag).Rows)
				{
					strListDepartmentID += ", " + drow[MST_DepartmentTable.DEPARTMENTID_FLD].ToString();
				}
				strListDepartmentID += ")";
				
				//Validate empty value
				if(strListDepartmentID == EMPTY_STRING)
				{
					strListDepartmentID = string.Empty;
				}
			}
			
			//Get ID of selected production lines
			if(txtProductionLine.Tag != null)
			{
				strListProductionLineID = "(0";
				foreach(DataRow drow in ((DataTable)txtProductionLine.Tag).Rows)
				{
					strListProductionLineID += ", " + drow[PRO_ProductionLineTable.PRODUCTIONLINEID_FLD].ToString();
				}
				strListProductionLineID += ")";

				//Validate empty value
				if(strListProductionLineID == EMPTY_STRING)
				{
					strListProductionLineID = string.Empty;
				}
			}
			
			//Get ID of selected categories
			if(txtCategory.Tag != null)
			{
				strListCategoryID = "(0";
				foreach(DataRow drow in ((DataTable)txtCategory.Tag).Rows)
				{
					strListCategoryID += ", " + drow[ITM_CategoryTable.CATEGORYID_FLD].ToString();
				}
				strListCategoryID += ")";

				//Validate empty value
				if(strListCategoryID == EMPTY_STRING)
				{
					strListCategoryID = string.Empty;
				}
			}
			
			//Get ID of selected Part Nos
			if(txtPartNo.Tag != null)
			{
				strListProductID = "(0";
				foreach(DataRow drow in ((DataTable)txtPartNo.Tag).Rows)
				{
					strListProductID += ", " + drow[ITM_ProductTable.PRODUCTID_FLD].ToString();
				}
				strListProductID += ")";

				//Validate empty value
				if(strListProductID == EMPTY_STRING)
				{
					strListProductID = string.Empty;
				}
			}

			C1PrintPreviewDialogBO boPrintPreview = new C1PrintPreviewDialogBO();

			return boPrintPreview.GetDestroyMaterialData(cboCCN.SelectedValue.ToString(),
					cboYear.Text, cboMonth.Text, 
					strListDepartmentID, strListProductionLineID, 
					strListCategoryID, strListProductID);			
		}

		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="pstrTableName"></param>
		private void ClearValueInRelatedControl(string pstrTableName)
		{
			switch(pstrTableName)
			{
				case MST_DepartmentTable.TABLE_NAME:
					txtDepartment.Text = string.Empty;
					txtDepartment.Tag = null;

					txtProductionLine.Text = string.Empty;
					txtProductionLine.Tag = null;

					txtPartNo.Text = string.Empty;
					txtPartNo.Tag = null;

					txtPartName.Text = string.Empty;
					txtModel.Text = string.Empty;					

					break;

				case PRO_ProductionLineTable.TABLE_NAME:
					txtProductionLine.Text = string.Empty;
					txtProductionLine.Tag = null;

					txtPartNo.Text = string.Empty;
					txtPartNo.Tag = null;

					txtPartName.Text = string.Empty;
					txtModel.Text = string.Empty;

					break;

				case ITM_CategoryTable.TABLE_NAME:
					txtCategory.Text = string.Empty;
					txtCategory.Tag = null;
					
					txtPartNo.Text = string.Empty;
					txtPartNo.Tag = null;

					txtPartName.Text = string.Empty;
					txtModel.Text = string.Empty;
					
					break;

				case ITM_ProductTable.TABLE_NAME:					
					txtPartNo.Text = string.Empty;
					txtPartNo.Tag = null;

					txtPartName.Text = string.Empty;
					txtModel.Text = string.Empty;
					break;
			}
		}

		
		/// <summary>
		/// Fill related data on controls when select Sale Department
		/// </summary>
		/// <param name="pblnAlwaysShowDialog"></param>
		/// <author> Tuan TQ, 01 Mar, 2006</author>
		private bool SelectProductionLine(bool pblnAlwaysShowDialog)
		{
			string strWhereClause = string.Empty;

			if(txtDepartment.Tag != null)
			{
				strWhereClause += Constants.WHITE_SPACE + PRO_ProductionLineTable.TABLE_NAME + "." + PRO_ProductionLineTable.DEPARTMENTID_FLD + " IN (0";

				foreach(DataRow drowCate in ((DataTable)txtDepartment.Tag).Rows)
				{
					strWhereClause += ", " + drowCate[MST_DepartmentTable.DEPARTMENTID_FLD].ToString();
				}
				strWhereClause += ")";				
			}

			//Call OpenSearchForm for selecting type
			DataTable dtbResult = FormControlComponents.OpenSearchFormForMultiSelectedRow(PRO_ProductionLineTable.TABLE_NAME, PRO_ProductionLineTable.CODE_FLD, txtProductionLine.Text, strWhereClause, pblnAlwaysShowDialog);
			
			// Fill values to form's controls
			if(dtbResult != null)
			{
				if(dtbResult.Rows.Count != 0)
				{
					string strSelectedValue = string.Empty;				
					foreach(DataRow drow in dtbResult.Rows)
					{
						strSelectedValue += drow[PRO_ProductionLineTable.CODE_FLD].ToString() + ", ";
					}
				
					txtProductionLine.Text = strSelectedValue.Substring(0, strSelectedValue.Length - 2);

					txtProductionLine.Tag = dtbResult;
				
					//Reset modify status
					txtProductionLine.Modified = false;				
				}
				else if(!pblnAlwaysShowDialog)
				{					
					txtProductionLine.Focus();
					return false;
				}				
			}
			else if(!pblnAlwaysShowDialog)
			{					
				txtProductionLine.Focus();
				return false;
			}

			return true;
		}


		/// <summary>
		/// Fill related data on controls when select Catagory
		/// </summary>
		/// <param name="pblnAlwaysShowDialog"></param>
		/// <author> Tuan TQ, 01 Mar, 2006</author>
		private bool SelectCategory(bool pblnAlwaysShowDialog)
		{
			//Call OpenSearchForm for selecting Master Location
			DataTable dtbResult = FormControlComponents.OpenSearchFormForMultiSelectedRow(ITM_CategoryTable.TABLE_NAME, ITM_CategoryTable.CODE_FLD, txtCategory.Text, string.Empty, pblnAlwaysShowDialog);
				
			// If has Master location matched searching condition, fill values to form's controls
			if(dtbResult != null)
			{
				if(dtbResult.Rows.Count != 0)
				{
					string strSelectedValue = string.Empty;				
					foreach(DataRow drow in dtbResult.Rows)
					{
						strSelectedValue += drow[ITM_CategoryTable.CODE_FLD].ToString() + ", ";
					}

					txtCategory.Text = strSelectedValue.Substring(0, strSelectedValue.Length - 2);

					txtCategory.Tag = dtbResult;
				
					//Reset modify status
					txtCategory.Modified = false;			
				}
				else if(!pblnAlwaysShowDialog)
				{					
					txtCategory.Focus();
					return false;
				}

			}
			else if(!pblnAlwaysShowDialog)
			{					
				txtCategory.Focus();
				return false;
			}

			return true;			
		}


		/// <summary>
		/// Fill related data on controls when select Model
		/// </summary>
		/// <param name="pblnAlwaysShowDialog"></param>
		/// <author> Tuan TQ, 01 Mar, 2006</author>
		private bool SelectDepartment(bool pblnAlwaysShowDialog)
		{			
			DataTable dtbResult = null;			

			string strWhereClause = string.Empty;

			if(cboCCN.SelectedValue != null)
			{
				strWhereClause += Constants.WHITE_SPACE + MST_DepartmentTable.TABLE_NAME + "." + MST_CCNTable.CCNID_FLD + "=" + cboCCN.SelectedValue.ToString();				
			}			

			//Call OpenSearchForm for selecting MPS planning cycle
			dtbResult = FormControlComponents.OpenSearchFormForMultiSelectedRow(MST_DepartmentTable.TABLE_NAME, MST_DepartmentTable.CODE_FLD, txtDepartment.Text.Trim(), strWhereClause, pblnAlwaysShowDialog);
			
			// If has Master location matched searching condition, fill values to form's controls
			if (dtbResult != null)
			{
				if(dtbResult.Rows.Count != 0)
				{
					string strSelectedValue = string.Empty;				
					foreach(DataRow drow in dtbResult.Rows)
					{
						strSelectedValue += drow[MST_DepartmentTable.CODE_FLD].ToString() + ", ";
					}
				
					txtDepartment.Text = strSelectedValue.Substring(0, strSelectedValue.Length - 2);
					txtDepartment.Tag = dtbResult;
				
					//Reset modify status
					txtDepartment.Modified = false;						
				}
				else if(!pblnAlwaysShowDialog)
				{						
					txtDepartment.Focus();
					return false;
				}				
			}
			else if(!pblnAlwaysShowDialog)
			{						
				txtDepartment.Focus();
				return false;
			}		

			return true;
		}


		/// <summary>
		/// Fill related data on controls when select part no
		/// </summary>
		/// <param name="pblnAlwaysShowDialog"></param>
		/// <author> Tuan TQ, 01 Mar, 2006</author>
		private bool SelectPartNo(bool pblnAlwaysShowDialog)
		{
			//Hashtable htbCriteria = new Hashtable();
			DataTable dtbResult = null;
			
			string strWhereClause = string.Empty;

			if(txtCategory.Tag != null)
			{
				strWhereClause += Constants.WHITE_SPACE + ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.CATEGORYID_FLD + " IN (0";

				foreach(DataRow drowCate in ((DataTable)txtCategory.Tag).Rows)
				{
					strWhereClause += ", " + drowCate[ITM_CategoryTable.CATEGORYID_FLD].ToString();
				}

				strWhereClause += ")";
				
				//htbCriteria.Add(ITM_CategoryTable.CATEGORYID_FLD, strCondition);
			}

			if(txtProductionLine.Tag != null)
			{
				if(strWhereClause != string.Empty)
				{
					strWhereClause += " AND (" + ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.PRODUCTIONLINEID_FLD + " IN (0";
				}
				else
				{
					strWhereClause += "(" + ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.PRODUCTIONLINEID_FLD + " IN (0";
				}

				foreach(DataRow drowProductionLine in ((DataTable)txtProductionLine.Tag).Rows)
				{
					strWhereClause += ", " + drowProductionLine[PRO_ProductionLineTable.PRODUCTIONLINEID_FLD].ToString();
				}
				strWhereClause += "))";
				
				//htbCriteria.Add(PRO_ProductionLineTable.PRODUCTIONLINEID_FLD, strCondition);
			}

			//Call OpenSearchForm for selecting MPS planning cycle
			dtbResult = FormControlComponents.OpenSearchFormForMultiSelectedRow(ITM_ProductTable.TABLE_NAME, ITM_ProductTable.CODE_FLD, txtPartNo.Text, strWhereClause, pblnAlwaysShowDialog);
			
			// Fill values to form's controls
			if (dtbResult != null)
			{	
				if(dtbResult.Rows.Count != 0)
				{
					string strPartNo = string.Empty;
					string strPartName = string.Empty;
					string strPartModel = string.Empty;

					foreach(DataRow drow in dtbResult.Rows)
					{
						strPartNo += drow[ITM_ProductTable.CODE_FLD].ToString() + ", ";
						strPartName += drow[ITM_ProductTable.DESCRIPTION_FLD].ToString() + ", ";
						strPartModel += drow[ITM_ProductTable.REVISION_FLD].ToString() + ", ";
					}

					txtPartNo.Text = strPartNo.Substring(0, strPartNo.Length - 2);
					txtPartName.Text = strPartName.Substring(0, strPartName.Length - 2);
					txtModel.Text = strPartModel.Substring(0, strPartModel.Length - 2);

					txtPartNo.Tag = dtbResult;

					//Reset modify status
					txtPartNo.Modified = false;
					txtPartName.Modified = false;
				}
				else if(!pblnAlwaysShowDialog)
				{			
					txtPartNo.Tag = null;
					txtPartNo.Focus();

					return false;			
				}

			}
			else if(!pblnAlwaysShowDialog)
			{			
				txtPartNo.Tag = null;
				txtPartNo.Focus();

				return false;			
			}

			return true;			
		}


		/// <summary>
		/// Fill related data on controls when select part no
		/// </summary>
		/// <param name="pblnAlwaysShowDialog"></param>
		/// <author> Tuan TQ, 01 Mar, 2006</author>
		private bool SelectPartName(bool pblnAlwaysShowDialog)
		{
			Hashtable htbCriteria = new Hashtable();
			DataTable dtbResult = null;
			
			string strWhereClause = string.Empty;

			if(txtCategory.Tag != null)
			{
				strWhereClause += Constants.WHITE_SPACE +  ITM_ProductTable.TABLE_NAME + "." +  ITM_ProductTable.CATEGORYID_FLD + " IN (0";

				foreach(DataRow drowCate in ((DataTable)txtCategory.Tag).Rows)
				{
					strWhereClause += ", " + drowCate[ITM_CategoryTable.CATEGORYID_FLD].ToString();
				}
				strWhereClause += ")";
				
				//htbCriteria.Add(ITM_CategoryTable.CATEGORYID_FLD, strCondition);
			}

			if(txtProductionLine.Tag != null)
			{
				if(strWhereClause != string.Empty)
				{
					strWhereClause += " AND (" + ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.PRODUCTIONLINEID_FLD + " IN (0";
				}
				else
				{
					strWhereClause += " (" + ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.PRODUCTIONLINEID_FLD + " IN (0";
				}

				foreach(DataRow drowProductionLine in ((DataTable)txtProductionLine.Tag).Rows)
				{
					strWhereClause += ", " + drowProductionLine[PRO_ProductionLineTable.PRODUCTIONLINEID_FLD].ToString();
				}

				strWhereClause += "))";
				
				//htbCriteria.Add(PRO_ProductionLineTable.PRODUCTIONLINEID_FLD, strCondition);
			}

			//Call OpenSearchForm for selecting MPS planning cycle
			dtbResult = FormControlComponents.OpenSearchFormForMultiSelectedRow(ITM_ProductTable.TABLE_NAME, ITM_ProductTable.DESCRIPTION_FLD, txtPartName.Text, strWhereClause, pblnAlwaysShowDialog);
			
			// Fill values to form's controls
			if (dtbResult != null)
			{	
				if(dtbResult.Rows.Count != 0)
				{
					string strPartNo = string.Empty;
					string strPartName = string.Empty;
					string strPartModel = string.Empty;

					foreach(DataRow drow in dtbResult.Rows)
					{
						strPartNo += drow[ITM_ProductTable.CODE_FLD].ToString() + ", ";
						strPartName += drow[ITM_ProductTable.DESCRIPTION_FLD].ToString() + ", ";
						strPartModel += drow[ITM_ProductTable.REVISION_FLD].ToString() + ", ";
					}

					txtPartNo.Text = strPartNo.Substring(0, strPartNo.Length - 2);
					txtPartName.Text = strPartName.Substring(0, strPartName.Length - 2);
					txtModel.Text = strPartModel.Substring(0, strPartModel.Length - 2);

					txtPartNo.Tag = dtbResult;

					//Reset modify status
					txtPartNo.Modified = false;
					txtPartName.Modified = false;
				}
				else if(!pblnAlwaysShowDialog)
				{
					txtPartNo.Tag = null;
					txtPartName.Focus();

					return false;
				}			

			}
			else if(!pblnAlwaysShowDialog)
			{
				txtPartNo.Tag = null;
				txtPartName.Focus();

				return false;			
			}

			return true;			
		}

		#endregion Class's Methods		

		#region Event Processing		
		
		private void btnPrint_EnabledChanged(object sender, System.EventArgs e)
		{
			btnPrintConfiguration.Enabled = ((Control)sender).Enabled;
		}	

		private void ActualCostRelatedReports_Load(object sender, System.EventArgs e)
		{
			const int ADDED_YEAR = 2;
			const int SUBTRACTED_YEAR = 5;
			const string PAD_CHAR = "0";

			const string METHOD_NAME = THIS + ".ActualCostRelatedReports_Load()";
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

				//Load CCN and set default CCN
				UtilsBO boUtils = new UtilsBO();
				DataSet dstCCN = boUtils.ListCCN();
				DateTime dtmServerDate = boUtils.GetDBDate();
				FormControlComponents.PutDataIntoC1ComboBox(cboCCN, dstCCN.Tables[MST_CCNTable.TABLE_NAME], MST_CCNTable.CODE_FLD, MST_CCNTable.CCNID_FLD, MST_CCNTable.TABLE_NAME);				
				//Set default CCN for CNN combobox
				if (SystemProperty.CCNID != 0)
				{
					cboCCN.SelectedValue = SystemProperty.CCNID;
				}
				else
				{
					cboCCN.SelectedIndex = 0;
				}

				FormControlComponents.InitMonthComboBox(cboMonth, true);				
				FormControlComponents.InitYearComboBox(cboYear, dtmServerDate.Year - SUBTRACTED_YEAR, dtmServerDate.Year + ADDED_YEAR, true);

				cboYear.Text  = dtmServerDate.Year.ToString();
				cboMonth.Text = (dtmServerDate.Month < 10)? PAD_CHAR + dtmServerDate.Month.ToString() : dtmServerDate.Month.ToString();

				this.btnPrintConfiguration.Click += new EventHandler(FormControlComponents.ShowMenuReportListHandler);
				this.btnPrint.Click += new EventHandler(FormControlComponents.RunDefaultReportEntriesHandler);

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
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
				}
			}

		}

		private void btnDepartment_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnDepartment_Click()";

			try
			{
				SelectDepartment(true);
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
		}
		
		private void btnPartName_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnPartName_Click()";

			try
			{
				SelectPartName(true);
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
		}

		private void btnPartNo_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnPartNo_Click()";

			try
			{
				SelectPartNo(true);
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
		}

		private void btnCategory_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnCategory_Click()";

			try
			{
				SelectCategory(true);
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
		}

		private void btnProductionLine_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnProductionLine_Click()";

			try
			{
				SelectProductionLine(true);
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
		}

		
		private void btnHelp_Click(object sender, System.EventArgs e)
		{
		
		}

		private void btnClose_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}
		
		private void txtDepartment_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtDepartment_Validating()";
			try
			{				
				//Exit immediately if empty or in default mode				
				if(txtDepartment.Text.Length == 0)
				{
					ClearValueInRelatedControl(MST_DepartmentTable.TABLE_NAME);
					return;
				}
				else if(!txtDepartment.Modified)
				{
					return;
				}
				
				e.Cancel = !SelectDepartment(false);
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
		}

		private void txtPartNo_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtPartNo_Validating()";
			try
			{				
				//Exit immediately if empty or in default mode				
				if(txtPartNo.Text.Length == 0)
				{
					ClearValueInRelatedControl(ITM_ProductTable.TABLE_NAME);
					return;
				}
				else if(!txtPartNo.Modified)
				{
					return;
				}
				
				e.Cancel = !SelectPartNo(false);
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
		}

		private void txtCategory_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtCategory_Validating()";
			try
			{				
				//Exit immediately if empty or in default mode				
				if(txtCategory.Text.Length == 0)
				{
					ClearValueInRelatedControl(ITM_CategoryTable.TABLE_NAME);
					return;
				}
				else if(!txtCategory.Modified)
				{
					return;
				}
				
				e.Cancel = !SelectCategory(false);
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
		}

		private void txtProductionLine_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtProductionLine_Validating()";
			try
			{				
				//Exit immediately if empty or in default mode				
				if(txtProductionLine.Text.Length == 0)
				{
					ClearValueInRelatedControl(PRO_ProductionLineTable.TABLE_NAME);
					return;
				}
				else if(!txtProductionLine.Modified)
				{
					return;
				}
				
				e.Cancel = !SelectProductionLine(false);
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
		}
		
		private void txtDepartment_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtDepartment_KeyDown()";

			try
			{
				if ((e.KeyCode == Keys.F4) && (btnDepartment.Enabled))
				{
					SelectDepartment(true);
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
		}

		private void txtCategory_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtCategory_KeyDown()";

			try
			{
				if ((e.KeyCode == Keys.F4) && (btnCategory.Enabled))
				{
					SelectCategory(true);
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
		}
		
		private void txtProductionLine_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtProductionLine_KeyDown()";

			try
			{
				if ((e.KeyCode == Keys.F4) && (btnProductionLine.Enabled))
				{
					SelectProductionLine(true);
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
		}		
		
		private void cboYear_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			cboMonth.SelectedIndex = 0;			
		}

			
		
		private void txtPartName_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtPartName_Validating()";
			try
			{				
				//Exit immediately if empty or in default mode				
				if(txtPartName.Text.Length == 0)
				{
					ClearValueInRelatedControl(ITM_ProductTable.TABLE_NAME);
					return;
				}
				else if(!txtPartName.Modified)
				{
					return;
				}
				
				e.Cancel = !SelectPartName(false);
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
		}		
		
		private void txtPartName_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtProductionLine_KeyDown()";

			try
			{
				if ((e.KeyCode == Keys.F4) && (btnPartName.Enabled))
				{
					SelectPartName(true);
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
		}
		
		private void txtPartNo_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtPartNo_KeyDown()";

			try
			{
				if ((e.KeyCode == Keys.F4) && (btnPartNo.Enabled))
				{
					SelectPartNo(true);
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
		}

		#endregion Event Processing				
	}
}
