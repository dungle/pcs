using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PCSTestReport
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                //NIGURI2.NIGURI2 report = new NIGURI2.NIGURI2();
                InvoiceForSaleOrderReport.InvoiceForSaleOrderReport report = new InvoiceForSaleOrderReport.InvoiceForSaleOrderReport();
                report.PCSConnectionString = "Provider=SQLOLEDB;Data Source=.;User ID=sa;Password=Niteco@123;Initial Catalog=MAP";
                var startupPath = Application.StartupPath;
                startupPath = startupPath.Replace(@"bin\Debug", "ReportDefinition");
                report.ReportDefinitionFolder = startupPath;
                DateTime month = new DateTime(2016, 1, 12);
                var data = report.ExecuteReport(month.ToString(), "", "", "", "", "", "");
                GridView.DataSource = data;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
