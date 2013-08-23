using System;
using System.Data;
using System.Data.OleDb;
using System.Drawing.Printing;
using System.Reflection;
using C1.Win.C1Preview;
using C1.C1Report;
using PCSUtils.Framework.ReportFrame;
using PCSUtils.Utils;
using C1PrintPreviewDialog = PCSUtils.Framework.ReportFrame.C1PrintPreviewDialog;

namespace SuperviseReport
{
	public class SuperviseReport : MarshalByRefObject, IDynamicReport
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

		const string DIFPOS_COL = "DifPos";
		const string DIFNEV_COL = "DifNev";
		const string METHOD_COL = "Method";
		const string OH_COL = "OHQuantity";
		const string ACTUAL_COL = "Actual";
		const string SLIPCODE_COL = "SlipCode";
		const string STOCKTAKINGDATE_FLD = "fldStockTakingDate";
		public DataTable ExecuteReport(string pstrPeriodID, string pstrMasterLocationID, string pstrLocationID, string pstrBinID)
		{
			#region report table

			DataTable dtbData = GetReportData(pstrPeriodID, pstrLocationID, pstrBinID);
			
			#endregion
			
			#region build report data
			
			DataTable dtbStockTaking = GetStockTakingData(pstrPeriodID, pstrLocationID, pstrBinID);
			dtbData.Columns.Add(new DataColumn(DIFPOS_COL, typeof(decimal)));
			dtbData.Columns.Add(new DataColumn(DIFNEV_COL, typeof(decimal)));
			dtbData.Columns.Add(new DataColumn(METHOD_COL, typeof(string)));
			dtbData.Columns.Add(new DataColumn(SLIPCODE_COL, typeof(string)));
			int intCountPos = 0, intCountNev = 0;
			decimal decQtyCheck = 0, intNumPos = 0, intNumNev = 0;
			// calculate different and fill counting method
			foreach (DataRow drowData in dtbData.Rows)
			{
				string strLocationID = drowData["LocationID"].ToString();
				string strBinID = drowData["BinID"].ToString();
				string strProductID = drowData["ProductID"].ToString();
				string strSlipCode;
				string strMethod = GetCountingMethod(strLocationID, strBinID, strProductID, dtbStockTaking, out strSlipCode);
				drowData[METHOD_COL] = strMethod;
				drowData[SLIPCODE_COL] = strSlipCode;
				decimal decOHQuantity = 0, decActual = 0;
				try
				{
					decOHQuantity = Convert.ToDecimal(drowData[OH_COL]);
				}
				catch{}
				try
				{
					decActual = Convert.ToDecimal(drowData[ACTUAL_COL]);
				}
				catch{}
				if (decActual - decOHQuantity > 0)
				{
					drowData[DIFPOS_COL] = decActual - decOHQuantity;
					intCountPos++;
					intNumPos += decActual - decOHQuantity;
				}
				else if (decActual - decOHQuantity < 0)
				{
					drowData[DIFNEV_COL] = decActual - decOHQuantity;
					intCountNev++;
					intNumNev += decActual - decOHQuantity;
				}
				decQtyCheck += decOHQuantity;
			}
			
			#endregion
			
			#region report

			C1Report rptReport = new C1Report();
			
			mLayoutFile = "SuperviseReport.xml";
			rptReport.Load(mReportFolder + "\\" + mLayoutFile, rptReport.GetReportInfo(mReportFolder + "\\" + mLayoutFile)[0]);
			rptReport.Layout.PaperSize = PaperKind.A3;

			#region Report constant
			const string PARAM_PERIOD = "fldParamPeriod";
			const string PARAM_MASLOC = "fldParamMasLoc";
			const string PARAM_LOC = "fldParamLocation";
			const string PARAM_BIN = "fldParamBin";
			const string COUNTPOS_FLD = "fldA";
			const string COUNTNEV_FLD = "fldB";
			const string NUMPOS_FLD = "fldC";
			const string NUMNEV_FLD = "fldD";
			const string NUMCHECK_FLD = "fldNumCheck";
			const string QTYCHECK_FLD = "fldQtyCheck";
			const string RATECOUNT_FLD = "fldRateCount";
			const string RATEQTY_FLD = "fldRateQuantity";
			#endregion

			#region report parameter
			
			DataRow drowPeriodInfo = GetPeriod(pstrPeriodID);
			try
			{
				rptReport.Fields[PARAM_PERIOD].Text = drowPeriodInfo["Description"].ToString();
			}
			catch{}
			try
			{
				rptReport.Fields[STOCKTAKINGDATE_FLD].Text = Convert.ToDateTime(drowPeriodInfo["FromDate"]).ToString("dd-MM-yyyy");
			}
			catch{}
			try
			{
				rptReport.Fields[PARAM_MASLOC].Text = GetMasLoc(pstrMasterLocationID);
			}
			catch{}
			try
			{
				rptReport.Fields[PARAM_LOC].Text = GetLocation(pstrLocationID);
			}
			catch{}
			try
			{
				rptReport.Fields[PARAM_BIN].Text = GetBin(pstrBinID);
			}
			catch{}
			try
			{
				rptReport.Fields[COUNTPOS_FLD].Text = intCountPos.ToString();
			}
			catch{}
			try
			{
				rptReport.Fields[COUNTNEV_FLD].Text = intCountNev.ToString();
			}
			catch{}
			try
			{
				rptReport.Fields[NUMPOS_FLD].Text = intNumPos.ToString();
			}
			catch{}
			try
			{
				rptReport.Fields[NUMNEV_FLD].Text = intNumNev.ToString();
			}
			catch{}
			try
			{
				rptReport.Fields[NUMCHECK_FLD].Text = dtbData.Rows.Count.ToString();
			}
			catch{}
			try
			{
				rptReport.Fields[QTYCHECK_FLD].Text = decQtyCheck.ToString();
			}
			catch{}
			try
			{
				// rate count = num dif/num check
				if (dtbData.Rows.Count > 0)
					rptReport.Fields[RATECOUNT_FLD].Text = "fldNumDif / fldNumCheck";
			}
			catch{}
			try
			{
				if (decQtyCheck != 0)
					rptReport.Fields[RATEQTY_FLD].Text = "fldQtyDif / fldQtyCheck";
			}
			catch{}
				
			#endregion
				
			// set datasource object that provides data to report.
			rptReport.DataSource.Recordset = dtbData;
			// render report
			rptReport.Render();

			// render the report into the PrintPreviewControl
			C1PrintPreviewDialog ppvViewer = new C1PrintPreviewDialog();
			ppvViewer.FormTitle = "Supervise Report";
			
			ppvViewer.ReportViewer.Document = rptReport.Document;
			ppvViewer.Show();
			
			#endregion

			return dtbData;
		}

		private DataTable GetReportData(string pstrPeriodID, string pstrLocationID, string pstrBinID)
		{
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS;
			try
			{
				string strSql = "SELECT L.LocationID, L.Code AS Location, B.BinID, B.Code AS Bin, P.ProductID,"
					+ " P.Code AS PartNo, P.Description AS PartName, P.Revision AS Model,"
					+ " C.Code AS Category, U.Code AS UM,"
					+ " SUM(ISNULL(OHQuantity,0)) OHQuantity, SUM(ISNULL(Actual,0)) Actual"
					+ " FROM ITM_Product P"
					+ " LEFT JOIN "
					+ " (SELECT LocationID, BinID, ProductID, SUM(ISNULL(BookQuantity,0)) AS OHQuantity, SUM(ISNULL(Quantity,0)) AS Actual"
					+ " FROM IV_StockTakingMaster SM JOIN IV_StockTaking ST"
					+ " ON SM.StockTakingMasterID = ST.StockTakingMasterID"
					+ " WHERE SM.StockTakingPeriodID = " + pstrPeriodID
					+ " GROUP BY LocationID, BinID, ProductID) AS A"
					+ " ON P.ProductID = A.ProductID"
					+ " LEFT JOIN ITM_Category C ON P.CategoryID = C.CategoryID"
					+ " JOIN MST_UnitOfMeasure U ON P.StockUMID = U.UnitOfMeasureID"
					+ " JOIN MST_Location L ON A.LocationID = L.LocationID"
					+ " JOIN MST_Bin B ON A.BinID = B.BinID"
					+ " WHERE 1=1";
				if (pstrLocationID != null && pstrLocationID.Length > 0)
					strSql += " AND A.LocationID IN (" + pstrLocationID + ")";
				if (pstrBinID != null && pstrBinID.Length > 0)
					strSql += " AND A.BinID IN (" + pstrBinID + ")";
				strSql += " GROUP BY L.LocationID, L.Code, B.BinID, B.Code, C.Code, "
					+ " P.ProductID, P.Code, P.Description, P.Revision, U.Code"
					+ " ORDER BY L.Code, B.Code, C.Code, "
					+ " P.Code, P.Description, P.Revision, U.Code";
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
		private DataTable GetStockTakingData(string pstrPeriodID, string pstrLocationID, string pstrBinID)
		{
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS;
			try
			{
				string strSql = "SELECT DISTINCT D.SlipCode, M.LocationID, M.BinID, D.ProductID,"
					+ " D.CountingMethodID, C.Code AS Method"
					+ " FROM IV_StockTaking D JOIN IV_StockTakingMaster M"
					+ " ON D.StockTakingMasterID = M.StockTakingMasteriD"
					+ " JOIN IV_CoutingMethod C ON D.CountingMethodID = C.CountingMethodID"
					+ " WHERE StockTakingPeriodID = " + pstrPeriodID;
				if (pstrLocationID != null && pstrLocationID.Length > 0)
					strSql += " AND M.LocationID IN (" + pstrLocationID + ")";
				if (pstrBinID != null && pstrBinID.Length > 0)
					strSql += " AND M.BinID IN (" + pstrBinID + ")";
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
		private DataRow GetPeriod(string pstrPeriodID)
		{
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS;
			try
			{
				string strSql = "SELECT Description, FromDate FROM IV_StockTakingPeriod WHERE StockTakingPeriodID = " + pstrPeriodID;
				oconPCS = new OleDbConnection(mConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();

				DataTable dtbData = new DataTable();
				OleDbDataAdapter adapter = new OleDbDataAdapter(ocmdPCS);
				adapter.Fill(dtbData);
				return dtbData.Rows[0];
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
		private string GetMasLoc(string pstrMasLocID)
		{
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS;
			try
			{
				string strSql = "SELECT Code + ' (' + Name + ')' FROM MST_MasterLocation"
					+ " WHERE MasterLocationID = " + pstrMasLocID;
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
		private string GetLocation(string pstrLocID)
		{
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS;
			try
			{
				string strSql = "SELECT Code + ' (' + Name + ')' FROM MST_Location"
					+ " WHERE LocationID = " + pstrLocID;
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
		private string GetBin(string pstrBinID)
		{
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS;
			try
			{
				string strSql = "SELECT Code + ' (' + Name + ')' FROM MST_Bin"
					+ " WHERE BinID = " + pstrBinID;
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
		private string GetCountingMethod(string pstrLocationID, string pstrBinID, string pstrProductID, DataTable pdtbStockTakingData, out string strSlipCode)
		{
			strSlipCode = string.Empty;
			string strMethod = string.Empty;
			string strFilter = "LocationID = " + pstrLocationID
				+ " AND BinID = " + pstrBinID
				+ " AND ProductID = " + pstrProductID;
			DataRow[] drowMethod = pdtbStockTakingData.Select(strFilter);
			if (drowMethod.Length > 0)
			{
				strMethod = drowMethod[0][METHOD_COL].ToString();
				foreach (DataRow drowSlip in drowMethod)
					strSlipCode += drowSlip[SLIPCODE_COL].ToString() + "+";
			}
			if (strSlipCode.LastIndexOf("+") >= 0)
				strSlipCode = strSlipCode.Remove(strSlipCode.LastIndexOfAny("+".ToCharArray()), 1);
			return strMethod;
		}
	}
}