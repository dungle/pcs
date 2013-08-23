using System;
//using System.Windows.Forms;
using System.Collections;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using PCSComUtils.Framework.TableFrame.DS;
using PCSComUtils.Framework.TableFrame.BO;
using PCSComUtils.Common;
using PCSComUtils.PCSExc;
using PCSUtils.Log;
using PCSUtils.Utils;


namespace PCSUtils.Framework.TableFrame
{
	/// <summary>
	/// Summary description for frmTableConfigDetail.
	/// </summary>
	public class TableConfigDetail : System.Windows.Forms.Form
	{
		private const string THIS = "PCSUtils.Framework.TableFrame.TableConfigDetail";
		private const int INT_NORMAL = 0;
		private const string STR_NORMAL = "Normal";
		private const int INT_UPPER = 1;
		private const string STR_UPPER = "To Upper";
		private const int INT_LOWER = 2;
		private const string STR_LOWER = "To Lower";

		private const int INT_LEFT = 0;
		private const string STR_LEFT = "Left";
		private const int INT_CENTER = 1;
		private const string STR_CENTER = "Center";
		private const int INT_RIGHT = 2;
		private const string STR_RIGHT = "Right";

		private const int INT_NONE = 0;
		private const string STR_NONE = "None";
		private const int INT_ASCENDING = 1;
		private const string STR_ASCENDING = "Ascending";
		private const int INT_DESCENDING = 2;
		private const string STR_DESCENDING = "Descending";
		private int intOldValue = 0;
		const string COLUMN_NAME = "COLUMN_NAME";
		const string DATA_TYPE = "DATA_TYPE";
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
		private EnumAction mEnumType;
		private int mTableID;
		private int mTableFieldID;
		private string mTableCode;
		private string mTableName;
		private string mTableOrView;
		private sys_TableFieldVO voTableField = new sys_TableFieldVO();
		private TableConfigDetailBO boTableConfigDetail = new TableConfigDetailBO();
		DataSet dstInformationSchema = new DataSet();
		DataSet dstInformationSchemaRelatedTable = new DataSet();
		private System.Windows.Forms.ComboBox cboFieldName;
		private System.Windows.Forms.ComboBox cboSortType;
		private System.Windows.Forms.TextBox txtItems;
		private System.Windows.Forms.CheckBox chkIdentity;
		private System.Windows.Forms.CheckBox chkUnique;
		private System.Windows.Forms.CheckBox chkNotAllowNull;
		private System.Windows.Forms.ComboBox cboAlign;
		private System.Windows.Forms.ComboBox cboFilterField2;
		private System.Windows.Forms.ComboBox cboFilterField1;
		private System.Windows.Forms.TextBox txtWidth;
		private System.Windows.Forms.ComboBox cboCase;
		private System.Windows.Forms.TextBox txtEnglish;
		private System.Windows.Forms.TextBox txtJapaness;
		private System.Windows.Forms.Button btnClose;
		private System.Windows.Forms.Button btnAdd;
		private System.Windows.Forms.Button btnDown;
		private System.Windows.Forms.Button btnUp;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.Button bntHelp;
		private System.Windows.Forms.Button btnSave;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.TextBox txtVietnamese;
		private System.Windows.Forms.TextBox txtFormat;
		private System.Windows.Forms.ListBox lstField;
		private System.Windows.Forms.Button btnDelete;
		private System.Windows.Forms.Label label15;
		private System.Windows.Forms.ComboBox cboFromTable;
		private System.Windows.Forms.ComboBox cboFromField;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.GroupBox grbExternalField;
		private System.Windows.Forms.Label lblFilter1;
		private System.Windows.Forms.Label lblEnglish1;
		private System.Windows.Forms.Label lblJapanese1;
		private System.Windows.Forms.Label lblVietnamese1;
		private System.Windows.Forms.TextBox txtVietnamese1;
		private System.Windows.Forms.TextBox txtEnglish1;
		private System.Windows.Forms.Label lblWidth1;
		private System.Windows.Forms.Label lblFormat1;
		private System.Windows.Forms.Label lblAlign1;
		private System.Windows.Forms.TextBox txtFormat1;
		private System.Windows.Forms.ComboBox cboAlign1;
		private System.Windows.Forms.Label lblFilter2;
		private System.Windows.Forms.Label lblEnglish2;
		private System.Windows.Forms.TextBox txtEnglish2;
		private System.Windows.Forms.Label lblJapanese2;
		private System.Windows.Forms.Label lblVietnamese2;
		private System.Windows.Forms.TextBox txtVietnamese2;
		private System.Windows.Forms.TextBox txtJapanese2;
		private System.Windows.Forms.Label lblAlign2;
		private System.Windows.Forms.Label lblWidth2;
		private System.Windows.Forms.Label lblFormat2;
		private System.Windows.Forms.TextBox txtFormat2;
		private System.Windows.Forms.ComboBox cboAlign2;
		private System.Windows.Forms.Label lblFilter3;
		private System.Windows.Forms.ComboBox cboFilterField3;
		private System.Windows.Forms.Label lblFormat3;
		private System.Windows.Forms.Label lblAlign3;
		private System.Windows.Forms.Label lblWidth3;
		private System.Windows.Forms.Label lblEnglish3;
		private System.Windows.Forms.Label lblJapanese3;
		private System.Windows.Forms.Label lblVietnamese3;
		private System.Windows.Forms.TextBox txtVietnamese3;
		private System.Windows.Forms.TextBox txtEnglish3;
		private System.Windows.Forms.TextBox txtFormat3;
		private System.Windows.Forms.TextBox txtJapanese3;
		private System.Windows.Forms.ComboBox cboAlign3;
		private System.Windows.Forms.TextBox txtJapanese1;
		private C1.Win.C1Input.C1NumericEdit txtWidth1;
		private C1.Win.C1Input.C1NumericEdit txtWidth2;
		private C1.Win.C1Input.C1NumericEdit txtWidth3;
		private System.Windows.Forms.Label lblLinkField;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public TableConfigDetail()
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
		public int TableID
		{
			get
			{
				return mTableID;
			}
			set
			{
				mTableID = value;
			}
		}	
		public int TableFieldID
		{
			get
			{
				return mTableFieldID;
			}
			set
			{
				mTableFieldID = value;
			}
		}	
		
		public string TableName
		{
			get
			{
				return mTableName;
			}
			set
			{
				mTableName = value;
			}
		}	

		public string TableOrView
		{
			get
			{
				return mTableOrView;
			}
			set
			{
				mTableOrView = value;
			}
		}	

		public string TableCode
		{
			get
			{
				return mTableCode;
			}
			set
			{
				mTableCode = value;
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(TableConfigDetail));
			this.cboFieldName = new System.Windows.Forms.ComboBox();
			this.cboSortType = new System.Windows.Forms.ComboBox();
			this.txtItems = new System.Windows.Forms.TextBox();
			this.txtFormat = new System.Windows.Forms.TextBox();
			this.chkIdentity = new System.Windows.Forms.CheckBox();
			this.chkUnique = new System.Windows.Forms.CheckBox();
			this.chkNotAllowNull = new System.Windows.Forms.CheckBox();
			this.cboAlign = new System.Windows.Forms.ComboBox();
			this.cboFilterField2 = new System.Windows.Forms.ComboBox();
			this.cboFilterField1 = new System.Windows.Forms.ComboBox();
			this.txtWidth = new System.Windows.Forms.TextBox();
			this.cboCase = new System.Windows.Forms.ComboBox();
			this.txtEnglish = new System.Windows.Forms.TextBox();
			this.txtJapaness = new System.Windows.Forms.TextBox();
			this.btnClose = new System.Windows.Forms.Button();
			this.btnAdd = new System.Windows.Forms.Button();
			this.btnDown = new System.Windows.Forms.Button();
			this.btnUp = new System.Windows.Forms.Button();
			this.label12 = new System.Windows.Forms.Label();
			this.bntHelp = new System.Windows.Forms.Button();
			this.btnSave = new System.Windows.Forms.Button();
			this.btnDelete = new System.Windows.Forms.Button();
			this.label11 = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.lblFilter2 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.lblFilter1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.label13 = new System.Windows.Forms.Label();
			this.label14 = new System.Windows.Forms.Label();
			this.txtVietnamese = new System.Windows.Forms.TextBox();
			this.lstField = new System.Windows.Forms.ListBox();
			this.lblFilter3 = new System.Windows.Forms.Label();
			this.cboFilterField3 = new System.Windows.Forms.ComboBox();
			this.grbExternalField = new System.Windows.Forms.GroupBox();
			this.lblFormat3 = new System.Windows.Forms.Label();
			this.lblAlign3 = new System.Windows.Forms.Label();
			this.lblWidth3 = new System.Windows.Forms.Label();
			this.lblEnglish3 = new System.Windows.Forms.Label();
			this.lblJapanese3 = new System.Windows.Forms.Label();
			this.lblVietnamese3 = new System.Windows.Forms.Label();
			this.txtVietnamese3 = new System.Windows.Forms.TextBox();
			this.txtEnglish3 = new System.Windows.Forms.TextBox();
			this.txtFormat3 = new System.Windows.Forms.TextBox();
			this.txtJapanese3 = new System.Windows.Forms.TextBox();
			this.cboAlign3 = new System.Windows.Forms.ComboBox();
			this.lblFormat2 = new System.Windows.Forms.Label();
			this.lblAlign2 = new System.Windows.Forms.Label();
			this.lblWidth2 = new System.Windows.Forms.Label();
			this.lblEnglish2 = new System.Windows.Forms.Label();
			this.lblJapanese2 = new System.Windows.Forms.Label();
			this.lblVietnamese2 = new System.Windows.Forms.Label();
			this.txtVietnamese2 = new System.Windows.Forms.TextBox();
			this.txtEnglish2 = new System.Windows.Forms.TextBox();
			this.txtFormat2 = new System.Windows.Forms.TextBox();
			this.txtJapanese2 = new System.Windows.Forms.TextBox();
			this.cboAlign2 = new System.Windows.Forms.ComboBox();
			this.lblFormat1 = new System.Windows.Forms.Label();
			this.lblAlign1 = new System.Windows.Forms.Label();
			this.lblWidth1 = new System.Windows.Forms.Label();
			this.lblEnglish1 = new System.Windows.Forms.Label();
			this.lblJapanese1 = new System.Windows.Forms.Label();
			this.lblVietnamese1 = new System.Windows.Forms.Label();
			this.txtVietnamese1 = new System.Windows.Forms.TextBox();
			this.txtEnglish1 = new System.Windows.Forms.TextBox();
			this.txtFormat1 = new System.Windows.Forms.TextBox();
			this.txtJapanese1 = new System.Windows.Forms.TextBox();
			this.cboAlign1 = new System.Windows.Forms.ComboBox();
			this.txtWidth1 = new C1.Win.C1Input.C1NumericEdit();
			this.txtWidth2 = new C1.Win.C1Input.C1NumericEdit();
			this.txtWidth3 = new C1.Win.C1Input.C1NumericEdit();
			this.label15 = new System.Windows.Forms.Label();
			this.cboFromTable = new System.Windows.Forms.ComboBox();
			this.cboFromField = new System.Windows.Forms.ComboBox();
			this.label5 = new System.Windows.Forms.Label();
			this.lblLinkField = new System.Windows.Forms.Label();
			this.grbExternalField.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.txtWidth1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.txtWidth2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.txtWidth3)).BeginInit();
			this.SuspendLayout();
			// 
			// cboFieldName
			// 
			this.cboFieldName.AccessibleDescription = resources.GetString("cboFieldName.AccessibleDescription");
			this.cboFieldName.AccessibleName = resources.GetString("cboFieldName.AccessibleName");
			this.cboFieldName.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("cboFieldName.Anchor")));
			this.cboFieldName.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cboFieldName.BackgroundImage")));
			this.cboFieldName.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("cboFieldName.Dock")));
			this.cboFieldName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboFieldName.Enabled = ((bool)(resources.GetObject("cboFieldName.Enabled")));
			this.cboFieldName.Font = ((System.Drawing.Font)(resources.GetObject("cboFieldName.Font")));
			this.cboFieldName.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("cboFieldName.ImeMode")));
			this.cboFieldName.IntegralHeight = ((bool)(resources.GetObject("cboFieldName.IntegralHeight")));
			this.cboFieldName.ItemHeight = ((int)(resources.GetObject("cboFieldName.ItemHeight")));
			this.cboFieldName.Location = ((System.Drawing.Point)(resources.GetObject("cboFieldName.Location")));
			this.cboFieldName.MaxDropDownItems = ((int)(resources.GetObject("cboFieldName.MaxDropDownItems")));
			this.cboFieldName.MaxLength = ((int)(resources.GetObject("cboFieldName.MaxLength")));
			this.cboFieldName.Name = "cboFieldName";
			this.cboFieldName.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("cboFieldName.RightToLeft")));
			this.cboFieldName.Size = ((System.Drawing.Size)(resources.GetObject("cboFieldName.Size")));
			this.cboFieldName.TabIndex = ((int)(resources.GetObject("cboFieldName.TabIndex")));
			this.cboFieldName.Text = resources.GetString("cboFieldName.Text");
			this.cboFieldName.Visible = ((bool)(resources.GetObject("cboFieldName.Visible")));
			this.cboFieldName.SelectedIndexChanged += new System.EventHandler(this.cboFieldName_SelectedIndexChanged);
			this.cboFieldName.Leave += new System.EventHandler(this.OnControlLeave);
			this.cboFieldName.Enter += new System.EventHandler(this.OnControlEnter);
			// 
			// cboSortType
			// 
			this.cboSortType.AccessibleDescription = resources.GetString("cboSortType.AccessibleDescription");
			this.cboSortType.AccessibleName = resources.GetString("cboSortType.AccessibleName");
			this.cboSortType.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("cboSortType.Anchor")));
			this.cboSortType.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cboSortType.BackgroundImage")));
			this.cboSortType.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("cboSortType.Dock")));
			this.cboSortType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboSortType.Enabled = ((bool)(resources.GetObject("cboSortType.Enabled")));
			this.cboSortType.Font = ((System.Drawing.Font)(resources.GetObject("cboSortType.Font")));
			this.cboSortType.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("cboSortType.ImeMode")));
			this.cboSortType.IntegralHeight = ((bool)(resources.GetObject("cboSortType.IntegralHeight")));
			this.cboSortType.ItemHeight = ((int)(resources.GetObject("cboSortType.ItemHeight")));
			this.cboSortType.Items.AddRange(new object[] {
															 resources.GetString("cboSortType.Items"),
															 resources.GetString("cboSortType.Items1"),
															 resources.GetString("cboSortType.Items2")});
			this.cboSortType.Location = ((System.Drawing.Point)(resources.GetObject("cboSortType.Location")));
			this.cboSortType.MaxDropDownItems = ((int)(resources.GetObject("cboSortType.MaxDropDownItems")));
			this.cboSortType.MaxLength = ((int)(resources.GetObject("cboSortType.MaxLength")));
			this.cboSortType.Name = "cboSortType";
			this.cboSortType.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("cboSortType.RightToLeft")));
			this.cboSortType.Size = ((System.Drawing.Size)(resources.GetObject("cboSortType.Size")));
			this.cboSortType.TabIndex = ((int)(resources.GetObject("cboSortType.TabIndex")));
			this.cboSortType.Text = resources.GetString("cboSortType.Text");
			this.cboSortType.Visible = ((bool)(resources.GetObject("cboSortType.Visible")));
			this.cboSortType.Leave += new System.EventHandler(this.OnControlLeave);
			this.cboSortType.Enter += new System.EventHandler(this.OnControlEnter);
			// 
			// txtItems
			// 
			this.txtItems.AccessibleDescription = resources.GetString("txtItems.AccessibleDescription");
			this.txtItems.AccessibleName = resources.GetString("txtItems.AccessibleName");
			this.txtItems.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("txtItems.Anchor")));
			this.txtItems.AutoSize = ((bool)(resources.GetObject("txtItems.AutoSize")));
			this.txtItems.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("txtItems.BackgroundImage")));
			this.txtItems.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("txtItems.Dock")));
			this.txtItems.Enabled = ((bool)(resources.GetObject("txtItems.Enabled")));
			this.txtItems.Font = ((System.Drawing.Font)(resources.GetObject("txtItems.Font")));
			this.txtItems.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("txtItems.ImeMode")));
			this.txtItems.Location = ((System.Drawing.Point)(resources.GetObject("txtItems.Location")));
			this.txtItems.MaxLength = ((int)(resources.GetObject("txtItems.MaxLength")));
			this.txtItems.Multiline = ((bool)(resources.GetObject("txtItems.Multiline")));
			this.txtItems.Name = "txtItems";
			this.txtItems.PasswordChar = ((char)(resources.GetObject("txtItems.PasswordChar")));
			this.txtItems.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("txtItems.RightToLeft")));
			this.txtItems.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("txtItems.ScrollBars")));
			this.txtItems.Size = ((System.Drawing.Size)(resources.GetObject("txtItems.Size")));
			this.txtItems.TabIndex = ((int)(resources.GetObject("txtItems.TabIndex")));
			this.txtItems.Text = resources.GetString("txtItems.Text");
			this.txtItems.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("txtItems.TextAlign")));
			this.txtItems.Visible = ((bool)(resources.GetObject("txtItems.Visible")));
			this.txtItems.WordWrap = ((bool)(resources.GetObject("txtItems.WordWrap")));
			this.txtItems.Leave += new System.EventHandler(this.OnControlLeave);
			this.txtItems.Enter += new System.EventHandler(this.OnControlEnter);
			// 
			// txtFormat
			// 
			this.txtFormat.AccessibleDescription = resources.GetString("txtFormat.AccessibleDescription");
			this.txtFormat.AccessibleName = resources.GetString("txtFormat.AccessibleName");
			this.txtFormat.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("txtFormat.Anchor")));
			this.txtFormat.AutoSize = ((bool)(resources.GetObject("txtFormat.AutoSize")));
			this.txtFormat.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("txtFormat.BackgroundImage")));
			this.txtFormat.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("txtFormat.Dock")));
			this.txtFormat.Enabled = ((bool)(resources.GetObject("txtFormat.Enabled")));
			this.txtFormat.Font = ((System.Drawing.Font)(resources.GetObject("txtFormat.Font")));
			this.txtFormat.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("txtFormat.ImeMode")));
			this.txtFormat.Location = ((System.Drawing.Point)(resources.GetObject("txtFormat.Location")));
			this.txtFormat.MaxLength = ((int)(resources.GetObject("txtFormat.MaxLength")));
			this.txtFormat.Multiline = ((bool)(resources.GetObject("txtFormat.Multiline")));
			this.txtFormat.Name = "txtFormat";
			this.txtFormat.PasswordChar = ((char)(resources.GetObject("txtFormat.PasswordChar")));
			this.txtFormat.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("txtFormat.RightToLeft")));
			this.txtFormat.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("txtFormat.ScrollBars")));
			this.txtFormat.Size = ((System.Drawing.Size)(resources.GetObject("txtFormat.Size")));
			this.txtFormat.TabIndex = ((int)(resources.GetObject("txtFormat.TabIndex")));
			this.txtFormat.Text = resources.GetString("txtFormat.Text");
			this.txtFormat.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("txtFormat.TextAlign")));
			this.txtFormat.Visible = ((bool)(resources.GetObject("txtFormat.Visible")));
			this.txtFormat.WordWrap = ((bool)(resources.GetObject("txtFormat.WordWrap")));
			this.txtFormat.Leave += new System.EventHandler(this.OnControlLeave);
			this.txtFormat.Enter += new System.EventHandler(this.OnControlEnter);
			// 
			// chkIdentity
			// 
			this.chkIdentity.AccessibleDescription = resources.GetString("chkIdentity.AccessibleDescription");
			this.chkIdentity.AccessibleName = resources.GetString("chkIdentity.AccessibleName");
			this.chkIdentity.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("chkIdentity.Anchor")));
			this.chkIdentity.Appearance = ((System.Windows.Forms.Appearance)(resources.GetObject("chkIdentity.Appearance")));
			this.chkIdentity.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("chkIdentity.BackgroundImage")));
			this.chkIdentity.CheckAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("chkIdentity.CheckAlign")));
			this.chkIdentity.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("chkIdentity.Dock")));
			this.chkIdentity.Enabled = ((bool)(resources.GetObject("chkIdentity.Enabled")));
			this.chkIdentity.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("chkIdentity.FlatStyle")));
			this.chkIdentity.Font = ((System.Drawing.Font)(resources.GetObject("chkIdentity.Font")));
			this.chkIdentity.Image = ((System.Drawing.Image)(resources.GetObject("chkIdentity.Image")));
			this.chkIdentity.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("chkIdentity.ImageAlign")));
			this.chkIdentity.ImageIndex = ((int)(resources.GetObject("chkIdentity.ImageIndex")));
			this.chkIdentity.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("chkIdentity.ImeMode")));
			this.chkIdentity.Location = ((System.Drawing.Point)(resources.GetObject("chkIdentity.Location")));
			this.chkIdentity.Name = "chkIdentity";
			this.chkIdentity.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("chkIdentity.RightToLeft")));
			this.chkIdentity.Size = ((System.Drawing.Size)(resources.GetObject("chkIdentity.Size")));
			this.chkIdentity.TabIndex = ((int)(resources.GetObject("chkIdentity.TabIndex")));
			this.chkIdentity.Text = resources.GetString("chkIdentity.Text");
			this.chkIdentity.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("chkIdentity.TextAlign")));
			this.chkIdentity.Visible = ((bool)(resources.GetObject("chkIdentity.Visible")));
			// 
			// chkUnique
			// 
			this.chkUnique.AccessibleDescription = resources.GetString("chkUnique.AccessibleDescription");
			this.chkUnique.AccessibleName = resources.GetString("chkUnique.AccessibleName");
			this.chkUnique.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("chkUnique.Anchor")));
			this.chkUnique.Appearance = ((System.Windows.Forms.Appearance)(resources.GetObject("chkUnique.Appearance")));
			this.chkUnique.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("chkUnique.BackgroundImage")));
			this.chkUnique.CheckAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("chkUnique.CheckAlign")));
			this.chkUnique.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("chkUnique.Dock")));
			this.chkUnique.Enabled = ((bool)(resources.GetObject("chkUnique.Enabled")));
			this.chkUnique.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("chkUnique.FlatStyle")));
			this.chkUnique.Font = ((System.Drawing.Font)(resources.GetObject("chkUnique.Font")));
			this.chkUnique.Image = ((System.Drawing.Image)(resources.GetObject("chkUnique.Image")));
			this.chkUnique.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("chkUnique.ImageAlign")));
			this.chkUnique.ImageIndex = ((int)(resources.GetObject("chkUnique.ImageIndex")));
			this.chkUnique.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("chkUnique.ImeMode")));
			this.chkUnique.Location = ((System.Drawing.Point)(resources.GetObject("chkUnique.Location")));
			this.chkUnique.Name = "chkUnique";
			this.chkUnique.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("chkUnique.RightToLeft")));
			this.chkUnique.Size = ((System.Drawing.Size)(resources.GetObject("chkUnique.Size")));
			this.chkUnique.TabIndex = ((int)(resources.GetObject("chkUnique.TabIndex")));
			this.chkUnique.Text = resources.GetString("chkUnique.Text");
			this.chkUnique.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("chkUnique.TextAlign")));
			this.chkUnique.Visible = ((bool)(resources.GetObject("chkUnique.Visible")));
			// 
			// chkNotAllowNull
			// 
			this.chkNotAllowNull.AccessibleDescription = resources.GetString("chkNotAllowNull.AccessibleDescription");
			this.chkNotAllowNull.AccessibleName = resources.GetString("chkNotAllowNull.AccessibleName");
			this.chkNotAllowNull.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("chkNotAllowNull.Anchor")));
			this.chkNotAllowNull.Appearance = ((System.Windows.Forms.Appearance)(resources.GetObject("chkNotAllowNull.Appearance")));
			this.chkNotAllowNull.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("chkNotAllowNull.BackgroundImage")));
			this.chkNotAllowNull.CheckAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("chkNotAllowNull.CheckAlign")));
			this.chkNotAllowNull.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("chkNotAllowNull.Dock")));
			this.chkNotAllowNull.Enabled = ((bool)(resources.GetObject("chkNotAllowNull.Enabled")));
			this.chkNotAllowNull.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("chkNotAllowNull.FlatStyle")));
			this.chkNotAllowNull.Font = ((System.Drawing.Font)(resources.GetObject("chkNotAllowNull.Font")));
			this.chkNotAllowNull.Image = ((System.Drawing.Image)(resources.GetObject("chkNotAllowNull.Image")));
			this.chkNotAllowNull.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("chkNotAllowNull.ImageAlign")));
			this.chkNotAllowNull.ImageIndex = ((int)(resources.GetObject("chkNotAllowNull.ImageIndex")));
			this.chkNotAllowNull.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("chkNotAllowNull.ImeMode")));
			this.chkNotAllowNull.Location = ((System.Drawing.Point)(resources.GetObject("chkNotAllowNull.Location")));
			this.chkNotAllowNull.Name = "chkNotAllowNull";
			this.chkNotAllowNull.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("chkNotAllowNull.RightToLeft")));
			this.chkNotAllowNull.Size = ((System.Drawing.Size)(resources.GetObject("chkNotAllowNull.Size")));
			this.chkNotAllowNull.TabIndex = ((int)(resources.GetObject("chkNotAllowNull.TabIndex")));
			this.chkNotAllowNull.Text = resources.GetString("chkNotAllowNull.Text");
			this.chkNotAllowNull.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("chkNotAllowNull.TextAlign")));
			this.chkNotAllowNull.Visible = ((bool)(resources.GetObject("chkNotAllowNull.Visible")));
			// 
			// cboAlign
			// 
			this.cboAlign.AccessibleDescription = resources.GetString("cboAlign.AccessibleDescription");
			this.cboAlign.AccessibleName = resources.GetString("cboAlign.AccessibleName");
			this.cboAlign.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("cboAlign.Anchor")));
			this.cboAlign.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cboAlign.BackgroundImage")));
			this.cboAlign.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("cboAlign.Dock")));
			this.cboAlign.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboAlign.Enabled = ((bool)(resources.GetObject("cboAlign.Enabled")));
			this.cboAlign.Font = ((System.Drawing.Font)(resources.GetObject("cboAlign.Font")));
			this.cboAlign.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("cboAlign.ImeMode")));
			this.cboAlign.IntegralHeight = ((bool)(resources.GetObject("cboAlign.IntegralHeight")));
			this.cboAlign.ItemHeight = ((int)(resources.GetObject("cboAlign.ItemHeight")));
			this.cboAlign.Items.AddRange(new object[] {
														  resources.GetString("cboAlign.Items"),
														  resources.GetString("cboAlign.Items1"),
														  resources.GetString("cboAlign.Items2")});
			this.cboAlign.Location = ((System.Drawing.Point)(resources.GetObject("cboAlign.Location")));
			this.cboAlign.MaxDropDownItems = ((int)(resources.GetObject("cboAlign.MaxDropDownItems")));
			this.cboAlign.MaxLength = ((int)(resources.GetObject("cboAlign.MaxLength")));
			this.cboAlign.Name = "cboAlign";
			this.cboAlign.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("cboAlign.RightToLeft")));
			this.cboAlign.Size = ((System.Drawing.Size)(resources.GetObject("cboAlign.Size")));
			this.cboAlign.TabIndex = ((int)(resources.GetObject("cboAlign.TabIndex")));
			this.cboAlign.Text = resources.GetString("cboAlign.Text");
			this.cboAlign.Visible = ((bool)(resources.GetObject("cboAlign.Visible")));
			this.cboAlign.Leave += new System.EventHandler(this.OnControlLeave);
			this.cboAlign.Enter += new System.EventHandler(this.OnControlEnter);
			// 
			// cboFilterField2
			// 
			this.cboFilterField2.AccessibleDescription = resources.GetString("cboFilterField2.AccessibleDescription");
			this.cboFilterField2.AccessibleName = resources.GetString("cboFilterField2.AccessibleName");
			this.cboFilterField2.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("cboFilterField2.Anchor")));
			this.cboFilterField2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cboFilterField2.BackgroundImage")));
			this.cboFilterField2.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("cboFilterField2.Dock")));
			this.cboFilterField2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboFilterField2.Enabled = ((bool)(resources.GetObject("cboFilterField2.Enabled")));
			this.cboFilterField2.Font = ((System.Drawing.Font)(resources.GetObject("cboFilterField2.Font")));
			this.cboFilterField2.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("cboFilterField2.ImeMode")));
			this.cboFilterField2.IntegralHeight = ((bool)(resources.GetObject("cboFilterField2.IntegralHeight")));
			this.cboFilterField2.ItemHeight = ((int)(resources.GetObject("cboFilterField2.ItemHeight")));
			this.cboFilterField2.Location = ((System.Drawing.Point)(resources.GetObject("cboFilterField2.Location")));
			this.cboFilterField2.MaxDropDownItems = ((int)(resources.GetObject("cboFilterField2.MaxDropDownItems")));
			this.cboFilterField2.MaxLength = ((int)(resources.GetObject("cboFilterField2.MaxLength")));
			this.cboFilterField2.Name = "cboFilterField2";
			this.cboFilterField2.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("cboFilterField2.RightToLeft")));
			this.cboFilterField2.Size = ((System.Drawing.Size)(resources.GetObject("cboFilterField2.Size")));
			this.cboFilterField2.TabIndex = ((int)(resources.GetObject("cboFilterField2.TabIndex")));
			this.cboFilterField2.Text = resources.GetString("cboFilterField2.Text");
			this.cboFilterField2.Visible = ((bool)(resources.GetObject("cboFilterField2.Visible")));
			this.cboFilterField2.SelectedIndexChanged += new System.EventHandler(this.cboFilterField2_SelectedIndexChanged);
			this.cboFilterField2.Leave += new System.EventHandler(this.OnControlLeave);
			this.cboFilterField2.Enter += new System.EventHandler(this.OnControlEnter);
			// 
			// cboFilterField1
			// 
			this.cboFilterField1.AccessibleDescription = resources.GetString("cboFilterField1.AccessibleDescription");
			this.cboFilterField1.AccessibleName = resources.GetString("cboFilterField1.AccessibleName");
			this.cboFilterField1.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("cboFilterField1.Anchor")));
			this.cboFilterField1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cboFilterField1.BackgroundImage")));
			this.cboFilterField1.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("cboFilterField1.Dock")));
			this.cboFilterField1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboFilterField1.Enabled = ((bool)(resources.GetObject("cboFilterField1.Enabled")));
			this.cboFilterField1.Font = ((System.Drawing.Font)(resources.GetObject("cboFilterField1.Font")));
			this.cboFilterField1.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("cboFilterField1.ImeMode")));
			this.cboFilterField1.IntegralHeight = ((bool)(resources.GetObject("cboFilterField1.IntegralHeight")));
			this.cboFilterField1.ItemHeight = ((int)(resources.GetObject("cboFilterField1.ItemHeight")));
			this.cboFilterField1.Location = ((System.Drawing.Point)(resources.GetObject("cboFilterField1.Location")));
			this.cboFilterField1.MaxDropDownItems = ((int)(resources.GetObject("cboFilterField1.MaxDropDownItems")));
			this.cboFilterField1.MaxLength = ((int)(resources.GetObject("cboFilterField1.MaxLength")));
			this.cboFilterField1.Name = "cboFilterField1";
			this.cboFilterField1.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("cboFilterField1.RightToLeft")));
			this.cboFilterField1.Size = ((System.Drawing.Size)(resources.GetObject("cboFilterField1.Size")));
			this.cboFilterField1.TabIndex = ((int)(resources.GetObject("cboFilterField1.TabIndex")));
			this.cboFilterField1.Text = resources.GetString("cboFilterField1.Text");
			this.cboFilterField1.Visible = ((bool)(resources.GetObject("cboFilterField1.Visible")));
			this.cboFilterField1.SelectedIndexChanged += new System.EventHandler(this.cboFilterField1_SelectedIndexChanged);
			this.cboFilterField1.Leave += new System.EventHandler(this.OnControlLeave);
			this.cboFilterField1.Enter += new System.EventHandler(this.OnControlEnter);
			// 
			// txtWidth
			// 
			this.txtWidth.AccessibleDescription = resources.GetString("txtWidth.AccessibleDescription");
			this.txtWidth.AccessibleName = resources.GetString("txtWidth.AccessibleName");
			this.txtWidth.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("txtWidth.Anchor")));
			this.txtWidth.AutoSize = ((bool)(resources.GetObject("txtWidth.AutoSize")));
			this.txtWidth.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("txtWidth.BackgroundImage")));
			this.txtWidth.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("txtWidth.Dock")));
			this.txtWidth.Enabled = ((bool)(resources.GetObject("txtWidth.Enabled")));
			this.txtWidth.Font = ((System.Drawing.Font)(resources.GetObject("txtWidth.Font")));
			this.txtWidth.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("txtWidth.ImeMode")));
			this.txtWidth.Location = ((System.Drawing.Point)(resources.GetObject("txtWidth.Location")));
			this.txtWidth.MaxLength = ((int)(resources.GetObject("txtWidth.MaxLength")));
			this.txtWidth.Multiline = ((bool)(resources.GetObject("txtWidth.Multiline")));
			this.txtWidth.Name = "txtWidth";
			this.txtWidth.PasswordChar = ((char)(resources.GetObject("txtWidth.PasswordChar")));
			this.txtWidth.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("txtWidth.RightToLeft")));
			this.txtWidth.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("txtWidth.ScrollBars")));
			this.txtWidth.Size = ((System.Drawing.Size)(resources.GetObject("txtWidth.Size")));
			this.txtWidth.TabIndex = ((int)(resources.GetObject("txtWidth.TabIndex")));
			this.txtWidth.Text = resources.GetString("txtWidth.Text");
			this.txtWidth.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("txtWidth.TextAlign")));
			this.txtWidth.Visible = ((bool)(resources.GetObject("txtWidth.Visible")));
			this.txtWidth.WordWrap = ((bool)(resources.GetObject("txtWidth.WordWrap")));
			this.txtWidth.Leave += new System.EventHandler(this.OnControlLeave);
			this.txtWidth.Enter += new System.EventHandler(this.OnControlEnter);
			// 
			// cboCase
			// 
			this.cboCase.AccessibleDescription = resources.GetString("cboCase.AccessibleDescription");
			this.cboCase.AccessibleName = resources.GetString("cboCase.AccessibleName");
			this.cboCase.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("cboCase.Anchor")));
			this.cboCase.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cboCase.BackgroundImage")));
			this.cboCase.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("cboCase.Dock")));
			this.cboCase.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboCase.Enabled = ((bool)(resources.GetObject("cboCase.Enabled")));
			this.cboCase.Font = ((System.Drawing.Font)(resources.GetObject("cboCase.Font")));
			this.cboCase.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("cboCase.ImeMode")));
			this.cboCase.IntegralHeight = ((bool)(resources.GetObject("cboCase.IntegralHeight")));
			this.cboCase.ItemHeight = ((int)(resources.GetObject("cboCase.ItemHeight")));
			this.cboCase.Items.AddRange(new object[] {
														 resources.GetString("cboCase.Items"),
														 resources.GetString("cboCase.Items1"),
														 resources.GetString("cboCase.Items2")});
			this.cboCase.Location = ((System.Drawing.Point)(resources.GetObject("cboCase.Location")));
			this.cboCase.MaxDropDownItems = ((int)(resources.GetObject("cboCase.MaxDropDownItems")));
			this.cboCase.MaxLength = ((int)(resources.GetObject("cboCase.MaxLength")));
			this.cboCase.Name = "cboCase";
			this.cboCase.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("cboCase.RightToLeft")));
			this.cboCase.Size = ((System.Drawing.Size)(resources.GetObject("cboCase.Size")));
			this.cboCase.TabIndex = ((int)(resources.GetObject("cboCase.TabIndex")));
			this.cboCase.Text = resources.GetString("cboCase.Text");
			this.cboCase.Visible = ((bool)(resources.GetObject("cboCase.Visible")));
			this.cboCase.Leave += new System.EventHandler(this.OnControlLeave);
			this.cboCase.Enter += new System.EventHandler(this.OnControlEnter);
			// 
			// txtEnglish
			// 
			this.txtEnglish.AccessibleDescription = resources.GetString("txtEnglish.AccessibleDescription");
			this.txtEnglish.AccessibleName = resources.GetString("txtEnglish.AccessibleName");
			this.txtEnglish.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("txtEnglish.Anchor")));
			this.txtEnglish.AutoSize = ((bool)(resources.GetObject("txtEnglish.AutoSize")));
			this.txtEnglish.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("txtEnglish.BackgroundImage")));
			this.txtEnglish.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("txtEnglish.Dock")));
			this.txtEnglish.Enabled = ((bool)(resources.GetObject("txtEnglish.Enabled")));
			this.txtEnglish.Font = ((System.Drawing.Font)(resources.GetObject("txtEnglish.Font")));
			this.txtEnglish.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("txtEnglish.ImeMode")));
			this.txtEnglish.Location = ((System.Drawing.Point)(resources.GetObject("txtEnglish.Location")));
			this.txtEnglish.MaxLength = ((int)(resources.GetObject("txtEnglish.MaxLength")));
			this.txtEnglish.Multiline = ((bool)(resources.GetObject("txtEnglish.Multiline")));
			this.txtEnglish.Name = "txtEnglish";
			this.txtEnglish.PasswordChar = ((char)(resources.GetObject("txtEnglish.PasswordChar")));
			this.txtEnglish.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("txtEnglish.RightToLeft")));
			this.txtEnglish.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("txtEnglish.ScrollBars")));
			this.txtEnglish.Size = ((System.Drawing.Size)(resources.GetObject("txtEnglish.Size")));
			this.txtEnglish.TabIndex = ((int)(resources.GetObject("txtEnglish.TabIndex")));
			this.txtEnglish.Text = resources.GetString("txtEnglish.Text");
			this.txtEnglish.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("txtEnglish.TextAlign")));
			this.txtEnglish.Visible = ((bool)(resources.GetObject("txtEnglish.Visible")));
			this.txtEnglish.WordWrap = ((bool)(resources.GetObject("txtEnglish.WordWrap")));
			this.txtEnglish.Leave += new System.EventHandler(this.OnControlLeave);
			this.txtEnglish.Enter += new System.EventHandler(this.OnControlEnter);
			// 
			// txtJapaness
			// 
			this.txtJapaness.AccessibleDescription = resources.GetString("txtJapaness.AccessibleDescription");
			this.txtJapaness.AccessibleName = resources.GetString("txtJapaness.AccessibleName");
			this.txtJapaness.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("txtJapaness.Anchor")));
			this.txtJapaness.AutoSize = ((bool)(resources.GetObject("txtJapaness.AutoSize")));
			this.txtJapaness.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("txtJapaness.BackgroundImage")));
			this.txtJapaness.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("txtJapaness.Dock")));
			this.txtJapaness.Enabled = ((bool)(resources.GetObject("txtJapaness.Enabled")));
			this.txtJapaness.Font = ((System.Drawing.Font)(resources.GetObject("txtJapaness.Font")));
			this.txtJapaness.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("txtJapaness.ImeMode")));
			this.txtJapaness.Location = ((System.Drawing.Point)(resources.GetObject("txtJapaness.Location")));
			this.txtJapaness.MaxLength = ((int)(resources.GetObject("txtJapaness.MaxLength")));
			this.txtJapaness.Multiline = ((bool)(resources.GetObject("txtJapaness.Multiline")));
			this.txtJapaness.Name = "txtJapaness";
			this.txtJapaness.PasswordChar = ((char)(resources.GetObject("txtJapaness.PasswordChar")));
			this.txtJapaness.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("txtJapaness.RightToLeft")));
			this.txtJapaness.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("txtJapaness.ScrollBars")));
			this.txtJapaness.Size = ((System.Drawing.Size)(resources.GetObject("txtJapaness.Size")));
			this.txtJapaness.TabIndex = ((int)(resources.GetObject("txtJapaness.TabIndex")));
			this.txtJapaness.Text = resources.GetString("txtJapaness.Text");
			this.txtJapaness.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("txtJapaness.TextAlign")));
			this.txtJapaness.Visible = ((bool)(resources.GetObject("txtJapaness.Visible")));
			this.txtJapaness.WordWrap = ((bool)(resources.GetObject("txtJapaness.WordWrap")));
			this.txtJapaness.Leave += new System.EventHandler(this.OnControlLeave);
			this.txtJapaness.Enter += new System.EventHandler(this.OnControlEnter);
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
			// label12
			// 
			this.label12.AccessibleDescription = resources.GetString("label12.AccessibleDescription");
			this.label12.AccessibleName = resources.GetString("label12.AccessibleName");
			this.label12.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("label12.Anchor")));
			this.label12.AutoSize = ((bool)(resources.GetObject("label12.AutoSize")));
			this.label12.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("label12.Dock")));
			this.label12.Enabled = ((bool)(resources.GetObject("label12.Enabled")));
			this.label12.Font = ((System.Drawing.Font)(resources.GetObject("label12.Font")));
			this.label12.ForeColor = System.Drawing.Color.Maroon;
			this.label12.Image = ((System.Drawing.Image)(resources.GetObject("label12.Image")));
			this.label12.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label12.ImageAlign")));
			this.label12.ImageIndex = ((int)(resources.GetObject("label12.ImageIndex")));
			this.label12.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("label12.ImeMode")));
			this.label12.Location = ((System.Drawing.Point)(resources.GetObject("label12.Location")));
			this.label12.Name = "label12";
			this.label12.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("label12.RightToLeft")));
			this.label12.Size = ((System.Drawing.Size)(resources.GetObject("label12.Size")));
			this.label12.TabIndex = ((int)(resources.GetObject("label12.TabIndex")));
			this.label12.Text = resources.GetString("label12.Text");
			this.label12.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label12.TextAlign")));
			this.label12.Visible = ((bool)(resources.GetObject("label12.Visible")));
			// 
			// bntHelp
			// 
			this.bntHelp.AccessibleDescription = resources.GetString("bntHelp.AccessibleDescription");
			this.bntHelp.AccessibleName = resources.GetString("bntHelp.AccessibleName");
			this.bntHelp.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("bntHelp.Anchor")));
			this.bntHelp.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("bntHelp.BackgroundImage")));
			this.bntHelp.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("bntHelp.Dock")));
			this.bntHelp.Enabled = ((bool)(resources.GetObject("bntHelp.Enabled")));
			this.bntHelp.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("bntHelp.FlatStyle")));
			this.bntHelp.Font = ((System.Drawing.Font)(resources.GetObject("bntHelp.Font")));
			this.bntHelp.Image = ((System.Drawing.Image)(resources.GetObject("bntHelp.Image")));
			this.bntHelp.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("bntHelp.ImageAlign")));
			this.bntHelp.ImageIndex = ((int)(resources.GetObject("bntHelp.ImageIndex")));
			this.bntHelp.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("bntHelp.ImeMode")));
			this.bntHelp.Location = ((System.Drawing.Point)(resources.GetObject("bntHelp.Location")));
			this.bntHelp.Name = "bntHelp";
			this.bntHelp.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("bntHelp.RightToLeft")));
			this.bntHelp.Size = ((System.Drawing.Size)(resources.GetObject("bntHelp.Size")));
			this.bntHelp.TabIndex = ((int)(resources.GetObject("bntHelp.TabIndex")));
			this.bntHelp.Text = resources.GetString("bntHelp.Text");
			this.bntHelp.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("bntHelp.TextAlign")));
			this.bntHelp.Visible = ((bool)(resources.GetObject("bntHelp.Visible")));
			this.bntHelp.Click += new System.EventHandler(this.bntHelp_Click);
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
			this.btnDelete.Click += new System.EventHandler(this.bntDelete_Click);
			// 
			// label11
			// 
			this.label11.AccessibleDescription = resources.GetString("label11.AccessibleDescription");
			this.label11.AccessibleName = resources.GetString("label11.AccessibleName");
			this.label11.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("label11.Anchor")));
			this.label11.AutoSize = ((bool)(resources.GetObject("label11.AutoSize")));
			this.label11.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("label11.Dock")));
			this.label11.Enabled = ((bool)(resources.GetObject("label11.Enabled")));
			this.label11.Font = ((System.Drawing.Font)(resources.GetObject("label11.Font")));
			this.label11.Image = ((System.Drawing.Image)(resources.GetObject("label11.Image")));
			this.label11.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label11.ImageAlign")));
			this.label11.ImageIndex = ((int)(resources.GetObject("label11.ImageIndex")));
			this.label11.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("label11.ImeMode")));
			this.label11.Location = ((System.Drawing.Point)(resources.GetObject("label11.Location")));
			this.label11.Name = "label11";
			this.label11.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("label11.RightToLeft")));
			this.label11.Size = ((System.Drawing.Size)(resources.GetObject("label11.Size")));
			this.label11.TabIndex = ((int)(resources.GetObject("label11.TabIndex")));
			this.label11.Text = resources.GetString("label11.Text");
			this.label11.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label11.TextAlign")));
			this.label11.Visible = ((bool)(resources.GetObject("label11.Visible")));
			// 
			// label9
			// 
			this.label9.AccessibleDescription = resources.GetString("label9.AccessibleDescription");
			this.label9.AccessibleName = resources.GetString("label9.AccessibleName");
			this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("label9.Anchor")));
			this.label9.AutoSize = ((bool)(resources.GetObject("label9.AutoSize")));
			this.label9.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("label9.Dock")));
			this.label9.Enabled = ((bool)(resources.GetObject("label9.Enabled")));
			this.label9.Font = ((System.Drawing.Font)(resources.GetObject("label9.Font")));
			this.label9.ForeColor = System.Drawing.Color.Maroon;
			this.label9.Image = ((System.Drawing.Image)(resources.GetObject("label9.Image")));
			this.label9.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label9.ImageAlign")));
			this.label9.ImageIndex = ((int)(resources.GetObject("label9.ImageIndex")));
			this.label9.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("label9.ImeMode")));
			this.label9.Location = ((System.Drawing.Point)(resources.GetObject("label9.Location")));
			this.label9.Name = "label9";
			this.label9.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("label9.RightToLeft")));
			this.label9.Size = ((System.Drawing.Size)(resources.GetObject("label9.Size")));
			this.label9.TabIndex = ((int)(resources.GetObject("label9.TabIndex")));
			this.label9.Text = resources.GetString("label9.Text");
			this.label9.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label9.TextAlign")));
			this.label9.Visible = ((bool)(resources.GetObject("label9.Visible")));
			// 
			// lblFilter2
			// 
			this.lblFilter2.AccessibleDescription = resources.GetString("lblFilter2.AccessibleDescription");
			this.lblFilter2.AccessibleName = resources.GetString("lblFilter2.AccessibleName");
			this.lblFilter2.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblFilter2.Anchor")));
			this.lblFilter2.AutoSize = ((bool)(resources.GetObject("lblFilter2.AutoSize")));
			this.lblFilter2.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblFilter2.Dock")));
			this.lblFilter2.Enabled = ((bool)(resources.GetObject("lblFilter2.Enabled")));
			this.lblFilter2.Font = ((System.Drawing.Font)(resources.GetObject("lblFilter2.Font")));
			this.lblFilter2.Image = ((System.Drawing.Image)(resources.GetObject("lblFilter2.Image")));
			this.lblFilter2.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblFilter2.ImageAlign")));
			this.lblFilter2.ImageIndex = ((int)(resources.GetObject("lblFilter2.ImageIndex")));
			this.lblFilter2.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblFilter2.ImeMode")));
			this.lblFilter2.Location = ((System.Drawing.Point)(resources.GetObject("lblFilter2.Location")));
			this.lblFilter2.Name = "lblFilter2";
			this.lblFilter2.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblFilter2.RightToLeft")));
			this.lblFilter2.Size = ((System.Drawing.Size)(resources.GetObject("lblFilter2.Size")));
			this.lblFilter2.TabIndex = ((int)(resources.GetObject("lblFilter2.TabIndex")));
			this.lblFilter2.Text = resources.GetString("lblFilter2.Text");
			this.lblFilter2.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblFilter2.TextAlign")));
			this.lblFilter2.Visible = ((bool)(resources.GetObject("lblFilter2.Visible")));
			// 
			// label7
			// 
			this.label7.AccessibleDescription = resources.GetString("label7.AccessibleDescription");
			this.label7.AccessibleName = resources.GetString("label7.AccessibleName");
			this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("label7.Anchor")));
			this.label7.AutoSize = ((bool)(resources.GetObject("label7.AutoSize")));
			this.label7.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("label7.Dock")));
			this.label7.Enabled = ((bool)(resources.GetObject("label7.Enabled")));
			this.label7.Font = ((System.Drawing.Font)(resources.GetObject("label7.Font")));
			this.label7.ForeColor = System.Drawing.Color.Maroon;
			this.label7.Image = ((System.Drawing.Image)(resources.GetObject("label7.Image")));
			this.label7.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label7.ImageAlign")));
			this.label7.ImageIndex = ((int)(resources.GetObject("label7.ImageIndex")));
			this.label7.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("label7.ImeMode")));
			this.label7.Location = ((System.Drawing.Point)(resources.GetObject("label7.Location")));
			this.label7.Name = "label7";
			this.label7.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("label7.RightToLeft")));
			this.label7.Size = ((System.Drawing.Size)(resources.GetObject("label7.Size")));
			this.label7.TabIndex = ((int)(resources.GetObject("label7.TabIndex")));
			this.label7.Text = resources.GetString("label7.Text");
			this.label7.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label7.TextAlign")));
			this.label7.Visible = ((bool)(resources.GetObject("label7.Visible")));
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
			// lblFilter1
			// 
			this.lblFilter1.AccessibleDescription = resources.GetString("lblFilter1.AccessibleDescription");
			this.lblFilter1.AccessibleName = resources.GetString("lblFilter1.AccessibleName");
			this.lblFilter1.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblFilter1.Anchor")));
			this.lblFilter1.AutoSize = ((bool)(resources.GetObject("lblFilter1.AutoSize")));
			this.lblFilter1.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblFilter1.Dock")));
			this.lblFilter1.Enabled = ((bool)(resources.GetObject("lblFilter1.Enabled")));
			this.lblFilter1.Font = ((System.Drawing.Font)(resources.GetObject("lblFilter1.Font")));
			this.lblFilter1.Image = ((System.Drawing.Image)(resources.GetObject("lblFilter1.Image")));
			this.lblFilter1.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblFilter1.ImageAlign")));
			this.lblFilter1.ImageIndex = ((int)(resources.GetObject("lblFilter1.ImageIndex")));
			this.lblFilter1.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblFilter1.ImeMode")));
			this.lblFilter1.Location = ((System.Drawing.Point)(resources.GetObject("lblFilter1.Location")));
			this.lblFilter1.Name = "lblFilter1";
			this.lblFilter1.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblFilter1.RightToLeft")));
			this.lblFilter1.Size = ((System.Drawing.Size)(resources.GetObject("lblFilter1.Size")));
			this.lblFilter1.TabIndex = ((int)(resources.GetObject("lblFilter1.TabIndex")));
			this.lblFilter1.Text = resources.GetString("lblFilter1.Text");
			this.lblFilter1.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblFilter1.TextAlign")));
			this.lblFilter1.Visible = ((bool)(resources.GetObject("lblFilter1.Visible")));
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
			// label1
			// 
			this.label1.AccessibleDescription = resources.GetString("label1.AccessibleDescription");
			this.label1.AccessibleName = resources.GetString("label1.AccessibleName");
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("label1.Anchor")));
			this.label1.AutoSize = ((bool)(resources.GetObject("label1.AutoSize")));
			this.label1.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("label1.Dock")));
			this.label1.Enabled = ((bool)(resources.GetObject("label1.Enabled")));
			this.label1.Font = ((System.Drawing.Font)(resources.GetObject("label1.Font")));
			this.label1.ForeColor = System.Drawing.Color.Maroon;
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
			// label10
			// 
			this.label10.AccessibleDescription = resources.GetString("label10.AccessibleDescription");
			this.label10.AccessibleName = resources.GetString("label10.AccessibleName");
			this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("label10.Anchor")));
			this.label10.AutoSize = ((bool)(resources.GetObject("label10.AutoSize")));
			this.label10.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("label10.Dock")));
			this.label10.Enabled = ((bool)(resources.GetObject("label10.Enabled")));
			this.label10.Font = ((System.Drawing.Font)(resources.GetObject("label10.Font")));
			this.label10.Image = ((System.Drawing.Image)(resources.GetObject("label10.Image")));
			this.label10.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label10.ImageAlign")));
			this.label10.ImageIndex = ((int)(resources.GetObject("label10.ImageIndex")));
			this.label10.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("label10.ImeMode")));
			this.label10.Location = ((System.Drawing.Point)(resources.GetObject("label10.Location")));
			this.label10.Name = "label10";
			this.label10.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("label10.RightToLeft")));
			this.label10.Size = ((System.Drawing.Size)(resources.GetObject("label10.Size")));
			this.label10.TabIndex = ((int)(resources.GetObject("label10.TabIndex")));
			this.label10.Text = resources.GetString("label10.Text");
			this.label10.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label10.TextAlign")));
			this.label10.Visible = ((bool)(resources.GetObject("label10.Visible")));
			// 
			// label13
			// 
			this.label13.AccessibleDescription = resources.GetString("label13.AccessibleDescription");
			this.label13.AccessibleName = resources.GetString("label13.AccessibleName");
			this.label13.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("label13.Anchor")));
			this.label13.AutoSize = ((bool)(resources.GetObject("label13.AutoSize")));
			this.label13.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("label13.Dock")));
			this.label13.Enabled = ((bool)(resources.GetObject("label13.Enabled")));
			this.label13.Font = ((System.Drawing.Font)(resources.GetObject("label13.Font")));
			this.label13.Image = ((System.Drawing.Image)(resources.GetObject("label13.Image")));
			this.label13.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label13.ImageAlign")));
			this.label13.ImageIndex = ((int)(resources.GetObject("label13.ImageIndex")));
			this.label13.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("label13.ImeMode")));
			this.label13.Location = ((System.Drawing.Point)(resources.GetObject("label13.Location")));
			this.label13.Name = "label13";
			this.label13.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("label13.RightToLeft")));
			this.label13.Size = ((System.Drawing.Size)(resources.GetObject("label13.Size")));
			this.label13.TabIndex = ((int)(resources.GetObject("label13.TabIndex")));
			this.label13.Text = resources.GetString("label13.Text");
			this.label13.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label13.TextAlign")));
			this.label13.Visible = ((bool)(resources.GetObject("label13.Visible")));
			// 
			// label14
			// 
			this.label14.AccessibleDescription = resources.GetString("label14.AccessibleDescription");
			this.label14.AccessibleName = resources.GetString("label14.AccessibleName");
			this.label14.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("label14.Anchor")));
			this.label14.AutoSize = ((bool)(resources.GetObject("label14.AutoSize")));
			this.label14.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("label14.Dock")));
			this.label14.Enabled = ((bool)(resources.GetObject("label14.Enabled")));
			this.label14.Font = ((System.Drawing.Font)(resources.GetObject("label14.Font")));
			this.label14.Image = ((System.Drawing.Image)(resources.GetObject("label14.Image")));
			this.label14.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label14.ImageAlign")));
			this.label14.ImageIndex = ((int)(resources.GetObject("label14.ImageIndex")));
			this.label14.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("label14.ImeMode")));
			this.label14.Location = ((System.Drawing.Point)(resources.GetObject("label14.Location")));
			this.label14.Name = "label14";
			this.label14.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("label14.RightToLeft")));
			this.label14.Size = ((System.Drawing.Size)(resources.GetObject("label14.Size")));
			this.label14.TabIndex = ((int)(resources.GetObject("label14.TabIndex")));
			this.label14.Text = resources.GetString("label14.Text");
			this.label14.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label14.TextAlign")));
			this.label14.Visible = ((bool)(resources.GetObject("label14.Visible")));
			// 
			// txtVietnamese
			// 
			this.txtVietnamese.AccessibleDescription = resources.GetString("txtVietnamese.AccessibleDescription");
			this.txtVietnamese.AccessibleName = resources.GetString("txtVietnamese.AccessibleName");
			this.txtVietnamese.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("txtVietnamese.Anchor")));
			this.txtVietnamese.AutoSize = ((bool)(resources.GetObject("txtVietnamese.AutoSize")));
			this.txtVietnamese.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("txtVietnamese.BackgroundImage")));
			this.txtVietnamese.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("txtVietnamese.Dock")));
			this.txtVietnamese.Enabled = ((bool)(resources.GetObject("txtVietnamese.Enabled")));
			this.txtVietnamese.Font = ((System.Drawing.Font)(resources.GetObject("txtVietnamese.Font")));
			this.txtVietnamese.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("txtVietnamese.ImeMode")));
			this.txtVietnamese.Location = ((System.Drawing.Point)(resources.GetObject("txtVietnamese.Location")));
			this.txtVietnamese.MaxLength = ((int)(resources.GetObject("txtVietnamese.MaxLength")));
			this.txtVietnamese.Multiline = ((bool)(resources.GetObject("txtVietnamese.Multiline")));
			this.txtVietnamese.Name = "txtVietnamese";
			this.txtVietnamese.PasswordChar = ((char)(resources.GetObject("txtVietnamese.PasswordChar")));
			this.txtVietnamese.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("txtVietnamese.RightToLeft")));
			this.txtVietnamese.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("txtVietnamese.ScrollBars")));
			this.txtVietnamese.Size = ((System.Drawing.Size)(resources.GetObject("txtVietnamese.Size")));
			this.txtVietnamese.TabIndex = ((int)(resources.GetObject("txtVietnamese.TabIndex")));
			this.txtVietnamese.Text = resources.GetString("txtVietnamese.Text");
			this.txtVietnamese.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("txtVietnamese.TextAlign")));
			this.txtVietnamese.Visible = ((bool)(resources.GetObject("txtVietnamese.Visible")));
			this.txtVietnamese.WordWrap = ((bool)(resources.GetObject("txtVietnamese.WordWrap")));
			this.txtVietnamese.Leave += new System.EventHandler(this.OnControlLeave);
			this.txtVietnamese.Enter += new System.EventHandler(this.OnControlEnter);
			// 
			// lstField
			// 
			this.lstField.AccessibleDescription = resources.GetString("lstField.AccessibleDescription");
			this.lstField.AccessibleName = resources.GetString("lstField.AccessibleName");
			this.lstField.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lstField.Anchor")));
			this.lstField.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("lstField.BackgroundImage")));
			this.lstField.ColumnWidth = ((int)(resources.GetObject("lstField.ColumnWidth")));
			this.lstField.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lstField.Dock")));
			this.lstField.Enabled = ((bool)(resources.GetObject("lstField.Enabled")));
			this.lstField.Font = ((System.Drawing.Font)(resources.GetObject("lstField.Font")));
			this.lstField.HorizontalExtent = ((int)(resources.GetObject("lstField.HorizontalExtent")));
			this.lstField.HorizontalScrollbar = ((bool)(resources.GetObject("lstField.HorizontalScrollbar")));
			this.lstField.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lstField.ImeMode")));
			this.lstField.IntegralHeight = ((bool)(resources.GetObject("lstField.IntegralHeight")));
			this.lstField.ItemHeight = ((int)(resources.GetObject("lstField.ItemHeight")));
			this.lstField.Location = ((System.Drawing.Point)(resources.GetObject("lstField.Location")));
			this.lstField.Name = "lstField";
			this.lstField.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lstField.RightToLeft")));
			this.lstField.ScrollAlwaysVisible = ((bool)(resources.GetObject("lstField.ScrollAlwaysVisible")));
			this.lstField.Size = ((System.Drawing.Size)(resources.GetObject("lstField.Size")));
			this.lstField.TabIndex = ((int)(resources.GetObject("lstField.TabIndex")));
			this.lstField.Visible = ((bool)(resources.GetObject("lstField.Visible")));
			this.lstField.SelectedIndexChanged += new System.EventHandler(this.lsField_SelectedIndexChanged);
			// 
			// lblFilter3
			// 
			this.lblFilter3.AccessibleDescription = resources.GetString("lblFilter3.AccessibleDescription");
			this.lblFilter3.AccessibleName = resources.GetString("lblFilter3.AccessibleName");
			this.lblFilter3.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblFilter3.Anchor")));
			this.lblFilter3.AutoSize = ((bool)(resources.GetObject("lblFilter3.AutoSize")));
			this.lblFilter3.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblFilter3.Dock")));
			this.lblFilter3.Enabled = ((bool)(resources.GetObject("lblFilter3.Enabled")));
			this.lblFilter3.Font = ((System.Drawing.Font)(resources.GetObject("lblFilter3.Font")));
			this.lblFilter3.Image = ((System.Drawing.Image)(resources.GetObject("lblFilter3.Image")));
			this.lblFilter3.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblFilter3.ImageAlign")));
			this.lblFilter3.ImageIndex = ((int)(resources.GetObject("lblFilter3.ImageIndex")));
			this.lblFilter3.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblFilter3.ImeMode")));
			this.lblFilter3.Location = ((System.Drawing.Point)(resources.GetObject("lblFilter3.Location")));
			this.lblFilter3.Name = "lblFilter3";
			this.lblFilter3.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblFilter3.RightToLeft")));
			this.lblFilter3.Size = ((System.Drawing.Size)(resources.GetObject("lblFilter3.Size")));
			this.lblFilter3.TabIndex = ((int)(resources.GetObject("lblFilter3.TabIndex")));
			this.lblFilter3.Text = resources.GetString("lblFilter3.Text");
			this.lblFilter3.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblFilter3.TextAlign")));
			this.lblFilter3.Visible = ((bool)(resources.GetObject("lblFilter3.Visible")));
			// 
			// cboFilterField3
			// 
			this.cboFilterField3.AccessibleDescription = resources.GetString("cboFilterField3.AccessibleDescription");
			this.cboFilterField3.AccessibleName = resources.GetString("cboFilterField3.AccessibleName");
			this.cboFilterField3.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("cboFilterField3.Anchor")));
			this.cboFilterField3.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cboFilterField3.BackgroundImage")));
			this.cboFilterField3.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("cboFilterField3.Dock")));
			this.cboFilterField3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboFilterField3.Enabled = ((bool)(resources.GetObject("cboFilterField3.Enabled")));
			this.cboFilterField3.Font = ((System.Drawing.Font)(resources.GetObject("cboFilterField3.Font")));
			this.cboFilterField3.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("cboFilterField3.ImeMode")));
			this.cboFilterField3.IntegralHeight = ((bool)(resources.GetObject("cboFilterField3.IntegralHeight")));
			this.cboFilterField3.ItemHeight = ((int)(resources.GetObject("cboFilterField3.ItemHeight")));
			this.cboFilterField3.Location = ((System.Drawing.Point)(resources.GetObject("cboFilterField3.Location")));
			this.cboFilterField3.MaxDropDownItems = ((int)(resources.GetObject("cboFilterField3.MaxDropDownItems")));
			this.cboFilterField3.MaxLength = ((int)(resources.GetObject("cboFilterField3.MaxLength")));
			this.cboFilterField3.Name = "cboFilterField3";
			this.cboFilterField3.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("cboFilterField3.RightToLeft")));
			this.cboFilterField3.Size = ((System.Drawing.Size)(resources.GetObject("cboFilterField3.Size")));
			this.cboFilterField3.TabIndex = ((int)(resources.GetObject("cboFilterField3.TabIndex")));
			this.cboFilterField3.Text = resources.GetString("cboFilterField3.Text");
			this.cboFilterField3.Visible = ((bool)(resources.GetObject("cboFilterField3.Visible")));
			this.cboFilterField3.SelectedIndexChanged += new System.EventHandler(this.cboFilterField3_SelectedIndexChanged);
			// 
			// grbExternalField
			// 
			this.grbExternalField.AccessibleDescription = resources.GetString("grbExternalField.AccessibleDescription");
			this.grbExternalField.AccessibleName = resources.GetString("grbExternalField.AccessibleName");
			this.grbExternalField.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("grbExternalField.Anchor")));
			this.grbExternalField.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("grbExternalField.BackgroundImage")));
			this.grbExternalField.Controls.Add(this.lblFormat3);
			this.grbExternalField.Controls.Add(this.lblAlign3);
			this.grbExternalField.Controls.Add(this.lblWidth3);
			this.grbExternalField.Controls.Add(this.lblEnglish3);
			this.grbExternalField.Controls.Add(this.lblJapanese3);
			this.grbExternalField.Controls.Add(this.lblVietnamese3);
			this.grbExternalField.Controls.Add(this.txtVietnamese3);
			this.grbExternalField.Controls.Add(this.txtEnglish3);
			this.grbExternalField.Controls.Add(this.txtFormat3);
			this.grbExternalField.Controls.Add(this.txtJapanese3);
			this.grbExternalField.Controls.Add(this.cboAlign3);
			this.grbExternalField.Controls.Add(this.lblFormat2);
			this.grbExternalField.Controls.Add(this.lblAlign2);
			this.grbExternalField.Controls.Add(this.lblWidth2);
			this.grbExternalField.Controls.Add(this.lblEnglish2);
			this.grbExternalField.Controls.Add(this.lblJapanese2);
			this.grbExternalField.Controls.Add(this.lblVietnamese2);
			this.grbExternalField.Controls.Add(this.txtVietnamese2);
			this.grbExternalField.Controls.Add(this.txtEnglish2);
			this.grbExternalField.Controls.Add(this.txtFormat2);
			this.grbExternalField.Controls.Add(this.txtJapanese2);
			this.grbExternalField.Controls.Add(this.cboAlign2);
			this.grbExternalField.Controls.Add(this.lblFormat1);
			this.grbExternalField.Controls.Add(this.lblAlign1);
			this.grbExternalField.Controls.Add(this.lblWidth1);
			this.grbExternalField.Controls.Add(this.lblEnglish1);
			this.grbExternalField.Controls.Add(this.lblJapanese1);
			this.grbExternalField.Controls.Add(this.lblVietnamese1);
			this.grbExternalField.Controls.Add(this.txtVietnamese1);
			this.grbExternalField.Controls.Add(this.txtEnglish1);
			this.grbExternalField.Controls.Add(this.txtFormat1);
			this.grbExternalField.Controls.Add(this.txtJapanese1);
			this.grbExternalField.Controls.Add(this.cboAlign1);
			this.grbExternalField.Controls.Add(this.lblFilter1);
			this.grbExternalField.Controls.Add(this.lblFilter3);
			this.grbExternalField.Controls.Add(this.cboFilterField3);
			this.grbExternalField.Controls.Add(this.cboFilterField2);
			this.grbExternalField.Controls.Add(this.cboFilterField1);
			this.grbExternalField.Controls.Add(this.lblFilter2);
			this.grbExternalField.Controls.Add(this.txtWidth1);
			this.grbExternalField.Controls.Add(this.txtWidth2);
			this.grbExternalField.Controls.Add(this.txtWidth3);
			this.grbExternalField.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("grbExternalField.Dock")));
			this.grbExternalField.Enabled = ((bool)(resources.GetObject("grbExternalField.Enabled")));
			this.grbExternalField.Font = ((System.Drawing.Font)(resources.GetObject("grbExternalField.Font")));
			this.grbExternalField.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("grbExternalField.ImeMode")));
			this.grbExternalField.Location = ((System.Drawing.Point)(resources.GetObject("grbExternalField.Location")));
			this.grbExternalField.Name = "grbExternalField";
			this.grbExternalField.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("grbExternalField.RightToLeft")));
			this.grbExternalField.Size = ((System.Drawing.Size)(resources.GetObject("grbExternalField.Size")));
			this.grbExternalField.TabIndex = ((int)(resources.GetObject("grbExternalField.TabIndex")));
			this.grbExternalField.TabStop = false;
			this.grbExternalField.Text = resources.GetString("grbExternalField.Text");
			this.grbExternalField.Visible = ((bool)(resources.GetObject("grbExternalField.Visible")));
			// 
			// lblFormat3
			// 
			this.lblFormat3.AccessibleDescription = resources.GetString("lblFormat3.AccessibleDescription");
			this.lblFormat3.AccessibleName = resources.GetString("lblFormat3.AccessibleName");
			this.lblFormat3.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblFormat3.Anchor")));
			this.lblFormat3.AutoSize = ((bool)(resources.GetObject("lblFormat3.AutoSize")));
			this.lblFormat3.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblFormat3.Dock")));
			this.lblFormat3.Enabled = ((bool)(resources.GetObject("lblFormat3.Enabled")));
			this.lblFormat3.Font = ((System.Drawing.Font)(resources.GetObject("lblFormat3.Font")));
			this.lblFormat3.Image = ((System.Drawing.Image)(resources.GetObject("lblFormat3.Image")));
			this.lblFormat3.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblFormat3.ImageAlign")));
			this.lblFormat3.ImageIndex = ((int)(resources.GetObject("lblFormat3.ImageIndex")));
			this.lblFormat3.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblFormat3.ImeMode")));
			this.lblFormat3.Location = ((System.Drawing.Point)(resources.GetObject("lblFormat3.Location")));
			this.lblFormat3.Name = "lblFormat3";
			this.lblFormat3.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblFormat3.RightToLeft")));
			this.lblFormat3.Size = ((System.Drawing.Size)(resources.GetObject("lblFormat3.Size")));
			this.lblFormat3.TabIndex = ((int)(resources.GetObject("lblFormat3.TabIndex")));
			this.lblFormat3.Text = resources.GetString("lblFormat3.Text");
			this.lblFormat3.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblFormat3.TextAlign")));
			this.lblFormat3.Visible = ((bool)(resources.GetObject("lblFormat3.Visible")));
			// 
			// lblAlign3
			// 
			this.lblAlign3.AccessibleDescription = resources.GetString("lblAlign3.AccessibleDescription");
			this.lblAlign3.AccessibleName = resources.GetString("lblAlign3.AccessibleName");
			this.lblAlign3.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblAlign3.Anchor")));
			this.lblAlign3.AutoSize = ((bool)(resources.GetObject("lblAlign3.AutoSize")));
			this.lblAlign3.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblAlign3.Dock")));
			this.lblAlign3.Enabled = ((bool)(resources.GetObject("lblAlign3.Enabled")));
			this.lblAlign3.Font = ((System.Drawing.Font)(resources.GetObject("lblAlign3.Font")));
			this.lblAlign3.ForeColor = System.Drawing.Color.Black;
			this.lblAlign3.Image = ((System.Drawing.Image)(resources.GetObject("lblAlign3.Image")));
			this.lblAlign3.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblAlign3.ImageAlign")));
			this.lblAlign3.ImageIndex = ((int)(resources.GetObject("lblAlign3.ImageIndex")));
			this.lblAlign3.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblAlign3.ImeMode")));
			this.lblAlign3.Location = ((System.Drawing.Point)(resources.GetObject("lblAlign3.Location")));
			this.lblAlign3.Name = "lblAlign3";
			this.lblAlign3.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblAlign3.RightToLeft")));
			this.lblAlign3.Size = ((System.Drawing.Size)(resources.GetObject("lblAlign3.Size")));
			this.lblAlign3.TabIndex = ((int)(resources.GetObject("lblAlign3.TabIndex")));
			this.lblAlign3.Text = resources.GetString("lblAlign3.Text");
			this.lblAlign3.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblAlign3.TextAlign")));
			this.lblAlign3.Visible = ((bool)(resources.GetObject("lblAlign3.Visible")));
			// 
			// lblWidth3
			// 
			this.lblWidth3.AccessibleDescription = resources.GetString("lblWidth3.AccessibleDescription");
			this.lblWidth3.AccessibleName = resources.GetString("lblWidth3.AccessibleName");
			this.lblWidth3.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblWidth3.Anchor")));
			this.lblWidth3.AutoSize = ((bool)(resources.GetObject("lblWidth3.AutoSize")));
			this.lblWidth3.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblWidth3.Dock")));
			this.lblWidth3.Enabled = ((bool)(resources.GetObject("lblWidth3.Enabled")));
			this.lblWidth3.Font = ((System.Drawing.Font)(resources.GetObject("lblWidth3.Font")));
			this.lblWidth3.Image = ((System.Drawing.Image)(resources.GetObject("lblWidth3.Image")));
			this.lblWidth3.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblWidth3.ImageAlign")));
			this.lblWidth3.ImageIndex = ((int)(resources.GetObject("lblWidth3.ImageIndex")));
			this.lblWidth3.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblWidth3.ImeMode")));
			this.lblWidth3.Location = ((System.Drawing.Point)(resources.GetObject("lblWidth3.Location")));
			this.lblWidth3.Name = "lblWidth3";
			this.lblWidth3.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblWidth3.RightToLeft")));
			this.lblWidth3.Size = ((System.Drawing.Size)(resources.GetObject("lblWidth3.Size")));
			this.lblWidth3.TabIndex = ((int)(resources.GetObject("lblWidth3.TabIndex")));
			this.lblWidth3.Text = resources.GetString("lblWidth3.Text");
			this.lblWidth3.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblWidth3.TextAlign")));
			this.lblWidth3.Visible = ((bool)(resources.GetObject("lblWidth3.Visible")));
			// 
			// lblEnglish3
			// 
			this.lblEnglish3.AccessibleDescription = resources.GetString("lblEnglish3.AccessibleDescription");
			this.lblEnglish3.AccessibleName = resources.GetString("lblEnglish3.AccessibleName");
			this.lblEnglish3.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblEnglish3.Anchor")));
			this.lblEnglish3.AutoSize = ((bool)(resources.GetObject("lblEnglish3.AutoSize")));
			this.lblEnglish3.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblEnglish3.Dock")));
			this.lblEnglish3.Enabled = ((bool)(resources.GetObject("lblEnglish3.Enabled")));
			this.lblEnglish3.Font = ((System.Drawing.Font)(resources.GetObject("lblEnglish3.Font")));
			this.lblEnglish3.Image = ((System.Drawing.Image)(resources.GetObject("lblEnglish3.Image")));
			this.lblEnglish3.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblEnglish3.ImageAlign")));
			this.lblEnglish3.ImageIndex = ((int)(resources.GetObject("lblEnglish3.ImageIndex")));
			this.lblEnglish3.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblEnglish3.ImeMode")));
			this.lblEnglish3.Location = ((System.Drawing.Point)(resources.GetObject("lblEnglish3.Location")));
			this.lblEnglish3.Name = "lblEnglish3";
			this.lblEnglish3.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblEnglish3.RightToLeft")));
			this.lblEnglish3.Size = ((System.Drawing.Size)(resources.GetObject("lblEnglish3.Size")));
			this.lblEnglish3.TabIndex = ((int)(resources.GetObject("lblEnglish3.TabIndex")));
			this.lblEnglish3.Text = resources.GetString("lblEnglish3.Text");
			this.lblEnglish3.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblEnglish3.TextAlign")));
			this.lblEnglish3.Visible = ((bool)(resources.GetObject("lblEnglish3.Visible")));
			// 
			// lblJapanese3
			// 
			this.lblJapanese3.AccessibleDescription = resources.GetString("lblJapanese3.AccessibleDescription");
			this.lblJapanese3.AccessibleName = resources.GetString("lblJapanese3.AccessibleName");
			this.lblJapanese3.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblJapanese3.Anchor")));
			this.lblJapanese3.AutoSize = ((bool)(resources.GetObject("lblJapanese3.AutoSize")));
			this.lblJapanese3.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblJapanese3.Dock")));
			this.lblJapanese3.Enabled = ((bool)(resources.GetObject("lblJapanese3.Enabled")));
			this.lblJapanese3.Font = ((System.Drawing.Font)(resources.GetObject("lblJapanese3.Font")));
			this.lblJapanese3.Image = ((System.Drawing.Image)(resources.GetObject("lblJapanese3.Image")));
			this.lblJapanese3.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblJapanese3.ImageAlign")));
			this.lblJapanese3.ImageIndex = ((int)(resources.GetObject("lblJapanese3.ImageIndex")));
			this.lblJapanese3.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblJapanese3.ImeMode")));
			this.lblJapanese3.Location = ((System.Drawing.Point)(resources.GetObject("lblJapanese3.Location")));
			this.lblJapanese3.Name = "lblJapanese3";
			this.lblJapanese3.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblJapanese3.RightToLeft")));
			this.lblJapanese3.Size = ((System.Drawing.Size)(resources.GetObject("lblJapanese3.Size")));
			this.lblJapanese3.TabIndex = ((int)(resources.GetObject("lblJapanese3.TabIndex")));
			this.lblJapanese3.Text = resources.GetString("lblJapanese3.Text");
			this.lblJapanese3.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblJapanese3.TextAlign")));
			this.lblJapanese3.Visible = ((bool)(resources.GetObject("lblJapanese3.Visible")));
			// 
			// lblVietnamese3
			// 
			this.lblVietnamese3.AccessibleDescription = resources.GetString("lblVietnamese3.AccessibleDescription");
			this.lblVietnamese3.AccessibleName = resources.GetString("lblVietnamese3.AccessibleName");
			this.lblVietnamese3.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblVietnamese3.Anchor")));
			this.lblVietnamese3.AutoSize = ((bool)(resources.GetObject("lblVietnamese3.AutoSize")));
			this.lblVietnamese3.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblVietnamese3.Dock")));
			this.lblVietnamese3.Enabled = ((bool)(resources.GetObject("lblVietnamese3.Enabled")));
			this.lblVietnamese3.Font = ((System.Drawing.Font)(resources.GetObject("lblVietnamese3.Font")));
			this.lblVietnamese3.Image = ((System.Drawing.Image)(resources.GetObject("lblVietnamese3.Image")));
			this.lblVietnamese3.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblVietnamese3.ImageAlign")));
			this.lblVietnamese3.ImageIndex = ((int)(resources.GetObject("lblVietnamese3.ImageIndex")));
			this.lblVietnamese3.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblVietnamese3.ImeMode")));
			this.lblVietnamese3.Location = ((System.Drawing.Point)(resources.GetObject("lblVietnamese3.Location")));
			this.lblVietnamese3.Name = "lblVietnamese3";
			this.lblVietnamese3.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblVietnamese3.RightToLeft")));
			this.lblVietnamese3.Size = ((System.Drawing.Size)(resources.GetObject("lblVietnamese3.Size")));
			this.lblVietnamese3.TabIndex = ((int)(resources.GetObject("lblVietnamese3.TabIndex")));
			this.lblVietnamese3.Text = resources.GetString("lblVietnamese3.Text");
			this.lblVietnamese3.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblVietnamese3.TextAlign")));
			this.lblVietnamese3.Visible = ((bool)(resources.GetObject("lblVietnamese3.Visible")));
			// 
			// txtVietnamese3
			// 
			this.txtVietnamese3.AccessibleDescription = resources.GetString("txtVietnamese3.AccessibleDescription");
			this.txtVietnamese3.AccessibleName = resources.GetString("txtVietnamese3.AccessibleName");
			this.txtVietnamese3.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("txtVietnamese3.Anchor")));
			this.txtVietnamese3.AutoSize = ((bool)(resources.GetObject("txtVietnamese3.AutoSize")));
			this.txtVietnamese3.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("txtVietnamese3.BackgroundImage")));
			this.txtVietnamese3.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("txtVietnamese3.Dock")));
			this.txtVietnamese3.Enabled = ((bool)(resources.GetObject("txtVietnamese3.Enabled")));
			this.txtVietnamese3.Font = ((System.Drawing.Font)(resources.GetObject("txtVietnamese3.Font")));
			this.txtVietnamese3.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("txtVietnamese3.ImeMode")));
			this.txtVietnamese3.Location = ((System.Drawing.Point)(resources.GetObject("txtVietnamese3.Location")));
			this.txtVietnamese3.MaxLength = ((int)(resources.GetObject("txtVietnamese3.MaxLength")));
			this.txtVietnamese3.Multiline = ((bool)(resources.GetObject("txtVietnamese3.Multiline")));
			this.txtVietnamese3.Name = "txtVietnamese3";
			this.txtVietnamese3.PasswordChar = ((char)(resources.GetObject("txtVietnamese3.PasswordChar")));
			this.txtVietnamese3.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("txtVietnamese3.RightToLeft")));
			this.txtVietnamese3.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("txtVietnamese3.ScrollBars")));
			this.txtVietnamese3.Size = ((System.Drawing.Size)(resources.GetObject("txtVietnamese3.Size")));
			this.txtVietnamese3.TabIndex = ((int)(resources.GetObject("txtVietnamese3.TabIndex")));
			this.txtVietnamese3.Text = resources.GetString("txtVietnamese3.Text");
			this.txtVietnamese3.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("txtVietnamese3.TextAlign")));
			this.txtVietnamese3.Visible = ((bool)(resources.GetObject("txtVietnamese3.Visible")));
			this.txtVietnamese3.WordWrap = ((bool)(resources.GetObject("txtVietnamese3.WordWrap")));
			// 
			// txtEnglish3
			// 
			this.txtEnglish3.AccessibleDescription = resources.GetString("txtEnglish3.AccessibleDescription");
			this.txtEnglish3.AccessibleName = resources.GetString("txtEnglish3.AccessibleName");
			this.txtEnglish3.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("txtEnglish3.Anchor")));
			this.txtEnglish3.AutoSize = ((bool)(resources.GetObject("txtEnglish3.AutoSize")));
			this.txtEnglish3.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("txtEnglish3.BackgroundImage")));
			this.txtEnglish3.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("txtEnglish3.Dock")));
			this.txtEnglish3.Enabled = ((bool)(resources.GetObject("txtEnglish3.Enabled")));
			this.txtEnglish3.Font = ((System.Drawing.Font)(resources.GetObject("txtEnglish3.Font")));
			this.txtEnglish3.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("txtEnglish3.ImeMode")));
			this.txtEnglish3.Location = ((System.Drawing.Point)(resources.GetObject("txtEnglish3.Location")));
			this.txtEnglish3.MaxLength = ((int)(resources.GetObject("txtEnglish3.MaxLength")));
			this.txtEnglish3.Multiline = ((bool)(resources.GetObject("txtEnglish3.Multiline")));
			this.txtEnglish3.Name = "txtEnglish3";
			this.txtEnglish3.PasswordChar = ((char)(resources.GetObject("txtEnglish3.PasswordChar")));
			this.txtEnglish3.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("txtEnglish3.RightToLeft")));
			this.txtEnglish3.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("txtEnglish3.ScrollBars")));
			this.txtEnglish3.Size = ((System.Drawing.Size)(resources.GetObject("txtEnglish3.Size")));
			this.txtEnglish3.TabIndex = ((int)(resources.GetObject("txtEnglish3.TabIndex")));
			this.txtEnglish3.Text = resources.GetString("txtEnglish3.Text");
			this.txtEnglish3.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("txtEnglish3.TextAlign")));
			this.txtEnglish3.Visible = ((bool)(resources.GetObject("txtEnglish3.Visible")));
			this.txtEnglish3.WordWrap = ((bool)(resources.GetObject("txtEnglish3.WordWrap")));
			// 
			// txtFormat3
			// 
			this.txtFormat3.AccessibleDescription = resources.GetString("txtFormat3.AccessibleDescription");
			this.txtFormat3.AccessibleName = resources.GetString("txtFormat3.AccessibleName");
			this.txtFormat3.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("txtFormat3.Anchor")));
			this.txtFormat3.AutoSize = ((bool)(resources.GetObject("txtFormat3.AutoSize")));
			this.txtFormat3.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("txtFormat3.BackgroundImage")));
			this.txtFormat3.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("txtFormat3.Dock")));
			this.txtFormat3.Enabled = ((bool)(resources.GetObject("txtFormat3.Enabled")));
			this.txtFormat3.Font = ((System.Drawing.Font)(resources.GetObject("txtFormat3.Font")));
			this.txtFormat3.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("txtFormat3.ImeMode")));
			this.txtFormat3.Location = ((System.Drawing.Point)(resources.GetObject("txtFormat3.Location")));
			this.txtFormat3.MaxLength = ((int)(resources.GetObject("txtFormat3.MaxLength")));
			this.txtFormat3.Multiline = ((bool)(resources.GetObject("txtFormat3.Multiline")));
			this.txtFormat3.Name = "txtFormat3";
			this.txtFormat3.PasswordChar = ((char)(resources.GetObject("txtFormat3.PasswordChar")));
			this.txtFormat3.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("txtFormat3.RightToLeft")));
			this.txtFormat3.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("txtFormat3.ScrollBars")));
			this.txtFormat3.Size = ((System.Drawing.Size)(resources.GetObject("txtFormat3.Size")));
			this.txtFormat3.TabIndex = ((int)(resources.GetObject("txtFormat3.TabIndex")));
			this.txtFormat3.Text = resources.GetString("txtFormat3.Text");
			this.txtFormat3.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("txtFormat3.TextAlign")));
			this.txtFormat3.Visible = ((bool)(resources.GetObject("txtFormat3.Visible")));
			this.txtFormat3.WordWrap = ((bool)(resources.GetObject("txtFormat3.WordWrap")));
			// 
			// txtJapanese3
			// 
			this.txtJapanese3.AccessibleDescription = resources.GetString("txtJapanese3.AccessibleDescription");
			this.txtJapanese3.AccessibleName = resources.GetString("txtJapanese3.AccessibleName");
			this.txtJapanese3.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("txtJapanese3.Anchor")));
			this.txtJapanese3.AutoSize = ((bool)(resources.GetObject("txtJapanese3.AutoSize")));
			this.txtJapanese3.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("txtJapanese3.BackgroundImage")));
			this.txtJapanese3.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("txtJapanese3.Dock")));
			this.txtJapanese3.Enabled = ((bool)(resources.GetObject("txtJapanese3.Enabled")));
			this.txtJapanese3.Font = ((System.Drawing.Font)(resources.GetObject("txtJapanese3.Font")));
			this.txtJapanese3.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("txtJapanese3.ImeMode")));
			this.txtJapanese3.Location = ((System.Drawing.Point)(resources.GetObject("txtJapanese3.Location")));
			this.txtJapanese3.MaxLength = ((int)(resources.GetObject("txtJapanese3.MaxLength")));
			this.txtJapanese3.Multiline = ((bool)(resources.GetObject("txtJapanese3.Multiline")));
			this.txtJapanese3.Name = "txtJapanese3";
			this.txtJapanese3.PasswordChar = ((char)(resources.GetObject("txtJapanese3.PasswordChar")));
			this.txtJapanese3.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("txtJapanese3.RightToLeft")));
			this.txtJapanese3.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("txtJapanese3.ScrollBars")));
			this.txtJapanese3.Size = ((System.Drawing.Size)(resources.GetObject("txtJapanese3.Size")));
			this.txtJapanese3.TabIndex = ((int)(resources.GetObject("txtJapanese3.TabIndex")));
			this.txtJapanese3.Text = resources.GetString("txtJapanese3.Text");
			this.txtJapanese3.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("txtJapanese3.TextAlign")));
			this.txtJapanese3.Visible = ((bool)(resources.GetObject("txtJapanese3.Visible")));
			this.txtJapanese3.WordWrap = ((bool)(resources.GetObject("txtJapanese3.WordWrap")));
			// 
			// cboAlign3
			// 
			this.cboAlign3.AccessibleDescription = resources.GetString("cboAlign3.AccessibleDescription");
			this.cboAlign3.AccessibleName = resources.GetString("cboAlign3.AccessibleName");
			this.cboAlign3.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("cboAlign3.Anchor")));
			this.cboAlign3.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cboAlign3.BackgroundImage")));
			this.cboAlign3.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("cboAlign3.Dock")));
			this.cboAlign3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboAlign3.Enabled = ((bool)(resources.GetObject("cboAlign3.Enabled")));
			this.cboAlign3.Font = ((System.Drawing.Font)(resources.GetObject("cboAlign3.Font")));
			this.cboAlign3.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("cboAlign3.ImeMode")));
			this.cboAlign3.IntegralHeight = ((bool)(resources.GetObject("cboAlign3.IntegralHeight")));
			this.cboAlign3.ItemHeight = ((int)(resources.GetObject("cboAlign3.ItemHeight")));
			this.cboAlign3.Items.AddRange(new object[] {
														   resources.GetString("cboAlign3.Items"),
														   resources.GetString("cboAlign3.Items1"),
														   resources.GetString("cboAlign3.Items2")});
			this.cboAlign3.Location = ((System.Drawing.Point)(resources.GetObject("cboAlign3.Location")));
			this.cboAlign3.MaxDropDownItems = ((int)(resources.GetObject("cboAlign3.MaxDropDownItems")));
			this.cboAlign3.MaxLength = ((int)(resources.GetObject("cboAlign3.MaxLength")));
			this.cboAlign3.Name = "cboAlign3";
			this.cboAlign3.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("cboAlign3.RightToLeft")));
			this.cboAlign3.Size = ((System.Drawing.Size)(resources.GetObject("cboAlign3.Size")));
			this.cboAlign3.TabIndex = ((int)(resources.GetObject("cboAlign3.TabIndex")));
			this.cboAlign3.Text = resources.GetString("cboAlign3.Text");
			this.cboAlign3.Visible = ((bool)(resources.GetObject("cboAlign3.Visible")));
			// 
			// lblFormat2
			// 
			this.lblFormat2.AccessibleDescription = resources.GetString("lblFormat2.AccessibleDescription");
			this.lblFormat2.AccessibleName = resources.GetString("lblFormat2.AccessibleName");
			this.lblFormat2.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblFormat2.Anchor")));
			this.lblFormat2.AutoSize = ((bool)(resources.GetObject("lblFormat2.AutoSize")));
			this.lblFormat2.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblFormat2.Dock")));
			this.lblFormat2.Enabled = ((bool)(resources.GetObject("lblFormat2.Enabled")));
			this.lblFormat2.Font = ((System.Drawing.Font)(resources.GetObject("lblFormat2.Font")));
			this.lblFormat2.Image = ((System.Drawing.Image)(resources.GetObject("lblFormat2.Image")));
			this.lblFormat2.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblFormat2.ImageAlign")));
			this.lblFormat2.ImageIndex = ((int)(resources.GetObject("lblFormat2.ImageIndex")));
			this.lblFormat2.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblFormat2.ImeMode")));
			this.lblFormat2.Location = ((System.Drawing.Point)(resources.GetObject("lblFormat2.Location")));
			this.lblFormat2.Name = "lblFormat2";
			this.lblFormat2.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblFormat2.RightToLeft")));
			this.lblFormat2.Size = ((System.Drawing.Size)(resources.GetObject("lblFormat2.Size")));
			this.lblFormat2.TabIndex = ((int)(resources.GetObject("lblFormat2.TabIndex")));
			this.lblFormat2.Text = resources.GetString("lblFormat2.Text");
			this.lblFormat2.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblFormat2.TextAlign")));
			this.lblFormat2.Visible = ((bool)(resources.GetObject("lblFormat2.Visible")));
			// 
			// lblAlign2
			// 
			this.lblAlign2.AccessibleDescription = resources.GetString("lblAlign2.AccessibleDescription");
			this.lblAlign2.AccessibleName = resources.GetString("lblAlign2.AccessibleName");
			this.lblAlign2.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblAlign2.Anchor")));
			this.lblAlign2.AutoSize = ((bool)(resources.GetObject("lblAlign2.AutoSize")));
			this.lblAlign2.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblAlign2.Dock")));
			this.lblAlign2.Enabled = ((bool)(resources.GetObject("lblAlign2.Enabled")));
			this.lblAlign2.Font = ((System.Drawing.Font)(resources.GetObject("lblAlign2.Font")));
			this.lblAlign2.ForeColor = System.Drawing.Color.Black;
			this.lblAlign2.Image = ((System.Drawing.Image)(resources.GetObject("lblAlign2.Image")));
			this.lblAlign2.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblAlign2.ImageAlign")));
			this.lblAlign2.ImageIndex = ((int)(resources.GetObject("lblAlign2.ImageIndex")));
			this.lblAlign2.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblAlign2.ImeMode")));
			this.lblAlign2.Location = ((System.Drawing.Point)(resources.GetObject("lblAlign2.Location")));
			this.lblAlign2.Name = "lblAlign2";
			this.lblAlign2.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblAlign2.RightToLeft")));
			this.lblAlign2.Size = ((System.Drawing.Size)(resources.GetObject("lblAlign2.Size")));
			this.lblAlign2.TabIndex = ((int)(resources.GetObject("lblAlign2.TabIndex")));
			this.lblAlign2.Text = resources.GetString("lblAlign2.Text");
			this.lblAlign2.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblAlign2.TextAlign")));
			this.lblAlign2.Visible = ((bool)(resources.GetObject("lblAlign2.Visible")));
			// 
			// lblWidth2
			// 
			this.lblWidth2.AccessibleDescription = resources.GetString("lblWidth2.AccessibleDescription");
			this.lblWidth2.AccessibleName = resources.GetString("lblWidth2.AccessibleName");
			this.lblWidth2.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblWidth2.Anchor")));
			this.lblWidth2.AutoSize = ((bool)(resources.GetObject("lblWidth2.AutoSize")));
			this.lblWidth2.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblWidth2.Dock")));
			this.lblWidth2.Enabled = ((bool)(resources.GetObject("lblWidth2.Enabled")));
			this.lblWidth2.Font = ((System.Drawing.Font)(resources.GetObject("lblWidth2.Font")));
			this.lblWidth2.Image = ((System.Drawing.Image)(resources.GetObject("lblWidth2.Image")));
			this.lblWidth2.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblWidth2.ImageAlign")));
			this.lblWidth2.ImageIndex = ((int)(resources.GetObject("lblWidth2.ImageIndex")));
			this.lblWidth2.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblWidth2.ImeMode")));
			this.lblWidth2.Location = ((System.Drawing.Point)(resources.GetObject("lblWidth2.Location")));
			this.lblWidth2.Name = "lblWidth2";
			this.lblWidth2.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblWidth2.RightToLeft")));
			this.lblWidth2.Size = ((System.Drawing.Size)(resources.GetObject("lblWidth2.Size")));
			this.lblWidth2.TabIndex = ((int)(resources.GetObject("lblWidth2.TabIndex")));
			this.lblWidth2.Text = resources.GetString("lblWidth2.Text");
			this.lblWidth2.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblWidth2.TextAlign")));
			this.lblWidth2.Visible = ((bool)(resources.GetObject("lblWidth2.Visible")));
			// 
			// lblEnglish2
			// 
			this.lblEnglish2.AccessibleDescription = resources.GetString("lblEnglish2.AccessibleDescription");
			this.lblEnglish2.AccessibleName = resources.GetString("lblEnglish2.AccessibleName");
			this.lblEnglish2.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblEnglish2.Anchor")));
			this.lblEnglish2.AutoSize = ((bool)(resources.GetObject("lblEnglish2.AutoSize")));
			this.lblEnglish2.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblEnglish2.Dock")));
			this.lblEnglish2.Enabled = ((bool)(resources.GetObject("lblEnglish2.Enabled")));
			this.lblEnglish2.Font = ((System.Drawing.Font)(resources.GetObject("lblEnglish2.Font")));
			this.lblEnglish2.Image = ((System.Drawing.Image)(resources.GetObject("lblEnglish2.Image")));
			this.lblEnglish2.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblEnglish2.ImageAlign")));
			this.lblEnglish2.ImageIndex = ((int)(resources.GetObject("lblEnglish2.ImageIndex")));
			this.lblEnglish2.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblEnglish2.ImeMode")));
			this.lblEnglish2.Location = ((System.Drawing.Point)(resources.GetObject("lblEnglish2.Location")));
			this.lblEnglish2.Name = "lblEnglish2";
			this.lblEnglish2.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblEnglish2.RightToLeft")));
			this.lblEnglish2.Size = ((System.Drawing.Size)(resources.GetObject("lblEnglish2.Size")));
			this.lblEnglish2.TabIndex = ((int)(resources.GetObject("lblEnglish2.TabIndex")));
			this.lblEnglish2.Text = resources.GetString("lblEnglish2.Text");
			this.lblEnglish2.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblEnglish2.TextAlign")));
			this.lblEnglish2.Visible = ((bool)(resources.GetObject("lblEnglish2.Visible")));
			// 
			// lblJapanese2
			// 
			this.lblJapanese2.AccessibleDescription = resources.GetString("lblJapanese2.AccessibleDescription");
			this.lblJapanese2.AccessibleName = resources.GetString("lblJapanese2.AccessibleName");
			this.lblJapanese2.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblJapanese2.Anchor")));
			this.lblJapanese2.AutoSize = ((bool)(resources.GetObject("lblJapanese2.AutoSize")));
			this.lblJapanese2.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblJapanese2.Dock")));
			this.lblJapanese2.Enabled = ((bool)(resources.GetObject("lblJapanese2.Enabled")));
			this.lblJapanese2.Font = ((System.Drawing.Font)(resources.GetObject("lblJapanese2.Font")));
			this.lblJapanese2.Image = ((System.Drawing.Image)(resources.GetObject("lblJapanese2.Image")));
			this.lblJapanese2.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblJapanese2.ImageAlign")));
			this.lblJapanese2.ImageIndex = ((int)(resources.GetObject("lblJapanese2.ImageIndex")));
			this.lblJapanese2.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblJapanese2.ImeMode")));
			this.lblJapanese2.Location = ((System.Drawing.Point)(resources.GetObject("lblJapanese2.Location")));
			this.lblJapanese2.Name = "lblJapanese2";
			this.lblJapanese2.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblJapanese2.RightToLeft")));
			this.lblJapanese2.Size = ((System.Drawing.Size)(resources.GetObject("lblJapanese2.Size")));
			this.lblJapanese2.TabIndex = ((int)(resources.GetObject("lblJapanese2.TabIndex")));
			this.lblJapanese2.Text = resources.GetString("lblJapanese2.Text");
			this.lblJapanese2.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblJapanese2.TextAlign")));
			this.lblJapanese2.Visible = ((bool)(resources.GetObject("lblJapanese2.Visible")));
			// 
			// lblVietnamese2
			// 
			this.lblVietnamese2.AccessibleDescription = resources.GetString("lblVietnamese2.AccessibleDescription");
			this.lblVietnamese2.AccessibleName = resources.GetString("lblVietnamese2.AccessibleName");
			this.lblVietnamese2.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblVietnamese2.Anchor")));
			this.lblVietnamese2.AutoSize = ((bool)(resources.GetObject("lblVietnamese2.AutoSize")));
			this.lblVietnamese2.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblVietnamese2.Dock")));
			this.lblVietnamese2.Enabled = ((bool)(resources.GetObject("lblVietnamese2.Enabled")));
			this.lblVietnamese2.Font = ((System.Drawing.Font)(resources.GetObject("lblVietnamese2.Font")));
			this.lblVietnamese2.Image = ((System.Drawing.Image)(resources.GetObject("lblVietnamese2.Image")));
			this.lblVietnamese2.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblVietnamese2.ImageAlign")));
			this.lblVietnamese2.ImageIndex = ((int)(resources.GetObject("lblVietnamese2.ImageIndex")));
			this.lblVietnamese2.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblVietnamese2.ImeMode")));
			this.lblVietnamese2.Location = ((System.Drawing.Point)(resources.GetObject("lblVietnamese2.Location")));
			this.lblVietnamese2.Name = "lblVietnamese2";
			this.lblVietnamese2.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblVietnamese2.RightToLeft")));
			this.lblVietnamese2.Size = ((System.Drawing.Size)(resources.GetObject("lblVietnamese2.Size")));
			this.lblVietnamese2.TabIndex = ((int)(resources.GetObject("lblVietnamese2.TabIndex")));
			this.lblVietnamese2.Text = resources.GetString("lblVietnamese2.Text");
			this.lblVietnamese2.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblVietnamese2.TextAlign")));
			this.lblVietnamese2.Visible = ((bool)(resources.GetObject("lblVietnamese2.Visible")));
			// 
			// txtVietnamese2
			// 
			this.txtVietnamese2.AccessibleDescription = resources.GetString("txtVietnamese2.AccessibleDescription");
			this.txtVietnamese2.AccessibleName = resources.GetString("txtVietnamese2.AccessibleName");
			this.txtVietnamese2.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("txtVietnamese2.Anchor")));
			this.txtVietnamese2.AutoSize = ((bool)(resources.GetObject("txtVietnamese2.AutoSize")));
			this.txtVietnamese2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("txtVietnamese2.BackgroundImage")));
			this.txtVietnamese2.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("txtVietnamese2.Dock")));
			this.txtVietnamese2.Enabled = ((bool)(resources.GetObject("txtVietnamese2.Enabled")));
			this.txtVietnamese2.Font = ((System.Drawing.Font)(resources.GetObject("txtVietnamese2.Font")));
			this.txtVietnamese2.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("txtVietnamese2.ImeMode")));
			this.txtVietnamese2.Location = ((System.Drawing.Point)(resources.GetObject("txtVietnamese2.Location")));
			this.txtVietnamese2.MaxLength = ((int)(resources.GetObject("txtVietnamese2.MaxLength")));
			this.txtVietnamese2.Multiline = ((bool)(resources.GetObject("txtVietnamese2.Multiline")));
			this.txtVietnamese2.Name = "txtVietnamese2";
			this.txtVietnamese2.PasswordChar = ((char)(resources.GetObject("txtVietnamese2.PasswordChar")));
			this.txtVietnamese2.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("txtVietnamese2.RightToLeft")));
			this.txtVietnamese2.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("txtVietnamese2.ScrollBars")));
			this.txtVietnamese2.Size = ((System.Drawing.Size)(resources.GetObject("txtVietnamese2.Size")));
			this.txtVietnamese2.TabIndex = ((int)(resources.GetObject("txtVietnamese2.TabIndex")));
			this.txtVietnamese2.Text = resources.GetString("txtVietnamese2.Text");
			this.txtVietnamese2.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("txtVietnamese2.TextAlign")));
			this.txtVietnamese2.Visible = ((bool)(resources.GetObject("txtVietnamese2.Visible")));
			this.txtVietnamese2.WordWrap = ((bool)(resources.GetObject("txtVietnamese2.WordWrap")));
			// 
			// txtEnglish2
			// 
			this.txtEnglish2.AccessibleDescription = resources.GetString("txtEnglish2.AccessibleDescription");
			this.txtEnglish2.AccessibleName = resources.GetString("txtEnglish2.AccessibleName");
			this.txtEnglish2.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("txtEnglish2.Anchor")));
			this.txtEnglish2.AutoSize = ((bool)(resources.GetObject("txtEnglish2.AutoSize")));
			this.txtEnglish2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("txtEnglish2.BackgroundImage")));
			this.txtEnglish2.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("txtEnglish2.Dock")));
			this.txtEnglish2.Enabled = ((bool)(resources.GetObject("txtEnglish2.Enabled")));
			this.txtEnglish2.Font = ((System.Drawing.Font)(resources.GetObject("txtEnglish2.Font")));
			this.txtEnglish2.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("txtEnglish2.ImeMode")));
			this.txtEnglish2.Location = ((System.Drawing.Point)(resources.GetObject("txtEnglish2.Location")));
			this.txtEnglish2.MaxLength = ((int)(resources.GetObject("txtEnglish2.MaxLength")));
			this.txtEnglish2.Multiline = ((bool)(resources.GetObject("txtEnglish2.Multiline")));
			this.txtEnglish2.Name = "txtEnglish2";
			this.txtEnglish2.PasswordChar = ((char)(resources.GetObject("txtEnglish2.PasswordChar")));
			this.txtEnglish2.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("txtEnglish2.RightToLeft")));
			this.txtEnglish2.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("txtEnglish2.ScrollBars")));
			this.txtEnglish2.Size = ((System.Drawing.Size)(resources.GetObject("txtEnglish2.Size")));
			this.txtEnglish2.TabIndex = ((int)(resources.GetObject("txtEnglish2.TabIndex")));
			this.txtEnglish2.Text = resources.GetString("txtEnglish2.Text");
			this.txtEnglish2.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("txtEnglish2.TextAlign")));
			this.txtEnglish2.Visible = ((bool)(resources.GetObject("txtEnglish2.Visible")));
			this.txtEnglish2.WordWrap = ((bool)(resources.GetObject("txtEnglish2.WordWrap")));
			// 
			// txtFormat2
			// 
			this.txtFormat2.AccessibleDescription = resources.GetString("txtFormat2.AccessibleDescription");
			this.txtFormat2.AccessibleName = resources.GetString("txtFormat2.AccessibleName");
			this.txtFormat2.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("txtFormat2.Anchor")));
			this.txtFormat2.AutoSize = ((bool)(resources.GetObject("txtFormat2.AutoSize")));
			this.txtFormat2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("txtFormat2.BackgroundImage")));
			this.txtFormat2.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("txtFormat2.Dock")));
			this.txtFormat2.Enabled = ((bool)(resources.GetObject("txtFormat2.Enabled")));
			this.txtFormat2.Font = ((System.Drawing.Font)(resources.GetObject("txtFormat2.Font")));
			this.txtFormat2.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("txtFormat2.ImeMode")));
			this.txtFormat2.Location = ((System.Drawing.Point)(resources.GetObject("txtFormat2.Location")));
			this.txtFormat2.MaxLength = ((int)(resources.GetObject("txtFormat2.MaxLength")));
			this.txtFormat2.Multiline = ((bool)(resources.GetObject("txtFormat2.Multiline")));
			this.txtFormat2.Name = "txtFormat2";
			this.txtFormat2.PasswordChar = ((char)(resources.GetObject("txtFormat2.PasswordChar")));
			this.txtFormat2.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("txtFormat2.RightToLeft")));
			this.txtFormat2.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("txtFormat2.ScrollBars")));
			this.txtFormat2.Size = ((System.Drawing.Size)(resources.GetObject("txtFormat2.Size")));
			this.txtFormat2.TabIndex = ((int)(resources.GetObject("txtFormat2.TabIndex")));
			this.txtFormat2.Text = resources.GetString("txtFormat2.Text");
			this.txtFormat2.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("txtFormat2.TextAlign")));
			this.txtFormat2.Visible = ((bool)(resources.GetObject("txtFormat2.Visible")));
			this.txtFormat2.WordWrap = ((bool)(resources.GetObject("txtFormat2.WordWrap")));
			// 
			// txtJapanese2
			// 
			this.txtJapanese2.AccessibleDescription = resources.GetString("txtJapanese2.AccessibleDescription");
			this.txtJapanese2.AccessibleName = resources.GetString("txtJapanese2.AccessibleName");
			this.txtJapanese2.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("txtJapanese2.Anchor")));
			this.txtJapanese2.AutoSize = ((bool)(resources.GetObject("txtJapanese2.AutoSize")));
			this.txtJapanese2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("txtJapanese2.BackgroundImage")));
			this.txtJapanese2.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("txtJapanese2.Dock")));
			this.txtJapanese2.Enabled = ((bool)(resources.GetObject("txtJapanese2.Enabled")));
			this.txtJapanese2.Font = ((System.Drawing.Font)(resources.GetObject("txtJapanese2.Font")));
			this.txtJapanese2.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("txtJapanese2.ImeMode")));
			this.txtJapanese2.Location = ((System.Drawing.Point)(resources.GetObject("txtJapanese2.Location")));
			this.txtJapanese2.MaxLength = ((int)(resources.GetObject("txtJapanese2.MaxLength")));
			this.txtJapanese2.Multiline = ((bool)(resources.GetObject("txtJapanese2.Multiline")));
			this.txtJapanese2.Name = "txtJapanese2";
			this.txtJapanese2.PasswordChar = ((char)(resources.GetObject("txtJapanese2.PasswordChar")));
			this.txtJapanese2.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("txtJapanese2.RightToLeft")));
			this.txtJapanese2.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("txtJapanese2.ScrollBars")));
			this.txtJapanese2.Size = ((System.Drawing.Size)(resources.GetObject("txtJapanese2.Size")));
			this.txtJapanese2.TabIndex = ((int)(resources.GetObject("txtJapanese2.TabIndex")));
			this.txtJapanese2.Text = resources.GetString("txtJapanese2.Text");
			this.txtJapanese2.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("txtJapanese2.TextAlign")));
			this.txtJapanese2.Visible = ((bool)(resources.GetObject("txtJapanese2.Visible")));
			this.txtJapanese2.WordWrap = ((bool)(resources.GetObject("txtJapanese2.WordWrap")));
			// 
			// cboAlign2
			// 
			this.cboAlign2.AccessibleDescription = resources.GetString("cboAlign2.AccessibleDescription");
			this.cboAlign2.AccessibleName = resources.GetString("cboAlign2.AccessibleName");
			this.cboAlign2.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("cboAlign2.Anchor")));
			this.cboAlign2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cboAlign2.BackgroundImage")));
			this.cboAlign2.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("cboAlign2.Dock")));
			this.cboAlign2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboAlign2.Enabled = ((bool)(resources.GetObject("cboAlign2.Enabled")));
			this.cboAlign2.Font = ((System.Drawing.Font)(resources.GetObject("cboAlign2.Font")));
			this.cboAlign2.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("cboAlign2.ImeMode")));
			this.cboAlign2.IntegralHeight = ((bool)(resources.GetObject("cboAlign2.IntegralHeight")));
			this.cboAlign2.ItemHeight = ((int)(resources.GetObject("cboAlign2.ItemHeight")));
			this.cboAlign2.Items.AddRange(new object[] {
														   resources.GetString("cboAlign2.Items"),
														   resources.GetString("cboAlign2.Items1"),
														   resources.GetString("cboAlign2.Items2")});
			this.cboAlign2.Location = ((System.Drawing.Point)(resources.GetObject("cboAlign2.Location")));
			this.cboAlign2.MaxDropDownItems = ((int)(resources.GetObject("cboAlign2.MaxDropDownItems")));
			this.cboAlign2.MaxLength = ((int)(resources.GetObject("cboAlign2.MaxLength")));
			this.cboAlign2.Name = "cboAlign2";
			this.cboAlign2.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("cboAlign2.RightToLeft")));
			this.cboAlign2.Size = ((System.Drawing.Size)(resources.GetObject("cboAlign2.Size")));
			this.cboAlign2.TabIndex = ((int)(resources.GetObject("cboAlign2.TabIndex")));
			this.cboAlign2.Text = resources.GetString("cboAlign2.Text");
			this.cboAlign2.Visible = ((bool)(resources.GetObject("cboAlign2.Visible")));
			// 
			// lblFormat1
			// 
			this.lblFormat1.AccessibleDescription = resources.GetString("lblFormat1.AccessibleDescription");
			this.lblFormat1.AccessibleName = resources.GetString("lblFormat1.AccessibleName");
			this.lblFormat1.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblFormat1.Anchor")));
			this.lblFormat1.AutoSize = ((bool)(resources.GetObject("lblFormat1.AutoSize")));
			this.lblFormat1.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblFormat1.Dock")));
			this.lblFormat1.Enabled = ((bool)(resources.GetObject("lblFormat1.Enabled")));
			this.lblFormat1.Font = ((System.Drawing.Font)(resources.GetObject("lblFormat1.Font")));
			this.lblFormat1.Image = ((System.Drawing.Image)(resources.GetObject("lblFormat1.Image")));
			this.lblFormat1.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblFormat1.ImageAlign")));
			this.lblFormat1.ImageIndex = ((int)(resources.GetObject("lblFormat1.ImageIndex")));
			this.lblFormat1.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblFormat1.ImeMode")));
			this.lblFormat1.Location = ((System.Drawing.Point)(resources.GetObject("lblFormat1.Location")));
			this.lblFormat1.Name = "lblFormat1";
			this.lblFormat1.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblFormat1.RightToLeft")));
			this.lblFormat1.Size = ((System.Drawing.Size)(resources.GetObject("lblFormat1.Size")));
			this.lblFormat1.TabIndex = ((int)(resources.GetObject("lblFormat1.TabIndex")));
			this.lblFormat1.Text = resources.GetString("lblFormat1.Text");
			this.lblFormat1.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblFormat1.TextAlign")));
			this.lblFormat1.Visible = ((bool)(resources.GetObject("lblFormat1.Visible")));
			// 
			// lblAlign1
			// 
			this.lblAlign1.AccessibleDescription = resources.GetString("lblAlign1.AccessibleDescription");
			this.lblAlign1.AccessibleName = resources.GetString("lblAlign1.AccessibleName");
			this.lblAlign1.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblAlign1.Anchor")));
			this.lblAlign1.AutoSize = ((bool)(resources.GetObject("lblAlign1.AutoSize")));
			this.lblAlign1.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblAlign1.Dock")));
			this.lblAlign1.Enabled = ((bool)(resources.GetObject("lblAlign1.Enabled")));
			this.lblAlign1.Font = ((System.Drawing.Font)(resources.GetObject("lblAlign1.Font")));
			this.lblAlign1.ForeColor = System.Drawing.Color.Black;
			this.lblAlign1.Image = ((System.Drawing.Image)(resources.GetObject("lblAlign1.Image")));
			this.lblAlign1.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblAlign1.ImageAlign")));
			this.lblAlign1.ImageIndex = ((int)(resources.GetObject("lblAlign1.ImageIndex")));
			this.lblAlign1.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblAlign1.ImeMode")));
			this.lblAlign1.Location = ((System.Drawing.Point)(resources.GetObject("lblAlign1.Location")));
			this.lblAlign1.Name = "lblAlign1";
			this.lblAlign1.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblAlign1.RightToLeft")));
			this.lblAlign1.Size = ((System.Drawing.Size)(resources.GetObject("lblAlign1.Size")));
			this.lblAlign1.TabIndex = ((int)(resources.GetObject("lblAlign1.TabIndex")));
			this.lblAlign1.Text = resources.GetString("lblAlign1.Text");
			this.lblAlign1.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblAlign1.TextAlign")));
			this.lblAlign1.Visible = ((bool)(resources.GetObject("lblAlign1.Visible")));
			// 
			// lblWidth1
			// 
			this.lblWidth1.AccessibleDescription = resources.GetString("lblWidth1.AccessibleDescription");
			this.lblWidth1.AccessibleName = resources.GetString("lblWidth1.AccessibleName");
			this.lblWidth1.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblWidth1.Anchor")));
			this.lblWidth1.AutoSize = ((bool)(resources.GetObject("lblWidth1.AutoSize")));
			this.lblWidth1.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblWidth1.Dock")));
			this.lblWidth1.Enabled = ((bool)(resources.GetObject("lblWidth1.Enabled")));
			this.lblWidth1.Font = ((System.Drawing.Font)(resources.GetObject("lblWidth1.Font")));
			this.lblWidth1.Image = ((System.Drawing.Image)(resources.GetObject("lblWidth1.Image")));
			this.lblWidth1.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblWidth1.ImageAlign")));
			this.lblWidth1.ImageIndex = ((int)(resources.GetObject("lblWidth1.ImageIndex")));
			this.lblWidth1.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblWidth1.ImeMode")));
			this.lblWidth1.Location = ((System.Drawing.Point)(resources.GetObject("lblWidth1.Location")));
			this.lblWidth1.Name = "lblWidth1";
			this.lblWidth1.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblWidth1.RightToLeft")));
			this.lblWidth1.Size = ((System.Drawing.Size)(resources.GetObject("lblWidth1.Size")));
			this.lblWidth1.TabIndex = ((int)(resources.GetObject("lblWidth1.TabIndex")));
			this.lblWidth1.Text = resources.GetString("lblWidth1.Text");
			this.lblWidth1.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblWidth1.TextAlign")));
			this.lblWidth1.Visible = ((bool)(resources.GetObject("lblWidth1.Visible")));
			// 
			// lblEnglish1
			// 
			this.lblEnglish1.AccessibleDescription = resources.GetString("lblEnglish1.AccessibleDescription");
			this.lblEnglish1.AccessibleName = resources.GetString("lblEnglish1.AccessibleName");
			this.lblEnglish1.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblEnglish1.Anchor")));
			this.lblEnglish1.AutoSize = ((bool)(resources.GetObject("lblEnglish1.AutoSize")));
			this.lblEnglish1.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblEnglish1.Dock")));
			this.lblEnglish1.Enabled = ((bool)(resources.GetObject("lblEnglish1.Enabled")));
			this.lblEnglish1.Font = ((System.Drawing.Font)(resources.GetObject("lblEnglish1.Font")));
			this.lblEnglish1.Image = ((System.Drawing.Image)(resources.GetObject("lblEnglish1.Image")));
			this.lblEnglish1.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblEnglish1.ImageAlign")));
			this.lblEnglish1.ImageIndex = ((int)(resources.GetObject("lblEnglish1.ImageIndex")));
			this.lblEnglish1.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblEnglish1.ImeMode")));
			this.lblEnglish1.Location = ((System.Drawing.Point)(resources.GetObject("lblEnglish1.Location")));
			this.lblEnglish1.Name = "lblEnglish1";
			this.lblEnglish1.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblEnglish1.RightToLeft")));
			this.lblEnglish1.Size = ((System.Drawing.Size)(resources.GetObject("lblEnglish1.Size")));
			this.lblEnglish1.TabIndex = ((int)(resources.GetObject("lblEnglish1.TabIndex")));
			this.lblEnglish1.Text = resources.GetString("lblEnglish1.Text");
			this.lblEnglish1.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblEnglish1.TextAlign")));
			this.lblEnglish1.Visible = ((bool)(resources.GetObject("lblEnglish1.Visible")));
			// 
			// lblJapanese1
			// 
			this.lblJapanese1.AccessibleDescription = resources.GetString("lblJapanese1.AccessibleDescription");
			this.lblJapanese1.AccessibleName = resources.GetString("lblJapanese1.AccessibleName");
			this.lblJapanese1.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblJapanese1.Anchor")));
			this.lblJapanese1.AutoSize = ((bool)(resources.GetObject("lblJapanese1.AutoSize")));
			this.lblJapanese1.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblJapanese1.Dock")));
			this.lblJapanese1.Enabled = ((bool)(resources.GetObject("lblJapanese1.Enabled")));
			this.lblJapanese1.Font = ((System.Drawing.Font)(resources.GetObject("lblJapanese1.Font")));
			this.lblJapanese1.Image = ((System.Drawing.Image)(resources.GetObject("lblJapanese1.Image")));
			this.lblJapanese1.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblJapanese1.ImageAlign")));
			this.lblJapanese1.ImageIndex = ((int)(resources.GetObject("lblJapanese1.ImageIndex")));
			this.lblJapanese1.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblJapanese1.ImeMode")));
			this.lblJapanese1.Location = ((System.Drawing.Point)(resources.GetObject("lblJapanese1.Location")));
			this.lblJapanese1.Name = "lblJapanese1";
			this.lblJapanese1.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblJapanese1.RightToLeft")));
			this.lblJapanese1.Size = ((System.Drawing.Size)(resources.GetObject("lblJapanese1.Size")));
			this.lblJapanese1.TabIndex = ((int)(resources.GetObject("lblJapanese1.TabIndex")));
			this.lblJapanese1.Text = resources.GetString("lblJapanese1.Text");
			this.lblJapanese1.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblJapanese1.TextAlign")));
			this.lblJapanese1.Visible = ((bool)(resources.GetObject("lblJapanese1.Visible")));
			// 
			// lblVietnamese1
			// 
			this.lblVietnamese1.AccessibleDescription = resources.GetString("lblVietnamese1.AccessibleDescription");
			this.lblVietnamese1.AccessibleName = resources.GetString("lblVietnamese1.AccessibleName");
			this.lblVietnamese1.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblVietnamese1.Anchor")));
			this.lblVietnamese1.AutoSize = ((bool)(resources.GetObject("lblVietnamese1.AutoSize")));
			this.lblVietnamese1.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblVietnamese1.Dock")));
			this.lblVietnamese1.Enabled = ((bool)(resources.GetObject("lblVietnamese1.Enabled")));
			this.lblVietnamese1.Font = ((System.Drawing.Font)(resources.GetObject("lblVietnamese1.Font")));
			this.lblVietnamese1.Image = ((System.Drawing.Image)(resources.GetObject("lblVietnamese1.Image")));
			this.lblVietnamese1.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblVietnamese1.ImageAlign")));
			this.lblVietnamese1.ImageIndex = ((int)(resources.GetObject("lblVietnamese1.ImageIndex")));
			this.lblVietnamese1.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblVietnamese1.ImeMode")));
			this.lblVietnamese1.Location = ((System.Drawing.Point)(resources.GetObject("lblVietnamese1.Location")));
			this.lblVietnamese1.Name = "lblVietnamese1";
			this.lblVietnamese1.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblVietnamese1.RightToLeft")));
			this.lblVietnamese1.Size = ((System.Drawing.Size)(resources.GetObject("lblVietnamese1.Size")));
			this.lblVietnamese1.TabIndex = ((int)(resources.GetObject("lblVietnamese1.TabIndex")));
			this.lblVietnamese1.Text = resources.GetString("lblVietnamese1.Text");
			this.lblVietnamese1.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblVietnamese1.TextAlign")));
			this.lblVietnamese1.Visible = ((bool)(resources.GetObject("lblVietnamese1.Visible")));
			// 
			// txtVietnamese1
			// 
			this.txtVietnamese1.AccessibleDescription = resources.GetString("txtVietnamese1.AccessibleDescription");
			this.txtVietnamese1.AccessibleName = resources.GetString("txtVietnamese1.AccessibleName");
			this.txtVietnamese1.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("txtVietnamese1.Anchor")));
			this.txtVietnamese1.AutoSize = ((bool)(resources.GetObject("txtVietnamese1.AutoSize")));
			this.txtVietnamese1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("txtVietnamese1.BackgroundImage")));
			this.txtVietnamese1.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("txtVietnamese1.Dock")));
			this.txtVietnamese1.Enabled = ((bool)(resources.GetObject("txtVietnamese1.Enabled")));
			this.txtVietnamese1.Font = ((System.Drawing.Font)(resources.GetObject("txtVietnamese1.Font")));
			this.txtVietnamese1.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("txtVietnamese1.ImeMode")));
			this.txtVietnamese1.Location = ((System.Drawing.Point)(resources.GetObject("txtVietnamese1.Location")));
			this.txtVietnamese1.MaxLength = ((int)(resources.GetObject("txtVietnamese1.MaxLength")));
			this.txtVietnamese1.Multiline = ((bool)(resources.GetObject("txtVietnamese1.Multiline")));
			this.txtVietnamese1.Name = "txtVietnamese1";
			this.txtVietnamese1.PasswordChar = ((char)(resources.GetObject("txtVietnamese1.PasswordChar")));
			this.txtVietnamese1.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("txtVietnamese1.RightToLeft")));
			this.txtVietnamese1.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("txtVietnamese1.ScrollBars")));
			this.txtVietnamese1.Size = ((System.Drawing.Size)(resources.GetObject("txtVietnamese1.Size")));
			this.txtVietnamese1.TabIndex = ((int)(resources.GetObject("txtVietnamese1.TabIndex")));
			this.txtVietnamese1.Text = resources.GetString("txtVietnamese1.Text");
			this.txtVietnamese1.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("txtVietnamese1.TextAlign")));
			this.txtVietnamese1.Visible = ((bool)(resources.GetObject("txtVietnamese1.Visible")));
			this.txtVietnamese1.WordWrap = ((bool)(resources.GetObject("txtVietnamese1.WordWrap")));
			// 
			// txtEnglish1
			// 
			this.txtEnglish1.AccessibleDescription = resources.GetString("txtEnglish1.AccessibleDescription");
			this.txtEnglish1.AccessibleName = resources.GetString("txtEnglish1.AccessibleName");
			this.txtEnglish1.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("txtEnglish1.Anchor")));
			this.txtEnglish1.AutoSize = ((bool)(resources.GetObject("txtEnglish1.AutoSize")));
			this.txtEnglish1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("txtEnglish1.BackgroundImage")));
			this.txtEnglish1.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("txtEnglish1.Dock")));
			this.txtEnglish1.Enabled = ((bool)(resources.GetObject("txtEnglish1.Enabled")));
			this.txtEnglish1.Font = ((System.Drawing.Font)(resources.GetObject("txtEnglish1.Font")));
			this.txtEnglish1.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("txtEnglish1.ImeMode")));
			this.txtEnglish1.Location = ((System.Drawing.Point)(resources.GetObject("txtEnglish1.Location")));
			this.txtEnglish1.MaxLength = ((int)(resources.GetObject("txtEnglish1.MaxLength")));
			this.txtEnglish1.Multiline = ((bool)(resources.GetObject("txtEnglish1.Multiline")));
			this.txtEnglish1.Name = "txtEnglish1";
			this.txtEnglish1.PasswordChar = ((char)(resources.GetObject("txtEnglish1.PasswordChar")));
			this.txtEnglish1.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("txtEnglish1.RightToLeft")));
			this.txtEnglish1.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("txtEnglish1.ScrollBars")));
			this.txtEnglish1.Size = ((System.Drawing.Size)(resources.GetObject("txtEnglish1.Size")));
			this.txtEnglish1.TabIndex = ((int)(resources.GetObject("txtEnglish1.TabIndex")));
			this.txtEnglish1.Text = resources.GetString("txtEnglish1.Text");
			this.txtEnglish1.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("txtEnglish1.TextAlign")));
			this.txtEnglish1.Visible = ((bool)(resources.GetObject("txtEnglish1.Visible")));
			this.txtEnglish1.WordWrap = ((bool)(resources.GetObject("txtEnglish1.WordWrap")));
			// 
			// txtFormat1
			// 
			this.txtFormat1.AccessibleDescription = resources.GetString("txtFormat1.AccessibleDescription");
			this.txtFormat1.AccessibleName = resources.GetString("txtFormat1.AccessibleName");
			this.txtFormat1.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("txtFormat1.Anchor")));
			this.txtFormat1.AutoSize = ((bool)(resources.GetObject("txtFormat1.AutoSize")));
			this.txtFormat1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("txtFormat1.BackgroundImage")));
			this.txtFormat1.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("txtFormat1.Dock")));
			this.txtFormat1.Enabled = ((bool)(resources.GetObject("txtFormat1.Enabled")));
			this.txtFormat1.Font = ((System.Drawing.Font)(resources.GetObject("txtFormat1.Font")));
			this.txtFormat1.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("txtFormat1.ImeMode")));
			this.txtFormat1.Location = ((System.Drawing.Point)(resources.GetObject("txtFormat1.Location")));
			this.txtFormat1.MaxLength = ((int)(resources.GetObject("txtFormat1.MaxLength")));
			this.txtFormat1.Multiline = ((bool)(resources.GetObject("txtFormat1.Multiline")));
			this.txtFormat1.Name = "txtFormat1";
			this.txtFormat1.PasswordChar = ((char)(resources.GetObject("txtFormat1.PasswordChar")));
			this.txtFormat1.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("txtFormat1.RightToLeft")));
			this.txtFormat1.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("txtFormat1.ScrollBars")));
			this.txtFormat1.Size = ((System.Drawing.Size)(resources.GetObject("txtFormat1.Size")));
			this.txtFormat1.TabIndex = ((int)(resources.GetObject("txtFormat1.TabIndex")));
			this.txtFormat1.Text = resources.GetString("txtFormat1.Text");
			this.txtFormat1.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("txtFormat1.TextAlign")));
			this.txtFormat1.Visible = ((bool)(resources.GetObject("txtFormat1.Visible")));
			this.txtFormat1.WordWrap = ((bool)(resources.GetObject("txtFormat1.WordWrap")));
			// 
			// txtJapanese1
			// 
			this.txtJapanese1.AccessibleDescription = resources.GetString("txtJapanese1.AccessibleDescription");
			this.txtJapanese1.AccessibleName = resources.GetString("txtJapanese1.AccessibleName");
			this.txtJapanese1.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("txtJapanese1.Anchor")));
			this.txtJapanese1.AutoSize = ((bool)(resources.GetObject("txtJapanese1.AutoSize")));
			this.txtJapanese1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("txtJapanese1.BackgroundImage")));
			this.txtJapanese1.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("txtJapanese1.Dock")));
			this.txtJapanese1.Enabled = ((bool)(resources.GetObject("txtJapanese1.Enabled")));
			this.txtJapanese1.Font = ((System.Drawing.Font)(resources.GetObject("txtJapanese1.Font")));
			this.txtJapanese1.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("txtJapanese1.ImeMode")));
			this.txtJapanese1.Location = ((System.Drawing.Point)(resources.GetObject("txtJapanese1.Location")));
			this.txtJapanese1.MaxLength = ((int)(resources.GetObject("txtJapanese1.MaxLength")));
			this.txtJapanese1.Multiline = ((bool)(resources.GetObject("txtJapanese1.Multiline")));
			this.txtJapanese1.Name = "txtJapanese1";
			this.txtJapanese1.PasswordChar = ((char)(resources.GetObject("txtJapanese1.PasswordChar")));
			this.txtJapanese1.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("txtJapanese1.RightToLeft")));
			this.txtJapanese1.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("txtJapanese1.ScrollBars")));
			this.txtJapanese1.Size = ((System.Drawing.Size)(resources.GetObject("txtJapanese1.Size")));
			this.txtJapanese1.TabIndex = ((int)(resources.GetObject("txtJapanese1.TabIndex")));
			this.txtJapanese1.Text = resources.GetString("txtJapanese1.Text");
			this.txtJapanese1.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("txtJapanese1.TextAlign")));
			this.txtJapanese1.Visible = ((bool)(resources.GetObject("txtJapanese1.Visible")));
			this.txtJapanese1.WordWrap = ((bool)(resources.GetObject("txtJapanese1.WordWrap")));
			// 
			// cboAlign1
			// 
			this.cboAlign1.AccessibleDescription = resources.GetString("cboAlign1.AccessibleDescription");
			this.cboAlign1.AccessibleName = resources.GetString("cboAlign1.AccessibleName");
			this.cboAlign1.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("cboAlign1.Anchor")));
			this.cboAlign1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cboAlign1.BackgroundImage")));
			this.cboAlign1.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("cboAlign1.Dock")));
			this.cboAlign1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboAlign1.Enabled = ((bool)(resources.GetObject("cboAlign1.Enabled")));
			this.cboAlign1.Font = ((System.Drawing.Font)(resources.GetObject("cboAlign1.Font")));
			this.cboAlign1.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("cboAlign1.ImeMode")));
			this.cboAlign1.IntegralHeight = ((bool)(resources.GetObject("cboAlign1.IntegralHeight")));
			this.cboAlign1.ItemHeight = ((int)(resources.GetObject("cboAlign1.ItemHeight")));
			this.cboAlign1.Items.AddRange(new object[] {
														   resources.GetString("cboAlign1.Items"),
														   resources.GetString("cboAlign1.Items1"),
														   resources.GetString("cboAlign1.Items2")});
			this.cboAlign1.Location = ((System.Drawing.Point)(resources.GetObject("cboAlign1.Location")));
			this.cboAlign1.MaxDropDownItems = ((int)(resources.GetObject("cboAlign1.MaxDropDownItems")));
			this.cboAlign1.MaxLength = ((int)(resources.GetObject("cboAlign1.MaxLength")));
			this.cboAlign1.Name = "cboAlign1";
			this.cboAlign1.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("cboAlign1.RightToLeft")));
			this.cboAlign1.Size = ((System.Drawing.Size)(resources.GetObject("cboAlign1.Size")));
			this.cboAlign1.TabIndex = ((int)(resources.GetObject("cboAlign1.TabIndex")));
			this.cboAlign1.Text = resources.GetString("cboAlign1.Text");
			this.cboAlign1.Visible = ((bool)(resources.GetObject("cboAlign1.Visible")));
			// 
			// txtWidth1
			// 
			this.txtWidth1.AcceptsEscape = ((bool)(resources.GetObject("txtWidth1.AcceptsEscape")));
			this.txtWidth1.AccessibleDescription = resources.GetString("txtWidth1.AccessibleDescription");
			this.txtWidth1.AccessibleName = resources.GetString("txtWidth1.AccessibleName");
			this.txtWidth1.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("txtWidth1.Anchor")));
			this.txtWidth1.AutoSize = ((bool)(resources.GetObject("txtWidth1.AutoSize")));
			this.txtWidth1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("txtWidth1.BackgroundImage")));
			this.txtWidth1.BorderStyle = ((System.Windows.Forms.BorderStyle)(resources.GetObject("txtWidth1.BorderStyle")));
			// 
			// txtWidth1.Calculator
			// 
			this.txtWidth1.Calculator.AccessibleDescription = resources.GetString("txtWidth1.Calculator.AccessibleDescription");
			this.txtWidth1.Calculator.AccessibleName = resources.GetString("txtWidth1.Calculator.AccessibleName");
			this.txtWidth1.Calculator.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("txtWidth1.Calculator.BackgroundImage")));
			this.txtWidth1.Calculator.ButtonFlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("txtWidth1.Calculator.ButtonFlatStyle")));
			this.txtWidth1.Calculator.DisplayFormat = resources.GetString("txtWidth1.Calculator.DisplayFormat");
			this.txtWidth1.Calculator.Font = ((System.Drawing.Font)(resources.GetObject("txtWidth1.Calculator.Font")));
			this.txtWidth1.Calculator.FormatOnClose = ((bool)(resources.GetObject("txtWidth1.Calculator.FormatOnClose")));
			this.txtWidth1.Calculator.StoredFormat = resources.GetString("txtWidth1.Calculator.StoredFormat");
			this.txtWidth1.Calculator.UIStrings.Content = ((string[])(resources.GetObject("txtWidth1.Calculator.UIStrings.Content")));
			this.txtWidth1.CaseSensitive = ((bool)(resources.GetObject("txtWidth1.CaseSensitive")));
			this.txtWidth1.Culture = ((int)(resources.GetObject("txtWidth1.Culture")));
			this.txtWidth1.CustomFormat = resources.GetString("txtWidth1.CustomFormat");
			this.txtWidth1.DataType = ((System.Type)(resources.GetObject("txtWidth1.DataType")));
			this.txtWidth1.DisplayFormat.CustomFormat = resources.GetString("txtWidth1.DisplayFormat.CustomFormat");
			this.txtWidth1.DisplayFormat.FormatType = ((C1.Win.C1Input.FormatTypeEnum)(resources.GetObject("txtWidth1.DisplayFormat.FormatType")));
			this.txtWidth1.DisplayFormat.Inherit = ((C1.Win.C1Input.FormatInfoInheritFlags)(resources.GetObject("txtWidth1.DisplayFormat.Inherit")));
			this.txtWidth1.DisplayFormat.NullText = resources.GetString("txtWidth1.DisplayFormat.NullText");
			this.txtWidth1.DisplayFormat.TrimEnd = ((bool)(resources.GetObject("txtWidth1.DisplayFormat.TrimEnd")));
			this.txtWidth1.DisplayFormat.TrimStart = ((bool)(resources.GetObject("txtWidth1.DisplayFormat.TrimStart")));
			this.txtWidth1.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("txtWidth1.Dock")));
			this.txtWidth1.DropDownFormAlign = ((C1.Win.C1Input.DropDownFormAlignmentEnum)(resources.GetObject("txtWidth1.DropDownFormAlign")));
			this.txtWidth1.EditFormat.CustomFormat = resources.GetString("txtWidth1.EditFormat.CustomFormat");
			this.txtWidth1.EditFormat.FormatType = ((C1.Win.C1Input.FormatTypeEnum)(resources.GetObject("txtWidth1.EditFormat.FormatType")));
			this.txtWidth1.EditFormat.Inherit = ((C1.Win.C1Input.FormatInfoInheritFlags)(resources.GetObject("txtWidth1.EditFormat.Inherit")));
			this.txtWidth1.EditFormat.NullText = resources.GetString("txtWidth1.EditFormat.NullText");
			this.txtWidth1.EditFormat.TrimEnd = ((bool)(resources.GetObject("txtWidth1.EditFormat.TrimEnd")));
			this.txtWidth1.EditFormat.TrimStart = ((bool)(resources.GetObject("txtWidth1.EditFormat.TrimStart")));
			this.txtWidth1.EditMask = resources.GetString("txtWidth1.EditMask");
			this.txtWidth1.EmptyAsNull = ((bool)(resources.GetObject("txtWidth1.EmptyAsNull")));
			this.txtWidth1.Enabled = ((bool)(resources.GetObject("txtWidth1.Enabled")));
			this.txtWidth1.ErrorInfo.BeepOnError = ((bool)(resources.GetObject("txtWidth1.ErrorInfo.BeepOnError")));
			this.txtWidth1.ErrorInfo.ErrorMessage = resources.GetString("txtWidth1.ErrorInfo.ErrorMessage");
			this.txtWidth1.ErrorInfo.ErrorMessageCaption = resources.GetString("txtWidth1.ErrorInfo.ErrorMessageCaption");
			this.txtWidth1.ErrorInfo.ShowErrorMessage = ((bool)(resources.GetObject("txtWidth1.ErrorInfo.ShowErrorMessage")));
			this.txtWidth1.ErrorInfo.ValueOnError = ((object)(resources.GetObject("txtWidth1.ErrorInfo.ValueOnError")));
			this.txtWidth1.Font = ((System.Drawing.Font)(resources.GetObject("txtWidth1.Font")));
			this.txtWidth1.FormatType = ((C1.Win.C1Input.FormatTypeEnum)(resources.GetObject("txtWidth1.FormatType")));
			this.txtWidth1.GapHeight = ((int)(resources.GetObject("txtWidth1.GapHeight")));
			this.txtWidth1.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("txtWidth1.ImeMode")));
			this.txtWidth1.Increment = ((object)(resources.GetObject("txtWidth1.Increment")));
			this.txtWidth1.InitialSelection = ((C1.Win.C1Input.InitialSelectionEnum)(resources.GetObject("txtWidth1.InitialSelection")));
			this.txtWidth1.Location = ((System.Drawing.Point)(resources.GetObject("txtWidth1.Location")));
			this.txtWidth1.MaskInfo.AutoTabWhenFilled = ((bool)(resources.GetObject("txtWidth1.MaskInfo.AutoTabWhenFilled")));
			this.txtWidth1.MaskInfo.CaseSensitive = ((bool)(resources.GetObject("txtWidth1.MaskInfo.CaseSensitive")));
			this.txtWidth1.MaskInfo.CopyWithLiterals = ((bool)(resources.GetObject("txtWidth1.MaskInfo.CopyWithLiterals")));
			this.txtWidth1.MaskInfo.EditMask = resources.GetString("txtWidth1.MaskInfo.EditMask");
			this.txtWidth1.MaskInfo.EmptyAsNull = ((bool)(resources.GetObject("txtWidth1.MaskInfo.EmptyAsNull")));
			this.txtWidth1.MaskInfo.ErrorMessage = resources.GetString("txtWidth1.MaskInfo.ErrorMessage");
			this.txtWidth1.MaskInfo.Inherit = ((C1.Win.C1Input.MaskInfoInheritFlags)(resources.GetObject("txtWidth1.MaskInfo.Inherit")));
			this.txtWidth1.MaskInfo.PromptChar = ((char)(resources.GetObject("txtWidth1.MaskInfo.PromptChar")));
			this.txtWidth1.MaskInfo.ShowLiterals = ((C1.Win.C1Input.ShowLiteralsEnum)(resources.GetObject("txtWidth1.MaskInfo.ShowLiterals")));
			this.txtWidth1.MaskInfo.StoredEmptyChar = ((char)(resources.GetObject("txtWidth1.MaskInfo.StoredEmptyChar")));
			this.txtWidth1.MaxLength = ((int)(resources.GetObject("txtWidth1.MaxLength")));
			this.txtWidth1.Name = "txtWidth1";
			this.txtWidth1.NullText = resources.GetString("txtWidth1.NullText");
			this.txtWidth1.ParseInfo.CaseSensitive = ((bool)(resources.GetObject("txtWidth1.ParseInfo.CaseSensitive")));
			this.txtWidth1.ParseInfo.CustomFormat = resources.GetString("txtWidth1.ParseInfo.CustomFormat");
			this.txtWidth1.ParseInfo.EmptyAsNull = ((bool)(resources.GetObject("txtWidth1.ParseInfo.EmptyAsNull")));
			this.txtWidth1.ParseInfo.ErrorMessage = resources.GetString("txtWidth1.ParseInfo.ErrorMessage");
			this.txtWidth1.ParseInfo.FormatType = ((C1.Win.C1Input.FormatTypeEnum)(resources.GetObject("txtWidth1.ParseInfo.FormatType")));
			this.txtWidth1.ParseInfo.Inherit = ((C1.Win.C1Input.ParseInfoInheritFlags)(resources.GetObject("txtWidth1.ParseInfo.Inherit")));
			this.txtWidth1.ParseInfo.NullText = resources.GetString("txtWidth1.ParseInfo.NullText");
			this.txtWidth1.ParseInfo.NumberStyle = ((C1.Win.C1Input.NumberStyleFlags)(resources.GetObject("txtWidth1.ParseInfo.NumberStyle")));
			this.txtWidth1.ParseInfo.TrimEnd = ((bool)(resources.GetObject("txtWidth1.ParseInfo.TrimEnd")));
			this.txtWidth1.ParseInfo.TrimStart = ((bool)(resources.GetObject("txtWidth1.ParseInfo.TrimStart")));
			this.txtWidth1.PasswordChar = ((char)(resources.GetObject("txtWidth1.PasswordChar")));
			this.txtWidth1.PostValidation.CaseSensitive = ((bool)(resources.GetObject("txtWidth1.PostValidation.CaseSensitive")));
			this.txtWidth1.PostValidation.ErrorMessage = resources.GetString("txtWidth1.PostValidation.ErrorMessage");
			this.txtWidth1.PostValidation.Inherit = ((C1.Win.C1Input.PostValidationInheritFlags)(resources.GetObject("txtWidth1.PostValidation.Inherit")));
			this.txtWidth1.PostValidation.Intervals.AddRange(new C1.Win.C1Input.ValueInterval[] {
																									((C1.Win.C1Input.ValueInterval)(resources.GetObject("txtWidth1.PostValidation.Intervals")))});
			this.txtWidth1.PostValidation.Validation = ((C1.Win.C1Input.PostValidationTypeEnum)(resources.GetObject("txtWidth1.PostValidation.Validation")));
			this.txtWidth1.PostValidation.Values = ((System.Array)(resources.GetObject("txtWidth1.PostValidation.Values")));
			this.txtWidth1.PostValidation.ValuesExcluded = ((System.Array)(resources.GetObject("txtWidth1.PostValidation.ValuesExcluded")));
			this.txtWidth1.PreValidation.CaseSensitive = ((bool)(resources.GetObject("txtWidth1.PreValidation.CaseSensitive")));
			this.txtWidth1.PreValidation.ErrorMessage = resources.GetString("txtWidth1.PreValidation.ErrorMessage");
			this.txtWidth1.PreValidation.Inherit = ((C1.Win.C1Input.PreValidationInheritFlags)(resources.GetObject("txtWidth1.PreValidation.Inherit")));
			this.txtWidth1.PreValidation.ItemSeparator = resources.GetString("txtWidth1.PreValidation.ItemSeparator");
			this.txtWidth1.PreValidation.PatternString = resources.GetString("txtWidth1.PreValidation.PatternString");
			this.txtWidth1.PreValidation.RegexOptions = ((C1.Win.C1Input.RegexOptionFlags)(resources.GetObject("txtWidth1.PreValidation.RegexOptions")));
			this.txtWidth1.PreValidation.TrimEnd = ((bool)(resources.GetObject("txtWidth1.PreValidation.TrimEnd")));
			this.txtWidth1.PreValidation.TrimStart = ((bool)(resources.GetObject("txtWidth1.PreValidation.TrimStart")));
			this.txtWidth1.PreValidation.Validation = ((C1.Win.C1Input.PreValidationTypeEnum)(resources.GetObject("txtWidth1.PreValidation.Validation")));
			this.txtWidth1.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("txtWidth1.RightToLeft")));
			this.txtWidth1.ShowFocusRectangle = ((bool)(resources.GetObject("txtWidth1.ShowFocusRectangle")));
			this.txtWidth1.Size = ((System.Drawing.Size)(resources.GetObject("txtWidth1.Size")));
			this.txtWidth1.TabIndex = ((int)(resources.GetObject("txtWidth1.TabIndex")));
			this.txtWidth1.Tag = ((object)(resources.GetObject("txtWidth1.Tag")));
			this.txtWidth1.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("txtWidth1.TextAlign")));
			this.txtWidth1.TrimEnd = ((bool)(resources.GetObject("txtWidth1.TrimEnd")));
			this.txtWidth1.TrimStart = ((bool)(resources.GetObject("txtWidth1.TrimStart")));
			this.txtWidth1.UserCultureOverride = ((bool)(resources.GetObject("txtWidth1.UserCultureOverride")));
			this.txtWidth1.Value = ((object)(resources.GetObject("txtWidth1.Value")));
			this.txtWidth1.VerticalAlign = ((C1.Win.C1Input.VerticalAlignEnum)(resources.GetObject("txtWidth1.VerticalAlign")));
			this.txtWidth1.Visible = ((bool)(resources.GetObject("txtWidth1.Visible")));
			this.txtWidth1.VisibleButtons = ((C1.Win.C1Input.DropDownControlButtonFlags)(resources.GetObject("txtWidth1.VisibleButtons")));
			// 
			// txtWidth2
			// 
			this.txtWidth2.AcceptsEscape = ((bool)(resources.GetObject("txtWidth2.AcceptsEscape")));
			this.txtWidth2.AccessibleDescription = resources.GetString("txtWidth2.AccessibleDescription");
			this.txtWidth2.AccessibleName = resources.GetString("txtWidth2.AccessibleName");
			this.txtWidth2.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("txtWidth2.Anchor")));
			this.txtWidth2.AutoSize = ((bool)(resources.GetObject("txtWidth2.AutoSize")));
			this.txtWidth2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("txtWidth2.BackgroundImage")));
			this.txtWidth2.BorderStyle = ((System.Windows.Forms.BorderStyle)(resources.GetObject("txtWidth2.BorderStyle")));
			// 
			// txtWidth2.Calculator
			// 
			this.txtWidth2.Calculator.AccessibleDescription = resources.GetString("txtWidth2.Calculator.AccessibleDescription");
			this.txtWidth2.Calculator.AccessibleName = resources.GetString("txtWidth2.Calculator.AccessibleName");
			this.txtWidth2.Calculator.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("txtWidth2.Calculator.BackgroundImage")));
			this.txtWidth2.Calculator.ButtonFlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("txtWidth2.Calculator.ButtonFlatStyle")));
			this.txtWidth2.Calculator.DisplayFormat = resources.GetString("txtWidth2.Calculator.DisplayFormat");
			this.txtWidth2.Calculator.Font = ((System.Drawing.Font)(resources.GetObject("txtWidth2.Calculator.Font")));
			this.txtWidth2.Calculator.FormatOnClose = ((bool)(resources.GetObject("txtWidth2.Calculator.FormatOnClose")));
			this.txtWidth2.Calculator.StoredFormat = resources.GetString("txtWidth2.Calculator.StoredFormat");
			this.txtWidth2.Calculator.UIStrings.Content = ((string[])(resources.GetObject("txtWidth2.Calculator.UIStrings.Content")));
			this.txtWidth2.CaseSensitive = ((bool)(resources.GetObject("txtWidth2.CaseSensitive")));
			this.txtWidth2.Culture = ((int)(resources.GetObject("txtWidth2.Culture")));
			this.txtWidth2.CustomFormat = resources.GetString("txtWidth2.CustomFormat");
			this.txtWidth2.DataType = ((System.Type)(resources.GetObject("txtWidth2.DataType")));
			this.txtWidth2.DisplayFormat.CustomFormat = resources.GetString("txtWidth2.DisplayFormat.CustomFormat");
			this.txtWidth2.DisplayFormat.FormatType = ((C1.Win.C1Input.FormatTypeEnum)(resources.GetObject("txtWidth2.DisplayFormat.FormatType")));
			this.txtWidth2.DisplayFormat.Inherit = ((C1.Win.C1Input.FormatInfoInheritFlags)(resources.GetObject("txtWidth2.DisplayFormat.Inherit")));
			this.txtWidth2.DisplayFormat.NullText = resources.GetString("txtWidth2.DisplayFormat.NullText");
			this.txtWidth2.DisplayFormat.TrimEnd = ((bool)(resources.GetObject("txtWidth2.DisplayFormat.TrimEnd")));
			this.txtWidth2.DisplayFormat.TrimStart = ((bool)(resources.GetObject("txtWidth2.DisplayFormat.TrimStart")));
			this.txtWidth2.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("txtWidth2.Dock")));
			this.txtWidth2.DropDownFormAlign = ((C1.Win.C1Input.DropDownFormAlignmentEnum)(resources.GetObject("txtWidth2.DropDownFormAlign")));
			this.txtWidth2.EditFormat.CustomFormat = resources.GetString("txtWidth2.EditFormat.CustomFormat");
			this.txtWidth2.EditFormat.FormatType = ((C1.Win.C1Input.FormatTypeEnum)(resources.GetObject("txtWidth2.EditFormat.FormatType")));
			this.txtWidth2.EditFormat.Inherit = ((C1.Win.C1Input.FormatInfoInheritFlags)(resources.GetObject("txtWidth2.EditFormat.Inherit")));
			this.txtWidth2.EditFormat.NullText = resources.GetString("txtWidth2.EditFormat.NullText");
			this.txtWidth2.EditFormat.TrimEnd = ((bool)(resources.GetObject("txtWidth2.EditFormat.TrimEnd")));
			this.txtWidth2.EditFormat.TrimStart = ((bool)(resources.GetObject("txtWidth2.EditFormat.TrimStart")));
			this.txtWidth2.EditMask = resources.GetString("txtWidth2.EditMask");
			this.txtWidth2.EmptyAsNull = ((bool)(resources.GetObject("txtWidth2.EmptyAsNull")));
			this.txtWidth2.Enabled = ((bool)(resources.GetObject("txtWidth2.Enabled")));
			this.txtWidth2.ErrorInfo.BeepOnError = ((bool)(resources.GetObject("txtWidth2.ErrorInfo.BeepOnError")));
			this.txtWidth2.ErrorInfo.ErrorMessage = resources.GetString("txtWidth2.ErrorInfo.ErrorMessage");
			this.txtWidth2.ErrorInfo.ErrorMessageCaption = resources.GetString("txtWidth2.ErrorInfo.ErrorMessageCaption");
			this.txtWidth2.ErrorInfo.ShowErrorMessage = ((bool)(resources.GetObject("txtWidth2.ErrorInfo.ShowErrorMessage")));
			this.txtWidth2.ErrorInfo.ValueOnError = ((object)(resources.GetObject("txtWidth2.ErrorInfo.ValueOnError")));
			this.txtWidth2.Font = ((System.Drawing.Font)(resources.GetObject("txtWidth2.Font")));
			this.txtWidth2.FormatType = ((C1.Win.C1Input.FormatTypeEnum)(resources.GetObject("txtWidth2.FormatType")));
			this.txtWidth2.GapHeight = ((int)(resources.GetObject("txtWidth2.GapHeight")));
			this.txtWidth2.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("txtWidth2.ImeMode")));
			this.txtWidth2.Increment = ((object)(resources.GetObject("txtWidth2.Increment")));
			this.txtWidth2.InitialSelection = ((C1.Win.C1Input.InitialSelectionEnum)(resources.GetObject("txtWidth2.InitialSelection")));
			this.txtWidth2.Location = ((System.Drawing.Point)(resources.GetObject("txtWidth2.Location")));
			this.txtWidth2.MaskInfo.AutoTabWhenFilled = ((bool)(resources.GetObject("txtWidth2.MaskInfo.AutoTabWhenFilled")));
			this.txtWidth2.MaskInfo.CaseSensitive = ((bool)(resources.GetObject("txtWidth2.MaskInfo.CaseSensitive")));
			this.txtWidth2.MaskInfo.CopyWithLiterals = ((bool)(resources.GetObject("txtWidth2.MaskInfo.CopyWithLiterals")));
			this.txtWidth2.MaskInfo.EditMask = resources.GetString("txtWidth2.MaskInfo.EditMask");
			this.txtWidth2.MaskInfo.EmptyAsNull = ((bool)(resources.GetObject("txtWidth2.MaskInfo.EmptyAsNull")));
			this.txtWidth2.MaskInfo.ErrorMessage = resources.GetString("txtWidth2.MaskInfo.ErrorMessage");
			this.txtWidth2.MaskInfo.Inherit = ((C1.Win.C1Input.MaskInfoInheritFlags)(resources.GetObject("txtWidth2.MaskInfo.Inherit")));
			this.txtWidth2.MaskInfo.PromptChar = ((char)(resources.GetObject("txtWidth2.MaskInfo.PromptChar")));
			this.txtWidth2.MaskInfo.ShowLiterals = ((C1.Win.C1Input.ShowLiteralsEnum)(resources.GetObject("txtWidth2.MaskInfo.ShowLiterals")));
			this.txtWidth2.MaskInfo.StoredEmptyChar = ((char)(resources.GetObject("txtWidth2.MaskInfo.StoredEmptyChar")));
			this.txtWidth2.MaxLength = ((int)(resources.GetObject("txtWidth2.MaxLength")));
			this.txtWidth2.Name = "txtWidth2";
			this.txtWidth2.NullText = resources.GetString("txtWidth2.NullText");
			this.txtWidth2.ParseInfo.CaseSensitive = ((bool)(resources.GetObject("txtWidth2.ParseInfo.CaseSensitive")));
			this.txtWidth2.ParseInfo.CustomFormat = resources.GetString("txtWidth2.ParseInfo.CustomFormat");
			this.txtWidth2.ParseInfo.EmptyAsNull = ((bool)(resources.GetObject("txtWidth2.ParseInfo.EmptyAsNull")));
			this.txtWidth2.ParseInfo.ErrorMessage = resources.GetString("txtWidth2.ParseInfo.ErrorMessage");
			this.txtWidth2.ParseInfo.FormatType = ((C1.Win.C1Input.FormatTypeEnum)(resources.GetObject("txtWidth2.ParseInfo.FormatType")));
			this.txtWidth2.ParseInfo.Inherit = ((C1.Win.C1Input.ParseInfoInheritFlags)(resources.GetObject("txtWidth2.ParseInfo.Inherit")));
			this.txtWidth2.ParseInfo.NullText = resources.GetString("txtWidth2.ParseInfo.NullText");
			this.txtWidth2.ParseInfo.NumberStyle = ((C1.Win.C1Input.NumberStyleFlags)(resources.GetObject("txtWidth2.ParseInfo.NumberStyle")));
			this.txtWidth2.ParseInfo.TrimEnd = ((bool)(resources.GetObject("txtWidth2.ParseInfo.TrimEnd")));
			this.txtWidth2.ParseInfo.TrimStart = ((bool)(resources.GetObject("txtWidth2.ParseInfo.TrimStart")));
			this.txtWidth2.PasswordChar = ((char)(resources.GetObject("txtWidth2.PasswordChar")));
			this.txtWidth2.PostValidation.CaseSensitive = ((bool)(resources.GetObject("txtWidth2.PostValidation.CaseSensitive")));
			this.txtWidth2.PostValidation.ErrorMessage = resources.GetString("txtWidth2.PostValidation.ErrorMessage");
			this.txtWidth2.PostValidation.Inherit = ((C1.Win.C1Input.PostValidationInheritFlags)(resources.GetObject("txtWidth2.PostValidation.Inherit")));
			this.txtWidth2.PostValidation.Intervals.AddRange(new C1.Win.C1Input.ValueInterval[] {
																									((C1.Win.C1Input.ValueInterval)(resources.GetObject("txtWidth2.PostValidation.Intervals")))});
			this.txtWidth2.PostValidation.Validation = ((C1.Win.C1Input.PostValidationTypeEnum)(resources.GetObject("txtWidth2.PostValidation.Validation")));
			this.txtWidth2.PostValidation.Values = ((System.Array)(resources.GetObject("txtWidth2.PostValidation.Values")));
			this.txtWidth2.PostValidation.ValuesExcluded = ((System.Array)(resources.GetObject("txtWidth2.PostValidation.ValuesExcluded")));
			this.txtWidth2.PreValidation.CaseSensitive = ((bool)(resources.GetObject("txtWidth2.PreValidation.CaseSensitive")));
			this.txtWidth2.PreValidation.ErrorMessage = resources.GetString("txtWidth2.PreValidation.ErrorMessage");
			this.txtWidth2.PreValidation.Inherit = ((C1.Win.C1Input.PreValidationInheritFlags)(resources.GetObject("txtWidth2.PreValidation.Inherit")));
			this.txtWidth2.PreValidation.ItemSeparator = resources.GetString("txtWidth2.PreValidation.ItemSeparator");
			this.txtWidth2.PreValidation.PatternString = resources.GetString("txtWidth2.PreValidation.PatternString");
			this.txtWidth2.PreValidation.RegexOptions = ((C1.Win.C1Input.RegexOptionFlags)(resources.GetObject("txtWidth2.PreValidation.RegexOptions")));
			this.txtWidth2.PreValidation.TrimEnd = ((bool)(resources.GetObject("txtWidth2.PreValidation.TrimEnd")));
			this.txtWidth2.PreValidation.TrimStart = ((bool)(resources.GetObject("txtWidth2.PreValidation.TrimStart")));
			this.txtWidth2.PreValidation.Validation = ((C1.Win.C1Input.PreValidationTypeEnum)(resources.GetObject("txtWidth2.PreValidation.Validation")));
			this.txtWidth2.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("txtWidth2.RightToLeft")));
			this.txtWidth2.ShowFocusRectangle = ((bool)(resources.GetObject("txtWidth2.ShowFocusRectangle")));
			this.txtWidth2.Size = ((System.Drawing.Size)(resources.GetObject("txtWidth2.Size")));
			this.txtWidth2.TabIndex = ((int)(resources.GetObject("txtWidth2.TabIndex")));
			this.txtWidth2.Tag = ((object)(resources.GetObject("txtWidth2.Tag")));
			this.txtWidth2.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("txtWidth2.TextAlign")));
			this.txtWidth2.TrimEnd = ((bool)(resources.GetObject("txtWidth2.TrimEnd")));
			this.txtWidth2.TrimStart = ((bool)(resources.GetObject("txtWidth2.TrimStart")));
			this.txtWidth2.UserCultureOverride = ((bool)(resources.GetObject("txtWidth2.UserCultureOverride")));
			this.txtWidth2.Value = ((object)(resources.GetObject("txtWidth2.Value")));
			this.txtWidth2.VerticalAlign = ((C1.Win.C1Input.VerticalAlignEnum)(resources.GetObject("txtWidth2.VerticalAlign")));
			this.txtWidth2.Visible = ((bool)(resources.GetObject("txtWidth2.Visible")));
			this.txtWidth2.VisibleButtons = ((C1.Win.C1Input.DropDownControlButtonFlags)(resources.GetObject("txtWidth2.VisibleButtons")));
			// 
			// txtWidth3
			// 
			this.txtWidth3.AcceptsEscape = ((bool)(resources.GetObject("txtWidth3.AcceptsEscape")));
			this.txtWidth3.AccessibleDescription = resources.GetString("txtWidth3.AccessibleDescription");
			this.txtWidth3.AccessibleName = resources.GetString("txtWidth3.AccessibleName");
			this.txtWidth3.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("txtWidth3.Anchor")));
			this.txtWidth3.AutoSize = ((bool)(resources.GetObject("txtWidth3.AutoSize")));
			this.txtWidth3.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("txtWidth3.BackgroundImage")));
			this.txtWidth3.BorderStyle = ((System.Windows.Forms.BorderStyle)(resources.GetObject("txtWidth3.BorderStyle")));
			// 
			// txtWidth3.Calculator
			// 
			this.txtWidth3.Calculator.AccessibleDescription = resources.GetString("txtWidth3.Calculator.AccessibleDescription");
			this.txtWidth3.Calculator.AccessibleName = resources.GetString("txtWidth3.Calculator.AccessibleName");
			this.txtWidth3.Calculator.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("txtWidth3.Calculator.BackgroundImage")));
			this.txtWidth3.Calculator.ButtonFlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("txtWidth3.Calculator.ButtonFlatStyle")));
			this.txtWidth3.Calculator.DisplayFormat = resources.GetString("txtWidth3.Calculator.DisplayFormat");
			this.txtWidth3.Calculator.Font = ((System.Drawing.Font)(resources.GetObject("txtWidth3.Calculator.Font")));
			this.txtWidth3.Calculator.FormatOnClose = ((bool)(resources.GetObject("txtWidth3.Calculator.FormatOnClose")));
			this.txtWidth3.Calculator.StoredFormat = resources.GetString("txtWidth3.Calculator.StoredFormat");
			this.txtWidth3.Calculator.UIStrings.Content = ((string[])(resources.GetObject("txtWidth3.Calculator.UIStrings.Content")));
			this.txtWidth3.CaseSensitive = ((bool)(resources.GetObject("txtWidth3.CaseSensitive")));
			this.txtWidth3.Culture = ((int)(resources.GetObject("txtWidth3.Culture")));
			this.txtWidth3.CustomFormat = resources.GetString("txtWidth3.CustomFormat");
			this.txtWidth3.DataType = ((System.Type)(resources.GetObject("txtWidth3.DataType")));
			this.txtWidth3.DisplayFormat.CustomFormat = resources.GetString("txtWidth3.DisplayFormat.CustomFormat");
			this.txtWidth3.DisplayFormat.FormatType = ((C1.Win.C1Input.FormatTypeEnum)(resources.GetObject("txtWidth3.DisplayFormat.FormatType")));
			this.txtWidth3.DisplayFormat.Inherit = ((C1.Win.C1Input.FormatInfoInheritFlags)(resources.GetObject("txtWidth3.DisplayFormat.Inherit")));
			this.txtWidth3.DisplayFormat.NullText = resources.GetString("txtWidth3.DisplayFormat.NullText");
			this.txtWidth3.DisplayFormat.TrimEnd = ((bool)(resources.GetObject("txtWidth3.DisplayFormat.TrimEnd")));
			this.txtWidth3.DisplayFormat.TrimStart = ((bool)(resources.GetObject("txtWidth3.DisplayFormat.TrimStart")));
			this.txtWidth3.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("txtWidth3.Dock")));
			this.txtWidth3.DropDownFormAlign = ((C1.Win.C1Input.DropDownFormAlignmentEnum)(resources.GetObject("txtWidth3.DropDownFormAlign")));
			this.txtWidth3.EditFormat.CustomFormat = resources.GetString("txtWidth3.EditFormat.CustomFormat");
			this.txtWidth3.EditFormat.FormatType = ((C1.Win.C1Input.FormatTypeEnum)(resources.GetObject("txtWidth3.EditFormat.FormatType")));
			this.txtWidth3.EditFormat.Inherit = ((C1.Win.C1Input.FormatInfoInheritFlags)(resources.GetObject("txtWidth3.EditFormat.Inherit")));
			this.txtWidth3.EditFormat.NullText = resources.GetString("txtWidth3.EditFormat.NullText");
			this.txtWidth3.EditFormat.TrimEnd = ((bool)(resources.GetObject("txtWidth3.EditFormat.TrimEnd")));
			this.txtWidth3.EditFormat.TrimStart = ((bool)(resources.GetObject("txtWidth3.EditFormat.TrimStart")));
			this.txtWidth3.EditMask = resources.GetString("txtWidth3.EditMask");
			this.txtWidth3.EmptyAsNull = ((bool)(resources.GetObject("txtWidth3.EmptyAsNull")));
			this.txtWidth3.Enabled = ((bool)(resources.GetObject("txtWidth3.Enabled")));
			this.txtWidth3.ErrorInfo.BeepOnError = ((bool)(resources.GetObject("txtWidth3.ErrorInfo.BeepOnError")));
			this.txtWidth3.ErrorInfo.ErrorMessage = resources.GetString("txtWidth3.ErrorInfo.ErrorMessage");
			this.txtWidth3.ErrorInfo.ErrorMessageCaption = resources.GetString("txtWidth3.ErrorInfo.ErrorMessageCaption");
			this.txtWidth3.ErrorInfo.ShowErrorMessage = ((bool)(resources.GetObject("txtWidth3.ErrorInfo.ShowErrorMessage")));
			this.txtWidth3.ErrorInfo.ValueOnError = ((object)(resources.GetObject("txtWidth3.ErrorInfo.ValueOnError")));
			this.txtWidth3.Font = ((System.Drawing.Font)(resources.GetObject("txtWidth3.Font")));
			this.txtWidth3.FormatType = ((C1.Win.C1Input.FormatTypeEnum)(resources.GetObject("txtWidth3.FormatType")));
			this.txtWidth3.GapHeight = ((int)(resources.GetObject("txtWidth3.GapHeight")));
			this.txtWidth3.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("txtWidth3.ImeMode")));
			this.txtWidth3.Increment = ((object)(resources.GetObject("txtWidth3.Increment")));
			this.txtWidth3.InitialSelection = ((C1.Win.C1Input.InitialSelectionEnum)(resources.GetObject("txtWidth3.InitialSelection")));
			this.txtWidth3.Location = ((System.Drawing.Point)(resources.GetObject("txtWidth3.Location")));
			this.txtWidth3.MaskInfo.AutoTabWhenFilled = ((bool)(resources.GetObject("txtWidth3.MaskInfo.AutoTabWhenFilled")));
			this.txtWidth3.MaskInfo.CaseSensitive = ((bool)(resources.GetObject("txtWidth3.MaskInfo.CaseSensitive")));
			this.txtWidth3.MaskInfo.CopyWithLiterals = ((bool)(resources.GetObject("txtWidth3.MaskInfo.CopyWithLiterals")));
			this.txtWidth3.MaskInfo.EditMask = resources.GetString("txtWidth3.MaskInfo.EditMask");
			this.txtWidth3.MaskInfo.EmptyAsNull = ((bool)(resources.GetObject("txtWidth3.MaskInfo.EmptyAsNull")));
			this.txtWidth3.MaskInfo.ErrorMessage = resources.GetString("txtWidth3.MaskInfo.ErrorMessage");
			this.txtWidth3.MaskInfo.Inherit = ((C1.Win.C1Input.MaskInfoInheritFlags)(resources.GetObject("txtWidth3.MaskInfo.Inherit")));
			this.txtWidth3.MaskInfo.PromptChar = ((char)(resources.GetObject("txtWidth3.MaskInfo.PromptChar")));
			this.txtWidth3.MaskInfo.ShowLiterals = ((C1.Win.C1Input.ShowLiteralsEnum)(resources.GetObject("txtWidth3.MaskInfo.ShowLiterals")));
			this.txtWidth3.MaskInfo.StoredEmptyChar = ((char)(resources.GetObject("txtWidth3.MaskInfo.StoredEmptyChar")));
			this.txtWidth3.MaxLength = ((int)(resources.GetObject("txtWidth3.MaxLength")));
			this.txtWidth3.Name = "txtWidth3";
			this.txtWidth3.NullText = resources.GetString("txtWidth3.NullText");
			this.txtWidth3.ParseInfo.CaseSensitive = ((bool)(resources.GetObject("txtWidth3.ParseInfo.CaseSensitive")));
			this.txtWidth3.ParseInfo.CustomFormat = resources.GetString("txtWidth3.ParseInfo.CustomFormat");
			this.txtWidth3.ParseInfo.EmptyAsNull = ((bool)(resources.GetObject("txtWidth3.ParseInfo.EmptyAsNull")));
			this.txtWidth3.ParseInfo.ErrorMessage = resources.GetString("txtWidth3.ParseInfo.ErrorMessage");
			this.txtWidth3.ParseInfo.FormatType = ((C1.Win.C1Input.FormatTypeEnum)(resources.GetObject("txtWidth3.ParseInfo.FormatType")));
			this.txtWidth3.ParseInfo.Inherit = ((C1.Win.C1Input.ParseInfoInheritFlags)(resources.GetObject("txtWidth3.ParseInfo.Inherit")));
			this.txtWidth3.ParseInfo.NullText = resources.GetString("txtWidth3.ParseInfo.NullText");
			this.txtWidth3.ParseInfo.NumberStyle = ((C1.Win.C1Input.NumberStyleFlags)(resources.GetObject("txtWidth3.ParseInfo.NumberStyle")));
			this.txtWidth3.ParseInfo.TrimEnd = ((bool)(resources.GetObject("txtWidth3.ParseInfo.TrimEnd")));
			this.txtWidth3.ParseInfo.TrimStart = ((bool)(resources.GetObject("txtWidth3.ParseInfo.TrimStart")));
			this.txtWidth3.PasswordChar = ((char)(resources.GetObject("txtWidth3.PasswordChar")));
			this.txtWidth3.PostValidation.CaseSensitive = ((bool)(resources.GetObject("txtWidth3.PostValidation.CaseSensitive")));
			this.txtWidth3.PostValidation.ErrorMessage = resources.GetString("txtWidth3.PostValidation.ErrorMessage");
			this.txtWidth3.PostValidation.Inherit = ((C1.Win.C1Input.PostValidationInheritFlags)(resources.GetObject("txtWidth3.PostValidation.Inherit")));
			this.txtWidth3.PostValidation.Intervals.AddRange(new C1.Win.C1Input.ValueInterval[] {
																									((C1.Win.C1Input.ValueInterval)(resources.GetObject("txtWidth3.PostValidation.Intervals")))});
			this.txtWidth3.PostValidation.Validation = ((C1.Win.C1Input.PostValidationTypeEnum)(resources.GetObject("txtWidth3.PostValidation.Validation")));
			this.txtWidth3.PostValidation.Values = ((System.Array)(resources.GetObject("txtWidth3.PostValidation.Values")));
			this.txtWidth3.PostValidation.ValuesExcluded = ((System.Array)(resources.GetObject("txtWidth3.PostValidation.ValuesExcluded")));
			this.txtWidth3.PreValidation.CaseSensitive = ((bool)(resources.GetObject("txtWidth3.PreValidation.CaseSensitive")));
			this.txtWidth3.PreValidation.ErrorMessage = resources.GetString("txtWidth3.PreValidation.ErrorMessage");
			this.txtWidth3.PreValidation.Inherit = ((C1.Win.C1Input.PreValidationInheritFlags)(resources.GetObject("txtWidth3.PreValidation.Inherit")));
			this.txtWidth3.PreValidation.ItemSeparator = resources.GetString("txtWidth3.PreValidation.ItemSeparator");
			this.txtWidth3.PreValidation.PatternString = resources.GetString("txtWidth3.PreValidation.PatternString");
			this.txtWidth3.PreValidation.RegexOptions = ((C1.Win.C1Input.RegexOptionFlags)(resources.GetObject("txtWidth3.PreValidation.RegexOptions")));
			this.txtWidth3.PreValidation.TrimEnd = ((bool)(resources.GetObject("txtWidth3.PreValidation.TrimEnd")));
			this.txtWidth3.PreValidation.TrimStart = ((bool)(resources.GetObject("txtWidth3.PreValidation.TrimStart")));
			this.txtWidth3.PreValidation.Validation = ((C1.Win.C1Input.PreValidationTypeEnum)(resources.GetObject("txtWidth3.PreValidation.Validation")));
			this.txtWidth3.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("txtWidth3.RightToLeft")));
			this.txtWidth3.ShowFocusRectangle = ((bool)(resources.GetObject("txtWidth3.ShowFocusRectangle")));
			this.txtWidth3.Size = ((System.Drawing.Size)(resources.GetObject("txtWidth3.Size")));
			this.txtWidth3.TabIndex = ((int)(resources.GetObject("txtWidth3.TabIndex")));
			this.txtWidth3.Tag = ((object)(resources.GetObject("txtWidth3.Tag")));
			this.txtWidth3.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("txtWidth3.TextAlign")));
			this.txtWidth3.TrimEnd = ((bool)(resources.GetObject("txtWidth3.TrimEnd")));
			this.txtWidth3.TrimStart = ((bool)(resources.GetObject("txtWidth3.TrimStart")));
			this.txtWidth3.UserCultureOverride = ((bool)(resources.GetObject("txtWidth3.UserCultureOverride")));
			this.txtWidth3.Value = ((object)(resources.GetObject("txtWidth3.Value")));
			this.txtWidth3.VerticalAlign = ((C1.Win.C1Input.VerticalAlignEnum)(resources.GetObject("txtWidth3.VerticalAlign")));
			this.txtWidth3.Visible = ((bool)(resources.GetObject("txtWidth3.Visible")));
			this.txtWidth3.VisibleButtons = ((C1.Win.C1Input.DropDownControlButtonFlags)(resources.GetObject("txtWidth3.VisibleButtons")));
			// 
			// label15
			// 
			this.label15.AccessibleDescription = resources.GetString("label15.AccessibleDescription");
			this.label15.AccessibleName = resources.GetString("label15.AccessibleName");
			this.label15.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("label15.Anchor")));
			this.label15.AutoSize = ((bool)(resources.GetObject("label15.AutoSize")));
			this.label15.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("label15.Dock")));
			this.label15.Enabled = ((bool)(resources.GetObject("label15.Enabled")));
			this.label15.Font = ((System.Drawing.Font)(resources.GetObject("label15.Font")));
			this.label15.Image = ((System.Drawing.Image)(resources.GetObject("label15.Image")));
			this.label15.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label15.ImageAlign")));
			this.label15.ImageIndex = ((int)(resources.GetObject("label15.ImageIndex")));
			this.label15.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("label15.ImeMode")));
			this.label15.Location = ((System.Drawing.Point)(resources.GetObject("label15.Location")));
			this.label15.Name = "label15";
			this.label15.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("label15.RightToLeft")));
			this.label15.Size = ((System.Drawing.Size)(resources.GetObject("label15.Size")));
			this.label15.TabIndex = ((int)(resources.GetObject("label15.TabIndex")));
			this.label15.Text = resources.GetString("label15.Text");
			this.label15.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label15.TextAlign")));
			this.label15.Visible = ((bool)(resources.GetObject("label15.Visible")));
			// 
			// cboFromTable
			// 
			this.cboFromTable.AccessibleDescription = resources.GetString("cboFromTable.AccessibleDescription");
			this.cboFromTable.AccessibleName = resources.GetString("cboFromTable.AccessibleName");
			this.cboFromTable.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("cboFromTable.Anchor")));
			this.cboFromTable.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cboFromTable.BackgroundImage")));
			this.cboFromTable.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("cboFromTable.Dock")));
			this.cboFromTable.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboFromTable.Enabled = ((bool)(resources.GetObject("cboFromTable.Enabled")));
			this.cboFromTable.Font = ((System.Drawing.Font)(resources.GetObject("cboFromTable.Font")));
			this.cboFromTable.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("cboFromTable.ImeMode")));
			this.cboFromTable.IntegralHeight = ((bool)(resources.GetObject("cboFromTable.IntegralHeight")));
			this.cboFromTable.ItemHeight = ((int)(resources.GetObject("cboFromTable.ItemHeight")));
			this.cboFromTable.Location = ((System.Drawing.Point)(resources.GetObject("cboFromTable.Location")));
			this.cboFromTable.MaxDropDownItems = ((int)(resources.GetObject("cboFromTable.MaxDropDownItems")));
			this.cboFromTable.MaxLength = ((int)(resources.GetObject("cboFromTable.MaxLength")));
			this.cboFromTable.Name = "cboFromTable";
			this.cboFromTable.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("cboFromTable.RightToLeft")));
			this.cboFromTable.Size = ((System.Drawing.Size)(resources.GetObject("cboFromTable.Size")));
			this.cboFromTable.TabIndex = ((int)(resources.GetObject("cboFromTable.TabIndex")));
			this.cboFromTable.Text = resources.GetString("cboFromTable.Text");
			this.cboFromTable.Visible = ((bool)(resources.GetObject("cboFromTable.Visible")));
			this.cboFromTable.SelectedIndexChanged += new System.EventHandler(this.cboFromTable_SelectedIndexChanged);
			// 
			// cboFromField
			// 
			this.cboFromField.AccessibleDescription = resources.GetString("cboFromField.AccessibleDescription");
			this.cboFromField.AccessibleName = resources.GetString("cboFromField.AccessibleName");
			this.cboFromField.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("cboFromField.Anchor")));
			this.cboFromField.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cboFromField.BackgroundImage")));
			this.cboFromField.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("cboFromField.Dock")));
			this.cboFromField.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboFromField.Enabled = ((bool)(resources.GetObject("cboFromField.Enabled")));
			this.cboFromField.Font = ((System.Drawing.Font)(resources.GetObject("cboFromField.Font")));
			this.cboFromField.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("cboFromField.ImeMode")));
			this.cboFromField.IntegralHeight = ((bool)(resources.GetObject("cboFromField.IntegralHeight")));
			this.cboFromField.ItemHeight = ((int)(resources.GetObject("cboFromField.ItemHeight")));
			this.cboFromField.Location = ((System.Drawing.Point)(resources.GetObject("cboFromField.Location")));
			this.cboFromField.MaxDropDownItems = ((int)(resources.GetObject("cboFromField.MaxDropDownItems")));
			this.cboFromField.MaxLength = ((int)(resources.GetObject("cboFromField.MaxLength")));
			this.cboFromField.Name = "cboFromField";
			this.cboFromField.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("cboFromField.RightToLeft")));
			this.cboFromField.Size = ((System.Drawing.Size)(resources.GetObject("cboFromField.Size")));
			this.cboFromField.TabIndex = ((int)(resources.GetObject("cboFromField.TabIndex")));
			this.cboFromField.Text = resources.GetString("cboFromField.Text");
			this.cboFromField.Visible = ((bool)(resources.GetObject("cboFromField.Visible")));
			this.cboFromField.SelectedIndexChanged += new System.EventHandler(this.cboFromField_SelectedIndexChanged);
			// 
			// label5
			// 
			this.label5.AccessibleDescription = resources.GetString("label5.AccessibleDescription");
			this.label5.AccessibleName = resources.GetString("label5.AccessibleName");
			this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("label5.Anchor")));
			this.label5.AutoSize = ((bool)(resources.GetObject("label5.AutoSize")));
			this.label5.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("label5.Dock")));
			this.label5.Enabled = ((bool)(resources.GetObject("label5.Enabled")));
			this.label5.Font = ((System.Drawing.Font)(resources.GetObject("label5.Font")));
			this.label5.Image = ((System.Drawing.Image)(resources.GetObject("label5.Image")));
			this.label5.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label5.ImageAlign")));
			this.label5.ImageIndex = ((int)(resources.GetObject("label5.ImageIndex")));
			this.label5.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("label5.ImeMode")));
			this.label5.Location = ((System.Drawing.Point)(resources.GetObject("label5.Location")));
			this.label5.Name = "label5";
			this.label5.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("label5.RightToLeft")));
			this.label5.Size = ((System.Drawing.Size)(resources.GetObject("label5.Size")));
			this.label5.TabIndex = ((int)(resources.GetObject("label5.TabIndex")));
			this.label5.Text = resources.GetString("label5.Text");
			this.label5.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label5.TextAlign")));
			this.label5.Visible = ((bool)(resources.GetObject("label5.Visible")));
			// 
			// lblLinkField
			// 
			this.lblLinkField.AccessibleDescription = resources.GetString("lblLinkField.AccessibleDescription");
			this.lblLinkField.AccessibleName = resources.GetString("lblLinkField.AccessibleName");
			this.lblLinkField.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblLinkField.Anchor")));
			this.lblLinkField.AutoSize = ((bool)(resources.GetObject("lblLinkField.AutoSize")));
			this.lblLinkField.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblLinkField.Dock")));
			this.lblLinkField.Enabled = ((bool)(resources.GetObject("lblLinkField.Enabled")));
			this.lblLinkField.Font = ((System.Drawing.Font)(resources.GetObject("lblLinkField.Font")));
			this.lblLinkField.Image = ((System.Drawing.Image)(resources.GetObject("lblLinkField.Image")));
			this.lblLinkField.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblLinkField.ImageAlign")));
			this.lblLinkField.ImageIndex = ((int)(resources.GetObject("lblLinkField.ImageIndex")));
			this.lblLinkField.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblLinkField.ImeMode")));
			this.lblLinkField.Location = ((System.Drawing.Point)(resources.GetObject("lblLinkField.Location")));
			this.lblLinkField.Name = "lblLinkField";
			this.lblLinkField.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblLinkField.RightToLeft")));
			this.lblLinkField.Size = ((System.Drawing.Size)(resources.GetObject("lblLinkField.Size")));
			this.lblLinkField.TabIndex = ((int)(resources.GetObject("lblLinkField.TabIndex")));
			this.lblLinkField.Text = resources.GetString("lblLinkField.Text");
			this.lblLinkField.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblLinkField.TextAlign")));
			this.lblLinkField.Visible = ((bool)(resources.GetObject("lblLinkField.Visible")));
			// 
			// TableConfigDetail
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
			this.Controls.Add(this.grbExternalField);
			this.Controls.Add(this.label15);
			this.Controls.Add(this.cboFromTable);
			this.Controls.Add(this.cboFromField);
			this.Controls.Add(this.lstField);
			this.Controls.Add(this.btnClose);
			this.Controls.Add(this.btnAdd);
			this.Controls.Add(this.btnDown);
			this.Controls.Add(this.btnUp);
			this.Controls.Add(this.label12);
			this.Controls.Add(this.bntHelp);
			this.Controls.Add(this.btnSave);
			this.Controls.Add(this.btnDelete);
			this.Controls.Add(this.label9);
			this.Controls.Add(this.label10);
			this.Controls.Add(this.txtVietnamese);
			this.Controls.Add(this.txtEnglish);
			this.Controls.Add(this.txtFormat);
			this.Controls.Add(this.txtItems);
			this.Controls.Add(this.txtJapaness);
			this.Controls.Add(this.txtWidth);
			this.Controls.Add(this.cboAlign);
			this.Controls.Add(this.cboCase);
			this.Controls.Add(this.cboFieldName);
			this.Controls.Add(this.cboSortType);
			this.Controls.Add(this.chkIdentity);
			this.Controls.Add(this.chkNotAllowNull);
			this.Controls.Add(this.chkUnique);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.lblLinkField);
			this.Controls.Add(this.label11);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.label13);
			this.Controls.Add(this.label14);
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
			this.Name = "TableConfigDetail";
			this.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("$this.RightToLeft")));
			this.StartPosition = ((System.Windows.Forms.FormStartPosition)(resources.GetObject("$this.StartPosition")));
			this.Text = resources.GetString("$this.Text");
			this.Load += new System.EventHandler(this.TableConfigDetail_Load);
			this.grbExternalField.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.txtWidth1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.txtWidth2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.txtWidth3)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		private void bntHelp_Click(object sender, System.EventArgs e)
		{
			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.WaitCursor;

			#endregion Code Inserted Automatically

			
			// Help

			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.Default;

			#endregion Code Inserted Automatically

			
		}

		//**************************************************************************              
		///    <Description>
		///       Insert new record into database
		///    </Description>
		///    <Inputs>
		///       object value 
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
		private void btnAdd_Click(object sender, System.EventArgs e)
		{
			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.WaitCursor;

			#endregion Code Inserted Automatically

			
			const string METHOD_NAME = THIS + ".btnAdd_Click()";
			try
			{
				const string DEFAULT_WIDTH = "100";
				// SetButtonToAdd();
				mEnumType = EnumAction.Add;
				cboCase.Text = STR_NORMAL;
				cboAlign.Text = STR_LEFT;
				cboSortType.Text = STR_NONE;
				txtWidth.Text = DEFAULT_WIDTH;
				txtEnglish.Text = string.Empty;
				txtVietnamese.Text = string.Empty;
				txtJapaness.Text = string.Empty;
				txtEnglish1.Text = string.Empty;
				txtVietnamese1.Text = string.Empty;
				txtJapanese1.Text = string.Empty;
				txtEnglish2.Text = string.Empty;
				txtVietnamese2.Text = string.Empty;
				txtJapanese2.Text = string.Empty;
				txtEnglish3.Text = string.Empty;
				txtVietnamese3.Text = string.Empty;
				txtJapanese3.Text = string.Empty;
				txtFormat.Text = string.Empty;
				txtFormat1.Text = string.Empty;
				txtFormat2.Text = string.Empty;
				txtFormat3.Text = string.Empty;
				txtItems.Text = string.Empty;
				cboFromTable.Text = string.Empty;
				cboFromField.Text = string.Empty;
				cboFilterField1.Text = string.Empty;
				cboFilterField2.Text = string.Empty;
				voTableField = new sys_TableFieldVO();
				cboFieldName.Focus();
				//fill default information for this field name
				

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


		//**************************************************************************              
		///    <Description>
		///      Save data in database
		///    </Description>
		///    <Inputs>
		///       object value 
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
		private void btnSave_Click(object sender, System.EventArgs e)
		{
			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.WaitCursor;

			#endregion Code Inserted Automatically

			
			const string METHOD_NAME = THIS + ".btnSave_Click()";
			voTableField.TableID = mTableID;
			voTableField.FieldName = cboFieldName.Text.Trim();
			voTableField.CaptionEN = txtEnglish.Text.Trim();
			voTableField.CaptionVN = txtVietnamese.Text.Trim();
			voTableField.CaptionJP = txtJapaness.Text.Trim();
			voTableField.Caption = voTableField.CaptionEN;			
			// cboCase
			if (cboCase.Text == STR_NORMAL)
			{
				voTableField.CharacterCase = INT_NORMAL;
			} 
			else if (cboCase.Text == STR_UPPER)
			{
				voTableField.CharacterCase = INT_UPPER;
			} 
			else if (cboCase.Text == STR_LOWER)
			{
				voTableField.CharacterCase = INT_LOWER;
			}
			// cboAlign
			if (cboAlign.Text == STR_LEFT)
			{
				voTableField.Align = INT_LEFT;
			} 
			else if (cboAlign.Text == STR_CENTER)
			{
				voTableField.Align = INT_CENTER;
			} 
			else if (cboAlign.Text == STR_RIGHT)
			{
				voTableField.Align = INT_RIGHT;
			}
			string strWidth = txtWidth.Text.Trim();

			try
			{
				voTableField.Width = int.Parse(strWidth);
			}
			catch
			{
				PCSMessageBox.Show(ErrorCode.MESSAGE_INVALID_NUMERIC,MessageBoxIcon.Error);
				txtWidth.Focus();
				txtWidth.SelectAll();
				// Code Inserted Automatically
				#region Code Inserted Automatically
				this.Cursor = Cursors.Default;
				#endregion Code Inserted Automatically
				return;
			}
			// SortType
			if (cboSortType.Text == STR_NONE)
			{
				voTableField.SortType = INT_NONE;
			} 
			else if (cboSortType.Text == STR_ASCENDING)
			{
				voTableField.SortType = INT_ASCENDING;
			} 
			else if (cboSortType.Text == STR_DESCENDING)
			{
				voTableField.SortType = INT_DESCENDING;
			}
			voTableField.Formats = txtFormat.Text;
			// chkUnique
			voTableField.UniqueColumn = chkUnique.Checked;

			// chkNotAllowNull
			voTableField.NotAllowNull = chkNotAllowNull.Checked;

			// chkIdentity
			voTableField.IdentityColumn = chkIdentity.Checked;
			voTableField.Items = txtItems.Text.Trim();
			voTableField.FromTable = cboFromTable.Text.Trim();
			voTableField.FromField = cboFromField.Text.Trim();
			voTableField.FilterField1 = cboFilterField1.Text.Trim();
			voTableField.FilterField2 = cboFilterField2.Text.Trim();
			voTableField.FilterField3 = cboFilterField3.Text.Trim();
			// HACK: Trada 05-04-2006
			//Filter 1
			if ((cboFilterField1.SelectedIndex != -1) && (cboFilterField1.SelectedIndex != 0))
			{
				voTableField.Align1 = cboAlign1.SelectedIndex;
				voTableField.CaptionEN1 = txtEnglish1.Text.Trim();
				voTableField.CaptionJP1 = txtJapanese1.Text.Trim();
				voTableField.CaptionVN1 = txtVietnamese1.Text.Trim();
				voTableField.Formats1 = txtFormat1.Text.Trim();
				voTableField.Width1 = int.Parse(txtWidth1.Value.ToString());
				
			}
			else
			{
				voTableField.Align1 = -1;
				voTableField.CaptionEN1 = string.Empty;
				voTableField.CaptionJP1 = string.Empty;
				voTableField.CaptionVN1 = string.Empty;
				voTableField.Formats1 = String.Empty;
				voTableField.Width1 = 0;
			}
			//Filter 2
			if ((cboFilterField2.SelectedIndex != -1)&& (cboFilterField2.SelectedIndex != 0))
			{
				voTableField.Align2 = cboAlign2.SelectedIndex;
				voTableField.CaptionEN2 = txtEnglish2.Text.Trim();
				voTableField.CaptionJP2 = txtJapanese2.Text.Trim();
				voTableField.CaptionVN2 = txtVietnamese2.Text.Trim();
				voTableField.Formats2 = txtFormat2.Text.Trim();
				voTableField.Width2 = int.Parse(txtWidth2.Value.ToString());
				
			}
			else
			{
				voTableField.Align2 = -1;
				voTableField.CaptionEN2 = string.Empty;
				voTableField.CaptionJP2 = string.Empty;
				voTableField.CaptionVN2 = string.Empty;
				voTableField.Formats2 = String.Empty;
				voTableField.Width2 = 0;
			}
			//Filter 3
			if ((cboFilterField3.SelectedIndex != -1)&& (cboFilterField3.SelectedIndex != 0))
			{
				voTableField.Align3 = cboAlign3.SelectedIndex;
				voTableField.CaptionEN3 = txtEnglish3.Text.Trim();
				voTableField.CaptionJP3 = txtJapanese3.Text.Trim();
				voTableField.CaptionVN3 = txtVietnamese3.Text.Trim();
				voTableField.Formats3 = txtFormat3.Text.Trim();
				voTableField.Width3 = int.Parse(txtWidth3.Value.ToString());
				
			}
			else
			{
				voTableField.Align3 = -1;
				voTableField.CaptionEN3 = string.Empty;
				voTableField.CaptionJP3 = string.Empty;
				voTableField.CaptionVN3 = string.Empty;
				voTableField.Formats3 = String.Empty;
				voTableField.Width3 = 0;
			}
			// END: Trada 05-04-2006


			// check data
			if(CheckData())
			{
				// add new record 
				if(mEnumType == EnumAction.Add)
				{
					//sys_TableFieldVO vo = new sys_TableFieldVO();
					try
					{
						TableConfigDetailBO bo = new TableConfigDetailBO();
						DataTable objDataTable = (DataTable)lstField.DataSource;
						voTableField.FieldOrder = objDataTable.Rows.Count + 1;
						bo.Add(voTableField);						
						LoadListField();
						// select the last value of list
						lstField.SetSelected(lstField.Items.Count - 1, true);
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
						// update record
				else if(mEnumType == EnumAction.Edit)
				{
					try
					{
						int intSelectedIndex = lstField.SelectedIndex;
						TableConfigDetailBO bo = new TableConfigDetailBO();
						bo.Update(voTableField);						
						LoadListField();
						lstField.SetSelected(intSelectedIndex, true);
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
				EnumType = EnumAction.Edit;
				// set disable button
			}
			

			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.Default;

			#endregion Code Inserted Automatically

			
		}

		//**************************************************************************              
		///    <Description>
		///      Save data in database
		///    </Description>
		///    <Inputs>
		///       object value 
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
		private void bntDelete_Click(object sender, System.EventArgs e)
		{
			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.WaitCursor;

			#endregion Code Inserted Automatically

			
			const string METHOD_NAME = THIS + ".btnDelete_Click()";
			try
			{
				if(PCSMessageBox.Show(ErrorCode.MESSAGE_DELETE_RECORD,MessageBoxButtons.YesNo) == DialogResult.Yes)
				{
					boTableConfigDetail.Delete(voTableField.TableFieldID);
					// Load data into field listbox
					LoadListField();
					if(lstField.Items.Count == 0)
					{
						btnAdd_Click(sender,e);
					}
				}
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

		//**************************************************************************              
		///    <Description>
		///       Load form and load data on form
		///    </Description>
		///    <Inputs>
		///       object value 
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
		private void TableConfigDetail_Load(object sender, System.EventArgs e)
		{
			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.WaitCursor;

			#endregion Code Inserted Automatically

			
			const string METHOD_NAME = THIS + ".TableConfigDetail_Load()";
			const string DOUBLE_COLON = " :: ";
			//Table Config Detail - [table code :: table name]
			this.Text = this.Text + mTableCode.Trim() + DOUBLE_COLON + mTableName.Trim() + Constants.CLOSE_SBRACKET; 
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
							

					return;
				}

				// Load data into FromTable combo
				TableConfigBO boTableConfig = new TableConfigBO();
				//DataSet dstTableName = boTableConfig.ListTableOrView();
				//ArrayList arrTableName = GetArrayFromDataSet(dstTableName, SchemaTableTable.TABLE_NAME);
				DataSet dstTableName = boTableConfig.List();
				ArrayList arrTableName = GetArrayFromDataSet(dstTableName, sys_TableTable.TABLE_NAME);
				cboFromTable.Items.Clear();
				cboFromTable.Items.Add(string.Empty);
				for (int i = 0; i < arrTableName.Count; i++)
				{
					DataRow row = (DataRow)arrTableName[i];
					cboFromTable.Items.Add(row[sys_TableTable.TABLEORVIEW_FLD].ToString().Trim());
				}
				cboFromTable.Sorted = true;

				// Load data into FieldName combo
				DataSet dstFieldName = boTableConfigDetail.ListFieldName(mTableOrView);
				ArrayList arrFieldName = GetArrayFromDataSet(dstFieldName, SchemaColumnTable.TABLE_NAME);
				for (int i = 0; i < arrFieldName.Count; i++)
				{
					DataRow row = (DataRow)arrFieldName[i];
					cboFieldName.Items.Add(row[SchemaColumnTable.COLUMN_NAME_FLD].ToString());
				}
				cboFieldName.Sorted = true;
				// Load data into field listbox
				LoadListField();

				// if list has no item
				if(lstField.Items.Count > 0)
				{
					// set first row
					lstField.SelectedIndex = 0;
				}
				else if(lstField.Items.Count == 0)
				{
					/// Datasource is empty	(after call LoadListField)					
					btnAdd_Click(btnAdd,new System.EventArgs());
				}
				//load information schema of this table
				dstInformationSchema = boTableConfigDetail.GetInformationSchema(this.mTableOrView);
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

		//**************************************************************************              
		///    <Description>
		///       close form
		///    </Description>
		///    <Inputs>
		///       object value 
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
		private void btnClose_Click(object sender, System.EventArgs e)
		{
			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.WaitCursor;

			#endregion Code Inserted Automatically

			
			// Close form
			Close();

			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.Default;

			#endregion Code Inserted Automatically

			
		}

		//**************************************************************************              
		///    <Description>
		///       Scroll select item on list field
		///    </Description>
		///    <Inputs>
		///       object value 
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
		private void btnUp_Click(object sender, System.EventArgs e)
		{
			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.WaitCursor;

			#endregion Code Inserted Automatically

			
			const string METHOD_NAME = THIS + ".lsField_SelectedIndexChanged()";
			// Get Upper item
			//MessageBox.Show(lstField.SelectedIndex.ToString());
			if(lstField.SelectedIndex <= 0)
			{
				#region Code Inserted Automatically

				this.Cursor = Cursors.Default;

				#endregion Code Inserted Automatically
				return;
			}
			try
			{
				int intSelectedIndex = lstField.SelectedIndex;
				TableConfigDetailBO boTableConfigDetail = new TableConfigDetailBO();
				// Current field
				voTableField.FieldOrder--;
				boTableConfigDetail.Update(voTableField);
				// prev field
				lstField.SelectedIndex--;
				voTableField.FieldOrder++;
				boTableConfigDetail.Update(voTableField);
				// Refresh list fields
				LoadListField();
				lstField.SetSelected(intSelectedIndex - 1, true);
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

		//**************************************************************************              
		///    <Description>
		///       Scroll select item on list field
		///    </Description>
		///    <Inputs>
		///       object value 
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
		private void btnDown_Click(object sender, System.EventArgs e)
		{
			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.WaitCursor;

			#endregion Code Inserted Automatically

			
			const string METHOD_NAME = THIS + ".lsField_SelectedIndexChanged()";
			// Get Upper item
			if(lstField.SelectedIndex == lstField.Items.Count - 1)
			{
				#region Code Inserted Automatically

				this.Cursor = Cursors.Default;

				#endregion Code Inserted Automatically
				return;
			}
			try
			{
				int intSelectedIndex = lstField.SelectedIndex;
				TableConfigDetailBO boTableConfigDetail = new TableConfigDetailBO();
				// Current field
				voTableField.FieldOrder++;
				boTableConfigDetail.Update(voTableField);
				// prev field
				lstField.SelectedIndex++;
				voTableField.FieldOrder--;
				boTableConfigDetail.Update(voTableField);
				// Refresh list fields
				LoadListField();
				lstField.SetSelected(intSelectedIndex + 1, true);
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

		//**************************************************************************              
		///    <Description>
		///       Scroll select item on list field
		///    </Description>
		///    <Inputs>
		///       object value 
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
		private void lsField_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".lsField_SelectedIndexChanged()";
			mEnumType = EnumAction.Edit;
			// Load infor to the controls on right form			
			try
			{
				TableConfigDetailBO boTableConfigDetail = new TableConfigDetailBO();				
				DataRowView row = (DataRowView)lstField.SelectedItem;
				voTableField = (sys_TableFieldVO) boTableConfigDetail.GetObjectVO(int.Parse(row[sys_TableFieldTable.TABLEFIELDID_FLD].ToString()));
				
				/// fill in form
				cboFieldName.Text = voTableField.FieldName.Trim();
				txtEnglish.Text = voTableField.CaptionEN.Trim();				
				txtVietnamese.Text = voTableField.CaptionVN.Trim();
				txtJapaness.Text = voTableField.CaptionJP.Trim();
				// cboCase
				if (voTableField.CharacterCase == INT_NORMAL)
				{
					cboCase.Text = STR_NORMAL;
				} 
				else if (voTableField.CharacterCase == INT_UPPER)
				{
					cboCase.Text = STR_UPPER;
				} 
				else if (voTableField.CharacterCase == INT_LOWER)
				{
					cboCase.Text = STR_LOWER;
				}
				// cboAlign
				if (voTableField.Align == INT_LEFT)
				{
					cboAlign.Text = STR_LEFT;
				} 
				else if (voTableField.Align == INT_CENTER)
				{
					cboAlign.Text = STR_CENTER;
				} 
				else if (voTableField.Align == INT_RIGHT)
				{
					cboAlign.Text = STR_RIGHT;
				}
				txtWidth.Text = voTableField.Width.ToString();
				// cboSortType
				if (voTableField.SortType == INT_NONE)
				{
					cboSortType.Text = STR_NONE;
				} 
				else if (voTableField.SortType == INT_ASCENDING)
				{
					cboSortType.Text = STR_ASCENDING;
				} 
				else if (voTableField.SortType == INT_DESCENDING)
				{
					cboSortType.Text = STR_DESCENDING;
				}
				txtFormat.Text = voTableField.Formats.Trim();
				
				// chkUnique
				chkUnique.Checked = voTableField.UniqueColumn;

				// chkNotAllowNull
				chkNotAllowNull.Checked = voTableField.NotAllowNull;

				// chkIdentity
				chkIdentity.Checked = voTableField.IdentityColumn;

				txtItems.Text = voTableField.Items.Trim();
				cboFromTable.Text = voTableField.FromTable.Trim();
				cboFromField.Text = voTableField.FromField.Trim();
				cboFilterField1.Text = voTableField.FilterField1.Trim();
				cboFilterField2.Text = voTableField.FilterField2.Trim();
				cboFilterField3.Text = voTableField.FilterField3.Trim();
				if (voTableField.FromTable.Trim() != string.Empty)
				{
					txtEnglish1.Text = voTableField.CaptionEN1;
					txtVietnamese1.Text = voTableField.CaptionVN1;
					txtJapanese1.Text = voTableField.CaptionJP1;
					txtWidth1.Value = voTableField.Width1;
					cboAlign1.SelectedIndex = voTableField.Align1;
					txtFormat1.Text = voTableField.Formats1;

					txtEnglish2.Text = voTableField.CaptionEN2;
					txtVietnamese2.Text = voTableField.CaptionVN2;
					txtJapanese2.Text = voTableField.CaptionJP2;
					txtWidth2.Value = voTableField.Width2;
					cboAlign2.SelectedIndex = voTableField.Align2;
					txtFormat2.Text = voTableField.Formats2;

					txtEnglish3.Text = voTableField.CaptionEN3;
					txtVietnamese3.Text = voTableField.CaptionVN3;
					txtJapanese3.Text = voTableField.CaptionJP3;
					txtWidth3.Value = voTableField.Width3;
					cboAlign3.SelectedIndex = voTableField.Align3;
					txtFormat3.Text = voTableField.Formats3;
				}
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
		//**************************************************************************              
		///    <Description>
		///       Check validate data
		///    </Description>
		///    <Inputs>
		///               
		///    </Inputs>
		///    <Outputs>
		///       
		///    </Outputs>
		///    <Returns>
		///       return true if correct data, return false if incorrect 
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
		private bool CheckData()
		{
			const int MAXWIDTH = 500;
			// Check Field name
			if(mEnumType == EnumAction.Add)
			{
				if(!IsDoubleFieldName())
				{
					PCSMessageBox.Show(ErrorCode.DUPLICATE_KEY, MessageBoxIcon.Error);
					cboFieldName.Focus();
					return false;
				}
			}
			// Check from table
			if(cboFromTable.Text.Trim().Length > 0)
			{
				if(cboFromField.Text.Trim().Length == 0) 
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID);
					cboFromField.Focus();
					return false;
				}
				if(cboFilterField1.Text.Trim().Length == 0) 
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID);
					cboFilterField1.Focus();
					return false;
				}
			}
			// Check negative number
			if(voTableField.Width < 0)
			{
				PCSMessageBox.Show(ErrorCode.POSITIVE_NUMBER, MessageBoxIcon.Error);
				txtWidth.Focus();
				return false;
			}
			// Check Max > 500 number
			if(voTableField.Width > MAXWIDTH)
			{
				// The Width field receive less than 500
				PCSMessageBox.Show(ErrorCode.MESSAGE_WIDTH_LESS_THAN_500, MessageBoxIcon.Error);
				txtWidth.Focus();
				return false;
			}
			return true;
		}

		private void cboFromTable_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".cboFromTable_SelectedIndexChanged()";
			//txtItems.Text = string.Empty;
			try
			{
				if ((cboFromTable.SelectedIndex != -1) && (cboFromTable.SelectedIndex != 0))
				{
					lblLinkField.ForeColor = Color.Maroon;
					//Get InformationSchemaRelate Table
					dstInformationSchemaRelatedTable = boTableConfigDetail.GetInformationSchema(cboFromTable.Text);
					//Clear all relate fields

					#region HACK: Trada 14-04-2006

					txtEnglish1.Text = string.Empty;
					txtEnglish2.Text = string.Empty;
					txtEnglish3.Text = string.Empty;
					txtJapanese1.Text = string.Empty;
					txtJapanese2.Text = string.Empty;
					txtJapanese3.Text = string.Empty;
					txtVietnamese1.Text = string.Empty;
					txtVietnamese3.Text = string.Empty;
					txtVietnamese2.Text = string.Empty;
					txtFormat1.Text = string.Empty;
					txtFormat2.Text = string.Empty;
					txtFormat3.Text = string.Empty;
					txtWidth1.Value = 0;
					txtWidth3.Value = 0;
					txtWidth2.Value = 0;
					cboAlign1.SelectedIndex = -1;
					cboAlign2.SelectedIndex = -1;
					cboAlign3.SelectedIndex = -1;

					#endregion END: Trada 14-04-2006
				}
				else
					lblLinkField.ForeColor = Color.Black;	
				// Load data into FromField combo
				//ComboBox cboFromTable = (ComboBox)sender;
				boTableConfigDetail = new TableConfigDetailBO();// .List(cboFromTable.Text.Trim());
				//DataSet dstFieldName = boTableConfigDetail.ListFieldName(cboFromTable.Text.Trim());
				//ArrayList arrFromField = GetArrayFromDataSet(dstFieldName, sys_TableFieldTable.TABLE_NAME);
				DataSet dstFieldName = boTableConfigDetail.List(cboFromTable.Text.Trim());
				ArrayList arrFromField = GetArrayFromDataSet(dstFieldName, sys_TableFieldTable.TABLE_NAME);
				cboFromField.Items.Clear();
				for (int i = 0; i < arrFromField.Count; i++)
				{
					DataRow row = (DataRow)arrFromField[i];
					cboFromField.Items.Add(row[sys_TableFieldTable.FIELDNAME_FLD].ToString().Trim());
				}
				cboFromField.Sorted = true;
				// Load data into FilterField1 combo
				cboFilterField1.Items.Clear();
				cboFilterField1.Items.Add(string.Empty);
				for (int i = 0; i < arrFromField.Count; i++)
				{
					DataRow row = (DataRow)arrFromField[i];
					cboFilterField1.Items.Add(row[sys_TableFieldTable.FIELDNAME_FLD].ToString().Trim());
				}
				cboFilterField1.Sorted = true;
				// Load data into FilterField2 combo
				cboFilterField2.Items.Clear();
				cboFilterField2.Items.Add(string.Empty);
				for (int i = 0; i < arrFromField.Count; i++)
				{
					DataRow row = (DataRow)arrFromField[i];
					cboFilterField2.Items.Add(row[sys_TableFieldTable.FIELDNAME_FLD].ToString().Trim());
				}
				cboFilterField2.Sorted = true;
			// Load data into FilterField3 combo
				cboFilterField3.Items.Clear();
				cboFilterField3.Items.Add(string.Empty);
				for (int i = 0; i < arrFromField.Count; i++)
				{
					DataRow row = (DataRow)arrFromField[i];
					cboFilterField3.Items.Add(row[sys_TableFieldTable.FIELDNAME_FLD].ToString().Trim());
				}
				cboFilterField3.Sorted = true;
				
				//clear relate-information when from table is null
				if ((cboFromTable.SelectedIndex == -1) || (cboFromTable.SelectedIndex == 0))
				{
					cboFilterField1.SelectedIndex = -1;
					cboFilterField2.SelectedIndex = -1;
					cboFilterField3.SelectedIndex = -1;
					txtEnglish1.Text = string.Empty;
					txtVietnamese1.Text = string.Empty;
					txtJapanese1.Text = string.Empty;
					txtWidth1.Value = 0;
					cboAlign1.SelectedIndex = -1;
					txtFormat1.Text = string.Empty;

					txtEnglish2.Text = string.Empty;
					txtVietnamese2.Text = string.Empty;
					txtJapanese2.Text = string.Empty;
					txtWidth2.Value = 0;
					cboAlign2.SelectedIndex = -1;
					txtFormat2.Text = string.Empty;

					txtEnglish3.Text = string.Empty;
					txtVietnamese3.Text = string.Empty;
					txtJapanese3.Text = string.Empty;
					txtWidth3.Value = 0;
					cboAlign3.SelectedIndex = -1;
					txtFormat3.Text = string.Empty;
				}
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

		private void cboFieldName_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".cboFieldName_SelectedIndexChanged()";
			
			try
			{
				string strFieldName = cboFieldName.Text.Trim();
				txtEnglish.Text = strFieldName;
				txtJapaness.Text = strFieldName;
				txtVietnamese.Text = strFieldName;
				TableConfigDetailBO boTableConfigDetail = new TableConfigDetailBO();
				ArrayList arrColumnProperty = boTableConfigDetail.GetColumnProperty(mTableOrView,strFieldName);
				int intValue = (int)arrColumnProperty[0];
				chkIdentity.Checked = (intValue > 0) ? true : false;

				#region added by duongna to update chkUnique value at runtime
				intValue = (int)arrColumnProperty[1];
				chkUnique.Checked = (intValue > 0) ? true : false;
				#endregion end added by duongna

				intValue = (int)arrColumnProperty[2];
				chkNotAllowNull.Checked = (intValue > 0) ? false : true;
				#region HACK: Trada 14-04-2006
				if ((dstInformationSchema != null)&&(dstInformationSchema.Tables.Count > 0))
				{
					//Get data type of this field name
					DataRow[] adrowDataType = dstInformationSchema.Tables[0].Select(COLUMN_NAME + " = '" + strFieldName + "'");
					if (adrowDataType.Length > 0)
					{
						if ((adrowDataType[0][DATA_TYPE].ToString() == BIGINT_TYPE)
							||(adrowDataType[0][DATA_TYPE].ToString() == INT_TYPE)
							||(adrowDataType[0][DATA_TYPE].ToString() == SMALLINT_TYPE)
							||(adrowDataType[0][DATA_TYPE].ToString() == DECIMAL_TYPE)
							||(adrowDataType[0][DATA_TYPE].ToString() == TINYINT_TYPE)
							||(adrowDataType[0][DATA_TYPE].ToString() == FLOAT_TYPE))
						{
							cboAlign.Text = STR_RIGHT;
						}
						else
						{
							if ((adrowDataType[0][DATA_TYPE].ToString() == CHAR_TYPE)
								||(adrowDataType[0][DATA_TYPE].ToString() == NVARCHAR_TYPE)
								||(adrowDataType[0][DATA_TYPE].ToString() == VARCHAR_TYPE))
							{
								cboAlign.Text = STR_LEFT;
							}
							else
							{
								cboAlign.Text = STR_CENTER;
							}
						}
					} 
				}
				#endregion END: Trada 14-04-2006

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
	
		//**************************************************************************              
		///    <Description>
		///       Convert data form Dataset to ArrayList
		///    </Description>
		///    <Inputs>
		///               
		///    </Inputs>
		///    <Outputs>
		///       
		///    </Outputs>
		///    <Returns>
		///       return true if correct data, return false if incorrect 
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
		private ArrayList GetArrayFromDataSet(DataSet pdstDataSet,string pstrTableName)
		{
			const string METHOD_NAME = THIS + ".GetArrayFromDataSet()";
			ArrayList arrList = new ArrayList();
			try
			{				
				foreach (DataRow row in pdstDataSet.Tables[pstrTableName].Rows)
				{
					arrList.Add(row);
				}
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
			return arrList;
		}

		//**************************************************************************              
		///    <Description>
		///       check Double FieldName
		///    </Description>
		///    <Inputs>
		///               
		///    </Inputs>
		///    <Outputs>
		///       
		///    </Outputs>
		///    <Returns>
		///       return true if correct data, return false if incorrect 
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
		private bool IsDoubleFieldName()
		{
			const string METHOD_NAME = THIS + ".IsDoubleFieldName()";
			try
			{
				DataTable objDataTable = (DataTable)lstField.DataSource;
				foreach(DataRow row in objDataTable.Rows)
				{
					if( row[sys_TableFieldTable.FIELDNAME_FLD].ToString().Trim() == cboFieldName.Text.Trim())
					{					
						return false;
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
			return true;
		}

		//**************************************************************************              
		///    <Description>
		///       Load List Field
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
		private void LoadListField()
		{
			const string METHOD_NAME = THIS + ".LoadListField()";
			try
			{
				// lstField.Items.Clear();
				DataSet dstList = boTableConfigDetail.List(mTableID);
				lstField.DisplayMember = sys_TableFieldTable.FIELDNAME_FLD;
				lstField.ValueMember = sys_TableFieldTable.TABLEFIELDID_FLD;
				lstField.DataSource = dstList.Tables[sys_TableFieldTable.TABLE_NAME];								
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

		private void OnControlEnter(object sender, System.EventArgs e)
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

		private void OnControlLeave(object sender, System.EventArgs e)
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
		/// <summary>
		/// cboFilterField1_SelectedIndexChanged
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Tuesday, April 4 2006</date>
		private void cboFilterField1_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			string METHOD_NAME = THIS + ".cboFilterField1_SelectedIndexChanged()";
			try 
			{
				string strFieldName = cboFilterField1.Text.Trim();
				if (strFieldName != string.Empty)
				{
					txtEnglish1.Text = strFieldName;
					txtJapanese1.Text = strFieldName;
					txtVietnamese1.Text = strFieldName;
					#region HACK: Trada 14-04-2006
					if ((dstInformationSchemaRelatedTable != null)&&(dstInformationSchemaRelatedTable.Tables.Count > 0))
					{
						DataRow[] adrowDataType = dstInformationSchemaRelatedTable.Tables[0].Select(COLUMN_NAME + " = '" + strFieldName + "'");
						if (adrowDataType.Length > 0)
						{
							if ((adrowDataType[0][DATA_TYPE].ToString() == BIGINT_TYPE)
								||(adrowDataType[0][DATA_TYPE].ToString() == INT_TYPE)
								||(adrowDataType[0][DATA_TYPE].ToString() == SMALLINT_TYPE)
								||(adrowDataType[0][DATA_TYPE].ToString() == DECIMAL_TYPE)
								||(adrowDataType[0][DATA_TYPE].ToString() == TINYINT_TYPE)
								||(adrowDataType[0][DATA_TYPE].ToString() == FLOAT_TYPE))
							{
								cboAlign1.Text = STR_RIGHT;
							}
							else
							{
								if ((adrowDataType[0][DATA_TYPE].ToString() == CHAR_TYPE)
									||(adrowDataType[0][DATA_TYPE].ToString() == NVARCHAR_TYPE)
									||(adrowDataType[0][DATA_TYPE].ToString() == VARCHAR_TYPE))
								{
									cboAlign1.Text = STR_LEFT;
								}
								else
								{
									cboAlign1.Text = STR_CENTER;
								}
							}
						}
					}
					#endregion END: Trada 14-04-2006
					txtWidth1.Value = 100;
				}
				else
				{
					txtEnglish1.Text = String.Empty;
					txtJapanese1.Text = String.Empty;
					txtVietnamese1.Text = String.Empty;
					cboAlign1.SelectedIndex = -1;
					txtFormat1.Text = string.Empty;
					txtWidth1.Value = 0;
				}
//				TableConfigDetailBO boTableConfigDetail = new TableConfigDetailBO();
//				ArrayList arrColumnProperty = boTableConfigDetail.GetColumnProperty(mTableOrView,strFieldName);
//				int intValue = (int)arrColumnProperty[0];
//				chkIdentity.Checked = (intValue > 0) ? true : false;
//		
//				intValue = (int)arrColumnProperty[1];
//				chkUnique.Checked = (intValue > 0) ? true : false;
//				intValue = (int)arrColumnProperty[2];
//				chkNotAllowNull.Checked = (intValue > 0) ? false : true;
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
		/// cboFilterField2_SelectedIndexChanged
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Tuesday, April 4 2006</date>
		private void cboFilterField2_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			string METHOD_NAME = THIS + ".cboFilterField2_SelectedIndexChanged()";
			try 
			{
				if (cboFilterField2.Items.Count > 1)
				{
					if (((cboFilterField2.SelectedIndex == -1) || (cboFilterField2.SelectedIndex == 0))
						&& ((cboFilterField3.SelectedIndex != -1) && (cboFilterField3.SelectedIndex != 0)))
					{
						string[] strParam = new string[2];
						strParam[0] = lblFilter3.Text;
						strParam[1] = lblFilter2.Text;
						PCSMessageBox.Show(ErrorCode.MESSAGE_PLS_CLEAR_RELATE_FIRST, MessageBoxIcon.Warning, strParam);
						cboFilterField2.SelectedIndex = intOldValue;
						return;
					}
					intOldValue = cboFilterField2.SelectedIndex;
				}
				string strFieldName = cboFilterField2.Text.Trim();
				if (strFieldName != string.Empty)
				{
					txtEnglish2.Text = strFieldName;
					txtJapanese2.Text = strFieldName;
					txtVietnamese2.Text = strFieldName;
					#region HACK: Trada 14-04-2006
					if ((dstInformationSchemaRelatedTable != null)&&(dstInformationSchemaRelatedTable.Tables.Count > 0))
					{
						DataRow[] adrowDataType = dstInformationSchemaRelatedTable.Tables[0].Select(COLUMN_NAME + " = '" + strFieldName + "'");
						if (adrowDataType.Length > 0)
						{
							if ((adrowDataType[0][DATA_TYPE].ToString() == BIGINT_TYPE)
								||(adrowDataType[0][DATA_TYPE].ToString() == INT_TYPE)
								||(adrowDataType[0][DATA_TYPE].ToString() == SMALLINT_TYPE)
								||(adrowDataType[0][DATA_TYPE].ToString() == DECIMAL_TYPE)
								||(adrowDataType[0][DATA_TYPE].ToString() == TINYINT_TYPE)
								||(adrowDataType[0][DATA_TYPE].ToString() == FLOAT_TYPE))
							{
								cboAlign2.Text = STR_RIGHT;
							}
							else
							{
								if ((adrowDataType[0][DATA_TYPE].ToString() == CHAR_TYPE)
									||(adrowDataType[0][DATA_TYPE].ToString() == NVARCHAR_TYPE)
									||(adrowDataType[0][DATA_TYPE].ToString() == VARCHAR_TYPE))
								{
									cboAlign2.Text = STR_LEFT;
								}
								else
								{
									cboAlign2.Text = STR_CENTER;
								}
							}
						}
					}
					#endregion END: Trada 14-04-2006
					txtWidth2.Value = 100;
				}
				else
				{
					txtEnglish2.Text = String.Empty;
					txtJapanese2.Text = String.Empty;
					txtVietnamese2.Text = String.Empty;
					cboAlign2.SelectedIndex = -1;
					txtWidth2.Value = 0;
					txtFormat2.Text = string.Empty;
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
		/// cboFilterField3_SelectedIndexChanged
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Tuesday, April 4 2006</date>
		private void cboFilterField3_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			string METHOD_NAME = THIS + ".cboFilterField3_SelectedIndexChanged()";
			try 
			{
				if (((cboFilterField2.SelectedIndex == -1)||(cboFilterField2.SelectedIndex == 0)) 
					&& (cboFilterField3.SelectedIndex != -1) && (cboFilterField3.SelectedIndex != 0))
				{
					string[] strParam = new string[2];
					strParam[0] = lblFilter2.Text;
					strParam[1] = lblFilter3.Text;
					PCSMessageBox.Show(ErrorCode.MESSAGE_SELECT_ONE_BEFORE_SELECT_ONE, MessageBoxIcon.Warning, strParam);
					cboFilterField3.SelectedIndex = -1;
					cboFilterField2.Focus();
					return;
				}
				string strFieldName = cboFilterField3.Text.Trim();
				
				if (strFieldName != string.Empty)
				{
					txtEnglish3.Text = strFieldName;
					txtJapanese3.Text = strFieldName;
					txtVietnamese3.Text = strFieldName;
					#region HACK: Trada 14-04-2006
					if ((dstInformationSchemaRelatedTable != null)&&(dstInformationSchemaRelatedTable.Tables.Count > 0))
					{
						DataRow[] adrowDataType = dstInformationSchemaRelatedTable.Tables[0].Select(COLUMN_NAME + " = '" + strFieldName + "'");
						if (adrowDataType.Length > 0)
						{
							if ((adrowDataType[0][DATA_TYPE].ToString() == BIGINT_TYPE)
								||(adrowDataType[0][DATA_TYPE].ToString() == INT_TYPE)
								||(adrowDataType[0][DATA_TYPE].ToString() == SMALLINT_TYPE)
								||(adrowDataType[0][DATA_TYPE].ToString() == DECIMAL_TYPE)
								||(adrowDataType[0][DATA_TYPE].ToString() == TINYINT_TYPE)
								||(adrowDataType[0][DATA_TYPE].ToString() == FLOAT_TYPE))
							{
								cboAlign3.Text = STR_RIGHT;
							}
							else
							{
								if ((adrowDataType[0][DATA_TYPE].ToString() == CHAR_TYPE)
									||(adrowDataType[0][DATA_TYPE].ToString() == NVARCHAR_TYPE)
									||(adrowDataType[0][DATA_TYPE].ToString() == VARCHAR_TYPE))
								{
									cboAlign3.Text = STR_LEFT;
								}
								else
								{
									cboAlign3.Text = STR_CENTER;
								}
							}
						}
					}
					#endregion END: Trada 14-04-2006
					txtWidth3.Value = 100;
				}
				else
				{
					txtEnglish3.Text = String.Empty;
					txtJapanese3.Text = String.Empty;
					txtVietnamese3.Text = String.Empty;
					cboAlign3.SelectedIndex = -1;
					txtWidth3.Value = 0;
					txtFormat3.Text = string.Empty;
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
		/// cboFromField_SelectedIndexChanged
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Tuesday, April 11 2006</date>
		private void cboFromField_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			string METHOD_NAME = THIS + ".cboFromField_SelectedIndexChanged()";
			try 
			{
				if (cboFromField.SelectedIndex != -1) 
				{
					lblFilter1.ForeColor = Color.Maroon;
				}
				else
					lblFilter1.ForeColor = Color.Black;
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

		private void cboFilterField2_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if (cboFilterField2.Items.Count > 1)
			{
				if ((cboFilterField2.SelectedIndex == -1) || (cboFilterField2.SelectedIndex == 0))
				{
					string[] strParam = new string[2];
					strParam[0] = lblFilter3.Text;
					strParam[1] = lblFilter2.Text;
					PCSMessageBox.Show(ErrorCode.MESSAGE_PLS_CLEAR_RELATE_FIRST, MessageBoxIcon.Warning, strParam);
					return;
				}
			}
		}
		
	}
}
