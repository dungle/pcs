using System;
using System.Data;
using PCSComUtils.PCSExc;
using PCSComUtils.Common;
using PCSComProduct.Items.DS;

namespace PCSComProduct.Items.BO
{
	public class ITM_CategoryBO
	{
		private const string THIS = ".IITM_CategoryBO";		
		public void Add(object pobjObjectVO)
		{
			ITM_CategoryDS  templateDS = new ITM_CategoryDS();
			templateDS.Add(pobjObjectVO);
		}	
	
		public object GetObjectVO(int pintID,string VOclass)
		{
			const string METHOD_NAME = THIS + ".GetObjectVO()";
			throw new PCSException(ErrorCode.NOT_IMPLEMENT, METHOD_NAME,new Exception());
		}	
		public void Delete(object pObjectVO)
		{
			const string METHOD_NAME = THIS + ".Delete()";
			throw new PCSException(ErrorCode.NOT_IMPLEMENT, METHOD_NAME,new Exception());
		}	
		public void Delete(int pintID)
		{
			ITM_CategoryDS  templateDS = new ITM_CategoryDS();
			templateDS.Delete(pintID);
		}
		public object GetObjectVO(string pstrProductCode)
		{	
			ITM_CategoryDS  templateDS = new ITM_CategoryDS();
			return templateDS.GetObjectVO(pstrProductCode);
		}
		public object GetObjectVO(int pintCategoryID)
		{	
			ITM_CategoryDS  templateDS = new ITM_CategoryDS();
			return templateDS.GetObjectVO(pintCategoryID);
		}
		public void Update(object pobjObjecVO)
		{
			ITM_CategoryDS  templateDS = new ITM_CategoryDS();
			templateDS.Update(pobjObjecVO);
		}
		public DataSet List()
		{
			ITM_CategoryDS  templateDS = new ITM_CategoryDS();
			return templateDS.List();
		}
		public void UpdateDataSet(DataSet pData)
		{
			ITM_CategoryDS  templateDS = new ITM_CategoryDS();
			templateDS.UpdateDataSet(pData);
		}
		public bool CheckAddNewCategory(int pintCategoryID)
		{
			ITM_CategoryDS dsCategory = new ITM_CategoryDS();
			return dsCategory.CheckAddNewCategory(pintCategoryID);
		}
		private bool CheckCategoryUsed(int pintCategoryID)
		{
			ITM_CategoryDS dsCategory = new ITM_CategoryDS();
			return dsCategory.CheckCategoryUsed(pintCategoryID);
		}
		private bool ValidateDeleteItem(int pintCategoryID, DataSet ds)
		{
			DataRow[] drChild = ds.Tables[ITM_CategoryTable.TABLE_NAME].Select("ParentCategoryID =" + pintCategoryID.ToString().Trim());
			if (drChild.Length == 0)
			{
				if (CheckCategoryUsed(pintCategoryID))
				{
					return false;
				}
			}
			else
			{
				foreach (DataRow dr in drChild)
				{
					ValidateDeleteItem(int.Parse(dr[ITM_CategoryTable.CATEGORYID_FLD].ToString()), ds);
				}	
			}
			return true;
		}
		public void Delete(int pintCategoryID, DataSet ds)
		{
			DataRow[] drChild = ds.Tables[ITM_CategoryTable.TABLE_NAME].Select("ParentCategoryID =" + pintCategoryID.ToString().Trim());
			if (drChild.Length == 0)
			{
				Delete(pintCategoryID);
			}
			else
			{
				foreach (DataRow dr in drChild)
				{
					Delete(int.Parse(dr[ITM_CategoryTable.CATEGORYID_FLD].ToString()), ds);
				}	
			}
		}
		public void CheckAndDelete(int pintCategory)
		{
			DataSet dsListCategory = List();
			if (ValidateDeleteItem(pintCategory, dsListCategory))
			{
				Delete(pintCategory, dsListCategory);
				Delete(pintCategory);
			}
			else
			{
				throw new PCSBOException(ErrorCode.CASCADE_DELETE_PREVENT, "DeleteNode", new Exception());
			}
		}
	}
}