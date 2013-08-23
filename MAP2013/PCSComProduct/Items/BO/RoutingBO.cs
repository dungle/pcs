using System.Data;
using PCSComProduct.Costing.DS;
using PCSComProduct.STDCost.DS;
using PCSComUtils.MasterSetup.DS;
using PCSComProduct.Items.DS;

namespace PCSComProduct.Items.BO
{
	public class RoutingBO
	{
		public void Add(object pObjectDetail)
		{
			// TODO:  Add RoutingBO.Add implementation

		}
	
		public void Delete(object pObjectVO)
		{
			// TODO:  Add RoutingBO.Delete implementation

		}
	
		public object GetObjectVO(int pintID, string VOclass)
		{
			// TODO:  Add RoutingBO.GetObjectVO implementation
			return null;
		}

		public void Update(object pObjectDetail)
		{
			// TODO:  Add RoutingBO.Update implementation
	
		}

		public void UpdateDataSet(DataSet dstData)
		{
			ITM_RoutingDS  dsRouting = new ITM_RoutingDS();
			dsRouting.UpdateDataSet(dstData);
		}

		public DataSet ListWorkCenter()
		{
			MST_WorkCenterDS dsWorkCenter = new MST_WorkCenterDS();
			return dsWorkCenter.ListForCombo();
		}
		public DataSet ListRoutingStatus()
		{
			ITM_RoutingStatusDS dsRoutingStatus = new ITM_RoutingStatusDS();
			return dsRoutingStatus.List();
		}

		public DataSet ListCostCenter()
		{
			ITM_CostCenterDS dsCostCenter = new ITM_CostCenterDS();
			return dsCostCenter.List();			
		}
		public DataSet ListRoutingByProduct(int pintProductID)
		{
			ITM_RoutingDS dsRouting = new ITM_RoutingDS();
			return dsRouting.ListRoutingByProduct(pintProductID);		
		}
		public object GetProductInfo(int pintProductID)
		{
			ITM_ProductDS dsProduct = new ITM_ProductDS();
			return dsProduct.GetObjectVO(pintProductID);				
		}
		public DataSet ListFunction()
		{
			MST_FunctionDS dsFunction = new MST_FunctionDS();
			return dsFunction.List();				
		}
		public DataSet ListParty()
		{
			MST_PartyDS dsParty = new MST_PartyDS();
			return dsParty.List();				
		}
		public void UpdateRoutingDescription(object pobjProduct)
		{
			ITM_RoutingDS  dsRouting = new ITM_RoutingDS();
			dsRouting.UpdateRoutingDescription(pobjProduct);			
		}
		public string GetProductionLineCode(int pintProductionLineID)
		{
			ITM_RoutingDS dsRouting = new ITM_RoutingDS();
			return dsRouting.GetProductionLineCode(pintProductionLineID);
		}
		public string GetProductGroupCode(int pintProductGroupID)
		{
			ITM_RoutingDS dsRouting = new ITM_RoutingDS();
			return dsRouting.GetProductGroupCode(pintProductGroupID);
		}
		public string GetCostCenterMasterCode(int pintCostCenterMasterID)
		{
			ITM_RoutingDS dsRouting = new ITM_RoutingDS();
			return dsRouting.GetCostCenterMasterCode(pintCostCenterMasterID);
		}
		public bool IsWorkCenterInProductionLine(int pintProductionLineID, int pintWorkCenterID)
		{
			ITM_RoutingDS dsRouting = new ITM_RoutingDS();
			return dsRouting.IsWorkCenterInProductionLine(pintProductionLineID, pintWorkCenterID);
		}
		public bool HasWorkCenterNotInProductionLine(int pintProductionLineID, string pstrWorkCenterIDs)
		{
			ITM_RoutingDS dsRouting = new ITM_RoutingDS();
			return dsRouting.HasWorkCenterNotInProductionLine(pintProductionLineID, pstrWorkCenterIDs);
		}
	}
}
