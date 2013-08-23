using System;
using System.Collections;
using System.Data;
using System.Data.OleDb;
using System.Text;
using PCSComUtils.Common;
using PCSComUtils.DataAccess;

using PCSComUtils.PCSExc;

namespace PCSComProduction.DCP.DS
{
	/// <summary>
	/// Summary description for DCPReportDS.
	/// </summary>
	public class DCPReportDS 
	{
		const string THIS = "PCSComProduction.DCP.DS.DCPReportDS";
		public DCPReportDS()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public Object GetObjectVO(int pintID)
		{
			throw new NotImplementedException();
		}

		public DataSet List()
		{
			throw new NotImplementedException();
		}

		public void Delete(int pintID)
		{
			throw new NotImplementedException();
		}

		public void Update(Object pobjObjecVO)
		{
			throw new NotImplementedException();
		}

		public void Add(Object pobjObjectVO)
		{
			throw new NotImplementedException();
		}

		public void UpdateDataSet(DataSet pData)
		{
			throw new NotImplementedException();
		}
		/// <summary>
		/// Gets the DCP Option Master object
		/// </summary>
		/// <param name="pintDCPCycleOptionID">DCP Option ID</param>
		/// <returns>DataRow</returns>
		public DataRow GetDCPCycleOptionMaster(int pintDCPCycleOptionID)
		{
			const string METHOD_NAME = THIS + ".GetDCPCycleOptionMaster()";
			OleDbConnection oconPCS = null;
			try
			{
				Utils objUtils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				
				DataTable dtbDCP = new DataTable("PRO_DCOptionMaster");
				string strSql = "SELECT DCOptionMasterID, CCNID, Description, ScheduleType, IgnoreMoveTime,"
					+ " MPSCycleOptionMasterID, ScheduleCode, LastUpdate, Cycle, IncludeCheckPoint"
					+ " FROM PRO_DCOptionMaster"
					+ " WHERE DCOptionMasterID = " + pintDCPCycleOptionID;
				OleDbDataAdapter odad = new OleDbDataAdapter(strSql, oconPCS);
				odad.Fill(dtbDCP);
				if (dtbDCP.Rows.Count > 0)
					return dtbDCP.Rows[0];
				else
					return null;
			}
			catch(OleDbException ex)
			{
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
			}			
			catch (Exception ex) 
			{
				throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
			}
			finally
			{
				if (oconPCS != null)
					if (oconPCS.State != ConnectionState.Closed)
						oconPCS.Close();
			}
		}

		/// <summary>
		/// Get delivery data
		/// </summary>
		/// <param name="parrDays">Report Days</param>
		/// <param name="pintCCNID">CCN</param>
		/// <param name="pintProductID">Product</param>
		/// <param name="pdtbData">Report Data Table</param>
		/// <returns>new row added</returns>
		public DataRow[] GetDeliveryAndProduce(ArrayList parrDays, int pintCCNID, int pintProductID, ref DataTable pdtbData)
		{
			const string METHOD_NAME = THIS + ".GetDeliveryAndProduce()";
			OleDbConnection oconPCS = null;
			OleDbDataAdapter odadData = null;
			try
			{
				Utils objUtils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				string strSql = string.Empty;
				DataRow drowDelivery = pdtbData.NewRow();
				DataRow drowProduce = pdtbData.NewRow();
				bool blnHasDelivery = false;
				bool blnHasProduce = false;
				foreach (DateTime dtmDays in parrDays)
				{
					DataTable dtbTempData = new DataTable();
					// delivery
					strSql = "SELECT " + ITM_CategoryTable.TABLE_NAME + "." + ITM_CategoryTable.CODE_FLD + " AS Category, "
						+ ITM_ProductTable.TABLE_NAME + "." + "Code" + " AS PartNumber, "
						+ "(SELECT ISNULL(SUM(ISNULL(" + MTR_CPOTable.TABLE_NAME + "." + MTR_CPOTable.QUANTITY_FLD + ", 0)), 0)"
						+ " FROM " + MTR_CPOTable.TABLE_NAME + " JOIN " + ITM_ProductTable.TABLE_NAME
						+ " ON " + MTR_CPOTable.TABLE_NAME + "." + MTR_CPOTable.PRODUCTID_FLD
						+ " = " + ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.PRODUCTID_FLD
						+ " WHERE " + ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.PRODUCTID_FLD + " = " + pintProductID
						+ " AND DATEPART(year, " + MTR_CPOTable.TABLE_NAME + "." + MTR_CPOTable.DUEDATE_FLD + ") = " + dtmDays.Year
						+ " AND DATEPART(month, " + MTR_CPOTable.TABLE_NAME + "." + MTR_CPOTable.DUEDATE_FLD + ") = " + dtmDays.Month
						+ " AND DATEPART(day, " + MTR_CPOTable.TABLE_NAME + "." + MTR_CPOTable.DUEDATE_FLD + ") = " + dtmDays.Day + ") AS Quantity, "
						+ ITM_ProductTable.PRODUCTID_FLD
						+ " FROM " + ITM_ProductTable.TABLE_NAME + " LEFT JOIN " + ITM_CategoryTable.TABLE_NAME
						+ " ON " + ITM_CategoryTable.TABLE_NAME + "." + ITM_CategoryTable.CATEGORYID_FLD
						+ " = " + ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.CATEGORYID_FLD
						+ " WHERE " + ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.PRODUCTID_FLD + " = " + pintProductID
						+ " AND " + ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.CCNID_FLD + " = " + pintCCNID;
					odadData = new OleDbDataAdapter(strSql, oconPCS);
					odadData.Fill(dtbTempData);
					if (dtbTempData.Rows.Count > 0)
					{
						drowDelivery["Category"] = dtbTempData.Rows[0]["Category"];
						drowDelivery["PartNumber"] = dtbTempData.Rows[0]["PartNumber"];
						drowDelivery["D" + dtmDays.Day.ToString("00")] = dtbTempData.Rows[0]["Quantity"];
					}
					else
						drowDelivery["D" + dtmDays.Day.ToString("00")] = decimal.Zero;
					drowDelivery["ProductID"] = pintProductID;
					drowDelivery["Type"] = 1;
					if (decimal.Parse(drowDelivery["D" + dtmDays.Day.ToString("00")].ToString()) != decimal.Zero)
						blnHasDelivery = true;

					// produce
					strSql = "SELECT " + ITM_CategoryTable.TABLE_NAME + "." + ITM_CategoryTable.CODE_FLD + " AS Category, "
						+ ITM_ProductTable.TABLE_NAME + "." + "Code" + " AS PartNumber, "
						+ "(SELECT ISNULL(SUM(ISNULL(" + PRO_WorkOrderDetailTable.TABLE_NAME + "." + PRO_WorkOrderDetailTable.ORDERQUANTITY_FLD + ", 0)), 0)"
						+ " FROM " + PRO_WorkOrderDetailTable.TABLE_NAME + " JOIN " + ITM_ProductTable.TABLE_NAME
						+ " ON " + PRO_WorkOrderDetailTable.TABLE_NAME + "." + PRO_WorkOrderDetailTable.PRODUCTID_FLD
						+ " = " + ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.PRODUCTID_FLD
						+ " WHERE " + ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.PRODUCTID_FLD + " = " + pintProductID
						+ " AND DATEPART(year, " + PRO_WorkOrderDetailTable.TABLE_NAME + "." + PRO_WorkOrderDetailTable.DUEDATE_FLD + ") = " + dtmDays.Year
						+ " AND DATEPART(month, " + PRO_WorkOrderDetailTable.TABLE_NAME + "." + PRO_WorkOrderDetailTable.DUEDATE_FLD + ") = " + dtmDays.Month
						+ " AND DATEPART(day, " + PRO_WorkOrderDetailTable.TABLE_NAME + "." + PRO_WorkOrderDetailTable.DUEDATE_FLD + ") = " + dtmDays.Day + ") AS Quantity, "
						+ ITM_ProductTable.PRODUCTID_FLD
						+ " FROM " + ITM_ProductTable.TABLE_NAME + " LEFT JOIN " + ITM_CategoryTable.TABLE_NAME
						+ " ON " + ITM_CategoryTable.TABLE_NAME + "." + ITM_CategoryTable.CATEGORYID_FLD
						+ " = " + ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.CATEGORYID_FLD
						+ " WHERE " + ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.PRODUCTID_FLD + " = " + pintProductID
						+ " AND " + ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.CCNID_FLD + " = " + pintCCNID;
					// clear all rows first
					dtbTempData.Rows.Clear();
					odadData = new OleDbDataAdapter(strSql, oconPCS);
					odadData.Fill(dtbTempData);
					if (dtbTempData.Rows.Count > 0)
					{
						drowProduce["Category"] = dtbTempData.Rows[0]["Category"];
						drowProduce["PartNumber"] = dtbTempData.Rows[0]["PartNumber"];
						drowProduce["D" + dtmDays.Day.ToString("00")] = dtbTempData.Rows[0]["Quantity"];
					}
					else
						drowProduce["D" + dtmDays.Day.ToString("00")] = decimal.Zero;
					drowProduce["ProductID"] = pintProductID;
					drowProduce["Type"] = 2;
					if (decimal.Parse(drowProduce["D" + dtmDays.Day.ToString("00")].ToString()) != decimal.Zero)
						blnHasProduce = true;
				}
				if (blnHasDelivery && blnHasProduce)
				{
					pdtbData.Rows.Add(drowDelivery);
					pdtbData.Rows.Add(drowProduce);
					DataRow[] drowResult = new DataRow[2];
					drowResult[0] = drowDelivery;
					drowResult[1] = drowProduce;
					return drowResult;
				}
				else
					return null;
			}
			catch(OleDbException ex)
			{
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
			}			
			catch (Exception ex) 
			{
				throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
			}
			finally
			{
				if (oconPCS != null)
					if (oconPCS.State != ConnectionState.Closed)
						oconPCS.Close();
			}
		}
		/// <summary>
		/// Gets delivery and produce data
		/// </summary>
		/// <param name="parrDays">Report Days</param>
		/// <param name="pintCCNID">CCN</param>
		/// <param name="pstrListOfItem">List of Product ID</param>
		/// <param name="pintWorkCenterID">Work Center ID</param>
		/// <returns>new row added</returns>
		public DataSet GetDeliveryAndProduce(ArrayList parrDays, int pintCCNID, string pstrListOfItem, int pintWorkCenterID)
		{
			const string METHOD_NAME = THIS + ".GetDeliveryAndProduce()";
			OleDbConnection oconPCS = null;
			OleDbDataAdapter odadData = null;
			try
			{
				Utils objUtils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				string strSql = string.Empty;
				DataSet dstData =  new DataSet();

				#region delivery and Produce quantity of each day in month

				foreach (DateTime dtmDays in parrDays)
				{
					DataTable dtbDelivery = new DataTable("Delivery" + dtmDays.Day.ToString("00"));
					DataTable dtbProduce = new DataTable("Produce" + dtmDays.Day.ToString("00"));
					// delivery
					strSql = "SELECT 1 AS 'Type', ITM_Category.Code AS Category, ITM_Product.Code AS PartNumber,"
						+ " ITM_Product.Description AS PartName, ITM_Product.Revision AS Model, MTR_CPO.Quantity,"
						+ " ITM_Product.ProductID, ITM_Category.CategoryID"
						+ " FROM ITM_Product JOIN"
						+ " (SELECT ISNULL(SUM(ISNULL(MTR_CPO.Quantity, 0)), 0) Quantity, MTR_CPO.ProductID"
						+ " FROM MTR_CPO"
						+ " WHERE DATEPART(year, MTR_CPO.DueDate) = " + dtmDays.Year
						+ " AND DATEPART(month, MTR_CPO.DueDate) = " + dtmDays.Month
						+ " AND DATEPART(day, MTR_CPO.DueDate) = " + dtmDays.Day
						//+ " AND PRO_DCPResultMaster.WorkCenterID = " + pintWorkCenterID
						+ " GROUP BY MTR_CPO.ProductID) MTR_CPO"
						+ " ON ITM_Product.ProductID = MTR_CPO.ProductID"
						+ " LEFT JOIN ITM_Category ON ITM_Category.CategoryID = ITM_Product.CategoryID"
						+ " WHERE ITM_Product.ProductID IN (" + pstrListOfItem + ")";
					odadData = new OleDbDataAdapter(strSql, oconPCS);
					odadData.Fill(dtbDelivery);
					// add to dataset
					dstData.Tables.Add(dtbDelivery);

					#region // HACK: DEL dungla 10-19-2005

//					if (dtbTempData.Rows.Count > 0)
//					{
//						drowDelivery["Category"] = dtbTempData.Rows[0]["Category"];
//						drowDelivery["PartNumber"] = dtbTempData.Rows[0]["PartNumber"];
//						drowDelivery["D" + dtmDays.Day.ToString("00")] = dtbTempData.Rows[0]["Quantity"];
//					}
//					else
//						drowDelivery["D" + dtmDays.Day.ToString("00")] = decimal.Zero;
//					drowDelivery["ProductID"] = pintProductID;
//					drowDelivery["Type"] = 1;
//					if (decimal.Parse(drowDelivery["D" + dtmDays.Day.ToString("00")].ToString()) != decimal.Zero)
//						blnHasDelivery = true;

					#endregion // END: DEL dungla 10-19-2005

					// produce
					strSql = "SELECT 2 AS 'Type', ITM_Category.Code AS Category, ITM_Product.Code AS PartNumber,"
						+ " ITM_Product.Description AS PartName, ITM_Product.Revision AS Model,"
						+ " PRO_DCPResultDetail.Quantity, ITM_Product.ProductID, ITM_Category.CategoryID"
						+ " FROM ITM_Product JOIN"
						+ " (SELECT ISNULL(SUM(ISNULL(PRO_DCPResultDetail.Quantity, 0)), 0) Quantity, PRO_DCPResultMaster.ProductID,"
						+ " DATEPART(year, PRO_DCPResultDetail.StartTime) StartYear,"
						+ " DATEPART(month, PRO_DCPResultDetail.StartTime) StartMonth,"
						+ " DATEPART(day, PRO_DCPResultDetail.StartTime) StartDay,"
						+ " PRO_DCPResultMaster.WorkCenterID"
						+ " FROM PRO_DCPResultDetail JOIN PRO_DCPResultMaster"
						+ " ON PRO_DCPResultMaster.DCPResultMasterID = PRO_DCPResultDetail.DCPResultMasterID"
						+ " GROUP BY DATEPART(year, PRO_DCPResultDetail.StartTime),"
						+ " DATEPART(month, PRO_DCPResultDetail.StartTime),"
						+ " DATEPART(day, PRO_DCPResultDetail.StartTime), PRO_DCPResultMaster.WorkCenterID, ProductID) PRO_DCPResultDetail"
						+ " ON ITM_Product.ProductID = PRO_DCPResultDetail.ProductID"
						+ " LEFT JOIN ITM_Category ON ITM_Category.CategoryID = ITM_Product.CategoryID"
						+ " WHERE PRO_DCPResultDetail.StartYear = " + dtmDays.Year
						+ " AND PRO_DCPResultDetail.StartMonth = " + dtmDays.Month
						+ " AND PRO_DCPResultDetail.StartDay = " + dtmDays.Day
						+ " AND ITM_Product.ProductID IN (" + pstrListOfItem + ")"
						+ " AND PRO_DCPResultDetail.WorkCenterID = " + pintWorkCenterID;
					odadData = new OleDbDataAdapter(strSql, oconPCS);
					odadData.Fill(dtbProduce);
					// add to dataset
					dstData.Tables.Add(dtbProduce);

					#region // HACK: DEL dungla 10-19-2005

//					if (dtbTempData.Rows.Count > 0)
//					{
//						drowProduce["Category"] = dtbTempData.Rows[0]["Category"];
//						drowProduce["PartNumber"] = dtbTempData.Rows[0]["PartNumber"];
//						drowProduce["D" + dtmDays.Day.ToString("00")] = dtbTempData.Rows[0]["Quantity"];
//					}
//					else
//						drowProduce["D" + dtmDays.Day.ToString("00")] = decimal.Zero;
//					drowProduce["ProductID"] = pintProductID;
//					drowProduce["Type"] = 2;
//					if (decimal.Parse(drowProduce["D" + dtmDays.Day.ToString("00")].ToString()) != decimal.Zero)
//						blnHasProduce = true;

					#endregion // END: DEL dungla 10-19-2005
				}

				#endregion

				#region delivery and produce of next two months

				DateTime dtmDay = (DateTime)parrDays[0];
				for (int i = 1; i <= 2; i++)
				{
					DataTable dtbDelivery = new DataTable("DeliveryMonth" + dtmDay.AddMonths(i).Month.ToString("00"));
					DataTable dtbProduce = new DataTable("ProduceMonth" + dtmDay.AddMonths(i).Month.ToString("00"));
					// delivery
					strSql = "SELECT 1 AS 'Type', ITM_Category.Code AS Category, ITM_Product.Code AS PartNumber,"
						+ " ITM_Product.Description AS PartName, ITM_Product.Revision AS Model, MTR_CPO.Quantity,"
						+ " ITM_Product.ProductID, ITM_Category.CategoryID"
						+ " FROM ITM_Product JOIN"
						+ " (SELECT ISNULL(SUM(ISNULL(MTR_CPO.Quantity, 0)), 0) Quantity, MTR_CPO.ProductID"
						+ " FROM MTR_CPO "
						+ " WHERE DATEPART(year, MTR_CPO.DueDate) = " + dtmDay.AddMonths(i).Year
						+ " AND DATEPART(month, MTR_CPO.DueDate) = " + dtmDay.AddMonths(i).Month
						+ " GROUP BY ProductID) MTR_CPO"
						+ " ON ITM_Product.ProductID = MTR_CPO.ProductID"
						+ " LEFT JOIN ITM_Category ON ITM_Category.CategoryID = ITM_Product.CategoryID"
						+ " WHERE ITM_Product.ProductID IN (" + pstrListOfItem + ")";
					odadData = new OleDbDataAdapter(strSql, oconPCS);
					odadData.Fill(dtbDelivery);
					// add to dataset
					dstData.Tables.Add(dtbDelivery);

					// produce
					strSql = "SELECT 2 AS 'Type', ITM_Category.Code AS Category, ITM_Product.Code AS PartNumber,"
						+ " ITM_Product.Description AS PartName, ITM_Product.Revision AS Model,"
						+ " PRO_DCPResultDetail.Quantity, ITM_Product.ProductID, ITM_Category.CategoryID"
						+ " FROM ITM_Product JOIN"
						+ " (SELECT ISNULL(SUM(ISNULL(PRO_DCPResultDetail.Quantity, 0)), 0) Quantity, PRO_DCPResultMaster.ProductID,"
						+ " DATEPART(year, PRO_DCPResultDetail.StartTime) StartYear,"
						+ " DATEPART(month, PRO_DCPResultDetail.StartTime) StartMonth,"
						+ " PRO_DCPResultMaster.WorkCenterID"
						+ " FROM PRO_DCPResultDetail JOIN PRO_DCPResultMaster"
						+ " ON PRO_DCPResultMaster.DCPResultMasterID = PRO_DCPResultDetail.DCPResultMasterID"
						+ " GROUP BY DATEPART(year, PRO_DCPResultDetail.StartTime),"
						+ " DATEPART(month, PRO_DCPResultDetail.StartTime),"
						+ " PRO_DCPResultMaster.WorkCenterID, ProductID) PRO_DCPResultDetail"
						+ " ON ITM_Product.ProductID = PRO_DCPResultDetail.ProductID"
						+ " LEFT JOIN ITM_Category ON ITM_Category.CategoryID = ITM_Product.CategoryID"
						+ " WHERE PRO_DCPResultDetail.StartYear = " + dtmDay.AddMonths(i).Year
						+ " AND PRO_DCPResultDetail.StartMonth = " + dtmDay.AddMonths(i).Month
						+ " AND ITM_Product.ProductID IN (" + pstrListOfItem + ")"
						+ " AND PRO_DCPResultDetail.WorkCenterID = " + pintWorkCenterID;
					odadData = new OleDbDataAdapter(strSql, oconPCS);
					odadData.Fill(dtbProduce);
					// add to dataset
					dstData.Tables.Add(dtbProduce);
				}

				#endregion

				return dstData;
			}
			catch(OleDbException ex)
			{
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
			}			
			catch (Exception ex) 
			{
				throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
			}
			finally
			{
				if (oconPCS != null)
					if (oconPCS.State != ConnectionState.Closed)
						oconPCS.Close();
			}
		}
		/// <summary>
		/// Gets delivery and produce data of all production line
		/// </summary>
		/// <param name="pintYear">Selected Year</param>
		/// <param name="pintMonth">Selected Month</param>
		/// <param name="pstrAllDays">All day in month</param>
		/// <param name="pintCCNID">CCN</param>
		/// <param name="pstrListOfItem">List of Product ID</param>
		/// <returns>DataSet</returns>
		public DataSet GetDeliveryAndProduce(int pintYear, int pintMonth, string pstrAllDays, int pintCCNID, string pstrListOfItem, int pintDCOptionMasterID)
		{
			const string METHOD_NAME = THIS + ".GetDeliveryAndProduce()";
			OleDbConnection oconPCS = null;
			OleDbDataAdapter odadData = null;
			try
			{
				Utils objUtils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				string strSql = string.Empty;
				DataSet dstData =  new DataSet();

				#region delivery and Produce quantity of each day in month
				DataTable dtbDelivery = new DataTable("Delivery");
				DataTable dtbProduce = new DataTable("Produce");
				// delivery
				strSql = "SELECT 1 AS 'Type', ITM_Category.Code AS Category, ITM_Product.Code AS PartNumber,"
					+ " ITM_Product.Description AS PartName, ITM_Product.Revision AS Model, MTR_CPO.Quantity,"
					+ " ITM_Product.ProductID, ITM_Category.CategoryID,"
					+ " MTR_CPO.DueDate"
					+ " FROM ITM_Product JOIN"
					+ " (SELECT ISNULL(SUM(ISNULL(MTR_CPO.Quantity, 0)), 0) Quantity, MTR_CPO.ProductID,"
					+ " MTR_CPO.DueDate"
					+ " FROM MTR_CPO"
					+ " WHERE DATEPART(year, MTR_CPO.DueDate) = " + pintYear
					+ " AND DATEPART(month, MTR_CPO.DueDate) = " + pintMonth
					+ " AND DATEPART(day, MTR_CPO.DueDate) IN (" + pstrAllDays + ")"
					+ " AND MTR_CPO.IsSafetyStock = 0"
					+ " GROUP BY MTR_CPO.ProductID,"
					+ " MTR_CPO.DueDate) MTR_CPO"
					+ " ON ITM_Product.ProductID = MTR_CPO.ProductID"
					+ " LEFT JOIN ITM_Category ON ITM_Category.CategoryID = ITM_Product.CategoryID";
				if (pstrListOfItem.Trim().Length > 0)
					strSql += " WHERE ITM_Product.ProductID IN (" + pstrListOfItem + ")";
				strSql += " ORDER BY ITM_Product.ProductID";
				odadData = new OleDbDataAdapter(strSql, oconPCS);
				odadData.Fill(dtbDelivery);
				// add to dataset
				dstData.Tables.Add(dtbDelivery);

				// produce
				strSql = "SELECT 2 AS 'Type', ITM_Category.Code AS Category, ITM_Product.Code AS PartNumber,"
					+ " ITM_Product.Description AS PartName, ITM_Product.Revision AS Model,"
					+ " PRO_DCPResultDetail.Quantity, ITM_Product.ProductID, ITM_Category.CategoryID,"
					+ " PRO_DCPResultDetail.StartTime, PRO_DCPResultDetail.EndTime"
					+ " FROM ITM_Product JOIN"
					+ " (SELECT ISNULL(SUM(ISNULL(PRO_DCPResultDetail.Quantity, 0)), 0) Quantity, PRO_DCPResultMaster.ProductID,"
					+ " PRO_DCPResultDetail.StartTime, PRO_DCPResultDetail.EndTime,"
					+ " PRO_DCPResultMaster.WorkCenterID"
					+ " FROM PRO_DCPResultDetail JOIN PRO_DCPResultMaster"
					+ " ON PRO_DCPResultMaster.DCPResultMasterID = PRO_DCPResultDetail.DCPResultMasterID"
					+ " WHERE PRO_DCPResultMaster.DCOptionMasterID = " + pintDCOptionMasterID
					+ " GROUP BY PRO_DCPResultDetail.StartTime, PRO_DCPResultDetail.EndTime,"
					+ " PRO_DCPResultMaster.WorkCenterID, ProductID) PRO_DCPResultDetail"
					+ " ON ITM_Product.ProductID = PRO_DCPResultDetail.ProductID"
					+ " JOIN MST_WorkCenter"
					+ " ON PRO_DCPResultDetail.WorkCenterID = MST_WorkCenter.WorkCenterID"
					+ " LEFT JOIN ITM_Category ON ITM_Category.CategoryID = ITM_Product.CategoryID"
					+ " WHERE MST_WorkCenter.IsMain = 1";
				if (pstrListOfItem.Trim().Length > 0)
					strSql += " AND ITM_Product.ProductID IN (" + pstrListOfItem + ")";
				strSql += " ORDER BY ITM_Product.ProductID";
				odadData = new OleDbDataAdapter(strSql, oconPCS);
				odadData.Fill(dtbProduce);
				// add to dataset
				dstData.Tables.Add(dtbProduce);

				#endregion

				#region delivery and produce of next two months

				DateTime dtmNextMonth = new DateTime(pintYear, pintMonth, 1).AddMonths(1);
				DateTime dtmNext2Month = dtmNextMonth.AddMonths(1);
				dtbDelivery = new DataTable("DeliveryMonth");
				dtbProduce = new DataTable("ProduceMonth");
				// delivery
				strSql = "SELECT 1 AS 'Type', ITM_Category.Code AS Category, ITM_Product.Code AS PartNumber,"
					+ " ITM_Product.Description AS PartName, ITM_Product.Revision AS Model, MTR_CPO.Quantity,"
					+ " ITM_Product.ProductID, ITM_Category.CategoryID,"
					+ " MTR_CPO.DueDate"
					+ " FROM ITM_Product JOIN"
					+ " (SELECT ISNULL(SUM(ISNULL(MTR_CPO.Quantity, 0)), 0) Quantity, MTR_CPO.ProductID,"
					+ " MTR_CPO.DueDate"
					+ " FROM MTR_CPO "
					+ " WHERE DATEPART(year, MTR_CPO.DueDate) IN (" + dtmNextMonth.Year + "," + dtmNext2Month.Year + ")"
					+ " AND DATEPART(month, MTR_CPO.DueDate) IN (" + dtmNextMonth.Month + "," + dtmNext2Month.Month + ")"
					+ " GROUP BY MTR_CPO.ProductID,"
					+ " MTR_CPO.DueDate) MTR_CPO"
					+ " ON ITM_Product.ProductID = MTR_CPO.ProductID"
					+ " LEFT JOIN ITM_Category ON ITM_Category.CategoryID = ITM_Product.CategoryID";
				if (pstrListOfItem.Trim().Length > 0)
					strSql += " WHERE ITM_Product.ProductID IN (" + pstrListOfItem + ")";
				strSql += " ORDER BY ITM_Product.ProductID";
				odadData = new OleDbDataAdapter(strSql, oconPCS);
				odadData.Fill(dtbDelivery);
				// add to dataset
				dstData.Tables.Add(dtbDelivery);

				// produce
				strSql = "SELECT 2 AS 'Type', ITM_Category.Code AS Category, ITM_Product.Code AS PartNumber,"
					+ " ITM_Product.Description AS PartName, ITM_Product.Revision AS Model,"
					+ " PRO_DCPResultDetail.Quantity, ITM_Product.ProductID, ITM_Category.CategoryID,"
					+ " PRO_DCPResultDetail.StartTime, PRO_DCPResultDetail.EndTime"
					+ " FROM ITM_Product JOIN"
					+ " (SELECT ISNULL(SUM(ISNULL(PRO_DCPResultDetail.Quantity, 0)), 0) Quantity, PRO_DCPResultMaster.ProductID,"
					+ " PRO_DCPResultDetail.StartTime,"
					+ " PRO_DCPResultDetail.EndTime,"
					+ " PRO_DCPResultMaster.WorkCenterID"
					+ " FROM PRO_DCPResultDetail JOIN PRO_DCPResultMaster"
					+ " ON PRO_DCPResultMaster.DCPResultMasterID = PRO_DCPResultDetail.DCPResultMasterID"
					+ " WHERE PRO_DCPResultMaster.DCOptionMasterID = " + pintDCOptionMasterID
					+ " GROUP BY PRO_DCPResultDetail.StartTime,"
					+ " PRO_DCPResultDetail.EndTime,"
					+ " PRO_DCPResultMaster.WorkCenterID, ProductID) PRO_DCPResultDetail"
					+ " ON ITM_Product.ProductID = PRO_DCPResultDetail.ProductID"
					+ " JOIN MST_WorkCenter"
					+ " ON PRO_DCPResultDetail.WorkCenterID = MST_WorkCenter.WorkCenterID"
					+ " LEFT JOIN ITM_Category ON ITM_Category.CategoryID = ITM_Product.CategoryID"
					+ " WHERE MST_WorkCenter.IsMain = 1";
				if (pstrListOfItem.Trim().Length > 0)
					strSql += " AND ITM_Product.ProductID IN (" + pstrListOfItem + ")";
				strSql += " ORDER BY ITM_Product.ProductID";
					
				odadData = new OleDbDataAdapter(strSql, oconPCS);
				odadData.Fill(dtbProduce);
				// add to dataset
				dstData.Tables.Add(dtbProduce);

				#endregion

				return dstData;
			}
			catch(OleDbException ex)
			{
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
			}			
			catch (Exception ex) 
			{
				throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
			}
			finally
			{
				if (oconPCS != null)
					if (oconPCS.State != ConnectionState.Closed)
						oconPCS.Close();
			}
		}
		/// <summary>
		/// Gets delivery and produce data selected production line
		/// </summary>
		/// <param name="pintYear">Selected Year</param>
		/// <param name="pintMonth">Selected Month</param>
		/// <param name="pstrAllDays">All day in month</param>
		/// <param name="pintCCNID">CCN</param>
		/// <param name="pstrListOfItem">List of Product ID</param>
		/// <param name="pstrProductionLineID">List of Production Line</param>
		/// <returns>DataSet</returns>
		public DataSet GetDeliveryAndProduce(int pintYear, int pintMonth, string pstrAllDays, int pintCCNID, string pstrListOfItem, string pstrProductionLineID, int pintDCOptionMasterID)
		{
			const string METHOD_NAME = THIS + ".GetDeliveryAndProduce()";
			OleDbConnection oconPCS = null;
			OleDbDataAdapter odadData = null;
			try
			{
				Utils objUtils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				string strSql = string.Empty;
				DataSet dstData =  new DataSet();

				#region delivery and Produce quantity of each day in month
				DataTable dtbDelivery = new DataTable("Delivery");
				DataTable dtbProduce = new DataTable("Produce");
				// delivery
				strSql = "SELECT 1 AS 'Type', ITM_Category.Code AS Category, ITM_Product.Code AS PartNumber,"
					+ " ITM_Product.Description AS PartName, ITM_Product.Revision AS Model, MTR_CPO.Quantity,"
					+ " ITM_Product.ProductID, ITM_Category.CategoryID,"
					+ " MTR_CPO.StartYear, MTR_CPO.StartMonth, MTR_CPO.StartDay"
					+ " FROM ITM_Product JOIN"
					+ " (SELECT ISNULL(SUM(ISNULL(MTR_CPO.Quantity, 0)), 0) Quantity, MTR_CPO.ProductID,"
					+ " DATEPART(year, MTR_CPO.DueDate) AS StartYear,"
					+ " DATEPART(month, MTR_CPO.DueDate) AS StartMonth,"
					+ " DATEPART(day, MTR_CPO.DueDate) AS StartDay"
					+ " FROM MTR_CPO"
					+ " WHERE DATEPART(year, MTR_CPO.DueDate) = " + pintYear
					+ " AND DATEPART(month, MTR_CPO.DueDate) = " + pintMonth
					+ " AND DATEPART(day, MTR_CPO.DueDate) IN (" + pstrAllDays + ")"
					+ " AND MTR_CPO.IsSafetyStock = 0"
					+ " GROUP BY MTR_CPO.ProductID,"
					+ " DATEPART(year, MTR_CPO.DueDate),"
					+ " DATEPART(month, MTR_CPO.DueDate),"
					+ " DATEPART(day, MTR_CPO.DueDate)) MTR_CPO"
					+ " ON ITM_Product.ProductID = MTR_CPO.ProductID"
					+ " LEFT JOIN ITM_Category ON ITM_Category.CategoryID = ITM_Product.CategoryID";
				if (pstrListOfItem.Trim().Length > 0)
					strSql += " WHERE ITM_Product.ProductID IN (" + pstrListOfItem + ")";
				odadData = new OleDbDataAdapter(strSql, oconPCS);
				odadData.Fill(dtbDelivery);
				// add to dataset
				dstData.Tables.Add(dtbDelivery);

				// produce
				strSql = "SELECT 2 AS 'Type', ITM_Category.Code AS Category, ITM_Product.Code AS PartNumber,"
					+ " ITM_Product.Description AS PartName, ITM_Product.Revision AS Model,"
					+ " PRO_DCPResultDetail.Quantity, ITM_Product.ProductID, ITM_Category.CategoryID,"
					+ " PRO_DCPResultDetail.StartTime, PRO_DCPResultDetail.EndTime"
					+ " FROM ITM_Product JOIN"
					+ " (SELECT ISNULL(SUM(ISNULL(PRO_DCPResultDetail.Quantity, 0)), 0) Quantity, PRO_DCPResultMaster.ProductID,"
					+ " PRO_DCPResultDetail.StartTime, PRO_DCPResultDetail.EndTime,"
					+ " PRO_DCPResultMaster.WorkCenterID"
					+ " FROM PRO_DCPResultDetail JOIN PRO_DCPResultMaster"
					+ " ON PRO_DCPResultMaster.DCPResultMasterID = PRO_DCPResultDetail.DCPResultMasterID"
					+ " WHERE PRO_DCPResultMaster.DCOptionMasterID = " + pintDCOptionMasterID
					+ " GROUP BY PRO_DCPResultDetail.StartTime, PRO_DCPResultDetail.EndTime,"
					+ " PRO_DCPResultMaster.WorkCenterID, ProductID) PRO_DCPResultDetail"
					+ " ON ITM_Product.ProductID = PRO_DCPResultDetail.ProductID"
					+ " JOIN MST_WorkCenter"
					+ " ON PRO_DCPResultDetail.WorkCenterID = MST_WorkCenter.WorkCenterID"
					+ " LEFT JOIN ITM_Category ON ITM_Category.CategoryID = ITM_Product.CategoryID"
					+ " WHERE MST_WorkCenter.ProductionLineID IN (" + pstrProductionLineID + ")"
					+ " AND MST_WorkCenter.IsMain = 1";
				if (pstrListOfItem.Trim().Length > 0)
					strSql += " AND ITM_Product.ProductID IN (" + pstrListOfItem + ")";
				odadData = new OleDbDataAdapter(strSql, oconPCS);
				odadData.Fill(dtbProduce);
				// add to dataset
				dstData.Tables.Add(dtbProduce);

				#endregion

				#region delivery and produce of next two months

				DateTime dtmNextMonth = new DateTime(pintYear, pintMonth, 1).AddMonths(1);
				DateTime dtmNext2Month = dtmNextMonth.AddMonths(1);
				dtbDelivery = new DataTable("DeliveryMonth");
				dtbProduce = new DataTable("ProduceMonth");
				// delivery
				strSql = "SELECT 1 AS 'Type', ITM_Category.Code AS Category, ITM_Product.Code AS PartNumber,"
					+ " ITM_Product.Description AS PartName, ITM_Product.Revision AS Model, MTR_CPO.Quantity,"
					+ " ITM_Product.ProductID, ITM_Category.CategoryID,"
					+ " MTR_CPO.StartYear, MTR_CPO.StartMonth"
					+ " FROM ITM_Product JOIN"
					+ " (SELECT ISNULL(SUM(ISNULL(MTR_CPO.Quantity, 0)), 0) Quantity, MTR_CPO.ProductID,"
					+ " DATEPART(year, MTR_CPO.DueDate) StartYear,"
					+ " DATEPART(month, MTR_CPO.DueDate) StartMonth"
					+ " FROM MTR_CPO "
					+ " WHERE DATEPART(year, MTR_CPO.DueDate) IN (" + dtmNextMonth.Year + "," + dtmNext2Month.Year + ")"
					+ " AND DATEPART(month, MTR_CPO.DueDate) IN (" + dtmNextMonth.Month + "," + dtmNext2Month.Month + ")"
					+ " GROUP BY MTR_CPO.ProductID,"
					+ " DATEPART(year, MTR_CPO.DueDate),"
					+ " DATEPART(month, MTR_CPO.DueDate)) MTR_CPO"
					+ " ON ITM_Product.ProductID = MTR_CPO.ProductID"
					+ " LEFT JOIN ITM_Category ON ITM_Category.CategoryID = ITM_Product.CategoryID";
				if (pstrListOfItem.Trim().Length > 0)
					strSql += " WHERE ITM_Product.ProductID IN (" + pstrListOfItem + ")";
				odadData = new OleDbDataAdapter(strSql, oconPCS);
				odadData.Fill(dtbDelivery);
				// add to dataset
				dstData.Tables.Add(dtbDelivery);

				// produce
				strSql = "SELECT 2 AS 'Type', ITM_Category.Code AS Category, ITM_Product.Code AS PartNumber,"
					+ " ITM_Product.Description AS PartName, ITM_Product.Revision AS Model,"
					+ " PRO_DCPResultDetail.Quantity, ITM_Product.ProductID, ITM_Category.CategoryID,"
					+ " PRO_DCPResultDetail.StartTime, PRO_DCPResultDetail.EndTime"
					+ " FROM ITM_Product JOIN"
					+ " (SELECT ISNULL(SUM(ISNULL(PRO_DCPResultDetail.Quantity, 0)), 0) Quantity, PRO_DCPResultMaster.ProductID,"
					+ " PRO_DCPResultDetail.StartTime,"
					+ " PRO_DCPResultDetail.EndTime,"
					+ " PRO_DCPResultMaster.WorkCenterID"
					+ " FROM PRO_DCPResultDetail JOIN PRO_DCPResultMaster"
					+ " ON PRO_DCPResultMaster.DCPResultMasterID = PRO_DCPResultDetail.DCPResultMasterID"
					+ " WHERE PRO_DCPResultMaster.DCOptionMasterID = " + pintDCOptionMasterID
					+ " GROUP BY PRO_DCPResultDetail.StartTime,"
					+ " PRO_DCPResultDetail.EndTime,"
					+ " PRO_DCPResultMaster.WorkCenterID, ProductID) PRO_DCPResultDetail"
					+ " ON ITM_Product.ProductID = PRO_DCPResultDetail.ProductID"
					+ " JOIN MST_WorkCenter"
					+ " ON PRO_DCPResultDetail.WorkCenterID = MST_WorkCenter.WorkCenterID"
					+ " LEFT JOIN ITM_Category ON ITM_Category.CategoryID = ITM_Product.CategoryID"
					+ " WHERE MST_WorkCenter.ProductionLineID IN (" + pstrProductionLineID + ")"
					+ " AND MST_WorkCenter.IsMain = 1";
				if (pstrListOfItem.Trim().Length > 0)
					strSql += " AND ITM_Product.ProductID IN (" + pstrListOfItem + ")";
					
				odadData = new OleDbDataAdapter(strSql, oconPCS);
				odadData.Fill(dtbProduce);
				// add to dataset
				dstData.Tables.Add(dtbProduce);

				#endregion

				return dstData;
			}
			catch(OleDbException ex)
			{
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
			}			
			catch (Exception ex) 
			{
				throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
			}
			finally
			{
				if (oconPCS != null)
					if (oconPCS.State != ConnectionState.Closed)
						oconPCS.Close();
			}
		}
		/// <summary>
		/// Gets delivery and produce data
		/// </summary>
		/// <param name="pintYear">Selected Year</param>
		/// <param name="pintMonth">Selected Month</param>
		/// <param name="pstrAllDays">All day in month</param>
		/// <param name="pintCCNID">CCN</param>
		/// <param name="pstrListOfItem">List of Product ID</param>
		/// <param name="pintProductionLineID">Production Line</param>
		/// <param name="pstrShift">Selected Shift</param>
		/// <param name="pintDCOptionMasterID">DCP Cycle Option Master</param>
		/// <returns>DataSet</returns>
		public DataSet GetDeliveryAndProduce(int pintYear, int pintMonth, string pstrAllDays, int pintCCNID, string pstrListOfItem, int pintProductionLineID, int pintDCOptionMasterID, string pstrShift)
		{
			const string METHOD_NAME = THIS + ".GetDeliveryAndProduce()";
			OleDbConnection oconPCS = null;
			OleDbDataAdapter odadData = null;
			try
			{
				Utils objUtils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				string strSql = string.Empty;
				DataSet dstData =  new DataSet();

				#region delivery and Produce quantity of each day in month
				DataTable dtbDelivery = new DataTable("Delivery");
				DataTable dtbProduce = new DataTable("Produce");
				// delivery
				strSql = "SELECT 1 AS 'Type', ITM_Category.Code AS Category, ITM_Product.Code AS PartNumber,"
					+ " ITM_Product.Description AS PartName, ITM_Product.Revision AS Model,"
					+ " FLOOR(MTR_CPO.Quantity * ((100 - ITM_Product.ScrapPercent)/100)) AS Quantity,"
					+ " ITM_Product.ProductID, ITM_Category.CategoryID, MTR_CPO.DueDate"
					//+ " MTR_CPO.StartYear, MTR_CPO.StartMonth, MTR_CPO.StartDay"
					+ " FROM ITM_Product JOIN"
					+ " (SELECT ISNULL(SUM(ISNULL(MTR_CPO.Quantity, 0)), 0) Quantity, MTR_CPO.ProductID,"
					+ " MTR_CPO.DueDate"
//					+ " DATEPART(year, MTR_CPO.DueDate) AS StartYear,"
//					+ " DATEPART(month, MTR_CPO.DueDate) AS StartMonth,"
//					+ " DATEPART(day, MTR_CPO.DueDate) AS StartDay"
					+ " FROM MTR_CPO"
					+ " WHERE DATEPART(year, MTR_CPO.DueDate) = " + pintYear
					+ " AND DATEPART(month, MTR_CPO.DueDate) = " + pintMonth
					+ " AND DATEPART(day, MTR_CPO.DueDate) IN (" + pstrAllDays + ")"
					+ " AND MTR_CPO.IsSafetyStock = 0"
					+ " GROUP BY MTR_CPO.ProductID,"
					+ " MTR_CPO.DueDate) MTR_CPO"
//					+ " DATEPART(year, MTR_CPO.DueDate),"
//					+ " DATEPART(month, MTR_CPO.DueDate),"
//					+ " DATEPART(day, MTR_CPO.DueDate)) MTR_CPO"
					+ " ON ITM_Product.ProductID = MTR_CPO.ProductID"
					+ " LEFT JOIN ITM_Category ON ITM_Category.CategoryID = ITM_Product.CategoryID";
				if (pstrListOfItem.Trim().Length > 0)
					strSql += " WHERE ITM_Product.ProductID IN (" + pstrListOfItem + ")";
				strSql += " ORDER BY ITM_Product.ProductID";
				odadData = new OleDbDataAdapter(strSql, oconPCS);
				odadData.Fill(dtbDelivery);
				// add to dataset
				dstData.Tables.Add(dtbDelivery);

				// produce
				strSql = "SELECT DISTINCT 2 AS 'Type', ITM_Category.Code AS Category, ITM_Product.Code AS PartNumber,"
					+ " ITM_Product.Description AS PartName, ITM_Product.Revision AS Model,"
					+ " PRO_DCPResultDetail.Quantity, ITM_Product.ProductID, ITM_Category.CategoryID, PRO_DCPResultDetail.WorkingDate"
					+ " FROM ITM_Product JOIN"
					+ " (SELECT ISNULL(SUM(ISNULL(PRO_DCPResultDetail.Quantity, 0)), 0) Quantity, PRO_DCPResultMaster.ProductID,"
					+ " PRO_DCPResultDetail.WorkingDate, PRO_DCPResultMaster.WorkCenterID"
					+ " FROM PRO_DCPResultDetail JOIN PRO_DCPResultMaster"
					+ " ON PRO_DCPResultMaster.DCPResultMasterID = PRO_DCPResultDetail.DCPResultMasterID"
					+ " WHERE PRO_DCPResultMaster.DCOptionMasterID = " + pintDCOptionMasterID
					+ " GROUP BY PRO_DCPResultDetail.WorkingDate,"
					+ " PRO_DCPResultMaster.WorkCenterID, ProductID) PRO_DCPResultDetail"
					+ " ON ITM_Product.ProductID = PRO_DCPResultDetail.ProductID"
					+ " JOIN MST_WorkCenter ON PRO_DCPResultDetail.WorkCenterID = MST_WorkCenter.WorkCenterID"
					+ " JOIN PRO_WCCapacity ON PRO_WCCapacity.WorkCenterID = MST_WorkCenter.WorkCenterID"
					+ " JOIN PRO_ShiftCapacity ON PRO_WCCapacity.WCCapacityID = PRO_ShiftCapacity.WCCapacityID"
					+ " JOIN PRO_Shift ON PRO_ShiftCapacity.ShiftID = PRO_Shift.ShiftID"
					+ " LEFT JOIN ITM_Category ON ITM_Category.CategoryID = ITM_Product.CategoryID"
					+ " WHERE MST_WorkCenter.IsMain = 1"
					+ " AND MST_WorkCenter.ProductionLineID = " + pintProductionLineID;
				if (pstrShift != null && pstrShift != string.Empty)
					strSql += " AND PRO_Shift.ShiftDesc = ?";
				if (pstrListOfItem.Trim().Length > 0)
					strSql += " AND ITM_Product.ProductID IN (" + pstrListOfItem + ")";
				strSql += " ORDER BY ITM_Product.ProductID";

				OleDbCommand cmdDay = new OleDbCommand(strSql, oconPCS);
				if (pstrShift != null && pstrShift != string.Empty)
				{
					cmdDay.Parameters.Add(new OleDbParameter(PRO_ShiftTable.SHIFTDESC_FLD, OleDbType.VarWChar));
					cmdDay.Parameters[PRO_ShiftTable.SHIFTDESC_FLD].Value = pstrShift;
				}

				odadData = new OleDbDataAdapter(cmdDay);
				odadData.Fill(dtbProduce);
				// add to dataset
				dstData.Tables.Add(dtbProduce);

				#endregion

				#region delivery and produce of next two months

				DateTime dtmNextMonth = new DateTime(pintYear, pintMonth, 1).AddMonths(1);
				DateTime dtmNext2Month = dtmNextMonth.AddMonths(1);
				dtbDelivery = new DataTable("DeliveryMonth");
				dtbProduce = new DataTable("ProduceMonth");
				// delivery
				strSql = "SELECT 1 AS 'Type', ITM_Category.Code AS Category, ITM_Product.Code AS PartNumber,"
					+ " ITM_Product.Description AS PartName, ITM_Product.Revision AS Model,"
					+ " FLOOR(MTR_CPO.Quantity * ((100 - ITM_Product.ScrapPercent)/100)) AS Quantity,"
					+ " ITM_Product.ProductID, ITM_Category.CategoryID, MTR_CPO.DueDate"
					//+ " MTR_CPO.StartYear, MTR_CPO.StartMonth"
					+ " FROM ITM_Product JOIN"
					+ " (SELECT ISNULL(SUM(ISNULL(MTR_CPO.Quantity, 0)), 0) Quantity, MTR_CPO.ProductID,"
					+ " MTR_CPO.DueDate"
//					+ " DATEPART(year, MTR_CPO.DueDate) AS StartYear,"
//					+ " DATEPART(month, MTR_CPO.DueDate) AS StartMonth"
					+ " FROM MTR_CPO "
					+ " WHERE DATEPART(year, MTR_CPO.DueDate) IN (" + dtmNextMonth.Year + "," + dtmNext2Month.Year + ")"
					+ " AND DATEPART(month, MTR_CPO.DueDate) IN (" + dtmNextMonth.Month + "," + dtmNext2Month.Month + ")"
					+ " GROUP BY MTR_CPO.ProductID,"
					+ " MTR_CPO.DueDate) MTR_CPO"
//					+ " DATEPART(year, MTR_CPO.DueDate),"
//					+ " DATEPART(month, MTR_CPO.DueDate)) MTR_CPO"
					+ " ON ITM_Product.ProductID = MTR_CPO.ProductID"
					+ " LEFT JOIN ITM_Category ON ITM_Category.CategoryID = ITM_Product.CategoryID";
				if (pstrListOfItem.Trim().Length > 0)
					strSql += " WHERE ITM_Product.ProductID IN (" + pstrListOfItem + ")";
				strSql += " ORDER BY ITM_Product.ProductID";
				odadData = new OleDbDataAdapter(strSql, oconPCS);
				odadData.Fill(dtbDelivery);
				// add to dataset
				dstData.Tables.Add(dtbDelivery);

				// produce
				strSql = "SELECT DISTINCT 2 AS 'Type', ITM_Category.Code AS Category, ITM_Product.Code AS PartNumber,"
					+ " ITM_Product.Description AS PartName, ITM_Product.Revision AS Model,"
					+ " PRO_DCPResultDetail.Quantity, ITM_Product.ProductID, ITM_Category.CategoryID,"
					+ " PRO_DCPResultDetail.WorkingDate"
					+ " FROM ITM_Product JOIN"
					+ " (SELECT ISNULL(SUM(ISNULL(PRO_DCPResultDetail.Quantity, 0)), 0) Quantity, PRO_DCPResultMaster.ProductID,"
					+ " PRO_DCPResultDetail.WorkingDate, PRO_DCPResultMaster.WorkCenterID"
					+ " FROM PRO_DCPResultDetail JOIN PRO_DCPResultMaster"
					+ " ON PRO_DCPResultMaster.DCPResultMasterID = PRO_DCPResultDetail.DCPResultMasterID"
					+ " WHERE PRO_DCPResultMaster.DCOptionMasterID = " + pintDCOptionMasterID
					+ " GROUP BY PRO_DCPResultDetail.WorkingDate,"
					+ " PRO_DCPResultMaster.WorkCenterID, ProductID) PRO_DCPResultDetail"
					+ " ON ITM_Product.ProductID = PRO_DCPResultDetail.ProductID"
					+ " JOIN MST_WorkCenter"
					+ " ON PRO_DCPResultDetail.WorkCenterID = MST_WorkCenter.WorkCenterID"
					+ " JOIN PRO_WCCapacity ON PRO_WCCapacity.WorkCenterID = MST_WorkCenter.WorkCenterID"
					+ " JOIN PRO_ShiftCapacity ON PRO_WCCapacity.WCCapacityID = PRO_ShiftCapacity.WCCapacityID"
					+ " JOIN PRO_Shift ON PRO_ShiftCapacity.ShiftID = PRO_Shift.ShiftID"
					+ " LEFT JOIN ITM_Category ON ITM_Category.CategoryID = ITM_Product.CategoryID"
					+ " WHERE MST_WorkCenter.IsMain = 1"
					+ " AND MST_WorkCenter.ProductionLineID = " + pintProductionLineID;
				if (pstrShift != null && pstrShift != string.Empty)
					strSql += " AND PRO_Shift.ShiftDesc = ?";
				if (pstrListOfItem.Trim().Length > 0)
					strSql += " AND ITM_Product.ProductID IN (" + pstrListOfItem + ")";
				strSql += " ORDER BY ITM_Product.ProductID";

				OleDbCommand cmdPCSMonth = new OleDbCommand(strSql, oconPCS);
				if (pstrShift != null && pstrShift != string.Empty)
				{
					cmdPCSMonth.Parameters.Add(new OleDbParameter(PRO_ShiftTable.SHIFTDESC_FLD, OleDbType.VarWChar));
					cmdPCSMonth.Parameters[PRO_ShiftTable.SHIFTDESC_FLD].Value = pstrShift;
				}

				odadData = new OleDbDataAdapter(cmdPCSMonth);
				odadData.Fill(dtbProduce);
				// add to dataset
				dstData.Tables.Add(dtbProduce);

				#endregion

				#region delivery and produce of previous month

				DateTime dtmPreviousMonth = new DateTime(pintYear, pintMonth, 1).AddMonths(-1);
				dtbDelivery = new DataTable("DeliveryPMonth");
				dtbProduce = new DataTable("ProducePMonth");
				// delivery
				strSql = "SELECT 1 AS 'Type', ITM_Category.Code AS Category, ITM_Product.Code AS PartNumber,"
					+ " ITM_Product.Description AS PartName, ITM_Product.Revision AS Model,"
					+ " FLOOR(MTR_CPO.Quantity * ((100 - ITM_Product.ScrapPercent)/100)) AS Quantity,"
					+ " ITM_Product.ProductID, ITM_Category.CategoryID, MTR_CPO.DueDate"
					//+ " MTR_CPO.StartYear, MTR_CPO.StartMonth"
					+ " FROM ITM_Product JOIN"
					+ " (SELECT ISNULL(SUM(ISNULL(MTR_CPO.Quantity, 0)), 0) Quantity, MTR_CPO.ProductID,"
					+ " MTR_CPO.DueDate"
					//					+ " DATEPART(year, MTR_CPO.DueDate) AS StartYear,"
					//					+ " DATEPART(month, MTR_CPO.DueDate) AS StartMonth"
					+ " FROM MTR_CPO "
					+ " WHERE DATEPART(year, MTR_CPO.DueDate) = " + dtmPreviousMonth.Year
					+ " AND DATEPART(month, MTR_CPO.DueDate) = " + dtmPreviousMonth.Month
					+ " GROUP BY MTR_CPO.ProductID,"
					+ " MTR_CPO.DueDate) MTR_CPO"
					//					+ " DATEPART(year, MTR_CPO.DueDate),"
					//					+ " DATEPART(month, MTR_CPO.DueDate)) MTR_CPO"
					+ " ON ITM_Product.ProductID = MTR_CPO.ProductID"
					+ " LEFT JOIN ITM_Category ON ITM_Category.CategoryID = ITM_Product.CategoryID";
				if (pstrListOfItem.Trim().Length > 0)
					strSql += " WHERE ITM_Product.ProductID IN (" + pstrListOfItem + ")";
				strSql += " ORDER BY ITM_Product.ProductID";
				odadData = new OleDbDataAdapter(strSql, oconPCS);
				odadData.Fill(dtbDelivery);
				// add to dataset
				dstData.Tables.Add(dtbDelivery);

				// produce
				strSql = "SELECT DISTINCT 2 AS 'Type', ITM_Category.Code AS Category, ITM_Product.Code AS PartNumber,"
					+ " ITM_Product.Description AS PartName, ITM_Product.Revision AS Model,"
					+ " PRO_DCPResultDetail.Quantity, ITM_Product.ProductID, ITM_Category.CategoryID,"
					+ " PRO_DCPResultDetail.WorkingDate"
					+ " FROM ITM_Product JOIN"
					+ " (SELECT ISNULL(SUM(ISNULL(PRO_DCPResultDetail.Quantity, 0)), 0) Quantity, PRO_DCPResultMaster.ProductID,"
					+ " PRO_DCPResultDetail.WorkingDate, PRO_DCPResultMaster.WorkCenterID"
					+ " FROM PRO_DCPResultDetail JOIN PRO_DCPResultMaster"
					+ " ON PRO_DCPResultMaster.DCPResultMasterID = PRO_DCPResultDetail.DCPResultMasterID"
					+ " WHERE PRO_DCPResultMaster.DCOptionMasterID = " + pintDCOptionMasterID
					+ " GROUP BY PRO_DCPResultDetail.WorkingDate,"
					+ " PRO_DCPResultMaster.WorkCenterID, ProductID) PRO_DCPResultDetail"
					+ " ON ITM_Product.ProductID = PRO_DCPResultDetail.ProductID"
					+ " JOIN MST_WorkCenter"
					+ " ON PRO_DCPResultDetail.WorkCenterID = MST_WorkCenter.WorkCenterID"
					+ " JOIN PRO_WCCapacity ON PRO_WCCapacity.WorkCenterID = MST_WorkCenter.WorkCenterID"
					+ " JOIN PRO_ShiftCapacity ON PRO_WCCapacity.WCCapacityID = PRO_ShiftCapacity.WCCapacityID"
					+ " JOIN PRO_Shift ON PRO_ShiftCapacity.ShiftID = PRO_Shift.ShiftID"
					+ " LEFT JOIN ITM_Category ON ITM_Category.CategoryID = ITM_Product.CategoryID"
					+ " WHERE MST_WorkCenter.IsMain = 1"
					+ " AND MST_WorkCenter.ProductionLineID = " + pintProductionLineID
					+ " AND DATEPART(year, PRO_DCPResultDetail.WorkingDate) = " + dtmPreviousMonth.Year
					+ " AND DATEPART(month, PRO_DCPResultDetail.WorkingDate) = " + dtmPreviousMonth.Month;
				if (pstrListOfItem.Trim().Length > 0)
					strSql += " AND ITM_Product.ProductID IN (" + pstrListOfItem + ")";
				strSql += " ORDER BY ITM_Product.ProductID";

				odadData = new OleDbDataAdapter(cmdPCSMonth);
				odadData.Fill(dtbProduce);
				// add to dataset
				dstData.Tables.Add(dtbProduce);

				#endregion

				return dstData;
			}
			catch(OleDbException ex)
			{
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
			}			
			catch (Exception ex) 
			{
				throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
			}
			finally
			{
				if (oconPCS != null)
					if (oconPCS.State != ConnectionState.Closed)
						oconPCS.Close();
			}
		}
		/// <summary>
		/// Gets Begin Net Quantity
		/// </summary>
		/// <param name="pintProductID">Product</param>
		/// <param name="pintCCNID">CCN</param>
		/// <returns>decimal</returns>
		public decimal GetBeginNetQuantity(int pintProductID, int pintCCNID)
		{
			const string METHOD_NAME = THIS + ".GetBeginNetQuantity()";
			OleDbConnection oconPCS = null;
			try
			{
				Utils objUtils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				string strSql = "SELECT ISNULL(SUM(ISNULL("
					+ IV_MasLocCacheTable.OHQUANTITY_FLD + ", 0)), 0) - ISNULL(SUM(ISNULL(" + IV_MasLocCacheTable.COMMITQUANTITY_FLD + ", 0)), 0)"
					+ " FROM " + IV_MasLocCacheTable.TABLE_NAME 
					+ " WHERE " + IV_MasLocCacheTable.PRODUCTID_FLD + "=" + pintProductID
					+ " AND " + IV_MasLocCacheTable.CCNID_FLD + "=" + pintCCNID;
				OleDbCommand cmdData = new OleDbCommand(strSql, oconPCS);
				cmdData.Connection.Open();
				object objReturn = cmdData.ExecuteScalar();

				decimal decResult = 0;
				try
				{
					decResult = decimal.Parse(objReturn.ToString());
				}
				catch{}
				return decResult;
			}
			catch(OleDbException ex)
			{
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
			}			
			catch (Exception ex) 
			{
				throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
			}
			finally
			{
				if (oconPCS != null)
					if (oconPCS.State != ConnectionState.Closed)
						oconPCS.Close();
			}
		}
		/// <summary>
		/// Gets Begin Net Quantity of a list of Item
		/// </summary>
		/// <param name="pstrProductID">List of Product</param>
		/// <param name="pstrMasterLocationIDs">List of Master Location</param>
		/// <returns>DataTable</returns>
		public DataTable GetBeginNetQuantity(string pstrProductID, string pstrMasterLocationIDs)
		{
			const string METHOD_NAME = THIS + ".GetBeginNetQuantity()";
			OleDbConnection oconPCS = null;
			try
			{
				DataTable dtbData = new DataTable();
				Utils objUtils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				string strSql = "SELECT SUM(ISNULL("
					+ IV_MasLocCacheTable.OHQUANTITY_FLD + ", 0))"
					+ " AS AvailableQuantity, " + IV_MasLocCacheTable.PRODUCTID_FLD //+ ", "
					//+ IV_MasLocCacheTable.MASTERLOCATIONID_FLD
					+ " FROM " + IV_MasLocCacheTable.TABLE_NAME 
					+ " WHERE " + IV_MasLocCacheTable.MASTERLOCATIONID_FLD + " IN (" + pstrMasterLocationIDs + ")";
				if (pstrProductID.Trim().Length > 0)
					strSql += " AND " + IV_MasLocCacheTable.PRODUCTID_FLD + " IN (" + pstrProductID + ")";
				strSql += " GROUP BY " //+ IV_MasLocCacheTable.MASTERLOCATIONID_FLD + ", "
					+ IV_MasLocCacheTable.PRODUCTID_FLD;
				OleDbDataAdapter odadData = new OleDbDataAdapter(strSql, oconPCS);
				odadData.Fill(dtbData);
				return dtbData;
			}
			catch(OleDbException ex)
			{
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
			}			
			catch (Exception ex) 
			{
				throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
			}
			finally
			{
				if (oconPCS != null)
					if (oconPCS.State != ConnectionState.Closed)
						oconPCS.Close();
			}
		}
		/// <summary>
		/// Gets all shift of a day in a work center
		/// </summary>
		/// <param name="pintWorkCenterID">Selected Work Center</param>
		/// <returns>List of Shift in selected day</returns>
		public DataTable GetShift(int pintWorkCenterID)
		{
			const string METHOD_NAME = THIS + ".GetShift()";
			OleDbConnection oconPCS = null;
			OleDbDataAdapter odadPCS = null;
			try
			{
				Utils objUtils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				string strSql = "SELECT " + PRO_ShiftTable.SHIFTDESC_FLD + ","
					+ PRO_WCCapacityTable.TABLE_NAME + "." + PRO_WCCapacityTable.BEGINDATE_FLD + ","
					+ PRO_WCCapacityTable.TABLE_NAME + "." + PRO_WCCapacityTable.ENDDATE_FLD
					+ " FROM " + PRO_ShiftTable.TABLE_NAME
					+ " JOIN " + PRO_ShiftCapacityTable.TABLE_NAME
					+ " ON " + PRO_ShiftTable.TABLE_NAME + "." + PRO_ShiftTable.SHIFTID_FLD
					+ " = " + PRO_ShiftCapacityTable.TABLE_NAME + "." + PRO_ShiftCapacityTable.SHIFTID_FLD
					+ " JOIN " + PRO_WCCapacityTable.TABLE_NAME
					+ " ON " + PRO_ShiftCapacityTable.TABLE_NAME + "." + PRO_ShiftCapacityTable.WCCAPACITYID_FLD
					+ " = " + PRO_WCCapacityTable.TABLE_NAME + "." + PRO_WCCapacityTable.WCCAPACITYID_FLD
					//+ " WHERE " + PRO_WCCapacityTable.TABLE_NAME + "." + PRO_WCCapacityTable.BEGINDATE_FLD + " <= ?"
					//+ " AND " + PRO_WCCapacityTable.TABLE_NAME + "." + PRO_WCCapacityTable.ENDDATE_FLD + " >= ?"
					+ " WHERE " + PRO_WCCapacityTable.TABLE_NAME + "." + PRO_WCCapacityTable.WORKCENTERID_FLD + " = " + pintWorkCenterID;
				OleDbCommand cmdData = new OleDbCommand(strSql, oconPCS);
				//cmdData.Parameters.Add(new OleDbParameter(PRO_WCCapacityTable.BEGINDATE_FLD, OleDbType.Date));
				//cmdData.Parameters[PRO_WCCapacityTable.BEGINDATE_FLD].Value = pdtmDate;
				//cmdData.Parameters.Add(new OleDbParameter(PRO_WCCapacityTable.ENDDATE_FLD, OleDbType.Date));
				//cmdData.Parameters[PRO_WCCapacityTable.ENDDATE_FLD].Value = pdtmDate;
				cmdData.Connection.Open();
				DataTable dtbResult = new DataTable();
				odadPCS = new OleDbDataAdapter(cmdData);
				odadPCS.Fill(dtbResult);
				return dtbResult;
			}
			catch(OleDbException ex)
			{
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
			}			
			catch (Exception ex) 
			{
				throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
			}
			finally
			{
				if (oconPCS != null)
					if (oconPCS.State != ConnectionState.Closed)
						oconPCS.Close();
			}
		}
		public DataTable GetShift()
		{
			const string METHOD_NAME = THIS + ".GetShift()";
			OleDbConnection oconPCS = null;
			OleDbDataAdapter odadPCS = null;
			try
			{
				Utils objUtils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				string strSql = "SELECT " + PRO_ShiftTable.SHIFTID_FLD + ","
					+ PRO_ShiftTable.SHIFTDESC_FLD
					+ " FROM " + PRO_ShiftTable.TABLE_NAME;
				OleDbCommand cmdData = new OleDbCommand(strSql, oconPCS);
				cmdData.Connection.Open();
				DataTable dtbResult = new DataTable(PRO_ShiftTable.TABLE_NAME);
				odadPCS = new OleDbDataAdapter(cmdData);
				odadPCS.Fill(dtbResult);
				return dtbResult;
			}
			catch(OleDbException ex)
			{
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
			}			
			catch (Exception ex) 
			{
				throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
			}
			finally
			{
				if (oconPCS != null)
					if (oconPCS.State != ConnectionState.Closed)
						oconPCS.Close();
			}
		}
		/// <summary>
		/// Gets standard capacity of work center in a day
		/// </summary>
		/// <param name="pintWorkCenterID">Selected Work Center</param>
		/// <param name="pintCCNID">CCN</param>
		/// <param name="pintProductionLineID">Production Line</param>
		/// <returns>Standard Capacity</returns>
		public DataTable GetStandardCapacity(int pintCCNID, int pintProductionLineID)
		{
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				Utils objUtils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				string strSql = "SELECT ISNULL(SUM(ISNULL(PRO_WCCapacity.Capacity, 0)), 0) AS 'Capacity',"
					+ " PRO_WCCapacity.BeginDate, PRO_WCCapacity.EndDate"
					+ " FROM PRO_WCCapacity JOIN MST_WorkCenter"
					+ " ON PRO_WCCapacity.WorkCenterID = MST_WorkCenter.WorkCenterID"
					+ " LEFT JOIN PRO_ProductionLine" 
					+ " ON MST_WorkCenter.ProductionLineID = PRO_ProductionLine.ProductionLineID"
					+ " WHERE ISNULL(MST_WorkCenter.IsMain, 0) = 1"
					+ " AND PRO_ProductionLine.ProductionLineID = " + pintProductionLineID
					+ " AND PRO_WCCapacity.CCNID = " + pintCCNID
					+ " GROUP BY PRO_WCCapacity.BeginDate, PRO_WCCapacity.EndDate";
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				DataTable dtbData = new DataTable();
				odadPCS.Fill(dtbData);
				return dtbData;
			}
			finally
			{
				if (oconPCS != null)
					if (oconPCS.State != ConnectionState.Closed)
						oconPCS.Close();
			}
		}

		/// <summary>
		/// Gets Total required capacity of a work center in a day
		/// </summary>
		/// <returns>Total Required Capacity</returns>
		public DataTable GetTotalRequiredCapacity(int pintProductionLineID, int pintCycleID, DateTime pdtmFromDate, DateTime pdtmToDate)
		{
			const string METHOD_NAME = THIS + ".GetTotalRequiredCapacity()";
			OleDbConnection oconPCS = null;
			OleDbDataAdapter odadPCS = null;
			try
			{
				Utils objUtils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				string strSql = "SELECT SUM(ISNULL(TotalSecond, 0)) AS TotalSecond, WorkingDate, DCOptionMasterID, ShiftID, ProductID"
					+ " FROM PRO_DCPResultDetail JOIN PRO_DCPResultMaster"
					+ " ON PRO_DCPResultDetail.DCPResultMasterID = PRO_DCPResultMaster.DCPResultMasterID"
					+ " JOIN MST_WorkCenter"
					+ " ON PRO_DCPResultMaster.WorkCenterID = MST_WorkCenter.WorkCenterID"
					+ " WHERE MST_WorkCenter.ProductionLineID = " + pintProductionLineID
					+ " AND DCOptionMasterID = " + pintCycleID
					+ " AND IsMain = 1"
					+ " AND WorkingDate >= ? AND WorkingDate <= ?"
					+ " AND ShiftID IS NOT NULL"
					+ " GROUP BY DCOptionMasterID, WorkingDate, ShiftID, ProductID";
				OleDbCommand cmdPCS = new OleDbCommand(strSql, oconPCS);
				cmdPCS.Parameters.Add(new OleDbParameter("FromDate", OleDbType.Date)).Value = pdtmFromDate;
				cmdPCS.Parameters.Add(new OleDbParameter("ToDate", OleDbType.Date)).Value = pdtmToDate;
				cmdPCS.Connection.Open();
				DataTable dtbTRC = new DataTable();
				odadPCS = new OleDbDataAdapter(cmdPCS);
				odadPCS.Fill(dtbTRC);
				return dtbTRC;
			}
			catch(OleDbException ex)
			{
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
			}			
			catch (Exception ex) 
			{
				throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
			}
			finally
			{
				if (oconPCS != null)
					if (oconPCS.State != ConnectionState.Closed)
						oconPCS.Close();
			}
		}
		/// <summary>
		/// Gets Total required capacity of a work center in a day
		/// </summary>
		/// <returns>Total Required Capacity</returns>
		public DataTable GetTotalRequiredCapacityByShift(int pintProductionLineID, int pintCycleID, DateTime pdtmFromDate, DateTime pdtmToDate)
		{
			const string METHOD_NAME = THIS + ".GetTotalRequiredCapacity()";
			OleDbConnection oconPCS = null;
			OleDbDataAdapter odadPCS = null;
			try
			{
				Utils objUtils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				string strSql = "SELECT SUM(ISNULL(TotalSecond, 0)) AS TotalSecond, WorkingDate, DCOptionMasterID, ShiftID"
					+ " FROM PRO_DCPResultDetail JOIN PRO_DCPResultMaster"
					+ " ON PRO_DCPResultDetail.DCPResultMasterID = PRO_DCPResultMaster.DCPResultMasterID"
					+ " JOIN MST_WorkCenter"
					+ " ON PRO_DCPResultMaster.WorkCenterID = MST_WorkCenter.WorkCenterID"
					+ " WHERE MST_WorkCenter.ProductionLineID = " + pintProductionLineID
					+ " AND DCOptionMasterID = " + pintCycleID
					+ " AND IsMain = 1"
					+ " AND WorkingDate >= ? AND WorkingDate <= ?"
					+ " AND ShiftID IS NOT NULL"
					+ " GROUP BY DCOptionMasterID, WorkingDate, ShiftID";
				OleDbCommand cmdPCS = new OleDbCommand(strSql, oconPCS);
				cmdPCS.Parameters.Add(new OleDbParameter("FromDate", OleDbType.Date)).Value = pdtmFromDate;
				cmdPCS.Parameters.Add(new OleDbParameter("ToDate", OleDbType.Date)).Value = pdtmToDate;
				cmdPCS.Connection.Open();
				DataTable dtbTRC = new DataTable();
				odadPCS = new OleDbDataAdapter(cmdPCS);
				odadPCS.Fill(dtbTRC);
				return dtbTRC;
			}
			catch(OleDbException ex)
			{
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
			}			
			catch (Exception ex) 
			{
				throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
			}
			finally
			{
				if (oconPCS != null)
					if (oconPCS.State != ConnectionState.Closed)
						oconPCS.Close();
			}
		}
		/// <summary>
		/// Gets total change category time in a work center
		/// </summary>
		/// <param name="pintYear">Selected Year</param>
		/// <param name="pintMonth">Selected Month</param>
		/// <param name="pstrAllDays">All days</param>
		/// <param name="pintWorkCenterID">Selected Work Center</param>
		/// <param name="pintDCPCycleID">DCP Cycle Option Master</param>
		/// <param name="pintProductID">Product</param>
		/// <returns>Total Change time</returns>
		public DataTable GetChangeTime(int pintYear, int pintMonth, string pstrAllDays, int pintWorkCenterID, int pintDCPCycleID, int pintProductID)
		{
			const string METHOD_NAME = THIS + ".GetChangeTime()";
			OleDbConnection oconPCS = null;
			OleDbDataAdapter odadPCS = null;
			try
			{
				Utils objUtils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				string strSql = "SELECT ISNULL(SUM(ISNULL(" + PRO_DCPResultDetailTable.TOTALSECOND_FLD + ", 0)), 0) AS " + PRO_DCPResultDetailTable.TOTALSECOND_FLD + ","
					+ PRO_DCPResultDetailTable.WORKINGDATE_FLD
					+ " FROM " + PRO_DCPResultDetailTable.TABLE_NAME
					+ " JOIN " + PRO_DCPResultMasterTable.TABLE_NAME
					+ " ON " + PRO_DCPResultDetailTable.TABLE_NAME + "." + PRO_DCPResultDetailTable.DCPRESULTMASTERID_FLD
					+ " = " + PRO_DCPResultMasterTable.TABLE_NAME + "." + PRO_DCPResultMasterTable.DCPRESULTMASTERID_FLD
					+ " WHERE DATEPART(year, " + PRO_DCPResultDetailTable.WORKINGDATE_FLD + ") = " + pintYear
					+ " AND DATEPART(month, " + PRO_DCPResultDetailTable.WORKINGDATE_FLD + ") = " + pintMonth
					+ " AND DATEPART(day, " + PRO_DCPResultDetailTable.WORKINGDATE_FLD + ") IN (" + pstrAllDays + ")"
					+ " AND " + PRO_DCPResultMasterTable.WORKCENTERID_FLD + " = " + pintWorkCenterID
					+ " AND " + PRO_DCPResultMasterTable.DCOPTIONMASTERID_FLD + " = " + pintDCPCycleID
					+ " AND PRO_DCPResultDetail.Type = " + (int)DCPResultTypeEnum.ChangeCategory;
				if (pintProductID > 0)
					strSql += " AND " + PRO_DCPResultMasterTable.PRODUCTID_FLD + " = " + pintProductID;
				strSql += " GROUP BY " + PRO_DCPResultDetailTable.WORKINGDATE_FLD;
				OleDbCommand cmdData = new OleDbCommand(strSql, oconPCS);
				cmdData.Connection.Open();
				DataTable dtbResult = new DataTable();
				odadPCS = new OleDbDataAdapter(cmdData);
				odadPCS.Fill(dtbResult);
				return dtbResult;;
			}
			catch(OleDbException ex)
			{
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
			}			
			catch (Exception ex) 
			{
				throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
			}
			finally
			{
				if (oconPCS != null)
					if (oconPCS.State != ConnectionState.Closed)
						oconPCS.Close();
			}
		}
		/// <summary>
		/// Gets total check point time in a work center
		/// </summary>
		/// <param name="pintYear">Selected Year</param>
		/// <param name="pintMonth">Selected Month</param>
		/// <param name="pstrAllDays">All days</param>
		/// <param name="pintWorkCenterID">Selected Work Center</param>
		/// <param name="pintDCPCycleID">Cycle Option</param>
		/// <param name="pintProductID">Product</param>
		/// <returns>Total Check point time</returns>
		public DataTable GetCheckpointTime(int pintYear, int pintMonth, string pstrAllDays, int pintWorkCenterID, int pintDCPCycleID, int pintProductID)
		{
			const string METHOD_NAME = THIS + ".GetCheckpointTime()";
			OleDbConnection oconPCS = null;
			OleDbDataAdapter odadPCS = null;
			try
			{
				Utils objUtils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				string strSql = "SELECT ISNULL(SUM(ISNULL(" + PRO_DCPResultDetailTable.TOTALSECOND_FLD + ", 0)), 0) AS " + PRO_DCPResultDetailTable.TOTALSECOND_FLD + ","
					+ PRO_DCPResultDetailTable.WORKINGDATE_FLD
					+ " FROM " + PRO_DCPResultDetailTable.TABLE_NAME
					+ " JOIN " + PRO_DCPResultMasterTable.TABLE_NAME
					+ " ON " + PRO_DCPResultDetailTable.TABLE_NAME + "." + PRO_DCPResultDetailTable.DCPRESULTMASTERID_FLD
					+ " = " + PRO_DCPResultMasterTable.TABLE_NAME + "." + PRO_DCPResultMasterTable.DCPRESULTMASTERID_FLD
					+ " WHERE DATEPART(year, " + PRO_DCPResultDetailTable.WORKINGDATE_FLD + ") = " + pintYear
					+ " AND DATEPART(month, " + PRO_DCPResultDetailTable.WORKINGDATE_FLD + ") = " + pintMonth
					+ " AND DATEPART(day, " + PRO_DCPResultDetailTable.WORKINGDATE_FLD + ") IN (" + pstrAllDays + ")"
					+ " AND " + PRO_DCPResultMasterTable.WORKCENTERID_FLD + " = " + pintWorkCenterID
					+ " AND " + PRO_DCPResultMasterTable.DCOPTIONMASTERID_FLD + " = " + pintDCPCycleID
					+ " AND PRO_DCPResultDetail.Type = " + (int)DCPResultTypeEnum.CheckPoint;
				if (pintProductID > 0)
					strSql += " AND " + PRO_DCPResultMasterTable.PRODUCTID_FLD + " = " + pintProductID;
				strSql += " GROUP BY " + PRO_DCPResultDetailTable.WORKINGDATE_FLD;
				OleDbCommand cmdData = new OleDbCommand(strSql, oconPCS);
				cmdData.Connection.Open();
				DataTable dtbResult = new DataTable();
				odadPCS = new OleDbDataAdapter(cmdData);
				odadPCS.Fill(dtbResult);
				return dtbResult;;
			}
			catch(OleDbException ex)
			{
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
			}			
			catch (Exception ex) 
			{
				throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
			}
			finally
			{
				if (oconPCS != null)
					if (oconPCS.State != ConnectionState.Closed)
						oconPCS.Close();
			}
		}

		/// <summary>
		/// Gets list of product has produce quantity in DCP result in Production Line
		/// </summary>
		/// <param name="pintCategoryID">Category</param>
		/// <param name="pintCCNID">CCN</param>
		/// <param name="pintProductionLineID">Production Line</param>
		/// <returns>List of Product</returns>
		public ArrayList GetProducts(int pintCategoryID, int pintCCNID, int pintProductionLineID, int pintDCOptionMasterID)
		{
			const string METHOD_NAME = THIS + ".GetProducts()";
			OleDbConnection oconPCS = null;
			OleDbDataReader odrdData = null;
			try
			{
				ArrayList arrProducts = new ArrayList();
				Utils objUtils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				string strSql = "SELECT DISTINCT PRO_DCPResultMaster.ProductID FROM PRO_DCPResultMaster"
					+ " JOIN ITM_Product"
					+ " ON PRO_DCPResultMaster.ProductID = ITM_Product.ProductID"
					+ " JOIN MST_WorkCenter"
					+ " ON PRO_DCPResultMaster.WorkCenterID = MST_WorkCenter.WorkCenterID"
					+ " WHERE ITM_Product.CCNID = " + pintCCNID
					+ " AND MST_WorkCenter.IsMain = 1"
					+ " AND PRO_DCPResultMaster.DCOptionMasterID = " + pintDCOptionMasterID
					+ " AND MST_WorkCenter.ProductionLineID = " + pintProductionLineID;
				if (pintCategoryID > 0)
					strSql += " AND ITM_Product.CategoryID = " + pintCategoryID;
				OleDbCommand cmdData = new OleDbCommand(strSql, oconPCS);
				cmdData.Connection.Open();
				odrdData = cmdData.ExecuteReader();
				while (odrdData.Read())
				{
					arrProducts.Add(odrdData.GetInt32(0));
				}
				arrProducts.TrimToSize();
				if (!odrdData.IsClosed)
				{
					odrdData.Close();
					odrdData = null;
				}
				return arrProducts;
			}
			catch(OleDbException ex)
			{
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
			}			
			catch (Exception ex) 
			{
				throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
			}
			finally
			{
				if (oconPCS != null)
					if (oconPCS.State != ConnectionState.Closed)
						oconPCS.Close();
			}
		}
		/// <summary>
		/// Gets product information
		/// </summary>
		/// <param name="pintProductID">Product ID</param>
		/// <returns>DataRow</returns>
		public DataRow GetProductInfo(int pintProductID)
		{
			const string METHOD_NAME = THIS + ".GetProductInfo()";
			OleDbConnection oconPCS = null;
			try 
			{
				string strSql =	"SELECT "
					+ "Code" + ","
					+ "Revision" + ","
					+ "Description"
					+ " FROM " + ITM_ProductTable.TABLE_NAME
					+ " WHERE " + ITM_ProductTable.PRODUCTID_FLD + " = " + pintProductID.ToString();

				Utils objUtils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				DataTable dtbData = new DataTable();
				OleDbDataAdapter odadData = new OleDbDataAdapter(strSql, oconPCS);
				odadData.Fill(dtbData);
				if (dtbData.Rows.Count > 0)
					return dtbData.Rows[0];
				else
					return null;
			}
			catch(OleDbException ex)
			{
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
			}			
			catch (Exception ex) 
			{
				throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
			}
			finally 
			{
				if (oconPCS!=null) 
				{
					if (oconPCS.State != ConnectionState.Closed) 
					{
						oconPCS.Close();
					}
				}
			}
		}
		/// <summary>
		/// Gets CCN Code
		/// </summary>
		/// <param name="pintCCNID">Product ID</param>
		/// <returns>string</returns>
		public string GetCCNCode(int pintCCNID)
		{
			const string METHOD_NAME = THIS + ".GetCCNCode()";
			OleDbConnection oconPCS = null;
			try 
			{
				string strSql =	"SELECT "
					+ MST_CCNTable.CODE_FLD
					+ " FROM " + MST_CCNTable.TABLE_NAME
					+ " WHERE " + MST_CCNTable.CCNID_FLD + " = " + pintCCNID.ToString();

				Utils objUtils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				OleDbCommand cmd = new OleDbCommand(strSql, oconPCS);
				cmd.Connection.Open();
				try
				{
					return cmd.ExecuteScalar().ToString();
				}
				catch
				{
					return string.Empty;
				}
			}
			catch(OleDbException ex)
			{
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
			}			
			catch (Exception ex) 
			{
				throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
			}
			finally 
			{
				if (oconPCS!=null) 
				{
					if (oconPCS.State != ConnectionState.Closed) 
					{
						oconPCS.Close();
					}
				}
			}
		}
		/// <summary>
		/// Gets Work Center Code
		/// </summary>
		/// <param name="pintWorkCenterID">Product ID</param>
		/// <returns>string</returns>
		public string GetWorkCenter(int pintWorkCenterID)
		{
			const string METHOD_NAME = THIS + ".GetWorkCenter()";
			OleDbConnection oconPCS = null;
			try 
			{
				string strSql =	"SELECT "
					+ MST_WorkCenterTable.CODE_FLD
					+ " FROM " + MST_WorkCenterTable.TABLE_NAME
					+ " WHERE " + MST_WorkCenterTable.WORKCENTERID_FLD + " = " + pintWorkCenterID.ToString();

				Utils objUtils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				OleDbCommand cmd = new OleDbCommand(strSql, oconPCS);
				cmd.Connection.Open();
				try
				{
					return cmd.ExecuteScalar().ToString();
				}
				catch
				{
					return string.Empty;
				}
			}
			catch(OleDbException ex)
			{
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
			}			
			catch (Exception ex) 
			{
				throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
			}
			finally 
			{
				if (oconPCS!=null) 
				{
					if (oconPCS.State != ConnectionState.Closed) 
					{
						oconPCS.Close();
					}
				}
			}
		}
		/// <summary>
		/// Gets Category code
		/// </summary>
		/// <param name="pintCategoryID">Product ID</param>
		/// <returns>string</returns>
		public string GetCategory(int pintCategoryID)
		{
			const string METHOD_NAME = THIS + ".GetCategory()";
			OleDbConnection oconPCS = null;
			try 
			{
				string strSql =	"SELECT "
					+ ITM_CategoryTable.CODE_FLD
					+ " FROM " + ITM_CategoryTable.TABLE_NAME
					+ " WHERE " + ITM_CategoryTable.CATEGORYID_FLD + " = " + pintCategoryID.ToString();

				Utils objUtils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				OleDbCommand cmd = new OleDbCommand(strSql, oconPCS);
				cmd.Connection.Open();
				try
				{
					return cmd.ExecuteScalar().ToString();
				}
				catch
				{
					return string.Empty;
				}
			}
			catch(OleDbException ex)
			{
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
			}			
			catch (Exception ex) 
			{
				throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
			}
			finally 
			{
				if (oconPCS!=null) 
				{
					if (oconPCS.State != ConnectionState.Closed) 
					{
						oconPCS.Close();
					}
				}
			}
		}
		/// <summary>
		/// Gets the total In transaction of inventory
		/// </summary>
		/// <param name="pstrItems">List of Items</param>
		/// <param name="pstrMasterLocation">List of Master Locations</param>
		/// <param name="pdtmFromDate">From Date</param>
		/// <param name="pdtmToDate">To Date</param>
		/// <returns>DataTable</returns>
		public DataTable GetTotalIn(string pstrItems, string pstrMasterLocation, DateTime pdtmFromDate, DateTime pdtmToDate)
		{
			const string METHOD_NAME = THIS + ".GetTotalIn()";
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				DataTable dtbData = new DataTable("TotalIn");
				Utils objUtils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);

				#region Build SQL String

				StringBuilder strSql = new StringBuilder();
				strSql.Append("SELECT	(SUM(ISNULL(InTransaction.IssueQuantityIn, 0))");
				strSql.Append("		+ SUM(ISNULL(InTransaction.ReversalQuantity, 0))");
				strSql.Append("		+ SUM(ISNULL(InTransaction.WOCompletedQuantity, 0))");
				strSql.Append("		+ SUM(ISNULL(InTransaction.MaterialReceiptQuantity, 0))");
				strSql.Append("		+ SUM(ISNULL(InTransaction.AdjustAddQuantity, 0))");
				strSql.Append("		+ SUM(ISNULL(InTransaction.LocToLocIn, 0))");
				strSql.Append("		+ SUM(ISNULL(InTransaction.InsResultQuantityIn, 0))");
				strSql.Append("		+ SUM(ISNULL(InTransaction.MRBResultQuantityIn, 0))");
				strSql.Append("		+ SUM(ISNULL(InTransaction.RGRQuantity, 0))");
				strSql.Append("		+ SUM(ISNULL(InTransaction.POReceiptQuantity, 0))) AS AvailableQuantity,");
				strSql.Append("		InTransaction.ProductID");
				strSql.Append(" FROM");
				strSql.Append(" /**********************************************");
				strSql.Append(" I. IN TRANSACTION");
				strSql.Append(" ***********************************************/");
				strSql.Append(" /* 1. Production Module: */");
				strSql.Append(" /* Issues Material (To Location) */");
				strSql.Append("(");
				strSql.Append(" (SELECT	CommitQuantity AS IssueQuantityIn, NULL ReversalQuantity, NULL WOCompletedQuantity,");
				strSql.Append("		NULL MaterialReceiptQuantity, NULL AdjustAddQuantity, NULL LocToLocIn,");
				strSql.Append("		NULL InsResultQuantityIn, NULL MRBResultQuantityIn, NULL RGRQuantity, NULL POReceiptQuantity,");
				strSql.Append("		PRO_IssueMaterialDetail.ProductID");
				strSql.Append("	FROM 	PRO_IssueMaterialDetail JOIN PRO_IssueMaterialMaster");
				strSql.Append("		ON PRO_IssueMaterialDetail.IssueMaterialMasterID = PRO_IssueMaterialMaster.IssueMaterialMasterID");
				strSql.Append(" WHERE 	PRO_IssueMaterialDetail.MasterLocationID IN (" + pstrMasterLocation + ")");
				if (pstrItems.Trim().Length > 0)
					strSql.Append("		AND PRO_IssueMaterialDetail.ProductID IN (" + pstrItems + ")");
				strSql.Append("		AND PRO_IssueMaterialMaster.PostDate >= ?");
				strSql.Append("		AND PRO_IssueMaterialMaster.PostDate <= ?");
				strSql.Append(" )");
				strSql.Append(" UNION");
				strSql.Append(" /* Work Order Issue Reversal (Reversal Quantity) */");
				strSql.Append(" (SELECT	NULL IssueQuantityIn, Quantity AS ReversalQuantity, NULL WOCompletedQuantity,");
				strSql.Append("		NULL MaterialReceiptQuantity, NULL AdjustAddQuantity, NULL LocToLocIn,");
				strSql.Append("		NULL InsResultQuantityIn, NULL MRBResultQuantityIn, NULL RGRQuantity, NULL POReceiptQuantity,");
				strSql.Append("		PRO_WOReversalDetail.ProductID");
				strSql.Append(" FROM 	PRO_WOReversalDetail JOIN PRO_WOReversalMaster");
				strSql.Append("		ON PRO_WOReversalDetail.WOReversalMasterID = PRO_WOReversalMaster.WOReversalMasterID");
				strSql.Append(" WHERE	PRO_WOReversalMaster.MasterLocationID IN (" + pstrMasterLocation + ")");
				if (pstrItems.Trim().Length > 0)
					strSql.Append("		AND PRO_WOReversalDetail.ProductID IN (" + pstrItems + ")");
				strSql.Append("		AND PRO_WOReversalMaster.PostDate >= ?");
				strSql.Append("		AND PRO_WOReversalMaster.PostDate <= ?");
				strSql.Append(" )");
				strSql.Append(" UNION");
				strSql.Append(" /* Work Order Completion (Completed Quantity) */");
				strSql.Append(" (SELECT	NULL IssueQuantityIn, NULL ReversalQuantity, CompletedQuantity AS WOCompletedQuantity,");
				strSql.Append("		NULL MaterialReceiptQuantity, NULL AdjustAddQuantity, NULL LocToLocIn,");
				strSql.Append("		NULL InsResultQuantityIn, NULL MRBResultQuantityIn, NULL RGRQuantity, NULL POReceiptQuantity,");
				strSql.Append("		PRO_WorkOrderCompletion.ProductID");
				strSql.Append(" FROM	PRO_WorkOrderCompletion");
				strSql.Append(" WHERE	PRO_WorkOrderCompletion.MasterLocationID IN (" + pstrMasterLocation + ")");
				if (pstrItems.Trim().Length > 0)
					strSql.Append("		AND PRO_WorkOrderCompletion.ProductID IN (" + pstrItems + ")");
				strSql.Append("		AND PRO_WorkOrderCompletion.PostDate >= ?");
				strSql.Append("		AND PRO_WorkOrderCompletion.PostDate <= ?");
				strSql.Append(" )");
				strSql.Append(" UNION");
				strSql.Append(" /* 2. Material Module: */");
				strSql.Append(" /* Material Receipts (Receive Quantity) */");
				strSql.Append(" (SELECT	NULL IssueQuantityIn, NULL ReversalQuantity, NULL WOCompletedQuantity,");
				strSql.Append("		Quantity AS MaterialReceiptQuantity, NULL AdjustAddQuantity, NULL LocToLocIn,");
				strSql.Append("		NULL InsResultQuantityIn, NULL MRBResultQuantityIn, NULL RGRQuantity, NULL POReceiptQuantity,");
				strSql.Append("		IV_MaterialReceipt.ProductID");
				strSql.Append(" FROM	IV_MaterialReceipt");
				strSql.Append(" WHERE	IV_MaterialReceipt.MasterLocationID IN (" + pstrMasterLocation + ")");
				if (pstrItems.Trim().Length > 0)
					strSql.Append("		AND IV_MaterialReceipt.ProductID IN (" + pstrItems + ")");
				strSql.Append("		AND IV_MaterialReceipt.PostDate >= ?");
				strSql.Append("		AND IV_MaterialReceipt.PostDate <= ?");
				strSql.Append(" )");
				strSql.Append(" UNION");
				strSql.Append(" /* Inventory Adjustment (Add Quantity) */");
				strSql.Append(" (SELECT	NULL IssueQuantityIn, NULL ReversalQuantity, NULL WOCompletedQuantity,");
				strSql.Append("		NULL MaterialReceiptQuantity, IV_Adjustment.AdjustQuantity AS AdjustAddQuantity, NULL LocToLocIn,");
				strSql.Append("		NULL InsResultQuantityIn, NULL MRBResultQuantityIn, NULL RGRQuantity, NULL POReceiptQuantity,");
				strSql.Append("		IV_Adjustment.ProductID");
				strSql.Append(" FROM	IV_Adjustment");
				strSql.Append(" WHERE	IV_Adjustment.MasterLocationID IN (" + pstrMasterLocation + ")");
				if (pstrItems.Trim().Length > 0)
					strSql.Append("		AND IV_Adjustment.ProductID IN (" + pstrItems + ")");
				strSql.Append("		AND IV_Adjustment.PostDate >= ?");
				strSql.Append("		AND IV_Adjustment.PostDate <= ?");
				strSql.Append("		AND IV_Adjustment.AdjustQuantity > 0");
				strSql.Append(" )");
				strSql.Append(" UNION");
				strSql.Append(" /* Location to Location Transfer (Destination Loc) */");
				strSql.Append(" (SELECT	NULL IssueQuantityIn, NULL ReversalQuantity, NULL WOCompletedQuantity,");
				strSql.Append("		NULL MaterialReceiptQuantity, NULL AdjustAddQuantity, TransferQuantity AS LocToLocIn,");
				strSql.Append("		NULL InsResultQuantityIn, NULL MRBResultQuantityIn, NULL RGRQuantity, NULL POReceiptQuantity,");
				strSql.Append("		IV_LocToLocTransferDetail.ProductID");
				strSql.Append(" FROM	IV_LocToLocTransferDetail JOIN IV_LocToLocTransferMaster");
				strSql.Append("		ON IV_LocToLocTransferDetail.LocToLocTransferMasterID = IV_LocToLocTransferMaster.LocToLocTransferMasterID");
				strSql.Append(" WHERE	IV_LocToLocTransferMaster.DesMasLocationID IN (" + pstrMasterLocation + ")");
				if (pstrItems.Trim().Length > 0)
					strSql.Append("		AND IV_LocToLocTransferDetail.ProductID IN (" + pstrItems + ")");
				strSql.Append("		AND IV_LocToLocTransferMaster.PostDate >= ?");
				strSql.Append("		AND IV_LocToLocTransferMaster.PostDate <= ?");
				strSql.Append(" )");
				strSql.Append(" UNION");
				strSql.Append(" /* Inspection Results (Accepted/Rejected Quantity) */");
				strSql.Append(" (SELECT	NULL IssueQuantityIn, NULL ReversalQuantity, NULL WOCompletedQuantity,");
				strSql.Append("		NULL MaterialReceiptQuantity, NULL AdjustAddQuantity, NULL LocToLocIn,");
				strSql.Append("		(ISNULL(AcceptedQuantity, 0) + ISNULL(RejectedQuantity, 0)) AS InsResultQuantityIn,");
				strSql.Append("		NULL MRBResultQuantityIn, NULL RGRQuantity, NULL POReceiptQuantity,");
				strSql.Append("		IV_INSResult.ProductID");
				strSql.Append(" FROM IV_INSResult");
				strSql.Append(" WHERE	IV_INSResult.MasterLocationID IN (" + pstrMasterLocation + ")");
				if (pstrItems.Trim().Length > 0)
					strSql.Append("		AND IV_INSResult.ProductID IN (" + pstrItems + ")");
				strSql.Append("		AND IV_INSResult.PostDate >= ?");
				strSql.Append("		AND IV_INSResult.PostDate <= ?");
				strSql.Append(" )");
				strSql.Append(" UNION");
				strSql.Append(" /* MRB Result (Disposition Quantity) */");
				strSql.Append(" (SELECT	NULL IssueQuantityIn, NULL ReversalQuantity, NULL WOCompletedQuantity,");
				strSql.Append("		NULL MaterialReceiptQuantity, NULL AdjustAddQuantity, NULL LocToLocIn,");
				strSql.Append("		NULL InsResultQuantityIn,");
				strSql.Append("		(ISNULL(UseAsIsQuantity, 0) + ISNULL(RTVQuantity, 0)");
				strSql.Append("		+ ISNULL(RTVReplaceQuantity, 0) + ISNULL(RTVReworkQuantity, 0)");
				strSql.Append("		+ ISNULL(ReworkPurQuantity, 0) + ISNULL(ReworkMfgQuantity, 0)");
				strSql.Append("		+ ISNULL(ScrapQuantity, 0)) AS MRBResultQuantityIn,");
				strSql.Append("		NULL RGRQuantity, NULL POReceiptQuantity,");
				strSql.Append("		IV_MRBResult.ProductID");
				strSql.Append(" FROM IV_MRBResult");
				strSql.Append(" WHERE IV_MRBResult.MasterLocationID IN (" + pstrMasterLocation + ")");
				if (pstrItems.Trim().Length > 0)
					strSql.Append("		AND IV_MRBResult.ProductID IN (" + pstrItems + ")");
				strSql.Append("		AND IV_MRBResult.PostDate >= ?");
				strSql.Append("		AND IV_MRBResult.PostDate <= ?");
				strSql.Append(" )");
				strSql.Append(" UNION");
				strSql.Append(" /* 3. Sales: */");
				strSql.Append(" /* Return Goods Receipt */");
				strSql.Append(" (SELECT	NULL IssueQuantityIn, NULL ReversalQuantity, NULL WOCompletedQuantity,");
				strSql.Append("		NULL MaterialReceiptQuantity, NULL AdjustAddQuantity, NULL LocToLocIn,");
				strSql.Append("		NULL InsResultQuantityIn, NULL MRBResultQuantityIn,");
				strSql.Append("		ISNULL(SO_ReturnedGoodsDetail.ReceiveQuantity,0) RGRQuantity, NULL POReceiptQuantity,");
				strSql.Append("		SO_ReturnedGoodsDetail.ProductID");
				strSql.Append(" FROM SO_ReturnedGoodsDetail JOIN SO_ReturnedGoodsMaster");
				strSql.Append("		ON SO_ReturnedGoodsDetail.ReturnedGoodsMasterID = SO_ReturnedGoodsMaster.ReturnedGoodsMasterID");
				strSql.Append(" WHERE 	SO_ReturnedGoodsMaster.PostDate >= ?");
				strSql.Append("		AND SO_ReturnedGoodsMaster.PostDate <= ?");
				strSql.Append("		AND SO_ReturnedGoodsMaster.MasterLocationID IN (" + pstrMasterLocation + ")");
				if (pstrItems.Trim().Length > 0)
					strSql.Append("		AND SO_ReturnedGoodsDetail.ProductID IN (" + pstrItems + ")");
				strSql.Append(" )");
				strSql.Append(" UNION");
				strSql.Append(" /* 4. Procurement: */");
				strSql.Append(" /* Purchase Order Receipts */");
				strSql.Append(" (SELECT	NULL IssueQuantityIn, NULL ReversalQuantity, NULL WOCompletedQuantity,");
				strSql.Append("		NULL MaterialReceiptQuantity, NULL AdjustAddQuantity, NULL LocToLocIn,");
				strSql.Append("		NULL InsResultQuantityIn, NULL MRBResultQuantityIn,");
				strSql.Append("		NULL RGRQuantity, ISNULL(PO_PurchaseOrderReceiptDetail.ReceiveQuantity, 0) AS POReceiptQuantity,");
				strSql.Append("		PO_PurchaseOrderReceiptDetail.ProductID");
				strSql.Append(" FROM 	PO_PurchaseOrderReceiptDetail JOIN PO_PurchaseOrderReceiptMaster");
				strSql.Append("		ON PO_PurchaseOrderReceiptDetail.PurchaseOrderReceiptID = PO_PurchaseOrderReceiptMaster.PurchaseOrderReceiptID");
				strSql.Append(" WHERE	PO_PurchaseOrderReceiptMaster.MasterLocationID IN (" + pstrMasterLocation + ")");
				if (pstrItems.Trim().Length > 0)
					strSql.Append("		AND PO_PurchaseOrderReceiptDetail.ProductID IN (" + pstrItems + ")");
				strSql.Append("		AND PO_PurchaseOrderReceiptMaster.PostDate >= ?");
				strSql.Append("		AND PO_PurchaseOrderReceiptMaster.PostDate <= ?");
				strSql.Append(" )");
				strSql.Append(" ) AS InTransaction");
				strSql.Append(" GROUP BY InTransaction.ProductID");

				#endregion

				ocmdPCS = new OleDbCommand(strSql.ToString(), oconPCS);

				#region Add parameters

				ocmdPCS.Parameters.Add(new OleDbParameter("IssueInFromDate", OleDbType.Date));
				ocmdPCS.Parameters["IssueInFromDate"].Value = pdtmFromDate;
				ocmdPCS.Parameters.Add(new OleDbParameter("IssueInToDate", OleDbType.Date));
				ocmdPCS.Parameters["IssueInToDate"].Value = pdtmToDate;

				ocmdPCS.Parameters.Add(new OleDbParameter("ReversalFromDate", OleDbType.Date));
				ocmdPCS.Parameters["ReversalFromDate"].Value = pdtmFromDate;
				ocmdPCS.Parameters.Add(new OleDbParameter("ReversalToDate", OleDbType.Date));
				ocmdPCS.Parameters["ReversalToDate"].Value = pdtmToDate;

				ocmdPCS.Parameters.Add(new OleDbParameter("WOCompletedFromDate", OleDbType.Date));
				ocmdPCS.Parameters["WOCompletedFromDate"].Value = pdtmFromDate;
				ocmdPCS.Parameters.Add(new OleDbParameter("WOCompletedToDate", OleDbType.Date));
				ocmdPCS.Parameters["WOCompletedToDate"].Value = pdtmToDate;

				ocmdPCS.Parameters.Add(new OleDbParameter("MaterialReceiptFromDate", OleDbType.Date));
				ocmdPCS.Parameters["MaterialReceiptFromDate"].Value = pdtmFromDate;
				ocmdPCS.Parameters.Add(new OleDbParameter("MaterialReceiptToDate", OleDbType.Date));
				ocmdPCS.Parameters["MaterialReceiptToDate"].Value = pdtmToDate;

				ocmdPCS.Parameters.Add(new OleDbParameter("AdjustInFromDate", OleDbType.Date));
				ocmdPCS.Parameters["AdjustInFromDate"].Value = pdtmFromDate;
				ocmdPCS.Parameters.Add(new OleDbParameter("AdjustInToDate", OleDbType.Date));
				ocmdPCS.Parameters["AdjustInToDate"].Value = pdtmToDate;

				ocmdPCS.Parameters.Add(new OleDbParameter("LocToLocInFromDate", OleDbType.Date));
				ocmdPCS.Parameters["LocToLocInFromDate"].Value = pdtmFromDate;
				ocmdPCS.Parameters.Add(new OleDbParameter("LocToLocInToDate", OleDbType.Date));
				ocmdPCS.Parameters["LocToLocInToDate"].Value = pdtmToDate;

				ocmdPCS.Parameters.Add(new OleDbParameter("INSResultInFromDate", OleDbType.Date));
				ocmdPCS.Parameters["INSResultInFromDate"].Value = pdtmFromDate;
				ocmdPCS.Parameters.Add(new OleDbParameter("INSResultInToDate", OleDbType.Date));
				ocmdPCS.Parameters["INSResultInToDate"].Value = pdtmToDate;

				ocmdPCS.Parameters.Add(new OleDbParameter("MRBResultInFromDate", OleDbType.Date));
				ocmdPCS.Parameters["MRBResultInFromDate"].Value = pdtmFromDate;
				ocmdPCS.Parameters.Add(new OleDbParameter("MRBResultInToDate", OleDbType.Date));
				ocmdPCS.Parameters["MRBResultInToDate"].Value = pdtmToDate;

				ocmdPCS.Parameters.Add(new OleDbParameter("RetunGoodsInFromDate", OleDbType.Date));
				ocmdPCS.Parameters["RetunGoodsInFromDate"].Value = pdtmFromDate;
				ocmdPCS.Parameters.Add(new OleDbParameter("RetunGoodsToDate", OleDbType.Date));
				ocmdPCS.Parameters["RetunGoodsToDate"].Value = pdtmToDate;

				ocmdPCS.Parameters.Add(new OleDbParameter("POReceiptsFromDate", OleDbType.Date));
				ocmdPCS.Parameters["POReceiptsFromDate"].Value = pdtmFromDate;
				ocmdPCS.Parameters.Add(new OleDbParameter("POReceiptsToDate", OleDbType.Date));
				ocmdPCS.Parameters["POReceiptsToDate"].Value = pdtmToDate;

				#endregion

				ocmdPCS.Connection.Open();

				OleDbDataAdapter odadData = new OleDbDataAdapter(ocmdPCS);
				odadData.Fill(dtbData);
				return dtbData;
			}
			catch(OleDbException ex)
			{
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
			}			
			catch (Exception ex) 
			{
				throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
			}
			finally
			{
				if (oconPCS != null)
					if (oconPCS.State != ConnectionState.Closed)
						oconPCS.Close();
			}
		}
		/// <summary>
		/// Gets the total Out transaction of inventory
		/// </summary>
		/// <param name="pstrItems">List of Items</param>
		/// <param name="pstrMasterLocation">List of Master Locations</param>
		/// <param name="pdtmFromDate">From Date</param>
		/// <param name="pdtmToDate">To Date</param>
		/// <returns>DataTable</returns>
		public DataTable GetTotalOut(string pstrItems, string pstrMasterLocation, DateTime pdtmFromDate, DateTime pdtmToDate)
		{
			const string METHOD_NAME = THIS + ".GetTotalOut()";
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				DataTable dtbData = new DataTable("TotalIn");
				Utils objUtils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);

				#region Build SQL String

				StringBuilder strSql = new StringBuilder();
				strSql.Append("SELECT	SUM(ISNULL(OutTransaction.IssueQuantityOut, 0))");
				strSql.Append(" + SUM(ISNULL(OutTransaction.AdjustOutQuantity, 0))");
				strSql.Append(" + SUM(ISNULL(OutTransaction.LocToLocOut, 0))");
				strSql.Append(" + SUM(ISNULL(OutTransaction.InsResultQuantityOut, 0))");
				strSql.Append(" + SUM(ISNULL(OutTransaction.MRBResultQuantityOut, 0))");
				strSql.Append(" + SUM(ISNULL(OutTransaction.ShippedQuantity, 0))");
				strSql.Append(" + SUM(ISNULL(OutTransaction.RGVQuantity, 0)) AS AvailableQuantity,");
				strSql.Append(" OutTransaction.ProductID");
				strSql.Append(" FROM");
				strSql.Append(" /**********************************************");
				strSql.Append(" II. OUT TRANSACTION");
				strSql.Append(" ***********************************************/");
				strSql.Append(" /* 1. Production Module: */");
				strSql.Append(" /* Issues Material (From Location) */");
				strSql.Append("(");
				strSql.Append("(SELECT CommitQuantity AS IssueQuantityOut,");
				strSql.Append(" NULL AdjustOutQuantity, NULL LocToLocOut,");
				strSql.Append(" NULL InsResultQuantityOut, NULL MRBResultQuantityOut,");
				strSql.Append(" NULL ShippedQuantity, NULL RGVQuantity,");
				strSql.Append(" PRO_IssueMaterialDetail.ProductID");
				strSql.Append(" FROM 	PRO_IssueMaterialDetail JOIN PRO_IssueMaterialMaster");
				strSql.Append(" ON PRO_IssueMaterialDetail.IssueMaterialMasterID = PRO_IssueMaterialMaster.IssueMaterialMasterID");
				strSql.Append(" WHERE 	PRO_IssueMaterialDetail.MasterLocationID IN (" + pstrMasterLocation + ")");
				if (pstrItems.Trim().Length > 0)
					strSql.Append(" AND PRO_IssueMaterialDetail.ProductID IN (" + pstrItems + ")");
				strSql.Append(" AND PRO_IssueMaterialMaster.PostDate >= ?");
				strSql.Append(" AND PRO_IssueMaterialMaster.PostDate <= ?");
				strSql.Append(" )");
				strSql.Append(" UNION");
				strSql.Append(" /* Inventory Adjustment (Subtract Quantity) */");
				strSql.Append("(SELECT	NULL IssueQuantityOut,");
				strSql.Append(" ABS(ISNULL(IV_Adjustment.AdjustQuantity, 0)) AS AdjustOutQuantity,");
				strSql.Append(" NULL LocToLocOut, NULL InsResultQuantityOut, NULL MRBResultQuantityOut,");
				strSql.Append(" NULL ShippedQuantity, NULL RGVQuantity,");
				strSql.Append(" IV_Adjustment.ProductID");
				strSql.Append(" FROM	IV_Adjustment");
				strSql.Append(" WHERE	IV_Adjustment.MasterLocationID IN (" + pstrMasterLocation + ")");
				if (pstrItems.Trim().Length > 0)
					strSql.Append(" AND IV_Adjustment.ProductID IN (" + pstrItems + ")");
				strSql.Append(" AND IV_Adjustment.PostDate >= ?");
				strSql.Append(" AND IV_Adjustment.PostDate <= ?");
				strSql.Append(" AND IV_Adjustment.AdjustQuantity < 0");
				strSql.Append(" )");
				strSql.Append(" UNION");
				strSql.Append(" /* Location to Location Transfer (Source Loc) */");
				strSql.Append("(SELECT	NULL IssueQuantityOut,");
				strSql.Append(" NULL AdjustOutQuantity,");
				strSql.Append(" ISNULL(TransferQuantity, 0) AS LocToLocOut,");
				strSql.Append(" NULL InsResultQuantityOut, NULL MRBResultQuantityOut,");
				strSql.Append(" NULL ShippedQuantity, NULL RGVQuantity,");
				strSql.Append(" IV_LocToLocTransferDetail.ProductID");
				strSql.Append(" FROM	IV_LocToLocTransferDetail JOIN IV_LocToLocTransferMaster");
				strSql.Append(" ON IV_LocToLocTransferDetail.LocToLocTransferMasterID = IV_LocToLocTransferMaster.LocToLocTransferMasterID");
				strSql.Append(" WHERE	IV_LocToLocTransferMaster.SourceMasLocationID IN (" + pstrMasterLocation + ")");
				if (pstrItems.Trim().Length > 0)
					strSql.Append(" AND IV_LocToLocTransferDetail.ProductID IN (" + pstrItems + ")");
				strSql.Append(" AND IV_LocToLocTransferMaster.PostDate >= ?");
				strSql.Append(" AND IV_LocToLocTransferMaster.PostDate <= ?");
				strSql.Append(" )");
				strSql.Append(" UNION");
				strSql.Append(" /* Inspection Results (Inspection Quantity From Source Location) */");
				strSql.Append(" (SELECT	NULL IssueQuantityOut, NULL AdjustOutQuantity, NULL AS LocToLocOut,");
				strSql.Append(" (ISNULL(AcceptedQuantity, 0) + ISNULL(RejectedQuantity, 0)) AS InsResultQuantityOut,");
				strSql.Append(" NULL MRBResultQuantityOut, NULL ShippedQuantity, NULL RGVQuantity,");
				strSql.Append(" IV_INSResult.ProductID");
				strSql.Append(" FROM IV_INSResult");
				strSql.Append(" WHERE	IV_INSResult.MasterLocationID IN (" + pstrMasterLocation + ")");
				if (pstrItems.Trim().Length > 0)
					strSql.Append(" AND IV_INSResult.ProductID IN (" + pstrItems + ")");
				strSql.Append(" AND IV_INSResult.PostDate >= ?");
				strSql.Append(" AND IV_INSResult.PostDate <= ?");
				strSql.Append(" )");
				strSql.Append(" UNION");
				strSql.Append(" /* MRB Result (Disposition Quantity) */");
				strSql.Append(" (SELECT	NULL IssueQuantityOut, NULL AdjustOutQuantity, NULL LocToLocOut, NULL InsResultQuantityOut,");
				strSql.Append(" (ISNULL(UseAsIsQuantity, 0) + ISNULL(RTVQuantity, 0)");
				strSql.Append(" + ISNULL(RTVReplaceQuantity, 0) + ISNULL(RTVReworkQuantity, 0)");
				strSql.Append(" + ISNULL(ReworkPurQuantity, 0) + ISNULL(ReworkMfgQuantity, 0)");
				strSql.Append(" + ISNULL(ScrapQuantity, 0)) AS MRBResultQuantityOut,");
				strSql.Append(" NULL ShippedQuantity, NULL RGVQuantity,");
				strSql.Append(" IV_MRBResult.ProductID");
				strSql.Append(" FROM IV_MRBResult");
				strSql.Append(" WHERE IV_MRBResult.MasterLocationID IN (" + pstrMasterLocation + ")");
				if (pstrItems.Trim().Length > 0)
					strSql.Append(" AND IV_MRBResult.ProductID IN (" + pstrItems + ")");
				strSql.Append(" AND IV_MRBResult.PostDate >= ?");
				strSql.Append(" AND IV_MRBResult.PostDate <= ?");
				strSql.Append(" )");
				strSql.Append(" UNION");
				strSql.Append(" /* 3. Sales: */");
				strSql.Append(" /* Confirm Shipment */");
				strSql.Append(" (SELECT	NULL IssueQuantityOut, NULL AdjustOutQuantity, NULL LocToLocOut,");
				strSql.Append(" NULL InsResultQuantityOut, NULL MRBResultQuantityOut,");
				strSql.Append(" ISNULL(SO_CommitInventoryDetail.CommitQuantity, 0) AS ShippedQuantity, NULL RGVQuantity,");
				strSql.Append(" SO_CommitInventoryDetail.ProductID");
				strSql.Append(" FROM	SO_CommitInventoryDetail");
				strSql.Append(" WHERE 	SO_CommitInventoryDetail.ShipDate >= ?");
				strSql.Append(" AND SO_CommitInventoryDetail.ShipDate <= ?");
				strSql.Append(" AND SO_CommitInventoryDetail.MasterLocationID IN (" + pstrMasterLocation + ")");
				if (pstrItems.Trim().Length > 0)
					strSql.Append(" AND SO_CommitInventoryDetail.ProductID IN (" + pstrItems + ")");
				strSql.Append(" AND ISNULL(SO_CommitInventoryDetail.Shipped, 0) = 1");
				strSql.Append(" )");
				strSql.Append(" UNION");
				strSql.Append(" /* 4. Procurement: */");
				strSql.Append(" /* Return to Vendor */");
				strSql.Append(" (SELECT	NULL IssueQuantityOut, NULL AdjustOutQuantity, NULL LocToLocOut,");
				strSql.Append(" NULL InsResultQuantityOut, NULL MRBResultQuantityOut, NULL ShippedQuantity,");
				strSql.Append(" ISNULL(PO_ReturnToVendorDetail.Quantity, 0) AS RGVQuantity,");
				strSql.Append(" PO_ReturnToVendorDetail.ProductID");
				strSql.Append(" FROM	PO_ReturnToVendorDetail JOIN PO_ReturnToVendorMaster");
				strSql.Append(" ON PO_ReturnToVendorDetail.ReturnToVendorMasterID = PO_ReturnToVendorMaster.ReturnToVendorMasterID");
				strSql.Append(" WHERE	PO_ReturnToVendorMaster.MasterLocationID IN (" + pstrMasterLocation + ")");
				if (pstrItems.Trim().Length > 0)
					strSql.Append(" AND PO_ReturnToVendorDetail.ProductID IN (" + pstrItems + ")");
				strSql.Append(" AND PO_ReturnToVendorMaster.PostDate >= ?");
				strSql.Append(" AND PO_ReturnToVendorMaster.PostDate <= ?");
				strSql.Append(" )");
				strSql.Append(" ) AS OutTransaction");
				strSql.Append(" GROUP BY OutTransaction.ProductID");

				#endregion

				ocmdPCS = new OleDbCommand(strSql.ToString(), oconPCS);

				#region Add Paramters

				ocmdPCS.Parameters.Add(new OleDbParameter("IssueOutFromDate", OleDbType.Date));
				ocmdPCS.Parameters["IssueOutFromDate"].Value = pdtmFromDate;
				ocmdPCS.Parameters.Add(new OleDbParameter("IssueOutToDate", OleDbType.Date));
				ocmdPCS.Parameters["IssueOutToDate"].Value = pdtmToDate;

				ocmdPCS.Parameters.Add(new OleDbParameter("AdjustOutFromDate", OleDbType.Date));
				ocmdPCS.Parameters["AdjustOutFromDate"].Value = pdtmFromDate;
				ocmdPCS.Parameters.Add(new OleDbParameter("AdjustOutToDate", OleDbType.Date));
				ocmdPCS.Parameters["AdjustOutToDate"].Value = pdtmToDate;

				ocmdPCS.Parameters.Add(new OleDbParameter("LocToLocOutFromDate", OleDbType.Date));
				ocmdPCS.Parameters["LocToLocOutFromDate"].Value = pdtmFromDate;
				ocmdPCS.Parameters.Add(new OleDbParameter("LocToLocOutToDate", OleDbType.Date));
				ocmdPCS.Parameters["LocToLocOutToDate"].Value = pdtmToDate;

				ocmdPCS.Parameters.Add(new OleDbParameter("INSResultOutFromDate", OleDbType.Date));
				ocmdPCS.Parameters["INSResultOutFromDate"].Value = pdtmFromDate;
				ocmdPCS.Parameters.Add(new OleDbParameter("INSResultOutToDate", OleDbType.Date));
				ocmdPCS.Parameters["INSResultOutToDate"].Value = pdtmToDate;

				ocmdPCS.Parameters.Add(new OleDbParameter("MRBResultOutFromDate", OleDbType.Date));
				ocmdPCS.Parameters["MRBResultOutFromDate"].Value = pdtmFromDate;
				ocmdPCS.Parameters.Add(new OleDbParameter("MRBResultOutToDate", OleDbType.Date));
				ocmdPCS.Parameters["MRBResultOutToDate"].Value = pdtmToDate;

				ocmdPCS.Parameters.Add(new OleDbParameter("ShippedFromDate", OleDbType.Date));
				ocmdPCS.Parameters["ShippedFromDate"].Value = pdtmFromDate;
				ocmdPCS.Parameters.Add(new OleDbParameter("ShippedToDate", OleDbType.Date));
				ocmdPCS.Parameters["ShippedToDate"].Value = pdtmToDate;

				ocmdPCS.Parameters.Add(new OleDbParameter("ReturnToVendorFromDate", OleDbType.Date));
				ocmdPCS.Parameters["ReturnToVendorFromDate"].Value = pdtmFromDate;
				ocmdPCS.Parameters.Add(new OleDbParameter("ReturnToVendorToDate", OleDbType.Date));
				ocmdPCS.Parameters["ReturnToVendorToDate"].Value = pdtmToDate;

				#endregion

				ocmdPCS.Connection.Open();

				OleDbDataAdapter odadData = new OleDbDataAdapter(ocmdPCS);
				odadData.Fill(dtbData);
				return dtbData;
			}
			catch(OleDbException ex)
			{
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
			}			
			catch (Exception ex) 
			{
				throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
			}
			finally
			{
				if (oconPCS != null)
					if (oconPCS.State != ConnectionState.Closed)
						oconPCS.Close();
			}
		}

		/// <summary>
		/// Get all sale orders in the period of time
		/// </summary>
		/// <param name="pstrItems">List of Items</param>
		/// <param name="pstrMasterLocation">List of Master Location</param>
		/// <param name="pdtmFromDate">From Date</param>
		/// <param name="pdtmToDate">To Date</param>
		/// <returns>List of Sale Orders</returns>
		public DataTable GetTotalSO(string pstrItems, string pstrMasterLocation, DateTime pdtmFromDate, DateTime pdtmToDate)
		{
			const string METHOD_NAME = THIS + ".GetTotalSO()";
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				DataTable dtbData = new DataTable();
				Utils objUtils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				string strSql = "SELECT	SUM(ISNULL(SO_DeliverySchedule.DeliveryQuantity, 0)) AS AvailableQuantity, SO_SaleOrderDetail.ProductID"
					+ " FROM	SO_DeliverySchedule"
					+ " JOIN SO_SaleOrderDetail"
					+ " ON SO_DeliverySchedule.SaleOrderDetailID = SO_SaleOrderDetail.SaleOrderDetailID"
					+ " JOIN SO_SaleOrderMaster"
					+ " ON SO_SaleOrderDetail.SaleOrderMasterID = SO_SaleOrderMaster.SaleOrderMasterID"
					+ " WHERE	SO_DeliverySchedule.ScheduleDate >= ?"
					+ " AND SO_DeliverySchedule.ScheduleDate <= ?";
				if (pstrItems.Trim().Length > 0)
                    strSql += " AND SO_SaleOrderDetail.ProductID IN (" + pstrItems + ")";
				strSql += " AND SO_SaleOrderMaster.ShipFromLocID IN (" + pstrMasterLocation + ")"
					+ " GROUP BY SO_SaleOrderDetail.ProductID";

				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Parameters.Add(new OleDbParameter("FromDate", OleDbType.Date));
				ocmdPCS.Parameters["FromDate"].Value = pdtmFromDate;
				ocmdPCS.Parameters.Add(new OleDbParameter("ToDate", OleDbType.Date));
				ocmdPCS.Parameters["ToDate"].Value = pdtmToDate;

				ocmdPCS.Connection.Open();

				OleDbDataAdapter odadData = new OleDbDataAdapter(ocmdPCS);
				odadData.Fill(dtbData);
				return dtbData;
			}
			catch(OleDbException ex)
			{
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
			}			
			catch (Exception ex) 
			{
				throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
			}
			finally
			{
				if (oconPCS != null)
					if (oconPCS.State != ConnectionState.Closed)
						oconPCS.Close();
			}
		}
		/// <summary>
		/// Get all purchase orders in the period of time
		/// </summary>
		/// <param name="pstrItems">List of Items</param>
		/// <param name="pstrMasterLocation">List of Master Location</param>
		/// <param name="pdtmFromDate">From Date</param>
		/// <param name="pdtmToDate">To Date</param>
		/// <returns>List of Purchase Orders</returns>
		public DataTable GetTotalPO(string pstrItems, string pstrMasterLocation, DateTime pdtmFromDate, DateTime pdtmToDate)
		{
			const string METHOD_NAME = THIS + ".GetTotalPO()";
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				DataTable dtbData = new DataTable();
				Utils objUtils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				string strSql = "SELECT	SUM(ISNULL(PO_DeliverySchedule.DeliveryQuantity, 0)) AS AvailableQuantity, PO_PurchaseOrderDetail.ProductID"
					+ " FROM	PO_DeliverySchedule"
					+ " JOIN PO_PurchaseOrderDetail"
					+ " ON PO_DeliverySchedule.PurchaseOrderDetailID = PO_PurchaseOrderDetail.PurchaseOrderDetailID"
					+ " JOIN PO_PurchaseOrderMaster"
					+ " ON PO_PurchaseOrderDetail.PurchaseOrderMasterID = PO_PurchaseOrderMaster.PurchaseOrderMasterID"
					+ " WHERE	PO_DeliverySchedule.ScheduleDate >= ?"
					+ " AND PO_DeliverySchedule.ScheduleDate <= ?";
				if (pstrItems.Trim().Length > 0)
					strSql += " AND PO_PurchaseOrderDetail.ProductID IN (" + pstrItems + ")";
				strSql += " AND ISNULL(PO_PurchaseOrderDetail.ApproverID, 0) > 0"
					+ " AND ISNULL(PO_PurchaseOrderDetail.Closed, 0) = 0"
					+ " AND PO_PurchaseOrderMaster.MasterLocationID IN (" + pstrMasterLocation + ")"
					+ " GROUP BY PO_PurchaseOrderDetail.ProductID";

				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Parameters.Add(new OleDbParameter("FromDate", OleDbType.Date));
				ocmdPCS.Parameters["FromDate"].Value = pdtmFromDate;
				ocmdPCS.Parameters.Add(new OleDbParameter("ToDate", OleDbType.Date));
				ocmdPCS.Parameters["ToDate"].Value = pdtmToDate;

				ocmdPCS.Connection.Open();

				OleDbDataAdapter odadData = new OleDbDataAdapter(ocmdPCS);
				odadData.Fill(dtbData);
				return dtbData;
			}
			catch(OleDbException ex)
			{
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
			}			
			catch (Exception ex) 
			{
				throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
			}
			finally
			{
				if (oconPCS != null)
					if (oconPCS.State != ConnectionState.Closed)
						oconPCS.Close();
			}
		}

		/// <summary>
		/// Get all work orders in the period of time
		/// </summary>
		/// <param name="pdtmFromDate">From Date</param>
		/// <param name="pdtmToDate">To Date</param>
		/// <returns>List of Work Orders</returns>
		public DataTable GetTotalWO(string pstrCCNID)
		{
			const string METHOD_NAME = THIS + ".GetTotalWO()";
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				DataTable dtbData = new DataTable();
				Utils objUtils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				string strSql = "SELECT	ISNULL(SUM(ISNULL(PRO_WorkOrderDetail.OrderQuantity, 0)), 0) AS 'Quantity',"
					+ " PRO_WorkOrderDetail.ProductID, PRO_WorkOrderDetail.DueDate"
					+ " FROM	PRO_WorkOrderDetail"
					+ " JOIN PRO_WorkOrderMaster"
					+ " ON PRO_WorkOrderDetail.WorkOrderMasterID = PRO_WorkOrderMaster.WorkOrderMasterID"
					+ " JOIN ITM_Routing"
					+ " ON PRO_WorkOrderDetail.ProductID = ITM_Routing.ProductID"
					+ " JOIN MST_WorkCenter"
					+ " ON ITM_Routing.WorkCenterID = MST_WorkCenter.WorkCenterID AND MST_WorkCenter.IsMain = 1"
					+ " WHERE PRO_WorkOrderDetail.Status = " + (int)WOLineStatus.Released
					+ " AND PRO_WorkOrderMaster.CCNID = " + pstrCCNID
					+ " GROUP BY PRO_WorkOrderDetail.ProductID, PRO_WorkOrderDetail.DueDate";

				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.CommandTimeout = 1000;
				ocmdPCS.Connection.Open();

				OleDbDataAdapter odadData = new OleDbDataAdapter(ocmdPCS);
				odadData.Fill(dtbData);
				return dtbData;
			}
			catch(OleDbException ex)
			{
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
			}			
			catch (Exception ex) 
			{
				throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
			}
			finally
			{
				if (oconPCS != null)
					if (oconPCS.State != ConnectionState.Closed)
						oconPCS.Close();
			}
		}

		/// <summary>
		/// Get Main Work Center of Production Line
		/// </summary>
		/// <param name="pintProductionLineID">Production Line</param>
		/// <returns>Main WorkCenter ID</returns>
		public int GetMainWorkCenter(int pintProductionLineID)
		{
			const string METHOD_NAME = THIS + ".GetMainWorkCenter()";
			OleDbConnection oconPCS = null;
			try
			{
				Utils objUtils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				string strSql = "SELECT MST_WorkCenter.WorkCenterID FROM MST_WorkCenter"
					+ " WHERE MST_WorkCenter.IsMain = 1"
					+ " AND MST_WorkCenter.ProductionLineID = " + pintProductionLineID;
				OleDbCommand cmdData = new OleDbCommand(strSql, oconPCS);
				cmdData.Connection.Open();
				try
				{
					return int.Parse(cmdData.ExecuteScalar().ToString());
				}
				catch
				{
					return 0;
				}
			}
			catch(OleDbException ex)
			{
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
			}			
			catch (Exception ex) 
			{
				throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
			}
			finally
			{
				if (oconPCS != null)
					if (oconPCS.State != ConnectionState.Closed)
						oconPCS.Close();
			}
		}
		/// <summary>
		/// Gets working time of work center
		/// </summary>
		/// <param name="pintProductionLineID">Production Line</param>
		/// <returns></returns>
		public DataTable GetWorkingTime(int pintProductionLineID)
		{
			const string METHOD_NAME = THIS + ".GetWorkingTime()";
			OleDbConnection oconPCS = null;
			OleDbDataAdapter odadPCS = null;
			try
			{
				DataTable dtbData = new DataTable();
				Utils objUtils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				string strSql = "SELECT SC.ShiftID, SP.EffectDateFrom, WCC.BeginDate, WCC.EndDate,"
					+ " SP.WorkTimeFrom, SP.WorkTimeTo, RegularStopFrom, RegularStopTo,"
					+ " RefreshingFrom, RefreshingTo, ExtraStopFrom, ExtraStopTo,"
					+ " WCC.WorkCenterID, WC.ProductionLineID"
					+ " FROM PRO_ShiftCapacity SC"
					+ " INNER JOIN PRO_WCCapacity WCC ON WCC.WCCapacityID=SC.WCCapacityID"
					+ " LEFT JOIN MST_WorkCenter WC ON WC.WorkCenterID=WCC.WorkCenterID"
					+ " INNER JOIN PRO_ShiftPattern SP ON SC.ShiftID=SP.ShiftID"
					+ " WHERE WC.ProductionLineID = " + pintProductionLineID
					+ " AND WC.IsMain = 1";
				OleDbCommand cmdData = new OleDbCommand(strSql, oconPCS);
				odadPCS = new OleDbDataAdapter(cmdData);
				cmdData.Connection.Open();
				odadPCS.Fill(dtbData);
				return dtbData;
			}
			catch(OleDbException ex)
			{
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
			}			
			catch (Exception ex) 
			{
				throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
			}
			finally
			{
				if (oconPCS != null)
					if (oconPCS.State != ConnectionState.Closed)
						oconPCS.Close();
			}
		}
		/// <summary>
		/// Gets working time of work center
		/// </summary>
		/// <returns></returns>
		public DataTable GetWorkingTime()
		{
			const string METHOD_NAME = THIS + ".GetWorkingTime()";
			OleDbConnection oconPCS = null;
			OleDbDataAdapter odadPCS = null;
			try
			{
				DataTable dtbData = new DataTable();
				Utils objUtils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				string strSql = "SELECT PRO_ShiftPattern.EffectDateFrom, "
					+ " PRO_ShiftPattern.WorkTimeFrom, PRO_ShiftPattern.WorkTimeTo"
					+ " FROM PRO_ShiftPattern JOIN PRO_Shift"
					+ " ON PRO_ShiftPattern.ShiftID = PRO_Shift.ShiftID"
					+ " WHERE ShiftDesc IN ('1S','2S','3S')";
				OleDbCommand cmdData = new OleDbCommand(strSql, oconPCS);
				odadPCS = new OleDbDataAdapter(cmdData);
				cmdData.Connection.Open();
				odadPCS.Fill(dtbData);
				return dtbData;
			}
			catch(OleDbException ex)
			{
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
			}			
			catch (Exception ex) 
			{
				throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
			}
			finally
			{
				if (oconPCS != null)
					if (oconPCS.State != ConnectionState.Closed)
						oconPCS.Close();
			}
		}
		public DataTable ListProductionLine()
		{
			const string METHOD_NAME = THIS + ".ListProductionLine()";
			OleDbConnection oconPCS = null;
			try
			{
				Utils objUtils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				string strSql=	"SELECT ProductionLineID FROM PRO_ProductionLine";
				OleDbCommand cmdPCS = new OleDbCommand(strSql, oconPCS);
				cmdPCS.Connection.Open();
				DataTable dtbData = new DataTable();
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(cmdPCS);
				odadPCS.Fill(dtbData);
				return dtbData;
			}
			catch(OleDbException ex)
			{
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
			}			
			catch (Exception ex) 
			{
				throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
			}
			finally
			{
				if (oconPCS != null)
					if (oconPCS.State != ConnectionState.Closed)
						oconPCS.Close();
			}
		}
		public DataTable ListProduct(string pstrCCNID, string pstrProductionLineList)
		{
			const string METHOD_NAME = THIS + ".ListProduct()";
			OleDbConnection oconPCS = null;
			try
			{
				Utils objUtils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				string strSql=	"SELECT ProductID, ProductionLineID, ISNULL(ScrapPercent,0) ScrapPercent FROM ITM_Product"
				              	+ " WHERE ProductionLineID IN (" + pstrProductionLineList + ")"
				              	+ " AND CCNID = " + pstrCCNID;
				OleDbCommand cmdPCS = new OleDbCommand(strSql, oconPCS);
				cmdPCS.CommandTimeout = 1000;
				cmdPCS.Connection.Open();
				DataTable dtbData = new DataTable();
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(cmdPCS);
				odadPCS.Fill(dtbData);
				return dtbData;
			}
			catch(OleDbException ex)
			{
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
			}			
			catch (Exception ex) 
			{
				throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
			}
			finally
			{
				if (oconPCS != null)
					if (oconPCS.State != ConnectionState.Closed)
						oconPCS.Close();
			}
		}
		public DataTable GetPlanningOffset(string pstrCCNID)
		{
			const string METHOD_NAME = THIS + ".GetPlanningOffset()";
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS =null;
			try
			{
				string strSql = "SELECT PRO_PlanningOffset.PlanningStartDate, PRO_PlanningOffset.DCOptionMasterID,"
					+ " PRO_PlanningOffset.ProductionLineID"
					+ " FROM PRO_PlanningOffset JOIN PRO_DCOptionMaster"
					+ " ON PRO_PlanningOffset.DCOptionMasterID = PRO_DCOptionMaster.DCOptionMasterID"
					+ " WHERE PRO_DCOptionMaster.CCNID = " + pstrCCNID;
				Utils objUtils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				DataTable dtbData = new DataTable();
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dtbData);
				return dtbData;
			}
			catch(OleDbException ex)
			{
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
			}			
			catch (Exception ex) 
			{
				throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
			}
			finally 
			{
				if (oconPCS!=null) 
					if (oconPCS.State != ConnectionState.Closed) 
						oconPCS.Close();
			}
		}

		public DataTable GetBeginStockForReportData()
		{
			const string METHOD_NAME = THIS + ".GetBeginStockForReportData()";
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS =null;
			try
			{
				string strSql = "SELECT * FROM IV_BeginDCPReport";
				Utils objUtils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				DataTable dtbData = new DataTable("IV_BeginDCPReport");
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dtbData);
				return dtbData;
			}
			catch(OleDbException ex)
			{
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
			}			
			catch (Exception ex) 
			{
				throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
			}
			finally 
			{
				if (oconPCS!=null) 
					if (oconPCS.State != ConnectionState.Closed) 
						oconPCS.Close();
			}
		}
		public DataTable GetBeginNetQuantity(string pstrCCNID)
		{
			const string METHOD_NAME = THIS + ".GetBeginNetQuantity()";
			OleDbConnection oconPCS = null;
			try
			{
				Utils objUtils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				string strSql=	"SELECT ISNULL(SUM(ISNULL(OHQuantity, 0)), 0) - ISNULL(SUM(ISNULL(CommitQuantity, 0)), 0) AS 'Quantity', ProductID"
					+ " FROM IV_BinCache JOIN MST_BIN"
					+ " ON IV_BinCache.BinID = MST_BIN.BinID"
					+ " WHERE CCNID = " + pstrCCNID
					+ " AND MST_BIN.BinTypeID NOT IN (" + (int)BinTypeEnum.NG + "," + (int)BinTypeEnum.LS + ")"
					+ " GROUP BY ProductID";
				OleDbCommand cmdPCS = new OleDbCommand(strSql, oconPCS);
				cmdPCS.Connection.Open();
				DataTable dtbTRC = new DataTable();
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(cmdPCS);
				odadPCS.Fill(dtbTRC);
				return dtbTRC;
			}
			catch(OleDbException ex)
			{
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
			}			
			catch (Exception ex) 
			{
				throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
			}
			finally
			{
				if (oconPCS != null)
					if (oconPCS.State != ConnectionState.Closed)
						oconPCS.Close();
			}
		}
		public DataTable GetTransactionHistory()
		{
			const string METHOD_NAME = THIS + ".GetTransactionHistory()";
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS =null;
			try
			{
				string strSql = "SELECT ProductID, TransDate, "
				                + " 	CASE Type"
				                + " 		WHEN 0 THEN -Quantity"
				                + " 		WHEN 1 THEN Quantity"
				                + " 		WHEN 2 THEN Quantity"
				                + " 	END AS Quantity"
				                + " FROM MST_TransactionHistory JOIN MST_TranType"
				                + " ON MST_TransactionHistory.TranTypeID = MST_TranType.TranTypeID"
				                + " WHERE Type IN (0,1,2) ORDER BY ProductID, TransDate";
				Utils objUtils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.CommandTimeout = 1000;

				ocmdPCS.Connection.Open();
				DataTable dtbData = new DataTable();
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dtbData);
				return dtbData;
			}
			catch(OleDbException ex)
			{
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
			}			
			catch (Exception ex) 
			{
				throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
			}
			finally 
			{
				if (oconPCS!=null) 
					if (oconPCS.State != ConnectionState.Closed) 
						oconPCS.Close();
			}
		}
		public DataTable GetDeliveryForSO()
		{
			const string METHOD_NAME = THIS + ".GetDeliveryForSO()";
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			OleDbDataAdapter odadPCS = null;
			try
			{
				Utils objUtils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				string strSql = "SELECT SUM(ISNULL(DeliveryQuantity, 0)) AS Quantity, ScheduleDate, ProductID"
					+ " FROM SO_DeliverySchedule JOIN SO_SaleOrderDetail"
					+ " ON SO_DeliverySchedule.SaleOrderDetailID = SO_SaleOrderDetail.SaleOrderDetailID"
					+ " GROUP BY ProductID, ScheduleDate ORDER BY ProductID, ScheduleDate";

				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.CommandTimeout = 1000;
				ocmdPCS.Connection.Open();

				DataTable dtbDelivery = new DataTable();
				odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dtbDelivery);
				return dtbDelivery;
			}
			catch(OleDbException ex)
			{
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
			}			
			catch (Exception ex) 
			{
				throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
			}
			finally
			{
				if (oconPCS != null)
					if (oconPCS.State != ConnectionState.Closed)
						oconPCS.Close();
			}
		}
		public DataTable GetDeliveryForSO(string strItems, DateTime pdtmFromDate, DateTime pdtmToDate)
		{
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			OleDbDataAdapter odadPCS = null;
			try
			{
				Utils objUtils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				string strSql = "SELECT SUM(ISNULL(DeliveryQuantity, 0)) AS Quantity, ScheduleDate, ProductID"
					+ " FROM SO_DeliverySchedule JOIN SO_SaleOrderDetail"
					+ " ON SO_DeliverySchedule.SaleOrderDetailID = SO_SaleOrderDetail.SaleOrderDetailID"
					+ " WHERE ScheduleDate >= ? AND ScheduleDate <= ?";
				if (strItems.Trim().Length > 0)
					strSql += " AND ProductID IN (" + strItems + ")";
				strSql += " GROUP BY ProductID, ScheduleDate ORDER BY ProductID, ScheduleDate";

				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Parameters.Add(new OleDbParameter("FromDate", OleDbType.Date)).Value = pdtmFromDate;
				ocmdPCS.Parameters.Add(new OleDbParameter("ToDate", OleDbType.Date)).Value = pdtmToDate;
				ocmdPCS.Connection.Open();

				DataTable dtbDelivery = new DataTable();
				odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dtbDelivery);
				return dtbDelivery;
			}
			finally
			{
				if (oconPCS != null)
					if (oconPCS.State != ConnectionState.Closed)
						oconPCS.Close();
			}
		}
		public DataTable GetProduce()
		{
			const string METHOD_NAME = THIS + ".GetProduce()";
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			OleDbDataAdapter odadPCS = null;
			try
			{
				Utils objUtils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				string strSql = "SELECT DISTINCT SUM(ISNULL(PRO_DCPResultDetail.Quantity, 0)) AS Quantity,"
					+ " PRO_DCPResultMaster.ProductID, PRO_DCPResultMaster.WorkCenterID, WorkingDate, PRO_DCPResultMaster.DCOptionMasterID,"
					+ " PRO_DCPResultDetail.StartTime, PRO_DCPResultDetail.EndTime, MST_WorkCenter.ProductionLineID"
					+ " FROM PRO_DCPResultDetail JOIN PRO_DCPResultMaster"
					+ " ON PRO_DCPResultMaster.DCPResultMasterID = PRO_DCPResultDetail.DCPResultMasterID"
					+ " JOIN MST_WorkCenter ON PRO_DCPResultMaster.WorkCenterID = MST_WorkCenter.WorkCenterID"
					+ " WHERE MST_WorkCenter.IsMain = 1"
					+ " GROUP BY PRO_DCPResultMaster.DCOptionMasterID, PRO_DCPResultDetail.WorkingDate,"
					+ " PRO_DCPResultMaster.WorkCenterID, PRO_DCPResultMaster.ProductID,"
					+ " PRO_DCPResultDetail.StartTime, MST_WorkCenter.ProductionLineID, PRO_DCPResultDetail.EndTime"
					+ " ORDER BY PRO_DCPResultMaster.DCOptionMasterID, PRO_DCPResultMaster.ProductID, WorkingDate";

				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.CommandTimeout = 1000;
				ocmdPCS.Connection.Open();

				DataTable dtbDelivery = new DataTable();
				odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dtbDelivery);
				return dtbDelivery;
			}
			catch(OleDbException ex)
			{
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
			}			
			catch (Exception ex) 
			{
				throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
			}
			finally
			{
				if (oconPCS != null)
					if (oconPCS.State != ConnectionState.Closed)
						oconPCS.Close();
			}
		}
		public DataTable GetProduce(string pstrOptionID, int pintProductionLineID, string strItems, DateTime pdtmFromDate, DateTime pdtmToDate)
		{
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			OleDbDataAdapter odadPCS = null;
			try
			{
				if (pstrOptionID.Length == 0)
					pstrOptionID = "0";
				Utils objUtils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				string strSql = "SELECT DISTINCT SUM(ISNULL(PRO_DCPResultDetail.Quantity, 0)) AS Quantity,"
					+ " PRO_DCPResultMaster.ProductID, PRO_DCPResultMaster.WorkCenterID, WorkingDate, PRO_DCPResultMaster.DCOptionMasterID,"
					+ " PRO_DCPResultDetail.StartTime, PRO_DCPResultDetail.EndTime, ShiftID"
					+ " FROM PRO_DCPResultDetail JOIN PRO_DCPResultMaster"
					+ " ON PRO_DCPResultMaster.DCPResultMasterID = PRO_DCPResultDetail.DCPResultMasterID"
					+ " JOIN MST_WorkCenter ON PRO_DCPResultMaster.WorkCenterID = MST_WorkCenter.WorkCenterID"
					+ " WHERE PRO_DCPResultMaster.DCOptionMasterID IN (" + pstrOptionID + ")"
					+ " AND MST_WorkCenter.ProductionLineID = " + pintProductionLineID
					+ " AND MST_WorkCenter.IsMain = 1"
					+ " AND WorkingDate >= ? AND WorkingDate <= ?";
				if (strItems != null && strItems.Trim().Length > 0)
					strSql += " AND PRO_DCPResultMaster.ProductID IN (" + strItems + ")";
				strSql += " GROUP BY PRO_DCPResultMaster.DCOptionMasterID, PRO_DCPResultDetail.WorkingDate,"
					+ " PRO_DCPResultMaster.WorkCenterID, PRO_DCPResultMaster.ProductID,"
					+ " PRO_DCPResultDetail.StartTime, PRO_DCPResultDetail.EndTime, ShiftID"
					+ " ORDER BY PRO_DCPResultMaster.DCOptionMasterID, PRO_DCPResultMaster.ProductID, WorkingDate";

				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Parameters.Add(new OleDbParameter("FromDate", OleDbType.Date)).Value = pdtmFromDate;
				ocmdPCS.Parameters.Add(new OleDbParameter("ToDate", OleDbType.Date)).Value = pdtmToDate;
				ocmdPCS.Connection.Open();

				DataTable dtbDelivery = new DataTable();
				odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dtbDelivery);
				return dtbDelivery;
			}
			finally
			{
				if (oconPCS != null)
					if (oconPCS.State != ConnectionState.Closed)
						oconPCS.Close();
			}
		}
		public DataTable GetDeliveryForParent()
		{
			const string METHOD_NAME = THIS + ".GetDeliveryForParent()";
			OleDbConnection oconPCS = null;
			OleDbCommand cmdPCS = null;
			OleDbDataAdapter odadPCS = null;
			try
			{
				Utils objUtils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				string strSql = "SELECT DISTINCT SUM(ISNULL((PRO_DCPResultDetail.Quantity * ITM_BOM.Quantity)/((100 - ISNULL(ITM_BOM.Shrink,0))/100), 0)) AS Quantity,"
					+ " ITM_BOM.ComponentID AS ProductID, PRO_DCPResultMaster.WorkCenterID, ITM_BOM.LeadTimeOffset, PRO_DCPResultDetail.WorkingDate,"
					+ " PRO_DCPResultMaster.DCOptionMasterID, PRO_DCPResultDetail.StartTime, PRO_DCPResultDetail.EndTime, ShiftID"
					+ " FROM PRO_DCPResultDetail JOIN PRO_DCPResultMaster"
					+ " ON PRO_DCPResultMaster.DCPResultMasterID = PRO_DCPResultDetail.DCPResultMasterID"
					+ " JOIN ITM_BOM ON PRO_DCPResultMaster.ProductID = ITM_BOM.ProductID"
					+ " JOIN MST_WorkCenter ON PRO_DCPResultMaster.WorkCenterID = MST_WorkCenter.WorkCenterID"
					+ " GROUP BY PRO_DCPResultMaster.DCOptionMasterID, PRO_DCPResultDetail.WorkingDate,"
					+ " PRO_DCPResultMaster.WorkCenterID, ITM_BOM.ComponentID, LeadTimeOffset,"
					+ " PRO_DCPResultDetail.StartTime, PRO_DCPResultDetail.EndTime, ShiftID"
					+ " ORDER BY PRO_DCPResultMaster.DCOptionMasterID, ITM_BOM.ComponentID, WorkingDate";
				
				cmdPCS = new OleDbCommand(strSql, oconPCS);
				cmdPCS.CommandTimeout = 1000;
				cmdPCS.Connection.Open();

				DataTable dtbDelivery = new DataTable();
				odadPCS = new OleDbDataAdapter(cmdPCS);
				odadPCS.Fill(dtbDelivery);
				return dtbDelivery;
			}
			catch(OleDbException ex)
			{
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
			}			
			catch (Exception ex) 
			{
				throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
			}
			finally
			{
				if (oconPCS != null)
					if (oconPCS.State != ConnectionState.Closed)
						oconPCS.Close();
			}
		}
		public DataTable GetDeliveryForParent(string pstrOptionID, string strItems, DateTime pdtmFromDate, DateTime pdtmToDate)
		{
			OleDbConnection oconPCS = null;
			OleDbCommand cmdPCS = null;
			OleDbDataAdapter odadPCS = null;
			try
			{
				if (pstrOptionID.Length == 0)
					pstrOptionID = "0";
				Utils objUtils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				string strSql = "SELECT DISTINCT SUM(ISNULL((PRO_DCPResultDetail.Quantity * ITM_BOM.Quantity)/((100 - ISNULL(ITM_BOM.Shrink,0))/100), 0)) AS Quantity,"
					+ " ITM_BOM.ComponentID AS ProductID, PRO_DCPResultMaster.WorkCenterID, ITM_BOM.LeadTimeOffset, PRO_DCPResultDetail.WorkingDate,"
					+ " PRO_DCPResultMaster.DCOptionMasterID, PRO_DCPResultDetail.StartTime, PRO_DCPResultDetail.EndTime, ShiftID"
					+ " FROM PRO_DCPResultDetail JOIN PRO_DCPResultMaster"
					+ " ON PRO_DCPResultMaster.DCPResultMasterID = PRO_DCPResultDetail.DCPResultMasterID"
					+ " JOIN ITM_BOM ON PRO_DCPResultMaster.ProductID = ITM_BOM.ProductID"
					+ " WHERE PRO_DCPResultMaster.DCOptionMasterID IN (" + pstrOptionID + ")"
					+ " AND WorkingDate >= ? AND WorkingDate <= ?";
				if (strItems.Trim().Length > 0)
					strSql += " AND ITM_BOM.ComponentID IN (" + strItems + ")";
				strSql += " GROUP BY PRO_DCPResultMaster.DCOptionMasterID, PRO_DCPResultDetail.WorkingDate,"
					+ " PRO_DCPResultMaster.WorkCenterID, ITM_BOM.ComponentID, LeadTimeOffset,"
					+ " PRO_DCPResultDetail.StartTime, PRO_DCPResultDetail.EndTime, ShiftID"
					+ " ORDER BY PRO_DCPResultMaster.DCOptionMasterID, ITM_BOM.ComponentID, WorkingDate";
				
				cmdPCS = new OleDbCommand(strSql, oconPCS);
				cmdPCS.Parameters.Add(new OleDbParameter("FromDate", OleDbType.Date)).Value = pdtmFromDate;
				cmdPCS.Parameters.Add(new OleDbParameter("ToDate", OleDbType.Date)).Value = pdtmToDate;
				cmdPCS.Connection.Open();

				DataTable dtbDelivery = new DataTable();
				odadPCS = new OleDbDataAdapter(cmdPCS);
				odadPCS.Fill(dtbDelivery);
				return dtbDelivery;
			}
			finally
			{
				if (oconPCS != null)
					if (oconPCS.State != ConnectionState.Closed)
						oconPCS.Close();
			}
		}
		public DataTable GetWorkingDateFromWCCapacity()
		{
			const string METHOD_NAME = THIS + ".GetWorkingDateFromWCCapacity()";
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			DataTable dtbData = new DataTable();
			try
			{
				string strSql = "SELECT BeginDate, EndDate, MST_WorkCenter.ProductionLineID"
				                + " FROM PRO_WCCapacity JOIN MST_WorkCenter"
				                + " ON PRO_WCCapacity.WorkCenterID = MST_WorkCenter.WorkCenterID"
				                + " JOIN PRO_ProductionLine ON MST_WorkCenter.ProductionLineID"
				                + " = PRO_ProductionLine.ProductionLineID"
				                + " WHERE MST_WorkCenter.IsMain = 1";
				Utils objUtils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dtbData);
				return dtbData;
			}
			catch(OleDbException ex)
			{
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
			}			
			catch (Exception ex) 
			{
				throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
			}
			finally 
			{
				if (oconPCS!=null) 
				{
					if (oconPCS.State != ConnectionState.Closed) 
					{
						oconPCS.Close();
					}
				}
			}
		}
		public DataTable GetWorkingDateFromWCCapacity(int pintProductionLineID)
		{
			const string METHOD_NAME = THIS + ".GetWorkingDateFromWCCapacity()";
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			DataTable dtbData = new DataTable();
			try
			{
				string strSql = "SELECT BeginDate, EndDate, MST_WorkCenter.ProductionLineID"
					+ " FROM PRO_WCCapacity JOIN MST_WorkCenter"
					+ " ON PRO_WCCapacity.WorkCenterID = MST_WorkCenter.WorkCenterID"
					+ " JOIN PRO_ProductionLine ON MST_WorkCenter.ProductionLineID"
					+ " = PRO_ProductionLine.ProductionLineID"
					+ " WHERE MST_WorkCenter.IsMain = 1"
					+ " AND PRO_ProductionLine.ProductionLineID = " + pintProductionLineID;
				Utils objUtils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dtbData);
				return dtbData;
			}
			catch(OleDbException ex)
			{
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
			}			
			catch (Exception ex) 
			{
				throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
			}
			finally 
			{
				if (oconPCS!=null) 
				{
					if (oconPCS.State != ConnectionState.Closed) 
					{
						oconPCS.Close();
					}
				}
			}
		}
		public ArrayList GetPlanningPeriod(string pstrCCNID)
		{
			const string METHOD_NAME = THIS + ".GetWorkingDateFromWCCapacity()";
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = "SELECT DISTINCT PlanningPeriod FROM PRO_DCOptionMaster WHERE CCNID = " + pstrCCNID;
				Utils objUtils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				OleDbDataReader reader = ocmdPCS.ExecuteReader();
				ArrayList arrDate = new ArrayList();
				while(reader.Read())
					arrDate.Add((DateTime)reader["PlanningPeriod"]);
				return arrDate;
			}
			catch(OleDbException ex)
			{
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
			}			
			catch (Exception ex) 
			{
				throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
			}
			finally 
			{
				if (oconPCS!=null) 
				{
					if (oconPCS.State != ConnectionState.Closed) 
					{
						oconPCS.Close();
					}
				}
			}
		}
		public DataTable GetCycles(string pstrCCNID)
		{
			OleDbConnection oconPCS = null;
			OleDbDataAdapter odadPCS = null;
			try
			{
				DataTable dtbData = new DataTable();
				Utils objUtils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				string strSql = "SELECT DCOptionMasterID, PlanningPeriod, Version,"
					+ " AsOfDate AS FromDate, DATEADD(dd, PlanHorizon, AsOfDate) AS ToDate"
					+ " FROM PRO_DCOptionMaster"
					+ " WHERE CCNID = " + pstrCCNID;
				OleDbCommand cmdData = new OleDbCommand(strSql, oconPCS);
				odadPCS = new OleDbDataAdapter(cmdData);
				cmdData.Connection.Open();
				odadPCS.Fill(dtbData);
				return dtbData;
			}
			finally
			{
				if (oconPCS != null)
					if (oconPCS.State != ConnectionState.Closed)
						oconPCS.Close();
			}
		}
		public void UpdateBeginStockData(DataSet pdstData)
		{
			const string METHOD_NAME = THIS + ".UpdateDataSet()";
			OleDbConnection oconPCS =null;
			OleDbCommandBuilder odcbPCS ;
			OleDbDataAdapter odadPCS = new OleDbDataAdapter();

			try
			{
				string strSql = "SELECT BeginDCPReportID, ProductID, LastUpdate, EffectDate, Quantity, Username"
				                + " FROM IV_BeginDCPReport";
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				odadPCS.SelectCommand = new OleDbCommand(strSql, oconPCS);
				odcbPCS = new OleDbCommandBuilder(odadPCS);
				pdstData.EnforceConstraints = false;
				odadPCS.Update(pdstData, "IV_BeginDCPReport");
			}
			catch(OleDbException ex)
			{
				if (ex.Errors.Count > 1)
				{
					if (ex.Errors[1].NativeError == ErrorCode.SQLDUPLICATE_KEYCODE)
						throw new PCSDBException(ErrorCode.DUPLICATE_KEY, METHOD_NAME, ex);
					else if (ex.Errors[1].NativeError == ErrorCode.SQLCASCADE_PREVENT_KEYCODE)
						throw new PCSDBException(ErrorCode.CASCADE_DELETE_PREVENT, METHOD_NAME, ex);
					else
						throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME,ex);
				}
				else
					throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME,ex);
			}
			catch(InvalidOperationException ex)
			{
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME,ex);
			}
			catch (Exception ex) 
			{
				throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
			}
			finally 
			{
				if (oconPCS!=null) 
					if (oconPCS.State != ConnectionState.Closed) 
						oconPCS.Close();
			}
		}
		public DataTable GetDemandWO(string pstrCCNID)
		{
			const string METHOD_NAME = THIS + ".GetDemandWO()";
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				DataTable dtbData = new DataTable();
				Utils objUtils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				string strSql = "SELECT	ISNULL(SUM(ISNULL(PRO_WorkOrderDetail.OrderQuantity, 0) * ISNULL(ITM_BOM.Quantity,0) / (1 - ISNULL(ITM_BOM.Shrink,0) * 0.01) ), 0) AS 'Quantity',"
					+ " ITM_BOM.ComponentID AS ProductID, DateAdd(ss,-ISNULL(ITM_BOM.LeadTimeOffset,0),PRO_WorkOrderDetail.StartDate) AS StartDate"
					+ " FROM	PRO_WorkOrderDetail"
					+ " JOIN PRO_WorkOrderMaster"
					+ " ON PRO_WorkOrderDetail.WorkOrderMasterID = PRO_WorkOrderMaster.WorkOrderMasterID"
					+ " JOIN ITM_BOM ON ITM_BOM.ProductID = PRO_WorkOrderDetail.ProductID"
					+ " JOIN ITM_Routing"
					+ " ON ITM_BOM.ComponentID = ITM_Routing.ProductID"
					+ " JOIN MST_WorkCenter"
					+ " ON ITM_Routing.WorkCenterID = MST_WorkCenter.WorkCenterID AND MST_WorkCenter.IsMain = 1"
					+ " WHERE PRO_WorkOrderDetail.Status = " + (int)WOLineStatus.Released
					+ " AND PRO_WorkOrderMaster.CCNID = " + pstrCCNID
					+ " GROUP BY ITM_BOM.ComponentID, PRO_WorkOrderDetail.StartDate,ITM_BOM.LeadTimeOffset";

				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.CommandTimeout = 1000;
				ocmdPCS.Connection.Open();

				OleDbDataAdapter odadData = new OleDbDataAdapter(ocmdPCS);
				odadData.Fill(dtbData);
				return dtbData;
			}
			catch(OleDbException ex)
			{
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
			}			
			catch (Exception ex) 
			{
				throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
			}
			finally
			{
				if (oconPCS != null)
					if (oconPCS.State != ConnectionState.Closed)
						oconPCS.Close();
			}
		}
		public DataTable GetOverItems(int pintProductionLineID, int pintCycleID)
		{
			const string METHOD_NAME = THIS + ".GetOverItems()";
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				DataTable dtbData = new DataTable(PRO_DCPResultDetailTable.TABLE_NAME);
				Utils objUtils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				string strSql = "SELECT PRO_DCPResultMaster.DCPResultMasterID, DCPResultDetailID,"
					+ " PRO_DCPResultMaster.ProductID, SUM(ISNULL(TotalSecond,0)) AS TotalSecond, " + ITM_ProductTable.LTVARIABLETIME_FLD + ","
					+ " SUM(ISNULL(PRO_DCPResultDetail.Quantity,0)) AS Quantity, " + ITM_ProductTable.SETUPPAIR_FLD
					+ " , MST_WorkCenter.WorkCenterID"
					+ " FROM PRO_DCPResultDetail JOIN PRO_DCPResultMaster"
					+ " ON PRO_DCPResultDetail.DCPResultMasterID = PRO_DCPResultMaster.DCPResultMasterID"
					+ " JOIN MST_WorkCenter ON PRO_DCPResultMaster.WorkCenterID = MST_WorkCenter.WorkCenterID"
					+ " JOIN ITM_Product ON PRO_DCPResultMaster.ProductID = ITM_Product.ProductID"
					+ " WHERE ShiftID IS NULL"
					+ " AND DCOptionMasterID = " + pintCycleID
					+ " AND MST_WorkCenter.ProductionLineID = " + pintProductionLineID
					+ " AND ISNULL(IsMain,0) = 1"
					+ " GROUP BY PRO_DCPResultMaster.DCPResultMasterID, DCPResultDetailID,"
					+ " PRO_DCPResultMaster.ProductID, " + ITM_ProductTable.SETUPPAIR_FLD + ", " + ITM_ProductTable.LTVARIABLETIME_FLD
					+ " , MST_WorkCenter.WorkCenterID"
					+ " ORDER BY PRO_DCPResultMaster.ProductID";

				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.CommandTimeout = 1000;
				ocmdPCS.Connection.Open();

				OleDbDataAdapter odadData = new OleDbDataAdapter(ocmdPCS);
				odadData.Fill(dtbData);
				return dtbData;
			}
			catch(OleDbException ex)
			{
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
			}			
			catch (Exception ex) 
			{
				throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
			}
			finally
			{
				if (oconPCS != null)
					if (oconPCS.State != ConnectionState.Closed)
						oconPCS.Close();
			}
		}
		public DataTable ListProduct(int pintProductionLineID, string pstrProductID)
		{
			const string METHOD_NAME = THIS + ".ListProduct()";
			OleDbConnection oconPCS = null;
			try
			{
				Utils objUtils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				string strSql=	"SELECT ProductID, Code, Revision, Description, "
					+ " ISNULL(MakeItem,0) MakeItem, ISNULL(OrderQuantity,0) OrderQuantity, ISNULL(LTRequisition,0) LTRequisition,"
					+ " ISNULL(LTSafetyStock,0) LTSafetyStock, ISNULL(OrderQuantityMultiple,1) OrderQuantityMultiple, "
					+ " ISNULL(ScrapPercent,0) ScrapPercent, ISNULL(MinimumStock,0) MinimumStock,"
					+ " ISNULL(MaximumStock,0) MaximumStock, ISNULL(LTFixedTime,0) LTFixedTime,"
					+ " ISNULL(LTVariableTime,0) LTVariableTime, ISNULL(LTOrderPrepare,0) LTOrderPrepare, "
					+ " ISNULL(LTShippingPrepare,0) LTShippingPrepare, ISNULL(LTSalesATP,0) LTSalesATP,"
					+ " CCNID, ISNULL(CategoryID,0) CategoryID, ISNULL(OrderPolicyID,0) OrderPolicyID,"
					+ " ISNULL(OrderRuleID,0) OrderRuleID, ISNULL(StockUMID,0) StockUMID, "
					+ " ISNULL(MasterLocationID,0) MasterLocationID, ISNULL(LocationID,0) LocationID,"
					+ " ISNULL(BinID,0) BinID, ISNULL(PrimaryVendorID,0) PrimaryVendorID, ISNULL(SafetyStock,0) SafetyStock,"
					+ " ISNULL(LTDockToStock,0) LTDockToStock, ISNULL(QuantitySet,0) QuantitySet,"
					+ " ISNULL(ProductionLineID,0) ProductionLineID, ISNULL(ProductGroupID,0) ProductGroupID,"
					+ " ISNULL(MaxProduce,0) MaxProduce, ISNULL(MinProduce,0) MinProduce, "
					+ " ISNULL(MaxRoundUpToMin,0) MaxRoundUpToMin, ISNULL(MaxRoundUpToMultiple,0) MaxRoundUpToMultiple,"
					+ " ISNULL(SetUpPair,0) SetUpPair"
					+ " FROM ITM_Product"
					+ " WHERE ProductionLineID = " + pintProductionLineID;
				if (pstrProductID != null && pstrProductID.Length > 0)
					strSql += " AND ProductID IN (" + pstrProductID + ")";
				OleDbCommand cmdPCS = new OleDbCommand(strSql, oconPCS);
				cmdPCS.CommandTimeout = 1000;
				cmdPCS.Connection.Open();
				DataTable dtbData = new DataTable();
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(cmdPCS);
				odadPCS.Fill(dtbData);
				return dtbData;
			}
			catch(OleDbException ex)
			{
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
			}			
			catch (Exception ex) 
			{
				throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
			}
			finally
			{
				if (oconPCS != null)
					if (oconPCS.State != ConnectionState.Closed)
						oconPCS.Close();
			}
		}
		public DataTable GetBeginStock(int pintCycleID)
		{
			const string METHOD_NAME = THIS + ".GetBeginStock()";
			OleDbConnection oconPCS = null;
			try
			{
				Utils objUtils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				string strSql=	"SELECT * FROM DCP_BeginQuantity"
					+ " WHERE DCOptionMasterID = " + pintCycleID;
				OleDbCommand cmdPCS = new OleDbCommand(strSql, oconPCS);
				cmdPCS.CommandTimeout = 1000;
				cmdPCS.Connection.Open();
				DataTable dtbData = new DataTable();
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(cmdPCS);
				odadPCS.Fill(dtbData);
				return dtbData;
			}
			catch(OleDbException ex)
			{
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
			}			
			catch (Exception ex) 
			{
				throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
			}
			finally
			{
				if (oconPCS != null)
					if (oconPCS.State != ConnectionState.Closed)
						oconPCS.Close();
			}
		}
		public DataTable GetShiftPattern(int pintProductionLineID)
		{
			const string METHOD_NAME = THIS + ".GetShiftPattern()";
			OleDbConnection oconPCS = null;
			try
			{
				Utils objUtils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				string strSql=	"SELECT DISTINCT BeginDate, EndDate, PRO_ShiftPattern.ShiftID, WorktimeFrom, WorktimeTo,"
					+ " (ISNULL(DATEDIFF(ss, WorktimeFrom, WorktimeTo),0)"
					+ " - ISNULL(DATEDIFF(ss, RegularStopFrom, RegularStopTo),0)"
					+ " - ISNULL(DATEDIFF(ss, RefreshingFrom, RefreshingTo),0)"
					+ " - ISNULL(DATEDIFF(ss, ExtraStopFrom, ExtraStopTo),0))"
					+ " * CASE WCType WHEN 0 THEN CrewSize ELSE MachineNo END"
					+ " * Factor / 100 AS Capacity"
					+ " FROM PRO_ShiftPattern JOIN PRO_ShiftCapacity ON PRO_ShiftPattern.ShiftID = PRO_ShiftCapacity.ShiftID"
					+ " JOIN PRO_WCCapacity ON PRO_ShiftCapacity.WCCapacityID = PRO_WCCapacity.WCCapacityID"
					+ " JOIN MST_WorkCenter ON PRO_WCCapacity.WorkCenterID = MST_WorkCenter.WorkCenterID"
					+ " WHERE MST_WorkCenter.ProductionLineID = " + pintProductionLineID
					+ " AND IsMain = 1";
				OleDbCommand cmdPCS = new OleDbCommand(strSql, oconPCS);
				cmdPCS.CommandTimeout = 1000;
				cmdPCS.Connection.Open();
				DataTable dtbData = new DataTable();
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(cmdPCS);
				odadPCS.Fill(dtbData);
				return dtbData;
			}
			catch(OleDbException ex)
			{
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
			}			
			catch (Exception ex) 
			{
				throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
			}
			finally
			{
				if (oconPCS != null)
					if (oconPCS.State != ConnectionState.Closed)
						oconPCS.Close();
			}
		}
		public DataTable GetProductionGroup(int pintProductionLineID)
		{
			const string METHOD_NAME = THIS + ".GetProductionGroup()";
			OleDbConnection oconPCS = null;
			try
			{
				Utils objUtils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				string strSql=	"SELECT PRO_PGProduct.ProductionGroupID, ProductionLineID, ProductID,"
					+ " Description, GroupProductionMax, Priority, CapacityOfGroup"
					+ " FROM PRO_PGProduct JOIN PRO_ProductionGroup"
					+ " ON PRO_PGProduct.ProductionGroupID = PRO_ProductionGroup.ProductionGroupID"
					+ " WHERE ProductionLineID = " + pintProductionLineID
					+ " ORDER BY ProductionLineID, ProductID";
				OleDbCommand cmdPCS = new OleDbCommand(strSql, oconPCS);
				cmdPCS.CommandTimeout = 1000;
				cmdPCS.Connection.Open();
				DataTable dtbData = new DataTable();
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(cmdPCS);
				odadPCS.Fill(dtbData);
				return dtbData;
			}
			catch(OleDbException ex)
			{
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
			}			
			catch (Exception ex) 
			{
				throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
			}
			finally
			{
				if (oconPCS != null)
					if (oconPCS.State != ConnectionState.Closed)
						oconPCS.Close();
			}
		}
		public DataTable GetDCPResultSchema()
		{
			const string METHOD_NAME = THIS + ".GetDCPResultSchema()";
			OleDbConnection oconPCS = null;
			try
			{
				Utils objUtils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				string strSql=	"SELECT TOP 0 * FROM PRO_DCPResultDetail";
				OleDbCommand cmdPCS = new OleDbCommand(strSql, oconPCS);
				cmdPCS.CommandTimeout = 1000;
				cmdPCS.Connection.Open();
				DataTable dtbData = new DataTable(PRO_DCPResultDetailTable.TABLE_NAME);
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(cmdPCS);
				odadPCS.Fill(dtbData);
				return dtbData;
			}
			catch(OleDbException ex)
			{
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
			}			
			catch (Exception ex) 
			{
				throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
			}
			finally
			{
				if (oconPCS != null)
					if (oconPCS.State != ConnectionState.Closed)
						oconPCS.Close();
			}
		}
		public object GetCycleInfo(int pintCycleID)
		{
			const string METHOD_NAME = THIS + ".GetCycleInfo()";
			DataSet dstPCS = new DataSet();

			OleDbDataReader odrPCS = null;
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;
				strSql = "SELECT * "
					+ " FROM " + PRO_DCOptionMasterTable.TABLE_NAME
					+ " WHERE " + PRO_DCOptionMasterTable.DCOPTIONMASTERID_FLD + "=" + pintCycleID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();

				PRO_DCOptionMasterVO objObject = new PRO_DCOptionMasterVO();

				while (odrPCS.Read())
				{
					try
					{
						objObject.AsOfDate = Convert.ToDateTime(odrPCS[PRO_DCOptionMasterTable.ASOFDATE_FLD]);
					}
					catch{}
					objObject.CCNID = Convert.ToInt32(odrPCS[PRO_DCOptionMasterTable.CCNID_FLD]);
					objObject.Cycle = odrPCS[PRO_DCOptionMasterTable.CYCLE_FLD].ToString().Trim();
					objObject.DCOptionMasterID = Convert.ToInt32(odrPCS[PRO_DCOptionMasterTable.DCOPTIONMASTERID_FLD]);
					objObject.PlanHorizon = Convert.ToInt32(odrPCS[PRO_DCOptionMasterTable.PLANHORIZON_FLD]);
				}
				return objObject;
			}
			catch (OleDbException ex)
			{
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
			}

			catch (Exception ex)
			{
				throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
			}

			finally
			{
				if (oconPCS != null)
				{
					if (oconPCS.State != ConnectionState.Closed)
					{
						oconPCS.Close();
					}
				}
			}
		}

		public DataTable GetOrderProduce(int pintCycleID)
		{
			const string METHOD_NAME = THIS + ".GetOrderProduce()";
			
			OleDbDataReader odrPCS = null;
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;
				strSql = "SELECT OP.*, CAST((SUBSTRING(ColumnName,1,4) + '-' + SUBSTRING(ColumnName,5,2)"
					+ " + '-' + SUBSTRING(ColumnName,7,2) ) AS datetime) AS WorkingDate, P.ProductionLineID"
					+ " FROM DCP_OrderProduce OP JOIN MST_WorkCenter WC ON OP.WorkCenterID = WC.WorkCenterID"
					+ " JOIN PRO_ProductionLine P ON WC.ProductionLineID = P.ProductionLineID"
					+ " WHERE DCOptionMasterID = " + pintCycleID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				DataTable dtbData = new DataTable();
				odadPCS.Fill(dtbData);
				return dtbData;
			}
			catch (OleDbException ex)
			{
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
			}
			catch (Exception ex)
			{
				throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
			}

			finally
			{
				if (oconPCS != null)
				{
					if (oconPCS.State != ConnectionState.Closed)
					{
						oconPCS.Close();
					}
				}
			}
		}
		public DataTable GetWCConfig(int pintProductionLineID)
		{
			const string METHOD_NAME = THIS + ".GetWCConfig()";
			DataSet dstPCS = new DataSet();
						
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = 
					" SELECT " +
					" 	PRO_WCCapacity.WorkCenterID," +
					"	MST_WorkCenter.Code WorkCenterCode," +
					" 	PRO_WCCapacity.Factor," +
					" 	IsNull(PRO_WCCapacity.CrewSize,0) CrewSize," +
					" 	IsNull(PRO_WCCapacity.MachineNo,0) MachineNo," +
					" 	PRO_WCCapacity.WCType," +
					" 	PRO_WCCapacity.Capacity," +
					"	PRO_WCCapacity.BeginDate," +
					"	PRO_WCCapacity.EndDate," +
					"	PRO_ShiftPattern.ShiftID," +
					"	PRO_ShiftPattern.WorkTimeFrom," +
					"	PRO_ShiftPattern.WorkTimeTo," +
					"	PRO_ShiftPattern.RegularStopFrom," +
					"	PRO_ShiftPattern.RegularStopTo," +
					"	PRO_ShiftPattern.RefreshingFrom," +
					"	PRO_ShiftPattern.RefreshingTo," +
					"	PRO_ShiftPattern.ExtraStopFrom," +
					"	PRO_ShiftPattern.ExtraStopTo," +

					" Case WCType When 1 Then " +
					"  (ISNULL(PRO_WCCapacity.Factor,0)/100) * IsNull(PRO_WCCapacity.MachineNo,0) * ( DATEDIFF(SECOND,WorkTimeFrom,WorkTimeTo)-ISNULL(DATEDIFF(SECOND,RegularStopFrom,RegularStopTo),0)-ISNULL(DATEDIFF(SECOND,RefreshingFrom,RefreshingTo),0)-ISNULL(DATEDIFF(SECOND,ExtraStopFrom,ExtraStopTo),0) )  " +
					" when 0 then " +
					"  (ISNULL(PRO_WCCapacity.Factor,0)/100) * IsNull(PRO_WCCapacity.CrewSize,0) * ( DATEDIFF(SECOND,WorkTimeFrom,WorkTimeTo)-ISNULL(DATEDIFF(SECOND,RegularStopFrom,RegularStopTo),0)-ISNULL(DATEDIFF(SECOND,RefreshingFrom,RefreshingTo),0)-ISNULL(DATEDIFF(SECOND,ExtraStopFrom,ExtraStopTo),0) )  " +
					" end ShiftCapacity " +

					" FROM " +
					"	PRO_WCCapacity	INNER JOIN PRO_ShiftCapacity ON  PRO_WCCapacity.WCCapacityID = PRO_ShiftCapacity.WCCapacityID" +
					"			INNER JOIN PRO_ShiftPattern ON PRO_ShiftCapacity.ShiftID = PRO_ShiftPattern.ShiftID" +
					"			INNER JOIN MST_WorkCenter ON PRO_WCCapacity.WorkCenterID = MST_WorkCenter.WorkCenterID AND MST_WorkCenter.IsMain = 1" +
					" WHERE	MST_WorkCenter.ProductionLineID = " + pintProductionLineID +
					" AND ISNULL(MST_WorkCenter.IsMain,0) = 1";

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS);

				return dstPCS.Tables[0];
			}
			catch(OleDbException ex)
			{			
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME,ex);
			}			

			catch (Exception ex) 
			{
				throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
			}

			finally 
			{
				if (oconPCS!=null) 
				{
					if (oconPCS.State != ConnectionState.Closed) 
					{
						oconPCS.Close();
					}
				}
			}
		}
	}
}
