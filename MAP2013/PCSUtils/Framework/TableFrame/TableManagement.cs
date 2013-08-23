using System;
using System.Collections;
using System.Windows.Forms;
using PCSComUtils.Common;
using PCSComUtils.Common.BO;
using PCSComUtils.Framework.TableFrame.BO;
using PCSComUtils.Framework.TableFrame.DS;
using PCSComUtils.PCSExc;
using PCSUtils.Log;
using PCSUtils.Utils;

namespace PCSUtils.Framework.TableFrame
{
	/// <summary>
	/// Summary description for TableManagement.
	/// </summary>
	public class TableManagement : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button btnMoveDown;
		private System.Windows.Forms.Button btnMoveUp;
		private System.Windows.Forms.Button btnAddGroup;
		private System.Windows.Forms.Button btnEditGroup;
		private System.Windows.Forms.Button btnCopyGroup;
		private System.Windows.Forms.Button btnViewTable;
		private System.Windows.Forms.Button btnDeleteTable;
		private System.Windows.Forms.Button btnDeleteGroup;
		private System.Windows.Forms.Button btnEditTable;
		private System.Windows.Forms.Button btnCopyTable;
		private System.Windows.Forms.Button btnPasteTable;
		private System.Windows.Forms.Button btnAddTable;
		private System.ComponentModel.IContainer components;
		private System.Windows.Forms.TreeView tvwTables;
		private System.Windows.Forms.Button btnClose;

		private FormControlComponents objFCC;
		private sys_TableGroupVO voTableGroup;
		private sys_TableVO voSysTable;
		private sys_TableVO voCopySysTable = null;
		private int intGroupID;
		private int intTableID;
		private bool blnCopy = false;
		private int intCopyGroupID = -1;
		private string strTableName;
		private System.Windows.Forms.Button btnHelp;
		private const string THIS = "PCSUtils.Framework.TableFrame.TableManagement";

		private const string TYPE_GROUP = "GROUP";
		private System.Windows.Forms.ImageList imglIcons;
		private const string TYPE_TABLE = "TABLE";
		private const int EXPAND_GROUP_IMG = 0;
		private const int COLLAPSE_GROUP_IMG = 1;
		//private const int TABLE_IMG = 2;
		private const int TABLE_IMG_EDIT = 3;
		private const int TABLE_IMG_VIEW = 4;
		public TableManagement()
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

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(TableManagement));
			this.tvwTables = new System.Windows.Forms.TreeView();
			this.imglIcons = new System.Windows.Forms.ImageList(this.components);
			this.label1 = new System.Windows.Forms.Label();
			this.btnDeleteTable = new System.Windows.Forms.Button();
			this.btnDeleteGroup = new System.Windows.Forms.Button();
			this.btnMoveDown = new System.Windows.Forms.Button();
			this.btnMoveUp = new System.Windows.Forms.Button();
			this.btnEditTable = new System.Windows.Forms.Button();
			this.btnCopyTable = new System.Windows.Forms.Button();
			this.btnPasteTable = new System.Windows.Forms.Button();
			this.btnAddGroup = new System.Windows.Forms.Button();
			this.btnEditGroup = new System.Windows.Forms.Button();
			this.btnCopyGroup = new System.Windows.Forms.Button();
			this.btnAddTable = new System.Windows.Forms.Button();
			this.btnViewTable = new System.Windows.Forms.Button();
			this.btnClose = new System.Windows.Forms.Button();
			this.btnHelp = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// tvwTables
			// 
			this.tvwTables.AccessibleDescription = resources.GetString("tvwTables.AccessibleDescription");
			this.tvwTables.AccessibleName = resources.GetString("tvwTables.AccessibleName");
			this.tvwTables.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tvwTables.Anchor")));
			this.tvwTables.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tvwTables.BackgroundImage")));
			this.tvwTables.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tvwTables.Dock")));
			this.tvwTables.Enabled = ((bool)(resources.GetObject("tvwTables.Enabled")));
			this.tvwTables.Font = ((System.Drawing.Font)(resources.GetObject("tvwTables.Font")));
			this.tvwTables.ImageIndex = ((int)(resources.GetObject("tvwTables.ImageIndex")));
			this.tvwTables.ImageList = this.imglIcons;
			this.tvwTables.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tvwTables.ImeMode")));
			this.tvwTables.Indent = ((int)(resources.GetObject("tvwTables.Indent")));
			this.tvwTables.ItemHeight = ((int)(resources.GetObject("tvwTables.ItemHeight")));
			this.tvwTables.Location = ((System.Drawing.Point)(resources.GetObject("tvwTables.Location")));
			this.tvwTables.Name = "tvwTables";
			this.tvwTables.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tvwTables.RightToLeft")));
			this.tvwTables.SelectedImageIndex = ((int)(resources.GetObject("tvwTables.SelectedImageIndex")));
			this.tvwTables.Size = ((System.Drawing.Size)(resources.GetObject("tvwTables.Size")));
			this.tvwTables.TabIndex = ((int)(resources.GetObject("tvwTables.TabIndex")));
			this.tvwTables.Text = resources.GetString("tvwTables.Text");
			this.tvwTables.Visible = ((bool)(resources.GetObject("tvwTables.Visible")));
			this.tvwTables.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tvwTables_KeyDown);
			this.tvwTables.AfterExpand += new System.Windows.Forms.TreeViewEventHandler(this.tvwTables_AfterExpand);
			this.tvwTables.AfterCollapse += new System.Windows.Forms.TreeViewEventHandler(this.tvwTables_AfterCollapse);
			this.tvwTables.DoubleClick += new System.EventHandler(this.tvwTables_DoubleClick);
			this.tvwTables.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvwTables_AfterSelect);
			// 
			// imglIcons
			// 
			this.imglIcons.ImageSize = ((System.Drawing.Size)(resources.GetObject("imglIcons.ImageSize")));
			this.imglIcons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imglIcons.ImageStream")));
			this.imglIcons.TransparentColor = System.Drawing.Color.Transparent;
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
			this.label1.Visible = ((bool)(resources.GetObject("label1.Visible")));
			// 
			// btnDeleteTable
			// 
			this.btnDeleteTable.AccessibleDescription = resources.GetString("btnDeleteTable.AccessibleDescription");
			this.btnDeleteTable.AccessibleName = resources.GetString("btnDeleteTable.AccessibleName");
			this.btnDeleteTable.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnDeleteTable.Anchor")));
			this.btnDeleteTable.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnDeleteTable.BackgroundImage")));
			this.btnDeleteTable.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnDeleteTable.Dock")));
			this.btnDeleteTable.Enabled = ((bool)(resources.GetObject("btnDeleteTable.Enabled")));
			this.btnDeleteTable.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnDeleteTable.FlatStyle")));
			this.btnDeleteTable.Font = ((System.Drawing.Font)(resources.GetObject("btnDeleteTable.Font")));
			this.btnDeleteTable.Image = ((System.Drawing.Image)(resources.GetObject("btnDeleteTable.Image")));
			this.btnDeleteTable.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnDeleteTable.ImageAlign")));
			this.btnDeleteTable.ImageIndex = ((int)(resources.GetObject("btnDeleteTable.ImageIndex")));
			this.btnDeleteTable.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnDeleteTable.ImeMode")));
			this.btnDeleteTable.Location = ((System.Drawing.Point)(resources.GetObject("btnDeleteTable.Location")));
			this.btnDeleteTable.Name = "btnDeleteTable";
			this.btnDeleteTable.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnDeleteTable.RightToLeft")));
			this.btnDeleteTable.Size = ((System.Drawing.Size)(resources.GetObject("btnDeleteTable.Size")));
			this.btnDeleteTable.TabIndex = ((int)(resources.GetObject("btnDeleteTable.TabIndex")));
			this.btnDeleteTable.Text = resources.GetString("btnDeleteTable.Text");
			this.btnDeleteTable.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnDeleteTable.TextAlign")));
			this.btnDeleteTable.Visible = ((bool)(resources.GetObject("btnDeleteTable.Visible")));
			this.btnDeleteTable.Click += new System.EventHandler(this.btnDeleteTable_Click);
			// 
			// btnDeleteGroup
			// 
			this.btnDeleteGroup.AccessibleDescription = resources.GetString("btnDeleteGroup.AccessibleDescription");
			this.btnDeleteGroup.AccessibleName = resources.GetString("btnDeleteGroup.AccessibleName");
			this.btnDeleteGroup.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnDeleteGroup.Anchor")));
			this.btnDeleteGroup.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnDeleteGroup.BackgroundImage")));
			this.btnDeleteGroup.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnDeleteGroup.Dock")));
			this.btnDeleteGroup.Enabled = ((bool)(resources.GetObject("btnDeleteGroup.Enabled")));
			this.btnDeleteGroup.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnDeleteGroup.FlatStyle")));
			this.btnDeleteGroup.Font = ((System.Drawing.Font)(resources.GetObject("btnDeleteGroup.Font")));
			this.btnDeleteGroup.Image = ((System.Drawing.Image)(resources.GetObject("btnDeleteGroup.Image")));
			this.btnDeleteGroup.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnDeleteGroup.ImageAlign")));
			this.btnDeleteGroup.ImageIndex = ((int)(resources.GetObject("btnDeleteGroup.ImageIndex")));
			this.btnDeleteGroup.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnDeleteGroup.ImeMode")));
			this.btnDeleteGroup.Location = ((System.Drawing.Point)(resources.GetObject("btnDeleteGroup.Location")));
			this.btnDeleteGroup.Name = "btnDeleteGroup";
			this.btnDeleteGroup.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnDeleteGroup.RightToLeft")));
			this.btnDeleteGroup.Size = ((System.Drawing.Size)(resources.GetObject("btnDeleteGroup.Size")));
			this.btnDeleteGroup.TabIndex = ((int)(resources.GetObject("btnDeleteGroup.TabIndex")));
			this.btnDeleteGroup.Text = resources.GetString("btnDeleteGroup.Text");
			this.btnDeleteGroup.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnDeleteGroup.TextAlign")));
			this.btnDeleteGroup.Visible = ((bool)(resources.GetObject("btnDeleteGroup.Visible")));
			this.btnDeleteGroup.Click += new System.EventHandler(this.btnDeleteGroup_Click);
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
			// btnEditTable
			// 
			this.btnEditTable.AccessibleDescription = resources.GetString("btnEditTable.AccessibleDescription");
			this.btnEditTable.AccessibleName = resources.GetString("btnEditTable.AccessibleName");
			this.btnEditTable.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnEditTable.Anchor")));
			this.btnEditTable.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnEditTable.BackgroundImage")));
			this.btnEditTable.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnEditTable.Dock")));
			this.btnEditTable.Enabled = ((bool)(resources.GetObject("btnEditTable.Enabled")));
			this.btnEditTable.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnEditTable.FlatStyle")));
			this.btnEditTable.Font = ((System.Drawing.Font)(resources.GetObject("btnEditTable.Font")));
			this.btnEditTable.Image = ((System.Drawing.Image)(resources.GetObject("btnEditTable.Image")));
			this.btnEditTable.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnEditTable.ImageAlign")));
			this.btnEditTable.ImageIndex = ((int)(resources.GetObject("btnEditTable.ImageIndex")));
			this.btnEditTable.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnEditTable.ImeMode")));
			this.btnEditTable.Location = ((System.Drawing.Point)(resources.GetObject("btnEditTable.Location")));
			this.btnEditTable.Name = "btnEditTable";
			this.btnEditTable.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnEditTable.RightToLeft")));
			this.btnEditTable.Size = ((System.Drawing.Size)(resources.GetObject("btnEditTable.Size")));
			this.btnEditTable.TabIndex = ((int)(resources.GetObject("btnEditTable.TabIndex")));
			this.btnEditTable.Text = resources.GetString("btnEditTable.Text");
			this.btnEditTable.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnEditTable.TextAlign")));
			this.btnEditTable.Visible = ((bool)(resources.GetObject("btnEditTable.Visible")));
			this.btnEditTable.Click += new System.EventHandler(this.btnEditTable_Click);
			// 
			// btnCopyTable
			// 
			this.btnCopyTable.AccessibleDescription = resources.GetString("btnCopyTable.AccessibleDescription");
			this.btnCopyTable.AccessibleName = resources.GetString("btnCopyTable.AccessibleName");
			this.btnCopyTable.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnCopyTable.Anchor")));
			this.btnCopyTable.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCopyTable.BackgroundImage")));
			this.btnCopyTable.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnCopyTable.Dock")));
			this.btnCopyTable.Enabled = ((bool)(resources.GetObject("btnCopyTable.Enabled")));
			this.btnCopyTable.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnCopyTable.FlatStyle")));
			this.btnCopyTable.Font = ((System.Drawing.Font)(resources.GetObject("btnCopyTable.Font")));
			this.btnCopyTable.Image = ((System.Drawing.Image)(resources.GetObject("btnCopyTable.Image")));
			this.btnCopyTable.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnCopyTable.ImageAlign")));
			this.btnCopyTable.ImageIndex = ((int)(resources.GetObject("btnCopyTable.ImageIndex")));
			this.btnCopyTable.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnCopyTable.ImeMode")));
			this.btnCopyTable.Location = ((System.Drawing.Point)(resources.GetObject("btnCopyTable.Location")));
			this.btnCopyTable.Name = "btnCopyTable";
			this.btnCopyTable.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnCopyTable.RightToLeft")));
			this.btnCopyTable.Size = ((System.Drawing.Size)(resources.GetObject("btnCopyTable.Size")));
			this.btnCopyTable.TabIndex = ((int)(resources.GetObject("btnCopyTable.TabIndex")));
			this.btnCopyTable.Text = resources.GetString("btnCopyTable.Text");
			this.btnCopyTable.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnCopyTable.TextAlign")));
			this.btnCopyTable.Visible = ((bool)(resources.GetObject("btnCopyTable.Visible")));
			this.btnCopyTable.Click += new System.EventHandler(this.btnCopyTable_Click);
			// 
			// btnPasteTable
			// 
			this.btnPasteTable.AccessibleDescription = resources.GetString("btnPasteTable.AccessibleDescription");
			this.btnPasteTable.AccessibleName = resources.GetString("btnPasteTable.AccessibleName");
			this.btnPasteTable.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnPasteTable.Anchor")));
			this.btnPasteTable.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnPasteTable.BackgroundImage")));
			this.btnPasteTable.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnPasteTable.Dock")));
			this.btnPasteTable.Enabled = ((bool)(resources.GetObject("btnPasteTable.Enabled")));
			this.btnPasteTable.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnPasteTable.FlatStyle")));
			this.btnPasteTable.Font = ((System.Drawing.Font)(resources.GetObject("btnPasteTable.Font")));
			this.btnPasteTable.Image = ((System.Drawing.Image)(resources.GetObject("btnPasteTable.Image")));
			this.btnPasteTable.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnPasteTable.ImageAlign")));
			this.btnPasteTable.ImageIndex = ((int)(resources.GetObject("btnPasteTable.ImageIndex")));
			this.btnPasteTable.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnPasteTable.ImeMode")));
			this.btnPasteTable.Location = ((System.Drawing.Point)(resources.GetObject("btnPasteTable.Location")));
			this.btnPasteTable.Name = "btnPasteTable";
			this.btnPasteTable.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnPasteTable.RightToLeft")));
			this.btnPasteTable.Size = ((System.Drawing.Size)(resources.GetObject("btnPasteTable.Size")));
			this.btnPasteTable.TabIndex = ((int)(resources.GetObject("btnPasteTable.TabIndex")));
			this.btnPasteTable.Text = resources.GetString("btnPasteTable.Text");
			this.btnPasteTable.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnPasteTable.TextAlign")));
			this.btnPasteTable.Visible = ((bool)(resources.GetObject("btnPasteTable.Visible")));
			this.btnPasteTable.Click += new System.EventHandler(this.btnPasteTable_Click);
			// 
			// btnAddGroup
			// 
			this.btnAddGroup.AccessibleDescription = resources.GetString("btnAddGroup.AccessibleDescription");
			this.btnAddGroup.AccessibleName = resources.GetString("btnAddGroup.AccessibleName");
			this.btnAddGroup.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnAddGroup.Anchor")));
			this.btnAddGroup.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnAddGroup.BackgroundImage")));
			this.btnAddGroup.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnAddGroup.Dock")));
			this.btnAddGroup.Enabled = ((bool)(resources.GetObject("btnAddGroup.Enabled")));
			this.btnAddGroup.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnAddGroup.FlatStyle")));
			this.btnAddGroup.Font = ((System.Drawing.Font)(resources.GetObject("btnAddGroup.Font")));
			this.btnAddGroup.Image = ((System.Drawing.Image)(resources.GetObject("btnAddGroup.Image")));
			this.btnAddGroup.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnAddGroup.ImageAlign")));
			this.btnAddGroup.ImageIndex = ((int)(resources.GetObject("btnAddGroup.ImageIndex")));
			this.btnAddGroup.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnAddGroup.ImeMode")));
			this.btnAddGroup.Location = ((System.Drawing.Point)(resources.GetObject("btnAddGroup.Location")));
			this.btnAddGroup.Name = "btnAddGroup";
			this.btnAddGroup.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnAddGroup.RightToLeft")));
			this.btnAddGroup.Size = ((System.Drawing.Size)(resources.GetObject("btnAddGroup.Size")));
			this.btnAddGroup.TabIndex = ((int)(resources.GetObject("btnAddGroup.TabIndex")));
			this.btnAddGroup.Text = resources.GetString("btnAddGroup.Text");
			this.btnAddGroup.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnAddGroup.TextAlign")));
			this.btnAddGroup.Visible = ((bool)(resources.GetObject("btnAddGroup.Visible")));
			this.btnAddGroup.Click += new System.EventHandler(this.btnAddGroup_Click);
			// 
			// btnEditGroup
			// 
			this.btnEditGroup.AccessibleDescription = resources.GetString("btnEditGroup.AccessibleDescription");
			this.btnEditGroup.AccessibleName = resources.GetString("btnEditGroup.AccessibleName");
			this.btnEditGroup.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnEditGroup.Anchor")));
			this.btnEditGroup.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnEditGroup.BackgroundImage")));
			this.btnEditGroup.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnEditGroup.Dock")));
			this.btnEditGroup.Enabled = ((bool)(resources.GetObject("btnEditGroup.Enabled")));
			this.btnEditGroup.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnEditGroup.FlatStyle")));
			this.btnEditGroup.Font = ((System.Drawing.Font)(resources.GetObject("btnEditGroup.Font")));
			this.btnEditGroup.Image = ((System.Drawing.Image)(resources.GetObject("btnEditGroup.Image")));
			this.btnEditGroup.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnEditGroup.ImageAlign")));
			this.btnEditGroup.ImageIndex = ((int)(resources.GetObject("btnEditGroup.ImageIndex")));
			this.btnEditGroup.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnEditGroup.ImeMode")));
			this.btnEditGroup.Location = ((System.Drawing.Point)(resources.GetObject("btnEditGroup.Location")));
			this.btnEditGroup.Name = "btnEditGroup";
			this.btnEditGroup.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnEditGroup.RightToLeft")));
			this.btnEditGroup.Size = ((System.Drawing.Size)(resources.GetObject("btnEditGroup.Size")));
			this.btnEditGroup.TabIndex = ((int)(resources.GetObject("btnEditGroup.TabIndex")));
			this.btnEditGroup.Text = resources.GetString("btnEditGroup.Text");
			this.btnEditGroup.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnEditGroup.TextAlign")));
			this.btnEditGroup.Visible = ((bool)(resources.GetObject("btnEditGroup.Visible")));
			this.btnEditGroup.Click += new System.EventHandler(this.btnEditGroup_Click);
			// 
			// btnCopyGroup
			// 
			this.btnCopyGroup.AccessibleDescription = resources.GetString("btnCopyGroup.AccessibleDescription");
			this.btnCopyGroup.AccessibleName = resources.GetString("btnCopyGroup.AccessibleName");
			this.btnCopyGroup.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnCopyGroup.Anchor")));
			this.btnCopyGroup.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCopyGroup.BackgroundImage")));
			this.btnCopyGroup.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnCopyGroup.Dock")));
			this.btnCopyGroup.Enabled = ((bool)(resources.GetObject("btnCopyGroup.Enabled")));
			this.btnCopyGroup.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnCopyGroup.FlatStyle")));
			this.btnCopyGroup.Font = ((System.Drawing.Font)(resources.GetObject("btnCopyGroup.Font")));
			this.btnCopyGroup.Image = ((System.Drawing.Image)(resources.GetObject("btnCopyGroup.Image")));
			this.btnCopyGroup.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnCopyGroup.ImageAlign")));
			this.btnCopyGroup.ImageIndex = ((int)(resources.GetObject("btnCopyGroup.ImageIndex")));
			this.btnCopyGroup.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnCopyGroup.ImeMode")));
			this.btnCopyGroup.Location = ((System.Drawing.Point)(resources.GetObject("btnCopyGroup.Location")));
			this.btnCopyGroup.Name = "btnCopyGroup";
			this.btnCopyGroup.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnCopyGroup.RightToLeft")));
			this.btnCopyGroup.Size = ((System.Drawing.Size)(resources.GetObject("btnCopyGroup.Size")));
			this.btnCopyGroup.TabIndex = ((int)(resources.GetObject("btnCopyGroup.TabIndex")));
			this.btnCopyGroup.Text = resources.GetString("btnCopyGroup.Text");
			this.btnCopyGroup.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnCopyGroup.TextAlign")));
			this.btnCopyGroup.Visible = ((bool)(resources.GetObject("btnCopyGroup.Visible")));
			this.btnCopyGroup.Click += new System.EventHandler(this.btnCopyGroup_Click);
			// 
			// btnAddTable
			// 
			this.btnAddTable.AccessibleDescription = resources.GetString("btnAddTable.AccessibleDescription");
			this.btnAddTable.AccessibleName = resources.GetString("btnAddTable.AccessibleName");
			this.btnAddTable.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnAddTable.Anchor")));
			this.btnAddTable.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnAddTable.BackgroundImage")));
			this.btnAddTable.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnAddTable.Dock")));
			this.btnAddTable.Enabled = ((bool)(resources.GetObject("btnAddTable.Enabled")));
			this.btnAddTable.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnAddTable.FlatStyle")));
			this.btnAddTable.Font = ((System.Drawing.Font)(resources.GetObject("btnAddTable.Font")));
			this.btnAddTable.Image = ((System.Drawing.Image)(resources.GetObject("btnAddTable.Image")));
			this.btnAddTable.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnAddTable.ImageAlign")));
			this.btnAddTable.ImageIndex = ((int)(resources.GetObject("btnAddTable.ImageIndex")));
			this.btnAddTable.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnAddTable.ImeMode")));
			this.btnAddTable.Location = ((System.Drawing.Point)(resources.GetObject("btnAddTable.Location")));
			this.btnAddTable.Name = "btnAddTable";
			this.btnAddTable.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnAddTable.RightToLeft")));
			this.btnAddTable.Size = ((System.Drawing.Size)(resources.GetObject("btnAddTable.Size")));
			this.btnAddTable.TabIndex = ((int)(resources.GetObject("btnAddTable.TabIndex")));
			this.btnAddTable.Text = resources.GetString("btnAddTable.Text");
			this.btnAddTable.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnAddTable.TextAlign")));
			this.btnAddTable.Visible = ((bool)(resources.GetObject("btnAddTable.Visible")));
			this.btnAddTable.Click += new System.EventHandler(this.btnAddTable_Click);
			// 
			// btnViewTable
			// 
			this.btnViewTable.AccessibleDescription = resources.GetString("btnViewTable.AccessibleDescription");
			this.btnViewTable.AccessibleName = resources.GetString("btnViewTable.AccessibleName");
			this.btnViewTable.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnViewTable.Anchor")));
			this.btnViewTable.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnViewTable.BackgroundImage")));
			this.btnViewTable.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnViewTable.Dock")));
			this.btnViewTable.Enabled = ((bool)(resources.GetObject("btnViewTable.Enabled")));
			this.btnViewTable.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnViewTable.FlatStyle")));
			this.btnViewTable.Font = ((System.Drawing.Font)(resources.GetObject("btnViewTable.Font")));
			this.btnViewTable.Image = ((System.Drawing.Image)(resources.GetObject("btnViewTable.Image")));
			this.btnViewTable.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnViewTable.ImageAlign")));
			this.btnViewTable.ImageIndex = ((int)(resources.GetObject("btnViewTable.ImageIndex")));
			this.btnViewTable.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnViewTable.ImeMode")));
			this.btnViewTable.Location = ((System.Drawing.Point)(resources.GetObject("btnViewTable.Location")));
			this.btnViewTable.Name = "btnViewTable";
			this.btnViewTable.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnViewTable.RightToLeft")));
			this.btnViewTable.Size = ((System.Drawing.Size)(resources.GetObject("btnViewTable.Size")));
			this.btnViewTable.TabIndex = ((int)(resources.GetObject("btnViewTable.TabIndex")));
			this.btnViewTable.Text = resources.GetString("btnViewTable.Text");
			this.btnViewTable.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnViewTable.TextAlign")));
			this.btnViewTable.Visible = ((bool)(resources.GetObject("btnViewTable.Visible")));
			this.btnViewTable.Click += new System.EventHandler(this.btnViewTable_Click);
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
			this.btnHelp.DialogResult = System.Windows.Forms.DialogResult.Cancel;
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
			// TableManagement
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
			this.Controls.Add(this.btnClose);
			this.Controls.Add(this.btnDeleteTable);
			this.Controls.Add(this.btnDeleteGroup);
			this.Controls.Add(this.btnMoveDown);
			this.Controls.Add(this.btnMoveUp);
			this.Controls.Add(this.btnEditTable);
			this.Controls.Add(this.btnCopyTable);
			this.Controls.Add(this.btnPasteTable);
			this.Controls.Add(this.btnAddGroup);
			this.Controls.Add(this.btnEditGroup);
			this.Controls.Add(this.btnCopyGroup);
			this.Controls.Add(this.btnAddTable);
			this.Controls.Add(this.btnViewTable);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.tvwTables);
			this.Controls.Add(this.btnHelp);
			this.Enabled = ((bool)(resources.GetObject("$this.Enabled")));
			this.Font = ((System.Drawing.Font)(resources.GetObject("$this.Font")));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("$this.ImeMode")));
			this.Location = ((System.Drawing.Point)(resources.GetObject("$this.Location")));
			this.MaximumSize = ((System.Drawing.Size)(resources.GetObject("$this.MaximumSize")));
			this.MinimumSize = ((System.Drawing.Size)(resources.GetObject("$this.MinimumSize")));
			this.Name = "TableManagement";
			this.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("$this.RightToLeft")));
			this.StartPosition = ((System.Windows.Forms.FormStartPosition)(resources.GetObject("$this.StartPosition")));
			this.Text = resources.GetString("$this.Text");
			this.Load += new System.EventHandler(this.TableManagement_Load);
			this.ResumeLayout(false);

		}

		#endregion

		//**************************************************************************              
		///    <Description>
		///       This method is used to load data to the tree view
		///       For each node on the tree view, we store its appropriate object
		///       for the Group - store Group object VO
		///       for the Table - store the table object VO
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
		///       NgocHT
		///    </Authors>
		///    <History>
		///       28-Dec-2004
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************

		private void TableManagement_Load(object sender, System.EventArgs e)
		{
			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.WaitCursor;

			#endregion Code Inserted Automatically

			
			const string METHOD_NAME = THIS + ".TableManagement_Load()";
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
					// Code Inserted Automatically
#region Code Inserted Automatically
this.Cursor = Cursors.Default;
#endregion Code Inserted Automatically

					return;
				}

				ArrayList arrNode = new ArrayList();
				uTreeView objuTreeView = new uTreeView();
				arrNode = objuTreeView.CreateNodes();
				tvwTables.Nodes.Clear();
				objFCC = new FormControlComponents(tvwTables, arrNode);
				objFCC.FillDataTreeView(null, 0);
				tvwTables.ExpandAll();
				if (tvwTables.Nodes.Count == 0)
				{
					TableClick(false);
					GroupClick(true);
					btnAddTable.Enabled = false;
					btnEditGroup.Enabled = false;
					btnCopyGroup.Enabled = false;
					btnDeleteGroup.Enabled = false;
					btnMoveUp.Enabled = false;
					btnMoveDown.Enabled = false;
				}
				tvwTables.SelectedNode = tvwTables.Nodes[0];
				tvwTables.Select();
				// collapse all node
				tvwTables.CollapseAll();
				// add images
				foreach (TreeNode objNode in tvwTables.Nodes)
				{
					// group node
					if (objNode.Tag is sys_TableGroupVO)
					{
						if (objNode.IsExpanded)
						{
							objNode.ImageIndex = EXPAND_GROUP_IMG;
							objNode.SelectedImageIndex = EXPAND_GROUP_IMG;
						} 
						else
						{
							objNode.ImageIndex = COLLAPSE_GROUP_IMG;
							objNode.SelectedImageIndex = COLLAPSE_GROUP_IMG;
						}
						// Add image for table
						foreach(TreeNode objNodeTable in objNode.Nodes)
						{
							if (objNodeTable.Tag is sys_TableVO) // table node
							{
								sys_TableVO voTable = (sys_TableVO) objNodeTable.Tag;
								if(voTable.IsViewOnly)
								{
									objNodeTable.ImageIndex = TABLE_IMG_VIEW;
									objNodeTable.SelectedImageIndex = TABLE_IMG_VIEW;
								}
								else
								{
									objNodeTable.ImageIndex = TABLE_IMG_EDIT;
									objNodeTable.SelectedImageIndex = TABLE_IMG_EDIT;
								}
							}
						}
					}
					//					else if (objNode.Tag is sys_TableVO) // table node
					//					{
					////						objNode.ImageIndex = TABLE_IMG;
					////						objNode.SelectedImageIndex = TABLE_IMG;
					//						sys_TableVO voTable = (sys_TableVO) objNode.Tag;
					//						if(voTable.IsViewOnly)
					//						{
					//							objNode.ImageIndex = TABLE_IMG4;
					//							objNode.SelectedImageIndex = TABLE_IMG4;
					//						}
					//						else
					//						{
					//							objNode.ImageIndex = TABLE_IMG3;
					//							objNode.SelectedImageIndex = TABLE_IMG3;
					//						}
					//					}
				}
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

			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.Default;

			#endregion Code Inserted Automatically

			
		}


		
		//**************************************************************************              
		///    <Description>
		///       This method uses to Enable or Diable the button 
		///       
		///    </Description>
		///    <Inputs>
		///        Tree View
		///    </Inputs>
		///    <Outputs>
		///      
		///    </Outputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <Authors>
		///       NgocHT
		///    </Authors>
		///    <History>
		///       28-Dec-2004
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************

		private void tvwTables_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e)
		{
			const string METHOD_NAME = THIS + ".tvwTables_AfterSelect()";
			try
			{
				object obj = tvwTables.SelectedNode.Tag;

				if (blnCopy)
				{
					//voTableGroup = (sys_TableGroupVO)obj;
					//intGroupID = voTableGroup.TableGroupID;
					CopyClick(true);
					//btnPasteTable.Focus();
					return;
				}
				if (obj is sys_TableVO)
				{
					if (e !=null)
					{
						if(((sys_TableVO)obj).IsViewOnly)
						{
							e.Node.ImageIndex = 
							e.Node.SelectedImageIndex = TABLE_IMG_VIEW;
						}
						else
						{
							e.Node.ImageIndex = 
							e.Node.SelectedImageIndex = TABLE_IMG_EDIT;
						}
					}
					voSysTable = (sys_TableVO) obj;
					intTableID = voSysTable.TableID;
					strTableName = voSysTable.TableOrView;
					GroupClick(false);
					TableClick(true);

					//get the TableGroupId and TableOrder in this group
					//and Enable or disable the up and down button
					object objGroup = tvwTables.SelectedNode.Parent.Tag;
					if (objGroup is sys_TableGroupVO)
					{
						//sys_TableGroupVO objsys_TableGroupVO = (sys_TableGroupVO) objGroup;
						//get the order for this table in this group
						//TableManagementBO objTableManagementBO = new TableManagementBO();
						voTableGroup = (sys_TableGroupVO) objGroup;
						intGroupID = voTableGroup.TableGroupID;
					}
				}
				else if (obj is sys_TableGroupVO)
				{
					if (e != null)
					{
						if (e.Node.IsExpanded)
							e.Node.SelectedImageIndex = EXPAND_GROUP_IMG;
						else
							e.Node.SelectedImageIndex = COLLAPSE_GROUP_IMG;
					}
					voTableGroup = (sys_TableGroupVO) obj;
					intGroupID = voTableGroup.TableGroupID;
					TableClick(false);
					GroupClick(true);
				}
				//Enable Up and Down button
				SetMoveUpMoveDownButton(tvwTables.SelectedNode);
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

		//**************************************************************************              
		///    <Description>
		///       This method uses to Enable or Diable the up and down button
		///       
		///    </Description>
		///    <Inputs>
		///        Tree View
		///    </Inputs>
		///    <Outputs>
		///      
		///    </Outputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <Authors>
		///       NgocHT
		///    </Authors>
		///    <History>
		///       28-Dec-2004
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private void SetMoveUpMoveDownButton(TreeNode treeSelectedNode)
		{
			btnMoveUp.Enabled = true;
			btnMoveDown.Enabled = true;
			if (treeSelectedNode.NextNode == null)
			{
				btnMoveDown.Enabled = false;
			}
			if (treeSelectedNode.PrevNode == null)
			{
				btnMoveUp.Enabled = false;
			}

			/*
			if (treeSelectedNode.NextNode !=null &&  treeSelectedNode.PrevNode !=null) 
			{
				btnMoveUp.Enabled = true;
				btnMoveDown.Enabled = true;
			}
			*/
		}
		//**************************************************************************              
		///    <Description>
		///       This method is used to open a new form to add a new group
		///       
		///    </Description>
		///    <Inputs>
		///        Tree View
		///    </Inputs>
		///    <Outputs>
		///      
		///    </Outputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <Authors>
		///       NgocHT
		///    </Authors>
		///    <History>
		///       28-Dec-2004
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************

		private void btnAddGroup_Click(object sender, System.EventArgs e)
		{
			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.WaitCursor;

			#endregion Code Inserted Automatically

			
			const string METHOD_NAME = THIS + ".btnAddGroup_Click()";
			try
			{
				TableGroup frm = new TableGroup();
				frm.EnumType = EnumAction.Add;
				frm.TableGroupID = -1;
				frm.ShowDialog();
				if (frm.TableGroupID != -1)
				{
					// add new node to tree
					TreeNode objNode = new TreeNode();
					objNode.Text = frm.TableGroupVO.TableGroupName;
					objNode.Tag = frm.TableGroupVO;
					objNode.ImageIndex = EXPAND_GROUP_IMG;
					tvwTables.Nodes.Add(objNode);
					tvwTables.SelectedNode = objNode;
					//TableManagement_Load(this, e);
					//go to the newly inserted group
					//GotoTreeNode(TYPE_GROUP,frm.TableGroupID,-1);
					frm.Close();
					tvwTables.Select();
				}

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

			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.Default;

			#endregion Code Inserted Automatically

			
		}

		//---------------
		private void btnEditGroup_Click(object sender, System.EventArgs e)
		{
			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.WaitCursor;

			#endregion Code Inserted Automatically

			
			const string METHOD_NAME = THIS + ".btnEditGroup_Click()";

			try
			{
				object obj = tvwTables.SelectedNode.Tag;
				if (obj is sys_TableGroupVO)
				{
					TableGroup frm = new TableGroup();
					frm.EnumType = EnumAction.Edit;
					frm.TableGroupVO = (sys_TableGroupVO) obj;
					frm.ShowDialog();
					// selected node
					tvwTables.SelectedNode.Text = frm.TableGroupVO.TableGroupName;
					tvwTables.SelectedNode.Tag = frm.TableGroupVO;
					//TableManagement_Load(this, e);
					//GotoTreeNode(TYPE_GROUP,frm.TableGroupID,-1);
					frm.Close();
					tvwTables.Select();
				}

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
			catch (NullReferenceException ex)
			{
				PCSMessageBox.Show(ErrorCode.MESSAGE_TABLEMANAGEMENT_FOCUS,MessageBoxIcon.Error);
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

			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.Default;

			#endregion Code Inserted Automatically

			
		}

		//**************************************************************************              
		///    <Description>
		///       This method is used to copy one group to another
		///       It will copy all of its table to a new group
		///       name and code of the new group will have the "copy" string in it
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
		///       NgocHT
		///    </Authors>
		///    <History>
		///       28-Dec-2004
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************

		private void btnCopyGroup_Click(object sender, System.EventArgs e)
		{
			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.WaitCursor;

			#endregion Code Inserted Automatically

			
			const string METHOD_NAME = THIS + ".btnCopyGroup_Click()";

			try
			{
				object obj = tvwTables.SelectedNode.Tag;
				if (obj is sys_TableGroupVO)
				{
					TreeNode objCurrentGroupNode = tvwTables.SelectedNode;

					sys_TableGroupVO voTableGroup = (sys_TableGroupVO) obj;
					intCopyGroupID = voTableGroup.TableGroupID;

					TableManagementBO boTableManagement = new TableManagementBO();
					int intNewCopiedGroupID = boTableManagement.CopyGroupAndReturnMaxID(intCopyGroupID);
					PCSMessageBox.Show(ErrorCode.MESSAGE_TABLEMANAGEMENT_AFTER_COPYGROUP);

					//Add a new group to the tree
					//First we need to add a new group
					sys_TableGroupVO vosys_TableGroupVO = (sys_TableGroupVO)boTableManagement.GetGroupInfo(intNewCopiedGroupID);
					TreeNode objNewGroupNode = new TreeNode(vosys_TableGroupVO.TableGroupName);
					objNewGroupNode.Tag = vosys_TableGroupVO;
					//Loop to all previous node and add to new node
					foreach (TreeNode objNode in objCurrentGroupNode.Nodes)
					{
						sys_TableVO vosys_TableVO = (sys_TableVO)objNode.Tag;
						TreeNode objNewTableNode = new TreeNode(vosys_TableVO.TableName);
						objNewTableNode.Tag = vosys_TableVO;
						objNewGroupNode.Nodes.Add(objNewTableNode);
					}

					int intNewGroupIndex = tvwTables.Nodes.Add(objNewGroupNode);

					//TableManagement_Load(this, e);
					//GotoTreeNode(TYPE_GROUP,intNewCopiedGroupID,-1);
					tvwTables.Select();

				}
				else
				{
					PCSMessageBox.Show(ErrorCode. MESSAGE_TABLEMANAGEMENT_SELECT_GROUP_TOCOPY,MessageBoxIcon.Warning);
				}

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
			catch (NullReferenceException ex)
			{
				PCSMessageBox.Show(ErrorCode.MESSAGE_TABLEMANAGEMENT_FOCUS,MessageBoxIcon.Error);
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

			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.Default;

			#endregion Code Inserted Automatically

			
		}

		//**************************************************************************              
		///    <Description>
		///       This method uses to delete a group
		///       
		///    </Description>
		///    <Inputs>
		///        Tree View
		///    </Inputs>
		///    <Outputs>
		///      
		///    </Outputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <Authors>
		///       NgocHT
		///    </Authors>
		///    <History>
		///       28-Dec-2004
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************

		private void btnDeleteGroup_Click(object sender, System.EventArgs e)
		{
			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.WaitCursor;

			#endregion Code Inserted Automatically

			
			const string METHOD_NAME = THIS + ".btnDeleteGroup_Click()";

			try
			{
				if (tvwTables.SelectedNode.Tag == null)
				{
					PCSMessageBox.Show(ErrorCode.MESSAGE_TABLEMANAGEMENT_FOCUS);
					// Code Inserted Automatically
#region Code Inserted Automatically
this.Cursor = Cursors.Default;
#endregion Code Inserted Automatically

					return;
				}

				object obj = tvwTables.SelectedNode.Tag;
				if (obj is sys_TableGroupVO)
				{
					DialogResult result = PCSMessageBox.Show(ErrorCode.MESSAGE_DELETE_GROUP, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
					if (result == DialogResult.Yes)
					{
						sys_TableGroupVO voTableGroup = (sys_TableGroupVO) obj;
						intGroupID = voTableGroup.TableGroupID;

						TableManagementBO boTableManagement = new TableManagementBO();
						boTableManagement.DeleteGroup(intGroupID);
						tvwTables.Nodes.Remove(tvwTables.SelectedNode);
						//TableManagement_Load(this, e);
						tvwTables.Select();
					}
				}

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
			catch (NullReferenceException ex)
			{
				PCSMessageBox.Show(ErrorCode.MESSAGE_TABLEMANAGEMENT_FOCUS,MessageBoxIcon.Error);
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

			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.Default;

			#endregion Code Inserted Automatically

			
		}

		//**************************************************************************              
		///    <Description>
		///       This method uses to open a new form ViewTable to view the table content
		///       
		///    </Description>
		///    <Inputs>
		///        Tree View
		///    </Inputs>
		///    <Outputs>
		///      
		///    </Outputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <Authors>
		///       NgocHT
		///    </Authors>
		///    <History>
		///       28-Dec-2004
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************

		private void btnViewTable_Click(object sender, System.EventArgs e)
		{
			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.WaitCursor;

			#endregion Code Inserted Automatically

			
			const string METHOD_NAME = THIS + ".btnViewTable_Click()";
			try
			{
				if (tvwTables.SelectedNode.Tag !=null && (tvwTables.SelectedNode.Tag) is sys_TableVO) 
				{
					ViewTable objTableVOForm = new ViewTable(intTableID.ToString(), strTableName.Trim());
					//ViewTable objTableVOForm = new ViewTable("3","ITM_FreightClass");
					//objTableVOForm.ViewOnly = false;
					objTableVOForm.ViewOnly = ((sys_TableVO)tvwTables.SelectedNode.Tag).IsViewOnly;
					objTableVOForm.GetData = false;

					string strConditionByRecord = (new UtilsBO()).GetConditionByRecord(SystemProperty.UserName,strTableName);
					
					objTableVOForm.WhereClause = " WHERE 1=1 " + strConditionByRecord;
					FormInfo infoForm = new FormInfo(objTableVOForm,string.Empty,string.Empty,string.Empty,string.Empty,SystemProperty.UserName);
					SystemProperty.ArrayForms.Add(infoForm);
					objTableVOForm.Show();
					//go to the newly inserted Table 
					//GotoTreeNode(TYPE_TABLE, intTableID, intGroupID);
					tvwTables.Select();
				}
			}
			catch (Exception ex)
			{
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

			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.Default;

			#endregion Code Inserted Automatically

			
		}

		//**************************************************************************              
		///    <Description>
		///       This method uses to open a new form to add a new Table to a group
		///       
		///    </Description>
		///    <Inputs>
		///        Tree View
		///    </Inputs>
		///    <Outputs>
		///      
		///    </Outputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <Authors>
		///       NgocHT
		///    </Authors>
		///    <History>
		///       28-Dec-2004
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		
		private void btnAddTable_Click(object sender, System.EventArgs e)
		{
			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.WaitCursor;

			#endregion Code Inserted Automatically

			
			const string METHOD_NAME = THIS + ".btnAddTable_Click()";
			try
			{
				object obj = tvwTables.SelectedNode.Tag;
				TreeNode objNode = tvwTables.SelectedNode;

				if (obj is sys_TableGroupVO)
				{
					sys_TableGroupVO voTableGroup = (sys_TableGroupVO) obj;
					intGroupID = voTableGroup.TableGroupID;
					
					TableConfig frm = new TableConfig();
					frm.GroupID = intGroupID;
					frm.TableID = -1;
					frm.EnumType = EnumAction.Add;
					frm.ShowDialog();

					//refill TreeView
					if (frm.TableID != -1) 
					{
						TreeNode objNewNode = new TreeNode();
						objNewNode.Text = frm.SysTableVO.TableName;
						objNewNode.Tag = frm.SysTableVO;
						if (frm.SysTableVO.IsViewOnly)
						{
							objNewNode.ImageIndex = TABLE_IMG_VIEW;
							objNewNode.SelectedImageIndex = TABLE_IMG_VIEW;
						}
						else
						{
							objNewNode.ImageIndex = TABLE_IMG_EDIT;
							objNewNode.SelectedImageIndex = TABLE_IMG_EDIT;
						}
						objNode.Nodes.Add(objNewNode);
						tvwTables.SelectedNode = objNewNode;
						//TableManagement_Load(this, e);
						//go to the newly inserted Table 
						//GotoTreeNode(TYPE_TABLE,frm.TableID,intStoreGroupID);
						frm.Close();
					}
					tvwTables.Select();

				}
				else
				{
					if (obj is sys_TableVO)
					{
						obj = tvwTables.SelectedNode.Parent.Tag;
						if (obj is sys_TableGroupVO)
						{
							sys_TableGroupVO voTableGroup = (sys_TableGroupVO) obj;
							intGroupID = voTableGroup.TableGroupID;
							TableConfig frm = new TableConfig();
							frm.GroupID = intGroupID;
							frm.EnumType = EnumAction.Add;
							frm.ShowDialog();

							//refill TreeView
							TableManagement_Load(this, e);
							tvwTables.Select();
						}
					}
				}

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

			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.Default;

			#endregion Code Inserted Automatically

			
		}

		//**************************************************************************              
		///    <Description>
		///       This method uses to open a new form to edit a table's information
		///       
		///    </Description>
		///    <Inputs>
		///        Tree View
		///    </Inputs>
		///    <Outputs>
		///      
		///    </Outputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <Authors>
		///       NgocHT
		///    </Authors>
		///    <History>
		///       28-Dec-2004
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		
		private void btnEditTable_Click(object sender, System.EventArgs e)
		{
			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.WaitCursor;

			#endregion Code Inserted Automatically

			
			const string METHOD_NAME = THIS + ".btnEditTable_Click()";


			try
			{
				object obj = tvwTables.SelectedNode.Tag;
				if (obj is sys_TableVO)
				{
					
					int intStoreTableID = ((sys_TableVO) obj).TableID;
					TableConfig frm = new TableConfig();
					frm.EnumType = EnumAction.Edit;
					frm.SysTableVO = (sys_TableVO) obj;
					frm.GroupID = intGroupID;
					int intStoreGroupID = intGroupID;
//					frm.TableID = intTableID;
					frm.ShowDialog();
					//refill data on tree
					TreeNode objNewNode = tvwTables.SelectedNode;
					objNewNode.Text = frm.SysTableVO.TableName;
					objNewNode.Tag = frm.SysTableVO;
					if (frm.SysTableVO.IsViewOnly)
					{
						objNewNode.ImageIndex = TABLE_IMG_VIEW;
						objNewNode.SelectedImageIndex = TABLE_IMG_VIEW;
					}
					else
					{
						objNewNode.ImageIndex = TABLE_IMG_EDIT;
						objNewNode.SelectedImageIndex = TABLE_IMG_EDIT;
					}
					//TableManagement_Load(this, e);
					//GotoTreeNode(TYPE_TABLE,intStoreTableID, intStoreGroupID);
					tvwTables.Select();

				}

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
			catch (NullReferenceException ex)
			{
				PCSMessageBox.Show(ErrorCode.MESSAGE_TABLEMANAGEMENT_FOCUS,MessageBoxIcon.Error);
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

			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.Default;

			#endregion Code Inserted Automatically

			
		}

		//**************************************************************************              
		///    <Description>
		///       This method uses to copy a Table to another group
		///       
		///    </Description>
		///    <Inputs>
		///        Tree View
		///    </Inputs>
		///    <Outputs>
		///      
		///    </Outputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <Authors>
		///       NgocHT
		///    </Authors>
		///    <History>
		///       28-Dec-2004
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		
		private void btnCopyTable_Click(object sender, System.EventArgs e)
		{
			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.WaitCursor;

			#endregion Code Inserted Automatically

			
			const string METHOD_NAME = THIS + ".btnCopyTable_Click()";
			try
			{
				object obj = tvwTables.SelectedNode.Tag;
				if (obj is sys_TableVO)
				{
					voCopySysTable = (sys_TableVO) obj;
					//PCSMessageBox.Show(ErrorCode.MESSAGE_TABLEMANAGEMENT_SELECT_TABLE_TOPASTE);
					blnCopy = true;
					CopyClick(true);
				}else
				{
					PCSMessageBox.Show(ErrorCode.MESSAGE_TABLEMANAGEMENT_SELECT_TABLE_TOCOPY,MessageBoxIcon.Warning);
				}
			}
			catch (PCSException ex)
			{
				// displays the error message.
				//PCSMessageBox.Show(ex.mCode,MessageBoxIcon.Error);
				// log message.

				// displays the error message.
				if (ex.mCode != ErrorCode.DUPLICATE_KEY && ex.mCode != ErrorCode.DUPLICATEKEY)
				{
					PCSMessageBox.Show(ex.mCode,MessageBoxIcon.Error);
				}
				else
				{
					PCSMessageBox.Show(ErrorCode.DUPLICATE_TABLECOPY, MessageBoxIcon.Error);
				}


				try
				{
					Logger.LogMessage(ex.CauseException, METHOD_NAME, Level.ERROR);
				}

				catch
				{
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION,MessageBoxIcon.Error);
				}
			}			
			catch (NullReferenceException ex)
			{
				PCSMessageBox.Show(ErrorCode.MESSAGE_TABLEMANAGEMENT_FOCUS,MessageBoxIcon.Error);
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

			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.Default;

			#endregion Code Inserted Automatically

			
		}

		//**************************************************************************              
		///    <Description>
		///       This method uses paste a copied table into another group
		///       
		///    </Description>
		///    <Inputs>
		///        Tree View
		///    </Inputs>
		///    <Outputs>
		///      
		///    </Outputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <Authors>
		///       NgocHT
		///    </Authors>
		///    <History>
		///       28-Dec-2004
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private void btnPasteTable_Click(object sender, System.EventArgs e)
		{
			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.WaitCursor;

			#endregion Code Inserted Automatically

			
			const string METHOD_NAME = THIS + ".btnPasteTable_Click()";
			try
			{
				object obj = tvwTables.SelectedNode.Tag;
				if (obj is sys_TableGroupVO)
				{

					//voCopySysTable
					sys_TableGroupVO voTableGroup = (sys_TableGroupVO) obj;
					//intCopyGroupID = voTableGroup.TableGroupID;

					//store TableId and GroupID
					int intStoreTableID = voCopySysTable.TableID;
					int intStoreGroupID = voTableGroup.TableGroupID;

					TableManagementBO boTableManagement = new TableManagementBO();
					boTableManagement.CopyTable(voCopySysTable.TableID,voTableGroup.TableGroupID);

					//insert a new node to 
					TreeNode objCurrentGroupNode = tvwTables.SelectedNode;
					TreeNode objNewNode = new TreeNode(voCopySysTable.TableName);
					objNewNode.Tag = voCopySysTable;
					if(voCopySysTable.IsViewOnly)
					{
						objNewNode.ImageIndex = 
						objNewNode.SelectedImageIndex = TABLE_IMG_VIEW;
					}
					else
					{
						objNewNode.ImageIndex = 
							objNewNode.SelectedImageIndex = TABLE_IMG_EDIT;
					}
					objCurrentGroupNode.Nodes.Add(objNewNode);

					//After copy -- Reload database
					blnCopy = false;
					PCSMessageBox.Show(ErrorCode. MESSAGE_TABLEMANAGEMENT_AFTER_PASTETABLE);
					//TableManagement_Load(this, e);
					//GotoTreeNode(TYPE_TABLE,intStoreTableID,intStoreGroupID);
					tvwTables.Select();
				}
				else
				{
					PCSMessageBox.Show(ErrorCode.MESSAGE_TABLEMANAGEMENT_SELECT_TABLE_TOPASTE,MessageBoxIcon.Warning);
				}

			}
			catch (PCSException ex)
			{
				// displays the error message.
				if (ex.mCode == ErrorCode.MESSAGE_TABLEMANAGEMENT_DUPLICATE_TABLE)
				{
					if (PCSMessageBox.Show(ErrorCode.MESSAGE_TABLEMANAGEMENT_DUPLICATE_TABLE_ASK_YESNO,MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes)
					{
						blnCopy = false;
						//CopyClick(false);
						//tvwTables.Select();
						tvwTables_AfterSelect(tvwTables,null);
						tvwTables.Select();
					}
				}
				else 
				{
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

			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.Default;

			#endregion Code Inserted Automatically

			
		}

		//**************************************************************************              
		///    <Description>
		///       This method uses to delete a table from a group
		///       
		///    </Description>
		///    <Inputs>
		///        Tree View
		///    </Inputs>
		///    <Outputs>
		///      
		///    </Outputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <Authors>
		///       NgocHT
		///    </Authors>
		///    <History>
		///       28-Dec-2004
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************

		private void btnDeleteTable_Click(object sender, System.EventArgs e)
		{
			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.WaitCursor;

			#endregion Code Inserted Automatically

			
			const string METHOD_NAME = THIS + ".btnDeleteTable_Click()";


			try
			{
				object obj = tvwTables.SelectedNode.Tag;
				if (obj is sys_TableVO)
				{
					sys_TableGroupVO objsys_TableGroupVO = (sys_TableGroupVO)tvwTables.SelectedNode.Parent.Tag;
					DialogResult result = PCSMessageBox.Show(ErrorCode.MESSAGE_DELETE_RECORD, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
					if (result.Equals(DialogResult.Yes))
					{
						TableManagementBO boTableManagement = new TableManagementBO();
						boTableManagement.DeleteTable(intTableID,objsys_TableGroupVO.TableGroupID);
						tvwTables.Nodes.Remove(tvwTables.SelectedNode);
						//refill data on TreeView
						//TableManagement_Load(this, e);
						//go to the group node
						//GotoTreeNode(TYPE_GROUP,objsys_TableGroupVO.TableGroupID,-1);
						tvwTables.Select();
					}
				}

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
			catch (NullReferenceException ex)
			{
				PCSMessageBox.Show(ErrorCode.MESSAGE_TABLEMANAGEMENT_FOCUS,MessageBoxIcon.Error);
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

			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.Default;

			#endregion Code Inserted Automatically

			
		}

		//**************************************************************************              
		///    <Description>
		///       This method uses to change the order of a table
		///       
		///    </Description>
		///    <Inputs>
		///        Tree View
		///    </Inputs>
		///    <Outputs>
		///      
		///    </Outputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <Authors>
		///       NgocHT
		///    </Authors>
		///    <History>
		///       28-Dec-2004
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************

		private void btnMoveUp_Click(object sender, System.EventArgs e)
		{
			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.WaitCursor;

			#endregion Code Inserted Automatically

			
			const string METHOD_NAME = THIS + ".btnMoveUp_Click()";
			try
			{
				TreeNode objSelectedNode = tvwTables.SelectedNode;
				if (tvwTables.SelectedNode.Tag == null)
				{
					PCSMessageBox.Show(ErrorCode.MESSAGE_TABLEMANAGEMENT_FOCUS);
					// Code Inserted Automatically
#region Code Inserted Automatically
this.Cursor = Cursors.Default;
#endregion Code Inserted Automatically

					return;
				}

				object obj = tvwTables.SelectedNode.Tag;

				if (obj is sys_TableVO)
				{
					sys_TableVO voSysTable = (sys_TableVO) obj;
					intTableID = voSysTable.TableID;

					sys_TableGroupVO objParentGroup =(sys_TableGroupVO) tvwTables.SelectedNode.Parent.Tag; 
					TableManagementBO boTableManagement = new TableManagementBO();
					//int intOrder = boTableManagement.MoveUpTable(intTableID);
					boTableManagement.MoveUpTable(intTableID,objParentGroup.TableGroupID);
					TreeNode objGroupNode = objSelectedNode.Parent;
					TreeNode objPreviousNode = (objSelectedNode.PrevVisibleNode == null) ? objSelectedNode.PrevNode : objSelectedNode.PrevVisibleNode;
					if (objPreviousNode != null)
					{
						// remove previous node
						objPreviousNode.Remove();
						// make a copy of previous node
						//TreeNode objNewNode = objPreviousNode;
						// move the previous node to under selected node
						objGroupNode.Nodes.Insert(objSelectedNode.Index + 1, objPreviousNode);
					}
					tvwTables.Focus();
					tvwTables.Select();
					tvwTables.SelectedNode = objSelectedNode;
				}
				else if (obj is sys_TableGroupVO)
				{
					sys_TableGroupVO voTableGroup = (sys_TableGroupVO) obj;
					intGroupID = voTableGroup.TableGroupID;

					TableManagementBO boTableManagement = new TableManagementBO();
					boTableManagement.MoveUpGroup(intGroupID);
					tvwTables.Nodes.Remove(objSelectedNode);
					tvwTables.Nodes.Insert(objSelectedNode.Index - 1, objSelectedNode);
					tvwTables.Focus();
					tvwTables.Select();
					tvwTables.SelectedNode = objSelectedNode;
				}
				SetMoveUpMoveDownButton(tvwTables.SelectedNode);
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

			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.Default;

			#endregion Code Inserted Automatically

			
		}

		//**************************************************************************              
		///    <Description>
		///       This method uses to change the order of a table or a group
		///       
		///    </Description>
		///    <Inputs>
		///        Tree View
		///    </Inputs>
		///    <Outputs>
		///      
		///    </Outputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <Authors>
		///       NgocHT
		///    </Authors>
		///    <History>
		///       28-Dec-2004
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		
		private void btnMoveDown_Click(object sender, System.EventArgs e)
		{
			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.WaitCursor;

			#endregion Code Inserted Automatically

			
			const string METHOD_NAME = THIS + ".btnMoveDown_Click()";

			try
			{
				TreeNode objSelectedNode = tvwTables.SelectedNode;
				if (tvwTables.SelectedNode.Tag == null)
				{
					PCSMessageBox.Show(ErrorCode.MESSAGE_TABLEMANAGEMENT_FOCUS);
					// Code Inserted Automatically
#region Code Inserted Automatically
this.Cursor = Cursors.Default;
#endregion Code Inserted Automatically

					return;
				}

				object obj = tvwTables.SelectedNode.Tag;

				if (obj is sys_TableVO)
				{
					sys_TableVO voSysTable = (sys_TableVO) obj;
					intTableID = voSysTable.TableID;

					//get the parent for this table 
					sys_TableGroupVO objParentGroup =(sys_TableGroupVO) tvwTables.SelectedNode.Parent.Tag; 

					TableManagementBO boTableManagement = new TableManagementBO();
					//int intOrder = boTableManagement.MoveDownTable(intTableID);
					boTableManagement.MoveDownTable(intTableID,objParentGroup.TableGroupID);
					TreeNode objGroupNode = objSelectedNode.Parent;
					TreeNode objNextNode = (objSelectedNode.NextVisibleNode == null) ? objSelectedNode.NextNode : objSelectedNode.NextVisibleNode;
					if (objNextNode != null)
					{
						// remove previous node
						objNextNode.Remove();
						// make a copy of previous node
						//TreeNode objNewNode = objNextNode;
						// move the previous node to under selected node
						objGroupNode.Nodes.Insert(objSelectedNode.Index - 1, objNextNode);
					}
					tvwTables.Focus();
					tvwTables.Select();
					tvwTables.SelectedNode = objSelectedNode;
				}
				else if (obj is sys_TableGroupVO)
				{
					sys_TableGroupVO voTableGroup = (sys_TableGroupVO) obj;
					intGroupID = voTableGroup.TableGroupID;

					TableManagementBO boTableManagement = new TableManagementBO();
					boTableManagement.MoveDownGroup(intGroupID);
					tvwTables.Nodes.Remove(objSelectedNode);
					tvwTables.Nodes.Insert(objSelectedNode.Index + 1, objSelectedNode);
					tvwTables.Focus();
					tvwTables.Select();
					tvwTables.SelectedNode = objSelectedNode;
				}
				SetMoveUpMoveDownButton(tvwTables.SelectedNode);
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
			catch (NullReferenceException ex)
			{
				PCSMessageBox.Show(ErrorCode.MESSAGE_TABLEMANAGEMENT_FOCUS,MessageBoxIcon.Error);
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

			
			this.Close();

			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.Default;

			#endregion Code Inserted Automatically

			
		}

		//**************************************************************************              
		///    <Description>
		///       This method uses to open a new form ViewTable to view the table content
		///       
		///    </Description>
		///    <Inputs>
		///        Tree View
		///    </Inputs>
		///    <Outputs>
		///      
		///    </Outputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <Authors>
		///       NgocHT
		///    </Authors>
		///    <History>
		///       28-Dec-2004
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************

		private void TableClick(bool blnStatus)
		{
			btnViewTable.Enabled = blnStatus;
			btnAddTable.Enabled = !blnStatus;
			btnEditTable.Enabled = blnStatus;
			btnCopyTable.Enabled = blnStatus;
			//btnPasteTable.Enabled = !blnStatus;
			btnDeleteTable.Enabled = blnStatus;
		}

		//**************************************************************************              
		///    <Description>
		///       This method is used to enable or disable the Group buttons 
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
		///       NgocHT
		///    </Authors>
		///    <History>
		///       28-Dec-2004
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************

		private void GroupClick(bool blnStatus)
		{
			btnAddGroup.Enabled = blnStatus;
			btnEditGroup.Enabled = blnStatus;
			btnCopyGroup.Enabled = blnStatus;
			btnDeleteGroup.Enabled = blnStatus;
			btnAddTable.Enabled = blnStatus;
			btnPasteTable.Enabled = blnCopy;
		}

		/*
		private void AfterGroupMove(int intOrder)
		{
			TableManagementBO boTableManagement = new TableManagementBO();
			bool blnMax = false;
			bool blnMin = false;
			boTableManagement.GroupPosition(intOrder, ref blnMin, ref blnMax);
			if (blnMin)
			{
				btnMoveUp.Enabled = false;
				btnMoveDown.Enabled = true;
			}
			else if (blnMax)
			{
				btnMoveUp.Enabled = true;
				btnMoveDown.Enabled = false;
			}
			else
			{
				btnMoveUp.Enabled = true;
				btnMoveDown.Enabled = true;
			}
		}
		*/


		/*
		private void AfterTableMove(int intGroupID, int intOrder)
		{
			TableManagementBO boTableManagement = new TableManagementBO();
			bool blnMax = false;
			bool blnMin = false;
			boTableManagement.TablePosition(intGroupID, intOrder, ref blnMin, ref blnMax);
			if (blnMin)
			{
				btnMoveUp.Enabled = false;
				btnMoveDown.Enabled = true;
			}
			else if (blnMax)
			{
				btnMoveUp.Enabled = true;
				btnMoveDown.Enabled = false;
			}
			else
			{
				btnMoveUp.Enabled = true;
				btnMoveDown.Enabled = true;
			}
		}
		*/

		//**************************************************************************              
		///    <Description>
		///       This method uses enable or disable all the button on form 
		///       
		///    </Description>
		///    <Inputs>
		///        Tree View
		///    </Inputs>
		///    <Outputs>
		///      
		///    </Outputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <Authors>
		///       NgocHT
		///    </Authors>
		///    <History>
		///       28-Dec-2004
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************

		private void CopyClick(bool blnStatus)
		{
			btnAddGroup.Enabled = !blnStatus;
			btnEditGroup.Enabled = !blnStatus;
			btnCopyGroup.Enabled = !blnStatus;
			btnDeleteGroup.Enabled = !blnStatus;
			btnViewTable.Enabled = !blnStatus;
			btnAddTable.Enabled = !blnStatus;
			btnEditTable.Enabled = !blnStatus;
			btnCopyTable.Enabled = blnStatus;
			btnPasteTable.Enabled = blnStatus;
			btnDeleteTable.Enabled = !blnStatus;
			btnMoveDown.Enabled = !blnStatus;
			btnMoveUp.Enabled = !blnStatus;
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
		///       Go to a specific node on tree 
		///       
		///    </Description>
		///    <Inputs>
		///        Tree View
		///    </Inputs>
		///    <Outputs>
		///      
		///    </Outputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <Authors>
		///       NgocHT
		///    </Authors>
		///    <History>
		///       28-Dec-2004
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************

		private void GotoTreeNode(string strType, int intID, int intParentID)
		{
			object objNode;
			sys_TableGroupVO objsys_TableGroupVO;
			foreach (TreeNode tnNode in tvwTables.Nodes)
			{
				objNode = tnNode.Tag;
				// expand selected group
				tnNode.Expand();
				if (strType == TYPE_GROUP)
				{
					if (objNode is sys_TableGroupVO)
					{
						objsys_TableGroupVO = (sys_TableGroupVO)objNode;
						if (objsys_TableGroupVO.TableGroupID == intID)
						{
							tvwTables.SelectedNode = tnNode;
							return;
						}
					}
				}
				else 
				{
					foreach (TreeNode tnNodeTable in tnNode.Nodes) 
					{
						objNode = tnNodeTable.Tag;
						if (objNode is sys_TableVO)
						{
							sys_TableVO objsys_TableVO = (sys_TableVO) objNode;
							objsys_TableGroupVO = (sys_TableGroupVO)tnNodeTable.Parent.Tag;
							if (objsys_TableVO.TableID == intID && objsys_TableGroupVO.TableGroupID == intParentID)
							{
								tvwTables.SelectedNode = tnNodeTable;
								return;
							}
						}
					}
				}
			}
			
		}

		private void tvwTables_AfterCollapse(object sender, System.Windows.Forms.TreeViewEventArgs e)
		{
			const string METHOD_NAME = THIS + ".tvwTables_AfterCollapse()";
			try
			{
				e.Node.ImageIndex = COLLAPSE_GROUP_IMG;
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

		private void tvwTables_AfterExpand(object sender, System.Windows.Forms.TreeViewEventArgs e)
		{
			const string METHOD_NAME = THIS + ".tvwTables_AfterExpand()";
			try
			{
				e.Node.ImageIndex = EXPAND_GROUP_IMG;
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

		private void tvwTables_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			const string METHOD_NAME = THIS + ".tvwTables_KeyDown()";
			try
			{
				if (e.KeyCode == Keys.Enter)
				{
					if (tvwTables.SelectedNode.Tag is sys_TableGroupVO)
					{
						if (tvwTables.SelectedNode.IsExpanded)
							tvwTables.SelectedNode.Collapse();
						else
							tvwTables.SelectedNode.Expand();
					}
					else if (tvwTables.SelectedNode.Tag is sys_TableVO)
					{
						btnViewTable_Click(null, null);
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

		private void tvwTables_DoubleClick(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".tvwTables_DoubleClick()";
			try
			{
				if (tvwTables.SelectedNode.Tag is sys_TableVO)
				{
					btnViewTable_Click(null, null);
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
		
	}
	
}

