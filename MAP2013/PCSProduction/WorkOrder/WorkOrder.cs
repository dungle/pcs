using System;
using System.Data;
using System.Collections;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Windows.Forms;
using C1.Win.C1Input;
using C1.Win.C1Preview;
using C1.C1Report;
using C1.Win.C1TrueDBGrid;
using PCSComMaterials.Plan.DS;
using PCSComProduction.DCP.BO;
using PCSComProduction.WorkOrder.BO;
using PCSComProduction.WorkOrder.DS;
using PCSComUtils.Common;
using PCSComUtils.Common.BO;
using PCSComUtils.MasterSetup.DS;
using PCSComUtils.PCSExc;
using PCSUtils.Log;
using PCSUtils.Utils;

namespace PCSProduction.WorkOrder
{
	/// <summary>
	/// This class uses to establish and maintain Work Order.
	/// </summary>
	public class WorkOrder : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label lblMasLoc;
		private System.Windows.Forms.Label lblCCN;
		private System.Windows.Forms.Label lblTransDate;
		private System.Windows.Forms.CheckBox chkManufacture;
		private System.Windows.Forms.CheckBox chkFinance;
		private System.Windows.Forms.TextBox txtDescription;
		private System.Windows.Forms.Label lblDescription;
		private System.Windows.Forms.TextBox txtWONo;
		private System.Windows.Forms.Label lblWO_No;
		private System.Windows.Forms.Button btnEdit;
		private System.Windows.Forms.Button btnClose;
		private System.Windows.Forms.Button btnHelp;
		private System.Windows.Forms.Button btnDelete;
		private System.Windows.Forms.Button btnSave;
		private System.Windows.Forms.Button btnAdd;
		private System.Windows.Forms.Button btnPrint;
		private System.Windows.Forms.Button btnRouting;
		private System.Windows.Forms.Button btnBOM;
		private System.Windows.Forms.Button btnCost;
		private System.Windows.Forms.Button btnSearchWO;
		private C1.Win.C1TrueDBGrid.C1TrueDBGrid dgrdData;
		private System.Windows.Forms.TextBox txtMasterLocation;
		private System.Windows.Forms.Button btnSearchMasLoc;
		private C1.Win.C1Input.C1DateEdit dtmTransDate;
		private C1.Win.C1List.C1Combo cboCCN;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		private PCSComProduction.WorkOrder.BO.WorkOrderBO boWorkOrder = new WorkOrderBO();
		public const string THIS = "PCSProduction.WorkOrder.WorkOrder";

		private const string UNRELEASE = "UnRelease";
		private const string PRODUCT = "Product";
		private const string SALE_ORDER_CODE = "SaleOrderCode";
		private const string UM = "UM";
		private const string AGC_CODE = "AGCCode";
		private const string SALE_ORDER_FOR_WOLINE_VIEWNAME = "v_SaleOrderForWOLine";
		private const string PRODUCT_IN_PRODUCTIONLINE_VIEWNAME = "v_ProductInProductionLine";
		
		private const string STATUS_STRING = "StatusString";
		private const int UM_CODE = 0;
		private const int COST_METHOD = 1;
		private const int AGC_COD = 2;
		private const int EST_CODE = 3;
		private string ReportDefinitionFolder = string.Empty;
		private const string ReportFileName = "WorkOrderReport.xml";

		private DataTable dtbGridLayOut;
		private PCSComProduction.WorkOrder.DS.PRO_WorkOrderMasterVO voWorkOrderMaster = new PRO_WorkOrderMasterVO();
		private DataSet dstGridData;
		private MST_MasterLocationVO voMasLoc = new MST_MasterLocationVO();
		private EnumAction mFormMode = EnumAction.Default;
		private ArrayList ArlWOLineID= new ArrayList();
		private C1.Win.C1Input.C1DateEdit dtmDate;
		private bool blnHasError = false;
		string strLastValidPro = string.Empty;

		public EnumAction FormMode
		{
			get { return mFormMode; }
			set { mFormMode = value; }
		}

		private WOFormState mWOFormState = WOFormState.Normal;
		private MTR_CPOVO voCPO;
		private System.Windows.Forms.TextBox txtProductionLine;
		private System.Windows.Forms.Label lblProductionLine;
		private System.Windows.Forms.Button btnProductionLine;
		private System.Windows.Forms.Label lblWorkOrderReport;
		private System.Windows.Forms.TextBox txtDCPCycle;
		private System.Windows.Forms.Label lblDCPCycle;
		private System.Windows.Forms.Button btnDCPCycle;
		private System.Windows.Forms.TextBox txtDCPDescription;
		private DataView dtwCPOs;
		public WorkOrder()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
		}
		public WorkOrder(int pintWOMasterID, int pintCCNID, int pintMasLocID, string pstrMasLoc)
		{
			const string METHOD_NAME = THIS + ".WorkOrder()";
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			try
			{
				voWorkOrderMaster.WorkOrderMasterID = pintWOMasterID;
				voMasLoc.MasterLocationID = pintMasLocID;
				txtMasterLocation.Text = pstrMasLoc;
				UtilsBO boUtils = new UtilsBO();
				DataSet dstCCN = boUtils.ListCCN();
				cboCCN.DataSource = dstCCN.Tables[MST_CCNTable.TABLE_NAME];
				cboCCN.DisplayMember	= MST_CCNTable.CODE_FLD;
				cboCCN.ValueMember		= MST_CCNTable.CCNID_FLD;
				FormControlComponents.PutDataIntoC1ComboBox(cboCCN,dstCCN.Tables[MST_CCNTable.TABLE_NAME],MST_CCNTable.CODE_FLD,MST_CCNTable.CCNID_FLD,MST_CCNTable.TABLE_NAME);
				cboCCN.SelectedValue = pintCCNID;
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

		public WorkOrder(DataView pdtwCPOs, int pintWOMasterID, object pobjCPO)
		{
			InitializeComponent();
			mWOFormState = WOFormState.CPOExistWO;
			voCPO = (MTR_CPOVO) pobjCPO;
			if (pdtwCPOs != null)
			{
				dtwCPOs = pdtwCPOs;
			}
			else pdtwCPOs = null;
			voWorkOrderMaster.WorkOrderMasterID = pintWOMasterID;
		}
		
		/// <summary>
		/// Convert CPO to existing WO
		/// </summary>
		private void FormLoadForExistWOForCPO()
		{
			const string METHOD_NAME = THIS + ".FormLoadForExistWOForCPO()";
			try
			{
				if (dtwCPOs == null || voWorkOrderMaster.WorkOrderMasterID == 0)
					return;
				cboCCN.SelectedValue = voCPO.CCNID;
				txtMasterLocation.Text = boWorkOrder.GetMasterLocByID(voCPO.MasterLocationID).Code;
				voMasLoc.MasterLocationID = voCPO.MasterLocationID;

				voWorkOrderMaster = (PRO_WorkOrderMasterVO) boWorkOrder.GetObjectWOMasterVO(voWorkOrderMaster.WorkOrderMasterID);
				txtDescription.Text = voWorkOrderMaster.Description;
				txtWONo.Text = voWorkOrderMaster.WorkOrderNo;

				if (voWorkOrderMaster.ProductionLineID >0)
				{
					DataRowView drvProLine = FormControlComponents.OpenSearchForm(PRO_ProductionLineTable.TABLE_NAME, PRO_ProductionLineTable.PRODUCTIONLINEID_FLD, voWorkOrderMaster.ProductionLineID.ToString(), null, false);
					if (drvProLine != null)
					{
						txtProductionLine.Text = drvProLine[PRO_ProductionLineTable.CODE_FLD].ToString();
						txtProductionLine.Tag = drvProLine[PRO_ProductionLineTable.PRODUCTIONLINEID_FLD];
					}
				}
				if (voWorkOrderMaster.DCOptionMasterID >0)
				{
					DataRowView drvProLine = FormControlComponents.OpenSearchForm(PRO_DCOptionMasterTable.TABLE_NAME, PRO_DCOptionMasterTable.DCOPTIONMASTERID_FLD, voWorkOrderMaster.DCOptionMasterID.ToString(), null, false);
					if (drvProLine != null)
					{
						txtDCPCycle.Text = drvProLine[PRO_DCOptionMasterTable.CYCLE_FLD].ToString();
						txtDCPCycle.Tag = drvProLine[PRO_DCOptionMasterTable.DCOPTIONMASTERID_FLD];
						txtDCPDescription.Text = drvProLine[PRO_DCOptionMasterTable.DESCRIPTION_FLD].ToString();
					}
				}
				dtmTransDate.Value = voWorkOrderMaster.TransDate;
				chkFinance.Checked = false;
				chkManufacture.Checked = false;
				dstGridData = boWorkOrder.GetWODetailByMaster(voWorkOrderMaster.WorkOrderMasterID);
				dstGridData.EnforceConstraints = false;
				for (int i =0; i < dtwCPOs.Count; i++)
				{
					DataRow drowWODetail = dstGridData.Tables[0].NewRow();
					drowWODetail[PRO_WorkOrderDetailTable.LINE_FLD] = int.Parse(dstGridData.Tables[0].Rows[dstGridData.Tables[0].Rows.Count - 1][PRO_WorkOrderDetailTable.LINE_FLD].ToString()) + 1;
					drowWODetail[ITM_ProductTable.PRODUCTID_FLD] = dtwCPOs[i][ITM_ProductTable.PRODUCTID_FLD];
					drowWODetail[ITM_ProductTable.STOCKUMID_FLD] = dtwCPOs[i][ITM_ProductTable.STOCKUMID_FLD];
					drowWODetail[ITM_ProductTable.CODE_FLD] = dtwCPOs[i][ITM_ProductTable.TABLE_NAME + ITM_ProductTable.CODE_FLD];
					drowWODetail[ITM_ProductTable.DESCRIPTION_FLD] = dtwCPOs[i][ITM_ProductTable.TABLE_NAME + ITM_ProductTable.DESCRIPTION_FLD];
					drowWODetail[ITM_ProductTable.REVISION_FLD] = dtwCPOs[i][ITM_ProductTable.TABLE_NAME + ITM_ProductTable.REVISION_FLD];
					drowWODetail[UM] = dtwCPOs[i][MST_UnitOfMeasureTable.TABLE_NAME + MST_UnitOfMeasureTable.CODE_FLD];
					drowWODetail[PRO_WorkOrderDetailTable.ORDERQUANTITY_FLD] = dtwCPOs[i][MTR_CPOTable.QUANTITY_FLD];
					drowWODetail[PRO_WorkOrderDetailTable.STARTDATE_FLD] = dtwCPOs[i][MTR_CPOTable.STARTDATE_FLD];
					drowWODetail[PRO_WorkOrderDetailTable.DUEDATE_FLD] = dtwCPOs[i][MTR_CPOTable.DUEDATE_FLD];
					drowWODetail[STATUS_STRING] = UNRELEASE;
					drowWODetail[PRO_WorkOrderDetailTable.STATUS_FLD] = WOLineStatus.Unreleased;
					drowWODetail[PRO_WorkOrderDetailTable.WORKORDERDETAILID_FLD] = DBNull.Value;
					dstGridData.Tables[0].Rows.Add(drowWODetail);
				}
				dgrdData.DataSource = dstGridData.Tables[0];
				for (int i = 0; i < dgrdData.RowCount; i++)
				{
					string strProductInfor;
					char chrSeparator = ';'; 
					strProductInfor = boWorkOrder.GetProductInforByID(int.Parse(dgrdData[i, PRO_WorkOrderDetailTable.PRODUCTID_FLD].ToString()));
					dgrdData[i, UM] = strProductInfor.Split(chrSeparator)[UM_CODE];
					dgrdData[i, ITM_ProductTable.COSTMETHOD_FLD] = strProductInfor.Split(chrSeparator)[COST_METHOD];
					dgrdData[i, AGC_CODE] = strProductInfor.Split(chrSeparator)[AGC_COD];
					dgrdData[i, PRO_WorkOrderDetailTable.STARTDATE_FLD] = new UtilsBO().GetDBDate();
					if (strProductInfor.Split(chrSeparator)[EST_CODE] != string.Empty)
					{
						dgrdData[i, PRO_WorkOrderDetailTable.ESTCST_FLD] = strProductInfor.Split(chrSeparator)[EST_CODE];
					}
					else
					{
						dgrdData[i, PRO_WorkOrderDetailTable.ESTCST_FLD] = DBNull.Value;
					}
					if (dgrdData[i, PRO_WorkOrderDetailTable.AGC_FLD].ToString() == string.Empty)
					{
						dgrdData[i, PRO_WorkOrderDetailTable.AGC_FLD] = DBNull.Value;
					}
					if (dgrdData[i, PRO_WorkOrderDetailTable.WORKORDERDETAILID_FLD].ToString() == string.Empty)
					{
						dgrdData[i, STATUS_STRING] = UNRELEASE;
						dgrdData[i, PRO_WorkOrderDetailTable.STATUS_FLD] = (int) WOLineStatus.Unreleased;
					}
				}
				//restore the layout of grid
				FormControlComponents.RestoreGridLayout(dgrdData, dtbGridLayOut);
				ConfigGrid(false);
				
				//enable and disbale button
				btnAdd.Enabled = false;
				if (CheckWOLines(WOLineStatus.MfgClose))
				{
					chkManufacture.Checked = true;
				}
				else
				{
					if (CheckWOLines(WOLineStatus.FinClose))
					{
						chkFinance.Checked = true;
					}
				}
				mFormMode = EnumAction.Edit;
				SwitchFormMode();
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
		/// - Fill data into item code, item description, item revision, stock unit of measure 
		/// - Fill data into start/due date time based on current server date time
		/// - Fill data into AGC, cost method, estimated cost based on product item setup
		/// </summary>
		private void FillItemData(DataRow pdrowData)
		{			
			int i = dgrdData.Row;
            
            dgrdData.EditActive = true;
            dgrdData[i, PRO_WorkOrderDetailTable.LINE_FLD] = i + 1;
            dgrdData[i, ITM_ProductTable.CODE_FLD] = pdrowData[ITM_ProductTable.CODE_FLD].ToString();
            dgrdData[i, ITM_ProductTable.DESCRIPTION_FLD] = pdrowData[ITM_ProductTable.DESCRIPTION_FLD].ToString();
            dgrdData[i, ITM_ProductTable.REVISION_FLD] = pdrowData[ITM_ProductTable.REVISION_FLD].ToString();
            dgrdData[i, ITM_CategoryTable.TABLE_NAME + ITM_CategoryTable.CODE_FLD] = pdrowData["Category"];
            dgrdData[i, PRO_WorkOrderDetailTable.PRODUCTID_FLD] = pdrowData[ITM_ProductTable.PRODUCTID_FLD].ToString();
            dgrdData[i, PRO_WorkOrderDetailTable.STOCKUMID_FLD] = pdrowData[ITM_ProductTable.STOCKUMID_FLD].ToString();
            dgrdData[i, PRO_WorkOrderDetailTable.AGC_FLD] = pdrowData[ITM_ProductTable.AGCID_FLD].ToString();
            var unit = Utilities.Instance.GetUnitOfMeasure(Convert.ToInt32(pdrowData[ITM_ProductTable.STOCKUMID_FLD]));
            if (unit != null)
            {
                dgrdData[i, UM] = unit.Code;
            }
            dgrdData[i, PRO_WorkOrderDetailTable.STARTDATE_FLD] = Utilities.Instance.GetServerDate();
            if (dgrdData[i, PRO_WorkOrderDetailTable.WORKORDERDETAILID_FLD].ToString() == string.Empty)
            {
                dgrdData[i, STATUS_STRING] = UNRELEASE;
                dgrdData[i, PRO_WorkOrderDetailTable.STATUS_FLD] = (int)WOLineStatus.Unreleased;
            }
            dgrdData.Col = dgrdData.Splits[0].DisplayColumns.IndexOf(dgrdData.Splits[0].DisplayColumns[ITM_ProductTable.CODE_FLD]);
            dgrdData.Focus();
		}

		/// <summary>
		/// check all work order line in grid, 
		/// if all work order line have status is unrelease
		/// 	allow delete
		/// else
		/// {
		/// display error message that user cannot delete because all line is not unrelease
		/// return;
		/// }
		/// </summary>
		private bool CheckWOLines(WOLineStatus pStatusToCheck)
		{
			int intCountStatus = 0, intCountLineDetail = 0;
			for (int i = 0; i < dgrdData.RowCount; i++)
			{
				if (dgrdData[i, PRO_WorkOrderDetailTable.WORKORDERDETAILID_FLD].ToString() != string.Empty)
				{
					intCountLineDetail++;
					if (pStatusToCheck == WOLineStatus.FinClose)
					{
						if (int.Parse(dgrdData[i, PRO_WorkOrderDetailTable.STATUS_FLD].ToString()) == (int) pStatusToCheck)
						{
							intCountStatus++;
						}
					}
					if (pStatusToCheck == WOLineStatus.MfgClose)
					{
						if (int.Parse(dgrdData[i, PRO_WorkOrderDetailTable.STATUS_FLD].ToString()) == (int) pStatusToCheck
							|| int.Parse(dgrdData[i, PRO_WorkOrderDetailTable.STATUS_FLD].ToString()) == (int) WOLineStatus.FinClose)
						{
							intCountStatus++;
						}
					}
					if (pStatusToCheck == WOLineStatus.Unreleased)
					{
						if (int.Parse(dgrdData[i, PRO_WorkOrderDetailTable.STATUS_FLD].ToString()) == (int) WOLineStatus.Unreleased)
						{
							intCountStatus++;
						}
					}
				}
			}
			return intCountStatus == intCountLineDetail;
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(WorkOrder));
			this.dgrdData = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
			this.btnSearchWO = new System.Windows.Forms.Button();
			this.chkManufacture = new System.Windows.Forms.CheckBox();
			this.chkFinance = new System.Windows.Forms.CheckBox();
			this.txtDescription = new System.Windows.Forms.TextBox();
			this.lblDescription = new System.Windows.Forms.Label();
			this.txtWONo = new System.Windows.Forms.TextBox();
			this.lblWO_No = new System.Windows.Forms.Label();
			this.lblMasLoc = new System.Windows.Forms.Label();
			this.lblCCN = new System.Windows.Forms.Label();
			this.btnEdit = new System.Windows.Forms.Button();
			this.btnClose = new System.Windows.Forms.Button();
			this.btnHelp = new System.Windows.Forms.Button();
			this.btnDelete = new System.Windows.Forms.Button();
			this.btnSave = new System.Windows.Forms.Button();
			this.btnAdd = new System.Windows.Forms.Button();
			this.btnPrint = new System.Windows.Forms.Button();
			this.lblTransDate = new System.Windows.Forms.Label();
			this.btnRouting = new System.Windows.Forms.Button();
			this.btnBOM = new System.Windows.Forms.Button();
			this.btnCost = new System.Windows.Forms.Button();
			this.txtMasterLocation = new System.Windows.Forms.TextBox();
			this.btnSearchMasLoc = new System.Windows.Forms.Button();
			this.dtmTransDate = new C1.Win.C1Input.C1DateEdit();
			this.cboCCN = new C1.Win.C1List.C1Combo();
			this.dtmDate = new C1.Win.C1Input.C1DateEdit();
			this.txtProductionLine = new System.Windows.Forms.TextBox();
			this.lblProductionLine = new System.Windows.Forms.Label();
			this.btnProductionLine = new System.Windows.Forms.Button();
			this.lblWorkOrderReport = new System.Windows.Forms.Label();
			this.txtDCPCycle = new System.Windows.Forms.TextBox();
			this.lblDCPCycle = new System.Windows.Forms.Label();
			this.btnDCPCycle = new System.Windows.Forms.Button();
			this.txtDCPDescription = new System.Windows.Forms.TextBox();
			((System.ComponentModel.ISupportInitialize)(this.dgrdData)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dtmTransDate)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.cboCCN)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dtmDate)).BeginInit();
			this.SuspendLayout();
			// 
			// dgrdData
			// 
			this.dgrdData.AllowAddNew = true;
			this.dgrdData.AllowDelete = true;
			this.dgrdData.AllowSort = false;
			this.dgrdData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.dgrdData.CaptionHeight = 17;
			this.dgrdData.CollapseColor = System.Drawing.Color.Black;
			this.dgrdData.ExpandColor = System.Drawing.Color.Black;
			this.dgrdData.FlatStyle = C1.Win.C1TrueDBGrid.FlatModeEnum.System;
			this.dgrdData.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.dgrdData.GroupByCaption = "Drag a column header here to group by that column";
			this.dgrdData.Images.Add(((System.Drawing.Image)(resources.GetObject("resource"))));
			this.dgrdData.Location = new System.Drawing.Point(3, 136);
			this.dgrdData.MarqueeStyle = C1.Win.C1TrueDBGrid.MarqueeEnum.DottedCellBorder;
			this.dgrdData.Name = "dgrdData";
			this.dgrdData.PreviewInfo.Location = new System.Drawing.Point(0, 0);
			this.dgrdData.PreviewInfo.Size = new System.Drawing.Size(0, 0);
			this.dgrdData.PreviewInfo.ZoomFactor = 75;
			this.dgrdData.PrintInfo.ShowOptionsDialog = false;
			this.dgrdData.RecordSelectorWidth = 16;
			this.dgrdData.RowDivider.Color = System.Drawing.Color.DarkGray;
			this.dgrdData.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.Single;
			this.dgrdData.RowHeight = 15;
			this.dgrdData.RowSubDividerColor = System.Drawing.Color.DarkGray;
			this.dgrdData.Size = new System.Drawing.Size(627, 326);
			this.dgrdData.TabIndex = 21;
			this.dgrdData.AfterDelete += new System.EventHandler(this.dgrdData_AfterDelete);
			this.dgrdData.RowColChange += new C1.Win.C1TrueDBGrid.RowColChangeEventHandler(this.dgrdData_RowColChange);
			this.dgrdData.BeforeColEdit += new C1.Win.C1TrueDBGrid.BeforeColEditEventHandler(this.dgrdData_BeforeColEdit);
			this.dgrdData.ButtonClick += new C1.Win.C1TrueDBGrid.ColEventHandler(this.dgrdData_ButtonClick);
			this.dgrdData.AfterColUpdate += new C1.Win.C1TrueDBGrid.ColEventHandler(this.dgrdData_AfterColUpdate);
			this.dgrdData.BeforeColUpdate += new C1.Win.C1TrueDBGrid.BeforeColUpdateEventHandler(this.dgrdData_BeforeColUpdate);
			this.dgrdData.BeforeDelete += new C1.Win.C1TrueDBGrid.CancelEventHandler(this.dgrdData_BeforeDelete);
			this.dgrdData.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgrdData_KeyDown);
			this.dgrdData.Enter += new System.EventHandler(this.dgrdData_Enter);
			this.dgrdData.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"Line\" DataF" +
				"ield=\"Line\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Ca" +
				"ption=\"Part Number\" DataField=\"Code\"><ValueItems /><GroupInfo /></C1DataColumn><" +
				"C1DataColumn Level=\"0\" Caption=\"Model\" DataField=\"Revision\"><ValueItems /><Group" +
				"Info /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Part Name\" DataField=\"Des" +
				"cription\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Capt" +
				"ion=\"Quantity\" DataField=\"OrderQuantity\"><ValueItems /><GroupInfo /></C1DataColu" +
				"mn><C1DataColumn Level=\"0\" Caption=\"Status\" DataField=\"StatusString\"><ValueItems" +
				" /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Due Date, Time\" " +
				"DataField=\"DueDate\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Leve" +
				"l=\"0\" Caption=\"UM\" DataField=\"UM\"><ValueItems /><GroupInfo /></C1DataColumn><C1D" +
				"ataColumn Level=\"0\" Caption=\"Sale Order\" DataField=\"SaleOrderCode\"><ValueItems /" +
				"><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Sale Order Line\" D" +
				"ataField=\"SaleOrderLine\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn" +
				" Level=\"0\" Caption=\"AGC\" DataField=\"AGCCode\"><ValueItems /><GroupInfo /></C1Data" +
				"Column><C1DataColumn Level=\"0\" Caption=\"Cst Mthd\" DataField=\"CostMethod\"><ValueI" +
				"tems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Est Cst\" Dat" +
				"aField=\"EstCst\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0" +
				"\" Caption=\"Start Date, Time\" DataField=\"StartDate\"><ValueItems /><GroupInfo /></" +
				"C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Category\" DataField=\"ITM_CategoryC" +
				"ode\"><ValueItems /><GroupInfo /></C1DataColumn></DataCols><Styles type=\"C1.Win.C" +
				"1TrueDBGrid.Design.ContextWrapper\"><Data>HighlightRow{ForeColor:HighlightText;Ba" +
				"ckColor:Highlight;}Inactive{ForeColor:InactiveCaptionText;BackColor:InactiveCapt" +
				"ion;}Style78{}Style79{}Style85{}Editor{}Style72{}Style73{}Style70{AlignHorz:Cent" +
				"er;}Style71{AlignHorz:Near;}Style76{AlignHorz:Center;}Style77{AlignHorz:Near;}St" +
				"yle74{}Style75{}Style84{}Style87{}Style86{}Style81{}Style80{}Style83{AlignHorz:N" +
				"ear;}Style82{AlignHorz:Center;}Style89{AlignHorz:Near;}Style88{AlignHorz:Center;" +
				"}Style104{}Style105{}Style100{AlignHorz:Center;ForeColor:Maroon;}Style101{AlignH" +
				"orz:Near;}Style102{}Style103{AlignHorz:Center;}Style94{AlignHorz:Center;}Style95" +
				"{AlignHorz:Near;}Style96{}Style97{}Style90{}Style91{}Style92{}Style93{}Style98{}" +
				"Style99{}Heading{Wrap:True;AlignVert:Center;Border:Raised,,1, 1, 1, 1;ForeColor:" +
				"ControlText;BackColor:Control;}FilterBar{}Style18{}Style19{}Style14{}Style15{}St" +
				"yle16{AlignHorz:Center;ForeColor:Maroon;}Style17{AlignHorz:Near;}Style10{AlignHo" +
				"rz:Near;}Style11{}Style12{}Style13{}Selected{ForeColor:HighlightText;BackColor:H" +
				"ighlight;}Style29{AlignHorz:Near;}Style22{AlignHorz:Center;ForeColor:Maroon;}Sty" +
				"le27{}Style28{AlignHorz:Center;}Style9{}Style8{}Style24{}Style26{}Style5{}Style4" +
				"{}Style7{}Style6{}Style25{}Style23{AlignHorz:Near;}Style3{}Style2{}Style21{Align" +
				"Horz:General;}Style20{}OddRow{}Style38{}Style39{}Style36{}Style37{}Style34{Align" +
				"Horz:Center;ForeColor:Maroon;}Style35{AlignHorz:Near;}Style32{}Style33{}Style30{" +
				"}Style49{}Style48{}Style31{}Normal{Font:Microsoft Sans Serif, 8.25pt;}Style41{Al" +
				"ignHorz:Near;}Style40{AlignHorz:Center;ForeColor:Maroon;}Style43{AlignHorz:Far;}" +
				"Style42{}Style45{}Style44{}Style47{AlignHorz:Center;}Style46{AlignHorz:Center;}E" +
				"venRow{BackColor:Aqua;}Style59{AlignHorz:Near;}Style58{AlignHorz:Center;ForeColo" +
				"r:Maroon;}RecordSelector{AlignImage:Center;}Style51{}Style50{}Footer{}Style52{Al" +
				"ignHorz:Center;}Style53{AlignHorz:Near;}Style54{}Style55{}Style56{}Style57{}Capt" +
				"ion{AlignHorz:Center;}Style69{}Style68{}Style1{}Style63{}Style62{}Style61{AlignH" +
				"orz:Center;}Style60{}Style67{}Style66{}Style65{AlignHorz:Center;}Style64{AlignHo" +
				"rz:Center;}Group{BackColor:ControlDark;Border:None,,0, 0, 0, 0;AlignVert:Center;" +
				"}</Data></Styles><Splits><C1.Win.C1TrueDBGrid.MergeView Name=\"\" CaptionHeight=\"1" +
				"7\" ColumnCaptionHeight=\"17\" ColumnFooterHeight=\"17\" MarqueeStyle=\"DottedCellBord" +
				"er\" RecordSelectorWidth=\"16\" DefRecSelWidth=\"16\" VerticalScrollGroup=\"1\" Horizon" +
				"talScrollGroup=\"1\"><ClientRect>0, 0, 623, 322</ClientRect><BorderSide>0</BorderS" +
				"ide><CaptionStyle parent=\"Style2\" me=\"Style10\" /><EditorStyle parent=\"Editor\" me" +
				"=\"Style5\" /><EvenRowStyle parent=\"EvenRow\" me=\"Style8\" /><FilterBarStyle parent=" +
				"\"FilterBar\" me=\"Style13\" /><FooterStyle parent=\"Footer\" me=\"Style3\" /><GroupStyl" +
				"e parent=\"Group\" me=\"Style12\" /><HeadingStyle parent=\"Heading\" me=\"Style2\" /><Hi" +
				"ghLightRowStyle parent=\"HighlightRow\" me=\"Style7\" /><InactiveStyle parent=\"Inact" +
				"ive\" me=\"Style4\" /><OddRowStyle parent=\"OddRow\" me=\"Style9\" /><RecordSelectorSty" +
				"le parent=\"RecordSelector\" me=\"Style11\" /><SelectedStyle parent=\"Selected\" me=\"S" +
				"tyle6\" /><Style parent=\"Normal\" me=\"Style1\" /><internalCols><C1DisplayColumn><He" +
				"adingStyle parent=\"Style2\" me=\"Style16\" /><Style parent=\"Style1\" me=\"Style17\" />" +
				"<FooterStyle parent=\"Style3\" me=\"Style18\" /><EditorStyle parent=\"Style5\" me=\"Sty" +
				"le19\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style21\" /><GroupFooterStyle paren" +
				"t=\"Style1\" me=\"Style20\" /><Visible>True</Visible><ColumnDivider>DarkGray,Single<" +
				"/ColumnDivider><Width>39</Width><Height>15</Height><DCIdx>0</DCIdx></C1DisplayCo" +
				"lumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style22\" /><Style parent" +
				"=\"Style1\" me=\"Style23\" /><FooterStyle parent=\"Style3\" me=\"Style24\" /><EditorStyl" +
				"e parent=\"Style5\" me=\"Style25\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style27\" " +
				"/><GroupFooterStyle parent=\"Style1\" me=\"Style26\" /><Visible>True</Visible><Colum" +
				"nDivider>DarkGray,Single</ColumnDivider><Width>150</Width><Height>15</Height><Bu" +
				"tton>True</Button><DCIdx>1</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingSty" +
				"le parent=\"Style2\" me=\"Style34\" /><Style parent=\"Style1\" me=\"Style35\" /><FooterS" +
				"tyle parent=\"Style3\" me=\"Style36\" /><EditorStyle parent=\"Style5\" me=\"Style37\" />" +
				"<GroupHeaderStyle parent=\"Style1\" me=\"Style39\" /><GroupFooterStyle parent=\"Style" +
				"1\" me=\"Style38\" /><Visible>True</Visible><ColumnDivider>DarkGray,Single</ColumnD" +
				"ivider><Width>168</Width><Height>15</Height><DCIdx>3</DCIdx></C1DisplayColumn><C" +
				"1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style28\" /><Style parent=\"Style" +
				"1\" me=\"Style29\" /><FooterStyle parent=\"Style3\" me=\"Style30\" /><EditorStyle paren" +
				"t=\"Style5\" me=\"Style31\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style33\" /><Grou" +
				"pFooterStyle parent=\"Style1\" me=\"Style32\" /><Visible>True</Visible><ColumnDivide" +
				"r>DarkGray,Single</ColumnDivider><Width>90</Width><Height>15</Height><DCIdx>2</D" +
				"CIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style5" +
				"2\" /><Style parent=\"Style1\" me=\"Style53\" /><FooterStyle parent=\"Style3\" me=\"Styl" +
				"e54\" /><EditorStyle parent=\"Style5\" me=\"Style55\" /><GroupHeaderStyle parent=\"Sty" +
				"le1\" me=\"Style57\" /><GroupFooterStyle parent=\"Style1\" me=\"Style56\" /><Visible>Tr" +
				"ue</Visible><ColumnDivider>DarkGray,Single</ColumnDivider><Width>52</Width><Heig" +
				"ht>15</Height><DCIdx>14</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle " +
				"parent=\"Style2\" me=\"Style64\" /><Style parent=\"Style1\" me=\"Style65\" /><FooterStyl" +
				"e parent=\"Style3\" me=\"Style66\" /><EditorStyle parent=\"Style5\" me=\"Style67\" /><Gr" +
				"oupHeaderStyle parent=\"Style1\" me=\"Style69\" /><GroupFooterStyle parent=\"Style1\" " +
				"me=\"Style68\" /><Visible>True</Visible><ColumnDivider>DarkGray,Single</ColumnDivi" +
				"der><Width>45</Width><Height>15</Height><DCIdx>7</DCIdx></C1DisplayColumn><C1Dis" +
				"playColumn><HeadingStyle parent=\"Style2\" me=\"Style40\" /><Style parent=\"Style1\" m" +
				"e=\"Style41\" /><FooterStyle parent=\"Style3\" me=\"Style42\" /><EditorStyle parent=\"S" +
				"tyle5\" me=\"Style43\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style45\" /><GroupFoo" +
				"terStyle parent=\"Style1\" me=\"Style44\" /><Visible>True</Visible><ColumnDivider>Da" +
				"rkGray,Single</ColumnDivider><Width>75</Width><Height>15</Height><DCIdx>4</DCIdx" +
				"></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style46\" /" +
				"><Style parent=\"Style1\" me=\"Style47\" /><FooterStyle parent=\"Style3\" me=\"Style48\"" +
				" /><EditorStyle parent=\"Style5\" me=\"Style49\" /><GroupHeaderStyle parent=\"Style1\"" +
				" me=\"Style51\" /><GroupFooterStyle parent=\"Style1\" me=\"Style50\" /><Visible>True</" +
				"Visible><ColumnDivider>DarkGray,Single</ColumnDivider><Width>66</Width><Height>1" +
				"5</Height><DCIdx>5</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle paren" +
				"t=\"Style2\" me=\"Style100\" /><Style parent=\"Style1\" me=\"Style101\" /><FooterStyle p" +
				"arent=\"Style3\" me=\"Style102\" /><EditorStyle parent=\"Style5\" me=\"Style103\" /><Gro" +
				"upHeaderStyle parent=\"Style1\" me=\"Style105\" /><GroupFooterStyle parent=\"Style1\" " +
				"me=\"Style104\" /><Visible>True</Visible><ColumnDivider>DarkGray,Single</ColumnDiv" +
				"ider><Width>114</Width><Height>15</Height><DCIdx>13</DCIdx></C1DisplayColumn><C1" +
				"DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style58\" /><Style parent=\"Style1" +
				"\" me=\"Style59\" /><FooterStyle parent=\"Style3\" me=\"Style60\" /><EditorStyle parent" +
				"=\"Style5\" me=\"Style61\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style63\" /><Group" +
				"FooterStyle parent=\"Style1\" me=\"Style62\" /><Visible>True</Visible><ColumnDivider" +
				">DarkGray,Single</ColumnDivider><Width>111</Width><Height>15</Height><DCIdx>6</D" +
				"CIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style7" +
				"0\" /><Style parent=\"Style1\" me=\"Style71\" /><FooterStyle parent=\"Style3\" me=\"Styl" +
				"e72\" /><EditorStyle parent=\"Style5\" me=\"Style73\" /><GroupHeaderStyle parent=\"Sty" +
				"le1\" me=\"Style75\" /><GroupFooterStyle parent=\"Style1\" me=\"Style74\" /><Visible>Tr" +
				"ue</Visible><ColumnDivider>DarkGray,Single</ColumnDivider><Width>119</Width><Hei" +
				"ght>15</Height><Button>True</Button><DCIdx>8</DCIdx></C1DisplayColumn><C1Display" +
				"Column><HeadingStyle parent=\"Style2\" me=\"Style76\" /><Style parent=\"Style1\" me=\"S" +
				"tyle77\" /><FooterStyle parent=\"Style3\" me=\"Style78\" /><EditorStyle parent=\"Style" +
				"5\" me=\"Style79\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style81\" /><GroupFooterS" +
				"tyle parent=\"Style1\" me=\"Style80\" /><Visible>True</Visible><ColumnDivider>DarkGr" +
				"ay,Single</ColumnDivider><Width>92</Width><Height>15</Height><Button>True</Butto" +
				"n><DCIdx>9</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style" +
				"2\" me=\"Style88\" /><Style parent=\"Style1\" me=\"Style89\" /><FooterStyle parent=\"Sty" +
				"le3\" me=\"Style90\" /><EditorStyle parent=\"Style5\" me=\"Style91\" /><GroupHeaderStyl" +
				"e parent=\"Style1\" me=\"Style93\" /><GroupFooterStyle parent=\"Style1\" me=\"Style92\" " +
				"/><Visible>True</Visible><ColumnDivider>DarkGray,Single</ColumnDivider><Width>10" +
				"1</Width><Height>15</Height><DCIdx>11</DCIdx></C1DisplayColumn><C1DisplayColumn>" +
				"<HeadingStyle parent=\"Style2\" me=\"Style94\" /><Style parent=\"Style1\" me=\"Style95\"" +
				" /><FooterStyle parent=\"Style3\" me=\"Style96\" /><EditorStyle parent=\"Style5\" me=\"" +
				"Style97\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style99\" /><GroupFooterStyle pa" +
				"rent=\"Style1\" me=\"Style98\" /><Visible>True</Visible><ColumnDivider>DarkGray,Sing" +
				"le</ColumnDivider><Height>15</Height><DCIdx>12</DCIdx></C1DisplayColumn><C1Displ" +
				"ayColumn><HeadingStyle parent=\"Style2\" me=\"Style82\" /><Style parent=\"Style1\" me=" +
				"\"Style83\" /><FooterStyle parent=\"Style3\" me=\"Style84\" /><EditorStyle parent=\"Sty" +
				"le5\" me=\"Style85\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style87\" /><GroupFoote" +
				"rStyle parent=\"Style1\" me=\"Style86\" /><Visible>True</Visible><ColumnDivider>Dark" +
				"Gray,Single</ColumnDivider><Width>50</Width><Height>15</Height><DCIdx>10</DCIdx>" +
				"</C1DisplayColumn></internalCols></C1.Win.C1TrueDBGrid.MergeView></Splits><Named" +
				"Styles><Style parent=\"\" me=\"Normal\" /><Style parent=\"Normal\" me=\"Heading\" /><Sty" +
				"le parent=\"Heading\" me=\"Footer\" /><Style parent=\"Heading\" me=\"Caption\" /><Style " +
				"parent=\"Heading\" me=\"Inactive\" /><Style parent=\"Normal\" me=\"Selected\" /><Style p" +
				"arent=\"Normal\" me=\"Editor\" /><Style parent=\"Normal\" me=\"HighlightRow\" /><Style p" +
				"arent=\"Normal\" me=\"EvenRow\" /><Style parent=\"Normal\" me=\"OddRow\" /><Style parent" +
				"=\"Heading\" me=\"RecordSelector\" /><Style parent=\"Normal\" me=\"FilterBar\" /><Style " +
				"parent=\"Caption\" me=\"Group\" /></NamedStyles><vertSplits>1</vertSplits><horzSplit" +
				"s>1</horzSplits><Layout>Modified</Layout><DefaultRecSelWidth>16</DefaultRecSelWi" +
				"dth><ClientArea>0, 0, 623, 322</ClientArea><PrintPageHeaderStyle parent=\"\" me=\"S" +
				"tyle14\" /><PrintPageFooterStyle parent=\"\" me=\"Style15\" /></Blob>";
			// 
			// btnSearchWO
			// 
			this.btnSearchWO.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnSearchWO.Location = new System.Drawing.Point(208, 48);
			this.btnSearchWO.Name = "btnSearchWO";
			this.btnSearchWO.Size = new System.Drawing.Size(24, 20);
			this.btnSearchWO.TabIndex = 9;
			this.btnSearchWO.Text = "...";
			this.btnSearchWO.Click += new System.EventHandler(this.btnSearchWO_Click);
			// 
			// chkManufacture
			// 
			this.chkManufacture.Enabled = false;
			this.chkManufacture.Location = new System.Drawing.Point(182, 114);
			this.chkManufacture.Name = "chkManufacture";
			this.chkManufacture.Size = new System.Drawing.Size(127, 20);
			this.chkManufacture.TabIndex = 20;
			this.chkManufacture.Text = "Manufacturing Close";
			// 
			// chkFinance
			// 
			this.chkFinance.Enabled = false;
			this.chkFinance.Location = new System.Drawing.Point(83, 114);
			this.chkFinance.Name = "chkFinance";
			this.chkFinance.Size = new System.Drawing.Size(96, 20);
			this.chkFinance.TabIndex = 19;
			this.chkFinance.Text = "Finance Close";
			// 
			// txtDescription
			// 
			this.txtDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.txtDescription.Location = new System.Drawing.Point(83, 92);
			this.txtDescription.MaxLength = 200;
			this.txtDescription.Name = "txtDescription";
			this.txtDescription.Size = new System.Drawing.Size(547, 20);
			this.txtDescription.TabIndex = 18;
			this.txtDescription.Text = "Description";
			this.txtDescription.Leave += new System.EventHandler(this.txtDescription_Leave);
			this.txtDescription.Enter += new System.EventHandler(this.OnEnterControl);
			// 
			// lblDescription
			// 
			this.lblDescription.Location = new System.Drawing.Point(6, 92);
			this.lblDescription.Name = "lblDescription";
			this.lblDescription.Size = new System.Drawing.Size(75, 20);
			this.lblDescription.TabIndex = 17;
			this.lblDescription.Text = "Description";
			this.lblDescription.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// txtWONo
			// 
			this.txtWONo.Location = new System.Drawing.Point(83, 48);
			this.txtWONo.MaxLength = 24;
			this.txtWONo.Name = "txtWONo";
			this.txtWONo.Size = new System.Drawing.Size(123, 20);
			this.txtWONo.TabIndex = 8;
			this.txtWONo.Text = "";
			this.txtWONo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtWONo_KeyDown);
			this.txtWONo.Validating += new System.ComponentModel.CancelEventHandler(this.txtWONo_Validating);
			this.txtWONo.Leave += new System.EventHandler(this.txtWONo_Leave);
			this.txtWONo.Enter += new System.EventHandler(this.OnEnterControl);
			// 
			// lblWO_No
			// 
			this.lblWO_No.ForeColor = System.Drawing.Color.Maroon;
			this.lblWO_No.Location = new System.Drawing.Point(6, 48);
			this.lblWO_No.Name = "lblWO_No";
			this.lblWO_No.Size = new System.Drawing.Size(75, 20);
			this.lblWO_No.TabIndex = 7;
			this.lblWO_No.Text = "WO No.";
			this.lblWO_No.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblMasLoc
			// 
			this.lblMasLoc.ForeColor = System.Drawing.Color.Maroon;
			this.lblMasLoc.Location = new System.Drawing.Point(6, 26);
			this.lblMasLoc.Name = "lblMasLoc";
			this.lblMasLoc.Size = new System.Drawing.Size(75, 20);
			this.lblMasLoc.TabIndex = 4;
			this.lblMasLoc.Text = "Mas. Location";
			this.lblMasLoc.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblCCN
			// 
			this.lblCCN.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.lblCCN.ForeColor = System.Drawing.Color.Maroon;
			this.lblCCN.Location = new System.Drawing.Point(516, 4);
			this.lblCCN.Name = "lblCCN";
			this.lblCCN.Size = new System.Drawing.Size(34, 20);
			this.lblCCN.TabIndex = 0;
			this.lblCCN.Text = "CCN";
			this.lblCCN.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// btnEdit
			// 
			this.btnEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnEdit.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnEdit.Location = new System.Drawing.Point(126, 468);
			this.btnEdit.Name = "btnEdit";
			this.btnEdit.Size = new System.Drawing.Size(60, 23);
			this.btnEdit.TabIndex = 24;
			this.btnEdit.Text = "&Edit";
			this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
			// 
			// btnClose
			// 
			this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnClose.Location = new System.Drawing.Point(569, 468);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(60, 23);
			this.btnClose.TabIndex = 31;
			this.btnClose.Text = "&Close";
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			// 
			// btnHelp
			// 
			this.btnHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnHelp.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnHelp.Location = new System.Drawing.Point(508, 468);
			this.btnHelp.Name = "btnHelp";
			this.btnHelp.Size = new System.Drawing.Size(60, 23);
			this.btnHelp.TabIndex = 30;
			this.btnHelp.Text = "&Help";
			// 
			// btnDelete
			// 
			this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnDelete.Location = new System.Drawing.Point(187, 468);
			this.btnDelete.Name = "btnDelete";
			this.btnDelete.Size = new System.Drawing.Size(60, 23);
			this.btnDelete.TabIndex = 25;
			this.btnDelete.Text = "&Delete";
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
			// 
			// btnSave
			// 
			this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnSave.Location = new System.Drawing.Point(65, 468);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(60, 23);
			this.btnSave.TabIndex = 23;
			this.btnSave.Text = "&Save";
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			// 
			// btnAdd
			// 
			this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnAdd.Location = new System.Drawing.Point(4, 468);
			this.btnAdd.Name = "btnAdd";
			this.btnAdd.Size = new System.Drawing.Size(60, 23);
			this.btnAdd.TabIndex = 22;
			this.btnAdd.Text = "&Add";
			this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
			// 
			// btnPrint
			// 
			this.btnPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnPrint.Location = new System.Drawing.Point(248, 468);
			this.btnPrint.Name = "btnPrint";
			this.btnPrint.Size = new System.Drawing.Size(60, 23);
			this.btnPrint.TabIndex = 26;
			this.btnPrint.Text = "&Print";
			this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
			// 
			// lblTransDate
			// 
			this.lblTransDate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.lblTransDate.ForeColor = System.Drawing.Color.Maroon;
			this.lblTransDate.Location = new System.Drawing.Point(6, 4);
			this.lblTransDate.Name = "lblTransDate";
			this.lblTransDate.Size = new System.Drawing.Size(75, 20);
			this.lblTransDate.TabIndex = 2;
			this.lblTransDate.Text = "Trans. Date";
			this.lblTransDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// btnRouting
			// 
			this.btnRouting.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnRouting.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnRouting.Location = new System.Drawing.Point(386, 468);
			this.btnRouting.Name = "btnRouting";
			this.btnRouting.Size = new System.Drawing.Size(60, 23);
			this.btnRouting.TabIndex = 28;
			this.btnRouting.Text = "&Routing";
			this.btnRouting.Click += new System.EventHandler(this.btnRouting_Click);
			// 
			// btnBOM
			// 
			this.btnBOM.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnBOM.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnBOM.Location = new System.Drawing.Point(325, 468);
			this.btnBOM.Name = "btnBOM";
			this.btnBOM.Size = new System.Drawing.Size(60, 23);
			this.btnBOM.TabIndex = 27;
			this.btnBOM.Text = "&BOM";
			this.btnBOM.Click += new System.EventHandler(this.btnBOM_Click);
			// 
			// btnCost
			// 
			this.btnCost.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCost.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnCost.Location = new System.Drawing.Point(447, 468);
			this.btnCost.Name = "btnCost";
			this.btnCost.Size = new System.Drawing.Size(60, 23);
			this.btnCost.TabIndex = 29;
			this.btnCost.Text = "Cos&t";
			this.btnCost.Click += new System.EventHandler(this.btnCost_Click);
			// 
			// txtMasterLocation
			// 
			this.txtMasterLocation.Location = new System.Drawing.Point(83, 26);
			this.txtMasterLocation.MaxLength = 24;
			this.txtMasterLocation.Name = "txtMasterLocation";
			this.txtMasterLocation.Size = new System.Drawing.Size(98, 20);
			this.txtMasterLocation.TabIndex = 5;
			this.txtMasterLocation.Text = "";
			this.txtMasterLocation.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtMasterLocation_KeyDown);
			this.txtMasterLocation.Validating += new System.ComponentModel.CancelEventHandler(this.txtMasterLocation_Validating);
			// 
			// btnSearchMasLoc
			// 
			this.btnSearchMasLoc.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnSearchMasLoc.Location = new System.Drawing.Point(182, 26);
			this.btnSearchMasLoc.Name = "btnSearchMasLoc";
			this.btnSearchMasLoc.Size = new System.Drawing.Size(24, 20);
			this.btnSearchMasLoc.TabIndex = 6;
			this.btnSearchMasLoc.Text = "...";
			this.btnSearchMasLoc.Click += new System.EventHandler(this.btnSearchMasLoc_Click);
			// 
			// dtmTransDate
			// 
			this.dtmTransDate.CustomFormat = "dd-MM-yyyy";
			this.dtmTransDate.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
			this.dtmTransDate.Location = new System.Drawing.Point(83, 4);
			this.dtmTransDate.MaxLength = 24;
			this.dtmTransDate.Name = "dtmTransDate";
			this.dtmTransDate.Size = new System.Drawing.Size(99, 20);
			this.dtmTransDate.TabIndex = 3;
			this.dtmTransDate.Tag = null;
			this.dtmTransDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.dtmTransDate.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.DropDown;
			this.dtmTransDate.Enter += new System.EventHandler(this.OnEnterControl);
			this.dtmTransDate.DropDownClosed += new DropDownClosedEventHandler(this.dtmTransDate_DropDownClosed);
			this.dtmTransDate.Leave += new System.EventHandler(this.OnLeaveControl);
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
			this.cboCCN.EditorFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.cboCCN.EditorForeColor = System.Drawing.SystemColors.WindowText;
			this.cboCCN.EditorHeight = 15;
			this.cboCCN.FlatStyle = C1.Win.C1List.FlatModeEnum.System;
			this.cboCCN.GapHeight = 2;
			this.cboCCN.ItemHeight = 15;
			this.cboCCN.Location = new System.Drawing.Point(552, 4);
			this.cboCCN.MatchEntryTimeout = ((long)(2000));
			this.cboCCN.MaxDropDownItems = ((short)(5));
			this.cboCCN.MaxLength = 32767;
			this.cboCCN.MouseCursor = System.Windows.Forms.Cursors.Default;
			this.cboCCN.Name = "cboCCN";
			this.cboCCN.RowDivider.Color = System.Drawing.Color.DarkGray;
			this.cboCCN.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None;
			this.cboCCN.RowSubDividerColor = System.Drawing.Color.DarkGray;
			this.cboCCN.Size = new System.Drawing.Size(78, 21);
			this.cboCCN.TabIndex = 1;
			this.cboCCN.Enter += new System.EventHandler(this.OnEnterControl);
			this.cboCCN.Leave += new System.EventHandler(this.OnLeaveControl);
			this.cboCCN.PropBag = "<?xml version=\"1.0\"?><Blob><Styles type=\"C1.Win.C1List.Design.ContextWrapper\"><Da" +
				"ta>Group{BackColor:ControlDark;Border:None,,0, 0, 0, 0;AlignVert:Center;}Style2{" +
				"}Style5{}Style4{}Style7{}Style6{}EvenRow{BackColor:Aqua;}Selected{ForeColor:High" +
				"lightText;BackColor:Highlight;}Style3{}Inactive{ForeColor:InactiveCaptionText;Ba" +
				"ckColor:InactiveCaption;}Footer{}Caption{AlignHorz:Center;}Normal{}HighlightRow{" +
				"ForeColor:HighlightText;BackColor:Highlight;}Style9{AlignHorz:Near;}OddRow{}Reco" +
				"rdSelector{AlignImage:Center;}Heading{Wrap:True;AlignVert:Center;Border:Raised,," +
				"1, 1, 1, 1;ForeColor:ControlText;BackColor:Control;}Style8{}Style10{}Style11{}St" +
				"yle1{}</Data></Styles><Splits><C1.Win.C1List.ListBoxView AllowColSelect=\"False\" " +
				"Name=\"\" CaptionHeight=\"17\" ColumnCaptionHeight=\"17\" ColumnFooterHeight=\"17\" Vert" +
				"icalScrollGroup=\"1\" HorizontalScrollGroup=\"1\"><ClientRect>0, 0, 116, 156</Client" +
				"Rect><VScrollBar><Width>17</Width></VScrollBar><HScrollBar><Height>17</Height></" +
				"HScrollBar><CaptionStyle parent=\"Style2\" me=\"Style9\" /><EvenRowStyle parent=\"Eve" +
				"nRow\" me=\"Style7\" /><FooterStyle parent=\"Footer\" me=\"Style3\" /><GroupStyle paren" +
				"t=\"Group\" me=\"Style11\" /><HeadingStyle parent=\"Heading\" me=\"Style2\" /><HighLight" +
				"RowStyle parent=\"HighlightRow\" me=\"Style6\" /><InactiveStyle parent=\"Inactive\" me" +
				"=\"Style4\" /><OddRowStyle parent=\"OddRow\" me=\"Style8\" /><RecordSelectorStyle pare" +
				"nt=\"RecordSelector\" me=\"Style10\" /><SelectedStyle parent=\"Selected\" me=\"Style5\" " +
				"/><Style parent=\"Normal\" me=\"Style1\" /></C1.Win.C1List.ListBoxView></Splits><Nam" +
				"edStyles><Style parent=\"\" me=\"Normal\" /><Style parent=\"Normal\" me=\"Heading\" /><S" +
				"tyle parent=\"Heading\" me=\"Footer\" /><Style parent=\"Heading\" me=\"Caption\" /><Styl" +
				"e parent=\"Heading\" me=\"Inactive\" /><Style parent=\"Normal\" me=\"Selected\" /><Style" +
				" parent=\"Normal\" me=\"HighlightRow\" /><Style parent=\"Normal\" me=\"EvenRow\" /><Styl" +
				"e parent=\"Normal\" me=\"OddRow\" /><Style parent=\"Heading\" me=\"RecordSelector\" /><S" +
				"tyle parent=\"Caption\" me=\"Group\" /></NamedStyles><vertSplits>1</vertSplits><horz" +
				"Splits>1</horzSplits><Layout>Modified</Layout><DefaultRecSelWidth>16</DefaultRec" +
				"SelWidth></Blob>";
			// 
			// dtmDate
			// 
			this.dtmDate.CustomFormat = "dd-MM-yyyy h:mm tt";
			this.dtmDate.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
			this.dtmDate.Location = new System.Drawing.Point(200, 256);
			this.dtmDate.Name = "dtmDate";
			this.dtmDate.Size = new System.Drawing.Size(134, 20);
			this.dtmDate.TabIndex = 33;
			this.dtmDate.Tag = null;
			this.dtmDate.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.DropDown;
			this.dtmDate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtmDate_KeyDown);
			this.dtmDate.Enter += new System.EventHandler(this.dtmDate_Enter);
			this.dtmDate.DropDownClosed += new DropDownClosedEventHandler(this.dtmDate_DropDownClosed);
			// 
			// txtProductionLine
			// 
			this.txtProductionLine.Location = new System.Drawing.Point(324, 48);
			this.txtProductionLine.MaxLength = 24;
			this.txtProductionLine.Name = "txtProductionLine";
			this.txtProductionLine.Size = new System.Drawing.Size(98, 20);
			this.txtProductionLine.TabIndex = 11;
			this.txtProductionLine.Text = "";
			this.txtProductionLine.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtProductionLine_KeyDown);
			this.txtProductionLine.Validating += new System.ComponentModel.CancelEventHandler(this.txtProductionLine_Validating);
			// 
			// lblProductionLine
			// 
			this.lblProductionLine.ForeColor = System.Drawing.Color.Maroon;
			this.lblProductionLine.Location = new System.Drawing.Point(234, 48);
			this.lblProductionLine.Name = "lblProductionLine";
			this.lblProductionLine.Size = new System.Drawing.Size(88, 20);
			this.lblProductionLine.TabIndex = 10;
			this.lblProductionLine.Text = "Production Line";
			this.lblProductionLine.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// btnProductionLine
			// 
			this.btnProductionLine.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnProductionLine.Location = new System.Drawing.Point(424, 48);
			this.btnProductionLine.Name = "btnProductionLine";
			this.btnProductionLine.Size = new System.Drawing.Size(24, 20);
			this.btnProductionLine.TabIndex = 12;
			this.btnProductionLine.Text = "...";
			this.btnProductionLine.Click += new System.EventHandler(this.btnProductionLine_Click);
			// 
			// lblWorkOrderReport
			// 
			this.lblWorkOrderReport.Location = new System.Drawing.Point(402, 8);
			this.lblWorkOrderReport.Name = "lblWorkOrderReport";
			this.lblWorkOrderReport.TabIndex = 32;
			this.lblWorkOrderReport.Text = "Work Order Report";
			this.lblWorkOrderReport.Visible = false;
			// 
			// txtDCPCycle
			// 
			this.txtDCPCycle.Location = new System.Drawing.Point(83, 70);
			this.txtDCPCycle.MaxLength = 24;
			this.txtDCPCycle.Name = "txtDCPCycle";
			this.txtDCPCycle.Size = new System.Drawing.Size(123, 20);
			this.txtDCPCycle.TabIndex = 14;
			this.txtDCPCycle.Text = "";
			this.txtDCPCycle.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtDCPCycle_KeyDown);
			this.txtDCPCycle.Validating += new System.ComponentModel.CancelEventHandler(this.txtDCPCycle_Validating);
			// 
			// lblDCPCycle
			// 
			this.lblDCPCycle.ForeColor = System.Drawing.Color.Black;
			this.lblDCPCycle.Location = new System.Drawing.Point(6, 70);
			this.lblDCPCycle.Name = "lblDCPCycle";
			this.lblDCPCycle.Size = new System.Drawing.Size(75, 20);
			this.lblDCPCycle.TabIndex = 13;
			this.lblDCPCycle.Text = "DCP Cycle";
			this.lblDCPCycle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// btnDCPCycle
			// 
			this.btnDCPCycle.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnDCPCycle.Location = new System.Drawing.Point(208, 70);
			this.btnDCPCycle.Name = "btnDCPCycle";
			this.btnDCPCycle.Size = new System.Drawing.Size(24, 20);
			this.btnDCPCycle.TabIndex = 15;
			this.btnDCPCycle.Text = "...";
			this.btnDCPCycle.Click += new System.EventHandler(this.btnDCPCycle_Click);
			// 
			// txtDCPDescription
			// 
			this.txtDCPDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.txtDCPDescription.Location = new System.Drawing.Point(234, 70);
			this.txtDCPDescription.MaxLength = 200;
			this.txtDCPDescription.Name = "txtDCPDescription";
			this.txtDCPDescription.ReadOnly = true;
			this.txtDCPDescription.Size = new System.Drawing.Size(214, 20);
			this.txtDCPDescription.TabIndex = 16;
			this.txtDCPDescription.Text = "";
			// 
			// WorkOrder
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.btnClose;
			this.ClientSize = new System.Drawing.Size(632, 494);
			this.Controls.Add(this.txtDCPDescription);
			this.Controls.Add(this.txtDCPCycle);
			this.Controls.Add(this.txtProductionLine);
			this.Controls.Add(this.txtMasterLocation);
			this.Controls.Add(this.txtDescription);
			this.Controls.Add(this.txtWONo);
			this.Controls.Add(this.dgrdData);
			this.Controls.Add(this.lblDCPCycle);
			this.Controls.Add(this.btnDCPCycle);
			this.Controls.Add(this.lblWorkOrderReport);
			this.Controls.Add(this.lblProductionLine);
			this.Controls.Add(this.btnProductionLine);
			this.Controls.Add(this.cboCCN);
			this.Controls.Add(this.dtmTransDate);
			this.Controls.Add(this.btnSearchMasLoc);
			this.Controls.Add(this.lblTransDate);
			this.Controls.Add(this.btnHelp);
			this.Controls.Add(this.btnDelete);
			this.Controls.Add(this.btnSave);
			this.Controls.Add(this.btnAdd);
			this.Controls.Add(this.btnPrint);
			this.Controls.Add(this.btnEdit);
			this.Controls.Add(this.btnClose);
			this.Controls.Add(this.lblDescription);
			this.Controls.Add(this.lblWO_No);
			this.Controls.Add(this.lblMasLoc);
			this.Controls.Add(this.lblCCN);
			this.Controls.Add(this.btnSearchWO);
			this.Controls.Add(this.chkManufacture);
			this.Controls.Add(this.chkFinance);
			this.Controls.Add(this.btnBOM);
			this.Controls.Add(this.btnCost);
			this.Controls.Add(this.btnRouting);
			this.Controls.Add(this.dtmDate);
			this.KeyPreview = true;
			this.Name = "WorkOrder";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Work Order Maintenance";
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.WorkOrder_KeyDown);
			this.Closing += new System.ComponentModel.CancelEventHandler(this.WorkOrder_Closing);
			this.Load += new System.EventHandler(this.WorkOrder_Load);
			((System.ComponentModel.ISupportInitialize)(this.dgrdData)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dtmTransDate)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.cboCCN)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dtmDate)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion
		
		/// <summary>
		/// Check user's permission
		/// After that : Reset Form
		/// After that : Load CCN and set default
		/// After that : Switch form's mode
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void WorkOrder_Load(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".WorkOrder_Load()";
			try
			{
				Security objSecurity = new Security();
				this.Name = THIS;
				if (objSecurity.SetRightForUserOnForm(this, SystemProperty.UserName) == 0)
				{
					this.Close();
					PCSMessageBox.Show(ErrorCode.MESSAGE_YOU_HAVE_NO_RIGHT_TO_VIEW, MessageBoxIcon.Warning);
					return;
				}
				

				//Load CCN and set default CCN
				UtilsBO boUtils = new UtilsBO();
				DataSet dstCCN = boUtils.ListCCN();
				cboCCN.DataSource = dstCCN.Tables[MST_CCNTable.TABLE_NAME];
				cboCCN.DisplayMember	= MST_CCNTable.CODE_FLD;
				cboCCN.ValueMember		= MST_CCNTable.CCNID_FLD;
				
				FormControlComponents.PutDataIntoC1ComboBox(cboCCN,dstCCN.Tables[MST_CCNTable.TABLE_NAME],MST_CCNTable.CODE_FLD,MST_CCNTable.CCNID_FLD,MST_CCNTable.TABLE_NAME);
				
				//get grid layout
				dtbGridLayOut = FormControlComponents.StoreGridLayout(dgrdData);

				if (SystemProperty.CCNID != 0)
				{
					cboCCN.SelectedValue = SystemProperty.CCNID;
				}

				if (mWOFormState == WOFormState.CPOExistWO)
				{
					FormLoadForExistWOForCPO();
					return;
				}
				
				if (voWorkOrderMaster.WorkOrderMasterID != 0)
				{
					dtbGridLayOut = FormControlComponents.StoreGridLayout(dgrdData);
					FillWOData(voWorkOrderMaster.WorkOrderMasterID);
					mFormMode = EnumAction.Default;
				}
				else
				{
					ResetForm();
				}
				SwitchFormMode();
				//Set default Master Location
				if (txtMasterLocation.Text.Trim() == string.Empty)
				{
					FormControlComponents.SetDefaultMasterLocation(txtMasterLocation);
					voMasLoc.MasterLocationID = SystemProperty.MasterLocationID;
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
		/// Create the template data set which 
		/// </summary>
		private void CreateDataSet()
		{
			const string METHOD_NAME = THIS + ".CreateDataSet()";
			try
			{
				dstGridData = new DataSet();
				dstGridData.Tables.Add(PRO_WorkOrderDetailTable.TABLE_NAME);

				//insert columns which is invisible but use to update
				dstGridData.Tables[PRO_WorkOrderDetailTable.TABLE_NAME].Columns.Add(PRO_WorkOrderDetailTable.WORKORDERMASTERID_FLD);
				dstGridData.Tables[PRO_WorkOrderDetailTable.TABLE_NAME].Columns.Add(PRO_WorkOrderDetailTable.SALEORDERMASTERID_FLD);
				dstGridData.Tables[PRO_WorkOrderDetailTable.TABLE_NAME].Columns.Add(PRO_WorkOrderDetailTable.SALEORDERDETAILID_FLD);
				dstGridData.Tables[PRO_WorkOrderDetailTable.TABLE_NAME].Columns.Add(PRO_WorkOrderDetailTable.WORKORDERDETAILID_FLD);
			
				dstGridData.Tables[PRO_WorkOrderDetailTable.TABLE_NAME].Columns.Add(ITM_ProductTable.PRODUCTID_FLD);
				dstGridData.Tables[PRO_WorkOrderDetailTable.TABLE_NAME].Columns.Add(PRO_WorkOrderDetailTable.MFGCLOSEDATE_FLD);
				dstGridData.Tables[PRO_WorkOrderDetailTable.TABLE_NAME].Columns.Add(PRO_WorkOrderDetailTable.FINCLOSEDATE_FLD);
				dstGridData.Tables[PRO_WorkOrderDetailTable.TABLE_NAME].Columns.Add(PRO_WorkOrderDetailTable.PRIORITY_FLD);
				dstGridData.Tables[PRO_WorkOrderDetailTable.TABLE_NAME].Columns.Add(PRO_WorkOrderDetailTable.STOCKUMID_FLD);
				dstGridData.Tables[PRO_WorkOrderDetailTable.TABLE_NAME].Columns.Add(PRO_WorkOrderDetailTable.STATUS_FLD);
				dstGridData.Tables[PRO_WorkOrderDetailTable.TABLE_NAME].Columns.Add(PRO_WorkOrderDetailTable.AGC_FLD);

				//insert display columns
				dstGridData.Tables[PRO_WorkOrderDetailTable.TABLE_NAME].Columns.Add(PRO_WorkOrderDetailTable.LINE_FLD);
				dstGridData.Tables[PRO_WorkOrderDetailTable.TABLE_NAME].Columns.Add(ITM_ProductTable.CODE_FLD);
				dstGridData.Tables[PRO_WorkOrderDetailTable.TABLE_NAME].Columns.Add(ITM_ProductTable.DESCRIPTION_FLD);
				dstGridData.Tables[PRO_WorkOrderDetailTable.TABLE_NAME].Columns.Add(ITM_ProductTable.REVISION_FLD);

				//HACK: added by Tuan TQ. 23 May, 2006. Add Category column
				dstGridData.Tables[PRO_WorkOrderDetailTable.TABLE_NAME].Columns.Add(ITM_CategoryTable.TABLE_NAME +  ITM_CategoryTable.CODE_FLD);
				//End hack

				dstGridData.Tables[PRO_WorkOrderDetailTable.TABLE_NAME].Columns.Add(UM);
				dstGridData.Tables[PRO_WorkOrderDetailTable.TABLE_NAME].Columns.Add(STATUS_STRING);
				dstGridData.Tables[PRO_WorkOrderDetailTable.TABLE_NAME].Columns.Add(PRO_WorkOrderDetailTable.ORDERQUANTITY_FLD, typeof(Decimal));
				dstGridData.Tables[PRO_WorkOrderDetailTable.TABLE_NAME].Columns.Add(PRO_WorkOrderDetailTable.STARTDATE_FLD, typeof(DateTime));
				dstGridData.Tables[PRO_WorkOrderDetailTable.TABLE_NAME].Columns.Add(PRO_WorkOrderDetailTable.DUEDATE_FLD, typeof(DateTime));
				dstGridData.Tables[PRO_WorkOrderDetailTable.TABLE_NAME].Columns.Add(SALE_ORDER_CODE);
				dstGridData.Tables[PRO_WorkOrderDetailTable.TABLE_NAME].Columns.Add(SO_SaleOrderDetailTable.SALEORDERLINE_FLD);
				dstGridData.Tables[PRO_WorkOrderDetailTable.TABLE_NAME].Columns.Add(ITM_ProductTable.COSTMETHOD_FLD);
				dstGridData.Tables[PRO_WorkOrderDetailTable.TABLE_NAME].Columns.Add(PRO_WorkOrderDetailTable.ESTCST_FLD, typeof(decimal));
				dstGridData.Tables[PRO_WorkOrderDetailTable.TABLE_NAME].Columns.Add(AGC_CODE);
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
		/// Config grid layout : lock column, set control to columns...
		/// </summary>
		/// <param name="pblnLock">which specifies that grid's locked or unlocked</param>
		private void ConfigGrid(bool pblnLock)
		{
			const string METHOD_NAME = THIS + ".ConfigGrid()";
			try
			{
				dgrdData.Enabled = true;
				for (int i =0; i <dgrdData.Splits[0].DisplayColumns.Count; i++)
				{
					dgrdData.Splits[0].DisplayColumns[i].Locked = true;
				}
				dgrdData.Splits[0].DisplayColumns[ITM_ProductTable.CODE_FLD].Locked = pblnLock;
				dgrdData.Splits[0].DisplayColumns[ITM_ProductTable.DESCRIPTION_FLD].Locked = pblnLock;
				dgrdData.Splits[0].DisplayColumns[PRO_WorkOrderDetailTable.ORDERQUANTITY_FLD].Locked = pblnLock;
				dgrdData.Splits[0].DisplayColumns[PRO_WorkOrderDetailTable.STARTDATE_FLD].Locked = pblnLock;
				dgrdData.Splits[0].DisplayColumns[PRO_WorkOrderDetailTable.DUEDATE_FLD].Locked = pblnLock;
				dgrdData.Splits[0].DisplayColumns[SALE_ORDER_CODE].Locked = pblnLock;
				dgrdData.Splits[0].DisplayColumns[SO_SaleOrderDetailTable.SALEORDERLINE_FLD].Locked = pblnLock;
				dgrdData.Splits[0].DisplayColumns[PRO_WorkOrderDetailTable.ORDERQUANTITY_FLD].DataColumn.NumberFormat = Constants.DECIMAL_NUMBERFORMAT;
				dgrdData.Splits[0].DisplayColumns[PRO_WorkOrderDetailTable.ORDERQUANTITY_FLD].Style.HorizontalAlignment = AlignHorzEnum.Far;
				dgrdData.Splits[0].DisplayColumns[PRO_WorkOrderDetailTable.STARTDATE_FLD].Style.HorizontalAlignment = AlignHorzEnum.Center;
				dgrdData.Splits[0].DisplayColumns[PRO_WorkOrderDetailTable.DUEDATE_FLD].Style.HorizontalAlignment = AlignHorzEnum.Center;
				dgrdData.Splits[0].DisplayColumns[PRO_WorkOrderDetailTable.LINE_FLD].Style.HorizontalAlignment = AlignHorzEnum.Far;
				dgrdData.Columns[PRO_WorkOrderDetailTable.STARTDATE_FLD].Editor = dtmDate;
				dgrdData.Columns[PRO_WorkOrderDetailTable.STARTDATE_FLD].NumberFormat = Constants.DATETIME_FORMAT_HOUR;
				dgrdData.Columns[PRO_WorkOrderDetailTable.DUEDATE_FLD].Editor = dtmDate;
				dgrdData.Columns[PRO_WorkOrderDetailTable.DUEDATE_FLD].NumberFormat = Constants.DATETIME_FORMAT_HOUR;
				dgrdData.Columns[PRO_WorkOrderDetailTable.ESTCST_FLD].NumberFormat = Constants.DECIMAL_NUMBERFORMAT;
				if (!pblnLock)
				{
					dgrdData.Splits[0].DisplayColumns[ITM_ProductTable.CODE_FLD].Button = true;
					dgrdData.Splits[0].DisplayColumns[ITM_ProductTable.DESCRIPTION_FLD].Button = true;
					dgrdData.Splits[0].DisplayColumns[SALE_ORDER_CODE].Button = true;
					dgrdData.Splits[0].DisplayColumns[SO_SaleOrderDetailTable.SALEORDERLINE_FLD].Button = true;
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
		/// Search Master Location by CCN
		/// </summary>
		///	<author>
		///	Do Manh Tuan
		///	Thursday, June. 2. 2005
		///	</author>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnSearchMasLoc_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnSearchMasLoc_Click()";
			try
			{
				DataRowView drwResult = null;
				Hashtable htbCondition = new Hashtable();
				
				if (cboCCN.SelectedValue != null)
				{
					htbCondition.Add(MST_MasterLocationTable.CCNID_FLD, cboCCN.SelectedValue.ToString());
				}
				else
				{
					PCSMessageBox.Show(ErrorCode.MESSAGE_RGA_CCN, MessageBoxIcon.Warning);
					cboCCN.Focus();
					return;
				}
				drwResult = FormControlComponents.OpenSearchForm(MST_MasterLocationTable.TABLE_NAME, MST_MasterLocationTable.CODE_FLD, txtMasterLocation.Text, htbCondition, true);
				if (drwResult != null)
				{
					txtMasterLocation.Text = drwResult[MST_MasterLocationTable.CODE_FLD].ToString();
					voMasLoc.MasterLocationID = int.Parse(drwResult[MST_MasterLocationTable.MASTERLOCATIONID_FLD].ToString());
					if ((voMasLoc.MasterLocationID != voWorkOrderMaster.MasterLocationID)&& (voWorkOrderMaster.MasterLocationID != 0))
					{
						ResetFormIfMasLocChanged();
					}
				}
				else
				{
					txtMasterLocation.Focus();
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
		/// Search WO event:
		///		Open the search form to search WO by WONo
		/// </summary>
		/// <author>
		/// Do Manh Tuan
		/// Thursday, Jun - 2 -2005
		/// </author>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnSearchWO_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnSearchWO_Click()";
			try
			{
				DataRowView drwResult = null;
				Hashtable htbCondition = new Hashtable();
				if (txtMasterLocation.Text.Trim() == string.Empty)
				{
					PCSMessageBox.Show(ErrorCode.MESSAGE_RGA_MASTERLOCATION, MessageBoxIcon.Warning);
					txtMasterLocation.Focus();
					return;
				}
				if (cboCCN.SelectedValue != null)
				{
					htbCondition.Add(PRO_WorkOrderMasterTable.CCNID_FLD, cboCCN.SelectedValue.ToString());
					htbCondition.Add(PRO_WorkOrderMasterTable.MASTERLOCATIONID_FLD, voMasLoc.MasterLocationID);
				}
				drwResult = FormControlComponents.OpenSearchForm(PRO_WorkOrderMasterTable.TABLE_NAME, PRO_WorkOrderMasterTable.WORKORDERNO_FLD, txtWONo.Text, htbCondition, true);
				if (drwResult != null)
				{
					FillWOData(drwResult.Row);
				}
				else
				{
					txtWONo.Focus();
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
		/// Add event :
		///		Reset form()
		///		Change form's mode
		///		Select data and unlock grid
		/// </summary>
		///	<author>
		///		Do Manh Tuan
		///		Thursday, Jun 2 2005
		///	</author>	
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnAdd_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnAdd_Click()";
			try
			{
				mFormMode = EnumAction.Add;
				ResetForm();
				SwitchFormMode();
				dtmTransDate.Value = new UtilsBO().GetDBDate();
				//txtWONo.Text = new UtilsBO().GetNoByMask(PRO_WorkOrderMasterTable.TABLE_NAME, PRO_WorkOrderMasterTable.WORKORDERNO_FLD,  DateTime.Parse(dtmTransDate.Value.ToString()), string.Empty );						
				txtWONo.Text = FormControlComponents.GetNoByMask(this);
				//Fill Default Master Location 
				FormControlComponents.SetDefaultMasterLocation(txtMasterLocation);
				voMasLoc.MasterLocationID = SystemProperty.MasterLocationID;
				CreateDataSet();
				dgrdData.DataSource = dstGridData.Tables[PRO_WorkOrderDetailTable.TABLE_NAME];
				FormControlComponents.RestoreGridLayout(dgrdData, dtbGridLayOut);
				ConfigGrid(false);
				strLastValidPro = string.Empty;
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
		/// Validate all data, and some business rules
		/// </summary>
		/// <returns></returns>
		private bool ValidateData()
		{
			const string METHOD_NAME = THIS + ".ValidateData()";
			try
			{
				if (FormControlComponents.CheckMandatory(dtmTransDate))
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID);
					dtmTransDate.Focus();
					dtmTransDate.Select();
					return false;
				}

				#region no need to check period
//				if (!FormControlComponents.CheckDateInCurrentPeriod((DateTime)dtmTransDate.Value))
//				{
//					PCSMessageBox.Show(ErrorCode.MESSAGE_RGV_POSTDATE_PERIOD);
//					dtmTransDate.Focus();
//					dtmTransDate.Select();
//					return false;
//				}
				#endregion

				if (FormControlComponents.CheckMandatory(txtMasterLocation))
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID);
					txtMasterLocation.Focus();
					txtMasterLocation.Select();
					return false;
				}
				if (FormControlComponents.CheckMandatory(txtWONo))
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID);
					txtWONo.Focus();
					txtWONo.Select();
					return false;
				}
				if (FormControlComponents.CheckMandatory(txtProductionLine))
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID);
					txtProductionLine.Focus();
					txtProductionLine.Select();
					return false;
				}

				//check row in grid
				int intCountRow =0;
				for (int i =0; i < dgrdData.RowCount; i++)
				{
					if (dgrdData[i, ITM_ProductTable.CODE_FLD].ToString() != string.Empty)
					{
						intCountRow++;
					}
				}
				if (intCountRow ==0)
				{
					PCSMessageBox.Show(ErrorCode.MESSAGE_WO_HASWOLINE);
					dgrdData.Row = 0;
					dgrdData.Col = dgrdData.Splits[0].DisplayColumns.IndexOf(dgrdData.Splits[0].DisplayColumns[ITM_ProductTable.CODE_FLD]);
					dgrdData.Focus();
					return false;
				}

				//check madatory field in grid
				dgrdData.UpdateData();
				int intRountCount = dgrdData.RowCount;
				for (int i = 0; i < intRountCount; i++)
				{
					if (dgrdData[i, ITM_ProductTable.CODE_FLD].ToString() == string.Empty)
					{
						PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Error);
						dgrdData.Row = i;
						dgrdData.Col = dgrdData.Splits[0].DisplayColumns.IndexOf(dgrdData.Splits[0].DisplayColumns[ITM_ProductTable.CODE_FLD]);
						dgrdData.Focus();
						return false;
					}
				}
				for (int i =0; i < dgrdData.RowCount; i++)
				{
					if (dgrdData[i, ITM_ProductTable.CODE_FLD].ToString() != string.Empty)
					{
						if (dgrdData[i, PRO_WorkOrderDetailTable.ORDERQUANTITY_FLD].ToString() == string.Empty)
						{
							PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Error);
							dgrdData.Row = i;
							dgrdData.Col = dgrdData.Splits[0].DisplayColumns.IndexOf(dgrdData.Splits[0].DisplayColumns[PRO_WorkOrderDetailTable.ORDERQUANTITY_FLD]);
							dgrdData.Focus();
							return false;
						}
						if (dgrdData[i, PRO_WorkOrderDetailTable.STARTDATE_FLD].ToString() == string.Empty)
						{
							PCSMessageBox.Show(ErrorCode.MESSAGE_WO_STARTDATE_TRANSDATE, MessageBoxIcon.Error);
							dgrdData.Row = i;
							dgrdData.Col = dgrdData.Splits[0].DisplayColumns.IndexOf(dgrdData.Splits[0].DisplayColumns[PRO_WorkOrderDetailTable.STARTDATE_FLD]);
							dgrdData.Focus();
							return false;
						}
						if (dgrdData[i, PRO_WorkOrderDetailTable.DUEDATE_FLD].ToString() == string.Empty)
						{
							PCSMessageBox.Show(ErrorCode.MESSAGE_WO_DUEDATE_TRANSDATE, MessageBoxIcon.Error);
							dgrdData.Row = i;
							dgrdData.Col = dgrdData.Splits[0].DisplayColumns.IndexOf(dgrdData.Splits[0].DisplayColumns[PRO_WorkOrderDetailTable.DUEDATE_FLD]);
							dgrdData.Focus();
							return false;
						}
					}
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
			return true;
		}


		/// <summary>
		/// Validate all data before select SO or SOLine
		/// </summary>
		/// <returns></returns>
		/// <param name="pintType">0: SaleOrder - 1: SaleOrderLine</param>
		private bool ValidateDataForSaleOrder(int pintType)
		{
			const string METHOD_NAME = THIS + ".ValidateDataForSaleOrder()";
			try
			{
				// if user input SaleOrder
				if (pintType == 0)
				{
					if (FormControlComponents.CheckMandatory(txtMasterLocation))
					{
						PCSMessageBox.Show(ErrorCode.MESSAGE_WO_SELECT_MASLOC);
						txtMasterLocation.Focus();
						return false;
					}
					if (dgrdData[dgrdData.Row,ITM_ProductTable.CODE_FLD].ToString() == string.Empty)
					{
						dgrdData.Col = dgrdData.Columns.IndexOf(dgrdData.Columns[ITM_ProductTable.CODE_FLD]);
						dgrdData.Focus();

						PCSMessageBox.Show(ErrorCode.MESSAGE_WO_SELECT_PRODUCT);
						return false;
					}
				}
				else
				{
					//if user input SaleOrderLine
					if (dgrdData[dgrdData.Row, SALE_ORDER_CODE].ToString() == string.Empty)
					{
						PCSMessageBox.Show(ErrorCode.MESSAGE_WO_SELECT_SALEORDER);
						return false;
					}
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
		/// Save event:
		///		Validate date
		///		Get data into voWorkOrderMaster
		///		Sate to database
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnSave_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnSave_Click()";
			try
			{
                blnHasError = true;
				
				if (!dgrdData.EditActive && ValidateData())
				{
					// HACK: Trada 13-12-2005
					if(Security.IsDifferencePrefix(this,lblWO_No,txtWONo))
						return;
					// END: Trada 13-12-2005
					boWorkOrder = new WorkOrderBO();
					GetDataOfWorkOrderMaster();
					if (FormMode == EnumAction.Add)
					{
						if (mWOFormState == WOFormState.Normal)
						{
							voWorkOrderMaster.WorkOrderMasterID = boWorkOrder.AddAndReturnID(voWorkOrderMaster, dstGridData);
						}
					}
					else
					{
						if (mWOFormState == WOFormState.Normal)
						{
							boWorkOrder.UpdateWOAndWOLines(voWorkOrderMaster, dstGridData, ArlWOLineID);
						}
						else
						{
							ArrayList arlCPOIDs = new ArrayList();
							if (dtwCPOs.Table.TableName == MTR_CPOTable.TABLE_NAME)
							{
								foreach (DataRowView drvCPO in dtwCPOs)
								{
									arlCPOIDs.Add(drvCPO[MTR_CPOTable.CPOID_FLD].ToString());
								}
								boWorkOrder.UpdateExistedWOImmediately(dstGridData, voWorkOrderMaster, ArlWOLineID, arlCPOIDs, PlanTypeEnum.MPS.ToString());
							}
							else
							{
								foreach (DataRowView drvCPO in dtwCPOs)
								{
									arlCPOIDs.Add(drvCPO[PRO_DCPResultDetailTable.DCPRESULTDETAILID_FLD].ToString());
								}
								boWorkOrder.UpdateExistedWOImmediately(dstGridData, voWorkOrderMaster, ArlWOLineID, arlCPOIDs, string.Empty);
							}
							
						}
					}
					Security.UpdateUserNameModifyTransaction(this, PRO_WorkOrderMasterTable.WORKORDERMASTERID_FLD, voWorkOrderMaster.WorkOrderMasterID);
					PCSMessageBox.Show(ErrorCode.MESSAGE_AFTER_SAVE_DATA);
					FormMode = EnumAction.Default;

					// HACK: SonHT 2005-12-08
					SwitchFormMode();
					// END: SonHT 2005-12-08

					//reload grid form database
					dstGridData = boWorkOrder.GetWODetailByMaster(voWorkOrderMaster.WorkOrderMasterID);
					dgrdData.DataSource = dstGridData.Tables[0];

					//restore the layout of grid
					dtmDate.CustomFormat = Constants.DATETIME_FORMAT_HOUR;
					FormControlComponents.RestoreGridLayout(dgrdData, dtbGridLayOut);
					ConfigGrid(true);
					btnDelete.Enabled = true;
					btnSearchWO.Enabled = true;
					btnEdit.Enabled = true;
					btnSave.Enabled = false;
					btnAdd.Enabled = true;
					blnHasError = false;
					btnBOM.Enabled = true;
					btnRouting.Enabled = true;
					if (CheckWOLines(WOLineStatus.MfgClose))
					{
						chkManufacture.Checked = true;
					}
					else
					{
						if (CheckWOLines(WOLineStatus.FinClose))
						{
							chkFinance.Checked = true;
						}
					}
				}
			}
			catch (PCSException ex)
			{
				PCSMessageBox.Show(ex.mCode, MessageBoxIcon.Error);
				if (ex.mCode == ErrorCode.DUPLICATE_KEY)
				{
					txtWONo.Focus();
				}
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
		/// Edit event:
		///		change FormMode
		///		Reload data
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnEdit_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnEdit_Click()";
			try
			{
				if(Security.NoRightToEditTransaction(this, PRO_WorkOrderMasterTable.WORKORDERMASTERID_FLD, voWorkOrderMaster.WorkOrderMasterID))
				{
					return;
				}
				FormMode = EnumAction.Edit;
				SwitchFormMode();
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


		private void btnClose_Click(object sender, EventArgs e)
		{
			if (dgrdData.EditActive) return;
			this.Close();
		}

		/// <summary>
		/// Transfer information to WORouting form
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnRouting_Click(object sender, EventArgs e)
		{
		}


		/// <summary>
		/// Transfer information to WOBOM form
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnBOM_Click(object sender, EventArgs e)
		{
		}
		
		/// <summary>
		/// Transfer information to Cost form
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnCost_Click(object sender, EventArgs e)
		{
		}


		/// <summary>
		/// Delete event:
		///		Check all work order lines
		///		Delete WO if all lines are unrelease
		/// </summary>
		/// <author>
		/// Do Manh Tuan
		/// 08-06-2005
		/// </author>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnDelete_Click(object sender, EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnDelete_Click()";
			try
			{
				if(Security.NoRightToDeleteTransaction(this, PRO_WorkOrderMasterTable.WORKORDERMASTERID_FLD, voWorkOrderMaster.WorkOrderMasterID))
				{
					return;
				}
				if (!dgrdData.EditActive && CheckWOLines(WOLineStatus.Unreleased))
				{
					//allow deleteS
					if (PCSMessageBox.Show(ErrorCode.MESSAGE_DELETE_RECORD, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
					{
						//reload grid form database
						dstGridData = boWorkOrder.GetWODetailByMaster(voWorkOrderMaster.WorkOrderMasterID);
						dgrdData.DataSource = dstGridData.Tables[0];

						//restore the layout of grid
						FormControlComponents.RestoreGridLayout(dgrdData, dtbGridLayOut);

						boWorkOrder.DeleteWOAndWOLines(voWorkOrderMaster, dstGridData, ArlWOLineID);
						FormMode = EnumAction.Default;
						string strMasLoc = txtMasterLocation.Text.Trim();
						ResetForm();
						txtMasterLocation.Text = strMasLoc;
						SwitchFormMode();
						CreateDataSet();
						dgrdData.DataSource = dstGridData.Tables[PRO_WorkOrderDetailTable.TABLE_NAME];
						FormControlComponents.RestoreGridLayout(dgrdData, dtbGridLayOut);
						ConfigGrid(true);
						txtMasterLocation.Text = string.Empty;
						dtmTransDate.Value = DBNull.Value;
						txtMasterLocation.Focus();
						//Set default Master Location
						FormControlComponents.SetDefaultMasterLocation(txtMasterLocation);
						voMasLoc.MasterLocationID = SystemProperty.MasterLocationID;
					}
				}
				else
				{
					//don't allow detete
					PCSMessageBox.Show(ErrorCode.MESSAGE_WO_CANNOT_DELWO, MessageBoxIcon.Error);
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
		/// Form Keydown event
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void WorkOrder_KeyDown(object sender, KeyEventArgs e)
		{
			const string METHOD_NAME = THIS + ".WorkOrder_KeyDown()";
			try
			{
				if (e.KeyCode == Keys.F12)
				{
					if (!dgrdData.Splits[0].DisplayColumns[ITM_ProductTable.CODE_FLD].Locked)
					{
						dgrdData.Row = dgrdData.RowCount;
						dgrdData.Col = dgrdData.Columns.IndexOf(dgrdData.Columns[ITM_ProductTable.CODE_FLD]);
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
		/// Keydown event:
		///		F12 : Add new row into the grid
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void dgrdData_KeyDown(object sender, KeyEventArgs e)
		{
			const string METHOD_NAME = THIS + ".dgrdData_KeyDown()";
			try
			{
				switch (e.KeyCode)
				{
					case Keys.F4:
						if (btnSave.Enabled)
						{
							dgrdData_ButtonClick(sender, null);
						}
						break;
					case Keys.Delete:
						if ((e.KeyCode == Keys.Delete)&&(dgrdData.SelectedRows.Count > 0))
						{
							if (btnSave.Enabled)
							{
								dgrdData.AllowDelete = true;
								FormControlComponents.DeleteMultiRowsOnTrueDBGrid(dgrdData);
							}
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
		/// Based on FormMode property to switching form mode
		/// case EnumAction.Default:
		/// case EnumAction.Add:
		/// case EnumAction.Edit:
		/// </summary>
		private void SwitchFormMode()
		{
			switch (mFormMode)
			{
				case EnumAction.Default:
					btnAdd.Enabled = true;
					dtmTransDate.Enabled = false;
					txtDescription.Enabled = false;
					btnSave.Enabled = false;
					btnEdit.Enabled = false;
					btnDelete.Enabled = false;
					btnBOM.Enabled = false;
					btnRouting.Enabled = false;
					btnCost.Enabled = false;
					dgrdData.AllowDelete = false;
					btnSearchWO.Enabled = true;
					ConfigGrid(true);
					btnPrint.Enabled = false;
					if (dgrdData.RowCount > 0)
					{
						//check security 
						btnBOM.Enabled = true;
						btnRouting.Enabled = true;
						btnCost.Enabled = true;

						btnEdit.Enabled = true;
						btnDelete.Enabled = true;
						btnPrint.Enabled = true;
					}
					txtMasterLocation.Enabled = true;
					btnSearchMasLoc.Enabled = true;
					btnProductionLine.Enabled = false;
					txtProductionLine.Enabled = false;
					btnDCPCycle.Enabled = false;
					txtDCPCycle.Enabled = false;
					break;
				case EnumAction.Add:
					btnAdd.Enabled = false;
					dtmTransDate.Enabled = true;
					txtDescription.Enabled = true;
					btnSave.Enabled = true;
					btnSearchWO.Enabled = false;
					btnEdit.Enabled = false;
					btnDelete.Enabled = false;
					ConfigGrid(false);
					dtmTransDate.Focus();
					dgrdData.AllowDelete = true;
					btnBOM.Enabled = false;
					btnRouting.Enabled = false;
					btnCost.Enabled = false;

					txtMasterLocation.Enabled = true;
					btnSearchMasLoc.Enabled = true;
					btnProductionLine.Enabled = true;
					txtProductionLine.Enabled = true;
					btnDCPCycle.Enabled = true;
					txtDCPCycle.Enabled = true;
					btnPrint.Enabled = false;
					break;
				case EnumAction.Edit:
					btnEdit.Enabled = false;
					btnAdd.Enabled = false;
					dtmTransDate.Enabled = true;
					txtDescription.Enabled = true;
					btnSearchWO.Enabled = false;
					btnSave.Enabled = true;
					ConfigGrid(false);
					dtmTransDate.Focus();
					dgrdData.AllowDelete = true;
					btnBOM.Enabled = false;
					btnRouting.Enabled = false;
					btnCost.Enabled = false;
					btnDelete.Enabled = false;

					txtMasterLocation.Enabled = false;
					btnSearchMasLoc.Enabled = false;
					btnProductionLine.Enabled = true;
					txtProductionLine.Enabled = true;
					btnDCPCycle.Enabled = true;
					txtDCPCycle.Enabled = true;
					btnPrint.Enabled = false;
					break;
			}
		}

		/// <summary>
		/// Clear all data in form to add new record
		/// </summary>
		private void ResetForm()
		{
			voWorkOrderMaster = new PRO_WorkOrderMasterVO();
			chkFinance.Checked = false;
			chkManufacture.Checked = false;
			txtMasterLocation.Text = string.Empty;
			txtWONo.Text = string.Empty;
			txtProductionLine.Text = string.Empty;
			txtProductionLine.Tag = null;
			txtDCPCycle.Text = string.Empty;
			txtDCPCycle.Tag = null;
			txtDCPDescription.Text = string.Empty;
			txtDescription.Text = string.Empty;
			dtmTransDate.Focus();
			dtmDate.CustomFormat = Constants.DATETIME_FORMAT_HOUR;
			dtmDate.Value = new UtilsBO().GetDBDate();
		}
		
		/// <summary>
		/// Get data of WOMaster into voWorkOrderMaster
		/// <author>
		/// Do Manh Tuan
		/// Friday, Jun - 3 - 2005
		/// </author>
		/// <addition>
		/// Addition function not in Design
		/// </addition>
		/// </summary>
		private void GetDataOfWorkOrderMaster()
		{
			voWorkOrderMaster.CCNID = int.Parse(cboCCN.SelectedValue.ToString());	
			voWorkOrderMaster.MasterLocationID = voMasLoc.MasterLocationID;
			if (txtProductionLine.Text != string.Empty)
				voWorkOrderMaster.ProductionLineID = int.Parse(txtProductionLine.Tag.ToString());
			else
				voWorkOrderMaster.ProductionLineID = 0;
			voWorkOrderMaster.WorkOrderNo = txtWONo.Text.Trim();
			voWorkOrderMaster.TransDate = DateTime.Parse(dtmTransDate.Value.ToString());
			voWorkOrderMaster.Description = txtDescription.Text.Trim();
			// dcp cycle information
			if (txtDCPCycle.Text != string.Empty)
				voWorkOrderMaster.DCOptionMasterID = Convert.ToInt32(txtDCPCycle.Tag);
			else
				voWorkOrderMaster.DCOptionMasterID = 0;
		}

		/// <summary>
		/// - Get SaleOrderMasterID from return value of OpenSearchForm method (DataRow)
		/// - Get SO_SaleOrderMasterVO object by SaleOrderMasterID
		/// - Fill value to following columns of grid: 
		/// 	+ Sale Order Master = SO_SaleOrderMasterVO.Code
		/// 	+ SaleOrderMasterID = SO_SaleOrderMasterVO.SaleOrderMasterID
		/// - Clear sale order line data: Sale Order Line, SaleOrderDetailID columns
		/// </summary>
		private void FillSaleOrderData(DataRow pdrowData)
		{
			const string METHOD_NAME = THIS + ".FillSaleOrderData()";
			try
			{
				int row = dgrdData.Row; 
				dgrdData[row, SALE_ORDER_CODE] = pdrowData[SALE_ORDER_CODE].ToString();
				dgrdData[row, PRO_WorkOrderDetailTable.SALEORDERMASTERID_FLD] = pdrowData[PRO_WorkOrderDetailTable.SALEORDERMASTERID_FLD].ToString();
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
		/// - Get SaleOrderDetailID from return value of OpenSearchForm method (DataRow)
		/// - Get SO_SaleOrderDetailVO object by SaleOrderDetailID
		/// - Fill value to following columns of grid: 
		/// 	+ Sale Order Line = SO_SaleOrderDetailVO.SaleOrderLine
		/// 	+ SaleOrderDetailID = SO_SaleOrderDetailVO.SaleOrderDetailID
		/// </summary>
		private void FillSOLineData(DataRow pdrowData)
		{
			const string METHOD_NAME = THIS + ".FillSOLineData()";
			try
			{
				int row = dgrdData.Row; 
				dgrdData[row, SO_SaleOrderDetailTable.SALEORDERLINE_FLD] = pdrowData[SO_SaleOrderDetailTable.SALEORDERLINE_FLD].ToString();
				dgrdData[row, PRO_WorkOrderDetailTable.SALEORDERDETAILID_FLD] = pdrowData[PRO_WorkOrderDetailTable.SALEORDERDETAILID_FLD].ToString();
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
		/// - get PRO_WorkOrderMasterVO from ID
		/// - fill data to TransDate, MasterLocation, Work Order No, Description
		/// - get all work order line by work order master id
		/// - bind data to grid.
		/// </summary>
		private void FillWOData(int pintWOMasterID)
		{
			voWorkOrderMaster = (PRO_WorkOrderMasterVO) boWorkOrder.GetObjectWOMasterVO(pintWOMasterID);
			txtDescription.Text = voWorkOrderMaster.Description;
			txtWONo.Text = voWorkOrderMaster.WorkOrderNo;
			dtmTransDate.Value = voWorkOrderMaster.TransDate;
			if (voWorkOrderMaster.ProductionLineID != 0)
			{
				txtProductionLine.Text = boWorkOrder.GetProductionLineByID(voWorkOrderMaster.ProductionLineID);
				txtProductionLine.Tag = voWorkOrderMaster.ProductionLineID;
			}
			else
			{
				txtProductionLine.Text = string.Empty;
				txtProductionLine.Tag = null;
			}
			if (voWorkOrderMaster.DCOptionMasterID >0)
			{
				DataRowView drvProLine = FormControlComponents.OpenSearchForm(PRO_DCOptionMasterTable.TABLE_NAME, PRO_DCOptionMasterTable.DCOPTIONMASTERID_FLD, voWorkOrderMaster.DCOptionMasterID.ToString(), null, false);
				if (drvProLine != null)
				{
					txtDCPCycle.Text = drvProLine[PRO_DCOptionMasterTable.CYCLE_FLD].ToString();
					txtDCPCycle.Tag = drvProLine[PRO_DCOptionMasterTable.DCOPTIONMASTERID_FLD];
					txtDCPDescription.Text = drvProLine[PRO_DCOptionMasterTable.DESCRIPTION_FLD].ToString();
				}
			}
			else
			{
				txtDCPCycle.Text = string.Empty;
				txtDCPCycle.Tag = null;
				txtDCPDescription.Text = string.Empty;
			}
			chkFinance.Checked = false;
			chkManufacture.Checked = false;
			BindDataToGrid();
			if (CheckWOLines(WOLineStatus.MfgClose))
			{
				chkManufacture.Checked = true;
			}
			else
			{
				if (CheckWOLines(WOLineStatus.FinClose))
				{
					chkFinance.Checked = true;
				}
			}
		}


		private void FillWOData(DataRow pdrowData)
		{
			voWorkOrderMaster.WorkOrderMasterID = int.Parse(pdrowData[PRO_WorkOrderMasterTable.WORKORDERMASTERID_FLD].ToString());
			voWorkOrderMaster.MasterLocationID = int.Parse(pdrowData[PRO_WorkOrderMasterTable.MASTERLOCATIONID_FLD].ToString());
			txtDescription.Text = pdrowData[PRO_WorkOrderMasterTable.DESCRIPTION_FLD].ToString().Trim();
			if ((pdrowData[PRO_WorkOrderMasterTable.PRODUCTIONLINEID_FLD].ToString() != string.Empty)
				&& (pdrowData[PRO_WorkOrderMasterTable.PRODUCTIONLINEID_FLD] != DBNull.Value))
			{
				txtProductionLine.Tag = pdrowData[PRO_WorkOrderMasterTable.PRODUCTIONLINEID_FLD];
				txtProductionLine.Text = boWorkOrder.GetProductionLineByID(int.Parse(pdrowData[PRO_WorkOrderMasterTable.PRODUCTIONLINEID_FLD].ToString()));
			}
			else
			{
				txtProductionLine.Text = string.Empty;
				txtProductionLine.Tag = null;
			}
			if (pdrowData[PRO_WorkOrderMasterTable.DCOPTIONMASTERID_FLD] != DBNull.Value &&
				pdrowData[PRO_WorkOrderMasterTable.DCOPTIONMASTERID_FLD].ToString() != "0")
			{
				DataRowView drvProLine = FormControlComponents.OpenSearchForm(PRO_DCOptionMasterTable.TABLE_NAME, PRO_DCOptionMasterTable.DCOPTIONMASTERID_FLD, pdrowData[PRO_WorkOrderMasterTable.DCOPTIONMASTERID_FLD].ToString(), null, false);
				if (drvProLine != null)
				{
					txtDCPCycle.Text = drvProLine[PRO_DCOptionMasterTable.CYCLE_FLD].ToString();
					txtDCPCycle.Tag = drvProLine[PRO_DCOptionMasterTable.DCOPTIONMASTERID_FLD];
					txtDCPDescription.Text = drvProLine[PRO_DCOptionMasterTable.DESCRIPTION_FLD].ToString();
				}
			}
			else
			{
				txtDCPCycle.Text = string.Empty;
				txtDCPCycle.Tag = null;
				txtDCPDescription.Text = string.Empty;
			}
			txtWONo.Text = pdrowData[PRO_WorkOrderMasterTable.WORKORDERNO_FLD].ToString();
			dtmTransDate.Value = pdrowData[PRO_WorkOrderMasterTable.TRANSDATE_FLD].ToString();
			BindDataToGrid();
			SwitchFormMode();
			chkFinance.Checked = false;
			chkManufacture.Checked = false;
			if (CheckWOLines(WOLineStatus.MfgClose))
			{
				chkManufacture.Checked = true;
			}
			else
			{
				if (CheckWOLines(WOLineStatus.FinClose))
				{
					chkFinance.Checked = true;
				}
			}
			strLastValidPro = txtProductionLine.Text;
		}
		/// <summary>
		/// - store all columns header caption for localize purpose
		/// - bind dataset to grid data source
		/// - restore all columns header caption
		/// </summary>
		/// <author>
		/// Do Manh Tuan
		/// 09-06-2005
		/// </author>
		private void BindDataToGrid()
		{
			const string METHOD_NAME = THIS + ".BindDataToGrid()";
			try
			{
				dstGridData = boWorkOrder.GetWODetailByMaster(voWorkOrderMaster.WorkOrderMasterID);
				dgrdData.DataSource = dstGridData.Tables[0];

				//restore the layout of grid
				FormControlComponents.RestoreGridLayout(dgrdData, dtbGridLayOut);
				
				//enable and disbale button
				btnAdd.Enabled = false;
				
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

		#region Grid Events

		/// <summary>
		/// When user move to another row of gird, we need to check
		/// status of this line
		/// - if status is un-release then allow user to change information
		/// - if status is released/mfg close/finance close then not allow user to chagne information
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void dgrdData_RowColChange(object sender, RowColChangeEventArgs e)
		{

		}

		/// <summary>
		/// before delete selected row, we need to check status of work order line
		/// if Status = unrelease then delete
		/// else cancel
		/// </summary>
		/// <param name="sender">TrueDBGrid</param>
		/// <param name="e">CancelEvent</param>
		private void dgrdData_BeforeDelete(object sender, CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".dgrdData_BeforeDelete()";
			try
			{
				//if WO's status is UnRealease
				if (dgrdData[dgrdData.Row, PRO_WorkOrderDetailTable.WORKORDERDETAILID_FLD].ToString() != string.Empty)
				{
					if ((int) WOLineStatus.Unreleased != int.Parse(dgrdData[dgrdData.Row, PRO_WorkOrderDetailTable.STATUS_FLD].ToString()))
					{
						//throw error message
						PCSMessageBox.Show(ErrorCode.MESSAGE_WO_CANNOT_DELWOLINE, MessageBoxIcon.Information);
						e.Cancel = true;
					}
					else
					{
						ArlWOLineID.Add(dgrdData[dgrdData.Row, PRO_WorkOrderDetailTable.WORKORDERDETAILID_FLD].ToString());
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
		/// - store value of cell before edit in order to restore if has error
		/// - we need to check status of current work order line
		/// if Status = unrelease then allow user to edit
		/// else lock column and cancel
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void dgrdData_BeforeColEdit(object sender, BeforeColEditEventArgs e)
		{
			const string METHOD_NAME = THIS + ".dgrdData_BeforeColEdit()";
			try
			{
				if (dgrdData[dgrdData.Row, PRO_WorkOrderDetailTable.WORKORDERDETAILID_FLD].ToString() != string.Empty)
				{
					if (dgrdData[dgrdData.Row, PRO_WorkOrderDetailTable.STATUS_FLD].ToString() != string.Empty)
					{
						if ((int) WOLineStatus.Unreleased != int.Parse(dgrdData[dgrdData.Row, PRO_WorkOrderDetailTable.STATUS_FLD].ToString()))
						{
							e.Cancel = true;
						}
					}
					if (e.Column.DataColumn.DataField == ITM_ProductTable.CODE_FLD
						|| e.Column.DataColumn.DataField == ITM_ProductTable.DESCRIPTION_FLD)
					{
						e.Cancel = true;
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
		/// after user end update data in cell, 
		/// we need to update related data column(s) if any.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void dgrdData_AfterColUpdate(object sender, ColEventArgs e)
		{
			const string METHOD_NAME = THIS + ".dgrdData_AfterColUpdate()";
			try
			{
				if (e.Column.DataColumn.DataField== ITM_ProductTable.CODE_FLD || e.Column.DataColumn.DataField== ITM_ProductTable.DESCRIPTION_FLD)
				{
					if ((e.Column.DataColumn.Tag == null) ||( e.Column.DataColumn.Value.ToString() == string.Empty))
					{
						int row = dgrdData.Row;

						//HACK: added by Tuan TQ. 23 May, 2006. Set value to Category column
						dgrdData[row, ITM_CategoryTable.TABLE_NAME +  ITM_CategoryTable.CODE_FLD] = string.Empty;
						//End hack

						dgrdData[row, PRO_WorkOrderDetailTable.LINE_FLD] = string.Empty;
						dgrdData[row, ITM_ProductTable.CODE_FLD] = string.Empty;
						dgrdData[row, ITM_ProductTable.DESCRIPTION_FLD] = string.Empty;
						dgrdData[row, ITM_ProductTable.REVISION_FLD] = string.Empty;
						dgrdData[row, PRO_WorkOrderDetailTable.PRODUCTID_FLD] = null;
						dgrdData[row, PRO_WorkOrderDetailTable.STOCKUMID_FLD] = null;
						dgrdData[row, PRO_WorkOrderDetailTable.AGC_FLD] = null;
						dgrdData[row, PRO_WorkOrderDetailTable.ESTCST_FLD] = string.Empty;
						dgrdData[row, PRO_WorkOrderDetailTable.AGC_FLD] = null;
						dgrdData[row, ITM_ProductTable.COSTMETHOD_FLD] = string.Empty;
						dgrdData[row, AGC_CODE] = string.Empty;
						dgrdData[row, UM] = string.Empty;
						dgrdData[row, STATUS_STRING] = string.Empty;
						dgrdData[row, PRO_WorkOrderDetailTable.STATUS_FLD] = string.Empty;
						dgrdData[row, PRO_WorkOrderDetailTable.WORKORDERMASTERID_FLD] = null;
						dgrdData[row, SALE_ORDER_CODE] = string.Empty;
						dgrdData[row, SO_SaleOrderDetailTable.SALEORDERLINE_FLD] = string.Empty;
						dgrdData[row, PRO_WorkOrderDetailTable.SALEORDERDETAILID_FLD] = null;
					}
					else
					{
						FillItemData((DataRow) e.Column.DataColumn.Tag);
						return;
					}
				}
				if (e.Column.DataColumn.DataField== SALE_ORDER_CODE)
				{
					if ((e.Column.DataColumn.Tag == null) || (dgrdData[dgrdData.Row, SALE_ORDER_CODE].ToString() == string.Empty))
					{
						int row = dgrdData.Row;
						dgrdData[row, PRO_WorkOrderDetailTable.SALEORDERMASTERID_FLD] = null;
						dgrdData[row, SALE_ORDER_CODE] = string.Empty;
						dgrdData[row, SO_SaleOrderDetailTable.SALEORDERLINE_FLD] = string.Empty;
						dgrdData[row, PRO_WorkOrderDetailTable.SALEORDERDETAILID_FLD] = null;
						return;
					}
					else
					{
						FillSaleOrderData((DataRow) e.Column.DataColumn.Tag);
					}
				}
				if ((e.Column.DataColumn.DataField== SO_SaleOrderDetailTable.SALEORDERLINE_FLD) )
				{
					if ((e.Column.DataColumn.Tag == null)|| (dgrdData[dgrdData.Row, SO_SaleOrderDetailTable.SALEORDERLINE_FLD].ToString() == string.Empty))
					{
						int row = dgrdData.Row;
						dgrdData[row, PRO_WorkOrderDetailTable.SALEORDERDETAILID_FLD] = string.Empty;
						dgrdData[row, SO_SaleOrderDetailTable.SALEORDERLINE_FLD] = null;
						return;
					}
					else
					{
						FillSOLineData((DataRow) e.Column.DataColumn.Tag);
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
		/// - get column name and opean search form for associated column
		/// - fill data to grid after user select a record
		/// </summary>
		/// <author>
		/// Do Manh Tuan
		/// Friday, Jun - 3 - 2005
		/// </author>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void dgrdData_ButtonClick(object sender, ColEventArgs e)
		{
			const string METHOD_NAME = THIS + ".dgrdData_ButtonClick()";
			try
			{
				DataRowView drwResult = null;
				Hashtable htbCondition= new Hashtable();
				if (!btnSave.Enabled) return;
				if (dgrdData[dgrdData.Row, PRO_WorkOrderDetailTable.WORKORDERDETAILID_FLD].ToString() != string.Empty)
				{
					if ((int) WOLineStatus.Unreleased != int.Parse(dgrdData[dgrdData.Row, PRO_WorkOrderDetailTable.STATUS_FLD].ToString()))
					{
						return;
					}
				}
				if ((dgrdData.Col == dgrdData.Columns.IndexOf(dgrdData.Columns[ITM_ProductTable.CODE_FLD])) && ((dgrdData[dgrdData.Row, PRO_WorkOrderDetailTable.WORKORDERDETAILID_FLD].ToString() == string.Empty)))
				{
					//open the search form to select Product
					htbCondition = new Hashtable();
					if (cboCCN.SelectedValue != null)
					{
						htbCondition.Add(ITM_ProductTable.MAKEITEM_FLD, 1);
						htbCondition.Add(ITM_ProductTable.CCNID_FLD, cboCCN.SelectedValue.ToString());
					}
					else
					{
						htbCondition.Add(ITM_ProductTable.CCNID_FLD, 0);
					}
					if (txtProductionLine.Text.Trim() != string.Empty)
					{
						htbCondition.Add(PRO_WorkOrderMasterTable.PRODUCTIONLINEID_FLD, int.Parse(txtProductionLine.Tag.ToString()));
					}
					else
					{
						String[] strParam = new string[2];
						strParam[0] = lblProductionLine.Text;
						strParam[1] = PRODUCT;
						PCSMessageBox.Show(ErrorCode.MESSAGE_SELECT_ONE_BEFORE_SELECT_ONE, MessageBoxIcon.Warning, strParam);
						txtProductionLine.Focus();
						return;
					}
					if (dgrdData.AddNewMode == AddNewModeEnum.AddNewCurrent)
					{
						drwResult = FormControlComponents.OpenSearchForm(PRODUCT_IN_PRODUCTIONLINE_VIEWNAME, ITM_ProductTable.CODE_FLD, dgrdData[dgrdData.Row, ITM_ProductTable.CODE_FLD].ToString(), htbCondition, true);
					}
					else
					{
						drwResult = FormControlComponents.OpenSearchForm(PRODUCT_IN_PRODUCTIONLINE_VIEWNAME, ITM_ProductTable.CODE_FLD, dgrdData.Columns[ITM_ProductTable.CODE_FLD].Text.Trim(), htbCondition, true);
					}
					if (drwResult != null)
					{
						FillItemData(drwResult.Row);
					}
				}

				if ((dgrdData.Col == dgrdData.Columns.IndexOf(dgrdData.Columns[ITM_ProductTable.DESCRIPTION_FLD])) && ((dgrdData[dgrdData.Row, PRO_WorkOrderDetailTable.WORKORDERDETAILID_FLD].ToString() == string.Empty)))
				{
					//open the search form to select Product
					htbCondition = new Hashtable();
					if (cboCCN.SelectedValue != null)
					{
						htbCondition.Add(ITM_ProductTable.MAKEITEM_FLD, 1);
						htbCondition.Add(ITM_ProductTable.CCNID_FLD, cboCCN.SelectedValue.ToString());
					}
					else
					{
						htbCondition.Add(ITM_ProductTable.CCNID_FLD, 0);
					}
					if (txtProductionLine.Text.Trim() != string.Empty)
					{
						htbCondition.Add(PRO_WorkOrderMasterTable.PRODUCTIONLINEID_FLD, int.Parse(txtProductionLine.Tag.ToString()));
					}
					else
					{
						String[] strParam = new string[2];
						strParam[0] = lblProductionLine.Text;
						strParam[1] = PRODUCT;
						PCSMessageBox.Show(ErrorCode.MESSAGE_SELECT_ONE_BEFORE_SELECT_ONE, MessageBoxIcon.Warning, strParam);
						txtProductionLine.Focus();
						return;
					}
					if (dgrdData.AddNewMode == AddNewModeEnum.AddNewCurrent)
					{
						drwResult = FormControlComponents.OpenSearchForm(PRODUCT_IN_PRODUCTIONLINE_VIEWNAME, ITM_ProductTable.DESCRIPTION_FLD, dgrdData[dgrdData.Row, ITM_ProductTable.DESCRIPTION_FLD].ToString(), htbCondition, true);
					}
					else
					{
						drwResult = FormControlComponents.OpenSearchForm(PRODUCT_IN_PRODUCTIONLINE_VIEWNAME, ITM_ProductTable.DESCRIPTION_FLD, dgrdData.Columns[ITM_ProductTable.DESCRIPTION_FLD].Text.Trim(), htbCondition, true);
					}
					if (drwResult != null)
					{
						FillItemData(drwResult.Row);
					}
				}

				//open the search form to select Sale Order Code
				if (dgrdData.Col == dgrdData.Columns.IndexOf(dgrdData.Columns[SALE_ORDER_CODE]))
				{
					if (ValidateDataForSaleOrder(0))
					{
						htbCondition = new Hashtable();
						htbCondition.Add(SO_SaleOrderMasterTable.CCNID_FLD, cboCCN.SelectedValue.ToString());
						htbCondition.Add(SO_SaleOrderMasterTable.SHIPFROMLOCID_FLD, voMasLoc.MasterLocationID);
						htbCondition.Add(ITM_ProductTable.PRODUCTID_FLD, dgrdData[dgrdData.Row, ITM_ProductTable.PRODUCTID_FLD].ToString());
						drwResult = FormControlComponents.OpenSearchForm(SALE_ORDER_FOR_WOLINE_VIEWNAME, SALE_ORDER_CODE,  dgrdData[dgrdData.Row, SALE_ORDER_CODE].ToString(),  htbCondition, true);
						if (drwResult != null)
						{
							FillSaleOrderData(drwResult.Row);
						}
					}
				}

				//open the search form to select Sale Order Line
				if (dgrdData.Col == dgrdData.Columns.IndexOf(dgrdData.Columns[SO_SaleOrderDetailTable.SALEORDERLINE_FLD]))
				{
					if (ValidateDataForSaleOrder(1))
					{
						htbCondition = new Hashtable();
						htbCondition.Add(SO_SaleOrderMasterTable.SALEORDERMASTERID_FLD, dgrdData[dgrdData.Row, SO_SaleOrderMasterTable.SALEORDERMASTERID_FLD].ToString());
						htbCondition.Add(SO_SaleOrderDetailTable.PRODUCTID_FLD, dgrdData[dgrdData.Row, ITM_ProductTable.PRODUCTID_FLD].ToString());

						drwResult = FormControlComponents.OpenSearchForm(SO_SaleOrderDetailTable.TABLE_NAME, SO_SaleOrderDetailTable.SALEORDERLINE_FLD,  dgrdData[dgrdData.Row, SO_SaleOrderDetailTable.SALEORDERLINE_FLD].ToString(),  htbCondition, true);
						if (drwResult != null)
						{
							FillSOLineData(drwResult.Row);
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
		/// after delete a row on grid, we need to update line cell
		/// </summary>
		/// <author>
		/// Do Manh Tuan
		/// 08-06-2005
		/// </author>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void dgrdData_AfterDelete(object sender, EventArgs e)
		{
			const string METHOD_NAME = THIS + ".dgrdData_AfterDelete()";
			try
			{
				for (int i = 0; i < dgrdData.RowCount; i++)
				{
					if (dgrdData[i, PRO_WorkOrderDetailTable.PRODUCTID_FLD].ToString() != string.Empty)
					{
						dgrdData[i, PRO_WorkOrderDetailTable.LINE_FLD] = i+1;
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

		private void dgrdData_Enter(object sender, EventArgs e)
		{
			const string METHOD_NAME = THIS + ".dgrdData_Enter()";
			try
			{
				if (dgrdData.Col == 0)
				{
					dgrdData.Row = 0;
					dgrdData.Col = dgrdData.Splits[0].DisplayColumns.IndexOf(dgrdData.Splits[0].DisplayColumns[ITM_ProductTable.CODE_FLD]);
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

		#endregion

		#region Change backcolor when focus or lost focus
		private void OnEnterControl(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ". OnEnterControl()";
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

		private void OnLeaveControl(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ". OnLeaveControl()";
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
		#endregion

		/// <summary>
		/// Reset all control if Master change
		/// </summary>
		private void ResetFormIfMasLocChanged()
		{
			const string METHOD_NAME = THIS + ".ResetFormIfMasLocChanged()";
			try
			{
				txtWONo.Text = string.Empty;
				txtDescription.Text = string.Empty;
				txtProductionLine.Text = string.Empty;
				txtProductionLine.Tag = null;
				dtmTransDate.Value = DBNull.Value;
				CreateDataSet();
				dgrdData.DataSource = dstGridData.Tables[0];
				FormControlComponents.RestoreGridLayout(dgrdData, dtbGridLayOut);
				FormMode = EnumAction.Default;
				SwitchFormMode();
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
		/// Reset all control if WO changed
		/// </summary>
		private void ResetFormIfWOChanged()
		{
			const string METHOD_NAME = THIS + ".";
			try
			{
				txtProductionLine.Text = string.Empty;
				txtProductionLine.Tag = null;
				txtDescription.Text = string.Empty;
				txtDCPCycle.Text = string.Empty;
				txtDCPCycle.Tag = null;
				txtDCPDescription.Text = string.Empty;
				dtmTransDate.Value = DBNull.Value;
				CreateDataSet();
				dgrdData.DataSource = dstGridData.Tables[0];
				FormControlComponents.RestoreGridLayout(dgrdData, dtbGridLayOut);
				FormMode = EnumAction.Default;
				SwitchFormMode();	
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
		/// Leave event:
		///		Open the search form if it's necessary
		/// </summary>
		/// <author>
		/// Do Manh Tuan
		/// Thursday, Jun - 2 - 2005
		/// </author>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void txtWONo_Leave(object sender, System.EventArgs e)
		{
			
		}

		/// <summary>
		/// Check data on grid
		/// </summary>
		/// <param name="sender"></param>
		/// <author>
		/// Do Manh Tuan
		/// 07 - 06 - 2005
		/// </author>
		/// <param name="e"></param>
		private void dgrdData_BeforeColUpdate(object sender, C1.Win.C1TrueDBGrid.BeforeColUpdateEventArgs e)
		{
			const string METHOD_NAME = THIS + ".dgrdData_BeforeColUpdate()";
			try
			{
				if (e.Column.DataColumn.Value.ToString() == string.Empty) return;
				string strCondition = string.Empty;
				Hashtable htbCriteria = new Hashtable();
				DataRowView drwResult = null;
				switch (e.Column.DataColumn.DataField)
				{
					case ITM_ProductTable.CODE_FLD:
						# region open Product search form 
						if (dgrdData.Columns[e.Column.DataColumn.DataField].Text.Trim() != string.Empty)
						{
							if (cboCCN.SelectedIndex >= 0)
							{
								htbCriteria.Add(MST_CCNTable.CCNID_FLD, cboCCN.SelectedValue.ToString());
								htbCriteria.Add(ITM_ProductTable.MAKEITEM_FLD, 1);
							}
							else
							{
								htbCriteria.Add(MST_CCNTable.CCNID_FLD, 0);
							}
							if (txtProductionLine.Text.Trim() != string.Empty)
							{
								htbCriteria.Add(PRO_WorkOrderMasterTable.PRODUCTIONLINEID_FLD, int.Parse(txtProductionLine.Tag.ToString()));
							}
							else
							{
								String[] strParam = new string[2];
								strParam[0] = lblProductionLine.Text;
								strParam[1] = PRODUCT;
								PCSMessageBox.Show(ErrorCode.MESSAGE_SELECT_ONE_BEFORE_SELECT_ONE, MessageBoxIcon.Warning, strParam);
								txtProductionLine.Focus();
								return;
							}
							drwResult = FormControlComponents.OpenSearchForm(PRODUCT_IN_PRODUCTIONLINE_VIEWNAME, ITM_ProductTable.CODE_FLD, dgrdData.Columns[e.Column.DataColumn.DataField].Text.Trim(), htbCriteria, false);
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
						# region open Product search form 
						if (dgrdData.Columns[e.Column.DataColumn.DataField].Text.Trim() != string.Empty)
						{
							if (cboCCN.SelectedIndex >= 0)
							{
								htbCriteria.Add(MST_CCNTable.CCNID_FLD, cboCCN.SelectedValue.ToString());
								htbCriteria.Add(ITM_ProductTable.MAKEITEM_FLD, 1);
							}
							else
							{
								htbCriteria.Add(MST_CCNTable.CCNID_FLD, 0);
							}
							if (txtProductionLine.Text.Trim() != string.Empty)
							{
								htbCriteria.Add(PRO_WorkOrderMasterTable.PRODUCTIONLINEID_FLD, int.Parse(txtProductionLine.Tag.ToString()));
							}
							else
							{
								String[] strParam = new string[2];
								strParam[0] = lblProductionLine.Text;
								strParam[1] = PRODUCT;
								PCSMessageBox.Show(ErrorCode.MESSAGE_SELECT_ONE_BEFORE_SELECT_ONE, MessageBoxIcon.Warning, strParam);
								txtProductionLine.Focus();
								return;
							}
							drwResult = FormControlComponents.OpenSearchForm(PRODUCT_IN_PRODUCTIONLINE_VIEWNAME, ITM_ProductTable.DESCRIPTION_FLD, dgrdData.Columns[e.Column.DataColumn.DataField].Text.Trim(), htbCriteria, false);
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
					case SALE_ORDER_CODE:
						#region open Sale Order search form
						if (dgrdData.Columns[e.Column.DataColumn.DataField].Text.Trim() != string.Empty)
						{
							if (ValidateDataForSaleOrder(0))
							{
								htbCriteria = new Hashtable();
								if (cboCCN.SelectedIndex >= 0)
								{
									htbCriteria.Add(MST_CCNTable.CCNID_FLD, cboCCN.SelectedValue.ToString());
								}
								else
								{
									htbCriteria.Add(MST_CCNTable.CCNID_FLD, 0);
								}
								htbCriteria.Add(ITM_ProductTable.PRODUCTID_FLD, dgrdData[dgrdData.Row, ITM_ProductTable.PRODUCTID_FLD].ToString());
								htbCriteria.Add(SO_SaleOrderMasterTable.SHIPFROMLOCID_FLD, voMasLoc.MasterLocationID);
								drwResult = FormControlComponents.OpenSearchForm(SALE_ORDER_FOR_WOLINE_VIEWNAME, SO_SaleOrderMasterTable.CODE_FLD, dgrdData.Columns[e.Column.DataColumn.DataField].Text.Trim(), htbCriteria, false);
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
						#endregion
						break;
					case SO_SaleOrderDetailTable.SALEORDERLINE_FLD:
						#region open Sale Order search form
						if (ValidateDataForSaleOrder(1))
						{
							htbCriteria = new Hashtable();
							if (dgrdData.Columns[e.Column.DataColumn.DataField].Text.Trim() != string.Empty)
							{
								htbCriteria.Add(SO_SaleOrderMasterTable.SALEORDERMASTERID_FLD, dgrdData[dgrdData.Row, PRO_WorkOrderDetailTable.SALEORDERMASTERID_FLD].ToString());
								drwResult = FormControlComponents.OpenSearchForm(SO_SaleOrderDetailTable.TABLE_NAME, SO_SaleOrderDetailTable.SALEORDERLINE_FLD, dgrdData.Columns[e.Column.DataColumn.DataField].Text.Trim(), htbCriteria, false);
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
						#endregion
						break;
				}

				//check quantity
				if (dgrdData.Splits[0].DisplayColumns[dgrdData.Col].DataColumn.DataField == PRO_WorkOrderDetailTable.ORDERQUANTITY_FLD)
				{
					try
					{
						float fQuantity = float.Parse(dgrdData.Splits[0].DisplayColumns[PRO_WorkOrderDetailTable.ORDERQUANTITY_FLD].DataColumn.Value.ToString().Trim());
						if (fQuantity < 0.0)
						{
							PCSMessageBox.Show(ErrorCode.MESSAGE_WO_ORDERQUANTITY, MessageBoxIcon.Error);
							e.Cancel = true;
						}
					}
					catch
					{
						//cancel update and throw PCSException
						e.Cancel = true;
						PCSMessageBox.Show(ErrorCode.MESSAGE_WO_ORDERQUANTITY, MessageBoxIcon.Error);
					}
				}
			
				//check start date
				if (e.Column.DataColumn.DataField == PRO_WorkOrderDetailTable.STARTDATE_FLD)
				{
					try
					{
						if (e.Column.DataColumn.Value.ToString() != string.Empty)
						{
							DateTime dtmStart = DateTime.Parse(dtmDate.Value.ToString());
							if (dgrdData[dgrdData.Row,PRO_WorkOrderDetailTable.DUEDATE_FLD].ToString() != string.Empty)
							{
								DateTime dtmDue = DateTime.Parse(dgrdData[dgrdData.Row,PRO_WorkOrderDetailTable.DUEDATE_FLD].ToString());
								if (dtmDue < dtmStart)
								{
									PCSMessageBox.Show(ErrorCode.MESSAGE_WO_STARTDATE_DUEDATE, MessageBoxIcon.Error);
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
				if (e.Column.DataColumn.DataField == PRO_WorkOrderDetailTable.DUEDATE_FLD)
				{
					try
					{
						if (e.Column.DataColumn.Value.ToString() != string.Empty)
						{
							DateTime dtmDue = DateTime.Parse(dtmDate.Value.ToString());
							if (dgrdData[dgrdData.Row,PRO_WorkOrderDetailTable.STARTDATE_FLD].ToString() != string.Empty)
							{
								DateTime dtmStart = DateTime.Parse(dgrdData[dgrdData.Row,PRO_WorkOrderDetailTable.STARTDATE_FLD].ToString());
								if (dtmDue < dtmStart)
								{
									PCSMessageBox.Show(ErrorCode.MESSAGE_WO_STARTDATE_DUEDATE, MessageBoxIcon.Error);
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
		/// txtDescription Leave event:
		///		Set focus to Part Number column in grid
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void txtDescription_Leave(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtDescription_Leave()";
			try
			{
				OnLeaveControl(sender, e);
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
		/// Closing event
		/// </summary>
		/// <author>
		/// Do Manh Tuan
		/// 09-06-2005
		/// </author>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void WorkOrder_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".WorkOrder_Closing()";
			try
			{
				if (FormMode != EnumAction.Default)
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
		/// Change column focus in the grid
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void dtmDate_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			const string METHOD_NAME = THIS + ".dtmDate_KeyDown()";
			try
			{
				if (e.KeyCode == Keys.Enter)
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
		/// Get fully datetime
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void dtmDate_DropDownClosed(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".dtmDate_DropDownClosed()";
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

		private void txtMasterLocation_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtMasterLocation_KeyDown()";
			try
			{
				if ((e.KeyCode == Keys.F4) && (btnSearchMasLoc.Enabled))
				{
					btnSearchMasLoc_Click(sender, e);
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

		private void txtWONo_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtWONo_KeyDown()";
			try
			{
				if ((e.KeyCode == Keys.F4) && (btnSearchWO.Enabled))
				{
					btnSearchWO_Click(sender, e);
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
		

		private void dtmDate_Enter(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".dtmDate_Enter()", FIRSTDATE = "01-01-0001";
			const int LENGTH = 10;
			try
			{
				if ((dtmDate.Text == string.Empty) || (dtmDate.Text.Substring(0, LENGTH)== FIRSTDATE))
				{
					dtmDate.Value = new UtilsBO().GetDBDate();
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

		private void dtmTransDate_DropDownClosed(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".dtmTransDate_DropDownClosed()";
			try
			{
				if (dtmTransDate.Text == string.Empty)
				{
					dtmTransDate.Value = DBNull.Value;
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

		private void txtMasterLocation_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtMasterLocation_Leave()";
			try
			{
				OnLeaveControl(sender, e);
				if (txtMasterLocation.Text == string.Empty)
				{
					if (voWorkOrderMaster.WorkOrderMasterID != 0)
					{
						voMasLoc.MasterLocationID =0;
						ResetFormIfMasLocChanged();
					}
					return;
				}
				if (!txtMasterLocation.Modified) return;
				Hashtable htbCriteria = new Hashtable();
				if (cboCCN.SelectedIndex != -1)
				{
					htbCriteria.Add(SO_SaleOrderMasterTable.CCNID_FLD, cboCCN.SelectedValue);
				}
				else
				{
					PCSMessageBox.Show(ErrorCode.MESSAGE_RGA_CCN, MessageBoxIcon.Warning);
					e.Cancel = true;
					return;
				}
				DataRowView drwResult = FormControlComponents.OpenSearchForm(MST_MasterLocationTable.TABLE_NAME, MST_MasterLocationTable.CODE_FLD, txtMasterLocation.Text.Trim(), htbCriteria, false);
				if (drwResult != null)
				{
					txtMasterLocation.Text = drwResult[MST_MasterLocationTable.CODE_FLD].ToString();
					voMasLoc.MasterLocationID = int.Parse(drwResult[MST_MasterLocationTable.MASTERLOCATIONID_FLD].ToString());
					if ((voMasLoc.MasterLocationID != voWorkOrderMaster.MasterLocationID)&& (voWorkOrderMaster.MasterLocationID != 0))
					{
						ResetFormIfMasLocChanged();
					}
				}
				else
					e.Cancel = true;
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
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION);
				}
			}
			catch(Exception ex)
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

		private void txtWONo_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtWONo_Validating()";
			try
			{
				if (mFormMode == EnumAction.Edit) return;
				if (mFormMode != EnumAction.Add)
				{
					if (txtWONo.Text == string.Empty)
					{
						if (voWorkOrderMaster.WorkOrderMasterID != 0)
						{
							voWorkOrderMaster.WorkOrderMasterID =0;
							ResetFormIfWOChanged();
						}
						return;
					}
					if (!txtWONo.Modified) return;
					Hashtable htbCondition = new Hashtable();
					if (txtMasterLocation.Text.Trim() != string.Empty)
					{
						htbCondition.Add(PRO_WorkOrderMasterTable.CCNID_FLD, cboCCN.SelectedValue);
						htbCondition.Add(PRO_WorkOrderMasterTable.MASTERLOCATIONID_FLD, voMasLoc.MasterLocationID);
					}
					else
					{
						PCSMessageBox.Show(ErrorCode.MESSAGE_RGA_MASTERLOCATION, MessageBoxIcon.Warning);
						e.Cancel = true;
						return;
					}
					DataRowView drwResult = FormControlComponents.OpenSearchForm(PRO_WorkOrderMasterTable.TABLE_NAME, PRO_WorkOrderMasterTable.WORKORDERNO_FLD, txtWONo.Text.Trim(), htbCondition, false);
					if (drwResult != null)
					{
						txtWONo.Text = drwResult[PRO_WorkOrderMasterTable.WORKORDERNO_FLD].ToString();
						txtDescription.Text = drwResult[PRO_WorkOrderMasterTable.DESCRIPTION_FLD].ToString();
						voWorkOrderMaster.WorkOrderMasterID = int.Parse(drwResult[PRO_WorkOrderMasterTable.WORKORDERMASTERID_FLD].ToString());
						voWorkOrderMaster.MasterLocationID = int.Parse(drwResult[PRO_WorkOrderMasterTable.MASTERLOCATIONID_FLD].ToString());
						voWorkOrderMaster.Description = drwResult[PRO_WorkOrderMasterTable.DESCRIPTION_FLD].ToString();
						FillWOData(drwResult.Row);
					}
					else 
						e.Cancel = true;
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
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION);
				}
			}
			catch(Exception ex)
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
		/// btnProductionLine_Click
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Wednesday, January 4 2006</date>
		private void btnProductionLine_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnProductionLine_Click()";
			try
			{
				// 28-04-2006 dungla: fix bug 3926 for NganNT 
				// Expected Results: Can't select other Production Line when has at lease one line has release.
				bool blnCanChangePro = CheckWOLines(WOLineStatus.Unreleased);
				if (!blnCanChangePro)
				{
					PCSMessageBox.Show(ErrorCode.MESSAGE_CANNOT_CHANGE_PRODUCTION_LINE, MessageBoxIcon.Warning);
					// restore last valid pro
					txtProductionLine.Text = strLastValidPro;
					return;
				}
				// 28-04-2006 dungla: fix bug 3926 for NganNT
				//Call OpenSearchForm for selecting Production Line
				DataRowView drwResult = FormControlComponents.OpenSearchForm(PRO_ProductionLineTable.TABLE_NAME, PRO_ProductionLineTable.CODE_FLD, txtProductionLine.Text.Trim(), null, true);
				
				//If has Production Line matched searching condition, fill values to form's controls
				if(drwResult != null)
				{
					txtProductionLine.Text = drwResult[PRO_ProductionLineTable.CODE_FLD].ToString();
					txtProductionLine.Tag = drwResult[PRO_ProductionLineTable.PRODUCTIONLINEID_FLD];
					// 17-04-2006 dungla: fix bug 3771 for NganNT: when user change production line, clear the grid
					if (!strLastValidPro.Equals(txtProductionLine.Text))
					{
						ChangeProductionLine();
						strLastValidPro = txtProductionLine.Text;
					}
					// 17-04-2006 dungla: fix bug 3771 for NganNT: when user change production line, clear the grid
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
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION);
				}
			}
			catch(Exception ex)
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
		/// txtProductionLine_Validating
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Wednesday, January 4 2006</date>
		private void txtProductionLine_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtProductionLine_Validating()";
			try
			{
				if (strLastValidPro.Equals(txtProductionLine.Text))
					return;
				// 28-04-2006 dungla: fix bug 3926 for NganNT 
				// Expected Results: Can't select other Production Line when has at lease one line has release.
				bool blnCanChangePro = CheckWOLines(WOLineStatus.Unreleased);
				if (!blnCanChangePro)
				{
					PCSMessageBox.Show(ErrorCode.MESSAGE_CANNOT_CHANGE_PRODUCTION_LINE, MessageBoxIcon.Warning);
					// restore last valid pro
					txtProductionLine.Text = strLastValidPro;
					return;
				}
				// 28-04-2006 dungla: fix bug 3926 for NganNT
				if (txtProductionLine.Text == string.Empty)
				{
					// 17-04-2006 dungla: fix bug 3771 for NganNT: when user change production line, clear the grid
					ChangeProductionLine();
					// 17-04-2006 dungla: fix bug 3771 for NganNT: when user change production line, clear the grid
					txtProductionLine.Tag = null;
					strLastValidPro = string.Empty;
					return;
				}
				//Call OpenSearchForm for selecting Production Line
				DataRowView drwResult = FormControlComponents.OpenSearchForm(PRO_ProductionLineTable.TABLE_NAME, PRO_ProductionLineTable.CODE_FLD, txtProductionLine.Text.Trim(), null, false);
				
				//If has Production Line matched searching condition, fill values to form's controls
				if(drwResult != null)
				{
					txtProductionLine.Text = drwResult[PRO_ProductionLineTable.CODE_FLD].ToString();
					txtProductionLine.Tag = drwResult[PRO_ProductionLineTable.PRODUCTIONLINEID_FLD];
					// 17-04-2006 dungla: fix bug 3771 for NganNT: when user change production line, clear the grid
					if (!strLastValidPro.Equals(txtProductionLine.Text))
					{
						ChangeProductionLine();
						strLastValidPro = txtProductionLine.Text;
					}
					// 17-04-2006 dungla: fix bug 3771 for NganNT: when user change production line, clear the grid
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
		/// txtProductionLine_KeyDown
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>wednesday, January 4 2006</date>
		private void txtProductionLine_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if (btnProductionLine.Enabled && e.KeyCode == Keys.F4)
			{
				btnProductionLine_Click(sender, new EventArgs());
			}
		}

		/// <summary>
		/// Show Work Order Report for each month in Work Order Line
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnPrint_Click(object sender, EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnPrint_Click()";
			const string TIME_FLD = "LineTime";
			const string PART_NUMBER_FLD = "PartsNumber";
			const string PART_NAME_FLD = "PartsName";
			const string MODEL_FLD = "Model";
			const string UNIT_FLD = "Unit";
			const string COL_PREFIX = "D";
			const string DATE_COL = "lblD";
			const string DAY_COL = "lblDayD";
			const string DAY_FORMAT = "00";
			const string MONTH_FORMAT = "MMM";
			const string SEPERATOR = "-";
			const string DIV_PREFIX = "div";
			const string DATA_PREFIX = "Data";
			const string FOOTER_PREFIX = "Ftr";
			const string TOTAL_FLD = "Total";
			const string LABEL_PREFIX = "lbl";
			const string FIELD_PREFIX = "fld";
			const double FIELD_WIDTH = 500;
			try
			{
				// get all months from due date of work order lines
				if (dstGridData != null && dstGridData.Tables.Count > 0)
				{
					// waiting cursor
					Cursor = Cursors.WaitCursor;
					ReportDefinitionFolder = Application.StartupPath + @"\ReportDefinition";
					if (!File.Exists(ReportDefinitionFolder + @"\" + ReportFileName))
					{
						PCSMessageBox.Show(ErrorCode.MESSAGE_REPORT_TEMPLATE_FILE_NOT_FOUND, MessageBoxIcon.Error);
						return;
					}
					DCPReportBO boReport = new DCPReportBO();
					DataTable dtbWorkingTime = boReport.GetWorkingTime();
					DataTable dtbOriginal = dstGridData.Tables[0].Copy();
					ArrayList arrMonths = new ArrayList();
					ArrayList arrProducts = new ArrayList();
					int intProductionLineID = 0;
					try
					{
						intProductionLineID = Convert.ToInt32(txtProductionLine.Tag);
					}
					catch{}
					DataTable dtbValidWorkDay = null;
					if (intProductionLineID > 0)
						dtbValidWorkDay = boReport.GetWorkingDateFromWCCapacity(intProductionLineID);
					foreach (DataRow drowData in dtbOriginal.Rows)
					{
						DateTime dtmDueDate = DateTime.MinValue;
						try
						{
							dtmDueDate = (DateTime)drowData[PRO_WorkOrderDetailTable.DUEDATE_FLD];
							dtmDueDate = new DateTime(dtmDueDate.Year, dtmDueDate.Month, 1);
						}
						catch{}
						if (dtmDueDate != DateTime.MinValue && !arrMonths.Contains(dtmDueDate))
							arrMonths.Add(dtmDueDate);
						int intProductID = int.Parse(drowData[ITM_ProductTable.PRODUCTID_FLD].ToString());
						if (!arrProducts.Contains(intProductID))
							arrProducts.Add(intProductID);
					}
					UtilsBO boUtils = new UtilsBO();
					foreach (DateTime dtmMonth in arrMonths)
					{
						ArrayList arrOffDay = null;
						ArrayList arrHolidays = null;
						if (intProductionLineID == 0)
						{
							arrOffDay = boUtils.GetWorkingDayByYear(dtmMonth.Year);
							arrHolidays = boUtils.GetHolidaysInYear(dtmMonth.Year);
						}
						string strMonthName = dtmMonth.ToString(MONTH_FORMAT);
						int intDayInMonth = DateTime.DaysInMonth(dtmMonth.Year, dtmMonth.Month);

						#region Build the report table

						DataTable dtbReportData = new DataTable(PRO_WorkOrderMasterTable.TABLE_NAME);
						//dtbReportData.Columns.Add(new DataColumn(PRO_WorkOrderDetailTable.LINE_FLD, typeof(int)));
						//dtbReportData.Columns.Add(new DataColumn(TIME_FLD, typeof(DateTime)));
						dtbReportData.Columns.Add(new DataColumn(PART_NUMBER_FLD, typeof(string)));
						dtbReportData.Columns.Add(new DataColumn(PART_NAME_FLD, typeof(string)));
						dtbReportData.Columns.Add(new DataColumn(MODEL_FLD, typeof(string)));
						dtbReportData.Columns.Add(new DataColumn(UNIT_FLD, typeof(string)));
						for (int i = 1; i <= intDayInMonth; i++)
						{
							dtbReportData.Columns.Add(new DataColumn(COL_PREFIX + i.ToString(DAY_FORMAT), typeof(decimal)));
							dtbReportData.Columns[COL_PREFIX + i.ToString(DAY_FORMAT)].AllowDBNull = true;
							dtbReportData.Columns[COL_PREFIX + i.ToString(DAY_FORMAT)].DefaultValue = DBNull.Value;
						}
						
						#endregion

						#region Copy data from original data to Report data

						foreach (int intProductID in arrProducts)
						{
							DataRow drowReport = dtbReportData.NewRow();
							bool blnHasValue = false;
							string strPartNumber = string.Empty;
							string strPartName = string.Empty;
							string strModel = string.Empty;
							string strUnit = string.Empty;
							// sum quantity by logical day (based on shift pattern)
							for (int i = 1; i <= intDayInMonth; i++)
							{
								string strColName = COL_PREFIX + i.ToString(DAY_FORMAT);
								DateTime dtmCurrentDay = new DateTime(dtmMonth.Year,  dtmMonth.Month, i);
								DateTime dtmStartTime = dtmMonth;
								DateTime dtmEndTime = dtmMonth;
								GetStartAndEndTime(dtmCurrentDay, ref dtmStartTime, ref dtmEndTime, dtbWorkingTime);
								string strExpression = ITM_ProductTable.PRODUCTID_FLD + "='" + intProductID + "' AND "
									+ PRO_WorkOrderDetailTable.DUEDATE_FLD + " >= '" + dtmStartTime.ToString("G") + "' AND "
									+ PRO_WorkOrderDetailTable.DUEDATE_FLD + " < '" + dtmEndTime.ToString("G") + "'";
								DataRow[] drowLines = dtbOriginal.Select(strExpression);
								decimal decQuantity = 0;
								if (drowLines.Length > 0)
								{
									try
									{
										decQuantity += Convert.ToDecimal(dtbOriginal.Compute("SUM(" + PRO_WorkOrderDetailTable.ORDERQUANTITY_FLD + ")", strExpression));
									}
									catch{}
									if (decQuantity != 0)
									{
										blnHasValue = true;
										strPartNumber = drowLines[0][ITM_ProductTable.CODE_FLD].ToString();
										strPartName = drowLines[0][ITM_ProductTable.DESCRIPTION_FLD].ToString();
										strModel = drowLines[0][ITM_ProductTable.REVISION_FLD].ToString();
										strUnit = drowLines[0][UM].ToString();
									}
									drowReport[strColName] = decQuantity;
								}
							}
							if (blnHasValue)
							{
								drowReport[PART_NUMBER_FLD] = strPartNumber;
								drowReport[PART_NAME_FLD] = strPartName;
								drowReport[MODEL_FLD] = strModel;
								drowReport[UNIT_FLD] = strUnit;
								dtbReportData.Rows.Add(drowReport);
							}
						}
						#endregion

						if (dtbReportData.Rows.Count == 0)
							continue;

						#region Rendering Report

						//ReportBuilder objRB = new ReportBuilder();
						C1Report rptWorkOrder = new C1Report();
						try
						{
							// TODO: get the report name from mstrReportName
							string[] arrstrReportInDefinitionFile = rptWorkOrder.GetReportInfo(
								ReportDefinitionFolder + "\\" + ReportFileName);
							rptWorkOrder.Load(ReportDefinitionFolder + "\\" + ReportFileName, arrstrReportInDefinitionFile[0]);
							arrstrReportInDefinitionFile = null;
						}
						catch
						{
							return;
						}
						rptWorkOrder.Layout.PaperSize = PaperKind.A3;
						string strTotal = string.Empty;

						#region Refine the heading

						for (int i = 1; i <= intDayInMonth; i++)
						{
							strTotal += "+ " + COL_PREFIX + i.ToString(DAY_FORMAT);
							string strColumnName = DATE_COL + i.ToString(DAY_FORMAT);
							string strDOWColName = DAY_COL + i.ToString(DAY_FORMAT);
							string strColHeading = i + SEPERATOR + strMonthName;
							DateTime dtmDay = new DateTime(dtmMonth.Year, dtmMonth.Month, i);
							string strDayOfWeek = dtmDay.DayOfWeek.ToString().Substring(0, 3);
							// draw heading
							try
							{
								rptWorkOrder.Fields[strColumnName].Text = strColHeading;
							}
							catch{}
							// draw day of week
							try
							{
								rptWorkOrder.Fields[strDOWColName].Text = strDayOfWeek;
							}
							catch{}
							if (intProductionLineID == 0)
							{
								if (arrOffDay.Contains(dtmDay.DayOfWeek) || arrHolidays.Contains(dtmDay))
								{
									try
									{
										if (dtmDay.DayOfWeek == DayOfWeek.Saturday)
										{
											rptWorkOrder.Fields[strColumnName].ForeColor = Color.Blue;
											rptWorkOrder.Fields[strColumnName].BackColor = Color.Yellow;
										}
										else
										{
											rptWorkOrder.Fields[strColumnName].ForeColor = Color.Red;
											rptWorkOrder.Fields[strColumnName].BackColor = Color.Yellow;
										}
									}
									catch{}
									try
									{
										if (dtmDay.DayOfWeek == DayOfWeek.Saturday)
										{
											rptWorkOrder.Fields[strDOWColName].ForeColor = Color.Blue;
											rptWorkOrder.Fields[strDOWColName].BackColor = Color.Yellow;
										}
										else
										{
											rptWorkOrder.Fields[strDOWColName].ForeColor = Color.Red;
											rptWorkOrder.Fields[strDOWColName].BackColor = Color.Yellow;
										}
									}
									catch{}
								}
							}
							else
							{
								string strExpression = "BeginDate <= '" + dtmDay.ToString("G") + "'"
									+ " AND EndDate >='" + dtmDay.ToString("G") + "'";
								DataRow[] drowValidWorkDay = dtbValidWorkDay.Select(strExpression);
								if (drowValidWorkDay.Length == 0)
								{
									try
									{
										if (dtmDay.DayOfWeek == DayOfWeek.Saturday)
										{
											rptWorkOrder.Fields[strColumnName].ForeColor = Color.Blue;
											rptWorkOrder.Fields[strColumnName].BackColor = Color.Yellow;
										}
										else
										{
											rptWorkOrder.Fields[strColumnName].ForeColor = Color.Red;
											rptWorkOrder.Fields[strColumnName].BackColor = Color.Yellow;
										}
									}
									catch{}
									try
									{
										if (dtmDay.DayOfWeek == DayOfWeek.Saturday)
										{
											rptWorkOrder.Fields[strDOWColName].ForeColor = Color.Blue;
											rptWorkOrder.Fields[strDOWColName].BackColor = Color.Yellow;
										}
										else
										{
											rptWorkOrder.Fields[strDOWColName].ForeColor = Color.Red;
											rptWorkOrder.Fields[strDOWColName].BackColor = Color.Yellow;
										}
									}
									catch{}
								}
							}
						}

						#endregion

						#region hide/show the related fields

						try
						{
							rptWorkOrder.Fields[FIELD_PREFIX + TOTAL_FLD].Text = strTotal.Remove(0, 1);
						}
						catch{}

						if (intDayInMonth < 31)
						{
							for (int i = intDayInMonth + 1; i <= 31; i++)
							{
								string strDateCol = DATE_COL + i.ToString(DAY_FORMAT);
								string strDayOfWeek = DAY_COL + i.ToString(DAY_FORMAT);
								string strDiv = DIV_PREFIX + i.ToString(DAY_FORMAT);
								string strDivDetail = DIV_PREFIX + DATA_PREFIX + i.ToString(DAY_FORMAT);
								string strDivFtr = DIV_PREFIX + FOOTER_PREFIX + i.ToString(DAY_FORMAT);
								string strDetail = FIELD_PREFIX + COL_PREFIX + i.ToString(DAY_FORMAT);
								string strFooter = FIELD_PREFIX + FOOTER_PREFIX + i.ToString(DAY_FORMAT);
								// hide the label first
								try
								{
									rptWorkOrder.Fields[strDateCol].Visible = false;
								}
								catch{}
								// hide the day of week
								try
								{
									rptWorkOrder.Fields[strDayOfWeek].Visible = false;
								}
								catch{}
								// hide the data field
								try
								{
									rptWorkOrder.Fields[strDetail].Visible = false;
								}
								catch{}
								// hide the footer
								try
								{
									rptWorkOrder.Fields[strFooter].Visible = false;
								}
								catch{}
								// hide the div
								try
								{
									rptWorkOrder.Fields[strDiv].Visible = false;
									rptWorkOrder.Fields[strDivDetail].Visible = false;
									rptWorkOrder.Fields[strDivFtr].Visible = false;
								}
								catch{}
							}
							try
							{
								#region Resize all line

								//double dWidth = rptReport.Fields["line1"].Width;
								for (int i = 1; i <= 5; i++)
									rptWorkOrder.Fields["line" + i].Width = rptWorkOrder.Fields["line" + i].Width - (31 - intDayInMonth)*FIELD_WIDTH;

								#endregion

								#region moving rest of field in report header

								double dWidthToChange = (31 - intDayInMonth)*FIELD_WIDTH;

								#region Total columns
								double dLeft = rptWorkOrder.Fields["lblD" + intDayInMonth.ToString("00")].Left + FIELD_WIDTH;
								rptWorkOrder.Fields["lblDayDTotal"].Left = rptWorkOrder.Fields["lblD" + intDayInMonth.ToString("00")].Left + FIELD_WIDTH;
								rptWorkOrder.Fields["fldTotal"].Left = rptWorkOrder.Fields["lblD" + intDayInMonth.ToString("00")].Left + FIELD_WIDTH;
								rptWorkOrder.Fields["lblTotal"].Left = rptWorkOrder.Fields["fldTotal"].Left;
								rptWorkOrder.Fields["fldFtrTotal"].Left = rptWorkOrder.Fields["lblD" + intDayInMonth.ToString("00")].Left + FIELD_WIDTH;
								rptWorkOrder.Fields["divTotal"].Left = rptWorkOrder.Fields["lblTotal"].Left + FIELD_WIDTH;
								rptWorkOrder.Fields["divDataTotal"].Left = rptWorkOrder.Fields["lblTotal"].Left + FIELD_WIDTH;
								rptWorkOrder.Fields["divFtrTotal"].Left = rptWorkOrder.Fields["lblTotal"].Left + FIELD_WIDTH;
								#endregion

								#endregion
							}
							catch{}
						}

						#endregion

						rptWorkOrder.DataSource.Recordset= dtbReportData;
						
						#endregion

						#region Modify the report layout

						const string CCN_FLD = "fldParameterCCN";
						const string MONTH_FLD = "fldMonth";
						const string YEAR_FLD = "fldYear";
						const string PRODUCTION_LINE_FLD = "fldProductionLine";
						const string WORK_ORDER_FLD = "fldParamWO";
						const string MAS_LOC_FLD = "fldParamMasLoc";
						const string DESCRIPTION_FLD = "fldDescription";

						try
						{
							rptWorkOrder.Fields[CCN_FLD].Text = cboCCN.SelectedText;
						}
						catch{}
						try
						{
							rptWorkOrder.Fields[MONTH_FLD].Text = dtmMonth.ToString("MMM");
						}
						catch{}
						try
						{
							rptWorkOrder.Fields[YEAR_FLD].Text = dtmMonth.Year.ToString();
						}
						catch{}
						try
						{
							rptWorkOrder.Fields[PRODUCTION_LINE_FLD].Text = txtProductionLine.Text;
						}
						catch{}
						try
						{
							rptWorkOrder.Fields[WORK_ORDER_FLD].Text = txtWONo.Text;
						}
						catch{}
						try
						{
							rptWorkOrder.Fields[MAS_LOC_FLD].Text = txtMasterLocation.Text;
						}
						catch{}
						try
						{
							if (txtDescription.Text.Trim() != string.Empty)
								rptWorkOrder.Fields[DESCRIPTION_FLD].Text = txtDescription.Text;
							else
							{
								rptWorkOrder.Fields[DESCRIPTION_FLD].Visible = false;
								rptWorkOrder.Fields[LABEL_PREFIX + PRO_WorkOrderMasterTable.DESCRIPTION_FLD].Visible = false;
								rptWorkOrder.Fields[DIV_PREFIX + PRO_WorkOrderMasterTable.DESCRIPTION_FLD].Visible = false;
							}
						}
						catch{}

						#endregion

						rptWorkOrder.Render();
						// and show it in preview dialog				
					    var printPreview = new PCSUtils.Framework.ReportFrame.C1PrintPreviewDialog
					                           {FormTitle = lblWorkOrderReport.Text};
					    printPreview.ReportViewer.PreviewNavigationPanel.Visible = false;
						printPreview.ReportViewer.PreviewPane.ZoomMode = ZoomModeEnum.PageWidth;
						printPreview.ReportViewer.Document = rptWorkOrder.Document;
						printPreview.Show();
					}
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
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION);
				}
			}
			catch(Exception ex)
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
				// restore cursor
				Cursor = Cursors.Default;
			}
		}
		/// <summary>
		/// Get working start time and end time of work center in a day
		/// </summary>
		/// <param name="pdtmCurrentDay">Current Day</param>
		/// <param name="pdtmStartTime">Start Time</param>
		/// <param name="pdtmEndTime">End Time</param>
		/// <param name="pdtmWorkingTime">Working Time</param>
		private void GetStartAndEndTime(DateTime pdtmCurrentDay, ref DateTime pdtmStartTime,
			ref DateTime pdtmEndTime, DataTable pdtmWorkingTime)
		{
			DataRow[] drowShifts = pdtmWorkingTime.Select(string.Empty, PRO_ShiftPatternTable.WORKTIMEFROM_FLD + " ASC");

			if (drowShifts.Length <= 0)
			{
				return;
			}
			//change shift configured day to working day
			pdtmStartTime = new DateTime(pdtmCurrentDay.Year, pdtmCurrentDay.Month, pdtmCurrentDay.Day,
				((DateTime)drowShifts[0][PRO_ShiftPatternTable.WORKTIMEFROM_FLD]).Hour,
				((DateTime)drowShifts[0][PRO_ShiftPatternTable.WORKTIMEFROM_FLD]).Minute,
				((DateTime)drowShifts[0][PRO_ShiftPatternTable.WORKTIMEFROM_FLD]).Second);
			pdtmEndTime = new DateTime(pdtmCurrentDay.Year, pdtmCurrentDay.Month, pdtmCurrentDay.Day,
				((DateTime)drowShifts[drowShifts.Length - 1][PRO_ShiftPatternTable.WORKTIMETO_FLD]).Hour,
				((DateTime)drowShifts[drowShifts.Length - 1][PRO_ShiftPatternTable.WORKTIMETO_FLD]).Minute,
				((DateTime)drowShifts[drowShifts.Length - 1][PRO_ShiftPatternTable.WORKTIMETO_FLD]).Second);
			double dblDiff = ((DateTime)drowShifts[drowShifts.Length - 1][PRO_ShiftPatternTable.WORKTIMETO_FLD]).
				Subtract((DateTime)drowShifts[0][PRO_ShiftPatternTable.WORKTIMEFROM_FLD]).Days;
			pdtmEndTime = pdtmEndTime.AddDays(dblDiff);
		}

		private void txtDCPCycle_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnDCPCycle_Click()";
			try
			{
				if (e.KeyCode == Keys.F4)
					btnDCPCycle_Click(btnDCPCycle, null);
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

		private void txtDCPCycle_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnDCPCycle_Click()";
			try
			{
				if (!txtDCPCycle.Modified)
					return;
				if (txtDCPCycle.Text == string.Empty)
				{
					// clear dcp cycle information
					// cycle
					txtDCPCycle.Text = string.Empty;
					txtDCPCycle.Tag = null;
					// description
					txtDCPDescription.Text = string.Empty;
					return;
				}
				DataRowView drwResult = null;
				Hashtable htbCondition = new Hashtable();
				
				if (cboCCN.SelectedValue != null)
					htbCondition.Add(PRO_DCOptionMasterTable.CCNID_FLD, cboCCN.SelectedValue.ToString());
				else
				{
					PCSMessageBox.Show(ErrorCode.MESSAGE_RGA_CCN, MessageBoxIcon.Warning);
					e.Cancel = true;
					return;
				}
				drwResult = FormControlComponents.OpenSearchForm(PRO_DCOptionMasterTable.TABLE_NAME, PRO_DCOptionMasterTable.CYCLE_FLD, txtDCPCycle.Text.Trim(), htbCondition, false);
				if (drwResult != null)
				{
					// cycle
					txtDCPCycle.Text = drwResult[PRO_DCOptionMasterTable.CYCLE_FLD].ToString();
					txtDCPCycle.Tag = drwResult[PRO_DCOptionMasterTable.DCOPTIONMASTERID_FLD];
					// description
					txtDCPDescription.Text = drwResult[PRO_DCOptionMasterTable.DESCRIPTION_FLD].ToString();
				}
				else
					txtDCPCycle.Focus();
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

		private void btnDCPCycle_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnDCPCycle_Click()";
			try
			{
				DataRowView drwResult = null;
				Hashtable htbCondition = new Hashtable();
				
				if (cboCCN.SelectedValue != null)
					htbCondition.Add(PRO_DCOptionMasterTable.CCNID_FLD, cboCCN.SelectedValue.ToString());
				else
				{
					PCSMessageBox.Show(ErrorCode.MESSAGE_RGA_CCN, MessageBoxIcon.Warning);
					cboCCN.Focus();
					return;
				}
				drwResult = FormControlComponents.OpenSearchForm(PRO_DCOptionMasterTable.TABLE_NAME, PRO_DCOptionMasterTable.CYCLE_FLD, txtDCPCycle.Text.Trim(), htbCondition, true);
				if (drwResult != null)
				{
					// cycle
					txtDCPCycle.Text = drwResult[PRO_DCOptionMasterTable.CYCLE_FLD].ToString();
					txtDCPCycle.Tag = drwResult[PRO_DCOptionMasterTable.DCOPTIONMASTERID_FLD];
					// description
					txtDCPDescription.Text = drwResult[PRO_DCOptionMasterTable.DESCRIPTION_FLD].ToString();
				}
				else
					txtMasterLocation.Focus();
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
		/// When user change production line, clear the grid
		/// </summary>
		/// <author>dungla</author>
		/// <date>17-04-2006</date>
		/// <purpose>fix bug 3771 for NganNT</purpose>
		private void ChangeProductionLine()
		{
//			foreach (DataRow drow in dstGridData.Tables[0].Rows)
//			{
//				ArlWOLineID.Add(dgrdData[dgrdData.Row, PRO_WorkOrderDetailTable.WORKORDERDETAILID_FLD].ToString());
//				drow.Delete();
//			}
			int intCount = dstGridData.Tables[0].Rows.Count;
			for (int i = intCount - 1; i >= 0; i--)
			{
				ArlWOLineID.Add(dgrdData[dgrdData.Row, PRO_WorkOrderDetailTable.WORKORDERDETAILID_FLD].ToString());
				dstGridData.Tables[0].Rows[i].Delete();
			}
			dgrdData.DataSource = dstGridData.Tables[0];
			FormControlComponents.RestoreGridLayout(dgrdData, dtbGridLayOut);
			ConfigGrid(false);
		}
	}

	
	public enum WOFormState
	{
		Normal = 1,
		CPONewWO = 2,
		CPOExistWO = 3
	}
}
