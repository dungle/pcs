using System;
using System.Collections;
using System.Data;
using System.Data.OleDb;
using System.Drawing.Printing;
using System.Reflection;
using PCSComUtils.Common;
using PCSUtils.Utils;
using C1.Win.C1Preview;
using C1.C1Report;
using C1PrintPreviewDialog = PCSUtils.Framework.ReportFrame.C1PrintPreviewDialog;

namespace DestroySlipsReport
{
	public class DestroySlipsReport : MarshalByRefObject, IDynamicReport
	{
        private const string REPORT_NAME = "Destroy Slip";

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

	    public DataTable ExecuteReport(string fromDate, string toDate, string fromLoc, string toLoc, string fromBin, string toBin)
	    {
	        var dateFrom = DateTime.Parse(fromDate).Date;
	        var dateTo = DateTime.Parse(toDate).Date;
	        int fromLocId, fromBinId, toLocId, toBinId;
	        int.TryParse(fromLoc, out fromLocId);
	        int.TryParse(toLoc, out toLocId);
	        int.TryParse(fromBin, out fromBinId);
	        int.TryParse(toBin, out toBinId);

            DataTable reportData = GetReportData(dateFrom, dateTo, fromLocId, toLocId, fromBinId, toBinId);

            #region report

            //Check rows to select valid report lay out
            mLayoutFile = "DestroySlipsReport.xml";

            C1Report rptReport = new C1Report();

            rptReport.Load(mReportFolder + "\\" + mLayoutFile, rptReport.GetReportInfo(mReportFolder + "\\" + mLayoutFile)[0]);
            rptReport.Fields["fldFromDate"].Text = dateFrom.ToString("yyyy-MM-dd");
            rptReport.Fields["fldToDate"].Text = dateTo.ToString("yyyy-MM-dd");
	        if (fromLocId > 0)
	        {
                rptReport.Fields["fldFromLoc"].Text = GetLocationInfo(fromLocId);
            }
            if (toLocId > 0)
            {
                rptReport.Fields["fldToLoc"].Text = GetLocationInfo(toLocId);
            }
            if (fromBinId > 0)
            {
                rptReport.Fields["fldFromBin"].Text = GetBinInfo(fromBinId);
            }
            if (toBinId > 0)
            {
                rptReport.Fields["fldToBin"].Text = GetBinInfo(toBinId);
            }


            // set datasource object that provides data to report.
            rptReport.DataSource.Recordset = reportData;
            // render report
            rptReport.Render();

            // render the report into the PrintPreviewControl
	        C1PrintPreviewDialog ppvViewer = new C1PrintPreviewDialog
	        {
	            FormTitle = REPORT_NAME,
	            Report = rptReport
	        };
	        ppvViewer.ReportViewer.PreviewNavigationPanel.Visible = false;
	        ppvViewer.ReportViewer.Document = rptReport.Document;

	        ppvViewer.Show();

            #endregion

            return reportData;
	    }

	    private string GetLocationInfo(int locationId)
	    {
            OleDbConnection oconPcs = null;

            try
            {
                var command = string.Format("SELECT Code FROM MST_Location WHERE LocationID = {0}", locationId);

                oconPcs = new OleDbConnection(mConnectionString);
                var ocmdPcs = new OleDbCommand(command, oconPcs) { CommandTimeout = 3600 };

                ocmdPcs.Connection.Open();
                var result = ocmdPcs.ExecuteScalar();
                return result.ToString();
            }
            finally
            {
                if (oconPcs != null)
                {
                    if (oconPcs.State != ConnectionState.Closed)
                    {
                        oconPcs.Close();
                    }
                }
            }
        }
        private string GetBinInfo(int binId)
        {
            OleDbConnection oconPcs = null;

            try
            {
                var command = string.Format("SELECT Code FROM MST_BIN WHERE BinID = {0}", binId);

                oconPcs = new OleDbConnection(mConnectionString);
                var ocmdPcs = new OleDbCommand(command, oconPcs) { CommandTimeout = 3600 };

                ocmdPcs.Connection.Open();
                var result = ocmdPcs.ExecuteScalar();
                return result.ToString();
            }
            finally
            {
                if (oconPcs != null)
                {
                    if (oconPcs.State != ConnectionState.Closed)
                    {
                        oconPcs.Close();
                    }
                }
            }
        }

        public DataTable GetReportData(DateTime fromDate, DateTime toDate, int fromLocId, int toLocId, int fromBinId, int toBinId)
        {
            DataSet dstPcs = new DataSet();
            OleDbConnection oconPcs = null;

            try
            {
                string strSql = @"SELECT IV_MiscellaneousIssueMaster.TransNo,
	                                IV_MiscellaneousIssueMaster.PostDate,
	                                IV_MiscellaneousIssueMaster.Comment,
	                                Loc_From.Name + ' (' + Loc_From.Code + ')' as FromLocInfo,
	                                Bin_From.Name + ' (' + Bin_From.Code + ')' as FromBinInfo,
                                        Loc_To.Name + ' (' + Loc_To.Code + ')' as ToLocInfo,
	                                Bin_To.Name + ' (' + Bin_To.Code + ')' as ToBinInfo,        
	                                ITM_Product.ProductID,
	                                ITM_Product.Code as ITM_ProductCode,
                                        ITM_Product.Description as ITM_ProductDescription, 
	                                ITM_Product.Revision as ITM_ProductRevision, 
	                                IV_MiscellaneousIssueDetail.Quantity,
	                                MST_UnitOfMeasure.Code as StockUM,
	                                ITM_Category.Code as ITM_CategoryCode,
	                                MST_Party.Code as MST_PartyCode,
	                                MST_Department.Code AS Department,
	                                MST_Reason.Code AS Reason

                                FROM   IV_MiscellaneousIssueDetail 
	                                INNER JOIN IV_MiscellaneousIssueMaster ON IV_MiscellaneousIssueDetail.MiscellaneousIssueMasterID = IV_MiscellaneousIssueMaster.MiscellaneousIssueMasterID

	                                INNER JOIN ITM_Product ON IV_MiscellaneousIssueDetail.ProductID = ITM_Product.ProductID
	                                INNER JOIN MST_UnitOfMeasure ON ITM_Product.StockUMID = MST_UnitOfMeasure.UnitOfMeasureID

	                                INNER JOIN MST_Location Loc_From ON IV_MiscellaneousIssueMaster.SourceLocationID = Loc_From.LocationID
	                                INNER JOIN MST_BIN Bin_From ON IV_MiscellaneousIssueMaster.SourceBinID = Bin_From.BinID	

  	                                LEFT JOIN MST_Location Loc_To ON IV_MiscellaneousIssueMaster.DesLocationID = Loc_To.LocationID	
	                                LEFT JOIN MST_BIN Bin_To ON IV_MiscellaneousIssueMaster.DesBinID = Bin_To.BinID

	                                LEFT JOIN ITM_Category ON ITM_Category.CategoryID = ITM_Product.CategoryID
	                                LEFT JOIN MST_Party ON MST_Party.PartyID = ITM_Product.PrimaryVendorID
	
	                                LEFT JOIN MST_Department ON IV_MiscellaneousIssueDetail.DepartmentID = MST_Department.DepartmentID
	                                LEFT JOIN MST_Reason ON IV_MiscellaneousIssueDetail.ReasonID = MST_Reason.ReasonID
                                WHERE IV_MiscellaneousIssueMaster.IssuePurposeID = 14";
                
                if (fromDate > DateTime.MinValue && toDate > DateTime.MinValue)
                {
                    strSql += string.Format(" AND IV_MiscellaneousIssueMaster.PostDate BETWEEN '{0:yyyy-MM-dd}' AND '{1:yyyy-MM-dd}'", fromDate, toDate);
                }
                if (fromLocId > 0)
                {
                    strSql += " AND IV_MiscellaneousIssueMaster.SourceLocationID = " + fromLocId;
                }
                if (toLocId > 0)
                {
                    strSql += " AND IV_MiscellaneousIssueMaster.DesLocationID = " + toLocId;
                }
                if (fromBinId > 0)
                {
                    strSql += " AND IV_MiscellaneousIssueMaster.SourceBinID = " + fromBinId;
                }
                if (toBinId > 0)
                {
                    strSql += " AND IV_MiscellaneousIssueMaster.DesBinID = " + toBinId;
                }
                strSql += " ORDER BY ITM_Category.Code, ITM_Product.Revision, ITM_Product.Code";

                oconPcs = new OleDbConnection(mConnectionString);
                var ocmdPcs = new OleDbCommand(strSql, oconPcs) { CommandTimeout = 3600 };

                ocmdPcs.Connection.Open();
                var odadPcs = new OleDbDataAdapter(ocmdPcs);
                odadPcs.Fill(dstPcs, IV_MiscellaneousIssueMasterTable.TABLE_NAME);

                return dstPcs.Tables.Count > 0 ? dstPcs.Tables[0] : new DataTable();
            }
            finally
            {
                if (oconPcs != null)
                {
                    if (oconPcs.State != ConnectionState.Closed)
                    {
                        oconPcs.Close();
                    }
                }
            }
        }
    }
}