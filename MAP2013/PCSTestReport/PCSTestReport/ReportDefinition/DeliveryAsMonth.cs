using System;
using System.Collections;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Drawing.Printing;
using System.Reflection;
using System.Text;
using PCSComUtils.Common;
using PCSUtils.Utils;
using C1.Win.C1Preview;
using C1.C1Report;
using PCSUtils.Framework.ReportFrame;
using C1PrintPreviewDialog = PCSUtils.Framework.ReportFrame.C1PrintPreviewDialog;

namespace DeliveryAsMonth
{
	public class DeliveryAsMonth : MarshalByRefObject, IDynamicReport
	{
		#region IDynamicReport Members

		private string mConnectionString;

		/// <summary>
		/// ConnectionString, provide for the Dynamic Report
		/// ALlow Dynamic Report to access the DataBase of PCS
		/// </summary>
		public string PCSConnectionString
		{
			get { return mConnectionString; }
			set { mConnectionString = value; }
		}

		private ReportBuilder mReportBuilder;

		/// <summary>
		/// Report Builder Utility Object
		/// Dynamic Report can use this object to render, modify, layout the report
		/// </summary>
		public ReportBuilder PCSReportBuilder
		{
			get { return mReportBuilder; }
			set { mReportBuilder = value; }
		}

		private C1PrintPreviewControl mViewer;

		/// <summary>
		/// ReportViewer Object, provide for the DynamicReport, 
		/// allow Dynamic Report to manipulate with the REportViewer, 
		/// modify the report after rendered if needed
		/// </summary>
		public C1PrintPreviewControl PCSReportViewer
		{
			get { return mViewer; }
			set { mViewer = value; }
		}

		private object mResult;

		/// <summary>
		/// Store other result if any. Ussually we store return DataTable here to display on the ReportViewer Form's Grid
		/// </summary>
		public object Result
		{
			get { return mResult; }
			set { mResult = value; }
		}

		private bool mUseEngine;

		/// <summary>
		/// Notify PCS whether the rendering report process is run by 
		/// this IDynamicReport
		/// or the ReportViewer Engine (in the ReportViewer form)
		/// </summary>
		public bool UseReportViewerRenderEngine
		{
			get { return mUseEngine; }
			set { mUseEngine = value; }
		}

		private string mReportFolder;

		/// <summary>
		/// Inform External Process where to find out the ReportLayout	 ( the PCS' ReportDefinition Folder Path )
		/// </summary>		
		public string ReportDefinitionFolder
		{
			get { return mReportFolder; }
			set { mReportFolder = value; }
		}

		private string mLayoutFile;

		/// <summary>
		/// Inform External Process about the Layout file
		/// in which PCS instruct to use
		/// (PCS will assign this property while ReportViewer Form execute,
		/// ReportVIewer form will use the layout file in the report config entry to put in this property)
		/// </summary>		
		public string ReportLayoutFile
		{
			get { return mLayoutFile; }
			set { mLayoutFile = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="pstrMethod">name of the method to call (which declare in the DynamicReport C# file)</param>
		/// <param name="pobjParameters">Array of parameters provide to call the Method with method name = pstrMethod</param>
		/// <returns></returns>
		public object Invoke(string pstrMethod, object[] pobjParameters)
		{
			return this.GetType().InvokeMember(pstrMethod, BindingFlags.InvokeMethod, null, this, pobjParameters);
		}

		#endregion

		private const string DELIVERY_TIMES = "DeliveryTimes";
		private const string WRONG_TIMES = "WrongTimes";

		public DataTable ExecuteReport(string pstrCCNID, string pstrYear, string pstrMapTarget, string pstrTolerance)
		{
			// delivery times of all makers in a year
			DataTable dtbDeliveryTimes = GetDeliveryTimesOfAllMakers(pstrCCNID, pstrYear);
			// wrong times of all makers
			DataTable dtbWrongTimes = GetWrongTimesOfAllMakers(pstrCCNID, pstrTolerance, pstrYear);
			
			#region report table

			DataTable dtbData = new DataTable();
			dtbData.Columns.Add(new DataColumn("ScheduleMonth", typeof (string)));
			dtbData.Columns.Add(new DataColumn(DELIVERY_TIMES, typeof (int)));
			dtbData.Columns.Add(new DataColumn(WRONG_TIMES, typeof (int)));
			dtbData.Columns.Add(new DataColumn("MapTarget", typeof (decimal)));
			dtbData.Columns["MapTarget"].DefaultValue = Convert.ToDecimal(pstrMapTarget);
			dtbData.Columns.Add(new DataColumn("PPM", typeof (decimal)));
			dtbData.Columns["PPM"].DefaultValue = decimal.Zero;

			#endregion
			
			// each maker will be one row in table
			for (int i = 1; i <= 12; i ++)
			{
				DateTime dtmDate = new DateTime(Convert.ToInt32(pstrYear), i, 1);
				DataRow drowReportData = dtbData.NewRow();
				
				string strFilter = "ScheduleMonth = '" + i.ToString() + "'";
				
				// delivery times
				DataRow[] drowDeliveryTimes = dtbDeliveryTimes.Select(strFilter);
				drowReportData[DELIVERY_TIMES] = drowDeliveryTimes.Length;
				
				// wrong times
				//DataRow[] drowWrongTimes = dtbWrongTimes.Select(strFilter);
				//drowReportData[WRONG_TIMES] = drowWrongTimes.Length;
				int intWrongTime = 0;
				foreach (DataRow drowDelivery in drowDeliveryTimes)
				{
					string strWrongTimeFilter = strFilter + " AND DeliveryScheduleID = " + drowDelivery["DeliveryScheduleID"];
					DataRow[] arrWrongTimes = dtbWrongTimes.Select(strWrongTimeFilter);
					intWrongTime += (arrWrongTimes.Length > 0) ? 1 : 0;
				}
				drowReportData[WRONG_TIMES] = intWrongTime;
				
				// ppm = wrong / delivery * 1000
				try
				{
					double dPPM = (Convert.ToDouble(intWrongTime)/Convert.ToDouble(drowDeliveryTimes.Length)) * 1000000;
					drowReportData["PPM"] = dPPM;
				}
				catch{}
				// Month
				drowReportData["ScheduleMonth"] = dtmDate.ToString("MMMM");
				// insert to datasource
				dtbData.Rows.Add(drowReportData);
			}
			
			#region report

			C1Report rptReport = new C1Report();
			
			mLayoutFile = "DeliveryAsMonth.xml";
			rptReport.Load(mReportFolder + "\\" + mLayoutFile, rptReport.GetReportInfo(mReportFolder + "\\" + mLayoutFile)[0]);
			rptReport.Layout.PaperSize = PaperKind.A4;

			#region report parameter
				
			try
			{
				rptReport.Fields["fldCCN"].Text = GetCCN(pstrCCNID);
			}
			catch{}
			try
			{
				rptReport.Fields["fldMonth"].Text = pstrYear.ToString();
			}
			catch{}
			try
			{
				rptReport.Fields["fldMapTarget"].Text = pstrMapTarget;
			}
			catch{}
			try
			{
				rptReport.Fields["fldTolerance"].Text = pstrTolerance;
			}
			catch{}
				
			#endregion
				
			// set datasource object that provides data to report.
			rptReport.DataSource.Recordset = dtbData;
			// render report
			rptReport.Render();

			// render the report into the PrintPreviewControl
			C1PrintPreviewDialog ppvViewer = new C1PrintPreviewDialog();
			ppvViewer.FormTitle = "DELIVERY EVALUATION AS MONTH";
			
			ppvViewer.ReportViewer.Document = rptReport.Document;
			ppvViewer.Show();
			
			#endregion

			return dtbData;
		}

		private DataTable GetDeliveryTimesOfAllMakers(string pstrCCNID, string pstrYear)
		{
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = "SELECT DISTINCT CAST((CAST(DATEPART(year,ScheduleDate) AS varchar) + '-'"
					+ " + CAST(DATEPART(month,ScheduleDate) AS varchar) + '-'"
					+ " + CAST(DATEPART(day,ScheduleDate) AS varchar)) AS datetime) AS ScheduleDate,"
					+ " DATEPART(month,ScheduleDate) AS ScheduleMonth, MakerID, "
					+ " PO_DeliverySchedule.DeliveryScheduleID "
					+ " FROM PO_DeliverySchedule JOIN PO_PurchaseOrderDetail"
					+ " ON PO_DeliverySchedule.PurchaseOrderDetailID = PO_PurchaseOrderDetail.PurchaseOrderDetailID"
					+ " JOIN PO_PurchaseOrderMaster ON PO_PurchaseOrderDetail.PurchaseOrderMasterID = PO_PurchaseOrderMaster.PurchaseOrderMasterID"
					+ " JOIN MST_Party ON PO_PurchaseOrderMaster.MakerID = MST_Party.PartyID"
					+ " WHERE PO_PurchaseOrderMaster.CCNID = " + pstrCCNID
					+ " AND DATEPART(year, ScheduleDate) = " + pstrYear
					+ " AND PO_PurchaseOrderDetail.ApproverID > 0"
					+ " AND MST_Party.CountryID <> (SELECT CountryID FROM MST_CCN WHERE CCNID = " + pstrCCNID + ")"
					+ " AND MST_Party.Type <> 0"
					+ " ORDER BY DATEPART(month,ScheduleDate),"
					+ " CAST((CAST(DATEPART(year,ScheduleDate) AS varchar) + '-'"
				    + " + CAST(DATEPART(month,ScheduleDate) AS varchar) + '-'"
				    + " + CAST(DATEPART(day,ScheduleDate) AS varchar)) AS datetime)";
				oconPCS = new OleDbConnection(mConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				DataTable dtbData = new DataTable();
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dtbData);
				
				return dtbData;
			}
			catch (Exception ex)
			{
				throw ex;
			}
			finally
			{
				if (oconPCS != null)
					if (oconPCS.State != ConnectionState.Closed)
						oconPCS.Close();
			}
		}
		private DataTable GetWrongTimesOfAllMakers(string pstrCCNID, string pstrTolerance, string pstrYear)
		{
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = "SELECT DISTINCT PO_InvoiceDetail.ProductID, MakerID,"
					+ " DATEPART(month,ScheduleDate) AS ScheduleMonth,"
					+ " ABS(DATEDIFF(day, ScheduleDate, PO_PurchaseOrderReceiptMaster.PostDate)) AS Tolerance, "
					+ " PO_DeliverySchedule.DeliveryScheduleID "
					+ " FROM PO_InvoiceDetail JOIN PO_PurchaseOrderReceiptDetail"
					+ " ON PO_InvoiceDetail.DeliveryScheduleID = PO_PurchaseOrderReceiptDetail.DeliveryScheduleID"
					+ " JOIN PO_PurchaseOrderReceiptMaster"
					+ " ON PO_PurchaseOrderReceiptDetail.PurchaseOrderReceiptID = PO_PurchaseOrderReceiptMaster.PurchaseOrderReceiptID"
					+ " JOIN PO_DeliverySchedule"
					+ " ON PO_InvoiceDetail.DeliveryScheduleID = PO_DeliverySchedule.DeliveryScheduleID"
					+ " JOIN PO_PurchaseOrderMaster"
					+ " ON PO_InvoiceDetail.PurchaseOrderMasterID = PO_PurchaseOrderMaster.PurchaseOrderMasterID"
					+ " JOIN MST_Party"
					+ " ON PO_PurchaseOrderMaster.MakerID = MST_Party.PartyID"
					+ " WHERE (ABS(DATEDIFF(day, ScheduleDate, PostDate)) > "+pstrTolerance+" OR InvoiceQuantity <> DeliveryQuantity)"
					+ " AND PO_PurchaseOrderMaster.CCNID = " + pstrCCNID
					+ " AND MST_Party.CountryID <> (SELECT CountryID FROM MST_CCN WHERE CCNID = " + pstrCCNID + ")"
					+ " AND MST_Party.Type <> 0"
					+ " AND DATEPART(year, ScheduleDate) = " + pstrYear
					+ " ORDER BY PO_InvoiceDetail.ProductID";
				oconPCS = new OleDbConnection(mConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				DataTable dtbData = new DataTable();
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dtbData);
				
				return dtbData;
			}
			catch (Exception ex)
			{
				throw ex;
			}
			finally
			{
				if (oconPCS != null)
					if (oconPCS.State != ConnectionState.Closed)
						oconPCS.Close();
			}
		}

		private DataTable ListMaker(string pstrCCNID)
		{
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = "SELECT PartyID AS MakerID, MST_Party.Code, MST_Party.Name"
					+ " FROM MST_Party"
					+ " WHERE MST_Party.CountryID <> (SELECT CountryID FROM MST_CCN WHERE CCNID = " + pstrCCNID + ")"
					+ " AND Type <> 0"; // vendor and both only
				oconPCS = new OleDbConnection(mConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				DataTable dtbData = new DataTable();
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dtbData);
				
				return dtbData;
			}
			catch (Exception ex)
			{
				throw ex;
			}
			finally
			{
				if (oconPCS != null)
					if (oconPCS.State != ConnectionState.Closed)
						oconPCS.Close();
			}
		}

		private string GetCCN(string pstrCCNID)
		{
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = "SELECT Code + ' (' + Description + ')' FROM MST_CCN WHERE CCNID = " + pstrCCNID;
				oconPCS = new OleDbConnection(mConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				object objResult = ocmdPCS.ExecuteScalar();
				return objResult.ToString();
			}
			catch (Exception ex)
			{
				throw ex;
			}
			finally
			{
				if (oconPCS != null)
					if (oconPCS.State != ConnectionState.Closed)
						oconPCS.Close();
			}
		}

	}
}