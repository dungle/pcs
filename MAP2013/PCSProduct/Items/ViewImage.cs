using System;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;
using C1.Win.C1Command;
using C1.Win.C1List;
using C1.C1Report;
using C1.Win.C1Input;

using PCSComProduct.Items.BO;
using PCSComProduct.Items.DS;
using PCSComUtils.PCSExc;
using PCSComUtils.Common;
using PCSUtils.Log;
using PCSUtils.Utils;
using PCSComUtils.Common.BO;
using PCSProduct.STDCost;

namespace PCSProduct.Items
{
    public partial class ViewImage : Form
    {
        private readonly Bitmap _imageSource;
        
        public ViewImage(Bitmap imageSource)
        {
            _imageSource = imageSource;
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ViewImages()
        {
            if (_imageSource != null)
            {
                ProductImage.Image = _imageSource;
                // resize the form to fit the image
                var size = new Size(_imageSource.Size.Width, _imageSource.Size.Height + 40);
                Size = size;
            }
        }

        private void ViewImage_Load(object sender, EventArgs e)
        {
            ViewImages();
        }
    }
}