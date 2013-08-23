using System;
using System.ComponentModel;
using System.Windows.Forms;
using PCSComUtils.Common;
using PCSComUtils.Framework.ReportFrame.BO;
using PCSComUtils.Framework.ReportFrame.DS;
using PCSComUtils.PCSExc;
using PCSUtils.Log;
using PCSUtils.Utils;

namespace PCSUtils.Framework.ReportFrame
{
	/// <summary>
	/// Summary description for ReportGroup.
	/// </summary>
	public class ReportGroup : Form
	{
		private TextBox txtGroupName;
		private Label lblGroupName;
		private Button btnCancel;
		private Button btnHelp;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private Container components = null;
		//Used for copy report group
		private const string THIS = "PCSUtils.Framework.ReportFrame.ReportGroup";
		
		private EnumAction mEnumType;
		private string mGroupName;
		private string mGroupID;
		private Button btnSave;
		private System.Windows.Forms.TextBox txtGroupCode;
		private System.Windows.Forms.Label lblGroupCode;
		private int mGroupOrder;
		private sys_ReportGroupVO mReportGroup = new sys_ReportGroupVO();
		private ReportGroupBO boReportGroup = new ReportGroupBO();

		public sys_ReportGroupVO ReportGroupVO
		{
			get { return mReportGroup; }
			set { mReportGroup = value; }
		}

		public EnumAction EnumType
		{
			get { return this.mEnumType; }
			set { this.mEnumType = value; }
		}
		public string GroupName
		{
			get { return this.mGroupName; }
			set { this.mGroupName = value; }
		}
		
		public string GroupID
		{
			get { return this.mGroupID; }
			set { this.mGroupID = value; }
		}

		public int GroupOrder
		{
			get { return this.mGroupOrder; }
			set { this.mGroupOrder = value; }
		}

		#region Constructors
		//**************************************************************************              
		///    <Description>
		///       Default constructor
		///    </Description>
		///    <Inputs>
		///       N/A
		///    </Inputs>
		///    <Outputs>
		///       N/A
		///    </Outputs>
		///    <Returns>
		///       
		///    </Returns>
		///    <Authors>
		///       DungLA
		///    </Authors>
		///    <History>
		///       3-Jan-2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public ReportGroup()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}


		#endregion

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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(ReportGroup));
			this.txtGroupName = new System.Windows.Forms.TextBox();
			this.lblGroupName = new System.Windows.Forms.Label();
			this.btnSave = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnHelp = new System.Windows.Forms.Button();
			this.txtGroupCode = new System.Windows.Forms.TextBox();
			this.lblGroupCode = new System.Windows.Forms.Label();
			this.SuspendLayout();
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
			this.txtGroupName.ForeColor = System.Drawing.SystemColors.WindowText;
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
			this.txtGroupName.Leave += new System.EventHandler(this.OnLeave);
			this.txtGroupName.Enter += new System.EventHandler(this.OnEnter);
			// 
			// lblGroupName
			// 
			this.lblGroupName.AccessibleDescription = resources.GetString("lblGroupName.AccessibleDescription");
			this.lblGroupName.AccessibleName = resources.GetString("lblGroupName.AccessibleName");
			this.lblGroupName.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblGroupName.Anchor")));
			this.lblGroupName.AutoSize = ((bool)(resources.GetObject("lblGroupName.AutoSize")));
			this.lblGroupName.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblGroupName.Dock")));
			this.lblGroupName.Enabled = ((bool)(resources.GetObject("lblGroupName.Enabled")));
			this.lblGroupName.Font = ((System.Drawing.Font)(resources.GetObject("lblGroupName.Font")));
			this.lblGroupName.ForeColor = System.Drawing.Color.Maroon;
			this.lblGroupName.Image = ((System.Drawing.Image)(resources.GetObject("lblGroupName.Image")));
			this.lblGroupName.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblGroupName.ImageAlign")));
			this.lblGroupName.ImageIndex = ((int)(resources.GetObject("lblGroupName.ImageIndex")));
			this.lblGroupName.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblGroupName.ImeMode")));
			this.lblGroupName.Location = ((System.Drawing.Point)(resources.GetObject("lblGroupName.Location")));
			this.lblGroupName.Name = "lblGroupName";
			this.lblGroupName.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblGroupName.RightToLeft")));
			this.lblGroupName.Size = ((System.Drawing.Size)(resources.GetObject("lblGroupName.Size")));
			this.lblGroupName.TabIndex = ((int)(resources.GetObject("lblGroupName.TabIndex")));
			this.lblGroupName.Text = resources.GetString("lblGroupName.Text");
			this.lblGroupName.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblGroupName.TextAlign")));
			this.lblGroupName.Visible = ((bool)(resources.GetObject("lblGroupName.Visible")));
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
			this.txtGroupCode.ReadOnly = true;
			this.txtGroupCode.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("txtGroupCode.RightToLeft")));
			this.txtGroupCode.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("txtGroupCode.ScrollBars")));
			this.txtGroupCode.Size = ((System.Drawing.Size)(resources.GetObject("txtGroupCode.Size")));
			this.txtGroupCode.TabIndex = ((int)(resources.GetObject("txtGroupCode.TabIndex")));
			this.txtGroupCode.TabStop = false;
			this.txtGroupCode.Text = resources.GetString("txtGroupCode.Text");
			this.txtGroupCode.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("txtGroupCode.TextAlign")));
			this.txtGroupCode.Visible = ((bool)(resources.GetObject("txtGroupCode.Visible")));
			this.txtGroupCode.WordWrap = ((bool)(resources.GetObject("txtGroupCode.WordWrap")));
			this.txtGroupCode.Leave += new System.EventHandler(this.OnLeave);
			this.txtGroupCode.Enter += new System.EventHandler(this.OnEnter);
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
			// ReportGroup
			// 
			this.AcceptButton = this.btnSave;
			this.AccessibleDescription = resources.GetString("$this.AccessibleDescription");
			this.AccessibleName = resources.GetString("$this.AccessibleName");
			this.AutoScaleBaseSize = ((System.Drawing.Size)(resources.GetObject("$this.AutoScaleBaseSize")));
			this.AutoScroll = ((bool)(resources.GetObject("$this.AutoScroll")));
			this.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("$this.AutoScrollMargin")));
			this.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("$this.AutoScrollMinSize")));
			this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
			this.CancelButton = this.btnCancel;
			this.ClientSize = ((System.Drawing.Size)(resources.GetObject("$this.ClientSize")));
			this.Controls.Add(this.txtGroupCode);
			this.Controls.Add(this.txtGroupName);
			this.Controls.Add(this.lblGroupCode);
			this.Controls.Add(this.btnHelp);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnSave);
			this.Controls.Add(this.lblGroupName);
			this.Enabled = ((bool)(resources.GetObject("$this.Enabled")));
			this.Font = ((System.Drawing.Font)(resources.GetObject("$this.Font")));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("$this.ImeMode")));
			this.Location = ((System.Drawing.Point)(resources.GetObject("$this.Location")));
			this.MaximizeBox = false;
			this.MaximumSize = ((System.Drawing.Size)(resources.GetObject("$this.MaximumSize")));
			this.MinimumSize = ((System.Drawing.Size)(resources.GetObject("$this.MinimumSize")));
			this.Name = "ReportGroup";
			this.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("$this.RightToLeft")));
			this.StartPosition = ((System.Windows.Forms.FormStartPosition)(resources.GetObject("$this.StartPosition")));
			this.Text = resources.GetString("$this.Text");
			this.Closing += new System.ComponentModel.CancelEventHandler(this.ReportGroup_Closing);
			this.Load += new System.EventHandler(this.ReportGroup_Load);
			this.ResumeLayout(false);

		}
		#endregion


		#region Form Events
		//**************************************************************************              
		///    <Description>
		///       Close the form
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
		///       DungLA
		///    </Authors>
		///    <History>
		///       06-Jan-2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private void btnCancel_Click(object sender, EventArgs e)
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
		///       Validate data and save new group information
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
		///       DungLA
		///    </Authors>
		///    <History>
		///       06-Jan-2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private void btnSave_Click(object sender, EventArgs e)
		{
			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.WaitCursor;

			#endregion Code Inserted Automatically

			
			const string METHOD_NAME = THIS + ".btnSave_Click()";
			try
			{
				// check mandatory fields
				if (!CheckMandatory(txtGroupCode))
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID);
					txtGroupCode.Focus();
					txtGroupCode.Select();
					// Code Inserted Automatically
#region Code Inserted Automatically
this.Cursor = Cursors.Default;
#endregion Code Inserted Automatically

					return;
				}
				if (!CheckMandatory(txtGroupName))
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID);
					txtGroupName.Focus();
					txtGroupName.Select();
					// Code Inserted Automatically
#region Code Inserted Automatically
this.Cursor = Cursors.Default;
#endregion Code Inserted Automatically

					return;
				}


				bool blnIsSaveDataOK = false;
				try
				{				
					blnIsSaveDataOK = SaveData();
					if (blnIsSaveDataOK)
					{
						PCSMessageBox.Show(ErrorCode.MESSAGE_AFTER_SAVE_DATA, MessageBoxIcon.Information);
						//DialogResult = DialogResult.OK;
						mEnumType = EnumAction.Default;
						// close the form after save succeed
						this.Close();
					}
					else
					{
						PCSMessageBox.Show(ErrorCode.MESSAGE_CANNOT_SAVE_TO_DB, MessageBoxIcon.Error);
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
						PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
					}
					txtGroupName.Select();
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
		///       Display help topic
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
		///       DungLA
		///    </Authors>
		///    <History>
		///       06-Jan-2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private void btnHelp_Click(object sender, EventArgs e)
		{
			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.WaitCursor;

			#endregion Code Inserted Automatically

			
			//PCSMessageBox.Show(ErrorCode.NOT_IMPLEMENT);

			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.Default;

			#endregion Code Inserted Automatically

			
		}
		//**************************************************************************              
		///    <Description>
		///       Form load event, determide mode for form
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
		///       DungLA
		///    </Authors>
		///    <History>
		///       06-Jan-2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private void ReportGroup_Load(object sender, EventArgs e)
		{
			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.WaitCursor;

			#endregion Code Inserted Automatically

			
			const string METHOD_NAME = THIS + ".ReportGroup_Load()";
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
					return;
				}
				#endregion

//				if (mEnumType != EnumAction.Edit)
//					txtGroupCode.Enabled = true;
				txtGroupCode.Text = mReportGroup.GroupID;
				txtGroupName.Text = mReportGroup.GroupName;
				// trim text to actual max length of control
				if (txtGroupCode.Text.Length > txtGroupCode.MaxLength)
					txtGroupCode.Text = txtGroupCode.Text.Substring(0, txtGroupCode.MaxLength - 1);
				if (txtGroupName.Text.Length > txtGroupName.MaxLength)
					txtGroupName.Text = txtGroupName.Text.Substring(0, txtGroupName.MaxLength - 1);
				txtGroupName.Focus();
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
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
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
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
				}
			}

			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.Default;

			#endregion Code Inserted Automatically

			
		}


		private void ReportGroup_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".ReportGroup_Closing()";
			try
			{
				if (mEnumType != EnumAction.Default)
				{
					// txtGroupCode.ModiModified //txtGroupCode.Text != string.Empty ||

//					if (txtGroupName.Modified || 				
//						txtGroupName.Text != string.Empty)
					
					if(txtGroupName.Text != string.Empty)
					{
						if (txtGroupName.Modified)
						{
							// display confirm message
							DialogResult dlgResult = PCSMessageBox.Show(ErrorCode.MESSAGE_QUESTION_STORE_INTO_DATABASE,MessageBoxButtons.YesNoCancel,MessageBoxIcon.Question,MessageBoxDefaultButton.Button1);
							switch (dlgResult)
							{
								case DialogResult.Cancel:
									e.Cancel = true;
									break;
								case DialogResult.Yes:
									try
									{
										// save data before close the form
										if (!SaveData())
											e.Cancel = true;
										else
											e.Cancel = false;
									}
									catch
									{
										e.Cancel = true;
									}
									break;
								case DialogResult.No:
									mReportGroup = new sys_ReportGroupVO();
									break;
							}
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
	
		#endregion


		#region Private Methods
		//**************************************************************************              
		///    <Description>
		///       This method use to check for mandatory field(s) in the form
		///    </Description>
		///    <Inputs>
		///       Control to check
		///    </Inputs>
		///    <Outputs>
		///       bool
		///    </Outputs>
		///    <Returns>
		///       true if validated, false if failed
		///    </Returns>
		///    <Authors>
		///       DungLA
		///    </Authors>
		///    <History>
		///       3-Jan-2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private bool CheckMandatory(Control pobjControl)
		{
			try
			{
				if (pobjControl.Text.Trim() == string.Empty)
				{
					return false;
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
			bool blnIsOK = false;
			try
			{
				switch (mEnumType)
				{
					case EnumAction.Edit:
						// prepare data for VO object
						string strOldID = mReportGroup.GroupID;
						mReportGroup.GroupID = txtGroupCode.Text.Trim();
						mReportGroup.GroupName = txtGroupName.Text.Trim();
						// update object to database
						boReportGroup.Update(mReportGroup, strOldID);
						mGroupID = mReportGroup.GroupID;
						mGroupName = mReportGroup.GroupName;
						mGroupOrder = mReportGroup.GroupOrder;
						blnIsOK = true;
						break;
					case EnumAction.Add:
					case EnumAction.Copy:
						mReportGroup = new sys_ReportGroupVO();
						// prepare data for VO object
						mReportGroup.GroupID = txtGroupCode.Text.Trim();
						mReportGroup.GroupName = txtGroupName.Text.Trim();
						// new group order = current max order + 1
						mReportGroup.GroupOrder = boReportGroup.GetMaxGroupOrder() + 1;
						// add new object to database
						boReportGroup.Add(mReportGroup);
						mGroupID = mReportGroup.GroupID;
						mGroupName = mReportGroup.GroupName;
						mGroupOrder = mReportGroup.GroupOrder;
						blnIsOK = true;
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
			return blnIsOK;
		}
		
		/// <summary>
		/// Controls is focused
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnEnter(object sender, EventArgs e)
		{
			const string METHOD_NAME = THIS + ".OnEnter()";
			try
			{
				FormControlComponents.OnEnterControl(sender, e);
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
		/// Control is lost focused
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnLeave(object sender, EventArgs e)
		{
			const string METHOD_NAME = THIS + ".OnLeave()";
			try
			{
				FormControlComponents.OnLeaveControl(sender, e);
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

		
	}
}
