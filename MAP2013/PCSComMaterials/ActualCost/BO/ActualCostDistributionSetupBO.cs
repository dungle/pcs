using System;
using System.Data;


using System.Collections;

//Using PCS's Namespaces

using PCSComUtils.Common;
using PCSComUtils.MasterSetup.DS;
using PCSComUtils.PCSExc;
using PCSComMaterials.ActualCost.DS;

namespace PCSComMaterials.ActualCost.BO
{
	public interface IActualCostDistributionSetupBO
	{
		void Add(object pobjMasterVO, System.Data.DataSet pdstDataSet);
		void Delete(int pintMasterID);
		object GetObjectVO(int pintID);
		object GetObjectVO(string pstrCode);
		bool IsPeriodOverlap(object pobjMasterVO);
		void Allocate(object pobjObject, DataTable pdtbDetail);
		
	}
	/// <summary>
	/// Summary description for ActualCostDistributionSetupBO.
	/// </summary>

	
	
	public class ActualCostDistributionSetupBO : IActualCostDistributionSetupBO
	{
		public ActualCostDistributionSetupBO()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		

		#region Cost Allocating
		
		private const string COMPLETED_QUANTITY_FLD = "CompletedQuantity";
		private const string QUANTITY_LEADTIME_FLD = "LeadTime";
		private const int ROUND_DIGIT_NUMBER = 4;

		/// <summary>
		/// Allocate cost for selected period
		/// </summary>
		/// <param name="pobjObject"></param>
		/// <param name="pdtbDetail"></param>
	
		public void Allocate(object pobjObject, DataTable pdtbDetail)
		{
			CST_AllocationResultDS dsAllocationResult = new CST_AllocationResultDS();
			CST_ActCostAllocationMasterVO voAllocationMaster = (CST_ActCostAllocationMasterVO)pobjObject;
			
			//Delete old data first
			dsAllocationResult.DeleteByAllocationMasterID(voAllocationMaster.ActCostAllocationMasterID);

			//copy table
			DataTable dtbSource = pdtbDetail.Copy();			
			
			//Table contain max lead time
			DataTable dtbLeadTime;
			dtbLeadTime = dsAllocationResult.GetLeadTimeData(voAllocationMaster.FromDate, voAllocationMaster.ToDate, voAllocationMaster.CCNID);
			
			//Table contain completed quantity by production line
			DataTable dtbCompletedByProductionLine;
			dtbCompletedByProductionLine = dsAllocationResult.GetCompletedQuantityByProductionLine(voAllocationMaster.FromDate, voAllocationMaster.ToDate, voAllocationMaster.CCNID);

			//Table contain completed quantity by product
			DataTable dtbCompletedByProductGroup;
			dtbCompletedByProductGroup = dsAllocationResult.GetCompletedQuantityByProductGroup(voAllocationMaster.FromDate, voAllocationMaster.ToDate, voAllocationMaster.CCNID);

			#region Temp table to keep completed quantity, leadtime of product
			DataTable dtbTemp = new DataTable();

			dtbTemp.Columns.Add(CST_AllocationResultTable.PRODUCTID_FLD, typeof(System.Int32));
			dtbTemp.Columns.Add(QUANTITY_LEADTIME_FLD, typeof(System.Decimal));
			dtbTemp.Columns.Add(COMPLETED_QUANTITY_FLD, typeof(System.Decimal));
			
			#endregion

			//Build template of AllocationResult table
			DataTable dtbAllocationResult = BuildAllocationResultTemplate();			

			//Loop each row and insert data
			foreach(DataRow drowSource in dtbSource.Rows)
			{
				//irnoge deleted row
				if(drowSource.RowState == DataRowState.Deleted)	continue;				
				
				//Processing if product is not null
				if(!drowSource[CST_ActCostAllocationDetailTable.PRODUCTID_FLD].Equals(DBNull.Value)
					&& drowSource[CST_ActCostAllocationDetailTable.PRODUCTID_FLD].ToString().Trim() != string.Empty)
				{
					//Allocate cost by product
					AllocateByProduct(drowSource, dtbCompletedByProductGroup, dtbAllocationResult);
					
					//move to next row
					continue;
				}
				
				//Processing if product group is not null
				if(!drowSource[CST_ActCostAllocationDetailTable.PRODUCTGROUPID_FLD].Equals(DBNull.Value)
				 && drowSource[CST_ActCostAllocationDetailTable.PRODUCTGROUPID_FLD].ToString().Trim() != string.Empty)
				{					
					//Allocate cost by product group
					AllocateByGroup(drowSource, dtbCompletedByProductGroup,dtbLeadTime, dtbTemp, dtbAllocationResult);
					
					//move to next row
					continue;
				}
				
				//Processing if production line is not null				
				if(!drowSource[CST_ActCostAllocationDetailTable.PRODUCTIONLINEID_FLD].Equals(DBNull.Value)
				 && drowSource[CST_ActCostAllocationDetailTable.PRODUCTIONLINEID_FLD].ToString().Trim() != string.Empty)
				{					
					//Allocate cost by production line
					AllocateByProductionLine(drowSource, dtbCompletedByProductionLine, dtbLeadTime, dtbTemp, dtbAllocationResult);
					
					//move to next row
					continue;
				}
				
				//Processing if deparment is not null
				if(!drowSource[CST_ActCostAllocationDetailTable.DEPARTMENTID_FLD].Equals(DBNull.Value)
					&& drowSource[CST_ActCostAllocationDetailTable.DEPARTMENTID_FLD].ToString().Trim() != string.Empty)
				{
					//Allocate cost by department
					AllocateByDepartment(drowSource, dtbCompletedByProductionLine, dtbLeadTime, dtbTemp, dtbAllocationResult);

					//move to next row
					continue;
				}
				
				//Finally, allocate cost by CCN
				AllocateByCCN(voAllocationMaster.CCNID, drowSource, dtbCompletedByProductionLine, dtbLeadTime, dtbTemp, dtbAllocationResult);

			}//foreach

			//Build dataset for allocation result
			DataSet dtsAllocation = dtbAllocationResult.DataSet;
			if(dtsAllocation == null)
			{
				dtsAllocation = new DataSet();
				dtsAllocation.Tables.Add(dtbAllocationResult);
			}

			//update dataset
			dsAllocationResult.UpdateDataSet(dtsAllocation);
			dsAllocationResult.InsertNewAllocation(voAllocationMaster.ActCostAllocationMasterID, voAllocationMaster.FromDate, voAllocationMaster.ToDate);
		}
		
	
		private DataTable BuildAllocationResultTemplate()
		{
			DataTable dtbTemplate = new DataTable(CST_AllocationResultTable.TABLE_NAME);

			dtbTemplate.Columns.Add(CST_AllocationResultTable.ACTCOSTALLOCATIONMASTERID_FLD, typeof(System.Int32));
			dtbTemplate.Columns.Add(CST_AllocationResultTable.ALLOCATIONRESULTID_FLD, typeof(System.Int32));
			dtbTemplate.Columns.Add(CST_AllocationResultTable.RATE_FLD, typeof(System.Decimal));
			dtbTemplate.Columns.Add(CST_AllocationResultTable.AMOUNT_FLD, typeof(System.Decimal));
			dtbTemplate.Columns.Add(CST_AllocationResultTable.COMPLETEDQUANTITY_FLD, typeof(System.Decimal));
			dtbTemplate.Columns.Add(CST_AllocationResultTable.COSTELEMENTID_FLD, typeof(System.Int32));
			dtbTemplate.Columns.Add(CST_AllocationResultTable.DEPARTMENTID_FLD, typeof(System.Int32));
			dtbTemplate.Columns.Add(CST_AllocationResultTable.PRODUCTGROUPID_FLD, typeof(System.Int32));
			dtbTemplate.Columns.Add(CST_AllocationResultTable.PRODUCTID_FLD, typeof(System.Int32));
			dtbTemplate.Columns.Add(CST_AllocationResultTable.PRODUCTIONLINEID_FLD, typeof(System.Int32));

			return dtbTemplate;
		}
		
		
		/// <summary>
		/// Insert directly with rate is 100 percent
		/// </summary>
		/// <param name="pdrowSource"></param>
		/// <param name="pdtbCompletedByProductGroup"></param>
		/// <param name="pdtbAllocationResult"></param>		
		private void AllocateByProduct(DataRow pdrowSource, DataTable pdtbCompletedByProductGroup, 
			DataTable pdtbAllocationResult)
		{
			decimal decCompletedQty = decimal.Zero;

			//select completed quantity from completed quantity table 
			string strFilter = CST_ActCostAllocationDetailTable.PRODUCTIONLINEID_FLD + "=" + pdrowSource[CST_ActCostAllocationDetailTable.PRODUCTIONLINEID_FLD].ToString();
			strFilter += " AND " + CST_ActCostAllocationDetailTable.PRODUCTGROUPID_FLD + "=" + pdrowSource[CST_ActCostAllocationDetailTable.PRODUCTGROUPID_FLD].ToString();
			strFilter += " AND " + CST_ActCostAllocationDetailTable.PRODUCTID_FLD + "=" + pdrowSource[CST_ActCostAllocationDetailTable.PRODUCTID_FLD].ToString();

			DataRow[] arrSameProduct = pdtbCompletedByProductGroup.Select(strFilter);
			foreach(DataRow drowSameProduct in arrSameProduct)
			{				
				if(!drowSameProduct[COMPLETED_QUANTITY_FLD].Equals(DBNull.Value))
				{
					decCompletedQty += decimal.Parse(drowSameProduct[COMPLETED_QUANTITY_FLD].ToString());
				}
			}
			
			if (decCompletedQty == 0)
				return;
			//create new allocation result row
			DataRow dnewRow = pdtbAllocationResult.NewRow();

			//assign columns's value
			dnewRow[CST_AllocationResultTable.RATE_FLD] = 100;			
			dnewRow[CST_AllocationResultTable.COMPLETEDQUANTITY_FLD] = decCompletedQty;					

			//other value get directly from source
			dnewRow[CST_AllocationResultTable.AMOUNT_FLD]			= pdrowSource[CST_ActCostAllocationDetailTable.ALLOCATIONAMOUNT_FLD];
			dnewRow[CST_AllocationResultTable.COSTELEMENTID_FLD]	= pdrowSource[CST_ActCostAllocationDetailTable.COSTELEMENTID_FLD];
			dnewRow[CST_AllocationResultTable.DEPARTMENTID_FLD]		= pdrowSource[CST_ActCostAllocationDetailTable.DEPARTMENTID_FLD];
			dnewRow[CST_AllocationResultTable.PRODUCTGROUPID_FLD]	= pdrowSource[CST_ActCostAllocationDetailTable.PRODUCTGROUPID_FLD];
			dnewRow[CST_AllocationResultTable.PRODUCTID_FLD]		= pdrowSource[CST_ActCostAllocationDetailTable.PRODUCTID_FLD];
			dnewRow[CST_AllocationResultTable.PRODUCTIONLINEID_FLD] = pdrowSource[CST_ActCostAllocationDetailTable.PRODUCTIONLINEID_FLD];
			dnewRow[CST_AllocationResultTable.ACTCOSTALLOCATIONMASTERID_FLD] = pdrowSource[CST_ActCostAllocationDetailTable.ACTCOSTALLOCATIONMASTERID_FLD];
			
			//add created row to AllocationResult table
			pdtbAllocationResult.Rows.Add(dnewRow);			
		}
		
		
		/// <summary>
		/// Get all completed product of by group then allocate
		/// </summary>
		/// <param name="pdrowSource"></param>
		/// <param name="pdtbCompletedByProductGroup"></param>
		/// <param name="pdtbLeadTime"></param>
		/// <param name="pdtbTemp"></param>
		/// <param name="pdtbAllocationResult"></param>		
		private void AllocateByGroup(DataRow pdrowSource, DataTable pdtbCompletedByProductGroup, 
			DataTable pdtbLeadTime, DataTable pdtbTemp, DataTable pdtbAllocationResult)
		{
			ArrayList arlProduct = new ArrayList();
			DataRow[] arrLeadTime;

			decimal decTotalValue = decimal.Zero;			
			decimal decRate = decimal.Zero;

			string strFilter = CST_ActCostAllocationDetailTable.PRODUCTIONLINEID_FLD + "=" + pdrowSource[CST_ActCostAllocationDetailTable.PRODUCTIONLINEID_FLD].ToString();
			strFilter += " AND " + CST_ActCostAllocationDetailTable.PRODUCTGROUPID_FLD + "=" + pdrowSource[CST_ActCostAllocationDetailTable.PRODUCTGROUPID_FLD].ToString();
			
			//Get all completed product of by group
			DataRow[] arrSameGroupRows = pdtbCompletedByProductGroup.Select(strFilter);

			//Clear temp table
			pdtbTemp.Rows.Clear();

			//Loop through product
			foreach(DataRow drowSameGroup in arrSameGroupRows)
			{
				//Move to next row if it has been processed
				if(arlProduct.Contains(drowSameGroup[CST_AllocationResultTable.PRODUCTID_FLD]))
				{
					continue;
				}

				arlProduct.Add(drowSameGroup[CST_AllocationResultTable.PRODUCTID_FLD]);				
				//
				strFilter = CST_ActCostAllocationDetailTable.PRODUCTIONLINEID_FLD + "=" + pdrowSource[CST_ActCostAllocationDetailTable.PRODUCTIONLINEID_FLD].ToString();
				strFilter += " AND " + CST_ActCostAllocationDetailTable.PRODUCTGROUPID_FLD + "=" + pdrowSource[CST_ActCostAllocationDetailTable.PRODUCTGROUPID_FLD].ToString();
				strFilter += " AND " + CST_AllocationResultTable.PRODUCTID_FLD + "=" + drowSameGroup[CST_AllocationResultTable.PRODUCTID_FLD].ToString();
				
				DataRow[] arrSameProduct = pdtbCompletedByProductGroup.Select(strFilter);				
				
				decimal decTotalQuantity = decimal.Zero;
				decimal decTotalQuantityLeadTime = decimal.Zero;

				foreach(DataRow drowSameProduct in arrSameProduct)
				{
					decimal decLeadTime = decimal.Zero;
					
					//Get lead time by Work Order Detail
					strFilter = PRO_WorkOrderCompletionTable.WORKORDERDETAILID_FLD + "=" + drowSameProduct[PRO_WorkOrderCompletionTable.WORKORDERDETAILID_FLD].ToString();
					arrLeadTime = pdtbLeadTime.Select(strFilter);

					if(arrLeadTime.Length > 0)
					{ 
						if(!arrLeadTime[0][QUANTITY_LEADTIME_FLD].Equals(DBNull.Value))
						{							
							decLeadTime = decimal.Parse(arrLeadTime[0][QUANTITY_LEADTIME_FLD].ToString());
						}
					}
					
					//sum total leadtime * quantity and total quantity of current product
					decTotalQuantityLeadTime += (decLeadTime * decimal.Parse(drowSameProduct[COMPLETED_QUANTITY_FLD].ToString()));				
					decTotalQuantity += decimal.Parse(drowSameProduct[COMPLETED_QUANTITY_FLD].ToString());
				}
				
				//Total value of all products
				decTotalValue += decTotalQuantityLeadTime;

				//create temp row to store information
				DataRow drowNewTemp = pdtbTemp.NewRow();

				drowNewTemp[CST_ActCostAllocationDetailTable.PRODUCTID_FLD] = drowSameGroup[CST_ActCostAllocationDetailTable.PRODUCTID_FLD];
				drowNewTemp[COMPLETED_QUANTITY_FLD] = decTotalQuantity;
				drowNewTemp[QUANTITY_LEADTIME_FLD] = decTotalQuantityLeadTime;

				pdtbTemp.Rows.Add(drowNewTemp);
			}
					
			//reset decTotalValue value to avoid division by 0 exception
			if(decTotalValue == 0) decTotalValue = 1;

			//loop from temp row and add to result
			foreach(DataRow drowTemp in pdtbTemp.Rows)
			{						
				//create new allocation result row
				DataRow dnewRow = pdtbAllocationResult.NewRow();
				decRate = decimal.Parse(drowTemp[QUANTITY_LEADTIME_FLD].ToString())/decTotalValue;

				//assign columns's value
				dnewRow[CST_AllocationResultTable.RATE_FLD] = decimal.Round(decRate * 100, ROUND_DIGIT_NUMBER);

				dnewRow[CST_AllocationResultTable.AMOUNT_FLD] = decimal.Round(decRate * decimal.Parse(pdrowSource[CST_ActCostAllocationDetailTable.ALLOCATIONAMOUNT_FLD].ToString()), ROUND_DIGIT_NUMBER);
				dnewRow[CST_AllocationResultTable.COMPLETEDQUANTITY_FLD] = drowTemp[COMPLETED_QUANTITY_FLD];					

				//other value get directly from source
				dnewRow[CST_AllocationResultTable.PRODUCTID_FLD]		= drowTemp[CST_ActCostAllocationDetailTable.PRODUCTID_FLD];
				dnewRow[CST_AllocationResultTable.COSTELEMENTID_FLD]	= pdrowSource[CST_ActCostAllocationDetailTable.COSTELEMENTID_FLD];
				dnewRow[CST_AllocationResultTable.DEPARTMENTID_FLD]		= pdrowSource[CST_ActCostAllocationDetailTable.DEPARTMENTID_FLD];
				dnewRow[CST_AllocationResultTable.PRODUCTGROUPID_FLD]	= pdrowSource[CST_ActCostAllocationDetailTable.PRODUCTGROUPID_FLD];						
				dnewRow[CST_AllocationResultTable.PRODUCTIONLINEID_FLD] = pdrowSource[CST_ActCostAllocationDetailTable.PRODUCTIONLINEID_FLD];				
				dnewRow[CST_AllocationResultTable.ACTCOSTALLOCATIONMASTERID_FLD] = pdrowSource[CST_ActCostAllocationDetailTable.ACTCOSTALLOCATIONMASTERID_FLD];

				//add created row to AllocationResult table
				pdtbAllocationResult.Rows.Add(dnewRow);
			}
		}
		
		
		/// <summary>
		/// Get all completed product of by production line then allocate
		/// </summary>
		/// <param name="pdrowSource"></param>
		/// <param name="pdtbCompletedByProductionLine"></param>
		/// <param name="pdtbLeadTime"></param>
		/// <param name="pdtbTemp"></param>
		/// <param name="pdtbAllocationResult"></param>		
		private void AllocateByProductionLine(DataRow pdrowSource, DataTable pdtbCompletedByProductionLine, 
			DataTable pdtbLeadTime, DataTable pdtbTemp, DataTable pdtbAllocationResult)
		{
			ArrayList arlProduct = new ArrayList();
			DataRow[] arrLeadTime;

			decimal decTotalValue = decimal.Zero;			
			decimal decRate = decimal.Zero;

			string strFilter = CST_ActCostAllocationDetailTable.PRODUCTIONLINEID_FLD + "=" + pdrowSource[CST_ActCostAllocationDetailTable.PRODUCTIONLINEID_FLD].ToString();
			strFilter += " AND " + CST_ActCostAllocationDetailTable.DEPARTMENTID_FLD + "=" + pdrowSource[CST_ActCostAllocationDetailTable.DEPARTMENTID_FLD].ToString();
			
			//Get all completed product of by production line
			DataRow[] arrSameProductionLine = pdtbCompletedByProductionLine.Select(strFilter);

			//clear temp table
			pdtbTemp.Rows.Clear();
					
			//Loop through product
			foreach(DataRow drowSameProductionLine in arrSameProductionLine)
			{
				//Move to next row if it has been processed
				if(arlProduct.Contains(drowSameProductionLine[CST_AllocationResultTable.PRODUCTID_FLD]))
				{
					continue;
				}

				arlProduct.Add(drowSameProductionLine[CST_AllocationResultTable.PRODUCTID_FLD]);
				
				//
				strFilter = CST_ActCostAllocationDetailTable.PRODUCTIONLINEID_FLD + "=" + pdrowSource[CST_ActCostAllocationDetailTable.PRODUCTIONLINEID_FLD].ToString();
				strFilter += " AND " + CST_ActCostAllocationDetailTable.DEPARTMENTID_FLD + "=" + pdrowSource[CST_ActCostAllocationDetailTable.DEPARTMENTID_FLD].ToString();
				strFilter += " AND " + CST_AllocationResultTable.PRODUCTID_FLD + "=" + drowSameProductionLine[CST_AllocationResultTable.PRODUCTID_FLD].ToString();
				
				DataRow[] arrSameProduct = pdtbCompletedByProductionLine.Select(strFilter);				
				
				decimal decTotalQuantity = decimal.Zero;
				decimal decTotalQuantityLeadTime = decimal.Zero;

				foreach(DataRow drowSameProduct in arrSameProduct)
				{
					decimal decLeadTime = decimal.Zero;
					
					//Get lead time by Work Order Detail
					strFilter = PRO_WorkOrderCompletionTable.WORKORDERDETAILID_FLD + "=" + drowSameProduct[PRO_WorkOrderCompletionTable.WORKORDERDETAILID_FLD].ToString();
					arrLeadTime = pdtbLeadTime.Select(strFilter);

					if(arrLeadTime.Length > 0)
					{ 
						if(!arrLeadTime[0][QUANTITY_LEADTIME_FLD].Equals(DBNull.Value))
						{							
							decLeadTime = decimal.Parse(arrLeadTime[0][QUANTITY_LEADTIME_FLD].ToString());
						}
					}
					
					//sum total leadtime * quantity and total quantity of current product
					decTotalQuantityLeadTime += (decLeadTime * decimal.Parse(drowSameProduct[COMPLETED_QUANTITY_FLD].ToString()));					
					decTotalQuantity += decimal.Parse(drowSameProduct[COMPLETED_QUANTITY_FLD].ToString());
				}

				//Total value of all products
				decTotalValue += decTotalQuantityLeadTime;

				//create temp row to store information
				DataRow drowNewTemp = pdtbTemp.NewRow();

				drowNewTemp[CST_ActCostAllocationDetailTable.PRODUCTID_FLD] = drowSameProductionLine[CST_ActCostAllocationDetailTable.PRODUCTID_FLD];
				drowNewTemp[COMPLETED_QUANTITY_FLD] = decTotalQuantity;
				drowNewTemp[QUANTITY_LEADTIME_FLD] = decTotalQuantityLeadTime;

				pdtbTemp.Rows.Add(drowNewTemp);
			}
					
			//reset decTotalValue value to avoid division by 0 exception
			if(decTotalValue == 0) decTotalValue = 1;

			//loop from temp row and add to result
			foreach(DataRow drowTemp in pdtbTemp.Rows)
			{						
				//create new allocation result row
				DataRow dnewRow = pdtbAllocationResult.NewRow();
				decRate = decimal.Parse(drowTemp[QUANTITY_LEADTIME_FLD].ToString())/ decTotalValue;

				//assign columns's value
				dnewRow[CST_AllocationResultTable.RATE_FLD] = decimal.Round(decRate * 100, ROUND_DIGIT_NUMBER);

				dnewRow[CST_AllocationResultTable.AMOUNT_FLD] = decimal.Round(decRate * decimal.Parse(pdrowSource[CST_ActCostAllocationDetailTable.ALLOCATIONAMOUNT_FLD].ToString()), ROUND_DIGIT_NUMBER);
				dnewRow[CST_AllocationResultTable.COMPLETEDQUANTITY_FLD] = drowTemp[COMPLETED_QUANTITY_FLD];					

				//other value get directly from source
				dnewRow[CST_AllocationResultTable.PRODUCTID_FLD]		= drowTemp[CST_ActCostAllocationDetailTable.PRODUCTID_FLD];
				dnewRow[CST_AllocationResultTable.COSTELEMENTID_FLD]	= pdrowSource[CST_ActCostAllocationDetailTable.COSTELEMENTID_FLD];
				dnewRow[CST_AllocationResultTable.DEPARTMENTID_FLD]		= pdrowSource[CST_ActCostAllocationDetailTable.DEPARTMENTID_FLD];
				dnewRow[CST_AllocationResultTable.PRODUCTGROUPID_FLD]	= pdrowSource[CST_ActCostAllocationDetailTable.PRODUCTGROUPID_FLD];						
				dnewRow[CST_AllocationResultTable.PRODUCTIONLINEID_FLD] = pdrowSource[CST_ActCostAllocationDetailTable.PRODUCTIONLINEID_FLD];				
				dnewRow[CST_AllocationResultTable.ACTCOSTALLOCATIONMASTERID_FLD] = pdrowSource[CST_ActCostAllocationDetailTable.ACTCOSTALLOCATIONMASTERID_FLD];

				//add created row to AllocationResult table
				pdtbAllocationResult.Rows.Add(dnewRow);
			}			
		}
		
		
		/// <summary>
		/// Get all completed product of by department then allocate
		/// </summary>
		/// <param name="pdrowSource"></param>
		/// <param name="pdtbCompletedByProductionLine"></param>
		/// <param name="pdtbLeadTime"></param>
		/// <param name="pdtbTemp"></param>
		/// <param name="pdtbAllocationResult"></param>		
		private void AllocateByDepartment(DataRow pdrowSource, DataTable pdtbCompletedByProductionLine, 
			DataTable pdtbLeadTime, DataTable pdtbTemp, DataTable pdtbAllocationResult)
		{
			ArrayList arlProduct = new ArrayList();
			DataRow[] arrLeadTime;

			decimal decTotalValue = decimal.Zero;			
			decimal decRate = decimal.Zero;

			string strFilter = CST_ActCostAllocationDetailTable.DEPARTMENTID_FLD + "=" + pdrowSource[CST_ActCostAllocationDetailTable.DEPARTMENTID_FLD].ToString();
			
			//Get all completed product of by department
			DataRow[] arrSameDepartment = pdtbCompletedByProductionLine.Select(strFilter);
			//clear temp table
			pdtbTemp.Rows.Clear();			
			
			//Loop through product
			foreach(DataRow drowSameDepartment in arrSameDepartment)
			{
				//Move to next row if it has been processed
				if(arlProduct.Contains(drowSameDepartment[CST_AllocationResultTable.PRODUCTID_FLD]))
				{
					continue;
				}

				//Added to processed collection
				arlProduct.Add(drowSameDepartment[CST_AllocationResultTable.PRODUCTID_FLD]);								
				//
				strFilter = CST_ActCostAllocationDetailTable.DEPARTMENTID_FLD + "=" + pdrowSource[CST_ActCostAllocationDetailTable.DEPARTMENTID_FLD].ToString();				
				strFilter += " AND " + CST_AllocationResultTable.PRODUCTID_FLD + "=" + drowSameDepartment[CST_AllocationResultTable.PRODUCTID_FLD].ToString();
				
				DataRow[] arrSameProduct = pdtbCompletedByProductionLine.Select(strFilter);				
				
				decimal decTotalQuantity = decimal.Zero;
				decimal decTotalQuantityLeadTime = decimal.Zero;
				foreach(DataRow drowSameProduct in arrSameProduct)
				{
					decimal decLeadTime = decimal.Zero;
					
					//Get lead time by Work Order Detail
					strFilter = PRO_WorkOrderCompletionTable.WORKORDERDETAILID_FLD + "=" + drowSameProduct[PRO_WorkOrderCompletionTable.WORKORDERDETAILID_FLD].ToString();				
					arrLeadTime = pdtbLeadTime.Select(strFilter);

					if(arrLeadTime.Length > 0)
					{ 
						if(!arrLeadTime[0][QUANTITY_LEADTIME_FLD].Equals(DBNull.Value))
						{							
							decLeadTime = decimal.Parse(arrLeadTime[0][QUANTITY_LEADTIME_FLD].ToString());
						}
					}
					
					//sum total leadtime * quantity and total quantity of current product
					decTotalQuantityLeadTime += (decLeadTime * decimal.Parse(drowSameProduct[COMPLETED_QUANTITY_FLD].ToString()));
					decTotalQuantity += decimal.Parse(drowSameProduct[COMPLETED_QUANTITY_FLD].ToString());
				}
				
				//Total value of all products
				decTotalValue += decTotalQuantityLeadTime;

				//create temp row to store information
				DataRow drowNewTemp = pdtbTemp.NewRow();

				drowNewTemp[CST_ActCostAllocationDetailTable.PRODUCTID_FLD] = drowSameDepartment[CST_ActCostAllocationDetailTable.PRODUCTID_FLD];
				drowNewTemp[COMPLETED_QUANTITY_FLD] = decTotalQuantity;
				drowNewTemp[QUANTITY_LEADTIME_FLD] = decTotalQuantityLeadTime;

				pdtbTemp.Rows.Add(drowNewTemp);
			}
					
			//reset decTotalValue value to avoid division by 0 exception
			if(decTotalValue == 0) decTotalValue = 1;

			//loop from temp row and add to result
			foreach(DataRow drowTemp in pdtbTemp.Rows)
			{						
				//create new allocation result row
				DataRow dnewRow = pdtbAllocationResult.NewRow();
				decRate = decimal.Parse(drowTemp[QUANTITY_LEADTIME_FLD].ToString())/ decTotalValue;

				//assign columns's value
				dnewRow[CST_AllocationResultTable.RATE_FLD] = decimal.Round(decRate * 100, ROUND_DIGIT_NUMBER);

				dnewRow[CST_AllocationResultTable.AMOUNT_FLD] = decimal.Round(decRate * decimal.Parse(pdrowSource[CST_ActCostAllocationDetailTable.ALLOCATIONAMOUNT_FLD].ToString()), ROUND_DIGIT_NUMBER);
				dnewRow[CST_AllocationResultTable.COMPLETEDQUANTITY_FLD] = drowTemp[COMPLETED_QUANTITY_FLD];					

				//other value get directly from source
				dnewRow[CST_AllocationResultTable.PRODUCTID_FLD]		= drowTemp[CST_ActCostAllocationDetailTable.PRODUCTID_FLD];
				dnewRow[CST_AllocationResultTable.COSTELEMENTID_FLD]	= pdrowSource[CST_ActCostAllocationDetailTable.COSTELEMENTID_FLD];
				dnewRow[CST_AllocationResultTable.DEPARTMENTID_FLD]		= pdrowSource[CST_ActCostAllocationDetailTable.DEPARTMENTID_FLD];
				dnewRow[CST_AllocationResultTable.PRODUCTGROUPID_FLD]	= pdrowSource[CST_ActCostAllocationDetailTable.PRODUCTGROUPID_FLD];						
				dnewRow[CST_AllocationResultTable.PRODUCTIONLINEID_FLD] = pdrowSource[CST_ActCostAllocationDetailTable.PRODUCTIONLINEID_FLD];				
				dnewRow[CST_AllocationResultTable.ACTCOSTALLOCATIONMASTERID_FLD] = pdrowSource[CST_ActCostAllocationDetailTable.ACTCOSTALLOCATIONMASTERID_FLD];

				//add created row to AllocationResult table
				pdtbAllocationResult.Rows.Add(dnewRow);
			}			
		}		
		

		/// <summary>
		/// Get all completed product of by CCN then allocate
		/// </summary>
		/// <param name="pintCCN"></param>
		/// <param name="pdrowSource"></param>
		/// <param name="pdtbCompletedByProductionLine"></param>
		/// <param name="pdtbLeadTime"></param>
		/// <param name="pdtbTemp"></param>
		/// <param name="pdtbAllocationResult"></param>		
		private void AllocateByCCN(int pintCCN, DataRow pdrowSource, DataTable pdtbCompletedByProductionLine, 
			DataTable pdtbLeadTime, DataTable pdtbTemp, DataTable pdtbAllocationResult)
		{
			ArrayList arlProduct = new ArrayList();
			DataRow[] arrLeadTime;

			decimal decTotalValue = decimal.Zero;			
			decimal decRate = decimal.Zero;

			string strFilter = MST_CCNTable.CCNID_FLD + "=" + pintCCN;

			//Get all completed product of by CCN
			DataRow[] arrSameCCN = pdtbCompletedByProductionLine.Select(strFilter);
			
			//Clear temp table
			pdtbTemp.Rows.Clear();
			
			//Loop through product
			foreach(DataRow drowSameCCN in arrSameCCN)
			{
				//Move to next row if it has been processed
				if(arlProduct.Contains(drowSameCCN[CST_AllocationResultTable.PRODUCTID_FLD]))
				{
					continue;
				}

				//Added to processed collection
				arlProduct.Add(drowSameCCN[CST_AllocationResultTable.PRODUCTID_FLD]);								
				
				//
				strFilter = MST_CCNTable.CCNID_FLD + "=" + pintCCN;				
				strFilter += " AND " + CST_AllocationResultTable.PRODUCTID_FLD + "=" + drowSameCCN[CST_AllocationResultTable.PRODUCTID_FLD].ToString();
				
				DataRow[] arrSameProduct = pdtbCompletedByProductionLine.Select(strFilter);				
				
				decimal decTotalQuantity = decimal.Zero;
				decimal decTotalQuantityLeadTime = decimal.Zero;

				foreach(DataRow drowSameProduct in arrSameProduct)
				{
					decimal decLeadTime = decimal.Zero;
					
					//Get lead time by Work Order Detail
					strFilter = PRO_WorkOrderCompletionTable.WORKORDERDETAILID_FLD + "=" + drowSameProduct[PRO_WorkOrderCompletionTable.WORKORDERDETAILID_FLD].ToString();

					arrLeadTime = pdtbLeadTime.Select(strFilter);

					if(arrLeadTime.Length > 0)
					{ 
						if(!arrLeadTime[0][QUANTITY_LEADTIME_FLD].Equals(DBNull.Value))
						{							
							decLeadTime = decimal.Parse(arrLeadTime[0][QUANTITY_LEADTIME_FLD].ToString());
						}
					}

					//sum total leadtime * quantity and total quantity of current product
					decTotalQuantityLeadTime += (decLeadTime * decimal.Parse(drowSameProduct[COMPLETED_QUANTITY_FLD].ToString()));					
					decTotalQuantity += decimal.Parse(drowSameProduct[COMPLETED_QUANTITY_FLD].ToString());
				}

				//Total value of all products
				decTotalValue += decTotalQuantityLeadTime;

				//create temp row to store information
				DataRow drowNewTemp = pdtbTemp.NewRow();

				drowNewTemp[CST_ActCostAllocationDetailTable.PRODUCTID_FLD] = drowSameCCN[CST_ActCostAllocationDetailTable.PRODUCTID_FLD];
				drowNewTemp[COMPLETED_QUANTITY_FLD] = decTotalQuantity;
				drowNewTemp[QUANTITY_LEADTIME_FLD] = decTotalQuantityLeadTime;

				pdtbTemp.Rows.Add(drowNewTemp);
			}
					
			//reset decTotalValue value to avoid division by 0 exception
			if(decTotalValue == 0) decTotalValue = 1;

			//loop from temp row and add to result
			foreach(DataRow drowTemp in pdtbTemp.Rows)
			{						
				//create new allocation result row
				DataRow dnewRow = pdtbAllocationResult.NewRow();
				decRate = decimal.Parse(drowTemp[QUANTITY_LEADTIME_FLD].ToString())/ decTotalValue;

				//assign columns's value
				dnewRow[CST_AllocationResultTable.RATE_FLD] = decimal.Round(decRate * 100, ROUND_DIGIT_NUMBER);

				dnewRow[CST_AllocationResultTable.AMOUNT_FLD] = decimal.Round(decRate * decimal.Parse(pdrowSource[CST_ActCostAllocationDetailTable.ALLOCATIONAMOUNT_FLD].ToString()), ROUND_DIGIT_NUMBER);
				dnewRow[CST_AllocationResultTable.COMPLETEDQUANTITY_FLD] = drowTemp[COMPLETED_QUANTITY_FLD];					

				//other value get directly from source
				dnewRow[CST_AllocationResultTable.PRODUCTID_FLD]		= drowTemp[CST_ActCostAllocationDetailTable.PRODUCTID_FLD];
				dnewRow[CST_AllocationResultTable.COSTELEMENTID_FLD]	= pdrowSource[CST_ActCostAllocationDetailTable.COSTELEMENTID_FLD];
				dnewRow[CST_AllocationResultTable.DEPARTMENTID_FLD]		= pdrowSource[CST_ActCostAllocationDetailTable.DEPARTMENTID_FLD];
				dnewRow[CST_AllocationResultTable.PRODUCTGROUPID_FLD]	= pdrowSource[CST_ActCostAllocationDetailTable.PRODUCTGROUPID_FLD];						
				dnewRow[CST_AllocationResultTable.PRODUCTIONLINEID_FLD] = pdrowSource[CST_ActCostAllocationDetailTable.PRODUCTIONLINEID_FLD];				
				dnewRow[CST_AllocationResultTable.ACTCOSTALLOCATIONMASTERID_FLD] = pdrowSource[CST_ActCostAllocationDetailTable.ACTCOSTALLOCATIONMASTERID_FLD];

				//add created row to AllocationResult table
				pdtbAllocationResult.Rows.Add(dnewRow);
			}			
		}		
		
		
		#endregion Cost Allocating

		/// <summary>
		/// Check if inserted row is overlap
		/// </summary>
		/// <param name="pobjMasterVO"></param>
		/// <returns></returns>
	
		public bool IsPeriodOverlap(object pobjMasterVO)
		{	
			CST_ActCostAllocationMasterDS dsMaster = new CST_ActCostAllocationMasterDS();

			return dsMaster.IsPeriodOverlap(pobjMasterVO);			
		}
		
		
		/// <summary>
		/// Insert a new record into database
		/// </summary>
	
		public void Add(object pObjectDetail)
		{
			throw new NotImplementedException();
		}

		
	
		public void Add(object pobjMasterVO, System.Data.DataSet pdstDataSet)
		{
			try
			{
				CST_ActCostAllocationMasterDS dsMaster = new CST_ActCostAllocationMasterDS();
				int intMasterId = dsMaster.AddAndReturnID(pobjMasterVO);
				
				//reset master id of voObject
				((CST_ActCostAllocationMasterVO)pobjMasterVO).ActCostAllocationMasterID = intMasterId;

				if(pdstDataSet != null)
				{
					foreach (DataRow row in pdstDataSet.Tables[0].Rows)
					{
						if(row.RowState == DataRowState.Deleted)
						{
							continue;
						}						
						row[CST_ActCostAllocationMasterTable.ACTCOSTALLOCATIONMASTERID_FLD] = intMasterId;
					}

					CST_ActCostAllocationDetailDS dsDetail = new CST_ActCostAllocationDetailDS();
					
					//Check dataset to call UpdateDataSet method
					dsDetail.UpdateDataSet(pdstDataSet);					
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
		/// Delete record by condition
		/// </summary>
	
		public void Delete(object pObjectVO)
		{
			throw new NotImplementedException();
		}
		
		
		/// <summary>
		/// Delete record by condition
		/// </summary>
			
		public void Delete(int pintMasterID)
		{
			//First, delete data on detail table
			CST_ActCostAllocationDetailDS dsDetail = new CST_ActCostAllocationDetailDS();			

			DataTable dtbDetail = dsDetail.GetDetailByMaster(pintMasterID);
			DataSet dtsDetail = dtbDetail.DataSet;
			if(dtsDetail == null)
			{
				dtsDetail = new DataSet();
				dtsDetail.Tables.Add(dtbDetail);
			}
				
			foreach(DataRow drow in dtbDetail.Rows)
			{				
				//delete row
				drow.Delete();
			}

			dsDetail.UpdateDataSet(dtsDetail);

			//Then delete data on master table
			CST_ActCostAllocationMasterDS dsMaster = new CST_ActCostAllocationMasterDS();
			dsMaster.Delete(pintMasterID);
			
		}

		
		/// <summary>
		/// Get the object information by ID of VO class
		/// </summary>
	
		public object GetObjectVO(int pintID, string VOclass)
		{
			try
			{				
				CST_ActCostAllocationMasterDS dsMaster = new CST_ActCostAllocationMasterDS();

				return dsMaster.GetObjectVO(pintID);
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
		/// Get the object information by ID of VO class
		/// </summary>
	
		public object GetObjectVO(string pstrPeriod)
		{
			try
			{		
				CST_ActCostAllocationMasterDS dsMaster = new CST_ActCostAllocationMasterDS();

				return dsMaster.GetObjectVO(pstrPeriod);
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
		/// Get the object information by ID of VO class
		/// </summary>
	
		public object GetObjectVO(int pintID)
		{
			try
			{
				CST_ActCostAllocationMasterDS dsMaster = new CST_ActCostAllocationMasterDS();

				return dsMaster.GetObjectVO(pintID);
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
			try
			{				
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
		
		
	
		public string GetCurrencyCode(int pintCurrencyID)
		{
			MST_CurrencyDS dsCurrency = new MST_CurrencyDS();
            
			MST_CurrencyVO voCurrency = (MST_CurrencyVO)dsCurrency.GetObjectVO(pintCurrencyID);

			if(voCurrency != null)
			{
				return voCurrency.Code;
			}

			return string.Empty;
		}

		
		/// <summary>
		/// Update into Database
		/// </summary>
		/// <param name="pobjMaster"></param>
		/// <param name="pdtbDetail"></param>
	
		public void Update(object pobjMaster, DataSet pdtsDetail)
		{
			CST_ActCostAllocationMasterDS dsMaster = new CST_ActCostAllocationMasterDS();
			dsMaster.Update(pobjMaster);
			
			//check and update master id for new added rows
			if(pdtsDetail != null)
			{
				foreach (DataRow row in pdtsDetail.Tables[0].Rows)
				{
					if(row.RowState == DataRowState.Deleted) continue;
					row[CST_ActCostAllocationMasterTable.ACTCOSTALLOCATIONMASTERID_FLD] = ((CST_ActCostAllocationMasterVO)pobjMaster).ActCostAllocationMasterID;
				}
			}

			CST_ActCostAllocationDetailDS dsDetail = new CST_ActCostAllocationDetailDS();
			//Check dataset to call UpdateDataSet method
			dsDetail.UpdateDataSet(pdtsDetail);			
		}


		/// <summary>
		/// GetDetailByMaster into Database
		/// </summary>
	
		public DataTable GetDetailByMaster(int pintMasterId)
		{
			try
			{
				CST_ActCostAllocationDetailDS deDetail = new CST_ActCostAllocationDetailDS();

				return deDetail.GetDetailByMaster(pintMasterId);
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
		/// GetAllProducts
		/// </summary>
	
		public DataSet GetAllProducts()
		{
			try
			{
				CST_ActCostAllocationDetailDS deDetail = new CST_ActCostAllocationDetailDS();
				return deDetail.GetAllProducts();
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
		/// GetAllProducts
		/// </summary>
	
		public DataSet GetAllCostElements()
		{
			try
			{
				CST_ActCostAllocationDetailDS deDetail = new CST_ActCostAllocationDetailDS();
				return deDetail.GetAllCostElements();
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
		
	
		public void DeleteAllocation(int pintAllocationMasterID)
		{
			(new CST_AllocationResultDS()).DeleteByAllocationMasterID(pintAllocationMasterID);
		}
		/// <summary>
		/// GetAllProducts
		/// </summary>
	
		public DataSet GetAllProductionLine()
		{
			CST_ActCostAllocationDetailDS deDetail = new CST_ActCostAllocationDetailDS();
			return deDetail.GetAllProductionLine();
		}

		/// <summary>
		/// GetAllProducts
		/// </summary>
	
		public DataSet GetAllDepartment()
		{
			MST_DepartmentDS dsDepartment = new MST_DepartmentDS();
			return dsDepartment.List();
		}

		/// <summary>
		/// GetAllProducts
		/// </summary>
	
		public DataSet GetAllGroup()
		{
			CST_ActCostAllocationDetailDS deDetail = new CST_ActCostAllocationDetailDS();
			return deDetail.GetAllGroup();
		}

		/// <summary>
		/// DelChargeAllocation
		/// </summary>
	
		public void DelChargeAllocation(int actCostAllocationMasterId)
		{
			CST_ActCostAllocationMasterDS objdeDetailDS = new CST_ActCostAllocationMasterDS();
			objdeDetailDS.DelChargeAllocation(actCostAllocationMasterId);
		}
	}
}
