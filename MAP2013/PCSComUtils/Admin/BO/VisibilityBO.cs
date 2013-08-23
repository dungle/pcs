using System;
using System.Data;
using PCSComUtils.Admin.DS;
using PCSComUtils.Common;

using PCSComUtils.PCSExc;

namespace PCSComUtils.Admin.BO
{
	public class VisibilityBO
	{
		private const string THIS = "PCSComUtils.Admin.BO.ISys_VisibilityItemBO";

		public VisibilityBO()
		{
		}
		
		/// <summary>
		/// This method checks business rule and call Add() method of DS class 
		/// </summary>
		/// <param name="pobjObjectVO">VisibityItemVO object</param>
		/// <Author> Hung LA, Dec 13, 2004</Author>
	
		public void Add(object pobjObjectVO)
		{
			try
			{
				Sys_VisibilityItemDS templateDS = new Sys_VisibilityItemDS();
				templateDS.Add(pobjObjectVO);				
			}
			catch (PCSDBException ex)
			{				
				throw ex;
			}
		}

		/// <summary>
		/// This method not implements yet
		/// </summary>
		/// <param name="pintID"></param>
		/// <param name="VOclass"></param>
		/// <returns></returns>
	
		public object GetObjectVO(int pintID, string VOclass)
		{
			const string METHOD_NAME = THIS + ".GetObjectVO()";
			throw new PCSException(ErrorCode.NOT_IMPLEMENT, METHOD_NAME, new Exception());
		}

		public void UpdateDataSet(DataSet dstData)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// This method not implements yet
		/// </summary>
		/// <param name="pObjectVO"></param>
	
		public void Delete(object pObjectVO)
		{
			const string METHOD_NAME = THIS + ".Delete()";
			throw new PCSException(ErrorCode.NOT_IMPLEMENT, METHOD_NAME, new Exception());
		}		
		
		/// <summary>
		/// Delete a record  from Database. This method checks business rule and call Delete() method of DS class 
		/// </summary>
		/// <param name="pintID"></param>
		/// <Author> Hung LA, Dec 13, 2004</Author>
	
		public void Delete(int pintID)
		{
			try
			{
				Sys_VisibilityItemDS templateDS = new Sys_VisibilityItemDS();
				templateDS.Delete(pintID);				
			}
			catch (PCSDBException ex)
			{				
				throw ex;
			}
		}	
		
		/// <summary>
		/// This method uses to get Sys_VisibilityItemVO object
		/// </summary>
		/// <param name="pintID">Sys_VisibilityItem identity</param>
		/// <returns></returns>
		/// <Author> Hung LA, Dec 13, 2004</Author>
	
		public object GetObjectVO(int pintID)
		{
			try
			{
				Sys_VisibilityItemDS templateDS = new Sys_VisibilityItemDS();
				return templateDS.GetObjectVO(pintID);

			}
			catch (PCSDBException ex)
			{
				throw ex;
			}
		}
		
		/// <summary>
		/// This method uses to update data
		/// </summary>
		/// <param name="pobjObjecVO"></param>
		/// <Author> Hung LA, Dec 13, 2004</Author>
	
		public void Update(object pobjObjecVO)
		{
			try
			{
				Sys_VisibilityItemDS templateDS = new Sys_VisibilityItemDS();
				templateDS.Update(pobjObjecVO);				
			}
			catch (PCSDBException ex)
			{				
				throw ex;
			}
		}	
		
		/// <summary>
		/// This method uses to get all data
		/// </summary>
		/// <returns>Dataset</returns>
		/// <Author> Hung LA, Dec 13, 2004</Author>
	
		public DataSet List()
		{
			try
			{
				Sys_VisibilityItemDS templateDS = new Sys_VisibilityItemDS();
				return templateDS.List();
			}
			catch (PCSDBException ex)
			{
				throw ex;
			}
		}
		
		
		/// <summary>
		/// This method uses to update a DataSet
		/// </summary>
		/// <param name="pData"></param>
		/// <Author> SonHT, Dec 13, 2004</Author>
	
		public void UpdateDataSetRoleAndItem(DataSet pData)
		{
			Sys_VisibilityGroupDS templateDS = new Sys_VisibilityGroupDS();
			templateDS.UpdateDataSetRoleAndItem(pData);
		}

		/// <summary>
		/// This method uses to get all visibility data
		/// </summary>
		/// <returns>DataSet</returns>
		/// <Author> Hung LA, Dec 13, 2004</Author>
	
		public DataSet GetVisibilityData()
		{
			const string METHOD_NAME = THIS + "GetVisibilityData()";
			
			// TODO : Remove after constant approved
			const int CANNOT_GET_VISIBILITY_DATA = -1;

			DataSet dstData = new DataSet();
			try
			{
				Sys_VisibilityGroupDS dsVisibilityGroup = new Sys_VisibilityGroupDS();
				dstData.Merge(dsVisibilityGroup.List().Tables[0]);
				Sys_VisibilityItemDS dsVisibilityItem = new Sys_VisibilityItemDS();
				dstData.Merge(dsVisibilityItem.List().Tables[0]);
				Sys_VisibilityGroup_RoleDS dsVisibilityGroup_Role = new Sys_VisibilityGroup_RoleDS();
				dstData.Merge(dsVisibilityGroup_Role.List().Tables[0]);
				Sys_RoleDS dsRole = new Sys_RoleDS();
				dstData.Merge(dsRole.ListAll().Tables[0]);
			}
			catch(PCSDBException ex)
			{
				throw new PCSBOException(CANNOT_GET_VISIBILITY_DATA,METHOD_NAME,ex);
			}		
			catch (Exception ex)
			{
				throw ex;
			}
			return dstData;
		}

		/// <summary>
		/// Add new Visibility group and return it's Identity
		/// </summary>
		/// <param name="pobjVO">Sys_VisibilityGroupVO object</param>
		/// <returns></returns>
		/// <Author> Hung LA, Dec 13, 2004</Author>
	
		public int AddAndReturnID(Sys_VisibilityGroupVO pobjVO)
		{
			const string METHOD_NAME = THIS + "GetVisibilityData()";
			const int CANNOT_INSERT_VISIBILITY_DATA = -1;

			try
			{
				Sys_VisibilityGroupDS dsVisibilityGroup = new Sys_VisibilityGroupDS();
				return dsVisibilityGroup.AddAndReturnID(pobjVO);
			}
			catch(PCSDBException ex)
			{
				throw new PCSBOException(CANNOT_INSERT_VISIBILITY_DATA,METHOD_NAME,ex);
			}		
			catch (Exception ex)
			{
				throw ex;
			}			
		}

		/// <summary>
		/// Update data in Sys_Visibility dataset
		/// </summary>
		/// <param name="pdstData">Sys_Visibility dataset</param>
		/// <Author> Hung LA, Dec 13, 2004</Author>
	
		public void UpdateAllDataSet(DataSet pdstData)
		{
			Sys_VisibilityGroupDS dsVisibilityGroup = new Sys_VisibilityGroupDS();
			dsVisibilityGroup.UpdateAllDataSet(pdstData);
		}

		/// <summary>
		/// Get visible control
		/// </summary>
		/// <param name="pstrFormName"></param>
		/// <param name="pstrRoleIDs"></param>
		/// <returns></returns>
		/// <Authors>SonHT</Authors>
	
		public DataTable GetVisibleControl(string pstrFormName,string[] pstrRoleIDs)
		{
			Sys_VisibilityGroupDS dsHidden = new Sys_VisibilityGroupDS();
			return dsHidden.GetVisibleControl(pstrFormName, string.Empty, pstrRoleIDs);
		}

		/// <summary>
		/// Get visible control
		/// </summary>
		/// <returns></returns>
		/// <Authors>SonHT </Authors>
	
		public void UpdateAllRole()
		{
			(new Sys_VisibilityGroup_RoleDS()).UpdateAllRole();
		}

	}
}