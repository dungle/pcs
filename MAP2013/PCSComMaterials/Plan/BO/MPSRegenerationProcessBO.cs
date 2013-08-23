using System;
using System.Collections;
using System.Data;
using System.Diagnostics;

using System.Globalization;
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
	public interface IMPSRegenerationProcessBO
	{
		/// <summary>
		/// This method will planing for all MPS Item from top level Item in BOM structure to lowest level. After scanning succeed, generate CPO if needed
		/// See Activity Diagram of MPS Regeneration Process for detail.
		/// </summary>
		DataSet RunMPSProcess(int pintCCNID, int pintCylceID);
		DataSet RunMPSProcessOffline(int pintCCNID, int pintCylceID);
		long AddAndReturnID(object pObjectDetail);
		bool CheckCalendarConfig(object pobjCycleOption);
		DataTable GetCycleDetail(int pintCycleMasterID);
		DataTable GetAllProducts(int pintCCNID);
		DataTable GetDayOfWeek();
		DataTable GetHolidays();
		DataTable GetAvailableQuantityForPlan(int pintCCNID, int pintMasterLocationID);
		DataTable RetrieveSaleOrders(int pintCCNID, int pintMasterLocationID, DateTime pdtmAsOfDate, DateTime pdtmDueDate);
		DataTable RetrieveParents(int pintCCNID, int pintMasterLocationID, DateTime pdtmAsOfDate, DateTime pdtmDueDate);
		DataTable RetrieveSupplyFromWO(int pintCCNID, int pintMasterLocationID, DateTime pdtmAsOfDate, DateTime pdtmDueDate);
		DataTable RetriveSupplyFromPO(int pintCCNID, int pintMasterLocationID, DateTime pdtmAsOfDate, DateTime pdtmDueDate);
		void DeleteCPOs(int pintCCNID, int pintCycleID);
		DataTable GetTotalSO(int pintCCNID, DateTime pdtmFromDate, DateTime pdtmToDate, int pintWorkCenterID);
		DataTable GetTotalPO(int pintCCNID, DateTime pdtmFromDate, DateTime pdtmToDate, int pintWorkCenterID);
		DataTable GetTotalWO(int pintCCNID, DateTime pdtmFromDate, DateTime pdtmToDate, int pintWorkCenterID);
		DataTable GetTotalDemandWO(int pintCCNID, DateTime pdtmFromDate, DateTime pdtmToDate, int pintWorkCenterID);
	}

	/// <summary>
	/// Summary description for MPSRegenerationProcessBO.
	/// </summary>
	
	public class MPSRegenerationProcessBO : IMPSRegenerationProcessBO
	{
		private const string THIS = "PCSComMaterials.Plan.BO.MPSRegenerationProcessBO";
		private ITM_ProductDS dsProduct = new ITM_ProductDS();
		private MPSRegenerationProcessDS dsRegenerationProcess = new MPSRegenerationProcessDS();
		private MTR_CPODS dsCPO = new MTR_CPODS();
		private UtilsBO boUtils = new UtilsBO();

		public MPSRegenerationProcessBO()
		{
		}

		/// <summary>
		/// Insert a new record into database
		/// </summary>
		public void Add(object pObjectDetail)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Insert a new record into database and return new ID
		/// </summary>
	
		public long AddAndReturnID(object pObjectDetail)
		{
			MTR_CPODS dsCPO = new MTR_CPODS();
			return dsCPO.AddAndReturnID(pObjectDetail);
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
		/// This method will planing for all MPS Item from top level Item in BOM structure to lowest level. After scanning succeed, generate CPO if needed
		/// See Activity Diagram of MPS Regeneration Process for detail.
		/// </summary>
	
		public DataSet RunMPSProcess(int pintCCNID, int pintCylceID)
		{
			const string METHOD_NAME = THIS + ".RunMPSProcess()";
			try
			{
				DataSet dstResult = new DataSet();
				DataTable dtbMPSCycleOptionDetail = new DataTable();
				DateTime dtmDueDate;
				DataTable dtbTopLevelItems = new DataTable();
				MTR_MPSCycleOptionMasterDS dsMPSCycleOptionMaster = new MTR_MPSCycleOptionMasterDS();
				MTR_MPSCycleOptionDetailDS dsMPSCycleOptionDetail = new MTR_MPSCycleOptionDetailDS();
				MPSRegenerationProcessDS dsMPSRegenerationProcess = new MPSRegenerationProcessDS();
				IV_MasLocCacheDS dsMasLocCache = new IV_MasLocCacheDS();
				MTR_CPODS dsCPO = new MTR_CPODS();
				// get MPSCycleOptionMaster object
				MTR_MPSCycleOptionMasterVO voMPSCycleOption = (MTR_MPSCycleOptionMasterVO) dsMPSCycleOptionMaster.GetObjectVO(pintCylceID);

				if (!CheckCalendarConfig(voMPSCycleOption))
				{
					throw new PCSBOException(ErrorCode.MESSAGE_DCP_CONFIG_CALENDAR_FROM_X_TO_Y, METHOD_NAME, null);
				}
				// get all detail data of MPSCycleOptionMaster
				dtbMPSCycleOptionDetail = dsMPSCycleOptionDetail.List(pintCylceID).Tables[0];
				// calculate DueDate = AsOfDate + PlanningHorizon
				dtmDueDate = voMPSCycleOption.AsOfDate.AddDays(voMPSCycleOption.PlanHorizon);
				// retrieve top level items in BOM structure 
				// from all item of selected CCN
				dtbTopLevelItems = dsMPSRegenerationProcess.GetTopLevelItems(pintCCNID);
				// if system is not setup any BOM, we will get all product from system of CCNID;
				if (dtbTopLevelItems.Rows.Count == 0)
					dtbTopLevelItems = dsMPSRegenerationProcess.GetAllProducts(pintCCNID);
				// delete all generated CPO of selected Cycle Option
				dsCPO.Delete(voMPSCycleOption.CCNID, voMPSCycleOption.MPSCycleOptionMasterID, true);
				ArrayList arrCPO = new ArrayList();
				arrCPO.TrimToSize();
				ArrayList arrDateTimes = new ArrayList();
				// start planning from top level items for each master location
				foreach (DataRow drowItem in dtbMPSCycleOptionDetail.Rows)
				{
					int intMasterLocationID = int.Parse(drowItem[MTR_MPSCycleOptionDetailTable.MASTERLOCATIONID_FLD].ToString());
					bool blnOnHand = Convert.ToBoolean(drowItem[MTR_MPSCycleOptionDetailTable.ONHAND_FLD].ToString());
					bool blnDemandWO = Convert.ToBoolean(drowItem[MTR_MPSCycleOptionDetailTable.DEMANDWO_FLD].ToString());
					bool blnSupplyWO = Convert.ToBoolean(drowItem[MTR_MPSCycleOptionDetailTable.SUPPLYWO_FLD].ToString());
					bool blnPO = Convert.ToBoolean(drowItem[MTR_MPSCycleOptionDetailTable.PURCHASEORDER_FLD].ToString());
					bool blnSO = Convert.ToBoolean(drowItem[MTR_MPSCycleOptionDetailTable.SALEORDER_FLD].ToString());
					DataTable dtbSaleOrders = null;
					DataTable dtbDemandWorkOrders = null;
					DataTable dtbSupplyWorkOrders = null;
					DataTable dtbPurchaseOrders = null;
					DataTable dtbInventory = null;

					// if user did not select any option for this master location
					// go to next master location in list
					if (!blnOnHand && !blnDemandWO && !blnSupplyWO && !blnPO && !blnSO)
						continue;

					#region Prepare data

					// if Onhand is checked we calculate NetAvailableQuantity with Iventory Onhand
					if (blnOnHand)
						dtbInventory = dsMasLocCache.GetAvailableQuantityForPlan(pintCCNID, intMasterLocationID);
					// retrieve all demand from sale order(s) which order current product
					// and have the schedule date in range of AsOfDate and DueDate
					if (blnSO)
						dtbSaleOrders = dsMPSRegenerationProcess.RetrieveSaleOrders(pintCCNID, intMasterLocationID, voMPSCycleOption.AsOfDate, dtmDueDate);
					// retrieve all demand from work order(s) of current product's parent(s)
					// which have the start date in range of AsOfDate and DueDate
					if (blnDemandWO)
						dtbDemandWorkOrders = dsMPSRegenerationProcess.RetrieveParents(pintCCNID, intMasterLocationID, voMPSCycleOption.AsOfDate, dtmDueDate);
					// retrieve all supply from work order(s) in process of current product
					// which have the due date in range of AsOfDate and DueDate
					if (blnSupplyWO)
						dtbSupplyWorkOrders = dsMPSRegenerationProcess.RetrieveSupplyFromWO(pintCCNID, intMasterLocationID, voMPSCycleOption.AsOfDate, dtmDueDate);
					// retrieve all supply from purchase order(s) which order current product
					// and have the delivery date in range of AsOfDate and DueDate
					if (blnPO)
						dtbPurchaseOrders = dsMPSRegenerationProcess.RetriveSupplyFromPO(pintCCNID, intMasterLocationID, voMPSCycleOption.AsOfDate, dtmDueDate);

					#endregion

					// get the demand date from sale order
					DateTime dtmPlanDate = DateTime.MinValue;
					if (blnSO)
					{
						foreach (DataRow drowSO in dtbSaleOrders.Rows)
						{
							dtmPlanDate = DateTime.Parse(drowSO[SO_DeliveryScheduleTable.SCHEDULEDATE_FLD].ToString().Trim());
							if (!arrDateTimes.Contains(dtmPlanDate))
							{
								// update to DataRow
								drowSO[SO_DeliveryScheduleTable.SCHEDULEDATE_FLD] = dtmPlanDate;
								arrDateTimes.Add(dtmPlanDate);
							}
						}
					}
					// get the demand date from work order
					if (blnDemandWO)
					{
						foreach (DataRow drowWO in dtbDemandWorkOrders.Rows)
						{
							dtmPlanDate = DateTime.Parse(drowWO[PRO_WorkOrderDetailTable.STARTDATE_FLD].ToString().Trim());
							if (!arrDateTimes.Contains(dtmPlanDate))
							{
								// update to DataRow
								drowWO[PRO_WorkOrderDetailTable.STARTDATE_FLD] = dtmPlanDate;
								arrDateTimes.Add(dtmPlanDate);
							}
						}
					}
					arrDateTimes.Sort();
					arrDateTimes.TrimToSize();

					// remove milliseconds from Schedule date and due date of PO/WO
					if (blnPO)
					{
						foreach (DataRow drowPO in dtbPurchaseOrders.Rows)
							drowPO[PO_DeliveryScheduleTable.SCHEDULEDATE_FLD] = DateTime.Parse(drowPO[PO_DeliveryScheduleTable.SCHEDULEDATE_FLD].ToString().Trim());
					}
					if (blnSupplyWO)
					{
						foreach (DataRow drowWO in dtbSupplyWorkOrders.Rows)
							drowWO[PRO_WorkOrderDetailTable.DUEDATE_FLD] = DateTime.Parse(drowWO[PRO_WorkOrderDetailTable.DUEDATE_FLD].ToString().Trim());
					}

					//GeneratePlan(dtbTopLevelItems, intMasterLocationID, voMPSCycleOption.AsOfDate, dtmDueDate, voMPSCycleOption, 0, dtbInventory, dtbSaleOrders, dtbDemandWorkOrders, dtbSupplyWorkOrders, dtbPurchaseOrders, arrCPO);
					GeneratePlan(dtbTopLevelItems, intMasterLocationID, arrDateTimes, voMPSCycleOption, 0, dtbInventory, dtbSaleOrders, dtbDemandWorkOrders, dtbSupplyWorkOrders, dtbPurchaseOrders, arrCPO);
				}
				// update MPS Gen Date of Cycle Option Master
				voMPSCycleOption.MPSGenDate = DateTime.Now;
				dsMPSCycleOptionMaster.Update(voMPSCycleOption);
				return dstResult;
			}
			catch (PCSBOException ex)
			{
				throw ex;
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
		/// This method will planing for all MPS Item from top level Item in BOM structure to lowest level. After scanning succeed, generate CPO if needed
		/// See Activity Diagram of MPS Regeneration Process for detail.
		/// </summary>
	
		public DataSet RunMPSProcessOffline(int pintCCNID, int pintCylceID)
		{
			const string METHOD_NAME = THIS + ".RunMPSProcess()";
			DataSet dstResult = new DataSet();
			DataTable dtbMPSCycleOptionDetail = new DataTable();
			DateTime dtmDueDate;
			DataTable dtbProducts = new DataTable();
			MTR_MPSCycleOptionMasterDS dsMPSCycleOptionMaster = new MTR_MPSCycleOptionMasterDS();
			MTR_MPSCycleOptionDetailDS dsMPSCycleOptionDetail = new MTR_MPSCycleOptionDetailDS();
			MPSRegenerationProcessDS dsMPSRegenerationProcess = new MPSRegenerationProcessDS();
			IV_MasLocCacheDS dsMasLocCache = new IV_MasLocCacheDS();
			MTR_CPODS dsCPO = new MTR_CPODS();
			// get MPSCycleOptionMaster object
			MTR_MPSCycleOptionMasterVO voMPSCycleOption = (MTR_MPSCycleOptionMasterVO) dsMPSCycleOptionMaster.GetObjectVO(pintCylceID);

			if (!CheckCalendarConfig(voMPSCycleOption))
			{
				throw new PCSBOException(ErrorCode.MESSAGE_DCP_CONFIG_CALENDAR_FROM_X_TO_Y, METHOD_NAME, null);
			}
			UtilsDS dsUtil = new UtilsDS();
			// get all detail data of MPSCycleOptionMaster
			dtbMPSCycleOptionDetail = dsMPSCycleOptionDetail.List(pintCylceID).Tables[0];
			// calculate DueDate = AsOfDate + PlanningHorizon
			dtmDueDate = voMPSCycleOption.AsOfDate.AddDays(voMPSCycleOption.PlanHorizon);
			dtbProducts = dsMPSRegenerationProcess.GetAllProducts(pintCCNID);
			// delete all generated CPO of selected Cycle Option
			dsCPO.Delete(voMPSCycleOption.CCNID, voMPSCycleOption.MPSCycleOptionMasterID, true);

			#region create CPO table with schema only

			DataTable dtbCPOs = new DataTable(MTR_CPOTable.TABLE_NAME);
			dtbCPOs.Columns.Add(new DataColumn(MTR_CPOTable.CPOID_FLD, typeof (int)));
			dtbCPOs.Columns[MTR_CPOTable.CPOID_FLD].AllowDBNull = false;
			dtbCPOs.Columns[MTR_CPOTable.CPOID_FLD].AutoIncrement = true;
			dtbCPOs.Columns[MTR_CPOTable.CPOID_FLD].AutoIncrementSeed = 1;
			dtbCPOs.Columns[MTR_CPOTable.CPOID_FLD].AutoIncrementStep = 1;
			dtbCPOs.Columns.Add(new DataColumn(MTR_CPOTable.QUANTITY_FLD, typeof (decimal)));
			dtbCPOs.Columns[MTR_CPOTable.QUANTITY_FLD].AllowDBNull = true;
			//dtbCPOs.Columns[MTR_CPOTable.QUANTITY_FLD].MaxLength = int.MaxValue;
			dtbCPOs.Columns.Add(new DataColumn(MTR_CPOTable.STARTDATE_FLD, typeof (DateTime)));
			dtbCPOs.Columns[MTR_CPOTable.STARTDATE_FLD].AllowDBNull = true;
			dtbCPOs.Columns.Add(new DataColumn(MTR_CPOTable.DUEDATE_FLD, typeof (DateTime)));
			dtbCPOs.Columns[MTR_CPOTable.DUEDATE_FLD].AllowDBNull = true;
			dtbCPOs.Columns.Add(new DataColumn(MTR_CPOTable.REFMASTERID_FLD, typeof (int)));
			dtbCPOs.Columns[MTR_CPOTable.REFMASTERID_FLD].AllowDBNull = true;
			dtbCPOs.Columns.Add(new DataColumn(MTR_CPOTable.REFDETAILID_FLD, typeof (int)));
			dtbCPOs.Columns[MTR_CPOTable.REFDETAILID_FLD].AllowDBNull = true;
			dtbCPOs.Columns.Add(new DataColumn(MTR_CPOTable.REFTYPE_FLD, typeof (int)));
			dtbCPOs.Columns[MTR_CPOTable.REFTYPE_FLD].AllowDBNull = true;
			dtbCPOs.Columns.Add(new DataColumn(MTR_CPOTable.NETAVAILABLEQUANTITY_FLD, typeof (decimal)));
			dtbCPOs.Columns[MTR_CPOTable.NETAVAILABLEQUANTITY_FLD].AllowDBNull = true;
			//dtbCPOs.Columns[MTR_CPOTable.NETAVAILABLEQUANTITY_FLD].MaxLength = int.MaxValue;
			dtbCPOs.Columns[MTR_CPOTable.NETAVAILABLEQUANTITY_FLD].DefaultValue = decimal.Zero;
			dtbCPOs.Columns.Add(new DataColumn(MTR_CPOTable.CCNID_FLD, typeof (int)));
			dtbCPOs.Columns[MTR_CPOTable.CCNID_FLD].AllowDBNull = false;
			dtbCPOs.Columns[MTR_CPOTable.CCNID_FLD].DefaultValue = pintCCNID;
			dtbCPOs.Columns.Add(new DataColumn(MTR_CPOTable.PRODUCTID_FLD, typeof (int)));
			dtbCPOs.Columns[MTR_CPOTable.PRODUCTID_FLD].AllowDBNull = false;
			dtbCPOs.Columns.Add(new DataColumn(MTR_CPOTable.MASTERLOCATIONID_FLD, typeof (int)));
			dtbCPOs.Columns[MTR_CPOTable.MASTERLOCATIONID_FLD].AllowDBNull = false;
			dtbCPOs.Columns.Add(new DataColumn(MTR_CPOTable.STOCKUMID_FLD, typeof (int)));
			dtbCPOs.Columns[MTR_CPOTable.STOCKUMID_FLD].AllowDBNull = true;
			dtbCPOs.Columns.Add(new DataColumn(MTR_CPOTable.ISMPS_FLD, typeof (bool)));
			dtbCPOs.Columns[MTR_CPOTable.ISMPS_FLD].AllowDBNull = false;
			dtbCPOs.Columns[MTR_CPOTable.ISMPS_FLD].DefaultValue = true;
			dtbCPOs.Columns.Add(new DataColumn(MTR_CPOTable.CONVERTED_FLD, typeof (bool)));
			dtbCPOs.Columns[MTR_CPOTable.CONVERTED_FLD].AllowDBNull = false;
			dtbCPOs.Columns[MTR_CPOTable.CONVERTED_FLD].DefaultValue = false;
			dtbCPOs.Columns.Add(new DataColumn(MTR_CPOTable.POGENERATEDID_FLD, typeof (int)));
			dtbCPOs.Columns[MTR_CPOTable.POGENERATEDID_FLD].AllowDBNull = true;
			dtbCPOs.Columns.Add(new DataColumn(MTR_CPOTable.WOGENERATEDID_FLD, typeof (int)));
			dtbCPOs.Columns[MTR_CPOTable.WOGENERATEDID_FLD].AllowDBNull = true;
			dtbCPOs.Columns.Add(new DataColumn(MTR_CPOTable.MRPCYCLEOPTIONMASTERID_FLD, typeof (int)));
			dtbCPOs.Columns[MTR_CPOTable.MRPCYCLEOPTIONMASTERID_FLD].AllowDBNull = true;
			dtbCPOs.Columns.Add(new DataColumn(MTR_CPOTable.MPSCYCLEOPTIONMASTERID_FLD, typeof (int)));
			dtbCPOs.Columns[MTR_CPOTable.MPSCYCLEOPTIONMASTERID_FLD].AllowDBNull = true;
			dtbCPOs.Columns[MTR_CPOTable.MPSCYCLEOPTIONMASTERID_FLD].DefaultValue = pintCylceID;
			dtbCPOs.Columns.Add(new DataColumn(MTR_CPOTable.PARENTCPOID_FLD, typeof (int)));
			dtbCPOs.Columns[MTR_CPOTable.PARENTCPOID_FLD].AllowDBNull = true;
			dtbCPOs.Columns.Add(new DataColumn(MTR_CPOTable.DEMANDQUANTITY_FLD, typeof (decimal)));
			dtbCPOs.Columns[MTR_CPOTable.DEMANDQUANTITY_FLD].AllowDBNull = true;
			//dtbCPOs.Columns[MTR_CPOTable.DEMANDQUANTITY_FLD].MaxLength = int.MaxValue;
			dtbCPOs.Columns[MTR_CPOTable.DEMANDQUANTITY_FLD].DefaultValue = decimal.Zero;
			dtbCPOs.Columns.Add(new DataColumn(MTR_CPOTable.SUPPLYQUANTITY_FLD, typeof (decimal)));
			dtbCPOs.Columns[MTR_CPOTable.SUPPLYQUANTITY_FLD].AllowDBNull = true;
			//dtbCPOs.Columns[MTR_CPOTable.SUPPLYQUANTITY_FLD].MaxLength = int.MaxValue;
			dtbCPOs.Columns[MTR_CPOTable.SUPPLYQUANTITY_FLD].DefaultValue = decimal.Zero;

			#endregion

			// get all valid day of all configured year in system
			DataTable dtbDayOfWeek = dsUtil.GetWorkingDay();
			// get all holidays in system.
			DataTable dtbHolidays = dsUtil.GetHolidays();

			ArrayList arrDateTimes = new ArrayList();
			// start planning from top level items for each master location
			foreach (DataRow drowItem in dtbMPSCycleOptionDetail.Rows)
			{
				int intMasterLocationID = int.Parse(drowItem[MTR_MPSCycleOptionDetailTable.MASTERLOCATIONID_FLD].ToString());
				bool blnOnHand = Convert.ToBoolean(drowItem[MTR_MPSCycleOptionDetailTable.ONHAND_FLD].ToString());
				bool blnDemandWO = Convert.ToBoolean(drowItem[MTR_MPSCycleOptionDetailTable.DEMANDWO_FLD].ToString());
				bool blnSupplyWO = Convert.ToBoolean(drowItem[MTR_MPSCycleOptionDetailTable.SUPPLYWO_FLD].ToString());
				bool blnPO = Convert.ToBoolean(drowItem[MTR_MPSCycleOptionDetailTable.PURCHASEORDER_FLD].ToString());
				bool blnSO = Convert.ToBoolean(drowItem[MTR_MPSCycleOptionDetailTable.SALEORDER_FLD].ToString());
				DataTable dtbSaleOrders = null;
				DataTable dtbDemandWorkOrders = null;
				DataTable dtbSupplyWorkOrders = null;
				DataTable dtbPurchaseOrders = null;
				DataTable dtbInventory = null;

				// if user did not select any option for this master location
				// go to next master location in list
				if (!blnOnHand && !blnDemandWO && !blnSupplyWO && !blnPO && !blnSO)
					continue;

				#region Prepare data

				// if Onhand is checked we calculate NetAvailableQuantity with Iventory Onhand
				if (blnOnHand)
					dtbInventory = dsMasLocCache.GetAvailableQuantityForPlan(pintCCNID, intMasterLocationID);
				// retrieve all demand from sale order(s) which order current product
				// and have the schedule date in range of AsOfDate and DueDate
				if (blnSO)
					dtbSaleOrders = dsMPSRegenerationProcess.RetrieveSaleOrders(pintCCNID, intMasterLocationID, voMPSCycleOption.AsOfDate, dtmDueDate);
				// retrieve all demand from work order(s) of current product's parent(s)
				// which have the start date in range of AsOfDate and DueDate
				if (blnDemandWO)
					dtbDemandWorkOrders = dsMPSRegenerationProcess.RetrieveParents(pintCCNID, intMasterLocationID, voMPSCycleOption.AsOfDate, dtmDueDate);
				// retrieve all supply from work order(s) in process of current product
				// which have the due date in range of AsOfDate and DueDate
				if (blnSupplyWO)
					dtbSupplyWorkOrders = dsMPSRegenerationProcess.RetrieveSupplyFromWO(pintCCNID, intMasterLocationID, voMPSCycleOption.AsOfDate, dtmDueDate);
				// retrieve all supply from purchase order(s) which order current product
				// and have the delivery date in range of AsOfDate and DueDate
				if (blnPO)
					dtbPurchaseOrders = dsMPSRegenerationProcess.RetriveSupplyFromPO(pintCCNID, intMasterLocationID, voMPSCycleOption.AsOfDate, dtmDueDate);

				#endregion

				// get the demand date from sale order
				DateTime dtmPlanDate = DateTime.MinValue;
				if (blnSO)
				{
					foreach (DataRow drowSO in dtbSaleOrders.Rows)
					{
						dtmPlanDate = DateTime.Parse(drowSO[SO_DeliveryScheduleTable.SCHEDULEDATE_FLD].ToString().Trim());
						if (!arrDateTimes.Contains(dtmPlanDate))
						{
							// update to DataRow
							drowSO[SO_DeliveryScheduleTable.SCHEDULEDATE_FLD] = dtmPlanDate;
							arrDateTimes.Add(dtmPlanDate);
						}
					}
				}
				// get the demand date from work order
				if (blnDemandWO)
				{
					foreach (DataRow drowWO in dtbDemandWorkOrders.Rows)
					{
						dtmPlanDate = DateTime.Parse(drowWO[PRO_WorkOrderDetailTable.STARTDATE_FLD].ToString().Trim());
						if (!arrDateTimes.Contains(dtmPlanDate))
						{
							// update to DataRow
							drowWO[PRO_WorkOrderDetailTable.STARTDATE_FLD] = dtmPlanDate;
							arrDateTimes.Add(dtmPlanDate);
						}
					}
				}
				arrDateTimes.Sort();
				arrDateTimes.TrimToSize();

				// remove milliseconds from Schedule date and due date of PO/WO
				if (blnPO)
				{
					foreach (DataRow drowPO in dtbPurchaseOrders.Rows)
						drowPO[PO_DeliveryScheduleTable.SCHEDULEDATE_FLD] = DateTime.Parse(drowPO[PO_DeliveryScheduleTable.SCHEDULEDATE_FLD].ToString().Trim());
				}
				if (blnSupplyWO)
				{
					foreach (DataRow drowWO in dtbSupplyWorkOrders.Rows)
						drowWO[PRO_WorkOrderDetailTable.DUEDATE_FLD] = DateTime.Parse(drowWO[PRO_WorkOrderDetailTable.DUEDATE_FLD].ToString().Trim());
				}

				// select all top level item
				DataRow[] drowProducts = dtbProducts.Select("ParentID IS NULL");
				// clear all result of previous master location
				dtbCPOs.Clear();
				// reset CPO ID column
				dtbCPOs.Columns[MTR_CPOTable.CPOID_FLD].AllowDBNull = false;
				dtbCPOs.Columns[MTR_CPOTable.CPOID_FLD].AutoIncrement = true;
				dtbCPOs.Columns[MTR_CPOTable.CPOID_FLD].AutoIncrementSeed = 1;
				dtbCPOs.Columns[MTR_CPOTable.CPOID_FLD].AutoIncrementStep = 1;
				// assign default value for master location id field
				dtbCPOs.Columns[MTR_CPOTable.MASTERLOCATIONID_FLD].DefaultValue = intMasterLocationID;
				// start regeneration process
				DataTable dtbResult = GeneratePlanOffline(dtbProducts, drowProducts, intMasterLocationID, arrDateTimes, voMPSCycleOption, 0, dtbInventory, dtbSaleOrders, dtbDemandWorkOrders, dtbSupplyWorkOrders, dtbPurchaseOrders, dtbCPOs, dtbDayOfWeek, dtbHolidays);
				// after generated all CPOs we need save to database 
				// and update reference of parent and child CPO
				// we need to turn of auto increment property of CPO ID in order to update child CPO
				dtbResult.Columns[MTR_CPOTable.CPOID_FLD].AutoIncrement = false;
				foreach (DataRow drowCPO in dtbResult.Rows)
				{
					// store old id
					int intOldID = int.Parse(drowCPO[MTR_CPOTable.CPOID_FLD].ToString());
					MTR_CPOVO voCPO = new MTR_CPOVO();
					// retrieve data from data row
					voCPO.CCNID = pintCCNID;
					voCPO.Converted = false;
					voCPO.DemandQuantity = decimal.Parse(drowCPO[MTR_CPOTable.DEMANDQUANTITY_FLD].ToString().Trim());
					voCPO.DueDate = (DateTime) drowCPO[MTR_CPOTable.DUEDATE_FLD];
					voCPO.IsMPS = true;
					voCPO.MasterLocationID = intMasterLocationID;
					voCPO.MPSCycleOptionMasterID = pintCylceID;
					voCPO.NetAvailableQuantity = decimal.Parse(drowCPO[MTR_CPOTable.NETAVAILABLEQUANTITY_FLD].ToString().Trim());
					if (drowCPO[MTR_CPOTable.PARENTCPOID_FLD] != null && drowCPO[MTR_CPOTable.PARENTCPOID_FLD] != DBNull.Value
						&& !drowCPO[MTR_CPOTable.PARENTCPOID_FLD].Equals(0))
						voCPO.ParentCPOID = int.Parse(drowCPO[MTR_CPOTable.PARENTCPOID_FLD].ToString().Trim());
					voCPO.ProductID = int.Parse(drowCPO[MTR_CPOTable.PRODUCTID_FLD].ToString().Trim());
					voCPO.StockUMID = int.Parse(drowCPO[MTR_CPOTable.STOCKUMID_FLD].ToString().Trim());
					// total supply of the day
					voCPO.SupplyQuantity = decimal.Parse(drowCPO[MTR_CPOTable.SUPPLYQUANTITY_FLD].ToString().Trim());
					// quantity need to be produce
					voCPO.Quantity = decimal.Parse(drowCPO[MTR_CPOTable.QUANTITY_FLD].ToString().Trim());
					voCPO.StartDate = (DateTime) drowCPO[MTR_CPOTable.STARTDATE_FLD];
					// save to database first and get new ID
					voCPO.CPOID = AddAndReturnID(voCPO);
					// select all child CPO of this CPO
					DataRow[] drowChildCPOs = dtbResult.Select(MTR_CPOTable.PARENTCPOID_FLD + "='" + intOldID + "'");
					if (drowChildCPOs != null && drowChildCPOs.Length > 0)
					{
						foreach (DataRow drowChild in drowChildCPOs)
						{
							// ignore all modified row
							if (drowChild.RowState == DataRowState.Modified)
								continue;
							// update parent CPOID
							drowChild[MTR_CPOTable.PARENTCPOID_FLD] = voCPO.CPOID;
						}
					}
				}
			}
			// update MPS Gen Date of Cycle Option Master
			voMPSCycleOption.MPSGenDate = DateTime.Now;
			dsMPSCycleOptionMaster.Update(voMPSCycleOption);
			return dstResult;
		}

		/// <summary>
		/// Return the DataSet (list of record) by inputing the FieldList and Condition
		/// </summary>
		public void UpdateDataSet(DataSet dstData)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Update MPS Cycle Option Master object
		/// </summary>
	
		public void Update(object pObjectDetail)
		{
			MTR_MPSCycleOptionMasterDS dsMPSCycleOptionMaster = new MTR_MPSCycleOptionMasterDS();
			dsMPSCycleOptionMaster.Update(pObjectDetail);
		}

		/// <summary>
		/// Scanning through the list of demand date and generated CPO for item if any.
		/// </summary>
		/// <param name="pdtbItems">List of Item to be planned</param>
		/// <param name="pintMasterLocationID">Master Location</param>
		/// <param name="parrDateTimes">List of all date which have demand</param>
		/// <param name="pobjMPSCycleOptionMaster">MPSCycleOptionMasterVO</param>
		/// <param name="pintParentID">Parent Product ID</param>
		/// <param name="pdtbInventory">Quantity from Inventory</param>
		/// <param name="pdtbSaleOrders">Demand from Sale Orders</param>
		/// <param name="pdtbDemandWOs">Demand from Work Orders</param>
		/// <param name="pdtbSupplyWOs">Supply from Work Orders</param>
		/// <param name="pdtbPOs">Supply from Purchase Orders</param>
		/// <param name="parrCPOs">List of generated Parent CPOs</param>
		private void GeneratePlan(DataTable pdtbItems,
		                          int pintMasterLocationID, ArrayList parrDateTimes,
		                          object pobjMPSCycleOptionMaster, int pintParentID, DataTable pdtbInventory,
		                          DataTable pdtbSaleOrders, DataTable pdtbDemandWOs, DataTable pdtbSupplyWOs, DataTable pdtbPOs,
		                          ArrayList parrCPOs)
		{
			try
			{
				MTR_MPSCycleOptionMasterVO voMPSCycleOptionMaster = (MTR_MPSCycleOptionMasterVO) pobjMPSCycleOptionMaster;
				// generate plan for each item in table
				foreach (DataRow drowItem in pdtbItems.Rows)
				{
					int intProductID = 0;
					decimal decNetAvailableQuantity = 0;
					decimal decYieldRate = 0;
					// remain quantity of previous day will be considered as available quantity of next day
					decimal decRemainQuantity = 0;
					decimal decInventoryQuantity = 0;
					// store all generated CPO in order to use later
					ArrayList arrCPOs = new ArrayList();
					// retrieve product information from product id
					intProductID = int.Parse(drowItem[ITM_ProductTable.PRODUCTID_FLD].ToString());
					ITM_ProductVO voProduct = (ITM_ProductVO) dsProduct.GetProductInfo(intProductID);
					// get all components of current product
					DataTable dtbComponents = dsRegenerationProcess.GetBOMComponents(intProductID);

					// if current product is MPS, start calculating plan data
					if (voProduct.PlanType.Equals((int) PlanTypeEnum.MPS))
					{
						#region refine lead time and other product information

						if (voProduct.ScrapPercent < 0)
							voProduct.ScrapPercent = decimal.Zero;
						if (voProduct.SafetyStock < 0)
							voProduct.SafetyStock = decimal.Zero;
						if (voProduct.LTFixedTime < 0)
							voProduct.LTFixedTime = decimal.Zero;
						if (voProduct.LTVariableTime < 0)
							voProduct.LTVariableTime = decimal.Zero;
						if (voProduct.LTDocToStock < 0)
							voProduct.LTDocToStock = decimal.Zero;
						if (voProduct.LTOrderPrepare < 0)
							voProduct.LTOrderPrepare = decimal.Zero;
						if (voProduct.LTShippingPrepare < 0)
							voProduct.LTShippingPrepare = decimal.Zero;

						#endregion

						DateTime dtmPrevDate = voMPSCycleOptionMaster.AsOfDate;
						//DateTime dtmStartDate;

						#region Supply quantity from inventory

						// supply quantity from inventory
						DataRow[] drowInventorys = null;
						if (pdtbInventory != null)
							drowInventorys = pdtbInventory.Select(ITM_ProductTable.PRODUCTID_FLD + "=" + intProductID);
						if (drowInventorys != null)
							foreach (DataRow drowInven in drowInventorys)
								decInventoryQuantity += decimal.Parse(drowInven[Constants.SUPPLY_QUANTITY_FLD].ToString());

						#endregion

						// calculate YieldRate of Product
						decYieldRate = decimal.Round((100 - voProduct.ScrapPercent)/100, 5);
						// net begin quantity = quantity from inventory - safety stock quantity
						decRemainQuantity = decNetAvailableQuantity - voProduct.SafetyStock + decInventoryQuantity;

						#region planning for each demand date

						// scan the Date Time array
						ArrayList arrDemandParentCPO = new ArrayList();
						foreach (DateTime dtmDueDate in parrDateTimes)
						{
							DataRow[] drowSaleOrders = null;
							DataRow[] drowDemandWorkOrders = null;
							DataRow[] drowPurchaseOrders = null;
							DataRow[] drowSupplyWorkOrders = null;
							decimal decSaleOrder = 0;
							decimal decDemandWOs = 0;
							decimal decSupplyWOs = 0;
							decimal decSupplyPOs = 0;
							decimal decDemandFromParentCPO = 0;

							#region Calculate Demands

							string strExpression = string.Empty;

							if (pdtbSaleOrders != null)
							{
								strExpression = SO_DeliveryScheduleTable.SCHEDULEDATE_FLD
									+ " >= '" + dtmPrevDate.ToString("G", DateTimeFormatInfo.CurrentInfo) + "' AND "
									+ SO_DeliveryScheduleTable.SCHEDULEDATE_FLD + " <= '" + dtmDueDate.ToString("G", DateTimeFormatInfo.CurrentInfo) + "'"
									+ " AND " + SO_SaleOrderDetailTable.PRODUCTID_FLD + "=" + intProductID;
								drowSaleOrders = pdtbSaleOrders.Select(strExpression);
							}
							// retrieve demand from work order of current day
							if (pdtbDemandWOs != null)
							{
								strExpression = PRO_WorkOrderDetailTable.STARTDATE_FLD
									+ " >= '" + dtmPrevDate.ToString("G", DateTimeFormatInfo.CurrentInfo) + "' AND "
									+ PRO_WorkOrderDetailTable.STARTDATE_FLD + " <= '" + dtmDueDate.ToString("G", DateTimeFormatInfo.CurrentInfo) + "'"
									+ " AND " + PRO_WorkOrderBomDetailTable.COMPONENTID_FLD + "=" + intProductID;
								drowDemandWorkOrders = pdtbDemandWOs.Select(strExpression);
							}
							// total demand = demand from SO + demand from WO + demand from CPO + safety stock
							if (drowSaleOrders != null && drowSaleOrders.Length > 0)
							{
								foreach (DataRow drowSO in drowSaleOrders)
								{
									decSaleOrder += decimal.Parse(drowSO[Constants.DEMAND_QUANTITY_FLD].ToString());
									drowSO[Constants.DEMAND_QUANTITY_FLD] = decimal.Zero;
								}
								pdtbSaleOrders.AcceptChanges();
							}
							if (drowDemandWorkOrders != null && drowDemandWorkOrders.Length > 0)
							{
								foreach (DataRow drowWOs in drowDemandWorkOrders)
								{
									decDemandWOs += decimal.Parse(drowWOs[Constants.DEMAND_QUANTITY_FLD].ToString());
									// update demand quantity
									drowWOs[Constants.DEMAND_QUANTITY_FLD] = decimal.Zero;
								}
								pdtbDemandWOs.AcceptChanges();
							}

							#region Demand from parent CPO

							// find the parent CPO which have start date in range of plan date
							if (parrCPOs.Count > 0)
							{
								for (int j = 0; j < parrCPOs.Count; j++)
								{
									MTR_CPOVO voDemandParent = (MTR_CPOVO) parrCPOs[j];
									if (voDemandParent.StartDate >= dtmPrevDate && voDemandParent.StartDate <= dtmDueDate)
									{
										bool blnIsUsed = false;
										foreach (MTR_CPOVO voAddedDemandCPO in arrDemandParentCPO)
										{
											if (voAddedDemandCPO.CPOID == voDemandParent.CPOID)
											{
												// this parent CPO already supplied
												blnIsUsed = true;
												voAddedDemandCPO.NetAvailableQuantity = decimal.Zero;
												break;
											}
										}
										if (!blnIsUsed)
										{
											// demand from parent's CPO
											decimal decRequiredQuantity = new ITM_BOMDS().GetRequiredQuantity(pintParentID, intProductID);
											if (decRequiredQuantity > 0)
												voDemandParent.NetAvailableQuantity = voDemandParent.Quantity*decRequiredQuantity;
											else
												voDemandParent.NetAvailableQuantity = voDemandParent.Quantity;
											decDemandFromParentCPO += voDemandParent.NetAvailableQuantity;
											arrDemandParentCPO.Add(voDemandParent);
										}
									}
								}
							}
							arrDemandParentCPO.TrimToSize();

							#endregion

							#endregion

							#region Calculate Supplies

							// retrieve supply from work order of current day
							if (pdtbSupplyWOs != null)
							{
								strExpression = PRO_WorkOrderDetailTable.DUEDATE_FLD
									+ " >= '" + dtmPrevDate.ToString("G", DateTimeFormatInfo.CurrentInfo) + "' AND "
									+ PRO_WorkOrderDetailTable.DUEDATE_FLD + " <= '" + dtmDueDate.ToString("G", DateTimeFormatInfo.CurrentInfo) + "'"
									+ " AND " + PRO_WorkOrderDetailTable.PRODUCTID_FLD + "=" + intProductID;
								drowSupplyWorkOrders = pdtbSupplyWOs.Select(strExpression);
							}
							// retrieve supply from purchase order of current day
							if (pdtbPOs != null)
							{
								strExpression = PO_DeliveryScheduleTable.SCHEDULEDATE_FLD
									+ " >= '" + dtmPrevDate.ToString("G", DateTimeFormatInfo.CurrentInfo) + "' AND "
									+ PO_DeliveryScheduleTable.SCHEDULEDATE_FLD + " <= '" + dtmDueDate.ToString("G", DateTimeFormatInfo.CurrentInfo) + "'"
									+ " AND " + PO_PurchaseOrderDetailTable.PRODUCTID_FLD + "=" + intProductID;
								drowPurchaseOrders = pdtbPOs.Select(strExpression);
							}
							// total supply of day = Supply from WO + Supply from PO
							if (drowSupplyWorkOrders != null && drowSupplyWorkOrders.Length > 0)
							{
								foreach (DataRow drowWOs in drowSupplyWorkOrders)
								{
									decSupplyWOs += decimal.Parse(drowWOs[Constants.SUPPLY_QUANTITY_FLD].ToString());
									// update supply quantity from data source
									drowWOs[Constants.SUPPLY_QUANTITY_FLD] = decimal.Zero;
								}
								pdtbSupplyWOs.AcceptChanges();
							}
							if (drowPurchaseOrders != null && drowPurchaseOrders.Length > 0)
							{
								foreach (DataRow drowPOs in drowPurchaseOrders)
								{
									decSupplyPOs += decimal.Parse(drowPOs[Constants.SUPPLY_QUANTITY_FLD].ToString());
									// update supply quantity from data source
									drowPOs[Constants.SUPPLY_QUANTITY_FLD] = decimal.Zero;
								}
								pdtbPOs.AcceptChanges();
							}

							#endregion

							// if today has no order then go to next day
							if ((decSaleOrder + decDemandWOs) == decimal.Zero)
							{
								// check demand from Parent CPO
								if (decDemandFromParentCPO <= decimal.Zero)
								{
									// plus today supply quantity to remain quantity of next day.
									decRemainQuantity += (decNetAvailableQuantity + decSupplyPOs + decSupplyWOs);
									continue;
								}
							}

							#region MPS Calculation

							// re-calculate NetAvailableQuantity of day = supply - demand
							decNetAvailableQuantity = (decSupplyPOs + decSupplyWOs + decRemainQuantity) - ((decSaleOrder + decDemandWOs));
							// if we can supply for today's demand, go to next day
							if (decNetAvailableQuantity >= 0)
							{
								if ((decNetAvailableQuantity - decDemandFromParentCPO) < 0)
								{
									#region Generate CPO for demand from parent CPO

									for (int k = 0; k < arrDemandParentCPO.Count; k++)
									{
										MTR_CPOVO voDemandParent = (MTR_CPOVO) arrDemandParentCPO[k];
										// if we can supply to this parent CPO, continue to next CPO
										if ((decNetAvailableQuantity - voDemandParent.NetAvailableQuantity) > 0)
										{
											voDemandParent.NetAvailableQuantity = 0;
											continue;
										}
										if (voDemandParent.NetAvailableQuantity != 0)
										{
											MTR_CPOVO voCPO = new MTR_CPOVO();
											voCPO.IsMPS = true;
											voCPO.ProductID = voProduct.ProductID;
											voCPO.MasterLocationID = pintMasterLocationID;
											voCPO.MPSCycleOptionMasterID = voMPSCycleOptionMaster.MPSCycleOptionMasterID;
											voCPO.StockUMID = voProduct.StockUMID;
											voCPO.CCNID = voMPSCycleOptionMaster.CCNID;
											voCPO.ParentCPOID = voDemandParent.CPOID;
											// Due date of child must equal to start date of parent
											voCPO.DueDate = voDemandParent.StartDate;
											// total demand of the day
											voCPO.DemandQuantity = decimal.Round(voDemandParent.NetAvailableQuantity + decSaleOrder + decDemandWOs, 5);
											// total supply of the day
											voCPO.SupplyQuantity = decimal.Round(decSupplyPOs + decSupplyWOs + decRemainQuantity, 5);
											// quantity need to be produce
											voCPO.Quantity = decimal.Round(Math.Abs(voDemandParent.NetAvailableQuantity - decNetAvailableQuantity)/decYieldRate, 5);
											// convert lead time from seconds to days
											// one day = 86400 seconds.
											decimal decDayToAdd = (voProduct.LTFixedTime + (voProduct.LTVariableTime*voCPO.Quantity) + voProduct.LTDocToStock
												+ voProduct.LTOrderPrepare + voProduct.LTShippingPrepare)/86400;
											//Debug.WriteLine(voProduct.Code + ":" + (decDayToAdd * 86400).ToString());
											// convert start date to valid working day
											voCPO.StartDate = boUtils.ConvertWorkingDay(voCPO.DueDate, decDayToAdd, ScheduleMethodEnum.Backward);
											// save to database
											voCPO.CPOID = dsCPO.AddAndReturnID(voCPO);
											// put CPO to array list in order to use as Parent CPO
											arrCPOs.Add(voCPO);
											// cause we just generated CPO to supply 
											// the demand from parent CPO, we need to update
											// required quantity from parent CPO
											voDemandParent.NetAvailableQuantity = 0;
										}
									}
									// reset remain quantity for next day
									decRemainQuantity = 0;
									// reset available quantity
									decNetAvailableQuantity = 0;

									#endregion
								}
							}
							else
							{
								#region Generate CPO for demand from SOs, WOs

								// if NetAvailableQuantity < 0, we generate new CPO
								MTR_CPOVO voCPO = new MTR_CPOVO();
								voCPO.IsMPS = true;
								voCPO.ProductID = voProduct.ProductID;
								voCPO.MasterLocationID = pintMasterLocationID;
								voCPO.MPSCycleOptionMasterID = voMPSCycleOptionMaster.MPSCycleOptionMasterID;
								voCPO.StockUMID = voProduct.StockUMID;
								voCPO.CCNID = voMPSCycleOptionMaster.CCNID;
								voCPO.DueDate = dtmDueDate;
								// total demand of the day
								voCPO.DemandQuantity = decimal.Round(decSaleOrder + decDemandWOs, 5);
								// total supply of the day
								voCPO.SupplyQuantity = decimal.Round(decSupplyPOs + decSupplyWOs + decRemainQuantity, 5);
								// quantity need to be produce
								voCPO.Quantity = decimal.Round(Math.Abs(decNetAvailableQuantity/decYieldRate), 5);
								// convert lead time from seconds to days
								// one day = 86400 seconds.
								decimal decDayToAdd = (voProduct.LTFixedTime + (voProduct.LTVariableTime*voCPO.Quantity) + voProduct.LTDocToStock
									+ voProduct.LTOrderPrepare + voProduct.LTShippingPrepare)/86400;
								// convert start date to valid working day
								voCPO.StartDate = boUtils.ConvertWorkingDay(voCPO.DueDate, decDayToAdd, ScheduleMethodEnum.Backward);
								// save to database
								voCPO.CPOID = dsCPO.AddAndReturnID(voCPO);
								// put CPO to array list in order to use as Parent CPO
								arrCPOs.Add(voCPO);

								#endregion

								#region Generate CPO for demand from parent CPO

								if (decDemandFromParentCPO > decimal.Zero)
								{
									for (int k = 0; k < arrDemandParentCPO.Count; k++)
									{
										MTR_CPOVO voDemandParent = (MTR_CPOVO) arrDemandParentCPO[k];
										// if we can supply to this parent CPO, continue to next CPO
										if ((decNetAvailableQuantity - voDemandParent.NetAvailableQuantity) > 0)
										{
											voDemandParent.NetAvailableQuantity = 0;
											continue;
										}
										if (voDemandParent.NetAvailableQuantity != 0)
										{
											voCPO = new MTR_CPOVO();
											voCPO.IsMPS = true;
											voCPO.ProductID = voProduct.ProductID;
											voCPO.MasterLocationID = pintMasterLocationID;
											voCPO.MPSCycleOptionMasterID = voMPSCycleOptionMaster.MPSCycleOptionMasterID;
											voCPO.StockUMID = voProduct.StockUMID;
											voCPO.CCNID = voMPSCycleOptionMaster.CCNID;
											voCPO.ParentCPOID = voDemandParent.CPOID;
											// Due date of child must equal to start date of parent
											voCPO.DueDate = voDemandParent.StartDate;
											// total demand of the day
											voCPO.DemandQuantity = decimal.Round(voDemandParent.NetAvailableQuantity, 5);
											// supply for parent CPO is zero because we supply all for SO and WO already.
											voCPO.SupplyQuantity = decimal.Zero; //decimal.Round(decSupplyPOs + decSupplyWOs + decRemainQuantity, 5);
											// quantity need to be produce
											voCPO.Quantity = decimal.Round(Math.Abs(voDemandParent.NetAvailableQuantity)/decYieldRate, 5);
											// convert lead time from seconds to days
											// one day = 86400 seconds.
											decDayToAdd = (voProduct.LTFixedTime + (voProduct.LTVariableTime*voCPO.Quantity) + voProduct.LTDocToStock
												+ voProduct.LTOrderPrepare + voProduct.LTShippingPrepare)/86400;
											// convert start date to valid working day
											voCPO.StartDate = boUtils.ConvertWorkingDay(voCPO.DueDate, decDayToAdd, ScheduleMethodEnum.Backward);
											// save to database
											voCPO.CPOID = dsCPO.AddAndReturnID(voCPO);
											// put CPO to array list in order to use as Parent CPO
											arrCPOs.Add(voCPO);
											// because we just generated CPO to supply 
											// the demand from parent CPO, we need to update
											// required quantity from parent CPO
											voDemandParent.NetAvailableQuantity = 0;
										}
									}
								}

								#endregion

								// reset remain quantity for next day
								decRemainQuantity = 0;
								// reset available quantity
								decNetAvailableQuantity = 0;
							}

							#region update supply quantity

//							// supply from inventory has higher priority
//							if (drowInventorys != null)
//							{
//								foreach (DataRow drowNew in drowInventorys)
//								{
//									drowNew[Constants.SUPPLY_QUANTITY_FLD] = decimal.Zero;
//								}
//							}
//							// next is supply from work order
//							// update supply quantity for work order
//							if (drowSupplyWorkOrders != null)
//							{
//								foreach (DataRow drowNew in drowSupplyWorkOrders)
//								{
//									drowNew[Constants.SUPPLY_QUANTITY_FLD] = decimal.Zero;
//								}
//							}
//							if (drowPurchaseOrders != null)
//							{
//								foreach (DataRow drowNew in drowPurchaseOrders)
//								{
//									drowNew[Constants.SUPPLY_QUANTITY_FLD] = decimal.Zero;
//								}
//							}
//							// Accept changes
//							if (pdtbInventory != null)
//								pdtbInventory.AcceptChanges();
//							if (pdtbSupplyWOs != null)
//								pdtbSupplyWOs.AcceptChanges();
//							if (pdtbPOs != null)
//								pdtbPOs.AcceptChanges();

							#endregion

							#endregion
						} // foreach (DateTime dtmDueDate in parrDateTimes)

						#endregion

						// trim to actual size
						arrCPOs.TrimToSize();

					} // if (voProduct.PlanType.Equals((int)PlanTypeEnum.MPS))
					// if current product has component(s)
					if (dtbComponents.Rows.Count > 0)
					{
						// we will generate plan for all component of current product
						GeneratePlan(dtbComponents, pintMasterLocationID, parrDateTimes, pobjMPSCycleOptionMaster, intProductID, pdtbInventory, pdtbSaleOrders, pdtbDemandWOs, pdtbSupplyWOs, pdtbPOs, arrCPOs);
					}
				} // foreach (DataRow drowItem in pdtbItems.Rows)
			} // try
			catch (PCSDBException ex)
			{
				throw ex;
			}
			catch (OverflowException ex)
			{
				throw ex;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message, ex);
			}
		}

		/// <summary>
		/// Check if user is setting calendar yet
		/// </summary>
		/// <param name="pobjCycleOption"></param>
		/// <returns>true if setting full calendar else return false</returns>
		/// <author>DungLA</author>
	
		public bool CheckCalendarConfig(object pobjCycleOption)
		{
			try
			{
				MST_WorkingDayMasterDS dsWorkingDay = new MST_WorkingDayMasterDS();
				DataSet dstCalendar = dsWorkingDay.GetCalendar();
				if (dstCalendar == null || dstCalendar.Tables.Count == 0)
					return false;
				MTR_MPSCycleOptionMasterVO voMPSCycle = (MTR_MPSCycleOptionMasterVO) pobjCycleOption;
				DateTime dtmEndCycle = voMPSCycle.AsOfDate.AddDays(voMPSCycle.PlanHorizon);
				for (int i = voMPSCycle.AsOfDate.Year; i <= dtmEndCycle.Year; i++)
				{
					DataRow[] drowsFilter = dstCalendar.Tables[0].Select(MST_WorkingDayMasterTable.YEAR_FLD + " = " + i);
					if (drowsFilter == null || drowsFilter.Length == 0)
					{
						return false;
					}
				}
				return true;
			}
			catch
			{
				return false;
			}
		}

		/// <summary>
		/// Scanning through the list of demand date and generated CPO for item if any.
		/// </summary>
		/// <param name="pdtbAllProducts">List of Item of selected CCN</param>
		/// <param name="pdrowProducts">All products to be planned</param>
		/// <param name="pintMasterLocationID">Master Location</param>
		/// <param name="parrDateTimes">List of all date which have demand</param>
		/// <param name="pobjMPSCycleOptionMaster">MPSCycleOptionMasterVO</param>
		/// <param name="pintParentID">Parent Product ID</param>
		/// <param name="pdtbInventory">Quantity from Inventory</param>
		/// <param name="pdtbSaleOrders">Demand from Sale Orders</param>
		/// <param name="pdtbDemandWOs">Demand from Work Orders</param>
		/// <param name="pdtbSupplyWOs">Supply from Work Orders</param>
		/// <param name="pdtbPOs">Supply from Purchase Orders</param>
		/// <param name="pdtbPOs">List of generated Parent CPOs</param>
		private DataTable GeneratePlanOffline(DataTable pdtbAllProducts, DataRow[] pdrowProducts,
		                                      int pintMasterLocationID, ArrayList parrDateTimes,
		                                      object pobjMPSCycleOptionMaster, int pintParentID, DataTable pdtbInventory,
		                                      DataTable pdtbSaleOrders, DataTable pdtbDemandWOs, DataTable pdtbSupplyWOs, DataTable pdtbPOs,
		                                      DataTable pdtbCPOs, DataTable pdtbDayOfWeek, DataTable pdtbHolidays)
		{
			MTR_MPSCycleOptionMasterVO voMPSCycleOptionMaster = (MTR_MPSCycleOptionMasterVO) pobjMPSCycleOptionMaster;
			// generate plan for each item in table
			foreach (DataRow drowItem in pdrowProducts)
			{
				decimal decNetAvailableQuantity = 0;
				decimal decYieldRate = 0;
				// remain quantity of previous day will be considered as available quantity of next day
				decimal decRemainQuantity = 0;
				decimal decInventoryQuantity = 0;

				#region product information

				ITM_ProductVO voProduct = new ITM_ProductVO();
				voProduct.ProductID = int.Parse(drowItem[ITM_ProductTable.PRODUCTID_FLD].ToString());
				voProduct.StockUMID = int.Parse(drowItem[ITM_ProductTable.STOCKUMID_FLD].ToString());
				try
				{
					voProduct.PlanType = int.Parse(drowItem[ITM_ProductTable.PLANTYPE_FLD].ToString());
				}
				catch
				{
				}
				try
				{
					voProduct.ScrapPercent = decimal.Parse(drowItem[ITM_ProductTable.SCRAPPERCENT_FLD].ToString());
					if (voProduct.ScrapPercent < 0)
						voProduct.ScrapPercent = decimal.Zero;
				}
				catch
				{
				}
				try
				{
					voProduct.SafetyStock = decimal.Parse(drowItem[ITM_ProductTable.SAFETYSTOCK_FLD].ToString());
					if (voProduct.ScrapPercent < 0)
						voProduct.ScrapPercent = decimal.Zero;
				}
				catch
				{
				}
				try
				{
					voProduct.LTFixedTime = decimal.Parse(drowItem[ITM_ProductTable.LTFIXEDTIME_FLD].ToString());
					if (voProduct.LTFixedTime < 0)
						voProduct.LTFixedTime = decimal.Zero;
				}
				catch
				{
				}
				try
				{
					voProduct.LTVariableTime = decimal.Parse(drowItem[ITM_ProductTable.LTVARIABLETIME_FLD].ToString());
					if (voProduct.LTVariableTime < 0)
						voProduct.LTVariableTime = decimal.Zero;
				}
				catch
				{
				}
				try
				{
					voProduct.LTDocToStock = decimal.Parse(drowItem[ITM_ProductTable.LTDOCKTOSTOCK_FLD].ToString());
					if (voProduct.LTDocToStock < 0)
						voProduct.LTDocToStock = decimal.Zero;
				}
				catch
				{
				}
				try
				{
					voProduct.LTOrderPrepare = decimal.Parse(drowItem[ITM_ProductTable.LTORDERPREPARE_FLD].ToString());
					if (voProduct.LTOrderPrepare < 0)
						voProduct.LTOrderPrepare = decimal.Zero;
				}
				catch
				{
				}
				try
				{
					voProduct.LTShippingPrepare = decimal.Parse(drowItem[ITM_ProductTable.LTSHIPPINGPREPARE_FLD].ToString());
					if (voProduct.LTShippingPrepare < 0)
						voProduct.LTShippingPrepare = decimal.Zero;
				}
				catch
				{
				}

				#endregion

				// get all components of current product
				DataRow[] drowComponents = pdtbAllProducts.Select("ParentID = '" + voProduct.ProductID + "'");

				// if current product is MPS, start calculating plan data
				if (voProduct.PlanType.Equals((int) PlanTypeEnum.MPS))
				{
					DateTime dtmPrevDate = voMPSCycleOptionMaster.AsOfDate;
					//DateTime dtmStartDate;

					#region Supply quantity from inventory

					// supply quantity from inventory
					DataRow[] drowInventorys = null;
					if (pdtbInventory != null)
						drowInventorys = pdtbInventory.Select(ITM_ProductTable.PRODUCTID_FLD + "='" + voProduct.ProductID + "'");
					if (drowInventorys != null)
						foreach (DataRow drowInven in drowInventorys)
							decInventoryQuantity += decimal.Parse(drowInven[Constants.SUPPLY_QUANTITY_FLD].ToString());

					#endregion

					// calculate YieldRate of Product
					decYieldRate = decimal.Round((100 - voProduct.ScrapPercent)/100, 5);
					// net begin quantity = quantity from inventory - safety stock quantity
					decRemainQuantity = decNetAvailableQuantity - voProduct.SafetyStock + decInventoryQuantity;

					#region planning for each demand date

					// scan the Date Time array
					foreach (DateTime dtmDueDate in parrDateTimes)
					{
						MTR_CPOVO voParentCPO = null;
						DataRow[] drowSaleOrders = null;
						DataRow[] drowDemandWorkOrders = null;
						DataRow[] drowPurchaseOrders = null;
						DataRow[] drowSupplyWorkOrders = null;
						decimal decSaleOrder = 0;
						decimal decDemandWOs = 0;
						decimal decSupplyWOs = 0;
						decimal decSupplyPOs = 0;
						decimal decDemandFromParentCPO = 0;

						#region Calculate Demands

						string strExpression = string.Empty;

						if (pdtbSaleOrders != null)
						{
							strExpression = SO_DeliveryScheduleTable.SCHEDULEDATE_FLD
								+ " >= '" + dtmPrevDate.ToString("G", DateTimeFormatInfo.CurrentInfo) + "' AND "
								+ SO_DeliveryScheduleTable.SCHEDULEDATE_FLD + " <= '" + dtmDueDate.ToString("G", DateTimeFormatInfo.CurrentInfo) + "'"
								+ " AND " + SO_SaleOrderDetailTable.PRODUCTID_FLD + "='" + voProduct.ProductID + "'";
							drowSaleOrders = pdtbSaleOrders.Select(strExpression);
						}
						// retrieve demand from work order of current day
						if (pdtbDemandWOs != null)
						{
							strExpression = PRO_WorkOrderDetailTable.STARTDATE_FLD
								+ " >= '" + dtmPrevDate.ToString("G", DateTimeFormatInfo.CurrentInfo) + "' AND "
								+ PRO_WorkOrderDetailTable.STARTDATE_FLD + " <= '" + dtmDueDate.ToString("G", DateTimeFormatInfo.CurrentInfo) + "'"
								+ " AND " + PRO_WorkOrderBomDetailTable.COMPONENTID_FLD + "='" + voProduct.ProductID + "'";
							drowDemandWorkOrders = pdtbDemandWOs.Select(strExpression);
						}
						// total demand = demand from SO + demand from WO + demand from CPO + safety stock
						if (drowSaleOrders != null && drowSaleOrders.Length > 0)
						{
							foreach (DataRow drowSO in drowSaleOrders)
							{
								decSaleOrder += decimal.Parse(drowSO[Constants.DEMAND_QUANTITY_FLD].ToString());
								drowSO[Constants.DEMAND_QUANTITY_FLD] = decimal.Zero;
							}
							pdtbSaleOrders.AcceptChanges();
						}
						if (drowDemandWorkOrders != null && drowDemandWorkOrders.Length > 0)
						{
							foreach (DataRow drowWOs in drowDemandWorkOrders)
							{
								decDemandWOs += decimal.Parse(drowWOs[Constants.DEMAND_QUANTITY_FLD].ToString());
								// update demand quantity
								drowWOs[Constants.DEMAND_QUANTITY_FLD] = decimal.Zero;
							}
							pdtbDemandWOs.AcceptChanges();
						}

						#region Demand from parent CPO

						// find the parent CPO which have start date in range of plan date
						if (pdtbCPOs.Rows.Count > 0)
						{
							foreach (DataRow drowCPO in pdtbCPOs.Rows)
							{
								int intParentID = int.Parse(drowCPO[MTR_CPOTable.PRODUCTID_FLD].ToString());
								// found the parent CPO
								if (intParentID == pintParentID)
								{
									voParentCPO = new MTR_CPOVO();
									voParentCPO.ProductID = intParentID;
									voParentCPO.CPOID = int.Parse(drowCPO[MTR_CPOTable.CPOID_FLD].ToString());
									voParentCPO.StartDate = (DateTime) drowCPO[MTR_CPOTable.STARTDATE_FLD];
									// now find extracly the parent CPO which have demand today
									if (voParentCPO.StartDate >= dtmPrevDate && voParentCPO.StartDate <= dtmDueDate)
									{
										DataRow[] drowComponent = pdtbAllProducts.Select("ParentID = '" + pintParentID + "' AND ProductID = '" + voProduct.ProductID + "'");
										if (drowComponent.Length == 1)
										{
											// demand from parent's CPO
											decimal decRequiredQuantity = 0;
											try
											{
												decRequiredQuantity = decimal.Parse(drowComponent[0]["RequiredQuantity"].ToString());
											}
											catch
											{
											}
											voParentCPO.Quantity = decimal.Parse(drowCPO[MTR_CPOTable.QUANTITY_FLD].ToString());
											if (decRequiredQuantity > 0)
												decDemandFromParentCPO += voParentCPO.Quantity * decRequiredQuantity;
											else
												decDemandFromParentCPO += voParentCPO.Quantity;
										}
										else
											decDemandFromParentCPO += voParentCPO.Quantity;
									}
								}
							} //for (int j = 0; j < parrCPOs.Count; j++)
						} //if (parrCPOs.Count > 0)

						#endregion

						#endregion

						#region Calculate Supplies

						// retrieve supply from work order of current day
						if (pdtbSupplyWOs != null)
						{
							strExpression = PRO_WorkOrderDetailTable.DUEDATE_FLD
								+ " >= '" + dtmPrevDate.ToString("G", DateTimeFormatInfo.CurrentInfo) + "' AND "
								+ PRO_WorkOrderDetailTable.DUEDATE_FLD + " <= '" + dtmDueDate.ToString("G", DateTimeFormatInfo.CurrentInfo) + "'"
								+ " AND " + PRO_WorkOrderDetailTable.PRODUCTID_FLD + "='" + voProduct.ProductID + "'";
							drowSupplyWorkOrders = pdtbSupplyWOs.Select(strExpression);
						}
						// retrieve supply from purchase order of current day
						if (pdtbPOs != null)
						{
							strExpression = PO_DeliveryScheduleTable.SCHEDULEDATE_FLD
								+ " >= '" + dtmPrevDate.ToString("G", DateTimeFormatInfo.CurrentInfo) + "' AND "
								+ PO_DeliveryScheduleTable.SCHEDULEDATE_FLD + " <= '" + dtmDueDate.ToString("G", DateTimeFormatInfo.CurrentInfo) + "'"
								+ " AND " + PO_PurchaseOrderDetailTable.PRODUCTID_FLD + "='" + voProduct.ProductID + "'";
							drowPurchaseOrders = pdtbPOs.Select(strExpression);
						}
						// total supply of day = Supply from WO + Supply from PO
						if (drowSupplyWorkOrders != null && drowSupplyWorkOrders.Length > 0)
						{
							foreach (DataRow drowWOs in drowSupplyWorkOrders)
							{
								decSupplyWOs += decimal.Parse(drowWOs[Constants.SUPPLY_QUANTITY_FLD].ToString());
								// update supply quantity from data source
								drowWOs[Constants.SUPPLY_QUANTITY_FLD] = decimal.Zero;
							}
							pdtbSupplyWOs.AcceptChanges();
						}
						if (drowPurchaseOrders != null && drowPurchaseOrders.Length > 0)
						{
							foreach (DataRow drowPOs in drowPurchaseOrders)
							{
								decSupplyPOs += decimal.Parse(drowPOs[Constants.SUPPLY_QUANTITY_FLD].ToString());
								// update supply quantity from data source
								drowPOs[Constants.SUPPLY_QUANTITY_FLD] = decimal.Zero;
							}
							pdtbPOs.AcceptChanges();
						}

						#endregion

						// if today has no order then go to next day
						if ((decSaleOrder + decDemandWOs) == decimal.Zero)
						{
							// check demand from Parent CPO
							if (decDemandFromParentCPO <= decimal.Zero)
							{
								// plus today supply quantity to remain quantity of next day.
								decRemainQuantity += (decNetAvailableQuantity + decSupplyPOs + decSupplyWOs);
								continue;
							}
						}

						#region MPS Calculation

						// re-calculate NetAvailableQuantity of day = supply - demand
						decNetAvailableQuantity = (decSupplyPOs + decSupplyWOs + decRemainQuantity) - ((decSaleOrder + decDemandWOs));
						// if we can supply for today's demand, go to next day
						if (decNetAvailableQuantity >= 0)
						{
							if (voParentCPO != null && voParentCPO.CPOID > 0
								&& (decNetAvailableQuantity - decDemandFromParentCPO) < 0)
							{
								#region Generate CPO for demand from parent CPO

								// create new row in CPO table
								DataRow drowNewCPO = pdtbCPOs.NewRow();
								drowNewCPO[MTR_CPOTable.PRODUCTID_FLD] = voProduct.ProductID;
								drowNewCPO[MTR_CPOTable.STOCKUMID_FLD] = voProduct.StockUMID;
								drowNewCPO[MTR_CPOTable.PARENTCPOID_FLD] = voParentCPO.CPOID;
								// Due date of child must equal to start date of parent
								drowNewCPO[MTR_CPOTable.DUEDATE_FLD] = voParentCPO.StartDate;
								// total demand of the day
								//Debug.WriteLine(decimal.Round(decDemandFromParentCPO + decSaleOrder + decDemandWOs, 5));
								drowNewCPO[MTR_CPOTable.DEMANDQUANTITY_FLD] = decimal.Round(decDemandFromParentCPO + decSaleOrder + decDemandWOs, 5);
								// total supply of the day
								drowNewCPO[MTR_CPOTable.SUPPLYQUANTITY_FLD] = decimal.Round(decSupplyPOs + decSupplyWOs + decRemainQuantity, 5);
								// quantity need to be produce
								decimal decQuantity = decimal.Round(Math.Abs(decDemandFromParentCPO - decNetAvailableQuantity)/decYieldRate, 5);
								drowNewCPO[MTR_CPOTable.QUANTITY_FLD] = decQuantity;
								// convert lead time from seconds to days
								// one day = 86400 seconds.
								decimal decDayToAdd = (voProduct.LTFixedTime + (voProduct.LTVariableTime*decQuantity) + voProduct.LTDocToStock
									+ voProduct.LTOrderPrepare + voProduct.LTShippingPrepare)/86400;
								//Debug.WriteLine(decDayToAdd);
								//Debug.WriteLine(voProduct.Code + ":" + (decDayToAdd * 86400).ToString());
								// convert start date to valid working day
								drowNewCPO[MTR_CPOTable.STARTDATE_FLD] = ConvertWorkingDay(pdtbDayOfWeek, pdtbHolidays, voParentCPO.StartDate, decDayToAdd);
								// put CPO to datatable in order to use as Parent CPO
								pdtbCPOs.Rows.Add(drowNewCPO);
								// reset remain quantity for next day
								decRemainQuantity = 0;
								// reset available quantity
								decNetAvailableQuantity = 0;

								#endregion
							}
						}
						else // if NetAvailableQuantity < 0, we generate new CPO
						{
							#region Generate CPO for demand from SOs, WOs

							DataRow drowNewCPO = pdtbCPOs.NewRow();
							drowNewCPO[MTR_CPOTable.PRODUCTID_FLD] = voProduct.ProductID;
							drowNewCPO[MTR_CPOTable.STOCKUMID_FLD] = voProduct.StockUMID;
							// Due date of child must equal to start date of parent
							drowNewCPO[MTR_CPOTable.DUEDATE_FLD] = dtmDueDate;
							// total demand of the day
							//Debug.WriteLine(decimal.Round(decSaleOrder + decDemandWOs, 5));
							drowNewCPO[MTR_CPOTable.DEMANDQUANTITY_FLD] = decimal.Round(decSaleOrder + decDemandWOs, 5);
							// total supply of the day
							drowNewCPO[MTR_CPOTable.SUPPLYQUANTITY_FLD] = decimal.Round(decSupplyPOs + decSupplyWOs + decRemainQuantity, 5);
							// quantity need to be produce
							decimal decQuantity = decimal.Round(Math.Abs(decNetAvailableQuantity/decYieldRate), 5);
							drowNewCPO[MTR_CPOTable.QUANTITY_FLD] = decQuantity;
							// convert lead time from seconds to days
							// one day = 86400 seconds.
							decimal decDayToAdd = (voProduct.LTFixedTime + (voProduct.LTVariableTime*decQuantity) + voProduct.LTDocToStock
								+ voProduct.LTOrderPrepare + voProduct.LTShippingPrepare)/86400;
							//Debug.WriteLine(voProduct.Code + ":" + (decDayToAdd * 86400).ToString());
							// convert start date to valid working day
							drowNewCPO[MTR_CPOTable.STARTDATE_FLD] = ConvertWorkingDay(pdtbDayOfWeek, pdtbHolidays, dtmDueDate, decDayToAdd);
							// put CPO to DataTable in order to use as Parent CPO
							pdtbCPOs.Rows.Add(drowNewCPO);

							#endregion

							#region Generate CPO for demand from parent CPO

							if (voParentCPO != null && voParentCPO.CPOID > 0
								&& decDemandFromParentCPO > decimal.Zero)
							{
								// create new row in CPO table
								DataRow drowNewCPOForParent = pdtbCPOs.NewRow();
								drowNewCPOForParent[MTR_CPOTable.PRODUCTID_FLD] = voProduct.ProductID;
								drowNewCPOForParent[MTR_CPOTable.STOCKUMID_FLD] = voProduct.StockUMID;
								drowNewCPOForParent[MTR_CPOTable.PARENTCPOID_FLD] = voParentCPO.CPOID;
								// Due date of child must equal to start date of parent
								drowNewCPOForParent[MTR_CPOTable.DUEDATE_FLD] = voParentCPO.StartDate;
								//Debug.WriteLine(decimal.Round(decDemandFromParentCPO, 5));
								// total demand of the day
								drowNewCPOForParent[MTR_CPOTable.DEMANDQUANTITY_FLD] = decimal.Round(decDemandFromParentCPO, 5);
								// total supply of the day
								drowNewCPOForParent[MTR_CPOTable.SUPPLYQUANTITY_FLD] = decimal.Zero;
								// quantity need to be produce
								decQuantity = decimal.Round(Math.Abs(decDemandFromParentCPO)/decYieldRate, 5);
								drowNewCPOForParent[MTR_CPOTable.QUANTITY_FLD] = decQuantity;
								// convert lead time from seconds to days
								// one day = 86400 seconds.
								decDayToAdd = (voProduct.LTFixedTime + (voProduct.LTVariableTime*decQuantity) + voProduct.LTDocToStock
									+ voProduct.LTOrderPrepare + voProduct.LTShippingPrepare)/86400;
								//Debug.WriteLine(voProduct.Code + ":" + (decDayToAdd * 86400).ToString());
								// convert start date to valid working day
								drowNewCPOForParent[MTR_CPOTable.STARTDATE_FLD] = ConvertWorkingDay(pdtbDayOfWeek, pdtbHolidays, voParentCPO.StartDate, decDayToAdd);
								// put CPO to datatable in order to use as Parent CPO
								pdtbCPOs.Rows.Add(drowNewCPOForParent);
							}

							#endregion

							// reset remain quantity for next day
							decRemainQuantity = 0;
							// reset available quantity
							decNetAvailableQuantity = 0;
						}

						#endregion
					} // foreach (DateTime dtmDueDate in parrDateTimes)

					#endregion
				} // if (voProduct.PlanType.Equals((int)PlanTypeEnum.MPS))
				// if current product has component(s)
				if (drowComponents.Length > 0)
				{
					// we will generate plan for all component of current product
					GeneratePlanOffline(pdtbAllProducts, drowComponents, pintMasterLocationID, parrDateTimes,
					                    pobjMPSCycleOptionMaster, voProduct.ProductID, pdtbInventory, pdtbSaleOrders,
					                    pdtbDemandWOs, pdtbSupplyWOs, pdtbPOs, pdtbCPOs, pdtbDayOfWeek, pdtbHolidays);
				}
			} // foreach (DataRow drowItem in pdtbItems.Rows)
			return pdtbCPOs;
		}


		/// <summary>
		/// Convert working day with offline method
		/// </summary>
		/// <param name="pdtbDayOfWeek">Valid work day of week</param>
		/// <param name="pdtbHolidays">Holidays in year</param>
		/// <param name="pdtmDate">Date to convert</param>
		/// <param name="pdecNumberOfDay">Number of day to add/subtract</param>
		/// <returns>Converted Date</returns>
		private DateTime ConvertWorkingDay(DataTable pdtbDayOfWeek, DataTable pdtbHolidays, 
			DateTime pdtmDate, decimal pdecNumberOfDay)
		{
			int intNumberOfDay = (int) decimal.Floor(pdecNumberOfDay);
			double dblRemainder = (double) (pdecNumberOfDay - (decimal) intNumberOfDay);

			ArrayList arrDayOfWeek = new ArrayList();
			if (pdtbDayOfWeek != null)
			{
				DataRow[] drowWorkingDay = pdtbDayOfWeek.Select(MST_WorkingDayMasterTable.YEAR_FLD + "='" + pdtmDate.Year + "'");
				if (drowWorkingDay.Length != 0)
				{
					DataRow drow = drowWorkingDay[0];

					if (!bool.Parse(drow[MST_WorkingDayMasterTable.MON_FLD].ToString()))
					{
						arrDayOfWeek.Add(DayOfWeek.Monday);
					}

					if (!bool.Parse(drow[MST_WorkingDayMasterTable.TUE_FLD].ToString()))
					{
						arrDayOfWeek.Add(DayOfWeek.Tuesday);
					}

					if (!bool.Parse(drow[MST_WorkingDayMasterTable.WED_FLD].ToString()))
					{
						arrDayOfWeek.Add(DayOfWeek.Wednesday);
					}

					if (!bool.Parse(drow[MST_WorkingDayMasterTable.THU_FLD].ToString()))
					{
						arrDayOfWeek.Add(DayOfWeek.Thursday);
					}

					if (!bool.Parse(drow[MST_WorkingDayMasterTable.FRI_FLD].ToString()))
					{
						arrDayOfWeek.Add(DayOfWeek.Friday);
					}

					if (!bool.Parse(drow[MST_WorkingDayMasterTable.SAT_FLD].ToString()))
					{
						arrDayOfWeek.Add(DayOfWeek.Saturday);
					}

					if (!bool.Parse(drow[MST_WorkingDayMasterTable.SUN_FLD].ToString()))
					{
						arrDayOfWeek.Add(DayOfWeek.Sunday);
					}
				}
			}

			ArrayList arrHolidays = new ArrayList();
			if (pdtbHolidays != null)
			{
				DataRow[] drowHoliday = pdtbHolidays.Select(MST_WorkingDayMasterTable.YEAR_FLD + "='" + pdtmDate.Year + "'");
				if (drowHoliday.Length != 0)
				{
					// have data--> create new array list to add items
					for (int i = 0; i < drowHoliday.Length; i++)
					{
						DateTime dtmTemp = DateTime.Parse(pdtbHolidays.Rows[i][MST_WorkingDayDetailTable.OFFDAY_FLD].ToString());
						// truncate hour, minute, second
						dtmTemp = new DateTime(dtmTemp.Year, dtmTemp.Month, dtmTemp.Day);
						arrHolidays.Add(dtmTemp);
					}
				}
			}

			DateTime dtmConvert = pdtmDate;
			for(int i =0; i < intNumberOfDay; i++)
			{							
				// 
				dtmConvert = dtmConvert.AddDays(-1);

				// goto next day if the day is holidayday
				while(arrHolidays.Contains(new DateTime(dtmConvert.Year, dtmConvert.Month, dtmConvert.Day)))
				{
					dtmConvert = dtmConvert.AddDays(-1);
				}

				// goto next day if the day is off day
				while(arrDayOfWeek.Contains( dtmConvert.DayOfWeek))
				{
					dtmConvert = dtmConvert.AddDays(-1);
					if(arrHolidays.Contains(new DateTime(dtmConvert.Year, dtmConvert.Month, dtmConvert.Day)))
					{
						dtmConvert = dtmConvert.AddDays(-1);
					}
				}

				// goto next day if the day is holiday
				while(arrHolidays.Contains(new DateTime(dtmConvert.Year, dtmConvert.Month, dtmConvert.Day)))
				{
					dtmConvert = dtmConvert.AddDays(-1);
				}
			}
						
			// Add remainder
			dtmConvert = dtmConvert.AddDays(-dblRemainder);

			// goto next day if the day is holidayday
			while(arrHolidays.Contains(new DateTime(dtmConvert.Year, dtmConvert.Month, dtmConvert.Day)))
			{
				dtmConvert = dtmConvert.AddDays(-1);
			}
						
			// goto next day if the day is off day
			while(arrDayOfWeek.Contains(dtmConvert.DayOfWeek))
			{
				dtmConvert = dtmConvert.AddDays(-1);
				if(arrHolidays.Contains(new DateTime(dtmConvert.Year, dtmConvert.Month, dtmConvert.Day)))
				{
					dtmConvert = dtmConvert.AddDays(-1);
				}
			}
						
			// goto next day if the day is holidayday
			while(arrHolidays.Contains(new DateTime(dtmConvert.Year, dtmConvert.Month, dtmConvert.Day)))
			{
				dtmConvert = dtmConvert.AddDays(-1);
			}

			return dtmConvert;
		}


		/// <summary>
		/// Get Cycle Detail from Master
		/// </summary>
		/// <param name="pintCycleMasterID">Cycle Master</param>
		/// <returns>Details of a Cycle</returns>
	
		public DataTable GetCycleDetail(int pintCycleMasterID)
		{
			// get all detail data of MPSCycleOptionMaster
			MTR_MPSCycleOptionDetailDS dsMPSCycleOptionDetail = new MTR_MPSCycleOptionDetailDS();
			return dsMPSCycleOptionDetail.GetDetailByMaster(pintCycleMasterID);
		}

		/// <summary>
		/// Get All products need to be planned from CCN
		/// </summary>
		/// <param name="pintCCNID">CCN</param>
		/// <returns>All Products</returns>
	
		public DataTable GetAllProducts(int pintCCNID)
		{
			MPSRegenerationProcessDS dsMPSRegenerationProcess = new MPSRegenerationProcessDS();
			return dsMPSRegenerationProcess.GetAllProducts(pintCCNID);
		}

		/// <summary>
		/// Get all valid day of all configured year in system
		/// </summary>
		/// <returns></returns>
	
		public DataTable GetDayOfWeek()
		{
			UtilsDS dsUtils = new UtilsDS();
			// get all valid day of all configured year in system
			return dsUtils.GetWorkingDay();
		}

		/// <summary>
		/// Get all holidays in system.
		/// </summary>
		/// <returns></returns>
	
		public DataTable GetHolidays()
		{
			UtilsDS dsUtils = new UtilsDS();
			// get all holidays in system.
			return dsUtils.GetHolidays();
		}

		/// <summary>
		/// Get available quantity of all product in CCN and Master Location
		/// </summary>
		/// <param name="pintCCNID">CCN</param>
		/// <param name="pintMasterLocationID">Master Location</param>
		/// <returns>DataTable</returns>
	
		public DataTable GetAvailableQuantityForPlan(int pintCCNID, int pintMasterLocationID)
		{
			IV_MasLocCacheDS dsMasLocCache = new IV_MasLocCacheDS();
			return dsMasLocCache.GetAvailableQuantityForPlan(pintCCNID, pintMasterLocationID);
		}

		/// <summary>
		/// Retrieve all demand from sale order
		/// </summary>
		/// <param name="pintCCNID">CCN</param>
		/// <param name="pintMasterLocationID">Master Location</param>
		/// <param name="pdtmAsOfDate">As Of Date</param>
		/// <param name="pdtmDueDate">Due Date</param>
		/// <returns>DataTable</returns>
	
		public DataTable RetrieveSaleOrders(int pintCCNID, int pintMasterLocationID, DateTime pdtmAsOfDate, DateTime pdtmDueDate)
		{
			MPSRegenerationProcessDS dsMPSRegenerationProcess = new MPSRegenerationProcessDS();
			return dsMPSRegenerationProcess.RetrieveSaleOrders(pintCCNID, pintMasterLocationID, pdtmAsOfDate, pdtmDueDate);
		}

		/// <summary>
		/// Retrieve all demand from work order of parent
		/// </summary>
		/// <param name="pintCCNID">CCN</param>
		/// <param name="pintMasterLocationID">Master Location</param>
		/// <param name="pdtmAsOfDate">As Of Date</param>
		/// <param name="pdtmDueDate">Due Date</param>
		/// <returns>DataTable</returns>
	
		public DataTable RetrieveParents(int pintCCNID, int pintMasterLocationID, DateTime pdtmAsOfDate, DateTime pdtmDueDate)
		{
			MPSRegenerationProcessDS dsMPSRegenerationProcess = new MPSRegenerationProcessDS();
			return dsMPSRegenerationProcess.RetrieveParents(pintCCNID, pintMasterLocationID, pdtmAsOfDate, pdtmDueDate);
		}

		/// <summary>
		/// Retrieve all supply from work order
		/// </summary>
		/// <param name="pintCCNID">CCN</param>
		/// <param name="pintMasterLocationID">Master Location</param>
		/// <param name="pdtmAsOfDate">As Of Date</param>
		/// <param name="pdtmDueDate">Due Date</param>
		/// <returns>DataTable</returns>
	
		public DataTable RetrieveSupplyFromWO(int pintCCNID, int pintMasterLocationID, DateTime pdtmAsOfDate, DateTime pdtmDueDate)
		{
			MPSRegenerationProcessDS dsMPSRegenerationProcess = new MPSRegenerationProcessDS();
			return dsMPSRegenerationProcess.RetrieveSupplyFromWO(pintCCNID, pintMasterLocationID, pdtmAsOfDate, pdtmDueDate);
		}

		/// <summary>
		/// Retrieve all supply from purchase order
		/// </summary>
		/// <param name="pintCCNID">CCN</param>
		/// <param name="pintMasterLocationID">Master Location</param>
		/// <param name="pdtmAsOfDate">As Of Date</param>
		/// <param name="pdtmDueDate">Due Date</param>
		/// <returns>DataTable</returns>
	
		public DataTable RetriveSupplyFromPO(int pintCCNID, int pintMasterLocationID, DateTime pdtmAsOfDate, DateTime pdtmDueDate)
		{
			MPSRegenerationProcessDS dsMPSRegenerationProcess = new MPSRegenerationProcessDS();
			return dsMPSRegenerationProcess.RetriveSupplyFromPO(pintCCNID, pintMasterLocationID, pdtmAsOfDate, pdtmDueDate);
		}

		/// <summary>
		/// Save all generated CPO from client to database. First we need to delete all existed CPO
		/// of selected cycle
		/// </summary>
		/// <param name="pintCCNID">CCN</param>
		/// <param name="pintCycleID">Cycle Master</param>
	
		public void DeleteCPOs(int pintCCNID, int pintCycleID)
		{
			MTR_CPODS dsCPO = new MTR_CPODS();
			// delete all generated CPO of selected Cycle Option
			dsCPO.Delete(pintCCNID, pintCycleID, true);
		}

		/// <summary>
		/// Get all sale orders in the period of time
		/// </summary>
		/// <param name="pintCCNID">CCN</param>
		/// <param name="pdtmFromDate">From Date</param>
		/// <param name="pdtmToDate">To Date</param>
		/// <returns>List of Sale Orders</returns>
	
		public DataTable GetTotalSO(int pintCCNID, DateTime pdtmFromDate, DateTime pdtmToDate, int pintWorkCenterID)
		{
			MPSRegenerationProcessDS dsMPS = new MPSRegenerationProcessDS();
			return dsMPS.GetTotalSO(pintCCNID, pdtmFromDate, pdtmToDate, pintWorkCenterID);
		}

		/// <summary>
		/// Get all purchase orders in the period of time
		/// </summary>
		/// <param name="pintCCNID">CCN</param>
		/// <param name="pdtmFromDate">From Date</param>
		/// <param name="pdtmToDate">To Date</param>
		/// <returns>List of Purchase Orders</returns>
	
		public DataTable GetTotalPO(int pintCCNID, DateTime pdtmFromDate, DateTime pdtmToDate, int pintWorkCenterID)
		{
			MPSRegenerationProcessDS dsMPS = new MPSRegenerationProcessDS();
			return dsMPS.GetTotalPO(pintCCNID, pdtmFromDate, pdtmToDate, pintWorkCenterID);
		}

		/// <summary>
		/// Get all work orders in the period of time
		/// </summary>
		/// <param name="pintCCNID">CCN</param>
		/// <param name="pdtmFromDate">From Date</param>
		/// <param name="pdtmToDate">To Date</param>
		/// <returns>List of Work Orders</returns>
	
		public DataTable GetTotalWO(int pintCCNID, DateTime pdtmFromDate, DateTime pdtmToDate, int pintWorkCenterID)
		{
			MPSRegenerationProcessDS dsMPS = new MPSRegenerationProcessDS();
			return dsMPS.GetTotalWO(pintCCNID, pdtmFromDate, pdtmToDate, pintWorkCenterID);
		}
		/// <summary>
		/// Get all demand work orders in the period of time
		/// </summary>
		/// <param name="pintCCNID">CCN</param>
		/// <param name="pdtmFromDate">From Date</param>
		/// <param name="pdtmToDate">To Date</param>
		/// <returns>List of Work Orders</returns>
	
		public DataTable GetTotalDemandWO(int pintCCNID, DateTime pdtmFromDate, DateTime pdtmToDate, int pintWorkCenterID)
		{
			MPSRegenerationProcessDS dsMPS = new MPSRegenerationProcessDS();
			return dsMPS.RetrieveParents(pintCCNID, pdtmFromDate, pdtmToDate);
		}
		/// <summary>
		/// Get all demand work orders in the period of time
		/// </summary>
		/// <param name="pintCCNID">CCN</param>
		/// <param name="pdtmFromDate">From Date</param>
		/// <param name="pdtmToDate">To Date</param>
		/// <returns>List of Work Orders</returns>
		/// <author>DuongNA</author>
	
		public DataTable GetDemandWO(int pintCCNID, DateTime pdtmFromDate, DateTime pdtmToDate, int pintWorkCenterID)
		{
			MPSRegenerationProcessDS dsMPS = new MPSRegenerationProcessDS();
			return dsMPS.GetDemandWO(pintCCNID, pdtmFromDate, pdtmToDate, pintWorkCenterID);
		}
	}
}