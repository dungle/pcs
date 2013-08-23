using System;
using System.Data;
using System.Windows.Forms;
using C1.Win.C1Input;
using C1.Win.C1TrueDBGrid;
using PCSComProcurement.Purchase.BO;
using PCSUtils.Utils;
using PCSUtils.Log;
using PCSComUtils.Common;
using PCSComUtils.Common.BO;
using PCSComUtils.PCSExc;


namespace PCSProcurement.Purchase
{
	/// <summary>
	/// Summary description for PODeliverySchedule.
	/// </summary>
	/// 

	public class PODeliverySchedule : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button btnDelete;
		private System.Windows.Forms.Button btnSave;
		private System.Windows.Forms.Button btnClose;
		private System.Windows.Forms.Button btnHelp;
		private System.Windows.Forms.Button btnPrint;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		private const string THIS = "PCSProcurement.Purchase.PODeliverySchedule";
		EnumAction enumAction;

		private PurchaseOrderInformationVO objPurchaseOrderInformationVO; //store the purchase order information
		private DataSet dstDeliverySchedule;
		decimal decAdjustment = 0;
		DateTime dtmServerDate = new DateTime();
		private DataTable dtGridDesign; //store the design of the grid
		const string ZERO ="Zero";
		const string WARNING ="Warning";
		private System.Windows.Forms.Label lblUM;
		private System.Windows.Forms.Label lblLine;
		private System.Windows.Forms.Label lblItem;
		private System.Windows.Forms.Label lblDescription;
		private System.Windows.Forms.Label lblRevision;
		private System.Windows.Forms.Label lblCCN;
		private System.Windows.Forms.Label lblOrderQty;
		private System.Windows.Forms.Label lblTotalQty;
		private C1.Win.C1Input.C1NumericEdit lblTotalDelivery;
		private C1.Win.C1Input.C1NumericEdit lblOrderQuantity;
		private C1.Win.C1Input.C1DateEdit dtmDate;
		private C1.Win.C1TrueDBGrid.C1TrueDBGrid dgrdData;
		private System.Windows.Forms.TextBox txtUnitCode;
		private System.Windows.Forms.TextBox txtDescription;
		private System.Windows.Forms.TextBox txtRevision;
		private System.Windows.Forms.TextBox txtPartNumber;
		private System.Windows.Forms.TextBox txtLine;
		private System.Windows.Forms.TextBox txtCCN;
		private System.Windows.Forms.TextBox txtCategory;
		private System.Windows.Forms.Label lblCategory;
		bool blnAddNewByF12 = true;

		public PODeliverySchedule()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
		}

		public PODeliverySchedule(PurchaseOrderInformationVO pobjPurchaseOrderInformationVO)
		{
			objPurchaseOrderInformationVO = pobjPurchaseOrderInformationVO;
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
		}

		private int GetRowsCount()
		{
			if (!blnAddNewByF12)
			{
				blnAddNewByF12 = true;
				return dstDeliverySchedule.Tables[0].Select("").Length - 1;

			}
			else
			{
				blnAddNewByF12 = true;
				return dstDeliverySchedule.Tables[0].Select("").Length;
			}
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PODeliverySchedule));
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnHelp = new System.Windows.Forms.Button();
            this.dgrdData = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
            this.lblUM = new System.Windows.Forms.Label();
            this.lblLine = new System.Windows.Forms.Label();
            this.lblItem = new System.Windows.Forms.Label();
            this.lblDescription = new System.Windows.Forms.Label();
            this.lblRevision = new System.Windows.Forms.Label();
            this.lblCCN = new System.Windows.Forms.Label();
            this.lblOrderQty = new System.Windows.Forms.Label();
            this.lblTotalQty = new System.Windows.Forms.Label();
            this.lblTotalDelivery = new C1.Win.C1Input.C1NumericEdit();
            this.lblOrderQuantity = new C1.Win.C1Input.C1NumericEdit();
            this.dtmDate = new C1.Win.C1Input.C1DateEdit();
            this.txtUnitCode = new System.Windows.Forms.TextBox();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.txtRevision = new System.Windows.Forms.TextBox();
            this.txtPartNumber = new System.Windows.Forms.TextBox();
            this.txtLine = new System.Windows.Forms.TextBox();
            this.txtCCN = new System.Windows.Forms.TextBox();
            this.txtCategory = new System.Windows.Forms.TextBox();
            this.lblCategory = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgrdData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblTotalDelivery)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblOrderQuantity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtmDate)).BeginInit();
            this.SuspendLayout();
            // 
            // btnPrint
            // 
            this.btnPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnPrint.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnPrint.Location = new System.Drawing.Point(134, 422);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(64, 23);
            this.btnPrint.TabIndex = 20;
            this.btnPrint.Text = "&Print";
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnDelete.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnDelete.Location = new System.Drawing.Point(69, 422);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(64, 23);
            this.btnDelete.TabIndex = 19;
            this.btnDelete.Text = "&Delete";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnSave.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnSave.Location = new System.Drawing.Point(4, 422);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(64, 23);
            this.btnSave.TabIndex = 18;
            this.btnSave.Text = "&Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnClose.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnClose.Location = new System.Drawing.Point(588, 422);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(64, 23);
            this.btnClose.TabIndex = 22;
            this.btnClose.Text = "&Close";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnHelp
            // 
            this.btnHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnHelp.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnHelp.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnHelp.Location = new System.Drawing.Point(523, 422);
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Size = new System.Drawing.Size(64, 23);
            this.btnHelp.TabIndex = 21;
            this.btnHelp.Text = "&Help";
            // 
            // dgrdData
            // 
            this.dgrdData.AllowAddNew = true;
            this.dgrdData.AllowDelete = true;
            this.dgrdData.AllowFilter = false;
            this.dgrdData.AllowSort = false;
            this.dgrdData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgrdData.GroupByCaption = "Drag a column header here to group by that column";
            this.dgrdData.Images.Add(((System.Drawing.Image)(resources.GetObject("dgrdData.Images"))));
            this.dgrdData.Location = new System.Drawing.Point(4, 84);
            this.dgrdData.Name = "dgrdData";
            this.dgrdData.PreviewInfo.Location = new System.Drawing.Point(0, 0);
            this.dgrdData.PreviewInfo.Size = new System.Drawing.Size(0, 0);
            this.dgrdData.PreviewInfo.ZoomFactor = 75;
            this.dgrdData.PrintInfo.PageSettings = ((System.Drawing.Printing.PageSettings)(resources.GetObject("dgrdData.PrintInfo.PageSettings")));
            this.dgrdData.Size = new System.Drawing.Size(648, 332);
            this.dgrdData.TabIndex = 16;
            this.dgrdData.Text = "c1TrueDBGrid1";
            this.dgrdData.BeforeColEdit += new C1.Win.C1TrueDBGrid.BeforeColEditEventHandler(this.dgrdData_BeforeColEdit);
            this.dgrdData.BeforeInsert += new C1.Win.C1TrueDBGrid.CancelEventHandler(this.dgrdData_BeforeInsert);
            this.dgrdData.BeforeColUpdate += new C1.Win.C1TrueDBGrid.BeforeColUpdateEventHandler(this.dgrdData_BeforeColUpdate);
            this.dgrdData.FetchCellStyle += new C1.Win.C1TrueDBGrid.FetchCellStyleEventHandler(this.dgrdData_FetchCellStyle);
            this.dgrdData.OnAddNew += new System.EventHandler(this.dgrdData_OnAddNew);
            this.dgrdData.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgrdData_KeyDown);
            this.dgrdData.AfterDelete += new System.EventHandler(this.dgrdData_AfterDelete);
            this.dgrdData.AfterUpdate += new System.EventHandler(this.dgrdData_AfterUpdate);
            this.dgrdData.AfterInsert += new System.EventHandler(this.dgrdData_AfterInsert);
            this.dgrdData.AfterColUpdate += new C1.Win.C1TrueDBGrid.ColEventHandler(this.dgrdData_AfterColUpdate);
            this.dgrdData.PropBag = resources.GetString("dgrdData.PropBag");
            // 
            // lblUM
            // 
            this.lblUM.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblUM.ForeColor = System.Drawing.Color.Black;
            this.lblUM.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblUM.Location = new System.Drawing.Point(380, 57);
            this.lblUM.Name = "lblUM";
            this.lblUM.Size = new System.Drawing.Size(28, 18);
            this.lblUM.TabIndex = 12;
            this.lblUM.Text = "Unit";
            this.lblUM.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblLine
            // 
            this.lblLine.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblLine.ForeColor = System.Drawing.Color.Black;
            this.lblLine.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblLine.Location = new System.Drawing.Point(4, 10);
            this.lblLine.Name = "lblLine";
            this.lblLine.Size = new System.Drawing.Size(76, 18);
            this.lblLine.TabIndex = 2;
            this.lblLine.Text = "Line";
            // 
            // lblItem
            // 
            this.lblItem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblItem.ForeColor = System.Drawing.Color.Black;
            this.lblItem.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblItem.Location = new System.Drawing.Point(4, 34);
            this.lblItem.Name = "lblItem";
            this.lblItem.Size = new System.Drawing.Size(76, 18);
            this.lblItem.TabIndex = 4;
            this.lblItem.Text = "Part Number";
            // 
            // lblDescription
            // 
            this.lblDescription.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblDescription.ForeColor = System.Drawing.Color.Black;
            this.lblDescription.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblDescription.Location = new System.Drawing.Point(4, 57);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(76, 18);
            this.lblDescription.TabIndex = 10;
            this.lblDescription.Text = "Part Name";
            // 
            // lblRevision
            // 
            this.lblRevision.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblRevision.ForeColor = System.Drawing.Color.Black;
            this.lblRevision.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblRevision.Location = new System.Drawing.Point(246, 34);
            this.lblRevision.Name = "lblRevision";
            this.lblRevision.Size = new System.Drawing.Size(50, 18);
            this.lblRevision.TabIndex = 6;
            this.lblRevision.Text = "Model";
            // 
            // lblCCN
            // 
            this.lblCCN.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCCN.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblCCN.ForeColor = System.Drawing.Color.Black;
            this.lblCCN.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblCCN.Location = new System.Drawing.Point(503, 8);
            this.lblCCN.Name = "lblCCN";
            this.lblCCN.Size = new System.Drawing.Size(30, 20);
            this.lblCCN.TabIndex = 0;
            this.lblCCN.Text = "CCN";
            this.lblCCN.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblOrderQty
            // 
            this.lblOrderQty.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblOrderQty.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblOrderQty.ForeColor = System.Drawing.Color.Black;
            this.lblOrderQty.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblOrderQty.Location = new System.Drawing.Point(455, 34);
            this.lblOrderQty.Name = "lblOrderQty";
            this.lblOrderQty.Size = new System.Drawing.Size(79, 18);
            this.lblOrderQty.TabIndex = 8;
            this.lblOrderQty.Text = "Order Quantity";
            // 
            // lblTotalQty
            // 
            this.lblTotalQty.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTotalQty.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblTotalQty.ForeColor = System.Drawing.Color.Black;
            this.lblTotalQty.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblTotalQty.Location = new System.Drawing.Point(455, 57);
            this.lblTotalQty.Name = "lblTotalQty";
            this.lblTotalQty.Size = new System.Drawing.Size(74, 18);
            this.lblTotalQty.TabIndex = 14;
            this.lblTotalQty.Text = "Total Delivery";
            this.lblTotalQty.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblTotalDelivery
            // 
            this.lblTotalDelivery.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTotalDelivery.BackColor = System.Drawing.SystemColors.Control;
            this.lblTotalDelivery.FormatType = C1.Win.C1Input.FormatTypeEnum.StandardNumber;
            this.lblTotalDelivery.Location = new System.Drawing.Point(537, 55);
            this.lblTotalDelivery.Name = "lblTotalDelivery";
            this.lblTotalDelivery.ReadOnly = true;
            this.lblTotalDelivery.Size = new System.Drawing.Size(115, 20);
            this.lblTotalDelivery.TabIndex = 15;
            this.lblTotalDelivery.TabStop = false;
            this.lblTotalDelivery.Tag = null;
            this.lblTotalDelivery.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.lblTotalDelivery.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.None;
            // 
            // lblOrderQuantity
            // 
            this.lblOrderQuantity.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblOrderQuantity.BackColor = System.Drawing.SystemColors.Control;
            this.lblOrderQuantity.FormatType = C1.Win.C1Input.FormatTypeEnum.StandardNumber;
            this.lblOrderQuantity.Location = new System.Drawing.Point(537, 32);
            this.lblOrderQuantity.Name = "lblOrderQuantity";
            this.lblOrderQuantity.ReadOnly = true;
            this.lblOrderQuantity.Size = new System.Drawing.Size(115, 20);
            this.lblOrderQuantity.TabIndex = 9;
            this.lblOrderQuantity.TabStop = false;
            this.lblOrderQuantity.Tag = null;
            this.lblOrderQuantity.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.lblOrderQuantity.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.None;
            // 
            // dtmDate
            // 
            // 
            // 
            // 
            this.dtmDate.Calendar.DayNameLength = 1;
            this.dtmDate.Calendar.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.dtmDate.EmptyAsNull = true;
            this.dtmDate.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
            this.dtmDate.Location = new System.Drawing.Point(256, 194);
            this.dtmDate.Name = "dtmDate";
            this.dtmDate.Size = new System.Drawing.Size(134, 20);
            this.dtmDate.TabIndex = 17;
            this.dtmDate.Tag = null;
            this.dtmDate.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.DropDown;
            this.dtmDate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtmDate_KeyDown);
            this.dtmDate.Validating += new System.ComponentModel.CancelEventHandler(this.dtmDate_Validating);
            // 
            // txtUnitCode
            // 
            this.txtUnitCode.BackColor = System.Drawing.SystemColors.Control;
            this.txtUnitCode.Location = new System.Drawing.Point(408, 55);
            this.txtUnitCode.Name = "txtUnitCode";
            this.txtUnitCode.ReadOnly = true;
            this.txtUnitCode.Size = new System.Drawing.Size(38, 20);
            this.txtUnitCode.TabIndex = 13;
            this.txtUnitCode.TabStop = false;
            // 
            // txtDescription
            // 
            this.txtDescription.BackColor = System.Drawing.SystemColors.Control;
            this.txtDescription.Location = new System.Drawing.Point(82, 55);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.ReadOnly = true;
            this.txtDescription.Size = new System.Drawing.Size(292, 20);
            this.txtDescription.TabIndex = 11;
            this.txtDescription.TabStop = false;
            // 
            // txtRevision
            // 
            this.txtRevision.BackColor = System.Drawing.SystemColors.Control;
            this.txtRevision.Location = new System.Drawing.Point(295, 32);
            this.txtRevision.Name = "txtRevision";
            this.txtRevision.ReadOnly = true;
            this.txtRevision.Size = new System.Drawing.Size(79, 20);
            this.txtRevision.TabIndex = 7;
            this.txtRevision.TabStop = false;
            // 
            // txtPartNumber
            // 
            this.txtPartNumber.BackColor = System.Drawing.SystemColors.Control;
            this.txtPartNumber.Location = new System.Drawing.Point(82, 32);
            this.txtPartNumber.Name = "txtPartNumber";
            this.txtPartNumber.ReadOnly = true;
            this.txtPartNumber.Size = new System.Drawing.Size(162, 20);
            this.txtPartNumber.TabIndex = 5;
            this.txtPartNumber.TabStop = false;
            // 
            // txtLine
            // 
            this.txtLine.BackColor = System.Drawing.SystemColors.Control;
            this.txtLine.Location = new System.Drawing.Point(82, 8);
            this.txtLine.Name = "txtLine";
            this.txtLine.ReadOnly = true;
            this.txtLine.Size = new System.Drawing.Size(84, 20);
            this.txtLine.TabIndex = 3;
            this.txtLine.TabStop = false;
            this.txtLine.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtCCN
            // 
            this.txtCCN.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCCN.BackColor = System.Drawing.SystemColors.Control;
            this.txtCCN.Location = new System.Drawing.Point(537, 8);
            this.txtCCN.Name = "txtCCN";
            this.txtCCN.ReadOnly = true;
            this.txtCCN.Size = new System.Drawing.Size(115, 20);
            this.txtCCN.TabIndex = 1;
            this.txtCCN.TabStop = false;
            this.txtCCN.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtCategory
            // 
            this.txtCategory.BackColor = System.Drawing.SystemColors.Control;
            this.txtCategory.Location = new System.Drawing.Point(295, 8);
            this.txtCategory.Name = "txtCategory";
            this.txtCategory.ReadOnly = true;
            this.txtCategory.Size = new System.Drawing.Size(79, 20);
            this.txtCategory.TabIndex = 24;
            this.txtCategory.TabStop = false;
            // 
            // lblCategory
            // 
            this.lblCategory.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblCategory.ForeColor = System.Drawing.Color.Black;
            this.lblCategory.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblCategory.Location = new System.Drawing.Point(246, 8);
            this.lblCategory.Name = "lblCategory";
            this.lblCategory.Size = new System.Drawing.Size(50, 18);
            this.lblCategory.TabIndex = 23;
            this.lblCategory.Text = "Category";
            // 
            // PODeliverySchedule
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(656, 450);
            this.Controls.Add(this.txtCategory);
            this.Controls.Add(this.lblCategory);
            this.Controls.Add(this.txtCCN);
            this.Controls.Add(this.txtLine);
            this.Controls.Add(this.txtPartNumber);
            this.Controls.Add(this.txtRevision);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.txtUnitCode);
            this.Controls.Add(this.dgrdData);
            this.Controls.Add(this.lblTotalDelivery);
            this.Controls.Add(this.lblOrderQuantity);
            this.Controls.Add(this.lblTotalQty);
            this.Controls.Add(this.lblOrderQty);
            this.Controls.Add(this.lblUM);
            this.Controls.Add(this.lblLine);
            this.Controls.Add(this.lblItem);
            this.Controls.Add(this.lblDescription);
            this.Controls.Add(this.lblRevision);
            this.Controls.Add(this.lblCCN);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnHelp);
            this.Controls.Add(this.dtmDate);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.Name = "PODeliverySchedule";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Purchase Order Delivery Schedule";
            this.Load += new System.EventHandler(this.PODeliverySchedule_Load);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.PODeliverySchedule_Closing);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.PODeliverySchedule_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.dgrdData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblTotalDelivery)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblOrderQuantity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtmDate)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		private void btnClose_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void DisplayPurchaseOrderInformation() 
		{
			//lblOrderNo.Text = objSaleOrderInformationVO.SaleOrderNo;
			txtLine.Text = objPurchaseOrderInformationVO.PurchaseOrderLine.ToString();
			txtPartNumber.Text = objPurchaseOrderInformationVO.ProductCode;
			txtRevision.Text = objPurchaseOrderInformationVO.ProductRevision;
			txtDescription.Text = objPurchaseOrderInformationVO.ProductDescription;
			txtUnitCode.Text = objPurchaseOrderInformationVO.UnitCode;
			txtCCN.Text = objPurchaseOrderInformationVO.CCNCode;
			lblOrderQuantity.Value = objPurchaseOrderInformationVO.OrderQuantity.ToString();

			//get the approval status
			PODeliveryScheduleBO objPODeliveryScheduleBO = new PODeliveryScheduleBO();
			objPurchaseOrderInformationVO.ApprovalStatus = objPODeliveryScheduleBO.GetPurchaseOrderApprovalStatus(objPurchaseOrderInformationVO.PurchaseOrderDetailID);

			// 25-05-2006 added by dungla: display category information
			txtCategory.Text = objPurchaseOrderInformationVO.Category;
		}
		private void LoadDeliverySchedule(int intPurchaseOrderLineID) 
		{

			PODeliveryScheduleBO objPODeliveryScheduleBO = new PODeliveryScheduleBO();
			dstDeliverySchedule = objPODeliveryScheduleBO.GetDeliverySchedule(intPurchaseOrderLineID);
			dgrdData.DataSource = dstDeliverySchedule.Tables[0];
			FormControlComponents.RestoreGridLayout(dgrdData,  dtGridDesign);
			
			//Set the read only for the LINE Column and Delivery No
			dgrdData.Splits[0].DisplayColumns[PO_DeliveryScheduleTable.DELIVERYLINE_FLD].Locked = true;
			dgrdData.Splits[0].DisplayColumns[PO_DeliveryScheduleTable.RECEIVEDQUANTITY_FLD].Locked = true;

			//align right for Line, Delivery Quantity, Delivery No
			dgrdData.Splits[0].DisplayColumns[PO_DeliveryScheduleTable.DELIVERYLINE_FLD].Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Far;
			dgrdData.Splits[0].DisplayColumns[PO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD].Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Far;
			dgrdData.Splits[0].DisplayColumns[PO_DeliveryScheduleTable.RECEIVEDQUANTITY_FLD].Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Far;
			dgrdData.Splits[0].DisplayColumns[PO_DeliveryScheduleTable.ADJUSTMENT_FLD].Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Far;
			dgrdData.Splits[0].DisplayColumns[PO_DeliveryScheduleTable.SCHEDULEDATE_FLD].Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center;
			dgrdData.Columns[PO_DeliveryScheduleTable.SCHEDULEDATE_FLD].Editor = dtmDate;
			dgrdData.Columns[PO_DeliveryScheduleTable.SCHEDULEDATE_FLD].NumberFormat = Constants.DATETIME_FORMAT_HOUR;
			dgrdData.Splits[0].DisplayColumns[PO_DeliveryScheduleTable.STARTDATE_FLD].Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center;
			dgrdData.Columns[PO_DeliveryScheduleTable.STARTDATE_FLD].Editor = dtmDate;
			dgrdData.Columns[PO_DeliveryScheduleTable.STARTDATE_FLD].NumberFormat = Constants.DATETIME_FORMAT_HOUR;
			dgrdData.Columns[WARNING].ValueItems.Presentation = PresentationEnum.CheckBox;
			dgrdData.Splits[0].DisplayColumns[WARNING].Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center;
			//Set the Delivery Quantity
			dgrdData.Columns[PO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD].NumberFormat = Constants.DECIMAL_NUMBERFORMAT;
			dgrdData.Columns[PO_DeliveryScheduleTable.RECEIVEDQUANTITY_FLD].NumberFormat = Constants.DECIMAL_NUMBERFORMAT;
			dgrdData.Columns[PO_DeliveryScheduleTable.ADJUSTMENT_FLD].NumberFormat = Constants.DECIMAL_NUMBERFORMAT;
			
			foreach (C1DisplayColumn dcol in dgrdData.Splits[0].DisplayColumns)
			{
				dcol.Locked = true;
			}
			dgrdData.Splits[0].DisplayColumns[PO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD].Locked = false;
			dgrdData.Splits[0].DisplayColumns[PO_DeliveryScheduleTable.SCHEDULEDATE_FLD].Locked = false;
			dgrdData.Splits[0].DisplayColumns[PO_DeliveryScheduleTable.STARTDATE_FLD].Locked = false;

			//Count the total of Delivery Quantity
			lblTotalDelivery.Value = SumTotalDeliveryQuantity().ToString();
		}

		private decimal SumTotalDeliveryQuantity ()
		{
			const string METHOD_NAME = THIS + ".SumTotalDeliveryQuantity()";
			try 
			{
				int intGridRows = this.dgrdData.RowCount;
			
				// now compute the number of unique values for the country and city columns
				decimal dblTotalValue = 0 ;
				for(int i = 0; i < intGridRows; i++)
				{
					try 
					{
						dblTotalValue += (decimal) this.dgrdData[i,PO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD];
					}
					catch
					{
						dblTotalValue += 0;
					}
				}
				return dblTotalValue;
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
				return 0;
			}
		}

		private void EnableDisableButtons()
		{
			dgrdData.AllowAddNew = true;
			dgrdData.AllowDelete = true;
			dgrdData.AllowUpdate = true;
		}
		private void PODeliverySchedule_Load(object sender, System.EventArgs e)
		{
			if (objPurchaseOrderInformationVO == null || objPurchaseOrderInformationVO.PurchaseOrderDetailID <=0) 
			{
				this.Close();
				// You don't have the right to view this item
				PCSMessageBox.Show(ErrorCode.MESSAGE_YOU_HAVE_NO_RIGHT_TO_VIEW,MessageBoxIcon.Warning);
				return;
			}
			
			//set the minimum date
			const string METHOD_NAME = THIS + ".PODeliverySchedule_Load()";
			try 
			{
				//Set authorization for user
				Security objSecurity = new Security();
				this.Name = THIS;
				if(objSecurity.SetRightForUserOnForm(this, SystemProperty.UserName) == 0)
				{
					this.Close();
					// You don't have the right to view this item
					PCSMessageBox.Show(ErrorCode.MESSAGE_YOU_HAVE_NO_RIGHT_TO_VIEW, MessageBoxIcon.Warning);
					return;
				}

				enumAction = new EnumAction();
				enumAction = EnumAction.Default;

				//Keep the Grid Design (for multi languages)
				dtGridDesign = new DataTable();
				dtGridDesign = FormControlComponents.StoreGridLayout(dgrdData);
				dtmDate.CustomFormat = Constants.DATETIME_FORMAT_HOUR;

				//Display Sale Order Information
				DisplayPurchaseOrderInformation();

				//Load the Delivery Detail data
				LoadDeliverySchedule(objPurchaseOrderInformationVO.PurchaseOrderDetailID);
				//change the color of cells which have start date < server date
				dtmServerDate = (new UtilsBO()).GetDBDate();
				foreach (DataRow drow in dstDeliverySchedule.Tables[0].Rows)
				{
					drow[WARNING] = false;
					if (drow[PO_DeliveryScheduleTable.STARTDATE_FLD] != DBNull.Value)
					{
						if ((DateTime)drow[PO_DeliveryScheduleTable.STARTDATE_FLD] < dtmServerDate)
						{
							drow[WARNING] = true;
						}
					}
				}

				dgrdData.Splits[0].DisplayColumns[WARNING].Visible = true;
				
				objPurchaseOrderInformationVO.OrderDate = new DateTime(objPurchaseOrderInformationVO.OrderDate.Year,
					objPurchaseOrderInformationVO.OrderDate.Month,
					objPurchaseOrderInformationVO.OrderDate.Day,
					0, 0, 0);

				//Diable or Enable buttons
				EnableDisableButtons();

				if (objPurchaseOrderInformationVO.ApprovalStatus)
				{
					btnSave.Enabled = false;
					btnDelete.Enabled = false;
				}
				else
				{
					dgrdData.AllowAddNew = true;
					dgrdData.AllowDelete = true;
					dgrdData.AllowUpdate = true;

					btnSave.Enabled = true;
					btnDelete.Enabled = true;
				}
			}
			catch (PCSException ex) 
			{
				// displays the error message.
				PCSMessageBox.Show(ex.mCode,MessageBoxIcon.Error);
				// log message.
				try
				{
					Logger.LogMessage(ex.CauseException, METHOD_NAME, Level.ERROR);
				}
				catch
				{
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION,MessageBoxIcon.Error);
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
		private bool CheckBeforeSaving()
		{
			//for (int i =0; i < GetRowsCount() - 1; i++)
			for (int i =0; i < GetRowsCount(); i++)
			{
				if (dgrdData[i, PO_DeliveryScheduleTable.SCHEDULEDATE_FLD].ToString() == string.Empty)
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Warning);
					dgrdData.Row = i;
					dgrdData.Col = dgrdData.Columns.IndexOf(dgrdData.Columns[PO_DeliveryScheduleTable.SCHEDULEDATE_FLD]);
					dgrdData.Focus();
					return false;
				}
				if (dgrdData[i, PO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD].ToString() == string.Empty)
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Warning);
					dgrdData.Row = i;
					dgrdData.Col = dgrdData.Columns.IndexOf(dgrdData.Columns[PO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD]);
					dgrdData.Focus();
					return false;
				}
			}

			//Check the business 
			decimal dblTotalDeliveryQuantity = SumTotalDeliveryQuantity();
			lblTotalDelivery.Value = dblTotalDeliveryQuantity.ToString();
			if (dblTotalDeliveryQuantity > decimal.Parse(lblOrderQuantity.Text))
			{
				//Display message here
				//MessageBox.Show("The total delivery Quantity must be less than or equal to Order Quantity");
				PCSMessageBox.Show(ErrorCode.MESSAGE_OVER_DELIVERYQTY,MessageBoxIcon.Error);
				return false;
			}

			return true;

		}
		private void SaveToDatabase()
		{
            //Init the BO class
            PODeliveryScheduleBO objPODeliveryScheduleBO = new PODeliveryScheduleBO();
            //call the update data set method
            for (int i = 0; i < dstDeliverySchedule.Tables[0].Rows.Count; i++)
            {
                if (dstDeliverySchedule.Tables[0].Rows[i].RowState != DataRowState.Deleted)
                {
                    dstDeliverySchedule.Tables[0].Rows[i][PO_DeliveryScheduleTable.PURCHASEORDERDETAILID_FLD] = objPurchaseOrderInformationVO.PurchaseOrderDetailID;
                }
            }
            objPODeliveryScheduleBO.UpdateDeliveryDataSet(dstDeliverySchedule, objPurchaseOrderInformationVO.PurchaseOrderDetailID);

            enumAction = EnumAction.Default;

            //re calculate the total Delivery quantity
            lblTotalDelivery.Value = SumTotalDeliveryQuantity().ToString();
		}

		private void PODeliverySchedule_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".PODeliverySchedule_Closing()";
			try
			{
				// 04-05-2006 removed: dungla fix bug 3939 for NganNT
//				if (dgrdData.EditActive)
//				{
//					e.Cancel = true;
//					return;
//				}
				if (enumAction == EnumAction.Add || enumAction == EnumAction.Edit)
				{
					System.Windows.Forms.DialogResult dlgResult = PCSMessageBox.Show(ErrorCode.MESSAGE_QUESTION_STORE_INTO_DATABASE, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
					switch (dlgResult)
					{
						case DialogResult.Yes:
							if (!CheckBeforeSaving())
							{
								e.Cancel = true;
								break;
							}

							SaveToDatabase();
							e.Cancel = false;
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
				PCSMessageBox.Show(ex.mCode,MessageBoxIcon.Error);
				// log message.
				try
				{
					Logger.LogMessage(ex.CauseException, METHOD_NAME, Level.ERROR);
				}
				catch
				{
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION,MessageBoxIcon.Error);
				}
				e.Cancel = true;
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
				e.Cancel = true;

			}
		
		}

		private void btnSave_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnSave_Click()";
			try 
			{
				if (dgrdData.EditActive) return;
				if (!dgrdData.EditActive && !CheckBeforeSaving())
				{
					return ;
				}
				SaveToDatabase();
				LoadDeliverySchedule(objPurchaseOrderInformationVO.PurchaseOrderDetailID);
				enumAction = EnumAction.Default;
				PCSMessageBox.Show(ErrorCode.MESSAGE_AFTER_SAVE_DATA, MessageBoxIcon.Information);
			}
			catch (PCSDBException ex) 
			{
				// displays the error message.
				PCSMessageBox.Show(ex.mCode,MessageBoxIcon.Error);
				// log message.
				try
				{
					Logger.LogMessage(ex.CauseException, METHOD_NAME, Level.ERROR);
				}
				catch
				{
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION,MessageBoxIcon.Error);
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

		private bool IsDeleteAllSchedule()
		{
            int intGridRows = dgrdData.RowCount;

            for (int i = 0; i < intGridRows; i++)
            {
                string strCommit = dgrdData[i, PO_DeliveryScheduleTable.RECEIVEDQUANTITY_FLD].ToString().Trim();
                if (strCommit != String.Empty)
                {
                    if (decimal.Parse(strCommit) > 0)
                    {
                        return false;
                    }
                }
            }
            return true;
		}

		private void btnDelete_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnDelete_Click()";
			DialogResult result;
			if (dgrdData.EditActive) return;
			result = PCSMessageBox.Show(ErrorCode.MESSAGE_DELETE_RECORD, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
			if (result == DialogResult.Yes)
			{
				try
				{
					if (!IsDeleteAllSchedule()) 
					{
						
						//MessageBox.Show("You cannot delete this delivery schedule because it already has received quantity");
						PCSMessageBox.Show(ErrorCode.MESSAGE_PODELIVERY_CANNOTDELETE,MessageBoxIcon.Error);
						return;
					}
					PODeliveryScheduleBO objPODeliveryScheduleBO = new PODeliveryScheduleBO();
					objPODeliveryScheduleBO.DeleteDeliveryDetail(objPurchaseOrderInformationVO.PurchaseOrderDetailID);
					LoadDeliverySchedule(objPurchaseOrderInformationVO.PurchaseOrderDetailID);
					enumAction = EnumAction.Default;
				}
				catch (Exception ex)
				{
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
		}
		private void AssignDefaultValue()
		{
			const string METHOD_NAME = THIS + ".AssignDefaultValue()";
			try 
			{
				if (dgrdData[dgrdData.Row,PO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD].ToString() == string.Empty
					&& dgrdData[dgrdData.Row,PO_DeliveryScheduleTable.SCHEDULEDATE_FLD].ToString() == string.Empty)
				{
					dgrdData[dgrdData.Row,PO_DeliveryScheduleTable.DELIVERYLINE_FLD] = GetRowsCount() + 1;
					dgrdData[dgrdData.Row,PO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD] = decimal.Parse(lblOrderQuantity.Text) - decimal.Parse(lblTotalDelivery.Text);					
					dgrdData[dgrdData.Row,PO_DeliveryScheduleTable.SCHEDULEDATE_FLD] = objPurchaseOrderInformationVO.OrderDate.ToString();
					dgrdData[dgrdData.Row,PO_DeliveryScheduleTable.PURCHASEORDERDETAILID_FLD] = objPurchaseOrderInformationVO.PurchaseOrderDetailID;
					lblTotalDelivery.Value = objPurchaseOrderInformationVO.OrderQuantity.ToString();
				}
				else{
					CurrencyManager cm;

					//dgrdData.Refresh();
					dgrdData.MoveLast();
					//dgrdData.Row = dgrdData.Row + 1;

					cm = (CurrencyManager) dgrdData.BindingContext[dgrdData.DataSource];
					cm.EndCurrentEdit();
					cm.AddNew();

					dgrdData[dgrdData.Row,PO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD] = decimal.Parse(lblOrderQuantity.Text) - decimal.Parse(lblTotalDelivery.Text);					
					dgrdData[dgrdData.Row,PO_DeliveryScheduleTable.DELIVERYLINE_FLD] = dgrdData.Row + 1;
					dgrdData[dgrdData.Row,PO_DeliveryScheduleTable.SCHEDULEDATE_FLD] = objPurchaseOrderInformationVO.OrderDate.ToString();
					dgrdData[dgrdData.Row,PO_DeliveryScheduleTable.PURCHASEORDERDETAILID_FLD] = objPurchaseOrderInformationVO.PurchaseOrderDetailID;
					lblTotalDelivery.Value = objPurchaseOrderInformationVO.OrderQuantity.ToString();
				}
				enumAction = EnumAction.Edit;
			}			
			catch (Exception ex)
			{
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

		private void PODeliverySchedule_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			const string METHOD_NAME = THIS + ".PODeliverySchedule_KeyDown()";
			if (e.KeyCode == Keys.F12)
			{
				dgrdData.UpdateData();
				//add a new row
				if (!dgrdData.AllowAddNew) return;
				try
				{
					if (decimal.Parse(lblTotalDelivery.Text.Trim()) >= decimal.Parse(lblOrderQuantity.Text.Trim()))
					{
						//MessageBox.Show("There are enought order quantity, you cannot add more row");
						PCSMessageBox.Show(ErrorCode.MESSAGE_PODELIVERY_ENOUGHQTY,MessageBoxIcon.Warning);
						return;
					}
					CurrencyManager cm;

					//dgrdData.Refresh();
					dgrdData.MoveLast();
					//dgrdData.Row = dgrdData.Row + 1;

					cm = (CurrencyManager) dgrdData.BindingContext[dgrdData.DataSource];
					cm.EndCurrentEdit();
					cm.AddNew();
					AssignDefaultValue();
					
					//focus on the first cell
					dgrdData.Focus();
					dgrdData.Col = dstDeliverySchedule.Tables[0].Columns.IndexOf(PO_DeliveryScheduleTable.SCHEDULEDATE_FLD);
				}
				catch (NoNullAllowedException ex) 
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID,MessageBoxIcon.Error);
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
				catch (ConstraintException ex) 
				{
					PCSMessageBox.Show(ErrorCode.DUPLICATE_KEY,MessageBoxIcon.Error);
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

				catch (Exception ex)
				{
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
		
		}

		private void dgrdData_AfterColUpdate(object sender, C1.Win.C1TrueDBGrid.ColEventArgs e)
		{
			const string METHOD_NAME = THIS + ".dgrdData_AfterColUpdate()";
			try
			{
				if (e.Column.DataColumn.DataField == PO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD) 
				{
					//Re calculate the total delivery quantity
					lblTotalDelivery.Value = SumTotalDeliveryQuantity().ToString();
				}
				
				if (e.Column.DataColumn.DataField == PO_DeliveryScheduleTable.ADJUSTMENT_FLD)
				{
					if (dgrdData.Columns[PO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD].Value.ToString() != string.Empty)
					{
						dgrdData.Columns[PO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD].Value = 
						(Decimal)dgrdData.Columns[PO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD].Value + decAdjustment;
					}
					
				}
				enumAction = EnumAction.Edit;
			}
			catch (Exception ex)
			{
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
		/// dgrdData_AfterDelete
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Wednesday, September 28 2005</date>
		private void dgrdData_AfterDelete(object sender, System.EventArgs e)
		{
			//re calculate the total delivery quantity
			const string METHOD_NAME = THIS + ".dgrdData_AfterDelete()";
			try 
			{
				for (int i = 0; i < dgrdData.RowCount; i++)
				{
					if (dgrdData[i, PO_DeliveryScheduleTable.SCHEDULEDATE_FLD].ToString() != string.Empty)
					{
						dgrdData[i, PO_DeliveryScheduleTable.DELIVERYLINE_FLD] = i+1;
					}
				}
				lblTotalDelivery.Value = SumTotalDeliveryQuantity().ToString();
				enumAction = EnumAction.Edit;
			}			
			catch (Exception ex)
			{
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

		private void dgrdData_AfterInsert(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".dgrdData_AfterInsert()";
			try 
			{
				lblTotalDelivery.Value = SumTotalDeliveryQuantity().ToString();
				enumAction = EnumAction.Edit;
			}			
			catch (Exception ex)
			{
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

		private void dgrdData_AfterUpdate(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".dgrdData_AfterUpdate()";
			try 
			{
				lblTotalDelivery.Value = SumTotalDeliveryQuantity().ToString();
				enumAction = EnumAction.Edit;
			}			
			catch (Exception ex)
			{
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

		private void dgrdData_BeforeColUpdate(object sender, C1.Win.C1TrueDBGrid.BeforeColUpdateEventArgs e)
		{
			const string METHOD_NAME = THIS + ".dgrdData_BeforeColEdit()";
			try 
			{
				//first check the Delivery Quantity 
				string strValue = String.Empty;
				if (e.Column.DataColumn.DataField == PO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD)
				{
					if (e.Column.DataColumn.Value.ToString() == string.Empty) return;
					//Delivery Quantity must be higher 0
					double dblDeliveryQuantity = 0;
					if (e.Column.DataColumn.Value.ToString().Trim() == String.Empty)
					{
						dblDeliveryQuantity = 0 ;
					}
					else
					{
						try 
						{
							dblDeliveryQuantity = double.Parse(e.Column.DataColumn.Value.ToString());
						}
						catch
						{
							dblDeliveryQuantity = 0;
						}
					}
					
					double dblOldValue ;
					if (e.OldValue.ToString().Trim() != String.Empty)
					{
						dblOldValue = double.Parse(e.OldValue.ToString());
					}
					else
					{
						dblOldValue = 0;
					}

					double dblRemainingQty = double.Parse(lblOrderQuantity.Text) + dblOldValue - double.Parse(lblTotalDelivery.Text);
					if (!(dblDeliveryQuantity >0 && dblDeliveryQuantity <=dblRemainingQty))
					{
						//MessageBox.Show("The Delivery Quantity must be higher than 0");
						PCSMessageBox.Show(ErrorCode.MESSAGE_MIN_DELIVERYQTY,MessageBoxIcon.Warning);
						e.Cancel = true;
						return;
					}
					int intDeliveryScheduleID = 0;
					try
					{
						intDeliveryScheduleID = Convert.ToInt32(dgrdData[dgrdData.Row, PO_DeliveryScheduleTable.DELIVERYSCHEDULEID_FLD]);
					}
					catch{}
					// get sum of receipt quantity of current line
					if (intDeliveryScheduleID > 0)
					{
						POPurchaseOrderReceiptsBO boPOPurchaseOrderReceipts = new POPurchaseOrderReceiptsBO();
						decimal decTotalReceiptQuantity = boPOPurchaseOrderReceipts.GetTotalReceiptQuantity(intDeliveryScheduleID);
						decimal decOrderQuantity = Convert.ToDecimal(e.Column.DataColumn.Text);
						if (decOrderQuantity < decTotalReceiptQuantity)
						{
							string[] msg = {dgrdData.Columns[PO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD].Caption,
											   "Receipt Quantity"};
							PCSMessageBox.Show(ErrorCode.MESSAGE_GREATER_THAN, MessageBoxIcon.Warning, msg);
							e.Cancel = true;
							return;
						}
					}
					btnSave.Enabled = true;
				}
				else
				{
					// HACK: Trada 24-11-2005
					if (e.Column.DataColumn.DataField == PO_DeliveryScheduleTable.ADJUSTMENT_FLD)
					{
						
						try
						{
							if (e.Column.DataColumn.Value.ToString() == string.Empty)
							{
								return;
							}
							decAdjustment = Decimal.Parse(e.Column.DataColumn.Text);
							if ((dgrdData[dgrdData.Row, PO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD].ToString() != string.Empty)&&(Decimal.Parse(dgrdData[dgrdData.Row, PO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD].ToString()) + decAdjustment <= 0))
							{
								string[] strParam = new string[1];
								strParam[0] = ZERO;
								PCSMessageBox.Show(ErrorCode.MESSAGE_SUM_OF_DELIVERY_ADJUSTMENT_GREATER_THAN, MessageBoxIcon.Warning, strParam);
								e.Cancel = true;
							}
						}
						catch
						{
							PCSMessageBox.Show(ErrorCode.MESSAGE_INVALID_NUMERIC, MessageBoxIcon.Warning);
							e.Cancel = true;
						}
						
					} 
					// END: Trada 24-11-2005

					//Second check the Date for : Schedule, Promise, and Require Date
					//These must be higher than Order Date
					switch (e.Column.DataColumn.DataField)
					{
						case PO_DeliveryScheduleTable.SCHEDULEDATE_FLD:
							//strValue = dgrdData[dgrdData.Row,e.ColIndex].ToString().Trim();
							strValue = e.Column.DataColumn.Text;
							break;
					}
					if (strValue != String.Empty)
					{
						try 
						{
							if (DateTime.Parse(dtmDate.Value.ToString()) < objPurchaseOrderInformationVO.OrderDate)
							{
								//MessageBox.Show("The date must be higher than Order Date");
								PCSMessageBox.Show(ErrorCode.MESSAGE_CHECK_ORDERDATE,MessageBoxIcon.Warning);
								e.Cancel = true;
							}
						}
						catch
						{
							PCSMessageBox.Show(ErrorCode.MESSAGE_CHECK_DATE,MessageBoxIcon.Warning);
							e.Cancel = true;
						}
					}
					else
					{
						return;
					}
				}
				string[] strParams = new string[2];

				//check start date
				if (e.Column.DataColumn.DataField == PO_DeliveryScheduleTable.STARTDATE_FLD)
				{
					try
					{
						if (e.Column.DataColumn.Value.ToString() != string.Empty)
						{
							DateTime dtmStart = DateTime.Parse(dtmDate.Value.ToString());
							if (dgrdData[dgrdData.Row,PO_DeliveryScheduleTable.SCHEDULEDATE_FLD].ToString() != string.Empty)
							{
								DateTime dtmDue = DateTime.Parse(dgrdData[dgrdData.Row,PO_DeliveryScheduleTable.SCHEDULEDATE_FLD].ToString());
								if (dtmDue < dtmStart)
								{
									strParams[0] = "Schedule Date";
									strParams[1] = "Start Date";
									PCSMessageBox.Show(ErrorCode.MESSAGE_GREATER_THAN, MessageBoxIcon.Warning, strParams);
									e.Cancel = true;
								}
							}
						}
					}
					catch
					{
						PCSMessageBox.Show(ErrorCode.MESSAGE_INVALID_DATETIME, MessageBoxIcon.Error);
						e.Cancel = true;
					}
				}

				//check due date
				if (e.Column.DataColumn.DataField == PO_DeliveryScheduleTable.SCHEDULEDATE_FLD)
				{
					try
					{
						if (e.Column.DataColumn.Value.ToString() != string.Empty)
						{
							DateTime dtmDue = DateTime.Parse(dtmDate.Value.ToString());
							if (dgrdData[dgrdData.Row,PO_DeliveryScheduleTable.STARTDATE_FLD].ToString() != string.Empty)
							{
								DateTime dtmStart = DateTime.Parse(dgrdData[dgrdData.Row,PO_DeliveryScheduleTable.STARTDATE_FLD].ToString());
								if (dtmDue < dtmStart)
								{
									strParams[0] = "Schedule Date";
									strParams[1] = "Start Date";
									PCSMessageBox.Show(ErrorCode.MESSAGE_GREATER_THAN, MessageBoxIcon.Warning, strParams);
									e.Cancel = true;
								}
							}
						}
					}
					catch
					{
						PCSMessageBox.Show(ErrorCode.MESSAGE_INVALID_DATETIME, MessageBoxIcon.Error);
						e.Cancel = true;
					}
				}
			}			
			catch (Exception ex)
			{
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

		private void dgrdData_BeforeInsert(object sender, C1.Win.C1TrueDBGrid.CancelEventArgs e)
		{
			blnAddNewByF12 = false;
			if (decimal.Parse(lblOrderQuantity.Text.Trim()) <= decimal.Parse(lblTotalDelivery.Text.Trim())) 
			{
				PCSMessageBox.Show(ErrorCode.MESSAGE_PODELIVERY_ENOUGHQTY,MessageBoxIcon.Warning);
				e.Cancel = true;
			}
		}

		private void dtmDate_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".dtmDate_Validating()";
			try
			{
				if (!dtmDate.Modified) return;
				if (dtmDate.Value != DBNull.Value)
				{
					DateTime dtinputDate = 
						DateTime.Parse(dtmDate.Value.ToString());
					DateTime dtorderDate = 
						new System.DateTime( objPurchaseOrderInformationVO.OrderDate.Year, objPurchaseOrderInformationVO.OrderDate.Month,objPurchaseOrderInformationVO.OrderDate.Day);
					int intCompareTwoDate = dtorderDate.CompareTo(dtinputDate);
					if ( intCompareTwoDate > 0)
					{
						PCSMessageBox.Show(ErrorCode.MESSAGE_CHECK_ORDERDATE,MessageBoxIcon.Error);
						e.Cancel = true;
					}
					else
					{
						//check the period
						if (!FormControlComponents.CheckDateInCurrentPeriod(dtinputDate))
						{
							//MessageBox.Show("Please input the Entry date");
							PCSMessageBox.Show(ErrorCode.MESSAGE_RTG_ENTRYDATE,MessageBoxIcon.Warning);
							e.Cancel = true;
						}
					}
				}
				else
				{
					PCSMessageBox.Show(ErrorCode.MESSAGE_CHECK_ORDERDATE,MessageBoxIcon.Error);
					e.Cancel = true;
				}
			}			
			catch (Exception ex)
			{
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

		private void dtmDate_DropDownClosed(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".dtmTransDate_DropDownClosed()";
			try
			{
				if (dtmDate.Text != string.Empty)
				{
					DateTime dtmValue = new DateTime(DateTime.Parse(dtmDate.Value.ToString()).Year, DateTime.Parse(dtmDate.Value.ToString()).Month, DateTime.Parse(dtmDate.Value.ToString()).Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
					dtmDate.Value = dtmValue;
				}
				else
				{
					dgrdData[dgrdData.Row, dgrdData.Col] = DBNull.Value;
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

		private void dtmDate_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			const string METHOD_NAME = THIS + ".dtmDate_KeyDown()";
			try
			{
				if ((dtmDate.Text.Trim() == string.Empty) || (e.KeyCode == Keys.Enter
					&& DateTime.Parse(dtmDate.Value.ToString()) >= new System.DateTime( objPurchaseOrderInformationVO.OrderDate.Year, objPurchaseOrderInformationVO.OrderDate.Month,objPurchaseOrderInformationVO.OrderDate.Day)))
				{
					dgrdData.Col = dgrdData.Col + 1;
					dgrdData.Focus();
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

		private void dgrdData_BeforeColEdit(object sender, C1.Win.C1TrueDBGrid.BeforeColEditEventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnMasLoc_Click()";
			try
			{
				dgrdData[dgrdData.Row, PO_DeliveryScheduleTable.DELIVERYLINE_FLD] = dgrdData.Row + 1;
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

		private void dgrdData_OnAddNew(object sender, System.EventArgs e)
		{
			CurrencyManager cm;

			//dgrdData.Refresh();
			dgrdData.MoveLast();
			//dgrdData.Row = dgrdData.Row + 1;

			cm = (CurrencyManager) dgrdData.BindingContext[dgrdData.DataSource];
			cm.EndCurrentEdit();
			cm.AddNew();
			AssignDefaultValue();
		}

		/// <summary>
		/// dgrdData_KeyDown
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Thursday, Feb 23 2006</date>
		private void dgrdData_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			const string METHOD_NAME = THIS + ".dgrdData_KeyDown()";
			try
			{
				if (e.KeyCode == Keys.Delete && dgrdData.SelectedRows.Count > 0)
				{
					if (btnSave.Enabled)
					{
						if (PCSMessageBox.Show(ErrorCode.YES_NO_MESSAGE, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
						{	
							FormControlComponents.DeleteMultiRowsOnTrueDBGrid(dgrdData);
							int intCount = 0;
							foreach (DataRow objRow in dstDeliverySchedule.Tables[0].Rows)
							{
								if(objRow.RowState != DataRowState.Deleted) 
									intCount++;
							}
							for (int i =0; i <intCount; i++)
								dgrdData[i, PO_DeliveryScheduleTable.DELIVERYLINE_FLD] = i+1;
							enumAction = EnumAction.Edit;
						}
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
		/// dgrdData_FetchCellStyle
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Thursday, September 7 2006</date>
		private void dgrdData_FetchCellStyle(object sender, C1.Win.C1TrueDBGrid.FetchCellStyleEventArgs e)
		{
			const string METHOD_NAME = THIS + ".dgrdData_FetchCellStyle()";
			try 
			{
//				if (dgrdData.Col == dgrdData.Columns.IndexOf(dgrdData.Columns[PO_DeliveryScheduleTable.STARTDATE_FLD]))
//				{
					if (dgrdData[dgrdData.Row, PO_DeliveryScheduleTable.STARTDATE_FLD].ToString() != string.Empty)
					{
						if ((DateTime)dgrdData[dgrdData.Row, PO_DeliveryScheduleTable.STARTDATE_FLD] < dtmServerDate)
						{
							e.CellStyle.ForeColor = System.Drawing.Color.Red;
						}
					}
				//}
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
	}
	[Serializable]
	public class PurchaseOrderInformationVO
	{
		private int mPurchaseOrderMasterID;
		private int mPurchaseOrderDetailID;
		private string mPurchaseOrderNo;
		private int mPurchaseOrderLine;
		private int mProductID;
		private string mProductCode;
		private string mProductRevision;
		private string mProductDescription;
		private string mUnitCode;
		private string mCCNCode;
		private decimal mOrderQuantity;
		private DateTime mOrderDate;
		private bool mApprovalStatus;
		private string mCategory;

        public int PartyId { get; set; }

		public string Category
		{
			get { return mCategory; }
			set { mCategory = value; }
		}

		public int PurchaseOrderMasterID
		{
			get
			{
				return mPurchaseOrderMasterID;
			}
			set 
			{
				mPurchaseOrderMasterID = value;
			}
		}
		public int PurchaseOrderDetailID
		{
			get
			{
				return mPurchaseOrderDetailID;
			}
			set 
			{
				mPurchaseOrderDetailID = value;
			}
		}
		public string PurchaseOrderNo
		{
			get
			{
				return mPurchaseOrderNo;
			}
			set 
			{
				mPurchaseOrderNo = value;
			}
		}
		public int PurchaseOrderLine
		{
			get
			{
				return mPurchaseOrderLine;
			}
			set 
			{
				mPurchaseOrderLine = value;
			}
		}
		public int ProductID
		{
			get
			{
				return mProductID;
			}
			set 
			{
				mProductID = value;
			}
		}
		public string ProductCode
		{
			get
			{
				return mProductCode;
			}
			set 
			{
				mProductCode = value;
			}
		}
		public string ProductRevision
		{
			get
			{
				return mProductRevision;
			}
			set 
			{
				mProductRevision = value;
			}
		}
		public string ProductDescription
		{
			get
			{
				return mProductDescription;
			}
			set 
			{
				mProductDescription = value;
			}
		}
		public string UnitCode
		{
			get
			{
				return mUnitCode;
			}
			set 
			{
				mUnitCode = value;
			}
		}
		public string CCNCode
		{
			get
			{
				return mCCNCode;
			}
			set 
			{
				mCCNCode = value;
			}
		}
		public decimal OrderQuantity
		{
			get
			{
				return mOrderQuantity;
			}
			set 
			{
				mOrderQuantity = value;
			}
		}

		public DateTime OrderDate
		{
			get
			{
				return mOrderDate;
			}
			set 
			{
				mOrderDate = value;
			}
		}
		public bool ApprovalStatus
		{
			get
			{
				return mApprovalStatus;
			}
			set 
			{
				mApprovalStatus = value;
			}
		}
		
	}
}
