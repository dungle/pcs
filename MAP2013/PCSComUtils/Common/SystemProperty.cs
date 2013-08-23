using System.Collections;
using System.Collections.Specialized;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using PCSComUtils.DataContext;

namespace PCSComUtils.Common
{
    /// <summary>
    /// Summary description for SystemProperty.
    /// </summary>
    public class SystemProperty
    {
        /// <summary>
        /// Name of Application
        /// </summary>
        public static string ApplicationName;

        public static Icon FormIcon = new Icon("App.ico");

        /// <summary>
        /// Keep form's handler
        /// </summary>
        public static ArrayList ArrayForms;

        /// <summary>
        /// Default CCNCode of PCS
        /// </summary>
        public static string CCNCode;

        public static string CCNDescription;

        /// <summary>
        /// Default CCNID of PCS
        /// </summary>
        public static int CCNID;

        /// <summary>
        /// Default Chart of account struct for current session
        /// </summary>
        public static int ChartOfAccountStructID;

        public static int CityID;

        public static string Code;
        public static int CountryID;
        public static string DefaultCurrency;
        public static int DefaultCurrencyID;
        public static string Description;
        public static string Email;
        public static int EmployeeID;
        public static string EmployeeName;

        public static float ExchangeRate;
        public static string ExchangeRateOperator;
        public static string ExecutablePath = string.Empty;
        public static string Fax;
        public static string HomeCurrency;
        public static int HomeCurrencyID;
        // HACK: dungla 10-21-2005

        /// <summary>
        /// Gets the logo file name
        /// </summary>
        public static string LogoFile;

        public static string MasterLocationCode;

        /// <summary>
        /// Default MasterLocationID
        /// </summary>
        public static int MasterLocationID;

        public static string MasterLocationName;
        public static string Name;
        public static string Phone;
        public static int RoleID;
        public static string State;

        /// <summary>
        /// Store all system paramters
        /// </summary>
        public static NameValueCollection SytemParams;

        // END: dungla 10-21-2005
        //Added By Tuan TQ -- 05 May 2005
        //public static DateTime CurrentPeriodFromDate;
        //public static DateTime CurrentPeriodToDate;

        public static List<Sys_Menu_Entry> TableMenuEntry;

        /// <summary>
        /// Collection of Temporary tables used by current user session
        /// </summary>
        public static List<string> TempTables;

        public static int UserID;
        public static string UserName = string.Empty;
        public static string UserPassword = string.Empty;
        public static string VAT;
        public static string WebSite;
        public static string ZipCode;
    }

    public class FormInfo
    {
        public Form mForm;
        public string mPrefix;
        public string mTableName;
        public string mTransFormat;
        public string mTransNoFieldName;
        public string mUserName;

        public FormInfo(Form pForm, string pstrPrefix, string pstrTransFormat, string pstrTableName,
                        string pstrTransNoFieldName, string pstrUserName)
        {
            mForm = pForm;
            mPrefix = pstrPrefix;
            mTransFormat = pstrTransFormat;
            mTableName = pstrTableName;
            mTransNoFieldName = pstrTransNoFieldName;
            mUserName = pstrUserName;
        }
    }
}