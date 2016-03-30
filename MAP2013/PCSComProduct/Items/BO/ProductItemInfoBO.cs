using System;
using System.Data;
using PCSComUtils.PCSExc;
using PCSComUtils.Common;
using PCSComProduct.Items.DS;
using PCSComUtils.MasterSetup.DS;
using PCSComUtils.Common.BO;

namespace PCSComProduct.Items.BO
{
	public class ProductItemInfoBO //: IProductItemInfoBO
	{
		private const string THIS = "PCSComProduct.Items.BO.ProductItemInfoBO";
		
		/// <summary>
		/// Update LTVariableTime property of product after updating routing info and return LTVariableTime value
		/// </summary>
		/// <param name="piProductID"></param>
		/// <returns>LTVariableTime value</returns>
		/// <author> Tuan TQ, 09 Jan, 2006. Apply proposal number: 3339</author>
	
		public decimal UpdateLTVariableTimeAndReturn(int piProductID)
		{
			ITM_ProductDS objITM_ProductDS = new ITM_ProductDS();

			return objITM_ProductDS.UpdateLTVariableTimeAndReturn(piProductID);
		}
		
	
		public string GetACAdjustCodeByID(int pintID)
		{
			ITM_ProductDS objITM_ProductDS = new ITM_ProductDS();

			return objITM_ProductDS.GetACAdjustCodeByID(pintID);
		}

		public int AddAndReturnID(object pObjectDetail, int copiedProductId)
		{
            if (ValidateBusiness(pObjectDetail))
            {
                ITM_ProductDS objITM_ProductDS = new ITM_ProductDS();
                int intNewlyAddedProductID = objITM_ProductDS.AddAndReturnID(pObjectDetail);
                if (copiedProductId > 0)
                {

                    //we have to copy its BOM, routing, and hearachy
                    //1.Copy from BOM
                    ITM_BOMDS dsITM_BOMDS = new ITM_BOMDS();
                    dsITM_BOMDS.CopyBOM(copiedProductId, intNewlyAddedProductID);
                    //2.Copy from Routing
                    ITM_RoutingDS dsITM_RoutingDS = new ITM_RoutingDS();
                    dsITM_RoutingDS.CopyRouting(copiedProductId, intNewlyAddedProductID);
                    //3.Copy hearachy
                    ITM_HierarchyDS dsITM_HierarchyDS = new ITM_HierarchyDS();
                    dsITM_HierarchyDS.CopyHierarchy(copiedProductId, intNewlyAddedProductID);

                }
                return intNewlyAddedProductID;
            }
            return -1;
        }

		/// <summary>
		/// Check for unique Stock Taking Code
		/// </summary>
		/// <param name="pstrStockTakingCode">Code to check</param>
		/// <returns>true if unique, false if failure</returns>
	
        public bool CheckUniqueStockTakingCode(int pintProductID, string pstrStockTakingCode)
        {
        	ITM_ProductDS dsProduct = new ITM_ProductDS();
			return dsProduct.CheckUniqueStockTakingCode(pintProductID, pstrStockTakingCode);
        }
	    public object GetProductInfo(int pintID)
		{
            ITM_ProductDS objITM_ProductDS = new ITM_ProductDS();
            return objITM_ProductDS.GetProductInfo(pintID);
        }
	
		public void Update(object pObjectDetail)
		{
            if (ValidateBusiness(pObjectDetail))
            {
                ITM_ProductDS objITM_ProductDS = new ITM_ProductDS();
                objITM_ProductDS.UpdateProductInfo(pObjectDetail);
            }
        }
	    public bool isTwoUnitOfMeasureScalled(int pintUnitID1, int pintUnitID2)
		{
            MST_UMRateDS objMST_UMRateDS = new MST_UMRateDS();
            return objMST_UMRateDS.isTwoUnitOfMeasureScalled(pintUnitID1, pintUnitID2);
        }
        
		public string GetCategoryCodeByProductID (int pintProductID)
		{
			ITM_ProductDS dsProduct = new ITM_ProductDS();
			return dsProduct.GetCategoryCodeByProductID(pintProductID);
		}

		#region IProductItemInfoBO Members

		public DataTable GetUnitOfMeasure()
		{
            MST_UnitOfMeasureDS objMST_UnitOfMeasureDS = new MST_UnitOfMeasureDS();

            DataTable dt = objMST_UnitOfMeasureDS.List().Tables[0];
            DataRow drEmptyRow = dt.NewRow();
            dt.Rows.InsertAt(drEmptyRow, 0);
            return dt;
        }

		public DataTable GetProductType()
		{
            ITM_ProductDS dsProduct = new ITM_ProductDS();
            DataSet dts = dsProduct.GetProductType();

            if (dts.Tables.Count > 0)
            {
                return dts.Tables[0];
            }
		    return null;
		}

		#region HACK: Tuan TQ - 04 Apr, 2006
		
		/// <summary>
		/// Get Cost method from database
		/// </summary>
		/// <returns></returns>
		/// <Author> Tuan TQ, 04 Apr, 2006</Author>
	
		public DataTable GetCostMethod()
		{
			ITM_ProductDS dsProduct = new ITM_ProductDS();
			return dsProduct.GetCostMethod();
		}

		#endregion

		public DataTable GetAGC()
		{
			MST_AGCDS objMST_AGCDS = new MST_AGCDS();
            DataTable dt = objMST_AGCDS.List().Tables[0];
            DataRow drEmptyRow = dt.NewRow();
            dt.Rows.InsertAt(drEmptyRow, 0);
            return dt;
        }

		public DataTable GetQAStatus()
		{
			const string ID_FIELD = "ID";
			const string VALUE_FIELD = "VALUE";

            DataTable dtQAStatus = new DataTable();
            dtQAStatus.Columns.Add(ID_FIELD);
            dtQAStatus.Columns.Add(VALUE_FIELD);
            DataRow drNewRow = dtQAStatus.NewRow();

            drNewRow = dtQAStatus.NewRow();
            dtQAStatus.Rows.Add(drNewRow);

            drNewRow = dtQAStatus.NewRow();
            drNewRow[ID_FIELD] = "1";
            drNewRow[VALUE_FIELD] = "not source quality assured and requires inspection";
            dtQAStatus.Rows.Add(drNewRow);

            drNewRow = dtQAStatus.NewRow();
            drNewRow[ID_FIELD] = "2";
            drNewRow[VALUE_FIELD] = "not source quality assured but does not require inspection";
            dtQAStatus.Rows.Add(drNewRow);

            drNewRow = dtQAStatus.NewRow();

            drNewRow[ID_FIELD] = "3";
            drNewRow[VALUE_FIELD] = "source quality assured";
            dtQAStatus.Rows.Add(drNewRow);

            return dtQAStatus;
        }

		public DataTable GetCategory()
		{
            ITM_CategoryDS objITM_CategoryDS = new ITM_CategoryDS();
            DataTable dt = objITM_CategoryDS.ListForProduct().Tables[0];
            DataRow drEmptyRow = dt.NewRow();
            dt.Rows.InsertAt(drEmptyRow, 0);
            return dt;
        }
		public DataTable GetSource()
		{
            ITM_SourceDS objITM_SourceDS = new ITM_SourceDS();
            DataTable dt = objITM_SourceDS.List().Tables[0];
            DataRow drEmptyRow = dt.NewRow();
            dt.Rows.InsertAt(drEmptyRow, 0);

            return dt;
        }

		public DataTable GetHarzard()
		{
            ITM_HazardDS objITM_HazardDS = new ITM_HazardDS();
            DataTable dt = objITM_HazardDS.List().Tables[0];
            DataRow drEmptyRow = dt.NewRow();
            dt.Rows.InsertAt(drEmptyRow, 0);

            return dt;
        }

		public DataTable GetFreightClass()
		{
            ITM_FreightClassDS objITM_FreightClassDS = new ITM_FreightClassDS();
            DataTable dt = objITM_FreightClassDS.List().Tables[0];
            DataRow drEmptyRow = dt.NewRow();
            dt.Rows.InsertAt(drEmptyRow, 0);

            return dt;
        }
		public DataTable GetDeleteReason()
		{
            ITM_DeleteReasonDS objITM_DeleteReasonDS = new ITM_DeleteReasonDS();
            DataTable dt = objITM_DeleteReasonDS.List().Tables[0];
            DataRow drEmptyRow = dt.NewRow();
            dt.Rows.InsertAt(drEmptyRow, 0);

            return dt;
        }

		public DataTable GetFormatCodes()
		{
            ITM_FormatCodeDS objITM_FormatCodeDS = new ITM_FormatCodeDS();

            DataTable dt = objITM_FormatCodeDS.List().Tables[0];
            DataRow drEmptyRow = dt.NewRow();
            dt.Rows.InsertAt(drEmptyRow, 0);

            return dt;
        }
		public DataTable GetCCN()
		{
            UtilsBO objUtilsBO = new UtilsBO();
            return objUtilsBO.ListCCN().Tables[0];
        }
	
		public DataTable GetDeliveryPolicy()
		{
            ITM_DeliveryPolicyDS objITM_DeliveryPolicyDS = new ITM_DeliveryPolicyDS();

            DataTable dt = objITM_DeliveryPolicyDS.List().Tables[0];
            DataRow drEmptyRow = dt.NewRow();
            dt.Rows.InsertAt(drEmptyRow, 0);

            return dt;
        }
		public DataTable GetOrderPolicy()
		{
            ITM_OrderPolicyDS objITM_OrderPolicyDS = new ITM_OrderPolicyDS();

            DataTable dt = objITM_OrderPolicyDS.List().Tables[0];
            DataRow drEmptyRow = dt.NewRow();
            dt.Rows.InsertAt(drEmptyRow, 0);

            return dt;
        }
		public DataTable GetShipTolerence()
		{
            ITM_ShipToleranceDS objITM_ShipToleranceDS = new ITM_ShipToleranceDS();

            DataTable dt = objITM_ShipToleranceDS.List().Tables[0];
            DataRow drEmptyRow = dt.NewRow();
            dt.Rows.InsertAt(drEmptyRow, 0);
            
            return dt;
        }
	
		public DataTable GetBuyer()
		{
            ITM_BuyerDS objITM_BuyerDS = new ITM_BuyerDS();

            DataTable dt = objITM_BuyerDS.List().Tables[0];
            DataRow drEmptyRow = dt.NewRow();
            dt.Rows.InsertAt(drEmptyRow, 0);

            return dt;
        }
	
		public DataTable GetVendorLocation()
		{
            MST_PartyLocationDS objMST_PartyLocationDS = new MST_PartyLocationDS();

            DataTable dt = objMST_PartyLocationDS.ListForCombo().Tables[0];
            DataRow drEmptyRow = dt.NewRow();
            dt.Rows.InsertAt(drEmptyRow, 0);
            
            return dt;
        }

		public DataTable GetOrderRule()
		{
            ITM_OrderRuleDS objITM_OrderRuleDS = new ITM_OrderRuleDS();

            DataTable dt = objITM_OrderRuleDS.List().Tables[0];
            DataRow drEmptyRow = dt.NewRow();
            dt.Rows.InsertAt(drEmptyRow, 0);

            return dt;
        }

	    public DataSet GetItemGroupAndClassified()
	    {
            ITM_ProductDS dsProduct = new ITM_ProductDS();
            return dsProduct.GetItemGroupAndClassified();
        }
	
		public DataSet GetLocation()
		{
            DataSet dstLocation = new DataSet();
            DataTable dt;
            DataRow drEmptyRow;
            //get Master Location
            MST_MasterLocationDS objMST_MasterLocationDS = new MST_MasterLocationDS();
            dt = objMST_MasterLocationDS.List().Tables[0].Copy();
            drEmptyRow = dt.NewRow();
            dt.Rows.InsertAt(drEmptyRow, 0);
            dstLocation.Tables.Add(dt);

            //Get Location
            MST_LocationDS objMST_LocationDS = new MST_LocationDS();
            dt = objMST_LocationDS.List().Tables[0].Copy();
            drEmptyRow = dt.NewRow();
            dt.Rows.InsertAt(drEmptyRow, 0);
            dstLocation.Tables.Add(dt);

            //Get Bin
            MST_BINDS objMST_BINDS = new MST_BINDS();
            dt = objMST_BINDS.List().Tables[0].Copy();
            drEmptyRow = dt.NewRow();
            dt.Rows.InsertAt(drEmptyRow, 0);
            dstLocation.Tables.Add(dt);

            return dstLocation;
        }
	
		private bool ValidateBusiness(object pObjectDetail) 
		{
			const string METHOD_NAME = THIS + ".ValidateBusiness()";
            ITM_ProductVO objITM_ProductVO = (ITM_ProductVO)pObjectDetail;
            if (objITM_ProductVO.CategoryID > 0)
            {
                //Check this category.
                //This category must be a leaf node 
                ITM_CategoryDS objITM_CategoryDS = new ITM_CategoryDS();
                if (!objITM_CategoryDS.IsLeafNode(objITM_ProductVO.CategoryID))
                {
                    throw new PCSBOException(ErrorCode.MSG_PRODUCTINFO_CATEGORY_NOTALEAFNODE, METHOD_NAME, null);
                }
            }
            return true;
        }
	
		public void DeleteProduct(int pintProductID)
		{
            ITM_ProductDS objITM_ProductDS = new ITM_ProductDS();
            objITM_ProductDS.Delete(pintProductID);
        }
	
		public string GetVendorCodeAndName(int pintVendorID)
		{
            MST_PartyDS objMST_PartyDS = new MST_PartyDS();
            return objMST_PartyDS.GetPartyCodeAndName(pintVendorID);
        }

		/// <summary>
		/// Get Currency Code 
		/// </summary>
		/// <param name="pintCurencyID"></param>
		/// <returns></returns>
	
		public string GetCurrencyCode(int pintCurencyID)
		{
            MST_CurrencyVO voCurrency = (MST_CurrencyVO)(new MST_CurrencyDS()).GetObjectVO(pintCurencyID);
            return voCurrency != null ? voCurrency.Code : string.Empty;
		}


        /// <summary>
        /// Get Currency Code 
        /// </summary>
        /// <param name="pintCurencyID"></param>
        /// <returns></returns>

        public string GetInventorCode(int pintInventorID)
		{
            MST_PartyVO voParty = (MST_PartyVO)(new MST_PartyDS()).GetObjectVO(pintInventorID);
            if (voParty != null)
            {
                return voParty.Code;
            }

            return string.Empty;
        }

		public int GetVendorID(string pstrVendorCode)
		{
            MST_PartyDS objMST_PartyDS = new MST_PartyDS();
            return objMST_PartyDS.GetPartyID(pstrVendorCode);
        }
	
		public bool HasVendorDeliverySchedule(int pintID)
		{
			ITM_ProductDS dsProduct = new ITM_ProductDS();
			return dsProduct.HasVendorDeliverySchedule(pintID);
		}

		#endregion
        
		#region HACKED: Thachnn: 01 / 03 / 2006 : add DB function to create data for ItemInformationReport


		/// <summary>
		/// Get report data for specific Product.
		/// When execute the SQL string, data adapter will return 5 table, Meta, Stock,BOm, Routing, StandardCost.
		/// We will name these table with provided names (get from Parameters).
		/// <author>Thachhnn: 01 03 2006 </author>
		/// </summary>
		/// <param name="pnProductID"Which Product to get info></param>
		/// <param name="pstrMetaDataTableName"></param>
		/// <param name="pstrStockStatusTableName"></param>
		/// <param name="pstrBOMTableName"></param>
		/// <param name="pstrStandardCostTableName"></param>
		/// <returns>DataSet with multiple datatable. This dataset will contain MetaDataTable, StockStatus, BOM, Routing, StandardCost</returns>		
		public DataSet GetItemInformationData(int pnProductID, 
			string pstrMetaDataTableName, string pstrStockStatusTableName, string pstrBOMTableName, string pstrRoutingTableName, string pstrStandardCostTableName)
		{

			ITM_ProductDS objITM_ProductDS = new ITM_ProductDS();
			return objITM_ProductDS.GetItemInformationData(pnProductID, 
				pstrMetaDataTableName, pstrStockStatusTableName, pstrBOMTableName, pstrRoutingTableName, pstrStandardCostTableName);		
		}

		
		#endregion ENDHACKED: Thachnn: 01 / 03 / 2006 : add DB function to create data for ItemInformationReport
	}
}
