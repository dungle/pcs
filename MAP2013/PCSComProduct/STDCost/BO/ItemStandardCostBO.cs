using System;
using System.Data;



//using PCS's namespace

using PCSComUtils.Common;
using PCSComUtils.PCSExc;
using PCSComProduct.STDCost.DS;

namespace PCSComProduct.STDCost.BO
{
	
	public interface IItemStandardCostBO 
	{
		void DeleteByProduct(int pintProductID);
		void Delete(int pintItemCostID);
		DataTable GetItemCostDetail(int pintProductID);
		object GetObjectVO(int pintID);
	}

	/// <summary>
	/// Summary description for .{}
	/// </summary>
	
	
	public class ItemStandardCostBO :IItemStandardCostBO
	{
		private const string THIS = "PCSComProduct.STDCost.BO.ItemStandardCostBO";
		public ItemStandardCostBO()
		{
			//
			// TODO: Add constructor logic here
			//
		}


	
		public void Add(object pobjDetail)
		{			
			CST_STDItemCostDS dsItemCost = new CST_STDItemCostDS();
			dsItemCost.Add(pobjDetail);
		}
		
	
		public void Delete(object pobjDetail)
		{

		}

	
		public void Delete(int pintItemCostID)
		{
			CST_STDItemCostDS dsItemCost = new CST_STDItemCostDS();
			dsItemCost.Delete(pintItemCostID);
		}

	
		public void DeleteByProduct(int pintProductID)
		{
			CST_STDItemCostDS dsItemCost = new CST_STDItemCostDS();
			dsItemCost.DeleteByProduct(pintProductID);
		}
		
	
		public object GetObjectVO(int pintID, string pstrClass)
		{
			return null;
		}

	
		public object GetObjectVO(int pintID)
		{
			CST_STDItemCostDS dsItemCost = new CST_STDItemCostDS();
			return  dsItemCost.GetObjectVO(pintID);			
		}

	
		public DataTable GetProductItemInfo(int pintProductID)
		{
			CST_STDItemCostDS dsItemCost = new CST_STDItemCostDS();
			return  dsItemCost.GetProductItemInfo(pintProductID);			
		}
		
	
		public DataTable GetItemCostDetail(int pintProductID)
		{
			CST_STDItemCostDS dsItemCost = new CST_STDItemCostDS();
			return dsItemCost.GetItemCostDetail(pintProductID);
		}

	
		public void Update(object pobjDetail)
		{
			CST_STDItemCostDS dsItemCost = new CST_STDItemCostDS();
			dsItemCost.Update(pobjDetail);
		}
		
	
		public void UpdateDataSet(DataSet dstData)
		{			
			CST_STDItemCostDS dsItemCost = new CST_STDItemCostDS();
			dsItemCost.UpdateDataSet(dstData);
		}
	}
}
