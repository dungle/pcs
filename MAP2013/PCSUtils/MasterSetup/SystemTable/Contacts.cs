using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
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
	/// Summary description for Contacts.
	/// </summary>
	public class Contacts : Form
	{
		#region Declaration
		
		#region Form's Controls
		private Button btnSearchCode;
		private TextBox txtLocation;
		private Label lblLocation;
		private Label lblContact;
		private TextBox txtName;
		private Label lblName;
		private TextBox txtTitle;
		private Label lblTitle;
		private TextBox txtFax;
		private Label lblFax;
		private TextBox txtPhone;
		private Label lblPhone;
		private TextBox txtMemo;
		private Label lblMemo;
		private TextBox txtEmail;
		private Label lblEmail;
		private TextBox txtExtension;
		private Button btnEdit;
		private Button btnDelete;
		private Button btnSave;
		private Button btnAdd;
		private Button btnClose;
		private Button btnHelp;
		private Label lblExtension;
		private TextBox txtContactCode;
		private Label lblParty;
		private TextBox txtParty;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private Container components = null;
		#endregion Form's Controls
		
		#region Constants

		private const string THIS = "PCSUtils.MasterSetup.SystemTable.Contacts";
		private const string DOT = ".";
		
		#endregion Constants

		private ContactBO boLocationContact = new ContactBO();
		private UtilsBO boUtils = new UtilsBO();
		private MST_PartyVO voParty = new MST_PartyVO();
		private MST_PartyLocationVO voPartyLocation = new MST_PartyLocationVO();
		private MST_PartyContactVO voLocationContact = new MST_PartyContactVO();		
		private EnumAction mFormMode = EnumAction.Default;		
		private bool blnDataIsValid = false;

		#endregion Declaration
		
		#region Properties
		
		public EnumAction FormMode
		{
			get { return mFormMode; }
			set { mFormMode = value; }
		}
		
		#endregion Declaration

		#region Constructor, Destructor
		
		public Contacts()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		public Contacts(MST_PartyVO pvoParty, MST_PartyLocationVO pvoPartyLocation)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
			voParty = pvoParty;
			voPartyLocation = pvoPartyLocation;
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
		
		#endregion Constructor, Destructor

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(Contacts));
			this.txtParty = new System.Windows.Forms.TextBox();
			this.lblParty = new System.Windows.Forms.Label();
			this.txtLocation = new System.Windows.Forms.TextBox();
			this.lblLocation = new System.Windows.Forms.Label();
			this.lblContact = new System.Windows.Forms.Label();
			this.txtName = new System.Windows.Forms.TextBox();
			this.lblName = new System.Windows.Forms.Label();
			this.txtTitle = new System.Windows.Forms.TextBox();
			this.lblTitle = new System.Windows.Forms.Label();
			this.txtFax = new System.Windows.Forms.TextBox();
			this.lblFax = new System.Windows.Forms.Label();
			this.txtPhone = new System.Windows.Forms.TextBox();
			this.lblPhone = new System.Windows.Forms.Label();
			this.txtMemo = new System.Windows.Forms.TextBox();
			this.lblMemo = new System.Windows.Forms.Label();
			this.txtEmail = new System.Windows.Forms.TextBox();
			this.lblEmail = new System.Windows.Forms.Label();
			this.txtExtension = new System.Windows.Forms.TextBox();
			this.lblExtension = new System.Windows.Forms.Label();
			this.btnEdit = new System.Windows.Forms.Button();
			this.btnDelete = new System.Windows.Forms.Button();
			this.btnSave = new System.Windows.Forms.Button();
			this.btnAdd = new System.Windows.Forms.Button();
			this.btnClose = new System.Windows.Forms.Button();
			this.btnHelp = new System.Windows.Forms.Button();
			this.txtContactCode = new System.Windows.Forms.TextBox();
			this.btnSearchCode = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// txtParty
			// 
			this.txtParty.AccessibleDescription = resources.GetString("txtParty.AccessibleDescription");
			this.txtParty.AccessibleName = resources.GetString("txtParty.AccessibleName");
			this.txtParty.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("txtParty.Anchor")));
			this.txtParty.AutoSize = ((bool)(resources.GetObject("txtParty.AutoSize")));
			this.txtParty.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("txtParty.BackgroundImage")));
			this.txtParty.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("txtParty.Dock")));
			this.txtParty.Enabled = ((bool)(resources.GetObject("txtParty.Enabled")));
			this.txtParty.Font = ((System.Drawing.Font)(resources.GetObject("txtParty.Font")));
			this.txtParty.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("txtParty.ImeMode")));
			this.txtParty.Location = ((System.Drawing.Point)(resources.GetObject("txtParty.Location")));
			this.txtParty.MaxLength = ((int)(resources.GetObject("txtParty.MaxLength")));
			this.txtParty.Multiline = ((bool)(resources.GetObject("txtParty.Multiline")));
			this.txtParty.Name = "txtParty";
			this.txtParty.PasswordChar = ((char)(resources.GetObject("txtParty.PasswordChar")));
			this.txtParty.ReadOnly = true;
			this.txtParty.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("txtParty.RightToLeft")));
			this.txtParty.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("txtParty.ScrollBars")));
			this.txtParty.Size = ((System.Drawing.Size)(resources.GetObject("txtParty.Size")));
			this.txtParty.TabIndex = ((int)(resources.GetObject("txtParty.TabIndex")));
			this.txtParty.Text = resources.GetString("txtParty.Text");
			this.txtParty.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("txtParty.TextAlign")));
			this.txtParty.Visible = ((bool)(resources.GetObject("txtParty.Visible")));
			this.txtParty.WordWrap = ((bool)(resources.GetObject("txtParty.WordWrap")));
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
			// txtLocation
			// 
			this.txtLocation.AccessibleDescription = resources.GetString("txtLocation.AccessibleDescription");
			this.txtLocation.AccessibleName = resources.GetString("txtLocation.AccessibleName");
			this.txtLocation.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("txtLocation.Anchor")));
			this.txtLocation.AutoSize = ((bool)(resources.GetObject("txtLocation.AutoSize")));
			this.txtLocation.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("txtLocation.BackgroundImage")));
			this.txtLocation.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("txtLocation.Dock")));
			this.txtLocation.Enabled = ((bool)(resources.GetObject("txtLocation.Enabled")));
			this.txtLocation.Font = ((System.Drawing.Font)(resources.GetObject("txtLocation.Font")));
			this.txtLocation.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("txtLocation.ImeMode")));
			this.txtLocation.Location = ((System.Drawing.Point)(resources.GetObject("txtLocation.Location")));
			this.txtLocation.MaxLength = ((int)(resources.GetObject("txtLocation.MaxLength")));
			this.txtLocation.Multiline = ((bool)(resources.GetObject("txtLocation.Multiline")));
			this.txtLocation.Name = "txtLocation";
			this.txtLocation.PasswordChar = ((char)(resources.GetObject("txtLocation.PasswordChar")));
			this.txtLocation.ReadOnly = true;
			this.txtLocation.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("txtLocation.RightToLeft")));
			this.txtLocation.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("txtLocation.ScrollBars")));
			this.txtLocation.Size = ((System.Drawing.Size)(resources.GetObject("txtLocation.Size")));
			this.txtLocation.TabIndex = ((int)(resources.GetObject("txtLocation.TabIndex")));
			this.txtLocation.Text = resources.GetString("txtLocation.Text");
			this.txtLocation.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("txtLocation.TextAlign")));
			this.txtLocation.Visible = ((bool)(resources.GetObject("txtLocation.Visible")));
			this.txtLocation.WordWrap = ((bool)(resources.GetObject("txtLocation.WordWrap")));
			// 
			// lblLocation
			// 
			this.lblLocation.AccessibleDescription = resources.GetString("lblLocation.AccessibleDescription");
			this.lblLocation.AccessibleName = resources.GetString("lblLocation.AccessibleName");
			this.lblLocation.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblLocation.Anchor")));
			this.lblLocation.AutoSize = ((bool)(resources.GetObject("lblLocation.AutoSize")));
			this.lblLocation.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblLocation.Dock")));
			this.lblLocation.Enabled = ((bool)(resources.GetObject("lblLocation.Enabled")));
			this.lblLocation.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.lblLocation.Font = ((System.Drawing.Font)(resources.GetObject("lblLocation.Font")));
			this.lblLocation.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblLocation.Image = ((System.Drawing.Image)(resources.GetObject("lblLocation.Image")));
			this.lblLocation.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblLocation.ImageAlign")));
			this.lblLocation.ImageIndex = ((int)(resources.GetObject("lblLocation.ImageIndex")));
			this.lblLocation.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblLocation.ImeMode")));
			this.lblLocation.Location = ((System.Drawing.Point)(resources.GetObject("lblLocation.Location")));
			this.lblLocation.Name = "lblLocation";
			this.lblLocation.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblLocation.RightToLeft")));
			this.lblLocation.Size = ((System.Drawing.Size)(resources.GetObject("lblLocation.Size")));
			this.lblLocation.TabIndex = ((int)(resources.GetObject("lblLocation.TabIndex")));
			this.lblLocation.Text = resources.GetString("lblLocation.Text");
			this.lblLocation.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblLocation.TextAlign")));
			this.lblLocation.Visible = ((bool)(resources.GetObject("lblLocation.Visible")));
			// 
			// lblContact
			// 
			this.lblContact.AccessibleDescription = resources.GetString("lblContact.AccessibleDescription");
			this.lblContact.AccessibleName = resources.GetString("lblContact.AccessibleName");
			this.lblContact.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblContact.Anchor")));
			this.lblContact.AutoSize = ((bool)(resources.GetObject("lblContact.AutoSize")));
			this.lblContact.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblContact.Dock")));
			this.lblContact.Enabled = ((bool)(resources.GetObject("lblContact.Enabled")));
			this.lblContact.Font = ((System.Drawing.Font)(resources.GetObject("lblContact.Font")));
			this.lblContact.ForeColor = System.Drawing.Color.Maroon;
			this.lblContact.Image = ((System.Drawing.Image)(resources.GetObject("lblContact.Image")));
			this.lblContact.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblContact.ImageAlign")));
			this.lblContact.ImageIndex = ((int)(resources.GetObject("lblContact.ImageIndex")));
			this.lblContact.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblContact.ImeMode")));
			this.lblContact.Location = ((System.Drawing.Point)(resources.GetObject("lblContact.Location")));
			this.lblContact.Name = "lblContact";
			this.lblContact.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblContact.RightToLeft")));
			this.lblContact.Size = ((System.Drawing.Size)(resources.GetObject("lblContact.Size")));
			this.lblContact.TabIndex = ((int)(resources.GetObject("lblContact.TabIndex")));
			this.lblContact.Text = resources.GetString("lblContact.Text");
			this.lblContact.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblContact.TextAlign")));
			this.lblContact.Visible = ((bool)(resources.GetObject("lblContact.Visible")));
			// 
			// txtName
			// 
			this.txtName.AccessibleDescription = resources.GetString("txtName.AccessibleDescription");
			this.txtName.AccessibleName = resources.GetString("txtName.AccessibleName");
			this.txtName.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("txtName.Anchor")));
			this.txtName.AutoSize = ((bool)(resources.GetObject("txtName.AutoSize")));
			this.txtName.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("txtName.BackgroundImage")));
			this.txtName.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("txtName.Dock")));
			this.txtName.Enabled = ((bool)(resources.GetObject("txtName.Enabled")));
			this.txtName.Font = ((System.Drawing.Font)(resources.GetObject("txtName.Font")));
			this.txtName.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("txtName.ImeMode")));
			this.txtName.Location = ((System.Drawing.Point)(resources.GetObject("txtName.Location")));
			this.txtName.MaxLength = ((int)(resources.GetObject("txtName.MaxLength")));
			this.txtName.Multiline = ((bool)(resources.GetObject("txtName.Multiline")));
			this.txtName.Name = "txtName";
			this.txtName.PasswordChar = ((char)(resources.GetObject("txtName.PasswordChar")));
			this.txtName.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("txtName.RightToLeft")));
			this.txtName.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("txtName.ScrollBars")));
			this.txtName.Size = ((System.Drawing.Size)(resources.GetObject("txtName.Size")));
			this.txtName.TabIndex = ((int)(resources.GetObject("txtName.TabIndex")));
			this.txtName.Text = resources.GetString("txtName.Text");
			this.txtName.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("txtName.TextAlign")));
			this.txtName.Visible = ((bool)(resources.GetObject("txtName.Visible")));
			this.txtName.WordWrap = ((bool)(resources.GetObject("txtName.WordWrap")));
			this.txtName.Leave += new System.EventHandler(this.OnLeaveControl);
			this.txtName.Enter += new System.EventHandler(this.OnEnterControl);
			// 
			// lblName
			// 
			this.lblName.AccessibleDescription = resources.GetString("lblName.AccessibleDescription");
			this.lblName.AccessibleName = resources.GetString("lblName.AccessibleName");
			this.lblName.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblName.Anchor")));
			this.lblName.AutoSize = ((bool)(resources.GetObject("lblName.AutoSize")));
			this.lblName.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblName.Dock")));
			this.lblName.Enabled = ((bool)(resources.GetObject("lblName.Enabled")));
			this.lblName.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.lblName.Font = ((System.Drawing.Font)(resources.GetObject("lblName.Font")));
			this.lblName.ForeColor = System.Drawing.Color.Maroon;
			this.lblName.Image = ((System.Drawing.Image)(resources.GetObject("lblName.Image")));
			this.lblName.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblName.ImageAlign")));
			this.lblName.ImageIndex = ((int)(resources.GetObject("lblName.ImageIndex")));
			this.lblName.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblName.ImeMode")));
			this.lblName.Location = ((System.Drawing.Point)(resources.GetObject("lblName.Location")));
			this.lblName.Name = "lblName";
			this.lblName.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblName.RightToLeft")));
			this.lblName.Size = ((System.Drawing.Size)(resources.GetObject("lblName.Size")));
			this.lblName.TabIndex = ((int)(resources.GetObject("lblName.TabIndex")));
			this.lblName.Text = resources.GetString("lblName.Text");
			this.lblName.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblName.TextAlign")));
			this.lblName.Visible = ((bool)(resources.GetObject("lblName.Visible")));
			// 
			// txtTitle
			// 
			this.txtTitle.AccessibleDescription = resources.GetString("txtTitle.AccessibleDescription");
			this.txtTitle.AccessibleName = resources.GetString("txtTitle.AccessibleName");
			this.txtTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("txtTitle.Anchor")));
			this.txtTitle.AutoSize = ((bool)(resources.GetObject("txtTitle.AutoSize")));
			this.txtTitle.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("txtTitle.BackgroundImage")));
			this.txtTitle.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("txtTitle.Dock")));
			this.txtTitle.Enabled = ((bool)(resources.GetObject("txtTitle.Enabled")));
			this.txtTitle.Font = ((System.Drawing.Font)(resources.GetObject("txtTitle.Font")));
			this.txtTitle.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("txtTitle.ImeMode")));
			this.txtTitle.Location = ((System.Drawing.Point)(resources.GetObject("txtTitle.Location")));
			this.txtTitle.MaxLength = ((int)(resources.GetObject("txtTitle.MaxLength")));
			this.txtTitle.Multiline = ((bool)(resources.GetObject("txtTitle.Multiline")));
			this.txtTitle.Name = "txtTitle";
			this.txtTitle.PasswordChar = ((char)(resources.GetObject("txtTitle.PasswordChar")));
			this.txtTitle.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("txtTitle.RightToLeft")));
			this.txtTitle.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("txtTitle.ScrollBars")));
			this.txtTitle.Size = ((System.Drawing.Size)(resources.GetObject("txtTitle.Size")));
			this.txtTitle.TabIndex = ((int)(resources.GetObject("txtTitle.TabIndex")));
			this.txtTitle.Text = resources.GetString("txtTitle.Text");
			this.txtTitle.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("txtTitle.TextAlign")));
			this.txtTitle.Visible = ((bool)(resources.GetObject("txtTitle.Visible")));
			this.txtTitle.WordWrap = ((bool)(resources.GetObject("txtTitle.WordWrap")));
			this.txtTitle.Leave += new System.EventHandler(this.OnLeaveControl);
			this.txtTitle.Enter += new System.EventHandler(this.OnEnterControl);
			// 
			// lblTitle
			// 
			this.lblTitle.AccessibleDescription = resources.GetString("lblTitle.AccessibleDescription");
			this.lblTitle.AccessibleName = resources.GetString("lblTitle.AccessibleName");
			this.lblTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblTitle.Anchor")));
			this.lblTitle.AutoSize = ((bool)(resources.GetObject("lblTitle.AutoSize")));
			this.lblTitle.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblTitle.Dock")));
			this.lblTitle.Enabled = ((bool)(resources.GetObject("lblTitle.Enabled")));
			this.lblTitle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.lblTitle.Font = ((System.Drawing.Font)(resources.GetObject("lblTitle.Font")));
			this.lblTitle.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblTitle.Image = ((System.Drawing.Image)(resources.GetObject("lblTitle.Image")));
			this.lblTitle.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblTitle.ImageAlign")));
			this.lblTitle.ImageIndex = ((int)(resources.GetObject("lblTitle.ImageIndex")));
			this.lblTitle.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblTitle.ImeMode")));
			this.lblTitle.Location = ((System.Drawing.Point)(resources.GetObject("lblTitle.Location")));
			this.lblTitle.Name = "lblTitle";
			this.lblTitle.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblTitle.RightToLeft")));
			this.lblTitle.Size = ((System.Drawing.Size)(resources.GetObject("lblTitle.Size")));
			this.lblTitle.TabIndex = ((int)(resources.GetObject("lblTitle.TabIndex")));
			this.lblTitle.Text = resources.GetString("lblTitle.Text");
			this.lblTitle.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblTitle.TextAlign")));
			this.lblTitle.Visible = ((bool)(resources.GetObject("lblTitle.Visible")));
			// 
			// txtFax
			// 
			this.txtFax.AccessibleDescription = resources.GetString("txtFax.AccessibleDescription");
			this.txtFax.AccessibleName = resources.GetString("txtFax.AccessibleName");
			this.txtFax.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("txtFax.Anchor")));
			this.txtFax.AutoSize = ((bool)(resources.GetObject("txtFax.AutoSize")));
			this.txtFax.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("txtFax.BackgroundImage")));
			this.txtFax.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("txtFax.Dock")));
			this.txtFax.Enabled = ((bool)(resources.GetObject("txtFax.Enabled")));
			this.txtFax.Font = ((System.Drawing.Font)(resources.GetObject("txtFax.Font")));
			this.txtFax.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("txtFax.ImeMode")));
			this.txtFax.Location = ((System.Drawing.Point)(resources.GetObject("txtFax.Location")));
			this.txtFax.MaxLength = ((int)(resources.GetObject("txtFax.MaxLength")));
			this.txtFax.Multiline = ((bool)(resources.GetObject("txtFax.Multiline")));
			this.txtFax.Name = "txtFax";
			this.txtFax.PasswordChar = ((char)(resources.GetObject("txtFax.PasswordChar")));
			this.txtFax.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("txtFax.RightToLeft")));
			this.txtFax.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("txtFax.ScrollBars")));
			this.txtFax.Size = ((System.Drawing.Size)(resources.GetObject("txtFax.Size")));
			this.txtFax.TabIndex = ((int)(resources.GetObject("txtFax.TabIndex")));
			this.txtFax.Text = resources.GetString("txtFax.Text");
			this.txtFax.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("txtFax.TextAlign")));
			this.txtFax.Visible = ((bool)(resources.GetObject("txtFax.Visible")));
			this.txtFax.WordWrap = ((bool)(resources.GetObject("txtFax.WordWrap")));
			this.txtFax.Leave += new System.EventHandler(this.OnLeaveControl);
			this.txtFax.Enter += new System.EventHandler(this.OnEnterControl);
			// 
			// lblFax
			// 
			this.lblFax.AccessibleDescription = resources.GetString("lblFax.AccessibleDescription");
			this.lblFax.AccessibleName = resources.GetString("lblFax.AccessibleName");
			this.lblFax.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblFax.Anchor")));
			this.lblFax.AutoSize = ((bool)(resources.GetObject("lblFax.AutoSize")));
			this.lblFax.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblFax.Dock")));
			this.lblFax.Enabled = ((bool)(resources.GetObject("lblFax.Enabled")));
			this.lblFax.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.lblFax.Font = ((System.Drawing.Font)(resources.GetObject("lblFax.Font")));
			this.lblFax.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblFax.Image = ((System.Drawing.Image)(resources.GetObject("lblFax.Image")));
			this.lblFax.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblFax.ImageAlign")));
			this.lblFax.ImageIndex = ((int)(resources.GetObject("lblFax.ImageIndex")));
			this.lblFax.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblFax.ImeMode")));
			this.lblFax.Location = ((System.Drawing.Point)(resources.GetObject("lblFax.Location")));
			this.lblFax.Name = "lblFax";
			this.lblFax.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblFax.RightToLeft")));
			this.lblFax.Size = ((System.Drawing.Size)(resources.GetObject("lblFax.Size")));
			this.lblFax.TabIndex = ((int)(resources.GetObject("lblFax.TabIndex")));
			this.lblFax.Text = resources.GetString("lblFax.Text");
			this.lblFax.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblFax.TextAlign")));
			this.lblFax.Visible = ((bool)(resources.GetObject("lblFax.Visible")));
			// 
			// txtPhone
			// 
			this.txtPhone.AccessibleDescription = resources.GetString("txtPhone.AccessibleDescription");
			this.txtPhone.AccessibleName = resources.GetString("txtPhone.AccessibleName");
			this.txtPhone.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("txtPhone.Anchor")));
			this.txtPhone.AutoSize = ((bool)(resources.GetObject("txtPhone.AutoSize")));
			this.txtPhone.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("txtPhone.BackgroundImage")));
			this.txtPhone.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("txtPhone.Dock")));
			this.txtPhone.Enabled = ((bool)(resources.GetObject("txtPhone.Enabled")));
			this.txtPhone.Font = ((System.Drawing.Font)(resources.GetObject("txtPhone.Font")));
			this.txtPhone.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("txtPhone.ImeMode")));
			this.txtPhone.Location = ((System.Drawing.Point)(resources.GetObject("txtPhone.Location")));
			this.txtPhone.MaxLength = ((int)(resources.GetObject("txtPhone.MaxLength")));
			this.txtPhone.Multiline = ((bool)(resources.GetObject("txtPhone.Multiline")));
			this.txtPhone.Name = "txtPhone";
			this.txtPhone.PasswordChar = ((char)(resources.GetObject("txtPhone.PasswordChar")));
			this.txtPhone.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("txtPhone.RightToLeft")));
			this.txtPhone.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("txtPhone.ScrollBars")));
			this.txtPhone.Size = ((System.Drawing.Size)(resources.GetObject("txtPhone.Size")));
			this.txtPhone.TabIndex = ((int)(resources.GetObject("txtPhone.TabIndex")));
			this.txtPhone.Text = resources.GetString("txtPhone.Text");
			this.txtPhone.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("txtPhone.TextAlign")));
			this.txtPhone.Visible = ((bool)(resources.GetObject("txtPhone.Visible")));
			this.txtPhone.WordWrap = ((bool)(resources.GetObject("txtPhone.WordWrap")));
			this.txtPhone.Leave += new System.EventHandler(this.OnLeaveControl);
			this.txtPhone.Enter += new System.EventHandler(this.OnEnterControl);
			// 
			// lblPhone
			// 
			this.lblPhone.AccessibleDescription = resources.GetString("lblPhone.AccessibleDescription");
			this.lblPhone.AccessibleName = resources.GetString("lblPhone.AccessibleName");
			this.lblPhone.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblPhone.Anchor")));
			this.lblPhone.AutoSize = ((bool)(resources.GetObject("lblPhone.AutoSize")));
			this.lblPhone.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblPhone.Dock")));
			this.lblPhone.Enabled = ((bool)(resources.GetObject("lblPhone.Enabled")));
			this.lblPhone.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.lblPhone.Font = ((System.Drawing.Font)(resources.GetObject("lblPhone.Font")));
			this.lblPhone.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblPhone.Image = ((System.Drawing.Image)(resources.GetObject("lblPhone.Image")));
			this.lblPhone.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblPhone.ImageAlign")));
			this.lblPhone.ImageIndex = ((int)(resources.GetObject("lblPhone.ImageIndex")));
			this.lblPhone.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblPhone.ImeMode")));
			this.lblPhone.Location = ((System.Drawing.Point)(resources.GetObject("lblPhone.Location")));
			this.lblPhone.Name = "lblPhone";
			this.lblPhone.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblPhone.RightToLeft")));
			this.lblPhone.Size = ((System.Drawing.Size)(resources.GetObject("lblPhone.Size")));
			this.lblPhone.TabIndex = ((int)(resources.GetObject("lblPhone.TabIndex")));
			this.lblPhone.Text = resources.GetString("lblPhone.Text");
			this.lblPhone.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblPhone.TextAlign")));
			this.lblPhone.Visible = ((bool)(resources.GetObject("lblPhone.Visible")));
			// 
			// txtMemo
			// 
			this.txtMemo.AccessibleDescription = resources.GetString("txtMemo.AccessibleDescription");
			this.txtMemo.AccessibleName = resources.GetString("txtMemo.AccessibleName");
			this.txtMemo.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("txtMemo.Anchor")));
			this.txtMemo.AutoSize = ((bool)(resources.GetObject("txtMemo.AutoSize")));
			this.txtMemo.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("txtMemo.BackgroundImage")));
			this.txtMemo.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("txtMemo.Dock")));
			this.txtMemo.Enabled = ((bool)(resources.GetObject("txtMemo.Enabled")));
			this.txtMemo.Font = ((System.Drawing.Font)(resources.GetObject("txtMemo.Font")));
			this.txtMemo.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("txtMemo.ImeMode")));
			this.txtMemo.Location = ((System.Drawing.Point)(resources.GetObject("txtMemo.Location")));
			this.txtMemo.MaxLength = ((int)(resources.GetObject("txtMemo.MaxLength")));
			this.txtMemo.Multiline = ((bool)(resources.GetObject("txtMemo.Multiline")));
			this.txtMemo.Name = "txtMemo";
			this.txtMemo.PasswordChar = ((char)(resources.GetObject("txtMemo.PasswordChar")));
			this.txtMemo.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("txtMemo.RightToLeft")));
			this.txtMemo.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("txtMemo.ScrollBars")));
			this.txtMemo.Size = ((System.Drawing.Size)(resources.GetObject("txtMemo.Size")));
			this.txtMemo.TabIndex = ((int)(resources.GetObject("txtMemo.TabIndex")));
			this.txtMemo.Text = resources.GetString("txtMemo.Text");
			this.txtMemo.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("txtMemo.TextAlign")));
			this.txtMemo.Visible = ((bool)(resources.GetObject("txtMemo.Visible")));
			this.txtMemo.WordWrap = ((bool)(resources.GetObject("txtMemo.WordWrap")));
			this.txtMemo.Leave += new System.EventHandler(this.OnLeaveControl);
			this.txtMemo.Enter += new System.EventHandler(this.OnEnterControl);
			// 
			// lblMemo
			// 
			this.lblMemo.AccessibleDescription = resources.GetString("lblMemo.AccessibleDescription");
			this.lblMemo.AccessibleName = resources.GetString("lblMemo.AccessibleName");
			this.lblMemo.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblMemo.Anchor")));
			this.lblMemo.AutoSize = ((bool)(resources.GetObject("lblMemo.AutoSize")));
			this.lblMemo.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblMemo.Dock")));
			this.lblMemo.Enabled = ((bool)(resources.GetObject("lblMemo.Enabled")));
			this.lblMemo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.lblMemo.Font = ((System.Drawing.Font)(resources.GetObject("lblMemo.Font")));
			this.lblMemo.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblMemo.Image = ((System.Drawing.Image)(resources.GetObject("lblMemo.Image")));
			this.lblMemo.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblMemo.ImageAlign")));
			this.lblMemo.ImageIndex = ((int)(resources.GetObject("lblMemo.ImageIndex")));
			this.lblMemo.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblMemo.ImeMode")));
			this.lblMemo.Location = ((System.Drawing.Point)(resources.GetObject("lblMemo.Location")));
			this.lblMemo.Name = "lblMemo";
			this.lblMemo.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblMemo.RightToLeft")));
			this.lblMemo.Size = ((System.Drawing.Size)(resources.GetObject("lblMemo.Size")));
			this.lblMemo.TabIndex = ((int)(resources.GetObject("lblMemo.TabIndex")));
			this.lblMemo.Text = resources.GetString("lblMemo.Text");
			this.lblMemo.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblMemo.TextAlign")));
			this.lblMemo.Visible = ((bool)(resources.GetObject("lblMemo.Visible")));
			// 
			// txtEmail
			// 
			this.txtEmail.AccessibleDescription = resources.GetString("txtEmail.AccessibleDescription");
			this.txtEmail.AccessibleName = resources.GetString("txtEmail.AccessibleName");
			this.txtEmail.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("txtEmail.Anchor")));
			this.txtEmail.AutoSize = ((bool)(resources.GetObject("txtEmail.AutoSize")));
			this.txtEmail.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("txtEmail.BackgroundImage")));
			this.txtEmail.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("txtEmail.Dock")));
			this.txtEmail.Enabled = ((bool)(resources.GetObject("txtEmail.Enabled")));
			this.txtEmail.Font = ((System.Drawing.Font)(resources.GetObject("txtEmail.Font")));
			this.txtEmail.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("txtEmail.ImeMode")));
			this.txtEmail.Location = ((System.Drawing.Point)(resources.GetObject("txtEmail.Location")));
			this.txtEmail.MaxLength = ((int)(resources.GetObject("txtEmail.MaxLength")));
			this.txtEmail.Multiline = ((bool)(resources.GetObject("txtEmail.Multiline")));
			this.txtEmail.Name = "txtEmail";
			this.txtEmail.PasswordChar = ((char)(resources.GetObject("txtEmail.PasswordChar")));
			this.txtEmail.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("txtEmail.RightToLeft")));
			this.txtEmail.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("txtEmail.ScrollBars")));
			this.txtEmail.Size = ((System.Drawing.Size)(resources.GetObject("txtEmail.Size")));
			this.txtEmail.TabIndex = ((int)(resources.GetObject("txtEmail.TabIndex")));
			this.txtEmail.Text = resources.GetString("txtEmail.Text");
			this.txtEmail.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("txtEmail.TextAlign")));
			this.txtEmail.Visible = ((bool)(resources.GetObject("txtEmail.Visible")));
			this.txtEmail.WordWrap = ((bool)(resources.GetObject("txtEmail.WordWrap")));
			this.txtEmail.Leave += new System.EventHandler(this.OnLeaveControl);
			this.txtEmail.Enter += new System.EventHandler(this.OnEnterControl);
			// 
			// lblEmail
			// 
			this.lblEmail.AccessibleDescription = resources.GetString("lblEmail.AccessibleDescription");
			this.lblEmail.AccessibleName = resources.GetString("lblEmail.AccessibleName");
			this.lblEmail.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblEmail.Anchor")));
			this.lblEmail.AutoSize = ((bool)(resources.GetObject("lblEmail.AutoSize")));
			this.lblEmail.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblEmail.Dock")));
			this.lblEmail.Enabled = ((bool)(resources.GetObject("lblEmail.Enabled")));
			this.lblEmail.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.lblEmail.Font = ((System.Drawing.Font)(resources.GetObject("lblEmail.Font")));
			this.lblEmail.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblEmail.Image = ((System.Drawing.Image)(resources.GetObject("lblEmail.Image")));
			this.lblEmail.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblEmail.ImageAlign")));
			this.lblEmail.ImageIndex = ((int)(resources.GetObject("lblEmail.ImageIndex")));
			this.lblEmail.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblEmail.ImeMode")));
			this.lblEmail.Location = ((System.Drawing.Point)(resources.GetObject("lblEmail.Location")));
			this.lblEmail.Name = "lblEmail";
			this.lblEmail.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblEmail.RightToLeft")));
			this.lblEmail.Size = ((System.Drawing.Size)(resources.GetObject("lblEmail.Size")));
			this.lblEmail.TabIndex = ((int)(resources.GetObject("lblEmail.TabIndex")));
			this.lblEmail.Text = resources.GetString("lblEmail.Text");
			this.lblEmail.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblEmail.TextAlign")));
			this.lblEmail.Visible = ((bool)(resources.GetObject("lblEmail.Visible")));
			// 
			// txtExtension
			// 
			this.txtExtension.AccessibleDescription = resources.GetString("txtExtension.AccessibleDescription");
			this.txtExtension.AccessibleName = resources.GetString("txtExtension.AccessibleName");
			this.txtExtension.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("txtExtension.Anchor")));
			this.txtExtension.AutoSize = ((bool)(resources.GetObject("txtExtension.AutoSize")));
			this.txtExtension.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("txtExtension.BackgroundImage")));
			this.txtExtension.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("txtExtension.Dock")));
			this.txtExtension.Enabled = ((bool)(resources.GetObject("txtExtension.Enabled")));
			this.txtExtension.Font = ((System.Drawing.Font)(resources.GetObject("txtExtension.Font")));
			this.txtExtension.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("txtExtension.ImeMode")));
			this.txtExtension.Location = ((System.Drawing.Point)(resources.GetObject("txtExtension.Location")));
			this.txtExtension.MaxLength = ((int)(resources.GetObject("txtExtension.MaxLength")));
			this.txtExtension.Multiline = ((bool)(resources.GetObject("txtExtension.Multiline")));
			this.txtExtension.Name = "txtExtension";
			this.txtExtension.PasswordChar = ((char)(resources.GetObject("txtExtension.PasswordChar")));
			this.txtExtension.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("txtExtension.RightToLeft")));
			this.txtExtension.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("txtExtension.ScrollBars")));
			this.txtExtension.Size = ((System.Drawing.Size)(resources.GetObject("txtExtension.Size")));
			this.txtExtension.TabIndex = ((int)(resources.GetObject("txtExtension.TabIndex")));
			this.txtExtension.Text = resources.GetString("txtExtension.Text");
			this.txtExtension.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("txtExtension.TextAlign")));
			this.txtExtension.Visible = ((bool)(resources.GetObject("txtExtension.Visible")));
			this.txtExtension.WordWrap = ((bool)(resources.GetObject("txtExtension.WordWrap")));
			this.txtExtension.Leave += new System.EventHandler(this.OnLeaveControl);
			this.txtExtension.Enter += new System.EventHandler(this.OnEnterControl);
			// 
			// lblExtension
			// 
			this.lblExtension.AccessibleDescription = resources.GetString("lblExtension.AccessibleDescription");
			this.lblExtension.AccessibleName = resources.GetString("lblExtension.AccessibleName");
			this.lblExtension.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblExtension.Anchor")));
			this.lblExtension.AutoSize = ((bool)(resources.GetObject("lblExtension.AutoSize")));
			this.lblExtension.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblExtension.Dock")));
			this.lblExtension.Enabled = ((bool)(resources.GetObject("lblExtension.Enabled")));
			this.lblExtension.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.lblExtension.Font = ((System.Drawing.Font)(resources.GetObject("lblExtension.Font")));
			this.lblExtension.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblExtension.Image = ((System.Drawing.Image)(resources.GetObject("lblExtension.Image")));
			this.lblExtension.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblExtension.ImageAlign")));
			this.lblExtension.ImageIndex = ((int)(resources.GetObject("lblExtension.ImageIndex")));
			this.lblExtension.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblExtension.ImeMode")));
			this.lblExtension.Location = ((System.Drawing.Point)(resources.GetObject("lblExtension.Location")));
			this.lblExtension.Name = "lblExtension";
			this.lblExtension.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblExtension.RightToLeft")));
			this.lblExtension.Size = ((System.Drawing.Size)(resources.GetObject("lblExtension.Size")));
			this.lblExtension.TabIndex = ((int)(resources.GetObject("lblExtension.TabIndex")));
			this.lblExtension.Text = resources.GetString("lblExtension.Text");
			this.lblExtension.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblExtension.TextAlign")));
			this.lblExtension.Visible = ((bool)(resources.GetObject("lblExtension.Visible")));
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
			// txtContactCode
			// 
			this.txtContactCode.AccessibleDescription = resources.GetString("txtContactCode.AccessibleDescription");
			this.txtContactCode.AccessibleName = resources.GetString("txtContactCode.AccessibleName");
			this.txtContactCode.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("txtContactCode.Anchor")));
			this.txtContactCode.AutoSize = ((bool)(resources.GetObject("txtContactCode.AutoSize")));
			this.txtContactCode.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("txtContactCode.BackgroundImage")));
			this.txtContactCode.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("txtContactCode.Dock")));
			this.txtContactCode.Enabled = ((bool)(resources.GetObject("txtContactCode.Enabled")));
			this.txtContactCode.Font = ((System.Drawing.Font)(resources.GetObject("txtContactCode.Font")));
			this.txtContactCode.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("txtContactCode.ImeMode")));
			this.txtContactCode.Location = ((System.Drawing.Point)(resources.GetObject("txtContactCode.Location")));
			this.txtContactCode.MaxLength = ((int)(resources.GetObject("txtContactCode.MaxLength")));
			this.txtContactCode.Multiline = ((bool)(resources.GetObject("txtContactCode.Multiline")));
			this.txtContactCode.Name = "txtContactCode";
			this.txtContactCode.PasswordChar = ((char)(resources.GetObject("txtContactCode.PasswordChar")));
			this.txtContactCode.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("txtContactCode.RightToLeft")));
			this.txtContactCode.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("txtContactCode.ScrollBars")));
			this.txtContactCode.Size = ((System.Drawing.Size)(resources.GetObject("txtContactCode.Size")));
			this.txtContactCode.TabIndex = ((int)(resources.GetObject("txtContactCode.TabIndex")));
			this.txtContactCode.Text = resources.GetString("txtContactCode.Text");
			this.txtContactCode.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("txtContactCode.TextAlign")));
			this.txtContactCode.Visible = ((bool)(resources.GetObject("txtContactCode.Visible")));
			this.txtContactCode.WordWrap = ((bool)(resources.GetObject("txtContactCode.WordWrap")));
			this.txtContactCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtContactCode_KeyDown);
			this.txtContactCode.Validating += new CancelEventHandler(txtContactCode_Validating);
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
			// Contacts
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
			this.Controls.Add(this.btnSearchCode);
			this.Controls.Add(this.txtContactCode);
			this.Controls.Add(this.txtExtension);
			this.Controls.Add(this.txtMemo);
			this.Controls.Add(this.txtEmail);
			this.Controls.Add(this.txtFax);
			this.Controls.Add(this.txtPhone);
			this.Controls.Add(this.txtTitle);
			this.Controls.Add(this.txtName);
			this.Controls.Add(this.txtLocation);
			this.Controls.Add(this.txtParty);
			this.Controls.Add(this.btnClose);
			this.Controls.Add(this.btnHelp);
			this.Controls.Add(this.btnEdit);
			this.Controls.Add(this.btnDelete);
			this.Controls.Add(this.btnSave);
			this.Controls.Add(this.btnAdd);
			this.Controls.Add(this.lblExtension);
			this.Controls.Add(this.lblMemo);
			this.Controls.Add(this.lblEmail);
			this.Controls.Add(this.lblFax);
			this.Controls.Add(this.lblPhone);
			this.Controls.Add(this.lblTitle);
			this.Controls.Add(this.lblName);
			this.Controls.Add(this.lblContact);
			this.Controls.Add(this.lblLocation);
			this.Controls.Add(this.lblParty);
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
			this.Name = "Contacts";
			this.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("$this.RightToLeft")));
			this.StartPosition = ((System.Windows.Forms.FormStartPosition)(resources.GetObject("$this.StartPosition")));
			this.Text = resources.GetString("$this.Text");
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Contacts_KeyDown);
			this.Closing += new System.ComponentModel.CancelEventHandler(this.Contacts_Closing);
			this.Load += new System.EventHandler(this.Contacts_Load);
			this.ResumeLayout(false);

		}
		#endregion		

		#region Private Methods
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
							if ((objControl is TextBox) || (objControl is ComboBox))
							{
								if (objControl != txtContactCode)
									objControl.Enabled = false;
							}
						}
						btnAdd.Enabled = true;
						btnSave.Enabled = false;
						if (voLocationContact.PartyContactID > 0)
						{
							btnEdit.Enabled = true;
							btnDelete.Enabled = true;
						}
						else
						{
							btnEdit.Enabled = false;
							btnDelete.Enabled = false;
						}
						btnSearchCode.Enabled = true;
						
						break;
						
					default:
						foreach (Control objControl in this.Controls)
						{
							if ((objControl is TextBox) || (objControl is ComboBox))
								objControl.Enabled = true;
						}
						txtLocation.Enabled = false;
						txtParty.Enabled = false;
						txtContactCode.Enabled = true  && (mFormMode != EnumAction.Default);
						btnSearchCode.Enabled = false;
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
		private void LoadDataToControl(MST_PartyContactVO pvoPartyContact)
		{
			try
			{
				if ((pvoPartyContact != null) && (pvoPartyContact.PartyContactID > 0))
				{ 
					// load all data from VO to controls
					txtContactCode.Text = pvoPartyContact.Code;
					txtName.Text = pvoPartyContact.Name;
					txtTitle.Text = pvoPartyContact.Title;
					txtPhone.Text = pvoPartyContact.Phone;
					txtExtension.Text = pvoPartyContact.Ext;
					txtFax.Text = pvoPartyContact.Fax;
					txtEmail.Text = pvoPartyContact.Email;
					txtMemo.Text = pvoPartyContact.Memo;
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

				txtParty.Text = voParty.Code;
				txtLocation.Text = voPartyLocation.Code;
				txtParty.Tag = voParty;
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
				voLocationContact.PartyID = voParty.PartyID;
				voLocationContact.PartyLocationID = voPartyLocation.PartyLocationID;
				voLocationContact.Code = txtContactCode.Text.Trim();
				voLocationContact.Name = txtName.Text.Trim();
				voLocationContact.Title = txtTitle.Text.Trim();
				voLocationContact.Phone = txtPhone.Text.Trim();
				voLocationContact.Ext = txtExtension.Text.Trim();
				voLocationContact.Fax = txtFax.Text.Trim();
				voLocationContact.Email = txtEmail.Text.Trim();
				voLocationContact.Memo = txtMemo.Text.Trim();
				switch (mFormMode)
				{
					case EnumAction.Add:
						voLocationContact.PartyContactID = boLocationContact.AddReturnID(voLocationContact);
						break;
					case EnumAction.Edit:
						boLocationContact.Update(voLocationContact);
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
				voLocationContact = (MST_PartyContactVO)boLocationContact.GetObjectVO(int.Parse(pdrowData[MST_PartyContactTable.PARTYCONTACTID_FLD].ToString()), string.Empty);
				
				// load data to form
				LoadDataToControl(voLocationContact);
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

		/// <summary>
		/// Call this method to PartyLocation open search form 
		/// </summary>
		/// <param name="pstrMethodName"></param>
		/// Method that called this method
		/// <param name="pblnAlwaysShowDialog"></param>
		/// 1: always show open search form
		/// 0: other else
		private bool SelectContactCode(string pstrMethodName, bool pblnAlwaysShowDialog)
		{
			try				
			{	
				Hashtable htCondition = new Hashtable();
				htCondition.Add(MST_PartyContactTable.PARTYID_FLD, voPartyLocation.PartyID);
				htCondition.Add(MST_PartyContactTable.PARTYLOCATIONID_FLD, voPartyLocation.PartyLocationID);				
				
				//Call OpenSearchForm for selecting	
				DataRowView drwResult = FormControlComponents.OpenSearchForm(MST_PartyContactTable.TABLE_NAME, MST_PartyContactTable.CODE_FLD , txtContactCode.Text, htCondition, pblnAlwaysShowDialog);

				// If has TransNo matched searching condition, fill values to form's controls
				if(drwResult != null)
				{
					FillData(drwResult.Row);
					//Reset modify status
					txtContactCode.Modified = false;
				}
				else if(!pblnAlwaysShowDialog)
				{
					txtContactCode.Focus();
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

		#endregion

		#region Event Processing

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
		private void Contacts_Load(object sender, EventArgs e)
		{
			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.WaitCursor;

			#endregion Code Inserted Automatically

			
			const string METHOD_NAME = THIS + ".Contacts_Load()";
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

				// switch form mode based on FormAction
				SwitchFormMode();
				if (mFormMode == EnumAction.Edit)
				{
					LoadDataToControl(voLocationContact);
				}
				else
				{
					LoadDataToControl(null);
				}
				txtContactCode.Enabled = true;
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
				SelectContactCode(METHOD_NAME, true);
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
				txtContactCode.Text = boUtils.GetNoByMask(MST_PartyContactTable.TABLE_NAME, MST_PartyContactTable.CODE_FLD, boUtils.GetDBDate(), string.Empty);
				txtContactCode.Focus();
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
				if (FormControlComponents.CheckMandatory(txtContactCode))
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxButtons.OK, MessageBoxIcon.Error);
					txtContactCode.Focus();
					txtContactCode.SelectAll();
					blnDataIsValid = false;
					// Code Inserted Automatically
#region Code Inserted Automatically
this.Cursor = Cursors.Default;
#endregion Code Inserted Automatically

					return;
				}

				if (FormControlComponents.CheckMandatory(txtName))
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxButtons.OK, MessageBoxIcon.Error);
					txtName.Focus();
					txtName.SelectAll();
					blnDataIsValid = false;
					// Code Inserted Automatically
#region Code Inserted Automatically
this.Cursor = Cursors.Default;
#endregion Code Inserted Automatically

					return;
				}

				if (txtPhone.Text.Trim() != string.Empty)
				{
					if (!FormControlComponents.IsPhoneFaxNumber(txtPhone.Text.Trim()))
					{
						PCSMessageBox.Show(ErrorCode.MESSAGE_INVALID_PHONE_NUMBER, MessageBoxButtons.OK, MessageBoxIcon.Error);
						txtPhone.Focus();
						txtPhone.SelectAll();
						blnDataIsValid = false;
						// Code Inserted Automatically
#region Code Inserted Automatically
this.Cursor = Cursors.Default;
#endregion Code Inserted Automatically

						return;
					}
				}

				if (txtExtension.Text.Trim() != string.Empty)
				{
					if (!FormControlComponents.IsPhoneFaxNumber(txtExtension.Text.Trim()))
					{
						PCSMessageBox.Show(ErrorCode.MESSAGE_INVALID_PHONE_NUMBER, MessageBoxButtons.OK, MessageBoxIcon.Error);
						txtExtension.Focus();
						txtExtension.SelectAll();
						blnDataIsValid = false;
						// Code Inserted Automatically
#region Code Inserted Automatically
this.Cursor = Cursors.Default;
#endregion Code Inserted Automatically

						return;
					}
				}

				if (txtFax.Text.Trim() != string.Empty)
				{
					if (!FormControlComponents.IsPhoneFaxNumber(txtExtension.Text.Trim()))
					{
						PCSMessageBox.Show(ErrorCode.MESSAGE_INVALID_PHONE_NUMBER, MessageBoxButtons.OK, MessageBoxIcon.Error);
						txtFax.Focus();
						txtFax.SelectAll();
						blnDataIsValid = false;
						// Code Inserted Automatically
#region Code Inserted Automatically
this.Cursor = Cursors.Default;
#endregion Code Inserted Automatically

						return;
					}
				}

				if (txtEmail.Text.Trim() != string.Empty)
				{
					if (!FormControlComponents.IsValidEmail(txtEmail.Text.Trim()))
					{
						PCSMessageBox.Show(ErrorCode.MESSAGE_INVALID_EMAIL, MessageBoxButtons.OK, MessageBoxIcon.Error);
						txtEmail.Focus();
						txtEmail.SelectAll();
						blnDataIsValid = false;
						// Code Inserted Automatically
#region Code Inserted Automatically
this.Cursor = Cursors.Default;
#endregion Code Inserted Automatically

						return;
					}
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
					txtContactCode.Focus();
					txtContactCode.SelectAll();
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
				txtContactCode.Focus();
				txtContactCode.SelectAll();
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
				txtContactCode.Focus();
				txtContactCode.SelectAll();
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
				if(voLocationContact.PartyContactID <= 0) 
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
				txtContactCode.Focus();
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
				if(voLocationContact.PartyContactID <= 0) 
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
					boLocationContact.Delete(voLocationContact);
					// clear data of vo object
					voLocationContact = new MST_PartyContactVO();
					LoadDataToControl(voLocationContact);
					// set form mode to DEFAULT
					mFormMode = EnumAction.Default;
					// switching form mode
					SwitchFormMode();
				}

				txtContactCode.Focus();
			}
			catch(PCSException ex)
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
		private void Contacts_Closing(object sender, CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".Contacts_Closing()";
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
		
		private void txtContactCode_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtContactCode_KeyDown()";
			try
			{
				if (e.KeyCode == Keys.F4 && btnSearchCode.Enabled)
				{
					SelectContactCode(METHOD_NAME, true);
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
		
		private void txtContactCode_Validating(object sender, CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtContactCode_Validating()";
			
			try
			{
				//Exit immediately if empty
				if(mFormMode != EnumAction.Default) return;
				if(txtContactCode.Text.Length == 0)
				{
					voLocationContact = new MST_PartyContactVO();
					voLocationContact.PartyContactID = 0;
					LoadDataToControl(voLocationContact);
					SwitchFormMode();
					return;
				}
				else if(!txtContactCode.Modified)
				{
					return;
				}

				e.Cancel = !SelectContactCode(METHOD_NAME, false);
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

		
		private void Contacts_KeyDown(object sender, KeyEventArgs e)
		{
			const string METHOD_NAME = THIS + ".Contacts_KeyDown()";
						
			try
			{					
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
	}
}
