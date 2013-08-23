using System.Windows.Forms;
namespace PCSUtils.MasterSetup.SystemTable
{
    partial class ViewData
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ViewData));
            C1.Win.C1TrueDBGrid.Style style1 = new C1.Win.C1TrueDBGrid.Style();
            C1.Win.C1TrueDBGrid.Style style2 = new C1.Win.C1TrueDBGrid.Style();
            C1.Win.C1TrueDBGrid.Style style3 = new C1.Win.C1TrueDBGrid.Style();
            C1.Win.C1TrueDBGrid.Style style4 = new C1.Win.C1TrueDBGrid.Style();
            C1.Win.C1TrueDBGrid.Style style5 = new C1.Win.C1TrueDBGrid.Style();
            C1.Win.C1TrueDBGrid.Style style6 = new C1.Win.C1TrueDBGrid.Style();
            C1.Win.C1TrueDBGrid.Style style7 = new C1.Win.C1TrueDBGrid.Style();
            C1.Win.C1TrueDBGrid.Style style8 = new C1.Win.C1TrueDBGrid.Style();
            C1.Win.C1TrueDBGrid.Style style9 = new C1.Win.C1TrueDBGrid.Style();
            C1.Win.C1TrueDBGrid.Style style10 = new C1.Win.C1TrueDBGrid.Style();
            C1.Win.C1TrueDBGrid.Style style11 = new C1.Win.C1TrueDBGrid.Style();
            C1.Win.C1TrueDBGrid.Style style12 = new C1.Win.C1TrueDBGrid.Style();
            C1.Win.C1TrueDBGrid.Style style13 = new C1.Win.C1TrueDBGrid.Style();
            this.tgridViewTable = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
            this.cmbPage = new System.Windows.Forms.ComboBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.mnuGrid = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mniStartWith = new System.Windows.Forms.ToolStripMenuItem();
            this.mniContain = new System.Windows.Forms.ToolStripMenuItem();
            this.mniEndWith = new System.Windows.Forms.ToolStripMenuItem();
            this.lblIntroduction = new System.Windows.Forms.Label();
            this.btnMove = new System.Windows.Forms.Button();
            this.lblPage = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.tgridViewTable)).BeginInit();
            this.mnuGrid.SuspendLayout();
            this.SuspendLayout();
            // 
            // tgridViewTable
            // 
            resources.ApplyResources(this.tgridViewTable, "tgridViewTable");
            this.tgridViewTable.CaptionStyle = style1;
            this.tgridViewTable.CausesValidation = false;
            this.tgridViewTable.EditorStyle = style2;
            this.tgridViewTable.EvenRowStyle = style3;
            this.tgridViewTable.FilterBarStyle = style4;
            this.tgridViewTable.FooterStyle = style5;
            this.tgridViewTable.ForeColor = System.Drawing.Color.LemonChiffon;
            this.tgridViewTable.GroupStyle = style6;
            this.tgridViewTable.HeadingStyle = style7;
            this.tgridViewTable.HighLightRowStyle = style8;
            this.tgridViewTable.Images.Add(((System.Drawing.Image)(resources.GetObject("tgridViewTable.Images"))));
            this.tgridViewTable.InactiveStyle = style9;
            this.tgridViewTable.MarqueeStyle = C1.Win.C1TrueDBGrid.MarqueeEnum.DottedCellBorder;
            this.tgridViewTable.Name = "tgridViewTable";
            this.tgridViewTable.OddRowStyle = style10;
            this.tgridViewTable.PreviewInfo.Location = ((System.Drawing.Point)(resources.GetObject("tgridViewTable.PreviewInfo.Location")));
            this.tgridViewTable.PreviewInfo.Size = ((System.Drawing.Size)(resources.GetObject("tgridViewTable.PreviewInfo.Size")));
            this.tgridViewTable.PreviewInfo.ZoomFactor = ((double)(resources.GetObject("tgridViewTable.PreviewInfo.ZoomFactor")));
            this.tgridViewTable.PrintInfo.PageSettings = ((System.Drawing.Printing.PageSettings)(resources.GetObject("tgridViewTable.PrintInfo.PageSettings")));
            this.tgridViewTable.PrintInfo.ShowOptionsDialog = ((bool)(resources.GetObject("tgridViewTable.PrintInfo.ShowOptionsDialog")));
            this.tgridViewTable.RecordSelectorStyle = style11;
            this.tgridViewTable.RowDivider.Color = ((System.Drawing.Color)(resources.GetObject("resource.Color")));
            this.tgridViewTable.RowDivider.Style = ((C1.Win.C1TrueDBGrid.LineStyleEnum)(resources.GetObject("resource.Style")));
            this.tgridViewTable.SelectedStyle = style12;
            this.tgridViewTable.Style = style13;
            this.tgridViewTable.AfterFilter += new C1.Win.C1TrueDBGrid.FilterEventHandler(this.tgridViewTable_AfterFilter);
            this.tgridViewTable.DoubleClick += new System.EventHandler(this.tgridViewTable_DoubleClick);
            this.tgridViewTable.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tgridViewTable_KeyDown);
            // 
            // cmbPage
            // 
            resources.ApplyResources(this.cmbPage, "cmbPage");
            this.cmbPage.FormattingEnabled = true;
            this.cmbPage.Name = "cmbPage";
            // 
            // btnClose
            // 
            resources.ApplyResources(this.btnClose, "btnClose");
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Name = "btnClose";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnSave
            // 
            resources.ApplyResources(this.btnSave, "btnSave");
            this.btnSave.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnSave.Name = "btnSave";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // mnuGrid
            // 
            this.mnuGrid.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mniStartWith,
            this.mniContain,
            this.mniEndWith});
            this.mnuGrid.Name = "mnuGrid";
            resources.ApplyResources(this.mnuGrid, "mnuGrid");
            // 
            // mniStartWith
            // 
            this.mniStartWith.Name = "mniStartWith";
            resources.ApplyResources(this.mniStartWith, "mniStartWith");
            // 
            // mniContain
            // 
            this.mniContain.Name = "mniContain";
            resources.ApplyResources(this.mniContain, "mniContain");
            // 
            // mniEndWith
            // 
            this.mniEndWith.Name = "mniEndWith";
            resources.ApplyResources(this.mniEndWith, "mniEndWith");
            // 
            // lblIntroduction
            // 
            resources.ApplyResources(this.lblIntroduction, "lblIntroduction");
            this.lblIntroduction.ForeColor = System.Drawing.Color.Maroon;
            this.lblIntroduction.Name = "lblIntroduction";
            // 
            // btnMove
            // 
            resources.ApplyResources(this.btnMove, "btnMove");
            this.btnMove.Name = "btnMove";
            this.btnMove.UseVisualStyleBackColor = true;
            this.btnMove.Click += new System.EventHandler(this.btnMove_Click);
            // 
            // lblPage
            // 
            resources.ApplyResources(this.lblPage, "lblPage");
            this.lblPage.ForeColor = System.Drawing.Color.Black;
            this.lblPage.Name = "lblPage";
            // 
            // ViewData
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.Controls.Add(this.btnMove);
            this.Controls.Add(this.lblPage);
            this.Controls.Add(this.lblIntroduction);
            this.Controls.Add(this.tgridViewTable);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.cmbPage);
            this.Name = "ViewData";
            this.Load += new System.EventHandler(this.SearchParty_Load);
            ((System.ComponentModel.ISupportInitialize)(this.tgridViewTable)).EndInit();
            this.mnuGrid.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private C1.Win.C1TrueDBGrid.C1TrueDBGrid tgridViewTable;
        private System.Windows.Forms.ComboBox cmbPage;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnSave;
        
        private ToolStripMenuItem mniContain;
        private ToolStripMenuItem mniEndWith;
        private ToolStripMenuItem mniStartWith;
        private ContextMenuStrip mnuGrid;
        private Label lblIntroduction;
        private Button btnMove;
        private Label lblPage;
    }
}