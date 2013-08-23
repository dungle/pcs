using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using C1.Win.C1List;
using PCSComUtils.Common;
using PCSComUtils.Common.BO;
using PCSComUtils.MasterSetup.BO;
using PCSComUtils.MasterSetup.DS;
using PCSComUtils.PCSExc;
using PCSUtils.Log;
using PCSUtils.Utils;

namespace PCSUtils.MasterSetup.SystemTable
{
	/// <summary>
	/// Summary description for Location.
	/// </summary>
	public class Location : Form
	{
		#region Declaration
		
		#region Form controls

		private Button btnClose;
		private Button btnHelp;
		private Button btnAdd;
		private Button btnSave;
		private Button btnDelete;
		private Button btnEdit;
		private Label lblCountry;
		private TextBox txtZIP;
		private Label lblZIP;
		private TextBox txtState;
		private Label lblState;
		private Label lblCity;
		private TextBox txtAddress;
		private Label lblAddress;
		private Button btnContact;
		private TextBox txtDescription;
		private Label lblDescription;
		private Label lblCode;
		private TextBox txtPartyCode;
		private TextBox txtLocationCode;

		#endregion

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private Container components = null;

		private const string THIS = "PCSUtils.MasterSetup.SystemTable.Location";
		private MST_PartyVO voParty;
		private MST_PartyLocationVO voPartyLocation = new MST_PartyLocationVO();
		private EnumAction mFormMode = EnumAction.Default;
		private DataTable tblCityData;
		private Button btnSearchCode;
		UtilsBO boUtils = new UtilsBO();
		LocationBO boLocation = new LocationBO();
		private Label lblParty;
		private int intCountryID = 0;
		private C1.Win.C1List.C1Combo cboCity;
		private C1.Win.C1List.C1Combo cboCountry;

		private int intCityID = 0;
		private bool blnDataIsValid = false;
		
		#endregion Declaration

		#region Properties
		
		public int DefaultCountryID
		{
			set { intCountryID = value;}
		}
		public int DefaultCityID
		{
			set { intCityID = value;}
		}
		

		public EnumAction FormMode
		{
			get { return mFormMode; }
			set { mFormMode = value; }
		}

		public Location()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		//**************************************************************************              
		///    <Description>
		///       Parameterisze ctor, load location of party
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
		///       DungLA
		///    </Authors>
		///    <History>
		///    04-Mar-2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public Location(MST_PartyVO pvoParty)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
			voParty = pvoParty;
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(Location));
			this.txtPartyCode = new System.Windows.Forms.TextBox();
			this.lblParty = new System.Windows.Forms.Label();
			this.btnClose = new System.Windows.Forms.Button();
			this.btnHelp = new System.Windows.Forms.Button();
			this.btnAdd = new System.Windows.Forms.Button();
			this.btnSave = new System.Windows.Forms.Button();
			this.btnDelete = new System.Windows.Forms.Button();
			this.btnEdit = new System.Windows.Forms.Button();
			this.lblCountry = new System.Windows.Forms.Label();
			this.txtZIP = new System.Windows.Forms.TextBox();
			this.lblZIP = new System.Windows.Forms.Label();
			this.txtState = new System.Windows.Forms.TextBox();
			this.lblState = new System.Windows.Forms.Label();
			this.lblCity = new System.Windows.Forms.Label();
			this.txtAddress = new System.Windows.Forms.TextBox();
			this.lblAddress = new System.Windows.Forms.Label();
			this.btnContact = new System.Windows.Forms.Button();
			this.txtDescription = new System.Windows.Forms.TextBox();
			this.lblDescription = new System.Windows.Forms.Label();
			this.lblCode = new System.Windows.Forms.Label();
			this.txtLocationCode = new System.Windows.Forms.TextBox();
			this.btnSearchCode = new System.Windows.Forms.Button();
			this.cboCity = new C1.Win.C1List.C1Combo();
			this.cboCountry = new C1.Win.C1List.C1Combo();
			((System.ComponentModel.ISupportInitialize)(this.cboCity)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.cboCountry)).BeginInit();
			this.SuspendLayout();
			// 
			// txtPartyCode
			// 
			this.txtPartyCode.AccessibleDescription = resources.GetString("txtPartyCode.AccessibleDescription");
			this.txtPartyCode.AccessibleName = resources.GetString("txtPartyCode.AccessibleName");
			this.txtPartyCode.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("txtPartyCode.Anchor")));
			this.txtPartyCode.AutoSize = ((bool)(resources.GetObject("txtPartyCode.AutoSize")));
			this.txtPartyCode.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("txtPartyCode.BackgroundImage")));
			this.txtPartyCode.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("txtPartyCode.Dock")));
			this.txtPartyCode.Enabled = ((bool)(resources.GetObject("txtPartyCode.Enabled")));
			this.txtPartyCode.Font = ((System.Drawing.Font)(resources.GetObject("txtPartyCode.Font")));
			this.txtPartyCode.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("txtPartyCode.ImeMode")));
			this.txtPartyCode.Location = ((System.Drawing.Point)(resources.GetObject("txtPartyCode.Location")));
			this.txtPartyCode.MaxLength = ((int)(resources.GetObject("txtPartyCode.MaxLength")));
			this.txtPartyCode.Multiline = ((bool)(resources.GetObject("txtPartyCode.Multiline")));
			this.txtPartyCode.Name = "txtPartyCode";
			this.txtPartyCode.PasswordChar = ((char)(resources.GetObject("txtPartyCode.PasswordChar")));
			this.txtPartyCode.ReadOnly = true;
			this.txtPartyCode.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("txtPartyCode.RightToLeft")));
			this.txtPartyCode.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("txtPartyCode.ScrollBars")));
			this.txtPartyCode.Size = ((System.Drawing.Size)(resources.GetObject("txtPartyCode.Size")));
			this.txtPartyCode.TabIndex = ((int)(resources.GetObject("txtPartyCode.TabIndex")));
			this.txtPartyCode.Text = resources.GetString("txtPartyCode.Text");
			this.txtPartyCode.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("txtPartyCode.TextAlign")));
			this.txtPartyCode.Visible = ((bool)(resources.GetObject("txtPartyCode.Visible")));
			this.txtPartyCode.WordWrap = ((bool)(resources.GetObject("txtPartyCode.WordWrap")));
			// 
			// lblParty
			// 
			this.lblParty.AccessibleDescription = resources.GetString("lblParty.AccessibleDescription");
			this.lblParty.AccessibleName = resources.GetString("lblParty.AccessibleName");
			this.lblParty.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblParty.Anchor")));
			this.lblParty.AutoSize = ((bool)(resources.GetObject("lblParty.AutoSize")));
			this.lblParty.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblParty.Dock")));
			this.lblParty.Enabled = ((bool)(resources.GetObject("lblParty.Enabled")));
			this.lblParty.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.lblParty.Font = ((System.Drawing.Font)(resources.GetObject("lblParty.Font")));
			this.lblParty.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblParty.Image = ((System.Drawing.Image)(resources.GetObject("lblParty.Image")));
			this.lblParty.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblParty.ImageAlign")));
			this.lblParty.ImageIndex = ((int)(resources.GetObject("lblParty.ImageIndex")));
			this.lblParty.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblParty.ImeMode")));
			this.lblParty.Location = ((System.Drawing.Point)(resources.GetObject("lblParty.Location")));
			this.lblParty.Name = "lblParty";
			this.lblParty.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblParty.RightToLeft")));
			this.lblParty.Size = ((System.Drawing.Size)(resources.GetObject("lblParty.Size")));
			this.lblParty.TabIndex = ((int)(resources.GetObject("lblParty.TabIndex")));
			this.lblParty.Text = resources.GetString("lblParty.Text");
			this.lblParty.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblParty.TextAlign")));
			this.lblParty.Visible = ((bool)(resources.GetObject("lblParty.Visible")));
			// 
			// btnClose
			// 
			this.btnClose.AccessibleDescription = resources.GetString("btnClose.AccessibleDescription");
			this.btnClose.AccessibleName = resources.GetString("btnClose.AccessibleName");
			this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnClose.Anchor")));
			this.btnClose.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClose.BackgroundImage")));
			this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnClose.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnClose.Dock")));
			this.btnClose.Enabled = ((bool)(resources.GetObject("btnClose.Enabled")));
			this.btnClose.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnClose.FlatStyle")));
			this.btnClose.Font = ((System.Drawing.Font)(resources.GetObject("btnClose.Font")));
			this.btnClose.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.Image")));
			this.btnClose.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnClose.ImageAlign")));
			this.btnClose.ImageIndex = ((int)(resources.GetObject("btnClose.ImageIndex")));
			this.btnClose.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnClose.ImeMode")));
			this.btnClose.Location = ((System.Drawing.Point)(resources.GetObject("btnClose.Location")));
			this.btnClose.Name = "btnClose";
			this.btnClose.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnClose.RightToLeft")));
			this.btnClose.Size = ((System.Drawing.Size)(resources.GetObject("btnClose.Size")));
			this.btnClose.TabIndex = ((int)(resources.GetObject("btnClose.TabIndex")));
			this.btnClose.Text = resources.GetString("btnClose.Text");
			this.btnClose.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnClose.TextAlign")));
			this.btnClose.Visible = ((bool)(resources.GetObject("btnClose.Visible")));
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			// 
			// btnHelp
			// 
			this.btnHelp.AccessibleDescription = resources.GetString("btnHelp.AccessibleDescription");
			this.btnHelp.AccessibleName = resources.GetString("btnHelp.AccessibleName");
			this.btnHelp.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnHelp.Anchor")));
			this.btnHelp.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnHelp.BackgroundImage")));
			this.btnHelp.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnHelp.Dock")));
			this.btnHelp.Enabled = ((bool)(resources.GetObject("btnHelp.Enabled")));
			this.btnHelp.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnHelp.FlatStyle")));
			this.btnHelp.Font = ((System.Drawing.Font)(resources.GetObject("btnHelp.Font")));
			this.btnHelp.Image = ((System.Drawing.Image)(resources.GetObject("btnHelp.Image")));
			this.btnHelp.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnHelp.ImageAlign")));
			this.btnHelp.ImageIndex = ((int)(resources.GetObject("btnHelp.ImageIndex")));
			this.btnHelp.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnHelp.ImeMode")));
			this.btnHelp.Location = ((System.Drawing.Point)(resources.GetObject("btnHelp.Location")));
			this.btnHelp.Name = "btnHelp";
			this.btnHelp.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnHelp.RightToLeft")));
			this.btnHelp.Size = ((System.Drawing.Size)(resources.GetObject("btnHelp.Size")));
			this.btnHelp.TabIndex = ((int)(resources.GetObject("btnHelp.TabIndex")));
			this.btnHelp.Text = resources.GetString("btnHelp.Text");
			this.btnHelp.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnHelp.TextAlign")));
			this.btnHelp.Visible = ((bool)(resources.GetObject("btnHelp.Visible")));
			this.btnHelp.Click += new System.EventHandler(this.btnHelp_Click);
			// 
			// btnAdd
			// 
			this.btnAdd.AccessibleDescription = resources.GetString("btnAdd.AccessibleDescription");
			this.btnAdd.AccessibleName = resources.GetString("btnAdd.AccessibleName");
			this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnAdd.Anchor")));
			this.btnAdd.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnAdd.BackgroundImage")));
			this.btnAdd.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnAdd.Dock")));
			this.btnAdd.Enabled = ((bool)(resources.GetObject("btnAdd.Enabled")));
			this.btnAdd.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnAdd.FlatStyle")));
			this.btnAdd.Font = ((System.Drawing.Font)(resources.GetObject("btnAdd.Font")));
			this.btnAdd.Image = ((System.Drawing.Image)(resources.GetObject("btnAdd.Image")));
			this.btnAdd.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnAdd.ImageAlign")));
			this.btnAdd.ImageIndex = ((int)(resources.GetObject("btnAdd.ImageIndex")));
			this.btnAdd.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnAdd.ImeMode")));
			this.btnAdd.Location = ((System.Drawing.Point)(resources.GetObject("btnAdd.Location")));
			this.btnAdd.Name = "btnAdd";
			this.btnAdd.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnAdd.RightToLeft")));
			this.btnAdd.Size = ((System.Drawing.Size)(resources.GetObject("btnAdd.Size")));
			this.btnAdd.TabIndex = ((int)(resources.GetObject("btnAdd.TabIndex")));
			this.btnAdd.Text = resources.GetString("btnAdd.Text");
			this.btnAdd.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnAdd.TextAlign")));
			this.btnAdd.Visible = ((bool)(resources.GetObject("btnAdd.Visible")));
			this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
			// 
			// btnSave
			// 
			this.btnSave.AccessibleDescription = resources.GetString("btnSave.AccessibleDescription");
			this.btnSave.AccessibleName = resources.GetString("btnSave.AccessibleName");
			this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnSave.Anchor")));
			this.btnSave.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSave.BackgroundImage")));
			this.btnSave.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnSave.Dock")));
			this.btnSave.Enabled = ((bool)(resources.GetObject("btnSave.Enabled")));
			this.btnSave.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnSave.FlatStyle")));
			this.btnSave.Font = ((System.Drawing.Font)(resources.GetObject("btnSave.Font")));
			this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
			this.btnSave.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnSave.ImageAlign")));
			this.btnSave.ImageIndex = ((int)(resources.GetObject("btnSave.ImageIndex")));
			this.btnSave.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnSave.ImeMode")));
			this.btnSave.Location = ((System.Drawing.Point)(resources.GetObject("btnSave.Location")));
			this.btnSave.Name = "btnSave";
			this.btnSave.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnSave.RightToLeft")));
			this.btnSave.Size = ((System.Drawing.Size)(resources.GetObject("btnSave.Size")));
			this.btnSave.TabIndex = ((int)(resources.GetObject("btnSave.TabIndex")));
			this.btnSave.Text = resources.GetString("btnSave.Text");
			this.btnSave.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnSave.TextAlign")));
			this.btnSave.Visible = ((bool)(resources.GetObject("btnSave.Visible")));
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			// 
			// btnDelete
			// 
			this.btnDelete.AccessibleDescription = resources.GetString("btnDelete.AccessibleDescription");
			this.btnDelete.AccessibleName = resources.GetString("btnDelete.AccessibleName");
			this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnDelete.Anchor")));
			this.btnDelete.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnDelete.BackgroundImage")));
			this.btnDelete.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnDelete.Dock")));
			this.btnDelete.Enabled = ((bool)(resources.GetObject("btnDelete.Enabled")));
			this.btnDelete.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnDelete.FlatStyle")));
			this.btnDelete.Font = ((System.Drawing.Font)(resources.GetObject("btnDelete.Font")));
			this.btnDelete.Image = ((System.Drawing.Image)(resources.GetObject("btnDelete.Image")));
			this.btnDelete.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnDelete.ImageAlign")));
			this.btnDelete.ImageIndex = ((int)(resources.GetObject("btnDelete.ImageIndex")));
			this.btnDelete.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnDelete.ImeMode")));
			this.btnDelete.Location = ((System.Drawing.Point)(resources.GetObject("btnDelete.Location")));
			this.btnDelete.Name = "btnDelete";
			this.btnDelete.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnDelete.RightToLeft")));
			this.btnDelete.Size = ((System.Drawing.Size)(resources.GetObject("btnDelete.Size")));
			this.btnDelete.TabIndex = ((int)(resources.GetObject("btnDelete.TabIndex")));
			this.btnDelete.Text = resources.GetString("btnDelete.Text");
			this.btnDelete.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnDelete.TextAlign")));
			this.btnDelete.Visible = ((bool)(resources.GetObject("btnDelete.Visible")));
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
			// 
			// btnEdit
			// 
			this.btnEdit.AccessibleDescription = resources.GetString("btnEdit.AccessibleDescription");
			this.btnEdit.AccessibleName = resources.GetString("btnEdit.AccessibleName");
			this.btnEdit.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnEdit.Anchor")));
			this.btnEdit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnEdit.BackgroundImage")));
			this.btnEdit.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnEdit.Dock")));
			this.btnEdit.Enabled = ((bool)(resources.GetObject("btnEdit.Enabled")));
			this.btnEdit.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnEdit.FlatStyle")));
			this.btnEdit.Font = ((System.Drawing.Font)(resources.GetObject("btnEdit.Font")));
			this.btnEdit.Image = ((System.Drawing.Image)(resources.GetObject("btnEdit.Image")));
			this.btnEdit.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnEdit.ImageAlign")));
			this.btnEdit.ImageIndex = ((int)(resources.GetObject("btnEdit.ImageIndex")));
			this.btnEdit.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnEdit.ImeMode")));
			this.btnEdit.Location = ((System.Drawing.Point)(resources.GetObject("btnEdit.Location")));
			this.btnEdit.Name = "btnEdit";
			this.btnEdit.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnEdit.RightToLeft")));
			this.btnEdit.Size = ((System.Drawing.Size)(resources.GetObject("btnEdit.Size")));
			this.btnEdit.TabIndex = ((int)(resources.GetObject("btnEdit.TabIndex")));
			this.btnEdit.Text = resources.GetString("btnEdit.Text");
			this.btnEdit.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnEdit.TextAlign")));
			this.btnEdit.Visible = ((bool)(resources.GetObject("btnEdit.Visible")));
			this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
			// 
			// lblCountry
			// 
			this.lblCountry.AccessibleDescription = resources.GetString("lblCountry.AccessibleDescription");
			this.lblCountry.AccessibleName = resources.GetString("lblCountry.AccessibleName");
			this.lblCountry.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblCountry.Anchor")));
			this.lblCountry.AutoSize = ((bool)(resources.GetObject("lblCountry.AutoSize")));
			this.lblCountry.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblCountry.Dock")));
			this.lblCountry.Enabled = ((bool)(resources.GetObject("lblCountry.Enabled")));
			this.lblCountry.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.lblCountry.Font = ((System.Drawing.Font)(resources.GetObject("lblCountry.Font")));
			this.lblCountry.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblCountry.Image = ((System.Drawing.Image)(resources.GetObject("lblCountry.Image")));
			this.lblCountry.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblCountry.ImageAlign")));
			this.lblCountry.ImageIndex = ((int)(resources.GetObject("lblCountry.ImageIndex")));
			this.lblCountry.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblCountry.ImeMode")));
			this.lblCountry.Location = ((System.Drawing.Point)(resources.GetObject("lblCountry.Location")));
			this.lblCountry.Name = "lblCountry";
			this.lblCountry.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblCountry.RightToLeft")));
			this.lblCountry.Size = ((System.Drawing.Size)(resources.GetObject("lblCountry.Size")));
			this.lblCountry.TabIndex = ((int)(resources.GetObject("lblCountry.TabIndex")));
			this.lblCountry.Text = resources.GetString("lblCountry.Text");
			this.lblCountry.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblCountry.TextAlign")));
			this.lblCountry.Visible = ((bool)(resources.GetObject("lblCountry.Visible")));
			// 
			// txtZIP
			// 
			this.txtZIP.AccessibleDescription = resources.GetString("txtZIP.AccessibleDescription");
			this.txtZIP.AccessibleName = resources.GetString("txtZIP.AccessibleName");
			this.txtZIP.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("txtZIP.Anchor")));
			this.txtZIP.AutoSize = ((bool)(resources.GetObject("txtZIP.AutoSize")));
			this.txtZIP.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("txtZIP.BackgroundImage")));
			this.txtZIP.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("txtZIP.Dock")));
			this.txtZIP.Enabled = ((bool)(resources.GetObject("txtZIP.Enabled")));
			this.txtZIP.Font = ((System.Drawing.Font)(resources.GetObject("txtZIP.Font")));
			this.txtZIP.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("txtZIP.ImeMode")));
			this.txtZIP.Location = ((System.Drawing.Point)(resources.GetObject("txtZIP.Location")));
			this.txtZIP.MaxLength = ((int)(resources.GetObject("txtZIP.MaxLength")));
			this.txtZIP.Multiline = ((bool)(resources.GetObject("txtZIP.Multiline")));
			this.txtZIP.Name = "txtZIP";
			this.txtZIP.PasswordChar = ((char)(resources.GetObject("txtZIP.PasswordChar")));
			this.txtZIP.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("txtZIP.RightToLeft")));
			this.txtZIP.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("txtZIP.ScrollBars")));
			this.txtZIP.Size = ((System.Drawing.Size)(resources.GetObject("txtZIP.Size")));
			this.txtZIP.TabIndex = ((int)(resources.GetObject("txtZIP.TabIndex")));
			this.txtZIP.Text = resources.GetString("txtZIP.Text");
			this.txtZIP.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("txtZIP.TextAlign")));
			this.txtZIP.Visible = ((bool)(resources.GetObject("txtZIP.Visible")));
			this.txtZIP.WordWrap = ((bool)(resources.GetObject("txtZIP.WordWrap")));
			this.txtZIP.Leave += new System.EventHandler(this.OnLeaveControl);
			this.txtZIP.Enter += new System.EventHandler(this.OnEnterControl);
			// 
			// lblZIP
			// 
			this.lblZIP.AccessibleDescription = resources.GetString("lblZIP.AccessibleDescription");
			this.lblZIP.AccessibleName = resources.GetString("lblZIP.AccessibleName");
			this.lblZIP.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblZIP.Anchor")));
			this.lblZIP.AutoSize = ((bool)(resources.GetObject("lblZIP.AutoSize")));
			this.lblZIP.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblZIP.Dock")));
			this.lblZIP.Enabled = ((bool)(resources.GetObject("lblZIP.Enabled")));
			this.lblZIP.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.lblZIP.Font = ((System.Drawing.Font)(resources.GetObject("lblZIP.Font")));
			this.lblZIP.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblZIP.Image = ((System.Drawing.Image)(resources.GetObject("lblZIP.Image")));
			this.lblZIP.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblZIP.ImageAlign")));
			this.lblZIP.ImageIndex = ((int)(resources.GetObject("lblZIP.ImageIndex")));
			this.lblZIP.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblZIP.ImeMode")));
			this.lblZIP.Location = ((System.Drawing.Point)(resources.GetObject("lblZIP.Location")));
			this.lblZIP.Name = "lblZIP";
			this.lblZIP.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblZIP.RightToLeft")));
			this.lblZIP.Size = ((System.Drawing.Size)(resources.GetObject("lblZIP.Size")));
			this.lblZIP.TabIndex = ((int)(resources.GetObject("lblZIP.TabIndex")));
			this.lblZIP.Text = resources.GetString("lblZIP.Text");
			this.lblZIP.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblZIP.TextAlign")));
			this.lblZIP.Visible = ((bool)(resources.GetObject("lblZIP.Visible")));
			// 
			// txtState
			// 
			this.txtState.AccessibleDescription = resources.GetString("txtState.AccessibleDescription");
			this.txtState.AccessibleName = resources.GetString("txtState.AccessibleName");
			this.txtState.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("txtState.Anchor")));
			this.txtState.AutoSize = ((bool)(resources.GetObject("txtState.AutoSize")));
			this.txtState.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("txtState.BackgroundImage")));
			this.txtState.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("txtState.Dock")));
			this.txtState.Enabled = ((bool)(resources.GetObject("txtState.Enabled")));
			this.txtState.Font = ((System.Drawing.Font)(resources.GetObject("txtState.Font")));
			this.txtState.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("txtState.ImeMode")));
			this.txtState.Location = ((System.Drawing.Point)(resources.GetObject("txtState.Location")));
			this.txtState.MaxLength = ((int)(resources.GetObject("txtState.MaxLength")));
			this.txtState.Multiline = ((bool)(resources.GetObject("txtState.Multiline")));
			this.txtState.Name = "txtState";
			this.txtState.PasswordChar = ((char)(resources.GetObject("txtState.PasswordChar")));
			this.txtState.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("txtState.RightToLeft")));
			this.txtState.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("txtState.ScrollBars")));
			this.txtState.Size = ((System.Drawing.Size)(resources.GetObject("txtState.Size")));
			this.txtState.TabIndex = ((int)(resources.GetObject("txtState.TabIndex")));
			this.txtState.Text = resources.GetString("txtState.Text");
			this.txtState.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("txtState.TextAlign")));
			this.txtState.Visible = ((bool)(resources.GetObject("txtState.Visible")));
			this.txtState.WordWrap = ((bool)(resources.GetObject("txtState.WordWrap")));
			this.txtState.Leave += new System.EventHandler(this.OnLeaveControl);
			this.txtState.Enter += new System.EventHandler(this.OnEnterControl);
			// 
			// lblState
			// 
			this.lblState.AccessibleDescription = resources.GetString("lblState.AccessibleDescription");
			this.lblState.AccessibleName = resources.GetString("lblState.AccessibleName");
			this.lblState.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblState.Anchor")));
			this.lblState.AutoSize = ((bool)(resources.GetObject("lblState.AutoSize")));
			this.lblState.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblState.Dock")));
			this.lblState.Enabled = ((bool)(resources.GetObject("lblState.Enabled")));
			this.lblState.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.lblState.Font = ((System.Drawing.Font)(resources.GetObject("lblState.Font")));
			this.lblState.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblState.Image = ((System.Drawing.Image)(resources.GetObject("lblState.Image")));
			this.lblState.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblState.ImageAlign")));
			this.lblState.ImageIndex = ((int)(resources.GetObject("lblState.ImageIndex")));
			this.lblState.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblState.ImeMode")));
			this.lblState.Location = ((System.Drawing.Point)(resources.GetObject("lblState.Location")));
			this.lblState.Name = "lblState";
			this.lblState.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblState.RightToLeft")));
			this.lblState.Size = ((System.Drawing.Size)(resources.GetObject("lblState.Size")));
			this.lblState.TabIndex = ((int)(resources.GetObject("lblState.TabIndex")));
			this.lblState.Text = resources.GetString("lblState.Text");
			this.lblState.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblState.TextAlign")));
			this.lblState.Visible = ((bool)(resources.GetObject("lblState.Visible")));
			// 
			// lblCity
			// 
			this.lblCity.AccessibleDescription = resources.GetString("lblCity.AccessibleDescription");
			this.lblCity.AccessibleName = resources.GetString("lblCity.AccessibleName");
			this.lblCity.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblCity.Anchor")));
			this.lblCity.AutoSize = ((bool)(resources.GetObject("lblCity.AutoSize")));
			this.lblCity.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblCity.Dock")));
			this.lblCity.Enabled = ((bool)(resources.GetObject("lblCity.Enabled")));
			this.lblCity.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.lblCity.Font = ((System.Drawing.Font)(resources.GetObject("lblCity.Font")));
			this.lblCity.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblCity.Image = ((System.Drawing.Image)(resources.GetObject("lblCity.Image")));
			this.lblCity.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblCity.ImageAlign")));
			this.lblCity.ImageIndex = ((int)(resources.GetObject("lblCity.ImageIndex")));
			this.lblCity.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblCity.ImeMode")));
			this.lblCity.Location = ((System.Drawing.Point)(resources.GetObject("lblCity.Location")));
			this.lblCity.Name = "lblCity";
			this.lblCity.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblCity.RightToLeft")));
			this.lblCity.Size = ((System.Drawing.Size)(resources.GetObject("lblCity.Size")));
			this.lblCity.TabIndex = ((int)(resources.GetObject("lblCity.TabIndex")));
			this.lblCity.Text = resources.GetString("lblCity.Text");
			this.lblCity.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblCity.TextAlign")));
			this.lblCity.Visible = ((bool)(resources.GetObject("lblCity.Visible")));
			// 
			// txtAddress
			// 
			this.txtAddress.AccessibleDescription = resources.GetString("txtAddress.AccessibleDescription");
			this.txtAddress.AccessibleName = resources.GetString("txtAddress.AccessibleName");
			this.txtAddress.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("txtAddress.Anchor")));
			this.txtAddress.AutoSize = ((bool)(resources.GetObject("txtAddress.AutoSize")));
			this.txtAddress.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("txtAddress.BackgroundImage")));
			this.txtAddress.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("txtAddress.Dock")));
			this.txtAddress.Enabled = ((bool)(resources.GetObject("txtAddress.Enabled")));
			this.txtAddress.Font = ((System.Drawing.Font)(resources.GetObject("txtAddress.Font")));
			this.txtAddress.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("txtAddress.ImeMode")));
			this.txtAddress.Location = ((System.Drawing.Point)(resources.GetObject("txtAddress.Location")));
			this.txtAddress.MaxLength = ((int)(resources.GetObject("txtAddress.MaxLength")));
			this.txtAddress.Multiline = ((bool)(resources.GetObject("txtAddress.Multiline")));
			this.txtAddress.Name = "txtAddress";
			this.txtAddress.PasswordChar = ((char)(resources.GetObject("txtAddress.PasswordChar")));
			this.txtAddress.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("txtAddress.RightToLeft")));
			this.txtAddress.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("txtAddress.ScrollBars")));
			this.txtAddress.Size = ((System.Drawing.Size)(resources.GetObject("txtAddress.Size")));
			this.txtAddress.TabIndex = ((int)(resources.GetObject("txtAddress.TabIndex")));
			this.txtAddress.Text = resources.GetString("txtAddress.Text");
			this.txtAddress.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("txtAddress.TextAlign")));
			this.txtAddress.Visible = ((bool)(resources.GetObject("txtAddress.Visible")));
			this.txtAddress.WordWrap = ((bool)(resources.GetObject("txtAddress.WordWrap")));
			this.txtAddress.Leave += new System.EventHandler(this.OnLeaveControl);
			this.txtAddress.Enter += new System.EventHandler(this.OnEnterControl);
			// 
			// lblAddress
			// 
			this.lblAddress.AccessibleDescription = resources.GetString("lblAddress.AccessibleDescription");
			this.lblAddress.AccessibleName = resources.GetString("lblAddress.AccessibleName");
			this.lblAddress.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblAddress.Anchor")));
			this.lblAddress.AutoSize = ((bool)(resources.GetObject("lblAddress.AutoSize")));
			this.lblAddress.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblAddress.Dock")));
			this.lblAddress.Enabled = ((bool)(resources.GetObject("lblAddress.Enabled")));
			this.lblAddress.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.lblAddress.Font = ((System.Drawing.Font)(resources.GetObject("lblAddress.Font")));
			this.lblAddress.ForeColor = System.Drawing.Color.Maroon;
			this.lblAddress.Image = ((System.Drawing.Image)(resources.GetObject("lblAddress.Image")));
			this.lblAddress.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblAddress.ImageAlign")));
			this.lblAddress.ImageIndex = ((int)(resources.GetObject("lblAddress.ImageIndex")));
			this.lblAddress.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblAddress.ImeMode")));
			this.lblAddress.Location = ((System.Drawing.Point)(resources.GetObject("lblAddress.Location")));
			this.lblAddress.Name = "lblAddress";
			this.lblAddress.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblAddress.RightToLeft")));
			this.lblAddress.Size = ((System.Drawing.Size)(resources.GetObject("lblAddress.Size")));
			this.lblAddress.TabIndex = ((int)(resources.GetObject("lblAddress.TabIndex")));
			this.lblAddress.Text = resources.GetString("lblAddress.Text");
			this.lblAddress.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblAddress.TextAlign")));
			this.lblAddress.Visible = ((bool)(resources.GetObject("lblAddress.Visible")));
			// 
			// btnContact
			// 
			this.btnContact.AccessibleDescription = resources.GetString("btnContact.AccessibleDescription");
			this.btnContact.AccessibleName = resources.GetString("btnContact.AccessibleName");
			this.btnContact.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnContact.Anchor")));
			this.btnContact.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnContact.BackgroundImage")));
			this.btnContact.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnContact.Dock")));
			this.btnContact.Enabled = ((bool)(resources.GetObject("btnContact.Enabled")));
			this.btnContact.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnContact.FlatStyle")));
			this.btnContact.Font = ((System.Drawing.Font)(resources.GetObject("btnContact.Font")));
			this.btnContact.Image = ((System.Drawing.Image)(resources.GetObject("btnContact.Image")));
			this.btnContact.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnContact.ImageAlign")));
			this.btnContact.ImageIndex = ((int)(resources.GetObject("btnContact.ImageIndex")));
			this.btnContact.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnContact.ImeMode")));
			this.btnContact.Location = ((System.Drawing.Point)(resources.GetObject("btnContact.Location")));
			this.btnContact.Name = "btnContact";
			this.btnContact.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnContact.RightToLeft")));
			this.btnContact.Size = ((System.Drawing.Size)(resources.GetObject("btnContact.Size")));
			this.btnContact.TabIndex = ((int)(resources.GetObject("btnContact.TabIndex")));
			this.btnContact.Text = resources.GetString("btnContact.Text");
			this.btnContact.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnContact.TextAlign")));
			this.btnContact.Visible = ((bool)(resources.GetObject("btnContact.Visible")));
			this.btnContact.Click += new System.EventHandler(this.btnContact_Click);
			// 
			// txtDescription
			// 
			this.txtDescription.AccessibleDescription = resources.GetString("txtDescription.AccessibleDescription");
			this.txtDescription.AccessibleName = resources.GetString("txtDescription.AccessibleName");
			this.txtDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("txtDescription.Anchor")));
			this.txtDescription.AutoSize = ((bool)(resources.GetObject("txtDescription.AutoSize")));
			this.txtDescription.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("txtDescription.BackgroundImage")));
			this.txtDescription.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("txtDescription.Dock")));
			this.txtDescription.Enabled = ((bool)(resources.GetObject("txtDescription.Enabled")));
			this.txtDescription.Font = ((System.Drawing.Font)(resources.GetObject("txtDescription.Font")));
			this.txtDescription.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("txtDescription.ImeMode")));
			this.txtDescription.Location = ((System.Drawing.Point)(resources.GetObject("txtDescription.Location")));
			this.txtDescription.MaxLength = ((int)(resources.GetObject("txtDescription.MaxLength")));
			this.txtDescription.Multiline = ((bool)(resources.GetObject("txtDescription.Multiline")));
			this.txtDescription.Name = "txtDescription";
			this.txtDescription.PasswordChar = ((char)(resources.GetObject("txtDescription.PasswordChar")));
			this.txtDescription.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("txtDescription.RightToLeft")));
			this.txtDescription.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("txtDescription.ScrollBars")));
			this.txtDescription.Size = ((System.Drawing.Size)(resources.GetObject("txtDescription.Size")));
			this.txtDescription.TabIndex = ((int)(resources.GetObject("txtDescription.TabIndex")));
			this.txtDescription.Text = resources.GetString("txtDescription.Text");
			this.txtDescription.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("txtDescription.TextAlign")));
			this.txtDescription.Visible = ((bool)(resources.GetObject("txtDescription.Visible")));
			this.txtDescription.WordWrap = ((bool)(resources.GetObject("txtDescription.WordWrap")));
			this.txtDescription.Leave += new System.EventHandler(this.OnLeaveControl);
			this.txtDescription.Enter += new System.EventHandler(this.OnEnterControl);
			// 
			// lblDescription
			// 
			this.lblDescription.AccessibleDescription = resources.GetString("lblDescription.AccessibleDescription");
			this.lblDescription.AccessibleName = resources.GetString("lblDescription.AccessibleName");
			this.lblDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblDescription.Anchor")));
			this.lblDescription.AutoSize = ((bool)(resources.GetObject("lblDescription.AutoSize")));
			this.lblDescription.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblDescription.Dock")));
			this.lblDescription.Enabled = ((bool)(resources.GetObject("lblDescription.Enabled")));
			this.lblDescription.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.lblDescription.Font = ((System.Drawing.Font)(resources.GetObject("lblDescription.Font")));
			this.lblDescription.ForeColor = System.Drawing.Color.Maroon;
			this.lblDescription.Image = ((System.Drawing.Image)(resources.GetObject("lblDescription.Image")));
			this.lblDescription.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblDescription.ImageAlign")));
			this.lblDescription.ImageIndex = ((int)(resources.GetObject("lblDescription.ImageIndex")));
			this.lblDescription.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblDescription.ImeMode")));
			this.lblDescription.Location = ((System.Drawing.Point)(resources.GetObject("lblDescription.Location")));
			this.lblDescription.Name = "lblDescription";
			this.lblDescription.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblDescription.RightToLeft")));
			this.lblDescription.Size = ((System.Drawing.Size)(resources.GetObject("lblDescription.Size")));
			this.lblDescription.TabIndex = ((int)(resources.GetObject("lblDescription.TabIndex")));
			this.lblDescription.Text = resources.GetString("lblDescription.Text");
			this.lblDescription.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblDescription.TextAlign")));
			this.lblDescription.Visible = ((bool)(resources.GetObject("lblDescription.Visible")));
			// 
			// lblCode
			// 
			this.lblCode.AccessibleDescription = resources.GetString("lblCode.AccessibleDescription");
			this.lblCode.AccessibleName = resources.GetString("lblCode.AccessibleName");
			this.lblCode.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblCode.Anchor")));
			this.lblCode.AutoSize = ((bool)(resources.GetObject("lblCode.AutoSize")));
			this.lblCode.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblCode.Dock")));
			this.lblCode.Enabled = ((bool)(resources.GetObject("lblCode.Enabled")));
			this.lblCode.Font = ((System.Drawing.Font)(resources.GetObject("lblCode.Font")));
			this.lblCode.ForeColor = System.Drawing.Color.Maroon;
			this.lblCode.Image = ((System.Drawing.Image)(resources.GetObject("lblCode.Image")));
			this.lblCode.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblCode.ImageAlign")));
			this.lblCode.ImageIndex = ((int)(resources.GetObject("lblCode.ImageIndex")));
			this.lblCode.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblCode.ImeMode")));
			this.lblCode.Location = ((System.Drawing.Point)(resources.GetObject("lblCode.Location")));
			this.lblCode.Name = "lblCode";
			this.lblCode.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblCode.RightToLeft")));
			this.lblCode.Size = ((System.Drawing.Size)(resources.GetObject("lblCode.Size")));
			this.lblCode.TabIndex = ((int)(resources.GetObject("lblCode.TabIndex")));
			this.lblCode.Text = resources.GetString("lblCode.Text");
			this.lblCode.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblCode.TextAlign")));
			this.lblCode.Visible = ((bool)(resources.GetObject("lblCode.Visible")));
			// 
			// txtLocationCode
			// 
			this.txtLocationCode.AccessibleDescription = resources.GetString("txtLocationCode.AccessibleDescription");
			this.txtLocationCode.AccessibleName = resources.GetString("txtLocationCode.AccessibleName");
			this.txtLocationCode.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("txtLocationCode.Anchor")));
			this.txtLocationCode.AutoSize = ((bool)(resources.GetObject("txtLocationCode.AutoSize")));
			this.txtLocationCode.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("txtLocationCode.BackgroundImage")));
			this.txtLocationCode.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("txtLocationCode.Dock")));
			this.txtLocationCode.Enabled = ((bool)(resources.GetObject("txtLocationCode.Enabled")));
			this.txtLocationCode.Font = ((System.Drawing.Font)(resources.GetObject("txtLocationCode.Font")));
			this.txtLocationCode.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("txtLocationCode.ImeMode")));
			this.txtLocationCode.Location = ((System.Drawing.Point)(resources.GetObject("txtLocationCode.Location")));
			this.txtLocationCode.MaxLength = ((int)(resources.GetObject("txtLocationCode.MaxLength")));
			this.txtLocationCode.Multiline = ((bool)(resources.GetObject("txtLocationCode.Multiline")));
			this.txtLocationCode.Name = "txtLocationCode";
			this.txtLocationCode.PasswordChar = ((char)(resources.GetObject("txtLocationCode.PasswordChar")));
			this.txtLocationCode.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("txtLocationCode.RightToLeft")));
			this.txtLocationCode.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("txtLocationCode.ScrollBars")));
			this.txtLocationCode.Size = ((System.Drawing.Size)(resources.GetObject("txtLocationCode.Size")));
			this.txtLocationCode.TabIndex = ((int)(resources.GetObject("txtLocationCode.TabIndex")));
			this.txtLocationCode.Text = resources.GetString("txtLocationCode.Text");
			this.txtLocationCode.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("txtLocationCode.TextAlign")));
			this.txtLocationCode.Visible = ((bool)(resources.GetObject("txtLocationCode.Visible")));
			this.txtLocationCode.WordWrap = ((bool)(resources.GetObject("txtLocationCode.WordWrap")));
			this.txtLocationCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtLocationCode_KeyDown);
			this.txtLocationCode.Validating += new CancelEventHandler(txtLocationCode_Validating);
			// 
			// btnSearchCode
			// 
			this.btnSearchCode.AccessibleDescription = resources.GetString("btnSearchCode.AccessibleDescription");
			this.btnSearchCode.AccessibleName = resources.GetString("btnSearchCode.AccessibleName");
			this.btnSearchCode.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnSearchCode.Anchor")));
			this.btnSearchCode.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSearchCode.BackgroundImage")));
			this.btnSearchCode.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnSearchCode.Dock")));
			this.btnSearchCode.Enabled = ((bool)(resources.GetObject("btnSearchCode.Enabled")));
			this.btnSearchCode.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnSearchCode.FlatStyle")));
			this.btnSearchCode.Font = ((System.Drawing.Font)(resources.GetObject("btnSearchCode.Font")));
			this.btnSearchCode.Image = ((System.Drawing.Image)(resources.GetObject("btnSearchCode.Image")));
			this.btnSearchCode.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnSearchCode.ImageAlign")));
			this.btnSearchCode.ImageIndex = ((int)(resources.GetObject("btnSearchCode.ImageIndex")));
			this.btnSearchCode.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnSearchCode.ImeMode")));
			this.btnSearchCode.Location = ((System.Drawing.Point)(resources.GetObject("btnSearchCode.Location")));
			this.btnSearchCode.Name = "btnSearchCode";
			this.btnSearchCode.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnSearchCode.RightToLeft")));
			this.btnSearchCode.Size = ((System.Drawing.Size)(resources.GetObject("btnSearchCode.Size")));
			this.btnSearchCode.TabIndex = ((int)(resources.GetObject("btnSearchCode.TabIndex")));
			this.btnSearchCode.Text = resources.GetString("btnSearchCode.Text");
			this.btnSearchCode.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnSearchCode.TextAlign")));
			this.btnSearchCode.Visible = ((bool)(resources.GetObject("btnSearchCode.Visible")));
			this.btnSearchCode.Click += new System.EventHandler(this.btnSearchCode_Click);
			// 
			// cboCity
			// 
			this.cboCity.AccessibleDescription = resources.GetString("cboCity.AccessibleDescription");
			this.cboCity.AccessibleName = resources.GetString("cboCity.AccessibleName");
			this.cboCity.AddItemSeparator = ';';
			this.cboCity.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("cboCity.Anchor")));
			this.cboCity.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cboCity.BackgroundImage")));
			this.cboCity.Caption = "";
			this.cboCity.CaptionHeight = 17;
			this.cboCity.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
			this.cboCity.ColumnCaptionHeight = 17;
			this.cboCity.ColumnFooterHeight = 17;
			this.cboCity.ComboStyle = C1.Win.C1List.ComboStyleEnum.DropdownList;
			this.cboCity.ContentHeight = 15;
			this.cboCity.DeadAreaBackColor = System.Drawing.Color.Empty;
			this.cboCity.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("cboCity.Dock")));
			this.cboCity.EditorBackColor = System.Drawing.SystemColors.Window;
			this.cboCity.EditorFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.cboCity.EditorForeColor = System.Drawing.SystemColors.WindowText;
			this.cboCity.EditorHeight = 15;
			this.cboCity.Enabled = ((bool)(resources.GetObject("cboCity.Enabled")));
			this.cboCity.FlatStyle = C1.Win.C1List.FlatModeEnum.System;
			this.cboCity.Font = ((System.Drawing.Font)(resources.GetObject("cboCity.Font")));
			this.cboCity.GapHeight = 2;
			this.cboCity.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("cboCity.ImeMode")));
			this.cboCity.ItemHeight = 15;
			this.cboCity.Location = ((System.Drawing.Point)(resources.GetObject("cboCity.Location")));
			this.cboCity.MatchEntryTimeout = ((long)(2000));
			this.cboCity.MaxDropDownItems = ((short)(5));
			this.cboCity.MaxLength = 32767;
			this.cboCity.MouseCursor = System.Windows.Forms.Cursors.Default;
			this.cboCity.Name = "cboCity";
			this.cboCity.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("cboCity.RightToLeft")));
			this.cboCity.RowDivider.Color = System.Drawing.Color.DarkGray;
			this.cboCity.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None;
			this.cboCity.RowSubDividerColor = System.Drawing.Color.DarkGray;
			this.cboCity.Size = ((System.Drawing.Size)(resources.GetObject("cboCity.Size")));
			this.cboCity.TabIndex = ((int)(resources.GetObject("cboCity.TabIndex")));
			this.cboCity.Text = resources.GetString("cboCity.Text");
			this.cboCity.Visible = ((bool)(resources.GetObject("cboCity.Visible")));
			this.cboCity.Enter += new System.EventHandler(this.OnEnterControl);
			this.cboCity.Leave += new System.EventHandler(this.OnLeaveControl);
			this.cboCity.PropBag = "<?xml version=\"1.0\"?><Blob><Styles type=\"C1.Win.C1List.Design.ContextWrapper\"><Da" +
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
				" 116, 156</ClientRect><VScrollBar><Width>17</Width></VScrollBar><HScrollBar><Hei" +
				"ght>17</Height></HScrollBar><CaptionStyle parent=\"Style2\" me=\"Style9\" /><EvenRow" +
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
				"th>16</DefaultRecSelWidth></Blob>";
			// 
			// cboCountry
			// 
			this.cboCountry.AccessibleDescription = resources.GetString("cboCountry.AccessibleDescription");
			this.cboCountry.AccessibleName = resources.GetString("cboCountry.AccessibleName");
			this.cboCountry.AddItemSeparator = ';';
			this.cboCountry.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("cboCountry.Anchor")));
			this.cboCountry.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cboCountry.BackgroundImage")));
			this.cboCountry.Caption = "";
			this.cboCountry.CaptionHeight = 17;
			this.cboCountry.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
			this.cboCountry.ColumnCaptionHeight = 17;
			this.cboCountry.ColumnFooterHeight = 17;
			this.cboCountry.ComboStyle = C1.Win.C1List.ComboStyleEnum.DropdownList;
			this.cboCountry.ContentHeight = 15;
			this.cboCountry.DeadAreaBackColor = System.Drawing.Color.Empty;
			this.cboCountry.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("cboCountry.Dock")));
			this.cboCountry.EditorBackColor = System.Drawing.SystemColors.Window;
			this.cboCountry.EditorFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.cboCountry.EditorForeColor = System.Drawing.SystemColors.WindowText;
			this.cboCountry.EditorHeight = 15;
			this.cboCountry.Enabled = ((bool)(resources.GetObject("cboCountry.Enabled")));
			this.cboCountry.FlatStyle = C1.Win.C1List.FlatModeEnum.System;
			this.cboCountry.Font = ((System.Drawing.Font)(resources.GetObject("cboCountry.Font")));
			this.cboCountry.GapHeight = 2;
			this.cboCountry.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("cboCountry.ImeMode")));
			this.cboCountry.ItemHeight = 15;
			this.cboCountry.Location = ((System.Drawing.Point)(resources.GetObject("cboCountry.Location")));
			this.cboCountry.MatchEntryTimeout = ((long)(2000));
			this.cboCountry.MaxDropDownItems = ((short)(5));
			this.cboCountry.MaxLength = 32767;
			this.cboCountry.MouseCursor = System.Windows.Forms.Cursors.Default;
			this.cboCountry.Name = "cboCountry";
			this.cboCountry.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("cboCountry.RightToLeft")));
			this.cboCountry.RowDivider.Color = System.Drawing.Color.DarkGray;
			this.cboCountry.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None;
			this.cboCountry.RowSubDividerColor = System.Drawing.Color.DarkGray;
			this.cboCountry.Size = ((System.Drawing.Size)(resources.GetObject("cboCountry.Size")));
			this.cboCountry.TabIndex = ((int)(resources.GetObject("cboCountry.TabIndex")));
			this.cboCountry.Text = resources.GetString("cboCountry.Text");
			this.cboCountry.Visible = ((bool)(resources.GetObject("cboCountry.Visible")));
			this.cboCountry.SelectedValueChanged += new System.EventHandler(this.cboCountry_SelectedIndexChanged);
			this.cboCountry.Enter += new System.EventHandler(this.OnEnterControl);
			this.cboCountry.Leave += new System.EventHandler(this.OnLeaveControl);
			this.cboCountry.PropBag = "<?xml version=\"1.0\"?><Blob><Styles type=\"C1.Win.C1List.Design.ContextWrapper\"><Da" +
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
				" 116, 156</ClientRect><VScrollBar><Width>17</Width></VScrollBar><HScrollBar><Hei" +
				"ght>17</Height></HScrollBar><CaptionStyle parent=\"Style2\" me=\"Style9\" /><EvenRow" +
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
				"th>16</DefaultRecSelWidth></Blob>";
			// 
			// Location
			// 
			this.AccessibleDescription = resources.GetString("$this.AccessibleDescription");
			this.AccessibleName = resources.GetString("$this.AccessibleName");
			this.AutoScaleBaseSize = ((System.Drawing.Size)(resources.GetObject("$this.AutoScaleBaseSize")));
			this.AutoScroll = ((bool)(resources.GetObject("$this.AutoScroll")));
			this.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("$this.AutoScrollMargin")));
			this.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("$this.AutoScrollMinSize")));
			this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
			this.CancelButton = this.btnClose;
			this.ClientSize = ((System.Drawing.Size)(resources.GetObject("$this.ClientSize")));
			this.Controls.Add(this.cboCity);
			this.Controls.Add(this.cboCountry);
			this.Controls.Add(this.btnSearchCode);
			this.Controls.Add(this.txtLocationCode);
			this.Controls.Add(this.txtDescription);
			this.Controls.Add(this.txtPartyCode);
			this.Controls.Add(this.txtAddress);
			this.Controls.Add(this.txtZIP);
			this.Controls.Add(this.txtState);
			this.Controls.Add(this.lblDescription);
			this.Controls.Add(this.lblCode);
			this.Controls.Add(this.btnContact);
			this.Controls.Add(this.btnClose);
			this.Controls.Add(this.btnHelp);
			this.Controls.Add(this.btnAdd);
			this.Controls.Add(this.btnSave);
			this.Controls.Add(this.btnDelete);
			this.Controls.Add(this.btnEdit);
			this.Controls.Add(this.lblParty);
			this.Controls.Add(this.lblCity);
			this.Controls.Add(this.lblAddress);
			this.Controls.Add(this.lblCountry);
			this.Controls.Add(this.lblZIP);
			this.Controls.Add(this.lblState);
			this.Enabled = ((bool)(resources.GetObject("$this.Enabled")));
			this.Font = ((System.Drawing.Font)(resources.GetObject("$this.Font")));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("$this.ImeMode")));
			this.KeyPreview = true;
			this.Location = ((System.Drawing.Point)(resources.GetObject("$this.Location")));
			this.MaximizeBox = false;
			this.MaximumSize = ((System.Drawing.Size)(resources.GetObject("$this.MaximumSize")));
			this.MinimumSize = ((System.Drawing.Size)(resources.GetObject("$this.MinimumSize")));
			this.Name = "Location";
			this.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("$this.RightToLeft")));
			this.StartPosition = ((System.Windows.Forms.FormStartPosition)(resources.GetObject("$this.StartPosition")));
			this.Text = resources.GetString("$this.Text");
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Location_KeyDown);
			this.Closing += new System.ComponentModel.CancelEventHandler(this.Location_Closing);
			this.Load += new System.EventHandler(this.Location_Load);
			((System.ComponentModel.ISupportInitialize)(this.cboCity)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.cboCountry)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		#region Private Methods
		//**************************************************************************              
		///    <Description>
		///       This method uses to intial data for combo boxs in form
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
		///       DungLA
		///    </Authors>
		///    <History>
		///    03-Mar-2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private void InitComboBoxs()
		{
			try
			{
				// get all city first
				tblCityData = boUtils.ListAllCity();
				DataTable tblCountry = boUtils.ListCountry();
				FormControlComponents.PutDataIntoC1ComboBox(cboCountry, tblCountry, MST_CountryTable.CODE_FLD, MST_CountryTable.COUNTRYID_FLD, MST_CountryTable.TABLE_NAME, true);
				//cboCountry.Items.Insert(0, string.Empty);
				if (intCountryID > 0)
				{
					cboCountry.SelectedValue = intCountryID;
					if (intCityID > 0)
						cboCity.SelectedValue = intCityID;
					else
						cboCity.SelectedIndex = -1;
				}
				else
					cboCountry.SelectedIndex = -1;
			}
			catch (PCSException ex)
			{
				throw ex;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		//**************************************************************************              
		///    <Description>
		///       This method uses to switching form mode based on form action
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
		///       DungLA
		///    </Authors>
		///    <History>
		///    03-Mar-2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private void SwitchFormMode()
		{
			try
			{
				switch (mFormMode)
				{
					
					case EnumAction.Default:
						foreach (Control objControl in this.Controls)
						{
							if ((objControl is TextBox) || (objControl is ComboBox) 
								|| (objControl is C1Combo) || (objControl is CheckBox))
							{
								if (objControl != txtLocationCode)
									objControl.Enabled = false;
							}
						}
						btnAdd.Enabled = true;
						btnSave.Enabled = false;
						if (voPartyLocation.PartyLocationID > 0)
						{
							btnEdit.Enabled = true;
							btnContact.Enabled = true;
							btnDelete.Enabled = true;
						}
						else
						{
							btnContact.Enabled = false;
							btnEdit.Enabled = false;
							btnDelete.Enabled = false;
						}
						btnSearchCode.Enabled = true;
						
						break;

					default:
						foreach (Control objControl in this.Controls)
						{
							if ((objControl is TextBox) || (objControl is ComboBox) 
								|| (objControl is C1Combo) || (objControl is CheckBox))
								objControl.Enabled = true;
						}
						txtPartyCode.Enabled = false;
						txtLocationCode.Enabled = true && (mFormMode != EnumAction.Default);
						btnSearchCode.Enabled = false;
						btnContact.Enabled = false;
						btnAdd.Enabled = false;
						btnSave.Enabled = true;
						btnEdit.Enabled = false;
						btnDelete.Enabled = false;
						
						break;					
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		//**************************************************************************              
		///    <Description>
		///       This method uses to load all data from VO object to controls
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
		///       DungLA
		///    </Authors>
		///    <History>
		///    03-Mar-2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private void LoadDataToControl(MST_PartyLocationVO pvoPartyLocation)
		{
			try
			{
				if ((pvoPartyLocation != null) && (pvoPartyLocation.PartyLocationID > 0))
				{ 
					// load all data from VO to controls
					txtLocationCode.Text = pvoPartyLocation.Code;
					txtDescription.Text = pvoPartyLocation.Description;
					txtAddress.Text = pvoPartyLocation.Address;
					cboCountry.SelectedValue = pvoPartyLocation.CountryID;
					cboCity.SelectedValue = pvoPartyLocation.CityID;
					txtState.Text = pvoPartyLocation.State;
					txtZIP.Text = pvoPartyLocation.ZipPost;
				}
				else
				{
					// clear data in form
					foreach (Control objControl in this.Controls)
					{
						if (objControl is TextBox)
						{
							objControl.Text = string.Empty;
						}
						
						if (objControl is ComboBox)
						{
							((ComboBox)objControl).SelectedIndex = -1;
						}

						if (objControl is C1.Win.C1List.C1Combo)
						{
							((C1.Win.C1List.C1Combo)objControl).SelectedIndex = -1;							
						}
					}
				}

				txtPartyCode.Text = voParty.Code;
				txtPartyCode.Tag = voParty;
			}
			catch (PCSException ex)
			{
				throw ex;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		//**************************************************************************              
		///    <Description>
		///       This method uses to save data to database
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
		///       DungLA
		///    </Authors>
		///    <History>
		///    03-Mar-2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private bool SaveData()
		{
			try
			{
				voPartyLocation.PartyID = voParty.PartyID;
				voPartyLocation.Code = txtLocationCode.Text.Trim();
				voPartyLocation.Description = txtDescription.Text.Trim();
				voPartyLocation.Address = txtAddress.Text.Trim();
				try
				{
					voPartyLocation.CountryID = int.Parse(cboCountry.SelectedValue.ToString().Trim());
				}
				catch {}
				try
				{
					voPartyLocation.CityID = int.Parse(cboCity.SelectedValue.ToString().Trim());
				}
				catch {}
				voPartyLocation.State = txtState.Text.Trim();
				voPartyLocation.ZipPost = txtZIP.Text.Trim();
				switch (mFormMode)
				{
					case EnumAction.Add:
						voPartyLocation.PartyLocationID = boLocation.AddReturnID(voPartyLocation);
						break;
					case EnumAction.Edit:
						boLocation.Update(voPartyLocation);
						break;
				}
				return true;
			}
			catch (PCSException ex)
			{
				throw ex;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		//**************************************************************************              
		///    <Description>
		///       This method uses to save data to database
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
		///       DungLA
		///    </Authors>
		///    <History>
		///    03-Mar-2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private void PutDataToCityCombo(DataRow[] pdrowData)
		{
			try
			{
				// clear data source and related info first
				cboCity.DataSource = null;
				DataTable tblData = new DataTable(MST_CityTable.TABLE_NAME);
				tblData.Columns.Add(MST_CityTable.CITYID_FLD);
				tblData.Columns.Add(MST_CityTable.CODE_FLD);
				tblData.Columns.Add(MST_CityTable.NAME_FLD);
				foreach (DataRow drowTemp in pdrowData)
				{
					DataRow drowNew = tblData.NewRow();
					drowNew[MST_CityTable.CITYID_FLD] = drowTemp[MST_CityTable.CITYID_FLD];
					drowNew[MST_CityTable.CODE_FLD] = drowTemp[MST_CityTable.CODE_FLD];
					drowNew[MST_CityTable.NAME_FLD] = drowTemp[MST_CityTable.NAME_FLD];
					tblData.Rows.Add(drowNew);
				}
				FormControlComponents.PutDataIntoC1ComboBox(cboCity, tblData, MST_CityTable.CODE_FLD, MST_CityTable.CITYID_FLD, MST_CityTable.TABLE_NAME, true);
				//cboCity.Items.Insert(0, string.Empty);
			}
			catch (PCSException ex)
			{
				throw ex;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		
		//**************************************************************************              
		///    <Description>
		///       This method use to format control when Enter event raise in the form
		///    </Description>
		///    <Inputs>
		///       Control to format
		///    </Inputs>
		///    <Outputs>
		///
		///    </Outputs>
		///    <Returns>
		///       
		///    </Returns>
		///    <Authors>
		///       DungLA
		///    </Authors>
		///    <History>
		///       04-Jan-2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private void OnEnterControl(object sender, EventArgs e)
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
		//**************************************************************************              
		///    <Description>
		///       This method use to format control when Leave event raise in the form
		///    </Description>
		///    <Inputs>
		///       Control to format
		///    </Inputs>
		///    <Outputs>
		///
		///    </Outputs>
		///    <Returns>
		///       
		///    </Returns>
		///    <Authors>
		///       DungLA
		///    </Authors>
		///    <History>
		///       04-Jan-2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private void OnLeaveControl(object sender, EventArgs e)
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
		
		/// <summary>
		/// Call this method to PartyLocation open search form 
		/// </summary>
		/// <param name="pstrMethodName"></param>
		/// Method that called this method
		/// <param name="pblnAlwaysShowDialog"></param>
		/// 1: always show open search form
		/// 0: other else
		private bool SelectLocationCode(string pstrMethodName, bool pblnAlwaysShowDialog)
		{
			try				
			{	
				Hashtable htCondition = new Hashtable();
				htCondition.Add(MST_PartyLocationTable.PARTYID_FLD, voParty.PartyID);				
				
				//Call OpenSearchForm for selecting	
				DataRowView drwResult = FormControlComponents.OpenSearchForm(MST_PartyLocationTable.TABLE_NAME, MST_PartyLocationTable.CODE_FLD , txtLocationCode.Text, htCondition, pblnAlwaysShowDialog);

				// If matched searching condition, fill values to form's controls
				if(drwResult != null)
				{
					FillData(drwResult.Row);
					//Reset modify status
					txtLocationCode.Modified = false;
				}
				else if(!pblnAlwaysShowDialog)
				{
					txtLocationCode.Focus();
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

		//**************************************************************************              
		///    <Description>
		///       This method use to fill data to form
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
		///       DungLA
		///    </Authors>
		///    <History>
		///       29-Mar-2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private void FillData(DataRow pdrowData)
		{
			try
			{
				// get MST_PartyVO object
				voPartyLocation = (MST_PartyLocationVO)boLocation.GetObjectVO(int.Parse(pdrowData[MST_PartyLocationTable.PARTYLOCATIONID_FLD].ToString()), string.Empty);				
				// load data to form
				LoadDataToControl(voPartyLocation);
				// switching form mode
				SwitchFormMode();
			}
			catch (PCSException ex)
			{
				throw ex;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}		

		private void txtLocationCode_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtLocationCode_KeyDown()";
			try
			{
				if (e.KeyCode == Keys.F4 && btnSearchCode.Enabled)
				{
					SelectLocationCode(METHOD_NAME, true);
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
	
		private void txtLocationCode_Validating(object sender, CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtLocationCode_Validating()";
			
			try
			{
				//Exit immediately if BIN is empty
				if(mFormMode != EnumAction.Default) return;
				if(txtLocationCode.Text.Length == 0)
				{
					voPartyLocation = new MST_PartyLocationVO();
					voPartyLocation.PartyLocationID = 0;
					LoadDataToControl(voPartyLocation);
					SwitchFormMode();
					return;
				}
				else if(!txtLocationCode.Modified)
				{
					return;
				}

				e.Cancel = !SelectLocationCode(METHOD_NAME, false);
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

		#endregion

		#region Events Processing

		//**************************************************************************              
		///    <Description>
		///       Init data, switching form mode and fill data to form
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
		///       DungLA
		///    </Authors>
		///    <History>
		///    03-Mar-2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private void Location_Load(object sender, EventArgs e)
		{
			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.WaitCursor;

			#endregion Code Inserted Automatically

			
			const string METHOD_NAME = THIS + ".Location_Load()";
			try
			{
				this.Name = THIS;
				//Set form security
				Security objSecurity = new Security();
				if (objSecurity.SetRightForUserOnForm(this, SystemProperty.UserName) == 0)
				{
					// You don't have the right to view this item
					PCSMessageBox.Show(ErrorCode.MESSAGE_YOU_HAVE_NO_RIGHT_TO_VIEW, MessageBoxIcon.Warning);
					this.Close();
					// Code Inserted Automatically
#region Code Inserted Automatically
this.Cursor = Cursors.Default;
#endregion Code Inserted Automatically

					return;
				}

				// initial combo box
				InitComboBoxs();
				// switch form mode based on FormAction
				SwitchFormMode();
				if (mFormMode == EnumAction.Edit)
				{
					LoadDataToControl(voPartyLocation);
				}
				else
				{
					LoadDataToControl(null);
				}				
				txtLocationCode.Enabled = true;
			}
			catch (PCSException ex)
			{
				// displays the error message.
				PCSMessageBox.Show(ex.mCode, MessageBoxButtons.OK, MessageBoxIcon.Error);
				// log message.
				try
				{
					Logger.LogMessage(ex.CauseException, METHOD_NAME, Level.ERROR);
				}
				catch
				{
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
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
		///       User enter code and click search button. Display search form, allows user to select Location
		///       turn form to Edit mode if user select a Party
		///       else turn form to Add mode
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
		///       DungLA
		///    </Authors>
		///    <History>
		///    03-Mar-2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private void btnSearchCode_Click(object sender, EventArgs e)
		{
			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.WaitCursor;

			#endregion Code Inserted Automatically

			
			const string METHOD_NAME = THIS + ".btnSearchCode_Click()";
			try
			{
				SelectLocationCode(METHOD_NAME, true);
			}
			catch (PCSException ex)
			{
				// displays the error message.
				PCSMessageBox.Show(ex.mCode, MessageBoxButtons.OK, MessageBoxIcon.Error);
				// log message.
				try
				{
					Logger.LogMessage(ex.CauseException, METHOD_NAME, Level.ERROR);
				}
				catch
				{
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
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
		///       
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
		///       DungLA
		///    </Authors>
		///    <History>
		///    03-Mar-2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private void btnContact_Click(object sender, EventArgs e)
		{
			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.WaitCursor;

			#endregion Code Inserted Automatically

			
			const string METHOD_NAME = THIS + ".btnContact_Click()";
			try
			{
				if(voPartyLocation.PartyLocationID <= 0) 
{
// Code Inserted Automatically
#region Code Inserted Automatically
this.Cursor = Cursors.Default;
#endregion Code Inserted Automatically

																																													return;
}

				SystemTable.Contacts frmContact = new SystemTable.Contacts(voParty,  voPartyLocation);
				frmContact.FormMode = EnumAction.Default;
				frmContact.ShowDialog();
			}
			catch (PCSException ex)
			{
				// displays the error message.
				PCSMessageBox.Show(ex.mCode, MessageBoxButtons.OK, MessageBoxIcon.Error);
				// log message.
				try
				{
					Logger.LogMessage(ex.CauseException, METHOD_NAME, Level.ERROR);
				}
				catch
				{
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
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
		///       Turn formto ADD mode and clear data in form
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
		///       DungLA
		///    </Authors>
		///    <History>
		///    03-Mar-2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private void btnAdd_Click(object sender, EventArgs e)
		{
			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.WaitCursor;

			#endregion Code Inserted Automatically

			
			const string METHOD_NAME = THIS + ".btnAdd_Click()";
			try
			{
				// turn form to ADD mode
				mFormMode = EnumAction.Add;
				// switching form mode
				SwitchFormMode();
				// reset form
				LoadDataToControl(null);
				txtLocationCode.Text = boUtils.GetNoByMask(MST_PartyLocationTable.TABLE_NAME, MST_PartyLocationTable.CODE_FLD, boUtils.GetDBDate(), string.Empty);
				txtLocationCode.Focus();
			}
			catch (PCSException ex)
			{
				// displays the error message.
				PCSMessageBox.Show(ex.mCode, MessageBoxButtons.OK, MessageBoxIcon.Error);
				// log message.
				try
				{
					Logger.LogMessage(ex.CauseException, METHOD_NAME, Level.ERROR);
				}
				catch
				{
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
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
		///       Validate data and save to database
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
		///       DungLA
		///    </Authors>
		///    <History>
		///    03-Mar-2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private void btnSave_Click(object sender, EventArgs e)
		{
			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.WaitCursor;

			#endregion Code Inserted Automatically

			
			const string METHOD_NAME = THIS + ".btnSave_Click()";
			try
			{
				// validate data
				if (FormControlComponents.CheckMandatory(txtLocationCode))
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxButtons.OK, MessageBoxIcon.Error);
					txtLocationCode.Focus();
					txtLocationCode.SelectAll();
					blnDataIsValid = false;
					// Code Inserted Automatically
#region Code Inserted Automatically
this.Cursor = Cursors.Default;
#endregion Code Inserted Automatically

					return;
				}

				if (FormControlComponents.CheckMandatory(txtDescription))
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxButtons.OK, MessageBoxIcon.Error);
					txtDescription.Focus();
					txtDescription.SelectAll();
					blnDataIsValid = false;
					// Code Inserted Automatically
#region Code Inserted Automatically
this.Cursor = Cursors.Default;
#endregion Code Inserted Automatically

					return;				
				}

				if (FormControlComponents.CheckMandatory(txtAddress))
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxButtons.OK, MessageBoxIcon.Error);
					txtAddress.Focus();
					txtAddress.SelectAll();
					blnDataIsValid = false;
					// Code Inserted Automatically
#region Code Inserted Automatically
this.Cursor = Cursors.Default;
#endregion Code Inserted Automatically

					return;
				}

				if (SaveData())
				{
					// display successful message
					PCSMessageBox.Show(ErrorCode.MESSAGE_AFTER_SAVE_DATA, MessageBoxButtons.OK, MessageBoxIcon.Information);
					// turn to DEFAULT mode
					mFormMode = EnumAction.Default;
					// switching form mode
					SwitchFormMode();
					blnDataIsValid = true;
				}
				else
				{
					// display error message
					PCSMessageBox.Show(ErrorCode.MESSAGE_CANNOT_SAVE_TO_DB, MessageBoxButtons.OK, MessageBoxIcon.Error);
					txtLocationCode.Focus();
					txtLocationCode.SelectAll();
					blnDataIsValid = false;
				}
			}
			catch (PCSException ex)
			{
				// displays the error message.
				PCSMessageBox.Show(ex.mCode, MessageBoxButtons.OK, MessageBoxIcon.Error);
				// log message.
				try
				{
					Logger.LogMessage(ex.CauseException, METHOD_NAME, Level.ERROR);
				}
				catch
				{
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				txtLocationCode.Focus();
				txtLocationCode.SelectAll();
				blnDataIsValid = false;
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
				txtLocationCode.Focus();
				txtLocationCode.SelectAll();
				blnDataIsValid = false;
			}

			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.Default;

			#endregion Code Inserted Automatically

			
		}

		//**************************************************************************              
		///    <Description>
		///       Turn form to edit mode
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
		///       DungLA
		///    </Authors>
		///    <History>
		///    03-Mar-2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private void btnEdit_Click(object sender, EventArgs e)
		{
			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.WaitCursor;

			#endregion Code Inserted Automatically

			
			const string METHOD_NAME = THIS + ".btnEdit_Click()";
			try
			{
				if(voPartyLocation.PartyLocationID <= 0) 
{
// Code Inserted Automatically
#region Code Inserted Automatically
this.Cursor = Cursors.Default;
#endregion Code Inserted Automatically

																																													return;
}
				// turn form to EDIT mode
				mFormMode = EnumAction.Edit;
				// switching form mode
				SwitchFormMode();
				txtLocationCode.Focus();
			}
			catch (PCSException ex)
			{
				// displays the error message.
				PCSMessageBox.Show(ex.mCode, MessageBoxButtons.OK, MessageBoxIcon.Error);
				// log message.
				try
				{
					Logger.LogMessage(ex.CauseException, METHOD_NAME, Level.ERROR);
				}
				catch
				{
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
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
		///       Display confirm message and do delete action if user select Yes option.
		///       After delete successful, turn form to DEFAULT mode and clear data in form.
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
		///       DungLA
		///    </Authors>
		///    <History>
		///    03-Mar-2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private void btnDelete_Click(object sender, EventArgs e)
		{
			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.WaitCursor;

			#endregion Code Inserted Automatically

			
			const string METHOD_NAME = THIS + ".btnDelete_Click()";
			try
			{
				if(voPartyLocation.PartyLocationID <= 0) 
{
// Code Inserted Automatically
#region Code Inserted Automatically
this.Cursor = Cursors.Default;
#endregion Code Inserted Automatically

																																													return;
}
				// display confirm message
				DialogResult dlgResult = PCSMessageBox.Show(ErrorCode.MESSAGE_DELETE_RECORD, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
				if (dlgResult == DialogResult.Yes)
				{
					// do delete action
					boLocation.Delete(voPartyLocation);
					// clear data of vo object
					voPartyLocation = new MST_PartyLocationVO();
					LoadDataToControl(voPartyLocation);
					// set form mode to DEFAULT
					mFormMode = EnumAction.Default;
					// switching form mode
					SwitchFormMode();					
				}
				txtLocationCode.Focus();
			}
			catch (PCSException ex)
			{
				// displays the error message.
				PCSMessageBox.Show(ex.mCode, MessageBoxButtons.OK, MessageBoxIcon.Error);
				// log message.
				try
				{
					Logger.LogMessage(ex.CauseException, METHOD_NAME, Level.ERROR);
				}
				catch
				{
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
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
		///       
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
		///       DungLA
		///    </Authors>
		///    <History>
		///    03-Mar-2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private void btnHelp_Click(object sender, EventArgs e)
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
		///       Close the form
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
		///       DungLA
		///    </Authors>
		///    <History>
		///    03-Mar-2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private void btnClose_Click(object sender, EventArgs e)
		{
			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.WaitCursor;

			#endregion Code Inserted Automatically

			
			Close();

			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.Default;

			#endregion Code Inserted Automatically

			
		}

		//**************************************************************************              
		///    <Description>
		///       If curretn form mode is not equal to DEFAULT 
		///       and ask user to save data to database before close the form
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
		///       DungLA
		///    </Authors>
		///    <History>
		///    03-Mar-2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private void Location_Closing(object sender, CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".Party_Closing()";
			try
			{
				// if form mode is not DEFAULT then display confirm message
				if (mFormMode != EnumAction.Default)
				{
					DialogResult dlgResult = PCSMessageBox.Show(ErrorCode.MESSAGE_QUESTION_STORE_INTO_DATABASE, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
					switch (dlgResult)
					{
						case DialogResult.Yes:
							try
							{
								btnSave_Click(sender, e);
								e.Cancel = !blnDataIsValid;
							}
							catch
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
			catch (PCSException ex)
			{
				// displays the error message.
				PCSMessageBox.Show(ex.mCode, MessageBoxButtons.OK, MessageBoxIcon.Error);
				// log message.
				try
				{
					Logger.LogMessage(ex.CauseException, METHOD_NAME, Level.ERROR);
				}
				catch
				{
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
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
		///       When user select another country, then get all city of selected country
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
		///       DungLA
		///    </Authors>
		///    <History>
		///    04-Mar-2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private void cboCountry_SelectedIndexChanged(object sender, EventArgs e)
		{
			const string METHOD_NAME = THIS + ".cboCountry_SelectedValueChanged()";
			try
			{
				if(cboCountry.SelectedIndex <=0 ) return;

				int intCountryID = int.Parse(cboCountry.SelectedValue.ToString());
				if (tblCityData.Rows.Count > 0)
				{ 
					// select data for city combo
					DataRow[] drowData = tblCityData.Select(MST_CityTable.COUNTRYID_FLD + Constants.EQUAL + intCountryID.ToString());
					// put data into City combo
					PutDataToCityCombo(drowData);
				}
			}
			catch (PCSException ex)
			{
				// displays the error message.
				PCSMessageBox.Show(ex.mCode, MessageBoxButtons.OK, MessageBoxIcon.Error);
				// log message.
				try
				{
					Logger.LogMessage(ex.CauseException, METHOD_NAME, Level.ERROR);
				}
				catch
				{
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
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
		///       F4 - Open search Location form
		///       F12 - Open Contact form with mode is ADD
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
		///       DungLA
		///    </Authors>
		///    <History>
		///    04-Mar-2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private void Location_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			
		}
		#endregion
		
	}
}
