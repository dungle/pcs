//using PCSAssemblyLoader;
using System;
using System.Data;
using System.Data.OleDb;
using System.Reflection;
using C1.Win.C1Preview;
using C1.C1Report;
using PCSUtils.Utils;
using C1PrintPreviewDialog = PCSUtils.Framework.ReportFrame.C1PrintPreviewDialog;

namespace Tonghopvattuxuatchuyenkho
{
	/// <summary>	
	/// </summary>
	[Serializable]
	public class Tonghopvattuxuatchuyenkho : MarshalByRefObject, IDynamicReport
	{
		public Tonghopvattuxuatchuyenkho()
		{
		}

		#region IDynamicReport Implementation

		private string mConnectionString;
		private ReportBuilder mReportBuilder;
		private C1PrintPreviewControl mReportViewer;
		private bool mUseReportViewerRenderEngine = true;
		private object mResult;
		private string mReportFolder = string.Empty;
		private string mLayoutFile = string.Empty;

		// const field
		const string RPT_YEAR_FLD = "fldyear";
		const string RPT_MONTH_FLD = "fldmonth";
		const string RPT_PRODUCT_FLD = "fldproduct";
		const string RPT_LOC_FLD = "fldloc";
		const string RPT_BIN_FLD = "fldbin";

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
		public bool UseReportViewerRenderEngine
		{
			get { return mUseReportViewerRenderEngine; }
			set { mUseReportViewerRenderEngine = value; }
		}


		/// <summary>
		/// Inform External Process where to find out the ReportLayout	 ( the PCS' ReportDefinition Folder Path )
		/// </summary>				
		public string ReportDefinitionFolder
		{
			get { return mReportFolder; }
			set { mReportFolder = value; }
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
			get { return mLayoutFile; }
			set { mLayoutFile = value; }
		}


		public object Invoke(string pstrMethod, object[] pobjParameters)
		{
			return this.GetType().InvokeMember(pstrMethod, BindingFlags.InvokeMethod, null, this, pobjParameters);
		}

		#endregion

		/// <summary>
		/// Main function, generate the result data Table for the REPORT VIEWER
		/// </summary>
		/// <param name="pstrCCNID"></param>
		/// <returns></returns>
		public DataTable ExecuteReport(string pstrYear, string pstrMonth ,string pstrProductID, string pstrLoc, string pstrBin)
		{
			DataTable dtbReportData = GetReportData(pstrYear, pstrMonth, pstrProductID, pstrLoc, pstrBin);
			C1Report rptReport = new C1Report();

			if (mLayoutFile == string.Empty)
				mLayoutFile = "TongHopVatTuXuatChuyenKho.xml";
			rptReport.Load(mReportFolder + "\\" + mLayoutFile, rptReport.GetReportInfo(mReportFolder + "\\" + mLayoutFile)[0]);
			//rptReport.Layout.PaperSize = PaperKind.A3;

			// set datasource object that provides data to report.
			rptReport.DataSource.Recordset = dtbReportData;

			#region add field to report
			
			rptReport.Fields[RPT_YEAR_FLD].Text = pstrYear;
			rptReport.Fields[RPT_MONTH_FLD].Text = pstrMonth;
            

			if (pstrProductID.Split(",".ToCharArray()).Length > 1 )
			{
				rptReport.Fields[RPT_PRODUCT_FLD].Text = "Multi-Selection";
			}
			else
			{
				if (pstrProductID.Trim() != string.Empty)
				{
					DataTable objProduct = GetProductInfo(pstrProductID);
					foreach (DataRow drowItem in objProduct.Rows)
						rptReport.Fields[RPT_PRODUCT_FLD].Text = drowItem["Code"].ToString();
				}
				
			}
			
			if (pstrLoc.Split(",".ToCharArray()).Length > 1 )
			{
				rptReport.Fields[RPT_LOC_FLD].Text ="Multi-Selection";
			}
			else
			{
				if (pstrLoc.Trim() != string.Empty)
				{
					DataTable objLoc = GetLocInfo(pstrLoc);
					foreach (DataRow drowItem in objLoc.Rows)
						rptReport.Fields[RPT_LOC_FLD].Text = drowItem["Code"].ToString();
				}
			}
            
			if (pstrBin.Split(",".ToCharArray()).Length > 1 )
			{
				rptReport.Fields[RPT_BIN_FLD].Text = "Multi-Selection";
			}
			else
			{
				if (pstrBin.Trim() != string.Empty)
				{
					DataTable objBin = GetBinInfo(pstrBin);
					foreach (DataRow drowItem in objBin.Rows)
						rptReport.Fields[RPT_BIN_FLD].Text = drowItem["Code"].ToString();
				}
			}
			#endregion
			// render report
			rptReport.Render();

			// render the report into the PrintPreviewControl
			C1PrintPreviewDialog ppvViewer = new C1PrintPreviewDialog();
			//ppvViewer.FormTitle = "Costing Description " + dtmDate.ToString("MMMM-yyyy");
			
			ppvViewer.ReportViewer.Document = rptReport.Document;
			ppvViewer.Show();
			return dtbReportData;
		}

		private DataTable GetBinInfo(string pstrBinID)
		{
			OleDbConnection oconPCS = null;
			try
			{
				oconPCS = new OleDbConnection(mConnectionString);
				string strSql = "SELECT Code FROM MST_BIN WHERE BinID IN (" + pstrBinID + ")";
				OleDbCommand cmdData = new OleDbCommand(strSql, oconPCS);
				cmdData.Connection.Open();
				DataTable dtbData = new DataTable();
				OleDbDataAdapter odad = new OleDbDataAdapter(cmdData);
				odad.Fill(dtbData);
				return dtbData;
			}
			finally
			{
				if (oconPCS != null)
					if (oconPCS.State != ConnectionState.Closed)
						oconPCS.Close();
			}
		}

		private DataTable GetLocInfo(string pstrLocID)
		{
			OleDbConnection oconPCS = null;
			try
			{
				oconPCS = new OleDbConnection(mConnectionString);
				string strSql = "SELECT Code FROM MST_Location WHERE LocationID IN (" + pstrLocID + ")";
				OleDbCommand cmdData = new OleDbCommand(strSql, oconPCS);
				cmdData.Connection.Open();
				DataTable dtbData = new DataTable();
				OleDbDataAdapter odad = new OleDbDataAdapter(cmdData);
				odad.Fill(dtbData);
				return dtbData;
			}
			finally
			{
				if (oconPCS != null)
					if (oconPCS.State != ConnectionState.Closed)
						oconPCS.Close();
			}
		}

		private DataTable GetProductInfo(string pstrProductID)
		{
			OleDbConnection oconPCS = null;
			try
			{
				oconPCS = new OleDbConnection(mConnectionString);
				string strSql = "SELECT Code FROM ITM_Product WHERE ProductID IN (" + pstrProductID + ")";
				OleDbCommand cmdData = new OleDbCommand(strSql, oconPCS);
				cmdData.Connection.Open();
				DataTable dtbData = new DataTable();
				OleDbDataAdapter odad = new OleDbDataAdapter(cmdData);
				odad.Fill(dtbData);
				return dtbData;
			}
			finally
			{
				if (oconPCS != null)
					if (oconPCS.State != ConnectionState.Closed)
						oconPCS.Close();
			}
		}

		private DataTable GetReportData(string pstrYear, string pstrMonth ,string pstrProductID, string pstrLoc, string pstrBin)
		{
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			string strSql = string.Empty;
			try
			{
				DataSet dstPCS = new DataSet();
				oconPCS = null;
				ocmdPCS = null;
				#region strSql 
				strSql = " SELECT  " + 
					"S.code Source,C.Code Category,P.Revision Model,P.Code PartNo,P.Description PartName,PT.code Type,U.Code Unit,sum(A.Quantity) Quantity,DATEPART(month,A.PostDate) Month," +
					" DATEPART(year,A.PostDate) Year," + 
					" CASE WHEN (SELECT sum(ACH.ActualCost) FROM CST_ActualCostHistory ACH join cst_ActCostAllocationMaster ACAM ON ACAM.ActCostAllocationMasterID = ACH.ActCostAllocationMasterID " + 
					" WHERE ACH.ProductID = A.ProductID and DATEPART(year,ACAM.FromDate) = " + pstrYear.Trim() + " and DATEPART(month,ACAM.FromDate) = " + pstrMonth.Trim() + ") IS NOT NULL" +
					" THEN (SELECT sum(ACH.ActualCost) FROM CST_ActualCostHistory ACH join cst_ActCostAllocationMaster ACAM ON ACAM.ActCostAllocationMasterID = ACH.ActCostAllocationMasterID " + 
					" WHERE ACH.ProductID = A.ProductID and DATEPART(year,ACAM.FromDate) = " + pstrYear.Trim() + " and DATEPART(month,ACAM.FromDate) = " + pstrMonth.Trim() + ")" + 
					" WHEN (SELECT sum(STD.Cost) FROM dbo.CST_STDItemCost STD WHERE STD.ProductID = A.ProductID) IS NOT NULL" + 
					" THEN (SELECT sum(STD.Cost) FROM dbo.CST_STDItemCost STD WHERE STD.ProductID = A.ProductID)" + 
					" ELSE 0 " + 
					" END AS Price," + 
					" A.FromLocationID,A.ToLocationID,A.FromBin,A.ToBin,A.ProductId" + 
					" FROM (SELECT sum(IMD.CommitQuantity) Quantity,IMD.ProductID,IMM.PostDate," + 
					" LF.Code FromLocationID,LT.Code ToLocationID,BF.Code FromBin,BTo.Code ToBin, LF.LocationID, BF.BinID " + 
					" FROM PRO_IssueMaterialDetail IMD " + 
					" join PRO_IssueMaterialMaster IMM ON IMD.IssueMaterialMasterID = IMM.IssueMaterialMasterID" + 
					" inner join MST_Location LF ON LF.LocationID = IMD.LocationID " + 
					" inner join MST_Location LT ON LT.LocationID = IMM.ToLocationID" + 
					" inner join MST_BIN BF ON BF.BinID = IMD.BinID " + 
					" inner join MST_BIN BTo ON BTo.BinID = IMM.ToBinID " + 
					" WHERE (DATEPART(year,IMM.PostDate) = " + pstrYear.Trim() + " and DATEPART(month,IMM.PostDate) = " + pstrMonth.Trim() + ") " + 
					" GROUP BY IMD.ProductID,IMM.PostDate,LF.Code ,LT.Code ,BF.Code ,BTo.Code, LF.LocationID, BF.BinID  " + 
					" UNION " + 
					" SELECT sum(MID.Quantity)  Quantity,MID.ProductID,MIM.PostDate,LF.Code FromLocationID," + 
					" LT.Code  ToLocationID,BF.Code FromBin,BTo.Code ToBin, LF.LocationID, BF.BinID" + 
					" FROM IV_MiscellaneousIssueDetail MID " + 
					" join IV_MiscellaneousIssueMaster MIM ON MID.MiscellaneousIssueMasterID = MIM.MiscellaneousIssueMasterID" + 
					" inner join MST_Location LF ON LF.LocationID = MIM.SourceLocationID " + 
					" left join MST_Location LT ON MIM.DesLocationID = LT.LocationID " + 
					" inner join MST_BIN BF ON BF.BinID = MIM.SourceBinID " + 
					" left join MST_BIN BTo ON MIM.DesBinID = BTo.BinID" + 
					" WHERE (DATEPART(year,MIM.PostDate) = " + pstrYear.Trim() + " and DATEPART(month,MIM.PostDate) = " + pstrMonth.Trim() + ") " + 
					" GROUP BY MID.ProductID,MIM.PostDate,LF.Code,LT.Code,BF.Code,BTo.Code, LF.LocationID, BF.BinID" + 
					" UNION " + 
					" SELECT sum(CSD.ScrapQuantity) Quantity,CSD.ComponentID ProductID,CSM.PostDate," + 
					" LF.Code FromLocationID,LT.Code ToLocationID,BF.Code FromBin,BTo.Code ToBin, LF.LocationID, BF.BinID " + 
					" FROM PRO_ComponentScrapDetail CSD " + 
					" join PRO_ComponentScrapMaster CSM ON CSD.ComponentScrapMasterID = CSM.ComponentScrapMasterID" + 
					" inner join MST_Location LF ON LF.LocationID = CSD.FromLocationID " + 
					" inner join MST_Location LT ON LT.LocationID = CSD.ToLocationID" + 
					" inner join MST_BIN BF ON BF.BinID = CSD.FromBinID " + 
					" inner join MST_BIN BTo ON BTo.BinID = CSD.ToBinID" + 
					" WHERE (DATEPART(year,CSM.PostDate) = " + pstrYear.Trim() + " and DATEPART(month,CSM.PostDate) = " + pstrMonth.Trim() + ")" + 
					" GROUP BY CSD.ComponentID,CSM.PostDate,LF.Code,LT.Code,BF.Code,BTo.Code, LF.LocationID, BF.BinID" + 
					" UNION " + 
					" SELECT sum(RMD.RecoverQuantity) Quantity,RMD.ProductID,RMM.PostDate," + 
					" LF.Code FromLocationID,LT.Code ToLocationID,BF.Code FromBin,BTo.Code ToBin, LF.LocationID, BF.BinID" + 
					" FROM  CST_RecoverMaterialDetail RMD " + 
					" inner join CST_RecoverMaterialMaster RMM ON RMD.RecoverMaterialMasterID = RMM.RecoverMaterialMasterID" + 
					" inner join MST_Location LF ON LF.LocationID = RMM.FromLocationID " + 
					" left join MST_Location LT ON LT.LocationID = RMD.ToLocationID" + 
					" inner join MST_BIN BF ON BF.BinID = RMM.FromBinID " + 
					" left join MST_BIN BTo ON BTo.BinID = RMD.ToBinID" + 
					" WHERE (DATEPART(year,RMM.PostDate) = " + pstrYear.Trim() + " and DATEPART(month,RMM.PostDate) = " + pstrMonth.Trim() + ")" + 
					" GROUP BY RMD.ProductID,RMM.PostDate,LF.Code,LT.Code,BF.Code,BTo.Code, LF.LocationID, BF.BinID ) A " + 
					" inner join ITM_Product P ON A.ProductID = P.ProductID " + 
					" left join ITM_Source S ON P.SourceID = S.SourceID " + 
					" left join ITM_PRoductType PT ON P.ProductTypeID = PT.ProductTypeID" + 
					" left join ITM_Category C ON P.CategoryID = C.CategoryID " + 
					" inner join MST_UnitOfMeasure U ON P.StockUMID = U.UnitOfMeasureID  " +
					" WHERE (1=1";
				if (pstrProductID != null && pstrProductID != string.Empty)
					strSql += " AND A.ProductID in(" + pstrProductID + ")";

				if (pstrLoc != null && pstrLoc != string.Empty)
					strSql += " AND A.LocationID in(" + pstrLoc + ")";
				
				if (pstrBin != null && pstrBin != string.Empty)
					strSql += " AND A.BinID in(" + pstrBin + ")" ;

				strSql += ") GROUP BY S.code,C.Code,P.Revision,P.Code,P.Description,PT.code,U.Code,DATEPART(month,A.PostDate),DATEPART(year,A.PostDate)," + 
					" A.FromLocationID,A.ToLocationID,A.FromBin,A.ToBin,A.ProductId" + "  ";
				#endregion
				
				oconPCS = new OleDbConnection(mConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS);

				return dstPCS.Tables[0];
			}
			catch (OleDbException ex)
			{
				throw new Exception(strSql, ex);
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
	}
}
