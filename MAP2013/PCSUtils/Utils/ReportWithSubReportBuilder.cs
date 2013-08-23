using System.Collections;

namespace PCSUtils.Utils
{

	/// <summary>
	/// Render report with named layout
	/// </summary>
	public class ReportWithSubReportBuilder : ReportBuilder
	{
		#region Properties

		protected string mstrApplicationPath;
		public string ApplicationPath
		{
			get{ return mstrApplicationPath;}
			set{ mstrApplicationPath = value;}
		}

		protected Hashtable mhtbSubreportDataSource;
		public Hashtable SubReportDataSources
		{
			get
			{ 
				if(mhtbSubreportDataSource != null)
				{
					return mhtbSubreportDataSource;
				}

				mhtbSubreportDataSource = new Hashtable();
				return mhtbSubreportDataSource;				
			}

			set
			{
				mhtbSubreportDataSource = value;
			}
		}		

		#endregion Properties

		/// <summary>
		/// Render the Report using DataSource on mppvReportViewer object
		/// </summary>
		public new void RenderReport()
		{
			// prevent re-entrant calls
			if (mrptReport.IsBusy)
			{
				return;
			}

			string strLayoutName = mstrReportDefinitionFolder + @"\" + mstrReportLayoutFile;
			
			mrptReport.Load(strLayoutName, mstrReportName);				
			// set datasource object that provides data to report.
			mrptReport.DataSource.Recordset = mdtbRenderDataTable;
			if(mhtbSubreportDataSource != null)
			{
				IDictionaryEnumerator iEnumerator = mhtbSubreportDataSource.GetEnumerator();
				while(iEnumerator.MoveNext())
				{
					string strFieldName = iEnumerator.Key.ToString().Trim();
					mrptReport.Fields[strFieldName].Subreport.Load(strLayoutName, strFieldName);
					mrptReport.Fields[strFieldName].Subreport.DataSource.Recordset = iEnumerator.Value;
					mrptReport.Fields[strFieldName].Subreport.Render();
					ReformatNumberInC1Report(mrptReport.Fields[strFieldName].Subreport);
				}
			}

			mrptReport.Render();
			ReformatNumberInC1Report(mrptReport);

			// render the report into the PrintPreviewControl
			mppvReportViewer.Document = mrptReport.Document;
		}
	}
}