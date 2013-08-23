using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using C1.Win.C1Input;
using C1.Win.C1Preview;
using C1.C1Report;
using C1.Win.C1TrueDBGrid;
using PCSComUtils.Common;
using PCSComUtils.Common.BO;
using PCSComUtils.Framework.ReportFrame.BO;
using PCSComUtils.Framework.ReportFrame.DS;
using PCSComUtils.Framework.TableFrame.DS;
using PCSComUtils.MasterSetup.DS;
using PCSComUtils.PCSExc;
using PCSUtils.Log;
using PCSUtils.Utils;
using CancelEventArgs = System.ComponentModel.CancelEventArgs;
using CancelEventHandler = System.ComponentModel.CancelEventHandler;

namespace PCSUtils.Framework.ReportFrame
{
	/// <summary>
	/// Summary description for ReportView.
	/// </summary>
	public class ViewReport : Form
	{
		#region Form Controls

		private SaveFileDialog dlgSaveFile;
		private Label lblSum;
		private C1TrueDBGrid gridReportData;
		private TabPage tabReportData;
		private TabPage tabParameter;
		private TabPage tabPrintPreview;
		private Button btnCCN;
		private Button btnUser;
		private Button btnPrint;
		private Button btnView;
		private Button btnReset;

		private IContainer components;
		private string reportID;
		private TabControl tabReportViewer;
		private Button btnShowLast10;
		private ToolBarButton toolBarButton1;
		private Button btnPrintLast10;

		private Label lblPrintedOn;
		private Label lblPage;
		private Label lblOf;
		private Label lblPages;

		private C1Report rptReportData;

		private Button btnCancel;
		private Button btnClose;
		private ToolBar tbarReportData;
		private ToolBarButton btnF2;
		private ToolBarButton btnF3;
		private ToolBarButton btnF4;
		private ToolBarButton btnF5;
		private ToolBarButton btnF6;
		private ToolBarButton btnF7;
		private ToolBarButton btnF8;
		private ToolBarButton btnF9;
		private ToolBarButton btnF10;
		private ToolBarButton btnF11;
		private ImageList imglstButtonImage;
		private ToolBarButton toolBarSeperator1;
		private ToolBarButton toolBarSeperator2;
		public Label lblSelectCCNFormCaption;
		public Label lblSelectUserFormCaption;
		
		#endregion

		#region Constants

		private const string SYSTEM_ASSEMBLY = "System.dll";
		private const string SYSTEM_DATA_ASSEMBLY = "System.Data.dll";
		private const string SYSTEM_XML_ASSEMBLY = "System.Xml.dll";
		private const string THIS = "PCSUtils.Framework.ReportFrame.ViewReport";
		private const string COMMAND_SEPERATOR = "-";
		private const char COMMAND_DELIMITER = '-';
		private const string REPORT_DOMAIN_NAME = "DynamicReport";
		private const string LABEL_PREFIX = "lbl";
		private const string TEXTBOX_PREFIX = "txt";
		private const string COMBO_PREFIX = "cbo";
		private const string CHECKBOX_PREFIX = "chk";
		private const string DATEPICKER_PREFIX = "dtm";
		private const string BUTTON_KEYFIELD_PREFIX = "btnKeyField";
		private const string BUTTON_FILTER1_PREFIX = "btnFilter1";
		private const string BUTTON_FILTER2_PREFIX = "btnFilter2";
		private const string TEXTBOX_KEYFIELD_PREFIX = "txtKeyField";
		private const string TEXTBOX_FILTER1_PREFIX = "txtFilter1";
		private const string TEXTBOX_FILTER2_PREFIX = "txtFilter2";
		private const int DEFAULT_HEIGHT = 21;
		private const int OPENSEARCHFORM_BUTTON_MAX_WIDTH = 24;
		private const string OPENSEARCHFORM_BUTTON_TEXT = "...";
		private const int X_LOCATION = 5;
		private const int Y_LOCATION = 25;
		private const int DEFAULT_FIELD_WIDTH = 720;
		private const string EQUAL = " = ";
		private const string OPEN_QUOTE = "(";
		private const string CLOSE_QUOTE = ")";
		private const string DLL_EXTENSION = ".dll";
		private const string SQL_DATE_TIME_FORMAT = "yyyy-MM-dd HH:mm:ss";
		
		#endregion

		private Hashtable htPaperSizes = new Hashtable();

		private ReportParameterBO boReportParameter = new ReportParameterBO();
		private ViewReportBO boViewReport = new ViewReportBO();

		public sys_ReportHistoryVO VoReportHistory
		{
			get { return voReportHistory; }
			set { voReportHistory = value; }
		}

		private sys_ReportHistoryVO voReportHistory;

		private ArrayList marrHistoryPara = new ArrayList();

		public ArrayList ArrHistoryPara
		{
			get { return marrHistoryPara; }
			set { marrHistoryPara = value; }
		}

		public DataTable ReportData
		{
			get { return mtblReportData; }
			set { mtblReportData = value; }
		}

		private DataTable mtblReportData;
		//private DataTable mtblReportAlterData;


		private ArrayList arrCCNs = new ArrayList();
		private ArrayList arrUsers = new ArrayList();

		private ArrayList marrFields = new ArrayList();

		public ArrayList ArrFields
		{
			get { return marrFields; }
			set { marrFields = value; }
		}


		private ArrayList arrParams = new ArrayList();

		private sys_ReportVO mvoReport;

		public sys_ReportVO VoReport
		{
			get { return this.mvoReport; }
			set { this.mvoReport = value; }
		}

		private string strFilterString = string.Empty;
		private string strPreviousFilterString = string.Empty;
		private Label lblError;
		private Label lblLine;
		private bool blnRunRowFilter ; // this variable is used to determine if user has run the RowFilter function before or not

		public ViewReportMode ViewMode
		{
			get { return mViewMode; }
			set { mViewMode = value; }
		}

		private ViewReportMode mViewMode;
		// make an instance of ReportBuilder class
		private ReportBuilder objBuilder = new ReportBuilder();

		//private IDynamicReport mDynamicReport;

		/// <summary>
		///  by default, we render report by Report Viewer engine (in the Report Builder process)
		///  But sometime, we render report in the External Process (in the External C# file) and show on the C1 PrintPreview Dialog (new dialog will show)
		///  so this field is used to determine  where we render the report
		/// </summary>
		private bool mblnUseReportViewerRenderEngine = true;


		private string mstrReportDefFolder = Application.StartupPath + "\\" + Constants.REPORT_DEFINITION_STORE_LOCATION;
		private Panel pnlParameters;
        private C1PrintPreviewControl ReportViewer;

		/// <summary>
		/// Report Data from grid
		/// </summary>
		private DataTable dtbData;

		public ViewReport()
		{
			const string METHOD_NAME = THIS + ".ViewReport()";
			try
			{
				try
				{
					ExcelReportBuilder erb = new ExcelReportBuilder("");
					erb = null;
				}
				catch{}

				InitializeComponent();

                if (SystemProperty.CCNID > 0)
					arrCCNs.Add(SystemProperty.CCNID);
				if (SystemProperty.UserName != string.Empty)
					arrUsers.Add(SystemProperty.UserName);

				#region init hashtable paper size supported: Letter, Legal, A3, A4

				htPaperSizes.Clear();

				htPaperSizes.Add((int) PaperKind.Letter, PaperKind.Letter.ToString());
				htPaperSizes.Add((int) PaperKind.Legal, PaperKind.Legal.ToString());
				htPaperSizes.Add((int) PaperKind.A3, PaperKind.A3.ToString());
				htPaperSizes.Add((int) PaperKind.A4, PaperKind.A4.ToString());

				#endregion
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ViewReport));
            this.tabReportViewer = new System.Windows.Forms.TabControl();
            this.tabParameter = new System.Windows.Forms.TabPage();
            this.lblSelectUserFormCaption = new System.Windows.Forms.Label();
            this.lblSelectCCNFormCaption = new System.Windows.Forms.Label();
            this.lblLine = new System.Windows.Forms.Label();
            this.lblError = new System.Windows.Forms.Label();
            this.lblPages = new System.Windows.Forms.Label();
            this.lblOf = new System.Windows.Forms.Label();
            this.lblPage = new System.Windows.Forms.Label();
            this.lblPrintedOn = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnView = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.lblSum = new System.Windows.Forms.Label();
            this.btnCCN = new System.Windows.Forms.Button();
            this.btnUser = new System.Windows.Forms.Button();
            this.pnlParameters = new System.Windows.Forms.Panel();
            this.btnShowLast10 = new System.Windows.Forms.Button();
            this.btnPrintLast10 = new System.Windows.Forms.Button();
            this.tabReportData = new System.Windows.Forms.TabPage();
            this.tbarReportData = new System.Windows.Forms.ToolBar();
            this.btnF2 = new System.Windows.Forms.ToolBarButton();
            this.btnF3 = new System.Windows.Forms.ToolBarButton();
            this.btnF4 = new System.Windows.Forms.ToolBarButton();
            this.btnF5 = new System.Windows.Forms.ToolBarButton();
            this.toolBarSeperator1 = new System.Windows.Forms.ToolBarButton();
            this.btnF6 = new System.Windows.Forms.ToolBarButton();
            this.btnF7 = new System.Windows.Forms.ToolBarButton();
            this.btnF8 = new System.Windows.Forms.ToolBarButton();
            this.toolBarSeperator2 = new System.Windows.Forms.ToolBarButton();
            this.btnF9 = new System.Windows.Forms.ToolBarButton();
            this.btnF10 = new System.Windows.Forms.ToolBarButton();
            this.btnF11 = new System.Windows.Forms.ToolBarButton();
            this.imglstButtonImage = new System.Windows.Forms.ImageList(this.components);
            this.gridReportData = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
            this.tabPrintPreview = new System.Windows.Forms.TabPage();
            this.ReportViewer = new C1.Win.C1Preview.C1PrintPreviewControl();
            this.toolBarButton1 = new System.Windows.Forms.ToolBarButton();
            this.rptReportData = new C1.C1Report.C1Report();
            this.dlgSaveFile = new System.Windows.Forms.SaveFileDialog();
            this.btnClose = new System.Windows.Forms.Button();
            this.tabReportViewer.SuspendLayout();
            this.tabParameter.SuspendLayout();
            this.tabReportData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridReportData)).BeginInit();
            this.tabPrintPreview.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ReportViewer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ReportViewer.PreviewPane)).BeginInit();
            this.ReportViewer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rptReportData)).BeginInit();
            this.SuspendLayout();
            // 
            // tabReportViewer
            // 
            this.tabReportViewer.Controls.Add(this.tabParameter);
            this.tabReportViewer.Controls.Add(this.tabReportData);
            this.tabReportViewer.Controls.Add(this.tabPrintPreview);
            this.tabReportViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabReportViewer.ItemSize = new System.Drawing.Size(65, 18);
            this.tabReportViewer.Location = new System.Drawing.Point(0, 0);
            this.tabReportViewer.Name = "tabReportViewer";
            this.tabReportViewer.SelectedIndex = 0;
            this.tabReportViewer.Size = new System.Drawing.Size(674, 448);
            this.tabReportViewer.TabIndex = 0;
            this.tabReportViewer.SelectedIndexChanged += new System.EventHandler(this.tabReportViewer_SelectedIndexChanged);
            // 
            // tabParameter
            // 
            this.tabParameter.Controls.Add(this.lblSelectUserFormCaption);
            this.tabParameter.Controls.Add(this.lblSelectCCNFormCaption);
            this.tabParameter.Controls.Add(this.lblLine);
            this.tabParameter.Controls.Add(this.lblError);
            this.tabParameter.Controls.Add(this.lblPages);
            this.tabParameter.Controls.Add(this.lblOf);
            this.tabParameter.Controls.Add(this.lblPage);
            this.tabParameter.Controls.Add(this.lblPrintedOn);
            this.tabParameter.Controls.Add(this.btnCancel);
            this.tabParameter.Controls.Add(this.btnPrint);
            this.tabParameter.Controls.Add(this.btnView);
            this.tabParameter.Controls.Add(this.btnReset);
            this.tabParameter.Controls.Add(this.lblSum);
            this.tabParameter.Controls.Add(this.btnCCN);
            this.tabParameter.Controls.Add(this.btnUser);
            this.tabParameter.Controls.Add(this.pnlParameters);
            this.tabParameter.Controls.Add(this.btnShowLast10);
            this.tabParameter.Controls.Add(this.btnPrintLast10);
            this.tabParameter.Location = new System.Drawing.Point(4, 22);
            this.tabParameter.Name = "tabParameter";
            this.tabParameter.Size = new System.Drawing.Size(666, 422);
            this.tabParameter.TabIndex = 0;
            this.tabParameter.Text = "Parameters";
            // 
            // lblSelectUserFormCaption
            // 
            this.lblSelectUserFormCaption.Location = new System.Drawing.Point(528, 382);
            this.lblSelectUserFormCaption.Name = "lblSelectUserFormCaption";
            this.lblSelectUserFormCaption.Size = new System.Drawing.Size(100, 23);
            this.lblSelectUserFormCaption.TabIndex = 36;
            this.lblSelectUserFormCaption.Text = "User List";
            this.lblSelectUserFormCaption.Visible = false;
            // 
            // lblSelectCCNFormCaption
            // 
            this.lblSelectCCNFormCaption.Location = new System.Drawing.Point(528, 354);
            this.lblSelectCCNFormCaption.Name = "lblSelectCCNFormCaption";
            this.lblSelectCCNFormCaption.Size = new System.Drawing.Size(100, 23);
            this.lblSelectCCNFormCaption.TabIndex = 35;
            this.lblSelectCCNFormCaption.Text = "CCN List";
            this.lblSelectCCNFormCaption.Visible = false;
            // 
            // lblLine
            // 
            this.lblLine.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblLine.Location = new System.Drawing.Point(530, 320);
            this.lblLine.Name = "lblLine";
            this.lblLine.Size = new System.Drawing.Size(94, 22);
            this.lblLine.TabIndex = 34;
            this.lblLine.Text = "\\r\\nLine: ";
            this.lblLine.Visible = false;
            // 
            // lblError
            // 
            this.lblError.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblError.Location = new System.Drawing.Point(530, 298);
            this.lblError.Name = "lblError";
            this.lblError.Size = new System.Drawing.Size(94, 22);
            this.lblError.TabIndex = 33;
            this.lblError.Text = " Error: ";
            this.lblError.Visible = false;
            // 
            // lblPages
            // 
            this.lblPages.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblPages.Location = new System.Drawing.Point(530, 232);
            this.lblPages.Name = "lblPages";
            this.lblPages.Size = new System.Drawing.Size(94, 22);
            this.lblPages.TabIndex = 31;
            this.lblPages.Text = " Pages";
            this.lblPages.Visible = false;
            // 
            // lblOf
            // 
            this.lblOf.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblOf.Location = new System.Drawing.Point(530, 188);
            this.lblOf.Name = "lblOf";
            this.lblOf.Size = new System.Drawing.Size(94, 22);
            this.lblOf.TabIndex = 30;
            this.lblOf.Text = " of ";
            this.lblOf.Visible = false;
            // 
            // lblPage
            // 
            this.lblPage.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblPage.Location = new System.Drawing.Point(530, 210);
            this.lblPage.Name = "lblPage";
            this.lblPage.Size = new System.Drawing.Size(94, 22);
            this.lblPage.TabIndex = 29;
            this.lblPage.Text = "Page ";
            this.lblPage.Visible = false;
            // 
            // lblPrintedOn
            // 
            this.lblPrintedOn.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblPrintedOn.Location = new System.Drawing.Point(530, 276);
            this.lblPrintedOn.Name = "lblPrintedOn";
            this.lblPrintedOn.Size = new System.Drawing.Size(94, 22);
            this.lblPrintedOn.TabIndex = 28;
            this.lblPrintedOn.Text = ": Printed On ";
            this.lblPrintedOn.Visible = false;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnCancel.Location = new System.Drawing.Point(560, 83);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(94, 22);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "&Close";
            this.btnCancel.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrint.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnPrint.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnPrint.Location = new System.Drawing.Point(560, 31);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(94, 22);
            this.btnPrint.TabIndex = 3;
            this.btnPrint.Text = "&Print";
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnView
            // 
            this.btnView.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnView.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnView.Location = new System.Drawing.Point(560, 5);
            this.btnView.Name = "btnView";
            this.btnView.Size = new System.Drawing.Size(94, 22);
            this.btnView.TabIndex = 1;
            this.btnView.Text = "&Execute";
            this.btnView.Click += new System.EventHandler(this.btnView_Click);
            // 
            // btnReset
            // 
            this.btnReset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReset.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnReset.Location = new System.Drawing.Point(560, 57);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(94, 22);
            this.btnReset.TabIndex = 5;
            this.btnReset.Text = "&Reset";
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // lblSum
            // 
            this.lblSum.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblSum.Location = new System.Drawing.Point(530, 254);
            this.lblSum.Name = "lblSum";
            this.lblSum.Size = new System.Drawing.Size(94, 22);
            this.lblSum.TabIndex = 32;
            this.lblSum.Text = "Sum [";
            this.lblSum.Visible = false;
            // 
            // btnCCN
            // 
            this.btnCCN.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCCN.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnCCN.Location = new System.Drawing.Point(560, 109);
            this.btnCCN.Name = "btnCCN";
            this.btnCCN.Size = new System.Drawing.Size(94, 22);
            this.btnCCN.TabIndex = 6;
            this.btnCCN.Text = "CC&N";
            this.btnCCN.Visible = false;
            this.btnCCN.Click += new System.EventHandler(this.btnCCN_Click);
            // 
            // btnUser
            // 
            this.btnUser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUser.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnUser.Location = new System.Drawing.Point(560, 135);
            this.btnUser.Name = "btnUser";
            this.btnUser.Size = new System.Drawing.Size(94, 22);
            this.btnUser.TabIndex = 7;
            this.btnUser.Text = "&User";
            this.btnUser.Visible = false;
            this.btnUser.Click += new System.EventHandler(this.btnUser_Click);
            // 
            // pnlParameters
            // 
            this.pnlParameters.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlParameters.AutoScroll = true;
            this.pnlParameters.Location = new System.Drawing.Point(0, 0);
            this.pnlParameters.Name = "pnlParameters";
            this.pnlParameters.Size = new System.Drawing.Size(548, 422);
            this.pnlParameters.TabIndex = 0;
            // 
            // btnShowLast10
            // 
            this.btnShowLast10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnShowLast10.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnShowLast10.Location = new System.Drawing.Point(637, 5);
            this.btnShowLast10.Name = "btnShowLast10";
            this.btnShowLast10.Size = new System.Drawing.Size(17, 22);
            this.btnShowLast10.TabIndex = 2;
            this.btnShowLast10.Text = "!";
            this.btnShowLast10.Visible = false;
            this.btnShowLast10.Click += new System.EventHandler(this.btnShowLast10_Click);
            // 
            // btnPrintLast10
            // 
            this.btnPrintLast10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrintLast10.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnPrintLast10.Location = new System.Drawing.Point(637, 31);
            this.btnPrintLast10.Name = "btnPrintLast10";
            this.btnPrintLast10.Size = new System.Drawing.Size(17, 22);
            this.btnPrintLast10.TabIndex = 4;
            this.btnPrintLast10.Text = "!";
            this.btnPrintLast10.Visible = false;
            this.btnPrintLast10.Click += new System.EventHandler(this.btnPrintLast10_Click);
            // 
            // tabReportData
            // 
            this.tabReportData.Controls.Add(this.tbarReportData);
            this.tabReportData.Controls.Add(this.gridReportData);
            this.tabReportData.Location = new System.Drawing.Point(4, 22);
            this.tabReportData.Name = "tabReportData";
            this.tabReportData.Size = new System.Drawing.Size(666, 422);
            this.tabReportData.TabIndex = 1;
            this.tabReportData.Text = "Report Data";
            // 
            // tbarReportData
            // 
            this.tbarReportData.AutoSize = false;
            this.tbarReportData.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
            this.btnF2,
            this.btnF3,
            this.btnF4,
            this.btnF5,
            this.toolBarSeperator1,
            this.btnF6,
            this.btnF7,
            this.btnF8,
            this.toolBarSeperator2,
            this.btnF9,
            this.btnF10,
            this.btnF11});
            this.tbarReportData.ButtonSize = new System.Drawing.Size(32, 32);
            this.tbarReportData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbarReportData.DropDownArrows = true;
            this.tbarReportData.ImageList = this.imglstButtonImage;
            this.tbarReportData.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.tbarReportData.Location = new System.Drawing.Point(0, 0);
            this.tbarReportData.Name = "tbarReportData";
            this.tbarReportData.ShowToolTips = true;
            this.tbarReportData.Size = new System.Drawing.Size(666, 48);
            this.tbarReportData.TabIndex = 0;
            this.tbarReportData.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.tbarReportData_ButtonClick);
            // 
            // btnF2
            // 
            this.btnF2.ImageIndex = 0;
            this.btnF2.Name = "btnF2";
            this.btnF2.ToolTipText = "F2 - Clear all filter";
            // 
            // btnF3
            // 
            this.btnF3.ImageIndex = 1;
            this.btnF3.Name = "btnF3";
            this.btnF3.ToolTipText = "F3: Filter with current value";
            // 
            // btnF4
            // 
            this.btnF4.ImageIndex = 2;
            this.btnF4.Name = "btnF4";
            this.btnF4.ToolTipText = "F4: Filter with except current value";
            // 
            // btnF5
            // 
            this.btnF5.ImageIndex = 3;
            this.btnF5.Name = "btnF5";
            this.btnF5.ToolTipText = "F5: Return previous filter";
            // 
            // toolBarSeperator1
            // 
            this.toolBarSeperator1.Name = "toolBarSeperator1";
            this.toolBarSeperator1.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // btnF6
            // 
            this.btnF6.ImageIndex = 4;
            this.btnF6.Name = "btnF6";
            this.btnF6.ToolTipText = "F6: Row filter";
            // 
            // btnF7
            // 
            this.btnF7.ImageIndex = 5;
            this.btnF7.Name = "btnF7";
            this.btnF7.ToolTipText = "F7: View single record";
            // 
            // btnF8
            // 
            this.btnF8.ImageIndex = 6;
            this.btnF8.Name = "btnF8";
            this.btnF8.ToolTipText = "F8: Sum current column";
            // 
            // toolBarSeperator2
            // 
            this.toolBarSeperator2.Name = "toolBarSeperator2";
            this.toolBarSeperator2.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // btnF9
            // 
            this.btnF9.ImageIndex = 7;
            this.btnF9.Name = "btnF9";
            this.btnF9.ToolTipText = "F9: Export data to Excel";
            // 
            // btnF10
            // 
            this.btnF10.ImageIndex = 8;
            this.btnF10.Name = "btnF10";
            this.btnF10.ToolTipText = "F10: Print data to printer";
            // 
            // btnF11
            // 
            this.btnF11.ImageIndex = 9;
            this.btnF11.Name = "btnF11";
            this.btnF11.ToolTipText = "F11: Show drill down report";
            // 
            // imglstButtonImage
            // 
            this.imglstButtonImage.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imglstButtonImage.ImageStream")));
            this.imglstButtonImage.TransparentColor = System.Drawing.Color.Transparent;
            this.imglstButtonImage.Images.SetKeyName(0, "");
            this.imglstButtonImage.Images.SetKeyName(1, "");
            this.imglstButtonImage.Images.SetKeyName(2, "");
            this.imglstButtonImage.Images.SetKeyName(3, "");
            this.imglstButtonImage.Images.SetKeyName(4, "");
            this.imglstButtonImage.Images.SetKeyName(5, "");
            this.imglstButtonImage.Images.SetKeyName(6, "");
            this.imglstButtonImage.Images.SetKeyName(7, "");
            this.imglstButtonImage.Images.SetKeyName(8, "");
            this.imglstButtonImage.Images.SetKeyName(9, "");
            // 
            // gridReportData
            // 
            this.gridReportData.AllowUpdate = false;
            this.gridReportData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gridReportData.GroupByCaption = "Drag a column header here to group by that column";
            this.gridReportData.Images.Add(((System.Drawing.Image)(resources.GetObject("gridReportData.Images"))));
            this.gridReportData.Location = new System.Drawing.Point(0, 48);
            this.gridReportData.Name = "gridReportData";
            this.gridReportData.PreviewInfo.Location = new System.Drawing.Point(0, 0);
            this.gridReportData.PreviewInfo.Size = new System.Drawing.Size(0, 0);
            this.gridReportData.PreviewInfo.ZoomFactor = 75;
            this.gridReportData.PrintInfo.PageSettings = ((System.Drawing.Printing.PageSettings)(resources.GetObject("gridReportData.PrintInfo.PageSettings")));
            this.gridReportData.Size = new System.Drawing.Size(666, 367);
            this.gridReportData.TabIndex = 1;
            this.gridReportData.PropBag = resources.GetString("gridReportData.PropBag");
            // 
            // tabPrintPreview
            // 
            this.tabPrintPreview.Controls.Add(this.ReportViewer);
            this.tabPrintPreview.Location = new System.Drawing.Point(4, 22);
            this.tabPrintPreview.Name = "tabPrintPreview";
            this.tabPrintPreview.Size = new System.Drawing.Size(666, 422);
            this.tabPrintPreview.TabIndex = 2;
            this.tabPrintPreview.Text = "Print Preview";
            // 
            // ReportViewer
            // 
            this.ReportViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ReportViewer.Location = new System.Drawing.Point(0, 0);
            this.ReportViewer.Name = "ReportViewer";
            this.ReportViewer.NavigationPanelVisible = false;
            // 
            // ReportViewer.OutlineView
            // 
            this.ReportViewer.PreviewOutlineView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ReportViewer.PreviewOutlineView.Location = new System.Drawing.Point(0, 0);
            this.ReportViewer.PreviewOutlineView.Name = "OutlineView";
            this.ReportViewer.PreviewOutlineView.Size = new System.Drawing.Size(165, 427);
            this.ReportViewer.PreviewOutlineView.TabIndex = 0;
            // 
            // ReportViewer.PreviewPane
            // 
            this.ReportViewer.PreviewPane.IntegrateExternalTools = true;
            this.ReportViewer.PreviewPane.TabIndex = 0;
            this.ReportViewer.PreviewPane.ZoomMode = C1.Win.C1Preview.ZoomModeEnum.PageWidth;
            // 
            // ReportViewer.PreviewTextSearchPanel
            // 
            this.ReportViewer.PreviewTextSearchPanel.Dock = System.Windows.Forms.DockStyle.Right;
            this.ReportViewer.PreviewTextSearchPanel.Location = new System.Drawing.Point(530, 0);
            this.ReportViewer.PreviewTextSearchPanel.MinimumSize = new System.Drawing.Size(200, 240);
            this.ReportViewer.PreviewTextSearchPanel.Name = "PreviewTextSearchPanel";
            this.ReportViewer.PreviewTextSearchPanel.Size = new System.Drawing.Size(200, 453);
            this.ReportViewer.PreviewTextSearchPanel.TabIndex = 0;
            this.ReportViewer.PreviewTextSearchPanel.Visible = false;
            // 
            // ReportViewer.ThumbnailView
            // 
            this.ReportViewer.PreviewThumbnailView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ReportViewer.PreviewThumbnailView.Location = new System.Drawing.Point(0, 0);
            this.ReportViewer.PreviewThumbnailView.Name = "ThumbnailView";
            this.ReportViewer.PreviewThumbnailView.Size = new System.Drawing.Size(165, 427);
            this.ReportViewer.PreviewThumbnailView.TabIndex = 0;
            this.ReportViewer.PreviewThumbnailView.UseImageAsThumbnail = false;
            this.ReportViewer.Size = new System.Drawing.Size(666, 422);
            this.ReportViewer.TabIndex = 0;
            this.ReportViewer.Text = "c1PrintPreviewControl1";
            // 
            // 
            // 
            this.ReportViewer.ToolBars.Page.Facing.Image = ((System.Drawing.Image)(resources.GetObject("ReportViewer.ToolBars.Page.Facing.Image")));
            this.ReportViewer.ToolBars.Page.Facing.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ReportViewer.ToolBars.Page.Facing.Name = "btnPageFacing";
            this.ReportViewer.ToolBars.Page.Facing.Size = new System.Drawing.Size(23, 22);
            this.ReportViewer.ToolBars.Page.Facing.Tag = "C1PreviewActionEnum.PageFacing";
            this.ReportViewer.ToolBars.Page.Facing.ToolTipText = "Pages Facing View";
            // 
            // 
            // 
            this.ReportViewer.ToolBars.Page.FacingContinuous.Image = ((System.Drawing.Image)(resources.GetObject("ReportViewer.ToolBars.Page.FacingContinuous.Image")));
            this.ReportViewer.ToolBars.Page.FacingContinuous.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ReportViewer.ToolBars.Page.FacingContinuous.Name = "btnPageFacingContinuous";
            this.ReportViewer.ToolBars.Page.FacingContinuous.Size = new System.Drawing.Size(23, 22);
            this.ReportViewer.ToolBars.Page.FacingContinuous.Tag = "C1PreviewActionEnum.PageFacingContinuous";
            this.ReportViewer.ToolBars.Page.FacingContinuous.ToolTipText = "Pages Facing Continuous View";
            // 
            // 
            // 
            this.ReportViewer.ToolBars.Text.Find.Image = ((System.Drawing.Image)(resources.GetObject("ReportViewer.ToolBars.Text.Find.Image")));
            this.ReportViewer.ToolBars.Text.Find.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ReportViewer.ToolBars.Text.Find.Name = "btnFind";
            this.ReportViewer.ToolBars.Text.Find.Size = new System.Drawing.Size(23, 20);
            this.ReportViewer.ToolBars.Text.Find.Tag = "C1PreviewActionEnum.Find";
            this.ReportViewer.ToolBars.Text.Find.ToolTipText = "Find Text";
            // 
            // 
            // 
            this.ReportViewer.ToolBars.Text.SelectText.Image = ((System.Drawing.Image)(resources.GetObject("ReportViewer.ToolBars.Text.SelectText.Image")));
            this.ReportViewer.ToolBars.Text.SelectText.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ReportViewer.ToolBars.Text.SelectText.Name = "btnSelectTextTool";
            this.ReportViewer.ToolBars.Text.SelectText.Size = new System.Drawing.Size(23, 20);
            this.ReportViewer.ToolBars.Text.SelectText.Tag = "C1PreviewActionEnum.SelectTextTool";
            this.ReportViewer.ToolBars.Text.SelectText.ToolTipText = "Text Select Tool";
            // 
            // toolBarButton1
            // 
            this.toolBarButton1.ImageIndex = 0;
            this.toolBarButton1.Name = "toolBarButton1";
            this.toolBarButton1.Text = "F2";
            this.toolBarButton1.ToolTipText = "F2";
            // 
            // rptReportData
            // 
            this.rptReportData.ReportDefinition = resources.GetString("rptReportData.ReportDefinition");
            this.rptReportData.ReportName = "Execute Report";
            // 
            // dlgSaveFile
            // 
            this.dlgSaveFile.Filter = "Excel file|*.xls";
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnClose.Location = new System.Drawing.Point(506, 18);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(94, 22);
            this.btnClose.TabIndex = 28;
            this.btnClose.Text = "&Close";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // ViewReport
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(674, 448);
            this.Controls.Add(this.tabReportViewer);
            this.Controls.Add(this.btnClose);
            this.KeyPreview = true;
            this.Name = "ViewReport";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Report Viewer - [";
            this.Load += new System.EventHandler(this.frmReportView_Load);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.ViewReport_Closing);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            this.tabReportViewer.ResumeLayout(false);
            this.tabParameter.ResumeLayout(false);
            this.tabReportData.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridReportData)).EndInit();
            this.tabPrintPreview.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ReportViewer.PreviewPane)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ReportViewer)).EndInit();
            this.ReportViewer.ResumeLayout(false);
            this.ReportViewer.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rptReportData)).EndInit();
            this.ResumeLayout(false);

		}

		#endregion

		#region Form events

		private void frmReportView_Load(object sender, EventArgs e)
		{
			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.WaitCursor;

			#endregion Code Inserted Automatically

			
			const string METHOD_NAME = THIS + ".frmReportView_Load()";
			try
			{
				FieldPropertiesBO boFieldProperties = new FieldPropertiesBO();
				marrFields = boFieldProperties.ListByReport(mvoReport.ReportID);

				// get all parameter of report
				arrParams = boReportParameter.ListByReport(mvoReport.ReportID);

				// fill parameter(s) to group box
				LoadParameter(arrParams, pnlParameters);		

				SetDefaultParameter();

				if (Text.IndexOf(mvoReport.ReportName) < 0)
				{
					Text = Text + mvoReport.ReportName + Constants.CLOSE_SBRACKET;
				}
				if (mViewMode == ViewReportMode.Normal)
				{
					tabPrintPreview.Enabled = true;
					tabReportData.Enabled = false;
				}
				else if (mViewMode == ViewReportMode.History)
				{
					tabPrintPreview.Enabled = true;
					tabReportData.Enabled = true;
					// get all parameter and its value from history
					marrHistoryPara = (new LastReportBO()).GetHistoryPara(voReportHistory.HistoryID.ToString());
					// assign value to all parameter control in group box
					AssignValue(pnlParameters);

					// bind to True DB grid
					gridReportData.DataSource = mtblReportData;
					gridReportData.UpdateData();


					// alert user to view the report
					PCSMessageBox.Show(ErrorCode.MESSAGE_EXECUTE_REPORT_SUCCEED, MessageBoxButtons.OK, MessageBoxIcon.Information);
					// select tab for user
					tabReportViewer.SelectedTab = tabReportData;
				}

				#region Security

				//Set authorization for user
				Security objSecurity = new Security();
				this.Name = THIS;
				if (objSecurity.SetRightForUserOnForm(this, SystemProperty.UserName) == 0)
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

				// focus on the first control in panel
				for (int i = 0; i < pnlParameters.Controls.Count; i++)
				{
					Control objControl = pnlParameters.Controls[i];
					if (objControl.GetType().Equals(typeof (Label)) || !objControl.Visible)
						continue;
					else
					{
						objControl.Focus();
						break;
					}
				}
				gridReportData.FilterBar = false;
				blnRunRowFilter = false;
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

		private void btnView_Click(object sender, EventArgs e)
		{
			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.WaitCursor;

			#endregion Code Inserted Automatically

			
			const string METHOD_NAME = THIS + ".btnView_Click()";
			try
			{
				Cursor = Cursors.WaitCursor;

				#region check for mandatory parameter first				

				foreach (Control objControl in pnlParameters.Controls)
				{
					if (!objControl.Visible)
						continue;
					if (!(objControl is Label) && !(objControl is Button)) // object is not label
					{
						sys_ReportParaVO voReportPapra = (sys_ReportParaVO) (objControl.Tag);
						if (!voReportPapra.Optional) // param is must-have value
						{
							if ((objControl is C1DateEdit) && ((C1DateEdit) objControl).ValueIsDbNull)
							{
								PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxButtons.OK, MessageBoxIcon.Warning);
								objControl.Focus();
								objControl.Select();
								// Code Inserted Automatically
#region Code Inserted Automatically
this.Cursor = Cursors.Default;
#endregion Code Inserted Automatically

								return;
							}
							else if ((objControl is CheckBox) && (((CheckBox) objControl).CheckState == CheckState.Indeterminate))
							{
								PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxButtons.OK, MessageBoxIcon.Warning);
								objControl.Focus();
								objControl.Select();
								// Code Inserted Automatically
#region Code Inserted Automatically
this.Cursor = Cursors.Default;
#endregion Code Inserted Automatically

								return;
							}
							else if (objControl is ComboBox)
							{
								if (((ComboBox) objControl).SelectedIndex < 0)
								{
									PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxButtons.OK, MessageBoxIcon.Warning);
									objControl.Focus();
									objControl.Select();
									// Code Inserted Automatically
#region Code Inserted Automatically
this.Cursor = Cursors.Default;
#endregion Code Inserted Automatically

									return;
								}
							}
							else if (objControl is TextBox)
							{
								if (objControl.Name.Equals(TEXTBOX_KEYFIELD_PREFIX + voReportPapra.ParaName + voReportPapra.FromField) ||
									objControl.Name.Equals(TEXTBOX_FILTER1_PREFIX + voReportPapra.ParaName + voReportPapra.FilterField1) ||
									objControl.Name.Equals(TEXTBOX_FILTER2_PREFIX + voReportPapra.ParaName + voReportPapra.FilterField2))
								{
									try
									{
										Control objFilter1 = GetControlFromPanel(this.pnlParameters, TEXTBOX_FILTER1_PREFIX + voReportPapra.ParaName + voReportPapra.FilterField1);
										if (objFilter1.Text.Trim() == string.Empty)
										{
											PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxButtons.OK, MessageBoxIcon.Warning);
											objFilter1.Focus();
											objFilter1.Select();
											// Code Inserted Automatically
#region Code Inserted Automatically
this.Cursor = Cursors.Default;
#endregion Code Inserted Automatically

											return;
										}
									}
									catch
									{
										// Code Inserted Automatically
#region Code Inserted Automatically
this.Cursor = Cursors.Default;
#endregion Code Inserted Automatically

										return;
									}
								}
								else // normal textbox
								{
									if (objControl.Text.Trim() == string.Empty)
									{
										PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxButtons.OK, MessageBoxIcon.Warning);
										objControl.Focus();
										objControl.Select();
										// Code Inserted Automatically
#region Code Inserted Automatically
this.Cursor = Cursors.Default;
#endregion Code Inserted Automatically

										return;
									}
								}
							}
						}
					}
				} // end foreach

				#endregion

				// temporarily suppends the layout logic for the control.
				SuspendLayout();
				// enable ReporData tab for user select
				tabReportData.Enabled = true;
				tabPrintPreview.Enabled = true;

				gridReportData.DataSource = null;

				#region execute report (run the SQL command, fill in the mtblReportData)

				if (mvoReport.ReportType == Constants.SQL_REPORT)
				{
					ExecuteSQLReport();
				}
				else if (mvoReport.ReportType == Constants.DLL_REPORT)
				{
					ExecuteDLLReport(REPORT_DOMAIN_NAME);
				}
				else if (mvoReport.ReportType == Constants.CSHARP_FILE_REPORT)
				{					
					ExecuteFileReport(REPORT_DOMAIN_NAME);
				}
				else if (mvoReport.ReportType == Constants.CUSTOM_REPORT)
				{
					//HACKED: Thachnn add
					ExecuteCustomReport();
				}

				#endregion

				// if run here, mean OK, alert user to view the report
				PCSMessageBox.Show(ErrorCode.MESSAGE_EXECUTE_REPORT_SUCCEED, MessageBoxButtons.OK, MessageBoxIcon.Information);
				// select tab for user
				//this.SetColumnForTrueDBGrid();

				/// HACKED: Thachnn: 27/12/2005
				if( mblnUseReportViewerRenderEngine )
				{
					tabReportViewer.SelectedTab = tabReportData;
				}
				/// ENDHACKED: Thachnn 27/12/2005: do not focus on the TabReportData if we render by  External Process
				/// We will focus on the External Report Viewer
			}
				#region catch	

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
			catch (ArgumentNullException ex)
			{
				// displays the error message.
				PCSMessageBox.Show(ErrorCode.ARGUMENTNULLEXCEPTION);
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
			catch (FileNotFoundException ex)
			{
				// displays the error message.
				PCSMessageBox.Show(ErrorCode.MESSAGE_REPORT_FILE_NOT_FOUND);
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
			catch (Exception ex)
			{
				// displays the error message.
				PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
				//DEBUG:
				//MessageBox.Show(ex.Message);

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
				#endregion

			finally
			{
				ResumeLayout(); // free the form
				Cursor = Cursors.Default;
			}

			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.Default;

			#endregion Code Inserted Automatically

			
		}

		private void btnPrint_Click(object sender, EventArgs e)
		{
			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.WaitCursor;

			#endregion Code Inserted Automatically

			const string METHOD_NAME = THIS + ".btnPrint_Click()";
			try
			{
				btnView_Click(sender, e);
			}
				#region catch	

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
			catch (ArgumentNullException ex)
			{
				// displays the error message.
				PCSMessageBox.Show(ErrorCode.ARGUMENTNULLEXCEPTION);
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
			catch (FileNotFoundException ex)
			{
				// displays the error message.
				PCSMessageBox.Show(ErrorCode.MESSAGE_REPORT_FILE_NOT_FOUND);
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
			catch (Exception ex)
			{
				// displays the error message.
				PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
				//DEBUG:
				//MessageBox.Show(ex.Message);

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
				#endregion

			finally
			{
				ResumeLayout(); // free the form
				Cursor = Cursors.Default;
			}

			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.Default;

			#endregion Code Inserted Automatically

			
		}

		private void btnShowLast10_Click(object sender, EventArgs e)
		{
			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.WaitCursor;

			#endregion Code Inserted Automatically

			
			const string METHOD_NAME = THIS + ".btnShowLast10_Click()";
			try
			{
				// show last report form
				LastReport frmLastReport = new LastReport();
				frmLastReport.ShowDialog();
				if (frmLastReport.ReturnValue == null)
				{
					// Code Inserted Automatically
#region Code Inserted Automatically
this.Cursor = Cursors.Default;
#endregion Code Inserted Automatically

					return;
				}
				mViewMode = ViewReportMode.History;
				// get report history table
				voReportHistory = frmLastReport.ReturnValue;
				// get data from history table
				mtblReportData = boViewReport.GetDataFromHistoryTable(voReportHistory.TableName);

				// reload form to get new data.
				frmReportView_Load(sender, e);
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

		private void btnPrintLast10_Click(object sender, EventArgs e)
		{
			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.WaitCursor;

			#endregion Code Inserted Automatically

			
			const string METHOD_NAME = THIS + ".btnPrintLast10_Click()";
			try
			{
				btnShowLast10_Click(this, e);
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

		private void btnReset_Click(object sender, EventArgs e)
		{
			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.WaitCursor;

			#endregion Code Inserted Automatically

			
			const string METHOD_NAME = THIS + "btnReset_Click()";
			try
			{
				foreach (Control objControl in pnlParameters.Controls)
				{
					if (objControl is Label)
					{
						continue;
					}
					if (objControl is C1DateEdit)
					{
						((C1DateEdit) objControl).Value = DBNull.Value;
						continue;
					}
					if (objControl is TextBox)
					{
						objControl.Text = string.Empty;
						continue;
					}
					if (objControl is ComboBox)
					{
						((ComboBox) objControl).SelectedIndex = 0;
						continue;
					}
					if (objControl is CheckBox)
					{
						((CheckBox) objControl).CheckState = CheckState.Indeterminate;
						continue;
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

		private void btnCCN_Click(object sender, EventArgs e)
		{
			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.WaitCursor;

			#endregion Code Inserted Automatically

			
			const string METHOD_NAME = THIS + ".btnCCN_Click()";
			try
			{
				UserCCNList frmUserCCN = new UserCCNList(MST_CCNTable.TABLE_NAME);
				frmUserCCN.Text = lblSelectCCNFormCaption.Text;
				frmUserCCN.ShowDialog();
				arrCCNs = frmUserCCN.ReturnList;
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

		private void btnUser_Click(object sender, EventArgs e)
		{
			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.WaitCursor;

			#endregion Code Inserted Automatically

			
			const string METHOD_NAME = THIS + ".btnUser_Click()";
			try
			{
				UserCCNList frmUserCCN = new UserCCNList(Sys_UserTable.TABLE_NAME);
				frmUserCCN.Text = lblSelectUserFormCaption.Text;
				frmUserCCN.ShowDialog();
				arrUsers = frmUserCCN.ReturnList;
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

		private void tabReportViewer_SelectedIndexChanged(object sender, EventArgs e)
		{
			const string METHOD_NAME = THIS + ".tabReportViewer_SelectedIndexChanged()";
			try
			{
				if (mtblReportData == null)
					return;

				if (tabReportViewer.SelectedTab.Equals(tabReportData) ||
					tabReportViewer.SelectedTab.Equals(tabPrintPreview))
					this.WindowState = FormWindowState.Maximized;
				else
					this.WindowState = FormWindowState.Normal;

				if (tabReportViewer.SelectedTab.Equals(tabPrintPreview))
				{
					this.Cursor = Cursors.WaitCursor;
					// get data from data source of grid			
					dtbData = mtblReportData.Clone();
                    for (int i = 0; i < gridReportData.RowCount; i++)
					{
						gridReportData.Row = i;
						DataRow drowData = dtbData.NewRow();
						foreach (DataColumn dcolData in dtbData.Columns)
							drowData[dcolData.ColumnName] = gridReportData.Columns[dcolData.ColumnName].Value;
						dtbData.Rows.Add(drowData);
					}
					if (mvoReport.UseTemplate)
					{
						RenderReportWithDefinitionFile(dtbData);
					}
					else
					{
						RenderReport(dtbData, SystemProperty.LogoFile);
					}
				}
				if (tabReportViewer.SelectedTab.Equals(tabReportData))
				{
					/// if we render report in the External C# process file, we remove the tabPrintPreview and do not focus on the tabReportData
					if(mblnUseReportViewerRenderEngine == false)
					{
						/// HACKED: Thachnn: 27/12/2005:
//						tabReportViewer.SelectedTab = tabReportData;
						/// ENDHACKED: Thachnn 27/12/2005
						try
						{
							tabReportViewer.TabPages.Remove(tabPrintPreview);						
						}
						catch{}
					}

					this.Cursor = Cursors.WaitCursor;
					gridReportData.DataSource = mtblReportData;					
					this.Cursor = Cursors.Default;
				}
			}
			catch (PCSException ex)
			{
				if (ex.mCode == ErrorCode.MESSAGE_INVALID_FORMAT)
				{
					string[] strMsg = new string[2];
					strMsg[0] = sys_ReportTable.SIGNATURES_FLD;
					strMsg[1] = "Title#(sign, name)";
					// displays the error message.
					PCSMessageBox.Show(ErrorCode.MESSAGE_INVALID_FORMAT, MessageBoxIcon.Error, strMsg);
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
				else
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
			}
			catch (Exception ex)
			{
				// displays the error message.
				//DEBUG:
				//MessageBox.Show(ex.Message);
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
			finally
			{
				this.Cursor = Cursors.Default;
			}
		}

		private void ControlKeyDown(object sender, KeyEventArgs e)
		{
			const string METHOD_NAME = THIS + ".ControlKeyDown()";
			try
			{ // enable shortcut when user are in ReportDataTab
				if (tabReportViewer.SelectedTab == tabReportData)
				{
					switch (e.KeyCode)
					{
						case Keys.F2:
							// clear all filter
							ClearAllFilter();
							break;
						case Keys.F3:
							// filter with current value
							FilterWithCurrentValue(false);
							break;
						case Keys.F4:
							// filter with except current value
							FilterWithCurrentValue(true);
							break;
						case Keys.F5:
							// return previous filter
							ReturnPreviousFilter();
							break;
						case Keys.F6:
							// row filter
							RowFilter();
							break;
						case Keys.F7:
							// single Record
							ViewSingleRecord();
							break;
						case Keys.F8:
							// sum current column
							SumCurrentColumn();
							break;
						case Keys.F9:
							// export to Excel
							// display form allow user to select file name
							SaveFileDialog saveFile = new SaveFileDialog();

							saveFile.DefaultExt = "*.xls";
							saveFile.Filter = "xls File|*.xls| All File|*.*";

							if(saveFile.ShowDialog() == System.Windows.Forms.DialogResult.OK &&	saveFile.FileName.Length > 0) 
							{
								try
								{	
									if(System.IO.File.Exists(saveFile.FileName))
										File.Delete(saveFile.FileName);

									//tgridViewTable.ExportToExcel(saveFile.FileName);
									gridReportData.ExportToDelimitedFile(saveFile.FileName,RowSelectorEnum.AllRows,"\t",""," ",true, "Unicode");
									PCSMessageBox.Show(ErrorCode.MESSAGE_TASK_COMPLETED,MessageBoxButtons.OK,MessageBoxIcon.Information,MessageBoxDefaultButton.Button1,new string[]{"Exporting"});
								}
								catch
								{
									PCSMessageBox.Show(ErrorCode.CAN_NOT_READ_EXCEL_FILE,MessageBoxButtons.OK,MessageBoxIcon.Error);
								}
							}
//							if (dlgSaveFile.ShowDialog() == DialogResult.OK)
//							{
//								// get the file name
//								string strFileName = dlgSaveFile.FileName;
//								// save data to file
//								try
//								{
//									gridReportData.ExportToExcel(strFileName);
//									PCSMessageBox.Show(ErrorCode.MESSAGE_EXPORT_EXCEL_SUCCEED, MessageBoxButtons.OK, MessageBoxIcon.Information);
//								}
//								catch (Exception ex)
//								{
//									throw ex;
//								}
//							}
							break;
						case Keys.F10:
							// Print data to printer
							PrintDataToPrinter();
							break;
						case Keys.F11:
							// Show drill down report
							ShowDrillDownReport();
							break;
						case Keys.Escape:
							Close();
							break;
					}
				}
				else
				{
					switch (e.KeyCode)
					{
						case Keys.Escape:
							Close();
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

		private void tbarReportData_ButtonClick(object sender, ToolBarButtonClickEventArgs e)
		{
			const string METHOD_NAME = THIS + ".tbarReportData_ButtonClick()";
			try
			{
				if (e.Button.Equals(this.btnF2))
				{
					this.ControlKeyDown(this, new KeyEventArgs(Keys.F2));
				}
				if (e.Button.Equals(this.btnF3))
				{
					this.ControlKeyDown(this, new KeyEventArgs(Keys.F3));
				}
				if (e.Button.Equals(this.btnF4))
				{
					this.ControlKeyDown(this, new KeyEventArgs(Keys.F4));
				}
				if (e.Button.Equals(this.btnF5))
				{
					this.ControlKeyDown(this, new KeyEventArgs(Keys.F5));
				}
				if (e.Button.Equals(this.btnF6))
				{
					this.ControlKeyDown(this, new KeyEventArgs(Keys.F6));
				}
				if (e.Button.Equals(this.btnF7))
				{
					this.ControlKeyDown(this, new KeyEventArgs(Keys.F7));
				}
				if (e.Button.Equals(this.btnF8))
				{
					this.ControlKeyDown(this, new KeyEventArgs(Keys.F8));
				}
				if (e.Button.Equals(this.btnF9))
				{
					this.ControlKeyDown(this, new KeyEventArgs(Keys.F9));
				}
				if (e.Button.Equals(this.btnF10))
				{
					this.ControlKeyDown(this, new KeyEventArgs(Keys.F10));
				}
				if (e.Button.Equals(this.btnF11))
				{
					this.ControlKeyDown(this, new KeyEventArgs(Keys.F11));
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

		private void ViewReport_Closing(object sender, CancelEventArgs e)
		{
			try
			{
				this.rptReportData.Dispose();
				this.gridReportData.Dispose();
				this.ReportViewer.Dispose();
			}
			catch{}
		}

		/// <summary>
		/// Open search form for KeyField, FilterField1, FilterField2
		/// this functtion depend on the objControl_Click() function (define below)
		/// </summary>
		/// <author>Thachnn</author>
		/// <date></date>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void objControl_KeyDown(object sender, KeyEventArgs e)
		{
			const string METHOD_NAME = THIS + ".objControl_KeyDown()";
			try
			{				
				if (e.KeyCode == Keys.F4)
				{
					Control objControl = (Control) sender;
					if (objControl.Tag == null)
						return;

					TextBox txt = (TextBox) sender;
					sys_ReportParaVO voParam = (sys_ReportParaVO) txt.Tag;
					string strControlName = string.Empty;
					if (txt.Name.IndexOf(voParam.FilterField1) >= 0)
						strControlName = BUTTON_FILTER1_PREFIX + voParam.ParaName + voParam.FilterField1;
					else if (txt.Name.IndexOf(voParam.FilterField2) >= 0)
						strControlName = BUTTON_FILTER2_PREFIX + voParam.ParaName + voParam.FilterField2;
					Button btn = (Button) GetControlFromPanel(this.pnlParameters, strControlName);
					objControl_Click(btn, null);
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
		/// When user try to input data for param which get value from a configured table
		/// we need to open the search form based on
		/// FromTable, KeyField, FilterField1 and FilterField2
		/// 
		/// /// if you modify the objControl_Click() function, you must modify the section mark by @@@MODIFY_WITH_TOO    in this function
		/// </summary>
		/// <author>Thachnn</author>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void objControl_Validating(object sender, CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".objControl_Validating()";
			try
			{
				Control objControl = (Control) sender;
				if (objControl.Tag == null)
					return;

				TextBox txt = (TextBox) sender;
				if (!txt.Modified)
					return;
				else
				{
					if (txt.Text.Trim() == string.Empty)
					{
						sys_ReportParaVO voParameter = null;
						try
						{
							voParameter = (sys_ReportParaVO) txt.Tag;
							// clear data of parameter first
							ClearDataOfParameter(voParameter);
						}
						catch{}
						return;
					}
				}

				sys_ReportParaVO voParam = (sys_ReportParaVO) txt.Tag;
				
				#region CLONE OF objControl_Click, but call openSearchFOrm with false

				TextBox txtKeyField = new TextBox();
				TextBox txtFilterField1 = new TextBox();
				TextBox txtFilterField2 = new TextBox();

				try
				{
					txtKeyField = (TextBox) GetControlFromPanel(this.pnlParameters, TEXTBOX_KEYFIELD_PREFIX + voParam.ParaName + voParam.FromField);
				}
				catch{}
				try
				{
					txtFilterField1 = (TextBox) GetControlFromPanel(this.pnlParameters, TEXTBOX_FILTER1_PREFIX + voParam.ParaName + voParam.FilterField1);
				}
				catch{}
				try
				{
					txtFilterField2 = (TextBox) GetControlFromPanel(this.pnlParameters, TEXTBOX_FILTER2_PREFIX + voParam.ParaName + voParam.FilterField2);
				}
				catch{}

				DataRowView drv = null;
				DataTable dtbResult = null;
				// build the where clause for search form
				string strWhereClause = BuildWhere(voParam);
				// filter field
				string strFilterField = string.Empty;
				string strFilterValue = txt.Text;
				if (txt.Name.IndexOf(TEXTBOX_FILTER1_PREFIX) >= 0)
					strFilterField = voParam.FilterField1;
				else if (txt.Name.IndexOf(TEXTBOX_FILTER2_PREFIX) >= 0)
					strFilterField = voParam.FilterField2;
				if (voParam.MultiSelection)
					dtbResult = FormControlComponents.OpenSearchFormForMultiSelectedRow(voParam.FromTable, strFilterField, strFilterValue, strWhereClause, false);
				else
					drv = FormControlComponents.OpenSearchFormWithWhere(voParam.FromTable, strFilterField, strFilterValue, strWhereClause, false);
				
				if (drv != null)
				{
					try
					{
						txtKeyField.Text = drv[voParam.FromField].ToString();
					}
					catch{}
					try
					{
						txtFilterField1.Text = drv[voParam.FilterField1].ToString();
					}
					catch{}
					try
					{
						txtFilterField2.Text = drv[voParam.FilterField2].ToString();
					}
					catch{}
					try
					{
						// now try to find the reference parameter in order to clear the data of its
						ArrayList arrReference = FindReferenceParameter(voParam.ParaName);
						foreach (sys_ReportParaVO voReference in arrReference)
							ClearDataOfParameter(voReference);
					}
					catch{}
				}
				else if (dtbResult != null && dtbResult.Rows.Count > 0)
				{
					StringBuilder sbFromField = new StringBuilder(dtbResult.Rows.Count);
					StringBuilder sbFilter1 = new StringBuilder(dtbResult.Rows.Count);
					StringBuilder sbFilter2 = new StringBuilder(dtbResult.Rows.Count);
					foreach (DataRow drowData in dtbResult.Rows)
					{
						sbFromField.Append(drowData[voParam.FromField].ToString()).Append(",");
						sbFilter1.Append(drowData[voParam.FilterField1].ToString()).Append(",");
						if (voParam.FilterField2.Trim() != string.Empty)
							sbFilter2.Append(drowData[voParam.FilterField2].ToString()).Append(",");
					}
					try
					{
						txtKeyField.Text = sbFromField.ToString(0, sbFromField.Length - 1);
					}
					catch{}
					try
					{
						txtFilterField1.Text = sbFilter1.ToString(0, sbFilter1.Length - 1);
					}
					catch{}
					try
					{
						if (voParam.FilterField2.Trim() != string.Empty)
							txtFilterField2.Text = sbFilter2.ToString(0, sbFilter2.Length - 1);
					}
					catch{}
					try
					{
						// now try to find the reference parameter in order to clear the data of its
						ArrayList arrReference = FindReferenceParameter(voParam.ParaName);
						foreach (sys_ReportParaVO voReference in arrReference)
							ClearDataOfParameter(voReference);
					}
					catch{}
				}
				else
					e.Cancel = true;
				
				#endregion
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
		/// When user try to input data for param which get value from a configured table
		/// we need to open the search form based on
		/// FromTable, KeyField, FilterField1 and FilterField2
		/// 
		/// if you modify this function, you must modify the section mark by @@@MODIFY_WITH_TOO    in the objControl_Validating() in the upper
		/// </summary>
		/// <author>Thachnn</author>
		/// <date>Thachnn: 22/12/2005 </date>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void objControl_Click(object sender, EventArgs e)
		{
			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.WaitCursor;

			#endregion Code Inserted Automatically

			
			const string METHOD_NAME = THIS + ".objControl_Click()";

			try
			{
				if (((Control) sender).Tag == null)
{
					// Code Inserted Automatically
#region Code Inserted Automatically
this.Cursor = Cursors.Default;
#endregion Code Inserted Automatically

					return;
}
				
				Button btnSender = (Button)sender;
				TextBox txtKeyField = new TextBox();
				TextBox txtFilterField1 = new TextBox();
				TextBox txtFilterField2 = new TextBox();

				Button btn = (Button) sender;
				sys_ReportParaVO voParam = (sys_ReportParaVO) btn.Tag;
				try
				{
					txtKeyField = (TextBox) GetControlFromPanel(this.pnlParameters, TEXTBOX_KEYFIELD_PREFIX + voParam.ParaName + voParam.FromField);
				}
				catch{}
				try
				{
					txtFilterField1 = (TextBox) GetControlFromPanel(this.pnlParameters, TEXTBOX_FILTER1_PREFIX + voParam.ParaName + voParam.FilterField1);
				}
				catch{}
				try
				{
					txtFilterField2 = (TextBox) GetControlFromPanel(this.pnlParameters, TEXTBOX_FILTER2_PREFIX + voParam.ParaName + voParam.FilterField2);
				}
				catch{}

				DataRowView drv = null;
				DataTable dtbResult = null;
				// build the where clause for search form
				string strWhereClause = BuildWhere(voParam);
				// filter field
				string strFilterField = string.Empty;
				string strFilterValue = string.Empty;
				if (btnSender.Name.IndexOf(BUTTON_FILTER1_PREFIX) >= 0)
				{
					strFilterField = voParam.FilterField1;
					strFilterValue = txtFilterField1.Text;
				}
				else if (btnSender.Name.IndexOf(BUTTON_FILTER2_PREFIX) >= 0)
				{
					strFilterField = voParam.FilterField2;
					strFilterValue = txtFilterField2.Text;
				}
				if (voParam.MultiSelection)
					dtbResult = FormControlComponents.OpenSearchFormForMultiSelectedRow(voParam.FromTable, strFilterField, strFilterValue, strWhereClause, true);
				else
					drv = FormControlComponents.OpenSearchFormWithWhere(voParam.FromTable, strFilterField, strFilterValue, strWhereClause, true);

				if (drv != null)
				{
					try
					{
						txtKeyField.Text = drv[voParam.FromField].ToString();
					}
					catch{}
					try
					{
						txtFilterField1.Text = drv[voParam.FilterField1].ToString();
					}
					catch{}
					try
					{
						txtFilterField2.Text = drv[voParam.FilterField2].ToString();
					}
					catch{}
					try
					{
						// now try to find the reference parameter in order to clear the data of its
						ArrayList arrReference = FindReferenceParameter(voParam.ParaName);
						foreach (sys_ReportParaVO voReference in arrReference)
							ClearDataOfParameter(voReference);
					}
					catch{}
				}
				else if (dtbResult != null && dtbResult.Rows.Count > 0)
				{
					StringBuilder sbFromField = new StringBuilder(dtbResult.Rows.Count);
					StringBuilder sbFilter1 = new StringBuilder(dtbResult.Rows.Count);
					StringBuilder sbFilter2 = new StringBuilder(dtbResult.Rows.Count);
					foreach (DataRow drowData in dtbResult.Rows)
					{
						sbFromField.Append(drowData[voParam.FromField].ToString()).Append(",");
						sbFilter1.Append(drowData[voParam.FilterField1].ToString()).Append(",");
						if (voParam.FilterField2.Trim() != string.Empty)
							sbFilter2.Append(drowData[voParam.FilterField2].ToString()).Append(",");
					}
					try
					{
						txtKeyField.Text = sbFromField.ToString(0, sbFromField.Length - 1);
					}
					catch{}
					try
					{
						txtFilterField1.Text = sbFilter1.ToString(0, sbFilter1.Length - 1);
					}
					catch{}
					try
					{
						if (voParam.FilterField2.Trim() != string.Empty)
							txtFilterField2.Text = sbFilter2.ToString(0, sbFilter2.Length - 1);
					}
					catch{}
					try
					{
						// now try to find the reference parameter in order to clear the data of its
						ArrayList arrReference = FindReferenceParameter(voParam.ParaName);
						foreach (sys_ReportParaVO voReference in arrReference)
							ClearDataOfParameter(voReference);
					}
					catch{}
				}
			}
			#region CATCH				

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
			catch (ArgumentException ex)
			{
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

			#endregion

			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.Default;

			#endregion Code Inserted Automatically

			
		}

		#endregion

		#region private methods

		private void LoadParameter(ArrayList parrParams, Panel ppnlContainer)
		{
			const string METHOD_NAME = THIS + ".LoadParameter()";
			const int DISTANCE_BETWEEN_CONTROL = 2;
			const float CHARACTER_WIDTH_RATIO = 6f;
			const int LABEL_WIDTH_ADJUSTMENT = 12;

			try
			{
				ppnlContainer.Controls.Clear();

				// getting the max character length of parameter caption, store in nMaxCharacterLength
				int nMaxCharacterLength = 0;
				foreach (sys_ReportParaVO voParam in arrParams)
				{
					nMaxCharacterLength = voParam.ParaCaption.Length > nMaxCharacterLength ? voParam.ParaCaption.Length : nMaxCharacterLength;
				}


				// Order of row
				int nYOrder = 1;
				// add to TabPage control
				int intTabIndex = 0;
				if (parrParams.Count > 0)
				{
					// used to store the last added control
					// in order to determine where to place next control
					Control objPreviousControl = new Control();
					Label objPreviousLabel = new Label();
					//objPreviousLabel.Location = new Point(X_LOCATION, Y_LOCATION);
					objPreviousControl.Location = new Point(X_LOCATION, Y_LOCATION);
					for (int i = 0; i < parrParams.Count; i++)
					{
						sys_ReportParaVO voParam = (sys_ReportParaVO) parrParams[i];

						#region Add Label

						Label lblParamName = new Label();
						// set name for parameter lbl + ParaName
						lblParamName.Name = LABEL_PREFIX + voParam.ParaName.Trim();
						// set text as para caption
						lblParamName.Text = voParam.ParaCaption.Trim();

						/// HACKED: Thachnn 13-Oct-2005: fix bug 2135
						lblParamName.Size = new Size((int) (nMaxCharacterLength*CHARACTER_WIDTH_RATIO+LABEL_WIDTH_ADJUSTMENT), DEFAULT_HEIGHT);
						/// ENDHACKED: Thachnn 13-Oct-2005: fix bug 2135

						if (!voParam.Optional)
							lblParamName.ForeColor = Color.Maroon;
						lblParamName.TextAlign = ContentAlignment.MiddleLeft;
						//lblParamName.BorderStyle = BorderStyle.FixedSingle;

						if (voParam.SameRow)
						{
							lblParamName.Left = objPreviousControl.Left + objPreviousControl.Width + DISTANCE_BETWEEN_CONTROL;
							lblParamName.Top = objPreviousLabel.Top;
						}
						else
						{
							lblParamName.Left = X_LOCATION;
							if (objPreviousLabel.Top >= Y_LOCATION)
								lblParamName.Top = objPreviousLabel.Top + objPreviousLabel.Height + DISTANCE_BETWEEN_CONTROL;
							else
								lblParamName.Top = Y_LOCATION;
							//lblParamName.Top = nYOrder*Y_LOCATION;
							nYOrder++;
						}

						// add control to GroupBox
						lblParamName.TabIndex = intTabIndex;
						ppnlContainer.Controls.Add(lblParamName);
						objPreviousLabel = lblParamName;
						intTabIndex++;

						#endregion

						#region Add TextBox, ComboBox, ListBox

						Control objControl = new Control();

						if ((voParam.DataType != (int) Type.GetTypeCode(typeof (bool))) &&
							(voParam.DataType != (int) Type.GetTypeCode(typeof (DateTime))))
						{
							// TextBox that not get data from FromTable, FromField
							if ((voParam.Items.Trim() == string.Empty) &&
								(voParam.FromTable.Trim() == string.Empty) &&
								(voParam.FromField.Trim() == string.Empty) &&
								(voParam.SQLCLause.Trim() == string.Empty))
							{
								objControl = new TextBox();
								objControl.Text = voParam.DefaultValue.Trim();
								objControl.Name = TEXTBOX_PREFIX + voParam.ParaName.Trim();
							}
								// TextBox get data from FromTable, FromField
							else if (voParam.FromTable.Trim() != string.Empty)
							{
								// if user defined KeyField and Filter Field, 
								// add new TextBox for key field

								#region Key Field

								objControl = new TextBox();
								objControl.Name = TEXTBOX_KEYFIELD_PREFIX + voParam.ParaName + voParam.FromField;
								objControl.Tag = voParam;

								objControl.Size = new Size(voParam.Width, DEFAULT_HEIGHT);
								objControl.TabIndex = intTabIndex;

								objControl.Location = new Point(lblParamName.Left + lblParamName.Width + DISTANCE_BETWEEN_CONTROL,
								                                lblParamName.Location.Y);
								// invisible key field
								objControl.Visible = false;
								// assign keydown event for key field in order to open search form
								objControl.KeyDown += new KeyEventHandler(objControl_KeyDown);
								// assign leave event for key field in order to open
								objControl.Validating += new CancelEventHandler(objControl_Validating);

								// add control to GroupBox
								ppnlContainer.Controls.Add(objControl);
								intTabIndex ++;

								// re-assign previous control with newly control
								//objPreviousControl = objControl;

								#endregion

								#region OpenSearchForm Button

								// no need button for key field because we not show it
								objControl = new Button();
								objControl.Name = BUTTON_KEYFIELD_PREFIX + voParam.ParaName + voParam.FromField;
								objControl.Tag = voParam;

								objControl.Size = new Size(OPENSEARCHFORM_BUTTON_MAX_WIDTH, DEFAULT_HEIGHT);
								objControl.TabIndex = intTabIndex;
								objControl.Text = OPENSEARCHFORM_BUTTON_TEXT;

								objControl.Location = new Point(objPreviousControl.Left + objPreviousControl.Width + DISTANCE_BETWEEN_CONTROL,
								                                lblParamName.Location.Y);
								// invisible key field
								objControl.Visible = false;

								// assign keydown event for key field in order to open search form
								objControl.Click += new EventHandler(objControl_Click);

								// add control to GroupBox
								ppnlContainer.Controls.Add(objControl);
								intTabIndex ++;

								// re-assign previous control with newly control
								//objPreviousControl = objControl;

								#endregion
								
								// filter field
								if ((voParam.FilterField1.Trim() != string.Empty))
								{
									#region Filter 1

									// add new TextBox for Filter field 1
									objControl = new TextBox();
									objControl.Name = TEXTBOX_FILTER1_PREFIX + voParam.ParaName + voParam.FilterField1;
									objControl.Tag = voParam;

									objControl.Size = new Size(voParam.FilterField1Width, DEFAULT_HEIGHT);
									objControl.TabIndex = intTabIndex;

									// Filter field control will be add in same line with Key Field control
									objControl.Location = new Point(lblParamName.Left + lblParamName.Width + DISTANCE_BETWEEN_CONTROL,
									                                lblParamName.Location.Y);
//									objControl.Location = new Point(objPreviousControl.Location.X + objPreviousControl.Size.Width + DISTANCE_BETWEEN_CONTROL,
//										objPreviousControl.Location.Y);
									// assign keydown event for key field in order to open search form
									objControl.KeyDown += new KeyEventHandler(objControl_KeyDown);
									// assign leave event for key field in order to open
									objControl.Validating += new CancelEventHandler(objControl_Validating);


									// add control to GroupBox
									ppnlContainer.Controls.Add(objControl);
									intTabIndex ++;

									// re-assign previous control with newly control
									objPreviousControl = objControl;

									#endregion

									#region OpenSearchForm Button

									objControl = new Button();
									objControl.Name = BUTTON_FILTER1_PREFIX + voParam.ParaName + voParam.FilterField1;
									objControl.Tag = voParam;

									objControl.Size = new Size(OPENSEARCHFORM_BUTTON_MAX_WIDTH, DEFAULT_HEIGHT);
									objControl.TabIndex = intTabIndex;
									objControl.Text = OPENSEARCHFORM_BUTTON_TEXT;

									objControl.Location = new Point(objPreviousControl.Left + objPreviousControl.Width + DISTANCE_BETWEEN_CONTROL,
									                                lblParamName.Location.Y);
									// assign keydown event for key field in order to open search form
									objControl.Click += new EventHandler(objControl_Click);

									// add control to GroupBox
									ppnlContainer.Controls.Add(objControl);
									intTabIndex ++;

									// re-assign previous control with newly control
									objPreviousControl = objControl;

									#endregion

									// if have filter field 2 then objControl will be FilterField2
									if ((voParam.FilterField2.Trim() != string.Empty))
									{
										#region Filter 2

										// add new TextBox for Filter field 2
										objControl = new TextBox();
										objControl.Name = TEXTBOX_FILTER2_PREFIX + voParam.ParaName + voParam.FilterField2;
										objControl.Tag = voParam;

										objControl.Size = new Size(voParam.FilterField2Width, DEFAULT_HEIGHT);
										objControl.TabIndex = intTabIndex;

										// Filter field control will be add in same line with Filter Field 1 control
										objControl.Location = new Point(objPreviousControl.Location.X + objPreviousControl.Size.Width + DISTANCE_BETWEEN_CONTROL,
										                                objPreviousControl.Location.Y);
										// assign keydown event for key field in order to open search form
										objControl.KeyDown += new KeyEventHandler(objControl_KeyDown);
										// assign leave event for key field in order to open
										objControl.Validating += new CancelEventHandler(objControl_Validating);

										// add control to GroupBox
										ppnlContainer.Controls.Add(objControl);
										intTabIndex ++;
										// re-assign previous control with newly control
										objPreviousControl = objControl;

										#endregion

										#region OpenSearchForm Button

										objControl = new Button();
										objControl.Name = BUTTON_FILTER2_PREFIX + voParam.ParaName + voParam.FilterField2;
										objControl.Tag = voParam;

										objControl.Size = new Size(OPENSEARCHFORM_BUTTON_MAX_WIDTH, DEFAULT_HEIGHT);
										objControl.TabIndex = intTabIndex;
										objControl.Text = OPENSEARCHFORM_BUTTON_TEXT;

										objControl.Location = new Point(objPreviousControl.Left + objPreviousControl.Width + DISTANCE_BETWEEN_CONTROL,
										                                lblParamName.Location.Y);
										// assign keydown event for key field in order to open search form
										objControl.Click += new EventHandler(objControl_Click);

										// add control to GroupBox
										ppnlContainer.Controls.Add(objControl);
										intTabIndex ++;

										// re-assign previous control with newly control
										objPreviousControl = objControl;

										#endregion

										// ignore the general information, go to next control
										continue;
									}
									else // ignore the general information, go to next control
										continue;
								}
								else // ignore the general information, go to next control								
									continue;
							}
							else // ComboBox
							{
								objControl = new ComboBox();
								objControl.Name = COMBO_PREFIX + voParam.ParaName.Trim();
								((ComboBox) objControl).DropDownStyle = ComboBoxStyle.DropDownList;
								// get the item for Combo from Items field
								if (voParam.Items.Trim() != string.Empty)
								{
									string[] strItems = voParam.Items.Split(Constants.VIEW_TABLE_ITEM_SEPARATOR);
									foreach (string strItem in strItems)
									{
										((ComboBox) objControl).Items.Add(strItem);
									}
									((ComboBox) objControl).SelectedItem = voParam.DefaultValue;
								}
								objControl.Tag = voParam;
							}
						}
							#endregion

							#region CheckBox

						else if (voParam.DataType == (int) Type.GetTypeCode(typeof (bool)))
						{
							objControl = new CheckBox();
							((CheckBox) objControl).ThreeState = true;
							objControl.Name = CHECKBOX_PREFIX + voParam.ParaName.Trim();
							if (voParam.DefaultValue != string.Empty)
							{
								try
								{
									((CheckBox) objControl).Checked = Convert.ToBoolean(voParam.DefaultValue.Trim());
								}
								catch
								{
									((CheckBox) objControl).CheckState = CheckState.Indeterminate;
								}
							}
							else
							{
								((CheckBox) objControl).CheckState = CheckState.Indeterminate;
							}
						}
							#endregion

							#region C1DateEdit

						else if (voParam.DataType == (int) Type.GetTypeCode(typeof (DateTime)))
						{
							objControl = new C1DateEdit();
							objControl.Name = DATEPICKER_PREFIX + voParam.ParaName.Trim();
							try
							{
								((C1DateEdit) objControl).Value = DateTime.Parse(voParam.DefaultValue.Trim());
							}
							catch
							{
								((C1DateEdit) objControl).Value = DBNull.Value;
							}
							((C1DateEdit) objControl).FormatType = FormatTypeEnum.CustomFormat;
							((C1DateEdit) objControl).CustomFormat = Constants.DATETIME_FORMAT_HOUR;
							((C1DateEdit) objControl).TextAlign = HorizontalAlignment.Center;
							((C1DateEdit) objControl).EmptyAsNull = true;
							((C1DateEdit) objControl).VisibleButtons = DropDownControlButtonFlags.DropDown;
						}

						#endregion

						#region General information

						objControl.Tag = voParam;
						objControl.Size = new Size(voParam.Width, DEFAULT_HEIGHT);
						objControl.TabIndex = intTabIndex;

						objControl.Location = new Point(lblParamName.Left + lblParamName.Width + DISTANCE_BETWEEN_CONTROL,
						                                lblParamName.Location.Y);

						// add control to GroupBox						
						ppnlContainer.Controls.Add(objControl);
						intTabIndex ++;

						objPreviousControl = objControl;
						objPreviousLabel = lblParamName;

						#endregion
					}
				}
			}
			catch (Exception ex)
			{
				// log message.
				try
				{
					Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
				}
				catch
				{
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				throw ex;
			}
		}

		private string BuildSqlCommand(string pstrOldCommand, out ArrayList oarrHistoryParaList)
		{
			try
			{
				///HACKED: Thachnn: replace \t \n \r to <space character> to avoid error while execute SQL command
				pstrOldCommand = StardandizeSQL(pstrOldCommand);
				string strNewCommand = pstrOldCommand;

				oarrHistoryParaList = new ArrayList();
				foreach (Control objControl in this.pnlParameters.Controls)
				{
					sys_ReportHistoryParaVO voReportHistoryPara = null;
					sys_ReportParaVO voTagPara = null;

					// if current control is TextBox/ComboBox/CheckBox/DateTimePicker then get the tag object
					if (objControl.GetType().Equals(typeof (TextBox)) ||
						objControl.GetType().Equals(typeof (ComboBox)) ||
						objControl.GetType().Equals(typeof (CheckBox)) ||
						objControl.GetType().Equals(typeof (C1DateEdit)))
					{
						voTagPara = (sys_ReportParaVO) (objControl.Tag);
						// if scanning thru filter field1 and 2, go to next control
						if (objControl.Name.Equals(TEXTBOX_FILTER1_PREFIX + voTagPara.ParaName + voTagPara.FilterField1) ||
							objControl.Name.Equals(TEXTBOX_FILTER2_PREFIX + voTagPara.ParaName + voTagPara.FilterField2))
							continue;
					}
					else // go to next control
						continue;
					voReportHistoryPara = new sys_ReportHistoryParaVO();
					voReportHistoryPara.ParaName = voTagPara.ParaName;

					/// C1DateEdit inherrit from TextBox, 
					/// so we need to check objControl against C1DateEdit before TextBox
					/// TODO: Bro.DungLA: I think this point we need to review
					/// ((C1DateEdit)objControl).Value.ToString()  may return bad date time format string
					/// depend on the current cultural info
					if (objControl is C1DateEdit) // C1DateEdit
					{
						if (voTagPara.DataType.Equals((int) TypeCode.DateTime))
						{
							if (!((C1DateEdit) objControl).ValueIsDbNull)
							{
								strNewCommand = FormControlComponents.FindAndReplaceParameterRegEx(strNewCommand, voTagPara.ParaName,"'"
									+ ((DateTime)((C1DateEdit) objControl).Value).ToString(SQL_DATE_TIME_FORMAT) + "'", voTagPara.MultiSelection);
							}
							else
								strNewCommand = FormControlComponents.FindAndReplaceParameterRegEx(strNewCommand, voTagPara.ParaName, string.Empty, voTagPara.MultiSelection);
						}
						// if TagValue = true then parameter value get from .Tag property
						if (voTagPara.TagValue)
						{
							voReportHistoryPara.TagValue = ((DateTime)((C1DateEdit) objControl).Value).ToString(SQL_DATE_TIME_FORMAT);
						}
						else // else TagValue = false then parameter value get from .Text property
						{
							if (((C1DateEdit) objControl).Value != DBNull.Value)
							{
								voReportHistoryPara.ParaValue = ((DateTime)((C1DateEdit) objControl).Value).ToString(SQL_DATE_TIME_FORMAT);
							}
							else
								voReportHistoryPara.ParaValue = string.Empty;
						}
					}
						// Textbox
					else if (objControl is TextBox)
					{
						// TextBox is not the FilterField1 or FilterField2
						//if (objControl.Name.Remove(0, TEXTBOX_PREFIX.Length + voTagPara.ParaName.Length) == string.Empty)
						if (objControl.Name.Equals(TEXTBOX_PREFIX + voTagPara.ParaName))
						{
							if ((voTagPara.DataType.Equals((int) TypeCode.Decimal)) ||
								(voTagPara.DataType.Equals((int) TypeCode.Double)) ||
								(voTagPara.DataType.Equals((int) TypeCode.Int16)) ||
								(voTagPara.DataType.Equals((int) TypeCode.Int32)) ||
								(voTagPara.DataType.Equals((int) TypeCode.Int64)))
							{
								// if TagValue = true then parameter value get from .Tag property
								if (voTagPara.TagValue)
								{
									strNewCommand = FormControlComponents.FindAndReplaceParameterRegEx(strNewCommand, voTagPara.ParaName, voTagPara.DefaultValue, voTagPara.MultiSelection);
									//strNewCommand = strNewCommand.Replace(voTagPara.ParaName, voTagPara.DefaultValue);
									voReportHistoryPara.TagValue = voTagPara.DefaultValue;
								}
								else // else TagValue = false then parameter value get from .Text property
								{
									strNewCommand = FormControlComponents.FindAndReplaceParameterRegEx(strNewCommand, voTagPara.ParaName, objControl.Text, voTagPara.MultiSelection);
									//strNewCommand = strNewCommand.Replace(voTagPara.ParaName, objControl.Text);
									voReportHistoryPara.ParaValue = objControl.Text;
								}
							}
							else if ((voTagPara.DataType.Equals((int) TypeCode.Char)) || (voTagPara.DataType.Equals((int) TypeCode.String)))
							{
								// if TagValue = true then parameter value get from .Tag property
								if (voTagPara.TagValue)
								{
									if (voTagPara.DefaultValue != string.Empty)
										strNewCommand = FormControlComponents.FindAndReplaceParameterRegEx(strNewCommand, voTagPara.ParaName, "'" + voTagPara.DefaultValue + "'", voTagPara.MultiSelection);
									else
										strNewCommand = FormControlComponents.FindAndReplaceParameterRegEx(strNewCommand, voTagPara.ParaName, string.Empty, voTagPara.MultiSelection);
									//strNewCommand = strNewCommand.Replace(voTagPara.ParaName, "'" + voTagPara.DefaultValue + "'");
									voReportHistoryPara.TagValue = voTagPara.DefaultValue;
								}
								else // else TagValue = false then parameter value get from .Text property
								{
									if (objControl.Text != string.Empty)
										strNewCommand = FormControlComponents.FindAndReplaceParameterRegEx(strNewCommand, voTagPara.ParaName, "'" + objControl.Text + "'", voTagPara.MultiSelection);
									else
										strNewCommand = FormControlComponents.FindAndReplaceParameterRegEx(strNewCommand, voTagPara.ParaName, string.Empty, voTagPara.MultiSelection);
									//strNewCommand = strNewCommand.Replace(voTagPara.ParaName, "'" + objControl.Text + "'");
									voReportHistoryPara.ParaValue = objControl.Text;
								}
							}
						}
						else if (objControl.Name.Equals(TEXTBOX_KEYFIELD_PREFIX + voTagPara.ParaName + voTagPara.FromField)) // TextBox is FromField
						{
							if ((voTagPara.DataType.Equals((int) TypeCode.Decimal)) ||
								(voTagPara.DataType.Equals((int) TypeCode.Double)) ||
								(voTagPara.DataType.Equals((int) TypeCode.Int16)) ||
								(voTagPara.DataType.Equals((int) TypeCode.Int32)) ||
								(voTagPara.DataType.Equals((int) TypeCode.Int64)))
							{
								// if TagValue = true then parameter value get from .Tag property
								if (voTagPara.TagValue)
								{
									strNewCommand = FormControlComponents.FindAndReplaceParameterRegEx(strNewCommand, voTagPara.ParaName, voTagPara.DefaultValue, voTagPara.MultiSelection);
									//strNewCommand = strNewCommand.Replace(voTagPara.ParaName, voTagPara.DefaultValue);
									voReportHistoryPara.TagValue = voTagPara.DefaultValue;
								}
								else // else TagValue = false then parameter value get from .Text property
								{
									strNewCommand = FormControlComponents.FindAndReplaceParameterRegEx(strNewCommand, voTagPara.ParaName, objControl.Text, voTagPara.MultiSelection);
									//strNewCommand = strNewCommand.Replace(voTagPara.ParaName, objControl.Text);
									voReportHistoryPara.ParaValue = objControl.Text;
								}
							}
							else if ((voTagPara.DataType.Equals((int) TypeCode.Char)) || (voTagPara.DataType.Equals((int) TypeCode.String)))
							{
								// if TagValue = true then parameter value get from .Tag property
								if (voTagPara.TagValue)
								{
									if (voTagPara.DefaultValue != string.Empty)
										strNewCommand = FormControlComponents.FindAndReplaceParameterRegEx(strNewCommand, voTagPara.ParaName, "'" + voTagPara.DefaultValue + "'", voTagPara.MultiSelection);
									else
										strNewCommand = FormControlComponents.FindAndReplaceParameterRegEx(strNewCommand, voTagPara.ParaName, string.Empty, voTagPara.MultiSelection);
									voReportHistoryPara.TagValue = voTagPara.DefaultValue;
								}
								else // else TagValue = false then parameter value get from .Text property
								{
									if (objControl.Text != string.Empty)
									{
										string strValue = objControl.Text;
										string strTemp = string.Empty;
										if (strValue.Trim().Length > 0)
										{
											if (strValue.IndexOf(",") >= 0)
											{
												string[] strValues = strValue.Split(",".ToCharArray());
												foreach (string strVal in strValues)
													strTemp = strTemp + "'" + strVal + "',";
											}
											else
												strTemp = "'" + strValue + "'";
										}
										// remove the last ","
										if (strTemp.Trim() != string.Empty && strTemp.IndexOf(",") >= 0)
											strTemp = strTemp.Substring(0, strTemp.Length - 1);
										strNewCommand = FormControlComponents.FindAndReplaceParameterRegEx(strNewCommand, voTagPara.ParaName, strTemp, voTagPara.MultiSelection);
									}
									else
										strNewCommand = FormControlComponents.FindAndReplaceParameterRegEx(strNewCommand, voTagPara.ParaName, string.Empty, voTagPara.MultiSelection);
									voReportHistoryPara.ParaValue = objControl.Text;
								}
							}

							try
							{
								// FilterField1
								Control objFilterField1 = GetControlFromPanel(pnlParameters, TEXTBOX_FILTER1_PREFIX + voTagPara.ParaName + voTagPara.FilterField1);
								voReportHistoryPara.FilterField1Value = objFilterField1.Text;
							}
							catch
							{
							}
							try
							{
								// FilterField2
								Control objFilterField2 = GetControlFromPanel(pnlParameters, TEXTBOX_FILTER2_PREFIX + voTagPara.ParaName + voTagPara.FilterField2);
								voReportHistoryPara.FilterField2Value = objFilterField2.Text;
							}
							catch
							{
							}
						}
					}
					else if (objControl is ComboBox) // ComboBox
					{
						if ((voTagPara.DataType.Equals((int) TypeCode.Decimal)) ||
							(voTagPara.DataType.Equals((int) TypeCode.Double)) ||
							(voTagPara.DataType.Equals((int) TypeCode.Int16)) ||
							(voTagPara.DataType.Equals((int) TypeCode.Int32)) ||
							(voTagPara.DataType.Equals((int) TypeCode.Int64)))
						{
							// if TagValue = true then parameter value get from .Tag property
							if (voTagPara.TagValue)
							{
								voReportHistoryPara.TagValue = ((ComboBox) objControl).SelectedItem.ToString();
								//strNewCommand = strNewCommand.Replace(voTagPara.ParaName, ((ComboBox)objControl).SelectedIndex.ToString());
							}
							else // else TagValue = false then parameter value get from .Text property
							{
								voReportHistoryPara.ParaValue = ((ComboBox) objControl).SelectedItem.ToString();
							}
							strNewCommand = FormControlComponents.FindAndReplaceParameterRegEx(strNewCommand, voTagPara.ParaName, ((ComboBox) objControl).SelectedItem.ToString(), voTagPara.MultiSelection);
						}
						else
						{
							// if TagValue = true then parameter value get from .Tag property
							if (voTagPara.TagValue)
							{
								voReportHistoryPara.TagValue = ((ComboBox) objControl).SelectedItem.ToString();
							}
							else // else TagValue = false then parameter value get from .Text property
							{
								voReportHistoryPara.ParaValue = ((ComboBox) objControl).SelectedItem.ToString();
							}
							if (((ComboBox) objControl).SelectedText != string.Empty)
								strNewCommand = FormControlComponents.FindAndReplaceParameterRegEx(strNewCommand, voTagPara.ParaName, "'" + ((ComboBox) objControl).SelectedItem.ToString() + "'", voTagPara.MultiSelection);
							else
								strNewCommand = FormControlComponents.FindAndReplaceParameterRegEx(strNewCommand, voTagPara.ParaName, string.Empty, voTagPara.MultiSelection);
						}
					}
					else if (objControl is CheckBox) // Checkbox
					{
						if (voTagPara.DataType.Equals((int) TypeCode.Boolean))
						{
							if (((CheckBox) objControl).CheckState != CheckState.Indeterminate)
								strNewCommand = FormControlComponents.FindAndReplaceParameterRegEx(strNewCommand, voTagPara.ParaName,
								                                                                   Convert.ToInt32(((CheckBox) objControl).Checked).ToString(), voTagPara.MultiSelection);
							else
								strNewCommand = FormControlComponents.FindAndReplaceParameterRegEx(strNewCommand, voTagPara.ParaName, string.Empty, voTagPara.MultiSelection);
						}
						// if TagValue = true then parameter value get from .Tag property
						if (voTagPara.TagValue)
							voReportHistoryPara.TagValue = voTagPara.DefaultValue;
						else // else TagValue = false then parameter value get from .Text property
						{
							if (((CheckBox) objControl).CheckState != CheckState.Indeterminate)
							{
								voReportHistoryPara.ParaValue = Convert.ToInt32(((CheckBox) objControl).Checked).ToString();
							}
							else
								voReportHistoryPara.ParaValue = string.Empty;
						}
					}

					else // ingore other control (not Label, TextBox, ComboBox, CheckBox, C1DateEdit), go to next control
						continue;
					// add to list
					oarrHistoryParaList.Add(voReportHistoryPara);
				}
				// trim to actual size
				oarrHistoryParaList.TrimToSize();
				// deleted: dungla 01-Mar-2006
//				marrHistoryPara.Clear();
//				marrHistoryPara = oarrHistoryParaList;
				// return new command with parameter replaced by its value
				return strNewCommand;
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

		private string AnalyzeCommand(string pstrCommand, out string ostrFileName, out string ostrNamespace, out string ostrMethod, out ArrayList oarrParaList)
		{
			const string METHOD_NAME = THIS + ".AnalyzeCommand()";
			try
			{
				// Command have following format: [FileName]-[NameSpace]-[Method]
				ostrFileName = string.Empty;
				ostrNamespace = string.Empty;
				ostrMethod = string.Empty;
				oarrParaList = new ArrayList();

				string strNewCommand = pstrCommand;
				string[] arrCommands = pstrCommand.Split(COMMAND_DELIMITER);
				ostrFileName = arrCommands[0].Replace(Constants.OPEN_SBRACKET, string.Empty).Replace(Constants.CLOSE_SBRACKET, string.Empty);
				ostrNamespace = arrCommands[1].Replace(Constants.OPEN_SBRACKET, string.Empty).Replace(Constants.CLOSE_SBRACKET, string.Empty);
				ostrMethod = arrCommands[2].Replace(Constants.OPEN_SBRACKET, string.Empty).Replace(Constants.CLOSE_SBRACKET, string.Empty);

				// now we have method name, get all parameter of method
				//string strParams = string.Empty;
				if (ostrMethod.IndexOf(CLOSE_QUOTE) >= 0)
					ostrMethod = ostrMethod.Substring(0, ostrMethod.IndexOf(OPEN_QUOTE));
				
				// replace para by value
				foreach (Control objControl in this.pnlParameters.Controls)
				{
					sys_ReportHistoryParaVO voReportHistoryPara = null;
					sys_ReportParaVO voTagPara = null;

					// if current control is TextBox/ComboBox/CheckBox/C1DateEdit then get the tag object
					if (objControl.GetType().Equals(typeof (TextBox)) ||
						objControl.GetType().Equals(typeof (ComboBox)) ||
						objControl.GetType().Equals(typeof (CheckBox)) ||
						objControl.GetType().Equals(typeof (C1DateEdit)))
					{
						voTagPara = (sys_ReportParaVO) (objControl.Tag);
						// if scanning thru filter field1 and 2, go to next control
						if (objControl.Name.Equals(TEXTBOX_FILTER1_PREFIX + voTagPara.ParaName + voTagPara.FilterField1) ||
							objControl.Name.Equals(TEXTBOX_FILTER2_PREFIX + voTagPara.ParaName + voTagPara.FilterField2))
							continue;
					}
					else // go to next control
						continue;

					voReportHistoryPara = new sys_ReportHistoryParaVO();
					voReportHistoryPara.ParaName = voTagPara.ParaName;


					// C1DateEdit
					if (objControl.GetType().Equals(typeof (C1DateEdit)))
					{
						C1DateEdit objDate = (C1DateEdit)objControl;
						if (voTagPara.DataType.Equals((int) TypeCode.DateTime))
						{
							if (!objDate.ValueIsDbNull)
								strNewCommand = strNewCommand.Replace(voTagPara.ParaName,
									((DateTime)((C1DateEdit) objControl).Value).ToString(SQL_DATE_TIME_FORMAT));
							else
								strNewCommand = strNewCommand.Replace(voTagPara.ParaName, string.Empty);
						}
						if (!objDate.ValueIsDbNull)
							voReportHistoryPara.ParaValue = ((DateTime)((C1DateEdit) objControl).Value).ToString(SQL_DATE_TIME_FORMAT);
						else
							voReportHistoryPara.ParaValue = string.Empty;
					}
						// Textbox
					else if (objControl.GetType().Equals(typeof (TextBox)))
					{
						// normal textbox
						if (objControl.Name.Equals(TEXTBOX_PREFIX + voTagPara.ParaName))
						{
							// if TagValue = true then parameter value get from .Tag property
							if (voTagPara.TagValue)
							{
								voReportHistoryPara.TagValue = voTagPara.DefaultValue;
								strNewCommand = strNewCommand.Replace(voTagPara.ParaName, voTagPara.DefaultValue);
							}
							else // else TagValue = false then parameter value get from .Text property
							{
								voReportHistoryPara.ParaValue = objControl.Text;
								strNewCommand = strNewCommand.Replace(voTagPara.ParaName, objControl.Text);
							}
						}
						else if (objControl.Name.Equals(TEXTBOX_KEYFIELD_PREFIX + voTagPara.ParaName + voTagPara.FromField)) // TextBox is FromField
						{
							// Key field, filter field1, filter field 2
							if (voTagPara.TagValue)
								voReportHistoryPara.TagValue = objControl.Text;
							else
								voReportHistoryPara.ParaValue = objControl.Text;
							
							if (voTagPara.MultiSelection)
							{
								if (voTagPara.DataType == (int)TypeCode.String)
								{
									string strValue = objControl.Text;
									string strTemp = string.Empty;
									if (strValue.Trim().Length > 0)
									{
										if (strValue.IndexOf(",") >= 0)
										{
											string[] strValues = strValue.Split(",".ToCharArray());
											foreach (string strVal in strValues)
												strTemp = strTemp + "'" + strVal + "',";
										}
										else
											strTemp = "'" + strValue + "',";
									}
									// remove the last ","
									if (strTemp.Trim() != string.Empty && strTemp.IndexOf(",") >= 0)
										strTemp = strTemp.Substring(0, strTemp.Length - 1);
									if (voTagPara.TagValue)
										voReportHistoryPara.TagValue = strTemp;
									else
										voReportHistoryPara.ParaValue = strTemp;
									strNewCommand = strNewCommand.Replace(voTagPara.ParaName, strTemp);
								}
								else
									strNewCommand = strNewCommand.Replace(voTagPara.ParaName, objControl.Text);
							}
							else
								strNewCommand = strNewCommand.Replace(voTagPara.ParaName, objControl.Text);

							try
							{
								// FilterField1
								Control objFilterField1 = GetControlFromPanel(pnlParameters, TEXTBOX_FILTER1_PREFIX + voTagPara.ParaName + voTagPara.FilterField1);
								voReportHistoryPara.FilterField1Value = objFilterField1.Text;
							}
							catch{}
							try
							{
								// FilterField2
								Control objFilterField2 = GetControlFromPanel(pnlParameters, TEXTBOX_FILTER2_PREFIX + voTagPara.ParaName + voTagPara.FilterField1);
								voReportHistoryPara.FilterField2Value = objFilterField2.Text;
							}
							catch{}
						}
					}
						// ComboBox
					else if (objControl.GetType().Equals(typeof (ComboBox)))
					{
						if ((voTagPara.DataType.Equals((int) TypeCode.Decimal)) ||
							(voTagPara.DataType.Equals((int) TypeCode.Double)) ||
							(voTagPara.DataType.Equals((int) TypeCode.Int16)) ||
							(voTagPara.DataType.Equals((int) TypeCode.Int32)) ||
							(voTagPara.DataType.Equals((int) TypeCode.Int64)))
						{
							voReportHistoryPara.ParaValue = ((ComboBox) objControl).SelectedItem.ToString();
							strNewCommand = strNewCommand.Replace(voTagPara.ParaName, ((ComboBox) objControl).SelectedItem.ToString());
						}
						else
						{
							voReportHistoryPara.ParaValue = ((ComboBox) objControl).SelectedItem.ToString();
							strNewCommand = strNewCommand.Replace(voTagPara.ParaName, ((ComboBox) objControl).SelectedItem.ToString());
						}
					}
						// Checkbox
					else if (objControl.GetType().Equals(typeof (CheckBox)))
					{

						if (voTagPara.DataType.Equals((int) TypeCode.Boolean))
						{
							//HACKED by TuanTQ: check Indeterminate status: 20 Dec 2005
							if(((CheckBox) objControl).CheckState == CheckState.Indeterminate)
								strNewCommand = strNewCommand.Replace(voTagPara.ParaName, string.Empty);
							else
								strNewCommand = strNewCommand.Replace(voTagPara.ParaName, ((CheckBox) objControl).Checked.ToString());
						}
						// if TagValue = true then parameter value get from .Tag property
						if (voTagPara.TagValue)
							voReportHistoryPara.TagValue = voTagPara.DefaultValue;
						else // else TagValue = false then parameter value get from .Text property
						{
							//HACKED by TuanTQ: check Indeterminate status: 20 Dec 2005
							if(((CheckBox) objControl).CheckState == CheckState.Indeterminate)
								voReportHistoryPara.ParaValue = string.Empty;
							else
								voReportHistoryPara.ParaValue = ((CheckBox) objControl).Checked.ToString();
						}
					}					
						// ingore other control (not TextBox, ComboBox, CheckBox, C1DateEdit), 
						// go to next control
					else
						continue;
					// add to list
					oarrParaList.Add(voReportHistoryPara);
				}
				// trim to actual size
				oarrParaList.TrimToSize();
				// return new command
				return strNewCommand;
			}
			catch (PCSException ex)
			{
				throw ex;
			}
			catch (IndexOutOfRangeException ex)
			{
				throw new PCSException(ErrorCode.MESSAGE_BAD_SQL_QUERY_IN_REPORT, METHOD_NAME, ex);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		/// <summary>
		/// Render report with ReportBuilder helper class
		/// </summary>
		/// <param name="pdtbData">Report Data</param>
		/// <param name="pstrLogoFile">Path to company logo file</param>
		private void RenderReport(DataTable pdtbData, string pstrLogoFile)
		{
			this.Cursor = Cursors.WaitCursor;
			objBuilder.ReportName = mvoReport.ReportName;
			// report object
			objBuilder.ReportVO = mvoReport;
			// list of field
			objBuilder.FieldList = marrFields;
			// parameters
			objBuilder.Parameters = arrParams;
			// parameters value
			objBuilder.ParamValues = marrHistoryPara;
			// use layout file or not
			objBuilder.UseLayoutFile = mvoReport.UseTemplate;
			// DataSource
			objBuilder.SourceDataTable = pdtbData;
			// logo file
			objBuilder.LogoFile = pstrLogoFile;
			objBuilder.ReportViewer = this.ReportViewer;
			// render the report
			objBuilder.RenderReport();
			this.Cursor = Cursors.Default;
		}

		private void RenderReportWithDefinitionFile(DataTable ptblSource)
		{
			//HACKED: Thachnn Review
			const string METHOD_NAME = THIS + ".RenderReportWithDefinitionFile(DataTable ptblSource)";
			
			try
			{
				Cursor = Cursors.WaitCursor; // reset in finally clause		


				#region HACKED: THACHNN: 19/01/2006 : Render using ReportBuilder instead of write duplicated code here

				#region INIT REPORT BUIDER OBJECT
				ReportBuilder objRB;	
				objRB = new ReportBuilder();			
				objRB.ReportName = 	mvoReport.ReportName;				
				objRB.SourceDataTable = ptblSource;

				try
				{
					objRB.ReportDefinitionFolder = mstrReportDefFolder;
					objRB.ReportLayoutFile = mvoReport.TemplateFile;					
					if(objRB.AnalyseLayoutFile() == false)
					{
						throw new IOException(METHOD_NAME + COMMAND_DELIMITER + mstrReportDefFolder + "\\" + mvoReport.TemplateFile);
					}
					objRB.UseLayoutFile = true;	// always use layout file
				}
				catch
				{
					objRB.UseLayoutFile = false;					
				}

				//C1.C1Report.Layout objLayout = objRB.Report.Layout;				
				#endregion
                
				objRB.MakeDataTableForRender();


				#region RENDER TO PRINT PREVIEW				
				objRB.ReportViewer = ReportViewer;
				objRB.RenderReport();			


				#region COMPANY INFO  header information get from system params
				try
				{
					objRB.DrawPredefinedField("fldCompany",SystemProperty.SytemParams.Get(SystemParam.COMPANY_FULL_NAME));
				}
				catch{}
				try
				{
					objRB.DrawPredefinedField("fldAddress",SystemProperty.SytemParams.Get(SystemParam.ADDRESS));					
				}
				catch{}
				try
				{
					objRB.DrawPredefinedField("fldTel",SystemProperty.SytemParams.Get(SystemParam.TEL));					
				}
				catch{}
				try
				{
					objRB.DrawPredefinedField("fldFax",SystemProperty.SytemParams.Get(SystemParam.FAX));					
				}
				catch{}

				#endregion

				#region REM -- DRAW Parameters
			
				
//				const string CCN = "CCN";
//				const string MASTER_LOCATION = "Master Location";
//				const string LOCATION = "Location";
//				const string CATEGORY = "Category";
//				const string MODEL = "Model";
//
//				System.Collections.Specialized.NameValueCollection arrParamAndValue = new System.Collections.Specialized.NameValueCollection();
//				arrParamAndValue.Add(CCN, strCCN);
//				arrParamAndValue.Add(MASTER_LOCATION, strMasterLocation);
//				if(pstrLocationID.Trim() != string.Empty)
//				{
//					arrParamAndValue.Add(LOCATION, strLocation);
//				}
//				if(pstrCategoryID.Trim() != string.Empty)
//				{
//					arrParamAndValue.Add(CATEGORY, strCategory);
//				}
//				if(pstrParameterModel.Trim() != string.Empty)
//				{
//					arrParamAndValue.Add(MODEL, pstrParameterModel);
//				}
//		
//				/// anchor the Parameter drawing canvas cordinate to the fldTitle
//				C1.C1Report.Field fldTitle = objRB.GetFieldByName(REPORTFLD_TITLE);
//				double dblStartX = fldTitle.Left;
//				double dblStartY = fldTitle.Top  + 1.3*fldTitle.RenderHeight;
//				objRB.GetSectionByName(HEADER).CanGrow = true;
//				objRB.DrawParameters( objRB.GetSectionByName(HEADER) ,dblStartX , dblStartY , arrParamAndValue, objRB.Report.Font.Size);

				#endregion

				// HACK: dungla 10-21-2005
				try
				{				
					objRB.ReportVO = mvoReport;
					objRB.Parameters = arrParams;
					objRB.DrawReportHeaderParameter(rptReportData.Sections[SectionTypeEnum.Header], marrHistoryPara);
				}
				catch{}
				// END: dungla 10-21-2005

				objRB.RefreshReport();				

				#endregion

				#endregion ENDHACKED: THACHNN: 19/01/2006 : Render using ReportBuilder instead of write duplicated code here				
											
			}
			catch (NullReferenceException ex)
			{
				// HACK: dungla 10-26-2005
				// log error message if any. for debug only
				try
				{
					Logger.LogMessage(ex, METHOD_NAME, Level.DEBUG);
				}
				catch
				{
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
				}
				// END: dungla 10-26-2005
				// The C1PrintPreviewControl can't dispose properly
			}
			catch (PCSException ex)
			{
				try
				{
					Logger.LogMessage(ex.CauseException, METHOD_NAME, Level.ERROR);
				}
				catch
				{
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
				}
				throw ex;
			}
			catch (IOException ex)
			{
				try
				{
					Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
				}
				catch
				{
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
				}
				throw new PCSException(ErrorCode.MESSAGE_REPORT_TEMPLATE_FILE_NOT_FOUND, METHOD_NAME, ex);
			}
			catch (DataAccessException ex)
			{
				// HACK: dungla 10-26-2005
				// log error message if any. for debug only
				try
				{
					Logger.LogMessage(ex, METHOD_NAME, Level.DEBUG);
				}
				catch
				{
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
				}
				// END: dungla 10-26-2005
				/// Thachnn says:
				/// layout file contain datasource, this datasource can raise error here, 
				/// we catch the exception and do not display this to users.
				/// Developer need to remove datasource string of the XML layout file before release to use in  
				/// PCS Report Framework

			}
			catch (Exception ex)
			{
				try
				{
					Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
				}
				catch
				{
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
				}
				throw ex;
			}
			finally
			{
				Cursor = Cursors.Default;
			}
		}
		private void ClearAllFilter()
		{
			try
			{
				if (mtblReportData != null)
				{
					this.mtblReportData.DefaultView.RowFilter = string.Empty;
					strFilterString = string.Empty;
					strPreviousFilterString = string.Empty;
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		private void FilterWithCurrentValue(bool blnExceptCurrentValue)
		{
			const string METHOD_NAME = THIS + ".FilterWithCurrentValue()";
			const string JOIN_FILTER_OPERATION = " and ";
			const string FILTER_OPERATION_LIKE = " like ";
			const string FILTER_OPERATION_EXCEPT = " <> ";
			// in order to use custom filter
			// we have to bind this True DbGrid to a Table not a DataSet
			try
			{
                if (gridReportData.RowCount == 0)
				{
					// if there is no row, this method will return without doing anything
					return;
				}
				// get the column name
				string strColumnName = gridReportData.Splits[0].DisplayColumns[gridReportData.Col].DataColumn.DataField;
				// get the cel value
				string strCellValue = gridReportData[gridReportData.Row, gridReportData.Col].ToString();

				// get the filter string

				string strFilterStringColumn = string.Empty;
				if (mtblReportData.Columns[strColumnName].DataType == typeof (string)
					|| mtblReportData.Columns[strColumnName].DataType == typeof (char))
				{
					if (!blnExceptCurrentValue)
					{
						strFilterStringColumn = "[" + strColumnName + "]" + FILTER_OPERATION_LIKE + " '" + strCellValue + "%'";
					}
					else
					{
						strFilterStringColumn = "[" + strColumnName + "]" + " not " + FILTER_OPERATION_LIKE + " '" + strCellValue + "%'";
					}
				}
				else
				{
					if (!blnExceptCurrentValue)
					{
						strFilterStringColumn = "[" + strColumnName + "]" + EQUAL + " '" + strCellValue + "'";
					}
					else
					{
						strFilterStringColumn = "[" + strColumnName + "]" + FILTER_OPERATION_EXCEPT + " '" + strCellValue + "'";
					}
				}

				strPreviousFilterString = strFilterString;
				if (strFilterString != string.Empty)
				{
					strFilterString += JOIN_FILTER_OPERATION + strFilterStringColumn;
				}
				else
				{
					strFilterString = strFilterStringColumn;
				}
				this.mtblReportData.DefaultView.RowFilter = strFilterString;

			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		private void ReturnPreviousFilter()
		{
			try
			{
				this.mtblReportData.DefaultView.RowFilter = strPreviousFilterString;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		private void RowFilter()
		{
			gridReportData.FilterBar = !gridReportData.FilterBar;
			if (blnRunRowFilter) 
				gridReportData.FilterActive = true;
			blnRunRowFilter = true;
			if (gridReportData.FilterBar)
			{
				DataTable dtData = mtblReportData.Copy();

				foreach (DataColumn dtColumn in dtData.Columns)
				{
					string strFieldName = dtColumn.ColumnName;
					if (gridReportData.Columns[strFieldName].ValueItems.Presentation == PresentationEnum.CheckBox)
						continue;
					if (!gridReportData.Splits[0].DisplayColumns[strFieldName].Visible)
						continue;
					AddValueIntoComboBoxInTrueDBGrid(gridReportData.Columns[strFieldName],strFieldName);
					gridReportData.Splits[0].DisplayColumns[strFieldName].FilterButton = true;
					gridReportData.Splits[0].DisplayColumns[strFieldName].Button = false;

				}
				gridReportData.FilterActive = true;
			}
		}

		private void SumCurrentColumn()
		{
			const string METHOD_NAME = THIS + ".SumCurrentColumn()";
			//In order to use custom filter
			//we have to bind this True DbGrid to a Table not a DataSet
			try
			{
                if (gridReportData.RowCount == 0)
				{
					return;
				}
				//get the column name
				string strColumnName = gridReportData.Splits[0].DisplayColumns[gridReportData.Col].DataColumn.DataField;

				int intGridRows = this.gridReportData.RowCount;

				// now compute the number of unique values for the country and city columns
				double dblTotalValue = 0;
				for (int i = 0; i < intGridRows; i++)
				{
					try
					{
						dblTotalValue += double.Parse(this.gridReportData[i, strColumnName].ToString());
					}
					catch
					{
						dblTotalValue += 0;
					}
				}
				gridReportData.ColumnFooters = true;
				this.gridReportData.Columns[strColumnName].FooterText = dblTotalValue.ToString();
			}
			catch (Exception ex)
			{
				throw ex;
			}

		}

		private void PrintDataToPrinter()
		{
			const string METHOD_NAME = THIS + ".PrintDataToPrinter()";
			try
			{ //gridReportData.PrintInfo;
				Font fntFont;
				fntFont = new Font(gridReportData.PrintInfo.PageHeaderStyle.Font.Name, gridReportData.PrintInfo.PageHeaderStyle.Font.Size, FontStyle.Italic);
				gridReportData.PrintInfo.PageHeaderStyle.Font = fntFont;
				gridReportData.PrintInfo.PageHeader = "Composers Table";

				//column headers will be on every page
				gridReportData.PrintInfo.RepeatColumnHeaders = true;

				//'display page numbers (centered)
				gridReportData.PrintInfo.PageFooter = "Page: \\p";

				//'invoke print preview
				gridReportData.PrintInfo.UseGridColors = true;
				gridReportData.PrintInfo.PrintPreview();

				// Insert code to print the file.    
			}
			catch (Exception ex)
			{
				throw ex;
			}

		}

		private void ShowDrillDownReport()
		{
			const string METHOD_NAME = THIS + ".ShowDrillDownReport()";
			try
			{
                if (this.gridReportData.RowCount == 0)
				{
					// if there is no row, this method will return without doing anything
					return;
				}
				ReportManagementBO boReportManagement = new ReportManagementBO();
				ArrayList arrDrillDowns = boReportManagement.GetDrillDownReports(this.mvoReport.ReportID);
				if (arrDrillDowns.Count > 0)
				{
					sys_ReportDrillDownVO voDrillDownReport = (sys_ReportDrillDownVO) arrDrillDowns[0];
					ViewReport frmViewReport = new ViewReport();
					// get detail report of current report
					sys_ReportVO voDetailReport = (sys_ReportVO) (new EditReportBO()).GetObjectVO(voDrillDownReport.DetailReportID);
					// assign value for ViewReport form
					frmViewReport.VoReport = voDetailReport;
					frmViewReport.ViewMode = ViewReportMode.Normal;
					frmViewReport.Show();
//					// get the report vo
//					// selected row number from C1TrueDBGrid is the row index of mtblReportData
//					DataRow drowData = this.mtblReportData.Rows[this.gridReportData.Row];
//					foreach (DataColumn dcolData in this.mtblReportData.Columns)
//					{
//						MessageBox.Show(drowData[dcolData].ToString());
//					}
				}
				else
				{
					return;
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

		private void ExecuteFileReport(string pstrDomainName)
		{
			const string METHOD_NAME = THIS + ".ExecuteFileReport()";
			string strAssemblyName = string.Empty;
			string strNamespace = string.Empty;
			string strMethodName = string.Empty;
			//ArrayList arrHistoryParaList = new ArrayList();
			AppDomain objDomain = null;
			// analyze report command
			try
			{
				// clear old data first
				marrHistoryPara.Clear();
				this.AnalyzeCommand(this.mvoReport.Command, out strAssemblyName, out strNamespace,
				                    out strMethodName, out marrHistoryPara);
			}
			catch (PCSException ex)
			{
				throw new PCSException(ex.mCode, METHOD_NAME, ex);
			}
			catch (Exception ex)
			{
				throw new PCSException(ErrorCode.MESSAGE_BAD_SQL_QUERY_IN_REPORT, METHOD_NAME, ex);
			}

			if( ! File.Exists(mstrReportDefFolder + "\\" + mvoReport.ReportFile)) // file C# is not exist !!!
			{
				throw new PCSException(ErrorCode.MESSAGE_REPORT_FILE_NOT_FOUND, THIS, new IOException(mstrReportDefFolder + "\\" + mvoReport.ReportFile)) ;
			}

			// get the parameters value in order to parsing as agrument when invoke method
			object[] objArgs = new object[marrHistoryPara.Count];
			for (int i = 0; i < marrHistoryPara.Count; i++)
			{
				sys_ReportHistoryParaVO voReportHistoryPara = (sys_ReportHistoryParaVO) marrHistoryPara[i];
				objArgs[i] = voReportHistoryPara.ParaValue;
			}
			// compile C# file to the DLL File (output of the Compile function is DLL file path
			FormControlComponents objFCC = new FormControlComponents();
			string strCompiledDLLFile =
				objFCC.CompileCSharpFile(mstrReportDefFolder + "\\" + mvoReport.ReportFile,
				                         DLL_EXTENSION,
				                         SYSTEM_ASSEMBLY,
				                         SYSTEM_DATA_ASSEMBLY,
				                         SYSTEM_XML_ASSEMBLY,
				                         COMMAND_SEPERATOR);

			IDynamicReport objDynamicReport = null;
			try
			{
				AppDomainSetup objSetup = new AppDomainSetup();
				objSetup.ApplicationBase = AppDomain.CurrentDomain.BaseDirectory;
				try
				{
					objDomain = AppDomain.CreateDomain(pstrDomainName, null, objSetup);
				}
				catch (Exception ex)
				{
					throw new PCSException(ErrorCode.CREATE_DOMAIN_ERROR, METHOD_NAME, ex);
				}
				try
				{
					objDynamicReport = (IDynamicReport) objDomain.CreateInstanceFrom(strCompiledDLLFile, strNamespace).Unwrap();
				}
				catch (Exception ex)
				{
					Logger.LogMessage(ex, METHOD_NAME, Level.DEBUG);
					throw new PCSException(ErrorCode.CREATE_REMOTE_INSTANCE_ERROR, METHOD_NAME, ex);
				}
				if (objDynamicReport == null)
					throw new PCSException(ErrorCode.TYPELOADEXCEPTION, METHOD_NAME, new Exception());

				//use SubString to copy instance, not reference
				objDynamicReport.PCSConnectionString = PCSComUtils.DataAccess.Utils.Instance.OleDbConnectionString;
				objDynamicReport.ReportDefinitionFolder = mstrReportDefFolder.Substring(0);
				objDynamicReport.ReportLayoutFile = mvoReport.TemplateFile;

				try
				{
					mtblReportData = (DataTable) objDynamicReport.Invoke(strMethodName, objArgs);
				}
				catch (MissingMethodException exMissMethod)
				{
					/// REVIEW: define new message if needed. 
					/// (declared namespace, class name or method name of the Dynamic Report is wrong)
					Logger.LogMessage(exMissMethod, METHOD_NAME, Level.DEBUG);
					throw new PCSException(ErrorCode.MESSAGE_BAD_SQL_QUERY_IN_REPORT, METHOD_NAME, exMissMethod);
				}
				catch(TargetInvocationException exFromDynamicMethod)
				{
					/// REVIEW: define new message if needed. 
					/// (declared BODY of the Dynamic Report is wrong)
					Logger.LogMessage(exFromDynamicMethod, strMethodName, Level.DEBUG);
					throw new PCSException(ErrorCode.INVOKE_METHOD_ERROR, METHOD_NAME, exFromDynamicMethod);
				}
				catch( IOException ex)
				{
					Logger.LogMessage(ex, METHOD_NAME, Level.DEBUG);
					throw new PCSException(ErrorCode.MESSAGE_REPORT_TEMPLATE_FILE_NOT_FOUND, METHOD_NAME, ex);
				}
				catch (Exception ex)
				{
					Logger.LogMessage(ex, METHOD_NAME, Level.DEBUG);
					throw new PCSException(ErrorCode.MESSAGE_BAD_SQL_QUERY_IN_REPORT, METHOD_NAME, ex);
				}
			}			
			catch (Exception ex)
			{
				throw ex;
			}
			finally
			{
				try
				{
					// hold the executed DynamicReport in the ReportViewer form
					/// But if hold, we do attach the External C# process to the PCS Process, so unload External mean unload PCS
					//mDynamicReport = objDynamicReport;	

					mblnUseReportViewerRenderEngine = objDynamicReport.UseReportViewerRenderEngine;

					objDynamicReport = null;

					if(mblnUseReportViewerRenderEngine == true)	// render report by PCS. We can unload the External Process.
					{
						// unload domain.
						AppDomain.Unload(objDomain);
						objDomain = null;
					}
				}
				catch (Exception ex)
				{
					Logger.LogMessage(ex, METHOD_NAME, Level.DEBUG);
				}
				// delete compiled assembly
				try
				{
					if(mblnUseReportViewerRenderEngine == true)	// render report by PCS. We can unload the External Process. and Delete temp file here
					{
						File.Delete(strCompiledDLLFile);
					}
				}
				catch (Exception ex)
				{
					Logger.LogMessage(ex, METHOD_NAME, Level.DEBUG);
				}
			}

			if (mtblReportData == null)
				return;
			// before invoke method to execute report (create report history table)
			// we need to delete old reports history, keep only last 10 reports
			// get list of history report executed by current user

			/// HACKED: Thachnn: fix bug CuongNT
			/// Create fields for report and insert into Sys_ReportField table
			/// only generate report fields when the report is not use the template file
			if (!mvoReport.UseTemplate)
				CreateFieldForReportInSys_ReportFieldTable(mtblReportData, ref marrFields);
			/// ENDHACKED: Thachnn: fix bug

			#region Bind Result to Grid

			// displays result in grid Report Data tab.
			try
			{
				gridReportData.DataSource = mtblReportData;
				gridReportData.DataMember = mtblReportData.TableName;
				LayoutGrid();
			}
			catch (ArgumentNullException ex)
			{
				throw ex;
			}
			catch (Exception ex)
			{
				throw ex;
			}

			#endregion
		}

		private void ExecuteDLLReport(string pstrDomainName)
		{
			const string METHOD_NAME = THIS + ".ExecuteFileReport()";
			AppDomain objDomain = null;
			string strFilePath = string.Empty;
			string strAssemblyName = string.Empty;
			string strNamespace = string.Empty;
			string strMethodName = string.Empty;
			//ArrayList arrHistoryParaList = new ArrayList();
			string strReportFile = mstrReportDefFolder + "\\" + mvoReport.ReportFile;
			// checking file version first
			Assembly assemReport = Assembly.LoadFrom(strReportFile);
			AssemblyName assemUtils = Assembly.GetAssembly(typeof(ViewReport)).GetName();
			AssemblyName[] assemRef = assemReport.GetReferencedAssemblies();
			foreach (AssemblyName assemItem in assemRef)
			{
				if (assemItem.Name == assemUtils.Name)
				{
					// if the report file of client not match with com assembly
					// cannot execute the report
					if (assemItem.Version != assemUtils.Version)
					{
						PCSMessageBox.Show(ErrorCode.MESSAGE_DLL_VERSION_NOT_MATCH, MessageBoxIcon.Exclamation);
						return;
					}
				}
			}
			// free the file
			assemReport = null;
			assemUtils = null;
			assemRef = null;
			// analyze report command
			try
			{
				// clear old data first
				marrHistoryPara.Clear();
				this.AnalyzeCommand(this.mvoReport.Command, out strAssemblyName, out strNamespace, out strMethodName, out marrHistoryPara);
			}
			catch (PCSException ex)
			{
				throw new PCSException(ex.mCode, METHOD_NAME, ex);
			}
			catch (Exception ex)
			{
				throw new PCSException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
			}
			// get the parameters value in order to parsing as agrument when invoke method
			object[] objArgs = new object[marrHistoryPara.Count];
			for (int i = 0; i < marrHistoryPara.Count; i++)
			{
				sys_ReportHistoryParaVO voReportHistoryPara = (sys_ReportHistoryParaVO) marrHistoryPara[i];
				objArgs[i] = voReportHistoryPara.ParaValue;
			}

			IDynamicReport objDynamicReport = null;
			try
			{
				// create an AppDomain
				AppDomainSetup objSetup = new AppDomainSetup();
				objSetup.ApplicationBase = AppDomain.CurrentDomain.BaseDirectory;
				try
				{
					objDomain = AppDomain.CreateDomain(pstrDomainName, null, objSetup);
					strFilePath = Path.Combine(objDomain.BaseDirectory, strAssemblyName + DLL_EXTENSION);
					// copy assembly file to BaseDirectory of new domain. Overwrite if existed.
					try
					{
						File.Copy(strReportFile, strFilePath, true);
					}
					catch
					{
					}
				}
				catch (Exception ex)
				{
					throw new PCSException(ErrorCode.CREATE_DOMAIN_ERROR, METHOD_NAME, ex);
				}

				try
				{
					objDynamicReport = (IDynamicReport) objDomain.CreateInstanceFrom(strFilePath, strNamespace).Unwrap();
				}
				catch (Exception ex)
				{
					Logger.LogMessage(ex, METHOD_NAME, Level.DEBUG);
					throw new PCSException(ErrorCode.CREATE_REMOTE_INSTANCE_ERROR, METHOD_NAME, ex);
				}
				if (objDynamicReport == null)
					throw new PCSException(ErrorCode.TYPELOADEXCEPTION, METHOD_NAME, new Exception());
                objDynamicReport.PCSConnectionString = PCSComUtils.DataAccess.Utils.Instance.OleDbConnectionString;
				objDynamicReport.PCSReportViewer = this.ReportViewer;
				objDynamicReport.PCSReportBuilder = this.objBuilder;
				try
				{
					mtblReportData = (DataTable) objDynamicReport.Invoke(strMethodName, objArgs);
				}
				catch (Exception ex)
				{
					Logger.LogMessage(ex, METHOD_NAME, Level.DEBUG);
					throw new PCSException(ErrorCode.MESSAGE_BAD_SQL_QUERY_IN_REPORT, METHOD_NAME, ex);
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
			finally
			{
				// free resource
				objDynamicReport = null;
				// unload domain
				AppDomain.Unload(objDomain);
				objDomain = null;
				// try to delete the executed dll file
				if (strFilePath != string.Empty)
				{
					try
					{
						File.Delete(strFilePath);
					}
					catch (Exception ex)
					{
						Logger.LogMessage(ex, METHOD_NAME, Level.DEBUG);
					}
				}
			}

			/// HACKED: Thachnn: Add the NumberedList to the DataTable mtblReportData

			#region // HACK: DEL dungla 10-26-2005

			//mtblReportAlterData = FormControlComponents.AddNumberedListToDataTable(mtblReportData);

			#endregion // END: DEL dungla 10-26-2005

			/// END HACKED:

			// before invoke method to execute report (create report history table)
			// we need to delete old reports history, keep only last 10 reports
			// get list of history report executed by current user

			/// HACKED: Thachnn: fix bug CuongNT
			/// Create fields for report and insert into Sys_ReportField table
			/// only generate report fields when the report is not use the template file
			if (!mvoReport.UseTemplate)
				CreateFieldForReportInSys_ReportFieldTable(mtblReportData, ref marrFields);
			/// ENDHACKED: Thachnn: fix bug CuongNT

			#region Bind Result to Grid

			// displays result in grid Report Data tab.
			try
			{
				gridReportData.DataSource = mtblReportData;
				gridReportData.DataMember = mtblReportData.TableName;
				LayoutGrid();
			}
			catch (ArgumentNullException ex)
			{
				throw ex;
			}
			catch (Exception ex)
			{
				throw ex;
			}

			#endregion
		}

		private void ExecuteSQLReport()
		{
			const string METHOD_NAME = THIS + ".ExecuteSQLReport()";
			try
			{
				// command of the report
				string strCommand = string.Empty;

				
				//ArrayList arrReportHistoryPara = new ArrayList();
				// clear old data first
				marrHistoryPara.Clear();

				strCommand = this.BuildSqlCommand(mvoReport.Command, out marrHistoryPara);

				// trim white space before analyze command
				strCommand = strCommand.Trim();
				
				// before invoke method to execute report (create report history table)
				// we need to delete old reports history, keep only last 10 reports
				// get list of history report executed by current user

				#region Get data, Bind Result to Grid

				// execute report command to get data
				DataSet dstResult;
				try
				{
					dstResult = boViewReport.ExecuteReportCommand(strCommand);
				}
				catch (Exception ex)
				{
					throw new PCSException(ErrorCode.MESSAGE_BAD_SQL_QUERY_IN_REPORT, METHOD_NAME, ex);
				}

				// store result into table
				mtblReportData = dstResult.Tables[0];


				/// HACKED: Thachnn: fix bug CuongNT
				/// Create fields for report and insert into Sys_ReportField table
				/// only generate report fields when the report is not use the template file
				if (!mvoReport.UseTemplate)
					CreateFieldForReportInSys_ReportFieldTable(mtblReportData, ref marrFields);
				/// HACKED: Thachnn: fix bug CuongNT


				// displays result in grid Report Data tab.
				gridReportData.DataSource = mtblReportData;
				gridReportData.DataMember = mtblReportData.TableName;

				LayoutGrid();

				#endregion
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

		private void ExecuteCustomReport()
		{
			const string METHOD_NAME = THIS + ".ExecuteCustomReport()";
			try
			{
				Cursor = Cursors.WaitCursor; // reset in finally clause		

				// command of the report
				string strCommand = string.Empty;

				ArrayList arrReportHistoryPara = new ArrayList();

				// prevent reentrant calls
				if (rptReportData.IsBusy) return;
				
				strCommand = this.BuildSqlCommand(mvoReport.Command, out arrReportHistoryPara);
				// trim white space before analyze command
				strCommand = strCommand.Trim();
				
				// before invoke method to execute report (create report history table)
				// we need to delete old reports history, keep only last 10 reports
				// get list of history report executed by current user

				#region Create Report History Table For Current User

				#endregion 

				#region GetData, Bind Result to Grid

				// execute report command to get data
				DataSet dstResult;
				try
				{
					dstResult = boViewReport.ExecuteReportCommand(strCommand);
				}
				catch (Exception ex)
				{
					throw new PCSException(ErrorCode.MESSAGE_BAD_SQL_QUERY_IN_REPORT, METHOD_NAME, ex);
				}

				// store result into table
				mtblReportData = dstResult.Tables[0];


				/// HACKED: Thachnn: Fix bug CuongNT
				/// Create fields for report and insert into Sys_ReportField table
				/// only generate report fields when the report is not use the template file
				if (!mvoReport.UseTemplate)
					CreateFieldForReportInSys_ReportFieldTable(mtblReportData, ref marrFields);
				/// ENDHACKED: Thachnn: fix bug CuongNT

				// displays result in grid Report Data tab.
				gridReportData.DataSource = mtblReportData;
				gridReportData.DataMember = mtblReportData.TableName;

				LayoutGrid();

				#endregion
			}
				#region catch 

			catch (ArgumentNullException ex)
			{
				try
				{
					Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
				}
				catch
				{
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
				}
				throw new PCSException(ErrorCode.ARGUMENTNULLEXCEPTION, METHOD_NAME, ex);
			}
			catch (FileNotFoundException ex)
			{
				try
				{
					Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
				}
				catch
				{
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
				}
				throw new PCSException(ErrorCode.MESSAGE_REPORT_FILE_NOT_FOUND, METHOD_NAME, ex);
			}
			catch (ArgumentOutOfRangeException ex)
			{
				try
				{
					Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
				}
				catch
				{
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
				}
				throw new PCSException(ErrorCode.MESSAGE_BAD_SQL_QUERY_IN_REPORT, METHOD_NAME, ex);
			}
			catch (PCSException ex)
			{
				//PCSMessageBox.Show(ex.mCode, MessageBoxIcon.Error);
				try
				{
					Logger.LogMessage(ex.CauseException, METHOD_NAME, Level.ERROR);
				}
				catch
				{
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
				}
				throw ex;
			}
			catch (Exception ex)
			{
				try
				{
					Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
				}
				catch
				{
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
				}
				throw ex;
			}
				#endregion catch

			finally
			{
				Cursor = Cursors.Default;
			}

		}

		private void AssignValue(Panel ppnlContainer)
		{
			const string METHOD_NAME = THIS + ".AssignValue()";
			try
			{
				foreach (Control objControl in ppnlContainer.Controls)
				{
					// ignore label control
					if (objControl.GetType().Equals(typeof (Label)))
						continue;

					// retrieve object from Tag property of control
					sys_ReportParaVO voPara = (sys_ReportParaVO) (objControl.Tag);

					try
					{
						// get para value from history
						sys_ReportHistoryParaVO voHistoryPara = GetHistoryParaObject(voPara.ParaName);

						// assign value for current control
						if (objControl is C1DateEdit)
						{
							try
							{
								((C1DateEdit) objControl).Value = DateTime.Parse(voHistoryPara.ParaValue);
							}
							catch
							{
								((C1DateEdit) objControl).Value = DBNull.Value;
							}
						}
						else if (objControl is TextBox) // text box control
						{
							if (objControl.Name == TEXTBOX_PREFIX + voPara.ParaName)
								objControl.Text = voHistoryPara.ParaValue;
							else if (objControl.Name == TEXTBOX_KEYFIELD_PREFIX + voPara.ParaName + voPara.FromField)
								objControl.Text = voHistoryPara.ParaValue;
							else if (objControl.Name == TEXTBOX_FILTER1_PREFIX + voPara.ParaName + voPara.FilterField1)
								objControl.Text = voHistoryPara.FilterField1Value;
							else if (objControl.Name == TEXTBOX_FILTER2_PREFIX + voPara.ParaName + voPara.FilterField2)
								objControl.Text = voHistoryPara.FilterField2Value;
						}
						else if (objControl is ComboBox)
						{
							((ComboBox) objControl).SelectedItem = voHistoryPara.ParaValue;
						}
						else if (objControl is CheckBox)
						{
							try
							{
								((CheckBox) objControl).Checked = Convert.ToBoolean(int.Parse(voHistoryPara.ParaValue));
							}
							catch
							{
								((CheckBox) objControl).CheckState = CheckState.Indeterminate;
							}
						}
					}
					catch
					{
					}
				}
			}
			catch (PCSException ex)
			{
				throw ex;
			}
			catch (InvalidCastException ex)
			{
				throw ex;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		private sys_ReportHistoryParaVO GetHistoryParaObject(string pstrParaName)
		{
			for (int i = 0; i < marrHistoryPara.Count; i++)
			{
				sys_ReportHistoryParaVO voHistoryPara = (sys_ReportHistoryParaVO) marrHistoryPara[i];
				if (voHistoryPara.ParaName == pstrParaName)
				{
					return voHistoryPara;
				}
			}
			throw new ArgumentException();
		}

		private void ViewSingleRecord()
		{
			const string METHOD_NAME = THIS + ".ViewSingleRecord()";
			// View single record
			try
			{
                if (gridReportData.RowCount <= 0)
					return;
				// get selected row
				DataRow drowSelected = this.mtblReportData.Rows[this.gridReportData.Row];
				// convert fields list from DataSet to ArrayList
				ArrayList arrFieldList = new ArrayList();
				foreach (DataColumn dcolField in mtblReportData.Columns)
				{
					sys_TableFieldVO voTableField = new sys_TableFieldVO();
					voTableField.FieldName = dcolField.ColumnName;
					voTableField.CaptionJP = dcolField.Caption;
					voTableField.CaptionVN = dcolField.Caption;
					voTableField.CaptionEN = dcolField.Caption;
					voTableField.Caption = dcolField.Caption;
					voTableField.Invisible = false;
					if (dcolField.DataType == typeof (string))
					{
						voTableField.Align = PCSAligmentType.LEFT;
					}
					else if (dcolField.DataType == typeof (DateTime))
					{
						voTableField.Align = PCSAligmentType.CENTER;
					}
					else
					{
						voTableField.Align = PCSAligmentType.RIGHT;
					}
					voTableField.Width = dcolField.MaxLength;
					voTableField.ReadOnly = dcolField.ReadOnly;
					voTableField.NotAllowNull = dcolField.AllowDBNull;
					voTableField.IdentityColumn = dcolField.AutoIncrement;
					voTableField.UniqueColumn = dcolField.Unique;
					// add to field list
					arrFieldList.Add(voTableField);
				}
				// trim to actual size
				arrFieldList.TrimToSize();
				ViewSingleRecord frmViewSingleRecord = new ViewSingleRecord();
				frmViewSingleRecord.TableData = this.mtblReportData.DataSet;
				frmViewSingleRecord.RecordData = drowSelected;
				frmViewSingleRecord.RecordFields = arrFieldList;
				frmViewSingleRecord.ViewOnly = true;

				frmViewSingleRecord.ShowDialog();
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

		private string StardandizeSQL(string pstrOld)
		{
			string sRet = pstrOld;
			sRet = sRet.Replace("\t", " ");
			sRet = sRet.Replace("\n", " ");
			sRet = sRet.Replace("\r", " ");
			sRet = sRet.Replace(@"\t", " ");
			sRet = sRet.Replace(@"\n", " ");
			sRet = sRet.Replace(@"\r", " ");
			return sRet;
		}

		/// <summary>
		/// Thachnn : 28/09/2005
		/// Create fields for report and insert into Sys_ReportField table
		/// CALL ONLY by ExecuteXXXReport()
		/// </summary>
		/// <param name="ptblReportData">data table, source of report</param>
		/// <param name="parrFields">output Array of recent inserted fields</param>
		/// <returns>true if Create SUCCESSED</returns>
		private bool CreateFieldForReportInSys_ReportFieldTable(DataTable ptblReportData, ref ArrayList parrFields)
		{
			bool blnRet = false;

			try
			{
				if (ptblReportData != null)
				{
					/// HACKED: Thachnn: Fix bug do not save Field properties
					FieldPropertiesBO boFieldProperties = new FieldPropertiesBO();

					/// Get old field list in the database
					ArrayList arrExistFieldList = boFieldProperties.ListByReport(mvoReport.ReportID);

					/// Get new field list in the report which has just generated
					ArrayList arrNewFieldList = new ArrayList();
					int intFieldOrder = boFieldProperties.GetMaxFieldOrder(this.mvoReport.ReportID) + 1;
					// default font "Name|Size|Style
					foreach (DataColumn dcolField in ptblReportData.Columns)
					{
						sys_ReportFieldsVO voField = new sys_ReportFieldsVO();
						voField.ReportID = this.mvoReport.ReportID;
						// Column name will be the field name
						voField.FieldName = voField.FieldCaption
							= voField.FieldCaptionEN = voField.FieldCaptionJP
								= voField.FieldCaptionVN = dcolField.ColumnName;
						voField.Width = DEFAULT_FIELD_WIDTH;
						voField.FieldOrder = intFieldOrder;
						voField.Font = string.Empty; //mvoReport.FontDetail;
						voField.Visisble = true;
						voField.DataType = (int) Type.GetTypeCode(dcolField.DataType);

						arrNewFieldList.Add(voField);

						intFieldOrder ++;
					}

					ArrayList arrIntersectFields = GetIntersectFields(arrExistFieldList, arrNewFieldList);

					ArrayList arrResult = new ArrayList();
					arrResult.AddRange(arrIntersectFields);
					arrResult.AddRange(arrNewFieldList);

					/// CLEAR OLD FIELD SETTING
					boFieldProperties.Delete(mvoReport.ReportID);
					// recalculate the field width
					//arrResult = ReCalcualteFieldWidth(arrResult);
					/// Add arrResult to the database
					foreach (sys_ReportFieldsVO voField in arrResult)
					{
						boFieldProperties.Add(voField);
					}
					parrFields = boFieldProperties.ListByReport(this.mvoReport.ReportID);
					blnRet = true; // if function flow comes here, it mean function completed successfully
				}
			}
			catch //(Exception ex)
			{
				blnRet = false;
			}
			return blnRet;
			/// ENDHACKED: Thachnn			
		}

		/// <summary>
		/// /// Thachnn : 11/10/2005
		/// Get intersect part of arrExistList and arrNewList
		/// </summary>
		/// <param name="arrExistList">ExistList to find</param>
		/// <param name="arrNewList">NewList to find</param>
		/// <returns>return intersect ArrayList of arrExistList and arrNewList, return Empty ArrayList if function FAIL or ERROR</returns>
		public ArrayList GetIntersectFields(ArrayList arrExistList, ArrayList arrNewList) //, ArrayList arrintKeyFields)
		{
			ArrayList arrRet = new ArrayList();

			try
			{
				foreach (sys_ReportFieldsVO vo in arrExistList)
				{
					int intContainPosition = NewListContainVO(vo, arrNewList);
					if (intContainPosition > -1)
					{
						arrRet.Add(vo);
						arrNewList.RemoveAt(intContainPosition);
					}
				}
			}
			catch
			{
				arrRet = new ArrayList(); // if ERROR return empty ArrayList
			}

			return arrRet;
		}

		/// <summary>
		/// Thachnn : 11/10/2005
		/// Find sys_ReportFieldsVO pvo in arrNewList
		/// Return position of pvo in the arrayList
		/// </summary>
		/// <param name="pvo">sys_ReportFieldsVO to find </param>
		/// <param name="arrNewList">arrayList to find in</param>
		/// <returns>return position of pvo in the arrNewList if arrNewList contain pvo, return -1 if pvo is not in arrNewList or when Error</returns>
		public int NewListContainVO(sys_ReportFieldsVO pvo, ArrayList arrNewList)
		{
			int intRet = -1;

			try
			{
				foreach (sys_ReportFieldsVO vo in arrNewList)
				{
					if (vo.ReportID.Equals(pvo.ReportID) && vo.FieldName.Equals(pvo.FieldName))
					{
						intRet = arrNewList.IndexOf(vo);
						break;
					}
				}
			}
			catch
			{
				intRet = -1;
			}

			return intRet;
		}

		/// <summary>
		/// Center all column caption in data grid
		/// </summary>
		private void LayoutGrid()
		{
			foreach (C1DisplayColumn dcolLayout in gridReportData.Splits[0].DisplayColumns)
			{
				dcolLayout.HeadingStyle.HorizontalAlignment = AlignHorzEnum.Center;
				dcolLayout.Locked = true;
				Type type = mtblReportData.Columns[dcolLayout.DataColumn.DataField].DataType;
				if (type.Equals(typeof(DateTime)))
				{
					dcolLayout.Style.HorizontalAlignment = AlignHorzEnum.Center;
					dcolLayout.DataColumn.NumberFormat = Constants.DATETIME_FORMAT_HOUR;
				}
				else if (type.Equals(typeof(string)))
					dcolLayout.Style.HorizontalAlignment = AlignHorzEnum.Near;
				else if (type.Equals(typeof(bool)))
					dcolLayout.Style.HorizontalAlignment = AlignHorzEnum.Center;
				else
				{
					dcolLayout.Style.HorizontalAlignment = AlignHorzEnum.Far;
					if (type.Equals(typeof(decimal)))
						dcolLayout.DataColumn.NumberFormat = Constants.DECIMAL_NUMBERFORMAT;
					else if (type.Equals(typeof(int)))
						dcolLayout.DataColumn.NumberFormat = Constants.INTERGER_NUMBERFORMAT;
				}
			}
		}

		/// <summary>
		/// Suft the Panel, return the Control in which has Name == pstrName		
		/// </summary>
		/// <exception cref="">ArgumentException</exception>
		/// <param name="ppnl">Panel to find</param>
		/// <param name="pstrControlName">Control Name to get</param>
		/// <returns>Control if found. Else return null</returns>
		private Control GetControlFromPanel(Panel ppnl, string pstrControlName)
		{
			foreach (Control objControl in ppnl.Controls)
			{
				if (objControl.Name.ToUpper().Equals(pstrControlName.ToUpper()))
				{
					return objControl;
				}
			}
			throw new ArgumentException(pstrControlName);
		}

		/// <summary>
		/// Find all parameters that reference to a Parameter
		/// </summary>
		/// <param name="pstrParamName">Referenced Parameter Name</param>
		/// <returns>List of parameters that reference to a Parameter</returns>
		private ArrayList FindReferenceParameter(string pstrParamName)
		{
			ArrayList arrResult = new ArrayList();
			foreach (sys_ReportParaVO voParam in arrParams)
			{
				// analyze the where clause of parameter in order to find the refenrence
				if (voParam.WhereClause.Trim() != string.Empty)
				{
					// check the parameter is reference or not
					if (IsReference(voParam.WhereClause, pstrParamName))
						arrResult.Add(voParam);
				}
			}
			arrResult.TrimToSize();
			return arrResult;
		}

		/// <summary>
		/// Check if parameter is referenced in where clause
		/// </summary>
		/// <param name="pstrWhereClause">Where Clause</param>
		/// <param name="pstrParamName">Parameter to check</param>
		/// <returns>true if match, false if failure</returns>
		private bool IsReference(string pstrWhereClause, string pstrParamName)
		{
			string strPattern = @"(?<fieldGR>(\s*)((\[|)(\w*)(\]|)(\.|)(\[|)(\w*)(\]|))(\s*))(?<operatorGR>((\s)like(\s))|=|>|<|>=|<=|<>)(\s*)(?<valueGR>" + pstrParamName + @")(\s+)";
			Match objMatch = Regex.Match(pstrWhereClause + " ", strPattern, RegexOptions.IgnoreCase);
			return objMatch.Success;
		}

		/// <summary>
		/// Clear data on form of parameter
		/// </summary>
		/// <param name="pvoParameter">Parameter to clear</param>
		private void ClearDataOfParameter(sys_ReportParaVO pvoParameter)
		{
			// now try to find the reference parameter in order to clear the data of its
			ArrayList arrReference = FindReferenceParameter(pvoParameter.ParaName);
			foreach (sys_ReportParaVO voReference in arrReference)
				ClearDataOfParameter(voReference);
			try
			{
				((C1DateEdit) GetControlFromPanel(this.pnlParameters, DATEPICKER_PREFIX + pvoParameter.ParaName)).Value = DBNull.Value;
			}
			catch{}
			try
			{
				((ComboBox) GetControlFromPanel(this.pnlParameters, COMBO_PREFIX + pvoParameter.ParaName)).SelectedIndex = -1;
			}
			catch{}
			try
			{
				GetControlFromPanel(this.pnlParameters, TEXTBOX_KEYFIELD_PREFIX + pvoParameter.ParaName + pvoParameter.FromField).Text = string.Empty;
			}
			catch{}
			try
			{
				GetControlFromPanel(this.pnlParameters, TEXTBOX_FILTER1_PREFIX + pvoParameter.ParaName + pvoParameter.FilterField1).Text = string.Empty;
			}
			catch{}
			try
			{
				GetControlFromPanel(this.pnlParameters, TEXTBOX_FILTER2_PREFIX + pvoParameter.ParaName + pvoParameter.FilterField2).Text = string.Empty;
			}
			catch{}
		}

		/// <summary>
		/// Find and replace the reference parameter with its value on the form
		/// </summary>
		/// <history>Thachnn: 28/12/2005: fix bug: when parameter is string, 
		/// put single quote to the parameter value while find and replace parameter</history>
		/// <param name="pvoParam">Select Param</param>
		/// <returns>New where clause</returns>
		private string FindAndReplaceReferenceParameter(sys_ReportParaVO pvoParam)
		{
			string strWhereClause = FormControlComponents.StardandizeSQL(pvoParam.WhereClause);
			// we analyze the where clause to get the constraint
			if (pvoParam.WhereClause != string.Empty)
			{
				// find the field name and the parameter
				string strPattern = @"(?<fieldGR>(\s*)((\[|)(\w*)(\]|)(\.|)(\[|)(\w*)(\]|))(\s*))"
					+ @"(?<operatorGR>((\s)in(\s))|((\s)like(\s))|=|>|<|>=|<=|<>)(\s*)"
					+ @"(?<valueGR>\(@(\w+)\)|@(\w+))(\s+)";
//				string strPattern = @"(?<fieldGR>(\s*)((\[|)(\w*)(\]|)(\.|)(\[|)(\w*)(\]|))(\s*))"
//					+ @"(?<operatorGR>((\s)in(\s))|((\s)like(\s))|=|>|<|>=|<=|<>)(\s*)"
//					+ @"(?<valueGR>\(" + pvoParam.ParaName + @"\)|" + pvoParam.ParaName +  @")(\s+)";
				MatchCollection objMatches = Regex.Matches(pvoParam.WhereClause + " ", strPattern, RegexOptions.IgnoreCase);
				foreach (Match objMatch in objMatches)
				{
					if (objMatch.Success)
					{
						string[] strFields = objMatch.Value.Trim().Split(" ".ToCharArray());
						string strParamName = string.Empty;
						if (strFields.Length > 1)
							strParamName = strFields[strFields.Length - 1].Trim();
						try
						{
							// find the param object from the name
							foreach (sys_ReportParaVO voRefParam in arrParams)
							{
								// found it
								if (voRefParam.ParaName.ToUpper().Equals(strParamName.ToUpper()))
								{
									if ((voRefParam.DataType != (int) Type.GetTypeCode(typeof (bool))) &&
										(voRefParam.DataType != (int) Type.GetTypeCode(typeof (DateTime))))
									{
										// get value from textbox
										// param get value from key field
										if (voRefParam.FromField != string.Empty)
										{
											TextBox txtField = (TextBox) GetControlFromPanel(this.pnlParameters, TEXTBOX_KEYFIELD_PREFIX + voRefParam.ParaName + voRefParam.FromField);
											string strValue = txtField.Text;
											string strTemp = string.Empty;
											if (voRefParam.DataType == (int)TypeCode.String)
											{
												if (strValue.Trim().Length > 0)
												{
													if (strValue.IndexOf(",") >= 0)
													{
														string[] strValues = strValue.Split(",".ToCharArray());
														foreach (string strVal in strValues)
															strTemp = strTemp + "'" + strVal + "',";
													}
													else
														strTemp = "'" + strValue + "'";
												}
												// remove the last ","
												if (strTemp != string.Empty && strTemp.IndexOf(",") >= 0)
													strTemp = strTemp.Substring(0, strTemp.Length - 1);
											}
											else
												strTemp = strValue;
											/// HACKED: Thachnn : 28/12/2005: add single quote to cover the textbox.Text
											if (voRefParam.MultiSelection)
												strWhereClause = FormControlComponents.FindAndReplaceParameterRegEx(strWhereClause, voRefParam.ParaName, strTemp, voRefParam.MultiSelection);
											else
												strWhereClause = FormControlComponents.FindAndReplaceParameterRegEx(strWhereClause, voRefParam.ParaName, "'" + txtField.Text.Trim() + "'", voRefParam.MultiSelection);
											/// ENDHACKED: Thachnn : 28/12/2005
										}
										else if (voRefParam.Items != string.Empty) // get value from combo box
										{
											ComboBox cboField = (ComboBox) GetControlFromPanel(this.pnlParameters, COMBO_PREFIX + voRefParam.ParaName);
											/// HACKED: Thachnn : 28/12/2005: add single quote to cover the replace value
											strWhereClause = FormControlComponents.FindAndReplaceParameterRegEx(strWhereClause, voRefParam.ParaName, "'" + cboField.SelectedItem.ToString() + "'", voRefParam.MultiSelection);
											/// ENDHACKED: Thachnn : 28/12/2005: add single quote to cover the replace value
										}
										else // get value from normal textbox
										{											
											TextBox txtField = (TextBox) GetControlFromPanel(this.pnlParameters, TEXTBOX_PREFIX + voRefParam.ParaName);
											/// HACKED: Thachnn : 28/12/2005: add single quote to cover the replace value
											strWhereClause = FormControlComponents.FindAndReplaceParameterRegEx(strWhereClause, voRefParam.ParaName, "'" + txtField.Text.Trim() + "'", voRefParam.MultiSelection);
											/// ENDHACKED: Thachnn : 28/12/2005: add single quote to cover the replace value
										}
									}
									else if (voRefParam.DataType == (int) Type.GetTypeCode(typeof (bool)))
									{
										// get value from checkbox
										CheckBox chkField = (CheckBox) GetControlFromPanel(this.pnlParameters, CHECKBOX_PREFIX + voRefParam.ParaName);
										strWhereClause = FormControlComponents.FindAndReplaceParameterRegEx(strWhereClause, voRefParam.ParaName, Convert.ToInt32(chkField.Checked).ToString(), voRefParam.MultiSelection);
									}
									else if (voRefParam.DataType == (int) Type.GetTypeCode(typeof (DateTime)))
									{
										// get value from C1 Datetime Edit
										C1DateEdit dtmField = (C1DateEdit) GetControlFromPanel(this.pnlParameters, DATEPICKER_PREFIX + voRefParam.ParaName);
										/// HACKED: Thachnn : 28/12/2005: add single quote to cover the replace value
										/// but I feel the PCS Program will not run here because param with type = DateTime will never care about the FromTable, FromField value of the parameterVO
										strWhereClause = FormControlComponents.FindAndReplaceParameterRegEx(strWhereClause, voRefParam.ParaName, "'" + dtmField.Value.ToString() + "'", voRefParam.MultiSelection);										
										/// HACKED: Thachnn : 28/12/2005: add single quote to cover the replace value
									}
									break;
								}
							}
						}
						catch{}
					}
				}
			}
			return strWhereClause;
		}

		/// <summary>
		/// Thachnn: 22/12/2005
		/// This function is used to determine the displaying control (on the ReportViewer form) is refer about CCN
		/// Other function will use this function to set the Default CCN value to this control
		/// 
		/// We think:
		/// CNN relate is something like: @CCN @CCNID		
		/// </summary>
		/// <authod>Thachnn</authod>
		/// <date>22/12/2005</date>
		/// <param name="pstrTextToFind">control to analyse</param>
		/// <returns>true if that control is about CCN</returns>
		public bool IsCCNParameter(string pstrTextToFind)
		{
			/// is CCN			/// 
			/// Find something like:
			/// Start with @
			/// follow by CCN
			/// follow by anything (can be null)
			/// Example matchs: @CCN  @CCNID 
			string strPattern = @"(?<AtSignGR>\@)(?<CCNGR>(CCN)(\w*))";
			if( System.Text.RegularExpressions.Regex.Match(pstrTextToFind, strPattern, System.Text.RegularExpressions.RegexOptions.IgnoreCase).Success )
			{
				return true;
			}
		
			/// not CCN
			return false;
		}

		/// <summary>
		/// Thachnn: 22/12/2005
		/// This function is used to determine the displaying control (on the ReportViewer form) is refer about  MasterLocation
		/// Other function will use this function to set the  Default MasterLocation value to this control
		/// 
		/// We think:		
		/// MasterLocation relate is something like: @MasterLocation, @MasterLocationID, @MasLoc, @MasLocID
		/// </summary>
		/// <authod>Thachnn</authod>
		/// <date>22/12/2005</date>
		/// <param name="pstrTextToFind">control to analyse</param>
		/// <returns>true if that control is about MasterLocation</returns>
		public bool IsMasterLocationParameter(string pstrTextToFind)
		{
			/// is Master Location	///
			/// Find something like:
			/// Start with @
			/// Follow by Mas...
			/// Follow by Loc...
			/// ... can be anything, can be null
			/// Example matchs:
			/// @MasLoc  @MasterLocation @MasLocID  @MasterLocationID
			string strPattern = @"(?<AtSignGR>\@)(?<MasterGR>(Mas)(\w*))(?<LocationGR>(Loc)(\w*))";
			if( System.Text.RegularExpressions.Regex.Match(pstrTextToFind, strPattern, System.Text.RegularExpressions.RegexOptions.IgnoreCase).Success )
			{
				return true;
			}

			/// not Master Location
			return false;
		}

		/// <summary>
		/// This function is used to determine the displaying control (on the ReportViewer form) is refer about Currency
		/// Other function will use this function to set the Default Currency value to this control
		
		/// We think:
		/// Currency relate is something like: @Currency @Currency
		/// </summary>
		/// <param name="pstrTextToFind">control to analyse</param>
		/// <returns>true if that control is about Currency</returns>
		public bool IsCurrencyParameter(string pstrTextToFind)
		{
			string strPattern = @"(?<AtSignGR>\@)(?<CurrencyGR>(Currency)(\w*))";
			if( System.Text.RegularExpressions.Regex.Match(pstrTextToFind, strPattern, System.Text.RegularExpressions.RegexOptions.IgnoreCase).Success )
			{
				return true;
			}
		
			return false;
		}

	    /// <summary>		/// 
	    /// Set default value for CCN or Master Location relate parameter 
	    /// Auto  fill the default CCNID , CCN Code, MasLocID, MasLocCode to the Parameter if any
	    /// this mechanism will make Report Viewer operate not correctly when CNN or MasterLocation config 
	    /// </summary>
	    private void SetDefaultParameter()
		{			
			foreach(sys_ReportParaVO voParam  in  arrParams)
			{
				#region if  parameter is CCN
				if (	 //IsCCNParameter(voParam.ParaName) // not check name constraint
					 voParam.DefaultValue == string.Empty 
					&& voParam.FromTable == MST_CCNTable.TABLE_NAME
					&& voParam.FromField == MST_CCNTable.CCNID_FLD					
					)
				{
					TextBox txtKeyField = new TextBox();
					TextBox txtFilterField1 = new TextBox();
					TextBox txtFilterField2 = new TextBox();
				
					try
					{
						txtKeyField = (TextBox) GetControlFromPanel(this.pnlParameters, TEXTBOX_KEYFIELD_PREFIX + voParam.ParaName + voParam.FromField);						
					}
					catch{	}
					try
					{
						txtFilterField1 = (TextBox) GetControlFromPanel(this.pnlParameters, TEXTBOX_FILTER1_PREFIX + voParam.ParaName + voParam.FilterField1);				
					}
					catch{	}
					try
					{
						txtFilterField2 = (TextBox) GetControlFromPanel(this.pnlParameters, TEXTBOX_FILTER2_PREFIX + voParam.ParaName + voParam.FilterField2);					
					}
					catch{	}
                    

					try
					{
						txtKeyField.Text = SystemProperty.CCNID.ToString();
					}
					catch{	}

					if(voParam.FilterField1 == MST_CCNTable.CODE_FLD)
					{
						try
						{
							txtFilterField1.Text = SystemProperty.CCNCode;
						}
						catch{	}

						if(voParam.FilterField2 == MST_CCNTable.DESCRIPTION_FLD )
						{
							try
							{
								txtFilterField2.Text = SystemProperty.CCNDescription;
							}
							catch{	}
						}
					}	// end filter filed1 = code

					else if(voParam.FilterField1 == MST_CCNTable.DESCRIPTION_FLD)
					{
						try
						{
							txtFilterField1.Text = SystemProperty.CCNDescription;
						}
						catch{	}					

						if(voParam.FilterField2 == MST_CCNTable.CODE_FLD )
						{							
							try
							{
								txtFilterField2.Text = SystemProperty.CCNCode;
							}
							catch{	}
						}					
					}	// end fileter field1 = description

				}
				#endregion if parameter is CCN

				#region if  parameter is MasterLocation
				if (	 //IsMasterLocationParameter(voParam.ParaName) // not check name constraint
					voParam.DefaultValue == string.Empty 
					&& voParam.FromTable == MST_MasterLocationTable.TABLE_NAME
					&& voParam.FromField == MST_MasterLocationTable.MASTERLOCATIONID_FLD					
					)
				{
					TextBox txtKeyField = new TextBox();
					TextBox txtFilterField1 = new TextBox();
					TextBox txtFilterField2 = new TextBox();
				
					try
					{
						txtKeyField = (TextBox) GetControlFromPanel(this.pnlParameters, TEXTBOX_KEYFIELD_PREFIX + voParam.ParaName + voParam.FromField);						
					}
					catch{	}
					try
					{
						txtFilterField1 = (TextBox) GetControlFromPanel(this.pnlParameters, TEXTBOX_FILTER1_PREFIX + voParam.ParaName + voParam.FilterField1);				
					}
					catch{	}
					try
					{
						txtFilterField2 = (TextBox) GetControlFromPanel(this.pnlParameters, TEXTBOX_FILTER2_PREFIX + voParam.ParaName + voParam.FilterField2);					
					}
					catch{	}
                    

					try
					{
						txtKeyField.Text = SystemProperty.MasterLocationID.ToString();
					}
					catch{	}

					if(voParam.FilterField1 == MST_MasterLocationTable.CODE_FLD)
					{
						try
						{
							txtFilterField1.Text = SystemProperty.MasterLocationCode;
						}
						catch{	}

						if(voParam.FilterField2 == MST_MasterLocationTable.NAME_FLD )
						{
							try
							{
								txtFilterField2.Text = SystemProperty.MasterLocationName;
							}
							catch{	}
						}
					}	// end filter filed1 = code

					else if(voParam.FilterField1 == MST_MasterLocationTable.NAME_FLD)
					{
						try
						{
							txtFilterField1.Text = SystemProperty.MasterLocationName;
						}
						catch{	}					

						if(voParam.FilterField2 == MST_MasterLocationTable.CODE_FLD )
						{							
							try
							{
								txtFilterField2.Text = SystemProperty.MasterLocationCode;
							}
							catch{	}
						}					
					}	// end fileter field1 = description

				}
				#endregion if parameter is MasterLocation
				
				#region if  parameter is Currency
				if (voParam.DefaultValue == string.Empty 
					&& voParam.FromTable == MST_CurrencyTable.TABLE_NAME
					&& voParam.FromField == MST_CurrencyTable.CURRENCYID_FLD					
					)
				{
					TextBox txtKeyField = new TextBox();
					TextBox txtFilterField1 = new TextBox();
					TextBox txtFilterField2 = new TextBox();
				
					try
					{
						txtKeyField = (TextBox) GetControlFromPanel(this.pnlParameters, TEXTBOX_KEYFIELD_PREFIX + voParam.ParaName + voParam.FromField);						
					}
					catch{	}
					try
					{
						txtFilterField1 = (TextBox) GetControlFromPanel(this.pnlParameters, TEXTBOX_FILTER1_PREFIX + voParam.ParaName + voParam.FilterField1);				
					}
					catch{	}
					try
					{
						txtFilterField2 = (TextBox) GetControlFromPanel(this.pnlParameters, TEXTBOX_FILTER2_PREFIX + voParam.ParaName + voParam.FilterField2);					
					}
					catch{	}
                    

					var boUtil = new UtilsBO();
					var voUSDCurrency = (MST_CurrencyVO)boUtil.GetUSDCurrency();
					try
					{
						txtKeyField.Text = voUSDCurrency.CurrencyID.ToString();
					}
					catch{	}

					if(voParam.FilterField1 == MST_CurrencyTable.CODE_FLD)
					{
						try
						{
							txtFilterField1.Text = voUSDCurrency.Code;
						}
						catch{	}

						if(voParam.FilterField2 == MST_CurrencyTable.NAME_FLD )
						{
							try
							{
								txtFilterField2.Text = voUSDCurrency.Name;
							}
							catch{	}
						}
					}	// end filter filed1 = code

					else if(voParam.FilterField1 == MST_CurrencyTable.NAME_FLD)
					{
						try
						{
							txtFilterField1.Text = voUSDCurrency.Name;
						}
						catch{	}					

						if(voParam.FilterField2 == MST_CurrencyTable.CODE_FLD )
						{							
							try
							{
								txtFilterField2.Text = voUSDCurrency.Code;
							}
							catch{	}
						}					
					}	// end fileter field1 = description

				}
				#endregion if parameter is Currency
			}			
			
		}

		private string BuildWhere(sys_ReportParaVO pvoParam)
		{
			string strWhereClause = string.Empty;
			// replace parameter name with its value on form
			string strParamValue = FindAndReplaceReferenceParameter(pvoParam).Trim();
			if (!string.IsNullOrEmpty(strParamValue))
				strWhereClause += " AND " + strParamValue;
			if (strWhereClause.Length > 0 && strWhereClause.IndexOf(" AND ") >= 0)
				strWhereClause = strWhereClause.Substring(" AND ".Length);
			return strWhereClause;
		}
		private void AddValueIntoComboBoxInTrueDBGrid(C1DataColumn c1dcDataColumn, string strFieldName) 
		{
			c1dcDataColumn.ValueItems.Presentation = PresentationEnum.ComboBox;


			ValueItem vi = new ValueItem();

			int intFirstRow = c1dcDataColumn.ValueItems.Values.Add(vi);
			c1dcDataColumn.ValueItems.Values[intFirstRow].DisplayValue = "ALL";
			c1dcDataColumn.ValueItems.Values[intFirstRow].Value = "";
			
			ArrayList arValue = new ArrayList();

            int intTotalRows = gridReportData.RowCount;
			
			for (int i=0; i<intTotalRows ; i++)
			{
				string strValue = String.Empty;
					
				if (gridReportData.Columns[strFieldName].CellValue(i) != DBNull.Value && gridReportData.Columns[strFieldName].CellValue(i).ToString() != String.Empty)
					strValue = gridReportData.Columns[strFieldName].CellText(i);
					
				if (strValue == String.Empty) 
					continue;

				if (arValue.IndexOf(strValue) < 0)
					arValue.Add(strValue);
			}

			arValue.Sort();
			IEnumerator myEnumerator = arValue.GetEnumerator();
			while ( myEnumerator.MoveNext() )
			{
				vi = new ValueItem(myEnumerator.Current.ToString(), myEnumerator.Current);
				c1dcDataColumn.ValueItems.Values.Add(vi);
			}
		}
		#endregion
	}
}