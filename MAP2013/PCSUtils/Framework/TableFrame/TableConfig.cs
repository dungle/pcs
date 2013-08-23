using System;
using System.Collections;
using System.Windows.Forms;
using System.Data;
using PCSUtils.Utils;
using PCSComUtils.Framework.TableFrame.BO;
using PCSComUtils.Framework.TableFrame.DS;
using PCSUtils.Log;
using PCSComUtils.PCSExc; 
using PCSComUtils.Common;

namespace PCSUtils.Framework.TableFrame
{
	/// <summary>
	/// Summary description for TableConfig.
	/// </summary>
	public class TableConfig : System.Windows.Forms.Form
	{
		private const string THIS = "PCSUtils.Framework.TableFrame.TableConfig";
		private const string COPY_OF = "copy of ";

		private EnumAction mEnumType;
		private const string BASE_TABLE = "BASE TABLE";
		private const string VIEW = "VIEW";
		private const string COLUMN_NAME = "Column_Name";
		private const string IS_NULLABLE = "Is_Nullable";
		private const string DATA_TYPE = "Data_Type";
		private const string ORDINAL_POSITION = "Ordinal_Position";
		private const string NVARCHAR_TYPE = "nvarchar";
		private const string BIT_TYPE = "bit";
		private const string INT_TYPE = "int";
		private const string DECIMAL_TYPE = "decimal";
		private const string DATETIME_TYPE = "datetime";
		private const string FLOAT_TYPE = "float";
		private const string TINYINT_TYPE = "tinyint";
		private const string SMALLDATETIME_TYPE = "smalldatetime";
		private const string CHAR_TYPE = "char";
		private const string SMALLINT_TYPE = "smallint";
		private const string IMAGE_TYPE = "image";
		private const string VARCHAR_TYPE = "varchar";
		private const string BIGINT_TYPE = "bigint";
		private const string YES = "YES";
		private const string NO = "No";
		private const int ALIGN_RIGHT = 2;
		private const int ALIGN_CENTER = 1;
		private const int ALIGN_LEFT = 0;
		private const int DEFAULT_WIDTH = 100;
		private const int INT_NONE = 0;
		private const int INT_NORMAL = 0;
		



		private int intTableID;
		private int mGroupID;
		private sys_TableVO voSysTable = new sys_TableVO();
		private bool blnSavingStatus;

		private bool blnEnableEditDetailConfig ; //This variable allows user to Edit detail or not

		private System.Windows.Forms.Button btnSave;
		private System.Windows.Forms.Button btnDetailConfig;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnHelp;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox txtTableName;
		private System.Windows.Forms.TextBox txtTableCode;
		private System.Windows.Forms.ComboBox cboTableView;
		private C1.Win.C1Input.C1NumericEdit txtHeight;
		private System.Windows.Forms.CheckBox chkIsViewOnly;	
	

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public TableConfig()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}
		public EnumAction EnumType
		{
			get 
			{
				return mEnumType;
			}
			set 
			{
				mEnumType = value;
			}
		}	
		public int GroupID
		{
			get 
			{
				return mGroupID;
			}
			set	
			{
				mGroupID = value;
			}
		}	
		public sys_TableVO SysTableVO
		{
			get 
			{
				return voSysTable;
			}
			set 
			{
				voSysTable = value;
			}
		}
		public int TableID
		{
			get
			{
				return intTableID;
			}
			set
			{
				intTableID = value;
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

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(TableConfig));
			this.btnDetailConfig = new System.Windows.Forms.Button();
			this.btnHelp = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnSave = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.txtTableName = new System.Windows.Forms.TextBox();
			this.txtTableCode = new System.Windows.Forms.TextBox();
			this.cboTableView = new System.Windows.Forms.ComboBox();
			this.txtHeight = new C1.Win.C1Input.C1NumericEdit();
			this.chkIsViewOnly = new System.Windows.Forms.CheckBox();
			((System.ComponentModel.ISupportInitialize)(this.txtHeight)).BeginInit();
			this.SuspendLayout();
			// 
			// btnDetailConfig
			// 
			this.btnDetailConfig.AccessibleDescription = resources.GetString("btnDetailConfig.AccessibleDescription");
			this.btnDetailConfig.AccessibleName = resources.GetString("btnDetailConfig.AccessibleName");
			this.btnDetailConfig.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnDetailConfig.Anchor")));
			this.btnDetailConfig.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnDetailConfig.BackgroundImage")));
			this.btnDetailConfig.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnDetailConfig.Dock")));
			this.btnDetailConfig.Enabled = ((bool)(resources.GetObject("btnDetailConfig.Enabled")));
			this.btnDetailConfig.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnDetailConfig.FlatStyle")));
			this.btnDetailConfig.Font = ((System.Drawing.Font)(resources.GetObject("btnDetailConfig.Font")));
			this.btnDetailConfig.Image = ((System.Drawing.Image)(resources.GetObject("btnDetailConfig.Image")));
			this.btnDetailConfig.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnDetailConfig.ImageAlign")));
			this.btnDetailConfig.ImageIndex = ((int)(resources.GetObject("btnDetailConfig.ImageIndex")));
			this.btnDetailConfig.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnDetailConfig.ImeMode")));
			this.btnDetailConfig.Location = ((System.Drawing.Point)(resources.GetObject("btnDetailConfig.Location")));
			this.btnDetailConfig.Name = "btnDetailConfig";
			this.btnDetailConfig.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnDetailConfig.RightToLeft")));
			this.btnDetailConfig.Size = ((System.Drawing.Size)(resources.GetObject("btnDetailConfig.Size")));
			this.btnDetailConfig.TabIndex = ((int)(resources.GetObject("btnDetailConfig.TabIndex")));
			this.btnDetailConfig.Text = resources.GetString("btnDetailConfig.Text");
			this.btnDetailConfig.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnDetailConfig.TextAlign")));
			this.btnDetailConfig.Visible = ((bool)(resources.GetObject("btnDetailConfig.Visible")));
			this.btnDetailConfig.Click += new System.EventHandler(this.btnDetailConfig_Click);
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
			this.btnCancel.Visible = ((bool)(resources.GetObject("btnCancel.Visible")));
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
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
			// label6
			// 
			this.label6.AccessibleDescription = resources.GetString("label6.AccessibleDescription");
			this.label6.AccessibleName = resources.GetString("label6.AccessibleName");
			this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("label6.Anchor")));
			this.label6.AutoSize = ((bool)(resources.GetObject("label6.AutoSize")));
			this.label6.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("label6.Dock")));
			this.label6.Enabled = ((bool)(resources.GetObject("label6.Enabled")));
			this.label6.Font = ((System.Drawing.Font)(resources.GetObject("label6.Font")));
			this.label6.Image = ((System.Drawing.Image)(resources.GetObject("label6.Image")));
			this.label6.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label6.ImageAlign")));
			this.label6.ImageIndex = ((int)(resources.GetObject("label6.ImageIndex")));
			this.label6.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("label6.ImeMode")));
			this.label6.Location = ((System.Drawing.Point)(resources.GetObject("label6.Location")));
			this.label6.Name = "label6";
			this.label6.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("label6.RightToLeft")));
			this.label6.Size = ((System.Drawing.Size)(resources.GetObject("label6.Size")));
			this.label6.TabIndex = ((int)(resources.GetObject("label6.TabIndex")));
			this.label6.Text = resources.GetString("label6.Text");
			this.label6.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label6.TextAlign")));
			this.label6.Visible = ((bool)(resources.GetObject("label6.Visible")));
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
			this.label2.Visible = ((bool)(resources.GetObject("label2.Visible")));
			// 
			// txtTableName
			// 
			this.txtTableName.AccessibleDescription = resources.GetString("txtTableName.AccessibleDescription");
			this.txtTableName.AccessibleName = resources.GetString("txtTableName.AccessibleName");
			this.txtTableName.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("txtTableName.Anchor")));
			this.txtTableName.AutoSize = ((bool)(resources.GetObject("txtTableName.AutoSize")));
			this.txtTableName.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("txtTableName.BackgroundImage")));
			this.txtTableName.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("txtTableName.Dock")));
			this.txtTableName.Enabled = ((bool)(resources.GetObject("txtTableName.Enabled")));
			this.txtTableName.Font = ((System.Drawing.Font)(resources.GetObject("txtTableName.Font")));
			this.txtTableName.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("txtTableName.ImeMode")));
			this.txtTableName.Location = ((System.Drawing.Point)(resources.GetObject("txtTableName.Location")));
			this.txtTableName.MaxLength = ((int)(resources.GetObject("txtTableName.MaxLength")));
			this.txtTableName.Multiline = ((bool)(resources.GetObject("txtTableName.Multiline")));
			this.txtTableName.Name = "txtTableName";
			this.txtTableName.PasswordChar = ((char)(resources.GetObject("txtTableName.PasswordChar")));
			this.txtTableName.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("txtTableName.RightToLeft")));
			this.txtTableName.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("txtTableName.ScrollBars")));
			this.txtTableName.Size = ((System.Drawing.Size)(resources.GetObject("txtTableName.Size")));
			this.txtTableName.TabIndex = ((int)(resources.GetObject("txtTableName.TabIndex")));
			this.txtTableName.Text = resources.GetString("txtTableName.Text");
			this.txtTableName.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("txtTableName.TextAlign")));
			this.txtTableName.Visible = ((bool)(resources.GetObject("txtTableName.Visible")));
			this.txtTableName.WordWrap = ((bool)(resources.GetObject("txtTableName.WordWrap")));
			this.txtTableName.TextChanged += new System.EventHandler(this.txtTableName_TextChanged);
			this.txtTableName.Leave += new System.EventHandler(this.OnLeaveControl);
			this.txtTableName.Enter += new System.EventHandler(this.OnEnterControl);
			// 
			// txtTableCode
			// 
			this.txtTableCode.AccessibleDescription = resources.GetString("txtTableCode.AccessibleDescription");
			this.txtTableCode.AccessibleName = resources.GetString("txtTableCode.AccessibleName");
			this.txtTableCode.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("txtTableCode.Anchor")));
			this.txtTableCode.AutoSize = ((bool)(resources.GetObject("txtTableCode.AutoSize")));
			this.txtTableCode.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("txtTableCode.BackgroundImage")));
			this.txtTableCode.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("txtTableCode.Dock")));
			this.txtTableCode.Enabled = ((bool)(resources.GetObject("txtTableCode.Enabled")));
			this.txtTableCode.Font = ((System.Drawing.Font)(resources.GetObject("txtTableCode.Font")));
			this.txtTableCode.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("txtTableCode.ImeMode")));
			this.txtTableCode.Location = ((System.Drawing.Point)(resources.GetObject("txtTableCode.Location")));
			this.txtTableCode.MaxLength = ((int)(resources.GetObject("txtTableCode.MaxLength")));
			this.txtTableCode.Multiline = ((bool)(resources.GetObject("txtTableCode.Multiline")));
			this.txtTableCode.Name = "txtTableCode";
			this.txtTableCode.PasswordChar = ((char)(resources.GetObject("txtTableCode.PasswordChar")));
			this.txtTableCode.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("txtTableCode.RightToLeft")));
			this.txtTableCode.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("txtTableCode.ScrollBars")));
			this.txtTableCode.Size = ((System.Drawing.Size)(resources.GetObject("txtTableCode.Size")));
			this.txtTableCode.TabIndex = ((int)(resources.GetObject("txtTableCode.TabIndex")));
			this.txtTableCode.Text = resources.GetString("txtTableCode.Text");
			this.txtTableCode.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("txtTableCode.TextAlign")));
			this.txtTableCode.Visible = ((bool)(resources.GetObject("txtTableCode.Visible")));
			this.txtTableCode.WordWrap = ((bool)(resources.GetObject("txtTableCode.WordWrap")));
			this.txtTableCode.TextChanged += new System.EventHandler(this.txtTableCode_TextChanged);
			this.txtTableCode.Leave += new System.EventHandler(this.OnLeaveControl);
			this.txtTableCode.Enter += new System.EventHandler(this.OnEnterControl);
			// 
			// cboTableView
			// 
			this.cboTableView.AccessibleDescription = resources.GetString("cboTableView.AccessibleDescription");
			this.cboTableView.AccessibleName = resources.GetString("cboTableView.AccessibleName");
			this.cboTableView.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("cboTableView.Anchor")));
			this.cboTableView.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cboTableView.BackgroundImage")));
			this.cboTableView.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("cboTableView.Dock")));
			this.cboTableView.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboTableView.Enabled = ((bool)(resources.GetObject("cboTableView.Enabled")));
			this.cboTableView.Font = ((System.Drawing.Font)(resources.GetObject("cboTableView.Font")));
			this.cboTableView.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("cboTableView.ImeMode")));
			this.cboTableView.IntegralHeight = ((bool)(resources.GetObject("cboTableView.IntegralHeight")));
			this.cboTableView.ItemHeight = ((int)(resources.GetObject("cboTableView.ItemHeight")));
			this.cboTableView.Location = ((System.Drawing.Point)(resources.GetObject("cboTableView.Location")));
			this.cboTableView.MaxDropDownItems = ((int)(resources.GetObject("cboTableView.MaxDropDownItems")));
			this.cboTableView.MaxLength = ((int)(resources.GetObject("cboTableView.MaxLength")));
			this.cboTableView.Name = "cboTableView";
			this.cboTableView.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("cboTableView.RightToLeft")));
			this.cboTableView.Size = ((System.Drawing.Size)(resources.GetObject("cboTableView.Size")));
			this.cboTableView.TabIndex = ((int)(resources.GetObject("cboTableView.TabIndex")));
			this.cboTableView.Text = resources.GetString("cboTableView.Text");
			this.cboTableView.Visible = ((bool)(resources.GetObject("cboTableView.Visible")));
			this.cboTableView.TextChanged += new System.EventHandler(this.cboTableView_TextChanged);
			this.cboTableView.SelectedIndexChanged += new System.EventHandler(this.cboTableView_SelectedIndexChanged);
			this.cboTableView.Leave += new System.EventHandler(this.OnLeaveControl);
			this.cboTableView.Enter += new System.EventHandler(this.OnEnterControl);
			// 
			// txtHeight
			// 
			this.txtHeight.AcceptsEscape = ((bool)(resources.GetObject("txtHeight.AcceptsEscape")));
			this.txtHeight.AccessibleDescription = resources.GetString("txtHeight.AccessibleDescription");
			this.txtHeight.AccessibleName = resources.GetString("txtHeight.AccessibleName");
			this.txtHeight.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("txtHeight.Anchor")));
			this.txtHeight.AutoSize = ((bool)(resources.GetObject("txtHeight.AutoSize")));
			this.txtHeight.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("txtHeight.BackgroundImage")));
			this.txtHeight.BorderStyle = ((System.Windows.Forms.BorderStyle)(resources.GetObject("txtHeight.BorderStyle")));
			// 
			// txtHeight.Calculator
			// 
			this.txtHeight.Calculator.AccessibleDescription = resources.GetString("txtHeight.Calculator.AccessibleDescription");
			this.txtHeight.Calculator.AccessibleName = resources.GetString("txtHeight.Calculator.AccessibleName");
			this.txtHeight.Calculator.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("txtHeight.Calculator.BackgroundImage")));
			this.txtHeight.Calculator.ButtonFlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("txtHeight.Calculator.ButtonFlatStyle")));
			this.txtHeight.Calculator.DisplayFormat = resources.GetString("txtHeight.Calculator.DisplayFormat");
			this.txtHeight.Calculator.Font = ((System.Drawing.Font)(resources.GetObject("txtHeight.Calculator.Font")));
			this.txtHeight.Calculator.FormatOnClose = ((bool)(resources.GetObject("txtHeight.Calculator.FormatOnClose")));
			this.txtHeight.Calculator.StoredFormat = resources.GetString("txtHeight.Calculator.StoredFormat");
			this.txtHeight.Calculator.UIStrings.Content = ((string[])(resources.GetObject("txtHeight.Calculator.UIStrings.Content")));
			this.txtHeight.CaseSensitive = ((bool)(resources.GetObject("txtHeight.CaseSensitive")));
			this.txtHeight.Culture = ((int)(resources.GetObject("txtHeight.Culture")));
			this.txtHeight.CustomFormat = resources.GetString("txtHeight.CustomFormat");
			this.txtHeight.DataType = ((System.Type)(resources.GetObject("txtHeight.DataType")));
			this.txtHeight.DisplayFormat.CustomFormat = resources.GetString("txtHeight.DisplayFormat.CustomFormat");
			this.txtHeight.DisplayFormat.FormatType = ((C1.Win.C1Input.FormatTypeEnum)(resources.GetObject("txtHeight.DisplayFormat.FormatType")));
			this.txtHeight.DisplayFormat.Inherit = ((C1.Win.C1Input.FormatInfoInheritFlags)(resources.GetObject("txtHeight.DisplayFormat.Inherit")));
			this.txtHeight.DisplayFormat.NullText = resources.GetString("txtHeight.DisplayFormat.NullText");
			this.txtHeight.DisplayFormat.TrimEnd = ((bool)(resources.GetObject("txtHeight.DisplayFormat.TrimEnd")));
			this.txtHeight.DisplayFormat.TrimStart = ((bool)(resources.GetObject("txtHeight.DisplayFormat.TrimStart")));
			this.txtHeight.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("txtHeight.Dock")));
			this.txtHeight.DropDownFormAlign = ((C1.Win.C1Input.DropDownFormAlignmentEnum)(resources.GetObject("txtHeight.DropDownFormAlign")));
			this.txtHeight.EditFormat.CustomFormat = resources.GetString("txtHeight.EditFormat.CustomFormat");
			this.txtHeight.EditFormat.FormatType = ((C1.Win.C1Input.FormatTypeEnum)(resources.GetObject("txtHeight.EditFormat.FormatType")));
			this.txtHeight.EditFormat.Inherit = ((C1.Win.C1Input.FormatInfoInheritFlags)(resources.GetObject("txtHeight.EditFormat.Inherit")));
			this.txtHeight.EditFormat.NullText = resources.GetString("txtHeight.EditFormat.NullText");
			this.txtHeight.EditFormat.TrimEnd = ((bool)(resources.GetObject("txtHeight.EditFormat.TrimEnd")));
			this.txtHeight.EditFormat.TrimStart = ((bool)(resources.GetObject("txtHeight.EditFormat.TrimStart")));
			this.txtHeight.EditMask = resources.GetString("txtHeight.EditMask");
			this.txtHeight.EmptyAsNull = ((bool)(resources.GetObject("txtHeight.EmptyAsNull")));
			this.txtHeight.Enabled = ((bool)(resources.GetObject("txtHeight.Enabled")));
			this.txtHeight.ErrorInfo.BeepOnError = ((bool)(resources.GetObject("txtHeight.ErrorInfo.BeepOnError")));
			this.txtHeight.ErrorInfo.ErrorMessage = resources.GetString("txtHeight.ErrorInfo.ErrorMessage");
			this.txtHeight.ErrorInfo.ErrorMessageCaption = resources.GetString("txtHeight.ErrorInfo.ErrorMessageCaption");
			this.txtHeight.ErrorInfo.ShowErrorMessage = ((bool)(resources.GetObject("txtHeight.ErrorInfo.ShowErrorMessage")));
			this.txtHeight.ErrorInfo.ValueOnError = ((object)(resources.GetObject("txtHeight.ErrorInfo.ValueOnError")));
			this.txtHeight.Font = ((System.Drawing.Font)(resources.GetObject("txtHeight.Font")));
			this.txtHeight.FormatType = ((C1.Win.C1Input.FormatTypeEnum)(resources.GetObject("txtHeight.FormatType")));
			this.txtHeight.GapHeight = ((int)(resources.GetObject("txtHeight.GapHeight")));
			this.txtHeight.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("txtHeight.ImeMode")));
			this.txtHeight.Increment = ((object)(resources.GetObject("txtHeight.Increment")));
			this.txtHeight.InitialSelection = ((C1.Win.C1Input.InitialSelectionEnum)(resources.GetObject("txtHeight.InitialSelection")));
			this.txtHeight.Location = ((System.Drawing.Point)(resources.GetObject("txtHeight.Location")));
			this.txtHeight.MaskInfo.AutoTabWhenFilled = ((bool)(resources.GetObject("txtHeight.MaskInfo.AutoTabWhenFilled")));
			this.txtHeight.MaskInfo.CaseSensitive = ((bool)(resources.GetObject("txtHeight.MaskInfo.CaseSensitive")));
			this.txtHeight.MaskInfo.CopyWithLiterals = ((bool)(resources.GetObject("txtHeight.MaskInfo.CopyWithLiterals")));
			this.txtHeight.MaskInfo.EditMask = resources.GetString("txtHeight.MaskInfo.EditMask");
			this.txtHeight.MaskInfo.EmptyAsNull = ((bool)(resources.GetObject("txtHeight.MaskInfo.EmptyAsNull")));
			this.txtHeight.MaskInfo.ErrorMessage = resources.GetString("txtHeight.MaskInfo.ErrorMessage");
			this.txtHeight.MaskInfo.Inherit = ((C1.Win.C1Input.MaskInfoInheritFlags)(resources.GetObject("txtHeight.MaskInfo.Inherit")));
			this.txtHeight.MaskInfo.PromptChar = ((char)(resources.GetObject("txtHeight.MaskInfo.PromptChar")));
			this.txtHeight.MaskInfo.SaveLiterals = false;
			this.txtHeight.MaskInfo.ShowLiterals = ((C1.Win.C1Input.ShowLiteralsEnum)(resources.GetObject("txtHeight.MaskInfo.ShowLiterals")));
			this.txtHeight.MaskInfo.StoredEmptyChar = ((char)(resources.GetObject("txtHeight.MaskInfo.StoredEmptyChar")));
			this.txtHeight.MaxLength = ((int)(resources.GetObject("txtHeight.MaxLength")));
			this.txtHeight.Name = "txtHeight";
			this.txtHeight.NullText = resources.GetString("txtHeight.NullText");
			this.txtHeight.NumericInputKeys = C1.Win.C1Input.NumericInputKeyFlags.None;
			this.txtHeight.ParseInfo.CaseSensitive = ((bool)(resources.GetObject("txtHeight.ParseInfo.CaseSensitive")));
			this.txtHeight.ParseInfo.CustomFormat = resources.GetString("txtHeight.ParseInfo.CustomFormat");
			this.txtHeight.ParseInfo.EmptyAsNull = ((bool)(resources.GetObject("txtHeight.ParseInfo.EmptyAsNull")));
			this.txtHeight.ParseInfo.ErrorMessage = resources.GetString("txtHeight.ParseInfo.ErrorMessage");
			this.txtHeight.ParseInfo.FormatType = ((C1.Win.C1Input.FormatTypeEnum)(resources.GetObject("txtHeight.ParseInfo.FormatType")));
			this.txtHeight.ParseInfo.Inherit = ((C1.Win.C1Input.ParseInfoInheritFlags)(resources.GetObject("txtHeight.ParseInfo.Inherit")));
			this.txtHeight.ParseInfo.NullText = resources.GetString("txtHeight.ParseInfo.NullText");
			this.txtHeight.ParseInfo.NumberStyle = ((C1.Win.C1Input.NumberStyleFlags)(resources.GetObject("txtHeight.ParseInfo.NumberStyle")));
			this.txtHeight.ParseInfo.TrimEnd = ((bool)(resources.GetObject("txtHeight.ParseInfo.TrimEnd")));
			this.txtHeight.ParseInfo.TrimStart = ((bool)(resources.GetObject("txtHeight.ParseInfo.TrimStart")));
			this.txtHeight.PasswordChar = ((char)(resources.GetObject("txtHeight.PasswordChar")));
			this.txtHeight.PostValidation.CaseSensitive = ((bool)(resources.GetObject("txtHeight.PostValidation.CaseSensitive")));
			this.txtHeight.PostValidation.ErrorMessage = resources.GetString("txtHeight.PostValidation.ErrorMessage");
			this.txtHeight.PostValidation.Inherit = ((C1.Win.C1Input.PostValidationInheritFlags)(resources.GetObject("txtHeight.PostValidation.Inherit")));
			this.txtHeight.PostValidation.Validation = ((C1.Win.C1Input.PostValidationTypeEnum)(resources.GetObject("txtHeight.PostValidation.Validation")));
			this.txtHeight.PostValidation.Values = ((System.Array)(resources.GetObject("txtHeight.PostValidation.Values")));
			this.txtHeight.PostValidation.ValuesExcluded = ((System.Array)(resources.GetObject("txtHeight.PostValidation.ValuesExcluded")));
			this.txtHeight.PreValidation.CaseSensitive = ((bool)(resources.GetObject("txtHeight.PreValidation.CaseSensitive")));
			this.txtHeight.PreValidation.ErrorMessage = resources.GetString("txtHeight.PreValidation.ErrorMessage");
			this.txtHeight.PreValidation.Inherit = ((C1.Win.C1Input.PreValidationInheritFlags)(resources.GetObject("txtHeight.PreValidation.Inherit")));
			this.txtHeight.PreValidation.ItemSeparator = resources.GetString("txtHeight.PreValidation.ItemSeparator");
			this.txtHeight.PreValidation.PatternString = resources.GetString("txtHeight.PreValidation.PatternString");
			this.txtHeight.PreValidation.RegexOptions = ((C1.Win.C1Input.RegexOptionFlags)(resources.GetObject("txtHeight.PreValidation.RegexOptions")));
			this.txtHeight.PreValidation.TrimEnd = ((bool)(resources.GetObject("txtHeight.PreValidation.TrimEnd")));
			this.txtHeight.PreValidation.TrimStart = ((bool)(resources.GetObject("txtHeight.PreValidation.TrimStart")));
			this.txtHeight.PreValidation.Validation = ((C1.Win.C1Input.PreValidationTypeEnum)(resources.GetObject("txtHeight.PreValidation.Validation")));
			this.txtHeight.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("txtHeight.RightToLeft")));
			this.txtHeight.ShowFocusRectangle = ((bool)(resources.GetObject("txtHeight.ShowFocusRectangle")));
			this.txtHeight.Size = ((System.Drawing.Size)(resources.GetObject("txtHeight.Size")));
			this.txtHeight.TabIndex = ((int)(resources.GetObject("txtHeight.TabIndex")));
			this.txtHeight.Tag = ((object)(resources.GetObject("txtHeight.Tag")));
			this.txtHeight.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("txtHeight.TextAlign")));
			this.txtHeight.TrimEnd = ((bool)(resources.GetObject("txtHeight.TrimEnd")));
			this.txtHeight.TrimStart = ((bool)(resources.GetObject("txtHeight.TrimStart")));
			this.txtHeight.UserCultureOverride = ((bool)(resources.GetObject("txtHeight.UserCultureOverride")));
			this.txtHeight.Value = ((object)(resources.GetObject("txtHeight.Value")));
			this.txtHeight.VerticalAlign = ((C1.Win.C1Input.VerticalAlignEnum)(resources.GetObject("txtHeight.VerticalAlign")));
			this.txtHeight.Visible = ((bool)(resources.GetObject("txtHeight.Visible")));
			this.txtHeight.VisibleButtons = ((C1.Win.C1Input.DropDownControlButtonFlags)(resources.GetObject("txtHeight.VisibleButtons")));
			this.txtHeight.TextChanged += new System.EventHandler(this.txtHeight_TextChanged_1);
			this.txtHeight.Enter += new System.EventHandler(this.OnEnterControl);
			this.txtHeight.Leave += new System.EventHandler(this.OnLeaveControl);
			// 
			// chkIsViewOnly
			// 
			this.chkIsViewOnly.AccessibleDescription = resources.GetString("chkIsViewOnly.AccessibleDescription");
			this.chkIsViewOnly.AccessibleName = resources.GetString("chkIsViewOnly.AccessibleName");
			this.chkIsViewOnly.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("chkIsViewOnly.Anchor")));
			this.chkIsViewOnly.Appearance = ((System.Windows.Forms.Appearance)(resources.GetObject("chkIsViewOnly.Appearance")));
			this.chkIsViewOnly.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("chkIsViewOnly.BackgroundImage")));
			this.chkIsViewOnly.CheckAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("chkIsViewOnly.CheckAlign")));
			this.chkIsViewOnly.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("chkIsViewOnly.Dock")));
			this.chkIsViewOnly.Enabled = ((bool)(resources.GetObject("chkIsViewOnly.Enabled")));
			this.chkIsViewOnly.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("chkIsViewOnly.FlatStyle")));
			this.chkIsViewOnly.Font = ((System.Drawing.Font)(resources.GetObject("chkIsViewOnly.Font")));
			this.chkIsViewOnly.Image = ((System.Drawing.Image)(resources.GetObject("chkIsViewOnly.Image")));
			this.chkIsViewOnly.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("chkIsViewOnly.ImageAlign")));
			this.chkIsViewOnly.ImageIndex = ((int)(resources.GetObject("chkIsViewOnly.ImageIndex")));
			this.chkIsViewOnly.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("chkIsViewOnly.ImeMode")));
			this.chkIsViewOnly.Location = ((System.Drawing.Point)(resources.GetObject("chkIsViewOnly.Location")));
			this.chkIsViewOnly.Name = "chkIsViewOnly";
			this.chkIsViewOnly.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("chkIsViewOnly.RightToLeft")));
			this.chkIsViewOnly.Size = ((System.Drawing.Size)(resources.GetObject("chkIsViewOnly.Size")));
			this.chkIsViewOnly.TabIndex = ((int)(resources.GetObject("chkIsViewOnly.TabIndex")));
			this.chkIsViewOnly.Text = resources.GetString("chkIsViewOnly.Text");
			this.chkIsViewOnly.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("chkIsViewOnly.TextAlign")));
			this.chkIsViewOnly.Visible = ((bool)(resources.GetObject("chkIsViewOnly.Visible")));
			// 
			// TableConfig
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
			this.Controls.Add(this.chkIsViewOnly);
			this.Controls.Add(this.txtHeight);
			this.Controls.Add(this.btnHelp);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnSave);
			this.Controls.Add(this.btnDetailConfig);
			this.Controls.Add(this.txtTableCode);
			this.Controls.Add(this.txtTableName);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.cboTableView);
			this.Enabled = ((bool)(resources.GetObject("$this.Enabled")));
			this.Font = ((System.Drawing.Font)(resources.GetObject("$this.Font")));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("$this.ImeMode")));
			this.Location = ((System.Drawing.Point)(resources.GetObject("$this.Location")));
			this.MaximizeBox = false;
			this.MaximumSize = ((System.Drawing.Size)(resources.GetObject("$this.MaximumSize")));
			this.MinimumSize = ((System.Drawing.Size)(resources.GetObject("$this.MinimumSize")));
			this.Name = "TableConfig";
			this.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("$this.RightToLeft")));
			this.StartPosition = ((System.Windows.Forms.FormStartPosition)(resources.GetObject("$this.StartPosition")));
			this.Text = resources.GetString("$this.Text");
			this.Closing += new System.ComponentModel.CancelEventHandler(this.TableConfig_Closing);
			this.Load += new System.EventHandler(this.TableConfig_Load);
			this.Activated += new System.EventHandler(this.TableConfig_Activated);
			((System.ComponentModel.ISupportInitialize)(this.txtHeight)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		//**************************************************************************              
		///    <Description>
		///       Load form and load data on form
		///    </Description>
		///    <Inputs>
		///               
		///    </Inputs>
		///    <Outputs>
		///       
		///    </Outputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <Authors>
		///       NgocHT - iSphere Software
		///    </Authors>
		///    <History>
		///       28-Dec-2004
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private void TableConfig_Load(object sender, System.EventArgs e)
		{
			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.WaitCursor;

			#endregion Code Inserted Automatically

			
			blnEnableEditDetailConfig = false;
			btnDetailConfig.Enabled = false;
			const string METHOD_NAME = THIS + ".TableConfig_Load()";
			try
			{
				TableConfigBO boTableConfig = new TableConfigBO();
				// Load Table/View Combobox 
				DataSet dstDataSet = boTableConfig.ListTableOrView();
				cboTableView.DataSource = dstDataSet.Tables[SchemaTableTable.TABLE_NAME];
				cboTableView.DisplayMember = SchemaTableTable.TABLENAME_FLD;
				cboTableView.ValueMember = SchemaTableTable.TABLETYPE_FLD;
				// if form showed to Add new row in sys_Table
				if(mEnumType == EnumAction.Add)
				{
					// Load blank form if form showed to Addnew record
					voSysTable = new sys_TableVO();
				}
				// if form showed to Edit row in sys_Table
				else if(mEnumType == EnumAction.Edit)
				{
					// Load detail if form showed to Edit record
					DisplayValueInComboBox(cboTableView,voSysTable.TableOrView);
					cboTableView.Enabled = false;
					txtTableCode.Text = voSysTable.Code.Trim();
					txtTableName.Text = voSysTable.TableName.Trim();
					txtHeight.Value = voSysTable.Height.ToString().Trim();
					intTableID = voSysTable.TableID;
					blnEnableEditDetailConfig = true;
					btnDetailConfig.Enabled = true;
				}		
				// if form showed to Paste row in sys_Table
				else if(mEnumType == EnumAction.Copy)
				{
					// Load detail if form showed to Edit record
					cboTableView.Text = voSysTable.TableOrView;
					txtTableCode.Text = COPY_OF + voSysTable.Code.Trim();
					txtTableName.Text = COPY_OF + voSysTable.TableName.Trim();					
					txtHeight.Value = voSysTable.Height.ToString().Trim();
				}	
				if (voSysTable.IsViewOnly)
				{
					chkIsViewOnly.Checked = true;
					if (cboTableView.SelectedValue.ToString() == VIEW)
					{
						chkIsViewOnly.Enabled = false;		
					}
					else
					{
						chkIsViewOnly.Enabled = true;
					}
				}
				else
				{
					
					if (cboTableView.SelectedValue.ToString() == VIEW)
					{
						chkIsViewOnly.Checked = true;
						chkIsViewOnly.Enabled = false;	
					}
					else
					{
						chkIsViewOnly.Enabled = true;
						chkIsViewOnly.Checked = false;
					}
				}
				
				//chkIsViewOnly.Enabled = false;
				// Set Saving status
				blnSavingStatus = true;
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
		///       Save data into database
		///    </Description>
		///    <Inputs>
		///               
		///    </Inputs>
		///    <Outputs>
		///       
		///    </Outputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <Authors>
		///       NgocHT - iSphere Software
		///    </Authors>
		///    <History>
		///       29-Dec-2004
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private bool SaveGroup()
		{
			const string METHOD_NAME = THIS + ".TableConfig_Load()";
			try
			{
				// check data in Table Code
				if (!CheckMandatory(txtTableCode))
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID,MessageBoxIcon.Error);
					txtTableCode.Focus();
					txtTableCode.Select();
					return false;					
				}
				// check data in Table Name
				if (!CheckMandatory(txtTableName))
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID,MessageBoxIcon.Error);
					txtTableName.Focus();
					txtTableName.Select();
					return false;					
				}
				// check data in Table or View
				if (!CheckMandatory(cboTableView))
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID,MessageBoxIcon.Error);
					cboTableView.Focus();
					return false;					
				}
				// check data in Height
				if (!CheckMandatory(txtHeight))
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID,MessageBoxIcon.Error);
					txtHeight.Focus();
					txtHeight.Select();
					return false;					
				}
				// check nuberic in Height textbox
				if (!ValidateNumeric(txtHeight))
				{
					PCSMessageBox.Show(ErrorCode.MESSAGE_TABLEMANAGEMENT_HEIGH_NUMERIC);
					txtHeight.Focus();
					txtHeight.Select();
					return false;					
				}
				// Check negative number
				if(int.Parse(txtHeight.Text.Trim()) <= 0)
				{
					PCSMessageBox.Show(ErrorCode.POSITIVE_NUMBER,MessageBoxIcon.Error);
					txtHeight.Focus();
					txtHeight.Select();
					return false;
				}
				// if form showed to Add new row in sys_Table
				if(mEnumType == EnumAction.Add)
				{
					// call function Add() method for sys_table  
					//sys_TableVO voSysTable = new sys_TableVO();
					voSysTable.Code = txtTableCode.Text.Trim() ;
					voSysTable.TableName = txtTableName.Text.Trim();
					voSysTable.TableOrView = cboTableView.Text.Trim();
					voSysTable.Height = int.Parse(txtHeight.Text.Trim());
					voSysTable.IsViewOnly = chkIsViewOnly.Checked;
					
					TableConfigBO boTableConfig = new TableConfigBO();
					//boTableConfig.AddTable(voSysTable,mGroupID);
					//Insert a new table and get its latest value
					intTableID = boTableConfig.AddTableAndReturnMaxID(voSysTable,mGroupID);
					voSysTable.TableID = intTableID;
					// HACK: Trada 30-11-2005
					//Save Detail Config
					//Get list all fields from Table
					DataSet dstListAllColumnName = boTableConfig.GetAllColumnNameOfTable(voSysTable.TableOrView);
					foreach(DataRow drow in dstListAllColumnName.Tables[0].Rows)
					{
						sys_TableFieldVO voTableField = new sys_TableFieldVO();
						voTableField.TableID = voSysTable.TableID;
						voTableField.Caption = voTableField.CaptionEN = 
							voTableField.CaptionJP = voTableField.CaptionVN =
							voTableField.FieldName = drow[COLUMN_NAME].ToString();
						if ((drow[DATA_TYPE].ToString() == BIGINT_TYPE)
							||(drow[DATA_TYPE].ToString() == INT_TYPE)
							||(drow[DATA_TYPE].ToString() == SMALLINT_TYPE)
							||(drow[DATA_TYPE].ToString() == DECIMAL_TYPE)
							||(drow[DATA_TYPE].ToString() == TINYINT_TYPE)
							||(drow[DATA_TYPE].ToString() == FLOAT_TYPE))
						{
							voTableField.Align = ALIGN_RIGHT;		
						}
						else
							if ((drow[DATA_TYPE].ToString() == CHAR_TYPE)
								||(drow[DATA_TYPE].ToString() == NVARCHAR_TYPE)
								||(drow[DATA_TYPE].ToString() == VARCHAR_TYPE))
							{
								voTableField.Align = ALIGN_LEFT;		
							}
							else
								{
									voTableField.Align = ALIGN_CENTER;
								}
						if (drow[IS_NULLABLE].ToString() == YES)
						{
							voTableField.NotAllowNull = false;
						}
						else
							voTableField.NotAllowNull = true;
						voTableField.Width = DEFAULT_WIDTH;
						voTableField.SortType = INT_NONE;
						voTableField.CharacterCase = INT_NORMAL;
						voTableField.Invisible = true;
						voTableField.ReadOnly = false;
						//Get Column Properties
						TableConfigDetailBO boTableConfigDetail = new TableConfigDetailBO();
						ArrayList arrColumnProperties = new ArrayList();
                        arrColumnProperties = boTableConfigDetail.GetColumnProperty(voSysTable.TableOrView, drow[COLUMN_NAME].ToString());
						int intValue;
						//IsIdentity
						intValue = (int)arrColumnProperties[0];
						voTableField.IdentityColumn = (intValue > 0) ? true : false;
						//IsIndexable
						intValue = (int)arrColumnProperties[1];
						voTableField.UniqueColumn = (intValue > 0) ? true : false;
						//Field Order
						voTableField.FieldOrder = int.Parse(drow[ORDINAL_POSITION].ToString());
						//Save to database
						boTableConfigDetail.Add(voTableField);
					}
					
					// END: Trada 30-11-2005
					//Change the Type of this form into Edit mode
					mEnumType = EnumAction.Edit;
					cboTableView.Enabled = false;

					//Enable user to open the detail config form
					blnEnableEditDetailConfig = true;
					btnDetailConfig.Enabled = true;
					PCSMessageBox.Show(ErrorCode.MESSAGE_TABLEMANAGEMENT_AFTER_ADDTABLE);
					txtTableCode.Focus();

					//this.Close();
					return true;
				}
				// if form showed to Edit row in sys_Table
				else if(mEnumType == EnumAction.Edit)
				{
					// call function Update() method				
					//sys_TableVO voSysTable = new sys_TableVO();
					voSysTable.TableID = intTableID;
					voSysTable.Code = txtTableCode.Text.Trim();
					voSysTable.TableName = txtTableName.Text.Trim();
					voSysTable.TableOrView = cboTableView.Text.Trim();
					voSysTable.Height = int.Parse(txtHeight.Text.Trim());
					voSysTable.IsViewOnly = chkIsViewOnly.Checked;
					
					TableConfigBO boTableConfig = new TableConfigBO();
					boTableConfig.UpdateTable(voSysTable);
					blnEnableEditDetailConfig = true;
					btnDetailConfig.Enabled = true;
					PCSMessageBox.Show(ErrorCode.MESSAGE_TABLEMANAGEMENT_AFTER_EDITTABLE);
					txtTableCode.Focus();
					//this.Close();
					return true;
				}
				// if form showed to Copy row in sys_Table
				else if(mEnumType == EnumAction.Copy)
				{
					// call function Add() method				
					sys_TableVO voSysTable = new sys_TableVO();
					voSysTable.Code =  txtTableCode.Text.Trim();
					voSysTable.TableName = txtTableName.Text.Trim();
					voSysTable.TableOrView = cboTableView.Text.Trim();
					voSysTable.Height = int.Parse(txtHeight.Text.Trim());
					
					TableConfigBO boTableConfig = new TableConfigBO();
					boTableConfig.AddTable(voSysTable,mGroupID);
					txtTableCode.Focus();
					return true;
				}
				
			}
			catch (PCSException ex)
			{
				// displays the error message.
				if (ex.mCode != ErrorCode.DUPLICATE_KEY && ex.mCode != ErrorCode.DUPLICATEKEY)
				{
					PCSMessageBox.Show(ex.mCode,MessageBoxIcon.Error);
				}
				else
				{
					PCSMessageBox.Show(ErrorCode.DUPLICATE_TABLECONFIG, MessageBoxIcon.Error);
				}
				// log message.
				try
				{
					Logger.LogMessage(ex.CauseException, METHOD_NAME, Level.ERROR);
				}
				catch
				{
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION,MessageBoxIcon.Error);
				}
				return false;
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
				return false;
			}		
			txtTableCode.Focus();
			return true;
		}

		//**************************************************************************              
		///    <Description>
		///       present detail config form
		///    </Description>
		///    <Inputs>
		///               
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
		///       06-Dec-2004
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private void btnDetailConfig_Click(object sender, System.EventArgs e)
		{
			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.WaitCursor;

			#endregion Code Inserted Automatically

			
			const string METHOD_NAME = THIS + ".btnDetailConfig_Click()";			
			try
			{
				// Show TableConfigDetail form
				TableConfigDetail frmTableConfigDetail = new TableConfigDetail();
				frmTableConfigDetail.TableID = intTableID;
				
				//OLDLINE: frmTableConfigDetail.EnumType = EnumAction.Edit;

				/// HACKED: Thachnn: Change the EnumAction of TableConfigDetail
				if(this.EnumType == EnumAction.Edit)
				{
					frmTableConfigDetail.EnumType = EnumAction.Edit;
				}
				else if(this.EnumType == EnumAction.Add)
				{
					frmTableConfigDetail.EnumType = EnumAction.Add;
				}
				/// ENDHACKED: Thachnn

				frmTableConfigDetail.TableCode = voSysTable.Code.Trim();
				frmTableConfigDetail.TableName = voSysTable.TableName.Trim();
				frmTableConfigDetail.TableOrView = voSysTable.TableOrView.Trim();
				frmTableConfigDetail.ShowDialog();
				/*
				if(blnSavingStatus == false)
				{
					PCSMessageBox.Show(ErrorCode.SAVE_BEFORE_OPEN_TABLE_CONFIG_DETAIL);
					btnSave.Focus();
				}
				else
				{
					frmTableConfigDetail.Show();
				}
				*/
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
		///       Show help information
		///    </Description>
		///    <Inputs>
		///               
		///    </Inputs>
		///    <Outputs>
		///       Help File
		///    </Outputs>
		///    <Returns>
		///       void 
		///    </Returns>
		///    <Authors>
		///       NgocHT
		///    </Authors>
		///    <History>
		///       29-Dec-2004
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private void btnHelp_Click(object sender, System.EventArgs e)
		{// Code Inserted Automatically
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
		///       Close form
		///    </Description>
		///    <Inputs>
		///               
		///    </Inputs>
		///    <Outputs>
		///       
		///    </Outputs>
		///    <Returns>
		///      void 
		///    </Returns>
		///    <Authors>
		///       NgocHT
		///    </Authors>
		///    <History>
		///       29-Dec-2004
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
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


		//**************************************************************************              
		///    <Description>
		///       check numberic
		///    </Description>
		///    <Inputs>
		///               
		///    </Inputs>
		///    <Outputs>
		///       
		///    </Outputs>
		///    <Returns>
		///      void 
		///    </Returns>
		///    <Authors>
		///       NgocHT
		///    </Authors>
		///    <History>
		///       29-Dec-2004
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private bool ValidateNumeric(TextBox txtTextBox)
		{
			if (!FormControlComponents.IsNumeric(txtTextBox.Text))
			{
				return false;
			}
			return true;
		}

		//**************************************************************************              
		///    <Description>
		///       Display data in combo box
		///    </Description>
		///    <Inputs>
		///               
		///    </Inputs>
		///    <Outputs>
		///       
		///    </Outputs>
		///    <Returns>
		///      void 
		///    </Returns>
		///    <Authors>
		///       NgocHT
		///    </Authors>
		///    <History>
		///       29-Dec-2004
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private void DisplayValueInComboBox (ComboBox cboCombo , string strValue) 
		{
			for (int i=0; i<cboCombo.Items.Count; i++) 
			{
				if (((DataRowView)cboCombo.Items[i])[SchemaTableTable.TABLENAME_FLD].ToString().Trim().ToUpper() == strValue.Trim().ToUpper()) 
				{
					cboCombo.SelectedIndex = i;
					return;
					//break;
				}
			}
			
		}
		
		//**************************************************************************              
		///    <Description>
		///       this method to check data in three texboxes and combo if they are empty
		///    </Description>
		///    <Inputs>
		///               
		///    </Inputs>
		///    <Outputs>
		///       
		///    </Outputs>
		///    <Returns>
		///      void 
		///    </Returns>
		///    <Authors>
		///       NgocHT
		///    </Authors>
		///    <History>
		///       29-Dec-2004
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private bool CheckMandatory(Control pobjControl)
		{
			if (pobjControl.Text.Trim() == string.Empty)
			{
				return false;
			}
			return true;
		}

		private void cboTableView_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			blnSavingStatus = false;
			txtTableCode.Text = cboTableView.Text.Trim();
			txtTableName.Text = cboTableView.Text.Trim();
			blnEnableEditDetailConfig = false;
			btnDetailConfig.Enabled = false;
			if (cboTableView.SelectedValue.ToString() == VIEW)
			{
				chkIsViewOnly.Checked = true;
				chkIsViewOnly.Enabled = false;
			}
			else
			{
				chkIsViewOnly.Enabled = true;
				chkIsViewOnly.Checked = false;
			}
		}

		private void cboTableView_TextChanged(object sender, System.EventArgs e)
		{
			
		}

		private void txtTableName_TextChanged(object sender, System.EventArgs e)
		{
			blnEnableEditDetailConfig = false;
			btnDetailConfig.Enabled = false;
			blnSavingStatus = false;
		}

		private void txtTableCode_TextChanged(object sender, System.EventArgs e)
		{
			blnEnableEditDetailConfig = false;
			btnDetailConfig.Enabled = false;
			blnSavingStatus = false;
		
		}

		private void txtHeight_TextChanged(object sender, System.EventArgs e)
		{
		
		}

		private void TableConfig_Activated(object sender, System.EventArgs e)
		{
			switch(mEnumType)
			{
				case EnumAction.Add:
					cboTableView.Focus();
					break;
				case EnumAction.Edit:
					txtTableCode.Focus();
					break;
			}
		}

		private void TableConfig_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if (!blnSavingStatus)
			{
				System.Windows.Forms.DialogResult dlgResult = PCSMessageBox.Show(ErrorCode.MESSAGE_QUESTION_STORE_INTO_DATABASE, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
				switch (dlgResult)
				{
					case DialogResult.Yes:
						if (!SaveGroup())
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

		private void btnSave_Click(object sender, System.EventArgs e)
		{
			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.WaitCursor;

			#endregion Code Inserted Automatically

			
			if (SaveGroup())
			{
				blnSavingStatus = true;
			}

			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.Default;

			#endregion Code Inserted Automatically

			
		}

		private void txtHeight_TextChanged_1(object sender, System.EventArgs e)
		{
			blnSavingStatus = false;
		}

		private void OnLeaveControl(object sender, System.EventArgs e)
		{		
			const string METHOD_NAME = THIS + ".txtCode_Leave()";
			//Only use when users use this to search for existing product
			try 
			{
				FormControlComponents.OnLeaveControl(sender, e);				
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
	}
}

