using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using C1.Win.C1Input;
using C1.Win.C1TrueDBGrid;

namespace PCSProduction.DCP
{
    partial class ImportPlanData
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private Container components = null;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImportPlanData));
            this.cboSheetnames = new System.Windows.Forms.ComboBox();
            this.txtRange = new System.Windows.Forms.TextBox();
            this.lblRange = new System.Windows.Forms.Label();
            this.btnOpenFileDlg = new System.Windows.Forms.Button();
            this.txtFileName = new System.Windows.Forms.TextBox();
            this.lblFileName = new System.Windows.Forms.Label();
            this.lblSheet = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnGetData = new System.Windows.Forms.Button();
            this.dgrdData = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
            this.txtCycle = new System.Windows.Forms.TextBox();
            this.btnSearchCycle = new System.Windows.Forms.Button();
            this.lblCycle = new System.Windows.Forms.Label();
            this.dtmMonth = new C1.Win.C1Input.C1DateEdit();
            this.lblMonth = new System.Windows.Forms.Label();
            this.txtProductionLine = new System.Windows.Forms.TextBox();
            this.lblProductionLine = new System.Windows.Forms.Label();
            this.btnProductionLine = new System.Windows.Forms.Button();
            this.txtShiftCode = new System.Windows.Forms.TextBox();
            this.btnShiftCode = new System.Windows.Forms.Button();
            this.lblShiftCode = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgrdData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtmMonth)).BeginInit();
            this.SuspendLayout();
            // 
            // cboSheetnames
            // 
            this.cboSheetnames.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSheetnames.Location = new System.Drawing.Point(110, 110);
            this.cboSheetnames.Name = "cboSheetnames";
            this.cboSheetnames.Size = new System.Drawing.Size(152, 24);
            this.cboSheetnames.TabIndex = 15;
            // 
            // txtRange
            // 
            this.txtRange.Location = new System.Drawing.Point(313, 110);
            this.txtRange.MaxLength = 20;
            this.txtRange.Name = "txtRange";
            this.txtRange.Size = new System.Drawing.Size(99, 22);
            this.txtRange.TabIndex = 17;
            this.txtRange.Text = "A2:AF29";
            this.txtRange.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblRange
            // 
            this.lblRange.Location = new System.Drawing.Point(264, 110);
            this.lblRange.Name = "lblRange";
            this.lblRange.Size = new System.Drawing.Size(47, 23);
            this.lblRange.TabIndex = 16;
            this.lblRange.Text = "Range";
            this.lblRange.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnOpenFileDlg
            // 
            this.btnOpenFileDlg.Location = new System.Drawing.Point(744, 84);
            this.btnOpenFileDlg.Name = "btnOpenFileDlg";
            this.btnOpenFileDlg.Size = new System.Drawing.Size(29, 23);
            this.btnOpenFileDlg.TabIndex = 13;
            this.btnOpenFileDlg.Text = "...";
            this.btnOpenFileDlg.Click += new System.EventHandler(this.btnOpenFileDlg_Click);
            // 
            // txtFileName
            // 
            this.txtFileName.Enabled = false;
            this.txtFileName.Location = new System.Drawing.Point(110, 84);
            this.txtFileName.Name = "txtFileName";
            this.txtFileName.ReadOnly = true;
            this.txtFileName.Size = new System.Drawing.Size(632, 22);
            this.txtFileName.TabIndex = 12;
            // 
            // lblFileName
            // 
            this.lblFileName.ForeColor = System.Drawing.Color.Maroon;
            this.lblFileName.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblFileName.Location = new System.Drawing.Point(5, 84);
            this.lblFileName.Name = "lblFileName";
            this.lblFileName.Size = new System.Drawing.Size(103, 23);
            this.lblFileName.TabIndex = 11;
            this.lblFileName.Text = "File Name";
            this.lblFileName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblSheet
            // 
            this.lblSheet.ForeColor = System.Drawing.Color.Maroon;
            this.lblSheet.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblSheet.Location = new System.Drawing.Point(5, 110);
            this.lblSheet.Name = "lblSheet";
            this.lblSheet.Size = new System.Drawing.Size(103, 23);
            this.lblSheet.TabIndex = 14;
            this.lblSheet.Text = "Sheet";
            this.lblSheet.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnSave.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnSave.Location = new System.Drawing.Point(5, 722);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(72, 26);
            this.btnSave.TabIndex = 19;
            this.btnSave.Text = "&Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnClose.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnClose.Location = new System.Drawing.Point(1333, 722);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(72, 26);
            this.btnClose.TabIndex = 21;
            this.btnClose.Text = "&Close";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnGetData
            // 
            this.btnGetData.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnGetData.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnGetData.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnGetData.Location = new System.Drawing.Point(79, 722);
            this.btnGetData.Name = "btnGetData";
            this.btnGetData.Size = new System.Drawing.Size(72, 26);
            this.btnGetData.TabIndex = 20;
            this.btnGetData.Text = "&Get Data";
            this.btnGetData.Click += new System.EventHandler(this.btnGetData_Click);
            // 
            // dgrdData
            // 
            this.dgrdData.AllowUpdate = false;
            this.dgrdData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgrdData.CaptionHeight = 17;
            this.dgrdData.Images.Add(((System.Drawing.Image)(resources.GetObject("dgrdData.Images"))));
            this.dgrdData.Location = new System.Drawing.Point(5, 136);
            this.dgrdData.Name = "dgrdData";
            this.dgrdData.PreviewInfo.Location = new System.Drawing.Point(0, 0);
            this.dgrdData.PreviewInfo.Size = new System.Drawing.Size(0, 0);
            this.dgrdData.PreviewInfo.ZoomFactor = 75D;
            this.dgrdData.PrintInfo.PageSettings = ((System.Drawing.Printing.PageSettings)(resources.GetObject("dgrdData.PrintInfo.PageSettings")));
            this.dgrdData.RowHeight = 15;
            this.dgrdData.Size = new System.Drawing.Size(1400, 583);
            this.dgrdData.TabIndex = 18;
            this.dgrdData.Text = "c1TrueDBGrid1";
            this.dgrdData.PropBag = resources.GetString("dgrdData.PropBag");
            // 
            // txtCycle
            // 
            this.txtCycle.Location = new System.Drawing.Point(110, 7);
            this.txtCycle.MaxLength = 20;
            this.txtCycle.Name = "txtCycle";
            this.txtCycle.Size = new System.Drawing.Size(152, 22);
            this.txtCycle.TabIndex = 1;
            this.txtCycle.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCycle_KeyDown);
            this.txtCycle.Validating += new System.ComponentModel.CancelEventHandler(this.txtCycle_Validating);
            // 
            // btnSearchCycle
            // 
            this.btnSearchCycle.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnSearchCycle.Location = new System.Drawing.Point(264, 7);
            this.btnSearchCycle.Name = "btnSearchCycle";
            this.btnSearchCycle.Size = new System.Drawing.Size(29, 23);
            this.btnSearchCycle.TabIndex = 2;
            this.btnSearchCycle.Text = "...";
            this.btnSearchCycle.Click += new System.EventHandler(this.btnSearchCycle_Click);
            // 
            // lblCycle
            // 
            this.lblCycle.ForeColor = System.Drawing.Color.Maroon;
            this.lblCycle.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblCycle.Location = new System.Drawing.Point(5, 7);
            this.lblCycle.Name = "lblCycle";
            this.lblCycle.Size = new System.Drawing.Size(103, 23);
            this.lblCycle.TabIndex = 0;
            this.lblCycle.Text = "Cycle";
            this.lblCycle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dtmMonth
            // 
            // 
            // 
            // 
            this.dtmMonth.Calendar.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.dtmMonth.CustomFormat = "dd-MM-yyyy";
            this.dtmMonth.EmptyAsNull = true;
            this.dtmMonth.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
            this.dtmMonth.Location = new System.Drawing.Point(110, 59);
            this.dtmMonth.Name = "dtmMonth";
            this.dtmMonth.Size = new System.Drawing.Size(152, 22);
            this.dtmMonth.TabIndex = 7;
            this.dtmMonth.Tag = null;
            this.dtmMonth.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.dtmMonth.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.DropDown;
            // 
            // lblMonth
            // 
            this.lblMonth.ForeColor = System.Drawing.Color.Maroon;
            this.lblMonth.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblMonth.Location = new System.Drawing.Point(5, 59);
            this.lblMonth.Name = "lblMonth";
            this.lblMonth.Size = new System.Drawing.Size(103, 23);
            this.lblMonth.TabIndex = 6;
            this.lblMonth.Text = "Month";
            this.lblMonth.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtProductionLine
            // 
            this.txtProductionLine.Location = new System.Drawing.Point(110, 32);
            this.txtProductionLine.MaxLength = 24;
            this.txtProductionLine.Name = "txtProductionLine";
            this.txtProductionLine.Size = new System.Drawing.Size(152, 22);
            this.txtProductionLine.TabIndex = 4;
            this.txtProductionLine.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtProductionLine_KeyDown);
            this.txtProductionLine.Validating += new System.ComponentModel.CancelEventHandler(this.txtProductionLine_Validating);
            // 
            // lblProductionLine
            // 
            this.lblProductionLine.ForeColor = System.Drawing.Color.Maroon;
            this.lblProductionLine.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblProductionLine.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblProductionLine.Location = new System.Drawing.Point(5, 32);
            this.lblProductionLine.Name = "lblProductionLine";
            this.lblProductionLine.Size = new System.Drawing.Size(103, 25);
            this.lblProductionLine.TabIndex = 3;
            this.lblProductionLine.Text = "Production Line";
            this.lblProductionLine.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnProductionLine
            // 
            this.btnProductionLine.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnProductionLine.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnProductionLine.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnProductionLine.Location = new System.Drawing.Point(264, 32);
            this.btnProductionLine.Name = "btnProductionLine";
            this.btnProductionLine.Size = new System.Drawing.Size(29, 23);
            this.btnProductionLine.TabIndex = 5;
            this.btnProductionLine.Text = "...";
            this.btnProductionLine.Click += new System.EventHandler(this.btnProductionLine_Click);
            // 
            // txtShiftCode
            // 
            this.txtShiftCode.Location = new System.Drawing.Point(366, 58);
            this.txtShiftCode.MaxLength = 24;
            this.txtShiftCode.Name = "txtShiftCode";
            this.txtShiftCode.Size = new System.Drawing.Size(82, 22);
            this.txtShiftCode.TabIndex = 9;
            this.txtShiftCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtShiftCode_KeyDown);
            this.txtShiftCode.Validating += new System.ComponentModel.CancelEventHandler(this.txtShiftCode_Validating);
            // 
            // btnShiftCode
            // 
            this.btnShiftCode.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnShiftCode.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnShiftCode.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnShiftCode.Location = new System.Drawing.Point(450, 58);
            this.btnShiftCode.Name = "btnShiftCode";
            this.btnShiftCode.Size = new System.Drawing.Size(29, 23);
            this.btnShiftCode.TabIndex = 10;
            this.btnShiftCode.Text = "...";
            this.btnShiftCode.Click += new System.EventHandler(this.btnShiftCode_Click);
            // 
            // lblShiftCode
            // 
            this.lblShiftCode.ForeColor = System.Drawing.Color.Maroon;
            this.lblShiftCode.Location = new System.Drawing.Point(295, 58);
            this.lblShiftCode.Name = "lblShiftCode";
            this.lblShiftCode.Size = new System.Drawing.Size(69, 23);
            this.lblShiftCode.TabIndex = 8;
            this.lblShiftCode.Text = "Shift Code";
            this.lblShiftCode.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ImportPlanData
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 15);
            this.ClientSize = new System.Drawing.Size(1410, 750);
            this.Controls.Add(this.txtShiftCode);
            this.Controls.Add(this.txtProductionLine);
            this.Controls.Add(this.txtCycle);
            this.Controls.Add(this.txtRange);
            this.Controls.Add(this.txtFileName);
            this.Controls.Add(this.dgrdData);
            this.Controls.Add(this.btnShiftCode);
            this.Controls.Add(this.lblShiftCode);
            this.Controls.Add(this.lblProductionLine);
            this.Controls.Add(this.btnProductionLine);
            this.Controls.Add(this.dtmMonth);
            this.Controls.Add(this.lblMonth);
            this.Controls.Add(this.btnSearchCycle);
            this.Controls.Add(this.lblCycle);
            this.Controls.Add(this.btnGetData);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.lblSheet);
            this.Controls.Add(this.lblFileName);
            this.Controls.Add(this.cboSheetnames);
            this.Controls.Add(this.lblRange);
            this.Controls.Add(this.btnOpenFileDlg);
            this.Name = "ImportPlanData";
            this.Text = "Import Planning Data";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.ImportPlanData_Closing);
            this.Load += new System.EventHandler(this.ImportPlanData_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgrdData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtmMonth)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        private System.Windows.Forms.ComboBox cboSheetnames;
        private System.Windows.Forms.TextBox txtRange;
        private System.Windows.Forms.Button btnOpenFileDlg;
        private System.Windows.Forms.TextBox txtFileName;
        private System.Windows.Forms.Label lblRange;
        private System.Windows.Forms.Label lblFileName;
        private System.Windows.Forms.Label lblSheet;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnGetData;
        private C1TrueDBGrid dgrdData;
        private System.Windows.Forms.TextBox txtCycle;
        private System.Windows.Forms.Button btnSearchCycle;
        private System.Windows.Forms.Label lblCycle;
        private C1DateEdit dtmMonth;
        private System.Windows.Forms.Label lblMonth;
        private System.Windows.Forms.TextBox txtProductionLine;
        private System.Windows.Forms.Label lblProductionLine;
        private System.Windows.Forms.Button btnProductionLine;
        private System.Windows.Forms.TextBox txtShiftCode;
        private System.Windows.Forms.Button btnShiftCode;
        private System.Windows.Forms.Label lblShiftCode;
    }
}
