using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Windows.Forms;
using PCSComUtils.Common;
using PCSComUtils.DataAccess;
using PCSUtils.Log;
using PCSUtils.Utils;

namespace PCSUtils.Admin
{
    /// <summary>
    /// Summary description for ConnectionMaintainance.
    /// </summary>
    public class ConnectionMaintainance : Form
    {
        #region Constants

        private const string THIS = "PCSUtils.Admin.Login";
        const string DATASOURCE = "Data Source";
        const string INITIAL_CATALOG = "Initial Catalog";
        const string USER = "User ID";
        const string PWD = "Password";
        const string AT_SIGN = "@";

        #endregion Constants

        #region Generated Code

        private Button btnClose;
        private Button btnOK;
        private CheckBox chkEncrypt;

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private Container components;

        private Label lblDatabase;
        private Label lblEncrypt;
        private Label lblPassword;
        private Label lblServer;
        private Label lblUserID;
        private TextBox txtDatabase;
        private TextBox txtPassword;
        private TextBox txtServer;
        private Label lblNotRegistered;
        private Label lblSaveChanges;
        private Label lblMandatory;
        private Label lblSystemError;
        private Label lblSaveOK;
        private TextBox txtUserID;

        public ConnectionMaintainance()
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConnectionMaintainance));
            this.lblServer = new System.Windows.Forms.Label();
            this.lblDatabase = new System.Windows.Forms.Label();
            this.lblUserID = new System.Windows.Forms.Label();
            this.lblPassword = new System.Windows.Forms.Label();
            this.txtServer = new System.Windows.Forms.TextBox();
            this.txtDatabase = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.txtUserID = new System.Windows.Forms.TextBox();
            this.chkEncrypt = new System.Windows.Forms.CheckBox();
            this.lblEncrypt = new System.Windows.Forms.Label();
            this.lblNotRegistered = new System.Windows.Forms.Label();
            this.lblSaveChanges = new System.Windows.Forms.Label();
            this.lblMandatory = new System.Windows.Forms.Label();
            this.lblSystemError = new System.Windows.Forms.Label();
            this.lblSaveOK = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblServer
            // 
            this.lblServer.ForeColor = System.Drawing.Color.Maroon;
            resources.ApplyResources(this.lblServer, "lblServer");
            this.lblServer.Name = "lblServer";
            // 
            // lblDatabase
            // 
            this.lblDatabase.ForeColor = System.Drawing.Color.Maroon;
            resources.ApplyResources(this.lblDatabase, "lblDatabase");
            this.lblDatabase.Name = "lblDatabase";
            this.lblDatabase.Click += new System.EventHandler(this.lblDatabase_Click);
            // 
            // lblUserID
            // 
            this.lblUserID.ForeColor = System.Drawing.Color.Maroon;
            resources.ApplyResources(this.lblUserID, "lblUserID");
            this.lblUserID.Name = "lblUserID";
            // 
            // lblPassword
            // 
            this.lblPassword.ForeColor = System.Drawing.Color.Maroon;
            resources.ApplyResources(this.lblPassword, "lblPassword");
            this.lblPassword.Name = "lblPassword";
            // 
            // txtServer
            // 
            resources.ApplyResources(this.txtServer, "txtServer");
            this.txtServer.Name = "txtServer";
            this.txtServer.Enter += new System.EventHandler(this.TextBoxEnter);
            // 
            // txtDatabase
            // 
            resources.ApplyResources(this.txtDatabase, "txtDatabase");
            this.txtDatabase.Name = "txtDatabase";
            this.txtDatabase.Enter += new System.EventHandler(this.TextBoxEnter);
            // 
            // txtPassword
            // 
            resources.ApplyResources(this.txtPassword, "txtPassword");
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.TextChanged += new System.EventHandler(this.txtPassword_TextChanged);
            this.txtPassword.Enter += new System.EventHandler(this.TextBoxEnter);
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.btnClose, "btnClose");
            this.btnClose.Name = "btnClose";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnOK
            // 
            resources.ApplyResources(this.btnOK, "btnOK");
            this.btnOK.Name = "btnOK";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // txtUserID
            // 
            resources.ApplyResources(this.txtUserID, "txtUserID");
            this.txtUserID.Name = "txtUserID";
            this.txtUserID.Enter += new System.EventHandler(this.TextBoxEnter);
            // 
            // chkEncrypt
            // 
            resources.ApplyResources(this.chkEncrypt, "chkEncrypt");
            this.chkEncrypt.Name = "chkEncrypt";
            // 
            // lblEncrypt
            // 
            this.lblEncrypt.ForeColor = System.Drawing.SystemColors.ControlText;
            resources.ApplyResources(this.lblEncrypt, "lblEncrypt");
            this.lblEncrypt.Name = "lblEncrypt";
            // 
            // lblNotRegistered
            // 
            resources.ApplyResources(this.lblNotRegistered, "lblNotRegistered");
            this.lblNotRegistered.Name = "lblNotRegistered";
            // 
            // lblSaveChanges
            // 
            resources.ApplyResources(this.lblSaveChanges, "lblSaveChanges");
            this.lblSaveChanges.Name = "lblSaveChanges";
            // 
            // lblMandatory
            // 
            resources.ApplyResources(this.lblMandatory, "lblMandatory");
            this.lblMandatory.Name = "lblMandatory";
            // 
            // lblSystemError
            // 
            resources.ApplyResources(this.lblSystemError, "lblSystemError");
            this.lblSystemError.Name = "lblSystemError";
            // 
            // lblSaveOK
            // 
            this.lblSaveOK.ForeColor = System.Drawing.Color.Maroon;
            resources.ApplyResources(this.lblSaveOK, "lblSaveOK");
            this.lblSaveOK.Name = "lblSaveOK";
            // 
            // ConnectionMaintainance
            // 
            this.AcceptButton = this.btnOK;
            resources.ApplyResources(this, "$this");
            this.CancelButton = this.btnClose;
            this.Controls.Add(this.lblSaveOK);
            this.Controls.Add(this.lblMandatory);
            this.Controls.Add(this.lblSaveChanges);
            this.Controls.Add(this.lblEncrypt);
            this.Controls.Add(this.chkEncrypt);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.txtUserID);
            this.Controls.Add(this.txtDatabase);
            this.Controls.Add(this.txtServer);
            this.Controls.Add(this.lblPassword);
            this.Controls.Add(this.lblUserID);
            this.Controls.Add(this.lblDatabase);
            this.Controls.Add(this.lblServer);
            this.Controls.Add(this.lblSystemError);
            this.Controls.Add(this.lblNotRegistered);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ConnectionMaintainance";
            this.Load += new System.EventHandler(this.ConnectionMaintainance_Load);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.ConnectionMaintainance_Closing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        #endregion Generated Code

        #region Event Handlers

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, EventArgs e)
        {
            const string METHOD_NAME = THIS + "btnOK_Click()";
            try
            {
                if (CheckMandatory())
                {
                    #region Save Connection String

                    var connectionString = ConstructConnString();
                    SaveConnectionString(connectionString);

                    PCSComUtils.DataAccess.Utils.Instance.ChangeConnection();

                    #endregion

                    MessageBox.Show(lblSaveOK.Text, Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnClose.Focus();
                }
            }
            catch (Exception ex)
            {
                PCSMessageBox.Show(lblSystemError.Text, MessageBoxButtons.OK, MessageBoxIcon.Error,
                                   MessageBoxDefaultButton.Button1);
                try
                {
                    Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
                }
                catch
                {
                }
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConnectionMaintainance_Load(object sender, EventArgs e)
        {
            const string METHOD_NAME = THIS + "ConnectionMaintainance_Load()";
            try
            {
                bool isEncrypted;
                var connStrings = GetOleConnectionString(out isEncrypted);

                if (connStrings != null)
                {
                    txtServer.Text = connStrings[0];
                    txtDatabase.Text = connStrings[1];
                    txtUserID.Text = connStrings[2];
                    txtPassword.Text = connStrings[3];

                    chkEncrypt.Checked = isEncrypted;
                }
                else
                {
                    txtServer.Text = string.Empty;
                    txtDatabase.Text = string.Empty;
                    txtUserID.Text = string.Empty;
                    txtPassword.Text = string.Empty;

                    chkEncrypt.Checked = false;
                }
            }
            catch (Exception ex)
            {
                PCSMessageBox.Show(lblNotRegistered.Text, MessageBoxButtons.OK, MessageBoxIcon.Error,
                                   MessageBoxDefaultButton.Button1);
                try
                {
                    Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
                }
                catch
                {
                    PCSMessageBox.Show(lblSystemError.Text, MessageBoxButtons.OK, MessageBoxIcon.Error,
                                       MessageBoxDefaultButton.Button1);
                }
                Close();
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConnectionMaintainance_Closing(object sender, CancelEventArgs e)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBoxEnter(object sender, EventArgs e)
        {
            var txtSender = (TextBox)sender;
            txtSender.SelectAll();
        }

        #endregion Event Handlers

        #region Private Methods

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private bool CheckMandatory()
        {
            if (txtServer.Text.Trim().Equals(string.Empty))
            {
                PCSMessageBox.Show(lblMandatory.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning,
                                   MessageBoxDefaultButton.Button1);
                txtServer.Focus();
                return false;
            }
            if (txtDatabase.Text.Trim().Equals(string.Empty))
            {
                PCSMessageBox.Show(lblMandatory.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning,
                                   MessageBoxDefaultButton.Button1);
                txtServer.Focus();
                return false;
            }
            if (txtUserID.Text.Trim().Equals(string.Empty))
            {
                PCSMessageBox.Show(lblMandatory.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning,
                                   MessageBoxDefaultButton.Button1);
                txtServer.Focus();
                return false;
            }
            return true;
        }

        /// <summary>
        /// Construct oledb connection string from informations
        /// </summary>
        /// <returns></returns>
        private string ConstructConnString()
        {
            string strConnStrTemplate = "Provider=SQLOLEDB;Data Source={0};User ID={1};Password={2};Initial Catalog={3}";
            string strServer = txtServer.Text.Trim();
            string strDatabase = txtDatabase.Text.Trim();
            string strUserID = txtUserID.Text.Trim();
            string strPwd = txtPassword.Text.Trim();

            if (chkEncrypt.Checked)
            {
                strPwd = CryptoUtil.Encrypt(strPwd);
                strPwd = "@" + strPwd;
            }

            string strConnStr = string.Format(strConnStrTemplate, strServer, strUserID, strPwd, strDatabase);
            return strConnStr;
        }

        /// <summary>
        ///     Store connection string into application configuration file.
        /// </summary>
        /// <param name="connectionString"></param>
        private static void SaveConnectionString(string connectionString)
        {
            // get the application configuration file.
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            #region Sql Connection string

            var dbSettings = config.ConnectionStrings.ConnectionStrings[Constants.ERP_CONNECTION];
            var connStringBuilder = new OleDbConnectionStringBuilder(connectionString);
            string password = connStringBuilder["Password"].ToString();

            if (password.StartsWith("@"))
            {
                password = CryptoUtil.Decrypt(password.Substring(1, password.Length - 1));
            }
            connStringBuilder["Password"] = password;
            var sqlConBuilder = new SqlConnectionStringBuilder
            {
                DataSource = connStringBuilder.DataSource,
                InitialCatalog = connStringBuilder["Initial Catalog"].ToString(),
                UserID = connStringBuilder["User ID"].ToString(),
                Password = connStringBuilder["Password"].ToString()
            };
            connectionString = sqlConBuilder.ConnectionString;
            if (dbSettings == null) // create a connection string element.
            {
                dbSettings = new ConnectionStringSettings(Constants.ERP_CONNECTION, connectionString);
                // get the connection strings section.
                var csSection = config.ConnectionStrings;

                // add the new element.
                csSection.ConnectionStrings.Add(dbSettings);
            }
            else // update new connection string
                dbSettings.ConnectionString = connectionString;

            #endregion


            #region OleConnectionString

            var oleDbSettings = config.ConnectionStrings.ConnectionStrings[Constants.OLE_CONNECTION];
            if (oleDbSettings == null) // create a connection string element.
            {
                oleDbSettings = new ConnectionStringSettings(Constants.OLE_CONNECTION, connStringBuilder.ConnectionString);
                // get the connection strings section.
                var csSection = config.ConnectionStrings;

                // add the new element.
                csSection.ConnectionStrings.Add(oleDbSettings);
            }
            else // update new connection string
                oleDbSettings.ConnectionString = connStringBuilder.ConnectionString;

            #endregion
            // save the configuration file.
            config.Save(ConfigurationSaveMode.Modified);

        }

        /// <summary>
        ///     Gets connection string from app.config file
        /// </summary>
        /// <returns>return null if no settings found</returns>
        private static List<string> GetOleConnectionString(out bool isEncrypted)
        {
            isEncrypted = false;
            // get the application configuration file.
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            var dbSettings = config.ConnectionStrings.ConnectionStrings[Constants.OLE_CONNECTION];
            if (dbSettings != null)
            {
                var connectionString = new List<string>();

                if (string.IsNullOrEmpty(dbSettings.ConnectionString))
                    return null;

                var connStringBuilder = new OleDbConnectionStringBuilder(dbSettings.ConnectionString);

                var server = connStringBuilder[DATASOURCE].ToString();
                connectionString.Add(server);
                var database = connStringBuilder[INITIAL_CATALOG].ToString();
                connectionString.Add(database);
                var userId = connStringBuilder[USER].ToString();
                connectionString.Add(userId);
                var password = connStringBuilder[PWD].ToString();

                if (password.StartsWith(AT_SIGN))
                {
                    //password = CryptoUtil.Decrypt(password.Substring(1, password.Length - 1));
                    isEncrypted = true;
                }
                connectionString.Add(password);
                return connectionString;
            }
            return null;
        }

        #endregion Private Methods

        private void lblDatabase_Click(object sender, EventArgs e)
        {

        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {

        }
    }
}