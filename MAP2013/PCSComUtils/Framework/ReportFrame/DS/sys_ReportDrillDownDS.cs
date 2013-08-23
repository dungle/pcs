using System;
using System.Collections;
using System.Data;
using System.Data.OleDb;
using PCSComUtils.Common;
using PCSComUtils.DataAccess;

using PCSComUtils.PCSExc;

namespace PCSComUtils.Framework.ReportFrame.DS
{
	public class sys_ReportDrillDownDS 
	{
		public sys_ReportDrillDownDS()
		{
		}

		private const string THIS = "PCSComUtils.Framework.ReportFrame.DS.DS.sys_ReportDrillDownDS";

		//**************************************************************************              
		///    <Description>
		///       This method uses to add data to sys_ReportDrillDown
		///    </Description>
		///    <Inputs>
		///        sys_ReportDrillDownVO       
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
		///       Monday, December 27, 2004
		///       Modified: DungLA - 07/Jan/2005
		///       12/Oct/2005 Thachnn: fix bug injection
		///    </History>
		///    <Notes>
		///		  Change strSql, insert data to one more field - MasterReportID
		///    </Notes>
		//**************************************************************************
		public void Add(object pobjObjectVO)
		{
			const string METHOD_NAME = THIS + ".Add()";

			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				sys_ReportDrillDownVO objObject = (sys_ReportDrillDownVO) pobjObjectVO;
				string strSql = String.Empty;

				strSql = "INSERT INTO " + sys_ReportDrillDownTable.TABLE_NAME + "("
					+ sys_ReportDrillDownTable.MASTERREPORTID_FLD + ","
					+ sys_ReportDrillDownTable.DETAILREPORTID_FLD + ","
					+ sys_ReportDrillDownTable.MASTERPARA_FLD + ","
					+ sys_ReportDrillDownTable.DETAILPARA_FLD + ","
					+ sys_ReportDrillDownTable.FROMCOLUMN_FLD + ","
					+ sys_ReportDrillDownTable.PARAORDER_FLD + ")"
					+ " VALUES (?,?,?,?,?,?)";

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportDrillDownTable.MASTERREPORTID_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[sys_ReportDrillDownTable.MASTERREPORTID_FLD].Value = objObject.MasterReportID;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportDrillDownTable.DETAILREPORTID_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[sys_ReportDrillDownTable.DETAILREPORTID_FLD].Value = objObject.DetailReportID;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportDrillDownTable.MASTERPARA_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[sys_ReportDrillDownTable.MASTERPARA_FLD].Value = objObject.MasterPara;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportDrillDownTable.DETAILPARA_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[sys_ReportDrillDownTable.DETAILPARA_FLD].Value = objObject.DetailPara;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportDrillDownTable.FROMCOLUMN_FLD, OleDbType.Boolean));
				ocmdPCS.Parameters[sys_ReportDrillDownTable.FROMCOLUMN_FLD].Value = objObject.FromColumn;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportDrillDownTable.PARAORDER_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[sys_ReportDrillDownTable.PARAORDER_FLD].Value = objObject.ParaOrder;


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
		///       This method uses to delete data from sys_ReportDrillDown
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
			strSql = "DELETE " + sys_ReportDrillDownTable.TABLE_NAME + " WHERE  " + sys_ReportDrillDownTable.MASTERREPORTID_FLD + "=" + pintID.ToString().Trim();
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
		///       This method uses to delete data from sys_ReportDrillDown
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
		///       12/Oct/2005 Thachnn: fix bug injection
		///    </Authors>
		///    <History>
		///       09-Dec-2004
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public void Delete(string pstrID)
		{
			const string METHOD_NAME = THIS + ".Delete()";
			string strSql = String.Empty;
			strSql = "DELETE " + sys_ReportDrillDownTable.TABLE_NAME
				+ " WHERE  " + sys_ReportDrillDownTable.MASTERREPORTID_FLD + " = ? ";// + pstrID + "'";
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);

				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportDrillDownTable.MASTERREPORTID_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[sys_ReportDrillDownTable.MASTERREPORTID_FLD].Value = pstrID;
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
		///       This method uses to delete data from sys_ReportDrillDown
		///    </Description>
		///    <Inputs>
		///        MasterReportID, DetailReportID    
		///    </Inputs>
		///    <Outputs>
		///       number of row(s) affected
		///    </Outputs>
		///    <Returns>
		///       int
		///    </Returns>
		///    <Authors>
		///       DungLA
		///    </Authors>
		///    <History>
		///       07-Jan-2005
		///       12/Oct/2005 Thachnn: fix bug injection
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public int Delete(string pstrMasterID, string pstrDetailID)
		{
			const string METHOD_NAME = THIS + ".Delete()";
			string strSql = String.Empty;
			strSql = "DELETE " + sys_ReportDrillDownTable.TABLE_NAME 
				+ " WHERE  " + sys_ReportDrillDownTable.MASTERREPORTID_FLD + "= ? " // + pstrMasterID + "'"
				+ " AND " + sys_ReportDrillDownTable.DETAILREPORTID_FLD + "= ? "; // + pstrDetailID + "'";
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);

				ocmdPCS = new OleDbCommand(strSql, oconPCS);
                ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportDrillDownTable.MASTERREPORTID_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[sys_ReportDrillDownTable.MASTERREPORTID_FLD].Value = pstrMasterID;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportDrillDownTable.DETAILREPORTID_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[sys_ReportDrillDownTable.DETAILREPORTID_FLD].Value = pstrDetailID;
				ocmdPCS.Connection.Open();
				int nReturn = ocmdPCS.ExecuteNonQuery();
				ocmdPCS = null;
				return nReturn;
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
		///       This method uses to get data from sys_ReportDrillDown
		///    </Description>
		///    <Inputs>
		///        ID       
		///    </Inputs>
		///    <Outputs>
		///       sys_ReportDrillDownVO
		///    </Outputs>
		///    <Returns>
		///       sys_ReportDrillDownVO
		///    </Returns>
		///    <Authors>
		///       HungLa
		///    </Authors>
		///    <History>
		///       Monday, December 27, 2004
		///       12/Oct/2005 Thachnn: fix bug injection
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public object GetObjectVO(int pintID)
		{
			const string METHOD_NAME = THIS + ".GetObjectVO()";

			OleDbDataReader odrPCS = null;
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;
				strSql = "SELECT "
					+ sys_ReportDrillDownTable.MASTERREPORTID_FLD + ","
					+ sys_ReportDrillDownTable.DETAILREPORTID_FLD + ","
					+ sys_ReportDrillDownTable.MASTERPARA_FLD + ","
					+ sys_ReportDrillDownTable.DETAILPARA_FLD + ","
					+ sys_ReportDrillDownTable.FROMCOLUMN_FLD + ","
					+ sys_ReportDrillDownTable.PARAORDER_FLD
					+ " FROM " + sys_ReportDrillDownTable.TABLE_NAME
					+ " WHERE " + sys_ReportDrillDownTable.MASTERREPORTID_FLD + "=" + pintID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();

				sys_ReportDrillDownVO objObject = new sys_ReportDrillDownVO();

				while (odrPCS.Read())
				{
					objObject.MasterReportID = odrPCS[sys_ReportDrillDownTable.MASTERREPORTID_FLD].ToString().Trim();
					objObject.DetailReportID = odrPCS[sys_ReportDrillDownTable.DETAILREPORTID_FLD].ToString().Trim();
					objObject.MasterPara = odrPCS[sys_ReportDrillDownTable.MASTERPARA_FLD].ToString().Trim();
					objObject.DetailPara = odrPCS[sys_ReportDrillDownTable.DETAILPARA_FLD].ToString().Trim();
					objObject.FromColumn = Boolean.Parse(odrPCS[sys_ReportDrillDownTable.FROMCOLUMN_FLD].ToString().Trim());
					objObject.ParaOrder = int.Parse(odrPCS[sys_ReportDrillDownTable.PARAORDER_FLD].ToString().Trim());

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
		///       This method uses to get data from sys_ReportDrillDown
		///    </Description>
		///    <Inputs>
		///        Master Report ID
		///    </Inputs>
		///    <Outputs>
		///       List of sys_ReportDrillDownVO object
		///    </Outputs>
		///    <Returns>
		///       ArrayList
		///    </Returns>
		///    <Authors>
		///       DungLA
		///    </Authors>
		///    <History>
		///       03-Jan-2005
		///       12/Oct/2005 Thachnn: fix bug injection
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public ArrayList GetObjectVOs(string pstrMasterID)
		{
			const string METHOD_NAME = THIS + ".GetObjectVOs()";

			ArrayList arrObjectVOs = new ArrayList();

			OleDbDataReader odrPCS = null;
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;
				strSql = "SELECT "
					+ sys_ReportDrillDownTable.MASTERREPORTID_FLD + ","
					+ sys_ReportDrillDownTable.DETAILREPORTID_FLD + ","
					+ sys_ReportDrillDownTable.MASTERPARA_FLD + ","
					+ sys_ReportDrillDownTable.DETAILPARA_FLD + ","
					+ sys_ReportDrillDownTable.FROMCOLUMN_FLD + ","
					+ sys_ReportDrillDownTable.PARAORDER_FLD
					+ " FROM " + sys_ReportDrillDownTable.TABLE_NAME
					+ " WHERE " + sys_ReportDrillDownTable.MASTERREPORTID_FLD + "= ? ";// + pstrMasterID + "'";

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);

				ocmdPCS = new OleDbCommand(strSql, oconPCS);
                ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportDrillDownTable.MASTERREPORTID_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[sys_ReportDrillDownTable.MASTERREPORTID_FLD].Value = pstrMasterID;
				ocmdPCS.Connection.Open();

				odrPCS = ocmdPCS.ExecuteReader();

				while (odrPCS.Read())
				{
					sys_ReportDrillDownVO voDrillDown = new sys_ReportDrillDownVO();

					voDrillDown.MasterReportID = odrPCS[sys_ReportDrillDownTable.MASTERREPORTID_FLD].ToString().Trim();
					voDrillDown.DetailReportID = odrPCS[sys_ReportDrillDownTable.DETAILREPORTID_FLD].ToString().Trim();
					voDrillDown.MasterPara = odrPCS[sys_ReportDrillDownTable.MASTERPARA_FLD].ToString().Trim();
					voDrillDown.DetailPara = odrPCS[sys_ReportDrillDownTable.DETAILPARA_FLD].ToString().Trim();
					voDrillDown.FromColumn = Boolean.Parse(odrPCS[sys_ReportDrillDownTable.FROMCOLUMN_FLD].ToString().Trim());
					voDrillDown.ParaOrder = int.Parse(odrPCS[sys_ReportDrillDownTable.PARAORDER_FLD].ToString().Trim());

					arrObjectVOs.Add(voDrillDown);
				}
				arrObjectVOs.TrimToSize();
				return arrObjectVOs;
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
		///       This method uses to update data to sys_ReportDrillDown
		///    </Description>
		///    <Inputs>
		///       sys_ReportDrillDownVO       
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
		///       12/Oct/2005 Thachnn: fix bug injection
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public void Update(object pobjObjecVO)
		{
			const string METHOD_NAME = THIS + ".Update()";

			sys_ReportDrillDownVO objObject = (sys_ReportDrillDownVO) pobjObjecVO;

			//prepare value for parameters
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;

				strSql = "UPDATE " + sys_ReportDrillDownTable.TABLE_NAME + " SET "
					+ sys_ReportDrillDownTable.DETAILREPORTID_FLD + "=   ?" + ","
					+ sys_ReportDrillDownTable.MASTERPARA_FLD + "=   ?" + ","
					+ sys_ReportDrillDownTable.DETAILPARA_FLD + "=   ?" + ","
					+ sys_ReportDrillDownTable.FROMCOLUMN_FLD + "=   ?" + ","
					+ sys_ReportDrillDownTable.PARAORDER_FLD + "=  ?"
					+ " WHERE " + sys_ReportDrillDownTable.MASTERREPORTID_FLD + "= ?";

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportDrillDownTable.DETAILREPORTID_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[sys_ReportDrillDownTable.DETAILREPORTID_FLD].Value = objObject.DetailReportID;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportDrillDownTable.MASTERPARA_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[sys_ReportDrillDownTable.MASTERPARA_FLD].Value = objObject.MasterPara;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportDrillDownTable.DETAILPARA_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[sys_ReportDrillDownTable.DETAILPARA_FLD].Value = objObject.DetailPara;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportDrillDownTable.FROMCOLUMN_FLD, OleDbType.Boolean));
				ocmdPCS.Parameters[sys_ReportDrillDownTable.FROMCOLUMN_FLD].Value = objObject.FromColumn;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportDrillDownTable.PARAORDER_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[sys_ReportDrillDownTable.PARAORDER_FLD].Value = objObject.ParaOrder;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportDrillDownTable.MASTERREPORTID_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[sys_ReportDrillDownTable.MASTERREPORTID_FLD].Value = objObject.MasterReportID;

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
		///       This method uses to get all data from sys_ReportDrillDown
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
		///       Monday, December 27, 2004
		///       12/Oct/2005 Thachnn: fix bug injection
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
				string strSql = String.Empty;

				strSql = "SELECT "
					+ sys_ReportDrillDownTable.MASTERREPORTID_FLD + ","
					+ sys_ReportDrillDownTable.DETAILREPORTID_FLD + ","
					+ sys_ReportDrillDownTable.MASTERPARA_FLD + ","
					+ sys_ReportDrillDownTable.DETAILPARA_FLD + ","
					+ sys_ReportDrillDownTable.FROMCOLUMN_FLD + ","
					+ sys_ReportDrillDownTable.PARAORDER_FLD
					+ " FROM " + sys_ReportDrillDownTable.TABLE_NAME;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();

				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS, sys_ReportDrillDownTable.TABLE_NAME);

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
		///       Monday, December 27, 2004
		///       
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
					+ sys_ReportDrillDownTable.MASTERREPORTID_FLD + ","
					+ sys_ReportDrillDownTable.DETAILREPORTID_FLD + ","
					+ sys_ReportDrillDownTable.MASTERPARA_FLD + ","
					+ sys_ReportDrillDownTable.DETAILPARA_FLD + ","
					+ sys_ReportDrillDownTable.FROMCOLUMN_FLD + ","
					+ sys_ReportDrillDownTable.PARAORDER_FLD
					+ " FROM " + sys_ReportDrillDownTable.TABLE_NAME;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				odadPCS.SelectCommand = new OleDbCommand(strSql, oconPCS);
				odcbPCS = new OleDbCommandBuilder(odadPCS);
				pData.EnforceConstraints = false;
				odadPCS.Update(pData, sys_ReportDrillDownTable.TABLE_NAME);

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

	
		//**************************************************************************              
		///    <Description>
		///       This method uses to get data from sys_ReportDrillDown
		///    </Description>
		///    <Inputs>
		///        MasterID, DetailID
		///    </Inputs>
		///    <Outputs>
		///       DataSet
		///    </Outputs>
		///    <Returns>
		///       DataSet
		///    </Returns>
		///    <Authors>
		///       DungLA
		///    </Authors>
		///    <History>
		///       06-Jan-2005
		///       12/Oct/2005 Thachnn: fix bug injection
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public DataSet GetObjectVO(string pstrMasterID, string pstrDetailID)
		{
			const string METHOD_NAME = THIS + ".GetObjectVO()";

			DataSet dstPCS = new DataSet();

			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;
				strSql = "SELECT "
					+ sys_ReportDrillDownTable.MASTERREPORTID_FLD + ","
					+ sys_ReportDrillDownTable.DETAILREPORTID_FLD + ","
					+ sys_ReportDrillDownTable.MASTERPARA_FLD + ","
					+ sys_ReportDrillDownTable.DETAILPARA_FLD + ","
					+ sys_ReportDrillDownTable.FROMCOLUMN_FLD + ","
					+ sys_ReportDrillDownTable.PARAORDER_FLD
					+ " FROM " + sys_ReportDrillDownTable.TABLE_NAME
					+ " WHERE " + sys_ReportDrillDownTable.MASTERREPORTID_FLD + "= ? "// + pstrMasterID + "'"
					+ " AND " + sys_ReportDrillDownTable.DETAILREPORTID_FLD + "= ? ";// + pstrDetailID + "'";

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);

				ocmdPCS = new OleDbCommand(strSql, oconPCS);
                ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportDrillDownTable.MASTERREPORTID_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[sys_ReportDrillDownTable.MASTERREPORTID_FLD].Value = pstrMasterID;
				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportDrillDownTable.DETAILREPORTID_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[sys_ReportDrillDownTable.DETAILREPORTID_FLD].Value = pstrDetailID;
				ocmdPCS.Connection.Open();
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS, sys_ReportDrillDownTable.TABLE_NAME);
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
	
	
		//**************************************************************************              
		///    <Description>
		///       This method uses to get data from sys_ReportDrillDown
		///    </Description>
		///    <Inputs>
		///        MasterID, DetailID
		///    </Inputs>
		///    <Outputs>
		///       ArrayList
		///    </Outputs>
		///    <Returns>
		///       ArrayList
		///    </Returns>
		///    <Authors>
		///       DungLA
		///    </Authors>
		///    <History>
		///       06-Jan-2005
		///       12/Oct/2005 Thachnn: fix bug injection
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public ArrayList GetObjectVOs(string pstrMasterID, string pstrDetailID)
		{
			const string METHOD_NAME = THIS + ".GetObjectVO()";

			ArrayList arrObjectVOs = new ArrayList();
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			OleDbDataReader odrPCS = null;
			try
			{
				string strSql = String.Empty;
				strSql = "SELECT "
					+ sys_ReportDrillDownTable.MASTERREPORTID_FLD + ","
					+ sys_ReportDrillDownTable.DETAILREPORTID_FLD + ","
					+ sys_ReportDrillDownTable.MASTERPARA_FLD + ","
					+ sys_ReportDrillDownTable.DETAILPARA_FLD + ","
					+ sys_ReportDrillDownTable.FROMCOLUMN_FLD + ","
					+ sys_ReportDrillDownTable.PARAORDER_FLD
					+ " FROM " + sys_ReportDrillDownTable.TABLE_NAME
					+ " WHERE " + sys_ReportDrillDownTable.MASTERREPORTID_FLD + "= ? " // + pstrMasterID + "'"
					+ " AND " + sys_ReportDrillDownTable.DETAILREPORTID_FLD + "= ? " ; //+ pstrDetailID + "'";

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
                ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportDrillDownTable.MASTERREPORTID_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[sys_ReportDrillDownTable.MASTERREPORTID_FLD].Value = pstrMasterID;
				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportDrillDownTable.DETAILREPORTID_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[sys_ReportDrillDownTable.DETAILREPORTID_FLD].Value = pstrDetailID;
				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();

				while (odrPCS.Read())
				{
					sys_ReportDrillDownVO voDrillDown = new sys_ReportDrillDownVO();

					voDrillDown.MasterReportID = odrPCS[sys_ReportDrillDownTable.MASTERREPORTID_FLD].ToString().Trim();
					voDrillDown.DetailReportID = odrPCS[sys_ReportDrillDownTable.DETAILREPORTID_FLD].ToString().Trim();
					voDrillDown.MasterPara = odrPCS[sys_ReportDrillDownTable.MASTERPARA_FLD].ToString().Trim();
					voDrillDown.DetailPara = odrPCS[sys_ReportDrillDownTable.DETAILPARA_FLD].ToString().Trim();
					voDrillDown.FromColumn = Boolean.Parse(odrPCS[sys_ReportDrillDownTable.FROMCOLUMN_FLD].ToString().Trim());
					voDrillDown.ParaOrder = int.Parse(odrPCS[sys_ReportDrillDownTable.PARAORDER_FLD].ToString().Trim());

					arrObjectVOs.Add(voDrillDown);
				}
				arrObjectVOs.TrimToSize();
				return arrObjectVOs;
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
		///       This method uses to get data for C1TrueDBGrid
		///    </Description>
		///    <Inputs>
		///        MasterID, DetailID
		///    </Inputs>
		///    <Outputs>
		///       DataTable
		///    </Outputs>
		///    <Returns>
		///       DataTable
		///    </Returns>
		///    <Authors>
		///       DungLA
		///    </Authors>
		///    <History>
		///       06-Jan-2005
		///       12/Oct/2005 Thachnn: fix bug injection
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public DataTable GetDataForTrueDBGrid(string pstrMasterID, string pstrDetailID, out bool oblnIsEdit)
		{
			const string METHOD_NAME = THIS + ".GetDataForTrueDBGrid()";

			string strSql = string.Empty;
			// create new table with 4 columns to store data and return to caller
			DataTable dtblSource = new DataTable(sys_ReportDrillDownTable.TABLE_NAME);
			dtblSource.Columns.Add(sys_ReportDrillDownTable.MASTERPARA_FLD);
			dtblSource.Columns.Add(sys_ReportDrillDownTable.DETAILPARA_FLD);
			dtblSource.Columns.Add(sys_ReportParaTable.DATATYPE_FLD);
			dtblSource.Columns.Add(sys_ReportDrillDownTable.FROMCOLUMN_FLD);
			dtblSource.Columns.Add(sys_ReportDrillDownTable.PARAORDER_FLD);
			// set data type for FromColumn column
			dtblSource.Columns[sys_ReportDrillDownTable.FROMCOLUMN_FLD].DataType = typeof (bool);
			// set default value for FromColumn
			dtblSource.Columns[sys_ReportDrillDownTable.FROMCOLUMN_FLD].DefaultValue = false;

			DataSet dstSource = new DataSet();
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;

			#region Existed record in sys_ReportDrillDown table

			// first we check for existing record in sys_ReportDrillDown table
			strSql = "SELECT " + sys_ReportDrillDownTable.MASTERREPORTID_FLD + ","
				+ sys_ReportDrillDownTable.DETAILREPORTID_FLD + ","
				+ sys_ReportDrillDownTable.MASTERPARA_FLD + ","
				+ sys_ReportDrillDownTable.DETAILPARA_FLD + ","
				+ sys_ReportDrillDownTable.FROMCOLUMN_FLD + ","
				+ sys_ReportDrillDownTable.PARAORDER_FLD
				+ " FROM " + sys_ReportDrillDownTable.TABLE_NAME
				+ " WHERE " + sys_ReportDrillDownTable.MASTERREPORTID_FLD + "= ? " //+ pstrMasterID + "'"
				+ " AND " + sys_ReportDrillDownTable.DETAILREPORTID_FLD + "= ? " ;// + pstrDetailID + "'";
			try
			{
				// if already existed, then get current data
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);

				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportDrillDownTable.MASTERREPORTID_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[sys_ReportDrillDownTable.MASTERREPORTID_FLD].Value = pstrMasterID;
				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportDrillDownTable.DETAILREPORTID_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[sys_ReportDrillDownTable.DETAILREPORTID_FLD].Value = pstrDetailID;
				ocmdPCS.Connection.Open();

				OleDbDataReader odrdPCS = ocmdPCS.ExecuteReader();
				if (odrdPCS.HasRows)
				{
					while (odrdPCS.Read())
					{
						DataRow drow = dtblSource.NewRow();
						drow[sys_ReportDrillDownTable.MASTERPARA_FLD] = odrdPCS[sys_ReportDrillDownTable.MASTERPARA_FLD];
						drow[sys_ReportDrillDownTable.DETAILPARA_FLD] = odrdPCS[sys_ReportDrillDownTable.DETAILPARA_FLD];
						drow[sys_ReportDrillDownTable.FROMCOLUMN_FLD] = odrdPCS[sys_ReportDrillDownTable.FROMCOLUMN_FLD];
						drow[sys_ReportDrillDownTable.PARAORDER_FLD] = odrdPCS[sys_ReportDrillDownTable.PARAORDER_FLD];
						dtblSource.Rows.Add(drow);
					}
					sys_ReportParaDS dsSysReportPara = new sys_ReportParaDS();
					foreach (DataRow drow in dtblSource.Rows)
					{
						drow[sys_ReportParaTable.DATATYPE_FLD] = dsSysReportPara.GetDataType(pstrDetailID, drow[sys_ReportDrillDownTable.DETAILPARA_FLD].ToString().Trim());
					}
					// return
					oblnIsEdit = true;
					return dtblSource;
				}
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

			#endregion

			oblnIsEdit = false;
			#region Not existed, make new data and return to user

			/// TODO: Bro.DungLA. Thachnn says: I feel bug here, when creating strSql, but I don't know why and how to fix.
			/// The old code is really fusion with continuing plus and plus SQL string.
			/// Please comeback to this point when there are any errors.			
			try
			{
				Utils utils = new Utils();
				oconPCS.ConnectionString = Utils.Instance.OleDbConnectionString;

				string strSqlOrder = " ORDER BY " + sys_ReportParaTable.PARAORDER_FLD + " ASC";

                
				string strSqlMaster = "SELECT "
					+ sys_ReportParaTable.PARAORDER_FLD + ","
					+ sys_ReportParaTable.PARANAME_FLD + ","
					+ sys_ReportParaTable.DATATYPE_FLD
					+ " FROM " + sys_ReportParaTable.TABLE_NAME
					+ " WHERE " + sys_ReportParaTable.REPORTID_FLD + "= ? " + strSqlOrder;

				OleDbCommand ocmdMaster = new OleDbCommand(strSqlMaster,oconPCS);
				ocmdMaster.Parameters.Add(new OleDbParameter(sys_ReportParaTable.REPORTID_FLD, OleDbType.VarWChar));
				ocmdMaster.Parameters[sys_ReportParaTable.REPORTID_FLD].Value = pstrMasterID;				
				// fill master para into dataset with new table 
				OleDbDataAdapter odadMaster = new OleDbDataAdapter(ocmdMaster);
				odadMaster.Fill(dstSource, sys_ReportDrillDownTable.MASTERPARA_FLD);


				string strSqlDetail = "SELECT "
					+ sys_ReportParaTable.PARAORDER_FLD + ","
					+ sys_ReportParaTable.PARANAME_FLD + ","
					+ sys_ReportParaTable.DATATYPE_FLD
					+ " FROM " + sys_ReportParaTable.TABLE_NAME
					+ " WHERE " + sys_ReportParaTable.REPORTID_FLD + "= ? " + strSqlOrder;

				OleDbCommand ocmdDetail = new OleDbCommand(strSqlDetail,oconPCS);
				ocmdDetail.Parameters.Add(new OleDbParameter(sys_ReportParaTable.REPORTID_FLD, OleDbType.VarWChar));
				ocmdDetail.Parameters[sys_ReportParaTable.REPORTID_FLD].Value = pstrDetailID;				

				// fill detail para into dataset with new table 
				OleDbDataAdapter odadDetail = new OleDbDataAdapter(ocmdDetail);
				odadDetail.Fill(dstSource, sys_ReportDrillDownTable.DETAILPARA_FLD);

				// get all rows
				DataRowCollection MasterRows = dstSource.Tables[sys_ReportDrillDownTable.MASTERPARA_FLD].Rows;
				DataRowCollection DetailRows = dstSource.Tables[sys_ReportDrillDownTable.DETAILPARA_FLD].Rows;
				
				// if number of master para is bigger than number of detail para
				// then the detail para is basic for filling parameter values
				if (MasterRows.Count > DetailRows.Count)
				{
					for (int i = 0; i < MasterRows.Count; i++)
					{
						DataRow drow = dtblSource.NewRow();
						drow[sys_ReportDrillDownTable.MASTERPARA_FLD] = MasterRows[i][sys_ReportParaTable.PARANAME_FLD];
						drow[sys_ReportDrillDownTable.PARAORDER_FLD] = i + 1;
						if (i < DetailRows.Count)
						{
							drow[sys_ReportDrillDownTable.DETAILPARA_FLD] = DetailRows[i][sys_ReportParaTable.PARANAME_FLD];
							drow[sys_ReportParaTable.DATATYPE_FLD] = DetailRows[i][sys_ReportParaTable.DATATYPE_FLD];
						}
						else
						{
							break;
						}
						dtblSource.Rows.Add(drow);
					}
				}
				// if number of detail para is bigger than number of master para
				// then the master para is basic for filling parameter values
				else
				{
					for (int i = 0; i < DetailRows.Count; i++)
					{
						DataRow drow = dtblSource.NewRow();
						drow[sys_ReportDrillDownTable.DETAILPARA_FLD] = DetailRows[i][sys_ReportParaTable.PARANAME_FLD];
						drow[sys_ReportDrillDownTable.PARAORDER_FLD] = i + 1;
						if (i < MasterRows.Count)
						{
							drow[sys_ReportDrillDownTable.MASTERPARA_FLD] = MasterRows[i][sys_ReportParaTable.PARANAME_FLD];
							drow[sys_ReportParaTable.DATATYPE_FLD] = MasterRows[i][sys_ReportParaTable.DATATYPE_FLD];
						}
						else
						{
							drow[sys_ReportDrillDownTable.MASTERPARA_FLD] = null;
							drow[sys_ReportParaTable.DATATYPE_FLD] = DetailRows[i][sys_ReportParaTable.DATATYPE_FLD];
						}
						dtblSource.Rows.Add(drow);
					}
				}
				oblnIsEdit = true;
				return dtblSource;
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

			#endregion
		}
	}
}