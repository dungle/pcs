using System;
using System.Collections;
using System.Data;
using System.Windows.Forms;
using PCSComUtils.Common;
using PCSComUtils.Common.BO;
using PCSComUtils.Framework.ReportFrame.BO;
using PCSComUtils.Framework.ReportFrame.DS;
using PCSComUtils.PCSExc;
using PCSUtils.Log;
using PCSUtils.Utils;

namespace PCSUtils.Framework.ReportFrame
{
	/// <summary>
	/// Summary description for Report.
	/// </summary>
	public class ReportManagement : Form
	{
		private Button btnRefreshTreeView;
		private Button btnShowLastReport;
		private Button btnDeleteReport;
		private Button btnDeleteGroup;
		private Button btnMoveDown;
		private Button btnMoveUp;
		private Button btnEditReport;
		private Button btnCopyReport;
		private Button btnPasteReport;
		private Button btnAddGroup;
		private Button btnEditGroup;
		private Button btnCopyGroup;
		private Button btnAddReport;
		private Button btnViewReport;
		private Label lblReportList;
		private TreeView tvwReportList;

		private const string THIS = "PCSUtils.Framework.ReportFrame.ReportManagement";
		private const string CODE_DATE_FORMAT = "yyyyMMddHHmmssfff";

		private ReportManagementBO boReportManagement = new ReportManagementBO();
		private EditReportBO boEditReport = new EditReportBO();
		private Button btnClose;
		private UtilsBO boUtils = new UtilsBO();
		private System.Windows.Forms.ImageList imglTreeNode;
		private System.ComponentModel.IContainer components;
		private sys_ReportGroupVO voSelectedGroup;
		private sys_ReportVO voSelectedReport;
		private System.Windows.Forms.Label lblShortCutKeys;
		private System.Windows.Forms.Label lblF1;
		private System.Windows.Forms.Label lblAddGroup;
		private System.Windows.Forms.Label lblShowLast;

		#region Thachnn Trace Node Processing

		//private TreeNode UpWall;
		//private TreeNode DownWall;
		private TreeNode Wall;

		private void InitTraceNode()
		{
			Wall = new TreeNode();
            Wall.Tag = null;
		}
		#endregion
		
		//**************************************************************************              
		///    <Description>
		///       Defaut constructor.
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
		public ReportManagement()
		{
			const string METHOD_NAME = THIS + ".ReportManagement()";

			InitializeComponent();
			try
			{
				InitTraceNode();
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
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(ReportManagement));
			this.lblShortCutKeys = new System.Windows.Forms.Label();
			this.btnRefreshTreeView = new System.Windows.Forms.Button();
			this.btnShowLastReport = new System.Windows.Forms.Button();
			this.btnDeleteReport = new System.Windows.Forms.Button();
			this.btnDeleteGroup = new System.Windows.Forms.Button();
			this.btnMoveDown = new System.Windows.Forms.Button();
			this.btnMoveUp = new System.Windows.Forms.Button();
			this.btnEditReport = new System.Windows.Forms.Button();
			this.btnCopyReport = new System.Windows.Forms.Button();
			this.btnPasteReport = new System.Windows.Forms.Button();
			this.btnAddGroup = new System.Windows.Forms.Button();
			this.btnEditGroup = new System.Windows.Forms.Button();
			this.btnCopyGroup = new System.Windows.Forms.Button();
			this.btnAddReport = new System.Windows.Forms.Button();
			this.btnViewReport = new System.Windows.Forms.Button();
			this.tvwReportList = new System.Windows.Forms.TreeView();
			this.imglTreeNode = new System.Windows.Forms.ImageList(this.components);
			this.lblReportList = new System.Windows.Forms.Label();
			this.btnClose = new System.Windows.Forms.Button();
			this.lblF1 = new System.Windows.Forms.Label();
			this.lblAddGroup = new System.Windows.Forms.Label();
			this.lblShowLast = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// lblShortCutKeys
			// 
			this.lblShortCutKeys.AccessibleDescription = resources.GetString("lblShortCutKeys.AccessibleDescription");
			this.lblShortCutKeys.AccessibleName = resources.GetString("lblShortCutKeys.AccessibleName");
			this.lblShortCutKeys.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblShortCutKeys.Anchor")));
			this.lblShortCutKeys.AutoSize = ((bool)(resources.GetObject("lblShortCutKeys.AutoSize")));
			this.lblShortCutKeys.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.lblShortCutKeys.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblShortCutKeys.Dock")));
			this.lblShortCutKeys.Enabled = ((bool)(resources.GetObject("lblShortCutKeys.Enabled")));
			this.lblShortCutKeys.Font = ((System.Drawing.Font)(resources.GetObject("lblShortCutKeys.Font")));
			this.lblShortCutKeys.Image = ((System.Drawing.Image)(resources.GetObject("lblShortCutKeys.Image")));
			this.lblShortCutKeys.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblShortCutKeys.ImageAlign")));
			this.lblShortCutKeys.ImageIndex = ((int)(resources.GetObject("lblShortCutKeys.ImageIndex")));
			this.lblShortCutKeys.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblShortCutKeys.ImeMode")));
			this.lblShortCutKeys.Location = ((System.Drawing.Point)(resources.GetObject("lblShortCutKeys.Location")));
			this.lblShortCutKeys.Name = "lblShortCutKeys";
			this.lblShortCutKeys.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblShortCutKeys.RightToLeft")));
			this.lblShortCutKeys.Size = ((System.Drawing.Size)(resources.GetObject("lblShortCutKeys.Size")));
			this.lblShortCutKeys.TabIndex = ((int)(resources.GetObject("lblShortCutKeys.TabIndex")));
			this.lblShortCutKeys.Text = resources.GetString("lblShortCutKeys.Text");
			this.lblShortCutKeys.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblShortCutKeys.TextAlign")));
			this.lblShortCutKeys.Visible = ((bool)(resources.GetObject("lblShortCutKeys.Visible")));
			// 
			// btnRefreshTreeView
			// 
			this.btnRefreshTreeView.AccessibleDescription = resources.GetString("btnRefreshTreeView.AccessibleDescription");
			this.btnRefreshTreeView.AccessibleName = resources.GetString("btnRefreshTreeView.AccessibleName");
			this.btnRefreshTreeView.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnRefreshTreeView.Anchor")));
			this.btnRefreshTreeView.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnRefreshTreeView.BackgroundImage")));
			this.btnRefreshTreeView.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnRefreshTreeView.Dock")));
			this.btnRefreshTreeView.Enabled = ((bool)(resources.GetObject("btnRefreshTreeView.Enabled")));
			this.btnRefreshTreeView.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnRefreshTreeView.FlatStyle")));
			this.btnRefreshTreeView.Font = ((System.Drawing.Font)(resources.GetObject("btnRefreshTreeView.Font")));
			this.btnRefreshTreeView.Image = ((System.Drawing.Image)(resources.GetObject("btnRefreshTreeView.Image")));
			this.btnRefreshTreeView.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnRefreshTreeView.ImageAlign")));
			this.btnRefreshTreeView.ImageIndex = ((int)(resources.GetObject("btnRefreshTreeView.ImageIndex")));
			this.btnRefreshTreeView.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnRefreshTreeView.ImeMode")));
			this.btnRefreshTreeView.Location = ((System.Drawing.Point)(resources.GetObject("btnRefreshTreeView.Location")));
			this.btnRefreshTreeView.Name = "btnRefreshTreeView";
			this.btnRefreshTreeView.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnRefreshTreeView.RightToLeft")));
			this.btnRefreshTreeView.Size = ((System.Drawing.Size)(resources.GetObject("btnRefreshTreeView.Size")));
			this.btnRefreshTreeView.TabIndex = ((int)(resources.GetObject("btnRefreshTreeView.TabIndex")));
			this.btnRefreshTreeView.Text = resources.GetString("btnRefreshTreeView.Text");
			this.btnRefreshTreeView.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnRefreshTreeView.TextAlign")));
			this.btnRefreshTreeView.Visible = ((bool)(resources.GetObject("btnRefreshTreeView.Visible")));
			this.btnRefreshTreeView.Click += new System.EventHandler(this.btnRefreshTreeView_Click);
			// 
			// btnShowLastReport
			// 
			this.btnShowLastReport.AccessibleDescription = resources.GetString("btnShowLastReport.AccessibleDescription");
			this.btnShowLastReport.AccessibleName = resources.GetString("btnShowLastReport.AccessibleName");
			this.btnShowLastReport.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnShowLastReport.Anchor")));
			this.btnShowLastReport.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnShowLastReport.BackgroundImage")));
			this.btnShowLastReport.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnShowLastReport.Dock")));
			this.btnShowLastReport.Enabled = ((bool)(resources.GetObject("btnShowLastReport.Enabled")));
			this.btnShowLastReport.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnShowLastReport.FlatStyle")));
			this.btnShowLastReport.Font = ((System.Drawing.Font)(resources.GetObject("btnShowLastReport.Font")));
			this.btnShowLastReport.Image = ((System.Drawing.Image)(resources.GetObject("btnShowLastReport.Image")));
			this.btnShowLastReport.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnShowLastReport.ImageAlign")));
			this.btnShowLastReport.ImageIndex = ((int)(resources.GetObject("btnShowLastReport.ImageIndex")));
			this.btnShowLastReport.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnShowLastReport.ImeMode")));
			this.btnShowLastReport.Location = ((System.Drawing.Point)(resources.GetObject("btnShowLastReport.Location")));
			this.btnShowLastReport.Name = "btnShowLastReport";
			this.btnShowLastReport.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnShowLastReport.RightToLeft")));
			this.btnShowLastReport.Size = ((System.Drawing.Size)(resources.GetObject("btnShowLastReport.Size")));
			this.btnShowLastReport.TabIndex = ((int)(resources.GetObject("btnShowLastReport.TabIndex")));
			this.btnShowLastReport.Text = resources.GetString("btnShowLastReport.Text");
			this.btnShowLastReport.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnShowLastReport.TextAlign")));
			this.btnShowLastReport.Visible = ((bool)(resources.GetObject("btnShowLastReport.Visible")));
			this.btnShowLastReport.Click += new System.EventHandler(this.btnShowLastReport_Click);
			// 
			// btnDeleteReport
			// 
			this.btnDeleteReport.AccessibleDescription = resources.GetString("btnDeleteReport.AccessibleDescription");
			this.btnDeleteReport.AccessibleName = resources.GetString("btnDeleteReport.AccessibleName");
			this.btnDeleteReport.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnDeleteReport.Anchor")));
			this.btnDeleteReport.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnDeleteReport.BackgroundImage")));
			this.btnDeleteReport.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnDeleteReport.Dock")));
			this.btnDeleteReport.Enabled = ((bool)(resources.GetObject("btnDeleteReport.Enabled")));
			this.btnDeleteReport.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnDeleteReport.FlatStyle")));
			this.btnDeleteReport.Font = ((System.Drawing.Font)(resources.GetObject("btnDeleteReport.Font")));
			this.btnDeleteReport.Image = ((System.Drawing.Image)(resources.GetObject("btnDeleteReport.Image")));
			this.btnDeleteReport.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnDeleteReport.ImageAlign")));
			this.btnDeleteReport.ImageIndex = ((int)(resources.GetObject("btnDeleteReport.ImageIndex")));
			this.btnDeleteReport.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnDeleteReport.ImeMode")));
			this.btnDeleteReport.Location = ((System.Drawing.Point)(resources.GetObject("btnDeleteReport.Location")));
			this.btnDeleteReport.Name = "btnDeleteReport";
			this.btnDeleteReport.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnDeleteReport.RightToLeft")));
			this.btnDeleteReport.Size = ((System.Drawing.Size)(resources.GetObject("btnDeleteReport.Size")));
			this.btnDeleteReport.TabIndex = ((int)(resources.GetObject("btnDeleteReport.TabIndex")));
			this.btnDeleteReport.Text = resources.GetString("btnDeleteReport.Text");
			this.btnDeleteReport.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnDeleteReport.TextAlign")));
			this.btnDeleteReport.Visible = ((bool)(resources.GetObject("btnDeleteReport.Visible")));
			this.btnDeleteReport.Click += new System.EventHandler(this.btnDeleteReport_Click);
			// 
			// btnDeleteGroup
			// 
			this.btnDeleteGroup.AccessibleDescription = resources.GetString("btnDeleteGroup.AccessibleDescription");
			this.btnDeleteGroup.AccessibleName = resources.GetString("btnDeleteGroup.AccessibleName");
			this.btnDeleteGroup.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnDeleteGroup.Anchor")));
			this.btnDeleteGroup.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnDeleteGroup.BackgroundImage")));
			this.btnDeleteGroup.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnDeleteGroup.Dock")));
			this.btnDeleteGroup.Enabled = ((bool)(resources.GetObject("btnDeleteGroup.Enabled")));
			this.btnDeleteGroup.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnDeleteGroup.FlatStyle")));
			this.btnDeleteGroup.Font = ((System.Drawing.Font)(resources.GetObject("btnDeleteGroup.Font")));
			this.btnDeleteGroup.Image = ((System.Drawing.Image)(resources.GetObject("btnDeleteGroup.Image")));
			this.btnDeleteGroup.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnDeleteGroup.ImageAlign")));
			this.btnDeleteGroup.ImageIndex = ((int)(resources.GetObject("btnDeleteGroup.ImageIndex")));
			this.btnDeleteGroup.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnDeleteGroup.ImeMode")));
			this.btnDeleteGroup.Location = ((System.Drawing.Point)(resources.GetObject("btnDeleteGroup.Location")));
			this.btnDeleteGroup.Name = "btnDeleteGroup";
			this.btnDeleteGroup.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnDeleteGroup.RightToLeft")));
			this.btnDeleteGroup.Size = ((System.Drawing.Size)(resources.GetObject("btnDeleteGroup.Size")));
			this.btnDeleteGroup.TabIndex = ((int)(resources.GetObject("btnDeleteGroup.TabIndex")));
			this.btnDeleteGroup.Text = resources.GetString("btnDeleteGroup.Text");
			this.btnDeleteGroup.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnDeleteGroup.TextAlign")));
			this.btnDeleteGroup.Visible = ((bool)(resources.GetObject("btnDeleteGroup.Visible")));
			this.btnDeleteGroup.Click += new System.EventHandler(this.btnDeleteGroup_Click);
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
			// btnEditReport
			// 
			this.btnEditReport.AccessibleDescription = resources.GetString("btnEditReport.AccessibleDescription");
			this.btnEditReport.AccessibleName = resources.GetString("btnEditReport.AccessibleName");
			this.btnEditReport.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnEditReport.Anchor")));
			this.btnEditReport.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnEditReport.BackgroundImage")));
			this.btnEditReport.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnEditReport.Dock")));
			this.btnEditReport.Enabled = ((bool)(resources.GetObject("btnEditReport.Enabled")));
			this.btnEditReport.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnEditReport.FlatStyle")));
			this.btnEditReport.Font = ((System.Drawing.Font)(resources.GetObject("btnEditReport.Font")));
			this.btnEditReport.Image = ((System.Drawing.Image)(resources.GetObject("btnEditReport.Image")));
			this.btnEditReport.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnEditReport.ImageAlign")));
			this.btnEditReport.ImageIndex = ((int)(resources.GetObject("btnEditReport.ImageIndex")));
			this.btnEditReport.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnEditReport.ImeMode")));
			this.btnEditReport.Location = ((System.Drawing.Point)(resources.GetObject("btnEditReport.Location")));
			this.btnEditReport.Name = "btnEditReport";
			this.btnEditReport.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnEditReport.RightToLeft")));
			this.btnEditReport.Size = ((System.Drawing.Size)(resources.GetObject("btnEditReport.Size")));
			this.btnEditReport.TabIndex = ((int)(resources.GetObject("btnEditReport.TabIndex")));
			this.btnEditReport.Text = resources.GetString("btnEditReport.Text");
			this.btnEditReport.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnEditReport.TextAlign")));
			this.btnEditReport.Visible = ((bool)(resources.GetObject("btnEditReport.Visible")));
			this.btnEditReport.Click += new System.EventHandler(this.btnEditReport_Click);
			// 
			// btnCopyReport
			// 
			this.btnCopyReport.AccessibleDescription = resources.GetString("btnCopyReport.AccessibleDescription");
			this.btnCopyReport.AccessibleName = resources.GetString("btnCopyReport.AccessibleName");
			this.btnCopyReport.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnCopyReport.Anchor")));
			this.btnCopyReport.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCopyReport.BackgroundImage")));
			this.btnCopyReport.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnCopyReport.Dock")));
			this.btnCopyReport.Enabled = ((bool)(resources.GetObject("btnCopyReport.Enabled")));
			this.btnCopyReport.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnCopyReport.FlatStyle")));
			this.btnCopyReport.Font = ((System.Drawing.Font)(resources.GetObject("btnCopyReport.Font")));
			this.btnCopyReport.Image = ((System.Drawing.Image)(resources.GetObject("btnCopyReport.Image")));
			this.btnCopyReport.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnCopyReport.ImageAlign")));
			this.btnCopyReport.ImageIndex = ((int)(resources.GetObject("btnCopyReport.ImageIndex")));
			this.btnCopyReport.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnCopyReport.ImeMode")));
			this.btnCopyReport.Location = ((System.Drawing.Point)(resources.GetObject("btnCopyReport.Location")));
			this.btnCopyReport.Name = "btnCopyReport";
			this.btnCopyReport.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnCopyReport.RightToLeft")));
			this.btnCopyReport.Size = ((System.Drawing.Size)(resources.GetObject("btnCopyReport.Size")));
			this.btnCopyReport.TabIndex = ((int)(resources.GetObject("btnCopyReport.TabIndex")));
			this.btnCopyReport.Text = resources.GetString("btnCopyReport.Text");
			this.btnCopyReport.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnCopyReport.TextAlign")));
			this.btnCopyReport.Visible = ((bool)(resources.GetObject("btnCopyReport.Visible")));
			this.btnCopyReport.Click += new System.EventHandler(this.btnCopyReport_Click);
			// 
			// btnPasteReport
			// 
			this.btnPasteReport.AccessibleDescription = resources.GetString("btnPasteReport.AccessibleDescription");
			this.btnPasteReport.AccessibleName = resources.GetString("btnPasteReport.AccessibleName");
			this.btnPasteReport.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnPasteReport.Anchor")));
			this.btnPasteReport.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnPasteReport.BackgroundImage")));
			this.btnPasteReport.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnPasteReport.Dock")));
			this.btnPasteReport.Enabled = ((bool)(resources.GetObject("btnPasteReport.Enabled")));
			this.btnPasteReport.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnPasteReport.FlatStyle")));
			this.btnPasteReport.Font = ((System.Drawing.Font)(resources.GetObject("btnPasteReport.Font")));
			this.btnPasteReport.Image = ((System.Drawing.Image)(resources.GetObject("btnPasteReport.Image")));
			this.btnPasteReport.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnPasteReport.ImageAlign")));
			this.btnPasteReport.ImageIndex = ((int)(resources.GetObject("btnPasteReport.ImageIndex")));
			this.btnPasteReport.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnPasteReport.ImeMode")));
			this.btnPasteReport.Location = ((System.Drawing.Point)(resources.GetObject("btnPasteReport.Location")));
			this.btnPasteReport.Name = "btnPasteReport";
			this.btnPasteReport.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnPasteReport.RightToLeft")));
			this.btnPasteReport.Size = ((System.Drawing.Size)(resources.GetObject("btnPasteReport.Size")));
			this.btnPasteReport.TabIndex = ((int)(resources.GetObject("btnPasteReport.TabIndex")));
			this.btnPasteReport.Text = resources.GetString("btnPasteReport.Text");
			this.btnPasteReport.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnPasteReport.TextAlign")));
			this.btnPasteReport.Visible = ((bool)(resources.GetObject("btnPasteReport.Visible")));
			this.btnPasteReport.Click += new System.EventHandler(this.btnPasteReport_Click);
			// 
			// btnAddGroup
			// 
			this.btnAddGroup.AccessibleDescription = resources.GetString("btnAddGroup.AccessibleDescription");
			this.btnAddGroup.AccessibleName = resources.GetString("btnAddGroup.AccessibleName");
			this.btnAddGroup.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnAddGroup.Anchor")));
			this.btnAddGroup.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnAddGroup.BackgroundImage")));
			this.btnAddGroup.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnAddGroup.Dock")));
			this.btnAddGroup.Enabled = ((bool)(resources.GetObject("btnAddGroup.Enabled")));
			this.btnAddGroup.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnAddGroup.FlatStyle")));
			this.btnAddGroup.Font = ((System.Drawing.Font)(resources.GetObject("btnAddGroup.Font")));
			this.btnAddGroup.Image = ((System.Drawing.Image)(resources.GetObject("btnAddGroup.Image")));
			this.btnAddGroup.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnAddGroup.ImageAlign")));
			this.btnAddGroup.ImageIndex = ((int)(resources.GetObject("btnAddGroup.ImageIndex")));
			this.btnAddGroup.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnAddGroup.ImeMode")));
			this.btnAddGroup.Location = ((System.Drawing.Point)(resources.GetObject("btnAddGroup.Location")));
			this.btnAddGroup.Name = "btnAddGroup";
			this.btnAddGroup.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnAddGroup.RightToLeft")));
			this.btnAddGroup.Size = ((System.Drawing.Size)(resources.GetObject("btnAddGroup.Size")));
			this.btnAddGroup.TabIndex = ((int)(resources.GetObject("btnAddGroup.TabIndex")));
			this.btnAddGroup.Text = resources.GetString("btnAddGroup.Text");
			this.btnAddGroup.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnAddGroup.TextAlign")));
			this.btnAddGroup.Visible = ((bool)(resources.GetObject("btnAddGroup.Visible")));
			this.btnAddGroup.Click += new System.EventHandler(this.btnAddGroup_Click);
			// 
			// btnEditGroup
			// 
			this.btnEditGroup.AccessibleDescription = resources.GetString("btnEditGroup.AccessibleDescription");
			this.btnEditGroup.AccessibleName = resources.GetString("btnEditGroup.AccessibleName");
			this.btnEditGroup.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnEditGroup.Anchor")));
			this.btnEditGroup.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnEditGroup.BackgroundImage")));
			this.btnEditGroup.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnEditGroup.Dock")));
			this.btnEditGroup.Enabled = ((bool)(resources.GetObject("btnEditGroup.Enabled")));
			this.btnEditGroup.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnEditGroup.FlatStyle")));
			this.btnEditGroup.Font = ((System.Drawing.Font)(resources.GetObject("btnEditGroup.Font")));
			this.btnEditGroup.Image = ((System.Drawing.Image)(resources.GetObject("btnEditGroup.Image")));
			this.btnEditGroup.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnEditGroup.ImageAlign")));
			this.btnEditGroup.ImageIndex = ((int)(resources.GetObject("btnEditGroup.ImageIndex")));
			this.btnEditGroup.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnEditGroup.ImeMode")));
			this.btnEditGroup.Location = ((System.Drawing.Point)(resources.GetObject("btnEditGroup.Location")));
			this.btnEditGroup.Name = "btnEditGroup";
			this.btnEditGroup.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnEditGroup.RightToLeft")));
			this.btnEditGroup.Size = ((System.Drawing.Size)(resources.GetObject("btnEditGroup.Size")));
			this.btnEditGroup.TabIndex = ((int)(resources.GetObject("btnEditGroup.TabIndex")));
			this.btnEditGroup.Text = resources.GetString("btnEditGroup.Text");
			this.btnEditGroup.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnEditGroup.TextAlign")));
			this.btnEditGroup.Visible = ((bool)(resources.GetObject("btnEditGroup.Visible")));
			this.btnEditGroup.Click += new System.EventHandler(this.btnEditGroup_Click);
			// 
			// btnCopyGroup
			// 
			this.btnCopyGroup.AccessibleDescription = resources.GetString("btnCopyGroup.AccessibleDescription");
			this.btnCopyGroup.AccessibleName = resources.GetString("btnCopyGroup.AccessibleName");
			this.btnCopyGroup.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnCopyGroup.Anchor")));
			this.btnCopyGroup.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCopyGroup.BackgroundImage")));
			this.btnCopyGroup.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnCopyGroup.Dock")));
			this.btnCopyGroup.Enabled = ((bool)(resources.GetObject("btnCopyGroup.Enabled")));
			this.btnCopyGroup.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnCopyGroup.FlatStyle")));
			this.btnCopyGroup.Font = ((System.Drawing.Font)(resources.GetObject("btnCopyGroup.Font")));
			this.btnCopyGroup.Image = ((System.Drawing.Image)(resources.GetObject("btnCopyGroup.Image")));
			this.btnCopyGroup.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnCopyGroup.ImageAlign")));
			this.btnCopyGroup.ImageIndex = ((int)(resources.GetObject("btnCopyGroup.ImageIndex")));
			this.btnCopyGroup.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnCopyGroup.ImeMode")));
			this.btnCopyGroup.Location = ((System.Drawing.Point)(resources.GetObject("btnCopyGroup.Location")));
			this.btnCopyGroup.Name = "btnCopyGroup";
			this.btnCopyGroup.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnCopyGroup.RightToLeft")));
			this.btnCopyGroup.Size = ((System.Drawing.Size)(resources.GetObject("btnCopyGroup.Size")));
			this.btnCopyGroup.TabIndex = ((int)(resources.GetObject("btnCopyGroup.TabIndex")));
			this.btnCopyGroup.Text = resources.GetString("btnCopyGroup.Text");
			this.btnCopyGroup.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnCopyGroup.TextAlign")));
			this.btnCopyGroup.Visible = ((bool)(resources.GetObject("btnCopyGroup.Visible")));
			this.btnCopyGroup.Click += new System.EventHandler(this.btnCopyGroup_Click);
			// 
			// btnAddReport
			// 
			this.btnAddReport.AccessibleDescription = resources.GetString("btnAddReport.AccessibleDescription");
			this.btnAddReport.AccessibleName = resources.GetString("btnAddReport.AccessibleName");
			this.btnAddReport.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnAddReport.Anchor")));
			this.btnAddReport.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnAddReport.BackgroundImage")));
			this.btnAddReport.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnAddReport.Dock")));
			this.btnAddReport.Enabled = ((bool)(resources.GetObject("btnAddReport.Enabled")));
			this.btnAddReport.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnAddReport.FlatStyle")));
			this.btnAddReport.Font = ((System.Drawing.Font)(resources.GetObject("btnAddReport.Font")));
			this.btnAddReport.Image = ((System.Drawing.Image)(resources.GetObject("btnAddReport.Image")));
			this.btnAddReport.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnAddReport.ImageAlign")));
			this.btnAddReport.ImageIndex = ((int)(resources.GetObject("btnAddReport.ImageIndex")));
			this.btnAddReport.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnAddReport.ImeMode")));
			this.btnAddReport.Location = ((System.Drawing.Point)(resources.GetObject("btnAddReport.Location")));
			this.btnAddReport.Name = "btnAddReport";
			this.btnAddReport.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnAddReport.RightToLeft")));
			this.btnAddReport.Size = ((System.Drawing.Size)(resources.GetObject("btnAddReport.Size")));
			this.btnAddReport.TabIndex = ((int)(resources.GetObject("btnAddReport.TabIndex")));
			this.btnAddReport.Text = resources.GetString("btnAddReport.Text");
			this.btnAddReport.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnAddReport.TextAlign")));
			this.btnAddReport.Visible = ((bool)(resources.GetObject("btnAddReport.Visible")));
			this.btnAddReport.Click += new System.EventHandler(this.btnAddReport_Click);
			// 
			// btnViewReport
			// 
			this.btnViewReport.AccessibleDescription = resources.GetString("btnViewReport.AccessibleDescription");
			this.btnViewReport.AccessibleName = resources.GetString("btnViewReport.AccessibleName");
			this.btnViewReport.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnViewReport.Anchor")));
			this.btnViewReport.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnViewReport.BackgroundImage")));
			this.btnViewReport.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnViewReport.Dock")));
			this.btnViewReport.Enabled = ((bool)(resources.GetObject("btnViewReport.Enabled")));
			this.btnViewReport.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnViewReport.FlatStyle")));
			this.btnViewReport.Font = ((System.Drawing.Font)(resources.GetObject("btnViewReport.Font")));
			this.btnViewReport.Image = ((System.Drawing.Image)(resources.GetObject("btnViewReport.Image")));
			this.btnViewReport.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnViewReport.ImageAlign")));
			this.btnViewReport.ImageIndex = ((int)(resources.GetObject("btnViewReport.ImageIndex")));
			this.btnViewReport.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnViewReport.ImeMode")));
			this.btnViewReport.Location = ((System.Drawing.Point)(resources.GetObject("btnViewReport.Location")));
			this.btnViewReport.Name = "btnViewReport";
			this.btnViewReport.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnViewReport.RightToLeft")));
			this.btnViewReport.Size = ((System.Drawing.Size)(resources.GetObject("btnViewReport.Size")));
			this.btnViewReport.TabIndex = ((int)(resources.GetObject("btnViewReport.TabIndex")));
			this.btnViewReport.Text = resources.GetString("btnViewReport.Text");
			this.btnViewReport.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnViewReport.TextAlign")));
			this.btnViewReport.Visible = ((bool)(resources.GetObject("btnViewReport.Visible")));
			this.btnViewReport.Click += new System.EventHandler(this.btnViewReport_Click);
			// 
			// tvwReportList
			// 
			this.tvwReportList.AccessibleDescription = resources.GetString("tvwReportList.AccessibleDescription");
			this.tvwReportList.AccessibleName = resources.GetString("tvwReportList.AccessibleName");
			this.tvwReportList.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tvwReportList.Anchor")));
			this.tvwReportList.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tvwReportList.BackgroundImage")));
			this.tvwReportList.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tvwReportList.Dock")));
			this.tvwReportList.Enabled = ((bool)(resources.GetObject("tvwReportList.Enabled")));
			this.tvwReportList.Font = ((System.Drawing.Font)(resources.GetObject("tvwReportList.Font")));
			this.tvwReportList.HideSelection = false;
			this.tvwReportList.ImageIndex = ((int)(resources.GetObject("tvwReportList.ImageIndex")));
			this.tvwReportList.ImageList = this.imglTreeNode;
			this.tvwReportList.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tvwReportList.ImeMode")));
			this.tvwReportList.Indent = ((int)(resources.GetObject("tvwReportList.Indent")));
			this.tvwReportList.ItemHeight = ((int)(resources.GetObject("tvwReportList.ItemHeight")));
			this.tvwReportList.Location = ((System.Drawing.Point)(resources.GetObject("tvwReportList.Location")));
			this.tvwReportList.Name = "tvwReportList";
			this.tvwReportList.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tvwReportList.RightToLeft")));
			this.tvwReportList.SelectedImageIndex = ((int)(resources.GetObject("tvwReportList.SelectedImageIndex")));
			this.tvwReportList.Size = ((System.Drawing.Size)(resources.GetObject("tvwReportList.Size")));
			this.tvwReportList.TabIndex = ((int)(resources.GetObject("tvwReportList.TabIndex")));
			this.tvwReportList.Text = resources.GetString("tvwReportList.Text");
			this.tvwReportList.Visible = ((bool)(resources.GetObject("tvwReportList.Visible")));
			this.tvwReportList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tvwReportList_KeyDown);
			this.tvwReportList.DoubleClick += new System.EventHandler(this.tvwReportList_DoubleClick);
			this.tvwReportList.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvwReportList_AfterSelect);
			this.tvwReportList.BeforeCollapse += new System.Windows.Forms.TreeViewCancelEventHandler(this.tvwReportList_BeforeCollapse);
			this.tvwReportList.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.tvwReportList_BeforeExpand);
			// 
			// imglTreeNode
			// 
			this.imglTreeNode.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
			this.imglTreeNode.ImageSize = ((System.Drawing.Size)(resources.GetObject("imglTreeNode.ImageSize")));
			this.imglTreeNode.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imglTreeNode.ImageStream")));
			this.imglTreeNode.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// lblReportList
			// 
			this.lblReportList.AccessibleDescription = resources.GetString("lblReportList.AccessibleDescription");
			this.lblReportList.AccessibleName = resources.GetString("lblReportList.AccessibleName");
			this.lblReportList.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblReportList.Anchor")));
			this.lblReportList.AutoSize = ((bool)(resources.GetObject("lblReportList.AutoSize")));
			this.lblReportList.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblReportList.Dock")));
			this.lblReportList.Enabled = ((bool)(resources.GetObject("lblReportList.Enabled")));
			this.lblReportList.Font = ((System.Drawing.Font)(resources.GetObject("lblReportList.Font")));
			this.lblReportList.Image = ((System.Drawing.Image)(resources.GetObject("lblReportList.Image")));
			this.lblReportList.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblReportList.ImageAlign")));
			this.lblReportList.ImageIndex = ((int)(resources.GetObject("lblReportList.ImageIndex")));
			this.lblReportList.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblReportList.ImeMode")));
			this.lblReportList.Location = ((System.Drawing.Point)(resources.GetObject("lblReportList.Location")));
			this.lblReportList.Name = "lblReportList";
			this.lblReportList.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblReportList.RightToLeft")));
			this.lblReportList.Size = ((System.Drawing.Size)(resources.GetObject("lblReportList.Size")));
			this.lblReportList.TabIndex = ((int)(resources.GetObject("lblReportList.TabIndex")));
			this.lblReportList.Text = resources.GetString("lblReportList.Text");
			this.lblReportList.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblReportList.TextAlign")));
			this.lblReportList.Visible = ((bool)(resources.GetObject("lblReportList.Visible")));
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
			// lblF1
			// 
			this.lblF1.AccessibleDescription = resources.GetString("lblF1.AccessibleDescription");
			this.lblF1.AccessibleName = resources.GetString("lblF1.AccessibleName");
			this.lblF1.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblF1.Anchor")));
			this.lblF1.AutoSize = ((bool)(resources.GetObject("lblF1.AutoSize")));
			this.lblF1.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblF1.Dock")));
			this.lblF1.Enabled = ((bool)(resources.GetObject("lblF1.Enabled")));
			this.lblF1.Font = ((System.Drawing.Font)(resources.GetObject("lblF1.Font")));
			this.lblF1.Image = ((System.Drawing.Image)(resources.GetObject("lblF1.Image")));
			this.lblF1.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblF1.ImageAlign")));
			this.lblF1.ImageIndex = ((int)(resources.GetObject("lblF1.ImageIndex")));
			this.lblF1.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblF1.ImeMode")));
			this.lblF1.Location = ((System.Drawing.Point)(resources.GetObject("lblF1.Location")));
			this.lblF1.Name = "lblF1";
			this.lblF1.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblF1.RightToLeft")));
			this.lblF1.Size = ((System.Drawing.Size)(resources.GetObject("lblF1.Size")));
			this.lblF1.TabIndex = ((int)(resources.GetObject("lblF1.TabIndex")));
			this.lblF1.Text = resources.GetString("lblF1.Text");
			this.lblF1.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblF1.TextAlign")));
			this.lblF1.Visible = ((bool)(resources.GetObject("lblF1.Visible")));
			// 
			// lblAddGroup
			// 
			this.lblAddGroup.AccessibleDescription = resources.GetString("lblAddGroup.AccessibleDescription");
			this.lblAddGroup.AccessibleName = resources.GetString("lblAddGroup.AccessibleName");
			this.lblAddGroup.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblAddGroup.Anchor")));
			this.lblAddGroup.AutoSize = ((bool)(resources.GetObject("lblAddGroup.AutoSize")));
			this.lblAddGroup.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblAddGroup.Dock")));
			this.lblAddGroup.Enabled = ((bool)(resources.GetObject("lblAddGroup.Enabled")));
			this.lblAddGroup.Font = ((System.Drawing.Font)(resources.GetObject("lblAddGroup.Font")));
			this.lblAddGroup.Image = ((System.Drawing.Image)(resources.GetObject("lblAddGroup.Image")));
			this.lblAddGroup.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblAddGroup.ImageAlign")));
			this.lblAddGroup.ImageIndex = ((int)(resources.GetObject("lblAddGroup.ImageIndex")));
			this.lblAddGroup.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblAddGroup.ImeMode")));
			this.lblAddGroup.Location = ((System.Drawing.Point)(resources.GetObject("lblAddGroup.Location")));
			this.lblAddGroup.Name = "lblAddGroup";
			this.lblAddGroup.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblAddGroup.RightToLeft")));
			this.lblAddGroup.Size = ((System.Drawing.Size)(resources.GetObject("lblAddGroup.Size")));
			this.lblAddGroup.TabIndex = ((int)(resources.GetObject("lblAddGroup.TabIndex")));
			this.lblAddGroup.Text = resources.GetString("lblAddGroup.Text");
			this.lblAddGroup.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblAddGroup.TextAlign")));
			this.lblAddGroup.Visible = ((bool)(resources.GetObject("lblAddGroup.Visible")));
			// 
			// lblShowLast
			// 
			this.lblShowLast.AccessibleDescription = resources.GetString("lblShowLast.AccessibleDescription");
			this.lblShowLast.AccessibleName = resources.GetString("lblShowLast.AccessibleName");
			this.lblShowLast.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblShowLast.Anchor")));
			this.lblShowLast.AutoSize = ((bool)(resources.GetObject("lblShowLast.AutoSize")));
			this.lblShowLast.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblShowLast.Dock")));
			this.lblShowLast.Enabled = ((bool)(resources.GetObject("lblShowLast.Enabled")));
			this.lblShowLast.Font = ((System.Drawing.Font)(resources.GetObject("lblShowLast.Font")));
			this.lblShowLast.Image = ((System.Drawing.Image)(resources.GetObject("lblShowLast.Image")));
			this.lblShowLast.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblShowLast.ImageAlign")));
			this.lblShowLast.ImageIndex = ((int)(resources.GetObject("lblShowLast.ImageIndex")));
			this.lblShowLast.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblShowLast.ImeMode")));
			this.lblShowLast.Location = ((System.Drawing.Point)(resources.GetObject("lblShowLast.Location")));
			this.lblShowLast.Name = "lblShowLast";
			this.lblShowLast.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblShowLast.RightToLeft")));
			this.lblShowLast.Size = ((System.Drawing.Size)(resources.GetObject("lblShowLast.Size")));
			this.lblShowLast.TabIndex = ((int)(resources.GetObject("lblShowLast.TabIndex")));
			this.lblShowLast.Text = resources.GetString("lblShowLast.Text");
			this.lblShowLast.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblShowLast.TextAlign")));
			this.lblShowLast.Visible = ((bool)(resources.GetObject("lblShowLast.Visible")));
			// 
			// ReportManagement
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
			this.Controls.Add(this.lblShowLast);
			this.Controls.Add(this.lblAddGroup);
			this.Controls.Add(this.btnClose);
			this.Controls.Add(this.btnRefreshTreeView);
			this.Controls.Add(this.btnShowLastReport);
			this.Controls.Add(this.btnDeleteReport);
			this.Controls.Add(this.btnDeleteGroup);
			this.Controls.Add(this.btnMoveDown);
			this.Controls.Add(this.btnMoveUp);
			this.Controls.Add(this.btnEditReport);
			this.Controls.Add(this.btnCopyReport);
			this.Controls.Add(this.btnPasteReport);
			this.Controls.Add(this.btnAddGroup);
			this.Controls.Add(this.btnEditGroup);
			this.Controls.Add(this.btnCopyGroup);
			this.Controls.Add(this.btnAddReport);
			this.Controls.Add(this.btnViewReport);
			this.Controls.Add(this.tvwReportList);
			this.Controls.Add(this.lblReportList);
			this.Controls.Add(this.lblF1);
			this.Controls.Add(this.lblShortCutKeys);
			this.Enabled = ((bool)(resources.GetObject("$this.Enabled")));
			this.Font = ((System.Drawing.Font)(resources.GetObject("$this.Font")));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("$this.ImeMode")));
			this.Location = ((System.Drawing.Point)(resources.GetObject("$this.Location")));
			this.MaximumSize = ((System.Drawing.Size)(resources.GetObject("$this.MaximumSize")));
			this.MinimumSize = ((System.Drawing.Size)(resources.GetObject("$this.MinimumSize")));
			this.Name = "ReportManagement";
			this.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("$this.RightToLeft")));
			this.StartPosition = ((System.Windows.Forms.FormStartPosition)(resources.GetObject("$this.StartPosition")));
			this.Text = resources.GetString("$this.Text");
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ReportManagement_KeyDown);
			this.Load += new System.EventHandler(this.ReportManagement_Load);
			this.ResumeLayout(false);

		}
		#endregion

		#region Form Events

		//**************************************************************************              
		///    <Description>
		///       Load the form and initialize variable.
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
		private void ReportManagement_Load(object sender, EventArgs e)
		{
			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.WaitCursor;

			#endregion Code Inserted Automatically

			
			const string METHOD_NAME = THIS + ".ReportManagement_Load()";
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

				//setting the KeyDown event for all controls in form
				//KeyDownSettings(this);

				//Display all reports in TreeView control
				BindTreeView();	
				// if we have no group/report or have no selected node then user only allows to add new group first
				if ((tvwReportList.Nodes.Count == 0) || (tvwReportList.SelectedNode == null))
				{
					btnAddGroup.Enabled = true;
					btnViewReport.Enabled = false;
					btnAddReport.Enabled = lblF1.Enabled = false;
					btnEditReport.Enabled = false;
					btnCopyReport.Enabled = false;
					btnPasteReport.Enabled = false;
					btnDeleteReport.Enabled = false;
					btnEditGroup.Enabled = false;
					btnCopyGroup.Enabled = false;
					btnDeleteGroup.Enabled = false;
					btnMoveDown.Enabled = false;
					btnMoveUp.Enabled = false;
				}
				else
				{
					btnShowLastReport.Enabled = true;
					btnMoveDown.Enabled = true;
					btnMoveUp.Enabled = true;
				}
				// collapse all node
				tvwReportList.CollapseAll();
				if (tvwReportList.TopNode != null)
					tvwReportList.SelectedNode = tvwReportList.TopNode;
				EnableButtons(tvwReportList.SelectedNode);
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
		///       get selected object
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
		private void tvwReportList_AfterSelect(object sender, TreeViewEventArgs e)
		{
			const string METHOD_NAME = THIS + ".tvwReportList_AfterSelect()";
			try
			{
				// TreeNode tnNode = tvwReportList.SelectedNode;
				TreeNode tnNode = e.Node;
				if (tnNode.Tag is sys_ReportGroupVO)
					voSelectedGroup = (sys_ReportGroupVO)tnNode.Tag;
				else
					voSelectedGroup = (sys_ReportGroupVO)tnNode.Parent.Tag;
				EnableButtons(tnNode);
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
		///       Display form to edit selected object
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
		private void tvwReportList_DoubleClick(object sender, EventArgs e)
		{
			const string METHOD_NAME = THIS + ".tvwReportList_DoubleClick()";
			try
			{
				TreeNode tnNode = tvwReportList.SelectedNode;
				if ((tnNode != null) && (!IsGroupNode(tnNode)))
				{
					btnViewReport_Click(sender, e);
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
		///       Display form to edit selected object
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
		private void tvwReportList_KeyDown(object sender, KeyEventArgs e)
		{
			const string METHOD_NAME = THIS + ".tvwReportList_KeyDown()";
			try
			{
				if (e.KeyCode == Keys.Enter)
				{
					TreeNode tnNode = tvwReportList.SelectedNode;
					if ((tnNode != null) && (!IsGroupNode(tnNode)))
					{
						btnViewReport_Click(sender, e);
					}
					else if ((tnNode != null) && (IsGroupNode(tnNode)))
					{
						if (tnNode.IsExpanded)
							tnNode.Collapse();
						else
							tnNode.Expand();
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
		///       Display ViewReport screen
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
		private void btnViewReport_Click(object sender, EventArgs e)
		{
			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.WaitCursor;

			#endregion Code Inserted Automatically

			
			const string METHOD_NAME = THIS + ".btnViewReport_Click()";
			try
			{
				ViewReport frmViewReport = new ViewReport();
				frmViewReport.VoReport = (sys_ReportVO)tvwReportList.SelectedNode.Tag;
				frmViewReport.ViewMode = ViewReportMode.Normal;
				frmViewReport.Show();
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
		///       Display EditReport form with ADD mode
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
		private void btnAddReport_Click(object sender, EventArgs e)
		{
			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.WaitCursor;

			#endregion Code Inserted Automatically

			
			const string METHOD_NAME = THIS + ".btnAddReport_Click()";

			try
			{
				TreeNode objGroupNode = null;
				objGroupNode = (tvwReportList.SelectedNode.Tag is sys_ReportVO)
					? tvwReportList.SelectedNode.Parent
					: tvwReportList.SelectedNode;
				if (btnAddReport.Enabled && (objGroupNode.Tag is sys_ReportGroupVO))
				{
					EditReport frmEditReport = new EditReport();

					frmEditReport.GroupID = voSelectedGroup.GroupID;
					// Get database server date time
					DateTime dtmDB = boUtils.GetDBDate();
					// set new id for new report
					frmEditReport.ReportID = dtmDB.ToString(CODE_DATE_FORMAT);

					frmEditReport.EnumType = EnumAction.Add;
					frmEditReport.ShowDialog();
					if ((frmEditReport.VOReport != null) && 
						(frmEditReport.VOReport.ReportID != null) &&
						(frmEditReport.VOReport.ReportID != string.Empty) && 
						(frmEditReport.VOReport.ReportName != string.Empty)   )
					{
						TreeNode objNode = new TreeNode();
						objNode.Text = frmEditReport.VOReport.ReportName;
						objNode.Tag = frmEditReport.VOReport;
						objGroupNode.Nodes.Add(objNode);
						tvwReportList.SelectedNode = objNode;
					}
				}
			}
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
			finally
			{
				tvwReportList.Select();
				tvwReportList.Focus();
			}

			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.Default;

			#endregion Code Inserted Automatically

			
		}

		//**************************************************************************              
		///    <Description>
		///       Display confirm message and delete selected report.
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
		private void btnDeleteReport_Click(object sender, EventArgs e)
		{
			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.WaitCursor;

			#endregion Code Inserted Automatically

			
			const string METHOD_NAME = THIS + ".btnDeleteReport_Click()";
			try
			{
				// alert user to confirm delete selection
				DialogResult dlgResult = PCSMessageBox.Show(ErrorCode.MESSAGE_DELETE_RECORD, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
				if (dlgResult == DialogResult.Yes)
				{
					sys_ReportVO voReport = (sys_ReportVO) (tvwReportList.SelectedNode.Tag);
					boReportManagement.Delete(voReport);
					tvwReportList.SelectedNode.Remove();
				}
				tvwReportList.Select();
				tvwReportList.Focus();
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
		///       Refresh the TreeView control.
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
		private void btnRefreshTreeView_Click(object sender, EventArgs e)
		{
			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.WaitCursor;

			#endregion Code Inserted Automatically

			
			const string METHOD_NAME = THIS + ".btnRefreshTreeView_Click()";
			try
			{
				tvwReportList.Refresh();
				tvwReportList.Select();
				tvwReportList.Focus();
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
		///       Display EditReport form to edit selected report.
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
		private void btnEditReport_Click(object sender, EventArgs e)
		{
			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.WaitCursor;

			#endregion Code Inserted Automatically

			
			const string METHOD_NAME = THIS + ".btnEditReport_Click()";
			try
			{
				TreeNode objNode = tvwReportList.SelectedNode;
				// make sure that the selected node is the report
				if (!IsGroupNode(tvwReportList.SelectedNode))
				{
					EditReport frmEditReport = new EditReport(tvwReportList.SelectedNode.Tag);
					frmEditReport.EnumType = EnumAction.Default;
					frmEditReport.GroupID = voSelectedGroup.GroupID;
					frmEditReport.ShowDialog();
					/// if frmEditReport.DialogResult = OK, it means there is a change, and we need to update the treeview
					/// if frmEditReport.DialogResult = Cancel, it means there is error when EditReport, no change affect, we don't update the treeview

					if ((frmEditReport.VOReport != null) &&
						(frmEditReport.VOReport.ReportID != null) &&
						(frmEditReport.VOReport.ReportID != string.Empty) && 
						frmEditReport.DialogResult == DialogResult.OK
						)
					{
						objNode.Text = frmEditReport.VOReport.ReportName;
						objNode.Tag = frmEditReport.VOReport;
						tvwReportList.SelectedNode = objNode;
					}
					else if (frmEditReport.VOReport == null ||
						frmEditReport.VOReport.ReportID == null ||
						frmEditReport.VOReport.ReportID == string.Empty)
					{
						// user delete the report on EditReport form, we need to remove the report in TreeView
						tvwReportList.Nodes.Remove(objNode);
					}
					tvwReportList.Focus();
					tvwReportList.Select();				
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
		///       Make a copy object of selected report.
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
		private void btnCopyReport_Click(object sender, EventArgs e)
		{
			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.WaitCursor;

			#endregion Code Inserted Automatically

			
			const string METHOD_NAME = THIS + ".btnCopyReport_Click()";
			try
			{
				// make sure that selected node is report
				if (tvwReportList.SelectedNode.Tag is sys_ReportVO)
					voSelectedReport = (sys_ReportVO) (tvwReportList.SelectedNode.Tag);
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
		///       Paste copied report to selected group.
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
		private void btnPasteReport_Click(object sender, EventArgs e)
		{
			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.WaitCursor;

			#endregion Code Inserted Automatically

			
			const string METHOD_NAME = THIS + ".btnPasteReport_Click()";
			try
			{
				TreeNode objGroupNode = tvwReportList.SelectedNode;
				// make sure that selected node is not null
				// and is the report node - child node (not group node)
				if ((voSelectedReport != null) && IsGroupNode(objGroupNode))
				{
					ReportManagementBO boReportManagement = new ReportManagementBO();
					int intReportOrder = 0;
					sys_ReportVO voReport = (sys_ReportVO)boReportManagement.CopyReport(voSelectedReport.ReportID, voSelectedGroup.GroupID, out intReportOrder);
					if ((voReport != null) && (voReport.ReportID != null) 
						&& (voReport.ReportID != string.Empty))
					{
						// create new node
						TreeNode objNode = new TreeNode(voReport.ReportName);
						// assign report to tree node
						objNode.Tag = voReport;
						// add new node to selected group
						objGroupNode.Nodes.Add(objNode);
						// select new node
						tvwReportList.SelectedNode = objNode;
					}					
				}
			}
			catch (PCSException ex)
			{
//				// just for testing
//				if (ex is PCSBOException)
//					MessageBox.Show("Report ID is too long. Please select another ID");
				// displays the error message.
				if (ex.mCode == ErrorCode.DUPLICATE_KEY)
					PCSMessageBox.Show(ErrorCode.MESSAGE_REPORT_EXISTED, MessageBoxButtons.OK, MessageBoxIcon.Error);
				else
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
			finally
			{
				voSelectedReport = null;
				tvwReportList.Select();
				tvwReportList.Focus();
			}

			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.Default;

			#endregion Code Inserted Automatically

			
		}

		//**************************************************************************              
		///    <Description>
		///       Move up item.
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
		private void btnMoveUp_Click(object sender, EventArgs e)
		{
			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.WaitCursor;

			#endregion Code Inserted Automatically

			
			const string METHOD_NAME = THIS + ".btnMoveUp_Click()";
			try
			{
				TreeNode objSelectedNode = tvwReportList.SelectedNode;
				if (objSelectedNode != null)
				{
					// move up report
					if (objSelectedNode.Tag is sys_ReportVO)
					{
						// get previous report
						sys_ReportVO voPrevReport = (sys_ReportVO)objSelectedNode.PrevVisibleNode.Tag;
						// get selected report
						sys_ReportVO voCurrentReport = (sys_ReportVO)objSelectedNode.Tag;
						// update report order to database
						sys_ReportAndGroupVO voSelected = (sys_ReportAndGroupVO)boReportManagement.GetReportAndGroupObject(voCurrentReport.ReportID, voSelectedGroup.GroupID);
						sys_ReportAndGroupVO voPrev = (sys_ReportAndGroupVO)boReportManagement.GetReportAndGroupObject(voPrevReport.ReportID, voSelectedGroup.GroupID);
						int intSelectedOrder = voSelected.ReportOrder;
						voSelected.ReportOrder = voPrev.ReportOrder;
						voPrev.ReportOrder = intSelectedOrder;
						boReportManagement.SwapReport(voSelected,  voPrev);
						// move report within its parent
						TreeNode objParentNode = objSelectedNode.Parent;
						objParentNode.Nodes.Remove(objSelectedNode);
						objParentNode.Nodes.Insert(objSelectedNode.Index - 1, objSelectedNode);
					}
					else // move up group
					{
						// get previous group
						sys_ReportGroupVO voPreGroup;
						// if previous node is a report, we need to get its parent to swap
						if (objSelectedNode.PrevVisibleNode.Tag is sys_ReportVO)
							voPreGroup = (sys_ReportGroupVO)objSelectedNode.PrevVisibleNode.Parent.Tag;
						else
							voPreGroup = (sys_ReportGroupVO)objSelectedNode.PrevVisibleNode.Tag;
						int intSelectedOrder = voSelectedGroup.GroupOrder;
						voSelectedGroup.GroupOrder = voPreGroup.GroupOrder;
						voPreGroup.GroupOrder = intSelectedOrder;
						boReportManagement.SwapGroup(voSelectedGroup, voPreGroup);
						// move group within tree view
						tvwReportList.Nodes.Remove(objSelectedNode);
						tvwReportList.Nodes.Insert(objSelectedNode.Index - 1, objSelectedNode);
					}
					// set focus on selected on
					tvwReportList.Focus();
					tvwReportList.Select();
					tvwReportList.SelectedNode = objSelectedNode;
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
		///       Move down item.
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
		private void btnMoveDown_Click(object sender, EventArgs e)
		{
			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.WaitCursor;

			#endregion Code Inserted Automatically

			
			const string METHOD_NAME = THIS + ".btnMoveUp_Click()";
			try
			{
				TreeNode objSelectedNode = tvwReportList.SelectedNode;
				if (objSelectedNode != null)
				{
					// move down report
					if (objSelectedNode.Tag is sys_ReportVO)
					{
						// get netxt report
						sys_ReportVO voNextReport = (sys_ReportVO)objSelectedNode.NextVisibleNode.Tag;
						// get selected report
						sys_ReportVO voCurrentReport = (sys_ReportVO)objSelectedNode.Tag;
						// update report order to database
						sys_ReportAndGroupVO voSelected = (sys_ReportAndGroupVO)boReportManagement.GetReportAndGroupObject(voCurrentReport.ReportID, voSelectedGroup.GroupID);
						sys_ReportAndGroupVO voNext = (sys_ReportAndGroupVO)boReportManagement.GetReportAndGroupObject(voNextReport.ReportID, voSelectedGroup.GroupID);
						int intSelectedOrder = voSelected.ReportOrder;
						voSelected.ReportOrder = voNext.ReportOrder;
						voNext.ReportOrder = intSelectedOrder;
						boReportManagement.SwapReport(voSelected,  voNext);
						// move report within its parent
						TreeNode objParentNode = objSelectedNode.Parent;
						objParentNode.Nodes.Remove(objSelectedNode);
						objParentNode.Nodes.Insert(objSelectedNode.Index + 1, objSelectedNode);
					}
					else // move up group
					{
						sys_ReportGroupVO voNextGroup;
						// if next node is report, we need to get its parent to swap
						if (objSelectedNode.NextVisibleNode.Tag is sys_ReportVO)
							voNextGroup = (sys_ReportGroupVO)objSelectedNode.NextVisibleNode.Parent.Tag;
						else
							voNextGroup = (sys_ReportGroupVO)objSelectedNode.NextVisibleNode.Tag;
						// switch order
						int intCurrentOrder = voSelectedGroup.GroupOrder;
						voSelectedGroup.GroupOrder = voNextGroup.GroupOrder;
						voNextGroup.GroupOrder = intCurrentOrder;
						// update to database
						boReportManagement.SwapGroup(voSelectedGroup, voNextGroup);
						// move group within tree view
						tvwReportList.Nodes.Remove(objSelectedNode);
						tvwReportList.Nodes.Insert(objSelectedNode.Index + 1, objSelectedNode);
					}
					// set focus on selected on
					tvwReportList.Focus();
					tvwReportList.Select();
					tvwReportList.SelectedNode = objSelectedNode;
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
		///       Display ReportGroup form to add new group.
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
		private void btnAddGroup_Click(object sender, EventArgs e)
		{
			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.WaitCursor;

			#endregion Code Inserted Automatically

			
			/// HACK: Thachnn : auto generate GroupCode, do not allow User to modify the GroupCOde
			/// Make Add Group act like CopyGroup, except setting group name behavious
			const string METHOD_NAME = THIS + ".btnAddGroup_Click()";
			const int GROUP_CODE_MAX_LENGTH = 20;
			
			try
			{
				ReportGroup frmReportGroup = new ReportGroup();
				sys_ReportGroupVO voNewGroup = new sys_ReportGroupVO();
				// Get database server date time
				DateTime dtmDB = boUtils.GetDBDate();
				// set new id for new group
				voNewGroup.GroupID = dtmDB.ToString(CODE_DATE_FORMAT);
				voNewGroup.GroupName = string.Empty;
				if (voNewGroup.GroupID.Length > GROUP_CODE_MAX_LENGTH)
				{
					PCSMessageBox.Show(ErrorCode.MESSAGE_VALUE_TOO_LONG, MessageBoxIcon.Error);
					// Code Inserted Automatically
#region Code Inserted Automatically
this.Cursor = Cursors.Default;
#endregion Code Inserted Automatically

					return;
				}
				frmReportGroup.ReportGroupVO = voNewGroup;
				frmReportGroup.EnumType = EnumAction.Copy;

				frmReportGroup.ShowDialog();

				if (frmReportGroup.ReportGroupVO.GroupID != null && 
					frmReportGroup.ReportGroupVO.GroupID != string.Empty && 
					frmReportGroup.ReportGroupVO.GroupName != string.Empty)
				{
					voSelectedGroup = frmReportGroup.ReportGroupVO;
					// create new TreeNode
					TreeNode objNode = new TreeNode(voSelectedGroup.GroupName);
					objNode.Tag = voSelectedGroup;
					// set image
					objNode.ImageIndex = objNode.SelectedImageIndex = 1;
					// add new node to tree
					tvwReportList.Nodes.Add(objNode);
					// select new node
					tvwReportList.SelectedNode = objNode;
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
			finally
			{
				tvwReportList.Select();
				// HACK: Thachnn: 14/Oct/2005 tvwReportList.Focus();
			}

			#region OLDLINES			
//			const string METHOD_NAME = THIS + ".btnAddGroup_Click()";
//			try
//			{
//				ReportGroup frmReportGroup = new ReportGroup();
//				frmReportGroup.EnumType = EnumAction.Add;
//				frmReportGroup.ShowDialog();
//				if (frmReportGroup.ReportGroupVO.GroupID != null && 
//					frmReportGroup.ReportGroupVO.GroupID != string.Empty)
//				{
//					voSelectedGroup = frmReportGroup.ReportGroupVO;
//					// create new TreeNode
//					TreeNode objNode = new TreeNode(voSelectedGroup.GroupName);
//					objNode.Tag = voSelectedGroup;
//					// set image
//					objNode.ImageIndex = objNode.SelectedImageIndex = 1;
//					// add new node to tree
//					tvwReportList.Nodes.Add(objNode);
//					// select new node
//					tvwReportList.SelectedNode = objNode;
//				}
//			}
//			catch (PCSException ex)
//			{
//				// displays the error message.
//				PCSMessageBox.Show(ex.mCode, MessageBoxButtons.OK, MessageBoxIcon.Error);
//				// log message.
//				try
//				{
//					Logger.LogMessage(ex.CauseException, METHOD_NAME, Level.ERROR);
//				}
//				catch
//				{
//					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
//				}
//			}
//			catch (Exception ex)
//			{
//				// displays the error message.
//				PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
//				// log message.
//				try
//				{
//					Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
//				}
//				catch
//				{
//					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
//				}
//			}
//			finally
//			{
//				tvwReportList.Select();
//				tvwReportList.Focus();
//			}
			#endregion

			/// ENDHACKED: Thachnn : auto generate GroupCode, do not allow User to modify the GroupCOde

			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.Default;

			#endregion Code Inserted Automatically

			
		}

		//**************************************************************************              
		///    <Description>
		///       Display ReportGroup form to edit selected group.
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
		///       12/Oct/2005 Thachnn: fix bug
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private void btnEditGroup_Click(object sender, EventArgs e)
		{
			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.WaitCursor;

			#endregion Code Inserted Automatically

			
			const string METHOD_NAME = THIS + ".btnEditGroup_Click()";
			try
			{
				// make sure that the selected node is the group
				if (IsGroupNode(tvwReportList.SelectedNode))
				{
					ReportGroup frmReportGroup = new ReportGroup();
					frmReportGroup.ReportGroupVO = voSelectedGroup;
					frmReportGroup.EnumType = EnumAction.Edit;
					frmReportGroup.ShowDialog();
					if (frmReportGroup.ReportGroupVO.GroupID != null && frmReportGroup.ReportGroupVO.GroupID != string.Empty)
					{
						voSelectedGroup = frmReportGroup.ReportGroupVO;
						tvwReportList.SelectedNode.Text = voSelectedGroup.GroupName;
						tvwReportList.SelectedNode.Tag = voSelectedGroup;
						///MOVE this line to below: tvwReportList.Select();
					}
					tvwReportList.Select();
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
		///       Display confirm message and delete selected group.
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
		private void btnDeleteGroup_Click(object sender, EventArgs e)
		{
			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.WaitCursor;

			#endregion Code Inserted Automatically

			
			const string METHOD_NAME = THIS + ".btnDeleteGroup_Click()";
			try
			{
				// determine whether the selected node is the group
				if (IsGroupNode(tvwReportList.SelectedNode))
				{
					// alert user to confirm delete selection
					DialogResult dlgResult = PCSMessageBox.Show(ErrorCode.MESSAGE_DELETE_GROUP, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
					if (dlgResult == DialogResult.Yes)
					{
						ReportGroupBO boReportGroup = new ReportGroupBO();
						if (!boReportGroup.DeleteGroup(voSelectedGroup.GroupID))
						{
							// unabled to delete
							PCSMessageBox.Show(ErrorCode.ERROR_CANNOT_DELETE_GROUP, MessageBoxButtons.OK, MessageBoxIcon.Error);
						}
						else // remove selected node from the list
						{
							voSelectedGroup = null;
							tvwReportList.SelectedNode.Remove();
						}
					}
					
					/// HACKED: Thachnn: fix bug 2013
					// if we have no group/report or have no selected node then user only allows to add new group first
					if ((tvwReportList.Nodes.Count == 0) || (tvwReportList.SelectedNode == null))
					{
						btnAddGroup.Enabled = true;
						btnViewReport.Enabled = false;
						btnAddReport.Enabled = lblF1.Enabled = false;
						btnEditReport.Enabled = false;
						btnCopyReport.Enabled = false;
						btnPasteReport.Enabled = false;
						btnDeleteReport.Enabled = false;
						btnEditGroup.Enabled = false;
						btnCopyGroup.Enabled = false;
						btnDeleteGroup.Enabled = false;
						btnMoveDown.Enabled = false;
						btnMoveUp.Enabled = false;
					}
					else
					{
						btnShowLastReport.Enabled = true;
						/// btnMoveDown.Enabled = true;
						/// btnMoveUp.Enabled = true;
					}
					/// collapse all node
					/// tvwReportList.CollapseAll();
					/// if (tvwReportList.TopNode != null)
					/// {
					/// tvwReportList.SelectedNode = tvwReportList.TopNode;
					/// }
					tvwReportList.Select();
					EnableButtons(tvwReportList.SelectedNode);
					/// ENDHACKED: Thachnn: fix bug 2013
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
		///       Make a copy object of selected group.
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
		private void btnCopyGroup_Click(object sender, EventArgs e)
		{
			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.WaitCursor;

			#endregion Code Inserted Automatically

			
			const string METHOD_NAME = THIS + ".btnCopyGroup_Click()";
			const int GROUP_CODE_MAX_LENGTH = 20;
			const string CODE_DATE_FORMAT = "yyyyMMddHHmmssfff";
			try
			{
				TreeNode objSelectedNode = tvwReportList.SelectedNode;
				// determine whether the selected node is the group
				if (IsGroupNode(objSelectedNode))
				{
					ReportGroup frmReportGroup = new ReportGroup();
					sys_ReportGroupVO voCopiedGroup = new sys_ReportGroupVO();
					// Get database server date time
					DateTime dtmDB = boUtils.GetDBDate();
					// set new id and name for copied group
					voCopiedGroup.GroupID = dtmDB.ToString(CODE_DATE_FORMAT);
					voCopiedGroup.GroupName = Constants.COPY_OF + voSelectedGroup.GroupName;
					if (voCopiedGroup.GroupID.Length > GROUP_CODE_MAX_LENGTH)
					{
						PCSMessageBox.Show(ErrorCode.MESSAGE_VALUE_TOO_LONG, MessageBoxIcon.Error);
						// Code Inserted Automatically
#region Code Inserted Automatically
this.Cursor = Cursors.Default;
#endregion Code Inserted Automatically

						return;
					}
					frmReportGroup.ReportGroupVO = voCopiedGroup;
					frmReportGroup.EnumType = EnumAction.Copy;

					frmReportGroup.ShowDialog();

					if (frmReportGroup.ReportGroupVO.GroupID != null && 
						frmReportGroup.ReportGroupVO.GroupID != string.Empty &&
						frmReportGroup.GroupID != null
						)
					{
						voSelectedGroup = frmReportGroup.ReportGroupVO;
						// create new TreeNode
						TreeNode objNode = new TreeNode(voSelectedGroup.GroupName);
						objNode.Tag = voSelectedGroup;
						// set image
						objNode.ImageIndex = objNode.SelectedImageIndex = 1;
						// add new node to tree
						tvwReportList.Nodes.Add(objNode);
						// select new node
						tvwReportList.SelectedNode = objNode;
					}
//					else
//					{
//						tvwReportList.SelectedNode = objSelectedNode;
//						voSelectedGroup = (sys_ReportGroupVO)objSelectedNode.Tag;
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
			finally
			{
				tvwReportList.Select();
				tvwReportList.Focus();
			}

			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.Default;

			#endregion Code Inserted Automatically

			
		}

		//**************************************************************************              
		///    <Description>
		///       Display LastReport form to show last 10 report by user.
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
		private void btnShowLastReport_Click(object sender, EventArgs e)
		{
			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.WaitCursor;

			#endregion Code Inserted Automatically

			
			const string METHOD_NAME = THIS + ".btnShowLastReport_Click()";
			try
			{
				ViewReportBO boViewReport = new ViewReportBO();
				// displays last 10 report executed by user
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
				// get report history table
				sys_ReportHistoryVO voHistoryData = frmLastReport.ReturnValue;
				// get report object
				sys_ReportVO voSelectedReport = (sys_ReportVO) (boEditReport.GetObjectVO(voHistoryData.ReportID));
				// get data from history table
				DataTable tblHistoryData = boViewReport.GetDataFromHistoryTable(voHistoryData.TableName);
				ViewReport frmViewReport = new ViewReport();

				// turn to mode View History
				frmViewReport.ViewMode = ViewReportMode.History;
				
				// assign Report object to form
				frmViewReport.VoReport = voSelectedReport;
				// assign report data to form
				frmViewReport.ReportData = tblHistoryData;
				// assign report history object
				frmViewReport.VoReportHistory = voHistoryData;

				// show the form
				frmViewReport.Show();
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
			catch(System.Data.OleDb.OleDbException ex)
			{
				// displays the error message.
				PCSMessageBox.Show(ErrorCode.MESSAGE_CANT_RELOAD_REPORT_FROM_HISTORY, MessageBoxButtons.OK, MessageBoxIcon.Error);
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

			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.Default;

			#endregion Code Inserted Automatically

			
		}

		//**************************************************************************              
		///    <Description>
		///       Handle keydown event of all controls in form.
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
		private void ReportManagement_KeyDown(object sender, KeyEventArgs e)
		{
			const string METHOD_NAME = THIS + ".ReportManagement_KeyDown()";
			try
			{
				switch (e.KeyCode)
				{
					case Keys.F1 :
						this.btnAddReport_Click(btnAddReport, e);
						break;
					case Keys.F2:
						this.btnAddGroup_Click(btnAddGroup, e);
						break;
					case Keys.F3:
						this.btnShowLastReport_Click(btnShowLastReport, e);
						break;
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


		private void tvwReportList_BeforeExpand(object sender, System.Windows.Forms.TreeViewCancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".tvwReportList_BeforeExpand()";
			try
			{
				if(IsGroupNode(e.Node))
				{
					e.Node.ImageIndex = e.Node.SelectedImageIndex = 1;
				}
				e.Cancel = false;
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

		private void tvwReportList_BeforeCollapse(object sender, System.Windows.Forms.TreeViewCancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".tvwReportList_BeforeCollapse()";
			try
			{
				if(IsGroupNode(e.Node))
				{
					e.Node.ImageIndex = e.Node.SelectedImageIndex = 2;
				}
				e.Cancel = false;
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
		///       07-Jan-2005
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

			
			Close();

			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.Default;

			#endregion Code Inserted Automatically

			
		}
		#endregion

		#region Private Methods

		//**************************************************************************              
		///    <Description>
		///       Settings KeyDown event for all controls in the form
		///    </Description>
		///    <Inputs>
		///       Control
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
		private void KeyDownSettings(Control pobjControl)
		{
			try
			{
				IEnumerator enumControls = pobjControl.Controls.GetEnumerator();
				while (enumControls.MoveNext())
				{
					Control objC = (Control)enumControls.Current;
					objC.KeyDown += new KeyEventHandler(this.ReportManagement_KeyDown);
					this.KeyDownSettings(objC);
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		
		//**************************************************************************              
		///    <Description>
		///       Binding all reports and groups of system to tree view
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
		private void BindTreeView()
		{
			try
			{
				tvwReportList.Nodes.Clear();
				// create new BO object
				ReportGroupBO boReportGroup = new ReportGroupBO();
				// get all groups
				ArrayList arrAllGroup = boReportGroup.GetAllGroup();
				foreach (sys_ReportGroupVO voReportGroup in arrAllGroup)
				{
					TreeNode tnReportGroup = new TreeNode(voReportGroup.GroupName);
					tnReportGroup.Tag = voReportGroup;
					tnReportGroup.ImageIndex = tnReportGroup.SelectedImageIndex = 1;
					tvwReportList.Nodes.Add(tnReportGroup);
					// now add report to group if any
					ArrayList arrAllReportInGroup = boReportManagement.GetAllReports(voReportGroup.GroupID);
					foreach (sys_ReportVO voReport in arrAllReportInGroup)
					{
						TreeNode tnReport = new TreeNode(voReport.ReportName);
						tnReport.Tag = voReport;
						tnReport.ImageIndex = tnReport.SelectedImageIndex = 0;
						tnReportGroup.Nodes.Add(tnReport);
					}
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
		///       Check a node to see if it is a group node or not.
		///    </Description>
		///    <Inputs>
		///       TreeNode
		///    </Inputs>
		///    <Outputs>
		///       bool
		///    </Outputs>
		///    <Returns>
		///       true if node is group, false if node is report
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
		private bool IsGroupNode(TreeNode ptnNode)
		{
			try
			{
				bool blnResult = false;
				if ((ptnNode != null) && (ptnNode.Tag is sys_ReportGroupVO))
				{
					blnResult = true;
				}
				return blnResult;
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
		///       boolean: is group or report
		///    </Inputs>
		///    <Outputs>
		///       N/A
		///    </Outputs>
		///    <Returns>
		///       Change effected to buttons
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
		private void EnableButtons(TreeNode ptnNode)
		{
			try
			{
				bool blnIsGroup = IsGroupNode(ptnNode);
				if (ptnNode == null)
				{
					btnViewReport.Enabled = false;
					btnAddReport.Enabled = lblF1.Enabled = false;
					btnEditReport.Enabled = false;
					btnCopyReport.Enabled = false;
					btnPasteReport.Enabled = false;
					btnDeleteReport.Enabled = false;
					btnAddGroup.Enabled = true;
					btnEditGroup.Enabled = false;
					btnCopyGroup.Enabled = false;
					btnDeleteGroup.Enabled = false;
					btnMoveUp.Enabled = false;
					btnMoveDown.Enabled = false;
					return;
				}
				btnViewReport.Enabled = (blnIsGroup) ? false : true;
				btnAddReport.Enabled = true; //lblF1.Enabled =  (blnIsGroup) ? true : false;
				btnEditReport.Enabled = (blnIsGroup) ? false : true;
				btnCopyReport.Enabled = (blnIsGroup) ? false : true;
				if (blnIsGroup && (voSelectedReport != null))
				{
					btnPasteReport.Enabled = true;
				}
				else
				{
					btnPasteReport.Enabled = false;
				}
				btnDeleteReport.Enabled = (blnIsGroup) ? false : true;
				//btnAddGroup.Enabled = (blnIsGroup) ? true : false;
				btnEditGroup.Enabled = (blnIsGroup) ? true : false;
				btnCopyGroup.Enabled = (blnIsGroup) ? true : false;
				btnDeleteGroup.Enabled = (blnIsGroup) ? true : false;


				if(IsOnlyOneGroup(tvwReportList,ptnNode))
				{
					OnlyOneLayout();
				}
				else if(IsOnlyOneChild(tvwReportList,ptnNode))
				{
					OnlyOneLayout();
				}
				else
				{			
					if(IsFirstGroup(tvwReportList, ptnNode))
					{
						//w(e.Node.Text + "is FIRST group");
						FirstGroupLayOut();
					}
					else if(IsLastGroup(tvwReportList, ptnNode))
					{
						//w(e.Node.Text + "is LAST group");
						LastGroupLayOut();
					}
					else if(IsFirstChild(tvwReportList, ptnNode))
					{
						//w(e.Node.Text + "is FRIST child");
						FirstChildLayOut();
					}
					else if(IsLastChild(tvwReportList, ptnNode))
					{
						//w(e.Node.Text + "is LAST child");
						LastChildLayOut();
					}
					else
						MiddleLayout();
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		
		
		#region Thachnn report management layout control

		/// <summary>
		/// Thachnn 10/Oct/2005
		/// Don't allow move up group
		/// </summary>
		private void FirstGroupLayOut()
		{
			try
			{			
				btnMoveUp.Enabled = false;
				btnMoveDown.Enabled = true;
			}
			catch(Exception ex)
			{
				throw ex;
			}
		}

		/// <summary>
		/// Thachnn 10/Oct/2005
		/// Don't allow move down group
		/// </summary>
		private void LastGroupLayOut()
		{
			try
			{			
				btnMoveUp.Enabled = true;
				btnMoveDown.Enabled = false;
			}
			catch(Exception ex)
			{
				throw ex;
			}
		}
		
		/// <summary>
		/// Thachnn 10/Oct/2005
		/// Don't allow move up report
		/// </summary>
		private void FirstChildLayOut()
		{
			try
			{			
				btnMoveUp.Enabled = false;
				btnMoveDown.Enabled = true;
			}
			catch(Exception ex)
			{
				throw ex;
			}
		}

		/// <summary>
		/// Thachnn 10/Oct/2005
		/// Don't allow move down report
		/// </summary>
		private void LastChildLayOut()
		{
			try
			{			
				btnMoveUp.Enabled = true;
				btnMoveDown.Enabled = false;
			}
			catch(Exception ex)
			{
				throw ex;
			}
		}

		/// <summary>
		/// Thachnn 10/Oct/2005
		/// Allow move up and move down current node (group or report)
		/// </summary>
		private void MiddleLayout()
		{
			try
			{			
				btnMoveUp.Enabled = true;
				btnMoveDown.Enabled = true;
			}
			catch(Exception ex)
			{
				throw ex;
			}
		}

		/// <summary>
		/// Thachnn 10/Oct/2005
		/// Do not allow to move up or move down current node (group or report)
		/// </summary>
		private void OnlyOneLayout()
		{
			try
			{			
				btnMoveUp.Enabled = false;
				btnMoveDown.Enabled = false;
			}
			catch(Exception ex)
			{
				throw ex;
			}			
		}


		/// <summary>
		/// Thachnn 10/Oct/2005
		/// check whether ptnNOde is First child report in its report group
		/// </summary>
		/// <param name="ptvw">treeview object to check</param>
		/// <param name="ptnNode">tree node to check whether is firstchild in group of the provided treeview</param>
		/// <returns>true if ptnNode is a child report and is First Node in its group</returns>
		private bool IsFirstChild(TreeView ptvw, TreeNode ptnNode)
		{
			bool blnRet = false;
			
			try
			{
				if (IsChild(ptnNode))
				{			
					if(ptvw.Nodes.Count >0 && ptnNode.Parent != null)
					{
						if(IsChild(ptnNode) && ptnNode.Parent.FirstNode.Equals(ptnNode))
						{
							blnRet = true;
						}
					}
				}
			}
			catch(Exception ex)
			{
				throw ex;
			}

			return blnRet;
		}

		
		/// <summary>
		/// Thachnn 10/Oct/2005
		/// check whether ptnNOde is Last child report in its report group
		/// </summary>
		/// <param name="ptvw">treeview object to check</param>
		/// <param name="ptnNode">tree node to check whether is last child in group of the provided treeview</param>
		/// <returns>true if ptnNode is a child report and is last report Node in its group</returns>
		private bool IsLastChild(TreeView ptvw, TreeNode ptnNode)
		{
			bool blnRet = false;
			
			try
			{				
				if (IsChild(ptnNode))
				{
					if(ptvw.Nodes.Count >0 && ptnNode.Parent != null)
					{
						if(IsChild(ptnNode) && ptnNode.Parent.LastNode.Equals(ptnNode))
						{
							blnRet = true;
						}
					}
				}
			}
			catch(Exception ex)
			{
				throw ex;
			}

			return blnRet;
		}

		/// <summary>
		/// Thachnn 10/Oct/2005
		/// check whether ptnNOde is First Group in provided tree view
		/// </summary>
		/// <param name="ptvw">tree view object to check</param>
		/// <param name="ptnNode">tree node object to check</param>
		/// <returns>true if ptnNode is Group and is the first Group in the tree view</returns>
		private bool IsFirstGroup(TreeView ptvw, TreeNode ptnNode)
		{
			bool blnRet = false;
			try
			{
				if(IsGroup(ptnNode) && ptvw.Nodes.Count >0)
				{
					if( ptvw.Nodes[0].Equals(ptnNode))
					{
						blnRet = true;
					}
				}
			}
			catch(Exception ex)
			{
				throw ex;
			}		

			return blnRet;
		}

		/// <summary>
		/// Thachnn 10/Oct/2005
		/// check whether ptnNOde is Last Group in provided tree view
		/// </summary>
		/// <param name="ptvw">tree view object to check</param>
		/// <param name="ptnNode">tree node object to check</param>
		/// <returns>true if ptnNode is Group and is the Last Group in the tree view</returns>
		private bool IsLastGroup(TreeView ptvw, TreeNode ptnNode)
		{
			bool blnRet = false;

			try
			{
				if(IsGroup(ptnNode) && ptvw.Nodes.Count >0)
				{
					if(ptvw.Nodes[ptvw.Nodes.Count-1].Equals(ptnNode))
					{
						blnRet = true;
					}
					else if(IsChild(ptvw.Nodes[ptvw.Nodes.Count-1]))					
					{
						if(ptvw.Nodes[ptvw.Nodes.Count-1].Parent.Equals(ptnNode))
							blnRet = true;
					}
				
				}
			}
			catch(Exception ex)
			{
				throw ex;
			}		

			return blnRet;
		}


		/// <summary>
		/// Thachnn 10/Oct/2005
		/// Check whether ptnNode is the Group node and is the only one Group in the provided tree view
		/// </summary>
		/// <param name="ptvw">tree view object to check</param>
		/// <param name="ptnNode">tree node to check</param>
		/// <returns>true if ptnNode is only one group in the tree view</returns>
		private bool IsOnlyOneGroup(TreeView ptvw, TreeNode ptnNode)
		{
			bool blnRet = false;

			try
			{
				if(ptvw.Nodes.Count  > 0 && IsGroup(ptnNode) && IsFirstGroup(ptvw, ptnNode) && IsLastGroup(ptvw,ptnNode) )
				{				
					blnRet = true;				
				}
			}
			catch(Exception ex)
			{
				throw ex;
			}
			
			return blnRet;
		}

		/// <summary>
		/// Thachnn 10/Oct/2005
		/// Check whether ptnNode is the Child report and is the only one Report in its Group in the provided tree view
		/// </summary>
		/// <param name="ptvw">tree view to check</param>
		/// <param name="ptnNode">report tree node to check</param>
		/// <returns>true if ptnNode is only one child report in its group</returns>
		private bool IsOnlyOneChild(TreeView ptvw, TreeNode ptnNode)
		{
			bool blnRet = false;

			try
			{
				if(ptvw.Nodes.Count  > 0 && IsChild(ptnNode) && IsFirstChild(ptvw, ptnNode) && IsLastChild(ptvw,ptnNode) )
				{				
					blnRet = true;				
				}
			}
			catch(Exception ex)
			{
				throw ex;
			}		

			return blnRet;
		}

		
		/// <summary>
		/// Thachnn 10/Oct/2005
		/// check whether provided node is a Group
		/// </summary>
		/// <param name="ptnNode">tree node to check</param>
		/// <returns>true if ptnNode is a group</returns>
		private bool IsGroup(TreeNode ptnNode)
		{
			try
			{
				return IsGroupNode(ptnNode);
			}
			catch(Exception ex)
			{
				throw ex;
			}			
		}

		/// <summary>
		/// Thachnn 10/Oct/2005
		/// check whether provided node is a Report (not a group)
		/// </summary>
		/// <param name="ptnNode">tree node to check</param>
		/// <returns>true if ptnNode is a report</returns>
		private bool IsChild(TreeNode ptnNode)
		{
			try
			{
				return !IsGroup(ptnNode);
			}
			catch(Exception ex)
			{
				throw ex;
			}			
		}
		#endregion	Thachnn report management layout control


		#endregion	

	}
}
