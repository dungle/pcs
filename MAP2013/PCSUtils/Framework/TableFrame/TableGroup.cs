using System;
using System.Windows.Forms;
using PCSComUtils.Common;
using PCSComUtils.Framework.TableFrame.BO;
using PCSComUtils.Framework.TableFrame.DS;
using PCSComUtils.PCSExc;
using PCSUtils.Log;
using PCSUtils.Utils;

namespace PCSUtils.Framework.TableFrame
{
	/// <summary>
	/// Summary description for TableGroup.
	/// </summary>
	public class TableGroup : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button btnSave;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnHelp;
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.TextBox txtGroupName;
		private System.Windows.Forms.TextBox txtGroupCode;

		private EnumAction mEnumType;
		private sys_TableGroupVO voTableGroup = new sys_TableGroupVO();

		private int intGroupID;
		private int intGroupOrder;
		private const string COPY_OF = "copy of ";
		private const string THIS = "PCSUtils.Framework.TableFrame.TableGroup";
		private bool blnSaveStatus;

		public EnumAction EnumType
		{
			set { mEnumType = value; }
			get { return mEnumType; }
		}

		public sys_TableGroupVO TableGroupVO
		{
			set { voTableGroup = value; }
			get { return voTableGroup; }
		}
		public int TableGroupID
		{
			get
			{
				return intGroupID;
			}
			set
			{
				intGroupID = value;
			}
		}

		//------------------
		public TableGroup()
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(TableGroup));
			this.btnHelp = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnSave = new System.Windows.Forms.Button();
			this.txtGroupName = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.txtGroupCode = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.SuspendLayout();
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
			// txtGroupName
			// 
			this.txtGroupName.AccessibleDescription = resources.GetString("txtGroupName.AccessibleDescription");
			this.txtGroupName.AccessibleName = resources.GetString("txtGroupName.AccessibleName");
			this.txtGroupName.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("txtGroupName.Anchor")));
			this.txtGroupName.AutoSize = ((bool)(resources.GetObject("txtGroupName.AutoSize")));
			this.txtGroupName.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("txtGroupName.BackgroundImage")));
			this.txtGroupName.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("txtGroupName.Dock")));
			this.txtGroupName.Enabled = ((bool)(resources.GetObject("txtGroupName.Enabled")));
			this.txtGroupName.Font = ((System.Drawing.Font)(resources.GetObject("txtGroupName.Font")));
			this.txtGroupName.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("txtGroupName.ImeMode")));
			this.txtGroupName.Location = ((System.Drawing.Point)(resources.GetObject("txtGroupName.Location")));
			this.txtGroupName.MaxLength = ((int)(resources.GetObject("txtGroupName.MaxLength")));
			this.txtGroupName.Multiline = ((bool)(resources.GetObject("txtGroupName.Multiline")));
			this.txtGroupName.Name = "txtGroupName";
			this.txtGroupName.PasswordChar = ((char)(resources.GetObject("txtGroupName.PasswordChar")));
			this.txtGroupName.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("txtGroupName.RightToLeft")));
			this.txtGroupName.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("txtGroupName.ScrollBars")));
			this.txtGroupName.Size = ((System.Drawing.Size)(resources.GetObject("txtGroupName.Size")));
			this.txtGroupName.TabIndex = ((int)(resources.GetObject("txtGroupName.TabIndex")));
			this.txtGroupName.Text = resources.GetString("txtGroupName.Text");
			this.txtGroupName.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("txtGroupName.TextAlign")));
			this.txtGroupName.Visible = ((bool)(resources.GetObject("txtGroupName.Visible")));
			this.txtGroupName.WordWrap = ((bool)(resources.GetObject("txtGroupName.WordWrap")));
			this.txtGroupName.TextChanged += new System.EventHandler(this.txtGroupName_TextChanged);
			this.txtGroupName.Leave += new System.EventHandler(this.OnLeaveControl);
			this.txtGroupName.Enter += new System.EventHandler(this.OnEnterControl);
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
			this.txtGroupCode.TextChanged += new System.EventHandler(this.txtGroupCode_TextChanged);
			this.txtGroupCode.Leave += new System.EventHandler(this.OnLeaveControl);
			this.txtGroupCode.Enter += new System.EventHandler(this.OnEnterControl);
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
			// TableGroup
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
			this.Controls.Add(this.btnHelp);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnSave);
			this.Controls.Add(this.txtGroupName);
			this.Controls.Add(this.txtGroupCode);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Enabled = ((bool)(resources.GetObject("$this.Enabled")));
			this.Font = ((System.Drawing.Font)(resources.GetObject("$this.Font")));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("$this.ImeMode")));
			this.Location = ((System.Drawing.Point)(resources.GetObject("$this.Location")));
			this.MaximizeBox = false;
			this.MaximumSize = ((System.Drawing.Size)(resources.GetObject("$this.MaximumSize")));
			this.MinimumSize = ((System.Drawing.Size)(resources.GetObject("$this.MinimumSize")));
			this.Name = "TableGroup";
			this.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("$this.RightToLeft")));
			this.StartPosition = ((System.Windows.Forms.FormStartPosition)(resources.GetObject("$this.StartPosition")));
			this.Text = resources.GetString("$this.Text");
			this.Closing += new System.ComponentModel.CancelEventHandler(this.TableGroup_Closing);
			this.Load += new System.EventHandler(this.TableGroup_Load);
			this.ResumeLayout(false);

		}

		#endregion

		private void TableGroup_Load(object sender, System.EventArgs e)
		{
			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.WaitCursor;

			#endregion Code Inserted Automatically

			
			const string METHOD_NAME = THIS + ".TableGroup_Load()";
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

				// if form open to edit
				if (mEnumType == EnumAction.Edit)
				{
					intGroupID = voTableGroup.TableGroupID;
					txtGroupCode.Text = (voTableGroup.Code).Trim();
					txtGroupName.Text = (voTableGroup.TableGroupName).Trim();
					intGroupOrder = voTableGroup.GroupOrder;
					txtGroupCode.ReadOnly = true;
					txtGroupCode.Focus();
				}
					// if form open to copy
				else if (mEnumType == EnumAction.Copy)
				{
					txtGroupCode.Text = COPY_OF + voTableGroup.Code;
					txtGroupName.Text = COPY_OF + voTableGroup.TableGroupName;
					btnCancel.Enabled = false;
				}

				blnSaveStatus = true;
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


		private bool SaveGroup()
		{
			const string METHOD_NAME = THIS + ".btnSave_Click()";
			// check data in textboxes
			try
			{
				if (!CheckMandatory(txtGroupCode))
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID,MessageBoxIcon.Warning);
					txtGroupCode.Focus();
					return false;
				}
				if (!CheckMandatory(txtGroupName))
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID,MessageBoxIcon.Warning);
					txtGroupName.Focus();
					return false;
				}

				// in case add new row 
				if (mEnumType == EnumAction.Add)
				{
					// call Add() method to insert data
					sys_TableGroupVO voTableGroup = new sys_TableGroupVO();
					TableGroupBO boTableGroup = new TableGroupBO();
					voTableGroup.Code = txtGroupCode.Text.Trim();
					voTableGroup.TableGroupName = txtGroupName.Text.Trim();
					voTableGroup.GroupOrder = boTableGroup.ReturnGroupOrder() + 1;
					voTableGroup.TableGroupID = boTableGroup.AddGroupAndReturnMaxID(voTableGroup);
					intGroupID = voTableGroup.TableGroupID;
					this.voTableGroup = voTableGroup;
					this.Close();
					return true;
				}
					// in case edit row 
				else if (mEnumType == EnumAction.Edit)
				{
					// call Update() method to update data
					sys_TableGroupVO voTableGroup = new sys_TableGroupVO();
					TableGroupBO boTableGroup = new TableGroupBO();
					voTableGroup.TableGroupID = intGroupID;
					voTableGroup.Code = txtGroupCode.Text.Trim();
					voTableGroup.TableGroupName = txtGroupName.Text.Trim();
					voTableGroup.GroupOrder = intGroupOrder;
					boTableGroup.UpdateGroup(voTableGroup);
					this.voTableGroup = voTableGroup;
					this.Close();
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
					PCSMessageBox.Show(ErrorCode.DUPLICATE_TABLEGROUP, MessageBoxIcon.Error);
					txtGroupCode.Focus();					
				}

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
			//this.Close();
			return true;
		}
		private void btnSave_Click(object sender, System.EventArgs e)
		{
			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.WaitCursor;

			#endregion Code Inserted Automatically

			
			if (SaveGroup())
			{
				blnSaveStatus = true;
			}

			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.Default;

			#endregion Code Inserted Automatically

			
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

		//this method to check data in two texboxes code and name
		private bool CheckMandatory(Control pobjControl)
		{
			if (pobjControl.Text.Trim() == string.Empty)
			{
				return false;
			}
			return true;
		}

		private void txtGroupCode_TextChanged(object sender, System.EventArgs e)
		{
			blnSaveStatus = false;
		}

		private void txtGroupName_TextChanged(object sender, System.EventArgs e)
		{
			blnSaveStatus = false;
		}

		private void TableGroup_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if (!blnSaveStatus)
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
	}
}