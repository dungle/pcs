using System;
using System.Data;
using PCSComUtils.PCSExc;
using PCSComUtils.Common;
using PCSComUtils.Framework.ReportFrame.DS;

namespace PCSComUtils.Framework.ReportFrame.BO
{
	public class DrillDownReportBO
	{
		private const string THIS = "PCSComUtils.Framework.ReportFrame.BO.DrillDownReportBO";		
		public void Add(object pobjObjectVO)
		{
			try
			{
				sys_ReportDrillDownDS  templateDS = new sys_ReportDrillDownDS();
				templateDS.Add(pobjObjectVO);
				
			}
			catch(PCSDBException ex)
			{
				
				throw ex;
			}
			catch(Exception ex)
			{
				
				throw ex;
			}
		}	
		public object GetObjectVO(int pintID,string VOclass)
		{
			const string METHOD_NAME = THIS + ".GetObjectVO()";
			throw new PCSException(ErrorCode.NOT_IMPLEMENT, METHOD_NAME,new Exception());
		}	
		public void Delete(object pObjectVO)
		{
			const string METHOD_NAME = THIS + ".Delete()";
			throw new PCSException(ErrorCode.NOT_IMPLEMENT, METHOD_NAME,new Exception());
		}	
		public void Delete(int pintID)
		{
			try
			{
				sys_ReportDrillDownDS  templateDS = new sys_ReportDrillDownDS();
				templateDS.Delete(pintID);
				
			}
			catch(PCSDBException ex)
			{
				
				throw ex;
			}
			catch(Exception ex)
			{
				
				throw ex;
			}
		}
		public int Delete(string pstrMasterID, string pstrDetailID)
		{
			try
			{
				sys_ReportDrillDownDS  dsReportDrillDown = new sys_ReportDrillDownDS();
				int nReturn = dsReportDrillDown.Delete(pstrMasterID, pstrDetailID);
				
				return nReturn;
			}
			catch(PCSDBException ex)
			{
				
				throw ex;
			}
			catch(Exception ex)
			{
				
				throw ex;
			}
		}
		public object GetObjectVO(int pintID)
		{	
			try
			{
				sys_ReportDrillDownDS  templateDS = new sys_ReportDrillDownDS();
				object objVO = templateDS.GetObjectVO(pintID);
				
				return objVO;
			}
			catch(PCSDBException ex)
			{
				
				throw ex;
			}
			catch(Exception ex)
			{
				
				throw ex;
			}
		}
		public DataSet GetObjectVO(string pstrMasterID, string pstrDetailID)
		{	
			try
			{
				sys_ReportDrillDownDS  dsReportDrillDown = new sys_ReportDrillDownDS();
				DataSet dstVO = dsReportDrillDown.GetObjectVO(pstrMasterID, pstrDetailID);
				
				return dstVO;
			}
			catch(PCSDBException ex)
			{
				
				throw ex;
			}
			catch(Exception ex)
			{
				
				throw ex;
			}
		}
		public void Update(object pobjObjecVO)
		{
			try
			{
				sys_ReportDrillDownDS  templateDS = new sys_ReportDrillDownDS();
				templateDS.Update(pobjObjecVO);
				
			}
			catch(PCSDBException ex)
			{
				
				throw ex;
			}
			catch(Exception ex)
			{
				
				throw ex;
			}
		}
		public DataSet List()
		{
			try
			{
				sys_ReportDrillDownDS  templateDS = new sys_ReportDrillDownDS();
				return templateDS.List();
			}
			catch(PCSDBException ex)
			{
				throw ex;
			}
		}
		public void UpdateDataSet(DataSet pData)
		{
			try
			{
				sys_ReportDrillDownDS  templateDS = new sys_ReportDrillDownDS();
				templateDS.UpdateDataSet(pData);
				
			}
			catch(PCSDBException ex)
			{
				
				throw ex;
			}
			catch (Exception ex)
			{
				
				throw ex;
			}
		}
		public DataTable GetDataForTrueDBGrid(string pstrMasterID, string pstrDetailID, out bool oblnIsEdit)
		{
			try
			{
				sys_ReportDrillDownDS dsReportDrillDown = new sys_ReportDrillDownDS();
				return dsReportDrillDown.GetDataForTrueDBGrid(pstrMasterID, pstrDetailID, out oblnIsEdit);
			}
			catch (PCSDBException ex)
			{
				throw ex;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
	}
}