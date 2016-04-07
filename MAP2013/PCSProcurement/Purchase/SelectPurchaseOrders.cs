using System;
using System.Data;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

//PCS's namespaces
using PCSComProcurement.Purchase.BO;
using PCSComProcurement.Purchase.DS;
using PCSUtils.Utils;
using PCSComUtils.Common.BO;
using PCSComUtils.Admin.BO;
using PCSComUtils.MasterSetup.BO;
using PCSComUtils.Common;
using PCSComUtils.PCSExc;
using PCSUtils.Log;
using PCSComUtils.MasterSetup.DS;

namespace PCSProcurement.Purchase
{
	/// <summary>
	/// Summary description for SelectPurchaseOrders.
	/// </summary>
	public class SelectPurchaseOrders : System.Windows.Forms.Form
	{
		#region Declarations

		private System.Windows.Forms.CheckBox chkSelectAll;
		private System.Windows.Forms.Button btnClose;
		private System.Windows.Forms.Button btnHelp;
		private System.Windows.Forms.Button btnSelect;
		private System.Windows.Forms.Button btnSearch;		
		private System.Windows.Forms.Label lblPO;
		private System.Windows.Forms.Button btnSearchBeginPO;
		private C1.Win.C1TrueDBGrid.C1TrueDBGrid dgrdData;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		/// 
		#region Constants
		
		private const string THIS = "PCSProcurement.Purchase.SelectPurchaseOrders";		
		private const string SELECTED_COL = "Selected";
		private const string ZERO_STRING = "0";		
		private const string UNCLOSE_PO_4_INVOICE_VIEW = "v_SelectUnclosedPO4Invoice";

		#endregion Constants
		
		private System.ComponentModel.Container components = null;		
		private DataTable dtbData;
		private DataTable dtStoreGridLayout;	
		
		#endregion Declarations		

		#region Properties
		
		private Hashtable mhtbCondition;
		private System.Windows.Forms.TextBox txtBeginPO;
		private C1.Win.C1Input.C1DateEdit dtmToDeliveryDate;
		private C1.Win.C1Input.C1DateEdit dtmFromDeliveryDate;
		private System.Windows.Forms.Label lblToDeliveryDate;
		private System.Windows.Forms.Label lblFromDeliveryDate;
	
		public Hashtable ConditionHashTable
		{
			set{ mhtbCondition = value;}
			get{ return mhtbCondition;}
		}
		
		private Hashtable mSelectedRows;
		public Hashtable SelectedRows
		{
			set{ mSelectedRows = value;}
			get{ return mSelectedRows;}
		}

		#endregion Properties
		
		#region Constructor, Destructor
		
		public SelectPurchaseOrders()
		{
			InitializeComponent();	
			mhtbCondition = new Hashtable();
		}

		public SelectPurchaseOrders(Hashtable phtbCondition)
		{
			InitializeComponent();
			if(phtbCondition != null)
			{
				mhtbCondition = phtbCondition;
			}
			else
			{
				mhtbCondition = new Hashtable();
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
		
		#endregion Properties
	
		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SelectPurchaseOrders));
            this.chkSelectAll = new System.Windows.Forms.CheckBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnHelp = new System.Windows.Forms.Button();
            this.btnSelect = new System.Windows.Forms.Button();
            this.dtmToDeliveryDate = new C1.Win.C1Input.C1DateEdit();
            this.dtmFromDeliveryDate = new C1.Win.C1Input.C1DateEdit();
            this.lblToDeliveryDate = new System.Windows.Forms.Label();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnSearchBeginPO = new System.Windows.Forms.Button();
            this.txtBeginPO = new System.Windows.Forms.TextBox();
            this.lblFromDeliveryDate = new System.Windows.Forms.Label();
            this.lblPO = new System.Windows.Forms.Label();
            this.dgrdData = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
            ((System.ComponentModel.ISupportInitialize)(this.dtmToDeliveryDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtmFromDeliveryDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgrdData)).BeginInit();
            this.SuspendLayout();
            // 
            // chkSelectAll
            // 
            resources.ApplyResources(this.chkSelectAll, "chkSelectAll");
            this.chkSelectAll.Name = "chkSelectAll";
            this.chkSelectAll.Click += new System.EventHandler(this.chkSelectAll_Click);
            // 
            // btnClose
            // 
            resources.ApplyResources(this.btnClose, "btnClose");
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Name = "btnClose";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnHelp
            // 
            resources.ApplyResources(this.btnHelp, "btnHelp");
            this.btnHelp.Name = "btnHelp";
            // 
            // btnSelect
            // 
            resources.ApplyResources(this.btnSelect, "btnSelect");
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // dtmToDeliveryDate
            // 
            // 
            // 
            // 
            this.dtmToDeliveryDate.Calendar.FirstDayOfWeek = ((System.Windows.Forms.Day)(resources.GetObject("dtmToDeliveryDate.Calendar.FirstDayOfWeek")));
            this.dtmToDeliveryDate.Calendar.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("dtmToDeliveryDate.Calendar.ImeMode")));
            this.dtmToDeliveryDate.Calendar.ShowClearButton = false;
            this.dtmToDeliveryDate.Calendar.ShowTodayButton = false;
            resources.ApplyResources(this.dtmToDeliveryDate, "dtmToDeliveryDate");
            this.dtmToDeliveryDate.DisplayFormat.Inherit = ((C1.Win.C1Input.FormatInfoInheritFlags)(resources.GetObject("dtmToDeliveryDate.DisplayFormat.Inherit")));
            this.dtmToDeliveryDate.EditFormat.Inherit = ((C1.Win.C1Input.FormatInfoInheritFlags)(resources.GetObject("dtmToDeliveryDate.EditFormat.Inherit")));
            this.dtmToDeliveryDate.Name = "dtmToDeliveryDate";
            this.dtmToDeliveryDate.ParseInfo.Inherit = ((C1.Win.C1Input.ParseInfoInheritFlags)(resources.GetObject("dtmToDeliveryDate.ParseInfo.Inherit")));
            this.dtmToDeliveryDate.PreValidation.Inherit = ((C1.Win.C1Input.PreValidationInheritFlags)(resources.GetObject("dtmToDeliveryDate.PreValidation.Inherit")));
            this.dtmToDeliveryDate.Validating += new System.ComponentModel.CancelEventHandler(this.dtmToPostDate_Validating);
            // 
            // dtmFromDeliveryDate
            // 
            // 
            // 
            // 
            this.dtmFromDeliveryDate.Calendar.FirstDayOfWeek = ((System.Windows.Forms.Day)(resources.GetObject("dtmFromDeliveryDate.Calendar.FirstDayOfWeek")));
            this.dtmFromDeliveryDate.Calendar.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("dtmFromDeliveryDate.Calendar.ImeMode")));
            this.dtmFromDeliveryDate.Calendar.ShowClearButton = false;
            this.dtmFromDeliveryDate.Calendar.ShowTodayButton = false;
            resources.ApplyResources(this.dtmFromDeliveryDate, "dtmFromDeliveryDate");
            this.dtmFromDeliveryDate.DisplayFormat.Inherit = ((C1.Win.C1Input.FormatInfoInheritFlags)(resources.GetObject("dtmFromDeliveryDate.DisplayFormat.Inherit")));
            this.dtmFromDeliveryDate.EditFormat.Inherit = ((C1.Win.C1Input.FormatInfoInheritFlags)(resources.GetObject("dtmFromDeliveryDate.EditFormat.Inherit")));
            this.dtmFromDeliveryDate.Name = "dtmFromDeliveryDate";
            this.dtmFromDeliveryDate.ParseInfo.Inherit = ((C1.Win.C1Input.ParseInfoInheritFlags)(resources.GetObject("dtmFromDeliveryDate.ParseInfo.Inherit")));
            this.dtmFromDeliveryDate.PreValidation.Inherit = ((C1.Win.C1Input.PreValidationInheritFlags)(resources.GetObject("dtmFromDeliveryDate.PreValidation.Inherit")));
            this.dtmFromDeliveryDate.Validating += new System.ComponentModel.CancelEventHandler(this.dtmFromPostDate_Validating);
            // 
            // lblToDeliveryDate
            // 
            resources.ApplyResources(this.lblToDeliveryDate, "lblToDeliveryDate");
            this.lblToDeliveryDate.Name = "lblToDeliveryDate";
            // 
            // btnSearch
            // 
            resources.ApplyResources(this.btnSearch, "btnSearch");
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnSearchBeginPO
            // 
            resources.ApplyResources(this.btnSearchBeginPO, "btnSearchBeginPO");
            this.btnSearchBeginPO.Name = "btnSearchBeginPO";
            this.btnSearchBeginPO.Click += new System.EventHandler(this.btnSearchBeginPO_Click);
            // 
            // txtBeginPO
            // 
            resources.ApplyResources(this.txtBeginPO, "txtBeginPO");
            this.txtBeginPO.Name = "txtBeginPO";
            this.txtBeginPO.Enter += new System.EventHandler(this.OnEnterControl);
            this.txtBeginPO.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBeginPO_KeyDown);
            this.txtBeginPO.Leave += new System.EventHandler(this.txtBeginPO_Leave);
            // 
            // lblFromDeliveryDate
            // 
            this.lblFromDeliveryDate.ForeColor = System.Drawing.SystemColors.ControlText;
            resources.ApplyResources(this.lblFromDeliveryDate, "lblFromDeliveryDate");
            this.lblFromDeliveryDate.Name = "lblFromDeliveryDate";
            // 
            // lblPO
            // 
            resources.ApplyResources(this.lblPO, "lblPO");
            this.lblPO.Name = "lblPO";
            // 
            // dgrdData
            // 
            resources.ApplyResources(this.dgrdData, "dgrdData");
            this.dgrdData.Images.Add(((System.Drawing.Image)(resources.GetObject("dgrdData.Images"))));
            this.dgrdData.Name = "dgrdData";
            this.dgrdData.PreviewInfo.Location = new System.Drawing.Point(0, 0);
            this.dgrdData.PreviewInfo.Size = new System.Drawing.Size(0, 0);
            this.dgrdData.PreviewInfo.ZoomFactor = 75D;
            this.dgrdData.PrintInfo.PageSettings = ((System.Drawing.Printing.PageSettings)(resources.GetObject("dgrdData.PrintInfo.PageSettings")));
            this.dgrdData.AfterColUpdate += new C1.Win.C1TrueDBGrid.ColEventHandler(this.dgrdData_AfterColUpdate);
            this.dgrdData.PropBag = resources.GetString("dgrdData.PropBag");
            // 
            // SelectPurchaseOrders
            // 
            resources.ApplyResources(this, "$this");
            this.CancelButton = this.btnClose;
            this.Controls.Add(this.dgrdData);
            this.Controls.Add(this.txtBeginPO);
            this.Controls.Add(this.dtmToDeliveryDate);
            this.Controls.Add(this.dtmFromDeliveryDate);
            this.Controls.Add(this.lblToDeliveryDate);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.btnSearchBeginPO);
            this.Controls.Add(this.lblFromDeliveryDate);
            this.Controls.Add(this.lblPO);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnHelp);
            this.Controls.Add(this.btnSelect);
            this.Controls.Add(this.chkSelectAll);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.Name = "SelectPurchaseOrders";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.SelectPurchaseOrders_Closing);
            this.Load += new System.EventHandler(this.SelectPurchaseOrders_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtmToDeliveryDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtmFromDeliveryDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgrdData)).EndInit();
            this.ResumeLayout(false);

		}
		#endregion
		
		#region Private Methods
		
		/// <summary>
		/// Fill related data on controls when select Payment Term
		/// </summary>
		/// <param name="pblnOpenOnly"></param>
		private void SelectPONo(string pstrMethodName, bool pblnOpenOnly)
		{
			try
			{
				Hashtable htbCriteria = new Hashtable();
				DataRowView drwResult = null;
				
				htbCriteria.Add(PO_PurchaseOrderMasterTable.CCNID_FLD, mhtbCondition[PO_PurchaseOrderMasterTable.CCNID_FLD]);
				htbCriteria.Add(PO_PurchaseOrderMasterTable.PARTYID_FLD, mhtbCondition[PO_PurchaseOrderMasterTable.PARTYID_FLD]);				
				
				//Call OpenSearchForm for selecting MPS planning cycle
				drwResult = FormControlComponents.OpenSearchForm(UNCLOSE_PO_4_INVOICE_VIEW, PO_PurchaseOrderMasterTable.TABLE_NAME + PO_PurchaseOrderMasterTable.CODE_FLD, txtBeginPO.Text, htbCriteria, pblnOpenOnly);
				
				// If has Master location matched searching condition, fill values to form's controls
				if (drwResult != null)
				{
					//Check if data was changed then reassign
					txtBeginPO.Text = drwResult[PO_PurchaseOrderMasterTable.TABLE_NAME + PO_PurchaseOrderMasterTable.CODE_FLD].ToString();
					//Reset modify status
					txtBeginPO.Modified = false;
				}
				else
				{
					if(!pblnOpenOnly)
					{				
						txtBeginPO.Focus();
					}					
				}				
			}
			catch (PCSException ex)
			{
				throw new PCSException(ex.mCode, pstrMethodName, ex);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message, ex);
			}	
		}

		/// <summary>
		/// Processign Enter event
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnEnterControl(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".OnEnterControl()";
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
		
		/// <summary>
		/// Processign Leave event
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnLeaveControl(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".OnLeaveControl()";
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
		
		#endregion Private Methods
		
		#region Event Processing

		private void SelectPurchaseOrders_Load(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".SelectPurchaseOrders_Load()";
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

				//store the gridlayout
				dtStoreGridLayout = FormControlComponents.StoreGridLayout(dgrdData);
				
				//Call the method in the BO Class to search for Work Order Line
				POInvoiceBO boInvoice = new POInvoiceBO();
				dtbData = boInvoice.SelectPO4Invoice(mhtbCondition);
				
				dgrdData.DataSource = dtbData;
				FormatDataGrid();
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
		/// Display the detail data after searching into the grid set layout for the grid
		/// </summary>
		/// <param name="dtData"></param>
		private void FormatDataGrid() 
		{
			try
			{
				//Restore the gridLayout
				FormControlComponents.RestoreGridLayout(dgrdData,dtStoreGridLayout);
				for(int i =0; i < dgrdData.Splits[0].DisplayColumns.Count; i++)
				{
					dgrdData.Splits[0].DisplayColumns[i].Locked = true;
				}

				dgrdData.Columns[PO_PurchaseOrderMasterTable.ORDERDATE_FLD].NumberFormat = Constants.DATETIME_FORMAT_HOUR;
				dgrdData.Columns[PO_DeliveryScheduleTable.SCHEDULEDATE_FLD].NumberFormat = Constants.DATETIME_FORMAT_HOUR;
				dgrdData.Columns[PO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD].NumberFormat = Constants.DECIMAL_NUMBERFORMAT;
				dgrdData.Columns[PO_PurchaseOrderDetailTable.TABLE_NAME + PO_PurchaseOrderDetailTable.UNITPRICE_FLD].NumberFormat = Constants.DECIMAL_NUMBERFORMAT;
				dgrdData.Columns[PO_PurchaseOrderDetailTable.TABLE_NAME + PO_PurchaseOrderDetailTable.VAT_FLD].NumberFormat = Constants.DECIMAL_NUMBERFORMAT;
				dgrdData.Columns[PO_PurchaseOrderDetailTable.TABLE_NAME + PO_PurchaseOrderDetailTable.SPECIALTAX_FLD].NumberFormat = Constants.DECIMAL_NUMBERFORMAT;
				dgrdData.Columns[PO_PurchaseOrderDetailTable.TABLE_NAME + PO_PurchaseOrderDetailTable.IMPORTTAX_FLD].NumberFormat = Constants.DECIMAL_NUMBERFORMAT;

				dgrdData.Splits[0].DisplayColumns[SELECTED_COL].Locked = false;				
				//set the selected to be the check box				
				dgrdData.Columns[SELECTED_COL].ValueItems.Presentation = C1.Win.C1TrueDBGrid.PresentationEnum.CheckBox;
				dgrdData.Columns[SELECTED_COL].DefaultValue = false.ToString();
				dgrdData.Columns[SELECTED_COL].ValueItems.Translate = true;				
			}
			catch (PCSException ex)
			{
				throw ex;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message, ex);
			}
		}

		public void btnSearch_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnSearch_Click()";
			const string SQL_DATE_FORMAT = "yyyy-MM-dd";
			const string END_TIME_OF_DAY = " 23:59:59";

			try 
			{
				string strFilter = string.Empty;
				bool blnHasFromPostDate = false;

				if(txtBeginPO.Text.Length > 0)
				{
					strFilter += PO_PurchaseOrderMasterTable.TABLE_NAME + PO_PurchaseOrderMasterTable.CODE_FLD;
					strFilter += " = '" + txtBeginPO.Text.Replace("'", "''") + "' ";
				}
				
				//if has From Post Date
				if(!dtmFromDeliveryDate.ValueIsDbNull && (dtmFromDeliveryDate.Text.Trim() != string.Empty))
				{
					if(strFilter.Length > 0)
					{
						strFilter += " AND " + PO_DeliveryScheduleTable.SCHEDULEDATE_FLD;
						strFilter += " >= '"  +  DateTime.Parse(dtmFromDeliveryDate.Value.ToString()).ToString(SQL_DATE_FORMAT) + "' ";
					}
					else
					{
						strFilter += PO_DeliveryScheduleTable.SCHEDULEDATE_FLD;
						strFilter += " >= '"  +  DateTime.Parse(dtmFromDeliveryDate.Value.ToString()).ToString(SQL_DATE_FORMAT) + "' ";
					}

					blnHasFromPostDate = true;
				}
				
				//if has To Post Date
				if(!dtmToDeliveryDate.ValueIsDbNull && (dtmToDeliveryDate.Text.Trim() != string.Empty))
				{
					if(blnHasFromPostDate)
					{
						DateTime dtmTmpFromDate = DateTime.Parse(((DateTime)dtmFromDeliveryDate.Value).ToString(SQL_DATE_FORMAT));
						DateTime dtmTmpToDate = DateTime.Parse(((DateTime)dtmToDeliveryDate.Value).ToString(SQL_DATE_FORMAT));
						if(dtmTmpFromDate > dtmTmpToDate)
						{							
							PCSMessageBox.Show(ErrorCode.MESSAGE_MP_PERIODDATE, MessageBoxIcon.Exclamation);
							dtmToDeliveryDate.Focus();
							return;
						}
					}

					if(strFilter.Length > 0)
					{
						strFilter += " AND " + PO_DeliveryScheduleTable.SCHEDULEDATE_FLD;
						strFilter += " <= '"  +  DateTime.Parse(dtmToDeliveryDate.Value.ToString()).ToString(SQL_DATE_FORMAT) + END_TIME_OF_DAY + "'";
					}
					else
					{
						strFilter += PO_DeliveryScheduleTable.SCHEDULEDATE_FLD;
						strFilter += " <= '"  +  DateTime.Parse(dtmToDeliveryDate.Value.ToString()).ToString(SQL_DATE_FORMAT) + END_TIME_OF_DAY + "'";
					}
				}

				//Filter data source by condition
				dtbData.DefaultView.RowFilter = strFilter;
				dgrdData.Refresh();				
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

		/// <summary>
		/// Search for specific WORK Order
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnSearchBeginPO_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnVendorName_Click()";
			try
			{
				SelectPONo(METHOD_NAME, true);
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

		private void btnClose_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnClose_Click";
			try
			{
				mSelectedRows = new Hashtable();
				this.DialogResult = DialogResult.No;
				this.Close();
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
		
		private void chkSelectAll_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".chkSelectAll_Click";
			try
			{
				
				for (int i=0 ; i < dgrdData.RowCount; i++) 
				{					
					dgrdData[i, SELECTED_COL] = chkSelectAll.Checked? 1: 0;
				}
				
				dgrdData.UpdateData();				
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

		private void dgrdData_BeforeColEdit(object sender, C1.Win.C1TrueDBGrid.BeforeColEditEventArgs e)
		{
			const string METHOD_NAME = THIS + ".dgrdData_BeforeColEdit";
			try
			{
				if (e.Column.DataColumn.DataField != SELECTED_COL)
				{
					e.Cancel = true;
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

		private void btnSelect_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnSelect_Click";			
			try
			{	
				int intIndex = 0;
				mSelectedRows = new Hashtable();
				for(int i =0; i< dtbData.Rows.Count; i++)
				{
					if(bool.Parse(dtbData.Rows[i][SELECTED_COL].ToString()))
					{
						mSelectedRows.Add(intIndex++, dtbData.Rows[i]);
					}
				}

				if(mSelectedRows.Count == 0)
				{
					PCSMessageBox.Show(ErrorCode.MESSAGE_RGV_MUST_SELECT_PURCHASE_ORDER, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return;
				}

				this.DialogResult = DialogResult.OK;
				this.Close();
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

		private void txtBeginPO_Leave(object sender, EventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtBeginPO_Leave()";
			try
			{
				OnLeaveControl(sender, e);
				//Exit immediately if BIN is empty				
				if(txtBeginPO.Text.Length == 0)
				{
					txtBeginPO.Tag = ZERO_STRING;
					return;
				}
				else if(!txtBeginPO.Modified)
				{
					return;
				}
				
				SelectPONo(METHOD_NAME, false);
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

		private void txtBeginPO_KeyDown(object sender, KeyEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtBeginPO_KeyDown()";

			try
			{
				if ((e.KeyCode == Keys.F4) && (btnSearchBeginPO.Enabled))
				{
					SelectPONo(METHOD_NAME, true);
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
		
		private void SelectPurchaseOrders_Closing(object sender, CancelEventArgs e)
		{
			try
			{
				if(this.DialogResult != DialogResult.OK)
				{
					mSelectedRows = new Hashtable();
				}
			}
			catch
			{}
		}
		
		private void dgrdData_AfterColUpdate(object sender, C1.Win.C1TrueDBGrid.ColEventArgs e)
		{
			const string METHOD_NAME = THIS + ".dgrdData_AfterColUpdate()";

			try
			{
				if(e.Column.DataColumn.DataField == SELECTED_COL)
				{
					bool blnCheckAll = true;
					for(int i =0; i < dgrdData.RowCount; i++)
					{
						blnCheckAll &= bool.Parse(dgrdData[i, SELECTED_COL].ToString());
					}
					chkSelectAll.Checked = blnCheckAll;
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
		

		private void dtmFromPostDate_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".dtmFromPostDate_Validating()";

			try
			{
				if(!dtmFromDeliveryDate.ValueIsDbNull
				&& !dtmFromDeliveryDate.Text.Trim().Equals(string.Empty)
				&& !dtmToDeliveryDate.ValueIsDbNull
				&& !dtmToDeliveryDate.Text.Trim().Equals(string.Empty)
				)
				{
					if(DateTime.Parse(dtmFromDeliveryDate.Value.ToString()) > DateTime.Parse(dtmToDeliveryDate.Value.ToString()))
					{
						PCSMessageBox.Show(ErrorCode.MESSAGE_MP_PERIODDATE, MessageBoxIcon.Exclamation);
						e.Cancel = true;
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

		private void dtmToPostDate_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".dtmToPostDate_Validating()";

			try
			{
				if(!dtmFromDeliveryDate.ValueIsDbNull
				&& !dtmFromDeliveryDate.Text.Trim().Equals(string.Empty)
				&& !dtmToDeliveryDate.ValueIsDbNull
				&& !dtmToDeliveryDate.Text.Trim().Equals(string.Empty)
				)
				{
					if(DateTime.Parse(dtmFromDeliveryDate.Value.ToString()) > DateTime.Parse(dtmToDeliveryDate.Value.ToString()))
					{
						PCSMessageBox.Show(ErrorCode.MESSAGE_MP_PERIODDATE, MessageBoxIcon.Exclamation);
						e.Cancel = true;
					}
				}
			}
			catch(Exception ex)
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
		
		#endregion Event Processing		
	}
}