using System;
using System.Data;
using System.Windows.Forms;
using C1.Win.C1TrueDBGrid;

//PCS namespaces
using PCSComUtils.Admin.BO;
using PCSComUtils.Common;
using PCSComUtils.PCSExc;
using PCSUtils.Log;
using PCSUtils.Utils;

namespace PCSUtils.Admin
{
	/// <summary>
	/// Summary description for GrantRoleUser.
	/// </summary>
	public class GrantRoleUser : System.Windows.Forms.Form
	{
		#region Declaration

		#region System Generated
		
		private System.Windows.Forms.Label label1;
		private System.ComponentModel.IContainer components;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private C1.Win.C1TrueDBGrid.C1TrueDBGrid tgridViewData;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnRemoveAllRoles;
		private System.Windows.Forms.Button btnRemoveRole;
		private System.Windows.Forms.Button btnaddAllRoles;
		private System.Windows.Forms.Button btnAddRole;
		private System.Windows.Forms.ListBox lstUserRole;
		private System.Windows.Forms.ListBox lstAllRolesNoInUser;
		private System.Windows.Forms.Button btnHelp;
		private System.Windows.Forms.ToolTip toolTip1;		
		private System.Windows.Forms.Label lblLoginCaption;
		private System.Windows.Forms.Label lblFullNameCaption;
		private System.Windows.Forms.Label lblDescriptionCaption;
		private System.Windows.Forms.Button btnOK;
		
		#endregion System Generated

		private const string THIS = "PCSUtils.Admin.GrantRoleUser";
		private bool blnEditUpdateDate;
		
		#endregion Declaration

		#region Constructor, Destructor
		public GrantRoleUser()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();		
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		#endregion Constructor, Destructor

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(GrantRoleUser));
			this.label1 = new System.Windows.Forms.Label();
			this.btnCancel = new System.Windows.Forms.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.lstUserRole = new System.Windows.Forms.ListBox();
			this.lstAllRolesNoInUser = new System.Windows.Forms.ListBox();
			this.btnRemoveAllRoles = new System.Windows.Forms.Button();
			this.btnRemoveRole = new System.Windows.Forms.Button();
			this.btnaddAllRoles = new System.Windows.Forms.Button();
			this.btnAddRole = new System.Windows.Forms.Button();
			this.tgridViewData = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
			this.btnHelp = new System.Windows.Forms.Button();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.lblLoginCaption = new System.Windows.Forms.Label();
			this.lblFullNameCaption = new System.Windows.Forms.Label();
			this.lblDescriptionCaption = new System.Windows.Forms.Label();
			this.btnOK = new System.Windows.Forms.Button();
			this.groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.tgridViewData)).BeginInit();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AccessibleDescription = resources.GetString("label1.AccessibleDescription");
			this.label1.AccessibleName = resources.GetString("label1.AccessibleName");
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("label1.Anchor")));
			this.label1.AutoSize = ((bool)(resources.GetObject("label1.AutoSize")));
			this.label1.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("label1.Dock")));
			this.label1.Enabled = ((bool)(resources.GetObject("label1.Enabled")));
			this.label1.Font = ((System.Drawing.Font)(resources.GetObject("label1.Font")));
			this.label1.Image = ((System.Drawing.Image)(resources.GetObject("label1.Image")));
			this.label1.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label1.ImageAlign")));
			this.label1.ImageIndex = ((int)(resources.GetObject("label1.ImageIndex")));
			this.label1.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("label1.ImeMode")));
			this.label1.Location = ((System.Drawing.Point)(resources.GetObject("label1.Location")));
			this.label1.Name = "label1";
			this.label1.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("label1.RightToLeft")));
			this.label1.Size = ((System.Drawing.Size)(resources.GetObject("label1.Size")));
			this.label1.TabIndex = ((int)(resources.GetObject("label1.TabIndex")));
			this.label1.Text = resources.GetString("label1.Text");
			this.label1.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label1.TextAlign")));
			this.toolTip1.SetToolTip(this.label1, resources.GetString("label1.ToolTip"));
			this.label1.Visible = ((bool)(resources.GetObject("label1.Visible")));
			this.label1.Click += new System.EventHandler(this.label1_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.AccessibleDescription = resources.GetString("btnCancel.AccessibleDescription");
			this.btnCancel.AccessibleName = resources.GetString("btnCancel.AccessibleName");
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnCancel.Anchor")));
			this.btnCancel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCancel.BackgroundImage")));
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnCancel.Dock")));
			this.btnCancel.Enabled = ((bool)(resources.GetObject("btnCancel.Enabled")));
			this.btnCancel.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnCancel.FlatStyle")));
			this.btnCancel.Font = ((System.Drawing.Font)(resources.GetObject("btnCancel.Font")));
			this.btnCancel.Image = ((System.Drawing.Image)(resources.GetObject("btnCancel.Image")));
			this.btnCancel.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnCancel.ImageAlign")));
			this.btnCancel.ImageIndex = ((int)(resources.GetObject("btnCancel.ImageIndex")));
			this.btnCancel.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnCancel.ImeMode")));
			this.btnCancel.Location = ((System.Drawing.Point)(resources.GetObject("btnCancel.Location")));
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnCancel.RightToLeft")));
			this.btnCancel.Size = ((System.Drawing.Size)(resources.GetObject("btnCancel.Size")));
			this.btnCancel.TabIndex = ((int)(resources.GetObject("btnCancel.TabIndex")));
			this.btnCancel.Text = resources.GetString("btnCancel.Text");
			this.btnCancel.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnCancel.TextAlign")));
			this.toolTip1.SetToolTip(this.btnCancel, resources.GetString("btnCancel.ToolTip"));
			this.btnCancel.Visible = ((bool)(resources.GetObject("btnCancel.Visible")));
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.AccessibleDescription = resources.GetString("groupBox1.AccessibleDescription");
			this.groupBox1.AccessibleName = resources.GetString("groupBox1.AccessibleName");
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("groupBox1.Anchor")));
			this.groupBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("groupBox1.BackgroundImage")));
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.lstUserRole);
			this.groupBox1.Controls.Add(this.lstAllRolesNoInUser);
			this.groupBox1.Controls.Add(this.btnRemoveAllRoles);
			this.groupBox1.Controls.Add(this.btnRemoveRole);
			this.groupBox1.Controls.Add(this.btnaddAllRoles);
			this.groupBox1.Controls.Add(this.btnAddRole);
			this.groupBox1.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("groupBox1.Dock")));
			this.groupBox1.Enabled = ((bool)(resources.GetObject("groupBox1.Enabled")));
			this.groupBox1.Font = ((System.Drawing.Font)(resources.GetObject("groupBox1.Font")));
			this.groupBox1.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("groupBox1.ImeMode")));
			this.groupBox1.Location = ((System.Drawing.Point)(resources.GetObject("groupBox1.Location")));
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("groupBox1.RightToLeft")));
			this.groupBox1.Size = ((System.Drawing.Size)(resources.GetObject("groupBox1.Size")));
			this.groupBox1.TabIndex = ((int)(resources.GetObject("groupBox1.TabIndex")));
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = resources.GetString("groupBox1.Text");
			this.toolTip1.SetToolTip(this.groupBox1, resources.GetString("groupBox1.ToolTip"));
			this.groupBox1.Visible = ((bool)(resources.GetObject("groupBox1.Visible")));
			this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
			// 
			// label3
			// 
			this.label3.AccessibleDescription = resources.GetString("label3.AccessibleDescription");
			this.label3.AccessibleName = resources.GetString("label3.AccessibleName");
			this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("label3.Anchor")));
			this.label3.AutoSize = ((bool)(resources.GetObject("label3.AutoSize")));
			this.label3.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("label3.Dock")));
			this.label3.Enabled = ((bool)(resources.GetObject("label3.Enabled")));
			this.label3.Font = ((System.Drawing.Font)(resources.GetObject("label3.Font")));
			this.label3.Image = ((System.Drawing.Image)(resources.GetObject("label3.Image")));
			this.label3.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label3.ImageAlign")));
			this.label3.ImageIndex = ((int)(resources.GetObject("label3.ImageIndex")));
			this.label3.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("label3.ImeMode")));
			this.label3.Location = ((System.Drawing.Point)(resources.GetObject("label3.Location")));
			this.label3.Name = "label3";
			this.label3.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("label3.RightToLeft")));
			this.label3.Size = ((System.Drawing.Size)(resources.GetObject("label3.Size")));
			this.label3.TabIndex = ((int)(resources.GetObject("label3.TabIndex")));
			this.label3.Text = resources.GetString("label3.Text");
			this.label3.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label3.TextAlign")));
			this.toolTip1.SetToolTip(this.label3, resources.GetString("label3.ToolTip"));
			this.label3.Visible = ((bool)(resources.GetObject("label3.Visible")));
			// 
			// label2
			// 
			this.label2.AccessibleDescription = resources.GetString("label2.AccessibleDescription");
			this.label2.AccessibleName = resources.GetString("label2.AccessibleName");
			this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("label2.Anchor")));
			this.label2.AutoSize = ((bool)(resources.GetObject("label2.AutoSize")));
			this.label2.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("label2.Dock")));
			this.label2.Enabled = ((bool)(resources.GetObject("label2.Enabled")));
			this.label2.Font = ((System.Drawing.Font)(resources.GetObject("label2.Font")));
			this.label2.Image = ((System.Drawing.Image)(resources.GetObject("label2.Image")));
			this.label2.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label2.ImageAlign")));
			this.label2.ImageIndex = ((int)(resources.GetObject("label2.ImageIndex")));
			this.label2.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("label2.ImeMode")));
			this.label2.Location = ((System.Drawing.Point)(resources.GetObject("label2.Location")));
			this.label2.Name = "label2";
			this.label2.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("label2.RightToLeft")));
			this.label2.Size = ((System.Drawing.Size)(resources.GetObject("label2.Size")));
			this.label2.TabIndex = ((int)(resources.GetObject("label2.TabIndex")));
			this.label2.Text = resources.GetString("label2.Text");
			this.label2.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label2.TextAlign")));
			this.toolTip1.SetToolTip(this.label2, resources.GetString("label2.ToolTip"));
			this.label2.Visible = ((bool)(resources.GetObject("label2.Visible")));
			// 
			// lstUserRole
			// 
			this.lstUserRole.AccessibleDescription = resources.GetString("lstUserRole.AccessibleDescription");
			this.lstUserRole.AccessibleName = resources.GetString("lstUserRole.AccessibleName");
			this.lstUserRole.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lstUserRole.Anchor")));
			this.lstUserRole.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("lstUserRole.BackgroundImage")));
			this.lstUserRole.ColumnWidth = ((int)(resources.GetObject("lstUserRole.ColumnWidth")));
			this.lstUserRole.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lstUserRole.Dock")));
			this.lstUserRole.Enabled = ((bool)(resources.GetObject("lstUserRole.Enabled")));
			this.lstUserRole.Font = ((System.Drawing.Font)(resources.GetObject("lstUserRole.Font")));
			this.lstUserRole.HorizontalExtent = ((int)(resources.GetObject("lstUserRole.HorizontalExtent")));
			this.lstUserRole.HorizontalScrollbar = ((bool)(resources.GetObject("lstUserRole.HorizontalScrollbar")));
			this.lstUserRole.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lstUserRole.ImeMode")));
			this.lstUserRole.IntegralHeight = ((bool)(resources.GetObject("lstUserRole.IntegralHeight")));
			this.lstUserRole.ItemHeight = ((int)(resources.GetObject("lstUserRole.ItemHeight")));
			this.lstUserRole.Location = ((System.Drawing.Point)(resources.GetObject("lstUserRole.Location")));
			this.lstUserRole.Name = "lstUserRole";
			this.lstUserRole.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lstUserRole.RightToLeft")));
			this.lstUserRole.ScrollAlwaysVisible = ((bool)(resources.GetObject("lstUserRole.ScrollAlwaysVisible")));
			this.lstUserRole.Size = ((System.Drawing.Size)(resources.GetObject("lstUserRole.Size")));
			this.lstUserRole.TabIndex = ((int)(resources.GetObject("lstUserRole.TabIndex")));
			this.toolTip1.SetToolTip(this.lstUserRole, resources.GetString("lstUserRole.ToolTip"));
			this.lstUserRole.Visible = ((bool)(resources.GetObject("lstUserRole.Visible")));
			this.lstUserRole.DoubleClick += new System.EventHandler(this.lstUserRole_DoubleClick);
			// 
			// lstAllRolesNoInUser
			// 
			this.lstAllRolesNoInUser.AccessibleDescription = resources.GetString("lstAllRolesNoInUser.AccessibleDescription");
			this.lstAllRolesNoInUser.AccessibleName = resources.GetString("lstAllRolesNoInUser.AccessibleName");
			this.lstAllRolesNoInUser.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lstAllRolesNoInUser.Anchor")));
			this.lstAllRolesNoInUser.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("lstAllRolesNoInUser.BackgroundImage")));
			this.lstAllRolesNoInUser.ColumnWidth = ((int)(resources.GetObject("lstAllRolesNoInUser.ColumnWidth")));
			this.lstAllRolesNoInUser.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lstAllRolesNoInUser.Dock")));
			this.lstAllRolesNoInUser.Enabled = ((bool)(resources.GetObject("lstAllRolesNoInUser.Enabled")));
			this.lstAllRolesNoInUser.Font = ((System.Drawing.Font)(resources.GetObject("lstAllRolesNoInUser.Font")));
			this.lstAllRolesNoInUser.HorizontalExtent = ((int)(resources.GetObject("lstAllRolesNoInUser.HorizontalExtent")));
			this.lstAllRolesNoInUser.HorizontalScrollbar = ((bool)(resources.GetObject("lstAllRolesNoInUser.HorizontalScrollbar")));
			this.lstAllRolesNoInUser.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lstAllRolesNoInUser.ImeMode")));
			this.lstAllRolesNoInUser.IntegralHeight = ((bool)(resources.GetObject("lstAllRolesNoInUser.IntegralHeight")));
			this.lstAllRolesNoInUser.ItemHeight = ((int)(resources.GetObject("lstAllRolesNoInUser.ItemHeight")));
			this.lstAllRolesNoInUser.Location = ((System.Drawing.Point)(resources.GetObject("lstAllRolesNoInUser.Location")));
			this.lstAllRolesNoInUser.Name = "lstAllRolesNoInUser";
			this.lstAllRolesNoInUser.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lstAllRolesNoInUser.RightToLeft")));
			this.lstAllRolesNoInUser.ScrollAlwaysVisible = ((bool)(resources.GetObject("lstAllRolesNoInUser.ScrollAlwaysVisible")));
			this.lstAllRolesNoInUser.Size = ((System.Drawing.Size)(resources.GetObject("lstAllRolesNoInUser.Size")));
			this.lstAllRolesNoInUser.TabIndex = ((int)(resources.GetObject("lstAllRolesNoInUser.TabIndex")));
			this.toolTip1.SetToolTip(this.lstAllRolesNoInUser, resources.GetString("lstAllRolesNoInUser.ToolTip"));
			this.lstAllRolesNoInUser.Visible = ((bool)(resources.GetObject("lstAllRolesNoInUser.Visible")));
			this.lstAllRolesNoInUser.DoubleClick += new System.EventHandler(this.lstAllRolesNoInUser_DoubleClick);
			this.lstAllRolesNoInUser.SelectedIndexChanged += new System.EventHandler(this.lstAllRolesNoInUser_SelectedIndexChanged);
			// 
			// btnRemoveAllRoles
			// 
			this.btnRemoveAllRoles.AccessibleDescription = resources.GetString("btnRemoveAllRoles.AccessibleDescription");
			this.btnRemoveAllRoles.AccessibleName = resources.GetString("btnRemoveAllRoles.AccessibleName");
			this.btnRemoveAllRoles.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnRemoveAllRoles.Anchor")));
			this.btnRemoveAllRoles.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnRemoveAllRoles.BackgroundImage")));
			this.btnRemoveAllRoles.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnRemoveAllRoles.Dock")));
			this.btnRemoveAllRoles.Enabled = ((bool)(resources.GetObject("btnRemoveAllRoles.Enabled")));
			this.btnRemoveAllRoles.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnRemoveAllRoles.FlatStyle")));
			this.btnRemoveAllRoles.Font = ((System.Drawing.Font)(resources.GetObject("btnRemoveAllRoles.Font")));
			this.btnRemoveAllRoles.Image = ((System.Drawing.Image)(resources.GetObject("btnRemoveAllRoles.Image")));
			this.btnRemoveAllRoles.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnRemoveAllRoles.ImageAlign")));
			this.btnRemoveAllRoles.ImageIndex = ((int)(resources.GetObject("btnRemoveAllRoles.ImageIndex")));
			this.btnRemoveAllRoles.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnRemoveAllRoles.ImeMode")));
			this.btnRemoveAllRoles.Location = ((System.Drawing.Point)(resources.GetObject("btnRemoveAllRoles.Location")));
			this.btnRemoveAllRoles.Name = "btnRemoveAllRoles";
			this.btnRemoveAllRoles.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnRemoveAllRoles.RightToLeft")));
			this.btnRemoveAllRoles.Size = ((System.Drawing.Size)(resources.GetObject("btnRemoveAllRoles.Size")));
			this.btnRemoveAllRoles.TabIndex = ((int)(resources.GetObject("btnRemoveAllRoles.TabIndex")));
			this.btnRemoveAllRoles.Text = resources.GetString("btnRemoveAllRoles.Text");
			this.btnRemoveAllRoles.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnRemoveAllRoles.TextAlign")));
			this.toolTip1.SetToolTip(this.btnRemoveAllRoles, resources.GetString("btnRemoveAllRoles.ToolTip"));
			this.btnRemoveAllRoles.Visible = ((bool)(resources.GetObject("btnRemoveAllRoles.Visible")));
			this.btnRemoveAllRoles.Click += new System.EventHandler(this.btnRemoveAllRoles_Click);
			// 
			// btnRemoveRole
			// 
			this.btnRemoveRole.AccessibleDescription = resources.GetString("btnRemoveRole.AccessibleDescription");
			this.btnRemoveRole.AccessibleName = resources.GetString("btnRemoveRole.AccessibleName");
			this.btnRemoveRole.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnRemoveRole.Anchor")));
			this.btnRemoveRole.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnRemoveRole.BackgroundImage")));
			this.btnRemoveRole.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnRemoveRole.Dock")));
			this.btnRemoveRole.Enabled = ((bool)(resources.GetObject("btnRemoveRole.Enabled")));
			this.btnRemoveRole.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnRemoveRole.FlatStyle")));
			this.btnRemoveRole.Font = ((System.Drawing.Font)(resources.GetObject("btnRemoveRole.Font")));
			this.btnRemoveRole.Image = ((System.Drawing.Image)(resources.GetObject("btnRemoveRole.Image")));
			this.btnRemoveRole.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnRemoveRole.ImageAlign")));
			this.btnRemoveRole.ImageIndex = ((int)(resources.GetObject("btnRemoveRole.ImageIndex")));
			this.btnRemoveRole.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnRemoveRole.ImeMode")));
			this.btnRemoveRole.Location = ((System.Drawing.Point)(resources.GetObject("btnRemoveRole.Location")));
			this.btnRemoveRole.Name = "btnRemoveRole";
			this.btnRemoveRole.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnRemoveRole.RightToLeft")));
			this.btnRemoveRole.Size = ((System.Drawing.Size)(resources.GetObject("btnRemoveRole.Size")));
			this.btnRemoveRole.TabIndex = ((int)(resources.GetObject("btnRemoveRole.TabIndex")));
			this.btnRemoveRole.Text = resources.GetString("btnRemoveRole.Text");
			this.btnRemoveRole.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnRemoveRole.TextAlign")));
			this.toolTip1.SetToolTip(this.btnRemoveRole, resources.GetString("btnRemoveRole.ToolTip"));
			this.btnRemoveRole.Visible = ((bool)(resources.GetObject("btnRemoveRole.Visible")));
			this.btnRemoveRole.Click += new System.EventHandler(this.btnRemoveRole_Click);
			// 
			// btnaddAllRoles
			// 
			this.btnaddAllRoles.AccessibleDescription = resources.GetString("btnaddAllRoles.AccessibleDescription");
			this.btnaddAllRoles.AccessibleName = resources.GetString("btnaddAllRoles.AccessibleName");
			this.btnaddAllRoles.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnaddAllRoles.Anchor")));
			this.btnaddAllRoles.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnaddAllRoles.BackgroundImage")));
			this.btnaddAllRoles.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnaddAllRoles.Dock")));
			this.btnaddAllRoles.Enabled = ((bool)(resources.GetObject("btnaddAllRoles.Enabled")));
			this.btnaddAllRoles.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnaddAllRoles.FlatStyle")));
			this.btnaddAllRoles.Font = ((System.Drawing.Font)(resources.GetObject("btnaddAllRoles.Font")));
			this.btnaddAllRoles.Image = ((System.Drawing.Image)(resources.GetObject("btnaddAllRoles.Image")));
			this.btnaddAllRoles.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnaddAllRoles.ImageAlign")));
			this.btnaddAllRoles.ImageIndex = ((int)(resources.GetObject("btnaddAllRoles.ImageIndex")));
			this.btnaddAllRoles.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnaddAllRoles.ImeMode")));
			this.btnaddAllRoles.Location = ((System.Drawing.Point)(resources.GetObject("btnaddAllRoles.Location")));
			this.btnaddAllRoles.Name = "btnaddAllRoles";
			this.btnaddAllRoles.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnaddAllRoles.RightToLeft")));
			this.btnaddAllRoles.Size = ((System.Drawing.Size)(resources.GetObject("btnaddAllRoles.Size")));
			this.btnaddAllRoles.TabIndex = ((int)(resources.GetObject("btnaddAllRoles.TabIndex")));
			this.btnaddAllRoles.Text = resources.GetString("btnaddAllRoles.Text");
			this.btnaddAllRoles.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnaddAllRoles.TextAlign")));
			this.toolTip1.SetToolTip(this.btnaddAllRoles, resources.GetString("btnaddAllRoles.ToolTip"));
			this.btnaddAllRoles.Visible = ((bool)(resources.GetObject("btnaddAllRoles.Visible")));
			this.btnaddAllRoles.Click += new System.EventHandler(this.btnaddAllRoles_Click);
			// 
			// btnAddRole
			// 
			this.btnAddRole.AccessibleDescription = resources.GetString("btnAddRole.AccessibleDescription");
			this.btnAddRole.AccessibleName = resources.GetString("btnAddRole.AccessibleName");
			this.btnAddRole.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnAddRole.Anchor")));
			this.btnAddRole.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnAddRole.BackgroundImage")));
			this.btnAddRole.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnAddRole.Dock")));
			this.btnAddRole.Enabled = ((bool)(resources.GetObject("btnAddRole.Enabled")));
			this.btnAddRole.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnAddRole.FlatStyle")));
			this.btnAddRole.Font = ((System.Drawing.Font)(resources.GetObject("btnAddRole.Font")));
			this.btnAddRole.Image = ((System.Drawing.Image)(resources.GetObject("btnAddRole.Image")));
			this.btnAddRole.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnAddRole.ImageAlign")));
			this.btnAddRole.ImageIndex = ((int)(resources.GetObject("btnAddRole.ImageIndex")));
			this.btnAddRole.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnAddRole.ImeMode")));
			this.btnAddRole.Location = ((System.Drawing.Point)(resources.GetObject("btnAddRole.Location")));
			this.btnAddRole.Name = "btnAddRole";
			this.btnAddRole.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnAddRole.RightToLeft")));
			this.btnAddRole.Size = ((System.Drawing.Size)(resources.GetObject("btnAddRole.Size")));
			this.btnAddRole.TabIndex = ((int)(resources.GetObject("btnAddRole.TabIndex")));
			this.btnAddRole.Text = resources.GetString("btnAddRole.Text");
			this.btnAddRole.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnAddRole.TextAlign")));
			this.toolTip1.SetToolTip(this.btnAddRole, resources.GetString("btnAddRole.ToolTip"));
			this.btnAddRole.Visible = ((bool)(resources.GetObject("btnAddRole.Visible")));
			this.btnAddRole.Click += new System.EventHandler(this.btnAddRole_Click);
			// 
			// tgridViewData
			// 
			this.tgridViewData.AccessibleDescription = resources.GetString("tgridViewData.AccessibleDescription");
			this.tgridViewData.AccessibleName = resources.GetString("tgridViewData.AccessibleName");
			this.tgridViewData.AllowAddNew = ((bool)(resources.GetObject("tgridViewData.AllowAddNew")));
			this.tgridViewData.AllowArrows = ((bool)(resources.GetObject("tgridViewData.AllowArrows")));
			this.tgridViewData.AllowColMove = ((bool)(resources.GetObject("tgridViewData.AllowColMove")));
			this.tgridViewData.AllowColSelect = ((bool)(resources.GetObject("tgridViewData.AllowColSelect")));
			this.tgridViewData.AllowDelete = ((bool)(resources.GetObject("tgridViewData.AllowDelete")));
			this.tgridViewData.AllowDrag = ((bool)(resources.GetObject("tgridViewData.AllowDrag")));
			this.tgridViewData.AllowFilter = ((bool)(resources.GetObject("tgridViewData.AllowFilter")));
			this.tgridViewData.AllowHorizontalSplit = ((bool)(resources.GetObject("tgridViewData.AllowHorizontalSplit")));
			this.tgridViewData.AllowRowSelect = ((bool)(resources.GetObject("tgridViewData.AllowRowSelect")));
			this.tgridViewData.AllowSort = ((bool)(resources.GetObject("tgridViewData.AllowSort")));
			this.tgridViewData.AllowUpdate = ((bool)(resources.GetObject("tgridViewData.AllowUpdate")));
			this.tgridViewData.AllowUpdateOnBlur = ((bool)(resources.GetObject("tgridViewData.AllowUpdateOnBlur")));
			this.tgridViewData.AllowVerticalSplit = ((bool)(resources.GetObject("tgridViewData.AllowVerticalSplit")));
			this.tgridViewData.AlternatingRows = ((bool)(resources.GetObject("tgridViewData.AlternatingRows")));
			this.tgridViewData.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tgridViewData.Anchor")));
			this.tgridViewData.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tgridViewData.BackgroundImage")));
			this.tgridViewData.BorderStyle = ((System.Windows.Forms.BorderStyle)(resources.GetObject("tgridViewData.BorderStyle")));
			this.tgridViewData.Caption = resources.GetString("tgridViewData.Caption");
			this.tgridViewData.CaptionHeight = ((int)(resources.GetObject("tgridViewData.CaptionHeight")));
			this.tgridViewData.CellTipsDelay = ((int)(resources.GetObject("tgridViewData.CellTipsDelay")));
			this.tgridViewData.CellTipsWidth = ((int)(resources.GetObject("tgridViewData.CellTipsWidth")));
			this.tgridViewData.ChildGrid = ((C1.Win.C1TrueDBGrid.C1TrueDBGrid)(resources.GetObject("tgridViewData.ChildGrid")));
			this.tgridViewData.CollapseColor = ((System.Drawing.Color)(resources.GetObject("tgridViewData.CollapseColor")));
			this.tgridViewData.ColumnFooters = ((bool)(resources.GetObject("tgridViewData.ColumnFooters")));
			this.tgridViewData.ColumnHeaders = ((bool)(resources.GetObject("tgridViewData.ColumnHeaders")));
			this.tgridViewData.DefColWidth = ((int)(resources.GetObject("tgridViewData.DefColWidth")));
			this.tgridViewData.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tgridViewData.Dock")));
			this.tgridViewData.EditDropDown = ((bool)(resources.GetObject("tgridViewData.EditDropDown")));
			this.tgridViewData.EmptyRows = ((bool)(resources.GetObject("tgridViewData.EmptyRows")));
			this.tgridViewData.Enabled = ((bool)(resources.GetObject("tgridViewData.Enabled")));
			this.tgridViewData.ExpandColor = ((System.Drawing.Color)(resources.GetObject("tgridViewData.ExpandColor")));
			this.tgridViewData.ExposeCellMode = ((C1.Win.C1TrueDBGrid.ExposeCellModeEnum)(resources.GetObject("tgridViewData.ExposeCellMode")));
			this.tgridViewData.ExtendRightColumn = ((bool)(resources.GetObject("tgridViewData.ExtendRightColumn")));
			this.tgridViewData.FetchRowStyles = ((bool)(resources.GetObject("tgridViewData.FetchRowStyles")));
			this.tgridViewData.FilterBar = ((bool)(resources.GetObject("tgridViewData.FilterBar")));
			this.tgridViewData.FlatStyle = ((C1.Win.C1TrueDBGrid.FlatModeEnum)(resources.GetObject("tgridViewData.FlatStyle")));
			this.tgridViewData.Font = ((System.Drawing.Font)(resources.GetObject("tgridViewData.Font")));
			this.tgridViewData.GroupByAreaVisible = ((bool)(resources.GetObject("tgridViewData.GroupByAreaVisible")));
			this.tgridViewData.GroupByCaption = resources.GetString("tgridViewData.GroupByCaption");
			this.tgridViewData.Images.Add(((System.Drawing.Image)(resources.GetObject("resource"))));
			this.tgridViewData.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tgridViewData.ImeMode")));
			this.tgridViewData.LinesPerRow = ((int)(resources.GetObject("tgridViewData.LinesPerRow")));
			this.tgridViewData.Location = ((System.Drawing.Point)(resources.GetObject("tgridViewData.Location")));
			this.tgridViewData.MarqueeStyle = C1.Win.C1TrueDBGrid.MarqueeEnum.HighlightRow;
			this.tgridViewData.Name = "tgridViewData";
			this.tgridViewData.PictureAddnewRow = ((System.Drawing.Image)(resources.GetObject("tgridViewData.PictureAddnewRow")));
			this.tgridViewData.PictureCurrentRow = ((System.Drawing.Image)(resources.GetObject("tgridViewData.PictureCurrentRow")));
			this.tgridViewData.PictureFilterBar = ((System.Drawing.Image)(resources.GetObject("tgridViewData.PictureFilterBar")));
			this.tgridViewData.PictureFooterRow = ((System.Drawing.Image)(resources.GetObject("tgridViewData.PictureFooterRow")));
			this.tgridViewData.PictureHeaderRow = ((System.Drawing.Image)(resources.GetObject("tgridViewData.PictureHeaderRow")));
			this.tgridViewData.PictureModifiedRow = ((System.Drawing.Image)(resources.GetObject("tgridViewData.PictureModifiedRow")));
			this.tgridViewData.PictureStandardRow = ((System.Drawing.Image)(resources.GetObject("tgridViewData.PictureStandardRow")));
			this.tgridViewData.PreviewInfo.AllowSizing = ((bool)(resources.GetObject("tgridViewData.PreviewInfo.AllowSizing")));
			this.tgridViewData.PreviewInfo.Caption = resources.GetString("tgridViewData.PreviewInfo.Caption");
			this.tgridViewData.PreviewInfo.Location = ((System.Drawing.Point)(resources.GetObject("tgridViewData.PreviewInfo.Location")));
			this.tgridViewData.PreviewInfo.Size = ((System.Drawing.Size)(resources.GetObject("tgridViewData.PreviewInfo.Size")));
			this.tgridViewData.PreviewInfo.ToolBars = ((bool)(resources.GetObject("tgridViewData.PreviewInfo.ToolBars")));
			this.tgridViewData.PreviewInfo.UIStrings.Content = ((string[])(resources.GetObject("tgridViewData.PreviewInfo.UIStrings.Content")));
			this.tgridViewData.PreviewInfo.ZoomFactor = ((System.Double)(resources.GetObject("tgridViewData.PreviewInfo.ZoomFactor")));
			this.tgridViewData.PrintInfo.MaxRowHeight = ((int)(resources.GetObject("tgridViewData.PrintInfo.MaxRowHeight")));
			this.tgridViewData.PrintInfo.OwnerDrawPageFooter = ((bool)(resources.GetObject("tgridViewData.PrintInfo.OwnerDrawPageFooter")));
			this.tgridViewData.PrintInfo.OwnerDrawPageHeader = ((bool)(resources.GetObject("tgridViewData.PrintInfo.OwnerDrawPageHeader")));
			this.tgridViewData.PrintInfo.PageFooter = resources.GetString("tgridViewData.PrintInfo.PageFooter");
			this.tgridViewData.PrintInfo.PageFooterHeight = ((int)(resources.GetObject("tgridViewData.PrintInfo.PageFooterHeight")));
			this.tgridViewData.PrintInfo.PageHeader = resources.GetString("tgridViewData.PrintInfo.PageHeader");
			this.tgridViewData.PrintInfo.PageHeaderHeight = ((int)(resources.GetObject("tgridViewData.PrintInfo.PageHeaderHeight")));
			this.tgridViewData.PrintInfo.PrintHorizontalSplits = ((bool)(resources.GetObject("tgridViewData.PrintInfo.PrintHorizontalSplits")));
			this.tgridViewData.PrintInfo.ProgressCaption = resources.GetString("tgridViewData.PrintInfo.ProgressCaption");
			this.tgridViewData.PrintInfo.RepeatColumnFooters = ((bool)(resources.GetObject("tgridViewData.PrintInfo.RepeatColumnFooters")));
			this.tgridViewData.PrintInfo.RepeatColumnHeaders = ((bool)(resources.GetObject("tgridViewData.PrintInfo.RepeatColumnHeaders")));
			this.tgridViewData.PrintInfo.RepeatGridHeader = ((bool)(resources.GetObject("tgridViewData.PrintInfo.RepeatGridHeader")));
			this.tgridViewData.PrintInfo.RepeatSplitHeaders = ((bool)(resources.GetObject("tgridViewData.PrintInfo.RepeatSplitHeaders")));
			this.tgridViewData.PrintInfo.ShowOptionsDialog = ((bool)(resources.GetObject("tgridViewData.PrintInfo.ShowOptionsDialog")));
			this.tgridViewData.PrintInfo.ShowProgressForm = ((bool)(resources.GetObject("tgridViewData.PrintInfo.ShowProgressForm")));
			this.tgridViewData.PrintInfo.ShowSelection = ((bool)(resources.GetObject("tgridViewData.PrintInfo.ShowSelection")));
			this.tgridViewData.PrintInfo.UseGridColors = ((bool)(resources.GetObject("tgridViewData.PrintInfo.UseGridColors")));
			this.tgridViewData.RecordSelectors = ((bool)(resources.GetObject("tgridViewData.RecordSelectors")));
			this.tgridViewData.RecordSelectorWidth = ((int)(resources.GetObject("tgridViewData.RecordSelectorWidth")));
			this.tgridViewData.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tgridViewData.RightToLeft")));
			this.tgridViewData.RowDivider.Color = ((System.Drawing.Color)(resources.GetObject("resource.Color")));
			this.tgridViewData.RowDivider.Style = ((C1.Win.C1TrueDBGrid.LineStyleEnum)(resources.GetObject("resource.Style")));
			this.tgridViewData.RowHeight = ((int)(resources.GetObject("tgridViewData.RowHeight")));
			this.tgridViewData.RowSubDividerColor = ((System.Drawing.Color)(resources.GetObject("tgridViewData.RowSubDividerColor")));
			this.tgridViewData.ScrollTips = ((bool)(resources.GetObject("tgridViewData.ScrollTips")));
			this.tgridViewData.ScrollTrack = ((bool)(resources.GetObject("tgridViewData.ScrollTrack")));
			this.tgridViewData.Size = ((System.Drawing.Size)(resources.GetObject("tgridViewData.Size")));
			this.tgridViewData.SpringMode = ((bool)(resources.GetObject("tgridViewData.SpringMode")));
			this.tgridViewData.TabAcrossSplits = ((bool)(resources.GetObject("tgridViewData.TabAcrossSplits")));
			this.tgridViewData.TabIndex = ((int)(resources.GetObject("tgridViewData.TabIndex")));
			this.tgridViewData.Text = resources.GetString("tgridViewData.Text");
			this.toolTip1.SetToolTip(this.tgridViewData, resources.GetString("tgridViewData.ToolTip"));
			this.tgridViewData.ViewCaptionWidth = ((int)(resources.GetObject("tgridViewData.ViewCaptionWidth")));
			this.tgridViewData.ViewColumnWidth = ((int)(resources.GetObject("tgridViewData.ViewColumnWidth")));
			this.tgridViewData.Visible = ((bool)(resources.GetObject("tgridViewData.Visible")));
			this.tgridViewData.WrapCellPointer = ((bool)(resources.GetObject("tgridViewData.WrapCellPointer")));
			this.tgridViewData.RowColChange += new C1.Win.C1TrueDBGrid.RowColChangeEventHandler(this.tgridViewData_RowColChange);
			this.tgridViewData.Click += new System.EventHandler(this.tgridViewData_Click);
			this.tgridViewData.BeforeRowColChange += new C1.Win.C1TrueDBGrid.CancelEventHandler(this.tgridViewData_BeforeRowColChange);
			this.tgridViewData.PropBag = resources.GetString("tgridViewData.PropBag");
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
			this.toolTip1.SetToolTip(this.btnHelp, resources.GetString("btnHelp.ToolTip"));
			this.btnHelp.Visible = ((bool)(resources.GetObject("btnHelp.Visible")));
			// 
			// lblLoginCaption
			// 
			this.lblLoginCaption.AccessibleDescription = resources.GetString("lblLoginCaption.AccessibleDescription");
			this.lblLoginCaption.AccessibleName = resources.GetString("lblLoginCaption.AccessibleName");
			this.lblLoginCaption.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblLoginCaption.Anchor")));
			this.lblLoginCaption.AutoSize = ((bool)(resources.GetObject("lblLoginCaption.AutoSize")));
			this.lblLoginCaption.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblLoginCaption.Dock")));
			this.lblLoginCaption.Enabled = ((bool)(resources.GetObject("lblLoginCaption.Enabled")));
			this.lblLoginCaption.Font = ((System.Drawing.Font)(resources.GetObject("lblLoginCaption.Font")));
			this.lblLoginCaption.Image = ((System.Drawing.Image)(resources.GetObject("lblLoginCaption.Image")));
			this.lblLoginCaption.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblLoginCaption.ImageAlign")));
			this.lblLoginCaption.ImageIndex = ((int)(resources.GetObject("lblLoginCaption.ImageIndex")));
			this.lblLoginCaption.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblLoginCaption.ImeMode")));
			this.lblLoginCaption.Location = ((System.Drawing.Point)(resources.GetObject("lblLoginCaption.Location")));
			this.lblLoginCaption.Name = "lblLoginCaption";
			this.lblLoginCaption.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblLoginCaption.RightToLeft")));
			this.lblLoginCaption.Size = ((System.Drawing.Size)(resources.GetObject("lblLoginCaption.Size")));
			this.lblLoginCaption.TabIndex = ((int)(resources.GetObject("lblLoginCaption.TabIndex")));
			this.lblLoginCaption.Text = resources.GetString("lblLoginCaption.Text");
			this.lblLoginCaption.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblLoginCaption.TextAlign")));
			this.toolTip1.SetToolTip(this.lblLoginCaption, resources.GetString("lblLoginCaption.ToolTip"));
			this.lblLoginCaption.Visible = ((bool)(resources.GetObject("lblLoginCaption.Visible")));
			// 
			// lblFullNameCaption
			// 
			this.lblFullNameCaption.AccessibleDescription = resources.GetString("lblFullNameCaption.AccessibleDescription");
			this.lblFullNameCaption.AccessibleName = resources.GetString("lblFullNameCaption.AccessibleName");
			this.lblFullNameCaption.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblFullNameCaption.Anchor")));
			this.lblFullNameCaption.AutoSize = ((bool)(resources.GetObject("lblFullNameCaption.AutoSize")));
			this.lblFullNameCaption.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblFullNameCaption.Dock")));
			this.lblFullNameCaption.Enabled = ((bool)(resources.GetObject("lblFullNameCaption.Enabled")));
			this.lblFullNameCaption.Font = ((System.Drawing.Font)(resources.GetObject("lblFullNameCaption.Font")));
			this.lblFullNameCaption.Image = ((System.Drawing.Image)(resources.GetObject("lblFullNameCaption.Image")));
			this.lblFullNameCaption.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblFullNameCaption.ImageAlign")));
			this.lblFullNameCaption.ImageIndex = ((int)(resources.GetObject("lblFullNameCaption.ImageIndex")));
			this.lblFullNameCaption.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblFullNameCaption.ImeMode")));
			this.lblFullNameCaption.Location = ((System.Drawing.Point)(resources.GetObject("lblFullNameCaption.Location")));
			this.lblFullNameCaption.Name = "lblFullNameCaption";
			this.lblFullNameCaption.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblFullNameCaption.RightToLeft")));
			this.lblFullNameCaption.Size = ((System.Drawing.Size)(resources.GetObject("lblFullNameCaption.Size")));
			this.lblFullNameCaption.TabIndex = ((int)(resources.GetObject("lblFullNameCaption.TabIndex")));
			this.lblFullNameCaption.Text = resources.GetString("lblFullNameCaption.Text");
			this.lblFullNameCaption.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblFullNameCaption.TextAlign")));
			this.toolTip1.SetToolTip(this.lblFullNameCaption, resources.GetString("lblFullNameCaption.ToolTip"));
			this.lblFullNameCaption.Visible = ((bool)(resources.GetObject("lblFullNameCaption.Visible")));
			// 
			// lblDescriptionCaption
			// 
			this.lblDescriptionCaption.AccessibleDescription = resources.GetString("lblDescriptionCaption.AccessibleDescription");
			this.lblDescriptionCaption.AccessibleName = resources.GetString("lblDescriptionCaption.AccessibleName");
			this.lblDescriptionCaption.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblDescriptionCaption.Anchor")));
			this.lblDescriptionCaption.AutoSize = ((bool)(resources.GetObject("lblDescriptionCaption.AutoSize")));
			this.lblDescriptionCaption.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblDescriptionCaption.Dock")));
			this.lblDescriptionCaption.Enabled = ((bool)(resources.GetObject("lblDescriptionCaption.Enabled")));
			this.lblDescriptionCaption.Font = ((System.Drawing.Font)(resources.GetObject("lblDescriptionCaption.Font")));
			this.lblDescriptionCaption.Image = ((System.Drawing.Image)(resources.GetObject("lblDescriptionCaption.Image")));
			this.lblDescriptionCaption.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblDescriptionCaption.ImageAlign")));
			this.lblDescriptionCaption.ImageIndex = ((int)(resources.GetObject("lblDescriptionCaption.ImageIndex")));
			this.lblDescriptionCaption.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblDescriptionCaption.ImeMode")));
			this.lblDescriptionCaption.Location = ((System.Drawing.Point)(resources.GetObject("lblDescriptionCaption.Location")));
			this.lblDescriptionCaption.Name = "lblDescriptionCaption";
			this.lblDescriptionCaption.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblDescriptionCaption.RightToLeft")));
			this.lblDescriptionCaption.Size = ((System.Drawing.Size)(resources.GetObject("lblDescriptionCaption.Size")));
			this.lblDescriptionCaption.TabIndex = ((int)(resources.GetObject("lblDescriptionCaption.TabIndex")));
			this.lblDescriptionCaption.Text = resources.GetString("lblDescriptionCaption.Text");
			this.lblDescriptionCaption.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblDescriptionCaption.TextAlign")));
			this.toolTip1.SetToolTip(this.lblDescriptionCaption, resources.GetString("lblDescriptionCaption.ToolTip"));
			this.lblDescriptionCaption.Visible = ((bool)(resources.GetObject("lblDescriptionCaption.Visible")));
			// 
			// btnOK
			// 
			this.btnOK.AccessibleDescription = resources.GetString("btnOK.AccessibleDescription");
			this.btnOK.AccessibleName = resources.GetString("btnOK.AccessibleName");
			this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnOK.Anchor")));
			this.btnOK.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnOK.BackgroundImage")));
			this.btnOK.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnOK.Dock")));
			this.btnOK.Enabled = ((bool)(resources.GetObject("btnOK.Enabled")));
			this.btnOK.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnOK.FlatStyle")));
			this.btnOK.Font = ((System.Drawing.Font)(resources.GetObject("btnOK.Font")));
			this.btnOK.Image = ((System.Drawing.Image)(resources.GetObject("btnOK.Image")));
			this.btnOK.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnOK.ImageAlign")));
			this.btnOK.ImageIndex = ((int)(resources.GetObject("btnOK.ImageIndex")));
			this.btnOK.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnOK.ImeMode")));
			this.btnOK.Location = ((System.Drawing.Point)(resources.GetObject("btnOK.Location")));
			this.btnOK.Name = "btnOK";
			this.btnOK.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnOK.RightToLeft")));
			this.btnOK.Size = ((System.Drawing.Size)(resources.GetObject("btnOK.Size")));
			this.btnOK.TabIndex = ((int)(resources.GetObject("btnOK.TabIndex")));
			this.btnOK.Text = resources.GetString("btnOK.Text");
			this.btnOK.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnOK.TextAlign")));
			this.toolTip1.SetToolTip(this.btnOK, resources.GetString("btnOK.ToolTip"));
			this.btnOK.Visible = ((bool)(resources.GetObject("btnOK.Visible")));
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// GrantRoleUser
			// 
			this.AccessibleDescription = resources.GetString("$this.AccessibleDescription");
			this.AccessibleName = resources.GetString("$this.AccessibleName");
			this.AutoScaleBaseSize = ((System.Drawing.Size)(resources.GetObject("$this.AutoScaleBaseSize")));
			this.AutoScroll = ((bool)(resources.GetObject("$this.AutoScroll")));
			this.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("$this.AutoScrollMargin")));
			this.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("$this.AutoScrollMinSize")));
			this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
			this.CancelButton = this.btnCancel;
			this.ClientSize = ((System.Drawing.Size)(resources.GetObject("$this.ClientSize")));
			this.Controls.Add(this.lblDescriptionCaption);
			this.Controls.Add(this.lblFullNameCaption);
			this.Controls.Add(this.lblLoginCaption);
			this.Controls.Add(this.tgridViewData);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.btnHelp);
			this.Controls.Add(this.btnOK);
			this.Enabled = ((bool)(resources.GetObject("$this.Enabled")));
			this.Font = ((System.Drawing.Font)(resources.GetObject("$this.Font")));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("$this.ImeMode")));
			this.Location = ((System.Drawing.Point)(resources.GetObject("$this.Location")));
			this.MaximizeBox = false;
			this.MaximumSize = ((System.Drawing.Size)(resources.GetObject("$this.MaximumSize")));
			this.MinimumSize = ((System.Drawing.Size)(resources.GetObject("$this.MinimumSize")));
			this.Name = "GrantRoleUser";
			this.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("$this.RightToLeft")));
			this.StartPosition = ((System.Windows.Forms.FormStartPosition)(resources.GetObject("$this.StartPosition")));
			this.Text = resources.GetString("$this.Text");
			this.toolTip1.SetToolTip(this, resources.GetString("$this.ToolTip"));
			this.Closing += new System.ComponentModel.CancelEventHandler(this.GrantRoleUser_Closing);
			this.Load += new System.EventHandler(this.GrantRoleUser_Load);
			this.groupBox1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.tgridViewData)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		#region Class's Method
		
		/// <summary>
		/// Change caption of displayed columns in grid
		/// </summary>
		/// <param name="c1Grid">C1TrueDBGrid</param>
		/// <param name="pDisplayColumns">Array of displayed columns</param>
		/// <param name="pDisplayCaptions">Array of captions of displayed columns</param>
		/// <Author> Thien HD, Jan 11, 2005</Author>		
		private void DisplayGridColumns(C1.Win.C1TrueDBGrid.C1TrueDBGrid c1Grid, string[] pDisplayColumns, string[] pDisplayCaptions)
		{
			for (int i = 0; i < c1Grid.Splits[0].DisplayColumns.Count; i++)
			{
				c1Grid.Splits[0].DisplayColumns[i].Visible = false;
			}
			for (int i = 0; i < pDisplayColumns.Length; i++)
			{
				c1Grid.Splits[0].DisplayColumns[pDisplayColumns[i]].Visible = true;
				c1Grid.Splits[0].DisplayColumns[pDisplayColumns[i]].AutoSize();
				c1Grid.Columns[pDisplayColumns[i]].Caption = pDisplayCaptions[i];
			}
		}


		/// <summary>
		/// Load data for this form
		/// Get a list of user into True DBGrid
		/// Get a list of granted  roles to user
		/// Get a list of not granted roles
		/// </summary>
		/// <Author> Thien HD, Jan 11, 2005</Author>
		private void LoadForm()
		{
			//Load list of users
			const int USERNAME_WIDTH = 100;
			const int NAME_WIDTH = 200;
			const int DESCRIPTION_WIDTH = 300;

			try
			{
				//Load user list into True DBGrid
				GrantRoleUserBO objGrantRoleUserBO = new GrantRoleUserBO();
				DataSet dstData = objGrantRoleUserBO.ListUser();

				//sort by user name
				dstData.Tables[0].DefaultView.Sort = Sys_UserTable.USERNAME_FLD;

				tgridViewData.DataSource = dstData.Tables[0];
				//tgridViewData.DataMember = dstData.Tables[0].TableName;

				//Display UserID, UserName,Name, Description
				DisplayGridColumns(tgridViewData, new string[] {Sys_UserTable.USERNAME_FLD, Sys_UserTable.NAME_FLD, Sys_UserTable.DESCRIPTION_FLD}, new string[] {lblLoginCaption.Text, lblFullNameCaption.Text, lblDescriptionCaption.Text});

				//Change width of several columns in truedb grid
				//tgridViewData.Splits[0].DisplayColumns[Sys_UserTable.USERID_FLD].Width = 50;
				//tgridViewData.Splits[0].DisplayColumns[Sys_UserTable.USERID_FLD].Visible = false;

				tgridViewData.Splits[0].DisplayColumns[Sys_UserTable.USERNAME_FLD].Width = USERNAME_WIDTH;
				tgridViewData.Splits[0].DisplayColumns[Sys_UserTable.NAME_FLD].Width = NAME_WIDTH;
				tgridViewData.Splits[0].DisplayColumns[Sys_UserTable.DESCRIPTION_FLD].Width = DESCRIPTION_WIDTH;

				//Center Heading
				for (int i = 0; i < tgridViewData.Splits[0].DisplayColumns.Count; i++)
				{
					tgridViewData.Splits[0].DisplayColumns[i].HeadingStyle.HorizontalAlignment = AlignHorzEnum.Center;
				}

				//Align the ID column
				tgridViewData.Splits[0].DisplayColumns[Sys_UserTable.USERID_FLD].Style.HorizontalAlignment = AlignHorzEnum.Near;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}


		/// <summary>
		/// Add a selected role and grant it to user
		/// </summary>
		/// <Author> Thien HD, Jan 11, 2005</Author>
		private void AddNewRole()
		{
			//get the selected row in the List box
			if (lstAllRolesNoInUser.SelectedIndex < 0)
			{
				return;
			}
			const string METHOD_NAME = THIS + ".AddNewRole()";
			try
			{
				//Get the current row in the list
				CurrencyManager cm;
				cm = (CurrencyManager) lstAllRolesNoInUser.BindingContext[lstAllRolesNoInUser.DataSource];
				DataRowView dv = (DataRowView) cm.Current;


				//get the current binding data table
				DataTable dt = ((DataTable) lstUserRole.DataSource);

				//get the current binding data table
				AddNewRowIntoDataTable(dv.Row, dt);
				cm.RemoveAt(cm.Position);

				/// HACKED: Thachnn: fix bug 1671
				((DataTable) lstUserRole.DataSource).AcceptChanges();
				((DataTable) lstAllRolesNoInUser.DataSource).AcceptChanges();			
				/// ENDHACKED: Thachnn
				
				blnEditUpdateDate = true;
			}
			catch (Exception ex)
			{
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
		/// Remove a selected granted role
		/// </summary>
		/// <Author> Thien HD, Jan 11, 2005</Author>
		private void RemoveRole()
		{
			//get the selected row in the List box
			if (lstUserRole.SelectedIndex < 0)
			{
				return;
			}
			const string METHOD_NAME = THIS + ".RemoveRole()";
			try
			{
				//Get the current row in the list
				CurrencyManager cm;
				cm = (CurrencyManager) lstUserRole.BindingContext[lstUserRole.DataSource];
				DataRowView dv = (DataRowView) cm.Current;


				//get the current binding data table
				DataTable dt = ((DataTable) lstAllRolesNoInUser.DataSource);
				AddNewRowIntoDataTable(dv.Row, dt);
				cm.RemoveAt(cm.Position);
				
				/// HACKED: Thachnn: fix bug 1671
				((DataTable) lstUserRole.DataSource).AcceptChanges();
				((DataTable) lstAllRolesNoInUser.DataSource).AcceptChanges();			
				/// ENDHACKED: Thachnn
			
				blnEditUpdateDate = true;
			}
			catch (Exception ex)
			{
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
		/// Add a new row into DataTable
		/// </summary>
		/// <param name="drDataRow">DataRow will used as an input for a new row </param>
		/// <param name="dt">DataTable will used as an input for a new row </param>
		/// <Author> Thien HD, Jan 11, 2005</Author>
		private void AddNewRowIntoDataTable(DataRow drDataRow, DataTable dt)
		{			
			//add a new row
			DataRow dr = dt.NewRow();			

			/// HACKED: Thachnn: fix bug 1671
			//grant this row value
			dr[Sys_RoleTable.ROLEID_FLD] = drDataRow[Sys_RoleTable.ROLEID_FLD];
			dr[Sys_RoleTable.NAME_FLD] = drDataRow[Sys_RoleTable.NAME_FLD];
			dr[Sys_RoleTable.DESCRIPTION_FLD] = drDataRow[Sys_RoleTable.DESCRIPTION_FLD];
			
			//add this row into table
			dt.Rows.Add(dr);
			dt.AcceptChanges();
			/// ENDHACKED: Thachnn: fix bug 1671
		}

				
		/// <summary>
		/// List of roles that not granted to user then bind to Listview
		/// </summary>
		/// <param name="pintUserid">User Identity</param>
		/// <Author> Thien HD, Jan 11, 2005</Author>
		private void FillListViewRoleNotGrantToUser(int pintUserid)
		{						
			GrantRoleUserBO objGrantRoleUserBO = new GrantRoleUserBO();			
			DataSet dstRoleNotInUser = objGrantRoleUserBO.ListRoleNotGrantToUser(pintUserid);
			//sort by role name
			dstRoleNotInUser.Tables[0].DefaultView.Sort = Sys_RoleTable.NAME_FLD;
			
			lstAllRolesNoInUser.DataSource = null;			
			lstAllRolesNoInUser.DataSource = dstRoleNotInUser.Tables[0];
			lstAllRolesNoInUser.DisplayMember = Sys_RoleTable.NAME_FLD;
			lstAllRolesNoInUser.ValueMember = Sys_RoleTable.ROLEID_FLD;
		}

		
		/// <summary>
		/// List of roles granted to user then bind to Listview
		/// </summary>
		/// <param name="pintUserid"></param>
		/// <Author> Thien HD, Jan 11, 2005</Author>
		private void FillListViewUserRole(int pintUserid)
		{
			GrantRoleUserBO objGrantRoleUserBO = new GrantRoleUserBO();			
			DataSet dstRolesInUser = objGrantRoleUserBO.ListRoleGrantToUser(pintUserid);
			//sort by role name
			dstRolesInUser.Tables[0].DefaultView.Sort = Sys_RoleTable.NAME_FLD;
			
			lstUserRole.DataSource = null;			
			lstUserRole.DataSource = dstRolesInUser.Tables[0];
			lstUserRole.DisplayMember = Sys_RoleTable.NAME_FLD;
			lstUserRole.ValueMember = Sys_RoleTable.ROLEID_FLD;
		}

		#endregion Class's Method

		#region Event Processing

		private void GrantRoleUser_Load(object sender, System.EventArgs e)
		{
			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.WaitCursor;

			#endregion Code Inserted Automatically

			
			const string METHOD_NAME = THIS + ".GrantRoleUser_Load()";
			try
			{
				#region Security
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
				#endregion

				LoadForm();
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
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
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
		
		private void label1_Click(object sender, System.EventArgs e)
		{// Code Inserted Automatically
			#region Code Inserted Automatically
			this.Cursor = Cursors.WaitCursor;
			#endregion Code Inserted Automatically

			// Code Inserted Automatically
			#region Code Inserted Automatically
			this.Cursor = Cursors.Default;
			#endregion Code Inserted Automatically
		}

		private void groupBox1_Enter(object sender, System.EventArgs e)
		{
		}

		private void btnCancel_Click(object sender, System.EventArgs e)
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

		private void tgridViewData_RowColChange(object sender, C1.Win.C1TrueDBGrid.RowColChangeEventArgs e)
		{
			const string METHOD_NAME = THIS + ".tgridViewData_RowColChange()";
			try
			{
				if (e.LastRow != tgridViewData.Row)
				{
					//DataSet ds =(DataSet) dbgDetailRouting.DataSource;
					CurrencyManager cm = (CurrencyManager) tgridViewData.BindingContext[tgridViewData.DataSource, tgridViewData.DataMember];
					if (cm.Position >= 0)
					{
						//DataRow dr = dstData.Tables[0].Rows[cm.Position];
						DataRowView dv = (DataRowView) cm.Current;

						//load the data for two list
						/// HACKED: Thachnn
						//First : List of roles that not granted to user
						FillListViewRoleNotGrantToUser(int.Parse(dv[Sys_UserTable.USERID_FLD].ToString()));
						
						//Second : List of roles that granted to user
						FillListViewUserRole(int.Parse(dv[Sys_UserTable.USERID_FLD].ToString()));
						/// ENDHACKED: Thachnn
						
						blnEditUpdateDate = false;
					}
				}

				//BEGIN/ Added by Tuan TQ -- 20 May 2005
				//Check user name for super administrator & lock related buttons
				bool bIsSuperUser = tgridViewData.Columns[Sys_UserTable.USERNAME_FLD].Text.Equals(Constants.SUPER_ADMIN_USER);
				btnAddRole.Enabled = !bIsSuperUser;
				btnaddAllRoles.Enabled = !bIsSuperUser;
				btnRemoveRole.Enabled = !bIsSuperUser;
				btnRemoveAllRoles.Enabled = !bIsSuperUser;
				//END/ Added by Tuan TQ
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
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
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

		private void tgridViewData_Click(object sender, System.EventArgs e)
		{// Code Inserted Automatically
#region Code Inserted Automatically
this.Cursor = Cursors.WaitCursor;
#endregion Code Inserted Automatically

// Code Inserted Automatically
#region Code Inserted Automatically
this.Cursor = Cursors.Default;
#endregion Code Inserted Automatically

		}
		
		private void btnAddRole_Click(object sender, System.EventArgs e)
		{
			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.WaitCursor;

			#endregion Code Inserted Automatically

			
			#region  DEL Trada 15-12-2005

			//AddNewRole();

			#endregion		
			lstAllRolesNoInUser_DoubleClick(null, null);

			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.Default;

			#endregion Code Inserted Automatically

			
		}
		
		private void btnRemoveRole_Click(object sender, System.EventArgs e)
		{
			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.WaitCursor;

			#endregion Code Inserted Automatically

			
			#region  DEL Trada 15-12-2005

			//RemoveRole();

			#endregion		
			lstUserRole_DoubleClick(null, null);

			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.Default;

			#endregion Code Inserted Automatically

			
		}				
		
		private void btnaddAllRoles_Click(object sender, System.EventArgs e)
		{
			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.WaitCursor;

			#endregion Code Inserted Automatically

			
			if (lstAllRolesNoInUser.Items.Count == 0)
			{
				return;
			}
			const string METHOD_NAME = THIS + ".btnaddAllRoles_Click()";
			try
			{
				DataTable dtRoleNotInUser = ((DataTable) lstAllRolesNoInUser.DataSource);
				DataTable dtRoleInUser = ((DataTable) lstUserRole.DataSource);
				foreach (DataRow dr in dtRoleNotInUser.Rows)
				{
					AddNewRowIntoDataTable(dr, dtRoleInUser);
				}
				dtRoleNotInUser.Rows.Clear();
				blnEditUpdateDate = true;
			}
			catch (Exception ex)
			{
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

		//**************************************************************************              
		///    <Description>
		///       Remove all roles from user
		///    </Description>
		///    <Inputs>
		///       lstUserRole
		///    </Inputs>
		///    <Outputs>
		///       All roles are added to lstAllRolesNoInUser 
		///    </Outputs>
		///    <Returns>
		///       
		///    </Returns>
		///    <Authors>
		///       THIENHD
		///    </Authors>
		///    <History>
		///       11-JAN-2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private void btnRemoveAllRoles_Click(object sender, System.EventArgs e)
		{
			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.WaitCursor;

			#endregion Code Inserted Automatically

			
			if (lstUserRole.Items.Count == 0)
			{
				return;
			}
			const string METHOD_NAME = THIS + ".btnRemoveAllRoles_Click()";
			try
			{
				DataTable dtRoleNotInUser = ((DataTable) lstAllRolesNoInUser.DataSource);
				DataTable dtRoleInUser = ((DataTable) lstUserRole.DataSource);
				foreach (DataRow dr in dtRoleInUser.Rows)
				{
					AddNewRowIntoDataTable(dr, dtRoleNotInUser);
				}
				dtRoleInUser.Rows.Clear();
				blnEditUpdateDate = true;
			}
			catch (Exception ex)
			{
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

		//**************************************************************************              
		///    <Description>
		///       Save all of this changes into database
		///    </Description>
		///    <Inputs>
		///       lstUserRole
		///    </Inputs>
		///    <Outputs>
		///       
		///    </Outputs>
		///    <Returns>
		///       
		///    </Returns>
		///    <Authors>
		///       THIENHD
		///    </Authors>
		///    <History>
		///       11-JAN-2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private void btnOK_Click(object sender, System.EventArgs e)
		{
			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.WaitCursor;

			#endregion Code Inserted Automatically

			
			if (!blnEditUpdateDate)
			{
				// Code Inserted Automatically

				#region Code Inserted Automatically

				this.Cursor = Cursors.Default;

				#endregion Code Inserted Automatically
				return;
			}
			SaveDataToDatabase();

			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.Default;

			#endregion Code Inserted Automatically

			
		}



		private bool SaveDataToDatabase() 
		{
			const string METHOD_NAME = THIS + ".SaveToDatabase()";
			try
			{
				int intUserID;
				CurrencyManager cm = (CurrencyManager) tgridViewData.BindingContext[tgridViewData.DataSource, tgridViewData.DataMember];
				if (cm.Position >= 0)
				{
					//DataRow dr = dstData.Tables[0].Rows[cm.Position];
					DataRowView dv = (DataRowView) cm.Current;
					intUserID = int.Parse(dv[Sys_UserTable.USERID_FLD].ToString());
				}
				else
				{
					return true;
				}

				GrantRoleUserBO objGrantRoleUserBO = new GrantRoleUserBO();
				DataTable dt = (DataTable) lstUserRole.DataSource;
				objGrantRoleUserBO.SaveRoleToUser(intUserID, dt);

				//change this status to false 
				blnEditUpdateDate = false;
				PCSMessageBox.Show(ErrorCode.MESSAGE_AFTER_SAVE_DATA, MessageBoxIcon.Information);
				return true;

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
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
				}
				return false;
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
				return false;
			}

		}

		//**************************************************************************              
		///    <Description>
		///       Check to see if user has changed data, if so
		///       display an warning message to warn user
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
		///       THIENHD		
		///    </Authors>
		///    <History>
		///       11-JAN-2005
		///       03/Oct/2005. Thachnn: fix bug when click MessageBox --> Yes
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private void tgridViewData_BeforeRowColChange(object sender, C1.Win.C1TrueDBGrid.CancelEventArgs e)
		{
			/// HACKED: Thachnn: make this button act like btnSearchProductCode_Click
			if (blnEditUpdateDate)
			{
				DialogResult dialogresult = PCSMessageBox.Show(ErrorCode.MESSAGE_QUESTION_STORE_INTO_DATABASE, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
				if(dialogresult == DialogResult.Yes)
				{
					//MessageBox.Show("Save");
					SaveDataToDatabase();
					e.Cancel = false;
				}
				else if(dialogresult == DialogResult.No)
				{
					e.Cancel = false;
				}
				else if(dialogresult == DialogResult.Cancel)
				{
					e.Cancel = true;
				}
			}
			/// ENDHACKED: Thachnn
		}

		//**************************************************************************              
		///    <Description>
		///       Check to see if user has changed data, if so
		///       display an warning message to warn user
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
		///       THIENHD
		///    </Authors>
		///    <History>
		///       11-JAN-2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private void GrantRoleUser_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if (blnEditUpdateDate)
			{
				//System.Windows.Forms.DialogResult dlgResult = PCSMessageBox.Show(ErrorCode.MESSAGE_QUESTION_STORE_INTO_DATABASE, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
				System.Windows.Forms.DialogResult dlgResult = PCSMessageBox.Show(ErrorCode.MESSAGE_QUESTION_STORE_INTO_DATABASE, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
				switch (dlgResult)
				{
					case DialogResult.Yes:
						if (!SaveDataToDatabase())
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

		private void lstAllRolesNoInUser_DoubleClick(object sender, System.EventArgs e)
		{
			try
			{
				if(tgridViewData.Columns[Sys_UserTable.USERNAME_FLD].Text.Equals(Constants.SUPER_ADMIN_USER))
				{
					return;
				}
				else if(lstUserRole.Items.Count == 0)
				{
					AddNewRole();
				}
				else
				{
					DialogResult result = PCSMessageBox.Show(ErrorCode.MESSAGE_REMOVE_GRANTED_ROLE_ON_THE_RIGHT,MessageBoxButtons.YesNo);
					if(result == DialogResult.No)
					{
						lstUserRole.Focus();
						return;
					}
					else
					{
						RemoveRole();
						AddNewRole();
					}
				}
			}
			catch
			{}
		}

		private void lstUserRole_DoubleClick(object sender, System.EventArgs e)
		{
			try
			{
				if(tgridViewData.Columns[Sys_UserTable.USERNAME_FLD].Text.Equals(Constants.SUPER_ADMIN_USER)) return;
				RemoveRole();
			}
			catch
			{}
		}
		
		#endregion Event Processing	

		private void lstAllRolesNoInUser_SelectedIndexChanged(object sender, System.EventArgs e)
		{
		
		}
	}
}