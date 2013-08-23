using System;
using System.Collections;
using System.Data;

using PCSComProduction.DCP.DS;
using PCSComUtils.Common;

using PCSComUtils.PCSExc;

namespace PCSComProduction.DCP.BO
{
	public interface IChangeCategoryBO
	{
		DataSet List(int pintChangeCategoryMasterID);
		void UpdateDataSet(DataSet pdstData, ArrayList parlDeletedItems);
		DataSet ListMatrixByChangeCategoryMasterID(int pintID);
		int UpdateMasterAndDetail(object pobjMaster, DataSet dstDetail, ArrayList parlDeletedItems);
		object GetObjectMaster(int pintCCNID, int pintWorkCenterID);
		void DeleteMasterAndDetail(int pintMasterID);
		void UpdateDataSetMatrix(DataSet pdstData, int pintMasterID);
	}
	/// <summary>
	/// Summary description for ChangeCategoryBO.
	/// </summary>
	
	public class ChangeCategoryBO : IChangeCategoryBO
	{
		public ChangeCategoryBO()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		
	
		public DataSet List(int pintChangeCategoryMasterID)
		{
			try
			{
				return new PRO_ChangeCategoryDetailDS().List(pintChangeCategoryMasterID);
			}
			catch (PCSDBException ex)
			{
				throw ex;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message, ex);
			}
		}

		/// <summary>
		/// Return the DataSet (list of record) by inputing the FieldList and Condition
		/// </summary>
	
		public void UpdateDataSet(DataSet pdstData, ArrayList parlDeletedItems)
		{
			try
			{
				string strListItemID = "(0";
				for (int i =0; i < parlDeletedItems.Count; i++)
				{
					strListItemID += "," + parlDeletedItems[i].ToString();
				}
				strListItemID += ")";
				
				new PRO_ChangeCategoryMatrixDS().DeleteByItems(strListItemID);
				new PRO_ChangeCategoryDetailDS().UpdateDataSet(pdstData);
			}
			catch (PCSDBException ex)
			{
				throw ex;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message, ex);
			}
		}

	
		public DataSet ListMatrixByChangeCategoryMasterID(int pintID)
		{
			try
			{
				return new PRO_ChangeCategoryMatrixDS().ListMatrixByChangeCategoryMasterID(pintID);
			}
			catch (PCSDBException ex)
			{
				throw ex;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message, ex);
			}
		}
		
	
		public int UpdateMasterAndDetail(object pobjMaster, DataSet dstDetail, ArrayList parlDeletedItems)
		{
			try
			{
				if (((PRO_ChangeCategoryMasterVO) pobjMaster).ChangeCategoryMasterID == 0)
				{
					// Add Master
					((PRO_ChangeCategoryMasterVO) pobjMaster).ChangeCategoryMasterID = new PRO_ChangeCategoryMasterDS().AddAndRetrurnID(pobjMaster);
				}
				else
				{
					new PRO_ChangeCategoryMasterDS().Update(pobjMaster);
				}

				// Update Detail
				foreach (DataRow drow in dstDetail.Tables[0].Rows)
				{
					if (drow.RowState == DataRowState.Added)
					{
						drow[PRO_ChangeCategoryDetailTable.CHANGECATEGORYMASTERID_FLD] = ((PRO_ChangeCategoryMasterVO) pobjMaster).ChangeCategoryMasterID;
					}
				}
				UpdateDataSet(dstDetail, parlDeletedItems);

				return ((PRO_ChangeCategoryMasterVO) pobjMaster).ChangeCategoryMasterID;
			}
			catch (PCSDBException ex)
			{
				throw ex;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message, ex);
			}
		}

	
		public object GetObjectMaster(int pintCCNID, int pintWorkCenterID)
		{
			try
			{
				return new PRO_ChangeCategoryMasterDS().GetObjectVO(pintCCNID, pintWorkCenterID);
			}
			catch (PCSDBException ex)
			{
				throw ex;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message, ex);
			}
		}

	
		public void DeleteMasterAndDetail(int pintMasterID)
		{
			try
			{
				DataSet dstDetail = new PRO_ChangeCategoryDetailDS().List(pintMasterID);
				ArrayList arlDetele = new ArrayList();
				foreach (DataRow drow in dstDetail.Tables[0].Rows)
				{
					arlDetele.Add(drow[ITM_ProductTable.PRODUCTID_FLD].ToString());
					drow.Delete();
				}
				UpdateDataSet(dstDetail, arlDetele);
				new PRO_ChangeCategoryMasterDS().Delete(pintMasterID);
			}
			catch (PCSDBException ex)
			{
				throw ex;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message, ex);
			}
		}

	
		public void UpdateDataSetMatrix(DataSet pdstData, int pintMasterID)
		{
			try
			{
				//Delete before update
				new PRO_ChangeCategoryMatrixDS().Delete(pintMasterID);

				//update
				new PRO_ChangeCategoryMatrixDS().UpdateDataSet(pdstData);
			}
			catch (PCSDBException ex)
			{
				throw ex;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message, ex);
			}
		}

		public void Delete(object pObjectVO)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Get the object information by ID of VO class
		/// </summary>
		public object GetObjectVO(int pintID, string VOclass)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Update into Database
		/// </summary>
		public void Update(object pObjectDetail)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Insert a new record into database
		/// </summary>
		public void Add(object pObjectDetail)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Insert a new record into database
		/// </summary>
		public void UpdateDataSet(DataSet dstData)
		{
			throw new NotImplementedException();
		}
	}
}
