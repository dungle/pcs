using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using C1.Win.C1Input;
using C1.Win.C1List;
using C1.Win.C1TrueDBGrid;
using PCSComProduction.WorkOrder.BO;
using PCSComProduction.WorkOrder.DS;
using PCSComUtils.Common;
using PCSComUtils.Common.BO;
using PCSComUtils.Framework.ReportFrame.BO;
using PCSComUtils.PCSExc;
using PCSUtils.Log;
using PCSUtils.Utils;
using PCSUtils.Framework.ReportFrame;
using PCSComMaterials.Inventory.BO;
using AlignHorzEnum = C1.Win.C1TrueDBGrid.AlignHorzEnum;
using BeforeColEditEventArgs = C1.Win.C1TrueDBGrid.BeforeColEditEventArgs;
using BeforeColUpdateEventArgs = C1.Win.C1TrueDBGrid.BeforeColUpdateEventArgs;
using CancelEventArgs = System.ComponentModel.CancelEventArgs;
using ColEventArgs = C1.Win.C1TrueDBGrid.ColEventArgs;
using PresentationEnum = C1.Win.C1TrueDBGrid.PresentationEnum;
using C1.C1Report;

namespace PCSProduction.WorkOrder
{
    /// <summary>
    /// Summary description for MultiWOIssueMaterial.
    /// </summary>
    public class MultiWOIssueMaterial : Form
    {
        #region System Generated Variables

        private Label lblIssueNo;
        private Label lblMasLoc;
        private Label lblPostDate;
        private Label lblCCN;
        private C1Combo CCNCombo;
        private C1TrueDBGrid DetailGrid;
        private C1DateEdit PostDatePicker;
        private System.Windows.Forms.Label lblToLocation;
        private System.Windows.Forms.Label lblToBin;
        private System.Windows.Forms.Label lblShift;
        private System.Windows.Forms.Label lblFromBin;
        private System.Windows.Forms.Label lblFromLoc;
        private Container components = null;

        #endregion Generated

        #region Constants

        private const string THIS = "PCSProduction.WorkOrder.MultiWOIssueMaterial";
        private const string REPORTFLD_TITLE = "fldTitle";
        //private const string CONVERT_DATE_TOSTRING = "d";
        //private const string CONVERT_TIME_TOSTRING = "t";
        private const string HASBIN = "HasBin";
        //define some constant column
        private const string WORKORDERNO_COL = PRO_WorkOrderMasterTable.TABLE_NAME + PRO_WorkOrderMasterTable.WORKORDERNO_FLD;
        private const string WORKORDERLINE_COL = PRO_WorkOrderDetailTable.TABLE_NAME + PRO_WorkOrderDetailTable.LINE_FLD;
        private const string LOCATIONCODE_COL = MST_LocationTable.TABLE_NAME + MST_LocationTable.CODE_FLD;
        private const string BINCODE_COL = MST_BINTable.TABLE_NAME + MST_BINTable.CODE_FLD;
        private const string PRODUCTCODE_COL = ITM_ProductTable.TABLE_NAME + ITM_ProductTable.CODE_FLD;
        private const string PRODUCTDESC_COL = ITM_ProductTable.TABLE_NAME + ITM_ProductTable.DESCRIPTION_FLD;
        private const string PRODUCTREVISION_COL = ITM_ProductTable.TABLE_NAME + ITM_ProductTable.REVISION_FLD;
        //HACK: Thachnn 27/10/2005
        private const string COMMITQUANTITY_COL = "CommitQuantity";
        private const string NEEDEDQUANTITY_COL = "NeededQuantity";
        private const string LOT_COL = "LOT";
        //ENDHACK: Thachnn 27/10/2005
        //private const string WORKORDERDETAIL_STATUS_COL = PRO_WorkOrderDetailTable.TABLE_NAME + PRO_WorkOrderDetailTable.STATUS_FLD;
        private const string UNITCODE_COL = MST_UnitOfMeasureTable.TABLE_NAME + MST_UnitOfMeasureTable.CODE_FLD;

        private const string AVAILABLE_QUANTITY = "AvailableQuantity";
        #endregion

        #region Variables

        private PRO_IssueMaterialMasterVO voPRO_IssueMaterialMasterVO;
        private EnumAction mFormMode;
        private DataSet dstData = null;
        private DataTable dtStoreGridLayout;
        private decimal pdecTempCommitQuantity;
        private decimal pdecChangedQuantity;
        private bool blnRecalculateCommit = true;
        private System.Windows.Forms.Label lblPurpose;
        private bool blnHasError = false;
        string strLastValidMasLoc = string.Empty;
        string strLastValidLocation = string.Empty;
        string strLastValidBin = string.Empty;
        string strLastValidPurpose = string.Empty;
        private C1.Win.C1Input.C1DateEdit IssueDayPicker;
        private System.Windows.Forms.Label lblIssueDay;
        private Label lblIssuePlan;
        private C1Button IssueNoButton;
        private C1TextBox IssueNoText;
        private C1TextBox MasterLocationText;
        private C1Button MasterLocationButton;
        private C1TextBox LocationText;
        private C1Button LocationButton;
        private C1TextBox BinText;
        private C1Button BinButton;
        private C1TextBox PurposeText;
        private C1Button PurposeButton;
        private C1TextBox ShiftText;
        private C1Button ShiftButton;
        private C1Button PrintButton;
        private C1Button SaveButton;
        private C1Button DeleteButton;
        private C1Button AddButton;
        private C1Button CloseButton;
        private C1Button HelpButton;
        private C1Button btnPrintConfiguration;
        private C1Button SelectComponenButton;
        string strLastValidShift = string.Empty;

        #endregion Variables

        #region Constructor, Destructor
        public MultiWOIssueMaterial()
        {
            InitializeComponent();
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


        #endregion Constructor, Destructor

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MultiWOIssueMaterial));
            this.lblIssueNo = new System.Windows.Forms.Label();
            this.lblMasLoc = new System.Windows.Forms.Label();
            this.lblPostDate = new System.Windows.Forms.Label();
            this.lblCCN = new System.Windows.Forms.Label();
            this.DetailGrid = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
            this.CCNCombo = new C1.Win.C1List.C1Combo();
            this.PostDatePicker = new C1.Win.C1Input.C1DateEdit();
            this.lblPurpose = new System.Windows.Forms.Label();
            this.lblToLocation = new System.Windows.Forms.Label();
            this.lblToBin = new System.Windows.Forms.Label();
            this.lblShift = new System.Windows.Forms.Label();
            this.lblFromBin = new System.Windows.Forms.Label();
            this.lblFromLoc = new System.Windows.Forms.Label();
            this.IssueDayPicker = new C1.Win.C1Input.C1DateEdit();
            this.lblIssueDay = new System.Windows.Forms.Label();
            this.lblIssuePlan = new System.Windows.Forms.Label();
            this.IssueNoButton = new C1.Win.C1Input.C1Button();
            this.IssueNoText = new C1.Win.C1Input.C1TextBox();
            this.MasterLocationText = new C1.Win.C1Input.C1TextBox();
            this.MasterLocationButton = new C1.Win.C1Input.C1Button();
            this.LocationText = new C1.Win.C1Input.C1TextBox();
            this.LocationButton = new C1.Win.C1Input.C1Button();
            this.BinText = new C1.Win.C1Input.C1TextBox();
            this.BinButton = new C1.Win.C1Input.C1Button();
            this.PurposeText = new C1.Win.C1Input.C1TextBox();
            this.PurposeButton = new C1.Win.C1Input.C1Button();
            this.ShiftText = new C1.Win.C1Input.C1TextBox();
            this.ShiftButton = new C1.Win.C1Input.C1Button();
            this.PrintButton = new C1.Win.C1Input.C1Button();
            this.SaveButton = new C1.Win.C1Input.C1Button();
            this.DeleteButton = new C1.Win.C1Input.C1Button();
            this.AddButton = new C1.Win.C1Input.C1Button();
            this.CloseButton = new C1.Win.C1Input.C1Button();
            this.HelpButton = new C1.Win.C1Input.C1Button();
            this.btnPrintConfiguration = new C1.Win.C1Input.C1Button();
            this.SelectComponenButton = new C1.Win.C1Input.C1Button();
            ((System.ComponentModel.ISupportInitialize)(this.DetailGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CCNCombo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PostDatePicker)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.IssueDayPicker)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.IssueNoText)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MasterLocationText)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LocationText)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BinText)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PurposeText)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ShiftText)).BeginInit();
            this.SuspendLayout();
            // 
            // lblIssueNo
            // 
            this.lblIssueNo.ForeColor = System.Drawing.Color.Maroon;
            resources.ApplyResources(this.lblIssueNo, "lblIssueNo");
            this.lblIssueNo.Name = "lblIssueNo";
            // 
            // lblMasLoc
            // 
            this.lblMasLoc.ForeColor = System.Drawing.Color.Maroon;
            resources.ApplyResources(this.lblMasLoc, "lblMasLoc");
            this.lblMasLoc.Name = "lblMasLoc";
            // 
            // lblPostDate
            // 
            this.lblPostDate.ForeColor = System.Drawing.Color.Maroon;
            resources.ApplyResources(this.lblPostDate, "lblPostDate");
            this.lblPostDate.Name = "lblPostDate";
            // 
            // lblCCN
            // 
            resources.ApplyResources(this.lblCCN, "lblCCN");
            this.lblCCN.ForeColor = System.Drawing.Color.Maroon;
            this.lblCCN.Name = "lblCCN";
            // 
            // DetailGrid
            // 
            resources.ApplyResources(this.DetailGrid, "DetailGrid");
            this.DetailGrid.Images.Add(((System.Drawing.Image)(resources.GetObject("DetailGrid.Images"))));
            this.DetailGrid.MaintainRowCurrency = true;
            this.DetailGrid.Name = "DetailGrid";
            this.DetailGrid.PreviewInfo.Location = new System.Drawing.Point(0, 0);
            this.DetailGrid.PreviewInfo.Size = new System.Drawing.Size(0, 0);
            this.DetailGrid.PreviewInfo.ZoomFactor = 75D;
            this.DetailGrid.PrintInfo.PageSettings = ((System.Drawing.Printing.PageSettings)(resources.GetObject("DetailGrid.PrintInfo.PageSettings")));
            this.DetailGrid.VisualStyle = C1.Win.C1TrueDBGrid.VisualStyle.Office2007Blue;
            this.DetailGrid.AfterColUpdate += new C1.Win.C1TrueDBGrid.ColEventHandler(this.dgrdData_AfterColUpdate);
            this.DetailGrid.AfterDelete += new System.EventHandler(this.dgrdData_AfterDelete);
            this.DetailGrid.BeforeColUpdate += new C1.Win.C1TrueDBGrid.BeforeColUpdateEventHandler(this.dgrdData_BeforeColUpdate);
            this.DetailGrid.BeforeColEdit += new C1.Win.C1TrueDBGrid.BeforeColEditEventHandler(this.dgrdData_BeforeColEdit);
            this.DetailGrid.ButtonClick += new C1.Win.C1TrueDBGrid.ColEventHandler(this.dgrdData_ButtonClick);
            this.DetailGrid.OnAddNew += new System.EventHandler(this.dgrdData_OnAddNew);
            this.DetailGrid.Error += new C1.Win.C1TrueDBGrid.ErrorEventHandler(this.dgrdData_Error);
            this.DetailGrid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgrdData_KeyDown);
            this.DetailGrid.PropBag = resources.GetString("DetailGrid.PropBag");
            // 
            // CCNCombo
            // 
            this.CCNCombo.AddItemSeparator = ';';
            resources.ApplyResources(this.CCNCombo, "CCNCombo");
            this.CCNCombo.CaptionHeight = 17;
            this.CCNCombo.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.CCNCombo.ColumnCaptionHeight = 17;
            this.CCNCombo.ColumnFooterHeight = 17;
            this.CCNCombo.ComboStyle = C1.Win.C1List.ComboStyleEnum.DropdownList;
            this.CCNCombo.ContentHeight = 15;
            this.CCNCombo.DeadAreaBackColor = System.Drawing.Color.Empty;
            this.CCNCombo.EditorBackColor = System.Drawing.SystemColors.Window;
            this.CCNCombo.EditorFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CCNCombo.EditorForeColor = System.Drawing.SystemColors.WindowText;
            this.CCNCombo.EditorHeight = 15;
            this.CCNCombo.Images.Add(((System.Drawing.Image)(resources.GetObject("CCNCombo.Images"))));
            this.CCNCombo.ItemHeight = 15;
            this.CCNCombo.MatchEntryTimeout = ((long)(2000));
            this.CCNCombo.MaxDropDownItems = ((short)(5));
            this.CCNCombo.MaxLength = 32767;
            this.CCNCombo.MouseCursor = System.Windows.Forms.Cursors.Default;
            this.CCNCombo.Name = "CCNCombo";
            this.CCNCombo.RowSubDividerColor = System.Drawing.Color.DarkGray;
            this.CCNCombo.VisualStyle = C1.Win.C1List.VisualStyle.Office2007Blue;
            this.CCNCombo.SelectedValueChanged += new System.EventHandler(this.cboCCN_SelectedValueChanged);
            this.CCNCombo.PropBag = resources.GetString("CCNCombo.PropBag");
            // 
            // PostDatePicker
            // 
            this.PostDatePicker.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(177)))), ((int)(((byte)(187)))), ((int)(((byte)(214)))));
            resources.ApplyResources(this.PostDatePicker, "PostDatePicker");
            // 
            // 
            // 
            this.PostDatePicker.Calendar.Font = ((System.Drawing.Font)(resources.GetObject("PostDatePicker.Calendar.Font")));
            this.PostDatePicker.Calendar.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("PostDatePicker.Calendar.ImeMode")));
            this.PostDatePicker.Calendar.ShowClearButton = false;
            this.PostDatePicker.Calendar.VisualStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.PostDatePicker.Calendar.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.PostDatePicker.ErrorInfo.ShowErrorMessage = ((bool)(resources.GetObject("PostDatePicker.ErrorInfo.ShowErrorMessage")));
            this.PostDatePicker.Name = "PostDatePicker";
            this.PostDatePicker.PostValidation.Intervals.AddRange(new C1.Win.C1Input.ValueInterval[] {
            ((C1.Win.C1Input.ValueInterval)(resources.GetObject("PostDatePicker.PostValidation.Intervals")))});
            this.PostDatePicker.VisualStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.PostDatePicker.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.PostDatePicker.Validating += new System.ComponentModel.CancelEventHandler(this.dtmPostDate_Validating);
            // 
            // lblPurpose
            // 
            this.lblPurpose.ForeColor = System.Drawing.Color.Maroon;
            resources.ApplyResources(this.lblPurpose, "lblPurpose");
            this.lblPurpose.Name = "lblPurpose";
            // 
            // lblToLocation
            // 
            this.lblToLocation.ForeColor = System.Drawing.Color.Maroon;
            resources.ApplyResources(this.lblToLocation, "lblToLocation");
            this.lblToLocation.Name = "lblToLocation";
            // 
            // lblToBin
            // 
            this.lblToBin.ForeColor = System.Drawing.Color.Maroon;
            resources.ApplyResources(this.lblToBin, "lblToBin");
            this.lblToBin.Name = "lblToBin";
            // 
            // lblShift
            // 
            this.lblShift.ForeColor = System.Drawing.Color.Maroon;
            resources.ApplyResources(this.lblShift, "lblShift");
            this.lblShift.Name = "lblShift";
            // 
            // lblFromBin
            // 
            resources.ApplyResources(this.lblFromBin, "lblFromBin");
            this.lblFromBin.Name = "lblFromBin";
            // 
            // lblFromLoc
            // 
            resources.ApplyResources(this.lblFromLoc, "lblFromLoc");
            this.lblFromLoc.Name = "lblFromLoc";
            // 
            // IssueDayPicker
            // 
            this.IssueDayPicker.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(177)))), ((int)(((byte)(187)))), ((int)(((byte)(214)))));
            resources.ApplyResources(this.IssueDayPicker, "IssueDayPicker");
            // 
            // 
            // 
            this.IssueDayPicker.Calendar.Font = ((System.Drawing.Font)(resources.GetObject("IssueDayPicker.Calendar.Font")));
            this.IssueDayPicker.Calendar.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("IssueDayPicker.Calendar.ImeMode")));
            this.IssueDayPicker.Calendar.ShowClearButton = false;
            this.IssueDayPicker.Calendar.VisualStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.IssueDayPicker.Calendar.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.IssueDayPicker.ErrorInfo.ShowErrorMessage = ((bool)(resources.GetObject("IssueDayPicker.ErrorInfo.ShowErrorMessage")));
            this.IssueDayPicker.Name = "IssueDayPicker";
            this.IssueDayPicker.PostValidation.Intervals.AddRange(new C1.Win.C1Input.ValueInterval[] {
            ((C1.Win.C1Input.ValueInterval)(resources.GetObject("IssueDayPicker.PostValidation.Intervals")))});
            this.IssueDayPicker.ReadOnly = true;
            this.IssueDayPicker.VisualStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.IssueDayPicker.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            // 
            // lblIssueDay
            // 
            this.lblIssueDay.ForeColor = System.Drawing.Color.Maroon;
            resources.ApplyResources(this.lblIssueDay, "lblIssueDay");
            this.lblIssueDay.Name = "lblIssueDay";
            // 
            // lblIssuePlan
            // 
            this.lblIssuePlan.ForeColor = System.Drawing.Color.Maroon;
            resources.ApplyResources(this.lblIssuePlan, "lblIssuePlan");
            this.lblIssuePlan.Name = "lblIssuePlan";
            // 
            // IssueNoButton
            // 
            resources.ApplyResources(this.IssueNoButton, "IssueNoButton");
            this.IssueNoButton.Name = "IssueNoButton";
            this.IssueNoButton.UseVisualStyleBackColor = true;
            this.IssueNoButton.VisualStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.IssueNoButton.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.IssueNoButton.Click += new System.EventHandler(this.btnFindIssueNo_Click);
            // 
            // IssueNoText
            // 
            this.IssueNoText.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(177)))), ((int)(((byte)(187)))), ((int)(((byte)(214)))));
            resources.ApplyResources(this.IssueNoText, "IssueNoText");
            this.IssueNoText.Name = "IssueNoText";
            this.IssueNoText.TextDetached = true;
            this.IssueNoText.VisualStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.IssueNoText.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.IssueNoText.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtIssueNo_KeyDown);
            this.IssueNoText.Validating += new System.ComponentModel.CancelEventHandler(this.txtIssueNo_Validating);
            // 
            // MasterLocationText
            // 
            this.MasterLocationText.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(177)))), ((int)(((byte)(187)))), ((int)(((byte)(214)))));
            resources.ApplyResources(this.MasterLocationText, "MasterLocationText");
            this.MasterLocationText.Name = "MasterLocationText";
            this.MasterLocationText.TextDetached = true;
            this.MasterLocationText.VisualStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.MasterLocationText.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.MasterLocationText.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtMasLoc_KeyDown);
            this.MasterLocationText.Validating += new System.ComponentModel.CancelEventHandler(this.txtMasLoc_Validating);
            // 
            // MasterLocationButton
            // 
            resources.ApplyResources(this.MasterLocationButton, "MasterLocationButton");
            this.MasterLocationButton.Name = "MasterLocationButton";
            this.MasterLocationButton.UseVisualStyleBackColor = true;
            this.MasterLocationButton.VisualStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.MasterLocationButton.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.MasterLocationButton.Click += new System.EventHandler(this.btnFindMasLoc_Click);
            // 
            // LocationText
            // 
            this.LocationText.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(177)))), ((int)(((byte)(187)))), ((int)(((byte)(214)))));
            resources.ApplyResources(this.LocationText, "LocationText");
            this.LocationText.Name = "LocationText";
            this.LocationText.TextDetached = true;
            this.LocationText.VisualStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.LocationText.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.LocationText.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtToLocation_KeyDown);
            this.LocationText.Validating += new System.ComponentModel.CancelEventHandler(this.txtToLocation_Validating);
            // 
            // LocationButton
            // 
            resources.ApplyResources(this.LocationButton, "LocationButton");
            this.LocationButton.Name = "LocationButton";
            this.LocationButton.UseVisualStyleBackColor = true;
            this.LocationButton.VisualStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.LocationButton.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.LocationButton.Click += new System.EventHandler(this.btnToLocation_Click);
            // 
            // BinText
            // 
            this.BinText.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(177)))), ((int)(((byte)(187)))), ((int)(((byte)(214)))));
            resources.ApplyResources(this.BinText, "BinText");
            this.BinText.Name = "BinText";
            this.BinText.TextDetached = true;
            this.BinText.VisualStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.BinText.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.BinText.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtToBin_KeyDown);
            this.BinText.Validating += new System.ComponentModel.CancelEventHandler(this.txtToBin_Validating);
            // 
            // BinButton
            // 
            resources.ApplyResources(this.BinButton, "BinButton");
            this.BinButton.Name = "BinButton";
            this.BinButton.UseVisualStyleBackColor = true;
            this.BinButton.VisualStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.BinButton.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.BinButton.Click += new System.EventHandler(this.btnToBin_Click);
            // 
            // PurposeText
            // 
            this.PurposeText.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(177)))), ((int)(((byte)(187)))), ((int)(((byte)(214)))));
            resources.ApplyResources(this.PurposeText, "PurposeText");
            this.PurposeText.Name = "PurposeText";
            this.PurposeText.TextDetached = true;
            this.PurposeText.VisualStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.PurposeText.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.PurposeText.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPurpose_KeyDown);
            this.PurposeText.Validating += new System.ComponentModel.CancelEventHandler(this.txtPurpose_Validating);
            // 
            // PurposeButton
            // 
            resources.ApplyResources(this.PurposeButton, "PurposeButton");
            this.PurposeButton.Name = "PurposeButton";
            this.PurposeButton.UseVisualStyleBackColor = true;
            this.PurposeButton.VisualStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.PurposeButton.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.PurposeButton.Click += new System.EventHandler(this.btnPurpose_Click);
            // 
            // ShiftText
            // 
            this.ShiftText.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(177)))), ((int)(((byte)(187)))), ((int)(((byte)(214)))));
            resources.ApplyResources(this.ShiftText, "ShiftText");
            this.ShiftText.Name = "ShiftText";
            this.ShiftText.TextDetached = true;
            this.ShiftText.VisualStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.ShiftText.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.ShiftText.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtShift_KeyDown);
            this.ShiftText.Validating += new System.ComponentModel.CancelEventHandler(this.txtShift_Validating);
            // 
            // ShiftButton
            // 
            resources.ApplyResources(this.ShiftButton, "ShiftButton");
            this.ShiftButton.Name = "ShiftButton";
            this.ShiftButton.UseVisualStyleBackColor = true;
            this.ShiftButton.VisualStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.ShiftButton.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.ShiftButton.Click += new System.EventHandler(this.btnShift_Click);
            // 
            // PrintButton
            // 
            resources.ApplyResources(this.PrintButton, "PrintButton");
            this.PrintButton.Name = "PrintButton";
            this.PrintButton.UseVisualStyleBackColor = true;
            this.PrintButton.VisualStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.PrintButton.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.PrintButton.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // SaveButton
            // 
            resources.ApplyResources(this.SaveButton, "SaveButton");
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.VisualStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.SaveButton.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.SaveButton.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // DeleteButton
            // 
            resources.ApplyResources(this.DeleteButton, "DeleteButton");
            this.DeleteButton.Name = "DeleteButton";
            this.DeleteButton.UseVisualStyleBackColor = true;
            this.DeleteButton.VisualStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.DeleteButton.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.DeleteButton.Click += new System.EventHandler(this.btnDelete_Click);
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
            // CloseButton
            // 
            resources.ApplyResources(this.CloseButton, "CloseButton");
            this.CloseButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.UseVisualStyleBackColor = true;
            this.CloseButton.VisualStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.CloseButton.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.CloseButton.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // HelpButton
            // 
            resources.ApplyResources(this.HelpButton, "HelpButton");
            this.HelpButton.Name = "HelpButton";
            this.HelpButton.UseVisualStyleBackColor = true;
            this.HelpButton.VisualStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.HelpButton.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.HelpButton.Click += new System.EventHandler(this.btnHelp_Click);
            // 
            // btnPrintConfiguration
            // 
            resources.ApplyResources(this.btnPrintConfiguration, "btnPrintConfiguration");
            this.btnPrintConfiguration.Name = "btnPrintConfiguration";
            this.btnPrintConfiguration.UseVisualStyleBackColor = true;
            this.btnPrintConfiguration.VisualStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.btnPrintConfiguration.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            // 
            // SelectComponenButton
            // 
            resources.ApplyResources(this.SelectComponenButton, "SelectComponenButton");
            this.SelectComponenButton.Name = "SelectComponenButton";
            this.SelectComponenButton.UseVisualStyleBackColor = true;
            this.SelectComponenButton.VisualStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.SelectComponenButton.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.SelectComponenButton.Click += new System.EventHandler(this.btnSelectWO_Click);
            // 
            // MultiWOIssueMaterial
            // 
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.CloseButton);
            this.Controls.Add(this.HelpButton);
            this.Controls.Add(this.btnPrintConfiguration);
            this.Controls.Add(this.PrintButton);
            this.Controls.Add(this.SelectComponenButton);
            this.Controls.Add(this.SaveButton);
            this.Controls.Add(this.DeleteButton);
            this.Controls.Add(this.AddButton);
            this.Controls.Add(this.ShiftButton);
            this.Controls.Add(this.PurposeButton);
            this.Controls.Add(this.BinButton);
            this.Controls.Add(this.LocationButton);
            this.Controls.Add(this.MasterLocationButton);
            this.Controls.Add(this.IssueNoButton);
            this.Controls.Add(this.ShiftText);
            this.Controls.Add(this.PurposeText);
            this.Controls.Add(this.BinText);
            this.Controls.Add(this.LocationText);
            this.Controls.Add(this.MasterLocationText);
            this.Controls.Add(this.IssueNoText);
            this.Controls.Add(this.lblIssuePlan);
            this.Controls.Add(this.IssueDayPicker);
            this.Controls.Add(this.lblIssueDay);
            this.Controls.Add(this.lblShift);
            this.Controls.Add(this.DetailGrid);
            this.Controls.Add(this.lblToBin);
            this.Controls.Add(this.lblToLocation);
            this.Controls.Add(this.lblPurpose);
            this.Controls.Add(this.PostDatePicker);
            this.Controls.Add(this.CCNCombo);
            this.Controls.Add(this.lblCCN);
            this.Controls.Add(this.lblPostDate);
            this.Controls.Add(this.lblMasLoc);
            this.Controls.Add(this.lblIssueNo);
            this.Controls.Add(this.lblFromBin);
            this.Controls.Add(this.lblFromLoc);
            this.KeyPreview = true;
            this.Name = "MultiWOIssueMaterial";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Closing += new System.ComponentModel.CancelEventHandler(this.MultiWOIssueMaterial_Closing);
            this.Load += new System.EventHandler(this.MultiWOIssueMaterial_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DetailGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CCNCombo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PostDatePicker)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.IssueDayPicker)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.IssueNoText)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MasterLocationText)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LocationText)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BinText)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PurposeText)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ShiftText)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        #region Properties
        public EnumAction FormMode
        {
            get { return mFormMode; }
            set { mFormMode = value; }
        }

        #endregion Properties

        #region Build & Show Reports: Added by Tuan TQ

        /// <summary>
        /// Build and show Issuance Slip Report
        /// </summary>
        /// <Author> Tuan TQ, 05 Dec, 2005</Author>
        private void ShowIssuanceSlipReport(object sender, System.EventArgs e)
        {

        }


        /// <summary>
        /// Build and show Other Issuance Slip Report
        /// </summary>
        /// <Author> Tuan TQ, 02 Dec, 2005</Author>
        private void ShowOtherIssuanceSlipReport(object sender, System.EventArgs e)
        {

        }

        /// <summary>
        /// Build and show Delivery To Next Slip Report
        /// </summary>
        /// <Author> Tuan TQ, 01 Dec, 2005</Author>
        private void ShowDelivery2NextSlipReport(object sender, System.EventArgs e)
        {

        }

        #endregion Issuance Report: Added by Tuan TQ

        #region Methods

        /// <summary>
        /// Load the data into the grid
        /// and then customize the interface for the grid
        /// Turn some columns to have button to open search form
        /// </summary>
        private void DisplayDetailData()
        {
            DetailGrid.DataSource = dstData.Tables[0];

            //restore the layout of the grid
            FormControlComponents.RestoreGridLayout(DetailGrid, dtStoreGridLayout);

            //align right for Line, Delivery Quantity, Delivery No
            DetailGrid.Splits[0].DisplayColumns[PRO_IssueMaterialDetailTable.COMMITQUANTITY_FLD].Style.HorizontalAlignment = AlignHorzEnum.Far;

            //Set the Delivery Quantity
            DetailGrid.Splits[0].DisplayColumns[HASBIN].Visible = false;
            DetailGrid.Splits[0].DisplayColumns[ITM_ProductTable.LOTCONTROL_FLD].Visible = false;

            DetailGrid.Splits[0].DisplayColumns[LOCATIONCODE_COL].Button = true;
            DetailGrid.Splits[0].DisplayColumns[BINCODE_COL].Button = true;

            DetailGrid.Splits[0].DisplayColumns[PRO_IssueMaterialDetailTable.LOT_FLD].Button = true;

            //align the number column
            DetailGrid.Splits[0].DisplayColumns[PRO_IssueMaterialDetailTable.LINE_FLD].Style.HorizontalAlignment = AlignHorzEnum.Far;
            DetailGrid.Splits[0].DisplayColumns[PRO_IssueMaterialDetailTable.COMMITQUANTITY_FLD].Style.HorizontalAlignment = AlignHorzEnum.Far;
            DetailGrid.Splits[0].DisplayColumns[Constants.AVAILABLE_QTY_COL].Style.HorizontalAlignment = AlignHorzEnum.Far;
            DetailGrid.Splits[0].DisplayColumns[Constants.COMMITED_QTY_COL].Style.HorizontalAlignment = AlignHorzEnum.Far;
            DetailGrid.Splits[0].DisplayColumns[Constants.NEEDED_QTY_COL].Style.HorizontalAlignment = AlignHorzEnum.Far;
            DetailGrid.Splits[0].DisplayColumns[PRO_IssueMaterialDetailTable.BOMQUANTITY_FLD].Style.HorizontalAlignment = AlignHorzEnum.Far;

            //Set the number format for the number column
            DetailGrid.Columns[PRO_IssueMaterialDetailTable.COMMITQUANTITY_FLD].NumberFormat = Constants.DECIMAL_NUMBERFORMAT;
            DetailGrid.Columns[Constants.AVAILABLE_QTY_COL].NumberFormat = Constants.DECIMAL_NUMBERFORMAT;
            DetailGrid.Columns[Constants.COMMITED_QTY_COL].NumberFormat = Constants.DECIMAL_NUMBERFORMAT;
            DetailGrid.Columns[Constants.NEEDED_QTY_COL].NumberFormat = Constants.DECIMAL_NUMBERFORMAT;
            DetailGrid.Columns[PRO_IssueMaterialDetailTable.BOMQUANTITY_FLD].NumberFormat = Constants.DECIMAL_LONG_FORMAT;

            DetailGrid.Columns[PRO_IssueMaterialDetailTable.QASTATUS_FLD].ValueItems.Presentation = PresentationEnum.ComboBox;
            DetailGrid.Columns[PRO_IssueMaterialDetailTable.QASTATUS_FLD].ValueItems.Translate = true;
            DetailGrid.Columns[PRO_IssueMaterialDetailTable.QASTATUS_FLD].ValueItems.Validate = true;

            //lock several column
            DetailGrid.Splits[0].DisplayColumns[Constants.AVAILABLE_QTY_COL].Locked = true;
            DetailGrid.Splits[0].DisplayColumns[Constants.COMMITED_QTY_COL].Locked = true;
            DetailGrid.Splits[0].DisplayColumns[Constants.NEEDED_QTY_COL].Locked = true;
            DetailGrid.Splits[0].DisplayColumns[PRO_IssueMaterialDetailTable.COMMITQUANTITY_FLD].Locked = false;
            DetailGrid.Splits[0].DisplayColumns[UNITCODE_COL].Locked = true;
            DetailGrid.Splits[0].DisplayColumns[PRO_IssueMaterialDetailTable.QASTATUS_FLD].Locked = true;
            DetailGrid.Splits[0].DisplayColumns[PRO_IssueMaterialDetailTable.LINE_FLD].Locked = true;
            DetailGrid.Splits[0].DisplayColumns[PRO_WorkOrderMasterTable.TABLE_NAME + PRO_WorkOrderMasterTable.WORKORDERNO_FLD].Locked = true;
            DetailGrid.Splits[0].DisplayColumns[PRO_WorkOrderDetailTable.TABLE_NAME + PRO_WorkOrderDetailTable.LINE_FLD].Locked = true;
            DetailGrid.Splits[0].DisplayColumns[ITM_ProductTable.TABLE_NAME + ITM_ProductTable.CODE_FLD].Locked = true;
            DetailGrid.Splits[0].DisplayColumns[ITM_ProductTable.TABLE_NAME + ITM_ProductTable.DESCRIPTION_FLD].Locked = true;
            DetailGrid.Splits[0].DisplayColumns[ITM_ProductTable.TABLE_NAME + ITM_ProductTable.REVISION_FLD].Locked = true;

            //HACK: added by Tuan TQ 23 May, 2006
            DetailGrid.Splits[0].DisplayColumns[ITM_CategoryTable.TABLE_NAME + ITM_CategoryTable.CODE_FLD].Locked = true;
            DetailGrid.Splits[0].DisplayColumns[MST_PartyTable.TABLE_NAME + MST_PartyTable.CODE_FLD].Locked = true;
            DetailGrid.Splits[0].DisplayColumns[MST_PartyTable.TABLE_NAME + MST_PartyTable.NAME_FLD].Locked = true;
            DetailGrid.Splits[0].DisplayColumns[PRO_IssueMaterialDetailTable.BOMQUANTITY_FLD].Locked = true;
            //End hack
        }


        /// <summary>
        /// This method is used to display the detail data into the grid
        /// First it will get the data from database
        /// Restore the grid layout
        /// Set the column layout, format , and load default data into grid
        /// </summary>
        /// <param name="pintMasterID"></param>		
        private void LoadDetailData(int pintMasterID)
        {
            MultiWOIssueMaterialBO boMultiWOIssueMaterialBO = new MultiWOIssueMaterialBO();
            //Get the detail data
            dstData = boMultiWOIssueMaterialBO.GetDetailData(pintMasterID);
            dstData.Tables[0].Columns.Add(HASBIN);
            dstData.Tables[0].Columns.Add(ITM_ProductTable.LOTCONTROL_FLD);

            //after get the detail data, will display it
            DisplayDetailData();
        }

        /// <summary>
        /// This method is used to lock the controls on form
        /// Based on the parameter input 
        /// </summary>
        /// <param name="blnLock"></param>
        private void LockForm(bool blnLock)
        {
            PostDatePicker.ReadOnly = blnLock;
            CCNCombo.ReadOnly = blnLock;
            IssueNoText.ReadOnly = blnLock;
            IssueNoButton.Enabled = !blnLock;

            MasterLocationText.ReadOnly = blnLock;
            MasterLocationButton.Enabled = !blnLock;

            DetailGrid.AllowUpdate = !blnLock;
            DetailGrid.AllowDelete = !blnLock;
        }

        /// <summary>
        /// This method is used to enable or disable all button on form
        /// based on the current Form Mode
        /// For the Default mode, we have to consider the right for each button on form
        /// to enable or disable that button
        /// </summary>
        private void EnableDisableButtons()
        {
            switch (mFormMode)
            {
                case EnumAction.Add:
                    IssueNoText.ReadOnly = false;
                    IssueNoButton.Enabled = false;
                    AddButton.Enabled = false;
                    btnPrintConfiguration.Enabled = PrintButton.Enabled = false;
                    SaveButton.Enabled = true;
                    SelectComponenButton.Enabled = true;
                    PurposeText.Enabled = true;
                    LocationText.Enabled = true;
                    LocationButton.Enabled = true;
                    PurposeButton.Enabled = true;
                    ShiftText.Enabled = true;
                    ShiftButton.Enabled = true;
                    DeleteButton.Enabled = false;
                    break;
                case EnumAction.Edit:
                    IssueNoText.ReadOnly = false;
                    IssueNoButton.Enabled = false;
                    //Disable Buttons
                    AddButton.Enabled = false;
                    btnPrintConfiguration.Enabled = PrintButton.Enabled = false;
                    SaveButton.Enabled = true;
                    SelectComponenButton.Enabled = true;
                    // HACK: Trada 27-10-2005
                    PurposeText.Enabled = true;
                    LocationText.Enabled = true;
                    LocationButton.Enabled = true;
                    PurposeButton.Enabled = true;
                    ShiftText.Enabled = true;
                    ShiftButton.Enabled = true;
                    // END: Trada 27-10-2005
                    DeleteButton.Enabled = false;
                    break;
                case EnumAction.Default:

                    IssueNoText.ReadOnly = false;
                    IssueNoButton.Enabled = true;
                    //Disable Buttons
                    SaveButton.Enabled = false;
                    SelectComponenButton.Enabled = false;
                    DeleteButton.Enabled = btnPrintConfiguration.Enabled = PrintButton.Enabled = (IssueNoText.Text.Trim() != string.Empty);
                    // HACK: Trada 27-10-2005
                    PurposeText.Enabled = false;
                    LocationText.Enabled = false;
                    LocationButton.Enabled = false;
                    BinText.Enabled = false;
                    BinButton.Enabled = false;
                    PurposeButton.Enabled = false;
                    ShiftText.Enabled = false;
                    ShiftButton.Enabled = false;
                    // END: Trada 27-10-2005

                    AddButton.Enabled = true;
                    break;
            }
        }

        /// <summary>
        /// This method is used to clear all data on form
        /// 
        /// </summary>
        private void ClearForm()
        {
            //general information
            PostDatePicker.Value = DBNull.Value;
            IssueDayPicker.Value = DBNull.Value;
            IssueNoText.Text = string.Empty;

            MasterLocationText.Text = string.Empty;
            LocationText.Text = string.Empty;
            BinText.Text = string.Empty;
            
            //clear data in the grid
            if (dstData != null && dstData.Tables.Count > 0)
                dstData.Tables[0].Rows.Clear();
            strLastValidMasLoc = string.Empty;
            strLastValidLocation = string.Empty;
            strLastValidBin = string.Empty;
        }

        /// <summary>
        /// After deleting each row in the grid,
        /// we need to reset its line number
        /// </summary>
        private void ChangeLineNumber()
        {
            int intGridRows = this.DetailGrid.RowCount;

            int i;
            for (i = 0; i < intGridRows; i++)
            {
                DetailGrid[i, PRO_IssueMaterialDetailTable.LINE_FLD] = i + 1;
            }
        }

        /// <summary>
        /// this method is used to get the max line number for the grid
        /// </summary>
        /// <returns></returns>
        private int GetTheNextLineNumber()
        {
            int intGridRows = this.DetailGrid.RowCount;
            if (intGridRows == 0)
            {
                return 1;
            }
            int intMax = 0;
            for (int i = 0; i < intGridRows; i++)
            {
                int intTmp;
                try
                {
                    intTmp = int.Parse(this.DetailGrid[i, PRO_IssueMaterialDetailTable.LINE_FLD].ToString());
                }
                catch
                {
                    intTmp = 0;
                }
                if (intMax < intTmp)
                {
                    intMax = intTmp;
                }
            }
            return intMax + 1;
        }

        /// <summary>
        /// Validate all the manadatory fields on form
        /// </summary>
        /// <returns></returns>
        private bool ValidateMandatoryControl()
        {
            if (voPRO_IssueMaterialMasterVO.MasterLocationID <= 0)
            {
                PCSMessageBox.Show(ErrorCode.MSG_LABORTIME_SELECTMASLOC, MessageBoxIcon.Warning);
                MasterLocationText.Focus();
                return false;
            }

            if (CCNCombo.SelectedIndex < 0)
            {
                PCSMessageBox.Show(ErrorCode.MESSAGE_RGA_CCN, MessageBoxIcon.Warning);
                CCNCombo.Focus();
                return false;
            }

            if (PostDatePicker.Value == DBNull.Value || PostDatePicker.Text.Trim() == string.Empty)
            {
                PCSMessageBox.Show(ErrorCode.MESSAGE_RGV_POSTDATE, MessageBoxIcon.Warning);
                PostDatePicker.Focus();
                return false;
            }

            //check the PostDate in the current period
            if (!FormControlComponents.CheckDateInCurrentPeriod((DateTime)PostDatePicker.Value))
            {
                PCSMessageBox.Show(ErrorCode.MESSAGE_RGV_POSTDATE_PERIOD, MessageBoxIcon.Warning);
                PostDatePicker.Focus();
                return false;
            }

            //HACK: Tuan TQ. 12 Jan, 2006. 
            //Post date must be in the past
            var boUtils = new UtilsBO();
            if (((DateTime)PostDatePicker.Value) > boUtils.GetDBDate())
            {
                PCSMessageBox.Show(ErrorCode.MESSAGE_INV_TRANSACTION_CANNOT_IN_FUTURE, MessageBoxIcon.Exclamation);
                PostDatePicker.Focus();
                return false;
            }
            //End hack

            if (IssueNoText.Text.Trim() == string.Empty)
            {
                PCSMessageBox.Show(ErrorCode.MSG_LABORTIME_TRANSNO, MessageBoxIcon.Warning);
                IssueNoText.Focus();
                return false;
            }

            if (MasterLocationText.Text.Trim() == string.Empty)
            {
                PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Warning);
                MasterLocationText.Focus();
                return false;
            }

            if (LocationText.Text.Trim() == string.Empty)
            {
                PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Warning);
                LocationText.Focus();
                return false;
            }

            if (BinText.Text == string.Empty)
            {
                PCSMessageBox.Show(ErrorCode.MESSAGE_IV_ADJUSTMENT_PLS_INPUT_BIN);
                BinText.Focus();
                return false;
            }

            if (PurposeText.Text.Trim() == string.Empty)
            {
                PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Warning);
                PurposeText.Focus();
                return false;
            }

            if (ShiftText.Text.Trim() == string.Empty)
            {
                PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Warning);
                ShiftText.Focus();
                return false;
            }

            int intDetailRows = DetailGrid.RowCount;
            
            if (intDetailRows <= 0)
            {
                PCSMessageBox.Show(ErrorCode.MESSAGE_PLEASE_ENTER_DETAIL_INFOR, MessageBoxIcon.Warning);

                DetailGrid.Focus();
                DetailGrid.Row = 0;
                DetailGrid.Col = dstData.Tables[0].Columns.IndexOf(WORKORDERNO_COL);

                return false;
            }
            //Check postdate in configuration
            if (!FormControlComponents.CheckPostDateInConfiguration((DateTime)PostDatePicker.Value))
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Validate all the mandatory fields in the grid
        /// </summary>
        /// <returns></returns>
        private bool CheckManatoryColumnInGrid()
        {
            if ((dstData.Tables.Count <= 0) || (DetailGrid.RowCount <= 0)) return false;

            int intTotalRow = DetailGrid.RowCount;

            for (int intRow = 0; intRow < intTotalRow; intRow++)
            {
                // set selected row first
                DetailGrid.Row = intRow;

                decimal decCommitQuantity;

                if (DetailGrid[intRow, PRO_IssueMaterialDetailTable.LOCATIONID_FLD] == DBNull.Value
                    || DetailGrid[intRow, PRO_IssueMaterialDetailTable.LOCATIONID_FLD].ToString().Trim() == string.Empty)
                {
                    PCSMessageBox.Show(ErrorCode.MSG_WOISSUE_MATERIAL_SELECT_LOCATION, MessageBoxIcon.Warning);
                    DetailGrid.Col = dstData.Tables[0].Columns.IndexOf(LOCATIONCODE_COL);
                    DetailGrid.Focus();
                    DetailGrid.Select();
                    return false;
                }

                if (DetailGrid[intRow, BINCODE_COL].ToString().Trim() == string.Empty)
                {
                    PCSMessageBox.Show(ErrorCode.MESSAGE_IV_ADJUSTMENT_PLS_INPUT_BIN, MessageBoxIcon.Warning);
                    DetailGrid.Col = DetailGrid.Splits[0].DisplayColumns.IndexOf(DetailGrid.Splits[0].DisplayColumns[BINCODE_COL]);
                    DetailGrid.Focus();
                    DetailGrid.Select();
                    return false;
                }

                //HACK: added by Tuan TQ. 23 Jan, 2006. From Bin must be diffirent from To Bin
                if (DetailGrid[intRow, PRO_IssueMaterialDetailTable.BINID_FLD].Equals(BinText.Tag)
                    && (BinText.Tag.ToString() != string.Empty))
                {
                    PCSMessageBox.Show(ErrorCode.MESSAGE_LOCTOLOC_DIFFERENCE_BIN, MessageBoxIcon.Warning);
                    DetailGrid.Col = DetailGrid.Splits[0].DisplayColumns.IndexOf(DetailGrid.Splits[0].DisplayColumns[BINCODE_COL]);
                    DetailGrid.Focus();
                    DetailGrid.Select();
                    return false;
                }
                //End hack					

                // lot control
                if (DetailGrid[intRow, ITM_ProductTable.LOTCONTROL_FLD].ToString() == 1.ToString())
                {
                    if (DetailGrid[intRow, PRO_IssueMaterialDetailTable.LOT_FLD].ToString() == string.Empty)
                    {
                        PCSMessageBox.Show(ErrorCode.MESSAGE_LOCTOLOC_SELECT_LOT, MessageBoxIcon.Warning);
                        DetailGrid.Col = DetailGrid.Splits[0].DisplayColumns.IndexOf(DetailGrid.Splits[0].DisplayColumns[PRO_IssueMaterialDetailTable.LOT_FLD]);
                        DetailGrid.Focus();
                        DetailGrid.Select();
                        return false;
                    }
                }

                if (DetailGrid[intRow, PRO_IssueMaterialDetailTable.PRODUCTID_FLD] == DBNull.Value
                    || DetailGrid[intRow, PRO_IssueMaterialDetailTable.PRODUCTID_FLD].ToString().Trim() == string.Empty)
                {
                    PCSMessageBox.Show(ErrorCode.MSG_WOISSUE_MATERIAL_SELECT_COMPONENT, MessageBoxIcon.Warning);
                    DetailGrid.Col = dstData.Tables[0].Columns.IndexOf(PRODUCTCODE_COL);
                    DetailGrid.Focus();
                    DetailGrid.Select();
                    return false;
                }

                try
                {
                    decCommitQuantity = decimal.Parse(DetailGrid[intRow, PRO_IssueMaterialDetailTable.COMMITQUANTITY_FLD].ToString());
                }
                catch
                {
                    PCSMessageBox.Show(ErrorCode.MESSAGE_INVALID_NUMERIC, MessageBoxIcon.Warning);
                    DetailGrid.Col = dstData.Tables[0].Columns.IndexOf(PRO_IssueMaterialDetailTable.COMMITQUANTITY_FLD);
                    DetailGrid.Focus();
                    DetailGrid.Select();
                    return false;
                }

                if (decCommitQuantity <= 0)
                {
                    PCSMessageBox.Show(ErrorCode.MSG_WOISSUE_MATERIAL_COMMIT_QTY_GREATER_THAN_ZERO, MessageBoxIcon.Warning);
                    DetailGrid.Col = dstData.Tables[0].Columns.IndexOf(PRO_IssueMaterialDetailTable.COMMITQUANTITY_FLD);
                    DetailGrid.Focus();
                    DetailGrid.Select();
                    return false;
                }

                #region check available quantity

                bool allowNegative = false;
                try
                {
                    allowNegative = (bool)DetailGrid[intRow, ITM_ProductTable.ALLOWNEGATIVEQTY_FLD];
                }
                catch{}
                if (!allowNegative)
                {
                    // check available quantity to commit
                    try
                    {
                        if (decimal.Parse(DetailGrid[intRow, AVAILABLE_QUANTITY].ToString()) <= 0)
                        {
                            PCSMessageBox.Show(ErrorCode.MESSAGE_AVAILABLE_QTY_MUST_GREATER_THAN_ZERO, MessageBoxIcon.Warning);
                            DetailGrid.Col = dstData.Tables[0].Columns.IndexOf(AVAILABLE_QUANTITY);
                            DetailGrid.Focus();
                            DetailGrid.Select();
                            return false;
                        }
                    }
                    catch
                    {
                        PCSMessageBox.Show(ErrorCode.MESSAGE_INVALID_NUMERIC, MessageBoxIcon.Warning);
                        DetailGrid.Col = dstData.Tables[0].Columns.IndexOf(AVAILABLE_QUANTITY);
                        DetailGrid.Focus();
                        DetailGrid.Select();
                        return false;
                    }

                    //count for total Commit Quantity at another rows
                    for (int i = 0; i < DetailGrid.RowCount; i++)
                    {
                        //Check if the row have same ProductID, LocationID and BinID with current row
                        if ((!DetailGrid[i, PRO_IssueMaterialDetailTable.BINID_FLD].Equals(DetailGrid[DetailGrid.Row, PRO_IssueMaterialDetailTable.BINID_FLD])) ||
                            (!DetailGrid[i, PRO_IssueMaterialDetailTable.LOCATIONID_FLD].Equals(DetailGrid[DetailGrid.Row, PRO_IssueMaterialDetailTable.LOCATIONID_FLD])) ||
                            (!DetailGrid[i, PRO_IssueMaterialDetailTable.PRODUCTID_FLD].Equals(DetailGrid[DetailGrid.Row, PRO_IssueMaterialDetailTable.PRODUCTID_FLD])) ||
                            (i == DetailGrid.Row))
                        {
                            continue;
                        }
                        try
                        {
                            decCommitQuantity += decimal.Parse(DetailGrid[i, PRO_IssueMaterialDetailTable.COMMITQUANTITY_FLD].ToString());
                        }
                        catch
                        {
                        }
                    }

                    //check the available quantity to commit
                    if (!CheckValidCommitQty(decCommitQuantity, string.Empty, string.Empty))
                    {
                        DetailGrid.Col = dstData.Tables[0].Columns.IndexOf(PRO_IssueMaterialDetailTable.COMMITQUANTITY_FLD);
                        DetailGrid.Focus();
                        DetailGrid.Select();
                        return false;
                    }
                }
                #endregion

                //Check if ToLoc = FromLoc
                if (voPRO_IssueMaterialMasterVO.ToLocationID == int.Parse(DetailGrid[intRow, PRO_IssueMaterialDetailTable.LOCATIONID_FLD].ToString()))
                {
                    var strParam = new string[2];
                    if (voPRO_IssueMaterialMasterVO.ToBinID != 0)
                    {
                        if (voPRO_IssueMaterialMasterVO.ToBinID == int.Parse(DetailGrid[intRow, PRO_IssueMaterialDetailTable.BINID_FLD].ToString()))
                        {
                            strParam[0] = lblToBin.Text;
                            strParam[1] = lblFromBin.Text;
                            PCSMessageBox.Show(ErrorCode.MESSAGE_INTERSECT_NOT_ALLOWED, MessageBoxIcon.Warning, strParam);
                            DetailGrid.Col = dstData.Tables[0].Columns.IndexOf(MST_BINTable.TABLE_NAME + MST_BINTable.CODE_FLD);
                            DetailGrid.Focus();
                            return false;
                        }
                    }
                    else
                    {
                        strParam[0] = lblToLocation.Text;
                        strParam[1] = lblFromLoc.Text;
                        PCSMessageBox.Show(ErrorCode.MESSAGE_INTERSECT_NOT_ALLOWED, MessageBoxIcon.Warning, strParam);
                        DetailGrid.Col = dstData.Tables[0].Columns.IndexOf(MST_LocationTable.TABLE_NAME + MST_LocationTable.CODE_FLD);
                        DetailGrid.Focus();
                        return false;
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// Assign value to the VO Master Object
        /// </summary>
        private void AssignValueToMaster()
        {
            voPRO_IssueMaterialMasterVO.CCNID = int.Parse(CCNCombo.SelectedValue.ToString());
            voPRO_IssueMaterialMasterVO.PostDate = (DateTime)PostDatePicker.Value;
            voPRO_IssueMaterialMasterVO.IssueNo = IssueNoText.Text.Trim();
            voPRO_IssueMaterialMasterVO.ShiftID = int.Parse(ShiftText.Tag.ToString());

            voPRO_IssueMaterialMasterVO.ToLocationID = int.Parse(LocationText.Tag.ToString());
            voPRO_IssueMaterialMasterVO.ToBinID = int.Parse(BinText.Tag.ToString());
            voPRO_IssueMaterialMasterVO.IssuePurposeID = int.Parse(PurposeText.Tag.ToString());
        }

        private bool SaveData()
        {
            AssignValueToMaster();

            // synchronyze data
            FormControlComponents.SynchronyGridData(DetailGrid);
            var boMultiWOIssueMaterialBO = new MultiWOIssueMaterialBO();

            switch (mFormMode)
            {
                case EnumAction.Add:
                    voPRO_IssueMaterialMasterVO.IssueMaterialMasterID = boMultiWOIssueMaterialBO.AddAndReturnId(voPRO_IssueMaterialMasterVO, dstData);
                    break;
            }

            return true;
        }

        /// <summary>
        /// This method is used to clear all the associated fields with master Location
        /// Work Order
        /// </summary>
        private void ChangeMasterLocation()
        {
            // clear location and bin data
            LocationText.Text = string.Empty;
            LocationText.Tag = 0;
            BinText.Text = string.Empty;
            BinText.Tag = 0;
            // clear location and bin in grid
            if (dstData != null && dstData.Tables.Count > 0)
            {
                foreach (DataRow drowData in
                    dstData.Tables[0].Rows.Cast<DataRow>().Where(drowData => drowData.RowState != DataRowState.Deleted))
                {
                    drowData[Constants.AVAILABLE_QTY_COL] = DBNull.Value;
                    drowData[MST_LocationTable.LOCATIONID_FLD] = DBNull.Value;
                    drowData[MST_BINTable.BINID_FLD] = DBNull.Value;
                    drowData[LOCATIONCODE_COL] = string.Empty;
                    drowData[BINCODE_COL] = string.Empty;
                }
            }
        }

        /// <summary>
        /// Load data from search result to control
        /// </summary>
        /// <param name="drData">Search result row</param>
        private void LoadData(DataRow drData)
        {
            if (drData == null)
            {
                ClearForm();
                EnableDisableButtons();
                return;
            }

            voPRO_IssueMaterialMasterVO = new PRO_IssueMaterialMasterVO();
            voPRO_IssueMaterialMasterVO.CCNID = int.Parse(drData[PRO_IssueMaterialMasterTable.CCNID_FLD].ToString());
            voPRO_IssueMaterialMasterVO.IssueMaterialMasterID = int.Parse(drData[PRO_IssueMaterialMasterTable.ISSUEMATERIALMASTERID_FLD].ToString());
            voPRO_IssueMaterialMasterVO.IssueNo = drData[PRO_IssueMaterialMasterTable.ISSUENO_FLD].ToString();
            voPRO_IssueMaterialMasterVO.PostDate = (DateTime)drData[PRO_IssueMaterialMasterTable.POSTDATE_FLD];
            voPRO_IssueMaterialMasterVO.MasterLocationID = int.Parse(drData[PRO_IssueMaterialMasterTable.MASTERLOCATIONID_FLD].ToString());
            voPRO_IssueMaterialMasterVO.IssuePurposeID = int.Parse(drData[PRO_IssueMaterialMasterTable.ISSUEPURPOSEID_FLD].ToString());
            voPRO_IssueMaterialMasterVO.ToLocationID = int.Parse(drData[PRO_IssueMaterialMasterTable.TOLOCATIONID_FLD].ToString());
            voPRO_IssueMaterialMasterVO.ShiftID = int.Parse(drData[PRO_IssueMaterialMasterTable.SHIFTID_FLD].ToString());

            //display the master information
            //1.CCN ID
            CCNCombo.SelectedValue = drData[PRO_IssueMaterialMasterTable.CCNID_FLD];
            //2.Post Date
            PostDatePicker.Value = drData[PRO_IssueMaterialMasterTable.POSTDATE_FLD];
            //3.Transno
            IssueNoText.Text = drData[PRO_IssueMaterialMasterTable.ISSUENO_FLD].ToString();

            // Get Master information
            var boMultiWOIssueMaterialBO = new MultiWOIssueMaterialBO();
            DataTable dtbMasterIssue = boMultiWOIssueMaterialBO.GetMasterIssue(voPRO_IssueMaterialMasterVO.IssueMaterialMasterID);

            //4.Master Location Code
            //first get the MasterLocation VO
            MasterLocationText.Text = dtbMasterIssue.Rows[0]["MasLoc"].ToString();
            voPRO_IssueMaterialMasterVO.MasterLocationID = Convert.ToInt32(dtbMasterIssue.Rows[0]["MasterLocationID"]);
            //5.ToLocation
            LocationText.Text = dtbMasterIssue.Rows[0]["Location"].ToString();
            //6.Purpose
            PurposeText.Text = dtbMasterIssue.Rows[0]["ISSUEPURPOSE"].ToString();
            //7.ToBin
            if ((drData[PRO_IssueMaterialMasterTable.TOBINID_FLD] != DBNull.Value) && (drData[PRO_IssueMaterialMasterTable.TOBINID_FLD].ToString() != 0.ToString()))
            {
                voPRO_IssueMaterialMasterVO.ToBinID = int.Parse(drData[PRO_IssueMaterialMasterTable.TOBINID_FLD].ToString());
                BinText.Text = dtbMasterIssue.Rows[0]["BIN"].ToString();
            }
            //8.Shift
            ShiftText.Text = dtbMasterIssue.Rows[0]["ShiftDesc"].ToString();
            ShiftText.Tag = Convert.ToInt32(dtbMasterIssue.Rows[0]["ShiftID"]);

            //Display the detail into the grid
            LoadDetailData(voPRO_IssueMaterialMasterVO.IssueMaterialMasterID);

            EnableDisableButtons();
        }

        /// <summary>
        /// update the value to the other associated column after searching
        /// </summary>
        /// <param name="strColumnName"></param>
        /// <param name="strColumnValue"></param>
        /// <param name="drvResult"></param>
        /// <param name="blnButtonClick"></param>
        private void UpdateValueAfterSearch(string strColumnName, string strColumnValue, DataRowView drvResult, bool blnButtonClick)
        {
            switch (strColumnName)
            {
                case LOCATIONCODE_COL:
                    if (drvResult != null)
                    {
                        //update the associated column
                        DetailGrid.Columns[PRO_IssueMaterialDetailTable.LOCATIONID_FLD].Value = drvResult[MST_LocationTable.LOCATIONID_FLD];
                        DetailGrid.Columns[LOCATIONCODE_COL].Value = drvResult[MST_LocationTable.CODE_FLD];

                        //Bin 
                        DetailGrid.Columns[PRO_IssueMaterialDetailTable.BINID_FLD].Value = string.Empty;
                        DetailGrid.Columns[BINCODE_COL].Value = string.Empty;

                        //other associated quantity column
                        DetailGrid.Columns[Constants.AVAILABLE_QTY_COL].Value = string.Empty;
                        DetailGrid.Columns[Constants.COMMITED_QTY_COL].Value = string.Empty;
                        DetailGrid.Columns[Constants.NEEDED_QTY_COL].Value = string.Empty;
                        //Disable Bin column if this Location hasn't Bin
                        DetailGrid.Columns[HASBIN].Value = bool.Parse(drvResult[MST_LocationTable.BIN_FLD].ToString()) ? 1 : 0;

                    }
                    else if (strColumnValue == string.Empty && !blnButtonClick)
                    {
                        //clear the associated column
                        DetailGrid.Columns[PRO_IssueMaterialDetailTable.LOCATIONID_FLD].Value = string.Empty;
                        DetailGrid.Columns[LOCATIONCODE_COL].Value = string.Empty;

                        //Bin 
                        DetailGrid.Columns[PRO_IssueMaterialDetailTable.BINID_FLD].Value = string.Empty;
                        DetailGrid.Columns[BINCODE_COL].Value = string.Empty;

                        //other associated quantity column
                        DetailGrid.Columns[Constants.AVAILABLE_QTY_COL].Value = string.Empty;
                        DetailGrid.Columns[Constants.COMMITED_QTY_COL].Value = string.Empty;
                        DetailGrid.Columns[Constants.NEEDED_QTY_COL].Value = string.Empty;
                    }

                    break;

                case "MST_BinCode":
                    if (drvResult != null)
                    {
                        //HACK: added by Tuan TQ. 23 Jan, 2006. From Bin must be diffirent from To Bin
                        if (drvResult[MST_BINTable.BINID_FLD].Equals(BinText.Tag) && (BinText.Text != string.Empty))
                        {
                            PCSMessageBox.Show(ErrorCode.MESSAGE_LOCTOLOC_DIFFERENCE_BIN, MessageBoxIcon.Warning);
                            DetailGrid.Col = DetailGrid.Splits[0].DisplayColumns.IndexOf(DetailGrid.Splits[0].DisplayColumns[BINCODE_COL]);
                            DetailGrid.Focus();
                            DetailGrid.Select();
                            return;
                        }
                        //End hack

                        //update the associated column
                        DetailGrid[DetailGrid.Row, PRO_IssueMaterialDetailTable.BINID_FLD] = drvResult[MST_BINTable.BINID_FLD];
                        DetailGrid[DetailGrid.Row, BINCODE_COL] = drvResult[MST_BINTable.CODE_FLD];
                        var objBO = new MultiWOIssueMaterialBO();
                        int iProductID = 0;
                        int iCCNID = SystemProperty.CCNID;
                        int iLocation = 0;
                        int iBinID = 0;
                        if (drvResult[MST_BINTable.BINID_FLD] != DBNull.Value)
                        {
                            iBinID = Convert.ToInt32(drvResult[MST_BINTable.BINID_FLD]);
                        }
                        if (DetailGrid[DetailGrid.Row, MST_LocationTable.LOCATIONID_FLD] != DBNull.Value)
                        {
                            iLocation = Convert.ToInt32(DetailGrid[DetailGrid.Row, MST_LocationTable.LOCATIONID_FLD]);
                        }
                        try
                        {
                            iProductID = Convert.ToInt32(DetailGrid[DetailGrid.Row, ITM_ProductTable.PRODUCTID_FLD]);
                        }
                        catch
                        { }
                        //other associated quantity column
                        decimal dOHQuantity = 0;
                        dOHQuantity = objBO.GetOHQuantity(iCCNID, iBinID, iLocation, iProductID);
                        DetailGrid.Columns[Constants.AVAILABLE_QTY_COL].Value = dOHQuantity;
                        DetailGrid.Columns[Constants.COMMITED_QTY_COL].Value = string.Empty;
                        DetailGrid.Columns[Constants.NEEDED_QTY_COL].Value = string.Empty;


                    }
                    else if (strColumnValue == string.Empty && !blnButtonClick)
                    {
                        //clear the associated column
                        DetailGrid.Columns[PRO_IssueMaterialDetailTable.BINID_FLD].Value = string.Empty;
                        DetailGrid.Columns[BINCODE_COL].Value = string.Empty;

                        //other associated quantity column
                        DetailGrid.Columns[Constants.AVAILABLE_QTY_COL].Value = string.Empty;
                        DetailGrid.Columns[Constants.COMMITED_QTY_COL].Value = string.Empty;
                        DetailGrid.Columns[Constants.NEEDED_QTY_COL].Value = string.Empty;
                    }
                    break;
            }


            //re-calculate quantity columns
            for (int i = DetailGrid.Row - 1; i >= 0; i--)
            {
                if ((DetailGrid[i, ITM_ProductTable.PRODUCTID_FLD].ToString() == DetailGrid[DetailGrid.Row, ITM_ProductTable.PRODUCTID_FLD].ToString())
                    && (DetailGrid[i, MST_LocationTable.LOCATIONID_FLD].ToString() == DetailGrid[DetailGrid.Row, MST_LocationTable.LOCATIONID_FLD].ToString())
                    && (DetailGrid[i, MST_BINTable.BINID_FLD].ToString() == DetailGrid[DetailGrid.Row, MST_BINTable.BINID_FLD].ToString()))
                {
                    if (DetailGrid[i, AVAILABLE_QUANTITY].ToString() != string.Empty)
                        DetailGrid.Columns[AVAILABLE_QUANTITY].Value = Convert.ToDecimal(DetailGrid[i, AVAILABLE_QUANTITY]);
                    break;
                }
            }

            DetailGrid.RefreshRow();
        }

        /// <summary>
        /// This method is used to Open a search form
        /// in case user clicks at the button or press key F4
        /// This method will call the OpenSearchForm 
        /// and this OpenSearch only display the search form
        /// </summary>
        /// <param name="strColumnName"></param>
        /// <param name="strColumnValue"></param>
        private void DisplaySearchForm(string strColumnName, string strColumnValue, bool pblnSearchOnly)
        {
            var drvResult = CallOpenSearchForm(strColumnName, strColumnValue, pblnSearchOnly);
            if (drvResult == null)
            {
                return;
            }
            UpdateValueAfterSearch(strColumnName, strColumnValue, drvResult, true);
        }

        /// <summary>
        /// This method will display a search form
        /// based on each column
        /// we have to define the Table Name, Filter Field Name, Filter Field value
        /// and the condition for each case .
        /// Based on the bool variable ==> it will search the current value or not
        /// If the blnSearchOnly = true ==> first it will search in the database ==> if it is found ==>
        /// it will return the result
        /// </summary>
        /// <param name="strColumnName"></param>
        /// <param name="strColumValue"></param>
        /// <returns></returns>
        private DataRowView CallOpenSearchForm(string strColumnName, string strColumValue, bool pblnAlwayShowDialog)
        {
            Hashtable hashSearchCondition = null;

            string strSearchTableName = string.Empty;
            string strSearchFieldName = string.Empty;
            string strSearchFieldValue = strColumValue;

            switch (strColumnName)
            {
                case MST_MasterLocationTable.TABLE_NAME + MST_MasterLocationTable.CODE_FLD:
                    strSearchTableName = MST_MasterLocationTable.TABLE_NAME;
                    strSearchFieldName = MST_MasterLocationTable.CODE_FLD;
                    hashSearchCondition = new Hashtable();
                    hashSearchCondition.Add(MST_MasterLocationTable.CCNID_FLD, CCNCombo.SelectedValue);

                    break;

                case LOCATIONCODE_COL:
                    if (voPRO_IssueMaterialMasterVO.MasterLocationID <= 0)
                    {
                        PCSMessageBox.Show(ErrorCode.MSG_LABORTIME_SELECTMASLOC, MessageBoxIcon.Warning);
                        MasterLocationText.Focus();
                        return null;
                    }
                    strSearchTableName = MST_LocationTable.TABLE_NAME;
                    strSearchFieldName = MST_LocationTable.CODE_FLD;
                    hashSearchCondition = new Hashtable();
                    hashSearchCondition.Add(MST_LocationTable.MASTERLOCATIONID_FLD, voPRO_IssueMaterialMasterVO.MasterLocationID);
                    break;

                case "MST_BinCode":
                    if (DetailGrid[DetailGrid.Row, HASBIN].ToString() == 1.ToString())
                    {
                        if (DetailGrid[DetailGrid.Row, PRO_IssueMaterialDetailTable.LOCATIONID_FLD] == DBNull.Value
                            || DetailGrid[DetailGrid.Row, PRO_IssueMaterialDetailTable.LOCATIONID_FLD].ToString().Trim() == string.Empty)
                        {
                            PCSMessageBox.Show(ErrorCode.MSG_WOISSUE_MATERIAL_SELECT_LOCATION_FIRST, MessageBoxIcon.Warning);
                            DetailGrid.Col = DetailGrid.Columns.IndexOf(DetailGrid.Columns[LOCATIONCODE_COL]);
                            DetailGrid.Focus();
                            DetailGrid.Select();

                            return null;
                        }

                        strSearchTableName = MST_BINTable.TABLE_NAME;
                        strSearchFieldName = MST_BINTable.CODE_FLD;
                        hashSearchCondition = new Hashtable();
                        hashSearchCondition.Add(MST_BINTable.LOCATIONID_FLD, DetailGrid[DetailGrid.Row, PRO_IssueMaterialDetailTable.LOCATIONID_FLD]);

                        //HACK: added by Tuan TQ. From BIN must be OK or OR Buffer (In)
                        string strConditionValue = ((int)BinTypeEnum.OK).ToString();
                        strConditionValue += "' OR " + MST_BINTable.TABLE_NAME + "." + MST_BINTable.BINTYPEID_FLD + "='" + (int)BinTypeEnum.IN;

                        hashSearchCondition.Add(MST_BINTable.BINTYPEID_FLD, strConditionValue);
                        //End Hack
                    }
                    else
                    {
                        return null;
                    }
                    break;
                case PRO_IssueMaterialDetailTable.LOT_FLD:
                    if (DetailGrid[DetailGrid.Row, ITM_ProductTable.LOTCONTROL_FLD].ToString() == 1.ToString())
                    {
                        if (DetailGrid[DetailGrid.Row, PRO_IssueMaterialDetailTable.PRODUCTID_FLD] == DBNull.Value
                            || DetailGrid[DetailGrid.Row, PRO_IssueMaterialDetailTable.PRODUCTID_FLD].ToString().Trim() == string.Empty)
                        {
                            PCSMessageBox.Show(ErrorCode.MSG_WOISSUE_MATERIAL_SELECT_COMPONENT_FIRST, MessageBoxIcon.Warning);
                            DetailGrid.Col = DetailGrid.Columns.IndexOf(DetailGrid.Columns[PRODUCTCODE_COL]);
                            DetailGrid.Focus();
                            DetailGrid.Select();
                            return null;
                        }

                        strSearchTableName = IV_LotItemTable.TABLE_NAME;
                        strSearchFieldName = IV_LotItemTable.LOT_FLD;
                        hashSearchCondition = new Hashtable();
                        hashSearchCondition.Add(IV_LotItemTable.PRODUCTID_FLD, DetailGrid[DetailGrid.Row, PRO_IssueMaterialDetailTable.PRODUCTID_FLD]);
                    }
                    else
                    {
                        return null;
                    }
                    break;
            }

            //call the search method to find out this text is existed in the database
            DataRowView drvResult = FormControlComponents.OpenSearchForm(strSearchTableName, strSearchFieldName, strSearchFieldValue, hashSearchCondition, pblnAlwayShowDialog);
            return drvResult;
        }

        /// <summary>
        /// This method is used to calculate the current available quantity
        /// This method will use the current row in the grid
        /// to get other conditional column to provide to the Method in BO
        /// the method in BO class will return the available quantity
        /// </summary>
        /// <returns></returns>
        private bool CheckValidCommitQty(decimal pdcmCommitQuantity, string pstrLot, string pstrSerial)
        {
            //get the commit quantity
            decimal dcmCommitQty = pdcmCommitQuantity;

            if (dcmCommitQty == 0)
            {
                return true;
            }

            //get Master Location
            int intMasterLocation = voPRO_IssueMaterialMasterVO.MasterLocationID;
            //get location ID
            int intLocationID = 0;
            if (DetailGrid[DetailGrid.Row, PRO_IssueMaterialDetailTable.LOCATIONID_FLD] != DBNull.Value
                && DetailGrid[DetailGrid.Row, PRO_IssueMaterialDetailTable.LOCATIONID_FLD].ToString().Trim() != string.Empty)
            {
                intLocationID = int.Parse(DetailGrid[DetailGrid.Row, PRO_IssueMaterialDetailTable.LOCATIONID_FLD].ToString());
            }

            //Get Bin ID
            int intBINID = 0;
            if (DetailGrid[DetailGrid.Row, PRO_IssueMaterialDetailTable.BINID_FLD] != DBNull.Value
                && DetailGrid[DetailGrid.Row, PRO_IssueMaterialDetailTable.BINID_FLD].ToString().Trim() != string.Empty)
            {
                intBINID = int.Parse(DetailGrid[DetailGrid.Row, PRO_IssueMaterialDetailTable.BINID_FLD].ToString());
            }

            //get CCN ID
            int intCCNID = int.Parse(CCNCombo.SelectedValue.ToString());

            //get product ID
            int intProductID = 0;
            if (DetailGrid[DetailGrid.Row, PRO_IssueMaterialDetailTable.PRODUCTID_FLD] != DBNull.Value
                && DetailGrid[DetailGrid.Row, PRO_IssueMaterialDetailTable.PRODUCTID_FLD].ToString().Trim() != string.Empty)
            {
                intProductID = int.Parse(DetailGrid[DetailGrid.Row, PRO_IssueMaterialDetailTable.PRODUCTID_FLD].ToString());
            }

            //HACKED by Tuan TQ: 10 Jan, 2005. Get Available Quantity by Post Date				
            var boInventoryUtil = new InventoryUtilsBO();
            decimal dcmAvailableQty = 0;

            if (DetailGrid[DetailGrid.Row, AVAILABLE_QUANTITY].ToString().Trim() != string.Empty)
                dcmAvailableQty = decimal.Parse(DetailGrid[DetailGrid.Row, AVAILABLE_QUANTITY].ToString());
            else
                return false;

            //Check the two quantities
            if (dcmCommitQty <= dcmAvailableQty)
            {
                //OK, check available at current date (against update nagative OHQty in Cache)
                DateTime dtmCurrentDate = (new UtilsBO()).GetDBDate().AddDays(1);

                dcmAvailableQty = boInventoryUtil.GetAvailableQtyByPostDate(dtmCurrentDate, intCCNID, intMasterLocation, intLocationID, intBINID, intProductID);

                if (dcmCommitQty <= dcmAvailableQty)
                    return true;
                PCSMessageBox.Show(ErrorCode.MESSAGE_AVAILABLE_WAS_USED_AFTER_POSTDATE, MessageBoxIcon.Warning);
                return false;
            }
            PCSMessageBox.Show(ErrorCode.MSG_WOISSUE_MATERIAL_COMMIT_QTY_DONOT_MEET_AVAILABLE_QTY, MessageBoxIcon.Warning);
            return false;
        }

        #endregion Methods

        #region Event Processing
        /// <summary>
        /// Implement the Add button click
        /// Initialize the default data
        /// Load the structure data into the grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddButton_Click(object sender, EventArgs e)
        {
            const string METHOD_NAME = THIS + ".AddButton_Click()";

            try
            {
                //Turn to Add action
                mFormMode = EnumAction.Add;

                //Unlock form
                LockForm(false);

                //clear controls
                ClearForm();

                //Chaneg to default CCN
                if (SystemProperty.CCNID > 0)
                {
                    CCNCombo.SelectedValue = SystemProperty.CCNID;
                }

                //setup date
                PostDatePicker.Value = Utilities.Instance.GetServerDate();

                //get the default returned goods number
                IssueNoText.Text = FormControlComponents.GetNoByMask(this);
                //Fill Default Master Location 
                FormControlComponents.SetDefaultMasterLocation(MasterLocationText);
                voPRO_IssueMaterialMasterVO = new PRO_IssueMaterialMasterVO();
                voPRO_IssueMaterialMasterVO.MasterLocationID = SystemProperty.MasterLocationID;
                //Enable Button
                EnableDisableButtons();
                //fill issue day
                FillIssueDate((DateTime)PostDatePicker.Value);

                LoadDetailData(0);
                //End hack
                LocationText.Focus();
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
        /// <summary>
        /// Fill Issue Date
        /// </summary>
        /// <param name="pdtmPostDate"></param>
        /// <author>Trada</author>
        /// <date>Monday, September 18 2006</date>
        private void FillIssueDate(DateTime pdtmPostDate)
        {
            var boMultiWOIssueMaterial = new MultiWOIssueMaterialBO();
            DateTime dtmWOCompDate;
            DataSet dstWorkingTime = boMultiWOIssueMaterial.GetWorkingTime();
            if (dstWorkingTime.Tables[0].Rows.Count == 3)
            {
                var dtmStartTimeFromDB = (DateTime)dstWorkingTime.Tables[0].Rows[0][PRO_ShiftPatternTable.WORKTIMEFROM_FLD];
                //Build StartTime and EndTime 
                var dtmStartTime = new DateTime(pdtmPostDate.Year, pdtmPostDate.Month, pdtmPostDate.Day,
                    dtmStartTimeFromDB.Hour, dtmStartTimeFromDB.Minute, 0);
                DateTime dtmEndTime = dtmStartTime.AddHours(24);
                if (pdtmPostDate >= dtmStartTime && pdtmPostDate <= dtmEndTime)
                {
                    dtmWOCompDate = new DateTime(pdtmPostDate.Year, pdtmPostDate.Month, pdtmPostDate.Day, 0, 0, 0);
                }
                else
                {
                    dtmWOCompDate = pdtmPostDate.AddDays(-1);
                    dtmWOCompDate = new DateTime(dtmWOCompDate.Year, dtmWOCompDate.Month, dtmWOCompDate.Day);
                }
                IssueDayPicker.Value = dtmWOCompDate;
            }
            else
            {
                var dtmStartTimeFromDB = new DateTime(pdtmPostDate.Year, pdtmPostDate.Month, pdtmPostDate.Day, 6, 15, 0);
                var dtmStartTime = new DateTime(pdtmPostDate.Year, pdtmPostDate.Month, pdtmPostDate.Day,
                    dtmStartTimeFromDB.Hour, dtmStartTimeFromDB.Minute, 0);
                if (pdtmPostDate >= dtmStartTime)
                {
                    dtmWOCompDate = new DateTime(pdtmPostDate.Year, pdtmPostDate.Month, pdtmPostDate.Day, 0, 0, 0);
                }
                else
                {
                    dtmWOCompDate = pdtmPostDate.AddDays(-1);
                    dtmWOCompDate = new DateTime(dtmWOCompDate.Year, dtmWOCompDate.Month, dtmWOCompDate.Day);
                }
                IssueDayPicker.Value = dtmWOCompDate;

            }
        }

        /// <summary>
        /// This method is used to load the default data on form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MultiWOIssueMaterial_Load(object sender, EventArgs e)
        {
            const string METHOD_NAME = THIS + ".MultiWOIssueMaterial_Load()";
            try
            {
                //Set authorization for user
                voPRO_IssueMaterialMasterVO = new PRO_IssueMaterialMasterVO();

                var objSecurity = new Security();
                this.Name = THIS;

                if (objSecurity.SetRightForUserOnForm(this, SystemProperty.UserName) == 0)
                {
                    // You don't have the right to view this item
                    PCSMessageBox.Show(ErrorCode.MESSAGE_YOU_HAVE_NO_RIGHT_TO_VIEW, MessageBoxIcon.Warning);
                    this.Close();
                    return;
                }

                //store the gridlayout
                dtStoreGridLayout = FormControlComponents.StoreGridLayout(DetailGrid);

                // HACK: Trada 10-18-2005
                // Load combo box
                var boUtil = new UtilsBO();
                DataSet dstCCN = boUtil.ListCCN();
                ShiftText.Text = "HC";
                ShiftText.Tag = boUtil.GetShiftDefault(ShiftText.Text.Trim()).ShiftID;
                PurposeText.Text = lblIssuePlan.Text;
                var drwResult = FormControlComponents.OpenSearchForm(PRO_IssuePurposeTable.TABLE_NAME, PRO_IssuePurposeTable.DESCRIPTION_FLD, PurposeText.Text.Trim(), null, false);
                if (drwResult != null)
                {
                    PurposeText.Text = drwResult[PRO_IssuePurposeTable.DESCRIPTION_FLD].ToString();
                    PurposeText.Tag = drwResult[PRO_IssuePurposeTable.ISSUEPURPOSEID_FLD];

                    voPRO_IssueMaterialMasterVO.IssuePurposeID = int.Parse(drwResult[PRO_IssuePurposeTable.ISSUEPURPOSEID_FLD].ToString());
                    strLastValidPurpose = PurposeText.Text.Trim();
                }

                CCNCombo.DataSource = dstCCN.Tables[MST_CCNTable.TABLE_NAME];
                CCNCombo.DisplayMember = MST_CCNTable.CODE_FLD;
                CCNCombo.ValueMember = MST_CCNTable.CCNID_FLD;
                FormControlComponents.PutDataIntoC1ComboBox(CCNCombo, dstCCN.Tables[MST_CCNTable.TABLE_NAME], MST_CCNTable.CODE_FLD, MST_CCNTable.CCNID_FLD, MST_CCNTable.TABLE_NAME);
                if (SystemProperty.CCNID != 0)
                {
                    CCNCombo.SelectedValue = SystemProperty.CCNID;
                } // END: Trada 10-18-2005
                //change the form mode
                mFormMode = EnumAction.Default;

                //Format dtmPostDate
                PostDatePicker.FormatType = FormatTypeEnum.CustomFormat;
                PostDatePicker.CustomFormat = Constants.DATETIME_FORMAT_HOUR;

                this.btnPrintConfiguration.Click += FormControlComponents.ShowMenuReportListHandler;
                this.PrintButton.Click += FormControlComponents.RunDefaultReportEntriesHandler;

                //clear data
                ClearForm();
                //lock control on form
                LockForm(true);
                //Diable or Enable buttons
                EnableDisableButtons();
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

        /// <summary>
        /// Display form allows user to select multi work order
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelectWO_Click(object sender, EventArgs e)
        {
            const string METHOD_NAME = THIS + ".btnSelectWO_Click()";
            try
            {
                //Check Master Location
                if (voPRO_IssueMaterialMasterVO.MasterLocationID <= 0)
                {
                    PCSMessageBox.Show(ErrorCode.MESSAGE_RGA_SELECTMASLOC, MessageBoxIcon.Warning);
                    MasterLocationText.Select();
                    MasterLocationText.Focus();
                    return;
                }

                if (LocationText.Text.Trim() == string.Empty)
                {
                    PCSMessageBox.Show(ErrorCode.MESSAGE_RGA_SELECTLOC, MessageBoxIcon.Warning);
                    LocationText.Focus();
                    return;
                }

                var frmSelectWorkOrders = new SelectWorkOrders
                                              {
                                                  MasterLocationCode = MasterLocationText.Text,
                                                  MasterLocationID = voPRO_IssueMaterialMasterVO.MasterLocationID,
                                                  CCNID = int.Parse(CCNCombo.SelectedValue.ToString()),
                                                  CCNCode = CCNCombo.Text,
                                                  ToLocationID = int.Parse(LocationText.Tag.ToString())
                                              };

                if (frmSelectWorkOrders.ShowDialog() == DialogResult.OK)
                {
                    dstData = frmSelectWorkOrders.SelectedResultDataSet;
                    DisplayDetailData();
                }

                frmSelectWorkOrders.Close();
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

        /// <summary>
        /// Save data into Database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            const string METHOD_NAME = THIS + ".btnSave_Click()";
            try
            {
                if (PCSMessageBox.Show(ErrorCode.MESSAGE_CONFIRM_BEFORE_SAVE_WOCOMPLETION, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2, new string[] { PostDatePicker.Text }) == DialogResult.Yes)
                {
                    blnHasError = true;

                    if (Security.IsDifferencePrefix(this, lblIssueNo, IssueNoText))
                        return;

                    if (DetailGrid.EditActive)
                        return;

                    if (!ValidateMandatoryControl())
                        return;

                    if (!CheckManatoryColumnInGrid())
                        return;

                    if (SaveData())
                    {
                        //Turn to Add action
                        mFormMode = EnumAction.Default;

                        //lock form
                        LockForm(true);

                        //Enable Button
                        EnableDisableButtons();
                        Security.UpdateUserNameModifyTransaction(this, PRO_IssueMaterialMasterTable.ISSUEMATERIALMASTERID_FLD, voPRO_IssueMaterialMasterVO.IssueMaterialMasterID);
                        PCSMessageBox.Show(ErrorCode.MESSAGE_AFTER_SAVE_DATA, MessageBoxIcon.Information);
                        blnHasError = false;
                    }
                }
            }
            catch (PCSException ex)
            {
                if (ex.mCode == ErrorCode.MESSAGE_NOT_ENOUGH_COMPONENT_TO_COMPLETE)
                {
                    //Show message
                    PCSMessageBox.Show(string.Format("Not enought quantity to issue for component {0}", ex.mMethod));
                    var productId = Convert.ToInt32(ex.Hash[ITM_ProductTable.PRODUCTID_FLD]);
                    var ohQuantity = Convert.ToDecimal(ex.Hash[IV_BinCacheTable.OHQUANTITY_FLD]);
                    // find in the grid and set focus
                    for (int i = 0; i < DetailGrid.RowCount; i++)
                    {
                        var product = Convert.ToInt32(DetailGrid[i, ITM_ProductTable.PRODUCTID_FLD]);
                        if (productId == product)
                        {
                            DetailGrid.Row = i;
                            DetailGrid.Col = DetailGrid.Columns.IndexOf(DetailGrid.Columns[PRO_IssueMaterialDetailTable.AVAILABLEQUANTITY_FLD]);
                            DetailGrid[DetailGrid.Row, PRO_IssueMaterialDetailTable.AVAILABLEQUANTITY_FLD] = ohQuantity;
                            DetailGrid.Focus();
                        }
                    }
                }
                else
                {
                    // displays the error message.
                    PCSMessageBox.Show(ex.mCode, MessageBoxIcon.Error);
                    //Check if IssueNo was duplicated
                    if (ex.mCode == ErrorCode.DUPLICATE_KEY)
                    {
                        IssueNoText.Focus();
                        IssueNoText.Select();
                    }
                }
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

        /// <summary>
        /// Implement the F4 Key
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtMasLoc_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F4)
                {
                    if (MasterLocationButton.Enabled)
                    {
                        btnFindMasLoc_Click(null, null);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        private void btnFindMasLoc_Click(object sender, EventArgs e)
        {
            const string METHOD_NAME = THIS + ".btnMasLoc_Click()";
            try
            {
                if (CCNCombo.SelectedIndex < 0)
                {
                    string[] msg = { lblCCN.Text, lblMasLoc.Text };
                    PCSMessageBox.Show(ErrorCode.MESSAGE_SELECT_ONE_BEFORE_SELECT_ONE, MessageBoxIcon.Warning, msg);
                    if (MasterLocationText.Text.Trim() != string.Empty)
                        MasterLocationText.Focus();
                    else
                        CCNCombo.Focus();
                    return;
                }

                var hastCondition = new Hashtable {{MST_MasterLocationTable.CCNID_FLD, CCNCombo.SelectedValue}};
                var drwResult = FormControlComponents.OpenSearchForm(MST_MasterLocationTable.TABLE_NAME, MST_MasterLocationTable.CODE_FLD, MasterLocationText.Text.Trim(), hastCondition, true);
                if (drwResult != null)
                {
                    // HACK: Trada 27-10-2005
                    if (voPRO_IssueMaterialMasterVO.MasterLocationID != 0)
                    {
                        if (voPRO_IssueMaterialMasterVO.MasterLocationID != int.Parse(drwResult[MST_MasterLocationTable.MASTERLOCATIONID_FLD].ToString()))
                        {
                            BinText.Text = string.Empty;
                            BinText.Tag = "0";
                            LocationText.Text = string.Empty;
                            LocationText.Tag = "0";

                            voPRO_IssueMaterialMasterVO.ToBinID = 0;
                            voPRO_IssueMaterialMasterVO.ToLocationID = 0;

                            LoadDetailData(0);
                        }
                    }
                    // END: Trada 27-10-2005

                    voPRO_IssueMaterialMasterVO.MasterLocationID = int.Parse(drwResult[MST_MasterLocationTable.MASTERLOCATIONID_FLD].ToString());
                    MasterLocationText.Text = drwResult[MST_MasterLocationTable.CODE_FLD].ToString();
                    MasterLocationText.Tag = drwResult[MST_MasterLocationTable.MASTERLOCATIONID_FLD];

                    strLastValidMasLoc = MasterLocationText.Text;

                    ChangeMasterLocation();
                }
                else
                {
                    // HACK: dungla 10-18-2005
                    MasterLocationText.Focus();
                    MasterLocationText.SelectAll();
                    // END: dungla 10-18-2005

                    BinText.Text = string.Empty;
                    BinText.Tag = "0";
                    LocationText.Text = string.Empty;
                    LocationText.Tag = "0";
                }
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

        private void cboCCN_SelectedValueChanged(object sender, EventArgs e)
        {
            const string METHOD_NAME = THIS + ".cboCCN_SelectedValueChanged()";
            try
            {
                voPRO_IssueMaterialMasterVO.MasterLocationID = 0;
                MasterLocationText.Text = string.Empty;
                ChangeMasterLocation();
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

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Implement the closing method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MultiWOIssueMaterial_Closing(object sender, CancelEventArgs e)
        {
            const string METHOD_NAME = THIS + ".MultiWOIssueMaterial_Closing()";
            try
            {
                if (mFormMode == EnumAction.Add || mFormMode == EnumAction.Edit)
                {
                    DialogResult dlgResult = PCSMessageBox.Show(ErrorCode.MESSAGE_QUESTION_STORE_INTO_DATABASE, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                    switch (dlgResult)
                    {
                        case DialogResult.Yes:
                            // HACK: Trada 13-12-2005
                            btnSave_Click(SaveButton, new EventArgs());
                            if (blnHasError)
                            {
                                e.Cancel = true;
                            }
                            // END: Trada 13-12-2005

                            break;
                        case DialogResult.No:
                            e.Cancel = false;
                            break;
                        case DialogResult.Cancel:
                            e.Cancel = true;
                            break;
                    }
                }
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
                e.Cancel = true;
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
                e.Cancel = true;

            }
        }

        private void txtIssueNo_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F4)
                {
                    if (IssueNoButton.Enabled)
                    {
                        btnFindIssueNo_Click(null, null);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

        }

        private void btnFindIssueNo_Click(object sender, EventArgs e)
        {
            const string METHOD_NAME = THIS + ".btnFindTransNo_Click()";
            try
            {
                Hashtable hashtCondition = null;

                DataRowView drwResult = null;
                // HACK: dungla 10-18-2005
                if (sender is TextBox)
                    drwResult = FormControlComponents.OpenSearchForm(PRO_IssueMaterialMasterTable.TABLE_NAME, PRO_IssueMaterialMasterTable.ISSUENO_FLD, IssueNoText.Text.Trim(), hashtCondition, false);
                else
                    drwResult = FormControlComponents.OpenSearchForm(PRO_IssueMaterialMasterTable.TABLE_NAME, PRO_IssueMaterialMasterTable.ISSUENO_FLD, IssueNoText.Text.Trim(), hashtCondition, true);
                // END: dungla 10-18-2005
                if (drwResult != null)
                {
                    LoadData(drwResult.Row);
                }
                else
                {
                    IssueNoText.Focus();
                    IssueNoText.SelectAll();
                }
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

        /// <summary>
        /// This method is used to validate the data
        /// before it is updated back to the dataset and grid
        /// It is the same as leave method in the text bo
        /// This method will check the data for 
        /// Work Order Master, Work Order Detail, Employee, ect..
        /// If this value doesn't exit in the table, it will open a search form for user to select 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgrdData_BeforeColUpdate(object sender, BeforeColUpdateEventArgs e)
        {
            //check the input check quantity
            const string METHOD_NAME = THIS + ".dgrdData_BeforeColUpdate()";

            try
            {
                string strColumnName = e.Column.DataColumn.DataField;
                string strColumValue = e.Column.DataColumn.Text.Trim();

                if (strColumnName == PRO_IssueMaterialDetailTable.COMMITQUANTITY_FLD)
                {
                    if (DetailGrid[DetailGrid.Row, PRO_IssueMaterialDetailTable.COMMITQUANTITY_FLD].ToString() != string.Empty)
                    {
                        pdecChangedQuantity = decimal.Parse(DetailGrid[DetailGrid.Row, PRO_IssueMaterialDetailTable.COMMITQUANTITY_FLD].ToString()) - pdecTempCommitQuantity;
                    }
                    else
                    {
                        pdecChangedQuantity = -pdecTempCommitQuantity;
                    }

                    for (int i = DetailGrid.Row + 1; i < DetailGrid.RowCount; i++)
                    {
                        #region // HACK: DuongNA 2005-10-14

                        decimal dcmTempAvailable = 0;
                        try
                        {
                            dcmTempAvailable = decimal.Parse(DetailGrid[i, AVAILABLE_QUANTITY].ToString());
                        }
                        catch
                        {
                        }
                        if ((dcmTempAvailable - pdecChangedQuantity) < 0)
                        {
                            PCSMessageBox.Show(ErrorCode.MESSAGE_RGA_LOCATION, MessageBoxIcon.Warning);
                            e.Cancel = true;
                            return;
                        }

                        #endregion // END: DuongNA 2005-10-14
                    }
                }

                //define the variable for checking the commit quantity
                decimal dcmCommitQuantity = 0;
                string strLot = string.Empty;
                string strSerial = string.Empty;

                //Call the search form and display the result
                if (e.Column.Button)
                {
                    if (strColumValue != string.Empty)
                    {
                        DataRowView drvResult = CallOpenSearchForm(strColumnName, strColumValue, false);

                        if (drvResult != null)
                        {
                            //HACK: added by Tuan TQ. 23 Jan, 2006. From Bin must be diffirent from To Bin
                            if (strColumnName == MST_BINTable.BINID_FLD)
                            {
                                if (drvResult[MST_BINTable.BINID_FLD].Equals(BinText.Tag) && (BinText.Tag.ToString() != string.Empty))
                                {
                                    PCSMessageBox.Show(ErrorCode.MESSAGE_LOCTOLOC_DIFFERENCE_BIN, MessageBoxIcon.Warning);
                                    e.Column.DataColumn.Tag = null;
                                    e.Cancel = true;
                                }
                            }
                            //End hack

                            e.Column.DataColumn.Tag = drvResult;
                        }
                        else
                        {
                            e.Cancel = true;
                        }
                    }
                    else
                    {
                        e.Column.DataColumn.Tag = null;
                    }
                }
                else
                {
                    //check the quantity, complete percent, and all the other columns
                    if (strColumValue == string.Empty)
                    {
                        return;
                    }

                    switch (strColumnName)
                    {
                        case PRO_IssueMaterialDetailTable.COMMITQUANTITY_FLD:
                            if (!FormControlComponents.IsPositiveNumeric(strColumValue))
                            {
                                PCSMessageBox.Show(ErrorCode.MSG_LABORTIME_QTY, MessageBoxIcon.Warning);

                                e.Cancel = true;
                                break;
                            }

                            //check if this value is available in the Inventory
                            dcmCommitQuantity = decimal.Parse(strColumValue);

                            #region // HACK: DuongNA 2005-10-14

                            //count for total Commit Quantity at another rows
                            for (int i = 0; i < DetailGrid.RowCount; i++)
                            {
                                //Check if the row have same ProductID, LocationID and BinID with current row
                                if ((!DetailGrid[i, PRO_IssueMaterialDetailTable.BINID_FLD].Equals(DetailGrid[DetailGrid.Row, PRO_IssueMaterialDetailTable.BINID_FLD])) ||
                                    (!DetailGrid[i, PRO_IssueMaterialDetailTable.LOCATIONID_FLD].Equals(DetailGrid[DetailGrid.Row, PRO_IssueMaterialDetailTable.LOCATIONID_FLD])) ||
                                    (!DetailGrid[i, PRO_IssueMaterialDetailTable.PRODUCTID_FLD].Equals(DetailGrid[DetailGrid.Row, PRO_IssueMaterialDetailTable.PRODUCTID_FLD])) ||
                                    (i == DetailGrid.Row))
                                {
                                    continue;
                                }
                                try
                                {
                                    dcmCommitQuantity += decimal.Parse(DetailGrid[i, PRO_IssueMaterialDetailTable.COMMITQUANTITY_FLD].ToString());
                                }
                                catch
                                {
                                }
                            }

                            #endregion // END: DuongNA 2005-10-14

                            blnRecalculateCommit = false;
                            break;
                        case PRO_IssueMaterialDetailTable.LOT_FLD:
                            break;

                        case PRO_IssueMaterialDetailTable.SERIAL_FLD:
                            //get the commit quantity value
                            if (DetailGrid[DetailGrid.Row, PRO_IssueMaterialDetailTable.COMMITQUANTITY_FLD] != DBNull.Value
                                && DetailGrid[DetailGrid.Row, PRO_IssueMaterialDetailTable.COMMITQUANTITY_FLD].ToString().Trim() != string.Empty)
                            {
                                dcmCommitQuantity = decimal.Parse(DetailGrid[DetailGrid.Row, PRO_IssueMaterialDetailTable.COMMITQUANTITY_FLD].ToString());
                            }
                            //get Lot
                            if (DetailGrid[DetailGrid.Row, PRO_IssueMaterialDetailTable.LOT_FLD] != DBNull.Value
                                && DetailGrid[DetailGrid.Row, PRO_IssueMaterialDetailTable.LOT_FLD].ToString().Trim() != string.Empty)
                            {
                                strLot = DetailGrid[DetailGrid.Row, PRO_IssueMaterialDetailTable.LOT_FLD].ToString().Trim();
                            }
                            //get Serial
                            strSerial = strColumValue;

                            //Check the quantity
                            if (!CheckValidCommitQty(dcmCommitQuantity, strLot, strSerial))
                            {
                                PCSMessageBox.Show(ErrorCode.MSG_WOISSUE_MATERIAL_COMMIT_QTY_DONOT_MEET_SERIAL_QTY, MessageBoxIcon.Warning);
                                e.Cancel = true;
                            }
                            break;
                    }
                }
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
                e.Cancel = true;
            }

            catch (Exception ex)
            {
                e.Cancel = true;
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

        /// <summary>
        /// Open the Search form when user click at the button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgrdData_ButtonClick(object sender, ColEventArgs e)
         {
            const string METHOD_NAME = THIS + ".dgrdData_ButtonClick()";
            try
            {
                if (!DetailGrid.AllowAddNew && !DetailGrid.AllowUpdate)
                {
                    return;
                }
                string strColumnName = e.Column.DataColumn.DataField;
                string strColumnValue = DetailGrid[DetailGrid.Row, strColumnName].ToString().Trim();

                if (strColumnName != PRO_IssueMaterialDetailTable.QASTATUS_FLD)
                {
                    DisplaySearchForm(strColumnName, strColumnValue, true);
                }
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

        /// <summary>
        /// Implement the F4 
        /// When user press this key, it will display the Search Form 
        /// for selecting values 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgrdData_KeyDown(object sender, KeyEventArgs e)
        {
            const string METHOD_NAME = THIS + ".dgrdData_KeyDown()";
            try
            {
                if ((e.KeyCode == Keys.Delete) && (DetailGrid.SelectedRows.Count > 0))
                {
                    if (SaveButton.Enabled)
                    {
                        FormControlComponents.DeleteMultiRowsOnTrueDBGrid(DetailGrid);
                        //Re-Calculate Line column
                        ChangeLineNumber();
                    }
                }
                if (e.KeyCode == Keys.F4)
                {
                    //first get the column name
                    string strColumName = DetailGrid.Columns[DetailGrid.Col].DataField;
                    string strColumnValue = DetailGrid.Columns[DetailGrid.Col].Text;
                    if (strColumName != WORKORDERNO_COL
                        && strColumName != WORKORDERLINE_COL
                        && strColumName != LOCATIONCODE_COL
                        && strColumName != BINCODE_COL
                        && strColumName != PRODUCTCODE_COL
                        && strColumName != PRO_IssueMaterialDetailTable.LOT_FLD)
                    {
                        return;
                    }
                    DisplaySearchForm(strColumName, strColumnValue, true);
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

        /// <summary>
        /// After column update
        /// we need to update to the other related column
        /// its data for the related column are stored in the Tag object of the column
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgrdData_AfterColUpdate(object sender, ColEventArgs e)
        {
            const string METHOD_NAME = THIS + ".dgrdData_AfterColUpdate()";
            try
            {
                string strColumnName = e.Column.DataColumn.DataField;
                string strColumnValue = e.Column.DataColumn.Text.Trim();
                if (e.Column.DataColumn.Tag != null)
                {
                    UpdateValueAfterSearch(strColumnName, strColumnValue, (DataRowView)e.Column.DataColumn.Tag, false);
                }
                else
                {
                    UpdateValueAfterSearch(strColumnName, strColumnValue, null, false);
                }


                DetailGrid.Refresh();
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

        /// <summary>
        /// This method is used to check if user can edit the column value
        /// For the related columns together, if user want to enter data
        /// the other column must have data first
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <authors>
        /// THIENHD
        /// </authors>
        private void dgrdData_BeforeColEdit(object sender, BeforeColEditEventArgs e)
        {
            const string METHOD_NAME = THIS + ".dgrdData_BeforeColEdit()";
            try
            {
                string strColumnName = e.Column.DataColumn.DataField;
                if (strColumnName == PRO_IssueMaterialDetailTable.COMMITQUANTITY_FLD)
                {
                    pdecTempCommitQuantity = DetailGrid[DetailGrid.Row, PRO_IssueMaterialDetailTable.COMMITQUANTITY_FLD].ToString() != string.Empty ? Decimal.Parse(DetailGrid[DetailGrid.Row, PRO_IssueMaterialDetailTable.COMMITQUANTITY_FLD].ToString()) : 0;
                }

                switch (strColumnName)
                {
                    case WORKORDERNO_COL:
                        if (voPRO_IssueMaterialMasterVO.MasterLocationID <= 0)
                        {
                            PCSMessageBox.Show(ErrorCode.MSG_LABORTIME_SELECTMASLOC, MessageBoxIcon.Warning);
                            e.Cancel = true;
                        }
                        break;

                    case WORKORDERLINE_COL:
                        if (DetailGrid[DetailGrid.Row, PRO_IssueMaterialDetailTable.WORKORDERMASTERID_FLD] == DBNull.Value
                            || DetailGrid[DetailGrid.Row, PRO_IssueMaterialDetailTable.WORKORDERMASTERID_FLD].ToString().Trim() == string.Empty)
                        {
                            PCSMessageBox.Show(ErrorCode.MSG_LABORTIME_SELECTWOMASTER, MessageBoxIcon.Warning);
                            e.Cancel = true;
                        }
                        break;

                    case LOCATIONCODE_COL:
                        if (voPRO_IssueMaterialMasterVO.MasterLocationID <= 0)
                        {
                            PCSMessageBox.Show(ErrorCode.MSG_LABORTIME_SELECTMASLOC, MessageBoxIcon.Warning);
                            e.Cancel = true;
                        }
                        break;

                    case BINCODE_COL:
                        if (DetailGrid[DetailGrid.Row, HASBIN].ToString() == 1.ToString())
                        {
                            if (DetailGrid[DetailGrid.Row, PRO_IssueMaterialDetailTable.LOCATIONID_FLD] == DBNull.Value
                                || DetailGrid[DetailGrid.Row, PRO_IssueMaterialDetailTable.LOCATIONID_FLD].ToString().Trim() == string.Empty)
                            {
                                PCSMessageBox.Show(ErrorCode.MSG_WOISSUE_MATERIAL_SELECT_LOCATION_FIRST, MessageBoxIcon.Warning);
                                e.Cancel = true;
                            }
                        }
                        else
                        {
                            e.Cancel = true;
                        }
                        break;

                    case PRODUCTCODE_COL:
                        if (DetailGrid[DetailGrid.Row, PRO_IssueMaterialDetailTable.WORKORDERDETAILID_FLD] == DBNull.Value
                            || DetailGrid[DetailGrid.Row, PRO_IssueMaterialDetailTable.WORKORDERDETAILID_FLD].ToString().Trim() == string.Empty)
                        {
                            PCSMessageBox.Show(ErrorCode.MSG_WOISSUE_MATERIAL_SELECT_WORKORDER_LINE_FIRST, MessageBoxIcon.Warning);
                            e.Cancel = true;
                        }
                        break;

                    case PRODUCTDESC_COL:
                        if (DetailGrid[DetailGrid.Row, PRO_IssueMaterialDetailTable.WORKORDERDETAILID_FLD] == DBNull.Value
                            || DetailGrid[DetailGrid.Row, PRO_IssueMaterialDetailTable.WORKORDERDETAILID_FLD].ToString().Trim() == string.Empty)
                        {
                            PCSMessageBox.Show(ErrorCode.MSG_WOISSUE_MATERIAL_SELECT_WORKORDER_LINE_FIRST, MessageBoxIcon.Warning);
                            e.Cancel = true;
                        }
                        break;

                    case PRODUCTREVISION_COL:
                        if (DetailGrid[DetailGrid.Row, PRO_IssueMaterialDetailTable.WORKORDERDETAILID_FLD] == DBNull.Value
                            || DetailGrid[DetailGrid.Row, PRO_IssueMaterialDetailTable.WORKORDERDETAILID_FLD].ToString().Trim() == string.Empty)
                        {
                            PCSMessageBox.Show(ErrorCode.MSG_WOISSUE_MATERIAL_SELECT_WORKORDER_LINE_FIRST, MessageBoxIcon.Warning);
                            e.Cancel = true;
                        }
                        break;

                    case PRO_IssueMaterialDetailTable.COMMITQUANTITY_FLD:
                        if (DetailGrid[DetailGrid.Row, PRO_IssueMaterialDetailTable.PRODUCTID_FLD] == DBNull.Value
                            || DetailGrid[DetailGrid.Row, PRO_IssueMaterialDetailTable.PRODUCTID_FLD].ToString().Trim() == string.Empty)
                        {
                            PCSMessageBox.Show(ErrorCode.MSG_WOISSUE_MATERIAL_SELECT_COMPONENT_FIRST, MessageBoxIcon.Warning);
                            e.Cancel = true;
                        }
                        break;

                    case PRO_IssueMaterialDetailTable.LOT_FLD:
                        if (DetailGrid[DetailGrid.Row, PRO_IssueMaterialDetailTable.PRODUCTID_FLD] == DBNull.Value
                            || DetailGrid[DetailGrid.Row, PRO_IssueMaterialDetailTable.PRODUCTID_FLD].ToString().Trim() == string.Empty)
                        {
                            PCSMessageBox.Show(ErrorCode.MSG_WOISSUE_MATERIAL_SELECT_COMPONENT_FIRST, MessageBoxIcon.Warning);
                            e.Cancel = true;
                        }
                        break;

                    case PRO_IssueMaterialDetailTable.SERIAL_FLD:
                        if (DetailGrid[DetailGrid.Row, PRO_IssueMaterialDetailTable.SERIAL_FLD] == DBNull.Value
                            || DetailGrid[DetailGrid.Row, PRO_IssueMaterialDetailTable.SERIAL_FLD].ToString().Trim() == string.Empty)
                        {
                            PCSMessageBox.Show(ErrorCode.MSG_WOISSUE_MATERIAL_SELECT_LOT_FIRST, MessageBoxIcon.Warning);
                            e.Cancel = true;
                        }
                        break;
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

        /// <summary>
        /// This method is used to set some default column values for the detail 
        /// when user add a new row into the grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgrdData_OnAddNew(object sender, EventArgs e)
        {
            //assign the master id to the detail
            const string METHOD_NAME = THIS + ".dgrdData_OnAddNew()";
            try
            {
                DetailGrid[DetailGrid.Row, PRO_IssueMaterialDetailTable.MASTERLOCATIONID_FLD] = voPRO_IssueMaterialMasterVO.MasterLocationID;
                DetailGrid[DetailGrid.Row, PRO_IssueMaterialDetailTable.LINE_FLD] = GetTheNextLineNumber();
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

        /// <summary>
        /// This method is used to reset the line number after deleting
        /// a row in the grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgrdData_AfterDelete(object sender, EventArgs e)
        {
            const string METHOD_NAME = THIS + ".dgrdData_OnAddNew()";
            try
            {
                ChangeLineNumber();
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
        /// <summary>
        /// btnPurpose_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Trada</author>
        /// <date>Wednesday, October 26 2005</date>
        private void btnPurpose_Click(object sender, System.EventArgs e)
        {
            const string METHOD_NAME = THIS + ".btnPurpose_Click()";
            try
            {
                //HACK: added by Tuan TQ. 18 Jan, 2006. apply purpose
                var htbCondition = new Hashtable();
                var iIssueMaterialTransID = (new UtilsBO()).GetTransTypeIDByCode(TransactionTypeEnum.PROIssueMaterial.ToString());
                htbCondition.Add(MST_TranTypeTable.TRANTYPEID_FLD, iIssueMaterialTransID);
                //End hack

                var drwResult = FormControlComponents.OpenSearchForm(PRO_IssuePurposeTable.TABLE_NAME, PRO_IssuePurposeTable.DESCRIPTION_FLD, PurposeText.Text.Trim(), htbCondition, true);
                if (drwResult != null)
                {
                    PurposeText.Text = drwResult[PRO_IssuePurposeTable.DESCRIPTION_FLD].ToString();
                    PurposeText.Tag = drwResult[PRO_IssuePurposeTable.ISSUEPURPOSEID_FLD];

                    voPRO_IssueMaterialMasterVO.IssuePurposeID = int.Parse(drwResult[PRO_IssuePurposeTable.ISSUEPURPOSEID_FLD].ToString());
                    strLastValidPurpose = PurposeText.Text.Trim();
                }
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
        /// <summary>
        /// btnToLocation_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Trada</author>
        /// <date>Wednesday, October 26 2005</date>
        private void btnToLocation_Click(object sender, System.EventArgs e)
        {
            const string METHOD_NAME = THIS + ".btnToLocation_Click()";
            try
            {
                var htbCriteria = new Hashtable();
                //User has entered MasLoc
                if (MasterLocationText.Text.Trim() != string.Empty)
                    htbCriteria.Add(PRO_IssueMaterialMasterTable.MASTERLOCATIONID_FLD, voPRO_IssueMaterialMasterVO.MasterLocationID);
                else //User has not entered MasLoc
                {
                    string[] msg = { lblMasLoc.Text, lblToLocation.Text };
                    PCSMessageBox.Show(ErrorCode.MESSAGE_SELECT_ONE_BEFORE_SELECT_ONE, MessageBoxIcon.Warning, msg);
                    if (LocationText.Text.Trim() != string.Empty)
                        LocationText.Focus();
                    else
                        MasterLocationText.Focus();
                    return;
                }

                htbCriteria.Add(MST_LocationTable.LOCATIONTYPEID_FLD, (int)LocationTypeEnum.Manufacturing);
                
                var drwResult = FormControlComponents.OpenSearchForm(MST_LocationTable.TABLE_NAME, MST_LocationTable.CODE_FLD, LocationText.Text.Trim(), htbCriteria, true);
                if (drwResult != null)
                {
                    //Check if Location has BinControl
                    bool blnBinControl = Convert.ToBoolean(drwResult[MST_LocationTable.BIN_FLD]);
                    BinText.Enabled = BinButton.Enabled = blnBinControl;
                    LocationText.Text = drwResult[MST_LocationTable.CODE_FLD].ToString();
                    LocationText.Tag = drwResult[MST_LocationTable.LOCATIONID_FLD];
                    voPRO_IssueMaterialMasterVO.ToLocationID = int.Parse(drwResult[MST_LocationTable.LOCATIONID_FLD].ToString());
                    // change location
                    if (strLastValidLocation != LocationText.Text)
                    {
                        BinText.Text = string.Empty;
                        BinText.Tag = "0";
                        voPRO_IssueMaterialMasterVO.ToBinID = 0;
                        strLastValidLocation = LocationText.Text;
                    }
                    btnToBin_Click(null, e);
                }
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
        /// <summary>
        /// btnToBin_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Trada</author>
        /// <date>Wednesday, October 26 2005</date>
        private void btnToBin_Click(object sender, System.EventArgs e)
        {
            const string METHOD_NAME = THIS + ".btnToBin_Click()";
            try
            {
                var htbCriteria = new Hashtable();
                //User has entered ToLocation
                if (LocationText.Text.Trim() != string.Empty)
                    htbCriteria.Add(MST_LocationTable.LOCATIONID_FLD, voPRO_IssueMaterialMasterVO.ToLocationID);
                else //User has not entered ToLocation
                {
                    string[] msg = { lblToLocation.Text, lblToBin.Text };
                    PCSMessageBox.Show(ErrorCode.MESSAGE_SELECT_ONE_BEFORE_SELECT_ONE, MessageBoxIcon.Warning, msg);
                    if (BinText.Text.Trim() != string.Empty)
                        BinText.Focus();
                    else
                        LocationText.Focus();
                    return;
                }

                //HACK: Added by Tuan TQ. 19 Jan, 2006. Tobin must be Incoming (Buffer)
                htbCriteria.Add(MST_BINTable.BINTYPEID_FLD, (int)BinTypeEnum.IN);
                //End Hacked
                bool show = false;
                if (sender != null) show = true;
                var drwResult = FormControlComponents.OpenSearchForm(MST_BINTable.TABLE_NAME, MST_BINTable.CODE_FLD, BinText.Text.Trim(), htbCriteria, show);
                if (drwResult != null)
                {
                    BinText.Text = drwResult[MST_BINTable.CODE_FLD].ToString();
                    BinText.Tag = drwResult[MST_BINTable.BINID_FLD];

                    voPRO_IssueMaterialMasterVO.ToBinID = int.Parse(drwResult[MST_BINTable.BINID_FLD].ToString());

                    strLastValidBin = BinText.Text;
                }
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
        /// <summary>
        /// txtPurpose_KeyDown
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Trada</author>
        /// <date>Wednesday, October 26 2005</date>
        private void txtPurpose_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F4)
            {
                if (PurposeButton.Enabled)
                {
                    btnPurpose_Click(null, null);
                }
            }
        }
        /// <summary>
        /// txtToLocation_KeyDown
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Trada</author>
        /// <date>Wednesday, October 26 2005</date>
        private void txtToLocation_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F4)
            {
                if (LocationButton.Enabled)
                {
                    btnToLocation_Click(null, null);
                }
            }
        }

        private void btnPrint_Click(object sender, System.EventArgs e)
        {

        }

        /// HACK:
        /// <summary>
        /// Thachnn: 27/10/2005
        /// Preview the report for this form
        /// Using the "Issue Slip.xml" layout
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrintIssueSlip_Click(object sender, System.EventArgs e)
        {
            #region Constants
            const string METHOD_NAME = THIS + ".txtToLocation_KeyDown()";
            string mstrReportDefFolder = Application.StartupPath + "\\" + Constants.REPORT_DEFINITION_STORE_LOCATION;
            const string REPORT_LAYOUT_FILE = "IssueSlip.xml";
            const string REPORT_NAME = "Issue Slip";

            const string REPORTFLD_COMPANY = "fldCompany";
            const string REPORTFLD_ADDRESS = "fldAddress";
            const string REPORTFLD_TEL = "fldTel";
            const string REPORTFLD_FAX = "fldFax";

            const string REPORTFLD_SLIPNO = "fldSlipNo";
            const string REPORTFLD_POSTDATE = "fldPostDate";
            const string REPORTFLD_POSTTIME = "fldPostTime";
            const string REPORTFLD_WORKORDERNO = "fldWorkOrderNo";
            const string REPORTFLD_LINENO = "fldLineNo";
            const string REPORTFLD_CATEGORYCODE = "fldCategoryCode";
            const string REPORTFLD_CATEGORYNAME = "fldCategoryName";
            const string REPORTFLD_CATEGORYMODEL = "fldCategoryModel";
            const string REPORTFLD_PURPOSE = "fldPurpose";

            const string REPORTFLD_FROMLOCATIONNAME = "fldFromLocationName";
            const string REPORTFLD_FROMLOCATIONCODE = "fldFromLocationCode";
            const string REPORTFLD_TOLOCATIONNAME = "fldToLocationName";
            const string REPORTFLD_TOLOCATIONCODE = "fldToLocationCode";

            const string REPORTFLD_PARTNUMBER = "fldPartNumber";
            const string REPORTFLD_PARTNAME = "fldPartName";
            const string REPORTFLD_UNIT = "fldUnit";
            const string REPORTFLD_QTYACTUAL = "fldQtyActual";
            const string REPORTFLD_SOURCE = "fldSource";
            const string REPORTFLD_QTYPLAN = "fldQtyPlan";

            #endregion

            var objBO = new MultiWOIssueMaterialBO();
            DataTable dtbResult;

            try
            {
                dtbResult = this.dstData.Tables[0].Copy();
            }
            catch
            {
                dtbResult = new DataTable();
            }


            ArrayList arrWOLines = GetColumnValuesFromTable(dtbResult, WORKORDERLINE_COL);
            ArrayList arrWONumbers = GetColumnValuesFromTable(dtbResult, WORKORDERNO_COL);
            if (arrWOLines.Count > 1 || arrWONumbers.Count > 1)
            {
                PCSMessageBox.Show("This report can only run with 1 Work Order and it has only 1 Line");
                return;
            }

            Cursor = Cursors.WaitCursor;

            try
            {
                var objRB = new ReportBuilder();
                try
                {
                    objRB.ReportName = REPORT_NAME;
                    objRB.SourceDataTable = dtbResult;
                }
                catch
                {
                    // we can't preview while we don't have any data
                    return;
                }

                #region BUILD ISSUE SLIP REPORT FIELD
                try
                {
                    objRB.ReportDefinitionFolder = mstrReportDefFolder;
                    objRB.ReportLayoutFile = REPORT_LAYOUT_FILE;
                    objRB.UseLayoutFile = objRB.AnalyseLayoutFile();
                }
                catch
                {
                    objRB.UseLayoutFile = false;
                    PCSMessageBox.Show(ErrorCode.MESSAGE_REPORT_TEMPLATE_FILE_NOT_FOUND);
                }
                #endregion

                objRB.MakeDataTableForRender();

                #region RENDER TO PRINT PREVIEW
                // and show it in preview dialog				
                var printPreview = new C1PrintPreviewDialog();

                objRB.ReportViewer = printPreview.ReportViewer;
                objRB.RenderReport();

                #region COMPANY INFO // header information get from system params
                try
                {
                    objRB.DrawPredefinedField(REPORTFLD_COMPANY, SystemProperty.SytemParams.Get(SystemParam.COMPANY_FULL_NAME));
                }
                catch { }
                try
                {
                    objRB.DrawPredefinedField(REPORTFLD_ADDRESS, SystemProperty.SytemParams.Get(SystemParam.ADDRESS));
                }
                catch { }
                try
                {
                    objRB.DrawPredefinedField(REPORTFLD_TEL, SystemProperty.SytemParams.Get(SystemParam.TEL));
                }
                catch { }
                try
                {
                    objRB.DrawPredefinedField(REPORTFLD_FAX, SystemProperty.SytemParams.Get(SystemParam.FAX));
                }
                catch { }
                #endregion


                if (voPRO_IssueMaterialMasterVO != null && dtbResult.Rows.Count > 0)
                {
                    try
                    {
                        objRB.DrawPredefinedField(REPORTFLD_SLIPNO, voPRO_IssueMaterialMasterVO.IssueNo);
                    }
                    catch { }

                    try
                    {
                        objRB.DrawPredefinedField(REPORTFLD_POSTDATE, voPRO_IssueMaterialMasterVO.PostDate.ToLongDateString());
                    }
                    catch { }
                    try
                    {
                        objRB.DrawPredefinedField(REPORTFLD_POSTTIME, voPRO_IssueMaterialMasterVO.PostDate.ToShortTimeString());
                    }
                    catch { }

                    try
                    {
                        string strWorkOrderNo = dtbResult.Rows[0][WORKORDERNO_COL].ToString();
                        objRB.DrawPredefinedField(REPORTFLD_WORKORDERNO, strWorkOrderNo);
                    }
                    catch { }

                    try
                    {
                        string strLineNo = dtbResult.Rows[0][WORKORDERLINE_COL].ToString();
                        objRB.DrawPredefinedField(REPORTFLD_LINENO, strLineNo);
                    }
                    catch { }

                    try
                    {
                        var objReportBO = new C1PrintPreviewDialogBO();
                        int nLine = int.Parse(dtbResult.Rows[0][WORKORDERLINE_COL].ToString());
                        string strWorkOrderNo = dtbResult.Rows[0][WORKORDERNO_COL].ToString();
                        objRB.DrawPredefinedField(REPORTFLD_CATEGORYCODE, objReportBO.GetCategoryCodeFromLineAndWorkOrderNo(nLine, strWorkOrderNo));
                        objRB.DrawPredefinedField(REPORTFLD_CATEGORYNAME, objReportBO.GetCategoryNameFromLineAndWorkOrderNo(nLine, strWorkOrderNo));
                        objRB.DrawPredefinedField(REPORTFLD_CATEGORYMODEL, objReportBO.GetProductModelFromLineAndWorkOrderNo(nLine, strWorkOrderNo));
                    }
                    catch { }

                    objRB.DrawPredefinedField(REPORTFLD_PURPOSE, PurposeText.Text);

                    try
                    {
                        string strFromLocationCode = dtbResult.Rows[0][LOCATIONCODE_COL].ToString();
                        objRB.DrawPredefinedField(REPORTFLD_FROMLOCATIONCODE, strFromLocationCode);

                        string strFromLocationName = objBO.GetLocationNameByLocationCode(strFromLocationCode);
                        objRB.DrawPredefinedField(REPORTFLD_FROMLOCATIONNAME, strFromLocationName);
                    }
                    catch { }

                    try
                    {
                        objRB.DrawPredefinedField(REPORTFLD_TOLOCATIONCODE, LocationText.Text);// ToLocationCode);					

                        string strToLocationName = objBO.GetLocationNameByLocationCode(LocationText.Text);
                        objRB.DrawPredefinedField(REPORTFLD_TOLOCATIONNAME, strToLocationName);
                    }
                    catch { }
                }

                objRB.DrawPredefinedField(REPORTFLD_PARTNUMBER, PRODUCTCODE_COL);
                objRB.DrawPredefinedField(REPORTFLD_PARTNAME, PRODUCTDESC_COL);
                objRB.DrawPredefinedField(REPORTFLD_UNIT, UNITCODE_COL);
                objRB.DrawPredefinedField(REPORTFLD_QTYACTUAL, COMMITQUANTITY_COL);
                objRB.DrawPredefinedField(REPORTFLD_SOURCE, LOT_COL);
                objRB.DrawPredefinedField(REPORTFLD_QTYPLAN, NEEDEDQUANTITY_COL);

                objRB.RefreshReport();

                try
                {
                    printPreview.FormTitle = objRB.GetFieldByName(REPORTFLD_TITLE).Text;
                }
                catch { }
                printPreview.Show();
                this.Cursor = Cursors.Default;

                #endregion
            }
            catch (Exception ex)
            {
                PCSMessageBox.Show(ErrorCode.OTHER_ERROR);
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
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// txtToBin_KeyDown
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Trada</author>
        /// <date>Wednesday, October 26 2005</date>
        private void txtToBin_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F4)
            {
                if (BinButton.Enabled)
                {
                    btnToBin_Click(null, null);
                }
            }
        }
        /// <summary>
        /// txtPurpose_Validating
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Trada</author>
        /// <date>Wednesday, October 26 2005</date>
        private void txtPurpose_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            const string METHOD_NAME = THIS + ".txtPurpose_Validating()";
            try
            {
                if (PurposeText.Text.Trim() == string.Empty)
                {
                    PurposeText.Tag = "0";
                    voPRO_IssueMaterialMasterVO.IssuePurposeID = 0;
                    strLastValidPurpose = string.Empty;
                    return;
                }

                if (strLastValidPurpose == PurposeText.Text.Trim()) return;

                //HACK: added by Tuan TQ. 18 Jan, 2006. apply purpose
                var htbCondition = new Hashtable();
                var iIssueMaterialTransID = (new UtilsBO()).GetTransTypeIDByCode(TransactionTypeEnum.PROIssueMaterial.ToString());
                htbCondition.Add(MST_TranTypeTable.TRANTYPEID_FLD, iIssueMaterialTransID);
                //End hack

                var drwResult = FormControlComponents.OpenSearchForm(PRO_IssuePurposeTable.TABLE_NAME, PRO_IssuePurposeTable.DESCRIPTION_FLD, PurposeText.Text.Trim(), htbCondition, false);
                if (drwResult != null)
                {
                    PurposeText.Text = drwResult[PRO_IssuePurposeTable.DESCRIPTION_FLD].ToString();
                    PurposeText.Tag = drwResult[PRO_IssuePurposeTable.ISSUEPURPOSEID_FLD];

                    voPRO_IssueMaterialMasterVO.IssuePurposeID = int.Parse(drwResult[PRO_IssuePurposeTable.ISSUEPURPOSEID_FLD].ToString());

                    strLastValidPurpose = PurposeText.Text.Trim();
                }
                else
                    e.Cancel = true;
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
        /// <summary>
        /// txtToLocation_Validating
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Trada</author>
        /// <date>Wednesday, October 26 2005</date>
        private void txtToLocation_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            const string METHOD_NAME = THIS + ".txtToLocation_Validating()";
            try
            {
                if (LocationText.Text.Trim() == string.Empty)
                {
                    LocationText.Tag = "0";
                    BinText.Text = string.Empty;
                    BinText.Tag = "0";
                    voPRO_IssueMaterialMasterVO.ToBinID = 0;
                    strLastValidLocation = string.Empty;
                    return;
                }

                if (strLastValidLocation == LocationText.Text.Trim()) return;

                var htbCriteria = new Hashtable();
                //User has entered MasLoc
                if (MasterLocationText.Text.Trim() != string.Empty)
                    htbCriteria.Add(PRO_IssueMaterialMasterTable.MASTERLOCATIONID_FLD, voPRO_IssueMaterialMasterVO.MasterLocationID);
                else //User has not entered MasLoc
                {
                    string[] msg = { lblMasLoc.Text, lblToLocation.Text };
                    PCSMessageBox.Show(ErrorCode.MESSAGE_SELECT_ONE_BEFORE_SELECT_ONE, MessageBoxIcon.Warning, msg);
                    e.Cancel = true;
                    return;
                }

                htbCriteria.Add(MST_LocationTable.LOCATIONTYPEID_FLD, (int)LocationTypeEnum.Manufacturing);
                var drwResult = FormControlComponents.OpenSearchForm(MST_LocationTable.TABLE_NAME, MST_LocationTable.CODE_FLD, LocationText.Text.Trim(), htbCriteria, false);
                if (drwResult != null)
                {
                    BinText.Enabled = BinButton.Enabled = Convert.ToBoolean(drwResult[MST_LocationTable.BIN_FLD]);

                    LocationText.Text = drwResult[MST_LocationTable.CODE_FLD].ToString();
                    LocationText.Tag = drwResult[MST_LocationTable.LOCATIONID_FLD];

                    voPRO_IssueMaterialMasterVO.ToLocationID = int.Parse(drwResult[MST_LocationTable.LOCATIONID_FLD].ToString());

                    if (strLastValidLocation != LocationText.Text)
                    {
                        BinText.Text = string.Empty;
                        BinText.Tag = "0";
                        voPRO_IssueMaterialMasterVO.ToBinID = 0;
                        strLastValidLocation = LocationText.Text;
                    }
                    btnToBin_Click(null, null);
                }
                else
                    e.Cancel = true;
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
        /// <summary>
        /// txtToBin_Validating
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Trada</author>
        /// <date>Wednesday, October 26 2005</date>
        private void txtToBin_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            const string METHOD_NAME = THIS + ".txtToBin_Validating()";
            try
            {
                if (BinText.Text.Trim() == string.Empty)
                {
                    BinText.Tag = "0";
                    strLastValidBin = string.Empty;
                    return;
                }

                if (strLastValidBin == BinText.Text.Trim()) return;

                var htbCriteria = new Hashtable();
                
                //User has entered ToLocation
                if (LocationText.Text.Trim() != string.Empty)
                    htbCriteria.Add(MST_LocationTable.LOCATIONID_FLD, voPRO_IssueMaterialMasterVO.ToLocationID);
                else //User has not entered ToLocation
                {
                    string[] msg = { lblToLocation.Text, lblToBin.Text };
                    PCSMessageBox.Show(ErrorCode.MESSAGE_SELECT_ONE_BEFORE_SELECT_ONE, MessageBoxIcon.Warning, msg);
                    e.Cancel = true;
                    return;
                }

                //HACK: Added by Tuan TQ. 19 Jan, 2006. Tobin must be Incoming
                htbCriteria.Add(MST_BINTable.BINTYPEID_FLD, (int)BinTypeEnum.IN);
                //End Hacked

                var drwResult = FormControlComponents.OpenSearchForm(MST_BINTable.TABLE_NAME, MST_BINTable.CODE_FLD, BinText.Text.Trim(), htbCriteria, false);
                if (drwResult != null)
                {
                    BinText.Text = drwResult[MST_BINTable.CODE_FLD].ToString();
                    BinText.Tag = drwResult[MST_BINTable.BINID_FLD];

                    voPRO_IssueMaterialMasterVO.ToBinID = int.Parse(drwResult[MST_BINTable.BINID_FLD].ToString());

                    strLastValidBin = BinText.Text;
                }
                else
                    e.Cancel = true;
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

        /// <summary>
        /// Thachnn : 15/Oct/2005
        /// Browse the DataTable, get all value of column with provided named.
        /// </summary>
        /// <param name="pdtb">DataTable to collect values</param>
        /// <param name="pstrColumnName">COlumn Name in pdtb DataTable to collect values from</param>
        /// <returns>ArrayList of object, collect from pdtb's column named pstrColumnName. Empty ArrayList if error or not found any row in pdtb.</returns>
        private static ArrayList GetColumnValuesFromTable(DataTable pdtb, string pstrColumnName)
        {
            var arrRet = new ArrayList();
            try
            {
                foreach (object objGet in
                    pdtb.Rows.Cast<DataRow>().Select(drow => drow[pstrColumnName]).Where(objGet => !arrRet.Contains(objGet)))
                {
                    arrRet.Add(objGet);
                }
            }
            catch
            {
                arrRet.Clear();
            }
            return arrRet;
        }
        /// <summary>
        /// btnShift_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Trada</author>
        /// <date>Wednesday, Nov 30 2005</date>
        private void btnShift_Click(object sender, System.EventArgs e)
        {
            const string METHOD_NAME = THIS + ".btnShift_Click()";
            try
            {
                var drwResult = FormControlComponents.OpenSearchForm(PRO_ShiftTable.TABLE_NAME, PRO_ShiftTable.SHIFTDESC_FLD, ShiftText.Text.Trim(), null, true);
                if (drwResult != null)
                {
                    ShiftText.Tag = drwResult[PRO_ShiftTable.SHIFTID_FLD];
                    ShiftText.Text = drwResult[PRO_ShiftTable.SHIFTDESC_FLD].ToString();
                    strLastValidShift = ShiftText.Text;
                }
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
        /// <summary>
        /// txtShift_Validating
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Trada</author>
        /// <date>Wednesday, Nov 30 2005</date>
        private void txtShift_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            const string METHOD_NAME = THIS + ".txtShift_Validating()";
            try
            {
                if (ShiftText.Text == string.Empty)
                {
                    ShiftText.Tag = "0";
                    strLastValidShift = string.Empty;
                    return;
                }

                if (strLastValidShift == ShiftText.Text.Trim()) return;

                var drwResult = FormControlComponents.OpenSearchForm(PRO_ShiftTable.TABLE_NAME, PRO_ShiftTable.SHIFTDESC_FLD, ShiftText.Text.Trim(), null, false);
                if (drwResult != null)
                {
                    ShiftText.Tag = drwResult[PRO_ShiftTable.SHIFTID_FLD];
                    ShiftText.Text = drwResult[PRO_ShiftTable.SHIFTDESC_FLD].ToString();
                    strLastValidShift = ShiftText.Text;
                }
                else
                    e.Cancel = true;
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

        /// <summary>
        /// Make btnPrintConfiguration always enable/disable like the PrintButton
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrint_EnabledChanged(object sender, System.EventArgs e)
        {
            btnPrintConfiguration.Enabled = ((Control)sender).Enabled;
        }

        private void txtShift_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F4)
            {
                if (ShiftButton.Enabled)
                {
                    btnShift_Click(null, null);
                }
            }
        }

        private void btnHelp_Click(object sender, System.EventArgs e)
        {
            
        }

        private void dtmPostDate_Validating(object sender, CancelEventArgs e)
        {
            const string METHOD_NAME = THIS + ".dtmPostDate_Validating()";
            try
            {
                if (PostDatePicker.Text.Trim() == string.Empty || PostDatePicker.ValueIsDbNull || mFormMode != EnumAction.Add)
                    return;
                //fill issue day
                FillIssueDate((DateTime)PostDatePicker.Value);
            }
            catch (Exception ex)
            {
                // error from date time control when user select date out of range
                if (ex.Message.Equals(PostDatePicker.PostValidation.ErrorMessage))
                    return;
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
        #endregion Event Processing

        private void dgrdData_Error(object sender, C1.Win.C1TrueDBGrid.ErrorEventArgs e)
        {
            e.Handled = true;
            e.Continue = true;
        }

        private void txtMasLoc_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            const string METHOD_NAME = THIS + ".txtMasLoc_Validating()";
            try
            {
                if (MasterLocationText.Text.Trim() == string.Empty)
                {
                    ChangeMasterLocation();
                    strLastValidMasLoc = string.Empty;
                    return;
                }
                if (strLastValidMasLoc == MasterLocationText.Text.Trim()) return;
                if (CCNCombo.SelectedValue == null)
                {
                    string[] msg = { lblCCN.Text, lblMasLoc.Text };
                    PCSMessageBox.Show(ErrorCode.MESSAGE_SELECT_ONE_BEFORE_SELECT_ONE, MessageBoxIcon.Warning, msg);
                    e.Cancel = true;
                    return;
                }
                var htCondition = new Hashtable
                                      {{MST_MasterLocationTable.CCNID_FLD, Convert.ToInt32(CCNCombo.SelectedValue)}};
                DataRowView drvResult = FormControlComponents.OpenSearchForm(MST_MasterLocationTable.TABLE_NAME, MST_MasterLocationTable.CODE_FLD, MasterLocationText.Text.Trim(), htCondition, false);
                if (drvResult != null)
                {
                    voPRO_IssueMaterialMasterVO.MasterLocationID = int.Parse(drvResult[MST_MasterLocationTable.MASTERLOCATIONID_FLD].ToString());
                    MasterLocationText.Text = drvResult[MST_MasterLocationTable.CODE_FLD].ToString();
                    MasterLocationText.Tag = drvResult[MST_MasterLocationTable.MASTERLOCATIONID_FLD];

                    // change master location
                    if (strLastValidMasLoc != MasterLocationText.Text.Trim())
                    {
                        voPRO_IssueMaterialMasterVO.ToBinID = 0;
                        voPRO_IssueMaterialMasterVO.ToLocationID = 0;

                        LoadDetailData(0);

                        ChangeMasterLocation();

                        strLastValidMasLoc = MasterLocationText.Text.Trim();
                    }
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
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION);
                }
            }
            catch (Exception ex)
            {
                PCSMessageBox.Show(ErrorCode.OTHER_ERROR);
                try
                {
                    Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
                }
                catch
                {
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION);
                }
            }
        }

        private void btnDelete_Click(object sender, System.EventArgs e)
        {
            const string METHOD_NAME = THIS + ".btnDelete_Click()";
            if (IssueNoText.Tag == null)
                return;
            if (PCSMessageBox.Show(ErrorCode.MESSAGE_DELETE_RECORD, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    FormMode = EnumAction.Delete;
                    // Delete transaction
                    var boMultiWOIssueMaterialBO = new MultiWOIssueMaterialBO();
                    boMultiWOIssueMaterialBO.DeleteTransaction(Convert.ToInt32(IssueNoText.Tag));

                    FormMode = EnumAction.Default;

                    //Turn to Add action
                    mFormMode = EnumAction.Default;

                    LockForm(true);
                    ClearForm();
                    EnableDisableButtons();
                    PCSMessageBox.Show(ErrorCode.MESSAGE_AFTER_SAVE_DATA, MessageBoxIcon.Information);
                    blnHasError = false;
                }
                catch (PCSException ex)
                {
                    if (ex.mCode == ErrorCode.MESSAGE_NOT_ENOUGH_COMPONENT_TO_COMPLETE)
                    {
                        //Show message
                        PCSMessageBox.Show(string.Format("Not enought quantity to delete for component {0}", ex.mMethod));
                        var productId = Convert.ToInt32(ex.Hash[ITM_ProductTable.PRODUCTID_FLD]);
                        // find in the grid and set focus
                        for (int i = 0; i < DetailGrid.RowCount; i++)
                        {
                            var product = Convert.ToInt32(DetailGrid[i, ITM_ProductTable.PRODUCTID_FLD]);
                            if (productId == product)
                            {
                                DetailGrid.Row = i;
                                DetailGrid.Col = DetailGrid.Columns.IndexOf(DetailGrid.Columns[PRO_IssueMaterialDetailTable.AVAILABLEQUANTITY_FLD]);
                                DetailGrid.Focus();
                            }
                        }
                    }
                    else
                    {
                        // displays the error message.
                        PCSMessageBox.Show(ex.mCode, MessageBoxIcon.Error);
                    }
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
        }

        private void txtIssueNo_Validating(object sender, CancelEventArgs e)
        {
            const string METHOD_NAME = THIS + ".btnFindTransNo_Click()";
            try
            {
                if (!IssueNoText.Modified) return;
                if (IssueNoText.Text.Trim() == string.Empty)
                {
                    IssueNoText.Text = string.Empty;
                    IssueNoText.Tag = null;

                    return;
                }

                var drwResult = FormControlComponents.OpenSearchForm(PRO_IssueMaterialMasterTable.TABLE_NAME, PRO_IssueMaterialMasterTable.ISSUENO_FLD, IssueNoText.Text.Trim(),null,false);
              
                if (drwResult != null)
                {
                    LoadData(drwResult.Row);
                }
                else
                {
                    IssueNoText.Focus();
                    IssueNoText.SelectAll();
                }
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
    }
}