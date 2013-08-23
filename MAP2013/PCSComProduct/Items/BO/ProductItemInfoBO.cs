using System;
using System.Data;
using PCSComUtils.PCSExc;


using PCSComUtils.Common;
using PCSComProduct.Items.DS;
using PCSComUtils.MasterSetup.DS;
using PCSComUtils.Common.BO;



namespace PCSComProduct.Items.BO
{
	public interface IProductItemInfoBO 
	{
		DataTable GetUnitOfMeasure();
		DataTable GetCostMethod();
		DataTable GetAGC();
		DataTable GetQAStatus();
		DataTable GetCategory();
		DataTable GetSource();
		DataTable GetHarzard();
		DataTable GetFreightClass();
		DataTable GetDeleteReason();
		DataTable GetFormatCodes();
		DataTable GetCCN();
		DataTable GetDeliveryPolicy();
		DataTable GetOrderPolicy();
		DataTable GetShipTolerence();
		DataTable GetBuyer();
		DataTable GetVendorLocation();
		DataTable GetOrderRule();
		DataTable GetProductType();
		DataSet GetLocation();
		int AddAndReturnID(object pObjectDetail, int pintCopyFromProductID);
		void DeleteProduct(int pintProductID) ;
		object GetProductInfo(int pintID);
		string GetVendorCodeAndName(int pintVendorID);
		int GetVendorID(string pstrVendorName);
		int GetProductIDByCode(string pstrCode);
		int GetProductIDByDescription(string pstrDescription);
		bool isTwoUnitOfMeasureScalled(int pintUnitID1, int pintUnitID2);
		decimal UpdateLTVariableTimeAndReturn(int piProductID);
		string GetCategoryCodeByProductID (int pintProductID);
		string GetACAdjustCodeByID(int pintID);
	}
	/// <summary>
	/// Summary description for .
	/// </summary>
	
	
	public class ProductItemInfoBO //: IProductItemInfoBO
	{
		private const string THIS = "PCSComProduct.Items.BO.ProductItemInfoBO";

		public ProductItemInfoBO()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		
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

	
		public void Add(object pObjectDetail)
		{
			// TODO:  Add ProductItemInfoBO.Add implementation

		}

		//**************************************************************************              
		///    <Description>
		///       add a new Product Item into database and return its new ID
		///    </Description>
		///    <Inputs>
		///       
		///    </Inputs>
		///    <Outputs>
		///       
		///    </Outputs>
		///    <Returns>
		///       
		///    </Returns>
		///    <Authors>
		///       THIENHD
		///    </Authors>
		///    <History>
		///       15-Dec-2004
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************

	
		public int AddAndReturnID(object pObjectDetail, int pintCopyFromProductID)
		{
			// TODO:  Add ProductItemInfoBO.Add implementation
			try
			{
				if (ValidateBusiness(pObjectDetail)) 
				{
					ITM_ProductDS objITM_ProductDS = new ITM_ProductDS();
					int intNewlyAddedProductID = objITM_ProductDS.AddAndReturnID(pObjectDetail);
					if (pintCopyFromProductID > 0)
					{

						//we have to copy its BOM, routing, and hearachy
						//1.Copy from BOM
						ITM_BOMDS dsITM_BOMDS = new ITM_BOMDS();
						dsITM_BOMDS.CopyBOM(pintCopyFromProductID, intNewlyAddedProductID);
						//2.Copy from Routing
						ITM_RoutingDS dsITM_RoutingDS = new ITM_RoutingDS();
						dsITM_RoutingDS.CopyRouting(pintCopyFromProductID, intNewlyAddedProductID);
						//3.Copy hearachy
						ITM_HierarchyDS dsITM_HierarchyDS = new ITM_HierarchyDS();
						dsITM_HierarchyDS.CopyHierarchy(pintCopyFromProductID, intNewlyAddedProductID);

					}
					return intNewlyAddedProductID;
				}
				else 
				{
					return -1;
				}
			}
			catch (PCSException ex)
			{
				throw ex;
			}
			catch (Exception ex)
			{
				throw ex;
			}
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
	
		public void Delete(object pObjectVO)
		{
			// TODO:  Add ProductItemInfoBO.Delete implementation

		}
	
	
		public object GetObjectVO(int pintID, string VOclass)
		{
			// TODO:  Add ProductItemInfoBO.GetObjectVO implementation
			return null;
		}

		//**************************************************************************              
		///    <Description>
		///       Get the Product Information from Database
		///    </Description>
		///    <Inputs>
		///       
		///    </Inputs>
		///    <Outputs>
		///       
		///    </Outputs>
		///    <Returns>
		///       
		///    </Returns>
		///    <Authors>
		///       THIENHD
		///    </Authors>
		///    <History>
		///       15-Dec-2004
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************

	
		public object GetProductInfo(int pintID)
		{
			// TODO:  Add ProductItemInfoBO.GetObjectVO implementation
			try 
			{
				ITM_ProductDS objITM_ProductDS = new ITM_ProductDS();
				return objITM_ProductDS.GetProductInfo(pintID);
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
	
		//**************************************************************************              
		///    <Description>
		///       Update the information for an existing product
		///    </Description>
		///    <Inputs>
		///       
		///    </Inputs>
		///    <Outputs>
		///       
		///    </Outputs>
		///    <Returns>
		///       
		///    </Returns>
		///    <Authors>
		///       THIENHD
		///    </Authors>
		///    <History>
		///       15-Dec-2004
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************

	
		public void Update(object pObjectDetail)
		{
			// TODO:  Add ProductItemInfoBO.Update implementation
			// TODO:  Add ProductItemInfoBO.Add implementation
			try
			{
				if (ValidateBusiness(pObjectDetail)) 
				{
					ITM_ProductDS objITM_ProductDS = new ITM_ProductDS();
					objITM_ProductDS.UpdateProductInfo(pObjectDetail);
				}
			}
			catch (PCSException ex)
			{
				throw ex;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
	
	
		public void UpdateDataSet(DataSet dstData)
		{
			// TODO:  Add ProductItemInfoBO.UpdateDataSet implementation

		}
		//**************************************************************************              
		///    <Description>
		///       Get the product ID by code
		///    </Description>
		///    <Inputs>
		///       
		///    </Inputs>
		///    <Outputs>
		///       
		///    </Outputs>
		///    <Returns>
		///       
		///    </Returns>
		///    <Authors>
		///       THIENHD
		///    </Authors>
		///    <History>
		///       15-Dec-2004
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************

	
		public int GetProductIDByCode(string pstrCode)
		{
			try 
			{
				ITM_ProductDS objITM_ProductDS = new ITM_ProductDS();
				return objITM_ProductDS.GetProductIDByCode(pstrCode);
			}
			catch (PCSException ex)
			{
				throw ex;
			}
			catch (Exception ex)
			{
				throw ex;
			}

		}
		//**************************************************************************              
		///    <Description>
		///       Get the product ID by Description
		///    </Description>
		///    <Inputs>
		///       
		///    </Inputs>
		///    <Outputs>
		///       
		///    </Outputs>
		///    <Returns>
		///       
		///    </Returns>
		///    <Authors>
		///       THIENHD
		///    </Authors>
		///    <History>
		///       15-Dec-2004
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************

	
		public int GetProductIDByDescription(string pstrDescription)
		{
			try 
			{
				ITM_ProductDS objITM_ProductDS = new ITM_ProductDS();
				return objITM_ProductDS.GetProductIDByDescription(pstrDescription);
			}
			catch (PCSException ex)
			{
				throw ex;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		//**************************************************************************              
		///    <Description>
		///       Compare two unit to know if they are scalled
		///    </Description>
		///    <Inputs>
		///       
		///    </Inputs>
		///    <Outputs>
		///       
		///    </Outputs>
		///    <Returns>
		///       
		///    </Returns>
		///    <Authors>
		///       THIENHD
		///    </Authors>
		///    <History>
		///       15-Dec-2004
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************

	
		public bool isTwoUnitOfMeasureScalled(int pintUnitID1, int pintUnitID2)
		{
			try 
			{
				MST_UMRateDS objMST_UMRateDS = new MST_UMRateDS();
				return objMST_UMRateDS.isTwoUnitOfMeasureScalled(pintUnitID1, pintUnitID2);
			}
			catch (PCSException ex)
			{
				throw ex;
			}
			catch (Exception ex)
			{
				throw ex;
			}

		}

	
		public string GetCategoryCodeByProductID (int pintProductID)
		{
			ITM_ProductDS dsProduct = new ITM_ProductDS();
			return dsProduct.GetCategoryCodeByProductID(pintProductID);
		}

		#region IProductItemInfoBO Members

		//**************************************************************************              
		///    <Description>
		///       Get list of Unit of measure
		///    </Description>
		///    <Inputs>
		///       
		///    </Inputs>
		///    <Outputs>
		///       
		///    </Outputs>
		///    <Returns>
		///       
		///    </Returns>
		///    <Authors>
		///       THIENHD
		///    </Authors>
		///    <History>
		///       15-Dec-2004
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************

	
		public DataTable GetUnitOfMeasure()
		{
			// TODO:  Add ProductItemInfoBO.GetUnitOfMeasure implementation
			try 
			{
				MST_UnitOfMeasureDS objMST_UnitOfMeasureDS = new MST_UnitOfMeasureDS();
				
				DataTable dt = objMST_UnitOfMeasureDS.List().Tables[0];
				DataRow drEmptyRow = dt.NewRow();
				dt.Rows.InsertAt(drEmptyRow,0);
				return dt;
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

	
		public DataTable GetProductType()
		{
			// TODO:  Add ProductItemInfoBO.GetUnitOfMeasure implementation
			try 
			{
				ITM_ProductDS dsProduct = new ITM_ProductDS();
				DataSet dts = dsProduct.GetProductType();

				if(dts.Tables.Count > 0)
				{
					return dts.Tables[0];
				}
				else
				{
					return null;
				}				
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

		//**************************************************************************              
		///    <Description>
		///       Get list of cost method
		///    </Description>
		///    <Inputs>
		///       
		///    </Inputs>
		///    <Outputs>
		///       
		///    </Outputs>
		///    <Returns>
		///       
		///    </Returns>
		///    <Authors>
		///       THIENHD
		///    </Authors>
		///    <History>
		///       15-Dec-2004
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************

		#region HACK: Tuan TQ - 04 Apr, 2006
		/*
		 * Del by Tuan TQ
	
		public DataTable GetCostMethod()
		{
			// TODO:  Add ProductItemInfoBO.GetCostMethod implementation
			const string ID_FIELD = "ID";
			const string VALUE_FIELD = "VALUE";

			try 
			{
				DataTable dtCostMethod = new DataTable();
				dtCostMethod.Columns.Add(ID_FIELD);
				dtCostMethod.Columns.Add(VALUE_FIELD);
				DataRow drNewRow = dtCostMethod.NewRow();
				drNewRow[ID_FIELD] = "";
				drNewRow[VALUE_FIELD] ="";
				dtCostMethod.Rows.Add(drNewRow);

				drNewRow = dtCostMethod.NewRow();
				drNewRow[ID_FIELD] = "0";
				drNewRow[VALUE_FIELD] ="ACT";
				dtCostMethod.Rows.Add(drNewRow);

				drNewRow = dtCostMethod.NewRow();
				drNewRow[ID_FIELD] = "1";
				drNewRow[VALUE_FIELD] ="STD";
				dtCostMethod.Rows.Add(drNewRow);

				drNewRow = dtCostMethod.NewRow();
				
				drNewRow[ID_FIELD] = "2";
				drNewRow[VALUE_FIELD] ="AVG";
				dtCostMethod.Rows.Add(drNewRow);

				return dtCostMethod;
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
		*/
		
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

		//**************************************************************************              
		///    <Description>
		///       Get list of Accounting group code
		///    </Description>
		///    <Inputs>
		///       
		///    </Inputs>
		///    <Outputs>
		///       
		///    </Outputs>
		///    <Returns>
		///       
		///    </Returns>
		///    <Authors>
		///       THIENHD
		///    </Authors>
		///    <History>
		///       15-Dec-2004
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************


	
		public DataTable GetAGC()
		{
			// TODO:  Add ProductItemInfoBO.GetAGC implementation
			const string ID_FIELD = "ID";
			const string VALUE_FIELD = "VALUE";

			try 
			{
				/*
				DataTable dtAGC = new DataTable();
				dtAGC.Columns.Add(ID_FIELD);
				dtAGC.Columns.Add(VALUE_FIELD);
				DataRow drNewRow = dtAGC.NewRow();
				
				drNewRow[ID_FIELD] = "1";
				drNewRow[VALUE_FIELD] ="AGC 1";
				dtAGC.Rows.Add(drNewRow);

				drNewRow = dtAGC.NewRow();
				drNewRow[ID_FIELD] = "2";
				drNewRow[VALUE_FIELD] ="AGC 2";
				dtAGC.Rows.Add(drNewRow);

				drNewRow = dtAGC.NewRow();
				
				drNewRow[ID_FIELD] = "3";
				drNewRow[VALUE_FIELD] ="AGC 3";
				dtAGC.Rows.Add(drNewRow);
				*/
				MST_AGCDS objMST_AGCDS = new MST_AGCDS();
				DataTable dt = objMST_AGCDS.List().Tables[0];
				DataRow drEmptyRow = dt.NewRow();
				dt.Rows.InsertAt(drEmptyRow,0);
				return dt;
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

		//**************************************************************************              
		///    <Description>
		///       Get list of QA status
		///    </Description>
		///    <Inputs>
		///       
		///    </Inputs>
		///    <Outputs>
		///       
		///    </Outputs>
		///    <Returns>
		///       
		///    </Returns>
		///    <Authors>
		///       THIENHD
		///    </Authors>
		///    <History>
		///       15-Dec-2004
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************


	
		public DataTable GetQAStatus()
		{
			const string ID_FIELD = "ID";
			const string VALUE_FIELD = "VALUE";

			// TODO:  Add ProductItemInfoBO.GetQAStatus implementation
			try 
			{
				DataTable dtQAStatus = new DataTable();
				dtQAStatus.Columns.Add(ID_FIELD);
				dtQAStatus.Columns.Add(VALUE_FIELD);
				DataRow drNewRow = dtQAStatus.NewRow();

				drNewRow = dtQAStatus.NewRow();
				dtQAStatus.Rows.Add(drNewRow);

				drNewRow = dtQAStatus.NewRow();
				drNewRow[ID_FIELD] = "1";
				drNewRow[VALUE_FIELD] ="not source quality assured and requires inspection";
				dtQAStatus.Rows.Add(drNewRow);

				drNewRow = dtQAStatus.NewRow();
				drNewRow[ID_FIELD] = "2";
				drNewRow[VALUE_FIELD] ="not source quality assured but does not require inspection";
				dtQAStatus.Rows.Add(drNewRow);

				drNewRow = dtQAStatus.NewRow();
				
				drNewRow[ID_FIELD] = "3";
				drNewRow[VALUE_FIELD] ="source quality assured";
				dtQAStatus.Rows.Add(drNewRow);

				return dtQAStatus;
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

		//**************************************************************************              
		///    <Description>
		///       Get list of Category
		///    </Description>
		///    <Inputs>
		///       
		///    </Inputs>
		///    <Outputs>
		///       
		///    </Outputs>
		///    <Returns>
		///       
		///    </Returns>
		///    <Authors>
		///       THIENHD
		///    </Authors>
		///    <History>
		///       15-Dec-2004
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************


	
		public DataTable GetCategory()
		{
			// TODO:  Add ProductItemInfoBO.GetCategory implementation
			try 
			{
				ITM_CategoryDS objITM_CategoryDS = new ITM_CategoryDS();
				DataTable dt = objITM_CategoryDS.ListForProduct().Tables[0];
				DataRow drEmptyRow = dt.NewRow();
				dt.Rows.InsertAt(drEmptyRow,0);
				return dt;
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

		//**************************************************************************              
		///    <Description>
		///       Get list of Source
		///    </Description>
		///    <Inputs>
		///       
		///    </Inputs>
		///    <Outputs>
		///       
		///    </Outputs>
		///    <Returns>
		///       
		///    </Returns>
		///    <Authors>
		///       THIENHD
		///    </Authors>
		///    <History>
		///       15-Dec-2004
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************


	
		public DataTable GetSource()
		{
			// TODO:  Add ProductItemInfoBO.GetSource implementation
			try 
			{
				ITM_SourceDS objITM_SourceDS = new ITM_SourceDS();
				DataTable dt = objITM_SourceDS.List().Tables[0];
				DataRow drEmptyRow = dt.NewRow();
				dt.Rows.InsertAt(drEmptyRow,0);

				return dt;
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

		//**************************************************************************              
		///    <Description>
		///       Get list of Unit of Hazard
		///    </Description>
		///    <Inputs>
		///       
		///    </Inputs>
		///    <Outputs>
		///       
		///    </Outputs>
		///    <Returns>
		///       
		///    </Returns>
		///    <Authors>
		///       THIENHD
		///    </Authors>
		///    <History>
		///       15-Dec-2004
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************


	
		public DataTable GetHarzard()
		{
			// TODO:  Add ProductItemInfoBO.GetHarzard implementation
			try 
			{
				ITM_HazardDS objITM_HazardDS = new ITM_HazardDS();
				DataTable dt = objITM_HazardDS.List().Tables[0];
				DataRow drEmptyRow = dt.NewRow();
				dt.Rows.InsertAt(drEmptyRow,0);

				return dt;
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

		//**************************************************************************              
		///    <Description>
		///       Get list of Freight Class
		///    </Description>
		///    <Inputs>
		///       
		///    </Inputs>
		///    <Outputs>
		///       
		///    </Outputs>
		///    <Returns>
		///       
		///    </Returns>
		///    <Authors>
		///       THIENHD
		///    </Authors>
		///    <History>
		///       15-Dec-2004
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************


	
		public DataTable GetFreightClass()
		{
			// TODO:  Add ProductItemInfoBO.GetFreightClass implementation
			try 
			{
				ITM_FreightClassDS objITM_FreightClassDS = new ITM_FreightClassDS();
				DataTable dt = objITM_FreightClassDS.List().Tables[0];
				DataRow drEmptyRow = dt.NewRow();
				dt.Rows.InsertAt(drEmptyRow,0);

				return dt;
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

		//**************************************************************************              
		///    <Description>
		///       Get list of Delete Reason
		///    </Description>
		///    <Inputs>
		///       
		///    </Inputs>
		///    <Outputs>
		///       
		///    </Outputs>
		///    <Returns>
		///       
		///    </Returns>
		///    <Authors>
		///       THIENHD
		///    </Authors>
		///    <History>
		///       15-Dec-2004
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************


	
		public DataTable GetDeleteReason()
		{
			// TODO:  Add ProductItemInfoBO.GetReason implementation
			try 
			{
				ITM_DeleteReasonDS objITM_DeleteReasonDS = new ITM_DeleteReasonDS();
				DataTable dt = objITM_DeleteReasonDS.List().Tables[0];
				DataRow drEmptyRow = dt.NewRow();
				dt.Rows.InsertAt(drEmptyRow,0);

				return dt;
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

		//**************************************************************************              
		///    <Description>
		///       Get list of Format Code
		///    </Description>
		///    <Inputs>
		///       
		///    </Inputs>
		///    <Outputs>
		///       
		///    </Outputs>
		///    <Returns>
		///       
		///    </Returns>
		///    <Authors>
		///       THIENHD
		///    </Authors>
		///    <History>
		///       15-Dec-2004
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************


	
		public DataTable GetFormatCodes()
		{
			// TODO:  Add ProductItemInfoBO.GetFormatCodes implementation
			try 
			{
				ITM_FormatCodeDS objITM_FormatCodeDS = new ITM_FormatCodeDS();

				DataTable dt = objITM_FormatCodeDS.List().Tables[0];
				DataRow drEmptyRow = dt.NewRow();
				dt.Rows.InsertAt(drEmptyRow,0);

				return dt;
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


		//**************************************************************************              
		///    <Description>
		///       Get list of CCN
		///    </Description>
		///    <Inputs>
		///       
		///    </Inputs>
		///    <Outputs>
		///       
		///    </Outputs>
		///    <Returns>
		///       
		///    </Returns>
		///    <Authors>
		///       THIENHD
		///    </Authors>
		///    <History>
		///       15-Dec-2004
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************


	
		public DataTable GetCCN()
		{
			// TODO:  Add ProductItemInfoBO.GetCCN implementation
			try 
			{
				//MST_CCNDS objMST_CCNDS = new MST_CCNDS();
				UtilsBO objUtilsBO = new UtilsBO();
				//return objMST_CCNDS.List().Tables[0];
				return objUtilsBO.ListCCN().Tables[0];
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

		//**************************************************************************              
		///    <Description>
		///       Get list of Delivery Policy
		///    </Description>
		///    <Inputs>
		///       
		///    </Inputs>
		///    <Outputs>
		///       
		///    </Outputs>
		///    <Returns>
		///       
		///    </Returns>
		///    <Authors>
		///       THIENHD
		///    </Authors>
		///    <History>
		///       15-Dec-2004
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************


	
		public DataTable GetDeliveryPolicy()
		{
			// TODO:  Add ProductItemInfoBO.GetDeliveryPolicy implementation
			try 
			{
				ITM_DeliveryPolicyDS objITM_DeliveryPolicyDS = new ITM_DeliveryPolicyDS();

				DataTable dt = objITM_DeliveryPolicyDS.List().Tables[0];
				DataRow drEmptyRow = dt.NewRow();
				dt.Rows.InsertAt(drEmptyRow,0);

				return dt;
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

		//**************************************************************************              
		///    <Description>
		///       Get list of GetOrderPolicy
		///    </Description>
		///    <Inputs>
		///       
		///    </Inputs>
		///    <Outputs>
		///       
		///    </Outputs>
		///    <Returns>
		///       
		///    </Returns>
		///    <Authors>
		///       THIENHD
		///    </Authors>
		///    <History>
		///       15-Dec-2004
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************


	
		public DataTable GetOrderPolicy()
		{
			// TODO:  Add ProductItemInfoBO.GetOrderPolicy implementation
			try 
			{
				ITM_OrderPolicyDS objITM_OrderPolicyDS = new ITM_OrderPolicyDS();

				DataTable dt = objITM_OrderPolicyDS.List().Tables[0];
				DataRow drEmptyRow = dt.NewRow();
				dt.Rows.InsertAt(drEmptyRow,0);


				return dt;
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

		//**************************************************************************              
		///    <Description>
		///       Get list of GetShipTolerence
		///    </Description>
		///    <Inputs>
		///       
		///    </Inputs>
		///    <Outputs>
		///       
		///    </Outputs>
		///    <Returns>
		///       
		///    </Returns>
		///    <Authors>
		///       THIENHD
		///    </Authors>
		///    <History>
		///       15-Dec-2004
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************

	
		public DataTable GetShipTolerence()
		{
			// TODO:  Add ProductItemInfoBO.GetShipTolerence implementation
			try 
			{
				ITM_ShipToleranceDS objITM_ShipToleranceDS = new ITM_ShipToleranceDS();

				DataTable dt = objITM_ShipToleranceDS.List().Tables[0];
				DataRow drEmptyRow = dt.NewRow();
				dt.Rows.InsertAt(drEmptyRow,0);


				return dt;
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

		//**************************************************************************              
		///    <Description>
		///       Get list of Buyer
		///    </Description>
		///    <Inputs>
		///       
		///    </Inputs>
		///    <Outputs>
		///       
		///    </Outputs>
		///    <Returns>
		///       
		///    </Returns>
		///    <Authors>
		///       THIENHD
		///    </Authors>
		///    <History>
		///       15-Dec-2004
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************

	
		public DataTable GetBuyer()
		{
			// TODO:  Add ProductItemInfoBO.GetBuyer implementation
			try 
			{
				ITM_BuyerDS objITM_BuyerDS = new ITM_BuyerDS();

				DataTable dt = objITM_BuyerDS.List().Tables[0];
				DataRow drEmptyRow = dt.NewRow();
				dt.Rows.InsertAt(drEmptyRow,0);



				return dt;
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

		//**************************************************************************              
		///    <Description>
		///       Get list of Vendor Location
		///    </Description>
		///    <Inputs>
		///       
		///    </Inputs>
		///    <Outputs>
		///       
		///    </Outputs>
		///    <Returns>
		///       
		///    </Returns>
		///    <Authors>
		///       THIENHD
		///    </Authors>
		///    <History>
		///       15-Dec-2004
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************

	
		public DataTable GetVendorLocation()
		{
			// TODO:  Add ProductItemInfoBO.GetVendorLocation implementation
			try 
			{
				MST_PartyLocationDS objMST_PartyLocationDS = new MST_PartyLocationDS();

				DataTable dt = objMST_PartyLocationDS.ListForCombo().Tables[0];
				DataRow drEmptyRow = dt.NewRow();
				dt.Rows.InsertAt(drEmptyRow,0);



				return dt;
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


		//**************************************************************************              
		///    <Description>
		///       Get list of Order Rule
		///    </Description>
		///    <Inputs>
		///       
		///    </Inputs>
		///    <Outputs>
		///       
		///    </Outputs>
		///    <Returns>
		///       
		///    </Returns>
		///    <Authors>
		///       THIENHD
		///    </Authors>
		///    <History>
		///       15-Dec-2004
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************

	
		public DataTable GetOrderRule()
		{
			// TODO:  Add ProductItemInfoBO.GetOrderRule implementation
			try 
			{
				ITM_OrderRuleDS objITM_OrderRuleDS = new ITM_OrderRuleDS();

				DataTable dt = objITM_OrderRuleDS.List().Tables[0];
				DataRow drEmptyRow = dt.NewRow();
				dt.Rows.InsertAt(drEmptyRow,0);


				return dt;
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

		//**************************************************************************              
		///    <Description>
		///       Get list of Location
		///    </Description>
		///    <Inputs>
		///       
		///    </Inputs>
		///    <Outputs>
		///       
		///    </Outputs>
		///    <Returns>
		///       
		///    </Returns>
		///    <Authors>
		///       THIENHD
		///    </Authors>
		///    <History>
		///       15-Dec-2004
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************

	
		public DataSet GetLocation()
		{
			try 
			{
				DataSet dstLocation = new DataSet();
				DataTable dt ;
				DataRow drEmptyRow ;
				//get Master Location
				MST_MasterLocationDS objMST_MasterLocationDS = new MST_MasterLocationDS();
				dt = objMST_MasterLocationDS.List().Tables[0].Copy();
				drEmptyRow = dt.NewRow();
				dt.Rows.InsertAt(drEmptyRow,0);
				dstLocation.Tables.Add(dt);

				//Get Location
				MST_LocationDS objMST_LocationDS = new MST_LocationDS();
				dt = objMST_LocationDS.List().Tables[0].Copy();
				drEmptyRow = dt.NewRow();
				dt.Rows.InsertAt(drEmptyRow,0);
				dstLocation.Tables.Add(dt);

				//Get Bin
				MST_BINDS objMST_BINDS = new MST_BINDS();
				dt = objMST_BINDS.List().Tables[0].Copy();
				drEmptyRow = dt.NewRow();
				dt.Rows.InsertAt(drEmptyRow,0);
				dstLocation.Tables.Add(dt);

				//Define relation for this dataset
				//1. Relation between Master Location and Location
				/*
				DataColumn[] dtcolMasterLocationColumnParent = new DataColumn[1]{dstLocation.Tables[MST_MasterLocationTable.TABLE_NAME].Columns[MST_MasterLocationTable.MASTERLOCATIONID_FLD]};
				DataColumn[] dtcolLocationColumnChild = new DataColumn[1]{dstLocation.Tables[MST_LocationTable.TABLE_NAME].Columns[MST_LocationTable.MASTERLOCATIONID_FLD]};
				dstLocation.Relations.Add(dtcolMasterLocationColumnParent,dtcolLocationColumnChild);
				*/


				//2.Relation between Location and Bin
				/*
				DataColumn[] dtcolLocationColumnParent = new DataColumn[1]{dstLocation.Tables[MST_LocationTable.TABLE_NAME].Columns[MST_LocationTable.LOCATIONID_FLD]};
				DataColumn[] dtcolBinColumnChild = new DataColumn[1]{dstLocation.Tables[MST_BINTable.TABLE_NAME].Columns[MST_BINTable.LOCATIONID_FLD]};
				dstLocation.Relations.Add(dtcolLocationColumnParent,dtcolBinColumnChild);
				*/

				return dstLocation;

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

		//**************************************************************************              
		///    <Description>
		///       Validate Bussiness rule
		///    </Description>
		///    <Inputs>
		///       
		///    </Inputs>
		///    <Outputs>
		///       
		///    </Outputs>
		///    <Returns>
		///       
		///    </Returns>
		///    <Authors>
		///       THIENHD
		///    </Authors>
		///    <History>
		///       15-Dec-2004
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************

	
		private bool ValidateBusiness(object pObjectDetail) 
		{
			const string METHOD_NAME = THIS + ".ValidateBusiness()";
			try 
			{
				ITM_ProductVO objITM_ProductVO = (ITM_ProductVO)pObjectDetail ; 
				if (objITM_ProductVO.CategoryID > 0)
				{
					//Check this category.
					//This category must be a leaf node 
					ITM_CategoryDS objITM_CategoryDS = new ITM_CategoryDS();
					if (!objITM_CategoryDS.IsLeafNode(objITM_ProductVO.CategoryID)) 
					{
						throw new PCSBOException(ErrorCode.MSG_PRODUCTINFO_CATEGORY_NOTALEAFNODE,METHOD_NAME,null);
					}
				}
				return true;
			}
			catch (PCSDBException ex)
			{
				throw ex;
			}
			catch(Exception ex) 
			{
				throw ex;
			}
		}
		//**************************************************************************              
		///    <Description>
		///       Delete a product
		///    </Description>
		///    <Inputs>
		///       
		///    </Inputs>
		///    <Outputs>
		///       
		///    </Outputs>
		///    <Returns>
		///       
		///    </Returns>
		///    <Authors>
		///       THIENHD
		///    </Authors>
		///    <History>
		///       15-Dec-2004
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************

	
		public void DeleteProduct(int pintProductID)
		{
			try 
			{
				ITM_ProductDS objITM_ProductDS = new ITM_ProductDS();
				objITM_ProductDS.Delete(pintProductID);
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
		//**************************************************************************              
		///    <Description>
		///       Get vendor code based on ID
		///    </Description>
		///    <Inputs>
		///       
		///    </Inputs>
		///    <Outputs>
		///       
		///    </Outputs>
		///    <Returns>
		///       
		///    </Returns>
		///    <Authors>
		///       THIENHD
		///    </Authors>
		///    <History>
		///       15-Dec-2004
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************

	
		public string GetVendorCodeAndName(int pintVendorID)
		{
			try 
			{
				MST_PartyDS objMST_PartyDS = new MST_PartyDS();
				return objMST_PartyDS.GetPartyCodeAndName(pintVendorID);
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

		/// <summary>
		/// Get Currency Code 
		/// </summary>
		/// <param name="pintCurencyID"></param>
		/// <returns></returns>
	
		public string GetCurrencyCode(int pintCurencyID)
		{
			try 
			{
				MST_CurrencyVO voCurrency = (MST_CurrencyVO)(new MST_CurrencyDS()).GetObjectVO(pintCurencyID);
				if(voCurrency != null)
				{
					return voCurrency.Code;
				}

				return string.Empty;
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

		
		/// <summary>
		/// Get Currency Code 
		/// </summary>
		/// <param name="pintCurencyID"></param>
		/// <returns></returns>
	
		public string GetInventorCode(int pintInventorID)
		{
			try 
			{
				MST_PartyVO voParty = (MST_PartyVO)(new MST_PartyDS()).GetObjectVO(pintInventorID);
				if(voParty != null)
				{
					return voParty.Code;
				}

				return string.Empty;
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

		//**************************************************************************              
		///    <Description>
		///       Get vendor ID
		///    </Description>
		///    <Inputs>
		///       
		///    </Inputs>
		///    <Outputs>
		///       
		///    </Outputs>
		///    <Returns>
		///       
		///    </Returns>
		///    <Authors>
		///       THIENHD
		///    </Authors>
		///    <History>
		///       15-Dec-2004
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************

	
		public int GetVendorID(string pstrVendorCode)
		{
			try 
			{
				MST_PartyDS objMST_PartyDS = new MST_PartyDS();
				return objMST_PartyDS.GetPartyID(pstrVendorCode);
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
			const string METHOD_NAME = THIS + ".GetItemInformationData()";

			ITM_ProductDS objITM_ProductDS = new ITM_ProductDS();
			return objITM_ProductDS.GetItemInformationData(pnProductID, 
				pstrMetaDataTableName, pstrStockStatusTableName, pstrBOMTableName, pstrRoutingTableName, pstrStandardCostTableName);		
		}

		
		#endregion ENDHACKED: Thachnn: 01 / 03 / 2006 : add DB function to create data for ItemInformationReport


	}
}
