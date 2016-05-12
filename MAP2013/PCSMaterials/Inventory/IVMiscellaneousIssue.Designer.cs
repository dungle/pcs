using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using C1.Win.C1Input;
using C1.Win.C1List;
using C1.Win.C1TrueDBGrid;
using System.Windows.Forms;


namespace PCSMaterials.Inventory
{
    partial class IVMiscellaneousIssue
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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

        private C1Button AddButton;
        private C1Button CloseButton;
        private C1TextBox CommentText;
        private C1Button DeleteButton;
        private C1Button DestBinButton;
        private C1TextBox DestBinText;
        private C1Button DestLocButton;
        private C1TextBox DestLocText;
        private C1Button DestMasLocButton;
        private C1TextBox DestMasLocText;
        private C1TrueDBGrid DetailGrid;
        private C1Button HelpButton;
        private C1Button PartyCodeButton;
        private C1TextBox PartyCodeText;
        private C1Button PartyNameButton;
        private C1TextBox PartyNameText;
        private C1DateEdit PostDatePicker;
        private C1Button PrintButton;
        private C1Button PurposeButton;
        private C1TextBox PurposeText;
        private C1Button SaveButton;
        private C1Button SourceBinButton;
        private C1TextBox SourceBinText;
        private C1Button SourceLocButton;
        private C1TextBox SourceLocText;
        private C1Button SourceMasLocButton;
        private C1TextBox SourceMasLocText;
        private C1Button TransNoButton;
        private C1TextBox TransNoText;
        private C1Combo cboCCN;
        private GroupBox grbDestination;
        private GroupBox grbSource;
        private Label lblCCN;
        private Label lblComment;
        private Label lblDestBin;
        private Label lblDestLoc;
        private Label lblDestMasLoc;
        private Label lblParty;
        private Label lblPostDate;
        private Label lblPurpose;
        private Label lblSourceBin;
        private Label lblSourceLoc;
        private Label lblSourceMasLoc;
        private Label lblTransNo;

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IVMiscellaneousIssue));
            this.lblCCN = new System.Windows.Forms.Label();
            this.grbSource = new System.Windows.Forms.GroupBox();
            this.SourceBinButton = new C1.Win.C1Input.C1Button();
            this.SourceLocButton = new C1.Win.C1Input.C1Button();
            this.SourceMasLocButton = new C1.Win.C1Input.C1Button();
            this.SourceBinText = new C1.Win.C1Input.C1TextBox();
            this.SourceLocText = new C1.Win.C1Input.C1TextBox();
            this.SourceMasLocText = new C1.Win.C1Input.C1TextBox();
            this.lblSourceLoc = new System.Windows.Forms.Label();
            this.lblSourceMasLoc = new System.Windows.Forms.Label();
            this.lblSourceBin = new System.Windows.Forms.Label();
            this.lblPostDate = new System.Windows.Forms.Label();
            this.lblComment = new System.Windows.Forms.Label();
            this.DetailGrid = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
            this.lblTransNo = new System.Windows.Forms.Label();
            this.grbDestination = new System.Windows.Forms.GroupBox();
            this.DestBinButton = new C1.Win.C1Input.C1Button();
            this.DestLocButton = new C1.Win.C1Input.C1Button();
            this.PartyNameButton = new C1.Win.C1Input.C1Button();
            this.PartyCodeButton = new C1.Win.C1Input.C1Button();
            this.DestMasLocButton = new C1.Win.C1Input.C1Button();
            this.DestBinText = new C1.Win.C1Input.C1TextBox();
            this.DestLocText = new C1.Win.C1Input.C1TextBox();
            this.PartyNameText = new C1.Win.C1Input.C1TextBox();
            this.PartyCodeText = new C1.Win.C1Input.C1TextBox();
            this.DestMasLocText = new C1.Win.C1Input.C1TextBox();
            this.lblDestLoc = new System.Windows.Forms.Label();
            this.lblDestMasLoc = new System.Windows.Forms.Label();
            this.lblDestBin = new System.Windows.Forms.Label();
            this.lblParty = new System.Windows.Forms.Label();
            this.cboCCN = new C1.Win.C1List.C1Combo();
            this.lblPurpose = new System.Windows.Forms.Label();
            this.PostDatePicker = new C1.Win.C1Input.C1DateEdit();
            this.TransNoButton = new C1.Win.C1Input.C1Button();
            this.TransNoText = new C1.Win.C1Input.C1TextBox();
            this.PurposeButton = new C1.Win.C1Input.C1Button();
            this.PurposeText = new C1.Win.C1Input.C1TextBox();
            this.CommentText = new C1.Win.C1Input.C1TextBox();
            this.SaveButton = new C1.Win.C1Input.C1Button();
            this.AddButton = new C1.Win.C1Input.C1Button();
            this.DeleteButton = new C1.Win.C1Input.C1Button();
            this.PrintButton = new C1.Win.C1Input.C1Button();
            this.HelpButton = new C1.Win.C1Input.C1Button();
            this.CloseButton = new C1.Win.C1Input.C1Button();
            this.ApproveDestroyButton = new C1.Win.C1Input.C1Button();
            this.grbSource.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SourceBinText)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SourceLocText)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SourceMasLocText)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DetailGrid)).BeginInit();
            this.grbDestination.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DestBinText)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DestLocText)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PartyNameText)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PartyCodeText)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DestMasLocText)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboCCN)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PostDatePicker)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TransNoText)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PurposeText)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CommentText)).BeginInit();
            this.SuspendLayout();
            // 
            // lblCCN
            // 
            resources.ApplyResources(this.lblCCN, "lblCCN");
            this.lblCCN.ForeColor = System.Drawing.Color.Maroon;
            this.lblCCN.Name = "lblCCN";
            // 
            // grbSource
            // 
            resources.ApplyResources(this.grbSource, "grbSource");
            this.grbSource.Controls.Add(this.SourceBinButton);
            this.grbSource.Controls.Add(this.SourceLocButton);
            this.grbSource.Controls.Add(this.SourceMasLocButton);
            this.grbSource.Controls.Add(this.SourceBinText);
            this.grbSource.Controls.Add(this.SourceLocText);
            this.grbSource.Controls.Add(this.SourceMasLocText);
            this.grbSource.Controls.Add(this.lblSourceLoc);
            this.grbSource.Controls.Add(this.lblSourceMasLoc);
            this.grbSource.Controls.Add(this.lblSourceBin);
            this.grbSource.Name = "grbSource";
            this.grbSource.TabStop = false;
            // 
            // SourceBinButton
            // 
            resources.ApplyResources(this.SourceBinButton, "SourceBinButton");
            this.SourceBinButton.Name = "SourceBinButton";
            this.SourceBinButton.UseVisualStyleBackColor = true;
            this.SourceBinButton.VisualStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.SourceBinButton.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.SourceBinButton.Click += new System.EventHandler(this.SourceBin_Click);
            // 
            // SourceLocButton
            // 
            resources.ApplyResources(this.SourceLocButton, "SourceLocButton");
            this.SourceLocButton.Name = "SourceLocButton";
            this.SourceLocButton.UseVisualStyleBackColor = true;
            this.SourceLocButton.VisualStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.SourceLocButton.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.SourceLocButton.Click += new System.EventHandler(this.SourceLoc_Click);
            // 
            // SourceMasLocButton
            // 
            resources.ApplyResources(this.SourceMasLocButton, "SourceMasLocButton");
            this.SourceMasLocButton.Name = "SourceMasLocButton";
            this.SourceMasLocButton.UseVisualStyleBackColor = true;
            this.SourceMasLocButton.VisualStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.SourceMasLocButton.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.SourceMasLocButton.Click += new System.EventHandler(this.SourceMasLoc_Click);
            // 
            // SourceBinText
            // 
            this.SourceBinText.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(177)))), ((int)(((byte)(187)))), ((int)(((byte)(214)))));
            resources.ApplyResources(this.SourceBinText, "SourceBinText");
            this.SourceBinText.Name = "SourceBinText";
            this.SourceBinText.TextDetached = true;
            this.SourceBinText.VisualStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.SourceBinText.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.SourceBinText.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SourceBin_KeyDown);
            this.SourceBinText.Validating += new System.ComponentModel.CancelEventHandler(this.SourceBin_Validating);
            // 
            // SourceLocText
            // 
            this.SourceLocText.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(177)))), ((int)(((byte)(187)))), ((int)(((byte)(214)))));
            resources.ApplyResources(this.SourceLocText, "SourceLocText");
            this.SourceLocText.Name = "SourceLocText";
            this.SourceLocText.TextDetached = true;
            this.SourceLocText.VisualStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.SourceLocText.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.SourceLocText.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SourceLoc_KeyDown);
            this.SourceLocText.Validating += new System.ComponentModel.CancelEventHandler(this.SourceLoc_Validating);
            // 
            // SourceMasLocText
            // 
            this.SourceMasLocText.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(177)))), ((int)(((byte)(187)))), ((int)(((byte)(214)))));
            resources.ApplyResources(this.SourceMasLocText, "SourceMasLocText");
            this.SourceMasLocText.Name = "SourceMasLocText";
            this.SourceMasLocText.TextDetached = true;
            this.SourceMasLocText.VisualStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.SourceMasLocText.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.SourceMasLocText.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SourceMasLoc_KeyDown);
            this.SourceMasLocText.Validating += new System.ComponentModel.CancelEventHandler(this.SourceMasLoc_Validating);
            // 
            // lblSourceLoc
            // 
            this.lblSourceLoc.ForeColor = System.Drawing.Color.Maroon;
            resources.ApplyResources(this.lblSourceLoc, "lblSourceLoc");
            this.lblSourceLoc.Name = "lblSourceLoc";
            // 
            // lblSourceMasLoc
            // 
            this.lblSourceMasLoc.ForeColor = System.Drawing.Color.Maroon;
            resources.ApplyResources(this.lblSourceMasLoc, "lblSourceMasLoc");
            this.lblSourceMasLoc.Name = "lblSourceMasLoc";
            // 
            // lblSourceBin
            // 
            resources.ApplyResources(this.lblSourceBin, "lblSourceBin");
            this.lblSourceBin.Name = "lblSourceBin";
            // 
            // lblPostDate
            // 
            this.lblPostDate.ForeColor = System.Drawing.Color.Maroon;
            resources.ApplyResources(this.lblPostDate, "lblPostDate");
            this.lblPostDate.Name = "lblPostDate";
            // 
            // lblComment
            // 
            this.lblComment.ForeColor = System.Drawing.SystemColors.ControlText;
            resources.ApplyResources(this.lblComment, "lblComment");
            this.lblComment.Name = "lblComment";
            // 
            // DetailGrid
            // 
            resources.ApplyResources(this.DetailGrid, "DetailGrid");
            this.DetailGrid.Images.Add(((System.Drawing.Image)(resources.GetObject("DetailGrid.Images"))));
            this.DetailGrid.Name = "DetailGrid";
            this.DetailGrid.PreviewInfo.Location = new System.Drawing.Point(0, 0);
            this.DetailGrid.PreviewInfo.Size = new System.Drawing.Size(0, 0);
            this.DetailGrid.PreviewInfo.ZoomFactor = 75D;
            this.DetailGrid.PrintInfo.PageSettings = ((System.Drawing.Printing.PageSettings)(resources.GetObject("DetailGrid.PrintInfo.PageSettings")));
            this.DetailGrid.VisualStyle = C1.Win.C1TrueDBGrid.VisualStyle.Office2007Blue;
            this.DetailGrid.AfterColUpdate += new C1.Win.C1TrueDBGrid.ColEventHandler(this.DetailGrid_AfterColUpdate);
            this.DetailGrid.BeforeColUpdate += new C1.Win.C1TrueDBGrid.BeforeColUpdateEventHandler(this.DetailGrid_BeforeColUpdate);
            this.DetailGrid.ButtonClick += new C1.Win.C1TrueDBGrid.ColEventHandler(this.DetailGrid_ButtonClick);
            this.DetailGrid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DetailGrid_KeyDown);
            this.DetailGrid.PropBag = resources.GetString("DetailGrid.PropBag");
            // 
            // lblTransNo
            // 
            this.lblTransNo.ForeColor = System.Drawing.Color.Maroon;
            resources.ApplyResources(this.lblTransNo, "lblTransNo");
            this.lblTransNo.Name = "lblTransNo";
            // 
            // grbDestination
            // 
            resources.ApplyResources(this.grbDestination, "grbDestination");
            this.grbDestination.Controls.Add(this.DestBinButton);
            this.grbDestination.Controls.Add(this.DestLocButton);
            this.grbDestination.Controls.Add(this.PartyNameButton);
            this.grbDestination.Controls.Add(this.PartyCodeButton);
            this.grbDestination.Controls.Add(this.DestMasLocButton);
            this.grbDestination.Controls.Add(this.DestBinText);
            this.grbDestination.Controls.Add(this.DestLocText);
            this.grbDestination.Controls.Add(this.PartyNameText);
            this.grbDestination.Controls.Add(this.PartyCodeText);
            this.grbDestination.Controls.Add(this.DestMasLocText);
            this.grbDestination.Controls.Add(this.lblDestLoc);
            this.grbDestination.Controls.Add(this.lblDestMasLoc);
            this.grbDestination.Controls.Add(this.lblDestBin);
            this.grbDestination.Controls.Add(this.lblParty);
            this.grbDestination.Name = "grbDestination";
            this.grbDestination.TabStop = false;
            // 
            // DestBinButton
            // 
            resources.ApplyResources(this.DestBinButton, "DestBinButton");
            this.DestBinButton.Name = "DestBinButton";
            this.DestBinButton.UseVisualStyleBackColor = true;
            this.DestBinButton.VisualStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.DestBinButton.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.DestBinButton.Click += new System.EventHandler(this.DestBin_Click);
            // 
            // DestLocButton
            // 
            resources.ApplyResources(this.DestLocButton, "DestLocButton");
            this.DestLocButton.Name = "DestLocButton";
            this.DestLocButton.UseVisualStyleBackColor = true;
            this.DestLocButton.VisualStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.DestLocButton.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.DestLocButton.Click += new System.EventHandler(this.DestLoc_Click);
            // 
            // PartyNameButton
            // 
            resources.ApplyResources(this.PartyNameButton, "PartyNameButton");
            this.PartyNameButton.Name = "PartyNameButton";
            this.PartyNameButton.UseVisualStyleBackColor = true;
            this.PartyNameButton.VisualStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.PartyNameButton.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.PartyNameButton.Click += new System.EventHandler(this.PartyName_Click);
            // 
            // PartyCodeButton
            // 
            resources.ApplyResources(this.PartyCodeButton, "PartyCodeButton");
            this.PartyCodeButton.Name = "PartyCodeButton";
            this.PartyCodeButton.UseVisualStyleBackColor = true;
            this.PartyCodeButton.VisualStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.PartyCodeButton.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.PartyCodeButton.Click += new System.EventHandler(this.Party_Click);
            // 
            // DestMasLocButton
            // 
            resources.ApplyResources(this.DestMasLocButton, "DestMasLocButton");
            this.DestMasLocButton.Name = "DestMasLocButton";
            this.DestMasLocButton.UseVisualStyleBackColor = true;
            this.DestMasLocButton.VisualStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.DestMasLocButton.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.DestMasLocButton.Click += new System.EventHandler(this.DestMasLoc_Click);
            // 
            // DestBinText
            // 
            this.DestBinText.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(177)))), ((int)(((byte)(187)))), ((int)(((byte)(214)))));
            resources.ApplyResources(this.DestBinText, "DestBinText");
            this.DestBinText.Name = "DestBinText";
            this.DestBinText.TextDetached = true;
            this.DestBinText.VisualStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.DestBinText.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.DestBinText.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DestBin_KeyDown);
            this.DestBinText.Validating += new System.ComponentModel.CancelEventHandler(this.DestBin_Validating);
            // 
            // DestLocText
            // 
            this.DestLocText.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(177)))), ((int)(((byte)(187)))), ((int)(((byte)(214)))));
            resources.ApplyResources(this.DestLocText, "DestLocText");
            this.DestLocText.Name = "DestLocText";
            this.DestLocText.TextDetached = true;
            this.DestLocText.VisualStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.DestLocText.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.DestLocText.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DestLoc_KeyDown);
            this.DestLocText.Validating += new System.ComponentModel.CancelEventHandler(this.DestLoc_Validating);
            // 
            // PartyNameText
            // 
            this.PartyNameText.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(177)))), ((int)(((byte)(187)))), ((int)(((byte)(214)))));
            resources.ApplyResources(this.PartyNameText, "PartyNameText");
            this.PartyNameText.Name = "PartyNameText";
            this.PartyNameText.TextDetached = true;
            this.PartyNameText.VisualStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.PartyNameText.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.PartyNameText.KeyDown += new System.Windows.Forms.KeyEventHandler(this.PartyName_KeyDown);
            this.PartyNameText.Validating += new System.ComponentModel.CancelEventHandler(this.PartyName_Validating);
            // 
            // PartyCodeText
            // 
            this.PartyCodeText.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(177)))), ((int)(((byte)(187)))), ((int)(((byte)(214)))));
            resources.ApplyResources(this.PartyCodeText, "PartyCodeText");
            this.PartyCodeText.Name = "PartyCodeText";
            this.PartyCodeText.TextDetached = true;
            this.PartyCodeText.VisualStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.PartyCodeText.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.PartyCodeText.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Party_KeyDown);
            this.PartyCodeText.Validating += new System.ComponentModel.CancelEventHandler(this.Party_Validating);
            // 
            // DestMasLocText
            // 
            this.DestMasLocText.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(177)))), ((int)(((byte)(187)))), ((int)(((byte)(214)))));
            resources.ApplyResources(this.DestMasLocText, "DestMasLocText");
            this.DestMasLocText.Name = "DestMasLocText";
            this.DestMasLocText.TextDetached = true;
            this.DestMasLocText.VisualStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.DestMasLocText.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.DestMasLocText.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DestMasLoc_KeyDown);
            this.DestMasLocText.Validating += new System.ComponentModel.CancelEventHandler(this.DestMasLoc_Validating);
            // 
            // lblDestLoc
            // 
            this.lblDestLoc.ForeColor = System.Drawing.SystemColors.ControlText;
            resources.ApplyResources(this.lblDestLoc, "lblDestLoc");
            this.lblDestLoc.Name = "lblDestLoc";
            // 
            // lblDestMasLoc
            // 
            this.lblDestMasLoc.ForeColor = System.Drawing.SystemColors.ControlText;
            resources.ApplyResources(this.lblDestMasLoc, "lblDestMasLoc");
            this.lblDestMasLoc.Name = "lblDestMasLoc";
            // 
            // lblDestBin
            // 
            resources.ApplyResources(this.lblDestBin, "lblDestBin");
            this.lblDestBin.Name = "lblDestBin";
            // 
            // lblParty
            // 
            resources.ApplyResources(this.lblParty, "lblParty");
            this.lblParty.Name = "lblParty";
            // 
            // cboCCN
            // 
            this.cboCCN.AddItemSeparator = ';';
            resources.ApplyResources(this.cboCCN, "cboCCN");
            this.cboCCN.CaptionHeight = 17;
            this.cboCCN.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.cboCCN.ColumnCaptionHeight = 17;
            this.cboCCN.ColumnFooterHeight = 17;
            this.cboCCN.ComboStyle = C1.Win.C1List.ComboStyleEnum.DropdownList;
            this.cboCCN.DeadAreaBackColor = System.Drawing.Color.Empty;
            this.cboCCN.EditorBackColor = System.Drawing.SystemColors.Window;
            this.cboCCN.EditorFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboCCN.EditorForeColor = System.Drawing.SystemColors.WindowText;
            this.cboCCN.Images.Add(((System.Drawing.Image)(resources.GetObject("cboCCN.Images"))));
            this.cboCCN.ItemHeight = 15;
            this.cboCCN.MatchEntryTimeout = ((long)(2000));
            this.cboCCN.MaxDropDownItems = ((short)(5));
            this.cboCCN.MaxLength = 32767;
            this.cboCCN.MouseCursor = System.Windows.Forms.Cursors.Default;
            this.cboCCN.Name = "cboCCN";
            this.cboCCN.RowSubDividerColor = System.Drawing.Color.DarkGray;
            this.cboCCN.VisualStyle = C1.Win.C1List.VisualStyle.Office2007Blue;
            this.cboCCN.PropBag = resources.GetString("cboCCN.PropBag");
            // 
            // lblPurpose
            // 
            this.lblPurpose.ForeColor = System.Drawing.Color.Maroon;
            resources.ApplyResources(this.lblPurpose, "lblPurpose");
            this.lblPurpose.Name = "lblPurpose";
            // 
            // PostDatePicker
            // 
            this.PostDatePicker.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(177)))), ((int)(((byte)(187)))), ((int)(((byte)(214)))));
            resources.ApplyResources(this.PostDatePicker, "PostDatePicker");
            // 
            // 
            // 
            this.PostDatePicker.Calendar.Font = ((System.Drawing.Font)(resources.GetObject("PostDatePicker.Calendar.Font")));
            this.PostDatePicker.Calendar.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("PostDatePicker.Calendar.RightToLeft")));
            this.PostDatePicker.Calendar.VisualStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.PostDatePicker.Calendar.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.PostDatePicker.Name = "PostDatePicker";
            this.PostDatePicker.VisualStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.PostDatePicker.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.PostDatePicker.Validating += new System.ComponentModel.CancelEventHandler(this.PostDate_Validating);
            // 
            // TransNoButton
            // 
            resources.ApplyResources(this.TransNoButton, "TransNoButton");
            this.TransNoButton.Name = "TransNoButton";
            this.TransNoButton.UseVisualStyleBackColor = true;
            this.TransNoButton.VisualStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.TransNoButton.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.TransNoButton.Click += new System.EventHandler(this.TransNo_Click);
            // 
            // TransNoText
            // 
            this.TransNoText.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(177)))), ((int)(((byte)(187)))), ((int)(((byte)(214)))));
            resources.ApplyResources(this.TransNoText, "TransNoText");
            this.TransNoText.Name = "TransNoText";
            this.TransNoText.TextDetached = true;
            this.TransNoText.VisualStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.TransNoText.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.TransNoText.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TransNo_KeyDown);
            this.TransNoText.Validating += new System.ComponentModel.CancelEventHandler(this.TransNo_Validating);
            // 
            // PurposeButton
            // 
            resources.ApplyResources(this.PurposeButton, "PurposeButton");
            this.PurposeButton.Name = "PurposeButton";
            this.PurposeButton.UseVisualStyleBackColor = true;
            this.PurposeButton.VisualStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.PurposeButton.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.PurposeButton.Click += new System.EventHandler(this.Purpose_Click);
            // 
            // PurposeText
            // 
            this.PurposeText.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(177)))), ((int)(((byte)(187)))), ((int)(((byte)(214)))));
            resources.ApplyResources(this.PurposeText, "PurposeText");
            this.PurposeText.Name = "PurposeText";
            this.PurposeText.TextDetached = true;
            this.PurposeText.VisualStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.PurposeText.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.PurposeText.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Purpose_KeyDown);
            this.PurposeText.Validating += new System.ComponentModel.CancelEventHandler(this.Purpose_Validating);
            // 
            // CommentText
            // 
            this.CommentText.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(177)))), ((int)(((byte)(187)))), ((int)(((byte)(214)))));
            resources.ApplyResources(this.CommentText, "CommentText");
            this.CommentText.Name = "CommentText";
            this.CommentText.TextDetached = true;
            this.CommentText.VisualStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.CommentText.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            // 
            // SaveButton
            // 
            resources.ApplyResources(this.SaveButton, "SaveButton");
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.VisualStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.SaveButton.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.SaveButton.Click += new System.EventHandler(this.Save_Click);
            // 
            // AddButton
            // 
            resources.ApplyResources(this.AddButton, "AddButton");
            this.AddButton.Name = "AddButton";
            this.AddButton.UseVisualStyleBackColor = true;
            this.AddButton.VisualStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.AddButton.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.AddButton.Click += new System.EventHandler(this.Add_Click);
            // 
            // DeleteButton
            // 
            resources.ApplyResources(this.DeleteButton, "DeleteButton");
            this.DeleteButton.Name = "DeleteButton";
            this.DeleteButton.UseVisualStyleBackColor = true;
            this.DeleteButton.VisualStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.DeleteButton.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.DeleteButton.Click += new System.EventHandler(this.Delete_Click);
            // 
            // PrintButton
            // 
            resources.ApplyResources(this.PrintButton, "PrintButton");
            this.PrintButton.Name = "PrintButton";
            this.PrintButton.UseVisualStyleBackColor = true;
            this.PrintButton.VisualStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.PrintButton.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.PrintButton.Click += new System.EventHandler(this.Print_Click);
            // 
            // HelpButton
            // 
            resources.ApplyResources(this.HelpButton, "HelpButton");
            this.HelpButton.Name = "HelpButton";
            this.HelpButton.UseVisualStyleBackColor = true;
            this.HelpButton.VisualStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.HelpButton.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            // 
            // CloseButton
            // 
            resources.ApplyResources(this.CloseButton, "CloseButton");
            this.CloseButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.UseVisualStyleBackColor = true;
            this.CloseButton.VisualStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.CloseButton.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.CloseButton.Click += new System.EventHandler(this.Close_Click);
            // 
            // ApproveDestroyButton
            // 
            resources.ApplyResources(this.ApproveDestroyButton, "ApproveDestroyButton");
            this.ApproveDestroyButton.Name = "ApproveDestroyButton";
            this.ApproveDestroyButton.UseVisualStyleBackColor = true;
            this.ApproveDestroyButton.VisualStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.ApproveDestroyButton.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.ApproveDestroyButton.Click += new System.EventHandler(this.ApproveDestroyButton_Click);
            // 
            // IVMiscellaneousIssue
            // 
            resources.ApplyResources(this, "$this");
            this.CancelButton = this.CloseButton;
            this.Controls.Add(this.CloseButton);
            this.Controls.Add(this.PrintButton);
            this.Controls.Add(this.SaveButton);
            this.Controls.Add(this.HelpButton);
            this.Controls.Add(this.ApproveDestroyButton);
            this.Controls.Add(this.DeleteButton);
            this.Controls.Add(this.AddButton);
            this.Controls.Add(this.CommentText);
            this.Controls.Add(this.PurposeButton);
            this.Controls.Add(this.TransNoButton);
            this.Controls.Add(this.PurposeText);
            this.Controls.Add(this.TransNoText);
            this.Controls.Add(this.PostDatePicker);
            this.Controls.Add(this.DetailGrid);
            this.Controls.Add(this.lblPurpose);
            this.Controls.Add(this.cboCCN);
            this.Controls.Add(this.grbDestination);
            this.Controls.Add(this.lblTransNo);
            this.Controls.Add(this.grbSource);
            this.Controls.Add(this.lblCCN);
            this.Controls.Add(this.lblComment);
            this.Controls.Add(this.lblPostDate);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.Name = "IVMiscellaneousIssue";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.MiscIssueFormClosing);
            this.Load += new System.EventHandler(this.FormLoad);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormKeyDown);
            this.grbSource.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SourceBinText)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SourceLocText)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SourceMasLocText)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DetailGrid)).EndInit();
            this.grbDestination.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DestBinText)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DestLocText)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PartyNameText)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PartyCodeText)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DestMasLocText)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboCCN)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PostDatePicker)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TransNoText)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PurposeText)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CommentText)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private C1Button ApproveDestroyButton;
    }
}
