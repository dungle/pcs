namespace PCSProcurement.Purchase
{
    partial class UpdatePurchaseOrder
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
            this.dtmToDateTime = new C1.Win.C1Input.C1DateEdit();
            this.dtmFromDateTime = new C1.Win.C1Input.C1DateEdit();
            this.lblToDateTime = new System.Windows.Forms.Label();
            this.lblFromDateTime = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnHelp = new System.Windows.Forms.Button();
            this.UpdateButton = new System.Windows.Forms.Button();
            this.VendorCodeButton = new System.Windows.Forms.Button();
            this.VendorCodeText = new System.Windows.Forms.TextBox();
            this.VendorCodeLabel = new System.Windows.Forms.Label();
            this.VendorNameLabel = new System.Windows.Forms.Label();
            this.VendorNameText = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dtmToDateTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtmFromDateTime)).BeginInit();
            this.SuspendLayout();
            // 
            // dtmToDateTime
            // 
            // 
            // 
            // 
            this.dtmToDateTime.Calendar.DayNameLength = 1;
            this.dtmToDateTime.Calendar.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.dtmToDateTime.Location = new System.Drawing.Point(112, 31);
            this.dtmToDateTime.Name = "dtmToDateTime";
            this.dtmToDateTime.Size = new System.Drawing.Size(165, 20);
            this.dtmToDateTime.TabIndex = 18;
            this.dtmToDateTime.Tag = null;
            this.dtmToDateTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.dtmToDateTime.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.DropDown;
            // 
            // dtmFromDateTime
            // 
            // 
            // 
            // 
            this.dtmFromDateTime.Calendar.DayNameLength = 1;
            this.dtmFromDateTime.Calendar.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.dtmFromDateTime.Location = new System.Drawing.Point(112, 9);
            this.dtmFromDateTime.Name = "dtmFromDateTime";
            this.dtmFromDateTime.Size = new System.Drawing.Size(165, 20);
            this.dtmFromDateTime.TabIndex = 16;
            this.dtmFromDateTime.Tag = null;
            this.dtmFromDateTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.dtmFromDateTime.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.DropDown;
            // 
            // lblToDateTime
            // 
            this.lblToDateTime.ForeColor = System.Drawing.Color.Maroon;
            this.lblToDateTime.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblToDateTime.Location = new System.Drawing.Point(4, 31);
            this.lblToDateTime.Name = "lblToDateTime";
            this.lblToDateTime.Size = new System.Drawing.Size(106, 20);
            this.lblToDateTime.TabIndex = 17;
            this.lblToDateTime.Text = "To Date";
            this.lblToDateTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblFromDateTime
            // 
            this.lblFromDateTime.ForeColor = System.Drawing.Color.Maroon;
            this.lblFromDateTime.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblFromDateTime.Location = new System.Drawing.Point(4, 9);
            this.lblFromDateTime.Name = "lblFromDateTime";
            this.lblFromDateTime.Size = new System.Drawing.Size(106, 20);
            this.lblFromDateTime.TabIndex = 15;
            this.lblFromDateTime.Text = "From Date";
            this.lblFromDateTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnClose.Location = new System.Drawing.Point(215, 105);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(62, 23);
            this.btnClose.TabIndex = 21;
            this.btnClose.Text = "&Close";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnHelp
            // 
            this.btnHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnHelp.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnHelp.Location = new System.Drawing.Point(150, 105);
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Size = new System.Drawing.Size(62, 23);
            this.btnHelp.TabIndex = 20;
            this.btnHelp.Text = "&Help";
            // 
            // UpdateButton
            // 
            this.UpdateButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.UpdateButton.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.UpdateButton.Location = new System.Drawing.Point(4, 105);
            this.UpdateButton.Name = "UpdateButton";
            this.UpdateButton.Size = new System.Drawing.Size(62, 23);
            this.UpdateButton.TabIndex = 19;
            this.UpdateButton.Text = "&Update";
            this.UpdateButton.Click += new System.EventHandler(this.UpdateButton_Click);
            // 
            // VendorCodeButton
            // 
            this.VendorCodeButton.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.VendorCodeButton.Location = new System.Drawing.Point(251, 53);
            this.VendorCodeButton.Name = "VendorCodeButton";
            this.VendorCodeButton.Size = new System.Drawing.Size(26, 20);
            this.VendorCodeButton.TabIndex = 24;
            this.VendorCodeButton.Text = "...";
            this.VendorCodeButton.UseVisualStyleBackColor = true;
            this.VendorCodeButton.Click += new System.EventHandler(this.VendorCodeButton_Click);
            // 
            // VendorCodeText
            // 
            this.VendorCodeText.Location = new System.Drawing.Point(112, 53);
            this.VendorCodeText.Name = "VendorCodeText";
            this.VendorCodeText.Size = new System.Drawing.Size(137, 20);
            this.VendorCodeText.TabIndex = 23;
            this.VendorCodeText.KeyDown += new System.Windows.Forms.KeyEventHandler(this.VendorCodeText_KeyDown);
            this.VendorCodeText.Validating += new System.ComponentModel.CancelEventHandler(this.VendorCodeText_Validating);
            // 
            // VendorCodeLabel
            // 
            this.VendorCodeLabel.ForeColor = System.Drawing.Color.Black;
            this.VendorCodeLabel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.VendorCodeLabel.Location = new System.Drawing.Point(4, 53);
            this.VendorCodeLabel.Name = "VendorCodeLabel";
            this.VendorCodeLabel.Size = new System.Drawing.Size(106, 20);
            this.VendorCodeLabel.TabIndex = 22;
            this.VendorCodeLabel.Text = "Vendor Code";
            this.VendorCodeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // VendorNameLabel
            // 
            this.VendorNameLabel.ForeColor = System.Drawing.Color.Black;
            this.VendorNameLabel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.VendorNameLabel.Location = new System.Drawing.Point(4, 75);
            this.VendorNameLabel.Name = "VendorNameLabel";
            this.VendorNameLabel.Size = new System.Drawing.Size(106, 20);
            this.VendorNameLabel.TabIndex = 22;
            this.VendorNameLabel.Text = "Vendor Name";
            this.VendorNameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // VendorNameText
            // 
            this.VendorNameText.Location = new System.Drawing.Point(112, 75);
            this.VendorNameText.Name = "VendorNameText";
            this.VendorNameText.ReadOnly = true;
            this.VendorNameText.Size = new System.Drawing.Size(165, 20);
            this.VendorNameText.TabIndex = 23;
            // 
            // UpdatePurchaseOrder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(281, 136);
            this.Controls.Add(this.VendorCodeButton);
            this.Controls.Add(this.VendorNameText);
            this.Controls.Add(this.VendorCodeText);
            this.Controls.Add(this.VendorNameLabel);
            this.Controls.Add(this.VendorCodeLabel);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnHelp);
            this.Controls.Add(this.UpdateButton);
            this.Controls.Add(this.dtmToDateTime);
            this.Controls.Add(this.dtmFromDateTime);
            this.Controls.Add(this.lblToDateTime);
            this.Controls.Add(this.lblFromDateTime);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "UpdatePurchaseOrder";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Update Purchase Orders";
            this.Load += new System.EventHandler(this.UpdatePurchaseOrder_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtmToDateTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtmFromDateTime)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private C1.Win.C1Input.C1DateEdit dtmToDateTime;
        private C1.Win.C1Input.C1DateEdit dtmFromDateTime;
        private System.Windows.Forms.Label lblToDateTime;
        private System.Windows.Forms.Label lblFromDateTime;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnHelp;
        private System.Windows.Forms.Button UpdateButton;
        private System.Windows.Forms.Button VendorCodeButton;
        private System.Windows.Forms.TextBox VendorCodeText;
        private System.Windows.Forms.Label VendorCodeLabel;
        private System.Windows.Forms.Label VendorNameLabel;
        private System.Windows.Forms.TextBox VendorNameText;
    }
}