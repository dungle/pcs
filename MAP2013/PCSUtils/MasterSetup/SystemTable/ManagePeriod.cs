using System;
using System.Data;
using System.Windows.Forms;
using C1.Win.C1TrueDBGrid;
using PCSComUtils.PCSExc;
using PCSComUtils.Common.BO;
using PCSComUtils.Common.DS;
using PCSUtils.Log;
using PCSUtils.Utils;
using PCSComUtils.Common;



namespace PCSUtils.MasterSetup.SystemTable
{
	/// <summary>
	/// Summary description for UpdatePeriod.
	/// </summary>
	public class ManagePeriod : System.Windows.Forms.Form
	{
		#region My variable
		const string THIS = "PCSUtils.MasterSetup.SystemTable.ManagePeriod";
		private const string DATETIME_FOMART = "dd-MM-yyyy";
		private EnumAction enumAction = EnumAction.Default;
		
		#endregion My variable

		private System.Windows.Forms.Button btnSave;
		private System.Windows.Forms.Button btnClose;
		private System.Windows.Forms.Button btnHelp;
		private C1.Win.C1TrueDBGrid.C1TrueDBGrid gridPeriod;
		DataTable dtbGridLayout;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.Button btnEdit;
		private System.Windows.Forms.Button btnAdd;
		private System.Windows.Forms.Button btnDelete;
		private System.Windows.Forms.Label lblStatus;
		private System.Windows.Forms.Label lblToDate;
		private C1.Win.C1Input.C1DateEdit dtmToDate;
		private System.Windows.Forms.CheckBox ckbStatus;
		private System.Windows.Forms.Label lblFromDate;
		private C1.Win.C1Input.C1DateEdit dtmFromDate;
		private System.Windows.Forms.Label lblEndDayOfMonth;
		private System.Windows.Forms.Label lblFirstDayOfMonth;
		Sys_PeriodVO voPeriod = new Sys_PeriodVO();
        DataSet dstPeriod = new DataSet();

		bool blnSaveOK = true;

		public ManagePeriod()
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
            C1.Win.C1TrueDBGrid.Style style1 = new C1.Win.C1TrueDBGrid.Style();
            C1.Win.C1TrueDBGrid.Style style2 = new C1.Win.C1TrueDBGrid.Style();
            C1.Win.C1TrueDBGrid.Style style3 = new C1.Win.C1TrueDBGrid.Style();
            C1.Win.C1TrueDBGrid.Style style4 = new C1.Win.C1TrueDBGrid.Style();
            C1.Win.C1TrueDBGrid.Style style5 = new C1.Win.C1TrueDBGrid.Style();
            C1.Win.C1TrueDBGrid.Style style6 = new C1.Win.C1TrueDBGrid.Style();
            C1.Win.C1TrueDBGrid.Style style7 = new C1.Win.C1TrueDBGrid.Style();
            C1.Win.C1TrueDBGrid.Style style8 = new C1.Win.C1TrueDBGrid.Style();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ManagePeriod));
            C1.Win.C1TrueDBGrid.Style style9 = new C1.Win.C1TrueDBGrid.Style();
            C1.Win.C1TrueDBGrid.Style style10 = new C1.Win.C1TrueDBGrid.Style();
            C1.Win.C1TrueDBGrid.Style style11 = new C1.Win.C1TrueDBGrid.Style();
            C1.Win.C1TrueDBGrid.Style style12 = new C1.Win.C1TrueDBGrid.Style();
            C1.Win.C1TrueDBGrid.Style style13 = new C1.Win.C1TrueDBGrid.Style();
            this.gridPeriod = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnHelp = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblToDate = new System.Windows.Forms.Label();
            this.dtmToDate = new C1.Win.C1Input.C1DateEdit();
            this.ckbStatus = new System.Windows.Forms.CheckBox();
            this.lblFromDate = new System.Windows.Forms.Label();
            this.dtmFromDate = new C1.Win.C1Input.C1DateEdit();
            this.lblEndDayOfMonth = new System.Windows.Forms.Label();
            this.lblFirstDayOfMonth = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.gridPeriod)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtmToDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtmFromDate)).BeginInit();
            this.SuspendLayout();
            // 
            // gridPeriod
            // 
            this.gridPeriod.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gridPeriod.CaptionHeight = 17;
            this.gridPeriod.CaptionStyle = style1;
            this.gridPeriod.CollapseColor = System.Drawing.Color.Black;
            this.gridPeriod.EditorStyle = style2;
            this.gridPeriod.EvenRowStyle = style3;
            this.gridPeriod.ExpandColor = System.Drawing.Color.Black;
            this.gridPeriod.FilterBarStyle = style4;
            this.gridPeriod.FooterStyle = style5;
            this.gridPeriod.GroupByCaption = "Drag a column header here to group by that column";
            this.gridPeriod.GroupStyle = style6;
            this.gridPeriod.HeadingStyle = style7;
            this.gridPeriod.HighLightRowStyle = style8;
            this.gridPeriod.Images.Add(((System.Drawing.Image)(resources.GetObject("gridPeriod.Images"))));
            this.gridPeriod.InactiveStyle = style9;
            this.gridPeriod.Location = new System.Drawing.Point(4, 76);
            this.gridPeriod.MarqueeStyle = C1.Win.C1TrueDBGrid.MarqueeEnum.DottedCellBorder;
            this.gridPeriod.Name = "gridPeriod";
            this.gridPeriod.OddRowStyle = style10;
            this.gridPeriod.PreviewInfo.Location = new System.Drawing.Point(0, 0);
            this.gridPeriod.PreviewInfo.Size = new System.Drawing.Size(0, 0);
            this.gridPeriod.PreviewInfo.ZoomFactor = 75;
            this.gridPeriod.PrintInfo.PageSettings = ((System.Drawing.Printing.PageSettings)(resources.GetObject("gridPeriod.PrintInfo.PageSettings")));
            this.gridPeriod.PrintInfo.ShowOptionsDialog = false;
            this.gridPeriod.RecordSelectorStyle = style11;
            this.gridPeriod.RecordSelectorWidth = 17;
            this.gridPeriod.RowDivider.Color = System.Drawing.Color.DarkGray;
            this.gridPeriod.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.Single;
            this.gridPeriod.RowHeight = 15;
            this.gridPeriod.RowSubDividerColor = System.Drawing.Color.DarkGray;
            this.gridPeriod.SelectedStyle = style12;
            this.gridPeriod.Size = new System.Drawing.Size(614, 252);
            this.gridPeriod.Style = style13;
            this.gridPeriod.TabIndex = 6;
            this.gridPeriod.Text = "c1TrueDBGrid1";
            this.gridPeriod.PropBag = resources.GetString("gridPeriod.PropBag");
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSave.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnSave.Location = new System.Drawing.Point(66, 332);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(62, 23);
            this.btnSave.TabIndex = 8;
            this.btnSave.Text = "&Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnClose.Location = new System.Drawing.Point(556, 332);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(62, 23);
            this.btnClose.TabIndex = 12;
            this.btnClose.Text = "  &Close";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnHelp
            // 
            this.btnHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnHelp.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnHelp.Location = new System.Drawing.Point(493, 332);
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Size = new System.Drawing.Size(62, 23);
            this.btnHelp.TabIndex = 11;
            this.btnHelp.Text = "&Help";
            // 
            // btnEdit
            // 
            this.btnEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnEdit.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnEdit.Location = new System.Drawing.Point(130, 332);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(62, 23);
            this.btnEdit.TabIndex = 9;
            this.btnEdit.Text = "&Edit";
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAdd.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnAdd.Location = new System.Drawing.Point(4, 332);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(62, 23);
            this.btnAdd.TabIndex = 7;
            this.btnAdd.Text = "&Add";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDelete.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnDelete.Location = new System.Drawing.Point(192, 332);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(62, 23);
            this.btnDelete.TabIndex = 10;
            this.btnDelete.Text = "&Delete";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.ForeColor = System.Drawing.Color.Maroon;
            this.lblStatus.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblStatus.Location = new System.Drawing.Point(4, 52);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(70, 20);
            this.lblStatus.TabIndex = 4;
            this.lblStatus.Text = "Activated";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblToDate
            // 
            this.lblToDate.ForeColor = System.Drawing.Color.Maroon;
            this.lblToDate.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblToDate.Location = new System.Drawing.Point(4, 30);
            this.lblToDate.Name = "lblToDate";
            this.lblToDate.Size = new System.Drawing.Size(70, 20);
            this.lblToDate.TabIndex = 2;
            this.lblToDate.Text = "To Date";
            this.lblToDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dtmToDate
            // 
            // 
            // 
            // 
            this.dtmToDate.Calendar.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.dtmToDate.CustomFormat = "dd-MM-yyyy";
            this.dtmToDate.EmptyAsNull = true;
            this.dtmToDate.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
            this.dtmToDate.Location = new System.Drawing.Point(82, 30);
            this.dtmToDate.Name = "dtmToDate";
            this.dtmToDate.Size = new System.Drawing.Size(112, 20);
            this.dtmToDate.TabIndex = 3;
            this.dtmToDate.Tag = null;
            this.dtmToDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.dtmToDate.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.DropDown;
            this.dtmToDate.Leave += new System.EventHandler(this.OnLeaveControl);
            this.dtmToDate.Enter += new System.EventHandler(this.OnEnterControl);
            // 
            // ckbStatus
            // 
            this.ckbStatus.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ckbStatus.Location = new System.Drawing.Point(82, 52);
            this.ckbStatus.Name = "ckbStatus";
            this.ckbStatus.Size = new System.Drawing.Size(16, 20);
            this.ckbStatus.TabIndex = 5;
            // 
            // lblFromDate
            // 
            this.lblFromDate.ForeColor = System.Drawing.Color.Maroon;
            this.lblFromDate.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblFromDate.Location = new System.Drawing.Point(4, 8);
            this.lblFromDate.Name = "lblFromDate";
            this.lblFromDate.Size = new System.Drawing.Size(70, 20);
            this.lblFromDate.TabIndex = 0;
            this.lblFromDate.Text = "From Date";
            this.lblFromDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dtmFromDate
            // 
            // 
            // 
            // 
            this.dtmFromDate.Calendar.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.dtmFromDate.CustomFormat = "dd-MM-yyyy";
            this.dtmFromDate.EmptyAsNull = true;
            this.dtmFromDate.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
            this.dtmFromDate.Location = new System.Drawing.Point(82, 8);
            this.dtmFromDate.Name = "dtmFromDate";
            this.dtmFromDate.Size = new System.Drawing.Size(112, 20);
            this.dtmFromDate.TabIndex = 1;
            this.dtmFromDate.Tag = null;
            this.dtmFromDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.dtmFromDate.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.DropDown;
            this.dtmFromDate.Leave += new System.EventHandler(this.OnLeaveControl);
            this.dtmFromDate.Enter += new System.EventHandler(this.OnEnterControl);
            // 
            // lblEndDayOfMonth
            // 
            this.lblEndDayOfMonth.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblEndDayOfMonth.Location = new System.Drawing.Point(264, 8);
            this.lblEndDayOfMonth.Name = "lblEndDayOfMonth";
            this.lblEndDayOfMonth.Size = new System.Drawing.Size(162, 16);
            this.lblEndDayOfMonth.TabIndex = 59;
            this.lblEndDayOfMonth.Text = "Last day of month";
            this.lblEndDayOfMonth.Visible = false;
            // 
            // lblFirstDayOfMonth
            // 
            this.lblFirstDayOfMonth.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblFirstDayOfMonth.Location = new System.Drawing.Point(266, 30);
            this.lblFirstDayOfMonth.Name = "lblFirstDayOfMonth";
            this.lblFirstDayOfMonth.Size = new System.Drawing.Size(162, 16);
            this.lblFirstDayOfMonth.TabIndex = 58;
            this.lblFirstDayOfMonth.Text = "First day of month";
            this.lblFirstDayOfMonth.Visible = false;
            // 
            // ManagePeriod
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(622, 361);
            this.Controls.Add(this.lblEndDayOfMonth);
            this.Controls.Add(this.lblFirstDayOfMonth);
            this.Controls.Add(this.dtmToDate);
            this.Controls.Add(this.dtmFromDate);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnHelp);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.ckbStatus);
            this.Controls.Add(this.gridPeriod);
            this.Controls.Add(this.lblToDate);
            this.Controls.Add(this.lblFromDate);
            this.KeyPreview = true;
            this.Name = "ManagePeriod";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Manage Period";
            this.Load += new System.EventHandler(this.ManagePeriod_Load);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.ManagePeriod_Closing);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ManagePeriod_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.gridPeriod)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtmToDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtmFromDate)).EndInit();
            this.ResumeLayout(false);

		}
		#endregion
				//**************************************************************************              
		///    <Description>
		///       ManagePeriod_Load
		///    </Description>
		///    <Inputs>
		///    </Inputs>
		///    <Outputs>
		///    </Outputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <Authors>
		///       Trada
		///    </Authors>
		///    <History>
		///       Thursday, May 5, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private void ManagePeriod_Load(object sender, System.EventArgs e)
		{
			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.WaitCursor;

			#endregion Code Inserted Automatically

			
			const string METHOD_NAME = THIS + ".ManagePeriod_Load()";
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
					// Code Inserted Automatically
#region Code Inserted Automatically
this.Cursor = Cursors.Default;
#endregion Code Inserted Automatically

					return;
				}
				enumAction = EnumAction.Default;
				dtmFromDate.Enabled = false;
				dtmToDate.Enabled = false;
				ckbStatus.Enabled = false;
				btnSave.Enabled = false;
				dtbGridLayout = FormControlComponents.StoreGridLayout(gridPeriod);
				BindDataToGrid();
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

			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.Default;

			#endregion Code Inserted Automatically

			
		}

		//**************************************************************************              
		///    <Description>
		///       btnClose_Click
		///    </Description>
		///    <Inputs>
		///    </Inputs>
		///    <Outputs>
		///    </Outputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <Authors>
		///       Trada
		///    </Authors>
		///    <History>
		///       Thursday, May 5, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
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
		//**************************************************************************              
		///    <Description>
		///       Bind data to grid
		///    </Description>
		///    <Inputs>
		///    </Inputs>
		///    <Outputs>
		///    </Outputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <Authors>
		///       Trada
		///    </Authors>
		///    <History>
		///       Thursday, May 5, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private void BindDataToGrid()
		{
			const string METHOD_NAME = THIS + ".BindDataToGrid()";
			try
			{
				ManagePeriodBO boManagePeriod = new ManagePeriodBO();
				dstPeriod = boManagePeriod.ListPeriod();
				gridPeriod.DataSource = dstPeriod.Tables[0];
				gridPeriod.Splits[0].DisplayColumns[Sys_PeriodTable.PERIODID_FLD].Visible = false;
				gridPeriod.Columns[Sys_PeriodTable.FROMDATE_FLD].NumberFormat = DATETIME_FOMART;
				gridPeriod.Splits[0].DisplayColumns[Sys_PeriodTable.FROMDATE_FLD].Locked = true;
				gridPeriod.Columns[Sys_PeriodTable.TODATE_FLD].NumberFormat = DATETIME_FOMART;
				gridPeriod.Splits[0].DisplayColumns[Sys_PeriodTable.TODATE_FLD].Locked = true;
				gridPeriod.Splits[0].DisplayColumns[Sys_PeriodTable.ACTIVATE_FLD].Locked = true;
				gridPeriod.Columns[Sys_PeriodTable.ACTIVATE_FLD].ValueItems.Presentation = PresentationEnum.CheckBox;
				//Align center caption of all column
				for (int i = 0; i < gridPeriod.Splits[0].DisplayColumns.Count; i++)
				{
					gridPeriod.Splits[0].DisplayColumns[i].HeadingStyle.HorizontalAlignment = AlignHorzEnum.Center;
				}
				FormControlComponents.RestoreGridLayout(gridPeriod, dtbGridLayout);
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
		//**************************************************************************              
		///    <Description>
		///       btnEdit_Click
		///    </Description>
		///    <Inputs>
		///    </Inputs>
		///    <Outputs>
		///    </Outputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <Authors>
		///       Trada
		///    </Authors>
		///    <History>
		///       Thursday, May 5, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private void btnEdit_Click(object sender, System.EventArgs e)
		{
			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.WaitCursor;

			#endregion Code Inserted Automatically

			
			const string METHOD_NAME = THIS + ".btnEdit_Click()";
			try
			{
				//Edit mode
				int intCountRow =0;
                for (int i = 0; i < gridPeriod.RowCount; i++)
				{
					if (gridPeriod[i, Sys_PeriodTable.FROMDATE_FLD].ToString() != string.Empty)
					{
						intCountRow++;
					}
				}
				if (intCountRow ==0)
				{
					PCSMessageBox.Show(ErrorCode.MESSAGE_PO_APPROVE_NO_DATA_IN_GRID);
					gridPeriod.Row = 0;
					gridPeriod.Col = gridPeriod.Splits[0].DisplayColumns.IndexOf(gridPeriod.Splits[0].DisplayColumns[Sys_PeriodTable.FROMDATE_FLD]);
					gridPeriod.Focus();
					// Code Inserted Automatically
					#region Code Inserted Automatically
this.Cursor = Cursors.Default;
#endregion Code Inserted Automatically

					return;
				}
				//added by duongna (21-Sep-2005)
				if (gridPeriod.VisibleRows == 0)
				{
					// Code Inserted Automatically
					#region Code Inserted Automatically
this.Cursor = Cursors.Default;
#endregion Code Inserted Automatically

					return;
				}
				
				btnAdd.Enabled = false;
				enumAction = EnumAction.Edit;
				int pintSelectedRow = gridPeriod.Row;
				dtmFromDate.Enabled = true;
				dtmToDate.Enabled = true;
				btnAdd.Enabled = false;
				btnSave.Enabled = true;
				btnDelete.Enabled = false;
				ckbStatus.Enabled = true;

				dtmFromDate.Value = DateTime.Parse(gridPeriod[pintSelectedRow, Sys_PeriodTable.FROMDATE_FLD].ToString());
				dtmToDate.Value = DateTime.Parse(gridPeriod[pintSelectedRow, Sys_PeriodTable.TODATE_FLD].ToString());
				ckbStatus.Checked = (bool)gridPeriod[pintSelectedRow, Sys_PeriodTable.ACTIVATE_FLD];
				btnEdit.Enabled = false;

				dtmFromDate.Focus();
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
			
				

			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.Default;

			#endregion Code Inserted Automatically

			
		}
		//**************************************************************************              
		///    <Description>
		///       btnAdd_Click
		///    </Description>
		///    <Inputs>
		///    </Inputs>
		///    <Outputs>
		///    </Outputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <Authors>
		///       Trada
		///    </Authors>
		///    <History>
		///       Thursday, May 5, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private void btnAdd_Click(object sender, System.EventArgs e)
		{
			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.WaitCursor;

			#endregion Code Inserted Automatically

			
			const string METHOD_NAME = THIS + ".btnAdd_Click()";
			try
			{
				dtmFromDate.Enabled = true;
				dtmToDate.Enabled = true;
				ckbStatus.Enabled = true;
				btnSave.Enabled = true;
				btnDelete.Enabled = false;
				btnAdd.Enabled = false;
				btnEdit.Enabled = false;
				//Add mode	
			
				dtmFromDate.Focus();

				enumAction = EnumAction.Add;
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

			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.Default;

			#endregion Code Inserted Automatically

			
		}
		//**************************************************************************              
		///    <Description>
		///       btnSave_Click
		///    </Description>
		///    <Inputs>
		///    </Inputs>
		///    <Outputs>
		///    </Outputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <Authors>
		///       Trada
		///    </Authors>
		///    <History>
		///       Thursday, May 5, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private void btnSave_Click(object sender, System.EventArgs e)
		{
			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.WaitCursor;

			#endregion Code Inserted Automatically

			
			const string METHOD_NAME = THIS + ".btnSave_Click()";
			try
			{
				blnSaveOK = false;
				if (FormControlComponents.CheckMandatory(dtmFromDate))
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID);
					dtmFromDate.Focus();
					dtmFromDate.Select();
					// Code Inserted Automatically
					#region Code Inserted Automatically
					this.Cursor = Cursors.Default;
					#endregion Code Inserted Automatically

					return;
				}
				if (FormControlComponents.CheckMandatory(dtmToDate))
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID);
					dtmToDate.Focus();
					dtmToDate.Select();
					// Code Inserted Automatically
					#region Code Inserted Automatically
					this.Cursor = Cursors.Default;
					#endregion Code Inserted Automatically

					return;
				}

				//Check from date
				if(dtmFromDate.ValueIsDbNull  || dtmFromDate.Text.Trim() == string.Empty)
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Exclamation);
					dtmFromDate.Focus();				
					return;
				}				
			
				if(((DateTime)dtmFromDate.Value).Day != 1)
				{
					string[] arrMessage = new string[2];
					arrMessage[0] = lblFirstDayOfMonth.Text + " (1)";
					arrMessage[1] = lblFromDate.Text;

					PCSMessageBox.Show(ErrorCode.MESSAGE_RELATION_REQUIRE, MessageBoxIcon.Exclamation, arrMessage);
					dtmFromDate.Focus();				
					return;
				}

				//Check to date
				if (dtmToDate.ValueIsDbNull || dtmToDate.Text.Trim() == string.Empty)
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Exclamation);
					dtmToDate.Focus();				
					return;
				}
			
				if(((DateTime)dtmToDate.Value).Date != ((DateTime)dtmFromDate.Value).Date.AddMonths(1).AddDays(-1))
				{
					string[] arrMessage = new string[2];
					arrMessage[0] = lblEndDayOfMonth.Text + " (" + DateTime.DaysInMonth(((DateTime)dtmToDate.Value).Year, ((DateTime)dtmToDate.Value).Month) + ")";
					arrMessage[1] = lblToDate.Text;

					PCSMessageBox.Show(ErrorCode.MESSAGE_RELATION_REQUIRE, MessageBoxIcon.Exclamation, arrMessage);
					dtmToDate.Focus();				
					return;
				}

				//Check if date is invalid
				if(DateTime.Parse(dtmFromDate.Value.ToString()) > DateTime.Parse(dtmToDate.Value.ToString()))
				{
					string[] arrMessage = new string[2];
					arrMessage[0] = lblToDate.Text;
					arrMessage[1] = lblFromDate.Text;

					PCSMessageBox.Show(ErrorCode.MESSAGE_GREATER_THAN, MessageBoxIcon.Exclamation, arrMessage);
					dtmToDate.Focus();				
					return;
				}
				ManagePeriodBO boPeriod = new ManagePeriodBO();
				voPeriod.Activate = ckbStatus.Checked;
				voPeriod.FromDate = Convert.ToDateTime(dtmFromDate.Value);
				voPeriod.ToDate = Convert.ToDateTime(dtmToDate.Value);
				if (enumAction == EnumAction.Edit)
				{
					int pintSelectedRow = gridPeriod.Row;
					voPeriod.PeriodID = int.Parse(gridPeriod[pintSelectedRow, Sys_PeriodTable.PERIODID_FLD].ToString());
				}
				if(boPeriod.IsPeriodOverlap(voPeriod))
				{
					PCSMessageBox.Show(ErrorCode.MESSAGE_DATE_IS_OVERLAP, MessageBoxIcon.Exclamation);					
					dtmFromDate.Focus();				
					return;
				}
				switch (enumAction)
				{
					case EnumAction.Add:
						voPeriod.PeriodID = boPeriod.AddAndReturnID(voPeriod);
						break;
					case EnumAction.Edit:
						boPeriod.Update(voPeriod);
						break;
				}
				
				enumAction = EnumAction.Default;
				BindDataToGrid();
				dtmFromDate.Enabled = false;
				dtmToDate.Enabled = false;
				ckbStatus.Enabled = false;
				btnAdd.Enabled = true;
				btnEdit.Enabled = true;
				btnSave.Enabled = false;
				btnDelete.Enabled = true;

				dtmFromDate.Value = DBNull.Value;
				dtmToDate.Value = DBNull.Value;
				ckbStatus.Checked = false;
				PCSMessageBox.Show(ErrorCode.MESSAGE_AFTER_SAVE_DATA);
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
			finally
			{
				this.Cursor = Cursors.Default;
			}
		}
		//**************************************************************************              
		///    <Description>
		///      btnDelete_Click
		///    </Description>
		///    <Inputs>
		///    fromDate, toDate
		///    </Inputs>
		///    <Outputs>
		///    </Outputs>
		///    <Returns>
		///       bool
		///    </Returns>
		///    <Authors>
		///       Trada
		///    </Authors>
		///    <History>
		///       Thursday, May 5, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private void btnDelete_Click(object sender, System.EventArgs e)
		{
			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.WaitCursor;

			#endregion Code Inserted Automatically

			
			const string METHOD_NAME = THIS + ".btnDelete_Click()";
			try
			{
				int intCountRow =0;
                for (int i = 0; i < gridPeriod.RowCount; i++)
				{
					if (gridPeriod[i, Sys_PeriodTable.FROMDATE_FLD].ToString() != string.Empty)
					{
						intCountRow++;
					}
				}
				if (intCountRow ==0)
				{
					PCSMessageBox.Show(ErrorCode.MESSAGE_PO_APPROVE_NO_DATA_IN_GRID);
					gridPeriod.Row = 0;
					gridPeriod.Col = gridPeriod.Splits[0].DisplayColumns.IndexOf(gridPeriod.Splits[0].DisplayColumns[Sys_PeriodTable.FROMDATE_FLD]);
					gridPeriod.Focus();
					// Code Inserted Automatically
#region Code Inserted Automatically
this.Cursor = Cursors.Default;
#endregion Code Inserted Automatically

					return;
				}
				int pintSelectedRow = gridPeriod.Row;
				int pintPeriodID = int.Parse(gridPeriod[pintSelectedRow, Sys_PeriodTable.PERIODID_FLD].ToString());
				ManagePeriodBO boManagePeriod = new ManagePeriodBO();
				if ((bool)gridPeriod[pintSelectedRow, Sys_PeriodTable.ACTIVATE_FLD])
				{
					PCSMessageBox.Show(ErrorCode.MESSAGE_MP_CAN_NOT_DEL_ROW, MessageBoxIcon.Error);
				}
				else
				{
					boManagePeriod.Delete(pintPeriodID);
					BindDataToGrid();
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

			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.Default;

			#endregion Code Inserted Automatically

			
		}

		//**************************************************************************              
		///    <Description>
		///      ManagePeriod_Closing
		///    </Description>
		///    <Inputs>
		///    fromDate, toDate
		///    </Inputs>
		///    <Outputs>
		///    </Outputs>
		///    <Returns>
		///       bool
		///    </Returns>
		///    <Authors>
		///       Trada
		///    </Authors>
		///    <History>
		///       Thursday, May 5, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private void ManagePeriod_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".ManagePeriod_Closing()";
			try
			{
				//Add mode
				if (enumAction == EnumAction.Add)
				{
					if ((FormControlComponents.CheckMandatory(dtmFromDate))&&(FormControlComponents.CheckMandatory(dtmToDate)))
					{
						return;
					}
					else
					{		
						System.Windows.Forms.DialogResult dlgResult = PCSMessageBox.Show(ErrorCode.MESSAGE_QUESTION_STORE_INTO_DATABASE, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
						switch (dlgResult)
						{
							case DialogResult.Yes:
								btnSave_Click(null,null);
								if (blnSaveOK)
								{
									e.Cancel = false;
								}
								else
								{
									e.Cancel = true;
								}
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
				//Edit Mode
				if (enumAction == EnumAction.Edit)
				{
					if ((FormControlComponents.CheckMandatory(dtmFromDate))&&(FormControlComponents.CheckMandatory(dtmToDate)))
					{
						return;
					}
					else
					{	
						System.Windows.Forms.DialogResult dlgResult = PCSMessageBox.Show(ErrorCode.MESSAGE_QUESTION_STORE_INTO_DATABASE, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
						switch (dlgResult)
						{
							case DialogResult.Yes:
								btnSave_Click(null,null);
								if (blnSaveOK)
								{
									e.Cancel = false;
								}
								else
								{
									e.Cancel = true;
								}
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

		/// <summary>
		/// ManagePeriod_KeyDown
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Friday, September 30 2005</date>
		private void ManagePeriod_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Escape)
			{
				this.Close();
			}
		}
		/// <summary>
		/// OnEnterControl
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Friday, September 30 2005</date>
		private void OnEnterControl(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".OnEnterControl()";
			try
			{
				FormControlComponents.OnEnterControl(sender, e);
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
		/// OnLeaveControl
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Thursday, September 1 2005</date>
		private void OnLeaveControl(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".OnLeaveControl()";
			try
			{
				FormControlComponents.OnLeaveControl(sender, e);
			}
			catch (PCSException ex)
			{
				// displays the error message.
				PCSMessageBox.Show(ex.mCode, MessageBoxIcon.Error);
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
	}
}
