using System;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;
using PCSComUtils.Common;
using PCSUtils.Admin;

namespace PCSMain
{
    static class Program
    {
        private static bool _stateLogIn = true;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (_stateLogIn)
            {
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(Constants.CULTURE_EN);
            }

            var thisForm = new MainForm();
            var frmLogin = new Login();

            frmLogin.ShowDialog();
            if (frmLogin.DialogResult == DialogResult.OK)
            {
                Application.Run(thisForm);
                _stateLogIn = true;
            }
            else
            {
                Application.Exit();
            }
        }
    }
}
