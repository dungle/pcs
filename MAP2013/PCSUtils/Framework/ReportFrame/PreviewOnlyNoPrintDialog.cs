using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using C1.Win.C1Preview;

namespace PCSUtils.Framework.ReportFrame
{
	/// <summary>
	/// PrintPreview only. User cannot print report from here.
	/// </summary>
	public class PreviewOnlyNoPrintDialog : System.Windows.Forms.Form
	{
		#region Declaration
		/// <summary>
		/// Required designer variable.
		/// </summary>		
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.Label lblFormTitle;
        private C1PrintPreviewControl mrpvReportViewer;
		private System.Windows.Forms.ToolBarButton c1pBtnClose;

		#endregion Declaration

		#region Constructors, Destructors


		/// <summary>
		/// Default constructor
		/// </summary>
		public PreviewOnlyNoPrintDialog()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();		
		}
		
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

		#endregion Constructors, Destructors		

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PreviewOnlyNoPrintDialog));
            this.lblFormTitle = new System.Windows.Forms.Label();
            this.c1pBtnClose = new System.Windows.Forms.ToolBarButton();
            this.mrpvReportViewer = new C1.Win.C1Preview.C1PrintPreviewControl();
            ((System.ComponentModel.ISupportInitialize)(this.mrpvReportViewer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mrpvReportViewer.PreviewPane)).BeginInit();
            this.mrpvReportViewer.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblFormTitle
            // 
            this.lblFormTitle.Location = new System.Drawing.Point(280, 0);
            this.lblFormTitle.Name = "lblFormTitle";
            this.lblFormTitle.Size = new System.Drawing.Size(72, 16);
            this.lblFormTitle.TabIndex = 1;
            this.lblFormTitle.Text = "Print Preview";
            this.lblFormTitle.Visible = false;
            // 
            // c1pBtnClose
            // 
            this.c1pBtnClose.ImageIndex = 5;
            this.c1pBtnClose.Name = "c1pBtnClose";
            this.c1pBtnClose.ToolTipText = "Close";
            // 
            // mrpvReportViewer
            // 
            this.mrpvReportViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mrpvReportViewer.Location = new System.Drawing.Point(0, 0);
            this.mrpvReportViewer.Name = "mrpvReportViewer";
            // 
            // mrpvReportViewer.OutlineView
            // 
            this.mrpvReportViewer.PreviewOutlineView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mrpvReportViewer.PreviewOutlineView.Location = new System.Drawing.Point(0, 0);
            this.mrpvReportViewer.PreviewOutlineView.Name = "OutlineView";
            this.mrpvReportViewer.PreviewOutlineView.Size = new System.Drawing.Size(165, 427);
            this.mrpvReportViewer.PreviewOutlineView.TabIndex = 0;
            // 
            // mrpvReportViewer.PreviewPane
            // 
            this.mrpvReportViewer.PreviewPane.IntegrateExternalTools = true;
            this.mrpvReportViewer.PreviewPane.TabIndex = 0;
            // 
            // mrpvReportViewer.PreviewTextSearchPanel
            // 
            this.mrpvReportViewer.PreviewTextSearchPanel.Dock = System.Windows.Forms.DockStyle.Right;
            this.mrpvReportViewer.PreviewTextSearchPanel.Location = new System.Drawing.Point(530, 0);
            this.mrpvReportViewer.PreviewTextSearchPanel.MinimumSize = new System.Drawing.Size(200, 240);
            this.mrpvReportViewer.PreviewTextSearchPanel.Name = "PreviewTextSearchPanel";
            this.mrpvReportViewer.PreviewTextSearchPanel.Size = new System.Drawing.Size(200, 453);
            this.mrpvReportViewer.PreviewTextSearchPanel.TabIndex = 0;
            this.mrpvReportViewer.PreviewTextSearchPanel.Visible = false;
            // 
            // mrpvReportViewer.ThumbnailView
            // 
            this.mrpvReportViewer.PreviewThumbnailView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mrpvReportViewer.PreviewThumbnailView.Location = new System.Drawing.Point(0, 0);
            this.mrpvReportViewer.PreviewThumbnailView.Name = "ThumbnailView";
            this.mrpvReportViewer.PreviewThumbnailView.Size = new System.Drawing.Size(165, 400);
            this.mrpvReportViewer.PreviewThumbnailView.TabIndex = 0;
            this.mrpvReportViewer.PreviewThumbnailView.UseImageAsThumbnail = false;
            this.mrpvReportViewer.Size = new System.Drawing.Size(689, 473);
            this.mrpvReportViewer.TabIndex = 2;
            this.mrpvReportViewer.Text = "c1PrintPreviewControl1";
            // 
            // 
            // 
            this.mrpvReportViewer.ToolBars.Page.FacingContinuous.Image = ((System.Drawing.Image)(resources.GetObject("c1PrintPreviewControl1.ToolBars.Page.FacingContinuous.Image")));
            this.mrpvReportViewer.ToolBars.Page.FacingContinuous.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mrpvReportViewer.ToolBars.Page.FacingContinuous.Name = "btnPageFacingContinuous";
            this.mrpvReportViewer.ToolBars.Page.FacingContinuous.Size = new System.Drawing.Size(23, 22);
            this.mrpvReportViewer.ToolBars.Page.FacingContinuous.Tag = "C1PreviewActionEnum.PageFacingContinuous";
            this.mrpvReportViewer.ToolBars.Page.FacingContinuous.ToolTipText = "Pages Facing Continuous View";
            // 
            // 
            // 
            this.mrpvReportViewer.ToolBars.Text.Find.Image = ((System.Drawing.Image)(resources.GetObject("c1PrintPreviewControl1.ToolBars.Text.Find.Image")));
            this.mrpvReportViewer.ToolBars.Text.Find.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mrpvReportViewer.ToolBars.Text.Find.Name = "btnFind";
            this.mrpvReportViewer.ToolBars.Text.Find.Size = new System.Drawing.Size(23, 22);
            this.mrpvReportViewer.ToolBars.Text.Find.Tag = "C1PreviewActionEnum.Find";
            this.mrpvReportViewer.ToolBars.Text.Find.ToolTipText = "Find Text";
            // 
            // 
            // 
            this.mrpvReportViewer.ToolBars.Text.SelectText.Image = ((System.Drawing.Image)(resources.GetObject("c1PrintPreviewControl1.ToolBars.Text.SelectText.Image")));
            this.mrpvReportViewer.ToolBars.Text.SelectText.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mrpvReportViewer.ToolBars.Text.SelectText.Name = "btnSelectTextTool";
            this.mrpvReportViewer.ToolBars.Text.SelectText.Size = new System.Drawing.Size(23, 22);
            this.mrpvReportViewer.ToolBars.Text.SelectText.Tag = "C1PreviewActionEnum.SelectTextTool";
            this.mrpvReportViewer.ToolBars.Text.SelectText.ToolTipText = "Text Select Tool";
            // 
            // PreviewOnlyNoPrintDialog
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(689, 473);
            this.Controls.Add(this.mrpvReportViewer);
            this.Controls.Add(this.lblFormTitle);
            this.KeyPreview = true;
            this.Name = "PreviewOnlyNoPrintDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Print Preview";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.mrpvReportViewer.PreviewPane)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mrpvReportViewer)).EndInit();
            this.mrpvReportViewer.ResumeLayout(false);
            this.mrpvReportViewer.PerformLayout();
            this.ResumeLayout(false);

		}
		#endregion		

		#region Properties

		public C1PrintPreviewControl ReportViewer
		{
			get
			{
				return mrpvReportViewer;
			}
			set
			{
				mrpvReportViewer = value;
			}
		}


		private string mstrFormTitle;
		public string FormTitle
		{
			get
			{
				return mstrFormTitle;
			}
			set
			{
				mstrFormTitle = value;
				this.Text = string.Format("{0} - {1}", this.lblFormTitle.Text, mstrFormTitle);
			}
		}        
		
		#endregion Properties
	}
}
