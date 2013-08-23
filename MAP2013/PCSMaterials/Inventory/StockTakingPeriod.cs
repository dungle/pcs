using System;
using System.ComponentModel;
using System.Data;
using System.Threading;
using System.Windows.Forms;
using C1.Win.C1Input;
using C1.Win.C1List;
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
	/// Summary description for StockTakingPeriod.
	/// </summary>
	public class StockTakingPeriod : Form
	{
		private C1Combo cboCCN;
		private Label lblCCN;
		private Button btnAdd;
		private Button btnSave;
		private Button btnClose;
		private Button btnHelp;
		private C1DateEdit dtmToDate;
		private Label lblToDate;
		private C1DateEdit dtmFromDate;
		private Label lblFromDate;
		private TextBox txtStockTakingPeriod;
		private Button btnStockTakingPeriod;
		private Label lblStockTakingPeriod;
		private Button btnDelete;
		private Button btnEdit;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private Container components = null;
		const string THIS = "PCSMaterials.Inventory.StockTakingPeriod";
		UtilsBO boUtil = new UtilsBO();
		EnumAction formMode;
		private Button btnUpdate;
		private CheckBox chkClose;
		private bool blnHasError = false;
		private bool IsUpdateInventory = false;
		private System.Windows.Forms.Button btnUpdateDiff;
        private Button CloseStockButton;
        private Button UpdateBeginButton;
		Thread thrProcess = null;
		public StockTakingPeriod()
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StockTakingPeriod));
            this.cboCCN = new C1.Win.C1List.C1Combo();
            this.lblCCN = new System.Windows.Forms.Label();
            this.dtmToDate = new C1.Win.C1Input.C1DateEdit();
            this.lblToDate = new System.Windows.Forms.Label();
            this.dtmFromDate = new C1.Win.C1Input.C1DateEdit();
            this.lblFromDate = new System.Windows.Forms.Label();
            this.txtStockTakingPeriod = new System.Windows.Forms.TextBox();
            this.btnStockTakingPeriod = new System.Windows.Forms.Button();
            this.lblStockTakingPeriod = new System.Windows.Forms.Label();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnHelp = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.chkClose = new System.Windows.Forms.CheckBox();
            this.btnUpdateDiff = new System.Windows.Forms.Button();
            this.CloseStockButton = new System.Windows.Forms.Button();
            this.UpdateBeginButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.cboCCN)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtmToDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtmFromDate)).BeginInit();
            this.SuspendLayout();
            // 
            // cboCCN
            // 
            this.cboCCN.AddItemSeparator = ';';
            this.cboCCN.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cboCCN.Caption = "";
            this.cboCCN.CaptionHeight = 17;
            this.cboCCN.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.cboCCN.ColumnCaptionHeight = 17;
            this.cboCCN.ColumnFooterHeight = 17;
            this.cboCCN.ComboStyle = C1.Win.C1List.ComboStyleEnum.DropdownList;
            this.cboCCN.ContentHeight = 15;
            this.cboCCN.DeadAreaBackColor = System.Drawing.Color.Empty;
            this.cboCCN.EditorBackColor = System.Drawing.SystemColors.Window;
            this.cboCCN.EditorFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboCCN.EditorForeColor = System.Drawing.SystemColors.WindowText;
            this.cboCCN.EditorHeight = 15;
            this.cboCCN.Images.Add(((System.Drawing.Image)(resources.GetObject("cboCCN.Images"))));
            this.cboCCN.ItemHeight = 15;
            this.cboCCN.Location = new System.Drawing.Point(378, 8);
            this.cboCCN.MatchEntryTimeout = ((long)(2000));
            this.cboCCN.MaxDropDownItems = ((short)(5));
            this.cboCCN.MaxLength = 32767;
            this.cboCCN.MouseCursor = System.Windows.Forms.Cursors.Default;
            this.cboCCN.Name = "cboCCN";
            this.cboCCN.RowSubDividerColor = System.Drawing.Color.DarkGray;
            this.cboCCN.Size = new System.Drawing.Size(80, 21);
            this.cboCCN.TabIndex = 1;
            this.cboCCN.PropBag = resources.GetString("cboCCN.PropBag");
            // 
            // lblCCN
            // 
            this.lblCCN.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCCN.ForeColor = System.Drawing.Color.Maroon;
            this.lblCCN.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblCCN.Location = new System.Drawing.Point(346, 8);
            this.lblCCN.Name = "lblCCN";
            this.lblCCN.Size = new System.Drawing.Size(30, 20);
            this.lblCCN.TabIndex = 0;
            this.lblCCN.Text = "CCN";
            this.lblCCN.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
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
            this.dtmToDate.Location = new System.Drawing.Point(120, 56);
            this.dtmToDate.Name = "dtmToDate";
            this.dtmToDate.PostValidation.Intervals.AddRange(new C1.Win.C1Input.ValueInterval[] {
            new C1.Win.C1Input.ValueInterval(new System.DateTime(1753, 1, 1, 0, 0, 0, 0), new System.DateTime(2100, 12, 31, 12, 0, 0, 0), true, true)});
            this.dtmToDate.Size = new System.Drawing.Size(122, 20);
            this.dtmToDate.TabIndex = 8;
            this.dtmToDate.Tag = null;
            this.dtmToDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.dtmToDate.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.DropDown;
            // 
            // lblToDate
            // 
            this.lblToDate.ForeColor = System.Drawing.Color.Maroon;
            this.lblToDate.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblToDate.Location = new System.Drawing.Point(8, 56);
            this.lblToDate.Name = "lblToDate";
            this.lblToDate.Size = new System.Drawing.Size(84, 20);
            this.lblToDate.TabIndex = 7;
            this.lblToDate.Text = "To date";
            this.lblToDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
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
            this.dtmFromDate.Location = new System.Drawing.Point(120, 32);
            this.dtmFromDate.Name = "dtmFromDate";
            this.dtmFromDate.PostValidation.Intervals.AddRange(new C1.Win.C1Input.ValueInterval[] {
            new C1.Win.C1Input.ValueInterval(new System.DateTime(1753, 1, 1, 0, 0, 0, 0), new System.DateTime(2100, 12, 31, 12, 0, 0, 0), true, true)});
            this.dtmFromDate.Size = new System.Drawing.Size(122, 20);
            this.dtmFromDate.TabIndex = 6;
            this.dtmFromDate.Tag = null;
            this.dtmFromDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.dtmFromDate.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.DropDown;
            // 
            // lblFromDate
            // 
            this.lblFromDate.ForeColor = System.Drawing.Color.Maroon;
            this.lblFromDate.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblFromDate.Location = new System.Drawing.Point(8, 32);
            this.lblFromDate.Name = "lblFromDate";
            this.lblFromDate.Size = new System.Drawing.Size(84, 20);
            this.lblFromDate.TabIndex = 5;
            this.lblFromDate.Text = "From date";
            this.lblFromDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtStockTakingPeriod
            // 
            this.txtStockTakingPeriod.Location = new System.Drawing.Point(120, 8);
            this.txtStockTakingPeriod.Name = "txtStockTakingPeriod";
            this.txtStockTakingPeriod.Size = new System.Drawing.Size(140, 20);
            this.txtStockTakingPeriod.TabIndex = 3;
            this.txtStockTakingPeriod.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtStockTakingPeriod_KeyDown);
            this.txtStockTakingPeriod.Validating += new System.ComponentModel.CancelEventHandler(this.txtStockTakingPeriod_Validating);
            // 
            // btnStockTakingPeriod
            // 
            this.btnStockTakingPeriod.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnStockTakingPeriod.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnStockTakingPeriod.Location = new System.Drawing.Point(261, 8);
            this.btnStockTakingPeriod.Name = "btnStockTakingPeriod";
            this.btnStockTakingPeriod.Size = new System.Drawing.Size(22, 20);
            this.btnStockTakingPeriod.TabIndex = 4;
            this.btnStockTakingPeriod.Text = "...";
            this.btnStockTakingPeriod.Click += new System.EventHandler(this.btnStockTakingPeriod_Click);
            // 
            // lblStockTakingPeriod
            // 
            this.lblStockTakingPeriod.ForeColor = System.Drawing.Color.Maroon;
            this.lblStockTakingPeriod.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblStockTakingPeriod.Location = new System.Drawing.Point(8, 8);
            this.lblStockTakingPeriod.Name = "lblStockTakingPeriod";
            this.lblStockTakingPeriod.Size = new System.Drawing.Size(104, 20);
            this.lblStockTakingPeriod.TabIndex = 2;
            this.lblStockTakingPeriod.Text = "Stock taking period";
            this.lblStockTakingPeriod.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnAdd
            // 
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnAdd.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnAdd.Location = new System.Drawing.Point(8, 120);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(60, 23);
            this.btnAdd.TabIndex = 13;
            this.btnAdd.Text = "&Add";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnSave
            // 
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnSave.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnSave.Location = new System.Drawing.Point(68, 120);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(60, 23);
            this.btnSave.TabIndex = 14;
            this.btnSave.Text = "&Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnClose.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnClose.Location = new System.Drawing.Point(398, 120);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(60, 23);
            this.btnClose.TabIndex = 18;
            this.btnClose.Text = "&Close";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnHelp
            // 
            this.btnHelp.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnHelp.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnHelp.Location = new System.Drawing.Point(334, 120);
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Size = new System.Drawing.Size(60, 23);
            this.btnHelp.TabIndex = 17;
            this.btnHelp.Text = "&Help";
            // 
            // btnDelete
            // 
            this.btnDelete.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnDelete.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnDelete.Location = new System.Drawing.Point(188, 120);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(60, 23);
            this.btnDelete.TabIndex = 16;
            this.btnDelete.Text = "&Delete";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnEdit.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnEdit.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnEdit.Location = new System.Drawing.Point(128, 120);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(60, 23);
            this.btnEdit.TabIndex = 15;
            this.btnEdit.Text = "&Edit";
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(8, 88);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(120, 23);
            this.btnUpdate.TabIndex = 10;
            this.btnUpdate.Text = "&Update Inventory";
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // chkClose
            // 
            this.chkClose.Location = new System.Drawing.Point(264, 56);
            this.chkClose.Name = "chkClose";
            this.chkClose.Size = new System.Drawing.Size(56, 16);
            this.chkClose.TabIndex = 9;
            this.chkClose.Text = "Close";
            // 
            // btnUpdateDiff
            // 
            this.btnUpdateDiff.Location = new System.Drawing.Point(130, 88);
            this.btnUpdateDiff.Name = "btnUpdateDiff";
            this.btnUpdateDiff.Size = new System.Drawing.Size(109, 23);
            this.btnUpdateDiff.TabIndex = 11;
            this.btnUpdateDiff.Text = "Update D&ifferent";
            this.btnUpdateDiff.Click += new System.EventHandler(this.btnUpdateDiff_Click);
            // 
            // CloseStockButton
            // 
            this.CloseStockButton.Location = new System.Drawing.Point(338, 88);
            this.CloseStockButton.Name = "CloseStockButton";
            this.CloseStockButton.Size = new System.Drawing.Size(120, 23);
            this.CloseStockButton.TabIndex = 12;
            this.CloseStockButton.Text = "Close Stock &Taking";
            this.CloseStockButton.Click += new System.EventHandler(this.CloseStockButton_Click);
            // 
            // UpdateBeginButton
            // 
            this.UpdateBeginButton.Location = new System.Drawing.Point(245, 88);
            this.UpdateBeginButton.Name = "UpdateBeginButton";
            this.UpdateBeginButton.Size = new System.Drawing.Size(87, 23);
            this.UpdateBeginButton.TabIndex = 11;
            this.UpdateBeginButton.Text = "Update &Begin";
            this.UpdateBeginButton.Click += new System.EventHandler(this.UpdateBeginButton_Click);
            // 
            // StockTakingPeriod
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(466, 150);
            this.Controls.Add(this.UpdateBeginButton);
            this.Controls.Add(this.btnUpdateDiff);
            this.Controls.Add(this.chkClose);
            this.Controls.Add(this.CloseStockButton);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnHelp);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.cboCCN);
            this.Controls.Add(this.lblCCN);
            this.Controls.Add(this.dtmToDate);
            this.Controls.Add(this.lblToDate);
            this.Controls.Add(this.dtmFromDate);
            this.Controls.Add(this.lblFromDate);
            this.Controls.Add(this.txtStockTakingPeriod);
            this.Controls.Add(this.btnStockTakingPeriod);
            this.Controls.Add(this.lblStockTakingPeriod);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "StockTakingPeriod";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Stock Taking Period";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.StockTakingPeriod_Closing);
            this.Load += new System.EventHandler(this.StockTakingPeriod_Load);
            ((System.ComponentModel.ISupportInitialize)(this.cboCCN)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtmToDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtmFromDate)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		private void btnClose_Click(object sender, EventArgs e)
		{
			this.Close();
		}
		/// <summary>
		/// StockTakingPeriod_Load
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Tuesday, July 25 2006</date>
		private void StockTakingPeriod_Load(object sender, EventArgs e)
		{
			const string METHOD_NAME = THIS + ".StockTakingPeriod_Load()";
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
				
				dtmFromDate.CustomFormat = Constants.DATETIME_FORMAT_HOUR;	
				dtmToDate.CustomFormat = Constants.DATETIME_FORMAT_HOUR;	
				
				//switch form mode
				formMode = EnumAction.Default;
				SwitchFormMode();
				btnUpdate.Enabled = false;
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
		/// SwitchFormMode
		/// </summary>
		/// <author>Trada</author>
		/// <date>Tuesday, July 25 2006</date>
		private void SwitchFormMode()
		{
			switch (formMode)
			{
				case EnumAction.Default:
					btnStockTakingPeriod.Enabled = true;
					txtStockTakingPeriod.Enabled = true;
					dtmFromDate.Enabled = false;
					dtmToDate.Enabled = false;
					btnAdd.Enabled = true;	
					btnSave.Enabled = false;
					btnEdit.Enabled = false;
					btnDelete.Enabled = false;
					chkClose.Enabled = false;
					if (txtStockTakingPeriod.Tag != null)
					{
						btnUpdate.Enabled = true;
						btnUpdateDiff.Enabled = true;
					}
					else
					{
						btnUpdate.Enabled = false;
						btnUpdateDiff.Enabled = false;
					}
					break;
				case EnumAction.Add:
					btnStockTakingPeriod.Enabled = false;
					txtStockTakingPeriod.Enabled = true;
					dtmFromDate.Enabled = true;
					dtmToDate.Enabled = true;
					btnAdd.Enabled = false;	
					btnSave.Enabled = true;
					btnEdit.Enabled = false;
					btnDelete.Enabled = false;
					btnUpdate.Enabled = false;
					btnUpdateDiff.Enabled = false;
					chkClose.Enabled = true;
					break;
				case EnumAction.Edit:
					btnStockTakingPeriod.Enabled = false;
					txtStockTakingPeriod.Enabled = true;
					dtmFromDate.Enabled = true;
					dtmToDate.Enabled = true;
					btnAdd.Enabled = false;	
					btnSave.Enabled = true;
					btnEdit.Enabled = false;
					btnDelete.Enabled = false;
					chkClose.Enabled = true;
					btnUpdate.Enabled = false;
					btnUpdateDiff.Enabled = false;
					break;
			}
		}
		/// <summary>
		/// Clear all control in form
		/// </summary>
		/// <author>Trada</author>
		/// <date>Tuesday, July 25 2006</date>
		private void ClearForm()
		{
			txtStockTakingPeriod.Text = string.Empty;
			txtStockTakingPeriod.Tag = null;
			dtmFromDate.Value = null;
			dtmToDate.Value = null;
			chkClose.Checked = false;				
		}
		/// <summary>
		/// btnAdd_Click
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Tuesday, July 25 2006</date>
		private void btnAdd_Click(object sender, EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnAdd_Click()";
			try
			{
				//Clear form
				ClearForm();
				//Switch form Mode
				formMode = EnumAction.Add;
				SwitchFormMode();
				txtStockTakingPeriod.Focus();
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
		/// Validating Data before saving
		/// </summary>
		/// <returns></returns>
		/// <author>Trada</author>
		/// <date>Tuesday, July 25 2006</date>
		private bool IsValidatingData()
		{
			if (FormControlComponents.CheckMandatory(txtStockTakingPeriod))
			{
				PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Warning);
				txtStockTakingPeriod.Focus();
				return false;
			}
			if (FormControlComponents.CheckMandatory(dtmFromDate))
			{
				PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Warning);
				dtmFromDate.Focus();
				return false;
			}
			if (FormControlComponents.CheckMandatory(dtmToDate))
			{
				PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Warning);
				dtmToDate.Focus();
				return false;
			}
			if (dtmFromDate.Value != DBNull.Value && dtmToDate.Value != DBNull.Value)
			{
				if ((DateTime)dtmFromDate.Value >= (DateTime) dtmToDate.Value)
				{
					PCSMessageBox.Show(ErrorCode.MESSAGE_MP_PERIODDATE, MessageBoxIcon.Warning);
					dtmToDate.Focus();
					return false;
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
		private void btnSave_Click(object sender, EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnSave_Click()";
			try
			{
				blnHasError = true;
				//Validating data
				if (IsValidatingData())
				{
					//Controls to VO
					IV_StockTakingPeriodVO voStockTakingPeriod = new IV_StockTakingPeriodVO();
					voStockTakingPeriod.CCNID = int.Parse(cboCCN.SelectedValue.ToString());
					voStockTakingPeriod.Description = txtStockTakingPeriod.Text;
					voStockTakingPeriod.FromDate = (DateTime)dtmFromDate.Value;
					voStockTakingPeriod.ToDate = (DateTime)dtmToDate.Value;
					voStockTakingPeriod.Closed = chkClose.Checked;
					//add to database
					StockTakingPeriodBO boStockTakingPeriod = new StockTakingPeriodBO();
					switch (formMode)
					{
						case EnumAction.Add:
							txtStockTakingPeriod.Tag = voStockTakingPeriod.StockTakingPeriodID = boStockTakingPeriod.AddAndReturnID(voStockTakingPeriod);
							break;
						case EnumAction.Edit:
							//add ID
							voStockTakingPeriod.StockTakingPeriodID = int.Parse(txtStockTakingPeriod.Tag.ToString());
							boStockTakingPeriod.Update(voStockTakingPeriod);
							break;
					}
					PCSMessageBox.Show(ErrorCode.MESSAGE_AFTER_SAVE_DATA);
					btnSave.Enabled = false;
					//ClearForm();
					formMode = EnumAction.Default;
					blnHasError = false;
					SwitchFormMode();
					btnAdd.Focus();
					btnAdd.Enabled = true;
					btnEdit.Enabled = true;
					btnDelete.Enabled = true;
					
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
		/// btnEdit_Click
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Tuesday, July 25 2006</date>
		private void btnEdit_Click(object sender, EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnEdit_Click()";
			try
			{
				//Switch form Mode
				formMode = EnumAction.Edit;
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
		/// fill data to all controls by StockTakingPeriodID
		/// </summary>
		/// <author>Trada</author>
		/// <date>Tuesday, July 25 2006</date>
		private void FillDataToControls(DataRowView pdrwResult)
		{
			dtmFromDate.Value = (DateTime)pdrwResult[IV_StockTakingPeriodTable.FROMDATE_FLD];
			dtmToDate.Value = (DateTime)pdrwResult[IV_StockTakingPeriodTable.TODATE_FLD];
		}
		/// <summary>
		/// btnStockTakingPeriod_Click
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Tuesday, July 25 2006</date>
		private void btnStockTakingPeriod_Click(object sender, EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnStockTakingPeriod_Click()";
			try
			{
				DataRowView drwResult = null;
				drwResult = FormControlComponents.OpenSearchForm(IV_StockTakingPeriodTable.TABLE_NAME, IV_StockTakingPeriodTable.DESCRIPTION_FLD, txtStockTakingPeriod.Text.Trim(), null, true);
				if (drwResult != null)
				{
					btnEdit.Enabled = true;
					btnDelete.Enabled = true;
					btnUpdate.Enabled = true;
					btnUpdateDiff.Enabled = true;
					txtStockTakingPeriod.Text = drwResult[IV_StockTakingPeriodTable.DESCRIPTION_FLD].ToString();
					txtStockTakingPeriod.Tag = drwResult[IV_StockTakingPeriodTable.STOCKTAKINGPERIODID_FLD];
					chkClose.Checked = (bool) drwResult[IV_StockTakingPeriodTable.CLOSED_FLD];
					FillDataToControls(drwResult);
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
		/// <date>Tuesday, July 25 2006</date>
		private void txtStockTakingPeriod_Validating(object sender, CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtStockTakingPeriod_Validating()";
			try
			{
				if (btnStockTakingPeriod.Enabled)
				{
					if (!txtStockTakingPeriod.Modified)
						return;
					if (txtStockTakingPeriod.Text == string.Empty)
					{
						dtmFromDate.Value = null;
						dtmToDate.Value = null;
						btnEdit.Enabled = false;
						btnDelete.Enabled = false;
						btnUpdate.Enabled = false;
						chkClose.Checked = false;
						return;
					}
					DataRowView drwResult = null;
					drwResult = FormControlComponents.OpenSearchForm(IV_StockTakingPeriodTable.TABLE_NAME, IV_StockTakingPeriodTable.DESCRIPTION_FLD, txtStockTakingPeriod.Text.Trim(), null, false);
					if (drwResult != null)
					{
						btnEdit.Enabled = true;
						btnDelete.Enabled = true;
						if (drwResult[IV_StockTakingPeriodTable.STOCKTAKINGDATE_FLD].ToString() != string.Empty)
						{
							btnUpdate.Enabled = true;
						}
						else
							btnUpdate.Enabled = false;
						txtStockTakingPeriod.Text = drwResult[IV_StockTakingPeriodTable.DESCRIPTION_FLD].ToString();
						txtStockTakingPeriod.Tag = drwResult[IV_StockTakingPeriodTable.STOCKTAKINGPERIODID_FLD];
						chkClose.Checked = (bool) drwResult[IV_StockTakingPeriodTable.CLOSED_FLD];
						FillDataToControls(drwResult);
					}
					else
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
		/// <summary>
		/// txtStockTakingPeriod_KeyDown
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Tuesday, July 25 2006</date>
		private void txtStockTakingPeriod_KeyDown(object sender, KeyEventArgs e)
		{
			if (btnStockTakingPeriod.Enabled && e.KeyCode == Keys.F4)
			{
				btnStockTakingPeriod_Click(null, null);
			}
		}
		
		/// <summary>
		/// btnDelete_Click
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Tuesday, July 25 2006</date>
		private void btnDelete_Click(object sender, EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnDelete_Click()";
			try
			{
				if (PCSMessageBox.Show(ErrorCode.MESSAGE_DELETE_RECORD, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
				{
					StockTakingPeriodBO boStockTakingPeriod = new StockTakingPeriodBO();
					//check before delete
					if (boStockTakingPeriod.CheckIfDataWasUsed(int.Parse(txtStockTakingPeriod.Tag.ToString())))
					{
						//delete 
						boStockTakingPeriod.DeleteByID(int.Parse(txtStockTakingPeriod.Tag.ToString()));
						formMode = EnumAction.Default;
						ClearForm();
						SwitchFormMode();
					}
					else
					{
						string[] strParam = new string[1];
						strParam[0] = " Stock Taking Period because this Period was used to setup Stock Taking";
						PCSMessageBox.Show(ErrorCode.MESSAGE_CAN_NOT_DELETE, MessageBoxIcon.Warning, strParam);
						return;
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
		/// StockTakingPeriod_Closing
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Tuesday, July 25 2006</date>
		private void StockTakingPeriod_Closing(object sender, CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".StockTakingPeriod_Closing()";
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
				else if (thrProcess != null)
				{
					if (thrProcess.IsAlive || thrProcess.ThreadState == ThreadState.Running)
					{
						string[] strMsg = {btnUpdate.Text.Replace("&", string.Empty)};
						DialogResult dlgResult = PCSMessageBox.Show(ErrorCode.MESSAGE_PROCESS_IS_RUNNING, MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2, strMsg);
						switch (dlgResult)
						{
							case DialogResult.OK:
								// try to stop the thread
								try
								{
									thrProcess.Abort();
								}
								catch
								{
									e.Cancel = false;
								}
								break;
							case DialogResult.Cancel:
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
		/// When Update Onhand button pressed, 
		/// we will calculate different between stock taking and current cache.
		/// After that, need to generate adjustment transaction
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>DungLA</author>
		/// <date>22-12-2006</date>
		private void btnUpdate_Click(object sender, EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnUpdate_Click()";
			try
			{
				if (cboCCN.SelectedValue.Equals(null))
				{
					PCSMessageBox.Show(ErrorCode.MESSAGE_RGA_CCN, MessageBoxIcon.Warning);
					cboCCN.Focus();
					return;
				}
				StockTakingPeriodBO boPeriod = new StockTakingPeriodBO();
				if (txtStockTakingPeriod.Tag == null)
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Warning);
					txtStockTakingPeriod.Focus();
					return;
				}
				// try to get period
				try
				{
					IV_StockTakingPeriodVO voPeriod = (IV_StockTakingPeriodVO)boPeriod.GetObjectVO(Convert.ToInt32(txtStockTakingPeriod.Tag), string.Empty);
					if (voPeriod.StockTakingPeriodID <= 0)
						throw new Exception();
				}
				catch
				{
					string[] strMessage = new string[1];
					strMessage[0] = lblStockTakingPeriod.Text;
					PCSMessageBox.Show(ErrorCode.MESSAGE_SELECTION_NOT_EXIST, MessageBoxIcon.Error, strMessage);
					// reset data
					txtStockTakingPeriod.Text = string.Empty;
					dtmFromDate.Value = DBNull.Value;
					dtmToDate.Value = DBNull.Value;
					chkClose.Checked = false;
					txtStockTakingPeriod.Focus();
					return;
				}

				// set mode to update inventory
				IsUpdateInventory = true;
				SwitchMode(true);
				thrProcess = new Thread(new ThreadStart(UpdateInventory));
				thrProcess.Start();
				if (thrProcess.ThreadState == ThreadState.Stopped || !thrProcess.IsAlive)
					thrProcess.Abort();
			}
			catch (ThreadAbortException ex)
			{
				Logger.LogMessage(ex, METHOD_NAME, Level.DEBUG);
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
		/// This will start a new thread for update inventory
		/// </summary>
		private void UpdateInventory()
		{
			const string METHOD_NAME = THIS + ".UpdateInventory()";
			try
			{
				this.Cursor = Cursors.WaitCursor;
				DateTime dtmDate = (DateTime)dtmFromDate.Value;
				if (IsUpdateInventory)
				{
					UpdateInventory(Convert.ToInt32(cboCCN.SelectedValue), Convert.ToInt32(txtStockTakingPeriod.Tag), dtmDate);
					string[] strMsg = new string[]{btnUpdate.Text.Replace("&", string.Empty)};
					PCSMessageBox.Show(ErrorCode.MESSAGE_TASK_COMPLETED, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, strMsg);
					this.btnUpdate.Enabled = true;
					this.btnUpdateDiff.Enabled = true;
				}
				else
				{
					UpdateDifferent(Convert.ToInt32(txtStockTakingPeriod.Tag), (DateTime)dtmToDate.Value);
					string[] strMsg = new string[]{btnUpdateDiff.Text.Replace("&", string.Empty)};
					PCSMessageBox.Show(ErrorCode.MESSAGE_TASK_COMPLETED, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, strMsg);
					this.btnUpdate.Enabled = true;
					this.btnUpdateDiff.Enabled = true;
				}
				this.Cursor = Cursors.Default;
				SwitchMode(false);
			}
			catch (ThreadAbortException ex)
			{
				Logger.LogMessage(ex.Message, METHOD_NAME, Level.DEBUG);
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
				string[] strMsg = new string[]{this.Text};
				PCSMessageBox.Show(ErrorCode.MESSAGE_CANNOT_ROLL_UP, MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, strMsg);
				try
				{
					Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
				}
				catch
				{
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION);
				}
			}
			finally
			{
				this.Cursor = Cursors.Default;
				SwitchMode(false);
			}
		}
		private void UpdateInventory(int pintCCNID, int pintPeriodID, DateTime pdtmStockTakingDate)
		{
			StockTakingPeriodBO boPeriod = new StockTakingPeriodBO();
			// first we will get all data of current period
			DataTable dtbStockTaking = boPeriod.GetStockTakingByPeriodID(pintPeriodID).Tables[0];
			// now get all data from all bin of current cache
			DataTable dtbCache = boPeriod.ListAllCache();
			// list all item with bin and location to be update
			DataTable dtbItemToUpdate = boPeriod.ListItemToUpdate(pintPeriodID);
			// list all location with master location
			DataTable dtbLocations = boPeriod.ListLocation();
			DataTable dtbAdjustmentTable = boPeriod.GetAdjustmentSchema();
			string strTransNo;
			FormInfo objFormInfo = FormControlComponents.GetFormInfo(new IVInventoryAdjustment(), out strTransNo);
			string strFormat_Number = objFormInfo.mTransFormat.Substring(objFormInfo.mTransFormat.IndexOf("#"));
			string strAutoNumber = strTransNo.Substring(strTransNo.Length - strFormat_Number.Length, strFormat_Number.Length);
			strTransNo = strTransNo.Substring(0, strTransNo.Length - strFormat_Number.Length);
			int intAutoNumber = Convert.ToInt32(strAutoNumber);
			string strComment = "Stock Taking " + pdtmStockTakingDate.ToString(Constants.DATETIME_FORMAT);
			foreach (DataRow drowItem in dtbItemToUpdate.Rows)
			{
				string strLocationID = drowItem[IV_BinCacheTable.LOCATIONID_FLD].ToString();
				string strBinID = drowItem[IV_BinCacheTable.BINID_FLD].ToString();
				string strProductID = drowItem[IV_BinCacheTable.PRODUCTID_FLD].ToString();
				string strStockUMID = drowItem[ITM_ProductTable.STOCKUMID_FLD].ToString();
				string strFilter = IV_BinCacheTable.LOCATIONID_FLD + "=" + strLocationID + " AND " + IV_BinCacheTable.BINID_FLD + "=" + strBinID + " AND " + IV_BinCacheTable.PRODUCTID_FLD + "=" + strProductID;
				decimal decCacheQuantity = 0, decTakingQuantity = 0, decCommitQuantity = 0;
				// quantity from cache
				try
				{
					decCacheQuantity = Convert.ToDecimal(dtbCache.Compute("SUM(" + IV_BinCacheTable.OHQUANTITY_FLD + ")", strFilter));
				}
				catch
				{}
				try
				{
					decCommitQuantity = Convert.ToDecimal(dtbCache.Compute("SUM(" + IV_BinCacheTable.COMMITQUANTITY_FLD + ")", strFilter));
				}
				catch
				{}
				// quantity from stock taking
				try
				{
					decTakingQuantity = Convert.ToDecimal(dtbStockTaking.Compute("SUM(" + IV_StockTakingTable.QUANTITY_FLD + ")", strFilter));
				}
				catch
				{
				}
				decimal decAdjustQuantity = decTakingQuantity - decCacheQuantity;
				int intMasterLocationID = GetMasterLocationIDByLocationID(strLocationID, dtbLocations);
				if (decAdjustQuantity != 0)
				{
					intAutoNumber++;

					#region make new adjustment record

					DataRow drowAdjust = dtbAdjustmentTable.NewRow();
					drowAdjust[IV_AdjustmentTable.POSTDATE_FLD] = pdtmStockTakingDate;
					drowAdjust[IV_AdjustmentTable.COMMENT_FLD] = strComment;
					drowAdjust[IV_AdjustmentTable.PRODUCTID_FLD] = strProductID;
					drowAdjust[IV_AdjustmentTable.STOCKUMID_FLD] = strStockUMID;
					drowAdjust[IV_AdjustmentTable.CCNID_FLD] = pintCCNID;
					drowAdjust[IV_AdjustmentTable.LOCATIONID_FLD] = strLocationID;
					drowAdjust[IV_AdjustmentTable.BINID_FLD] = strBinID;
					drowAdjust[IV_AdjustmentTable.MASTERLOCATIONID_FLD] = intMasterLocationID;
					drowAdjust[IV_AdjustmentTable.ADJUSTQUANTITY_FLD] = decAdjustQuantity;
					drowAdjust[IV_AdjustmentTable.AVAILABLEQTY_FLD] = decCacheQuantity - decCommitQuantity;
					drowAdjust[IV_AdjustmentTable.USEDBYCOSTING_FLD] = false;
					drowAdjust[IV_AdjustmentTable.USERNAME_FLD] = SystemProperty.UserName;
					drowAdjust[IV_AdjustmentTable.TRANSNO_FLD] = strTransNo + intAutoNumber.ToString().PadLeft(strFormat_Number.Length, '0');
					dtbAdjustmentTable.Rows.Add(drowAdjust);

					#endregion
				}

				#region update cache
				DataRow[] drowCaches = dtbCache.Select(strFilter);
				if (drowCaches.Length > 0)
					drowCaches[0][IV_BinCacheTable.OHQUANTITY_FLD] = decTakingQuantity;
				else
				{
					DataRow drowCache = dtbCache.NewRow();
					drowCache[IV_BinCacheTable.CCNID_FLD] = pintCCNID;
					drowCache[IV_BinCacheTable.MASTERLOCATIONID_FLD] = intMasterLocationID;
					drowCache[IV_BinCacheTable.LOCATIONID_FLD] = strLocationID;
					drowCache[IV_BinCacheTable.BINID_FLD] = strBinID;
					drowCache[IV_BinCacheTable.PRODUCTID_FLD] = strProductID;
					drowCache[IV_BinCacheTable.OHQUANTITY_FLD] = decTakingQuantity;
					dtbCache.Rows.Add(drowCache);
				}
				#endregion
			}
			boPeriod.UpdateInventory(dtbAdjustmentTable, dtbCache, strComment);
		}

		private void UpdateDifferent(int pintPeriodID, DateTime pdtmStockTakingDate)
		{
			StockTakingPeriodBO boPeriod = new StockTakingPeriodBO();
			InventoryUtilsBO boIVUtils = new InventoryUtilsBO();
			// first we will get all data of current period
			DataTable dtbStockTaking = boPeriod.GetStockTakingByPeriodID(pintPeriodID).Tables[0];
			DataTable dtbCache = boIVUtils.GetOHQtyByPostDate(pdtmStockTakingDate, 0, 0, 0, 0);
			// transaction history of stock taking date
			DataTable dtbHistory = boPeriod.ListTransactionHistory(pdtmStockTakingDate);
			// list all item with bin and location to be update
			DataTable dtbItemToUpdate = boPeriod.ListItemToUpdate(pintPeriodID);
			// data for IV_StockTakingDifferent
			DataTable dtbStockTakingDifferent = boPeriod.GetStockTakingDifferent(pintPeriodID);
			string strLocationID, strBinID, strProductID, strFilter;
			decimal decCacheQuantity = 0, decTakingQuantity = 0, decHistory = 0, decAdjustQuantity = 0;
			foreach (DataRow drowItem in dtbItemToUpdate.Rows)
			{
				strLocationID = drowItem[IV_BinCacheTable.LOCATIONID_FLD].ToString();
				strBinID = drowItem[IV_BinCacheTable.BINID_FLD].ToString();
				strProductID = drowItem[IV_BinCacheTable.PRODUCTID_FLD].ToString();
				strFilter = IV_BinCacheTable.LOCATIONID_FLD + "=" + strLocationID
					+ " AND " + IV_BinCacheTable.BINID_FLD + "=" + strBinID
					+ " AND " + IV_BinCacheTable.PRODUCTID_FLD + "=" + strProductID;
				decCacheQuantity = 0;
				decTakingQuantity = 0;
				decHistory = 0;
				// actual quantity in stock taking time
				try
				{
					decCacheQuantity = Convert.ToDecimal(dtbCache.Compute("SUM(" + IV_BinCacheTable.OHQUANTITY_FLD + ")", strFilter));
				}
				catch{}
				// quantity from stock taking
				try
				{
					decTakingQuantity = Convert.ToDecimal(dtbStockTaking.Compute("SUM(" + IV_StockTakingTable.QUANTITY_FLD + ")", strFilter));
				}
				catch{}
				decAdjustQuantity = decTakingQuantity - decCacheQuantity;
				try
				{
					decHistory = Convert.ToDecimal(dtbHistory.Compute("SUM(" + MST_TransactionHistoryTable.QUANTITY_FLD + ")", strFilter));
				}
				catch{}
				DataRow[] drowDiffs = null;
				try
				{
					drowDiffs = dtbStockTakingDifferent.Select(strFilter);
				}
				catch
				{
					Logger.LogMessage(strFilter, string.Empty, Level.DEBUG);
				}
				if (drowDiffs.Length > 0)// already exist
				{
					// update the quantity only
					drowDiffs[0][IV_StockTakingDifferentTable.OHQUANTITY_FLD] = decCacheQuantity;
					drowDiffs[0][IV_StockTakingDifferentTable.ACTUALQUANTITY_FLD] = decTakingQuantity;
					drowDiffs[0][IV_StockTakingDifferentTable.DIFFERENTQUANTITY_FLD] = decAdjustQuantity;
					drowDiffs[0][IV_StockTakingDifferentTable.HISTORYQUANTITY_FLD] = decHistory;
					drowDiffs[0][IV_StockTakingDifferentTable.STOCKTAKINGDATE_FLD] = pdtmStockTakingDate;
				}
				else // create new record
				{
					DataRow drowDiff = dtbStockTakingDifferent.NewRow();
					drowDiff[IV_StockTakingDifferentTable.BINID_FLD] = strBinID;
					drowDiff[IV_StockTakingDifferentTable.LOCATIONID_FLD] = strLocationID;
					drowDiff[IV_StockTakingDifferentTable.PRODUCTID_FLD] = strProductID;
					drowDiff[IV_StockTakingDifferentTable.STOCKTAKINGDATE_FLD] = pdtmStockTakingDate;
					drowDiff[IV_StockTakingDifferentTable.STOCKTAKINGPERIODID_FLD] = pintPeriodID;
					drowDiff[IV_StockTakingDifferentTable.OHQUANTITY_FLD] = decCacheQuantity;
					drowDiff[IV_StockTakingDifferentTable.ACTUALQUANTITY_FLD] = decTakingQuantity;
					drowDiff[IV_StockTakingDifferentTable.DIFFERENTQUANTITY_FLD] = decAdjustQuantity;
					drowDiff[IV_StockTakingDifferentTable.HISTORYQUANTITY_FLD] = decHistory;
					dtbStockTakingDifferent.Rows.Add(drowDiff);
				}
			}
			boPeriod.UpdateDifferent(dtbStockTakingDifferent);
		}

		private int GetMasterLocationIDByLocationID(string pstrLocationID, DataTable pdtbLocation)
		{
			return Convert.ToInt32(pdtbLocation.Select(MST_LocationTable.LOCATIONID_FLD + "=" + pstrLocationID)[0][MST_LocationTable.MASTERLOCATIONID_FLD]);
		}

		private void SwitchMode(bool pblnProcessing)
		{
			cboCCN.ReadOnly = pblnProcessing;
			txtStockTakingPeriod.Enabled = !pblnProcessing;
			dtmFromDate.Enabled = !pblnProcessing;
			dtmToDate.Enabled = !pblnProcessing;
			chkClose.Enabled = !pblnProcessing;
			btnStockTakingPeriod.Enabled = !pblnProcessing;
			btnAdd.Enabled = !pblnProcessing;
			btnEdit.Enabled = !pblnProcessing;
			btnDelete.Enabled = !pblnProcessing;
			btnSave.Enabled = !pblnProcessing;
			btnUpdate.Enabled = !pblnProcessing;
			btnUpdateDiff.Enabled = !pblnProcessing;
		}

		private void btnUpdateDiff_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnUpdate_Click()";
			try
			{
				if (cboCCN.SelectedValue.Equals(null))
				{
					PCSMessageBox.Show(ErrorCode.MESSAGE_RGA_CCN, MessageBoxIcon.Warning);
					cboCCN.Focus();
					return;
				}
				StockTakingPeriodBO boPeriod = new StockTakingPeriodBO();
				if (txtStockTakingPeriod.Tag == null)
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Warning);
					txtStockTakingPeriod.Focus();
					return;
				}
				// try to get period
				try
				{
					IV_StockTakingPeriodVO voPeriod = (IV_StockTakingPeriodVO)boPeriod.GetObjectVO(Convert.ToInt32(txtStockTakingPeriod.Tag), string.Empty);
					if (voPeriod.StockTakingPeriodID <= 0)
						throw new Exception();
				}
				catch
				{
					string[] strMessage = new string[1];
					strMessage[0] = lblStockTakingPeriod.Text;
					PCSMessageBox.Show(ErrorCode.MESSAGE_SELECTION_NOT_EXIST, MessageBoxIcon.Error, strMessage);
					// reset data
					txtStockTakingPeriod.Text = string.Empty;
					dtmFromDate.Value = DBNull.Value;
					dtmToDate.Value = DBNull.Value;
					chkClose.Checked = false;
					txtStockTakingPeriod.Focus();
					return;
				}

				// set mode to update different
				IsUpdateInventory = false;
				SwitchMode(true);
				thrProcess = new Thread(new ThreadStart(UpdateInventory));
				thrProcess.Start();
				if (thrProcess.ThreadState == ThreadState.Stopped || !thrProcess.IsAlive)
					thrProcess.Abort();
			}
			catch (ThreadAbortException ex)
			{
				Logger.LogMessage(ex, METHOD_NAME, Level.DEBUG);
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

        private void CloseStockButton_Click(object sender, EventArgs e)
        {
            const string METHOD_NAME = THIS + ".CloseStockButton_Click()";
            Cursor = Cursors.WaitCursor;
            try
            {
                StockTakingPeriodBO boPeriod = new StockTakingPeriodBO();
                // run script to close the stock taking
                int periodId = Convert.ToInt32(txtStockTakingPeriod.Tag);
                DateTime stockTakingDate = (DateTime)dtmFromDate.Value;
                // reset to begin of month
                stockTakingDate = new DateTime(stockTakingDate.Year, stockTakingDate.Month, 1);
                boPeriod.CloseStockTaking(periodId, stockTakingDate);
            }
            catch (ThreadAbortException ex)
            {
                Logger.LogMessage(ex, METHOD_NAME, Level.DEBUG);
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
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void UpdateBeginButton_Click(object sender, EventArgs e)
        {
            const string METHOD_NAME = THIS + ".UpdateBeginButton_Click()";
            Cursor = Cursors.WaitCursor;
            try
            {
                StockTakingPeriodBO boPeriod = new StockTakingPeriodBO();
                // run script to update begin stock
                int periodId = Convert.ToInt32(txtStockTakingPeriod.Tag);
                DateTime effectDate = (DateTime)dtmFromDate.Value;
                // reset to begin of month
                effectDate = new DateTime(effectDate.Year, effectDate.Month, 1);
                boPeriod.UpdateBeginStock(periodId, effectDate);
            }
            catch (ThreadAbortException ex)
            {
                Logger.LogMessage(ex, METHOD_NAME, Level.DEBUG);
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
            finally
            {
                Cursor = Cursors.Default;
            }
        }
	}
}
