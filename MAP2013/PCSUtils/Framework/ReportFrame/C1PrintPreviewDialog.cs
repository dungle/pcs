using System.Linq;
using System.Text;
using System.Windows.Forms;
using C1.C1Preview;
using C1.C1Report;
using C1.Win.C1Preview;

namespace PCSUtils.Framework.ReportFrame
{
	/// <summary>
	/// Summary description for C1PrintPreviewDialog.
	/// </summary>
	public class C1PrintPreviewDialog : Form
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components;
		private Label lblFormTitle;
		private ToolBarButton c1pBtnClose;
        private C1PrintPreviewControl ReportViewerControl;
		
        #region USER DEFINED
		public C1PrintPreviewControl ReportViewer
		{
			get
			{
                ReportViewerControl.PreviewNavigationPanel.Visible = false;
                ReportViewerControl.PreviewOutlineView.Visible = false;
                ReportViewerControl.PreviewPane.ZoomMode = ZoomModeEnum.PageWidth;
				return ReportViewerControl;
			}
			set
			{
				ReportViewerControl = value;
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
				this.Text = string.Format("{0} - {1}",this.lblFormTitle.Text,mstrFormTitle);
			}
		}

        public bool HandlePrintEvent { get; set; }

        public C1Report Report { get; set; }
        
		#endregion

        /// <summary>
		/// Default constructor
		/// </summary>
		public C1PrintPreviewDialog()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();		
		}
		
        #region GENERATE CODE SECTION		

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(C1PrintPreviewDialog));
            this.lblFormTitle = new System.Windows.Forms.Label();
            this.c1pBtnClose = new System.Windows.Forms.ToolBarButton();
            this.ReportViewerControl = new C1.Win.C1Preview.C1PrintPreviewControl();
            ((System.ComponentModel.ISupportInitialize)(this.ReportViewerControl)).BeginInit();
            this.ReportViewerControl.SuspendLayout();
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
            // ReportViewerControl
            // 
            this.ReportViewerControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ReportViewerControl.Location = new System.Drawing.Point(0, 0);
            this.ReportViewerControl.Name = "ReportViewerControl";
            // 
            // ReportViewerControl.OutlineView
            // 
            this.ReportViewerControl.PreviewOutlineView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ReportViewerControl.PreviewOutlineView.Location = new System.Drawing.Point(0, 0);
            this.ReportViewerControl.PreviewOutlineView.Name = "OutlineView";
            this.ReportViewerControl.PreviewOutlineView.Size = new System.Drawing.Size(165, 427);
            this.ReportViewerControl.PreviewOutlineView.TabIndex = 0;
            this.ReportViewerControl.PreviewOutlineView.Visible = false;
            // 
            // ReportViewerControl.PreviewPane
            // 
            this.ReportViewerControl.PreviewPane.IntegrateExternalTools = true;
            this.ReportViewerControl.PreviewPane.TabIndex = 0;
            // 
            // ReportViewerControl.PreviewTextSearchPanel
            // 
            this.ReportViewerControl.PreviewTextSearchPanel.Dock = System.Windows.Forms.DockStyle.Right;
            this.ReportViewerControl.PreviewTextSearchPanel.Location = new System.Drawing.Point(530, 0);
            this.ReportViewerControl.PreviewTextSearchPanel.MinimumSize = new System.Drawing.Size(200, 240);
            this.ReportViewerControl.PreviewTextSearchPanel.Name = "PreviewTextSearchPanel";
            this.ReportViewerControl.PreviewTextSearchPanel.Size = new System.Drawing.Size(200, 453);
            this.ReportViewerControl.PreviewTextSearchPanel.TabIndex = 0;
            this.ReportViewerControl.PreviewTextSearchPanel.Visible = false;
            // 
            // ReportViewerControl.ThumbnailView
            // 
            this.ReportViewerControl.PreviewThumbnailView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ReportViewerControl.PreviewThumbnailView.Location = new System.Drawing.Point(0, 0);
            this.ReportViewerControl.PreviewThumbnailView.Name = "ThumbnailView";
            this.ReportViewerControl.PreviewThumbnailView.Size = new System.Drawing.Size(165, 324);
            this.ReportViewerControl.PreviewThumbnailView.TabIndex = 0;
            this.ReportViewerControl.PreviewThumbnailView.UseImageAsThumbnail = false;
            this.ReportViewerControl.Size = new System.Drawing.Size(648, 397);
            this.ReportViewerControl.TabIndex = 2;
            this.ReportViewerControl.Text = "c1PrintPreviewControl1";
            // 
            // 
            // 
            this.ReportViewerControl.ToolBars.Page.Continuous.Checked = true;
            this.ReportViewerControl.ToolBars.Page.Continuous.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ReportViewerControl.ToolBars.Page.Continuous.Image = ((System.Drawing.Image)(resources.GetObject("mppvReportViewer.ToolBars.Page.Continuous.Image")));
            this.ReportViewerControl.ToolBars.Page.Continuous.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ReportViewerControl.ToolBars.Page.Continuous.Name = "btnPageContinuous";
            this.ReportViewerControl.ToolBars.Page.Continuous.Size = new System.Drawing.Size(23, 22);
            this.ReportViewerControl.ToolBars.Page.Continuous.Tag = "C1PreviewActionEnum.PageContinuous";
            this.ReportViewerControl.ToolBars.Page.Continuous.ToolTipText = "Continuous View";
            // 
            // 
            // 
            this.ReportViewerControl.ToolBars.Page.Facing.Image = ((System.Drawing.Image)(resources.GetObject("mppvReportViewer.ToolBars.Page.Facing.Image")));
            this.ReportViewerControl.ToolBars.Page.Facing.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ReportViewerControl.ToolBars.Page.Facing.Name = "btnPageFacing";
            this.ReportViewerControl.ToolBars.Page.Facing.Size = new System.Drawing.Size(23, 22);
            this.ReportViewerControl.ToolBars.Page.Facing.Tag = "C1PreviewActionEnum.PageFacing";
            this.ReportViewerControl.ToolBars.Page.Facing.ToolTipText = "Pages Facing View";
            // 
            // 
            // 
            this.ReportViewerControl.ToolBars.Page.FacingContinuous.Image = ((System.Drawing.Image)(resources.GetObject("mppvReportViewer.ToolBars.Page.FacingContinuous.Image")));
            this.ReportViewerControl.ToolBars.Page.FacingContinuous.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ReportViewerControl.ToolBars.Page.FacingContinuous.Name = "btnPageFacingContinuous";
            this.ReportViewerControl.ToolBars.Page.FacingContinuous.Size = new System.Drawing.Size(23, 22);
            this.ReportViewerControl.ToolBars.Page.FacingContinuous.Tag = "C1PreviewActionEnum.PageFacingContinuous";
            this.ReportViewerControl.ToolBars.Page.FacingContinuous.ToolTipText = "Pages Facing Continuous View";
            // 
            // 
            // 
            this.ReportViewerControl.ToolBars.Text.Find.Image = ((System.Drawing.Image)(resources.GetObject("mppvReportViewer.ToolBars.Text.Find.Image")));
            this.ReportViewerControl.ToolBars.Text.Find.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ReportViewerControl.ToolBars.Text.Find.Name = "btnFind";
            this.ReportViewerControl.ToolBars.Text.Find.Size = new System.Drawing.Size(23, 20);
            this.ReportViewerControl.ToolBars.Text.Find.Tag = "C1PreviewActionEnum.Find";
            this.ReportViewerControl.ToolBars.Text.Find.ToolTipText = "Find Text";
            // 
            // 
            // 
            this.ReportViewerControl.ToolBars.Text.SelectText.Image = ((System.Drawing.Image)(resources.GetObject("mppvReportViewer.ToolBars.Text.SelectText.Image")));
            this.ReportViewerControl.ToolBars.Text.SelectText.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ReportViewerControl.ToolBars.Text.SelectText.Name = "btnSelectTextTool";
            this.ReportViewerControl.ToolBars.Text.SelectText.Size = new System.Drawing.Size(23, 20);
            this.ReportViewerControl.ToolBars.Text.SelectText.Tag = "C1PreviewActionEnum.SelectTextTool";
            this.ReportViewerControl.ToolBars.Text.SelectText.ToolTipText = "Text Select Tool";
            // 
            // C1PrintPreviewDialog
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(648, 397);
            this.Controls.Add(this.ReportViewerControl);
            this.Controls.Add(this.lblFormTitle);
            this.Name = "C1PrintPreviewDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Print Preview";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.C1PrintPreviewDialog_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ReportViewerControl)).EndInit();
            this.ReportViewerControl.ResumeLayout(false);
            this.ReportViewerControl.PerformLayout();
            this.ResumeLayout(false);

		}
		#endregion

        private void C1PrintPreviewDialog_Load(object sender, System.EventArgs e)
        {
            if (HandlePrintEvent)
            {
                ReportViewerControl.PreviewPane.PreviewPrint += new PreviewPrintEventHandler(PreviewPane_PreviewPrint);
            }
        }

        void PreviewPane_PreviewPrint(object sender, PreviewPrintEventArgs e)
        {
            if (HandlePrintEvent)
            {
                PrintHandler();
                e.PrintDialog.Document = Report.Document;
            }
        }


        void PrintHandler()
        {
            var script = new StringBuilder();

            #region Header section

            foreach (Field field in Report.Sections.Header.Fields.Cast<Field>().Where(field => field.Tag == null))
            {
                script.AppendLine(string.Format("{0}.Visible = False", field.Name));
            }
            Report.Sections.Header.OnPrint = script.ToString();

            #endregion

            #region Page Header section

            script = new StringBuilder();
            foreach (Field field in Report.Sections.PageHeader.Fields.Cast<Field>().Where(field => field.Tag == null))
            {
                script.AppendLine(string.Format("{0}.Visible = False", field.Name));
            }
            Report.Sections.PageHeader.OnPrint = script.ToString();

            #endregion

            #region Group Header section

            script = new StringBuilder();
            if (Report.Groups.Count > 0)
            {
                foreach (Field field in Report.Groups[0].SectionHeader.Fields.Cast<Field>().Where(field => field.Tag == null))
                {
                    script.AppendLine(string.Format("{0}.Visible = False", field.Name));
                }
                Report.Groups[0].SectionHeader.OnPrint = script.ToString();
            }

            #endregion

            #region Page Footer section

            script = new StringBuilder();
            foreach (Field field in Report.Sections.PageFooter.Fields.Cast<Field>().Where(field => field.Tag == null))
            {
                script.AppendLine(string.Format("{0}.Visible = False", field.Name));
            }
            Report.Sections.PageFooter.OnPrint = script.ToString();

            #endregion

            // re-render report
            Report.Render();
        }
	
		#endregion
	}
}
