using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using C1.Win.C1TrueDBGrid;
using PCSComProduct.Items.BO;
using PCSComProduct.Items.DS;
using PCSComUtils.Admin.BO;
using PCSComUtils.Common;
using PCSComUtils.PCSExc;
using PCSUtils.Framework.TableFrame;
using PCSUtils.Log;
using PCSUtils.Utils;

namespace PCSProduct.Items
{
	/// <summary>
	/// Summary description for Routing.
	/// </summary>
	public class Routing : System.Windows.Forms.Form
	{
		#region My variables
		private const string THIS = "PCSProduct.Items.Routing";
		private const string INSIDE_PROCESS = "Inside Process";
		private const string OUTSIDE_PROCESS = "Outside Process";
		private const string PRODUCTION_LINE = "ProductionLine";
		private const string BOTH = "B";
		private const string LABOR = "L";
		private const string MACHINE = "M";
		private const int INSIDE = 0;
		private const int OUTSIDE = 1;
		private const string PERCEN_STYLE = "!90.^99";
		private const string DECIMAL_STYLE = "!9999990.^99";
		
		private int mProductID;
		public int ProductID
		{
			set{mProductID = value;}
			get{return mProductID;}
		}
		public string strViewRouting;
		private DataSet dstRouting = new DataSet();

		private ITM_ProductVO voProduct = new ITM_ProductVO();
		private EnumAction mFormAction = EnumAction.Default;
		public EnumAction FormAction
		{
			set{mFormAction = value;}
			get{return mFormAction;}
		}
		private bool blnIsChanged = false;
		private bool blnIsChangedRouting = false;
		object[] objControls;
		private string strMachineTabCaption; //= tabMachine.Text;
		private string strOutsideTabCaption; //= lblTabOutside.Text;

		private ITM_RoutingVO voRouting = new ITM_RoutingVO();
		private ITM_RoutingVO voOldRouting = new ITM_RoutingVO();
		//DataSet dstFunction;
		private bool blnIsLstSaveSuccess = true;
		#endregion My variables

		#region Private variable generate automatic
		private System.ComponentModel.IContainer components;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label lblIncremental;
		private System.Windows.Forms.Label lblRevision;
		private System.Windows.Forms.Button btnHelp;
		private System.Windows.Forms.Label label17;
		private System.Windows.Forms.Label label16;
		private System.Windows.Forms.Label label19;
		private System.Windows.Forms.Button btnFindItem;
		private System.Windows.Forms.Button btnLstEdit;
		private System.Windows.Forms.Button btnLstDelete;
		private System.Windows.Forms.Button btnLstAdd;
		private System.Windows.Forms.Button btnLstSave;
		private System.Windows.Forms.Button btnClose;
		private System.Windows.Forms.GroupBox grpRoutingGeneralInfo;
		private System.Windows.Forms.Button btnEdit;
		private System.Windows.Forms.Button btnSave;
		private System.Windows.Forms.Button btnDelete;
		private System.Windows.Forms.Button btnLstCancel;
		private System.Windows.Forms.Label label20;
		private System.Windows.Forms.Label label33;
		private C1.Win.C1List.C1Combo cboCCN;
		private C1.Win.C1Input.C1TextBox txtItem;
		private System.Windows.Forms.Button btnAdd;
		private System.Windows.Forms.TabControl tabRouting;
		private System.Windows.Forms.TabPage tabMachine;
		private System.Windows.Forms.ComboBox cboType;
		private System.Windows.Forms.Label lblValue2;
		private System.Windows.Forms.RadioButton radSchedSeq2;
		private System.Windows.Forms.RadioButton radOverlap2;
		private System.Windows.Forms.RadioButton radOverlapQty2;
		private System.Windows.Forms.Label label32;
		private System.Windows.Forms.Label label31;
		private System.Windows.Forms.Label label29;
		private System.Windows.Forms.Label label30;
		private C1.Win.C1Input.C1TextBox txtVendor;
		private System.Windows.Forms.Button btnVendor;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Panel pnlOutside;
		private System.Windows.Forms.Label lblTabOutside;
		private System.Windows.Forms.Panel pnlMachine;
		private System.Windows.Forms.Label lblValue;
		private System.Windows.Forms.RadioButton radSchedSeq;
		private System.Windows.Forms.RadioButton radOverlap;
		private System.Windows.Forms.RadioButton radOverlapQty;
		private C1.Win.C1List.C1Combo cboLabCC;
		private C1.Win.C1List.C1Combo cboMachCC;
		private System.Windows.Forms.Label lblMachine;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label lblRountingSetup;
		private System.Windows.Forms.Label label15;
		private System.Windows.Forms.Label lblMachRun;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.Label lblMachSetup;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.Label label13;
		private C1.Win.C1Input.C1NumericEdit txtStep;
		private C1.Win.C1Input.C1NumericEdit txtIncrement;
		private System.Windows.Forms.TextBox txtDescription;
		private System.Windows.Forms.TextBox txtRevision;
		private C1.Win.C1Input.C1NumericEdit txtMachSU;
		private C1.Win.C1Input.C1NumericEdit txtMachRun;
		private C1.Win.C1Input.C1NumericEdit txtMachines;
		private C1.Win.C1Input.C1NumericEdit txtSetupQty;
		private C1.Win.C1Input.C1NumericEdit txtMoveTime;
		private C1.Win.C1Input.C1NumericEdit txtLabSetup;
		private C1.Win.C1Input.C1NumericEdit txtLabRun;
		private C1.Win.C1Input.C1NumericEdit txtCrewSize;
		private C1.Win.C1Input.C1NumericEdit txtRunQty;
		private C1.Win.C1Input.C1NumericEdit txtTimeStudy;
		private C1.Win.C1Input.C1NumericEdit txtFixLeadTime2;
		private C1.Win.C1Input.C1NumericEdit txtVarLeadTime2;
		private C1.Win.C1Input.C1NumericEdit txtMoveTime2;
		private C1.Win.C1Input.C1NumericEdit txtCost;
		private C1.Win.C1Input.C1NumericEdit txtValue2;
		private C1.Win.C1Input.C1NumericEdit txtValue;
		private System.Windows.Forms.TextBox txtRoutDescription;
		private C1.Win.C1Input.C1DateEdit cboBeginDate;
		private C1.Win.C1Input.C1DateEdit cboEndDate;
		private System.Windows.Forms.Label lblPacer;
		private System.Windows.Forms.ComboBox cboPacer;
		private System.Windows.Forms.RadioButton radLinenear;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label21;
		private System.Windows.Forms.Label label22;
		private System.Windows.Forms.Label label23;
		private System.Windows.Forms.Label label24;
		private System.Windows.Forms.Label label25;
		private System.Windows.Forms.RadioButton radLinenear2;
		private C1.Win.C1TrueDBGrid.C1TrueDBGrid gridRouting;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label26;
		private System.Windows.Forms.Label label27;
		private System.Windows.Forms.Label lblMan;
		private System.Windows.Forms.Label lblFunctionName;
		private System.Windows.Forms.TextBox txtFunctionName;
		private System.Windows.Forms.TextBox txtFunction;
		private System.Windows.Forms.Button btnFunction;
		private System.Windows.Forms.Button btnWorkCenter;
		private System.Windows.Forms.TextBox txtWorkCenter;
		private System.Windows.Forms.Button btnStatus;
		private System.Windows.Forms.TextBox txtStatus;
		private System.Windows.Forms.Label lblProductionLine;
		private System.Windows.Forms.Button btnProductionLine;
		private System.Windows.Forms.TextBox txtProductionLine;
		private System.Windows.Forms.Label lblWorkCenter;
		private System.Windows.Forms.Label lblProductGroup;
		private System.Windows.Forms.Button btnProductGroup;
		private System.Windows.Forms.TextBox txtProductGroup;
		private System.Windows.Forms.Button btnCostCenterRate;
		private System.Windows.Forms.TextBox txtCostCenterRate;
		private System.Windows.Forms.Label lblCostCenterRate;
		private System.Windows.Forms.Label label34;
		#endregion Private variable generate automatic

		#region Windows Form Designer generated code
		public Routing()		
		{	
			InitializeComponent();
		}
		public Routing(int pintProductID)
		{	
			mProductID = pintProductID;
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

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(Routing));
			this.btnFindItem = new System.Windows.Forms.Button();
			this.txtItem = new C1.Win.C1Input.C1TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.lblIncremental = new System.Windows.Forms.Label();
			this.lblRevision = new System.Windows.Forms.Label();
			this.btnClose = new System.Windows.Forms.Button();
			this.btnHelp = new System.Windows.Forms.Button();
			this.btnEdit = new System.Windows.Forms.Button();
			this.btnSave = new System.Windows.Forms.Button();
			this.btnDelete = new System.Windows.Forms.Button();
			this.btnLstEdit = new System.Windows.Forms.Button();
			this.btnLstDelete = new System.Windows.Forms.Button();
			this.btnLstAdd = new System.Windows.Forms.Button();
			this.grpRoutingGeneralInfo = new System.Windows.Forms.GroupBox();
			this.btnStatus = new System.Windows.Forms.Button();
			this.txtStatus = new System.Windows.Forms.TextBox();
			this.btnWorkCenter = new System.Windows.Forms.Button();
			this.txtWorkCenter = new System.Windows.Forms.TextBox();
			this.btnFunction = new System.Windows.Forms.Button();
			this.txtFunction = new System.Windows.Forms.TextBox();
			this.txtFunctionName = new System.Windows.Forms.TextBox();
			this.lblFunctionName = new System.Windows.Forms.Label();
			this.txtStep = new C1.Win.C1Input.C1NumericEdit();
			this.cboType = new System.Windows.Forms.ComboBox();
			this.tabRouting = new System.Windows.Forms.TabControl();
			this.tabMachine = new System.Windows.Forms.TabPage();
			this.pnlMachine = new System.Windows.Forms.Panel();
			this.lblMan = new System.Windows.Forms.Label();
			this.label25 = new System.Windows.Forms.Label();
			this.label24 = new System.Windows.Forms.Label();
			this.label23 = new System.Windows.Forms.Label();
			this.label22 = new System.Windows.Forms.Label();
			this.label21 = new System.Windows.Forms.Label();
			this.radLinenear = new System.Windows.Forms.RadioButton();
			this.cboPacer = new System.Windows.Forms.ComboBox();
			this.txtValue = new C1.Win.C1Input.C1NumericEdit();
			this.txtTimeStudy = new C1.Win.C1Input.C1NumericEdit();
			this.txtRunQty = new C1.Win.C1Input.C1NumericEdit();
			this.txtCrewSize = new C1.Win.C1Input.C1NumericEdit();
			this.txtLabRun = new C1.Win.C1Input.C1NumericEdit();
			this.txtMoveTime = new C1.Win.C1Input.C1NumericEdit();
			this.txtSetupQty = new C1.Win.C1Input.C1NumericEdit();
			this.txtMachines = new C1.Win.C1Input.C1NumericEdit();
			this.txtMachRun = new C1.Win.C1Input.C1NumericEdit();
			this.lblValue = new System.Windows.Forms.Label();
			this.radSchedSeq = new System.Windows.Forms.RadioButton();
			this.radOverlap = new System.Windows.Forms.RadioButton();
			this.radOverlapQty = new System.Windows.Forms.RadioButton();
			this.cboLabCC = new C1.Win.C1List.C1Combo();
			this.cboMachCC = new C1.Win.C1List.C1Combo();
			this.lblMachine = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.label15 = new System.Windows.Forms.Label();
			this.lblMachRun = new System.Windows.Forms.Label();
			this.label11 = new System.Windows.Forms.Label();
			this.txtLabSetup = new C1.Win.C1Input.C1NumericEdit();
			this.txtMachSU = new C1.Win.C1Input.C1NumericEdit();
			this.label9 = new System.Windows.Forms.Label();
			this.lblPacer = new System.Windows.Forms.Label();
			this.lblMachSetup = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label14 = new System.Windows.Forms.Label();
			this.label12 = new System.Windows.Forms.Label();
			this.label13 = new System.Windows.Forms.Label();
			this.lblRountingSetup = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.pnlOutside = new System.Windows.Forms.Panel();
			this.label27 = new System.Windows.Forms.Label();
			this.label26 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.radLinenear2 = new System.Windows.Forms.RadioButton();
			this.txtValue2 = new C1.Win.C1Input.C1NumericEdit();
			this.txtCost = new C1.Win.C1Input.C1NumericEdit();
			this.txtMoveTime2 = new C1.Win.C1Input.C1NumericEdit();
			this.txtVarLeadTime2 = new C1.Win.C1Input.C1NumericEdit();
			this.txtFixLeadTime2 = new C1.Win.C1Input.C1NumericEdit();
			this.lblValue2 = new System.Windows.Forms.Label();
			this.radSchedSeq2 = new System.Windows.Forms.RadioButton();
			this.radOverlap2 = new System.Windows.Forms.RadioButton();
			this.radOverlapQty2 = new System.Windows.Forms.RadioButton();
			this.label32 = new System.Windows.Forms.Label();
			this.label31 = new System.Windows.Forms.Label();
			this.label29 = new System.Windows.Forms.Label();
			this.label30 = new System.Windows.Forms.Label();
			this.txtVendor = new C1.Win.C1Input.C1TextBox();
			this.btnVendor = new System.Windows.Forms.Button();
			this.label8 = new System.Windows.Forms.Label();
			this.label20 = new System.Windows.Forms.Label();
			this.lblWorkCenter = new System.Windows.Forms.Label();
			this.label17 = new System.Windows.Forms.Label();
			this.label16 = new System.Windows.Forms.Label();
			this.label19 = new System.Windows.Forms.Label();
			this.cboEndDate = new C1.Win.C1Input.C1DateEdit();
			this.cboBeginDate = new C1.Win.C1Input.C1DateEdit();
			this.label34 = new System.Windows.Forms.Label();
			this.label33 = new System.Windows.Forms.Label();
			this.btnProductionLine = new System.Windows.Forms.Button();
			this.txtProductionLine = new System.Windows.Forms.TextBox();
			this.lblProductionLine = new System.Windows.Forms.Label();
			this.btnLstSave = new System.Windows.Forms.Button();
			this.btnLstCancel = new System.Windows.Forms.Button();
			this.cboCCN = new C1.Win.C1List.C1Combo();
			this.btnAdd = new System.Windows.Forms.Button();
			this.lblTabOutside = new System.Windows.Forms.Label();
			this.txtIncrement = new C1.Win.C1Input.C1NumericEdit();
			this.txtDescription = new System.Windows.Forms.TextBox();
			this.txtRevision = new System.Windows.Forms.TextBox();
			this.txtRoutDescription = new System.Windows.Forms.TextBox();
			this.gridRouting = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
			this.lblProductGroup = new System.Windows.Forms.Label();
			this.btnProductGroup = new System.Windows.Forms.Button();
			this.txtProductGroup = new System.Windows.Forms.TextBox();
			this.btnCostCenterRate = new System.Windows.Forms.Button();
			this.txtCostCenterRate = new System.Windows.Forms.TextBox();
			this.lblCostCenterRate = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.txtItem)).BeginInit();
			this.grpRoutingGeneralInfo.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.txtStep)).BeginInit();
			this.tabRouting.SuspendLayout();
			this.tabMachine.SuspendLayout();
			this.pnlMachine.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.txtValue)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.txtTimeStudy)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.txtRunQty)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.txtCrewSize)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.txtLabRun)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.txtMoveTime)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.txtSetupQty)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.txtMachines)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.txtMachRun)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.cboLabCC)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.cboMachCC)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.txtLabSetup)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.txtMachSU)).BeginInit();
			this.pnlOutside.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.txtValue2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.txtCost)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.txtMoveTime2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.txtVarLeadTime2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.txtFixLeadTime2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.txtVendor)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.cboEndDate)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.cboBeginDate)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.cboCCN)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.txtIncrement)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gridRouting)).BeginInit();
			this.SuspendLayout();
			// 
			// btnFindItem
			// 
			this.btnFindItem.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnFindItem.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnFindItem.Location = new System.Drawing.Point(226, 6);
			this.btnFindItem.Name = "btnFindItem";
			this.btnFindItem.Size = new System.Drawing.Size(21, 20);
			this.btnFindItem.TabIndex = 4;
			this.btnFindItem.Text = "...";
			this.btnFindItem.Click += new System.EventHandler(this.btnFindItem_Click);
			// 
			// txtItem
			// 
			this.txtItem.Location = new System.Drawing.Point(91, 6);
			this.txtItem.Name = "txtItem";
			this.txtItem.ReadOnly = true;
			this.txtItem.Size = new System.Drawing.Size(134, 20);
			this.txtItem.TabIndex = 3;
			this.txtItem.Tag = null;
			this.txtItem.TextDetached = true;
			// 
			// label1
			// 
			this.label1.ForeColor = System.Drawing.Color.Maroon;
			this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.label1.Location = new System.Drawing.Point(3, 30);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(89, 18);
			this.label1.TabIndex = 7;
			this.label1.Text = "Part name";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label2
			// 
			this.label2.ForeColor = System.Drawing.Color.Maroon;
			this.label2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.label2.Location = new System.Drawing.Point(3, 6);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(89, 18);
			this.label2.TabIndex = 2;
			this.label2.Text = "Part number";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label3
			// 
			this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label3.ForeColor = System.Drawing.Color.Maroon;
			this.label3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.label3.Location = new System.Drawing.Point(624, 7);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(32, 19);
			this.label3.TabIndex = 0;
			this.label3.Text = "CCN";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label4
			// 
			this.label4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.label4.Location = new System.Drawing.Point(3, 53);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(89, 18);
			this.label4.TabIndex = 11;
			this.label4.Text = "Rout Description";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblIncremental
			// 
			this.lblIncremental.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblIncremental.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lblIncremental.Location = new System.Drawing.Point(364, 32);
			this.lblIncremental.Name = "lblIncremental";
			this.lblIncremental.Size = new System.Drawing.Size(58, 18);
			this.lblIncremental.TabIndex = 9;
			this.lblIncremental.Text = "Increment";
			this.lblIncremental.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblRevision
			// 
			this.lblRevision.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblRevision.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lblRevision.Location = new System.Drawing.Point(260, 7);
			this.lblRevision.Name = "lblRevision";
			this.lblRevision.Size = new System.Drawing.Size(39, 18);
			this.lblRevision.TabIndex = 5;
			this.lblRevision.Text = "Model";
			this.lblRevision.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// btnClose
			// 
			this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnClose.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnClose.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnClose.Location = new System.Drawing.Point(674, 461);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(60, 24);
			this.btnClose.TabIndex = 39;
			this.btnClose.Text = "&Close";
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			// 
			// btnHelp
			// 
			this.btnHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnHelp.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnHelp.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnHelp.Location = new System.Drawing.Point(613, 461);
			this.btnHelp.Name = "btnHelp";
			this.btnHelp.Size = new System.Drawing.Size(60, 24);
			this.btnHelp.TabIndex = 38;
			this.btnHelp.Text = "&Help";
			this.btnHelp.Click += new System.EventHandler(this.btnHelp_Click);
			// 
			// btnEdit
			// 
			this.btnEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnEdit.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnEdit.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnEdit.Location = new System.Drawing.Point(126, 461);
			this.btnEdit.Name = "btnEdit";
			this.btnEdit.Size = new System.Drawing.Size(60, 24);
			this.btnEdit.TabIndex = 27;
			this.btnEdit.Text = "&Edit";
			this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
			// 
			// btnSave
			// 
			this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnSave.Enabled = false;
			this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnSave.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnSave.Location = new System.Drawing.Point(65, 461);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(60, 24);
			this.btnSave.TabIndex = 26;
			this.btnSave.Text = "&Save";
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			// 
			// btnDelete
			// 
			this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnDelete.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnDelete.Location = new System.Drawing.Point(187, 461);
			this.btnDelete.Name = "btnDelete";
			this.btnDelete.Size = new System.Drawing.Size(60, 24);
			this.btnDelete.TabIndex = 28;
			this.btnDelete.Text = "&Delete";
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
			// 
			// btnLstEdit
			// 
			this.btnLstEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnLstEdit.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnLstEdit.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F);
			this.btnLstEdit.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnLstEdit.Location = new System.Drawing.Point(604, 438);
			this.btnLstEdit.Name = "btnLstEdit";
			this.btnLstEdit.Size = new System.Drawing.Size(42, 20);
			this.btnLstEdit.TabIndex = 35;
			this.btnLstEdit.Text = "Edit";
			this.btnLstEdit.Click += new System.EventHandler(this.btnLstEdit_Click);
			// 
			// btnLstDelete
			// 
			this.btnLstDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnLstDelete.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnLstDelete.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F);
			this.btnLstDelete.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnLstDelete.Location = new System.Drawing.Point(646, 438);
			this.btnLstDelete.Name = "btnLstDelete";
			this.btnLstDelete.Size = new System.Drawing.Size(42, 20);
			this.btnLstDelete.TabIndex = 36;
			this.btnLstDelete.Text = "Delete";
			this.btnLstDelete.Click += new System.EventHandler(this.btnLstDelete_Click);
			// 
			// btnLstAdd
			// 
			this.btnLstAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnLstAdd.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnLstAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F);
			this.btnLstAdd.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnLstAdd.Location = new System.Drawing.Point(520, 438);
			this.btnLstAdd.Name = "btnLstAdd";
			this.btnLstAdd.Size = new System.Drawing.Size(42, 20);
			this.btnLstAdd.TabIndex = 33;
			this.btnLstAdd.Text = "Add";
			this.btnLstAdd.Click += new System.EventHandler(this.btnLstAdd_Click);
			// 
			// grpRoutingGeneralInfo
			// 
			this.grpRoutingGeneralInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.grpRoutingGeneralInfo.Controls.Add(this.btnStatus);
			this.grpRoutingGeneralInfo.Controls.Add(this.txtStatus);
			this.grpRoutingGeneralInfo.Controls.Add(this.btnWorkCenter);
			this.grpRoutingGeneralInfo.Controls.Add(this.txtWorkCenter);
			this.grpRoutingGeneralInfo.Controls.Add(this.btnFunction);
			this.grpRoutingGeneralInfo.Controls.Add(this.txtFunction);
			this.grpRoutingGeneralInfo.Controls.Add(this.txtFunctionName);
			this.grpRoutingGeneralInfo.Controls.Add(this.lblFunctionName);
			this.grpRoutingGeneralInfo.Controls.Add(this.txtStep);
			this.grpRoutingGeneralInfo.Controls.Add(this.cboType);
			this.grpRoutingGeneralInfo.Controls.Add(this.tabRouting);
			this.grpRoutingGeneralInfo.Controls.Add(this.label20);
			this.grpRoutingGeneralInfo.Controls.Add(this.lblWorkCenter);
			this.grpRoutingGeneralInfo.Controls.Add(this.label17);
			this.grpRoutingGeneralInfo.Controls.Add(this.label16);
			this.grpRoutingGeneralInfo.Controls.Add(this.label19);
			this.grpRoutingGeneralInfo.Location = new System.Drawing.Point(298, 102);
			this.grpRoutingGeneralInfo.Name = "grpRoutingGeneralInfo";
			this.grpRoutingGeneralInfo.Size = new System.Drawing.Size(438, 334);
			this.grpRoutingGeneralInfo.TabIndex = 23;
			this.grpRoutingGeneralInfo.TabStop = false;
			// 
			// btnStatus
			// 
			this.btnStatus.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnStatus.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnStatus.Location = new System.Drawing.Point(155, 55);
			this.btnStatus.Name = "btnStatus";
			this.btnStatus.Size = new System.Drawing.Size(21, 20);
			this.btnStatus.TabIndex = 6;
			this.btnStatus.Text = "...";
			this.btnStatus.Click += new System.EventHandler(this.btnStatus_Click);
			// 
			// txtStatus
			// 
			this.txtStatus.Location = new System.Drawing.Point(58, 55);
			this.txtStatus.Name = "txtStatus";
			this.txtStatus.Size = new System.Drawing.Size(96, 20);
			this.txtStatus.TabIndex = 5;
			this.txtStatus.Text = "";
			this.txtStatus.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtStatus_KeyDown);
			this.txtStatus.Validating += new System.ComponentModel.CancelEventHandler(this.txtStatus_Validating);
			// 
			// btnWorkCenter
			// 
			this.btnWorkCenter.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnWorkCenter.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnWorkCenter.Location = new System.Drawing.Point(408, 11);
			this.btnWorkCenter.Name = "btnWorkCenter";
			this.btnWorkCenter.Size = new System.Drawing.Size(21, 20);
			this.btnWorkCenter.TabIndex = 12;
			this.btnWorkCenter.Text = "...";
			this.btnWorkCenter.Click += new System.EventHandler(this.btnWorkCenter_Click);
			// 
			// txtWorkCenter
			// 
			this.txtWorkCenter.Location = new System.Drawing.Point(310, 11);
			this.txtWorkCenter.Name = "txtWorkCenter";
			this.txtWorkCenter.Size = new System.Drawing.Size(96, 20);
			this.txtWorkCenter.TabIndex = 11;
			this.txtWorkCenter.Text = "";
			this.txtWorkCenter.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtWorkCenter_KeyDown);
			this.txtWorkCenter.Validating += new System.ComponentModel.CancelEventHandler(this.txtWorkCenter_Validating);
			// 
			// btnFunction
			// 
			this.btnFunction.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnFunction.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnFunction.Location = new System.Drawing.Point(408, 32);
			this.btnFunction.Name = "btnFunction";
			this.btnFunction.Size = new System.Drawing.Size(21, 20);
			this.btnFunction.TabIndex = 15;
			this.btnFunction.Text = "...";
			this.btnFunction.Click += new System.EventHandler(this.btnFunction_Click);
			// 
			// txtFunction
			// 
			this.txtFunction.Location = new System.Drawing.Point(310, 33);
			this.txtFunction.Name = "txtFunction";
			this.txtFunction.Size = new System.Drawing.Size(96, 20);
			this.txtFunction.TabIndex = 14;
			this.txtFunction.Text = "";
			this.txtFunction.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtFunction_KeyDown);
			this.txtFunction.Validating += new System.ComponentModel.CancelEventHandler(this.txtFunction_Validating);
			// 
			// txtFunctionName
			// 
			this.txtFunctionName.Location = new System.Drawing.Point(310, 55);
			this.txtFunctionName.Name = "txtFunctionName";
			this.txtFunctionName.ReadOnly = true;
			this.txtFunctionName.Size = new System.Drawing.Size(98, 20);
			this.txtFunctionName.TabIndex = 17;
			this.txtFunctionName.Text = "";
			// 
			// lblFunctionName
			// 
			this.lblFunctionName.Location = new System.Drawing.Point(216, 55);
			this.lblFunctionName.Name = "lblFunctionName";
			this.lblFunctionName.TabIndex = 16;
			this.lblFunctionName.Text = "Function Name";
			// 
			// txtStep
			// 
			this.txtStep.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.txtStep.AutoSize = false;
			this.txtStep.CustomFormat = "###";
			this.txtStep.DataType = typeof(System.Byte);
			this.txtStep.ErrorInfo.ShowErrorMessage = false;
			this.txtStep.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
			this.txtStep.Location = new System.Drawing.Point(58, 10);
			this.txtStep.MaxLength = 10;
			this.txtStep.Name = "txtStep";
			this.txtStep.NumericInputKeys = C1.Win.C1Input.NumericInputKeyFlags.F9;
			this.txtStep.Size = new System.Drawing.Size(40, 20);
			this.txtStep.TabIndex = 1;
			this.txtStep.Tag = null;
			this.txtStep.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.txtStep.Value = ((System.Byte)(0));
			this.txtStep.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.None;
			this.txtStep.Enter += new System.EventHandler(this.OnEnterControl);
			this.txtStep.Leave += new System.EventHandler(this.OnLeaveControl);
			// 
			// cboType
			// 
			this.cboType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cboType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboType.ItemHeight = 13;
			this.cboType.Location = new System.Drawing.Point(58, 32);
			this.cboType.Name = "cboType";
			this.cboType.Size = new System.Drawing.Size(118, 21);
			this.cboType.TabIndex = 3;
			this.cboType.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cboControl_Keydown);
			this.cboType.SelectedIndexChanged += new System.EventHandler(this.cboType_SelectedIndexChanged);
			this.cboType.Leave += new System.EventHandler(this.OnLeaveControl);
			this.cboType.Enter += new System.EventHandler(this.OnEnterControl);
			// 
			// tabRouting
			// 
			this.tabRouting.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.tabRouting.Controls.Add(this.tabMachine);
			this.tabRouting.ItemSize = new System.Drawing.Size(111, 18);
			this.tabRouting.Location = new System.Drawing.Point(6, 80);
			this.tabRouting.Name = "tabRouting";
			this.tabRouting.SelectedIndex = 0;
			this.tabRouting.Size = new System.Drawing.Size(424, 237);
			this.tabRouting.TabIndex = 15;
			// 
			// tabMachine
			// 
			this.tabMachine.Controls.Add(this.pnlMachine);
			this.tabMachine.Controls.Add(this.pnlOutside);
			this.tabMachine.Location = new System.Drawing.Point(4, 22);
			this.tabMachine.Name = "tabMachine";
			this.tabMachine.Size = new System.Drawing.Size(416, 211);
			this.tabMachine.TabIndex = 0;
			this.tabMachine.Text = "Machine & Labor Data";
			// 
			// pnlMachine
			// 
			this.pnlMachine.Controls.Add(this.lblMan);
			this.pnlMachine.Controls.Add(this.label25);
			this.pnlMachine.Controls.Add(this.label24);
			this.pnlMachine.Controls.Add(this.label23);
			this.pnlMachine.Controls.Add(this.label22);
			this.pnlMachine.Controls.Add(this.label21);
			this.pnlMachine.Controls.Add(this.radLinenear);
			this.pnlMachine.Controls.Add(this.cboPacer);
			this.pnlMachine.Controls.Add(this.txtValue);
			this.pnlMachine.Controls.Add(this.txtTimeStudy);
			this.pnlMachine.Controls.Add(this.txtRunQty);
			this.pnlMachine.Controls.Add(this.txtCrewSize);
			this.pnlMachine.Controls.Add(this.txtLabRun);
			this.pnlMachine.Controls.Add(this.txtMoveTime);
			this.pnlMachine.Controls.Add(this.txtSetupQty);
			this.pnlMachine.Controls.Add(this.txtMachines);
			this.pnlMachine.Controls.Add(this.txtMachRun);
			this.pnlMachine.Controls.Add(this.lblValue);
			this.pnlMachine.Controls.Add(this.radSchedSeq);
			this.pnlMachine.Controls.Add(this.radOverlap);
			this.pnlMachine.Controls.Add(this.radOverlapQty);
			this.pnlMachine.Controls.Add(this.cboLabCC);
			this.pnlMachine.Controls.Add(this.cboMachCC);
			this.pnlMachine.Controls.Add(this.lblMachine);
			this.pnlMachine.Controls.Add(this.label10);
			this.pnlMachine.Controls.Add(this.label15);
			this.pnlMachine.Controls.Add(this.lblMachRun);
			this.pnlMachine.Controls.Add(this.label11);
			this.pnlMachine.Controls.Add(this.txtLabSetup);
			this.pnlMachine.Controls.Add(this.txtMachSU);
			this.pnlMachine.Controls.Add(this.label9);
			this.pnlMachine.Controls.Add(this.lblPacer);
			this.pnlMachine.Controls.Add(this.lblMachSetup);
			this.pnlMachine.Controls.Add(this.label6);
			this.pnlMachine.Controls.Add(this.label14);
			this.pnlMachine.Controls.Add(this.label12);
			this.pnlMachine.Controls.Add(this.label13);
			this.pnlMachine.Controls.Add(this.lblRountingSetup);
			this.pnlMachine.Controls.Add(this.label5);
			this.pnlMachine.Location = new System.Drawing.Point(0, 0);
			this.pnlMachine.Name = "pnlMachine";
			this.pnlMachine.Size = new System.Drawing.Size(454, 228);
			this.pnlMachine.TabIndex = 0;
			this.pnlMachine.Visible = false;
			this.pnlMachine.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlMachine_Paint);
			// 
			// lblMan
			// 
			this.lblMan.Location = new System.Drawing.Point(378, 28);
			this.lblMan.Name = "lblMan";
			this.lblMan.Size = new System.Drawing.Size(40, 20);
			this.lblMan.TabIndex = 40;
			this.lblMan.Text = "(man)";
			// 
			// label25
			// 
			this.label25.ForeColor = System.Drawing.SystemColors.ControlText;
			this.label25.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.label25.Location = new System.Drawing.Point(378, 118);
			this.label25.Name = "label25";
			this.label25.Size = new System.Drawing.Size(18, 18);
			this.label25.TabIndex = 39;
			this.label25.Text = "(s)";
			this.label25.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label24
			// 
			this.label24.ForeColor = System.Drawing.SystemColors.ControlText;
			this.label24.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.label24.Location = new System.Drawing.Point(378, 96);
			this.label24.Name = "label24";
			this.label24.Size = new System.Drawing.Size(18, 18);
			this.label24.TabIndex = 38;
			this.label24.Text = "(s)";
			this.label24.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label23
			// 
			this.label23.ForeColor = System.Drawing.SystemColors.ControlText;
			this.label23.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.label23.Location = new System.Drawing.Point(378, 6);
			this.label23.Name = "label23";
			this.label23.Size = new System.Drawing.Size(18, 18);
			this.label23.TabIndex = 37;
			this.label23.Text = "(s)";
			this.label23.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label22
			// 
			this.label22.ForeColor = System.Drawing.SystemColors.ControlText;
			this.label22.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.label22.Location = new System.Drawing.Point(182, 118);
			this.label22.Name = "label22";
			this.label22.Size = new System.Drawing.Size(18, 18);
			this.label22.TabIndex = 36;
			this.label22.Text = "(s)";
			this.label22.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label21
			// 
			this.label21.ForeColor = System.Drawing.SystemColors.ControlText;
			this.label21.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.label21.Location = new System.Drawing.Point(182, 94);
			this.label21.Name = "label21";
			this.label21.Size = new System.Drawing.Size(18, 18);
			this.label21.TabIndex = 35;
			this.label21.Text = "(s)";
			this.label21.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// radLinenear
			// 
			this.radLinenear.Checked = true;
			this.radLinenear.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.radLinenear.Location = new System.Drawing.Point(112, 140);
			this.radLinenear.Name = "radLinenear";
			this.radLinenear.Size = new System.Drawing.Size(88, 21);
			this.radLinenear.TabIndex = 24;
			this.radLinenear.TabStop = true;
			this.radLinenear.Text = "Line Near";
			this.radLinenear.CheckedChanged += new System.EventHandler(this.radLinenear_CheckedChanged);
			// 
			// cboPacer
			// 
			this.cboPacer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboPacer.ItemHeight = 13;
			this.cboPacer.Items.AddRange(new object[] {
														  "B",
														  "L",
														  "M"});
			this.cboPacer.Location = new System.Drawing.Point(302, 140);
			this.cboPacer.Name = "cboPacer";
			this.cboPacer.Size = new System.Drawing.Size(56, 21);
			this.cboPacer.TabIndex = 29;
			this.cboPacer.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cboControl_Keydown);
			this.cboPacer.SelectedIndexChanged += new System.EventHandler(this.cboPacer_SelectedIndexChanged);
			this.cboPacer.Leave += new System.EventHandler(this.OnLeaveControl);
			this.cboPacer.Enter += new System.EventHandler(this.OnEnterControl);
			// 
			// txtValue
			// 
			this.txtValue.CustomFormat = "##,###,###,###,###,0.00";
			this.txtValue.EmptyAsNull = true;
			this.txtValue.ErrorInfo.ShowErrorMessage = false;
			this.txtValue.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
			this.txtValue.Location = new System.Drawing.Point(302, 189);
			this.txtValue.Name = "txtValue";
			this.txtValue.NumericInputKeys = ((C1.Win.C1Input.NumericInputKeyFlags)((((C1.Win.C1Input.NumericInputKeyFlags.F9 | C1.Win.C1Input.NumericInputKeyFlags.Plus) 
				| C1.Win.C1Input.NumericInputKeyFlags.Decimal) 
				| C1.Win.C1Input.NumericInputKeyFlags.X)));
			this.txtValue.PostValidation.Intervals.AddRange(new C1.Win.C1Input.ValueInterval[] {
																								   new C1.Win.C1Input.ValueInterval(new System.Decimal(new int[] {
																																									 0,
																																									 0,
																																									 0,
																																									 0}), null, true, true)});
			this.txtValue.Size = new System.Drawing.Size(90, 20);
			this.txtValue.TabIndex = 31;
			this.txtValue.Tag = null;
			this.txtValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.txtValue.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.None;
			this.txtValue.Enter += new System.EventHandler(this.OnEnterControl);
			this.txtValue.Leave += new System.EventHandler(this.txtValue_Leave);
			// 
			// txtTimeStudy
			// 
			this.txtTimeStudy.CustomFormat = "##,###,###,###,###,0.00";
			this.txtTimeStudy.EmptyAsNull = true;
			this.txtTimeStudy.ErrorInfo.ShowErrorMessage = false;
			this.txtTimeStudy.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
			this.txtTimeStudy.Location = new System.Drawing.Point(302, 96);
			this.txtTimeStudy.Name = "txtTimeStudy";
			this.txtTimeStudy.NumericInputKeys = ((C1.Win.C1Input.NumericInputKeyFlags)((((C1.Win.C1Input.NumericInputKeyFlags.F9 | C1.Win.C1Input.NumericInputKeyFlags.Plus) 
				| C1.Win.C1Input.NumericInputKeyFlags.Decimal) 
				| C1.Win.C1Input.NumericInputKeyFlags.X)));
			this.txtTimeStudy.PostValidation.Intervals.AddRange(new C1.Win.C1Input.ValueInterval[] {
																									   new C1.Win.C1Input.ValueInterval(new System.Decimal(new int[] {
																																										 0,
																																										 0,
																																										 0,
																																										 0}), null, true, true)});
			this.txtTimeStudy.Size = new System.Drawing.Size(76, 20);
			this.txtTimeStudy.TabIndex = 19;
			this.txtTimeStudy.Tag = null;
			this.txtTimeStudy.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.txtTimeStudy.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.None;
			this.txtTimeStudy.Enter += new System.EventHandler(this.OnEnterControl);
			this.txtTimeStudy.Leave += new System.EventHandler(this.OnLeaveControl);
			// 
			// txtRunQty
			// 
			this.txtRunQty.CustomFormat = "##,###,###,###,###,0.00";
			this.txtRunQty.EmptyAsNull = true;
			this.txtRunQty.ErrorInfo.ShowErrorMessage = false;
			this.txtRunQty.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
			this.txtRunQty.Location = new System.Drawing.Point(302, 74);
			this.txtRunQty.Name = "txtRunQty";
			this.txtRunQty.NumericInputKeys = ((C1.Win.C1Input.NumericInputKeyFlags)((((C1.Win.C1Input.NumericInputKeyFlags.F9 | C1.Win.C1Input.NumericInputKeyFlags.Plus) 
				| C1.Win.C1Input.NumericInputKeyFlags.Decimal) 
				| C1.Win.C1Input.NumericInputKeyFlags.X)));
			this.txtRunQty.PostValidation.Intervals.AddRange(new C1.Win.C1Input.ValueInterval[] {
																									new C1.Win.C1Input.ValueInterval(new System.Decimal(new int[] {
																																									  0,
																																									  0,
																																									  0,
																																									  0}), null, true, true)});
			this.txtRunQty.Size = new System.Drawing.Size(76, 20);
			this.txtRunQty.TabIndex = 15;
			this.txtRunQty.Tag = null;
			this.txtRunQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.txtRunQty.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.None;
			this.txtRunQty.Enter += new System.EventHandler(this.OnEnterControl);
			this.txtRunQty.Leave += new System.EventHandler(this.OnLeaveControl);
			// 
			// txtCrewSize
			// 
			this.txtCrewSize.CustomFormat = "##,###,###,###,###,0.00";
			this.txtCrewSize.EmptyAsNull = true;
			this.txtCrewSize.ErrorInfo.ShowErrorMessage = false;
			this.txtCrewSize.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
			this.txtCrewSize.Location = new System.Drawing.Point(302, 28);
			this.txtCrewSize.Name = "txtCrewSize";
			this.txtCrewSize.NumericInputKeys = ((C1.Win.C1Input.NumericInputKeyFlags)((((C1.Win.C1Input.NumericInputKeyFlags.F9 | C1.Win.C1Input.NumericInputKeyFlags.Plus) 
				| C1.Win.C1Input.NumericInputKeyFlags.Decimal) 
				| C1.Win.C1Input.NumericInputKeyFlags.X)));
			this.txtCrewSize.PostValidation.Intervals.AddRange(new C1.Win.C1Input.ValueInterval[] {
																									  new C1.Win.C1Input.ValueInterval(new System.Decimal(new int[] {
																																										0,
																																										0,
																																										0,
																																										0}), null, true, true)});
			this.txtCrewSize.Size = new System.Drawing.Size(76, 20);
			this.txtCrewSize.TabIndex = 7;
			this.txtCrewSize.Tag = null;
			this.txtCrewSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.txtCrewSize.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.None;
			this.txtCrewSize.Enter += new System.EventHandler(this.OnEnterControl);
			this.txtCrewSize.Leave += new System.EventHandler(this.OnLeaveControl);
			// 
			// txtLabRun
			// 
			this.txtLabRun.CustomFormat = "##,###,###,###,###,0.00";
			this.txtLabRun.EmptyAsNull = true;
			this.txtLabRun.ErrorInfo.ShowErrorMessage = false;
			this.txtLabRun.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
			this.txtLabRun.Location = new System.Drawing.Point(302, 6);
			this.txtLabRun.Name = "txtLabRun";
			this.txtLabRun.NumericInputKeys = ((C1.Win.C1Input.NumericInputKeyFlags)((((C1.Win.C1Input.NumericInputKeyFlags.F9 | C1.Win.C1Input.NumericInputKeyFlags.Plus) 
				| C1.Win.C1Input.NumericInputKeyFlags.Decimal) 
				| C1.Win.C1Input.NumericInputKeyFlags.X)));
			this.txtLabRun.PostValidation.Intervals.AddRange(new C1.Win.C1Input.ValueInterval[] {
																									new C1.Win.C1Input.ValueInterval(new System.Decimal(new int[] {
																																									  0,
																																									  0,
																																									  0,
																																									  0}), null, true, true)});
			this.txtLabRun.Size = new System.Drawing.Size(76, 20);
			this.txtLabRun.TabIndex = 3;
			this.txtLabRun.Tag = null;
			this.txtLabRun.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.txtLabRun.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.None;
			this.txtLabRun.Enter += new System.EventHandler(this.OnEnterControl);
			this.txtLabRun.Leave += new System.EventHandler(this.OnLeaveControl);
			// 
			// txtMoveTime
			// 
			this.txtMoveTime.CustomFormat = "##,###,###,###,###,0.00";
			this.txtMoveTime.EmptyAsNull = true;
			this.txtMoveTime.ErrorInfo.ShowErrorMessage = false;
			this.txtMoveTime.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
			this.txtMoveTime.Location = new System.Drawing.Point(112, 95);
			this.txtMoveTime.Name = "txtMoveTime";
			this.txtMoveTime.NumericInputKeys = ((C1.Win.C1Input.NumericInputKeyFlags)((((C1.Win.C1Input.NumericInputKeyFlags.F9 | C1.Win.C1Input.NumericInputKeyFlags.Plus) 
				| C1.Win.C1Input.NumericInputKeyFlags.Decimal) 
				| C1.Win.C1Input.NumericInputKeyFlags.X)));
			this.txtMoveTime.PostValidation.Intervals.AddRange(new C1.Win.C1Input.ValueInterval[] {
																									  new C1.Win.C1Input.ValueInterval(new System.Decimal(new int[] {
																																										0,
																																										0,
																																										0,
																																										0}), null, true, true)});
			this.txtMoveTime.Size = new System.Drawing.Size(70, 20);
			this.txtMoveTime.TabIndex = 17;
			this.txtMoveTime.Tag = null;
			this.txtMoveTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.txtMoveTime.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.None;
			this.txtMoveTime.Enter += new System.EventHandler(this.OnEnterControl);
			this.txtMoveTime.Leave += new System.EventHandler(this.OnLeaveControl);
			// 
			// txtSetupQty
			// 
			this.txtSetupQty.CustomFormat = "##,###,###,###,###,0.00";
			this.txtSetupQty.EmptyAsNull = true;
			this.txtSetupQty.ErrorInfo.ShowErrorMessage = false;
			this.txtSetupQty.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
			this.txtSetupQty.Location = new System.Drawing.Point(112, 73);
			this.txtSetupQty.Name = "txtSetupQty";
			this.txtSetupQty.NumericInputKeys = ((C1.Win.C1Input.NumericInputKeyFlags)((((C1.Win.C1Input.NumericInputKeyFlags.F9 | C1.Win.C1Input.NumericInputKeyFlags.Plus) 
				| C1.Win.C1Input.NumericInputKeyFlags.Decimal) 
				| C1.Win.C1Input.NumericInputKeyFlags.X)));
			this.txtSetupQty.PostValidation.Intervals.AddRange(new C1.Win.C1Input.ValueInterval[] {
																									  new C1.Win.C1Input.ValueInterval(new System.Decimal(new int[] {
																																										0,
																																										0,
																																										0,
																																										0}), null, true, true)});
			this.txtSetupQty.Size = new System.Drawing.Size(70, 20);
			this.txtSetupQty.TabIndex = 13;
			this.txtSetupQty.Tag = null;
			this.txtSetupQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.txtSetupQty.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.None;
			this.txtSetupQty.Enter += new System.EventHandler(this.OnEnterControl);
			this.txtSetupQty.Leave += new System.EventHandler(this.OnLeaveControl);
			// 
			// txtMachines
			// 
			this.txtMachines.CustomFormat = "##,###,###,###,###,0.00";
			this.txtMachines.EmptyAsNull = true;
			this.txtMachines.ErrorInfo.ShowErrorMessage = false;
			this.txtMachines.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
			this.txtMachines.Location = new System.Drawing.Point(112, 28);
			this.txtMachines.Name = "txtMachines";
			this.txtMachines.NumericInputKeys = ((C1.Win.C1Input.NumericInputKeyFlags)((((C1.Win.C1Input.NumericInputKeyFlags.F9 | C1.Win.C1Input.NumericInputKeyFlags.Plus) 
				| C1.Win.C1Input.NumericInputKeyFlags.Decimal) 
				| C1.Win.C1Input.NumericInputKeyFlags.X)));
			this.txtMachines.PostValidation.Intervals.AddRange(new C1.Win.C1Input.ValueInterval[] {
																									  new C1.Win.C1Input.ValueInterval(new System.Decimal(new int[] {
																																										0,
																																										0,
																																										0,
																																										0}), null, true, true)});
			this.txtMachines.Size = new System.Drawing.Size(70, 20);
			this.txtMachines.TabIndex = 5;
			this.txtMachines.Tag = null;
			this.txtMachines.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.txtMachines.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.None;
			this.txtMachines.TextChanged += new System.EventHandler(this.txtMachines_TextChanged);
			this.txtMachines.Enter += new System.EventHandler(this.OnEnterControl);
			this.txtMachines.Leave += new System.EventHandler(this.OnLeaveControl);
			// 
			// txtMachRun
			// 
			this.txtMachRun.CustomFormat = "##,###,###,###,###,0.00";
			this.txtMachRun.EmptyAsNull = true;
			this.txtMachRun.ErrorInfo.ShowErrorMessage = false;
			this.txtMachRun.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
			this.txtMachRun.Location = new System.Drawing.Point(112, 6);
			this.txtMachRun.Name = "txtMachRun";
			this.txtMachRun.NumericInputKeys = ((C1.Win.C1Input.NumericInputKeyFlags)((((C1.Win.C1Input.NumericInputKeyFlags.F9 | C1.Win.C1Input.NumericInputKeyFlags.Plus) 
				| C1.Win.C1Input.NumericInputKeyFlags.Decimal) 
				| C1.Win.C1Input.NumericInputKeyFlags.X)));
			this.txtMachRun.PostValidation.Intervals.AddRange(new C1.Win.C1Input.ValueInterval[] {
																									 new C1.Win.C1Input.ValueInterval(new System.Decimal(new int[] {
																																									   0,
																																									   0,
																																									   0,
																																									   0}), null, true, true)});
			this.txtMachRun.Size = new System.Drawing.Size(70, 20);
			this.txtMachRun.TabIndex = 1;
			this.txtMachRun.Tag = null;
			this.txtMachRun.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.txtMachRun.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.None;
			this.txtMachRun.Enter += new System.EventHandler(this.OnEnterControl);
			this.txtMachRun.Leave += new System.EventHandler(this.OnLeaveControl);
			// 
			// lblValue
			// 
			this.lblValue.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lblValue.Location = new System.Drawing.Point(210, 189);
			this.lblValue.Name = "lblValue";
			this.lblValue.Size = new System.Drawing.Size(92, 20);
			this.lblValue.TabIndex = 30;
			this.lblValue.Text = "Overlap Quantity";
			this.lblValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// radSchedSeq
			// 
			this.radSchedSeq.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.radSchedSeq.Location = new System.Drawing.Point(112, 189);
			this.radSchedSeq.Name = "radSchedSeq";
			this.radSchedSeq.Size = new System.Drawing.Size(96, 20);
			this.radSchedSeq.TabIndex = 26;
			this.radSchedSeq.TabStop = true;
			this.radSchedSeq.Text = "Schedule Seq.";
			this.radSchedSeq.CheckedChanged += new System.EventHandler(this.radOverlapQty_CheckedChanged);
			// 
			// radOverlap
			// 
			this.radOverlap.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.radOverlap.Location = new System.Drawing.Point(302, 167);
			this.radOverlap.Name = "radOverlap";
			this.radOverlap.Size = new System.Drawing.Size(88, 20);
			this.radOverlap.TabIndex = 27;
			this.radOverlap.Text = "Overlap %";
			this.radOverlap.CheckedChanged += new System.EventHandler(this.radOverlapQty_CheckedChanged);
			// 
			// radOverlapQty
			// 
			this.radOverlapQty.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.radOverlapQty.Location = new System.Drawing.Point(112, 167);
			this.radOverlapQty.Name = "radOverlapQty";
			this.radOverlapQty.Size = new System.Drawing.Size(110, 20);
			this.radOverlapQty.TabIndex = 25;
			this.radOverlapQty.TabStop = true;
			this.radOverlapQty.Text = "Overlap Quantity";
			this.radOverlapQty.CheckedChanged += new System.EventHandler(this.radOverlapQty_CheckedChanged);
			// 
			// cboLabCC
			// 
			this.cboLabCC.AddItemSeparator = ';';
			this.cboLabCC.Caption = "";
			this.cboLabCC.CaptionHeight = 17;
			this.cboLabCC.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
			this.cboLabCC.ColumnCaptionHeight = 17;
			this.cboLabCC.ColumnFooterHeight = 17;
			this.cboLabCC.ComboStyle = C1.Win.C1List.ComboStyleEnum.DropdownList;
			this.cboLabCC.ContentHeight = 15;
			this.cboLabCC.DeadAreaBackColor = System.Drawing.Color.Empty;
			this.cboLabCC.DropdownPosition = C1.Win.C1List.DropdownPositionEnum.RightDown;
			this.cboLabCC.DropDownWidth = 200;
			this.cboLabCC.EditorBackColor = System.Drawing.SystemColors.Window;
			this.cboLabCC.EditorFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.cboLabCC.EditorForeColor = System.Drawing.SystemColors.WindowText;
			this.cboLabCC.EditorHeight = 15;
			this.cboLabCC.GapHeight = 2;
			this.cboLabCC.ItemHeight = 13;
			this.cboLabCC.Location = new System.Drawing.Point(302, 50);
			this.cboLabCC.MatchEntryTimeout = ((long)(2000));
			this.cboLabCC.MaxDropDownItems = ((short)(5));
			this.cboLabCC.MaxLength = 32767;
			this.cboLabCC.MouseCursor = System.Windows.Forms.Cursors.Default;
			this.cboLabCC.Name = "cboLabCC";
			this.cboLabCC.RowDivider.Color = System.Drawing.Color.DarkGray;
			this.cboLabCC.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None;
			this.cboLabCC.RowSubDividerColor = System.Drawing.Color.DarkGray;
			this.cboLabCC.Size = new System.Drawing.Size(76, 21);
			this.cboLabCC.TabIndex = 11;
			this.cboLabCC.Enter += new System.EventHandler(this.OnEnterControl);
			this.cboLabCC.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cboControl_Keydown);
			this.cboLabCC.Leave += new System.EventHandler(this.OnLeaveControl);
			this.cboLabCC.PropBag = "<?xml version=\"1.0\"?><Blob><Styles type=\"C1.Win.C1List.Design.ContextWrapper\"><Da" +
				"ta>Group{BackColor:ControlDark;Border:None,,0, 0, 0, 0;AlignVert:Center;}Style2{" +
				"}Style5{}Style4{}Style7{}Style6{}EvenRow{BackColor:Aqua;}Selected{ForeColor:High" +
				"lightText;BackColor:Highlight;}Style3{}Inactive{ForeColor:InactiveCaptionText;Ba" +
				"ckColor:InactiveCaption;}Footer{}Caption{AlignHorz:Center;}Normal{BackColor:Wind" +
				"ow;}HighlightRow{ForeColor:HighlightText;BackColor:Highlight;}Style9{AlignHorz:N" +
				"ear;}OddRow{}RecordSelector{AlignImage:Center;}Heading{Wrap:True;AlignVert:Cente" +
				"r;Border:Raised,,1, 1, 1, 1;ForeColor:ControlText;BackColor:Control;}Style8{}Sty" +
				"le10{}Style11{}Style1{}</Data></Styles><Splits><C1.Win.C1List.ListBoxView AllowC" +
				"olSelect=\"False\" Name=\"\" CaptionHeight=\"17\" ColumnCaptionHeight=\"17\" ColumnFoote" +
				"rHeight=\"17\" VerticalScrollGroup=\"1\" HorizontalScrollGroup=\"1\"><ClientRect>0, 0," +
				" 116, 156</ClientRect><VScrollBar><Width>16</Width></VScrollBar><HScrollBar><Hei" +
				"ght>16</Height></HScrollBar><CaptionStyle parent=\"Style2\" me=\"Style9\" /><EvenRow" +
				"Style parent=\"EvenRow\" me=\"Style7\" /><FooterStyle parent=\"Footer\" me=\"Style3\" />" +
				"<GroupStyle parent=\"Group\" me=\"Style11\" /><HeadingStyle parent=\"Heading\" me=\"Sty" +
				"le2\" /><HighLightRowStyle parent=\"HighlightRow\" me=\"Style6\" /><InactiveStyle par" +
				"ent=\"Inactive\" me=\"Style4\" /><OddRowStyle parent=\"OddRow\" me=\"Style8\" /><RecordS" +
				"electorStyle parent=\"RecordSelector\" me=\"Style10\" /><SelectedStyle parent=\"Selec" +
				"ted\" me=\"Style5\" /><Style parent=\"Normal\" me=\"Style1\" /></C1.Win.C1List.ListBoxV" +
				"iew></Splits><NamedStyles><Style parent=\"\" me=\"Normal\" /><Style parent=\"Normal\" " +
				"me=\"Heading\" /><Style parent=\"Heading\" me=\"Footer\" /><Style parent=\"Heading\" me=" +
				"\"Caption\" /><Style parent=\"Heading\" me=\"Inactive\" /><Style parent=\"Normal\" me=\"S" +
				"elected\" /><Style parent=\"Normal\" me=\"HighlightRow\" /><Style parent=\"Normal\" me=" +
				"\"EvenRow\" /><Style parent=\"Normal\" me=\"OddRow\" /><Style parent=\"Heading\" me=\"Rec" +
				"ordSelector\" /><Style parent=\"Caption\" me=\"Group\" /></NamedStyles><vertSplits>1<" +
				"/vertSplits><horzSplits>1</horzSplits><Layout>Modified</Layout><DefaultRecSelWid" +
				"th>17</DefaultRecSelWidth></Blob>";
			// 
			// cboMachCC
			// 
			this.cboMachCC.AddItemSeparator = ';';
			this.cboMachCC.Caption = "";
			this.cboMachCC.CaptionHeight = 17;
			this.cboMachCC.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
			this.cboMachCC.ColumnCaptionHeight = 17;
			this.cboMachCC.ColumnFooterHeight = 17;
			this.cboMachCC.ComboStyle = C1.Win.C1List.ComboStyleEnum.DropdownList;
			this.cboMachCC.ContentHeight = 15;
			this.cboMachCC.DeadAreaBackColor = System.Drawing.Color.Empty;
			this.cboMachCC.DropDownWidth = 200;
			this.cboMachCC.EditorBackColor = System.Drawing.SystemColors.Window;
			this.cboMachCC.EditorFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.cboMachCC.EditorForeColor = System.Drawing.SystemColors.WindowText;
			this.cboMachCC.EditorHeight = 15;
			this.cboMachCC.GapHeight = 2;
			this.cboMachCC.ItemHeight = 13;
			this.cboMachCC.Location = new System.Drawing.Point(112, 50);
			this.cboMachCC.MatchEntryTimeout = ((long)(2000));
			this.cboMachCC.MaxDropDownItems = ((short)(5));
			this.cboMachCC.MaxLength = 32767;
			this.cboMachCC.MouseCursor = System.Windows.Forms.Cursors.Default;
			this.cboMachCC.Name = "cboMachCC";
			this.cboMachCC.RowDivider.Color = System.Drawing.Color.DarkGray;
			this.cboMachCC.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None;
			this.cboMachCC.RowSubDividerColor = System.Drawing.Color.DarkGray;
			this.cboMachCC.Size = new System.Drawing.Size(70, 21);
			this.cboMachCC.TabIndex = 9;
			this.cboMachCC.Enter += new System.EventHandler(this.OnEnterControl);
			this.cboMachCC.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cboControl_Keydown);
			this.cboMachCC.Leave += new System.EventHandler(this.OnLeaveControl);
			this.cboMachCC.PropBag = "<?xml version=\"1.0\"?><Blob><Styles type=\"C1.Win.C1List.Design.ContextWrapper\"><Da" +
				"ta>Group{BackColor:ControlDark;Border:None,,0, 0, 0, 0;AlignVert:Center;}Style2{" +
				"}Style5{}Style4{}Style7{}Style6{}EvenRow{BackColor:Aqua;}Selected{ForeColor:High" +
				"lightText;BackColor:Highlight;}Style3{}Inactive{ForeColor:InactiveCaptionText;Ba" +
				"ckColor:InactiveCaption;}Footer{}Caption{AlignHorz:Center;}Normal{BackColor:Wind" +
				"ow;}HighlightRow{ForeColor:HighlightText;BackColor:Highlight;}Style9{AlignHorz:N" +
				"ear;}OddRow{}RecordSelector{AlignImage:Center;}Heading{Wrap:True;AlignVert:Cente" +
				"r;Border:Raised,,1, 1, 1, 1;ForeColor:ControlText;BackColor:Control;}Style8{}Sty" +
				"le10{}Style11{}Style1{}</Data></Styles><Splits><C1.Win.C1List.ListBoxView AllowC" +
				"olSelect=\"False\" Name=\"\" CaptionHeight=\"17\" ColumnCaptionHeight=\"17\" ColumnFoote" +
				"rHeight=\"17\" VerticalScrollGroup=\"1\" HorizontalScrollGroup=\"1\"><ClientRect>0, 0," +
				" 116, 156</ClientRect><VScrollBar><Width>16</Width></VScrollBar><HScrollBar><Hei" +
				"ght>16</Height></HScrollBar><CaptionStyle parent=\"Style2\" me=\"Style9\" /><EvenRow" +
				"Style parent=\"EvenRow\" me=\"Style7\" /><FooterStyle parent=\"Footer\" me=\"Style3\" />" +
				"<GroupStyle parent=\"Group\" me=\"Style11\" /><HeadingStyle parent=\"Heading\" me=\"Sty" +
				"le2\" /><HighLightRowStyle parent=\"HighlightRow\" me=\"Style6\" /><InactiveStyle par" +
				"ent=\"Inactive\" me=\"Style4\" /><OddRowStyle parent=\"OddRow\" me=\"Style8\" /><RecordS" +
				"electorStyle parent=\"RecordSelector\" me=\"Style10\" /><SelectedStyle parent=\"Selec" +
				"ted\" me=\"Style5\" /><Style parent=\"Normal\" me=\"Style1\" /></C1.Win.C1List.ListBoxV" +
				"iew></Splits><NamedStyles><Style parent=\"\" me=\"Normal\" /><Style parent=\"Normal\" " +
				"me=\"Heading\" /><Style parent=\"Heading\" me=\"Footer\" /><Style parent=\"Heading\" me=" +
				"\"Caption\" /><Style parent=\"Heading\" me=\"Inactive\" /><Style parent=\"Normal\" me=\"S" +
				"elected\" /><Style parent=\"Normal\" me=\"HighlightRow\" /><Style parent=\"Normal\" me=" +
				"\"EvenRow\" /><Style parent=\"Normal\" me=\"OddRow\" /><Style parent=\"Heading\" me=\"Rec" +
				"ordSelector\" /><Style parent=\"Caption\" me=\"Group\" /></NamedStyles><vertSplits>1<" +
				"/vertSplits><horzSplits>1</horzSplits><Layout>Modified</Layout><DefaultRecSelWid" +
				"th>17</DefaultRecSelWidth></Blob>";
			// 
			// lblMachine
			// 
			this.lblMachine.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lblMachine.Location = new System.Drawing.Point(6, 6);
			this.lblMachine.Name = "lblMachine";
			this.lblMachine.Size = new System.Drawing.Size(110, 20);
			this.lblMachine.TabIndex = 0;
			this.lblMachine.Text = "Machine Run";
			this.lblMachine.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label10
			// 
			this.label10.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.label10.Location = new System.Drawing.Point(6, 50);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(110, 20);
			this.label10.TabIndex = 8;
			this.label10.Text = "Machine Cost Center";
			this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label15
			// 
			this.label15.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.label15.Location = new System.Drawing.Point(6, 96);
			this.label15.Name = "label15";
			this.label15.Size = new System.Drawing.Size(110, 20);
			this.label15.TabIndex = 16;
			this.label15.Text = "Move Time";
			this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblMachRun
			// 
			this.lblMachRun.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lblMachRun.Location = new System.Drawing.Point(6, 28);
			this.lblMachRun.Name = "lblMachRun";
			this.lblMachRun.Size = new System.Drawing.Size(110, 20);
			this.lblMachRun.TabIndex = 4;
			this.lblMachRun.Text = "No. of Machines";
			this.lblMachRun.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label11
			// 
			this.label11.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.label11.Location = new System.Drawing.Point(6, 74);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(110, 20);
			this.label11.TabIndex = 12;
			this.label11.Text = "Setup Quantity";
			this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// txtLabSetup
			// 
			this.txtLabSetup.CustomFormat = "##,###,###,###,###,0.00";
			this.txtLabSetup.EmptyAsNull = true;
			this.txtLabSetup.ErrorInfo.ShowErrorMessage = false;
			this.txtLabSetup.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
			this.txtLabSetup.Location = new System.Drawing.Point(302, 118);
			this.txtLabSetup.Name = "txtLabSetup";
			this.txtLabSetup.NumericInputKeys = ((C1.Win.C1Input.NumericInputKeyFlags)((((C1.Win.C1Input.NumericInputKeyFlags.F9 | C1.Win.C1Input.NumericInputKeyFlags.Plus) 
				| C1.Win.C1Input.NumericInputKeyFlags.Decimal) 
				| C1.Win.C1Input.NumericInputKeyFlags.X)));
			this.txtLabSetup.PostValidation.Intervals.AddRange(new C1.Win.C1Input.ValueInterval[] {
																									  new C1.Win.C1Input.ValueInterval(new System.Decimal(new int[] {
																																										0,
																																										0,
																																										0,
																																										0}), null, true, true)});
			this.txtLabSetup.Size = new System.Drawing.Size(76, 20);
			this.txtLabSetup.TabIndex = 23;
			this.txtLabSetup.Tag = null;
			this.txtLabSetup.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.txtLabSetup.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.None;
			this.txtLabSetup.Enter += new System.EventHandler(this.OnEnterControl);
			this.txtLabSetup.Leave += new System.EventHandler(this.OnLeaveControl);
			// 
			// txtMachSU
			// 
			this.txtMachSU.CustomFormat = "##,###,###,###,###,0.00";
			this.txtMachSU.EmptyAsNull = true;
			this.txtMachSU.ErrorInfo.ShowErrorMessage = false;
			this.txtMachSU.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
			this.txtMachSU.Location = new System.Drawing.Point(112, 117);
			this.txtMachSU.Name = "txtMachSU";
			this.txtMachSU.NumericInputKeys = ((C1.Win.C1Input.NumericInputKeyFlags)((((C1.Win.C1Input.NumericInputKeyFlags.F9 | C1.Win.C1Input.NumericInputKeyFlags.Plus) 
				| C1.Win.C1Input.NumericInputKeyFlags.Decimal) 
				| C1.Win.C1Input.NumericInputKeyFlags.X)));
			this.txtMachSU.PostValidation.Intervals.AddRange(new C1.Win.C1Input.ValueInterval[] {
																									new C1.Win.C1Input.ValueInterval(new System.Decimal(new int[] {
																																									  0,
																																									  0,
																																									  0,
																																									  0}), null, true, true)});
			this.txtMachSU.Size = new System.Drawing.Size(70, 20);
			this.txtMachSU.TabIndex = 21;
			this.txtMachSU.Tag = null;
			this.txtMachSU.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.txtMachSU.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.None;
			this.txtMachSU.Enter += new System.EventHandler(this.OnEnterControl);
			this.txtMachSU.Leave += new System.EventHandler(this.OnLeaveControl);
			// 
			// label9
			// 
			this.label9.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.label9.Location = new System.Drawing.Point(6, 118);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(110, 20);
			this.label9.TabIndex = 20;
			this.label9.Text = "Machine Setup";
			this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblPacer
			// 
			this.lblPacer.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lblPacer.Location = new System.Drawing.Point(208, 140);
			this.lblPacer.Name = "lblPacer";
			this.lblPacer.Size = new System.Drawing.Size(96, 21);
			this.lblPacer.TabIndex = 28;
			this.lblPacer.Text = "Pacer";
			// 
			// lblMachSetup
			// 
			this.lblMachSetup.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lblMachSetup.Location = new System.Drawing.Point(208, 6);
			this.lblMachSetup.Name = "lblMachSetup";
			this.lblMachSetup.Size = new System.Drawing.Size(96, 20);
			this.lblMachSetup.TabIndex = 2;
			this.lblMachSetup.Text = "Labor Run";
			this.lblMachSetup.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label6
			// 
			this.label6.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.label6.Location = new System.Drawing.Point(208, 28);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(96, 20);
			this.label6.TabIndex = 6;
			this.label6.Text = "Crew Size";
			this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label14
			// 
			this.label14.BackColor = System.Drawing.SystemColors.ActiveBorder;
			this.label14.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.label14.Location = new System.Drawing.Point(208, 96);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(96, 20);
			this.label14.TabIndex = 18;
			this.label14.Text = "Time Study";
			this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label12
			// 
			this.label12.BackColor = System.Drawing.Color.Transparent;
			this.label12.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.label12.Location = new System.Drawing.Point(208, 74);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(96, 20);
			this.label12.TabIndex = 14;
			this.label12.Text = "Run Quantity";
			this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label13
			// 
			this.label13.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.label13.Location = new System.Drawing.Point(208, 50);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(96, 20);
			this.label13.TabIndex = 10;
			this.label13.Text = "Labor Cost Center";
			this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblRountingSetup
			// 
			this.lblRountingSetup.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lblRountingSetup.Location = new System.Drawing.Point(208, 118);
			this.lblRountingSetup.Name = "lblRountingSetup";
			this.lblRountingSetup.Size = new System.Drawing.Size(96, 20);
			this.lblRountingSetup.TabIndex = 22;
			this.lblRountingSetup.Text = "Labor Setup";
			this.lblRountingSetup.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label5
			// 
			this.label5.ForeColor = System.Drawing.SystemColors.ControlText;
			this.label5.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.label5.Location = new System.Drawing.Point(182, 6);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(18, 18);
			this.label5.TabIndex = 29;
			this.label5.Text = "(s)";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// pnlOutside
			// 
			this.pnlOutside.Controls.Add(this.label27);
			this.pnlOutside.Controls.Add(this.label26);
			this.pnlOutside.Controls.Add(this.label7);
			this.pnlOutside.Controls.Add(this.radLinenear2);
			this.pnlOutside.Controls.Add(this.txtValue2);
			this.pnlOutside.Controls.Add(this.txtCost);
			this.pnlOutside.Controls.Add(this.txtMoveTime2);
			this.pnlOutside.Controls.Add(this.txtVarLeadTime2);
			this.pnlOutside.Controls.Add(this.txtFixLeadTime2);
			this.pnlOutside.Controls.Add(this.lblValue2);
			this.pnlOutside.Controls.Add(this.radSchedSeq2);
			this.pnlOutside.Controls.Add(this.radOverlap2);
			this.pnlOutside.Controls.Add(this.radOverlapQty2);
			this.pnlOutside.Controls.Add(this.label32);
			this.pnlOutside.Controls.Add(this.label31);
			this.pnlOutside.Controls.Add(this.label29);
			this.pnlOutside.Controls.Add(this.label30);
			this.pnlOutside.Controls.Add(this.txtVendor);
			this.pnlOutside.Controls.Add(this.btnVendor);
			this.pnlOutside.Controls.Add(this.label8);
			this.pnlOutside.Location = new System.Drawing.Point(0, 0);
			this.pnlOutside.Name = "pnlOutside";
			this.pnlOutside.Size = new System.Drawing.Size(404, 212);
			this.pnlOutside.TabIndex = 1;
			this.pnlOutside.Visible = false;
			this.pnlOutside.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlOutside_Paint);
			// 
			// label27
			// 
			this.label27.ForeColor = System.Drawing.SystemColors.ControlText;
			this.label27.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.label27.Location = new System.Drawing.Point(172, 57);
			this.label27.Name = "label27";
			this.label27.Size = new System.Drawing.Size(18, 18);
			this.label27.TabIndex = 38;
			this.label27.Text = "(s)";
			this.label27.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label26
			// 
			this.label26.ForeColor = System.Drawing.SystemColors.ControlText;
			this.label26.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.label26.Location = new System.Drawing.Point(172, 36);
			this.label26.Name = "label26";
			this.label26.Size = new System.Drawing.Size(18, 18);
			this.label26.TabIndex = 37;
			this.label26.Text = "(s)";
			this.label26.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label7
			// 
			this.label7.ForeColor = System.Drawing.SystemColors.ControlText;
			this.label7.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.label7.Location = new System.Drawing.Point(172, 14);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(18, 18);
			this.label7.TabIndex = 36;
			this.label7.Text = "(s)";
			this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// radLinenear2
			// 
			this.radLinenear2.Checked = true;
			this.radLinenear2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.radLinenear2.Location = new System.Drawing.Point(88, 81);
			this.radLinenear2.Name = "radLinenear2";
			this.radLinenear2.Size = new System.Drawing.Size(110, 21);
			this.radLinenear2.TabIndex = 11;
			this.radLinenear2.TabStop = true;
			this.radLinenear2.Text = "Line Near";
			this.radLinenear2.CheckedChanged += new System.EventHandler(this.radLinenear2_CheckedChanged);
			// 
			// txtValue2
			// 
			this.txtValue2.EmptyAsNull = true;
			this.txtValue2.ErrorInfo.ShowErrorMessage = false;
			this.txtValue2.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
			this.txtValue2.Location = new System.Drawing.Point(304, 106);
			this.txtValue2.Name = "txtValue2";
			this.txtValue2.PostValidation.Intervals.AddRange(new C1.Win.C1Input.ValueInterval[] {
																									new C1.Win.C1Input.ValueInterval(new System.Decimal(new int[] {
																																									  0,
																																									  0,
																																									  0,
																																									  0}), null, true, true)});
			this.txtValue2.Size = new System.Drawing.Size(64, 20);
			this.txtValue2.TabIndex = 16;
			this.txtValue2.Tag = null;
			this.txtValue2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.txtValue2.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.None;
			this.txtValue2.Enter += new System.EventHandler(this.OnEnterControl);
			this.txtValue2.Leave += new System.EventHandler(this.txtValue2_Leave);
			// 
			// txtCost
			// 
			this.txtCost.CustomFormat = "##,###,###,###,###,0.00";
			this.txtCost.EmptyAsNull = true;
			this.txtCost.ErrorInfo.ShowErrorMessage = false;
			this.txtCost.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
			this.txtCost.Location = new System.Drawing.Point(272, 34);
			this.txtCost.Name = "txtCost";
			this.txtCost.PostValidation.Intervals.AddRange(new C1.Win.C1Input.ValueInterval[] {
																								  new C1.Win.C1Input.ValueInterval(new System.Decimal(new int[] {
																																									0,
																																									0,
																																									0,
																																									0}), null, true, true)});
			this.txtCost.Size = new System.Drawing.Size(96, 20);
			this.txtCost.TabIndex = 8;
			this.txtCost.Tag = null;
			this.txtCost.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.txtCost.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.None;
			this.txtCost.Enter += new System.EventHandler(this.OnEnterControl);
			this.txtCost.Leave += new System.EventHandler(this.OnLeaveControl);
			// 
			// txtMoveTime2
			// 
			this.txtMoveTime2.CustomFormat = "##,###,###,###,###,0.00";
			this.txtMoveTime2.EmptyAsNull = true;
			this.txtMoveTime2.ErrorInfo.ShowErrorMessage = false;
			this.txtMoveTime2.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
			this.txtMoveTime2.Location = new System.Drawing.Point(88, 56);
			this.txtMoveTime2.Name = "txtMoveTime2";
			this.txtMoveTime2.PostValidation.Intervals.AddRange(new C1.Win.C1Input.ValueInterval[] {
																									   new C1.Win.C1Input.ValueInterval(new System.Decimal(new int[] {
																																										 0,
																																										 0,
																																										 0,
																																										 0}), null, true, true)});
			this.txtMoveTime2.Size = new System.Drawing.Size(82, 20);
			this.txtMoveTime2.TabIndex = 10;
			this.txtMoveTime2.Tag = null;
			this.txtMoveTime2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.txtMoveTime2.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.None;
			this.txtMoveTime2.Enter += new System.EventHandler(this.OnEnterControl);
			this.txtMoveTime2.Leave += new System.EventHandler(this.OnLeaveControl);
			// 
			// txtVarLeadTime2
			// 
			this.txtVarLeadTime2.CustomFormat = "#######,0.00";
			this.txtVarLeadTime2.EmptyAsNull = true;
			this.txtVarLeadTime2.ErrorInfo.ShowErrorMessage = false;
			this.txtVarLeadTime2.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
			this.txtVarLeadTime2.Location = new System.Drawing.Point(88, 34);
			this.txtVarLeadTime2.Name = "txtVarLeadTime2";
			this.txtVarLeadTime2.PostValidation.Intervals.AddRange(new C1.Win.C1Input.ValueInterval[] {
																										  new C1.Win.C1Input.ValueInterval(new System.Decimal(new int[] {
																																											0,
																																											0,
																																											0,
																																											0}), null, true, true)});
			this.txtVarLeadTime2.Size = new System.Drawing.Size(82, 20);
			this.txtVarLeadTime2.TabIndex = 6;
			this.txtVarLeadTime2.Tag = null;
			this.txtVarLeadTime2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.txtVarLeadTime2.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.None;
			this.txtVarLeadTime2.Enter += new System.EventHandler(this.OnEnterControl);
			this.txtVarLeadTime2.Leave += new System.EventHandler(this.OnLeaveControl);
			// 
			// txtFixLeadTime2
			// 
			this.txtFixLeadTime2.CustomFormat = "######,0.00";
			this.txtFixLeadTime2.EmptyAsNull = true;
			this.txtFixLeadTime2.ErrorInfo.ShowErrorMessage = false;
			this.txtFixLeadTime2.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
			this.txtFixLeadTime2.Location = new System.Drawing.Point(88, 12);
			this.txtFixLeadTime2.Name = "txtFixLeadTime2";
			this.txtFixLeadTime2.PostValidation.Intervals.AddRange(new C1.Win.C1Input.ValueInterval[] {
																										  new C1.Win.C1Input.ValueInterval(new System.Decimal(new int[] {
																																											0,
																																											0,
																																											0,
																																											0}), null, true, true)});
			this.txtFixLeadTime2.Size = new System.Drawing.Size(82, 20);
			this.txtFixLeadTime2.TabIndex = 1;
			this.txtFixLeadTime2.Tag = null;
			this.txtFixLeadTime2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.txtFixLeadTime2.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.None;
			this.txtFixLeadTime2.Enter += new System.EventHandler(this.OnEnterControl);
			this.txtFixLeadTime2.Leave += new System.EventHandler(this.OnLeaveControl);
			// 
			// lblValue2
			// 
			this.lblValue2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lblValue2.Location = new System.Drawing.Point(216, 106);
			this.lblValue2.Name = "lblValue2";
			this.lblValue2.Size = new System.Drawing.Size(94, 20);
			this.lblValue2.TabIndex = 15;
			this.lblValue2.Text = "Overlap Quantity";
			this.lblValue2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// radSchedSeq2
			// 
			this.radSchedSeq2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.radSchedSeq2.Location = new System.Drawing.Point(88, 130);
			this.radSchedSeq2.Name = "radSchedSeq2";
			this.radSchedSeq2.Size = new System.Drawing.Size(110, 20);
			this.radSchedSeq2.TabIndex = 13;
			this.radSchedSeq2.Text = "Schedule Seq.";
			this.radSchedSeq2.CheckedChanged += new System.EventHandler(this.radOverlapQty2_CheckedChanged);
			// 
			// radOverlap2
			// 
			this.radOverlap2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.radOverlap2.Location = new System.Drawing.Point(216, 82);
			this.radOverlap2.Name = "radOverlap2";
			this.radOverlap2.Size = new System.Drawing.Size(88, 20);
			this.radOverlap2.TabIndex = 14;
			this.radOverlap2.Text = "Overlap %";
			this.radOverlap2.CheckedChanged += new System.EventHandler(this.radOverlapQty2_CheckedChanged);
			// 
			// radOverlapQty2
			// 
			this.radOverlapQty2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.radOverlapQty2.Location = new System.Drawing.Point(88, 106);
			this.radOverlapQty2.Name = "radOverlapQty2";
			this.radOverlapQty2.Size = new System.Drawing.Size(110, 20);
			this.radOverlapQty2.TabIndex = 12;
			this.radOverlapQty2.Text = "Overlap Quantity";
			this.radOverlapQty2.CheckedChanged += new System.EventHandler(this.radOverlapQty2_CheckedChanged);
			// 
			// label32
			// 
			this.label32.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.label32.Location = new System.Drawing.Point(216, 34);
			this.label32.Name = "label32";
			this.label32.Size = new System.Drawing.Size(52, 20);
			this.label32.TabIndex = 7;
			this.label32.Text = "Cost";
			this.label32.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label31
			// 
			this.label31.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.label31.Location = new System.Drawing.Point(6, 56);
			this.label31.Name = "label31";
			this.label31.Size = new System.Drawing.Size(80, 20);
			this.label31.TabIndex = 9;
			this.label31.Text = "Move Time";
			this.label31.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label29
			// 
			this.label29.BackColor = System.Drawing.SystemColors.ActiveBorder;
			this.label29.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.label29.Location = new System.Drawing.Point(6, 32);
			this.label29.Name = "label29";
			this.label29.Size = new System.Drawing.Size(80, 20);
			this.label29.TabIndex = 5;
			this.label29.Text = "Var Lead Time";
			this.label29.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label30
			// 
			this.label30.BackColor = System.Drawing.SystemColors.ActiveBorder;
			this.label30.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.label30.Location = new System.Drawing.Point(6, 10);
			this.label30.Name = "label30";
			this.label30.Size = new System.Drawing.Size(80, 20);
			this.label30.TabIndex = 0;
			this.label30.Text = "Fix Lead Time";
			this.label30.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// txtVendor
			// 
			this.txtVendor.EmptyAsNull = true;
			this.txtVendor.Location = new System.Drawing.Point(272, 12);
			this.txtVendor.Name = "txtVendor";
			this.txtVendor.Size = new System.Drawing.Size(95, 20);
			this.txtVendor.TabIndex = 3;
			this.txtVendor.Tag = null;
			this.txtVendor.TextDetached = true;
			this.txtVendor.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtVendor_KeyDown);
			this.txtVendor.Enter += new System.EventHandler(this.OnEnterControl);
			this.txtVendor.Leave += new System.EventHandler(this.txtVendor_Leave);
			// 
			// btnVendor
			// 
			this.btnVendor.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnVendor.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnVendor.Location = new System.Drawing.Point(368, 12);
			this.btnVendor.Name = "btnVendor";
			this.btnVendor.Size = new System.Drawing.Size(21, 19);
			this.btnVendor.TabIndex = 4;
			this.btnVendor.Text = "...";
			this.btnVendor.Click += new System.EventHandler(this.btnVendor_Click);
			// 
			// label8
			// 
			this.label8.BackColor = System.Drawing.SystemColors.ActiveBorder;
			this.label8.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.label8.Location = new System.Drawing.Point(216, 14);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(52, 18);
			this.label8.TabIndex = 2;
			this.label8.Text = "Vendor";
			this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label20
			// 
			this.label20.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label20.ForeColor = System.Drawing.Color.Maroon;
			this.label20.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.label20.Location = new System.Drawing.Point(6, 12);
			this.label20.Name = "label20";
			this.label20.Size = new System.Drawing.Size(50, 20);
			this.label20.TabIndex = 0;
			this.label20.Text = "Step";
			this.label20.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblWorkCenter
			// 
			this.lblWorkCenter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.lblWorkCenter.ForeColor = System.Drawing.Color.Maroon;
			this.lblWorkCenter.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lblWorkCenter.Location = new System.Drawing.Point(216, 11);
			this.lblWorkCenter.Name = "lblWorkCenter";
			this.lblWorkCenter.Size = new System.Drawing.Size(70, 20);
			this.lblWorkCenter.TabIndex = 10;
			this.lblWorkCenter.Text = "Work Center";
			this.lblWorkCenter.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label17
			// 
			this.label17.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label17.ForeColor = System.Drawing.Color.Maroon;
			this.label17.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.label17.Location = new System.Drawing.Point(216, 33);
			this.label17.Name = "label17";
			this.label17.Size = new System.Drawing.Size(50, 20);
			this.label17.TabIndex = 13;
			this.label17.Text = "Function";
			this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label16
			// 
			this.label16.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label16.ForeColor = System.Drawing.Color.Maroon;
			this.label16.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.label16.Location = new System.Drawing.Point(6, 34);
			this.label16.Name = "label16";
			this.label16.Size = new System.Drawing.Size(50, 20);
			this.label16.TabIndex = 2;
			this.label16.Text = "Type";
			this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label19
			// 
			this.label19.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label19.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.label19.Location = new System.Drawing.Point(6, 58);
			this.label19.Name = "label19";
			this.label19.Size = new System.Drawing.Size(36, 20);
			this.label19.TabIndex = 4;
			this.label19.Text = "Status";
			this.label19.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// cboEndDate
			// 
			// 
			// cboEndDate.Calendar
			// 
			this.cboEndDate.Calendar.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.cboEndDate.CustomFormat = "dd-MM-yyyy";
			this.cboEndDate.EmptyAsNull = true;
			this.cboEndDate.ErrorInfo.ShowErrorMessage = false;
			this.cboEndDate.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
			this.cboEndDate.Location = new System.Drawing.Point(416, 470);
			this.cboEndDate.Name = "cboEndDate";
			this.cboEndDate.Size = new System.Drawing.Size(100, 20);
			this.cboEndDate.TabIndex = 32;
			this.cboEndDate.Tag = null;
			this.cboEndDate.Visible = false;
			this.cboEndDate.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.DropDown;
			this.cboEndDate.Enter += new System.EventHandler(this.OnEnterControl);
			this.cboEndDate.Leave += new System.EventHandler(this.OnLeaveControl);
			// 
			// cboBeginDate
			// 
			// 
			// cboBeginDate.Calendar
			// 
			this.cboBeginDate.Calendar.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.cboBeginDate.CustomFormat = "dd-MM-yyyy";
			this.cboBeginDate.EmptyAsNull = true;
			this.cboBeginDate.ErrorInfo.ShowErrorMessage = false;
			this.cboBeginDate.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
			this.cboBeginDate.Location = new System.Drawing.Point(416, 446);
			this.cboBeginDate.Name = "cboBeginDate";
			this.cboBeginDate.Size = new System.Drawing.Size(100, 20);
			this.cboBeginDate.TabIndex = 30;
			this.cboBeginDate.Tag = null;
			this.cboBeginDate.Visible = false;
			this.cboBeginDate.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.DropDown;
			this.cboBeginDate.Enter += new System.EventHandler(this.OnEnterControl);
			this.cboBeginDate.Leave += new System.EventHandler(this.OnLeaveControl);
			// 
			// label34
			// 
			this.label34.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.label34.ForeColor = System.Drawing.Color.Black;
			this.label34.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.label34.Location = new System.Drawing.Point(302, 466);
			this.label34.Name = "label34";
			this.label34.Size = new System.Drawing.Size(108, 20);
			this.label34.TabIndex = 31;
			this.label34.Text = "Effective End Date";
			this.label34.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.label34.Visible = false;
			// 
			// label33
			// 
			this.label33.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.label33.ForeColor = System.Drawing.Color.Black;
			this.label33.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.label33.Location = new System.Drawing.Point(302, 442);
			this.label33.Name = "label33";
			this.label33.Size = new System.Drawing.Size(108, 20);
			this.label33.TabIndex = 29;
			this.label33.Text = "Effective Begin Date";
			this.label33.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.label33.Visible = false;
			// 
			// btnProductionLine
			// 
			this.btnProductionLine.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnProductionLine.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnProductionLine.Location = new System.Drawing.Point(188, 76);
			this.btnProductionLine.Name = "btnProductionLine";
			this.btnProductionLine.Size = new System.Drawing.Size(21, 20);
			this.btnProductionLine.TabIndex = 15;
			this.btnProductionLine.Text = "...";
			this.btnProductionLine.Click += new System.EventHandler(this.btnProductionLine_Click);
			// 
			// txtProductionLine
			// 
			this.txtProductionLine.Location = new System.Drawing.Point(90, 76);
			this.txtProductionLine.Name = "txtProductionLine";
			this.txtProductionLine.Size = new System.Drawing.Size(96, 20);
			this.txtProductionLine.TabIndex = 14;
			this.txtProductionLine.Text = "";
			this.txtProductionLine.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtProductionLine_KeyDown);
			this.txtProductionLine.Validating += new System.ComponentModel.CancelEventHandler(this.txtProductionLine_Validating);
			// 
			// lblProductionLine
			// 
			this.lblProductionLine.ForeColor = System.Drawing.Color.Maroon;
			this.lblProductionLine.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lblProductionLine.Location = new System.Drawing.Point(3, 76);
			this.lblProductionLine.Name = "lblProductionLine";
			this.lblProductionLine.Size = new System.Drawing.Size(89, 20);
			this.lblProductionLine.TabIndex = 13;
			this.lblProductionLine.Text = "Production Line";
			this.lblProductionLine.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// btnLstSave
			// 
			this.btnLstSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnLstSave.Enabled = false;
			this.btnLstSave.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnLstSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F);
			this.btnLstSave.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnLstSave.Location = new System.Drawing.Point(562, 438);
			this.btnLstSave.Name = "btnLstSave";
			this.btnLstSave.Size = new System.Drawing.Size(42, 20);
			this.btnLstSave.TabIndex = 34;
			this.btnLstSave.Text = "Save";
			this.btnLstSave.Click += new System.EventHandler(this.btnLstSave_Click);
			// 
			// btnLstCancel
			// 
			this.btnLstCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnLstCancel.Enabled = false;
			this.btnLstCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnLstCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F);
			this.btnLstCancel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnLstCancel.Location = new System.Drawing.Point(688, 438);
			this.btnLstCancel.Name = "btnLstCancel";
			this.btnLstCancel.Size = new System.Drawing.Size(46, 20);
			this.btnLstCancel.TabIndex = 37;
			this.btnLstCancel.Text = "Cancel";
			this.btnLstCancel.Click += new System.EventHandler(this.btnLstCancel_Click);
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
			this.cboCCN.DropdownPosition = C1.Win.C1List.DropdownPositionEnum.RightDown;
			this.cboCCN.DropDownWidth = 200;
			this.cboCCN.EditorBackColor = System.Drawing.SystemColors.Window;
			this.cboCCN.EditorFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.cboCCN.EditorForeColor = System.Drawing.SystemColors.WindowText;
			this.cboCCN.EditorHeight = 15;
			this.cboCCN.Enabled = false;
			this.cboCCN.GapHeight = 2;
			this.cboCCN.ItemHeight = 13;
			this.cboCCN.Location = new System.Drawing.Point(656, 6);
			this.cboCCN.MatchEntryTimeout = ((long)(2000));
			this.cboCCN.MaxDropDownItems = ((short)(5));
			this.cboCCN.MaxLength = 32767;
			this.cboCCN.MouseCursor = System.Windows.Forms.Cursors.Default;
			this.cboCCN.Name = "cboCCN";
			this.cboCCN.RowDivider.Color = System.Drawing.Color.DarkGray;
			this.cboCCN.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None;
			this.cboCCN.RowSubDividerColor = System.Drawing.Color.DarkGray;
			this.cboCCN.Size = new System.Drawing.Size(80, 21);
			this.cboCCN.TabIndex = 1;
			this.cboCCN.Enter += new System.EventHandler(this.OnEnterControl);
			this.cboCCN.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cboControl_Keydown);
			this.cboCCN.Leave += new System.EventHandler(this.OnLeaveControl);
			this.cboCCN.PropBag = "<?xml version=\"1.0\"?><Blob><Styles type=\"C1.Win.C1List.Design.ContextWrapper\"><Da" +
				"ta>Group{AlignVert:Center;Border:None,,0, 0, 0, 0;BackColor:ControlDark;}Style2{" +
				"}Style5{}Style4{}Style7{}Style6{}EvenRow{BackColor:Aqua;}Selected{ForeColor:High" +
				"lightText;BackColor:Highlight;}Style3{}Inactive{ForeColor:InactiveCaptionText;Ba" +
				"ckColor:InactiveCaption;}Footer{}Caption{AlignHorz:Center;}Normal{BackColor:Wind" +
				"ow;}HighlightRow{ForeColor:HighlightText;BackColor:Highlight;}Style1{}OddRow{}Re" +
				"cordSelector{AlignImage:Center;}Heading{Wrap:True;BackColor:Control;Border:Raise" +
				"d,,1, 1, 1, 1;ForeColor:ControlText;AlignVert:Center;}Style8{}Style10{}Style11{}" +
				"Style9{AlignHorz:Near;}</Data></Styles><Splits><C1.Win.C1List.ListBoxView AllowC" +
				"olSelect=\"False\" Name=\"\" CaptionHeight=\"17\" ColumnCaptionHeight=\"17\" ColumnFoote" +
				"rHeight=\"17\" VerticalScrollGroup=\"1\" HorizontalScrollGroup=\"1\"><ClientRect>0, 0," +
				" 116, 156</ClientRect><VScrollBar><Width>16</Width></VScrollBar><HScrollBar><Hei" +
				"ght>16</Height></HScrollBar><CaptionStyle parent=\"Style2\" me=\"Style9\" /><EvenRow" +
				"Style parent=\"EvenRow\" me=\"Style7\" /><FooterStyle parent=\"Footer\" me=\"Style3\" />" +
				"<GroupStyle parent=\"Group\" me=\"Style11\" /><HeadingStyle parent=\"Heading\" me=\"Sty" +
				"le2\" /><HighLightRowStyle parent=\"HighlightRow\" me=\"Style6\" /><InactiveStyle par" +
				"ent=\"Inactive\" me=\"Style4\" /><OddRowStyle parent=\"OddRow\" me=\"Style8\" /><RecordS" +
				"electorStyle parent=\"RecordSelector\" me=\"Style10\" /><SelectedStyle parent=\"Selec" +
				"ted\" me=\"Style5\" /><Style parent=\"Normal\" me=\"Style1\" /></C1.Win.C1List.ListBoxV" +
				"iew></Splits><NamedStyles><Style parent=\"\" me=\"Normal\" /><Style parent=\"Normal\" " +
				"me=\"Heading\" /><Style parent=\"Heading\" me=\"Footer\" /><Style parent=\"Heading\" me=" +
				"\"Caption\" /><Style parent=\"Heading\" me=\"Inactive\" /><Style parent=\"Normal\" me=\"S" +
				"elected\" /><Style parent=\"Normal\" me=\"HighlightRow\" /><Style parent=\"Normal\" me=" +
				"\"EvenRow\" /><Style parent=\"Normal\" me=\"OddRow\" /><Style parent=\"Heading\" me=\"Rec" +
				"ordSelector\" /><Style parent=\"Caption\" me=\"Group\" /></NamedStyles><vertSplits>1<" +
				"/vertSplits><horzSplits>1</horzSplits><Layout>Modified</Layout><DefaultRecSelWid" +
				"th>17</DefaultRecSelWidth></Blob>";
			// 
			// btnAdd
			// 
			this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnAdd.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnAdd.Location = new System.Drawing.Point(4, 461);
			this.btnAdd.Name = "btnAdd";
			this.btnAdd.Size = new System.Drawing.Size(60, 24);
			this.btnAdd.TabIndex = 25;
			this.btnAdd.Text = "&Add";
			this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
			// 
			// lblTabOutside
			// 
			this.lblTabOutside.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.lblTabOutside.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lblTabOutside.Location = new System.Drawing.Point(194, 435);
			this.lblTabOutside.Name = "lblTabOutside";
			this.lblTabOutside.Size = new System.Drawing.Size(114, 20);
			this.lblTabOutside.TabIndex = 24;
			this.lblTabOutside.Text = "Outside Processing";
			this.lblTabOutside.Visible = false;
			// 
			// txtIncrement
			// 
			this.txtIncrement.AutoSize = false;
			this.txtIncrement.CustomFormat = "#0";
			this.txtIncrement.DataType = typeof(System.Byte);
			this.txtIncrement.ErrorInfo.ShowErrorMessage = false;
			this.txtIncrement.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
			this.txtIncrement.Location = new System.Drawing.Point(418, 30);
			this.txtIncrement.MaxLength = 10;
			this.txtIncrement.Name = "txtIncrement";
			this.txtIncrement.NumericInputKeys = C1.Win.C1Input.NumericInputKeyFlags.F9;
			this.txtIncrement.Size = new System.Drawing.Size(44, 20);
			this.txtIncrement.TabIndex = 10;
			this.txtIncrement.Tag = null;
			this.txtIncrement.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.txtIncrement.Value = ((System.Byte)(1));
			this.txtIncrement.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.None;
			this.txtIncrement.Enter += new System.EventHandler(this.OnEnterControl);
			this.txtIncrement.Leave += new System.EventHandler(this.OnLeaveControl);
			// 
			// txtDescription
			// 
			this.txtDescription.Enabled = false;
			this.txtDescription.Location = new System.Drawing.Point(90, 30);
			this.txtDescription.Name = "txtDescription";
			this.txtDescription.Size = new System.Drawing.Size(272, 20);
			this.txtDescription.TabIndex = 8;
			this.txtDescription.Text = "";
			this.txtDescription.Leave += new System.EventHandler(this.OnLeaveControl);
			this.txtDescription.Enter += new System.EventHandler(this.OnEnterControl);
			// 
			// txtRevision
			// 
			this.txtRevision.Enabled = false;
			this.txtRevision.Location = new System.Drawing.Point(298, 6);
			this.txtRevision.Name = "txtRevision";
			this.txtRevision.Size = new System.Drawing.Size(64, 20);
			this.txtRevision.TabIndex = 6;
			this.txtRevision.Text = "";
			this.txtRevision.Leave += new System.EventHandler(this.OnLeaveControl);
			this.txtRevision.Enter += new System.EventHandler(this.OnEnterControl);
			// 
			// txtRoutDescription
			// 
			this.txtRoutDescription.Location = new System.Drawing.Point(90, 53);
			this.txtRoutDescription.MaxLength = 200;
			this.txtRoutDescription.Name = "txtRoutDescription";
			this.txtRoutDescription.Size = new System.Drawing.Size(372, 20);
			this.txtRoutDescription.TabIndex = 12;
			this.txtRoutDescription.Text = "";
			this.txtRoutDescription.Leave += new System.EventHandler(this.OnLeaveControl);
			this.txtRoutDescription.Enter += new System.EventHandler(this.OnEnterControl);
			// 
			// gridRouting
			// 
			this.gridRouting.AllowDelete = true;
			this.gridRouting.AllowUpdate = false;
			this.gridRouting.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.gridRouting.CaptionHeight = 17;
			this.gridRouting.CollapseColor = System.Drawing.Color.Black;
			this.gridRouting.ExpandColor = System.Drawing.Color.Black;
			this.gridRouting.GroupByCaption = "Drag a column header here to group by that column";
			this.gridRouting.Images.Add(((System.Drawing.Image)(resources.GetObject("resource"))));
			this.gridRouting.Location = new System.Drawing.Point(6, 108);
			this.gridRouting.MarqueeStyle = C1.Win.C1TrueDBGrid.MarqueeEnum.DottedCellBorder;
			this.gridRouting.Name = "gridRouting";
			this.gridRouting.PreviewInfo.Location = new System.Drawing.Point(0, 0);
			this.gridRouting.PreviewInfo.Size = new System.Drawing.Size(0, 0);
			this.gridRouting.PreviewInfo.ZoomFactor = 75;
			this.gridRouting.PrintInfo.ShowOptionsDialog = false;
			this.gridRouting.RecordSelectorWidth = 17;
			this.gridRouting.RowDivider.Color = System.Drawing.Color.DarkGray;
			this.gridRouting.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.Single;
			this.gridRouting.RowHeight = 15;
			this.gridRouting.RowSubDividerColor = System.Drawing.Color.DarkGray;
			this.gridRouting.Size = new System.Drawing.Size(286, 328);
			this.gridRouting.TabIndex = 22;
			this.gridRouting.Text = "gridRouting";
			this.gridRouting.AfterDelete += new System.EventHandler(this.gridRouting_AfterDelete);
			this.gridRouting.RowColChange += new C1.Win.C1TrueDBGrid.RowColChangeEventHandler(this.gridRouting_RowColChange);
			this.gridRouting.BeforeRowColChange += new C1.Win.C1TrueDBGrid.CancelEventHandler(this.gridRouting_BeforeRowColChange);
			this.gridRouting.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"Step\" DataF" +
				"ield=\"Step\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Ca" +
				"ption=\"Type\" DataField=\"TypeID\"><ValueItems /><GroupInfo /></C1DataColumn><C1Dat" +
				"aColumn Level=\"0\" Caption=\"Function\" DataField=\"FunctionID\"><ValueItems /><Group" +
				"Info /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Work center\" DataField=\"W" +
				"orkCenterID\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" C" +
				"aption=\"Status\" DataField=\"RoutingStatusID\"><ValueItems /><GroupInfo /></C1DataC" +
				"olumn><C1DataColumn Level=\"0\" Caption=\"IsMain\" DataField=\"IsMain\"><ValueItems />" +
				"<GroupInfo /></C1DataColumn></DataCols><Styles type=\"C1.Win.C1TrueDBGrid.Design." +
				"ContextWrapper\"><Data>RecordSelector{AlignImage:Center;}Style50{}Style51{}Captio" +
				"n{AlignHorz:Center;}Style27{}Normal{}Selected{ForeColor:HighlightText;BackColor:" +
				"Highlight;}Editor{}Style31{}Style18{}Style19{}Style14{}Style15{}Style16{AlignHor" +
				"z:Near;}Style17{AlignHorz:Near;}Style10{AlignHorz:Near;}Style11{}Style12{}Style1" +
				"3{}Style42{}Style38{}Style37{}Style34{AlignHorz:Near;}Style35{AlignHorz:Near;}St" +
				"yle32{}Style33{}OddRow{}Style29{AlignHorz:Near;}Style28{AlignHorz:Near;}Highligh" +
				"tRow{ForeColor:HighlightText;BackColor:Highlight;}Style26{}Style25{}Footer{}Styl" +
				"e23{AlignHorz:Near;}Style22{AlignHorz:Near;}Style21{}Style20{}Group{AlignVert:Ce" +
				"nter;Border:None,,0, 0, 0, 0;BackColor:ControlDark;}Inactive{ForeColor:InactiveC" +
				"aptionText;BackColor:InactiveCaption;}EvenRow{BackColor:Aqua;}Heading{Wrap:True;" +
				"BackColor:Control;Border:Raised,,1, 1, 1, 1;ForeColor:ControlText;AlignVert:Cent" +
				"er;}Style49{}Style48{}Style24{}Style9{}Style41{AlignHorz:Near;}Style40{AlignHorz" +
				":Near;}Style43{}FilterBar{}Style45{}Style44{}Style47{AlignHorz:Near;}Style46{Ali" +
				"gnHorz:Near;}Style8{}Style39{}Style36{}Style5{}Style4{}Style7{}Style6{}Style1{}S" +
				"tyle30{}Style3{}Style2{}</Data></Styles><Splits><C1.Win.C1TrueDBGrid.MergeView N" +
				"ame=\"\" CaptionHeight=\"17\" ColumnCaptionHeight=\"17\" ColumnFooterHeight=\"17\" Marqu" +
				"eeStyle=\"DottedCellBorder\" RecordSelectorWidth=\"17\" DefRecSelWidth=\"17\" Vertical" +
				"ScrollGroup=\"1\" HorizontalScrollGroup=\"1\"><ClientRect>0, 0, 282, 324</ClientRect" +
				"><BorderSide>0</BorderSide><CaptionStyle parent=\"Style2\" me=\"Style10\" /><EditorS" +
				"tyle parent=\"Editor\" me=\"Style5\" /><EvenRowStyle parent=\"EvenRow\" me=\"Style8\" />" +
				"<FilterBarStyle parent=\"FilterBar\" me=\"Style13\" /><FooterStyle parent=\"Footer\" m" +
				"e=\"Style3\" /><GroupStyle parent=\"Group\" me=\"Style12\" /><HeadingStyle parent=\"Hea" +
				"ding\" me=\"Style2\" /><HighLightRowStyle parent=\"HighlightRow\" me=\"Style7\" /><Inac" +
				"tiveStyle parent=\"Inactive\" me=\"Style4\" /><OddRowStyle parent=\"OddRow\" me=\"Style" +
				"9\" /><RecordSelectorStyle parent=\"RecordSelector\" me=\"Style11\" /><SelectedStyle " +
				"parent=\"Selected\" me=\"Style6\" /><Style parent=\"Normal\" me=\"Style1\" /><internalCo" +
				"ls><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style16\" /><Style parent=\"" +
				"Style1\" me=\"Style17\" /><FooterStyle parent=\"Style3\" me=\"Style18\" /><EditorStyle " +
				"parent=\"Style5\" me=\"Style19\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style21\" />" +
				"<GroupFooterStyle parent=\"Style1\" me=\"Style20\" /><Visible>True</Visible><ColumnD" +
				"ivider>DarkGray,Single</ColumnDivider><Width>38</Width><Height>15</Height><DCIdx" +
				">0</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"S" +
				"tyle22\" /><Style parent=\"Style1\" me=\"Style23\" /><FooterStyle parent=\"Style3\" me=" +
				"\"Style24\" /><EditorStyle parent=\"Style5\" me=\"Style25\" /><GroupHeaderStyle parent" +
				"=\"Style1\" me=\"Style27\" /><GroupFooterStyle parent=\"Style1\" me=\"Style26\" /><Visib" +
				"le>True</Visible><ColumnDivider>DarkGray,Single</ColumnDivider><Width>43</Width>" +
				"<Height>15</Height><DCIdx>1</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingSt" +
				"yle parent=\"Style2\" me=\"Style28\" /><Style parent=\"Style1\" me=\"Style29\" /><Footer" +
				"Style parent=\"Style3\" me=\"Style30\" /><EditorStyle parent=\"Style5\" me=\"Style31\" /" +
				"><GroupHeaderStyle parent=\"Style1\" me=\"Style33\" /><GroupFooterStyle parent=\"Styl" +
				"e1\" me=\"Style32\" /><Visible>True</Visible><ColumnDivider>DarkGray,Single</Column" +
				"Divider><Width>68</Width><Height>15</Height><DCIdx>2</DCIdx></C1DisplayColumn><C" +
				"1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style34\" /><Style parent=\"Style" +
				"1\" me=\"Style35\" /><FooterStyle parent=\"Style3\" me=\"Style36\" /><EditorStyle paren" +
				"t=\"Style5\" me=\"Style37\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style39\" /><Grou" +
				"pFooterStyle parent=\"Style1\" me=\"Style38\" /><Visible>True</Visible><ColumnDivide" +
				"r>DarkGray,Single</ColumnDivider><Width>75</Width><Height>15</Height><DCIdx>3</D" +
				"CIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style4" +
				"0\" /><Style parent=\"Style1\" me=\"Style41\" /><FooterStyle parent=\"Style3\" me=\"Styl" +
				"e42\" /><EditorStyle parent=\"Style5\" me=\"Style43\" /><GroupHeaderStyle parent=\"Sty" +
				"le1\" me=\"Style45\" /><GroupFooterStyle parent=\"Style1\" me=\"Style44\" /><Visible>Tr" +
				"ue</Visible><ColumnDivider>DarkGray,Single</ColumnDivider><Width>41</Width><Heig" +
				"ht>15</Height><DCIdx>4</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle p" +
				"arent=\"Style2\" me=\"Style46\" /><Style parent=\"Style1\" me=\"Style47\" /><FooterStyle" +
				" parent=\"Style3\" me=\"Style48\" /><EditorStyle parent=\"Style5\" me=\"Style49\" /><Gro" +
				"upHeaderStyle parent=\"Style1\" me=\"Style51\" /><GroupFooterStyle parent=\"Style1\" m" +
				"e=\"Style50\" /><Visible>True</Visible><ColumnDivider>DarkGray,Single</ColumnDivid" +
				"er><Height>15</Height><DCIdx>5</DCIdx></C1DisplayColumn></internalCols></C1.Win." +
				"C1TrueDBGrid.MergeView></Splits><NamedStyles><Style parent=\"\" me=\"Normal\" /><Sty" +
				"le parent=\"Normal\" me=\"Heading\" /><Style parent=\"Heading\" me=\"Footer\" /><Style p" +
				"arent=\"Heading\" me=\"Caption\" /><Style parent=\"Heading\" me=\"Inactive\" /><Style pa" +
				"rent=\"Normal\" me=\"Selected\" /><Style parent=\"Normal\" me=\"Editor\" /><Style parent" +
				"=\"Normal\" me=\"HighlightRow\" /><Style parent=\"Normal\" me=\"EvenRow\" /><Style paren" +
				"t=\"Normal\" me=\"OddRow\" /><Style parent=\"Heading\" me=\"RecordSelector\" /><Style pa" +
				"rent=\"Normal\" me=\"FilterBar\" /><Style parent=\"Caption\" me=\"Group\" /></NamedStyle" +
				"s><vertSplits>1</vertSplits><horzSplits>1</horzSplits><Layout>Modified</Layout><" +
				"DefaultRecSelWidth>17</DefaultRecSelWidth><ClientArea>0, 0, 282, 324</ClientArea" +
				"><PrintPageHeaderStyle parent=\"\" me=\"Style14\" /><PrintPageFooterStyle parent=\"\" " +
				"me=\"Style15\" /></Blob>";
			// 
			// lblProductGroup
			// 
			this.lblProductGroup.ForeColor = System.Drawing.Color.Maroon;
			this.lblProductGroup.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lblProductGroup.Location = new System.Drawing.Point(278, 76);
			this.lblProductGroup.Name = "lblProductGroup";
			this.lblProductGroup.Size = new System.Drawing.Size(89, 20);
			this.lblProductGroup.TabIndex = 16;
			this.lblProductGroup.Text = "Product Group";
			this.lblProductGroup.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// btnProductGroup
			// 
			this.btnProductGroup.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnProductGroup.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnProductGroup.Location = new System.Drawing.Point(464, 76);
			this.btnProductGroup.Name = "btnProductGroup";
			this.btnProductGroup.Size = new System.Drawing.Size(21, 20);
			this.btnProductGroup.TabIndex = 18;
			this.btnProductGroup.Text = "...";
			this.btnProductGroup.Click += new System.EventHandler(this.btnProductGroup_Click);
			// 
			// txtProductGroup
			// 
			this.txtProductGroup.Location = new System.Drawing.Point(366, 76);
			this.txtProductGroup.Name = "txtProductGroup";
			this.txtProductGroup.Size = new System.Drawing.Size(96, 20);
			this.txtProductGroup.TabIndex = 17;
			this.txtProductGroup.Text = "";
			this.txtProductGroup.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtProductionLine_KeyDown);
			this.txtProductGroup.Validating += new System.ComponentModel.CancelEventHandler(this.Control_Validating);
			// 
			// btnCostCenterRate
			// 
			this.btnCostCenterRate.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnCostCenterRate.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnCostCenterRate.Location = new System.Drawing.Point(706, 76);
			this.btnCostCenterRate.Name = "btnCostCenterRate";
			this.btnCostCenterRate.Size = new System.Drawing.Size(21, 20);
			this.btnCostCenterRate.TabIndex = 21;
			this.btnCostCenterRate.Text = "...";
			this.btnCostCenterRate.Click += new System.EventHandler(this.btnCostCenterRate_Click);
			// 
			// txtCostCenterRate
			// 
			this.txtCostCenterRate.Location = new System.Drawing.Point(608, 76);
			this.txtCostCenterRate.Name = "txtCostCenterRate";
			this.txtCostCenterRate.Size = new System.Drawing.Size(96, 20);
			this.txtCostCenterRate.TabIndex = 20;
			this.txtCostCenterRate.Text = "";
			this.txtCostCenterRate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtProductionLine_KeyDown);
			this.txtCostCenterRate.Validating += new System.ComponentModel.CancelEventHandler(this.Control_Validating);
			// 
			// lblCostCenterRate
			// 
			this.lblCostCenterRate.ForeColor = System.Drawing.Color.Maroon;
			this.lblCostCenterRate.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lblCostCenterRate.Location = new System.Drawing.Point(514, 76);
			this.lblCostCenterRate.Name = "lblCostCenterRate";
			this.lblCostCenterRate.Size = new System.Drawing.Size(92, 20);
			this.lblCostCenterRate.TabIndex = 19;
			this.lblCostCenterRate.Text = "Cost Center Rate";
			this.lblCostCenterRate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// Routing
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.btnClose;
			this.ClientSize = new System.Drawing.Size(742, 492);
			this.Controls.Add(this.txtProductionLine);
			this.Controls.Add(this.txtRoutDescription);
			this.Controls.Add(this.txtRevision);
			this.Controls.Add(this.txtDescription);
			this.Controls.Add(this.txtProductGroup);
			this.Controls.Add(this.txtCostCenterRate);
			this.Controls.Add(this.gridRouting);
			this.Controls.Add(this.txtIncrement);
			this.Controls.Add(this.lblTabOutside);
			this.Controls.Add(this.btnAdd);
			this.Controls.Add(this.cboCCN);
			this.Controls.Add(this.btnSave);
			this.Controls.Add(this.btnDelete);
			this.Controls.Add(this.btnClose);
			this.Controls.Add(this.btnHelp);
			this.Controls.Add(this.btnEdit);
			this.Controls.Add(this.btnLstCancel);
			this.Controls.Add(this.btnLstAdd);
			this.Controls.Add(this.btnLstEdit);
			this.Controls.Add(this.btnLstDelete);
			this.Controls.Add(this.btnLstSave);
			this.Controls.Add(this.btnFindItem);
			this.Controls.Add(this.txtItem);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.lblRevision);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.lblIncremental);
			this.Controls.Add(this.grpRoutingGeneralInfo);
			this.Controls.Add(this.lblProductionLine);
			this.Controls.Add(this.btnProductionLine);
			this.Controls.Add(this.cboEndDate);
			this.Controls.Add(this.cboBeginDate);
			this.Controls.Add(this.label34);
			this.Controls.Add(this.label33);
			this.Controls.Add(this.btnProductGroup);
			this.Controls.Add(this.btnCostCenterRate);
			this.Controls.Add(this.lblCostCenterRate);
			this.Controls.Add(this.lblProductGroup);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.KeyPreview = true;
			this.Name = "Routing";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Item Routing";
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Routing_KeyDown);
			this.Closing += new System.ComponentModel.CancelEventHandler(this.Routing_Closing);
			this.Load += new System.EventHandler(this.Routing_Load);
			((System.ComponentModel.ISupportInitialize)(this.txtItem)).EndInit();
			this.grpRoutingGeneralInfo.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.txtStep)).EndInit();
			this.tabRouting.ResumeLayout(false);
			this.tabMachine.ResumeLayout(false);
			this.pnlMachine.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.txtValue)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.txtTimeStudy)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.txtRunQty)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.txtCrewSize)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.txtLabRun)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.txtMoveTime)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.txtSetupQty)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.txtMachines)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.txtMachRun)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.cboLabCC)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.cboMachCC)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.txtLabSetup)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.txtMachSU)).EndInit();
			this.pnlOutside.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.txtValue2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.txtCost)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.txtMoveTime2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.txtVarLeadTime2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.txtFixLeadTime2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.txtVendor)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.cboEndDate)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.cboBeginDate)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.cboCCN)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.txtIncrement)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gridRouting)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		#region DataRow -> VO -> Controls
		//**************************************************************************              
		///    <Description>
		///       copy data from VO to DataRow
		///    </Description>
		///    <Inputs>
		///               
		///    </Inputs>
		///    <Outputs>
		///       
		///    </Outputs>
		///    <Returns>
		///        
		///    </Returns>
		///    <Authors>
		///       Hoang Trung Son - iSphere Software
		///    </Authors>
		///    <History>
		///       15-Dec-2004
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************

		private void VOToDataRow(ref DataRow pobjRow)
		{
			const string METHOD_NAME = THIS + ".VOToDataRow()";
			if(pobjRow == null) return;
			try
			{
				if(voRouting.PartyID > 0)
					pobjRow[ITM_RoutingTable.PARTYID_FLD] = voRouting.PartyID;
				else pobjRow[ITM_RoutingTable.PARTYID_FLD] = DBNull.Value;

				pobjRow[ITM_RoutingTable.CREWSIZE_FLD] = voRouting.CrewSize;
				if(voRouting.EffectBeginDate > DateTime.MinValue)
					pobjRow[ITM_RoutingTable.EFFECTBEGINDATE_FLD] = voRouting.EffectBeginDate;
				else
					pobjRow[ITM_RoutingTable.EFFECTBEGINDATE_FLD] = DBNull.Value;

				if(voRouting.EffectEndDate > DateTime.MinValue)
					pobjRow[ITM_RoutingTable.EFFECTENDDATE_FLD] = voRouting.EffectEndDate;
				else
					pobjRow[ITM_RoutingTable.EFFECTENDDATE_FLD] = DBNull.Value;
				
				pobjRow[ITM_RoutingTable.FIXLT_FLD] = voRouting.FixLT;
				if(voRouting.FunctionID > 0)
					pobjRow[ITM_RoutingTable.FUNCTIONID_FLD] = voRouting.FunctionID;
				else pobjRow[ITM_RoutingTable.FUNCTIONID_FLD] = DBNull.Value;
				if(voRouting.LaborCostCenterID > 0) 
					pobjRow[ITM_RoutingTable.LABORCOSTCENTERID_FLD] = voRouting.LaborCostCenterID;
				else pobjRow[ITM_RoutingTable.LABORCOSTCENTERID_FLD] = DBNull.Value;
				pobjRow[ITM_RoutingTable.LABORRUNTIME_FLD] = voRouting.LaborRunTime;
				pobjRow[ITM_RoutingTable.LABORSETUPTIME_FLD] = voRouting.LaborSetupTime;
				if (voRouting.MachineCostCenterID > 0)
					pobjRow[ITM_RoutingTable.MACHINECOSTCENTERID_FLD] = voRouting.MachineCostCenterID;
				else pobjRow[ITM_RoutingTable.MACHINECOSTCENTERID_FLD] = DBNull.Value;
				pobjRow[ITM_RoutingTable.MACHINERUNTIME_FLD] = voRouting.MachineRunTime;
				pobjRow[ITM_RoutingTable.MACHINES_FLD] = voRouting.Machines;
				pobjRow[ITM_RoutingTable.MACHINESETUPTIME_FLD] = voRouting.MachineSetupTime;
				pobjRow[ITM_RoutingTable.MOVETIME_FLD] = voRouting.MoveTime;
				pobjRow[ITM_RoutingTable.OSCOST_FLD] = voRouting.OSCost;
				pobjRow[ITM_RoutingTable.OSFIXLT_FLD] = voRouting.OSFixLT;
				pobjRow[ITM_RoutingTable.OSOVERLAPPERCENT_FLD] = voRouting.OSOverlapPercent;
				pobjRow[ITM_RoutingTable.OSOVERLAPQTY_FLD] = voRouting.OSOverlapQty;
				pobjRow[ITM_RoutingTable.OSSCHEDULESEQ_FLD] = voRouting.OSScheduleSeq;
				pobjRow[ITM_RoutingTable.OSVARLT_FLD] = voRouting.OSVarLT;
				pobjRow[ITM_RoutingTable.OVERLAPPERCENT_FLD] = voRouting.OverlapPercent;
				pobjRow[ITM_RoutingTable.OVERLAPQTY_FLD] = voRouting.OverlapQty;
				if (voRouting.ProductID > 0)
					pobjRow[ITM_RoutingTable.PRODUCTID_FLD] = voRouting.ProductID;
				else pobjRow[ITM_RoutingTable.PRODUCTID_FLD] = DBNull.Value;
				if (voRouting.RoutingStatusID > 0)
					pobjRow[ITM_RoutingTable.ROUTINGSTATUSID_FLD] = voRouting.RoutingStatusID;
				else pobjRow[ITM_RoutingTable.ROUTINGSTATUSID_FLD] = DBNull.Value;
				pobjRow[ITM_RoutingTable.RUNQUANTITY_FLD] = voRouting.RunQuantity;
				pobjRow[ITM_RoutingTable.SCHEDULESEQ_FLD] = voRouting.ScheduleSeq;
				pobjRow[ITM_RoutingTable.SETUPQUANTITY_FLD] = voRouting.SetupQuantity;
				pobjRow[ITM_RoutingTable.STEP_FLD] = voRouting.Step;
				pobjRow[ITM_RoutingTable.STUDYTIME_FLD] = voRouting.StudyTime;
				pobjRow[ITM_RoutingTable.TYPE_FLD] = voRouting.Type;
				pobjRow[ITM_RoutingTable.VARLT_FLD] = voRouting.VarLT;
				if (voRouting.WorkCenterID > 0)
					pobjRow[ITM_RoutingTable.WORKCENTERID_FLD] = voRouting.WorkCenterID;
				else pobjRow[ITM_RoutingTable.WORKCENTERID_FLD] = DBNull.Value;
				pobjRow[ITM_RoutingTable.PACER_FLD] = voRouting.Pacer;
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

		//**************************************************************************              
		///    <Description>
		///       This method uses to copy data from DataRow to VO
		///    </Description>
		///    <Inputs>
		///            
		///    </Inputs>
		///    <Outputs>
		///       
		///    </Outputs>
		///    <Returns>
		///       
		///    </Returns>
		///    <Authors>
		///       Hoang Trung Son - iSphere software
		///    </Authors>
		///    <History>
		///       Thursday, January 27, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private void DataRowToVO(ref DataRow pobjRow)
		{
			const string METHOD_NAME = THIS + ".DataRowToVO()";
			try
			{
				if(pobjRow == null) return;
				voRouting = new ITM_RoutingVO();
				if(FormControlComponents.IsNumeric(pobjRow[ITM_RoutingTable.PARTYID_FLD].ToString()))
				{
					voRouting.PartyID = int.Parse(pobjRow[ITM_RoutingTable.PARTYID_FLD].ToString());
				}
				if(FormControlComponents.IsNumeric(pobjRow[ITM_RoutingTable.CREWSIZE_FLD].ToString()))
				{
					voRouting.CrewSize = decimal.Parse(pobjRow[ITM_RoutingTable.CREWSIZE_FLD].ToString());
				}
				if(pobjRow[ITM_RoutingTable.EFFECTBEGINDATE_FLD] != DBNull.Value)
				{
					voRouting.EffectBeginDate = DateTime.Parse(pobjRow[ITM_RoutingTable.EFFECTBEGINDATE_FLD].ToString());
				}
				else
				{
					voRouting.EffectBeginDate = DateTime.MinValue;
				}
				if(pobjRow[ITM_RoutingTable.EFFECTENDDATE_FLD] != DBNull.Value)
				{
					voRouting.EffectEndDate = DateTime.Parse(pobjRow[ITM_RoutingTable.EFFECTENDDATE_FLD].ToString());
				}
				else
				{
					voRouting.EffectEndDate = DateTime.MinValue;
				}
				if(pobjRow[ITM_RoutingTable.FIXLT_FLD] != DBNull.Value)
				{
					voRouting.FixLT = decimal.Parse(pobjRow[ITM_RoutingTable.FIXLT_FLD].ToString());
				}
				if(pobjRow[ITM_RoutingTable.FUNCTIONID_FLD] != DBNull.Value)
				{
					voRouting.FunctionID = int.Parse(pobjRow[ITM_RoutingTable.FUNCTIONID_FLD].ToString());
				}
				if(pobjRow[ITM_RoutingTable.LABORCOSTCENTERID_FLD] != DBNull.Value)
				{
					voRouting.LaborCostCenterID = int.Parse(pobjRow[ITM_RoutingTable.LABORCOSTCENTERID_FLD].ToString());
				}
				if(pobjRow[ITM_RoutingTable.LABORRUNTIME_FLD] != DBNull.Value)
				{
					voRouting.LaborRunTime = decimal.Parse(pobjRow[ITM_RoutingTable.LABORRUNTIME_FLD].ToString());
				}
				if(pobjRow[ITM_RoutingTable.LABORSETUPTIME_FLD] != DBNull.Value)
				{
					voRouting.LaborSetupTime = decimal.Parse(pobjRow[ITM_RoutingTable.LABORSETUPTIME_FLD].ToString());
				}
				if(pobjRow[ITM_RoutingTable.MACHINECOSTCENTERID_FLD] != DBNull.Value)
				{
					voRouting.MachineCostCenterID = int.Parse(pobjRow[ITM_RoutingTable.MACHINECOSTCENTERID_FLD].ToString());
				}
				if(pobjRow[ITM_RoutingTable.MACHINERUNTIME_FLD] != DBNull.Value)
				{
					voRouting.MachineRunTime = decimal.Parse(pobjRow[ITM_RoutingTable.MACHINERUNTIME_FLD].ToString());
				}
				if(pobjRow[ITM_RoutingTable.MACHINES_FLD] != DBNull.Value)
				{
					voRouting.Machines = decimal.Parse(pobjRow[ITM_RoutingTable.MACHINES_FLD].ToString());
				}
				if(pobjRow[ITM_RoutingTable.MACHINESETUPTIME_FLD] != DBNull.Value)
				{
					voRouting.MachineSetupTime = decimal.Parse(pobjRow[ITM_RoutingTable.MACHINESETUPTIME_FLD].ToString());
				}
				if(pobjRow[ITM_RoutingTable.MOVETIME_FLD] != DBNull.Value)
				{
					voRouting.MoveTime = decimal.Parse(pobjRow[ITM_RoutingTable.MOVETIME_FLD].ToString());
				}
				if(pobjRow[ITM_RoutingTable.OSCOST_FLD] != DBNull.Value)
				{
					voRouting.OSCost = decimal.Parse(pobjRow[ITM_RoutingTable.OSCOST_FLD].ToString());
				}
				if(pobjRow[ITM_RoutingTable.OSFIXLT_FLD] != DBNull.Value)
				{
					voRouting.OSFixLT = decimal.Parse(pobjRow[ITM_RoutingTable.OSFIXLT_FLD].ToString());
				}
				if(pobjRow[ITM_RoutingTable.OSOVERLAPPERCENT_FLD] != DBNull.Value)
				{
					voRouting.OSOverlapPercent = decimal.Parse(pobjRow[ITM_RoutingTable.OSOVERLAPPERCENT_FLD].ToString());
				}
				if(pobjRow[ITM_RoutingTable.OSOVERLAPQTY_FLD] != DBNull.Value)
				{
					voRouting.OSOverlapQty = decimal.Parse(pobjRow[ITM_RoutingTable.OSOVERLAPQTY_FLD].ToString());
				}
				if(pobjRow[ITM_RoutingTable.OSSCHEDULESEQ_FLD] != DBNull.Value)
				{
					voRouting.OSScheduleSeq = decimal.Parse(pobjRow[ITM_RoutingTable.OSSCHEDULESEQ_FLD].ToString());
				}
				if(pobjRow[ITM_RoutingTable.OSVARLT_FLD] != DBNull.Value)
				{
					voRouting.OSVarLT = decimal.Parse(pobjRow[ITM_RoutingTable.OSVARLT_FLD].ToString());
				}
				if(pobjRow[ITM_RoutingTable.OVERLAPPERCENT_FLD] != DBNull.Value)
				{
					voRouting.OverlapPercent = decimal.Parse(pobjRow[ITM_RoutingTable.OVERLAPPERCENT_FLD].ToString());
				}
				if(pobjRow[ITM_RoutingTable.OVERLAPQTY_FLD] != DBNull.Value)
				{
					voRouting.OverlapQty = decimal.Parse(pobjRow[ITM_RoutingTable.OVERLAPQTY_FLD].ToString());
				}
				if(pobjRow[ITM_RoutingTable.PRODUCTID_FLD] != DBNull.Value)
				{
					voRouting.ProductID = int.Parse(pobjRow[ITM_RoutingTable.PRODUCTID_FLD].ToString());
				}
				voRouting.RoutingID = int.Parse(pobjRow[ITM_RoutingTable.ROUTINGID_FLD].ToString());
				if(pobjRow[ITM_RoutingTable.ROUTINGSTATUSID_FLD] != DBNull.Value)
				{
					voRouting.RoutingStatusID = int.Parse(pobjRow[ITM_RoutingTable.ROUTINGSTATUSID_FLD].ToString());
				}
				if(pobjRow[ITM_RoutingTable.RUNQUANTITY_FLD] != DBNull.Value)
				{
					voRouting.RunQuantity = decimal.Parse(pobjRow[ITM_RoutingTable.RUNQUANTITY_FLD].ToString());
				}
				if(pobjRow[ITM_RoutingTable.SCHEDULESEQ_FLD] != DBNull.Value)
				{
					voRouting.ScheduleSeq = decimal.Parse(pobjRow[ITM_RoutingTable.SCHEDULESEQ_FLD].ToString());
				}
				if(pobjRow[ITM_RoutingTable.SETUPQUANTITY_FLD] != DBNull.Value)
				{
					voRouting.SetupQuantity = decimal.Parse(pobjRow[ITM_RoutingTable.SETUPQUANTITY_FLD].ToString());
				}
				if(pobjRow[ITM_RoutingTable.STEP_FLD] != DBNull.Value)
				{
					voRouting.Step = int.Parse(pobjRow[ITM_RoutingTable.STEP_FLD].ToString());
				}
				if(pobjRow[ITM_RoutingTable.STUDYTIME_FLD] != DBNull.Value)
				{
					voRouting.StudyTime = decimal.Parse(pobjRow[ITM_RoutingTable.STUDYTIME_FLD].ToString());
				}
				if(pobjRow[ITM_RoutingTable.TYPE_FLD] != DBNull.Value)
				{
					voRouting.Type = int.Parse(pobjRow[ITM_RoutingTable.TYPE_FLD].ToString());
				}
				if(pobjRow[ITM_RoutingTable.VARLT_FLD] != DBNull.Value)
				{
					voRouting.VarLT = decimal.Parse(pobjRow[ITM_RoutingTable.VARLT_FLD].ToString());
				}
				if(pobjRow[ITM_RoutingTable.WORKCENTERID_FLD] != DBNull.Value)
				{
					voRouting.WorkCenterID = int.Parse(pobjRow[ITM_RoutingTable.WORKCENTERID_FLD].ToString());
				}
				if(pobjRow[ITM_RoutingTable.PACER_FLD] != DBNull.Value)
				{
					voRouting.Pacer = pobjRow[ITM_RoutingTable.PACER_FLD].ToString();
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

		//**************************************************************************              
		///    <Description>
		///       This method uses to update vo from controls on form
		///    </Description>
		///    <Inputs>
		///            
		///    </Inputs>
		///    <Outputs>
		///       
		///    </Outputs>
		///    <Returns>
		///       
		///    </Returns>
		///    <Authors>
		///       Hoang Trung Son - iSphere software
		///    </Authors>
		///    <History>
		///       Thursday, January 27, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private void ControlsToVO()
		{
			const string METHOD_NAME = THIS + ".ControlsToVO()";
			// store to VO
			if(voRouting == null) return;
			try
			{
				if(txtVendor.Text.Trim().Length > 0)
					voRouting.PartyID = int.Parse(txtVendor.Value.ToString());
				else voRouting.PartyID = 0;
				
				
				//if(FormControlComponents.IsNumeric(txtCost.Text))
				//{
					if (txtCost.Text == string.Empty)
					{
						voRouting.OSCost = 0;
					}
					else
						voRouting.OSCost = decimal.Parse(txtCost.Value.ToString());
				//}
//				if(FormControlComponents.IsNumeric(txtMachines.Text))
//				{
					if (txtMachines.Text != string.Empty)
					{
						voRouting.Machines = decimal.Parse(txtMachines.Value.ToString());
					}
					else
						voRouting.Machines = 0;
//				}
//                if(FormControlComponents.IsNumeric(txtTimeStudy.Text))
//                {
					if (txtTimeStudy.Text != string.Empty)
					{
						voRouting.StudyTime = decimal.Parse(txtTimeStudy.Value.ToString());
					}
					else
						voRouting.StudyTime = 0;
//                }

				if(cboLabCC.Text.Trim().Length > 0)
					voRouting.LaborCostCenterID = int.Parse(cboLabCC.SelectedValue.ToString());
				else voRouting.LaborCostCenterID = 0;
				
				if(cboMachCC.Text.Trim().Length > 0)

					voRouting.MachineCostCenterID = int.Parse(cboMachCC.SelectedValue.ToString());
				else voRouting.MachineCostCenterID = 0;
				

//				if(FormControlComponents.IsNumeric(txtMachRun.Text))
//				{
					if (txtMachRun.Text != string.Empty)
					{
						voRouting.MachineRunTime = decimal.Parse(txtMachRun.Value.ToString());
					}
					else
						voRouting.MachineRunTime = 0;
//				}
//				if(FormControlComponents.IsNumeric(txtLabRun.Text))
//				{
					if (txtLabRun.Text != string.Empty)
					{
						voRouting.LaborRunTime = decimal.Parse(txtLabRun.Value.ToString());
					}
					else
						voRouting.LaborRunTime = 0;
//				}
//				if(FormControlComponents.IsNumeric(txtMachSU.Text))
//				{
					if (txtMachSU.Text != string.Empty)
					{
						voRouting.MachineSetupTime = decimal.Parse(txtMachSU.Value.ToString());
					}
					else
						voRouting.MachineSetupTime = 0;
//				}
//				if(FormControlComponents.IsNumeric(txtLabSetup.Text))
//				{
					if (txtLabSetup.Text != string.Empty)
					{
						voRouting.LaborSetupTime = decimal.Parse(txtLabSetup.Value.ToString());
					}
					else
						voRouting.LaborSetupTime = 0;
//				}
//				if(FormControlComponents.IsNumeric(txtCrewSize.Text))
//				{
					if (txtCrewSize.Text != string.Empty)
					{
						voRouting.CrewSize = decimal.Parse(txtCrewSize.Value.ToString());
					}
					else
						voRouting.CrewSize = 0;
//				}
//
				if(pnlMachine.Visible == true) // if pnlMachine is visible
				{
					if(txtMoveTime.Text != string.Empty)
					{
						voRouting.MoveTime = decimal.Parse(txtMoveTime.Value.ToString());
					}
					else
						voRouting.MoveTime = 0;
//					if(FormControlComponents.IsNumeric(txtVarLeadTime.Text))
//					{
//						voRouting.VarLT = decimal.Parse(txtVarLeadTime.Text);
//					}
//					if(FormControlComponents.IsNumeric(txtFixLeadTime.Text))
//					{
//						voRouting.FixLT = decimal.Parse(txtFixLeadTime.Text);
//					}
				if(txtValue.Text != string.Empty)
				{
					voRouting.ScheduleSeq = 0;
					voRouting.OverlapPercent = 0;
					voRouting.OverlapQty = 0;
						
							
					if(radSchedSeq.Checked == true)
					{
						voRouting.ScheduleSeq = decimal.Parse(txtValue.Value.ToString());
					}
					else if(radOverlap.Checked == true)
					{
						voRouting.OverlapPercent = decimal.Parse(txtValue.Value.ToString());
					}
					else if(radOverlapQty.Checked == true)
					{
						voRouting.OverlapQty = decimal.Parse(txtValue.Value.ToString());
					}
				}
				else
				{
					voRouting.ScheduleSeq = 0;
					voRouting.OverlapPercent = 0;
					voRouting.OverlapQty = 0;
				}
					pnlOutside.Visible = true;
					if (!txtMoveTime2.Visible)
					{
						txtMoveTime2.Visible = true;
						txtMoveTime2.Value = txtMoveTime.Value;
						txtMoveTime2.Visible = false;
					}
					else
					{
						txtMoveTime2.Value = txtMoveTime.Value;
					}
//					txtVarLeadTime2.Text = txtVarLeadTime.Text;
//					txtFixLeadTime2.Text = txtFixLeadTime.Text;
					if (!txtValue2.Visible)
					{
						txtValue2.Visible = true;
						txtValue2.Value = txtValue.Value;
						txtValue2.Visible = false;
					}
					else
					{
						txtValue2.Value = txtValue.Value;
					}
					radSchedSeq2.Checked = radSchedSeq.Checked;
					radOverlap2.Checked = radOverlap.Checked;
					radOverlapQty2.Checked = radOverlapQty.Checked;
					pnlOutside.Visible = false;
				} 
				else if(pnlOutside.Visible == true) // if tabOutSide is visible
				//if (cboType.Text == OUTSIDE_PROCESS)
				{
					if(txtMoveTime2.Text != string.Empty)
					{
						voRouting.MoveTime = decimal.Parse(txtMoveTime2.Value.ToString());
					}
					else
						voRouting.MoveTime = 0;
					if(txtVarLeadTime2.Text != string.Empty)
					{
						voRouting.VarLT = decimal.Parse(txtVarLeadTime2.Value.ToString());
					}
					else
						voRouting.VarLT = 0;
					if(txtFixLeadTime2.Text != string.Empty)
					{
						voRouting.FixLT = decimal.Parse(txtFixLeadTime2.Value.ToString());
					}
					else
						voRouting.FixLT = 0;
					if(txtValue2.Text != string.Empty)
					{
						voRouting.ScheduleSeq = 0;
						voRouting.OverlapPercent = 0;
						voRouting.OverlapQty = 0;
						if(radSchedSeq2.Checked == true)
						{
							voRouting.ScheduleSeq = decimal.Parse(txtValue2.Value.ToString());
						}
						else if(radOverlap2.Checked == true)
						{
							voRouting.OverlapPercent = decimal.Parse(txtValue2.Value.ToString());
						}
						else if(radOverlapQty2.Checked == true)
						{
							voRouting.OverlapQty = decimal.Parse(txtValue2.Value.ToString());
						}
					}
					else
					{
						voRouting.ScheduleSeq = 0;
						voRouting.OverlapPercent = 0;
						voRouting.OverlapQty = 0;
					}
					pnlMachine.Visible = true;
					if (!txtMoveTime.Visible)
					{
						txtMoveTime.Visible = true;
						txtMoveTime.Value = txtMoveTime2.Value;
						txtMoveTime.Visible = false;
					}
					else
					{
						txtMoveTime.Value = txtMoveTime2.Value;
					}
//					txtVarLeadTime.Text = txtVarLeadTime2.Text;
//					txtFixLeadTime.Text = txtFixLeadTime2.Text;
					if (!txtValue.Visible)
					{
						txtValue.Visible = true;
						txtValue.Value = txtValue2.Value;
						txtValue.Visible = false;
					}
					else
					{
						txtValue.Value = txtValue2.Value;
					}
					radSchedSeq.Checked = radSchedSeq2.Checked;
					radOverlap.Checked = radOverlap2.Checked;
					radOverlapQty.Checked = radOverlapQty2.Checked;
					pnlMachine.Visible = false;
				}

//				if(FormControlComponents.IsNumeric(txtRunQty.Text))
//				{
					if (txtRunQty.Text != string.Empty)
					{
						voRouting.RunQuantity = decimal.Parse(txtRunQty.Value.ToString());
					}
					else
						voRouting.RunQuantity = 0;
//				
//				if(FormControlComponents.IsNumeric(txtSetupQty.Text))
//				{
					if(txtSetupQty.Text != string.Empty)
					{
						voRouting.SetupQuantity = decimal.Parse(txtSetupQty.Value.ToString());
					}
					else
						voRouting.SetupQuantity = 0;
//				}

//				if(cboStatus.Text.Trim().Length > 0)
//					voRouting.RoutingStatusID = int.Parse(cboStatus.SelectedValue.ToString());
//				else voRouting.RoutingStatusID = 0;
//				
				if(txtStatus.Text.Trim() != string.Empty)
					voRouting.RoutingStatusID = int.Parse(txtStatus.Tag.ToString());
				else voRouting.RoutingStatusID = 0;
//				if(cboWorkCenter.Text.Trim().Length > 0)
//					voRouting.WorkCenterID = int.Parse(cboWorkCenter.SelectedValue.ToString());
//				else voRouting.WorkCenterID = 0;
				if(txtWorkCenter.Text.Trim() != string.Empty)
					voRouting.WorkCenterID = int.Parse(txtWorkCenter.Tag.ToString());
				else voRouting.WorkCenterID = 0;					
				if(cboEndDate.Value != DBNull.Value)
				{
					voRouting.EffectEndDate = (DateTime)cboEndDate.Value;
				}
				else
				{
					voRouting.EffectEndDate = DateTime.MinValue;
				}

				if(cboBeginDate.Value != DBNull.Value)
				{
					voRouting.EffectBeginDate = (DateTime)cboBeginDate.Value;
				}
				else
				{
					voRouting.EffectBeginDate = DateTime.MinValue;
				}
				if(FormControlComponents.IsNumeric(txtStep.Text))
					voRouting.Step = int.Parse(txtStep.Value.ToString());

				if(cboType.Text == INSIDE_PROCESS)
					voRouting.Type = (int)OperationType.Inside;
				else voRouting.Type = (int)OperationType.Outside;

//				if(cboFunction.Text.Trim().Length > 0)
//					voRouting.FunctionID = int.Parse(cboFunction.SelectedValue.ToString());
//				else voRouting.FunctionID = 0;
				if(txtFunction.Text.Trim() != string.Empty)
					voRouting.FunctionID = int.Parse(txtFunction.Tag.ToString());
				else voRouting.FunctionID = 0;
				voRouting.Pacer = cboPacer.Text;

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

		//**************************************************************************              
		///    <Description>
		///       This method uses to update controls from vo
		///    </Description>
		///    <Inputs>
		///            
		///    </Inputs>
		///    <Outputs>
		///       
		///    </Outputs>
		///    <Returns>
		///       
		///    </Returns>
		///    <Authors>
		///       Hoang Trung Son - iSphere software
		///    </Authors>
		///    <History>
		///       Thursday, January 27, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private void VOToControls(ITM_RoutingVO pvoRouting)
		{
			const string METHOD_NAME = THIS + ".VOToControls()";
			const string PACER_DEFAULT = "B";
			RoutingBO boRouting = new RoutingBO();
			// store to VO
			if(pvoRouting == null) return;
			try
			{
				pnlOutside.Visible = pnlMachine.Visible = true;
				SetEditableControl(objControls,true);
				
				if(pvoRouting.PartyID > 0)
				{
					DataSet dstParty = new DataSet();
					dstParty = boRouting.ListParty();
					DataColumn[] objCols = new DataColumn[1];
					objCols[0] = dstParty.Tables[0].Columns[MST_PartyTable.PARTYID_FLD];
					dstParty.Tables[0].PrimaryKey = objCols;
					txtVendor.Text = dstParty.Tables[0].Rows.Find(pvoRouting.PartyID)[MST_PartyTable.CODE_FLD].ToString();
					txtVendor.Value = pvoRouting.PartyID;
				}
				else
				{
					txtVendor.Text = string.Empty;
					txtVendor.Value = null;
				}
				if (pvoRouting.OSCost != 0)
				{
					txtCost.Value = pvoRouting.OSCost;
				}
				else
					txtCost.Value = null;
				if (pvoRouting.Machines != 0)
				{
					txtMachines.Value = pvoRouting.Machines;
				}
				else
					txtMachines.Value = null;
				if (pvoRouting.StudyTime != 0)
				{
					txtTimeStudy.Value = pvoRouting.StudyTime;
				}
				else
					txtTimeStudy.Value = null;

				if (pvoRouting.LaborCostCenterID > 0)
					cboLabCC.SelectedValue = pvoRouting.LaborCostCenterID;
				else cboLabCC.Text = string.Empty;
				if (pvoRouting.MachineCostCenterID > 0)
					cboMachCC.SelectedValue = pvoRouting.MachineCostCenterID;
				else cboMachCC.Text = string.Empty;
				if (pvoRouting.MachineRunTime != 0)
				{
					txtMachRun.Value = pvoRouting.MachineRunTime;
				}
				else
					txtMachRun.Value = null;
				if (pvoRouting.LaborRunTime != 0)
				{
					txtLabRun.Value = pvoRouting.LaborRunTime;
				}
				else
					txtLabRun.Value = null;
				if (pvoRouting.MachineSetupTime != 0)
				{
					txtMachSU.Value = pvoRouting.MachineSetupTime;
				}
				else
					txtMachSU.Value = null;
				if (pvoRouting.LaborSetupTime != 0)
				{
					txtLabSetup.Value = pvoRouting.LaborSetupTime;
				}
				else
					txtLabSetup.Value = null;	
				if (pvoRouting.CrewSize != 0)
				{
					txtCrewSize.Value = pvoRouting.CrewSize;
				}
				else
					txtCrewSize.Value = null;
				if (pvoRouting.MoveTime != 0)
				{
					txtMoveTime.Value = pvoRouting.MoveTime;
				}
				else
					txtMoveTime.Value = null;
				if (txtMoveTime.Value.ToString() != string.Empty)
				{
					txtMoveTime2.Value = txtMoveTime.Value;
				}
				else
					txtMoveTime2.Value = null;
//				txtVarLeadTime.Value = pvoRouting.VarLT;
//				txtVarLeadTime2.Value = txtVarLeadTime.Value;
//				txtFixLeadTime.Value = pvoRouting.FixLT;
//				txtFixLeadTime2.Value = txtFixLeadTime.Value; 
				txtValue.Value = DBNull.Value;
				radLinenear.Checked = true;
				lblValue.Text = string.Empty;
				if(pvoRouting.ScheduleSeq > 0)
				{
					radSchedSeq.Checked = true;
					txtValue.Value = pvoRouting.ScheduleSeq;
				}
				if(pvoRouting.OverlapPercent > 0)
				{
					radOverlap.Checked = true;
					txtValue.Value = pvoRouting.OverlapPercent;
				}
				if(pvoRouting.OverlapQty > 0)
				{
					radOverlapQty.Checked = true;
					txtValue.Value = pvoRouting.OverlapQty;
				}
				radSchedSeq2.Checked = radSchedSeq.Checked;
				radOverlap2.Checked = radOverlap.Checked;
				radOverlapQty2.Checked = radOverlapQty.Checked;
				txtValue2.Value = txtValue.Value;
				lblValue2.Text = lblValue.Text;
				if (pvoRouting.RunQuantity != 0)
				{
					txtRunQty.Value = pvoRouting.RunQuantity;
				}
				else 
					txtRunQty.Value = null;
				if (pvoRouting.SetupQuantity != 0)
				{
					txtSetupQty.Value = pvoRouting.SetupQuantity;
				}
				else
					txtSetupQty.Value = null;
				if (pvoRouting.RoutingStatusID > 0)
					//cboStatus.SelectedValue = pvoRouting.RoutingStatusID;
				{
					txtStatus.Tag = pvoRouting.RoutingStatusID;
					DataSet dstStatus = new DataSet();
					dstStatus = boRouting.ListRoutingStatus();
					DataRow[] adrowRoutingStatus = dstStatus.Tables[0].Select(ITM_RoutingStatusTable.ROUTINGSTATUSID_FLD + " = " + pvoRouting.RoutingStatusID);
					if (adrowRoutingStatus.Length > 0)
					{
						txtStatus.Text = adrowRoutingStatus[0][ITM_RoutingStatusTable.CODE_FLD].ToString();
					}
				}
				else
				{
					txtStatus.Tag = null;
					txtStatus.Text = string.Empty;
				}
				if (pvoRouting.WorkCenterID > 0)
				{
					txtWorkCenter.Tag = pvoRouting.WorkCenterID;
					DataSet dstWorkCenter = new DataSet();
					dstWorkCenter = boRouting.ListWorkCenter();
					DataRow[] adrowWorkCenter = dstWorkCenter.Tables[0].Select(MST_WorkCenterTable.WORKCENTERID_FLD + " = " + pvoRouting.WorkCenterID);
					if (adrowWorkCenter.Length > 0)
					{
						txtWorkCenter.Text = adrowWorkCenter[0][MST_WorkCenterTable.CODE_FLD].ToString();
					}
				}
				else
				{
					txtWorkCenter.Tag = null;
					txtWorkCenter.Text = string.Empty;
				}

				if((DateTime.MinValue < pvoRouting.EffectEndDate) && (pvoRouting.EffectEndDate < DateTime.MaxValue))
				{
					cboEndDate.Value = pvoRouting.EffectEndDate;
				}
				else
				{
					cboEndDate.Value = DBNull.Value;
				}
				if((DateTime.MinValue < pvoRouting.EffectBeginDate) && (pvoRouting.EffectBeginDate < DateTime.MaxValue))
				{
					cboBeginDate.Value = pvoRouting.EffectBeginDate;
				}
				else
				{
					cboBeginDate.Value = DBNull.Value;
				}
				txtStep.Value = pvoRouting.Step;
				if (pvoRouting.Type == (int)OperationType.Inside)
				{
					cboType.Text = INSIDE_PROCESS;
					pnlOutside.Visible = false;
					pnlMachine.Visible = true;
				}
				else
				{
					cboType.Text = OUTSIDE_PROCESS;
					pnlOutside.Visible = true;
					pnlMachine.Visible = false;
				}

				if (pvoRouting.FunctionID > 0)
				{
					txtFunction.Tag = pvoRouting.FunctionID;
					DataSet dstFunction = new DataSet();
					dstFunction = boRouting.ListFunction();
					DataRow[] adrowFunction = dstFunction.Tables[0].Select(MST_FunctionTable.FUNCTIONID_FLD + " = " + pvoRouting.FunctionID);
					if (adrowFunction.Length > 0)
					{
						txtFunction.Text = adrowFunction[0][MST_FunctionTable.CODE_FLD].ToString();
						txtFunctionName.Text = adrowFunction[0][MST_FunctionTable.DESCRIPTION_FLD].ToString();
					}
				}
				else
				{
					txtFunction.Tag = null;
					txtFunction.Text = string.Empty;
					txtFunctionName.Text = string.Empty;
				}

				if (pvoRouting.Pacer != null)
					cboPacer.Text = pvoRouting.Pacer;
				else cboPacer.Text = PACER_DEFAULT;

				SetEditableControl(objControls,false);

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

		//**************************************************************************              
		///    <Description>
		///       This method uses to fill data from voproduc to controls
		///    </Description>
		///    <Inputs>
		///            
		///    </Inputs>
		///    <Outputs>
		///       
		///    </Outputs>
		///    <Returns>
		///       
		///    </Returns>
		///    <Authors>
		///       Hoang Trung Son - iSphere software
		///    </Authors>
		///    <History>
		///       Friday, January 28, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private void VOProductToControls()
		{
			//mProductID = 2;			// remove later
			RoutingBO boRouting = new RoutingBO();
			voProduct = (ITM_ProductVO) boRouting.GetProductInfo(mProductID);
			// Load Product item
			if(voProduct.CCNID > 0)
				cboCCN.SelectedValue = voProduct.CCNID;
			txtItem.Text = voProduct.Code;
			txtRevision.Text = voProduct.Revision;
			txtDescription.Text = voProduct.Description;
			txtRoutDescription.Text = voProduct.RoutingDescription;
			txtIncrement.Enabled = true;
			txtIncrement.Value = voProduct.RoutingIncrement;
			txtIncrement.Enabled = false;

			// added: dungla 15-02-2005
			// production line information
			if (voProduct.ProductionLineID > 0)
			{
				txtProductionLine.Text = boRouting.GetProductionLineCode(voProduct.ProductionLineID);
				txtProductionLine.Tag = voProduct.ProductionLineID;
			}
			// product group information
			if (voProduct.ProductGroupID > 0)
			{
				txtProductGroup.Text = boRouting.GetProductGroupCode(voProduct.ProductGroupID);
				txtProductGroup.Tag = voProduct.ProductGroupID;
			}
			// cost center rate master information
			if (voProduct.CostCenterRateMasterID > 0)
			{
				txtCostCenterRate.Text = boRouting.GetCostCenterMasterCode(voProduct.CostCenterRateMasterID);
				txtCostCenterRate.Tag = voProduct.CostCenterRateMasterID;
			}
			// end added: dungla 15-02-2005
		}

		#endregion DataRow -> VO -> Controls

		#region Load, list Save,Add,Edit,Delete,Cancel buttons
		//**************************************************************************              
		///    <Description>
		///       This method uses to load form
		///    </Description>
		///    <Inputs>
		///            
		///    </Inputs>
		///    <Outputs>
		///       
		///    </Outputs>
		///    <Returns>
		///       
		///    </Returns>
		///    <Authors>
		///       Hoang Trung Son - iSphere software
		///    </Authors>
		///    <History>
		///       Thursday, January 27, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private void Routing_Load(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".Routing_Load()";
			try
			{
				#region Security
				//Set authorization for user
				Security objSecurity = new Security();
				this.Name = THIS;
				if(objSecurity.SetRightForUserOnForm(this, SystemProperty.UserName) == 0)
				{
					// You don't have the right to view this item
					PCSMessageBox.Show(ErrorCode.MESSAGE_YOU_HAVE_NO_RIGHT_TO_VIEW, MessageBoxIcon.Warning);
					this.Close();					
					return;
				}
				#endregion

				// store caption 
				strMachineTabCaption = tabMachine.Text;
				strOutsideTabCaption = lblTabOutside.Text;

				// Select control to set enable or disable
				SetControls();

				// Load cboCCN
				ManageUserBO boManageUser = new ManageUserBO();
				DataSet dstCCN = boManageUser.ListCCN();
				FormControlComponents.PutDataIntoC1ComboBox(cboCCN,dstCCN.Tables[MST_CCNTable.TABLE_NAME],MST_CCNTable.CODE_FLD,MST_CCNTable.CCNID_FLD,MST_CCNTable.TABLE_NAME);
				
				// Load cboWorkCenter
				RoutingBO boRouting = new RoutingBO();
						
				// Load cboType
				cboType.Items.Add(INSIDE_PROCESS);
				cboType.Items.Add(OUTSIDE_PROCESS);
				cboType.Text = INSIDE_PROCESS;
				// Store dstParty
//				DataSet dstParty = new DataSet();
//				dstParty = boRouting.ListParty();

				// Load data for grid
				InitRouting();

				// Load cboMachCC, cboLabCC
				DataSet dstCostCenter = boRouting.ListCostCenter();
				DataRow objBlankRowCostCenter = dstCostCenter.Tables[ITM_CostCenterTable.TABLE_NAME].NewRow();
				dstCostCenter.Tables[ITM_CostCenterTable.TABLE_NAME].Rows.InsertAt(objBlankRowCostCenter,0);
				DataTable dtbDataTable = dstCostCenter.Tables[ITM_CostCenterTable.TABLE_NAME].Copy();
				FormControlComponents.PutDataIntoC1ComboBox(cboMachCC,dtbDataTable,ITM_CostCenterTable.CODE_FLD,ITM_CostCenterTable.COSTCENTERID_FLD,ITM_CostCenterTable.TABLE_NAME);
				FormControlComponents.PutDataIntoC1ComboBox(cboLabCC,dstCostCenter.Tables[ITM_CostCenterTable.TABLE_NAME],ITM_CostCenterTable.CODE_FLD,ITM_CostCenterTable.COSTCENTERID_FLD,ITM_CostCenterTable.TABLE_NAME);
				
				// call this function to load product
				btnLstCancel_Click(sender,e);
				txtIncrement.Enabled = txtRoutDescription.Enabled = true;
				txtIncrement.Value = 1;
				txtProductionLine.Enabled = true;
				btnProductionLine.Enabled = true;
				txtProductGroup.Enabled = true;
				btnProductGroup.Enabled = true;
				txtCostCenterRate.Enabled = true;
				btnCostCenterRate.Enabled = true;
				txtItem.Enabled = false;
				// edited: dungla 07-04-2006: fix bug for thuypt, allows user to update routing detail
				btnLstAdd.Enabled = true;
				btnLstCancel.Enabled = false;
				if (voRouting != null && voRouting.RoutingID > 0)
				{
					btnLstDelete.Enabled = true;
					btnLstEdit.Enabled = true;
				}
				else
				{
					btnLstDelete.Enabled = false;
					btnLstEdit.Enabled = false;
				}
				// end dungla 07-04-2006
				btnLstSave.Enabled = false;
				if (radLinenear.Checked)
				{
					lblValue.Text = string.Empty;
					txtValue.Enabled = false;
				}
				else
					txtValue.Enabled = true;
				//Config CustomFormat
				txtCost.CustomFormat = Constants.EDIT_NUM_MASK;
				txtCrewSize.CustomFormat = Constants.EDIT_NUM_MASK;
				txtFixLeadTime2.CustomFormat = Constants.EDIT_NUM_MASK;
				txtLabRun.CustomFormat = Constants.EDIT_NUM_MASK;
				txtLabSetup.CustomFormat = Constants.EDIT_NUM_MASK;
				txtMachines.CustomFormat = Constants.EDIT_NUM_MASK;
				txtMachRun.CustomFormat = Constants.EDIT_NUM_MASK;
				txtMachSU.CustomFormat = Constants.EDIT_NUM_MASK;
				txtMoveTime.CustomFormat = Constants.EDIT_NUM_MASK;
				txtMoveTime2.CustomFormat = Constants.EDIT_NUM_MASK;
				txtRunQty.CustomFormat = Constants.EDIT_NUM_MASK;
				txtSetupQty.CustomFormat = Constants.EDIT_NUM_MASK;
				txtTimeStudy.CustomFormat = Constants.EDIT_NUM_MASK;
				txtVarLeadTime2.CustomFormat = Constants.EDIT_NUM_MASK;
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
		//**************************************************************************              
		///    <Description>
		///       This method uses to set edit control
		///    </Description>
		///    <Inputs>
		///            
		///    </Inputs>
		///    <Outputs>
		///       
		///    </Outputs>
		///    <Returns>
		///       
		///    </Returns>
		///    <Authors>
		///       Hoang Trung Son - iSphere software
		///    </Authors>
		///    <History>
		///       Thursday, January 27, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private void btnLstEdit_Click(object sender, System.EventArgs e)
		{
			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.WaitCursor;

			#endregion Code Inserted Automatically

			
			const string METHOD_NAME = THIS + ".btnLstEdit_Click()";
			try
			{
				if (dstRouting.Tables[0].Rows.Count > 0)
				{
					// store current voResult to restore data if user click Cancel
					voOldRouting = voRouting;
					mFormAction = EnumAction.Edit;
					blnIsChanged = true;
					// set enable button
					//btnSave.Enabled = btnAdd.Enabled = btnEdit.Enabled = btnDelete.Enabled = false;
					SetEnableButtons();

					SetEditableControl(objControls,true);
					btnLstSave.Enabled = btnLstCancel.Enabled = true;
					btnLstAdd.Enabled = btnLstEdit.Enabled = btnLstDelete.Enabled = gridRouting.Enabled = false;
					txtStep.Focus();
					txtStep.SelectAll();
					if (radLinenear.Checked)
					{
						lblValue.Text = string.Empty;
						txtValue.Enabled = false;
					}
					else
					{
						txtValue.Enabled = true;
					}
					if (radLinenear2.Checked)
					{
						lblValue2.Text = string.Empty;
						txtValue2.Enabled = false;
					}
					else
					{
						txtValue2.Enabled = true;
					}
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

			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.Default;

			#endregion Code Inserted Automatically

			
		}

		//**************************************************************************              
		///    <Description>
		///       This method uses to delete record
		///    </Description>
		///    <Inputs>
		///            
		///    </Inputs>
		///    <Outputs>
		///       
		///    </Outputs>
		///    <Returns>
		///       
		///    </Returns>
		///    <Authors>
		///       Hoang Trung Son - iSphere software
		///    </Authors>
		///    <History>
		///       Thursday, January 27, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private void btnLstDelete_Click(object sender, System.EventArgs e)
		{
			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.WaitCursor;

			#endregion Code Inserted Automatically

			
			const string METHOD_NAME = THIS + ".btnLstDelete_Click()";
			try
			{
                if (gridRouting.RowCount > 0)
				{
					DialogResult enumResult = PCSMessageBox.Show(ErrorCode.MESSAGE_DELETE_RECORD,MessageBoxButtons.YesNo,MessageBoxIcon.Question);
					if(enumResult == DialogResult.Yes)
					{
						// fix bug for NgaHT, we will mark the selected row as Deleted from datasource
						// deleted selected row from source table
						gridRouting.Delete();
						//gridRouting.Update();

						voRouting = new ITM_RoutingVO();						
						// store data on form to voRouting
						VOToControls(voRouting);

						SetEditableControl(objControls,false);
						btnLstSave.Enabled = btnLstCancel.Enabled = false;
						btnLstAdd.Enabled = btnLstEdit.Enabled = btnLstDelete.Enabled = gridRouting.Enabled = true;
						btnSave.Enabled = true;
						blnIsChanged = true;

						#region Clearform
						// Clear form & assign form status
						txtVendor.Text = string.Empty;
						txtCost.Value = null;
						txtMoveTime.Value = null;
						txtMachines.Value = null;
						txtTimeStudy.Value = null;
						cboLabCC.Text = string.Empty;
						cboMachCC.Text = string.Empty;
						txtMachRun.Value = null;
						txtLabRun.Value = null;
						txtMachSU.Value = null;
						txtLabSetup.Value = null;
						txtCrewSize.Value = null;
						txtValue.Value = null;
						radLinenear.Checked = true;

						if (cboPacer.Items.Count == 0)
						{
							cboPacer.DataSource = null;
							cboPacer.Items.Add(BOTH);
							cboPacer.Items.Add(LABOR);
							cboPacer.Items.Add(MACHINE);
						}
						cboPacer.SelectedIndex = 0;
						//				txtFixLeadTime.Value = 0;
						txtRunQty.Value = null;
						txtSetupQty.Value = null;
						txtValue2.Value = null;
						radOverlapQty2.Checked = true;
						txtMoveTime2.Value = null;
						txtVarLeadTime2.Value = null;
						txtFixLeadTime2.Value = null;
//						cboStatus.Text = string.Empty;
//						cboWorkCenter.Text = string.Empty;
						cboEndDate.Value = DBNull.Value;
						cboBeginDate.Value = DBNull.Value;
						txtStep.Value = null;//GetStepValue();//= txtIncrement.Value + [max step in routing];
						//txtStep.Text = string.Empty;
						cboType.Text = INSIDE_PROCESS;
//						cboFunction.Text = string.Empty;
						txtFunction.Text = string.Empty;
						txtFunction.Tag = null;
						txtWorkCenter.Text = string.Empty;
						txtWorkCenter.Tag = null;
						txtStatus.Text = string.Empty;
						txtStatus.Tag = null;
						txtFunctionName.Text = string.Empty;
						//cboPacer.SelectedIndex = 0;
						// set focus
						#endregion

						btnAdd.Enabled = false;
						btnDelete.Enabled = false;
						btnEdit.Enabled = false;

                        if (gridRouting.RowCount > 0)
						{
							// update record
							object objKey;
							voRouting = new ITM_RoutingVO();
							objKey = gridRouting[gridRouting.Row,ITM_RoutingTable.ROUTINGID_FLD];
							voRouting.RoutingID = int.Parse(objKey.ToString());
							DataRow objRow = dstRouting.Tables[ITM_RoutingTable.TABLE_NAME].Rows.Find(objKey);
							// load data from DataRow to VO
							DataRowToVO(ref objRow);
							// store data on form to voRouting
							VOToControls(voRouting);
						}
					}
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

			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.Default;

			#endregion Code Inserted Automatically

			
		}

		//**************************************************************************              
		///    <Description>
		///       This method uses to cancel edit
		///    </Description>
		///    <Inputs>
		///            
		///    </Inputs>
		///    <Outputs>
		///       
		///    </Outputs>
		///    <Returns>
		///       
		///    </Returns>
		///    <Authors>
		///       Hoang Trung Son - iSphere software
		///    </Authors>
		///    <History>
		///       Thursday, January 27, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private void btnLstCancel_Click(object sender, System.EventArgs e)
		{
			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.WaitCursor;

			#endregion Code Inserted Automatically

			
			const string METHOD_NAME = THIS + ".btnLstCancel_Click()";
			try
			{
				btnLstSave.Enabled = btnLstCancel.Enabled = false;
				btnLstAdd.Enabled = btnLstEdit.Enabled = btnLstDelete.Enabled = gridRouting.Enabled = true;
				mFormAction = EnumAction.Default;
				if(voOldRouting.RoutingID > 0)
					VOToControls(voOldRouting);
				SetEditableControl(objControls,false);
				// set for parent group buttons
				//SetEnableButtons();
				btnAdd.Enabled = btnEdit.Enabled = btnDelete.Enabled = false;
				btnSave.Enabled = true;
				btnFindItem.Enabled = false;
				txtIncrement.Enabled = txtRoutDescription.Enabled = false;
				// Restore old data on form
				VOProductToControls();
				//Clear form
                if (gridRouting.RowCount == 0)
					ClearForm();
                // 17-04-2006 dungla: fix bug for ThuyPT: when user press Cancel button and Save form Master
				blnIsLstSaveSuccess = true;
				// 17-04-2006 dungla: fix bug for ThuyPT: when user press Cancel button and Save form Master
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


			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.Default;

			#endregion Code Inserted Automatically

			
		}
		/// <summary>
		/// ClearForm
		/// </summary>
		/// <author>Trada</author>
		/// <date>Tuesday, October 11 2005</date>
		private void ClearForm()
		{
			const string METHOD_NAME = THIS + ".void()";
			try
			{
				txtStep.Value = null;
				cboType.SelectedValue = null;
//				cboFunction.Text = string.Empty;
//				cboWorkCenter.Text = string.Empty;
//				cboStatus.Text = string.Empty;
				txtFunction.Text = string.Empty;
				txtFunction.Tag = null;
				txtWorkCenter.Tag = null;
				txtStatus.Tag = null;
				txtWorkCenter.Text = string.Empty;	
				txtStatus.Text = string.Empty;	
				txtCost.Value = null;
				txtCrewSize.Value = null;
				txtFixLeadTime2.Value = null;
				txtLabRun.Value = null;
				txtLabSetup.Value = null;
				txtMachines.Value = null;
				txtMachRun.Value = null;
				txtMachSU.Value = null;
				txtMoveTime.Value = null;
				txtMoveTime2.Value = null;
				txtRunQty.Value = null;
				txtSetupQty.Value = null;
				txtTimeStudy.Value = null;
				txtValue.Value = null;
				txtValue2.Value = null;
				txtVarLeadTime2.Value = null;
				txtVendor.Value = null;
				cboBeginDate.Value = DBNull.Value;
				cboEndDate.Value = DBNull.Value;
				cboLabCC.Text = string.Empty;
				cboMachCC.Text = string.Empty;
				cboPacer.Text = string.Empty;
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
		//**************************************************************************              
		///    <Description>
		///       This method uses to add a record
		///    </Description>
		///    <Inputs>
		///            
		///    </Inputs>
		///    <Outputs>
		///       
		///    </Outputs>
		///    <Returns>
		///       
		///    </Returns>
		///    <Authors>
		///       Hoang Trung Son - iSphere software
		///    </Authors>
		///    <History>
		///       Thursday, January 27, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private void btnLstAdd_Click(object sender, System.EventArgs e)
		{
			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.WaitCursor;

			#endregion Code Inserted Automatically

			
			const string METHOD_NAME = THIS + ".btnLstAdd_Click()";
			try
			{
				// go to last row 
				gridRouting.MoveLast();
				// store current voResult to restore data if user click Cancel
				voOldRouting = voRouting;
				mFormAction = EnumAction.Add;
				blnIsChanged = true;
				SetEditableControl(objControls,true);
				btnLstSave.Enabled = btnLstCancel.Enabled = true;
				btnLstAdd.Enabled = btnLstEdit.Enabled = btnLstDelete.Enabled = gridRouting.Enabled = false;
				// set enable button
				//btnSave.Enabled = btnAdd.Enabled = btnEdit.Enabled = btnDelete.Enabled = false;
				SetEnableButtons();

				// Clear form & assign form status
				txtVendor.Text = string.Empty;
				txtCost.Value = null;
				txtMoveTime.Value = null;
				txtMachines.Value = null;
				txtTimeStudy.Value = null;
				cboLabCC.Text = string.Empty;
				cboMachCC.Text = string.Empty;
				txtMachRun.Value = null;
				txtLabRun.Value = null;
				txtMachSU.Value = null;
				txtLabSetup.Value = null;
				txtCrewSize.Value = null;
				txtValue.Value = null;
				radLinenear.Checked = true;

				if (cboPacer.Items.Count == 0)
				{
					cboPacer.DataSource = null;
					cboPacer.Items.Add(BOTH);
					cboPacer.Items.Add(LABOR);
					cboPacer.Items.Add(MACHINE);
				}
				cboPacer.SelectedIndex = 0;
				txtRunQty.Value = null;
				txtSetupQty.Value = null;
				txtValue2.Value = null;
				radLinenear2.Checked = true;
				txtMoveTime2.Value = null;
				txtVarLeadTime2.Value = null;
				txtFixLeadTime2.Value = null;
				cboEndDate.Value = DBNull.Value;
				cboBeginDate.Value = DBNull.Value;
				txtStep.Value = GetStepValue();//= txtIncrement.Value + [max step in routing];
				cboType.Text = INSIDE_PROCESS;
				txtFunction.Text = string.Empty;
				txtFunction.Tag = null;
				txtWorkCenter.Text = string.Empty;
				txtWorkCenter.Tag = null;
				txtStatus.Text = string.Empty;
				txtStatus.Tag = null;
				txtFunctionName.Text = string.Empty;
				// set focus
				txtStep.Focus();
				txtStep.SelectAll();
				if (radLinenear.Checked)
				{
					lblValue.Text = string.Empty;
					txtValue.Enabled = false;
				}
				else
				{
					txtValue.Enabled = true;
				}
				if (radLinenear2.Checked)
				{
					lblValue2.Text = string.Empty;
					txtValue2.Enabled = false;
				}
				else
				{
					txtValue2.Enabled = true;
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

			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.Default;

			#endregion Code Inserted Automatically

			
		}

		//**************************************************************************              
		///    <Description>
		///       This method uses to save data on form into dataset
		///    </Description>
		///    <Inputs>
		///            
		///    </Inputs>
		///    <Outputs>
		///       
		///    </Outputs>
		///    <Returns>
		///       
		///    </Returns>
		///    <Authors>
		///       Hoang Trung Son - iSphere software
		///    </Authors>
		///    <History>
		///       Thursday, January 27, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private void btnLstSave_Click(object sender, System.EventArgs e)
		{
			// Code Inserted Automatically

			#region Code Inserted Automatically

			// this.Cursor = Cursors.WaitCursor;

			#endregion Code Inserted Automatically

			
			const string METHOD_NAME = THIS + ".btnLstSave_Click()";
			if(CheckMandatory(txtStep))
			{
				PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID,MessageBoxIcon.Exclamation);
				blnIsLstSaveSuccess = false;
				txtStep.Focus();
				return;
			}
			// 5.a If the user change the Step value then the system display error message 
			// and restore value if the current Step is smaller then previous step
			DataRow[] arrRow = dstRouting.Tables[0].Select(ITM_RoutingTable.STEP_FLD + "='" + txtStep.Text.Replace("'", "''") + "'");

			if(FormAction == EnumAction.Edit)
			{
				if(arrRow.Length == 1 && !arrRow[0][ITM_RoutingTable.ROUTINGID_FLD].Equals(voRouting.RoutingID))
				{
					PCSMessageBox.Show(ErrorCode.DUPLICATE_KEY, MessageBoxIcon.Exclamation);
					txtStep.Focus();
					blnIsLstSaveSuccess = false;
					return;
				}

				if(IsIncorrectUpdateStep())
				{
					PCSMessageBox.Show(ErrorCode.MESSAGE_STEP_GREATER_THAN,MessageBoxIcon.Exclamation);
					txtStep.Focus();
					blnIsLstSaveSuccess = false;
					return;
				}
			}

			if(FormAction == EnumAction.Add)
			{
				if(arrRow.Length == 1)
				{
					PCSMessageBox.Show(ErrorCode.DUPLICATE_KEY, MessageBoxIcon.Exclamation);
					txtStep.Focus();
					blnIsLstSaveSuccess = false;
					return;
				}

				if(IsIncorrectAddNewStep())
				{
					PCSMessageBox.Show(ErrorCode.MESSAGE_STEP_GREATER_THAN,MessageBoxIcon.Exclamation);
					txtStep.Focus();
					blnIsLstSaveSuccess = false;
					return;
				}
			}

			if(CheckMandatory(txtWorkCenter))
			{
				PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID,MessageBoxIcon.Exclamation);
				txtWorkCenter.Focus();
				blnIsLstSaveSuccess = false;
				return;
			}
			if(CheckMandatory(cboType))
			{
				PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID,MessageBoxIcon.Exclamation);
				cboType.Focus();
				blnIsLstSaveSuccess = false;
				return;
			}
			
			if(CheckMandatory(txtFunction))
			{
				PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID,MessageBoxIcon.Exclamation);
				txtFunction.Focus();
				blnIsLstSaveSuccess = false;
				return;
			}

			if(IsNotExistVendor())
			{
				PCSMessageBox.Show(ErrorCode.MESSAGE_VENDOR_DONT_EXIST,MessageBoxIcon.Exclamation);
				txtVendor.Focus();
				blnIsLstSaveSuccess = false;
				return;
			}
			
			if((cboBeginDate.Value != DBNull.Value) && (cboEndDate.Value != DBNull.Value))
			{
				if((DateTime)cboBeginDate.Value > (DateTime)cboEndDate.Value)
				{
					PCSMessageBox.Show(ErrorCode.MESSAGE_ENDDATE_GREATER_THAN_BEGINDATE,MessageBoxIcon.Exclamation);
					cboBeginDate.Focus();
					blnIsLstSaveSuccess = false;
					return;
				}
			}
			//Check Pacer
			if(cboType.Text == INSIDE_PROCESS)
			switch (cboPacer.SelectedIndex)
			{
				case 0:
					if ((txtMachRun.Value == DBNull.Value) || ((Decimal)txtMachRun.Value <= 0))
					{
						PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID,MessageBoxIcon.Warning);
						txtMachRun.Focus();
						blnIsLstSaveSuccess = false;
						return;
					}
					if ((txtLabRun.Value == DBNull.Value) || ((Decimal)txtLabRun.Value <= 0))
					{
						PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID,MessageBoxIcon.Warning);
						txtLabRun.Focus();
						blnIsLstSaveSuccess = false;
						return;
					}
					break;
				case 1:
					if ((txtLabRun.Value == DBNull.Value) || ((Decimal)txtLabRun.Value <= 0))
					{
						PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID,MessageBoxIcon.Warning);
						txtLabRun.Focus();
						blnIsLstSaveSuccess = false;
						return;
					}
					break;
				case 2:
					if ((txtMachRun.Value == DBNull.Value) || ((Decimal)txtMachRun.Value <= 0))
					{
						PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID,MessageBoxIcon.Warning);
						txtMachRun.Focus();
						blnIsLstSaveSuccess = false;
						return;
					}
					break;
			}
			// selected work center is not in select production line
			RoutingBO boRouting = new RoutingBO();
			bool blnIsWorkCenterInProductionLine = boRouting.IsWorkCenterInProductionLine(Convert.ToInt32(txtProductionLine.Tag), Convert.ToInt32(txtWorkCenter.Tag));
			if (!blnIsWorkCenterInProductionLine)
			{
				PCSMessageBox.Show(ErrorCode.MESSAGE_SELECTED_WC_NOT_IN_PRODUCTIONLINE, MessageBoxIcon.Exclamation);
				txtWorkCenter.Focus();
				blnIsLstSaveSuccess = false;
				return;
			}
			// product can go thru a work center one time only.
			string strFilter = ITM_RoutingTable.WORKCENTERID_FLD + "='" + txtWorkCenter.Tag.ToString() + "'";
			if (mFormAction == EnumAction.Edit)
				strFilter += " AND " + ITM_RoutingTable.ROUTINGID_FLD + " <> '" + voRouting.RoutingID + "'" ;
			DataRow[] drowsWorkCenter = dstRouting.Tables[0].Select(strFilter);
			if (drowsWorkCenter.Length > 0)
			{
				PCSMessageBox.Show(ErrorCode.DUPLICATE_KEY, MessageBoxIcon.Exclamation);
				txtWorkCenter.Focus();
				blnIsLstSaveSuccess = false;
				return;
			}
			try
			{
				ControlsToVO();
				// update dataset routing
				voRouting.ProductID = mProductID;
				if(mFormAction == EnumAction.Add)
				{
					DataRow objRow;
					DataTable objTable = (DataTable) gridRouting.DataSource;
					objRow = objTable.NewRow();
					objRow[ITM_RoutingTable.ROUTINGID_FLD] = ++voRouting.RoutingID;
					VOToDataRow(ref objRow);
					dstRouting.Tables[ITM_RoutingTable.TABLE_NAME].Columns[ITM_RoutingTable.ROUTINGID_FLD].AutoIncrement = true;
					dstRouting.Tables[ITM_RoutingTable.TABLE_NAME].Rows.Add(objRow);
					gridRouting.UpdateData();
					gridRouting.MoveLast();
				}
				else if(mFormAction == EnumAction.Edit)
				{
					// update record
					object objKey;
					objKey = voRouting.RoutingID;
					DataRow objRow = dstRouting.Tables[ITM_RoutingTable.TABLE_NAME].Rows.Find(objKey);
					VOToDataRow(ref objRow);
				}

				VOToControls(voRouting);
				btnLstSave.Enabled = btnLstCancel.Enabled = false;
				btnLstAdd.Enabled = btnLstEdit.Enabled = btnLstDelete.Enabled = gridRouting.Enabled = true;
				blnIsChanged = true;
				mFormAction = EnumAction.Default;
				SetEditableControl(objControls,false);
				btnLstAdd.Focus();
				blnIsLstSaveSuccess = true;
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

			// Code Inserted Automatically

			#region Code Inserted Automatically

			//this.Cursor = Cursors.Default;

			#endregion Code Inserted Automatically

			
		}

		#endregion List Save,Add,Edit,Delete,Cancel buttons

		#region Add,Save,Edit,Delete buttons
		//**************************************************************************              
		///    <Description>
		///       This method uses to add new form
		///    </Description>
		///    <Inputs>
		///            
		///    </Inputs>
		///    <Outputs>
		///       
		///    </Outputs>
		///    <Returns>
		///       
		///    </Returns>
		///    <Authors>
		///       Hoang Trung Son - iSphere software
		///    </Authors>
		///    <History>
		///       Thursday, January 27, 2005
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

			
			const string METHOD_NAME = THIS + ".METHOD_NAME()";
			try
			{
				// clear control
				blnIsChanged = true;
				txtItem.Text = string.Empty;
				if(SystemProperty.CCNID > 0)
					cboCCN.SelectedValue = SystemProperty.CCNID;
				else cboCCN.Text = string.Empty;
				txtRevision.Text = string.Empty;
				txtDescription.Text = string.Empty;			
				txtRoutDescription.Text = string.Empty;
				txtIncrement.Enabled = txtRoutDescription.Enabled = false;
				txtProductionLine.Enabled = true;
				btnProductionLine.Enabled = true;
				txtProductGroup.Enabled = true;
				txtProductGroup.Text = string.Empty;
				txtProductGroup.Tag = null;
				btnProductGroup.Enabled = true;
				txtCostCenterRate.Enabled = true;
				txtCostCenterRate.Text = string.Empty;
				txtCostCenterRate.Tag = null;
				btnCostCenterRate.Enabled = true;
				txtProductionLine.Text = string.Empty;
				txtProductionLine.Tag = null;
				txtIncrement.Value = 1;
				// set enable button
//				btnSave.Enabled = 
				btnFindItem.Enabled = true;
//				btnAdd.Enabled = btnEdit.Enabled = btnDelete.Enabled = false;
				mFormAction = EnumAction.Add;
				SetEnableButtons();
				btnLstAdd.Enabled = btnLstEdit.Enabled = btnLstDelete.Enabled = true;
				btnLstSave.Enabled = btnLstCancel.Enabled  = false;
				// set focus
				btnFindItem.Focus();
				btnLstAdd.Enabled = false;
				btnLstCancel.Enabled = false;
				btnLstDelete.Enabled = false;
				btnLstEdit.Enabled = false;
				btnLstSave.Enabled = false;
				// Load data for grid
				mProductID = 0;
				InitRouting();
				//Clear routing detail
				ClearForm();
				
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

			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.Default;

			#endregion Code Inserted Automatically

			
		}

		//**************************************************************************              
		///    <Description>
		///       This method uses to save data into database
		///    </Description>
		///    <Inputs>
		///            
		///    </Inputs>
		///    <Outputs>
		///       
		///    </Outputs>
		///    <Returns>
		///       
		///    </Returns>
		///    <Authors>
		///       Hoang Trung Son - iSphere software
		///    </Authors>
		///    <History>
		///       Thursday, January 27, 2005
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
				if(CheckMandatory(txtItem))
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID,MessageBoxIcon.Exclamation);
					btnFindItem.Focus();
					return;
				}
				if(CheckMandatory(cboCCN))
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID,MessageBoxIcon.Exclamation);
					cboCCN.Focus();
					return;
				}
				if(CheckMandatory(txtIncrement))
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID,MessageBoxIcon.Exclamation);
					txtIncrement.Focus();
					return;
				}
				if(txtProductionLine.Tag == null)
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID,MessageBoxIcon.Exclamation);
					txtProductionLine.Focus();
					return;
				}
				if(txtProductGroup.Tag == null)
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID,MessageBoxIcon.Exclamation);
					txtProductGroup.Focus();
					return;
				}
				if(txtCostCenterRate.Tag == null)
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID,MessageBoxIcon.Exclamation);
					txtCostCenterRate.Focus();
					return;
				}
				RoutingBO boRouting = new RoutingBO();
				// gets list of work center
				string strWorkCenterIDs = string.Empty;
				foreach (DataRow drowWC in dstRouting.Tables[0].Rows)
				{
					if (drowWC.RowState != DataRowState.Deleted)
						strWorkCenterIDs += drowWC[MST_WorkCenterTable.WORKCENTERID_FLD].ToString() + ",";
				}
				// remove the last comma
				if (strWorkCenterIDs.IndexOf(",") >= 0)
					strWorkCenterIDs = strWorkCenterIDs.Substring(0, strWorkCenterIDs.Length - 1);
				// check for selected work center and production line in case of user change production line
				bool blnHasWorkCenterNotInProductionLine = boRouting.HasWorkCenterNotInProductionLine(Convert.ToInt32(txtProductionLine.Tag), strWorkCenterIDs);
				if (blnHasWorkCenterNotInProductionLine)
				{
					PCSMessageBox.Show(ErrorCode.MESSAGE_AT_LEAST_WC_INVALID,MessageBoxIcon.Exclamation);
					txtProductionLine.Focus();
					return;
				}
				if(btnLstSave.Enabled == true)
					btnLstSave_Click(sender,e);
				else
				{
					btnLstAdd.Enabled = btnLstEdit.Enabled = btnLstDelete.Enabled = true;
					btnLstSave.Enabled = btnLstCancel.Enabled = false;
				}
				if (blnIsLstSaveSuccess)
				{
					boRouting.UpdateDataSet(dstRouting);
					if(FormControlComponents.IsNumeric(txtIncrement.Text))
						voProduct.RoutingIncrement = int.Parse(txtIncrement.Text);
					else
					{
						PCSMessageBox.Show(ErrorCode.MESSAGE_INVALID_NUMERIC,MessageBoxIcon.Exclamation);
						txtIncrement.Focus();
						txtIncrement.SelectAll();
					}
					voProduct.RoutingDescription = txtRoutDescription.Text.Trim();
					// cost center rate master id
					voProduct.CostCenterRateMasterID = Convert.ToInt32(txtCostCenterRate.Tag);
					// production line id
					voProduct.ProductionLineID = Convert.ToInt32(txtProductionLine.Tag);
					// product group id
					voProduct.ProductGroupID = Convert.ToInt32(txtProductGroup.Tag);
					//boProduct.Update(voProduct);
					boRouting.UpdateRoutingDescription(voProduct);
					blnIsChanged = false;
					//				btnSave.Enabled = 
					btnFindItem.Enabled = false;
					//				btnAdd.Enabled = btnEdit.Enabled = btnDelete.Enabled = true;
					mFormAction = EnumAction.Default;
					SetEnableButtons();
					txtIncrement.Enabled = txtRoutDescription.Enabled = false;
					txtProductionLine.Enabled = false;
					btnProductionLine.Enabled = false;
					txtProductGroup.Enabled = false;
					btnProductGroup.Enabled = false;
					txtCostCenterRate.Enabled = false;
					btnCostCenterRate.Enabled = false;
					PCSMessageBox.Show(ErrorCode.MESSAGE_AFTER_SAVE_DATA);
					// Reload data for grid
					InitRouting();
					btnLstAdd.Focus();
				}
			}
			catch (PCSException ex)
			{
				// if any error, rejects all changes in grid
				dstRouting.RejectChanges();
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

			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.Default;

			#endregion Code Inserted Automatically

			
		}

		//**************************************************************************              
		///    <Description>
		///       This method uses to set form editable
		///    </Description>
		///    <Inputs>
		///            
		///    </Inputs>
		///    <Outputs>
		///       
		///    </Outputs>
		///    <Returns>
		///       
		///    </Returns>
		///    <Authors>
		///       Hoang Trung Son - iSphere software
		///    </Authors>
		///    <History>
		///       Thursday, January 27, 2005
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

			
			blnIsChanged = true;
			txtIncrement.Enabled = txtRoutDescription.Enabled = true;
			txtProductionLine.Enabled = true;
			btnProductionLine.Enabled = true;
			txtProductGroup.Enabled = true;
			btnProductGroup.Enabled = true;
			txtCostCenterRate.Enabled = true;
			btnCostCenterRate.Enabled = true;
			// set enable button
//			btnSave.Enabled = true;
			btnFindItem.Enabled = false;
//			btnAdd.Enabled = btnEdit.Enabled = btnDelete.Enabled = false;		
			mFormAction = EnumAction.Edit;
			SetEnableButtons();

			btnLstAdd.Enabled = btnLstSave.Enabled = btnLstEdit.Enabled = 
				btnLstDelete.Enabled = btnLstCancel.Enabled  = false;
			// set focus
			txtIncrement.Focus();

			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.Default;

			#endregion Code Inserted Automatically

			
		}
		//**************************************************************************              
		///    <Description>
		///       This method uses to delete data form
		///    </Description>
		///    <Inputs>
		///            
		///    </Inputs>
		///    <Outputs>
		///       
		///    </Outputs>
		///    <Returns>
		///       
		///    </Returns>
		///    <Authors>
		///       Hoang Trung Son - iSphere software
		///    </Authors>
		///    <History>
		///       Thursday, January 27, 2005
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
				// if exist product then excute deleted, else do nothing
				if (voProduct.ProductID > 0)	
				{
					DialogResult enumResult = PCSMessageBox.Show(ErrorCode.MESSAGE_DELETE_RECORD,MessageBoxButtons.YesNo,MessageBoxIcon.Question);
					if(enumResult == DialogResult.Yes)
					{
						
						RoutingBO boRouting = new RoutingBO();
						
						// Delete all row in the routing grid
						//if(dstRouting.Tables.Count > 0)
						foreach(DataRow objRow in dstRouting.Tables[0].Rows)
							if(objRow.RowState != DataRowState.Deleted) objRow.Delete();
						// 08-04-2006 dungla: fix bug for NgaHT: when delete routing which already in use by DCP
						// update to database first
						boRouting.UpdateDataSet(dstRouting);
						// if succeed, update grid
						gridRouting.Update();
						// 08-04-2006 dungla
						//Delete Routing information in Product table
						voProduct.RoutingDescription = string.Empty;
						voProduct.RoutingIncrement = 0;
						voProduct.ProductionLineID = 0;
						voProduct.ProductGroupID = 0;
						voProduct.CostCenterRateMasterID = 0;
						boRouting.UpdateRoutingDescription(voProduct);
						// Set enable button and clear info on form
						blnIsChanged = false;
						//btnAdd.Enabled = true;
						//btnDelete.Enabled = btnSave.Enabled = btnEdit.Enabled = false;
						mFormAction = EnumAction.Delete;
						SetEnableButtons();
						
						btnFindItem.Enabled = txtIncrement.Enabled = txtRoutDescription.Enabled = false;
						// clear routing information in child form
						voRouting = new ITM_RoutingVO();
						VOToControls(voRouting);
						// Clear routing product information on form
						voProduct = new ITM_ProductVO();
						txtItem.Text = txtRevision.Text = txtDescription.Text = txtRoutDescription.Text = txtProductionLine.Text = string.Empty;
						txtProductGroup.Text = txtCostCenterRate.Text = string.Empty;
						txtProductionLine.Tag = txtProductGroup.Tag = txtCostCenterRate.Tag = null;
						txtIncrement.Enabled = true;
						txtIncrement.Value = 1;
						txtIncrement.Enabled = false;
						btnAdd.Enabled = true;
						btnSave.Enabled = false;
						btnLstAdd.Enabled = false;
						btnLstCancel.Enabled = false;
						btnLstDelete.Enabled = false;
						btnLstEdit.Enabled = false;
						btnLstSave.Enabled = false;
						txtIncrement.Value = null;
						
					}
				}
			}
			catch (PCSException ex)
			{
				// reject changes if error
				dstRouting.RejectChanges();
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

			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.Default;

			#endregion Code Inserted Automatically

			
		}

		#endregion Add,Save,Edit,Delete buttons

		#region Other events


		//**************************************************************************              
		///    <Description>
		///       This method uses to selected index changed
		///    </Description>
		///    <Inputs>
		///            
		///    </Inputs>
		///    <Outputs>
		///       
		///    </Outputs>
		///    <Returns>
		///       
		///    </Returns>
		///    <Authors>
		///       Hoang Trung Son - iSphere software
		///    </Authors>
		///    <History>
		///       Thursday, January 27, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private void cboType_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if(cboType.Text == INSIDE_PROCESS)
			{
				tabMachine.Text = strMachineTabCaption;
				pnlMachine.Show();
				pnlOutside.Hide();
				pnlOutside.Visible = false;
				pnlMachine.Visible = true;
			}
			else if(cboType.Text == OUTSIDE_PROCESS)
			{
				tabMachine.Text = strOutsideTabCaption;
				pnlMachine.Hide();
				pnlOutside.Show();
				pnlOutside.Visible = true;
				pnlMachine.Visible = false;
			}
		}


		private void btnHelp_Click(object sender, System.EventArgs e)
		{
		// Code Inserted Automatically

		#region Code Inserted Automatically

		this.Cursor = Cursors.WaitCursor;

		#endregion Code Inserted Automatically

		
		

		// Code Inserted Automatically

		#region Code Inserted Automatically

		this.Cursor = Cursors.Default;

		#endregion Code Inserted Automatically

		
		}

		//**************************************************************************              
		///    <Description>
		///       This method uses to close form
		///    </Description>
		///    <Inputs>
		///            
		///    </Inputs>
		///    <Outputs>
		///       
		///    </Outputs>
		///    <Returns>
		///       
		///    </Returns>
		///    <Authors>
		///       Hoang Trung Son - iSphere software
		///    </Authors>
		///    <History>
		///       Thursday, January 27, 2005
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
		///       This method uses to find item
		///    </Description>
		///    <Inputs>
		///            
		///    </Inputs>
		///    <Outputs>
		///       
		///    </Outputs>
		///    <Returns>
		///       
		///    </Returns>
		///    <Authors>
		///       Hoang Trung Son - iSphere software
		///    </Authors>
		///    <History>
		///       Thursday, January 27, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private void btnFindItem_Click(object sender, System.EventArgs e)
		{
			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.WaitCursor;

			#endregion Code Inserted Automatically

			
			const string METHOD_NAME = THIS + ".btnFindItem_Click()";
			const string MAKE_ITEM = "1";
			try
			{
				voProduct = new ITM_ProductVO();

				// Open form to select Product
//				ViewTable frmViewTableForm = new ViewTable(ITM_ProductTable.TABLE_NAME);
//				frmViewTableForm.ViewOnly = true;
//				frmViewTableForm.GetData = true;
//				frmViewTableForm.ReturnField = ITM_ProductTable.PRODUCTID_FLD;
//				frmViewTableForm.FilterField1 = ITM_ProductTable.CODE_FLD;
//				frmViewTableForm.FilterFieldValue1 = txtItem.Text.Trim();
//				frmViewTableForm.FilterField2 = ITM_ProductTable.MAKEITEM_FLD;
//				frmViewTableForm.FilterFieldValue2 = MAKE_ITEM;
				Hashtable htbCondition = new Hashtable();
				htbCondition.Add(ITM_ProductTable.MAKEITEM_FLD,MAKE_ITEM);
				DataRowView drowView = FormControlComponents.OpenSearchForm(ITM_ProductTable.TABLE_NAME,ITM_ProductTable.CODE_FLD,txtItem.Text.Trim(),htbCondition,true);
				//if (frmViewTableForm.ShowDialog() == DialogResult.OK)
				if(drowView != null)
				{
					//mProductID = int.Parse(frmViewTableForm.ReturnField);
					mProductID = int.Parse(drowView[ITM_ProductTable.PRODUCTID_FLD].ToString());
					VOProductToControls();
					// Load dstRouting
					RoutingBO boRout = new RoutingBO();
					dstRouting = boRout.ListRoutingByProduct(voProduct.ProductID);
					if (dstRouting.Tables[0].Rows.Count > 0)
					{
						
					}
					DataColumn[] objDataCols = new DataColumn[1];
					objDataCols[0] = dstRouting.Tables[ITM_RoutingTable.TABLE_NAME].Columns[ITM_RoutingTable.ROUTINGID_FLD];
					dstRouting.Tables[ITM_RoutingTable.TABLE_NAME].PrimaryKey = objDataCols;
					cboPacer.DataSource = dstRouting.Tables[ITM_RoutingTable.TABLE_NAME];
					cboPacer.DisplayMember = ITM_RoutingTable.PACER_FLD;
					cboPacer.ValueMember = ITM_RoutingTable.PACER_FLD;
					InitRouting();
					
				}
				//frmViewTableForm.Close();
				txtIncrement.Enabled = txtRoutDescription.Enabled = true;
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

			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.Default;

			#endregion Code Inserted Automatically

			
		}

		//**************************************************************************              
		///    <Description>
		///       This method uses to update data when user change col row
		///    </Description>
		///    <Inputs>
		///            
		///    </Inputs>
		///    <Outputs>
		///       
		///    </Outputs>
		///    <Returns>
		///       
		///    </Returns>
		///    <Authors>
		///       Hoang Trung Son - iSphere software
		///    </Authors>
		///    <History>
		///       Thursday, January 27, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private void gridRouting_RowColChange(object sender, C1.Win.C1TrueDBGrid.RowColChangeEventArgs e)
		{
			const string METHOD_NAME = THIS + ".gridRouting_RowColChange()";
			try
			{
				if(dstRouting.Tables.Count == 0) return;
				if(e.LastRow != gridRouting.Row)
				{
					if(dstRouting.Tables[0].Rows.Count > 0)
					{
						// update record
						object objKey;
						voRouting = new ITM_RoutingVO();
						objKey = gridRouting[gridRouting.Row,ITM_RoutingTable.ROUTINGID_FLD];
						voRouting.RoutingID = int.Parse(objKey.ToString());
						DataRow objRow = dstRouting.Tables[ITM_RoutingTable.TABLE_NAME].Rows.Find(objKey);
						// load data from DataRow to VO
						DataRowToVO(ref objRow);
						// store data on form to voRouting
						VOToControls(voRouting);
					}
				}
			}
			catch (Exception ex)
			{
				// displays the error message.
				PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxIcon.Error);
				// log message.
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

		//**************************************************************************              
		///    <Description>
		///       This method uses to check before change
		///    </Description>
		///    <Inputs>
		///            
		///    </Inputs>
		///    <Outputs>
		///       
		///    </Outputs>
		///    <Returns>
		///       
		///    </Returns>
		///    <Authors>
		///       Hoang Trung Son - iSphere software
		///    </Authors>
		///    <History>
		///       Thursday, January 27, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private void gridRouting_BeforeRowColChange(object sender, C1.Win.C1TrueDBGrid.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".gridRouting_BeforeRowColChange()";
			try
			{
				if(blnIsChangedRouting)
				{
					// update record to dataset
					object objKey;
					objKey = gridRouting[gridRouting.Row,ITM_RoutingTable.ROUTINGID_FLD];
					// store data on form to voRouting
					ControlsToVO();
					DataRow objRow = dstRouting.Tables[ITM_RoutingTable.TABLE_NAME].Rows.Find(objKey);
					VOToDataRow(ref objRow);
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

		//**************************************************************************              
		///    <Description>
		///       This method uses to set value for txtValue
		///    </Description>
		///    <Inputs>
		///            
		///    </Inputs>
		///    <Outputs>
		///       
		///    </Outputs>
		///    <Returns>
		///       
		///    </Returns>
		///    <Authors>
		///       Hoang Trung Son - iSphere software
		///    </Authors>
		///    <History>
		///       Thursday, January 27, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private void radOverlapQty_CheckedChanged(object sender, System.EventArgs e)
		{
			RadioButton radCheckedChanged = (RadioButton) sender;
			txtValue.Value = DBNull.Value;
			if(radCheckedChanged.Checked == true)
			{
				lblValue.Text = radCheckedChanged.Text;
				txtValue.CustomFormat = Constants.INT_DSP_NUM_MASK;
				txtValue.Value = null;
				txtValue.Enabled = true;
			}

			if(radOverlap.Checked == true)
			{
				txtValue.CustomFormat = Constants.EDIT_NUM_MASK;
			}
			else
			{
				txtValue.CustomFormat = Constants.INT_DSP_NUM_MASK;
			}
		}

		//**************************************************************************              
		///    <Description>
		///       This method uses to set value for txtValue2
		///    </Description>
		///    <Inputs>
		///            
		///    </Inputs>
		///    <Outputs>
		///       
		///    </Outputs>
		///    <Returns>
		///       
		///    </Returns>
		///    <Authors>
		///       Hoang Trung Son - iSphere software
		///    </Authors>
		///    <History>
		///       Thursday, January 27, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private void radOverlapQty2_CheckedChanged(object sender, System.EventArgs e)
		{
			RadioButton radCheckedChanged = (RadioButton) sender;
			txtValue2.Value = DBNull.Value;
			if(radCheckedChanged.Checked == true) lblValue2.Text = radCheckedChanged.Text;
			if(radCheckedChanged.Checked == true)
			{
				txtValue2.CustomFormat = Constants.INT_DSP_NUM_MASK;
				txtValue2.Value = null;	
				txtValue2.Enabled = true;
			}
			if(radOverlap2.Checked == true)
			{
				txtValue2.CustomFormat = Constants.EDIT_NUM_MASK;
			}
			else
			{
				txtValue2.CustomFormat = Constants.INT_DSP_NUM_MASK;
			}
			
		}

		//**************************************************************************              
		///    <Description>
		///       This method uses to insert vendor
		///    </Description>
		///    <Inputs>
		///            
		///    </Inputs>
		///    <Outputs>
		///       
		///    </Outputs>
		///    <Returns>
		///       
		///    </Returns>
		///    <Authors>
		///       Hoang Trung Son - iSphere software
		///    </Authors>
		///    <History>
		///       Thursday, January 27, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private void btnVendor_Click(object sender, System.EventArgs e)
		{
			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.WaitCursor;

			#endregion Code Inserted Automatically

			
			const string METHOD_NAME = THIS + ".btnVendor_Click()";
			try
			{
				DataRowView drwResult = null;
				drwResult = FormControlComponents.OpenSearchForm(MST_PartyTable.TABLE_NAME, MST_PartyTable.CODE_FLD, txtVendor.Text.Trim(), null, true);
				if (drwResult != null)
				{
					txtVendor.Value = int.Parse(drwResult[MST_PartyTable.PARTYID_FLD].ToString());
					txtVendor.Text = drwResult[MST_PartyTable.CODE_FLD].ToString();
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

			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.Default;

			#endregion Code Inserted Automatically

			
		}

		//**************************************************************************              
		///    <Description>
		///       This method uses to check mandatory
		///    </Description>
		///    <Inputs>
		///            
		///    </Inputs>
		///    <Outputs>
		///       
		///    </Outputs>
		///    <Returns>
		///       
		///    </Returns>
		///    <Authors>
		///       Hoang Trung Son - iSphere software
		///    </Authors>
		///    <History>
		///       Thursday, January 27, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private bool CheckMandatory(Control pobjControl)
		{
			if (pobjControl.Text.Trim() == string.Empty)
			{
				return true;
			}
			return false;
		}

		//**************************************************************************              
		///    <Description>
		///       This method uses to process when form close
		///    </Description>
		///    <Inputs>
		///            
		///    </Inputs>
		///    <Outputs>
		///       
		///    </Outputs>
		///    <Returns>
		///       
		///    </Returns>
		///    <Authors>
		///       Hoang Trung Son - iSphere software
		///    </Authors>
		///    <History>
		///       Friday, January 28, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private void Routing_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnClose_Click()";
			try
			{
				// if the form has been change then ask to store database
				if(blnIsChanged ) 
				{
					DialogResult enumDialog = PCSMessageBox.Show(ErrorCode.MESSAGE_QUESTION_STORE_INTO_DATABASE,MessageBoxButtons.YesNoCancel,MessageBoxIcon.Question);
					if( enumDialog == DialogResult.Yes)
					{
						// store database
//						RoutingBO boRouting = new RoutingBO();
//						boRouting.UpdateDataSet(dstRouting);
						//Close();
						//Save before exit
						btnSave_Click(btnSave, new EventArgs());
						//e.Cancel = false;
					} 
					else if( enumDialog == DialogResult.No) // click No button
					{
						//Close();
						e.Cancel = false;
					}
					else if( enumDialog == DialogResult.Cancel) // click Cancel button
					{
						e.Cancel = true;
					}
				}
				else // if has no change
				{
					e.Cancel = false;
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

		#endregion Other events

		#region Other Functions
		//**************************************************************************              
		///    <Description>
		///       This method uses to process item 4.a
		///       4.a The system automatic fill data into Step field with formula: 
		///       step = last step + increment
		///    </Description>
		///    <Inputs>
		///            
		///    </Inputs>
		///    <Outputs>
		///       
		///    </Outputs>
		///    <Returns>
		///       
		///    </Returns>
		///    <Authors>
		///       Hoang Trung Son - iSphere software
		///    </Authors>
		///    <History>
		///       Wednesday, February 02, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private int GetStepValue()
		{
			try
			{
				int intMaxStep = 0;
				if(gridRouting.RowCount > 0)
					intMaxStep = int.Parse(gridRouting[gridRouting.RowCount - 1,ITM_RoutingTable.STEP_FLD].ToString());
				return int.Parse(txtIncrement.Value.ToString()) + intMaxStep;
			}
			catch
			{
				return int.Parse(txtIncrement.Value.ToString());
			}
		}

		//**************************************************************************              
		///    <Description>
		///       This method uses to 5.a
		///       5.a If the user change the Step value then the system display error message 
		///       and restore value if the current Step is smaller then previous step
		///    </Description>
		///    <Inputs>
		///            
		///    </Inputs>
		///    <Outputs>
		///       
		///    </Outputs>
		///    <Returns>
		///       
		///    </Returns>
		///    <Authors>
		///       Hoang Trung Son - iSphere software
		///    </Authors>
		///    <History>
		///       Wednesday, February 02, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private bool IsIncorrectUpdateStep()
		{
			try
			{
				int intMaxStep = 0;
				int intMinStep = 0;
				// if grid has more than 1 rows then check prev and next row
				if(gridRouting.RowCount >= 1)
				{
					// if current row is first row then check for next
					if(gridRouting.Row == 0)
					{
						intMaxStep = int.Parse(gridRouting[gridRouting.Row + 1,ITM_RoutingTable.STEP_FLD].ToString());
						if(int.Parse(txtStep.Value.ToString()) >= intMaxStep) return true;
					}
					// if current row is last row then check for prev
					else if(gridRouting.Row == gridRouting.RowCount)
					{
						intMinStep = int.Parse(gridRouting[gridRouting.Row - 1,ITM_RoutingTable.STEP_FLD].ToString());
						if(int.Parse(txtStep.Value.ToString()) <= intMinStep) return true;
					}
					// if current row has prev row and next row then check for two
					else
					{
						intMaxStep = int.Parse(gridRouting[gridRouting.Row + 1,ITM_RoutingTable.STEP_FLD].ToString());
						intMinStep = int.Parse(gridRouting[gridRouting.Row - 1,ITM_RoutingTable.STEP_FLD].ToString());
						if((int.Parse(txtStep.Value.ToString()) >= intMaxStep) || 
							(int.Parse(txtStep.Value.ToString()) <= intMinStep)) return true;
					}
					return false;
				}
				else
				{
					return false;
				}
			}
			catch
			{
				return true;
			}
		}

		//**************************************************************************              
		///    <Description>
		///       This method uses to 5.a
		///       5.a If the user change the Step value then the system display error message 
		///       and restore value if the current Step is smaller then previous step
		///    </Description>
		///    <Inputs>
		///            
		///    </Inputs>
		///    <Outputs>
		///       
		///    </Outputs>
		///    <Returns>
		///       
		///    </Returns>
		///    <Authors>
		///       Hoang Trung Son - iSphere software
		///    </Authors>
		///    <History>
		///       Wednesday, February 02, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************

		private bool IsIncorrectAddNewStep()
		{
			try
			{
				int intMinStep = 0;
				// if grid has more than or equal 1 rows then check prev and next row
				if(gridRouting.RowCount > 0)
				{
					// if current row is last row then check for prev
					if(gridRouting.Row == gridRouting.RowCount - 1)
					{
						intMinStep = int.Parse(gridRouting[gridRouting.Row,ITM_RoutingTable.STEP_FLD].ToString());
						if(int.Parse(txtStep.Value.ToString()) <= intMinStep) return true;
					}
				}
				return false;
			}
			catch
			{
				return true;
			}
		}

		//**************************************************************************              
		///    <Description>
		///       This method uses to set editable controls
		///    </Description>
		///    <Inputs>
		///            
		///    </Inputs>
		///    <Outputs>
		///       
		///    </Outputs>
		///    <Returns>
		///       
		///    </Returns>
		///    <Authors>
		///       Hoang Trung Son - iSphere software
		///    </Authors>
		///    <History>
		///       Thursday, January 27, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private void SetEditableControl(object[] pobjControls, bool pblnEnable)
		{
			foreach (object objControl in pobjControls)
			{
				if(objControl == null) continue;
				Control objContr = (Control) objControl;
				objContr.Enabled = pblnEnable;
			}
		}


		//**************************************************************************              
		///    <Description>
		///       This method uses to check existed vendor
		///    </Description>
		///    <Inputs>
		///            
		///    </Inputs>
		///    <Outputs>
		///       
		///    </Outputs>
		///    <Returns>
		///       
		///    </Returns>
		///    <Authors>
		///       Hoang Trung Son - iSphere software
		///    </Authors>
		///    <History>
		///       Thursday, February 03, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private bool IsNotExistVendor()
		{
			try
			{
				string strVendor = txtVendor.Text.Trim();
				if(strVendor.Length > 0)
				{
					DataSet dstParty = new DataSet();
					RoutingBO boRouting = new RoutingBO();
					dstParty = boRouting.ListParty();
					int intCount = 0;
					if(dstParty.Tables[0].Rows.Count > 0)
					{
						intCount = dstParty.Tables[0].Select(MST_PartyTable.CODE_FLD + "='" + strVendor + "'").Length;
					}
					if(intCount == 0) return true;
				}
				return false;
			}
			catch
			{
				return true;
			}
		}

		//**************************************************************************              
		///    <Description>
		///       This method uses to fill combo in grid
		///    </Description>
		///    <Inputs>
		///            
		///    </Inputs>
		///    <Outputs>
		///       
		///    </Outputs>
		///    <Returns>
		///       
		///    </Returns>
		///    <Authors>
		///       Hoang Trung Son - iSphere software
		///    </Authors>
		///    <History>
		///       Thursday, February 03, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private void FillDisplayFieldInGrid(string pstrFieldName, string pstrFieldDisplay,DataTable pobjDataTable)
		{
			const string METHOD_NAME = THIS + ".FillDisplayFieldInGrid()";
			try
			{
				gridRouting.Splits[0].DisplayColumns[pstrFieldName].DropDownList = false;
				gridRouting.Splits[0].DisplayColumns[pstrFieldName].AutoDropDown = false;
				gridRouting.Splits[0].DisplayColumns[pstrFieldName].Button = false;
				//gridRouting.Columns[pstrFieldName].ValueItems.Presentation = PresentationEnum.ComboBox;
				gridRouting.Columns[pstrFieldName].ValueItems.Translate = true;
				gridRouting.Columns[pstrFieldName].ValueItems.Validate = true;

				foreach (DataRow objRow in pobjDataTable.Rows)
				{
					ValueItem objItem = new ValueItem(objRow[pstrFieldName].ToString(), objRow[pstrFieldDisplay].ToString());
					gridRouting.Columns[pstrFieldName].ValueItems.Values.Add(objItem);
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

		//**************************************************************************              
		///    <Description>
		///       This method uses to fill column type
		///    </Description>
		///    <Inputs>
		///            
		///    </Inputs>
		///    <Outputs>
		///       
		///    </Outputs>
		///    <Returns>
		///       
		///    </Returns>
		///    <Authors>
		///       Hoang Trung Son - iSphere software
		///    </Authors>
		///    <History>
		///       Thursday, February 03, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private void FillTypeColumnInGrid()
		{
			const string METHOD_NAME = THIS + ".METHOD_NAME()";
			try
			{
				gridRouting.Splits[0].DisplayColumns[ITM_RoutingTable.TYPE_FLD].DropDownList = false;
				gridRouting.Splits[0].DisplayColumns[ITM_RoutingTable.TYPE_FLD].AutoDropDown = false;
				gridRouting.Splits[0].DisplayColumns[ITM_RoutingTable.TYPE_FLD].Button = false;
				//gridRouting.Columns[pstrFieldName].ValueItems.Presentation = PresentationEnum.ComboBox;
				gridRouting.Columns[ITM_RoutingTable.TYPE_FLD].ValueItems.Translate = true;
				gridRouting.Columns[ITM_RoutingTable.TYPE_FLD].ValueItems.Validate = true;

				ValueItem objItem = new ValueItem(((int)OperationType.Inside).ToString(), INSIDE_PROCESS);
				gridRouting.Columns[ITM_RoutingTable.TYPE_FLD].ValueItems.Values.Add(objItem);
				objItem = new ValueItem(((int)OperationType.Outside).ToString(), OUTSIDE_PROCESS);
				gridRouting.Columns[ITM_RoutingTable.TYPE_FLD].ValueItems.Values.Add(objItem);
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

		//**************************************************************************              
		///    <Description>
		///       This method uses to set editable controls
		///    </Description>
		///    <Inputs>
		///            
		///    </Inputs>
		///    <Outputs>
		///       
		///    </Outputs>
		///    <Returns>
		///       
		///    </Returns>
		///    <Authors>
		///       Hoang Trung Son - iSphere software
		///    </Authors>
		///    <History>
		///       Thursday, February 03, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private void SetControls()
		{
			const int MAX = 100;
			int i = 0;
			objControls = new object[MAX];
			objControls[i++] = txtStep;
			//objControls[i++] = cboWorkCenter;
			objControls[i++] = cboType;
//			objControls[i++] = cboStatus;
//			objControls[i++] = cboFunction;
			objControls[i++] = cboPacer;
			objControls[i++] = txtFixLeadTime2;
			objControls[i++] = radLinenear;
			objControls[i++] = txtVarLeadTime2;
			objControls[i++] = txtMoveTime;
			objControls[i++] = txtMoveTime2;
			objControls[i++] = txtMoveTime;
			objControls[i++] = btnVendor;
			objControls[i++] = txtCost;
			objControls[i++] = txtValue;
			objControls[i++] = txtValue2;
			objControls[i++] = txtMachSU;
			objControls[i++] = txtMachRun;
			objControls[i++] = txtMachines;
			objControls[i++] = cboMachCC;
			objControls[i++] = txtSetupQty;
			objControls[i++] = txtLabSetup;
			objControls[i++] = txtLabRun;
			objControls[i++] = txtCrewSize;
			objControls[i++] = cboLabCC;
			objControls[i++] = txtRunQty;
			objControls[i++] = txtTimeStudy;
			objControls[i++] = cboBeginDate;
			objControls[i++] = cboEndDate;
			objControls[i++] = radOverlapQty;
			objControls[i++] = radOverlapQty2;
			objControls[i++] = radSchedSeq;
			objControls[i++] = radOverlap;
			objControls[i++] = radSchedSeq2;
			objControls[i++] = radOverlap2;
			objControls[i++] = txtVendor;			
			objControls[i++] = txtFunction;			
			objControls[i++] = txtWorkCenter;			
			objControls[i++] = txtStatus;			
			objControls[i++] = btnFunction;			
			objControls[i++] = btnWorkCenter;
//			objControls[i++] = btnProductionLine;
//			objControls[i++] = txtProductionLine;
			objControls[i++] = btnStatus;
		}

		//**************************************************************************              
		///    <Description>
		///       This method uses to init gird routing
		///    </Description>
		///    <Inputs>
		///            
		///    </Inputs>
		///    <Outputs>
		///       
		///    </Outputs>
		///    <Returns>
		///       
		///    </Returns>
		///    <Authors>
		///       Hoang Trung Son - iSphere software
		///    </Authors>
		///    <History>
		///       Thursday, February 03, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private void InitRouting()
		{
			const string METHOD_NAME = THIS + ".InitRouting()";
			try
			{
				// set size of pnlTabOutside
				pnlOutside.Top = pnlOutside.Left = 0;
				pnlOutside.Height = tabMachine.Height;
				pnlOutside.Width = tabMachine.Width;
				// set size of pnlMachine
				pnlMachine.Top = pnlMachine.Left = 0;
				pnlMachine.Height = tabMachine.Height;
				pnlMachine.Width = tabMachine.Width;

				// store columns header
				string strHeaderStep = gridRouting.Columns[ITM_RoutingTable.STEP_FLD].Caption;
				int intWidthStep = gridRouting.Splits[0].DisplayColumns[ITM_RoutingTable.STEP_FLD].Width;
				string strHeaderType = gridRouting.Columns[ITM_RoutingTable.TYPE_FLD].Caption;
				int intWidthType = gridRouting.Splits[0].DisplayColumns[ITM_RoutingTable.TYPE_FLD].Width;
				string strHeaderFunction = gridRouting.Columns[ITM_RoutingTable.FUNCTIONID_FLD].Caption;
				int intWidthFunction = gridRouting.Splits[0].DisplayColumns[ITM_RoutingTable.FUNCTIONID_FLD].Width;
				string strHeaderWorkCenter = gridRouting.Columns[ITM_RoutingTable.WORKCENTERID_FLD].Caption;
				int intWidthWorkCenter = gridRouting.Splits[0].DisplayColumns[ITM_RoutingTable.WORKCENTERID_FLD].Width;
				string strHeaderStatus = gridRouting.Columns[ITM_RoutingTable.ROUTINGSTATUSID_FLD].Caption;
				int intWidthStatus = gridRouting.Splits[0].DisplayColumns[ITM_RoutingTable.ROUTINGSTATUSID_FLD].Width;

				RoutingBO boRouting = new RoutingBO();
				dstRouting = boRouting.ListRoutingByProduct(mProductID);
				DataColumn[] objColumns = new DataColumn[1];
				objColumns[0] = dstRouting.Tables[ITM_RoutingTable.TABLE_NAME].Columns[ITM_RoutingTable.ROUTINGID_FLD];

				// fill data into grid
				dstRouting.Tables[ITM_RoutingTable.TABLE_NAME].PrimaryKey = objColumns; 
				gridRouting.DataSource = dstRouting.Tables[ITM_RoutingTable.TABLE_NAME];


			
				// show columns
				foreach(C1DisplayColumn objCol in gridRouting.Splits[0].DisplayColumns)
				{
					objCol.Visible = false;
					objCol.HeadingStyle.HorizontalAlignment = AlignHorzEnum.Center;
				}
				// Step col
				gridRouting.Splits[0].DisplayColumns[ITM_RoutingTable.STEP_FLD].Visible = true;
				gridRouting.Columns[ITM_RoutingTable.STEP_FLD].Caption = strHeaderStep;
				gridRouting.Splits[0].DisplayColumns[ITM_RoutingTable.STEP_FLD].Width = intWidthStep;
				// Type col
				gridRouting.Splits[0].DisplayColumns[ITM_RoutingTable.TYPE_FLD].Visible = true;
				gridRouting.Columns[ITM_RoutingTable.TYPE_FLD].Caption = strHeaderType;
				gridRouting.Splits[0].DisplayColumns[ITM_RoutingTable.TYPE_FLD].Width = intWidthType;
				// Function col
				gridRouting.Splits[0].DisplayColumns[ITM_RoutingTable.FUNCTIONID_FLD].Visible = true;
				gridRouting.Columns[ITM_RoutingTable.FUNCTIONID_FLD].Caption = strHeaderFunction;
				gridRouting.Splits[0].DisplayColumns[ITM_RoutingTable.FUNCTIONID_FLD].Width = intWidthFunction;
				// Work center col
				gridRouting.Splits[0].DisplayColumns[ITM_RoutingTable.WORKCENTERID_FLD].Visible = true;
				gridRouting.Columns[ITM_RoutingTable.WORKCENTERID_FLD].Caption = strHeaderWorkCenter;
				gridRouting.Splits[0].DisplayColumns[ITM_RoutingTable.WORKCENTERID_FLD].Width = intWidthWorkCenter;
				// Status col
				gridRouting.Splits[0].DisplayColumns[ITM_RoutingTable.ROUTINGSTATUSID_FLD].Visible = true;			
				gridRouting.Columns[ITM_RoutingTable.ROUTINGSTATUSID_FLD].Caption = strHeaderStatus;
				gridRouting.Splits[0].DisplayColumns[ITM_RoutingTable.ROUTINGSTATUSID_FLD].Width = intWidthStatus;
				// IsMain col
				gridRouting.Splits[0].DisplayColumns[MST_WorkCenterTable.ISMAIN_FLD].Visible = true;			
				gridRouting.Splits[0].DisplayColumns[MST_WorkCenterTable.ISMAIN_FLD].Width = intWidthStatus;
				FillTypeColumnInGrid();
				DataSet dstFunction = new DataSet();
				dstFunction = boRouting.ListFunction();
				FillDisplayFieldInGrid(MST_FunctionTable.FUNCTIONID_FLD,MST_FunctionTable.CODE_FLD,dstFunction.Tables[MST_FunctionTable.TABLE_NAME]);
				DataSet dstWorkCenter = new DataSet();
				dstWorkCenter = boRouting.ListWorkCenter();
				FillDisplayFieldInGrid(MST_WorkCenterTable.WORKCENTERID_FLD,MST_WorkCenterTable.CODE_FLD,dstWorkCenter.Tables[MST_WorkCenterTable.TABLE_NAME]);

				DataSet dstStatus = new DataSet();
				dstStatus = boRouting.ListRoutingStatus();
				FillDisplayFieldInGrid(ITM_RoutingStatusTable.ROUTINGSTATUSID_FLD,ITM_RoutingStatusTable.CODE_FLD,dstStatus.Tables[ITM_RoutingStatusTable.TABLE_NAME]);
				//gridRouting.Update();
				if(dstRouting.Tables[ITM_RoutingTable.TABLE_NAME].Rows.Count > 0)
				{
					DataRow objRow = dstRouting.Tables[ITM_RoutingTable.TABLE_NAME].Rows[0];
					DataRowToVO(ref objRow);
					VOToControls(voRouting);
					//Fill Production Line
					txtProductionLine.Text = dstRouting.Tables[ITM_RoutingTable.TABLE_NAME].Rows[0][PRODUCTION_LINE].ToString();
					txtProductionLine.Tag = dstRouting.Tables[ITM_RoutingTable.TABLE_NAME].Rows[0][PRO_ProductionLineTable.PRODUCTIONLINEID_FLD];
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

		//**************************************************************************              
		///    <Description>
		///       Enable and Disable button according to each Form Status
		///    </Description>
		///    <Inputs>
		///       
		///    </Inputs>
		///    <Outputs>
		///       Button will be set enable = true|false
		///    </Outputs>
		///    <Returns>
		///       
		///    </Returns>
		///    <Authors>
		///       Hoang Trung Son - iSphere software
		///    </Authors>
		///    <History>
		///       15-Dec-2004
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private void SetEnableButtons() 
		{
			switch (mFormAction)
			{
				case EnumAction.Default:
					//Disable Buttons
					btnSave.Enabled = false;
//					ExtendedProButton extendPro ;
					//Enable Buttons
//					extendPro = (ExtendedProButton) btnAdd.Tag;
//					if(!extendPro.IsDisable)
//					{
						btnAdd.Enabled = true;
//					}
//					else
//					{
//						btnAdd.Enabled = false;
//					}
					if(voProduct.ProductID >0) 
					{
						//Edit 
//						extendPro = (ExtendedProButton) btnEdit.Tag;
//						if (!extendPro.IsDisable) 
//						{
							btnEdit.Enabled = true;
//						}
//						else
//						{
//							btnEdit.Enabled = false;
//						}
						//Delete
//						extendPro = (ExtendedProButton) btnDelete.Tag;
//						if (!extendPro.IsDisable)
//						{
							btnDelete.Enabled = true;
//						}
//						else
//						{
//							btnDelete.Enabled = false;
//						}

//						extendPro = (ExtendedProButton) btnEdit.Tag;
					}
					else 
					{
						btnEdit.Enabled = false;
						btnDelete.Enabled = false;
					}
					break;

				default:
					//Disable Buttons
					btnAdd.Enabled = false;
					btnEdit.Enabled = false;
					btnDelete.Enabled = false;
					//Enable Buttons
					btnSave.Enabled = true;
					break;				
			}
		}


		#endregion Other Functions

		private void radLinenear_CheckedChanged(object sender, System.EventArgs e)
		{
			RadioButton radLinenear = (RadioButton) sender;
			txtValue.Value = DBNull.Value;
			if(radLinenear.Checked == true)
			{
				lblValue.Text = string.Empty;
				txtValue.Enabled = false;
			}
		}

		private void txtMachines_TextChanged(object sender, System.EventArgs e)
		{
		
		}

		private void pnlMachine_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
		
		}
		/// <summary>
		/// OnEnterControl
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Wednesday</date>
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
		/// <summary>
		/// txtVendor_KeyDown
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Friday, October 7 2005</date>
		private void txtVendor_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtVendor_KeyDown()";
			try
			{
				if (e.KeyCode == Keys.F4)
				{
					btnVendor_Click(sender, e);
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
		/// txtVendor_Leave
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Friday, October 7 2005</date>
		private void txtVendor_Leave(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtVendor_Leave()";
			try 
			{
				OnLeaveControl(sender, e);
				if ((txtVendor.Text == string.Empty)||(!txtVendor.Modified))
				{
					return;
				}
				DataRowView drwResult = null;
				drwResult = FormControlComponents.OpenSearchForm(MST_PartyTable.TABLE_NAME, MST_PartyTable.CODE_FLD, txtVendor.Text.Trim(), null, false);
				if (drwResult != null)
				{
					txtVendor.Value = int.Parse(drwResult[MST_PartyTable.PARTYID_FLD].ToString());
					txtVendor.Text = drwResult[MST_PartyTable.CODE_FLD].ToString();
				}
				else
				{
					txtVendor.Focus();
					txtVendor.Select();
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
		/// Routing_KeyDown
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Monday, October 10 2005</date>
		private void Routing_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
		}

		private void radLinenear2_CheckedChanged(object sender, System.EventArgs e)
		{
			RadioButton radLinenear = (RadioButton) sender;
			txtValue2.Value = DBNull.Value;
			if(radLinenear.Checked == true)
			{
				lblValue2.Text = string.Empty;
				txtValue2.Enabled = false;
			}
		}

		private void pnlOutside_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
		}

		private void gridRouting_AfterDelete(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".gridRouting_AfterDelete()";
			try
			{
				if(gridRouting.RowCount == 0) return;
//				if(e.LastRow != gridRouting.Row)
//				{
					if(gridRouting.RowCount > 0)
					{
						// update record
						object objKey;
						voRouting = new ITM_RoutingVO();
						objKey = gridRouting[gridRouting.Row,ITM_RoutingTable.ROUTINGID_FLD];
						voRouting.RoutingID = int.Parse(objKey.ToString());
						DataRow objRow = dstRouting.Tables[ITM_RoutingTable.TABLE_NAME].Rows.Find(objKey);
						// load data from DataRow to VO
						DataRowToVO(ref objRow);
						// store data on form to voRouting
						VOToControls(voRouting);
					}
//				}
			}
			catch (Exception ex)
			{
				// displays the error message.
				PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxIcon.Error);
				// log message.
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
		/// txtValue_Leave
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Tuesday, October 11 2005</date>
		private void txtValue_Leave(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtValue_Leave()";
			try
			{
				OnLeaveControl(sender, e);
				if ((txtValue.Value == null)||(txtValue.Value == DBNull.Value))
					return;
				if (radOverlap.Checked == true)
				{
					txtValue.CustomFormat = Constants.EDIT_NUM_MASK;
					if ((decimal.Parse(txtValue.Value.ToString()) > 100) || (decimal.Parse(txtValue.Value.ToString()) < 0))
					{
						PCSMessageBox.Show(ErrorCode.MESSAGE_INVALID_PERCENT, MessageBoxIcon.Warning);
						txtValue.Focus();
					}
				}
				if (radOverlapQty.Checked == true)
				{
					if (decimal.Parse(txtValue.Value.ToString()) < 0)
					{
						PCSMessageBox.Show(ErrorCode.POSITIVE_NUMBER, MessageBoxIcon.Warning);
						txtValue.Focus();
					}
				}
				if (radSchedSeq.Checked == true)
				{
					txtValue.CustomFormat = Constants.INT_DSP_NUM_MASK;
				}
			}
			catch (Exception ex)
			{
				// displays the error message.
				PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxIcon.Error);
				// log message.
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
		/// cboControl_Keydown
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Tuesday, October 11 2005</date>
		private void cboControl_Keydown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			const string METHOD_NAME = THIS + ".cboControl_Keydown()";
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
		/// txtValue2_Leave
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Tuesday, October 11 2005</date>
		private void txtValue2_Leave(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtValue2_Leave()";
			try
			{
				OnLeaveControl(sender, e);
				if ((txtValue2.Value == null) ||(txtValue2.Value == DBNull.Value))	
					return;
				if (radOverlap2.Checked == true)
				{
					txtValue2.CustomFormat = Constants.EDIT_NUM_MASK;
					if ((decimal.Parse(txtValue2.Value.ToString()) > 100) || (decimal.Parse(txtValue2.Value.ToString()) < 0))
					{
						PCSMessageBox.Show(ErrorCode.MESSAGE_INVALID_PERCENT, MessageBoxIcon.Warning);
						txtValue2.Focus();
					}
				}
				if (radOverlapQty2.Checked == true)
				{
					if (decimal.Parse(txtValue2.Value.ToString()) < 0)
					{
						PCSMessageBox.Show(ErrorCode.POSITIVE_NUMBER, MessageBoxIcon.Warning);
						txtValue2.Focus();
					}
				}
				if (radSchedSeq2.Checked == true)
				{
					txtValue2.CustomFormat = Constants.INT_DSP_NUM_MASK;
				}
			}
			catch (Exception ex)
			{
				// displays the error message.
				PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxIcon.Error);
				// log message.
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
		/// btnFunction_Click
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Tuesday, January 3 2006</date>
		private void btnFunction_Click(object sender, System.EventArgs e)
		{
			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.WaitCursor;

			#endregion Code Inserted Automatically

			
			const string METHOD_NAME = THIS + ".btnFunction_Click()";
			try
			{
				Hashtable hstCondition = new Hashtable();
				DataRowView drwResult = null;
				if (txtWorkCenter.Text.Trim() != string.Empty)
				{
					hstCondition.Add(MST_WorkCenterTable.WORKCENTERID_FLD, int.Parse(txtWorkCenter.Tag.ToString()));
				}
				else
				{
					PCSMessageBox.Show(ErrorCode.MESSAGE_WOROUTING_INPUT_ROUTINGWORKCENTER, MessageBoxIcon.Warning);
					txtWorkCenter.Focus();
					// Code Inserted Automatically
#region Code Inserted Automatically
this.Cursor = Cursors.Default;
#endregion Code Inserted Automatically

					return;
				}
				drwResult = FormControlComponents.OpenSearchForm(MST_FunctionTable.TABLE_NAME, MST_FunctionTable.CODE_FLD, txtFunction.Text.Trim(), hstCondition, true);
				if (drwResult != null)
				{
					txtFunction.Text = drwResult[MST_FunctionTable.CODE_FLD].ToString();
					txtFunctionName.Text = drwResult[MST_FunctionTable.DESCRIPTION_FLD].ToString();
					txtFunction.Tag = int.Parse(drwResult[MST_FunctionTable.FUNCTIONID_FLD].ToString());
				}
			}
			catch (Exception ex)
			{
				// displays the error message.
				PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxIcon.Error);
				// log message.
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
		/// <summary>
		/// txtFunction_Validating
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Tuesday, January 3 2006</date>
		private void txtFunction_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtFunction_Validating()";
			try
			{
				Hashtable hstCondition = new Hashtable();
				if (txtFunction.Text == string.Empty)
				{
					txtFunction.Tag = null;
					return;
				}
				if (!txtFunction.Modified)
					return;
				if (txtWorkCenter.Text.Trim() != string.Empty)
				{
					hstCondition.Add(MST_WorkCenterTable.WORKCENTERID_FLD, int.Parse(txtWorkCenter.Tag.ToString()));
				}
				else
				{
					PCSMessageBox.Show(ErrorCode.MESSAGE_WOROUTING_INPUT_ROUTINGWORKCENTER, MessageBoxIcon.Warning);
					e.Cancel = true;
					return;
				}
				DataRowView drwResult = null;
				drwResult = FormControlComponents.OpenSearchForm(MST_FunctionTable.TABLE_NAME, MST_FunctionTable.CODE_FLD, txtFunction.Text.Trim(), hstCondition, false);
				if (drwResult != null)
				{
					txtFunction.Text = drwResult[MST_FunctionTable.CODE_FLD].ToString();
					txtFunctionName.Text = drwResult[MST_FunctionTable.DESCRIPTION_FLD].ToString();
					txtFunction.Tag = drwResult[MST_FunctionTable.FUNCTIONID_FLD];
				}
				else 
					e.Cancel = true;
			}
			catch (Exception ex)
			{
				// displays the error message.
				PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxIcon.Error);
				// log message.
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
		/// txtFunction_KeyDown
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Tuesday, January 3 2006</date>
		private void txtFunction_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if (e.KeyCode == Keys.F4)
				btnFunction_Click(sender, e);
		}
		/// <summary>
		/// btnWorkCenter_Click
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Tuesday, January 3 2006</date>
		private void btnWorkCenter_Click(object sender, System.EventArgs e)
		{
			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.WaitCursor;

			#endregion Code Inserted Automatically

			
			const string METHOD_NAME = THIS + ".btnWorkCenter_Click()";
			try
			{
				Hashtable htbCondition = new Hashtable();
				DataRowView drwResult = null;
				if (txtProductionLine.Text.Trim() != string.Empty)
				{
					htbCondition.Add(MST_WorkCenterTable.PRODUCTIONLINEID_FLD, int.Parse(txtProductionLine.Tag.ToString()));
				}
				else
				{
					string[] strParam = new string[2];
					strParam[0] = lblProductionLine.Text;
					strParam[1] = lblWorkCenter.Text;
					PCSMessageBox.Show(ErrorCode.MESSAGE_SELECT_ONE_BEFORE_SELECT_ONE, MessageBoxIcon.Warning, strParam);
					txtProductionLine.Focus();
					// Code Inserted Automatically
#region Code Inserted Automatically
this.Cursor = Cursors.Default;
#endregion Code Inserted Automatically

					return;
				}
				drwResult = FormControlComponents.OpenSearchForm(MST_WorkCenterTable.TABLE_NAME, MST_WorkCenterTable.CODE_FLD, txtWorkCenter.Text.Trim(), htbCondition, true);
				if (drwResult != null)
				{
					txtWorkCenter.Text = drwResult[MST_WorkCenterTable.CODE_FLD].ToString();
					txtWorkCenter.Tag = drwResult[MST_WorkCenterTable.WORKCENTERID_FLD];
					txtFunction.Tag = null;
					txtFunction.Text = string.Empty;
					txtFunctionName.Text = string.Empty;
				}
			}
			catch (Exception ex)
			{
				// displays the error message.
				PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxIcon.Error);
				// log message.
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
		/// <summary>
		/// txtWorkCenter_Validating
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Tuesday, January 3 2006</date>
		private void txtWorkCenter_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtWorkCenter_Validating()";
			try
			{
				if (txtWorkCenter.Text == string.Empty)
				{
					txtFunction.Tag = null;
					txtFunction.Text = string.Empty;
					txtFunctionName.Text = string.Empty;
					txtWorkCenter.Tag = null;
					return;
				}
				if (!txtWorkCenter.Modified)
					return;
				Hashtable htbCondition = new Hashtable();
				DataRowView drwResult = null;
				if (txtProductionLine.Text.Trim() != string.Empty)
				{
					htbCondition.Add(MST_WorkCenterTable.PRODUCTIONLINEID_FLD, int.Parse(txtProductionLine.Tag.ToString()));
				}
				else
				{
					string[] strParam = new string[2];
					strParam[0] = lblProductionLine.Text;
					strParam[1] = lblWorkCenter.Text;
					PCSMessageBox.Show(ErrorCode.MESSAGE_SELECT_ONE_BEFORE_SELECT_ONE, MessageBoxIcon.Warning, strParam);
					e.Cancel = true;
					return;
				}
				drwResult = FormControlComponents.OpenSearchForm(MST_WorkCenterTable.TABLE_NAME, MST_WorkCenterTable.CODE_FLD, txtWorkCenter.Text.Trim(), null, false);
				if (drwResult != null)
				{
					txtWorkCenter.Text = drwResult[MST_FunctionTable.CODE_FLD].ToString();
					txtWorkCenter.Tag = drwResult[MST_WorkCenterTable.WORKCENTERID_FLD];
					txtFunction.Tag = null;
					txtFunction.Text = string.Empty;
					txtFunctionName.Text = string.Empty;
				}
				else 
					e.Cancel = true;
			}
			catch (Exception ex)
			{
				// displays the error message.
				PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxIcon.Error);
				// log message.
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
		/// txtWorkCenter_KeyDown
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Tuesday, January 3 2006</date>
		private void txtWorkCenter_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if (e.KeyCode == Keys.F4)
				btnWorkCenter_Click(sender, e);
		}
		/// <summary>
		/// btnStatus_Click
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnStatus_Click(object sender, System.EventArgs e)
		{
			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.WaitCursor;

			#endregion Code Inserted Automatically

			
			const string METHOD_NAME = THIS + ".btnStatus_Click()";
			try
			{
				DataRowView drwResult = null;
				drwResult = FormControlComponents.OpenSearchForm(ITM_RoutingStatusTable.TABLE_NAME, ITM_RoutingStatusTable.CODE_FLD, txtStatus.Text.Trim(), null, true);
				if (drwResult != null)
				{
					txtStatus.Text = drwResult[ITM_RoutingStatusTable.CODE_FLD].ToString();
					txtStatus.Tag = drwResult[ITM_RoutingStatusTable.ROUTINGSTATUSID_FLD];
				}
			}
			catch (Exception ex)
			{
				// displays the error message.
				PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxIcon.Error);
				// log message.
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
		/// <summary>
		/// txtStatus_Validating
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Tuesday, January 3 2006</date>
		private void txtStatus_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtStatus_Validating()";
			try
			{
				if (txtStatus.Text == string.Empty)
				{
					txtStatus.Tag = null;
					return;
				}
				if (!txtStatus.Modified)
					return;
				DataRowView drwResult = null;
				drwResult = FormControlComponents.OpenSearchForm(ITM_RoutingStatusTable.TABLE_NAME, ITM_RoutingStatusTable.CODE_FLD, txtStatus.Text.Trim(), null, false);
				if (drwResult != null)
				{
					txtStatus.Text = drwResult[ITM_RoutingStatusTable.CODE_FLD].ToString();
					txtStatus.Tag = drwResult[ITM_RoutingStatusTable.ROUTINGSTATUSID_FLD];
				}
				else 
					e.Cancel = true;
			}
			catch (Exception ex)
			{
				// displays the error message.
				PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxIcon.Error);
				// log message.
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

		private void txtStatus_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if (e.KeyCode == Keys.F4)
				btnStatus_Click(sender, e);
		}
		/// <summary>
		/// btnProductionLine_Click
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Thursday, January 12 2006</date>
		private void btnProductionLine_Click(object sender, System.EventArgs e)
		{
			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.WaitCursor;

			#endregion Code Inserted Automatically

			
			const string METHOD_NAME = THIS + ".btnProductionLine_Click()";
			try
			{
				// store current value in order to restore when user do not want to make changes
				int intProductionLine = 0;
				try
				{
					intProductionLine = Convert.ToInt32(txtProductionLine.Tag);
				}
				catch{}
				RoutingBO boRouting = new RoutingBO();
				//Call OpenSearchForm for selecting Production Line
				DataRowView drwResult = FormControlComponents.OpenSearchForm(PRO_ProductionLineTable.TABLE_NAME, PRO_ProductionLineTable.CODE_FLD, txtProductionLine.Text.Trim(), null, true);
				
				//If has Production Line matched searching condition, fill values to form's controls
				if(drwResult != null)
				{
					if (intProductionLine != int.Parse(drwResult[PRO_ProductionLineTable.PRODUCTIONLINEID_FLD].ToString()))
					{
						if(dstRouting.Tables[0].Rows.Count > 0)
						{
							DialogResult enumResult = PCSMessageBox.Show(ErrorCode.MESSAGE_DELETE_RECORD,MessageBoxButtons.YesNo,MessageBoxIcon.Question);
							if(enumResult == DialogResult.Yes)
							{
								//delete routing detail
								if(dstRouting.Tables.Count > 0)
								{
									foreach(DataRow objRow in dstRouting.Tables[0].Rows)
									{
										if(objRow.RowState != DataRowState.Deleted) objRow.Delete();
									}
									boRouting.UpdateDataSet(dstRouting);
									gridRouting.Update();
									// clear routing information in child form
									voRouting = new ITM_RoutingVO();
									VOToControls(voRouting);
								}
							}
						}
					}
					txtProductionLine.Text = drwResult[PRO_ProductionLineTable.CODE_FLD].ToString();
					txtProductionLine.Tag = Convert.ToInt32(drwResult[PRO_ProductionLineTable.PRODUCTIONLINEID_FLD]);
					// clear product group information
					txtProductGroup.Text = string.Empty;
					txtProductGroup.Tag = null;
				}
			}
			catch(PCSException ex)
			{
				dstRouting.RejectChanges();
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
				dstRouting.RejectChanges();
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

			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.Default;

			#endregion Code Inserted Automatically

			
		}
		/// <summary>
		/// txtProductionLine_Validating
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Thursday, January 12 2006</date>
		private void txtProductionLine_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtProductionLine_Validating()";
			try
			{
				if (!txtProductionLine.Modified) return;
				// store current value in order to restore when user do not want to make changes
				int intProductionLine = 0;
				try
				{
					intProductionLine = Convert.ToInt32(txtProductionLine.Tag);
				}
				catch{}
				RoutingBO boRouting = new RoutingBO();
				if (txtProductionLine.Text == string.Empty)
				{
                    if(dstRouting.Tables.Count > 0)
					{
						DialogResult enumResult = PCSMessageBox.Show(ErrorCode.MESSAGE_DELETE_RECORD,MessageBoxButtons.YesNo,MessageBoxIcon.Question);
						if(enumResult == DialogResult.Yes)
						{
							//delete routing detail
							if(dstRouting.Tables[0].Rows.Count > 0)
							{
								foreach(DataRow objRow in dstRouting.Tables[0].Rows)
								{
									if(objRow.RowState != DataRowState.Deleted) objRow.Delete();
								}
									
								boRouting.UpdateDataSet(dstRouting);
								gridRouting.Update();
								// clear routing information in child form
								voRouting = new ITM_RoutingVO();
								VOToControls(voRouting);
							}
						}
					}
					txtProductionLine.Tag = null;
					txtWorkCenter.Text = string.Empty;
					txtWorkCenter.Tag = null;
					txtFunction.Text = string.Empty;
					txtFunctionName.Text = string.Empty;
					txtFunction.Tag = null;
					// added: dungla 17-02-2006
					// clear product group info
					txtProductGroup.Text = string.Empty;
					txtProductGroup.Tag = null;
					// end added: dungla 17-02-2006
					return;
				}
				//Call OpenSearchForm for selecting Production Line
				DataRowView drwResult = FormControlComponents.OpenSearchForm(PRO_ProductionLineTable.TABLE_NAME, PRO_ProductionLineTable.CODE_FLD, txtProductionLine.Text.Trim(), null, false);
				
				//If has Production Line matched searching condition, fill values to form's controls
				if(drwResult != null)
				{
					// user select another production line
					if (intProductionLine != int.Parse(drwResult[PRO_ProductionLineTable.PRODUCTIONLINEID_FLD].ToString()))
					{
						if(dstRouting.Tables.Count > 0)
						{
							DialogResult enumResult = PCSMessageBox.Show(ErrorCode.MESSAGE_DELETE_RECORD,MessageBoxButtons.YesNo,MessageBoxIcon.Question);
							if(enumResult == DialogResult.Yes)
							{
								//delete routing detail
								if(dstRouting.Tables[0].Rows.Count > 0)
								{
									foreach(DataRow objRow in dstRouting.Tables[0].Rows)
									{
										if(objRow.RowState != DataRowState.Deleted) objRow.Delete();
									}
									
									boRouting.UpdateDataSet(dstRouting);
									gridRouting.Update();
									// clear routing information in child form
									voRouting = new ITM_RoutingVO();
									VOToControls(voRouting);
								}
							}
						}
					}
					txtProductionLine.Text = drwResult[PRO_ProductionLineTable.CODE_FLD].ToString();
					txtProductionLine.Tag = Convert.ToInt32(drwResult[PRO_ProductionLineTable.PRODUCTIONLINEID_FLD]);
					// clear product group information
					txtProductGroup.Text = string.Empty;
					txtProductGroup.Tag = null;
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

		private void txtProductionLine_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			// HACKED: dungla 15-02-2006
			if (e.KeyCode == Keys.F4)
			{
				if (sender.Equals(txtProductionLine) && btnProductionLine.Enabled)
					btnProductionLine_Click(sender, new EventArgs());
				else if (sender.Equals(txtProductGroup) && btnProductGroup.Enabled)
					btnProductGroup_Click(null, null);
				else if (sender.Equals(txtCostCenterRate) && btnCostCenterRate.Enabled)
					btnCostCenterRate_Click(null, null);
			}
			// END: dungla 15-02-2006
		}

		private void btnProductGroup_Click(object sender, System.EventArgs e)
		{
			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.WaitCursor;

			#endregion Code Inserted Automatically

			
			const string METHOD_NAME = THIS + ".btnProductGroup_Click()";
			try
			{
				DataRowView drvResult = null;
				Hashtable htbCondition = new Hashtable();
				if (txtProductionLine.Tag != null)
					htbCondition.Add(CST_ProductGroupTable.PRODUCTIONLINEID_FLD, txtProductionLine.Tag.ToString());
				else
				{
					string[] strMsg = new string[2];
					strMsg[0] = lblProductionLine.Text;
					strMsg[1] = lblProductGroup.Text;
					PCSMessageBox.Show(ErrorCode.MESSAGE_SELECT_ONE_BEFORE_SELECT_ONE, MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, strMsg);
					txtProductionLine.Focus();
					// Code Inserted Automatically
#region Code Inserted Automatically
this.Cursor = Cursors.Default;
#endregion Code Inserted Automatically

					return;
				}
				if (sender is TextBox && sender != null)
					drvResult = FormControlComponents.OpenSearchForm(CST_ProductGroupTable.TABLE_NAME, CST_ProductGroupTable.CODE_FLD,
						txtProductGroup.Text.Trim(), htbCondition, false);
				else
					drvResult = FormControlComponents.OpenSearchForm(CST_ProductGroupTable.TABLE_NAME, CST_ProductGroupTable.CODE_FLD,
						txtProductGroup.Text.Trim(), htbCondition, true);
				if (drvResult != null)
				{
					txtProductGroup.Text = drvResult[CST_ProductGroupTable.CODE_FLD].ToString();
					txtProductGroup.Tag = Convert.ToInt32(drvResult[CST_ProductGroupTable.PRODUCTGROUPID_FLD]);
				}
				else
					txtProductGroup.Focus();
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

			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.Default;

			#endregion Code Inserted Automatically

			
		}

		private void btnCostCenterRate_Click(object sender, System.EventArgs e)
		{
			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.WaitCursor;

			#endregion Code Inserted Automatically

			
			const string METHOD_NAME = THIS + ".btnCostCenterRate_Click()";
			try
			{
				DataRowView drvResult = null;
				Hashtable htbCondition = new Hashtable();
				if (cboCCN.SelectedValue != null)
					htbCondition.Add(STD_CostCenterRateMasterTable.CCNID_FLD, cboCCN.SelectedValue.ToString());
				else
				{
					PCSMessageBox.Show(ErrorCode.MESSAGE_RGA_CCN, MessageBoxIcon.Warning);
					cboCCN.Focus();
					// Code Inserted Automatically
#region Code Inserted Automatically
this.Cursor = Cursors.Default;
#endregion Code Inserted Automatically

					return;
				}
				if (sender is TextBox && sender != null)
					drvResult = FormControlComponents.OpenSearchForm(STD_CostCenterRateMasterTable.TABLE_NAME, STD_CostCenterRateMasterTable.CODE_FLD,
						txtCostCenterRate.Text.Trim(), htbCondition, false);
				else
					drvResult = FormControlComponents.OpenSearchForm(STD_CostCenterRateMasterTable.TABLE_NAME, STD_CostCenterRateMasterTable.CODE_FLD,
						txtCostCenterRate.Text.Trim(), htbCondition, true);
				if (drvResult != null)
				{
					txtCostCenterRate.Text = drvResult[STD_CostCenterRateMasterTable.CODE_FLD].ToString();
					txtCostCenterRate.Tag = Convert.ToInt32(drvResult[STD_CostCenterRateMasterTable.COSTCENTERRATEMASTERID_FLD]);
				}
				else
					txtCostCenterRate.Focus();
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

			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.Default;

			#endregion Code Inserted Automatically

			
		}

		private void Control_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".Control_Validating()";
			try
			{
				if (sender.Equals(txtProductGroup))
				{
					if (txtProductGroup.Modified && btnProductGroup.Enabled)
					{
						if (txtProductGroup.Text.Trim() != string.Empty)
							btnProductGroup_Click(sender, e);
						else
						{
							txtProductGroup.Text = string.Empty;
							txtProductGroup.Tag = null;
						}
					}
				}
				else if (sender.Equals(txtCostCenterRate))
				{
					if (txtCostCenterRate.Modified && btnCostCenterRate.Enabled)
					{
						if (txtCostCenterRate.Text.Trim() != string.Empty)
							btnCostCenterRate_Click(sender, e);
						else
						{
							txtCostCenterRate.Text = string.Empty;
							txtCostCenterRate.Tag = null;
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
		/// cboPacer_SelectedIndexChanged
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Monday, April 10 2006</date>
		private void cboPacer_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".cboPacer_SelectedIndexChanged()";
			try
			{
				switch (cboPacer.SelectedIndex) 
				{
					case 0:
						lblMachine.ForeColor = Color.Maroon;
						lblMachSetup.ForeColor = Color.Maroon;
						break;
					case 1:	
						lblMachSetup.ForeColor = Color.Maroon;
						lblMachine.ForeColor = Color.Black;	
						break;	
					case 2:
						lblMachSetup.ForeColor = Color.Black;
						lblMachine.ForeColor = Color.Maroon;	
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
	}
}
