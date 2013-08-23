using System;
using System.Collections;
using System.Data;
using PCSComProduct.Items.DS;
using PCSComUtils.Common;
using PCSComUtils.MasterSetup.DS;
using PCSComUtils.PCSExc;

namespace PCSComProduct.Items.BO
{
	public class BomBO
	{
		#region IObjectBO Members

		public void UpdateDataSet(DataSet dstData)
		{
            ITM_BOMDS dsBOM = new ITM_BOMDS();
            dsBOM.UpdateDataSet(dstData);
		}

		#endregion

		public object GetObjectVOForBOM(int pintID)
		{
			try
			{
				return new ITM_ProductDS().GetObjectVOForBOM(pintID);
			}
			catch (PCSDBException ex)
			{
				throw ex;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public object GetObjectUM(int pintUMID)
		{
			try
			{
				return new MST_UnitOfMeasureDS().GetObjectVO(pintUMID);
			}
			catch (PCSDBException ex)
			{
				throw ex;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public DataTable ListRoutingOfProduct(int pintProductID)
		{
			try
			{
				return new ITM_RoutingDS().ListRoutingByProduct(pintProductID).Tables[0];
			}
			catch (PCSDBException ex)
			{
				throw ex;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		public object GetObjectVOForBOMByCode(string pstrCode)
		{
			try
			{
				return new ITM_ProductDS().GetObjectVOForBOMByCode(pstrCode);
			}
			catch (PCSDBException ex)
			{
				throw ex;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		public void UpdateAll(DataSet dstData, object pobjProduct)
		{
			// TODO:  Add BomBO.UpdateDataSet implementation
			try
			{
				//update dataset BOM
				ITM_BOMDS dsBOM = new ITM_BOMDS();
				DataSet dstBOM = new DataSet();
				dstBOM.Tables.Add(dstData.Tables[ITM_BOMTable.TABLE_NAME].Copy());
				dsBOM.UpdateDataSet(dstBOM);

				//update dataset Hierarchy
				DataSet dstHierarchy = new DataSet();
				dstHierarchy.Tables.Add(dstData.Tables[ITM_HierarchyTable.TABLE_NAME].Copy());
				ITM_HierarchyDS dsHierarchyDS = new ITM_HierarchyDS();
				dsHierarchyDS.UpdateDataSet(dstData);

				//update Product
				new ITM_ProductDS().UpdateForBom(pobjProduct);
				dstData.AcceptChanges();
			}
			catch (PCSDBException ex)
			{
				throw ex;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		public DataTable ListBOMDetailsOfProduct(int pintProductID)
		{
			try
			{
				ITM_BOMDS dsBOM = new ITM_BOMDS();
				return dsBOM.ListBomDetailOfProduct(pintProductID);
			}
			catch (PCSDBException ex)
			{
				throw ex;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		public DataTable ListHierarchyOfProduct(int pintProductID)
		{
			try
			{
				return new ITM_HierarchyDS().ListForProduct(pintProductID);
			}
			catch (PCSDBException ex)
			{
				throw ex;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		public ArrayList CheckBussinessForBOM(DataTable pdtbComponent, int pintProductID)
		{
			try
			{
				ArrayList arrReturn = new ArrayList();
				ITM_HierarchyDS dsHierarchy = new ITM_HierarchyDS();
				DataTable dtbParent = dsHierarchy.ListParentOfProduct(pintProductID);
				dsHierarchy = null;

				//compare to find a wrong component
				for (int i =0; i <pdtbComponent.Rows.Count; i++)
				{
					if (pdtbComponent.Rows[i].RowState != DataRowState.Deleted)
					{
						foreach (DataRow drow in dtbParent.Rows)
						{
							if (pdtbComponent.Rows[i][ITM_BOMTable.COMPONENTID_FLD].ToString().Trim() == drow[0].ToString().Trim())
							{
								arrReturn.Add(pdtbComponent.Rows[i][ITM_BOMTable.COMPONENTID_FLD].ToString().Trim() + ";" + i.ToString());
							}
						}
					}
				}
				return arrReturn;
			}
			catch (PCSDBException ex)
			{
				throw ex;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message, ex);
			}
		}
		public string ConditionToSearchItem(int pintCCNID, EnumAction enumAction)
		{
			string strCondition = " WHERE " + v_ITMBOM_Product.VIEW_NAME + "." + ITM_ProductTable.CCNID_FLD + " = " + pintCCNID.ToString() + " and " + v_ITMBOM_Product.VIEW_NAME + "." + ITM_ProductTable.MAKEITEM_FLD + " = 1 and "
				+ v_ITMBOM_Product.VIEW_NAME + "." + v_ITMBOM_Product.HASBOM_FLD + " = ";
			if (enumAction == EnumAction.Add)
			{
				strCondition += "0";
			}
			else
			{
				strCondition += "1";
			}
			return strCondition;
		}
		public DataSet ListComponents()
		{
			ITM_BOMDS dsBOM = new ITM_BOMDS();
			return dsBOM.ListComponents();
		}
		public DataSet ListComponents(int pintProductID)
		{
			ITM_BOMDS dsBOM = new ITM_BOMDS();
			return dsBOM.ListComponents(pintProductID);
		}
		public DataRow GetItemInfo(int pintProductID)
		{
			ITM_ProductDS dsProduct = new ITM_ProductDS();
			return dsProduct.GetItemInfo(pintProductID);
		}
	}
}