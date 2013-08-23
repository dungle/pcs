using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Data;
using PCSUtils.Utils;
using PCSComUtils.PCSExc;
using PCSComUtils.Common;
using PCSComProduct.Items.BO;
using PCSComProduct.Items.DS;
using PCSUtils.Log;

namespace PCSProduct.Items
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Catalog : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button btnHelp;
		private System.Windows.Forms.Button btnClose;
		private System.Windows.Forms.Button btnDelete;
		private System.Windows.Forms.Button btnSave;
		private System.Windows.Forms.Button btnEdit;
		private System.Windows.Forms.Button btnAdd;
		private System.Windows.Forms.Label lblCategoryNameVN;
		private System.Windows.Forms.Label lblDesc;
		private System.Windows.Forms.Label lblCategoryName;
		private System.Windows.Forms.Label lblCategoryCode;
		private System.Windows.Forms.Label lblRootCaption;
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.TreeView tvwCategory;

		/// <summary>
		/// Required designer variable.
		/// </summary>		
		private const string THIS = "PCSProduct.Items.Catalog";
		private const string CHECKED = "Checked";
		private const string TRUE	= "True";
		private const string ROOT = "Root";
		
		private EnumAction enumAction = EnumAction.Default;
		private System.Windows.Forms.TextBox txtProductCode;
		private System.Windows.Forms.TextBox txtProdcutName;
		private System.Windows.Forms.TextBox txtCategoryNameVN;
		private System.Windows.Forms.TextBox txtDesc;
		private System.Windows.Forms.Label lblPicture;
		private System.Windows.Forms.PictureBox picCategory;
		private System.Windows.Forms.Button btnChangePicture;
		private System.Windows.Forms.OpenFileDialog dlgSelectPicture;
		private System.Windows.Forms.Button btnClearPicture;
		
		private bool blnHasError = false;
		
		public Catalog()
		{
			InitializeComponent();
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
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
			this.btnHelp = new System.Windows.Forms.Button();
			this.btnClose = new System.Windows.Forms.Button();
			this.btnEdit = new System.Windows.Forms.Button();
			this.btnAdd = new System.Windows.Forms.Button();
			this.btnSave = new System.Windows.Forms.Button();
			this.btnDelete = new System.Windows.Forms.Button();
			this.lblDesc = new System.Windows.Forms.Label();
			this.lblCategoryName = new System.Windows.Forms.Label();
			this.lblCategoryCode = new System.Windows.Forms.Label();
			this.tvwCategory = new System.Windows.Forms.TreeView();
			this.lblRootCaption = new System.Windows.Forms.Label();
			this.lblCategoryNameVN = new System.Windows.Forms.Label();
			this.txtProductCode = new System.Windows.Forms.TextBox();
			this.txtProdcutName = new System.Windows.Forms.TextBox();
			this.txtCategoryNameVN = new System.Windows.Forms.TextBox();
			this.txtDesc = new System.Windows.Forms.TextBox();
			this.lblPicture = new System.Windows.Forms.Label();
			this.picCategory = new System.Windows.Forms.PictureBox();
			this.btnChangePicture = new System.Windows.Forms.Button();
			this.dlgSelectPicture = new System.Windows.Forms.OpenFileDialog();
			this.btnClearPicture = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// btnHelp
			// 
			this.btnHelp.AccessibleDescription = "";
			this.btnHelp.AccessibleName = "";
			this.btnHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnHelp.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnHelp.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnHelp.Location = new System.Drawing.Point(587, 380);
			this.btnHelp.Name = "btnHelp";
			this.btnHelp.Size = new System.Drawing.Size(64, 23);
			this.btnHelp.TabIndex = 17;
			this.btnHelp.Text = "&Help";
			// 
			// btnClose
			// 
			this.btnClose.AccessibleDescription = "";
			this.btnClose.AccessibleName = "";
			this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnClose.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnClose.Location = new System.Drawing.Point(652, 380);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(64, 23);
			this.btnClose.TabIndex = 18;
			this.btnClose.Text = "&Close";
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			// 
			// btnEdit
			// 
			this.btnEdit.AccessibleDescription = "";
			this.btnEdit.AccessibleName = "";
			this.btnEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnEdit.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnEdit.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnEdit.Location = new System.Drawing.Point(134, 380);
			this.btnEdit.Name = "btnEdit";
			this.btnEdit.Size = new System.Drawing.Size(64, 23);
			this.btnEdit.TabIndex = 14;
			this.btnEdit.Text = "&Edit";
			this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
			// 
			// btnAdd
			// 
			this.btnAdd.AccessibleDescription = "";
			this.btnAdd.AccessibleName = "";
			this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnAdd.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnAdd.Location = new System.Drawing.Point(4, 380);
			this.btnAdd.Name = "btnAdd";
			this.btnAdd.Size = new System.Drawing.Size(64, 23);
			this.btnAdd.TabIndex = 12;
			this.btnAdd.Text = "&Add";
			this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
			// 
			// btnSave
			// 
			this.btnSave.AccessibleDescription = "";
			this.btnSave.AccessibleName = "";
			this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnSave.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnSave.Location = new System.Drawing.Point(69, 380);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(64, 23);
			this.btnSave.TabIndex = 13;
			this.btnSave.Text = "&Save";
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			// 
			// btnDelete
			// 
			this.btnDelete.AccessibleDescription = "";
			this.btnDelete.AccessibleName = "";
			this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnDelete.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnDelete.Location = new System.Drawing.Point(199, 380);
			this.btnDelete.Name = "btnDelete";
			this.btnDelete.Size = new System.Drawing.Size(64, 23);
			this.btnDelete.TabIndex = 15;
			this.btnDelete.Text = "&Delete";
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
			// 
			// lblDesc
			// 
			this.lblDesc.AccessibleDescription = "";
			this.lblDesc.AccessibleName = "";
			this.lblDesc.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.lblDesc.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lblDesc.Location = new System.Drawing.Point(270, 79);
			this.lblDesc.Name = "lblDesc";
			this.lblDesc.Size = new System.Drawing.Size(112, 20);
			this.lblDesc.TabIndex = 8;
			this.lblDesc.Text = "Description";
			this.lblDesc.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblCategoryName
			// 
			this.lblCategoryName.AccessibleDescription = "";
			this.lblCategoryName.AccessibleName = "";
			this.lblCategoryName.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.lblCategoryName.ForeColor = System.Drawing.Color.Maroon;
			this.lblCategoryName.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lblCategoryName.Location = new System.Drawing.Point(270, 28);
			this.lblCategoryName.Name = "lblCategoryName";
			this.lblCategoryName.Size = new System.Drawing.Size(111, 20);
			this.lblCategoryName.TabIndex = 4;
			this.lblCategoryName.Text = "Category Name";
			this.lblCategoryName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblCategoryCode
			// 
			this.lblCategoryCode.AccessibleDescription = "";
			this.lblCategoryCode.AccessibleName = "";
			this.lblCategoryCode.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.lblCategoryCode.ForeColor = System.Drawing.Color.Maroon;
			this.lblCategoryCode.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lblCategoryCode.Location = new System.Drawing.Point(270, 4);
			this.lblCategoryCode.Name = "lblCategoryCode";
			this.lblCategoryCode.Size = new System.Drawing.Size(112, 20);
			this.lblCategoryCode.TabIndex = 1;
			this.lblCategoryCode.Text = "Category Code";
			this.lblCategoryCode.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// tvwCategory
			// 
			this.tvwCategory.AccessibleDescription = "";
			this.tvwCategory.AccessibleName = "";
			this.tvwCategory.HideSelection = false;
			this.tvwCategory.ImageIndex = -1;
			this.tvwCategory.Indent = 19;
			this.tvwCategory.ItemHeight = 16;
			this.tvwCategory.Location = new System.Drawing.Point(4, 4);
			this.tvwCategory.Name = "tvwCategory";
			this.tvwCategory.SelectedImageIndex = -1;
			this.tvwCategory.Size = new System.Drawing.Size(262, 370);
			this.tvwCategory.TabIndex = 0;
			this.tvwCategory.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvwCategory_AfterSelect);
			// 
			// lblRootCaption
			// 
			this.lblRootCaption.AccessibleDescription = "";
			this.lblRootCaption.AccessibleName = "";
			this.lblRootCaption.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.lblRootCaption.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lblRootCaption.Location = new System.Drawing.Point(274, 382);
			this.lblRootCaption.Name = "lblRootCaption";
			this.lblRootCaption.Size = new System.Drawing.Size(124, 23);
			this.lblRootCaption.TabIndex = 16;
			this.lblRootCaption.Text = "The list of categories";
			this.lblRootCaption.Visible = false;
			// 
			// lblCategoryNameVN
			// 
			this.lblCategoryNameVN.AccessibleDescription = "";
			this.lblCategoryNameVN.AccessibleName = "";
			this.lblCategoryNameVN.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.lblCategoryNameVN.ForeColor = System.Drawing.Color.Black;
			this.lblCategoryNameVN.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lblCategoryNameVN.Location = new System.Drawing.Point(270, 52);
			this.lblCategoryNameVN.Name = "lblCategoryNameVN";
			this.lblCategoryNameVN.Size = new System.Drawing.Size(112, 20);
			this.lblCategoryNameVN.TabIndex = 6;
			this.lblCategoryNameVN.Text = "Category Name (VN)";
			this.lblCategoryNameVN.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// txtProductCode
			// 
			this.txtProductCode.Location = new System.Drawing.Point(374, 4);
			this.txtProductCode.MaxLength = 20;
			this.txtProductCode.Name = "txtProductCode";
			this.txtProductCode.Size = new System.Drawing.Size(168, 20);
			this.txtProductCode.TabIndex = 2;
			this.txtProductCode.Text = "";
			// 
			// txtProdcutName
			// 
			this.txtProdcutName.Location = new System.Drawing.Point(374, 28);
			this.txtProdcutName.MaxLength = 100;
			this.txtProdcutName.Name = "txtProdcutName";
			this.txtProdcutName.Size = new System.Drawing.Size(168, 20);
			this.txtProdcutName.TabIndex = 5;
			this.txtProdcutName.Text = "";
			// 
			// txtCategoryNameVN
			// 
			this.txtCategoryNameVN.Location = new System.Drawing.Point(374, 52);
			this.txtCategoryNameVN.MaxLength = 100;
			this.txtCategoryNameVN.Name = "txtCategoryNameVN";
			this.txtCategoryNameVN.Size = new System.Drawing.Size(168, 20);
			this.txtCategoryNameVN.TabIndex = 7;
			this.txtCategoryNameVN.Text = "";
			// 
			// txtDesc
			// 
			this.txtDesc.Location = new System.Drawing.Point(272, 101);
			this.txtDesc.MaxLength = 200;
			this.txtDesc.Multiline = true;
			this.txtDesc.Name = "txtDesc";
			this.txtDesc.Size = new System.Drawing.Size(444, 273);
			this.txtDesc.TabIndex = 11;
			this.txtDesc.Text = "";
			// 
			// lblPicture
			// 
			this.lblPicture.Location = new System.Drawing.Point(544, 4);
			this.lblPicture.Name = "lblPicture";
			this.lblPicture.Size = new System.Drawing.Size(44, 23);
			this.lblPicture.TabIndex = 3;
			this.lblPicture.Text = "Picture";
			this.lblPicture.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// picCategory
			// 
			this.picCategory.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.picCategory.Location = new System.Drawing.Point(584, 4);
			this.picCategory.Name = "picCategory";
			this.picCategory.Size = new System.Drawing.Size(132, 68);
			this.picCategory.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
			this.picCategory.TabIndex = 17;
			this.picCategory.TabStop = false;
			// 
			// btnChangePicture
			// 
			this.btnChangePicture.Location = new System.Drawing.Point(662, 74);
			this.btnChangePicture.Name = "btnChangePicture";
			this.btnChangePicture.Size = new System.Drawing.Size(54, 23);
			this.btnChangePicture.TabIndex = 10;
			this.btnChangePicture.Text = "C&hange";
			this.btnChangePicture.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btnChangePicture.Click += new System.EventHandler(this.btnChangePicture_Click);
			// 
			// dlgSelectPicture
			// 
			this.dlgSelectPicture.Filter = "Image Files |*.jpg|*.bmp|*.gif";
			this.dlgSelectPicture.RestoreDirectory = true;
			this.dlgSelectPicture.Title = "Select Image File";
			// 
			// btnClearPicture
			// 
			this.btnClearPicture.Location = new System.Drawing.Point(583, 74);
			this.btnClearPicture.Name = "btnClearPicture";
			this.btnClearPicture.Size = new System.Drawing.Size(79, 23);
			this.btnClearPicture.TabIndex = 9;
			this.btnClearPicture.Text = "C&lear Picture";
			this.btnClearPicture.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btnClearPicture.Click += new System.EventHandler(this.btnClearPicture_Click);
			// 
			// Catalog
			// 
			this.AccessibleDescription = "";
			this.AccessibleName = "";
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(722, 407);
			this.Controls.Add(this.btnClearPicture);
			this.Controls.Add(this.btnChangePicture);
			this.Controls.Add(this.picCategory);
			this.Controls.Add(this.lblPicture);
			this.Controls.Add(this.txtDesc);
			this.Controls.Add(this.txtCategoryNameVN);
			this.Controls.Add(this.txtProdcutName);
			this.Controls.Add(this.txtProductCode);
			this.Controls.Add(this.lblCategoryNameVN);
			this.Controls.Add(this.lblRootCaption);
			this.Controls.Add(this.tvwCategory);
			this.Controls.Add(this.lblDesc);
			this.Controls.Add(this.lblCategoryName);
			this.Controls.Add(this.lblCategoryCode);
			this.Controls.Add(this.btnHelp);
			this.Controls.Add(this.btnClose);
			this.Controls.Add(this.btnEdit);
			this.Controls.Add(this.btnAdd);
			this.Controls.Add(this.btnSave);
			this.Controls.Add(this.btnDelete);
			this.DockPadding.All = 2;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.KeyPreview = true;
			this.MaximizeBox = false;
			this.Name = "Catalog";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Product Category";
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Catalog_KeyDown);
			this.Closing += new System.ComponentModel.CancelEventHandler(this.Catalog_Closing);
			this.Load += new System.EventHandler(this.Catalog_Load);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		//-------------------
		private void Catalog_Load(object sender, System.EventArgs e)
		{
			string METHOD_NAME = THIS + ".Catalog_Load()";
			try
			{
				//Set authorization for user
				Security objSecurity = new Security();
				this.Name = THIS;
				if (objSecurity.SetRightForUserOnForm(this, SystemProperty.UserName) == 0)
				{
					this.Close();
					return;
				}

				//Load form
				LoadTree();
				ResetForm();
				ControlState(false);
				btnEdit.Enabled = false;
				tvwCategory.Focus();
				tvwCategory.SelectedNode = tvwCategory.Nodes[0];
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

		//**************************************************************************              
		///    <Description>
		///		Reset all control
		///    </Description>
		///    <Inputs>
		///    </Inputs>
		///    <Outputs>
		///    </Outputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <Authors>
		///       TuanDm
		///    </Authors>
		///    <History>
		///       Monday, Mar 14, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private void ResetForm()
		{
			txtProductCode.Text = string.Empty;
			txtProdcutName.Text = string.Empty;
			txtCategoryNameVN.Text = string.Empty;
			txtDesc.Text = string.Empty;
			picCategory.Image = null;
			txtProductCode.Focus();
		}

		//**************************************************************************              
		///    <Description>
		///     Enabale and Disbale control if form changed the state
		///    </Description>
		///    <Inputs>
		///		pblnState : bool
		///    </Inputs>
		///    <Outputs>
		///    </Outputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <Authors>
		///       TuanDm
		///    </Authors>
		///    <History>
		///       Monday, Mar 14, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private void ControlState(bool pblnState)
		{
			switch(enumAction)
			{
				case EnumAction.Default:
					txtProductCode.Enabled = false;
					txtProdcutName.Enabled = false;
					txtDesc.Enabled = false;
					txtCategoryNameVN.Enabled = false;
					btnClearPicture.Enabled = false;
					btnChangePicture.Enabled = false;
					btnAdd.Enabled = true;
					btnDelete.Enabled = false;
					btnEdit.Enabled = false;
					btnSave.Enabled = false;
					break;
				default:
					txtProductCode.Enabled = true && (enumAction == EnumAction.Add);
					txtProdcutName.Enabled = true;
					txtDesc.Enabled = true;
					txtCategoryNameVN.Enabled = true;
					btnClearPicture.Enabled = true;
					btnChangePicture.Enabled = true;
					btnSave.Enabled = true;
					btnAdd.Enabled = false;
					btnDelete.Enabled = false;
					btnEdit.Enabled = false;
					break;			
			}
		}

		//**************************************************************************              
		///    <Description>
		///		Get data from tree node to bind in the form
		///    </Description>
		///    <Inputs>
		///    </Inputs>
		///    <Outputs>
		///    </Outputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <Authors>
		///       TuanDm
		///    </Authors>
		///    <History>
		///       Monday, Mar 14, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private void BindDataFromNodeToForm(TreeNode pSelectNode)
		{
			ResetForm();
			ControlState(false);

			ITM_CategoryVO voCategory = new ITM_CategoryVO();
			voCategory = (ITM_CategoryVO) pSelectNode.Tag;
			txtProductCode.Text = voCategory.Code;
			txtProdcutName.Text = voCategory.Name;
			txtDesc.Text = voCategory.Description;
			txtCategoryNameVN.Text = voCategory.CatalogNameVN;
			picCategory.Image = voCategory.Picture;
		}

		//**************************************************************************              
		///    <Description>
		///		Get data from form into the category object
		///    </Description>
		///    <Inputs>
		///    </Inputs>
		///    <Outputs>
		///    </Outputs>
		///    <Returns>
		///       ITM_CategoryVO
		///    </Returns>
		///    <Authors>
		///       TuanDm
		///    </Authors>
		///    <History>
		///       Monday, Mar 14, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private ITM_CategoryVO GetDataFromFormToObj()
		{
			ITM_CategoryVO voCategory = new ITM_CategoryVO();
			voCategory.Code = txtProductCode.Text.ToString();
			voCategory.Name = txtProdcutName.Text.ToString();
			voCategory.CatalogNameVN = txtCategoryNameVN.Text.ToString();
			voCategory.Description = txtDesc.Text.ToString();
			if (picCategory.Image != null)
				voCategory.Picture = new Bitmap(picCategory.Image);
			else
				voCategory.Picture = null;
			return voCategory;
		}

		//**************************************************************************              
		///    <Description>
		///       Load Category tree
		///    </Description>
		///    <Inputs>
		///       ptvwTreeView   
		///    </Inputs>
		///    <Outputs>
		///    </Outputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <Authors>
		///       TuanDm
		///    </Authors>
		///    <History>
		///       Thursday, January 20, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private void LoadTree()
		{
			ITM_CategoryBO boCategory = new ITM_CategoryBO();

			DataSet dsTree = boCategory.List();
			dsTree.Tables[ITM_CategoryTable.TABLE_NAME].Columns.Add(CHECKED, typeof(bool));

			TreeNode rootNode = new TreeNode();
			rootNode.Text = lblRootCaption.Text;
			rootNode.Tag = ROOT;

			tvwCategory.Nodes.Add(rootNode);

			foreach (DataRow dr in dsTree.Tables[ITM_CategoryTable.TABLE_NAME].Rows)
			{
				if ((dr[CHECKED].ToString().Trim() != TRUE) && (dr[ITM_CategoryTable.PARENTCATEGORYID_FLD] == DBNull.Value))
				{
					TreeNode node = NewNode(dr);
					rootNode.Nodes.Add(node);
					dr[CHECKED] = true;
					BuildTree(dsTree, node);
				}
			}
		}

		//**************************************************************************              
		///    <Description>
		///       This method uses to build root node
		///    </Description>
		///    <Inputs>
		///        dsTree : contains list of Category
		///        TreeNode : current node
		///    </Inputs>
		///    <Outputs>
		///    </Outputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <Authors>
		///       TuanDm
		///    </Authors>
		///    <History>
		///       Thursday , January 20, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private void BuildTree(DataSet dsTree, TreeNode node)
		{
			foreach (DataRow drNode in dsTree.Tables[0].Rows)
			{
				if (drNode[CHECKED].ToString().Trim() != TRUE)
				{
					string sqlSelect = ITM_CategoryTable.PARENTCATEGORYID_FLD + " = " + ((ITM_CategoryVO) node.Tag).CategoryID;
					DataRow[] drChildren = dsTree.Tables[ITM_CategoryTable.TABLE_NAME].Select(sqlSelect);
					foreach (DataRow drChild in drChildren)
					{
						if (drChild[CHECKED].ToString().Trim() != TRUE)
						{
							TreeNode childNode = new TreeNode();
							childNode = NewNode(drChild);
							node.Nodes.Add(childNode);
							drChild[CHECKED] = true;
							BuildTree(dsTree, childNode);
						}
					}
				}
			}
		}

		//**************************************************************************              
		///    <Description>
		///       Create a new node for tree
		///    </Description>
		///    <Inputs>
		///        DataRow contains all informations of Category      
		///    </Inputs>
		///    <Outputs>
		///    </Outputs>
		///    <Returns>
		///       TreeNode
		///    </Returns>
		///    <Authors>
		///       TuanDm
		///    </Authors>
		///    <History>
		///       Thursday, January 20, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private TreeNode NewNode(DataRow row)
		{
			ITM_CategoryVO voCategory = new ITM_CategoryVO();
			voCategory.CategoryID = int.Parse(row[ITM_CategoryTable.CATEGORYID_FLD].ToString());
			voCategory.Code = row[ITM_CategoryTable.CODE_FLD].ToString();
			voCategory.Name = row[ITM_CategoryTable.NAME_FLD].ToString();
			voCategory.CatalogNameVN = row[ITM_CategoryTable.CATALOGNAME_FLD].ToString();
			voCategory.Description = row[ITM_CategoryTable.DESCRIPTION_FLD].ToString();

			try
			{
				voCategory.ParentCategoryId = int.Parse(row[ITM_CategoryTable.PARENTCATEGORYID_FLD].ToString());
			}
			catch
			{
			}

			TreeNode node = new TreeNode();
			node.Tag = voCategory;
			node.Text= voCategory.Name;

			return node;
		}

		//**************************************************************************              
		///    <Description>
		///		Validate data on the form
		///    </Description>
		///    <Inputs>
		///    </Inputs>
		///    <Outputs>
		///    </Outputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <Authors>
		///       TuanDm
		///    </Authors>
		///    <History>
		///       Monday, Mar 14, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private bool ValidateData()
		{
			if (FormControlComponents.CheckMandatory(txtProductCode))
			{
				PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxButtons.OK);
				txtProductCode.Focus();
				return false;
			}

			if (FormControlComponents.CheckMandatory(txtProdcutName))
			{
				PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxButtons.OK);
				txtProdcutName.Focus();
				return false;
			}
			return true;
		}

		//**************************************************************************              
		///    <Description>
		///			Process when users click the node in the tree
		///    </Description>
		///    <Inputs>
		///    </Inputs>
		///    <Outputs>
		///    </Outputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <Authors>
		///       TuanDm
		///    </Authors>
		///    <History>
		///       Monday, Mar 14, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private void tvwCategory_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e)
		{
			if (tvwCategory.SelectedNode.Tag.ToString().Trim() != ROOT)
			{
				BindDataFromNodeToForm(tvwCategory.SelectedNode);
				enumAction = EnumAction.Default;
				ControlState(false);
//				ExtendedProButton extendPro = (ExtendedProButton) btnDelete.Tag;
//				if (!extendPro.IsDisable)
//				{
					btnDelete.Enabled = true;
//				}
//				extendPro = (ExtendedProButton) btnEdit.Tag;
//				if (!extendPro.IsDisable)
//				{
					btnEdit.Enabled = true;
//				}
			}
			else
			{
				ResetForm();
				ControlState(false);
				btnDelete.Enabled = false;
			}
		}

		//**************************************************************************              
		///    <Description>
		///		  Add event, reset form and change form's state
		///    </Description>
		///    <Inputs>
		///    </Inputs>
		///    <Outputs>
		///    </Outputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <Authors>
		///       TuanDm
		///    </Authors>
		///    <History>
		///       Monday, Mar 14, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private void btnAdd_Click(object sender, System.EventArgs e)
		{
			enumAction = EnumAction.Add;
			ControlState(false);
			ResetForm();
		}

		//**************************************************************************              
		///    <Description>
		///		Save event, validate data
		///			check bussiness and insert into database
		///    </Description>
		///    <Inputs>
		///    </Inputs>
		///    <Outputs>
		///    </Outputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <Authors>
		///       TuanDm
		///    </Authors>
		///    <History>
		///       Monday, Mar 14, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private void btnSave_Click(object sender, System.EventArgs e)
		{
			blnHasError = false;
			if (enumAction != EnumAction.Default)
			{
				string METHOD_NAME = THIS + "btnSave_Click()";
				if (ValidateData())
				{
					ITM_CategoryBO boCategory = new ITM_CategoryBO();
					ITM_CategoryVO voCategory = new ITM_CategoryVO();
					try
					{
						voCategory = GetDataFromFormToObj();
						if (enumAction == EnumAction.Edit)
						{
							voCategory.CategoryID =((ITM_CategoryVO) (tvwCategory.SelectedNode.Tag)).CategoryID;
							boCategory.Update(voCategory);
						}
						if (enumAction == EnumAction.Add)
						{
							if (tvwCategory.SelectedNode.Tag.ToString().Trim() == ROOT)
							{
                                boCategory.Add(voCategory);
							}
							else
							{
								if ( tvwCategory.SelectedNode.Tag.ToString()!= ROOT )
								{
									if (!boCategory.CheckAddNewCategory(((ITM_CategoryVO) tvwCategory.SelectedNode.Tag).CategoryID))
									{
										voCategory.ParentCategoryId = ((ITM_CategoryVO) tvwCategory.SelectedNode.Tag).CategoryID;
										boCategory.Add(voCategory);
									}
									else
									{
										PCSMessageBox.Show(ErrorCode.MESSAGE_CATEGORY_NOADDCHILD, MessageBoxIcon.Error);
										return;
									}
								}
							}
						}
						PCSMessageBox.Show(ErrorCode.MESSAGE_AFTER_SAVE_DATA);
						txtCategoryNameVN.Enabled = false;
						txtDesc.Enabled = false;
						txtProdcutName.Enabled = false;
						UpdateNodeIntoTree();
						enumAction = EnumAction.Default;
						ControlState(false);
						if (tvwCategory.SelectedNode.Tag.ToString().Trim() != ROOT)
						{
							btnEdit.Enabled = true;
							btnDelete.Enabled = true;
							btnSave.Enabled = false;
							btnAdd.Enabled = true;
						}
						else
						{
							btnEdit.Enabled = false;
							btnDelete.Enabled = false;
							btnSave.Enabled = false;
							btnAdd.Enabled = true;
						}
						blnHasError = true;
						btnAdd.Focus();
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
						if (ex.mCode == ErrorCode.DUPLICATE_KEY)
						{
							txtProductCode.Focus();
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
			}
		}

		//**************************************************************************              
		///    <Description>
		///		Update node if users click save button
		///    </Description>
		///    <Inputs>
		///    </Inputs>
		///    <Outputs>
		///    </Outputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <Authors>
		///       TuanDm
		///    </Authors>
		///    <History>
		///       Monday, Mar 14, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private void UpdateNodeIntoTree()
		{
			ITM_CategoryBO boCategory = new ITM_CategoryBO();
			//edit node
			if (enumAction == EnumAction.Edit)
			{
				TreeNode newNode = tvwCategory.SelectedNode;
				newNode.Text = txtProdcutName.Text.ToString();
				newNode.Tag	= boCategory.GetObjectVO(txtProductCode.Text.ToString());
			}
			//create new node
			if (enumAction == EnumAction.Add)
			{
				TreeNode newNode = new TreeNode();
				newNode.Text = txtProdcutName.Text.ToString();
				newNode.Tag	= boCategory.GetObjectVO(txtProductCode.Text.ToString());
				tvwCategory.SelectedNode.Nodes.Add(newNode);
			}
		}

		//**************************************************************************              
		///    <Description>
		///		Edit vent, change form's staet and fill data on the controls to edit
		///    </Description>
		///    <Inputs>
		///    </Inputs>
		///    <Outputs>
		///    </Outputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <Authors>
		///       TuanDm
		///    </Authors>
		///    <History>
		///       Monday, Mar 14, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private void btnEdit_Click(object sender, System.EventArgs e)
		{
			if (tvwCategory.SelectedNode.Tag.ToString().ToString().Trim()!= ROOT)
			{
				enumAction = EnumAction.Edit;
				BindDataFromNodeToForm(tvwCategory.SelectedNode);
				ControlState(true);
				txtProductCode.Enabled = false;
				txtDesc.Enabled = true;
				txtProdcutName.Focus();
			}
		}

		//**************************************************************************              
		///    <Description>
		///		Delete event, check child of node and delete it if can
		///    </Description>
		///    <Inputs>
		///    </Inputs>
		///    <Outputs>
		///    </Outputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <Authors>
		///       TuanDm
		///    </Authors>
		///    <History>
		///       Monday, Mar 14, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private void btnDelete_Click(object sender, System.EventArgs e)
		{
			string METHOD_NAME = THIS + "btnDelete_Click";
			if (tvwCategory.SelectedNode.Tag.ToString()==ROOT)
			{
				return;
			}
			if (PCSMessageBox.Show(ErrorCode.MESSAGE_DELETE_RECORD, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
			{
				
				try
				{
					ITM_CategoryBO boCategory = new ITM_CategoryBO();
					ITM_CategoryVO voCategory = (ITM_CategoryVO) tvwCategory.SelectedNode.Tag;
					boCategory.CheckAndDelete(voCategory.CategoryID);
					tvwCategory.SelectedNode.Remove();
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
		}

		//**************************************************************************              
		///    <Description>
		///		Close form event
		///    </Description>
		///    <Inputs>
		///    </Inputs>
		///    <Outputs>
		///    </Outputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <Authors>
		///       TuanDm
		///    </Authors>
		///    <History>
		///       Monday, Mar 14, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private void btnClose_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		//**************************************************************************              
		///    <Description>
		///		 Close form, check form's state and question if has change
		///    </Description>
		///    <Inputs>
		///    </Inputs>
		///    <Outputs>
		///    </Outputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <Authors>
		///       TuanDm
		///    </Authors>
		///    <History>
		///       Monday, Mar 14, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private void Catalog_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if ((enumAction == EnumAction.Add)||(enumAction == EnumAction.Edit))
			{
				DialogResult confirmDialog = PCSMessageBox.Show(ErrorCode.MESSAGE_QUESTION_STORE_INTO_DATABASE, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
				switch (confirmDialog)
				{
					case DialogResult.Yes:
						//Save before exit
						btnSave_Click(btnSave, new EventArgs());
						if (!blnHasError)
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

		private void Catalog_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Escape)
			{
				this.Close();
			}
		}

		private void btnChangePicture_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnChangePicture_Click()";
			try
			{
				if (System.Windows.Forms.DialogResult.Cancel == dlgSelectPicture.ShowDialog())
					return;
				Image img = Image.FromFile(dlgSelectPicture.FileName);
				if (img.Size.Width > picCategory.Size.Width ||
					img.Size.Height > picCategory.Size.Height)
					picCategory.SizeMode = PictureBoxSizeMode.StretchImage;
				else
					picCategory.SizeMode = PictureBoxSizeMode.CenterImage;
				picCategory.Image = Image.FromFile(dlgSelectPicture.FileName);
				picCategory.Image = Image.FromFile(dlgSelectPicture.FileName);
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

		private void btnClearPicture_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnClearPicture_Click()";
			try
			{
				picCategory.Image = null;
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
