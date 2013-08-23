using System;
using System.Data;
using System.Data.OleDb;
using PCSComUtils.Common;
using PCSComUtils.DataAccess;

using PCSComUtils.PCSExc;

namespace PCSComMaterials.ActualCost.DS
{
	/// <summary>
	/// Summary description for CST_DSAndRecycleAllocationDS.
	/// </summary>
	public class CST_DSAndRecycleAllocationDS 
	{
		public CST_DSAndRecycleAllocationDS()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public Object GetObjectVO(int pintID)
		{
			throw new NotImplementedException();
		}

		public DataSet List()
		{
			throw new NotImplementedException();
		}

		public void Delete(int pintID)
		{
			const string METHOD_NAME = ".Delete()";
			string strSql = "DELETE " + CST_DSAndRecycleAllocationTable.TABLE_NAME
				+ " WHERE " + CST_DSAndRecycleAllocationTable.ACTCOSTALLOCATIONMASTERID_FLD + "=" + pintID;
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			
			try
			{
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				ocmdPCS.ExecuteNonQuery();
			}
			catch(OleDbException ex)
			{
				if (ex.Errors.Count > 1)
				{
					if (ex.Errors[1].NativeError == ErrorCode.SQLCASCADE_PREVENT_KEYCODE)
						throw new PCSDBException(ErrorCode.CASCADE_DELETE_PREVENT, METHOD_NAME, ex);
				}
				else
					throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME,ex);
			}			
			catch(InvalidOperationException ex)
			{
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME,ex);
			}
			catch (Exception ex) 
			{
				throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
			}
			finally 
			{
				if (oconPCS!=null) 
					if (oconPCS.State != ConnectionState.Closed) 
						oconPCS.Close();
			}
		}

		public void Update(Object pobjObjecVO)
		{
			throw new NotImplementedException();
		}

		public void Add(Object pobjObjectVO)
		{
			throw new NotImplementedException();
		}

		public void UpdateDataSet(DataSet pData)
		{
			const string METHOD_NAME = ".UpdateDataSet()";
			string strSql;
			OleDbConnection oconPCS =null;
			OleDbCommandBuilder odcbPCS ;
			OleDbDataAdapter odadPCS = new OleDbDataAdapter();

			try
			{
				strSql=	"SELECT "
					+ CST_DSAndRecycleAllocationTable.ACTCOSTALLOCATIONMASTERID_FLD + ","
					+ CST_DSAndRecycleAllocationTable.COSTELEMENTID_FLD + ","
					+ CST_DSAndRecycleAllocationTable.DSADALLOCACTIONID_FLD + ","
					+ CST_DSAndRecycleAllocationTable.DSAMOUNT_FLD + ","
					+ CST_DSAndRecycleAllocationTable.DSRATE_FLD + ","
					+ CST_DSAndRecycleAllocationTable.PRODUCTID_FLD + ","
					+ CST_DSAndRecycleAllocationTable.RECYCLEAMOUNT_FLD + ","
					+ CST_DSAndRecycleAllocationTable.RETURNGOODSRECEIPTQTY_FLD + ","
					+ CST_DSAndRecycleAllocationTable.ADJUSTAMOUNT_FLD + ","
					+ CST_DSAndRecycleAllocationTable.OH_ADJUSTAMOUNT_FLD + ","
					+ CST_DSAndRecycleAllocationTable.OH_DSAMOUNT_FLD + ","
					+ CST_DSAndRecycleAllocationTable.OH_RECYCLEAMOUNT_FLD + ","
					+ CST_DSAndRecycleAllocationTable.DSOHRATE_FLD + ","
					+ CST_DSAndRecycleAllocationTable.SHIPPINGQTY_FLD
					+ "  FROM " + CST_DSAndRecycleAllocationTable.TABLE_NAME;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				odadPCS.SelectCommand = new OleDbCommand(strSql, oconPCS);
				odcbPCS = new OleDbCommandBuilder(odadPCS);
				pData.EnforceConstraints = false;
				odadPCS.Update(pData,CST_DSAndRecycleAllocationTable.TABLE_NAME);

			}
			catch(OleDbException ex)
			{
				if(ex.Errors.Count > 1)
				{
					if (ex.Errors[1].NativeError == ErrorCode.SQLDUPLICATE_KEYCODE)
					{
						throw new PCSDBException(ErrorCode.DUPLICATE_KEY, METHOD_NAME, ex);				
					}
					else if (ex.Errors[1].NativeError == ErrorCode.SQLCASCADE_PREVENT_KEYCODE)
					{
						throw new PCSDBException(ErrorCode.CASCADE_DELETE_PREVENT, METHOD_NAME, ex);				
					}
				}
				else
				{
					throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME,ex);
				}
			}			

			catch(InvalidOperationException ex)
			{
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME,ex);
			}
			catch (Exception ex) 
			{
				throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
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
	}
}
