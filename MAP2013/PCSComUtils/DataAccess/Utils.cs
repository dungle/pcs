using System.Configuration;
using PCSComUtils.Common;
using System.Data.SqlClient;
using System.Data.OleDb;

namespace PCSComUtils.DataAccess
{
	/// <summary>
	/// Summary description for Utils.
	/// </summary>	
	public class Utils 
	{
        private static readonly object SyncRoot = new object();
        private static Utils _instance;
        private string _connectionString = string.Empty;
        private string _oleConnectionString = string.Empty;
        private string _otherConnectionString = string.Empty;

        public Utils()
        {
            _connectionString = GetConnectionString();
            _oleConnectionString = GetOleConnectionString();
            _otherConnectionString = GetOtherDbConnectionString();
        }

        /// <summary>
        ///     Gets Utils instance (Singleton)
        /// </summary>
        public static Utils Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (SyncRoot)
                    {
                        if (_instance == null)
                            _instance = new Utils();
                    }
                }
                return _instance;
            }
        }

        /// <summary>
        ///     Gets ERP Connection string
        /// </summary>
        public string ConnectionString
        {
            get
            {
                return _connectionString;
            }
        }

        /// <summary>
        ///     Gets ERP Connection string for OleDbConnection
        /// </summary>
        public string OleDbConnectionString
        {
            get
            {
                return _oleConnectionString;
            }
        }

        /// <summary>
        ///     Get other Db connection string
        /// </summary>
        public string OtherDbConnectionString
        {
            get
            {
                return _otherConnectionString;
            }
        }

        /// <summary>
        ///     Gets Connection String from AppConfig for ERP connection
        /// </summary>
        /// <returns>Connection String</returns>
        private static string GetConnectionString()
        {
            // get the application configuration file.
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            // load connection string
            ConnectionStringSettings dbSettings = config.ConnectionStrings.ConnectionStrings[Constants.ERP_CONNECTION];
            if (dbSettings != null)
            {
                if (string.IsNullOrEmpty(dbSettings.ConnectionString))
                    return string.Empty;

                var connStringBuilder = new SqlConnectionStringBuilder(dbSettings.ConnectionString);

                string password = connStringBuilder["Password"].ToString();

                if (password.StartsWith("@"))
                {
                    password = CryptoUtil.Decrypt(password.Substring(1, password.Length - 1));
                }
                connStringBuilder["Password"] = password;
                return connStringBuilder.ConnectionString;
            }
            return string.Empty;
        }

        private static string GetOleConnectionString()
        {
            // get the application configuration file.
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            // load EffectDB connection string
            ConnectionStringSettings dbSettings = config.ConnectionStrings.ConnectionStrings[Constants.OLE_CONNECTION];
            if (dbSettings != null)
            {
                if (string.IsNullOrEmpty(dbSettings.ConnectionString))
                    return string.Empty;

                var connStringBuilder = new OleDbConnectionStringBuilder(dbSettings.ConnectionString);

                string password = connStringBuilder["Password"].ToString();

                if (password.StartsWith("@"))
                {
                    password = CryptoUtil.Decrypt(password.Substring(1, password.Length - 1));
                }
                connStringBuilder["Password"] = password;
                return connStringBuilder.ConnectionString;
            }
            return string.Empty;
        }

        /// <summary>
        ///     Gets Connection String from AppConfig for Other Db connection
        /// </summary>
        /// <returns>Connection String</returns>
        private static string GetOtherDbConnectionString()
        {
            // get the application configuration file.
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            // load EffectDB connection string
            ConnectionStringSettings otherDbSettings =
                config.ConnectionStrings.ConnectionStrings[Constants.OTHERDBB_CONNECTION];
            if (otherDbSettings != null)
            {
                if (string.IsNullOrEmpty(otherDbSettings.ConnectionString))
                    return string.Empty;

                var connStringBuilder = new OleDbConnectionStringBuilder(otherDbSettings.ConnectionString);

                string password = connStringBuilder["Password"].ToString();

                if (password.StartsWith("@"))
                {
                    password = CryptoUtil.Decrypt(password.Substring(1, password.Length - 1));
                }
                connStringBuilder["Password"] = password;
                return connStringBuilder.ConnectionString;
            }
            return string.Empty;
        }

        public void ChangeConnection()
        {
            _connectionString = GetConnectionString();
            _oleConnectionString = GetOleConnectionString();
            _otherConnectionString = GetOtherDbConnectionString();
        }

        /// <summary>
        ///     Max row for sale order invoice report
        /// </summary>
	    public int MaxRowSaleOrderInvoiceReport
	    {
	        get
	        {
	            var maxRowValue = ConfigurationManager.AppSettings["maxRowSaleOrderInvoice"];
	            int maxRow;
	            if (!int.TryParse(maxRowValue, out maxRow) || maxRow < 1)
	            {
	                maxRow = 15;
	            }
	            return maxRow;
	        }
	    }
	}
}
