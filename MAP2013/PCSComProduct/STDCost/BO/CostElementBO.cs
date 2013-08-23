using System;
using System.Data;
using PCSComUtils.Common;
using PCSComUtils.PCSExc;
using PCSComProduct.STDCost.DS;

namespace PCSComProduct.STDCost.BO
{
	public class CostElementBO
	{
		private const string THIS = "PCSComProduct.STDCost.BO.CostElementBO";
		public CostElementBO()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		/// <summary>
		/// Insert a new record into database
		/// </summary>
		public void Add(object pobjDetail)
		{
			STD_CostElementDS dsCostElement = new STD_CostElementDS();
			dsCostElement.Add(pobjDetail);
		}
		public int AddAndReturnID(object pobjDetail)
		{
			STD_CostElementDS dsCostElement = new STD_CostElementDS();
			return dsCostElement.AddAndReturnID(pobjDetail);
		}
		public void Delete(int pintID)
		{
			STD_CostElementDS dsCostElement = new STD_CostElementDS();
			dsCostElement.Delete(pintID);
		}
		public object GetObjectVO(int pintID, string VOclass)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Get the Element Type by
		/// </summary>
		public object GetElementTypeVO(int pintElementTypeID)
		{
			STD_CostElementTypeDS dsElementType = new STD_CostElementTypeDS();
			return dsElementType.GetObjectVO(pintElementTypeID);
		}

		public void Delete(object pobjDetail)
		{
			throw new NotImplementedException();
		}
		
		/// <summary>
		/// Get Cost Element by ID
		/// </summary>
		/// <param name="pintID"></param>
		/// <returns></returns>
		public object GetObjectVO(int pintID)
		{
			STD_CostElementDS dsCostElement = new STD_CostElementDS();
			return dsCostElement.GetObjectVO(pintID);
		}

		/// <summary>
		/// Get all elements
		/// </summary>
		/// <returns></returns>
		public DataSet List()
		{
			STD_CostElementDS dsCostElement = new STD_CostElementDS();
			return dsCostElement.List();
		}

		/// <summary>
		/// Get leaf elements
		/// </summary>
		/// <returns></returns>
		public DataSet ListLeafElements()
		{
			STD_CostElementDS dsCostElement = new STD_CostElementDS();
			return dsCostElement.ListLeafElements();
		}

		public DataSet ListCostElementType()
		{
			STD_CostElementTypeDS dsCostElementType = new STD_CostElementTypeDS();
			return dsCostElementType.List();
		}
		
		/// <summary>
		/// Check if cost element was used in transactions
		/// </summary>
		/// <param name="pintCostElementID"></param>
		/// <returns></returns>
		public bool IsCostElementUsed(int pintCostElementID)
		{
			STD_CostElementDS dsCostElement = new STD_CostElementDS();
			return dsCostElement.IsCostElementUsed(pintCostElementID);
		}

		public bool IsCostElementDuplicate(string pstrField, string pstrValue, int pintCostElementID)
		{
			STD_CostElementDS dsCostElement = new STD_CostElementDS();
			return dsCostElement.IsCostElementDuplicate(pstrField, pstrValue, pintCostElementID);
		}
		
		/// <summary>
		/// Check if element type is duplicate
		/// </summary>
		/// <param name="pstrValue"></param>
		/// <param name="pintCostElementID"></param>
		/// <returns></returns>
		public bool IsElementTypeDuplicate(string pstrValue, int pintCostElementID)
		{
			STD_CostElementDS dsCostElement = new STD_CostElementDS();
			return dsCostElement.IsElementTypeDuplicate(pstrValue, pintCostElementID);
		}

		/// <summary>
		/// Return the DataSet (list of record) by inputing the FieldList and Condition
		/// </summary>
		public void UpdateDataSet(DataSet pdstData)
		{
			STD_CostElementDS dsCostElement = new STD_CostElementDS();
			dsCostElement.UpdateDataSet(pdstData);
		}

		/// <summary>
		/// Update into Database
		/// </summary>
		public void Update(object pobjDetail)
		{
			STD_CostElementDS dsCostElement = new STD_CostElementDS();
			dsCostElement.Update(pobjDetail);
		}
	}
}
