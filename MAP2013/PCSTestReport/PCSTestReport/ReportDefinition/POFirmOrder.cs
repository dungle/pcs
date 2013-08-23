using System;
using System.Collections;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.Drawing;
using System.Reflection;
using C1.Win.C1Preview;
using C1.C1Report;
using PCSComUtils.Common;
using PCSUtils.Framework.ReportFrame;
using PCSUtils.Utils;
using C1PrintPreviewDialog = PCSUtils.Framework.ReportFrame.C1PrintPreviewDialog;

namespace POFirmOrder
{
	/// <summary>
	/// <author>Thachnn</author>	
	/// It combines severals .NET DataTables with C1Report
	/// The last line I drop here: DON"T BELIEVE IN 3rd Vendor COmponent Provider. C1REport is blsht when you processing huge report.
	/// </summary>
	[Serializable]	
	public class POFirmOrder : MarshalByRefObject, IDynamicReport		            
	{
		#region IDynamicReport Implementation
	
		private string mConnectionString;
		private ReportBuilder mReportBuilder = new ReportBuilder();		
		private C1PrintPreviewControl mReportViewer;
		private bool mUseReportViewerRenderEngine = false;	

		private string mstrReportDefinitionFolder = string.Empty;
		string mstrReportLayoutFile = string.Empty;

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
		/// this IDynamicReport 
		/// or the ReportViewer Engine (in the ReportViewer form) 
		/// </summary> 		
		public bool UseReportViewerRenderEngine { get { return mUseReportViewerRenderEngine; } set { mUseReportViewerRenderEngine = value; } }

		
		/// <summary>
		/// Inform External Process where to find out the ReportLayout	 ( the PCS' ReportDefinition Folder Path )
		/// </summary>				
		public string ReportDefinitionFolder
		{
			get 
			{
				return mstrReportDefinitionFolder;
			}
			set
			{
				mstrReportDefinitionFolder = value;
			}
		}


	
		/// <summary>
		/// Inform External Process where to find out the ReportLayout	 ( the PCS' ReportDefinition Folder Path )
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

		public object Invoke(string pstrMethod, object[] pobjParameters)
		{			
			return this.GetType().InvokeMember(pstrMethod, BindingFlags.InvokeMethod, null, this, pobjParameters);
		}


		#endregion

		public POFirmOrder()
		{
		}	
	

		#region GLOBAL CONSTANT
		
		const string TABLE_NAME = "POFirmOrder";	
		const string REPORT_NAME = "POFirmOrder";
		short COPIES = 1;
		const string REPORTFLD_PARAMETER_ELEMENT					= "fldParameterPurchaseOrder";		


		const string REPORTFLD_REQUEST_DELIVERY_TO = LBL + "RequestDeliveryTimeToXXX";
		const string REPORTFLD_UNITPRICE = LBL + "UnitPrice";
		const string REPORTFLD_NEXTMONTH_VALUE = LBL + "NextMonthValue";
		const string REPORTFLD_NEXTNEXTMONTH_VALUE = LBL + "NextNextMonthValue";
		const string REPORTFLD_CUSTOMER_CONFIRM = LBL + "CustomerConfirm";
		const string REPORTFLD_TRANSDATE = LBL + "TransDate";

		const string REPORTFLD_MESRSS = LBL + "Mesrss";
		const string REPORTFLD_MESRSS_ATTN = LBL + "MesrssAttn";
		const string REPORTFLD_CC = LBL + "Cc";
		const string REPORTFLD_CC_ATTN = LBL + "CcAttn";
		const string REPORTFLD_CC_FAX = LBL + "CcFax";


		/// Result Data Table Column names, contain:
		/// PRODUCTID, CATEGORY,PARTNO,MODEL,BEGIN
		/// DATE
		/// PLAN+QUANTITY, ACTUAL+QUANTITY
		const string PRODUCTID = "ProductID";
		const string ITEMNO = "ItemNo";
		const string PARTNO = "PartNo";
		const string PARTNAME = "PartName";
		const string QTYSET = "QtySet";
		const string UNITPRICE = "UnitPrice";

		const string DATE = "Day";
		const string QUANTITY = "Quantity";	
		const string NEXTMONTH = "NextMonth";
		const string NEXTNEXTMONTH = "NextNextMonth";		

		const string PREFIX_DAYINMONTH = "lblDayInMonth";
		const string PREFIX_DAYINMONTHNEXT = "lblDayInMonthNext";
		const string PREFIX_LOT_HEADING = "fldLot";
		const string PREFIX_QTYP_HEADING = "fldQtyP";
		const string PREFIX_QTYS_HEADING = "fldQtyS";
		const string PLAN = "PO";
		const string FLD = "fld";		
		const string LBL = "lbl";
		const string REPORTFLD_TITLE = FLD + "Title";
        	
		string PO_TABLE_NAME = "POTable";
		string NEXTMONTH_TABLE_NAME = "NextMonthTable";
		string NEXTNEXTMONTH_TABLE_NAME = "NextNextMonthTable";	
		string MISC_INFO_TABLE_NAME = "MiscInfoTable";
		const string MY_NUMBERFORMAT = "##############,0.0000";

		#endregion GLOBAL CONSTANT

		#region GLOBAL VAR	

		DataSet dstMAIN = new DataSet();	

		#endregion GLOBAL VAR

		/// <summary>
		/// Main function, generate the result data Table for the REPORT VIEWER
		/// Modify the REPORT VIEWER to display the report
		/// </summary>
		/// <param name="pstrCCNID"></param>
		/// <param name="pstrYear"></param>
		/// <param name="pstrMonth"></param>
		/// <returns></returns>
		public DataTable ExecuteReport(string pstrCCNID, string pstrYear, string pstrMonth, string pstrVendorID, string pstrPurchaseOrderID)
		{
			#region My variables						

			int nMonth = int.Parse(pstrMonth);
			int nYear = int.Parse(pstrYear);

			//  for display on the Report parameter Section
			string strReportParameter_PurchaseOrder = string.Empty;						

			/// contain array of string: Name of the column (with days have data in the dtbSourceData)
			/// FOr Example:
			/// dtbSourceData contain: 01-Oct: has Plan Quantity
			/// 02-Oct has Actual Quantity
			/// So arrHasValueDateHeading contain: Plan01, Actual02
			ArrayList arrHasValueDateHeading = new ArrayList();
			ArrayList arrHasValueDateHeadingForLot = new ArrayList();
			
			// get data and cache all in the dstMAIN			
			dstMAIN = GetDataAndCache(pstrCCNID, pstrYear, pstrMonth, pstrVendorID, pstrPurchaseOrderID);	
			dstMAIN.DataSetName = pstrCCNID + pstrYear + pstrMonth + pstrPurchaseOrderID;

			DataTable dtbPOTable;
			dtbPOTable  = dstMAIN.Tables[PO_TABLE_NAME];					
			DataTable dtbNextMonthTable;
			dtbNextMonthTable = dstMAIN.Tables[NEXTMONTH_TABLE_NAME];
			DataTable dtbNextNextMonthTable;
			dtbNextNextMonthTable = dstMAIN.Tables[NEXTNEXTMONTH_TABLE_NAME];
		
			DataTable 	dtbOtherInfo = dstMAIN.Tables[MISC_INFO_TABLE_NAME];
            string strPOMasterVendorCode = string.Empty;
			string strPOMasterPricingType = string.Empty;
			string strTransDate = string.Empty;
			string strCurrency = string.Empty;
			int nRequestDeliveryTimeAmount = 0;

			string strPOMasterVendorName  = string.Empty;
			string strPOMasterVendorContactName    = string.Empty;
			string strPOMasterMakerName    = string.Empty;
			string strPOMasterMakerContactName  = string.Empty;
			string strContactFax  = string.Empty;
			string strPartyID = string.Empty;

			try
			{
				strPOMasterVendorCode  = dtbOtherInfo.Rows[0][0].ToString();
				strPOMasterPricingType  = dtbOtherInfo.Rows[0][1].ToString();
				nRequestDeliveryTimeAmount = ReportBuilder.ToInt32(dtbOtherInfo.Rows[0][2] );				
				strCurrency = dtbOtherInfo.Rows[0][4].ToString();

				strPOMasterVendorName  = dtbOtherInfo.Rows[0][5].ToString();
				strPOMasterVendorContactName  = dtbOtherInfo.Rows[0][6].ToString();
				strPOMasterMakerName  = dtbOtherInfo.Rows[0][7].ToString();
				strPOMasterMakerContactName  = dtbOtherInfo.Rows[0][8].ToString();
				strContactFax  = dtbOtherInfo.Rows[0][9].ToString();
				strPartyID  = dtbOtherInfo.Rows[0][10].ToString();
				strReportParameter_PurchaseOrder = "No." + dtbOtherInfo.Rows[0][11].ToString();

				strTransDate = ((DateTime)dtbOtherInfo.Rows[0][3]).ToString("dd-MMM-yyyy");
			}
			catch{}
			#endregion  My Variables
			
			/// transform TABLE column names
			/// transform TABLE will contain :
			/// PRODUCTID, 
			/// META INFO  = ,
			/// PLAN+i.ToString("00")
			/// TOTAL, NEXTMONTH, NEXTNEXTMONTH			
			#region TRANSFORM ORIGINAL TABLE FOR REPORT		
	
			#region GETTING THE DATE HEADING
			/// arrPlanDate contain DateTime object from actual dtbSourceData
			ArrayList arrPlanDate = GetColumnValuesFromTable(dtbPOTable,PLAN+DATE);
			arrPlanDate.Sort();	// IMPORTANT>   sort, then arrPlanDate from 1 - - 31, then arrDateHeading will be sorted.

			DataTable dtbPartyItemList = GetPartyItemList(string.Empty);
			DataRow[] drowPartyItemList = dtbPartyItemList.Select("PartyID = " + strPartyID);

			//ArrayList arrItems = GetCategory_PartNo_Model_ProductID_FromTable(dtbPOTable,CATEGORY,PARTNO,MODEL,PRODUCTID);
			ArrayList arrItems = GetProductIDValuesFromTable(dtbPOTable, drowPartyItemList, PRODUCTID);

			/// PUSH: has-value (in the dtbSourceData) to the arrHasValueDateHeading
			/// 
			/// HACKED: Thachnn: 20/12/2005
			/// don't remove this dummy code of casting object in arrPlanDate to int
			/// because sometime, data in the database is not correct, return dbnull to the arrPlanDate. If we use normal foreach(int nDay in arrPlanDate)
			/// exception of Invalid cast will be throw
			/// In this case: ActualDate can be omit and = DBNull because an Item can be Plan, but it didn't produce in any day in this month
			foreach(object obj  in arrPlanDate)
			{
				try
				{
					int nDay = (int)obj;
					DateTime dtm = new DateTime(nYear,nMonth,nDay);
					string strColumnName = PLAN + dtm.Day.ToString("00");
					string strSColumnName = PLAN + "S" + dtm.Day.ToString("00");
					arrHasValueDateHeading.Add(strColumnName);
					arrHasValueDateHeadingForLot.Add(strColumnName);
					arrHasValueDateHeading.Add(strSColumnName);
				}
				catch{}
			}			
			/// ENDHACKED: Thachnn: 20/12/2005
			/// after this snip of code. arrHasValueDateHeading will contain Plan03 Plan04... 
			/// depend on the DataTable
			/// Which day has value (Plan ), the columnName will exist in the arrHasValueDateHeading
			/// and then, the Transform DataTable dtbTransform will has some columns named like string in arrHasValueDateHeading			

			#endregion		
            			
			DataTable dtbTransform = BuildTransformTable(arrHasValueDateHeading);
		
			#endregion  TRANSFORM ORIGINAL TABLE FOR REPORT
						
			#region FILL ABSOLUTE DATA FROM Plan  to the TRANSFORM DATATABLE
			
			/// GUIDE: with each Items
			foreach(object obj in arrItems)
			{
				string strItem = obj.ToString();
				bool blnIsMass = IsMassOrder(dtbPartyItemList, strItem);
				// Create DUMMYROW FIRST
				DataRow dtrNew = dtbTransform.NewRow();

				// because of: meta table , also contains data
				#region	- fill ITEM meta info  and Data to the new dummy row
				
				string strFilterMeta = string.Empty;
				
				strFilterMeta = string.Format("[{0}]='{1}' ",		
					PRODUCTID,	strItem);

				/// GUIDE: get all rows of this Item from the dtbSourceData
				DataRow[] dtrows = dtbPOTable.Select(strFilterMeta);
				decimal decQtySet = 0;

				/// GUIDE: for each rows in result (datarow contain map ProductID -- MetaInfo)
				foreach(DataRow dtr in dtrows)
				{
					// fill data to the dummy row
					dtrNew[PRODUCTID] = dtr[PRODUCTID];
					dtrNew[ITEMNO] = dtr[ITEMNO];
					dtrNew[PARTNO] = dtr[PARTNO];
					dtrNew[PARTNAME] = dtr[PARTNAME];
					dtrNew[QTYSET] = dtr[QTYSET];
					try
					{
						decQtySet = Convert.ToDecimal(dtr[QTYSET]);
					}
					catch{}

					string strUnitPrice = string.Empty;
					try
					{
						strUnitPrice = Convert.ToDecimal(dtr[UNITPRICE]).ToString(MY_NUMBERFORMAT);
					}
					catch{}
					dtrNew[UNITPRICE] = strCurrency + " " + strUnitPrice;

					break;
				}

				if (dtrows.Length == 0)
				{
					DataRow[] dtr = dtbPartyItemList.Select("ProductID = " + strItem);
					// fill data to the dummy row
					dtrNew[PRODUCTID] = dtr[0][PRODUCTID];
					dtrNew[ITEMNO] = dtr[0][ITEMNO];
					dtrNew[PARTNO] = dtr[0][PARTNO];
					dtrNew[PARTNAME] = dtr[0][PARTNAME];
					dtrNew[QTYSET] = dtr[0][QTYSET];
					try
					{
						decQtySet = Convert.ToDecimal(dtr[0][QTYSET]);
					}
					catch{}

					dtrNew[UNITPRICE] = strCurrency + " 0.0000";
				}

				#endregion	- fill ITEM meta info to the new dummy row
			
				#region	- fill PLAN quantity to the new dummy row				
								
				string strFilterPlan = string.Empty;
				
				strFilterPlan = 
					string.Format("[{0}]='{1}' ",					
					PRODUCTID,	strItem	);			
				
				/// GUIDE: get all rows of this Item from the dtbPlan
				DataRow[] dtrowsPlan = dtbPOTable.Select(strFilterPlan);

				/// GUIDE: for each rows in of this Item OF dtbPlan - fill plan quantity ITEM
				foreach(DataRow dtr in dtrowsPlan)
				{					
					/// Fill Plan Quantity to destination column of Transform table, in this new rows					
					string strDateColumnToFill = PLAN + Convert.ToInt32( dtr[PLAN+DATE]).ToString("00");
					string strSDateColumnToFill = PLAN + "S" + Convert.ToInt32( dtr[PLAN+DATE]).ToString("00");
					decimal decPlanQuantity = 0;
					try
					{
						decPlanQuantity = Convert.ToDecimal(dtr[PLAN+QUANTITY]);
					}
					catch{}
					if (decQtySet != 0)
						decPlanQuantity = decPlanQuantity / decQtySet;
					dtrNew[strDateColumnToFill] = decPlanQuantity;
					dtrNew[strSDateColumnToFill] = dtr[PLAN+QUANTITY];
				}

				#endregion - fill PLAN quantity to the new dummy row
		
				#region - fill NextMonth quantity to the new dummy row
				
				string strFilterNEXTMONTH = string.Empty;
				strFilterNEXTMONTH = 
					string.Format("[{0}]='{1}' ",
					PRODUCTID,	strItem	);		
				
				/// GUIDE: get all rows of this Item from the dtbNextMonth
				DataRow[] dtrowsNEXTMONTH = dtbNextMonthTable.Select(strFilterNEXTMONTH);
				if (dtrowsNEXTMONTH.Length == 0 && blnIsMass)
				{
					/// Fill NEXTMONTH Quantity to destination column of Transform table, in this new rows
					string strDateColumnToFill = NEXTMONTH;
					string strDateColumnToFill1 = NEXTMONTH + "P";
					dtrNew[strDateColumnToFill] = 0;
					dtrNew[strDateColumnToFill1] = 0;
				}

				/// GUIDE: for each rows  of this Item in NEXTMONTH DataTable- fill actual quantity to the dummy ROW
				foreach(DataRow dtr in dtrowsNEXTMONTH)
				{
					/// Fill NEXTMONTH Quantity to destination column of Transform table, in this new rows
					string strDateColumnToFill = NEXTMONTH;
					string strDateColumnToFill1 = NEXTMONTH + "P";
					decimal decNextMonth = 0, decNextMonthSet = 0;
					try
					{
						decNextMonth = Convert.ToDecimal(dtr[NEXTMONTH]);
					}
					catch{}
					if (decQtySet != 0)
						decNextMonthSet = decNextMonth / decQtySet;
					if (blnIsMass)
					{
						dtrNew[strDateColumnToFill] = decNextMonthSet;
						dtrNew[strDateColumnToFill1] = decNextMonth;
					}
					else
					{
						if (decNextMonthSet != 0)
							dtrNew[strDateColumnToFill] = decNextMonthSet;
						dtrNew[strDateColumnToFill1] = dtr[NEXTMONTH];
					}
				}
				#endregion - fill NEXTMONTH quantity to the new dummy row

				#region - fill NextNextMonth quantity to the new dummy row
				
				string strFilterNEXTNEXTMONTH = string.Empty;
				strFilterNEXTNEXTMONTH = 
					string.Format("[{0}]='{1}' ",
					PRODUCTID,	strItem	);		
				
				/// GUIDE: get all rows of this Item from the dtbNextNextMonth
				DataRow[] dtrowsNEXTNEXTMONTH = dtbNextNextMonthTable.Select(strFilterNEXTNEXTMONTH);

				if (dtrowsNEXTNEXTMONTH.Length == 0 && blnIsMass)
				{
					/// Fill NEXTNEXTMONTH Quantity to destination column of Transform table, in this new rows
					string strDateColumnToFill = NEXTNEXTMONTH;
					string strDateColumnToFill1 = NEXTNEXTMONTH + "P";
					dtrNew[strDateColumnToFill] = 0;
					dtrNew[strDateColumnToFill1] = 0;
				}
				/// GUIDE: for each rows  of this Item in NEXTNEXTMONTH DataTable- fill actual quantity to the dummy ROW
				foreach(DataRow dtr in dtrowsNEXTNEXTMONTH)
				{
					/// Fill NEXTNEXTMONTH Quantity to destination column of Transform table, in this new rows
					string strDateColumnToFill = NEXTNEXTMONTH;
					string strDateColumnToFill1 = NEXTNEXTMONTH + "P";
					decimal decNextNextMonth = 0, decNextNextMonthSet = 0;
					try
					{
						decNextNextMonth = Convert.ToDecimal(dtr[NEXTNEXTMONTH]);
					}
					catch{}
					if (decQtySet != 0)
						decNextNextMonthSet = decNextNextMonth / decQtySet;
					if (blnIsMass)
					{
						dtrNew[strDateColumnToFill] = decNextNextMonthSet;
						dtrNew[strDateColumnToFill1] = decNextNextMonth;
					}
					else
					{
						if (decNextNextMonthSet != 0)
							dtrNew[strDateColumnToFill] = decNextNextMonthSet;
						dtrNew[strDateColumnToFill1] = dtr[NEXTNEXTMONTH];
					}
				}
				#endregion - fill NEXTNEXTMONTH quantity to the new dummy row

				// add to the transform data table
				dtbTransform.Rows.Add(dtrNew);				
			}	    
			#endregion FILL DATA FROM Plan DTB && ActualCompletion DTB && Adjust DTB to the TRANSFORM DATATABLE
			
			#region RENDER REPORT
			
			// refine data for mass order item
			foreach (DataRow drowData in dtbTransform.Rows)
			{
				string strItem = drowData["ProductID"].ToString();
				if (IsMassOrder(dtbPartyItemList, strItem))
				{
					for (int i = 1; i <= 31; i++)
					{
						string strDateColumnToFill = PLAN + i.ToString("00");
						string strSDateColumnToFill = PLAN + "S" + i.ToString("00");
						decimal decPlanQuantity = 0, decPlanSQuantity = 0;
						try
						{
							decPlanQuantity = Convert.ToDecimal(drowData[strDateColumnToFill]);
						}
						catch{}
						try
						{
							decPlanSQuantity = Convert.ToDecimal(drowData[strSDateColumnToFill]);
						}
						catch{}
						drowData[strDateColumnToFill] = decPlanQuantity;
						drowData[strSDateColumnToFill] = decPlanSQuantity;
					}
				}
			}
			ReportBuilder objRB = mReportBuilder; //new ReportBuilder();
			objRB.ReportName = REPORT_NAME;
			objRB.SourceDataTable = dtbTransform;
			
			#region INIT REPORT BUIDER OBJECT
			try
			{
				objRB.ReportDefinitionFolder = mstrReportDefinitionFolder;
				objRB.ReportLayoutFile = mstrReportLayoutFile;
				if(objRB.AnalyseLayoutFile() == false)
				{
					//					PCSMessageBox.Show(ErrorCode.MESSAGE_REPORT_TEMPLATE_FILE_NOT_FOUND, MessageBoxIcon.Error);
					return new DataTable();
				}
				//objRB.UseLayoutFile = objRB.AnalyseLayoutFile();	// use layout file if any , auto drawing if not found layout file
				objRB.UseLayoutFile = true;	// always use layout file
			}
			catch
			{
				objRB.UseLayoutFile = false;
				//				PCSMessageBox.Show(ErrorCode.MESSAGE_REPORT_TEMPLATE_FILE_NOT_FOUND,MessageBoxIcon.Error);
			}

			#endregion				
		
			objRB.MakeDataTableForRender();
				
			// and show it in preview dialog				
			C1PrintPreviewDialog printPreview = new C1PrintPreviewDialog();				
			printPreview.FormTitle = REPORT_NAME;
			objRB.ReportViewer = printPreview.ReportViewer;			
			objRB.RenderReport();
			
			#region MODIFY THE REPORT LAYOUT - do not modify data value on report from now on

			#region PUSH some VALUEs  to LABELS
			
			objRB.DrawPredefinedField(REPORTFLD_PARAMETER_ELEMENT, strReportParameter_PurchaseOrder);
			objRB.DrawPredefinedField(REPORTFLD_MESRSS, strPOMasterVendorName);
			objRB.DrawPredefinedField(REPORTFLD_MESRSS_ATTN, strPOMasterVendorContactName);
			objRB.DrawPredefinedField(REPORTFLD_CC, strPOMasterMakerName);
			objRB.DrawPredefinedField(REPORTFLD_CC_ATTN, strPOMasterMakerContactName);
			objRB.DrawPredefinedField(REPORTFLD_CC_FAX, strContactFax);

			objRB.DrawPredefinedField(REPORTFLD_UNITPRICE, objRB.GetFieldByName(REPORTFLD_UNITPRICE).Text + "\n(" + strPOMasterPricingType + ")");
			objRB.DrawPredefinedField(REPORTFLD_REQUEST_DELIVERY_TO, objRB.GetFieldByName(REPORTFLD_REQUEST_DELIVERY_TO).Text + strPOMasterVendorCode  );
			objRB.DrawPredefinedField(REPORTFLD_TRANSDATE,  strTransDate  );
			
			DateTime dtmTemp = new DateTime(nYear,nMonth,1);
			dtmTemp = dtmTemp.AddMonths(-1);
			objRB.DrawPredefinedField("lblBlank", dtmTemp.ToString("MMM-yyyy") );
			objRB.DrawPredefinedField(REPORTFLD_NEXTMONTH_VALUE, dtmTemp.AddMonths(1).ToString("MMM-yyyy") );
			objRB.DrawPredefinedField(REPORTFLD_NEXTNEXTMONTH_VALUE, dtmTemp.AddMonths(2).ToString("MMM-yyyy") );
			
			objRB.DrawPredefinedField(REPORTFLD_CUSTOMER_CONFIRM, strPOMasterVendorCode);
            
			#endregion		 PUSH some VALUEs  to LABELS

			#region FILL THE REQUEST DELIVERY TIME (2 LINES)			
			for(int i = 0; i <= 31; i++) /// clear the heading text
			{
				objRB.DrawPredefinedField(PREFIX_DAYINMONTH  +i.ToString(ReportBuilder.FORMAT_DAY_2CHAR),"");
				//objRB.DrawPredefinedField(PREFIX_DAYINMONTHNEXT  +i.ToString(ReportBuilder.FORMAT_DAY_2CHAR),"");
			}

			#region			draw date time list at the bottom of the report
			
			string SUNDAY_3CHAR = "Sun";
			Color SUNDAY_FORECOLOR = Color.Red;
			Color SUNDAY_BACKCOLOR = Color.Yellow;
			
			for(int i = 1 ; i <= DateTime.DaysInMonth(nYear,nMonth) ; i++)
			{
				try
				{	
					DateTime dtm = new DateTime(nYear, nMonth, i );
					// gen string
					string strDayInMonth = 
						i.ToString(ReportBuilder.FORMAT_DAY_2CHAR) + ReportBuilder.SEPERATOR_DATETIME +  
						dtm.ToString(ReportBuilder.FORMAT_MONTH_3CHAR) + ReportBuilder.SEPERATOR_DATETIME +  
						dtm.ToString("yy");											
					// draw
					objRB.DrawPredefinedField(PREFIX_DAYINMONTH +i.ToString(ReportBuilder.FORMAT_DAY_2CHAR), strDayInMonth);				
					
					#region		Make Sunday RedText on Yellow Background
					string strDayOfWeek = dtm.DayOfWeek.ToString().Substring(0,3);
					if(strDayOfWeek == SUNDAY_3CHAR)
					{
						Field fldSunDay ;

						fldSunDay = objRB.GetFieldByName(PREFIX_DAYINMONTH +i.ToString(ReportBuilder. FORMAT_DAY_2CHAR) );						
						fldSunDay.BackColor = SUNDAY_BACKCOLOR;
						fldSunDay.ForeColor = SUNDAY_FORECOLOR;				
					}

					#endregion	Make Sunday RedText on Yellow Background
                    // subtract with nRequestDeliveryTimeAmount
                    dtm = dtm.AddDays( 0 - nRequestDeliveryTimeAmount /*negate the nRequestDeliveryTimeAmount*/ );
					string strDayInMonthNext = 
						dtm.ToString("dd") + ReportBuilder.SEPERATOR_DATETIME +  
						dtm.ToString(ReportBuilder.FORMAT_MONTH_3CHAR) + ReportBuilder.SEPERATOR_DATETIME +  
						dtm.ToString("yy");											
					// draw
					//objRB.DrawPredefinedField(PREFIX_DAYINMONTHNEXT +i.ToString(ReportBuilder.FORMAT_DAY_2CHAR), strDayInMonthNext);

					#region		Make NEXT DAY    Sunday RedText on Yellow Background
					string strDayOfWeekNext = dtm.DayOfWeek.ToString().Substring(0,3);
					if(strDayOfWeekNext == SUNDAY_3CHAR)
					{
						Field fldSunDay ;

						fldSunDay = objRB.GetFieldByName(PREFIX_DAYINMONTHNEXT +i.ToString(ReportBuilder. FORMAT_DAY_2CHAR) );
						fldSunDay.BackColor = SUNDAY_BACKCOLOR;
						fldSunDay.ForeColor = SUNDAY_FORECOLOR;
					}

					#endregion	Make Sunday RedText on Yellow Background


				}
				catch{}
			}

			#endregion	draw date time list

			#endregion RENAME THE COLUMN HEADING TEXT

			#region		fill lot index
			int nLotIndex = 1;
			foreach(string str in arrHasValueDateHeadingForLot)
			{
				string strFieldName = PREFIX_LOT_HEADING  + str.Substring(str.Length-2);
				// get 2 ending characters, is 01, 02, 03, 04, ...
				objRB.DrawPredefinedField(strFieldName,  "LOT No." + nLotIndex);	// write the lot index
				nLotIndex++;	// increase index for next lot
			}
			#endregion		fill lot index

			// 1. IN1T :: what to show, and spread
			string[] arrFieldToClear = {
										   PREFIX_QTYP_HEADING,
										   PREFIX_QTYS_HEADING,
										   PREFIX_LOT_HEADING,
										   PREFIX_DAYINMONTH,	
										   PREFIX_DAYINMONTHNEXT,	
										   FLD + PLAN,
										   FLD + PLAN + "S"
									   };		// contain name of field need to clear if day column is not exist
			string[] arrSubFieldToClear = {
											  PREFIX_LOT_HEADING,
											  PREFIX_DAYINMONTH,
											  PREFIX_DAYINMONTHNEXT
										  };
			
			#region SHOW the column of existed lot
		
			foreach(string str in arrFieldToClear)
			{
				foreach(int i in arrPlanDate)
				{					
					Field fldToClear = objRB.GetFieldByName(str+i.ToString("00"));
					fldToClear.ForeColor = Color.Black;
					fldToClear.Visible = true;
				}
			}
			#endregion SHOW the column of existed lot

			#region		SpreadColumnsWithinWidth (all lot-enabled columns and 3 last columns)		
			
			string LEFT_MARGIN_FLD = "lblLeftMarginToSpread";
			string RIGHT_MARGIN_FLD = "lblRightMarginToSpread";

			double dblStartLeftPoint = objRB.GetFieldByName(LEFT_MARGIN_FLD).Left ;
			double dblWidthToSpead = objRB.GetFieldByName(RIGHT_MARGIN_FLD).Left  + objRB.GetFieldByName(RIGHT_MARGIN_FLD).Width
				- objRB.GetFieldByName(LEFT_MARGIN_FLD).Left  ;

			double dblWidthOfEachColumn = dblWidthToSpead / (arrPlanDate.Count + 2);

			foreach(string str in arrSubFieldToClear)
			{
				int nColumnIndex = 0;
				foreach (int i in arrPlanDate)
				{
					Field fld = objRB.GetFieldByName(str+i.ToString("00"));
					fld.Width = dblWidthOfEachColumn;
					fld.Left = dblStartLeftPoint + nColumnIndex*dblWidthOfEachColumn;
					nColumnIndex ++;
				}
			}
			double dblWidthSubCol = dblWidthOfEachColumn / 2;
			double dblNextLeft = dblStartLeftPoint;
			for (int j = 0; j < arrPlanDate.Count; j++)
			{
				int i = Convert.ToInt32(arrPlanDate[j]);
				string strQtyPCol = "fldQtyP" + i.ToString("00");
				string strQtySCol = "fldQtyS" + i.ToString("00");
				string strPOCol = "fldPO" + i.ToString("00");
				string strPOSCol = "fldPOS" + i.ToString("00");
				Field fldQtyP = objRB.GetFieldByName(strQtyPCol);
				Field fldQtyS = objRB.GetFieldByName(strQtySCol);
				Field fldPOCol = objRB.GetFieldByName(strPOCol);
				Field fldPOSCol = objRB.GetFieldByName(strPOSCol);
				fldQtyP.Width = fldQtyS.Width = 
					fldPOCol.Width = fldPOSCol.Width = dblWidthSubCol;
				fldQtyP.Left = fldPOCol.Left = dblNextLeft;
				fldQtyS.Left = fldPOSCol.Left = fldQtyP.Left + fldQtyP.Width;
				dblNextLeft = dblNextLeft + dblWidthOfEachColumn;
			}
			
			// same width for all
			objRB.GetFieldByName("lblNextMonth").Width = dblWidthOfEachColumn;
			objRB.GetFieldByName("lblFirmOrder").Width = objRB.GetFieldByName("lblBlank").Width = dblWidthToSpead - dblWidthOfEachColumn * 2;
			objRB.GetFieldByName("lblNextMonth1").Width = objRB.GetFieldByName("lblNextMonth2").Width = dblWidthOfEachColumn / 2;
			objRB.GetFieldByName("lblNextNextMonth").Width = dblWidthOfEachColumn;
			objRB.GetFieldByName("lblNextNextMonth1").Width = objRB.GetFieldByName("lblNextNextMonth2").Width = dblWidthOfEachColumn / 2;
			objRB.GetFieldByName("fldNextMonth").Width = objRB.GetFieldByName("fldNextMonth1").Width = dblWidthOfEachColumn / 2;
			objRB.GetFieldByName("fldNextNextMonth").Width = objRB.GetFieldByName("fldNextNextMonth1").Width = dblWidthOfEachColumn / 2;
			objRB.GetFieldByName("lblNextMonthValue").Width = 
				objRB.GetFieldByName("lblNextMonthValue1").Width = 
				objRB.GetFieldByName("lblNextMonthValue2").Width = 
				dblWidthOfEachColumn;
			objRB.GetFieldByName("lblNextNextMonthValue").Width = 
				objRB.GetFieldByName("lblNextNextMonthValue1").Width = 
				objRB.GetFieldByName("lblNextNextMonthValue2").Width = 
				dblWidthOfEachColumn;

			objRB.GetFieldByName("lblNextMonth").Left = 
				objRB.GetFieldByName("fldNextMonth").Left = 
				objRB.GetFieldByName("lblNextMonthValue").Left =
				objRB.GetFieldByName("lblNextMonthValue1").Left =
				objRB.GetFieldByName("lblNextMonthValue2").Left =
				objRB.GetFieldByName("lblNextMonth1").Left = 
				dblStartLeftPoint + (arrPlanDate.Count) * dblWidthOfEachColumn;
			objRB.GetFieldByName("lblNextMonth2").Left = 
				objRB.GetFieldByName("fldNextMonth1").Left = 
				objRB.GetFieldByName("lblNextMonth1").Left + objRB.GetFieldByName("lblNextMonth1").Width;

			objRB.GetFieldByName("lblNextNextMonth").Left = 
				objRB.GetFieldByName("fldNextNextMonth").Left = 
				objRB.GetFieldByName("lblNextNextMonthValue").Left =
				objRB.GetFieldByName("lblNextNextMonthValue1").Left =
				objRB.GetFieldByName("lblNextNextMonthValue2").Left =
				objRB.GetFieldByName("lblNextNextMonth1").Left = 
				dblStartLeftPoint + (arrPlanDate.Count + 1) * dblWidthOfEachColumn;
			objRB.GetFieldByName("lblNextNextMonth2").Left = 
				objRB.GetFieldByName("fldNextNextMonth1").Left = 
				objRB.GetFieldByName("lblNextNextMonth1").Left + objRB.GetFieldByName("lblNextNextMonth1").Width;

			#endregion		SpreadColumnsWithinWidth (all lot-enabled columns and 3 last columns)			
			
			// mark red to negative number fields
			objRB.MarkRedToNegativeNumberField();

			#endregion MODIFY THE REPORT LAYOUT - do not modify data value on report from now on
						
			objRB.RefreshReport();

			/// force the copies number
			printPreview.FormTitle = objRB.GetFieldByName(REPORTFLD_TITLE).Text;			
			printPreview.Show();
			#endregion
			
			UseReportViewerRenderEngine = false;
			
			return dtbTransform;
		}
		
		/// <summary>
		/// Thachnn : 15/Oct/2005
		/// Browse the DataTable, get all value of column with provided named.
		/// </summary>
		/// <param name="pdtb">DataTable to collect values</param>
		/// <param name="pstrColumnName">COlumn Name in pdtb DataTable to collect values from</param>
		/// <returns>ArrayList of object, collect from pdtb's column named pstrColumnName. Empty ArrayList if error or not found any row in pdtb.</returns>
		private static ArrayList GetColumnValuesFromTable(DataTable pdtb, string pstrColumnName)
		{
			ArrayList arrRet = new ArrayList();
			try
			{
				foreach (DataRow drow in pdtb.Rows)
				{
					object objGet = drow[pstrColumnName];
					if(!arrRet.Contains(objGet))
						arrRet.Add(objGet);
				}
			}
			catch
			{
				arrRet.Clear();
			}
			return arrRet;
		}

		private static ArrayList GetProductIDValuesFromTable(DataTable pdtb, DataRow[] pdrowPartyItemList, string pstrColumnName)
		{
			ArrayList arrRet = new ArrayList();
			try
			{
				foreach (DataRow drow in pdtb.Rows)
				{
					object objGet = drow[pstrColumnName];
					if(!arrRet.Contains(objGet))
						arrRet.Add(objGet);
				}
				foreach (DataRow drow in pdrowPartyItemList)
				{
					object objGet = drow[pstrColumnName];
					if(!arrRet.Contains(objGet))
						arrRet.Add(objGet);
				}
			}
			catch
			{
				arrRet.Clear();
			}
			return arrRet;
		}

		/// <summary>
		/// build a new datatable with column = productid, category,partno,model,begin,
		/// and somecolumn with names in arrHasValueDateHeading
		/// Index column is : Plan, Adj, Actual, Return, ProgressDay, Progress Accumulate, Assessment
		/// 
		/// </summary>
		/// <remarks>		
		/// </remarks>
		/// <returns>DataTable</returns>
		private DataTable BuildTransformTable(ArrayList parrHasValueDateHeading)
		{
			DataTable dtbRet = new DataTable(TABLE_NAME);
			dtbRet.Columns.Add(PRODUCTID,typeof(Int32) );
			dtbRet.Columns.Add(ITEMNO,typeof(String) );
			dtbRet.Columns.Add(PARTNO,typeof(String));
			dtbRet.Columns.Add(PARTNAME,typeof(String));
			dtbRet.Columns.Add(QTYSET,typeof(Int32));
			dtbRet.Columns.Add(UNITPRICE,typeof(String));			

			dtbRet.Columns.Add(NEXTMONTH,typeof(Double));
			dtbRet.Columns.Add(NEXTMONTH + "P",typeof(Double));
			dtbRet.Columns.Add(NEXTNEXTMONTH,typeof(Double));
			dtbRet.Columns.Add(NEXTNEXTMONTH + "P",typeof(Double));

			/// fill the column (Double type) in which the date exist in the dtbSourceData (has value contain in the parrDueDateHeading)
			/// then fill the column with String type (so that it will display correctly in the report, not #,##0.00, because it has null value)
					
			foreach(string strColumnName in parrHasValueDateHeading)
			{					
				try
				{
					dtbRet.Columns.Add(strColumnName,typeof(Double));
				}
				catch{}
			}
			// FILL the null column				
			for(int i = 1; i <=31; i++)												  
			{
				if(parrHasValueDateHeading.Contains(PLAN + i.ToString("00")) == false )
				{		
					try
					{
						dtbRet.Columns.Add(PLAN + i.ToString("00"),typeof(String));
						dtbRet.Columns.Add(PLAN + "S" + i.ToString("00"),typeof(String));
					}
					catch{}
				}			

			} 	// FILL the null column
			
			return dtbRet;		
		}	// end build transform tables

		/// <summary>
		/// Get all data for this report and cache in the dstMAIN dataset
		/// just improve the speed for this report
		/// </summary>
		/// <param name="pstrCCNID"></param>
		/// <param name="pstrYear"></param>
		/// <param name="pstrMonth"></param>
		/// <param name="pstrPurchaseOrderID"></param>
		private DataSet GetDataAndCache(string pstrCCNID, string pstrYear, string pstrMonth, string pstrVendorID, string pstrPurchaseOrderID)
		{	
			DataSet dstRET = new DataSet();
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;

			DateTime dtmCurrent = new DateTime(int.Parse(pstrYear)   , int.Parse(pstrMonth), 1);
			string pstrNextYear = dtmCurrent.AddMonths(1).Year.ToString();
			string pstrNextMonth = dtmCurrent.AddMonths(1).Month.ToString("00");
			string pstrNextNextYear = dtmCurrent.AddMonths(2).Year.ToString();
			string pstrNextNextMonth = dtmCurrent.AddMonths(2).Month.ToString("00");
			int intVendorID = 0;
			try
			{
				intVendorID = Convert.ToInt32(pstrVendorID);
			}
			catch{}

			#region MAIN SQL QUERY
				
			string strSql = 	
		
 " Declare @pstrYear char(4) " + 
 " Declare @pstrMonth char(2) " + 
 " Declare @pstrNextYear char(4) " + 
 " Declare @pstrNextMonth char(2) " + 
 " Declare @pstrNextNextYear char(4) " + 
 " Declare @pstrNextNextMonth char(2) " + 
 "  " + 
 " Declare @pstrCCNID int " + 
 " Declare @pstrPurchaseOrderMasterID int ";
			if (intVendorID > 0)
				strSql += " Declare @pstrVendorID int " + " Set @pstrVendorID = " +pstrVendorID+ " ";
 strSql += " /*-----------------------------------*/ " + 
 " Set @pstrYear = '" +pstrYear+ "' " + 
 " Set @pstrMonth = '" +pstrMonth+ "' " + 
 " Set @pstrNextYear = '" +pstrNextYear+ "' " + 
 " Set @pstrNextMonth = '" +pstrNextMonth+ "' " + 
 " Set @pstrNextNextYear = '" +pstrNextNextYear+ "' " + 
 " Set @pstrNextNextMonth = '" +pstrNextNextMonth+ "' " + 
 "  " + 
 " Set @pstrCCNID = " +pstrCCNID+ " " + 
 " Set @pstrPurchaseOrderMasterID = " +pstrPurchaseOrderID+ " " + 
 " /*-----------------------------------*/ " + 
 				"  " + 					
				"  " ;

			#endregion MAIN QUERY

			
			#region META _DATA and PODATA

			string strSql_META_TABLE =
 " SELECT    " + 
 " PRODUCT.ProductID, " + 
 " PRODUCT.RegisteredCode as [ItemNo],  " + 
 " PRODUCT.Code AS [PartNo],   " + 
 " PRODUCT.Description AS [PartName],  " + 
 " PRODUCT.QuantitySet as [QtySet], " + 
 " IsNull(PODETAIL.UnitPrice, 0.0000) as [UnitPrice],  " + 
 " DATEPART(dd, PO_DeliverySchedule.ScheduleDate) as [PODay], " + 
 " SUM(PO_DeliverySchedule.DeliveryQuantity) AS [POQuantity] , " + 
 " 0.00 as [NextMonth], " + 
 " 0.00 as [NextNextMonth] " + 
 "  " + 
 " FROM        " + 
 " PO_DeliverySchedule INNER JOIN PO_PurchaseOrderDetail as PODETAIL " + 
 " 	 on PO_DeliverySchedule.PurchaseOrderDetailID = PODETAIL.PurchaseOrderDetailID " + 
 " INNER JOIN ITM_Product as PRODUCT " + 
 " 	on PODETAIL.ProductID = PRODUCT.ProductID " + 
 " INNER JOIN PO_PurchaseOrderMaster as POMASTER " + 
 " 	on POMASTER.PurchaseOrderMasterID = PODETAIL.PurchaseOrderMasterID " + 
 "  " + 
 " WHERE     " + 
 " POMASTER.CCNID = @pstrCCNID   " + 
 " AND DATEPART(yyyy, PO_DeliverySchedule.ScheduleDate) = @pstrYear  " + 
 " AND DATEPART(mm, PO_DeliverySchedule.ScheduleDate) = @pstrMonth " + 
 " AND POMASTER.PurchaseOrderMasterID = @pstrPurchaseOrderMasterID " + 
 " AND POMASTER.PartyID in /* Import */ ( " + 
 " 	select PartyID from MST_Party " + 
 " 	where CountryID <> (select Top 1 CountryID from MST_CCN where CCNID = @pstrCCNID) " + 
 "  	) ";
			if (intVendorID > 0)
				strSql_META_TABLE += " AND POMASTER.MakerID = " + intVendorID;
			strSql_META_TABLE += " GROUP BY    " + 
" PRODUCT.ProductID, " + 
 " PRODUCT.RegisteredCode,   " + 
 " PRODUCT.Code,   " + 
 " PRODUCT.Description,  " + 
 " PRODUCT.QuantitySet, " + 
 " PODETAIL.UnitPrice, " + 
 " DATEPART(dd, PO_DeliverySchedule.ScheduleDate) " + 
 "  " + 

				"  " ; 
			#endregion META _DATA


			#region NEXT MONTH
		
			string strSqlNEXTTABLE =	
 " 				/****************************** NEXT MONTH **************************************/ " + 
 " SELECT    " + 
 " PRODUCT.ProductID, " + 
 " SUM(PO_DeliverySchedule.DeliveryQuantity) AS [NextMonth] " + 
 "  " + 
 " FROM        " + 
 " PO_DeliverySchedule INNER JOIN PO_PurchaseOrderDetail as PODETAIL " + 
 " 	 on PO_DeliverySchedule.PurchaseOrderDetailID = PODETAIL.PurchaseOrderDetailID " + 
 " INNER JOIN ITM_Product as PRODUCT " + 
 " 	on PODETAIL.ProductID = PRODUCT.ProductID " + 
 " INNER JOIN PO_PurchaseOrderMaster as POMASTER " + 
 " 	on POMASTER.PurchaseOrderMasterID = PODETAIL.PurchaseOrderMasterID " + 
 "  " + 
 " WHERE     " + 
 " POMASTER.CCNID = @pstrCCNID   " + 
 " AND DATEPART(yyyy, PO_DeliverySchedule.ScheduleDate) = @pstrNextYear  " + 
 " AND DATEPART(mm, PO_DeliverySchedule.ScheduleDate) = @pstrNextMonth " + 
 //" AND POMASTER.PurchaseOrderMasterID = @pstrPurchaseOrderMasterID " + 
 " AND POMASTER.PartyID in /* Import */ ( " + 
 " 	select PartyID from MST_Party " + 
 " 	where CountryID <> (select Top 1 CountryID from MST_CCN where CCNID = @pstrCCNID) " + 
 "  	) ";
			if (intVendorID > 0)
				strSqlNEXTTABLE += " AND POMASTER.MakerID = " + intVendorID;
			strSqlNEXTTABLE += " GROUP BY    " + 
 " PRODUCT.ProductID " + 
 " /****************************** NEXT MONTH **************************************/ " + 
                ""	
				;

			#endregion NEXT MONTH
			/* ============================================================== */
				
			#region NEXT NEXT MONTH
		
			string strSqlNEXTNEXTTABLE =	
 " 				/****************************** NEXT NEXT MONTH **************************************/ " + 
 " SELECT    " + 
 " PRODUCT.ProductID, " + 
 " SUM(PO_DeliverySchedule.DeliveryQuantity) AS [NextNextMonth] " + 
 "  " + 
 " FROM        " + 
 " PO_DeliverySchedule INNER JOIN PO_PurchaseOrderDetail as PODETAIL " + 
 " 	 on PO_DeliverySchedule.PurchaseOrderDetailID = PODETAIL.PurchaseOrderDetailID " + 
 " INNER JOIN ITM_Product as PRODUCT " + 
 " 	on PODETAIL.ProductID = PRODUCT.ProductID " + 
 " INNER JOIN PO_PurchaseOrderMaster as POMASTER " + 
 " 	on POMASTER.PurchaseOrderMasterID = PODETAIL.PurchaseOrderMasterID " + 
 "  " + 
 " WHERE     " + 
 " POMASTER.CCNID = @pstrCCNID   " + 
 " AND DATEPART(yyyy, PO_DeliverySchedule.ScheduleDate) = @pstrNextNextYear  " + 
 " AND DATEPART(mm, PO_DeliverySchedule.ScheduleDate) = @pstrNextNextMonth " + 
// " AND POMASTER.PurchaseOrderMasterID = @pstrPurchaseOrderMasterID " + 
 " AND POMASTER.PartyID in /* Import */ ( " + 
 " 	select PartyID from MST_Party " + 
 " 	where CountryID <> (select Top 1 CountryID from MST_CCN where CCNID = @pstrCCNID) " + 
 "  	) ";
			if (intVendorID > 0)
				strSqlNEXTNEXTTABLE += " AND POMASTER.MakerID = " + intVendorID;
			strSqlNEXTNEXTTABLE += " GROUP BY    " + 
 " PRODUCT.ProductID " + 
 " /****************************** NEXT NEXT MONTH **************************************/ " + 
 "  " + 
				""	
				;

			#endregion NEXT NEXT MONTH
			/* ============================================================== */
	
			#region MISC INFO
		
			string strSqlMISCINFOTABLE =	
				/****************************** MISC INFO, get something from POMaster *************************/
 
 " 				select  " + 
 " top 1 " + 
 " VENDORPARTY.Code as VendorCode, " + 
 " PRICING.Description as PricingType, " + 
 " MASTER.RequestDeliveryTime as RequestDeliveryTime,  " + 
 " MASTER.OrderDate as TransDate, " + 
 " MST_Currency.Code, " + 
 " IsNull(VENDORPARTY.Name, ' ') as VendorName, " + 
 " IsNull(VENDORCONTACT.Name, ' ') as VendorContactName, " + 
 " IsNull(MAKERPARTY.Name, ' ') as MakerName, " + 
 " IsNull(MAKERCONTACT.Name, ' ') as MakerContactName, " + 
 " MAKERCONTACT.Fax, MASTER.MakerID AS PartyID, " + 
 " MASTER.Code AS PurchaseOrderNo " + 
 " from PO_PurchaseOrderMaster as MASTER " + 
 " join MST_Party as VENDORPARTY " + 
 " on MASTER.PartyID = VENDORPARTY.PartyID " + 
 " and MASTER.PurchaseOrderMasterID = @pstrPurchaseOrderMasterID ";
//			if (intVendorID > 0)
//				strSqlMISCINFOTABLE += " AND MASTER.MakerID = " + intVendorID;
			strSqlMISCINFOTABLE += 
 " left join ENM_PricingType PRICING " + 
 " on MASTER.PricingTypeID = PRICING.PricingTypeID " + 
 " left join MST_Currency " + 
 " on MASTER.CurrencyID = MST_Currency.CurrencyID " + 
 " left join (select PartyContactID, Name, Fax from MST_PartyContact) as VENDORCONTACT " + 
 " on MASTER.PartyContactID = VENDORCONTACT.PartyContactID " + 
 " left join MST_Party as MAKERPARTY " + 
 " on MASTER.MakerID = MAKERPARTY.PartyID " + 
 " left join (select PartyContactID, PartyID, Name, Fax from MST_PartyContact ) as MAKERCONTACT " + 
 " on MAKERPARTY.PartyID = MAKERCONTACT.PartyID " + 
 "  " + 
 "  " ;
			/* ============================================================== */
			#endregion MISC INFO

			try 
			{
				
				oconPCS = null;
				ocmdPCS = null;
			
				strSql += 
					strSql_META_TABLE + "\n" +  					
					strSqlNEXTTABLE + "\n" + 
					strSqlNEXTNEXTTABLE + "\n" +
					strSqlMISCINFOTABLE + "\n" 
					;	

				Debug.WriteLine(strSql_META_TABLE);
				oconPCS = new OleDbConnection(mConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				
				ocmdPCS.Connection.Open();				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstRET);

				dstRET.Tables[0].TableName = PO_TABLE_NAME;
				dstRET.Tables[1].TableName = NEXTMONTH_TABLE_NAME;
				dstRET.Tables[2].TableName = NEXTNEXTMONTH_TABLE_NAME;
				dstRET.Tables[3].TableName = MISC_INFO_TABLE_NAME;
			}
			catch(OleDbException ex)
			{
				throw new Exception(strSql,ex);
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
			
			return dstRET;
		}

		private DataTable GetPartyItemList(string pstrPartyList)
		{
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{			
				string strSql = "SELECT ProductID, PrimaryVendorID AS PartyID, RegisteredCode AS ItemNo, QuantitySet AS QtySet,"
					+ " ITM_Product.Code [PartNo], ITM_Product.Description [PartName], ITM_Product.Revision [Model],"
					+ " MST_Party.Code + '( ' + MST_Party.Name + ')' as [Vendor], ISNULL(MassOrder,0) MassOrder"
					+ " FROM ITM_Product JOIN MST_Party ON ITM_Product.PrimaryVendorID = MST_Party.PartyID"
					+ " WHERE PrimaryVendorID IS NOT NULL"
					+ " AND ISNULL(MassOrder,0) = 1";
				if (pstrPartyList != null && pstrPartyList.Trim() != string.Empty)
					strSql += " AND PrimaryVendorID IN (" + pstrPartyList + ")";
				strSql += " ORDER BY MST_Party.Code";
				oconPCS = new OleDbConnection(mConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				
				ocmdPCS.Connection.Open();
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				DataTable dtbData = new DataTable();
				odadPCS.Fill(dtbData);
				return dtbData;
			}
			catch(Exception ex)
			{
				throw ex;
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
		private bool IsMassOrder(DataTable dtbItemList, string pstrItem)
		{
			try
			{
				return Convert.ToBoolean(dtbItemList.Select("ProductID = " + pstrItem)[0]["MassOrder"]);
			}
			catch
			{
				return false;
			}
		}
	}

}
