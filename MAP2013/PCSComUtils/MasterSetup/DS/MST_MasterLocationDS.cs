using System;
using System.Data;
using System.Data.OleDb;
using PCSComUtils.Common;
using PCSComUtils.DataAccess;
using PCSComUtils.PCSExc;

namespace PCSComUtils.MasterSetup.DS
{
	public class MST_MasterLocationDS 
	{
		public MST_MasterLocationDS()
		{
		}

		private const string THIS = "PCSComUtils.MasterSetup.DS.MST_MasterLocationDS";


		//**************************************************************************              
		///    <Description>
		///       This method uses to add data to MST_MasterLocation
		///    </Description>
		///    <Inputs>
		///        MST_MasterLocationVO       
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
		///       Tuesday, January 25, 2005
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
				MST_MasterLocationVO objObject = (MST_MasterLocationVO) pobjObjectVO;
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand("", oconPCS);

				strSql = "INSERT INTO MST_MasterLocation("
					+ MST_MasterLocationTable.CODE_FLD + ","
					+ MST_MasterLocationTable.NAME_FLD + ","
					+ MST_MasterLocationTable.ADDRESS_FLD + ","
					+ MST_MasterLocationTable.STATE_FLD + ","
					+ MST_MasterLocationTable.ZIPPOST_FLD + ","
					+ MST_MasterLocationTable.CCNID_FLD + ","
					+ MST_MasterLocationTable.CITYID_FLD + ","
					+ MST_MasterLocationTable.COUNTRYID_FLD + ")"
					+ "VALUES(?,?,?,?,?,?,?,?)";

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_MasterLocationTable.CODE_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[MST_MasterLocationTable.CODE_FLD].Value = objObject.Code;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_MasterLocationTable.NAME_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[MST_MasterLocationTable.NAME_FLD].Value = objObject.Name;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_MasterLocationTable.ADDRESS_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[MST_MasterLocationTable.ADDRESS_FLD].Value = objObject.Address;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_MasterLocationTable.STATE_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[MST_MasterLocationTable.STATE_FLD].Value = objObject.State;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_MasterLocationTable.ZIPPOST_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[MST_MasterLocationTable.ZIPPOST_FLD].Value = objObject.ZipPost;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_MasterLocationTable.CCNID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[MST_MasterLocationTable.CCNID_FLD].Value = objObject.CCNID;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_MasterLocationTable.CITYID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[MST_MasterLocationTable.CITYID_FLD].Value = objObject.CityID;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_MasterLocationTable.COUNTRYID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[MST_MasterLocationTable.COUNTRYID_FLD].Value = objObject.CountryID;


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
		///       This method uses to delete data from MST_MasterLocation
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
			strSql = "DELETE " + MST_MasterLocationTable.TABLE_NAME + " WHERE  " + "MasterLocationID" + "=" + pintID.ToString();
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
		///       This method uses to get data from MST_MasterLocation
		///    </Description>
		///    <Inputs>
		///        ID       
		///    </Inputs>
		///    <Outputs>
		///       MST_MasterLocationVO
		///    </Outputs>
		///    <Returns>
		///       MST_MasterLocationVO
		///    </Returns>
		///    <Authors>
		///       HungLa
		///    </Authors>
		///    <History>
		///       Tuesday, January 25, 2005
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
					+ MST_MasterLocationTable.MASTERLOCATIONID_FLD + ","
					+ MST_MasterLocationTable.CODE_FLD + ","
					+ MST_MasterLocationTable.NAME_FLD + ","
					+ MST_MasterLocationTable.ADDRESS_FLD + ","
					+ MST_MasterLocationTable.STATE_FLD + ","
					+ MST_MasterLocationTable.ZIPPOST_FLD + ","
					+ MST_MasterLocationTable.CCNID_FLD + ","
					+ MST_MasterLocationTable.CITYID_FLD + ","
					+ MST_MasterLocationTable.COUNTRYID_FLD
					+ " FROM " + MST_MasterLocationTable.TABLE_NAME
					+ " WHERE " + MST_MasterLocationTable.MASTERLOCATIONID_FLD + "=" + pintID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();

				MST_MasterLocationVO objObject = new MST_MasterLocationVO();

				while (odrPCS.Read())
				{
					objObject.MasterLocationID = int.Parse(odrPCS[MST_MasterLocationTable.MASTERLOCATIONID_FLD].ToString().Trim());
					objObject.Code = odrPCS[MST_MasterLocationTable.CODE_FLD].ToString().Trim();
					objObject.Name = odrPCS[MST_MasterLocationTable.NAME_FLD].ToString().Trim();
					objObject.Address = odrPCS[MST_MasterLocationTable.ADDRESS_FLD].ToString().Trim();
					objObject.State = odrPCS[MST_MasterLocationTable.STATE_FLD].ToString().Trim();
					objObject.ZipPost = odrPCS[MST_MasterLocationTable.ZIPPOST_FLD].ToString().Trim();
					if (odrPCS[MST_MasterLocationTable.CCNID_FLD] != DBNull.Value)
						objObject.CCNID = int.Parse(odrPCS[MST_MasterLocationTable.CCNID_FLD].ToString().Trim());
					if (odrPCS[MST_MasterLocationTable.CITYID_FLD] != DBNull.Value)
						objObject.CityID = int.Parse(odrPCS[MST_MasterLocationTable.CITYID_FLD].ToString().Trim());
					if (odrPCS[MST_MasterLocationTable.COUNTRYID_FLD] != DBNull.Value)
						objObject.CountryID = int.Parse(odrPCS[MST_MasterLocationTable.COUNTRYID_FLD].ToString().Trim());

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
		///       This method uses to update data to MST_MasterLocation
		///    </Description>
		///    <Inputs>
		///       MST_MasterLocationVO       
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

			MST_MasterLocationVO objObject = (MST_MasterLocationVO) pobjObjecVO;


			//prepare value for parameters
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				strSql = "UPDATE MST_MasterLocation SET "
					+ MST_MasterLocationTable.CODE_FLD + "=   ?" + ","
					+ MST_MasterLocationTable.NAME_FLD + "=   ?" + ","
					+ MST_MasterLocationTable.ADDRESS_FLD + "=   ?" + ","
					+ MST_MasterLocationTable.STATE_FLD + "=   ?" + ","
					+ MST_MasterLocationTable.ZIPPOST_FLD + "=   ?" + ","
					+ MST_MasterLocationTable.CCNID_FLD + "=   ?" + ","
					+ MST_MasterLocationTable.CITYID_FLD + "=   ?" + ","
					+ MST_MasterLocationTable.COUNTRYID_FLD + "=  ?"
					+ " WHERE " + MST_MasterLocationTable.MASTERLOCATIONID_FLD + "= ?";

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_MasterLocationTable.CODE_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[MST_MasterLocationTable.CODE_FLD].Value = objObject.Code;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_MasterLocationTable.NAME_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[MST_MasterLocationTable.NAME_FLD].Value = objObject.Name;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_MasterLocationTable.ADDRESS_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[MST_MasterLocationTable.ADDRESS_FLD].Value = objObject.Address;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_MasterLocationTable.STATE_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[MST_MasterLocationTable.STATE_FLD].Value = objObject.State;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_MasterLocationTable.ZIPPOST_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[MST_MasterLocationTable.ZIPPOST_FLD].Value = objObject.ZipPost;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_MasterLocationTable.CCNID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[MST_MasterLocationTable.CCNID_FLD].Value = objObject.CCNID;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_MasterLocationTable.CITYID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[MST_MasterLocationTable.CITYID_FLD].Value = objObject.CityID;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_MasterLocationTable.COUNTRYID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[MST_MasterLocationTable.COUNTRYID_FLD].Value = objObject.CountryID;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_MasterLocationTable.MASTERLOCATIONID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[MST_MasterLocationTable.MASTERLOCATIONID_FLD].Value = objObject.MasterLocationID;


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
		///       This method uses to get all data from MST_MasterLocation
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
		///       Tuesday, January 25, 2005
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
					+ MST_MasterLocationTable.MASTERLOCATIONID_FLD + ","
					+ MST_MasterLocationTable.CODE_FLD + ","
					+ MST_MasterLocationTable.NAME_FLD + ","
					+ MST_MasterLocationTable.ADDRESS_FLD + ","
					+ MST_MasterLocationTable.STATE_FLD + ","
					+ MST_MasterLocationTable.ZIPPOST_FLD + ","
					+ MST_MasterLocationTable.CCNID_FLD + ","
					+ MST_MasterLocationTable.CITYID_FLD + ","
					+ MST_MasterLocationTable.COUNTRYID_FLD
					+ " FROM " + MST_MasterLocationTable.TABLE_NAME;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();

				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS, MST_MasterLocationTable.TABLE_NAME);

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

		public DataSet List(string pstrQueryCondition)
		{
			const string METHOD_NAME = THIS + ".List()";
			DataSet dstPCS = new DataSet();
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;

				strSql = "SELECT "
					+ MST_MasterLocationTable.MASTERLOCATIONID_FLD + ","
					+ MST_MasterLocationTable.CODE_FLD + ","
					+ MST_MasterLocationTable.NAME_FLD + ","
					+ MST_MasterLocationTable.ADDRESS_FLD + ","
					+ MST_MasterLocationTable.STATE_FLD + ","
					+ MST_MasterLocationTable.ZIPPOST_FLD + ","
					+ MST_MasterLocationTable.CCNID_FLD + ","
					+ MST_MasterLocationTable.CITYID_FLD + ","
					+ MST_MasterLocationTable.COUNTRYID_FLD
					+ " FROM " + MST_MasterLocationTable.TABLE_NAME
					+ " WHERE " + pstrQueryCondition;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();

				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS, MST_MasterLocationTable.TABLE_NAME);

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
		///       Tuesday, January 25, 2005
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
					+ MST_MasterLocationTable.MASTERLOCATIONID_FLD + ","
					+ MST_MasterLocationTable.CODE_FLD + ","
					+ MST_MasterLocationTable.NAME_FLD + ","
					+ MST_MasterLocationTable.ADDRESS_FLD + ","
					+ MST_MasterLocationTable.STATE_FLD + ","
					+ MST_MasterLocationTable.ZIPPOST_FLD + ","
					+ MST_MasterLocationTable.CCNID_FLD + ","
					+ MST_MasterLocationTable.CITYID_FLD + ","
					+ MST_MasterLocationTable.COUNTRYID_FLD
					+ "  FROM " + MST_MasterLocationTable.TABLE_NAME;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				odadPCS.SelectCommand = new OleDbCommand(strSql, oconPCS);
				odcbPCS = new OleDbCommandBuilder(odadPCS);
				pData.EnforceConstraints = false;
				odadPCS.Update(pData, MST_MasterLocationTable.TABLE_NAME);

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
		///       This method uses to get master location by CCN ID
		///    </Description>
		///    <Inputs>
		///        int
		///    </Inputs>
		///    <Outputs>
		///       DataSet
		///    </Outputs>
		///    <Returns>
		///       DataSet
		///    </Returns>
		///    <Authors>
		///       DungLa
		///    </Authors>
		///    <History>
		///       Feb - 17 - 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public DataSet ListByCCNID(int pintCCNID)
		{
			const string METHOD_NAME = THIS + ".ListByCCNID()";
			DataSet dstPCS = new DataSet();

			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;

				strSql = "SELECT "
					+ MST_MasterLocationTable.MASTERLOCATIONID_FLD + ","
					+ MST_MasterLocationTable.CODE_FLD + ","
					+ MST_MasterLocationTable.NAME_FLD + ","
					+ MST_MasterLocationTable.ADDRESS_FLD + ","
					+ MST_MasterLocationTable.STATE_FLD + ","
					+ MST_MasterLocationTable.ZIPPOST_FLD + ","
					+ MST_MasterLocationTable.CCNID_FLD + ","
					+ MST_MasterLocationTable.CITYID_FLD + ","
					+ MST_MasterLocationTable.COUNTRYID_FLD
					+ " FROM " + MST_MasterLocationTable.TABLE_NAME
					+ " WHERE " + MST_MasterLocationTable.CCNID_FLD + "=" + pintCCNID;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();

				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS, MST_MasterLocationTable.TABLE_NAME);

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
		///       This method uses to get data for MasterLocation dropdown
		///    </Description>
		///    <Inputs>
		///        int
		///    </Inputs>
		///    <Outputs>
		///       DataTable
		///    </Outputs>
		///    <Returns>
		///       DataTable
		///    </Returns>
		///    <Authors>
		///       DungLa
		///    </Authors>
		///    <History>
		///       17-Feb-2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public DataTable GetDataForTrueDBDropDown(int pintCCNID)
		{
			const string METHOD_NAME = THIS + ".GetDataForMasLocDropDown()";
			DataSet dstPCS = new DataSet();

			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;

				strSql = "SELECT "
					+ MST_MasterLocationTable.MASTERLOCATIONID_FLD + ","
					+ MST_MasterLocationTable.CODE_FLD
					+ " FROM " + MST_MasterLocationTable.TABLE_NAME;
				if (pintCCNID != 0)
					strSql += " WHERE " + MST_MasterLocationTable.CCNID_FLD + "=" + pintCCNID;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();

				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS, MST_MasterLocationTable.TABLE_NAME);

				return dstPCS.Tables[MST_MasterLocationTable.TABLE_NAME];
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
		///       This method uses to get master location code from id
		///    </Description>
		///    <Inputs>
		///        id
		///    </Inputs>
		///    <Outputs>
		///       string Code
		///    </Outputs>
		///    <Returns>
		///       string Code
		///    </Returns>
		///    <Authors>
		///       DungLa
		///    </Authors>
		///    <History>
		///       21-Apr-2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public string GetCodeFromID(int pintID)
		{
			const string METHOD_NAME = THIS + ".GetCodeFromID()";
			
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				strSql=	"SELECT "
					+ MST_MasterLocationTable.CODE_FLD
					+ " FROM " + MST_MasterLocationTable.TABLE_NAME
					+" WHERE " + MST_MasterLocationTable.MASTERLOCATIONID_FLD + "=" + pintID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				object objResult = ocmdPCS.ExecuteScalar();
				if (objResult != null)
					return objResult.ToString().Trim();
				else
					return string.Empty;
			}
			catch(OleDbException ex)
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