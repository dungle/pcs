using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Text;
using System.Windows.Forms;
using C1.Win.C1Input;
using C1.Win.C1TrueDBGrid;
using PCSComProduction.WorkOrder.BO;
using PCSComUtils.Common;
using PCSComUtils.Common.BO;
using PCSComUtils.PCSExc;
using PCSUtils.Log;
using PCSUtils.Utils;

namespace PCSProduction.WorkOrder
{
	/// <summary>
	/// Summary description for SelectWorkOrders.
	/// </summary>
	public class SelectWorkOrders : Form
	{
		private C1TrueDBGrid dgrdData;
		private CheckBox chkSelectAll;
		private Button btnClose;
		private Button btnHelp;
		private Button btnSelect;
		private C1DateEdit dtmToStartDate;
		private C1DateEdit dtmFromStartDate;
		private Label lblToStartDate;
		private Button btnSearch;
		private Label lblCCN;
		private Button btnSearchBeginWO;
		private TextBox txtBeginWO;
		private Label lblMasLoc;
		private Label lblFromStartDate;
		private Label lblWO;
		private Label lblMasLocValue;
		private Label lblCCNValue;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private Container components = null;

        DataSet dtbData = new DataSet();

		private const string THIS = "PCSProduction.WorkOrder.SelectWorkOrders";

		private const string HASBIN = "HasBin";
		
		private const string REMAIN_WO_FOR_ISSUE_VIEW = "v_RemainWOForIssue";		
		private const string REMAIN_COMPONENT_FOR_ISSUE_VIEW = "v_RemainComponentForWOIssueWithParentInfo";

		private DataTable dtStoreGridLayout;
		SelectWorkOrdersBO objSelectWorkOrdersBO = new SelectWorkOrdersBO();
		
		private int mMasterLocationID;
		private string mMasterLocCode;
		private int mCCNID;
		private string mCCNCode;
		private int intWorkOrderMasterID;
		private StringBuilder sbCondition = new StringBuilder();

		private const string SELECTED_COL = "SELECTED";
		#region Properties

		private int mToLocationID = 0;

		DataSet mReturnResult;
		
		public DataSet SelectedResultDataSet
		{
			get{ return mReturnResult;}
		}

		
		public int ToLocationID
		{
			set{ mToLocationID = value;}
			get{ return mToLocationID;}
		}

		public int MasterLocationID 
		{
			set{ mMasterLocationID = value;}
		}

		public string MasterLocationCode 
		{
			set{ mMasterLocCode = value;}
		}

		public int CCNID 
		{
			set{ mCCNID = value;}
		}

		public string CCNCode 
		{
			set{ mCCNCode = value;}
		}

		#endregion Properties

		public SelectWorkOrders()
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SelectWorkOrders));
            this.dgrdData = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
            this.chkSelectAll = new System.Windows.Forms.CheckBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnHelp = new System.Windows.Forms.Button();
            this.btnSelect = new System.Windows.Forms.Button();
            this.dtmToStartDate = new C1.Win.C1Input.C1DateEdit();
            this.dtmFromStartDate = new C1.Win.C1Input.C1DateEdit();
            this.lblToStartDate = new System.Windows.Forms.Label();
            this.btnSearch = new System.Windows.Forms.Button();
            this.lblCCN = new System.Windows.Forms.Label();
            this.btnSearchBeginWO = new System.Windows.Forms.Button();
            this.txtBeginWO = new System.Windows.Forms.TextBox();
            this.lblMasLoc = new System.Windows.Forms.Label();
            this.lblFromStartDate = new System.Windows.Forms.Label();
            this.lblWO = new System.Windows.Forms.Label();
            this.lblMasLocValue = new System.Windows.Forms.Label();
            this.lblCCNValue = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgrdData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtmToStartDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtmFromStartDate)).BeginInit();
            this.SuspendLayout();
            // 
            // dgrdData
            // 
            this.dgrdData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgrdData.DataView = C1.Win.C1TrueDBGrid.DataViewEnum.GroupBy;
            this.dgrdData.FilterBar = true;
            this.dgrdData.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.dgrdData.GroupByAreaVisible = false;
            this.dgrdData.GroupByCaption = "";
            this.dgrdData.Images.Add(((System.Drawing.Image)(resources.GetObject("dgrdData.Images"))));
            this.dgrdData.Location = new System.Drawing.Point(4, 74);
            this.dgrdData.MultiSelect = C1.Win.C1TrueDBGrid.MultiSelectEnum.None;
            this.dgrdData.Name = "dgrdData";
            this.dgrdData.PreviewInfo.Location = new System.Drawing.Point(0, 0);
            this.dgrdData.PreviewInfo.Size = new System.Drawing.Size(0, 0);
            this.dgrdData.PreviewInfo.ZoomFactor = 75;
            this.dgrdData.PrintInfo.PageSettings = ((System.Drawing.Printing.PageSettings)(resources.GetObject("dgrdData.PrintInfo.PageSettings")));
            this.dgrdData.Size = new System.Drawing.Size(762, 350);
            this.dgrdData.TabIndex = 9;
            this.dgrdData.AfterColUpdate += new C1.Win.C1TrueDBGrid.ColEventHandler(this.dgrdData_AfterColUpdate);
            this.dgrdData.PropBag = resources.GetString("dgrdData.PropBag");
            // 
            // chkSelectAll
            // 
            this.chkSelectAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkSelectAll.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.chkSelectAll.Location = new System.Drawing.Point(74, 427);
            this.chkSelectAll.Name = "chkSelectAll";
            this.chkSelectAll.Size = new System.Drawing.Size(81, 23);
            this.chkSelectAll.TabIndex = 11;
            this.chkSelectAll.Text = "Select &All";
            this.chkSelectAll.Click += new System.EventHandler(this.chkSelectAll_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnClose.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnClose.Location = new System.Drawing.Point(704, 427);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(60, 23);
            this.btnClose.TabIndex = 13;
            this.btnClose.Text = "&Close";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnHelp
            // 
            this.btnHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnHelp.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnHelp.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnHelp.Location = new System.Drawing.Point(640, 427);
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Size = new System.Drawing.Size(64, 23);
            this.btnHelp.TabIndex = 12;
            this.btnHelp.Text = "&Help";
            // 
            // btnSelect
            // 
            this.btnSelect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSelect.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnSelect.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnSelect.Location = new System.Drawing.Point(4, 427);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(67, 23);
            this.btnSelect.TabIndex = 10;
            this.btnSelect.Text = "S&elect";
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // dtmToStartDate
            // 
            this.dtmToStartDate.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(177)))), ((int)(((byte)(187)))), ((int)(((byte)(214)))));
            this.dtmToStartDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            // 
            // 
            // 
            this.dtmToStartDate.Calendar.Font = new System.Drawing.Font("Tahoma", 8F);
            this.dtmToStartDate.Calendar.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.dtmToStartDate.Calendar.VisualStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.dtmToStartDate.Calendar.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.dtmToStartDate.CustomFormat = "dd-MM-yyyy";
            this.dtmToStartDate.EmptyAsNull = true;
            this.dtmToStartDate.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
            this.dtmToStartDate.Location = new System.Drawing.Point(312, 50);
            this.dtmToStartDate.Name = "dtmToStartDate";
            this.dtmToStartDate.Size = new System.Drawing.Size(128, 18);
            this.dtmToStartDate.TabIndex = 7;
            this.dtmToStartDate.Tag = null;
            this.dtmToStartDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.dtmToStartDate.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.DropDown;
            this.dtmToStartDate.VisualStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.dtmToStartDate.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.dtmToStartDate.WrapDateTimeFields = false;
            // 
            // dtmFromStartDate
            // 
            this.dtmFromStartDate.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(177)))), ((int)(((byte)(187)))), ((int)(((byte)(214)))));
            this.dtmFromStartDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            // 
            // 
            // 
            this.dtmFromStartDate.Calendar.Font = new System.Drawing.Font("Tahoma", 8F);
            this.dtmFromStartDate.Calendar.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.dtmFromStartDate.Calendar.VisualStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.dtmFromStartDate.Calendar.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.dtmFromStartDate.CustomFormat = "dd-MM-yyyy ";
            this.dtmFromStartDate.EmptyAsNull = true;
            this.dtmFromStartDate.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
            this.dtmFromStartDate.Location = new System.Drawing.Point(106, 50);
            this.dtmFromStartDate.Name = "dtmFromStartDate";
            this.dtmFromStartDate.Size = new System.Drawing.Size(128, 18);
            this.dtmFromStartDate.TabIndex = 5;
            this.dtmFromStartDate.Tag = null;
            this.dtmFromStartDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.dtmFromStartDate.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.DropDown;
            this.dtmFromStartDate.VisualStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.dtmFromStartDate.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.dtmFromStartDate.WrapDateTimeFields = false;
            // 
            // lblToStartDate
            // 
            this.lblToStartDate.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblToStartDate.Location = new System.Drawing.Point(236, 50);
            this.lblToStartDate.Name = "lblToStartDate";
            this.lblToStartDate.Size = new System.Drawing.Size(74, 20);
            this.lblToStartDate.TabIndex = 6;
            this.lblToStartDate.Text = "To Start Date";
            this.lblToStartDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnSearch.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnSearch.Location = new System.Drawing.Point(682, 47);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(82, 23);
            this.btnSearch.TabIndex = 8;
            this.btnSearch.Text = "&Search";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // lblCCN
            // 
            this.lblCCN.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCCN.ForeColor = System.Drawing.Color.Maroon;
            this.lblCCN.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblCCN.Location = new System.Drawing.Point(662, 6);
            this.lblCCN.Name = "lblCCN";
            this.lblCCN.Size = new System.Drawing.Size(30, 20);
            this.lblCCN.TabIndex = 17;
            this.lblCCN.Text = "CCN";
            this.lblCCN.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnSearchBeginWO
            // 
            this.btnSearchBeginWO.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnSearchBeginWO.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnSearchBeginWO.Location = new System.Drawing.Point(236, 28);
            this.btnSearchBeginWO.Name = "btnSearchBeginWO";
            this.btnSearchBeginWO.Size = new System.Drawing.Size(24, 20);
            this.btnSearchBeginWO.TabIndex = 4;
            this.btnSearchBeginWO.Text = "...";
            this.btnSearchBeginWO.Click += new System.EventHandler(this.btnSearchBeginWO_Click);
            // 
            // txtBeginWO
            // 
            this.txtBeginWO.Location = new System.Drawing.Point(106, 28);
            this.txtBeginWO.Name = "txtBeginWO";
            this.txtBeginWO.Size = new System.Drawing.Size(128, 20);
            this.txtBeginWO.TabIndex = 3;
            this.txtBeginWO.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBeginWO_KeyDown);
            this.txtBeginWO.Leave += new System.EventHandler(this.txtBeginWO_Leave);
            // 
            // lblMasLoc
            // 
            this.lblMasLoc.ForeColor = System.Drawing.Color.Maroon;
            this.lblMasLoc.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblMasLoc.Location = new System.Drawing.Point(4, 6);
            this.lblMasLoc.Name = "lblMasLoc";
            this.lblMasLoc.Size = new System.Drawing.Size(100, 20);
            this.lblMasLoc.TabIndex = 14;
            this.lblMasLoc.Text = "Mas. Location";
            this.lblMasLoc.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblFromStartDate
            // 
            this.lblFromStartDate.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblFromStartDate.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblFromStartDate.Location = new System.Drawing.Point(4, 50);
            this.lblFromStartDate.Name = "lblFromStartDate";
            this.lblFromStartDate.Size = new System.Drawing.Size(100, 20);
            this.lblFromStartDate.TabIndex = 16;
            this.lblFromStartDate.Text = "From Start Date";
            this.lblFromStartDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblWO
            // 
            this.lblWO.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblWO.Location = new System.Drawing.Point(4, 28);
            this.lblWO.Name = "lblWO";
            this.lblWO.Size = new System.Drawing.Size(100, 20);
            this.lblWO.TabIndex = 15;
            this.lblWO.Text = "Work Order";
            this.lblWO.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblMasLocValue
            // 
            this.lblMasLocValue.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblMasLocValue.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblMasLocValue.Location = new System.Drawing.Point(106, 6);
            this.lblMasLocValue.Name = "lblMasLocValue";
            this.lblMasLocValue.Size = new System.Drawing.Size(128, 20);
            this.lblMasLocValue.TabIndex = 18;
            this.lblMasLocValue.Text = "MasLoc";
            this.lblMasLocValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblCCNValue
            // 
            this.lblCCNValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCCNValue.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCCNValue.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblCCNValue.Location = new System.Drawing.Point(694, 6);
            this.lblCCNValue.Name = "lblCCNValue";
            this.lblCCNValue.Size = new System.Drawing.Size(70, 20);
            this.lblCCNValue.TabIndex = 19;
            this.lblCCNValue.Text = "CCNValue";
            this.lblCCNValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // SelectWorkOrders
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(768, 453);
            this.Controls.Add(this.lblCCNValue);
            this.Controls.Add(this.lblMasLocValue);
            this.Controls.Add(this.dtmToStartDate);
            this.Controls.Add(this.dtmFromStartDate);
            this.Controls.Add(this.txtBeginWO);
            this.Controls.Add(this.dgrdData);
            this.Controls.Add(this.lblToStartDate);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.lblCCN);
            this.Controls.Add(this.btnSearchBeginWO);
            this.Controls.Add(this.lblMasLoc);
            this.Controls.Add(this.lblFromStartDate);
            this.Controls.Add(this.lblWO);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnHelp);
            this.Controls.Add(this.btnSelect);
            this.Controls.Add(this.chkSelectAll);
            this.KeyPreview = true;
            this.Name = "SelectWorkOrders";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Select Component";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.SelectWorkOrders_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgrdData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtmToStartDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtmFromStartDate)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		private void SelectWorkOrders_Load(object sender, EventArgs e)
		{
			const string METHOD_NAME = THIS + ".MultiWOIssueMaterial_Load()";
			try 
			{
				//Set form security
				Security objSecurity = new Security();
				this.Name = THIS;
				if (objSecurity.SetRightForUserOnForm(this, SystemProperty.UserName) == 0)
				{
					// You don't have the right to view this item
					PCSMessageBox.Show(ErrorCode.MESSAGE_YOU_HAVE_NO_RIGHT_TO_VIEW, MessageBoxIcon.Warning);
					this.Close();
					return;
				}

				//check if the Location and the CCN has data
				if (mMasterLocationID <=0 || mCCNID <=0)
				{
					//MessageBox.Show("Please select the Master Location and CCN id");
					PCSMessageBox.Show(ErrorCode.MSG_WOISSUE_MATERIAL_SELECT_MASLOC_AND_CCN, MessageBoxIcon.Warning);
					this.Close();
					return;
				}
				//Format DateTime control
				dtmFromStartDate.CustomFormat = Constants.DATETIME_FORMAT_HOUR;
				dtmToStartDate.CustomFormat = Constants.DATETIME_FORMAT_HOUR;
			    var workingPeriod = Utilities.Instance.GetWorkingPeriod();
			    dtmFromStartDate.Value = workingPeriod.FromDate;
			    dtmToStartDate.Value = Utilities.Instance.GetServerDate();
				//store the gridlayout
				dtStoreGridLayout = FormControlComponents.StoreGridLayout(dgrdData);

				//Display the data on form
				lblMasLocValue.Text = mMasterLocCode;
				lblCCNValue.Text = mCCNCode;               
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
		/// Display the detail data after searching into the grid
		/// //set layout for the grid
		/// </summary>
		/// <param name="pdtbData"></param>
		private void LoadDataDetail(DataSet pdtbData) 
		{
			//load this data into the grid
			dgrdData.DataSource = pdtbData.Tables[0];

			//Restore the gridLayout
			FormControlComponents.RestoreGridLayout(dgrdData, dtStoreGridLayout);
				
			//HACK: added by Tuan TQ. Lock columns
			for(int i =0; i < dgrdData.Splits[0].DisplayColumns.Count; i++)
			{
				dgrdData.Splits[0].DisplayColumns[i].Locked = true;
			}

			dgrdData.Splits[0].DisplayColumns[SELECTED_COL].Locked = false;

			//End hack
			//set the column to be check box
			//Align center for date
			dgrdData.Splits[0].DisplayColumns[PRO_WorkOrderDetailTable.STARTDATE_FLD].Style.HorizontalAlignment = AlignHorzEnum.Center;
			dgrdData.Splits[0].DisplayColumns[PRO_WorkOrderDetailTable.DUEDATE_FLD].Style.HorizontalAlignment = AlignHorzEnum.Center;


			//align right for Quantity
			dgrdData.Splits[0].DisplayColumns[PRO_WorkOrderDetailTable.ORDERQUANTITY_FLD].Style.HorizontalAlignment = AlignHorzEnum.Far;
            dgrdData.Splits[0].DisplayColumns[PRO_WorkOrderDetailTable.TABLE_NAME + PRO_WorkOrderDetailTable.LINE_FLD].Style.HorizontalAlignment = AlignHorzEnum.Far;

			//Align center for the Selected Column
			dgrdData.Splits[0].DisplayColumns[SELECTED_COL].Style.HorizontalAlignment = AlignHorzEnum.Center;

			//Set format for the Quantity
			dgrdData.Columns[PRO_WorkOrderDetailTable.ORDERQUANTITY_FLD].NumberFormat = Constants.DECIMAL_NUMBERFORMAT;
			dgrdData.Columns[PRO_WorkOrderDetailTable.STARTDATE_FLD].NumberFormat = Constants.DATETIME_FORMAT_HOUR;
			dgrdData.Columns[PRO_WorkOrderDetailTable.DUEDATE_FLD].NumberFormat = Constants.DATETIME_FORMAT_HOUR;
				
			//set the selected to be the check box
			dgrdData.Columns[SELECTED_COL].ValueItems.Presentation = PresentationEnum.CheckBox;
		}

		public void btnSearch_Click(object sender, EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnSearch_Click()";
			try
			{
				
				DateTime dtmFromDate = DateTime.MinValue, dtmToDate = DateTime.MinValue;
				if (dtmFromStartDate.Value != DBNull.Value && dtmFromStartDate.Text != String.Empty)
				{
					DateTime dtmDate = (DateTime)dtmFromStartDate.Value;
					dtmFromDate = new DateTime(dtmDate.Year, dtmDate.Month, dtmDate.Day, dtmDate.Hour, dtmDate.Minute, 0);
				}

				if (dtmToStartDate.Value != DBNull.Value && dtmToStartDate.Text != String.Empty)
				{
					DateTime dtmDate = (DateTime)dtmToStartDate.Value;
					dtmToDate = new DateTime(dtmDate.Year, dtmDate.Month, dtmDate.Day, dtmDate.Hour, dtmDate.Minute, 0);
				}
				string strCondition = (new UtilsBO()).GetConditionByRecord(SystemProperty.UserName, REMAIN_COMPONENT_FOR_ISSUE_VIEW);
			    dtbData = objSelectWorkOrdersBO.SearchWorkOrderToIssueMaterial(strCondition, mMasterLocationID, mToLocationID, intWorkOrderMasterID, dtmFromDate, dtmToDate);
                if (dtbData != null && dtbData.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in dtbData.Tables[0].Rows)
                    {
                        dr["SELECTED"] = "False";
                    }
                }

				LoadDataDetail(dtbData);
				chkSelectAll.Checked = false;
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
		
		/// <summary>
		/// Search for specific WORK Order
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnSearchBeginWO_Click(object sender, EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnSearchBeginWO_Click()";
			try 
			{

				DataRowView drwResult = null;
				//search for master location id
				Hashtable hashCondition = new Hashtable();

				hashCondition.Add(PRO_WorkOrderMasterTable.MASTERLOCATIONID_FLD, mMasterLocationID);
				
				//HACK: added by Tuan TQ. 19 Jan, 2006. Apply To Location for issuing
				hashCondition.Add(MST_LocationTable.LOCATIONID_FLD, mToLocationID);
				//End Hack

				drwResult = FormControlComponents.OpenSearchForm(REMAIN_WO_FOR_ISSUE_VIEW, PRO_WorkOrderMasterTable.WORKORDERNO_FLD,txtBeginWO.Text.Trim(), hashCondition,true);
				if (drwResult != null)
				{
					intWorkOrderMasterID = int.Parse(drwResult[PRO_WorkOrderMasterTable.WORKORDERMASTERID_FLD].ToString());
					txtBeginWO.Text = drwResult[PRO_WorkOrderMasterTable.WORKORDERNO_FLD].ToString();
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

		private void btnClose_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.No;
			this.Close();
		}
		
		private void txtBeginWO_KeyDown(object sender, KeyEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtBeginWO_KeyDown";
			try
			{
				if (e.KeyCode == Keys.F4)
				{
					if (btnSearchBeginWO.Enabled)
					{
						btnSearchBeginWO_Click(null,null);
					}
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

		private void txtBeginWO_Leave(object sender, EventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtBeginWO_Leave";
			try
			{
				FormControlComponents.OnLeaveControl(sender, e);
				if (!btnSearchBeginWO.Enabled || !txtBeginWO.Modified)
				{
					return;
				}

				txtBeginWO.Text = txtBeginWO.Text.Trim();
				if (txtBeginWO.Text == String.Empty)
				{
					intWorkOrderMasterID = 0;
					return;
				}

				UtilsBO objUtilsBO = new UtilsBO();
				intWorkOrderMasterID = 0;
				//search for master location id
				Hashtable hashCondition = new Hashtable();
				hashCondition.Add(PRO_WorkOrderMasterTable.MASTERLOCATIONID_FLD,mMasterLocationID);

				DataTable dtResult = objUtilsBO.GetRows(REMAIN_WO_FOR_ISSUE_VIEW, PRO_WorkOrderMasterTable.WORKORDERNO_FLD, txtBeginWO.Text, hashCondition);
				if (dtResult.Rows.Count == 0)
				{
					txtBeginWO.Text = String.Empty;
					btnSearchBeginWO_Click(null,null);
				}
				else
				{
					if (dtResult.Rows.Count > 1)
					{
						btnSearchBeginWO_Click(null,null);
					}
					else
					{
						intWorkOrderMasterID = int.Parse(dtResult.Rows[0][PRO_WorkOrderMasterTable.WORKORDERMASTERID_FLD].ToString());
						txtBeginWO.Text = dtResult.Rows[0][PRO_WorkOrderMasterTable.WORKORDERNO_FLD].ToString();
					}
				}

				if (intWorkOrderMasterID <=0 )
				{
					txtBeginWO.Text = String.Empty;
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
		
		private void btnSelect_Click(object sender, EventArgs e)
		 {
			const string METHOD_NAME = THIS + ".btnSelect_Click";			
			
			try
			{
				this.Cursor = Cursors.WaitCursor;

				MultiWOIssueMaterialBO boWOIssue = new MultiWOIssueMaterialBO();			

				mReturnResult = boWOIssue.GetDetailData(0);

				mReturnResult.Tables[0].Columns.Add(HASBIN);
				mReturnResult.Tables[0].Columns.Add(ITM_ProductTable.LOTCONTROL_FLD);
				mReturnResult.Tables[0].Columns[PRO_IssueMaterialDetailTable.LINE_FLD].AutoIncrement = true;
				mReturnResult.Tables[0].Columns[PRO_IssueMaterialDetailTable.LINE_FLD].AutoIncrementSeed = 1;
				mReturnResult.Tables[0].Columns[PRO_IssueMaterialDetailTable.LINE_FLD].AutoIncrementStep = 1;

				mReturnResult = objSelectWorkOrdersBO.GetSelectedRecords(dtbData.Tables[0].TableName, mReturnResult);

				if(mReturnResult.Tables[0].Rows.Count == 0)
				{
					PCSMessageBox.Show(ErrorCode.MSG_WOISSUE_MATERIAL_SELECT_ATLEAST_ONE_WOLINE, MessageBoxIcon.Warning);
					this.Cursor = Cursors.Default;
					return;
				}
				else
				{	
					this.Cursor = Cursors.Default;
					this.DialogResult = DialogResult.OK;
					this.Close();					
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
			finally
			{
				this.Cursor = Cursors.Default;
			}
		}
		
		private void dgrdData_AfterColUpdate(object sender, ColEventArgs e)
		{
			const string METHOD_NAME = THIS + ".dgrdData_AfterColUpdate()";

			try
			{
				if(e.Column.DataColumn.DataField == SELECTED_COL)
				{
					string strExpression = "(" + PRO_WorkOrderDetailTable.WORKORDERMASTERID_FLD + "=" + dgrdData[dgrdData.Row, PRO_WorkOrderDetailTable.WORKORDERMASTERID_FLD].ToString()
						+ " AND " + PRO_WorkOrderDetailTable.WORKORDERDETAILID_FLD + "=" + dgrdData[dgrdData.Row, PRO_WorkOrderDetailTable.WORKORDERDETAILID_FLD].ToString()
                        + " AND " + PRO_WorkOrderDetailTable.TABLE_NAME + PRO_WorkOrderDetailTable.LINE_FLD + "=" + dgrdData[dgrdData.Row, PRO_WorkOrderDetailTable.TABLE_NAME + PRO_WorkOrderDetailTable.LINE_FLD].ToString()
						+ " AND " + MST_LocationTable.LOCATIONID_FLD + "=" + dgrdData[dgrdData.Row, MST_LocationTable.LOCATIONID_FLD].ToString()
						+ " AND " + MST_BINTable.BINID_FLD + "=" + dgrdData[dgrdData.Row, MST_BINTable.BINID_FLD].ToString()
						+ " AND " + ITM_ProductTable.PRODUCTID_FLD + "=" + dgrdData[dgrdData.Row, ITM_ProductTable.PRODUCTID_FLD].ToString() + ")";

					if(dgrdData[dgrdData.Row, SELECTED_COL].Equals(DBNull.Value)
						|| dgrdData[dgrdData.Row, SELECTED_COL].ToString().Trim() == string.Empty)
					{
						chkSelectAll.Checked = false;
						sbCondition.Replace(strExpression + "|", string.Empty);
						return;
					}

					if(!bool.Parse(dgrdData[dgrdData.Row, SELECTED_COL].ToString()))
					{
						chkSelectAll.Checked = false;
						sbCondition.Replace(strExpression + "|", string.Empty);
						return;
					}

					int nViewCount = dtbData.Tables[0].DefaultView.Count;
					string strFilter = dtbData.Tables[0].DefaultView.RowFilter;
					if (strFilter == string.Empty)
						strFilter = SELECTED_COL + "=1";
					else
						strFilter += " AND " + SELECTED_COL + "=1";
					int nSelectedCount = dtbData.Tables[0].Select(strFilter).Length + 1;
					
					chkSelectAll.Checked = nViewCount == nSelectedCount;
					sbCondition.Append(strExpression).Append(" OR ");
                    
                    int i = 0;
                    UtilsBO objUtilBO = new UtilsBO();
                    int iProductId = 0;
                    int iWorkOrderMasterID = 0;
                    int iWorkOrderDetailID=0;
                    int iComponentID=0;
                    bool b = false;
                    if (dgrdData[dgrdData.Row, "ProductID"] != DBNull.Value)
                    {
                        iProductId = Convert.ToInt32(dgrdData[dgrdData.Row, "ProductID"]);
                    }
                    if (dgrdData[dgrdData.Row, "WorkOrderMasterID"] != DBNull.Value)
                    {
                        iWorkOrderMasterID = Convert.ToInt32(dgrdData[dgrdData.Row, "WorkOrderMasterID"]);
                    }
                    if (dgrdData[dgrdData.Row, "WorkOrderDetailID"] != DBNull.Value)
                    {
                        iWorkOrderDetailID = Convert.ToInt32(dgrdData[dgrdData.Row, "WorkOrderDetailID"]);
                    }
                    if (dgrdData[dgrdData.Row, "ComponentID"] != DBNull.Value)
                    {
                        iComponentID = Convert.ToInt32(dgrdData[dgrdData.Row, "ComponentID"]);
                    }
                    if (dgrdData[dgrdData.Row, "SELECTED"] != DBNull.Value)
                    {
                        b = Convert.ToBoolean(dgrdData[dgrdData.Row, "SELECTED"]);
                    }
                    //string strFilter = ((DataTable)dgrdData.DataSource).DefaultView.RowFilter;

                    objUtilBO.UpdateSelectedRow(dtbData.Tables[0].TableName,"", b, iProductId,iWorkOrderMasterID,iWorkOrderDetailID,iComponentID);
                    //LoadDataDetail(dtbData);
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

		private void chkSelectAll_Click(object sender, EventArgs e)
		{
			const string METHOD_NAME = THIS + ".chkSelectAll_Click()";

			try
			{
				string strFilter = ((DataTable)dgrdData.DataSource).DefaultView.RowFilter;
				var boUtils = new UtilsBO();
				dtbData = boUtils.UpdateSelected(dtbData.Tables[0].TableName, strFilter, chkSelectAll.Checked);
				LoadDataDetail(dtbData);
				dtbData.Tables[0].DefaultView.RowFilter = strFilter;
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
