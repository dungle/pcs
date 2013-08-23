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
	/// Summary description for ReportList.
	/// </summary>
	public class ReportList : Form
	{
		private ListBox lstReportList;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private Container components = null;

		private const string THIS = "PCSUtils.Framework.ReportFrame.ReportList";

		// ID of master report
		private string strMasterID = string.Empty;
		// search by report code or report name
		private string strSearchBy = string.Empty;
		
		private sys_ReportVO mvoReport;

		private ArrayList arrReports = new ArrayList();

		/// <summary>
		/// Return selected report
		/// </summary>
		public sys_ReportVO SelectedReport
		{
			get { return this.mvoReport; }
		}

		//**************************************************************************              
		///    <Description>
		///       Defaut constructor with parameter
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
		public ReportList(string pstrMasterID, string pstrSearchBy)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			strMasterID = pstrMasterID;
			strSearchBy = pstrSearchBy;
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(ReportList));
			this.lstReportList = new System.Windows.Forms.ListBox();
			this.SuspendLayout();
			// 
			// lstReportList
			// 
			this.lstReportList.AccessibleDescription = resources.GetString("lstReportList.AccessibleDescription");
			this.lstReportList.AccessibleName = resources.GetString("lstReportList.AccessibleName");
			this.lstReportList.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lstReportList.Anchor")));
			this.lstReportList.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("lstReportList.BackgroundImage")));
			this.lstReportList.ColumnWidth = ((int)(resources.GetObject("lstReportList.ColumnWidth")));
			this.lstReportList.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lstReportList.Dock")));
			this.lstReportList.Enabled = ((bool)(resources.GetObject("lstReportList.Enabled")));
			this.lstReportList.Font = ((System.Drawing.Font)(resources.GetObject("lstReportList.Font")));
			this.lstReportList.HorizontalExtent = ((int)(resources.GetObject("lstReportList.HorizontalExtent")));
			this.lstReportList.HorizontalScrollbar = ((bool)(resources.GetObject("lstReportList.HorizontalScrollbar")));
			this.lstReportList.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lstReportList.ImeMode")));
			this.lstReportList.IntegralHeight = ((bool)(resources.GetObject("lstReportList.IntegralHeight")));
			this.lstReportList.ItemHeight = ((int)(resources.GetObject("lstReportList.ItemHeight")));
			this.lstReportList.Location = ((System.Drawing.Point)(resources.GetObject("lstReportList.Location")));
			this.lstReportList.Name = "lstReportList";
			this.lstReportList.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lstReportList.RightToLeft")));
			this.lstReportList.ScrollAlwaysVisible = ((bool)(resources.GetObject("lstReportList.ScrollAlwaysVisible")));
			this.lstReportList.Size = ((System.Drawing.Size)(resources.GetObject("lstReportList.Size")));
			this.lstReportList.TabIndex = ((int)(resources.GetObject("lstReportList.TabIndex")));
			this.lstReportList.Visible = ((bool)(resources.GetObject("lstReportList.Visible")));
			this.lstReportList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ReportList_KeyDown);
			this.lstReportList.DoubleClick += new System.EventHandler(this.lstReportList_DoubleClick);
			// 
			// ReportList
			// 
			this.AccessibleDescription = resources.GetString("$this.AccessibleDescription");
			this.AccessibleName = resources.GetString("$this.AccessibleName");
			this.AutoScaleBaseSize = ((System.Drawing.Size)(resources.GetObject("$this.AutoScaleBaseSize")));
			this.AutoScroll = ((bool)(resources.GetObject("$this.AutoScroll")));
			this.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("$this.AutoScrollMargin")));
			this.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("$this.AutoScrollMinSize")));
			this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
			this.ClientSize = ((System.Drawing.Size)(resources.GetObject("$this.ClientSize")));
			this.Controls.Add(this.lstReportList);
			this.Enabled = ((bool)(resources.GetObject("$this.Enabled")));
			this.Font = ((System.Drawing.Font)(resources.GetObject("$this.Font")));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.KeyPreview = true;
			this.Location = ((System.Drawing.Point)(resources.GetObject("$this.Location")));
			this.MaximumSize = ((System.Drawing.Size)(resources.GetObject("$this.MaximumSize")));
			this.MinimumSize = ((System.Drawing.Size)(resources.GetObject("$this.MinimumSize")));
			this.Name = "ReportList";
			this.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("$this.RightToLeft")));
			this.StartPosition = ((System.Windows.Forms.FormStartPosition)(resources.GetObject("$this.StartPosition")));
			this.Text = resources.GetString("$this.Text");
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ReportList_KeyDown);
			this.Load += new System.EventHandler(this.ReportList_Load);
			this.ResumeLayout(false);

		}

		#endregion

		//**************************************************************************              
		///    <Description>
		///       When load the form, get all data from sys_Report table and fill to list
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
		private void ReportList_Load(object sender, EventArgs e)
		{
			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.WaitCursor;

			#endregion Code Inserted Automatically

			
			const string METHOD_NAME = THIS + ".ReportList_Load()";
			try
			{
				ReportManagementBO boReportManagement = new ReportManagementBO();
				arrReports = boReportManagement.GetAllReports();
				if (arrReports.Count > 0)
				{
					sys_ReportVO voReport;
					for (int i = 0; i < arrReports.Count; i++)
					{
						voReport = (sys_ReportVO) arrReports[i];
						// add to list
						if (!voReport.ReportID.Equals(this.strMasterID))
						{
							if (this.strSearchBy.Equals(sys_ReportTable.REPORTID_FLD))
							{
								lstReportList.Items.Add(voReport.ReportID);
							}
							else
							{
								lstReportList.Items.Add(voReport.ReportName);
							}
						}
					}
					this.lstReportList.Focus();
					this.lstReportList.Select();
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

		private void ReportList_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Escape)
			{
				this.Close();
			}
			if (e.KeyCode == Keys.Enter)
			{
				// return selected report for drill down form
				if (lstReportList.SelectedItem != null)
				{
					for (int i = 0; i < arrReports.Count; i++)
					{
						try
						{
							this.mvoReport = (sys_ReportVO) arrReports[i];
						}
						catch // catch InvalidCastException
						{
							PCSMessageBox.Show(ErrorCode.MESSAGE_DATA_CAST);
						}
						// search by report id
						if (strSearchBy.Equals(sys_ReportTable.REPORTID_FLD))
						{
							if (mvoReport.ReportID.Equals(lstReportList.SelectedItem.ToString()))
							{
								break;
							}
						}
							// search by report name
						else
						{
							if (mvoReport.ReportName.Equals(lstReportList.SelectedItem.ToString()))
							{
								break;
							}
						}
					}
					// close the form
					this.Close();
				}
			}
		}

		private void lstReportList_DoubleClick(object sender, System.EventArgs e)
		{
			this.ReportList_KeyDown(this, new KeyEventArgs(Keys.Enter));
		}
	}
}