using System;
using System.Data;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using PCSComUtils.Common;

namespace PCSUtils.Utils
{
	/// <summary>
	/// Summary description for MessageBoxFormForItems.
	/// </summary>
	public class MessageBoxFormForItems : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button btnClose;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		public string FormTitle;
		public string MessageDescription;
		private C1.Win.C1TrueDBGrid.C1TrueDBGrid dgrdData;
		public System.Windows.Forms.Label lblPrimaryVendor;
		public System.Windows.Forms.Label lblListPrice;
		public System.Windows.Forms.Label lblTitle;
		public System.Windows.Forms.Label lblVendorCurrency;
		public System.Windows.Forms.Label lblExchangeRate;
		public System.Windows.Forms.Label lblVendorLoc;
		public System.Windows.Forms.Label lblProductionLine;
		public System.Windows.Forms.Label lblVendorDeliverySchedule;
		public DataTable BugReason;

		public MessageBoxFormForItems()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		private void btnClose_Click(object sender, System.EventArgs e)
		{
			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.WaitCursor;

			#endregion Code Inserted Automatically

			
			this.Close();

			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.Default;

			#endregion Code Inserted Automatically

			
		}

		private void MessageBoxFormForItems_Load(object sender, System.EventArgs e)
		{
			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.WaitCursor;

			#endregion Code Inserted Automatically

			
			new Security().SetRightForUserOnForm(this, SystemProperty.UserName);

			DataTable dtbGridLayOut = FormControlComponents.StoreGridLayout(dgrdData);
			dgrdData.DataSource = BugReason;

			//Restore layout
			FormControlComponents.RestoreGridLayout(dgrdData, dtbGridLayOut);

			this.MaximizeBox = false;
			this.FormBorderStyle = FormBorderStyle.FixedSingle;

			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.Default;

			#endregion Code Inserted Automatically

			
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MessageBoxFormForItems));
            this.lblTitle = new System.Windows.Forms.Label();
            this.dgrdData = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
            this.btnClose = new System.Windows.Forms.Button();
            this.lblPrimaryVendor = new System.Windows.Forms.Label();
            this.lblListPrice = new System.Windows.Forms.Label();
            this.lblVendorLoc = new System.Windows.Forms.Label();
            this.lblVendorCurrency = new System.Windows.Forms.Label();
            this.lblExchangeRate = new System.Windows.Forms.Label();
            this.lblProductionLine = new System.Windows.Forms.Label();
            this.lblVendorDeliverySchedule = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgrdData)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.Location = new System.Drawing.Point(7, 2);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(807, 27);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "The list of items which is not enough information";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dgrdData
            // 
            this.dgrdData.AllowUpdate = false;
            this.dgrdData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgrdData.CaptionHeight = 17;
            this.dgrdData.Images.Add(((System.Drawing.Image)(resources.GetObject("dgrdData.Images"))));
            this.dgrdData.Location = new System.Drawing.Point(7, 30);
            this.dgrdData.Name = "dgrdData";
            this.dgrdData.PreviewInfo.Location = new System.Drawing.Point(0, 0);
            this.dgrdData.PreviewInfo.Size = new System.Drawing.Size(0, 0);
            this.dgrdData.PreviewInfo.ZoomFactor = 75D;
            this.dgrdData.PrintInfo.PageSettings = ((System.Drawing.Printing.PageSettings)(resources.GetObject("dgrdData.PrintInfo.PageSettings")));
            this.dgrdData.RowHeight = 15;
            this.dgrdData.Size = new System.Drawing.Size(620, 388);
            this.dgrdData.TabIndex = 1;
            this.dgrdData.PropBag = resources.GetString("dgrdData.PropBag");
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(543, 424);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(84, 26);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "&Close";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // lblPrimaryVendor
            // 
            this.lblPrimaryVendor.Location = new System.Drawing.Point(67, 92);
            this.lblPrimaryVendor.Name = "lblPrimaryVendor";
            this.lblPrimaryVendor.Size = new System.Drawing.Size(120, 27);
            this.lblPrimaryVendor.TabIndex = 3;
            this.lblPrimaryVendor.Text = "Primary Vendor";
            this.lblPrimaryVendor.Visible = false;
            // 
            // lblListPrice
            // 
            this.lblListPrice.Location = new System.Drawing.Point(67, 115);
            this.lblListPrice.Name = "lblListPrice";
            this.lblListPrice.Size = new System.Drawing.Size(120, 27);
            this.lblListPrice.TabIndex = 4;
            this.lblListPrice.Text = "Purchasing Price";
            this.lblListPrice.Visible = false;
            // 
            // lblVendorLoc
            // 
            this.lblVendorLoc.Location = new System.Drawing.Point(67, 141);
            this.lblVendorLoc.Name = "lblVendorLoc";
            this.lblVendorLoc.Size = new System.Drawing.Size(120, 26);
            this.lblVendorLoc.TabIndex = 5;
            this.lblVendorLoc.Text = "Vendor\'s Location";
            this.lblVendorLoc.Visible = false;
            // 
            // lblVendorCurrency
            // 
            this.lblVendorCurrency.Location = new System.Drawing.Point(67, 166);
            this.lblVendorCurrency.Name = "lblVendorCurrency";
            this.lblVendorCurrency.Size = new System.Drawing.Size(120, 27);
            this.lblVendorCurrency.TabIndex = 6;
            this.lblVendorCurrency.Text = "Vendor\'s Currency";
            this.lblVendorCurrency.Visible = false;
            // 
            // lblExchangeRate
            // 
            this.lblExchangeRate.Location = new System.Drawing.Point(266, 150);
            this.lblExchangeRate.Name = "lblExchangeRate";
            this.lblExchangeRate.Size = new System.Drawing.Size(168, 27);
            this.lblExchangeRate.TabIndex = 7;
            this.lblExchangeRate.Text = "Currency Exchange Rate";
            this.lblExchangeRate.Visible = false;
            // 
            // lblProductionLine
            // 
            this.lblProductionLine.Location = new System.Drawing.Point(269, 175);
            this.lblProductionLine.Name = "lblProductionLine";
            this.lblProductionLine.Size = new System.Drawing.Size(168, 27);
            this.lblProductionLine.TabIndex = 8;
            this.lblProductionLine.Text = "Production Line";
            this.lblProductionLine.Visible = false;
            // 
            // lblVendorDeliverySchedule
            // 
            this.lblVendorDeliverySchedule.Location = new System.Drawing.Point(266, 203);
            this.lblVendorDeliverySchedule.Name = "lblVendorDeliverySchedule";
            this.lblVendorDeliverySchedule.Size = new System.Drawing.Size(178, 27);
            this.lblVendorDeliverySchedule.TabIndex = 9;
            this.lblVendorDeliverySchedule.Text = "Vendor\'s Delivery Schedule";
            this.lblVendorDeliverySchedule.Visible = false;
            // 
            // MessageBoxFormForItems
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 15);
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(634, 455);
            this.Controls.Add(this.lblVendorDeliverySchedule);
            this.Controls.Add(this.lblProductionLine);
            this.Controls.Add(this.lblExchangeRate);
            this.Controls.Add(this.lblVendorCurrency);
            this.Controls.Add(this.lblVendorLoc);
            this.Controls.Add(this.lblListPrice);
            this.Controls.Add(this.lblPrimaryVendor);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.dgrdData);
            this.Controls.Add(this.lblTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MessageBoxFormForItems";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PCS Message Box";
            this.Load += new System.EventHandler(this.MessageBoxFormForItems_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgrdData)).EndInit();
            this.ResumeLayout(false);

		}
		#endregion
		public MessageBoxFormForItems(string pstrFormTitle, string pstrMessageDes, DataTable pdtbSource)
		{
			FormTitle = pstrFormTitle;
			if (pstrFormTitle != null && pstrFormTitle.Trim() != string.Empty)
				lblTitle.Text = pstrFormTitle;
			MessageDescription = pstrMessageDes;
			BugReason = pdtbSource;
		}

		public MessageBoxFormForItems(DataTable pdtbSource)
		{
			BugReason = pdtbSource;
		}
	}
}
