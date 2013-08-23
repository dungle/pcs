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
                BaoCaoTongHopSanXuat.BaoCaoTongHopSanXuat report = new BaoCaoTongHopSanXuat.BaoCaoTongHopSanXuat();
                report.PCSConnectionString = "Provider=SQLOLEDB;Data Source=.;User ID=sa;Password=khongbiet;Initial Catalog=MAP";
                var startupPath = Application.StartupPath;
                startupPath = startupPath.Replace(@"bin\Debug", "ReportDefinition");
                report.ReportDefinitionFolder = startupPath;
                //report.ReportDefinitionFolder = @"I:\iMAS\PCSTestReport\PCSTestReport\ReportDefinition";
                DateTime month = new DateTime(2011, 1, 1);
                var data = report.ExecuteReport(month.ToString(), "", "", "111", "", "", "");
                GridView.DataSource = data;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
