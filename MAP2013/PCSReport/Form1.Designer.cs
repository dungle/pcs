namespace PCSReport
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.c1Combo2 = new C1.Win.C1List.C1Combo();
            this.c1TrueDBGrid2 = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
            this.c1Report1 = new C1.C1Report.C1Report();
            this.c1PrintDocument1 = new C1.C1Preview.C1PrintDocument();
            this.c1PrintPreviewControl1 = new C1.Win.C1Preview.C1PrintPreviewControl();
            this.c1PrintPreviewDialog1 = new C1.Win.C1Preview.C1PrintPreviewDialog();
            ((System.ComponentModel.ISupportInitialize)(this.c1Combo2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1TrueDBGrid2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1Report1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1PrintDocument1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1PrintPreviewControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1PrintPreviewControl1.PreviewPane)).BeginInit();
            this.c1PrintPreviewControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1PrintPreviewDialog1.PrintPreviewControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1PrintPreviewDialog1.PrintPreviewControl.PreviewPane)).BeginInit();
            this.c1PrintPreviewDialog1.PrintPreviewControl.SuspendLayout();
            this.c1PrintPreviewDialog1.SuspendLayout();
            this.SuspendLayout();
            // 
            // c1Combo2
            // 
            this.c1Combo2.AddItemSeparator = ';';
            this.c1Combo2.Caption = "";
            this.c1Combo2.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.c1Combo2.DeadAreaBackColor = System.Drawing.Color.Empty;
            this.c1Combo2.EditorBackColor = System.Drawing.SystemColors.Window;
            this.c1Combo2.EditorFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.c1Combo2.EditorForeColor = System.Drawing.SystemColors.WindowText;
            this.c1Combo2.Images.Add(((System.Drawing.Image)(resources.GetObject("c1Combo2.Images"))));
            this.c1Combo2.Location = new System.Drawing.Point(47, 25);
            this.c1Combo2.MatchEntryTimeout = ((long)(2000));
            this.c1Combo2.MaxDropDownItems = ((short)(5));
            this.c1Combo2.MaxLength = 32767;
            this.c1Combo2.MouseCursor = System.Windows.Forms.Cursors.Default;
            this.c1Combo2.Name = "c1Combo2";
            this.c1Combo2.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None;
            this.c1Combo2.RowSubDividerColor = System.Drawing.Color.DarkGray;
            this.c1Combo2.Size = new System.Drawing.Size(121, 21);
            this.c1Combo2.TabIndex = 0;
            this.c1Combo2.Text = "c1Combo2";
            this.c1Combo2.PropBag = resources.GetString("c1Combo2.PropBag");
            // 
            // c1TrueDBGrid2
            // 
            this.c1TrueDBGrid2.Images.Add(((System.Drawing.Image)(resources.GetObject("c1TrueDBGrid2.Images"))));
            this.c1TrueDBGrid2.Location = new System.Drawing.Point(113, 380);
            this.c1TrueDBGrid2.Name = "c1TrueDBGrid2";
            this.c1TrueDBGrid2.PreviewInfo.Location = new System.Drawing.Point(0, 0);
            this.c1TrueDBGrid2.PreviewInfo.Size = new System.Drawing.Size(0, 0);
            this.c1TrueDBGrid2.PreviewInfo.ZoomFactor = 75D;
            this.c1TrueDBGrid2.PrintInfo.PageSettings = ((System.Drawing.Printing.PageSettings)(resources.GetObject("c1TrueDBGrid2.PrintInfo.PageSettings")));
            this.c1TrueDBGrid2.Size = new System.Drawing.Size(240, 150);
            this.c1TrueDBGrid2.TabIndex = 1;
            this.c1TrueDBGrid2.Text = "c1TrueDBGrid2";
            this.c1TrueDBGrid2.PropBag = resources.GetString("c1TrueDBGrid2.PropBag");
            // 
            // c1Report1
            // 
            this.c1Report1.ReportDefinition = resources.GetString("c1Report1.ReportDefinition");
            this.c1Report1.ReportName = "c1Report1";
            // 
            // c1PrintDocument1
            // 
            this.c1PrintDocument1.PageLayouts.Default.PageSettings = new C1.C1Preview.C1PageSettings(false, System.Drawing.Printing.PaperKind.Letter, false, "1in", "1in", "1in", "1in", System.Drawing.Printing.PaperSourceKind.FormSource, 15, null, System.Drawing.Printing.PrinterResolutionKind.Custom, 600, 600);
            // 
            // c1PrintPreviewControl1
            // 
            this.c1PrintPreviewControl1.Location = new System.Drawing.Point(74, 242);
            this.c1PrintPreviewControl1.Name = "c1PrintPreviewControl1";
            // 
            // c1PrintPreviewControl1.OutlineView
            // 
            this.c1PrintPreviewControl1.PreviewOutlineView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1PrintPreviewControl1.PreviewOutlineView.Location = new System.Drawing.Point(0, 0);
            this.c1PrintPreviewControl1.PreviewOutlineView.Name = "OutlineView";
            this.c1PrintPreviewControl1.PreviewOutlineView.Size = new System.Drawing.Size(165, 427);
            this.c1PrintPreviewControl1.PreviewOutlineView.TabIndex = 0;
            // 
            // c1PrintPreviewControl1.PreviewPane
            // 
            this.c1PrintPreviewControl1.PreviewPane.IntegrateExternalTools = true;
            this.c1PrintPreviewControl1.PreviewPane.TabIndex = 0;
            // 
            // c1PrintPreviewControl1.PreviewTextSearchPanel
            // 
            this.c1PrintPreviewControl1.PreviewTextSearchPanel.Dock = System.Windows.Forms.DockStyle.Right;
            this.c1PrintPreviewControl1.PreviewTextSearchPanel.Location = new System.Drawing.Point(530, 0);
            this.c1PrintPreviewControl1.PreviewTextSearchPanel.MinimumSize = new System.Drawing.Size(200, 240);
            this.c1PrintPreviewControl1.PreviewTextSearchPanel.Name = "PreviewTextSearchPanel";
            this.c1PrintPreviewControl1.PreviewTextSearchPanel.Size = new System.Drawing.Size(200, 453);
            this.c1PrintPreviewControl1.PreviewTextSearchPanel.TabIndex = 0;
            this.c1PrintPreviewControl1.PreviewTextSearchPanel.Visible = false;
            // 
            // c1PrintPreviewControl1.ThumbnailView
            // 
            this.c1PrintPreviewControl1.PreviewThumbnailView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1PrintPreviewControl1.PreviewThumbnailView.Location = new System.Drawing.Point(0, 0);
            this.c1PrintPreviewControl1.PreviewThumbnailView.Name = "ThumbnailView";
            this.c1PrintPreviewControl1.PreviewThumbnailView.Size = new System.Drawing.Size(165, 427);
            this.c1PrintPreviewControl1.PreviewThumbnailView.TabIndex = 0;
            this.c1PrintPreviewControl1.PreviewThumbnailView.UseImageAsThumbnail = false;
            this.c1PrintPreviewControl1.Size = new System.Drawing.Size(730, 500);
            this.c1PrintPreviewControl1.TabIndex = 2;
            this.c1PrintPreviewControl1.Text = "c1PrintPreviewControl1";
            // 
            // c1PrintPreviewDialog1
            // 
            this.c1PrintPreviewDialog1.ClientSize = new System.Drawing.Size(716, 543);
            this.c1PrintPreviewDialog1.Icon = ((System.Drawing.Icon)(resources.GetObject("c1PrintPreviewDialog1.Icon")));
            this.c1PrintPreviewDialog1.Name = "C1PrintPreviewDialog";
            // 
            // c1PrintPreviewDialog1.PrintPreviewControl
            // 
            // 
            // c1PrintPreviewDialog1.PrintPreviewControl.OutlineView
            // 
            this.c1PrintPreviewDialog1.PrintPreviewControl.PreviewOutlineView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1PrintPreviewDialog1.PrintPreviewControl.PreviewOutlineView.LineColor = System.Drawing.Color.Empty;
            this.c1PrintPreviewDialog1.PrintPreviewControl.PreviewOutlineView.Location = new System.Drawing.Point(0, 0);
            this.c1PrintPreviewDialog1.PrintPreviewControl.PreviewOutlineView.Name = "OutlineView";
            this.c1PrintPreviewDialog1.PrintPreviewControl.PreviewOutlineView.Size = new System.Drawing.Size(162, 527);
            this.c1PrintPreviewDialog1.PrintPreviewControl.PreviewOutlineView.TabIndex = 0;
            // 
            // c1PrintPreviewDialog1.PrintPreviewControl.PreviewPane
            // 
            this.c1PrintPreviewDialog1.PrintPreviewControl.PreviewPane.IntegrateExternalTools = true;
            this.c1PrintPreviewDialog1.PrintPreviewControl.PreviewPane.TabIndex = 0;
            // 
            // c1PrintPreviewDialog1.PrintPreviewControl.PreviewTextSearchPanel
            // 
            this.c1PrintPreviewDialog1.PrintPreviewControl.PreviewTextSearchPanel.Dock = System.Windows.Forms.DockStyle.Right;
            this.c1PrintPreviewDialog1.PrintPreviewControl.PreviewTextSearchPanel.Location = new System.Drawing.Point(516, 0);
            this.c1PrintPreviewDialog1.PrintPreviewControl.PreviewTextSearchPanel.MinimumSize = new System.Drawing.Size(200, 240);
            this.c1PrintPreviewDialog1.PrintPreviewControl.PreviewTextSearchPanel.Name = "PreviewTextSearchPanel";
            this.c1PrintPreviewDialog1.PrintPreviewControl.PreviewTextSearchPanel.Size = new System.Drawing.Size(200, 496);
            this.c1PrintPreviewDialog1.PrintPreviewControl.PreviewTextSearchPanel.TabIndex = 0;
            this.c1PrintPreviewDialog1.PrintPreviewControl.PreviewTextSearchPanel.Visible = false;
            // 
            // c1PrintPreviewDialog1.PrintPreviewControl.ThumbnailView
            // 
            this.c1PrintPreviewDialog1.PrintPreviewControl.PreviewThumbnailView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1PrintPreviewDialog1.PrintPreviewControl.PreviewThumbnailView.Location = new System.Drawing.Point(0, 0);
            this.c1PrintPreviewDialog1.PrintPreviewControl.PreviewThumbnailView.Name = "ThumbnailView";
            this.c1PrintPreviewDialog1.PrintPreviewControl.PreviewThumbnailView.Size = new System.Drawing.Size(165, 464);
            this.c1PrintPreviewDialog1.PrintPreviewControl.PreviewThumbnailView.TabIndex = 0;
            this.c1PrintPreviewDialog1.PrintPreviewControl.PreviewThumbnailView.UseImageAsThumbnail = false;
            this.c1PrintPreviewDialog1.PrintPreviewControl.Text = "c1PrintPreviewControl1";
            // 
            // 
            // 
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.File.Open.Image = ((System.Drawing.Image)(resources.GetObject("c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.File.Open.Image")));
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.File.Open.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.File.Open.Name = "btnFileOpen";
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.File.Open.Size = new System.Drawing.Size(32, 22);
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.File.Open.Tag = "C1PreviewActionEnum.FileOpen";
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.File.Open.ToolTipText = "Open File";
            // 
            // 
            // 
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.File.PageSetup.Image = ((System.Drawing.Image)(resources.GetObject("c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.File.PageSetup.Image")));
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.File.PageSetup.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.File.PageSetup.Name = "btnPageSetup";
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.File.PageSetup.Size = new System.Drawing.Size(23, 22);
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.File.PageSetup.Tag = "C1PreviewActionEnum.PageSetup";
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.File.PageSetup.ToolTipText = "Page Setup";
            // 
            // 
            // 
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.File.Print.Image = ((System.Drawing.Image)(resources.GetObject("c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.File.Print.Image")));
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.File.Print.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.File.Print.Name = "btnPrint";
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.File.Print.Size = new System.Drawing.Size(23, 22);
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.File.Print.Tag = "C1PreviewActionEnum.Print";
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.File.Print.ToolTipText = "Print";
            // 
            // 
            // 
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.File.Reflow.Image = ((System.Drawing.Image)(resources.GetObject("c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.File.Reflow.Image")));
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.File.Reflow.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.File.Reflow.Name = "btnReflow";
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.File.Reflow.Size = new System.Drawing.Size(23, 22);
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.File.Reflow.Tag = "C1PreviewActionEnum.Reflow";
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.File.Reflow.ToolTipText = "Reflow";
            // 
            // 
            // 
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.File.Save.Image = ((System.Drawing.Image)(resources.GetObject("c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.File.Save.Image")));
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.File.Save.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.File.Save.Name = "btnFileSave";
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.File.Save.Size = new System.Drawing.Size(23, 22);
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.File.Save.Tag = "C1PreviewActionEnum.FileSave";
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.File.Save.ToolTipText = "Save File";
            // 
            // 
            // 
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Navigation.GoFirst.Image = ((System.Drawing.Image)(resources.GetObject("c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Navigation.GoFirst.Image")));
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Navigation.GoFirst.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Navigation.GoFirst.Name = "btnGoFirst";
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Navigation.GoFirst.Size = new System.Drawing.Size(23, 22);
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Navigation.GoFirst.Tag = "C1PreviewActionEnum.GoFirst";
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Navigation.GoFirst.ToolTipText = "Go To First Page";
            // 
            // 
            // 
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Navigation.GoLast.Image = ((System.Drawing.Image)(resources.GetObject("c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Navigation.GoLast.Image")));
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Navigation.GoLast.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Navigation.GoLast.Name = "btnGoLast";
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Navigation.GoLast.Size = new System.Drawing.Size(23, 22);
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Navigation.GoLast.Tag = "C1PreviewActionEnum.GoLast";
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Navigation.GoLast.ToolTipText = "Go To Last Page";
            // 
            // 
            // 
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Navigation.GoNext.Image = ((System.Drawing.Image)(resources.GetObject("c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Navigation.GoNext.Image")));
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Navigation.GoNext.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Navigation.GoNext.Name = "btnGoNext";
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Navigation.GoNext.Size = new System.Drawing.Size(23, 22);
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Navigation.GoNext.Tag = "C1PreviewActionEnum.GoNext";
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Navigation.GoNext.ToolTipText = "Go To Next Page";
            // 
            // 
            // 
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Navigation.GoPrev.Image = ((System.Drawing.Image)(resources.GetObject("c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Navigation.GoPrev.Image")));
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Navigation.GoPrev.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Navigation.GoPrev.Name = "btnGoPrev";
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Navigation.GoPrev.Size = new System.Drawing.Size(23, 22);
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Navigation.GoPrev.Tag = "C1PreviewActionEnum.GoPrev";
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Navigation.GoPrev.ToolTipText = "Go To Previous Page";
            // 
            // 
            // 
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Navigation.HistoryNext.Image = ((System.Drawing.Image)(resources.GetObject("c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Navigation.HistoryNext.Image")));
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Navigation.HistoryNext.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Navigation.HistoryNext.Name = "btnHistoryNext";
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Navigation.HistoryNext.Size = new System.Drawing.Size(32, 22);
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Navigation.HistoryNext.Tag = "C1PreviewActionEnum.HistoryNext";
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Navigation.HistoryNext.ToolTipText = "Next View";
            // 
            // 
            // 
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Navigation.HistoryPrev.Image = ((System.Drawing.Image)(resources.GetObject("c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Navigation.HistoryPrev.Image")));
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Navigation.HistoryPrev.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Navigation.HistoryPrev.Name = "btnHistoryPrev";
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Navigation.HistoryPrev.Size = new System.Drawing.Size(32, 22);
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Navigation.HistoryPrev.Tag = "C1PreviewActionEnum.HistoryPrev";
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Navigation.HistoryPrev.ToolTipText = "Previous View";
            // 
            // 
            // 
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Navigation.LblOfPages.Name = "lblOfPages";
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Navigation.LblOfPages.Size = new System.Drawing.Size(27, 22);
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Navigation.LblOfPages.Tag = "C1PreviewActionEnum.GoPageCount";
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Navigation.LblOfPages.Text = "of 0";
            // 
            // 
            // 
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Navigation.LblPage.Name = "lblPage";
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Navigation.LblPage.Size = new System.Drawing.Size(33, 22);
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Navigation.LblPage.Tag = "C1PreviewActionEnum.GoPageLabel";
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Navigation.LblPage.Text = "Page";
            // 
            // 
            // 
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Navigation.PageNo.Name = "txtPageNo";
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Navigation.PageNo.Size = new System.Drawing.Size(34, 25);
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Navigation.PageNo.Tag = "C1PreviewActionEnum.GoPageNumber";
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Navigation.PageNo.Text = "1";
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Navigation.ToolTipPageNo = null;
            // 
            // 
            // 
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Page.Continuous.Checked = true;
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Page.Continuous.CheckState = System.Windows.Forms.CheckState.Checked;
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Page.Continuous.Image = ((System.Drawing.Image)(resources.GetObject("c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Page.Continuous.Image")));
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Page.Continuous.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Page.Continuous.Name = "btnPageContinuous";
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Page.Continuous.Size = new System.Drawing.Size(23, 22);
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Page.Continuous.Tag = "C1PreviewActionEnum.PageContinuous";
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Page.Continuous.ToolTipText = "Continuous View";
            // 
            // 
            // 
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Page.Facing.Image = ((System.Drawing.Image)(resources.GetObject("c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Page.Facing.Image")));
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Page.Facing.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Page.Facing.Name = "btnPageFacing";
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Page.Facing.Size = new System.Drawing.Size(23, 22);
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Page.Facing.Tag = "C1PreviewActionEnum.PageFacing";
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Page.Facing.ToolTipText = "Pages Facing View";
            // 
            // 
            // 
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Page.FacingContinuous.Image = ((System.Drawing.Image)(resources.GetObject("c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Page.FacingContinuous.Image")));
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Page.FacingContinuous.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Page.FacingContinuous.Name = "btnPageFacingContinuous";
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Page.FacingContinuous.Size = new System.Drawing.Size(23, 22);
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Page.FacingContinuous.Tag = "C1PreviewActionEnum.PageFacingContinuous";
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Page.FacingContinuous.ToolTipText = "Pages Facing Continuous View";
            // 
            // 
            // 
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Page.Single.Image = ((System.Drawing.Image)(resources.GetObject("c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Page.Single.Image")));
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Page.Single.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Page.Single.Name = "btnPageSingle";
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Page.Single.Size = new System.Drawing.Size(23, 22);
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Page.Single.Tag = "C1PreviewActionEnum.PageSingle";
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Page.Single.ToolTipText = "Single Page View";
            // 
            // 
            // 
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Text.Hand.Checked = true;
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Text.Hand.CheckState = System.Windows.Forms.CheckState.Checked;
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Text.Hand.Image = ((System.Drawing.Image)(resources.GetObject("c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Text.Hand.Image")));
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Text.Hand.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Text.Hand.Name = "btnHandTool";
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Text.Hand.Size = new System.Drawing.Size(23, 22);
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Text.Hand.Tag = "C1PreviewActionEnum.HandTool";
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Text.Hand.ToolTipText = "Hand Tool";
            // 
            // 
            // 
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Zoom.DropZoomFactor.Name = "dropZoomFactor";
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Zoom.DropZoomFactor.Size = new System.Drawing.Size(13, 22);
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Zoom.DropZoomFactor.Tag = "C1PreviewActionEnum.ZoomFactor";
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Zoom.ToolTipZoomFactor = null;
            // 
            // 
            // 
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Zoom.ZoomFactor.Name = "txtZoomFactor";
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Zoom.ZoomFactor.Size = new System.Drawing.Size(48, 25);
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Zoom.ZoomFactor.Tag = "C1PreviewActionEnum.ZoomFactor";
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Zoom.ZoomFactor.Text = "100%";
            // 
            // 
            // 
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Zoom.ZoomIn.Image = ((System.Drawing.Image)(resources.GetObject("c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Zoom.ZoomIn.Image")));
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Zoom.ZoomIn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Zoom.ZoomIn.Name = "btnZoomIn";
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Zoom.ZoomIn.Size = new System.Drawing.Size(23, 22);
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Zoom.ZoomIn.Tag = "C1PreviewActionEnum.ZoomIn";
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Zoom.ZoomIn.ToolTipText = "Zoom In";
            // 
            // 
            // 
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Zoom.ZoomOut.Image = ((System.Drawing.Image)(resources.GetObject("c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Zoom.ZoomOut.Image")));
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Zoom.ZoomOut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Zoom.ZoomOut.Name = "btnZoomOut";
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Zoom.ZoomOut.Size = new System.Drawing.Size(23, 22);
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Zoom.ZoomOut.Tag = "C1PreviewActionEnum.ZoomOut";
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Zoom.ZoomOut.ToolTipText = "Zoom Out";
            // 
            // 
            // 
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Zoom.ZoomTool.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Zoom.ZoomInTool,
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Zoom.ZoomOutTool});
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Zoom.ZoomTool.Image = ((System.Drawing.Image)(resources.GetObject("c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Zoom.ZoomTool.Image")));
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Zoom.ZoomTool.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Zoom.ZoomTool.Name = "btnZoomTool";
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Zoom.ZoomTool.Size = new System.Drawing.Size(32, 22);
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Zoom.ZoomTool.Tag = "C1PreviewActionEnum.ZoomInTool";
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Zoom.ZoomTool.ToolTipText = "Zoom In Tool";
            this.c1PrintPreviewDialog1.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Auto;
            this.c1PrintPreviewDialog1.Text = "c1PrintPreviewDialog1";
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(690, 555);
            this.Controls.Add(this.c1PrintPreviewControl1);
            this.Controls.Add(this.c1TrueDBGrid2);
            this.Controls.Add(this.c1Combo2);
            this.Name = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.c1Combo2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1TrueDBGrid2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1Report1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1PrintDocument1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1PrintPreviewControl1.PreviewPane)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1PrintPreviewControl1)).EndInit();
            this.c1PrintPreviewControl1.ResumeLayout(false);
            this.c1PrintPreviewControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1PrintPreviewDialog1.PrintPreviewControl.PreviewPane)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1PrintPreviewDialog1.PrintPreviewControl)).EndInit();
            this.c1PrintPreviewDialog1.PrintPreviewControl.ResumeLayout(false);
            this.c1PrintPreviewDialog1.PrintPreviewControl.PerformLayout();
            this.c1PrintPreviewDialog1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private C1.Win.C1TrueDBGrid.C1TrueDBGrid c1TrueDBGrid1;
        private C1.Win.C1Preview.C1PreviewPane c1PreviewPane1;
        private C1.Win.C1List.C1Combo c1Combo1;
        private C1.Win.C1List.C1Combo c1Combo2;
        private C1.Win.C1TrueDBGrid.C1TrueDBGrid c1TrueDBGrid2;
        private C1.C1Report.C1Report c1Report1;
        private C1.C1Preview.C1PrintDocument c1PrintDocument1;
        private C1.Win.C1Preview.C1PrintPreviewControl c1PrintPreviewControl1;
        private C1.Win.C1Preview.C1PrintPreviewDialog c1PrintPreviewDialog1;
    }
}