using System;
using System.Collections.Generic;
using System.Globalization;
using PCSComUtils.Common;
using PCSComUtils.DataContext;

namespace PCSMain
{
    public partial class MainScreen : PCSUtils.BaseForm
    {
        private List<Sys_Menu_Entry> menuList = new List<Sys_Menu_Entry>();    
        public MainScreen()
        {
            InitializeComponent();
        }

        private void MainScreen_Load(object sender, EventArgs e)
        {
            menuList = !SystemProperty.UserName.ToLower().Equals(Constants.SUPER_ADMIN_USER) ? 
                boCommon.GetAllMenusWithImageFields(CultureInfo.CurrentUICulture, SystemProperty.UserName)
                : obj.GetMenuAllWithImageFields();
        }
    }
}
