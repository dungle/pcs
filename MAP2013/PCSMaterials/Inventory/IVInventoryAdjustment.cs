using System;
using System.Data;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

using PCSComUtils.Common;
using PCSComUtils.MasterSetup.DS;
using PCSComUtils.Common.BO;
using PCSComUtils.Framework.ReportFrame.BO;
using PCSComMaterials.Inventory.BO;
using PCSComMaterials.Inventory.DS;

using PCSComUtils.PCSExc;
using PCSUtils.Utils;
using PCSUtils.Log;
using PCSUtils.MasterSetup;
using PCSUtils.Framework.ReportFrame;

namespace PCSMaterials.Inventory
{
	/// <summary>
	/// Summary description for IVInventoryAdjustment.
	/// </summary>
	public class IVInventoryAdjustment : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label lblLocation;
		private System.Windows.Forms.Label lblMasLoc;
		private System.Windows.Forms.Label lblPostDate;
		private System.Windows.Forms.Label lblCCN;
		private System.Windows.Forms.Label lblBin;
		private System.Windows.Forms.Button btnClose;
		private System.Windows.Forms.Button btnHelp;
		private System.Windows.Forms.Button btnPrint;
		private System.Windows.Forms.Button btnSave;
		private System.Windows.Forms.Label lblUM;
		private System.Windows.Forms.TextBox txtModel;
		private System.Windows.Forms.Button btnPartNumber;
		private System.Windows.Forms.TextBox txtPartNumber;
		private System.Windows.Forms.Label lblPartNumber;
		private System.Windows.Forms.Label lblModel;
		private System.Windows.Forms.TextBox txtComment;
		private System.Windows.Forms.Label lblComment;
		private System.Windows.Forms.Button btnPartName;
		private System.Windows.Forms.TextBox txtPartName;
		private System.Windows.Forms.Label lblPartName;
		private System.Windows.Forms.TextBox txtSerial;
		private System.Windows.Forms.Label lblSerial;
		private System.Windows.Forms.TextBox txtLot;
		private System.Windows.Forms.Label lblLot;
		private System.Windows.Forms.Button btnTransNo;
		private System.Windows.Forms.TextBox txtTransNo;
		private System.Windows.Forms.Label lblTransNo;
		private System.Windows.Forms.Label lblAdjustQuantity;
		private System.Windows.Forms.Label lblAvailQuantity;
		private System.Windows.Forms.Button btnAdd;
		private C1.Win.C1Input.C1DateEdit dtmPostDate;
		private C1.Win.C1List.C1Combo cboCCN;
		private System.Windows.Forms.Button btnMasLoc;
		private System.Windows.Forms.Button btnLocation;
		private System.Windows.Forms.Button btnBin;
		private System.Windows.Forms.Button btnLot;
		private System.Windows.Forms.Label lblQAStatus;
		private System.Windows.Forms.TextBox txtQAStatus;
		private System.Windows.Forms.TextBox txtMasLoc;
		private System.Windows.Forms.TextBox txtLocation;
		private System.Windows.Forms.TextBox txtBin;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		const string THIS = "PCSMaterials.Inventory.IVInventoryAdjustment";
		EnumAction formMode;
		UtilsBO boUtil = new UtilsBO();
		private const string V_PRODUCTINVENTORYADJUSTMENT = "V_ProductInventoryAdjustment";
		private const string V_MASTERLOCATIONITEM = "V_MasterLocationItem";
		private const string V_LOCATIONITEM = "V_LocationItem";
		private const string V_BINITEM = "V_BinItem";
		private const string V_LOTBYBIN = "V_LOTBYBIN";
		private const string V_LOTBYLOC = "V_LOTBYLOC";
		private const string V_IVADJUSTMENTANDPRODUCT = "V_IVAdjustmentAndProduct";
		private const string PARTNUMBER = "PartNumber";
		private const string PARTNAME = "PartName";
		private const string MODEL = "Model";
		private const string PRODUCT_CODE = "ProductCode";
		private const string MASLOCNAME = "MasLocName";
		private const string LOCNAME = "LocName";
		private const string BINCONTROL = "BinControl";
		private const string BINNAME = "BinName";
		private const string LOCQASTATUS = "LocQAStatus";
		private const string BINQASTATUS = "BinQAStatus";
		private const string LOCAVAILABLE_QUANTITY = "LocAvailableQuantity";
		private const string BIN_AVAILABLE_QUANTITY = "BinAvailableQuantity";
		private const string UM = "UM";
		private const string TRUE = "True";
		private const string AVAILABLE_QUANTITY = "AvailQuantity";
		private const string ISSUE_QUANTITY = "IssueQuantity";
		private bool blnLotControl = false;
		private bool blnBinControl = false;
		private bool blnHasError = false;
		private int intLotSize;
		private DataSet dstAvailableQtyAndInsStatus;
		DateTime dtmCurrentDate = new DateTime();
		private IV_AdjustmentVO voIV_Adjustment = new IV_AdjustmentVO();
		private C1.Win.C1Input.C1NumericEdit txtAdjustQuantity;
		private C1.Win.C1Input.C1NumericEdit txtAvailQuantity;
		private System.Windows.Forms.TextBox txtUM;
		private System.Windows.Forms.CheckBox chkUsedByCosting;
		private System.Windows.Forms.Button btnDelete;
		private System.Windows.Forms.Button btnDelTran;
				
		private System.ComponentModel.Container components = null;

		public IVInventoryAdjustment()
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
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.lblLocation = new System.Windows.Forms.Label();
			this.lblMasLoc = new System.Windows.Forms.Label();
			this.lblPostDate = new System.Windows.Forms.Label();
			this.lblCCN = new System.Windows.Forms.Label();
			this.lblBin = new System.Windows.Forms.Label();
			this.btnClose = new System.Windows.Forms.Button();
			this.btnHelp = new System.Windows.Forms.Button();
			this.btnPrint = new System.Windows.Forms.Button();
			this.btnSave = new System.Windows.Forms.Button();
			this.lblUM = new System.Windows.Forms.Label();
			this.txtModel = new System.Windows.Forms.TextBox();
			this.btnPartNumber = new System.Windows.Forms.Button();
			this.txtPartNumber = new System.Windows.Forms.TextBox();
			this.lblPartNumber = new System.Windows.Forms.Label();
			this.lblModel = new System.Windows.Forms.Label();
			this.txtComment = new System.Windows.Forms.TextBox();
			this.lblComment = new System.Windows.Forms.Label();
			this.btnPartName = new System.Windows.Forms.Button();
			this.txtPartName = new System.Windows.Forms.TextBox();
			this.lblPartName = new System.Windows.Forms.Label();
			this.txtSerial = new System.Windows.Forms.TextBox();
			this.lblSerial = new System.Windows.Forms.Label();
			this.txtLot = new System.Windows.Forms.TextBox();
			this.lblLot = new System.Windows.Forms.Label();
			this.btnTransNo = new System.Windows.Forms.Button();
			this.txtTransNo = new System.Windows.Forms.TextBox();
			this.lblTransNo = new System.Windows.Forms.Label();
			this.lblAdjustQuantity = new System.Windows.Forms.Label();
			this.lblAvailQuantity = new System.Windows.Forms.Label();
			this.btnAdd = new System.Windows.Forms.Button();
			this.dtmPostDate = new C1.Win.C1Input.C1DateEdit();
			this.cboCCN = new C1.Win.C1List.C1Combo();
			this.btnMasLoc = new System.Windows.Forms.Button();
			this.txtMasLoc = new System.Windows.Forms.TextBox();
			this.btnLocation = new System.Windows.Forms.Button();
			this.txtLocation = new System.Windows.Forms.TextBox();
			this.btnBin = new System.Windows.Forms.Button();
			this.txtBin = new System.Windows.Forms.TextBox();
			this.btnLot = new System.Windows.Forms.Button();
			this.lblQAStatus = new System.Windows.Forms.Label();
			this.txtQAStatus = new System.Windows.Forms.TextBox();
			this.txtAdjustQuantity = new C1.Win.C1Input.C1NumericEdit();
			this.txtAvailQuantity = new C1.Win.C1Input.C1NumericEdit();
			this.txtUM = new System.Windows.Forms.TextBox();
			this.chkUsedByCosting = new System.Windows.Forms.CheckBox();
			this.btnDelete = new System.Windows.Forms.Button();
			this.btnDelTran = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.dtmPostDate)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.cboCCN)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.txtAdjustQuantity)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.txtAvailQuantity)).BeginInit();
			this.SuspendLayout();
			// 
			// lblLocation
			// 
			this.lblLocation.ForeColor = System.Drawing.Color.Maroon;
			this.lblLocation.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lblLocation.Location = new System.Drawing.Point(8, 136);
			this.lblLocation.Name = "lblLocation";
			this.lblLocation.Size = new System.Drawing.Size(82, 20);
			this.lblLocation.TabIndex = 22;
			this.lblLocation.Text = "Location";
			this.lblLocation.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblMasLoc
			// 
			this.lblMasLoc.ForeColor = System.Drawing.Color.Maroon;
			this.lblMasLoc.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lblMasLoc.Location = new System.Drawing.Point(8, 114);
			this.lblMasLoc.Name = "lblMasLoc";
			this.lblMasLoc.Size = new System.Drawing.Size(82, 20);
			this.lblMasLoc.TabIndex = 19;
			this.lblMasLoc.Text = "Mas. Location";
			this.lblMasLoc.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblPostDate
			// 
			this.lblPostDate.ForeColor = System.Drawing.Color.Maroon;
			this.lblPostDate.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lblPostDate.Location = new System.Drawing.Point(8, 4);
			this.lblPostDate.Name = "lblPostDate";
			this.lblPostDate.Size = new System.Drawing.Size(82, 20);
			this.lblPostDate.TabIndex = 2;
			this.lblPostDate.Text = "Post Date";
			this.lblPostDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblCCN
			// 
			this.lblCCN.ForeColor = System.Drawing.Color.Maroon;
			this.lblCCN.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lblCCN.Location = new System.Drawing.Point(430, 4);
			this.lblCCN.Name = "lblCCN";
			this.lblCCN.Size = new System.Drawing.Size(32, 19);
			this.lblCCN.TabIndex = 1;
			this.lblCCN.Text = "CCN";
			this.lblCCN.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblBin
			// 
			this.lblBin.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lblBin.Location = new System.Drawing.Point(282, 136);
			this.lblBin.Name = "lblBin";
			this.lblBin.Size = new System.Drawing.Size(36, 20);
			this.lblBin.TabIndex = 25;
			this.lblBin.Text = "Bin";
			this.lblBin.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// btnClose
			// 
			this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnClose.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnClose.Location = new System.Drawing.Point(483, 250);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(60, 23);
			this.btnClose.TabIndex = 43;
			this.btnClose.Text = "&Close";
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			// 
			// btnHelp
			// 
			this.btnHelp.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnHelp.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnHelp.Location = new System.Drawing.Point(422, 250);
			this.btnHelp.Name = "btnHelp";
			this.btnHelp.Size = new System.Drawing.Size(60, 23);
			this.btnHelp.TabIndex = 42;
			this.btnHelp.Text = "&Help";
			this.btnHelp.Click += new System.EventHandler(this.btnHelp_Click);
			// 
			// btnPrint
			// 
			this.btnPrint.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnPrint.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnPrint.Location = new System.Drawing.Point(298, 250);
			this.btnPrint.Name = "btnPrint";
			this.btnPrint.Size = new System.Drawing.Size(60, 23);
			this.btnPrint.TabIndex = 41;
			this.btnPrint.Text = "&Print";
			this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
			// 
			// btnSave
			// 
			this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnSave.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnSave.Location = new System.Drawing.Point(68, 250);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(60, 23);
			this.btnSave.TabIndex = 40;
			this.btnSave.Text = "&Save";
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			// 
			// lblUM
			// 
			this.lblUM.ForeColor = System.Drawing.Color.Black;
			this.lblUM.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lblUM.Location = new System.Drawing.Point(458, 92);
			this.lblUM.Name = "lblUM";
			this.lblUM.Size = new System.Drawing.Size(26, 19);
			this.lblUM.TabIndex = 17;
			this.lblUM.Text = "UM";
			this.lblUM.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// txtModel
			// 
			this.txtModel.Location = new System.Drawing.Point(318, 70);
			this.txtModel.Name = "txtModel";
			this.txtModel.Size = new System.Drawing.Size(102, 20);
			this.txtModel.TabIndex = 13;
			this.txtModel.Text = "";
			// 
			// btnPartNumber
			// 
			this.btnPartNumber.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnPartNumber.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnPartNumber.Location = new System.Drawing.Point(232, 70);
			this.btnPartNumber.Name = "btnPartNumber";
			this.btnPartNumber.Size = new System.Drawing.Size(24, 20);
			this.btnPartNumber.TabIndex = 11;
			this.btnPartNumber.Text = "...";
			this.btnPartNumber.Click += new System.EventHandler(this.btnPartNumber_Click);
			// 
			// txtPartNumber
			// 
			this.txtPartNumber.Location = new System.Drawing.Point(88, 70);
			this.txtPartNumber.Name = "txtPartNumber";
			this.txtPartNumber.Size = new System.Drawing.Size(142, 20);
			this.txtPartNumber.TabIndex = 10;
			this.txtPartNumber.Text = "";
			this.txtPartNumber.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPartNumber_KeyDown);
			this.txtPartNumber.Validating += new System.ComponentModel.CancelEventHandler(this.txtPartNumber_Validating);
			// 
			// lblPartNumber
			// 
			this.lblPartNumber.ForeColor = System.Drawing.Color.Maroon;
			this.lblPartNumber.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lblPartNumber.Location = new System.Drawing.Point(8, 70);
			this.lblPartNumber.Name = "lblPartNumber";
			this.lblPartNumber.Size = new System.Drawing.Size(82, 20);
			this.lblPartNumber.TabIndex = 9;
			this.lblPartNumber.Text = "Part Number";
			this.lblPartNumber.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblModel
			// 
			this.lblModel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lblModel.Location = new System.Drawing.Point(282, 70);
			this.lblModel.Name = "lblModel";
			this.lblModel.Size = new System.Drawing.Size(36, 20);
			this.lblModel.TabIndex = 12;
			this.lblModel.Text = "Model";
			this.lblModel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// txtComment
			// 
			this.txtComment.Location = new System.Drawing.Point(88, 48);
			this.txtComment.MaxLength = 200;
			this.txtComment.Name = "txtComment";
			this.txtComment.Size = new System.Drawing.Size(456, 20);
			this.txtComment.TabIndex = 8;
			this.txtComment.Text = "";
			// 
			// lblComment
			// 
			this.lblComment.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lblComment.Location = new System.Drawing.Point(8, 48);
			this.lblComment.Name = "lblComment";
			this.lblComment.Size = new System.Drawing.Size(82, 20);
			this.lblComment.TabIndex = 7;
			this.lblComment.Text = "Comment";
			this.lblComment.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// btnPartName
			// 
			this.btnPartName.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnPartName.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnPartName.Location = new System.Drawing.Point(421, 92);
			this.btnPartName.Name = "btnPartName";
			this.btnPartName.Size = new System.Drawing.Size(24, 20);
			this.btnPartName.TabIndex = 16;
			this.btnPartName.Text = "...";
			this.btnPartName.Click += new System.EventHandler(this.btnPartName_Click);
			// 
			// txtPartName
			// 
			this.txtPartName.Location = new System.Drawing.Point(88, 92);
			this.txtPartName.Name = "txtPartName";
			this.txtPartName.Size = new System.Drawing.Size(332, 20);
			this.txtPartName.TabIndex = 15;
			this.txtPartName.Text = "";
			this.txtPartName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPartName_KeyDown);
			this.txtPartName.Validating += new System.ComponentModel.CancelEventHandler(this.txtPartName_Validating);
			// 
			// lblPartName
			// 
			this.lblPartName.ForeColor = System.Drawing.Color.Maroon;
			this.lblPartName.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lblPartName.Location = new System.Drawing.Point(8, 92);
			this.lblPartName.Name = "lblPartName";
			this.lblPartName.Size = new System.Drawing.Size(82, 20);
			this.lblPartName.TabIndex = 14;
			this.lblPartName.Text = "Part Name";
			this.lblPartName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// txtSerial
			// 
			this.txtSerial.Location = new System.Drawing.Point(319, 158);
			this.txtSerial.Name = "txtSerial";
			this.txtSerial.Size = new System.Drawing.Size(101, 20);
			this.txtSerial.TabIndex = 32;
			this.txtSerial.Text = "";
			// 
			// lblSerial
			// 
			this.lblSerial.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lblSerial.Location = new System.Drawing.Point(282, 158);
			this.lblSerial.Name = "lblSerial";
			this.lblSerial.Size = new System.Drawing.Size(36, 20);
			this.lblSerial.TabIndex = 31;
			this.lblSerial.Text = "Serial";
			this.lblSerial.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// txtLot
			// 
			this.txtLot.Location = new System.Drawing.Point(88, 158);
			this.txtLot.Name = "txtLot";
			this.txtLot.Size = new System.Drawing.Size(112, 20);
			this.txtLot.TabIndex = 29;
			this.txtLot.Text = "";
			this.txtLot.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtLot_KeyDown);
			this.txtLot.Leave += new System.EventHandler(this.txtLot_Leave);
			// 
			// lblLot
			// 
			this.lblLot.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lblLot.Location = new System.Drawing.Point(8, 158);
			this.lblLot.Name = "lblLot";
			this.lblLot.Size = new System.Drawing.Size(82, 20);
			this.lblLot.TabIndex = 28;
			this.lblLot.Text = "Lot";
			this.lblLot.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// btnTransNo
			// 
			this.btnTransNo.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnTransNo.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnTransNo.Location = new System.Drawing.Point(219, 26);
			this.btnTransNo.Name = "btnTransNo";
			this.btnTransNo.Size = new System.Drawing.Size(24, 20);
			this.btnTransNo.TabIndex = 6;
			this.btnTransNo.Text = "...";
			this.btnTransNo.Click += new System.EventHandler(this.btnTransNo_Click);
			// 
			// txtTransNo
			// 
			this.txtTransNo.Location = new System.Drawing.Point(88, 26);
			this.txtTransNo.MaxLength = 20;
			this.txtTransNo.Name = "txtTransNo";
			this.txtTransNo.Size = new System.Drawing.Size(130, 20);
			this.txtTransNo.TabIndex = 5;
			this.txtTransNo.Text = "";
			this.txtTransNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtTransNo_KeyDown);
			this.txtTransNo.Leave += new System.EventHandler(this.txtTransNo_Leave);
			// 
			// lblTransNo
			// 
			this.lblTransNo.ForeColor = System.Drawing.Color.Maroon;
			this.lblTransNo.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lblTransNo.Location = new System.Drawing.Point(8, 26);
			this.lblTransNo.Name = "lblTransNo";
			this.lblTransNo.Size = new System.Drawing.Size(82, 20);
			this.lblTransNo.TabIndex = 4;
			this.lblTransNo.Text = "Trans. No.";
			this.lblTransNo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblAdjustQuantity
			// 
			this.lblAdjustQuantity.ForeColor = System.Drawing.Color.Maroon;
			this.lblAdjustQuantity.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lblAdjustQuantity.Location = new System.Drawing.Point(8, 180);
			this.lblAdjustQuantity.Name = "lblAdjustQuantity";
			this.lblAdjustQuantity.Size = new System.Drawing.Size(82, 20);
			this.lblAdjustQuantity.TabIndex = 33;
			this.lblAdjustQuantity.Text = "Adjust Quantity";
			this.lblAdjustQuantity.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblAvailQuantity
			// 
			this.lblAvailQuantity.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblAvailQuantity.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lblAvailQuantity.Location = new System.Drawing.Point(8, 202);
			this.lblAvailQuantity.Name = "lblAvailQuantity";
			this.lblAvailQuantity.Size = new System.Drawing.Size(82, 20);
			this.lblAvailQuantity.TabIndex = 37;
			this.lblAvailQuantity.Text = "Avai. Quantity";
			this.lblAvailQuantity.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// btnAdd
			// 
			this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnAdd.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnAdd.Location = new System.Drawing.Point(7, 250);
			this.btnAdd.Name = "btnAdd";
			this.btnAdd.Size = new System.Drawing.Size(60, 23);
			this.btnAdd.TabIndex = 39;
			this.btnAdd.Text = "&Add";
			this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
			// 
			// dtmPostDate
			// 
			// 
			// dtmPostDate.Calendar
			// 
			this.dtmPostDate.Calendar.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.dtmPostDate.Calendar.MaxDate = new System.DateTime(2100, 12, 31, 0, 0, 0, 0);
			this.dtmPostDate.Calendar.ShowClearButton = false;
			this.dtmPostDate.CustomFormat = "dd-MM-yyyy";
			this.dtmPostDate.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
			this.dtmPostDate.Location = new System.Drawing.Point(88, 4);
			this.dtmPostDate.Name = "dtmPostDate";
			this.dtmPostDate.PostValidation.Intervals.AddRange(new C1.Win.C1Input.ValueInterval[] {
																									  new C1.Win.C1Input.ValueInterval(new System.DateTime(1753, 1, 1, 0, 0, 0, 0), new System.DateTime(2100, 12, 31, 12, 0, 0, 0), true, true)});
			this.dtmPostDate.Size = new System.Drawing.Size(130, 20);
			this.dtmPostDate.TabIndex = 3;
			this.dtmPostDate.Tag = null;
			this.dtmPostDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.dtmPostDate.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.DropDown;
			this.dtmPostDate.Leave += new System.EventHandler(this.dtmPostDate_Leave);
			// 
			// cboCCN
			// 
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
			this.cboCCN.Location = new System.Drawing.Point(464, 4);
			this.cboCCN.MatchEntryTimeout = ((long)(2000));
			this.cboCCN.MaxDropDownItems = ((short)(5));
			this.cboCCN.MaxLength = 32767;
			this.cboCCN.MouseCursor = System.Windows.Forms.Cursors.Default;
			this.cboCCN.Name = "cboCCN";
			this.cboCCN.RowDivider.Color = System.Drawing.Color.DarkGray;
			this.cboCCN.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None;
			this.cboCCN.RowSubDividerColor = System.Drawing.Color.DarkGray;
			this.cboCCN.Size = new System.Drawing.Size(80, 21);
			this.cboCCN.TabIndex = 0;
			this.cboCCN.PropBag = "<?xml version=\"1.0\"?><Blob><Styles type=\"C1.Win.C1List.Design.ContextWrapper\"><Da" +
				"ta>Group{AlignVert:Center;Border:None,,0, 0, 0, 0;BackColor:ControlDark;}Style2{" +
				"}Style5{}Style4{}Style7{}Style6{}EvenRow{BackColor:Aqua;}Selected{ForeColor:High" +
				"lightText;BackColor:Highlight;}Style3{}Inactive{ForeColor:InactiveCaptionText;Ba" +
				"ckColor:InactiveCaption;}Footer{}Caption{AlignHorz:Center;}Normal{BackColor:Wind" +
				"ow;}HighlightRow{ForeColor:HighlightText;BackColor:Highlight;}Style1{}OddRow{}Re" +
				"cordSelector{AlignImage:Center;}Heading{Wrap:True;BackColor:Control;Border:Raise" +
				"d,,1, 1, 1, 1;ForeColor:ControlText;AlignVert:Center;}Style8{}Style10{}Style11{}" +
				"Style9{AlignHorz:Near;}</Data></Styles><Splits><C1.Win.C1List.ListBoxView AllowC" +
				"olSelect=\"False\" Name=\"\" CaptionHeight=\"17\" ColumnCaptionHeight=\"17\" ColumnFoote" +
				"rHeight=\"17\" VerticalScrollGroup=\"1\" HorizontalScrollGroup=\"1\"><ClientRect>0, 0," +
				" 116, 156</ClientRect><VScrollBar><Width>16</Width></VScrollBar><HScrollBar><Hei" +
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
				"th>16</DefaultRecSelWidth></Blob>";
			// 
			// btnMasLoc
			// 
			this.btnMasLoc.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnMasLoc.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnMasLoc.Location = new System.Drawing.Point(202, 114);
			this.btnMasLoc.Name = "btnMasLoc";
			this.btnMasLoc.Size = new System.Drawing.Size(24, 20);
			this.btnMasLoc.TabIndex = 21;
			this.btnMasLoc.Text = "...";
			this.btnMasLoc.Click += new System.EventHandler(this.btnMasLoc_Click);
			// 
			// txtMasLoc
			// 
			this.txtMasLoc.Location = new System.Drawing.Point(88, 114);
			this.txtMasLoc.Name = "txtMasLoc";
			this.txtMasLoc.Size = new System.Drawing.Size(112, 20);
			this.txtMasLoc.TabIndex = 20;
			this.txtMasLoc.Text = "";
			this.txtMasLoc.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtMasLoc_KeyDown);
			this.txtMasLoc.Validating += new System.ComponentModel.CancelEventHandler(this.txtMasLoc_Validating);
			// 
			// btnLocation
			// 
			this.btnLocation.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnLocation.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnLocation.Location = new System.Drawing.Point(202, 136);
			this.btnLocation.Name = "btnLocation";
			this.btnLocation.Size = new System.Drawing.Size(24, 20);
			this.btnLocation.TabIndex = 24;
			this.btnLocation.Text = "...";
			this.btnLocation.Click += new System.EventHandler(this.btnLocation_Click);
			// 
			// txtLocation
			// 
			this.txtLocation.Location = new System.Drawing.Point(88, 136);
			this.txtLocation.Name = "txtLocation";
			this.txtLocation.Size = new System.Drawing.Size(112, 20);
			this.txtLocation.TabIndex = 23;
			this.txtLocation.Text = "";
			this.txtLocation.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtLocation_KeyDown);
			this.txtLocation.Validating += new System.ComponentModel.CancelEventHandler(this.txtLocation_Validating);
			// 
			// btnBin
			// 
			this.btnBin.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnBin.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnBin.Location = new System.Drawing.Point(421, 136);
			this.btnBin.Name = "btnBin";
			this.btnBin.Size = new System.Drawing.Size(24, 20);
			this.btnBin.TabIndex = 27;
			this.btnBin.Text = "...";
			this.btnBin.Click += new System.EventHandler(this.btnBin_Click);
			// 
			// txtBin
			// 
			this.txtBin.Location = new System.Drawing.Point(319, 136);
			this.txtBin.Name = "txtBin";
			this.txtBin.Size = new System.Drawing.Size(101, 20);
			this.txtBin.TabIndex = 26;
			this.txtBin.Text = "";
			this.txtBin.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBin_KeyDown);
			this.txtBin.Validating += new System.ComponentModel.CancelEventHandler(this.txtBin_Validating);
			// 
			// btnLot
			// 
			this.btnLot.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnLot.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnLot.Location = new System.Drawing.Point(202, 159);
			this.btnLot.Name = "btnLot";
			this.btnLot.Size = new System.Drawing.Size(24, 20);
			this.btnLot.TabIndex = 30;
			this.btnLot.Text = "...";
			this.btnLot.Click += new System.EventHandler(this.btnLot_Click);
			// 
			// lblQAStatus
			// 
			this.lblQAStatus.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lblQAStatus.Location = new System.Drawing.Point(282, 180);
			this.lblQAStatus.Name = "lblQAStatus";
			this.lblQAStatus.Size = new System.Drawing.Size(36, 20);
			this.lblQAStatus.TabIndex = 35;
			this.lblQAStatus.Text = "Status";
			this.lblQAStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// txtQAStatus
			// 
			this.txtQAStatus.Location = new System.Drawing.Point(319, 180);
			this.txtQAStatus.Name = "txtQAStatus";
			this.txtQAStatus.Size = new System.Drawing.Size(101, 20);
			this.txtQAStatus.TabIndex = 36;
			this.txtQAStatus.Text = "";
			// 
			// txtAdjustQuantity
			// 
			this.txtAdjustQuantity.EmptyAsNull = true;
			this.txtAdjustQuantity.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
			this.txtAdjustQuantity.Location = new System.Drawing.Point(88, 180);
			this.txtAdjustQuantity.Name = "txtAdjustQuantity";
			this.txtAdjustQuantity.Size = new System.Drawing.Size(112, 20);
			this.txtAdjustQuantity.TabIndex = 34;
			this.txtAdjustQuantity.Tag = null;
			this.txtAdjustQuantity.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.txtAdjustQuantity.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.None;
			// 
			// txtAvailQuantity
			// 
			this.txtAvailQuantity.EmptyAsNull = true;
			this.txtAvailQuantity.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
			this.txtAvailQuantity.Location = new System.Drawing.Point(88, 202);
			this.txtAvailQuantity.Name = "txtAvailQuantity";
			this.txtAvailQuantity.Size = new System.Drawing.Size(112, 20);
			this.txtAvailQuantity.TabIndex = 44;
			this.txtAvailQuantity.Tag = null;
			this.txtAvailQuantity.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.txtAvailQuantity.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.None;
			// 
			// txtUM
			// 
			this.txtUM.Enabled = false;
			this.txtUM.Location = new System.Drawing.Point(484, 92);
			this.txtUM.Name = "txtUM";
			this.txtUM.ReadOnly = true;
			this.txtUM.Size = new System.Drawing.Size(60, 20);
			this.txtUM.TabIndex = 18;
			this.txtUM.TabStop = false;
			this.txtUM.Text = "";
			// 
			// chkUsedByCosting
			// 
			this.chkUsedByCosting.Location = new System.Drawing.Point(282, 202);
			this.chkUsedByCosting.Name = "chkUsedByCosting";
			this.chkUsedByCosting.Size = new System.Drawing.Size(138, 20);
			this.chkUsedByCosting.TabIndex = 45;
			this.chkUsedByCosting.Text = "Used By Costing";
			// 
			// btnDelete
			// 
			this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnDelete.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnDelete.Location = new System.Drawing.Point(130, 250);
			this.btnDelete.Name = "btnDelete";
			this.btnDelete.Size = new System.Drawing.Size(60, 23);
			this.btnDelete.TabIndex = 40;
			this.btnDelete.Text = "&Delete";
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
			// 
			// btnDelTran
			// 
			this.btnDelTran.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnDelTran.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnDelTran.Location = new System.Drawing.Point(192, 250);
			this.btnDelTran.Name = "btnDelTran";
			this.btnDelTran.Size = new System.Drawing.Size(104, 23);
			this.btnDelTran.TabIndex = 40;
			this.btnDelTran.Text = "&Delete Transaction";
			this.btnDelTran.Click += new System.EventHandler(this.btnDelTran_Click);
			// 
			// IVInventoryAdjustment
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.btnClose;
			this.ClientSize = new System.Drawing.Size(550, 280);
			this.Controls.Add(this.chkUsedByCosting);
			this.Controls.Add(this.txtUM);
			this.Controls.Add(this.txtBin);
			this.Controls.Add(this.txtLocation);
			this.Controls.Add(this.txtMasLoc);
			this.Controls.Add(this.txtTransNo);
			this.Controls.Add(this.txtSerial);
			this.Controls.Add(this.txtLot);
			this.Controls.Add(this.txtPartName);
			this.Controls.Add(this.txtComment);
			this.Controls.Add(this.txtModel);
			this.Controls.Add(this.txtPartNumber);
			this.Controls.Add(this.txtQAStatus);
			this.Controls.Add(this.txtAvailQuantity);
			this.Controls.Add(this.txtAdjustQuantity);
			this.Controls.Add(this.lblQAStatus);
			this.Controls.Add(this.btnLot);
			this.Controls.Add(this.btnBin);
			this.Controls.Add(this.btnLocation);
			this.Controls.Add(this.btnMasLoc);
			this.Controls.Add(this.dtmPostDate);
			this.Controls.Add(this.cboCCN);
			this.Controls.Add(this.btnAdd);
			this.Controls.Add(this.lblAvailQuantity);
			this.Controls.Add(this.lblAdjustQuantity);
			this.Controls.Add(this.btnTransNo);
			this.Controls.Add(this.lblTransNo);
			this.Controls.Add(this.lblSerial);
			this.Controls.Add(this.lblLot);
			this.Controls.Add(this.btnPartName);
			this.Controls.Add(this.lblPartName);
			this.Controls.Add(this.lblComment);
			this.Controls.Add(this.lblUM);
			this.Controls.Add(this.btnPartNumber);
			this.Controls.Add(this.lblPartNumber);
			this.Controls.Add(this.lblModel);
			this.Controls.Add(this.btnSave);
			this.Controls.Add(this.btnClose);
			this.Controls.Add(this.btnHelp);
			this.Controls.Add(this.btnPrint);
			this.Controls.Add(this.lblCCN);
			this.Controls.Add(this.lblPostDate);
			this.Controls.Add(this.lblLocation);
			this.Controls.Add(this.lblMasLoc);
			this.Controls.Add(this.lblBin);
			this.Controls.Add(this.btnDelete);
			this.Controls.Add(this.btnDelTran);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.KeyPreview = true;
			this.MaximizeBox = false;
			this.Name = "IVInventoryAdjustment";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Inventory Adjustment";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.IVInventoryAdjustment_Closing);
			this.Load += new System.EventHandler(this.IVInventoryAddjustment_Load);
			((System.ComponentModel.ISupportInitialize)(this.dtmPostDate)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.cboCCN)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.txtAdjustQuantity)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.txtAvailQuantity)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion
		/// <summary>
		/// IVInventoryAddjustment_Load
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Friday, July 22 2005</date>
		private void IVInventoryAddjustment_Load(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".IVInventoryAddjustment_Load()";
			try
			{
				//Set authorization for user
				Security objSecurity = new Security();
				this.Name = THIS;
				if(objSecurity.SetRightForUserOnForm(this, SystemProperty.UserName) == 0)
				{
					this.Close();
					// You don't have the right to view this item
					PCSMessageBox.Show(ErrorCode.MESSAGE_YOU_HAVE_NO_RIGHT_TO_VIEW ,MessageBoxIcon.Warning);
					return;
				}
				// Load combo box
				
				DataSet dstCCN = boUtil.ListCCN();
				cboCCN.DataSource = dstCCN.Tables[MST_CCNTable.TABLE_NAME];
				cboCCN.DisplayMember = MST_CCNTable.CODE_FLD;
				cboCCN.ValueMember = MST_CCNTable.CCNID_FLD;
				FormControlComponents.PutDataIntoC1ComboBox(cboCCN, dstCCN.Tables[MST_CCNTable.TABLE_NAME],MST_CCNTable.CODE_FLD,MST_CCNTable.CCNID_FLD,MST_CCNTable.TABLE_NAME);
				if (SystemProperty.CCNID != 0)
				{
					cboCCN.SelectedValue = SystemProperty.CCNID;
				}
				//Set NumberFormat for textbox.
//				txtAvailQuantity.CustomFormat = Constants.EDIT_NUM_MASK;
//				txtAdjustQuantity.CustomFormat = Constants.EDIT_NUM_MASK;
				dtmPostDate.CustomFormat = Constants.DATETIME_FORMAT_HOUR;	
				dtmCurrentDate = (new UtilsBO()).GetDBDate().AddDays(1);
				//switch form mode
				formMode = EnumAction.Default;
				SwitchFormMode();
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
		/// ClearForm
		/// </summary>
		/// <author>Trada</author>
		/// <date>Friday, July 22 2005</date>
		private void ClearForm()
		{	
			const string METHOD_NAME = THIS + ".ClearForm()";
			try
			{
				txtAdjustQuantity.Value = null;
				txtAvailQuantity.Value = null;
				txtComment.Text = string.Empty;
				txtLot.Text = string.Empty;
				txtModel.Text = string.Empty;
				txtPartName.Text = string.Empty;
				txtPartNumber.Text = string.Empty;
				txtQAStatus.Text = string.Empty;
				txtSerial.Text = string.Empty;
				txtTransNo.Text = string.Empty;
				txtMasLoc.Text = string.Empty;
				txtLocation.Text = string.Empty;
				txtBin.Text = string.Empty;
				txtUM.Text = string.Empty;
				chkUsedByCosting.Checked = false;
			}
			catch (PCSException ex)
			{
				throw new PCSException(ex.mCode, METHOD_NAME, ex);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message, ex);
			}
		}
		/// <summary>
		/// SwitchFormMode
		/// </summary>
		/// <author>Trada</author>
		/// <date>Friday, July 22 2005</date>
		private void SwitchFormMode()
		{
			const string METHOD_NAME = THIS + ".SwitchFormMode()";
			try
			{
				switch (formMode)
				{
					case EnumAction.Default:
						dtmPostDate.Enabled = false;
						txtComment.Enabled = false;
						txtAdjustQuantity.Enabled = false;
						txtAvailQuantity.Enabled = false;
						txtLot.Enabled = false;
						txtModel.Enabled = false;
						txtPartName.Enabled = false;
						txtPartNumber.Enabled = false;
						txtQAStatus.Enabled = false;
						txtSerial.Enabled = false;
						txtLocation.Enabled = false;
						txtMasLoc.Enabled = false;
						txtBin.Enabled = false;
						btnBin.Enabled = false;
						btnLocation.Enabled = false;
						btnLot.Enabled = false;
						btnMasLoc.Enabled = false;
						btnPartName.Enabled = false;
						btnPartNumber.Enabled = false;
						btnSave.Enabled = false;
						btnAdd.Enabled = true;
						//cboCCN.Enabled = false;
						btnTransNo.Enabled = true;
						chkUsedByCosting.Enabled = false;
						if (txtTransNo.Text.Trim() != string.Empty)
						{
							btnDelete.Enabled = true;
							btnDelTran.Enabled = true;
						}
						else
						{
							btnDelete.Enabled = false;
							btnDelTran.Enabled = false;
						}
                        break;
					case EnumAction.Add:
						dtmPostDate.Enabled = true;
						txtComment.Enabled = true;
						txtAdjustQuantity.Enabled = true;
						txtLot.Enabled = true;
						txtModel.Enabled = false;
						txtPartName.Enabled = true;
						txtPartNumber.Enabled = true;
						txtQAStatus.Enabled = false;
						txtSerial.Enabled = true;
						txtLocation.Enabled = true;
						txtMasLoc.Enabled = true;
						txtBin.Enabled = true;
						btnBin.Enabled = true;
						btnLocation.Enabled = true;
						btnLot.Enabled = true;
						btnMasLoc.Enabled = true;
						btnPartName.Enabled = true;
						btnPartNumber.Enabled = true;
						btnSave.Enabled = true;
						cboCCN.Enabled = true;
						btnAdd.Enabled = false;
						btnTransNo.Enabled = false;
						chkUsedByCosting.Enabled = true;
						chkUsedByCosting.Checked = false;
                        btnDelete.Enabled = false;
                        btnDelTran.Enabled = false;
						break;
					case EnumAction.Edit:
						break;
				}
				
			}
			catch (PCSException ex)
			{
				throw new PCSException(ex.mCode, METHOD_NAME, ex);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message, ex);
			}
		}
		/// <summary>
		/// ClearSomeControls
		/// </summary>
		/// <author>Trada</author>
		/// <date>Friday, July 22 2005</date>
		private void ClearSomeControls()
		{
			const string METHOD_NAME = THIS + ".ClearSomeControls()";
			try
			{
				//txtMasLoc.Text = string.Empty;
				txtLocation.Text = string.Empty;
				txtBin.Text = string.Empty;
				txtAvailQuantity.Value = null;
				txtQAStatus.Text = string.Empty;
				txtLot.Text = string.Empty;
				txtSerial.Text = string.Empty;
			}
			catch (PCSException ex)
			{
				throw new PCSException(ex.mCode, METHOD_NAME, ex);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message, ex);
			}
		}
		/// <summary>
		/// btnAdd_Click
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Friday, July 22 2005</date>
		private void btnAdd_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".IVInventoryAddjustment_Load()";
			try
			{
				//Switch form Mode
				formMode = EnumAction.Add;
				ClearForm();
				SwitchFormMode();
				// Load PostDate
				IV_AdjustmentVO voIV_Adjustment = new IV_AdjustmentVO();
				voIV_Adjustment.PostDate = boUtil.GetDBDate();
				if((DateTime.MinValue < voIV_Adjustment.PostDate) && (voIV_Adjustment.PostDate < DateTime.MaxValue))
					dtmPostDate.Value = voIV_Adjustment.PostDate;
				else
					dtmPostDate.Value = DBNull.Value;
				//Fill Transfer Number
				//txtTransNo.Text = boUtil.GetNoByMask(IV_AdjustmentTable.TABLE_NAME, IV_AdjustmentTable.TRANSNO_FLD, DateTime.Parse(dtmPostDate.Value.ToString()), string.Empty);
				txtTransNo.Text = FormControlComponents.GetNoByMask(this);
				//Fill Default Master Location 
				FormControlComponents.SetDefaultMasterLocation(txtMasLoc);
				voIV_Adjustment.MasterLocationID = SystemProperty.MasterLocationID;
				//Set focus to PostDate
				dtmPostDate.Focus();
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
		/// IsValidateData
		/// </summary>
		/// <returns></returns>
		/// <author>Trada</author>
		/// <date>Tuesday, July 26 2005</date>
		private bool IsValidateData()
		{
			const string METHOD_NAME = THIS + ".IsValidateData()";
			try
			{
				//Check Mandatory
				if (FormControlComponents.CheckMandatory(dtmPostDate))
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID);
					dtmPostDate.Focus();
					dtmPostDate.Select();
					return false;
				}

				if (!FormControlComponents.CheckDateInCurrentPeriod(DateTime.Parse(dtmPostDate.Value.ToString())))
				{
					PCSMessageBox.Show(ErrorCode.MESSAGE_RGV_POSTDATE_PERIOD);
					dtmPostDate.Focus();
					dtmPostDate.Select();
					return false;
				}

				//check the PostDate smaller than the current date
				if ((DateTime)dtmPostDate.Value > new UtilsBO().GetDBDate())
				{
					//MessageBox.Show("The Post Date you input is not in the current period");
					PCSMessageBox.Show(ErrorCode.MESSAGE_INV_TRANSACTION_CANNOT_IN_FUTURE,MessageBoxIcon.Warning);
					dtmPostDate.Focus();
					return false;
				}

				if (FormControlComponents.CheckMandatory(txtTransNo))
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID);
					txtTransNo.Focus();
					txtTransNo.Select();
					return false;
				}
				if (FormControlComponents.CheckMandatory(txtPartNumber))
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID);
					txtPartNumber.Focus();
					txtPartNumber.Select();
					return false;
				}
				if (FormControlComponents.CheckMandatory(txtPartName))
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID);
					txtPartName.Focus();
					txtPartName.Select();
					return false;
				}
				if (FormControlComponents.CheckMandatory(txtMasLoc))
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID);
					txtMasLoc.Focus();
					txtMasLoc.Select();
					return false;
				}
				if (FormControlComponents.CheckMandatory(txtLocation))
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID);
					txtLocation.Focus();
					txtLocation.Select();
					return false;
				}
				if (FormControlComponents.CheckMandatory(txtAdjustQuantity))
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID);
					txtAdjustQuantity.Focus();
					txtAdjustQuantity.Select();
					return false;
				}
				if ((decimal) txtAdjustQuantity.Value == 0)
				{
					PCSMessageBox.Show(ErrorCode.MESSAGE_MATERIALRECEIPT_QUANTITY_HIGHER_0);
					txtAdjustQuantity.Focus();
					return false;
				}
				
				if (blnLotControl) 
				{
					if (txtLot.Text.Trim() == string.Empty)
					{
						PCSMessageBox.Show(ErrorCode.MESSAGE_IV_ADJUSTMENT_PLS_INPUT_LOT);
						txtLot.Focus();
						txtLot.Select();
						return false;
					}
					//Check if over lotsize
					if ((txtAvailQuantity.Value != null) && (txtAvailQuantity.Value != DBNull.Value))
					{
						if ((decimal.Parse(txtAdjustQuantity.Value.ToString()) + decimal.Parse(txtAvailQuantity.Value.ToString())) > intLotSize)
						{
							PCSMessageBox.Show(ErrorCode.MESSAGE_THIS_LOT_IS_OVER_LOTSIZE, MessageBoxIcon.Warning);
							txtAdjustQuantity.Focus();
							txtAdjustQuantity.Select();
							return false;
						}
					}	

				}
				if (blnBinControl)
				{
					if (txtBin.Text.Trim() == string.Empty)
					{
						PCSMessageBox.Show(ErrorCode.MESSAGE_IV_ADJUSTMENT_PLS_INPUT_BIN);
						txtBin.Focus();
						txtBin.Select();
						return false;
					}
				}
				if ((txtAvailQuantity.Value != null) && (txtAvailQuantity.Value != DBNull.Value))
				{
					if ((decimal.Parse(txtAdjustQuantity.Value.ToString()) + decimal.Parse(txtAvailQuantity.Value.ToString())) < 0)
					{
						PCSMessageBox.Show(ErrorCode.MESSAGE_IV_ADJUSTMENT_ADJUSTQTY_MUST_BE_SMALLER_THAN_AVAILABLEQTY, MessageBoxIcon.Warning);
						txtAdjustQuantity.Focus();
						txtAdjustQuantity.Select();
						return false;
					}
				}
				//Check postdate in configuration
				if (!FormControlComponents.CheckPostDateInConfiguration((DateTime)dtmPostDate.Value))
				{
					return false;
				}
				return true;
			}
			catch (PCSException ex)
			{
				throw new PCSException(ex.mCode, METHOD_NAME, ex);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message, ex);
			}
		}
		/// <summary>
		/// btnSave_Click
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Friday, July 22 2005</date>
		private void btnSave_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnSave_Click()";
			try
			{
				if (PCSMessageBox.Show(ErrorCode.MESSAGE_CONFIRM_BEFORE_SAVE_WOCOMPLETION, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2, new string[] {dtmPostDate.Text}) == DialogResult.Yes)
				{
					blnHasError = true;
					if (IsValidateData())
					{
						if(Security.IsDifferencePrefix(this,lblTransNo,txtTransNo))
						{
							return;
						}

						int pintIV_Adjustment;
						voIV_Adjustment.AdjustQuantity = decimal.Parse(txtAdjustQuantity.Value.ToString());
						voIV_Adjustment.AvailableQuantity = Convert.ToDecimal(txtAvailQuantity.Value);
						if (txtBin.Tag != null)
						{
							voIV_Adjustment.BinID = int.Parse(txtBin.Tag.ToString());
						}
						else 
							voIV_Adjustment.BinID = 0;
						voIV_Adjustment.CCNID = int.Parse(cboCCN.SelectedValue.ToString());
						voIV_Adjustment.Comment = txtComment.Text;
						voIV_Adjustment.LocationID = int.Parse(txtLocation.Tag.ToString());
						voIV_Adjustment.Lot = txtLot.Text;
						//voIV_Adjustment.MasterLocationID = voMasLoc.MasterLocationID;
						//Set Date only
						DateTime dtmPostDateOnly = new DateTime(DateTime.Parse(dtmPostDate.Value.ToString()).Year, 
							DateTime.Parse(dtmPostDate.Value.ToString()).Month, DateTime.Parse(dtmPostDate.Value.ToString()).Day,
							DateTime.Parse(dtmPostDate.Value.ToString()).Hour, DateTime.Parse(dtmPostDate.Value.ToString()).Minute, 0);
						//voIV_Adjustment.PostDate = DateTime.Parse(dtmPostDate.Value.ToString());
						voIV_Adjustment.PostDate = dtmPostDateOnly;
						voIV_Adjustment.Serial = txtSerial.Text;
						voIV_Adjustment.TransNo = txtTransNo.Text; 
						// 20-06-2006 dungla add one more field
						voIV_Adjustment.UsedByCosting = chkUsedByCosting.Checked;
						IVInventoryAdjustmentBO boIVInventoryAdjustment = new IVInventoryAdjustmentBO();
						pintIV_Adjustment = boIVInventoryAdjustment.AddAndReturnID(voIV_Adjustment);
						Security.UpdateUserNameModifyTransaction(this, IV_AdjustmentTable.ADJUSTMENTID_FLD, pintIV_Adjustment);
						PCSMessageBox.Show(ErrorCode.MESSAGE_AFTER_SAVE_DATA);
						btnSave.Enabled = false;
						btnAdd.Enabled = true;
						//ClearForm();
						formMode = EnumAction.Default;
						blnHasError = false;
						SwitchFormMode();
						btnAdd.Focus();

						//HACK: added by Tuan TQ- 12 July, 2006- Store TransID for printing
						txtTransNo.Tag =  pintIV_Adjustment;
						//End hack
					}
				}
			}		
			catch (PCSException ex)
			{
				
				// Displays the error message if throwed from PCSException.
				PCSMessageBox.Show(ex.mCode, MessageBoxIcon.Error);
				if (ex.mCode == ErrorCode.DUPLICATE_KEY)
				{
					txtTransNo.Focus();
					txtTransNo.Select();
				}
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

		#region Print Adjustment Slip - Tuan TQ
		/// <summary>
		/// Build and show Inventory Adjustment Slip Report
		/// </summary>
		/// <Author> Tuan TQ, 12 July, 2006</Author>
		private void ShowAdjustmentSlipReport(object sender, System.EventArgs e)
		{	
			const string METHOD_NAME = THIS + ".ShowAdjustmentSlipReport()";
			try
			{				
				const string REPORT_NAME = "InventoryAdjustmentReport";
				const string REPORT_LAYOUT_FILE = "InventoryAdjustmentReport.xml";
				const string REPORTFLD_TITLE = "fldTitle";				
				
				const string APPLICATION_PATH  = @"PCSMain\bin\Debug";
			
				const string REPORTFLD_COMPANY = "fldCompany";
				
				//return if no record was selected
				if(txtTransNo.Tag == null)
				{
					return;
				}

				int intMasterID;
				intMasterID = int.Parse(txtTransNo.Tag.ToString());
				if(intMasterID <= 0)
				{
					return;
				}				
				
				//Change cursor to wait status
				this.Cursor = Cursors.WaitCursor;

				C1PrintPreviewDialog   printPreview = new C1PrintPreviewDialog();
				C1PrintPreviewDialogBO boDataReport = new C1PrintPreviewDialogBO();
			
				DataTable dtbResult;
				dtbResult = boDataReport.GetInventoryAdjustmentData(intMasterID);
				
				ReportBuilder reportBuilder = new ReportBuilder();
			
				//Get actual application path
				string strReportPath = Application.StartupPath;
				int intIndex = strReportPath.IndexOf(APPLICATION_PATH);
				if ( intIndex > -1 ) 
				{
					strReportPath = strReportPath.Substring(0, intIndex);
				}

				if(strReportPath.Substring(strReportPath.Length -1) == @"\")
				{
					strReportPath += Constants.REPORT_DEFINITION_STORE_LOCATION;
				}
				else
				{
					strReportPath += "\\" + Constants.REPORT_DEFINITION_STORE_LOCATION;
				}				
			
				//Set datasource and lay-out path for reports
				reportBuilder.ReportName = REPORT_NAME;
				reportBuilder.SourceDataTable = dtbResult;				
				reportBuilder.ReportDefinitionFolder = strReportPath;
			
				reportBuilder.ReportLayoutFile = REPORT_LAYOUT_FILE;

				//check if layout is valid
				if(reportBuilder.AnalyseLayoutFile())
				{					
					reportBuilder.UseLayoutFile = true;
				}
				else
				{
					//Set cursor to default
					this.Cursor = Cursors.Default;
					PCSMessageBox.Show(ErrorCode.MESSAGE_REPORT_TEMPLATE_FILE_NOT_FOUND, MessageBoxIcon.Error);
					return;
				}

				reportBuilder.MakeDataTableForRender();

				// and show it in preview dialog
				reportBuilder.ReportViewer = printPreview.ReportViewer;
				reportBuilder.RenderReport();
		
				//Header information get from system params				
				reportBuilder.DrawPredefinedField(REPORTFLD_COMPANY, SystemProperty.SytemParams.Get(SystemParam.COMPANY_FULL_NAME));				
			
				reportBuilder.RefreshReport();
			
				//Print report
				try
				{
					printPreview.FormTitle = reportBuilder.GetFieldByName(REPORTFLD_TITLE).Text;
				}
				catch
				{}

				printPreview.Show();	
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
			catch(Exception ex)
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
			finally
			{
				//Set cursor to default status
				this.Cursor = Cursors.Default;
			}
		}


		/// <summary>
		/// btnPrint_Click
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Tuan TQ</author>
		/// <date>Wednesday, July 12 2006</date>
		private void btnPrint_Click(object sender, System.EventArgs e)
		{
			ShowAdjustmentSlipReport(sender, e);
		}
		#endregion

		/// <summary>
		/// btnHelp_Click
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <Date>Friday, July 22 2005</Date>
		private void btnHelp_Click(object sender, System.EventArgs e)
		{
			
		}
		/// <summary>
		/// btnClose_Click
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Friday, July 22 2005</date>
		private void btnClose_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".IVInventoryAddjustment_Load()";
			try
			{
				this.Close();
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
		/// btnTransNo_Click
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Friday, July 22 2005</date>
		private void btnTransNo_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnTransNo_Click()";
			try
			{
				Hashtable htbCriteria = new Hashtable();
				DataRowView drwResult = null;
				if (cboCCN.SelectedValue != null)
				{
					htbCriteria.Add(IV_AdjustmentTable.CCNID_FLD, cboCCN.SelectedValue);
				}
				else
				{
					PCSMessageBox.Show(ErrorCode.MESSAGE_RGA_CCN, MessageBoxIcon.Warning);
					cboCCN.Focus();
				}	
				drwResult = FormControlComponents.OpenSearchForm(V_IVADJUSTMENTANDPRODUCT, IV_AdjustmentTable.TRANSNO_FLD, txtTransNo.Text.Trim(), htbCriteria, true);
				if (drwResult != null)
				{
					//HACK: added by Tuan TQ- 12 July, 2006- Store TransID for printing
					txtTransNo.Tag =  drwResult[IV_AdjustmentTable.ADJUSTMENTID_FLD];
					//End hack

					txtTransNo.Text = drwResult[IV_AdjustmentTable.TRANSNO_FLD].ToString();
					txtComment.Text = drwResult[IV_AdjustmentTable.COMMENT_FLD].ToString();
					dtmPostDate.Value = drwResult[IV_AdjustmentTable.POSTDATE_FLD];
					txtPartName.Text = drwResult[PARTNAME].ToString();
					txtPartNumber.Text = drwResult[PARTNUMBER].ToString();
					txtModel.Text = drwResult[MODEL].ToString();
					txtMasLoc.Text = drwResult[MASLOCNAME].ToString();
					txtLocation.Text = drwResult[LOCNAME].ToString();
					txtUM.Text = drwResult[UM].ToString();
					if (drwResult[BINNAME] != DBNull.Value)
					{
						txtBin.Text = drwResult[BINNAME].ToString();
					}
					if (drwResult[IV_AdjustmentTable.LOT_FLD] != DBNull.Value)
					{
						txtLot.Text = drwResult[IV_AdjustmentTable.LOT_FLD].ToString();
					}
					if (drwResult[IV_AdjustmentTable.SERIAL_FLD] != DBNull.Value)
					{
						txtSerial.Text = drwResult[IV_AdjustmentTable.SERIAL_FLD].ToString();
					}
					txtAdjustQuantity.Value = drwResult[IV_AdjustmentTable.ADJUSTQUANTITY_FLD];
					txtAvailQuantity.Value = drwResult[IV_AdjustmentTable.AVAILABLEQTY_FLD];
					try
					{
						chkUsedByCosting.Checked = Convert.ToBoolean(drwResult["UsedByCosting"]);
					}
					catch
					{
						chkUsedByCosting.Checked = false;
					}
                    SwitchFormMode();
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
		/// <summary>
		/// Fill available Quantity if we have Bin and Location
		/// </summary>
		/// <param name="pintProductID"></param>
		/// <author>Trada</author>
		/// <date>Friday, September 8 2006</date>
		private void FillAvailableQuantity(int pintProductID)
		{
			if (txtBin.Text != string.Empty && txtLocation.Text != string.Empty)
			{
				decimal decAvailableQuantity = new InventoryUtilsBO().GetAvailableQtyByPostDate(dtmCurrentDate, int.Parse(cboCCN.SelectedValue.ToString()),
					voIV_Adjustment.MasterLocationID, int.Parse(txtLocation.Tag.ToString()), (int) txtBin.Tag, pintProductID);
				txtAvailQuantity.Value = decAvailableQuantity;
			}
		}
		/// <summary>
		/// btnPartNumber_Click
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Friday, July 22 2005</date>
		private void btnPartNumber_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnPartNumber_Click()";
			try
			{
				if (FormControlComponents.CheckMandatory(dtmPostDate))
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID);
					dtmPostDate.Focus();
					dtmPostDate.Select();
					return;
				}
				//Search Part Number
				Hashtable htbCriteria = new Hashtable();
				DataRowView drwResult = null;
				drwResult = FormControlComponents.OpenSearchForm(V_PRODUCTINVENTORYADJUSTMENT, PARTNUMBER, txtPartNumber.Text.Trim(), null, true);
				if (drwResult != null)
				{
					voIV_Adjustment.ProductID = int.Parse(drwResult[ITM_ProductTable.PRODUCTID_FLD].ToString());
					voIV_Adjustment.StockUMID = int.Parse(drwResult[ITM_ProductTable.STOCKUMID_FLD].ToString());
					txtPartNumber.Text = drwResult[PARTNUMBER].ToString();
					txtPartName.Text = drwResult[PARTNAME].ToString();
					txtModel.Text = drwResult[MODEL].ToString();
					if (drwResult[MASLOCNAME] != DBNull.Value)
					{
						txtMasLoc.Text = drwResult[MASLOCNAME].ToString();
						voIV_Adjustment.MasterLocationID = int.Parse(drwResult[MST_MasterLocationTable.MASTERLOCATIONID_FLD].ToString());
					}
					else 
						voIV_Adjustment.MasterLocationID = 0;
//					if (drwResult[LOCNAME] != DBNull.Value)
//					{
//						txtLocation.Text = drwResult[LOCNAME].ToString();
//						txtLocation.Tag = drwResult[MST_LocationTable.LOCATIONID_FLD];
//					}
					if (drwResult[UM] != DBNull.Value)
					{
						txtUM.Text = drwResult[UM].ToString();
						voIV_Adjustment.StockUMID = int.Parse(drwResult[ITM_ProductTable.STOCKUMID_FLD].ToString());
					}
					//Check Available if have Bin and Location
					FillAvailableQuantity(voIV_Adjustment.ProductID);
					//Have Lot
//					if (drwResult[ITM_ProductTable.LOTCONTROL_FLD] != DBNull.Value)
//					{
//						if (drwResult[ITM_ProductTable.LOTCONTROL_FLD].ToString() == TRUE)
//						{
//							blnLotControl = true;
//							txtLot.Enabled = true;
//							btnLot.Enabled = true;
//							txtSerial.Enabled = true;
//							intLotSize = int.Parse(drwResult[ITM_ProductTable.LOTSIZE_FLD].ToString());
//						}
//						else
//						{
//							blnLotControl = false;
//							txtLot.Enabled = false;
//							btnLot.Enabled = false;
//							txtSerial.Enabled = false;
//						}
//					}
//					if(drwResult[BINCONTROL] != DBNull.Value)
//					{
//						if (drwResult[BINCONTROL].ToString() == TRUE)
//						{
//							blnBinControl = true;
//							txtBin.Enabled = true;
//							btnBin.Enabled = true;
//							txtQAStatus.Text = drwResult[BINQASTATUS].ToString();
//							txtAvailQuantity.Value = drwResult[BIN_AVAILABLE_QUANTITY];	
//							
//							if (drwResult[BINNAME] != DBNull.Value)
//							{
//								txtBin.Text = drwResult[BINNAME].ToString();
//							}
//							if (drwResult[MST_BINTable.BINID_FLD] != DBNull.Value)
//							{
//								txtBin.Tag = drwResult[MST_BINTable.BINID_FLD];	
//								decimal decAvailableQuantity = new InventoryUtilsBO().GetAvailableQtyByPostDate((DateTime)dtmPostDate.Value, int.Parse(cboCCN.SelectedValue.ToString()),
//									voIV_Adjustment.MasterLocationID, int.Parse(txtLocation.Tag.ToString()), (int) txtBin.Tag, voIV_Adjustment.ProductID);
//								txtAvailQuantity.Value = decAvailableQuantity;
//							}
//							else
//								txtAvailQuantity.Value = DBNull.Value;
//						}
//						else
//						{
//							blnBinControl = false;
//							txtBin.Enabled = false;
//							btnBin.Enabled = false;
//							txtQAStatus.Text = drwResult[LOCQASTATUS].ToString();
//							txtAvailQuantity.Value = drwResult[LOCAVAILABLE_QUANTITY];
//							txtBin.Tag = null;
//							txtBin.Text = string.Empty;
//
//							decimal decAvailableQuantity = new InventoryUtilsBO().GetAvailableQtyByPostDate((DateTime)dtmPostDate.Value, int.Parse(cboCCN.SelectedValue.ToString()),
//								voIV_Adjustment.MasterLocationID, int.Parse(txtLocation.Tag.ToString()), 0, voIV_Adjustment.ProductID);
//							txtAvailQuantity.Value = decAvailableQuantity;
//						}
//					}
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
		/// <summary>
		/// btnPartName_Click
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Friday, July 22 2005</date>
		private void btnPartName_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnPartName_Click()";
			try
			{
				if (FormControlComponents.CheckMandatory(dtmPostDate))
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID);
					dtmPostDate.Focus();
					dtmPostDate.Select();
					return;
				}
				//Search Part Name
				Hashtable htbCriteria = new Hashtable();
				DataRowView drwResult = null;
				drwResult = FormControlComponents.OpenSearchForm(V_PRODUCTINVENTORYADJUSTMENT, PARTNAME, txtPartName.Text.Trim(), null, true);
				if (drwResult != null)
				{
					voIV_Adjustment.ProductID = int.Parse(drwResult[ITM_ProductTable.PRODUCTID_FLD].ToString());
					txtPartNumber.Text = drwResult[PARTNUMBER].ToString();
					txtPartName.Text = drwResult[PARTNAME].ToString();
					txtModel.Text = drwResult[MODEL].ToString();
					if (drwResult[MASLOCNAME] != DBNull.Value)
					{
						txtMasLoc.Text = drwResult[MASLOCNAME].ToString();
						voIV_Adjustment.MasterLocationID = int.Parse(drwResult[MST_MasterLocationTable.MASTERLOCATIONID_FLD].ToString());
					}
					else
						voIV_Adjustment.MasterLocationID = 0;
//					if (drwResult[LOCNAME] != DBNull.Value)
//					{
//						txtLocation.Text = drwResult[LOCNAME].ToString();
//						txtLocation.Tag = drwResult[MST_LocationTable.LOCATIONID_FLD];
//					}
					if (drwResult[UM] != DBNull.Value)
					{
						txtUM.Text = drwResult[UM].ToString();
						voIV_Adjustment.StockUMID = int.Parse(drwResult[ITM_ProductTable.STOCKUMID_FLD].ToString());
					}
					//Check Available if have Bin and Location
					FillAvailableQuantity(voIV_Adjustment.ProductID);
//					if (drwResult[ITM_ProductTable.LOTCONTROL_FLD] != DBNull.Value)
//					{
//						if (drwResult[ITM_ProductTable.LOTCONTROL_FLD].ToString() == TRUE)
//						{
//							blnLotControl = true;
//							txtLot.Enabled = true;
//							btnLot.Enabled = true;
//							txtSerial.Enabled = true;
//							intLotSize = int.Parse(drwResult[ITM_ProductTable.LOTSIZE_FLD].ToString());
//						}
//						else
//						{
//							blnLotControl = false;
//							txtLot.Enabled = false;
//							btnLot.Enabled = false;
//							txtSerial.Enabled = false;
//						}
//					}
//					if (drwResult[BINCONTROL] != DBNull.Value)
//					{
//						//Check if BinControl == true
//						if (drwResult[BINCONTROL].ToString() == TRUE)
//						{
//							blnBinControl = true;
//							txtBin.Enabled = true;
//							btnBin.Enabled = true;
//							txtQAStatus.Text = drwResult[BINQASTATUS].ToString();
//							txtAvailQuantity.Value = drwResult[BIN_AVAILABLE_QUANTITY];	
//							if (drwResult[BINNAME] != DBNull.Value)
//							{
//								txtBin.Text = drwResult[BINNAME].ToString();
//							}
//							if (drwResult[MST_BINTable.BINID_FLD] != DBNull.Value)
//							{
//								txtBin.Tag = drwResult[MST_BINTable.BINID_FLD];	
//								decimal decAvailableQuantity = new InventoryUtilsBO().GetAvailableQtyByPostDate((DateTime)dtmPostDate.Value, int.Parse(cboCCN.SelectedValue.ToString()),
//									voIV_Adjustment.MasterLocationID, int.Parse(txtLocation.Tag.ToString()), (int) txtBin.Tag, voIV_Adjustment.ProductID);
//								txtAvailQuantity.Value = decAvailableQuantity;
//							}
//							else
//							{
//								txtAvailQuantity.Value = DBNull.Value;
//							}
//						}
//						else
//						{
//							blnBinControl = false;
//							txtBin.Enabled = false;
//							btnBin.Enabled = false;
//							txtQAStatus.Text = drwResult[LOCQASTATUS].ToString();
//							txtAvailQuantity.Value = drwResult[LOCAVAILABLE_QUANTITY];
//							txtBin.Tag = null;
//							txtBin.Text = string.Empty;
//							decimal decAvailableQuantity = new InventoryUtilsBO().GetAvailableQtyByPostDate((DateTime)dtmPostDate.Value, int.Parse(cboCCN.SelectedValue.ToString()),
//								voIV_Adjustment.MasterLocationID, int.Parse(txtLocation.Tag.ToString()), 0, voIV_Adjustment.ProductID);
//							txtAvailQuantity.Value = decAvailableQuantity;
//						}
//					}
//					
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

		/// <summary>
		/// btnBin_Click
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Friday, July 22 2005</date>
		private void btnBin_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnBin_Click()";
			try
			{
				Hashtable htbCriteria = new Hashtable();
				if (txtLocation.Text.Trim() == string.Empty)
				{
					PCSMessageBox.Show(ErrorCode.MESSAGE_RGA_SELECTLOC, MessageBoxIcon.Error);
					txtLocation.Focus();
					txtLocation.Select();
					return;
				}
				else
					htbCriteria.Add(MST_BINTable.LOCATIONID_FLD, int.Parse(txtLocation.Tag.ToString()));

				DataRowView drwResult = null;
				drwResult = FormControlComponents.OpenSearchForm(MST_BINTable.TABLE_NAME, MST_BINTable.CODE_FLD, txtBin.Text.Trim(), htbCriteria, true);
				if (drwResult != null)
				{
					txtBin.Text = drwResult[MST_BINTable.CODE_FLD].ToString();
					txtBin.Tag = drwResult[MST_BINTable.BINID_FLD];

					//Get available quantity where product
//					IVInventoryAdjustmentBO boIVInventoryAdjustment = new IVInventoryAdjustmentBO();
//					dstAvailableQtyAndInsStatus = boIVInventoryAdjustment.GetAvalableQuantity(int.Parse(cboCCN.SelectedValue.ToString()),
//						voIV_Adjustment.MasterLocationID, int.Parse(txtLocation.Tag.ToString()), int.Parse(txtBin.Tag.ToString()), voIV_Adjustment.ProductID);
					if (txtPartNumber.Text != string.Empty)
					{
						decimal decAvailableQuantity = new InventoryUtilsBO().GetAvailableQtyByPostDate(dtmCurrentDate, int.Parse(cboCCN.SelectedValue.ToString()),
							voIV_Adjustment.MasterLocationID, int.Parse(txtLocation.Tag.ToString()), (int) txtBin.Tag, voIV_Adjustment.ProductID);
						txtAvailQuantity.Value = decAvailableQuantity;
					}
//					if (dstAvailableQtyAndInsStatus.Tables[0].Rows.Count > 0)
//					{
//						txtAvailQuantity.Value = decAvailableQuantity;
//						txtQAStatus.Text = dstAvailableQtyAndInsStatus.Tables[0].Rows[0][IV_LocationCacheTable.INSPSTATUS_FLD].ToString();
//					}
//					else
//					{
//						txtAvailQuantity.Value = 0;
//						txtQAStatus.Text = string.Empty;
//					}
					
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

		/// <summary>
		/// btnLocation_Click
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Friday, July 22 2005</date>
		private void btnLocation_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnLocation_Click()";
			try
			{
				Hashtable htbCriteria = new Hashtable();
				if (txtMasLoc.Text.Trim() == string.Empty)
				{
					PCSMessageBox.Show(ErrorCode.MSG_LABORTIME_SELECTMASLOC, MessageBoxIcon.Error);
					txtMasLoc.Focus();
					txtMasLoc.Select();
					return;
				}
				else
					htbCriteria.Add(MST_LocationTable.MASTERLOCATIONID_FLD, voIV_Adjustment.MasterLocationID);
				#region HACK: DEL Trada 10-14-2005

//				if (txtPartNumber.Text.Trim() == string.Empty)
//				{
//					PCSMessageBox.Show(ErrorCode.MESSAGE_IV_ADJUSTMENT_PLS_INPUT_PATHNUMBER, MessageBoxIcon.Warning);
//					txtPartNumber.Focus();
//					return;
//				}
//				else 
//					htbCriteria.Add(ITM_ProductTable.PRODUCTID_FLD, voIV_Adjustment.ProductID);

				#endregion END: DEL Trada 10-14-2005
				DataRowView drwResult = null;
				drwResult = FormControlComponents.OpenSearchForm(MST_LocationTable.TABLE_NAME, MST_LocationTable.CODE_FLD, txtLocation.Text.Trim(), htbCriteria, true);
				if (drwResult != null)
				{
					if (txtLocation.Tag != null)
					{
						if (txtLocation.Tag.ToString() != drwResult[MST_LocationTable.LOCATIONID_FLD].ToString())
						{
							#region HACK: DEL Trada 10-17-2005

//							lblLocation.Tag = drwResult[PRODUCT_CODE];

							#endregion END: DEL Trada 10-17-2005

							//ClearForm();
							ClearSomeControls();
						}
					}
					#region HACK: DEL Trada 10-17-2005

//					lblLocation.Tag = drwResult[PRODUCT_CODE];

					#endregion END: DEL Trada 10-17-2005
					txtLocation.Tag = drwResult[MST_LocationTable.LOCATIONID_FLD];
					txtLocation.Text = drwResult[MST_LocationTable.CODE_FLD].ToString();
					
					//Check if bin control is true
					//if (drwResult[MST_LocationTable.BIN_FLD] != DBNull.Value)
//					if (drwResult[MST_LocationTable.BIN_FLD].ToString() == TRUE)
//					{
//						//Clear Bin, QA Status, Available Quantity
//						//txtBin.Text = string.Empty;
//						txtBin.Enabled = true;
//						btnBin.Enabled = true;
//						txtQAStatus.Text = string.Empty;
//						txtAvailQuantity.Value = null;
//						blnBinControl = true;
//
//					}
//					else 
//					{
//						blnBinControl = false;
//						txtBin.Text = string.Empty;
//						txtBin.Tag = null;
//						txtBin.Enabled = false;
//						btnBin.Enabled = false;
//						#region HACK: DEL Trada 10-17-2005
//
////						if (drwResult[AVAILABLE_QUANTITY] != DBNull.Value)
////						{
////							txtAvailQuantity.Value = drwResult[AVAILABLE_QUANTITY];
////						}
//
//						
////						if (drwResult[IV_LocationCacheTable.INSPSTATUS_FLD] != DBNull.Value)
////						{
////							txtQAStatus.Text = drwResult[IV_LocationCacheTable.INSPSTATUS_FLD].ToString();
////						}
//						#endregion END: DEL Trada 10-17-2005
//						//Get available quantity where product
//						IVInventoryAdjustmentBO boIVInventoryAdjustment = new IVInventoryAdjustmentBO();
//						dstAvailableQtyAndInsStatus = boIVInventoryAdjustment.GetAvalableQuantity(int.Parse(cboCCN.SelectedValue.ToString()),
//							voIV_Adjustment.MasterLocationID, int.Parse(txtLocation.Tag.ToString()), 0, voIV_Adjustment.ProductID);
//						decimal decAvailableQuantity = new InventoryUtilsBO().GetAvailableQtyByPostDate((DateTime)dtmPostDate.Value, int.Parse(cboCCN.SelectedValue.ToString()),
//							voIV_Adjustment.MasterLocationID, int.Parse(txtLocation.Tag.ToString()), 0, voIV_Adjustment.ProductID);
//						if (dstAvailableQtyAndInsStatus.Tables[0].Rows.Count > 0)
//						{
//							txtAvailQuantity.Value = decAvailableQuantity;
//							txtQAStatus.Text = dstAvailableQtyAndInsStatus.Tables[0].Rows[0][IV_LocationCacheTable.INSPSTATUS_FLD].ToString();
//						}
//						else
//						{
//							txtAvailQuantity.Value = 0;
//							txtQAStatus.Text = string.Empty;
//						}
//					}
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

		/// <summary>
		/// btnMasLoc_Click
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Friday, July 22 2005</date>
		private void btnMasLoc_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnMasLoc_Click()";
			try
			{
				DataRowView drwResult = null;
				Hashtable htbCriteria = new Hashtable();
				#region // HACK: DEL Trada 10-14-2005

//				if (txtPartNumber.Text.Trim() == string.Empty)
//				{
//					PCSMessageBox.Show(ErrorCode.MESSAGE_IV_ADJUSTMENT_PLS_INPUT_PATHNUMBER, MessageBoxIcon.Warning);
//					txtPartNumber.Focus();
//					return;
//				}
//				else 
//					htbCriteria.Add(ITM_ProductTable.PRODUCTID_FLD, voIV_Adjustment.ProductID);

				#endregion // END: DEL Trada 10-14-2005

				drwResult = FormControlComponents.OpenSearchForm(MST_MasterLocationTable.TABLE_NAME, MST_MasterLocationTable.CODE_FLD, txtMasLoc.Text.Trim(), htbCriteria, true);
                if (drwResult != null)
				{
					if (voIV_Adjustment.MasterLocationID != 0)
					{
						if (voIV_Adjustment.MasterLocationID != int.Parse(drwResult[MST_MasterLocationTable.MASTERLOCATIONID_FLD].ToString()))
						{
							//lblMasLoc.Tag = drwResult[MST_MasterLocationTable.MASTERLOCATIONID_FLD];
							//ClearForm();
							ClearSomeControls();
						}
						
					}
					#region HACK: DEL Trada 10-17-2005

//					if (txtMasLoc.Tag != null)
//					{
//						if (txtMasLoc.Tag.ToString() != drwResult[PRODUCT_CODE].ToString())
//						{
//
//							txtMasLoc.Tag = drwResult[PRODUCT_CODE];
//							//ClearForm();
//							ClearSomeControls();
//						}
//					}

					#endregion END: DEL Trada 10-17-2005
					//lblMasLoc.Tag = drwResult[MST_MasterLocationTable.MASTERLOCATIONID_FLD];
					voIV_Adjustment.MasterLocationID = int.Parse(drwResult[MST_MasterLocationTable.MASTERLOCATIONID_FLD].ToString());
					#region HACK: DEL Trada 10-17-2005

//					txtMasLoc.Tag = drwResult[PRODUCT_CODE];

					#endregion END: DEL Trada 10-17-2005
					txtMasLoc.Text = drwResult[MST_MasterLocationTable.CODE_FLD].ToString();
					#region HACK: DEL Trada 10-17-2005

//					txtAvailQuantity.Value = drwResult[AVAILABLE_QUANTITY];

					#endregion END: DEL Trada 10-17-2005
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
		/// <summary>
		/// txtPartName_KeyDown
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Friday, July 22 2005</date>
		private void txtPartName_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtPartName_KeyDown()";
			try
			{
				if (e.KeyCode == Keys.F4)
				{
					btnPartName_Click(sender, e);
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
				// displays the error message.
				PCSMessageBox.Show(ErrorCode.OTHER_ERROR,MessageBoxIcon.Error);
				// log message.
				try
				{
					Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
				}
				catch
				{
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION,MessageBoxIcon.Error);
				}
			}
		}
		/// <summary>
		/// txtPartNumber_KeyDown
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Friday, July 22 2005</date>
		private void txtPartNumber_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtPartNumber_KeyDown()";
			try
			{
				if (e.KeyCode == Keys.F4)
				{
					btnPartNumber_Click(sender, e);
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
				// displays the error message.
				PCSMessageBox.Show(ErrorCode.OTHER_ERROR,MessageBoxIcon.Error);
				// log message.
				try
				{
					Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
				}
				catch
				{
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION,MessageBoxIcon.Error);
				}
			}

		}

		/// <summary>
		/// txtMasLoc_KeyDown
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Friday, July 22 2005</date>
		private void txtMasLoc_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtMasLoc_KeyDown()";
			try
			{
				if (e.KeyCode == Keys.F4)
				{
					btnMasLoc_Click(sender, e);
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
				// displays the error message.
				PCSMessageBox.Show(ErrorCode.OTHER_ERROR,MessageBoxIcon.Error);
				// log message.
				try
				{
					Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
				}
				catch
				{
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION,MessageBoxIcon.Error);
				}
			}
		}
		/// <summary>
		/// btnLot_Click
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Wednesday, July 27 2005</date>
		private void btnLot_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnLot_Click()";
			try
			{
				Hashtable htbCriteria = new Hashtable();
				DataRowView drwResult = null;
				if (blnBinControl)
				{	
					if (txtBin.Text == string.Empty)
					{
						PCSMessageBox.Show(ErrorCode.MESSAGE_MUST_SELECT_BIN_FOR_LOCATION, MessageBoxIcon.Warning);
						txtBin.Focus();
						return;
					}
					else
						htbCriteria.Add(MST_BINTable.BINID_FLD, int.Parse(txtBin.Tag.ToString()));
					htbCriteria.Add(ITM_ProductTable.PRODUCTID_FLD, voIV_Adjustment.ProductID);
					drwResult = FormControlComponents.OpenSearchForm(V_LOTBYBIN, IV_BinCacheTable.LOT_FLD, txtLot.Text.Trim(), htbCriteria, true);
					if (drwResult != null)
					{
						txtLot.Text = drwResult[IV_BinCacheTable.LOT_FLD].ToString();
						txtSerial.Text= drwResult[IV_ItemSerialTable.SERIAL_FLD].ToString();
						txtAvailQuantity.Value = drwResult[ISSUE_QUANTITY];
					}
				}
				else
				{
					if (txtLocation.Text == string.Empty)
					{
						PCSMessageBox.Show(ErrorCode.MESSAGE_MATERIALRECEIPT_SELECT_LOCATION, MessageBoxIcon.Warning);
						txtLocation.Focus();
						return;
					}
					else
						htbCriteria.Add(MST_LocationTable.LOCATIONID_FLD, int.Parse(txtLocation.Tag.ToString()));
					htbCriteria.Add(ITM_ProductTable.PRODUCTID_FLD, voIV_Adjustment.ProductID);
					drwResult = FormControlComponents.OpenSearchForm(V_LOTBYLOC, IV_LocationCacheTable.LOT_FLD, txtBin.Text.Trim(), htbCriteria, true);
					if (drwResult != null)
					{
						txtLot.Text = drwResult[IV_LocationCacheTable.LOT_FLD].ToString();
						txtSerial.Text = drwResult[IV_ItemSerialTable.SERIAL_FLD].ToString();
						txtAvailQuantity.Value = drwResult[AVAILABLE_QUANTITY];
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
				// displays the error message.
				PCSMessageBox.Show(ErrorCode.OTHER_ERROR,MessageBoxIcon.Error);
				// log message.
				try
				{
					Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
				}
				catch
				{
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION,MessageBoxIcon.Error);
				}
			}
		}
		/// <summary>
		/// txtLocation_KeyDown
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Thursday, July 28 2005</date>
		private void txtLocation_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{	
			const string METHOD_NAME = THIS + ".txtLocation_KeyDown()";
			try
			{
				if (e.KeyCode == Keys.F4)
				{
					btnLocation_Click(sender, e);
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
				// displays the error message.
				PCSMessageBox.Show(ErrorCode.OTHER_ERROR,MessageBoxIcon.Error);
				// log message.
				try
				{
					Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
				}
				catch
				{
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION,MessageBoxIcon.Error);
				}
			}
		}
		/// <summary>
		/// txtBin_KeyDown
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Thursday, July 28 2005</date>
		private void txtBin_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtBin_KeyDown()";
			try
			{
				if (e.KeyCode == Keys.F4)
				{
					btnBin_Click(sender, e);
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
				// displays the error message.
				PCSMessageBox.Show(ErrorCode.OTHER_ERROR,MessageBoxIcon.Error);
				// log message.
				try
				{
					Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
				}
				catch
				{
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION,MessageBoxIcon.Error);
				}
			}
		}
		/// <summary>
		/// txtLot_Leave
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Thursday, July 28 2005</date>
		private void txtLot_Leave(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtLot_Leave()";
			try
			{
				if (!txtLot.Modified) return;
				if (txtLot.Text.Trim() == string.Empty)
				{
					txtSerial.Text = string.Empty;
					return;
				}
				Hashtable htbCriteria = new Hashtable();
				DataRowView drwResult = null;
				if (blnBinControl)
				{
					if (txtBin.Text == string.Empty)
					{
						PCSMessageBox.Show(ErrorCode.MESSAGE_MUST_SELECT_BIN_FOR_LOCATION, MessageBoxIcon.Warning);
						txtLot.Text = string.Empty;
						txtBin.Focus();
						return;
					}
					else
						htbCriteria.Add(MST_BINTable.BINID_FLD, int.Parse(txtBin.Tag.ToString()));
					htbCriteria.Add(ITM_ProductTable.PRODUCTID_FLD, voIV_Adjustment.ProductID);
					drwResult = FormControlComponents.OpenSearchForm(V_LOTBYBIN, IV_BinCacheTable.LOT_FLD, txtLot.Text.Trim(), null, false);
					if (drwResult != null)
					{
						txtLot.Text = drwResult[IV_BinCacheTable.LOT_FLD].ToString();
						txtSerial.Text= drwResult[IV_ItemSerialTable.SERIAL_FLD].ToString();
						txtAvailQuantity.Value = drwResult[ISSUE_QUANTITY];
					}
					else 
						txtLot.Focus();
				}
				else
				{
					if (txtLocation.Text == string.Empty)
					{
						PCSMessageBox.Show(ErrorCode.MESSAGE_MATERIALRECEIPT_SELECT_LOCATION, MessageBoxIcon.Warning);
						txtLocation.Focus();
						return;
					}
					else
						htbCriteria.Add(MST_LocationTable.LOCATIONID_FLD, int.Parse(txtLocation.Tag.ToString()));
					htbCriteria.Add(ITM_ProductTable.PRODUCTID_FLD, voIV_Adjustment.ProductID);
					drwResult = FormControlComponents.OpenSearchForm(V_LOTBYLOC, IV_LocationCacheTable.LOT_FLD, txtLot.Text.Trim(), htbCriteria, false);
					if (drwResult != null)
					{
						txtLot.Text = drwResult[IV_LocationCacheTable.LOT_FLD].ToString();
						txtSerial.Text = drwResult[IV_ItemSerialTable.SERIAL_FLD].ToString();
						txtAvailQuantity.Value = drwResult[AVAILABLE_QUANTITY];
					}
					else
						txtLot.Focus();
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
				// displays the error message.
				PCSMessageBox.Show(ErrorCode.OTHER_ERROR,MessageBoxIcon.Error);
				// log message.
				try
				{
					Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
				}
				catch
				{
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION,MessageBoxIcon.Error);
				}
			}
		}
		/// <summary>
		/// txtLot_KeyDown
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Friday, July 29 2005</date>
		private void txtLot_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtLot_KeyDown()";
			try
			{
				if (e.KeyCode == Keys.F4)
				{
					btnLot_Click(sender, e);
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
				// displays the error message.
				PCSMessageBox.Show(ErrorCode.OTHER_ERROR,MessageBoxIcon.Error);
				// log message.
				try
				{
					Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
				}
				catch
				{
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION,MessageBoxIcon.Error);
				}
			}
		}

		/// <summary>
		/// txtTransNo_Leave
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Thursday, July 28 2005</date>
		private void txtTransNo_Leave(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnTransNo_Click()";
			try
			{
				if (!txtTransNo.Modified) return;
				if (formMode == EnumAction.Add)
				{
					return;
				}
				if (txtTransNo.Text.Trim() == string.Empty)
				{
					ClearForm();
					dtmPostDate.Value = DBNull.Value;
					return;
				}
				Hashtable htbCriteria = new Hashtable();
				DataRowView drwResult = null;
				if (cboCCN.SelectedValue != null)
				{
					htbCriteria.Add(IV_AdjustmentTable.CCNID_FLD, cboCCN.SelectedValue);
				}
				else
				{
					PCSMessageBox.Show(ErrorCode.MESSAGE_RGA_CCN, MessageBoxIcon.Warning);
					cboCCN.Focus();
				}	
				drwResult = FormControlComponents.OpenSearchForm(V_IVADJUSTMENTANDPRODUCT, IV_AdjustmentTable.TRANSNO_FLD, txtTransNo.Text.Trim(), htbCriteria, false);
				if (drwResult != null)
				{
					//HACK: added by Tuan TQ- 12 July, 2006- Store TransID for printing
					txtTransNo.Tag =  drwResult[IV_AdjustmentTable.ADJUSTMENTID_FLD];
					//End hack

					txtTransNo.Text = drwResult[IV_AdjustmentTable.TRANSNO_FLD].ToString();
					txtComment.Text = drwResult[IV_AdjustmentTable.COMMENT_FLD].ToString();
					dtmPostDate.Value = drwResult[IV_AdjustmentTable.POSTDATE_FLD];
					txtPartName.Text = drwResult[PARTNAME].ToString();
					txtPartNumber.Text = drwResult[PARTNUMBER].ToString();
					txtModel.Text = drwResult[MODEL].ToString();
					txtMasLoc.Text = drwResult[MASLOCNAME].ToString();
					txtLocation.Text = drwResult[LOCNAME].ToString();
					txtUM.Text = drwResult[UM].ToString();
					if (drwResult[BINNAME] != DBNull.Value)
					{
						txtBin.Text = drwResult[BINNAME].ToString();
					}
					if (drwResult[IV_AdjustmentTable.LOT_FLD] != DBNull.Value)
					{
						txtLot.Text = drwResult[IV_AdjustmentTable.LOT_FLD].ToString();
					}
					if (drwResult[IV_AdjustmentTable.SERIAL_FLD] != DBNull.Value)
					{
						txtSerial.Text = drwResult[IV_AdjustmentTable.SERIAL_FLD].ToString();
					}
					txtAdjustQuantity.Value = drwResult[IV_AdjustmentTable.ADJUSTQUANTITY_FLD];
					txtAvailQuantity.Value = drwResult[IV_AdjustmentTable.AVAILABLEQTY_FLD];
				}
				else 
					txtTransNo.Focus();
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
		/// txtTransNo_KeyDown
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Thursday, July 28 2005</date>
		private void txtTransNo_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtTransNo_KeyDown()";
			try
			{
				if (e.KeyCode == Keys.F4)
				{
					btnTransNo_Click(sender, e);
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
				// displays the error message.
				PCSMessageBox.Show(ErrorCode.OTHER_ERROR,MessageBoxIcon.Error);
				// log message.
				try
				{
					Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
				}
				catch
				{
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION,MessageBoxIcon.Error);
				}
			}

		}
		/// <summary>
		/// IVInventoryAdjustment_Closing
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Tuesday, October 18 2005</date>
		private void IVInventoryAdjustment_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".IVInventoryAdjustment_Closing()";
			try
			{
				if (formMode != EnumAction.Default)
				{
					DialogResult confirmDialog = PCSMessageBox.Show(ErrorCode.MESSAGE_QUESTION_STORE_INTO_DATABASE, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
					switch (confirmDialog)
					{
						case DialogResult.Yes:
							//Save before exit
							btnSave_Click(btnSave, new EventArgs());
							if (blnHasError)
							{
								e.Cancel = true;
							}
							break;
						case DialogResult.No:
							break;
						case DialogResult.Cancel:
							e.Cancel = true;
							break;
					}
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

		private void dtmPostDate_Leave(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".dtmPostDate_Leave()";
			try
			{
				if (txtPartName.Text.Trim() != string.Empty
					&& cboCCN.SelectedValue != DBNull.Value
					&& txtMasLoc.Text.Trim() != string.Empty
					&& txtLocation.Text.Trim() != string.Empty)
				{
					if (dtmPostDate.Text.Trim() != string.Empty)
					{
						if (txtBin.Text.Trim() == string.Empty)
						{
//							txtAvailQuantity.Value = new InventoryUtilsBO().GetAvailableQtyByPostDate((DateTime)dtmPostDate.Value, int.Parse(cboCCN.SelectedValue.ToString()),
//								voIV_Adjustment.MasterLocationID, int.Parse(txtLocation.Tag.ToString()), 0, voIV_Adjustment.ProductID);
							txtAvailQuantity.Value = new InventoryUtilsBO().GetAvailableQtyByPostDate(dtmCurrentDate, int.Parse(cboCCN.SelectedValue.ToString()),
								voIV_Adjustment.MasterLocationID, int.Parse(txtLocation.Tag.ToString()), 0, voIV_Adjustment.ProductID);
						}
						else
//							txtAvailQuantity.Value = new InventoryUtilsBO().GetAvailableQtyByPostDate((DateTime)dtmPostDate.Value, int.Parse(cboCCN.SelectedValue.ToString()),
//								voIV_Adjustment.MasterLocationID, int.Parse(txtLocation.Tag.ToString()), (int) txtBin.Tag, voIV_Adjustment.ProductID);
							txtAvailQuantity.Value = new InventoryUtilsBO().GetAvailableQtyByPostDate(dtmCurrentDate, int.Parse(cboCCN.SelectedValue.ToString()),
								voIV_Adjustment.MasterLocationID, int.Parse(txtLocation.Tag.ToString()), (int) txtBin.Tag, voIV_Adjustment.ProductID);
						}
				}
			}
			catch (Exception ex)
			{
				// error from date time control when user select date out of range
				if (ex.Message.Equals(dtmPostDate.PostValidation.ErrorMessage))
					return;
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

		private void txtPartNumber_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtPartNumber_Validating()";
			try
			{
				if (txtPartNumber.Text.Trim() == string.Empty)
				{
					ClearSomeControls();
					txtPartName.Text = string.Empty;
					txtModel.Text = string.Empty;
					return;
				}
				if (!txtPartNumber.Modified) return;
				
				if (FormControlComponents.CheckMandatory(dtmPostDate))
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID);
					dtmPostDate.Focus();
					dtmPostDate.Select();
					return;
				}
				//Search Part Name
				Hashtable htbCriteria = new Hashtable();
				DataRowView drwResult = null;
				drwResult = FormControlComponents.OpenSearchForm(V_PRODUCTINVENTORYADJUSTMENT, PARTNUMBER, txtPartNumber.Text.Trim(), null, false);
				if (drwResult != null)
				{
					voIV_Adjustment.ProductID = int.Parse(drwResult[ITM_ProductTable.PRODUCTID_FLD].ToString());
					txtPartNumber.Text = drwResult[PARTNUMBER].ToString();
					txtPartName.Text = drwResult[PARTNAME].ToString();
					txtModel.Text = drwResult[MODEL].ToString();
					if (drwResult[MASLOCNAME] != DBNull.Value)
					{
						voIV_Adjustment.MasterLocationID = int.Parse(drwResult[MST_MasterLocationTable.MASTERLOCATIONID_FLD].ToString());
						txtMasLoc.Text = drwResult[MASLOCNAME].ToString();
					}
					else
						voIV_Adjustment.MasterLocationID = 0;
//					if (drwResult[LOCNAME] != DBNull.Value)
//					{
//						txtLocation.Text = drwResult[LOCNAME].ToString();
//						txtLocation.Tag = drwResult[MST_LocationTable.LOCATIONID_FLD];
//					}
					if (drwResult[UM] != DBNull.Value)
					{
						txtUM.Text = drwResult[UM].ToString();
						voIV_Adjustment.StockUMID = int.Parse(drwResult[ITM_ProductTable.STOCKUMID_FLD].ToString());
					}
					//Check Available if have Bin and Location
					FillAvailableQuantity(voIV_Adjustment.ProductID);
//					if (drwResult[ITM_ProductTable.LOTCONTROL_FLD] != DBNull.Value)
//					{
//						if (drwResult[ITM_ProductTable.LOTCONTROL_FLD].ToString() == TRUE)
//						{
//							blnLotControl = true;
//							txtLot.Enabled = true;
//							btnLot.Enabled = true;
//							txtSerial.Enabled = true;
//							intLotSize = int.Parse(drwResult[ITM_ProductTable.LOTSIZE_FLD].ToString());
//						}
//						else
//						{
//							blnLotControl = false;
//							txtLot.Enabled = false;
//							btnLot.Enabled = false;
//							txtSerial.Enabled = false;
//						}
//					}
//					if (drwResult[BINCONTROL] != DBNull.Value)
//					{
//						//Check if BinControl == true
//						if (drwResult[BINCONTROL].ToString() == TRUE)
//						{
//							blnBinControl = true;
//							txtBin.Enabled = true;
//							btnBin.Enabled = true;
//							txtQAStatus.Text = drwResult[BINQASTATUS].ToString();
//							txtAvailQuantity.Value = drwResult[BIN_AVAILABLE_QUANTITY];	
//							if (drwResult[BINNAME] != DBNull.Value)
//							{
//								txtBin.Text = drwResult[BINNAME].ToString();
//							}
//							if (drwResult[MST_BINTable.BINID_FLD] != DBNull.Value)
//							{
//								txtBin.Tag = drwResult[MST_BINTable.BINID_FLD];	
//								decimal decAvailableQuantity = new InventoryUtilsBO().GetAvailableQtyByPostDate((DateTime)dtmPostDate.Value, int.Parse(cboCCN.SelectedValue.ToString()),
//									voIV_Adjustment.MasterLocationID, int.Parse(txtLocation.Tag.ToString()), (int) txtBin.Tag, voIV_Adjustment.ProductID);
//								txtAvailQuantity.Value = decAvailableQuantity;
//							}
//							else
//								txtAdjustQuantity.Value = DBNull.Value;
//						}
//						else
//						{
//							blnBinControl = false;
//							txtBin.Enabled = false;
//							btnBin.Enabled = false;
//							txtQAStatus.Text = drwResult[LOCQASTATUS].ToString();
//							txtAvailQuantity.Value = drwResult[LOCAVAILABLE_QUANTITY];
//							txtBin.Tag = null;
//							txtBin.Text = string.Empty;
//							decimal decAvailableQuantity = new InventoryUtilsBO().GetAvailableQtyByPostDate((DateTime)dtmPostDate.Value, int.Parse(cboCCN.SelectedValue.ToString()),
//								voIV_Adjustment.MasterLocationID, int.Parse(txtLocation.Tag.ToString()), 0, voIV_Adjustment.ProductID);
//							txtAvailQuantity.Value = decAvailableQuantity;
//						}
//					}
//					
				}
				else
					e.Cancel = true;
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

		private void txtMasLoc_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtMasLoc_Validating()";
			try
			{		
				if (!txtMasLoc.Modified) return;
				if (txtMasLoc.Text.Trim() == string.Empty)
				{
					voIV_Adjustment.MasterLocationID = 0;
					txtMasLoc.Tag = null;
					lblMasLoc.Tag = null;
					txtLocation.Text = string.Empty;
					txtLocation.Tag = null;
					txtBin.Tag = null;
					txtBin.Text = string.Empty;
					txtAvailQuantity.Value = null;
					return;
				}
				DataRowView drwResult = null;
				Hashtable htbCriteria = new Hashtable();

				drwResult = FormControlComponents.OpenSearchForm(MST_MasterLocationTable.TABLE_NAME, MST_MasterLocationTable.CODE_FLD, txtMasLoc.Text.Trim(), htbCriteria, false);
				if (drwResult != null)
				{
					if (voIV_Adjustment.MasterLocationID != 0)
					{
						if (voIV_Adjustment.MasterLocationID != int.Parse(drwResult[MST_MasterLocationTable.MASTERLOCATIONID_FLD].ToString()))
						{
							ClearSomeControls();
						}
					}
					//lblMasLoc.Tag = drwResult[MST_MasterLocationTable.MASTERLOCATIONID_FLD];
					voIV_Adjustment.MasterLocationID = int.Parse(drwResult[MST_MasterLocationTable.MASTERLOCATIONID_FLD].ToString());

					txtMasLoc.Text = drwResult[MST_MasterLocationTable.CODE_FLD].ToString();
				}
				else
					e.Cancel = true;
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
				// displays the error message.
				PCSMessageBox.Show(ErrorCode.OTHER_ERROR,MessageBoxIcon.Error);
				// log message.
				try
				{
					Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
				}
				catch
				{
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION,MessageBoxIcon.Error);
				}
			}
		}

		private void txtLocation_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtLocation_Validating()";
			try
			{
				if (!txtLocation.Modified) return;
				Hashtable htbCriteria = new Hashtable();
				if (txtLocation.Text.Trim() == string.Empty)
				{
					txtLocation.Tag = null;
					txtBin.Text = string.Empty;
					txtBin.Tag = null;
					ClearSomeControls();
					return;
				}
				if (txtMasLoc.Text.Trim() == string.Empty)
				{
					PCSMessageBox.Show(ErrorCode.MSG_LABORTIME_SELECTMASLOC, MessageBoxIcon.Error);
					txtLocation.Text = string.Empty;
					txtMasLoc.Focus();
					txtMasLoc.Select();
					return;
				}
				else
					htbCriteria.Add(MST_LocationTable.MASTERLOCATIONID_FLD, voIV_Adjustment.MasterLocationID);

				DataRowView drwResult = null;
				drwResult = FormControlComponents.OpenSearchForm(MST_LocationTable.TABLE_NAME, MST_LocationTable.CODE_FLD, txtLocation.Text.Trim(), htbCriteria, false);
				if (drwResult != null)
				{
					if (txtLocation.Tag != null)
					{
						if (txtLocation.Tag.ToString() != drwResult[MST_LocationTable.LOCATIONID_FLD].ToString())
						{
						
							txtLocation.Tag = drwResult[MST_LocationTable.LOCATIONID_FLD];
							//ClearForm();
							ClearSomeControls();
						}
					}
					txtLocation.Tag = drwResult[MST_LocationTable.LOCATIONID_FLD];
					txtLocation.Text = drwResult[MST_LocationTable.CODE_FLD].ToString();
					//Check if bin control is true
//					if (drwResult[MST_LocationTable.BIN_FLD].ToString() == TRUE)
//					{
//						//Clear Bin, QA Status, Available Quantity
//						txtQAStatus.Text = string.Empty;
//						txtAvailQuantity.Value = null;
//						blnBinControl = true;
//					}
//					else 
//					{
//						blnBinControl = false;
//						txtBin.Text = string.Empty;
//						txtBin.Tag = null;
//						txtBin.Enabled = false;
//						btnBin.Enabled = false;
//
//						//Get available quantity where product
//						IVInventoryAdjustmentBO boIVInventoryAdjustment = new IVInventoryAdjustmentBO();
//						dstAvailableQtyAndInsStatus = boIVInventoryAdjustment.GetAvalableQuantity(int.Parse(cboCCN.SelectedValue.ToString()),
//							voIV_Adjustment.MasterLocationID, int.Parse(txtLocation.Tag.ToString()), 0, voIV_Adjustment.ProductID);
//						if (dstAvailableQtyAndInsStatus.Tables[0].Rows.Count > 0)
//						{
//							txtAvailQuantity.Value = dstAvailableQtyAndInsStatus.Tables[0].Rows[0][AVAILABLE_QUANTITY];
//							txtQAStatus.Text = dstAvailableQtyAndInsStatus.Tables[0].Rows[0][IV_LocationCacheTable.INSPSTATUS_FLD].ToString();
//						}
//						else
//						{
//							txtAvailQuantity.Value = 0;
//							txtQAStatus.Text = string.Empty;
//						}
//					}
				}
				else
					e.Cancel = true;
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

		private void txtBin_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtBin_Validating()";
			try
			{
				Hashtable htbCriteria = new Hashtable();
				if (!txtBin.Modified) return;
				if (txtBin.Text.Trim() == string.Empty)
				{
					txtBin.Tag = null;
					txtAvailQuantity.Value = null;
					txtQAStatus.Text = string.Empty;
					return;
				}
				if (txtLocation.Text.Trim() == string.Empty)
				{
					PCSMessageBox.Show(ErrorCode.MESSAGE_RGA_SELECTLOC, MessageBoxIcon.Error);
					txtBin.Text = string.Empty;
					txtLocation.Focus();
					txtLocation.Select();
					return;
				}
				else
					htbCriteria.Add(MST_BINTable.LOCATIONID_FLD, int.Parse(txtLocation.Tag.ToString()));

				DataRowView drwResult = null;
				drwResult = FormControlComponents.OpenSearchForm(MST_BINTable.TABLE_NAME, MST_BINTable.CODE_FLD, txtBin.Text.Trim(), htbCriteria, false);
				if (drwResult != null)
				{
					txtBin.Text = drwResult[MST_BINTable.CODE_FLD].ToString();
					txtBin.Tag = drwResult[MST_BINTable.BINID_FLD];

					//Get available quantity where product
//					IVInventoryAdjustmentBO boIVInventoryAdjustment = new IVInventoryAdjustmentBO();
//					dstAvailableQtyAndInsStatus = boIVInventoryAdjustment.GetAvalableQuantity(int.Parse(cboCCN.SelectedValue.ToString()),
//						voIV_Adjustment.MasterLocationID, int.Parse(txtLocation.Tag.ToString()), int.Parse(txtBin.Tag.ToString()), voIV_Adjustment.ProductID);
//					
					if (txtPartNumber.Text.Trim() != string.Empty)
					{
						decimal decAvailableQuantity = new InventoryUtilsBO().GetAvailableQtyByPostDate(dtmCurrentDate, int.Parse(cboCCN.SelectedValue.ToString()),
							voIV_Adjustment.MasterLocationID, int.Parse(txtLocation.Tag.ToString()), (int) txtBin.Tag, voIV_Adjustment.ProductID);
						txtAvailQuantity.Value = decAvailableQuantity;
					}
//					if (dstAvailableQtyAndInsStatus.Tables[0].Rows.Count > 0)
//					{
//						txtAvailQuantity.Value = dstAvailableQtyAndInsStatus.Tables[0].Rows[0][AVAILABLE_QUANTITY];
//						txtQAStatus.Text = dstAvailableQtyAndInsStatus.Tables[0].Rows[0][IV_LocationCacheTable.INSPSTATUS_FLD].ToString();
//					}
//					else
//					{
//						txtAvailQuantity.Value = decAvailableQuantity;
//						txtQAStatus.Text = string.Empty;
//					}
					
				}
				else
					e.Cancel = true;
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
				// displays the error message.
				PCSMessageBox.Show(ErrorCode.OTHER_ERROR,MessageBoxIcon.Error);
				// log message.
				try
				{
					Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
				}
				catch
				{
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION,MessageBoxIcon.Error);
				}
			}
		}
		
		private void txtPartName_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtPartName_Validating()";
			try
			{
				if (txtPartName.Text.Trim() == string.Empty)
				{
					ClearSomeControls();
					txtPartNumber.Text = string.Empty;
					txtModel.Text = string.Empty;
					return;
				}

				if (!txtPartName.Modified) return;
				if (FormControlComponents.CheckMandatory(dtmPostDate))
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID);
					dtmPostDate.Focus();
					dtmPostDate.Select();
					return;
				}
				//Search Part Name
				Hashtable htbCriteria = new Hashtable();
				DataRowView drwResult = null;
				drwResult = FormControlComponents.OpenSearchForm(V_PRODUCTINVENTORYADJUSTMENT, PARTNAME, txtPartName.Text.Trim(), null, false);
				if (drwResult != null)
				{
					voIV_Adjustment.ProductID = int.Parse(drwResult[ITM_ProductTable.PRODUCTID_FLD].ToString());
					txtPartNumber.Text = drwResult[PARTNUMBER].ToString();
					txtPartName.Text = drwResult[PARTNAME].ToString();
					txtModel.Text = drwResult[MODEL].ToString();
					if (drwResult[MASLOCNAME] != DBNull.Value)
					{
						voIV_Adjustment.MasterLocationID = int.Parse(drwResult[MST_MasterLocationTable.MASTERLOCATIONID_FLD].ToString());
						txtMasLoc.Text = drwResult[MASLOCNAME].ToString();
					}
//					if (drwResult[LOCNAME] != DBNull.Value)
//					{
//						txtLocation.Text = drwResult[LOCNAME].ToString();
//						txtLocation.Tag = drwResult[MST_LocationTable.LOCATIONID_FLD];
//					}
					if (drwResult[UM] != DBNull.Value)
					{
						txtUM.Text = drwResult[UM].ToString();
						voIV_Adjustment.StockUMID = int.Parse(drwResult[ITM_ProductTable.STOCKUMID_FLD].ToString());
					}
					//Check Available if have Bin and Location
					FillAvailableQuantity(voIV_Adjustment.ProductID);
//					if (drwResult[ITM_ProductTable.LOTCONTROL_FLD] != DBNull.Value)
//					{
//						if (drwResult[ITM_ProductTable.LOTCONTROL_FLD].ToString() == TRUE)
//						{
//							blnLotControl = true;
//							txtLot.Enabled = true;
//							btnLot.Enabled = true;
//							txtSerial.Enabled = true;
//							intLotSize = int.Parse(drwResult[ITM_ProductTable.LOTSIZE_FLD].ToString());
//						}
//						else
//						{
//							blnLotControl = false;
//							txtLot.Enabled = false;
//							btnLot.Enabled = false;
//							txtSerial.Enabled = false;
//						}
//					}
//					if (drwResult[BINCONTROL] != DBNull.Value)
//					{
//						//Check if BinControl == true
//						if (drwResult[BINCONTROL].ToString() == TRUE)
//						{
//							blnBinControl = true;
//							txtBin.Enabled = true;
//							btnBin.Enabled = true;
//							txtQAStatus.Text = drwResult[BINQASTATUS].ToString();
//							txtAvailQuantity.Value = drwResult[BIN_AVAILABLE_QUANTITY];	
//							if (drwResult[BINNAME] != DBNull.Value)
//							{
//								txtBin.Text = drwResult[BINNAME].ToString();
//							}
//							if (drwResult[MST_BINTable.BINID_FLD] != DBNull.Value)
//							{
//								txtBin.Tag = drwResult[MST_BINTable.BINID_FLD];	
//								decimal decAvailableQuantity = new InventoryUtilsBO().GetAvailableQtyByPostDate((DateTime)dtmPostDate.Value, int.Parse(cboCCN.SelectedValue.ToString()),
//									voIV_Adjustment.MasterLocationID, int.Parse(txtLocation.Tag.ToString()), (int) txtBin.Tag, voIV_Adjustment.ProductID);
//								txtAvailQuantity.Value = decAvailableQuantity;
//							}
//							else
//							{
//								txtAvailQuantity.Value = DBNull.Value;
//							}
//						}
//						else
//						{
//							blnBinControl = false;
//							txtBin.Enabled = false;
//							btnBin.Enabled = false;
//							txtQAStatus.Text = drwResult[LOCQASTATUS].ToString();
//							txtAvailQuantity.Value = drwResult[LOCAVAILABLE_QUANTITY];
//							txtBin.Tag = null;
//							txtBin.Text = string.Empty;
//							decimal decAvailableQuantity = new InventoryUtilsBO().GetAvailableQtyByPostDate((DateTime)dtmPostDate.Value, int.Parse(cboCCN.SelectedValue.ToString()),
//								voIV_Adjustment.MasterLocationID, int.Parse(txtLocation.Tag.ToString()), 0, voIV_Adjustment.ProductID);
//							txtAvailQuantity.Value = decAvailableQuantity;
//						}
//					}
//					
				}
				else
					e.Cancel = true;
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

		private void btnDelete_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnDelete_Click()";
			try
			{
				if (PCSMessageBox.Show(ErrorCode.MESSAGE_DELETE_RECORD, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
				{ // Delete voucher
					IVInventoryAdjustmentBO boIVInventoryAdjustment = new IVInventoryAdjustmentBO();
					boIVInventoryAdjustment.DeleteInventoryAdjustment((int)txtTransNo.Tag);

					// Reset form IVInventoryAdjustment
					ClearForm();
					SwitchFormMode();
				
					PCSMessageBox.Show(ErrorCode.MESSAGE_AFTER_SAVE_DATA);

					//Set focus to PostDate
					dtmPostDate.Focus();
					
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

		private void btnDelTran_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnDelete_Click()";
			try
			{
				if (PCSMessageBox.Show(ErrorCode.MESSAGE_DELETE_RECORD, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
				{ // Delete voucher
					IVInventoryAdjustmentBO boIVInventoryAdjustment = new IVInventoryAdjustmentBO();
					boIVInventoryAdjustment.DeleteInventoryAdjustmentTransaction((int)txtTransNo.Tag);

					// Reset form IVInventoryAdjustment
					ClearForm();
					SwitchFormMode();
				
					PCSMessageBox.Show(ErrorCode.MESSAGE_AFTER_SAVE_DATA);

					//Set focus to PostDate
					dtmPostDate.Focus();
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

	}
}
