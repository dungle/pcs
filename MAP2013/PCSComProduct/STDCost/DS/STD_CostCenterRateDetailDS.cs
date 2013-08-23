using System;
using System.Data;
using System.Data.OleDb;
using PCSComUtils.Common;
using PCSComUtils.DataAccess;

using PCSComUtils.PCSExc;

namespace PCSComProduct.STDCost.DS
{
	public class STD_CostCenterRateDetailDS 
	{
		public STD_CostCenterRateDetailDS()
		{
		}

		private const string THIS = "PCSComProduct.STDCost.DS.STD_CostCenterRateDetailDS";
		//**************************************************************************              
		///    <Description>
		///       This method uses to add data to STD_CostCenterRateDetail
		///    </Description>
		///    <Inputs>
		///        STD_CostCenterRateDetailVO       
		///    </Inputs>
		///    <Outputs>
		///       newly inserted primarkey value
		///    </Outputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <Authors>
		///       HungLa
		///    </Authors>
		///    <History>
		///       Tuesday, February 07, 2006
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public void Add(object pobjObjectVO)
		{
			const string METHOD_NAME = THIS + ".Add()";

			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				STD_CostCenterRateDetailVO objObject = (STD_CostCenterRateDetailVO) pobjObjectVO;
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand("", oconPCS);

				strSql = "INSERT INTO STD_CostCenterRateDetail("
					+ STD_CostCenterRateDetailTable.COSTCENTERRATEMASTERID_FLD + ","
					+ STD_CostCenterRateDetailTable.COSTELEMENTID_FLD + ","
					+ STD_CostCenterRateDetailTable.COST_FLD + ")"
					+ "VALUES(?,?,?)";

				ocmdPCS.Parameters.Add(new OleDbParameter(STD_CostCenterRateDetailTable.COSTCENTERRATEMASTERID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[STD_CostCenterRateDetailTable.COSTCENTERRATEMASTERID_FLD].Value = objObject.CostCenterRateMasterID;

				ocmdPCS.Parameters.Add(new OleDbParameter(STD_CostCenterRateDetailTable.COSTELEMENTID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[STD_CostCenterRateDetailTable.COSTELEMENTID_FLD].Value = objObject.CostElementID;

				ocmdPCS.Parameters.Add(new OleDbParameter(STD_CostCenterRateDetailTable.COST_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[STD_CostCenterRateDetailTable.COST_FLD].Value = objObject.Cost;


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

		/// <summary>
		/// Delete by Master object
		/// </summary>
		/// <param name="pintID">Master ID</param>
		public void Delete(int pintID)
		{
			const string METHOD_NAME = THIS + ".Delete()";
			string strSql = String.Empty;
			strSql = "DELETE " + STD_CostCenterRateDetailTable.TABLE_NAME + " WHERE  " + STD_CostCenterRateDetailTable.COSTCENTERRATEMASTERID_FLD + "=" + pintID.ToString();
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
		/// Get CostCenterRateDetailVO object from ID
		/// </summary>
		/// <param name="pintID"></param>
		/// <returns></returns>
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
					+ STD_CostCenterRateDetailTable.COSTCENTERRATEDETAILID_FLD + ","
					+ STD_CostCenterRateDetailTable.COSTCENTERRATEMASTERID_FLD + ","
					+ STD_CostCenterRateDetailTable.COSTELEMENTID_FLD + ","
					+ STD_CostCenterRateDetailTable.COST_FLD
					+ " FROM " + STD_CostCenterRateDetailTable.TABLE_NAME
					+ " WHERE " + STD_CostCenterRateDetailTable.COSTCENTERRATEDETAILID_FLD + "=" + pintID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();

				STD_CostCenterRateDetailVO objObject = new STD_CostCenterRateDetailVO();

				while (odrPCS.Read())
				{
					objObject.CostCenterRateDetailID = int.Parse(odrPCS[STD_CostCenterRateDetailTable.COSTCENTERRATEDETAILID_FLD].ToString().Trim());
					objObject.CostCenterRateMasterID = int.Parse(odrPCS[STD_CostCenterRateDetailTable.COSTCENTERRATEMASTERID_FLD].ToString().Trim());
					objObject.CostElementID = int.Parse(odrPCS[STD_CostCenterRateDetailTable.COSTELEMENTID_FLD].ToString().Trim());
					objObject.Cost = Decimal.Parse(odrPCS[STD_CostCenterRateDetailTable.COST_FLD].ToString().Trim());

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

		//**************************************************************************              
		///    <Description>
		///       This method uses to update data to STD_CostCenterRateDetail
		///    </Description>
		///    <Inputs>
		///       STD_CostCenterRateDetailVO       
		///    </Inputs>
		///    <Outputs>
		///       
		///    </Outputs>
		///    <Returns>
		///       
		///    </Returns>
		///    <Authors>
		///       HungLa
		///    </Authors>
		///    <History>
		///       09-Dec-2004
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public void Update(object pobjObjecVO)
		{
			const string METHOD_NAME = THIS + ".Update()";

			STD_CostCenterRateDetailVO objObject = (STD_CostCenterRateDetailVO) pobjObjecVO;


			//prepare value for parameters
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				strSql = "UPDATE STD_CostCenterRateDetail SET "
					+ STD_CostCenterRateDetailTable.COSTCENTERRATEMASTERID_FLD + "=   ?" + ","
					+ STD_CostCenterRateDetailTable.COSTELEMENTID_FLD + "=   ?" + ","
					+ STD_CostCenterRateDetailTable.COST_FLD + "=  ?"
					+ " WHERE " + STD_CostCenterRateDetailTable.COSTCENTERRATEDETAILID_FLD + "= ?";

				ocmdPCS.Parameters.Add(new OleDbParameter(STD_CostCenterRateDetailTable.COSTCENTERRATEMASTERID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[STD_CostCenterRateDetailTable.COSTCENTERRATEMASTERID_FLD].Value = objObject.CostCenterRateMasterID;

				ocmdPCS.Parameters.Add(new OleDbParameter(STD_CostCenterRateDetailTable.COSTELEMENTID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[STD_CostCenterRateDetailTable.COSTELEMENTID_FLD].Value = objObject.CostElementID;

				ocmdPCS.Parameters.Add(new OleDbParameter(STD_CostCenterRateDetailTable.COST_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[STD_CostCenterRateDetailTable.COST_FLD].Value = objObject.Cost;

				ocmdPCS.Parameters.Add(new OleDbParameter(STD_CostCenterRateDetailTable.COSTCENTERRATEDETAILID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[STD_CostCenterRateDetailTable.COSTCENTERRATEDETAILID_FLD].Value = objObject.CostCenterRateDetailID;


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

		//**************************************************************************              
		///    <Description>
		///       This method uses to get all data from STD_CostCenterRateDetail
		///    </Description>
		///    <Inputs>
		///               
		///    </Inputs>
		///    <Outputs>
		///       DataSet
		///    </Outputs>
		///    <Returns>
		///       DataSet
		///    </Returns>
		///    <Authors>
		///       HungLa
		///    </Authors>
		///    <History>
		///       Tuesday, February 07, 2006
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public DataSet List()
		{
			const string METHOD_NAME = THIS + ".List()";
			DataSet dstPCS = new DataSet();

			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = "SELECT STD_CostElement.Code, STD_CostElement.Name, STD_CostElement.CostElementID"
					+ " FROM STD_CostElement"
					+ " JOIN STD_CostElementType ON STD_CostElement.CostElementTypeID = STD_CostElementType.CostElementTypeID"
					+ " WHERE STD_CostElement.IsLeaf = 1"
					+ " AND STD_CostElementType.Code <> " + (int)CostElementType.Material
					+ " ORDER BY OrderNo ASC";
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();

				DataTable dtbData = new DataTable(STD_CostCenterRateDetailTable.TABLE_NAME);
				dtbData.Columns.Add(new DataColumn(STD_CostElementTable.CODE_FLD, typeof(string)));
				dtbData.Columns.Add(new DataColumn(STD_CostElementTable.NAME_FLD, typeof(string)));
				dtbData.Columns.Add(new DataColumn(STD_CostCenterRateDetailTable.COST_FLD, typeof(decimal)));
				dtbData.Columns.Add(new DataColumn(STD_CostElementTable.COSTELEMENTID_FLD, typeof(int)));
				dtbData.Columns.Add(new DataColumn(STD_CostCenterRateDetailTable.COSTCENTERRATEDETAILID_FLD, typeof(int)));
				dtbData.Columns[STD_CostCenterRateDetailTable.COSTCENTERRATEDETAILID_FLD].AutoIncrement = true;
				dtbData.Columns[STD_CostCenterRateDetailTable.COSTCENTERRATEDETAILID_FLD].AutoIncrementStep = 1;
				dtbData.Columns[STD_CostCenterRateDetailTable.COSTCENTERRATEDETAILID_FLD].AutoIncrementSeed = 1;
				dtbData.Columns.Add(new DataColumn(STD_CostCenterRateDetailTable.COSTCENTERRATEMASTERID_FLD, typeof(int)));

				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);

				DataTable dtbTemp = new DataTable();
				odadPCS.Fill(dtbTemp);

				foreach (DataRow drowTemp in dtbTemp.Rows)
				{
					DataRow drowData = dtbData.NewRow();
					foreach (DataColumn dcolData in dtbTemp.Columns)
					{
						drowData[dcolData.ColumnName] = drowTemp[dcolData.ColumnName];
					}
					dtbData.Rows.Add(drowData);
				}
				
				dstPCS.Tables.Add(dtbData);
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

		/// <summary>
		/// List detail by Master ID
		/// </summary>
		/// <param name="pintMasterID">Center Master ID</param>
		/// <returns>Details</returns>
		public DataSet List(int pintMasterID)
		{
			const string METHOD_NAME = THIS + ".List()";
			DataSet dstPCS = new DataSet();

			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = "SELECT STD_CostElement.Code, STD_CostElement.Name, STD_CostCenterRateDetail.Cost,"
					+ " STD_CostElement.CostElementID, STD_CostCenterRateDetail.CostCenterRateDetailID, "
					+ " STD_CostCenterRateDetail.CostCenterRateMasterID"
					+ " FROM STD_CostElement JOIN STD_CostElementType"
					+ " ON STD_CostElement.CostElementTypeID = STD_CostElementType.CostElementTypeID"
					+ " LEFT JOIN STD_CostCenterRateDetail"
					+ " ON STD_CostElement.CostElementID = STD_CostCenterRateDetail.CostElementID"
					+ " WHERE STD_CostElement.IsLeaf = 1"
					+ " AND STD_CostElementType.Code <> " + (int)CostElementType.Material;
				if (pintMasterID > 0)
					strSql += " AND (STD_CostCenterRateDetail.CostCenterRateMasterID = " + pintMasterID
						+ " OR STD_CostCenterRateDetail.CostCenterRateMasterID IS NULL)";
				strSql += " ORDER BY STD_CostElement.OrderNo";
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();

				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS, STD_CostCenterRateDetailTable.TABLE_NAME);

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

		/// <summary>
		/// List detail by CCN
		/// </summary>
		/// <param name="pintCCNID">CCN ID</param>
		/// <returns>Details</returns>
		public DataTable ListByCCN(int pintCCNID)
		{
			const string METHOD_NAME = THIS + ".ListByCCN()";
			
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = "SELECT CostCenterRateDetailID, STD_CostCenterRateDetail.CostCenterRateMasterID, CostElementID, Cost"
					+ " FROM STD_CostCenterRateDetail JOIN STD_CostCenterRateMaster"
					+ " ON STD_CostCenterRateDetail.CostCenterRateMasterID = STD_CostCenterRateMaster.CostCenterRateMasterID"
					+ " WHERE STD_CostCenterRateMaster.CCNID = " + pintCCNID;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();

				DataTable dtbData = new DataTable();
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dtbData);

				return dtbData;
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

		/// <summary>
		/// Update DataSet
		/// </summary>
		/// <param name="pData">Data to update</param>
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
					+ STD_CostCenterRateDetailTable.COSTCENTERRATEDETAILID_FLD + ","
					+ STD_CostCenterRateDetailTable.COSTCENTERRATEMASTERID_FLD + ","
					+ STD_CostCenterRateDetailTable.COSTELEMENTID_FLD + ","
					+ STD_CostCenterRateDetailTable.COST_FLD
					+ "  FROM " + STD_CostCenterRateDetailTable.TABLE_NAME;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				odadPCS.SelectCommand = new OleDbCommand(strSql, oconPCS);
				odcbPCS = new OleDbCommandBuilder(odadPCS);
				pData.EnforceConstraints = false;
				odadPCS.Update(pData, STD_CostCenterRateDetailTable.TABLE_NAME);

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
		/// Get miss elements from cost element
		/// </summary>
		/// <param name="pintRateMasterID">Cost Center Rate Master ID</param>
		/// <returns>DataTable</returns>
		public DataTable GetMissElement(int pintRateMasterID)
		{
			const string METHOD_NAME = THIS + ".GetMissElement()";
			DataTable dtbPCS = new DataTable();

			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = "SELECT STD_CostElement.Code, STD_CostElement.Name, STD_CostElement.CostElementID"
					+ " FROM STD_CostElement"
					+ " JOIN STD_CostElementType ON STD_CostElement.CostElementTypeID = STD_CostElementType.CostElementTypeID"
					+ " WHERE STD_CostElement.CostElementID NOT IN"
					+ " (SELECT CostElementID FROM STD_CostCenterRateDetail"
					+ " WHERE CostCenterRateMasterID = " + pintRateMasterID + ")"
					+ " AND STD_CostElementType.Code <> " + (int)CostElementType.Material
					+ " AND IsLeaf = 1";
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