using System;
using System.Collections.Generic;
using System.Data;
using PCSComUtils.Common;
using PCSComUtils.PCSExc;
using PCSComUtils.Common.DS;
using PCSComUtils.MasterSetup.DS;
using PCSComMaterials.Inventory.DS;
using PCSComUtils.Common.BO;
using PCSComProduct.Items.DS;
using PCSComProduct.Items.BO;

namespace PCSComMaterials.Inventory.BO
{
	/// <summary>
	/// Represents all utilities provided in Inventory module
	/// </summary>	
	public class InventoryUtilsBO
	{
		private CommonDS dsCommon = new CommonDS();		
		
		#region Reservered Methods
		/// <summary>
		/// Insert a new record into database
		/// </summary>
	
		public void Add(object pObjectDetail)
		{
			throw new NotImplementedException();
		}
		
		/// <summary>
		/// Delete record by condition
		/// </summary>
	
		public void Delete(object pObjectVO)
		{
			throw new NotImplementedException();
		}

		
		/// <summary>
		/// Get the object information by ID of VO class
		/// </summary>
	
		public object GetObjectVO(int pintID, string VOclass)
		{
			throw new NotImplementedException();
		}		
		
		/// <summary>
		/// Return the DataSet (list of record) by inputing the FieldList and Condition
		/// </summary>
	
		public void UpdateDataSet(DataSet dstData)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Update into Database
		/// </summary>
	
		public void Update(object pObjectDetail)
		{
			throw new NotImplementedException();
		}


		public int GetPurposeCode(int pintPurposeID)
		{
			return dsCommon.GetPurposeCode(pintPurposeID);
		}
		#endregion Reservered

		#region Quantity Related Methods
		
	
		public void UpdateAllQuantityFromBin()
		{
			IV_LocationCacheDS dsLocCache = new IV_LocationCacheDS();
			dsLocCache.UpdateAllQuantityFromBin();
			IV_MasLocCacheDS dsMasLocCache = new IV_MasLocCacheDS();
			dsMasLocCache.UpdateAllQuantityFromLocation();
		}

	
		public DataTable GetAvailableQtyByPostDate(DateTime pdtmPostDate)
		{
			IV_BinCacheDS dsBinCache = new IV_BinCacheDS();
			return dsBinCache.GetAvailableQtyByPostDate(pdtmPostDate);
		}
	
		public DataTable GetAvailableQtyByPostDate(DateTime pdtmPostDate, string pstrProductIDs)
		{
			IV_BinCacheDS dsBinCache = new IV_BinCacheDS();
			return dsBinCache.GetAvailableQtyByPostDate(pdtmPostDate, pstrProductIDs);
		}
		/// <summary>
		/// Determine that Product is existed in Bin yet
		/// </summary>
		/// <param name="pintBinID">Bin to check</param>
		/// <param name="pintProductID">Product to check</param>
		/// <returns>true if exist, false if not exist</returns>
	
		public bool IsHasProductInBin(int pintBinID, int pintProductID)
		{
			IV_BinCacheDS dsBinCache = new IV_BinCacheDS();
			return dsBinCache.IsHasProductInBin(pintBinID, pintProductID);
		}
	
		public void CheckQuantity()
		{
		}
		
		/// <param name="pdecQuantity">Quantity to add</param>
	
		private void UpdateAddOHQuantityLoc(int pintCCNID, int pintMasterLocationID, int pintLocationID, int pintProductID, decimal pdecQuantity, string pstrLot)
		{
				
				//validate data
				if(pstrLot == null) pstrLot = string.Empty;

				IV_LocationCacheDS dsLocationCache = new IV_LocationCacheDS();

				//Check if this product does not exist in LocationCache
				if(!dsLocationCache.HasProductID(pintProductID, pintCCNID, pintMasterLocationID, pintLocationID, pstrLot))
				{
					//Create new LocationCache object 
					IV_LocationCacheVO voLocationCache = new IV_LocationCacheVO();
				
					//Set transfer values to object
					voLocationCache.CCNID = pintCCNID;
					voLocationCache.Lot = pstrLot;
					voLocationCache.MasterLocationID = pintMasterLocationID;
					voLocationCache.ProductID = pintProductID;
					voLocationCache.OHQuantity = 0;
					voLocationCache.LocationID = pintLocationID;

					//Set default values to object
					voLocationCache.CommitQuantity = 0;	
					voLocationCache.DemanQuantity = 0;
					voLocationCache.SupplyQuantity = 0;						
				
					//call add method to add new
					dsLocationCache.Add(voLocationCache);
				}

				if(pstrLot.Length != 0)
				{
					dsLocationCache.UpdateAddOHQuantity(pintCCNID, pintMasterLocationID, pintLocationID, pintProductID, pstrLot, pdecQuantity);
				}
				else
				{
					dsLocationCache.UpdateAddOHQuantity(pintCCNID, pintMasterLocationID, pintLocationID, pintProductID, pdecQuantity);
				}
			
		}

		/// <param name="pdecQuantity">Quantity to add</param>
	
		private void UpdateAddOHQuantityMasLoc(int pintCCNID, int pintMasterLocationID, int pintProductID, decimal pdecQuantity, string pstrLot)
		{
			
				//validate data
				if(pstrLot == null) pstrLot = string.Empty;

				IV_MasLocCacheDS dsMasLocCache = new IV_MasLocCacheDS();

				//Check if this product does not exist in MasLocCache
				if(!dsMasLocCache.HasProductID(pintProductID, pintCCNID, pintMasterLocationID, pstrLot))
				{
					//Create new MasLocCache object 
					IV_MasLocCacheVO voMasLocCache = new IV_MasLocCacheVO();
					
					//Set transfer values to object
					voMasLocCache.CCNID = pintCCNID;
					voMasLocCache.Lot = pstrLot;
					voMasLocCache.MasterLocationID = pintMasterLocationID;
					voMasLocCache.ProductID = pintProductID;
					voMasLocCache.OHQuantity = 0;
					
					//Set default values to object
					voMasLocCache.CommitQuantity = 0;	
					voMasLocCache.DemanQuantity = 0;
					voMasLocCache.SummItemCost21 = 0;
					voMasLocCache.SupplyQuantity = 0;
					
					//call add method to add new
					dsMasLocCache.Add(voMasLocCache);
				}

				if(pstrLot.Length != 0)
				{
					dsMasLocCache.UpdateAddOHQuantity(pintCCNID, pintMasterLocationID, pintProductID, pstrLot, pdecQuantity);
				}
				else
				{
					dsMasLocCache.UpdateAddOHQuantity(pintCCNID, pintMasterLocationID, pintProductID, pdecQuantity);
				}
			
		}
		
		/// <param name="pdecQuantity">Quantity to add</param>
	
		private void UpdateAddOHQuantityBin(int pintCCNID, int pintMasterLocationID, int pintLocationID, int pintBinID, int pintProductID, decimal pdecQuantity, string pstrLot)
		{
				//validate data
				if(pstrLot == null) pstrLot = string.Empty;

				IV_BinCacheDS dsBinCache = new IV_BinCacheDS();

				//Check if this product does not exist in BinCache
				if(!dsBinCache.HasProductID(pintProductID, pintCCNID, pintMasterLocationID, pintLocationID, pintBinID, pstrLot))
				{
					//Create new BinCache object 
					IV_BinCacheVO voBinCache = new IV_BinCacheVO();
				
					//Set transfer values to object
					voBinCache.CCNID = pintCCNID;
					voBinCache.Lot = pstrLot;
					voBinCache.MasterLocationID = pintMasterLocationID;
					voBinCache.ProductID = pintProductID;
					voBinCache.OHQuantity = 0;
					voBinCache.LocationID = pintLocationID;
					voBinCache.BinID = pintBinID;

					//Set default values to object
					voBinCache.CommitQuantity = 0;	
					voBinCache.DemanQuantity = 0;
					voBinCache.SupplyQuantity = 0;
				
					//call add method to add new
					dsBinCache.Add(voBinCache);
				}

				if(pstrLot.Length != 0)
				{
					dsBinCache.UpdateAddOHQuantity(pintCCNID, pintMasterLocationID, pintLocationID, pintBinID, pintProductID, pstrLot, pdecQuantity);
				}
				else
				{
					dsBinCache.UpdateAddOHQuantity(pintCCNID, pintMasterLocationID, pintLocationID,  pintBinID, pintProductID, pdecQuantity);
				}
			
		}

		/// <param name="pdecQuantity">Quantity to add</param>
	
		public void UpdateSubtractOHQuantity(int pintCCNID, int pintMasterLocationID, int pintLocationID, int pintBinID, int pintProductID, decimal pdecQuantity, string pstrLot, string pstrSerial)
		{
				//validate data
				if(pstrLot == null) pstrLot = string.Empty;
				if(pstrSerial == null) pstrSerial = string.Empty;


				IV_BinCacheDS dsBinCache = new IV_BinCacheDS();
				IV_LocationCacheDS dsLocCache = new IV_LocationCacheDS();
				IV_MasLocCacheDS dsMasLocCache = new IV_MasLocCacheDS();
				
				if(pstrLot.Length != 0 && pstrSerial.Length != 0 && pintBinID > 0)
				{
					//if it has both Bin, Lot and Serial
					dsBinCache.UpdateSubtractOHQuantity(pintCCNID, pintMasterLocationID, pintLocationID, pintBinID, pintProductID, pstrLot, pdecQuantity);
					dsLocCache.UpdateSubtractOHQuantity(pintCCNID, pintMasterLocationID, pintLocationID, pintProductID, pstrLot, pdecQuantity);
					dsMasLocCache.UpdateSubtractOHQuantity(pintCCNID, pintMasterLocationID, pintProductID, pstrLot, pdecQuantity);					
				}
				else if(pintBinID > 0 && pstrLot.Length != 0)
				{
					#region Check before subtract OHQuantity 
					// HACK: SonHT
					IV_BinCacheVO voBinCache = new IV_BinCacheVO();
					voBinCache.CCNID = pintCCNID;
					voBinCache.MasterLocationID = pintMasterLocationID;
					voBinCache.LocationID = pintLocationID;
					voBinCache.BinID = pintBinID;
					voBinCache.ProductID = pintProductID;
					if(dsBinCache.GetOnhandQty(voBinCache) < pdecQuantity)
					{
						throw new PCSBOException(ErrorCode.MESSAGE_AVAILABLE_WAS_USED_AFTER_POSTDATE, this.GetType().FullName + ".UpdateSubtractOHQuantity()", null);
					}

					#endregion
					//if it has both Bin and Lot
					dsBinCache.UpdateSubtractOHQuantity(pintCCNID, pintMasterLocationID, pintLocationID, pintBinID, pintProductID, pstrLot, pdecQuantity);
					dsLocCache.UpdateSubtractOHQuantity(pintCCNID, pintMasterLocationID, pintLocationID, pintProductID, pstrLot, pdecQuantity);
					dsMasLocCache.UpdateSubtractOHQuantity(pintCCNID, pintMasterLocationID, pintProductID, pstrLot, pdecQuantity);
				}
				else if(pintBinID > 0 && pstrLot.Length == 0 && pstrSerial.Length == 0)
				{
					#region Check before subtract OHQuantity 
					// HACK: SonHT
					IV_BinCacheVO voBinCache = new IV_BinCacheVO();
					voBinCache.CCNID = pintCCNID;
					voBinCache.MasterLocationID = pintMasterLocationID;
					voBinCache.LocationID = pintLocationID;
					voBinCache.BinID = pintBinID;
					voBinCache.ProductID = pintProductID;
                    if(dsBinCache.GetOnhandQty(voBinCache) < pdecQuantity)
					{
						throw new PCSBOException(ErrorCode.MESSAGE_AVAILABLE_WAS_USED_AFTER_POSTDATE, this.GetType().FullName + ".UpdateSubtractOHQuantity()", null);
					}

					#endregion
					//if it has Bin and no Lot, no Serial
					dsBinCache.UpdateSubtractOHQuantity(pintCCNID, pintMasterLocationID, pintLocationID, pintBinID, pintProductID, pdecQuantity);
					dsLocCache.UpdateSubtractOHQuantity(pintCCNID, pintMasterLocationID, pintLocationID, pintProductID, pdecQuantity);
					dsMasLocCache.UpdateSubtractOHQuantity(pintCCNID, pintMasterLocationID, pintProductID, pdecQuantity);
				}				
				else if( pintLocationID > 0 && pintBinID <=0  && pstrLot.Length == 0 && pstrSerial.Length == 0)
				{
					//if it has Loc and (no Lot, no Bin)
					dsLocCache.UpdateSubtractOHQuantity(pintCCNID, pintMasterLocationID, pintLocationID, pintProductID, pdecQuantity);
					dsMasLocCache.UpdateSubtractOHQuantity(pintCCNID, pintMasterLocationID, pintProductID, pdecQuantity);
				}
				else if(pintLocationID > 0 && pstrLot.Length != 0 && pintBinID <=0 && pstrSerial.Length == 0)
				{
					//if it has both Loc and Lot (no Bin)
					dsLocCache.UpdateSubtractOHQuantity(pintCCNID, pintMasterLocationID, pintLocationID, pintProductID, pstrLot, pdecQuantity);
					dsMasLocCache.UpdateSubtractOHQuantity(pintCCNID, pintMasterLocationID, pintProductID, pstrLot, pdecQuantity);
				}
				else if(pstrLot.Length != 0 && pstrSerial.Length != 0 && pintBinID <=0)
				{
					//if it has Lot and Serial but no Bin
					dsLocCache.UpdateSubtractOHQuantity(pintCCNID, pintMasterLocationID, pintLocationID, pintProductID, pstrLot, pdecQuantity);
					dsMasLocCache.UpdateSubtractOHQuantity(pintCCNID, pintMasterLocationID, pintProductID, pstrLot, pdecQuantity);					
				}				

		}

		/// <param name="pdecQuantity">Quantity to add</param>
	
		public void UpdateAddOHQuantity(int pintCCNID, int pintMasterLocationID, int pintLocationID, int pintBinID, int pintProductID, decimal pdecQuantity, string pstrLot, string pstrSerial)
		{

				//validate data
				if(pstrLot == null) pstrLot = string.Empty;				
				if(pstrSerial == null) pstrSerial = string.Empty;

                if (pintBinID > 0)
                {
                    // if it has Bin, Lot and Serial
                    if (pstrLot.Length != 0 && pstrSerial.Length != 0)
                    {
                        UpdateAddOHQuantityBin(pintCCNID, pintMasterLocationID, pintLocationID, pintBinID, pintProductID, pdecQuantity, pstrLot);
                        UpdateAddOHQuantityLoc(pintCCNID, pintMasterLocationID, pintLocationID, pintProductID, pdecQuantity, pstrLot);
                        UpdateAddOHQuantityMasLoc(pintCCNID, pintMasterLocationID, pintProductID, pdecQuantity, pstrLot);
                    }
                    else
                    {
                        //if has Bin and (dont has Lot or dont has Serial) 
                        UpdateAddOHQuantityBin(pintCCNID, pintMasterLocationID, pintLocationID, pintBinID, pintProductID, pdecQuantity, pstrLot);
                        UpdateAddOHQuantityLoc(pintCCNID, pintMasterLocationID, pintLocationID, pintProductID, pdecQuantity, pstrLot);
                        UpdateAddOHQuantityMasLoc(pintCCNID, pintMasterLocationID, pintProductID, pdecQuantity, pstrLot);
                    }
                }
                else //Bin <= 0 
                {
                    UpdateAddOHQuantityLoc(pintCCNID, pintMasterLocationID, pintLocationID, pintProductID, pdecQuantity,
                                           pstrLot);
                    UpdateAddOHQuantityMasLoc(pintCCNID, pintMasterLocationID, pintProductID, pdecQuantity, pstrLot);
                }
		}

		/// <param name="pdecQuantity">Quantity to add</param>

        public decimal CheckAvailableQuantity(int pintCCNID, int pintMasterLocationID, int pintLocationID, int pintBinID, int pintProductID, decimal pdecQuantity, string pstrLot, string pstrSerial)
		{

		    //validate data
		    if (pstrLot == null) pstrLot = string.Empty;
		    if (pstrSerial == null) pstrSerial = string.Empty;

		    decimal decQuantity = 0;
		    //if it has Bin and has no Lot
		    if (pintBinID > 0 && pstrLot.Length == 0)
		    {
		        IV_BinCacheDS dsBinCache = new IV_BinCacheDS();
		        decQuantity = dsBinCache.GetAvailableQuantity(pintCCNID, pintMasterLocationID, pintLocationID, pintBinID,
		                                                      pintProductID);
		        if (decQuantity >= pdecQuantity)
		        {
		            return -1;
		        }
		        return decQuantity;
		    }
		    if (pintBinID > 0 && pstrLot.Length != 0)
		    {
		        //if has both Bin and Lot
		        IV_BinCacheDS dsBinCache = new IV_BinCacheDS();
		        decQuantity = dsBinCache.GetAvailableQuantityByLot(pintCCNID, pintMasterLocationID, pintLocationID, pintBinID,
		                                                           pstrLot, pintProductID);
		        return decQuantity >= pdecQuantity ? -1 : decQuantity;
		    }
		    if (pintLocationID > 0 && pstrLot.Length != 0)
		    {
		        //if has both Bin and Lot
		        IV_LocationCacheDS dsLocationCache = new IV_LocationCacheDS();
		        decQuantity = dsLocationCache.GetAvailableQuantityByLot(pintCCNID, pintMasterLocationID, pintLocationID,
		                                                                pstrLot, pintProductID);
		        return decQuantity >= pdecQuantity ? -1 : decQuantity;
		    }
		    if (pintLocationID > 0 && pstrLot.Length == 0)
		    {
		        //if has both Bin and Lot
		        IV_LocationCacheDS dsLocationCache = new IV_LocationCacheDS();
		        decQuantity = dsLocationCache.GetAvailableQuantityByLocation(pintCCNID, pintMasterLocationID, pintLocationID,
		                                                                     pintProductID);
		        return decQuantity >= pdecQuantity ? -1 : decQuantity;
		    }
		    if (pintBinID > 0 && pstrLot.Length != 0 && pstrSerial.Length != 0)
		    {
		        return 0;
		    }
		    return -1;
		}

	    /// <summary>
		///  Get commit quantity base on BinID if BinID > 0 else base on LocID
		/// </summary>
		/// <param name="pdtmPostDate"></param>
		/// <param name="pintCCNID"></param>
		/// <param name="pintMasterLocationID"></param>
		/// <param name="pintLocationID"></param>
		/// <param name="pintBinID"></param>
		/// <param name="pintProductID"></param>
		/// <returns></returns>
		/// <author>Trada</author>
		/// <date>Monday, December 19 2005</date>
	
		public decimal GetCommitQtyByPostDate(DateTime pdtmPostDate, int pintCCNID, int pintMasterLocationID, int pintLocationID, int pintBinID, int pintProductID)
		{
			if (pintBinID > 0)
				{
					IV_BinCacheDS dsBinCache = new IV_BinCacheDS();
					decimal decCommitQty = 0;
					if (pdtmPostDate < (new UtilsBO()).GetDBDate())
					{	
						decCommitQty = dsBinCache.GetCommitQtyByPostDate(pdtmPostDate, pintCCNID, pintMasterLocationID, pintLocationID,pintBinID, pintProductID);
						return decCommitQty;
					}
					else
					{
						decCommitQty = dsBinCache.GetCommitQtyByPostDate(pintCCNID, pintMasterLocationID, pintLocationID, pintBinID, pintProductID);
						return decCommitQty;
					}
				}
				else
				{
					IV_LocationCacheDS dsLocationCache = new IV_LocationCacheDS();
					decimal decCommitQty = 0;
					if (pdtmPostDate < (new UtilsBO()).GetDBDate())
					{
						decCommitQty = dsLocationCache.GetCommitQtyByPostDate(pdtmPostDate, pintCCNID, pintMasterLocationID, pintLocationID, pintProductID);		
						return decCommitQty;
					}
					else
					{
						decCommitQty = dsLocationCache.GetCommitQtyByPostDate(pintCCNID, pintMasterLocationID, pintLocationID, pintProductID);
						return decCommitQty;
					}
				}
		}
		/// <summary>
		/// GetAvailableQtyByPostDate
		/// </summary>
		/// <param name="pdtmPostDate"></param>
		/// <param name="pintCCNID"></param>
		/// <param name="pintMasterLocationID"></param>
		/// <param name="pintLocationID"></param>
		/// <param name="pintBinID"></param>
		/// <param name="pintProductID"></param>
		/// <returns></returns>
		/// <author>Trada</author>
		/// <date>Tuesday, December 20 2005</date>
	
		public decimal GetAvailableQtyByPostDate(DateTime pdtmPostDate, int pintCCNID, int pintMasterLocationID, int pintLocationID, int pintBinID, int pintProductID)
		{
			return GetOHQtyByPostDate(pdtmPostDate, pintCCNID, pintMasterLocationID, pintLocationID,pintBinID, pintProductID)
				- GetCommitQtyByPostDate(pdtmPostDate, pintCCNID, pintMasterLocationID, pintLocationID,pintBinID, pintProductID);
		}
		/// <summary>
		/// GetOHQtyByPostDate
		/// </summary>
		/// <param name="pdtmPostDate"></param>
		/// <param name="pintCCNID"></param>
		/// <param name="pintMasterLocationID"></param>
		/// <param name="pintLocationID"></param>
		/// <param name="pintBinID"></param>
		/// <param name="pintProductID"></param>
		/// <returns></returns>
		/// <author>Trada</author>
		/// <date>Tuesday, December 20 2005</date>
	
		public decimal GetOHQtyByPostDate(DateTime pdtmPostDate, int pintCCNID, int pintMasterLocationID, int pintLocationID, int pintBinID, int pintProductID)
		{
				if (pintBinID > 0)
				{
					IV_BinCacheDS dsBinCache = new IV_BinCacheDS();
					decimal decCommitQty = 0;
					if (pdtmPostDate < (new UtilsBO()).GetDBDate())
					{	
						decCommitQty = dsBinCache.GetOHQtyByPostDate(pdtmPostDate, pintCCNID, pintMasterLocationID, pintLocationID,pintBinID, pintProductID);
						return decCommitQty;
					}
					else
					{
						decCommitQty = dsBinCache.GetOHQtyByPostDate(pintCCNID, pintMasterLocationID, pintLocationID, pintBinID, pintProductID);
						return decCommitQty;
					}
				}
				else
				{
					IV_LocationCacheDS dsLocationCache = new IV_LocationCacheDS();
					decimal decCommitQty = 0;
					if (pdtmPostDate < (new UtilsBO()).GetDBDate())
					{
						decCommitQty = dsLocationCache.GetOHQtyByPostDate(pdtmPostDate, pintCCNID, pintMasterLocationID, pintLocationID, pintProductID);		
						return decCommitQty;
					}
					else
					{
						decCommitQty = dsLocationCache.GetOHQtyByPostDate(pintCCNID, pintMasterLocationID, pintLocationID, pintProductID);
						return decCommitQty;
					}
				}
			
		}	
	
		public DataTable GetOHQtyByPostDate(DateTime pdtmPostDate, int pintMasterLocationID, int pintLocationID, int pintBinID, int pintProductID)
		{
			IV_BinCacheDS dsBinCache = new IV_BinCacheDS();
			if (pdtmPostDate < (new UtilsBO()).GetDBDate())
				return dsBinCache.GetOHQtyByPostDate(pdtmPostDate, pintMasterLocationID, pintLocationID,pintBinID, pintProductID);
			else
				return dsBinCache.GetOHQtyByPostDate(pintMasterLocationID, pintLocationID,pintBinID, pintProductID);
		}	
		/// <summary>
		/// Get available quantity in Bin
		/// </summary>
		/// <param name="pintBinID">BinID</param>
		/// <returns></returns>
	
		public DataTable GetAvailableQuantity(int pintBinID)
		{
			IV_BinCacheDS dsBinCache = new IV_BinCacheDS();
			return dsBinCache.GetAvailableQuantity(pintBinID);
		}
	
		public DataSet ListAllBinCache()
		{
			IV_BinCacheDS dsBinCache = new IV_BinCacheDS();
			return dsBinCache.ListAllBinCache();
		}

        public DataTable ListAllBinCache(List<string> binIdList)
        {
            var dsBinCache = new IV_BinCacheDS();
            return dsBinCache.ListAllBinCache(binIdList);
        }
	
		public DataSet ListAllLocationCache()
		{
			IV_LocationCacheDS dsLocCache = new IV_LocationCacheDS();
			return dsLocCache.ListAllCache();
		}
	
		public DataSet ListAllMasLocCache()
		{
			IV_MasLocCacheDS dsMas = new IV_MasLocCacheDS();
			return dsMas.ListAllCache();
		}
		
		#region // HACK: DuongNA 2005-10-31
	
		public void UpdateSubtractCommitQuantity(int pintCCNID, int pintMasterLocationID, int pintLocationID, int pintBinID, int pintProductID, decimal pdecQuantity, string pstrLot, string pstrSerial)
		{
			
				//validate data
				if(pstrLot == null) pstrLot = string.Empty;
				if(pstrSerial == null) pstrSerial = string.Empty;


				IV_BinCacheDS dsBinCache = new IV_BinCacheDS();
				IV_LocationCacheDS dsLocCache = new IV_LocationCacheDS();
				IV_MasLocCacheDS dsMasLocCache = new IV_MasLocCacheDS();
				
				if(pstrLot.Length != 0 && pstrSerial.Length != 0 && pintBinID > 0)
				{
					//if it has both Bin, Lot and Serial
					dsBinCache.UpdateSubtractCommitQuantity(pintCCNID, pintMasterLocationID, pintLocationID, pintBinID, pintProductID, pdecQuantity);
					dsLocCache.UpdateSubtractCommitQuantity(pintCCNID, pintMasterLocationID, pintLocationID, pintProductID, pdecQuantity);
					dsMasLocCache.UpdateSubtractCommitQuantity(pintCCNID, pintMasterLocationID, pintProductID, pdecQuantity);
					
					/*IV_ItemSerialDS dsItemSerial = new IV_ItemSerialDS();
					if(dsItemSerial.HasSerial(pstrSerial, pintCCNID))
					{						
						dsItemSerial.UpdateRemain(pstrSerial, pintCCNID, pintProductID, true);
					}*/					
				}
				else if(pintBinID > 0 && pstrLot.Length != 0)
				{
					//if it has both Bin and Lot
					dsBinCache.UpdateSubtractCommitQuantity(pintCCNID, pintMasterLocationID, pintLocationID, pintBinID, pintProductID, pdecQuantity);
					dsLocCache.UpdateSubtractCommitQuantity(pintCCNID, pintMasterLocationID, pintLocationID, pintProductID, pdecQuantity);
					dsMasLocCache.UpdateSubtractCommitQuantity(pintCCNID, pintMasterLocationID, pintProductID, pdecQuantity);
				}
				else if(pintBinID > 0 && pstrLot.Length == 0 && pstrSerial.Length == 0)
				{
					//if it has Bin and no Lot, no Serial
					dsBinCache.UpdateSubtractCommitQuantity(pintCCNID, pintMasterLocationID, pintLocationID, pintBinID, pintProductID, pdecQuantity);
					dsLocCache.UpdateSubtractCommitQuantity(pintCCNID, pintMasterLocationID, pintLocationID, pintProductID, pdecQuantity);
					dsMasLocCache.UpdateSubtractCommitQuantity(pintCCNID, pintMasterLocationID, pintProductID, pdecQuantity);
				}				
				else if( pintLocationID > 0 && pintBinID <=0  && pstrLot.Length == 0 && pstrSerial.Length == 0)
				{					
					//if it has Loc and (no Lot, no Bin)
					dsLocCache.UpdateSubtractCommitQuantity(pintCCNID, pintMasterLocationID, pintLocationID, pintProductID, pdecQuantity);
					dsMasLocCache.UpdateSubtractCommitQuantity(pintCCNID, pintMasterLocationID, pintProductID, pdecQuantity);
				}
				else if(pintLocationID > 0 && pstrLot.Length != 0 && pintBinID <=0 && pstrSerial.Length == 0)
				{
					//if it has both Loc and Lot (no Bin)
					dsLocCache.UpdateSubtractCommitQuantity(pintCCNID, pintMasterLocationID, pintLocationID, pintProductID, pdecQuantity);
					dsMasLocCache.UpdateSubtractCommitQuantity(pintCCNID, pintMasterLocationID, pintProductID, pdecQuantity);
				}
				else if(pstrLot.Length != 0 && pstrSerial.Length != 0 && pintBinID <=0)
				{
					//if it has Lot and Serial but no Bin
					dsLocCache.UpdateSubtractCommitQuantity(pintCCNID, pintMasterLocationID, pintLocationID, pintProductID, pdecQuantity);
					dsMasLocCache.UpdateSubtractCommitQuantity(pintCCNID, pintMasterLocationID, pintProductID, pdecQuantity);
					
					/*IV_ItemSerialDS dsItemSerial = new IV_ItemSerialDS();
					if(dsItemSerial.HasSerial(pstrSerial, pintCCNID))
					{						
						dsItemSerial.UpdateRemain(pstrSerial, pintCCNID, pintProductID, true);
					}*/
				}				
			
		}
		#endregion // END: DuongNA 2005-10-31

		#endregion Quantity Related Methods
				
		#region Transaction History Related Methods
		/// <summary>
		/// Prepare: 
		/// - Put Transaction Type as parameter (via Constants)
		/// - assign following properties of MST_TransactionVO: (for Master object only)
		/// + CCNID, MasterLocationID, LocationID (if have), BinID (if have)
		/// + Lot, Serial (if have)
		/// + RefMasterID
		/// + TransTypeID = MST_TransTypeDS.GetTranTypeID(TransactionType Code)
		/// + ProductID (if have).
		/// 
		/// 1. get master object via RefMasterID
		/// 2. get all detail data of Master object if any
		/// 3. foreach detail object
		/// 3.1 check transaction type
		/// 3.2 assign value associated with trans type
		/// 3.3 assign common value: On-hand quantity, cost
		/// 3.4 save to database
		/// </summary>
		/// <param name="pstrTransType">Transaction Type - from Constants</param>
		/// <param name="pobjTransactionHistoryVO">MST_TransactionHistoryVO. You need to input CCNID, TransDate, TransTypeID, MasterLocationID, RefMasterID</param>
		/// <param name="pintIssuePurposeID">Issue Purpose</param>
		public void SaveTransactionHistory(string pstrTransType, int pintIssuePurposeID, object pobjTransactionHistoryVO)
		{
			try
			{	
				MST_TransactionHistoryVO voTransHis = (MST_TransactionHistoryVO)pobjTransactionHistoryVO;
				MST_TransactionHistoryDS dsTransHis = new MST_TransactionHistoryDS();
				DataTable dtbItem = null;

				//Assign transaction type ID
				voTransHis.TranTypeID = (new MST_TranTypeDS()).GetTranTypeID(pstrTransType);	

				//HACKED by Tuan TQ. 20 Dec, 2005. Set Transaction Date to current server date
				voTransHis.TransDate = (new UtilsDS()).GetDBDate();
				voTransHis.PurposeID = pintIssuePurposeID;

				// username
				voTransHis.Username = SystemProperty.UserName;

				switch(pstrTransType)
				{
					//HACKED --Rem by Tuan TQ. 28 Dec, 2005
					//Transaction History duplicate insert. Already insert on form before calling this method
					/*
					case TransactionType.PURCHASE_ORDER_RECEIPT:
							
						dtbItem = dsCommon.GetPOReceiptDetailByMaster(voTransHis.RefMasterID);
						if(dtbItem != null)
						{
							foreach(DataRow row in dtbItem.Rows)
							{
								voTransHis.RefDetailID = int.Parse(row[PO_PurchaseOrderReceiptDetailTable.PURCHASEORDERRECEIPTDETAILID_FLD].ToString());
								
								//Assign values
								AssignValueForPOReceipt(voTransHis);
								AssignCommonValue(voTransHis);
								
								// Call add method of Transaction History DS to save to database			
								dsTransHis.Add(voTransHis);
							}
						}

						break;

					case TransactionType.RETURN_TO_VENDOR:
						
						dtbItem = dsCommon.GetPOReturnToVendorDetailByMaster(voTransHis.RefMasterID);
						if(dtbItem != null)
						{
							foreach(DataRow row in dtbItem.Rows)
							{
								voTransHis.RefDetailID = int.Parse(row[PO_ReturnToVendorDetailTable.RETURNTOVENDORDETAILID_FLD].ToString());
								
								//Assign values
								AssignValueForRTV(voTransHis);
								AssignCommonValue(voTransHis);
								
								// Cal Add method of Transaction History DS to save to database			
								dsTransHis.Add(voTransHis);
							}
						}						
						break;

					case TransactionType.RETURN_GOODS_RECEIVE:
						
						dtbItem = dsCommon.GetSOReturnGoodsDetailByMaster(voTransHis.RefMasterID);
						if(dtbItem != null)
						{
							foreach(DataRow row in dtbItem.Rows)
							{
								voTransHis.RefDetailID = int.Parse(row[SO_ReturnedGoodsDetailTable.RETURNEDGOODSDETAILID_FLD].ToString());
								
								//Assign values
								AssignValueForRGReceive(voTransHis);
								AssignCommonValue(voTransHis);
								
								// Cal Add method of Transaction History DS to save to database			
								dsTransHis.Add(voTransHis);
							}
						}

						break;
						
					//End hacked
					*/

					default:
						//Assign values
						AssignValueForInventory(voTransHis);
						AssignCommonValue(voTransHis);
						// 19-06-2006 dungla: refine quantity before save to database
						if (voTransHis.BinOHQuantity < 0)
							voTransHis.BinOHQuantity = 0;
						if (voTransHis.LocationOHQuantity < 0)
							voTransHis.LocationOHQuantity = 0;
						if (voTransHis.MasLocOHQuantity < 0)
							voTransHis.MasLocOHQuantity = 0;
						// Cal Add method of Transaction History DS to save to database			
						dsTransHis.Add(voTransHis);
						break;
				}				
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
	
		public void UpdateTransactionHistory(string pstrTransType, int pintIssuePurposeID, object pobjTransactionHistoryVO)
		{
			try
			{	
				MST_TransactionHistoryVO voTransHis = (MST_TransactionHistoryVO)pobjTransactionHistoryVO;
				MST_TransactionHistoryDS dsTransHis = new MST_TransactionHistoryDS();
				DataTable dtbItem = null;

				//Assign transaction type ID
				voTransHis.TranTypeID = (new MST_TranTypeDS()).GetTranTypeID(pstrTransType);	

				//HACKED by Tuan TQ. 20 Dec, 2005. Set Transaction Date to current server date
				voTransHis.TransDate = (new UtilsDS()).GetDBDate();
				voTransHis.PurposeID = pintIssuePurposeID;

				// username
				voTransHis.Username = SystemProperty.UserName;

				switch(pstrTransType)
				{
					default:
					// 19-06-2006 dungla: refine quantity before save to database
						AssignValueForInventory(voTransHis);
						AssignCommonValue(voTransHis);

						if (voTransHis.BinOHQuantity < 0)
							voTransHis.BinOHQuantity = 0;
						if (voTransHis.LocationOHQuantity < 0)
							voTransHis.LocationOHQuantity = 0;
						if (voTransHis.MasLocOHQuantity < 0)
							voTransHis.MasLocOHQuantity = 0;						
						
						// Cal Add method of Transaction History DS to save to database		
						dsTransHis.UpdatebyRefMasterID(voTransHis);
						break;
				}
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
						
		/// <summary>
		/// Need to convert UM to StockUMID
		/// 
		/// 1. Prepare: Retrieve ITM_ProductVO, PO_PurchaseOrderReceiptMasterVO, PO_PurchaseOrderReceiptDetailVO value object from database based on ID from MST_TransactionHistoryVO.
		/// If PO_PurchaseOrderReceiptMasterVO.PurchaseOrderMasterID > 0
		/// - Retrieve PO_PurchaseOrderMasterVO from ID
		/// If PO_PurchaseOrderReceiptDetailVO.PurchaseOrderDetailID > 0
		/// - Retrieve PO_PurchaseOrderDetailVO from ID
		/// 
		/// 2. PartyID, PartyLocationID, CurrencyID, ExchangeRate from Purchase Order Master
		/// If PO_PurchaseOrderMasterVO != null
		/// - MST_TransactionHistoryVO.PartyID = PO_PurchaseOrderMasterVO.PartyID
		/// - MST_TransactionHistoryVO.PartyLocationID = PO_PurchaseOrderMasterVO.VendorLocID
		/// - MST_TransactionHistoryVO.CurrencyID = PO_PurchaseOrderMasterVO.CurrencyID
		/// - MST_TransactionHistoryVO.ExchangeRate = PO_PurchaseOrderMasterVO.ExchangeRate
		/// 
		/// 3. If PO_PurchaseOrderReceiptDetailVO != null
		/// - MST_TransactionHistoryVO.Lot = PO_PurchaseOrderReceiptDetailVO.Lot
		/// - MST_TransactionHistoryVO.Serial = PO_PurchaseOrderReceiptDetailVO.Serial
		/// 
		/// 4. If ITM_ProductVO != null and ITM_ProductVO.CostMethod = CostMethodEnum.AVG (Average Cost)
		/// - MST_TransactionHistoryVO.OldAvgCost = MST_TransactionHistoryVO.Cost
		/// - MST_TransactionHistoryVO.NewAvgCost = 
		/// ((IV_MasLocCache.OHQuantity * IV_MasLocCache.AvgCost) + (MST_TransactionHistoryVO.Quantity * PO_PurchaseOrderDetailVO.UnitPrice)) / (IV_MasLocCache.OHQuantity + MST_TransactionHistoryVO.Quantity)
		/// </summary>
		/// <param name="pobjTransactionHistory">MST_TransactionHistoryVO</param>
		public void AssignValueForPOReceipt( object pobjTransactionHistory)
		{
				decimal dUnitPrice = 0;
			MST_TransactionHistoryVO voTransHis = (MST_TransactionHistoryVO)pobjTransactionHistory;				
			//Get product info
			ITM_ProductVO voProduct = (ITM_ProductVO)(new ProductItemInfoBO()).GetProductInfo(voTransHis.ProductID);
				
			//Get PO Receipt Master infor
			DataRow drowPOMaster = null;
			DataRow drowPODetail = null;

			//DataRow drowPOReceiptMaster = dsCommon.GetPOReceiptMaster(voTransHis.RefMasterID);
			DataRow drowPOReceiptDetail = dsCommon.GetPOReceiptDetail(voTransHis.RefDetailID);
				
			//If PO_PurchaseOrderReceiptMasterVO.PurchaseOrderMasterID > 0,
			//Retrieve PO_PurchaseOrderMasterVO from ID
			if(drowPOReceiptDetail != null)
			{
				if(drowPOReceiptDetail[PO_PurchaseOrderReceiptDetailTable.PURCHASEORDERMASTERID_FLD] != null &&
					drowPOReceiptDetail[PO_PurchaseOrderReceiptDetailTable.PURCHASEORDERMASTERID_FLD] != DBNull.Value)
				{
					int intPOMasterID = 0;
					try
					{
						intPOMasterID = int.Parse(drowPOReceiptDetail[PO_PurchaseOrderReceiptDetailTable.PURCHASEORDERMASTERID_FLD].ToString());
					}
					catch{}
					drowPOMaster = dsCommon.GetPOMaster(intPOMasterID);
				}
			}
				
			//If PO_PurchaseOrderReceiptDetailVO.PurchaseOrderDetailID > 0
			//Retrieve PO_PurchaseOrderDetailVO from ID
			if(drowPOReceiptDetail != null)
			{
				if(drowPOReceiptDetail[PO_PurchaseOrderReceiptDetailTable.PURCHASEORDERRECEIPTDETAILID_FLD] != null &&
					drowPOReceiptDetail[PO_PurchaseOrderReceiptDetailTable.PURCHASEORDERRECEIPTDETAILID_FLD] != DBNull.Value)
				{
					int intPODetailID = 0;
					try
					{
						intPODetailID = int.Parse(drowPOReceiptDetail[PO_PurchaseOrderReceiptDetailTable.PURCHASEORDERDETAILID_FLD].ToString());
					}
					catch{}
					drowPODetail = dsCommon.GetPODetail(intPODetailID);
					//Get unit price of this product
					if(drowPODetail != null)
					{
						try
						{
							dUnitPrice = decimal.Parse(drowPODetail[PO_PurchaseOrderDetailTable.UNITPRICE_FLD].ToString());
						}
						catch{}
					}
				}
			}

			//Set PartyID, PartyLocationID, CurrencyID, ExchangeRate from Purchase Order Master to Trans History				
			if(drowPOMaster != null)
			{
				try
				{
					voTransHis.PartyID = int.Parse(drowPOMaster[PO_PurchaseOrderMasterTable.PARTYID_FLD].ToString());
				}
				catch{}
				try
				{
					voTransHis.PartyLocationID = int.Parse(drowPOMaster[PO_PurchaseOrderMasterTable.VENDORLOCID_FLD].ToString());
				}
				catch{}
				try
				{
					voTransHis.CurrencyID = int.Parse(drowPOMaster[PO_PurchaseOrderMasterTable.CURRENCYID_FLD].ToString());
				}
				catch{}
				try
				{
					voTransHis.ExchangeRate = decimal.Parse(drowPOMaster[PO_PurchaseOrderMasterTable.EXCHANGERATE_FLD].ToString());
				}
				catch{}
			}

			//Set value to Trans History If PO_PurchaseOrderReceiptDetailVO != null
			//MST_TransactionHistoryVO.Lot = PO_PurchaseOrderReceiptDetailVO.Lot
			//MST_TransactionHistoryVO.Serial = PO_PurchaseOrderReceiptDetailVO.Serial
			if(drowPOReceiptDetail != null)
			{
				voTransHis.Lot =  drowPOReceiptDetail[PO_PurchaseOrderReceiptDetailTable.LOT_FLD].ToString();
				voTransHis.Serial =  drowPOReceiptDetail[PO_PurchaseOrderReceiptDetailTable.SERIAL_FLD].ToString();
			}
				
			/// 4. If ITM_ProductVO != null and ITM_ProductVO.CostMethod = CostMethodEnum.AVG (Average Cost)
			/// - MST_TransactionHistoryVO.OldAvgCost = MST_TransactionHistoryVO.Cost
			/// - MST_TransactionHistoryVO.NewAvgCost = 
			/// ((IV_MasLocCache.OHQuantity * IV_MasLocCache.AvgCost) + (MST_TransactionHistoryVO.Quantity * PO_PurchaseOrderDetailVO.UnitPrice)) / (IV_MasLocCache.OHQuantity + MST_TransactionHistoryVO.Quantity)
			if(voProduct != null)
			{
				if(voProduct.CostMethod == (int)CostMethodEnum.AVG)
				{
					voTransHis.OldAvgCost = voTransHis.Cost;
					//Get masloc cache
					IV_MasLocCacheVO voMasLocCache = (IV_MasLocCacheVO)(new IV_MasLocCacheDS()).GetObjectVO(voTransHis.MasterLocationID, voProduct.ProductID, voTransHis.CCNID);
					if(voMasLocCache != null)
					{							
						voTransHis.NewAvgCost = (voMasLocCache.OHQuantity * voMasLocCache.AVGCost)
							+ (voTransHis.Quantity *  dUnitPrice)/(voMasLocCache.OHQuantity + voTransHis.Quantity);
					}
				}
			}

			//Re-assign TransactionHistory
			pobjTransactionHistory = voTransHis;
		}
		
		/// <summary>
		/// 1. Prepare: Retrieve ITM_ProductVO, SO_SaleOrderMasterVO, SO_SaleOrderDetailVO, SO_ReturnedGoodsMasterVO, SO_ReturnedGoodsDetailVO value object from database based on ID from MST_TransactionHistoryVO.
		/// 
		/// 2. PartyID, PartyLocationID retrieve from SO_ReturnedGoodsMasterVO
		/// 3. ExchangeRate, CurrencyID retrieve from SO_SaleOrderMasterVO
		/// 
		/// 4. If ITM_ProductVO != null and ITM_ProductVO.CostMethod = CostMethodEnum.AVG (Average Cost)
		/// - MST_TransactionHistoryVO.OldAvgCost = MST_TransactionHistoryVO.Cost
		/// - MST_TransactionHistoryVO.NewAvgCost = 
		/// ((IV_MasLocCache.OHQuantity * IV_MasLocCache.AvgCost) + (MST_TransactionHistoryVO.Quantity * SO_DetailCommitment.StdCost)) / (IV_MasLocCache.OHQuantity + MST_TransactionHistoryVO.Quantity)
		/// </summary>
		/// <param name="pobjTransactionHistory">MST_TransactionHistoryVO</param>
		public void AssignValueForRGReceive(object pobjTransactionHistory)
		{
			decimal dStdCost = 0;
			MST_TransactionHistoryVO voTransHis = (MST_TransactionHistoryVO)pobjTransactionHistory;				
			//Get product info
			ITM_ProductVO voProduct = (ITM_ProductVO)(new ProductItemInfoBO()).GetProductInfo(voTransHis.ProductID);
				
			//Get SO Receipt Master infor
			DataRow drowSOMaster = null;				

			DataRow drowSOReturnMaster = dsCommon.GetSOReturnGoodsMaster(voTransHis.RefMasterID);
			DataRow drowSOCommitDetail = dsCommon.GetSOCommitDetail(voTransHis.CCNID, voTransHis.MasterLocationID, voTransHis.ProductID);
				
			//If SO_ReturnGoodsMasterVO.SalerOrderMasterID > 0,
			//Retrieve SO_SaleOrderMasterVO from ID
			if(drowSOReturnMaster != null)
			{
				if(drowSOReturnMaster[SO_ReturnedGoodsMasterTable.SALEORDERMASTERID_FLD].ToString() != string.Empty)
				{
					drowSOMaster = dsCommon.GetSOMaster(int.Parse(drowSOReturnMaster[SO_ReturnedGoodsMasterTable.SALEORDERMASTERID_FLD].ToString()));
				}
			}				
			//PartyID, PartyLocationID retrieve from SO_ReturnedGoodsMasterVO
			if(drowSOReturnMaster != null)
			{
				try
				{
					voTransHis.PartyID = int.Parse(drowSOReturnMaster[SO_ReturnedGoodsMasterTable.PARTYID_FLD].ToString());
				}
				catch{}
				try
				{
					voTransHis.PartyLocationID = int.Parse(drowSOReturnMaster[SO_ReturnedGoodsMasterTable.PARTYLOCATIONID_FLD].ToString());
				}
				catch{}
			}

			//ExchangeRate, CurrencyID retrieve from SO_SaleOrderMasterVO
			if(drowSOMaster != null)
			{					
				try
				{
					voTransHis.CurrencyID = int.Parse(drowSOMaster[SO_SaleOrderMasterTable.CURRENCYID_FLD].ToString());
				}
				catch{}
				try
				{
					voTransHis.ExchangeRate = decimal.Parse(drowSOMaster[SO_SaleOrderMasterTable.EXCHANGERATE_FLD].ToString());
				}
				catch{}
			}
				
			// 4. If ITM_ProductVO != null and ITM_ProductVO.CostMethod = CostMethodEnum.AVG (Average Cost)
			// - MST_TransactionHistoryVO.OldAvgCost = MST_TransactionHistoryVO.Cost
			// - MST_TransactionHistoryVO.NewAvgCost = 
			// ((IV_MasLocCache.OHQuantity * IV_MasLocCache.AvgCost) + (MST_TransactionHistoryVO.Quantity * SO_DetailCommitment.StdCost)) / (IV_MasLocCache.OHQuantity + MST_TransactionHistoryVO.Quantity)
			if(voProduct != null)
			{
				if(voProduct.CostMethod == (int)CostMethodEnum.AVG)
				{
					voTransHis.OldAvgCost = voTransHis.Cost;
					//Get masloc cache
					IV_MasLocCacheVO voMasLocCache = (IV_MasLocCacheVO)(new IV_MasLocCacheDS()).GetObjectVO(voTransHis.MasterLocationID, voProduct.ProductID, voTransHis.CCNID);
						
					if(voMasLocCache != null & drowSOCommitDetail != null)
					{
						try
						{
							dStdCost = decimal.Parse(drowSOCommitDetail[SO_CommitInventoryDetailTable.STDCOST_FLD].ToString());
						}
						catch{}
						try
						{
							voTransHis.NewAvgCost = (voMasLocCache.OHQuantity * voMasLocCache.AVGCost)
								+ (voTransHis.Quantity *  dStdCost)/(voMasLocCache.OHQuantity + voTransHis.Quantity);
						}
						catch{}
					}
				}
			}

			//Re-assign TransactionHistory
			pobjTransactionHistory = voTransHis;
		}
		
		/// <summary>
		/// 1. Prepare: Retrieve ITM_ProductVO, PO_PurchaseOderMasterVO, PO_PurchaseOderDetailVO, 
		///	 PO_ReturnToVendorMasterVO, PO_ReturnToVendorDetailVO  value object from database based on ID from MST_TransactionHistoryVO.
		/// 
		/// 2. PartyID, PartyLocationID retrieve from PO_ReturnToVendorMasterVO
		/// 3. ExchangeRate, CurrencyID retrieve from PO_PurchaseOderMasterVO
		/// 
		/// 4. If ITM_ProductVO != null and ITM_ProductVO.CostMethod = CostMethodEnum.AVG (Average Cost)
		/// - MST_TransactionHistoryVO.OldAvgCost = MST_TransactionHistoryVO.Cost
		/// - MST_TransactionHistoryVO.NewAvgCost = 
		/// ((IV_MasLocCache.OHQuantity * IV_MasLocCache.AvgCost) + (MST_TransactionHistoryVO.Quantity * PO_PurchaseOrderDetailVO.UnitPrice)) / (IV_MasLocCache.OHQuantity + MST_TransactionHistoryVO.Quantity)
		/// </summary>
		/// <param name="pobjTransactionHistory">MST_TransactionHistoryVO</param>
		public void AssignValueForRTV( object pobjTransactionHistory)
		{
			decimal dUnitPrice = 0;
			MST_TransactionHistoryVO voTransHis = (MST_TransactionHistoryVO)pobjTransactionHistory;				
			//Get product info
			ITM_ProductVO voProduct = (ITM_ProductVO)(new ProductItemInfoBO()).GetProductInfo(voTransHis.ProductID);
				
			//Get SO Receipt Master infor
			DataRow drowPOMaster = null;
			DataRow drowPODetail = null;
				
			DataRow drowPOReturn2VendorMaster = dsCommon.GetPOReturnToVendorMaster(voTransHis.RefMasterID);
			DataRow drowPOReturn2VendorDetail = dsCommon.GetPOReturnToVendorDetail(voTransHis.RefDetailID);				
				
			//If PO_Return2Vendor not null, get PO_PurchaseOrderMaster, PO_PurchaseOrderDetail
			if(drowPOReturn2VendorMaster != null)
			{
				if(drowPOReturn2VendorMaster[PO_ReturnToVendorMasterTable.PURCHASEORDERMASTERID_FLD] != null)
				{
					drowPOMaster = dsCommon.GetPOMaster(int.Parse(drowPOReturn2VendorMaster[PO_ReturnToVendorMasterTable.PURCHASEORDERMASTERID_FLD].ToString()));
					drowPODetail = dsCommon.GetPODetail(int.Parse(drowPOReturn2VendorMaster[PO_ReturnToVendorMasterTable.PURCHASEORDERMASTERID_FLD].ToString()), voTransHis.ProductID);
				}
				if(drowPODetail != null)
				{
					try
					{
						dUnitPrice = decimal.Parse(drowPODetail[PO_PurchaseOrderDetailTable.UNITPRICE_FLD].ToString());
					}
					catch{}
				}
			}
				
			//PartyID, PartyLocationID, ExchangeRate, CurrencyID retrieve from PO_PurchaseOrder
			if(drowPOMaster != null)
			{
				try
				{
					voTransHis.PartyID = int.Parse(drowPOMaster[PO_PurchaseOrderMasterTable.PARTYID_FLD].ToString());
				}
				catch{}
				try
				{
					voTransHis.PartyLocationID = int.Parse(drowPOMaster[PO_PurchaseOrderMasterTable.VENDORLOCID_FLD].ToString());
				}
				catch{}

				try
				{
					voTransHis.CurrencyID = int.Parse(drowPOMaster[PO_PurchaseOrderMasterTable.CURRENCYID_FLD].ToString());
				}
				catch{}
				if(!drowPOMaster[PO_PurchaseOrderMasterTable.EXCHANGERATE_FLD].Equals(DBNull.Value) && drowPOMaster[PO_PurchaseOrderMasterTable.EXCHANGERATE_FLD].ToString().Trim() != string.Empty)
					voTransHis.ExchangeRate = decimal.Parse(drowPOMaster[PO_PurchaseOrderMasterTable.EXCHANGERATE_FLD].ToString());
			}
				
			// 4. If ITM_ProductVO != null and ITM_ProductVO.CostMethod = CostMethodEnum.AVG (Average Cost)
			// - MST_TransactionHistoryVO.OldAvgCost = MST_TransactionHistoryVO.Cost
			// - MST_TransactionHistoryVO.NewAvgCost = 
			// ((IV_MasLocCache.OHQuantity * IV_MasLocCache.AvgCost) + (MST_TransactionHistoryVO.Quantity * PO_PurchaseOrderDetailVO.UnitPrice)) / (IV_MasLocCache.OHQuantity + MST_TransactionHistoryVO.Quantity)
			if(voProduct != null)
			{
				if(voProduct.CostMethod == (int)CostMethodEnum.AVG)
				{
					voTransHis.OldAvgCost = voTransHis.Cost;
					//Get masloc cache
					IV_MasLocCacheVO voMasLocCache = (IV_MasLocCacheVO)(new IV_MasLocCacheDS()).GetObjectVO(voTransHis.MasterLocationID, voProduct.ProductID, voTransHis.CCNID);
						
					if(voMasLocCache != null)
					{
						try
						{
							voTransHis.NewAvgCost = (voMasLocCache.OHQuantity * voMasLocCache.AVGCost)
							+ (voTransHis.Quantity *  dUnitPrice)/(voMasLocCache.OHQuantity + voTransHis.Quantity);
						}
						catch{}
					}
				}
			}

			//Re-assign TransactionHistory
			pobjTransactionHistory = voTransHis;
		}
		
		/// <summary>
		/// 1. Prepare: Retrieve ITM_ProductVO, MasterVO, DetailVO(if any) value object from database based on ID from MST_TransactionHistoryVO.
		/// 
		/// 2. PartyID, PartyLocationID = 0
		/// 3. ExchangeRate, CurrencyID = 0
		/// 
		/// 4. 
		/// - MST_TransactionHistoryVO.OldAvgCost = 0
		/// 4.1 If TransType is Work Order Complettion and ITM_Product.CostMethod = CostMethod.AVG
		/// - MST_TransactionHistoryVO.NewAvgCost =  WorkOrderDetail.EstCost
		/// </summary>
		/// <param name="pobjTransactionHistory">MST_TransactionHistoryVO</param>
		public void AssignValueForInventory( object pobjTransactionHistory)
		{
			decimal dEstimateCost = 0;
			MST_TransactionHistoryVO voTransHis = (MST_TransactionHistoryVO)pobjTransactionHistory;				
			//Get product info
			ITM_ProductVO voProduct = (ITM_ProductVO)(new ProductItemInfoBO()).GetProductInfo(voTransHis.ProductID);
				
			//Get WO Detail infor
			DataRow drowPODetail = dsCommon.GetWODetail(voTransHis.RefDetailID);
			if(drowPODetail != null)
			{
				try
				{
					dEstimateCost = decimal.Parse(drowPODetail[PRO_WorkOrderDetailTable.ESTCST_FLD].ToString());
				}
				catch{}
			}
				
			//HACKED -- Rem by Tuan TQ: fix error 3205 for Tuan DM. 27 Dec, 2005.
			//voTransHis.PartyID = 0;
			//voTransHis.PartyLocationID = 0;
			//voTransHis.CurrencyID = 0;
			//voTransHis.ExchangeRate = 0;
			//voTransHis.OldAvgCost = 0;
			//End hacked

			//If TransType is Work Order Complettion and ITM_Product.CostMethod = CostMethod.AVG
			//MST_TransactionHistoryVO.NewAvgCost =  WorkOrderDetail.EstCost
			if(voProduct != null) 
			{
				if(voProduct.CostMethod == (int)CostMethodEnum.AVG)
				{						
					voTransHis.NewAvgCost = dEstimateCost;
				}
			}

			//Re-assign TransactionHistory
			pobjTransactionHistory = voTransHis;
		}
		
		/// <summary>
		/// 1. Prepare: Retrieve ITM_ProductVO, PO_PurchaseOderMasterVO, 
		///	   PO_PurchaseOderDetailVO, PO_ReturnToVendorMasterVO, 
		///	   PO_ReturnToVendorDetailVO  value object from database based on ID from MST_TransactionHistoryVO.
		/// 
		/// 2. PartyID, PartyLocationID retrieve from PO_ReturnToVendorMasterVO
		/// 3. ExchangeRate, CurrencyID retrieve from PO_PurchaseOderMasterVO
		/// 
		/// 4. If ITM_ProductVO != null and ITM_ProductVO.CostMethod = CostMethodEnum.AVG (Average Cost)
		/// - MST_TransactionHistoryVO.OldAvgCost = MST_TransactionHistoryVO.Cost
		/// - MST_TransactionHistoryVO.NewAvgCost = 
		/// ((IV_MasLocCache.OHQuantity * IV_MasLocCache.AvgCost) + (MST_TransactionHistoryVO.Quantity * PO_PurchaseOrderDetailVO.UnitPrice))
		///  / (IV_MasLocCache.OHQuantity + MST_TransactionHistoryVO.Quantity)
		/// 
		/// 5. If PO_PurchaseOderDetailVO != null
		/// - MST_TransactionHistoryVO.BuySellCost = PO_PurchaseOderDetailVO.UnitPrice
		/// </summary>
		/// <param name="pobTransactionHistory">MST_TransactionHistoryVO</param>
		public void AssignValueForConfirmShipment( object pobjTransactionHistory)
		{
			decimal dUnitPrice = 0;
			MST_TransactionHistoryVO voTransHis = (MST_TransactionHistoryVO)pobjTransactionHistory;
			//Get product info
			ITM_ProductVO voProduct = (ITM_ProductVO)(new ProductItemInfoBO()).GetProductInfo(voTransHis.ProductID);
				
			//Get SO Receipt Master infor
			DataRow drowPOMaster = null;
			DataRow drowPODetail = null;
				
			DataRow drowPOReturn2VendorMaster = dsCommon.GetPOReturnToVendorMaster(voTransHis.RefMasterID);
			DataRow drowPOReturn2VendorDetail = dsCommon.GetPOReturnToVendorDetail(voTransHis.RefDetailID);				
				
			//If PO_Return2Vendor not null, get PO_PurchaseOrderMaster, PO_PurchaseOrderDetail
			if(drowPOReturn2VendorMaster != null)
			{
				if(drowPOReturn2VendorMaster[PO_ReturnToVendorMasterTable.PURCHASEORDERMASTERID_FLD] != null)
				{
					drowPOMaster = dsCommon.GetPOMaster(int.Parse(drowPOReturn2VendorMaster[PO_ReturnToVendorMasterTable.PURCHASEORDERMASTERID_FLD].ToString()));
					drowPODetail = dsCommon.GetPODetail(int.Parse(drowPOReturn2VendorMaster[PO_ReturnToVendorMasterTable.PURCHASEORDERMASTERID_FLD].ToString()), voTransHis.ProductID);
				}
				if(drowPODetail != null)
				{
					try
					{
						dUnitPrice = decimal.Parse(drowPODetail[PO_PurchaseOrderDetailTable.UNITPRICE_FLD].ToString());
					}
					catch{}
				}
			}
				
			//PartyID, PartyLocationID, ExchangeRate, CurrencyID retrieve from PO_PurchaseOrder
			if(drowPOMaster != null)
			{
				try
				{
					voTransHis.PartyID = int.Parse(drowPOMaster[PO_PurchaseOrderMasterTable.PARTYID_FLD].ToString());
				}
				catch{}
				try
				{
					voTransHis.PartyLocationID = int.Parse(drowPOMaster[PO_PurchaseOrderMasterTable.VENDORLOCID_FLD].ToString());
				}
				catch{}

				try
				{
					voTransHis.CurrencyID = int.Parse(drowPOMaster[PO_PurchaseOrderMasterTable.CURRENCYID_FLD].ToString());
				}
				catch{}
				try
				{
					voTransHis.ExchangeRate = decimal.Parse(drowPOMaster[PO_PurchaseOrderMasterTable.EXCHANGERATE_FLD].ToString());
				}
				catch{}
			}				
				
			// 4. If ITM_ProductVO != null and ITM_ProductVO.CostMethod = CostMethodEnum.AVG (Average Cost)
			// - MST_TransactionHistoryVO.OldAvgCost = MST_TransactionHistoryVO.Cost
			// - MST_TransactionHistoryVO.NewAvgCost = 
			// ((IV_MasLocCache.OHQuantity * IV_MasLocCache.AvgCost) + (MST_TransactionHistoryVO.Quantity * PO_PurchaseOrderDetailVO.UnitPrice)) / (IV_MasLocCache.OHQuantity + MST_TransactionHistoryVO.Quantity)
			if(voProduct != null)
			{
				if(voProduct.CostMethod == (int)CostMethodEnum.AVG)
				{
					voTransHis.OldAvgCost = voTransHis.Cost;
					//Get masloc cache
					IV_MasLocCacheVO voMasLocCache = (IV_MasLocCacheVO)(new IV_MasLocCacheDS()).GetObjectVO(voTransHis.MasterLocationID, voProduct.ProductID, voTransHis.CCNID);
						
					if(voMasLocCache != null)
					{							
						try
						{
							voTransHis.NewAvgCost = (voMasLocCache.OHQuantity * voMasLocCache.AVGCost)
							+ (voTransHis.Quantity *  dUnitPrice)/(voMasLocCache.OHQuantity + voTransHis.Quantity);
						}
						catch{}
					}
				}
			}

			//5. If PO_PurchaseOderDetailVO != null
			//MST_TransactionHistoryVO.BuySellCost = PO_PurchaseOderDetailVO.UnitPrice
			voTransHis.BuySellCost = dUnitPrice;

			//Re-assign TransactionHistory
			pobjTransactionHistory = voTransHis;
		}

		/// <summary>
		/// Assign common value for all transaction: On-hand quantity and cost
		/// 
		/// 1. MasLocOHQuantity
		/// - If MST_TransactionHistoryVO.Lot != null: MST_TransactionHistoryVO.MasLocOHQuantity = IV_MasterLocationCacheDS.GetQuantityOnHandByLot(MST_TransactionHistoryVO.CCNID, MST_TransactionHistoryVO.MasterLocationID, MST_TransactionHistoryVO.ProductID, MST_TransactionHistoryVO.Lot)
		/// - Else MST_TransactionHistoryVO.MasLocOHQuantity = IV_MasterLocationCacheDS.GetQuantityOnHand(MST_TransactionHistoryVO.CCNID, MST_TransactionHistoryVO.MasterLocationID, MST_TransactionHistoryVO.ProductID)
		/// 
		/// 2. LocationOHQuantity
		/// If MST_TransactionHistoryVO.LocationID > 0
		/// - If MST_TransactionHistoryVO.Lot != null: MST_TransactionHistoryVO.LocationOHQuantity = IV_LocationCacheDS.GetQuantityOnHandByLot(MST_TransactionHistoryVO.CCNID, MST_TransactionHistoryVO.MasterLocationID, MST_TransactionHistoryVO.LocationID, MST_TransactionHistoryVO.ProductID, MST_TransactionHistoryVO.Lot)
		/// - Else MST_TransactionHistoryVO.LocationOHQuantity = IV_LocationCacheDS.GetQuantityOnHand(MST_TransactionHistoryVO.CCNID, MST_TransactionHistoryVO.MasterLocationID, MST_TransactionHistoryVO.LocationID, MST_TransactionHistoryVO.ProductID)
		/// 
		/// 3. BinOHQuantity
		/// If MST_TransactionHistoryVO.BinID > 0
		/// - If MST_TransactionHistoryVO.Lot != null: MST_TransactionHistoryVO.BinOHQuantity = IV_BinCacheDS.GetQuantityOnHandByLot(MST_TransactionHistoryVO.CCNID, MST_TransactionHistoryVO.MasterLocationID, MST_TransactionHistoryVO.LocationID, MST_TransactionHistoryVO.BinID, MST_TransactionHistoryVO.ProductID, MST_TransactionHistoryVO.Lot)
		/// - Else MST_TransactionHistoryVO.BinOHQuantity = IV_BinCacheDS.GetQuantityOnHand(MST_TransactionHistoryVO.CCNID, MST_TransactionHistoryVO.MasterLocationID, MST_TransactionHistoryVO.BinID, MST_TransactionHistoryVO.LocationID, MST_TransactionHistoryVO.ProductID)
		/// 
		/// 4. Cost
		/// If ProductID > 0
		/// MST_TransactionHistoryVO.Cost = ITM_CostDS.GetItemCostTotalAmount21ByProductIDCCNID(pintProductID, pintCCNID)
		/// </summary>
		public void AssignCommonValue( object pobjTransactionHistory)
		{
			MST_TransactionHistoryVO voTransHis = (MST_TransactionHistoryVO)pobjTransactionHistory;				
			//1. MasLocOHQuantity
			//- If MST_TransactionHistoryVO.Lot != null: MST_TransactionHistoryVO.MasLocOHQuantity = IV_MasterLocationCacheDS.GetQuantityOnHandByLot(MST_TransactionHistoryVO.CCNID, MST_TransactionHistoryVO.MasterLocationID, MST_TransactionHistoryVO.ProductID, MST_TransactionHistoryVO.Lot)
			//- Else MST_TransactionHistoryVO.MasLocOHQuantity = IV_MasterLocationCacheDS.GetQuantityOnHand(MST_TransactionHistoryVO.CCNID, MST_TransactionHistoryVO.MasterLocationID, MST_TransactionHistoryVO.ProductID)
			if(voTransHis.Lot != null && voTransHis.Lot != string.Empty)
			{
				voTransHis.MasLocOHQuantity = (new IV_MasLocCacheDS()).GetQuantityOnHandByLot(voTransHis.CCNID, voTransHis.MasterLocationID, voTransHis.ProductID, voTransHis.Lot);
				voTransHis.MasLocCommitQuantity = (new IV_MasLocCacheDS()).GetCommitQuantity(voTransHis.CCNID, voTransHis.MasterLocationID, voTransHis.Lot, voTransHis.ProductID);
			}
			else
			{
				voTransHis.MasLocOHQuantity = (new IV_MasLocCacheDS()).GetQuantityOnHand(voTransHis.CCNID, voTransHis.MasterLocationID, voTransHis.ProductID);
				voTransHis.MasLocCommitQuantity = (new IV_MasLocCacheDS()).GetCommitQuantity(voTransHis.CCNID, voTransHis.MasterLocationID, voTransHis.ProductID);
			}
				
			// 2. LocationOHQuantity
			// If MST_TransactionHistoryVO.LocationID > 0
			// - If MST_TransactionHistoryVO.Lot != null: MST_TransactionHistoryVO.LocationOHQuantity = IV_LocationCacheDS.GetQuantityOnHandByLot(MST_TransactionHistoryVO.CCNID, MST_TransactionHistoryVO.MasterLocationID, MST_TransactionHistoryVO.LocationID, MST_TransactionHistoryVO.ProductID, MST_TransactionHistoryVO.Lot)
			// - Else MST_TransactionHistoryVO.LocationOHQuantity = IV_LocationCacheDS.GetQuantityOnHand(MST_TransactionHistoryVO.CCNID, MST_TransactionHistoryVO.MasterLocationID, MST_TransactionHistoryVO.LocationID, MST_TransactionHistoryVO.ProductID)
			if(voTransHis.LocationID > 0 )
			{
				if(voTransHis.Lot != null && voTransHis.Lot != string.Empty)
				{
					voTransHis.LocationOHQuantity = (new IV_LocationCacheDS()).GetQuantityOnHandByLot(voTransHis.CCNID, voTransHis.MasterLocationID, voTransHis.LocationID, voTransHis.ProductID, voTransHis.Lot);
					voTransHis.LocationCommitQuantity = (new IV_LocationCacheDS()).GetCommitQuantity(voTransHis.CCNID, voTransHis.MasterLocationID, voTransHis.LocationID, voTransHis.Lot, voTransHis.ProductID);
				}
				else
				{
					voTransHis.LocationOHQuantity = (new IV_LocationCacheDS()).GetQuantityOnHand(voTransHis.CCNID, voTransHis.MasterLocationID, voTransHis.LocationID, voTransHis.ProductID);
					voTransHis.LocationCommitQuantity = (new IV_LocationCacheDS()).GetCommitQuantity(voTransHis.CCNID, voTransHis.MasterLocationID, voTransHis.LocationID, voTransHis.ProductID);
				}
			}
				
			// 3. BinOHQuantity
			// If MST_TransactionHistoryVO.BinID > 0
			// - If MST_TransactionHistoryVO.Lot != null: MST_TransactionHistoryVO.BinOHQuantity = IV_BinCacheDS.GetQuantityOnHandByLot(MST_TransactionHistoryVO.CCNID, MST_TransactionHistoryVO.MasterLocationID, MST_TransactionHistoryVO.LocationID, MST_TransactionHistoryVO.BinID, MST_TransactionHistoryVO.ProductID, MST_TransactionHistoryVO.Lot)
			// - Else MST_TransactionHistoryVO.BinOHQuantity = IV_BinCacheDS.GetQuantityOnHand(MST_TransactionHistoryVO.CCNID, MST_TransactionHistoryVO.MasterLocationID, MST_TransactionHistoryVO.BinID, MST_TransactionHistoryVO.LocationID, MST_TransactionHistoryVO.ProductID)
				
			if(voTransHis.BinID > 0 )
			{
				if(voTransHis.Lot != null &&  voTransHis.Lot != string.Empty)
				{
					voTransHis.BinOHQuantity = (new IV_BinCacheDS()).GetQuantityOnHandByLot(voTransHis.CCNID, voTransHis.MasterLocationID, voTransHis.LocationID, voTransHis.BinID, voTransHis.ProductID, voTransHis.Lot);
					voTransHis.BinCommitQuantity = (new IV_BinCacheDS()).GetCommitQuantity(voTransHis.CCNID, voTransHis.MasterLocationID, voTransHis.LocationID, voTransHis.BinID, voTransHis.Lot, voTransHis.ProductID);
				}
				else
				{
					voTransHis.BinOHQuantity = (new IV_BinCacheDS()).GetQuantityOnHand(voTransHis.CCNID, voTransHis.MasterLocationID, voTransHis.LocationID, voTransHis.BinID, voTransHis.ProductID);
					voTransHis.BinCommitQuantity = (new IV_BinCacheDS()).GetCommitQuantity(voTransHis.CCNID, voTransHis.MasterLocationID, voTransHis.LocationID, voTransHis.BinID, voTransHis.ProductID);
				}
			}
		}
		
		public DataSet ListTransactionHistory(int pintID)
		{
			MST_TransactionHistoryDS dsHist = new MST_TransactionHistoryDS();
			return dsHist.List(pintID);
		}
		#endregion Transaction History Related Methods					
	}
}