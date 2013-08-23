using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using C1.Win.C1Input;
using PCSComMaterials.Inventory.BO;
using PCSComMaterials.Inventory.DS;
using PCSComUtils.Common;
using PCSComUtils.Common.DS;
using PCSComUtils.PCSExc;
using PCSUtils.Log;
using PCSUtils.Utils;

namespace PCSMaterials.Inventory
{
	/// <summary>
	/// Summary description for OpenAndClosePeriod.
	/// </summary>
	public class OpenAndClosePeriod : Form
	{
		private Button btnClose;
		private Button btnHelp;
		private Button btnCloseOpen;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private Container components = null;
		const string THIS = "PCSMaterials.Inventory.OpenAndClosePeriod";
		private System.Windows.Forms.Button btnSelectPeriod;
		private C1.Win.C1Input.C1DateEdit dtmToDate;
		private C1.Win.C1Input.C1DateEdit dtmFromDate;
		private System.Windows.Forms.Label lblToDate;
		private System.Windows.Forms.Label lblFromDate;
		private System.Windows.Forms.Label lblMonth;
		private System.Windows.Forms.Label lblYear;
		private System.Windows.Forms.TextBox txtYear;
		private System.Windows.Forms.TextBox txtMonth;
		public OpenAndClosePeriod()
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
			this.btnClose = new System.Windows.Forms.Button();
			this.btnHelp = new System.Windows.Forms.Button();
			this.btnCloseOpen = new System.Windows.Forms.Button();
			this.btnSelectPeriod = new System.Windows.Forms.Button();
			this.dtmToDate = new C1.Win.C1Input.C1DateEdit();
			this.dtmFromDate = new C1.Win.C1Input.C1DateEdit();
			this.lblToDate = new System.Windows.Forms.Label();
			this.lblFromDate = new System.Windows.Forms.Label();
			this.lblMonth = new System.Windows.Forms.Label();
			this.lblYear = new System.Windows.Forms.Label();
			this.txtYear = new System.Windows.Forms.TextBox();
			this.txtMonth = new System.Windows.Forms.TextBox();
			((System.ComponentModel.ISupportInitialize)(this.dtmToDate)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dtmFromDate)).BeginInit();
			this.SuspendLayout();
			// 
			// btnClose
			// 
			this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnClose.Location = new System.Drawing.Point(345, 118);
			this.btnClose.Name = "btnClose";
			this.btnClose.TabIndex = 11;
			this.btnClose.Text = "&Close";
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			// 
			// btnHelp
			// 
			this.btnHelp.Location = new System.Drawing.Point(268, 118);
			this.btnHelp.Name = "btnHelp";
			this.btnHelp.TabIndex = 10;
			this.btnHelp.Text = "&Help";
			// 
			// btnCloseOpen
			// 
			this.btnCloseOpen.Location = new System.Drawing.Point(110, 118);
			this.btnCloseOpen.Name = "btnCloseOpen";
			this.btnCloseOpen.Size = new System.Drawing.Size(104, 23);
			this.btnCloseOpen.TabIndex = 9;
			this.btnCloseOpen.Text = "C&lose Period";
			this.btnCloseOpen.Click += new System.EventHandler(this.btnCloseOpen_Click);
			// 
			// btnSelectPeriod
			// 
			this.btnSelectPeriod.Location = new System.Drawing.Point(4, 118);
			this.btnSelectPeriod.Name = "btnSelectPeriod";
			this.btnSelectPeriod.Size = new System.Drawing.Size(104, 23);
			this.btnSelectPeriod.TabIndex = 8;
			this.btnSelectPeriod.Text = "&Select Period";
			this.btnSelectPeriod.Click += new System.EventHandler(this.btnSelectPeriod_Click);
			// 
			// dtmToDate
			// 
			// 
			// dtmToDate.Calendar
			// 
			this.dtmToDate.Calendar.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.dtmToDate.CustomFormat = "dd-MM-yyyy";
			this.dtmToDate.EmptyAsNull = true;
			this.dtmToDate.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
			this.dtmToDate.Location = new System.Drawing.Point(76, 26);
			this.dtmToDate.Name = "dtmToDate";
			this.dtmToDate.ReadOnly = true;
			this.dtmToDate.Size = new System.Drawing.Size(112, 20);
			this.dtmToDate.TabIndex = 3;
			this.dtmToDate.Tag = null;
			this.dtmToDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.dtmToDate.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.None;
			// 
			// dtmFromDate
			// 
			// 
			// dtmFromDate.Calendar
			// 
			this.dtmFromDate.Calendar.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.dtmFromDate.CustomFormat = "dd-MM-yyyy";
			this.dtmFromDate.EmptyAsNull = true;
			this.dtmFromDate.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
			this.dtmFromDate.Location = new System.Drawing.Point(76, 4);
			this.dtmFromDate.Name = "dtmFromDate";
			this.dtmFromDate.ReadOnly = true;
			this.dtmFromDate.Size = new System.Drawing.Size(112, 20);
			this.dtmFromDate.TabIndex = 1;
			this.dtmFromDate.Tag = null;
			this.dtmFromDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.dtmFromDate.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.None;
			// 
			// lblToDate
			// 
			this.lblToDate.ForeColor = System.Drawing.Color.Maroon;
			this.lblToDate.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lblToDate.Location = new System.Drawing.Point(4, 26);
			this.lblToDate.Name = "lblToDate";
			this.lblToDate.Size = new System.Drawing.Size(70, 20);
			this.lblToDate.TabIndex = 2;
			this.lblToDate.Text = "To Date";
			this.lblToDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblFromDate
			// 
			this.lblFromDate.ForeColor = System.Drawing.Color.Maroon;
			this.lblFromDate.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lblFromDate.Location = new System.Drawing.Point(4, 4);
			this.lblFromDate.Name = "lblFromDate";
			this.lblFromDate.Size = new System.Drawing.Size(70, 20);
			this.lblFromDate.TabIndex = 0;
			this.lblFromDate.Text = "From Date";
			this.lblFromDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblMonth
			// 
			this.lblMonth.ForeColor = System.Drawing.Color.Maroon;
			this.lblMonth.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lblMonth.Location = new System.Drawing.Point(4, 70);
			this.lblMonth.Name = "lblMonth";
			this.lblMonth.Size = new System.Drawing.Size(70, 20);
			this.lblMonth.TabIndex = 6;
			this.lblMonth.Text = "Month";
			this.lblMonth.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblYear
			// 
			this.lblYear.ForeColor = System.Drawing.Color.Maroon;
			this.lblYear.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lblYear.Location = new System.Drawing.Point(4, 48);
			this.lblYear.Name = "lblYear";
			this.lblYear.Size = new System.Drawing.Size(70, 20);
			this.lblYear.TabIndex = 4;
			this.lblYear.Text = "Year";
			this.lblYear.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// txtYear
			// 
			this.txtYear.Location = new System.Drawing.Point(76, 48);
			this.txtYear.Name = "txtYear";
			this.txtYear.ReadOnly = true;
			this.txtYear.Size = new System.Drawing.Size(112, 20);
			this.txtYear.TabIndex = 5;
			this.txtYear.Text = "";
			// 
			// txtMonth
			// 
			this.txtMonth.Location = new System.Drawing.Point(76, 70);
			this.txtMonth.Name = "txtMonth";
			this.txtMonth.ReadOnly = true;
			this.txtMonth.Size = new System.Drawing.Size(112, 20);
			this.txtMonth.TabIndex = 7;
			this.txtMonth.Text = "";
			// 
			// OpenAndClosePeriod
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.btnClose;
			this.ClientSize = new System.Drawing.Size(424, 144);
			this.Controls.Add(this.txtYear);
			this.Controls.Add(this.dtmToDate);
			this.Controls.Add(this.dtmFromDate);
			this.Controls.Add(this.lblToDate);
			this.Controls.Add(this.lblFromDate);
			this.Controls.Add(this.btnCloseOpen);
			this.Controls.Add(this.btnHelp);
			this.Controls.Add(this.btnClose);
			this.Controls.Add(this.btnSelectPeriod);
			this.Controls.Add(this.lblMonth);
			this.Controls.Add(this.lblYear);
			this.Controls.Add(this.txtMonth);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.KeyPreview = true;
			this.MaximizeBox = false;
			this.Name = "OpenAndClosePeriod";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Open And Close Period";
			this.Load += new System.EventHandler(this.OpenAndClosePeriod_Load);
			((System.ComponentModel.ISupportInitialize)(this.dtmToDate)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dtmFromDate)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		private void OpenAndClosePeriod_Load(object sender, EventArgs e)
		{
			const string METHOD_NAME = THIS + ".OpenAndClosePeriod_Load()";
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
		private void btnClose_Click(object sender, EventArgs e)
		{
			this.Close();
		}
		/// <summary>
		/// btnCloseOpen_Click
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Friday, October 6 2006</date>
		private void btnCloseOpen_Click(object sender, EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnCloseOpen_Click()";
			try
			{
				this.Cursor = Cursors.WaitCursor;
				var boOpenAndClosePeriod = new OpenAndClosePeriodBO();
				DateTime dtmFromDatePeriod = Convert.ToDateTime(dtmFromDate.Value);
                var dtmEffectDate = new DateTime(dtmFromDatePeriod.Year, dtmFromDatePeriod.Month, 1);
                boOpenAndClosePeriod.ClosePeriod(dtmEffectDate, Convert.ToInt32(txtYear.Tag));
			    
				PCSMessageBox.Show(ErrorCode.MESSAGE_AFTER_SAVE_DATA);
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
				this.Cursor = Cursors.Default;
			}
		}

		private void btnSelectPeriod_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnSelectPeriod_Click()";
			try
			{
				Hashtable htCondition = new Hashtable();
				htCondition.Add(Sys_PeriodTable.ACTIVATE_FLD, 1);
				DataRowView drwResult = null;
				drwResult = FormControlComponents.OpenSearchForm(Sys_PeriodTable.TABLE_NAME, string.Empty, "", htCondition, true);
				if (drwResult != null)
				{
					dtmFromDate.Value = drwResult[Sys_PeriodTable.FROMDATE_FLD];
					dtmToDate.Value = drwResult[Sys_PeriodTable.TODATE_FLD];
					txtYear.Text = Convert.ToDateTime(dtmFromDate.Value).Year.ToString("0000");
					txtYear.Tag = drwResult[Sys_PeriodTable.PERIODID_FLD].ToString();
					txtMonth.Text = Convert.ToDateTime(dtmFromDate.Value).Month.ToString();
					btnCloseOpen.Enabled = true;
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
