namespace PCSMain
{
    partial class MainScreen
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
            this.menuGroupBar = new Syncfusion.Windows.Forms.Tools.GroupBar();
            ((System.ComponentModel.ISupportInitialize)(this.menuGroupBar)).BeginInit();
            this.SuspendLayout();
            // 
            // menuGroupBar
            // 
            this.menuGroupBar.AllowDrop = true;
            this.menuGroupBar.AnimatedSelection = false;
            this.menuGroupBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(235)))), ((int)(((byte)(235)))));
            this.menuGroupBar.BorderColor = System.Drawing.Color.White;
            this.menuGroupBar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.menuGroupBar.Dock = System.Windows.Forms.DockStyle.Left;
            this.menuGroupBar.ExpandButtonToolTip = null;
            this.menuGroupBar.ExpandedWidth = 220;
            this.menuGroupBar.FlatLook = true;
            this.menuGroupBar.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuGroupBar.ForeColor = System.Drawing.Color.White;
            this.menuGroupBar.GroupBarDropDownToolTip = null;
            this.menuGroupBar.HeaderBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(158)))), ((int)(((byte)(218)))));
            this.menuGroupBar.IndexOnVisibleItems = true;
            this.menuGroupBar.IntegratedScrolling = true;
            this.menuGroupBar.Location = new System.Drawing.Point(0, 0);
            this.menuGroupBar.MinimizeButtonToolTip = null;
            this.menuGroupBar.Name = "menuGroupBar";
            this.menuGroupBar.NavigationPaneTooltip = null;
            this.menuGroupBar.PopupClientSize = new System.Drawing.Size(0, 0);
            this.menuGroupBar.Size = new System.Drawing.Size(293, 633);
            this.menuGroupBar.TabIndex = 0;
            this.menuGroupBar.Text = "groupBar1";
            this.menuGroupBar.TextAlign = Syncfusion.Windows.Forms.Tools.TextAlignment.Left;
            this.menuGroupBar.VisualStyle = Syncfusion.Windows.Forms.VisualStyle.Metro;
            // 
            // MainScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(895, 633);
            this.Controls.Add(this.menuGroupBar);
            this.Name = "MainScreen";
            this.Load += new System.EventHandler(this.MainScreen_Load);
            ((System.ComponentModel.ISupportInitialize)(this.menuGroupBar)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Syncfusion.Windows.Forms.Tools.GroupBar menuGroupBar;
    }
}
