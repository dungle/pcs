using System;
using System.Drawing;
using System.Data;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

//using PCS's namespaces
using PCSUtils.Utils;
using PCSComUtils.PCSExc;
using PCSComUtils.Common;
using PCSComProduct.STDCost.BO;
using PCSComProduct.STDCost.DS;
using PCSUtils.Log;

namespace PCSProduct.STDCost
{
	/// <summary>
	/// Summary description for CostElement.
	/// </summary>
	public class CostElement : System.Windows.Forms.Form
	{
		#region Declaration

		#region System Generated

		private System.Windows.Forms.Label lblCategoryName;
		private System.Windows.Forms.Label lblCategoryCode;
		private System.Windows.Forms.Button btnHelp;
		private System.Windows.Forms.Button btnClose;
		private System.Windows.Forms.Button btnEdit;
		private System.Windows.Forms.Button btnAdd;
		private System.Windows.Forms.Button btnSave;
		private System.Windows.Forms.Button btnDelete;
		private System.Windows.Forms.TextBox txtName;
		private System.Windows.Forms.TextBox txtCode;
		private System.Windows.Forms.TextBox txtType;
		private System.Windows.Forms.Button btnSearchType;
		private System.Windows.Forms.Button btnUp;
		private System.Windows.Forms.Button btnDown;
		private System.Windows.Forms.Label lblRootCaption;
		private System.Windows.Forms.Label lblType;
		private System.ComponentModel.Container components = null;

		#endregion System Generated
	
		#region Variables
		
		private const string THIS = "PCSProduct.STDCost.CostElement";
		private const string CHECKED = "Checked";
		private const string TRUE	= "True";
		private const string ROOT = "Root";
		
		private EnumAction enumAction;
		private Hashtable htbCostElementType;
		private bool blnDataIsValid;
		private bool blnElementOrderChange = false;
		private bool blnChangeType = false;
		
		private CostElementBO boCostElement;
		private System.Windows.Forms.TreeView tvwCostElement;
		private DataSet dstCostElement;

		#endregion Variables

		#endregion Declaration

		#region Constructor, Destructor
		
		public CostElement()
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
		
		#endregion Constructor, Destructor

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(CostElement));
			this.txtName = new System.Windows.Forms.TextBox();
			this.txtCode = new System.Windows.Forms.TextBox();
			this.lblCategoryName = new System.Windows.Forms.Label();
			this.lblCategoryCode = new System.Windows.Forms.Label();
			this.btnHelp = new System.Windows.Forms.Button();
			this.btnClose = new System.Windows.Forms.Button();
			this.btnEdit = new System.Windows.Forms.Button();
			this.btnAdd = new System.Windows.Forms.Button();
			this.btnSave = new System.Windows.Forms.Button();
			this.btnDelete = new System.Windows.Forms.Button();
			this.txtType = new System.Windows.Forms.TextBox();
			this.lblType = new System.Windows.Forms.Label();
			this.btnSearchType = new System.Windows.Forms.Button();
			this.btnUp = new System.Windows.Forms.Button();
			this.btnDown = new System.Windows.Forms.Button();
			this.lblRootCaption = new System.Windows.Forms.Label();
			this.tvwCostElement = new System.Windows.Forms.TreeView();
			this.SuspendLayout();
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
			// 
			// txtCode
			// 
			this.txtCode.AccessibleDescription = resources.GetString("txtCode.AccessibleDescription");
			this.txtCode.AccessibleName = resources.GetString("txtCode.AccessibleName");
			this.txtCode.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("txtCode.Anchor")));
			this.txtCode.AutoSize = ((bool)(resources.GetObject("txtCode.AutoSize")));
			this.txtCode.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("txtCode.BackgroundImage")));
			this.txtCode.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("txtCode.Dock")));
			this.txtCode.Enabled = ((bool)(resources.GetObject("txtCode.Enabled")));
			this.txtCode.Font = ((System.Drawing.Font)(resources.GetObject("txtCode.Font")));
			this.txtCode.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("txtCode.ImeMode")));
			this.txtCode.Location = ((System.Drawing.Point)(resources.GetObject("txtCode.Location")));
			this.txtCode.MaxLength = ((int)(resources.GetObject("txtCode.MaxLength")));
			this.txtCode.Multiline = ((bool)(resources.GetObject("txtCode.Multiline")));
			this.txtCode.Name = "txtCode";
			this.txtCode.PasswordChar = ((char)(resources.GetObject("txtCode.PasswordChar")));
			this.txtCode.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("txtCode.RightToLeft")));
			this.txtCode.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("txtCode.ScrollBars")));
			this.txtCode.Size = ((System.Drawing.Size)(resources.GetObject("txtCode.Size")));
			this.txtCode.TabIndex = ((int)(resources.GetObject("txtCode.TabIndex")));
			this.txtCode.Text = resources.GetString("txtCode.Text");
			this.txtCode.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("txtCode.TextAlign")));
			this.txtCode.Visible = ((bool)(resources.GetObject("txtCode.Visible")));
			this.txtCode.WordWrap = ((bool)(resources.GetObject("txtCode.WordWrap")));
			// 
			// lblCategoryName
			// 
			this.lblCategoryName.AccessibleDescription = resources.GetString("lblCategoryName.AccessibleDescription");
			this.lblCategoryName.AccessibleName = resources.GetString("lblCategoryName.AccessibleName");
			this.lblCategoryName.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblCategoryName.Anchor")));
			this.lblCategoryName.AutoSize = ((bool)(resources.GetObject("lblCategoryName.AutoSize")));
			this.lblCategoryName.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblCategoryName.Dock")));
			this.lblCategoryName.Enabled = ((bool)(resources.GetObject("lblCategoryName.Enabled")));
			this.lblCategoryName.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.lblCategoryName.Font = ((System.Drawing.Font)(resources.GetObject("lblCategoryName.Font")));
			this.lblCategoryName.ForeColor = System.Drawing.Color.Maroon;
			this.lblCategoryName.Image = ((System.Drawing.Image)(resources.GetObject("lblCategoryName.Image")));
			this.lblCategoryName.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblCategoryName.ImageAlign")));
			this.lblCategoryName.ImageIndex = ((int)(resources.GetObject("lblCategoryName.ImageIndex")));
			this.lblCategoryName.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblCategoryName.ImeMode")));
			this.lblCategoryName.Location = ((System.Drawing.Point)(resources.GetObject("lblCategoryName.Location")));
			this.lblCategoryName.Name = "lblCategoryName";
			this.lblCategoryName.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblCategoryName.RightToLeft")));
			this.lblCategoryName.Size = ((System.Drawing.Size)(resources.GetObject("lblCategoryName.Size")));
			this.lblCategoryName.TabIndex = ((int)(resources.GetObject("lblCategoryName.TabIndex")));
			this.lblCategoryName.Text = resources.GetString("lblCategoryName.Text");
			this.lblCategoryName.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblCategoryName.TextAlign")));
			this.lblCategoryName.Visible = ((bool)(resources.GetObject("lblCategoryName.Visible")));
			// 
			// lblCategoryCode
			// 
			this.lblCategoryCode.AccessibleDescription = resources.GetString("lblCategoryCode.AccessibleDescription");
			this.lblCategoryCode.AccessibleName = resources.GetString("lblCategoryCode.AccessibleName");
			this.lblCategoryCode.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblCategoryCode.Anchor")));
			this.lblCategoryCode.AutoSize = ((bool)(resources.GetObject("lblCategoryCode.AutoSize")));
			this.lblCategoryCode.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblCategoryCode.Dock")));
			this.lblCategoryCode.Enabled = ((bool)(resources.GetObject("lblCategoryCode.Enabled")));
			this.lblCategoryCode.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.lblCategoryCode.Font = ((System.Drawing.Font)(resources.GetObject("lblCategoryCode.Font")));
			this.lblCategoryCode.ForeColor = System.Drawing.Color.Maroon;
			this.lblCategoryCode.Image = ((System.Drawing.Image)(resources.GetObject("lblCategoryCode.Image")));
			this.lblCategoryCode.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblCategoryCode.ImageAlign")));
			this.lblCategoryCode.ImageIndex = ((int)(resources.GetObject("lblCategoryCode.ImageIndex")));
			this.lblCategoryCode.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblCategoryCode.ImeMode")));
			this.lblCategoryCode.Location = ((System.Drawing.Point)(resources.GetObject("lblCategoryCode.Location")));
			this.lblCategoryCode.Name = "lblCategoryCode";
			this.lblCategoryCode.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblCategoryCode.RightToLeft")));
			this.lblCategoryCode.Size = ((System.Drawing.Size)(resources.GetObject("lblCategoryCode.Size")));
			this.lblCategoryCode.TabIndex = ((int)(resources.GetObject("lblCategoryCode.TabIndex")));
			this.lblCategoryCode.Text = resources.GetString("lblCategoryCode.Text");
			this.lblCategoryCode.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblCategoryCode.TextAlign")));
			this.lblCategoryCode.Visible = ((bool)(resources.GetObject("lblCategoryCode.Visible")));
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
			// txtType
			// 
			this.txtType.AccessibleDescription = resources.GetString("txtType.AccessibleDescription");
			this.txtType.AccessibleName = resources.GetString("txtType.AccessibleName");
			this.txtType.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("txtType.Anchor")));
			this.txtType.AutoSize = ((bool)(resources.GetObject("txtType.AutoSize")));
			this.txtType.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("txtType.BackgroundImage")));
			this.txtType.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("txtType.Dock")));
			this.txtType.Enabled = ((bool)(resources.GetObject("txtType.Enabled")));
			this.txtType.Font = ((System.Drawing.Font)(resources.GetObject("txtType.Font")));
			this.txtType.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("txtType.ImeMode")));
			this.txtType.Location = ((System.Drawing.Point)(resources.GetObject("txtType.Location")));
			this.txtType.MaxLength = ((int)(resources.GetObject("txtType.MaxLength")));
			this.txtType.Multiline = ((bool)(resources.GetObject("txtType.Multiline")));
			this.txtType.Name = "txtType";
			this.txtType.PasswordChar = ((char)(resources.GetObject("txtType.PasswordChar")));
			this.txtType.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("txtType.RightToLeft")));
			this.txtType.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("txtType.ScrollBars")));
			this.txtType.Size = ((System.Drawing.Size)(resources.GetObject("txtType.Size")));
			this.txtType.TabIndex = ((int)(resources.GetObject("txtType.TabIndex")));
			this.txtType.Text = resources.GetString("txtType.Text");
			this.txtType.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("txtType.TextAlign")));
			this.txtType.Visible = ((bool)(resources.GetObject("txtType.Visible")));
			this.txtType.WordWrap = ((bool)(resources.GetObject("txtType.WordWrap")));
			this.txtType.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtType_KeyDown);
			this.txtType.Validating += new System.ComponentModel.CancelEventHandler(this.txtType_Validating);
			// 
			// lblType
			// 
			this.lblType.AccessibleDescription = resources.GetString("lblType.AccessibleDescription");
			this.lblType.AccessibleName = resources.GetString("lblType.AccessibleName");
			this.lblType.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblType.Anchor")));
			this.lblType.AutoSize = ((bool)(resources.GetObject("lblType.AutoSize")));
			this.lblType.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblType.Dock")));
			this.lblType.Enabled = ((bool)(resources.GetObject("lblType.Enabled")));
			this.lblType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.lblType.Font = ((System.Drawing.Font)(resources.GetObject("lblType.Font")));
			this.lblType.ForeColor = System.Drawing.Color.Maroon;
			this.lblType.Image = ((System.Drawing.Image)(resources.GetObject("lblType.Image")));
			this.lblType.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblType.ImageAlign")));
			this.lblType.ImageIndex = ((int)(resources.GetObject("lblType.ImageIndex")));
			this.lblType.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblType.ImeMode")));
			this.lblType.Location = ((System.Drawing.Point)(resources.GetObject("lblType.Location")));
			this.lblType.Name = "lblType";
			this.lblType.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblType.RightToLeft")));
			this.lblType.Size = ((System.Drawing.Size)(resources.GetObject("lblType.Size")));
			this.lblType.TabIndex = ((int)(resources.GetObject("lblType.TabIndex")));
			this.lblType.Text = resources.GetString("lblType.Text");
			this.lblType.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblType.TextAlign")));
			this.lblType.Visible = ((bool)(resources.GetObject("lblType.Visible")));
			// 
			// btnSearchType
			// 
			this.btnSearchType.AccessibleDescription = resources.GetString("btnSearchType.AccessibleDescription");
			this.btnSearchType.AccessibleName = resources.GetString("btnSearchType.AccessibleName");
			this.btnSearchType.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnSearchType.Anchor")));
			this.btnSearchType.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSearchType.BackgroundImage")));
			this.btnSearchType.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnSearchType.Dock")));
			this.btnSearchType.Enabled = ((bool)(resources.GetObject("btnSearchType.Enabled")));
			this.btnSearchType.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnSearchType.FlatStyle")));
			this.btnSearchType.Font = ((System.Drawing.Font)(resources.GetObject("btnSearchType.Font")));
			this.btnSearchType.Image = ((System.Drawing.Image)(resources.GetObject("btnSearchType.Image")));
			this.btnSearchType.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnSearchType.ImageAlign")));
			this.btnSearchType.ImageIndex = ((int)(resources.GetObject("btnSearchType.ImageIndex")));
			this.btnSearchType.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnSearchType.ImeMode")));
			this.btnSearchType.Location = ((System.Drawing.Point)(resources.GetObject("btnSearchType.Location")));
			this.btnSearchType.Name = "btnSearchType";
			this.btnSearchType.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnSearchType.RightToLeft")));
			this.btnSearchType.Size = ((System.Drawing.Size)(resources.GetObject("btnSearchType.Size")));
			this.btnSearchType.TabIndex = ((int)(resources.GetObject("btnSearchType.TabIndex")));
			this.btnSearchType.Text = resources.GetString("btnSearchType.Text");
			this.btnSearchType.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnSearchType.TextAlign")));
			this.btnSearchType.Visible = ((bool)(resources.GetObject("btnSearchType.Visible")));
			this.btnSearchType.Click += new System.EventHandler(this.btnSearchType_Click);
			// 
			// btnUp
			// 
			this.btnUp.AccessibleDescription = resources.GetString("btnUp.AccessibleDescription");
			this.btnUp.AccessibleName = resources.GetString("btnUp.AccessibleName");
			this.btnUp.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnUp.Anchor")));
			this.btnUp.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnUp.BackgroundImage")));
			this.btnUp.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnUp.Dock")));
			this.btnUp.Enabled = ((bool)(resources.GetObject("btnUp.Enabled")));
			this.btnUp.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnUp.FlatStyle")));
			this.btnUp.Font = ((System.Drawing.Font)(resources.GetObject("btnUp.Font")));
			this.btnUp.Image = ((System.Drawing.Image)(resources.GetObject("btnUp.Image")));
			this.btnUp.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnUp.ImageAlign")));
			this.btnUp.ImageIndex = ((int)(resources.GetObject("btnUp.ImageIndex")));
			this.btnUp.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnUp.ImeMode")));
			this.btnUp.Location = ((System.Drawing.Point)(resources.GetObject("btnUp.Location")));
			this.btnUp.Name = "btnUp";
			this.btnUp.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnUp.RightToLeft")));
			this.btnUp.Size = ((System.Drawing.Size)(resources.GetObject("btnUp.Size")));
			this.btnUp.TabIndex = ((int)(resources.GetObject("btnUp.TabIndex")));
			this.btnUp.Text = resources.GetString("btnUp.Text");
			this.btnUp.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnUp.TextAlign")));
			this.btnUp.Visible = ((bool)(resources.GetObject("btnUp.Visible")));
			this.btnUp.Click += new System.EventHandler(this.btnUp_Click);
			// 
			// btnDown
			// 
			this.btnDown.AccessibleDescription = resources.GetString("btnDown.AccessibleDescription");
			this.btnDown.AccessibleName = resources.GetString("btnDown.AccessibleName");
			this.btnDown.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnDown.Anchor")));
			this.btnDown.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnDown.BackgroundImage")));
			this.btnDown.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnDown.Dock")));
			this.btnDown.Enabled = ((bool)(resources.GetObject("btnDown.Enabled")));
			this.btnDown.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnDown.FlatStyle")));
			this.btnDown.Font = ((System.Drawing.Font)(resources.GetObject("btnDown.Font")));
			this.btnDown.Image = ((System.Drawing.Image)(resources.GetObject("btnDown.Image")));
			this.btnDown.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnDown.ImageAlign")));
			this.btnDown.ImageIndex = ((int)(resources.GetObject("btnDown.ImageIndex")));
			this.btnDown.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnDown.ImeMode")));
			this.btnDown.Location = ((System.Drawing.Point)(resources.GetObject("btnDown.Location")));
			this.btnDown.Name = "btnDown";
			this.btnDown.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnDown.RightToLeft")));
			this.btnDown.Size = ((System.Drawing.Size)(resources.GetObject("btnDown.Size")));
			this.btnDown.TabIndex = ((int)(resources.GetObject("btnDown.TabIndex")));
			this.btnDown.Text = resources.GetString("btnDown.Text");
			this.btnDown.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnDown.TextAlign")));
			this.btnDown.Visible = ((bool)(resources.GetObject("btnDown.Visible")));
			this.btnDown.Click += new System.EventHandler(this.btnDown_Click);
			// 
			// lblRootCaption
			// 
			this.lblRootCaption.AccessibleDescription = resources.GetString("lblRootCaption.AccessibleDescription");
			this.lblRootCaption.AccessibleName = resources.GetString("lblRootCaption.AccessibleName");
			this.lblRootCaption.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblRootCaption.Anchor")));
			this.lblRootCaption.AutoSize = ((bool)(resources.GetObject("lblRootCaption.AutoSize")));
			this.lblRootCaption.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblRootCaption.Dock")));
			this.lblRootCaption.Enabled = ((bool)(resources.GetObject("lblRootCaption.Enabled")));
			this.lblRootCaption.Font = ((System.Drawing.Font)(resources.GetObject("lblRootCaption.Font")));
			this.lblRootCaption.Image = ((System.Drawing.Image)(resources.GetObject("lblRootCaption.Image")));
			this.lblRootCaption.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblRootCaption.ImageAlign")));
			this.lblRootCaption.ImageIndex = ((int)(resources.GetObject("lblRootCaption.ImageIndex")));
			this.lblRootCaption.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblRootCaption.ImeMode")));
			this.lblRootCaption.Location = ((System.Drawing.Point)(resources.GetObject("lblRootCaption.Location")));
			this.lblRootCaption.Name = "lblRootCaption";
			this.lblRootCaption.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblRootCaption.RightToLeft")));
			this.lblRootCaption.Size = ((System.Drawing.Size)(resources.GetObject("lblRootCaption.Size")));
			this.lblRootCaption.TabIndex = ((int)(resources.GetObject("lblRootCaption.TabIndex")));
			this.lblRootCaption.Text = resources.GetString("lblRootCaption.Text");
			this.lblRootCaption.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblRootCaption.TextAlign")));
			this.lblRootCaption.Visible = ((bool)(resources.GetObject("lblRootCaption.Visible")));
			// 
			// tvwCostElement
			// 
			this.tvwCostElement.AccessibleDescription = resources.GetString("tvwCostElement.AccessibleDescription");
			this.tvwCostElement.AccessibleName = resources.GetString("tvwCostElement.AccessibleName");
			this.tvwCostElement.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tvwCostElement.Anchor")));
			this.tvwCostElement.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tvwCostElement.BackgroundImage")));
			this.tvwCostElement.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tvwCostElement.Dock")));
			this.tvwCostElement.Enabled = ((bool)(resources.GetObject("tvwCostElement.Enabled")));
			this.tvwCostElement.Font = ((System.Drawing.Font)(resources.GetObject("tvwCostElement.Font")));
			this.tvwCostElement.HideSelection = false;
			this.tvwCostElement.ImageIndex = ((int)(resources.GetObject("tvwCostElement.ImageIndex")));
			this.tvwCostElement.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tvwCostElement.ImeMode")));
			this.tvwCostElement.Indent = ((int)(resources.GetObject("tvwCostElement.Indent")));
			this.tvwCostElement.ItemHeight = ((int)(resources.GetObject("tvwCostElement.ItemHeight")));
			this.tvwCostElement.Location = ((System.Drawing.Point)(resources.GetObject("tvwCostElement.Location")));
			this.tvwCostElement.Name = "tvwCostElement";
			this.tvwCostElement.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tvwCostElement.RightToLeft")));
			this.tvwCostElement.SelectedImageIndex = ((int)(resources.GetObject("tvwCostElement.SelectedImageIndex")));
			this.tvwCostElement.Size = ((System.Drawing.Size)(resources.GetObject("tvwCostElement.Size")));
			this.tvwCostElement.TabIndex = ((int)(resources.GetObject("tvwCostElement.TabIndex")));
			this.tvwCostElement.Text = resources.GetString("tvwCostElement.Text");
			this.tvwCostElement.Visible = ((bool)(resources.GetObject("tvwCostElement.Visible")));
			this.tvwCostElement.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tvwCostElement_KeyDown);
			this.tvwCostElement.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvwCostElement_AfterSelect);
			// 
			// CostElement
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
			this.Controls.Add(this.lblRootCaption);
			this.Controls.Add(this.btnUp);
			this.Controls.Add(this.btnDown);
			this.Controls.Add(this.btnSearchType);
			this.Controls.Add(this.txtType);
			this.Controls.Add(this.txtName);
			this.Controls.Add(this.txtCode);
			this.Controls.Add(this.lblType);
			this.Controls.Add(this.lblCategoryName);
			this.Controls.Add(this.lblCategoryCode);
			this.Controls.Add(this.btnHelp);
			this.Controls.Add(this.btnClose);
			this.Controls.Add(this.btnEdit);
			this.Controls.Add(this.btnAdd);
			this.Controls.Add(this.btnSave);
			this.Controls.Add(this.btnDelete);
			this.Controls.Add(this.tvwCostElement);
			this.Enabled = ((bool)(resources.GetObject("$this.Enabled")));
			this.Font = ((System.Drawing.Font)(resources.GetObject("$this.Font")));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("$this.ImeMode")));
			this.Location = ((System.Drawing.Point)(resources.GetObject("$this.Location")));
			this.MaximizeBox = false;
			this.MaximumSize = ((System.Drawing.Size)(resources.GetObject("$this.MaximumSize")));
			this.MinimumSize = ((System.Drawing.Size)(resources.GetObject("$this.MinimumSize")));
			this.Name = "CostElement";
			this.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("$this.RightToLeft")));
			this.StartPosition = ((System.Windows.Forms.FormStartPosition)(resources.GetObject("$this.StartPosition")));
			this.Text = resources.GetString("$this.Text");
			this.Closing += new System.ComponentModel.CancelEventHandler(this.CostElement_Closing);
			this.Load += new System.EventHandler(this.CostElement_Load);
			this.ResumeLayout(false);

		}
		#endregion
		
		#region Methods
		
		/// <summary>
		/// Load Cost Element Type from Database for displaying
		/// </summary>
		private void LoadCostElementType()
		{			
			if(htbCostElementType != null)
			{
				htbCostElementType.Clear();
			}
			else
			{
				htbCostElementType = new Hashtable();
			}

			DataSet dtsCostElementType = boCostElement.ListCostElementType();

			if(dtsCostElementType != null)
			{
				foreach(DataRow drow in dtsCostElementType.Tables[0].Rows)
				{
					htbCostElementType.Add(drow[STD_CostElementTypeTable.COSTELEMENTTYPEID_FLD], drow[STD_CostElementTypeTable.DESCRIPTION_FLD]);					
				}
			}
		}

		/// <summary>
		/// Clear form's value
		/// </summary>
		/// <author>Tuan TQ. 08 Jan, 2006</author>
		private void ResetControlsValue()
		{
			txtCode.Text = string.Empty;
			txtCode.Tag  = string.Empty;
			txtName.Text = string.Empty;
			txtName.Tag  = string.Empty;
			txtType.Text = string.Empty;
			txtType.Tag  = string.Empty;		
		}

		/// <summary>
		/// Build Cost Element Tree
		/// </summary>
		/// <author>Tuan TQ. 08 Jan, 2006</author>
		private void LoadCostElementTree()
		{
			dstCostElement = boCostElement.List();

			dstCostElement.Tables[STD_CostElementTable.TABLE_NAME].Columns.Add(CHECKED, typeof(bool));

			TreeNode rootNode = new TreeNode();
			rootNode.Text = lblRootCaption.Text;
			rootNode.Tag = ROOT;

			tvwCostElement.Nodes.Add(rootNode);

			foreach (DataRow drow in dstCostElement.Tables[STD_CostElementTable.TABLE_NAME].Rows)
			{
				if ((drow[CHECKED].ToString().Trim() != TRUE) 
					&& (drow[STD_CostElementTable.PARENTID_FLD] == DBNull.Value)
					)
				{
					TreeNode node = NewNode(drow);
					rootNode.Nodes.Add(node);
					drow[CHECKED] = true;
					BuildTree(dstCostElement, node);
				}
			}
		}		

		/// <summary>
		/// Create cost element tree node & it's child nodes
		/// </summary>
		/// <param name="dstCostElement"></param>
		/// <param name="node"></param>
		/// <author>Tuan TQ. 08 Jan, 2006</author>
		private void BuildTree(DataSet dstCostElement, TreeNode node)
		{
			foreach (DataRow drNode in dstCostElement.Tables[0].Rows)
			{
				if (drNode[CHECKED].ToString().Trim() != TRUE)
				{
					string sqlSelect = STD_CostElementTable.PARENTID_FLD + " = " + ((STD_CostElementVO) node.Tag).CostElementID;

					DataRow[] arrChildren = dstCostElement.Tables[STD_CostElementTable.TABLE_NAME].Select(sqlSelect, "ParentID, OrderNo ASC");

					foreach(DataRow drChild in arrChildren)
					{
						if (drChild[CHECKED].ToString().Trim() != TRUE)
						{
							TreeNode childNode = new TreeNode();
							childNode = NewNode(drChild);
							node.Nodes.Add(childNode);

							drChild[CHECKED] = true;
							
							//recusive calling
							BuildTree(dstCostElement, childNode);
						}
					}
				}
			}
		}
		
		/// <summary>
		/// Create new node based on a data row
		/// </summary>
		/// <param name="row"></param>
		/// <returns></returns>
		/// <author>Tuan TQ. 08 Jan, 2006</author>
		private TreeNode NewNode(DataRow row)
		{	
			//create cost element
			STD_CostElementVO voCostElement = new STD_CostElementVO();
			voCostElement.CostElementID = int.Parse(row[STD_CostElementTable.COSTELEMENTID_FLD].ToString());
			voCostElement.Code = row[STD_CostElementTable.CODE_FLD].ToString();
			voCostElement.Name = row[STD_CostElementTable.NAME_FLD].ToString();
			voCostElement.CostElementTypeID = int.Parse(row[STD_CostElementTable.COSTELEMENTTYPEID_FLD].ToString());
			voCostElement.OrderNo = int.Parse(row[STD_CostElementTable.ORDERNO_FLD].ToString());
			voCostElement.ParentID = row[STD_CostElementTable.PARENTID_FLD].Equals(DBNull.Value)? 0: int.Parse(row[STD_CostElementTable.PARENTID_FLD].ToString());
			voCostElement.IsLeaf = row[STD_CostElementTable.ISLEAF_FLD].Equals(DBNull.Value)? 0: int.Parse(row[STD_CostElementTable.ISLEAF_FLD].ToString());

			//create node then assign values
			TreeNode node = new TreeNode();
			node.Tag = voCostElement;
			node.Text = voCostElement.Name + Constants.WHITE_SPACE + Constants.OPEN_SBRACKET + voCostElement.Code + Constants.CLOSE_SBRACKET;

			return node;
		}

		/// <summary>
		/// Bind data from selected node to controls
		/// </summary>
		/// <param name="pSelectNode"></param>
		/// <author>Tuan TQ. 08 Jan, 2006</author>
		private void BindDataFromNodeToForm(TreeNode pSelectNode)
		{
			if(pSelectNode == null)
			{
				ResetControlsValue();
				return;
			}
			
			if(pSelectNode.Tag == null)
			{
				ResetControlsValue();
				return;
			}

			STD_CostElementVO voCostElement = (STD_CostElementVO)pSelectNode.Tag;

			txtCode.Text = voCostElement.Code;
			txtCode.Tag = voCostElement.CostElementID;
			txtName.Text = voCostElement.Name;
			txtName.Tag = voCostElement.ParentID;
            
			object objValue = htbCostElementType[(byte)voCostElement.CostElementTypeID];
			if(objValue != null)
			{
				txtType.Text = objValue.ToString();
			}
			else
			{
				txtType.Text = string.Empty;
			}
			
			txtType.Tag =  voCostElement.CostElementTypeID;
		}
		
		/// <summary>
		/// Lock controls
		/// </summary>
		/// <param name="pblnLock"></param>
		/// <author>Tuan TQ. 08 Jan, 2006</author>
		private void LockControl(bool pblnLock)
		{
			bool blnIsCostElementRoot = true;
			bool blnIsTreeRoot = true;
			bool blnIsLeaf = true;			
			bool blnIsMaterial = false;
	
			if(tvwCostElement.SelectedNode != null)
			{
				if(tvwCostElement.SelectedNode.Tag.ToString().Trim() != ROOT)
				{					
					blnIsLeaf = (tvwCostElement.SelectedNode.Nodes.Count == 0);
					blnIsCostElementRoot = (((STD_CostElementVO)tvwCostElement.SelectedNode.Tag).ParentID == 0);
					blnIsTreeRoot = false;					
				}
				else
				{
					blnIsCostElementRoot = false;
					blnIsLeaf = false;
				}

				btnUp.Enabled = pblnLock && !blnIsTreeRoot && (tvwCostElement.SelectedNode.PrevNode != null);
				btnDown.Enabled = pblnLock&& !blnIsTreeRoot && (tvwCostElement.SelectedNode.NextNode != null);
			}
			
			btnDelete.Enabled = pblnLock && blnIsLeaf && !blnIsTreeRoot;
			btnEdit.Enabled = pblnLock && !blnIsTreeRoot;
			
			btnAdd.Enabled = pblnLock;

			txtCode.Enabled = !pblnLock;
			txtName.Enabled = !pblnLock;
			
			if(!blnIsTreeRoot)
			{
				object objValue = htbCostElementType[(byte)((STD_CostElementVO)tvwCostElement.SelectedNode.Tag).CostElementTypeID];
				if(objValue != null)
				{
					txtType.Text = objValue.ToString();
				}
				else
				{
					txtType.Text = string.Empty;
				}
				
				txtType.Tag = ((STD_CostElementVO)tvwCostElement.SelectedNode.Tag).CostElementTypeID;
			}

			txtType.Enabled       = !pblnLock && (blnIsTreeRoot || (blnIsCostElementRoot && (enumAction == EnumAction.Edit)));
			btnSearchType.Enabled = !pblnLock && (blnIsTreeRoot || (blnIsCostElementRoot && (enumAction == EnumAction.Edit)));

			btnSave.Enabled = !pblnLock;			
		}
		
		/// <summary>
		/// Validate data in controls
		/// </summary>
		/// <returns></returns>
		/// <author>Tuan TQ. 08 Jan, 2006</author>
		private bool ValidateData()
		{
			if (FormControlComponents.CheckMandatory(txtCode))
			{
				PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Exclamation);
				txtCode.Focus();
				return false;
			}

			if (FormControlComponents.CheckMandatory(txtName))
			{
				PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Exclamation);
				txtName.Focus();
				return false;
			}

			if (FormControlComponents.CheckMandatory(txtType))
			{
				PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Exclamation);
				txtType.Focus();
				return false;
			}
			
			int intCostElementID = 0;

			if(txtCode.Tag != null)
			{
				if(txtCode.Tag.ToString().Trim() != string.Empty)
				{
					intCostElementID = int.Parse(txtCode.Tag.ToString());
				}
			}
			
			if(boCostElement.IsCostElementDuplicate(STD_CostElementTable.CODE_FLD, txtCode.Text.Trim(), intCostElementID))
			{
				PCSMessageBox.Show(ErrorCode.DUPLICATE_KEY, MessageBoxIcon.Exclamation);
				txtCode.Focus();
				return false;
			}

			if(boCostElement.IsCostElementDuplicate(STD_CostElementTable.NAME_FLD, txtName.Text.Trim(), intCostElementID))
			{
				PCSMessageBox.Show(ErrorCode.DUPLICATE_KEY, MessageBoxIcon.Exclamation);
				txtName.Focus();
				return false;
			}
			

			if(enumAction == EnumAction.Edit)
			{
				//Only check duplicate type of root nodes
				if( ((STD_CostElementVO)tvwCostElement.SelectedNode.Tag).ParentID <= 0)
				{
					if(boCostElement.IsElementTypeDuplicate(txtType.Tag.ToString(), intCostElementID))
					{
						PCSMessageBox.Show(ErrorCode.DUPLICATE_KEY, MessageBoxIcon.Exclamation);
						txtType.Focus();
						return false;
					}
				}

				if(tvwCostElement.SelectedNode.Nodes.Count != 0 && (int.Parse(txtType.Tag.ToString()) == (int)CostElementType.Material))
				{
					PCSMessageBox.Show(ErrorCode.MESSAGE_CANNOT_ADD_CHILD_IN_MATERIAL_COST_ELEMENT, MessageBoxIcon.Exclamation);
					txtType.Focus();
					return false;
				}
			}
			else if(tvwCostElement.SelectedNode.Tag.ToString() == ROOT)
			{
				if(boCostElement.IsElementTypeDuplicate(txtType.Tag.ToString(), intCostElementID))
				{
					PCSMessageBox.Show(ErrorCode.DUPLICATE_KEY, MessageBoxIcon.Exclamation);
					txtType.Focus();
					return false;
				}			
			}

			return true;
		}

		/// <summary>
		/// Update information of selected node
		/// </summary>
		/// <author>Tuan TQ. 08 Jan, 2006</author>
		private void UpdateNodeIntoTree(STD_CostElementVO pvoCostElement)
		{
			TreeNode trnSelectedNode;

			//edit node
			if (enumAction == EnumAction.Edit)
			{
				trnSelectedNode = tvwCostElement.SelectedNode;				
			}
			else
			{
				trnSelectedNode = new TreeNode();				
				tvwCostElement.SelectedNode.Nodes.Add(trnSelectedNode);
			}

			//assign node value
			trnSelectedNode.Text = pvoCostElement.Name + Constants.WHITE_SPACE + Constants.OPEN_SBRACKET + pvoCostElement.Code + Constants.CLOSE_SBRACKET;;
			trnSelectedNode.Tag = pvoCostElement;
			
			tvwCostElement.SelectedNode = trnSelectedNode;
		}		

		/// <summary>
		/// Update type of child elements
		/// </summary>
		/// <param name="prootNode"></param>
		/// <param name="pintElementTypeID"></param>
		private void UpdateElementType(TreeNode prootNode, int pintElementTypeID)
		{
			foreach(TreeNode node in prootNode.Nodes)
			{
				STD_CostElementVO voTemp = (STD_CostElementVO)node.Tag;

				DataRow[] arrFoundRows = dstCostElement.Tables[0].Select(STD_CostElementTable.COSTELEMENTID_FLD + "=" + voTemp.CostElementID);
				if(arrFoundRows.Length != 0)
				{
					//update element tye in data table
					arrFoundRows[0][STD_CostElementTable.COSTELEMENTTYPEID_FLD] = pintElementTypeID;
					
					//change element of node 
					voTemp.CostElementTypeID = pintElementTypeID;
					node.Tag = voTemp;

					//recursive calling
					UpdateElementType(node, pintElementTypeID);
				}
			}
		}

		/// <summary>
		/// Update Order & Leaf information of elements
		/// </summary>
		/// <param name="prootNode"></param>
		/// <param name="pintBeginOrder"></param>
		/// <author>Tuan TQ. 08 Jan, 2006</author>
		private void UpdateOrderAndLeafInfo(TreeNode prootNode, ref int pintBeginOrder)
		{
			foreach(TreeNode node in prootNode.Nodes)
			{
				STD_CostElementVO voTemp = (STD_CostElementVO)node.Tag;

				DataRow[] arrFoundRows = dstCostElement.Tables[0].Select(STD_CostElementTable.COSTELEMENTID_FLD + "=" + voTemp.CostElementID);
				if(arrFoundRows.Length != 0)
				{
					//update order in data table
					arrFoundRows[0][STD_CostElementTable.ORDERNO_FLD] = ++pintBeginOrder;					
					//update leaf property
					arrFoundRows[0][STD_CostElementTable.ISLEAF_FLD] = (node.Nodes.Count > 0)? 0 : 1;
 
					//change order of node 
					voTemp.OrderNo = pintBeginOrder;
					voTemp.IsLeaf = (node.Nodes.Count > 0) ? 0 : 1;
					node.Tag = voTemp;

					//recursive calling
					UpdateOrderAndLeafInfo(node, ref pintBeginOrder);
				}
			}
		}

		/// <summary>
		/// Fill related data on controls when select Cost Element Type
		/// </summary>
		/// <param name="pblnAlwayShowDialog"></param>
		/// <author>Tuan TQ. 08 Jan, 2006</author>
		private bool SelectCostElementType(string pstrMethodName, bool pblnAlwayShowDialog)
		{			
			DataRowView drwResult = null;
			
			//Call OpenSearchForm for selecting Cost Element Type
			drwResult = FormControlComponents.OpenSearchForm(STD_CostElementTypeTable.TABLE_NAME, STD_CostElementTypeTable.DESCRIPTION_FLD, txtType.Text, null, pblnAlwayShowDialog);
			
			// If matched searching condition, fill values to form's controls
			if (drwResult != null)
			{
				//if data ok and in edit mode --> type has been changed
				blnChangeType = (enumAction == EnumAction.Edit) && (txtType.Tag != drwResult[STD_CostElementTypeTable.COSTELEMENTTYPEID_FLD]);

				txtType.Text = drwResult[STD_CostElementTypeTable.DESCRIPTION_FLD].ToString();
				txtType.Tag = drwResult[STD_CostElementTypeTable.COSTELEMENTTYPEID_FLD];
			}
			else
			{
				if(!pblnAlwayShowDialog)
				{	
					//Reset type's info 
					txtType.Text = string.Empty;
					txtType.Tag = string.Empty;
					txtType.Focus();
					return false;
				}				
			}

			return true;			
		}		
		
		/// <summary>
		/// Move selected node to next/previous position
		/// </summary>
		/// <param name="pnodeSelectedNode"></param>
		/// <param name="pblnMoveUp"></param>
		private void MoveNodeInTree(TreeNode pnodeSelectedNode, bool pblnMoveUp)
		{
			//return if selected node is null
			if (pnodeSelectedNode == null)
			{
				return;
			}

			//return if selected node is tree root
			if((pnodeSelectedNode.Tag.ToString() == ROOT) 
			 ||(pnodeSelectedNode.Parent == null))
			{
				return;
			}

			TreeNode nodeNextNode;
			int intNewIndex;

			if(pblnMoveUp)
			{
				nodeNextNode = pnodeSelectedNode.PrevNode;
				intNewIndex  = pnodeSelectedNode.Index - 1;
			}
			else
			{
				nodeNextNode = pnodeSelectedNode.NextNode;
				intNewIndex  = pnodeSelectedNode.Index + 1;
			}
			
			//return if next node is null
			if(nodeNextNode == null || intNewIndex < 0)
			{
				return;
			}
			
			// moving
			TreeNode nodeParentNode = pnodeSelectedNode.Parent;
			pnodeSelectedNode.Remove();			
			nodeParentNode.Nodes.Insert(intNewIndex, pnodeSelectedNode);

			// set focus on selected on
			tvwCostElement.SelectedNode = pnodeSelectedNode;
			tvwCostElement.Refresh();
			tvwCostElement.Focus();

			blnElementOrderChange = true;			
		}

		/// <summary>
		/// Assign values from form to VO
		/// </summary>
		/// <returns></returns>
		private STD_CostElementVO GetDataFromControlsToVO()
		{
			STD_CostElementVO voTempCostElement = new STD_CostElementVO();

			voTempCostElement.Code = txtCode.Text.Trim();
			voTempCostElement.Name = txtName.Text.Trim();
			voTempCostElement.CostElementTypeID = int.Parse(txtType.Tag.ToString());

			return voTempCostElement;
		}

		#endregion Methods
		
		#region Event Processing
		
		private void CostElement_Load(object sender, System.EventArgs e)
		{
			string METHOD_NAME = THIS + ".CostElement_Load()";
			try
			{
				//Set authorization for user
				Security objSecurity = new Security();

				this.Name = THIS;

				if (objSecurity.SetRightForUserOnForm(this, SystemProperty.UserName) == 0)
				{
					// You don't have the right to view this item
					PCSMessageBox.Show(ErrorCode.MESSAGE_YOU_HAVE_NO_RIGHT_TO_VIEW, MessageBoxIcon.Warning);
					this.Close();
					return;
				}

				//init variables
				boCostElement = new CostElementBO();
				enumAction = EnumAction.Default;
				blnDataIsValid = false;

				//Load form
				LoadCostElementType();
				LoadCostElementTree();
				ResetControlsValue();			

				tvwCostElement.Focus();
				tvwCostElement.SelectedNode = tvwCostElement.Nodes[0];
				tvwCostElement.Nodes[0].Expand();

				LockControl(true);
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

		private void CostElement_Activated(object sender, System.EventArgs e)
		{
			string METHOD_NAME = THIS + ".CostElement_Activated()";
			try
			{
				LoadCostElementType();
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


		private void btnAdd_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnAdd_Click()";

			try
			{	
				//Check if none node was selected
				if(tvwCostElement.SelectedNode == null)
				{
					return;
				}

				//Check if selected node was used
				if(tvwCostElement.SelectedNode.Tag == null)
				{
					return;
				}			
				
				if(tvwCostElement.SelectedNode.Tag.ToString() != ROOT)
				{
					//Check if element type is material --> cannot add child
					if( ((STD_CostElementVO)tvwCostElement.SelectedNode.Tag).CostElementTypeID == (int)CostElementType.Material)
					{
						PCSMessageBox.Show(ErrorCode.MESSAGE_CANNOT_ADD_CHILD_IN_MATERIAL_COST_ELEMENT, MessageBoxIcon.Exclamation);
						return;
					}

					//STD_CostElementTypeVO voElementType = (STD_CostElementTypeVO)boCostElement.GetElementTypeVO(((STD_CostElementVO)tvwCostElement.SelectedNode.Tag).CostElementTypeID);
					//if(voElementType != null)
					//{
					//	if(((STD_CostElementVO)tvwCostElement.SelectedNode.Tag).CostElementTypeID == (int)CostElementType.Material)
					//	{
					//		PCSMessageBox.Show(ErrorCode.MESSAGE_CANNOT_ADD_CHILD_IN_MATERIAL_COST_ELEMENT, MessageBoxIcon.Exclamation);
					//		return;
					//	}	
					//}
					
					//Check if element has been used
					bool blnIsUsed = boCostElement.IsCostElementUsed(((STD_CostElementVO)tvwCostElement.SelectedNode.Tag).CostElementID);

					if(blnIsUsed)
					{
						PCSMessageBox.Show(ErrorCode.MESSAGE_CANNOT_ADD_CHILDREN_IN_COST_ELEMENT, MessageBoxIcon.Exclamation);
						return;
					}
				}

				enumAction = EnumAction.Add;
				ResetControlsValue();
				LockControl(false);

				//focus to txtCode 
				txtCode.Focus();
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
				//exit if not in update mode
				if(enumAction == EnumAction.Default)
				{
					return;
				}
				
				//Validate data
				blnDataIsValid = ValidateData();

				if(!blnDataIsValid)
				{
					//reset change order status
					blnElementOrderChange = false;
					return;
				}
				
				STD_CostElementVO voCostElement = GetDataFromControlsToVO();

				switch(enumAction)
				{
					case EnumAction.Edit:
						voCostElement.CostElementID =((STD_CostElementVO)(tvwCostElement.SelectedNode.Tag)).CostElementID;
						voCostElement.ParentID = ((STD_CostElementVO)(tvwCostElement.SelectedNode.Tag)).ParentID;
						voCostElement.OrderNo = ((STD_CostElementVO)(tvwCostElement.SelectedNode.Tag)).OrderNo;
						voCostElement.IsLeaf = ((STD_CostElementVO)(tvwCostElement.SelectedNode.Tag)).IsLeaf;

						boCostElement.Update(voCostElement);
						
						if(blnChangeType)
						{							
							UpdateElementType(tvwCostElement.SelectedNode, voCostElement.CostElementTypeID);
							boCostElement.UpdateDataSet(dstCostElement);
							
							//reload dataset
							dstCostElement = boCostElement.List();
							blnChangeType = false;
						}

						//Update element
						UpdateNodeIntoTree(voCostElement);
	
						break;

					case EnumAction.Add:
						
						if(tvwCostElement.SelectedNode.Tag.ToString().Trim() == ROOT)
						{
							voCostElement.ParentID = 0;							
						}
						else
						{
							voCostElement.ParentID = ((STD_CostElementVO) tvwCostElement.SelectedNode.Tag).CostElementID;							
						}
						voCostElement.IsLeaf = 1;
						voCostElement.CostElementID = boCostElement.AddAndReturnID(voCostElement);

						//Update element
						UpdateNodeIntoTree(voCostElement);
						
						//reload dataset
						dstCostElement = boCostElement.List();

						//update order
						int intBeginOrder = 0;					
						UpdateOrderAndLeafInfo(tvwCostElement.Nodes[0], ref intBeginOrder);
						boCostElement.UpdateDataSet(dstCostElement);				
						break;
				}
				
				//reload dataset
				dstCostElement = boCostElement.List();

				//show successfull message
				PCSMessageBox.Show(ErrorCode.MESSAGE_AFTER_SAVE_DATA);

				//reset value
				enumAction = EnumAction.Default;				
				LockControl(true);

				tvwCostElement.Focus();
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

		private void btnEdit_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnEdit_Click()";

			try
			{		
				enumAction = EnumAction.Edit;

				BindDataFromNodeToForm(tvwCostElement.SelectedNode);
				LockControl(false);

				txtCode.Focus();				
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

		private void btnDelete_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnDelete_Click()";

			try
			{				
				if (tvwCostElement.SelectedNode.Tag.ToString() == ROOT)
				{
					return;
				}
				
				if(boCostElement.IsCostElementUsed(((STD_CostElementVO)tvwCostElement.SelectedNode.Tag).CostElementID))
				{
					PCSMessageBox.Show(ErrorCode.MESSAGE_CANNOT_DELETE_COST_ELEMENT, MessageBoxIcon.Error);
					return;
				}
				
				//Return if choose No
				if (PCSMessageBox.Show(ErrorCode.MESSAGE_DELETE_RECORD, MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
				{
					return;
				}
				
				//Delete selected Cost Element
				STD_CostElementVO voCostElement = (STD_CostElementVO)tvwCostElement.SelectedNode.Tag;
				
				//Get sibling node
				TreeNode nodeNext = tvwCostElement.SelectedNode.NextNode;
				if(nodeNext == null)
				{
					nodeNext = tvwCostElement.SelectedNode.PrevNode;
				}

				//Remove node from tree
				tvwCostElement.SelectedNode.Remove();
				if(nodeNext != null)
				{
					tvwCostElement.SelectedNode = nodeNext;
				}
				
				tvwCostElement.Refresh();
				tvwCostElement.Focus();

				//update order
				int intBeginOrder = 0;					
				UpdateOrderAndLeafInfo(tvwCostElement.Nodes[0], ref intBeginOrder);
				
				//Delete from dataset
				DataRow[] arrFoundRow = dstCostElement.Tables[0].Select(STD_CostElementTable.COSTELEMENTID_FLD + "=" + voCostElement.CostElementID);
				if(arrFoundRow.Length != 0)
				{
					arrFoundRow[0].Delete();
				}

				//Update changes in dataset
				boCostElement.UpdateDataSet(dstCostElement);									

				//Relock control
				LockControl(true);				
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
		
		
		private void btnUp_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnUp_Click()";

			try
			{
				MoveNodeInTree(tvwCostElement.SelectedNode, true);
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

		private void btnDown_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnDown_Click()";

			try
			{
				MoveNodeInTree(tvwCostElement.SelectedNode, false);
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

		private void btnHelp_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnHelp_Click()";

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

		private void btnClose_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnClose_Click()";

			try
			{
				this.Close();
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

		private void btnSearchType_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnSearchType_Click()";

			try
			{
				SelectCostElementType(METHOD_NAME, true);
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
		
		private void tvwCostElement_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			const string METHOD_NAME = THIS + ".tvwCostElement_KeyDown()";
			try
			{
				if (e.KeyCode == Keys.Enter)
				{					
					if (tvwCostElement.SelectedNode != null)
					{
						if (tvwCostElement.SelectedNode.IsExpanded)
						{
							tvwCostElement.SelectedNode.Collapse();
						}
						else
						{
							tvwCostElement.SelectedNode.Expand();
						}
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
		
		private void tvwCostElement_AfterSelect(object sender, TreeViewEventArgs e)
		{
			const string METHOD_NAME = THIS + ".tvwCostElement_AfterSelect()";

			try
			{		
				if (tvwCostElement.SelectedNode.Tag.ToString().Trim() != ROOT)
				{
					BindDataFromNodeToForm(tvwCostElement.SelectedNode);				
				}
				else
				{
					ResetControlsValue();					
				}
				
				enumAction = EnumAction.Default;

				LockControl(true);
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
		
		private void txtType_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtType_KeyDown()";

			try
			{
				if ((e.KeyCode == Keys.F4) && (btnSearchType.Enabled))
				{
					SelectCostElementType(METHOD_NAME, true);
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

		private void txtType_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtType_Validating()";

			try
			{
				//exit if empty				
				if(txtType.Text.Length == 0)
				{				
					return;
				}
				else if(!txtType.Modified)
				{
					return;
				}				

				e.Cancel = !SelectCostElementType(METHOD_NAME, false);				
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
		
		private void CostElement_Closing(object sender, CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".CostElement_Closing()";
			try
			{
				//update order
				if(blnElementOrderChange)
				{					
					int intBeginOrder = 0;					
					UpdateOrderAndLeafInfo(tvwCostElement.Nodes[0], ref intBeginOrder);
					boCostElement.UpdateDataSet(dstCostElement);	
					blnElementOrderChange = false;
				}

				// if the form has been changed then ask to store database
				if(enumAction != EnumAction.Default)
				{
					DialogResult enumDialog = PCSMessageBox.Show(ErrorCode.MESSAGE_QUESTION_STORE_INTO_DATABASE, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
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

		#endregion Event Processing			
	}
}