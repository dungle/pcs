using System;
using System.Data;

using System.Text;
using PCSComProduct.Items.DS;
using PCSComProduct.STDCost.DS;
using PCSComUtils.Common;

using PCSComUtils.PCSExc;

namespace PCSComProduct.STDCost.BO
{
	public class CostRollupBO
	{
		const string THIS = "PCSComProduct.STDCost.BO.CostRollupBO()";
		private const string PARENTID_FLD = "ParentID";
		private const string TYPECODE_FLD = "TypeCode";
		public CostRollupBO()
		{
			//
			// TODO: Add constructor logic here
            //
		}
		public void Add(object pObjectDetail)
		{
			// TODO:  

		}
	
		public void Delete(object pObjectVO)
		{
			// TODO:  

		}
	
		public object GetObjectVO(int pintID, string VOclass)
		{
			// TODO:  
			return null;
		}
	
		public void Update(object pObjectDetail)
		{
			// TODO:  

		}
	
		/// <summary>
		/// Update dataset
		/// </summary>
		/// <param name="dstData">Data to update</param>
	
		public void UpdateDataSet(DataSet dstData)
		{
			CST_STDItemCostDS dsItemCost = new CST_STDItemCostDS();
			dsItemCost.UpdateDataSet(dstData);
		}

	
		public void RollUp(DateTime pdtmRollupDate, int pintCCNID)
		{
			const string METHOD_NAME = THIS + ".Rollup()";
			// schema for cost table
			DataTable dtbCost = new DataTable(CST_STDItemCostTable.TABLE_NAME);
			//dtbCost.Columns.Add(new DataColumn(CST_STDItemCostTable.STDITEMCOSTID_FLD, typeof(int)));
			dtbCost.Columns.Add(new DataColumn(CST_STDItemCostTable.COST_FLD, typeof(decimal)));
			dtbCost.Columns[CST_STDItemCostTable.COST_FLD].AutoIncrement = true;
			dtbCost.Columns[CST_STDItemCostTable.COST_FLD].AutoIncrementSeed = 1;
			dtbCost.Columns[CST_STDItemCostTable.COST_FLD].AutoIncrementStep = 1;
			dtbCost.Columns.Add(new DataColumn(CST_STDItemCostTable.ROLLUPDATE_FLD, typeof(DateTime)));
			dtbCost.Columns[CST_STDItemCostTable.ROLLUPDATE_FLD].DefaultValue = pdtmRollupDate;
			dtbCost.Columns.Add(new DataColumn(CST_STDItemCostTable.PRODUCTID_FLD, typeof(int)));
			dtbCost.Columns.Add(new DataColumn(CST_STDItemCostTable.COSTELEMENTID_FLD, typeof(int)));
			// get top level item first
			DataTable dtbTopItem = GetTopLevelItem(pintCCNID);
			// get the list of leaf cost element
			DataTable dtbCostElement = GetCostElement();
			// get all element which have type is Material
			StringBuilder sbMaterialElement = new StringBuilder();
			sbMaterialElement.Append("0");
			StringBuilder sbOtherElement = new StringBuilder();
			sbOtherElement.Append("0");
			foreach (DataRow drowElement in dtbCostElement.Rows)
			{
				int intType = Convert.ToInt32(drowElement[TYPECODE_FLD]);
				switch (intType)
				{
					case (int)CostElementType.Material:
						sbMaterialElement.Append("," + drowElement[STD_CostElementTable.COSTELEMENTID_FLD].ToString());
						break;
					default:
						sbOtherElement.Append("," + drowElement[STD_CostElementTable.COSTELEMENTID_FLD].ToString());
						break;
				}
			}
			// bom structure
			DataTable dtbBOM = GetBOM(pintCCNID);
			// cost of all none make item
			DataTable dtbCostNoneMake = GetNoneMakeCost(pintCCNID);
			// cost for all item
			DataTable dtbCostFromCostCenter = GetCostFromCostCenter(pintCCNID);
			// delete old cost first, but delete cost of make item only.
			try
			{
				DeleteMakeItemCost(pintCCNID);
			}
			catch
			{
				throw new PCSBOException(ErrorCode.MESSAGE_CAN_NOT_DELETE, METHOD_NAME, null);
			}
			// start roll up
			dtbCost = RollUp(dtbTopItem, dtbCostElement, dtbCost, dtbBOM, dtbCostNoneMake, dtbCostFromCostCenter, null, sbMaterialElement.ToString(), sbOtherElement.ToString());
			// update to database
			DataSet dstData = new DataSet();
			dstData.Tables.Add(dtbCost);
			UpdateDataSet(dstData);
		}
		
		/// <summary>
		/// Get all leaf element from cost element
		/// </summary>
		/// <returns>DataTable</returns>
	
		public DataTable GetCostElement()
		{
			STD_CostElementDS dsElement = new STD_CostElementDS();
			return dsElement.ListLeafElements().Tables[0];
		}
		/// <summary>
		/// Gets top level item of system
		/// </summary>
		/// <param name="pintCCNID">CCN</param>
		/// <returns>DataTable</returns>
	
		public DataTable GetTopLevelItem(int pintCCNID)
		{
			ITM_BOMDS dsBOM = new ITM_BOMDS();
			return dsBOM.GetTopLevelItem(pintCCNID);
		}
		/// <summary>
		/// Get all BOM structure of system
		/// </summary>
		/// <param name="pintCCNID">CCN</param>
		/// <returns>DataTable</returns>
	
		public DataTable GetBOM(int pintCCNID)
		{
			ITM_BOMDS dsBOM = new ITM_BOMDS();
			return dsBOM.GetBOMStructure(pintCCNID);
		}
		/// <summary>
		/// Get cost of none make item
		/// </summary>
		/// <param name="pintCCNID">CCN</param>
		/// <returns>DataTable</returns>
	
		public DataTable GetNoneMakeCost(int pintCCNID)
		{
			CST_STDItemCostDS dsCost = new CST_STDItemCostDS();
			return dsCost.GetNoneMakeCost(pintCCNID);
		}
		/// <summary>
		/// Gets cost of all item from cost center rate
		/// </summary>
		/// <param name="pintCCNID">CCN</param>
		/// <returns></returns>
	
		public DataTable GetCostFromCostCenter(int pintCCNID)
		{
			STD_CostCenterRateMasterDS dsRateMaster = new STD_CostCenterRateMasterDS();
			return dsRateMaster.GetCostFromCostCenterRate(pintCCNID);
		}
		/// <summary>
		/// Delete cost of make item
		/// </summary>
		/// <param name="pintCCNID">CCN</param>
	
		public void DeleteMakeItemCost(int pintCCNID)
		{
			CST_STDItemCostDS dsItemCost = new CST_STDItemCostDS();
			dsItemCost.DeleteMakeItemCost(pintCCNID);
		}
		/// <summary>
		/// Roll up cost from the lowest child to top level item. This is recursive function
		/// </summary>
		/// <param name="pdtbTopItem">All Top Level Item</param>
		/// <param name="pdtbCostElements">All Cost Elements</param>
		/// <param name="pdtbItemCost">Result Table</param>
		/// <param name="pdtbBOM">BOM Structure</param>
		/// <param name="pdtbNoneMakeCost">Cost of None make Item</param>
		/// <param name="pdtbCostFromCostCente">Cost of Item from Cost Center Rate</param>
		/// <param name="drowItem">Item to roll</param>
		/// <param name="pstrMaterialElement">All element have type is Material</param>
		/// <param name="pstrOtherElement">All element which have type is not Material</param>
		/// <returns>Items Cost</returns>
		private DataTable RollUp(DataTable pdtbTopItem, DataTable pdtbCostElements, DataTable pdtbItemCost, DataTable pdtbBOM,
			DataTable pdtbNoneMakeCost, DataTable pdtbCostFromCostCente, DataRow drowItem, string pstrMaterialElement, string pstrOtherElement)
		{
			if (drowItem == null)
			{
				foreach (DataRow drowTopItem in pdtbTopItem.Rows)
				{
					// roll make item only
					if (Convert.ToBoolean(drowTopItem[ITM_ProductTable.MAKEITEM_FLD]))
						RollUp(pdtbTopItem, pdtbCostElements, pdtbItemCost, pdtbBOM, pdtbNoneMakeCost, pdtbCostFromCostCente, drowTopItem, pstrMaterialElement, pstrOtherElement);
				}
			}
			else
			{
				if (Convert.ToBoolean(drowItem[ITM_ProductTable.MAKEITEM_FLD]))
				{
					string strProductID = drowItem[ITM_ProductTable.PRODUCTID_FLD].ToString();
					string strCostCenterRateMasterID = drowItem[ITM_ProductTable.COSTCENTERRATEMASTERID_FLD].ToString();
					DataRow[] drowSubItem = pdtbBOM.Select(PARENTID_FLD + "= '" + strProductID + "'");
					foreach (DataRow drowChild in drowSubItem)
					{
						// if item is make item, roll up cost
						if (Convert.ToBoolean(drowChild[ITM_ProductTable.MAKEITEM_FLD]))
							RollUp(pdtbTopItem, pdtbCostElements, pdtbItemCost, pdtbBOM, pdtbNoneMakeCost, pdtbCostFromCostCente, drowChild, pstrMaterialElement, pstrOtherElement);
					}

					#region now calculate each cost element of current item from sub item

					foreach (DataRow drowElement in pdtbCostElements.Rows)
					{
						decimal decCost = 0, decSubCost = 0;
						string strCostElementID = drowElement[CST_STDItemCostTable.COSTELEMENTID_FLD].ToString();
						int intElementType = -1;
						try
						{
							intElementType = Convert.ToInt32(drowElement[TYPECODE_FLD]);
						}
						catch{}
						// check if we already calculate cost for current item
						DataRow[] drowExisted = pdtbItemCost.Select(ITM_ProductTable.PRODUCTID_FLD + "='" + strProductID + "'"
							+ " AND " + STD_CostElementTable.COSTELEMENTID_FLD + "='" + strCostElementID + "'");
						// already calculate, go to next element.
						if (drowExisted.Length > 0)
							continue;
						// make new row for each cost element
						DataRow drowItemCost = pdtbItemCost.NewRow();
						// productid
						drowItemCost[ITM_ProductTable.PRODUCTID_FLD] = strProductID;
						// cost element id
						drowItemCost[CST_STDItemCostTable.COSTELEMENTID_FLD] = strCostElementID;
						// roll cost element from all sub items
						foreach (DataRow drowChild in drowSubItem)
						{
							decimal decBomQuantity = 0;
							decimal decChildCost = 0;
							decimal decChildOH = 0;
							try
							{
								decBomQuantity = Convert.ToDecimal(drowChild[ITM_BOMTable.QUANTITY_FLD]);
							}
							catch{}
							// make item
							if (Convert.ToBoolean(drowChild[ITM_ProductTable.MAKEITEM_FLD]))
							{
								DataRow[] drowSubCost = pdtbItemCost.Select(ITM_ProductTable.PRODUCTID_FLD + "='"
									+ drowChild[ITM_ProductTable.PRODUCTID_FLD].ToString() + "'"
									+ " AND " + STD_CostElementTable.COSTELEMENTID_FLD + "='" + strCostElementID + "'");
								foreach (DataRow drowData in drowSubCost)
								{
									try
									{
										decChildCost += Convert.ToDecimal(drowData[CST_STDItemCostTable.COST_FLD]);
									}
									catch{}
								}
							}
							else if (intElementType == (int)CostElementType.Material)
							{
								// none make item
								// P = R (Cost from Material element type) + O (Overhead - sum of all element type <> Material)
								// Material element type
								DataRow[] drowSubCostMaterial = pdtbNoneMakeCost.Select(ITM_ProductTable.PRODUCTID_FLD + "='"
									+ drowChild[ITM_ProductTable.PRODUCTID_FLD].ToString() + "'"
									+ " AND " + STD_CostElementTable.COSTELEMENTID_FLD + " IN (" + pstrMaterialElement + ")");
								// other element type
								DataRow[] drowSubCostOther = pdtbNoneMakeCost.Select(ITM_ProductTable.PRODUCTID_FLD + "='"
									+ drowChild[ITM_ProductTable.PRODUCTID_FLD].ToString() + "'"
									+ " AND " + STD_CostElementTable.COSTELEMENTID_FLD + " IN (" + pstrOtherElement + ")");
								foreach (DataRow drowData in drowSubCostMaterial)
								{
									try
									{
										decChildCost += Convert.ToDecimal(drowData[CST_STDItemCostTable.COST_FLD]);
									}
									catch{}
								}
								foreach (DataRow drowData in drowSubCostOther)
								{
									try
									{
										decChildOH += Convert.ToDecimal(drowData[CST_STDItemCostTable.COST_FLD]);
									}
									catch{}
								}
								// P(none make) = R(none make) + O (none make)
								decChildCost = decChildCost + decChildOH;
							}
							// cost = cost * BOM quantity
							decChildCost = decChildCost * decBomQuantity;
							decSubCost += decChildCost;
						}
						// cost of current item based on cost center rate
						DataRow[] drowCostFromCostCenter = pdtbCostFromCostCente.Select(ITM_ProductTable.PRODUCTID_FLD + "='" + strProductID + "'"
							+ " AND " + STD_CostCenterRateMasterTable.COSTCENTERRATEMASTERID_FLD + "='" + strCostCenterRateMasterID + "'"
							+ " AND " + STD_CostElementTable.COSTELEMENTID_FLD + "='" + strCostElementID + "'");
						foreach (DataRow drowCostFromCenter in drowCostFromCostCenter)
						{
							try
							{
								decCost += Convert.ToDecimal(drowCostFromCenter[STD_CostCenterRateDetailTable.COST_FLD]);
							}
							catch{}
						}
						decimal decItemCost = 0;
						switch (intElementType)
						{
							case (int)CostElementType.Material:
								// CostElement(i) = BOM.Quantity * P(if child item is none make item) 
								// + BOM.Quantity * Rm(Child)(if child item is make item)
								decItemCost = decSubCost;
								break;
							default:
								// CostElement(i) = CostElement(current)(i) + BOM.Quantity * ChildCostElement(i)
								decItemCost = decCost + decSubCost;
								break;
						}
						// CostElement(i) = CostCenterRate.CostElement(i) + Child.CostElement(i) * BOM.Quantity
						drowItemCost[CST_STDItemCostTable.COST_FLD] = decItemCost;
						pdtbItemCost.Rows.Add(drowItemCost);
					}

					#endregion 
				}
			}
			// return
			return pdtbItemCost;
		}
	}
}
