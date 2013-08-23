using System;
using System.Collections;
using System.Windows.Forms;
using System.Collections.Specialized;
using System.Data;
using System.Drawing;

using System.Data.OleDb;
using System.Reflection;
using C1.Win.C1Preview;

using PCSComUtils.Common;
using PCSUtils.Utils;

namespace Delivery2CustomerByMonthReport
{
	[Serializable]
	public class Delivery2CustomerByMonthReport : MarshalByRefObject, IDynamicReport
	{
		public Delivery2CustomerByMonthReport()
		{
		}

		#region Constants
		
		private const string PRODUCT_CODE = "Code";
		private const string PRODUCT_NAME = "Description";
		private const string PRODUCT_MODEL = "Revision";

		private const string CODE_FIELD = "Code";
		private const string NAME_FIELD = "Name";

		private const string TABLE_NAME = "Delivery2Customer";
		
		private const int MAX_DAYS_IN_MONTH = 31;
		private const string APPLICATION_PATH     = @"PCSMain\bin\Debug";
				
		private const string DELIVERY_FLD_PREFIX = "DeliveryQuantity_";
		
		private const string DELIVERY_DAY_FLD = "DeliveryDay";
		private const string DELIVERY_MONTH_FLD = "DeliveryMonth";
		private const string DELIVERY_YEAR_FLD = "DeliveryYear";
		
		private const string PARTS_NUMBER_FLD = "ITM_ProductCode";
		private const string PARTS_MODEL_FLD = "ITM_ProductRevision";
		private const string PARTS_UM_FLD = "MST_UnitOfMeasureCode";
		private const string PARTS_NAME_FLD = "ITM_ProductDescription";
		private const string CATEGORY_FLD = "ITM_CategoryCode";
		
		private const string PARTY_CODE_NAME_FLD = "MST_PartyCodeName";
		private const string TOTAL_DELIVERY_FLD = "TotalDeliveryQuantity";

		#endregion		

		#region IDynamicReport Members
		
		private bool mUseReportViewerRenderEngine = false;
		private string mConnectionString;
		private ReportBuilder mReportBuilder;
		private C1PrintPreviewControl mReportViewer;

		private object mResult;

		/// <summary>
		/// ConnectionString, provide for the Dynamic Report
		/// ALlow Dynamic Report to access the DataBase of PCS
		/// </summary>
		public string PCSConnectionString
		{
			get { return mConnectionString; }
			set { mConnectionString = value; }
		}

		/// <summary>
		/// Report Builder Utility Object
		/// Dynamic Report can use this object to render, modify, layout the report
		/// </summary>
		public ReportBuilder PCSReportBuilder
		{
			get { return mReportBuilder; }
			set { mReportBuilder = value; }
		}

		/// <summary>
		/// ReportViewer Object, provide for the DynamicReport, 
		/// allow Dynamic Report to manipulate with the REportViewer, 
		/// modify the report after rendered if needed
		/// </summary>
		public C1PrintPreviewControl PCSReportViewer
		{
			get { return mReportViewer; }
			set { mReportViewer = value; }
		}

		/// <summary>
		/// Store other result if any. Ussually we store return DataTable here to display on the ReportViewer Form's Grid
		/// </summary>
		public object Result
		{
			get { return mResult; }
			set { mResult = value; }
		}		

		/// <summary>
		/// Notify PCS whether the rendering report process is run by
		/// this IDynamicReport or the ReportViewer Engine (in the ReportViewer form)
		/// </summary>
		public bool UseReportViewerRenderEngine
		{
			get { return mUseReportViewerRenderEngine; }
			set { mUseReportViewerRenderEngine = value; }
		}

		private string mstrReportDefinitionFolder = string.Empty;
		/// <summary>
		/// Inform External Process where to find out the ReportLayout	 ( the PCS' ReportDefinition Folder Path )
		/// </summary>				
		public string ReportDefinitionFolder
		{
			get { return mstrReportDefinitionFolder; }
			set { mstrReportDefinitionFolder = value; }
		}


		private string mstrReportLayoutFile = string.Empty;		
		/// <summary>
		/// Inform External Process about the Layout file
		/// in which PCS instruct to use
		/// (PCS will assign this property while ReportViewer Form execute,
		/// ReportVIewer form will use the layout file in the report config entry to put in this property)
		/// </summary>		
		public string ReportLayoutFile
		{
			get 
			{
				return mstrReportLayoutFile;
			}
			set
			{
				mstrReportLayoutFile = value;
			}
		}

		#endregion
		
		#region Delivery To Customer Schedule Report: Tuan TQ
		
		/// <summary>
		/// Get CCN Info
		/// </summary>
		/// <param name="pstrID"></param>
		/// <returns></returns>
		private string GetCompanyFullName()
		{			
			const string FULL_COMPANY_NAME = "CompanyFullName";
			OleDbConnection oconPCS = null;

			try
			{
				string strResult = string.Empty;
				OleDbDataReader odrPCS = null;				
				
				OleDbCommand ocmdPCS = null;

				string strSql =	"SELECT [Value]"
					+ " FROM Sys_Param"
					+ " WHERE [Name] = '" + FULL_COMPANY_NAME + "'";
			
				oconPCS = new OleDbConnection(mConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();
				if(odrPCS != null)
				{
					if(odrPCS.Read())
					{
						strResult = odrPCS["Value"].ToString().Trim();
					}
				}
			
				return strResult;
			}
			catch (Exception ex)
			{
				throw ex;
			}
			finally
			{
				if (oconPCS != null)
				{
					if (oconPCS.State != ConnectionState.Closed)
						oconPCS.Close();
					oconPCS = null;
				}
			}
		}

		/// <summary>
		/// Get CCN Info
		/// </summary>
		/// <param name="pstrID"></param>
		/// <returns></returns>
		private string GetCCNInfoByID(string pstrID)
		{
			string strResult = string.Empty;
			OleDbConnection oconPCS = null;

			
			try
			{
				OleDbDataReader odrPCS = null;
				OleDbCommand ocmdPCS = null;

				string strSql =	"SELECT " + CODE_FIELD + ", " + NAME_FIELD
					+ " FROM MST_CCN"
					+ " WHERE MST_CCN.CCNID = " + pstrID;
				
				oconPCS = new OleDbConnection(mConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();
				if(odrPCS != null)
				{
					if(odrPCS.Read())
					{
						strResult = odrPCS[CODE_FIELD].ToString().Trim() + " (" + odrPCS[NAME_FIELD].ToString().Trim() + ")";
					}
				}

				return strResult;
			
			}
			catch (Exception ex)
			{
				throw ex;
			}
			finally
			{
				if (oconPCS != null)
				{
					if (oconPCS.State != ConnectionState.Closed)
						oconPCS.Close();
					oconPCS = null;

				}
			}
		}

		/// <summary>
		/// Get Category Info
		/// </summary>
		/// <param name="pstrID"></param>
		/// <returns></returns>
		private string GetCategoryInfoByID(string pstrID)
		{			
			string strResult = string.Empty;

			OleDbDataReader odrPCS = null;
			OleDbConnection oconPCS = null;
			try
			{
				OleDbCommand ocmdPCS = null;

				string strSql =	"SELECT " + CODE_FIELD + ", " + NAME_FIELD
					+ " FROM ITM_Category"
					+ " WHERE CategoryID = " + pstrID;
				
				oconPCS = new OleDbConnection(mConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();
				if(odrPCS != null)
				{
					if(odrPCS.Read())
					{
						strResult = odrPCS[CODE_FIELD].ToString().Trim() + " (" + odrPCS[NAME_FIELD].ToString().Trim() + ")";
					}
				}
			
				return strResult;
			}
			catch (Exception ex)
			{
				throw ex;
			}
			finally
			{
				if (oconPCS != null)
				{
					if (oconPCS.State != ConnectionState.Closed)
						oconPCS.Close();
					oconPCS = null;

				}
			}
		}


		/// <summary>
		/// Get Category Info
		/// </summary>
		/// <param name="pstrID"></param>
		/// <returns></returns>
		private string GetCustomerInfoByID(string pstrID)
		{			
			string strResult = string.Empty;

			OleDbDataReader odrPCS = null;
			OleDbConnection oconPCS = null;
			try
			{
				OleDbCommand ocmdPCS = null;

				string strSql =	"SELECT " + CODE_FIELD + ", " + NAME_FIELD
					+ " FROM MST_Party"
					+ " WHERE PartyID = " + pstrID;
				
				oconPCS = new OleDbConnection(mConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();
				if(odrPCS != null)
				{
					if(odrPCS.Read())
					{
						strResult = odrPCS[CODE_FIELD].ToString().Trim() + " (" + odrPCS[NAME_FIELD].ToString().Trim() + ")";
					}
				}
			
				return strResult;
			}
			catch (Exception ex)
			{
				throw ex;
			}
			finally
			{
				if (oconPCS != null)
				{
					if (oconPCS.State != ConnectionState.Closed)
						oconPCS.Close();
					oconPCS = null;

				}
			}
		}

		
		/// <summary>
		/// Get Product Information
		/// </summary>
		/// <param name="pstrID"></param>
		/// <returns></returns>
		private Hashtable GetProductInfoByID(string pstrID)
		{					
			Hashtable htbResult = new Hashtable();
			OleDbDataReader odrPCS = null;
			OleDbConnection oconPCS = null;
			try
			{
				OleDbCommand ocmdPCS = null;

				string strSql =	"SELECT " + PRODUCT_CODE + ", " + PRODUCT_NAME + ", " + PRODUCT_MODEL
					+ " FROM ITM_Product"
					+ " WHERE ITM_Product.ProductID = " + pstrID;
			
				oconPCS = new OleDbConnection(mConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();
				if(odrPCS != null)
				{
					if(odrPCS.Read())
					{
						htbResult.Add(PRODUCT_CODE, odrPCS[PRODUCT_CODE]);
						htbResult.Add(PRODUCT_NAME, odrPCS[PRODUCT_NAME]);
						htbResult.Add(PRODUCT_MODEL, odrPCS[PRODUCT_MODEL]);
					}
				}
				return htbResult;
			}
			catch (Exception ex)
			{
				throw ex;
			}
			finally
			{
				if (oconPCS != null)
				{
					if (oconPCS.State != ConnectionState.Closed)
						oconPCS.Close();
					oconPCS = null;

				}
			}
		}

		/// <summary>
		/// Get Delivery To Customer Schedule data
		/// </summary>
		/// <param name="pintIssueMasterialMasterId">Issue Masterial Master Id</param>
		/// <returns>Top 40 row of result</returns>
		private DataTable GetDelivery2CustomerData(string pstrCCNID, string pstrMonth, string pstrYear, string pstrPartyID, string pstrCategoryID, string pstrProductID)
		{			
			DataTable dtbResultTable = new DataTable(TABLE_NAME);
			OleDbConnection oconPCS = null;
			try
			{
				OleDbCommand ocmdPCS = null;

				string strSql = "SELECT  Day(SO_DeliverySchedule.ScheduleDate) as DeliveryDay,";
				strSql += " Month(SO_DeliverySchedule.ScheduleDate) as DeliveryMonth,";
				strSql += " Year(SO_DeliverySchedule.ScheduleDate) as DeliveryYear,";

				strSql += " ITM_Product.Code as ITM_ProductCode,";
				strSql += " ITM_Product.Description as ITM_ProductDescription,";
				strSql += " ITM_Category.Code as ITM_CategoryCode,";
				strSql += " ITM_Product.Revision as ITM_ProductRevision,";
				strSql += " MST_UnitOfMeasure.Code as MST_UnitOfMeasureCode,";
						
				strSql += " MST_Party.Code + ' (' + MST_Party.Name + ')' as MST_PartyCodeName,";				
				strSql += " SUM(SO_DeliverySchedule.DeliveryQuantity) as DeliveryQuantity";

				strSql += " FROM    SO_DeliverySchedule";
				strSql += " INNER JOIN SO_SaleOrderDetail ON SO_SaleOrderDetail.SaleOrderDetailID = SO_DeliverySchedule.SaleOrderDetailID";
				strSql += " INNER JOIN MST_UnitOfMeasure ON SO_SaleOrderDetail.SellingUMID = MST_UnitOfMeasure.UnitOfMeasureID";
				strSql += " INNER JOIN ITM_Product ON SO_SaleOrderDetail.ProductID = ITM_Product.ProductID";
				strSql += " INNER JOIN SO_SaleOrderMaster ON SO_SaleOrderMaster.SaleOrderMasterID = SO_SaleOrderDetail.SaleOrderMasterID";
				strSql += " INNER JOIN MST_Party ON MST_Party.PartyID = SO_SaleOrderMaster.PartyID";
				strSql += " LEFT JOIN ITM_Category ON ITM_Product.CategoryID = ITM_Category.CategoryID";
			
				//Where clause
				strSql += " WHERE SO_SaleOrderMaster.CCNID = " + pstrCCNID;
				strSql += " AND Month(SO_DeliverySchedule.ScheduleDate) = " + pstrMonth;
				strSql += " AND Year(SO_DeliverySchedule.ScheduleDate) = " + pstrYear;

				if(pstrPartyID != null && pstrPartyID != string.Empty)
				{
					strSql += " AND MST_Party.PartyID = " + pstrPartyID;
				}

				if(pstrCategoryID != null && pstrCategoryID != string.Empty)
				{
					strSql += " AND ITM_Category.CategoryID = " + pstrCategoryID;
				}

				if(pstrProductID != null && pstrProductID != string.Empty)
				{
					strSql += " AND ITM_Product.ProductID = " + pstrProductID;
				}

				//Group by clause
				strSql += " GROUP BY Day(SO_DeliverySchedule.ScheduleDate),";
				strSql += " Month(SO_DeliverySchedule.ScheduleDate),";
				strSql += " Year(SO_DeliverySchedule.ScheduleDate),";
				strSql += " ITM_Product.Code,";
				strSql += " ITM_Product.Description,";
				strSql += " ITM_Category.Code,";
				strSql += " ITM_Product.Revision,";
				strSql += " MST_UnitOfMeasure.Code,";			
				strSql += " MST_Party.Code + ' (' + MST_Party.Name + ')'";

				strSql += " ORDER BY ITM_Category.Code, ITM_Product.Code ASC";
			
				oconPCS = new OleDbConnection(mConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
			
				ocmdPCS.Connection.Open();				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dtbResultTable);

				return dtbResultTable;			
			}
			catch (Exception ex)
			{
				throw ex;
			}
			finally
			{
				if (oconPCS != null)
				{
					if (oconPCS.State != ConnectionState.Closed)
						oconPCS.Close();
					oconPCS = null;

				}
			}
		}
		
		private void AdjustWidthAndLocation(ReportBuilder pobjReportBuilder, int piDaysInMonth)
		{
			try
			{
				#region Constants
			
				const string RPT_LINE_FLD  = "fldLine";
				const string RPT_DOT_LINE_FLD  = "fldDotLine";

				const string RPT_LINE0_FLD  = "fldLine0";
				const string RPT_LINE281_FLD  = "fldLine281";			
				const string RPT_LINE291_FLD  = "fldLine291";
				const string RPT_LINE292_FLD  = "fldLine292";
				const string RPT_LINE301_FLD  = "fldLine301";
				const string RPT_LINE302_FLD  = "fldLine302";
				const string RPT_LINE311_FLD  = "fldLine311";
				const string RPT_LINE312_FLD  = "fldLine312";
			
				const string RPT_FRAME1_FLD   = "fldFrame1";
				const string RPT_FRAME2_FLD   = "fldFrame2";
				const string RPT_PAGE_INFO_FLD = "fldPageInfo";
				const string RPT_TOTAL_HEADER_FLD = "fldTotalHeader";
				const string RPT_TOTAL_QUANTITY_FLD = "fldTotalQuantity";
			
				const int REPORT_LEFT_MARGIN  = 60;
				const int HEADER_LEFT_MARGIN  = 270;
				const int TOTAL_FIELD_WIDTH = 670;

				#endregion Constants
			
				switch(piDaysInMonth)
				{
					case 28:
						pobjReportBuilder.Report.Fields[RPT_LINE291_FLD].Visible = false;
						pobjReportBuilder.Report.Fields[RPT_LINE292_FLD].Visible = false;
						pobjReportBuilder.Report.Fields[RPT_LINE301_FLD].Visible = false;
						pobjReportBuilder.Report.Fields[RPT_LINE302_FLD].Visible = false;
						pobjReportBuilder.Report.Fields[RPT_LINE311_FLD].Visible = false;
						pobjReportBuilder.Report.Fields[RPT_LINE312_FLD].Visible = false;

						//Change horizontal line with
						//pobjReportBuilder.Report.Fields[RPT_LINE_FLD].Width = pobjReportBuilder.Report.Fields[RPT_LINE281_FLD].Left + TOTAL_FIELD_WIDTH - HEADER_LEFT_MARGIN;
						//Change header frame's with
						pobjReportBuilder.Report.Fields[RPT_FRAME1_FLD].Width = pobjReportBuilder.Report.Fields[RPT_LINE281_FLD].Left + TOTAL_FIELD_WIDTH - REPORT_LEFT_MARGIN;
						//Change detail frame's with
						pobjReportBuilder.Report.Fields[RPT_FRAME2_FLD].Width = pobjReportBuilder.Report.Fields[RPT_LINE281_FLD].Left + TOTAL_FIELD_WIDTH - REPORT_LEFT_MARGIN;
						//Change dot-line width
						pobjReportBuilder.Report.Fields[RPT_DOT_LINE_FLD].Width = pobjReportBuilder.Report.Fields[RPT_LINE281_FLD].Left - pobjReportBuilder.Report.Fields[RPT_LINE0_FLD].Left - 10;

						//Change total location	
						pobjReportBuilder.Report.Fields[RPT_TOTAL_HEADER_FLD].Left = pobjReportBuilder.Report.Fields[RPT_LINE281_FLD].Left;
						pobjReportBuilder.Report.Fields[RPT_TOTAL_QUANTITY_FLD].Left = pobjReportBuilder.Report.Fields[RPT_LINE281_FLD].Left;

						break;

					case 29:
						pobjReportBuilder.Report.Fields[RPT_LINE301_FLD].Visible = false;
						pobjReportBuilder.Report.Fields[RPT_LINE302_FLD].Visible = false;
						pobjReportBuilder.Report.Fields[RPT_LINE311_FLD].Visible = false;
						pobjReportBuilder.Report.Fields[RPT_LINE312_FLD].Visible = false;
					
						//Change horizontal line with
						//pobjReportBuilder.Report.Fields[RPT_LINE_FLD].Width = pobjReportBuilder.Report.Fields[RPT_LINE291_FLD].Left + TOTAL_FIELD_WIDTH - HEADER_LEFT_MARGIN;
						//Change header frame's with
						pobjReportBuilder.Report.Fields[RPT_FRAME1_FLD].Width = pobjReportBuilder.Report.Fields[RPT_LINE291_FLD].Left + TOTAL_FIELD_WIDTH - REPORT_LEFT_MARGIN;
						//Change detail frame's with
						pobjReportBuilder.Report.Fields[RPT_FRAME2_FLD].Width = pobjReportBuilder.Report.Fields[RPT_LINE291_FLD].Left + TOTAL_FIELD_WIDTH - REPORT_LEFT_MARGIN;
						//Change dot-line width
						pobjReportBuilder.Report.Fields[RPT_DOT_LINE_FLD].Width = pobjReportBuilder.Report.Fields[RPT_LINE291_FLD].Left - pobjReportBuilder.Report.Fields[RPT_LINE0_FLD].Left - 10;

						//Change total location	
						pobjReportBuilder.Report.Fields[RPT_TOTAL_HEADER_FLD].Left = pobjReportBuilder.Report.Fields[RPT_LINE291_FLD].Left;
						pobjReportBuilder.Report.Fields[RPT_TOTAL_QUANTITY_FLD].Left = pobjReportBuilder.Report.Fields[RPT_LINE291_FLD].Left;
						break;

					case 30:
						pobjReportBuilder.Report.Fields[RPT_LINE311_FLD].Visible = false;
						pobjReportBuilder.Report.Fields[RPT_LINE312_FLD].Visible = false;

						//Change horizontal line with
						//pobjReportBuilder.Report.Fields[RPT_LINE_FLD].Width = pobjReportBuilder.Report.Fields[RPT_LINE301_FLD].Left + TOTAL_FIELD_WIDTH - HEADER_LEFT_MARGIN;
						//Change header frame's with
						pobjReportBuilder.Report.Fields[RPT_FRAME1_FLD].Width = pobjReportBuilder.Report.Fields[RPT_LINE301_FLD].Left + TOTAL_FIELD_WIDTH - REPORT_LEFT_MARGIN;
						//Change detail frame's with
						pobjReportBuilder.Report.Fields[RPT_FRAME2_FLD].Width = pobjReportBuilder.Report.Fields[RPT_LINE301_FLD].Left + TOTAL_FIELD_WIDTH - REPORT_LEFT_MARGIN;
						//Change dot-line width
						pobjReportBuilder.Report.Fields[RPT_DOT_LINE_FLD].Width = pobjReportBuilder.Report.Fields[RPT_LINE301_FLD].Left - pobjReportBuilder.Report.Fields[RPT_LINE0_FLD].Left - 10;

						//Change total location	
						pobjReportBuilder.Report.Fields[RPT_TOTAL_HEADER_FLD].Left = pobjReportBuilder.Report.Fields[RPT_LINE301_FLD].Left;
						pobjReportBuilder.Report.Fields[RPT_TOTAL_QUANTITY_FLD].Left = pobjReportBuilder.Report.Fields[RPT_LINE301_FLD].Left;
						break;				
				}
			
				//Change page info's location
				pobjReportBuilder.Report.Fields[RPT_PAGE_INFO_FLD].Left = pobjReportBuilder.Report.Fields[RPT_FRAME2_FLD].Width + REPORT_LEFT_MARGIN - pobjReportBuilder.Report.Fields[RPT_PAGE_INFO_FLD].Width;			
			}
			catch (Exception ex)
			{
				throw ex;
			}			
		}

		/// <summary>
		/// Change header & column header of report
		/// </summary>
		/// <param name="pobjReportBuilder"></param>
		/// <param name="pintReportYear.ToString()"></param>
		/// <author> Tuan TQ, 07 Dec, 2005</author>
		private void ChangeReportDisplayInfo(ReportBuilder pobjReportBuilder, string pstrCCN, string pstrReportYear, string pstrReportMonth, string pstrPartyID, string pstrCategoryID, string pstrProductID)
		{
			try
			{
				#region Constants
			
				const string SHORT_DATE_FORMAT = "d-MMM";
						
				//Report Field's Name
				const string RPT_COMPANY_FLD  = "fldCompany";			
				const string RPT_PAGE_HEADER = "PageHeader";
				//Report field names
				const string RPT_TITLE_FIELD = "fldTitle";

				const string RPT_CCN_FLD = "CCN";
				const string RPT_MONTH_FLD = "Month";
				const string RPT_YEAR_FLD = "Year";
				const string RPT_PARTY_CODE_NAME_FLD = "Customer";
				const string RPT_CATEGORY_FLD = "Category";
				const string RPT_ITEM_INFO_FLD = "Item(Part No., Part Name, Model)";

				const string RPT_DAY_PREFIX  = "fldDay_";
				const string RPT_DAY_OF_WEEK_PREFIX  = "fldDayOfWeek_";			
				const string RPT_QUANTITY_PREFIX  = "fldQuantity_";			

				#endregion Constants
			
				//set start day in month
				int iReportYear = int.Parse(pstrReportYear);
				int iReportMonth = int.Parse(pstrReportMonth);

				DateTime dtmReportDay = new DateTime(iReportYear, iReportMonth, 1);
				int iDaysInMonth = DateTime.DaysInMonth(iReportYear, iReportMonth);
			
				//Change report header's value
				pobjReportBuilder.DrawPredefinedField(RPT_COMPANY_FLD, GetCompanyFullName());
				
				//Draw parameters				
				System.Collections.Specialized.NameValueCollection arrParamAndValue = new System.Collections.Specialized.NameValueCollection();
				
				arrParamAndValue.Add(RPT_CCN_FLD, GetCCNInfoByID(pstrCCN));
				
				arrParamAndValue.Add(RPT_MONTH_FLD, pstrReportMonth);
				arrParamAndValue.Add(RPT_YEAR_FLD, pstrReportYear);

				if(pstrPartyID != null && pstrPartyID != string.Empty)
				{
					arrParamAndValue.Add(RPT_PARTY_CODE_NAME_FLD, GetCustomerInfoByID(pstrPartyID));
				}				

				if(pstrCategoryID != null && pstrCategoryID != string.Empty)
				{
					arrParamAndValue.Add(RPT_CATEGORY_FLD, GetCategoryInfoByID(pstrCategoryID));
				}
				
				if(pstrProductID != null && pstrProductID != string.Empty)
				{
					string strItemInfo = string.Empty;
					Hashtable htbItem = GetProductInfoByID(pstrProductID);				
					if(htbItem != null)
					{
						strItemInfo = htbItem[PRODUCT_CODE].ToString();
						strItemInfo += ", " + htbItem[PRODUCT_NAME].ToString();
						strItemInfo += ", " + htbItem[PRODUCT_MODEL].ToString();
					}

					arrParamAndValue.Add(RPT_ITEM_INFO_FLD, strItemInfo);
				}				
				
				//Anchor the Parameter drawing canvas cordinate to the fldTitle
				C1.C1Report.Field fldTitle = pobjReportBuilder.GetFieldByName(RPT_TITLE_FIELD);
				double dblStartX = fldTitle.Left;
				double dblStartY = fldTitle.Top  + 1.3 * fldTitle.RenderHeight;
				pobjReportBuilder.GetSectionByName(RPT_PAGE_HEADER).CanGrow = true;
				pobjReportBuilder.DrawParameters(pobjReportBuilder.GetSectionByName(RPT_PAGE_HEADER), dblStartX, dblStartY, arrParamAndValue, pobjReportBuilder.Report.Font.Size);			

				//loop and change caption
				for(int i = 1; i <= iDaysInMonth; i++)
				{
					pobjReportBuilder.DrawPredefinedField(RPT_DAY_PREFIX + i, dtmReportDay.ToString(SHORT_DATE_FORMAT));
					pobjReportBuilder.DrawPredefinedField(RPT_DAY_OF_WEEK_PREFIX + i, dtmReportDay.DayOfWeek.ToString().Substring(0, 3));				

					if(dtmReportDay.DayOfWeek == DayOfWeek.Sunday)
					{
						pobjReportBuilder.Report.Fields[RPT_DAY_PREFIX + i.ToString()].BackColor = Color.Yellow;
						pobjReportBuilder.Report.Fields[RPT_DAY_PREFIX + i.ToString()].ForeColor = Color.Red;

						pobjReportBuilder.Report.Fields[RPT_DAY_OF_WEEK_PREFIX + i.ToString()].BackColor = Color.Yellow;
						pobjReportBuilder.Report.Fields[RPT_DAY_OF_WEEK_PREFIX + i.ToString()].ForeColor = Color.Red;
					
						//pobjReportBuilder.Report.Fields[RPT_QUANTITY_PREFIX + i.ToString()].Visible = false;
					}

					dtmReportDay = dtmReportDay.AddDays(1);
				}

				//Company Info
				//pobjReportBuilder.DrawPredefinedField(RPT_COMPANY_FLD, SystemProperty.SytemParams.Get(SystemParam.COMPANY_FULL_NAME));
			
				//Hide fields those are not displayed
				for(int i = iDaysInMonth + 1; i<= MAX_DAYS_IN_MONTH; i++)
				{
					pobjReportBuilder.Report.Fields[RPT_DAY_PREFIX + i.ToString()].Visible = false;
					pobjReportBuilder.Report.Fields[RPT_DAY_OF_WEEK_PREFIX + i.ToString()].Visible = false;
					pobjReportBuilder.Report.Fields[RPT_QUANTITY_PREFIX + i.ToString()].Visible = false;
				}			
			
				AdjustWidthAndLocation(pobjReportBuilder, iDaysInMonth);
			}
			catch (Exception ex)
			{
				throw ex;
			}
			
		}

		/// <summary>
		/// Create Delivery To Customer Schedule data template
		/// </summary>
		/// <returns></returns>
		/// <author> Tuan TQ, 06 Dec, 2005</author>
		private DataTable BuildDataTemplateTable()
		{
			try
			{
				DataTable dtbReport = new DataTable();

				//Add column						
				dtbReport.Columns.Add(DELIVERY_DAY_FLD, typeof(System.Int32));
				dtbReport.Columns.Add(DELIVERY_MONTH_FLD, typeof(System.Int32));
				dtbReport.Columns.Add(DELIVERY_YEAR_FLD, typeof(System.Int32));

				dtbReport.Columns.Add(PARTS_NUMBER_FLD, typeof(System.String));
				dtbReport.Columns.Add(PARTS_NAME_FLD, typeof(System.String));			
				dtbReport.Columns.Add(CATEGORY_FLD, typeof(System.String));
				dtbReport.Columns.Add(PARTS_MODEL_FLD, typeof(System.String));
				dtbReport.Columns.Add(PARTS_UM_FLD, typeof(System.String));
			
				dtbReport.Columns.Add(PARTY_CODE_NAME_FLD, typeof(System.String));			
			
				for(int i = 1; i <= MAX_DAYS_IN_MONTH; i++)
				{
					dtbReport.Columns.Add(DELIVERY_FLD_PREFIX + i.ToString(), typeof(System.Decimal));				
				}

				dtbReport.Columns.Add(TOTAL_DELIVERY_FLD, typeof(System.Decimal));			

				return dtbReport;
			}
			catch (Exception ex)
			{
				throw ex;
			}			
		}		

		/// <summary>
		/// Build Data table for Delivery To Customer Schedule Report
		/// </summary>
		/// <param name="pdtbData">Source Data</param>
		/// <returns>Data with template as data template of report</returns>
		/// <author> Tuan TQ, 06 Dec, 2005</author>
		private DataTable BuildReportTable(DataTable pdtbSourceData, int pintDaysInMonth)
		{
			
			try
			{
				PCSComUtils.Framework.ReportFrame.BO.C1PrintPreviewDialogBO boPrintPreview = new PCSComUtils.Framework.ReportFrame.BO.C1PrintPreviewDialogBO();

				DataTable dtbTransform = BuildDataTemplateTable();
				if(pdtbSourceData == null)
				{
					return dtbTransform;
				}
			
				//Collection of processed item
				ArrayList arlProcessedItems = new ArrayList();			
				DataRow[] arrItem = null;
			
				//select condition string
				string strSelectCondition = string.Empty;

				//keeps Product, UM
				string strProductUM = string.Empty;

				//indicate data will be added or modified
				bool blnFirstTime = false;

				foreach(DataRow dtRow in pdtbSourceData.Rows)
				{
					//Mark processed items
					strProductUM = dtRow[PARTS_NUMBER_FLD].ToString() + dtRow[PARTS_UM_FLD].ToString();

					if(!arlProcessedItems.Contains(strProductUM))
					{
						strSelectCondition = PARTS_NUMBER_FLD + "='" + dtRow[PARTS_NUMBER_FLD].ToString().Replace("'", "''") + "'";
					
						if(!dtRow[PARTS_UM_FLD].Equals(DBNull.Value))
						{
							strSelectCondition += " AND " + PARTS_UM_FLD + "='" + dtRow[PARTS_UM_FLD].ToString().Replace("'", "''") + "'";
						}
						else
						{
							strSelectCondition += " AND " + PARTS_UM_FLD + " IS NULL";
						}					

						//Select data related by select condition
						arrItem = pdtbSourceData.Select(strSelectCondition);
					
						//reset firt time flag
						blnFirstTime = true;

						//loop in result table and process data
						for(int i = 0; i < arrItem.Length; i++)
						{						
							InsertRow2ReportTable(arrItem[i], dtbTransform, blnFirstTime, pintDaysInMonth);
							blnFirstTime = false;
						}

						//Add item to collection as processed
						arlProcessedItems.Add(strProductUM);
					}
				}		

				return dtbTransform;
			}
			catch (Exception ex)
			{
				throw ex;
			}			
		}		
		
		/// <summary>
		/// Insert or update row into report data table
		/// </summary>
		/// <param name="ptdrSourceRow"></param>
		/// <param name="pdtbReporTable"></param>
		/// <param name="pblnFirstTime"></param>
		/// <author> Tuan TQ, 05 Dec, 2005</author>
		private void InsertRow2ReportTable(DataRow ptdrSourceRow, DataTable pdtbReporTable, bool pblnFirstTime, int pintDaysInMonth)
		{
			try
			{
				DataRow dtrNewRow;

				//First time means insert new row
				if(pblnFirstTime)
				{
					dtrNewRow = pdtbReporTable.NewRow();

					dtrNewRow[DELIVERY_DAY_FLD]  = ptdrSourceRow[DELIVERY_DAY_FLD];
					dtrNewRow[DELIVERY_YEAR_FLD] = ptdrSourceRow[DELIVERY_YEAR_FLD];
					dtrNewRow[DELIVERY_MONTH_FLD] = ptdrSourceRow[DELIVERY_MONTH_FLD];

					dtrNewRow[PARTS_MODEL_FLD] = ptdrSourceRow[PARTS_MODEL_FLD];
					dtrNewRow[PARTS_NAME_FLD] = ptdrSourceRow[PARTS_NAME_FLD];
					dtrNewRow[PARTS_NUMBER_FLD] = ptdrSourceRow[PARTS_NUMBER_FLD];
					dtrNewRow[PARTS_UM_FLD] = ptdrSourceRow[PARTS_UM_FLD];

					dtrNewRow[CATEGORY_FLD] = ptdrSourceRow[CATEGORY_FLD];
				
					dtrNewRow[PARTY_CODE_NAME_FLD] = ptdrSourceRow[PARTY_CODE_NAME_FLD];
				
					//Set 0 to other quantity columns
					for(int i =1; i <= pintDaysInMonth; i++)
					{
						dtrNewRow[DELIVERY_FLD_PREFIX + i] = DBNull.Value;
					}

					//dtrNewRow[TOTAL_CAPACITY_FLD] = DBNull.Value;
					dtrNewRow[TOTAL_DELIVERY_FLD] = decimal.Zero;

					//Add to colection
					pdtbReporTable.Rows.Add(dtrNewRow);
				}
				else
				{
					//Update data of last row
					dtrNewRow = pdtbReporTable.Rows[pdtbReporTable.Rows.Count - 1];
				}

				//Get deleivery day from data source
				int iDeliveryDay = int.Parse(ptdrSourceRow[DELIVERY_DAY_FLD].ToString());
			
				//Assign delivery quantity
				dtrNewRow[DELIVERY_FLD_PREFIX + iDeliveryDay.ToString()] = ptdrSourceRow[SO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD];
			
				//Sum total delivery of item
				if(!ptdrSourceRow[SO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD].Equals(DBNull.Value)
					&& !ptdrSourceRow[SO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD].ToString().Equals(string.Empty))
				{
					dtrNewRow[TOTAL_DELIVERY_FLD] = decimal.Parse(dtrNewRow[TOTAL_DELIVERY_FLD].ToString()) + decimal.Parse(ptdrSourceRow[SO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD].ToString());
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
			
		}		

		#endregion Delivery To Customer Schedule Report: Tuan TQ
		
		#region Public Method

		public object Invoke(string pstrMethod, object[] pobjParameters)
		{
			return this.GetType().InvokeMember(pstrMethod, BindingFlags.InvokeMethod, null, this, pobjParameters);
		}
		
		/// <summary>
		/// Build and show Delivery To Customer Report
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author> Tuan TQ, 06 Dec, 2005</author>
		public DataTable ExecuteReport(string pstrCCNID, string pstrMonth, string pstrYear, string pstrPartyID, string pstrCategoryID, string pstrProductID)
		{
			try
			{
				const string DELIVERY_TO_CUSTOMER_REPORT = "Delivery2CustomerByMonthReport.xml";
				const string REPORT_NAME = "Delivery2CustomerByMonthReport";
				const string RPT_TITLE_FIELD = "fldTitle";

				DataTable dtbResult = null;
				DataTable dtbTransform = null;

				int iReportMonth = int.Parse(pstrMonth);
				int iReportYear  = int.Parse(pstrYear);
			
				//Get delivery to customer data
				dtbResult = GetDelivery2CustomerData(pstrCCNID, pstrMonth, pstrYear, pstrPartyID, pstrCategoryID, pstrProductID);
			
				//Return if data is null or no data
				if(dtbResult == null)
				{
					return null;
				}
			
				//Get total days in report month
				int iDaysInMonth = DateTime.DaysInMonth(iReportYear, iReportMonth);

				//Transform Data
				dtbTransform = BuildReportTable(dtbResult, iDaysInMonth);
			
				//Return if data is null or no data
				if(dtbTransform == null)
				{
					return null;
				}

				//Create builder object
				ReportWithSubReportBuilder reportBuilder = new ReportWithSubReportBuilder();
			
				//Set report name
				reportBuilder.ReportName = REPORT_NAME;
				//Set Datasource
				reportBuilder.SourceDataTable = dtbTransform;
				//Set report layout location
				reportBuilder.ReportDefinitionFolder = mstrReportDefinitionFolder;
				reportBuilder.ReportLayoutFile = DELIVERY_TO_CUSTOMER_REPORT;				
				
				reportBuilder.UseLayoutFile = true;
				reportBuilder.MakeDataTableForRender();

				// and show it in preview dialog				
				PCSUtils.Framework.ReportFrame.C1PrintPreviewDialog	printPreview = new PCSUtils.Framework.ReportFrame.C1PrintPreviewDialog();
								
				//Attach report viewer
				reportBuilder.ReportViewer = printPreview.ReportViewer;				
				reportBuilder.RenderReport();
				
				//Change report header & column header
				ChangeReportDisplayInfo(reportBuilder, pstrCCNID, pstrYear, pstrMonth, pstrPartyID, pstrCategoryID, pstrProductID);
				
				try
				{
					printPreview.FormTitle = reportBuilder.GetFieldByName(RPT_TITLE_FIELD).Text;
				}
				catch{}

				reportBuilder.RefreshReport();
				printPreview.Show();

				//return table
				return dtbResult;
			}
			catch (Exception ex)
			{
				throw ex;
			}			
		}
		
		#endregion Public Method	
		
	}
}