using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PCSMaterials.Mps
{
    public partial class CpoWrongItemDateForm : Form
    {
        /// <summary>
        /// Set data source to display on grid
        /// </summary>
        public DataTable DataSource { get; set; }

        public CpoWrongItemDateForm()
        {
            InitializeComponent();
        }
    }
}
