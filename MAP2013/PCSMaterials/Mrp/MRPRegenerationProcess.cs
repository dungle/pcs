using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using C1.Win.C1List;
using PCSComMaterials.Plan.BO;
using PCSComMaterials.Plan.DS;
using PCSComUtils.Common;
using PCSComUtils.Common.BO;
using PCSComUtils.PCSExc;
using PCSUtils.Log;
using PCSUtils.Utils;

namespace PCSMaterials.Mrp
{
    /// <summary>
    /// Summary description for MRPRegenerationProcess.
    /// </summary>
    public class MRPRegenerationProcess : Form
    {
        private Button btnClose;
        private Button btnHelp;
        private Label lblCycle;
        private Label lblCCN;
        private Button btnPreviewCycle;
        private Button btnProcess;
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private Container components = null;
        private C1Combo cboCCN;
        private TextBox txtCycle;
        private Button btnCycle;
        private PictureBox ptbImage;
        private DateTime dtmAsOfDate;
        private int intPlanHorizon = 0;
        private int intMRPCycleOptionMasterID = 0;
        private Thread thrNewGen;
        private Label lblVendor;
        private Label lblCategory;
        private Label lblModel;
        private Label lblPartNumber;
        private Label lblPartName;
        private TextBox txtVendor;
        private Button btnVendor;
        private TextBox txtCategory;
        private Button btnCategory;
        private TextBox txtModel;
        private Button btnModel;
        private TextBox txtPartNumber;
        private Button btnPartNumber;
        private TextBox txtPartName;
        private Button btnPartName;
        private const string THIS = "PCSMaterials.Mrp.MRPRegenerationProcess";
        private const string MODEL_VIEW = "v_ModelList";
        public MRPRegenerationProcess()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
        }

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

        public MRPRegenerationProcess(int pintMRPCycleOptionMasterID)
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
            intMRPCycleOptionMasterID = pintMRPCycleOptionMasterID;
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
            System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(MRPRegenerationProcess));
            this.btnClose = new System.Windows.Forms.Button();
            this.btnHelp = new System.Windows.Forms.Button();
            this.lblCycle = new System.Windows.Forms.Label();
            this.lblCCN = new System.Windows.Forms.Label();
            this.btnPreviewCycle = new System.Windows.Forms.Button();
            this.btnProcess = new System.Windows.Forms.Button();
            this.cboCCN = new C1.Win.C1List.C1Combo();
            this.txtCycle = new System.Windows.Forms.TextBox();
            this.btnCycle = new System.Windows.Forms.Button();
            this.ptbImage = new System.Windows.Forms.PictureBox();
            this.lblVendor = new System.Windows.Forms.Label();
            this.lblCategory = new System.Windows.Forms.Label();
            this.lblModel = new System.Windows.Forms.Label();
            this.lblPartNumber = new System.Windows.Forms.Label();
            this.lblPartName = new System.Windows.Forms.Label();
            this.txtVendor = new System.Windows.Forms.TextBox();
            this.btnVendor = new System.Windows.Forms.Button();
            this.txtCategory = new System.Windows.Forms.TextBox();
            this.btnCategory = new System.Windows.Forms.Button();
            this.txtModel = new System.Windows.Forms.TextBox();
            this.btnModel = new System.Windows.Forms.Button();
            this.txtPartNumber = new System.Windows.Forms.TextBox();
            this.btnPartNumber = new System.Windows.Forms.Button();
            this.txtPartName = new System.Windows.Forms.TextBox();
            this.btnPartName = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.cboCCN)).BeginInit();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.AccessibleDescription = "";
            this.btnClose.AccessibleName = "";
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnClose.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnClose.Location = new System.Drawing.Point(220, 177);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(60, 23);
            this.btnClose.TabIndex = 8;
            this.btnClose.Text = "&Close";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnHelp
            // 
            this.btnHelp.AccessibleDescription = "";
            this.btnHelp.AccessibleName = "";
            this.btnHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnHelp.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnHelp.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnHelp.Location = new System.Drawing.Point(159, 177);
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Size = new System.Drawing.Size(60, 23);
            this.btnHelp.TabIndex = 7;
            this.btnHelp.Text = "&Help";
            // 
            // lblCycle
            // 
            this.lblCycle.AccessibleDescription = "";
            this.lblCycle.AccessibleName = "";
            this.lblCycle.ForeColor = System.Drawing.Color.Maroon;
            this.lblCycle.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblCycle.Location = new System.Drawing.Point(9, 33);
            this.lblCycle.Name = "lblCycle";
            this.lblCycle.Size = new System.Drawing.Size(76, 21);
            this.lblCycle.TabIndex = 2;
            this.lblCycle.Text = "Cycle";
            this.lblCycle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblCCN
            // 
            this.lblCCN.AccessibleDescription = "";
            this.lblCCN.AccessibleName = "";
            this.lblCCN.ForeColor = System.Drawing.Color.Maroon;
            this.lblCCN.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblCCN.Location = new System.Drawing.Point(9, 10);
            this.lblCCN.Name = "lblCCN";
            this.lblCCN.Size = new System.Drawing.Size(76, 21);
            this.lblCCN.TabIndex = 0;
            this.lblCCN.Text = "CCN";
            this.lblCCN.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnPreviewCycle
            // 
            this.btnPreviewCycle.AccessibleDescription = "";
            this.btnPreviewCycle.AccessibleName = "";
            this.btnPreviewCycle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnPreviewCycle.Enabled = false;
            this.btnPreviewCycle.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnPreviewCycle.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnPreviewCycle.Location = new System.Drawing.Point(9, 177);
            this.btnPreviewCycle.Name = "btnPreviewCycle";
            this.btnPreviewCycle.Size = new System.Drawing.Size(88, 23);
            this.btnPreviewCycle.TabIndex = 5;
            this.btnPreviewCycle.Text = "P&review Cycle";
            this.btnPreviewCycle.Click += new System.EventHandler(this.btnPreviewCycle_Click);
            // 
            // btnProcess
            // 
            this.btnProcess.AccessibleDescription = "";
            this.btnProcess.AccessibleName = "";
            this.btnProcess.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnProcess.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnProcess.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnProcess.Location = new System.Drawing.Point(98, 177);
            this.btnProcess.Name = "btnProcess";
            this.btnProcess.Size = new System.Drawing.Size(60, 23);
            this.btnProcess.TabIndex = 6;
            this.btnProcess.Text = "&Process";
            this.btnProcess.Click += new System.EventHandler(this.btnProcess_Click);
            // 
            // cboCCN
            // 
            this.cboCCN.AccessibleDescription = "";
            this.cboCCN.AccessibleName = "";
            this.cboCCN.AddItemSeparator = ';';
            this.cboCCN.Caption = "";
            this.cboCCN.CaptionHeight = 17;
            this.cboCCN.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.cboCCN.ColumnCaptionHeight = 17;
            this.cboCCN.ColumnFooterHeight = 17;
            this.cboCCN.ComboStyle = C1.Win.C1List.ComboStyleEnum.DropdownList;
            this.cboCCN.ContentHeight = 15;
            this.cboCCN.DeadAreaBackColor = System.Drawing.Color.Empty;
            this.cboCCN.EditorBackColor = System.Drawing.SystemColors.Window;
            this.cboCCN.EditorFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
            this.cboCCN.EditorForeColor = System.Drawing.SystemColors.WindowText;
            this.cboCCN.EditorHeight = 15;
            this.cboCCN.FlatStyle = C1.Win.C1List.FlatModeEnum.System;
            this.cboCCN.GapHeight = 2;
            this.cboCCN.ItemHeight = 15;
            this.cboCCN.Location = new System.Drawing.Point(87, 10);
            this.cboCCN.MatchEntryTimeout = ((long)(2000));
            this.cboCCN.MaxDropDownItems = ((short)(5));
            this.cboCCN.MaxLength = 32767;
            this.cboCCN.MouseCursor = System.Windows.Forms.Cursors.Default;
            this.cboCCN.Name = "cboCCN";
            this.cboCCN.RowDivider.Color = System.Drawing.Color.DarkGray;
            this.cboCCN.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None;
            this.cboCCN.RowSubDividerColor = System.Drawing.Color.DarkGray;
            this.cboCCN.Size = new System.Drawing.Size(96, 21);
            this.cboCCN.TabIndex = 1;
            this.cboCCN.Enter += new System.EventHandler(this.OnEnterControl);
            this.cboCCN.Leave += new System.EventHandler(this.OnLeaveControl);
            this.cboCCN.PropBag = "<?xml version=\"1.0\"?><Blob><Styles type=\"C1.Win.C1List.Design.ContextWrapper\"><Da" +
                "ta>Group{BackColor:ControlDark;Border:None,,0, 0, 0, 0;AlignVert:Center;}Style2{" +
                "}Style5{}Style4{}Style7{}Style6{}EvenRow{BackColor:Aqua;}Selected{ForeColor:High" +
                "lightText;BackColor:Highlight;}Style3{}Inactive{ForeColor:InactiveCaptionText;Ba" +
                "ckColor:InactiveCaption;}Footer{}Caption{AlignHorz:Center;}Normal{BackColor:Wind" +
                "ow;}HighlightRow{ForeColor:HighlightText;BackColor:Highlight;}Style9{AlignHorz:N" +
                "ear;}OddRow{}RecordSelector{AlignImage:Center;}Heading{Wrap:True;AlignVert:Cente" +
                "r;Border:Raised,,1, 1, 1, 1;ForeColor:ControlText;BackColor:Control;}Style8{}Sty" +
                "le10{}Style11{}Style1{}</Data></Styles><Splits><C1.Win.C1List.ListBoxView AllowC" +
                "olSelect=\"False\" Name=\"\" CaptionHeight=\"17\" ColumnCaptionHeight=\"17\" ColumnFoote" +
                "rHeight=\"17\" VerticalScrollGroup=\"1\" HorizontalScrollGroup=\"1\"><ClientRect>0, 0," +
                " 118, 158</ClientRect><VScrollBar><Width>16</Width></VScrollBar><HScrollBar><Hei" +
                "ght>16</Height></HScrollBar><CaptionStyle parent=\"Style2\" me=\"Style9\" /><EvenRow" +
                "Style parent=\"EvenRow\" me=\"Style7\" /><FooterStyle parent=\"Footer\" me=\"Style3\" />" +
                "<GroupStyle parent=\"Group\" me=\"Style11\" /><HeadingStyle parent=\"Heading\" me=\"Sty" +
                "le2\" /><HighLightRowStyle parent=\"HighlightRow\" me=\"Style6\" /><InactiveStyle par" +
                "ent=\"Inactive\" me=\"Style4\" /><OddRowStyle parent=\"OddRow\" me=\"Style8\" /><RecordS" +
                "electorStyle parent=\"RecordSelector\" me=\"Style10\" /><SelectedStyle parent=\"Selec" +
                "ted\" me=\"Style5\" /><Style parent=\"Normal\" me=\"Style1\" /></C1.Win.C1List.ListBoxV" +
                "iew></Splits><NamedStyles><Style parent=\"\" me=\"Normal\" /><Style parent=\"Normal\" " +
                "me=\"Heading\" /><Style parent=\"Heading\" me=\"Footer\" /><Style parent=\"Heading\" me=" +
                "\"Caption\" /><Style parent=\"Heading\" me=\"Inactive\" /><Style parent=\"Normal\" me=\"S" +
                "elected\" /><Style parent=\"Normal\" me=\"HighlightRow\" /><Style parent=\"Normal\" me=" +
                "\"EvenRow\" /><Style parent=\"Normal\" me=\"OddRow\" /><Style parent=\"Heading\" me=\"Rec" +
                "ordSelector\" /><Style parent=\"Caption\" me=\"Group\" /></NamedStyles><vertSplits>1<" +
                "/vertSplits><horzSplits>1</horzSplits><Layout>Modified</Layout><DefaultRecSelWid" +
                "th>17</DefaultRecSelWidth></Blob>";
            // 
            // txtCycle
            // 
            this.txtCycle.Location = new System.Drawing.Point(87, 34);
            this.txtCycle.Name = "txtCycle";
            this.txtCycle.Size = new System.Drawing.Size(96, 20);
            this.txtCycle.TabIndex = 3;
            this.txtCycle.Text = "";
            this.txtCycle.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCycle_KeyDown);
            this.txtCycle.Validating += new System.ComponentModel.CancelEventHandler(this.txtCycle_Validating);
            // 
            // btnCycle
            // 
            this.btnCycle.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnCycle.Location = new System.Drawing.Point(185, 32);
            this.btnCycle.Name = "btnCycle";
            this.btnCycle.Size = new System.Drawing.Size(23, 22);
            this.btnCycle.TabIndex = 4;
            this.btnCycle.Text = "...";
            this.btnCycle.Click += new System.EventHandler(this.btnCycle_Click);
            // 
            // ptbImage
            // 
            this.ptbImage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ptbImage.Image = ((System.Drawing.Image)(resources.GetObject("ptbImage.Image")));
            this.ptbImage.Location = new System.Drawing.Point(211, 6);
            this.ptbImage.Name = "ptbImage";
            this.ptbImage.Size = new System.Drawing.Size(68, 68);
            this.ptbImage.TabIndex = 9;
            this.ptbImage.TabStop = false;
            this.ptbImage.Visible = false;
            // 
            // lblVendor
            // 
            this.lblVendor.AccessibleDescription = "";
            this.lblVendor.AccessibleName = "";
            this.lblVendor.ForeColor = System.Drawing.Color.Black;
            this.lblVendor.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblVendor.Location = new System.Drawing.Point(9, 56);
            this.lblVendor.Name = "lblVendor";
            this.lblVendor.Size = new System.Drawing.Size(76, 21);
            this.lblVendor.TabIndex = 10;
            this.lblVendor.Text = "Vendor";
            this.lblVendor.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblCategory
            // 
            this.lblCategory.AccessibleDescription = "";
            this.lblCategory.AccessibleName = "";
            this.lblCategory.ForeColor = System.Drawing.Color.Black;
            this.lblCategory.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblCategory.Location = new System.Drawing.Point(9, 79);
            this.lblCategory.Name = "lblCategory";
            this.lblCategory.Size = new System.Drawing.Size(76, 21);
            this.lblCategory.TabIndex = 10;
            this.lblCategory.Text = "Category";
            this.lblCategory.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblModel
            // 
            this.lblModel.AccessibleDescription = "";
            this.lblModel.AccessibleName = "";
            this.lblModel.ForeColor = System.Drawing.Color.Black;
            this.lblModel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblModel.Location = new System.Drawing.Point(9, 102);
            this.lblModel.Name = "lblModel";
            this.lblModel.Size = new System.Drawing.Size(76, 21);
            this.lblModel.TabIndex = 10;
            this.lblModel.Text = "Model";
            this.lblModel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblPartNumber
            // 
            this.lblPartNumber.AccessibleDescription = "";
            this.lblPartNumber.AccessibleName = "";
            this.lblPartNumber.ForeColor = System.Drawing.Color.Black;
            this.lblPartNumber.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblPartNumber.Location = new System.Drawing.Point(9, 125);
            this.lblPartNumber.Name = "lblPartNumber";
            this.lblPartNumber.Size = new System.Drawing.Size(76, 21);
            this.lblPartNumber.TabIndex = 10;
            this.lblPartNumber.Text = "Part Number";
            this.lblPartNumber.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblPartName
            // 
            this.lblPartName.AccessibleDescription = "";
            this.lblPartName.AccessibleName = "";
            this.lblPartName.ForeColor = System.Drawing.Color.Black;
            this.lblPartName.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblPartName.Location = new System.Drawing.Point(9, 148);
            this.lblPartName.Name = "lblPartName";
            this.lblPartName.Size = new System.Drawing.Size(76, 21);
            this.lblPartName.TabIndex = 10;
            this.lblPartName.Text = "Part Name";
            this.lblPartName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtVendor
            // 
            this.txtVendor.Location = new System.Drawing.Point(87, 57);
            this.txtVendor.Name = "txtVendor";
            this.txtVendor.Size = new System.Drawing.Size(96, 20);
            this.txtVendor.TabIndex = 3;
            this.txtVendor.Text = "";
            this.txtVendor.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtVendor_KeyDown);
            this.txtVendor.Validating += new System.ComponentModel.CancelEventHandler(this.txtVendor_Validating);
            // 
            // btnVendor
            // 
            this.btnVendor.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnVendor.Location = new System.Drawing.Point(185, 55);
            this.btnVendor.Name = "btnVendor";
            this.btnVendor.Size = new System.Drawing.Size(23, 22);
            this.btnVendor.TabIndex = 4;
            this.btnVendor.Text = "...";
            this.btnVendor.Click += new System.EventHandler(this.btnVendor_Click);
            // 
            // txtCategory
            // 
            this.txtCategory.Location = new System.Drawing.Point(87, 80);
            this.txtCategory.Name = "txtCategory";
            this.txtCategory.Size = new System.Drawing.Size(96, 20);
            this.txtCategory.TabIndex = 3;
            this.txtCategory.Text = "";
            this.txtCategory.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCategory_KeyDown);
            this.txtCategory.Validating += new System.ComponentModel.CancelEventHandler(this.txtCategory_Validating);
            // 
            // btnCategory
            // 
            this.btnCategory.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnCategory.Location = new System.Drawing.Point(185, 78);
            this.btnCategory.Name = "btnCategory";
            this.btnCategory.Size = new System.Drawing.Size(23, 22);
            this.btnCategory.TabIndex = 4;
            this.btnCategory.Text = "...";
            this.btnCategory.Click += new System.EventHandler(this.btnCategory_Click);
            // 
            // txtModel
            // 
            this.txtModel.Location = new System.Drawing.Point(87, 103);
            this.txtModel.Name = "txtModel";
            this.txtModel.Size = new System.Drawing.Size(96, 20);
            this.txtModel.TabIndex = 3;
            this.txtModel.Text = "";
            this.txtModel.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtModel_KeyDown);
            this.txtModel.Validating += new System.ComponentModel.CancelEventHandler(this.txtModel_Validating);
            // 
            // btnModel
            // 
            this.btnModel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnModel.Location = new System.Drawing.Point(185, 101);
            this.btnModel.Name = "btnModel";
            this.btnModel.Size = new System.Drawing.Size(23, 22);
            this.btnModel.TabIndex = 4;
            this.btnModel.Text = "...";
            this.btnModel.Click += new System.EventHandler(this.btnModel_Click);
            // 
            // txtPartNumber
            // 
            this.txtPartNumber.Location = new System.Drawing.Point(87, 126);
            this.txtPartNumber.Name = "txtPartNumber";
            this.txtPartNumber.Size = new System.Drawing.Size(96, 20);
            this.txtPartNumber.TabIndex = 3;
            this.txtPartNumber.Text = "";
            this.txtPartNumber.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPartNumber_KeyDown);
            this.txtPartNumber.Validating += new System.ComponentModel.CancelEventHandler(this.txtPartNumber_Validating);
            // 
            // btnPartNumber
            // 
            this.btnPartNumber.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnPartNumber.Location = new System.Drawing.Point(185, 124);
            this.btnPartNumber.Name = "btnPartNumber";
            this.btnPartNumber.Size = new System.Drawing.Size(23, 22);
            this.btnPartNumber.TabIndex = 4;
            this.btnPartNumber.Text = "...";
            this.btnPartNumber.Click += new System.EventHandler(this.btnPartNumber_Click);
            // 
            // txtPartName
            // 
            this.txtPartName.Location = new System.Drawing.Point(87, 149);
            this.txtPartName.Name = "txtPartName";
            this.txtPartName.Size = new System.Drawing.Size(96, 20);
            this.txtPartName.TabIndex = 3;
            this.txtPartName.Text = "";
            this.txtPartName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPartName_KeyDown);
            this.txtPartName.Validating += new System.ComponentModel.CancelEventHandler(this.txtPartName_Validating);
            // 
            // btnPartName
            // 
            this.btnPartName.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnPartName.Location = new System.Drawing.Point(185, 147);
            this.btnPartName.Name = "btnPartName";
            this.btnPartName.Size = new System.Drawing.Size(23, 22);
            this.btnPartName.TabIndex = 4;
            this.btnPartName.Text = "...";
            this.btnPartName.Click += new System.EventHandler(this.btnPartName_Click);
            // 
            // MRPRegenerationProcess
            // 
            this.AccessibleDescription = "";
            this.AccessibleName = "";
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(286, 204);
            this.Controls.Add(this.lblVendor);
            this.Controls.Add(this.ptbImage);
            this.Controls.Add(this.btnCycle);
            this.Controls.Add(this.txtCycle);
            this.Controls.Add(this.cboCCN);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnHelp);
            this.Controls.Add(this.lblCycle);
            this.Controls.Add(this.lblCCN);
            this.Controls.Add(this.btnPreviewCycle);
            this.Controls.Add(this.btnProcess);
            this.Controls.Add(this.lblCategory);
            this.Controls.Add(this.lblModel);
            this.Controls.Add(this.lblPartNumber);
            this.Controls.Add(this.lblPartName);
            this.Controls.Add(this.txtVendor);
            this.Controls.Add(this.btnVendor);
            this.Controls.Add(this.txtCategory);
            this.Controls.Add(this.btnCategory);
            this.Controls.Add(this.txtModel);
            this.Controls.Add(this.btnModel);
            this.Controls.Add(this.txtPartNumber);
            this.Controls.Add(this.btnPartNumber);
            this.Controls.Add(this.txtPartName);
            this.Controls.Add(this.btnPartName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "MRPRegenerationProcess";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MRP Regeneration Process";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.MRPRegenerationProcess_Closing);
            this.Load += new System.EventHandler(this.MRPRegenerationProcess_Load);
            ((System.ComponentModel.ISupportInitialize)(this.cboCCN)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion

        /// <summary>
        /// Form load event :
        ///		- Reset form
        ///		- Fill defaul data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MRPRegenerationProcess_Load(object sender, EventArgs e)
        {
            const string METHOD_NAME = THIS + ".MRPRegenerationProcess_Load()";
            try
            {
                Security objSecurity = new Security();
                this.Name = THIS;
                if (objSecurity.SetRightForUserOnForm(this, SystemProperty.UserName) == 0)
                {
                    this.Close();
                    return;
                }

                txtCycle.Text = string.Empty;

                //Load CCN and set default CCN
                UtilsBO boUtils = new UtilsBO();
                DataSet dstCCN = boUtils.ListCCN();
                cboCCN.DataSource = dstCCN.Tables[MST_CCNTable.TABLE_NAME];
                cboCCN.DisplayMember = MST_CCNTable.CODE_FLD;
                cboCCN.ValueMember = MST_CCNTable.CCNID_FLD;
                FormControlComponents.PutDataIntoC1ComboBox(cboCCN, dstCCN.Tables[MST_CCNTable.TABLE_NAME], MST_CCNTable.CODE_FLD, MST_CCNTable.CCNID_FLD, MST_CCNTable.TABLE_NAME);
                txtVendor.Tag = string.Empty;
                txtCategory.Tag = string.Empty;
                txtModel.Tag = string.Empty;
                txtPartNumber.Tag = string.Empty;
                if (SystemProperty.CCNID != 0)
                {
                    cboCCN.SelectedValue = SystemProperty.CCNID;
                }
                if (intMRPCycleOptionMasterID > 0)
                {
                    MRPRegenerationProcessBO boMRPRegenerationProcess = new MRPRegenerationProcessBO();
                    MTR_MRPCycleOptionMasterVO voMTR_MRPCycleOptionMaster = (MTR_MRPCycleOptionMasterVO)boMRPRegenerationProcess.GetCycleByMasterID(intMRPCycleOptionMasterID);
                    txtCycle.Text = voMTR_MRPCycleOptionMaster.Cycle;
                    txtCycle.Tag = voMTR_MRPCycleOptionMaster.MRPCycleOptionMasterID;
                    dtmAsOfDate = voMTR_MRPCycleOptionMaster.AsOfDate;
                    intPlanHorizon = voMTR_MRPCycleOptionMaster.PlanHorizon;
                    btnPreviewCycle.Enabled = true;
                }
                else
                    btnPreviewCycle.Enabled = false;
            }
            catch (PCSException ex)
            {
                PCSMessageBox.Show(ex.mCode, MessageBoxIcon.Error);
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
        }

        /// <summary>
        ///  It's called by the Thread
        /// </summary>
        private void GenMRPPlanThreadOffLine()
        {
            const string METHOD_NAME = THIS + ".GenMRPPlanThreadOffLine()";
            try
            {                
                const string MRP_FLD = "MRP";
                var boMRPRegen = new MRPRegenerationProcessBO();

                DateTime dtmStart = DateTime.Now;
                boMRPRegen.GenMRPPlanOffLine(int.Parse(cboCCN.SelectedValue.ToString()), int.Parse(txtCycle.Tag.ToString()),
                    txtVendor.Tag.ToString(), txtCategory.Tag.ToString(), txtModel.Tag.ToString(), txtPartNumber.Tag.ToString());
                DateTime dtmEnd = DateTime.Now;
                double dblTotalMinutes = dtmEnd.Subtract(dtmStart).TotalMinutes;
                string message = string.Format("MRP Regeneration completed in : {0} minute(s)", dblTotalMinutes.ToString("#,##0.00"));
                MessageBox.Show(message, "Production Control System", MessageBoxButtons.OK, MessageBoxIcon.Information);
                var strMessage = new string[1];
                strMessage[0] = MRP_FLD;
                PCSMessageBox.Show(ErrorCode.MESSAGE_GENERATED_SUCCESSFULLY, MessageBoxIcon.Information, strMessage);
            }
            catch (PCSException ex)
            {
                if (ex.mCode == ErrorCode.MESSAGE_DCP_CONFIG_CALENDAR_FROM_X_TO_Y)
                {
                    var strMessage = new string[2];
                    strMessage[0] = dtmAsOfDate.Year.ToString();
                    strMessage[1] = dtmAsOfDate.AddDays(intPlanHorizon).Year.ToString();
                    PCSMessageBox.Show(ex.mCode, MessageBoxIcon.Error, strMessage);
                }
                else
                    PCSMessageBox.Show(ex.mCode, MessageBoxIcon.Error);
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
                SetControlPropertyValue(ptbImage, "Visible", false);
                SetControlPropertyValue(btnProcess, "Enabled", true);
            }
        }

        /// <summary>
        /// Start the CPO generation process 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnProcess_Click(object sender, EventArgs e)
        {
            const string METHOD_NAME = THIS + ".btnProcess_Click()";
            try
            {
                if (cboCCN.SelectedValue == null)
                {
                    PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Warning);
                    cboCCN.Focus();
                    return;
                }
                if (txtCycle.Text.Trim() == string.Empty)
                {
                    PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Warning);
                    txtCycle.Focus();
                    return;
                }
                DateTime dtmDBDate = new UtilsBO().GetDBDate();
                dtmDBDate = new DateTime(dtmDBDate.Year, dtmDBDate.Month, dtmDBDate.Day);
                if (dtmAsOfDate < dtmDBDate)
                {
                    PCSMessageBox.Show(ErrorCode.MESSAGE_DCP_ASOFDATE_MUST_IN_FUTURE, MessageBoxIcon.Error);
                    txtCycle.Focus();
                    return;
                }
                ptbImage.Visible = true;
                btnProcess.Enabled = false;
                thrNewGen = new Thread(GenMRPPlanThreadOffLine);
                thrNewGen.Start();
                if (thrNewGen.ThreadState == ThreadState.Stopped || !thrNewGen.IsAlive)
                {
                    thrNewGen = null;
                    ptbImage.Visible = false;
                    btnProcess.Enabled = true;
                }
            }
            catch (PCSException ex)
            {
                if (ex.mCode == ErrorCode.MESSAGE_DCP_CONFIG_CALENDAR_FROM_X_TO_Y)
                {
                    string[] strMessage = new string[2];
                    strMessage[0] = dtmAsOfDate.Year.ToString();
                    strMessage[1] = dtmAsOfDate.AddDays(intPlanHorizon).Year.ToString();
                    PCSMessageBox.Show(ex.mCode, MessageBoxIcon.Error, strMessage);
                }
                else
                    PCSMessageBox.Show(ex.mCode, MessageBoxIcon.Error);
                try
                {
                    Logger.LogMessage(ex.CauseException, METHOD_NAME, Level.ERROR);
                }
                catch
                {
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
                }
                btnProcess.Enabled = true;
            }
            catch (Exception ex)
            {
                PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxIcon.Error);
                try
                {
                    Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
                }
                catch
                {
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
                }
                btnProcess.Enabled = true;
            }
        }

        /// <summary>
        /// Open Cycle Option
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPreviewCycle_Click(object sender, EventArgs e)
        {
            const string METHOD_NAME = THIS + ".btnPreviewCycle_Click()";
            try
            {
                if (txtCycle.Text.Trim() != string.Empty)
                {
                    MRPCycleOption frmCycleOption = new MRPCycleOption(int.Parse(txtCycle.Tag.ToString()), int.Parse(cboCCN.SelectedValue.ToString()), txtCycle.Text.Trim());
                    frmCycleOption.ShowDialog();
                    txtCycle.Focus();
                }
            }
            catch (PCSException ex)
            {
                PCSMessageBox.Show(ex.mCode, MessageBoxIcon.Error);
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
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Open search form to select the Cycle
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCycle_Click(object sender, EventArgs e)
        {
            const string METHOD_NAME = THIS + ".btnCycle_Click()";
            try
            {
                Hashtable htbCriterial = new Hashtable();
                if (cboCCN.SelectedValue == null)
                {
                    PCSMessageBox.Show(ErrorCode.MESSAGE_RGA_CCN, MessageBoxIcon.Warning);
                    cboCCN.Focus();
                    return;
                }
                else
                {
                    htbCriterial.Add(MTR_MPSCycleOptionMasterTable.CCNID_FLD, cboCCN.SelectedValue);
                }
                DataRowView drvResult = FormControlComponents.OpenSearchForm(MTR_MRPCycleOptionMasterTable.TABLE_NAME, MTR_MPSCycleOptionMasterTable.CYCLE_FLD, txtCycle.Text.Trim(), htbCriterial, true);
                if (drvResult != null)
                {
                    txtCycle.Text = drvResult[MTR_MRPCycleOptionMasterTable.CYCLE_FLD].ToString();
                    txtCycle.Tag = drvResult[MTR_MRPCycleOptionMasterTable.MRPCYCLEOPTIONMASTERID_FLD].ToString();
                    btnPreviewCycle.Enabled = true;
                    dtmAsOfDate = (DateTime)drvResult[MTR_MRPCycleOptionMasterTable.ASOFDATE_FLD];
                    if (drvResult[MTR_MRPCycleOptionMasterTable.ASOFDATE_FLD].ToString() != string.Empty)
                        if (drvResult[MTR_MRPCycleOptionMasterTable.PLANHORIZON_FLD].ToString() != string.Empty)
                        {
                            intPlanHorizon = (int)drvResult[MTR_MRPCycleOptionMasterTable.PLANHORIZON_FLD];
                        }
                        else intPlanHorizon = 0;
                }
                else
                {
                    txtCycle.Focus();
                }
            }
            catch (PCSException ex)
            {
                PCSMessageBox.Show(ex.mCode, MessageBoxIcon.Error);
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
        }

        #region Change backcolor when focus or lost focus
        private void OnEnterControl(object sender, EventArgs e)
        {
            const string METHOD_NAME = THIS + ". OnEnterControl()";
            try
            {
                FormControlComponents.OnEnterControl(sender, e);
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

        private void OnLeaveControl(object sender, EventArgs e)
        {
            const string METHOD_NAME = THIS + ". OnLeaveControl()";
            try
            {
                FormControlComponents.OnLeaveControl(sender, e);
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
        #endregion

        private void txtCycle_KeyDown(object sender, KeyEventArgs e)
        {
            const string METHOD_NAME = THIS + ".txtCycle_KeyDown()";
            try
            {
                if ((e.KeyCode == Keys.F4) && (btnCycle.Enabled))
                {
                    btnCycle_Click(btnCycle, new EventArgs());
                }
            }
            catch (PCSException ex)
            {
                PCSMessageBox.Show(ex.mCode, MessageBoxIcon.Error);
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
        }

        private void MRPRegenerationProcess_Closing(object sender, CancelEventArgs e)
        {
            const string METHOD_NAME = THIS + ".MPSRegenerationProcess_Closed()";
            try
            {
                // ask user to stop the thread
                if (thrNewGen != null)
                {
                    if (thrNewGen.IsAlive || thrNewGen.ThreadState == ThreadState.Running)
                    {
                        thrNewGen.Interrupt();
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

        private void txtCycle_Validating(object sender, CancelEventArgs e)
        {
            const string METHOD_NAME = THIS + ".txtCycle_Leave()";
            try
            {
                OnLeaveControl(sender, e);
                if (txtCycle.Text.Trim() == string.Empty)
                {
                    btnPreviewCycle.Enabled = false;
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
                    htbCriterial.Add(MTR_MPSCycleOptionMasterTable.CCNID_FLD, cboCCN.SelectedValue);
                }
                DataRowView drvResult = FormControlComponents.OpenSearchForm(MTR_MRPCycleOptionMasterTable.TABLE_NAME, MTR_MPSCycleOptionMasterTable.CYCLE_FLD, txtCycle.Text.Trim(), htbCriterial, false);
                if (drvResult != null)
                {
                    btnPreviewCycle.Enabled = true;
                    txtCycle.Text = drvResult[MTR_MRPCycleOptionMasterTable.CYCLE_FLD].ToString();
                    txtCycle.Tag = drvResult[MTR_MRPCycleOptionMasterTable.MRPCYCLEOPTIONMASTERID_FLD].ToString();
                    dtmAsOfDate = (DateTime)drvResult[MTR_MRPCycleOptionMasterTable.ASOFDATE_FLD];
                    if (drvResult[MTR_MRPCycleOptionMasterTable.ASOFDATE_FLD].ToString() != string.Empty)
                        if (drvResult[MTR_MRPCycleOptionMasterTable.PLANHORIZON_FLD].ToString() != string.Empty)
                        {
                            intPlanHorizon = (int)drvResult[MTR_MRPCycleOptionMasterTable.PLANHORIZON_FLD];
                        }
                        else intPlanHorizon = 0;
                }
                else
                    e.Cancel = true;
            }
            catch (PCSException ex)
            {
                PCSMessageBox.Show(ex.mCode, MessageBoxIcon.Error);
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
        }

        private void txtVendor_Validating(object sender, CancelEventArgs e)
        {
            const string METHOD_NAME = THIS + ".txtVendor_Validating()";
            try
            {
                if (!txtVendor.Modified) return;
                if (txtVendor.Text.Trim() == string.Empty)
                {
                    txtVendor.Tag = string.Empty;
                    txtPartNumber.Text = string.Empty;
                    txtPartNumber.Tag = string.Empty;
                    txtPartName.Text = string.Empty;
                    return;
                }
                DataTable dtbData = FormControlComponents.OpenSearchFormForMultiSelectedRow(MST_PartyTable.TABLE_NAME, MST_PartyTable.CODE_FLD, txtVendor.Text, string.Empty, false);
                if (dtbData != null && dtbData.Rows.Count > 0)
                {
                    StringBuilder sbID = new StringBuilder();
                    foreach (DataRow drowData in dtbData.Rows)
                        sbID.Append(drowData[MST_PartyTable.PARTYID_FLD].ToString()).Append(",");
                    txtVendor.Text = (dtbData.Rows.Count > 1) ? "Multi Selection" : dtbData.Rows[0][MST_PartyTable.CODE_FLD].ToString();
                    txtVendor.Tag = sbID.ToString(0, sbID.Length - 1);
                    txtPartNumber.Text = string.Empty;
                    txtPartNumber.Tag = string.Empty;
                    txtPartName.Text = string.Empty;
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

        private void txtVendor_KeyDown(object sender, KeyEventArgs e)
        {
            const string METHOD_NAME = THIS + ".txtVendor_KeyDown()";
            try
            {
                if (e.KeyCode == Keys.F4)
                    btnVendor_Click(null, null);
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

        private void btnVendor_Click(object sender, EventArgs e)
        {
            const string METHOD_NAME = THIS + ".btnVendor_Click()";
            try
            {
                DataTable dtbData = FormControlComponents.OpenSearchFormForMultiSelectedRow(MST_PartyTable.TABLE_NAME, MST_PartyTable.CODE_FLD, txtVendor.Text, string.Empty, true);
                if (dtbData != null && dtbData.Rows.Count > 0)
                {
                    StringBuilder sbID = new StringBuilder();
                    foreach (DataRow drowData in dtbData.Rows)
                        sbID.Append(drowData[MST_PartyTable.PARTYID_FLD].ToString()).Append(",");
                    txtVendor.Text = (dtbData.Rows.Count > 1) ? "Multi Selection" : dtbData.Rows[0][MST_PartyTable.CODE_FLD].ToString();
                    txtVendor.Tag = sbID.ToString(0, sbID.Length - 1);
                    txtPartNumber.Text = string.Empty;
                    txtPartNumber.Tag = string.Empty;
                    txtPartName.Text = string.Empty;
                }
                else
                    txtVendor.Focus();
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

        private void txtCategory_Validating(object sender, CancelEventArgs e)
        {
            const string METHOD_NAME = THIS + ".txtCategory_Validating()";
            try
            {
                if (!txtCategory.Modified) return;
                if (txtCategory.Text.Trim() == string.Empty)
                {
                    txtCategory.Tag = string.Empty;
                    txtPartNumber.Text = string.Empty;
                    txtPartNumber.Tag = string.Empty;
                    txtPartName.Text = string.Empty;
                    return;
                }
                DataTable dtbData = FormControlComponents.OpenSearchFormForMultiSelectedRow(ITM_CategoryTable.TABLE_NAME, ITM_CategoryTable.CODE_FLD, txtCategory.Text, string.Empty, false);
                if (dtbData != null && dtbData.Rows.Count > 0)
                {
                    StringBuilder sbID = new StringBuilder();
                    foreach (DataRow drowData in dtbData.Rows)
                        sbID.Append(drowData[ITM_CategoryTable.CATEGORYID_FLD].ToString()).Append(",");
                    txtCategory.Text = (dtbData.Rows.Count > 1) ? "Multi Selection" : dtbData.Rows[0][ITM_CategoryTable.CODE_FLD].ToString();
                    txtCategory.Tag = sbID.ToString(0, sbID.Length - 1);
                    txtPartNumber.Text = string.Empty;
                    txtPartNumber.Tag = string.Empty;
                    txtPartName.Text = string.Empty;
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

        private void txtCategory_KeyDown(object sender, KeyEventArgs e)
        {
            const string METHOD_NAME = THIS + ".txtCategory_KeyDown()";
            try
            {
                if (e.KeyCode == Keys.F4)
                    btnCategory_Click(null, null);
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

        private void btnCategory_Click(object sender, EventArgs e)
        {
            const string METHOD_NAME = THIS + ".btnCategory_Click()";
            try
            {
                DataTable dtbData = FormControlComponents.OpenSearchFormForMultiSelectedRow(ITM_CategoryTable.TABLE_NAME, ITM_CategoryTable.CODE_FLD, txtCategory.Text, string.Empty, true);
                if (dtbData != null && dtbData.Rows.Count > 0)
                {
                    StringBuilder sbID = new StringBuilder();
                    foreach (DataRow drowData in dtbData.Rows)
                        sbID.Append(drowData[ITM_CategoryTable.CATEGORYID_FLD].ToString()).Append(",");
                    txtCategory.Text = (dtbData.Rows.Count > 1) ? "Multi Selection" : dtbData.Rows[0][ITM_CategoryTable.CODE_FLD].ToString();
                    txtCategory.Tag = sbID.ToString(0, sbID.Length - 1);
                    txtPartNumber.Text = string.Empty;
                    txtPartNumber.Tag = string.Empty;
                    txtPartName.Text = string.Empty;
                }
                else
                    txtCategory.Focus();
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

        private void txtModel_Validating(object sender, CancelEventArgs e)
        {
            const string METHOD_NAME = THIS + ".txtModel_Validating()";
            try
            {
                if (!txtModel.Modified) return;
                if (txtModel.Text.Trim() == string.Empty)
                {
                    txtModel.Tag = string.Empty;
                    txtPartNumber.Text = string.Empty;
                    txtPartNumber.Tag = string.Empty;
                    txtPartName.Text = string.Empty;
                    return;
                }
                DataTable dtbData = FormControlComponents.OpenSearchFormForMultiSelectedRow(MODEL_VIEW, ITM_ProductTable.REVISION_FLD, txtModel.Text, string.Empty, false);
                if (dtbData != null && dtbData.Rows.Count > 0)
                {
                    StringBuilder sbCode = new StringBuilder();
                    foreach (DataRow drowData in dtbData.Rows)
                        sbCode.Append("'" + drowData[ITM_ProductTable.REVISION_FLD].ToString() + "'").Append(",");
                    txtModel.Text = (dtbData.Rows.Count > 1) ? "Multi Selection" : sbCode.ToString(0, sbCode.Length - 1);
                    txtModel.Tag = sbCode.ToString(0, sbCode.Length - 1);
                    txtPartNumber.Text = string.Empty;
                    txtPartNumber.Tag = string.Empty;
                    txtPartName.Text = string.Empty;
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

        private void txtModel_KeyDown(object sender, KeyEventArgs e)
        {
            const string METHOD_NAME = THIS + ".txtVendor_KeyDown()";
            try
            {
                if (e.KeyCode == Keys.F4)
                    btnModel_Click(null, null);
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

        private void btnModel_Click(object sender, EventArgs e)
        {
            const string METHOD_NAME = THIS + ".btnVendor_Click()";
            try
            {
                DataTable dtbData = FormControlComponents.OpenSearchFormForMultiSelectedRow(MODEL_VIEW, ITM_ProductTable.REVISION_FLD, txtModel.Text, string.Empty, true);
                if (dtbData != null && dtbData.Rows.Count > 0)
                {
                    StringBuilder sbCode = new StringBuilder();
                    foreach (DataRow drowData in dtbData.Rows)
                        sbCode.Append("'" + drowData[ITM_ProductTable.REVISION_FLD].ToString() + "'").Append(",");
                    txtModel.Text = (dtbData.Rows.Count > 1) ? "Multi Selection" : sbCode.ToString(0, sbCode.Length - 1);
                    txtModel.Tag = sbCode.ToString(0, sbCode.Length - 1);
                    txtPartNumber.Text = string.Empty;
                    txtPartNumber.Tag = string.Empty;
                    txtPartName.Text = string.Empty;
                }
                else
                    txtModel.Focus();
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

        private void txtPartNumber_Validating(object sender, CancelEventArgs e)
        {
            const string METHOD_NAME = THIS + ".txtPartNumber_Validating()";
            try
            {
                if (!txtPartNumber.Modified) return;
                if (txtPartNumber.Text.Trim() == string.Empty)
                {
                    txtPartNumber.Tag = string.Empty;
                    txtPartName.Text = string.Empty;
                    return;
                }
                string strFilter = ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.CCNID_FLD + "=" + cboCCN.SelectedValue.ToString()
                    + " AND " + ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.MAKEITEM_FLD + "=0";

                if (txtVendor.Tag != null && txtVendor.Tag.ToString() != string.Empty)
                    strFilter += " AND " + ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.PRIMARYVENDORID_FLD + " IN (" + txtVendor.Tag.ToString() + ")";
                if (txtCategory.Tag != null && txtCategory.Tag.ToString() != string.Empty)
                    strFilter += " AND " + ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.CATEGORYID_FLD + " IN (" + txtCategory.Tag.ToString() + ")";
                if (txtModel.Tag != null && txtModel.Tag.ToString() != string.Empty)
                    strFilter += " AND " + ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.REVISION_FLD + " IN (" + txtModel.Tag.ToString() + ")";
                DataTable dtbData = FormControlComponents.OpenSearchFormForMultiSelectedRow(ITM_ProductTable.TABLE_NAME, ITM_ProductTable.CODE_FLD, txtPartNumber.Text, strFilter, false);
                if (dtbData != null && dtbData.Rows.Count > 0)
                {
                    StringBuilder sbID = new StringBuilder();
                    foreach (DataRow drowData in dtbData.Rows)
                        sbID.Append(drowData[ITM_ProductTable.PRODUCTID_FLD].ToString()).Append(",");
                    txtPartNumber.Text = (dtbData.Rows.Count > 1) ? "Multi Selection" : dtbData.Rows[0][ITM_ProductTable.CODE_FLD].ToString();
                    txtPartNumber.Tag = sbID.ToString(0, sbID.Length - 1);
                    txtPartName.Text = (dtbData.Rows.Count > 1) ? "Multi Selection" : dtbData.Rows[0][ITM_ProductTable.DESCRIPTION_FLD].ToString();
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

        private void txtPartNumber_KeyDown(object sender, KeyEventArgs e)
        {
            const string METHOD_NAME = THIS + ".txtPartNumber_KeyDown()";
            try
            {
                if (e.KeyCode == Keys.F4)
                    btnPartNumber_Click(null, null);
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

        private void btnPartNumber_Click(object sender, EventArgs e)
        {
            const string METHOD_NAME = THIS + ".btnPartNumber_Click()";
            try
            {
                string strFilter = ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.CCNID_FLD + "=" + cboCCN.SelectedValue.ToString()
                    + " AND " + ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.MAKEITEM_FLD + "=0";

                if (txtVendor.Tag != null && txtVendor.Tag.ToString() != string.Empty)
                    strFilter += " AND " + ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.PRIMARYVENDORID_FLD + " IN (" + txtVendor.Tag.ToString() + ")";
                if (txtCategory.Tag != null && txtCategory.Tag.ToString() != string.Empty)
                    strFilter += " AND " + ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.CATEGORYID_FLD + " IN (" + txtCategory.Tag.ToString() + ")";
                if (txtModel.Tag != null && txtModel.Tag.ToString() != string.Empty)
                    strFilter += " AND " + ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.REVISION_FLD + " IN (" + txtModel.Tag.ToString() + ")";
                DataTable dtbData = FormControlComponents.OpenSearchFormForMultiSelectedRow(ITM_ProductTable.TABLE_NAME, ITM_ProductTable.CODE_FLD, txtPartNumber.Text, strFilter, true);
                if (dtbData != null && dtbData.Rows.Count > 0)
                {
                    StringBuilder sbID = new StringBuilder();
                    foreach (DataRow drowData in dtbData.Rows)
                        sbID.Append(drowData[ITM_ProductTable.PRODUCTID_FLD].ToString()).Append(",");
                    txtPartNumber.Text = (dtbData.Rows.Count > 1) ? "Multi Selection" : dtbData.Rows[0][ITM_ProductTable.CODE_FLD].ToString();
                    txtPartNumber.Tag = sbID.ToString(0, sbID.Length - 1);
                    txtPartName.Text = (dtbData.Rows.Count > 1) ? "Multi Selection" : dtbData.Rows[0][ITM_ProductTable.DESCRIPTION_FLD].ToString();
                }
                else
                    txtPartNumber.Focus();
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

        private void txtPartName_Validating(object sender, CancelEventArgs e)
        {
            const string METHOD_NAME = THIS + ".txtPartName_Validating()";
            try
            {
                if (!txtPartName.Modified) return;
                if (txtPartName.Text.Trim() == string.Empty)
                {
                    txtPartNumber.Text = string.Empty;
                    txtPartNumber.Tag = null;
                    txtPartName.Text = string.Empty;
                    return;
                }
                string strFilter = ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.CCNID_FLD + "=" + cboCCN.SelectedValue.ToString()
                    + " AND " + ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.MAKEITEM_FLD + "=0";

                if (txtVendor.Tag != null && txtVendor.Tag.ToString() != string.Empty)
                    strFilter += " AND " + ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.PRIMARYVENDORID_FLD + " IN (" + txtVendor.Tag.ToString() + ")";
                if (txtCategory.Tag != null && txtCategory.Tag.ToString() != string.Empty)
                    strFilter += " AND " + ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.CATEGORYID_FLD + " IN (" + txtCategory.Tag.ToString() + ")";
                if (txtModel.Tag != null && txtModel.Tag.ToString() != string.Empty)
                    strFilter += " AND " + ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.REVISION_FLD + " IN (" + txtModel.Tag.ToString() + ")";
                DataTable dtbData = FormControlComponents.OpenSearchFormForMultiSelectedRow(ITM_ProductTable.TABLE_NAME, ITM_ProductTable.CODE_FLD, txtPartNumber.Text, strFilter, false);
                if (dtbData != null && dtbData.Rows.Count > 0)
                {
                    StringBuilder sbID = new StringBuilder();
                    foreach (DataRow drowData in dtbData.Rows)
                        sbID.Append(drowData[ITM_ProductTable.PRODUCTID_FLD].ToString()).Append(",");
                    txtPartNumber.Text = (dtbData.Rows.Count > 1) ? "Multi Selection" : dtbData.Rows[0][ITM_ProductTable.CODE_FLD].ToString();
                    txtPartNumber.Tag = sbID.ToString(0, sbID.Length - 1);
                    txtPartName.Text = (dtbData.Rows.Count > 1) ? "Multi Selection" : dtbData.Rows[0][ITM_ProductTable.DESCRIPTION_FLD].ToString();
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

        private void txtPartName_KeyDown(object sender, KeyEventArgs e)
        {
            const string METHOD_NAME = THIS + ".txtPartName_KeyDown()";
            try
            {
                if (e.KeyCode == Keys.F4)
                    btnPartName_Click(null, null);
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

        private void btnPartName_Click(object sender, EventArgs e)
        {
            const string METHOD_NAME = THIS + ".btnPartName_Click()";
            try
            {
                string strFilter = ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.CCNID_FLD + "=" + cboCCN.SelectedValue.ToString()
                    + " AND " + ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.MAKEITEM_FLD + "=0";

                if (txtVendor.Tag != null && txtVendor.Tag.ToString() != string.Empty)
                    strFilter += " AND " + ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.PRIMARYVENDORID_FLD + " IN (" + txtVendor.Tag.ToString() + ")";
                if (txtCategory.Tag != null && txtCategory.Tag.ToString() != string.Empty)
                    strFilter += " AND " + ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.CATEGORYID_FLD + " IN (" + txtCategory.Tag.ToString() + ")";
                if (txtModel.Tag != null && txtModel.Tag.ToString() != string.Empty)
                    strFilter += " AND " + ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.REVISION_FLD + " IN (" + txtModel.Tag.ToString() + ")";
                DataTable dtbData = FormControlComponents.OpenSearchFormForMultiSelectedRow(ITM_ProductTable.TABLE_NAME, ITM_ProductTable.CODE_FLD, txtPartNumber.Text, strFilter, true);
                if (dtbData != null && dtbData.Rows.Count > 0)
                {
                    StringBuilder sbID = new StringBuilder();
                    foreach (DataRow drowData in dtbData.Rows)
                        sbID.Append(drowData[ITM_ProductTable.PRODUCTID_FLD].ToString()).Append(",");
                    txtPartNumber.Text = (dtbData.Rows.Count > 1) ? "Multi Selection" : dtbData.Rows[0][ITM_ProductTable.CODE_FLD].ToString();
                    txtPartNumber.Tag = sbID.ToString(0, sbID.Length - 1);
                    txtPartName.Text = (dtbData.Rows.Count > 1) ? "Multi Selection" : dtbData.Rows[0][ITM_ProductTable.DESCRIPTION_FLD].ToString();
                }
                else
                    txtPartName.Focus();
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
