using System;
using System.Data;
using System.Data.OleDb;
using System.Collections;
using System.Configuration;
using PCSComUtils.DataAccess;

using PCSComUtils.PCSExc;
using PCSComUtils.Common;


namespace PCSComSale.Order.DS
{
	
	public class SO_CommitInventoryMasterDS 
	{
		public SO_CommitInventoryMasterDS()
		{
		}

		private const string THIS = "PCSComSale.Order.DS.SO_CommitInventoryMasterDS";
		//**************************************************************************              
		///    <Description>
		///       This method uses to add data to SO_CommitInventoryMaster
		///    </Description>
		///    <Inputs>
		///        SO_CommitInventoryMasterVO       
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
		///       Wednesday, February 23, 2005
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
				SO_CommitInventoryMasterVO objObject = (SO_CommitInventoryMasterVO) pobjObjectVO;
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand("", oconPCS);

				strSql = "INSERT INTO SO_CommitInventoryMaster("
					+ SO_CommitInventoryMasterTable.COMMITMENTNO_FLD + ","
					+ SO_CommitInventoryMasterTable.COMMITDATE_FLD + ","
					+ SO_CommitInventoryMasterTable.SALEORDERMASTERID_FLD + ","
					+ SO_CommitInventoryMasterTable.EMPLOYEEID_FLD + ")"
					+ "VALUES(?,?,?,?)";

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_CommitInventoryMasterTable.COMMITMENTNO_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[SO_CommitInventoryMasterTable.COMMITMENTNO_FLD].Value = objObject.CommitmentNo;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_CommitInventoryMasterTable.COMMITDATE_FLD, OleDbType.Date));
				ocmdPCS.Parameters[SO_CommitInventoryMasterTable.COMMITDATE_FLD].Value = objObject.CommitDate;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_CommitInventoryMasterTable.SALEORDERMASTERID_FLD, OleDbType.Integer));
				if (objObject.SaleOrderMasterID > 0)
					ocmdPCS.Parameters[SO_CommitInventoryMasterTable.SALEORDERMASTERID_FLD].Value = objObject.SaleOrderMasterID;
				else
					ocmdPCS.Parameters[SO_CommitInventoryMasterTable.SALEORDERMASTERID_FLD].Value = DBNull.Value;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_CommitInventoryMasterTable.EMPLOYEEID_FLD, OleDbType.Integer));
				if (objObject.EmployeeID > 0)
					ocmdPCS.Parameters[SO_CommitInventoryMasterTable.EMPLOYEEID_FLD].Value = objObject.EmployeeID;
				else
					ocmdPCS.Parameters[SO_CommitInventoryMasterTable.EMPLOYEEID_FLD].Value = DBNull.Value;


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
		///       This method uses to add data to SO_CommitInventoryMaster and return newly ID
		///    </Description>
		///    <Inputs>
		///        SO_CommitInventoryMasterVO       
		///    </Inputs>
		///    <Outputs>
		///       newly inserted primarkey value
		///    </Outputs>
		///    <Returns>
		///       int
		///    </Returns>
		///    <Authors>
		///       DungLa
		///    </Authors>
		///    <History>
		///       01-Mar-2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public int AddAndReturnID(object pobjObjectVO)
		{
			const string METHOD_NAME = THIS + ".AddAndReturnID()";

			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				SO_CommitInventoryMasterVO objObject = (SO_CommitInventoryMasterVO) pobjObjectVO;
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand("", oconPCS);

				strSql = "INSERT INTO SO_CommitInventoryMaster("
					+ SO_CommitInventoryMasterTable.COMMITMENTNO_FLD + ","
					+ SO_CommitInventoryMasterTable.COMMITDATE_FLD + ","
					+ SO_CommitInventoryMasterTable.SALEORDERMASTERID_FLD + ","
					+ SO_CommitInventoryMasterTable.USERNAME_FLD + ","
					+ SO_CommitInventoryMasterTable.EMPLOYEEID_FLD + ")"
					+ "VALUES(?,?,?,?,?)";

				strSql += " ; Select @@IDENTITY as NEWID";

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_CommitInventoryMasterTable.COMMITMENTNO_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[SO_CommitInventoryMasterTable.COMMITMENTNO_FLD].Value = objObject.CommitmentNo;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_CommitInventoryMasterTable.COMMITDATE_FLD, OleDbType.Date));
				ocmdPCS.Parameters[SO_CommitInventoryMasterTable.COMMITDATE_FLD].Value = objObject.CommitDate;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_CommitInventoryMasterTable.SALEORDERMASTERID_FLD, OleDbType.Integer));
				if (objObject.SaleOrderMasterID > 0)
					ocmdPCS.Parameters[SO_CommitInventoryMasterTable.SALEORDERMASTERID_FLD].Value = objObject.SaleOrderMasterID;
				else
					ocmdPCS.Parameters[SO_CommitInventoryMasterTable.SALEORDERMASTERID_FLD].Value = DBNull.Value;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_CommitInventoryMasterTable.USERNAME_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[SO_CommitInventoryMasterTable.USERNAME_FLD].Value = objObject.Username;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_CommitInventoryMasterTable.EMPLOYEEID_FLD, OleDbType.Integer));
				if (objObject.EmployeeID > 0)
					ocmdPCS.Parameters[SO_CommitInventoryMasterTable.EMPLOYEEID_FLD].Value = objObject.EmployeeID;
				else
					ocmdPCS.Parameters[SO_CommitInventoryMasterTable.EMPLOYEEID_FLD].Value = DBNull.Value;


				ocmdPCS.CommandText = strSql;
				ocmdPCS.Connection.Open();
				return int.Parse(ocmdPCS.ExecuteScalar().ToString());
				//ocmdPCS.ExecuteNonQuery();
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
		///       This method uses to delete data from SO_CommitInventoryMaster
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
			strSql = "DELETE " + SO_CommitInventoryMasterTable.TABLE_NAME + " WHERE  " + "CommitInventoryMasterID" + "=" + pintID.ToString();
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
		///       This method uses to get data from SO_CommitInventoryMaster
		///    </Description>
		///    <Inputs>
		///        ID       
		///    </Inputs>
		///    <Outputs>
		///       SO_CommitInventoryMasterVO
		///    </Outputs>
		///    <Returns>
		///       SO_CommitInventoryMasterVO
		///    </Returns>
		///    <Authors>
		///       HungLa
		///    </Authors>
		///    <History>
		///       Wednesday, February 23, 2005
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
					+ SO_CommitInventoryMasterTable.COMMITINVENTORYMASTERID_FLD + ","
					+ SO_CommitInventoryMasterTable.COMMITMENTNO_FLD + ","
					+ SO_CommitInventoryMasterTable.COMMITDATE_FLD + ","
					+ SO_CommitInventoryMasterTable.SALEORDERMASTERID_FLD + ","
					+ SO_CommitInventoryMasterTable.EMPLOYEEID_FLD
					+ " FROM " + SO_CommitInventoryMasterTable.TABLE_NAME
					+ " WHERE " + SO_CommitInventoryMasterTable.COMMITINVENTORYMASTERID_FLD + "=" + pintID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();

				SO_CommitInventoryMasterVO objObject = new SO_CommitInventoryMasterVO();

				while (odrPCS.Read())
				{
					if (odrPCS[SO_CommitInventoryMasterTable.COMMITINVENTORYMASTERID_FLD] != DBNull.Value)
						objObject.CommitInventoryMasterID = int.Parse(odrPCS[SO_CommitInventoryMasterTable.COMMITINVENTORYMASTERID_FLD].ToString().Trim());
					objObject.CommitmentNo = odrPCS[SO_CommitInventoryMasterTable.COMMITMENTNO_FLD].ToString().Trim();
					if (odrPCS[SO_CommitInventoryMasterTable.COMMITDATE_FLD] != DBNull.Value)
						objObject.CommitDate = DateTime.Parse(odrPCS[SO_CommitInventoryMasterTable.COMMITDATE_FLD].ToString().Trim());
					if (odrPCS[SO_CommitInventoryMasterTable.SALEORDERMASTERID_FLD] != DBNull.Value)
						objObject.SaleOrderMasterID = int.Parse(odrPCS[SO_CommitInventoryMasterTable.SALEORDERMASTERID_FLD].ToString().Trim());
					if (odrPCS[SO_CommitInventoryMasterTable.EMPLOYEEID_FLD] != DBNull.Value)
						objObject.EmployeeID = int.Parse(odrPCS[SO_CommitInventoryMasterTable.EMPLOYEEID_FLD].ToString().Trim());
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
		///       This method uses to update data to SO_CommitInventoryMaster
		///    </Description>
		///    <Inputs>
		///       SO_CommitInventoryMasterVO       
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

			SO_CommitInventoryMasterVO objObject = (SO_CommitInventoryMasterVO) pobjObjecVO;


			//prepare value for parameters
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				strSql = "UPDATE SO_CommitInventoryMaster SET "
					+ SO_CommitInventoryMasterTable.COMMITMENTNO_FLD + "=   ?" + ","
					+ SO_CommitInventoryMasterTable.COMMITDATE_FLD + "=   ?" + ","
					+ SO_CommitInventoryMasterTable.SALEORDERMASTERID_FLD + "=   ?";
				if (objObject.EmployeeID > 0)
					strSql += ", " + SO_CommitInventoryMasterTable.EMPLOYEEID_FLD + "=  ?";
				strSql += " WHERE " + SO_CommitInventoryMasterTable.COMMITINVENTORYMASTERID_FLD + "= ?";

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_CommitInventoryMasterTable.COMMITMENTNO_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[SO_CommitInventoryMasterTable.COMMITMENTNO_FLD].Value = objObject.CommitmentNo;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_CommitInventoryMasterTable.COMMITDATE_FLD, OleDbType.Date));
				ocmdPCS.Parameters[SO_CommitInventoryMasterTable.COMMITDATE_FLD].Value = objObject.CommitDate;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_CommitInventoryMasterTable.SALEORDERMASTERID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[SO_CommitInventoryMasterTable.SALEORDERMASTERID_FLD].Value = objObject.SaleOrderMasterID;

				if (objObject.EmployeeID > 0)
				{
					ocmdPCS.Parameters.Add(new OleDbParameter(SO_CommitInventoryMasterTable.EMPLOYEEID_FLD, OleDbType.Integer));
					ocmdPCS.Parameters[SO_CommitInventoryMasterTable.EMPLOYEEID_FLD].Value = objObject.EmployeeID;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_CommitInventoryMasterTable.COMMITINVENTORYMASTERID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[SO_CommitInventoryMasterTable.COMMITINVENTORYMASTERID_FLD].Value = objObject.CommitInventoryMasterID;


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
		///       This method uses to get all data from SO_CommitInventoryMaster
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
		///       Wednesday, February 23, 2005
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
					+ SO_CommitInventoryMasterTable.COMMITINVENTORYMASTERID_FLD + ","
					+ SO_CommitInventoryMasterTable.COMMITMENTNO_FLD + ","
					+ SO_CommitInventoryMasterTable.COMMITDATE_FLD + ","
					+ SO_CommitInventoryMasterTable.SALEORDERMASTERID_FLD + ","
					+ SO_CommitInventoryMasterTable.EMPLOYEEID_FLD
					+ " FROM " + SO_CommitInventoryMasterTable.TABLE_NAME;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();

				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS, SO_CommitInventoryMasterTable.TABLE_NAME);

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
		/// ListCommittedInventoryMasterBySaleOrderMasterID
		/// </summary>
		/// <param name="pintSaleOrderMasterID"></param>
		/// <returns></returns>
		/// <author>Trada</author>
		/// <date>Thursday, July 7 2005</date>
		public DataSet ListCommittedInventoryMasterBySaleOrderMasterID(int pintSaleOrderMasterID)
		{
			const string METHOD_NAME = THIS + ".List()";
			DataSet dstPCS = new DataSet();


			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = "SELECT "
					+ SO_CommitInventoryMasterTable.COMMITINVENTORYMASTERID_FLD + ","
					+ SO_CommitInventoryMasterTable.COMMITMENTNO_FLD + ","
					+ SO_CommitInventoryMasterTable.COMMITDATE_FLD + ","
					+ SO_CommitInventoryMasterTable.SALEORDERMASTERID_FLD + ","
					+ SO_CommitInventoryMasterTable.EMPLOYEEID_FLD
					+ " FROM " + SO_CommitInventoryMasterTable.TABLE_NAME
					+ " WHERE " + SO_CommitInventoryMasterTable.SALEORDERMASTERID_FLD + " = " + pintSaleOrderMasterID;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();

				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS, SO_CommitInventoryMasterTable.TABLE_NAME);

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
		///       Wednesday, February 23, 2005
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
					+ SO_CommitInventoryMasterTable.COMMITINVENTORYMASTERID_FLD + ","
					+ SO_CommitInventoryMasterTable.COMMITMENTNO_FLD + ","
					+ SO_CommitInventoryMasterTable.COMMITDATE_FLD + ","
					+ SO_CommitInventoryMasterTable.SALEORDERMASTERID_FLD + ","
					+ SO_CommitInventoryMasterTable.EMPLOYEEID_FLD
					+ "  FROM " + SO_CommitInventoryMasterTable.TABLE_NAME;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				odadPCS.SelectCommand = new OleDbCommand(strSql, oconPCS);
				odcbPCS = new OleDbCommandBuilder(odadPCS);
				pData.EnforceConstraints = false;
				odadPCS.Update(pData, SO_CommitInventoryMasterTable.TABLE_NAME);

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
		///			Auto delete all data in the SO_COmmitInventoryMaster if has no item in SO_CommitInventoryDetail
		///    </Description>
		///    <Inputs>
		///    </Inputs>
		///    <Outputs>
		///    </Outputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <Authors>
		///       TuanDm
		///    </Authors>
		///    <History>
		///       Monday, Mar 07, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public void AutoDeleteCommitMaster()
		{
			const string METHOD_NAME = THIS + ".UpdateDataSet()";
			string strSql;
			OleDbConnection oconPCS = null;
			try
			{
				strSql =	" Delete " +
							" From " + SO_CommitInventoryMasterTable.TABLE_NAME +
							" Where (Select count (*)" +
							" From " + SO_CommitInventoryDetailTable.TABLE_NAME +
							" Where "+ SO_CommitInventoryDetailTable.TABLE_NAME + "." + SO_CommitInventoryDetailTable.COMMITINVENTORYMASTERID_FLD + " = " + SO_CommitInventoryMasterTable.TABLE_NAME + "." + SO_CommitInventoryMasterTable.COMMITINVENTORYMASTERID_FLD + ") = 0";

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				OleDbCommand ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				ocmdPCS.ExecuteNonQuery();
				ocmdPCS = null;
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
