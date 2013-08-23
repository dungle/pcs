using System;
using System.Data;
using System.Collections;
using System.Linq;
using System.Windows.Forms;
using C1.Win.C1TrueDBGrid;
using PCSComMaterials.Inventory.BO;
using PCSComMaterials.Inventory.DS;
using PCSComUtils.Common;
using PCSComUtils.Common.BO;
using PCSComUtils.PCSExc;
using PCSUtils.Log;
using PCSUtils.Utils;

namespace PCSMaterials.Inventory
{
	/// <summary>
	/// Summary description for StockTaking.
	/// </summary>
	public class StockTaking : System.Windows.Forms.Form
	{
		DataSet dstTemp;
		DataTable dtbOnHandQty;
		EnumAction frmStatus = EnumAction.Default;
		private C1.Win.C1TrueDBGrid.C1TrueDBGrid dgrdData;
		private System.Windows.Forms.Label lblCCN;
		private System.Windows.Forms.Button btnClose;
		private System.Windows.Forms.Button btnHelp;
		private System.Windows.Forms.TextBox txtStockTakingPeriod;
		private System.Windows.Forms.Label lblStockTakingPeriod;
		private System.Windows.Forms.Button btnStockTakingPeriod;
		private C1.Win.C1Input.C1DateEdit dtmStockTakingDate;
		private System.Windows.Forms.Label lblStockTakingDate;
		private C1.Win.C1Input.C1DateEdit dtmFromDate;
		private System.Windows.Forms.Label lblFromDate;
		private C1.Win.C1Input.C1DateEdit dtmToDate;
		private System.Windows.Forms.Label lblToDate;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		const string THIS = "PCSMaterials.Inventory.StockTaking";
		const string V_PRODUCTFORSTOCKTAKING = "V_ProductForStockTaking";
		const string V_LOCATIONANDPRODUCTIONLINE = "V_LocationAndProductionLine";
		
		private System.Windows.Forms.TextBox txtCCN;
		DataTable dtbGridDesign = new DataTable(); 
		DataSet dstGridData = new DataSet();
		DataSet dstStockTaking = new DataSet();
		bool blnAddNewDataSet = false;
		private System.Windows.Forms.Label lblDepartment;
		private System.Windows.Forms.Label lblProductionLine;
		private System.Windows.Forms.TextBox txtDepartment;
		private System.Windows.Forms.Button btnDepartment;
		private System.Windows.Forms.TextBox txtProductionLine;
		private System.Windows.Forms.Button btnProductionLine;
		private System.Windows.Forms.TextBox txtLocation;
		private System.Windows.Forms.Button btnLocation;
		private System.Windows.Forms.Label lblLocation;
		private System.Windows.Forms.TextBox txtBin;
		private System.Windows.Forms.Button btnBin;
		private System.Windows.Forms.Label lblBin;
		private System.Windows.Forms.TextBox txtStockTakingNo;
		private System.Windows.Forms.Button btnStockTakingNo;
		private System.Windows.Forms.Label lblStockTakingNo;
		private System.Windows.Forms.Button btnEdit;
		private System.Windows.Forms.Button btnDelete;
		private System.Windows.Forms.Button btnAdd;
		private System.Windows.Forms.Button btnSave;
		bool blnHasError = false;
		public StockTaking()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StockTaking));
            this.dgrdData = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
            this.txtStockTakingPeriod = new System.Windows.Forms.TextBox();
            this.btnStockTakingPeriod = new System.Windows.Forms.Button();
            this.lblStockTakingPeriod = new System.Windows.Forms.Label();
            this.dtmFromDate = new C1.Win.C1Input.C1DateEdit();
            this.lblFromDate = new System.Windows.Forms.Label();
            this.dtmStockTakingDate = new C1.Win.C1Input.C1DateEdit();
            this.lblStockTakingDate = new System.Windows.Forms.Label();
            this.dtmToDate = new C1.Win.C1Input.C1DateEdit();
            this.lblToDate = new System.Windows.Forms.Label();
            this.lblCCN = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnHelp = new System.Windows.Forms.Button();
            this.txtCCN = new System.Windows.Forms.TextBox();
            this.txtDepartment = new System.Windows.Forms.TextBox();
            this.btnDepartment = new System.Windows.Forms.Button();
            this.lblDepartment = new System.Windows.Forms.Label();
            this.txtProductionLine = new System.Windows.Forms.TextBox();
            this.btnProductionLine = new System.Windows.Forms.Button();
            this.lblProductionLine = new System.Windows.Forms.Label();
            this.txtLocation = new System.Windows.Forms.TextBox();
            this.btnLocation = new System.Windows.Forms.Button();
            this.lblLocation = new System.Windows.Forms.Label();
            this.txtBin = new System.Windows.Forms.TextBox();
            this.btnBin = new System.Windows.Forms.Button();
            this.lblBin = new System.Windows.Forms.Label();
            this.txtStockTakingNo = new System.Windows.Forms.TextBox();
            this.btnStockTakingNo = new System.Windows.Forms.Button();
            this.lblStockTakingNo = new System.Windows.Forms.Label();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgrdData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtmFromDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtmStockTakingDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtmToDate)).BeginInit();
            this.SuspendLayout();
            // 
            // dgrdData
            // 
            this.dgrdData.AllowAddNew = true;
            this.dgrdData.AllowDelete = true;
            this.dgrdData.AllowFilter = false;
            this.dgrdData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgrdData.GroupByCaption = "Drag a column header here to group by that column";
            this.dgrdData.Images.Add(((System.Drawing.Image)(resources.GetObject("dgrdData.Images"))));
            this.dgrdData.Location = new System.Drawing.Point(8, 98);
            this.dgrdData.Name = "dgrdData";
            this.dgrdData.PreviewInfo.Location = new System.Drawing.Point(0, 0);
            this.dgrdData.PreviewInfo.Size = new System.Drawing.Size(0, 0);
            this.dgrdData.PreviewInfo.ZoomFactor = 75D;
            this.dgrdData.PrintInfo.PageSettings = ((System.Drawing.Printing.PageSettings)(resources.GetObject("dgrdData.PrintInfo.PageSettings")));
            this.dgrdData.Size = new System.Drawing.Size(712, 346);
            this.dgrdData.TabIndex = 26;
            this.dgrdData.Text = "c1TrueDBGrid1";
            this.dgrdData.AfterColUpdate += new C1.Win.C1TrueDBGrid.ColEventHandler(this.dgrdData_AfterColUpdate);
            this.dgrdData.BeforeColUpdate += new C1.Win.C1TrueDBGrid.BeforeColUpdateEventHandler(this.dgrdData_BeforeColUpdate);
            this.dgrdData.ButtonClick += new C1.Win.C1TrueDBGrid.ColEventHandler(this.dgrdData_ButtonClick);
            this.dgrdData.Click += new System.EventHandler(this.dgrdData_Click);
            this.dgrdData.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgrdData_KeyDown);
            this.dgrdData.PropBag = resources.GetString("dgrdData.PropBag");
            // 
            // txtStockTakingPeriod
            // 
            this.txtStockTakingPeriod.Location = new System.Drawing.Point(116, 30);
            this.txtStockTakingPeriod.Name = "txtStockTakingPeriod";
            this.txtStockTakingPeriod.Size = new System.Drawing.Size(96, 20);
            this.txtStockTakingPeriod.TabIndex = 6;
            this.txtStockTakingPeriod.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtStockTakingPeriod_KeyDown);
            this.txtStockTakingPeriod.Validating += new System.ComponentModel.CancelEventHandler(this.txtStockTakingPeriod_Validating);
            // 
            // btnStockTakingPeriod
            // 
            this.btnStockTakingPeriod.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnStockTakingPeriod.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnStockTakingPeriod.Location = new System.Drawing.Point(214, 30);
            this.btnStockTakingPeriod.Name = "btnStockTakingPeriod";
            this.btnStockTakingPeriod.Size = new System.Drawing.Size(22, 20);
            this.btnStockTakingPeriod.TabIndex = 7;
            this.btnStockTakingPeriod.Text = "...";
            this.btnStockTakingPeriod.Click += new System.EventHandler(this.btnStockTakingPeriod_Click);
            // 
            // lblStockTakingPeriod
            // 
            this.lblStockTakingPeriod.ForeColor = System.Drawing.Color.Maroon;
            this.lblStockTakingPeriod.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblStockTakingPeriod.Location = new System.Drawing.Point(8, 30);
            this.lblStockTakingPeriod.Name = "lblStockTakingPeriod";
            this.lblStockTakingPeriod.Size = new System.Drawing.Size(106, 20);
            this.lblStockTakingPeriod.TabIndex = 5;
            this.lblStockTakingPeriod.Text = "Stock Taking Period";
            this.lblStockTakingPeriod.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dtmFromDate
            // 
            // 
            // 
            // 
            this.dtmFromDate.Calendar.DayNameLength = 1;
            this.dtmFromDate.Calendar.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.dtmFromDate.Calendar.MaxDate = new System.DateTime(2100, 12, 31, 0, 0, 0, 0);
            this.dtmFromDate.Calendar.ShowClearButton = false;
            this.dtmFromDate.CustomFormat = "dd-MM-yyyy HH:mm";
            this.dtmFromDate.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
            this.dtmFromDate.Location = new System.Drawing.Point(116, 52);
            this.dtmFromDate.Name = "dtmFromDate";
            this.dtmFromDate.PostValidation.Intervals.AddRange(new C1.Win.C1Input.ValueInterval[] {
            new C1.Win.C1Input.ValueInterval(new System.DateTime(1753, 1, 1, 0, 0, 0, 0), new System.DateTime(2100, 12, 31, 12, 0, 0, 0), true, true)});
            this.dtmFromDate.ReadOnly = true;
            this.dtmFromDate.Size = new System.Drawing.Size(122, 20);
            this.dtmFromDate.TabIndex = 9;
            this.dtmFromDate.Tag = null;
            this.dtmFromDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.dtmFromDate.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.DropDown;
            // 
            // lblFromDate
            // 
            this.lblFromDate.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblFromDate.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblFromDate.Location = new System.Drawing.Point(8, 52);
            this.lblFromDate.Name = "lblFromDate";
            this.lblFromDate.Size = new System.Drawing.Size(106, 20);
            this.lblFromDate.TabIndex = 8;
            this.lblFromDate.Text = "From Date";
            this.lblFromDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dtmStockTakingDate
            // 
            // 
            // 
            // 
            this.dtmStockTakingDate.Calendar.DayNameLength = 1;
            this.dtmStockTakingDate.Calendar.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.dtmStockTakingDate.Calendar.MaxDate = new System.DateTime(2100, 12, 31, 0, 0, 0, 0);
            this.dtmStockTakingDate.Calendar.ShowClearButton = false;
            this.dtmStockTakingDate.CustomFormat = "dd-MM-yyyy HH:mm";
            this.dtmStockTakingDate.EmptyAsNull = true;
            this.dtmStockTakingDate.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
            this.dtmStockTakingDate.Location = new System.Drawing.Point(394, 8);
            this.dtmStockTakingDate.Name = "dtmStockTakingDate";
            this.dtmStockTakingDate.PostValidation.Intervals.AddRange(new C1.Win.C1Input.ValueInterval[] {
            new C1.Win.C1Input.ValueInterval(new System.DateTime(1753, 1, 1, 0, 0, 0, 0), new System.DateTime(2100, 12, 31, 12, 0, 0, 0), true, true)});
            this.dtmStockTakingDate.Size = new System.Drawing.Size(122, 20);
            this.dtmStockTakingDate.TabIndex = 13;
            this.dtmStockTakingDate.Tag = null;
            this.dtmStockTakingDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.dtmStockTakingDate.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.DropDown;
            this.dtmStockTakingDate.Validating += new System.ComponentModel.CancelEventHandler(this.dtmStockTakingDate_Validating);
            // 
            // lblStockTakingDate
            // 
            this.lblStockTakingDate.ForeColor = System.Drawing.Color.Maroon;
            this.lblStockTakingDate.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblStockTakingDate.Location = new System.Drawing.Point(296, 8);
            this.lblStockTakingDate.Name = "lblStockTakingDate";
            this.lblStockTakingDate.Size = new System.Drawing.Size(96, 20);
            this.lblStockTakingDate.TabIndex = 12;
            this.lblStockTakingDate.Text = "Taking Date";
            this.lblStockTakingDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dtmToDate
            // 
            // 
            // 
            // 
            this.dtmToDate.Calendar.DayNameLength = 1;
            this.dtmToDate.Calendar.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.dtmToDate.Calendar.MaxDate = new System.DateTime(2100, 12, 31, 0, 0, 0, 0);
            this.dtmToDate.Calendar.ShowClearButton = false;
            this.dtmToDate.CustomFormat = "dd-MM-yyyy HH:mm";
            this.dtmToDate.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
            this.dtmToDate.Location = new System.Drawing.Point(116, 74);
            this.dtmToDate.Name = "dtmToDate";
            this.dtmToDate.PostValidation.Intervals.AddRange(new C1.Win.C1Input.ValueInterval[] {
            new C1.Win.C1Input.ValueInterval(new System.DateTime(1753, 1, 1, 0, 0, 0, 0), new System.DateTime(2100, 12, 31, 12, 0, 0, 0), true, true)});
            this.dtmToDate.ReadOnly = true;
            this.dtmToDate.Size = new System.Drawing.Size(122, 20);
            this.dtmToDate.TabIndex = 11;
            this.dtmToDate.Tag = null;
            this.dtmToDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.dtmToDate.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.DropDown;
            // 
            // lblToDate
            // 
            this.lblToDate.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblToDate.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblToDate.Location = new System.Drawing.Point(8, 74);
            this.lblToDate.Name = "lblToDate";
            this.lblToDate.Size = new System.Drawing.Size(106, 20);
            this.lblToDate.TabIndex = 10;
            this.lblToDate.Text = "To Date";
            this.lblToDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblCCN
            // 
            this.lblCCN.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCCN.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblCCN.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblCCN.Location = new System.Drawing.Point(606, 8);
            this.lblCCN.Name = "lblCCN";
            this.lblCCN.Size = new System.Drawing.Size(30, 20);
            this.lblCCN.TabIndex = 0;
            this.lblCCN.Text = "CCN";
            this.lblCCN.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(645, 448);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 32;
            this.btnClose.Text = "C&lose";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnHelp
            // 
            this.btnHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnHelp.Location = new System.Drawing.Point(568, 448);
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Size = new System.Drawing.Size(75, 23);
            this.btnHelp.TabIndex = 31;
            this.btnHelp.Text = "&Help";
            // 
            // txtCCN
            // 
            this.txtCCN.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCCN.Location = new System.Drawing.Point(646, 8);
            this.txtCCN.Name = "txtCCN";
            this.txtCCN.ReadOnly = true;
            this.txtCCN.Size = new System.Drawing.Size(72, 20);
            this.txtCCN.TabIndex = 1;
            // 
            // txtDepartment
            // 
            this.txtDepartment.Location = new System.Drawing.Point(394, 30);
            this.txtDepartment.Name = "txtDepartment";
            this.txtDepartment.Size = new System.Drawing.Size(96, 20);
            this.txtDepartment.TabIndex = 15;
            this.txtDepartment.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtDepartment_KeyDown);
            this.txtDepartment.Validating += new System.ComponentModel.CancelEventHandler(this.txtDepartment_Validating);
            // 
            // btnDepartment
            // 
            this.btnDepartment.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnDepartment.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnDepartment.Location = new System.Drawing.Point(492, 30);
            this.btnDepartment.Name = "btnDepartment";
            this.btnDepartment.Size = new System.Drawing.Size(22, 20);
            this.btnDepartment.TabIndex = 16;
            this.btnDepartment.Text = "...";
            this.btnDepartment.Click += new System.EventHandler(this.btnDepartment_Click);
            // 
            // lblDepartment
            // 
            this.lblDepartment.ForeColor = System.Drawing.Color.Maroon;
            this.lblDepartment.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblDepartment.Location = new System.Drawing.Point(296, 30);
            this.lblDepartment.Name = "lblDepartment";
            this.lblDepartment.Size = new System.Drawing.Size(96, 20);
            this.lblDepartment.TabIndex = 14;
            this.lblDepartment.Text = "Department";
            this.lblDepartment.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtProductionLine
            // 
            this.txtProductionLine.Location = new System.Drawing.Point(394, 52);
            this.txtProductionLine.Name = "txtProductionLine";
            this.txtProductionLine.Size = new System.Drawing.Size(96, 20);
            this.txtProductionLine.TabIndex = 18;
            this.txtProductionLine.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtProductionLine_KeyDown);
            this.txtProductionLine.Validating += new System.ComponentModel.CancelEventHandler(this.txtProductionLine_Validating);
            // 
            // btnProductionLine
            // 
            this.btnProductionLine.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnProductionLine.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnProductionLine.Location = new System.Drawing.Point(492, 52);
            this.btnProductionLine.Name = "btnProductionLine";
            this.btnProductionLine.Size = new System.Drawing.Size(22, 20);
            this.btnProductionLine.TabIndex = 19;
            this.btnProductionLine.Text = "...";
            this.btnProductionLine.Click += new System.EventHandler(this.btnProductionLine_Click);
            // 
            // lblProductionLine
            // 
            this.lblProductionLine.ForeColor = System.Drawing.Color.Maroon;
            this.lblProductionLine.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblProductionLine.Location = new System.Drawing.Point(296, 52);
            this.lblProductionLine.Name = "lblProductionLine";
            this.lblProductionLine.Size = new System.Drawing.Size(96, 20);
            this.lblProductionLine.TabIndex = 17;
            this.lblProductionLine.Text = "Production Line";
            this.lblProductionLine.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtLocation
            // 
            this.txtLocation.Location = new System.Drawing.Point(598, 30);
            this.txtLocation.Name = "txtLocation";
            this.txtLocation.Size = new System.Drawing.Size(96, 20);
            this.txtLocation.TabIndex = 21;
            this.txtLocation.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtLocation_KeyDown);
            this.txtLocation.Validating += new System.ComponentModel.CancelEventHandler(this.txtLocation_Validating);
            // 
            // btnLocation
            // 
            this.btnLocation.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnLocation.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnLocation.Location = new System.Drawing.Point(696, 30);
            this.btnLocation.Name = "btnLocation";
            this.btnLocation.Size = new System.Drawing.Size(22, 20);
            this.btnLocation.TabIndex = 22;
            this.btnLocation.Text = "...";
            this.btnLocation.Click += new System.EventHandler(this.btnLocation_Click);
            // 
            // lblLocation
            // 
            this.lblLocation.ForeColor = System.Drawing.Color.Maroon;
            this.lblLocation.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblLocation.Location = new System.Drawing.Point(538, 30);
            this.lblLocation.Name = "lblLocation";
            this.lblLocation.Size = new System.Drawing.Size(58, 20);
            this.lblLocation.TabIndex = 20;
            this.lblLocation.Text = "Location";
            this.lblLocation.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtBin
            // 
            this.txtBin.Location = new System.Drawing.Point(598, 52);
            this.txtBin.Name = "txtBin";
            this.txtBin.Size = new System.Drawing.Size(96, 20);
            this.txtBin.TabIndex = 24;
            this.txtBin.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBin_KeyDown);
            this.txtBin.Validating += new System.ComponentModel.CancelEventHandler(this.txtBin_Validating);
            // 
            // btnBin
            // 
            this.btnBin.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnBin.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnBin.Location = new System.Drawing.Point(696, 52);
            this.btnBin.Name = "btnBin";
            this.btnBin.Size = new System.Drawing.Size(22, 20);
            this.btnBin.TabIndex = 25;
            this.btnBin.Text = "...";
            this.btnBin.Click += new System.EventHandler(this.btnBin_Click);
            // 
            // lblBin
            // 
            this.lblBin.ForeColor = System.Drawing.Color.Maroon;
            this.lblBin.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblBin.Location = new System.Drawing.Point(538, 52);
            this.lblBin.Name = "lblBin";
            this.lblBin.Size = new System.Drawing.Size(58, 20);
            this.lblBin.TabIndex = 23;
            this.lblBin.Text = "Bin";
            this.lblBin.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtStockTakingNo
            // 
            this.txtStockTakingNo.Location = new System.Drawing.Point(116, 8);
            this.txtStockTakingNo.Name = "txtStockTakingNo";
            this.txtStockTakingNo.Size = new System.Drawing.Size(96, 20);
            this.txtStockTakingNo.TabIndex = 3;
            this.txtStockTakingNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtStockTakingNo_KeyDown);
            this.txtStockTakingNo.Validating += new System.ComponentModel.CancelEventHandler(this.txtStockTakingNo_Validating);
            // 
            // btnStockTakingNo
            // 
            this.btnStockTakingNo.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnStockTakingNo.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnStockTakingNo.Location = new System.Drawing.Point(214, 8);
            this.btnStockTakingNo.Name = "btnStockTakingNo";
            this.btnStockTakingNo.Size = new System.Drawing.Size(22, 20);
            this.btnStockTakingNo.TabIndex = 4;
            this.btnStockTakingNo.Text = "...";
            this.btnStockTakingNo.Click += new System.EventHandler(this.btnStockTakingNo_Click);
            // 
            // lblStockTakingNo
            // 
            this.lblStockTakingNo.ForeColor = System.Drawing.Color.Maroon;
            this.lblStockTakingNo.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblStockTakingNo.Location = new System.Drawing.Point(8, 8);
            this.lblStockTakingNo.Name = "lblStockTakingNo";
            this.lblStockTakingNo.Size = new System.Drawing.Size(106, 20);
            this.lblStockTakingNo.TabIndex = 2;
            this.lblStockTakingNo.Text = "Stock Taking No";
            this.lblStockTakingNo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnEdit
            // 
            this.btnEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnEdit.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnEdit.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnEdit.Location = new System.Drawing.Point(132, 448);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(60, 23);
            this.btnEdit.TabIndex = 29;
            this.btnEdit.Text = "&Edit";
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnDelete.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnDelete.Location = new System.Drawing.Point(194, 448);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(60, 23);
            this.btnDelete.TabIndex = 30;
            this.btnDelete.Text = "&Delete";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnSave.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnSave.Location = new System.Drawing.Point(70, 448);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(60, 23);
            this.btnSave.TabIndex = 28;
            this.btnSave.Text = "&Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnAdd.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnAdd.Location = new System.Drawing.Point(8, 448);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(60, 23);
            this.btnAdd.TabIndex = 27;
            this.btnAdd.Text = "&Add";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // StockTaking
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(728, 478);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.txtStockTakingNo);
            this.Controls.Add(this.txtBin);
            this.Controls.Add(this.txtLocation);
            this.Controls.Add(this.txtProductionLine);
            this.Controls.Add(this.txtDepartment);
            this.Controls.Add(this.txtCCN);
            this.Controls.Add(this.txtStockTakingPeriod);
            this.Controls.Add(this.dgrdData);
            this.Controls.Add(this.btnStockTakingNo);
            this.Controls.Add(this.lblStockTakingNo);
            this.Controls.Add(this.btnBin);
            this.Controls.Add(this.lblBin);
            this.Controls.Add(this.btnLocation);
            this.Controls.Add(this.lblLocation);
            this.Controls.Add(this.btnProductionLine);
            this.Controls.Add(this.lblProductionLine);
            this.Controls.Add(this.btnDepartment);
            this.Controls.Add(this.lblDepartment);
            this.Controls.Add(this.btnHelp);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.lblCCN);
            this.Controls.Add(this.dtmToDate);
            this.Controls.Add(this.lblToDate);
            this.Controls.Add(this.dtmStockTakingDate);
            this.Controls.Add(this.lblStockTakingDate);
            this.Controls.Add(this.dtmFromDate);
            this.Controls.Add(this.lblFromDate);
            this.Controls.Add(this.btnStockTakingPeriod);
            this.Controls.Add(this.lblStockTakingPeriod);
            this.KeyPreview = true;
            this.Name = "StockTaking";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Stock Taking";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.StockTaking_Closing);
            this.Load += new System.EventHandler(this.StockTaking_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.StockTaking_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.dgrdData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtmFromDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtmStockTakingDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtmToDate)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		private void btnClose_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}
		/// <summary>
		/// ConfigGrid
		/// </summary>
		/// <param name="pblnLock"></param>
		/// <author>Trada</author>
		/// <date>Tuesday, July 25 2006</date>
		private void ConfigGrid(bool pblnLock)
		{
			dgrdData.Enabled = true;
			for (int i =0; i <dgrdData.Splits[0].DisplayColumns.Count; i++)
			{
				dgrdData.Splits[0].DisplayColumns[i].Locked = true;
			}
			dgrdData.Splits[0].DisplayColumns["StockTakingCode"].Locked = pblnLock;
			dgrdData.Splits[0].DisplayColumns[ITM_ProductTable.CODE_FLD].Locked = pblnLock;
			dgrdData.Splits[0].DisplayColumns[ITM_ProductTable.DESCRIPTION_FLD].Locked = pblnLock;
			dgrdData.Splits[0].DisplayColumns[IV_StockTakingTable.QUANTITY_FLD].Locked = pblnLock;
			dgrdData.Splits[0].DisplayColumns[IV_StockTakingTable.SLIPCODE_FLD].Locked = pblnLock;
			dgrdData.Splits[0].DisplayColumns[IV_StockTakingTable.NOTE_FLD].Locked = pblnLock;
			dgrdData.Splits[0].DisplayColumns[IV_CoutingMethodTable.TABLE_NAME + IV_CoutingMethodTable.CODE_FLD].Locked = pblnLock;
			dgrdData.Splits[0].DisplayColumns[IV_StockTakingTable.QUANTITY_FLD].DataColumn.NumberFormat = Constants.DECIMAL_NUMBERFORMAT;
			dgrdData.Splits[0].DisplayColumns[IV_StockTakingTable.QUANTITY_FLD].Style.HorizontalAlignment = AlignHorzEnum.Far;
			dgrdData.Splits[0].DisplayColumns[IV_StockTakingTable.BOOKQUANTITY_FLD].DataColumn.NumberFormat = Constants.DECIMAL_NUMBERFORMAT;
			dgrdData.Splits[0].DisplayColumns[IV_StockTakingTable.BOOKQUANTITY_FLD].Style.HorizontalAlignment = AlignHorzEnum.Far;
			dgrdData.Splits[0].DisplayColumns[ITM_ProductTable.CODE_FLD].Locked = true;
			dgrdData.Splits[0].DisplayColumns[ITM_ProductTable.DESCRIPTION_FLD].Locked = true;

			if (!pblnLock)
			{
				dgrdData.Splits[0].DisplayColumns["StockTakingCode"].Button = true;
				dgrdData.Splits[0].DisplayColumns[IV_CoutingMethodTable.TABLE_NAME + IV_CoutingMethodTable.CODE_FLD].Button = true;
			}
		}

		/// <summary>
		/// CreateDataSet
		/// </summary>
		/// <author>Trada</author>
		/// <date>Tuesday, July 25 2006</date>
		private void CreateDataSet()
		{
			dstGridData = new DataSet();
			dstGridData.Tables.Add(IV_StockTakingTable.TABLE_NAME);

			dstGridData.Tables[IV_StockTakingTable.TABLE_NAME].Columns.Add(IV_StockTakingTable.PRODUCTID_FLD);
			dstGridData.Tables[IV_StockTakingTable.TABLE_NAME].Columns.Add(IV_StockTakingTable.STOCKUMID_FLD);
			dstGridData.Tables[IV_StockTakingTable.TABLE_NAME].Columns.Add(IV_StockTakingTable.COUNTINGMETHODID_FLD);
			dstGridData.Tables[IV_StockTakingTable.TABLE_NAME].Columns[IV_StockTakingTable.COUNTINGMETHODID_FLD].DefaultValue = 1;
			dstGridData.Tables[IV_StockTakingTable.TABLE_NAME].Columns.Add(IV_StockTakingMasterTable.STOCKTAKINGPERIODID_FLD);
			dstGridData.Tables[IV_StockTakingTable.TABLE_NAME].Columns.Add(IV_StockTakingTable.STOCKTAKINGID_FLD);
			dstGridData.Tables[IV_StockTakingTable.TABLE_NAME].Columns.Add(IV_StockTakingTable.STOCKTAKINGMASTERID_FLD);

			dstGridData.Tables[IV_StockTakingTable.TABLE_NAME].Columns.Add(IV_StockTakingTable.SLIPCODE_FLD);
			dstGridData.Tables[IV_StockTakingTable.TABLE_NAME].Columns.Add("StockTakingCode");
			dstGridData.Tables[IV_StockTakingTable.TABLE_NAME].Columns.Add(ITM_CategoryTable.TABLE_NAME + ITM_CategoryTable.CODE_FLD);
			dstGridData.Tables[IV_StockTakingTable.TABLE_NAME].Columns.Add(ITM_ProductTable.CODE_FLD);
			dstGridData.Tables[IV_StockTakingTable.TABLE_NAME].Columns.Add(ITM_ProductTable.DESCRIPTION_FLD);
			dstGridData.Tables[IV_StockTakingTable.TABLE_NAME].Columns.Add(ITM_ProductTable.REVISION_FLD);
			
			dstGridData.Tables[IV_StockTakingTable.TABLE_NAME].Columns.Add("ProductType");
			dstGridData.Tables[IV_StockTakingTable.TABLE_NAME].Columns.Add("Source");
			dstGridData.Tables[IV_StockTakingTable.TABLE_NAME].Columns.Add("Vendor");
			dstGridData.Tables[IV_StockTakingTable.TABLE_NAME].Columns.Add(MST_UnitOfMeasureTable.TABLE_NAME + MST_UnitOfMeasureTable.CODE_FLD);
			dstGridData.Tables[IV_StockTakingTable.TABLE_NAME].Columns.Add(IV_StockTakingTable.QUANTITY_FLD, typeof(Decimal));
			dstGridData.Tables[IV_StockTakingTable.TABLE_NAME].Columns.Add(IV_StockTakingTable.BOOKQUANTITY_FLD, typeof(Decimal));
			dstGridData.Tables[IV_StockTakingTable.TABLE_NAME].Columns.Add(IV_CoutingMethodTable.TABLE_NAME + IV_CoutingMethodTable.CODE_FLD);
		    var defaultCountMethod = InventoryUtilities.Instance.GetDefaultCountingMethod();
            if (defaultCountMethod != null)
            {
                dstGridData.Tables[IV_StockTakingTable.TABLE_NAME].Columns[IV_CoutingMethodTable.TABLE_NAME + IV_CoutingMethodTable.CODE_FLD].DefaultValue = defaultCountMethod.Code;
            }
			dstGridData.Tables[IV_StockTakingTable.TABLE_NAME].Columns.Add(IV_StockTakingTable.NOTE_FLD);
			
		}
		/// <summary>
		/// StockTaking_Load
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Monday, July 24 2006</date>
		private void StockTaking_Load(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".StockTaking_Load()";
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
				// Store grid layout
				dtbGridDesign = FormControlComponents.StoreGridLayout(dgrdData);
				CreateDataSet();
				
                dgrdData.DataSource = dstGridData.Tables[0];
				FormControlComponents.RestoreGridLayout(dgrdData, dtbGridDesign);
				ConfigGrid(true);
				frmStatus = EnumAction.Default;
				SwitchFormMode();
				btnEdit.Enabled = false;
				btnDelete.Enabled = false;

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
		/// fill data to all control by StockTakingPeriodID
		/// </summary>
		/// <param name="pintStockTakingPeriodID"></param>
		/// <author>Trada</author>
		/// <date>Monday, July 24 2006</date>
		private void FillDataToControl(int pintStockTakingPeriodID)
		{
			StockTakingBO boStockTaking = new StockTakingBO();
			//Get data from database by StockTakingPeriodID
			FillQuantityInToGrid();
			dstStockTaking = boStockTaking.GetDataFromStockTaking(pintStockTakingPeriodID);
			//Fill master data
			if (dstStockTaking.Tables[0].Rows.Count > 0)
			{
				if (dstStockTaking.Tables[0].Rows[0][IV_StockTakingPeriodTable.STOCKTAKINGDATE_FLD].ToString() != string.Empty)
				{
					dtmStockTakingDate.Value = (DateTime)dstStockTaking.Tables[0].Rows[0][IV_StockTakingPeriodTable.STOCKTAKINGDATE_FLD];
				}
				else
					dtmStockTakingDate.Value = null;
				dtmFromDate.Value = (DateTime)dstStockTaking.Tables[0].Rows[0][IV_StockTakingPeriodTable.FROMDATE_FLD];
				dtmToDate.Value = (DateTime)dstStockTaking.Tables[0].Rows[0][IV_StockTakingPeriodTable.TODATE_FLD];
				txtCCN.Text = dstStockTaking.Tables[0].Rows[0]["CCN"].ToString();
				txtCCN.Tag = dstStockTaking.Tables[0].Rows[0][MST_CCNTable.CCNID_FLD];
				txtStockTakingNo.Text = dstStockTaking.Tables[0].Rows[0]["Code"].ToString();
				txtStockTakingNo.Tag = dstStockTaking.Tables[0].Rows[0]["StockTakingMasterID"].ToString();
				txtStockTakingPeriod.Text = dstStockTaking.Tables[0].Rows[0]["Description"].ToString();
				txtStockTakingPeriod.Tag = dstStockTaking.Tables[0].Rows[0]["StockTakingPeriodID"];

				txtDepartment.Text = dstStockTaking.Tables[0].Rows[0]["Department"].ToString();
				txtDepartment.Tag = dstStockTaking.Tables[0].Rows[0]["DepartmentID"];
				txtProductionLine.Text = dstStockTaking.Tables[0].Rows[0]["ProductionLine"].ToString();
				txtProductionLine.Tag = dstStockTaking.Tables[0].Rows[0]["ProductionLineID"];
				txtLocation.Text = dstStockTaking.Tables[0].Rows[0]["Location"].ToString();
				txtLocation.Tag = dstStockTaking.Tables[0].Rows[0]["LocationID"];
				txtBin.Text = dstStockTaking.Tables[0].Rows[0]["Bin"].ToString();
				txtBin.Tag = dstStockTaking.Tables[0].Rows[0]["BinID"];

			}
			//fill detail
			dgrdData.DataSource = dstStockTaking.Tables[1];
			FormControlComponents.RestoreGridLayout(dgrdData, dtbGridDesign);
			ConfigGrid(false);
			SwitchFormMode();
		}
		/// <summary>
		/// btnStockTakingPeriod_Click
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Monday, July 24 2006</date>
		private void btnStockTakingPeriod_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnStockTakingPeriod_Click()";
			try
			{
				DataRowView drwResult = null;
				Hashtable condition = new Hashtable();
				condition.Add("Closed",0);
				drwResult = FormControlComponents.OpenSearchForm(IV_StockTakingPeriodTable.TABLE_NAME, IV_StockTakingPeriodTable.DESCRIPTION_FLD, txtStockTakingPeriod.Text.Trim(), condition, true);
				if (drwResult != null)
				{
					txtStockTakingPeriod.Tag = drwResult[IV_StockTakingPeriodTable.STOCKTAKINGPERIODID_FLD];
					txtStockTakingPeriod.Text = drwResult[IV_StockTakingPeriodTable.DESCRIPTION_FLD].ToString();
					dtmStockTakingDate.Value = drwResult[IV_StockTakingPeriodTable.STOCKTAKINGDATE_FLD];
					dtmFromDate.Value = drwResult[IV_StockTakingPeriodTable.FROMDATE_FLD];
					dtmToDate.Value = drwResult[IV_StockTakingPeriodTable.TODATE_FLD];
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
		/// txtStockTakingPeriod_Validating
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Monday, July 24 2006</date>
		private void txtStockTakingPeriod_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtStockTakingPeriod_Validating()";
			try
			{
				if (!txtStockTakingPeriod.Modified) return;

				if (txtStockTakingPeriod.Text.Trim() ==  string.Empty)
				{
					//clear all controls
					dtmFromDate.Value = null;
					dtmToDate.Value = null;
					dtmStockTakingDate.Value = null;
					txtCCN.Text = string.Empty;
					txtStockTakingPeriod.Tag = null;
					return;
				}
				DataRowView drwResult = null;
				Hashtable condition = new Hashtable();
				condition.Add("Closed",0);

				drwResult = FormControlComponents.OpenSearchForm(IV_StockTakingPeriodTable.TABLE_NAME, IV_StockTakingPeriodTable.DESCRIPTION_FLD, txtStockTakingPeriod.Text.Trim(), condition, false);
				if (drwResult != null)
				{
					txtStockTakingPeriod.Tag = drwResult[IV_StockTakingPeriodTable.STOCKTAKINGPERIODID_FLD];
					txtStockTakingPeriod.Text = drwResult[IV_StockTakingPeriodTable.DESCRIPTION_FLD].ToString();
					dtmStockTakingDate.Value = drwResult[IV_StockTakingPeriodTable.STOCKTAKINGDATE_FLD];
					dtmFromDate.Value = drwResult[IV_StockTakingPeriodTable.FROMDATE_FLD];
					dtmToDate.Value = drwResult[IV_StockTakingPeriodTable.TODATE_FLD];
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
		/// <summary>
		/// txtStockTakingPeriod_KeyDown
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Monday, July 24 2006</date>
		private void txtStockTakingPeriod_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if (btnSave.Enabled && e.KeyCode == Keys.F4)
			{
				btnStockTakingPeriod_Click(null, null);
			}
		}
		/// <summary>
		/// FillItemDataToGrid
		/// </summary>
		/// <param name="pdrowData"></param>
		/// <author>Trada</author>
		/// <date>Tuesday, July 25 2006</date>
		private void FillItemDataToGrid(DataRow pdrowData)
		{
			dgrdData.EditActive = true;
			dgrdData.Columns[ITM_ProductTable.REVISION_FLD].Value = pdrowData[ITM_ProductTable.REVISION_FLD];
			dgrdData.Columns[MST_UnitOfMeasureTable.TABLE_NAME + MST_UnitOfMeasureTable.CODE_FLD].Value  = pdrowData[MST_UnitOfMeasureTable.TABLE_NAME + MST_UnitOfMeasureTable.CODE_FLD];
			dgrdData.Columns[IV_StockTakingTable.STOCKUMID_FLD].Value  = pdrowData[ITM_ProductTable.STOCKUMID_FLD];
			dgrdData.Columns[ITM_CategoryTable.TABLE_NAME + ITM_CategoryTable.CODE_FLD].Value  = pdrowData[ITM_CategoryTable.TABLE_NAME + ITM_CategoryTable.CODE_FLD];
			dgrdData[dgrdData.Row, ITM_ProductTable.PRODUCTID_FLD] = int.Parse(pdrowData[ITM_ProductTable.PRODUCTID_FLD].ToString());
			dgrdData[dgrdData.Row, ITM_ProductTable.CODE_FLD] = pdrowData[ITM_ProductTable.CODE_FLD];
			dgrdData[dgrdData.Row, ITM_ProductTable.DESCRIPTION_FLD]	= pdrowData[ITM_ProductTable.DESCRIPTION_FLD];
			dgrdData[dgrdData.Row, "ProductType"]	= pdrowData["ProductType"];
			dgrdData[dgrdData.Row, "Source"]	= pdrowData["Source"];
			dgrdData[dgrdData.Row, "Vendor"]	= pdrowData["Vendor"];
			dgrdData[dgrdData.Row, "StockTakingCode"]	= pdrowData["StockTakingCode"];

			int intOHQty = 0;

			if(dtbOnHandQty == null) FillQuantityInToGrid();
			if(dtbOnHandQty != null)
			{
				DataRow[] rowOHs = dtbOnHandQty.Select("ProductID = " + pdrowData["ProductID"]);
				if(rowOHs.Length > 0)
					intOHQty = Convert.ToInt32(rowOHs[0]["OHQuantity"]);
			}
			dgrdData[dgrdData.Row, "BookQuantity"]	= intOHQty;
		}
		
		/// <summary>
		/// FillLocationByProductionLine
		/// </summary>
		/// <param name="pintLocationID"></param>
		/// <author>Trada, Tuesday, August 15 2006</author>
		private void FillLocationByProductionLine(int pintLocationID)
		{
			StockTakingBO boStockTaking = new StockTakingBO();
			dgrdData[dgrdData.Row, MST_LocationTable.TABLE_NAME + MST_LocationTable.CODE_FLD] = boStockTaking.FillLocation(pintLocationID);
		}
		/// <summary>
		/// dgrdData_ButtonClick
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Tuesday, July 25 2006</date>
		private void dgrdData_ButtonClick(object sender, C1.Win.C1TrueDBGrid.ColEventArgs e)
		{
			const string METHOD_NAME = THIS + ".dgrdData_ButtonClick()";
			try
			{
				DataRowView drwResult = null;
				Hashtable htbCondition= new Hashtable();
				String[] strParam = new string[2];
				
				if (!btnSave.Enabled) return;
				if (dgrdData.Col == dgrdData.Columns.IndexOf(dgrdData.Columns["StockTakingCode"]))
				{
					if (dgrdData.AddNewMode == AddNewModeEnum.AddNewCurrent)
					{
						drwResult = FormControlComponents.OpenSearchForm(V_PRODUCTFORSTOCKTAKING, "StockTakingCode", dgrdData[dgrdData.Row, "StockTakingCode"].ToString(), htbCondition, true);
					}
					else
					{
						drwResult = FormControlComponents.OpenSearchForm(V_PRODUCTFORSTOCKTAKING, "StockTakingCode", dgrdData.Columns["StockTakingCode"].Value.ToString().Trim(), htbCondition, true);
					}
					if (drwResult != null)
					{
						dgrdData.EditActive = true;
						FillItemDataToGrid(drwResult.Row);
					}
				}

				//Select Item
				if (dgrdData.Col == dgrdData.Columns.IndexOf(dgrdData.Columns[ITM_ProductTable.CODE_FLD])
					|| dgrdData.Col == dgrdData.Columns.IndexOf(dgrdData.Columns[ITM_ProductTable.DESCRIPTION_FLD]))
				{
					return;
				}
				//Select Counting Method
				if (dgrdData.Col == dgrdData.Columns.IndexOf(dgrdData.Columns[IV_CoutingMethodTable.TABLE_NAME + IV_CoutingMethodTable.CODE_FLD]))
				{
					if (dgrdData.AddNewMode == AddNewModeEnum.AddNewCurrent)
					{
						drwResult = FormControlComponents.OpenSearchForm(IV_CoutingMethodTable.TABLE_NAME, IV_CoutingMethodTable.CODE_FLD, dgrdData[dgrdData.Row, IV_CoutingMethodTable.TABLE_NAME + IV_CoutingMethodTable.CODE_FLD].ToString(), htbCondition, true);
					}
					else
					{
						drwResult = FormControlComponents.OpenSearchForm(IV_CoutingMethodTable.TABLE_NAME, IV_CoutingMethodTable.CODE_FLD, dgrdData.Columns[IV_CoutingMethodTable.TABLE_NAME + IV_CoutingMethodTable.CODE_FLD].Value.ToString().Trim(), htbCondition, true);
					}
					if (drwResult != null)
					{
						dgrdData.EditActive = true;
						dgrdData.Columns[IV_CoutingMethodTable.TABLE_NAME + IV_CoutingMethodTable.CODE_FLD].Value = drwResult[IV_CoutingMethodTable.CODE_FLD].ToString();
						dgrdData.Columns[IV_StockTakingTable.COUNTINGMETHODID_FLD].Value = int.Parse(drwResult[IV_CoutingMethodTable.COUNTINGMETHODID_FLD].ToString());
					}
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
		/// <summary>
		/// dgrdData_BeforeColUpdate
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Tuesday, July 25 2006</date>
		private void dgrdData_BeforeColUpdate(object sender, C1.Win.C1TrueDBGrid.BeforeColUpdateEventArgs e)
		{
			const string METHOD_NAME = THIS + ".dgrdData_BeforeColUpdate()";
			try
			{
				Hashtable htbCondition = new Hashtable();
				string strColumnName = e.Column.DataColumn.DataField;
				string[] strParam = new string[2];
                DataRowView drwResult = null;
				switch (strColumnName)
				{
					case "StockTakingCode":
						# region StockTakingCode
						if (dgrdData.Columns[e.Column.DataColumn.DataField].Text.Trim() != string.Empty)
						{
							drwResult = FormControlComponents.OpenSearchForm(V_PRODUCTFORSTOCKTAKING, "StockTakingCode", dgrdData.Columns[e.Column.DataColumn.DataField].Value.ToString().Trim(), null, false);
							if (drwResult != null)
							{
								e.Column.DataColumn.Tag = drwResult.Row;	
							}
							else
							{
								
								e.Cancel = true;
							}
						}
						else
						{
							// if StockTakingCode = null -> BookQuantity = 0
							dgrdData[dgrdData.Row, IV_StockTakingTable.BOOKQUANTITY_FLD] = 0;
						}
						
						#endregion
						break;
					case ITM_ProductTable.CODE_FLD:
						# region Open ComNumber search form
						if (dgrdData.Columns[e.Column.DataColumn.DataField].Text.Trim() != string.Empty)
						{
							drwResult = FormControlComponents.OpenSearchForm(V_PRODUCTFORSTOCKTAKING, ITM_ProductTable.CODE_FLD, dgrdData.Columns[e.Column.DataColumn.DataField].Value.ToString().Trim(), null, false);
							if (drwResult != null)
							{
								e.Column.DataColumn.Tag = drwResult.Row;	
							}
							else
							{
								e.Cancel = true;
							}
						}
						#endregion
						break;
					case ITM_ProductTable.DESCRIPTION_FLD:
						# region Open ComName search form
						if (dgrdData.Columns[e.Column.DataColumn.DataField].Text.Trim() != string.Empty)
						{
							drwResult = FormControlComponents.OpenSearchForm(V_PRODUCTFORSTOCKTAKING, ITM_ProductTable.DESCRIPTION_FLD, dgrdData.Columns[e.Column.DataColumn.DataField].Value.ToString().Trim(), null, false);
							if (drwResult != null)
							{
								e.Column.DataColumn.Tag = drwResult.Row;	
							}
							else
							{
								e.Cancel = true;
							}
						}
						#endregion
						break;
					case MST_DepartmentTable.TABLE_NAME + MST_DepartmentTable.CODE_FLD:
						# region Open Department search form
						if (dgrdData.Columns[e.Column.DataColumn.DataField].Text.Trim() != string.Empty)
						{
							drwResult = FormControlComponents.OpenSearchForm(MST_DepartmentTable.TABLE_NAME, MST_DepartmentTable.CODE_FLD, dgrdData.Columns[e.Column.DataColumn.DataField].Value.ToString().Trim(), null, false);
							if (drwResult != null)
							{
								e.Column.DataColumn.Tag = drwResult.Row;	
							}
							else
							{
								e.Cancel = true;
							}
						}
						#endregion
						break;
					case PRO_ProductionLineTable.TABLE_NAME + PRO_ProductionLineTable.CODE_FLD:
						# region Open ProductionLine search form
						if (dgrdData.Columns[e.Column.DataColumn.DataField].Text.Trim() != string.Empty)
						{
							if (dgrdData[dgrdData.Row, MST_DepartmentTable.DEPARTMENTID_FLD].ToString() == string.Empty)
							{
								strParam[0] = dgrdData.Columns[MST_DepartmentTable.TABLE_NAME + MST_DepartmentTable.CODE_FLD].Caption;
								strParam[1] = dgrdData.Columns[PRO_ProductionLineTable.TABLE_NAME + PRO_ProductionLineTable.CODE_FLD].Caption;
								PCSMessageBox.Show(ErrorCode.MESSAGE_SELECT_ONE_BEFORE_SELECT_ONE, MessageBoxIcon.Warning, strParam);
								dgrdData.Col = dgrdData.Splits[0].DisplayColumns.IndexOf(dgrdData.Splits[0].DisplayColumns[MST_DepartmentTable.TABLE_NAME + MST_DepartmentTable.CODE_FLD]);
								dgrdData.Focus();
								return;
							}
							else
							{
								htbCondition.Add(PRO_ProductionLineTable.DEPARTMENTID_FLD, dgrdData[dgrdData.Row, MST_DepartmentTable.DEPARTMENTID_FLD]);
							}
							drwResult = FormControlComponents.OpenSearchForm(PRO_ProductionLineTable.TABLE_NAME, PRO_ProductionLineTable.CODE_FLD, dgrdData.Columns[e.Column.DataColumn.DataField].Value.ToString().Trim(), htbCondition, false);
							if (drwResult != null)
							{
								e.Column.DataColumn.Tag = drwResult.Row;	
							}
							else
							{
								e.Cancel = true;
							}
						}
						#endregion
						break;
					case MST_LocationTable.TABLE_NAME + MST_LocationTable.CODE_FLD:
						# region Open Location search form
						if (dgrdData.Columns[e.Column.DataColumn.DataField].Text.Trim() != string.Empty)
						{
							if (dgrdData[dgrdData.Row, PRO_ProductionLineTable.PRODUCTIONLINEID_FLD].ToString() == string.Empty)
							{
								strParam[0] = dgrdData.Columns[PRO_ProductionLineTable.TABLE_NAME + PRO_ProductionLineTable.CODE_FLD].Caption;
								strParam[1] = dgrdData.Columns[MST_LocationTable.TABLE_NAME + MST_LocationTable.CODE_FLD].Caption;
								PCSMessageBox.Show(ErrorCode.MESSAGE_SELECT_ONE_BEFORE_SELECT_ONE, MessageBoxIcon.Warning, strParam);
								dgrdData.Col = dgrdData.Splits[0].DisplayColumns.IndexOf(dgrdData.Splits[0].DisplayColumns[PRO_ProductionLineTable.TABLE_NAME + PRO_ProductionLineTable.CODE_FLD]);
								dgrdData.Focus();
								return;
							}
							else
							{
								htbCondition.Add(PRO_ProductionLineTable.PRODUCTIONLINEID_FLD, dgrdData[dgrdData.Row, PRO_ProductionLineTable.PRODUCTIONLINEID_FLD]);
							}
							drwResult = FormControlComponents.OpenSearchForm(V_LOCATIONANDPRODUCTIONLINE, MST_LocationTable.CODE_FLD, dgrdData.Columns[e.Column.DataColumn.DataField].Value.ToString().Trim(), htbCondition, false);
							if (drwResult != null)
							{
								e.Column.DataColumn.Tag = drwResult.Row;	
							}
							else
							{
								e.Cancel = true;
							}
						}
						#endregion
						break;
					case MST_BINTable.TABLE_NAME + MST_BINTable.CODE_FLD:
						# region Open Bin search form
						if (dgrdData.Columns[e.Column.DataColumn.DataField].Text.Trim() != string.Empty)
						{
							if (dgrdData[dgrdData.Row, MST_LocationTable.TABLE_NAME + MST_LocationTable.CODE_FLD].ToString() == string.Empty)
							{
								PCSMessageBox.Show(ErrorCode.MESSAGE_MUST_SELECT_LOCATION, MessageBoxIcon.Warning);
								dgrdData.Col = dgrdData.Splits[0].DisplayColumns.IndexOf(dgrdData.Splits[0].DisplayColumns[MST_LocationTable.TABLE_NAME + MST_LocationTable.CODE_FLD]);
								dgrdData.Focus();
								return;
							}
							else
							{
								htbCondition.Add(MST_BINTable.LOCATIONID_FLD, int.Parse(dgrdData[dgrdData.Row, MST_LocationTable.LOCATIONID_FLD].ToString()));
							}
							drwResult = FormControlComponents.OpenSearchForm(MST_BINTable.TABLE_NAME, MST_BINTable.CODE_FLD, dgrdData.Columns[e.Column.DataColumn.DataField].Value.ToString().Trim(), htbCondition, false);
							if (drwResult != null)
							{
								e.Column.DataColumn.Tag = drwResult.Row;	
							}
							else
							{
								e.Cancel = true;
							}
						}
						#endregion
						break;
					case IV_CoutingMethodTable.TABLE_NAME + IV_CoutingMethodTable.CODE_FLD:
						# region Open Counting Method search form
						if (dgrdData.Columns[e.Column.DataColumn.DataField].Text.Trim() != string.Empty)
						{
							drwResult = FormControlComponents.OpenSearchForm(IV_CoutingMethodTable.TABLE_NAME, IV_CoutingMethodTable.CODE_FLD, dgrdData.Columns[e.Column.DataColumn.DataField].Value.ToString().Trim(), null, false);
							if (drwResult != null)
							{
								e.Column.DataColumn.Tag = drwResult.Row;	
							}
							else
							{
								e.Cancel = true;
							}
						}
						#endregion
						break;
					case IV_StockTakingTable.QUANTITY_FLD:
						if (e.Column.DataColumn.Text == string.Empty)
						{
							return;
						}
						break;
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
		/// <summary>
		/// dgrdData_AfterColUpdate
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Tuesday, July 25 2006</date>
		private void dgrdData_AfterColUpdate(object sender, C1.Win.C1TrueDBGrid.ColEventArgs e)
		{
			const string METHOD_NAME = THIS + ".dgrdData_AfterColUpdate()";
			try
			{
				DataRow drwResult = (DataRow) e.Column.DataColumn.Tag;
				//Fill Data to ComNumber
				if (e.Column.DataColumn.DataField == "StockTakingCode")
				{
					if ((e.Column.DataColumn.Tag == null) ||(dgrdData[dgrdData.Row, "StockTakingCode"].ToString() == string.Empty))
					{
						dgrdData.Columns["StockTakingCode"].Value = string.Empty;
						dgrdData.Columns[ITM_ProductTable.CODE_FLD].Value = string.Empty;
						dgrdData.Columns[ITM_ProductTable.DESCRIPTION_FLD].Value = string.Empty;
						dgrdData.Columns[ITM_ProductTable.REVISION_FLD].Value = string.Empty;
						dgrdData.Columns[ITM_ProductTable.PRODUCTID_FLD].Value = null;
						dgrdData.Columns[MST_UnitOfMeasureTable.TABLE_NAME + MST_UnitOfMeasureTable.CODE_FLD].Value = string.Empty;
						dgrdData.Columns[ITM_CategoryTable.TABLE_NAME + ITM_CategoryTable.CODE_FLD].Value = string.Empty;
						dgrdData.Columns["Vendor"].Value = string.Empty;
						dgrdData.Columns["ProductType"].Value = string.Empty;
						dgrdData.Columns["Source"].Value = string.Empty;
					}
					else
					{
						dgrdData.EditActive = true;
						FillItemDataToGrid(drwResult);
					}
				}
				//Fill Data to ComName
				if(e.Column.DataColumn.DataField == ITM_ProductTable.DESCRIPTION_FLD)
				{
					if ((e.Column.DataColumn.Tag == null) ||(dgrdData[dgrdData.Row, ITM_ProductTable.DESCRIPTION_FLD].ToString() == string.Empty))
					{
						dgrdData.Columns[ITM_ProductTable.CODE_FLD].Value = string.Empty;
						dgrdData.Columns[ITM_ProductTable.DESCRIPTION_FLD].Value = string.Empty;
						dgrdData.Columns[ITM_ProductTable.REVISION_FLD].Value	 = string.Empty;
						dgrdData.Columns[ITM_ProductTable.PRODUCTID_FLD].Value = null;
						dgrdData.Columns[MST_UnitOfMeasureTable.TABLE_NAME + MST_UnitOfMeasureTable.CODE_FLD].Value = string.Empty;
						dgrdData.Columns[ITM_CategoryTable.TABLE_NAME + ITM_CategoryTable.CODE_FLD].Value = string.Empty;
					}
					else
					{
						dgrdData.EditActive = true;
						FillItemDataToGrid(drwResult);
					}
				}
				//Fill Data to Department
				if(e.Column.DataColumn.DataField == MST_DepartmentTable.TABLE_NAME + MST_DepartmentTable.CODE_FLD)
				{
					if ((e.Column.DataColumn.Tag == null) ||(dgrdData[dgrdData.Row, MST_DepartmentTable.TABLE_NAME + MST_DepartmentTable.CODE_FLD].ToString() == string.Empty))
					{
						dgrdData[dgrdData.Row, MST_DepartmentTable.TABLE_NAME + MST_DepartmentTable.CODE_FLD] = string.Empty;
						dgrdData[dgrdData.Row, IV_StockTakingMasterTable.DEPARTMENTID_FLD] = string.Empty;
						//clear relate information
						dgrdData[dgrdData.Row, PRO_ProductionLineTable.TABLE_NAME + PRO_ProductionLineTable.CODE_FLD] = string.Empty;
						dgrdData[dgrdData.Row, PRO_ProductionLineTable.PRODUCTIONLINEID_FLD] = string.Empty;
						dgrdData[dgrdData.Row, MST_LocationTable.TABLE_NAME + MST_LocationTable.CODE_FLD] = string.Empty;
						dgrdData[dgrdData.Row, MST_LocationTable.LOCATIONID_FLD] = string.Empty;
						dgrdData[dgrdData.Row, MST_BINTable.TABLE_NAME + MST_BINTable.CODE_FLD] = string.Empty;
						dgrdData[dgrdData.Row, MST_BINTable.BINID_FLD] = string.Empty;
					}
					else
					{
						dgrdData.EditActive = true;
						dgrdData[dgrdData.Row, IV_StockTakingMasterTable.DEPARTMENTID_FLD] = int.Parse(drwResult[MST_DepartmentTable.DEPARTMENTID_FLD].ToString());
						dgrdData[dgrdData.Row, MST_DepartmentTable.TABLE_NAME + MST_DepartmentTable.CODE_FLD] = drwResult[MST_DepartmentTable.CODE_FLD];
						
					}
				}
				//Fill Data to ProductionLine
				if(e.Column.DataColumn.DataField == PRO_ProductionLineTable.TABLE_NAME + PRO_ProductionLineTable.CODE_FLD)
				{
					if ((e.Column.DataColumn.Tag == null) ||(dgrdData[dgrdData.Row, PRO_ProductionLineTable.TABLE_NAME + PRO_ProductionLineTable.CODE_FLD].ToString() == string.Empty))
					{
						dgrdData.Columns[PRO_ProductionLineTable.TABLE_NAME + PRO_ProductionLineTable.CODE_FLD].Value = string.Empty;
						dgrdData.Columns[IV_StockTakingMasterTable.PRODUCTIONLINEID_FLD].Value = null;
						dgrdData[dgrdData.Row, MST_LocationTable.TABLE_NAME + MST_LocationTable.CODE_FLD] = string.Empty;
						dgrdData[dgrdData.Row, MST_LocationTable.LOCATIONID_FLD] = string.Empty;
						dgrdData[dgrdData.Row, MST_BINTable.TABLE_NAME + MST_BINTable.CODE_FLD] = string.Empty;
						dgrdData[dgrdData.Row, MST_BINTable.BINID_FLD] = string.Empty;
					}
					else
					{
						dgrdData.EditActive = true;
						dgrdData[dgrdData.Row, IV_StockTakingMasterTable.PRODUCTIONLINEID_FLD] = int.Parse(drwResult[PRO_ProductionLineTable.PRODUCTIONLINEID_FLD].ToString());
						dgrdData[dgrdData.Row, PRO_ProductionLineTable.TABLE_NAME + PRO_ProductionLineTable.CODE_FLD] = drwResult[PRO_ProductionLineTable.CODE_FLD];
						//fill Location
						FillLocationByProductionLine(int.Parse(drwResult[PRO_ProductionLineTable.LOCATIONID_FLD].ToString()));
						dgrdData[dgrdData.Row, MST_LocationTable.LOCATIONID_FLD] = int.Parse(drwResult[PRO_ProductionLineTable.LOCATIONID_FLD].ToString());
					}
				}
				//Fill Data to Location
				if(e.Column.DataColumn.DataField == MST_LocationTable.TABLE_NAME + MST_LocationTable.CODE_FLD)
				{
					if ((dgrdData[dgrdData.Row, MST_LocationTable.TABLE_NAME + MST_LocationTable.CODE_FLD].ToString() == string.Empty))
					{
						dgrdData[dgrdData.Row, IV_StockTakingMasterTable.LOCATIONID_FLD] = null;
						//clear bin
						dgrdData[dgrdData.Row, MST_BINTable.TABLE_NAME + MST_BINTable.CODE_FLD] = string.Empty;
						dgrdData[dgrdData.Row, IV_StockTakingMasterTable.BINID_FLD] = string.Empty;
					}
					else
					{
						dgrdData.EditActive = true;
						if (dgrdData[dgrdData.Row, IV_StockTakingMasterTable.LOCATIONID_FLD].ToString() != string.Empty)
						{
							if (int.Parse(dgrdData[dgrdData.Row, IV_StockTakingMasterTable.LOCATIONID_FLD].ToString()) != int.Parse(drwResult[MST_LocationTable.LOCATIONID_FLD].ToString()))
							{
								//clear bin
								dgrdData[dgrdData.Row, MST_BINTable.TABLE_NAME + MST_BINTable.CODE_FLD] = string.Empty;
								dgrdData[dgrdData.Row, IV_StockTakingMasterTable.BINID_FLD] = string.Empty;
							}
						}
						dgrdData.Columns[MST_LocationTable.TABLE_NAME + MST_LocationTable.CODE_FLD].Value = drwResult[MST_LocationTable.CODE_FLD].ToString();
						dgrdData.Columns[IV_StockTakingMasterTable.LOCATIONID_FLD].Value = drwResult[MST_LocationTable.LOCATIONID_FLD];
					}
				}
				//Fill Data to Bin
				if(e.Column.DataColumn.DataField == MST_BINTable.TABLE_NAME + MST_BINTable.CODE_FLD)
				{
					if ((e.Column.DataColumn.Tag == null) ||(dgrdData[dgrdData.Row, MST_BINTable.TABLE_NAME + MST_BINTable.CODE_FLD].ToString() == string.Empty))
					{
						dgrdData.Columns[MST_BINTable.TABLE_NAME + MST_BINTable.CODE_FLD].Value = string.Empty;
						dgrdData.Columns[IV_StockTakingMasterTable.BINID_FLD].Value = null;
					}
					else
					{
						dgrdData.EditActive = true;
						dgrdData.Columns[IV_StockTakingMasterTable.BINID_FLD].Value = int.Parse(drwResult[MST_BINTable.BINID_FLD].ToString());
						dgrdData.Columns[MST_BINTable.TABLE_NAME + MST_BINTable.CODE_FLD].Value = drwResult[MST_BINTable.CODE_FLD];
					}
				}
				//Fill Data to Counting Method
				if(e.Column.DataColumn.DataField == IV_CoutingMethodTable.TABLE_NAME + IV_CoutingMethodTable.CODE_FLD)
				{
					if ((e.Column.DataColumn.Tag == null) ||(dgrdData[dgrdData.Row, IV_CoutingMethodTable.TABLE_NAME + IV_CoutingMethodTable.CODE_FLD].ToString() == string.Empty))
					{
						dgrdData.Columns[IV_CoutingMethodTable.TABLE_NAME + IV_CoutingMethodTable.CODE_FLD].Value = string.Empty;
						dgrdData.Columns[IV_StockTakingTable.COUNTINGMETHODID_FLD].Value = null;
					}
					else
					{
						dgrdData.EditActive = true;
						dgrdData[dgrdData.Row, IV_StockTakingTable.COUNTINGMETHODID_FLD] = int.Parse(drwResult[IV_CoutingMethodTable.COUNTINGMETHODID_FLD].ToString());
						dgrdData[dgrdData.Row, IV_CoutingMethodTable.TABLE_NAME + IV_CoutingMethodTable.CODE_FLD] = drwResult[IV_CoutingMethodTable.CODE_FLD];
						
					}
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
		/// <summary>
		/// dgrdData_KeyDown
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Tuesday, July 25 2006</date>
		private void dgrdData_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			const string METHOD_NAME = THIS + ".dgrdData_KeyDown()";
			try
			{
				if ((e.KeyCode == Keys.F4 && dgrdData.Splits[0].DisplayColumns["StockTakingCode"].Button)
					||(e.KeyCode == Keys.F4 && dgrdData.Splits[0].DisplayColumns["IV_CoutingMethodCode"].Button))
				{
					dgrdData_ButtonClick(null, null);
				}
				if (e.KeyCode == Keys.Delete && dgrdData.RowCount > 0 
					&& btnSave.Enabled)
				{
					FormControlComponents.DeleteMultiRowsOnTrueDBGrid(dgrdData);
					int intCount = 0;
					if (blnAddNewDataSet)
					{
					    intCount = dstGridData.Tables[0].Rows.Cast<DataRow>().Count(objRow => objRow.RowState != DataRowState.Deleted);
					}
				}
				if (e.KeyCode == Keys.F12 && btnSave.Enabled)
				{
					dgrdData.AllowAddNew = true;
					dgrdData.Row = dgrdData.RowCount;
					dgrdData.Col = dgrdData.Splits[0].DisplayColumns.IndexOf(dgrdData.Splits[0].DisplayColumns["SlipCode"]);
					dgrdData.Focus();
					return;

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
		/// <summary>
		/// Validating data before saving
		/// </summary>
		/// <returns></returns>
		/// <author>Trada</author>
		/// <date>Tuesday, July 25 2006</date>
		private bool ValidateData()
		{
			if (FormControlComponents.CheckMandatory(txtStockTakingNo))
			{
				PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Warning);
				txtStockTakingNo.Focus();
				txtStockTakingNo.Select();
				return false;
			}
			if (FormControlComponents.CheckMandatory(txtStockTakingPeriod))
			{
				PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Warning);
				txtStockTakingPeriod.Focus();
				txtStockTakingPeriod.Select();
				return false;
			}
			if (FormControlComponents.CheckMandatory(dtmStockTakingDate))
			{
				PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Warning);
				dtmStockTakingDate.Focus();
				dtmStockTakingDate.Select();
				return false;
			}
			
			if ((DateTime)dtmStockTakingDate.Value > (DateTime)dtmToDate.Value 
				|| (DateTime)dtmStockTakingDate.Value < (DateTime)dtmFromDate.Value)
			{
				PCSMessageBox.Show(ErrorCode.MESSAGE_STOCK_TAKING_BETWEEN_IN,MessageBoxIcon.Warning,new string[]{((DateTime)dtmFromDate.Value).ToString(Constants.DATETIME_FORMAT_HOUR),((DateTime)dtmToDate.Value).ToString(Constants.DATETIME_FORMAT_HOUR)});
				dtmStockTakingDate.Focus();
				return false;
			}

			if (FormControlComponents.CheckMandatory(txtStockTakingNo))
			{
				PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Warning);
				txtStockTakingNo.Focus();
				txtStockTakingNo.Select();
				return false;
			}
			if (FormControlComponents.CheckMandatory(txtDepartment))
			{
				PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Warning);
				txtDepartment.Focus();
				txtDepartment.Select();
				return false;
			}
			if (FormControlComponents.CheckMandatory(txtProductionLine))
			{
				PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Warning);
				txtProductionLine.Focus();
				txtProductionLine.Select();
				return false;
			}
			if (FormControlComponents.CheckMandatory(txtLocation))
			{
				PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Warning);
				txtLocation.Focus();
				txtLocation.Select();
				return false;
			}
			if (FormControlComponents.CheckMandatory(txtBin))
			{
				PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Warning);
				txtBin.Focus();
				txtBin.Select();
				return false;
			}

			//Check data in the grid
			if (dgrdData.RowCount == 0)
			{
				PCSMessageBox.Show(ErrorCode.MESSAGE_PLEASE_ENTER_DETAIL_INFOR, MessageBoxIcon.Warning);
				dgrdData.Focus();
				return false;
			}
			for (int i = 0; i < dgrdData.RowCount; i++)
			{
				if (dgrdData[i, "SlipCode"].ToString() == string.Empty)
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Warning);
					dgrdData.Row = i;
					dgrdData.Col = dgrdData.Splits[0].DisplayColumns.IndexOf(dgrdData.Splits[0].DisplayColumns["SlipCode"]);
					dgrdData.Focus();
					return false;
				}
				if (dgrdData[i, ITM_ProductTable.CODE_FLD].ToString() == string.Empty)
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Warning);
					dgrdData.Row = i;
					dgrdData.Col = dgrdData.Splits[0].DisplayColumns.IndexOf(dgrdData.Splits[0].DisplayColumns[ITM_ProductTable.CODE_FLD]);
					dgrdData.Focus();
					return false;
				}
				if (dgrdData[i, IV_StockTakingTable.QUANTITY_FLD].ToString() == string.Empty)
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Warning);
					dgrdData.Row = i;
					dgrdData.Col = dgrdData.Splits[0].DisplayColumns.IndexOf(dgrdData.Splits[0].DisplayColumns[IV_StockTakingTable.QUANTITY_FLD]);
					dgrdData.Focus();
					return false;
				}
				
				if (dgrdData[i, "IV_CoutingMethodCode"].ToString() == string.Empty)
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Warning);
					dgrdData.Row = i;
					dgrdData.Col = dgrdData.Splits[0].DisplayColumns.IndexOf(dgrdData.Splits[0].DisplayColumns["IV_CoutingMethodCode"]);
					dgrdData.Focus();
					return false;
				}
				
				for (int j = i + 1; j < dgrdData.RowCount; j++)
				{
					if (dgrdData[i, IV_StockTakingTable.PRODUCTID_FLD].ToString() != string.Empty
						&& dgrdData[j, IV_StockTakingTable.PRODUCTID_FLD].ToString() != string.Empty
						)
					{
						if (int.Parse(dgrdData[i, IV_StockTakingTable.PRODUCTID_FLD].ToString()) == int.Parse(dgrdData[j, IV_StockTakingTable.PRODUCTID_FLD].ToString())
							&& (dgrdData[i, "SlipCode"].ToString() == dgrdData[j, "SlipCode"].ToString())
							)
						{
							PCSMessageBox.Show(ErrorCode.DUPLICATE_KEY, MessageBoxIcon.Warning);
							dgrdData.Row = j;
							dgrdData.Col = dgrdData.Splits[0].DisplayColumns.IndexOf(dgrdData.Splits[0].DisplayColumns["SlipCode"]);
							dgrdData.Focus();
							return false;
						}
					}
				}
				
			}
			return true;
		}
		/// <summary>
		/// btnSave_Click
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Tuesday, July 25 2006</date>
		private void btnSave_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnSave_Click()";
			try
			{
				blnHasError = true;
				if (!dgrdData.EditActive && ValidateData())
				{
					dstTemp = dstStockTaking.Copy();
					IV_StockTakingMasterVO voMaster = new IV_StockTakingMasterVO();
					voMaster.BinID = Convert.ToInt32(txtBin.Tag); 
					voMaster.LocationID = Convert.ToInt32(txtLocation.Tag);
					voMaster.ProductionLineID = Convert.ToInt32(txtProductionLine.Tag);
					voMaster.DepartmentID = Convert.ToInt32(txtDepartment.Tag);
					voMaster.StockTakingPeriodID = Convert.ToInt32(txtStockTakingPeriod.Tag);
					voMaster.Code = txtStockTakingNo.Text;
					voMaster.StockTakingDate = (DateTime)dtmStockTakingDate.Value;

					StockTakingBO boStockTaking = new StockTakingBO();
					if (frmStatus == EnumAction.Add) //dstGridData
					{
						dstTemp = dstGridData.Copy();
						voMaster.StockTakingMasterID = boStockTaking.AddNewStockTaking(voMaster, dstGridData);
						txtStockTakingNo.Tag = voMaster.StockTakingMasterID;
					}
					else if (frmStatus == EnumAction.Edit) //dstStockTaking
					{
						//Set name for dstStockTaking.Tables[1]
						voMaster.StockTakingMasterID = Convert.ToInt32(txtStockTakingNo.Tag);
						boStockTaking.UpdateStockTaking(voMaster, dstStockTaking);
					}
					frmStatus = EnumAction.Default;
					//re-load data to grid
					FillDataToControl(int.Parse(txtStockTakingNo.Tag.ToString()));
					PCSMessageBox.Show(ErrorCode.MESSAGE_AFTER_SAVE_DATA);

					blnHasError = false;
					
					SwitchFormMode();
					
				}
			}
			catch (PCSException ex)
			{
				if (ex.mCode == ErrorCode.DUPLICATE_KEY)
				{
					try
					{
						PCSMessageBox.Show(ErrorCode.DUPLICATE_KEY, MessageBoxIcon.Error);
						if(dstStockTaking.Tables.Count > 1)
						{
							dstStockTaking = dstTemp.Copy();
						}
						else
						{
							dstStockTaking = dstTemp.Clone();
							foreach(DataRow row in dstTemp.Tables[0].Rows)
							{
								DataRow rowNew = dstStockTaking.Tables[0].NewRow();
								rowNew.ItemArray = row.ItemArray;
                                dstStockTaking.Tables[0].Rows.Add(rowNew);
							}
						} 
						//dgrdData.Refresh();
						dgrdData.Col = dgrdData.Columns.IndexOf(dgrdData.Columns["SlipCode"]);
						if(dstStockTaking.Tables.Count > 1)
							dgrdData.DataSource = dstStockTaking.Tables[1];
						else
						{
							dgrdData.DataSource = dstStockTaking.Tables[0];
							dstGridData = dstStockTaking;
						}
						FormControlComponents.RestoreGridLayout(dgrdData, dtbGridDesign);
						txtStockTakingNo.Focus();
						return;
					}
					catch
					{
						PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION);
					}
				}
				else
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
		/// <summary>
		/// StockTaking_Closing
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Wednesday, July 26 2006</date>
		private void StockTaking_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".StockTaking_Closing()";
			try
			{
				if (((DataTable)dgrdData.DataSource).DataSet.HasChanges())
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
		/// <summary>
		/// StockTaking_KeyDown
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Wednesday, July 26 2006</date>
		private void StockTaking_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			const string METHOD_NAME = THIS + ".StockTaking_KeyDown()";
			try
			{
				if (e.KeyCode == Keys.F12)
				{
					if (!dgrdData.Splits[0].DisplayColumns[ITM_ProductTable.CODE_FLD].Locked)
					{
						dgrdData.Row = dgrdData.RowCount;
						dgrdData.Col = dgrdData.Columns.IndexOf(dgrdData.Columns[MST_DepartmentTable.TABLE_NAME + MST_DepartmentTable.CODE_FLD]);
						dgrdData.Focus();
						dgrdData.EditActive = false;
					}
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
		/// <summary>
		/// dtmStockTakingDate_Validating
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Friday, August 18 2006</date>
		private void dtmStockTakingDate_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".dtmStockTakingDate_Validating()";
			try
			{
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

		private void btnDepartment_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnDepartment_Click()";
			try
			{
				if(txtStockTakingPeriod.Text.Trim() == string.Empty)
				{
					string[] strParam = new string[2];
					strParam[0] = lblStockTakingPeriod.Text;
					strParam[1] = lblDepartment.Text;
					PCSMessageBox.Show(ErrorCode.MESSAGE_SELECT_ONE_BEFORE_SELECT_ONE, MessageBoxIcon.Warning, strParam);
					txtStockTakingPeriod.Focus();
					return;
				}

				DataRowView drwResult = null;
				drwResult = FormControlComponents.OpenSearchForm(MST_DepartmentTable.TABLE_NAME, MST_DepartmentTable.CODE_FLD, txtDepartment.Text.Trim(), null, true);
				if (drwResult != null)
				{
					txtDepartment.Tag = drwResult[MST_DepartmentTable.DEPARTMENTID_FLD];
					txtDepartment.Text = drwResult[MST_DepartmentTable.CODE_FLD].ToString();
					//Fill data to all controls
					txtProductionLine.Tag = string.Empty;
					txtProductionLine.Text = null;
					txtLocation.Tag = string.Empty;
					txtLocation.Text = null;
					txtBin.Text = string.Empty;
					txtBin.Tag = null;

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

		private void txtDepartment_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if (btnSave.Enabled && e.KeyCode == Keys.F4)
			{
				btnDepartment_Click(null, null);
			}
		}

		private void txtDepartment_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".()";
			try
			{
				if(!txtDepartment.Modified) return;
				if(txtDepartment.Text.Trim() == "")
				{
					txtProductionLine.Text = string.Empty;
					txtProductionLine.Tag = null;
					txtLocation.Tag = null;
					txtLocation.Text = string.Empty;
					txtBin.Text = string.Empty;
					txtBin.Tag = null;
					return;
				}

				if(txtStockTakingPeriod.Text.Trim() == string.Empty)
				{
					txtDepartment.Clear();
					e.Cancel = true;
					string[] strParam = new string[2];
					strParam[0] = lblStockTakingPeriod.Text;
					strParam[1] = lblDepartment.Text;
					PCSMessageBox.Show(ErrorCode.MESSAGE_SELECT_ONE_BEFORE_SELECT_ONE, MessageBoxIcon.Warning, strParam);
					txtStockTakingPeriod.Focus();
					return;
				}
				Hashtable  htbCondition = new Hashtable();
				htbCondition.Add(IV_StockTakingPeriodTable.STOCKTAKINGPERIODID_FLD, txtStockTakingPeriod.Tag);

				DataRowView drwResult = null;
				drwResult = FormControlComponents.OpenSearchForm(MST_DepartmentTable.TABLE_NAME, MST_DepartmentTable.CODE_FLD, txtDepartment.Text.Trim(), null, false);
				if (drwResult != null)
				{
					txtDepartment.Tag = drwResult[MST_DepartmentTable.DEPARTMENTID_FLD];
					txtDepartment.Text = drwResult[MST_DepartmentTable.CODE_FLD].ToString();
					//Fill data to all controls
					txtProductionLine.Tag = string.Empty;
					txtProductionLine.Text = null;
					txtLocation.Tag = string.Empty;
					txtLocation.Text = null;
					txtBin.Text = string.Empty;
					txtBin.Tag = null;
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

		private void btnProductionLine_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnDepartment_Click()";
			try
			{
				if(txtDepartment.Text.Trim() == string.Empty)
				{
					string[] strParam = new string[2];
					strParam[0] = lblDepartment.Text;
					strParam[1] = lblProductionLine.Text;
					PCSMessageBox.Show(ErrorCode.MESSAGE_SELECT_ONE_BEFORE_SELECT_ONE, MessageBoxIcon.Warning, strParam);
					txtDepartment.Focus();
					return;
				}

				Hashtable hashPara = new Hashtable();
				hashPara.Add(PRO_ProductionLineTable.DEPARTMENTID_FLD,txtDepartment.Tag);
				DataRowView drwResult = null;
				drwResult = FormControlComponents.OpenSearchForm(PRO_ProductionLineTable.TABLE_NAME, PRO_ProductionLineTable.CODE_FLD, txtProductionLine.Text.Trim(), hashPara, true);
				if (drwResult != null)
				{
					txtProductionLine.Tag = drwResult[PRO_ProductionLineTable.PRODUCTIONLINEID_FLD];
					txtProductionLine.Text = drwResult[PRO_ProductionLineTable.CODE_FLD].ToString();
					//Fill data to all controls
					txtLocation.Tag = string.Empty;
					txtLocation.Text = null;
					txtBin.Text = string.Empty;
					txtBin.Tag = null;
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

		private void txtProductionLine_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if (btnSave.Enabled && e.KeyCode == Keys.F4)
			{
				btnProductionLine_Click(null, null);
			}
		}

		private void txtProductionLine_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".()";
			try
			{
				if(!txtProductionLine.Modified) return;
				if(txtProductionLine.Text.Trim() == "")
				{
					txtLocation.Tag = null;
					txtLocation.Text = string.Empty;
					txtBin.Text = string.Empty;
					txtBin.Tag = null;
					return;
				}
				if(txtDepartment.Text.Trim() == string.Empty)
				{
					txtProductionLine.Clear();
					string[] strParam = new string[2];
					strParam[0] = lblDepartment.Text;
					strParam[1] = lblProductionLine.Text;
					PCSMessageBox.Show(ErrorCode.MESSAGE_SELECT_ONE_BEFORE_SELECT_ONE, MessageBoxIcon.Warning, strParam);
					txtDepartment.Focus();
					e.Cancel = true;
					return;
				}
				Hashtable  htbCondition = new Hashtable();
				htbCondition.Add(MST_DepartmentTable.DEPARTMENTID_FLD, txtDepartment.Tag);

				DataRowView drwResult = null;
				drwResult = FormControlComponents.OpenSearchForm(PRO_ProductionLineTable.TABLE_NAME, PRO_ProductionLineTable.CODE_FLD, txtProductionLine.Text.Trim(), htbCondition, false);
				if (drwResult != null)
				{
					txtProductionLine.Tag = drwResult[PRO_ProductionLineTable.PRODUCTIONLINEID_FLD];
					txtProductionLine.Text = drwResult[PRO_ProductionLineTable.CODE_FLD].ToString();
					//Fill data to all controls
					txtLocation.Tag = string.Empty;
					txtLocation.Text = null;
					txtBin.Text = string.Empty;
					txtBin.Tag = null;

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

		private void btnLocation_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnDepartment_Click()";
			try
			{
				if(txtProductionLine.Text.Trim() == string.Empty)
				{
					string[] strParam = new string[2];
					strParam[0] = lblProductionLine.Text;
					strParam[1] = lblLocation.Text;
					PCSMessageBox.Show(ErrorCode.MESSAGE_SELECT_ONE_BEFORE_SELECT_ONE, MessageBoxIcon.Warning, strParam);
					txtProductionLine.Focus();
					return;
				}

				Hashtable hashPara = new Hashtable();
				hashPara.Add(PRO_ProductionLineTable.PRODUCTIONLINEID_FLD,txtProductionLine.Tag);
				DataRowView drwResult = null;
				drwResult = FormControlComponents.OpenSearchForm(V_LOCATIONANDPRODUCTIONLINE, MST_LocationTable.CODE_FLD, txtLocation.Text.Trim(), hashPara, true);
				if (drwResult != null)
				{
					txtLocation.Tag = drwResult[MST_LocationTable.LOCATIONID_FLD];
					txtLocation.Text = drwResult[MST_LocationTable.CODE_FLD].ToString();
					//Fill data to all controls
					txtBin.Text = string.Empty;
					txtBin.Tag = null;
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

		private void txtLocation_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if (btnSave.Enabled && e.KeyCode == Keys.F4)
			{
				btnLocation_Click(null, null);
			}
		}

		private void txtLocation_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".()";
			try
			{
				if(!txtLocation.Modified) return;
				if(txtLocation.Text.Trim() == "")
				{
					txtBin.Text = string.Empty;
					txtBin.Tag = null;
					return;
				}

				if(txtProductionLine.Text.Trim() == string.Empty)
				{
					txtLocation.Clear();
					e.Cancel = true;
					string[] strParam = new string[2];
					strParam[0] = lblProductionLine.Text;
					strParam[1] = lblLocation.Text;
					PCSMessageBox.Show(ErrorCode.MESSAGE_SELECT_ONE_BEFORE_SELECT_ONE, MessageBoxIcon.Warning, strParam);
					txtProductionLine.Focus();
					return;
				}
				Hashtable  htbCondition = new Hashtable();
				htbCondition.Add(PRO_ProductionLineTable.PRODUCTIONLINEID_FLD, txtProductionLine.Tag);

				DataRowView drwResult = null;
				drwResult = FormControlComponents.OpenSearchForm(V_LOCATIONANDPRODUCTIONLINE, MST_LocationTable.CODE_FLD, txtLocation.Text.Trim(), htbCondition, false);
				if (drwResult != null)
				{
					txtLocation.Tag = drwResult[MST_LocationTable.LOCATIONID_FLD];
					txtLocation.Text = drwResult[MST_LocationTable.CODE_FLD].ToString();
					//Fill data to all controls
					txtBin.Text = string.Empty;
					txtBin.Tag = null;
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

		private void btnBin_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnBin_Click()";
			try
			{
				if(txtLocation.Text.Trim() == string.Empty)
				{
					string[] strParam = new string[2];
					strParam[0] = lblLocation.Text;
					strParam[1] = lblBin.Text;
					PCSMessageBox.Show(ErrorCode.MESSAGE_SELECT_ONE_BEFORE_SELECT_ONE, MessageBoxIcon.Warning, strParam);
					txtLocation.Focus();
					return;
				}
				Hashtable htbCondition = new Hashtable();
				htbCondition.Add(MST_LocationTable.LOCATIONID_FLD, txtLocation.Tag);
				DataRowView drwResult = null;
				drwResult = FormControlComponents.OpenSearchForm(MST_BINTable.TABLE_NAME, MST_BINTable.CODE_FLD, txtBin.Text.Trim(), htbCondition, true);
				if (drwResult != null)
				{
					txtBin.Tag = drwResult[MST_BINTable.BINID_FLD];
					txtBin.Text = drwResult[MST_BINTable.CODE_FLD].ToString();
					//Fill data to all controls
					FillQuantityInToGrid();
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

		private void txtBin_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if (btnSave.Enabled && e.KeyCode == Keys.F4)
			{
				btnBin_Click(null, null);
			}
		}

		private void txtBin_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".()";
			try
			{
				if(!txtBin.Modified) return;
				if(txtLocation.Text.Trim() == string.Empty)
				{
					txtBin.Clear();
					e.Cancel = true;
					string[] strParam = new string[2];
					strParam[0] = lblLocation.Text;
					strParam[1] = lblBin.Text;
					PCSMessageBox.Show(ErrorCode.MESSAGE_SELECT_ONE_BEFORE_SELECT_ONE, MessageBoxIcon.Warning, strParam);
					txtLocation.Focus();
					return;
				}
				Hashtable  htbCondition = new Hashtable();
				htbCondition.Add(MST_LocationTable.LOCATIONID_FLD, txtLocation.Tag);

				DataRowView drwResult = null;
				drwResult = FormControlComponents.OpenSearchForm(MST_BINTable.TABLE_NAME, MST_BINTable.CODE_FLD, txtBin.Text.Trim(), htbCondition, false);
				if (drwResult != null)
				{
					txtBin.Tag = drwResult[MST_BINTable.BINID_FLD];
					txtBin.Text = drwResult[MST_BINTable.CODE_FLD].ToString();
					//Fill data to all controls
					FillQuantityInToGrid();
				}
				else
				{
					FillQuantityInToGrid();
					e.Cancel = true;
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

		private void btnAdd_Click(object sender, System.EventArgs e)
		{
            const string METHOD_NAME = THIS + ".btnAdd_Click()";
			try
			{
				frmStatus = EnumAction.Add;
				SwitchFormMode();
				txtStockTakingNo.Text = FormControlComponents.GetNoByMask(this,SystemProperty.UserName);

				txtStockTakingNo.Focus();
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

		private void btnStockTakingNo_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnStockTakingPeriod_Click()";
			try
			{
				DataRowView drwResult = null;
				drwResult = FormControlComponents.OpenSearchForm(IV_StockTakingMasterTable.TABLE_NAME, "Code", txtStockTakingNo.Text.Trim(), null, true);
				if (drwResult != null)
				{
					txtStockTakingNo.Tag = drwResult[IV_StockTakingMasterTable.STOCKTAKINGMASTERID_FLD];
					txtStockTakingNo.Text = drwResult["Code"].ToString();
					//Fill data to all controls
					FillDataToControl(int.Parse(txtStockTakingNo.Tag.ToString()));
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

		private void btnEdit_Click(object sender, System.EventArgs e)
		{
			if(Convert.ToBoolean(dstStockTaking.Tables[0].Rows[0]["Closed"]))
			{
				PCSMessageBox.Show(ErrorCode.MESSAGE_SOMETHING_CLOSED,MessageBoxIcon.Warning,new string[]{"Stock taking"});
				return;
			}
			frmStatus = EnumAction.Edit;
			SwitchFormMode();
		}

		
		private void SwitchFormMode()
		{
			switch (frmStatus)
			{
				case EnumAction.Add:
					foreach(Control c in this.Controls)
					{
						if(c.GetType().Equals(typeof(TextBox)))
						{
							if(c != txtCCN)
							{
								c.Text = string.Empty;
								c.Tag = null;
								c.Enabled = true;
							}
						}
						else if(c.GetType().Equals(typeof(C1.Win.C1Input.C1DateEdit)))
						{
							((C1.Win.C1Input.C1DateEdit)c).Value = null;
						}
						else if(c.GetType().Equals(typeof(Button)))
						{
							c.Enabled = true;
						}
					}
					dtmStockTakingDate.Enabled = true;
					btnStockTakingNo.Enabled = false;
					dgrdData.AllowAddNew = true;
					btnAdd.Enabled = btnEdit.Enabled = btnDelete.Enabled = false;
					btnSave.Enabled = true;
					txtCCN.Text = SystemProperty.CCNCode;
					txtCCN.Tag = SystemProperty.CCNID;
					CreateDataSet();
					dgrdData.DataSource = dstGridData.Tables[0];
					FormControlComponents.RestoreGridLayout(dgrdData, dtbGridDesign);
					ConfigGrid(false);
					dgrdData.AllowDelete = true;
					break;
				case EnumAction.Edit:
					foreach(Control c in this.Controls)
					{
						if(c.GetType().Equals(typeof(Button)))
						{
							c.Enabled = true;
						}
						else if(c.GetType().Equals(typeof(TextBox)))
						{
							c.Enabled = true;
						}
					}
					dtmStockTakingDate.Enabled = true;
					btnStockTakingNo.Enabled = false;
					dgrdData.AllowAddNew = true;
					btnAdd.Enabled = btnEdit.Enabled = btnDelete.Enabled = false;
					btnSave.Enabled = true;
					txtCCN.Text = SystemProperty.CCNCode;
					txtCCN.Tag = SystemProperty.CCNID;
					ConfigGrid(false);
					dgrdData.AllowDelete = true;
					break;
				default:
					foreach(Control c in this.Controls)
					{
						if((c.GetType().Equals(typeof(Button))) || (c.GetType().Equals(typeof(TextBox))))
						{
							c.Enabled = false;
						}
					}
					txtStockTakingNo.Enabled = btnStockTakingNo.Enabled = true;
					dtmStockTakingDate.Enabled = false;
					dgrdData.AllowAddNew = false;
					btnAdd.Enabled = btnEdit.Enabled = btnDelete.Enabled = true;
					btnSave.Enabled = false;
					ConfigGrid(true);
					dgrdData.AllowDelete = false;
					break;
			}
			btnHelp.Enabled = btnClose.Enabled = true;
		}

		private void dgrdData_Click(object sender, System.EventArgs e)
		{
		
		}

		private void btnDelete_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnSave_Click()";
			try
			{
				System.Windows.Forms.DialogResult result = PCSMessageBox.Show(ErrorCode.MESSAGE_DELETE_RECORD,MessageBoxButtons.YesNo,MessageBoxIcon.Question);
				if(result == DialogResult.No) return;

				if(Convert.ToBoolean(dstStockTaking.Tables[0].Rows[0]["Closed"]))
				{
					PCSMessageBox.Show(ErrorCode.MESSAGE_SOMETHING_CLOSED,MessageBoxIcon.Warning,new string[]{"Stock taking"});
					return;
				}

				StockTakingBO boStockTaking = new StockTakingBO();
				
				boStockTaking.DeleteStockTaking(Convert.ToInt32(txtStockTakingNo.Tag));
				foreach(Control c in Controls)
				{
					if(c.GetType().Equals(typeof(TextBox))) 
						if(c != txtCCN) c.Text = "";
					dtmStockTakingDate.Value = null;
					dtmFromDate.Value = null;
					dtmToDate.Value = null;
				}
				CreateDataSet();
				dgrdData.DataSource = dstGridData.Tables[0];
				FormControlComponents.RestoreGridLayout(dgrdData, dtbGridDesign);
				ConfigGrid(true);

				blnHasError = false;
				frmStatus = EnumAction.Default;
				SwitchFormMode();
				btnEdit.Enabled = false;
				btnDelete.Enabled = false;

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

		private void txtStockTakingNo_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".()";
			try
			{
				if(!txtStockTakingNo.Modified) return;
				if(!btnStockTakingNo.Enabled) return;
				if(txtStockTakingNo.Text.Trim() == string.Empty)
				{
					if(btnStockTakingNo.Enabled)
					{
						foreach(Control c in Controls)
						{
							if(c.GetType().Equals(typeof(TextBox))) 
								if(c != txtCCN) c.Text = "";
						}
						dtmStockTakingDate.Value = dtmFromDate.Value = dtmToDate.Value = null;
						CreateDataSet();
						dgrdData.DataSource = dstGridData.Tables[0];
						FormControlComponents.RestoreGridLayout(dgrdData, dtbGridDesign);
						ConfigGrid(true);

						blnHasError = false;
						frmStatus = EnumAction.Default;
						SwitchFormMode();
						btnEdit.Enabled=btnDelete.Enabled=false;
					}
					return;
				}

				DataRowView drwResult = null;
				drwResult = FormControlComponents.OpenSearchForm(IV_StockTakingMasterTable.TABLE_NAME, "Code", txtStockTakingNo.Text.Trim(), null, false);
				if (drwResult != null)
				{
					txtStockTakingNo.Tag = drwResult[IV_StockTakingMasterTable.STOCKTAKINGMASTERID_FLD];
					txtStockTakingNo.Text = drwResult[MST_BINTable.CODE_FLD].ToString();
					//Fill data to all controls
					FillDataToControl(int.Parse(txtStockTakingNo.Tag.ToString()));
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

		private void txtStockTakingNo_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if (!btnSave.Enabled && e.KeyCode == Keys.F4)
			{
				if(btnStockTakingNo.Enabled)
					btnStockTakingNo_Click(null, null);
			}
		}

		void FillQuantityInToGrid()
		{
			// get data from database
			StockTakingBO boStockTaking = new StockTakingBO();
			if(txtBin.Tag != null)
			{
				dtbOnHandQty = boStockTaking.GetQuantityFromBin(Convert.ToInt32(txtLocation.Tag),Convert.ToInt32(txtBin.Tag));
				// fill data into grid
				DataTable dtbData = (DataTable) dgrdData.DataSource;
				foreach(DataRow row in dtbData.Rows)
				{
					if(row.RowState == DataRowState.Deleted) continue;
					if(row[IV_StockTakingTable.PRODUCTID_FLD] == DBNull.Value)continue;

					DataRow[] rowOHs = dtbOnHandQty.Select(IV_StockTakingTable.PRODUCTID_FLD+ " = " + row[IV_StockTakingTable.PRODUCTID_FLD]);
					if(rowOHs.Length > 0)
					{
						if (txtBin.Text.Trim() == string.Empty)
							row[IV_StockTakingTable.BOOKQUANTITY_FLD] = 0;
						else
							row[IV_StockTakingTable.BOOKQUANTITY_FLD] = rowOHs[0]["OHQuantity"];
					}
					else 
						row[IV_StockTakingTable.BOOKQUANTITY_FLD] = 0;
				}
			}
		}
	}
}
