namespace PCSProcurement
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
            this.c1FlexGrid2 = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.c1TrueDBGrid2 = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
            this.c1Report1 = new C1.C1Report.C1Report();
            this.c1PrintDocument1 = new C1.C1Preview.C1PrintDocument();
            this.c1PreviewPane2 = new C1.Win.C1Preview.C1PreviewPane();
            this.c1PrintPreviewControl1 = new C1.Win.C1Preview.C1PrintPreviewControl();
            this.c1DateEdit1 = new C1.Win.C1Input.C1DateEdit();
            this.c1DropDownControl1 = new C1.Win.C1Input.C1DropDownControl();
            this.c1Combo2 = new C1.Win.C1List.C1Combo();
            ((System.ComponentModel.ISupportInitialize)(this.c1FlexGrid2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1TrueDBGrid2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1Report1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1PrintDocument1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1PreviewPane2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1PrintPreviewControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1PrintPreviewControl1.PreviewPane)).BeginInit();
            this.c1PrintPreviewControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1DateEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1DropDownControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1Combo2)).BeginInit();
            this.SuspendLayout();
            // 
            // c1FlexGrid2
            // 
            this.c1FlexGrid2.ColumnInfo = "10,1,0,0,0,95,Columns:";
            this.c1FlexGrid2.Location = new System.Drawing.Point(42, 31);
            this.c1FlexGrid2.Name = "c1FlexGrid2";
            this.c1FlexGrid2.Rows.DefaultSize = 19;
            this.c1FlexGrid2.Size = new System.Drawing.Size(240, 150);
            this.c1FlexGrid2.TabIndex = 0;
            // 
            // c1TrueDBGrid2
            // 
            this.c1TrueDBGrid2.Images.Add(((System.Drawing.Image)(resources.GetObject("c1TrueDBGrid2.Images"))));
            this.c1TrueDBGrid2.Location = new System.Drawing.Point(-23, -46);
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
            // c1PreviewPane2
            // 
            this.c1PreviewPane2.Document = this.c1Report1;
            this.c1PreviewPane2.Location = new System.Drawing.Point(-23, -46);
            this.c1PreviewPane2.Name = "c1PreviewPane2";
            this.c1PreviewPane2.Size = new System.Drawing.Size(200, 260);
            this.c1PreviewPane2.TabIndex = 2;
            // 
            // c1PrintPreviewControl1
            // 
            this.c1PrintPreviewControl1.Location = new System.Drawing.Point(-23, -46);
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
            this.c1PrintPreviewControl1.TabIndex = 3;
            this.c1PrintPreviewControl1.Text = "c1PrintPreviewControl1";
            // 
            // 
            // 
            this.c1PrintPreviewControl1.ToolBars.File.Open.Name = "btnFileOpen";
            this.c1PrintPreviewControl1.ToolBars.File.Open.Size = new System.Drawing.Size(16, 22);
            // 
            // 
            // 
            this.c1PrintPreviewControl1.ToolBars.File.PageSetup.Name = "btnPageSetup";
            this.c1PrintPreviewControl1.ToolBars.File.PageSetup.Size = new System.Drawing.Size(23, 22);
            // 
            // 
            // 
            this.c1PrintPreviewControl1.ToolBars.File.Print.Name = "btnPrint";
            this.c1PrintPreviewControl1.ToolBars.File.Print.Size = new System.Drawing.Size(23, 22);
            // 
            // 
            // 
            this.c1PrintPreviewControl1.ToolBars.File.Reflow.Name = "btnReflow";
            this.c1PrintPreviewControl1.ToolBars.File.Reflow.Size = new System.Drawing.Size(23, 22);
            // 
            // 
            // 
            this.c1PrintPreviewControl1.ToolBars.File.Save.Name = "btnFileSave";
            this.c1PrintPreviewControl1.ToolBars.File.Save.Size = new System.Drawing.Size(23, 22);
            this.c1PrintPreviewControl1.ToolBars.File.ToolTipFileOpen = "";
            this.c1PrintPreviewControl1.ToolBars.File.ToolTipFileSave = "";
            this.c1PrintPreviewControl1.ToolBars.File.ToolTipPageSetup = "";
            this.c1PrintPreviewControl1.ToolBars.File.ToolTipPrint = "";
            this.c1PrintPreviewControl1.ToolBars.File.ToolTipReflow = "";
            // 
            // 
            // 
            this.c1PrintPreviewControl1.ToolBars.Navigation.GoFirst.Name = "btnGoFirst";
            this.c1PrintPreviewControl1.ToolBars.Navigation.GoFirst.Size = new System.Drawing.Size(23, 22);
            // 
            // 
            // 
            this.c1PrintPreviewControl1.ToolBars.Navigation.GoLast.Name = "btnGoLast";
            this.c1PrintPreviewControl1.ToolBars.Navigation.GoLast.Size = new System.Drawing.Size(23, 22);
            // 
            // 
            // 
            this.c1PrintPreviewControl1.ToolBars.Navigation.GoNext.Name = "btnGoNext";
            this.c1PrintPreviewControl1.ToolBars.Navigation.GoNext.Size = new System.Drawing.Size(23, 22);
            // 
            // 
            // 
            this.c1PrintPreviewControl1.ToolBars.Navigation.GoPrev.Name = "btnGoPrev";
            this.c1PrintPreviewControl1.ToolBars.Navigation.GoPrev.Size = new System.Drawing.Size(23, 22);
            // 
            // 
            // 
            this.c1PrintPreviewControl1.ToolBars.Navigation.HistoryNext.Name = "btnHistoryNext";
            this.c1PrintPreviewControl1.ToolBars.Navigation.HistoryNext.Size = new System.Drawing.Size(16, 22);
            // 
            // 
            // 
            this.c1PrintPreviewControl1.ToolBars.Navigation.HistoryPrev.Name = "btnHistoryPrev";
            this.c1PrintPreviewControl1.ToolBars.Navigation.HistoryPrev.Size = new System.Drawing.Size(16, 22);
            // 
            // 
            // 
            this.c1PrintPreviewControl1.ToolBars.Navigation.LblOfPages.Name = "lblOfPages";
            this.c1PrintPreviewControl1.ToolBars.Navigation.LblOfPages.Size = new System.Drawing.Size(0, 22);
            // 
            // 
            // 
            this.c1PrintPreviewControl1.ToolBars.Navigation.LblPage.Name = "lblPage";
            this.c1PrintPreviewControl1.ToolBars.Navigation.LblPage.Size = new System.Drawing.Size(0, 22);
            this.c1PrintPreviewControl1.ToolBars.Navigation.ToolTipGoToFirstPage = "";
            this.c1PrintPreviewControl1.ToolBars.Navigation.ToolTipGoToLastPage = "";
            this.c1PrintPreviewControl1.ToolBars.Navigation.ToolTipGoToNextPage = "";
            this.c1PrintPreviewControl1.ToolBars.Navigation.ToolTipGoToPrevPage = "";
            this.c1PrintPreviewControl1.ToolBars.Navigation.ToolTipHistoryNext = "";
            this.c1PrintPreviewControl1.ToolBars.Navigation.ToolTipHistoryPrev = "";
            this.c1PrintPreviewControl1.ToolBars.Navigation.ToolTipPageNo = null;
            // 
            // 
            // 
            this.c1PrintPreviewControl1.ToolBars.Page.Continuous.Name = "btnPageContinuous";
            this.c1PrintPreviewControl1.ToolBars.Page.Continuous.Size = new System.Drawing.Size(23, 22);
            // 
            // 
            // 
            this.c1PrintPreviewControl1.ToolBars.Page.Facing.Name = "btnPageFacing";
            this.c1PrintPreviewControl1.ToolBars.Page.Facing.Size = new System.Drawing.Size(23, 22);
            // 
            // 
            // 
            this.c1PrintPreviewControl1.ToolBars.Page.FacingContinuous.Name = "btnPageFacingContinuous";
            this.c1PrintPreviewControl1.ToolBars.Page.FacingContinuous.Size = new System.Drawing.Size(23, 22);
            // 
            // 
            // 
            this.c1PrintPreviewControl1.ToolBars.Page.Single.Name = "btnPageSingle";
            this.c1PrintPreviewControl1.ToolBars.Page.Single.Size = new System.Drawing.Size(23, 22);
            this.c1PrintPreviewControl1.ToolBars.Page.ToolTipViewContinuous = "";
            this.c1PrintPreviewControl1.ToolBars.Page.ToolTipViewFacing = "";
            this.c1PrintPreviewControl1.ToolBars.Page.ToolTipViewFacingContinuous = "";
            this.c1PrintPreviewControl1.ToolBars.Page.ToolTipViewSinglePage = "";
            // 
            // 
            // 
            this.c1PrintPreviewControl1.ToolBars.Text.Find.Name = "btnFind";
            this.c1PrintPreviewControl1.ToolBars.Text.Find.Size = new System.Drawing.Size(23, 22);
            // 
            // 
            // 
            this.c1PrintPreviewControl1.ToolBars.Text.Hand.Name = "btnHandTool";
            this.c1PrintPreviewControl1.ToolBars.Text.Hand.Size = new System.Drawing.Size(23, 22);
            // 
            // 
            // 
            this.c1PrintPreviewControl1.ToolBars.Text.SelectText.Name = "btnSelectTextTool";
            this.c1PrintPreviewControl1.ToolBars.Text.SelectText.Size = new System.Drawing.Size(23, 22);
            this.c1PrintPreviewControl1.ToolBars.Text.ToolTipFind = "";
            this.c1PrintPreviewControl1.ToolBars.Text.ToolTipToolHand = "";
            this.c1PrintPreviewControl1.ToolBars.Text.ToolTipToolTextSelect = "";
            this.c1PrintPreviewControl1.ToolBars.Zoom.ToolTipToolZoomIn = null;
            this.c1PrintPreviewControl1.ToolBars.Zoom.ToolTipToolZoomOut = null;
            this.c1PrintPreviewControl1.ToolBars.Zoom.ToolTipZoomFactor = null;
            this.c1PrintPreviewControl1.ToolBars.Zoom.ToolTipZoomIn = "";
            this.c1PrintPreviewControl1.ToolBars.Zoom.ToolTipZoomOut = "";
            this.c1PrintPreviewControl1.ToolBars.Zoom.ToolTipZoomTool = "";
            // 
            // 
            // 
            this.c1PrintPreviewControl1.ToolBars.Zoom.ZoomIn.Name = "btnZoomIn";
            this.c1PrintPreviewControl1.ToolBars.Zoom.ZoomIn.Size = new System.Drawing.Size(23, 22);
            // 
            // 
            // 
            this.c1PrintPreviewControl1.ToolBars.Zoom.ZoomInTool.Name = "itemZoomInTool";
            this.c1PrintPreviewControl1.ToolBars.Zoom.ZoomInTool.Size = new System.Drawing.Size(67, 22);
            // 
            // 
            // 
            this.c1PrintPreviewControl1.ToolBars.Zoom.ZoomOut.Name = "btnZoomOut";
            this.c1PrintPreviewControl1.ToolBars.Zoom.ZoomOut.Size = new System.Drawing.Size(23, 22);
            // 
            // 
            // 
            this.c1PrintPreviewControl1.ToolBars.Zoom.ZoomOutTool.Name = "itemZoomOutTool";
            this.c1PrintPreviewControl1.ToolBars.Zoom.ZoomOutTool.Size = new System.Drawing.Size(67, 22);
            // 
            // 
            // 
            this.c1PrintPreviewControl1.ToolBars.Zoom.ZoomTool.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.c1PrintPreviewControl1.ToolBars.Zoom.ZoomInTool,
            this.c1PrintPreviewControl1.ToolBars.Zoom.ZoomOutTool});
            this.c1PrintPreviewControl1.ToolBars.Zoom.ZoomTool.Name = "btnZoomTool";
            this.c1PrintPreviewControl1.ToolBars.Zoom.ZoomTool.Size = new System.Drawing.Size(16, 22);
            // 
            // c1DateEdit1
            // 
            // 
            // 
            // 
            this.c1DateEdit1.Calendar.DayNameLength = 1;
            this.c1DateEdit1.Location = new System.Drawing.Point(166, 0);
            this.c1DateEdit1.Name = "c1DateEdit1";
            this.c1DateEdit1.Size = new System.Drawing.Size(200, 20);
            this.c1DateEdit1.TabIndex = 4;
            this.c1DateEdit1.Tag = null;
            // 
            // c1DropDownControl1
            // 
            this.c1DropDownControl1.Location = new System.Drawing.Point(143, 494);
            this.c1DropDownControl1.Name = "c1DropDownControl1";
            this.c1DropDownControl1.Size = new System.Drawing.Size(200, 20);
            this.c1DropDownControl1.TabIndex = 5;
            this.c1DropDownControl1.Tag = null;
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
            this.c1Combo2.Location = new System.Drawing.Point(448, 505);
            this.c1Combo2.MatchEntryTimeout = ((long)(2000));
            this.c1Combo2.MaxDropDownItems = ((short)(5));
            this.c1Combo2.MaxLength = 32767;
            this.c1Combo2.MouseCursor = System.Windows.Forms.Cursors.Default;
            this.c1Combo2.Name = "c1Combo2";
            this.c1Combo2.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None;
            this.c1Combo2.RowSubDividerColor = System.Drawing.Color.DarkGray;
            this.c1Combo2.Size = new System.Drawing.Size(121, 21);
            this.c1Combo2.TabIndex = 6;
            this.c1Combo2.Text = "c1Combo2";
            this.c1Combo2.PropBag = resources.GetString("c1Combo2.PropBag");
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(911, 657);
            this.Controls.Add(this.c1Combo2);
            this.Controls.Add(this.c1DropDownControl1);
            this.Controls.Add(this.c1DateEdit1);
            this.Controls.Add(this.c1PrintPreviewControl1);
            this.Controls.Add(this.c1PreviewPane2);
            this.Controls.Add(this.c1TrueDBGrid2);
            this.Controls.Add(this.c1FlexGrid2);
            this.Name = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.c1FlexGrid2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1TrueDBGrid2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1Report1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1PrintDocument1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1PreviewPane2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1PrintPreviewControl1.PreviewPane)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1PrintPreviewControl1)).EndInit();
            this.c1PrintPreviewControl1.ResumeLayout(false);
            this.c1PrintPreviewControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1DateEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1DropDownControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1Combo2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private C1.Win.C1TrueDBGrid.C1TrueDBGrid c1TrueDBGrid1;
        private C1.Win.C1FlexGrid.C1FlexGrid c1FlexGrid1;
        private C1.Win.C1Input.C1NumericEdit c1NumericEdit1;
        private C1.Win.C1List.C1Combo c1Combo1;
        private C1.Win.C1Preview.C1PreviewPane c1PreviewPane1;
        private C1.Win.C1FlexGrid.C1FlexGrid c1FlexGrid2;
        private C1.Win.C1TrueDBGrid.C1TrueDBGrid c1TrueDBGrid2;
        private C1.C1Report.C1Report c1Report1;
        private C1.C1Preview.C1PrintDocument c1PrintDocument1;
        private C1.Win.C1Preview.C1PreviewPane c1PreviewPane2;
        private C1.Win.C1Preview.C1PrintPreviewControl c1PrintPreviewControl1;
        private C1.Win.C1Input.C1DateEdit c1DateEdit1;
        private C1.Win.C1Input.C1DropDownControl c1DropDownControl1;
        private C1.Win.C1List.C1Combo c1Combo2;
    }
}