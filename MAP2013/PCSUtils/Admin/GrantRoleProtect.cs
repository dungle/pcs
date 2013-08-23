using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using C1.Win.C1FlexGrid;

//PCS namespaces
using PCSComUtils.Admin.DS;
using PCSComUtils.Admin.BO;
using PCSComUtils.Common;
using PCSComUtils.PCSExc;
using PCSUtils.Log;
using PCSUtils.Utils;

namespace PCSUtils.Admin
{
	/// <summary>
	/// Summary description for GrantRoleProtect.
	/// </summary>
	public class GrantRoleProtect : System.Windows.Forms.Form
	{
		#region Declaration
		
		#region System Generated
		
		private Panel pnlRoleProtection;
		private GroupBox grpProtection;
		private GroupBox grpRole;
		private Button btnSave;
		private Button btnClose;	// Store status of ProtectionTree
		private C1FlexGrid c1FlexGridProtect;
		private Splitter splitter1;
		private C1FlexGrid c1FlexGridRole;
		private System.Windows.Forms.Button btnHelp;
		
		#endregion System Generated

		private const string THIS = "PCSUtils.Admin.GrantRoleProtect";
		// Constant declare
		private const int INT_START = 1;
		private const int COLUMN_WIDTH_BASE = 30;
		// const position of column
		const int INT_POS_TREE = 1;
		const int INT_POS_ALL = 2;
		const int INT_POS_VIEW = 3;
		const int INT_POS_ADD = 4;
		const int INT_POS_EDIT = 5;
		const int INT_POS_DELETE = 6;
		const int INT_POS_PRINT = 7;
		const int INT_POS_MENU_ENTRY = 8;
		const int INT_POS_PARENT_CHILD = 9;
		const int INT_POS_PERMISSION = 10;

		// Action const
		private const int ACTION_NONE = 0;
		private const int ACTION_VIEW = 1;
		private const int ACTION_ADD = 2;
		private const int ACTION_EDIT = 4;
		private const int ACTION_DELETE = 8;
		private const int ACTION_PRINT = 16;
		private const int ACTION_ALL = 31;
		
		private bool blnIsModified = false;
		
		private int intCurrentRow = 0;
		private DataSet dstProtection = new DataSet();		
		private ArrayList arrSysMenuEntry = new ArrayList();

		#endregion

		#region Windows Form Designer generated code

		public GrantRoleProtect()
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
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GrantRoleProtect));
            this.pnlRoleProtection = new System.Windows.Forms.Panel();
            this.grpProtection = new System.Windows.Forms.GroupBox();
            this.c1FlexGridProtect = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.grpRole = new System.Windows.Forms.GroupBox();
            this.c1FlexGridRole = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnHelp = new System.Windows.Forms.Button();
            this.pnlRoleProtection.SuspendLayout();
            this.grpProtection.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1FlexGridProtect)).BeginInit();
            this.grpRole.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1FlexGridRole)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlRoleProtection
            // 
            this.pnlRoleProtection.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlRoleProtection.Controls.Add(this.grpProtection);
            this.pnlRoleProtection.Controls.Add(this.splitter1);
            this.pnlRoleProtection.Controls.Add(this.grpRole);
            this.pnlRoleProtection.Location = new System.Drawing.Point(4, 4);
            this.pnlRoleProtection.Name = "pnlRoleProtection";
            this.pnlRoleProtection.Size = new System.Drawing.Size(668, 392);
            this.pnlRoleProtection.TabIndex = 0;
            // 
            // grpProtection
            // 
            this.grpProtection.Controls.Add(this.c1FlexGridProtect);
            this.grpProtection.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpProtection.Location = new System.Drawing.Point(324, 0);
            this.grpProtection.Name = "grpProtection";
            this.grpProtection.Size = new System.Drawing.Size(344, 392);
            this.grpProtection.TabIndex = 1;
            this.grpProtection.TabStop = false;
            this.grpProtection.Text = "List of Functions";
            // 
            // c1FlexGridProtect
            // 
            this.c1FlexGridProtect.ColumnInfo = resources.GetString("c1FlexGridProtect.ColumnInfo");
            this.c1FlexGridProtect.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1FlexGridProtect.Location = new System.Drawing.Point(3, 16);
            this.c1FlexGridProtect.Name = "c1FlexGridProtect";
            this.c1FlexGridProtect.Rows.Count = 1;
            this.c1FlexGridProtect.Rows.DefaultSize = 17;
            this.c1FlexGridProtect.Size = new System.Drawing.Size(338, 373);
            this.c1FlexGridProtect.StyleInfo = resources.GetString("c1FlexGridProtect.StyleInfo");
            this.c1FlexGridProtect.TabIndex = 0;
            this.c1FlexGridProtect.Tree.Column = 1;
            this.c1FlexGridProtect.Tree.Indent = 10;
            this.c1FlexGridProtect.KeyUp += new System.Windows.Forms.KeyEventHandler(this.c1FlexGridProtect_KeyUp);
            this.c1FlexGridProtect.Click += new System.EventHandler(this.c1FlexGridProtect_Click);
            // 
            // splitter1
            // 
            this.splitter1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.splitter1.Location = new System.Drawing.Point(321, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 392);
            this.splitter1.TabIndex = 7;
            this.splitter1.TabStop = false;
            // 
            // grpRole
            // 
            this.grpRole.Controls.Add(this.c1FlexGridRole);
            this.grpRole.Dock = System.Windows.Forms.DockStyle.Left;
            this.grpRole.Location = new System.Drawing.Point(0, 0);
            this.grpRole.Name = "grpRole";
            this.grpRole.Size = new System.Drawing.Size(321, 392);
            this.grpRole.TabIndex = 0;
            this.grpRole.TabStop = false;
            this.grpRole.Text = "List of Roles";
            // 
            // c1FlexGridRole
            // 
            this.c1FlexGridRole.AllowEditing = false;
            this.c1FlexGridRole.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            this.c1FlexGridRole.ColumnInfo = "4,1,0,0,0,85,Columns:0{Width:19;}\t1{Width:27;Name:\"ID\";Visible:False;}\t2{Width:68" +
                ";Name:\"colName\";Caption:\"Role name\";}\t3{Width:166;Name:\"colDescription\";Caption:" +
                "\"Description\";}\t";
            this.c1FlexGridRole.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1FlexGridRole.Location = new System.Drawing.Point(3, 16);
            this.c1FlexGridRole.Name = "c1FlexGridRole";
            this.c1FlexGridRole.Rows.Count = 1;
            this.c1FlexGridRole.Rows.DefaultSize = 17;
            this.c1FlexGridRole.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.c1FlexGridRole.Size = new System.Drawing.Size(315, 373);
            this.c1FlexGridRole.StyleInfo = resources.GetString("c1FlexGridRole.StyleInfo");
            this.c1FlexGridRole.TabIndex = 0;
            this.c1FlexGridRole.BeforeRowColChange += new C1.Win.C1FlexGrid.RangeEventHandler(this.c1FlexGridRole_BeforeRowColChange);
            this.c1FlexGridRole.AfterRowColChange += new C1.Win.C1FlexGrid.RangeEventHandler(this.c1FlexGridRole_AfterRowColChange);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSave.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnSave.Location = new System.Drawing.Point(4, 402);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(60, 23);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "&Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnClose.Location = new System.Drawing.Point(612, 402);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(60, 23);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "&Close";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnHelp
            // 
            this.btnHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnHelp.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnHelp.Location = new System.Drawing.Point(550, 402);
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Size = new System.Drawing.Size(60, 23);
            this.btnHelp.TabIndex = 1;
            this.btnHelp.Text = "&Help";
            // 
            // GrantRoleProtect
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(678, 429);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.pnlRoleProtection);
            this.Controls.Add(this.btnHelp);
            this.Name = "GrantRoleProtect";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Right Assignment";
            this.Load += new System.EventHandler(this.GrantRoleProtect_Load);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.GrantRoleProtect_Closing);
            this.pnlRoleProtection.ResumeLayout(false);
            this.grpProtection.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1FlexGridProtect)).EndInit();
            this.grpRole.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1FlexGridRole)).EndInit();
            this.ResumeLayout(false);

		}
		#endregion		

		#region Class's Methods

		//**************************************************************************
		/// <summary>
		/// Load data from arrList into TreeProtect, This function 
		///	will build the tree, get data from collection arrStorePermission.
		///	RoleProtectionForm_Load and c1FlexGridRole_AfterRowColChange will call this function.
		/// </summary>
		/// <param name="c1FlexGridProtect"></param>
		/// <param name="pstrShortcut"></param>
		/// <param name="pintLevel"></param>
		/// <Author> Son HT, Dec 30, 2004</Author>
		private void LoadTreeProtect(ref C1FlexGrid c1FlexGridProtect,string pstrShortcut, int pintLevel)
		{
		    // Scan all rows in Sys_Menu_Entry table to build tree 
		    foreach (var voMenu in arrSysMenuEntry.Cast<Sys_Menu_EntryVO>().Where(voMenu => (voMenu.Parent_Shortcut.Equals(pstrShortcut))))
		    {
		        // Root Node
		        intCurrentRow++;
		        Node objNode = c1FlexGridProtect.Rows.InsertNode(intCurrentRow, pintLevel);
		        // Set Text for tree node
		        c1FlexGridProtect[intCurrentRow, INT_POS_TREE] = voMenu.Text_CaptionDefault;
		        c1FlexGridProtect[intCurrentRow, INT_POS_MENU_ENTRY] = voMenu.Menu_EntryID;
		        c1FlexGridProtect[intCurrentRow, INT_POS_PARENT_CHILD] = voMenu.Parent_Child;
		        // If it is a leaf, then set checkbox otherwise don't set
		        if (voMenu.Parent_Child == (int)MenuParentChildEnum.LeafMenu)
		        {
		            // Set check box for next cols
		            c1FlexGridProtect.SetCellCheck(intCurrentRow, INT_POS_TREE, CheckEnum.Unchecked);
		            // Set checking for column 'All'
		            c1FlexGridProtect.SetCellCheck(intCurrentRow, INT_POS_ALL, CheckEnum.TSUnchecked);
		            // Set checking for Columns
		            c1FlexGridProtect.SetCellCheck(intCurrentRow, INT_POS_VIEW, CheckEnum.Unchecked);
		            c1FlexGridProtect.SetCellCheck(intCurrentRow, INT_POS_ADD, CheckEnum.Unchecked);
		            c1FlexGridProtect.SetCellCheck(intCurrentRow, INT_POS_EDIT, CheckEnum.Unchecked);
		            c1FlexGridProtect.SetCellCheck(intCurrentRow, INT_POS_DELETE, CheckEnum.Unchecked);
		            c1FlexGridProtect.SetCellCheck(intCurrentRow, INT_POS_PRINT, CheckEnum.Unchecked);

		        }
		        else
		        {
		            // Set TSUnchecked for column Tree
		            c1FlexGridProtect.SetCellCheck(intCurrentRow, INT_POS_TREE, CheckEnum.TSUnchecked);
		        }
		        c1FlexGridProtect[intCurrentRow, INT_POS_PERMISSION] = 0;
		        // Recursion build child node
		        LoadTreeProtect(ref c1FlexGridProtect, voMenu.Shortcut, pintLevel + 1);
		        if (objNode.Level == 0)
		        {
		            objNode.Collapsed = true;
		        }
		    }
		}

	    //**************************************************************************
		/// <summary>
		/// Load Role into List, get data from sys_Role table and load
		///	into c1FlexGridRole control. This function will be executed 
		///	only one time when the form is loaded.
		/// </summary>
		/// <Author> Son HT, Dec 30, 2004</Author>
		private void LoadRole()
		{
            // Build the grid
            c1FlexGridRole.DataSource = dstProtection.Tables[Sys_RoleTable.TABLE_NAME];

            // Setting for First Column and First Row
            c1FlexGridRole.Cols[0].Width = c1FlexGridRole.Rows[0].HeightDisplay;
            // hidden RoleID column
            c1FlexGridRole.Cols[INT_START].Visible = false;
            c1FlexGridRole.Sort(SortFlags.Ascending, c1FlexGridRole.Cols[Sys_RoleTable.NAME_FLD].Index);	
		}

		//**************************************************************************              		
		/// <summary>
		/// Set width column, column number, set styte for c1FlexGridProtect
		///	this function will be executed only one time when the form is loaded. 
		/// </summary>
		/// <Author> Son HT, Dec 30, 2004</Author>
		private void SetProtectFormat()
		{
            // Set Tree column
            c1FlexGridProtect.Tree.Column = INT_POS_TREE;

            //Begin edit by duongna
            //Follow error 1994
            // Set width columns
            c1FlexGridProtect.Cols[0].Width = COLUMN_WIDTH_BASE;
            c1FlexGridProtect.Cols[INT_POS_TREE].Width = COLUMN_WIDTH_BASE * 10;
            c1FlexGridProtect.Cols[INT_POS_VIEW].Width = COLUMN_WIDTH_BASE;
            c1FlexGridProtect.Cols[INT_POS_ADD].Width = COLUMN_WIDTH_BASE;
            c1FlexGridProtect.Cols[INT_POS_DELETE].Width = COLUMN_WIDTH_BASE;
            c1FlexGridProtect.Cols[INT_POS_PRINT].Width = COLUMN_WIDTH_BASE;
            //End edit by duongna

            // Center alignment
            c1FlexGridProtect.Cols[INT_POS_ALL].ImageAlign =
            c1FlexGridProtect.Cols[INT_POS_VIEW].ImageAlign =
            c1FlexGridProtect.Cols[INT_POS_ADD].ImageAlign =
            c1FlexGridProtect.Cols[INT_POS_EDIT].ImageAlign =
            c1FlexGridProtect.Cols[INT_POS_DELETE].ImageAlign =
            c1FlexGridProtect.Cols[INT_POS_PRINT].ImageAlign = ImageAlignEnum.CenterCenter;	
		}
		
		//**************************************************************************
		/// <summary>
		/// Store all change of Role-Protection into sys_ProtectObj table.
		///	This function will be executed when user change selected row in 
		///	c1FlexGridRole or user click Close button on the form.
		/// </summary>
		/// <Author> Son HT, Dec 30, 2004</Author>
		private void StoreDatabase()
		{
            CommonBO boCommon = new CommonBO();
            boCommon.UpdateRight(dstProtection);
		}
		
		
		//**************************************************************************
		/// <summary>
		/// Store permission to dataset
		/// </summary>
		/// <param name="pvoSysRight"></param>
		/// <Author> Son HT, Dec 30, 2004</Author>
		private void StoreToDataSet(Sys_RightVO pvoSysRight)
		{
            // select a record from data table
            DataRow[] objRows = dstProtection.Tables[Sys_RightTable.TABLE_NAME].Select("(" +
                Sys_RightTable.MENU_ENTRYID_FLD + "=" + pvoSysRight.Menu_EntryID.ToString() + ") AND (" +
                Sys_RightTable.ROLEID_FLD + "=" + pvoSysRight.RoleID.ToString() + ")");
            // if 1 record is existed in database
            if (objRows.Length > 0)
            {
                objRows[0][Sys_RightTable.PERMISSION_FLD] = pvoSysRight.Permission;
            }
            else // add new record
            {
                DataRow objRow = dstProtection.Tables[Sys_RightTable.TABLE_NAME].NewRow();
                objRow[Sys_RightTable.MENU_ENTRYID_FLD] = pvoSysRight.Menu_EntryID;
                objRow[Sys_RightTable.ROLEID_FLD] = pvoSysRight.RoleID;
                objRow[Sys_RightTable.PERMISSION_FLD] = pvoSysRight.Permission;
                if (pvoSysRight.Permission > 0)
                    dstProtection.Tables[Sys_RightTable.TABLE_NAME].Rows.Add(objRow);
            }
		}

		//**************************************************************************		
		/// <summary>
		/// Set checking for tree protection. This function wil be executed
		///	when user click on Protection tree column in c1FlexGridProtection
		///	(c1FlexGridProtect_Click will executed this function)
		/// </summary>
		/// <param name="pobjFlex">ref C1.Win.C1FlexGrid.C1FlexGrid objFlex</param>
		/// <param name="pintRow">the selected row.</param>
		/// <Author> Son HT, Dec 30, 2004</Author>
		private void CheckTreeColumn(ref C1FlexGrid pobjFlex, int pintRow)
		{
            Node objNode = pobjFlex.Rows[pintRow].Node;
            // if Node of RowSel is Leaf node then Set properties for next columns
            if (int.Parse(c1FlexGridProtect.GetDataDisplay(pintRow, INT_POS_PARENT_CHILD)) == (int)MenuParentChildEnum.LeafMenu)
            {
                int intOldPermission = int.Parse(c1FlexGridProtect.GetDataDisplay(pintRow, INT_POS_PERMISSION));
                // Setting checking for All column
                // if current node is Checked - Set Checked all column
                if (pobjFlex.GetCellCheck(pintRow, INT_POS_TREE) == CheckEnum.Checked)
                {
                    pobjFlex.SetCellCheck(pintRow, INT_POS_ALL, CheckEnum.TSChecked);
                    SetCheckedColumn(ref pobjFlex, pintRow);
                    // Set Permission value
                    pobjFlex.SetData(pintRow, INT_POS_PERMISSION, ACTION_ALL);
                }
                // if current node is Checked - Set Unchecked all column
                else if (pobjFlex.GetCellCheck(pintRow, INT_POS_TREE) == CheckEnum.Unchecked)
                {
                    // Set TSUnchecked for all col
                    pobjFlex.SetCellCheck(pintRow, INT_POS_ALL, CheckEnum.TSUnchecked);
                    SetUncheckedColumn(ref pobjFlex, pintRow);
                    // Set Permission value
                    pobjFlex.SetData(pintRow, INT_POS_PERMISSION, ACTION_NONE);
                }
                int intNewPermission = int.Parse(c1FlexGridProtect.GetDataDisplay(pintRow, INT_POS_PERMISSION));
                if (intNewPermission != intOldPermission) blnIsModified = true;
                // Set checking for parent node
                SetCheckForParent(ref pobjFlex, ref objNode);
            }
            // Set properties for Parent Node or Root and Special node on the Tree
            else
            {
                int intOldPermission = int.Parse(c1FlexGridProtect.GetDataDisplay(pintRow, INT_POS_PERMISSION));
                if ((pobjFlex.GetCellCheck(pintRow, INT_POS_TREE) == CheckEnum.TSGrayed)
                    || (pobjFlex.GetCellCheck(pintRow, INT_POS_TREE) == CheckEnum.TSChecked))
                {
                    pobjFlex.SetCellCheck(pintRow, INT_POS_TREE, CheckEnum.TSChecked);
                    c1FlexGridProtect.SetData(pintRow, INT_POS_PERMISSION, ACTION_VIEW);
                }
                else if (pobjFlex.GetCellCheck(pintRow, INT_POS_TREE) == CheckEnum.TSUnchecked)
                {
                    c1FlexGridProtect.SetData(pintRow, INT_POS_PERMISSION, ACTION_NONE);
                }
                // with node has no children
                if (pobjFlex.GetCellCheck(pintRow, INT_POS_TREE) == CheckEnum.Checked)
                {
                    c1FlexGridProtect.SetData(pintRow, INT_POS_PERMISSION, ACTION_VIEW);
                }
                else if (pobjFlex.GetCellCheck(pintRow, INT_POS_TREE) == CheckEnum.Unchecked)
                {
                    c1FlexGridProtect.SetData(pintRow, INT_POS_PERMISSION, ACTION_NONE);
                }
                int intNewPermission = int.Parse(c1FlexGridProtect.GetDataDisplay(pintRow, INT_POS_PERMISSION));
                if (intNewPermission != intOldPermission)
                {
                    blnIsModified = true;
                }
                // Set checking for parent node
                SetCheckForParent(ref pobjFlex, ref objNode);
                if (objNode.Children > 0)
                {
                    SetCheckForChild(ref pobjFlex, ref objNode);
                }
            }	
		}
		
		//**************************************************************************		
		/// <summary>
		/// Set checking for child node when user click a parent node on the tree.
		///	This function set all child node is checked,unchecked,TSGrayed, TSChecked or TSUnchecked.
		///	This function will be execute by CheckTreeColumn function
		/// </summary>
		/// <param name="objFlex">pointer to c1FlexGridProtection control</param>
		/// <param name="pNode">current seleted node (user click on this node)</param>
		/// <Author> Son HT, Dec 30, 2004 </Author>
		private void SetCheckForChild(ref C1FlexGrid objFlex,ref Node pNode)
		{
            Node objFirstChild = pNode.GetNode(NodeTypeEnum.FirstChild);
            // Get check enum of current node
            CheckEnum chkParentEnum = objFlex.GetCellCheck(pNode.Row.Index, INT_POS_TREE);
            while (objFirstChild != null)
            {
                CheckEnum chkEnum = chkParentEnum;
                if (chkParentEnum != CheckEnum.TSGrayed)
                {
                    // if child is not a leaf node
                    bool blnUnchecked = (objFlex.GetCellCheck(objFirstChild.Row.Index, INT_POS_TREE) == CheckEnum.Unchecked);
                    bool blnChecked = (objFlex.GetCellCheck(objFirstChild.Row.Index, INT_POS_TREE) == CheckEnum.Checked);
                    if ((blnUnchecked) || (blnChecked))
                    {
                        chkEnum = chkParentEnum == CheckEnum.TSChecked ? CheckEnum.Checked : CheckEnum.Unchecked;
                    }
                    // Set check for child
                    objFlex.SetCellCheck(objFirstChild.Row.Index, INT_POS_TREE, chkEnum);
                    objFlex.SetData(objFirstChild.Row.Index, INT_POS_PERMISSION,
                                    objFlex.GetCellCheck(objFirstChild.Row.Index, INT_POS_TREE) == CheckEnum.TSUnchecked
                                        ? ACTION_NONE
                                        : ACTION_VIEW);
                    // Set check for all columns
                    if (chkEnum == CheckEnum.Checked)
                    {
                        // if objFirstChild is leaf
                        if (int.Parse(objFlex.GetData(objFirstChild.Row.Index, INT_POS_PARENT_CHILD).ToString()) == (int)MenuParentChildEnum.LeafMenu)
                        {
                            SetCheckedColumn(ref objFlex, objFirstChild.Row.Index);
                            objFlex.SetCellCheck(objFirstChild.Row.Index, INT_POS_ALL, CheckEnum.Checked);
                            c1FlexGridProtect.SetData(objFirstChild.Row.Index, INT_POS_PERMISSION, ACTION_ALL);
                            blnIsModified = true;
                        }
                    }
                    else if (chkEnum == CheckEnum.Unchecked)
                    {
                        if (int.Parse(objFlex.GetData(objFirstChild.Row.Index, INT_POS_PARENT_CHILD).ToString()) == (int)MenuParentChildEnum.LeafMenu)
                        {
                            SetUncheckedColumn(ref objFlex, objFirstChild.Row.Index);
                            objFlex.SetCellCheck(objFirstChild.Row.Index, INT_POS_ALL, CheckEnum.TSUnchecked);
                            c1FlexGridProtect.SetData(objFirstChild.Row.Index, INT_POS_PERMISSION, ACTION_NONE);
                            blnIsModified = true;
                        }
                    }
                    // Rescursion
                    SetCheckForChild(ref objFlex, ref objFirstChild);
                }
                objFirstChild = objFirstChild.GetNode(NodeTypeEnum.NextSibling);
            }	
		}

		
		//**************************************************************************		
		/// <summary>
		/// Set checking for parent node when user click a child node on the tree.
		///	This function set all parent node of pNode(selected node) to be Checked,Unchecked,...
		///	This function will be executed by CheckTreeColumn function
		/// </summary>
		/// <param name="pobjFlex">pointer to c1FlexGridProtection control</param>
		/// <param name="pobjNode">current seleted node (user click on this node)</param>
		/// <Author> Son HT, Dec 30, 2004</Author>
		private void SetCheckForParent(ref C1FlexGrid pobjFlex,ref Node pobjNode)
		{
            // if current node is first node
            Node objFirst = pobjNode.GetNode(NodeTypeEnum.FirstSibling) ?? pobjNode;
		    // Get parent node
            Node objParent = pobjNode.GetNode(NodeTypeEnum.Parent);
            bool blnIsChecked = true;
            bool blnIsUnchecked = true;
            // Scan all node on the tree
            while (objFirst != null)
            {
                // get condition
                bool blnTSUnchecked = (pobjFlex.GetCellCheck(objFirst.Row.Index, INT_POS_TREE) == CheckEnum.TSUnchecked);
                bool blnUnchecked = (pobjFlex.GetCellCheck(objFirst.Row.Index, INT_POS_TREE) == CheckEnum.Unchecked);
                bool blnTSChecked = (pobjFlex.GetCellCheck(objFirst.Row.Index, INT_POS_TREE) == CheckEnum.TSChecked);
                bool blnTSGrayed = (pobjFlex.GetCellCheck(objFirst.Row.Index, INT_POS_TREE) == CheckEnum.TSGrayed);
                bool blnChecked = (pobjFlex.GetCellCheck(objFirst.Row.Index, INT_POS_TREE) == CheckEnum.Checked);
                // if exit a node uncheck, set blnCheck = false 
                if ((blnTSUnchecked) || (blnTSGrayed) || (blnUnchecked))
                {
                    blnIsChecked = false;
                }
                // if exit a node checked, set blnUncheck = false 
                if ((blnTSChecked) || (blnTSGrayed) || (blnChecked))
                {
                    blnIsUnchecked = false;
                }
                // Move to next node
                objFirst = objFirst.GetNode(NodeTypeEnum.NextSibling);
            }
            // Setting for parent node
            if (objParent != null)
            {
                // if all child item is checked
                if (blnIsChecked)
                {
                    // Set checked for parent
                    pobjFlex.SetCellCheck(objParent.Row.Index, INT_POS_TREE, CheckEnum.TSChecked);
                    pobjFlex.SetData(objParent.Row.Index, INT_POS_PERMISSION, ACTION_VIEW);
                }
                else if (blnIsUnchecked) // if all child item is unchecked
                {
                    // Set Unchecked for parent
                    pobjFlex.SetCellCheck(objParent.Row.Index, INT_POS_TREE, CheckEnum.TSUnchecked);
                    pobjFlex.SetData(objParent.Row.Index, INT_POS_PERMISSION, ACTION_NONE);
                }
                else // if some child item is checked and some is unchecked 
                {
                    // Set TSGrayed for parent
                    pobjFlex.SetCellCheck(objParent.Row.Index, INT_POS_TREE, CheckEnum.TSGrayed);
                    pobjFlex.SetData(objParent.Row.Index, INT_POS_PERMISSION, ACTION_VIEW);
                }
                // rescursion
                SetCheckForParent(ref pobjFlex, ref objParent);
            }	
		}		           
		
		//**************************************************************************
		/// <summary>
		/// Set checking for the column All. When user click on the column All.
		///	All properties will be setted, and the checkbox in the tree is setted too.
		///	This function will be execute when user click on the c1FlexGridProtection control
		/// </summary>
		/// <param name="pobjFlex"> </param>
		/// <param name="pintRow"> </param>
		/// <Author> Son HT, Dec 30, 2004</Author>
		private void CheckAllColumn(ref C1FlexGrid pobjFlex, int pintRow)
		{
            // Select Checked all
            if ((pobjFlex.GetCellCheck(pintRow, INT_POS_ALL) == CheckEnum.TSChecked) ||
                (pobjFlex.GetCellCheck(pintRow, INT_POS_ALL) == CheckEnum.TSGrayed))
            {
                if (pobjFlex.GetCellCheck(pintRow, INT_POS_ALL) == CheckEnum.TSGrayed)
                    pobjFlex.SetCellCheck(pintRow, INT_POS_ALL, CheckEnum.TSChecked);
                pobjFlex.SetCellCheck(pintRow, INT_POS_TREE, CheckEnum.Checked);
                SetCheckedColumn(ref pobjFlex, pintRow);
                c1FlexGridProtect.SetData(pintRow, INT_POS_PERMISSION, ACTION_ALL);
                blnIsModified = true;
            }
            // Select UnChecked all
            else if (pobjFlex.GetCellCheck(pintRow, INT_POS_ALL) == CheckEnum.TSUnchecked)
            {
                pobjFlex.SetCellCheck(pintRow, INT_POS_TREE, CheckEnum.Unchecked);
                SetUncheckedColumn(ref pobjFlex, pintRow);
                c1FlexGridProtect.SetData(pintRow, INT_POS_PERMISSION, ACTION_NONE);
                blnIsModified = true;
            }
            // Set checking for parent node
            Node objNode = pobjFlex.Rows[pintRow].Node;
            SetCheckForParent(ref pobjFlex, ref objNode);		
		}		
		
		//**************************************************************************
		/// <summary>
		/// Set checking for all columns in grid and set permission for object
		/// </summary>
		/// <param name="pobjFlex"></param>
		/// <param name="pintRow"></param>
		/// <Author> Son HT, Dec 30, 2004</Author>
		private void CheckPropertiesColumn(ref C1FlexGrid pobjFlex,int pintRow)
		{
            // Get action of user, assign action to intAction variable
            int intAction = 0;		// get Action value
            if (pobjFlex.ColSel == INT_POS_VIEW) intAction = ACTION_VIEW;
            else if (pobjFlex.ColSel == INT_POS_ADD) intAction = ACTION_ADD;
            else if (pobjFlex.ColSel == INT_POS_EDIT) intAction = ACTION_EDIT;
            else if (pobjFlex.ColSel == INT_POS_DELETE) intAction = ACTION_DELETE;
            else if (pobjFlex.ColSel == INT_POS_PRINT) intAction = ACTION_PRINT;

            // if selected cell change from Unchecked to Checked then add permission on this item
            if (pobjFlex.GetCellCheck(pintRow, pobjFlex.ColSel) == CheckEnum.Checked)
            {
                int intPermission = int.Parse(c1FlexGridProtect.GetDataDisplay(pintRow, INT_POS_PERMISSION).ToString());
                // If the object has had an Action in permission then add right to permission
                if ((intPermission & intAction) != intAction)
                {
                    // Add action when user change from Uncheck -> Check
                    intPermission = intPermission + intAction;
                    c1FlexGridProtect.SetData(pintRow, INT_POS_PERMISSION, intPermission);
                    blnIsModified = true;
                }
                // auto set checked for View column
                if ((intPermission & ACTION_VIEW) != ACTION_VIEW)
                {
                    // Add action when user change from Uncheck -> Check
                    intPermission = intPermission + ACTION_VIEW;
                    c1FlexGridProtect.SetData(pintRow, INT_POS_PERMISSION, intPermission);
                    c1FlexGridProtect.SetCellCheck(pintRow, INT_POS_VIEW, CheckEnum.Checked);
                    blnIsModified = true;
                }
            }
            // if current cell change from Checked to Unchecked then move permission
            else if (pobjFlex.GetCellCheck(pintRow, pobjFlex.ColSel) == CheckEnum.Unchecked)
            {
                // Remove action when user change from Check -> Uncheck
                int intPermission = int.Parse(c1FlexGridProtect.GetDataDisplay(pintRow, INT_POS_PERMISSION).ToString());

                // if column View change from Checked -> Uncheck and other column still checked then warning message
                if ((pobjFlex.ColSel == INT_POS_VIEW) && (intPermission > ACTION_VIEW) && ((intPermission & ACTION_VIEW) == ACTION_VIEW))
                {
                    // Warning: You must set uncheck for all other columns before set uncheck for this column
                    PCSMessageBox.Show(ErrorCode.MESSAGE_UNCHECK_OTHER_COLUMNS_BEFORE_VIEW, MessageBoxIcon.Warning);
                    pobjFlex.SetCellCheck(pintRow, pobjFlex.ColSel, CheckEnum.Checked);
                    return;
                }

                if ((intPermission & intAction) == intAction)
                {
                    // Add action when user change from Uncheck -> Check
                    intPermission = intPermission - intAction;
                    c1FlexGridProtect.SetData(pintRow, INT_POS_PERMISSION, intPermission);
                    blnIsModified = true;
                }
            }

            // Set TSChecked for column All 
            if (int.Parse(c1FlexGridProtect.GetDataDisplay(pintRow, INT_POS_PERMISSION).ToString()) == ACTION_ALL)
            {
                pobjFlex.SetCellCheck(pintRow, INT_POS_ALL, CheckEnum.TSChecked);
            }
            // Set Unchecked
            else if (int.Parse(c1FlexGridProtect.GetDataDisplay(pintRow, INT_POS_PERMISSION).ToString()) == ACTION_NONE)
            {
                pobjFlex.SetCellCheck(pintRow, INT_POS_ALL, CheckEnum.TSUnchecked);
            }
            // Set Grayed
            else
            {
                pobjFlex.SetCellCheck(pintRow, INT_POS_ALL, CheckEnum.TSGrayed);
            }
            // Set Checking for column Tree 
            if (int.Parse(c1FlexGridProtect.GetDataDisplay(pintRow, INT_POS_PERMISSION).ToString()) == ACTION_NONE)
            {
                pobjFlex.SetCellCheck(pintRow, INT_POS_TREE, CheckEnum.Unchecked);
            }
            else
            {
                pobjFlex.SetCellCheck(pintRow, INT_POS_TREE, CheckEnum.Checked);
            }
            // Set current status checkbox
            Node objNode = pobjFlex.Rows[pintRow].Node;
            SetCheckForParent(ref pobjFlex, ref objNode);	
		}
		
		//**************************************************************************
		/// <summary>
		/// Set Unchecked for properties column, this function set all properties
		///	column is Unchecked, more function call this fucntion, it's help us 
		///	easy to read the code.
		/// </summary>
		/// <param name="pobjFlex"></param>
		/// <param name="pintRow"></param>
		/// <Author> Son HT, Dec 30, 2004</Author>
		private void SetUncheckedColumn(ref C1FlexGrid pobjFlex,int pintRow)
		{
			pobjFlex.SetCellCheck(pintRow,INT_POS_VIEW,CheckEnum.Unchecked);
			pobjFlex.SetCellCheck(pintRow,INT_POS_ADD,CheckEnum.Unchecked);
			pobjFlex.SetCellCheck(pintRow,INT_POS_EDIT,CheckEnum.Unchecked);
			pobjFlex.SetCellCheck(pintRow,INT_POS_DELETE,CheckEnum.Unchecked);			
			pobjFlex.SetCellCheck(pintRow,INT_POS_PRINT,CheckEnum.Unchecked);			
		}

		//**************************************************************************		
		/// <summary>
		/// Set checked for properties column, this function set all properties
		///	column is checked, more function call this fucntion, it's help us 
		///	easy to read the code.
		/// </summary>
		/// <param name="pobjFlex"></param>
		/// <param name="pintRow"></param>
		/// <Author> Son HT, Dec 30, 2004</Author>
		private void SetCheckedColumn(ref C1FlexGrid pobjFlex,int pintRow)
		{			
			pobjFlex.SetCellCheck(pintRow,INT_POS_VIEW,CheckEnum.Checked);
			pobjFlex.SetCellCheck(pintRow,INT_POS_ADD,CheckEnum.Checked);
			pobjFlex.SetCellCheck(pintRow,INT_POS_EDIT,CheckEnum.Checked);
			pobjFlex.SetCellCheck(pintRow,INT_POS_DELETE,CheckEnum.Checked);			
			pobjFlex.SetCellCheck(pintRow,INT_POS_PRINT,CheckEnum.Checked);			
		}
		
		//**************************************************************************
		/// <summary>
		/// This method uses to Store data
		/// </summary>
		/// <Author> Son HT, Jan 20, 2005</Author>
		private void CleanAndStore()
		{
            for (int i = INT_START; i < c1FlexGridProtect.Rows.Count; i++)
            {
                // is leaf
                if (int.Parse(c1FlexGridProtect.GetDataDisplay(i, INT_POS_PARENT_CHILD).ToString()) == (int)MenuParentChildEnum.LeafMenu)
                {
                    // Store to dataset
                    Sys_RightVO voSysRight = new Sys_RightVO();
                    voSysRight.Menu_EntryID = int.Parse(c1FlexGridProtect.GetDataDisplay(i, INT_POS_MENU_ENTRY).ToString());
                    voSysRight.RoleID = int.Parse(c1FlexGridRole.GetDataDisplay(c1FlexGridRole.RowSel, INT_START).ToString());
                    voSysRight.Permission = int.Parse(c1FlexGridProtect.GetDataDisplay(i, INT_POS_PERMISSION).ToString());
                    StoreToDataSet(voSysRight);
                    SetUncheckedColumn(ref c1FlexGridProtect, i);
                    c1FlexGridProtect.SetCellCheck(i, INT_POS_ALL, CheckEnum.TSUnchecked);
                    c1FlexGridProtect.SetCellCheck(i, INT_POS_TREE, CheckEnum.Unchecked);
                    c1FlexGridProtect[i, INT_POS_PERMISSION] = ACTION_NONE;
                }
                else
                {
                    // Store to dataset
                    Sys_RightVO voSysRight = new Sys_RightVO();
                    voSysRight.Menu_EntryID = int.Parse(c1FlexGridProtect.GetDataDisplay(i, INT_POS_MENU_ENTRY).ToString());
                    voSysRight.RoleID = int.Parse(c1FlexGridRole.GetDataDisplay(c1FlexGridRole.RowSel, INT_START).ToString());
                    voSysRight.Permission = int.Parse(c1FlexGridProtect.GetDataDisplay(i, INT_POS_PERMISSION).ToString());
                    StoreToDataSet(voSysRight);
                    c1FlexGridProtect.SetCellCheck(i, INT_POS_TREE, CheckEnum.TSUnchecked);
                    c1FlexGridProtect[i, INT_POS_PERMISSION] = ACTION_NONE;
                }
            }
		}
		
		//**************************************************************************		
		/// <summary>
		/// This method uses to 
		/// </summary>
		/// <param name="pintRoleID">Role ID</param>
		/// <Author> Son HT, Dec 30, 2004</Author>
		private void LoadCheckingOfProtect(int pintRoleID)
		{
            int intRow = 1;
            string menuEntryId = string.Empty;
            if (intRow < c1FlexGridProtect.Rows.Count)
            {
                menuEntryId = c1FlexGridProtect.GetDataDisplay(intRow, INT_POS_MENU_ENTRY).Trim();
            }
            int intPermission = 0;
            // Scan all row in protected object
            while (menuEntryId != string.Empty)
            {
                // set checked for leaf
                if (int.Parse(c1FlexGridProtect.GetDataDisplay(intRow, INT_POS_PARENT_CHILD)) == (int)MenuParentChildEnum.LeafMenu)
                {
                    DataRow[] objRows = dstProtection.Tables[Sys_RightTable.TABLE_NAME].Select("(" + Sys_RightTable.MENU_ENTRYID_FLD + "="
                        + menuEntryId + ") AND (" + Sys_RightTable.ROLEID_FLD + "=" + pintRoleID + ")");
                    if (objRows.Length > 0)
                    {
                        if (objRows[0][Sys_RightTable.PERMISSION_FLD].ToString().Length > 0)
                            intPermission = int.Parse(objRows[0][Sys_RightTable.PERMISSION_FLD].ToString());
                        // Set Permission for leaf
                        c1FlexGridProtect.SetCellCheck(intRow, INT_POS_TREE, intPermission > ACTION_NONE
                                                           ? CheckEnum.Checked
                                                           : CheckEnum.Unchecked);
                        // Column 'All'
                        switch (intPermission)
                        {
                            case ACTION_ALL:
                                c1FlexGridProtect.SetCellCheck(intRow, INT_POS_ALL, CheckEnum.TSChecked);
                                break;
                            case ACTION_NONE:
                                c1FlexGridProtect.SetCellCheck(intRow, INT_POS_ALL, CheckEnum.TSUnchecked);
                                break;
                            default:
                                c1FlexGridProtect.SetCellCheck(intRow, INT_POS_ALL, CheckEnum.TSGrayed);
                                break;
                        }
                        // other Columns 
                        c1FlexGridProtect.SetCellCheck(intRow, INT_POS_VIEW,
                                                       ((intPermission & ACTION_VIEW) == ACTION_VIEW ? CheckEnum.Checked : CheckEnum.Unchecked));
                        c1FlexGridProtect.SetCellCheck(intRow, INT_POS_ADD,
                                                       ((intPermission & ACTION_ADD) == ACTION_ADD ? CheckEnum.Checked : CheckEnum.Unchecked));
                        c1FlexGridProtect.SetCellCheck(intRow, INT_POS_EDIT,
                                                       ((intPermission & ACTION_EDIT) == ACTION_EDIT ? CheckEnum.Checked : CheckEnum.Unchecked));
                        c1FlexGridProtect.SetCellCheck(intRow, INT_POS_DELETE,
                                                       ((intPermission & ACTION_DELETE) == ACTION_DELETE ? CheckEnum.Checked : CheckEnum.Unchecked));
                        c1FlexGridProtect.SetCellCheck(intRow, INT_POS_PRINT,
                                                       ((intPermission & ACTION_PRINT) == ACTION_PRINT ? CheckEnum.Checked : CheckEnum.Unchecked));
                        c1FlexGridProtect.SetData(intRow, INT_POS_PERMISSION, intPermission);

                        // Set checking for parent node
                        Node objNode = c1FlexGridProtect.Rows[intRow].Node;
                        SetCheckForParent(ref c1FlexGridProtect, ref objNode);
                    }
                }
                else // set permission for parent or root
                {
                    DataRow[] objRows = dstProtection.Tables[Sys_RightTable.TABLE_NAME].Select("(" + Sys_RightTable.MENU_ENTRYID_FLD + "="
                        + menuEntryId + ") AND (" + Sys_RightTable.ROLEID_FLD + "=" + pintRoleID.ToString() + ")");
                    if (objRows.Length > 0)
                    {
                        if (objRows[0][Sys_RightTable.PERMISSION_FLD].ToString().Length > 0)
                            intPermission = int.Parse(objRows[0][Sys_RightTable.PERMISSION_FLD].ToString());
                        c1FlexGridProtect.SetData(intRow, INT_POS_PERMISSION, intPermission);
                        // Set Permission for parent
                        if (int.Parse(c1FlexGridProtect.GetDataDisplay(intRow, INT_POS_PARENT_CHILD)) != (int)MenuParentChildEnum.LeafMenu)
                        {
                            c1FlexGridProtect.SetCellCheck(intRow, INT_POS_TREE, intPermission > ACTION_NONE
                                                               ? CheckEnum.Checked
                                                               : CheckEnum.Unchecked);
                        }

                        // Set checking for parent node
                        Node objNode = c1FlexGridProtect.Rows[intRow].Node;
                        SetCheckForParent(ref c1FlexGridProtect, ref objNode);
                    }
                }

                // next row in grid function
                intRow++;
                menuEntryId = intRow < c1FlexGridProtect.Rows.Count
                                  ? c1FlexGridProtect.GetDataDisplay(intRow, INT_POS_MENU_ENTRY).Trim()
                                  : string.Empty;
            }
		}

		//**************************************************************************		
		/// <summary>
		/// This method uses to 
		/// </summary>
		/// <param name="pstrTableName"> Table name</param>
		/// <returns></returns>
		/// <Author> Son HT, Dec 30, 2004</Author>
		private ArrayList TableToArray(string pstrTableName)
		{
			ArrayList arrObject = new ArrayList();
            if (pstrTableName.Equals(Sys_Menu_EntryTable.TABLE_NAME))
            {
                foreach (DataRow objRow in dstProtection.Tables[pstrTableName].Rows)
                {
                    Sys_Menu_EntryVO voMenu = new Sys_Menu_EntryVO();
                    voMenu.Parent_Shortcut = objRow[Sys_Menu_EntryTable.PARENT_SHORTCUT_FLD].ToString().Trim();
                    voMenu.Menu_EntryID = int.Parse(objRow[Sys_Menu_EntryTable.MENU_ENTRYID_FLD].ToString());
                    voMenu.Shortcut = objRow[Sys_Menu_EntryTable.SHORTCUT_FLD].ToString().Trim();
                    int butonCaption;
                    if (int.TryParse(objRow[Sys_Menu_EntryTable.BUTTON_CAPTION_FLD] as string, out butonCaption))
                    {
                        voMenu.Button_Caption = butonCaption;
                    }
                    voMenu.Text_CaptionDefault = objRow[Sys_Menu_EntryTable.TEXT_CAPTIONDEFAULT_FLD].ToString().Trim();
                    voMenu.Text_Caption_Vi_VN = objRow[Sys_Menu_EntryTable.PARENT_CHILD_FLD].ToString().Trim();
                    voMenu.Text_Caption_EN_US = objRow[Sys_Menu_EntryTable.TEXT_CAPTION_EN_US_FLD].ToString().Trim();
                    voMenu.Text_Caption_JA_JP = objRow[Sys_Menu_EntryTable.TEXT_CAPTION_JA_JP_FLD].ToString().Trim();
                    voMenu.Text_Caption_Language_Default = objRow[Sys_Menu_EntryTable.TEXT_CAPTION_LANGUAGE_DEFAULT_FLD].ToString().Trim();
                    int parentChild;
                    if (int.TryParse(objRow[Sys_Menu_EntryTable.PARENT_CHILD_FLD] as string, out parentChild))
                    {
                        voMenu.Parent_Child = parentChild;
                    }
                    voMenu.FormLoad = objRow[Sys_Menu_EntryTable.FORMLOAD_FLD].ToString().Trim();

                    int menuType;
                    if (int.TryParse(objRow[Sys_Menu_EntryTable.TYPE_FLD] as string, out menuType))
                    {
                        voMenu.Type = menuType;
                    }
                    arrObject.Add(voMenu);
                }
            }
			return arrObject;
		}

		#endregion

		#region Event Processing

		//**************************************************************************
		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <Author> Son HT, Dec 30, 2004</Author>
		private void GrantRoleProtect_Load(object sender, EventArgs e)
		{
			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.WaitCursor;

			#endregion Code Inserted Automatically

			
			const string METHOD_NAME = THIS + ".GrantRoleProtect_Load()";
			const string MAIN = "MAIN";
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
				
				// Load data from database
				CommonBO boCommon = new CommonBO();
				dstProtection = boCommon.GetAllProtection();
				dstProtection.Tables[Sys_RightTable.TABLE_NAME].Columns[Sys_RightTable.RIGHTID_FLD].AutoIncrement = true;

				// Load Role GridFlex table
			    if (dstProtection.Tables[Sys_RoleTable.TABLE_NAME].Rows.Count <= 0)
			    {
			        return;
			    }
                LoadRole();
			    // fill array from table
				arrSysMenuEntry = TableToArray(Sys_Menu_EntryTable.TABLE_NAME);
				// Set visible columns
				c1FlexGridProtect.Cols[INT_POS_MENU_ENTRY].Visible = false;
				c1FlexGridProtect.Cols[INT_POS_PARENT_CHILD].Visible = false;
				c1FlexGridProtect.Cols[INT_POS_PERMISSION].Visible = false;

				//Begin edit by duongna 11-10-2005
				//Follow error 1994
				c1FlexGridProtect.Cols[INT_POS_ALL].Visible = false;
				c1FlexGridProtect.Cols[INT_POS_VIEW].Visible = false;
				c1FlexGridProtect.Cols[INT_POS_ADD].Visible = false;
				c1FlexGridProtect.Cols[INT_POS_EDIT].Visible = false;
				c1FlexGridProtect.Cols[INT_POS_DELETE].Visible = false;
				c1FlexGridProtect.Cols[INT_POS_PRINT].Visible = false;
				//End edit by duongna 11-10-2005

				// Set table Protect format
				SetProtectFormat();
				// Current row that system create row when Load tree data
				intCurrentRow = 0;
				LoadTreeProtect(ref c1FlexGridProtect, MAIN, intCurrentRow);
				// load checking of tree
				if (c1FlexGridRole.Rows.Count > 0)
				{
					LoadCheckingOfProtect(int.Parse(c1FlexGridRole.GetDataDisplay(INT_START,INT_START).ToString()));
				}
				//set start col outside of tree col
				c1FlexGridProtect.Col = INT_POS_PRINT + 1;
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
		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <Author> Son HT, Dec 30, 2004</Author>
		private void c1FlexGridProtect_Click(object sender, EventArgs e)
		{
			// Get C1FlexGrid object
			C1FlexGrid objC1FlexGrid = (C1FlexGrid)sender;
			// Check row in range
			if(objC1FlexGrid.RowSel > 0)
			{
				// Check Tree columns 
				if(objC1FlexGrid.ColSel == INT_POS_TREE)
				{
					CheckTreeColumn(ref objC1FlexGrid, objC1FlexGrid.RowSel);
				}

				// If user clicks on checkbox of Leaf
				if (int.Parse(c1FlexGridProtect.GetDataDisplay(objC1FlexGrid.RowSel,INT_POS_PARENT_CHILD).ToString()) == (int)MenuParentChildEnum.LeafMenu) 
				{
					// if user click on All column
					if(objC1FlexGrid.ColSel == INT_POS_ALL)
					{
						// Set checking for All column
						CheckAllColumn(ref objC1FlexGrid, objC1FlexGrid.RowSel);
					}
					else
					{
						if(objC1FlexGrid.ColSel > INT_POS_ALL)
						{
							// Set checking for Read|Write|Execute|Delete
							CheckPropertiesColumn(ref objC1FlexGrid, objC1FlexGrid.RowSel);
						}
					}
				}
			}
		}
		
		//**************************************************************************
		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <Author> Son HT, Dec 30, 2004</Author>
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
		
		//**************************************************************************
		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <Author> Son HT, Dec 30, 2004</Author>
		private void c1FlexGridRole_BeforeRowColChange(object sender, RangeEventArgs e)
		{	
			const string METHOD_NAME = THIS + ".c1FlexGridRole_BeforeRowColChange()";
			try
			{
				// if user move to an other row
				if(e.OldRange.r1 != e.NewRange.r1)
				{
					// Clear checked and store permission to dataset
					CleanAndStore();
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
		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <Author> Son HT, Dec 30, 2004</Author>
		private void c1FlexGridRole_AfterRowColChange(object sender, RangeEventArgs e)
		{
			const string METHOD_NAME = THIS + ".c1FlexGridRole_AfterRowColChange()";
			try
			{
				// Get RoleID
				int intRowSel = c1FlexGridRole.RowSel;
				// if user change selected row
				if(e.OldRange.r1 != e.NewRange.r1)
				{
					// Check Empty row
					if (intRowSel > 0)
					{
						// Get data display on column 1 of Role
						string strRoleID = c1FlexGridRole.GetDataDisplay(c1FlexGridRole.RowSel,INT_START);
						// load checking of tree
						if(strRoleID.Length > 0)
						{
							LoadCheckingOfProtect(int.Parse(strRoleID));
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
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <Author> Son HT, Dec 30, 2004</Author>
		private void btnSave_Click(object sender, EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnSave_Click()";
			// if user doesn't change data on form
			if(!blnIsModified) 
			{
				// Code Inserted Automatically
				#region Code Inserted Automatically
				this.Cursor = Cursors.Default;
				#endregion Code Inserted Automatically																						
				return;
			}
			try
			{
				for (int i = INT_START; i < c1FlexGridProtect.Rows.Count; i++)
				{
                    // Store to dataset
                    Sys_RightVO voSysRight = new Sys_RightVO();
                    voSysRight.Menu_EntryID = int.Parse(c1FlexGridProtect.GetDataDisplay(i, INT_POS_MENU_ENTRY));
                    voSysRight.RoleID = int.Parse(c1FlexGridRole.GetDataDisplay(c1FlexGridRole.RowSel, INT_START));
                    voSysRight.Permission = int.Parse(c1FlexGridProtect.GetDataDisplay(i, INT_POS_PERMISSION));
                    StoreToDataSet(voSysRight);
				}
				StoreDatabase();
				blnIsModified = false;
				PCSMessageBox.Show(ErrorCode.MESSAGE_AFTER_SAVE_DATA,MessageBoxButtons.OK);
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
		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <Author> Son HT, Dec 30, 2004</Author>
		private void GrantRoleProtect_Closing(object sender, CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".GrantRoleProtect_Closing()";
			// if user doesn't change data on form
			if(!blnIsModified) return;
			try
			{
				DialogResult enumDialog = PCSMessageBox.Show(ErrorCode.MESSAGE_QUESTION_STORE_INTO_DATABASE,MessageBoxButtons.YesNoCancel,MessageBoxIcon.Question);
				if(enumDialog == DialogResult.Yes)
				{
					// StoreDatabase();
					btnSave_Click(sender,e);
				}
				else if(enumDialog == DialogResult.Cancel)
				{
					e.Cancel = true;
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
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <Author> Son HT, Dec 30, 2004</Author>
		private void c1FlexGridProtect_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Space)
			{
				c1FlexGridProtect_Click(c1FlexGridProtect,null);
			}
		}

		#endregion
	}
}