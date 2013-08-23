using System;
using System.Collections;
using PCSComUtils.Framework.ReportFrame.DS;

using PCSComUtils.PCSExc;

namespace PCSComUtils.Framework.ReportFrame.BO
{
	/// <summary>
	/// Summary description for LastReportBO.
	/// </summary>
	public class LastReportBO
	{
		public LastReportBO()
		{
		}
		#region IObjectBO Members

		public void Add(object pObjectDetail)
		{
			// TODO:  Add LastReportBO.Add implementation
		}

		public void Delete(object pObjectVO)
		{
			// TODO:  Add LastReportBO.Delete implementation
		}

		public object GetObjectVO(int pintID, string VOclass)
		{
			// TODO:  Add LastReportBO.GetObjectVO implementation
			return null;
		}

		public void UpdateDataSet(System.Data.DataSet dstData)
		{
			// TODO:  Add LastReportBO.UpdateDataSet implementation
		}

		public void Update(object pObjectDetail)
		{
			// TODO:  Add LastReportBO.Update implementation
		}

		#endregion

		//**************************************************************************              
		///    <Description>
		///       Get last 10 executed report from history
		///    </Description>
		///    <Inputs>
		///       
		///    </Inputs>
		///    <Outputs>
		///       
		///    </Outputs>
		///    <Returns>
		///       
		///    </Returns>
		///    <Authors>
		///       DungLA
		///    </Authors>
		///    <History>
		///       20-Jan-2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
	
		public ArrayList GetLast10Report(string pstrUserName)
		{
			try
			{
				sys_ReportHistoryDS dsHistory = new sys_ReportHistoryDS();
				return dsHistory.GetLast10Report(pstrUserName);
			}
			catch (PCSException ex)
			{
				throw ex.CauseException;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		//**************************************************************************              
		///    <Description>
		///       Get HistoryPara value
		///    </Description>
		///    <Inputs>
		///       
		///    </Inputs>
		///    <Outputs>
		///       
		///    </Outputs>
		///    <Returns>
		///       
		///    </Returns>
		///    <Authors>
		///       DungLA
		///    </Authors>
		///    <History>
		///       20-Jan-2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
	
		public ArrayList GetHistoryPara(string pstrHistoryID)
		{
			try
			{
				sys_ReportHistoryParaDS dsHistoryPara = new sys_ReportHistoryParaDS();
				return dsHistoryPara.ListByHistory(pstrHistoryID);
			}
			catch (PCSException ex)
			{
				throw ex.CauseException;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
	}
}
