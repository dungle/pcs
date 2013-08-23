namespace PCSProduction.WorkOrder
{
    partial class MultiCompletion
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MultiCompletion));
            this.PostDateLabel = new System.Windows.Forms.Label();
            this.MasterLocationLabel = new System.Windows.Forms.Label();
            this.FromDateLabel = new System.Windows.Forms.Label();
            this.ToDateLabel = new System.Windows.Forms.Label();
            this.WorkOrderNoLabel = new System.Windows.Forms.Label();
            this.ShiftLabel = new System.Windows.Forms.Label();
            this.PostDatePicker = new C1.Win.C1Input.C1DateEdit();
            this.FromDatePicker = new C1.Win.C1Input.C1DateEdit();
            this.ToDatePicker = new C1.Win.C1Input.C1DateEdit();
            this.MultiNoLabel = new System.Windows.Forms.Label();
            this.DetailGrid = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
            this.PurposeLabel = new System.Windows.Forms.Label();
            this.ButtonSearch = new C1.Win.C1Input.C1Button();
            this.MasterLocationText = new C1.Win.C1Input.C1TextBox();
            this.AddButton = new C1.Win.C1Input.C1Button();
            this.SaveButton = new C1.Win.C1Input.C1Button();
            this.BomShortageButton = new C1.Win.C1Input.C1Button();
            this.CloseButton = new C1.Win.C1Input.C1Button();
            this.btnHelp = new C1.Win.C1Input.C1Button();
            this.MasterLocationButton = new C1.Win.C1Input.C1Button();
            this.MultiCompletionNoText = new C1.Win.C1Input.C1TextBox();
            this.WorkOrderNoText = new C1.Win.C1Input.C1TextBox();
            this.ShiftText = new C1.Win.C1Input.C1TextBox();
            this.PurposeText = new C1.Win.C1Input.C1TextBox();
            this.MultiNoButton = new C1.Win.C1Input.C1Button();
            this.WorkOrderNoButton = new C1.Win.C1Input.C1Button();
            this.ShiftButton = new C1.Win.C1Input.C1Button();
            ((System.ComponentModel.ISupportInitialize)(this.PostDatePicker)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FromDatePicker)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ToDatePicker)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DetailGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MasterLocationText)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MultiCompletionNoText)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.WorkOrderNoText)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ShiftText)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PurposeText)).BeginInit();
            this.SuspendLayout();
            // 
            // PostDateLabel
            // 
            this.PostDateLabel.ForeColor = System.Drawing.Color.Maroon;
            resources.ApplyResources(this.PostDateLabel, "PostDateLabel");
            this.PostDateLabel.Name = "PostDateLabel";
            // 
            // MasterLocationLabel
            // 
            this.MasterLocationLabel.ForeColor = System.Drawing.Color.Maroon;
            resources.ApplyResources(this.MasterLocationLabel, "MasterLocationLabel");
            this.MasterLocationLabel.Name = "MasterLocationLabel";
            // 
            // FromDateLabel
            // 
            this.FromDateLabel.ForeColor = System.Drawing.Color.Maroon;
            resources.ApplyResources(this.FromDateLabel, "FromDateLabel");
            this.FromDateLabel.Name = "FromDateLabel";
            // 
            // ToDateLabel
            // 
            this.ToDateLabel.ForeColor = System.Drawing.Color.Maroon;
            resources.ApplyResources(this.ToDateLabel, "ToDateLabel");
            this.ToDateLabel.Name = "ToDateLabel";
            // 
            // WorkOrderNoLabel
            // 
            this.WorkOrderNoLabel.ForeColor = System.Drawing.Color.Maroon;
            resources.ApplyResources(this.WorkOrderNoLabel, "WorkOrderNoLabel");
            this.WorkOrderNoLabel.Name = "WorkOrderNoLabel";
            // 
            // ShiftLabel
            // 
            this.ShiftLabel.ForeColor = System.Drawing.Color.Maroon;
            resources.ApplyResources(this.ShiftLabel, "ShiftLabel");
            this.ShiftLabel.Name = "ShiftLabel";
            // 
            // PostDatePicker
            // 
            this.PostDatePicker.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(177)))), ((int)(((byte)(187)))), ((int)(((byte)(214)))));
            resources.ApplyResources(this.PostDatePicker, "PostDatePicker");
            // 
            // 
            // 
            this.PostDatePicker.Calendar.Font = ((System.Drawing.Font)(resources.GetObject("PostDatePicker.Calendar.Font")));
            this.PostDatePicker.Calendar.VisualStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.PostDatePicker.Calendar.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.PostDatePicker.Label = this.PostDateLabel;
            this.PostDatePicker.Name = "PostDatePicker";
            this.PostDatePicker.Tag = null;
            this.PostDatePicker.VisualStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.PostDatePicker.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.PostDatePicker.ValueChanged += new System.EventHandler(this.PostDatePicker_ValueChanged);
            // 
            // FromDatePicker
            // 
            this.FromDatePicker.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(177)))), ((int)(((byte)(187)))), ((int)(((byte)(214)))));
            resources.ApplyResources(this.FromDatePicker, "FromDatePicker");
            // 
            // 
            // 
            this.FromDatePicker.Calendar.Font = ((System.Drawing.Font)(resources.GetObject("FromDatePicker.Calendar.Font")));
            this.FromDatePicker.Calendar.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("FromDatePicker.Calendar.ImeMode")));
            this.FromDatePicker.Calendar.VisualStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.FromDatePicker.Calendar.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.FromDatePicker.Label = this.FromDateLabel;
            this.FromDatePicker.Name = "FromDatePicker";
            this.FromDatePicker.Tag = null;
            this.FromDatePicker.VisualStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.FromDatePicker.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            // 
            // ToDatePicker
            // 
            this.ToDatePicker.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(177)))), ((int)(((byte)(187)))), ((int)(((byte)(214)))));
            resources.ApplyResources(this.ToDatePicker, "ToDatePicker");
            // 
            // 
            // 
            this.ToDatePicker.Calendar.Font = ((System.Drawing.Font)(resources.GetObject("ToDatePicker.Calendar.Font")));
            this.ToDatePicker.Calendar.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("ToDatePicker.Calendar.ImeMode")));
            this.ToDatePicker.Calendar.VisualStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.ToDatePicker.Calendar.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.ToDatePicker.Label = this.ToDateLabel;
            this.ToDatePicker.Name = "ToDatePicker";
            this.ToDatePicker.Tag = null;
            this.ToDatePicker.VisualStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.ToDatePicker.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            // 
            // MultiNoLabel
            // 
            resources.ApplyResources(this.MultiNoLabel, "MultiNoLabel");
            this.MultiNoLabel.Name = "MultiNoLabel";
            // 
            // DetailGrid
            // 
            resources.ApplyResources(this.DetailGrid, "DetailGrid");
            this.DetailGrid.Images.Add(((System.Drawing.Image)(resources.GetObject("DetailGrid.Images"))));
            this.DetailGrid.Name = "DetailGrid";
            this.DetailGrid.PreviewInfo.Location = new System.Drawing.Point(0, 0);
            this.DetailGrid.PreviewInfo.Size = new System.Drawing.Size(0, 0);
            this.DetailGrid.PreviewInfo.ZoomFactor = 75;
            this.DetailGrid.PrintInfo.PageSettings = ((System.Drawing.Printing.PageSettings)(resources.GetObject("DetailGrid.PrintInfo.PageSettings")));
            this.DetailGrid.VisualStyle = C1.Win.C1TrueDBGrid.VisualStyle.Office2007Blue;
            this.DetailGrid.ButtonClick += new C1.Win.C1TrueDBGrid.ColEventHandler(this.DetailGrid_ButtonClick);
            this.DetailGrid.BeforeColUpdate += new C1.Win.C1TrueDBGrid.BeforeColUpdateEventHandler(this.DetailGrid_BeforeColUpdate);
            this.DetailGrid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DetailGrid_KeyDown);
            this.DetailGrid.AfterColUpdate += new C1.Win.C1TrueDBGrid.ColEventHandler(this.DetailGrid_AfterColUpdate);
            this.DetailGrid.PropBag = resources.GetString("DetailGrid.PropBag");
            // 
            // PurposeLabel
            // 
            this.PurposeLabel.ForeColor = System.Drawing.Color.Maroon;
            resources.ApplyResources(this.PurposeLabel, "PurposeLabel");
            this.PurposeLabel.Name = "PurposeLabel";
            // 
            // ButtonSearch
            // 
            resources.ApplyResources(this.ButtonSearch, "ButtonSearch");
            this.ButtonSearch.Name = "ButtonSearch";
            this.ButtonSearch.UseVisualStyleBackColor = true;
            this.ButtonSearch.VisualStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.ButtonSearch.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.ButtonSearch.Click += new System.EventHandler(this.SearchButton_Click);
            // 
            // MasterLocationText
            // 
            this.MasterLocationText.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(177)))), ((int)(((byte)(187)))), ((int)(((byte)(214)))));
            resources.ApplyResources(this.MasterLocationText, "MasterLocationText");
            this.MasterLocationText.Label = this.MasterLocationLabel;
            this.MasterLocationText.Name = "MasterLocationText";
            this.MasterLocationText.Tag = null;
            this.MasterLocationText.TextDetached = true;
            this.MasterLocationText.VisualStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.MasterLocationText.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.MasterLocationText.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MasterLocationText_KeyDown);
            this.MasterLocationText.Validating += new System.ComponentModel.CancelEventHandler(this.MasterLocationText_Validating);
            // 
            // AddButton
            // 
            resources.ApplyResources(this.AddButton, "AddButton");
            this.AddButton.Name = "AddButton";
            this.AddButton.UseVisualStyleBackColor = true;
            this.AddButton.VisualStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.AddButton.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.AddButton.Click += new System.EventHandler(this.AddButton_Click);
            // 
            // SaveButton
            // 
            resources.ApplyResources(this.SaveButton, "SaveButton");
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.VisualStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.SaveButton.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // BomShortageButton
            // 
            resources.ApplyResources(this.BomShortageButton, "BomShortageButton");
            this.BomShortageButton.Name = "BomShortageButton";
            this.BomShortageButton.UseVisualStyleBackColor = true;
            this.BomShortageButton.VisualStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.BomShortageButton.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.BomShortageButton.Click += new System.EventHandler(this.BomShortageButton_Click);
            // 
            // CloseButton
            // 
            resources.ApplyResources(this.CloseButton, "CloseButton");
            this.CloseButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.UseVisualStyleBackColor = true;
            this.CloseButton.VisualStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.CloseButton.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
            // 
            // btnHelp
            // 
            resources.ApplyResources(this.btnHelp, "btnHelp");
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.UseVisualStyleBackColor = true;
            this.btnHelp.VisualStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.btnHelp.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            // 
            // MasterLocationButton
            // 
            resources.ApplyResources(this.MasterLocationButton, "MasterLocationButton");
            this.MasterLocationButton.Name = "MasterLocationButton";
            this.MasterLocationButton.UseVisualStyleBackColor = true;
            this.MasterLocationButton.VisualStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.MasterLocationButton.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.MasterLocationButton.Click += new System.EventHandler(this.MasterLocationButton_Click);
            // 
            // MultiCompletionNoText
            // 
            this.MultiCompletionNoText.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(177)))), ((int)(((byte)(187)))), ((int)(((byte)(214)))));
            resources.ApplyResources(this.MultiCompletionNoText, "MultiCompletionNoText");
            this.MultiCompletionNoText.Label = this.MultiNoLabel;
            this.MultiCompletionNoText.Name = "MultiCompletionNoText";
            this.MultiCompletionNoText.Tag = null;
            this.MultiCompletionNoText.TextDetached = true;
            this.MultiCompletionNoText.VisualStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.MultiCompletionNoText.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.MultiCompletionNoText.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MultiCompletionNoText_KeyDown);
            this.MultiCompletionNoText.Validating += new System.ComponentModel.CancelEventHandler(this.MultiCompletionNoText_Validating);
            // 
            // WorkOrderNoText
            // 
            this.WorkOrderNoText.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(177)))), ((int)(((byte)(187)))), ((int)(((byte)(214)))));
            resources.ApplyResources(this.WorkOrderNoText, "WorkOrderNoText");
            this.WorkOrderNoText.Label = this.WorkOrderNoLabel;
            this.WorkOrderNoText.Name = "WorkOrderNoText";
            this.WorkOrderNoText.Tag = null;
            this.WorkOrderNoText.TextDetached = true;
            this.WorkOrderNoText.VisualStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.WorkOrderNoText.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.WorkOrderNoText.KeyDown += new System.Windows.Forms.KeyEventHandler(this.WorkOrderNoText_KeyDown);
            this.WorkOrderNoText.Validating += new System.ComponentModel.CancelEventHandler(this.WorkOrderNoText_Validating);
            // 
            // ShiftText
            // 
            this.ShiftText.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(177)))), ((int)(((byte)(187)))), ((int)(((byte)(214)))));
            resources.ApplyResources(this.ShiftText, "ShiftText");
            this.ShiftText.Label = this.ShiftLabel;
            this.ShiftText.Name = "ShiftText";
            this.ShiftText.Tag = null;
            this.ShiftText.TextDetached = true;
            this.ShiftText.VisualStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.ShiftText.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.ShiftText.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ShiftText_KeyDown);
            this.ShiftText.Validating += new System.ComponentModel.CancelEventHandler(this.ShiftText_Validating);
            // 
            // PurposeText
            // 
            this.PurposeText.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(177)))), ((int)(((byte)(187)))), ((int)(((byte)(214)))));
            resources.ApplyResources(this.PurposeText, "PurposeText");
            this.PurposeText.Label = this.PurposeLabel;
            this.PurposeText.Name = "PurposeText";
            this.PurposeText.ReadOnly = true;
            this.PurposeText.Tag = null;
            this.PurposeText.TextDetached = true;
            this.PurposeText.VisualStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.PurposeText.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            // 
            // MultiNoButton
            // 
            resources.ApplyResources(this.MultiNoButton, "MultiNoButton");
            this.MultiNoButton.Name = "MultiNoButton";
            this.MultiNoButton.UseVisualStyleBackColor = true;
            this.MultiNoButton.VisualStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.MultiNoButton.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.MultiNoButton.Click += new System.EventHandler(this.MultiNoButton_Click);
            // 
            // WorkOrderNoButton
            // 
            resources.ApplyResources(this.WorkOrderNoButton, "WorkOrderNoButton");
            this.WorkOrderNoButton.Name = "WorkOrderNoButton";
            this.WorkOrderNoButton.UseVisualStyleBackColor = true;
            this.WorkOrderNoButton.VisualStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.WorkOrderNoButton.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.WorkOrderNoButton.Click += new System.EventHandler(this.WorkOrderNoButton_Click);
            // 
            // ShiftButton
            // 
            resources.ApplyResources(this.ShiftButton, "ShiftButton");
            this.ShiftButton.Name = "ShiftButton";
            this.ShiftButton.UseVisualStyleBackColor = true;
            this.ShiftButton.VisualStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.ShiftButton.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.ShiftButton.Click += new System.EventHandler(this.ShiftButton_Click);
            // 
            // MultiCompletion
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.CloseButton;
            this.Controls.Add(this.ShiftButton);
            this.Controls.Add(this.WorkOrderNoButton);
            this.Controls.Add(this.MultiNoButton);
            this.Controls.Add(this.MasterLocationButton);
            this.Controls.Add(this.PurposeText);
            this.Controls.Add(this.ShiftText);
            this.Controls.Add(this.WorkOrderNoText);
            this.Controls.Add(this.MultiCompletionNoText);
            this.Controls.Add(this.MasterLocationText);
            this.Controls.Add(this.btnHelp);
            this.Controls.Add(this.CloseButton);
            this.Controls.Add(this.BomShortageButton);
            this.Controls.Add(this.SaveButton);
            this.Controls.Add(this.AddButton);
            this.Controls.Add(this.ButtonSearch);
            this.Controls.Add(this.PurposeLabel);
            this.Controls.Add(this.DetailGrid);
            this.Controls.Add(this.ToDatePicker);
            this.Controls.Add(this.FromDatePicker);
            this.Controls.Add(this.PostDatePicker);
            this.Controls.Add(this.MultiNoLabel);
            this.Controls.Add(this.WorkOrderNoLabel);
            this.Controls.Add(this.ShiftLabel);
            this.Controls.Add(this.ToDateLabel);
            this.Controls.Add(this.FromDateLabel);
            this.Controls.Add(this.MasterLocationLabel);
            this.Controls.Add(this.PostDateLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.Name = "MultiCompletion";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.MultiCompletion_Load);
            ((System.ComponentModel.ISupportInitialize)(this.PostDatePicker)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FromDatePicker)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ToDatePicker)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DetailGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MasterLocationText)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MultiCompletionNoText)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.WorkOrderNoText)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ShiftText)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PurposeText)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label PostDateLabel;
        private System.Windows.Forms.Label MasterLocationLabel;
        private System.Windows.Forms.Label FromDateLabel;
        private System.Windows.Forms.Label ToDateLabel;
        private System.Windows.Forms.Label WorkOrderNoLabel;
        private System.Windows.Forms.Label ShiftLabel;
        private C1.Win.C1Input.C1DateEdit PostDatePicker;
        private C1.Win.C1Input.C1DateEdit FromDatePicker;
        private C1.Win.C1Input.C1DateEdit ToDatePicker;
        private System.Windows.Forms.Label MultiNoLabel;
        private C1.Win.C1TrueDBGrid.C1TrueDBGrid DetailGrid;
        private System.Windows.Forms.Label PurposeLabel;
        private C1.Win.C1Input.C1Button ButtonSearch;
        private C1.Win.C1Input.C1TextBox MasterLocationText;
        private C1.Win.C1Input.C1Button AddButton;
        private C1.Win.C1Input.C1Button SaveButton;
        private C1.Win.C1Input.C1Button BomShortageButton;
        private C1.Win.C1Input.C1Button CloseButton;
        private C1.Win.C1Input.C1Button btnHelp;
        private C1.Win.C1Input.C1Button MasterLocationButton;
        private C1.Win.C1Input.C1TextBox MultiCompletionNoText;
        private C1.Win.C1Input.C1TextBox WorkOrderNoText;
        private C1.Win.C1Input.C1TextBox ShiftText;
        private C1.Win.C1Input.C1TextBox PurposeText;
        private C1.Win.C1Input.C1Button MultiNoButton;
        private C1.Win.C1Input.C1Button WorkOrderNoButton;
        private C1.Win.C1Input.C1Button ShiftButton;
    }
}