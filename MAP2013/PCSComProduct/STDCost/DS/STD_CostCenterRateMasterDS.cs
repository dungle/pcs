using System;
using System.Data;
using System.Data.OleDb;
using PCSComUtils.Common;
using PCSComUtils.DataAccess;

using PCSComUtils.PCSExc;

namespace PCSComProduct.STDCost.DS
{
	public class STD_CostCenterRateMasterDS 
	{
		public STD_CostCenterRateMasterDS()
		{
		}

		private const string THIS = "PCSComProduct.STDCost.DS.STD_CostCenterRateMasterDS";

		public void Add(object pobjObjectVO)
		{
			const string METHOD_NAME = THIS + ".Add()";

			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				STD_CostCenterRateMasterVO objObject = (STD_CostCenterRateMasterVO) pobjObjectVO;
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand("", oconPCS);

				strSql = "INSERT INTO STD_CostCenterRateMaster("
					+ STD_CostCenterRateMasterTable.CCNID_FLD + ","
					+ STD_CostCenterRateMasterTable.CODE_FLD + ","
					+ STD_CostCenterRateMasterTable.NAME_FLD + ")"
					+ "VALUES(?,?,?)";

				ocmdPCS.Parameters.Add(new OleDbParameter(STD_CostCenterRateMasterTable.CCNID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[STD_CostCenterRateMasterTable.CCNID_FLD].Value = objObject.CCNID;

				ocmdPCS.Parameters.Add(new OleDbParameter(STD_CostCenterRateMasterTable.CODE_FLD, OleDbType.WChar));
				ocmdPCS.Parameters[STD_CostCenterRateMasterTable.CODE_FLD].Value = objObject.Code;

				ocmdPCS.Parameters.Add(new OleDbParameter(STD_CostCenterRateMasterTable.NAME_FLD, OleDbType.WChar));
				ocmdPCS.Parameters[STD_CostCenterRateMasterTable.NAME_FLD].Value = objObject.Name;


				ocmdPCS.CommandText = strSql;
				ocmdPCS.Connection.Open();
				ocmdPCS.ExecuteNonQuery();

			}
			catch (OleDbException ex)
			{
				if (ex.Errors.Count > 1)
				{
					if (ex.Errors[1].NativeError == ErrorCode.SQLDUPLICATE_KEYCODE)
					{
						throw new PCSDBException(ErrorCode.DUPLICATE_KEY, METHOD_NAME, ex);
					}
				}
				else
				{
					throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
				}
			}

			catch (InvalidOperationException ex)
			{
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
			}
			catch (Exception ex)
			{
				throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
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

		public int AddAndReturnID(object pobjObjectVO)
		{
			const string METHOD_NAME = THIS + ".AddAndReturnID()";
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				STD_CostCenterRateMasterVO objObject = (STD_CostCenterRateMasterVO) pobjObjectVO;
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand("", oconPCS);

				strSql = "INSERT INTO STD_CostCenterRateMaster("
					+ STD_CostCenterRateMasterTable.CCNID_FLD + ","
					+ STD_CostCenterRateMasterTable.CODE_FLD + ","
					+ STD_CostCenterRateMasterTable.NAME_FLD + ")"
					+ " VALUES(?,?,?)"
					+ "; SELECT @@IDENTITY AS NEWID";

				ocmdPCS.Parameters.Add(new OleDbParameter(STD_CostCenterRateMasterTable.CCNID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[STD_CostCenterRateMasterTable.CCNID_FLD].Value = objObject.CCNID;

				ocmdPCS.Parameters.Add(new OleDbParameter(STD_CostCenterRateMasterTable.CODE_FLD, OleDbType.WChar));
				ocmdPCS.Parameters[STD_CostCenterRateMasterTable.CODE_FLD].Value = objObject.Code;

				ocmdPCS.Parameters.Add(new OleDbParameter(STD_CostCenterRateMasterTable.NAME_FLD, OleDbType.WChar));
				ocmdPCS.Parameters[STD_CostCenterRateMasterTable.NAME_FLD].Value = objObject.Name;


				ocmdPCS.CommandText = strSql;
				ocmdPCS.Connection.Open();
				return int.Parse(ocmdPCS.ExecuteScalar().ToString());
			}
			catch (OleDbException ex)
			{
				if (ex.Errors.Count > 1)
				{
					if (ex.Errors[1].NativeError == ErrorCode.SQLDUPLICATE_KEYCODE)
					{
						throw new PCSDBException(ErrorCode.DUPLICATE_KEY, METHOD_NAME, ex);
					}
					else
					{
						throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
					}
				}
				else
				{
					throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
				}
			}

			catch (InvalidOperationException ex)
			{
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
			}
			catch (Exception ex)
			{
				throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
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

		public void Delete(int pintID)
		{
			const string METHOD_NAME = THIS + ".Delete()";
			string strSql = String.Empty;
			strSql = "DELETE " + STD_CostCenterRateMasterTable.TABLE_NAME + " WHERE  " + "CostCenterRateMasterID" + "=" + pintID.ToString();
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				ocmdPCS.ExecuteNonQuery();
				ocmdPCS = null;

			}
			catch (OleDbException ex)
			{
				if (ex.Errors.Count > 1)
				{
					if (ex.Errors[1].NativeError == ErrorCode.SQLCASCADE_PREVENT_KEYCODE)
					{
						throw new PCSDBException(ErrorCode.CASCADE_DELETE_PREVENT, METHOD_NAME, ex);
					}
				}
				else
				{
					throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
				}
			}
			catch (Exception ex)
			{
				throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
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

		/// <summary>
		/// Gets CostCenterRateMasterVO obejct from ID
		/// </summary>
		/// <param name="pintID">Master ID</param>
		/// <returns>CostCenterRateMasterVO</returns>
		public object GetObjectVO(int pintID)
		{
			const string METHOD_NAME = THIS + ".GetObjectVO()";
			DataSet dstPCS = new DataSet();

			OleDbDataReader odrPCS = null;
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;
				strSql = "SELECT "
					+ STD_CostCenterRateMasterTable.COSTCENTERRATEMASTERID_FLD + ","
					+ STD_CostCenterRateMasterTable.CCNID_FLD + ","
					+ STD_CostCenterRateMasterTable.CODE_FLD + ","
					+ STD_CostCenterRateMasterTable.NAME_FLD
					+ " FROM " + STD_CostCenterRateMasterTable.TABLE_NAME
					+ " WHERE " + STD_CostCenterRateMasterTable.COSTCENTERRATEMASTERID_FLD + "=" + pintID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();

				STD_CostCenterRateMasterVO objObject = new STD_CostCenterRateMasterVO();

				while (odrPCS.Read())
				{
					objObject.CostCenterRateMasterID = int.Parse(odrPCS[STD_CostCenterRateMasterTable.COSTCENTERRATEMASTERID_FLD].ToString().Trim());
					objObject.CCNID = int.Parse(odrPCS[STD_CostCenterRateMasterTable.CCNID_FLD].ToString().Trim());
					objObject.Code = odrPCS[STD_CostCenterRateMasterTable.CODE_FLD].ToString().Trim();
					objObject.Name = odrPCS[STD_CostCenterRateMasterTable.NAME_FLD].ToString().Trim();

				}
				return objObject;
			}
			catch (OleDbException ex)
			{
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
			}

			catch (Exception ex)
			{
				throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
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

		public void Update(object pobjObjecVO)
		{
			const string METHOD_NAME = THIS + ".Update()";

			STD_CostCenterRateMasterVO objObject = (STD_CostCenterRateMasterVO) pobjObjecVO;


			//prepare value for parameters
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				strSql = "UPDATE STD_CostCenterRateMaster SET "
					+ STD_CostCenterRateMasterTable.CCNID_FLD + "=   ?" + ","
					+ STD_CostCenterRateMasterTable.CODE_FLD + "=   ?" + ","
					+ STD_CostCenterRateMasterTable.NAME_FLD + "=  ?"
					+ " WHERE " + STD_CostCenterRateMasterTable.COSTCENTERRATEMASTERID_FLD + "= ?";

				ocmdPCS.Parameters.Add(new OleDbParameter(STD_CostCenterRateMasterTable.CCNID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[STD_CostCenterRateMasterTable.CCNID_FLD].Value = objObject.CCNID;

				ocmdPCS.Parameters.Add(new OleDbParameter(STD_CostCenterRateMasterTable.CODE_FLD, OleDbType.WChar));
				ocmdPCS.Parameters[STD_CostCenterRateMasterTable.CODE_FLD].Value = objObject.Code;

				ocmdPCS.Parameters.Add(new OleDbParameter(STD_CostCenterRateMasterTable.NAME_FLD, OleDbType.WChar));
				ocmdPCS.Parameters[STD_CostCenterRateMasterTable.NAME_FLD].Value = objObject.Name;

				ocmdPCS.Parameters.Add(new OleDbParameter(STD_CostCenterRateMasterTable.COSTCENTERRATEMASTERID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[STD_CostCenterRateMasterTable.COSTCENTERRATEMASTERID_FLD].Value = objObject.CostCenterRateMasterID;


				ocmdPCS.CommandText = strSql;
				ocmdPCS.Connection.Open();
				ocmdPCS.ExecuteNonQuery();
			}
			catch (OleDbException ex)
			{
				if (ex.Errors.Count > 1)
				{
					if (ex.Errors[1].NativeError == ErrorCode.SQLDUPLICATE_KEYCODE)
					{
						throw new PCSDBException(ErrorCode.DUPLICATE_KEY, METHOD_NAME, ex);
					}
				}
				else
				{
					throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
				}
			}

			catch (InvalidOperationException ex)
			{
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
			}
			catch (Exception ex)
			{
				throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
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

		public DataSet List()
		{
			const string METHOD_NAME = THIS + ".List()";
			DataSet dstPCS = new DataSet();

			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;

				strSql = "SELECT "
					+ STD_CostCenterRateMasterTable.COSTCENTERRATEMASTERID_FLD + ","
					+ STD_CostCenterRateMasterTable.CCNID_FLD + ","
					+ STD_CostCenterRateMasterTable.CODE_FLD + ","
					+ STD_CostCenterRateMasterTable.NAME_FLD
					+ " FROM " + STD_CostCenterRateMasterTable.TABLE_NAME;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();

				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS, STD_CostCenterRateMasterTable.TABLE_NAME);

				return dstPCS;
			}
			catch (OleDbException ex)
			{
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
			}
			catch (Exception ex)
			{
				throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
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

		public void UpdateDataSet(DataSet pData)
		{
			const string METHOD_NAME = THIS + ".UpdateDataSet()";
			string strSql;
			OleDbConnection oconPCS = null;
			OleDbCommandBuilder odcbPCS;
			OleDbDataAdapter odadPCS = new OleDbDataAdapter();

			try
			{
				strSql = "SELECT "
					+ STD_CostCenterRateMasterTable.COSTCENTERRATEMASTERID_FLD + ","
					+ STD_CostCenterRateMasterTable.CCNID_FLD + ","
					+ STD_CostCenterRateMasterTable.CODE_FLD + ","
					+ STD_CostCenterRateMasterTable.NAME_FLD
					+ "  FROM " + STD_CostCenterRateMasterTable.TABLE_NAME;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				odadPCS.SelectCommand = new OleDbCommand(strSql, oconPCS);
				odcbPCS = new OleDbCommandBuilder(odadPCS);
				pData.EnforceConstraints = false;
				odadPCS.Update(pData, STD_CostCenterRateMasterTable.TABLE_NAME);

			}
			catch (OleDbException ex)
			{
				if (ex.Errors.Count > 1)
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
					throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
				}
			}

			catch (InvalidOperationException ex)
			{
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
			}
			catch (Exception ex)
			{
				throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
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
		/// <summary>
		/// Get cost by item from cost center rate
		/// </summary>
		/// <param name="pintCCNID">CCN</param>
		/// <returns>DataTable</returns>
		public DataTable GetCostFromCostCenterRate(int pintCCNID)
		{
			const string METHOD_NAME = THIS + ".GetCostFromCostCenterRate()";
			DataTable dtbPCS = new DataTable();

			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = "SELECT ITM_Product.ProductID, ITM_Product.CostCenterRateMasterID, STD_CostElement.CostElementID,"
					+ " STD_CostCenterRateDetail.Cost"
					+ " FROM ITM_Product JOIN STD_CostCenterRateMaster"
					+ " ON ITM_Product.CostCenterRateMasterID = STD_CostCenterRateMaster.CostCenterRateMasterID"
					+ " JOIN STD_CostCenterRateDetail"
					+ " ON STD_CostCenterRateMaster.CostCenterRateMasterID = STD_CostCenterRateDetail.CostCenterRateMasterID"
					+ " JOIN STD_CostElement"
					+ " ON STD_CostCenterRateDetail.CostElementID = STD_CostElement.CostElementID"
					+ " WHERE ITM_Product.CCNID = " + pintCCNID;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();

				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dtbPCS);

				return dtbPCS;
			}
			catch (OleDbException ex)
			{
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
			}
			catch (Exception ex)
			{
				throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
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