using System;
using System.Collections;
using System.Data;
using System.Data.OleDb;
using PCSComUtils.Common;
using PCSComUtils.DataAccess;

using PCSComUtils.PCSExc;

namespace PCSComProduction.DCP.DS
{
	public class PRO_ProductionLineDS 
	{
		public PRO_ProductionLineDS()
		{
		}

		private const string THIS = "PCSComProduction.DCP.DS.PRO_ProductionLineDS";


		//**************************************************************************              
		///    <Description>
		///       This method uses to add data to PRO_ProductionLine
		///    </Description>
		///    <Inputs>
		///        PRO_ProductionLineVO       
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
		///       Wednesday, November 09, 2005
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
				PRO_ProductionLineVO objObject = (PRO_ProductionLineVO) pobjObjectVO;
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand("", oconPCS);

				strSql = "INSERT INTO PRO_ProductionLine("
					+ PRO_ProductionLineTable.CODE_FLD + ","
					+ PRO_ProductionLineTable.NAME_FLD + ","
					+ PRO_ProductionLineTable.DEPARTMENTID_FLD + ")"
					+ "VALUES(?,?,?)";

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_ProductionLineTable.CODE_FLD, OleDbType.WChar));
				ocmdPCS.Parameters[PRO_ProductionLineTable.CODE_FLD].Value = objObject.Code;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_ProductionLineTable.NAME_FLD, OleDbType.WChar));
				ocmdPCS.Parameters[PRO_ProductionLineTable.NAME_FLD].Value = objObject.Name;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_ProductionLineTable.DEPARTMENTID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PRO_ProductionLineTable.DEPARTMENTID_FLD].Value = objObject.DepartmentID;

				ocmdPCS.CommandText = strSql;
				ocmdPCS.Connection.Open();
				ocmdPCS.ExecuteNonQuery();

			}
			catch (OleDbException ex)
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
		///       This method uses to delete data from PRO_ProductionLine
		///    </Description>
		///    <Inputs>
		///        ID       
		///    </Inputs>
		///    <Outputs>
		///       void
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
		public void Delete(int pintID)
		{
			const string METHOD_NAME = THIS + ".Delete()";
			string strSql = String.Empty;
			strSql = "DELETE " + PRO_ProductionLineTable.TABLE_NAME + " WHERE  " + "ProductionLineID" + "=" + pintID.ToString();
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
				if (ex.Errors[1].NativeError == ErrorCode.SQLCASCADE_PREVENT_KEYCODE)
				{
					throw new PCSDBException(ErrorCode.CASCADE_DELETE_PREVENT, METHOD_NAME, ex);
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


		//**************************************************************************              
		///    <Description>
		///       This method uses to get data from PRO_ProductionLine
		///    </Description>
		///    <Inputs>
		///        ID       
		///    </Inputs>
		///    <Outputs>
		///       PRO_ProductionLineVO
		///    </Outputs>
		///    <Returns>
		///       PRO_ProductionLineVO
		///    </Returns>
		///    <Authors>
		///       HungLa
		///    </Authors>
		///    <History>
		///       Wednesday, November 09, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
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
					+ PRO_ProductionLineTable.PRODUCTIONLINEID_FLD + ","
					+ PRO_ProductionLineTable.CODE_FLD + ","
					+ PRO_ProductionLineTable.NAME_FLD + ","
					+ PRO_ProductionLineTable.DEPARTMENTID_FLD
					+ " FROM " + PRO_ProductionLineTable.TABLE_NAME
					+ " WHERE " + PRO_ProductionLineTable.PRODUCTIONLINEID_FLD + "=" + pintID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();

				PRO_ProductionLineVO objObject = new PRO_ProductionLineVO();

				while (odrPCS.Read())
				{
					objObject.ProductionLineID = int.Parse(odrPCS[PRO_ProductionLineTable.PRODUCTIONLINEID_FLD].ToString().Trim());
					objObject.Code = odrPCS[PRO_ProductionLineTable.CODE_FLD].ToString().Trim();
					objObject.Name = odrPCS[PRO_ProductionLineTable.NAME_FLD].ToString().Trim();
					objObject.DepartmentID = int.Parse(odrPCS[PRO_ProductionLineTable.DEPARTMENTID_FLD].ToString().Trim());

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
		///       This method uses to update data to PRO_ProductionLine
		///    </Description>
		///    <Inputs>
		///       PRO_ProductionLineVO       
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

			PRO_ProductionLineVO objObject = (PRO_ProductionLineVO) pobjObjecVO;


			//prepare value for parameters
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				strSql = "UPDATE PRO_ProductionLine SET "
					+ PRO_ProductionLineTable.CODE_FLD + "=   ?" + ","
					+ PRO_ProductionLineTable.NAME_FLD + "=   ?" + ","
					+ PRO_ProductionLineTable.DEPARTMENTID_FLD + "=  ?"
					+ " WHERE " + PRO_ProductionLineTable.PRODUCTIONLINEID_FLD + "= ?";

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_ProductionLineTable.CODE_FLD, OleDbType.WChar));
				ocmdPCS.Parameters[PRO_ProductionLineTable.CODE_FLD].Value = objObject.Code;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_ProductionLineTable.NAME_FLD, OleDbType.WChar));
				ocmdPCS.Parameters[PRO_ProductionLineTable.NAME_FLD].Value = objObject.Name;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_ProductionLineTable.DEPARTMENTID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PRO_ProductionLineTable.DEPARTMENTID_FLD].Value = objObject.DepartmentID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_ProductionLineTable.PRODUCTIONLINEID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PRO_ProductionLineTable.PRODUCTIONLINEID_FLD].Value = objObject.ProductionLineID;


				ocmdPCS.CommandText = strSql;
				ocmdPCS.Connection.Open();
				ocmdPCS.ExecuteNonQuery();
			}
			catch (OleDbException ex)
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
		/// Set Production Line value to Work Order
		/// </summary>
		/// <param name="phtbWorkCenterIDList">List of work order</param>
		/// <param name="pintProductionLineID">Pro. Line ID value: 0 or negative value means NULL value </param>
		public bool SetProductionLine4WorkOrder(Hashtable phtbWorkCenterIDList, int pintProductionLineID)
		{
			const string METHOD_NAME = THIS + ".SetProductionLine4WorkOrder()";

			//prepare value for parameters
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);

				//Build SQL string
				string strSql1 = " UPDATE MST_WorkCenter SET MST_WorkCenter.ProductionLineID = NULL";
				strSql1 += ", MST_WorkCenter.IsMain = 0";
				strSql1 += " WHERE MST_WorkCenter.WorkCenterID IN (SELECT WorkCenterID FROM MST_WorkCenter WHERE MST_WorkCenter.ProductionLineID =" + pintProductionLineID + ");";

				string strSql2 = " UPDATE MST_WorkCenter SET MST_WorkCenter.ProductionLineID = " + pintProductionLineID;
				strSql2 += ", MST_WorkCenter.IsMain = 0";
				strSql2 += " WHERE MST_WorkCenter.WorkCenterID IN (0";

				//Add condition list into SQL string
				if (phtbWorkCenterIDList != null)
				{
					IDictionaryEnumerator myEnumerator = phtbWorkCenterIDList.GetEnumerator();
					while (myEnumerator.MoveNext())
					{
						strSql2 += ", " + myEnumerator.Value.ToString();
					}
				}

				strSql2 += ")";

				ocmdPCS = new OleDbCommand(strSql1, oconPCS);

				ocmdPCS.Connection.Open();
				ocmdPCS.ExecuteNonQuery();

				ocmdPCS.CommandText = strSql2;
				ocmdPCS.ExecuteNonQuery();

				return true;
			}
			catch (OleDbException ex)
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
		/// Get list of Work center which belong to specific Production Line
		/// </summary>
		/// <param name="pintProductionLineID"></param>
		/// <returns>DataTable</returns>
		public DataTable GetWorkCenterByProductionLine(int pintProductionLineID)
		{
			const string METHOD_NAME = THIS + ".GetWorkCenterByProductionLine()";
			DataSet dstPCS = new DataSet();

			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;

			try
			{
				string strSql = " SELECT MST_WorkCenter.Code, "
					+ " MST_WorkCenter.[Name], "
					+ " MST_WorkCenter.IsMain, "
					+ " MST_WorkCenter.ProductionLineID, "
					+ " MST_WorkCenter.WorkCenterID "
					+ " FROM MST_WorkCenter "
					+ " WHERE MST_WorkCenter.ProductionLineID = " + pintProductionLineID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();

				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS, MST_WorkCenterTable.TABLE_NAME);

				if (dstPCS != null)
				{
					return dstPCS.Tables[MST_WorkCenterTable.TABLE_NAME];
				}

				return null;
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
		///       This method uses to get all data from PRO_ProductionLine
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
		///       Wednesday, November 09, 2005
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
				string strSql = "SELECT CAST(0 AS bit) Ignored, P.ProductionLineID, P.Code, P.Name,"
					+ " D.Code AS Department, L.Code AS Location, P.BalancePlanning, W.WorkCenterID"
					+ " FROM PRO_ProductionLine P JOIN MST_Department D"
					+ " ON P.DepartmentID = D.DepartmentID"
					+ " JOIN MST_Location L ON P.LocationID = L.LocationID"
					+ " JOIN MST_WorkCenter W ON P.ProductionLineID = W.ProductionLineID"
					+ " WHERE IsMain = 1";
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();

				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS, PRO_ProductionLineTable.TABLE_NAME);

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
		/// GetProductionLineByID
		/// </summary>
		/// <param name="pintProductionLineID"></param>
		/// <returns></returns>
		/// <author>Trada</author>
		/// <date>Wednesday, Mar 22 2006</date>
		public DataTable GetProductionLineByID(int pintProductionLineID)
		{
			const string METHOD_NAME = THIS + ".GetProductionLineByID()";
			DataSet dstPCS = new DataSet();


			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;

				strSql = "SELECT "
					+ PRO_ProductionLineTable.PRODUCTIONLINEID_FLD + ","
					+ PRO_ProductionLineTable.CODE_FLD + ","
					+ PRO_ProductionLineTable.NAME_FLD + ","
					+ PRO_ProductionLineTable.DEPARTMENTID_FLD
					+ " FROM " + PRO_ProductionLineTable.TABLE_NAME
					+ " WHERE " + PRO_ProductionLineTable.PRODUCTIONLINEID_FLD + " = " + pintProductionLineID.ToString();
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();

				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS, PRO_ProductionLineTable.TABLE_NAME);

				return dstPCS.Tables[0];
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
		/// List by CCN
		/// </summary>
		/// <param name="pintCCNID">CCN</param>
		/// <returns>DataTable</returns>
		public DataTable List(int pintCCNID)
		{
			const string METHOD_NAME = THIS + ".List()";
			DataTable dtbData = new DataTable(PRO_ProductionLineTable.TABLE_NAME);
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;

				strSql = "SELECT ProductionLineID FROM"
					+ " PRO_ProductionLine JOIN MST_Department"
					+ " ON PRO_ProductionLine.DepartmentID = MST_Department.DepartmentID"
					+ " WHERE MST_Department.CCNID = " + pintCCNID;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();

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


		//**************************************************************************              
		///    <Description>
		///       This method uses to update a DataSet
		///    </Description>
		///    <Inputs>
		///        DataSet       
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
		///       Wednesday, November 09, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
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
					+ PRO_ProductionLineTable.PRODUCTIONLINEID_FLD + ","
					+ PRO_ProductionLineTable.CODE_FLD + ","
					+ PRO_ProductionLineTable.NAME_FLD + ","
					+ PRO_ProductionLineTable.DEPARTMENTID_FLD
					+ "  FROM " + PRO_ProductionLineTable.TABLE_NAME;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				odadPCS.SelectCommand = new OleDbCommand(strSql, oconPCS);
				odcbPCS = new OleDbCommandBuilder(odadPCS);
				pData.EnforceConstraints = false;
				odadPCS.Update(pData, PRO_ProductionLineTable.TABLE_NAME);

			}
			catch (OleDbException ex)
			{
				if (ex.Errors[1].NativeError == ErrorCode.SQLDUPLICATE_KEYCODE)
				{
					throw new PCSDBException(ErrorCode.DUPLICATE_KEY, METHOD_NAME, ex);
				}
				else if (ex.Errors[1].NativeError == ErrorCode.SQLCASCADE_PREVENT_KEYCODE)
				{
					throw new PCSDBException(ErrorCode.CASCADE_DELETE_PREVENT, METHOD_NAME, ex);
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
	}
}