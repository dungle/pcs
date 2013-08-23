using System;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using PCSComUtils.Common;
using PCSComUtils.Framework.ReportFrame.BO;
using PCSComUtils.Framework.ReportFrame.DS;
using PCSComUtils.PCSExc;
using PCSUtils.Log;
using PCSUtils.Utils;

namespace PCSUtils.Framework.ReportFrame
{
	/// <summary>
	/// Summary description for LastReport.
	/// </summary>
	public class LastReport : Form
	{

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private Container components = null;
		private System.Windows.Forms.TreeView tvwReportList;

		private const string THIS = "PCSUtils.Framework.ReportFrame.LastReport";

		// read-only property
		public sys_ReportHistoryVO ReturnValue
		{
			get { return mReturnValue; }
		}

		private sys_ReportHistoryVO mReturnValue;

		public LastReport()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(LastReport));
			this.tvwReportList = new System.Windows.Forms.TreeView();
			this.SuspendLayout();
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
			this.tvwReportList.ImageIndex = ((int)(resources.GetObject("tvwReportList.ImageIndex")));
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
			this.tvwReportList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.LastReport_KeyDown);
			this.tvwReportList.DoubleClick += new System.EventHandler(this.tvwReportList_DoubleClick);
			// 
			// LastReport
			// 
			this.AccessibleDescription = resources.GetString("$this.AccessibleDescription");
			this.AccessibleName = resources.GetString("$this.AccessibleName");
			this.AutoScaleBaseSize = ((System.Drawing.Size)(resources.GetObject("$this.AutoScaleBaseSize")));
			this.AutoScroll = ((bool)(resources.GetObject("$this.AutoScroll")));
			this.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("$this.AutoScrollMargin")));
			this.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("$this.AutoScrollMinSize")));
			this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
			this.ClientSize = ((System.Drawing.Size)(resources.GetObject("$this.ClientSize")));
			this.Controls.Add(this.tvwReportList);
			this.Enabled = ((bool)(resources.GetObject("$this.Enabled")));
			this.Font = ((System.Drawing.Font)(resources.GetObject("$this.Font")));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("$this.ImeMode")));
			this.KeyPreview = true;
			this.Location = ((System.Drawing.Point)(resources.GetObject("$this.Location")));
			this.MaximumSize = ((System.Drawing.Size)(resources.GetObject("$this.MaximumSize")));
			this.MinimumSize = ((System.Drawing.Size)(resources.GetObject("$this.MinimumSize")));
			this.Name = "LastReport";
			this.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("$this.RightToLeft")));
			this.StartPosition = ((System.Windows.Forms.FormStartPosition)(resources.GetObject("$this.StartPosition")));
			this.Text = resources.GetString("$this.Text");
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.LastReport_KeyDown);
			this.Load += new System.EventHandler(this.LastReport_Load);
			this.ResumeLayout(false);

		}

		#endregion

		private void LastReport_Load(object sender, EventArgs e)
		{
			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.WaitCursor;

			#endregion Code Inserted Automatically

			
			const string METHOD_NAME = THIS + ".LastReport_Load()";
			const string SEPARATOR = " - ";
			const string DATE_FORMAT = "dd-MM-yyyy hh:mm:ss";
			try
			{
				// get last 10 report executed by user
				ArrayList arrObjects = new ArrayList();
				LastReportBO boLastReport = new LastReportBO();
				ReportManagementBO boReportManagement = new ReportManagementBO();
				arrObjects = boLastReport.GetLast10Report(SystemProperty.UserName);

				// bind to list
				sys_ReportHistoryVO voReportHistory;
				string strReportName = string.Empty;
				TreeNode tnNode;
				for (int i = 0; i < arrObjects.Count; i++)
				{
					voReportHistory = (sys_ReportHistoryVO)arrObjects[i];
					strReportName = boReportManagement.GetReportName(voReportHistory.ReportID);
					tnNode = new TreeNode(Constants.OPEN_SBRACKET + voReportHistory.ExecDateTime.ToString(DATE_FORMAT) + 
						Constants.CLOSE_SBRACKET + SEPARATOR + strReportName);
					tnNode.Tag = voReportHistory;
					tvwReportList.Nodes.Add(tnNode);
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

			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.Default;

			#endregion Code Inserted Automatically

			
		}

		private void LastReport_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				// get selected report data
				if (tvwReportList.SelectedNode != null)
				{
					// return to parent
					this.mReturnValue = (sys_ReportHistoryVO)tvwReportList.SelectedNode.Tag;
					// close the form
					this.Close();
				}
			}
			if (e.KeyCode == Keys.Escape)
			{
				this.Close();
			}
		}

		private void tvwReportList_DoubleClick(object sender, EventArgs e)
		{
			this.LastReport_KeyDown(this, new KeyEventArgs(Keys.Enter));
		}
	}
}