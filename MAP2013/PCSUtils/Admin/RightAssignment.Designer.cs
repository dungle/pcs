namespace PCSUtils.Admin
{
    partial class RightAssignment
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RightAssignment));
            Syncfusion.Windows.Forms.Tools.TreeNodeAdvStyleInfo treeNodeAdvStyleInfo1 = new Syncfusion.Windows.Forms.Tools.TreeNodeAdvStyleInfo();
            this.RoleListGroup = new System.Windows.Forms.GroupBox();
            this.RoleList = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.FunctionListGroup = new System.Windows.Forms.GroupBox();
            this.FunctionListTree = new Syncfusion.Windows.Forms.Tools.TreeViewAdv();
            this.SaveButton = new C1.Win.C1Input.C1Button();
            this.CloseButton = new C1.Win.C1Input.C1Button();
            this.HelpButton = new C1.Win.C1Input.C1Button();
            this.RoleListGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RoleList)).BeginInit();
            this.FunctionListGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FunctionListTree)).BeginInit();
            this.SuspendLayout();
            // 
            // RoleListGroup
            // 
            resources.ApplyResources(this.RoleListGroup, "RoleListGroup");
            this.RoleListGroup.Controls.Add(this.RoleList);
            this.RoleListGroup.Name = "RoleListGroup";
            this.RoleListGroup.TabStop = false;
            // 
            // RoleList
            // 
            this.RoleList.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
            this.RoleList.AllowEditing = false;
            this.RoleList.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            resources.ApplyResources(this.RoleList, "RoleList");
            this.RoleList.AutoResize = true;
            this.RoleList.Name = "RoleList";
            this.RoleList.Rows.Count = 1;
            this.RoleList.Rows.DefaultSize = 17;
            this.RoleList.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.RoleList.StyleInfo = resources.GetString("RoleList.StyleInfo");
            this.RoleList.VisualStyle = C1.Win.C1FlexGrid.VisualStyle.Office2007Blue;
            this.RoleList.AfterRowColChange += new C1.Win.C1FlexGrid.RangeEventHandler(this.RoleList_AfterRowColChange);
            // 
            // FunctionListGroup
            // 
            resources.ApplyResources(this.FunctionListGroup, "FunctionListGroup");
            this.FunctionListGroup.Controls.Add(this.FunctionListTree);
            this.FunctionListGroup.Name = "FunctionListGroup";
            this.FunctionListGroup.TabStop = false;
            // 
            // FunctionListTree
            // 
            resources.ApplyResources(this.FunctionListTree, "FunctionListTree");
            treeNodeAdvStyleInfo1.EnsureDefaultOptionedChild = true;
            this.FunctionListTree.BaseStylePairs.AddRange(new Syncfusion.Windows.Forms.Tools.StyleNamePair[] {
            new Syncfusion.Windows.Forms.Tools.StyleNamePair("Standard", treeNodeAdvStyleInfo1)});
            // 
            // 
            // 
            this.FunctionListTree.HelpTextControl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.FunctionListTree.HelpTextControl.Location = ((System.Drawing.Point)(resources.GetObject("FunctionListTree.HelpTextControl.Location")));
            this.FunctionListTree.HelpTextControl.Name = "helpText";
            this.FunctionListTree.HelpTextControl.Size = ((System.Drawing.Size)(resources.GetObject("FunctionListTree.HelpTextControl.Size")));
            this.FunctionListTree.HelpTextControl.TabIndex = ((int)(resources.GetObject("FunctionListTree.HelpTextControl.TabIndex")));
            this.FunctionListTree.HelpTextControl.Text = resources.GetString("FunctionListTree.HelpTextControl.Text");
            this.FunctionListTree.LoadOnDemand = true;
            this.FunctionListTree.Name = "FunctionListTree";
            this.FunctionListTree.Office2007ScrollBars = true;
            this.FunctionListTree.ShowCheckBoxes = true;
            // 
            // 
            // 
            this.FunctionListTree.ToolTipControl.BackColor = System.Drawing.SystemColors.Info;
            this.FunctionListTree.ToolTipControl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.FunctionListTree.ToolTipControl.Location = ((System.Drawing.Point)(resources.GetObject("FunctionListTree.ToolTipControl.Location")));
            this.FunctionListTree.ToolTipControl.Name = "toolTip";
            this.FunctionListTree.ToolTipControl.Size = ((System.Drawing.Size)(resources.GetObject("FunctionListTree.ToolTipControl.Size")));
            this.FunctionListTree.ToolTipControl.TabIndex = ((int)(resources.GetObject("FunctionListTree.ToolTipControl.TabIndex")));
            this.FunctionListTree.ToolTipControl.Text = resources.GetString("FunctionListTree.ToolTipControl.Text");
            this.FunctionListTree.BeforeExpand += new Syncfusion.Windows.Forms.Tools.TreeViewAdvCancelableNodeEventHandler(this.FunctionListTree_BeforeExpand);
            // 
            // SaveButton
            // 
            resources.ApplyResources(this.SaveButton, "SaveButton");
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // CloseButton
            // 
            resources.ApplyResources(this.CloseButton, "CloseButton");
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.UseVisualStyleBackColor = true;
            this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
            // 
            // HelpButton
            // 
            resources.ApplyResources(this.HelpButton, "HelpButton");
            this.HelpButton.Name = "HelpButton";
            this.HelpButton.UseVisualStyleBackColor = true;
            // 
            // RightAssignment
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.HelpButton);
            this.Controls.Add(this.CloseButton);
            this.Controls.Add(this.SaveButton);
            this.Controls.Add(this.FunctionListGroup);
            this.Controls.Add(this.RoleListGroup);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "RightAssignment";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.RightAssignment_FormClosing);
            this.Load += new System.EventHandler(this.RightAssignment_Load);
            this.RoleListGroup.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.RoleList)).EndInit();
            this.FunctionListGroup.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.FunctionListTree)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox RoleListGroup;
        private System.Windows.Forms.GroupBox FunctionListGroup;
        private C1.Win.C1Input.C1Button SaveButton;
        private C1.Win.C1Input.C1Button CloseButton;
        private C1.Win.C1Input.C1Button HelpButton;
        private C1.Win.C1FlexGrid.C1FlexGrid RoleList;
        private Syncfusion.Windows.Forms.Tools.TreeViewAdv FunctionListTree;
    }
}