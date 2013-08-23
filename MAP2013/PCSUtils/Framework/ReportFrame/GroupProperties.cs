using System;
using System.Data;
using System.Collections;
using System.Windows.Forms;
using PCSComUtils.Common;
using PCSComUtils.PCSExc;
using PCSUtils.Log;
using PCSUtils.Utils;
using PCSComUtils.Framework.ReportFrame.BO;
using PCSComUtils.Framework.ReportFrame.DS;

namespace PCSUtils.Framework.ReportFrame
{
	/// <summary>
	/// Summary description for GroupProperties.
	/// </summary>
	public class GroupProperties : System.Windows.Forms.Form
	{
		#region controls

		private GroupBox grpFieldProperties;
		private TextBox txtCaptionVN;
		private Label lblCaptionVN;
		private TextBox txtCaptionEN;
		private Label lblCaptionEN;
		private TextBox txtCaptionJP;
		private Label lblCaptionJP;
		private Button btnSave;
		private Button btnDelete;
		private Button btnHelp;
		private Button btnClose;
		private TreeView tvwFieldGroups;
		private ListBox lstSelected;
		private ListBox lstAvailable;
		private TextBox txtOrder;
		private Label lblOrder;
		private ComboBox cboLevel;
		private Label lblLevel;
		private TextBox txtGroupCode;
		private Label lblGroupCode;
		private Button btnRemove;
		private Button btnSelect;
		private Label lblAvailable;
		private Button btnAdd;
		private Label lblSelected;
		private Button btnMoveDown;
		private Button btnMoveUp;
		private Button btnRemoveAll;
		private Button btnSelectAll;

		#endregion

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		#region variables

		private const string THIS = "PCSUtils.Framework.ReportFrame.GroupProperties";
		private EnumAction mFormMode = EnumAction.Default;
		private Button btnEdit;
		private sys_ReportVO mReport;
		private GroupPropertiesBO boFieldGroup;
		private Sys_FieldGroupVO voFieldGroup;
		private DataTable dtbFieldGroup = new DataTable(Sys_FieldGroupTable.TABLE_NAME);
		ArrayList arrSelected = new ArrayList();
		ArrayList arrAvailable = new ArrayList();
		DataTable dtbAvailableField = new DataTable(sys_ReportFieldsTable.TABLE_NAME);
		TreeNode objSelectedNode = null;

		public sys_ReportVO Report
		{
			get { return mReport; }
			set { mReport = value; }
		}

		#endregion

		public GroupProperties()
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(GroupProperties));
			this.tvwFieldGroups = new System.Windows.Forms.TreeView();
			this.lstSelected = new System.Windows.Forms.ListBox();
			this.lstAvailable = new System.Windows.Forms.ListBox();
			this.grpFieldProperties = new System.Windows.Forms.GroupBox();
			this.btnRemoveAll = new System.Windows.Forms.Button();
			this.btnSelectAll = new System.Windows.Forms.Button();
			this.lblSelected = new System.Windows.Forms.Label();
			this.txtOrder = new System.Windows.Forms.TextBox();
			this.lblOrder = new System.Windows.Forms.Label();
			this.txtCaptionVN = new System.Windows.Forms.TextBox();
			this.lblCaptionVN = new System.Windows.Forms.Label();
			this.txtCaptionEN = new System.Windows.Forms.TextBox();
			this.lblCaptionEN = new System.Windows.Forms.Label();
			this.txtCaptionJP = new System.Windows.Forms.TextBox();
			this.lblCaptionJP = new System.Windows.Forms.Label();
			this.cboLevel = new System.Windows.Forms.ComboBox();
			this.lblLevel = new System.Windows.Forms.Label();
			this.txtGroupCode = new System.Windows.Forms.TextBox();
			this.lblGroupCode = new System.Windows.Forms.Label();
			this.btnRemove = new System.Windows.Forms.Button();
			this.btnSelect = new System.Windows.Forms.Button();
			this.lblAvailable = new System.Windows.Forms.Label();
			this.btnSave = new System.Windows.Forms.Button();
			this.btnDelete = new System.Windows.Forms.Button();
			this.btnAdd = new System.Windows.Forms.Button();
			this.btnHelp = new System.Windows.Forms.Button();
			this.btnClose = new System.Windows.Forms.Button();
			this.btnMoveDown = new System.Windows.Forms.Button();
			this.btnMoveUp = new System.Windows.Forms.Button();
			this.btnEdit = new System.Windows.Forms.Button();
			this.grpFieldProperties.SuspendLayout();
			this.SuspendLayout();
			// 
			// tvwFieldGroups
			// 
			this.tvwFieldGroups.AccessibleDescription = resources.GetString("tvwFieldGroups.AccessibleDescription");
			this.tvwFieldGroups.AccessibleName = resources.GetString("tvwFieldGroups.AccessibleName");
			this.tvwFieldGroups.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tvwFieldGroups.Anchor")));
			this.tvwFieldGroups.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tvwFieldGroups.BackgroundImage")));
			this.tvwFieldGroups.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tvwFieldGroups.Dock")));
			this.tvwFieldGroups.Enabled = ((bool)(resources.GetObject("tvwFieldGroups.Enabled")));
			this.tvwFieldGroups.Font = ((System.Drawing.Font)(resources.GetObject("tvwFieldGroups.Font")));
			this.tvwFieldGroups.ImageIndex = ((int)(resources.GetObject("tvwFieldGroups.ImageIndex")));
			this.tvwFieldGroups.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tvwFieldGroups.ImeMode")));
			this.tvwFieldGroups.Indent = ((int)(resources.GetObject("tvwFieldGroups.Indent")));
			this.tvwFieldGroups.ItemHeight = ((int)(resources.GetObject("tvwFieldGroups.ItemHeight")));
			this.tvwFieldGroups.Location = ((System.Drawing.Point)(resources.GetObject("tvwFieldGroups.Location")));
			this.tvwFieldGroups.Name = "tvwFieldGroups";
			this.tvwFieldGroups.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tvwFieldGroups.RightToLeft")));
			this.tvwFieldGroups.SelectedImageIndex = ((int)(resources.GetObject("tvwFieldGroups.SelectedImageIndex")));
			this.tvwFieldGroups.Size = ((System.Drawing.Size)(resources.GetObject("tvwFieldGroups.Size")));
			this.tvwFieldGroups.TabIndex = ((int)(resources.GetObject("tvwFieldGroups.TabIndex")));
			this.tvwFieldGroups.Text = resources.GetString("tvwFieldGroups.Text");
			this.tvwFieldGroups.Visible = ((bool)(resources.GetObject("tvwFieldGroups.Visible")));
			this.tvwFieldGroups.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tvwFieldGroups_KeyDown);
			this.tvwFieldGroups.DoubleClick += new System.EventHandler(this.tvwFieldGroups_DoubleClick);
			this.tvwFieldGroups.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvwFieldGroups_AfterSelect);
			// 
			// lstSelected
			// 
			this.lstSelected.AccessibleDescription = resources.GetString("lstSelected.AccessibleDescription");
			this.lstSelected.AccessibleName = resources.GetString("lstSelected.AccessibleName");
			this.lstSelected.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lstSelected.Anchor")));
			this.lstSelected.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("lstSelected.BackgroundImage")));
			this.lstSelected.ColumnWidth = ((int)(resources.GetObject("lstSelected.ColumnWidth")));
			this.lstSelected.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lstSelected.Dock")));
			this.lstSelected.Enabled = ((bool)(resources.GetObject("lstSelected.Enabled")));
			this.lstSelected.Font = ((System.Drawing.Font)(resources.GetObject("lstSelected.Font")));
			this.lstSelected.HorizontalExtent = ((int)(resources.GetObject("lstSelected.HorizontalExtent")));
			this.lstSelected.HorizontalScrollbar = ((bool)(resources.GetObject("lstSelected.HorizontalScrollbar")));
			this.lstSelected.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lstSelected.ImeMode")));
			this.lstSelected.IntegralHeight = ((bool)(resources.GetObject("lstSelected.IntegralHeight")));
			this.lstSelected.ItemHeight = ((int)(resources.GetObject("lstSelected.ItemHeight")));
			this.lstSelected.Location = ((System.Drawing.Point)(resources.GetObject("lstSelected.Location")));
			this.lstSelected.Name = "lstSelected";
			this.lstSelected.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lstSelected.RightToLeft")));
			this.lstSelected.ScrollAlwaysVisible = ((bool)(resources.GetObject("lstSelected.ScrollAlwaysVisible")));
			this.lstSelected.Size = ((System.Drawing.Size)(resources.GetObject("lstSelected.Size")));
			this.lstSelected.TabIndex = ((int)(resources.GetObject("lstSelected.TabIndex")));
			this.lstSelected.Visible = ((bool)(resources.GetObject("lstSelected.Visible")));
			this.lstSelected.SelectedIndexChanged += new System.EventHandler(this.lstSelected_SelectedIndexChanged);
			// 
			// lstAvailable
			// 
			this.lstAvailable.AccessibleDescription = resources.GetString("lstAvailable.AccessibleDescription");
			this.lstAvailable.AccessibleName = resources.GetString("lstAvailable.AccessibleName");
			this.lstAvailable.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lstAvailable.Anchor")));
			this.lstAvailable.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("lstAvailable.BackgroundImage")));
			this.lstAvailable.ColumnWidth = ((int)(resources.GetObject("lstAvailable.ColumnWidth")));
			this.lstAvailable.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lstAvailable.Dock")));
			this.lstAvailable.Enabled = ((bool)(resources.GetObject("lstAvailable.Enabled")));
			this.lstAvailable.Font = ((System.Drawing.Font)(resources.GetObject("lstAvailable.Font")));
			this.lstAvailable.HorizontalExtent = ((int)(resources.GetObject("lstAvailable.HorizontalExtent")));
			this.lstAvailable.HorizontalScrollbar = ((bool)(resources.GetObject("lstAvailable.HorizontalScrollbar")));
			this.lstAvailable.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lstAvailable.ImeMode")));
			this.lstAvailable.IntegralHeight = ((bool)(resources.GetObject("lstAvailable.IntegralHeight")));
			this.lstAvailable.ItemHeight = ((int)(resources.GetObject("lstAvailable.ItemHeight")));
			this.lstAvailable.Location = ((System.Drawing.Point)(resources.GetObject("lstAvailable.Location")));
			this.lstAvailable.Name = "lstAvailable";
			this.lstAvailable.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lstAvailable.RightToLeft")));
			this.lstAvailable.ScrollAlwaysVisible = ((bool)(resources.GetObject("lstAvailable.ScrollAlwaysVisible")));
			this.lstAvailable.Size = ((System.Drawing.Size)(resources.GetObject("lstAvailable.Size")));
			this.lstAvailable.TabIndex = ((int)(resources.GetObject("lstAvailable.TabIndex")));
			this.lstAvailable.Visible = ((bool)(resources.GetObject("lstAvailable.Visible")));
			this.lstAvailable.DoubleClick += new System.EventHandler(this.lstAvailable_DoubleClick);
			// 
			// grpFieldProperties
			// 
			this.grpFieldProperties.AccessibleDescription = resources.GetString("grpFieldProperties.AccessibleDescription");
			this.grpFieldProperties.AccessibleName = resources.GetString("grpFieldProperties.AccessibleName");
			this.grpFieldProperties.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("grpFieldProperties.Anchor")));
			this.grpFieldProperties.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("grpFieldProperties.BackgroundImage")));
			this.grpFieldProperties.Controls.Add(this.btnRemoveAll);
			this.grpFieldProperties.Controls.Add(this.btnSelectAll);
			this.grpFieldProperties.Controls.Add(this.lblSelected);
			this.grpFieldProperties.Controls.Add(this.txtOrder);
			this.grpFieldProperties.Controls.Add(this.lblOrder);
			this.grpFieldProperties.Controls.Add(this.txtCaptionVN);
			this.grpFieldProperties.Controls.Add(this.lblCaptionVN);
			this.grpFieldProperties.Controls.Add(this.txtCaptionEN);
			this.grpFieldProperties.Controls.Add(this.lblCaptionEN);
			this.grpFieldProperties.Controls.Add(this.txtCaptionJP);
			this.grpFieldProperties.Controls.Add(this.lblCaptionJP);
			this.grpFieldProperties.Controls.Add(this.cboLevel);
			this.grpFieldProperties.Controls.Add(this.lblLevel);
			this.grpFieldProperties.Controls.Add(this.txtGroupCode);
			this.grpFieldProperties.Controls.Add(this.lblGroupCode);
			this.grpFieldProperties.Controls.Add(this.lstSelected);
			this.grpFieldProperties.Controls.Add(this.lstAvailable);
			this.grpFieldProperties.Controls.Add(this.btnRemove);
			this.grpFieldProperties.Controls.Add(this.btnSelect);
			this.grpFieldProperties.Controls.Add(this.lblAvailable);
			this.grpFieldProperties.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("grpFieldProperties.Dock")));
			this.grpFieldProperties.Enabled = ((bool)(resources.GetObject("grpFieldProperties.Enabled")));
			this.grpFieldProperties.Font = ((System.Drawing.Font)(resources.GetObject("grpFieldProperties.Font")));
			this.grpFieldProperties.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("grpFieldProperties.ImeMode")));
			this.grpFieldProperties.Location = ((System.Drawing.Point)(resources.GetObject("grpFieldProperties.Location")));
			this.grpFieldProperties.Name = "grpFieldProperties";
			this.grpFieldProperties.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("grpFieldProperties.RightToLeft")));
			this.grpFieldProperties.Size = ((System.Drawing.Size)(resources.GetObject("grpFieldProperties.Size")));
			this.grpFieldProperties.TabIndex = ((int)(resources.GetObject("grpFieldProperties.TabIndex")));
			this.grpFieldProperties.TabStop = false;
			this.grpFieldProperties.Text = resources.GetString("grpFieldProperties.Text");
			this.grpFieldProperties.Visible = ((bool)(resources.GetObject("grpFieldProperties.Visible")));
			// 
			// btnRemoveAll
			// 
			this.btnRemoveAll.AccessibleDescription = resources.GetString("btnRemoveAll.AccessibleDescription");
			this.btnRemoveAll.AccessibleName = resources.GetString("btnRemoveAll.AccessibleName");
			this.btnRemoveAll.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnRemoveAll.Anchor")));
			this.btnRemoveAll.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnRemoveAll.BackgroundImage")));
			this.btnRemoveAll.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnRemoveAll.Dock")));
			this.btnRemoveAll.Enabled = ((bool)(resources.GetObject("btnRemoveAll.Enabled")));
			this.btnRemoveAll.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnRemoveAll.FlatStyle")));
			this.btnRemoveAll.Font = ((System.Drawing.Font)(resources.GetObject("btnRemoveAll.Font")));
			this.btnRemoveAll.Image = ((System.Drawing.Image)(resources.GetObject("btnRemoveAll.Image")));
			this.btnRemoveAll.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnRemoveAll.ImageAlign")));
			this.btnRemoveAll.ImageIndex = ((int)(resources.GetObject("btnRemoveAll.ImageIndex")));
			this.btnRemoveAll.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnRemoveAll.ImeMode")));
			this.btnRemoveAll.Location = ((System.Drawing.Point)(resources.GetObject("btnRemoveAll.Location")));
			this.btnRemoveAll.Name = "btnRemoveAll";
			this.btnRemoveAll.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnRemoveAll.RightToLeft")));
			this.btnRemoveAll.Size = ((System.Drawing.Size)(resources.GetObject("btnRemoveAll.Size")));
			this.btnRemoveAll.TabIndex = ((int)(resources.GetObject("btnRemoveAll.TabIndex")));
			this.btnRemoveAll.Text = resources.GetString("btnRemoveAll.Text");
			this.btnRemoveAll.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnRemoveAll.TextAlign")));
			this.btnRemoveAll.Visible = ((bool)(resources.GetObject("btnRemoveAll.Visible")));
			this.btnRemoveAll.Click += new System.EventHandler(this.btnRemoveAll_Click);
			// 
			// btnSelectAll
			// 
			this.btnSelectAll.AccessibleDescription = resources.GetString("btnSelectAll.AccessibleDescription");
			this.btnSelectAll.AccessibleName = resources.GetString("btnSelectAll.AccessibleName");
			this.btnSelectAll.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnSelectAll.Anchor")));
			this.btnSelectAll.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSelectAll.BackgroundImage")));
			this.btnSelectAll.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnSelectAll.Dock")));
			this.btnSelectAll.Enabled = ((bool)(resources.GetObject("btnSelectAll.Enabled")));
			this.btnSelectAll.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnSelectAll.FlatStyle")));
			this.btnSelectAll.Font = ((System.Drawing.Font)(resources.GetObject("btnSelectAll.Font")));
			this.btnSelectAll.Image = ((System.Drawing.Image)(resources.GetObject("btnSelectAll.Image")));
			this.btnSelectAll.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnSelectAll.ImageAlign")));
			this.btnSelectAll.ImageIndex = ((int)(resources.GetObject("btnSelectAll.ImageIndex")));
			this.btnSelectAll.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnSelectAll.ImeMode")));
			this.btnSelectAll.Location = ((System.Drawing.Point)(resources.GetObject("btnSelectAll.Location")));
			this.btnSelectAll.Name = "btnSelectAll";
			this.btnSelectAll.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnSelectAll.RightToLeft")));
			this.btnSelectAll.Size = ((System.Drawing.Size)(resources.GetObject("btnSelectAll.Size")));
			this.btnSelectAll.TabIndex = ((int)(resources.GetObject("btnSelectAll.TabIndex")));
			this.btnSelectAll.Text = resources.GetString("btnSelectAll.Text");
			this.btnSelectAll.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnSelectAll.TextAlign")));
			this.btnSelectAll.Visible = ((bool)(resources.GetObject("btnSelectAll.Visible")));
			this.btnSelectAll.Click += new System.EventHandler(this.btnSelectAll_Click);
			// 
			// lblSelected
			// 
			this.lblSelected.AccessibleDescription = resources.GetString("lblSelected.AccessibleDescription");
			this.lblSelected.AccessibleName = resources.GetString("lblSelected.AccessibleName");
			this.lblSelected.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblSelected.Anchor")));
			this.lblSelected.AutoSize = ((bool)(resources.GetObject("lblSelected.AutoSize")));
			this.lblSelected.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblSelected.Dock")));
			this.lblSelected.Enabled = ((bool)(resources.GetObject("lblSelected.Enabled")));
			this.lblSelected.Font = ((System.Drawing.Font)(resources.GetObject("lblSelected.Font")));
			this.lblSelected.ForeColor = System.Drawing.Color.Maroon;
			this.lblSelected.Image = ((System.Drawing.Image)(resources.GetObject("lblSelected.Image")));
			this.lblSelected.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblSelected.ImageAlign")));
			this.lblSelected.ImageIndex = ((int)(resources.GetObject("lblSelected.ImageIndex")));
			this.lblSelected.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblSelected.ImeMode")));
			this.lblSelected.Location = ((System.Drawing.Point)(resources.GetObject("lblSelected.Location")));
			this.lblSelected.Name = "lblSelected";
			this.lblSelected.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblSelected.RightToLeft")));
			this.lblSelected.Size = ((System.Drawing.Size)(resources.GetObject("lblSelected.Size")));
			this.lblSelected.TabIndex = ((int)(resources.GetObject("lblSelected.TabIndex")));
			this.lblSelected.Text = resources.GetString("lblSelected.Text");
			this.lblSelected.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblSelected.TextAlign")));
			this.lblSelected.Visible = ((bool)(resources.GetObject("lblSelected.Visible")));
			// 
			// txtOrder
			// 
			this.txtOrder.AccessibleDescription = resources.GetString("txtOrder.AccessibleDescription");
			this.txtOrder.AccessibleName = resources.GetString("txtOrder.AccessibleName");
			this.txtOrder.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("txtOrder.Anchor")));
			this.txtOrder.AutoSize = ((bool)(resources.GetObject("txtOrder.AutoSize")));
			this.txtOrder.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("txtOrder.BackgroundImage")));
			this.txtOrder.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("txtOrder.Dock")));
			this.txtOrder.Enabled = ((bool)(resources.GetObject("txtOrder.Enabled")));
			this.txtOrder.Font = ((System.Drawing.Font)(resources.GetObject("txtOrder.Font")));
			this.txtOrder.ForeColor = System.Drawing.SystemColors.WindowText;
			this.txtOrder.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("txtOrder.ImeMode")));
			this.txtOrder.Location = ((System.Drawing.Point)(resources.GetObject("txtOrder.Location")));
			this.txtOrder.MaxLength = ((int)(resources.GetObject("txtOrder.MaxLength")));
			this.txtOrder.Multiline = ((bool)(resources.GetObject("txtOrder.Multiline")));
			this.txtOrder.Name = "txtOrder";
			this.txtOrder.PasswordChar = ((char)(resources.GetObject("txtOrder.PasswordChar")));
			this.txtOrder.ReadOnly = true;
			this.txtOrder.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("txtOrder.RightToLeft")));
			this.txtOrder.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("txtOrder.ScrollBars")));
			this.txtOrder.Size = ((System.Drawing.Size)(resources.GetObject("txtOrder.Size")));
			this.txtOrder.TabIndex = ((int)(resources.GetObject("txtOrder.TabIndex")));
			this.txtOrder.Text = resources.GetString("txtOrder.Text");
			this.txtOrder.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("txtOrder.TextAlign")));
			this.txtOrder.Visible = ((bool)(resources.GetObject("txtOrder.Visible")));
			this.txtOrder.WordWrap = ((bool)(resources.GetObject("txtOrder.WordWrap")));
			// 
			// lblOrder
			// 
			this.lblOrder.AccessibleDescription = resources.GetString("lblOrder.AccessibleDescription");
			this.lblOrder.AccessibleName = resources.GetString("lblOrder.AccessibleName");
			this.lblOrder.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblOrder.Anchor")));
			this.lblOrder.AutoSize = ((bool)(resources.GetObject("lblOrder.AutoSize")));
			this.lblOrder.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblOrder.Dock")));
			this.lblOrder.Enabled = ((bool)(resources.GetObject("lblOrder.Enabled")));
			this.lblOrder.Font = ((System.Drawing.Font)(resources.GetObject("lblOrder.Font")));
			this.lblOrder.ForeColor = System.Drawing.Color.Black;
			this.lblOrder.Image = ((System.Drawing.Image)(resources.GetObject("lblOrder.Image")));
			this.lblOrder.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblOrder.ImageAlign")));
			this.lblOrder.ImageIndex = ((int)(resources.GetObject("lblOrder.ImageIndex")));
			this.lblOrder.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblOrder.ImeMode")));
			this.lblOrder.Location = ((System.Drawing.Point)(resources.GetObject("lblOrder.Location")));
			this.lblOrder.Name = "lblOrder";
			this.lblOrder.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblOrder.RightToLeft")));
			this.lblOrder.Size = ((System.Drawing.Size)(resources.GetObject("lblOrder.Size")));
			this.lblOrder.TabIndex = ((int)(resources.GetObject("lblOrder.TabIndex")));
			this.lblOrder.Text = resources.GetString("lblOrder.Text");
			this.lblOrder.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblOrder.TextAlign")));
			this.lblOrder.Visible = ((bool)(resources.GetObject("lblOrder.Visible")));
			// 
			// txtCaptionVN
			// 
			this.txtCaptionVN.AccessibleDescription = resources.GetString("txtCaptionVN.AccessibleDescription");
			this.txtCaptionVN.AccessibleName = resources.GetString("txtCaptionVN.AccessibleName");
			this.txtCaptionVN.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("txtCaptionVN.Anchor")));
			this.txtCaptionVN.AutoSize = ((bool)(resources.GetObject("txtCaptionVN.AutoSize")));
			this.txtCaptionVN.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("txtCaptionVN.BackgroundImage")));
			this.txtCaptionVN.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("txtCaptionVN.Dock")));
			this.txtCaptionVN.Enabled = ((bool)(resources.GetObject("txtCaptionVN.Enabled")));
			this.txtCaptionVN.Font = ((System.Drawing.Font)(resources.GetObject("txtCaptionVN.Font")));
			this.txtCaptionVN.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("txtCaptionVN.ImeMode")));
			this.txtCaptionVN.Location = ((System.Drawing.Point)(resources.GetObject("txtCaptionVN.Location")));
			this.txtCaptionVN.MaxLength = ((int)(resources.GetObject("txtCaptionVN.MaxLength")));
			this.txtCaptionVN.Multiline = ((bool)(resources.GetObject("txtCaptionVN.Multiline")));
			this.txtCaptionVN.Name = "txtCaptionVN";
			this.txtCaptionVN.PasswordChar = ((char)(resources.GetObject("txtCaptionVN.PasswordChar")));
			this.txtCaptionVN.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("txtCaptionVN.RightToLeft")));
			this.txtCaptionVN.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("txtCaptionVN.ScrollBars")));
			this.txtCaptionVN.Size = ((System.Drawing.Size)(resources.GetObject("txtCaptionVN.Size")));
			this.txtCaptionVN.TabIndex = ((int)(resources.GetObject("txtCaptionVN.TabIndex")));
			this.txtCaptionVN.Text = resources.GetString("txtCaptionVN.Text");
			this.txtCaptionVN.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("txtCaptionVN.TextAlign")));
			this.txtCaptionVN.Visible = ((bool)(resources.GetObject("txtCaptionVN.Visible")));
			this.txtCaptionVN.WordWrap = ((bool)(resources.GetObject("txtCaptionVN.WordWrap")));
			// 
			// lblCaptionVN
			// 
			this.lblCaptionVN.AccessibleDescription = resources.GetString("lblCaptionVN.AccessibleDescription");
			this.lblCaptionVN.AccessibleName = resources.GetString("lblCaptionVN.AccessibleName");
			this.lblCaptionVN.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblCaptionVN.Anchor")));
			this.lblCaptionVN.AutoSize = ((bool)(resources.GetObject("lblCaptionVN.AutoSize")));
			this.lblCaptionVN.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblCaptionVN.Dock")));
			this.lblCaptionVN.Enabled = ((bool)(resources.GetObject("lblCaptionVN.Enabled")));
			this.lblCaptionVN.Font = ((System.Drawing.Font)(resources.GetObject("lblCaptionVN.Font")));
			this.lblCaptionVN.ForeColor = System.Drawing.Color.Maroon;
			this.lblCaptionVN.Image = ((System.Drawing.Image)(resources.GetObject("lblCaptionVN.Image")));
			this.lblCaptionVN.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblCaptionVN.ImageAlign")));
			this.lblCaptionVN.ImageIndex = ((int)(resources.GetObject("lblCaptionVN.ImageIndex")));
			this.lblCaptionVN.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblCaptionVN.ImeMode")));
			this.lblCaptionVN.Location = ((System.Drawing.Point)(resources.GetObject("lblCaptionVN.Location")));
			this.lblCaptionVN.Name = "lblCaptionVN";
			this.lblCaptionVN.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblCaptionVN.RightToLeft")));
			this.lblCaptionVN.Size = ((System.Drawing.Size)(resources.GetObject("lblCaptionVN.Size")));
			this.lblCaptionVN.TabIndex = ((int)(resources.GetObject("lblCaptionVN.TabIndex")));
			this.lblCaptionVN.Text = resources.GetString("lblCaptionVN.Text");
			this.lblCaptionVN.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblCaptionVN.TextAlign")));
			this.lblCaptionVN.Visible = ((bool)(resources.GetObject("lblCaptionVN.Visible")));
			// 
			// txtCaptionEN
			// 
			this.txtCaptionEN.AccessibleDescription = resources.GetString("txtCaptionEN.AccessibleDescription");
			this.txtCaptionEN.AccessibleName = resources.GetString("txtCaptionEN.AccessibleName");
			this.txtCaptionEN.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("txtCaptionEN.Anchor")));
			this.txtCaptionEN.AutoSize = ((bool)(resources.GetObject("txtCaptionEN.AutoSize")));
			this.txtCaptionEN.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("txtCaptionEN.BackgroundImage")));
			this.txtCaptionEN.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("txtCaptionEN.Dock")));
			this.txtCaptionEN.Enabled = ((bool)(resources.GetObject("txtCaptionEN.Enabled")));
			this.txtCaptionEN.Font = ((System.Drawing.Font)(resources.GetObject("txtCaptionEN.Font")));
			this.txtCaptionEN.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("txtCaptionEN.ImeMode")));
			this.txtCaptionEN.Location = ((System.Drawing.Point)(resources.GetObject("txtCaptionEN.Location")));
			this.txtCaptionEN.MaxLength = ((int)(resources.GetObject("txtCaptionEN.MaxLength")));
			this.txtCaptionEN.Multiline = ((bool)(resources.GetObject("txtCaptionEN.Multiline")));
			this.txtCaptionEN.Name = "txtCaptionEN";
			this.txtCaptionEN.PasswordChar = ((char)(resources.GetObject("txtCaptionEN.PasswordChar")));
			this.txtCaptionEN.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("txtCaptionEN.RightToLeft")));
			this.txtCaptionEN.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("txtCaptionEN.ScrollBars")));
			this.txtCaptionEN.Size = ((System.Drawing.Size)(resources.GetObject("txtCaptionEN.Size")));
			this.txtCaptionEN.TabIndex = ((int)(resources.GetObject("txtCaptionEN.TabIndex")));
			this.txtCaptionEN.Text = resources.GetString("txtCaptionEN.Text");
			this.txtCaptionEN.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("txtCaptionEN.TextAlign")));
			this.txtCaptionEN.Visible = ((bool)(resources.GetObject("txtCaptionEN.Visible")));
			this.txtCaptionEN.WordWrap = ((bool)(resources.GetObject("txtCaptionEN.WordWrap")));
			// 
			// lblCaptionEN
			// 
			this.lblCaptionEN.AccessibleDescription = resources.GetString("lblCaptionEN.AccessibleDescription");
			this.lblCaptionEN.AccessibleName = resources.GetString("lblCaptionEN.AccessibleName");
			this.lblCaptionEN.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblCaptionEN.Anchor")));
			this.lblCaptionEN.AutoSize = ((bool)(resources.GetObject("lblCaptionEN.AutoSize")));
			this.lblCaptionEN.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblCaptionEN.Dock")));
			this.lblCaptionEN.Enabled = ((bool)(resources.GetObject("lblCaptionEN.Enabled")));
			this.lblCaptionEN.Font = ((System.Drawing.Font)(resources.GetObject("lblCaptionEN.Font")));
			this.lblCaptionEN.ForeColor = System.Drawing.Color.Maroon;
			this.lblCaptionEN.Image = ((System.Drawing.Image)(resources.GetObject("lblCaptionEN.Image")));
			this.lblCaptionEN.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblCaptionEN.ImageAlign")));
			this.lblCaptionEN.ImageIndex = ((int)(resources.GetObject("lblCaptionEN.ImageIndex")));
			this.lblCaptionEN.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblCaptionEN.ImeMode")));
			this.lblCaptionEN.Location = ((System.Drawing.Point)(resources.GetObject("lblCaptionEN.Location")));
			this.lblCaptionEN.Name = "lblCaptionEN";
			this.lblCaptionEN.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblCaptionEN.RightToLeft")));
			this.lblCaptionEN.Size = ((System.Drawing.Size)(resources.GetObject("lblCaptionEN.Size")));
			this.lblCaptionEN.TabIndex = ((int)(resources.GetObject("lblCaptionEN.TabIndex")));
			this.lblCaptionEN.Text = resources.GetString("lblCaptionEN.Text");
			this.lblCaptionEN.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblCaptionEN.TextAlign")));
			this.lblCaptionEN.Visible = ((bool)(resources.GetObject("lblCaptionEN.Visible")));
			// 
			// txtCaptionJP
			// 
			this.txtCaptionJP.AccessibleDescription = resources.GetString("txtCaptionJP.AccessibleDescription");
			this.txtCaptionJP.AccessibleName = resources.GetString("txtCaptionJP.AccessibleName");
			this.txtCaptionJP.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("txtCaptionJP.Anchor")));
			this.txtCaptionJP.AutoSize = ((bool)(resources.GetObject("txtCaptionJP.AutoSize")));
			this.txtCaptionJP.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("txtCaptionJP.BackgroundImage")));
			this.txtCaptionJP.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("txtCaptionJP.Dock")));
			this.txtCaptionJP.Enabled = ((bool)(resources.GetObject("txtCaptionJP.Enabled")));
			this.txtCaptionJP.Font = ((System.Drawing.Font)(resources.GetObject("txtCaptionJP.Font")));
			this.txtCaptionJP.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("txtCaptionJP.ImeMode")));
			this.txtCaptionJP.Location = ((System.Drawing.Point)(resources.GetObject("txtCaptionJP.Location")));
			this.txtCaptionJP.MaxLength = ((int)(resources.GetObject("txtCaptionJP.MaxLength")));
			this.txtCaptionJP.Multiline = ((bool)(resources.GetObject("txtCaptionJP.Multiline")));
			this.txtCaptionJP.Name = "txtCaptionJP";
			this.txtCaptionJP.PasswordChar = ((char)(resources.GetObject("txtCaptionJP.PasswordChar")));
			this.txtCaptionJP.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("txtCaptionJP.RightToLeft")));
			this.txtCaptionJP.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("txtCaptionJP.ScrollBars")));
			this.txtCaptionJP.Size = ((System.Drawing.Size)(resources.GetObject("txtCaptionJP.Size")));
			this.txtCaptionJP.TabIndex = ((int)(resources.GetObject("txtCaptionJP.TabIndex")));
			this.txtCaptionJP.Text = resources.GetString("txtCaptionJP.Text");
			this.txtCaptionJP.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("txtCaptionJP.TextAlign")));
			this.txtCaptionJP.Visible = ((bool)(resources.GetObject("txtCaptionJP.Visible")));
			this.txtCaptionJP.WordWrap = ((bool)(resources.GetObject("txtCaptionJP.WordWrap")));
			// 
			// lblCaptionJP
			// 
			this.lblCaptionJP.AccessibleDescription = resources.GetString("lblCaptionJP.AccessibleDescription");
			this.lblCaptionJP.AccessibleName = resources.GetString("lblCaptionJP.AccessibleName");
			this.lblCaptionJP.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblCaptionJP.Anchor")));
			this.lblCaptionJP.AutoSize = ((bool)(resources.GetObject("lblCaptionJP.AutoSize")));
			this.lblCaptionJP.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblCaptionJP.Dock")));
			this.lblCaptionJP.Enabled = ((bool)(resources.GetObject("lblCaptionJP.Enabled")));
			this.lblCaptionJP.Font = ((System.Drawing.Font)(resources.GetObject("lblCaptionJP.Font")));
			this.lblCaptionJP.ForeColor = System.Drawing.Color.Maroon;
			this.lblCaptionJP.Image = ((System.Drawing.Image)(resources.GetObject("lblCaptionJP.Image")));
			this.lblCaptionJP.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblCaptionJP.ImageAlign")));
			this.lblCaptionJP.ImageIndex = ((int)(resources.GetObject("lblCaptionJP.ImageIndex")));
			this.lblCaptionJP.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblCaptionJP.ImeMode")));
			this.lblCaptionJP.Location = ((System.Drawing.Point)(resources.GetObject("lblCaptionJP.Location")));
			this.lblCaptionJP.Name = "lblCaptionJP";
			this.lblCaptionJP.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblCaptionJP.RightToLeft")));
			this.lblCaptionJP.Size = ((System.Drawing.Size)(resources.GetObject("lblCaptionJP.Size")));
			this.lblCaptionJP.TabIndex = ((int)(resources.GetObject("lblCaptionJP.TabIndex")));
			this.lblCaptionJP.Text = resources.GetString("lblCaptionJP.Text");
			this.lblCaptionJP.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblCaptionJP.TextAlign")));
			this.lblCaptionJP.Visible = ((bool)(resources.GetObject("lblCaptionJP.Visible")));
			// 
			// cboLevel
			// 
			this.cboLevel.AccessibleDescription = resources.GetString("cboLevel.AccessibleDescription");
			this.cboLevel.AccessibleName = resources.GetString("cboLevel.AccessibleName");
			this.cboLevel.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("cboLevel.Anchor")));
			this.cboLevel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cboLevel.BackgroundImage")));
			this.cboLevel.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("cboLevel.Dock")));
			this.cboLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboLevel.Enabled = ((bool)(resources.GetObject("cboLevel.Enabled")));
			this.cboLevel.Font = ((System.Drawing.Font)(resources.GetObject("cboLevel.Font")));
			this.cboLevel.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("cboLevel.ImeMode")));
			this.cboLevel.IntegralHeight = ((bool)(resources.GetObject("cboLevel.IntegralHeight")));
			this.cboLevel.ItemHeight = ((int)(resources.GetObject("cboLevel.ItemHeight")));
			this.cboLevel.Items.AddRange(new object[] {
														  resources.GetString("cboLevel.Items"),
														  resources.GetString("cboLevel.Items1")});
			this.cboLevel.Location = ((System.Drawing.Point)(resources.GetObject("cboLevel.Location")));
			this.cboLevel.MaxDropDownItems = ((int)(resources.GetObject("cboLevel.MaxDropDownItems")));
			this.cboLevel.MaxLength = ((int)(resources.GetObject("cboLevel.MaxLength")));
			this.cboLevel.Name = "cboLevel";
			this.cboLevel.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("cboLevel.RightToLeft")));
			this.cboLevel.Size = ((System.Drawing.Size)(resources.GetObject("cboLevel.Size")));
			this.cboLevel.TabIndex = ((int)(resources.GetObject("cboLevel.TabIndex")));
			this.cboLevel.Text = resources.GetString("cboLevel.Text");
			this.cboLevel.Visible = ((bool)(resources.GetObject("cboLevel.Visible")));
			this.cboLevel.SelectedIndexChanged += new System.EventHandler(this.cboLevel_SelectedIndexChanged);
			// 
			// lblLevel
			// 
			this.lblLevel.AccessibleDescription = resources.GetString("lblLevel.AccessibleDescription");
			this.lblLevel.AccessibleName = resources.GetString("lblLevel.AccessibleName");
			this.lblLevel.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblLevel.Anchor")));
			this.lblLevel.AutoSize = ((bool)(resources.GetObject("lblLevel.AutoSize")));
			this.lblLevel.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblLevel.Dock")));
			this.lblLevel.Enabled = ((bool)(resources.GetObject("lblLevel.Enabled")));
			this.lblLevel.Font = ((System.Drawing.Font)(resources.GetObject("lblLevel.Font")));
			this.lblLevel.ForeColor = System.Drawing.Color.Maroon;
			this.lblLevel.Image = ((System.Drawing.Image)(resources.GetObject("lblLevel.Image")));
			this.lblLevel.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblLevel.ImageAlign")));
			this.lblLevel.ImageIndex = ((int)(resources.GetObject("lblLevel.ImageIndex")));
			this.lblLevel.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblLevel.ImeMode")));
			this.lblLevel.Location = ((System.Drawing.Point)(resources.GetObject("lblLevel.Location")));
			this.lblLevel.Name = "lblLevel";
			this.lblLevel.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblLevel.RightToLeft")));
			this.lblLevel.Size = ((System.Drawing.Size)(resources.GetObject("lblLevel.Size")));
			this.lblLevel.TabIndex = ((int)(resources.GetObject("lblLevel.TabIndex")));
			this.lblLevel.Text = resources.GetString("lblLevel.Text");
			this.lblLevel.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblLevel.TextAlign")));
			this.lblLevel.Visible = ((bool)(resources.GetObject("lblLevel.Visible")));
			// 
			// txtGroupCode
			// 
			this.txtGroupCode.AccessibleDescription = resources.GetString("txtGroupCode.AccessibleDescription");
			this.txtGroupCode.AccessibleName = resources.GetString("txtGroupCode.AccessibleName");
			this.txtGroupCode.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("txtGroupCode.Anchor")));
			this.txtGroupCode.AutoSize = ((bool)(resources.GetObject("txtGroupCode.AutoSize")));
			this.txtGroupCode.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("txtGroupCode.BackgroundImage")));
			this.txtGroupCode.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("txtGroupCode.Dock")));
			this.txtGroupCode.Enabled = ((bool)(resources.GetObject("txtGroupCode.Enabled")));
			this.txtGroupCode.Font = ((System.Drawing.Font)(resources.GetObject("txtGroupCode.Font")));
			this.txtGroupCode.ForeColor = System.Drawing.SystemColors.WindowText;
			this.txtGroupCode.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("txtGroupCode.ImeMode")));
			this.txtGroupCode.Location = ((System.Drawing.Point)(resources.GetObject("txtGroupCode.Location")));
			this.txtGroupCode.MaxLength = ((int)(resources.GetObject("txtGroupCode.MaxLength")));
			this.txtGroupCode.Multiline = ((bool)(resources.GetObject("txtGroupCode.Multiline")));
			this.txtGroupCode.Name = "txtGroupCode";
			this.txtGroupCode.PasswordChar = ((char)(resources.GetObject("txtGroupCode.PasswordChar")));
			this.txtGroupCode.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("txtGroupCode.RightToLeft")));
			this.txtGroupCode.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("txtGroupCode.ScrollBars")));
			this.txtGroupCode.Size = ((System.Drawing.Size)(resources.GetObject("txtGroupCode.Size")));
			this.txtGroupCode.TabIndex = ((int)(resources.GetObject("txtGroupCode.TabIndex")));
			this.txtGroupCode.Text = resources.GetString("txtGroupCode.Text");
			this.txtGroupCode.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("txtGroupCode.TextAlign")));
			this.txtGroupCode.Visible = ((bool)(resources.GetObject("txtGroupCode.Visible")));
			this.txtGroupCode.WordWrap = ((bool)(resources.GetObject("txtGroupCode.WordWrap")));
			// 
			// lblGroupCode
			// 
			this.lblGroupCode.AccessibleDescription = resources.GetString("lblGroupCode.AccessibleDescription");
			this.lblGroupCode.AccessibleName = resources.GetString("lblGroupCode.AccessibleName");
			this.lblGroupCode.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblGroupCode.Anchor")));
			this.lblGroupCode.AutoSize = ((bool)(resources.GetObject("lblGroupCode.AutoSize")));
			this.lblGroupCode.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblGroupCode.Dock")));
			this.lblGroupCode.Enabled = ((bool)(resources.GetObject("lblGroupCode.Enabled")));
			this.lblGroupCode.Font = ((System.Drawing.Font)(resources.GetObject("lblGroupCode.Font")));
			this.lblGroupCode.ForeColor = System.Drawing.Color.Maroon;
			this.lblGroupCode.Image = ((System.Drawing.Image)(resources.GetObject("lblGroupCode.Image")));
			this.lblGroupCode.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblGroupCode.ImageAlign")));
			this.lblGroupCode.ImageIndex = ((int)(resources.GetObject("lblGroupCode.ImageIndex")));
			this.lblGroupCode.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblGroupCode.ImeMode")));
			this.lblGroupCode.Location = ((System.Drawing.Point)(resources.GetObject("lblGroupCode.Location")));
			this.lblGroupCode.Name = "lblGroupCode";
			this.lblGroupCode.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblGroupCode.RightToLeft")));
			this.lblGroupCode.Size = ((System.Drawing.Size)(resources.GetObject("lblGroupCode.Size")));
			this.lblGroupCode.TabIndex = ((int)(resources.GetObject("lblGroupCode.TabIndex")));
			this.lblGroupCode.Text = resources.GetString("lblGroupCode.Text");
			this.lblGroupCode.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblGroupCode.TextAlign")));
			this.lblGroupCode.Visible = ((bool)(resources.GetObject("lblGroupCode.Visible")));
			// 
			// btnRemove
			// 
			this.btnRemove.AccessibleDescription = resources.GetString("btnRemove.AccessibleDescription");
			this.btnRemove.AccessibleName = resources.GetString("btnRemove.AccessibleName");
			this.btnRemove.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnRemove.Anchor")));
			this.btnRemove.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnRemove.BackgroundImage")));
			this.btnRemove.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnRemove.Dock")));
			this.btnRemove.Enabled = ((bool)(resources.GetObject("btnRemove.Enabled")));
			this.btnRemove.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnRemove.FlatStyle")));
			this.btnRemove.Font = ((System.Drawing.Font)(resources.GetObject("btnRemove.Font")));
			this.btnRemove.Image = ((System.Drawing.Image)(resources.GetObject("btnRemove.Image")));
			this.btnRemove.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnRemove.ImageAlign")));
			this.btnRemove.ImageIndex = ((int)(resources.GetObject("btnRemove.ImageIndex")));
			this.btnRemove.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnRemove.ImeMode")));
			this.btnRemove.Location = ((System.Drawing.Point)(resources.GetObject("btnRemove.Location")));
			this.btnRemove.Name = "btnRemove";
			this.btnRemove.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnRemove.RightToLeft")));
			this.btnRemove.Size = ((System.Drawing.Size)(resources.GetObject("btnRemove.Size")));
			this.btnRemove.TabIndex = ((int)(resources.GetObject("btnRemove.TabIndex")));
			this.btnRemove.Text = resources.GetString("btnRemove.Text");
			this.btnRemove.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnRemove.TextAlign")));
			this.btnRemove.Visible = ((bool)(resources.GetObject("btnRemove.Visible")));
			this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
			// 
			// btnSelect
			// 
			this.btnSelect.AccessibleDescription = resources.GetString("btnSelect.AccessibleDescription");
			this.btnSelect.AccessibleName = resources.GetString("btnSelect.AccessibleName");
			this.btnSelect.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnSelect.Anchor")));
			this.btnSelect.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSelect.BackgroundImage")));
			this.btnSelect.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnSelect.Dock")));
			this.btnSelect.Enabled = ((bool)(resources.GetObject("btnSelect.Enabled")));
			this.btnSelect.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnSelect.FlatStyle")));
			this.btnSelect.Font = ((System.Drawing.Font)(resources.GetObject("btnSelect.Font")));
			this.btnSelect.Image = ((System.Drawing.Image)(resources.GetObject("btnSelect.Image")));
			this.btnSelect.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnSelect.ImageAlign")));
			this.btnSelect.ImageIndex = ((int)(resources.GetObject("btnSelect.ImageIndex")));
			this.btnSelect.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnSelect.ImeMode")));
			this.btnSelect.Location = ((System.Drawing.Point)(resources.GetObject("btnSelect.Location")));
			this.btnSelect.Name = "btnSelect";
			this.btnSelect.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnSelect.RightToLeft")));
			this.btnSelect.Size = ((System.Drawing.Size)(resources.GetObject("btnSelect.Size")));
			this.btnSelect.TabIndex = ((int)(resources.GetObject("btnSelect.TabIndex")));
			this.btnSelect.Text = resources.GetString("btnSelect.Text");
			this.btnSelect.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnSelect.TextAlign")));
			this.btnSelect.Visible = ((bool)(resources.GetObject("btnSelect.Visible")));
			this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
			// 
			// lblAvailable
			// 
			this.lblAvailable.AccessibleDescription = resources.GetString("lblAvailable.AccessibleDescription");
			this.lblAvailable.AccessibleName = resources.GetString("lblAvailable.AccessibleName");
			this.lblAvailable.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblAvailable.Anchor")));
			this.lblAvailable.AutoSize = ((bool)(resources.GetObject("lblAvailable.AutoSize")));
			this.lblAvailable.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblAvailable.Dock")));
			this.lblAvailable.Enabled = ((bool)(resources.GetObject("lblAvailable.Enabled")));
			this.lblAvailable.Font = ((System.Drawing.Font)(resources.GetObject("lblAvailable.Font")));
			this.lblAvailable.ForeColor = System.Drawing.Color.Black;
			this.lblAvailable.Image = ((System.Drawing.Image)(resources.GetObject("lblAvailable.Image")));
			this.lblAvailable.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblAvailable.ImageAlign")));
			this.lblAvailable.ImageIndex = ((int)(resources.GetObject("lblAvailable.ImageIndex")));
			this.lblAvailable.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblAvailable.ImeMode")));
			this.lblAvailable.Location = ((System.Drawing.Point)(resources.GetObject("lblAvailable.Location")));
			this.lblAvailable.Name = "lblAvailable";
			this.lblAvailable.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblAvailable.RightToLeft")));
			this.lblAvailable.Size = ((System.Drawing.Size)(resources.GetObject("lblAvailable.Size")));
			this.lblAvailable.TabIndex = ((int)(resources.GetObject("lblAvailable.TabIndex")));
			this.lblAvailable.Text = resources.GetString("lblAvailable.Text");
			this.lblAvailable.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblAvailable.TextAlign")));
			this.lblAvailable.Visible = ((bool)(resources.GetObject("lblAvailable.Visible")));
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
			// btnMoveDown
			// 
			this.btnMoveDown.AccessibleDescription = resources.GetString("btnMoveDown.AccessibleDescription");
			this.btnMoveDown.AccessibleName = resources.GetString("btnMoveDown.AccessibleName");
			this.btnMoveDown.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnMoveDown.Anchor")));
			this.btnMoveDown.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnMoveDown.BackgroundImage")));
			this.btnMoveDown.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnMoveDown.Dock")));
			this.btnMoveDown.Enabled = ((bool)(resources.GetObject("btnMoveDown.Enabled")));
			this.btnMoveDown.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnMoveDown.FlatStyle")));
			this.btnMoveDown.Font = ((System.Drawing.Font)(resources.GetObject("btnMoveDown.Font")));
			this.btnMoveDown.Image = ((System.Drawing.Image)(resources.GetObject("btnMoveDown.Image")));
			this.btnMoveDown.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnMoveDown.ImageAlign")));
			this.btnMoveDown.ImageIndex = ((int)(resources.GetObject("btnMoveDown.ImageIndex")));
			this.btnMoveDown.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnMoveDown.ImeMode")));
			this.btnMoveDown.Location = ((System.Drawing.Point)(resources.GetObject("btnMoveDown.Location")));
			this.btnMoveDown.Name = "btnMoveDown";
			this.btnMoveDown.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnMoveDown.RightToLeft")));
			this.btnMoveDown.Size = ((System.Drawing.Size)(resources.GetObject("btnMoveDown.Size")));
			this.btnMoveDown.TabIndex = ((int)(resources.GetObject("btnMoveDown.TabIndex")));
			this.btnMoveDown.Text = resources.GetString("btnMoveDown.Text");
			this.btnMoveDown.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnMoveDown.TextAlign")));
			this.btnMoveDown.Visible = ((bool)(resources.GetObject("btnMoveDown.Visible")));
			this.btnMoveDown.Click += new System.EventHandler(this.btnMoveDown_Click);
			// 
			// btnMoveUp
			// 
			this.btnMoveUp.AccessibleDescription = resources.GetString("btnMoveUp.AccessibleDescription");
			this.btnMoveUp.AccessibleName = resources.GetString("btnMoveUp.AccessibleName");
			this.btnMoveUp.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnMoveUp.Anchor")));
			this.btnMoveUp.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnMoveUp.BackgroundImage")));
			this.btnMoveUp.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnMoveUp.Dock")));
			this.btnMoveUp.Enabled = ((bool)(resources.GetObject("btnMoveUp.Enabled")));
			this.btnMoveUp.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnMoveUp.FlatStyle")));
			this.btnMoveUp.Font = ((System.Drawing.Font)(resources.GetObject("btnMoveUp.Font")));
			this.btnMoveUp.Image = ((System.Drawing.Image)(resources.GetObject("btnMoveUp.Image")));
			this.btnMoveUp.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnMoveUp.ImageAlign")));
			this.btnMoveUp.ImageIndex = ((int)(resources.GetObject("btnMoveUp.ImageIndex")));
			this.btnMoveUp.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnMoveUp.ImeMode")));
			this.btnMoveUp.Location = ((System.Drawing.Point)(resources.GetObject("btnMoveUp.Location")));
			this.btnMoveUp.Name = "btnMoveUp";
			this.btnMoveUp.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnMoveUp.RightToLeft")));
			this.btnMoveUp.Size = ((System.Drawing.Size)(resources.GetObject("btnMoveUp.Size")));
			this.btnMoveUp.TabIndex = ((int)(resources.GetObject("btnMoveUp.TabIndex")));
			this.btnMoveUp.Text = resources.GetString("btnMoveUp.Text");
			this.btnMoveUp.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnMoveUp.TextAlign")));
			this.btnMoveUp.Visible = ((bool)(resources.GetObject("btnMoveUp.Visible")));
			this.btnMoveUp.Click += new System.EventHandler(this.btnMoveUp_Click);
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
			// GroupProperties
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
			this.Controls.Add(this.btnEdit);
			this.Controls.Add(this.btnMoveDown);
			this.Controls.Add(this.btnMoveUp);
			this.Controls.Add(this.btnHelp);
			this.Controls.Add(this.btnClose);
			this.Controls.Add(this.btnAdd);
			this.Controls.Add(this.btnSave);
			this.Controls.Add(this.btnDelete);
			this.Controls.Add(this.grpFieldProperties);
			this.Controls.Add(this.tvwFieldGroups);
			this.Enabled = ((bool)(resources.GetObject("$this.Enabled")));
			this.Font = ((System.Drawing.Font)(resources.GetObject("$this.Font")));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("$this.ImeMode")));
			this.KeyPreview = true;
			this.Location = ((System.Drawing.Point)(resources.GetObject("$this.Location")));
			this.MaximizeBox = false;
			this.MaximumSize = ((System.Drawing.Size)(resources.GetObject("$this.MaximumSize")));
			this.MinimizeBox = false;
			this.MinimumSize = ((System.Drawing.Size)(resources.GetObject("$this.MinimumSize")));
			this.Name = "GroupProperties";
			this.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("$this.RightToLeft")));
			this.StartPosition = ((System.Windows.Forms.FormStartPosition)(resources.GetObject("$this.StartPosition")));
			this.Text = resources.GetString("$this.Text");
			this.Load += new System.EventHandler(this.GroupProperties_Load);
			this.grpFieldProperties.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// Check security, load the tree view and intialize form
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void GroupProperties_Load(object sender, System.EventArgs e)
		{
			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.WaitCursor;

			#endregion Code Inserted Automatically

			
			const string METHOD_NAME = THIS + ".GroupProperties_Load()";
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
					PCSMessageBox.Show(ErrorCode.MESSAGE_YOU_HAVE_NO_RIGHT_TO_VIEW);
					// Code Inserted Automatically
#region Code Inserted Automatically
this.Cursor = Cursors.Default;
#endregion Code Inserted Automatically

					return;
				}
				#endregion

				// initialize all variable
				boFieldGroup = new GroupPropertiesBO();
				// get all group of report
				dtbFieldGroup = boFieldGroup.List(mReport.ReportID);
				// create the tree view
				CreateTree(dtbFieldGroup);
				// switch form mode
				SwitchFormMode();
				// collapse all node
				tvwFieldGroups.CollapseAll();
				// set focus on first node of tree view
				tvwFieldGroups.Focus();
				tvwFieldGroups.Select();
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

		/// <summary>
		/// After select a node from Tree, we need to enable button
		/// and assign value of selected node to detail form.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void tvwFieldGroups_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e)
		{
			const string METHOD_NAME = THIS + ".tvwFieldGroups_AfterSelect()";
			try
			{
				if (tvwFieldGroups.SelectedNode != null)
				{
					// get selected node
					objSelectedNode = tvwFieldGroups.SelectedNode;
					// if selected node is field group, load data to form
					if (objSelectedNode.Tag != null && objSelectedNode.Tag is Sys_FieldGroupVO)
					{
						voFieldGroup = (Sys_FieldGroupVO)objSelectedNode.Tag;
						// assign value of selected field to detail form
						AssignValueToControl(objSelectedNode);
					}
					else
					{
						// clear all data in detail information
						FormControlComponents.ClearForm(grpFieldProperties);
					}
					// enable associated button with selected node
					EnableButtons(tvwFieldGroups.SelectedNode);
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

		/// <summary>
		/// Edit the selected node
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void tvwFieldGroups_DoubleClick(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".tvwFieldGroups_DoubleClick()";
			try
			{
				if (tvwFieldGroups.SelectedNode != null && 
					tvwFieldGroups.SelectedNode.Tag != null &&
					tvwFieldGroups.SelectedNode.Tag is Sys_FieldGroupVO)
					btnEdit_Click(null, null);
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

		/// <summary>
		/// If user press Enter key, edit the selected node
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void tvwFieldGroups_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			const string METHOD_NAME = THIS + ".tvwFieldGroups_KeyDown()";
			try
			{
				switch (e.KeyCode)
				{
					case Keys.Enter:
						btnEdit_Click(null, null);
						break;
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

		/// <summary>
		/// Select available group/field
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnSelect_Click(object sender, System.EventArgs e)
		{
			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.WaitCursor;

			#endregion Code Inserted Automatically

			
			const string METHOD_NAME = THIS + ".btnSelect_Click()";
			try
			{
				if (lstAvailable.SelectedValue != null)
				{
					int intLevel = cboLevel.SelectedIndex + 1;
					switch (intLevel)
					{
						case (int)GroupFieldLevel.One:
							for (int i = 0; i < arrAvailable.Count; i++)
							{
								Sys_FieldGroupVO voGroup = (Sys_FieldGroupVO)arrAvailable[i];
								if (voGroup.FieldGroupID == int.Parse(lstAvailable.SelectedValue.ToString()))
								{
									// remove from available list
									arrAvailable.RemoveAt(i);
									// add to selected list
									arrSelected.Add(voGroup);
									break;
								}
							}
							arrAvailable.TrimToSize();
							arrSelected.TrimToSize();
							// rebind data source
							// available
							lstAvailable.DataSource = null;
							lstAvailable.DataSource = arrAvailable;
							lstAvailable.DisplayMember = Sys_FieldGroupTable.GROUPCODE_FLD;
							lstAvailable.ValueMember = Sys_FieldGroupTable.FIELDGROUPID_FLD;
							// selected
							lstSelected.DataSource = null;
							lstSelected.DataSource = arrSelected;
							lstSelected.DisplayMember = Sys_FieldGroupTable.GROUPCODE_FLD;
							lstSelected.ValueMember = Sys_FieldGroupTable.FIELDGROUPID_FLD;
							break;
						case (int)GroupFieldLevel.Two:
							for (int i = 0; i < arrAvailable.Count; i++)
							{
								sys_ReportFieldsVO voReportField = (sys_ReportFieldsVO)arrAvailable[i];
								if (voReportField.FieldName == lstAvailable.SelectedValue.ToString())
								{
									// add to selected list
									arrSelected.Add(voReportField);
									// remove from available list
									arrAvailable.RemoveAt(i);
									break;
								}
							}
							arrSelected.TrimToSize();
							arrAvailable.TrimToSize();
							// rebind data source
							// available
							lstAvailable.DataSource = null;
							lstAvailable.DataSource = arrAvailable;
							lstAvailable.DisplayMember = sys_ReportFieldsTable.FIELDNAME_FLD;
							lstAvailable.ValueMember = sys_ReportFieldsTable.FIELDNAME_FLD;
							// selected
							lstSelected.DataSource = null;
							lstSelected.DataSource = arrSelected;
							lstSelected.DisplayMember = sys_ReportFieldsTable.FIELDNAME_FLD;
							lstSelected.ValueMember = sys_ReportFieldsTable.FIELDNAME_FLD;
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

			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.Default;

			#endregion Code Inserted Automatically

			
		}

		/// <summary>
		/// Remove selected group/field
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnRemove_Click(object sender, System.EventArgs e)
		{
			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.WaitCursor;

			#endregion Code Inserted Automatically

			
			const string METHOD_NAME = THIS + ".btnRemove_Click()";
			try
			{
				int intLevel = cboLevel.SelectedIndex + 1;
				if (lstSelected.SelectedValue != null)
				{
					switch (intLevel)
					{
						case (int)GroupFieldLevel.One:
							for (int i = 0; i < arrSelected.Count; i++)
							{
								Sys_FieldGroupVO voGroup = (Sys_FieldGroupVO)arrSelected[i];
								if (voGroup.FieldGroupID == int.Parse(lstSelected.SelectedValue.ToString()))
								{
									// remove from available list
									arrSelected.RemoveAt(i);
									// add to selected list
									arrAvailable.Add(voGroup);
									break;
								}
							}
							arrAvailable.TrimToSize();
							arrSelected.TrimToSize();
							// rebind data source
							// available
							lstAvailable.DataSource = null;
							lstAvailable.DataSource = arrAvailable;
							lstAvailable.DisplayMember = Sys_FieldGroupTable.GROUPCODE_FLD;
							lstAvailable.ValueMember = Sys_FieldGroupTable.FIELDGROUPID_FLD;
							// selected
							lstSelected.DataSource = null;
							lstSelected.DataSource = arrSelected;
							lstSelected.DisplayMember = Sys_FieldGroupTable.GROUPCODE_FLD;
							lstSelected.ValueMember = Sys_FieldGroupTable.FIELDGROUPID_FLD;
							break;
						case (int)GroupFieldLevel.Two:
							for (int i = 0; i < arrSelected.Count; i++)
							{
								sys_ReportFieldsVO voReportField = (sys_ReportFieldsVO)arrSelected[i];
								if (voReportField.FieldName == lstSelected.SelectedValue.ToString())
								{
									// remove from selected list
									arrSelected.RemoveAt(i);
									// add to available list
									arrAvailable.Add(voReportField);
									break;
								}
							}
							arrSelected.TrimToSize();
							arrAvailable.TrimToSize();
							// rebind data source
							// available
							lstAvailable.DataSource = null;
							lstAvailable.DataSource = arrAvailable;
							lstAvailable.DisplayMember = sys_ReportFieldsTable.FIELDNAME_FLD;
							lstAvailable.ValueMember = sys_ReportFieldsTable.FIELDNAME_FLD;
							// selected
							lstSelected.DataSource = null;
							lstSelected.DataSource = arrSelected;
							lstSelected.DisplayMember = sys_ReportFieldsTable.FIELDNAME_FLD;
							lstSelected.ValueMember = sys_ReportFieldsTable.FIELDNAME_FLD;
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

			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.Default;

			#endregion Code Inserted Automatically

			
		}

		/// <summary>
		/// Select available group/field
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void lstAvailable_DoubleClick(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".lstAvailable_DoubleClick()";
			try
			{
				btnSelect_Click(null, null);
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

		private void lstSelected_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".lstSelected_SelectedIndexChanged()";
			try
			{
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

		/// <summary>
		/// Select all available group/field
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnSelectAll_Click(object sender, System.EventArgs e)
		{
			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.WaitCursor;

			#endregion Code Inserted Automatically

			
			const string METHOD_NAME = THIS + ".btnSelectAll_Click()";
			try
			{
				int intLevel = cboLevel.SelectedIndex + 1;
				switch (intLevel)
				{
					case (int)GroupFieldLevel.One:
						for (int i = 0; i < arrAvailable.Count; i++)
						{
							Sys_FieldGroupVO voGroup = (Sys_FieldGroupVO)arrAvailable[i];
							// add to selected list
							arrSelected.Add(voGroup);
						}
						// clear all items in available list
						arrAvailable.Clear();
						arrSelected.TrimToSize();
						// rebind data source
						// available
						lstAvailable.DataSource = null;
						lstAvailable.Items.Clear();
						// selected
						lstSelected.DataSource = null;
						lstSelected.DataSource = arrSelected;
						lstSelected.DisplayMember = Sys_FieldGroupTable.GROUPCODE_FLD;
						lstSelected.ValueMember = Sys_FieldGroupTable.FIELDGROUPID_FLD;
						break;
					case (int)GroupFieldLevel.Two:
						for (int i = 0; i < arrAvailable.Count; i++)
						{
							sys_ReportFieldsVO voReportField = (sys_ReportFieldsVO)arrAvailable[i];
							// add to selected list
							arrSelected.Add(voReportField);
						}
						arrSelected.TrimToSize();
						// clear all items in available list
						arrAvailable.Clear();
						// rebind data source
						// available
						lstAvailable.DataSource = null;
						lstAvailable.Items.Clear();
						// selected
						lstSelected.DataSource = null;
						lstSelected.DataSource = arrSelected;
						lstSelected.DisplayMember = sys_ReportFieldsTable.FIELDNAME_FLD;
						lstSelected.ValueMember = sys_ReportFieldsTable.FIELDNAME_FLD;
						break;
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

			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.Default;

			#endregion Code Inserted Automatically

			
		}

		/// <summary>
		/// Remove all selected group/field
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnRemoveAll_Click(object sender, System.EventArgs e)
		{
			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.WaitCursor;

			#endregion Code Inserted Automatically

			
			const string METHOD_NAME = THIS + ".btnRemoveAll_Click()";
			try
			{
				int intLevel = cboLevel.SelectedIndex + 1;
				switch (intLevel)
				{
					case (int)GroupFieldLevel.One:
						for (int i = 0; i < arrSelected.Count; i++)
						{
							Sys_FieldGroupVO voGroup = (Sys_FieldGroupVO)arrSelected[i];
							// add to selected list
							arrAvailable.Add(voGroup);
						}
						arrAvailable.TrimToSize();
						// clear all items in selected list
						arrSelected.Clear();
						// rebind data source
						// available
						lstAvailable.DataSource = null;
						lstAvailable.DataSource = arrAvailable;
						lstAvailable.DisplayMember = Sys_FieldGroupTable.GROUPCODE_FLD;
						lstAvailable.ValueMember = Sys_FieldGroupTable.FIELDGROUPID_FLD;
						// selected
						lstSelected.DataSource = null;
						lstSelected.Items.Clear();
						break;
					case (int)GroupFieldLevel.Two:
						for (int i = 0; i < arrSelected.Count; i++)
						{
							sys_ReportFieldsVO voReportField = (sys_ReportFieldsVO)arrSelected[i];
							// add to available list
							arrAvailable.Add(voReportField);
						}
						arrAvailable.TrimToSize();
						// clear all items in selected list
						arrSelected.Clear();
						// rebind data source
						// available
						lstAvailable.DataSource = null;
						lstAvailable.DataSource = arrAvailable;
						lstAvailable.DisplayMember = sys_ReportFieldsTable.FIELDNAME_FLD;
						lstAvailable.ValueMember = sys_ReportFieldsTable.FIELDNAME_FLD;
						// selected
						lstSelected.DataSource = null;
						lstSelected.Items.Clear();
						break;
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

			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.Default;

			#endregion Code Inserted Automatically

			
		}

		/// <summary>
		/// Turn form mode to Add
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnAdd_Click(object sender, System.EventArgs e)
		{
			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.WaitCursor;

			#endregion Code Inserted Automatically

			
			const string METHOD_NAME = THIS + ".btnAdd_Click()";
			try
			{
				AssignValueToControl(null);
				mFormMode = EnumAction.Add;
				SwitchFormMode();
				txtGroupCode.Focus();
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

		/// <summary>
		/// Turn form mode to Edit
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnEdit_Click(object sender, System.EventArgs e)
		{
			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.WaitCursor;

			#endregion Code Inserted Automatically

			
			const string METHOD_NAME = THIS + ".btnEdit_Click()";
			try
			{
				if (tvwFieldGroups.SelectedNode != null && 
					tvwFieldGroups.SelectedNode.Tag != null &&
					tvwFieldGroups.SelectedNode.Tag is Sys_FieldGroupVO)
				{
					AssignValueToControl(tvwFieldGroups.SelectedNode);
					mFormMode = EnumAction.Edit;
					SwitchFormMode();
					txtGroupCode.Focus();
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

			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.Default;

			#endregion Code Inserted Automatically

			
		}
		/// <summary>
		/// Check mandatory field and save to database
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnSave_Click(object sender, System.EventArgs e)
		{
			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.WaitCursor;

			#endregion Code Inserted Automatically

			
			const string METHOD_NAME = THIS + ".btnSave_Click()";
			try
			{
				#region Check mandatory fields
                
				// check mandatory field
				if (FormControlComponents.CheckMandatory(txtGroupCode))
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Exclamation);
					txtGroupCode.Focus();
					// Code Inserted Automatically
#region Code Inserted Automatically
this.Cursor = Cursors.Default;
#endregion Code Inserted Automatically

					return;
				}
				if (FormControlComponents.CheckMandatory(cboLevel))
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Exclamation);
					cboLevel.Focus();
					// Code Inserted Automatically
#region Code Inserted Automatically
this.Cursor = Cursors.Default;
#endregion Code Inserted Automatically

					return;
				}
				if (FormControlComponents.CheckMandatory(txtCaptionEN))
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Exclamation);
					txtCaptionEN.Focus();
					// Code Inserted Automatically
#region Code Inserted Automatically
this.Cursor = Cursors.Default;
#endregion Code Inserted Automatically

					return;
				}
				if (FormControlComponents.CheckMandatory(txtCaptionJP))
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Exclamation);
					txtCaptionJP.Focus();
					// Code Inserted Automatically
#region Code Inserted Automatically
this.Cursor = Cursors.Default;
#endregion Code Inserted Automatically

					return;
				}
				if (FormControlComponents.CheckMandatory(txtCaptionVN))
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Exclamation);
					txtCaptionVN.Focus();
					// Code Inserted Automatically
#region Code Inserted Automatically
this.Cursor = Cursors.Default;
#endregion Code Inserted Automatically

					return;
				}
				if (lstSelected.Items.Count == 0)
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Exclamation);
					lstSelected.Focus();
					// Code Inserted Automatically
#region Code Inserted Automatically
this.Cursor = Cursors.Default;
#endregion Code Inserted Automatically

					return;
				}

				#endregion

				if (SaveData())
				{
                    switch (mFormMode)
					{
						case EnumAction.Add:
							// create new TreeNode
							TreeNode objNewNode = new TreeNode(voFieldGroup.GroupCode);
							objNewNode.Tag = voFieldGroup;
							// add new node to TreeView
							tvwFieldGroups.Nodes.Add(objNewNode);
							// now add all sub group and field if any
							CreateLeaf(objNewNode);
							// focus on new node
							tvwFieldGroups.Focus();
							tvwFieldGroups.Select();
							tvwFieldGroups.SelectedNode = objNewNode;
							break;
						case EnumAction.Edit:
							// update selected node
							objSelectedNode.Text = voFieldGroup.GroupCode;
							objSelectedNode.Tag = voFieldGroup;
							// clear all old sub group and field if any
							objSelectedNode.Nodes.Clear();
							// now add all new sub group and field if any
							CreateLeaf(objSelectedNode);
							// focus on edited node
							tvwFieldGroups.Focus();
							tvwFieldGroups.Select();
							tvwFieldGroups.SelectedNode = objSelectedNode;
							break;
					}
					// turn form mode to Default
					mFormMode = EnumAction.Default;
					SwitchFormMode();
					PCSMessageBox.Show(ErrorCode.MESSAGE_AFTER_SAVE_DATA);
					EnableButtons(tvwFieldGroups.SelectedNode);
				}
				else
					PCSMessageBox.Show(ErrorCode.MESSAGE_CANNOT_SAVE_TO_DB, MessageBoxIcon.Error);
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

		/// <summary>
		/// Delete the seleceted node
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnDelete_Click(object sender, System.EventArgs e)
		{
			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.WaitCursor;

			#endregion Code Inserted Automatically

			
			const string METHOD_NAME = THIS + ".btnDelete_Click()";
			try
			{
				if (objSelectedNode != null && 
					objSelectedNode.Tag != null &&
					objSelectedNode.Tag is Sys_FieldGroupVO)
				{
					// display confirm message
					DialogResult dlgResult = PCSMessageBox.Show(ErrorCode.MESSAGE_DELETE_RECORD, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
					if (dlgResult == DialogResult.Yes)
					{
						// if selected node is group level 1
						// move all sub group to tree view with new order
						if (voFieldGroup != null && voFieldGroup.GroupLevel == (int)GroupFieldLevel.One)
						{
							foreach (TreeNode objSubNode in objSelectedNode.Nodes)
							{
								// create new TreeNode
								TreeNode objNewNode = objSubNode;
								// move all field of old node to new node
								foreach (TreeNode objFieldNode in objSubNode.Nodes)
								{
									objNewNode.Nodes.Add(objFieldNode);
								}
								// add new node to tree view
								tvwFieldGroups.Nodes.Add(objNewNode);
								// update group order of new node
								Sys_FieldGroupVO voNewFieldGroup = (Sys_FieldGroupVO)objSubNode.Tag;
								voNewFieldGroup.GroupOrder = tvwFieldGroups.Nodes.IndexOf(objNewNode) + 1;
								objNewNode.Tag = voNewFieldGroup;
							}
						}
						// delete the selected record
						boFieldGroup.Delete(objSelectedNode.Tag, arrSelected);
						// remove selected node from TreeView
						tvwFieldGroups.Nodes.Remove(objSelectedNode);
						// reset value of selected object
						voFieldGroup = null;
						// focus on top node
						tvwFieldGroups.Focus();
						tvwFieldGroups.Select();
						if (tvwFieldGroups.Nodes.Count > 0)
						{
							tvwFieldGroups.SelectedNode = tvwFieldGroups.TopNode;
							EnableButtons(tvwFieldGroups.SelectedNode);
						}
						else
						{
							FormControlComponents.ClearForm(grpFieldProperties);
							EnableButtons(null);
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

			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.Default;

			#endregion Code Inserted Automatically

			
		}
		/// <summary>
		/// Move selected node up
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnMoveUp_Click(object sender, System.EventArgs e)
		{
			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.WaitCursor;

			#endregion Code Inserted Automatically

			
			const string METHOD_NAME = THIS + ".btnMoveUp_Click()";
			try
			{
				TreeNode objNode = tvwFieldGroups.SelectedNode;
				if (objNode != null)
				{
					// move up field
					if (objNode.Tag is sys_ReportFieldsVO)
					{
						sys_ReportFieldsVO voCurrentField = (sys_ReportFieldsVO)objNode.Tag;
						sys_ReportFieldsVO voPrevField = (sys_ReportFieldsVO)objNode.PrevVisibleNode.Tag;
						// move within parent node
						TreeNode objParent = objNode.Parent;
						objParent.Nodes.Remove(objNode);
						objParent.Nodes.Insert(objNode.Index - 1, objNode);
						voCurrentField.FieldOrder = voPrevField.FieldOrder;
						voPrevField.FieldOrder = voPrevField.FieldOrder + 1;
						FieldPropertiesBO boFieldProperties = new FieldPropertiesBO();
						boFieldProperties.SwitchFields(voCurrentField, voPrevField);
					}
					else // move up group
					{
						voFieldGroup = (Sys_FieldGroupVO)objNode.Tag;
						Sys_FieldGroupVO voPrevGroup = (Sys_FieldGroupVO)objNode.PrevVisibleNode.Tag;
						// move within tree view
						tvwFieldGroups.Nodes.Remove(objNode);
						tvwFieldGroups.Nodes.Insert(objNode.Index - 1, objNode);
						voFieldGroup.GroupOrder = voPrevGroup.GroupOrder;
						voPrevGroup.GroupOrder = voPrevGroup.GroupOrder + 1;
						boFieldGroup.SwitchFieldGroup(voFieldGroup, voPrevGroup);
					}
					// set focus on selected on
					tvwFieldGroups.Focus();
					tvwFieldGroups.Select();
					tvwFieldGroups.SelectedNode = objNode;
					EnableButtons(tvwFieldGroups.SelectedNode);
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

			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.Default;

			#endregion Code Inserted Automatically

			
		}

		/// <summary>
		/// Move to selected node down
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnMoveDown_Click(object sender, System.EventArgs e)
		{
			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.WaitCursor;

			#endregion Code Inserted Automatically

			
			const string METHOD_NAME = THIS + ".btnMoveDown_Click()";
			try
			{
				TreeNode objNode = tvwFieldGroups.SelectedNode;
				if (objNode != null)
				{
					// move down field
					if (objNode.Tag is sys_ReportFieldsVO)
					{
						sys_ReportFieldsVO voCurrentField = (sys_ReportFieldsVO)objNode.Tag;
						sys_ReportFieldsVO voNextField = (sys_ReportFieldsVO)objNode.NextVisibleNode.Tag;
						voCurrentField.FieldOrder = voNextField.FieldOrder;
						voNextField.FieldOrder = voNextField.FieldOrder - 1;
						FieldPropertiesBO boFieldProperties = new FieldPropertiesBO();
						boFieldProperties.SwitchFields(voCurrentField, voNextField);
						TreeNode objParentNode = objNode.Parent;
						// move within parent node
						objParentNode.Nodes.Remove(objNode);
						objParentNode.Nodes.Insert(objNode.Index + 1, objNode);
					}
					else // move down group
					{
						Sys_FieldGroupVO voNextGroup = (Sys_FieldGroupVO)objNode.NextVisibleNode.Tag;
						// move within tree view
						tvwFieldGroups.Nodes.Remove(objNode);
						tvwFieldGroups.Nodes.Insert(objNode.Index + 1, objNode);
						voFieldGroup.GroupOrder = voNextGroup.GroupOrder;
						voNextGroup.GroupOrder = voNextGroup.GroupOrder - 1;
						boFieldGroup.SwitchFieldGroup(voFieldGroup, voNextGroup);
					}
					// set focus on selected on
					tvwFieldGroups.Focus();
					tvwFieldGroups.Select();
					tvwFieldGroups.SelectedNode = objNode;
					EnableButtons(tvwFieldGroups.SelectedNode);
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

			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.Default;

			#endregion Code Inserted Automatically

			
		}

		/// <summary>
		/// When user change the Level of group, get the associated field or group to be selected
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void cboLevel_SelectedIndexChanged(object sender, EventArgs e)
		{
			const string METHOD_NAME = THIS + ".cboLevel_SelectedIndexChanged()";
			try
			{
				int intLevel = cboLevel.SelectedIndex + 1;
				switch (intLevel)
				{
					case (int)GroupFieldLevel.One:
						// available
						// get all available group level 2 from the tree view
						arrAvailable.Clear();
						foreach (TreeNode objNode in tvwFieldGroups.Nodes)
						{
							if (objNode.Tag != null && objNode.Tag is Sys_FieldGroupVO)
							{
								Sys_FieldGroupVO voGroupLevel2 = (Sys_FieldGroupVO)objNode.Tag;
								if (voGroupLevel2.GroupLevel == (int)GroupFieldLevel.Two &&
									voGroupLevel2.ParentFieldGroupID == 0)
								{
									arrAvailable.Add(voGroupLevel2);
								}
							}
						}
						arrAvailable.TrimToSize();
						if (arrAvailable.Count > 0)
						{
							lstAvailable.DataSource = arrAvailable;
							lstAvailable.DisplayMember = Sys_FieldGroupTable.GROUPCODE_FLD;
							lstAvailable.ValueMember = Sys_FieldGroupTable.FIELDGROUPID_FLD;
						}
						else
						{
							lstAvailable.DataSource = null;
							lstAvailable.Items.Clear();
						}
						break;
					case (int)GroupFieldLevel.Two:
						// available
						arrAvailable.Clear();
						/// list all fields which not belong to any group in this report
						dtbAvailableField = boFieldGroup.GetAvailableFields(mReport.ReportID);
						foreach (DataRow drowField in dtbAvailableField.Rows)
						{
							sys_ReportFieldsVO voReportFields = new sys_ReportFieldsVO();
							voReportFields.ReportID = drowField[sys_ReportFieldsTable.REPORTID_FLD].ToString().Trim();
							voReportFields.FieldOrder = int.Parse(drowField[sys_ReportFieldsTable.FIELDORDER_FLD].ToString().Trim());
							voReportFields.FieldName = drowField[sys_ReportFieldsTable.FIELDNAME_FLD].ToString().Trim();
							voReportFields.FieldCaption = drowField[sys_ReportFieldsTable.FIELDCAPTION_FLD].ToString().Trim();
							voReportFields.FieldCaptionEN = drowField[sys_ReportFieldsTable.FIELDCAPTIONEN_FLD].ToString().Trim();
							voReportFields.FieldCaptionVN = drowField[sys_ReportFieldsTable.FIELDCAPTIONVN_FLD].ToString().Trim();
							voReportFields.FieldCaptionJP = drowField[sys_ReportFieldsTable.FIELDCAPTIONJP_FLD].ToString().Trim();
							voReportFields.Font = drowField[sys_ReportFieldsTable.FONT_FLD].ToString().Trim();
							voReportFields.Visisble = Boolean.Parse(drowField[sys_ReportFieldsTable.VISISBLE_FLD].ToString().Trim());
							voReportFields.Type = int.Parse(drowField[sys_ReportFieldsTable.TYPE_FLD].ToString().Trim());
							voReportFields.PrintPreview = Boolean.Parse(drowField[sys_ReportFieldsTable.PRINTPREVIEW_FLD].ToString().Trim());
							voReportFields.Width = int.Parse(drowField[sys_ReportFieldsTable.WIDTH_FLD].ToString().Trim());
							voReportFields.Format = drowField[sys_ReportFieldsTable.FORMAT_FLD].ToString().Trim();
							voReportFields.Sort = int.Parse(drowField[sys_ReportFieldsTable.SORT_FLD].ToString().Trim());
							voReportFields.GroupBy = Boolean.Parse(drowField[sys_ReportFieldsTable.GROUPBY_FLD].ToString().Trim());
							voReportFields.BottomGroup = Boolean.Parse(drowField[sys_ReportFieldsTable.BOTTOMGROUP_FLD].ToString().Trim());
							voReportFields.SumTopPage = Boolean.Parse(drowField[sys_ReportFieldsTable.SUMTOPPAGE_FLD].ToString().Trim());
							voReportFields.SumBottomPage = Boolean.Parse(drowField[sys_ReportFieldsTable.SUMBOTTOMPAGE_FLD].ToString().Trim());
							voReportFields.SumTopReport = Boolean.Parse(drowField[sys_ReportFieldsTable.SUMTOPREPORT_FLD].ToString().Trim());
							voReportFields.SumBottomReport = Boolean.Parse(drowField[sys_ReportFieldsTable.SUMBOTTOMREPORT_FLD].ToString().Trim());
							if (drowField[sys_ReportFieldsTable.DATATYPE_FLD] != DBNull.Value)
								voReportFields.DataType = int.Parse(drowField[sys_ReportFieldsTable.DATATYPE_FLD].ToString().Trim());
							arrAvailable.Add(voReportFields);
						}
						arrAvailable.TrimToSize();
						if (arrAvailable.Count > 0)
						{
							lstAvailable.DataSource = arrAvailable;
							lstAvailable.DisplayMember = sys_ReportFieldsTable.FIELDNAME_FLD;
							lstAvailable.ValueMember = sys_ReportFieldsTable.FIELDNAME_FLD;
						}
						else
						{
							lstAvailable.DataSource = null;
							lstAvailable.Items.Clear();
						}
						break;
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
		private void btnHelp_Click(object sender, System.EventArgs e)
		{
			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.WaitCursor;

			#endregion Code Inserted Automatically

			
			const string METHOD_NAME = THIS + ".btnHelp_Click()";
			try
			{
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

		private void btnClose_Click(object sender, System.EventArgs e)
		{
			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.WaitCursor;

			#endregion Code Inserted Automatically

			
			const string METHOD_NAME = THIS + ".btnClose_Click()";
			try
			{
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

		/// <summary>
		/// Enable button based on selected tree noew
		/// </summary>
		/// <param name="pobjSelectedNode">Selected Tree Node</param>
		private void EnableButtons(TreeNode pobjSelectedNode)
		{
			try
			{
				if (pobjSelectedNode == null)
				{
					/// disable all control except button add
					btnAdd.Enabled = true;
					btnDelete.Enabled = false;
					btnEdit.Enabled = false;
					btnSave.Enabled = false;
					btnMoveDown.Enabled = false;
					btnMoveUp.Enabled = false;
					grpFieldProperties.Enabled = false;
					return;
				}
				#region Move up and move down button

				if(IsOnlyOneGroup(this.tvwFieldGroups,pobjSelectedNode))
				{
					OnlyOneLayout();
				}
				else if(IsOnlyOneChild(this.tvwFieldGroups,pobjSelectedNode))
				{
					OnlyOneLayout();
				}
				else
				{			
					if(IsFirstGroup(this.tvwFieldGroups, pobjSelectedNode))
					{				
						FirstGroupLayOut();						
					}
					else if(IsLastGroup(this.tvwFieldGroups, pobjSelectedNode))
					{				
						LastGroupLayOut();						
					}
					else if(IsFirstChild(this.tvwFieldGroups, pobjSelectedNode))
					{
						FirstChildLayOut();						
					}
					else if(IsLastChild(this.tvwFieldGroups, pobjSelectedNode))
					{					
						LastChildLayOut();						
					}
					else
					{
						MiddleLayout();
					}
				}
				#endregion 

				// if selected node is report field, disable Edit and Delete button
				if (pobjSelectedNode.Tag is sys_ReportFieldsVO)
				{
					btnEdit.Enabled = false;
					btnDelete.Enabled = false;
				}
				else
				{
					btnEdit.Enabled = true;
					btnDelete.Enabled = true;
				}			
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
		/// Switch form to associated mode
		/// </summary>
		private void SwitchFormMode()
		{
			try
			{
				switch (mFormMode)
				{
					case EnumAction.Add:
						txtGroupCode.Enabled = true;
						cboLevel.Enabled = true;
						txtCaptionEN.Enabled = true;
						txtCaptionVN.Enabled = true;
						txtCaptionJP.Enabled = true;
						lstAvailable.Enabled = true;
						lstSelected.Enabled = true;
						btnAdd.Enabled = false;
						btnEdit.Enabled = false;
						btnDelete.Enabled = false;
                        btnSave.Enabled = true;
						break;
					case EnumAction.Edit:
						txtGroupCode.Enabled = true;
						// unable to change the level of group
						cboLevel.Enabled = false;
						txtCaptionEN.Enabled = true;
						txtCaptionVN.Enabled = true;
						txtCaptionJP.Enabled = true;
						lstAvailable.Enabled = true;
						lstSelected.Enabled = true;
						btnAdd.Enabled = false;
						btnEdit.Enabled = false;
						btnDelete.Enabled = false;
						btnSave.Enabled = true;
						break;
					case EnumAction.Default:
						txtGroupCode.Enabled = false;
						cboLevel.Enabled = false;
						txtCaptionEN.Enabled = false;
						txtCaptionVN.Enabled = false;
						txtCaptionJP.Enabled = false;
						lstAvailable.Enabled = false;
						lstSelected.Enabled = false;
						btnAdd.Enabled = true;
						btnEdit.Enabled = false;
						btnDelete.Enabled = false;
						btnSave.Enabled = false;
						break;
				}
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
		/// Create the Tree with Group Level 1 node
		/// </summary>
		/// <param name="pdtbFieldGroup">List of Field Group</param>
		private void CreateTree(DataTable pdtbFieldGroup)
		{
			try
			{
				// clear all node
				tvwFieldGroups.Nodes.Clear();
				foreach (DataRow drowItem in pdtbFieldGroup.Rows)
				{
					// all parent node
					if (drowItem[Sys_FieldGroupTable.PARENTFIELDGROUPID_FLD] == null
						|| drowItem[Sys_FieldGroupTable.PARENTFIELDGROUPID_FLD] == DBNull.Value
						|| drowItem[Sys_FieldGroupTable.PARENTFIELDGROUPID_FLD].Equals(0))
					{
						// create new FieldGroupVO
						Sys_FieldGroupVO voCurrentGroup = new Sys_FieldGroupVO();
						voCurrentGroup.FieldGroupID = int.Parse(drowItem[Sys_FieldGroupTable.FIELDGROUPID_FLD].ToString());
						voCurrentGroup.GroupCode = drowItem[Sys_FieldGroupTable.GROUPCODE_FLD].ToString().Trim();
						voCurrentGroup.GroupLevel = int.Parse(drowItem[Sys_FieldGroupTable.GROUPLEVEL_FLD].ToString());
						voCurrentGroup.GroupNameEN = drowItem[Sys_FieldGroupTable.GROUPNAMEEN_FLD].ToString().Trim();
						voCurrentGroup.GroupNameJP = drowItem[Sys_FieldGroupTable.GROUPNAMEJP_FLD].ToString().Trim();
						voCurrentGroup.GroupNameVN = drowItem[Sys_FieldGroupTable.GROUPNAMEVN_FLD].ToString().Trim();
						voCurrentGroup.GroupOrder = int.Parse(drowItem[Sys_FieldGroupTable.GROUPORDER_FLD].ToString().Trim());
						voCurrentGroup.ReportID = drowItem[Sys_FieldGroupTable.REPORTID_FLD].ToString().Trim();
						// create new node
						TreeNode objNode = new TreeNode(voCurrentGroup.GroupCode);
						objNode.Tag = voCurrentGroup;
						// add new node to tree view
						tvwFieldGroups.Nodes.Add(objNode);
					}
				}
				// now add child group and leaf
				foreach (TreeNode objNode in tvwFieldGroups.Nodes)
				{
					CreateLeaf(objNode);
				}
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
		/// Create the Tree with Group Level 2 node and Report Field node
		/// </summary>
		/// <param name="pobjNode">Parent Node</param>
		private void CreateLeaf(TreeNode pobjNode)
		{
			try
			{
				Sys_FieldGroupVO voParentGroup = (Sys_FieldGroupVO)pobjNode.Tag;
				if (voParentGroup.GroupLevel == (int)GroupFieldLevel.One)
				{
					// get all child of current node from database
					DataTable dtbChildNodes= boFieldGroup.GetLowerGroup(voParentGroup.FieldGroupID);
					foreach (DataRow drowItem in dtbChildNodes.Rows)
					{
						Sys_FieldGroupVO voCurrentGroup = new Sys_FieldGroupVO();
						voCurrentGroup.FieldGroupID = int.Parse(drowItem[Sys_FieldGroupTable.FIELDGROUPID_FLD].ToString());
						voCurrentGroup.GroupCode = drowItem[Sys_FieldGroupTable.GROUPCODE_FLD].ToString().Trim();
						voCurrentGroup.GroupLevel = int.Parse(drowItem[Sys_FieldGroupTable.GROUPLEVEL_FLD].ToString());
						voCurrentGroup.GroupNameEN = drowItem[Sys_FieldGroupTable.GROUPNAMEEN_FLD].ToString().Trim();
						voCurrentGroup.GroupNameJP = drowItem[Sys_FieldGroupTable.GROUPNAMEJP_FLD].ToString().Trim();
						voCurrentGroup.GroupNameVN = drowItem[Sys_FieldGroupTable.GROUPNAMEVN_FLD].ToString().Trim();
						voCurrentGroup.GroupOrder = int.Parse(drowItem[Sys_FieldGroupTable.GROUPORDER_FLD].ToString().Trim());
						voCurrentGroup.ReportID = drowItem[Sys_FieldGroupTable.REPORTID_FLD].ToString().Trim();
						voCurrentGroup.ParentFieldGroupID = voParentGroup.FieldGroupID;
						// create new node
						TreeNode objNode = new TreeNode(voCurrentGroup.GroupCode);
						objNode.Tag = voCurrentGroup;
						// add new node to parent node
						pobjNode.Nodes.Add(objNode);
						// add field if any
						CreateLeaf(objNode);
					}
				}
				else
				{
					// get all fields of current node
					DataTable dtbFields = boFieldGroup.GetFields(voParentGroup.FieldGroupID, voParentGroup.ReportID);
					foreach (DataRow drowField in dtbFields.Rows)
					{
						// create new field node
						TreeNode objFieldNode = new TreeNode(drowField[sys_ReportFieldsTable.FIELDNAME_FLD].ToString().Trim());
						sys_ReportFieldsVO voReportFields = new sys_ReportFieldsVO();
						voReportFields.ReportID = drowField[sys_ReportFieldsTable.REPORTID_FLD].ToString().Trim();
						voReportFields.FieldOrder = int.Parse(drowField[sys_ReportFieldsTable.FIELDORDER_FLD].ToString().Trim());
						voReportFields.FieldName = drowField[sys_ReportFieldsTable.FIELDNAME_FLD].ToString().Trim();
						voReportFields.FieldCaption = drowField[sys_ReportFieldsTable.FIELDCAPTION_FLD].ToString().Trim();
						voReportFields.FieldCaptionEN = drowField[sys_ReportFieldsTable.FIELDCAPTIONEN_FLD].ToString().Trim();
						voReportFields.FieldCaptionVN = drowField[sys_ReportFieldsTable.FIELDCAPTIONVN_FLD].ToString().Trim();
						voReportFields.FieldCaptionJP = drowField[sys_ReportFieldsTable.FIELDCAPTIONJP_FLD].ToString().Trim();
						voReportFields.Font = drowField[sys_ReportFieldsTable.FONT_FLD].ToString().Trim();
						if (drowField[sys_ReportFieldsTable.VISISBLE_FLD] != DBNull.Value)
							voReportFields.Visisble = Boolean.Parse(drowField[sys_ReportFieldsTable.VISISBLE_FLD].ToString().Trim());
						if (drowField[sys_ReportFieldsTable.TYPE_FLD] != DBNull.Value)
							voReportFields.Type = int.Parse(drowField[sys_ReportFieldsTable.TYPE_FLD].ToString().Trim());
						if (drowField[sys_ReportFieldsTable.PRINTPREVIEW_FLD] != DBNull.Value)
							voReportFields.PrintPreview = Boolean.Parse(drowField[sys_ReportFieldsTable.PRINTPREVIEW_FLD].ToString().Trim());
						if (drowField[sys_ReportFieldsTable.WIDTH_FLD] != DBNull.Value)
							voReportFields.Width = int.Parse(drowField[sys_ReportFieldsTable.WIDTH_FLD].ToString().Trim());
						voReportFields.Format = drowField[sys_ReportFieldsTable.FORMAT_FLD].ToString().Trim();
						if (drowField[sys_ReportFieldsTable.SORT_FLD] != DBNull.Value)
							voReportFields.Sort = int.Parse(drowField[sys_ReportFieldsTable.SORT_FLD].ToString().Trim());
						if (drowField[sys_ReportFieldsTable.GROUPBY_FLD] != DBNull.Value)
							voReportFields.GroupBy = Boolean.Parse(drowField[sys_ReportFieldsTable.GROUPBY_FLD].ToString().Trim());
						if (drowField[sys_ReportFieldsTable.BOTTOMGROUP_FLD] != DBNull.Value)
							voReportFields.BottomGroup = Boolean.Parse(drowField[sys_ReportFieldsTable.BOTTOMGROUP_FLD].ToString().Trim());
						if (drowField[sys_ReportFieldsTable.SUMTOPPAGE_FLD] != DBNull.Value)
							voReportFields.SumTopPage = Boolean.Parse(drowField[sys_ReportFieldsTable.SUMTOPPAGE_FLD].ToString().Trim());
						if (drowField[sys_ReportFieldsTable.SUMBOTTOMPAGE_FLD] != DBNull.Value)
							voReportFields.SumBottomPage = Boolean.Parse(drowField[sys_ReportFieldsTable.SUMBOTTOMPAGE_FLD].ToString().Trim());
						if (drowField[sys_ReportFieldsTable.SUMTOPREPORT_FLD] != DBNull.Value)
							voReportFields.SumTopReport = Boolean.Parse(drowField[sys_ReportFieldsTable.SUMTOPREPORT_FLD].ToString().Trim());
						if (drowField[sys_ReportFieldsTable.SUMBOTTOMREPORT_FLD] != DBNull.Value)
							voReportFields.SumBottomReport = Boolean.Parse(drowField[sys_ReportFieldsTable.SUMBOTTOMREPORT_FLD].ToString().Trim());
						if (drowField[sys_ReportFieldsTable.DATATYPE_FLD] != DBNull.Value)
							voReportFields.DataType = int.Parse(drowField[sys_ReportFieldsTable.DATATYPE_FLD].ToString().Trim());
						objFieldNode.Tag = voReportFields;
						// add field to group level 2
						pobjNode.Nodes.Add(objFieldNode);
					}
				}
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
		/// Assign value of selected node to detail form
		/// </summary>
		/// <param name="pobjSelectedNode">Select Node</param>
		private void AssignValueToControl(TreeNode pobjSelectedNode)
		{
			try
			{
				Sys_FieldGroupVO voSelectedGroup = null;
				if (pobjSelectedNode != null && pobjSelectedNode.Tag != null)
					voSelectedGroup = (Sys_FieldGroupVO)pobjSelectedNode.Tag;
				if (voSelectedGroup != null)
				{
					txtGroupCode.Text = voSelectedGroup.GroupCode;
					txtCaptionEN.Text = voSelectedGroup.GroupNameEN;
					txtCaptionVN.Text = voSelectedGroup.GroupNameVN;
					txtCaptionJP.Text = voSelectedGroup.GroupNameJP;
					if (voSelectedGroup.FieldGroupID > 0)
					{
						cboLevel.SelectedItem = voSelectedGroup.GroupLevel;
						// selected list
						lstSelected.DataSource = null;
						lstSelected.Items.Clear();
						arrSelected.Clear();
						foreach (TreeNode objSubNode in pobjSelectedNode.Nodes)
						{
							if (objSubNode.Tag is Sys_FieldGroupVO)
								arrSelected.Add((Sys_FieldGroupVO)objSubNode.Tag);
							else if (objSubNode.Tag is DataRow)
								arrSelected.Add((sys_ReportFieldsVO)objSubNode.Tag);
						}
						arrSelected.TrimToSize();
						if (arrSelected.Count > 0)
						{
							// bind data source
							lstSelected.DataSource = arrSelected;
							switch (voSelectedGroup.GroupLevel)
							{
								case (int)GroupFieldLevel.One:
									// selected
									lstSelected.DisplayMember = Sys_FieldGroupTable.GROUPCODE_FLD;
									lstSelected.ValueMember = Sys_FieldGroupTable.FIELDGROUPID_FLD;
									break;
								case (int)GroupFieldLevel.Two:
									// selected
									lstSelected.DisplayMember = sys_ReportFieldsTable.FIELDNAME_FLD;
									lstSelected.ValueMember = sys_ReportFieldsTable.FIELDNAME_FLD;
									break;
							}
						}
						else
						{
							lstSelected.DataSource = null;
							lstSelected.Items.Clear();
						}
					}
					// available list
					lstAvailable.DataSource = null;
					lstAvailable.Items.Clear();
					switch (voSelectedGroup.GroupLevel)
					{
						case (int)GroupFieldLevel.One:
							// available
							// get all available group level 2 from the tree view
							arrAvailable.Clear();
							foreach (TreeNode objNode in tvwFieldGroups.Nodes)
							{
								if (objNode.Tag != null && objNode.Tag is Sys_FieldGroupVO)
								{
									Sys_FieldGroupVO voGroupLevel2 = (Sys_FieldGroupVO)objNode.Tag;
									if (voGroupLevel2.GroupLevel == (int)GroupFieldLevel.Two &&
										voGroupLevel2.ParentFieldGroupID == 0)
									{
										arrAvailable.Add(voGroupLevel2);
									}
								}
							}
							arrAvailable.TrimToSize();
							if (arrAvailable.Count > 0)
							{
								lstAvailable.DataSource = arrAvailable;
								lstAvailable.DisplayMember = Sys_FieldGroupTable.GROUPCODE_FLD;
								lstAvailable.ValueMember = Sys_FieldGroupTable.FIELDGROUPID_FLD;
							}
							else
							{
								lstAvailable.DataSource = null;
								lstAvailable.Items.Clear();
							}
							break;
						case (int)GroupFieldLevel.Two:
							// available
							arrAvailable.Clear();
							dtbAvailableField = boFieldGroup.GetAvailableFields(voSelectedGroup.ReportID);
							foreach (DataRow drowField in dtbAvailableField.Rows)
							{
								sys_ReportFieldsVO voReportFields = new sys_ReportFieldsVO();
								voReportFields.ReportID = drowField[sys_ReportFieldsTable.REPORTID_FLD].ToString().Trim();
								voReportFields.FieldOrder = int.Parse(drowField[sys_ReportFieldsTable.FIELDORDER_FLD].ToString().Trim());
								voReportFields.FieldName = drowField[sys_ReportFieldsTable.FIELDNAME_FLD].ToString().Trim();
								voReportFields.FieldCaption = drowField[sys_ReportFieldsTable.FIELDCAPTION_FLD].ToString().Trim();
								voReportFields.FieldCaptionEN = drowField[sys_ReportFieldsTable.FIELDCAPTIONEN_FLD].ToString().Trim();
								voReportFields.FieldCaptionVN = drowField[sys_ReportFieldsTable.FIELDCAPTIONVN_FLD].ToString().Trim();
								voReportFields.FieldCaptionJP = drowField[sys_ReportFieldsTable.FIELDCAPTIONJP_FLD].ToString().Trim();
								voReportFields.Font = drowField[sys_ReportFieldsTable.FONT_FLD].ToString().Trim();
								voReportFields.Visisble = Boolean.Parse(drowField[sys_ReportFieldsTable.VISISBLE_FLD].ToString().Trim());
								voReportFields.Type = int.Parse(drowField[sys_ReportFieldsTable.TYPE_FLD].ToString().Trim());
								voReportFields.PrintPreview = Boolean.Parse(drowField[sys_ReportFieldsTable.PRINTPREVIEW_FLD].ToString().Trim());
								voReportFields.Width = int.Parse(drowField[sys_ReportFieldsTable.WIDTH_FLD].ToString().Trim());
								voReportFields.Format = drowField[sys_ReportFieldsTable.FORMAT_FLD].ToString().Trim();
								voReportFields.Sort = int.Parse(drowField[sys_ReportFieldsTable.SORT_FLD].ToString().Trim());
								voReportFields.GroupBy = Boolean.Parse(drowField[sys_ReportFieldsTable.GROUPBY_FLD].ToString().Trim());
								voReportFields.BottomGroup = Boolean.Parse(drowField[sys_ReportFieldsTable.BOTTOMGROUP_FLD].ToString().Trim());
								voReportFields.SumTopPage = Boolean.Parse(drowField[sys_ReportFieldsTable.SUMTOPPAGE_FLD].ToString().Trim());
								voReportFields.SumBottomPage = Boolean.Parse(drowField[sys_ReportFieldsTable.SUMBOTTOMPAGE_FLD].ToString().Trim());
								voReportFields.SumTopReport = Boolean.Parse(drowField[sys_ReportFieldsTable.SUMTOPREPORT_FLD].ToString().Trim());
								voReportFields.SumBottomReport = Boolean.Parse(drowField[sys_ReportFieldsTable.SUMBOTTOMREPORT_FLD].ToString().Trim());
								if (drowField[sys_ReportFieldsTable.DATATYPE_FLD] != DBNull.Value)
									voReportFields.DataType = int.Parse(drowField[sys_ReportFieldsTable.DATATYPE_FLD].ToString().Trim());
								arrAvailable.Add(voReportFields);
							}
							arrAvailable.TrimToSize();
							if (arrAvailable.Count > 0)
							{
								lstAvailable.DataSource = arrAvailable;
								lstAvailable.DisplayMember = sys_ReportFieldsTable.FIELDNAME_FLD;
								lstAvailable.ValueMember = sys_ReportFieldsTable.FIELDNAME_FLD;
							}
							else
							{
								lstAvailable.DataSource = null;
								lstAvailable.Items.Clear();
							}
							break;
					}
				}
				else
					FormControlComponents.ClearForm(grpFieldProperties);
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
		/// Save Data to database
		/// </summary>
		/// <returns>True if succeed, False if failure</returns>
		private bool SaveData()
		{
			try
			{
				switch (mFormMode)
				{
					case EnumAction.Add:
						voFieldGroup = new Sys_FieldGroupVO();
						voFieldGroup.GroupCode = txtGroupCode.Text.Trim();
						voFieldGroup.GroupLevel = cboLevel.SelectedIndex + 1;
						voFieldGroup.GroupNameEN = txtCaptionEN.Text.Trim();
						voFieldGroup.GroupNameJP = txtCaptionJP.Text.Trim();
						voFieldGroup.GroupNameVN = txtCaptionVN.Text.Trim();
						voFieldGroup.ReportID = mReport.ReportID;
						// list of selected group/field
						voFieldGroup.FieldGroupID = boFieldGroup.AddAndReturnID(voFieldGroup, arrSelected);
						break;
					case EnumAction.Edit:
						voFieldGroup.GroupCode = txtGroupCode.Text.Trim();
						//voFieldGroup.GroupLevel = int.Parse(cboLevel.SelectedText.Trim());
						voFieldGroup.GroupNameEN = txtCaptionEN.Text.Trim();
						voFieldGroup.GroupNameJP = txtCaptionJP.Text.Trim();
						voFieldGroup.GroupNameVN = txtCaptionVN.Text.Trim();
						boFieldGroup.Update(voFieldGroup, arrSelected, arrAvailable);
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


		#region layout control	

		/// <summary>
		/// Thachnn 25/Oct/2005
		/// Don't allow move up group
		/// </summary>
		private void FirstGroupLayOut()
		{			
			btnMoveUp.Enabled = false;
			btnMoveDown.Enabled = true;			
		}

		/// <summary>
		/// Thachnn 25/Oct/2005
		/// Don't allow move down group
		/// </summary>
		private void LastGroupLayOut()
		{		
			btnMoveUp.Enabled = true;
			btnMoveDown.Enabled = false;		
		}
		
		/// <summary>
		/// Thachnn 25/Oct/2005
		/// Don't allow move up report
		/// </summary>
		private void FirstChildLayOut()
		{				
			btnMoveUp.Enabled = false;
			btnMoveDown.Enabled = true;			
		}

		/// <summary>
		/// Thachnn 25/Oct/2005
		/// Don't allow move down report
		/// </summary>
		private void LastChildLayOut()
		{				
			btnMoveUp.Enabled = true;
			btnMoveDown.Enabled = false;			
		}

		/// <summary>
		/// Thachnn 25/Oct/2005
		/// Allow move up and move down current node (group or report)
		/// </summary>
		private void MiddleLayout()
		{			
			btnMoveUp.Enabled = true;
			btnMoveDown.Enabled = true;			
		}

		/// <summary>
		/// Thachnn 25/Oct/2005
		/// Do not allow to move up or move down current node (group or report)
		/// </summary>
		private void OnlyOneLayout()
		{				
			btnMoveUp.Enabled = false;
			btnMoveDown.Enabled = false;			
		}


		#endregion	layout control

		#region Thachnn report management layout control


		/// <summary>
		/// Thachnn 25/Oct/2005
		/// check whether ptnNOde is First child report in its report group.
		/// @Is child
		/// @
		/// </summary>
		/// <param name="ptvw">treeview object to check</param>
		/// <param name="ptnNode">tree node to check whether is firstchild in group of the provided treeview</param>
		/// <returns>true if ptnNode is a child report and is First Node in its group</returns>
		private bool IsFirstChild(TreeView ptvw, TreeNode ptnNode)
		{
			bool blnRet = false;
			
			try
			{
				if (IsChild(ptnNode))
				{			
					if(ptvw.Nodes.Count >0 && ptnNode.Parent != null)
					{
						if(IsChild(ptnNode) && ptnNode.Parent.FirstNode.Equals(ptnNode))
						{
							blnRet = true;
						}						
					}
				}
			}
			catch(Exception ex)
			{
				throw ex;
			}

			return blnRet;
		}

		/// <summary>
		/// Thachnn 25/Oct/2005
		/// check whether ptnNOde is Last child report in its report group
		/// </summary>
		/// <param name="ptvw">treeview object to check</param>
		/// <param name="ptnNode">tree node to check whether is last child in group of the provided treeview</param>
		/// <returns>true if ptnNode is a child report and is last report Node in its group</returns>
		private bool IsLastChild(TreeView ptvw, TreeNode ptnNode)
		{
			bool blnRet = false;
										
			if (IsChild(ptnNode))
			{
				if(ptvw.Nodes.Count >0 && ptnNode.Parent != null)
				{
					if(ptnNode.Parent.LastNode.Equals(ptnNode))
					{
						blnRet = true;
					}
				}
			}		

			return blnRet;
		}

		/// <summary>
		/// Thachnn 25/Oct/2005
		/// Check whether ptnNode is the Group node and is the only one Group in the provided tree view
		/// </summary>
		/// <param name="ptvw">tree view object to check</param>
		/// <param name="ptnNode">tree node to check</param>
		/// <returns>true if ptnNode is only one group in the tree view</returns>
		private bool IsOnlyOneGroup(TreeView ptvw, TreeNode ptnNode)
		{
			bool blnRet = false;

			try
			{
				if(ptvw.Nodes.Count  > 0 && (IsGroup(ptnNode) || IsSubGroup(ptnNode)) && IsFirstGroup(ptvw, ptnNode) && IsLastGroup(ptvw,ptnNode) )
				{				
					blnRet = true;				
				}
			}
			catch(Exception ex)
			{
				throw ex;
			}
			
			return blnRet;
		}

		/// <summary>
		/// Thachnn 25/Oct/2005
		/// Check whether ptnNode is the Child report and is the only one Report in its Group in the provided tree view
		/// </summary>
		/// <param name="ptvw">tree view to check</param>
		/// <param name="ptnNode">report tree node to check</param>
		/// <returns>true if ptnNode is only one child report in its group</returns>
		private bool IsOnlyOneChild(TreeView ptvw, TreeNode ptnNode)
		{
			bool blnRet = false;

			try
			{
				if(ptvw.Nodes.Count  > 0 && IsChild(ptnNode) && IsFirstChild(ptvw, ptnNode) && IsLastChild(ptvw,ptnNode) )
				{				
					blnRet = true;					
				}
			}
			catch(Exception ex)
			{
				throw ex;
			}		

			return blnRet;
		}

		/// <summary>
		/// Thachnn 25/Oct/2005
		/// check whether ptnNOde is First Root Group in provided tree view
		/// </summary>
		/// <param name="ptvw">tree view object to check</param>
		/// <param name="ptnNode">tree node object to check</param>
		/// <returns>true if ptnNode is Root Group and is the first Group in the tree view</returns>
		private bool IsFirstGroup(TreeView ptvw, TreeNode ptnNode)
		{
			bool blnRet = false;
			if(ptvw.Nodes.Count >0)
			{
				if(IsGroup(ptnNode) )
				{	
					if( ptvw.Nodes[0].Equals(ptnNode))
					{
						blnRet = true;
					}
				}
				else if(IsSubGroup(ptnNode) && ptnNode.PrevNode == null )	// is sub(level 2) and prev node is parent
				{					
                    blnRet = true;                    
				}
			}

			return blnRet;
		}

		/// <summary>
		/// Thachnn 25/Oct/2005
		/// check whether ptnNOde is Last Root Group in provided tree view
		/// </summary>
		/// <param name="ptvw">tree view object to check</param>
		/// <param name="ptnNode">tree node object to check</param>
		/// <returns>true if ptnNode is Root Group and is the Last Group in the tree view</returns>
		private bool IsLastGroup(TreeView ptvw, TreeNode ptnNode)
		{
			bool blnRet = false;

			if( ptvw.Nodes.Count >0)
			{
				if(IsGroup(ptnNode))
				{
					if(ptvw.Nodes[ptvw.Nodes.Count-1].Equals(ptnNode))	// Alone last Root
					{
						blnRet = true;
					}						
					else if(IsChild(ptvw.Nodes[ptvw.Nodes.Count-1]))	// last node is child, and parent of that child is this node
					{
						if(ptvw.Nodes[ptvw.Nodes.Count-1].Parent.Equals(ptnNode))
							blnRet = true;
					}				
				}					
				else if(IsSubGroup(ptnNode) && ptnNode.NextNode == null )	// is sub(level 2) and nextnode
				{					
					blnRet = true;                    
				}				
			}
			return blnRet;
		}

		/// <summary>
		/// Thachnn 25/Oct/2005
		/// Get Previous report in current group and in current tree view
		/// </summary>
		/// <param name="ptvw">tree view to find</param>
		/// <param name="ptnNode">current report node</param>
		/// <returns>previous report in current group, null if not found</returns>
		private TreeNode GetPrevChild(TreeView ptvw, TreeNode ptnNode)
		{
			TreeNode tnRet = null;
			try
			{
				if(IsChild(ptnNode))
				{
					if (IsFirstChild(ptvw,ptnNode))
					{
						return null;
					}
					else
					{
						tnRet = ptnNode.PrevNode;
					}
				}
				else
					return null;
			}
			catch(Exception ex)
			{
				throw ex;
			}
			
			return tnRet;
		}

		/// <summary>
		/// Thachnn 25/Oct/2005
		/// Get Next report in current group and in current tree view
		/// </summary>
		/// <param name="ptvw">tree view to find</param>
		/// <param name="ptnNode">current report node</param>
		/// <returns>next report in current group, null if not found</returns>
		private TreeNode GetNextChild(TreeView ptvw, TreeNode ptnNode)
		{
			TreeNode tnRet = null;
			try
			{
				if(IsChild(ptnNode))
				{
					if (IsLastChild(ptvw,ptnNode))				
					{
						return null;
					}
					else
					{
						tnRet = ptnNode.NextNode;
					}
				}
				else
				{
					return null;
				}
			}
			catch(Exception ex)
			{
				throw ex;
			}		

			return tnRet;
		}

		/// <summary>
		/// Thachnn 25/Oct/2005
		/// Get previous group of current group
		/// </summary>
		/// <param name="ptvw">tree view to find</param>
		/// <param name="ptnNode">current tree node</param>
		/// <returns>previous group of current group, null if there is no prev group</returns>
		private TreeNode GetPrevGroup(TreeView ptvw, TreeNode ptnNode)
		{
			TreeNode tnRet = null;

			if (IsGroup(ptnNode))	// root group of tree (level 1)
			{			
				if (IsFirstGroup(ptvw,ptnNode))	// first root group of tree
				{
					return null;
				}
				else // can be another root group or level 1 subgroup or child
				{
					if(ptnNode.PrevNode != null) //process the prevnode
					{
						if(IsGroup(ptnNode.PrevNode))	// prev is Group
						{
							tnRet = ptnNode.PrevNode;
						}
						else if(IsSubGroup(ptnNode.PrevNode))	// prev is subgroup
						{
							if(ptnNode.PrevNode.Parent == null)	// prev is level 1 subgroup
							{
								tnRet = ptnNode.PrevNode;
							}
							else	// prev is level 2 subgroup
							{
								tnRet = ptnNode.PrevNode.Parent;
							}
						}
						else	// prev is child
						{
							if(ptnNode.PrevNode.Parent.Parent == null)	// prev is child of Subgroup Level 1
							{
								tnRet = ptnNode.PrevNode.Parent;
							}
							else	// prev is child of level 2 subgroup
							{
								tnRet = ptnNode.PrevNode.Parent.Parent;
							}
						}
					}
					else //prev node == null
					{
						return null;
					}
				}
			}
			else if(IsSubGroup(ptnNode))	//level 2 group
			{
				if(ptnNode.PrevNode != null) //process the prevnode
				{
					if(ptnNode.PrevNode.Equals(ptnNode.Parent) )	//ptnNode is first subgroup
					{
						return null;
					}
					else
					{
						if(IsSubGroup(ptnNode.PrevNode) )	// prev is level 2 group
						{
							tnRet = ptnNode.PrevNode;
						}
						else if(IsChild(ptnNode.PrevNode) )	// prev is child
						{
							tnRet = ptnNode.PrevNode.Parent;                        						
						}
					}
				}
				else	// prev node == null
				{
					return null;
				}
			}
			else	// is child
			{
				return null;
			}
			
			return tnRet;
		}

		/// <summary>
		/// Thachnn 25/Oct/2005
		/// Get Next group of current group
		/// </summary>
		/// <param name="ptvw">tree view to find</param>
		/// <param name="ptnNode">current tree node</param>
		/// <returns>next group of current group, null if there is no next group</returns>
		private TreeNode GetNextGroup(TreeView ptvw, TreeNode ptnNode)
		{
			TreeNode tnRet = null;

			if (IsGroup(ptnNode))	// current node is level 1 group
			{			
				if (IsLastGroup(ptvw,ptnNode))	//current node is last root group
				{
					return null;
				}
				else	// can be another level 1 group
				{				
					if(ptnNode.NextNode != null)	// process the next node of current node
					{
						if (IsGroup(ptnNode.NextNode)) //next node is level 1 group, current group has no child
						{
							tnRet = ptnNode.NextNode;
						}
						else // next node is a child (of current node), get the last child (of current node), get a node next to the last child
						{
							if (ptnNode.LastNode != null && ptnNode.LastNode.NextNode != null)
							{
								tnRet = ptnNode.LastNode.NextNode;								
							}
						}
					}
					else	//next node == null
					{
						return null;
					}
				}
			}	
			else if(IsSubGroup(ptnNode))	// level 2 group
			{
				if(ptnNode.NextNode != null) //process the NextNode
				{
					if(IsGroup(ptnNode.NextNode) )	//ptnNode is first subgroup
					{
						return null;
					}
					else
					{
						if(IsSubGroup(ptnNode.NextNode) )	// NextNode is level 2 group
						{
							tnRet = ptnNode.NextNode;
						}
						else if(IsChild(ptnNode.NextNode) )	// NextNode is child
						{
							if(ptnNode.LastNode != null && ptnNode.LastNode.NextNode != null)
							{
								tnRet = ptnNode.LastNode.NextNode;
							}
						}
					}
				}
				else	// NextNode node == null
				{
					return null;
				}

			}
			else	// is child
			{
				return null;
			}

			return tnRet;
		}

		/// <summary>
		/// Thachnn 25/Oct/2005
		/// check whether provided node is a Group
		/// </summary>
		/// <param name="ptnNode">tree node to check</param>
		/// <returns>true if ptnNode is a group</returns>
		private bool IsGroup(TreeNode ptnNode)
		{
			try
			{
				return (!IsChild(ptnNode)) && (!IsSubGroup(ptnNode));
			}
			catch(Exception ex)
			{
				throw ex;
			}
		}
		/// <summary>
		/// Thachnn 25/Oct/2005
		/// check whether provided node is a SubGroup
		/// </summary>
		/// <param name="ptnNode">tree node to check</param>
		/// <returns>true if ptnNode is a group</returns>
		private bool IsSubGroup(TreeNode ptnNode)
		{
			try
			{
				if (ptnNode == null || ptnNode.Tag == null || !(ptnNode.Tag is Sys_FieldGroupVO))
					return false;
				Sys_FieldGroupVO voSubGroup = (Sys_FieldGroupVO)ptnNode.Tag;
				if (voSubGroup.GroupLevel == (int)GroupFieldLevel.Two)
				{
					return true;
				}
				else
					return false;
			}
			catch(Exception ex)
			{
				throw ex;
			}			
		}

		/// <summary>
		/// Thachnn 25/Oct/2005
		/// check whether provided node is a Report (not a group)
		/// </summary>
		/// <param name="ptnNode">tree node to check</param>
		/// <returns>true if ptnNode is a report</returns>
		private bool IsChild(TreeNode ptnNode)
		{
			try
			{
				if (ptnNode == null || ptnNode.Tag == null)
					return false;
				if (ptnNode.Tag is sys_ReportFieldsVO)
				{
					return true;
				}
				else
					return false;
			}
			catch(Exception ex)
			{
				throw ex;
			}			
		}
	
		
		#endregion	Thachnn report management layout control

	}
}