using System;
using System.Collections.Generic;
using System.Data;
using PCSComUtils.Common;
using PCSComUtils.Framework.ReportFrame.DS;

namespace PCSComUtils.Framework.ReportFrame.BO
{
	public class C1PrintPreviewDialogBO
	{
		private const string THIS = "PCSComUtils.Framework.ReportFrame.BO.C1PrintPreviewDialogBO";		
		public C1PrintPreviewDialogBO()
		{
		}

		#region ISSUE SLIP REPORT. THACHNN

		//**************************************************************************              
		///    <Description>
		///       Get CategoryCode From Line And WorkOrderNo
		///    </Description>
		///    <Inputs>
		///        Line
		///    </Inputs>		
		///    <Inputs>
		///        WOrk Order No
		///    </Inputs>		
		///    <Returns>
		///       string Category Code
		///    </Returns>
		///    <Authors>
		///       Thachnn
		///    </Authors>
		///    <History>
		///       28-10-2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
	
		public string GetCategoryCodeFromLineAndWorkOrderNo(int pnLine,string pstrWONo)
		{
			C1PrintPreviewDialogDS objDS = new C1PrintPreviewDialogDS();
			return objDS.GetCategoryCodeFromLineAndWorkOrderNo(pnLine,pstrWONo);
		}

		//**************************************************************************              
		///    <Description>
		///       Get CategoryName From Line And WorkOrderNo
		///    </Description>
		///    <Inputs>
		///        Line
		///    </Inputs>		
		///    <Inputs>
		///        WOrk Order No
		///    </Inputs>		
		///    <Returns>
		///       string Category Name
		///    </Returns>
		///    <Authors>
		///       Thachnn
		///    </Authors>
		///    <History>
		///       28-10-2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
	
		public string GetCategoryNameFromLineAndWorkOrderNo(int pnLine,string pstrWONo)
		{			
			C1PrintPreviewDialogDS objDS = new C1PrintPreviewDialogDS();
			return objDS.GetCategoryNameFromLineAndWorkOrderNo(pnLine,pstrWONo);
		}

		//**************************************************************************              
		///    <Description>
		///       Get Product Model From Line And WorkOrderNo
		///    </Description>
		///    <Inputs>
		///        Line
		///    </Inputs>		
		///    <Inputs>
		///        WOrk Order No
		///    </Inputs>		
		///    <Returns>
		///       string Model
		///    </Returns>
		///    <Authors>
		///       Thachnn
		///    </Authors>
		///    <History>
		///       28-10-2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
	
		public string GetProductModelFromLineAndWorkOrderNo(int pnLine,string pstrWONo)
		{			
			C1PrintPreviewDialogDS objDS = new C1PrintPreviewDialogDS();
			return objDS.GetProductModelFromLineAndWorkOrderNo(pnLine,pstrWONo);
		}


		#endregion

		#region INVENTORY STATUS REPORT. THACHNN

		/// <summary>
		/// Thachnn: 28/10/2005 - my bd
		/// Return data for Inventory Status Report
		/// </summary>
		/// <param name="pnCCNID"></param>
		/// <param name="pnMasterLocationID"></param>
		/// <param name="pnLocationID"></param>
		/// <param name="pnCategoryID"></param>
		/// <returns></returns>
	
		public DataTable GetInventoryStatusFromCCNMasLocLocationCategory(int pnCCNID, int pnMasterLocationID,int pnLocationID, int pnCategoryID)
		{
			C1PrintPreviewDialogDS objDS = new C1PrintPreviewDialogDS();
			return objDS.GetInventoryStatusFromCCNMasLocLocationCategory(pnCCNID,pnMasterLocationID,pnLocationID,pnCategoryID);
		}



		/// <summary>
		/// Thachnn 07/01/2006
		/// </summary>
		/// <param name="pnID"></param>
		/// <returns></returns>		
	
		public string GetLocationCodeFromID(int pnID)
		{
			C1PrintPreviewDialogDS objDS = new C1PrintPreviewDialogDS();
			return objDS.GetLocationCodeFromID(pnID);			
		}

		/// <summary>
		/// Thachnn 07/01/2006
		/// </summary>
		/// <param name="pnID"></param>
		/// <returns></returns>		
	
		public string GetLocationNameFromID(int pnID)
		{
			C1PrintPreviewDialogDS objDS = new C1PrintPreviewDialogDS();
			return objDS.GetLocationNameFromID(pnID);
		}



		/// <summary>
		/// Thachnn 22/01/2006
		/// </summary>
		/// <param name="pnID"></param>
		/// <returns></returns>		
	
		public string GetBinCodeFromID(int pnID)
		{
			C1PrintPreviewDialogDS objDS = new C1PrintPreviewDialogDS();
			return objDS.GetBinCodeFromID(pnID);			
		}



		#endregion
		
		#region CPO REPORT. THACHNN
		/// <summary>
		/// THACHNN : 08/11/2005
		/// Return data for rendering CPO Report,		
		/// </summary>
		/// <param name="pstrPlanType">MPS or MRP string</param>
		/// <param name="pnCCNID"></param>
		/// <param name="pnMonth"></param>
		/// <param name="pnYear"></param>
		/// <param name="pnCycle"></param>
		/// <param name="pnProductionLineID"></param>
		/// <param name="pnMasterLocationID"></param>
		/// <param name="pnCategoryID"></param>
		/// <returns></returns>
	
		public DataTable GetCPOReportData(string pstrPlanType, int pnCCNID, int pnMonth, int pnYear, int pnCycle,
			int pnProductionLineID, int pnMasterLocationID, int pnCategoryID,
			int pnVendorID, int pnProductID, string pstrModel)
		{
			C1PrintPreviewDialogDS objDS = new C1PrintPreviewDialogDS();
			pstrPlanType = pstrPlanType.Trim();
			if(pstrPlanType == PlanTypeEnum.MPS.ToString())
			{
				return objDS.GetMPS_CPOReportData( pnCCNID,  pnMonth,  pnYear,  pnCycle,  pnProductionLineID,
					pnMasterLocationID,  pnCategoryID, pnVendorID, pnProductID, pstrModel);
			}
			else if(pstrPlanType == PlanTypeEnum.MRP.ToString())
			{
				return objDS.GetMRP_CPOReportData( pnCCNID,  pnMonth,  pnYear,  pnCycle,  pnProductionLineID,
					pnMasterLocationID,  pnCategoryID, pnVendorID, pnProductID, pstrModel);
			}
			else
				return new DataTable();
		}


		/// <summary>
		/// Thachnn 10/11/2005
		/// </summary>
		/// <param name="pnID"></param>
		/// <returns></returns>
	
		public string GetMRPCycleFromID(int pnID)
		{
			C1PrintPreviewDialogDS objDS = new C1PrintPreviewDialogDS();
			return objDS.GetMRPCycleFromID(pnID);
		}
		/// <summary>
		/// Thachnn 10/11/2005
		/// </summary>
		/// <param name="pnID"></param>
		/// <returns></returns>
	
		public string GetMPSCycleFromID(int pnID)
		{
			C1PrintPreviewDialogDS objDS = new C1PrintPreviewDialogDS();
			return objDS.GetMPSCycleFromID(pnID);									
		}
		/// <summary>
		/// Thachnn 10/11/2005
		/// </summary>
		/// <param name="pnID"></param>
		/// <returns></returns>
	
		public string GetMRPCycleDescriptionFromID(int pnID)
		{
			C1PrintPreviewDialogDS objDS = new C1PrintPreviewDialogDS();
			return objDS.GetMRPCycleDescriptionFromID(pnID);
		}
		/// <summary>
		/// Thachnn 10/11/2005
		/// </summary>
		/// <param name="pnID"></param>
		/// <returns></returns>
	
		public string GetMPSCycleDescriptionFromID(int pnID)
		{
			C1PrintPreviewDialogDS objDS = new C1PrintPreviewDialogDS();
			return objDS.GetMPSCycleDescriptionFromID(pnID);									
		}

		
		/// <summary>
		/// Thachnn 10/11/2005
		/// </summary>
		/// <param name="pnID"></param>
		/// <returns></returns>
	
		public string GetProductLineCodeFromID(int pnID)
		{
			C1PrintPreviewDialogDS objDS = new C1PrintPreviewDialogDS();
			return objDS.GetProductLineCodeFromID(pnID);			
		}
		/// <summary>
		/// Thachnn 10/11/2005
		/// </summary>
		/// <param name="pnID"></param>
		/// <returns></returns>
	
		public string GetProductLineNameFromID(int pnID)
		{
			C1PrintPreviewDialogDS objDS = new C1PrintPreviewDialogDS();
			return objDS.GetProductLineNameFromID(pnID);			
		}
		/// <summary>
		/// Thachnn 10/11/2005
		/// </summary>
		/// <param name="pnID"></param>
		/// <returns></returns>		
	
		public string GetMasterLocationCodeFromID(int pnID)
		{
			C1PrintPreviewDialogDS objDS = new C1PrintPreviewDialogDS();
			return objDS.GetMasterLocationCodeFromID(pnID);			
		}
		/// <summary>
		/// Thachnn 10/11/2005
		/// </summary>
		/// <param name="pnID"></param>
		/// <returns></returns>		
	
		public string GetMasterLocationNameFromID(int pnID)
		{
			C1PrintPreviewDialogDS objDS = new C1PrintPreviewDialogDS();
			return objDS.GetMasterLocationNameFromID(pnID);			
		}
		/// <summary>
		/// Thachnn 10/11/2005
		/// </summary>
		/// <param name="pnID"></param>
		/// <returns></returns>		
	
		public string GetCategoryNameFromID(int pnID)
		{
			C1PrintPreviewDialogDS objDS = new C1PrintPreviewDialogDS();
			return objDS.GetCategoryNameFromID(pnID);			
		}

		/// <summary>
		/// Thachnn 10/11/2005
		/// </summary>
		/// <param name="pnID"></param>
		/// <returns></returns>		
	
		public string GetCategoryCodeFromID(int pnID)
		{
			C1PrintPreviewDialogDS objDS = new C1PrintPreviewDialogDS();
			return objDS.GetCategoryCodeFromID(pnID);			
		}
	
		public string GetCategoryCodeFromID(string pstrID)
		{
			C1PrintPreviewDialogDS objDS = new C1PrintPreviewDialogDS();
			return objDS.GetCategoryCodeFromID(pstrID);			
		}

	
		public string GetVendorInfo(int pnID)
		{
			C1PrintPreviewDialogDS objDS = new C1PrintPreviewDialogDS();
			return objDS.GetVendorInfo(pnID);			
		}
	
		public string GetItemInfor(int pnID)
		{
			C1PrintPreviewDialogDS objDS = new C1PrintPreviewDialogDS();
			return objDS.GetItemInfor(pnID);			
		}
		
		#endregion

		#region SCHEDULE OF LOCAL PARTS IN MONTH REPORT. THACHNN

		/// <summary>
		/// THACHNN: 11/11/2005
		/// Getting the data for Schedule of local parts in month REport		
		/// </summary>
		/// <param name="pnCCNID"></param>
		/// <param name="pnMonth"></param>
		/// <param name="pnYear"></param>
		/// <param name="pnVendorID"></param>
		/// <param name="pnCategoryID"></param>
		/// <param name="pnProductID"></param>
		/// <returns></returns>
	
		public DataSet GetScheduleOfLocalPartsInMonthReportData(int pnCCNID, int pnMonth, int pnYear, int pnVendorID,  int pnCategoryID, int pnProductID)
		{
			DataSet dstRet = new DataSet("SOLPIMReport");
			dstRet.Tables.Add( (new C1PrintPreviewDialogDS()).GetScheduleOfLocalPartsInMonthReportData(pnCCNID,pnMonth		,pnYear,pnVendorID,pnCategoryID,pnProductID));
			DataTable dtbNextMonth = (new C1PrintPreviewDialogDS()).GetScheduleOfLocalPartsInMonthReportData(pnCCNID,pnMonth+1	,pnYear,pnVendorID,pnCategoryID,pnProductID);
			dtbNextMonth.TableName = dtbNextMonth.TableName  + "NextMonth";
			dstRet.Tables.Add(dtbNextMonth);
			DataTable dtbNextNextMonth = (new C1PrintPreviewDialogDS()).GetScheduleOfLocalPartsInMonthReportData(pnCCNID,pnMonth+2	,pnYear,pnVendorID,pnCategoryID,pnProductID);
			dtbNextNextMonth.TableName = dtbNextNextMonth.TableName  + "NextNextMonth";
			dstRet.Tables.Add(dtbNextNextMonth);
			return dstRet;
		}


		/// <summary>
		/// Thachnn 10/11/2005
		/// </summary>
		/// <param name="pnID"></param>
		/// <returns></returns>		
	
		public string GetVendorNameFromID(int pnID)
		{
			C1PrintPreviewDialogDS objDS = new C1PrintPreviewDialogDS();
			return objDS.GetVendorNameFromID(pnID);			
		}

		/// <summary>
		/// Thachnn 10/11/2005
		/// </summary>
		/// <param name="pnID"></param>
		/// <returns></returns>		
	
		public string GetVendorCodeFromID(int pnID)
		{
			C1PrintPreviewDialogDS objDS = new C1PrintPreviewDialogDS();
			return objDS.GetVendorCodeFromID(pnID);			
		}



		/// <summary>
		/// Thachnn 10/11/2005
		/// </summary>
		/// <param name="pnID"></param>
		/// <returns></returns>		
	
		public string GetProductNameFromID(int pnID)
		{
			C1PrintPreviewDialogDS objDS = new C1PrintPreviewDialogDS();
			return objDS.GetProductNameFromID(pnID);			
		}

		/// <summary>
		/// Thachnn 10/11/2005
		/// </summary>
		/// <param name="pnID"></param>
		/// <returns></returns>		
	
		public string GetProductCodeFromID(int pnID)
		{
			C1PrintPreviewDialogDS objDS = new C1PrintPreviewDialogDS();
			return objDS.GetProductCodeFromID(pnID);			
		}


		#endregion

		#region PART ORDER SHEET REPORT. THACHNN
		/// <summary>
		/// THACHNN: 11/11/2005
		/// Getting the data for Schedule of local parts in month REport		
		/// </summary>
		/// <param name="pnCCNID"></param>
		/// <param name="pnMonth"></param>
		/// <param name="pnYear"></param>
		/// <param name="pnVendorID"></param>
		/// <param name="pnCategoryID"></param>
		/// <param name="pnProductID"></param>
		/// <returns></returns>
	
		public DataSet GetPartOrderSheetReportData(int pnCCNID, int pnPartyID, string pstrPurchaseOrderMasterID)
		{
				
			DataSet dstRet  = (new C1PrintPreviewDialogDS()).GetPartOrderSheetReportData(pnCCNID,pnPartyID, pstrPurchaseOrderMasterID);
			dstRet.DataSetName = "PartOrderSheetReport";
			return dstRet;

//			DataSet dstRet = new DataSet("PartOrderSheetReport");
//			dstRet.Tables.Add( (new C1PrintPreviewDialogDS()).GetPartOrderSheetReportData(pnCCNID,pnMonth,pnYear,pnVendorID, pstrPurchaseOrderMasterCode));
//
//			DataTable dtbNextMonth = (new C1PrintPreviewDialogDS()).GetPartOrderSheetReportData(pnCCNID,pnMonth+1	,pnYear,pnVendorID, pstrPurchaseOrderMasterCode);
//			dtbNextMonth.TableName = dtbNextMonth.TableName  + "NextMonth";
//			dstRet.Tables.Add(dtbNextMonth);
//
//			DataTable dtbNextNextMonth = (new C1PrintPreviewDialogDS()).GetPartOrderSheetReportData(pnCCNID,pnMonth+2	,pnYear,pnVendorID, pstrPurchaseOrderMasterCode);
//			dtbNextNextMonth.TableName = dtbNextNextMonth.TableName  + "NextNextMonth";
//			dstRet.Tables.Add(dtbNextNextMonth);
//
//			return dstRet;
		}

		#endregion

		#region PART ORDER SHEET REPORT MULTI VENDOR. THACHNN
		/// <summary>
		/// THACHNN: 11/11/2005
		/// Getting the data for Schedule of local parts in month REport, with multi vendors
		/// </summary>
		/// <param name="pnCCNID"></param>
		/// <param name="pnMonth"></param>
		/// <param name="pnYear"></param>
		/// <param name="pnVendorID"></param>
		/// <param name="pnCategoryID"></param>
		/// <param name="pnProductID"></param>
		/// <returns></returns>
	
		public DataSet GetPartOrderSheetMultiVendorReportData(int pnCCNID, int pnMonth, int pnYear, string pstrVendorID_List)
		{
			DataSet dstRet = new DataSet("PartOrderSheetMultiVendorReport");
			DataTable dtbCurrentMonth	= (new C1PrintPreviewDialogDS()).GetPartOrderSheetMultiVendorReportData(pnCCNID,	pnMonth,		pnYear,	pstrVendorID_List);
//			dtbCurrentMonth.TableName = 
			dstRet.Tables.Add(dtbCurrentMonth );

			DataTable dtbNextMonth		= (new C1PrintPreviewDialogDS()).GetPartOrderSheetMultiVendorReportData(pnCCNID,	pnMonth+1	,	pnYear,	pstrVendorID_List);
			dtbNextMonth.TableName = dtbNextMonth.TableName  + "NextMonth";
			dstRet.Tables.Add(dtbNextMonth);

			DataTable dtbNextNextMonth	= (new C1PrintPreviewDialogDS()).GetPartOrderSheetMultiVendorReportData(pnCCNID,	pnMonth+2	,	pnYear,	pstrVendorID_List);
			dtbNextNextMonth.TableName = dtbNextNextMonth.TableName  + "NextNextMonth";
			dstRet.Tables.Add(dtbNextNextMonth);

			return dstRet;
		}

		#endregion

		#region MONTHLY DELIVERY REPORT . THACHNN
		/// <summary>
		/// THACHNN: 11/10/2005
		/// Getting the data for Schedule of local parts in month REport
		/// </summary>		
		/// <returns></returns>
	
		public DataTable GetMonthlyDeliveryReportData(int pnCCNID, int pnMonth, int pnYear, int pnCustomerID,  int pnCategoryID, int pnProductID)
		{
			return (new C1PrintPreviewDialogDS()).GetMonthlyDeliveryReportData(pnCCNID,pnMonth,pnYear,pnCustomerID,pnCategoryID,pnProductID);
		}
		#endregion

		#region PRODUCTION LINE  REPORTS . THACHNN
		/// <summary>
		/// Thachnn 06/02/2006		
		/// </summary>
		/// <param name="pnID"></param>
		/// <returns></returns>
	
		public string GetWorkOrderMasterNoFromID(int pnID)
		{
			C1PrintPreviewDialogDS objDS = new C1PrintPreviewDialogDS();
			return objDS.GetWorkOrderMasterNoFromID(pnID);			
		}
		#endregion

		#region VENDOR DELIVERY ASSESSMENT - THACHNN
		
		/// <summary>
		/// Thachnn 10/03/2006
		/// </summary>
		/// <param name="pnID"></param>
		/// <returns></returns>		
	
		public string GetPurchaseOrderMasterCodeFromID(int pnID)
		{
			C1PrintPreviewDialogDS objDS = new C1PrintPreviewDialogDS();
			return objDS.GetPurchaseOrderMasterCodeFromID(pnID);
		}
		#endregion VENDOR DELIVERY ASSESSMENT - THACHNN

		#region SO Invoice Report: Tuan TQ
		/// <summary>		
		/// Return data for SO Invoice report
		/// </summary>
		/// <returns></returns>
		/// <author>Tuan TQ. Oct 28, 2005</author>
	
		public DataTable GetSaleOrderCommitData(int pintSOCommitMasterID)
		{
			C1PrintPreviewDialogDS objDS = new C1PrintPreviewDialogDS();
			return objDS.GetSaleOrderCommitData(pintSOCommitMasterID);
		}
	
		public DataTable GetSaleOrderInvoiceData(int pintSOInvoiceMasterID)
		{
			C1PrintPreviewDialogDS objDS = new C1PrintPreviewDialogDS();
			return objDS.GetSaleOrderInvoiceData(pintSOInvoiceMasterID);
		}

		#endregion

		#region Orders Summary Report: Tuan TQ		
		
		/// <summary>
		/// Return Order Summary Report
		/// </summary>
		/// <param name="pintCCNID"></param>
		/// <param name="pintYear"></param>
		/// <param name="pstrOtherCondition"></param>
		/// <returns></returns>
		/// <author>Tuan TQ. Nov 21, 2005</author>
	
		public DataTable GetOrderSummaryData(int pintCCNID, int pintYear, string pstrOtherCondition)
		{
			C1PrintPreviewDialogDS objDS = new C1PrintPreviewDialogDS();
			return objDS.GetOrderSummaryData(pintCCNID, pintYear, pstrOtherCondition);
		}
		
		/// <summary>
		/// Get max lead time of a product in a specific category
		/// </summary>
		/// <param name="pintCategoryID"></param>
		/// <param name="pstrListOfProducts"></param>
		/// <returns></returns>
	
		public DataTable GetProductCapacityOfCategory(string pstrCategoryID, string pstrListOfProducts)
		{
			C1PrintPreviewDialogDS objDS = new C1PrintPreviewDialogDS();
			return objDS.GetProductCapacityOfCategory(pstrCategoryID, pstrListOfProducts);
		}

		#endregion

		#region Issuance Slip Report: Tuan TQ
		/// <summary>
		/// Get Issuance Slip Data
		/// </summary>
		/// <param name="pintIssueMasterialMasterId"></param>
		/// <param name="pintMaxRow"></param>
		/// <returns></returns>
	
		public DataTable GetIssuanceSlipData(int pintIssueMasterialMasterId, string pstrMaxRow)
		{
			C1PrintPreviewDialogDS objDS = new C1PrintPreviewDialogDS();
			return objDS.GetIssuanceSlipData(pintIssueMasterialMasterId, pstrMaxRow);
		}

		#endregion

		#region Delivery To Next Stage Slip Report: Tuan TQ
		
		/// <summary>
		/// Get Delivery To Next Stage Slip Data
		/// </summary>
		/// <param name="pintIssueMasterialMasterId"></param>
		/// <param name="pintMaxRow"></param>
		/// <returns></returns>
	
		public DataTable GetDelivery2NextSlipData(int pintIssueMasterialMasterId, string pstrMaxRow)
		{
			C1PrintPreviewDialogDS objDS = new C1PrintPreviewDialogDS();
			return objDS.GetDelivery2NextSlipData(pintIssueMasterialMasterId, pstrMaxRow);
		}

		#endregion

		#region Other Issuance Slip Report: Tuan TQ
	
	
		public DataTable GetOtherIssuanceSlipData(int pintIssueMasterialMasterId, string pstrMaxRow)
		{
			C1PrintPreviewDialogDS objDS = new C1PrintPreviewDialogDS();
			return objDS.GetOtherIssuanceSlipData(pintIssueMasterialMasterId, pstrMaxRow);
		}

		#endregion
		
		#region Delivery To Customer Report: Tuan TQ
	
	
		public DataTable GetDelivery2CustomerData(int pintSaleOrderMasterId)
		{
			C1PrintPreviewDialogDS objDS = new C1PrintPreviewDialogDS();
			return objDS.GetDelivery2CustomerData(pintSaleOrderMasterId);
		}

		#endregion

		#region Return Goods Receipt Slip: Trada
		/// <summary>
		/// GetReturnGoodsReceiptByMasterID
		/// </summary>
		/// <param name="pintReturnGoodsReceiptMasterID"></param>
		/// <returns></returns>
		/// <author>Trada</author>
		/// <date>Monday, Mar 13 2006</date>
	
		public DataTable GetReturnGoodsReceiptByMasterID(int pintReturnGoodsReceiptMasterID)
		{
			C1PrintPreviewDialogDS objDS = new C1PrintPreviewDialogDS();
			return objDS.GetReturnGoodsReceiptByMasterID(pintReturnGoodsReceiptMasterID);
		}
		#endregion

		#region WO Completion Slip Report: Tuan TQ
		
	
		public DataTable GetWOCompletionData(int pintWOCompletionId)
		{
			C1PrintPreviewDialogDS objDS = new C1PrintPreviewDialogDS();
			return objDS.GetWOCompletionData(pintWOCompletionId);
		}

		#endregion
		
		#region BOM Shortage Report: Tuan TQ


        public DataTable GetBOMShortageData(List<string> pintWODetailId, decimal pdecCompletedQty)
		{
			var dsReport = new C1PrintPreviewDialogDS();
			DataTable dtbTemp = dsReport.GetBOMShortageData(pintWODetailId, pdecCompletedQty);

			return dtbTemp;
		}

        public DataTable GetMultiBomShortageData(List<string> pintWODetailId)
        {
            var dsReport = new C1PrintPreviewDialogDS();
            DataTable dtbTemp = dsReport.GetMultiBomShortageData(pintWODetailId);

            return dtbTemp;
        }

		#endregion
		
		#region Delivery To Outsourcing Vendor Report: Tuan TQ
		/// <summary>
		/// Get Delivery To Outsourcing Vendor data
		/// </summary>
		/// <param name="pintWODetailId"></param>
		/// <returns></returns>
		/// <author> Tuan TQ, 29 Dec, 2005</author>
	
		public DataTable GetDelivery2OutsourcingData(int pintIssueMasterialMasterId, string pstrMaxRow)
		{
			C1PrintPreviewDialogDS objDS = new C1PrintPreviewDialogDS();
			return objDS.GetDelivery2OutsourcingData(pintIssueMasterialMasterId, pstrMaxRow);
		}

		#endregion

		#region Destroy Slip Report: Tuan TQ
		/// <summary>
		/// Get Destroy Slip data
		/// </summary>
		/// <param name="pintWODetailId"></param>
		/// <returns></returns>
		/// <author> Tuan TQ, 14 Mar, 2006</author>
	
		public DataTable GetDestroySlipData(int pintIssueMasterialMasterId, string pstrMaxRow)
		{
			C1PrintPreviewDialogDS objDS = new C1PrintPreviewDialogDS();
			return objDS.GetDestroySlipData(pintIssueMasterialMasterId, pstrMaxRow);
		}

		#endregion
		
		#region PO Summary Report: Tuan TQ		
		/// <summary>
		/// Get PO Summary data
		/// </summary>
		/// <param name="pintWODetailId"></param>
		/// <returns></returns>
		/// <author> Tuan TQ, 30 Dec, 2005</author>
	
		public DataTable GetPOSummaryData(int pintPOId, DateTime pdtmOrderDate)
		{
			C1PrintPreviewDialogDS objDS = new C1PrintPreviewDialogDS();
			return objDS.GetPOSummaryData(pintPOId, pdtmOrderDate);
		}

		#endregion
		
		#region PO Invoice Report: Tuan TQ		
		/// <summary>
		/// Get PO Invoice data
		/// </summary>
		/// <param name="pintWODetailId"></param>
		/// <returns></returns>
		/// <author> Tuan TQ, 05 Jan, 2006</author>
	
		public DataTable GetPOInvoiceData(int pintInvoiceMasterId)
		{
			C1PrintPreviewDialogDS objDS = new C1PrintPreviewDialogDS();
			return objDS.GetPOInvoiceData(pintInvoiceMasterId);
		}

		#endregion

		#region PO BOM Shortage Report: Tuan TQ		
		/// <summary>
		/// Get PO Invoice data
		/// </summary>
		/// <returns></returns>
		/// <author> Tuan TQ, 06 Apr, 2006</author>
	
		public DataTable GetPOBOMShortageData(DateTime pdtmPostDate, int pintProductionLineID, string pstrPONo, string pstrProductIDList)
		{
			C1PrintPreviewDialogDS dsReport = new C1PrintPreviewDialogDS();
			return dsReport.GetPOBOMShortageData(pdtmPostDate, pintProductionLineID, pstrPONo, pstrProductIDList);
		}

		#endregion

		#region In-Out Stock Report: Tuan TQ		
		/// <summary>
		/// Get In-Out Stock data
		/// </summary>
		/// <returns></returns>
		/// <author> Tuan TQ, 16 Jan, 2006</author>
	
		public DataTable GetInOutStockData(string pstrCCNID, 
			string pstrMasterLocID, 
			string pstrLocationID, 
			string pstrBinType, 
			string pstrBinID, 
			string pstrFromDate, 
			string pstrToDate, 
			string pstrCategoryID, 
			string pstrProductSourceID, 
			string pstrModel,
			string pstrProductID)
		{
			C1PrintPreviewDialogDS objDS = new C1PrintPreviewDialogDS();
			return objDS.GetInOutStockData(pstrCCNID, pstrMasterLocID, pstrLocationID, pstrBinType, 
											pstrBinID, pstrFromDate, pstrToDate, pstrCategoryID, pstrProductSourceID, 
											pstrModel, pstrProductID);
		}

		#endregion

		#region In-Out Stock Report: Tuan TQ		
		/// <summary>
		/// Get In-Out Stock data
		/// </summary>
		/// <returns></returns>
		/// <author> Tuan TQ, 16 Jan, 2006</author>
	
		public DataTable GetInOutStockData(string pstrCCNID, 
			string pstrMasterLocID, 
			string pstrLocationID, 
			string pstrBinType, 
			string pstrBinID, 
			DateTime pdtmFromDate, 
			DateTime pdtmToDate, 
			string pstrCategoryID, 
			string pstrProductSourceID, 
			string pstrModel,
			string pstrProductID)
		{
			C1PrintPreviewDialogDS objDS = new C1PrintPreviewDialogDS();
			return objDS.GetInOutStockData(pstrCCNID, pstrMasterLocID, pstrLocationID, pstrBinType, 
				pstrBinID, pdtmFromDate, pdtmToDate, pstrCategoryID, pstrProductSourceID, 
				pstrModel, pstrProductID);
		}

		#endregion

		#region Destroy Material Report: Tuan TQ
	
		public DataTable GetDestroyMaterialData(string pstrCCNID, string pstrYear, string pstrMonth, string pstrListDepartmentID, string pstrListProductionLineID, string pstrListCategoryID, string pstrListProductID)
		{
			C1PrintPreviewDialogDS dsPrint = new C1PrintPreviewDialogDS();
			return dsPrint.GetDestroyMaterialData(pstrCCNID, pstrYear, pstrMonth, pstrListDepartmentID, pstrListProductionLineID, pstrListCategoryID, pstrListProductID);			
		}

		#endregion
		
		#region Item Cost By Month: Tuan TQ

	
		public DataTable GetItemCostByMonthData(string pstrCCNID, string pstrYear, string pstrListDepartmentID, string pstrListProductionLineID, string pstrListCategoryID, string pstrListProductID)
		{
			C1PrintPreviewDialogDS objDS = new C1PrintPreviewDialogDS();
			return objDS.GetItemCostByMonthData(pstrCCNID, pstrYear, pstrListDepartmentID, 
						 pstrListProductionLineID, pstrListCategoryID, pstrListProductID);
		}

		#endregion

		#region Item Cost Detailed By Element: Tuan TQ

	
		public DataTable GetItemCostDetailedByElementData(string pstrCCNID, string pstrYear, string pstrMonth, string pstrListDepartmentID, string pstrListProductionLineID, string pstrListCategoryID, string pstrListProductID, int pintMakeItem)
		{
			C1PrintPreviewDialogDS objDS = new C1PrintPreviewDialogDS();
			return objDS.GetItemCostDetailedByElementData(pstrCCNID, pstrYear, pstrMonth, pstrListDepartmentID, 
				pstrListProductionLineID, pstrListCategoryID, pstrListProductID, pintMakeItem);
		}

		#endregion

		#region Item Cost Detailed By Element (By Production Line): Tuan TQ

	
		public DataTable GetItemCostDetailedByElementAndProductionLineData(string pstrCCNID, string pstrYear, string pstrMonth, string pstrListDepartmentID, string pstrListProductionLineID, string pstrListCategoryID, string pstrListProductID, int pintMakeItem)
		{
			C1PrintPreviewDialogDS objDS = new C1PrintPreviewDialogDS();
			return objDS.GetItemCostDetailedByElementAndProductionLineData(pstrCCNID, pstrYear, pstrMonth, pstrListDepartmentID, 
				pstrListProductionLineID, pstrListCategoryID, pstrListProductID, pintMakeItem);
		}

		#endregion
		
		#region Get Salvaging Material Data: Tuan TQ
	
		public DataTable GetSalvagingMaterialData(string pstrCCNID, string pstrYear, string pstrMonth, string pstrListDepartmentID, string pstrListProductionLineID, string pstrListCategoryID, string pstrListProductID)
		{
			C1PrintPreviewDialogDS objDS = new C1PrintPreviewDialogDS();
			return objDS.GetSalvagingMaterialData(pstrCCNID, pstrYear, pstrMonth, pstrListDepartmentID, 
				pstrListProductionLineID, pstrListCategoryID, pstrListProductID);
		}

		#endregion

		#region Detailed Item Cost By Month: Tuan TQ

	
		public DataTable GetDetailedItemCostByMonthData(string pstrCCNID, string pstrYear, string pstrListDepartmentID, string pstrListProductionLineID, string pstrListCategoryID, string pstrListProductID)
		{
			C1PrintPreviewDialogDS objDS = new C1PrintPreviewDialogDS();
			return objDS.GetDetailedItemCostByMonthData(pstrCCNID, pstrYear, pstrListDepartmentID, 
				pstrListProductionLineID, pstrListCategoryID, pstrListProductID);
		}

		#endregion

		#region PO Slip Data: Tuan TQ
	
		public DataTable GetPOSlipData(int pintPOReceiptMasterID)
		{
			C1PrintPreviewDialogDS dsReport = new C1PrintPreviewDialogDS();
			return dsReport.GetPOSlipData(pintPOReceiptMasterID);
		}
		#endregion				

		#region PO Slip Data: CanhNv
	
		public DataTable GetPOSlipDatavedor(int pintPOReceiptMasterID)
		{
			C1PrintPreviewDialogDS dsReport = new C1PrintPreviewDialogDS();
			return dsReport.GetPOSlipDatavedor(pintPOReceiptMasterID);
		}
		#endregion
		#region SO Shipping Data - Tuan TQ

		/// <summary>
		/// Get SO Shipping Detail for Importing Invoice Report 
		/// </summary>
		/// <param name="pintShippingMasterID"></param>
		/// <returns></returns>
		/// <author> Tuan TQ</author>
		/// <Created Date> 01 June, 2006</Created>
	
		public DataTable GetSOShippingDetailData4ImportInvoice(int pintShippingMasterID)
		{
			C1PrintPreviewDialogDS dsReport = new C1PrintPreviewDialogDS();
			return dsReport.GetSOShippingDetailData4ImportInvoice(pintShippingMasterID);
		}
	
		public DataTable GetSOInvoiceDetailData4ImportInvoice(int pintSOInvoiceMasterID)
		{
			C1PrintPreviewDialogDS dsReport = new C1PrintPreviewDialogDS();
			return dsReport.GetSOInvoiceDetailData4ImportInvoice(pintSOInvoiceMasterID);
		}

		#endregion SO Invoice Data

		#region Inventory Adjustment Slip Data: Tuan TQ
	
		public DataTable GetInventoryAdjustmentData(int pintMasterID)
		{
			C1PrintPreviewDialogDS dsReport = new C1PrintPreviewDialogDS();
			return dsReport.GetInventoryAdjustmentData(pintMasterID);
		}
		#endregion				

		#region Debit Note Data: Tuan TQ
	
		public DataTable GetDebitNoteData(int pintMasterID)
		{
			C1PrintPreviewDialogDS dsReport = new C1PrintPreviewDialogDS();
			return dsReport.GetDebitNoteData(pintMasterID);
		}
		#endregion		
		
		#region Credit Note Data: Trada

	
		public DataTable GetCreditNoteData(int pintMasterID)
		{
			C1PrintPreviewDialogDS dsReport = new C1PrintPreviewDialogDS();
			return dsReport.GetCreditNoteData(pintMasterID);
		}
		#endregion		

		#region CUSTOMS LIST REPORT. THACHNN

		/// <summary>
		/// Thachnn : 08/Jun/2006		
		/// </summary>		
	
		public DataSet GetCustomsListReportData(int pnCCNID, int pnInvoiceMasterID)
		{
			C1PrintPreviewDialogDS dsReport = new C1PrintPreviewDialogDS();
			return dsReport.GetCustomsListReportData(pnCCNID,pnInvoiceMasterID);
		}

		#endregion		


		#region BUSINESS FUNCTION FOR PRODUCTION LINE PLANNING REPORT. THACHNN


		/// <summary>
		/// Return array list of int, show that which day in provided month is off day (of current Production Line )
		/// </summary>
		/// <param name="pstrCCNID"></param>
		/// <param name="pstrYear"></param>
		/// <param name="pstrMonth"></param>
		/// <param name="pstrProductionLineID"></param>
		/// <returns></returns>
	
		public System.Collections.ArrayList GetOffDayOfProductionLineInMonth(string pstrCCNID, string pstrYear, string pstrMonth, string pstrProductionLineID)
		{
			System.Collections.ArrayList arrRet = new System.Collections.ArrayList();			
			DataTable pdtbWorkingTime  = GetAllPeriodOfWorkingTime(pstrCCNID,pstrYear,pstrMonth,pstrProductionLineID);			

			const string BEGINDATE = "BeginDate";
			const string ENDDATE = "EndDate";
			
			for(int iDayLoop = 1 ; iDayLoop <=  DateTime.DaysInMonth(int.Parse(pstrYear), int.Parse(pstrMonth) )  ; iDayLoop++)
			{
				DateTime pdtmNeedToResolve = new DateTime(int.Parse(pstrYear), int.Parse(pstrMonth), iDayLoop );
				
				bool blnFoundCurrentDayLoopInCapacityRange = false;
				foreach(DataRow drow in pdtbWorkingTime.Rows)
				{
					if(blnFoundCurrentDayLoopInCapacityRange )	// found in previous loop of each DataRow (capacity period)
					{
						break; // break the capacity period loop
					}
					DateTime dtmBeginDate = (DateTime)drow[BEGINDATE];
					DateTime dtmEndDate = (DateTime)drow[ENDDATE];
					if(dtmBeginDate <= pdtmNeedToResolve && pdtmNeedToResolve <= dtmEndDate) // in range, mean current Day is working day of this production line
					{
						arrRet.Add(iDayLoop);
						blnFoundCurrentDayLoopInCapacityRange = true;
					}					
				}		// for each capacity period
			}	// for each day in month			

			// arrRet contain WorkInngDay, so we will find the not working day of a month
			System.Collections.ArrayList arrResult = new System.Collections.ArrayList();	// real result
			for(int i = 1 ; i <=  DateTime.DaysInMonth(int.Parse(pstrYear), int.Parse(pstrMonth) )  ; i++)
			{
				if( ! arrRet.Contains(i) )
				{
					arrResult.Add(i);
				}
			}

			return arrResult;			
		}

		/// <summary>
		/// NOTE : GET WORKING TIME OF MAIN WORK CENTER ONLY
		/// 
		/// get the reference table for GetRealWorkingDay() function
		/// result is the table with each record contain: 
		/// BeginDate, EndDate (of configured WCCapacity)
		/// WorkTimeFrom, WorkTimeTo	(Real working time of each shift in a working day)
		/// 
		/// SCHEMA: BeginDate, EndDate, WorkTimeFrom, WorkTimeTo
		/// 
		/// </summary>
		/// <author>Thachnn</author>
		/// <param name="pstrCCNID"></param>
		/// <param name="pstrYear"></param>
		/// <param name="pstrMonth"></param>
		/// <param name="pstrProductionLineID"></param>
		/// <returns></returns>
	
		public  DataTable GetAllPeriodOfWorkingTime(string pstrCCNID, string pstrYear, string pstrMonth, string pstrProductionLineID)
		{
			C1PrintPreviewDialogDS dsReport = new C1PrintPreviewDialogDS();
			return dsReport.GetAllPeriodOfWorkingTime(pstrCCNID,pstrYear, pstrMonth, pstrProductionLineID);
		}

		#endregion BUSINESS FUNCTION FOR PRODUCTION LINE PLANNING REPORT. THACHNN

		#region IObjectBO Members

		public void Add(object pObjectDetail)
		{
			// TODO:  Add C1PrintPreviewDialogBO.Add implementation
		}

		public void Delete(object pObjectVO)
		{
			// TODO:  Add C1PrintPreviewDialogBO.Delete implementation
		}

		public object GetObjectVO(int pintID, string VOclass)
		{
			// TODO:  Add C1PrintPreviewDialogBO.GetObjectVO implementation
			return null;
		}

		public void UpdateDataSet(DataSet dstData)
		{
			// TODO:  Add C1PrintPreviewDialogBO.UpdateDataSet implementation
		}

		public void Update(object pObjectDetail)
		{
			// TODO:  Add C1PrintPreviewDialogBO.Update implementation
		}

		#endregion
	}
}