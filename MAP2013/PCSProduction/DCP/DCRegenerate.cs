using System;
using System.Collections.Generic;
using System.Data;
using System.Collections;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using PCSComProduction.DCP.BO;
using PCSComProduction.DCP.DS;
using PCSComUtils.Common;
using PCSComUtils.Common.BO;
using PCSComUtils.DataContext;
using PCSComUtils.PCSExc;
using PCSUtils.Log;
using PCSUtils.Utils;

namespace PCSProduction.DCP
{
    /// <summary>
    /// Summary description for DCRegenerate.
    /// </summary>
    public class DCRegenerate : System.Windows.Forms.Form
    {
        #region Members
        private int intCCNID = 0;
        private int intDCOptionMasterID = 0;
        #endregion

        const string THIS = "PCSProduction.DCP.DCRegenerate";
        const string DETAIL_CAPACITY_PLANNING = "DCP ";
        DateTime dtmServerDate = DateTime.MinValue;
        Thread thrNewGen;
        private System.Windows.Forms.Label lblCCN;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnHelp;
        private System.Windows.Forms.Button btnPreviewCycle;
        private System.Windows.Forms.Button btnProcess;
        private C1.Win.C1List.C1Combo cboCCN;
        private System.Windows.Forms.PictureBox picRunning;
        private System.Windows.Forms.Panel pnControls;
        private System.Windows.Forms.Button btnCycle;
        private System.Windows.Forms.TextBox txtCycle;
        private System.Windows.Forms.Label lblCycle;
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;
        private System.Windows.Forms.Button btnUpdateStock;
        private System.Windows.Forms.Button btnProLine;
        private System.Windows.Forms.TextBox txtProductionLine;
        private System.Windows.Forms.Label lblProLine;
        bool m_blnAutoRun = false;

        delegate void SetControlValueCallback(Control control, string propName, object propValue);

        /// <summary>
        /// Set control property value thread-safe
        /// </summary>
        /// <param name="control"></param>
        /// <param name="propName"></param>
        /// <param name="propValue"></param>
        private void SetControlPropertyValue(Control control, string propName, object propValue)
        {
            if (control.InvokeRequired)
            {
                SetControlValueCallback d = new SetControlValueCallback(SetControlPropertyValue);
                control.Invoke(d, new object[] { control, propName, propValue });
            }
            else
            {
                var type = control.GetType();
                var property = type.GetProperty(propName);
                if (property != null)
                {
                    property.SetValue(control, propValue, null);
                }
            }
        }

        public DCRegenerate()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //

        }

        /// <summary>
        /// 
        /// </summary>
        public DCRegenerate(string pstrCycleOption, int pintDCPCycleOptionMasterID)
            : this()
        {
            txtCycle.Text = pstrCycleOption;
            txtCycle.Tag = pintDCPCycleOptionMasterID;
            m_blnAutoRun = true;
        }

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
            System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(DCRegenerate));
            this.btnClose = new System.Windows.Forms.Button();
            this.btnHelp = new System.Windows.Forms.Button();
            this.lblCCN = new System.Windows.Forms.Label();
            this.btnPreviewCycle = new System.Windows.Forms.Button();
            this.btnProcess = new System.Windows.Forms.Button();
            this.cboCCN = new C1.Win.C1List.C1Combo();
            this.pnControls = new System.Windows.Forms.Panel();
            this.picRunning = new System.Windows.Forms.PictureBox();
            this.btnCycle = new System.Windows.Forms.Button();
            this.txtCycle = new System.Windows.Forms.TextBox();
            this.lblCycle = new System.Windows.Forms.Label();
            this.btnUpdateStock = new System.Windows.Forms.Button();
            this.btnProLine = new System.Windows.Forms.Button();
            this.txtProductionLine = new System.Windows.Forms.TextBox();
            this.lblProLine = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.cboCCN)).BeginInit();
            this.pnControls.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.AccessibleDescription = resources.GetString("btnClose.AccessibleDescription");
            this.btnClose.AccessibleName = resources.GetString("btnClose.AccessibleName");
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnClose.Anchor")));
            this.btnClose.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClose.BackgroundImage")));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnClose.Dock")));
            this.btnClose.Enabled = ((bool)(resources.GetObject("btnClose.Enabled")));
            this.btnClose.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnClose.FlatStyle")));
            this.btnClose.Font = ((System.Drawing.Font)(resources.GetObject("btnClose.Font")));
            this.btnClose.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.Image")));
            this.btnClose.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnClose.ImageAlign")));
            this.btnClose.ImageIndex = ((int)(resources.GetObject("btnClose.ImageIndex")));
            this.btnClose.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnClose.ImeMode")));
            this.btnClose.Location = ((System.Drawing.Point)(resources.GetObject("btnClose.Location")));
            this.btnClose.Name = "btnClose";
            this.btnClose.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnClose.RightToLeft")));
            this.btnClose.Size = ((System.Drawing.Size)(resources.GetObject("btnClose.Size")));
            this.btnClose.TabIndex = ((int)(resources.GetObject("btnClose.TabIndex")));
            this.btnClose.Text = resources.GetString("btnClose.Text");
            this.btnClose.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnClose.TextAlign")));
            this.btnClose.Visible = ((bool)(resources.GetObject("btnClose.Visible")));
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnHelp
            // 
            this.btnHelp.AccessibleDescription = resources.GetString("btnHelp.AccessibleDescription");
            this.btnHelp.AccessibleName = resources.GetString("btnHelp.AccessibleName");
            this.btnHelp.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnHelp.Anchor")));
            this.btnHelp.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnHelp.BackgroundImage")));
            this.btnHelp.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnHelp.Dock")));
            this.btnHelp.Enabled = ((bool)(resources.GetObject("btnHelp.Enabled")));
            this.btnHelp.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnHelp.FlatStyle")));
            this.btnHelp.Font = ((System.Drawing.Font)(resources.GetObject("btnHelp.Font")));
            this.btnHelp.Image = ((System.Drawing.Image)(resources.GetObject("btnHelp.Image")));
            this.btnHelp.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnHelp.ImageAlign")));
            this.btnHelp.ImageIndex = ((int)(resources.GetObject("btnHelp.ImageIndex")));
            this.btnHelp.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnHelp.ImeMode")));
            this.btnHelp.Location = ((System.Drawing.Point)(resources.GetObject("btnHelp.Location")));
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnHelp.RightToLeft")));
            this.btnHelp.Size = ((System.Drawing.Size)(resources.GetObject("btnHelp.Size")));
            this.btnHelp.TabIndex = ((int)(resources.GetObject("btnHelp.TabIndex")));
            this.btnHelp.Text = resources.GetString("btnHelp.Text");
            this.btnHelp.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnHelp.TextAlign")));
            this.btnHelp.Visible = ((bool)(resources.GetObject("btnHelp.Visible")));
            this.btnHelp.Click += new System.EventHandler(this.btnHelp_Click);
            // 
            // lblCCN
            // 
            this.lblCCN.AccessibleDescription = resources.GetString("lblCCN.AccessibleDescription");
            this.lblCCN.AccessibleName = resources.GetString("lblCCN.AccessibleName");
            this.lblCCN.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblCCN.Anchor")));
            this.lblCCN.AutoSize = ((bool)(resources.GetObject("lblCCN.AutoSize")));
            this.lblCCN.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblCCN.Dock")));
            this.lblCCN.Enabled = ((bool)(resources.GetObject("lblCCN.Enabled")));
            this.lblCCN.Font = ((System.Drawing.Font)(resources.GetObject("lblCCN.Font")));
            this.lblCCN.ForeColor = System.Drawing.Color.Maroon;
            this.lblCCN.Image = ((System.Drawing.Image)(resources.GetObject("lblCCN.Image")));
            this.lblCCN.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblCCN.ImageAlign")));
            this.lblCCN.ImageIndex = ((int)(resources.GetObject("lblCCN.ImageIndex")));
            this.lblCCN.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblCCN.ImeMode")));
            this.lblCCN.Location = ((System.Drawing.Point)(resources.GetObject("lblCCN.Location")));
            this.lblCCN.Name = "lblCCN";
            this.lblCCN.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblCCN.RightToLeft")));
            this.lblCCN.Size = ((System.Drawing.Size)(resources.GetObject("lblCCN.Size")));
            this.lblCCN.TabIndex = ((int)(resources.GetObject("lblCCN.TabIndex")));
            this.lblCCN.Text = resources.GetString("lblCCN.Text");
            this.lblCCN.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblCCN.TextAlign")));
            this.lblCCN.Visible = ((bool)(resources.GetObject("lblCCN.Visible")));
            // 
            // btnPreviewCycle
            // 
            this.btnPreviewCycle.AccessibleDescription = resources.GetString("btnPreviewCycle.AccessibleDescription");
            this.btnPreviewCycle.AccessibleName = resources.GetString("btnPreviewCycle.AccessibleName");
            this.btnPreviewCycle.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnPreviewCycle.Anchor")));
            this.btnPreviewCycle.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnPreviewCycle.BackgroundImage")));
            this.btnPreviewCycle.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnPreviewCycle.Dock")));
            this.btnPreviewCycle.Enabled = ((bool)(resources.GetObject("btnPreviewCycle.Enabled")));
            this.btnPreviewCycle.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnPreviewCycle.FlatStyle")));
            this.btnPreviewCycle.Font = ((System.Drawing.Font)(resources.GetObject("btnPreviewCycle.Font")));
            this.btnPreviewCycle.Image = ((System.Drawing.Image)(resources.GetObject("btnPreviewCycle.Image")));
            this.btnPreviewCycle.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnPreviewCycle.ImageAlign")));
            this.btnPreviewCycle.ImageIndex = ((int)(resources.GetObject("btnPreviewCycle.ImageIndex")));
            this.btnPreviewCycle.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnPreviewCycle.ImeMode")));
            this.btnPreviewCycle.Location = ((System.Drawing.Point)(resources.GetObject("btnPreviewCycle.Location")));
            this.btnPreviewCycle.Name = "btnPreviewCycle";
            this.btnPreviewCycle.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnPreviewCycle.RightToLeft")));
            this.btnPreviewCycle.Size = ((System.Drawing.Size)(resources.GetObject("btnPreviewCycle.Size")));
            this.btnPreviewCycle.TabIndex = ((int)(resources.GetObject("btnPreviewCycle.TabIndex")));
            this.btnPreviewCycle.Text = resources.GetString("btnPreviewCycle.Text");
            this.btnPreviewCycle.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnPreviewCycle.TextAlign")));
            this.btnPreviewCycle.Visible = ((bool)(resources.GetObject("btnPreviewCycle.Visible")));
            this.btnPreviewCycle.Click += new System.EventHandler(this.btnPreviewCycle_Click);
            // 
            // btnProcess
            // 
            this.btnProcess.AccessibleDescription = resources.GetString("btnProcess.AccessibleDescription");
            this.btnProcess.AccessibleName = resources.GetString("btnProcess.AccessibleName");
            this.btnProcess.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnProcess.Anchor")));
            this.btnProcess.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnProcess.BackgroundImage")));
            this.btnProcess.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnProcess.Dock")));
            this.btnProcess.Enabled = ((bool)(resources.GetObject("btnProcess.Enabled")));
            this.btnProcess.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnProcess.FlatStyle")));
            this.btnProcess.Font = ((System.Drawing.Font)(resources.GetObject("btnProcess.Font")));
            this.btnProcess.Image = ((System.Drawing.Image)(resources.GetObject("btnProcess.Image")));
            this.btnProcess.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnProcess.ImageAlign")));
            this.btnProcess.ImageIndex = ((int)(resources.GetObject("btnProcess.ImageIndex")));
            this.btnProcess.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnProcess.ImeMode")));
            this.btnProcess.Location = ((System.Drawing.Point)(resources.GetObject("btnProcess.Location")));
            this.btnProcess.Name = "btnProcess";
            this.btnProcess.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnProcess.RightToLeft")));
            this.btnProcess.Size = ((System.Drawing.Size)(resources.GetObject("btnProcess.Size")));
            this.btnProcess.TabIndex = ((int)(resources.GetObject("btnProcess.TabIndex")));
            this.btnProcess.Text = resources.GetString("btnProcess.Text");
            this.btnProcess.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnProcess.TextAlign")));
            this.btnProcess.Visible = ((bool)(resources.GetObject("btnProcess.Visible")));
            this.btnProcess.Click += new System.EventHandler(this.btnProcess_Click);
            // 
            // cboCCN
            // 
            this.cboCCN.AccessibleDescription = resources.GetString("cboCCN.AccessibleDescription");
            this.cboCCN.AccessibleName = resources.GetString("cboCCN.AccessibleName");
            this.cboCCN.AddItemSeparator = ';';
            this.cboCCN.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("cboCCN.Anchor")));
            this.cboCCN.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cboCCN.BackgroundImage")));
            this.cboCCN.Caption = "";
            this.cboCCN.CaptionHeight = 17;
            this.cboCCN.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.cboCCN.ColumnCaptionHeight = 17;
            this.cboCCN.ColumnFooterHeight = 17;
            this.cboCCN.ContentHeight = 15;
            this.cboCCN.DeadAreaBackColor = System.Drawing.Color.Empty;
            this.cboCCN.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("cboCCN.Dock")));
            this.cboCCN.DropdownPosition = C1.Win.C1List.DropdownPositionEnum.LeftDown;
            this.cboCCN.DropDownWidth = 200;
            this.cboCCN.EditorBackColor = System.Drawing.SystemColors.Window;
            this.cboCCN.EditorFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
            this.cboCCN.EditorForeColor = System.Drawing.SystemColors.WindowText;
            this.cboCCN.EditorHeight = 15;
            this.cboCCN.Enabled = ((bool)(resources.GetObject("cboCCN.Enabled")));
            this.cboCCN.FlatStyle = C1.Win.C1List.FlatModeEnum.System;
            this.cboCCN.Font = ((System.Drawing.Font)(resources.GetObject("cboCCN.Font")));
            this.cboCCN.GapHeight = 2;
            this.cboCCN.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("cboCCN.ImeMode")));
            this.cboCCN.ItemHeight = 15;
            this.cboCCN.Location = ((System.Drawing.Point)(resources.GetObject("cboCCN.Location")));
            this.cboCCN.MatchEntryTimeout = ((long)(2000));
            this.cboCCN.MaxDropDownItems = ((short)(5));
            this.cboCCN.MaxLength = 32767;
            this.cboCCN.MouseCursor = System.Windows.Forms.Cursors.Default;
            this.cboCCN.Name = "cboCCN";
            this.cboCCN.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("cboCCN.RightToLeft")));
            this.cboCCN.RowDivider.Color = System.Drawing.Color.DarkGray;
            this.cboCCN.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None;
            this.cboCCN.RowSubDividerColor = System.Drawing.Color.DarkGray;
            this.cboCCN.Size = ((System.Drawing.Size)(resources.GetObject("cboCCN.Size")));
            this.cboCCN.TabIndex = ((int)(resources.GetObject("cboCCN.TabIndex")));
            this.cboCCN.Text = resources.GetString("cboCCN.Text");
            this.cboCCN.Visible = ((bool)(resources.GetObject("cboCCN.Visible")));
            this.cboCCN.PropBag = "<?xml version=\"1.0\"?><Blob><Styles type=\"C1.Win.C1List.Design.ContextWrapper\"><Da" +
                "ta>Group{BackColor:ControlDark;Border:None,,0, 0, 0, 0;AlignVert:Center;}Style2{" +
                "}Style5{}Style4{}Style7{}Style6{}EvenRow{BackColor:Aqua;}Selected{ForeColor:High" +
                "lightText;BackColor:Highlight;}Style3{}Inactive{ForeColor:InactiveCaptionText;Ba" +
                "ckColor:InactiveCaption;}Footer{}Caption{AlignHorz:Center;}Normal{}HighlightRow{" +
                "ForeColor:HighlightText;BackColor:Highlight;}Style9{AlignHorz:Near;}OddRow{}Reco" +
                "rdSelector{AlignImage:Center;}Heading{Wrap:True;AlignVert:Center;Border:Raised,," +
                "1, 1, 1, 1;ForeColor:ControlText;BackColor:Control;}Style8{}Style10{}Style11{}St" +
                "yle1{}</Data></Styles><Splits><C1.Win.C1List.ListBoxView AllowColSelect=\"False\" " +
                "Name=\"\" CaptionHeight=\"17\" ColumnCaptionHeight=\"17\" ColumnFooterHeight=\"17\" Vert" +
                "icalScrollGroup=\"1\" HorizontalScrollGroup=\"1\"><ClientRect>0, 0, 116, 156</Client" +
                "Rect><VScrollBar><Width>17</Width></VScrollBar><HScrollBar><Height>17</Height></" +
                "HScrollBar><CaptionStyle parent=\"Style2\" me=\"Style9\" /><EvenRowStyle parent=\"Eve" +
                "nRow\" me=\"Style7\" /><FooterStyle parent=\"Footer\" me=\"Style3\" /><GroupStyle paren" +
                "t=\"Group\" me=\"Style11\" /><HeadingStyle parent=\"Heading\" me=\"Style2\" /><HighLight" +
                "RowStyle parent=\"HighlightRow\" me=\"Style6\" /><InactiveStyle parent=\"Inactive\" me" +
                "=\"Style4\" /><OddRowStyle parent=\"OddRow\" me=\"Style8\" /><RecordSelectorStyle pare" +
                "nt=\"RecordSelector\" me=\"Style10\" /><SelectedStyle parent=\"Selected\" me=\"Style5\" " +
                "/><Style parent=\"Normal\" me=\"Style1\" /></C1.Win.C1List.ListBoxView></Splits><Nam" +
                "edStyles><Style parent=\"\" me=\"Normal\" /><Style parent=\"Normal\" me=\"Heading\" /><S" +
                "tyle parent=\"Heading\" me=\"Footer\" /><Style parent=\"Heading\" me=\"Caption\" /><Styl" +
                "e parent=\"Heading\" me=\"Inactive\" /><Style parent=\"Normal\" me=\"Selected\" /><Style" +
                " parent=\"Normal\" me=\"HighlightRow\" /><Style parent=\"Normal\" me=\"EvenRow\" /><Styl" +
                "e parent=\"Normal\" me=\"OddRow\" /><Style parent=\"Heading\" me=\"RecordSelector\" /><S" +
                "tyle parent=\"Caption\" me=\"Group\" /></NamedStyles><vertSplits>1</vertSplits><horz" +
                "Splits>1</horzSplits><Layout>Modified</Layout><DefaultRecSelWidth>16</DefaultRec" +
                "SelWidth></Blob>";
            // 
            // pnControls
            // 
            this.pnControls.AccessibleDescription = resources.GetString("pnControls.AccessibleDescription");
            this.pnControls.AccessibleName = resources.GetString("pnControls.AccessibleName");
            this.pnControls.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("pnControls.Anchor")));
            this.pnControls.AutoScroll = ((bool)(resources.GetObject("pnControls.AutoScroll")));
            this.pnControls.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("pnControls.AutoScrollMargin")));
            this.pnControls.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("pnControls.AutoScrollMinSize")));
            this.pnControls.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnControls.BackgroundImage")));
            this.pnControls.Controls.Add(this.picRunning);
            this.pnControls.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("pnControls.Dock")));
            this.pnControls.Enabled = ((bool)(resources.GetObject("pnControls.Enabled")));
            this.pnControls.Font = ((System.Drawing.Font)(resources.GetObject("pnControls.Font")));
            this.pnControls.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("pnControls.ImeMode")));
            this.pnControls.Location = ((System.Drawing.Point)(resources.GetObject("pnControls.Location")));
            this.pnControls.Name = "pnControls";
            this.pnControls.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("pnControls.RightToLeft")));
            this.pnControls.Size = ((System.Drawing.Size)(resources.GetObject("pnControls.Size")));
            this.pnControls.TabIndex = ((int)(resources.GetObject("pnControls.TabIndex")));
            this.pnControls.Text = resources.GetString("pnControls.Text");
            this.pnControls.Visible = ((bool)(resources.GetObject("pnControls.Visible")));
            // 
            // picRunning
            // 
            this.picRunning.AccessibleDescription = resources.GetString("picRunning.AccessibleDescription");
            this.picRunning.AccessibleName = resources.GetString("picRunning.AccessibleName");
            this.picRunning.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("picRunning.Anchor")));
            this.picRunning.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("picRunning.BackgroundImage")));
            this.picRunning.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("picRunning.Dock")));
            this.picRunning.Enabled = ((bool)(resources.GetObject("picRunning.Enabled")));
            this.picRunning.Font = ((System.Drawing.Font)(resources.GetObject("picRunning.Font")));
            this.picRunning.Image = ((System.Drawing.Image)(resources.GetObject("picRunning.Image")));
            this.picRunning.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("picRunning.ImeMode")));
            this.picRunning.Location = ((System.Drawing.Point)(resources.GetObject("picRunning.Location")));
            this.picRunning.Name = "picRunning";
            this.picRunning.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("picRunning.RightToLeft")));
            this.picRunning.Size = ((System.Drawing.Size)(resources.GetObject("picRunning.Size")));
            this.picRunning.SizeMode = ((System.Windows.Forms.PictureBoxSizeMode)(resources.GetObject("picRunning.SizeMode")));
            this.picRunning.TabIndex = ((int)(resources.GetObject("picRunning.TabIndex")));
            this.picRunning.TabStop = false;
            this.picRunning.Text = resources.GetString("picRunning.Text");
            this.picRunning.Visible = ((bool)(resources.GetObject("picRunning.Visible")));
            // 
            // btnCycle
            // 
            this.btnCycle.AccessibleDescription = resources.GetString("btnCycle.AccessibleDescription");
            this.btnCycle.AccessibleName = resources.GetString("btnCycle.AccessibleName");
            this.btnCycle.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnCycle.Anchor")));
            this.btnCycle.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCycle.BackgroundImage")));
            this.btnCycle.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnCycle.Dock")));
            this.btnCycle.Enabled = ((bool)(resources.GetObject("btnCycle.Enabled")));
            this.btnCycle.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnCycle.FlatStyle")));
            this.btnCycle.Font = ((System.Drawing.Font)(resources.GetObject("btnCycle.Font")));
            this.btnCycle.Image = ((System.Drawing.Image)(resources.GetObject("btnCycle.Image")));
            this.btnCycle.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnCycle.ImageAlign")));
            this.btnCycle.ImageIndex = ((int)(resources.GetObject("btnCycle.ImageIndex")));
            this.btnCycle.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnCycle.ImeMode")));
            this.btnCycle.Location = ((System.Drawing.Point)(resources.GetObject("btnCycle.Location")));
            this.btnCycle.Name = "btnCycle";
            this.btnCycle.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnCycle.RightToLeft")));
            this.btnCycle.Size = ((System.Drawing.Size)(resources.GetObject("btnCycle.Size")));
            this.btnCycle.TabIndex = ((int)(resources.GetObject("btnCycle.TabIndex")));
            this.btnCycle.Text = resources.GetString("btnCycle.Text");
            this.btnCycle.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnCycle.TextAlign")));
            this.btnCycle.Visible = ((bool)(resources.GetObject("btnCycle.Visible")));
            this.btnCycle.Click += new System.EventHandler(this.btnCycle_Click);
            // 
            // txtCycle
            // 
            this.txtCycle.AccessibleDescription = resources.GetString("txtCycle.AccessibleDescription");
            this.txtCycle.AccessibleName = resources.GetString("txtCycle.AccessibleName");
            this.txtCycle.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("txtCycle.Anchor")));
            this.txtCycle.AutoSize = ((bool)(resources.GetObject("txtCycle.AutoSize")));
            this.txtCycle.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("txtCycle.BackgroundImage")));
            this.txtCycle.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("txtCycle.Dock")));
            this.txtCycle.Enabled = ((bool)(resources.GetObject("txtCycle.Enabled")));
            this.txtCycle.Font = ((System.Drawing.Font)(resources.GetObject("txtCycle.Font")));
            this.txtCycle.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("txtCycle.ImeMode")));
            this.txtCycle.Location = ((System.Drawing.Point)(resources.GetObject("txtCycle.Location")));
            this.txtCycle.MaxLength = ((int)(resources.GetObject("txtCycle.MaxLength")));
            this.txtCycle.Multiline = ((bool)(resources.GetObject("txtCycle.Multiline")));
            this.txtCycle.Name = "txtCycle";
            this.txtCycle.PasswordChar = ((char)(resources.GetObject("txtCycle.PasswordChar")));
            this.txtCycle.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("txtCycle.RightToLeft")));
            this.txtCycle.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("txtCycle.ScrollBars")));
            this.txtCycle.Size = ((System.Drawing.Size)(resources.GetObject("txtCycle.Size")));
            this.txtCycle.TabIndex = ((int)(resources.GetObject("txtCycle.TabIndex")));
            this.txtCycle.Text = resources.GetString("txtCycle.Text");
            this.txtCycle.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("txtCycle.TextAlign")));
            this.txtCycle.Visible = ((bool)(resources.GetObject("txtCycle.Visible")));
            this.txtCycle.WordWrap = ((bool)(resources.GetObject("txtCycle.WordWrap")));
            this.txtCycle.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCycle_KeyDown);
            this.txtCycle.Leave += new System.EventHandler(this.txtCycle_Leave);
            // 
            // lblCycle
            // 
            this.lblCycle.AccessibleDescription = resources.GetString("lblCycle.AccessibleDescription");
            this.lblCycle.AccessibleName = resources.GetString("lblCycle.AccessibleName");
            this.lblCycle.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblCycle.Anchor")));
            this.lblCycle.AutoSize = ((bool)(resources.GetObject("lblCycle.AutoSize")));
            this.lblCycle.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblCycle.Dock")));
            this.lblCycle.Enabled = ((bool)(resources.GetObject("lblCycle.Enabled")));
            this.lblCycle.Font = ((System.Drawing.Font)(resources.GetObject("lblCycle.Font")));
            this.lblCycle.ForeColor = System.Drawing.Color.Maroon;
            this.lblCycle.Image = ((System.Drawing.Image)(resources.GetObject("lblCycle.Image")));
            this.lblCycle.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblCycle.ImageAlign")));
            this.lblCycle.ImageIndex = ((int)(resources.GetObject("lblCycle.ImageIndex")));
            this.lblCycle.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblCycle.ImeMode")));
            this.lblCycle.Location = ((System.Drawing.Point)(resources.GetObject("lblCycle.Location")));
            this.lblCycle.Name = "lblCycle";
            this.lblCycle.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblCycle.RightToLeft")));
            this.lblCycle.Size = ((System.Drawing.Size)(resources.GetObject("lblCycle.Size")));
            this.lblCycle.TabIndex = ((int)(resources.GetObject("lblCycle.TabIndex")));
            this.lblCycle.Text = resources.GetString("lblCycle.Text");
            this.lblCycle.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblCycle.TextAlign")));
            this.lblCycle.Visible = ((bool)(resources.GetObject("lblCycle.Visible")));
            // 
            // btnUpdateStock
            // 
            this.btnUpdateStock.AccessibleDescription = resources.GetString("btnUpdateStock.AccessibleDescription");
            this.btnUpdateStock.AccessibleName = resources.GetString("btnUpdateStock.AccessibleName");
            this.btnUpdateStock.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnUpdateStock.Anchor")));
            this.btnUpdateStock.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnUpdateStock.BackgroundImage")));
            this.btnUpdateStock.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnUpdateStock.Dock")));
            this.btnUpdateStock.Enabled = ((bool)(resources.GetObject("btnUpdateStock.Enabled")));
            this.btnUpdateStock.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnUpdateStock.FlatStyle")));
            this.btnUpdateStock.Font = ((System.Drawing.Font)(resources.GetObject("btnUpdateStock.Font")));
            this.btnUpdateStock.Image = ((System.Drawing.Image)(resources.GetObject("btnUpdateStock.Image")));
            this.btnUpdateStock.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnUpdateStock.ImageAlign")));
            this.btnUpdateStock.ImageIndex = ((int)(resources.GetObject("btnUpdateStock.ImageIndex")));
            this.btnUpdateStock.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnUpdateStock.ImeMode")));
            this.btnUpdateStock.Location = ((System.Drawing.Point)(resources.GetObject("btnUpdateStock.Location")));
            this.btnUpdateStock.Name = "btnUpdateStock";
            this.btnUpdateStock.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnUpdateStock.RightToLeft")));
            this.btnUpdateStock.Size = ((System.Drawing.Size)(resources.GetObject("btnUpdateStock.Size")));
            this.btnUpdateStock.TabIndex = ((int)(resources.GetObject("btnUpdateStock.TabIndex")));
            this.btnUpdateStock.Text = resources.GetString("btnUpdateStock.Text");
            this.btnUpdateStock.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnUpdateStock.TextAlign")));
            this.btnUpdateStock.Visible = ((bool)(resources.GetObject("btnUpdateStock.Visible")));
            this.btnUpdateStock.Click += new System.EventHandler(this.btnUpdateStock_Click);
            // 
            // btnProLine
            // 
            this.btnProLine.AccessibleDescription = resources.GetString("btnProLine.AccessibleDescription");
            this.btnProLine.AccessibleName = resources.GetString("btnProLine.AccessibleName");
            this.btnProLine.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnProLine.Anchor")));
            this.btnProLine.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnProLine.BackgroundImage")));
            this.btnProLine.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnProLine.Dock")));
            this.btnProLine.Enabled = ((bool)(resources.GetObject("btnProLine.Enabled")));
            this.btnProLine.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnProLine.FlatStyle")));
            this.btnProLine.Font = ((System.Drawing.Font)(resources.GetObject("btnProLine.Font")));
            this.btnProLine.Image = ((System.Drawing.Image)(resources.GetObject("btnProLine.Image")));
            this.btnProLine.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnProLine.ImageAlign")));
            this.btnProLine.ImageIndex = ((int)(resources.GetObject("btnProLine.ImageIndex")));
            this.btnProLine.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnProLine.ImeMode")));
            this.btnProLine.Location = ((System.Drawing.Point)(resources.GetObject("btnProLine.Location")));
            this.btnProLine.Name = "btnProLine";
            this.btnProLine.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnProLine.RightToLeft")));
            this.btnProLine.Size = ((System.Drawing.Size)(resources.GetObject("btnProLine.Size")));
            this.btnProLine.TabIndex = ((int)(resources.GetObject("btnProLine.TabIndex")));
            this.btnProLine.Text = resources.GetString("btnProLine.Text");
            this.btnProLine.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnProLine.TextAlign")));
            this.btnProLine.Visible = ((bool)(resources.GetObject("btnProLine.Visible")));
            this.btnProLine.Click += new System.EventHandler(this.btnProLine_Click);
            // 
            // txtProductionLine
            // 
            this.txtProductionLine.AccessibleDescription = resources.GetString("txtProductionLine.AccessibleDescription");
            this.txtProductionLine.AccessibleName = resources.GetString("txtProductionLine.AccessibleName");
            this.txtProductionLine.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("txtProductionLine.Anchor")));
            this.txtProductionLine.AutoSize = ((bool)(resources.GetObject("txtProductionLine.AutoSize")));
            this.txtProductionLine.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("txtProductionLine.BackgroundImage")));
            this.txtProductionLine.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("txtProductionLine.Dock")));
            this.txtProductionLine.Enabled = ((bool)(resources.GetObject("txtProductionLine.Enabled")));
            this.txtProductionLine.Font = ((System.Drawing.Font)(resources.GetObject("txtProductionLine.Font")));
            this.txtProductionLine.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("txtProductionLine.ImeMode")));
            this.txtProductionLine.Location = ((System.Drawing.Point)(resources.GetObject("txtProductionLine.Location")));
            this.txtProductionLine.MaxLength = ((int)(resources.GetObject("txtProductionLine.MaxLength")));
            this.txtProductionLine.Multiline = ((bool)(resources.GetObject("txtProductionLine.Multiline")));
            this.txtProductionLine.Name = "txtProductionLine";
            this.txtProductionLine.PasswordChar = ((char)(resources.GetObject("txtProductionLine.PasswordChar")));
            this.txtProductionLine.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("txtProductionLine.RightToLeft")));
            this.txtProductionLine.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("txtProductionLine.ScrollBars")));
            this.txtProductionLine.Size = ((System.Drawing.Size)(resources.GetObject("txtProductionLine.Size")));
            this.txtProductionLine.TabIndex = ((int)(resources.GetObject("txtProductionLine.TabIndex")));
            this.txtProductionLine.Text = resources.GetString("txtProductionLine.Text");
            this.txtProductionLine.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("txtProductionLine.TextAlign")));
            this.txtProductionLine.Visible = ((bool)(resources.GetObject("txtProductionLine.Visible")));
            this.txtProductionLine.WordWrap = ((bool)(resources.GetObject("txtProductionLine.WordWrap")));
            this.txtProductionLine.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtProductionLine_KeyDown);
            this.txtProductionLine.Validating += new System.ComponentModel.CancelEventHandler(this.txtProductionLine_Validating);
            // 
            // lblProLine
            // 
            this.lblProLine.AccessibleDescription = resources.GetString("lblProLine.AccessibleDescription");
            this.lblProLine.AccessibleName = resources.GetString("lblProLine.AccessibleName");
            this.lblProLine.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblProLine.Anchor")));
            this.lblProLine.AutoSize = ((bool)(resources.GetObject("lblProLine.AutoSize")));
            this.lblProLine.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblProLine.Dock")));
            this.lblProLine.Enabled = ((bool)(resources.GetObject("lblProLine.Enabled")));
            this.lblProLine.Font = ((System.Drawing.Font)(resources.GetObject("lblProLine.Font")));
            this.lblProLine.ForeColor = System.Drawing.Color.Black;
            this.lblProLine.Image = ((System.Drawing.Image)(resources.GetObject("lblProLine.Image")));
            this.lblProLine.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblProLine.ImageAlign")));
            this.lblProLine.ImageIndex = ((int)(resources.GetObject("lblProLine.ImageIndex")));
            this.lblProLine.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblProLine.ImeMode")));
            this.lblProLine.Location = ((System.Drawing.Point)(resources.GetObject("lblProLine.Location")));
            this.lblProLine.Name = "lblProLine";
            this.lblProLine.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblProLine.RightToLeft")));
            this.lblProLine.Size = ((System.Drawing.Size)(resources.GetObject("lblProLine.Size")));
            this.lblProLine.TabIndex = ((int)(resources.GetObject("lblProLine.TabIndex")));
            this.lblProLine.Text = resources.GetString("lblProLine.Text");
            this.lblProLine.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblProLine.TextAlign")));
            this.lblProLine.Visible = ((bool)(resources.GetObject("lblProLine.Visible")));
            // 
            // DCRegenerate
            // 
            this.AccessibleDescription = resources.GetString("$this.AccessibleDescription");
            this.AccessibleName = resources.GetString("$this.AccessibleName");
            this.AutoScaleBaseSize = ((System.Drawing.Size)(resources.GetObject("$this.AutoScaleBaseSize")));
            this.AutoScroll = ((bool)(resources.GetObject("$this.AutoScroll")));
            this.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("$this.AutoScrollMargin")));
            this.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("$this.AutoScrollMinSize")));
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.CancelButton = this.btnClose;
            this.ClientSize = ((System.Drawing.Size)(resources.GetObject("$this.ClientSize")));
            this.Controls.Add(this.btnProLine);
            this.Controls.Add(this.txtProductionLine);
            this.Controls.Add(this.txtCycle);
            this.Controls.Add(this.lblProLine);
            this.Controls.Add(this.btnCycle);
            this.Controls.Add(this.lblCycle);
            this.Controls.Add(this.pnControls);
            this.Controls.Add(this.cboCCN);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnHelp);
            this.Controls.Add(this.lblCCN);
            this.Controls.Add(this.btnPreviewCycle);
            this.Controls.Add(this.btnProcess);
            this.Controls.Add(this.btnUpdateStock);
            this.Enabled = ((bool)(resources.GetObject("$this.Enabled")));
            this.Font = ((System.Drawing.Font)(resources.GetObject("$this.Font")));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("$this.ImeMode")));
            this.KeyPreview = true;
            this.Location = ((System.Drawing.Point)(resources.GetObject("$this.Location")));
            this.MaximizeBox = false;
            this.MaximumSize = ((System.Drawing.Size)(resources.GetObject("$this.MaximumSize")));
            this.MinimumSize = ((System.Drawing.Size)(resources.GetObject("$this.MinimumSize")));
            this.Name = "DCRegenerate";
            this.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("$this.RightToLeft")));
            this.StartPosition = ((System.Windows.Forms.FormStartPosition)(resources.GetObject("$this.StartPosition")));
            this.Text = resources.GetString("$this.Text");
            this.Closing += new System.ComponentModel.CancelEventHandler(this.DCRegenerate_Closing);
            this.Load += new System.EventHandler(this.DCRegenerate_Load);
            ((System.ComponentModel.ISupportInitialize)(this.cboCCN)).EndInit();
            this.pnControls.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        #endregion

        private void DCRegenerate_Load(object sender, System.EventArgs e)
        {
            this.Name = THIS;
            //Set authorization for user
            Security secForm = new Security();
            if (secForm.SetRightForUserOnForm(this, SystemProperty.UserName) == 0)
            {
                this.Close();
                return;
            }
            LoadDataForCombo();
            if (m_blnAutoRun)
            {
                btnProcess_Click(null, null);
            }
        }

        private void LoadDataForCombo()
        {
            const string METHOD_NAME = THIS + ".LoadDataForCombo()";
            try
            {
                //Load data for CNN combo box
                UtilsBO boUtils = new UtilsBO();
                FormControlComponents.PutDataIntoC1ComboBox(cboCCN, boUtils.ListCCN().Tables[0], MST_CCNTable.CODE_FLD, MST_CCNTable.CCNID_FLD, MST_CCNTable.TABLE_NAME);
                //FormControlComponents.PutDataIntoC1ComboBox(cboCycle,(new DCRegenerateBO()).ListDCOption(),PRO_DCOptionMasterTable.CYCLE_FLD,PRO_DCOptionMasterTable.DCOPTIONMASTERID_FLD, PRO_DCOptionMasterTable.TABLE_NAME);
            }
            catch (PCSException ex)
            {
                // displays the error message.
                PCSMessageBox.Show(ex.mCode, MessageBoxIcon.Error);
                // log message.
                try
                {
                    Logger.LogMessage(ex.CauseException, METHOD_NAME, Level.ERROR);
                }
                catch
                {
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                // displays the error message.
                PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxIcon.Error);
                // log message.
                try
                {
                    Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
                }
                catch
                {
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
                }
            }
        }


        private void btnPreviewCycle_Click(object sender, System.EventArgs e)
        {
            const string METHOD_NAME = THIS + ".btnPreviewCycle_Click()";
            try
            {
                if (txtCycle.Text == string.Empty)
                {
                    PCSMessageBox.Show(ErrorCode.MESSAGE_DCP_SELECT_CYCLE);
                    txtCycle.Focus();
                    return;
                }
                else if (txtCycle.Tag != null)
                {
                    DCOptions dcOption = new DCOptions(int.Parse(txtCycle.Tag.ToString()));
                    dcOption.Show();
                }
            }
            catch (PCSException ex)
            {
                // Displays the error message if throwed from PCSException.
                PCSMessageBox.Show(ex.mCode, MessageBoxIcon.Error);
                try
                {
                    // Log error message into log file.
                    Logger.LogMessage(ex.CauseException, METHOD_NAME, Level.ERROR);
                }
                catch
                {
                    // Show message if logger has an error.
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                // Displays the error message if throwed from system.
                PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxIcon.Error);
                try
                {
                    // Log error message into log file.
                    Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
                }
                catch
                {
                    // Show message if logger has an error.
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
                }
            }
        }

        private void btnProcess_Click(object sender, System.EventArgs e)
        {
            const string METHOD_NAME = THIS + ".btnProcess_Click()";
            try
            {
                if (cboCCN.Text == string.Empty)
                {
                    PCSMessageBox.Show(ErrorCode.MESSAGE_RGA_CCN);
                    cboCCN.Focus();
                    return;
                }

                if (txtCycle.Text == string.Empty)
                {
                    // Input cycle 
                    PCSMessageBox.Show(ErrorCode.MESSAGE_DCP_SELECT_CYCLE);
                    txtCycle.Focus();
                    return;
                }

                if (txtCycle.Tag != null)
                {
                    intDCOptionMasterID = int.Parse(txtCycle.Tag.ToString());
                }

                DateTime dtmCurrentDate = (new PCSComUtils.Common.BO.UtilsBO()).GetDBDate().Date;
                DataRow drowOption = (new PCSComProduction.DCP.BO.DCOptionsBO()).GetDCOptionMaster(intDCOptionMasterID);
                DateTime dtmAsOfDate = Convert.ToDateTime(drowOption[MTR_MPSCycleOptionMasterTable.ASOFDATE_FLD]);
                if (dtmAsOfDate <= dtmCurrentDate)
                {
                    PCSMessageBox.Show(ErrorCode.MESSAGE_DCP_ASOFDATE_MUST_IN_FUTURE, MessageBoxIcon.Error);
                    txtCycle.Focus();
                    return;
                }


                // show the loading image
                picRunning.Visible = true;
                // disable the other controls
                pnControls.Visible = true;
                btnPreviewCycle.Enabled = btnProcess.Enabled = btnUpdateStock.Enabled = false;

                thrNewGen = new Thread(new ThreadStart(GenDCP));
                thrNewGen.Start();
                dtmServerDate = dtmCurrentDate;
                while (thrNewGen.ThreadState != ThreadState.Stopped || thrNewGen.IsAlive || thrNewGen.ThreadState == ThreadState.Running)
                {
                    pnControls.Visible = false;
                    btnPreviewCycle.Enabled = btnProcess.Enabled = btnUpdateStock.Enabled = true;
                }
                //DCRegenerateBO boDC = new DCRegenerateBO();
                // DateTime dtmFrom = DateTime.Now;
                //boDC.RunDCP(intCCNID, intDCOptionMasterID);

                // DateTime dtmTo = DateTime.Now;
                // MessageBox.Show("Runing time From: " + dtmFrom + " To: " + dtmTo);

                // PCSMessageBox.Show(ErrorCode.MESSAGE_DCP_PROCESS_SUCCESSFUL);

            }
            catch (PCSException ex)
            {
                // Displays the error message if throwed from PCSException.
                PCSMessageBox.Show(ex.mCode, MessageBoxIcon.Error);
                try
                {
                    // Log error message into log file.
                    Logger.LogMessage(ex.CauseException, METHOD_NAME, Level.ERROR);
                }
                catch
                {
                    // Show message if logger has an error.
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                // Displays the error message if throwed from system.
                PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxIcon.Error);
                try
                {
                    // Log error message into log file.
                    Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
                }
                catch
                {
                    // Show message if logger has an error.
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
                }
            }

        }

        /// <summary>
        ///  It's called by the Thread
        /// </summary>
        private void GenDCP()
        {
            const string METHOD_NAME = THIS + ".GenDCP()";
            try
            {
                DCRegenerateBO boDCRen = new DCRegenerateBO();
                if (cboCCN.SelectedValue != null)
                {
                    intCCNID = int.Parse(cboCCN.SelectedValue.ToString());
                }
                if (txtCycle.Tag != null)
                {
                    intDCOptionMasterID = int.Parse(txtCycle.Tag.ToString());
                }

                DataTable dtbOverCapacityWC = new DataTable();
                //check for invalid workcenter
                DataTable dtbInvalidWC = boDCRen.GetNotConfiguredWC(intDCOptionMasterID);
                if (dtbInvalidWC.Rows.Count > 0)
                {
                    NotConfiguredWC frmNotConfiguredWC = new NotConfiguredWC(dtbInvalidWC);
                    frmNotConfiguredWC.ShowDialog(this);
                    //frmInvalidCPO.Show();
                }
                else
                {
                    PRO_DCOptionMasterVO voDCOptionMaster = (PRO_DCOptionMasterVO)(new DCOptionsBO()).GetMasterVO(intDCOptionMasterID);
                    DateTime dtmStart = DateTime.Now;
                    int intResult = boDCRen.RunDCP(intCCNID, intDCOptionMasterID, dtbOverCapacityWC);
                    if (intResult != 0)
                    {
                        MessageBox.Show("A productionline start planning date is on the past", "Production Control System", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    DateTime dtmEnd = DateTime.Now;
                    double dblTotalMinutes = dtmEnd.Subtract(dtmStart).TotalMinutes;

                    //pnControls.Visible = false;
                    // TODO: Change message
                    string[] strParams = new string[1];
                    strParams[0] = DETAIL_CAPACITY_PLANNING; // TODO: Will add constant in next version
                    MessageBox.Show("DCP completed in : " + /*((int)Math.Round(dblTotalMinutes)).ToString()*/ dblTotalMinutes.ToString("#,##0.00") + " minute(s)", "Production Control System", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //PCSMessageBox.Show(ErrorCode.MESSAGE_GENERATED_SUCCESSFULLY,MessageBoxIcon.Information,strParams);
                    //btnPreviewCycle.Enabled = btnProcess.Enabled = btnUpdateStock.Enabled = true;
                    if (thrNewGen != null)
                    {
                        thrNewGen = null;
                    }
                    if ((dtbOverCapacityWC.Rows.Count > 0) && (voDCOptionMaster.ScheduleType != (int)ScheduleType.LoadAveraging))
                    {
                        OutOfCapacityWC frmOutOfCapacity = new OutOfCapacityWC();
                        frmOutOfCapacity.OverCapacityWC = dtbOverCapacityWC;
                        frmOutOfCapacity.CycleOptionID = intDCOptionMasterID;
                        frmOutOfCapacity.ShowDialog(this);
                        //frmOutOfCapacity.Show();
                    }
                }
            }
            catch (PCSException ex)
            {
                string[] strParams;
                try
                {
                    if (ex.mCode == ErrorCode.MESSAGE_DCP_CONFIG_CALENDAR_FROM_X_TO_Y)
                    {
                        // TODO: Change message
                        strParams = (new DCRegenerateBO()).GetFromYearToYear(intDCOptionMasterID);
                        PCSMessageBox.Show(ErrorCode.MESSAGE_DCP_CONFIG_CALENDAR_FROM_X_TO_Y, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, strParams);
                    }
                    else if (ex.mCode == ErrorCode.MESSAGE_DCP_CONFIG_WORKCENTER)
                    {
                        // strParams = (new DCRegenerateBO()).GetWorkCenterNotConfig(int.Parse(cboCycle.SelectedValue.ToString()));
                        strParams = new string[1];
                        strParams[0] = ex.Hash[0].ToString();
                        // TODO: Change message

                        PCSMessageBox.Show(ErrorCode.MESSAGE_DCP_CONFIG_WORKCENTER, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, strParams);
                        //						string strMessage;
                        //						PCSMessageBox.Show(strMessage, MessageBoxButtons.OK,)
                        //pnControls.Visible = false;

                    }
                    else if (ex.mCode == ErrorCode.MESSAGE_DCP_SETTING_WORKING_CALENDAR)
                    {
                        strParams = new string[1];
                        strParams[0] = (new DCRegenerateBO()).GetLackOfYearInCalendar(intDCOptionMasterID).ToString();
                        PCSMessageBox.Show(ErrorCode.MESSAGE_DCP_SETTING_WORKING_CALENDAR, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, strParams);
                        pnControls.Visible = false;

                    }
                    else if (ex.mCode == ErrorCode.MESSAGE_ERROR_WORKCENTER)
                    {
                        // TODO: Change message
                        strParams = new string[] { ex.mMethod };
                        PCSMessageBox.Show(ex.mCode, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, strParams);
                    }
                }
                catch
                {
                    try
                    {
                        // Log error message into log file.
                        Logger.LogMessage(ex.CauseException, METHOD_NAME, Level.ERROR);
                    }
                    catch
                    {
                        // Show message if logger has an error.
                        PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
                    }
                }
            }
            catch (ThreadAbortException ex)
            {
                // Do nothing
                ex.ToString();
            }
            catch (Exception ex)
            {
                //do nothing
                PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxIcon.Error);
                try
                {
                    Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
                }
                catch
                {
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
                }
            }
            //pnControls.Visible = false;
        }

        private void btnHelp_Click(object sender, System.EventArgs e)
        {

        }

        private void btnClose_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

        private void btnCycle_Click(object sender, System.EventArgs e)
        {
            const string METHOD_NAME = THIS + ".btnCycle_Click()";
            try
            {
                DataRowView drwResult = null;
                // Hashtable htbCondition = new Hashtable();
                if (cboCCN.SelectedValue != null)
                {
                    intCCNID = int.Parse(cboCCN.SelectedValue.ToString());
                }
                else
                {
                    PCSMessageBox.Show(ErrorCode.MESSAGE_RGA_CCN, MessageBoxIcon.Warning);
                    cboCCN.Focus();
                    return;
                }
                Hashtable hshCondition = new Hashtable();
                hshCondition.Add(MST_CCNTable.CCNID_FLD, intCCNID);
                drwResult = FormControlComponents.OpenSearchForm(PRO_DCOptionMasterTable.TABLE_NAME, PRO_DCOptionMasterTable.CYCLE_FLD, txtCycle.Text, hshCondition);
                if (drwResult != null)
                {
                    txtCycle.Text = drwResult[PRO_DCOptionMasterTable.CYCLE_FLD].ToString();
                    txtCycle.Tag = drwResult[PRO_DCOptionMasterTable.DCOPTIONMASTERID_FLD].ToString();
                    // voMPSCycleOptionMaster = (MTR_MPSCycleOptionMasterVO)boCycleOption.GetCycleOptionMaster(int.Parse(drwResult[MTR_MPSCycleOptionMasterTable.MPSCYCLEOPTIONMASTERID_FLD].ToString()));
                    btnPreviewCycle.Enabled = true;
                }
                else
                {
                    txtCycle.Focus();
                    txtCycle.SelectAll();
                }
            }
            catch (PCSException ex)
            {
                // Displays the error message if throwed from PCSException.
                PCSMessageBox.Show(ex.mCode, MessageBoxIcon.Error);
                try
                {
                    // Log error message into log file.
                    Logger.LogMessage(ex.CauseException, METHOD_NAME, Level.ERROR);
                }
                catch
                {
                    // Show message if logger has an error.
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                // Displays the error message if throwed from system.
                PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxIcon.Error);
                try
                {
                    // Log error message into log file.
                    Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
                }
                catch
                {
                    // Show message if logger has an error.
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
                }
            }
        }

        private void txtCycle_Leave(object sender, System.EventArgs e)
        {
            const string METHOD_NAME = THIS + ".txtCycle_Leave()";
            try
            {
                if (!txtCycle.Modified || txtCycle.Text.Trim() == string.Empty)
                {
                    return;
                }
                Hashtable htbCriterial = new Hashtable();
                if (cboCCN.SelectedValue == null)
                {
                    PCSMessageBox.Show(ErrorCode.MESSAGE_RGA_CCN, MessageBoxIcon.Warning);
                    cboCCN.Focus();
                    return;
                }
                else
                {
                    htbCriterial.Add(PRO_DCOptionMasterTable.CCNID_FLD, cboCCN.SelectedValue);
                }
                DataRowView drvResult = FormControlComponents.OpenSearchForm(PRO_DCOptionMasterTable.TABLE_NAME, PRO_DCOptionMasterTable.CYCLE_FLD, txtCycle.Text.Trim(), htbCriterial, false);
                if (drvResult != null)
                {
                    txtCycle.Text = drvResult[PRO_DCOptionMasterTable.CYCLE_FLD].ToString();
                    txtCycle.Tag = drvResult[PRO_DCOptionMasterTable.DCOPTIONMASTERID_FLD].ToString();
                }
            }
            catch (PCSException ex)
            {
                // Displays the error message if throwed from PCSException.
                PCSMessageBox.Show(ex.mCode, MessageBoxIcon.Error);
                try
                {
                    // Log error message into log file.
                    Logger.LogMessage(ex.CauseException, METHOD_NAME, Level.ERROR);
                }
                catch
                {
                    // Show message if logger has an error.
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                // Displays the error message if throwed from system.
                PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxIcon.Error);
                try
                {
                    // Log error message into log file.
                    Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
                }
                catch
                {
                    // Show message if logger has an error.
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
                }
            }
        }

        private void txtCycle_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.F4) && (btnCycle.Enabled))
            {
                btnCycle_Click(sender, e);
            }
        }

        private void DCRegenerate_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            const string METHOD_NAME = THIS + ".DCRegenerate_Closing()";
            try
            {
                // ask user to stop the thread
                if (thrNewGen != null)
                {
                    if (thrNewGen.IsAlive || thrNewGen.ThreadState == ThreadState.Running)
                    {
                        string[] strMsg = { DETAIL_CAPACITY_PLANNING };
                        DialogResult dlgResult = PCSMessageBox.Show(ErrorCode.MESSAGE_PROCESS_IS_RUNNING, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2, strMsg);
                        //DialogResult dlgResult = MessageBox.Show("Running. Close it?", "Close", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                        switch (dlgResult)
                        {
                            case System.Windows.Forms.DialogResult.Yes:
                                // try to stop the thread
                                try
                                {
                                    thrNewGen.Abort();
                                }
                                catch (ThreadAbortException ex)
                                {
                                    Logger.LogMessage(ex.Message, METHOD_NAME, Level.DEBUG);
                                    e.Cancel = false;
                                }
                                break;
                            case System.Windows.Forms.DialogResult.No:
                                e.Cancel = true;
                                break;
                            case System.Windows.Forms.DialogResult.Cancel:
                                e.Cancel = true;
                                break;
                        }
                    }
                }
            }
            catch (ThreadAbortException ex)
            {
                Logger.LogMessage(ex.Message, METHOD_NAME, Level.DEBUG);
            }
            catch (PCSException ex)
            {
                // displays the error message.
                PCSMessageBox.Show(ex.mCode, MessageBoxButtons.OK, MessageBoxIcon.Error);
                // log message.
                try
                {
                    Logger.LogMessage(ex.CauseException, METHOD_NAME, Level.ERROR);
                }
                catch
                {
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                // displays the error message.
                PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
                // log message.
                try
                {
                    Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
                }
                catch
                {
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnUpdateStock_Click(object sender, System.EventArgs e)
        {
            const string METHOD_NAME = THIS + ".btnUpdateStock_Click()";
            Cursor = Cursors.WaitCursor;
            try
            {
                if (txtCycle.Text == string.Empty)
                {
                    PCSMessageBox.Show(ErrorCode.MESSAGE_DCP_SELECT_CYCLE);
                    txtCycle.Focus();
                    return;
                }
                else if (txtCycle.Tag != null)
                {
                    // show the loading image
                    picRunning.Visible = true;
                    // disable the other controls
                    pnControls.Visible = true;
                    btnPreviewCycle.Enabled = btnProcess.Enabled = btnUpdateStock.Enabled = false;

                    thrNewGen = new Thread(UpdateBeginStock);
                    thrNewGen.Start();
                    if (thrNewGen.ThreadState == ThreadState.Stopped || !thrNewGen.IsAlive)
                    {
                        thrNewGen.Abort();
                    }
                }
            }
            catch (PCSException ex)
            {
                // Displays the error message if throwed from PCSException.
                PCSMessageBox.Show(ex.mCode, MessageBoxIcon.Error);
                try
                {
                    // Log error message into log file.
                    Logger.LogMessage(ex.CauseException, METHOD_NAME, Level.ERROR);
                }
                catch
                {
                    // Show message if logger has an error.
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                // Displays the error message if throwed from system.
                PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxIcon.Error);
                try
                {
                    // Log error message into log file.
                    Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
                }
                catch
                {
                    // Show message if logger has an error.
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
                }
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void UpdateBeginStock()
        {
            const string METHOD_NAME = THIS + ".UpdateBeginStock()";
            try
            {
                // get cycle option object
                var boReport = new DCPReportBO();
                var voCycle = (PRO_DCOptionMasterVO)boReport.GetCyclerMasterObject(int.Parse(txtCycle.Tag.ToString()));
                dtmServerDate = Utilities.Instance.GetServerDate();
                ArrayList arrProLineID = null;
                try
                {
                    arrProLineID = (ArrayList)txtProductionLine.Tag;
                }
                catch { }
                DateTime dtmStart = DateTime.Now;
                UpdateBeginStock(voCycle, dtmServerDate, arrProLineID);
                DateTime dtmEnd = DateTime.Now;
                double dblTotalMinutes = dtmEnd.Subtract(dtmStart).TotalMinutes;
                PCSMessageBox.Show(string.Format("DCP Estimation completed in : {0} minute(s)", dblTotalMinutes.ToString("#,##0.00")));
                // update successfull
                PCSMessageBox.Show(ErrorCode.MESSAGE_UPDATE_BEGIN_FOR_REPORT_SUCCESS, MessageBoxIcon.Information);
                thrNewGen = null;
            }
            catch (ThreadAbortException) { }
            catch (Exception ex)
            {
                //do nothing
                PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxIcon.Error);
                try
                {
                    Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
                }
                catch
                {
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
                }
            }
            finally
            {
                SetControlPropertyValue(pnControls, "Visible", false);
                SetControlPropertyValue(btnPreviewCycle, "Enabled", true);
                SetControlPropertyValue(btnProcess, "Enabled", true);
                SetControlPropertyValue(btnUpdateStock, "Enabled", true);
            }
        }

        private void UpdateBeginStock(PRO_DCOptionMasterVO voCycle, DateTime dtmServerDate, ArrayList parrProLineID)
        {
            var boReport = new DCPReportBO();
            DateTime effectDate = voCycle.PlanningPeriod.Date;

            var beginStockPreMonth = boReport.GetBeginStock(voCycle.PlanningPeriod.Date.AddMonths(-1));
            var beginStock = new List<IV_BeginDCPReport>();
            // get planning offset
            var planningOffset = boReport.GetPlanningOffset(voCycle.DCOptionMasterID);
            // get list of production line
            List<PRO_ProductionLine> productionLines;
            var productionLineIds = new List<int>();
            if (parrProLineID == null)
            {
                productionLines = boReport.GetProductionLine();
                productionLineIds.AddRange(productionLines.Select(p => p.ProductionLineID));
            }
            else
            {
                productionLineIds = new List<object>(parrProLineID.ToArray()).ConvertAll(Convert.ToInt32);
            }

            // get list of all product
            var productList = boReport.ListProduct(productionLineIds);
            // delivery for parent
            var deliveryForParent = boReport.GetDeliveryForParent(voCycle.PlanningPeriod.Date.AddMonths(-1), voCycle.PlanningPeriod.Date.AddMonths(1));
            // valid work day
            var validWorkDays = boReport.GetWorkingDateFromWCCapacity(productionLineIds);
            // delivery for SO
            var deliveryForSO = boReport.GetDeliveryForSale(voCycle.PlanningPeriod.Date.AddMonths(-1), voCycle.PlanningPeriod.Date);
            // produce from dcp
            var produceDcp = boReport.GetProduce(voCycle.PlanningPeriod.Date.AddMonths(-1), voCycle.PlanningPeriod.Date.AddMonths(1));
            // working time
            var workingTime = boReport.ListWorkingTime();
            // calculate foreach product
            foreach (var product in productList)
            {
                // outside processing item
                if (!validWorkDays.Any(w => w.MST_WorkCenter.ProductionLineID == product.ProductionLineID))
                {
                    continue;
                }

                // refine cycle
                if (planningOffset != null)
                {
                    var startDate = Convert.ToDateTime(planningOffset.PlanningStartDate);
                    voCycle.AsOfDate = new DateTime(startDate.Year, startDate.Month, startDate.Day);
                }
                DateTime dtmFromDate = voCycle.PlanningPeriod.Date.AddMonths(-1);
                // get first valid work day of current month
                DateTime dtmFirstValidDay = GetFirstValidWorkDay(validWorkDays, dtmFromDate, voCycle.AsOfDate.AddDays(voCycle.PlanHorizon));
                var deliveryNextWc = new List<DeliveryScheduleData>();
                // refine the delivery date
                foreach (var delivery in deliveryForParent.Where(d => d.ProductId == product.ProductID))
                {
                    // using StartTime of parent instead of WorkingDate
                    DateTime dtmDate = delivery.StartTime;
                    // EndTime to check for over quantity from parent
                    DateTime dtmEndTime = delivery.EndTime;
                    // do nothing with over quantity from parent
                    if (dtmDate.Equals(dtmEndTime))
                    {
                        deliveryNextWc.Add(delivery);
                        continue;
                    }
                    var dtmTemp = new DateTime(dtmDate.Year, dtmDate.Month, dtmDate.Day);
                    if (dtmTemp <= dtmFirstValidDay && dtmTemp >= dtmFromDate)
                    {
                        dtmDate = new DateTime(dtmFirstValidDay.Year, dtmFirstValidDay.Month, dtmFirstValidDay.Day,
                            dtmDate.Hour, dtmDate.Minute, dtmDate.Second);
                        delivery.StartTime = dtmDate;
                        deliveryNextWc.Add(delivery);
                        continue;
                    }
                    decimal decLeadTimeOffset = 0;
                    try
                    {
                        decLeadTimeOffset = Convert.ToDecimal(delivery.LeadTime);
                    }
                    catch { }
                    decimal decNumOfDay = decLeadTimeOffset / 86400;
                    // convert to valid work day
                    dtmDate = ConvertWorkingDay(workingTime, validWorkDays, delivery.ScheduleDate, dtmDate, decNumOfDay);
                    delivery.StartTime = dtmDate;
                    deliveryNextWc.Add(delivery);
                }
                decimal decScrapPercent = Convert.ToDecimal(product.ScrapPercent);
                decimal decBeginQuantityLastMonth = 0;
                var beginLastMonth = beginStockPreMonth.FirstOrDefault(b => b.ProductID == product.ProductID);
                if (beginLastMonth != null)
                {
                    decBeginQuantityLastMonth = beginLastMonth.Quantity.GetValueOrDefault(0);
                }

                var beginRecord = new IV_BeginDCPReport
                {
                    ProductID = product.ProductID,
                    LastUpdate = dtmServerDate,
                    EffectDate = voCycle.PlanningPeriod.Date,
                    Username = SystemProperty.UserName
                };

                CalculateQuantity(voCycle.PlanningPeriod.Date, voCycle.PlanningPeriod.Date.AddMonths(-1), product.ProductID,
                    decBeginQuantityLastMonth, beginRecord, deliveryForSO,
                    deliveryNextWc, workingTime, produceDcp, decScrapPercent);
                beginStock.Add(beginRecord);
            }
            boReport.UpdateBeginStock(beginStock, effectDate, productList);
        }
        private static void CalculateQuantity(DateTime dtmToDate, DateTime dtmFromDate, int productId,
                                          decimal pdecBeginQuantityLastMonth, IV_BeginDCPReport beginRecord,
                                          IEnumerable<DeliveryScheduleData> deliveryForSO, IEnumerable<DeliveryScheduleData> deliveryForParent,
                                          List<PRO_ShiftPattern> workingTime, IEnumerable<DeliveryScheduleData> produceDCP, decimal pdecScrapPercent)
        {
            DateTime dtmToDateTime = dtmToDate;
            DateTime dtmEndTime = dtmToDate;
            GetStartAndEndTime(dtmToDate, ref dtmToDateTime, ref dtmEndTime, workingTime);
            DateTime dtmFromTime = dtmFromDate;
            GetStartAndEndTime(dtmFromDate, ref dtmFromTime, ref dtmEndTime, workingTime);

            #region Delivery for parent and produce

            decimal decDeliveryForParent = 0, decProduce = 0;

            try
            {
                decDeliveryForParent += deliveryForParent.Where(d => d.ProductId == productId
                    && d.StartTime >= dtmFromTime && d.StartTime < dtmToDateTime).Sum(d => d.Quantity);
            }
            catch { }

            #region produce

            try
            {
                decProduce += produceDCP.Where(p => p.ProductId == productId && p.EndTime >= dtmFromTime && p.EndTime < dtmToDateTime).Sum(p => p.Quantity);
            }
            catch { }

            #endregion

            #endregion

            #region Delivery for SO

            decimal decDeliveryForSO = 0;
            try
            {
                decDeliveryForSO += deliveryForSO.Where(d => d.ProductId == productId).Sum(d => d.Quantity);
            }
            catch { }

            #endregion

            // quantity = cache + produce - delivery (so + parent)
            decimal decQuantity = pdecBeginQuantityLastMonth + decProduce - (decDeliveryForParent + decDeliveryForSO);

            decQuantity = (decQuantity < 0) ? 0 : decQuantity;
            beginRecord.Quantity = decQuantity * (1 - pdecScrapPercent / 100);
        }
        private static void GetStartAndEndTime(DateTime pdtmCurrentDay, ref DateTime pdtmStartTime, ref DateTime pdtmEndTime, List<PRO_ShiftPattern> workingTime)
        {
            if (workingTime.Count <= 0)
            {
                return;
            }
            
            var workTimeOrdered = workingTime.OrderBy(w => w.WorkTimeFrom);
            var workTimeFrom = Convert.ToDateTime(workTimeOrdered.FirstOrDefault().WorkTimeFrom);
            var workTimeTo = Convert.ToDateTime(workTimeOrdered.LastOrDefault().WorkTimeTo);
            
            //change shift configured day to working day
            pdtmStartTime = new DateTime(pdtmCurrentDay.Year, pdtmCurrentDay.Month, pdtmCurrentDay.Day,
                workTimeFrom.Hour, workTimeFrom.Minute, workTimeFrom.Second);
            pdtmEndTime = new DateTime(pdtmCurrentDay.Year, pdtmCurrentDay.Month, pdtmCurrentDay.Day,
                workTimeTo.Hour, workTimeTo.Minute, workTimeTo.Second);
            double dblDiff = workTimeTo.Subtract(workTimeFrom).Days;
            pdtmEndTime = pdtmEndTime.AddDays(dblDiff);
        }
        /// <summary>
        /// Gets the first valid work day.
        /// </summary>
        /// <param name="validDays">The valid days.</param>
        /// <param name="fromDate">From date.</param>
        /// <param name="toDate">To date.</param>
        /// <returns></returns>
        private static DateTime GetFirstValidWorkDay(List<PRO_WCCapacity> validDays, DateTime fromDate, DateTime toDate)
        {
            var validDay = fromDate;
            for (DateTime date = fromDate; date <= toDate; date = date.AddDays(1))
            {
                if (validDays.Any(d => d.BeginDate.CompareTo(date) <= 0 && d.EndDate.CompareTo(date) >= 0))
                {
                    validDay = date;
                    break;
                }
            }
            return validDay;
        }
        private static DateTime ConvertWorkingDay(List<PRO_ShiftPattern> workingTime, List<PRO_WCCapacity> validDays, DateTime workingDay, DateTime date, decimal numberOfDay)
        {
            DateTime dtmConvert = date;

            dtmConvert = dtmConvert.AddDays(-(double)numberOfDay);
            bool isOk = false;
            while (!isOk)
            {
                DateTime dtmOld = dtmConvert;
                DateTime dtmConverted = new DateTime(workingDay.Year, workingDay.Month, workingDay.Day);
                var day = validDays.FirstOrDefault(d => d.BeginDate.CompareTo(dtmConverted) <= 0 && d.EndDate.CompareTo(dtmConverted) >= 0);
                if (day == null)
                {
                    day = validDays.FirstOrDefault(d => d.BeginDate.CompareTo(dtmConverted) <= 0);
                    if (day == null)
                    {
                        if (validDays.FirstOrDefault() != null)
                        {
                            dtmConvert = validDays.FirstOrDefault().BeginDate;
                        }
                        break;
                    }

                    dtmConvert = dtmConvert.AddDays(-1);
                    workingDay = GetRealWorkingDay(dtmConvert, workingTime);
                }

                if (dtmOld == dtmConvert)
                {
                    isOk = true;
                }
            }

            return dtmConvert;
        }
        private static DateTime GetRealWorkingDay(DateTime pdtmNeedToResolve, List<PRO_ShiftPattern> pdtbWorkingTime)
        {
            var drowShifts = pdtbWorkingTime.OrderBy(w => w.WorkTimeFrom);

            if (drowShifts.Count() <= 0)
            {
                return DateTime.MinValue;
            }

            var firstShift = drowShifts.FirstOrDefault();
            DateTime dtmResolvedDate = pdtmNeedToResolve;
            DateTime workTimeFrom = firstShift.WorkTimeFrom.GetValueOrDefault(DateTime.Now);
            //change shift configured day to working day
            DateTime dtmStartTime = new DateTime(pdtmNeedToResolve.Year, pdtmNeedToResolve.Month, pdtmNeedToResolve.Day,
                workTimeFrom.Hour, workTimeFrom.Minute, workTimeFrom.Second);

            while (dtmResolvedDate < dtmStartTime)
            {
                dtmStartTime = dtmStartTime.AddDays(-1);
            }

            return dtmStartTime;
        }

        private void txtProductionLine_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            const string METHOD_NAME = THIS + ".txtProductionLine_Validating()";
            try
            {
                if (!txtProductionLine.Modified) return;
                if (txtProductionLine.Text.Trim() == string.Empty)
                {
                    txtProductionLine.Tag = string.Empty;
                    return;
                }
                DataTable dtbData = FormControlComponents.OpenSearchFormForMultiSelectedRow(PRO_ProductionLineTable.TABLE_NAME, PRO_ProductionLineTable.CODE_FLD, txtProductionLine.Text, string.Empty, false);
                if (dtbData != null && dtbData.Rows.Count > 0)
                {
                    ArrayList arrProLineID = new ArrayList();
                    foreach (DataRow drowData in dtbData.Rows)
                        arrProLineID.Add(drowData[PRO_ProductionLineTable.PRODUCTIONLINEID_FLD].ToString());
                    txtProductionLine.Text = (dtbData.Rows.Count > 1) ? "Multi Selection" : dtbData.Rows[0][PRO_ProductionLineTable.CODE_FLD].ToString();
                    txtProductionLine.Tag = arrProLineID;
                }
                else
                    e.Cancel = true;
            }
            catch (PCSException ex)
            {
                // displays the error message.
                PCSMessageBox.Show(ex.mCode, MessageBoxButtons.OK, MessageBoxIcon.Error);
                // log message.
                try
                {
                    Logger.LogMessage(ex.CauseException, METHOD_NAME, Level.ERROR);
                }
                catch
                {
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                // displays the error message.
                PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
                // log message.
                try
                {
                    Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
                }
                catch
                {
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void txtProductionLine_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            const string METHOD_NAME = THIS + ".btnProLine_Click()";
            try
            {
                if (e.KeyCode == Keys.F4)
                    btnProLine_Click(null, null);
            }
            catch (PCSException ex)
            {
                // displays the error message.
                PCSMessageBox.Show(ex.mCode, MessageBoxButtons.OK, MessageBoxIcon.Error);
                // log message.
                try
                {
                    Logger.LogMessage(ex.CauseException, METHOD_NAME, Level.ERROR);
                }
                catch
                {
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                // displays the error message.
                PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
                // log message.
                try
                {
                    Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
                }
                catch
                {
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnProLine_Click(object sender, System.EventArgs e)
        {
            const string METHOD_NAME = THIS + ".btnProLine_Click()";
            try
            {
                DataTable dtbData = FormControlComponents.OpenSearchFormForMultiSelectedRow(PRO_ProductionLineTable.TABLE_NAME, PRO_ProductionLineTable.CODE_FLD, txtProductionLine.Text, string.Empty, true);
                if (dtbData != null && dtbData.Rows.Count > 0)
                {
                    ArrayList arrProLineID = new ArrayList();
                    foreach (DataRow drowData in dtbData.Rows)
                        arrProLineID.Add(drowData[PRO_ProductionLineTable.PRODUCTIONLINEID_FLD].ToString());
                    txtProductionLine.Text = (dtbData.Rows.Count > 1) ? "Multi Selection" : dtbData.Rows[0][PRO_ProductionLineTable.CODE_FLD].ToString();
                    txtProductionLine.Tag = arrProLineID;
                }
                else
                    txtProductionLine.Focus();
            }
            catch (PCSException ex)
            {
                // displays the error message.
                PCSMessageBox.Show(ex.mCode, MessageBoxButtons.OK, MessageBoxIcon.Error);
                // log message.
                try
                {
                    Logger.LogMessage(ex.CauseException, METHOD_NAME, Level.ERROR);
                }
                catch
                {
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                // displays the error message.
                PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
                // log message.
                try
                {
                    Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
                }
                catch
                {
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
