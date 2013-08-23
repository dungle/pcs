using System;
using System.Collections;
using System.Data;
using System.Globalization;
using System.Transactions;
using PCSComMaterials.Inventory.DS;
using PCSComMaterials.Plan.DS;
using PCSComProduct.Items.DS;
using PCSComUtils.Common;
using PCSComUtils.Common.BO;
using PCSComUtils.Common.DS;

using PCSComUtils.MasterSetup.DS;
using PCSComUtils.PCSExc;

namespace PCSComMaterials.Plan.BO
{
    public class MPSRegenerationForKMPCompanyBO
    {
        public DataSet GetList(int iMPSCycleOptionMasterIDID)
        {
            MPSRegenerationForKMPCompanyDS objDs = new MPSRegenerationForKMPCompanyDS();
            return objDs.GetList(iMPSCycleOptionMasterIDID);
        }
    }
}
