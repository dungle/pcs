using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using C1.Win.C1Command;
using C1.Win.C1FlexGrid;
using C1.Win.C1Input;
using C1.Win.C1List;
using C1.Win.C1TrueDBGrid;
using PCSComUtils.Admin.BO;
using PCSComUtils.Common;
using PCSComUtils.PCSExc;
using PCSUtils.Log;
using PCSUtils.Utils;
using C1DisplayColumn = C1.Win.C1TrueDBGrid.C1DisplayColumn;
using CancelEventHandler = System.ComponentModel.CancelEventHandler;

namespace PCSUtils.Admin
{
	/// <summary>
	/// Summary description for GroupControls.
	/// </summary>
	public class GroupControls : Form
	{
		#region Generated
		private IContainer components;

		public GroupControls()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}
		public GroupControls(Form pfrmForm)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
			//WorkingForm = pfrmForm;
			objForm = pfrmForm;
			if(objForm != null)
			{
				objForm.Closed += new EventHandler(objForm_Closed);
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


		#endregion

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(GroupControls));
			this.btnEdit = new System.Windows.Forms.Button();
			this.btnClose = new System.Windows.Forms.Button();
			this.btnHelp = new System.Windows.Forms.Button();
			this.btnDelete = new System.Windows.Forms.Button();
			this.btnSave = new System.Windows.Forms.Button();
			this.btnAdd = new System.Windows.Forms.Button();
			this.pnlMain = new System.Windows.Forms.Panel();
			this.pnlRight = new System.Windows.Forms.Panel();
			this.lblControl = new System.Windows.Forms.Label();
			this.lvwControls = new System.Windows.Forms.ListView();
			this.splView = new System.Windows.Forms.Splitter();
			this.pnlLeft = new System.Windows.Forms.Panel();
			this.lblGroup = new System.Windows.Forms.Label();
			this.lblRole = new System.Windows.Forms.Label();
			this.tvwGroups = new System.Windows.Forms.TreeView();
			this.imglIcons = new System.Windows.Forms.ImageList(this.components);
			this.cboRoles = new System.Windows.Forms.ComboBox();
			this.colControl = new System.Windows.Forms.ColumnHeader();
			this.pnlMain.SuspendLayout();
			this.pnlRight.SuspendLayout();
			this.pnlLeft.SuspendLayout();
			this.SuspendLayout();
			// 
			// btnEdit
			// 
			this.btnEdit.AccessibleDescription = "";
			this.btnEdit.AccessibleName = "";
			this.btnEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnEdit.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnEdit.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnEdit.Location = new System.Drawing.Point(130, 367);
			this.btnEdit.Name = "btnEdit";
			this.btnEdit.Size = new System.Drawing.Size(60, 23);
			this.btnEdit.TabIndex = 81;
			this.btnEdit.Text = "&Edit";
			this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
			// 
			// btnClose
			// 
			this.btnClose.AccessibleDescription = "";
			this.btnClose.AccessibleName = "";
			this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnClose.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnClose.Location = new System.Drawing.Point(560, 367);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(60, 23);
			this.btnClose.TabIndex = 84;
			this.btnClose.Text = "&Close";
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			// 
			// btnHelp
			// 
			this.btnHelp.AccessibleDescription = "";
			this.btnHelp.AccessibleName = "";
			this.btnHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnHelp.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnHelp.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnHelp.Location = new System.Drawing.Point(498, 367);
			this.btnHelp.Name = "btnHelp";
			this.btnHelp.Size = new System.Drawing.Size(60, 23);
			this.btnHelp.TabIndex = 83;
			this.btnHelp.Text = "&Help";
			// 
			// btnDelete
			// 
			this.btnDelete.AccessibleDescription = "";
			this.btnDelete.AccessibleName = "";
			this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnDelete.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnDelete.Location = new System.Drawing.Point(192, 367);
			this.btnDelete.Name = "btnDelete";
			this.btnDelete.Size = new System.Drawing.Size(60, 23);
			this.btnDelete.TabIndex = 82;
			this.btnDelete.Text = "&Delete";
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
			// 
			// btnSave
			// 
			this.btnSave.AccessibleDescription = "";
			this.btnSave.AccessibleName = "";
			this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnSave.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnSave.Location = new System.Drawing.Point(68, 367);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(60, 23);
			this.btnSave.TabIndex = 80;
			this.btnSave.Text = "&Save";
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			// 
			// btnAdd
			// 
			this.btnAdd.AccessibleDescription = "";
			this.btnAdd.AccessibleName = "";
			this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnAdd.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnAdd.Location = new System.Drawing.Point(6, 367);
			this.btnAdd.Name = "btnAdd";
			this.btnAdd.Size = new System.Drawing.Size(60, 23);
			this.btnAdd.TabIndex = 79;
			this.btnAdd.Text = "&Add";
			this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
			// 
			// pnlMain
			// 
			this.pnlMain.AllowDrop = true;
			this.pnlMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.pnlMain.Controls.Add(this.pnlRight);
			this.pnlMain.Controls.Add(this.splView);
			this.pnlMain.Controls.Add(this.pnlLeft);
			this.pnlMain.Location = new System.Drawing.Point(6, 8);
			this.pnlMain.Name = "pnlMain";
			this.pnlMain.Size = new System.Drawing.Size(614, 349);
			this.pnlMain.TabIndex = 85;
			// 
			// pnlRight
			// 
			this.pnlRight.Controls.Add(this.lblControl);
			this.pnlRight.Controls.Add(this.lvwControls);
			this.pnlRight.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlRight.Location = new System.Drawing.Point(223, 0);
			this.pnlRight.Name = "pnlRight";
			this.pnlRight.Size = new System.Drawing.Size(391, 349);
			this.pnlRight.TabIndex = 9;
			// 
			// lblControl
			// 
			this.lblControl.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblControl.Location = new System.Drawing.Point(2, 2);
			this.lblControl.Name = "lblControl";
			this.lblControl.Size = new System.Drawing.Size(178, 20);
			this.lblControl.TabIndex = 11;
			this.lblControl.Text = "Controls in Group";
			this.lblControl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lvwControls
			// 
			this.lvwControls.AllowDrop = true;
			this.lvwControls.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.lvwControls.HideSelection = false;
			this.lvwControls.Location = new System.Drawing.Point(0, 22);
			this.lvwControls.Name = "lvwControls";
			this.lvwControls.Size = new System.Drawing.Size(391, 325);
			this.lvwControls.TabIndex = 8;
			this.lvwControls.View = System.Windows.Forms.View.List;
			this.lvwControls.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lvwControls_KeyDown);
			this.lvwControls.DragOver += new System.Windows.Forms.DragEventHandler(this.lvwControls_DragOver);
			this.lvwControls.DragDrop += new System.Windows.Forms.DragEventHandler(this.lvwControls_DragDrop);
			// 
			// splView
			// 
			this.splView.Location = new System.Drawing.Point(218, 0);
			this.splView.Name = "splView";
			this.splView.Size = new System.Drawing.Size(5, 349);
			this.splView.TabIndex = 7;
			this.splView.TabStop = false;
			// 
			// pnlLeft
			// 
			this.pnlLeft.Controls.Add(this.lblGroup);
			this.pnlLeft.Controls.Add(this.lblRole);
			this.pnlLeft.Controls.Add(this.tvwGroups);
			this.pnlLeft.Controls.Add(this.cboRoles);
			this.pnlLeft.Dock = System.Windows.Forms.DockStyle.Left;
			this.pnlLeft.Location = new System.Drawing.Point(0, 0);
			this.pnlLeft.Name = "pnlLeft";
			this.pnlLeft.Size = new System.Drawing.Size(218, 349);
			this.pnlLeft.TabIndex = 6;
			// 
			// lblGroup
			// 
			this.lblGroup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.lblGroup.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblGroup.Location = new System.Drawing.Point(0, 46);
			this.lblGroup.Name = "lblGroup";
			this.lblGroup.Size = new System.Drawing.Size(218, 20);
			this.lblGroup.TabIndex = 9;
			this.lblGroup.Text = "Data Field";
			this.lblGroup.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblRole
			// 
			this.lblRole.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.lblRole.ForeColor = System.Drawing.Color.Maroon;
			this.lblRole.Location = new System.Drawing.Point(0, 0);
			this.lblRole.Name = "lblRole";
			this.lblRole.Size = new System.Drawing.Size(218, 20);
			this.lblRole.TabIndex = 8;
			this.lblRole.Text = "Role";
			this.lblRole.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// tvwGroups
			// 
			this.tvwGroups.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.tvwGroups.CheckBoxes = true;
			this.tvwGroups.HideSelection = false;
			this.tvwGroups.ImageList = this.imglIcons;
			this.tvwGroups.LabelEdit = true;
			this.tvwGroups.Location = new System.Drawing.Point(0, 66);
			this.tvwGroups.Name = "tvwGroups";
			this.tvwGroups.Size = new System.Drawing.Size(218, 283);
			this.tvwGroups.TabIndex = 7;
			this.tvwGroups.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tvwGroups_KeyDown);
			this.tvwGroups.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.tvwGroups_AfterCheck);
			this.tvwGroups.DoubleClick += new System.EventHandler(this.tvwGroups_DoubleClick);
			this.tvwGroups.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvwGroups_AfterSelect);
			this.tvwGroups.AfterLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.tvwGroups_AfterLabelEdit);
			// 
			// imglIcons
			// 
			this.imglIcons.ImageSize = new System.Drawing.Size(16, 16);
			this.imglIcons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imglIcons.ImageStream")));
			this.imglIcons.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// cboRoles
			// 
			this.cboRoles.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.cboRoles.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboRoles.Location = new System.Drawing.Point(0, 20);
			this.cboRoles.Name = "cboRoles";
			this.cboRoles.Size = new System.Drawing.Size(218, 21);
			this.cboRoles.TabIndex = 0;
			this.cboRoles.SelectedValueChanged += new System.EventHandler(this.cboRoles_SelectedValueChanged);
			// 
			// colControl
			// 
			this.colControl.Text = "Control";
			this.colControl.Width = 225;
			// 
			// GroupControls
			// 
			this.AcceptButton = this.btnAdd;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.btnClose;
			this.ClientSize = new System.Drawing.Size(626, 396);
			this.Controls.Add(this.pnlMain);
			this.Controls.Add(this.btnEdit);
			this.Controls.Add(this.btnClose);
			this.Controls.Add(this.btnHelp);
			this.Controls.Add(this.btnDelete);
			this.Controls.Add(this.btnSave);
			this.Controls.Add(this.btnAdd);
			this.Name = "GroupControls";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Set Field Visibility";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.GroupControls_Closing);
			this.Load += new System.EventHandler(this.GroupControls_Load);
			this.Closed += new System.EventHandler(this.GroupControls_Closed);
			this.pnlMain.ResumeLayout(false);
			this.pnlRight.ResumeLayout(false);
			this.pnlLeft.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		#region Constants

		private const string THIS = "PCSUtils.Admin.GroupControls";
		/// <summary>
		/// This message was used for only Super admin so that no need to add into database
		/// </summary>
		const string MESSAGE_GENERATE_AUTOMATIC = "Do you want generate automatically?";
		const string NEW_NODE_TEXT = "New Group";
		const string SUPER_MODE_TEXT = " [Super Mode]";
		const string SQR_BEGIN = "[";
		const string SQR_END = "]";
		const string AND = "&";
		const char DATA_SEPARATOR = ';';

		const int IMAGE_CONTROL = 0;
		const int IMAGE_FORM = 1;
		const int IMAGE_CONTAINER = 2;
		const int IMAGE_GROUP = 3;
		const int IMAGE_GRID = 4;

//		const int NATURE_GROUP = 1;
//		const int DEFINE_GROUP = 2;

		class TagData
		{
			public Control objControl;
			public int intGroupID;
		}
		
		#endregion

		#region Member variables

		private Form objForm;
		private DataSet dstData;
		private Button btnEdit;
		private Button btnClose;
		private Button btnHelp;
		private Button btnDelete;
		private Button btnSave;
		private Button btnAdd;
		private Panel pnlMain;
		private System.Windows.Forms.Panel pnlLeft;
		private System.Windows.Forms.ComboBox cboRoles;
		private System.Windows.Forms.TreeView tvwGroups;
		private System.Windows.Forms.Splitter splView;
		private System.Windows.Forms.Label lblRole;
		private System.Windows.Forms.Label lblGroup;
		private System.Windows.Forms.Panel pnlRight;
		private System.Windows.Forms.ListView lvwControls;
		private System.Windows.Forms.ColumnHeader colControl;
		private System.Windows.Forms.Label lblControl;
		private ImageList imglIcons;

		bool blnIsChanged = false;
		bool blnCorrectingCheck = false;

		#endregion

		#region Properties

//		public Form WorkingForm
//		{
//			set
//			{
//				objForm = value;
//				if(objForm != null)
//				{
//					objForm.Closed += new EventHandler(objForm_Closed);
//				}
//			}
//		}
		
		#endregion

		#region Methods
		/// <summary>
		/// Set enable,visible and add events ...
		/// </summary>
		/// <param name="objCtrl"></param>
		private void HookControl(Control objCtrl)
		{
			if(objCtrl != null)
			{
				objCtrl.Enabled = true;
				objCtrl.Visible = true;
				objCtrl.MouseDown += new MouseEventHandler(objCtrl_MouseDown);
				// objCtrl.VisibleChanged += new EventHandler(objCtrl_VisibleChanged);
				objCtrl.EnabledChanged += new EventHandler(objCtrl_EnabledChanged);
			}
		}

		
		private string GetGroupText(Control pobjContainer)
		{
			DataRow[] drowGroups = dstData.Tables[Sys_VisibilityGroupTable.TABLE_NAME].Select(
				Sys_VisibilityGroupTable.FORMNAME_FLD + "= '" + objForm.Name + "'"
				+ " AND " + Sys_VisibilityGroupTable.CONTROLNAME_FLD + "= '" + pobjContainer.Name + "'");
			if(drowGroups.Length > 0)
			{
				return drowGroups[0][Sys_VisibilityGroupTable.GROUPTEXT_FLD].ToString();
			}
			else
			{
				return SQR_BEGIN + pobjContainer.Name + SQR_END;
			}
		}

		/// <summary>
		/// Load container
		/// </summary>
		/// <param name="objContainer"></param>
		/// <returns></returns>
		private TreeNode LoadContainerControls(Control objContainer)
		{
			HookControl(objContainer);
			if (objContainer is Form)
			{
				// ((Form)objContainer).Closing -= new CancelEventHandler();
				TreeNode objNode = new TreeNode(objContainer.Text,IMAGE_FORM,IMAGE_FORM);
				objNode.Checked = true;

				TagData objTagData = new TagData();
				objTagData.objControl = objContainer;
				objNode.Tag = objTagData;

				foreach (Control objCtrl in objContainer.Controls)
				{
					TreeNode objChildNode = LoadContainerControls(objCtrl);
					if (objChildNode != null)
					{
						objNode.Nodes.Add(objChildNode);
					}
				}
				return objNode;
			}
			else if ((objContainer.GetType().Equals(typeof(GroupBox))) 
				|| (objContainer.GetType().Equals(typeof(C1DockingTabPage)))
				|| (objContainer.GetType().Equals(typeof(TabPage))))
			{
				string strContainerText = objContainer.Text;
				if (strContainerText.Trim().Equals(string.Empty))
				{
					#region // HACK: DEL SonHT 2005-11-09

//					DataRow[] drowGroups = dstData.Tables[Sys_VisibilityGroupTable.TABLE_NAME].Select(
//						Sys_VisibilityGroupTable.FORMNAME_FLD + "= '" + objForm.Name + "'"
//							+ Sys_VisibilityGroupTable.CONTROLNAME_FLD + "= '" + objContainer.Name + "'");
//					if(drowGroups.Length > 0)
//					{
//						strContainerText = drowGroups[0][Sys_VisibilityGroupTable.GROUPTEXT_FLD].ToString();
//					}
//					else
//					{
//						strContainerText = SQR_BEGIN + objContainer.Name + SQR_END;
//					}
					
					#endregion // END: DEL SonHT 2005-11-09

					strContainerText = GetGroupText(objContainer);
				}
				TreeNode objNode  =  new TreeNode(strContainerText,IMAGE_CONTAINER,IMAGE_CONTAINER);
				objNode.Checked = true;

				TagData objTagData = new TagData();
				objTagData.objControl = objContainer;
				objNode.Tag = objTagData;
				
				foreach (Control objCtrl in objContainer.Controls)
				{
					TreeNode objChildNode = LoadContainerControls(objCtrl);
					if (objChildNode != null)
					{
						objNode.Nodes.Add(objChildNode);
					}
				}
				return objNode;
			}
			else if (objContainer is C1TrueDBGrid)
			{
				C1TrueDBGrid objGrid = (C1TrueDBGrid)objContainer;
				// string strContainerText = SQR_BEGIN + objGrid.Name + SQR_END;
				string strContainerText = GetGroupText(objContainer);
				TreeNode objNode = new TreeNode(strContainerText,IMAGE_CONTAINER,IMAGE_CONTAINER);
				objNode.Checked = true;
				
				TagData objTagData = new TagData();
				objTagData.objControl = objContainer;
				tvwGroups.SelectedNode = objNode;
				objNode.Tag = objTagData;

				#region // HACK: DEL SonHT 2005-11-09

				//				foreach (C1.Win.C1TrueDBGrid.C1DisplayColumn colC1 in objGrid.Splits[0].DisplayColumns)
				//				{
				//					TreeNode objChildNode = new TreeNode(colC1.DataColumn.Caption,IMAGE_GROUP,IMAGE_GROUP);
				//					if (objChildNode != null)
				//					{
				//						objNode.Nodes.Add(objChildNode);
				//						objNode.Checked = true;
				//						TagData tagData = new TagData();
				//						tagData.objControl = objGrid;
				//						objNode.Tag = objTagData;
				//					}
				//				}
				
				#endregion // END: DEL SonHT 2005-11-09

				return objNode;
			}
			else if (objContainer is C1FlexGrid)
			{				
				C1FlexGrid objGrid = (C1FlexGrid)objContainer;
				//string strContainerText = SQR_BEGIN + objGrid.Name + SQR_END;
				string strContainerText = GetGroupText(objGrid);
				TreeNode objNode = new TreeNode(strContainerText,IMAGE_CONTAINER,IMAGE_CONTAINER);
				objNode.Checked = true;

				TagData objTagData = new TagData();
				objTagData.objControl = objContainer;
				objNode.Tag = objTagData;
				
				return objNode;
			}
			else if (objContainer is C1DockingTab)
			{				
				C1DockingTab objTab = (C1DockingTab)objContainer;
				//string strContainerText = SQR_BEGIN + objTab.Name + SQR_END;
				string strContainerText = GetGroupText(objContainer);
				TreeNode objNode  =  new TreeNode(strContainerText,IMAGE_CONTAINER,IMAGE_CONTAINER);
				objNode.Checked = true;

				TagData objTagData = new TagData();
				objTagData.objControl = objContainer;
				objNode.Tag = objTagData;
				
				foreach (C1DockingTabPage objPage in objTab.TabPages)
				{			
					TreeNode objChildNode = LoadContainerControls(objPage);
					if (objChildNode != null)
					{
						objNode.Nodes.Add(objChildNode);
					}
				}
				return objNode;
			}
			else if (objContainer is TabControl)
			{				
				TabControl objTab = (TabControl)objContainer;
				//string strContainerText = SQR_BEGIN + objTab.Name + SQR_END;
				string strContainerText = GetGroupText(objContainer);
				TreeNode objNode  =  new TreeNode(strContainerText,IMAGE_CONTAINER,IMAGE_CONTAINER);
				objNode.Checked = true;

				TagData objTagData = new TagData();
				objTagData.objControl = objContainer;
				objNode.Tag = objTagData;
				
				foreach (TabPage objPage in objTab.TabPages)
				{			
					TreeNode objChildNode = LoadContainerControls(objPage);
					if (objChildNode != null)
					{
						objNode.Nodes.Add(objChildNode);
					}
				}
				return objNode;
			}
			else if (objContainer.GetType().Equals(typeof(Panel)))
			{				
				Panel pnlPanel = (Panel)objContainer;
				//string strContainerText = SQR_BEGIN + pnlPanel.Name + SQR_END;
				string strContainerText = GetGroupText(objContainer);
				TreeNode objNode  =  new TreeNode(strContainerText,IMAGE_CONTAINER,IMAGE_CONTAINER);
				objNode.Checked = true;

				TagData objTagData = new TagData();
				objTagData.objControl = objContainer;
				objNode.Tag = objTagData;
				
				foreach (Control objCtrl in objContainer.Controls)
				{
					TreeNode objChildNode = LoadContainerControls(objCtrl);
					if (objChildNode != null)
					{
						objNode.Nodes.Add(objChildNode);
					}
				}
				return objNode;
			}			
			else
			{
				return null;
			}
		}


		private bool EnableDestinationControl(Control objContainer, string strControlName, bool blnEnable)
		{
			foreach (Control objCtrl in objContainer.Controls)
			{
				if (objCtrl.Name.Equals(strControlName))
				{
					//objCtrl.Visible = 
					objCtrl.Enabled = blnEnable;
//					if(blnEnable)
//					{
//						objCtrl.BackColor = Color.Red;
//					}
//					else
//					{
//						objCtrl.ResetBackColor();
//					}
					return true;
				}
				else if (EnableDestinationControl(objCtrl,strControlName, blnEnable))
				{
					return true;
				}
			}
			return false;
		}


		/// <summary>
		/// Check if strCtrlName is contener of objContainer
		/// </summary>
		/// <param name="objContainer"></param>
		/// <param name="strCtrlName"></param>
		/// <returns></returns>
		private bool IsContainerOf(Control objContainer, string strCtrlName)
		{
			bool blnContain = false;
			foreach (Control objCtrl in objContainer.Controls)
			{
				if (objCtrl.Name.Equals(strCtrlName))
				{
					blnContain = true;
					break;
				}
			}
			
			return blnContain;
		}

	
		private void DoEdit()
		{
			TreeNode objCurrentNode = tvwGroups.SelectedNode;
			if (objCurrentNode.ImageIndex == IMAGE_GROUP)
			{
				tvwGroups.SelectedNode.BeginEdit();
			}
		}

	
		private void DoSave()
		{				
			VisibilityBO boVisibility = new VisibilityBO();
			boVisibility.UpdateAllDataSet(dstData);
			// Update all hidden for all role
			if(this.objForm == null)
			{
				if(cboRoles.Text.ToLower().Equals(Constants.ALL_ROLE.ToLower()))
				{
					boVisibility.UpdateAllRole();
					DoLoadData(ref dstData);
				}
			}
		}

		private void DoAdd(string pstrNodeText)
		{
			const int MAXLENGHT = 40;
			//Mark to not update check state in dataset
			blnCorrectingCheck = true;
			if(pstrNodeText.Length > MAXLENGHT)
			{
				pstrNodeText = pstrNodeText.Substring(0,MAXLENGHT);
			}
			TreeNode objCurrentNode = tvwGroups.SelectedNode;
			if ((objCurrentNode != null) && (objCurrentNode.Tag != null))
			{
				//TreeNode objNewNode = new TreeNode(NEW_NODE_TEXT,IMAGE_GROUP,IMAGE_GROUP);
				TreeNode objNewNode = new TreeNode(pstrNodeText,IMAGE_GROUP,IMAGE_GROUP);
				// objNewNode.Tag = objCurrentNode.Tag;
				objNewNode.Checked = true;
				if (objCurrentNode.ImageIndex != IMAGE_GROUP)
				{
					objCurrentNode.Nodes.Add(objNewNode);
				}
				else
				{
					objCurrentNode.Parent.Nodes.Add(objNewNode);
				}
				objCurrentNode.ExpandAll();
				tvwGroups.SelectedNode = objNewNode;

				//Get parent id
				int intParentID = 0;
				try
				{
					TreeNode objParent = objNewNode.Parent;
					string strParentName = ((TagData)objParent.Tag).objControl.Name;
					DataRow drParent = dstData.Tables[Sys_VisibilityGroupTable.TABLE_NAME].Select(Sys_VisibilityGroupTable.CONTROLNAME_FLD + "='" + strParentName + "'"
						+ " AND " + Sys_VisibilityGroupTable.FORMNAME_FLD + "='" + objForm.Name + "'")[0];
					intParentID = Convert.ToInt32(drParent[Sys_VisibilityGroupTable.VISIBILITYGROUPID_FLD].ToString());
				}
				catch
				{
					intParentID = 0;
				}

				#region // HACK: DEL DuongNA 2005-10-19
				////Add and return id
				//Sys_VisibilityGroupVO voGroup = new Sys_VisibilityGroupVO();
				////voGroup.ControlName = strNodeName;
				//voGroup.FormName = objForm.Name;
				//voGroup.GroupText = NEW_NODE_TEXT;
				////TODO : Multilingual processing
				//voGroup.ParentID = intParentID;
				//VisibilityBO boVisibility = new VisibilityBO();
				//int intID = boVisibility.AddAndReturnID(voGroup);
				#endregion // END: DEL DuongNA 2005-10-19

				//Add to dataset
				DataRow drGroup = dstData.Tables[Sys_VisibilityGroupTable.TABLE_NAME].NewRow();						
				//drGroup[Sys_VisibilityGroupTable.CONTROLNAME_FLD] = ;
				drGroup[Sys_VisibilityGroupTable.FORMNAME_FLD] = objForm.Name;
				drGroup[Sys_VisibilityGroupTable.GROUPTEXT_FLD] = pstrNodeText;
				drGroup[Sys_VisibilityGroupTable.TYPE_FLD] = VisibilityGroupTypeEnum.GroupNormal.GetHashCode(); //DEFINE_GROUP;
				if (intParentID != 0)
				{
					drGroup[Sys_VisibilityGroupTable.PARENTID_FLD] = intParentID;
				}
				//drGroup[Sys_VisibilityGroupTable.VISIBILITYGROUPID_FLD] = intID;

				dstData.Tables[Sys_VisibilityGroupTable.TABLE_NAME].Rows.Add(drGroup);
				VisibilityBO boVisibility = new VisibilityBO();
				// TODO: Don't update direct into database
				boVisibility.UpdateAllDataSet(dstData);

					
				#region // HACK: DEL DuongNA 2005-10-19
				//dstData.Tables[Sys_VisibilityGroupTable.TABLE_NAME].AcceptChanges();
				#endregion // END: DEL DuongNA 2005-10-19

				TagData objTagData = new TagData();
				objTagData.objControl = ((TagData)objCurrentNode.Tag).objControl;
				int intRowIndex = dstData.Tables[Sys_VisibilityGroupTable.TABLE_NAME].Rows.Count - 1;
				objTagData.intGroupID = int.Parse(dstData.Tables[Sys_VisibilityGroupTable.TABLE_NAME].Rows[intRowIndex][Sys_VisibilityGroupTable.VISIBILITYGROUPID_FLD].ToString());//drGroup;
				objNewNode.Tag = objTagData;

				// objNewNode.BeginEdit();
			}
		

			//Unmark to not allow update check state in dataset
			blnCorrectingCheck = true;				
			
		}
		
		private void DoDeleteGroup()
		{
			TreeNode objCurrentNode = tvwGroups.SelectedNode;
		
			if (objCurrentNode.ImageIndex == IMAGE_GROUP)
			{
				//delete from dataset
				int intGroupID = ((TagData)objCurrentNode.Tag).intGroupID;
				//Find row and delete
				foreach (DataRow dr in dstData.Tables[Sys_VisibilityGroupTable.TABLE_NAME].Rows)
				{
					if(dr.RowState != DataRowState.Deleted)
					{
						if (int.Parse(dr[Sys_VisibilityGroupTable.VISIBILITYGROUPID_FLD].ToString()) == intGroupID)
						{
							// Delete in Visibility Item
							foreach (DataRow drItem in dstData.Tables[Sys_VisibilityItemTable.TABLE_NAME].Rows)
							{
								if(drItem.RowState == DataRowState.Deleted) continue;
								if (drItem[Sys_VisibilityItemTable.GROUPID_FLD].ToString().Equals(intGroupID.ToString()))
								{
									drItem.Delete();
									//break;
								}
							}

							// Delete in Visibility Item
							foreach (DataRow drRole in dstData.Tables[Sys_VisibilityGroup_RoleTable.TABLE_NAME].Rows)
							{
								if(drRole.RowState == DataRowState.Deleted) continue;
								if (drRole[Sys_VisibilityGroup_RoleTable.GROUPID_FLD].ToString().Equals(intGroupID.ToString()))
								{
									drRole.Delete();
									//break;
								}
							}
							dr.Delete();
							// return;
							break;
						}
					}
				}
				(new VisibilityBO()).UpdateDataSetRoleAndItem(dstData);
				
				//remove treenode
				TreeNode objParentNode = objCurrentNode.Parent;
				objParentNode.Nodes.Remove(objCurrentNode);
			}
			else if (objCurrentNode.ImageIndex == IMAGE_CONTAINER)
			{
				if(objCurrentNode.Nodes.Count == 0)
				{
					int intGroupID = ((TagData)objCurrentNode.Tag).intGroupID;
					//Find row and delete
					foreach (DataRow dr in dstData.Tables[Sys_VisibilityGroupTable.TABLE_NAME].Rows)
					{
						if(dr.RowState != DataRowState.Deleted)
						{
							if (int.Parse(dr[Sys_VisibilityGroupTable.VISIBILITYGROUPID_FLD].ToString()) == intGroupID)
							{
								// Delete in Visibility Item
								foreach (DataRow drItem in dstData.Tables[Sys_VisibilityItemTable.TABLE_NAME].Rows)
								{
									if(drItem.RowState == DataRowState.Deleted) continue;
									if (drItem[Sys_VisibilityItemTable.GROUPID_FLD].ToString().Equals(intGroupID.ToString()))
									{
										drItem.Delete();
										break;
									}
								}

								// Delete in Sys_VisibilityGroup_RoleTable
								foreach (DataRow drRole in dstData.Tables[Sys_VisibilityGroup_RoleTable.TABLE_NAME].Rows)
								{
									if(drRole.RowState == DataRowState.Deleted) continue;
									if (drRole[Sys_VisibilityGroup_RoleTable.GROUPID_FLD].ToString().Equals(intGroupID.ToString()))
									{
										drRole.Delete();
										break;
									}
								}
								dr.Delete();
								// return;
								break;
							}
						}
					}
					(new VisibilityBO()).UpdateDataSetRoleAndItem(dstData);
				
					//remove treenode
					TreeNode objParentNode = objCurrentNode.Parent;
					objParentNode.Nodes.Remove(objCurrentNode);
				}
			}
		}


		private void DoDeleteItem()
		{
			TagData tagData = (TagData)tvwGroups.SelectedNode.Tag;
			foreach (ListViewItem objItem in lvwControls.SelectedItems)
			{
				string strItemName = objItem.Tag.ToString();
				lvwControls.Items.Remove(objItem);
				//UndoDragDropControl(objForm, strItemName);
					
				//And delete from dataset
				foreach (DataRow drItem in dstData.Tables[Sys_VisibilityItemTable.TABLE_NAME].Rows)
				{
					if(drItem.RowState == DataRowState.Deleted) continue;
					if (drItem[Sys_VisibilityItemTable.NAME_FLD].ToString().Equals(strItemName.Trim())
						&& (drItem[Sys_VisibilityItemTable.GROUPID_FLD].ToString().Equals(tagData.intGroupID.ToString())))
					{
						drItem.Delete();
						break;
					}
				}

				//Enable in destination form
				EnableDestinationControl(objForm,strItemName, true);
			}
		}


		private void DoLoadData(ref DataSet dstData)
		{
			VisibilityBO boVisibility = new VisibilityBO();
			dstData = boVisibility.GetVisibilityData();
		
		}

		/// <summary>
		/// Load tree view 
		/// </summary>
		/// <param name="tvwTree"></param>
		private void DoLoadTree(TreeView tvwTree)
		{			
			TreeNode objRootNode = LoadContainerControls(objForm);
			if (objRootNode != null)
			{
				tvwTree.Nodes.Add(objRootNode);
			}
			tvwTree.ExpandAll();
		}

	
		private void DoSynchonize(DataSet dstData, TreeView tvwTree)
		{
			// const string METHOD_NAME = THIS + ".DoSynchonize()";
			Stack stkNode = new Stack();
			TreeNode objNode;
			
			blnCorrectingCheck = true;
			//Search throw tree to add not existed group to datatabe
			//At this time, treeview contain natural groups only !!!
			//First, add first node to stack
			stkNode.Push(tvwTree.Nodes[0]);
				
			while (true)
			{
				object obj;
				if(stkNode.Count > 0)
				{
					obj = stkNode.Pop();
				}
				else
				{
					//stack empty
					break;
				}
				objNode = (TreeNode)obj;
					
				//Check this node if this node don't have control
				if(((TagData)objNode.Tag == null) || (((TagData)objNode.Tag).objControl == null))
				{
					break;
				}
				string strNodeName = ((TagData)objNode.Tag).objControl.Name;
				int intParentID = 0;
				try
				{
					// parent node has processed, can get info directly from tag as datarow
					TreeNode objParent = objNode.Parent;
					string strParentName = ((TagData)objParent.Tag).objControl.Name;
					DataRow drParent = dstData.Tables[Sys_VisibilityGroupTable.TABLE_NAME].Select(Sys_VisibilityGroupTable.CONTROLNAME_FLD + "='" + strParentName + "'"
						+ " AND " + Sys_VisibilityGroupTable.FORMNAME_FLD + "='" + objForm.Name + "'")[0];
					intParentID = Convert.ToInt32(drParent[Sys_VisibilityGroupTable.VISIBILITYGROUPID_FLD].ToString());
				}
				catch
				{
					intParentID = 0;
				}

				DataRow[] arrRows = dstData.Tables[Sys_VisibilityGroupTable.TABLE_NAME].Select(Sys_VisibilityGroupTable.CONTROLNAME_FLD + "='" + strNodeName + "'"
					+ " AND " + Sys_VisibilityGroupTable.FORMNAME_FLD + "='" + objForm.Name + "'");
				if (arrRows.Length == 0)
				{
					bool blnCreateNew = false;
					Control ctr = ((TagData)objNode.Tag).objControl;
					if(ctr is Form)
					{
						blnCreateNew = true;
					}
					else
					{
						DialogResult result = MessageBox.Show("Do you want to create node for control: " + ctr.Name,"PCS",MessageBoxButtons.YesNo);
						if(result == DialogResult.Yes)
						{
							blnCreateNew = true;
						}
						else
						{
							objNode.Remove();
						}
					}
					if(blnCreateNew)
					{

						#region // HACK: DEL DuongNA 2005-10-19
						////Add and return id
						//Sys_VisibilityGroupVO voGroup = new Sys_VisibilityGroupVO();
						//voGroup.ControlName = strNodeName;
						//voGroup.FormName = objForm.Name;
						//voGroup.GroupText = objNode.Text;
						////TODO : Multilingual processing
						//voGroup.ParentID = intParentID;
						//VisibilityBO boVisibility = new VisibilityBO();
						//int intID = boVisibility.AddAndReturnID(voGroup);
						#endregion // END: DEL DuongNA 2005-10-19

						//Add to dataset and update to take synchronized
						DataRow drGroup = dstData.Tables[Sys_VisibilityGroupTable.TABLE_NAME].NewRow();
						
						drGroup[Sys_VisibilityGroupTable.CONTROLNAME_FLD] = strNodeName;
						drGroup[Sys_VisibilityGroupTable.FORMNAME_FLD] = objForm.Name;
						drGroup[Sys_VisibilityGroupTable.GROUPTEXT_FLD] = objNode.Text;
						if (intParentID != 0)
						{
							drGroup[Sys_VisibilityGroupTable.PARENTID_FLD] = intParentID;
						}
						// drGroup[Sys_VisibilityGroupTable.VISIBILITYGROUPID_FLD] = intID;
						drGroup[Sys_VisibilityGroupTable.TYPE_FLD] = VisibilityGroupTypeEnum.Container.GetHashCode(); // NATURE_GROUP;

						dstData.Tables[Sys_VisibilityGroupTable.TABLE_NAME].Rows.Add(drGroup);
										
						VisibilityBO boVisibility = new VisibilityBO();
						boVisibility.UpdateAllDataSet(dstData);

						#region // HACK: DEL DuongNA 2005-10-19
						//dstData.Tables[Sys_VisibilityGroupTable.TABLE_NAME].AcceptChanges();
						#endregion // END: DEL DuongNA 2005-10-19

						int intRowIndex = dstData.Tables[Sys_VisibilityGroupTable.TABLE_NAME].Rows.Count - 1;
						((TagData)objNode.Tag).intGroupID = int.Parse(dstData.Tables[Sys_VisibilityGroupTable.TABLE_NAME].Rows[intRowIndex][Sys_VisibilityGroupTable.VISIBILITYGROUPID_FLD].ToString());//drGroup;

					}
				}
				else
				{
					//existed
					((TagData)objNode.Tag).intGroupID = int.Parse(arrRows[0][Sys_VisibilityGroupTable.VISIBILITYGROUPID_FLD].ToString());
				}

				//Add all children
				foreach (TreeNode objChildNode in objNode.Nodes)
				{
					stkNode.Push(objChildNode);
				}
			}

			//Search throw datatable to add not existed node to tree
			foreach (DataRow dr in dstData.Tables[Sys_VisibilityGroupTable.TABLE_NAME].Rows)
			{
				if(dr.RowState == DataRowState.Deleted) continue;
				if (int.Parse(dr[Sys_VisibilityGroupTable.TYPE_FLD].ToString()) == VisibilityGroupTypeEnum.GroupNormal.GetHashCode()) //DEFINE_GROUP)
				{
					//Find parent node
					AddNewNode(tvwTree.Nodes[0],dr);
				}
			}

			//Disable destination control
			foreach (DataRow dr in dstData.Tables[Sys_VisibilityItemTable.TABLE_NAME].Rows)
			{
				if(dr.RowState == DataRowState.Deleted) continue;
				string strControl = dr[Sys_VisibilityItemTable.NAME_FLD].ToString().Trim();
				EnableDestinationControl(objForm, strControl,false)	;
			}

			#region // HACK: DEL SonHT 2005-11-06
			//Correct check status
			// CorrectNodeCheck();
			#endregion // END: DEL SonHT 2005-11-06

			blnCorrectingCheck = false;
		}

		private bool AddNewNode(TreeNode objNode, DataRow drData)
		{
			int intParentID = int.Parse(drData[Sys_VisibilityGroupTable.PARENTID_FLD].ToString());

			if (((TagData)objNode.Tag).intGroupID == intParentID)
			{
				TreeNode objNewNode = new TreeNode(drData[Sys_VisibilityGroupTable.GROUPTEXT_FLD].ToString(),IMAGE_GROUP,IMAGE_GROUP);
				TagData objTagData = new TagData();
				objTagData.intGroupID = int.Parse(drData[Sys_VisibilityGroupTable.VISIBILITYGROUPID_FLD].ToString());
				objTagData.objControl = ((TagData)objNode.Tag).objControl;
				objNewNode.Tag = objTagData;
				objNewNode.Checked = true;
				objNode.Nodes.Add(objNewNode);
				return true;
			}
			else
			{
				foreach (TreeNode objChild in objNode.Nodes)
				{
					if (AddNewNode(objChild,drData))
					{
						return true;
					}
				}
			}
			return false;
		
		}

		private void CorrectNodeCheck()
		{
			if(cboRoles.SelectedValue == null) return;
			int intSelectedRoleID = int.Parse(cboRoles.SelectedValue.ToString());
			//if(pobjRoot == null) return;
			DataRow[] drs = dstData.Tables[Sys_VisibilityGroup_RoleTable.TABLE_NAME].Select("RoleID=" + intSelectedRoleID);
			foreach (DataRow dr in drs)
			{
				if(dr.RowState == DataRowState.Deleted) continue;
				int intDBGroupID = int.Parse(dr[Sys_VisibilityGroup_RoleTable.GROUPID_FLD].ToString());
				if(tvwGroups.Nodes.Count > 0)
				{
					for(int i = 0; i < tvwGroups.Nodes.Count; i++)
					{
						if(tvwGroups.Nodes[i].Tag != null)
						{
							int intNodeGroupID = ((TagData)tvwGroups.Nodes[i].Tag).intGroupID;
							if(intDBGroupID == intNodeGroupID)
							{
								tvwGroups.Nodes[i].Checked = false;
							}
						}
					}
				}
			}

		}

		private void SetNodeCheckForChild(TreeNode objRootNode, bool pblnCheck)
		{
			//objRootNode.Checked = pblnCheck;
			foreach (TreeNode objChild in objRootNode.Nodes)
			{
				//SetNodeCheckForChild(objChild,pblnCheck);
				objChild.Checked = pblnCheck;
			}
		}

		#endregion

		#region Event Handlers

		private void objCtrl_MouseDown(object sender, MouseEventArgs e)
		{
			const string METHOD_NAME = THIS + ".objCtrl_MouseDown()";
			try 
			{
				Control objCtrl = (Control)sender;
				objCtrl.EnabledChanged -= new EventHandler(objCtrl_EnabledChanged);
				string strDragDropData = string.Empty;
				//				if ((objCtrl is TextBox) 
				//					//|| (objCtrl.GetType().Equals(typeof(C1TextBox)))
				//					|| (objCtrl.GetType().Equals(typeof(ComboBox)))
				//					|| (objCtrl.GetType().Equals(typeof(C1Combo)))
				//					|| (objCtrl.GetType().Equals(typeof(ListBox)))
				//					|| (objCtrl.GetType().Equals(typeof(C1List))))
				//				{
				//					strDragDropData = objCtrl.Name + DATA_SEPARATOR + objCtrl.Text;
				//				}
				//				else
			
				// TODO: If it has difference 
				strDragDropData = objCtrl.Name + DATA_SEPARATOR + objCtrl.Text;
			
				objCtrl.DoDragDrop(strDragDropData,DragDropEffects.All);
				
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


		private void objCtrl_VisibleChanged(object sender, EventArgs e)
		{
			Control ctrl = (Control)sender;
			if(ctrl != null)
			{
				if(ctrl.Visible == false) ctrl.Visible = true;
			}
		}
		private void objCtrl_EnabledChanged(object sender, EventArgs e)
		{
			Control ctrl = (Control)sender;
			if(ctrl != null)
			{
				if(ctrl.Enabled == false) ctrl.Enabled = true;
			}
		}

		
		private void ScanControl(Control pobjCtrl)
		{
			if(pobjCtrl == null) return;
			pobjCtrl.EnabledChanged -= new EventHandler(objCtrl_EnabledChanged);
			if(pobjCtrl.Controls.Count > 0)
			{
				foreach(Control ctr in pobjCtrl.Controls)
				{
					ScanControl(ctr);
				}
			}
		}

		private void DoLoadTreeFull(TreeNode pobjNode, string pstrVisibilityGroupID)
		{
			DataRow[] drowControls = dstData.Tables[Sys_VisibilityGroupTable.TABLE_NAME].Select(
							Sys_VisibilityGroupTable.PARENTID_FLD + " = " + pstrVisibilityGroupID,
							Sys_VisibilityGroupTable.TYPE_FLD + " ASC," +
							Sys_VisibilityGroupTable.GROUPTEXT_FLD + " ASC");
			foreach(DataRow dr in drowControls)
			{
				if(dr[Sys_VisibilityGroupTable.TYPE_FLD] != DBNull.Value)
				{
					if(int.Parse(dr[Sys_VisibilityGroupTable.TYPE_FLD].ToString()) == VisibilityGroupTypeEnum.Container.GetHashCode()) //NATURE_GROUP)
					{
						TreeNode objNode = new TreeNode(dr[Sys_VisibilityGroupTable.GROUPTEXT_FLD].ToString(), IMAGE_CONTAINER, IMAGE_CONTAINER);
						TagData tagData = new TagData();
						tagData.intGroupID = int.Parse(dr[Sys_VisibilityGroupTable.VISIBILITYGROUPID_FLD].ToString());
						objNode.Tag = tagData;
						pobjNode.Nodes.Add(objNode);
						// Set check
						DataRow[] drRights = dstData.Tables[Sys_VisibilityGroup_RoleTable.TABLE_NAME].Select(
								Sys_VisibilityGroup_RoleTable.GROUPID_FLD + "=" + tagData.intGroupID
							+ " AND "+ Sys_VisibilityGroup_RoleTable.ROLEID_FLD + "=" + cboRoles.SelectedValue.ToString());
						if(drRights.Length > 0)
						{
							objNode.Checked = false;
						}
						else
						{
							objNode.Checked = true;
						}
						DoLoadTreeFull(objNode,dr[Sys_VisibilityGroupTable.VISIBILITYGROUPID_FLD].ToString());
						
					}
					else if(int.Parse(dr[Sys_VisibilityGroupTable.TYPE_FLD].ToString()) == VisibilityGroupTypeEnum.GroupNormal.GetHashCode()) //DEFINE_GROUP)
					{
						TreeNode objNode = new TreeNode(dr[Sys_VisibilityGroupTable.GROUPTEXT_FLD].ToString(), IMAGE_GROUP, IMAGE_GROUP);
						TagData tagData = new TagData();
						tagData.intGroupID = int.Parse(dr[Sys_VisibilityGroupTable.VISIBILITYGROUPID_FLD].ToString());
						objNode.Tag = tagData;
						pobjNode.Nodes.Add(objNode);
						// Set check
						DataRow[] drRights = dstData.Tables[Sys_VisibilityGroup_RoleTable.TABLE_NAME].Select(
							Sys_VisibilityGroup_RoleTable.GROUPID_FLD + "=" + tagData.intGroupID
							+ " AND " + Sys_VisibilityGroup_RoleTable.ROLEID_FLD + "=" + cboRoles.SelectedValue.ToString());
						if(drRights.Length > 0)
						{
							objNode.Checked = false;
						}
						else
						{
							objNode.Checked = true;
						}

					}
				}
			}
		}


		private void GroupControls_Load(object sender, EventArgs e)
		{
			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.WaitCursor;

			#endregion Code Inserted Automatically

			
			const string METHOD_NAME = THIS + ".GroupControls_Load()";
			try 
			{
				#region Security
				if(!SystemProperty.UserName.ToLower().Equals(Constants.SUPER_ADMIN_USER))
				{ 
					//Set authorization for user
					Security objSecurity = new Security();
					this.Name = THIS;
					if(objSecurity.SetRightForUserOnForm(this, SystemProperty.UserName) == 0)
					{
						this.Close();
						// You don't have the right to view this item
						PCSMessageBox.Show(ErrorCode.MESSAGE_YOU_HAVE_NO_RIGHT_TO_VIEW ,MessageBoxIcon.Warning);
						return;
					}
				}
				#endregion

				DoLoadData(ref dstData);
				cboRoles.DataSource = dstData.Tables[Sys_RoleTable.TABLE_NAME];
				cboRoles.DisplayMember = Sys_RoleTable.NAME_FLD;
				cboRoles.ValueMember = Sys_RoleTable.ROLEID_FLD;
				if(dstData.Tables[Sys_RoleTable.TABLE_NAME].Rows.Count == 0)
				{
					return;
				}
				cboRoles.SelectedValue = int.Parse(dstData.Tables[Sys_RoleTable.TABLE_NAME].Rows[0][Sys_RoleTable.ROLEID_FLD].ToString());
				tvwGroups.Nodes.Clear();
				if(objForm == null)
				{
					blnCorrectingCheck = true;
					btnSave.Top = btnAdd.Top;
					btnSave.Left = btnAdd.Left;
					btnAdd.Visible = btnEdit.Visible = btnDelete.Visible = false;
					// this.StartPosition = FormStartPosition.CenterScreen;
					splView.Visible = pnlRight.Visible = false;
					pnlLeft.Dock = DockStyle.Fill;
					tvwGroups.LabelEdit = false;
					

					// TODO: Build full tree for all form
					DataRow[] drowForms = dstData.Tables[Sys_VisibilityGroupTable.TABLE_NAME].Select(
						Sys_VisibilityGroupTable.TYPE_FLD + "=" + VisibilityGroupTypeEnum.Container.GetHashCode() //NATURE_GROUP
						+ " AND " + Sys_VisibilityGroupTable.PARENTID_FLD + " = 0",
						Sys_VisibilityGroupTable.GROUPTEXT_FLD + " ASC");
					foreach(DataRow dr in drowForms)
					{
						if(dr[Sys_VisibilityGroupTable.CONTROLNAME_FLD] != DBNull.Value)
						{
							TreeNode objNode = new TreeNode(dr[Sys_VisibilityGroupTable.GROUPTEXT_FLD].ToString(), IMAGE_FORM, IMAGE_FORM);
							TagData tagData = new TagData();
							tagData.intGroupID = int.Parse(dr[Sys_VisibilityGroupTable.VISIBILITYGROUPID_FLD].ToString());
							objNode.Tag = tagData;
							tvwGroups.Nodes.Add(objNode);
							// Set check
							DataRow[] drRights = dstData.Tables[Sys_VisibilityGroup_RoleTable.TABLE_NAME].Select(
								Sys_VisibilityGroup_RoleTable.GROUPID_FLD + "=" + tagData.intGroupID +
								" AND " + Sys_VisibilityGroup_RoleTable.ROLEID_FLD + "=" + cboRoles.SelectedValue.ToString());
							if(drRights.Length > 0)
							{
								objNode.Checked = false;
							}
							else
							{
								objNode.Checked = true;
							}
							DoLoadTreeFull(objNode,dr[Sys_VisibilityGroupTable.VISIBILITYGROUPID_FLD].ToString());
						}
					}
					blnCorrectingCheck = false;
				}
				else
				{//TreeViewEventHandler hander =
					tvwGroups.AfterCheck -= new TreeViewEventHandler(tvwGroups_AfterCheck);
					tvwGroups.CheckBoxes = false;
					cboRoles.Enabled = false;
					DoLoadTree(tvwGroups);
					DoSynchonize(dstData,tvwGroups);
					objForm.Text = objForm.Text + SUPER_MODE_TEXT;
					// ScanControl(objForm);
					
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


		private void lvwControls_DragOver(object sender, DragEventArgs e)
		{
			const string METHOD_NAME = THIS + ".lvwControls_DragOver()";
			try 
			{
				if (tvwGroups.SelectedNode == null)
				{
					e.Effect = DragDropEffects.None;
				}
				else
				{
					string[] arrDragDropData = e.Data.GetData(DataFormats.StringFormat).ToString().Split(DATA_SEPARATOR);
					if ((arrDragDropData.Length > 0) && (IsContainerOf(((TagData)tvwGroups.SelectedNode.Tag).objControl,arrDragDropData[0])))
					{
						e.Effect = DragDropEffects.All;
					}
					else
					{
						e.Effect = DragDropEffects.None;
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
		}


		private void lvwControls_DragDrop(object sender, DragEventArgs e)
		{
			const string METHOD_NAME = THIS + ".lvwControls_DragDrop()";
			try 
			{
				string[] arrDragDropData = e.Data.GetData(DataFormats.StringFormat).ToString().Split(DATA_SEPARATOR);
				TreeNode objCurrentNode = tvwGroups.SelectedNode;
				if (objCurrentNode == null)
				{
					return;
				}
				blnIsChanged = true;
				if (arrDragDropData.Length > 1)
				{
					//Invisible and disable in hooked form
					EnableDestinationControl(objForm,arrDragDropData[0], false);

					#region // HACK: DEL DuongNA 2005-10-19
					//					//Add to list
					//					ListViewItem objItem = lvwControls.Items.Add(SQR_BEGIN + arrDragDropData[0] + SQR_END + arrDragDropData[1],IMAGE_CONTROL);
					//					objItem.Tag = arrDragDropData[0];
					#endregion // END: DEL DuongNA 2005-10-19
					
					//Find row of this control
					int intGroupID = ((TagData)objCurrentNode.Tag).intGroupID;
					DataRow drItem = null;
					DataRow[] drItems = dstData.Tables[Sys_VisibilityItemTable.TABLE_NAME].Select(
						Sys_VisibilityItemTable.GROUPID_FLD + "=" + intGroupID);

					foreach (DataRow dr in drItems)
					{
						if(dr.RowState == DataRowState.Deleted) continue;
						if (dr[Sys_VisibilityItemTable.NAME_FLD].ToString().Equals(arrDragDropData[0].Trim()))
						{
							drItem = dr;
							break;
						}
					}

					if (drItem == null)
					{
						//Add new to dataset
						//Sys_VisibilityItemVO voItem = new Sys_VisibilityItemVO();
						drItem = dstData.Tables[Sys_VisibilityItemTable.TABLE_NAME].NewRow();
						drItem[Sys_VisibilityItemTable.NAME_FLD] = arrDragDropData[0];
						drItem[Sys_VisibilityItemTable.GROUPID_FLD] = intGroupID;
						dstData.Tables[Sys_VisibilityItemTable.TABLE_NAME].Rows.Add(drItem);
					}
//					else
//					{
//						//Update group id
//						drItem[Sys_VisibilityItemTable.GROUPID_FLD] = intGroupID;
//					}

					tvwGroups_AfterSelect(null,null);
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


		private void btnAdd_Click(object sender, EventArgs e)
		{
			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.WaitCursor;

			#endregion Code Inserted Automatically

			
			const string METHOD_NAME = THIS + ".btnAdd_Click()";
			try 
			{
				DoAdd(NEW_NODE_TEXT);
				tvwGroups.SelectedNode.BeginEdit();
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


		private void tvwGroups_KeyDown(object sender, KeyEventArgs e)
		{
			const string METHOD_NAME = THIS + ".tvwGroups_KeyDown()";
			try 
			{
				switch (e.KeyCode)
				{
					case Keys.F2:
						DoEdit();
						e.Handled = true;
						break;
//					case Keys.Delete:
//						DoDeleteGroup();
//						e.Handled = true;
//						break;
					default:
						break;
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

	
		private void btnEdit_Click(object sender, EventArgs e)
		{
			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.WaitCursor;

			#endregion Code Inserted Automatically

			
			const string METHOD_NAME = THIS + ".btnEdit_Click()";
			try 
			{
				this.DoEdit();
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


		private void btnClose_Click(object sender, EventArgs e)
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


		private void btnDelete_Click(object sender, EventArgs e)
		{
			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.WaitCursor;

			#endregion Code Inserted Automatically

			
			const string METHOD_NAME = THIS + ".btnDelete_Click()";
			try 
			{
				DialogResult result = PCSMessageBox.Show(ErrorCode.MESSAGE_DELETE_RECORD,MessageBoxButtons.YesNo, MessageBoxIcon.Question);
				if(result == DialogResult.Yes)
				{
					this.DoDeleteGroup();
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


		private void lvwControls_KeyDown(object sender, KeyEventArgs e)
		{
			const string METHOD_NAME = THIS + ".lvwControls_KeyDown()";
			try
			{
				if(tvwGroups.SelectedNode != null)
				{
					if(((TagData)tvwGroups.SelectedNode.Tag).objControl != null)
					{
						switch (e.KeyCode)
						{
							case Keys.Delete:
								this.DoDeleteItem();
								e.Handled = true;
								break;
							default:
								break;
						}
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
		}

	
		private void objForm_Closed(object sender, EventArgs e)
		{
			this.Close();
		}

	
		private void btnSave_Click(object sender, EventArgs e)
		{
			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.WaitCursor;

			#endregion Code Inserted Automatically

			
			const string METHOD_NAME = THIS + ".btnSave_Click()";
			try 
			{
				DoSave();
				// display successful message
				PCSMessageBox.Show(ErrorCode.MESSAGE_AFTER_SAVE_DATA, MessageBoxButtons.OK, MessageBoxIcon.Information);
				// turn to DEFAULT mode
				blnIsChanged = false;
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

		private void tvwGroups_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnSave_Click()";
			try
			{
				blnIsChanged = true;
				//DataRow drGroup = dstData.Tables[Sys_VisibilityGroupTable.TABLE_NAME].Rows.Find(((TagData)e.Node.Tag).intGroupID);
				DataRow drGroup = null;
				foreach (DataRow dr in dstData.Tables[Sys_VisibilityGroupTable.TABLE_NAME].Rows)
				{
					if(dr.RowState == DataRowState.Deleted) continue;
					if((TagData)e.Node.Tag != null)
						if(dr[Sys_VisibilityGroupTable.VISIBILITYGROUPID_FLD] != DBNull.Value)
							if (int.Parse(dr[Sys_VisibilityGroupTable.VISIBILITYGROUPID_FLD].ToString()) == ((TagData)e.Node.Tag).intGroupID)
							{
								drGroup = dr;
								break;
							}
				}
				if (drGroup == null)
				{
					return;
				}

				if (e.Label == null)
				{
					e.CancelEdit = true;
				}
				else
				{
					drGroup[Sys_VisibilityGroupTable.GROUPTEXT_FLD] = e.Label;
					//TODO : Multilingual process
					e.CancelEdit = false;
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

		private void tvwGroups_AfterSelect(object sender, TreeViewEventArgs e)
		{
			const string METHOD_NAME = THIS + ".tvwGroups_AfterSelect()";
			try
			{
				//Reload Listview
				lvwControls.Items.Clear();
				TreeNode objCurrentNode = tvwGroups.SelectedNode;//e.Node;
				if (objCurrentNode == null)
				{
					return;
				}
				int intGroupID;
				try
				{
					intGroupID = ((TagData)objCurrentNode.Tag).intGroupID;
				}
				catch
				{
					return;
				}
				//Get all item
				foreach (DataRow dr in dstData.Tables[Sys_VisibilityItemTable.TABLE_NAME].Rows)
				{
					if(dr.RowState != DataRowState.Deleted)
						if(dr[Sys_VisibilityItemTable.GROUPID_FLD] != null)
							if (Convert.ToInt32(dr[Sys_VisibilityItemTable.GROUPID_FLD].ToString()) == intGroupID)
							{
								ListViewItem objItem = lvwControls.Items.Add(SQR_BEGIN + dr[Sys_VisibilityItemTable.NAME_FLD].ToString() + SQR_END,IMAGE_CONTROL);
								objItem.Tag = dr[Sys_VisibilityItemTable.NAME_FLD].ToString();
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
		}

		private void tvwGroups_AfterCheck(object sender, System.Windows.Forms.TreeViewEventArgs e)
		{
			const string METHOD_NAME = THIS + ".tvwGroups_AfterCheck()";
			// Return if set check by code
			if (blnCorrectingCheck)
			{
				return;
			}
			blnIsChanged = true;
			//Stack stkNodes = new Stack();
			try
			{
				TreeNode objCurrentNode = e.Node;
				if (objCurrentNode == null)
				{
					return;
				}

				#region // HACK: DEL DuongNA 2005-10-19
								//				stkNodes.Push(objCurrentNode);
								//				if ((objCurrentNode != null) && (objCurrentNode.Checked))
								//				{					
								//					//Check all of its parents
								//					while (true)
								//					{
								//						try
								//						{
								//							TreeNode objNode = (TreeNode)stkNodes.Pop();
								//							objNode.Checked = true;
								//							if (objNode.Parent != null)
								//							{
								//								stkNodes.Push(objNode.Parent);
								//							}
								//						}
								//						catch
								//						{
								//							break;
								//						}
								//					}
								//				}
								//				else
								//				{
								//					//UnCheck all of its child
								//					while (true)
								//					{
								//						try
								//						{
								//							TreeNode objNode = (TreeNode)stkNodes.Pop();
								//							objNode.Checked = false;
								//							foreach (TreeNode objChild in objNode.Nodes)
								//							{
								//								stkNodes.Push(objChild);
								//							}
								//						}
								//						catch
								//						{
								//							break;
								//						}
								//					}
								//				}

					#endregion // END: DEL DuongNA 2005-10-19			
				
				if (objCurrentNode.Checked)
				{
					//Delete from dataset
					int intGroupID = ((TagData)objCurrentNode.Tag).intGroupID;
					int intRoleID = int.Parse(cboRoles.SelectedValue.ToString());
					// HACK: SonHT 2005-10-15
					DataRow[] drowGroups = dstData.Tables[Sys_VisibilityGroup_RoleTable.TABLE_NAME].Select(
						Sys_VisibilityGroup_RoleTable.GROUPID_FLD + "=" + intGroupID
						+ " AND " + Sys_VisibilityGroup_RoleTable.ROLEID_FLD + "=" + intRoleID);
					foreach (DataRow dr in drowGroups)
					{
						if(dr.RowState == DataRowState.Deleted) continue;
						dr.Delete();
//						int intGID = int.Parse(dr[Sys_VisibilityGroup_RoleTable.GROUPID_FLD].ToString());
//						int intRID = int.Parse(dr[Sys_VisibilityGroup_RoleTable.ROLEID_FLD].ToString());
//						if ((intGroupID == intGID) && (intRoleID == intRID))
//						{
//							dr.Delete();
//							break;
//						}
					}
					
					// End: SonHT 2005-10-15
					//Check it parents
					if (objCurrentNode.Parent != null)
					{
						objCurrentNode.Parent.Checked = true;
					}
//					foreach (TreeNode objChild in objCurrentNode.Nodes)
//					{
//						objChild.Checked = true;
//					}
				}
				else
				{
					//Add to dataset
					int intGroupID = ((TagData)objCurrentNode.Tag).intGroupID;
					int intRoleID = int.Parse(cboRoles.SelectedValue.ToString());
					DataRow dr = dstData.Tables[Sys_VisibilityGroup_RoleTable.TABLE_NAME].NewRow();
					dr[Sys_VisibilityGroup_RoleTable.GROUPID_FLD] = intGroupID;
					dr[Sys_VisibilityGroup_RoleTable.ROLEID_FLD] = intRoleID;
					dstData.Tables[Sys_VisibilityGroup_RoleTable.TABLE_NAME].Rows.Add(dr);
				
					foreach (TreeNode objChild in objCurrentNode.Nodes)
					{
						objChild.Checked = false;
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
		}

		private void GroupControls_Closed(object sender, System.EventArgs e)
		{
			//objForm.Close();
		}

		private void GroupControls_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			// Orcurs whenever users closed form
			const string METHOD_NAME = THIS + ".SaleOrder_Closing()";
			try
			{
				// if the form has been change then ask to store database
				if(blnIsChanged) 
				{
					DialogResult enumDialog = PCSMessageBox.Show(ErrorCode.MESSAGE_QUESTION_STORE_INTO_DATABASE,MessageBoxButtons.YesNoCancel,MessageBoxIcon.Question);
					if( enumDialog == DialogResult.Yes)
					{
						// store database
						//StoreDatabase();
						btnSave_Click(sender, e);
						e.Cancel = false;
					} 
					else if( enumDialog == DialogResult.No) // click No button
					{
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

		#endregion

		private TreeNode GetRoot(TreeNode pobjNode)
		{
			if(pobjNode.Parent != null)
			{
				return GetRoot(pobjNode.Parent);
			}
			else
			{
				return pobjNode;
			}
		}

		private void cboRoles_SelectedValueChanged(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".cboRoles_SelectedValueChanged()";
			try
			{
				blnCorrectingCheck = true;
				// Do not process on load form that has no RoleID
				int intRoleID = -1;
				try
				{
					intRoleID = int.Parse(cboRoles.SelectedValue.ToString());
				}
				catch
				{
					// Do nothing 
				}

				if (intRoleID > -1)
				{
					// Remove all node
					tvwGroups.Nodes.Clear();

					// TODO: Build full tree for all form
					DataRow[] drowForms = dstData.Tables[Sys_VisibilityGroupTable.TABLE_NAME].Select(
						Sys_VisibilityGroupTable.TYPE_FLD + "=" + VisibilityGroupTypeEnum.Container.GetHashCode(), //NATURE_GROUP
						// + " AND " + Sys_VisibilityGroupTable.PARENTID_FLD + " = 0",
						Sys_VisibilityGroupTable.GROUPTEXT_FLD + " ASC");
					foreach(DataRow dr in drowForms)
					{
						if((dr[Sys_VisibilityGroupTable.PARENTID_FLD].ToString() == 0.ToString()) 
							|| (dr[Sys_VisibilityGroupTable.PARENTID_FLD].ToString() == string.Empty))
						if(dr[Sys_VisibilityGroupTable.CONTROLNAME_FLD] != DBNull.Value)
						{
							TreeNode objNode = new TreeNode(dr[Sys_VisibilityGroupTable.GROUPTEXT_FLD].ToString(), IMAGE_FORM, IMAGE_FORM);
							TagData tagData = new TagData();
							tagData.intGroupID = int.Parse(dr[Sys_VisibilityGroupTable.VISIBILITYGROUPID_FLD].ToString());
							objNode.Tag = tagData;
							tvwGroups.Nodes.Add(objNode);
							// Set check
							DataRow[] drRights = dstData.Tables[Sys_VisibilityGroup_RoleTable.TABLE_NAME].Select(
								Sys_VisibilityGroup_RoleTable.GROUPID_FLD + "=" + tagData.intGroupID +
								" AND " + Sys_VisibilityGroup_RoleTable.ROLEID_FLD + "=" + cboRoles.SelectedValue.ToString());
							if(drRights.Length > 0)
							{
								objNode.Checked = false;
							}
							else
							{
								objNode.Checked = true;
							}
							DoLoadTreeFull(objNode,dr[Sys_VisibilityGroupTable.VISIBILITYGROUPID_FLD].ToString());
						}
					}
					blnCorrectingCheck = false;
					//if(tvwGroups.SelectedNode != null)
					//{
						//TreeNode objNode = GetRoot(tvwGroups.SelectedNode);
						//CheckAllNode(true);
						//CorrectNodeCheck();
					//}
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
			finally
			{				
				blnCorrectingCheck = false;
			}
		}

		private void CheckAllNode(bool pblnChecked)
		{
			for (int i = 0; i < tvwGroups.Nodes.Count; i++)
			{
				tvwGroups.Nodes[i].Checked = pblnChecked;
			}
		}

		private void tvwGroups_DoubleClick(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".tvwGroups_DoubleClick()";
			const string SEACHFORM = "...";
			if(tvwGroups.CheckBoxes) return;
			try
			{
				if(tvwGroups.SelectedNode != null)
				{
					TagData tagData = (TagData)tvwGroups.SelectedNode.Tag;
					if(tagData.objControl == null) return;
					if(tagData.objControl is C1TrueDBGrid)
					{
						if (tvwGroups.SelectedNode.Nodes.Count == 0)
							if (tvwGroups.SelectedNode.ImageIndex != IMAGE_GROUP)
							{
								C1TrueDBGrid objGrid = (C1TrueDBGrid)tagData.objControl;
								foreach (C1DisplayColumn colC1 in objGrid.Splits[0].DisplayColumns)
								{
									if(colC1.Visible)
									{
										DoAdd(colC1.DataColumn.Caption);
										//Add new to dataset
										//Sys_VisibilityItemVO voItem = new Sys_VisibilityItemVO();
										int intGroupID = ((TagData)tvwGroups.SelectedNode.Tag).intGroupID;
										DataRow drItem = dstData.Tables[Sys_VisibilityItemTable.TABLE_NAME].NewRow();
										drItem[Sys_VisibilityItemTable.NAME_FLD] = colC1.DataColumn.DataField;
										drItem[Sys_VisibilityItemTable.GROUPID_FLD] = intGroupID;
										drItem[Sys_VisibilityItemTable.TYPE_FLD] = VisibilityItemTypeEnum.ColumnTrueDBGrid.GetHashCode();
										dstData.Tables[Sys_VisibilityItemTable.TABLE_NAME].Rows.Add(drItem);
									}
								}
							}
					}
					else if(tagData.objControl.Controls.Count > 0)
					{
						// If node has atleast one node is group then do nothing
						foreach(TreeNode treeNode in tvwGroups.SelectedNode.Nodes)
						{
							if(treeNode.ImageIndex == IMAGE_GROUP) return;
						}
						// HACK: SONHT HardCode
						System.Windows.Forms.DialogResult result = PCSMessageBox.Show(MESSAGE_GENERATE_AUTOMATIC,MessageBoxButtons.YesNo,MessageBoxIcon.Question,MessageBoxDefaultButton.Button2);
						if(result == DialogResult.Yes)
						{
							if (tvwGroups.SelectedNode.ImageIndex != IMAGE_GROUP)
								foreach(Control objCtr in tagData.objControl.Controls)
								{
									if((objCtr is Label) || (objCtr is CheckBox) || (objCtr is RadioButton))
									{
										bool blnIsExisted = false;
										foreach(TreeNode treeNode in tvwGroups.SelectedNode.Nodes)
										{
											if(((TagData)treeNode.Tag).objControl.Name==objCtr.Name)
											{
												blnIsExisted = true;
												break;
											}
										}
										if(!blnIsExisted)
										{
											DoAdd(objCtr.Text);
											//Add new to dataset
											//Sys_VisibilityItemVO voItem = new Sys_VisibilityItemVO();
											int intGroupID = ((TagData)tvwGroups.SelectedNode.Tag).intGroupID;
											DataRow drItem = dstData.Tables[Sys_VisibilityItemTable.TABLE_NAME].NewRow();
											drItem[Sys_VisibilityItemTable.NAME_FLD] = objCtr.Name;
											drItem[Sys_VisibilityItemTable.GROUPID_FLD] = intGroupID;
											dstData.Tables[Sys_VisibilityItemTable.TABLE_NAME].Rows.Add(drItem);
										}
									}
									else if(objCtr is Button)
									{
										if(objCtr.Text != SEACHFORM)
										{
											bool blnIsExisted = false;
											foreach(TreeNode treeNode in tvwGroups.SelectedNode.Nodes)
											{
												if(((TagData)treeNode.Tag).objControl.Name==objCtr.Name)
												{
													blnIsExisted = true;
													break;
												}
											}
											if(!blnIsExisted)
											{
												DoAdd(objCtr.Text.Replace(AND,string.Empty));
												//Add new to dataset
												//Sys_VisibilityItemVO voItem = new Sys_VisibilityItemVO();
												int intGroupID = ((TagData)tvwGroups.SelectedNode.Tag).intGroupID;
												DataRow drItem = dstData.Tables[Sys_VisibilityItemTable.TABLE_NAME].NewRow();
												drItem[Sys_VisibilityItemTable.NAME_FLD] = objCtr.Name;
												drItem[Sys_VisibilityItemTable.GROUPID_FLD] = intGroupID;
												dstData.Tables[Sys_VisibilityItemTable.TABLE_NAME].Rows.Add(drItem);
											}
										}
									}
//									else if(objCtr is GroupBox)
//									{
//										bool blnIsExisted = false;
//										foreach(TreeNode treeNode in tvwGroups.SelectedNode.Nodes)
//										{
//											if(((TagData)treeNode.Tag).objControl.Name==objCtr.Name)
//											{
//												blnIsExisted = true;
//												break;
//											}
//										}
//										if(!blnIsExisted)
//										{
//											DoAdd(objCtr.Text);
//											//Add new to dataset
//											//Sys_VisibilityItemVO voItem = new Sys_VisibilityItemVO();
//											tvwGroups.SelectedNode.ImageIndex = IMAGE_CONTAINER;
//											int intGroupID = ((TagData)tvwGroups.SelectedNode.Tag).intGroupID;
//											DataRow drItem = dstData.Tables[Sys_VisibilityItemTable.TABLE_NAME].NewRow();
//											drItem[Sys_VisibilityItemTable.NAME_FLD] = objCtr.Name;
//											drItem[Sys_VisibilityItemTable.GROUPID_FLD] = intGroupID;
//											drItem[Sys_VisibilityItemTable.TYPE_FLD] = VisibilityGroupTypeEnum.Container;
//											dstData.Tables[Sys_VisibilityItemTable.TABLE_NAME].Rows.Add(drItem);
//										}	
//									}
								}
						}
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
		}
	}
}
