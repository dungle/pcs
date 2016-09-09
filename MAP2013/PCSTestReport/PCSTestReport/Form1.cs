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
                //DestroySlipsReport.DestroySlipsReport report = new DestroySlipsReport.DestroySlipsReport();
                report.PCSConnectionString = "Provider=SQLOLEDB;Data Source=.;User ID=sa;Password=Niteco@123;Initial Catalog=TEST";
                var startupPath = Application.StartupPath;
                startupPath = startupPath.Replace(@"bin\Debug", "ReportDefinition");
                report.ReportDefinitionFolder = startupPath;
                DateTime from = new DateTime(2016, 2, 01);
                DateTime to = new DateTime(2016, 2, 29);
                DateTime day = new DateTime(2016, 9, 7);
                var data = report.ExecuteReport(day.ToString(), "77", "", "", "", "", "");
                GridView.DataSource = data;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
