using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using C1.Win.C1Input;
using C1.Win.C1List;
using PCSComUtils.Common;
using PCSComUtils.Framework.TableFrame.BO;
using PCSComUtils.Framework.TableFrame.DS;
using PCSComUtils.PCSExc;
using PCSUtils.Log;
using PCSUtils.Utils;

namespace PCSUtils.Framework.ReportFrame
{
	/// <summary>
	/// Summary description for ViewSingleRecord.
	/// </summary>
	public class ViewSingleRecord : Form
	{
		private GroupBox grbButtons;
		private Button btnAdd;
		private Button btnEdit;
		private Button btnSave;
		private Button btnDelete;
		private Button btnHelp;
		private Button btnClose;
		private Panel grbControls;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private Container components = null;
		private const string THIS = "PCSUtils.Framework.ReportFrame.ViewSingleRecord";

		private const string LABEL_PREFIX = "lbl";
		private const string TEXTBOX_PREFIX = "txt";
		private const string COMBO_PREFIX = "cbo";
		private const string CHECKBOX_PREFIX = "chk";
		private const string DATEPICKER_PREFIX = "dtm";
		private const int DEFAULT_WIDTH = 100;
		private const int DEFAULT_HEIGHT = 23;
		private const int CONTROL_X_LOCATION = 112;
		private const int Y_LOCATION = 20;
		private const int LABEL_X_LOCATION = 8;
		private ViewTableBO boViewTable = new ViewTableBO();

		private ArrayList mRecordFields;
		public ArrayList RecordFields
		{
			get { return mRecordFields; }
			set { mRecordFields = value; }
		}


		private string mSelectCommand;
		private string mUpdateCommand;
		
		public string SelectCommand
		{
			get { return mSelectCommand; }
			set { mSelectCommand = value; }
		}

		public string UpdateCommand
		{
			get { return mUpdateCommand; }
			set { mUpdateCommand = value; }
		}

		private bool mViewOnly;
		public bool ViewOnly
		{
			get { return mViewOnly; }
			set { mViewOnly = value; }
		}

		private DataRow mRecordData;
		public DataRow RecordData
		{
			get { return mRecordData; }
			set { mRecordData = value; }
		}


		private bool mDeleteRow = false;

		public bool DeleteRow
		{
			get { return mDeleteRow; }
		}

		private DataSet mTableData;
		public DataSet TableData
		{
			get { return mTableData; }
			set { mTableData = value; }
		}

		private EnumAction mFormAction;
		public EnumAction FormAction
		{
			get { return mFormAction; }
			set { mFormAction = value; }
		}

		
		public ViewSingleRecord()
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(ViewSingleRecord));
			this.grbButtons = new System.Windows.Forms.GroupBox();
			this.btnClose = new System.Windows.Forms.Button();
			this.btnHelp = new System.Windows.Forms.Button();
			this.btnDelete = new System.Windows.Forms.Button();
			this.btnSave = new System.Windows.Forms.Button();
			this.btnEdit = new System.Windows.Forms.Button();
			this.btnAdd = new System.Windows.Forms.Button();
			this.grbControls = new System.Windows.Forms.Panel();
			this.grbButtons.SuspendLayout();
			this.SuspendLayout();
			// 
			// grbButtons
			// 
			this.grbButtons.AccessibleDescription = resources.GetString("grbButtons.AccessibleDescription");
			this.grbButtons.AccessibleName = resources.GetString("grbButtons.AccessibleName");
			this.grbButtons.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("grbButtons.Anchor")));
			this.grbButtons.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("grbButtons.BackgroundImage")));
			this.grbButtons.Controls.Add(this.btnClose);
			this.grbButtons.Controls.Add(this.btnHelp);
			this.grbButtons.Controls.Add(this.btnDelete);
			this.grbButtons.Controls.Add(this.btnSave);
			this.grbButtons.Controls.Add(this.btnEdit);
			this.grbButtons.Controls.Add(this.btnAdd);
			this.grbButtons.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("grbButtons.Dock")));
			this.grbButtons.Enabled = ((bool)(resources.GetObject("grbButtons.Enabled")));
			this.grbButtons.Font = ((System.Drawing.Font)(resources.GetObject("grbButtons.Font")));
			this.grbButtons.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("grbButtons.ImeMode")));
			this.grbButtons.Location = ((System.Drawing.Point)(resources.GetObject("grbButtons.Location")));
			this.grbButtons.Name = "grbButtons";
			this.grbButtons.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("grbButtons.RightToLeft")));
			this.grbButtons.Size = ((System.Drawing.Size)(resources.GetObject("grbButtons.Size")));
			this.grbButtons.TabIndex = ((int)(resources.GetObject("grbButtons.TabIndex")));
			this.grbButtons.TabStop = false;
			this.grbButtons.Text = resources.GetString("grbButtons.Text");
			this.grbButtons.Visible = ((bool)(resources.GetObject("grbButtons.Visible")));
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
			// grbControls
			// 
			this.grbControls.AccessibleDescription = resources.GetString("grbControls.AccessibleDescription");
			this.grbControls.AccessibleName = resources.GetString("grbControls.AccessibleName");
			this.grbControls.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("grbControls.Anchor")));
			this.grbControls.AutoScroll = ((bool)(resources.GetObject("grbControls.AutoScroll")));
			this.grbControls.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("grbControls.AutoScrollMargin")));
			this.grbControls.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("grbControls.AutoScrollMinSize")));
			this.grbControls.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("grbControls.BackgroundImage")));
			this.grbControls.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("grbControls.Dock")));
			this.grbControls.Enabled = ((bool)(resources.GetObject("grbControls.Enabled")));
			this.grbControls.Font = ((System.Drawing.Font)(resources.GetObject("grbControls.Font")));
			this.grbControls.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("grbControls.ImeMode")));
			this.grbControls.Location = ((System.Drawing.Point)(resources.GetObject("grbControls.Location")));
			this.grbControls.Name = "grbControls";
			this.grbControls.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("grbControls.RightToLeft")));
			this.grbControls.Size = ((System.Drawing.Size)(resources.GetObject("grbControls.Size")));
			this.grbControls.TabIndex = ((int)(resources.GetObject("grbControls.TabIndex")));
			this.grbControls.Text = resources.GetString("grbControls.Text");
			this.grbControls.Visible = ((bool)(resources.GetObject("grbControls.Visible")));
			// 
			// ViewSingleRecord
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
			this.Controls.Add(this.grbControls);
			this.Controls.Add(this.grbButtons);
			this.Enabled = ((bool)(resources.GetObject("$this.Enabled")));
			this.Font = ((System.Drawing.Font)(resources.GetObject("$this.Font")));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("$this.ImeMode")));
			this.Location = ((System.Drawing.Point)(resources.GetObject("$this.Location")));
			this.MaximumSize = ((System.Drawing.Size)(resources.GetObject("$this.MaximumSize")));
			this.MinimumSize = ((System.Drawing.Size)(resources.GetObject("$this.MinimumSize")));
			this.Name = "ViewSingleRecord";
			this.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("$this.RightToLeft")));
			this.StartPosition = ((System.Windows.Forms.FormStartPosition)(resources.GetObject("$this.StartPosition")));
			this.Text = resources.GetString("$this.Text");
			this.Closing += new System.ComponentModel.CancelEventHandler(this.ViewSingleRecord_Closing);
			this.Load += new System.EventHandler(this.ViewSingleRecord_Load);
			this.grbButtons.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		#region Form events

		private void ViewSingleRecord_Load(object sender, EventArgs e)
		{
			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.WaitCursor;

			#endregion Code Inserted Automatically

			
			const string METHOD_NAME = THIS + ".ViewSingleRecord_Load()";
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
				this.LoadControls(this.mRecordFields, this.mRecordData);
				if (this.mFormAction == EnumAction.Add)
				{
					btnDelete.Enabled = false;
					btnEdit.Enabled = false;
				}
				else if (this.mFormAction == EnumAction.Edit)
				{
					btnDelete.Enabled = true;
					btnEdit.Enabled = false;
				}
				else if (this.mFormAction == EnumAction.Default)
				{
					btnDelete.Enabled = true;
					btnEdit.Enabled = true;
				}
				if (this.mViewOnly)
				{
					this.btnAdd.Enabled = false;
					this.btnEdit.Enabled = false;
					this.btnSave.Enabled = false;
					this.btnDelete.Enabled = false;
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

		private void btnAdd_Click(object sender, EventArgs e)
		{
			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.WaitCursor;

			#endregion Code Inserted Automatically

			
			const string METHOD_NAME = THIS + ".btnAdd_Click()";
			try
			{
				// clear all value control and allow user to input data
				foreach (Control objControl in this.grbControls.Controls)
				{
					objControl.Enabled = true;
					Type objType = objControl.GetType();
					if (objType.Equals(typeof(C1TextBox)))
					{
						sys_TableFieldVO voTableField = (sys_TableFieldVO)objControl.Tag;
						// if field is Identity column then we need to increae value
						if (voTableField.IdentityColumn)
						{
							// get max value from table
							int intMaxValue = boViewTable.GetMaxValue(mTableData.Tables[0].TableName, voTableField.FieldName);
							// get increament step
							long lngStep = mTableData.Tables[0].Columns[voTableField.FieldName].AutoIncrementStep;
							// assign value
							((C1TextBox)objControl).Value = (intMaxValue + lngStep).ToString();
						}
						else
						{
							((C1TextBox)objControl).Value = string.Empty;
						}
					}
					else if (objType.Equals(typeof(TextBox)))
					{
						objControl.Text = string.Empty;
					}
					else if (objType.Equals(typeof(C1Combo)))
					{
						((C1Combo)objControl).ReadOnly = false;
					}
					else
					{
						continue;
					}
				}
				// turn on Add action
				this.mFormAction = EnumAction.Add;
				this.btnSave.Enabled = true;
				this.btnDelete.Enabled = false;
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

		private void btnSave_Click(object sender, EventArgs e)
		{
			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.WaitCursor;

			#endregion Code Inserted Automatically

			
			const string METHOD_NAME = THIS + ".btnSave_Click()";
			try
			{
				this.SaveData();
				// turn to Default mode
				this.mFormAction = EnumAction.Default;
				this.btnSave.Enabled = false;
				// unable to edit
				foreach (Control objControl in this.grbControls.Controls)
				{
					if (!objControl.GetType().Equals(typeof(Label)))
					{
						objControl.Enabled = false;
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

		private void btnEdit_Click(object sender, EventArgs e)
		{
			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.WaitCursor;

			#endregion Code Inserted Automatically

			
			const string METHOD_NAME = THIS + ".btnEdit_Click()";
			try
			{ // enable all editable field
				foreach (Control objControl in this.grbControls.Controls)
				{
					if (!objControl.GetType().Equals(typeof(Label)))
					{
						objControl.Enabled = true;
						if (objControl.GetType().Equals(typeof(C1Combo)))
						{
							((C1Combo)objControl).ReadOnly = false;
						}
					}
				}
				this.btnSave.Enabled = true;
				// turn on Edit action
				this.mFormAction = EnumAction.Edit;
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

		private void btnDelete_Click(object sender, EventArgs e)
		{
			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.WaitCursor;

			#endregion Code Inserted Automatically

			
			const string METHOD_NAME = THIS + ".btnDelete_Click()";
			try
			{
				// display confirm message
				DialogResult dlgResult = PCSMessageBox.Show(ErrorCode.MESSAGE_DELETE_RECORD, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
				// delete
				if (dlgResult == DialogResult.Yes)
				{
					// tell ViewTable form to delete data
					this.mDeleteRow = true;
					// turn to Default action in order to close the form
					this.mFormAction = EnumAction.Default;
					// close the form
					this.Close();
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

		private void btnHelp_Click(object sender, EventArgs e)
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

		private void ViewSingleRecord_Closing(object sender, CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".VIewSingleRecord_Closing()";
			try
			{
				if (this.mFormAction != EnumAction.Default)
				{
					DialogResult dlgResult = PCSMessageBox.Show(ErrorCode.MESSAGE_QUESTION_STORE_INTO_DATABASE, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
					switch (dlgResult)
					{
						case DialogResult.Yes:
							try
							{
								if (!this.SaveData())
								{
									e.Cancel =true;
								}
								else
								{
									e.Cancel = false;
								}
							}
							catch
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
				else
				{
					e.Cancel = false;
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

		#endregion

		#region private methods

		//**************************************************************************              
		///    <Description>
		///       Load fields and its value to form
		///    </Description>
		///    <Inputs>
		///       DataRow
		///    </Inputs>
		///    <Outputs>
		///       N/A
		///    </Outputs>
		///    <Returns>
		///       N/A
		///    </Returns>
		///    <Authors>
		///       DungLA
		///    </Authors>
		///    <History>
		///       02-Feb-2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private void LoadControls(ArrayList parrFields, DataRow pdrowData)
		{
			const string METHOD_NAME = THIS + ".LoadControls()";
			const int DISTANCE_BETWEEN_CONTROL = 2;
			try
			{
				this.grbControls.Controls.Clear();
				int intTabIndex = grbControls.TabIndex + 1;

				for (int i = 0; i < parrFields.Count; i++)
				{
					Control objLastControl = null;
					if (this.grbControls.Controls.Count > 0)
					{
						objLastControl = this.grbControls.Controls[this.grbControls.Controls.Count -1];
					}
					sys_TableFieldVO voTableField = (sys_TableFieldVO)parrFields[i];
					DataColumn dcolData = pdrowData.Table.Columns[voTableField.FieldName];
					
					#region add label
					Label lblFieldName = new Label();
					lblFieldName.Name = LABEL_PREFIX + voTableField.FieldName;
					if (Thread.CurrentThread.CurrentCulture.Name == Constants.CULTURE_EN)
					{
						lblFieldName.Text = voTableField.CaptionEN;
					}
					if (Thread.CurrentThread.CurrentCulture.Name == Constants.CULTURE_VN)
					{
						lblFieldName.Text = voTableField.CaptionVN;
					}
					if (Thread.CurrentThread.CurrentCulture.Name == Constants.CULTURE_JP)
					{
						lblFieldName.Text = voTableField.CaptionJP;
					}
					lblFieldName.Text = voTableField.Caption;
					lblFieldName.Size = new Size(DEFAULT_WIDTH, DEFAULT_HEIGHT);
					if (objLastControl != null)
					{
						lblFieldName.Location = new Point(LABEL_X_LOCATION, DISTANCE_BETWEEN_CONTROL + objLastControl.Location.Y + objLastControl.Height);
					}
					else
					{
						lblFieldName.Location = new Point(LABEL_X_LOCATION, intTabIndex * Y_LOCATION + DISTANCE_BETWEEN_CONTROL);
					}
					
					this.grbControls.Controls.Add(lblFieldName);
					#endregion

					#region combo box
					Control objFieldControl = new Control();
					if ((dcolData.DataType != typeof(bool)) && (dcolData.DataType != typeof(DateTime)))
					{
						if (((voTableField.Items != null) && (voTableField.Items.Trim() != string.Empty))
							|| ((voTableField.FromTable != null) && (voTableField.FromTable.Trim() != string.Empty)))
						{
							C1Combo cboField = new C1Combo();
							cboField.Name = COMBO_PREFIX + voTableField.FieldName;
							cboField.Location = new Point(CONTROL_X_LOCATION, lblFieldName.Location.Y);
							cboField.Size = new Size(voTableField.Width, DEFAULT_HEIGHT);
							cboField.TabIndex = ++ intTabIndex;
							cboField.Tag = voTableField;
							cboField.ComboStyle = ComboStyleEnum.DropdownList;
							cboField.DropdownPosition = DropdownPositionEnum.LeftDown;
							cboField.DroppedDown = true;
							this.grbControls.Controls.Add(cboField);
							if ((voTableField.Items != null) && (voTableField.Items.Trim() != string.Empty))
							{
								// get a list values (separated by #)
								string[] strItems = voTableField.Items.Split(Constants.VIEW_TABLE_ITEM_SEPARATOR);

								for (int j = 0; j < strItems.Length; j++)
								{
									strItems[j] = strItems[j].Trim();
								}
								cboField.DataSource = strItems;
								// set selected item
								cboField.SelectedValue = pdrowData[dcolData];
							}
							// for a field that gets data from another field table
							// we add new button to show ViewTable form
							if ((voTableField.FromTable != null) && (voTableField.FromTable.Trim() != string.Empty))
							{
								// get data from FromTable and return data table to bind to combo
								SingleRecordBO boSingleRecord = new SingleRecordBO();
								DataTable tblData = boSingleRecord.GetDataFromTable(voTableField.FromTable, voTableField.FromField, voTableField.FilterField1, voTableField.FilterField2);
								// bind data to combo
								FormControlComponents.PutDataIntoC1ComboBox(cboField, tblData, voTableField.FilterField1, voTableField.FromField, voTableField.FromTable);
								// set selected item
								cboField.SelectedValue = pdrowData[dcolData];
							}
							// if open form with Default action, then unable to edit data
							if (this.mFormAction == EnumAction.Default)
							{
								cboField.ReadOnly = true;
								//cboField.Enabled = false;
							}
							else
							{
								cboField.ReadOnly = false;
								//cboField.Enabled = true;
							}
						}

							#endregion

					#region textbox

						else // textbox
						{
							C1TextBox txtField = new C1TextBox();
							txtField.Name = TEXTBOX_PREFIX + voTableField.FieldName;
							txtField.Value = pdrowData[dcolData].ToString();
							if (voTableField.Align == 0)
							{
								txtField.TextAlign = HorizontalAlignment.Left;
							}
							else if (voTableField.Align == 1)
							{
								txtField.TextAlign = HorizontalAlignment.Center;
							}
							else if (voTableField.Align == 2)
							{
								txtField.TextAlign = HorizontalAlignment.Right;
							}
							if ((voTableField.Formats != null) && (voTableField.Formats.Trim() != string.Empty))
							{
								txtField.EditMask = voTableField.Formats;
							}

							txtField.Location = new Point(CONTROL_X_LOCATION, lblFieldName.Location.Y);
							txtField.Size = new Size(voTableField.Width, DEFAULT_HEIGHT);

							// for the Auto Increment column, user is not allowed to edit data
							if (dcolData.AutoIncrement || voTableField.ReadOnly)
							{
								txtField.ReadOnly = true;
							}
							else
							{
								txtField.ReadOnly = false;
							}
							txtField.TabIndex = ++ intTabIndex;
							txtField.Tag = voTableField;
							// add text box to Controls list
							this.grbControls.Controls.Add(txtField);
							if (this.mFormAction == EnumAction.Default)
							{
								txtField.Enabled = false;
							}
							else
							{
								txtField.Enabled = true;
							}
						}
					}

					#endregion

					#region checkbox
					if (dcolData.DataType == typeof(bool))
					{
						objFieldControl = new CheckBox();
						objFieldControl.Name = CHECKBOX_PREFIX + dcolData.ColumnName;
						((CheckBox)objFieldControl).Checked = Convert.ToBoolean(pdrowData[dcolData].ToString());
						objFieldControl.Location = new Point(CONTROL_X_LOCATION, lblFieldName.Location.Y);
						objFieldControl.Size = new Size(voTableField.Width, DEFAULT_HEIGHT);
						objFieldControl.TabIndex = ++ intTabIndex;
						objFieldControl.Tag = voTableField;
						this.grbControls.Controls.Add(objFieldControl);
						
						if (this.mFormAction == EnumAction.Default)
						{
							objFieldControl.Enabled = false;
						}
						else
						{
							objFieldControl.Enabled = true;
						}
					}
					#endregion

					#region DateTimePicker
					if (dcolData.DataType == typeof(DateTime))
					{
						objFieldControl = new DateTimePicker();
						objFieldControl.Name = DATEPICKER_PREFIX + dcolData.ColumnName;
						((DateTimePicker)objFieldControl).Format = DateTimePickerFormat.Short;
						try
						{
							((DateTimePicker)objFieldControl).Value = DateTime.Parse(pdrowData[dcolData].ToString());
						}
						catch
						{
							((DateTimePicker)objFieldControl).Value = DateTime.Now;
						}
						objFieldControl.Location = new Point(CONTROL_X_LOCATION, lblFieldName.Location.Y);
						objFieldControl.Size = new Size(voTableField.Width, DEFAULT_HEIGHT);
						objFieldControl.TabIndex = ++ intTabIndex;
						objFieldControl.Tag = voTableField;
						this.grbControls.Controls.Add(objFieldControl);

						if (this.mFormAction == EnumAction.Default)
						{
							objFieldControl.Enabled = false;
						}
						else
						{
							objFieldControl.Enabled = true;
						}
					}
					#endregion
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


		//**************************************************************************              
		///    <Description>
		///       This method use to save data to data base
		///    </Description>
		///    <Inputs>
		///       
		///    </Inputs>
		///    <Outputs>
		///       bool
		///    </Outputs>
		///    <Returns>
		///       true if successful, false if failed
		///    </Returns>
		///    <Authors>
		///       DungLA
		///    </Authors>
		///    <History>
		///       03-Feb-2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private bool SaveData()
		{
			try
			{
				DataRow drowNewData = null;
				if (this.mFormAction == EnumAction.Add)
				{
					// create new DataRow from source table
					drowNewData = this.mTableData.Tables[0].NewRow();
				}
				else if (this.mFormAction == EnumAction.Edit)
				{
					// update current rowm
					drowNewData = this.mRecordData;
				}
				foreach (Control objControl in this.grbControls.Controls)
				{
					Type objType = objControl.GetType();
					// ignore Label control
					if (objType.Equals(typeof(Label)))
					{
						continue;
					}
					else
					{
						sys_TableFieldVO voTableField = (sys_TableFieldVO)objControl.Tag;
						// if field is not allow null, we check mandatory
						if (voTableField.NotAllowNull)
						{
							if (this.mTableData.Tables[0].Columns[voTableField.FieldName].DataType != typeof(bool))
							{
								if (FormControlComponents.CheckMandatory(objControl))
								{
									PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxButtons.OK, MessageBoxIcon.Error);
									objControl.Focus();
									objControl.Select();
									return false;
								}
							}
						}
						// getting data from control
						if (objType.Equals(typeof(C1TextBox)))
						{
							drowNewData[voTableField.FieldName] = ((C1TextBox)objControl).Value;
						}
						else if (objType.Equals(typeof(C1Combo)))
						{
							drowNewData[voTableField.FieldName] = ((C1Combo)objControl).SelectedValue;
						}
						else if (objType.Equals(typeof(CheckBox)))
						{
							drowNewData[voTableField.FieldName] = ((CheckBox)objControl).Checked;
						}
						else if (objType.Equals(typeof(DateTimePicker)))
						{
							drowNewData[voTableField.FieldName] = ((DateTimePicker)objControl).Value;
						}
					}
				}
				if (this.mFormAction == EnumAction.Add)
				{
					this.mTableData.Tables[0].Rows.Add(drowNewData);
				}
				// save changes to database
				this.boViewTable.UpdateDataSetViewTable(this.mTableData, this.mSelectCommand, !this.mViewOnly, this.mUpdateCommand);
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
		#endregion
	}
}
