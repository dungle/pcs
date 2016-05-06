using System;
using System.Data;
using System.Collections;
using System.Windows.Forms;
using C1.Win.C1Input;
using C1.Win.C1TrueDBGrid;
using PCSComMaterials.Plan.BO;
using PCSComMaterials.Plan.DS;
using PCSComProcurement.Purchase.BO;
using PCSComProcurement.Purchase.DS;
using PCSComProduction.DCP.BO;
using PCSComProduction.WorkOrder.DS;
using PCSComUtils.Common;
using PCSComUtils.Common.BO;
using PCSComUtils.Framework.ReportFrame.BO;
using PCSComUtils.Framework.ReportFrame.DS;
using PCSComUtils.MasterSetup.BO;
using PCSComUtils.MasterSetup.DS;
using PCSComUtils.PCSExc;
using PCSProcurement.Purchase;
using PCSUtils.Framework.ReportFrame;
using PCSUtils.Log;
using PCSUtils.Utils;
using PCSProduction.WorkOrder;
using PCSComProduction.WorkOrder.BO;
using DCOptionsBO = PCSComProduction.DCP.BO.DCOptionsBO;
using PCSComUtils.DataContext;
using System.Linq;

namespace PCSMaterials.Mps
{
	/// <summary>
	/// Summary description for CPODataViewer.
	/// </summary>
	public partial class CPODataViewer : System.Windows.Forms.Form
	{
		#region Declaration

		#region Constants
		
		private const string THIS = "PCSMaterials.Mps.CPODataViewer";
		private const string ZERO_STRING = "0";		
		private const string VIEW_PRODUCTINFOR = "V_ProductInWorkCenter";
		#endregion Constants

		#region Private Variables

		private UtilsBO boUtils = new UtilsBO();
		private PurchaseOrderBO boPurchaseOrder = new PurchaseOrderBO();
		private WorkOrderBO boWorkOrder = new WorkOrderBO();
		private int intToPOorToWO = 0;
		
		CPODataViewerBO boCPODataViewer = new CPODataViewerBO();
		private DataTable dtbGridLayOut;
		private DataTable dtbCPODetail;
		private bool blnDataIsValid = false;
		MessageBoxFormForItems frmMessageBoxForm;
		DataTable dtbListErrorItem;
		const string REASON_FLD = "Reason";
		bool blnConvertPOSuccess = false;
		bool blnConvertWOSuccess = false;
		string strMasterIDToUpdate = "0";
		string strCPOIDToDelete = "0";
		//string strMasterIDToUpdate = string.Empty;
		ArrayList arrMasterIDToUpdate = new ArrayList();
		DataTable dtbVendor = new DataTable(MST_PartyTable.TABLE_NAME);
		#endregion	

		#endregion Declaration

		public CPODataViewer()
		{
			InitializeComponent();
		}

		#region Methods
		
		/// <summary>
		/// Fill related data on controls when select Vendor
		/// </summary>
		private bool SelectVendor(bool pblnAlwaysShowDialog)
		{	
			const string VENDOR_VIEW = "v_Vendor";		

			//Call OpenSearchForm for selecting Vendor
			DataRowView drwResult = FormControlComponents.OpenSearchForm(VENDOR_VIEW, MST_PartyTable.CODE_FLD, txtVendor.Text, null, pblnAlwaysShowDialog);

			//If has Vendor matched searching condition, fill values to form's controls
			if(drwResult != null)
			{			
				txtVendor.Text = drwResult[MST_PartyTable.CODE_FLD].ToString();
				txtVendor.Tag  = drwResult[MST_PartyTable.PARTYID_FLD];
				
				//reset modify status
				txtVendor.Modified = false;				
			}				
			else if(!pblnAlwaysShowDialog)
			{
				txtVendor.Focus();
				return false;
			}

			//Return true = ok
			return true;			
		}
		
		/// <summary>
		/// Processing control get focus event
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author> Tran Quoc Tuan </author>
		/// <created date> 
		/// 15 June 2005
		/// </created>
		private void OnEnterControl(object sender, EventArgs e)
		{
			try
			{
				FormControlComponents.OnEnterControl(sender, e);
			}
			catch
			{}
		}
		
		/// <summary>
		/// Processing control get focus event
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author> Tran Quoc Tuan </author>
		/// <created date> 
		/// 15 June 2005
		/// </created>
		private void OnLeaveControl(object sender, EventArgs e)
		{
			try
			{
				FormControlComponents.OnLeaveControl(sender, e);
			}
			catch
			{}
		}

		private bool ValidateData()
		{
			try
			{
				//Check Cycle no
				if (FormControlComponents.CheckMandatory(txtCycle))
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Exclamation);
					txtCycle.Focus();				
					return false;
				}
				if (dgrdData.RowCount <= 0)
				{
					PCSMessageBox.Show(ErrorCode.MESSAGE_INPUT_AT_LEAST_RECORD_IN_GRID,MessageBoxIcon.Exclamation);
					dgrdData.Focus();
					return false;
				}
				//variable to indicate grid's row index
				for (int i = 0; i < dgrdData.RowCount; i++)
				{
					if(dgrdData[i, MTR_CPOTable.QUANTITY_FLD].Equals(DBNull.Value)
						|| dgrdData[i, MTR_CPOTable.QUANTITY_FLD].ToString().Equals(string.Empty)
						|| dgrdData[i, MTR_CPOTable.QUANTITY_FLD].ToString().Equals(ZERO_STRING)
						|| !FormControlComponents.IsPositiveNumeric(dgrdData[i, MTR_CPOTable.QUANTITY_FLD].ToString())
						)
					{
						// Please input Item field for each records.
						PCSMessageBox.Show(ErrorCode.MESSAGE_OUTPROCESSING_INVALID_NUMBER, MessageBoxIcon.Exclamation);
						dgrdData.Row = i;
						dgrdData.Col = dgrdData.Columns.IndexOf(dgrdData.Columns[MTR_CPOTable.QUANTITY_FLD]);
						dgrdData.Focus();
						return false;
					}
					if(dgrdData[i, PRO_DCPResultDetailTable.SAFETYSTOCKAMOUNT].ToString() != string.Empty)
					{
						decimal decSafetyStockQty = 0;
						try
						{
							decSafetyStockQty = decimal.Parse(dgrdData[i, PRO_DCPResultDetailTable.SAFETYSTOCKAMOUNT].ToString());
						}
						catch
						{
							PCSMessageBox.Show(ErrorCode.MESSAGE_OUTPROCESSING_INVALID_NUMBER, MessageBoxIcon.Exclamation);
							dgrdData.Row = i;
							dgrdData.Col = dgrdData.Columns.IndexOf(dgrdData.Columns[PRO_DCPResultDetailTable.SAFETYSTOCKAMOUNT]);
							dgrdData.Focus();
							return false;

						}
						if (decSafetyStockQty > decimal.Parse(dgrdData[i, MTR_CPOTable.QUANTITY_FLD].ToString()))
						{
							// Please input Order quantity field for each records.
							string[] strParam = new string[2];
							strParam[0] = "Quantity";
							strParam[1] = "Safety Stock Quantity";
							PCSMessageBox.Show(ErrorCode.MESSAGE_GREATER_THAN, MessageBoxIcon.Exclamation, strParam);
							dgrdData.Row = i;
							dgrdData.Col = dgrdData.Columns.IndexOf(dgrdData.Columns[PRO_DCPResultDetailTable.SAFETYSTOCKAMOUNT]);
							dgrdData.Focus();
							return false;
						}
					}
					if(dgrdData[i, MTR_CPOTable.STARTDATE_FLD].ToString().Trim().Equals(string.Empty)
						|| dgrdData[i, MTR_CPOTable.STARTDATE_FLD].Equals(DBNull.Value)
						)
					{
						PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Exclamation);
						dgrdData.Row = i;
						dgrdData.Col = dgrdData.Columns.IndexOf(dgrdData.Columns[MTR_CPOTable.STARTDATE_FLD]);
						dgrdData.Focus();
						return false;
					}

					if(dgrdData[i, MTR_CPOTable.DUEDATE_FLD].ToString().Trim().Equals(string.Empty)
						|| dgrdData[i, MTR_CPOTable.DUEDATE_FLD].Equals(DBNull.Value)
						)
					{
						PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Exclamation);
						dgrdData.Row = i;
						dgrdData.Col = dgrdData.Columns.IndexOf(dgrdData.Columns[MTR_CPOTable.DUEDATE_FLD]);
						dgrdData.Focus();
						return false;
					}

					if(DateTime.Parse(dgrdData[i, MTR_CPOTable.STARTDATE_FLD].ToString()) > DateTime.Parse(dgrdData[i, MTR_CPOTable.DUEDATE_FLD].ToString()))
					{
						// Please input Item field for each records.
						PCSMessageBox.Show(ErrorCode.MESSAGE_ENDDATE_GREATER_THAN_BEGINDATE, MessageBoxIcon.Exclamation);
						dgrdData.Row = i;
						dgrdData.Col = dgrdData.Columns.IndexOf(dgrdData.Columns[MTR_CPOTable.DUEDATE_FLD]);
						dgrdData.Focus();
						return false;
					}
					if(cboPlanType.Text == lblDCP.Text)
					if(dgrdData[i, PRO_ShiftTable.SHIFTDESC_FLD].ToString() ==  string.Empty)
					{
						// Please input Item field for each records.
						PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Exclamation);
						dgrdData.Row = i;
						dgrdData.Col = dgrdData.Columns.IndexOf(dgrdData.Columns[PRO_ShiftTable.SHIFTDESC_FLD]);
						dgrdData.Focus();
						return false;
					}
					
				}
				int intRowIndex = -1;
				if(cboPlanType.Text == lblDCP.Text)
				foreach (DataRow drowRow in dtbCPODetail.Rows)
				{
					intRowIndex++;
					//Check data has just entered
					if(drowRow.RowState == DataRowState.Added) 
					{
						//Items
						if(drowRow[ITM_ProductTable.TABLE_NAME + ITM_ProductTable.CODE_FLD].ToString().Trim().Equals(string.Empty)
							|| drowRow[ITM_ProductTable.TABLE_NAME + ITM_ProductTable.CODE_FLD].Equals(DBNull.Value))
						{
							PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Exclamation);
							dgrdData.Row = intRowIndex;
							dgrdData.Col = dgrdData.Columns.IndexOf(dgrdData.Columns[ITM_ProductTable.TABLE_NAME + ITM_ProductTable.CODE_FLD]);
							dgrdData.Focus();
							return false;
						}

						if(drowRow[ITM_ProductTable.TABLE_NAME + ITM_ProductTable.DESCRIPTION_FLD].ToString().Trim().Equals(string.Empty)
							|| drowRow[ITM_ProductTable.TABLE_NAME + ITM_ProductTable.DESCRIPTION_FLD].Equals(DBNull.Value))
						{
							PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Exclamation);
							dgrdData.Row = intRowIndex;
							dgrdData.Col = dgrdData.Columns.IndexOf(dgrdData.Columns[ITM_ProductTable.TABLE_NAME + ITM_ProductTable.DESCRIPTION_FLD]);
							dgrdData.Focus();
							return false;
						}
						//Shift
						if(drowRow[PRO_ShiftTable.SHIFTDESC_FLD].ToString().Trim().Equals(string.Empty)
							|| drowRow[PRO_ShiftTable.SHIFTDESC_FLD].Equals(DBNull.Value))
						{
							PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Exclamation);
							dgrdData.Row = intRowIndex;
							dgrdData.Col = dgrdData.Columns.IndexOf(dgrdData.Columns[PRO_ShiftTable.SHIFTDESC_FLD]);
							dgrdData.Focus();
							return false;
						}	
					}
				}
				return true;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message, ex);
			}
		}

		/// <summary>
		/// Buid condition
		/// </summary>
		/// <author> Tran Quoc Tuan </author>
		/// <created date> 
		/// 10 Aug 2005
		/// </created>
		private bool BuildConditionHashTable(out Hashtable phtbCondition, out string pstrExtraCondition)
		{
			const string SQL_DATETIME_FORMAT = "yyyy-MM-dd";
			const string ENDDAY_TIME_VALUE = " 23:59:59";

			phtbCondition = new Hashtable();
			pstrExtraCondition = "1=1";
				
			//Check if user does not select plan type
			if(cboCCN.SelectedIndex <0)
			{
				PCSMessageBox.Show(ErrorCode.MESSAGE_CPODATAVIEWER_PLEASE_SELECT_CCN);
				cboCCN.Focus();
				return false;
			}
				
			//Check if user does not select plan type
			if((cboPlanType.SelectedIndex == 0) || (cboPlanType.Text.Trim() == string.Empty))
			{
				PCSMessageBox.Show(ErrorCode.MESSAGE_CPODATAVIEWER_PLEASE_SELECT_PLAN_TYPE);
				cboPlanType.Focus();
				return false;
			}

			//Check if user does not select Cycle
			if(txtCycle.Text.Trim() == string.Empty || txtCycle.Tag.ToString().Equals(ZERO_STRING))
			{
				PCSMessageBox.Show(ErrorCode.MESSAGE_CPODATAVIEWER_PLEASE_SELECT_CYCLE);
				txtCycle.Focus();
				return false;
			}

			//Check if user does not select Master Location
			if((txtMasLoc.Text.Trim() == string.Empty|| txtMasLoc.Tag.ToString().Equals(ZERO_STRING)) && (cboPlanType.Text.Trim() != lblDCP.Text.Trim()))
			{
				PCSMessageBox.Show(ErrorCode.MESSAGE_CPODATAVIEWER_PLEASE_SELECT_MASLOC);
				txtMasLoc.Focus();
				return false;
			}

			if(!dtmFromDueDate.ValueIsDbNull || !dtmFromDueDate.Text.Equals(string.Empty))
			{
				if(dtmToDueDate.ValueIsDbNull || dtmToDueDate.Text.Equals(string.Empty))
				{
					PCSMessageBox.Show(ErrorCode.MESSAGE_MANDATORY_FIELD_REQUIRED, MessageBoxIcon.Exclamation, new string[]{lblToDueDate.Text});
					dtmToDueDate.Focus();
					return false;
				}
				else
				{
					DateTime dtmFrom = DateTime.Parse(dtmFromDueDate.Value.ToString());
					DateTime dtmTo = DateTime.Parse(dtmToDueDate.Value.ToString());
							
					pstrExtraCondition += " AND " + MTR_CPOTable.DUEDATE_FLD + " >='" + dtmFrom.ToString(SQL_DATETIME_FORMAT);
					pstrExtraCondition += "' AND " + MTR_CPOTable.DUEDATE_FLD + "<= '" + dtmTo.ToString(SQL_DATETIME_FORMAT) + ENDDAY_TIME_VALUE + "'";
				}
			}				

			//Add CCN, Mas Loc, IsMPS condition to filter
			if (cboPlanType.Text.Trim() != lblDCP.Text.Trim())
			{
				phtbCondition.Add(MTR_CPOTable.TABLE_NAME + "." + MTR_CPOTable.CCNID_FLD, cboCCN.SelectedValue);
				phtbCondition.Add(MTR_CPOTable.TABLE_NAME + "." + MTR_CPOTable.MASTERLOCATIONID_FLD, txtMasLoc.Tag);
			}
			else
				phtbCondition.Add(PRO_DCOptionMasterTable.TABLE_NAME + "." + PRO_DCOptionMasterTable.CCNID_FLD, cboCCN.SelectedValue);

			phtbCondition.Add(PRO_ProductionLineTable.TABLE_NAME + "." + PRO_ProductionLineTable.PRODUCTIONLINEID_FLD, txtProductionLine.Tag);

			if (cboPlanType.Text.Trim() != lblDCP.Text.Trim())
				phtbCondition.Add(MTR_CPOTable.TABLE_NAME + "." + MTR_CPOTable.ISMPS_FLD, cboPlanType.SelectedIndex - 1);
				
			if(cboPlanType.Text == PlanTypeEnum.MRP.ToString()) //MRP
				phtbCondition.Add(MTR_CPOTable.TABLE_NAME + "." + MTR_CPOTable.MRPCYCLEOPTIONMASTERID_FLD, txtCycle.Tag);

			if (cboPlanType.Text == PlanTypeEnum.MPS.ToString()) //MPS
				phtbCondition.Add(MTR_CPOTable.TABLE_NAME + "." + MTR_CPOTable.MPSCYCLEOPTIONMASTERID_FLD, txtCycle.Tag);

			if (cboPlanType.Text.Trim() == lblDCP.Text.ToString())
				phtbCondition.Add(PRO_DCOptionMasterTable.TABLE_NAME + "." + PRO_DCOptionMasterTable.DCOPTIONMASTERID_FLD, txtCycle.Tag);
				
			if(!txtCategory.Tag.ToString().Equals(ZERO_STRING))
				phtbCondition.Add(ITM_CategoryTable.TABLE_NAME + "." + ITM_CategoryTable.CATEGORYID_FLD, txtCategory.Tag);			
				
			if(txtVendor.Text.Trim() != string.Empty && txtVendor.Tag != null)
				phtbCondition.Add(MST_PartyTable.TABLE_NAME + "." + MST_PartyTable.PARTYID_FLD, txtVendor.Tag);
				
			if (txtPartNumber.Text != string.Empty)
				phtbCondition.Add(ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.PRODUCTID_FLD, txtPartNumber.Tag);

			return true;
		}

		/// <summary>
		/// Reformat data grid
		/// </summary>
		/// <author> Tran Quoc Tuan </author>
		/// <created date> 
		/// 10 Aug 2005
		/// </created>
		private void FormatDataGrid()
		{
            //Restore layout
            FormControlComponents.RestoreGridLayout(dgrdData, dtbGridLayOut);

            dgrdData.AllowAddNew = false;
            dgrdData.AllowDelete = false;
            dgrdData.AllowUpdate = true;

            //Change presentation
            dgrdData.Columns[MTR_CPOTable.CONVERTED_FLD].ValueItems.Presentation = PresentationEnum.CheckBox;
            dgrdData.Columns[MTR_CPODS.SELECT_COLUMN].ValueItems.Presentation = PresentationEnum.CheckBox;
            dgrdData.Columns[PRO_DCPResultDetailTable.ISMANUAL_FLD].ValueItems.Presentation = PresentationEnum.CheckBox;

            //Lock all columns
            foreach (C1DisplayColumn col in dgrdData.Splits[0].DisplayColumns)
            {
                col.Locked = true;
            }

            //unlock for editing
            dgrdData.Splits[0].DisplayColumns[MTR_CPODS.SELECT_COLUMN].Locked = false;
            dgrdData.Splits[0].DisplayColumns[MTR_CPOTable.QUANTITY_FLD].Locked = false;
            dgrdData.Splits[0].DisplayColumns[PRO_DCPResultDetailTable.SAFETYSTOCKAMOUNT].Locked = false;
            dgrdData.Splits[0].DisplayColumns[MTR_CPOTable.STARTDATE_FLD].Locked = false;
            dgrdData.Splits[0].DisplayColumns[MTR_CPOTable.DUEDATE_FLD].Locked = false;
            dgrdData.Splits[0].DisplayColumns[MTR_CPOTable.DUEDATE_FLD].Style.HorizontalAlignment = AlignHorzEnum.Center;
            dgrdData.Splits[0].DisplayColumns[MTR_CPOTable.STARTDATE_FLD].Style.HorizontalAlignment = AlignHorzEnum.Center;
            //For selecting Items
            dgrdData.Splits[0].DisplayColumns[ITM_ProductTable.TABLE_NAME + ITM_ProductTable.CODE_FLD].Locked = false;
            dgrdData.Splits[0].DisplayColumns[ITM_ProductTable.TABLE_NAME + ITM_ProductTable.DESCRIPTION_FLD].Locked = false;
            dgrdData.Splits[0].DisplayColumns[ITM_ProductTable.TABLE_NAME + ITM_ProductTable.DESCRIPTION_FLD].Button = true;
            dgrdData.Splits[0].DisplayColumns[ITM_ProductTable.TABLE_NAME + ITM_ProductTable.CODE_FLD].Button = true;
            dgrdData.Splits[0].DisplayColumns[PRO_ShiftTable.SHIFTDESC_FLD].Button = true;
            dgrdData.Splits[0].DisplayColumns[PRO_ShiftTable.SHIFTDESC_FLD].Locked = false;

            //Column button
            dgrdData.Splits[0].DisplayColumns[PRO_WorkOrderMasterTable.TABLE_NAME + PRO_WorkOrderMasterTable.WORKORDERNO_FLD].Button = true;
            dgrdData.Splits[0].DisplayColumns[PRO_WorkOrderMasterTable.TABLE_NAME + PRO_WorkOrderMasterTable.WORKORDERNO_FLD].ButtonText = true;

            dgrdData.Splits[0].DisplayColumns[PO_PurchaseOrderMasterTable.TABLE_NAME + PO_PurchaseOrderMasterTable.CODE_FLD].Button = true;
            dgrdData.Splits[0].DisplayColumns[PO_PurchaseOrderMasterTable.TABLE_NAME + PO_PurchaseOrderMasterTable.CODE_FLD].ButtonText = true;

            //Change display format
            dgrdData.Columns[MTR_CPOTable.QUANTITY_FLD].NumberFormat = Constants.DECIMAL_NUMBERFORMAT;

            dgrdData.Columns[PRO_DCPResultDetailTable.SAFETYSTOCKAMOUNT].NumberFormat = Constants.DECIMAL_NUMBERFORMAT;
            dgrdData.Splits[0].DisplayColumns[PRO_DCPResultDetailTable.SAFETYSTOCKAMOUNT].HeadingStyle.HorizontalAlignment = AlignHorzEnum.Far;
            dgrdData.Columns[MTR_CPOTable.STARTDATE_FLD].NumberFormat = Constants.DATETIME_FORMAT_HOUR;
            dgrdData.Columns[MTR_CPOTable.DUEDATE_FLD].NumberFormat = Constants.DATETIME_FORMAT_HOUR;

            //Assign editor to Date Columns
            dgrdData.Columns[MTR_CPOTable.STARTDATE_FLD].Editor = dtmDate;
            dgrdData.Columns[MTR_CPOTable.DUEDATE_FLD].Editor = dtmDate;
            dgrdData.Columns[MTR_CPOTable.QUANTITY_FLD].Editor = numQuantity;
            if (cboPlanType.SelectedItem.ToString() == lblDCP.Text.Trim()) //Change to DCP mode
            {
                txtMasLoc.Text = string.Empty;

                #region HACK: Trada 21-04-2006
                //add some new columns
                //Shift column
                dgrdData.Splits[0].DisplayColumns[PRO_ShiftTable.SHIFTDESC_FLD].Visible = true;
                //Safety Stock Qty Column
                dgrdData.Splits[0].DisplayColumns[PRO_DCPResultDetailTable.SAFETYSTOCKAMOUNT].Visible = true;
                //IsManual Column
                dgrdData.Splits[0].DisplayColumns[PRO_DCPResultDetailTable.ISMANUAL_FLD].Visible = true;
                //config new columns
                dgrdData.Columns[PRO_DCPResultDetailTable.SAFETYSTOCKAMOUNT].NumberFormat = Constants.DECIMAL_NUMBERFORMAT;
                dgrdData.Splits[0].DisplayColumns[PRO_DCPResultDetailTable.SAFETYSTOCKAMOUNT].HeadingStyle.HorizontalAlignment = AlignHorzEnum.Far;
                dgrdData.Splits[0].DisplayColumns[PRO_DCPResultDetailTable.ISMANUAL_FLD].HeadingStyle.HorizontalAlignment = AlignHorzEnum.Center;

                dgrdData.Splits[0].DisplayColumns[ITM_ProductTable.TABLE_NAME + ITM_ProductTable.CODE_FLD].Button = true;
                dgrdData.Splits[0].DisplayColumns[PRO_ShiftTable.SHIFTDESC_FLD].Button = true;
                dgrdData.Splits[0].DisplayColumns[ITM_ProductTable.TABLE_NAME + ITM_ProductTable.DESCRIPTION_FLD].Button = true;

                dgrdData.Columns[PRO_DCPResultDetailTable.TOTALSECOND_FLD].NumberFormat = Constants.DECIMAL_NUMBERFORMAT;
                dgrdData.Splits[0].DisplayColumns[PRO_DCPResultDetailTable.TOTALSECOND_FLD].Visible = true;
                dgrdData.Splits[0].DisplayColumns[PRO_DCPResultDetailTable.TOTALSECOND_FLD].Locked = false;

                #endregion END: Trada 21-04-2006
            }
            else //Change to default mode
            {
                #region HACK: Trada 21-04-2006
                dgrdData.AllowAddNew = false;
                //invisible some columns
                //Shift column
                dgrdData.Splits[0].DisplayColumns[PRO_ShiftTable.SHIFTDESC_FLD].Visible = false;
                //Safety Stock Qty Column
                dgrdData.Splits[0].DisplayColumns[PRO_DCPResultDetailTable.SAFETYSTOCKAMOUNT].Visible = false;
                //IsManual Column
                dgrdData.Splits[0].DisplayColumns[PRO_DCPResultDetailTable.ISMANUAL_FLD].Visible = false;
                try
                {
                    dgrdData.Splits[0].DisplayColumns[PRO_DCPResultDetailTable.TOTALSECOND_FLD].Visible = false;
                    dgrdData.Splits[0].DisplayColumns[PRO_DCPResultDetailTable.TOTALSECOND_FLD].Locked = true;
                }
                catch { }
                #endregion END: Trada 21-04-2006
            }
            //HACKED by Tuan TQ. 22 Dec, 2005. Apply proposal no. 3159
            dgrdData.Splits[0].DisplayColumns[MTR_CPOTable.CPOID_FLD].Visible = false;
            dgrdData.Splits[0].DisplayColumns[MTR_CPOTable.CPOID_FLD].AllowSizing = false;
		}

		/// <summary>
		/// Build structure of MTR_CPO table for binding to grid
		/// </summary>
		/// <remarks>
		/// Structure of this table based on struct which be returned by calling
		/// MTR_CPODSDS.Search() method.
		/// So we should keep them always are identical.
		/// </remarks>
		/// <returns>DataTable</returns>
		private DataTable BuildCPODetailTable()
		{
            //Create table
            DataTable dtbDetail = new DataTable(MTR_CPOTable.TABLE_NAME);
            //Add columns

            //HACKED by Tuan TQ. 22 Dec, 2005. Apply proposal no. 3159
            dtbDetail.Columns.Add(MTR_CPODS.LINE_NUMBER_COLUMN, typeof(System.Int32));
            //End
            dtbDetail.Columns.Add(MTR_CPODS.SELECT_COLUMN, typeof(System.Boolean));
            dtbDetail.Columns.Add(MTR_CPOTable.CONVERTED_FLD, typeof(System.Boolean));
            dtbDetail.Columns.Add(MTR_CPOTable.CPOID_FLD, typeof(System.Int32));

            dtbDetail.Columns.Add(ITM_CategoryTable.CATEGORYID_FLD, typeof(System.Int32));
            dtbDetail.Columns.Add(ITM_CategoryTable.TABLE_NAME + ITM_CategoryTable.CODE_FLD, typeof(System.String));

            dtbDetail.Columns.Add(ITM_ProductTable.PRODUCTID_FLD, typeof(System.Int32));
            dtbDetail.Columns.Add(ITM_ProductTable.TABLE_NAME + ITM_ProductTable.CODE_FLD, typeof(System.String));
            dtbDetail.Columns.Add(ITM_ProductTable.TABLE_NAME + ITM_ProductTable.DESCRIPTION_FLD, typeof(System.String));
            dtbDetail.Columns.Add(ITM_ProductTable.TABLE_NAME + ITM_ProductTable.REVISION_FLD, typeof(System.String));

            dtbDetail.Columns.Add(MST_UnitOfMeasureTable.TABLE_NAME + MST_UnitOfMeasureTable.CODE_FLD, typeof(System.String));

            //HACK: added by Tuan TQ. 17 Feb, 2006
            dtbDetail.Columns.Add(MST_PartyTable.TABLE_NAME + MST_PartyTable.CODE_FLD, typeof(System.String));
            //End hack

            dtbDetail.Columns.Add(MST_WorkCenterTable.TABLE_NAME + MST_WorkCenterTable.CODE_FLD, typeof(System.String));

            dtbDetail.Columns.Add(PRO_WorkOrderMasterTable.TABLE_NAME + PRO_WorkOrderMasterTable.WORKORDERNO_FLD, typeof(System.String));
            dtbDetail.Columns.Add(MTR_CPOTable.QUANTITY_FLD, typeof(System.Double));
            dtbDetail.Columns.Add(MTR_CPOTable.STARTDATE_FLD, typeof(System.DateTime));
            dtbDetail.Columns.Add(MTR_CPOTable.DUEDATE_FLD, typeof(System.DateTime));
            dtbDetail.Columns.Add(PRO_ShiftTable.SHIFTDESC_FLD, typeof(System.String));
            dtbDetail.Columns.Add(PRO_ShiftTable.SHIFTID_FLD, typeof(System.Int32));
            dtbDetail.Columns.Add(PRO_DCPResultDetailTable.TOTALSECOND_FLD, typeof(decimal));
            dtbDetail.Columns.Add(PO_PurchaseOrderMasterTable.TABLE_NAME + PO_PurchaseOrderMasterTable.CODE_FLD, typeof(System.String));
            dtbDetail.Columns.Add(PRO_WorkOrderMasterTable.WORKORDERMASTERID_FLD, typeof(System.Int32));
            dtbDetail.Columns.Add(PO_PurchaseOrderMasterTable.PURCHASEORDERMASTERID_FLD, typeof(System.Int32));

            dtbDetail.Columns.Add(MTR_CPOTable.CCNID_FLD, typeof(System.Int32));
            dtbDetail.Columns.Add(MTR_CPOTable.MASTERLOCATIONID_FLD, typeof(System.Int32));
            dtbDetail.Columns.Add(MTR_CPOTable.ISMPS_FLD, typeof(System.Boolean));

            dtbDetail.Columns.Add(MTR_CPOTable.REFMASTERID_FLD, typeof(System.Int32));
            dtbDetail.Columns.Add(MTR_CPOTable.REFTYPE_FLD, typeof(System.Int32));
            dtbDetail.Columns.Add(MTR_CPOTable.NETAVAILABLEQUANTITY_FLD, typeof(System.Double));
            dtbDetail.Columns.Add(MTR_CPOTable.STOCKUMID_FLD, typeof(System.Int32));
            dtbDetail.Columns.Add(MTR_CPOTable.POGENERATEDID_FLD, typeof(System.Int32));
            dtbDetail.Columns.Add(MTR_CPOTable.WOGENERATEDID_FLD, typeof(System.Int32));
            dtbDetail.Columns.Add(MTR_CPOTable.MRPCYCLEOPTIONMASTERID_FLD, typeof(System.Int32));
            dtbDetail.Columns.Add(MTR_CPOTable.MPSCYCLEOPTIONMASTERID_FLD, typeof(System.Int32));

            dtbDetail.Columns.Add(PRO_DCPResultDetailTable.SAFETYSTOCKAMOUNT, typeof(System.Double));
            dtbDetail.Columns.Add(PRO_DCPResultDetailTable.ISMANUAL_FLD, typeof(System.Double));
            dtbDetail.Columns.Add(ITM_ProductTable.LTVARIABLETIME_FLD, typeof(decimal));

            dtbDetail.Columns.Add("FixLT", typeof(System.Decimal));

            return dtbDetail;
        }

		/// <summary>
		/// Clear all condition information on the form
		/// </summary>
		private void ClearSearchingCondition()
		{
			try
			{
				txtCategory.Text = string.Empty;
				txtCategory.Tag = ZERO_STRING;

				txtCycle.Text = string.Empty;
				txtCycle.Tag = ZERO_STRING;

				#region  DEL Trada 22-12-2005

				//				txtMasLoc.Text = string.Empty;
				//				txtMasLoc.Tag = ZERO_STRING;

				#endregion 

				txtProductionLine.Text = string.Empty;
				txtProductionLine.Tag = ZERO_STRING;
								
				txtPartNumber.Text = string.Empty;
				txtPartNumber.Tag = ZERO_STRING;

				txtPartName.Text = string.Empty;
				txtRevision.Text = string.Empty;

				txtVendor.Text = string.Empty;

				dtmFromDueDate.Value = DBNull.Value;
				dtmToDueDate.Value = DBNull.Value;

				cboPlanType.Focus();				
				btnSave.Enabled = false;				
			}			
			catch (Exception ex)
			{
				throw new Exception(ex.Message, ex);
			}	
		}

		///<summary>	
		/// Clear item information on the form
		/// </summary>
		private void ClearItemInfo()
		{
			try
			{
				txtPartNumber.Text = string.Empty;
				txtPartNumber.Tag = ZERO_STRING;

				txtPartName.Text = string.Empty;
				txtRevision.Text = string.Empty;
			}			
			catch (Exception ex)
			{
				throw new Exception(ex.Message, ex);
			}	
		}

		/// <summary>
		/// Fill related data on controls when select PartName
		/// </summary>
		/// <param name="pblnAlwaysShowDialog"></param>
		private bool SelectPartName(string pstrMethodName, bool pblnAlwaysShowDialog)
		{
			try
			{
				string strWhere = ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.CCNID_FLD + "=" + cboCCN.SelectedValue;
				if (txtCategory.Tag.ToString() != "0")
					strWhere += " AND " + ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.CATEGORYID_FLD + "=" + txtCategory.Tag.ToString();

				DataTable dtbResult = FormControlComponents.OpenSearchFormForMultiSelectedRow(ITM_ProductTable.TABLE_NAME, ITM_ProductTable.DESCRIPTION_FLD, txtPartName.Text, strWhere, pblnAlwaysShowDialog);
				
				if(dtbResult != null && dtbResult.Rows.Count > 0)
				{
					string strProductID = string.Empty;
					if (dtbResult.Rows.Count > 1)
					{
						foreach (DataRow drowData in dtbResult.Rows)
							strProductID += drowData[ITM_ProductTable.PRODUCTID_FLD] + ",";
						strProductID += "0";
						txtPartNumber.Text = "Multi Selection";
						txtPartNumber.Tag = strProductID;
						txtPartName.Text = "Multi Selection";
						txtRevision.Text = "Multi Selection";
					}
					else
					{
						txtPartNumber.Text = dtbResult.Rows[0][ITM_ProductTable.CODE_FLD].ToString();
						txtPartNumber.Tag = dtbResult.Rows[0][ITM_ProductTable.PRODUCTID_FLD];
						txtPartName.Text = dtbResult.Rows[0][ITM_ProductTable.DESCRIPTION_FLD].ToString();
						txtRevision.Text = dtbResult.Rows[0][ITM_ProductTable.REVISION_FLD].ToString();		
					}

					//Reset modify status
					txtPartName.Modified = false;					
					txtPartNumber.Modified = false;
				}
				else if(!pblnAlwaysShowDialog)
				{
					txtPartNumber.Focus();
					return false;
				}

				return true;
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
		/// Fill related data on controls when select Part Name
		/// </summary>
		/// <param name="pblnAlwaysShowDialog"></param>
		private bool SelectPartNumber(string pstrMethodName, bool pblnAlwaysShowDialog)
		{
			try
			{
				string strWhere = ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.CCNID_FLD + "=" + cboCCN.SelectedValue;
				if (txtCategory.Tag.ToString() != "0")
					strWhere += " AND " + ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.CATEGORYID_FLD + "=" + txtCategory.Tag.ToString();

				DataTable dtbResult = FormControlComponents.OpenSearchFormForMultiSelectedRow(ITM_ProductTable.TABLE_NAME, ITM_ProductTable.CODE_FLD, txtPartNumber.Text, strWhere, pblnAlwaysShowDialog);
				
				if(dtbResult != null && dtbResult.Rows.Count > 0)
				{
					string strProductID = string.Empty;
					if (dtbResult.Rows.Count > 1)
					{
						foreach (DataRow drowData in dtbResult.Rows)
							strProductID += drowData[ITM_ProductTable.PRODUCTID_FLD] + ",";
						strProductID += "0";
						txtPartNumber.Text = "Multi Selection";
						txtPartNumber.Tag = strProductID;
						txtPartName.Text = "Multi Selection";
						txtRevision.Text = "Multi Selection";
					}
					else
					{
						txtPartNumber.Text = dtbResult.Rows[0][ITM_ProductTable.CODE_FLD].ToString();
						txtPartNumber.Tag = dtbResult.Rows[0][ITM_ProductTable.PRODUCTID_FLD];
						txtPartName.Text = dtbResult.Rows[0][ITM_ProductTable.DESCRIPTION_FLD].ToString();
						txtRevision.Text = dtbResult.Rows[0][ITM_ProductTable.REVISION_FLD].ToString();		
					}

					//Reset modify status
					txtPartName.Modified = false;					
					txtPartNumber.Modified = false;
				}
				else if(!pblnAlwaysShowDialog)
				{
					txtPartNumber.Focus();
					return false;
				}

				return true;
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
		/// Fill related data on controls when select Master Location
		/// </summary>
		/// <param name="pblnAlwaysShowDialog"></param>
		private bool SelectMasterLocation(string pstrMethodName, bool pblnAlwaysShowDialog)
		{
			try				
			{
				Hashtable htbCriteria = new Hashtable();

				if(cboCCN.SelectedValue != null)
				{
					htbCriteria.Add(MST_MasterLocationTable.CCNID_FLD, cboCCN.SelectedValue);
				}
				else
				{
					htbCriteria.Add(MST_MasterLocationTable.CCNID_FLD, 0);
				}

				//Call OpenSearchForm for selecting Master Location
				DataRowView drwResult = FormControlComponents.OpenSearchForm(MST_MasterLocationTable.TABLE_NAME, MST_MasterLocationTable.CODE_FLD, txtMasLoc.Text, htbCriteria, pblnAlwaysShowDialog);
				
				// If has Master location matched searching condition, fill values to form's controls
				if(drwResult != null)
				{
					//Check if master location was changed then clear grid content
					txtMasLoc.Text = drwResult[MST_MasterLocationTable.CODE_FLD].ToString();
					txtMasLoc.Tag = drwResult[MST_MasterLocationTable.MASTERLOCATIONID_FLD];
					ClearItemInfo();					
					
					//Reset modify status
					txtMasLoc.Modified = false;
				}
				else if(!pblnAlwaysShowDialog)
				{
					txtMasLoc.Focus();
					return false;
				}

				return true;
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
		/// Fill related data on controls when select Master Location
		/// </summary>
		/// <param name="pblnAlwaysShowDialog"></param>
		private bool SelectCategory(string pstrMethodName, bool pblnAlwaysShowDialog)
		{
			try				
			{
				Hashtable htbCriteria = new Hashtable();		

				//Call OpenSearchForm for selecting Master Location
				DataRowView drwResult = FormControlComponents.OpenSearchForm(ITM_CategoryTable.TABLE_NAME, ITM_CategoryTable.CODE_FLD, txtCategory.Text, htbCriteria, pblnAlwaysShowDialog);
				
				// If has Master location matched searching condition, fill values to form's controls
				if(drwResult != null)
				{
					//Check if master location was changed then clear grid content
					txtCategory.Text = drwResult[ITM_CategoryTable.CODE_FLD].ToString();
					txtCategory.Tag = drwResult[ITM_CategoryTable.CATEGORYID_FLD];

					//Clear item infor
					ClearItemInfo();
					
					//Reset modify status
					txtCategory.Modified = false;
				
				}
				else if(!pblnAlwaysShowDialog)
				{					
					txtCategory.Focus();
					return false;
				}
				
				return true;
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
		/// Fill related data on controls when select BIN
		/// </summary>
		/// <param name="pblnAlwaysShowDialog"></param>
		private bool SelectCycle(string pstrMethodName, bool pblnAlwaysShowDialog)
		{
			try				
			{
				Hashtable htbCriteria = new Hashtable();
				DataRowView drwResult = null;
				
				if(cboCCN.SelectedValue != null)
				{
					htbCriteria.Add(MST_MasterLocationTable.CCNID_FLD, cboCCN.SelectedValue);
				}
				else
				{
					htbCriteria.Add(MST_MasterLocationTable.CCNID_FLD, 0);
				}
				
				if(cboPlanType.SelectedIndex == 0)
				{
					PCSMessageBox.Show(ErrorCode.MESSAGE_CPODATAVIEWER_PLEASE_SELECT_PLAN_TYPE);
					cboPlanType.Focus();
					return false;
				}

				if(cboPlanType.Text.ToString() == PlanTypeEnum.MPS.ToString())
				{
					//Call OpenSearchForm for selecting MPS planning cycle
					drwResult = FormControlComponents.OpenSearchForm(MTR_MPSCycleOptionMasterTable.TABLE_NAME, MTR_MPSCycleOptionMasterTable.CYCLE_FLD, txtCycle.Text, htbCriteria, pblnAlwaysShowDialog);
				}
				if (cboPlanType.Text.Trim() == PlanTypeEnum.MRP.ToString())
				{
					//Call OpenSearchForm for selecting MRP planning cycle
					drwResult = FormControlComponents.OpenSearchForm(MTR_MRPCycleOptionMasterTable.TABLE_NAME, MTR_MPSCycleOptionMasterTable.CYCLE_FLD, txtCycle.Text, htbCriteria, pblnAlwaysShowDialog);
				}
				if (cboPlanType.Text.Trim() == lblDCP.Text.Trim())
				{
					//Call OpenSearchForm for selecting MRP planning cycle
					drwResult = FormControlComponents.OpenSearchForm(PRO_DCOptionMasterTable.TABLE_NAME, PRO_DCOptionMasterTable.CYCLE_FLD, txtCycle.Text, htbCriteria, pblnAlwaysShowDialog);
				}
				
				// If has Master location matched searching condition, fill values to form's controls
				if (drwResult != null)
				{
					if(cboPlanType.Text.ToString() == PlanTypeEnum.MPS.ToString())
					{
						txtCycle.Text = drwResult[MTR_MPSCycleOptionMasterTable.CYCLE_FLD].ToString();
						txtCycle.Tag = drwResult[MTR_MPSCycleOptionMasterTable.MPSCYCLEOPTIONMASTERID_FLD];	
					}

					if(cboPlanType.Text.ToString() == PlanTypeEnum.MRP.ToString())
					{
						txtCycle.Text = drwResult[MTR_MRPCycleOptionMasterTable.CYCLE_FLD].ToString();
						txtCycle.Tag = drwResult[MTR_MRPCycleOptionMasterTable.MRPCYCLEOPTIONMASTERID_FLD];
					}

					if(cboPlanType.Text.ToString() == lblDCP.Text.Trim())
					{
						txtCycle.Text = drwResult[PRO_DCOptionMasterTable.CYCLE_FLD].ToString();
						txtCycle.Tag = drwResult[PRO_DCOptionMasterTable.DCOPTIONMASTERID_FLD];
					}

					//Reset modify status
					txtCycle.Modified = false;
				}
				else if(!pblnAlwaysShowDialog)
				{
					txtCycle.Focus();
					return false;
				}

				return true;
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
		/// Calculate information in the Detail table after saving data
		/// </summary>
		/// <author>Trada</author>
		/// <date>Saturday, June 10 2006</date>
		private void RecalculateDataAfterSaving()
		{
			//Get Data from database
			//Condition HashTable
			Hashtable htbCriteria = new Hashtable();
			string strExtraCondition = string.Empty;
				
			//Call BO's method to retrieve data				
			DataSet dstResult = new DataSet();
			if(BuildConditionHashTable(out htbCriteria, out strExtraCondition))
			{
				dstResult = boCPODataViewer.SearchForDCP(htbCriteria);	
				//Calculate data
				DataRow[] adrowResultDetail = null;
				foreach (DataRow drow in dstResult.Tables[0].Rows)
				{
					if (drow[PRO_DCPResultDetailTable.DCPRESULTMASTERID_FLD] != DBNull.Value)
					{
						adrowResultDetail = dstResult.Tables[0].Select(PRO_DCPResultDetailTable.DCPRESULTMASTERID_FLD 
							+ " = " + drow[PRO_DCPResultDetailTable.DCPRESULTMASTERID_FLD].ToString());
						if (adrowResultDetail.Length > 0)
						{
							decimal decTotalQuantity = 0;
							for (int i = 0; i < adrowResultDetail.Length; i++)
								decTotalQuantity += decimal.Parse(adrowResultDetail[i][PRO_DCPResultDetailTable.QUANTITY_FLD].ToString());
							foreach (DataRow drowNewResult in adrowResultDetail)
							{
								drowNewResult[PRO_DCPResultDetailTable.PERCENTAGE_FLD] = Convert.ToDecimal(drowNewResult[PRO_DCPResultDetailTable.QUANTITY_FLD])
									/decTotalQuantity * 100;
							}
						}
					}
				}
				//Update Database
				DCOptionsBO boDCOptions = new DCOptionsBO();
				boDCOptions.UpdateDataSetAfterSaving(dstResult);
			}
		}
		#endregion Methods
		
		#region Event Processing

		private void CPODataViewer_Load(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".CPODataViewer_Load()";
			try
			{
				dtmDate.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
				dtmDate.CustomFormat = Constants.DATETIME_FORMAT_HOUR;
				//dtmDate.TextDetached = true;

				numQuantity.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
				numQuantity.CustomFormat = Constants.DECIMAL_NUMBERFORMAT;

				txtNumRows.FormatType = FormatTypeEnum.CustomFormat;
				txtNumRows.CustomFormat = Constants.INTERGER_NUMBERFORMAT;
				//numQuantity.TextDetached = true;				
				
				//Set form security
				Security objSecurity = new Security();
				this.Name = THIS;
				if(objSecurity.SetRightForUserOnForm(this, SystemProperty.UserName) == 0)
				{
					// You don't have the right to view this item
					PCSMessageBox.Show(ErrorCode.MESSAGE_YOU_HAVE_NO_RIGHT_TO_VIEW, MessageBoxIcon.Warning);
					this.Close();
					return;
				}

				//Load CCN and set default CCN
				UtilsBO boUtils = new UtilsBO();
				DataSet dstCCN = boUtils.ListCCN();
				FormControlComponents.PutDataIntoC1ComboBox(cboCCN, dstCCN.Tables[MST_CCNTable.TABLE_NAME], MST_CCNTable.CODE_FLD, MST_CCNTable.CCNID_FLD, MST_CCNTable.TABLE_NAME);
				
				//Set default CCN for CNN combo box
				if (SystemProperty.CCNID != 0)
					cboCCN.SelectedValue = SystemProperty.CCNID;

				//initiate combo box of Plan type
				cboPlanType.Items.Clear();
				cboPlanType.Items.Add(string.Empty);
				cboPlanType.Items.Add(PlanTypeEnum.MRP.ToString());
				cboPlanType.Items.Add(PlanTypeEnum.MPS.ToString());
				cboPlanType.Items.Add(lblDCP.Text);
				cboPlanType.SelectedIndex = 0;
				
				ClearSearchingCondition();

				//get grid layout
				dtbGridLayOut = FormControlComponents.StoreGridLayout(dgrdData);
				
				//Bind data to grid then reformat it
				dtbCPODetail = BuildCPODetailTable();
				dgrdData.DataSource = dtbCPODetail;
				FormatDataGrid();
				dgrdData.FilterBar = true;

				btnNewWOConvert.Enabled = false;
				btnUpdateWorkOrder.Enabled = false;

				btnNewPOConvert.Enabled = false;
				btnExistingPOConvert.Enabled = false;
				btnPrint.Enabled = false;
				chkSelectAll.Enabled = false;

				//Fill Default Master Location 
				FormControlComponents.SetDefaultMasterLocation(txtMasLoc);

				// cache vendor list
				PartyBO boParty = new PartyBO();
				dtbVendor = boParty.ListVendor();
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

		private void chkSelectAll_CheckedChanged(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".chkSelectAll_CheckedChanged()";
			try
			{
				int intRowCount = dgrdData.RowCount;
				if(chkSelectAll.Checked)
				{
					for (int i=0 ; i < intRowCount; i++) 
						dtbCPODetail.DefaultView[i].Row[MTR_CPODS.SELECT_COLUMN] = true;
				}
				else
				{
					for (int i=0 ; i < intRowCount; i++) 
						dtbCPODetail.DefaultView[i].Row[MTR_CPODS.SELECT_COLUMN] = false;
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

		private void btnSave_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnSave_Click()";
			try
			{
				if(dgrdData.RowCount == 0)
					return;

				//Validate data
				blnDataIsValid = ValidateData() && !dgrdData.EditActive;;
				if(!blnDataIsValid) return;
				DCOptionsBO boDCOptions = new DCOptionsBO();
				if (cboPlanType.Text.Trim() != lblDCP.Text.Trim())  // MRP Mode
					boCPODataViewer.DeleteMRP(strCPOIDToDelete);
				else
				{
					//Check dataset of table
					if(dtbCPODetail.DataSet != null)
					{
						DataSet dstTest = new DataSet();
						dstTest.Tables.Add(dtbCPODetail.Copy());
						boDCOptions.UpdateDataSetForDCP(dstTest, strMasterIDToUpdate, arrMasterIDToUpdate, int.Parse(txtCycle.Tag.ToString()));
					}
					else
					{
						DataSet dtsData = new DataSet();
						dtsData.Tables.Add(dtbCPODetail);
						boDCOptions.UpdateDataSetForDCP(dtsData, strMasterIDToUpdate, arrMasterIDToUpdate, int.Parse(txtCycle.Tag.ToString()));
					}
				}
				arrMasterIDToUpdate = new ArrayList();
				strMasterIDToUpdate = "0";
				strCPOIDToDelete = "0";
				btnSave.Enabled = false;
				btnSearch.Enabled = true;
				if (cboPlanType.Text.Trim() == lblDCP.Text.Trim())
					RecalculateDataAfterSaving();
				btnSearch_Click(null, null);
				//Show successful message
				PCSMessageBox.Show(ErrorCode.MESSAGE_AFTER_SAVE_DATA);
			}
			catch (PCSException ex)
			{
				blnDataIsValid = false;
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
				blnDataIsValid = false;
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

		private void btnCycleSearch_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnCycleSearch_Click()";

			try
			{
				SelectCycle(METHOD_NAME, true);
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

		private void btnMasLocSearch_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnMasLocSearch_Click()";
			
			try
			{
				SelectMasterLocation(METHOD_NAME, true);
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

		private void btnPartNumberSearch_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnPartNumberSearch_Click()";
			
			try
			{
				SelectPartNumber(METHOD_NAME, true);
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

		private void btnSearch_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnSearch_Click()";
			try
			{
				txtNumRows.Value = 0;
				chkSelectAll.Checked = false;
				dgrdData.FetchRowStyles = false;
				this.Cursor = Cursors.WaitCursor;
				//Condition HashTable
				Hashtable htbCriteria = new Hashtable();
				string strExtraCondition = string.Empty;
				
				//Call BO's method to retrieve data				
				DataSet dstResult = null;
				if(!BuildConditionHashTable(out htbCriteria, out strExtraCondition))
				{
					//HACKED-Added by Tuan TQ. 27 Dec, 2005. - Fix error no. 3195
					dtbCPODetail = BuildCPODetailTable();
					dgrdData.DataSource = dtbCPODetail;

					//Reformat grid
					FormatDataGrid();
					this.Cursor = Cursors.Default;
					//End hacked

					return;
				}

				if (cboPlanType.Text.Trim() != lblDCP.Text.Trim())
				{
					#region Search for MRP/MPS

					dstResult = boCPODataViewer.Search(htbCriteria);
					if(dstResult != null)
					{					
						if(dstResult.Tables.Count != 0)
						{
							dtbCPODetail = dstResult.Tables[0];						
							dtbCPODetail.DefaultView.RowFilter = strExtraCondition;
							dtbCPODetail.DefaultView.Sort = ITM_ProductTable.PRIMARYVENDORID_FLD + ", "
								+ ITM_CategoryTable.TABLE_NAME + ITM_CategoryTable.CODE_FLD + ","
								+ ITM_ProductTable.TABLE_NAME + ITM_ProductTable.REVISION_FLD + ","
								+ ITM_ProductTable.TABLE_NAME + ITM_ProductTable.CODE_FLD;
						}
					}
					else
						dtbCPODetail = BuildCPODetailTable();

					dgrdData.DataSource = dtbCPODetail;

					//Reformat grid
					FormatDataGrid();
					dgrdData.Splits[0].DisplayColumns["Quantity"].Locked = true;
					dgrdData.AllowAddNew = false;
					#endregion
				}
				else
				{
					#region Search for DCP

					dstResult = boCPODataViewer.SearchForDCP(htbCriteria);
					if(dstResult != null)
					{					

						if(dstResult.Tables.Count != 0)
						{
							dtbCPODetail = dstResult.Tables[0];						
							dtbCPODetail.DefaultView.RowFilter = strExtraCondition;
							dtbCPODetail.DefaultView.Sort = ITM_ProductTable.PRIMARYVENDORID_FLD + ", "
								+ ITM_CategoryTable.TABLE_NAME + ITM_CategoryTable.CODE_FLD + ","
								+ ITM_ProductTable.TABLE_NAME + ITM_ProductTable.REVISION_FLD + ","
								+ ITM_ProductTable.TABLE_NAME + ITM_ProductTable.CODE_FLD;
						}					
					}
					else
						dtbCPODetail = BuildCPODetailTable();
					
					dgrdData.DataSource = dtbCPODetail;
					//Reformat grid
					FormatDataGrid();
					dgrdData.Splits[0].DisplayColumns[MTR_CPOTable.CPOID_FLD].Visible = true;
					dgrdData.Splits[0].DisplayColumns[PO_PurchaseOrderMasterTable.TABLE_NAME + PO_PurchaseOrderMasterTable.CODE_FLD].Visible = true;
					dgrdData.AllowAddNew = true;
					#endregion
				}
				dgrdData.Splits[0].DisplayColumns[MTR_CPOTable.CONVERTED_FLD].Style.HorizontalAlignment = AlignHorzEnum.Center;
				dgrdData.Splits[0].DisplayColumns[MTR_CPOTable.CPOID_FLD].Visible = (cboPlanType.Text.Trim() != lblDCP.Text.Trim());
				dgrdData.Splits[0].DisplayColumns[MST_PartyTable.TABLE_NAME + MST_PartyTable.CODE_FLD].Visible = (cboPlanType.Text.Trim() != lblDCP.Text.Trim());
				dgrdData.FilterBar = true;
				//Enable button
				dgrdData.Splits[0].DisplayColumns[ITM_ProductTable.TABLE_NAME + ITM_ProductTable.CODE_FLD].Button = true;
				dgrdData.Splits[0].DisplayColumns[PRO_ShiftTable.SHIFTDESC_FLD].Button = true;
				dgrdData.Splits[0].DisplayColumns[ITM_ProductTable.TABLE_NAME + ITM_ProductTable.DESCRIPTION_FLD].Button = true;
				//Disable convert to WO if plan type not is DCP
				btnNewWOConvert.Enabled = (cboPlanType.Text != PlanTypeEnum.MRP.ToString().Trim() && (dgrdData.RowCount > 0));
				btnUpdateWorkOrder.Enabled = (cboPlanType.Text != PlanTypeEnum.MRP.ToString().Trim() && (dgrdData.RowCount > 0));
			    txtNumRows.Value = dgrdData.RowCount != 0 ? dgrdData.RowCount : 0;
			    btnNewPOConvert.Enabled = (dgrdData.RowCount > 0);
				btnPrint.Enabled = (dgrdData.RowCount > 0);
				btnExistingPOConvert.Enabled = btnNewPOConvert.Enabled;
				chkSelectAll.Enabled = (dgrdData.RowCount > 0);
				btnSave.Enabled = true;
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
			finally
			{
				this.Cursor = Cursors.Default;
			}
		}

		private void btnHelp_Click(object sender, System.EventArgs e)
		{
		
		}

		private void btnClose_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void GroupBox_Enter(object sender, System.EventArgs e)
		{
		
		}


		private void btnCategorySearch_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnCategorySearch_Click()";
			
			try
			{
				SelectCategory(METHOD_NAME, true);
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

		private void btnExistingPOConvert_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnExistingPOConvert_Click()";
			try
			{
				Boolean blnSelected = false;
				for (int i =0; i <dgrdData.RowCount; i++)
				{
					if (dgrdData[i, MTR_CPODS.SELECT_COLUMN].ToString() == true.ToString())
					{
						blnSelected = true;
						break;
					}
				}
				if (!blnSelected)
				{
					PCSMessageBox.Show(ErrorCode.MESSAGE_SELECT_ROW_TO_CONVERT, MessageBoxIcon.Warning);
					return;
				}
				if (dgrdData.RowCount == 0) return;
				
				//Open search form to select PO which was not closed and approved
				string strCondition = Constants.WHERE_KEYWORD + " (SELECT Count(*) FROM "  + PO_PurchaseOrderDetailTable.TABLE_NAME 
					+ " " + Constants.WHERE_KEYWORD + Constants.WHITE_SPACE + "(" + PO_PurchaseOrderDetailTable.APPROVERID_FLD + " Is Not Null "
					+ " OR " + PO_PurchaseOrderDetailTable.CLOSED_FLD + " = 1 ) " + Constants.AND + " " 
					+ PO_PurchaseOrderDetailTable.PURCHASEORDERMASTERID_FLD + "=" + PO_PurchaseOrderMasterTable.TABLE_NAME + "." + PO_PurchaseOrderMasterTable.PURCHASEORDERMASTERID_FLD + ") = 0 "
					+ " AND " + PO_PurchaseOrderMasterTable.TABLE_NAME + "." + PO_PurchaseOrderMasterTable.MASTERLOCATIONID_FLD + "=" + txtMasLoc.Tag.ToString();
				DataRowView drvResult = FormControlComponents.OpenSearchForm(PO_PurchaseOrderMasterTable.TABLE_NAME, PO_PurchaseOrderMasterTable.CODE_FLD, string.Empty, strCondition);
				if (drvResult != null)	
				{
					MTR_CPOVO voCPO = new MTR_CPOVO();
					voCPO.CCNID = int.Parse(cboCCN.SelectedValue.ToString());
					voCPO.MasterLocationID = int.Parse(txtMasLoc.Tag.ToString());
					PO_PurchaseOrderMasterVO voPOMaster = new PO_PurchaseOrderMasterVO();
					voPOMaster.PurchaseOrderMasterID = int.Parse(drvResult[PO_PurchaseOrderMasterTable.PURCHASEORDERMASTERID_FLD].ToString());
					DataView dtwAllCPOAffterSort = (dtbCPODetail.Copy()).DefaultView;
					dtwAllCPOAffterSort.RowFilter = MTR_CPODS.SELECT_COLUMN + Constants.EQUAL + 1.ToString()
						+ " AND " + ITM_ProductTable.LISTPRICE_FLD+ " > 0";
					//Create Detail & PODelivery
					DataSet dstPODelivery = new DataSet();
					dstPODelivery.Tables.Add(PO_DeliveryScheduleTable.TABLE_NAME);
					dstPODelivery.Tables[0].Columns.Add(PO_DeliveryScheduleTable.DELIVERYSCHEDULEID_FLD, typeof(int));
					dstPODelivery.Tables[0].Columns.Add(PO_DeliveryScheduleTable.DELIVERYLINE_FLD, typeof(string));
					dstPODelivery.Tables[0].Columns.Add(PO_DeliveryScheduleTable.SCHEDULEDATE_FLD, typeof(DateTime));
					dstPODelivery.Tables[0].Columns.Add(PO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD, typeof(decimal));
					dstPODelivery.Tables[0].Columns.Add(PO_DeliveryScheduleTable.RECEIVEDQUANTITY_FLD, typeof(decimal));
					dstPODelivery.Tables[0].Columns.Add(PO_DeliveryScheduleTable.PURCHASEORDERDETAILID_FLD, typeof(int));
                    dstPODelivery.Tables[0].Columns.Add(ITM_ProductTable.PRODUCTID_FLD, typeof(int));
					dtwAllCPOAffterSort.Sort = ITM_ProductTable.PRODUCTID_FLD;
					int intStep = 0;
					int i =0;
					while (i <dtwAllCPOAffterSort.Count)
					{
						DataRowView[] drowSameItems = dtwAllCPOAffterSort.FindRows(dtwAllCPOAffterSort[i][ITM_ProductTable.PRODUCTID_FLD].ToString());

						intStep = drowSameItems.Length;
						//create the new row for POLine
						int intDeliveryLine = 0;
						foreach (DataRowView drowData in drowSameItems)
						{
							//create the new PODeliveryLine
							DataRow drowPOSche = dstPODelivery.Tables[0].NewRow();
							drowPOSche[ITM_ProductTable.PRODUCTID_FLD] = drowData[ITM_ProductTable.PRODUCTID_FLD];
							drowPOSche[PO_DeliveryScheduleTable.DELIVERYLINE_FLD] = ++intDeliveryLine;
							drowPOSche[PO_DeliveryScheduleTable.SCHEDULEDATE_FLD] = drowData[MTR_CPOTable.DUEDATE_FLD];
							drowPOSche[PO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD] = drowData[MTR_CPOTable.QUANTITY_FLD];
							dstPODelivery.Tables[0].Rows.Add(drowPOSche);
						}
						i = i + intStep;
					}
					PCSProcurement.Purchase.PurchaseOrder frmPO = new PurchaseOrder(dtwAllCPOAffterSort, voCPO, voPOMaster, dstPODelivery);
					frmPO.ShowDialog();
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

		private void btnNewPOConvert_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnNewPOConvert_Click()";
			Cursor = Cursors.WaitCursor;
			try
			{
				#region Check condition before convert
				if(FormControlComponents.CheckMandatory(cboPlanType))
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID);
					cboPlanType.Focus();
					Cursor = Cursors.Default; return;
				}
				if(FormControlComponents.CheckMandatory(txtCycle))
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID);
					txtCycle.Focus();
					txtCycle.Select();
					Cursor = Cursors.Default; return;
				}
				if (cboPlanType.Text.Trim() == lblDCP.Text.Trim())
				{
					//Check if this is new row
					for (int i = 0; i < dgrdData.RowCount; i++)
					{
						if (dgrdData[i, MTR_CPODS.SELECT_COLUMN].ToString() != string.Empty) 
						{
							if ((bool)dgrdData[i, MTR_CPODS.SELECT_COLUMN] == true)
							{
								if (dgrdData[i, PRO_DCPResultDetailTable.DCPRESULTDETAILID_FLD] == DBNull.Value)
								{
									PCSMessageBox.Show(ErrorCode.MESSAGE_CPO_CAN_NOT_CONVERT, MessageBoxIcon.Exclamation);
									dgrdData.Row = i;
									dgrdData.Col = dgrdData.Columns.IndexOf(dgrdData.Columns[ITM_ProductTable.TABLE_NAME + ITM_ProductTable.CODE_FLD]);
									dgrdData.Focus();
									Cursor = Cursors.Default; return;
								}
							}
						}
					}
				}
				blnConvertPOSuccess = false;
				//create error items table
				dtbListErrorItem = new DataTable();
				dtbListErrorItem.Columns.Add(ITM_ProductTable.PRODUCTID_FLD, typeof(int));
				dtbListErrorItem.Columns.Add(ITM_ProductTable.CODE_FLD);
				dtbListErrorItem.Columns.Add(ITM_ProductTable.DESCRIPTION_FLD);
				dtbListErrorItem.Columns.Add(ITM_ProductTable.REVISION_FLD);
				dtbListErrorItem.Columns.Add(REASON_FLD);

				frmMessageBoxForm = new MessageBoxFormForItems();

				intToPOorToWO = 1;
				string strReason = string.Empty;
				DataRow[] drowSameItem = dtbCPODetail.Select(MTR_CPODS.SELECT_COLUMN + "=" + 1);

				#region Get list error item

				foreach (DataRow drow in drowSameItem)
				{
					strReason = string.Empty;
					if (drow[MTR_CPODS.SELECT_COLUMN].ToString() == true.ToString() && dtbListErrorItem.Select(ITM_ProductTable.PRODUCTID_FLD + "=" + (int) drow[ITM_ProductTable.PRODUCTID_FLD]).Length == 0)
					{
						
						if (intToPOorToWO == 1)
						{
							if (drow[ITM_ProductTable.PRIMARYVENDORID_FLD].ToString() == string.Empty)
							{
								strReason += frmMessageBoxForm.lblPrimaryVendor.Text.Trim() + ";";
							}
							else
							{
								//Create fake data for Vendor
								DataTable dtbDeliverySchedule = boCPODataViewer.GetVendorDeliveryPolicyByParty((int) drow[ITM_ProductTable.PRIMARYVENDORID_FLD]);

								DataRow[] drowDelForProduct = dtbDeliverySchedule.Select(ITM_ProductTable.PRODUCTID_FLD + "=" + (int) drow[ITM_ProductTable.PRODUCTID_FLD]);
								if (drowDelForProduct.Length == 0)
								{
									strReason += frmMessageBoxForm.lblVendorDeliverySchedule.Text.Trim() + ";";;
								}
							}
							if (drow[ITM_ProductTable.VENDORLOCATIONID_FLD].ToString() == string.Empty)
							{
								strReason += frmMessageBoxForm.lblVendorLoc.Text.Trim() + ";";
							}
							if (drow[ITM_ProductTable.VENDORCURRENCYID_FLD].ToString() == string.Empty)
							{
								strReason += frmMessageBoxForm.lblVendorCurrency.Text.Trim() + ";";
							}
							if (drow[ITM_ProductTable.LISTPRICE_FLD].ToString() == string.Empty)
							{
								strReason += frmMessageBoxForm.lblListPrice.Text.Trim() + ";";
							}
							else
							{
								if (Convert.ToDecimal(drow[ITM_ProductTable.LISTPRICE_FLD]) <= 0)
								{
									strReason += frmMessageBoxForm.lblListPrice.Text.Trim() + ";";
								}
							}
							if (drow[ITM_ProductTable.VENDORCURRENCYID_FLD].ToString() != string.Empty && drow[MST_ExchangeRateTable.RATE_FLD].ToString() == string.Empty && (int) drow[ITM_ProductTable.VENDORCURRENCYID_FLD] != SystemProperty.HomeCurrencyID)
							{
								strReason += frmMessageBoxForm.lblExchangeRate.Text.Trim() + ";";
							}
							
							if (strReason != string.Empty)
							{
								DataRow drowItem = dtbListErrorItem.NewRow();
								drowItem[ITM_ProductTable.PRODUCTID_FLD] = drow[ITM_ProductTable.PRODUCTID_FLD];
								drowItem[ITM_ProductTable.CODE_FLD] = drow[ITM_ProductTable.TABLE_NAME + ITM_ProductTable.CODE_FLD];
								drowItem[ITM_ProductTable.DESCRIPTION_FLD] = drow[ITM_ProductTable.TABLE_NAME + ITM_ProductTable.DESCRIPTION_FLD];
								drowItem[ITM_ProductTable.REVISION_FLD] = drow[ITM_ProductTable.TABLE_NAME + ITM_ProductTable.REVISION_FLD];
								drowItem[REASON_FLD] = strReason.Substring(0, strReason.Length-1);

								dtbListErrorItem.Rows.Add(drowItem);
							}
						}
					}
				}

				#endregion

				Boolean blnSelected = false;
				for (int i =0; i <dtbCPODetail.Rows.Count; i++)
				{
					if (dtbCPODetail.Rows[i][MTR_CPODS.SELECT_COLUMN].ToString() == true.ToString())
					{
						blnSelected = true;
						break;
					}
				}
				if (!blnSelected)
				{
					PCSMessageBox.Show(ErrorCode.MESSAGE_SELECT_ROW_TO_CONVERT, MessageBoxIcon.Warning);
					Cursor = Cursors.Default; return;
				}

				#endregion

				if (frmMessageBoxForm != null && dtbListErrorItem.Rows.Count > 0)
				{
					frmMessageBoxForm.BugReason = dtbListErrorItem;
					frmMessageBoxForm.ShowDialog();
				}
				else
				{
					ThreadForGenNewPO();
					if (blnConvertPOSuccess)
					{
						PCSMessageBox.Show(ErrorCode.CONVERTED_SUCCESSFULLY, MessageBoxIcon.Information);
						Cursor = Cursors.Default; return;
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
			Cursor = Cursors.Default;
		}

		private void btnNewWOConvert_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnNewWOConvert_Click()";
			Cursor = Cursors.WaitCursor;
			try
			{
				#region Check data before convert
				if(FormControlComponents.CheckMandatory(cboPlanType))
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID);
					cboPlanType.Focus();
					Cursor = Cursors.Default; return;
				}

				if(FormControlComponents.CheckMandatory(txtCycle))
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID);
					txtCycle.Focus();
					txtCycle.Select();
					Cursor = Cursors.Default; return;
				}
				if(cboPlanType.Text == PlanTypeEnum.MPS.ToString())
				{
					if(FormControlComponents.CheckMandatory(txtMasLoc))
					{
						PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID);
						txtMasLoc.Focus();
						txtMasLoc.Select();
						Cursor = Cursors.Default; return;
					}
				}
				else if(cboPlanType.Text == PlanTypeEnum.MRP.ToString())
				{
					PCSMessageBox.Show(ErrorCode.MESSAGE_SELECT_DCP_OR_MPS_BEFORE_CONVERT_WO);
					cboPlanType.Focus();
					Cursor = Cursors.Default; return;
				}
				if (cboPlanType.Text.Trim() == lblDCP.Text.Trim())
				{
					//Check if this is new row
					for (int i = 0; i < dgrdData.RowCount; i++)
					{
						if (dgrdData[i, MTR_CPODS.SELECT_COLUMN].ToString() != string.Empty) 
						{
							if ((bool)dgrdData[i, MTR_CPODS.SELECT_COLUMN] == true)
							{
								if (dgrdData[i, PRO_DCPResultDetailTable.DCPRESULTDETAILID_FLD] == DBNull.Value)
								{
									PCSMessageBox.Show(ErrorCode.MESSAGE_CPO_CAN_NOT_CONVERT, MessageBoxIcon.Exclamation);
									dgrdData.Row = i;
									dgrdData.Col = dgrdData.Columns.IndexOf(dgrdData.Columns[ITM_ProductTable.TABLE_NAME + ITM_ProductTable.CODE_FLD]);
									dgrdData.Focus();
									Cursor = Cursors.Default; return;
								}
							}
						}
					}
				}
				blnConvertWOSuccess = false;

				#region //create error items table
				dtbListErrorItem = new DataTable();
				dtbListErrorItem.Columns.Add(ITM_ProductTable.PRODUCTID_FLD, typeof(int));
				dtbListErrorItem.Columns.Add(ITM_ProductTable.CODE_FLD);
				dtbListErrorItem.Columns.Add(ITM_ProductTable.DESCRIPTION_FLD);
				dtbListErrorItem.Columns.Add(ITM_ProductTable.REVISION_FLD);
				dtbListErrorItem.Columns.Add(REASON_FLD);

				frmMessageBoxForm = new MessageBoxFormForItems();

				Boolean blnSelected = false;
				intToPOorToWO = 2;
				DataRow[] drowSameItem = dtbCPODetail.Select(MTR_CPODS.SELECT_COLUMN + "=" + 1);
				foreach (DataRow drow in drowSameItem)
				{
					if (drow[MTR_CPODS.SELECT_COLUMN].ToString() == true.ToString())
					{
						if (intToPOorToWO == 2) 
						{
							if (drow[PRO_ProductionLineTable.PRODUCTIONLINEID_FLD].ToString() == string.Empty &&
								dtbListErrorItem.Select(ITM_ProductTable.PRODUCTID_FLD + "=" + (int) drow[ITM_ProductTable.PRODUCTID_FLD]).Length == 0)
							{
								DataRow drowItem = dtbListErrorItem.NewRow();
								drowItem[ITM_ProductTable.PRODUCTID_FLD] = drow[ITM_ProductTable.PRODUCTID_FLD];
								drowItem[ITM_ProductTable.CODE_FLD] = drow[ITM_ProductTable.TABLE_NAME + ITM_ProductTable.CODE_FLD];
								drowItem[ITM_ProductTable.DESCRIPTION_FLD] = drow[ITM_ProductTable.TABLE_NAME + ITM_ProductTable.DESCRIPTION_FLD];
								drowItem[ITM_ProductTable.REVISION_FLD] = drow[ITM_ProductTable.TABLE_NAME + ITM_ProductTable.REVISION_FLD];
								drowItem[REASON_FLD] = frmMessageBoxForm.lblProductionLine.Text.Trim();

								dtbListErrorItem.Rows.Add(drowItem);
							}
						}
					}
				}

				#endregion

				for (int i =0; i <dgrdData.RowCount; i++)
				{
					if (dgrdData[i, MTR_CPODS.SELECT_COLUMN].ToString() == true.ToString())
					{
						blnSelected = true;
						break;
					}
				}
				if (!blnSelected)
				{
					PCSMessageBox.Show(ErrorCode.MESSAGE_SELECT_ROW_TO_CONVERT, MessageBoxIcon.Warning);
					Cursor = Cursors.Default; return;
				}

				#endregion

				ThreadForGenNewWO();
				if (frmMessageBoxForm != null && dtbListErrorItem.Rows.Count > 0)
				{
					frmMessageBoxForm.BugReason = dtbListErrorItem;
					frmMessageBoxForm.ShowDialog();
				}
				else
				{
					if (blnConvertWOSuccess == true)
					{
						btnSearch_Click(null, null);
						PCSMessageBox.Show(ErrorCode.CONVERTED_SUCCESSFULLY, MessageBoxIcon.Information);
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
			Cursor = Cursors.Default;
		}		

		private void btnUpdateWorkOrder_Click(object sender, System.EventArgs e)
		{
            if (dgrdData.RowCount == 0)
            {
                return;
            }

            // get list of generated WO ID
		    var dvGeneratedId = new DataView(dtbCPODetail.Copy())
		    {
		        RowFilter = string.Format("{0} IS NOT NULL", MTR_CPOTable.WOGENERATEDID_FLD),
		        Sort = MTR_CPOTable.WOGENERATEDID_FLD
		    };
		    // distinct generated id
		    var distinctGeneratedWO = dvGeneratedId.ToTable(true, MTR_CPOTable.WOGENERATEDID_FLD);
            DataView dtwAllCpoAffterSort = (dtbCPODetail.Copy()).DefaultView;
            // filter with items are generated to WO
            dtwAllCpoAffterSort.RowFilter = string.Format("{0} IS NOT NULL", MTR_CPOTable.WOGENERATEDID_FLD);
            dtwAllCpoAffterSort.Sort = string.Format("{0}, {1}, {2}", MTR_CPOTable.WOGENERATEDID_FLD, MTR_CPOTable.PRODUCTID_FLD, MTR_CPOTable.DUEDATE_FLD);
            foreach (DataRow woRow in distinctGeneratedWO.Rows)
		    {
                int workOrderId;
                int.TryParse(woRow[MTR_CPOTable.WOGENERATEDID_FLD].ToString(), out workOrderId);
                if (workOrderId <= 0)
                {
                    continue;
                }
                // get list of item belong to generated work order
		        var workOrderView = new DataView(dtbCPODetail.Copy())
		        {
		            RowFilter = string.Format("{0} IS NOT NULL AND {0} = {1}", MTR_CPOTable.WOGENERATEDID_FLD, workOrderId),
		            Sort = string.Format("{0}, {1}, {2}", MTR_CPOTable.WOGENERATEDID_FLD, MTR_CPOTable.PRODUCTID_FLD, MTR_CPOTable.DUEDATE_FLD)
		        };
                // update work order detail
		        foreach (DataRowView rowView in workOrderView)
		        {
                    var productId = Convert.ToInt32(rowView[MTR_CPOTable.PRODUCTID_FLD]);
                    var quantity = Convert.ToDecimal(rowView[MTR_CPOTable.QUANTITY_FLD]);
                    var startDate = Convert.ToDateTime(rowView[MTR_CPOTable.STARTDATE_FLD]);
                    var dueDate = Convert.ToDateTime(rowView[MTR_CPOTable.DUEDATE_FLD]);
                    startDate = startDate.Truncate(TimeSpan.FromSeconds(60));
                    dueDate = dueDate.Truncate(TimeSpan.FromSeconds(60));

                    boCPODataViewer.UpdateWorkOrderDetail(workOrderId, productId, quantity, startDate, dueDate);
                }
		    }

            PCSMessageBox.Show(ErrorCode.MESSAGE_AFTER_SAVE_DATA);
        }
		
		private void btnPartNameSearch_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnPartNameSearch_Click()";
			
			try
			{
				SelectPartName(METHOD_NAME, true);
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
		
		private void txtCategory_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtCategory_Validating()";
			try
			{				
				if(txtCategory.Text.Length == 0)
				{
					txtCategory.Tag = ZERO_STRING;
					return;
				}
				else if(!txtCategory.Modified)
				{
					return;
				}

				e.Cancel = !SelectCategory(METHOD_NAME, false);
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
        
		private void txtCategory_KeyDown(object sender, KeyEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtCategory_KeyDown()";

			try
			{
				if ((e.KeyCode == Keys.F4) && (btnCategorySearch.Enabled))
				{
					SelectCategory(METHOD_NAME, true);
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
		
		private void txtCycle_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtCycle_Validating()";

			try
			{				
				if(txtCycle.Text.Length == 0)
				{
					txtCycle.Tag =ZERO_STRING;
					return;
				}
				else if(!txtCycle.Modified)
				{
					return;
				}

				e.Cancel = !SelectCycle(METHOD_NAME, false);
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

		private void txtCycle_KeyDown(object sender, KeyEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtCycle_KeyDown()";

			try
			{
				if ((e.KeyCode == Keys.F4) && (btnCycleSearch.Enabled))
				{
					SelectCycle(METHOD_NAME, true);
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
		
		private void txtMasLoc_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtMasLoc_Validating()";

			try
			{				
				if(txtMasLoc.Text.Length == 0)
				{
					txtMasLoc.Tag =ZERO_STRING;
					return;
				}
				else if(!txtMasLoc.Modified)
				{
					return;
				}

				e.Cancel = !SelectMasterLocation(METHOD_NAME, false);
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
        
		private void txtMasLoc_KeyDown(object sender, KeyEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtMasLoc_KeyDown()";

			try
			{
				if ((e.KeyCode == Keys.F4) && (btnMasLocSearch.Enabled))
				{
					SelectMasterLocation(METHOD_NAME, true);
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
		
		private void txtPartName_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtPartName_Validating()";

			try
			{
				if(txtPartName.Text.Length == 0)
				{
					txtPartNumber.Text = string.Empty;
					txtRevision.Text = string.Empty;
					txtPartNumber.Tag = ZERO_STRING;
					return;
				}
				else if(!txtPartName.Modified)
				{
					return;
				}
				
				e.Cancel = !SelectPartName(METHOD_NAME, false);				
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

		private void txtPartName_KeyDown(object sender, KeyEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtPartName_KeyDown()";

			try
			{
				if ((e.KeyCode == Keys.F4) && (btnPartNameSearch.Enabled))
				{
					SelectPartName(METHOD_NAME, true);
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
		
		private void txtPartNumber_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtPartNumber_Validating()";

			try
			{
				if(txtPartNumber.Text.Length == 0)
				{
					txtPartName.Text = string.Empty;
					txtRevision.Text = string.Empty;
					txtPartNumber.Tag = ZERO_STRING;
					return;
				}
				else if(!txtPartNumber.Modified)
				{
					return;
				}
				
				e.Cancel = !SelectPartNumber(METHOD_NAME, false);				
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

		
		private void txtPartNumber_KeyDown(object sender, KeyEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtPartNumber_KeyDown()";

			try
			{
				if ((e.KeyCode == Keys.F4) && (btnPartNumberSearch.Enabled))
				{
					SelectPartNumber(METHOD_NAME, true);
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

		private void cboPlanType_SelectedIndexChanged(object sender, EventArgs e)
		{
			const string METHOD_NAME = THIS + ".cboPlanType_SelectedIndexChanged()";

			try
			{
				if(cboPlanType.SelectedItem == null) return;

				ClearSearchingCondition();

				btnProductionLine.Enabled = cboPlanType.SelectedItem.ToString() != PlanTypeEnum.MRP.ToString();
				txtProductionLine.Enabled = cboPlanType.SelectedItem.ToString() != PlanTypeEnum.MRP.ToString();

				btnMasLocSearch.Enabled = cboPlanType.SelectedItem.ToString() != lblDCP.Text.Trim();
				txtMasLoc.Enabled = cboPlanType.SelectedItem.ToString() != lblDCP.Text.Trim();
				lblMasLoc.Enabled = cboPlanType.SelectedItem.ToString() != lblDCP.Text.Trim();
				
				if (cboPlanType.SelectedItem.ToString() == lblDCP.Text.Trim()) //Change to DCP mode
				{
					txtMasLoc.Text = string.Empty;

					#region HACK: Trada 21-04-2006
					//add some new columns
					//Shift column
					dgrdData.Splits[0].DisplayColumns[PRO_ShiftTable.SHIFTDESC_FLD].Visible = true;
					//Safety Stock Qty Column
					dgrdData.Splits[0].DisplayColumns[PRO_DCPResultDetailTable.SAFETYSTOCKAMOUNT].Visible = true;
					//IsManual Column
					dgrdData.Splits[0].DisplayColumns[PRO_DCPResultDetailTable.ISMANUAL_FLD].Visible = true;
					//config new columns
					dgrdData.Columns[PRO_DCPResultDetailTable.SAFETYSTOCKAMOUNT].NumberFormat = Constants.DECIMAL_NUMBERFORMAT;
					dgrdData.Splits[0].DisplayColumns[PRO_DCPResultDetailTable.SAFETYSTOCKAMOUNT].HeadingStyle.HorizontalAlignment = AlignHorzEnum.Far;
					dgrdData.Splits[0].DisplayColumns[PRO_DCPResultDetailTable.ISMANUAL_FLD].HeadingStyle.HorizontalAlignment = AlignHorzEnum.Center;
					for (int i =0; i <dgrdData.Splits[0].DisplayColumns.Count; i++)
					{
						dgrdData.Splits[0].DisplayColumns[i].Locked = true;
					}
					dgrdData.Splits[0].DisplayColumns[ITM_ProductTable.TABLE_NAME + ITM_ProductTable.CODE_FLD].Locked = false;
					dgrdData.Splits[0].DisplayColumns[ITM_ProductTable.TABLE_NAME + ITM_ProductTable.DESCRIPTION_FLD].Locked = false;
					dgrdData.Splits[0].DisplayColumns["Quantity"].Locked = false;
					#endregion END: Trada 21-04-2006
				}
				else //Change to default mode
				{
					#region HACK: Trada 21-04-2006
					dgrdData.AllowAddNew = false;
					//invisible some columns
					//Shift column
					dgrdData.Splits[0].DisplayColumns[PRO_ShiftTable.SHIFTDESC_FLD].Visible = false;
					//Safety Stock Qty Column
					dgrdData.Splits[0].DisplayColumns[PRO_DCPResultDetailTable.SAFETYSTOCKAMOUNT].Visible = false;
					//IsManual Column
					dgrdData.Splits[0].DisplayColumns[PRO_DCPResultDetailTable.ISMANUAL_FLD].Visible = false;
					dgrdData.Splits[0].DisplayColumns["Quantity"].Locked = true;
					#endregion END: Trada 21-04-2006
				}
				//HACK: added by Tuan TQ. 17 Feb, 2006
				txtVendor.Enabled = (cboPlanType.SelectedItem.ToString() == PlanTypeEnum.MRP.ToString());				
				btnVendorSearch.Enabled = txtVendor.Enabled;
				//End hack
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
		
		private void CPODataViewer_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".CPODataViewer_Closing()";
			try
			{	
				// if the form has been changed then ask to store database
				if(btnSave.Enabled) 
				{
					DialogResult enumDialog = PCSMessageBox.Show(ErrorCode.MESSAGE_QUESTION_STORE_INTO_DATABASE,MessageBoxButtons.YesNoCancel,MessageBoxIcon.Question);
					if( enumDialog == DialogResult.Yes)
					{
						btnSave_Click(sender, e);	
						e.Cancel = !blnDataIsValid;
					}
					else if( enumDialog == DialogResult.Cancel)//click Cancel button
					{
						e.Cancel = true;
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
		
		private void CPODataViewer_KeyDown(object sender, KeyEventArgs e)
		{
			const string METHOD_NAME = THIS + ".CPODataViewer_KeyDown()";

			try
			{				
				if (e.KeyCode == Keys.F12)
				{
					dgrdData.Row = dgrdData.RowCount;
					dgrdData.Col = dgrdData.Columns.IndexOf(dgrdData.Columns[ITM_ProductTable.TABLE_NAME + ITM_ProductTable.CODE_FLD]);
					dgrdData.Focus();
				}
			}
			catch(PCSException ex)
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

		private void dgrdData_AfterColUpdate(object sender, ColEventArgs e)
		{
			const string METHOD_NAME = THIS + ".dgrdData_AfterColUpdate()";

			try
			{
					if(e.Column.DataColumn == null) return;

				switch (e.Column.DataColumn.DataField)
				{
					case MTR_CPOTable.QUANTITY_FLD:
						if (cboPlanType.Text.Trim() == lblDCP.Text.Trim())
						{
							// calculate total time
							decimal decQuantity = 0, decLeadTime = 0, decTotalSecond = 0;
							try
							{
								decQuantity = Convert.ToDecimal(dgrdData.Columns[e.Column.DataColumn.DataField].Value);
							}
							catch{}
							try
							{
								decLeadTime = Convert.ToDecimal(dgrdData.Columns[ITM_ProductTable.LTVARIABLETIME_FLD].Value);
							}
							catch{}
							decTotalSecond = decQuantity * decLeadTime;
							// update to grid
							dgrdData.Columns[PRO_DCPResultDetailTable.TOTALSECOND_FLD].Value = decTotalSecond;
						}
						btnSave.Enabled = true;
						break;
					case MTR_CPOTable.STARTDATE_FLD:
					case MTR_CPOTable.DUEDATE_FLD:					
						btnSave.Enabled = true;
						break;
				}

				#region HACK: Trada 21-04-2006

				DataRow drwResult = (DataRow) e.Column.DataColumn.Tag;
				//Fill Data to ComNumber
				if (e.Column.DataColumn.DataField == ITM_ProductTable.TABLE_NAME + ITM_ProductTable.CODE_FLD)
				{
					if ((e.Column.DataColumn.Tag == null) ||(dgrdData[dgrdData.Row, ITM_ProductTable.TABLE_NAME +ITM_ProductTable.CODE_FLD].ToString() == string.Empty))
					{
						dgrdData.Columns[ITM_ProductTable.TABLE_NAME +ITM_ProductTable.CODE_FLD].Value = string.Empty;
						dgrdData.Columns[ITM_ProductTable.TABLE_NAME +ITM_ProductTable.DESCRIPTION_FLD].Value = string.Empty;
						dgrdData.Columns[ITM_ProductTable.TABLE_NAME +ITM_ProductTable.REVISION_FLD].Value = string.Empty;
						dgrdData.Columns[ITM_ProductTable.PRODUCTID_FLD].Value = null;
						dgrdData.Columns[MST_UnitOfMeasureTable.TABLE_NAME + MST_UnitOfMeasureTable.CODE_FLD].Value = string.Empty;
						dgrdData.Columns[ITM_CategoryTable.TABLE_NAME + ITM_CategoryTable.CODE_FLD].Value  = string.Empty;
						dgrdData.Columns[MST_WorkCenterTable.TABLE_NAME + MST_WorkCenterTable.CODE_FLD].Value  = string.Empty;
						dgrdData.Columns[ITM_ProductTable.LTVARIABLETIME_FLD].Value = string.Empty;
						dgrdData.Columns[PRO_DCPResultDetailTable.TOTALSECOND_FLD].Value = string.Empty;
					}
					else
					{
						dgrdData.EditActive = true;
						FillItemDataToGrid(drwResult);
						
					}
				}
				//Fill Data to ComName
				if(e.Column.DataColumn.DataField == ITM_ProductTable.TABLE_NAME + ITM_ProductTable.DESCRIPTION_FLD)
				{
					if ((e.Column.DataColumn.Tag == null) ||(dgrdData[dgrdData.Row, ITM_ProductTable.TABLE_NAME + ITM_ProductTable.DESCRIPTION_FLD].ToString() == string.Empty))
					{
						dgrdData.Columns[ITM_ProductTable.TABLE_NAME +ITM_ProductTable.CODE_FLD].Value = string.Empty;
						dgrdData.Columns[ITM_ProductTable.TABLE_NAME +ITM_ProductTable.DESCRIPTION_FLD].Value = string.Empty;
						dgrdData.Columns[ITM_ProductTable.TABLE_NAME +ITM_ProductTable.REVISION_FLD].Value	 = string.Empty;
						dgrdData.Columns[ITM_ProductTable.PRODUCTID_FLD].Value = null;
						dgrdData.Columns[MST_PartyTable.TABLE_NAME + MST_PartyTable.CODE_FLD].Value = string.Empty;
						dgrdData.Columns[ITM_CategoryTable.TABLE_NAME + ITM_CategoryTable.CODE_FLD].Value  = string.Empty;
						dgrdData.Columns[MST_WorkCenterTable.TABLE_NAME + MST_WorkCenterTable.CODE_FLD].Value  = string.Empty;
						dgrdData.Columns[ITM_ProductTable.LTVARIABLETIME_FLD].Value = string.Empty;
						dgrdData.Columns[PRO_DCPResultDetailTable.TOTALSECOND_FLD].Value = string.Empty;
					}
					else
					{
						dgrdData.EditActive = true;

						FillItemDataToGrid(drwResult);
					}
				}
				//Fill data to shitf column
				if(e.Column.DataColumn.DataField == PRO_ShiftTable.SHIFTDESC_FLD)
				{
					if ((e.Column.DataColumn.Tag == null) ||(dgrdData[dgrdData.Row, PRO_ShiftTable.SHIFTDESC_FLD].ToString() == string.Empty))
					{
						dgrdData.Columns[PRO_ShiftTable.SHIFTDESC_FLD].Value = string.Empty;
						dgrdData.Columns[PRO_ShiftTable.SHIFTID_FLD].Value = null;
						
					}
					else
					{
						dgrdData.EditActive = true;
						//Fill data to grid
						dgrdData.Columns[PRO_ShiftTable.SHIFTDESC_FLD].Value = drwResult[PRO_ShiftTable.SHIFTDESC_FLD];
						dgrdData.Columns[PRO_ShiftTable.SHIFTID_FLD].Value = int.Parse(drwResult[PRO_ShiftTable.SHIFTID_FLD].ToString());		
					}
				}
				#endregion END: Trada 21-04-2006
			}
			catch(PCSException ex)
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
		
		private void dgrdData_AfterDelete(object sender, EventArgs e)
		{
			btnSave.Enabled = true;
		}
		
		private void dgrdData_BeforeRowColChange(object sender, C1.Win.C1TrueDBGrid.CancelEventArgs e)
		{
		}

		private void btnPrint_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnPrint_Click()";
			const string REPORTID = "20060126093222153";
			try
			{
				/// HACKED: Thachnn: 24/01/2006: add call to DCPReport form
				if(cboPlanType.SelectedItem != null && cboPlanType.SelectedIndex != 0)
				{
					if(cboPlanType.SelectedItem.ToString() == PlanTypeEnum.MRP.ToString() ||
						cboPlanType.SelectedItem.ToString() == PlanTypeEnum.MPS.ToString() 
						) /// show CPO Report with specified Plan type
					{
						/// OLD CODE GO HERE
						CPOReport rptCPO = new CPOReport();
						rptCPO.Show();
						if (cboCCN.SelectedValue != null)
						{
							rptCPO.cboCCN.SelectedValue = cboCCN.SelectedValue;
						}
						if (cboPlanType.SelectedItem.ToString() == PlanTypeEnum.MRP.ToString()
							|| cboPlanType.SelectedItem.ToString() == PlanTypeEnum.MPS.ToString())
						{
							rptCPO.cboPlanType.SelectedItem = cboPlanType.SelectedItem;
						}
						if (txtCycle.Text.Trim() != string.Empty)
						{
							rptCPO.txtCycle.Text = txtCycle.Text.Trim();
							rptCPO.txtCycle.Tag = txtCycle.Tag;
						}
						if (txtMasLoc.Text.Trim() != string.Empty)
						{
							rptCPO.txtMasLoc.Text = txtMasLoc.Text.Trim();
							rptCPO.txtMasLoc.Tag = txtMasLoc.Tag;
						}
						if (txtProductionLine.Text.Trim() != string.Empty)
						{
							rptCPO.txtProductionLine.Text = txtProductionLine.Text.Trim();
							rptCPO.txtProductionLine.Tag = txtProductionLine.Tag;
						}
						/// END OLD CODE
					}

					else // show DCP Report
					{
						#region SonHT DELETED

//						PCSProduction.DCP.DCPReport objDCPReport = new PCSProduction.DCP.DCPReport();
//						objDCPReport.Show();
//						if (cboCCN.SelectedValue != null)
//						{
//							objDCPReport.CboCCN.SelectedValue = cboCCN.SelectedValue;
//						} 
//						if (txtCycle.Text.Trim() != string.Empty)
//						{
//							objDCPReport.TxtCycle.Text = txtCycle.Text.Trim();
//							objDCPReport.TxtCycle.Tag = txtCycle.Tag;
//						} 
//
//						if (txtProductionLine.Text.Trim() != string.Empty)
//						{
//							objDCPReport.TxtProductionLine.Text = txtProductionLine.Text.Trim();
//							objDCPReport.TxtProductionLine.Tag = txtProductionLine.Tag;
//						} 
						#endregion
						ViewReport frmReport = new ViewReport();
						sys_ReportVO voReport = (sys_ReportVO)(new ViewReportBO()).GetReportByReportID(REPORTID);
						frmReport.VoReport = voReport;
						frmReport.Show();

					}
				}

				else
				{
					PCSMessageBox.Show(ErrorCode.MESSAGE_CPODATAVIEWER_PLEASE_SELECT_PLAN_TYPE);
					cboPlanType.Focus();
					return; 
				}
				/// ENDHACKED: Thachnn: 24/01/2006: add call to DCPReport form				
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

		private void dtmDate_DropDownOpened(object sender, System.EventArgs e)
		{
			try
			{
				dtmDate.Value = DateTime.Now;
			}
			catch
			{
			}
		}

		private void dtmDate_DropDownClosed(object sender, System.EventArgs e)
		{
			try
			{
				if (dtmDate.Text.ToString() == string.Empty)
				{
					dgrdData[dgrdData.Row, dgrdData.Col] = DBNull.Value;
				}
			}
			catch
			{
			}
		}

		private void btnVendorSearch_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnVendorSearch_Click()";

			try
			{
				SelectVendor(true);
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

		private void txtVendor_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtVendor_Validating()";

			try
			{
				//exit if empty				
				if(txtVendor.Text.Length == 0)
				{					
					return;
				}
				else if(!txtVendor.Modified)
				{
					return;
				}				

				e.Cancel = !SelectVendor(false);				
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

		private void txtVendor_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtVendor_KeyDown()";

			try
			{
				if ((e.KeyCode == Keys.F4) && (btnVendorSearch.Enabled))
				{
					SelectVendor(true);
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

		#endregion Event Processing

		#region All prrocedures for convert to PO, by TUANDM
		
		/// <summary>
		/// Convert to PO main function
		/// </summary>
		private void ThreadForGenNewPO()
		{
			const string METHOD_NAME = THIS + ".ThreadForGenNewPO()";
			try
			{
				this.Cursor = Cursors.WaitCursor;
				if (dgrdData.RowCount == 0)
					return;
				DataTable dtbTableOfVendor = dtbCPODetail.Clone();
				// Get shema of PO detail
				DataSet dstPODetailSchema = boPurchaseOrder.ListDetailByMaster(0);
				DataView dtwAllCPOAffterSort = (dtbCPODetail.Copy()).DefaultView;
				dtwAllCPOAffterSort.RowFilter = MTR_CPODS.SELECT_COLUMN + Constants.EQUAL + 1.ToString()
					+ " AND " + ITM_ProductTable.PRIMARYVENDORID_FLD + " > 0"
					+ " AND " + ITM_ProductTable.VENDORLOCATIONID_FLD+ " > 0"
					+ " AND " + ITM_ProductTable.VENDORCURRENCYID_FLD+ " > 0"
					+ " AND " + ITM_ProductTable.LISTPRICE_FLD+ " > 0";
				// sort by vendor, category, model then product
				dtwAllCPOAffterSort.Sort = ITM_ProductTable.PRIMARYVENDORID_FLD + ", "
					+ ITM_CategoryTable.TABLE_NAME + ITM_CategoryTable.CODE_FLD + ","
					+ ITM_ProductTable.TABLE_NAME + ITM_ProductTable.REVISION_FLD + ","
					+ ITM_ProductTable.TABLE_NAME + ITM_ProductTable.CODE_FLD;
				if (dtwAllCPOAffterSort.Count == 0)
				{
					this.Cursor = Cursors.Default;
					return;
				}
				btnNewPOConvert.Enabled = false;
				btnExistingPOConvert.Enabled = false;
				int intWONew = 0, intWOExist = 0;
				if (btnNewWOConvert.Enabled)
				{
					btnNewWOConvert.Enabled = false;
					intWONew = 1;
				}
				if (btnUpdateWorkOrder.Enabled)
				{
					btnUpdateWorkOrder.Enabled = false;
					intWOExist = 1;
				}
				int i =0;
			    bool blnIsMRP = cboPlanType.Text != lblDCP.Text;
			    DateTime dtmAsOfDate = boCPODataViewer.GetAsOfDate(Convert.ToInt32(txtCycle.Tag), blnIsMRP);
				DataSet dstCalendar = boCPODataViewer.GetWorkDayCalendar();
				DateTime dtmFirstValidWorkDay = GetFirstValidWorkDay(dtmAsOfDate, dstCalendar);
				while (i < dtwAllCPOAffterSort.Count)
				{
					dtbTableOfVendor.Rows.Clear();
					dtbTableOfVendor.AcceptChanges();
					DataSet dstPODetail = dstPODetailSchema.Clone();
					for (int j =i; j < dtwAllCPOAffterSort.Count; j++)
					{
						if ((int)dtwAllCPOAffterSort[i][ITM_ProductTable.PRIMARYVENDORID_FLD]
							==(int)dtwAllCPOAffterSort[j][ITM_ProductTable.PRIMARYVENDORID_FLD])
							dtbTableOfVendor.ImportRow(dtwAllCPOAffterSort[j].Row);
						else
						{
							ConvertCPOToNewPO(dtbTableOfVendor, dstPODetail, dtmFirstValidWorkDay, dstCalendar, blnIsMRP);
							i = j;
							break;
						}
						if (j == dtwAllCPOAffterSort.Count-1) 
						{
							ConvertCPOToNewPO(dtbTableOfVendor, dstPODetail, dtmFirstValidWorkDay, dstCalendar, blnIsMRP);
							i = j+1;
						}
					}
				}
				if (intWONew == 1)
					btnNewWOConvert.Enabled = true;
				if (intWOExist == 1)
					btnUpdateWorkOrder.Enabled = true;
				btnNewPOConvert.Enabled = true;
				btnExistingPOConvert.Enabled = true;
				this.Cursor = Cursors.Default;
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
				this.Cursor = Cursors.Default;
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
				this.Cursor = Cursors.Default;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="pdstCPOOfVendor">List of vendors</param>
		/// <param name="pdstPODetail"></param>
		private void ConvertCPOToNewPO(DataTable pdstCPOOfVendor, DataSet pdstPODetail, DateTime pdtmFirstValidWorkDay, DataSet pdstCalendar, bool blnIsMRP)
		{
			pdstCPOOfVendor.DefaultView.Sort = ITM_ProductTable.PRIMARYVENDORID_FLD + ", "
				+ ITM_CategoryTable.TABLE_NAME + ITM_CategoryTable.CODE_FLD + ","
				+ ITM_ProductTable.TABLE_NAME + ITM_ProductTable.REVISION_FLD + ","
				+ ITM_ProductTable.TABLE_NAME + ITM_ProductTable.CODE_FLD;

			int intPrimaryVendor = (int) pdstCPOOfVendor.Rows[0][ITM_ProductTable.PRIMARYVENDORID_FLD];
			bool blnIsLocal = IsLocal(intPrimaryVendor);
		
			//Create data for Vendor
			DataTable dtbDeliverySchedule = boCPODataViewer.GetVendorDeliveryPolicyByParty(intPrimaryVendor);

			#region //Create Master
			PO_PurchaseOrderMasterVO voMaster = new PO_PurchaseOrderMasterVO();
			voMaster.OrderDate = DateTime.Parse(boUtils.GetDBDate().ToShortDateString());
			voMaster.CCNID = (int) cboCCN.SelectedValue;
			voMaster.MasterLocationID = Convert.ToInt32(txtMasLoc.Tag);
			// Get prefix menu for TransNo field
            var list = (from obj in SystemProperty.TableMenuEntry
                        where obj.FormLoad == PurchaseOrder.THIS
                        orderby obj.Menu_EntryID ascending
                        select obj).ToList();
			UtilsBO boUtil = new UtilsBO();
			string strFormat = "-yy-mm-dd-##";
			string strMakerCode =
				(boUtil.GetRows(MST_PartyTable.TABLE_NAME,
				"where PartyID=" + pdstCPOOfVendor.DefaultView[0][ITM_ProductTable.PRIMARYVENDORID_FLD])).Rows[0][MST_PartyTable.CODE_FLD].ToString().Trim();
			if (strMakerCode != string.Empty)
			{
				string strPONumber = boUtil.GetNoByMask(PO_PurchaseOrderMasterTable.TABLE_NAME, PO_PurchaseOrderMasterTable.CODE_FLD, strMakerCode, strFormat);
				voMaster.Code = strPONumber;
			}
			voMaster.ShipToLocID = int.Parse(txtMasLoc.Tag.ToString());
			voMaster.InvToLocID  = voMaster.ShipToLocID;
			voMaster.CurrencyID = (int) pdstCPOOfVendor.DefaultView[0][ITM_ProductTable.VENDORCURRENCYID_FLD];
			// Update maker
			voMaster.MakerLocationID = voMaster.VendorLocID= (int) pdstCPOOfVendor.DefaultView[0][ITM_ProductTable.VENDORLOCATIONID_FLD];
			voMaster.MakerID = voMaster.PartyID = (int) pdstCPOOfVendor.DefaultView[0][ITM_ProductTable.PRIMARYVENDORID_FLD];
			voMaster.ExchangeRate = FillExchangeRate(voMaster.CurrencyID, voMaster.OrderDate);

			#endregion

			#region // Create dataset structure
			DataSet dstPODelivery = new DataSet();
			dstPODelivery.Tables.Add(PO_DeliveryScheduleTable.TABLE_NAME);
			dstPODelivery.Tables[0].Columns.Add(PO_DeliveryScheduleTable.DELIVERYSCHEDULEID_FLD, typeof(int));
			dstPODelivery.Tables[0].Columns.Add(PO_DeliveryScheduleTable.DELIVERYLINE_FLD, typeof(string));
			dstPODelivery.Tables[0].Columns.Add(PO_DeliveryScheduleTable.SCHEDULEDATE_FLD, typeof(DateTime));
			dstPODelivery.Tables[0].Columns.Add(PO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD, typeof(decimal));
			dstPODelivery.Tables[0].Columns.Add(PO_DeliveryScheduleTable.RECEIVEDQUANTITY_FLD, typeof(decimal));
			dstPODelivery.Tables[0].Columns.Add(ITM_ProductTable.PRODUCTID_FLD, typeof(int));
			dstPODelivery.Tables[0].Columns.Add(PO_DeliveryScheduleTable.PURCHASEORDERDETAILID_FLD, typeof(int));
			dstPODelivery.Tables[0].Columns.Add(PO_DeliveryScheduleTable.ADJUSTMENT_FLD, typeof(decimal));
			dstPODelivery.Tables[0].Columns.Add("OriginalDeliveryQuantity", typeof(decimal));
			dstPODelivery.Tables[0].Columns.Add("StartOfSCHEDULEDATE", typeof(DateTime));

			dstPODelivery.Tables[0].Columns.Add("FixLT", typeof(decimal));
			dstPODelivery.Tables[0].Columns.Add("CPOID", typeof(int));

			DataSet dstDelTemp = dstPODelivery.Clone();
			dstDelTemp.Tables[0].Columns.Add(MTR_CPOTable.CONVERTED_FLD, typeof(bool));

			#endregion

			int intLine = 0, intStep = 0;
			int i =0;
			pdstPODetail.Tables[0].Columns.Add(PO_DeliveryScheduleTable.SCHEDULEDATE_FLD, typeof(DateTime));
			// scan all vendor
			while (i < pdstCPOOfVendor.DefaultView.Count)
			{
				// Get same items
				DataRowView[] drowSameItems = pdstCPOOfVendor.DefaultView.FindRows(new object[]{
						pdstCPOOfVendor.DefaultView[i][ITM_ProductTable.PRIMARYVENDORID_FLD].ToString(),
						pdstCPOOfVendor.DefaultView[i][ITM_CategoryTable.TABLE_NAME + ITM_CategoryTable.CODE_FLD].ToString(),
						pdstCPOOfVendor.DefaultView[i][ITM_ProductTable.TABLE_NAME + ITM_ProductTable.REVISION_FLD].ToString(),
						pdstCPOOfVendor.DefaultView[i][ITM_ProductTable.TABLE_NAME + ITM_ProductTable.CODE_FLD].ToString()
						});
				// get all delivery by Item
			    var productId = (int) drowSameItems[0][ITM_ProductTable.PRODUCTID_FLD];
                DataRow[] drowDelForProduct = dtbDeliverySchedule.Select(ITM_ProductTable.PRODUCTID_FLD + "=" + productId);
				intStep = drowSameItems.Length;
				// if existed delivery schedule
				if (drowDelForProduct.Length > 0)
				{
					#region //create the new row for POLine
					DataRow drowPODetail = pdstPODetail.Tables[0].NewRow();
					drowPODetail[PRO_WorkOrderDetailTable.LINE_FLD] = ++intLine;
                    drowPODetail[ITM_ProductTable.PRODUCTID_FLD] = productId;
					drowPODetail[ITM_ProductTable.BUYINGUMID_FLD] = drowSameItems[0][ITM_ProductTable.BUYINGUMID_FLD];
					drowPODetail[ITM_ProductTable.STOCKUMID_FLD]  = drowSameItems[0][ITM_ProductTable.STOCKUMID_FLD];
					drowPODetail[PO_PurchaseOrderDetailTable.UNITPRICE_FLD] = drowSameItems[0][ITM_ProductTable.LISTPRICE_FLD];
					drowPODetail[PO_PurchaseOrderDetailTable.VAT_FLD]		= drowSameItems[0][ITM_ProductTable.VAT_FLD];
					drowPODetail[PO_PurchaseOrderDetailTable.IMPORTTAX_FLD] = drowSameItems[0][ITM_ProductTable.IMPORTTAX_FLD];
					drowPODetail[PO_PurchaseOrderDetailTable.SPECIALTAX_FLD] = drowSameItems[0][ITM_ProductTable.SPECIALTAX_FLD];

					#endregion

					int intDeliveryLine = 0;

					#region // scan all CPO that is similar ProductID to sum quantity
					foreach (DataRowView drowData in drowSameItems)
					{
						if (drowData[MTR_CPODS.SELECT_COLUMN].ToString() == true.ToString())
						{
							if (drowPODetail[PO_PurchaseOrderDetailTable.ORDERQUANTITY_FLD].ToString() == string.Empty)
								drowPODetail[PO_PurchaseOrderDetailTable.ORDERQUANTITY_FLD] = 0;

							//create the new PODeliveryLine
							DataRow drowPOSche = dstDelTemp.Tables[0].NewRow();
							drowPOSche[ITM_ProductTable.PRODUCTID_FLD] = drowData[ITM_ProductTable.PRODUCTID_FLD];
							drowPOSche[PO_DeliveryScheduleTable.DELIVERYLINE_FLD] = ++intDeliveryLine;
							drowPOSche[PO_DeliveryScheduleTable.SCHEDULEDATE_FLD] = drowData[MTR_CPOTable.DUEDATE_FLD];

							drowPOSche["OriginalDeliveryQuantity"] = Convert.ToDecimal(drowData[MTR_CPOTable.QUANTITY_FLD]);
							drowPOSche["FixLT"] = drowData["FixLT"];
							drowPOSche["CPOID"] = drowData["CPOID"];

                            drowPOSche[PO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD] = drowData[MTR_CPOTable.QUANTITY_FLD] =
                                GetQuantityForCPO(drowData, Convert.ToDecimal(drowData[MTR_CPOTable.QUANTITY_FLD]));                            

							dstDelTemp.Tables[0].Rows.Add(drowPOSche);

							drowPODetail[PO_PurchaseOrderDetailTable.ORDERQUANTITY_FLD] = Convert.ToDecimal(drowPODetail[PO_PurchaseOrderDetailTable.ORDERQUANTITY_FLD]) 
								+ Convert.ToDecimal(drowData[MTR_CPOTable.QUANTITY_FLD]);
						}
					}
					pdstPODetail.Tables[0].Rows.Add(drowPODetail);

					#endregion

					DataTable dtbWeekMonthDelivery = new DataTable();
					if (drowDelForProduct[0][PO_VendorDeliveryScheduleTable.DELIVERYTYPE_FLD].ToString() == ((int) PODeliveryTypeEnum.Daily).ToString())
					{
						#region delivery group by Daily

						DataRow[] drvRealDel = dtbDeliverySchedule.Select(ITM_ProductTable.PRODUCTID_FLD + "=" + (int) drowSameItems[0][ITM_ProductTable.PRODUCTID_FLD],
						                                                  PO_VendorDeliveryScheduleTable.DELHOUR_FLD + " ASC," + PO_VendorDeliveryScheduleTable.DELMIN_FLD + " ASC");
						dtbWeekMonthDelivery = GroupPODeliverys(dstDelTemp.Tables[0], drvRealDel, PODeliveryTypeEnum.Daily, pdtmFirstValidWorkDay, pdstCalendar, blnIsLocal).Copy();

						#endregion
					} 
					else if (drowDelForProduct[0][PO_VendorDeliveryScheduleTable.DELIVERYTYPE_FLD].ToString() == ((int) PODeliveryTypeEnum.Weekly).ToString())
					{
						#region delivery group by Weekly

						DataRow[] drvRealDel = dtbDeliverySchedule.Select(ITM_ProductTable.PRODUCTID_FLD + "=" + (int) drowSameItems[0][ITM_ProductTable.PRODUCTID_FLD],
						                                                  "WeeklyDay ASC," + PO_VendorDeliveryScheduleTable.DELHOUR_FLD + " ASC," + PO_VendorDeliveryScheduleTable.DELMIN_FLD + " ASC");
						dtbWeekMonthDelivery = GroupPODeliverys(dstDelTemp.Tables[0], drvRealDel, PODeliveryTypeEnum.Weekly, pdtmFirstValidWorkDay, pdstCalendar, blnIsLocal).Copy();

						#endregion
					}
					else if (drowDelForProduct[0][PO_VendorDeliveryScheduleTable.DELIVERYTYPE_FLD].ToString() == ((int) PODeliveryTypeEnum.Monthly).ToString())
					{
						#region delivery group by Monthly

						DataRow[] drvRealDel = dtbDeliverySchedule.Select(ITM_ProductTable.PRODUCTID_FLD + "=" + (int) drowSameItems[0][ITM_ProductTable.PRODUCTID_FLD],
						                                                  PO_VendorDeliveryScheduleTable.MONTHLYDATE_FLD + " ASC," + PO_VendorDeliveryScheduleTable.DELHOUR_FLD + " ASC ," + PO_VendorDeliveryScheduleTable.DELMIN_FLD +" ASC");
						dtbWeekMonthDelivery = GroupPODeliverys(dstDelTemp.Tables[0], drvRealDel, PODeliveryTypeEnum.Monthly, pdtmFirstValidWorkDay, pdstCalendar, blnIsLocal).Copy();

						#endregion
					}

					foreach (DataRow drowDel in dtbWeekMonthDelivery.Rows)
					{
					    dstPODelivery.Tables[0].ImportRow(drowDel);
					}
					dstDelTemp.Tables[0].Rows.Clear();
					dstDelTemp.Tables[0].AcceptChanges();
				}
				i = i + intStep;
			}
			
            #region update new convert

			UpdateNewConvertToPO(voMaster, pdstPODetail, dstPODelivery, pdstCPOOfVendor);

			#endregion
			
			// Tinh toan lai cac truong trong PO field
			ReCalculate(pdstPODetail, voMaster);

			while(dstPODelivery.Tables[0].Rows.Count > 0)
			{
                PO_PurchaseOrderMasterVO voNewMaster = new PO_PurchaseOrderMasterVO();
				voNewMaster.BuyerID = voMaster.BuyerID;
				voNewMaster.CCNID = voMaster.CCNID;
				voNewMaster.CurrencyID = voMaster.CurrencyID;
				voNewMaster.DeliveryTermsID = voMaster.DeliveryTermsID;
				voNewMaster.DiscountTermID = voMaster.DiscountTermID;
				voNewMaster.ExchangeRate = voMaster.ExchangeRate;
				voNewMaster.InvToLocID = voMaster.InvToLocID;
				voNewMaster.MakerID = voMaster.MakerID;
				voNewMaster.MakerLocationID = voMaster.MakerLocationID;
				voNewMaster.MasterLocationID = voMaster.MasterLocationID;
				voNewMaster.PartyContactID = voMaster.PartyContactID;
				voNewMaster.PartyID = voMaster.PartyID;
				voNewMaster.ShipToLocID = voMaster.ShipToLocID;
				voNewMaster.VendorLocID = voMaster.VendorLocID;
				voNewMaster.VendorSO = voMaster.VendorSO;
                voNewMaster.OrderDate = voMaster.OrderDate;

				if (blnIsMRP)
				{
					// new biz: insert po type based on vendor type
					DataRow[] drowVendor = dtbVendor.Select("PartyID = " + voMaster.PartyID);
					switch (Convert.ToInt32(drowVendor[0][MST_PartyTable.TYPE_FLD]))
					{
						case (int)PartyTypeEnum.OUTSIDE:
							voNewMaster.PurchaseTypeID = (int)POType.Outside;
							break;
						default:
							if (blnIsLocal)
								voNewMaster.PurchaseTypeID = (int)POType.Domestic;
							else
								voNewMaster.PurchaseTypeID = (int)POType.Import;
							break;
					}
				}
				else // convert from DCP Result, po type always is Outside
				{
				    voNewMaster.PurchaseTypeID = (int)POType.Outside;
				}
				
				DataRow[] rowDeliverys = dstPODelivery.Tables[0].Select("","StartOfSCHEDULEDATE DESC");
				if (rowDeliverys.Length == 0)
				{
				    break;
				}
				DataSet dstNewPODelivery = dstPODelivery.Clone();
				for(int j = rowDeliverys.Length - 1; j >= 0 ; j--)
				{
					DataRow r = dstNewPODelivery.Tables[0].NewRow();
					r.ItemArray = rowDeliverys[j].ItemArray;
					rowDeliverys[j].Delete();
					dstNewPODelivery.Tables[0].Rows.Add(r);
				}

				if(dstNewPODelivery.Tables[0].Rows.Count == 0)
				{
				    break;
				}


				ArrayList arlCPOIDs = new ArrayList();
				if (cboPlanType.Text.Trim() != lblDCP.Text.Trim())
				{
					#region Get CPOID

					foreach (DataRow drvCPO in dstNewPODelivery.Tables[0].Rows)
						arlCPOIDs.Add(drvCPO[MTR_CPOTable.CPOID_FLD].ToString());

					#endregion
				}
				else
				{
					#region Get DCPRESULTDETAILID

					arlCPOIDs.Add(-1);
					foreach (DataRow drvCPO in pdstCPOOfVendor.Rows)
						arlCPOIDs.Add(drvCPO[PRO_DCPResultDetailTable.DCPRESULTDETAILID_FLD].ToString());

					#endregion
				}

				DataSet dstNewPODetail = pdstPODetail.Copy();

				if (dstNewPODetail.Tables[0].Rows.Count == 0)
				{
				    break;
				}

				for(int j = dstNewPODetail.Tables[0].Rows.Count - 1; j >= 0; j--)
				{
					DataRow rowDetail = dstNewPODetail.Tables[0].Rows[j];
					DataRow[] rowDels = dstNewPODelivery.Tables[0].Select("ProductID=" + rowDetail["ProductID"]);
					rowDetail["OrderQuantity"] = Convert.ToDecimal(dstNewPODelivery.Tables[0].Compute("SUM(DeliveryQuantity)","ProductID=" + rowDetail["ProductID"]));
					if(rowDels.Length == 0)
						rowDetail.Delete();
				}

				if(dstNewPODetail.Tables[0].Rows.Count == 0)
				{
				    break;
				}
				if(dstNewPODelivery.Tables[0].Rows.Count == 0)
				{
				    break;
				}

				ReCalculate(dstNewPODetail,voNewMaster);
				dstNewPODelivery.Tables[0].Columns["StartOfSCHEDULEDATE"].ColumnName = "StartDate";
			    DateTime deliveryDate = Convert.ToDateTime(dstNewPODelivery.Tables[0].Rows[0][PO_DeliveryScheduleTable.SCHEDULEDATE_FLD]);

                strMakerCode = "P-" + (boUtil.GetRows(MST_PartyTable.TABLE_NAME, "WHERE PartyID=" + voMaster.PartyID)).Rows[0][MST_PartyTable.CODE_FLD].ToString().Trim();
                if (strMakerCode != string.Empty)
                {
                    string format = "-YYMM-##";
                    if (voNewMaster.PurchaseTypeID == (int)POType.Outside)
                    {
                        format = "-YYMMA-##";
                    }
                    int revision;
                    string poNumber = boUtil.GetNoByMask(PO_PurchaseOrderMasterTable.TABLE_NAME, PO_PurchaseOrderMasterTable.CODE_FLD, strMakerCode, format, deliveryDate, out revision);
                    voNewMaster.Code = poNumber;
                    voNewMaster.ReferenceNo = poNumber;
                    voNewMaster.PORevision = revision;
                }

			    voNewMaster.UserName = SystemProperty.UserName;
			    voNewMaster.LastChange = voMaster.OrderDate;
				voNewMaster.PurchaseOrderMasterID = boPurchaseOrder.AddPOAndDelScheduleImmediate(voNewMaster, dstNewPODetail, dstNewPODelivery, arlCPOIDs);
				// alway break
				break;
			}
			blnConvertPOSuccess = true;
		}

		internal void UpdateNewConvertToPO(PO_PurchaseOrderMasterVO pvoMaster, DataSet pdstPODetail, DataSet pdstPODelivery, DataTable pdtbCPOOfVendor)
		{
			pdtbCPOOfVendor.DefaultView.Sort = ITM_ProductTable.PRODUCTID_FLD;
			foreach(DataRow rowPOLine in pdstPODetail.Tables[0].Rows)
			{
				DataRowView[] drowSameItems = pdtbCPOOfVendor.DefaultView.FindRows(rowPOLine[ITM_ProductTable.PRODUCTID_FLD].ToString());
				DataRow[] rowDeliverys = pdstPODelivery.Tables[0].Select("ProductID = " + rowPOLine["ProductID"]);
				if(rowDeliverys.Length > 0) // re-calculate DeliveryQuanity
				{
					decimal decQuantity = GetQuantityForCPO(drowSameItems[0],
						Convert.ToDecimal(rowDeliverys[0]["OriginalDeliveryQuantity"]));
					rowDeliverys[0]["DeliveryQuantity"] = decQuantity;
					decimal decPrevDeliveryQuantity = Convert.ToDecimal(rowDeliverys[0]["DeliveryQuantity"]);
					decimal decPrevOriginalDeliveryQuantity = Convert.ToDecimal(rowDeliverys[0]["OriginalDeliveryQuantity"]);
					decimal decPrevRedundalQty = decPrevDeliveryQuantity - decPrevOriginalDeliveryQuantity;
					for(int i = 1; i < rowDeliverys.Length; i++)
					{						
						DataRow rowDelivery = rowDeliverys[i];
						decimal decDeliveryQty = Convert.ToDecimal(rowDelivery["OriginalDeliveryQuantity"])	- decPrevRedundalQty;
						decimal decNewQuantity = GetQuantityForCPO(drowSameItems[0],decDeliveryQty);
						rowDelivery["DeliveryQuantity"] = decNewQuantity;
						decPrevRedundalQty = Convert.ToDecimal(rowDelivery["DeliveryQuantity"]) - decDeliveryQty;
					}
				}
			}

			for(int i = pdstPODelivery.Tables[0].Rows.Count - 1; i >= 0; i--)
			{
				DataRow rowDelivery = pdstPODelivery.Tables[0].Rows[i];
				if(Convert.ToDecimal(rowDelivery["DeliveryQuantity"]) == 0)
					rowDelivery.Delete();
			}
			pdstPODelivery.Tables[0].AcceptChanges();
		}

		/// <summary>
	    /// Get Start time base on delivery and working day
	    /// </summary>
	    /// <param name="pdtmDate"></param>
	    /// <param name="pType"></param>
	    /// <param name="pdrowDeliverys"></param>
	    /// <param name="pdstCalendar"></param>
	    private DateTime[] GetStartEndDate(DateTime pdtmDate, PODeliveryTypeEnum pType, DataRow[] pdrowDeliverys, DataSet pdstCalendar)
        {
            DateTime dtmStartOfSpace = pdtmDate;

            DateTime dtmEndOfSpace = new DateTime();

            #region determine oStartDate, oEndDate

            if (pType == PODeliveryTypeEnum.Weekly)
            {
                #region By weekly
                int intDayOfWeek = Convert.ToInt32(pdrowDeliverys[pdrowDeliverys.Length - 1][PO_VendorDeliveryScheduleTable.WEEKLYDAY_FLD]);
                int intHour = Convert.ToInt32(pdrowDeliverys[pdrowDeliverys.Length - 1][PO_VendorDeliveryScheduleTable.DELHOUR_FLD]);
                int intMin = Convert.ToInt32(pdrowDeliverys[pdrowDeliverys.Length - 1][PO_VendorDeliveryScheduleTable.DELMIN_FLD]);
                int intDayOfWeekDate = (int)pdtmDate.DayOfWeek;

                if (pdrowDeliverys.Length == 1)
                {
                    #region // if a week has one time delivery

                    if (intDayOfWeekDate > intDayOfWeek)
                    {
                        dtmStartOfSpace = pdtmDate.AddDays(-(double)((int)pdtmDate.DayOfWeek - intDayOfWeek));
                        dtmStartOfSpace = new DateTime(dtmStartOfSpace.Year, dtmStartOfSpace.Month, dtmStartOfSpace.Day, intHour, intMin, 0);
                    }
                    else if (intDayOfWeekDate == intDayOfWeek)
                    {
                        dtmStartOfSpace = pdtmDate;
                        dtmStartOfSpace = new DateTime(dtmStartOfSpace.Year, dtmStartOfSpace.Month, dtmStartOfSpace.Day, intHour, intMin, 0);
                    }
                    else if (intDayOfWeekDate < intDayOfWeek)
                    {
                        dtmStartOfSpace = pdtmDate.AddDays(-(double)((int)pdtmDate.DayOfWeek - intDayOfWeek));
                        dtmStartOfSpace = new DateTime(dtmStartOfSpace.Year, dtmStartOfSpace.Month, dtmStartOfSpace.Day, intHour, intMin, 0);
                        // alway -7
                        dtmStartOfSpace = dtmStartOfSpace.AddDays(-7);
                    }
                    // correct start 
                    if (dtmStartOfSpace > pdtmDate)
                    {
                        dtmStartOfSpace = dtmStartOfSpace.AddDays(-7);
                    }
                    // Get EndDate
                    dtmEndOfSpace = dtmStartOfSpace.AddDays(7);
                    dtmStartOfSpace = GetNearestWorkingDay(dtmStartOfSpace, pdstCalendar);

                    #endregion
                }
                else
                {
                    #region // if 1 week has more than one time delivery
                    DateTime[] dtmDeliTimes = new DateTime[pdrowDeliverys.Length];
                    bool blnOK = false;
                    // fill data for each element of delivery date time
                    for (int i = 0; i < pdrowDeliverys.Length; i++)
                    {
                        intDayOfWeek = Convert.ToInt32(pdrowDeliverys[i][PO_VendorDeliveryScheduleTable.WEEKLYDAY_FLD]);
                        intHour = Convert.ToInt32(pdrowDeliverys[i][PO_VendorDeliveryScheduleTable.DELHOUR_FLD]);
                        intMin = Convert.ToInt32(pdrowDeliverys[i][PO_VendorDeliveryScheduleTable.DELMIN_FLD]);
                        dtmDeliTimes[i] = new DateTime(pdtmDate.Year, pdtmDate.Month, pdtmDate.Day, intHour, intMin, 0).AddDays((double)intDayOfWeek - (double)pdtmDate.DayOfWeek);
                    }
                    // check the rule
                    for (int i = pdrowDeliverys.Length - 1; i >= 0; i--)
                    {
                        intDayOfWeek = Convert.ToInt32(pdrowDeliverys[i][PO_VendorDeliveryScheduleTable.WEEKLYDAY_FLD]);
                        intHour = Convert.ToInt32(pdrowDeliverys[i][PO_VendorDeliveryScheduleTable.DELHOUR_FLD]);
                        intMin = Convert.ToInt32(pdrowDeliverys[i][PO_VendorDeliveryScheduleTable.DELMIN_FLD]);
                        dtmDeliTimes[i] = new DateTime(pdtmDate.Year, pdtmDate.Month, pdtmDate.Day, intHour, intMin, 0).AddDays((double)intDayOfWeek - (double)pdtmDate.DayOfWeek);
                        if (dtmDeliTimes[i] <= pdtmDate)
                        {
                            dtmStartOfSpace = dtmDeliTimes[i];
                            // Get EndDate
                            if (i + 1 < pdrowDeliverys.Length)
                            {
                                dtmEndOfSpace = dtmDeliTimes[i + 1];
                            }
                            else // move to next week and get first day
                            {
                                dtmEndOfSpace = (dtmDeliTimes[0]).AddDays(7);
                            }
                            blnOK = true;
                            break;
                        }
                    }
                    // If it's not belong to this week then change to prev week
                    if (!blnOK)
                    {
                        dtmStartOfSpace = dtmDeliTimes[pdrowDeliverys.Length - 1].AddDays(-7);
                        // Get EndDate
                        dtmEndOfSpace = dtmDeliTimes[0];
                    }
                    dtmStartOfSpace = GetNearestWorkingDay(dtmStartOfSpace, pdstCalendar);
                    #endregion
                }
                #endregion
            }
            else if (pType == PODeliveryTypeEnum.Monthly)
            {
                #region By monthly
                int intMonthDate = Convert.ToInt32(pdrowDeliverys[pdrowDeliverys.Length - 1][PO_VendorDeliveryScheduleTable.MONTHLYDATE_FLD]);
                int intHour = Convert.ToInt32(pdrowDeliverys[pdrowDeliverys.Length - 1][PO_VendorDeliveryScheduleTable.DELHOUR_FLD]);
                int intMin = Convert.ToInt32(pdrowDeliverys[pdrowDeliverys.Length - 1][PO_VendorDeliveryScheduleTable.DELMIN_FLD]);
                // if a month has one time delivery
                if (pdrowDeliverys.Length == 1)
                {
                    #region // one delivery every month
                    if (intMonthDate > GetMaxDayOfMonth(pdtmDate))
                    {
                        intMonthDate = GetMaxDayOfMonth(pdtmDate);
                    }
                    DateTime dtmDateOfMonth = new DateTime(pdtmDate.Year, pdtmDate.Month, intMonthDate, intHour, intMin, 0);
                    // back for 1 month
                    if (dtmDateOfMonth > pdtmDate)
                    {
                        dtmDateOfMonth = dtmDateOfMonth.AddMonths(-1);
                        // Get max day of month
                        intMonthDate = GetMaxDayOfMonth(dtmDateOfMonth);
                        if (intMonthDate > Convert.ToInt32(pdrowDeliverys[pdrowDeliverys.Length - 1][PO_VendorDeliveryScheduleTable.MONTHLYDATE_FLD]))
                            intMonthDate = Convert.ToInt32(pdrowDeliverys[pdrowDeliverys.Length - 1][PO_VendorDeliveryScheduleTable.MONTHLYDATE_FLD]);
                        dtmDateOfMonth = dtmDateOfMonth.AddDays(intMonthDate - dtmDateOfMonth.Day);
                    }
                    dtmEndOfSpace = dtmDateOfMonth.AddMonths(1);
                    dtmStartOfSpace = GetNearestWorkingDay(dtmDateOfMonth, pdstCalendar);
                    #endregion
                }
                else
                {
                    #region // if a month has more than one delivery time
                    DateTime[] dtmDeliTimes = new DateTime[pdrowDeliverys.Length];
                    bool blnOK = false;
                    // fill data for each element of delivery date time
                    for (int i = 0; i < pdrowDeliverys.Length; i++)
                    {
                        intMonthDate = Convert.ToInt32(pdrowDeliverys[i][PO_VendorDeliveryScheduleTable.MONTHLYDATE_FLD]);
                        intHour = Convert.ToInt32(pdrowDeliverys[i][PO_VendorDeliveryScheduleTable.DELHOUR_FLD]);
                        intMin = Convert.ToInt32(pdrowDeliverys[i][PO_VendorDeliveryScheduleTable.DELMIN_FLD]);
                        if (intMonthDate > DateTime.DaysInMonth(pdtmDate.Year, pdtmDate.Month))
                            intMonthDate = DateTime.DaysInMonth(pdtmDate.Year, pdtmDate.Month);
                        DateTime dtmDate = new DateTime(pdtmDate.Year, pdtmDate.Month, pdtmDate.Day, intHour, intMin, 0).AddDays((double)intMonthDate - (double)pdtmDate.Day);
                        dtmDeliTimes[i] = dtmDate;
                    }
                    // check the rule
                    for (int i = pdrowDeliverys.Length - 1; i >= 0; i--)
                    {
                        intMonthDate = Convert.ToInt32(pdrowDeliverys[i][PO_VendorDeliveryScheduleTable.MONTHLYDATE_FLD]);
                        intHour = Convert.ToInt32(pdrowDeliverys[i][PO_VendorDeliveryScheduleTable.DELHOUR_FLD]);
                        intMin = Convert.ToInt32(pdrowDeliverys[i][PO_VendorDeliveryScheduleTable.DELMIN_FLD]);
                        // Day of current month out of range
                        if (intMonthDate > GetMaxDayOfMonth(pdtmDate))
                        {
                            intMonthDate = GetMaxDayOfMonth(pdtmDate);
                        }
                        DateTime dtmDate = new DateTime(pdtmDate.Year, pdtmDate.Month, pdtmDate.Day, intHour, intMin, 0).AddDays((double)intMonthDate - (double)pdtmDate.Day);

                        if (dtmDate <= pdtmDate)
                        {
                            dtmStartOfSpace = dtmDate;
                            // Get EndDate
                            if (i + 1 < pdrowDeliverys.Length)
                            {
                                dtmEndOfSpace = dtmDeliTimes[i + 1]; // get next day
                            }
                            else // move to next month and get first day
                            {
                                dtmEndOfSpace = (dtmDeliTimes[0]).AddMonths(1);
                            }

                            blnOK = true;
                            break;
                        }
                    }
                    // If it's not belong to this week then change to prev month
                    if (!blnOK)
                    {
                        dtmStartOfSpace = dtmDeliTimes[pdrowDeliverys.Length - 1].AddMonths(-1);
                        // Get EndDate
                        dtmEndOfSpace = dtmDeliTimes[0]; // get first day of this month
                    }

                    dtmStartOfSpace = GetNearestWorkingDay(dtmStartOfSpace, pdstCalendar);
                    #endregion
                }
                #endregion
            }
            else if (pType == PODeliveryTypeEnum.Daily)
            {
                #region By daily
                int intHour = Convert.ToInt32(pdrowDeliverys[pdrowDeliverys.Length - 1][PO_VendorDeliveryScheduleTable.DELHOUR_FLD]);
                int intMin = Convert.ToInt32(pdrowDeliverys[pdrowDeliverys.Length - 1][PO_VendorDeliveryScheduleTable.DELMIN_FLD]);
                if (pdrowDeliverys.Length == 1)
                {
                    #region // one delivery every day
                    DateTime dtmDate = new DateTime(pdtmDate.Year, pdtmDate.Month, pdtmDate.Day, intHour, intMin, 0);
                    // Back one day
                    if (dtmDate > pdtmDate)
                    {
                        dtmDate = dtmDate.AddDays(-1);
                    }
                    dtmEndOfSpace = dtmDate.AddDays(1);
                    dtmStartOfSpace = GetNearestWorkingDay(dtmDate, pdstCalendar);
                    #endregion
                }
                else
                {
                    #region // if a day has more than one time delivery
                    DateTime[] dtmDeliTimes = new DateTime[pdrowDeliverys.Length];
                    bool blnOK = false;
                    // fill data for each element of delivery date time
                    for (int i = 0; i < pdrowDeliverys.Length; i++)
                    {
                        intHour = Convert.ToInt32(pdrowDeliverys[i][PO_VendorDeliveryScheduleTable.DELHOUR_FLD]);
                        intMin = Convert.ToInt32(pdrowDeliverys[i][PO_VendorDeliveryScheduleTable.DELMIN_FLD]);
                        dtmDeliTimes[i] = new DateTime(pdtmDate.Year, pdtmDate.Month, pdtmDate.Day, intHour, intMin, 0);
                    }
                    // check the rule
                    for (int i = pdrowDeliverys.Length - 1; i >= 0; i--)
                    {
                        intHour = Convert.ToInt32(pdrowDeliverys[i][PO_VendorDeliveryScheduleTable.DELHOUR_FLD]);
                        intMin = Convert.ToInt32(pdrowDeliverys[i][PO_VendorDeliveryScheduleTable.DELMIN_FLD]);
                        dtmDeliTimes[i] = new DateTime(pdtmDate.Year, pdtmDate.Month, pdtmDate.Day, intHour, intMin, 0);
                        if (dtmDeliTimes[i] <= pdtmDate)
                        {
                            dtmStartOfSpace = dtmDeliTimes[i];
                            blnOK = true;
                            // Get EndDate
                            if (i + 1 < pdrowDeliverys.Length)
                            {
                                dtmEndOfSpace = dtmDeliTimes[i + 1]; // get next day
                            }
                            else // move to next day
                            {
                                dtmEndOfSpace = (dtmDeliTimes[0]).AddDays(1);
                            }
                            break;
                        }
                    }
                    // If it's not belong to this day then change to prev day
                    if (!blnOK)
                    {
                        dtmStartOfSpace = dtmDeliTimes[pdrowDeliverys.Length - 1].AddDays(-1);
                        dtmEndOfSpace = dtmDeliTimes[0];
                    }

                    dtmStartOfSpace = GetNearestWorkingDay(dtmStartOfSpace, pdstCalendar);
                    #endregion
                }
                #endregion
            }

            #endregion

            return new[] {dtmStartOfSpace, dtmEndOfSpace};
        }

        /// <summary>
        /// Group all PODelivery by Delivery Policy of Vendor
        /// </summary>
        /// <param name="pdtbPODelivery"></param>
        /// <param name="pdrowDeliverys"></param>
        /// <param name="pType"></param>
        /// <param name="pdtmFirstValidWorkDay"></param>
        /// <param name="dstCalendar"></param>
        /// <param name="blnIsLocal"></param>
        /// <returns></returns>
        private DataTable GroupPODeliverys(DataTable pdtbPODelivery, DataRow[] pdrowDeliverys, PODeliveryTypeEnum pType, DateTime pdtmFirstValidWorkDay, DataSet dstCalendar, bool blnIsLocal)
		{
			DataTable dtbResultAfterGroup = pdtbPODelivery.Clone();
			
			pdtbPODelivery.DefaultView.Sort = PO_DeliveryScheduleTable.SCHEDULEDATE_FLD;
			DateTime dtmStart = new DateTime(), dtmEnd = new DateTime();
			DateTime dtmOriginSchedule;
			if (blnIsLocal)
				dtmOriginSchedule = (DateTime) pdtbPODelivery.DefaultView[0][PO_DeliveryScheduleTable.SCHEDULEDATE_FLD];
			else
				dtmOriginSchedule = (DateTime) pdtbPODelivery.DefaultView[pdtbPODelivery.DefaultView.Count - 1][PO_DeliveryScheduleTable.SCHEDULEDATE_FLD];

			var startEndDates = GetStartEndDate(dtmOriginSchedule, pType, pdrowDeliverys, dstCalendar);
            dtmStart = startEndDates[0];
            dtmEnd = startEndDates[1];

			bool okNewSpace = true;
			if (blnIsLocal) // domestic
			{
				for (int i =0; i <pdtbPODelivery.DefaultView.Count; i++)
				{
				    var rowScheduleDate = (DateTime) pdtbPODelivery.DefaultView[i][PO_DeliveryScheduleTable.SCHEDULEDATE_FLD];

                    if (rowScheduleDate >= dtmStart && rowScheduleDate < dtmEnd)
					{
						#region if SCHEDULEDATE_FLD belong [StartDate,EndDate)

						//add to Deleviry line
						if (okNewSpace)
						{
							#region new delivery line

							DataRow drowData = dtbResultAfterGroup.NewRow();

							#region verify schedule date
							DateTime dtmScheduleDate = dtmStart;
							DateTime dtmMyStart = dtmStart, dtmMyEnd = dtmEnd;
							while (dtmMyEnd < pdtmFirstValidWorkDay)
							{
								switch (pType)
								{
									case PODeliveryTypeEnum.Daily:
										dtmMyStart = dtmMyStart.AddDays(1);
										dtmMyEnd = dtmMyEnd.AddDays(1);
										break;
									case PODeliveryTypeEnum.Weekly:
										dtmMyStart = dtmMyStart.AddDays(7);
										dtmMyEnd = dtmMyEnd.AddDays(7);
										break;
									default:
										dtmMyStart = dtmMyStart.AddMonths(1);
										dtmMyEnd = dtmMyEnd.AddMonths(1);
										break;
								}
							}
							dtmScheduleDate = (dtmMyStart < pdtmFirstValidWorkDay) ? dtmMyEnd : dtmMyStart;
							#endregion

							dtmScheduleDate = GetNearestWorkingDay(dtmScheduleDate, dstCalendar);
							drowData[PO_DeliveryScheduleTable.SCHEDULEDATE_FLD] = dtmScheduleDate;
							drowData[PO_PurchaseOrderDetailTable.PRODUCTID_FLD] = pdtbPODelivery.DefaultView[i][PO_PurchaseOrderDetailTable.PRODUCTID_FLD];
							drowData[PO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD] = pdtbPODelivery.DefaultView[i][PO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD];
						
							drowData["OriginalDELIVERYQUANTITY"] = pdtbPODelivery.DefaultView[i]["OriginalDELIVERYQUANTITY"];
							drowData["CPOID"] = pdtbPODelivery.DefaultView[i]["CPOID"];

							double decFixLT = Convert.ToDouble(pdtbPODelivery.DefaultView[i]["FixLT"]);
							drowData["StartOfSCHEDULEDATE"] = dtmScheduleDate.AddSeconds(-decFixLT);
							dtbResultAfterGroup.Rows.Add(drowData);
							okNewSpace = false;

							#endregion
						}
						else
						{
							#region increase quantity for delivery line

							dtbResultAfterGroup.Rows[dtbResultAfterGroup.Rows.Count -1][PO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD]
								= (decimal) dtbResultAfterGroup.Rows[dtbResultAfterGroup.Rows.Count -1][PO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD]
								+ (decimal)	pdtbPODelivery.DefaultView[i][PO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD];

							dtbResultAfterGroup.Rows[dtbResultAfterGroup.Rows.Count -1]["OriginalDELIVERYQUANTITY"]
								= (decimal) dtbResultAfterGroup.Rows[dtbResultAfterGroup.Rows.Count -1]["OriginalDELIVERYQUANTITY"]
								+ (decimal)	pdtbPODelivery.DefaultView[i]["OriginalDELIVERYQUANTITY"];

							#endregion
						}

						#endregion
					}
					else
					{
						#region SCHEDULEDATE_FLD not belong to [StartDate,EndDate)

						startEndDates = GetStartEndDate(rowScheduleDate, pType, pdrowDeliverys, dstCalendar);
                        dtmStart = startEndDates[0];
                        dtmEnd = startEndDates[1];
                        //add to Delivery line
                        okNewSpace = false;
					
						#region verify schedule date
						DateTime dtmScheduleDate = dtmStart;
						DateTime dtmMyStart = dtmStart, dtmMyEnd = dtmEnd;
						while (dtmMyEnd < pdtmFirstValidWorkDay)
						{
							switch (pType)
							{
								case PODeliveryTypeEnum.Daily:
									dtmMyStart = dtmMyStart.AddDays(1);
									dtmMyEnd = dtmMyEnd.AddDays(1);
									break;
								case PODeliveryTypeEnum.Weekly:
									dtmMyStart = dtmMyStart.AddDays(7);
									dtmMyEnd = dtmMyEnd.AddDays(7);
									break;
								default:
									dtmMyStart = dtmMyStart.AddMonths(1);
									dtmMyEnd = dtmMyEnd.AddMonths(1);
									break;
							}
						}
						dtmScheduleDate = (dtmMyStart < pdtmFirstValidWorkDay) ? dtmMyEnd : dtmMyStart;
						#endregion

						dtmScheduleDate = GetNearestWorkingDay(dtmScheduleDate, dstCalendar);

						// try to check the schedule date
						string strFilter = PO_DeliveryScheduleTable.SCHEDULEDATE_FLD + "='" + dtmScheduleDate.ToString("G") + "'";
						if (dtbResultAfterGroup.Select(strFilter).Length > 0)
						{
							#region increase quantity for delivery line

							dtbResultAfterGroup.Select(strFilter)[0][PO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD]
								= (decimal) dtbResultAfterGroup.Select(strFilter)[0][PO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD]
								+ (decimal)	pdtbPODelivery.DefaultView[i][PO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD];

							dtbResultAfterGroup.Select(strFilter)[0]["OriginalDELIVERYQUANTITY"]
								= (decimal) dtbResultAfterGroup.Select(strFilter)[0]["OriginalDELIVERYQUANTITY"]
								+ (decimal)	pdtbPODelivery.DefaultView[i]["OriginalDELIVERYQUANTITY"];

							#endregion
						}
						else
						{
							#region add new delivery line

							DataRow drowData = dtbResultAfterGroup.NewRow();
							drowData[PO_DeliveryScheduleTable.SCHEDULEDATE_FLD] = dtmScheduleDate;
							drowData[PO_PurchaseOrderDetailTable.PRODUCTID_FLD] = pdtbPODelivery.DefaultView[i][PO_PurchaseOrderDetailTable.PRODUCTID_FLD];
							drowData[PO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD] = pdtbPODelivery.DefaultView[i][PO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD];

							drowData["OriginalDELIVERYQUANTITY"] = pdtbPODelivery.DefaultView[i]["OriginalDELIVERYQUANTITY"];
							drowData["CPOID"] = pdtbPODelivery.DefaultView[i]["CPOID"];

							double decFixLT = Convert.ToDouble(pdtbPODelivery.DefaultView[i]["FixLT"]);
							drowData["StartOfSCHEDULEDATE"] = dtmScheduleDate.AddSeconds(-decFixLT);

							dtbResultAfterGroup.Rows.Add(drowData);

							#endregion
						}

						#endregion
					}
				}
			}
			else // export
			{
				for (int i =0; i < pdtbPODelivery.DefaultView.Count; i++)
				{
					//add to Deleviry line
					if (okNewSpace)
					{
						#region new delivery line

						DataRow drowData = dtbResultAfterGroup.NewRow();
                        //DateTime dtmScheduleDate = dtmStart.AddMonths(-1);
                        DateTime dtmScheduleDate = dtmStart;

                        dtmScheduleDate = GetNearestWorkingDay(dtmScheduleDate, dstCalendar);
						drowData[PO_DeliveryScheduleTable.SCHEDULEDATE_FLD] = dtmScheduleDate;
						drowData[PO_PurchaseOrderDetailTable.PRODUCTID_FLD] = pdtbPODelivery.DefaultView[i][PO_PurchaseOrderDetailTable.PRODUCTID_FLD];
						drowData[PO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD] = pdtbPODelivery.DefaultView[i][PO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD];
						
						drowData["OriginalDELIVERYQUANTITY"] = pdtbPODelivery.DefaultView[i]["OriginalDELIVERYQUANTITY"];
						drowData["CPOID"] = pdtbPODelivery.DefaultView[i]["CPOID"];

						double decFixLT = Convert.ToDouble(pdtbPODelivery.DefaultView[i]["FixLT"]);
						drowData["StartOfSCHEDULEDATE"] = dtmScheduleDate.AddSeconds(-decFixLT);
						dtbResultAfterGroup.Rows.Add(drowData);
						okNewSpace = false;

						#endregion
					}
					else
					{
						#region increase quantity for delivery line

						dtbResultAfterGroup.Rows[dtbResultAfterGroup.Rows.Count -1][PO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD]
							= (decimal) dtbResultAfterGroup.Rows[dtbResultAfterGroup.Rows.Count -1][PO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD]
							+ (decimal)	pdtbPODelivery.DefaultView[i][PO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD];

						dtbResultAfterGroup.Rows[dtbResultAfterGroup.Rows.Count -1]["OriginalDELIVERYQUANTITY"]
							= (decimal) dtbResultAfterGroup.Rows[dtbResultAfterGroup.Rows.Count -1]["OriginalDELIVERYQUANTITY"]
							+ (decimal)	pdtbPODelivery.DefaultView[i]["OriginalDELIVERYQUANTITY"];

						#endregion
					}
				}
			}
			
			return dtbResultAfterGroup;
		}
		
		/// <summary>
		/// Tinh toan cac thong so tren form PO
		/// </summary>
		/// <param name="pdstPODetail"></param>
		/// <param name="pvoMaster"></param>
		private void ReCalculate(DataSet pdstPODetail, PO_PurchaseOrderMasterVO pvoMaster)
		{
			if(pdstPODetail.Tables.Count > 0)
			{
                foreach (DataRow objRow in pdstPODetail.Tables[0].Rows)
                {
                    if (objRow.RowState == DataRowState.Deleted) continue;
                    //•	If VAT field checked then the system calculate VAT amount base on unit price and VAT percent of the item
                    if (objRow[PO_PurchaseOrderDetailTable.PRODUCTID_FLD] == DBNull.Value)
                    {
                        continue;
                    }
                    decimal decDiscountAmount = 0;
                    if (objRow[PO_PurchaseOrderDetailTable.DISCOUNTAMOUNT_FLD] != DBNull.Value)
                        decDiscountAmount = decimal.Parse(objRow[PO_PurchaseOrderDetailTable.DISCOUNTAMOUNT_FLD].ToString());

                    try
                    {
                        objRow[PO_PurchaseOrderDetailTable.VATAMOUNT_FLD] = (decimal.Parse(objRow[PO_PurchaseOrderDetailTable.ORDERQUANTITY_FLD].ToString())
                            * decimal.Parse(objRow[PO_PurchaseOrderDetailTable.UNITPRICE_FLD].ToString()) - decDiscountAmount)
                            * decimal.Parse(objRow[PO_PurchaseOrderDetailTable.VAT_FLD].ToString()) / 100;
                        pvoMaster.TotalVAT += (decimal)objRow[PO_PurchaseOrderDetailTable.VATAMOUNT_FLD];
                    }
                    catch
                    {
                        objRow[PO_PurchaseOrderDetailTable.VATAMOUNT_FLD] = 0;
                    }
                    //•	If Export Tax field checked then the system calculate export tax amount base on unit price and export tax percent of the item 
                    try
                    {
                        objRow[PO_PurchaseOrderDetailTable.IMPORTTAXAMOUNT_FLD] = (decimal.Parse(objRow[PO_PurchaseOrderDetailTable.ORDERQUANTITY_FLD].ToString())
                            * decimal.Parse(objRow[PO_PurchaseOrderDetailTable.UNITPRICE_FLD].ToString()) - decDiscountAmount)
                            * decimal.Parse(objRow[PO_PurchaseOrderDetailTable.IMPORTTAX_FLD].ToString()) / 100;
                        pvoMaster.TotalImportTax += (decimal)objRow[PO_PurchaseOrderDetailTable.IMPORTTAXAMOUNT_FLD];
                    }
                    catch
                    {
                        objRow[PO_PurchaseOrderDetailTable.IMPORTTAXAMOUNT_FLD] = 0;
                    }
                    //•	If Special Tax field checked then the system calculate special tax amount base on unit price and special tax percent of the item 
                    try
                    {
                        decimal decImpTax, decVatAmount;
                        if ((objRow[PO_PurchaseOrderDetailTable.VATAMOUNT_FLD] != null) && (objRow[PO_PurchaseOrderDetailTable.VATAMOUNT_FLD] != DBNull.Value))
                        {
                            decVatAmount = decimal.Parse(objRow[PO_PurchaseOrderDetailTable.VATAMOUNT_FLD].ToString());
                        }
                        else
                            decVatAmount = 0;
                        if ((objRow[PO_PurchaseOrderDetailTable.IMPORTTAXAMOUNT_FLD] != null) && (objRow[PO_PurchaseOrderDetailTable.IMPORTTAXAMOUNT_FLD] != DBNull.Value))
                        {
                            decImpTax = decimal.Parse(objRow[PO_PurchaseOrderDetailTable.IMPORTTAXAMOUNT_FLD].ToString());
                        }
                        else
                            decImpTax = 0;
                        objRow[PO_PurchaseOrderDetailTable.SPECIALTAXAMOUNT_FLD] = ((decimal.Parse(objRow[PO_PurchaseOrderDetailTable.ORDERQUANTITY_FLD].ToString())
                            * decimal.Parse(objRow[PO_PurchaseOrderDetailTable.UNITPRICE_FLD].ToString()) - decDiscountAmount) + decImpTax + decVatAmount)
                            * decimal.Parse(objRow[PO_PurchaseOrderDetailTable.SPECIALTAX_FLD].ToString()) / 100;
                    }
                    catch
                    {
                    }
                    decimal decVAT;
                    try
                    {
                        decVAT = decimal.Parse(objRow[PO_PurchaseOrderDetailTable.VATAMOUNT_FLD].ToString());
                    }
                    catch
                    {
                        decVAT = 0;
                    }
                    decimal decImportTax;
                    try
                    {
                        decImportTax = decimal.Parse(objRow[PO_PurchaseOrderDetailTable.IMPORTTAXAMOUNT_FLD].ToString());
                    }
                    catch
                    {
                        decImportTax = 0;
                    }
                    decimal decSpecialTax;
                    try
                    {
                        decSpecialTax = decimal.Parse(objRow[PO_PurchaseOrderDetailTable.SPECIALTAXAMOUNT_FLD].ToString());
                        pvoMaster.TotalSpecialTax += (decimal)objRow[PO_PurchaseOrderDetailTable.SPECIALTAXAMOUNT_FLD];
                    }
                    catch
                    {
                        decSpecialTax = 0;
                    }
                    //•	The system calculate Total Amount = (quantity * unit price)+ VAT + Export Tax + Special Tax
                    try
                    {
                        objRow[PO_PurchaseOrderDetailTable.TOTALAMOUNT_FLD] = decimal.Parse(objRow[PO_PurchaseOrderDetailTable.ORDERQUANTITY_FLD].ToString())
                            * decimal.Parse(objRow[PO_PurchaseOrderDetailTable.UNITPRICE_FLD].ToString()) - decDiscountAmount + decVAT + decImportTax + decSpecialTax;
                        pvoMaster.TotalAmount += (decimal)objRow[PO_PurchaseOrderDetailTable.TOTALAMOUNT_FLD];
                    }
                    catch
                    {
                        // do nothing
                    }
                    //•	The system calculate Net Amount = Total Amount – Discount Amount
                    try
                    {
                        objRow[PO_PurchaseOrderDetailTable.NETAMOUNT_FLD] = decimal.Parse(objRow[PO_PurchaseOrderDetailTable.ORDERQUANTITY_FLD].ToString())
                            * decimal.Parse(objRow[PO_PurchaseOrderDetailTable.UNITPRICE_FLD].ToString()) - decDiscountAmount;
                        pvoMaster.TotalNetAmount += (decimal)objRow[PO_PurchaseOrderDetailTable.NETAMOUNT_FLD];
                    }
                    catch
                    {
                        // do nothing
                    }
                }
			}
		}

		/// <summary>
		/// Gan gia tri exchange rate
		/// </summary>
		/// <param name="pintCurrencyID"></param>
		/// <param name="pdtmDate"></param>
		/// <returns></returns>
		private decimal FillExchangeRate(int pintCurrencyID, DateTime pdtmDate)
		{
			// Fill Exch. Rate if the system configured the exchange rate get form Exchange Rate Table
			// based on currency and transaction date (begin date<= transaction date <= end date and approved)
			const decimal DEFAULT_RATE = 1;
			const string  METHOD_NAME = THIS + ".FillExchangeRate()";
			int intExchangeRateID = 0;
			if (pintCurrencyID == 0) return intExchangeRateID;
			//•	If the currency is same as base(Home - CuongNT fixed) currency then the system automatically fill the number 1 to exchange rate field
			if(pintCurrencyID == SystemProperty.HomeCurrencyID)
			{
				return DEFAULT_RATE;
			}
			try
			{
				// Input Transaction date before execute this function
				PurchaseOrderBO boOrder = new PurchaseOrderBO();
				MST_ExchangeRateVO voExchange = (MST_ExchangeRateVO) boOrder.GetExchangeRate(pintCurrencyID, pdtmDate);
				if(voExchange.ExchangeRateID == 0)
				{
					// Do not found any Exchange Rate records which (begin effective date <= the current date <= end of effective date)
					return DEFAULT_RATE;
				}
				// fill value and return
				return voExchange.Rate;
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
			return intExchangeRateID;
		}

		/// <summary>
		/// Tim ngay lam viec gan nhat
		/// </summary>
		/// <param name="pdtmDate"></param>
		/// <param name="pdstCalendar"></param>
		/// <returns></returns>
		DateTime GetNearestWorkingDay(DateTime pdtmDate,DataSet pdstCalendar)
		{
			while(IsOffDay(pdtmDate,pdstCalendar))
			{
				pdtmDate = pdtmDate.AddDays(-1);
			}
			return pdtmDate;
		}

		/// <summary>
		/// Check the day is off day
		/// Return true if isoffday else return false
		/// </summary>
		/// <param name="pdtmDate"></param>
		/// <param name="pdstCalendar"></param>
		/// <returns></returns>
		private bool IsOffDay(DateTime pdtmDate, DataSet pdstCalendar)
		{
			const string METHOD_NAME = THIS + ".IsOffDay()";
			DayOfWeek objWeekDay = pdtmDate.DayOfWeek;
			DataRow[] drows = pdstCalendar.Tables[0].Select(MST_WorkingDayMasterTable.YEAR_FLD + " = " + pdtmDate.Year);
			DataRow[] drowDetails = pdstCalendar.Tables[1].Select(MST_WorkingDayDetailTable.OFFDAY_FLD + " = '" + pdtmDate.Date + "'");
			if(drowDetails.Length > 0)
			{
				return true;
			}
			if(drows.Length == 0) // return true;
			{
				throw new PCSBOException(ErrorCode.MESSAGE_DCP_SETTING_WORKING_CALENDAR,METHOD_NAME,null);
				// return true;
			}
			if(objWeekDay == DayOfWeek.Sunday)
			{
				if(bool.Parse(drows[0][MST_WorkingDayMasterTable.SUN_FLD].ToString()) == true) return false;
			}
			else if(objWeekDay == DayOfWeek.Saturday)
			{
				if(bool.Parse(drows[0][MST_WorkingDayMasterTable.SAT_FLD].ToString()) == true) return false;
			}
			else if(objWeekDay == DayOfWeek.Friday)
			{
				if(bool.Parse(drows[0][MST_WorkingDayMasterTable.FRI_FLD].ToString()) == true) return false;
			}
			else if(objWeekDay == DayOfWeek.Thursday)
			{
				if(bool.Parse(drows[0][MST_WorkingDayMasterTable.THU_FLD].ToString()) == true) return false;
			}
			else if(objWeekDay == DayOfWeek.Wednesday)
			{
				if(bool.Parse(drows[0][MST_WorkingDayMasterTable.WED_FLD].ToString()) == true) return false;
			}
			else if(objWeekDay == DayOfWeek.Tuesday)
			{
				if(bool.Parse(drows[0][MST_WorkingDayMasterTable.TUE_FLD].ToString()) == true) return false;
			}
			else if(objWeekDay == DayOfWeek.Monday)
			{
				if(bool.Parse(drows[0][MST_WorkingDayMasterTable.MON_FLD].ToString()) == true) return false;
			}
			return true;
		}
		
		/// <summary>
		/// So ngay lon nhat trong 1 thang bat ky
		/// </summary>
		/// <param name="pdtmTemp"></param>
		/// <returns></returns>
		int GetMaxDayOfMonth(DateTime pdtmTemp)
		{
			if(pdtmTemp.Month == 12) return 31;
			else
			{
				pdtmTemp = new DateTime(pdtmTemp.Year,pdtmTemp.Month+1,1,0,0,0);
				return (pdtmTemp.AddDays(-1)).Day;
			}
		}
		/// <summary>
		/// Check if the vendor is Local or Domestic
		/// </summary>
		/// <param name="pintVendorID">Vendor</param>
		/// <returns></returns>
		private bool IsLocal(int pintVendorID)
		{
			DataRow[] drowVendor = dtbVendor.Select(MST_PartyTable.PARTYID_FLD + "=" + pintVendorID);
			int intCountryID = 0;
			try
			{
				intCountryID = Convert.ToInt32(drowVendor[0][MST_PartyTable.COUNTRYID_FLD]);
			}
			catch{}
			return intCountryID == SystemProperty.CountryID;
		}
		#endregion		

		#region Convert 2 WO - Tuan DM
		
		private void ThreadForGenNewWO()
		{
			const string METHOD_NAME = THIS + ".ThreadForGenNewWO()";
			try
			{
				this.Cursor = Cursors.WaitCursor;
				if (dgrdData.RowCount == 0) return;
				DataTable dtbTableOfWorkOrder = dtbCPODetail.Clone();
				DataSet dstWODetailSchema = boWorkOrder.GetWODetailByMaster(0);
				DataView dtwAllCPOAffterSort = (dtbCPODetail.Copy()).DefaultView;
				dtwAllCPOAffterSort.RowFilter = MTR_CPODS.SELECT_COLUMN + Constants.EQUAL + 1.ToString()
					+ " AND " + MST_WorkCenterTable.PRODUCTIONLINEID_FLD + " > 0";
				dtwAllCPOAffterSort.Sort = MST_WorkCenterTable.PRODUCTIONLINEID_FLD + ", " + ITM_ProductTable.PRODUCTID_FLD + "," + MTR_CPOTable.STARTDATE_FLD;
				int i =0;
				if (dtwAllCPOAffterSort.Count == 0)
				{
					this.Cursor = Cursors.Default;
					return;
				}
				btnNewPOConvert.Enabled = false;
				btnExistingPOConvert.Enabled = false;
				btnNewWOConvert.Enabled = false;
				btnUpdateWorkOrder.Enabled = false;
				while (i < dtwAllCPOAffterSort.Count)
				{
					dtbTableOfWorkOrder.Rows.Clear();
					dtbTableOfWorkOrder.AcceptChanges();
					DataSet dstWODetail = dstWODetailSchema.Clone();
					for (int j =i; j < dtwAllCPOAffterSort.Count; j++)
					{
						if ((int)dtwAllCPOAffterSort[i][MST_WorkCenterTable.PRODUCTIONLINEID_FLD]
							==(int)dtwAllCPOAffterSort[j][MST_WorkCenterTable.PRODUCTIONLINEID_FLD])
						{
							dtbTableOfWorkOrder.ImportRow(dtwAllCPOAffterSort[j].Row);
						}
						else
						{
							DataRow[] drowsSort = dtbTableOfWorkOrder.Select(string.Empty, MTR_CPOTable.STARTDATE_FLD + " ASC, " + ITM_ProductTable.PRODUCTID_FLD + " ASC");
							DataTable dtbRealData = dtbTableOfWorkOrder.Clone();
							foreach (DataRow drow in drowsSort)
								dtbRealData.ImportRow(drow);
							ConvertToNewWorkOrder(dtbRealData, dstWODetail);
							i = j;
							break;
						}
						if (j == dtwAllCPOAffterSort.Count-1) 
						{
							DataRow[] drowsSort = dtbTableOfWorkOrder.Select(string.Empty, MTR_CPOTable.STARTDATE_FLD + " ASC, " + ITM_ProductTable.PRODUCTID_FLD + " ASC");
							DataTable dtbRealData = dtbTableOfWorkOrder.Clone();
							foreach (DataRow drow in drowsSort)
								dtbRealData.ImportRow(drow);
							ConvertToNewWorkOrder(dtbRealData, dstWODetail);
							i = j+1;
						}
					}
				}
				if (cboPlanType.Text.Trim() != lblDCP.Text.Trim())
				{
					btnNewPOConvert.Enabled = true;
					btnExistingPOConvert.Enabled = true;
				}
				btnNewWOConvert.Enabled = true;
				btnUpdateWorkOrder.Enabled = true;
				this.Cursor = Cursors.Default;
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
				this.Cursor = Cursors.Default;
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
				this.Cursor = Cursors.Default;
			}
		}

		/// <summary>
		/// Convert to new wo
		/// </summary>
		/// <param name="pdstCPOOfWorkCenter"></param>
		/// <param name="pdstWODetail"></param>
		private void ConvertToNewWorkOrder(DataTable pdstCPOOfWorkCenter, DataSet pdstWODetail)
		{
			PRO_WorkOrderMasterVO voWOMaster = new PRO_WorkOrderMasterVO();
			//create wo master automatically
			voWOMaster.CCNID = (int) cboCCN.SelectedValue;
			voWOMaster.MasterLocationID = Convert.ToInt32(txtMasLoc.Tag);
			voWOMaster.TransDate = DateTime.Parse(boUtils.GetDBDate().ToShortDateString());
			voWOMaster.ProductionLineID = Convert.ToInt32(pdstCPOOfWorkCenter.Rows[0][PRO_WorkOrderMasterTable.PRODUCTIONLINEID_FLD]);
			if(cboPlanType.Text == lblDCP.Text)
			{
				voWOMaster.DCOptionMasterID = Convert.ToInt32(txtCycle.Tag);
			}
			//Make prefix for work order 
			//Get Production Line Code
			ProductionLineBO boProductionLine = new ProductionLineBO();
			DataTable dtbProductionLine = boProductionLine.GetProductionLineCode(voWOMaster.ProductionLineID);
			string strWorkOrderNo = string.Empty;
			string strFormat_Number = "####";
			const string WONOMAX = "WorkOrderNoMax";

			if (dtbProductionLine.Rows.Count > 0)
			{
				strWorkOrderNo = dtbProductionLine.Rows[0][PRO_ProductionLineTable.CODE_FLD].ToString();
			}
			//Get Year, Month and Version of DCOption master
			PCSComProduction.DCP.BO.DCOptionsBO boDCOptions = new DCOptionsBO();
			DataRow drowDCOptionMaster = boDCOptions.GetDCOptionMaster(int.Parse(txtCycle.Tag.ToString()));
			DateTime dtmPlanningPeriod = (DateTime) drowDCOptionMaster[PRO_DCOptionMasterTable.PLANNINGPERIOD_FLD];
			strWorkOrderNo += dtmPlanningPeriod.Year.ToString();
			if (dtmPlanningPeriod.Month >= 10)
			{
				strWorkOrderNo += "-" + dtmPlanningPeriod.Month.ToString();
			}
			else
				strWorkOrderNo += "-0" + dtmPlanningPeriod.Month.ToString();
			
			strWorkOrderNo += "-V" + drowDCOptionMaster[PRO_DCOptionMasterTable.VERSION_FLD].ToString() + "-";
			//Build query
			string strSql = String.Empty;
			strSql =  " SELECT max(" + PRO_WorkOrderMasterTable.WORKORDERNO_FLD + ") WorkOrderNoMax " ;
			strSql += " FROM " + PRO_WorkOrderMasterTable.TABLE_NAME ;
			strSql += " WHERE " + PRO_WorkOrderMasterTable.WORKORDERNO_FLD + " like '"+ strWorkOrderNo + "%'" ;

			WorkOrderBO boWorkOrder = new WorkOrderBO();
			DataSet dstWorkOrderNo = new DataSet();
			dstWorkOrderNo = boWorkOrder.GetWorkOrderNo(strSql);
			if (dstWorkOrderNo.Tables[0].Rows.Count == 0)
			{
				strFormat_Number = "1".PadLeft(strFormat_Number.Length,'0');
				strWorkOrderNo += strFormat_Number;
			}
			else
			{
				int intNumberLength = strFormat_Number.Length;
				string strMaxValue = dstWorkOrderNo.Tables[0].Rows[0][WONOMAX].ToString();
				int intNextValue = 0;
				try 
				{
					intNextValue = int.Parse(strMaxValue.Substring(strWorkOrderNo.Length)) + 1;
				}
				catch 
				{
					//Find to the second max value to parse
					strSql = "select WorkOrderNo from pro_workordermaster where workorderno like '"
						+ strWorkOrderNo + "%' order by WorkOrderNo desc";
					dstWorkOrderNo = boWorkOrder.GetWorkOrderNo(strSql);
					int i = 0;
					while (i < dstWorkOrderNo.Tables[0].Rows.Count)
					{
						try
						{
							intNextValue = int.Parse(dstWorkOrderNo.Tables[0].Rows[i][PRO_WorkOrderMasterTable.WORKORDERNO_FLD].ToString().Substring(strWorkOrderNo.Length));
							break;
						}
						catch
						{
							i++;
						}
					}
					intNextValue += 1;
				}
				strWorkOrderNo += intNextValue.ToString().PadLeft(intNumberLength,'0');
			}
			voWOMaster.WorkOrderNo = strWorkOrderNo;
			
            var list = (from obj in SystemProperty.TableMenuEntry
                        where obj.FormLoad == WorkOrder.THIS
                        orderby obj.Menu_EntryID ascending
                        select obj).ToList<Sys_Menu_Entry>();

			//create wo detail
			int intLine = 0;
			
			if (pdstCPOOfWorkCenter.Columns.IndexOf(MTR_CPOTable.TABLE_NAME + MTR_CPOTable.STARTDATE_FLD) == -1)
			{
				pdstCPOOfWorkCenter.Columns.Add(MTR_CPOTable.TABLE_NAME + MTR_CPOTable.STARTDATE_FLD, typeof(DateTime));
				pdstCPOOfWorkCenter.Columns.Add(MTR_CPOTable.TABLE_NAME + MTR_CPOTable.DUEDATE_FLD, typeof(DateTime));
			}
			for (int i =0; i < pdstCPOOfWorkCenter.Rows.Count; i++)
			{
				DateTime dtmStartDate = (DateTime) pdstCPOOfWorkCenter.Rows[i][MTR_CPOTable.STARTDATE_FLD];
				pdstCPOOfWorkCenter.Rows[i][MTR_CPOTable.TABLE_NAME + MTR_CPOTable.STARTDATE_FLD] = new DateTime(dtmStartDate.Year, dtmStartDate.Month, dtmStartDate.Day, dtmStartDate.Hour, 0, 0);//.ToString(Constants.DATETIME_FORMAT_HOUR);

				DateTime dtmDueDate = (DateTime) pdstCPOOfWorkCenter.Rows[i][MTR_CPOTable.DUEDATE_FLD];
				pdstCPOOfWorkCenter.Rows[i][MTR_CPOTable.TABLE_NAME + MTR_CPOTable.DUEDATE_FLD] = new DateTime(dtmDueDate.Year, dtmDueDate.Month, dtmDueDate.Day, dtmDueDate.Hour, 0, 0);//.ToString(Constants.DATETIME_FORMAT_HOUR);
			}
			
			int j =0;
			string strLastProductID = string.Empty;
			DateTime dtmLastStart = DateTime.MinValue;
			DateTime dtmLastDue = DateTime.MinValue;
			while (j < pdstCPOOfWorkCenter.Rows.Count)
			{
				#region edited by dungla, fix bug overcome item in the list

				DateTime dtmStartDate = (DateTime)pdstCPOOfWorkCenter.Rows[j][MTR_CPOTable.TABLE_NAME + MTR_CPOTable.STARTDATE_FLD];
				DateTime dtmDueDate = (DateTime)pdstCPOOfWorkCenter.Rows[j][MTR_CPOTable.TABLE_NAME + MTR_CPOTable.DUEDATE_FLD];
				string strProductID = pdstCPOOfWorkCenter.Rows[j][MTR_CPOTable.PRODUCTID_FLD].ToString();
				if (dtmLastStart.Equals(dtmStartDate) && dtmLastDue.Equals(dtmDueDate) && strLastProductID.Equals(strProductID))
				{
					j++;
					continue;
				}
				dtmLastStart = dtmStartDate;
				dtmLastDue = dtmDueDate;
				strLastProductID = strProductID;

				#endregion

				string strSelect = MTR_CPOTable.TABLE_NAME + MTR_CPOTable.STARTDATE_FLD + "='" + pdstCPOOfWorkCenter.Rows[j][MTR_CPOTable.TABLE_NAME + MTR_CPOTable.STARTDATE_FLD].ToString() + "'"
					+ " and " + MTR_CPOTable.TABLE_NAME + MTR_CPOTable.DUEDATE_FLD + "='" + pdstCPOOfWorkCenter.Rows[j][MTR_CPOTable.TABLE_NAME + MTR_CPOTable.DUEDATE_FLD].ToString() + "'"
					+ " and " + MTR_CPOTable.PRODUCTID_FLD + " = " + strProductID;
				DataRow[] drowSames = pdstCPOOfWorkCenter.Select(strSelect);
				DataRow drowWODetail = pdstWODetail.Tables[0].NewRow();
				drowWODetail[PRO_WorkOrderDetailTable.LINE_FLD] = ++intLine;
				drowWODetail[ITM_ProductTable.PRODUCTID_FLD] = pdstCPOOfWorkCenter.Rows[j][MTR_CPOTable.PRODUCTID_FLD];
				drowWODetail[ITM_ProductTable.STOCKUMID_FLD] = pdstCPOOfWorkCenter.Rows[j][MTR_CPOTable.STOCKUMID_FLD];
				drowWODetail[PRO_WorkOrderDetailTable.STARTDATE_FLD] = pdstCPOOfWorkCenter.Rows[j][MTR_CPOTable.STARTDATE_FLD];
				drowWODetail[PRO_WorkOrderDetailTable.DUEDATE_FLD] = drowSames[drowSames.Length-1][MTR_CPOTable.DUEDATE_FLD];
				drowWODetail[PRO_WorkOrderDetailTable.STATUS_FLD] = WOLineStatus.Unreleased;
				try
				{
					drowWODetail[PRO_WorkOrderDetailTable.ORDERQUANTITY_FLD] = 
						Convert.ToDecimal(pdstCPOOfWorkCenter.Compute("SUM(" + MTR_CPOTable.QUANTITY_FLD + ")", strSelect));
				}
				catch{}
				j++;
				pdstWODetail.Tables[0].Rows.Add(drowWODetail);
			}

			//update master and detail
			ArrayList arlCPOIDs = new ArrayList();
			if (cboPlanType.Text.Trim() == PlanTypeEnum.MPS.ToString())
			{
				foreach (DataRow drvCPO in pdstCPOOfWorkCenter.Rows)
				{
					arlCPOIDs.Add(drvCPO[MTR_CPOTable.CPOID_FLD].ToString());
				}
			}
			else
			{
				foreach (DataRow drvCPO in pdstCPOOfWorkCenter.Rows)
				{
					arlCPOIDs.Add(drvCPO[PRO_DCPResultDetailTable.DCPRESULTDETAILID_FLD].ToString());
				}
			}
			voWOMaster.WorkOrderMasterID = boWorkOrder.AddNewWOImmediately(voWOMaster, pdstWODetail, arlCPOIDs, cboPlanType.Text.Trim());
			if (voWOMaster.WorkOrderMasterID != 0)
                new UtilsBO().UpdateUserNameModifyTransaction(SystemProperty.UserName, list[0].TableName, PRO_WorkOrderMasterTable.WORKORDERMASTERID_FLD, voWOMaster.WorkOrderMasterID);
			blnConvertWOSuccess = true;
		}		

		#endregion

		#region HACKED by Tuan TQ, Nov 04, 2005: Change request
		
		private void dtmDate_VisibleChanged(object sender, EventArgs e)
		{
			dtmDate.TextDetached = !dtmDate.Visible;
		}

		private void numQuantity_VisibleChanged(object sender, EventArgs e)
		{
			numQuantity.TextDetached = !numQuantity.Visible;
		}

		//Add: production line in searching condition area.
		//Double click on grid: open Work Order or Purchase Order

		/// <summary>
		/// Fill related data on controls when select Production Line
		/// </summary>
		/// <param name="pblnAlwaysShowDialog"></param>
		private bool SelectProductionLine(string pstrMethodName, bool pblnAlwaysShowDialog)
		{
			try				
			{				
				Hashtable htbCriteria = new Hashtable();				

				//Call OpenSearchForm for selecting Production Line
				DataRowView drwResult = FormControlComponents.OpenSearchForm(PRO_ProductionLineTable.TABLE_NAME, PRO_ProductionLineTable.CODE_FLD, txtProductionLine.Text.Trim(), htbCriteria, pblnAlwaysShowDialog);
				
				//If has Production Line matched searching condition, fill values to form's controls
				if(drwResult != null)
				{
					txtProductionLine.Text = drwResult[PRO_ProductionLineTable.CODE_FLD].ToString();
					txtProductionLine.Tag = drwResult[PRO_ProductionLineTable.PRODUCTIONLINEID_FLD];

					//Reset Modified status
					txtProductionLine.Modified = false;
				}
				else if(!pblnAlwaysShowDialog)
				{
					txtProductionLine.Focus();
					return false;
				}

				return true;
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

		private void btnProductionLine_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnProductionLine_Click()";
			
			try
			{
				SelectProductionLine(METHOD_NAME, true);
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
		
		private void txtProductionLine_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtProductionLine_Validating()";

			try
			{
				if(txtProductionLine.Text.Length == 0)
				{
					txtProductionLine.Tag =ZERO_STRING;
					return;
				}
				else if(!txtProductionLine.Modified)
				{
					return;
				}

				e.Cancel = !SelectProductionLine(METHOD_NAME, false);
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
		
		private void txtProductionLine_KeyDown(object sender, KeyEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtProductionLine_KeyDown()";
			
			try
			{
				if ((e.KeyCode == Keys.F4) && (btnProductionLine.Enabled))
				{
					SelectProductionLine(METHOD_NAME, true);
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
		/// FillItemDataToGrid
		/// </summary>
		/// <param name="pdrowData"></param>
		/// <author>Trada</author>
		/// <date>Friday, April 21 2006</date>
		private void FillItemDataToGrid(DataRow pdrowData)
		{
			try
			{
				dgrdData.EditActive = true;
				dgrdData.Columns[ITM_ProductTable.PRODUCTID_FLD].Value = int.Parse(pdrowData[ITM_ProductTable.PRODUCTID_FLD].ToString());
				dgrdData.Columns[ITM_ProductTable.TABLE_NAME + ITM_ProductTable.CODE_FLD].Value = pdrowData[ITM_ProductTable.CODE_FLD];
				dgrdData.Columns[ITM_ProductTable.TABLE_NAME + ITM_ProductTable.DESCRIPTION_FLD].Value	= pdrowData[ITM_ProductTable.DESCRIPTION_FLD];
				dgrdData.Columns[ITM_ProductTable.TABLE_NAME + ITM_ProductTable.REVISION_FLD].Value = pdrowData[ITM_ProductTable.REVISION_FLD];
				dgrdData.Columns[MST_UnitOfMeasureTable.TABLE_NAME + MST_UnitOfMeasureTable.CODE_FLD].Value  = pdrowData[MST_UnitOfMeasureTable.TABLE_NAME + MST_UnitOfMeasureTable.CODE_FLD];
				dgrdData.Columns[ITM_CategoryTable.TABLE_NAME + ITM_CategoryTable.CODE_FLD].Value  = pdrowData[ITM_CategoryTable.TABLE_NAME + ITM_CategoryTable.CODE_FLD];
				dgrdData.Columns[MST_WorkCenterTable.TABLE_NAME + MST_WorkCenterTable.CODE_FLD].Value  = pdrowData[MST_WorkCenterTable.TABLE_NAME + MST_WorkCenterTable.CODE_FLD];
				dgrdData.Columns[MST_WorkCenterTable.WORKCENTERID_FLD].Value  = int.Parse(pdrowData[MST_WorkCenterTable.WORKCENTERID_FLD].ToString());
				dgrdData.Columns[ITM_RoutingTable.ROUTINGID_FLD].Value  = int.Parse(pdrowData[ITM_RoutingTable.ROUTINGID_FLD].ToString());
				try
				{
					dgrdData.Columns[ITM_ProductTable.LTVARIABLETIME_FLD].Value  = Convert.ToDecimal(pdrowData[ITM_ProductTable.LTVARIABLETIME_FLD]);
				}
				catch{}
				//Change status for IsManuale column
				dgrdData.Columns[PRO_DCPResultDetailTable.ISMANUAL_FLD].Value  = true;
				
			}
			catch (PCSDBException ex)
			{
				throw ex;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message, ex);
			}

		}
		/// <summary>
		/// dgrdData_ButtonClick
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Friday, April 21 2006</date>
		private void dgrdData_ButtonClick(object sender, ColEventArgs e)
		{
			const string METHOD_NAME = THIS + ".dgrdData_ButtonClick()";
			
			try
			{
				#region Old code
				//exit if grid is empty
				if(dgrdData.RowCount <= 0)
				{
					return;
				}

				//Click on WO No column
				if(dgrdData.Col == dgrdData.Columns.IndexOf(dgrdData.Columns[PRO_WorkOrderMasterTable.TABLE_NAME + PRO_WorkOrderMasterTable.WORKORDERNO_FLD]))
				{
					if(dgrdData.Columns[PRO_WorkOrderMasterTable.TABLE_NAME + PRO_WorkOrderMasterTable.WORKORDERNO_FLD].Value.ToString().Trim().Length == 0)
					{
						return;
					}

					int intWOMasterID = int.Parse(dgrdData.Columns[PRO_WorkOrderMasterTable.WORKORDERMASTERID_FLD].Value.ToString());
					int intCCNID = int.Parse(dgrdData.Columns[MTR_CPOTable.CCNID_FLD].Value.ToString());
					int intMasLocID = 0;
					string strMasLoc = string.Empty;

					PRO_WorkOrderMasterVO voWOMaster = (PRO_WorkOrderMasterVO) boWorkOrder.GetObjectWOMasterVO(intWOMasterID);
					if (voWOMaster != null && voWOMaster.MasterLocationID > 0)
					{
						intMasLocID = voWOMaster.MasterLocationID;
						DataRowView drvResult = FormControlComponents.OpenSearchForm(MST_MasterLocationTable.TABLE_NAME, MST_MasterLocationTable.MASTERLOCATIONID_FLD, intMasLocID.ToString(), null, false);
						if (drvResult != null)
							strMasLoc = drvResult[MST_MasterLocationTable.CODE_FLD].ToString();
					}
				
					WorkOrder frmWorkOrder = new WorkOrder(intWOMasterID, intCCNID, intMasLocID, strMasLoc);
					frmWorkOrder.ShowDialog();					
				}
				
				//Click on PO No column
				//if(e.ColIndex == dgrdData.Columns.IndexOf(dgrdData.Columns[PO_PurchaseOrderMasterTable.TABLE_NAME + PO_PurchaseOrderMasterTable.CODE_FLD]))
				if(dgrdData.Col == dgrdData.Columns.IndexOf(dgrdData.Columns[PO_PurchaseOrderMasterTable.TABLE_NAME + PO_PurchaseOrderMasterTable.CODE_FLD]))
				{
					if(dgrdData.Columns[PO_PurchaseOrderMasterTable.TABLE_NAME + PO_PurchaseOrderMasterTable.CODE_FLD].Value.ToString().Trim().Length == 0)
						return;
										
					int intPOMasterId =  int.Parse(dgrdData.Columns[PO_PurchaseOrderMasterTable.PURCHASEORDERMASTERID_FLD].Value.ToString());
					
					PurchaseOrder frmPurchaseOrder = new PurchaseOrder(intPOMasterId);
					frmPurchaseOrder.ShowDialog();
				}
				#endregion

				#region HACK: Trada 21-04-2006

				DataRowView drwResult = null;
				Hashtable htbCondition= new Hashtable();
				
				//Select Item
				if (dgrdData.Col == dgrdData.Columns.IndexOf(dgrdData.Columns[ITM_ProductTable.TABLE_NAME + ITM_ProductTable.CODE_FLD])
					|| dgrdData.Col == dgrdData.Columns.IndexOf(dgrdData.Columns[ITM_ProductTable.TABLE_NAME + ITM_ProductTable.DESCRIPTION_FLD]))
				{
					//If this row is old row then return
					if (dgrdData.Row != dgrdData.RowCount)
					{
						if (dtbCPODetail.Rows.Count == 0) return;
						if (dgrdData.AddNewMode != AddNewModeEnum.AddNewPending)
						{
							if (dtbCPODetail.Rows[dgrdData.Row].RowState != DataRowState.Added) return;
						}
					}
					if (txtProductionLine.Text.Trim() != string.Empty)
					{
						htbCondition.Add(PRO_ProductionLineTable.PRODUCTIONLINEID_FLD, int.Parse(txtProductionLine.Tag.ToString()));
					}
					if (dgrdData.AddNewMode == AddNewModeEnum.AddNewCurrent)
					{
						if (dgrdData.Col == dgrdData.Columns.IndexOf(dgrdData.Columns[ITM_ProductTable.TABLE_NAME + ITM_ProductTable.CODE_FLD]))
							drwResult = FormControlComponents.OpenSearchForm(VIEW_PRODUCTINFOR, ITM_ProductTable.CODE_FLD, dgrdData[dgrdData.Row, ITM_ProductTable.TABLE_NAME + ITM_ProductTable.CODE_FLD].ToString(), htbCondition, true);
						else
							drwResult = FormControlComponents.OpenSearchForm(VIEW_PRODUCTINFOR, ITM_ProductTable.DESCRIPTION_FLD, dgrdData[dgrdData.Row,ITM_ProductTable.TABLE_NAME + ITM_ProductTable.DESCRIPTION_FLD].ToString(), htbCondition, true);
					}
					else
					{
						if (dgrdData.Col == dgrdData.Columns.IndexOf(dgrdData.Columns[ITM_ProductTable.TABLE_NAME +ITM_ProductTable.CODE_FLD]))
							drwResult = FormControlComponents.OpenSearchForm(VIEW_PRODUCTINFOR, ITM_ProductTable.CODE_FLD, dgrdData.Columns[ITM_ProductTable.TABLE_NAME + ITM_ProductTable.CODE_FLD].Value.ToString().Trim(), htbCondition, true);
						else
							drwResult = FormControlComponents.OpenSearchForm(VIEW_PRODUCTINFOR, ITM_ProductTable.DESCRIPTION_FLD, dgrdData.Columns[ITM_ProductTable.TABLE_NAME + ITM_ProductTable.DESCRIPTION_FLD].Value.ToString().Trim(), htbCondition, true);
					}
					if (drwResult != null)
					{
						dgrdData.EditActive = true;
						FillItemDataToGrid(drwResult.Row);
					}
				}
				//Select Shift 
				if (dgrdData.Col == dgrdData.Columns.IndexOf(dgrdData.Columns[PRO_ShiftTable.SHIFTDESC_FLD]))
				{
//					//If this row is old row then return
//					if (dgrdData.Row != dtbCPODetail.Rows.Count)
//					{
//						if (dtbCPODetail.Rows.Count == 0) return;
//						if (dgrdData.AddNewMode != AddNewModeEnum.AddNewPending)
//						{
//							if (dtbCPODetail.Rows[dgrdData.Row].RowState != DataRowState.Added) return;
//						}
//					}
					if (dgrdData.AddNewMode == AddNewModeEnum.AddNewCurrent)
					{
						drwResult = FormControlComponents.OpenSearchForm(PRO_ShiftTable.TABLE_NAME, PRO_ShiftTable.SHIFTDESC_FLD, dgrdData[dgrdData.Row, PRO_ShiftTable.SHIFTDESC_FLD].ToString(), htbCondition, true);
					}
					else
					{
						drwResult = FormControlComponents.OpenSearchForm(PRO_ShiftTable.TABLE_NAME, PRO_ShiftTable.SHIFTDESC_FLD, dgrdData.Columns[PRO_ShiftTable.SHIFTDESC_FLD].Text.Trim(), htbCondition, true);
					}
					if (drwResult != null)
					{
						dgrdData.EditActive = true;
						//Fill data to grid
						dgrdData[dgrdData.Row, PRO_ShiftTable.SHIFTDESC_FLD] = drwResult[PRO_ShiftTable.SHIFTDESC_FLD].ToString();
						dgrdData.Columns[PRO_ShiftTable.SHIFTID_FLD].Value = int.Parse(drwResult[PRO_ShiftTable.SHIFTID_FLD].ToString());
					}
				}
				#endregion END: Trada 21-04-2006
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
		
		#endregion HACKED by Tuan TQ, Nov 04, 2005: Change request	
		
		/// <summary>
		/// dgrdData_BeforeColUpdate
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Friday, April 21 2006</date>
		private void dgrdData_BeforeColUpdate(object sender, C1.Win.C1TrueDBGrid.BeforeColUpdateEventArgs e)
		{
			const string METHOD_NAME = THIS + ".dgrdData_BeforeColUpdate()";
			try
			{
				Hashtable htbCriteria = new Hashtable();
				DataRowView drwResult = null;
				//Code
				if (e.Column.DataColumn.DataField == ITM_ProductTable.TABLE_NAME + ITM_ProductTable.CODE_FLD)
				{
					//If this row is old row then return
					if ((dgrdData.Row != dgrdData.RowCount - 1) && (dgrdData.Row != dgrdData.RowCount))
					{
						if ((dtbCPODetail.Rows[dgrdData.Row].RowState != DataRowState.Added) 
							&& ((bool)dgrdData.Columns[PRO_DCPResultDetailTable.ISMANUAL_FLD].Value != true))
							e.Cancel = true;
					}
					if (dgrdData.Columns[e.Column.DataColumn.DataField].Text.Trim() != string.Empty)
					{
						drwResult = FormControlComponents.OpenSearchForm(VIEW_PRODUCTINFOR, ITM_ProductTable.CODE_FLD, dgrdData.Columns[e.Column.DataColumn.DataField].Text.Trim(), htbCriteria, false);
						if (drwResult != null)
						{
							e.Column.DataColumn.Tag = drwResult.Row;	
						}
						else
						{
							e.Cancel = true;
						}
					}
				}
				//Description
				if (e.Column.DataColumn.DataField == ITM_ProductTable.TABLE_NAME + ITM_ProductTable.DESCRIPTION_FLD)
				{
					//If this row is old row then return
					if ((dgrdData.Row != dgrdData.RowCount - 1) && (dgrdData.Row != dgrdData.RowCount))
					{
						if ((dtbCPODetail.Rows[dgrdData.Row].RowState != DataRowState.Added) 
							&& ((bool)dgrdData.Columns[PRO_DCPResultDetailTable.ISMANUAL_FLD].Value != true))
							e.Cancel = true;
					}
					if (dgrdData.Columns[e.Column.DataColumn.DataField].Text.Trim() != string.Empty)
					{
						drwResult = FormControlComponents.OpenSearchForm(VIEW_PRODUCTINFOR, ITM_ProductTable.DESCRIPTION_FLD, dgrdData.Columns[e.Column.DataColumn.DataField].Text.Trim(), htbCriteria, false);
						if (drwResult != null)
						{
							e.Column.DataColumn.Tag = drwResult.Row;	
						}
						else
						{
							e.Cancel = true;
						}
					}
				}
				//Shift
				if (e.Column.DataColumn.DataField == PRO_ShiftTable.SHIFTDESC_FLD)
				{
					if (dgrdData.Columns[e.Column.DataColumn.DataField].Text.Trim() != string.Empty)
					{
						drwResult = FormControlComponents.OpenSearchForm(PRO_ShiftTable.TABLE_NAME, PRO_ShiftTable.SHIFTDESC_FLD, dgrdData.Columns[e.Column.DataColumn.DataField].Text.Trim(), htbCriteria, false);
						if (drwResult != null)
						{
							e.Column.DataColumn.Tag = drwResult.Row;	
						}
						else
						{
							e.Cancel = true;
						}
					}
				}
				//SafetyStock Amount Column
				if (e.Column.DataColumn.DataField == PRO_DCPResultDetailTable.SAFETYSTOCKAMOUNT)
				{
					if (e.Column.DataColumn.Text == string.Empty)
					{
						return;
					}
					try
					{
						decimal.Parse(e.Column.DataColumn.Text);
					}
					catch
					{
						e.Cancel = true;
						return;
					}
					if(decimal.Parse(e.Column.DataColumn.Text) < 0) 
						e.Cancel = true;
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
		/// dgrdData_BeforeColEdit
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Friday, April 21 2006</date>
		private void dgrdData_BeforeColEdit(object sender, C1.Win.C1TrueDBGrid.BeforeColEditEventArgs e)
		{
			const string METHOD_NAME = THIS + ".dgrdData_BeforeColEdit()";
			try
			{
				//If this row is old row then return
				if ((e.Column.DataColumn.DataField == ITM_ProductTable.TABLE_NAME + ITM_ProductTable.CODE_FLD)
					||(e.Column.DataColumn.DataField == ITM_ProductTable.TABLE_NAME + ITM_ProductTable.DESCRIPTION_FLD))
				{
					if (cboPlanType.SelectedItem.ToString() == PlanTypeEnum.MRP.ToString())
					{
						e.Cancel = true;
						return;
					}
					//if (dgrdData.Row != dtbCPODetail.Rows.Count && dgrdData.Row != 0)
					//if (dgrdData.Row != dtbCPODetail.Rows.Count)
					try 
					{
						if ((dtbCPODetail.Rows[dgrdData.Row].RowState != DataRowState.Added))
							e.Cancel = true;
					}
					catch 
					{}
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
		/// dgrdData_KeyDown
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Monday, April 24 2006</date>
		private void dgrdData_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			const string METHOD_NAME = THIS + ".dgrdData_KeyDown()";
			try
			{
				if ((e.KeyCode == Keys.Delete)&&(dgrdData.SelectedRows.Count > 0))
				{
					if (btnSave.Enabled)
					{
						dgrdData.AllowDelete = true;
						
						#region save ID for delete multirows purpose

						//Save selected rows
						int intSelectRows = dgrdData.SelectedRows.Count;
						ArrayList intIndexOfSelectedRows = new ArrayList();
						for (int i = 0; i < intSelectRows; i++)
							intIndexOfSelectedRows.Add(int.Parse(dgrdData.SelectedRows[i].ToString()));
						intIndexOfSelectedRows.Sort();
						//Save MasterID to Delete
						for (int i = intSelectRows - 1; i >= 0;  i--)
						{
							dgrdData.Row = (int) intIndexOfSelectedRows[i];
							if (cboPlanType.SelectedItem.ToString() == lblDCP.Text.Trim()) //DCP mode
							{
								#region DCP Mode

								if ((dgrdData.Columns[PRO_DCPResultMasterTable.DCPRESULTMASTERID_FLD].Value	 != null)
									&& (dgrdData.Columns[PRO_DCPResultMasterTable.DCPRESULTMASTERID_FLD].Value != DBNull.Value))
								{
									//check if arrMasterIDToUpdate has this DCPResultMasterID
									Int64 intDCPResultMasterID = Convert.ToInt64(dgrdData.Columns[PRO_DCPResultMasterTable.DCPRESULTMASTERID_FLD].Value);
									if (arrMasterIDToUpdate.Count > 0)
									{
										int intCounter = 0;
										for (int j = 0; j < arrMasterIDToUpdate.Count; j++)
											if ((Int64)arrMasterIDToUpdate[j] != intDCPResultMasterID)
												intCounter++;
										if (intCounter == arrMasterIDToUpdate.Count)
										{
											//add new value to array
											strMasterIDToUpdate += "," + dgrdData.Columns[PRO_DCPResultMasterTable.DCPRESULTMASTERID_FLD].Value.ToString();
											arrMasterIDToUpdate.Add(Convert.ToInt64(dgrdData.Columns[PRO_DCPResultDetailTable.DCPRESULTMASTERID_FLD].Value));
										}
									}
									else
									{
										strMasterIDToUpdate += "," + dgrdData.Columns[PRO_DCPResultMasterTable.DCPRESULTMASTERID_FLD].Value.ToString();
										arrMasterIDToUpdate.Add(Convert.ToInt64(dgrdData.Columns[PRO_DCPResultDetailTable.DCPRESULTMASTERID_FLD].Value));
									}
								}

								#endregion
							}
							else // MRP Mode
							{
								#region MRP Mode
								strCPOIDToDelete += "," + dgrdData.Columns[MTR_CPOTable.CPOID_FLD].Value.ToString();
								#endregion
							}
						}
						
						#endregion

						//delete detail row
						FormControlComponents.DeleteMultiRowsOnTrueDBGrid(dgrdData);
						
						txtNumRows.Value = dgrdData.RowCount;
					}

				}
				if (e.KeyCode == Keys.F4)
				{
					if (btnSave.Enabled)
					{
						dgrdData_ButtonClick(sender, null);
					}
				}
				if (e.KeyCode == Keys.F12)
				{
					dgrdData.Row = dgrdData.RowCount;
					dgrdData.Col = dgrdData.Columns.IndexOf(dgrdData.Columns[ITM_ProductTable.TABLE_NAME + ITM_ProductTable.CODE_FLD]);
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

		private void dgrdData_OnAddNew(object sender, System.EventArgs e)
		{
		
		}
		
		private decimal GetQuantityForCPO(DataRowView drowItem, decimal pdecQuantity)
		{
			#region remove minorder and multiple order
			if(pdecQuantity < 0) return 0;

			if (drowItem[ITM_ProductTable.ORDERQUANTITY_FLD].ToString().Trim() != string.Empty && pdecQuantity < decimal.Parse(drowItem[ITM_ProductTable.ORDERQUANTITY_FLD].ToString()))
			{
				pdecQuantity = decimal.Parse(drowItem[ITM_ProductTable.ORDERQUANTITY_FLD].ToString());
			}
			// drowItem[ITM_ProductTable.ORDERQUANTITY_FLD].ToString().Trim() != string.Empty && 
			if (drowItem[ITM_ProductTable.ORDERQUANTITYMULTIPLE_FLD].ToString() != string.Empty 
				&& decimal.Parse(drowItem[ITM_ProductTable.ORDERQUANTITYMULTIPLE_FLD].ToString()) != 0 
				&& (pdecQuantity % decimal.Parse(drowItem[ITM_ProductTable.ORDERQUANTITYMULTIPLE_FLD].ToString().Trim()) != 0)
				)
			{
				pdecQuantity =  (decimal.Floor(pdecQuantity/decimal.Parse(drowItem[ITM_ProductTable.ORDERQUANTITYMULTIPLE_FLD].ToString().Trim())) + 1)
					*decimal.Parse(drowItem[ITM_ProductTable.ORDERQUANTITYMULTIPLE_FLD].ToString().Trim());
			}

			#endregion

			return pdecQuantity;
		}
		
		/// <summary>
		/// Gets the first valid work day of cycle
		/// </summary>
		/// <param name="pdtmAsOfDate">As Of Date</param>
		/// <param name="pdstCalendar">Workday Calendar</param>
		/// <returns>First Valid work day</returns>
		private DateTime GetFirstValidWorkDay(DateTime pdtmAsOfDate, DataSet pdstCalendar)
		{
			while (IsOffDay(pdtmAsOfDate, pdstCalendar))
				pdtmAsOfDate = pdtmAsOfDate.AddDays(1);
			return pdtmAsOfDate;
		}
	}
}				