using System;
using System.Windows.Forms;
using PCSUtils.Utils;
using PCSComUtils.Common;
using PCSComUtils.PCSExc;
using PCSUtils.Log;
using PCSComUtils.Admin.BO;
//using PCSComUtils.Admin.DS;

namespace PCSUtils.Admin
{
	/// <summary>
	/// Summary description for UpdatePassword.
	/// </summary>
	public class UpdatePassword : System.Windows.Forms.Form
	{
		#region Declaration
		
		#region System Generated

		private System.Windows.Forms.TextBox txtOldPassword;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.TextBox txtNewPassword;
		private System.Windows.Forms.TextBox txtConfirmPassword;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.Button btnHelp;
		private System.Windows.Forms.Label lblOldPass;
		private System.Windows.Forms.Label lblNewPass;
		private System.Windows.Forms.Label lblConfirmPass;
		
		#endregion System Generated
		
		const string THIS = "PCSUtils.Admin.UpdatePassword";
		private string strUserName;

		#endregion Declaration
		
		#region Constructor, Destructor
		public UpdatePassword()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
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
		
		#endregion Constructor, Destructor

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(UpdatePassword));
			this.lblOldPass = new System.Windows.Forms.Label();
			this.txtOldPassword = new System.Windows.Forms.TextBox();
			this.lblNewPass = new System.Windows.Forms.Label();
			this.txtNewPassword = new System.Windows.Forms.TextBox();
			this.lblConfirmPass = new System.Windows.Forms.Label();
			this.txtConfirmPassword = new System.Windows.Forms.TextBox();
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnOK = new System.Windows.Forms.Button();
			this.btnHelp = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// lblOldPass
			// 
			this.lblOldPass.AccessibleDescription = resources.GetString("lblOldPass.AccessibleDescription");
			this.lblOldPass.AccessibleName = resources.GetString("lblOldPass.AccessibleName");
			this.lblOldPass.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblOldPass.Anchor")));
			this.lblOldPass.AutoSize = ((bool)(resources.GetObject("lblOldPass.AutoSize")));
			this.lblOldPass.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblOldPass.Dock")));
			this.lblOldPass.Enabled = ((bool)(resources.GetObject("lblOldPass.Enabled")));
			this.lblOldPass.Font = ((System.Drawing.Font)(resources.GetObject("lblOldPass.Font")));
			this.lblOldPass.ForeColor = System.Drawing.Color.Maroon;
			this.lblOldPass.Image = ((System.Drawing.Image)(resources.GetObject("lblOldPass.Image")));
			this.lblOldPass.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblOldPass.ImageAlign")));
			this.lblOldPass.ImageIndex = ((int)(resources.GetObject("lblOldPass.ImageIndex")));
			this.lblOldPass.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblOldPass.ImeMode")));
			this.lblOldPass.Location = ((System.Drawing.Point)(resources.GetObject("lblOldPass.Location")));
			this.lblOldPass.Name = "lblOldPass";
			this.lblOldPass.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblOldPass.RightToLeft")));
			this.lblOldPass.Size = ((System.Drawing.Size)(resources.GetObject("lblOldPass.Size")));
			this.lblOldPass.TabIndex = ((int)(resources.GetObject("lblOldPass.TabIndex")));
			this.lblOldPass.Text = resources.GetString("lblOldPass.Text");
			this.lblOldPass.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblOldPass.TextAlign")));
			this.lblOldPass.Visible = ((bool)(resources.GetObject("lblOldPass.Visible")));
			// 
			// txtOldPassword
			// 
			this.txtOldPassword.AccessibleDescription = resources.GetString("txtOldPassword.AccessibleDescription");
			this.txtOldPassword.AccessibleName = resources.GetString("txtOldPassword.AccessibleName");
			this.txtOldPassword.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("txtOldPassword.Anchor")));
			this.txtOldPassword.AutoSize = ((bool)(resources.GetObject("txtOldPassword.AutoSize")));
			this.txtOldPassword.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("txtOldPassword.BackgroundImage")));
			this.txtOldPassword.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("txtOldPassword.Dock")));
			this.txtOldPassword.Enabled = ((bool)(resources.GetObject("txtOldPassword.Enabled")));
			this.txtOldPassword.Font = ((System.Drawing.Font)(resources.GetObject("txtOldPassword.Font")));
			this.txtOldPassword.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("txtOldPassword.ImeMode")));
			this.txtOldPassword.Location = ((System.Drawing.Point)(resources.GetObject("txtOldPassword.Location")));
			this.txtOldPassword.MaxLength = ((int)(resources.GetObject("txtOldPassword.MaxLength")));
			this.txtOldPassword.Multiline = ((bool)(resources.GetObject("txtOldPassword.Multiline")));
			this.txtOldPassword.Name = "txtOldPassword";
			this.txtOldPassword.PasswordChar = ((char)(resources.GetObject("txtOldPassword.PasswordChar")));
			this.txtOldPassword.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("txtOldPassword.RightToLeft")));
			this.txtOldPassword.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("txtOldPassword.ScrollBars")));
			this.txtOldPassword.Size = ((System.Drawing.Size)(resources.GetObject("txtOldPassword.Size")));
			this.txtOldPassword.TabIndex = ((int)(resources.GetObject("txtOldPassword.TabIndex")));
			this.txtOldPassword.Text = resources.GetString("txtOldPassword.Text");
			this.txtOldPassword.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("txtOldPassword.TextAlign")));
			this.txtOldPassword.Visible = ((bool)(resources.GetObject("txtOldPassword.Visible")));
			this.txtOldPassword.WordWrap = ((bool)(resources.GetObject("txtOldPassword.WordWrap")));
			this.txtOldPassword.Leave += new System.EventHandler(this.OnLeaveControl);
			this.txtOldPassword.Enter += new System.EventHandler(this.OnEnterControl);
			// 
			// lblNewPass
			// 
			this.lblNewPass.AccessibleDescription = resources.GetString("lblNewPass.AccessibleDescription");
			this.lblNewPass.AccessibleName = resources.GetString("lblNewPass.AccessibleName");
			this.lblNewPass.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblNewPass.Anchor")));
			this.lblNewPass.AutoSize = ((bool)(resources.GetObject("lblNewPass.AutoSize")));
			this.lblNewPass.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblNewPass.Dock")));
			this.lblNewPass.Enabled = ((bool)(resources.GetObject("lblNewPass.Enabled")));
			this.lblNewPass.Font = ((System.Drawing.Font)(resources.GetObject("lblNewPass.Font")));
			this.lblNewPass.ForeColor = System.Drawing.Color.Maroon;
			this.lblNewPass.Image = ((System.Drawing.Image)(resources.GetObject("lblNewPass.Image")));
			this.lblNewPass.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblNewPass.ImageAlign")));
			this.lblNewPass.ImageIndex = ((int)(resources.GetObject("lblNewPass.ImageIndex")));
			this.lblNewPass.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblNewPass.ImeMode")));
			this.lblNewPass.Location = ((System.Drawing.Point)(resources.GetObject("lblNewPass.Location")));
			this.lblNewPass.Name = "lblNewPass";
			this.lblNewPass.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblNewPass.RightToLeft")));
			this.lblNewPass.Size = ((System.Drawing.Size)(resources.GetObject("lblNewPass.Size")));
			this.lblNewPass.TabIndex = ((int)(resources.GetObject("lblNewPass.TabIndex")));
			this.lblNewPass.Text = resources.GetString("lblNewPass.Text");
			this.lblNewPass.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblNewPass.TextAlign")));
			this.lblNewPass.Visible = ((bool)(resources.GetObject("lblNewPass.Visible")));
			// 
			// txtNewPassword
			// 
			this.txtNewPassword.AccessibleDescription = resources.GetString("txtNewPassword.AccessibleDescription");
			this.txtNewPassword.AccessibleName = resources.GetString("txtNewPassword.AccessibleName");
			this.txtNewPassword.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("txtNewPassword.Anchor")));
			this.txtNewPassword.AutoSize = ((bool)(resources.GetObject("txtNewPassword.AutoSize")));
			this.txtNewPassword.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("txtNewPassword.BackgroundImage")));
			this.txtNewPassword.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("txtNewPassword.Dock")));
			this.txtNewPassword.Enabled = ((bool)(resources.GetObject("txtNewPassword.Enabled")));
			this.txtNewPassword.Font = ((System.Drawing.Font)(resources.GetObject("txtNewPassword.Font")));
			this.txtNewPassword.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("txtNewPassword.ImeMode")));
			this.txtNewPassword.Location = ((System.Drawing.Point)(resources.GetObject("txtNewPassword.Location")));
			this.txtNewPassword.MaxLength = ((int)(resources.GetObject("txtNewPassword.MaxLength")));
			this.txtNewPassword.Multiline = ((bool)(resources.GetObject("txtNewPassword.Multiline")));
			this.txtNewPassword.Name = "txtNewPassword";
			this.txtNewPassword.PasswordChar = ((char)(resources.GetObject("txtNewPassword.PasswordChar")));
			this.txtNewPassword.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("txtNewPassword.RightToLeft")));
			this.txtNewPassword.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("txtNewPassword.ScrollBars")));
			this.txtNewPassword.Size = ((System.Drawing.Size)(resources.GetObject("txtNewPassword.Size")));
			this.txtNewPassword.TabIndex = ((int)(resources.GetObject("txtNewPassword.TabIndex")));
			this.txtNewPassword.Text = resources.GetString("txtNewPassword.Text");
			this.txtNewPassword.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("txtNewPassword.TextAlign")));
			this.txtNewPassword.Visible = ((bool)(resources.GetObject("txtNewPassword.Visible")));
			this.txtNewPassword.WordWrap = ((bool)(resources.GetObject("txtNewPassword.WordWrap")));
			this.txtNewPassword.Leave += new System.EventHandler(this.OnLeaveControl);
			this.txtNewPassword.Enter += new System.EventHandler(this.OnEnterControl);
			// 
			// lblConfirmPass
			// 
			this.lblConfirmPass.AccessibleDescription = resources.GetString("lblConfirmPass.AccessibleDescription");
			this.lblConfirmPass.AccessibleName = resources.GetString("lblConfirmPass.AccessibleName");
			this.lblConfirmPass.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblConfirmPass.Anchor")));
			this.lblConfirmPass.AutoSize = ((bool)(resources.GetObject("lblConfirmPass.AutoSize")));
			this.lblConfirmPass.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblConfirmPass.Dock")));
			this.lblConfirmPass.Enabled = ((bool)(resources.GetObject("lblConfirmPass.Enabled")));
			this.lblConfirmPass.Font = ((System.Drawing.Font)(resources.GetObject("lblConfirmPass.Font")));
			this.lblConfirmPass.ForeColor = System.Drawing.Color.Maroon;
			this.lblConfirmPass.Image = ((System.Drawing.Image)(resources.GetObject("lblConfirmPass.Image")));
			this.lblConfirmPass.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblConfirmPass.ImageAlign")));
			this.lblConfirmPass.ImageIndex = ((int)(resources.GetObject("lblConfirmPass.ImageIndex")));
			this.lblConfirmPass.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblConfirmPass.ImeMode")));
			this.lblConfirmPass.Location = ((System.Drawing.Point)(resources.GetObject("lblConfirmPass.Location")));
			this.lblConfirmPass.Name = "lblConfirmPass";
			this.lblConfirmPass.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblConfirmPass.RightToLeft")));
			this.lblConfirmPass.Size = ((System.Drawing.Size)(resources.GetObject("lblConfirmPass.Size")));
			this.lblConfirmPass.TabIndex = ((int)(resources.GetObject("lblConfirmPass.TabIndex")));
			this.lblConfirmPass.Text = resources.GetString("lblConfirmPass.Text");
			this.lblConfirmPass.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblConfirmPass.TextAlign")));
			this.lblConfirmPass.Visible = ((bool)(resources.GetObject("lblConfirmPass.Visible")));
			// 
			// txtConfirmPassword
			// 
			this.txtConfirmPassword.AccessibleDescription = resources.GetString("txtConfirmPassword.AccessibleDescription");
			this.txtConfirmPassword.AccessibleName = resources.GetString("txtConfirmPassword.AccessibleName");
			this.txtConfirmPassword.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("txtConfirmPassword.Anchor")));
			this.txtConfirmPassword.AutoSize = ((bool)(resources.GetObject("txtConfirmPassword.AutoSize")));
			this.txtConfirmPassword.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("txtConfirmPassword.BackgroundImage")));
			this.txtConfirmPassword.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("txtConfirmPassword.Dock")));
			this.txtConfirmPassword.Enabled = ((bool)(resources.GetObject("txtConfirmPassword.Enabled")));
			this.txtConfirmPassword.Font = ((System.Drawing.Font)(resources.GetObject("txtConfirmPassword.Font")));
			this.txtConfirmPassword.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("txtConfirmPassword.ImeMode")));
			this.txtConfirmPassword.Location = ((System.Drawing.Point)(resources.GetObject("txtConfirmPassword.Location")));
			this.txtConfirmPassword.MaxLength = ((int)(resources.GetObject("txtConfirmPassword.MaxLength")));
			this.txtConfirmPassword.Multiline = ((bool)(resources.GetObject("txtConfirmPassword.Multiline")));
			this.txtConfirmPassword.Name = "txtConfirmPassword";
			this.txtConfirmPassword.PasswordChar = ((char)(resources.GetObject("txtConfirmPassword.PasswordChar")));
			this.txtConfirmPassword.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("txtConfirmPassword.RightToLeft")));
			this.txtConfirmPassword.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("txtConfirmPassword.ScrollBars")));
			this.txtConfirmPassword.Size = ((System.Drawing.Size)(resources.GetObject("txtConfirmPassword.Size")));
			this.txtConfirmPassword.TabIndex = ((int)(resources.GetObject("txtConfirmPassword.TabIndex")));
			this.txtConfirmPassword.Text = resources.GetString("txtConfirmPassword.Text");
			this.txtConfirmPassword.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("txtConfirmPassword.TextAlign")));
			this.txtConfirmPassword.Visible = ((bool)(resources.GetObject("txtConfirmPassword.Visible")));
			this.txtConfirmPassword.WordWrap = ((bool)(resources.GetObject("txtConfirmPassword.WordWrap")));
			this.txtConfirmPassword.Leave += new System.EventHandler(this.OnLeaveControl);
			this.txtConfirmPassword.Enter += new System.EventHandler(this.OnEnterControl);
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
			// btnOK
			// 
			this.btnOK.AccessibleDescription = resources.GetString("btnOK.AccessibleDescription");
			this.btnOK.AccessibleName = resources.GetString("btnOK.AccessibleName");
			this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnOK.Anchor")));
			this.btnOK.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnOK.BackgroundImage")));
			this.btnOK.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnOK.Dock")));
			this.btnOK.Enabled = ((bool)(resources.GetObject("btnOK.Enabled")));
			this.btnOK.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnOK.FlatStyle")));
			this.btnOK.Font = ((System.Drawing.Font)(resources.GetObject("btnOK.Font")));
			this.btnOK.Image = ((System.Drawing.Image)(resources.GetObject("btnOK.Image")));
			this.btnOK.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnOK.ImageAlign")));
			this.btnOK.ImageIndex = ((int)(resources.GetObject("btnOK.ImageIndex")));
			this.btnOK.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnOK.ImeMode")));
			this.btnOK.Location = ((System.Drawing.Point)(resources.GetObject("btnOK.Location")));
			this.btnOK.Name = "btnOK";
			this.btnOK.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnOK.RightToLeft")));
			this.btnOK.Size = ((System.Drawing.Size)(resources.GetObject("btnOK.Size")));
			this.btnOK.TabIndex = ((int)(resources.GetObject("btnOK.TabIndex")));
			this.btnOK.Text = resources.GetString("btnOK.Text");
			this.btnOK.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnOK.TextAlign")));
			this.btnOK.Visible = ((bool)(resources.GetObject("btnOK.Visible")));
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
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
			// 
			// UpdatePassword
			// 
			this.AcceptButton = this.btnOK;
			this.AccessibleDescription = resources.GetString("$this.AccessibleDescription");
			this.AccessibleName = resources.GetString("$this.AccessibleName");
			this.AutoScaleBaseSize = ((System.Drawing.Size)(resources.GetObject("$this.AutoScaleBaseSize")));
			this.AutoScroll = ((bool)(resources.GetObject("$this.AutoScroll")));
			this.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("$this.AutoScrollMargin")));
			this.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("$this.AutoScrollMinSize")));
			this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
			this.CancelButton = this.btnCancel;
			this.ClientSize = ((System.Drawing.Size)(resources.GetObject("$this.ClientSize")));
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOK);
			this.Controls.Add(this.txtConfirmPassword);
			this.Controls.Add(this.txtNewPassword);
			this.Controls.Add(this.txtOldPassword);
			this.Controls.Add(this.lblConfirmPass);
			this.Controls.Add(this.lblNewPass);
			this.Controls.Add(this.lblOldPass);
			this.Controls.Add(this.btnHelp);
			this.Enabled = ((bool)(resources.GetObject("$this.Enabled")));
			this.Font = ((System.Drawing.Font)(resources.GetObject("$this.Font")));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("$this.ImeMode")));
			this.Location = ((System.Drawing.Point)(resources.GetObject("$this.Location")));
			this.MaximizeBox = false;
			this.MaximumSize = ((System.Drawing.Size)(resources.GetObject("$this.MaximumSize")));
			this.MinimumSize = ((System.Drawing.Size)(resources.GetObject("$this.MinimumSize")));
			this.Name = "UpdatePassword";
			this.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("$this.RightToLeft")));
			this.StartPosition = ((System.Windows.Forms.FormStartPosition)(resources.GetObject("$this.StartPosition")));
			this.Text = resources.GetString("$this.Text");
			this.Load += new System.EventHandler(this.UpdatePassword_Load);
			this.ResumeLayout(false);

		}
		#endregion

		#region Class's Method And Properties
				
		/// <summary>
		/// This method uses to validate data on this form
		/// </summary>
		/// <returns></returns>
		/// <author> Son HT, Dec 13, 2004</author>
		private bool IsValidated()
		{
			const string METHOD_NAME = THIS + ".IsValidated()";
			bool blnIsCorrect = true;
			try
			{
				CommonBO boCommon = new CommonBO();
				//blnIsCorrect = boCommon.IsCorrectedPassword(strUserName,txtOldPassword.Text);
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

			// if incorrect old password
			if(!blnIsCorrect)
			{
				PCSMessageBox.Show(ErrorCode.INVALID_PASSWORD, MessageBoxIcon.Error);
				txtOldPassword.Focus();
				txtOldPassword.Select(0,txtOldPassword.Text.Length);
				return false;
			}

			// if new password != confirm password
			if(txtNewPassword.Text != txtConfirmPassword.Text)
			{
				PCSMessageBox.Show(ErrorCode.NEWPASSWORD_DIFFERENT_CONFIRMPASSWORD, MessageBoxIcon.Error);
				txtNewPassword.Focus();
				txtNewPassword.Select(0,txtNewPassword.Text.Length);
				return false;		
			}
			return true;
		}
				
		/// <summary>
		/// Check mandatory of a specific control
		/// </summary>
		/// <param name="pobjControl"></param>
		/// <returns></returns>
		/// <author> Son HT, Jan 20, 2005</author>
		public bool CheckMandatory(Control pobjControl)
		{
			if (pobjControl.Text == string.Empty)
			{
				return true;
			}
			return false;
		}		
		
		/// <summary>
		/// Make this button act like btnSearchProductCode_Click
		/// HACKED: Thachnn		
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author> Son HT, Jan 20, 2005</author>
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

		/// <summary>
		/// Process onleave control event
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author> Son HT, Jan 20, 2005</author>
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
		/// ENDHACKED: Thachnn		
		
		#endregion Class's Method And Properties
		
		#region Event Processing
		
		/// <summary>
		/// Processing Load event on this form 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author> Son HT, Dec 13, 2004</author>
		private void UpdatePassword_Load(object sender, System.EventArgs e)
		{
			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.WaitCursor;

			#endregion Code Inserted Automatically

			
			const string METHOD_NAME = THIS + ".UpdatePassword_Load()";
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
				strUserName = SystemProperty.UserName;
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

			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.Default;

			#endregion Code Inserted Automatically

			
		}
		
		/// <summary>
		/// Processing click event on Cancel button
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author> Son HT, Dec 13, 2004</author>
		private void btnCancel_Click(object sender, System.EventArgs e)
		{
			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.WaitCursor;

			#endregion Code Inserted Automatically

			
			Close();

			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.Default;

			#endregion Code Inserted Automatically

			
		}

				
		/// <summary>
		/// Processing click event on OK button
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author> Son HT, Dec 13, 2004</author>
		private void btnOK_Click(object sender, System.EventArgs e)
		{
			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.WaitCursor;

			#endregion Code Inserted Automatically

			
			const string METHOD_NAME = THIS + ".btnOK_Click()";
			try
			{
				// check mandatory old password
				if(CheckMandatory(txtOldPassword))
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Error);
					txtOldPassword.Focus();
					txtOldPassword.SelectAll();
					// Code Inserted Automatically
#region Code Inserted Automatically
this.Cursor = Cursors.Default;
#endregion Code Inserted Automatically

					return;
				}
				// check mandatory new password
				if(CheckMandatory(txtNewPassword))
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Error);
					txtNewPassword.Focus();
					txtNewPassword.SelectAll();
					// Code Inserted Automatically
#region Code Inserted Automatically
this.Cursor = Cursors.Default;
#endregion Code Inserted Automatically

					return;
				}
				// check mandatory confirm password
				if(CheckMandatory(txtConfirmPassword))
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Error);
					txtConfirmPassword.Focus();
					txtConfirmPassword.SelectAll();
					// Code Inserted Automatically
#region Code Inserted Automatically
this.Cursor = Cursors.Default;
#endregion Code Inserted Automatically

					return;
				}
				if(IsValidated())
				{
					// Update password
					CommonBO boCommon = new CommonBO();
					boCommon.UpdateNewPassword(strUserName,txtNewPassword.Text);
					PCSMessageBox.Show(ErrorCode.PASSWORD_UPDATE_SUCCESSFUL);
					Close();
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

			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.Default;

			#endregion Code Inserted Automatically

			
		}

		#endregion Event Processing
	}
}