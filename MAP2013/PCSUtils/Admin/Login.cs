using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Data;
using System.Runtime.Remoting;
using System.Collections.Specialized;

//PCS namespaces
using PCSComUtils.Admin.BO;
using PCSComUtils.Admin.DS;
using PCSComUtils.Common;
using PCSComUtils.Common.BO;
using PCSComUtils.MasterSetup.DS;
using PCSComUtils.PCSExc;
using PCSUtils.Log;
using PCSUtils.Utils;
using System.Threading;
using System.Globalization;

namespace PCSUtils.Admin
{
	/// <summary>
	/// Summary description for Login.
	/// </summary>
	public class Login : System.Windows.Forms.Form
	{
		#region Declaration
		
		#region System Generated
		
		private System.Windows.Forms.Label lblUserName;
		private System.Windows.Forms.Label lblPassword;
		private System.Windows.Forms.TextBox txtUser;
		private System.Windows.Forms.TextBox txtPwd;
		private System.ComponentModel.IContainer components;		
		private System.Windows.Forms.PictureBox ptrLogin;
		private System.Windows.Forms.PictureBox ptbBox;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.PictureBox btnOK;
		private System.Windows.Forms.PictureBox btnCancel;
		private System.Windows.Forms.Button btnOK1;
		private System.Windows.Forms.Button btnCancel1;
		private System.Windows.Forms.ImageList imlImage;
		
		#endregion System Generated
		
		const string CONFIG_FILE = "config.inf";
		const string CONNECTION_STRING = "ConnectionString=";
		private const string THIS = "PCSUtils.Admin.Login";
		public Sys_UserVO pObjobjecVO = new Sys_UserVO();

		#endregion Declaration
		
		#region Constructor, Destructor
	
		public Login()
		{
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(Constants.CULTURE_EN);
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
		
		#endregion Constructor, Destructor
		
		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(Login));
			this.lblUserName = new System.Windows.Forms.Label();
			this.txtUser = new System.Windows.Forms.TextBox();
			this.txtPwd = new System.Windows.Forms.TextBox();
			this.lblPassword = new System.Windows.Forms.Label();
			this.ptrLogin = new System.Windows.Forms.PictureBox();
			this.ptbBox = new System.Windows.Forms.PictureBox();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.btnOK = new System.Windows.Forms.PictureBox();
			this.btnCancel = new System.Windows.Forms.PictureBox();
			this.btnOK1 = new System.Windows.Forms.Button();
			this.btnCancel1 = new System.Windows.Forms.Button();
			this.imlImage = new System.Windows.Forms.ImageList(this.components);
			this.SuspendLayout();
			// 
			// lblUserName
			// 
			this.lblUserName.AccessibleDescription = resources.GetString("lblUserName.AccessibleDescription");
			this.lblUserName.AccessibleName = resources.GetString("lblUserName.AccessibleName");
			this.lblUserName.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblUserName.Anchor")));
			this.lblUserName.AutoSize = ((bool)(resources.GetObject("lblUserName.AutoSize")));
			this.lblUserName.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblUserName.Dock")));
			this.lblUserName.Enabled = ((bool)(resources.GetObject("lblUserName.Enabled")));
			this.lblUserName.Font = ((System.Drawing.Font)(resources.GetObject("lblUserName.Font")));
			this.lblUserName.ForeColor = System.Drawing.Color.Black;
			this.lblUserName.Image = ((System.Drawing.Image)(resources.GetObject("lblUserName.Image")));
			this.lblUserName.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblUserName.ImageAlign")));
			this.lblUserName.ImageIndex = ((int)(resources.GetObject("lblUserName.ImageIndex")));
			this.lblUserName.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblUserName.ImeMode")));
			this.lblUserName.Location = ((System.Drawing.Point)(resources.GetObject("lblUserName.Location")));
			this.lblUserName.Name = "lblUserName";
			this.lblUserName.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblUserName.RightToLeft")));
			this.lblUserName.Size = ((System.Drawing.Size)(resources.GetObject("lblUserName.Size")));
			this.lblUserName.TabIndex = ((int)(resources.GetObject("lblUserName.TabIndex")));
			this.lblUserName.Text = resources.GetString("lblUserName.Text");
			this.lblUserName.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblUserName.TextAlign")));
			this.lblUserName.Visible = ((bool)(resources.GetObject("lblUserName.Visible")));
			// 
			// txtUser
			// 
			this.txtUser.AccessibleDescription = resources.GetString("txtUser.AccessibleDescription");
			this.txtUser.AccessibleName = resources.GetString("txtUser.AccessibleName");
			this.txtUser.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("txtUser.Anchor")));
			this.txtUser.AutoSize = ((bool)(resources.GetObject("txtUser.AutoSize")));
			this.txtUser.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(227)), ((System.Byte)(227)), ((System.Byte)(227)));
			this.txtUser.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("txtUser.BackgroundImage")));
			this.txtUser.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.txtUser.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("txtUser.Dock")));
			this.txtUser.Enabled = ((bool)(resources.GetObject("txtUser.Enabled")));
			this.txtUser.Font = ((System.Drawing.Font)(resources.GetObject("txtUser.Font")));
			this.txtUser.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("txtUser.ImeMode")));
			this.txtUser.Location = ((System.Drawing.Point)(resources.GetObject("txtUser.Location")));
			this.txtUser.MaxLength = ((int)(resources.GetObject("txtUser.MaxLength")));
			this.txtUser.Multiline = ((bool)(resources.GetObject("txtUser.Multiline")));
			this.txtUser.Name = "txtUser";
			this.txtUser.PasswordChar = ((char)(resources.GetObject("txtUser.PasswordChar")));
			this.txtUser.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("txtUser.RightToLeft")));
			this.txtUser.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("txtUser.ScrollBars")));
			this.txtUser.Size = ((System.Drawing.Size)(resources.GetObject("txtUser.Size")));
			this.txtUser.TabIndex = ((int)(resources.GetObject("txtUser.TabIndex")));
			this.txtUser.Text = resources.GetString("txtUser.Text");
			this.txtUser.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("txtUser.TextAlign")));
			this.txtUser.Visible = ((bool)(resources.GetObject("txtUser.Visible")));
			this.txtUser.WordWrap = ((bool)(resources.GetObject("txtUser.WordWrap")));
			this.txtUser.Enter += new System.EventHandler(this.txtUser_Enter);
			// 
			// txtPwd
			// 
			this.txtPwd.AccessibleDescription = resources.GetString("txtPwd.AccessibleDescription");
			this.txtPwd.AccessibleName = resources.GetString("txtPwd.AccessibleName");
			this.txtPwd.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("txtPwd.Anchor")));
			this.txtPwd.AutoSize = ((bool)(resources.GetObject("txtPwd.AutoSize")));
			this.txtPwd.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(227)), ((System.Byte)(227)), ((System.Byte)(227)));
			this.txtPwd.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("txtPwd.BackgroundImage")));
			this.txtPwd.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.txtPwd.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("txtPwd.Dock")));
			this.txtPwd.Enabled = ((bool)(resources.GetObject("txtPwd.Enabled")));
			this.txtPwd.Font = ((System.Drawing.Font)(resources.GetObject("txtPwd.Font")));
			this.txtPwd.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("txtPwd.ImeMode")));
			this.txtPwd.Location = ((System.Drawing.Point)(resources.GetObject("txtPwd.Location")));
			this.txtPwd.MaxLength = ((int)(resources.GetObject("txtPwd.MaxLength")));
			this.txtPwd.Multiline = ((bool)(resources.GetObject("txtPwd.Multiline")));
			this.txtPwd.Name = "txtPwd";
			this.txtPwd.PasswordChar = ((char)(resources.GetObject("txtPwd.PasswordChar")));
			this.txtPwd.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("txtPwd.RightToLeft")));
			this.txtPwd.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("txtPwd.ScrollBars")));
			this.txtPwd.Size = ((System.Drawing.Size)(resources.GetObject("txtPwd.Size")));
			this.txtPwd.TabIndex = ((int)(resources.GetObject("txtPwd.TabIndex")));
			this.txtPwd.Text = resources.GetString("txtPwd.Text");
			this.txtPwd.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("txtPwd.TextAlign")));
			this.txtPwd.Visible = ((bool)(resources.GetObject("txtPwd.Visible")));
			this.txtPwd.WordWrap = ((bool)(resources.GetObject("txtPwd.WordWrap")));
			this.txtPwd.Enter += new System.EventHandler(this.txtPwd_Enter);
			// 
			// lblPassword
			// 
			this.lblPassword.AccessibleDescription = resources.GetString("lblPassword.AccessibleDescription");
			this.lblPassword.AccessibleName = resources.GetString("lblPassword.AccessibleName");
			this.lblPassword.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblPassword.Anchor")));
			this.lblPassword.AutoSize = ((bool)(resources.GetObject("lblPassword.AutoSize")));
			this.lblPassword.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblPassword.Dock")));
			this.lblPassword.Enabled = ((bool)(resources.GetObject("lblPassword.Enabled")));
			this.lblPassword.Font = ((System.Drawing.Font)(resources.GetObject("lblPassword.Font")));
			this.lblPassword.ForeColor = System.Drawing.Color.Black;
			this.lblPassword.Image = ((System.Drawing.Image)(resources.GetObject("lblPassword.Image")));
			this.lblPassword.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblPassword.ImageAlign")));
			this.lblPassword.ImageIndex = ((int)(resources.GetObject("lblPassword.ImageIndex")));
			this.lblPassword.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblPassword.ImeMode")));
			this.lblPassword.Location = ((System.Drawing.Point)(resources.GetObject("lblPassword.Location")));
			this.lblPassword.Name = "lblPassword";
			this.lblPassword.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblPassword.RightToLeft")));
			this.lblPassword.Size = ((System.Drawing.Size)(resources.GetObject("lblPassword.Size")));
			this.lblPassword.TabIndex = ((int)(resources.GetObject("lblPassword.TabIndex")));
			this.lblPassword.Text = resources.GetString("lblPassword.Text");
			this.lblPassword.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblPassword.TextAlign")));
			this.lblPassword.Visible = ((bool)(resources.GetObject("lblPassword.Visible")));
			// 
			// ptrLogin
			// 
			this.ptrLogin.AccessibleDescription = resources.GetString("ptrLogin.AccessibleDescription");
			this.ptrLogin.AccessibleName = resources.GetString("ptrLogin.AccessibleName");
			this.ptrLogin.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("ptrLogin.Anchor")));
			this.ptrLogin.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ptrLogin.BackgroundImage")));
			this.ptrLogin.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("ptrLogin.Dock")));
			this.ptrLogin.Enabled = ((bool)(resources.GetObject("ptrLogin.Enabled")));
			this.ptrLogin.Font = ((System.Drawing.Font)(resources.GetObject("ptrLogin.Font")));
			this.ptrLogin.Image = ((System.Drawing.Image)(resources.GetObject("ptrLogin.Image")));
			this.ptrLogin.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("ptrLogin.ImeMode")));
			this.ptrLogin.Location = ((System.Drawing.Point)(resources.GetObject("ptrLogin.Location")));
			this.ptrLogin.Name = "ptrLogin";
			this.ptrLogin.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("ptrLogin.RightToLeft")));
			this.ptrLogin.Size = ((System.Drawing.Size)(resources.GetObject("ptrLogin.Size")));
			this.ptrLogin.SizeMode = ((System.Windows.Forms.PictureBoxSizeMode)(resources.GetObject("ptrLogin.SizeMode")));
			this.ptrLogin.TabIndex = ((int)(resources.GetObject("ptrLogin.TabIndex")));
			this.ptrLogin.TabStop = false;
			this.ptrLogin.Text = resources.GetString("ptrLogin.Text");
			this.ptrLogin.Visible = ((bool)(resources.GetObject("ptrLogin.Visible")));
			// 
			// ptbBox
			// 
			this.ptbBox.AccessibleDescription = resources.GetString("ptbBox.AccessibleDescription");
			this.ptbBox.AccessibleName = resources.GetString("ptbBox.AccessibleName");
			this.ptbBox.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("ptbBox.Anchor")));
			this.ptbBox.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ptbBox.BackgroundImage")));
			this.ptbBox.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("ptbBox.Dock")));
			this.ptbBox.Enabled = ((bool)(resources.GetObject("ptbBox.Enabled")));
			this.ptbBox.Font = ((System.Drawing.Font)(resources.GetObject("ptbBox.Font")));
			this.ptbBox.Image = ((System.Drawing.Image)(resources.GetObject("ptbBox.Image")));
			this.ptbBox.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("ptbBox.ImeMode")));
			this.ptbBox.Location = ((System.Drawing.Point)(resources.GetObject("ptbBox.Location")));
			this.ptbBox.Name = "ptbBox";
			this.ptbBox.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("ptbBox.RightToLeft")));
			this.ptbBox.Size = ((System.Drawing.Size)(resources.GetObject("ptbBox.Size")));
			this.ptbBox.SizeMode = ((System.Windows.Forms.PictureBoxSizeMode)(resources.GetObject("ptbBox.SizeMode")));
			this.ptbBox.TabIndex = ((int)(resources.GetObject("ptbBox.TabIndex")));
			this.ptbBox.TabStop = false;
			this.ptbBox.Text = resources.GetString("ptbBox.Text");
			this.ptbBox.Visible = ((bool)(resources.GetObject("ptbBox.Visible")));
			// 
			// pictureBox1
			// 
			this.pictureBox1.AccessibleDescription = resources.GetString("pictureBox1.AccessibleDescription");
			this.pictureBox1.AccessibleName = resources.GetString("pictureBox1.AccessibleName");
			this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("pictureBox1.Anchor")));
			this.pictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
			this.pictureBox1.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("pictureBox1.Dock")));
			this.pictureBox1.Enabled = ((bool)(resources.GetObject("pictureBox1.Enabled")));
			this.pictureBox1.Font = ((System.Drawing.Font)(resources.GetObject("pictureBox1.Font")));
			this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
			this.pictureBox1.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("pictureBox1.ImeMode")));
			this.pictureBox1.Location = ((System.Drawing.Point)(resources.GetObject("pictureBox1.Location")));
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("pictureBox1.RightToLeft")));
			this.pictureBox1.Size = ((System.Drawing.Size)(resources.GetObject("pictureBox1.Size")));
			this.pictureBox1.SizeMode = ((System.Windows.Forms.PictureBoxSizeMode)(resources.GetObject("pictureBox1.SizeMode")));
			this.pictureBox1.TabIndex = ((int)(resources.GetObject("pictureBox1.TabIndex")));
			this.pictureBox1.TabStop = false;
			this.pictureBox1.Text = resources.GetString("pictureBox1.Text");
			this.pictureBox1.Visible = ((bool)(resources.GetObject("pictureBox1.Visible")));
			// 
			// btnOK
			// 
			this.btnOK.AccessibleDescription = resources.GetString("btnOK.AccessibleDescription");
			this.btnOK.AccessibleName = resources.GetString("btnOK.AccessibleName");
			this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnOK.Anchor")));
			this.btnOK.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnOK.BackgroundImage")));
			this.btnOK.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnOK.Dock")));
			this.btnOK.Enabled = ((bool)(resources.GetObject("btnOK.Enabled")));
			this.btnOK.Font = ((System.Drawing.Font)(resources.GetObject("btnOK.Font")));
			this.btnOK.Image = ((System.Drawing.Image)(resources.GetObject("btnOK.Image")));
			this.btnOK.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnOK.ImeMode")));
			this.btnOK.Location = ((System.Drawing.Point)(resources.GetObject("btnOK.Location")));
			this.btnOK.Name = "btnOK";
			this.btnOK.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnOK.RightToLeft")));
			this.btnOK.Size = ((System.Drawing.Size)(resources.GetObject("btnOK.Size")));
			this.btnOK.SizeMode = ((System.Windows.Forms.PictureBoxSizeMode)(resources.GetObject("btnOK.SizeMode")));
			this.btnOK.TabIndex = ((int)(resources.GetObject("btnOK.TabIndex")));
			this.btnOK.TabStop = false;
			this.btnOK.Text = resources.GetString("btnOK.Text");
			this.btnOK.Visible = ((bool)(resources.GetObject("btnOK.Visible")));
			this.btnOK.Click += new System.EventHandler(this.btnOk_Click);
			this.btnOK.MouseEnter += new System.EventHandler(this.btnOK_MouseEnter);
			this.btnOK.MouseLeave += new System.EventHandler(this.btnOK_MouseLeave);
			// 
			// btnCancel
			// 
			this.btnCancel.AccessibleDescription = resources.GetString("btnCancel.AccessibleDescription");
			this.btnCancel.AccessibleName = resources.GetString("btnCancel.AccessibleName");
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnCancel.Anchor")));
			this.btnCancel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCancel.BackgroundImage")));
			this.btnCancel.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnCancel.Dock")));
			this.btnCancel.Enabled = ((bool)(resources.GetObject("btnCancel.Enabled")));
			this.btnCancel.Font = ((System.Drawing.Font)(resources.GetObject("btnCancel.Font")));
			this.btnCancel.Image = ((System.Drawing.Image)(resources.GetObject("btnCancel.Image")));
			this.btnCancel.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnCancel.ImeMode")));
			this.btnCancel.Location = ((System.Drawing.Point)(resources.GetObject("btnCancel.Location")));
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnCancel.RightToLeft")));
			this.btnCancel.Size = ((System.Drawing.Size)(resources.GetObject("btnCancel.Size")));
			this.btnCancel.SizeMode = ((System.Windows.Forms.PictureBoxSizeMode)(resources.GetObject("btnCancel.SizeMode")));
			this.btnCancel.TabIndex = ((int)(resources.GetObject("btnCancel.TabIndex")));
			this.btnCancel.TabStop = false;
			this.btnCancel.Text = resources.GetString("btnCancel.Text");
			this.btnCancel.Visible = ((bool)(resources.GetObject("btnCancel.Visible")));
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			this.btnCancel.MouseEnter += new System.EventHandler(this.btnCancel_MouseEnter);
			this.btnCancel.MouseLeave += new System.EventHandler(this.btnCancel_MouseLeave);
			// 
			// btnOK1
			// 
			this.btnOK1.AccessibleDescription = resources.GetString("btnOK1.AccessibleDescription");
			this.btnOK1.AccessibleName = resources.GetString("btnOK1.AccessibleName");
			this.btnOK1.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnOK1.Anchor")));
			this.btnOK1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnOK1.BackgroundImage")));
			this.btnOK1.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnOK1.Dock")));
			this.btnOK1.Enabled = ((bool)(resources.GetObject("btnOK1.Enabled")));
			this.btnOK1.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnOK1.FlatStyle")));
			this.btnOK1.Font = ((System.Drawing.Font)(resources.GetObject("btnOK1.Font")));
			this.btnOK1.Image = ((System.Drawing.Image)(resources.GetObject("btnOK1.Image")));
			this.btnOK1.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnOK1.ImageAlign")));
			this.btnOK1.ImageIndex = ((int)(resources.GetObject("btnOK1.ImageIndex")));
			this.btnOK1.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnOK1.ImeMode")));
			this.btnOK1.Location = ((System.Drawing.Point)(resources.GetObject("btnOK1.Location")));
			this.btnOK1.Name = "btnOK1";
			this.btnOK1.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnOK1.RightToLeft")));
			this.btnOK1.Size = ((System.Drawing.Size)(resources.GetObject("btnOK1.Size")));
			this.btnOK1.TabIndex = ((int)(resources.GetObject("btnOK1.TabIndex")));
			this.btnOK1.Text = resources.GetString("btnOK1.Text");
			this.btnOK1.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnOK1.TextAlign")));
			this.btnOK1.Visible = ((bool)(resources.GetObject("btnOK1.Visible")));
			this.btnOK1.Click += new System.EventHandler(this.btnOk_Click);
			this.btnOK1.Enter += new System.EventHandler(this.btnOK1_Enter);
			this.btnOK1.Leave += new System.EventHandler(this.btnOK1_Leave);
			// 
			// btnCancel1
			// 
			this.btnCancel1.AccessibleDescription = resources.GetString("btnCancel1.AccessibleDescription");
			this.btnCancel1.AccessibleName = resources.GetString("btnCancel1.AccessibleName");
			this.btnCancel1.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnCancel1.Anchor")));
			this.btnCancel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCancel1.BackgroundImage")));
			this.btnCancel1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel1.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnCancel1.Dock")));
			this.btnCancel1.Enabled = ((bool)(resources.GetObject("btnCancel1.Enabled")));
			this.btnCancel1.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnCancel1.FlatStyle")));
			this.btnCancel1.Font = ((System.Drawing.Font)(resources.GetObject("btnCancel1.Font")));
			this.btnCancel1.Image = ((System.Drawing.Image)(resources.GetObject("btnCancel1.Image")));
			this.btnCancel1.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnCancel1.ImageAlign")));
			this.btnCancel1.ImageIndex = ((int)(resources.GetObject("btnCancel1.ImageIndex")));
			this.btnCancel1.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnCancel1.ImeMode")));
			this.btnCancel1.Location = ((System.Drawing.Point)(resources.GetObject("btnCancel1.Location")));
			this.btnCancel1.Name = "btnCancel1";
			this.btnCancel1.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnCancel1.RightToLeft")));
			this.btnCancel1.Size = ((System.Drawing.Size)(resources.GetObject("btnCancel1.Size")));
			this.btnCancel1.TabIndex = ((int)(resources.GetObject("btnCancel1.TabIndex")));
			this.btnCancel1.Text = resources.GetString("btnCancel1.Text");
			this.btnCancel1.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnCancel1.TextAlign")));
			this.btnCancel1.Visible = ((bool)(resources.GetObject("btnCancel1.Visible")));
			this.btnCancel1.Click += new System.EventHandler(this.btnCancel_Click);
			this.btnCancel1.Enter += new System.EventHandler(this.btnCancel1_Enter);
			this.btnCancel1.Leave += new System.EventHandler(this.btnCancel1_Leave);
			// 
			// imlImage
			// 
			this.imlImage.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
			this.imlImage.ImageSize = ((System.Drawing.Size)(resources.GetObject("imlImage.ImageSize")));
			this.imlImage.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imlImage.ImageStream")));
			this.imlImage.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// Login
			// 
			this.AcceptButton = this.btnOK1;
			this.AccessibleDescription = resources.GetString("$this.AccessibleDescription");
			this.AccessibleName = resources.GetString("$this.AccessibleName");
			this.AutoScaleBaseSize = ((System.Drawing.Size)(resources.GetObject("$this.AutoScaleBaseSize")));
			this.AutoScroll = ((bool)(resources.GetObject("$this.AutoScroll")));
			this.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("$this.AutoScrollMargin")));
			this.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("$this.AutoScrollMinSize")));
			this.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(234)), ((System.Byte)(234)), ((System.Byte)(234)));
			this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
			this.CancelButton = this.btnCancel1;
			this.ClientSize = ((System.Drawing.Size)(resources.GetObject("$this.ClientSize")));
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.ptrLogin);
			this.Controls.Add(this.lblPassword);
			this.Controls.Add(this.txtPwd);
			this.Controls.Add(this.txtUser);
			this.Controls.Add(this.lblUserName);
			this.Controls.Add(this.ptbBox);
			this.Controls.Add(this.pictureBox1);
			this.Controls.Add(this.btnOK);
			this.Controls.Add(this.btnCancel1);
			this.Controls.Add(this.btnOK1);
			this.Enabled = ((bool)(resources.GetObject("$this.Enabled")));
			this.Font = ((System.Drawing.Font)(resources.GetObject("$this.Font")));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("$this.ImeMode")));
			this.KeyPreview = true;
			this.Location = ((System.Drawing.Point)(resources.GetObject("$this.Location")));
			this.MaximizeBox = false;
			this.MaximumSize = ((System.Drawing.Size)(resources.GetObject("$this.MaximumSize")));
			this.MinimizeBox = false;
			this.MinimumSize = ((System.Drawing.Size)(resources.GetObject("$this.MinimumSize")));
			this.Name = "Login";
			this.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("$this.RightToLeft")));
			this.StartPosition = ((System.Windows.Forms.FormStartPosition)(resources.GetObject("$this.StartPosition")));
			this.Text = resources.GetString("$this.Text");
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Login_KeyDown);
			this.Load += new System.EventHandler(this.Login_Load);
			this.Closed += new System.EventHandler(this.Login_Closed);
			this.ResumeLayout(false);

		}

		#endregion

		#region Class's Method

		#region Change backcolor when focus or lost focus
		
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
			const string METHOD_NAME = THIS + ". OnLeaveControl()";
			try
			{
				FormControlComponents.OnLeaveControl(sender, e);
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

		private bool CheckMandatory(Control pobjControl)
		{
			if (pobjControl.Text.Trim() == string.Empty)
			{
				return false;
			}
			return true;
		}
		
		private bool ValidateData() 
		{
			// check data in textboxes
			if (!CheckMandatory(txtUser))
			{
				PCSMessageBox.Show(ErrorCode.LOGIN_MANDATORY_INVALID);
				txtUser.Focus();
				txtUser.SelectAll();
				return false;					
			}
			if (!CheckMandatory(txtPwd))
			{
				PCSMessageBox.Show(ErrorCode.LOGIN_MANDATORY_INVALID);
				txtPwd.Focus();
				txtPwd.SelectAll();
				return false;
			}
			return true;
		}
		
		private Sys_UserVO CheckAuthenticate(Sys_UserVO pobjObjecVO)
		{
			CommonBO boCommon = new CommonBO();
			try
			{
							
				Sys_UserVO sysUserVO= new Sys_UserVO();
				object obj = (object) pobjObjecVO;
				sysUserVO=(Sys_UserVO)boCommon.CheckAuthenticate(obj);
								
				return sysUserVO;
			}
			catch (PCSException ex)
			{
				throw ex;
			}
			catch (Exception ex1)
			{
				throw ex1;
			}

		}
		
		#endregion Class's Method
		
		#region Event Processing

		private void Login_Load(object sender, System.EventArgs e)
		{
		    Icon = SystemProperty.FormIcon;
		}		
		
		/// <summary>
		/// Event for Ok click
		/// If user's information is correct then login system else then messages
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <Author> Tuan DM, Jan 10, 2005</Author>
		private void btnOk_Click(object sender, System.EventArgs e)
		{
			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.WaitCursor;

			#endregion Code Inserted Automatically

			
			const string METHOD_NAME = THIS + "btnOk_Click()";
			const string HOME_CURRENCY = "HomeCurrency";
			const string EMPLOYEE_NAME = "EmployeeName";
			const string MASTERLOCATIONCODE = "MasterLocationCode";
			const string MASTERLOCATIONNAME = "MasterLocationName";
			const string PCSMAIN = "PCSMain.Exe";

			pObjobjecVO = new Sys_UserVO();
			
            if (ValidateData())
			{
				pObjobjecVO.UserName = txtUser.Text.Trim();
				pObjobjecVO.Pwd = txtPwd.Text.Trim();
				try
				{
                    DataSet dstData = (new UtilsBO()).GetDefaultInfomation();
                    ErrorMessage.dsErrorMessage = dstData.Tables[Sys_Error_MsgTable.TABLE_NAME];

					Sys_UserVO sysUserVO = new Sys_UserVO();				
					sysUserVO= CheckAuthenticate(pObjobjecVO);
					CommonBO boCommon = new CommonBO();
					// HACK: Trada 14-12-2005
					DataSet dstRoleAndParam = boCommon.ListRoleAndSysParam(sysUserVO.UserID, pObjobjecVO.UserName);
					//check if no-right assigned
					if (dstRoleAndParam.Tables[0].Rows.Count == 0)
					{
						PCSMessageBox.Show(ErrorCode.MESSAGE_NORIGHT_LOGIN,MessageBoxButtons.OK);
						txtUser.SelectionStart = 0;
						txtUser.SelectionLength = txtUser.Text.Length;
						txtUser.Focus();
						// Code Inserted Automatically
                        #region Code Inserted Automatically
this.Cursor = Cursors.Default;
#endregion Code Inserted Automatically

						return;
					}

					pObjobjecVO = new Sys_UserVO();
					pObjobjecVO = sysUserVO;
					SystemProperty.ExecutablePath = Application.StartupPath;
					SystemProperty.UserID = pObjobjecVO.UserID;
					SystemProperty.UserName = pObjobjecVO.UserName;
					// Get the first Role
					SystemProperty.RoleID = int.Parse(dstRoleAndParam.Tables[0].Rows[0][Sys_RoleTable.ROLEID_FLD].ToString());
					//Set SystemProperties
					DataRow drowMST_CCN = dstRoleAndParam.Tables[1].Rows[0];
					SystemProperty.CCNID = int.Parse(drowMST_CCN[MST_CCNTable.CCNID_FLD].ToString());
					SystemProperty.CCNCode = drowMST_CCN[MST_CCNTable.CODE_FLD].ToString();
					SystemProperty.CCNDescription = drowMST_CCN[MST_CCNTable.DESCRIPTION_FLD].ToString();
                    
					if (drowMST_CCN[MST_CCNTable.CITYID_FLD].ToString() != string.Empty)
					{
						SystemProperty.CityID = int.Parse(drowMST_CCN[MST_CCNTable.CITYID_FLD].ToString());
					}
					else 
						SystemProperty.CityID = 0;
					SystemProperty.Code = drowMST_CCN[MST_CCNTable.CODE_FLD].ToString();
					if (drowMST_CCN[MST_CCNTable.CITYID_FLD].ToString() != string.Empty)
					{
						SystemProperty.CountryID = int.Parse(drowMST_CCN[MST_CCNTable.COUNTRYID_FLD].ToString());
					}
					else 
						SystemProperty.CountryID = 0;
					if (drowMST_CCN[MST_CCNTable.DEFAULTCURRENCYID_FLD].ToString() != string.Empty)
					{
						SystemProperty.DefaultCurrencyID = int.Parse(drowMST_CCN[MST_CCNTable.DEFAULTCURRENCYID_FLD].ToString());
					}
					SystemProperty.Description = drowMST_CCN[MST_CCNTable.DESCRIPTION_FLD].ToString();
					SystemProperty.Email = drowMST_CCN[MST_CCNTable.EMAIL_FLD].ToString();
					if (drowMST_CCN[MST_CCNTable.EXCHANGERATE_FLD].ToString() != string.Empty)
					{
						SystemProperty.ExchangeRate = float.Parse(drowMST_CCN[MST_CCNTable.EXCHANGERATE_FLD].ToString());
					}
					else 
						SystemProperty.ExchangeRate = 0;
					SystemProperty.ExchangeRateOperator = drowMST_CCN[MST_CCNTable.EXCHANGERATEOPERATOR_FLD].ToString();
					SystemProperty.Fax = drowMST_CCN[MST_CCNTable.FAX_FLD].ToString();
					if (drowMST_CCN[MST_CCNTable.HOMECURRENCYID_FLD].ToString() != string.Empty)
					{
						SystemProperty.HomeCurrencyID = int.Parse(drowMST_CCN[MST_CCNTable.HOMECURRENCYID_FLD].ToString());
					}
					else
						SystemProperty.HomeCurrencyID = 0;
					SystemProperty.HomeCurrency = drowMST_CCN[HOME_CURRENCY].ToString();
					SystemProperty.Name = drowMST_CCN[MST_CCNTable.NAME_FLD].ToString();
					SystemProperty.Phone = drowMST_CCN[MST_CCNTable.PHONE_FLD].ToString();
					SystemProperty.State = drowMST_CCN[MST_CCNTable.STATE_FLD].ToString();
					SystemProperty.VAT = drowMST_CCN[MST_CCNTable.VAT_FLD].ToString();
					SystemProperty.WebSite = drowMST_CCN[MST_CCNTable.WEBSITE_FLD].ToString();
					SystemProperty.ZipCode = drowMST_CCN[MST_CCNTable.ZIPCODE_FLD].ToString();
					if (drowMST_CCN[MST_EmployeeTable.EMPLOYEEID_FLD].ToString() != string.Empty)
					{
						SystemProperty.EmployeeID = int.Parse(drowMST_CCN[MST_EmployeeTable.EMPLOYEEID_FLD].ToString());
					}
					else
						SystemProperty.EmployeeID = 0;
					SystemProperty.EmployeeName = drowMST_CCN[EMPLOYEE_NAME].ToString();
					SystemProperty.MasterLocationID = int.Parse(drowMST_CCN[MST_MasterLocationTable.MASTERLOCATIONID_FLD].ToString());
					SystemProperty.MasterLocationCode = drowMST_CCN[MASTERLOCATIONCODE].ToString();
					SystemProperty.MasterLocationName = drowMST_CCN[MASTERLOCATIONNAME].ToString();
					// HACK: dungla 10-21-2005
					// get all system parameters
					NameValueCollection objParam = new NameValueCollection();
					// put to Collection
					foreach (DataRow drowData in dstRoleAndParam.Tables[2].Rows)
						objParam.Add(drowData[Sys_ParamTable.NAME_FLD].ToString().Trim(), drowData[Sys_ParamTable.VALUE_FLD].ToString().Trim());
					SystemProperty.SytemParams = objParam;
					// set the report logo file name
					SystemProperty.LogoFile = Application.StartupPath + @"\logo.jpg";
					// END: dungla 10-21-2005
					this.DialogResult = DialogResult.OK;

					// HACKED: 23/05/2006 Thachnn : Delete Temporary PCS Report File
					string mstrReportDefFolder = Application.StartupPath + "\\" +Constants.REPORT_DEFINITION_STORE_LOCATION;					
					FormControlComponents.DeletePCSTempReportFile(mstrReportDefFolder);					
					// ENDHACKED: 23/05/2006 Thachnn : Delete Temporary PCS Report File

					this.Close();
				}
				catch (PCSException ex)
				{
					PCSMessageBox.Show(ex.mCode);
					try
					{
						Logger.LogMessage(ex.CauseException, METHOD_NAME, Level.ERROR);
					}
					catch
					{
						PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION);
					}
					txtUser.SelectionStart = 0;
					txtUser.SelectionLength = txtUser.Text.Length;
					txtUser.Focus();
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

			
			Application.Exit();

			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.Default;

			#endregion Code Inserted Automatically

			
		}

		private void Login_Closed(object sender, System.EventArgs e)
		{
			if (this.DialogResult != DialogResult.OK)
			{
				Application.Exit();
			}
		}
		
		private void btnOK_MouseEnter(object sender, System.EventArgs e)
		{
			btnOK.Image = imlImage.Images[1];
			btnCancel.Image = imlImage.Images[2];
		}

		private void btnOK1_Enter(object sender, System.EventArgs e)
		{
			btnOK.Image = imlImage.Images[1];
		}

		private void btnCancel1_Enter(object sender, System.EventArgs e)
		{
			btnCancel.Image = imlImage.Images[3];
			btnOK.Image = imlImage.Images[0];
		}

		private void txtUser_Enter(object sender, System.EventArgs e)
		{
			btnCancel.Image = imlImage.Images[2];
			txtUser.SelectionStart = 0;
			txtUser.SelectionLength = txtUser.Text.Length;
		}

		private void btnOK_MouseLeave(object sender, System.EventArgs e)
		{
			btnOK.Image = imlImage.Images[0];	
		}

		private void btnCancel_MouseEnter(object sender, System.EventArgs e)
		{
			btnCancel.Image = imlImage.Images[3];
			btnOK.Image = imlImage.Images[0];
		}

		private void btnCancel_MouseLeave(object sender, System.EventArgs e)
		{
			//btnCancel.Image = imlImage.Images[2];
		}

		private void btnOK1_Leave(object sender, System.EventArgs e)
		{
			btnOK.Image = imlImage.Images[0];
		}

		private void btnCancel1_Leave(object sender, System.EventArgs e)
		{
			btnCancel.Image = imlImage.Images[2];
		}

		private void txtPwd_Enter(object sender, System.EventArgs e)
		{
			txtPwd.SelectionStart = 0;
			txtPwd.SelectionLength = txtPwd.Text.Length;
		}

		private void Login_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if ((e.KeyCode == Keys.F1) && (e.Modifiers == Keys.Control))
			{
				ConnectionMaintainance frmConnMaintainance = new ConnectionMaintainance();
				frmConnMaintainance.ShowDialog(this);
			}
		}

		#endregion Event Processing

	}
}
					