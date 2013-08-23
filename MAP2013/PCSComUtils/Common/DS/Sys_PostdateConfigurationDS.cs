using System;
using System.Data;
using System.Data.OleDb;
using PCSComUtils.DataAccess;

using PCSComUtils.PCSExc;

namespace PCSComUtils.Common.DS
{
	public class Sys_PostdateConfigurationDS
	{
		public Sys_PostdateConfigurationDS()
		{
		}

		private const string THIS = "PCSComUtils.Common.DS.DS.Sys_PostdateConfigurationDS";
		//**************************************************************************              
		///    <Description>
		///       This method uses to add data to Sys_PostdateConfiguration
		///    </Description>
		///    <Inputs>
		///        Sys_PostdateConfigurationVO       
		///    </Inputs>
		///    <Outputs>
		///       newly inserted primarkey value
		///    </Outputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <Authors>
		///       Code generate
		///    </Authors>
		///    <History>
		///       Monday, September 18, 2006
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
				Sys_PostdateConfigurationVO objObject = (Sys_PostdateConfigurationVO) pobjObjectVO;
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand("", oconPCS);

				strSql = "INSERT INTO Sys_PostdateConfiguration("
					+ Sys_PostdateConfigurationTable.DAYBEFORE_FLD + ","
					+ Sys_PostdateConfigurationTable.LASTUPDATED_FLD + ","
					+ Sys_PostdateConfigurationTable.USERNAME_FLD + ")"
					+ "VALUES(?,?,?)";

				ocmdPCS.Parameters.Add(new OleDbParameter(Sys_PostdateConfigurationTable.DAYBEFORE_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[Sys_PostdateConfigurationTable.DAYBEFORE_FLD].Value = objObject.DayBefore;

				ocmdPCS.Parameters.Add(new OleDbParameter(Sys_PostdateConfigurationTable.LASTUPDATED_FLD, OleDbType.Date));
				ocmdPCS.Parameters[Sys_PostdateConfigurationTable.LASTUPDATED_FLD].Value = objObject.LastUpdated;

				ocmdPCS.Parameters.Add(new OleDbParameter(Sys_PostdateConfigurationTable.USERNAME_FLD, OleDbType.WChar));
				ocmdPCS.Parameters[Sys_PostdateConfigurationTable.USERNAME_FLD].Value = objObject.Username;


				ocmdPCS.CommandText = strSql;
				ocmdPCS.Connection.Open();
				ocmdPCS.ExecuteNonQuery();

			}
			catch (OleDbException ex)
			{
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
			}

			catch (InvalidOperationException ex)
			{
				throw new PCSDBException(ErrorCode.DUPLICATE_KEY, METHOD_NAME, ex);
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
		///       This method uses to delete data from Sys_PostdateConfiguration
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
		///       Code generate
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
			strSql = "DELETE " + Sys_PostdateConfigurationTable.TABLE_NAME + " WHERE  " + "PostdateConfigurationID" + "=" + pintID.ToString();
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
		///       This method uses to get data from Sys_PostdateConfiguration
		///    </Description>
		///    <Inputs>
		///        ID       
		///    </Inputs>
		///    <Outputs>
		///       Sys_PostdateConfigurationVO
		///    </Outputs>
		///    <Returns>
		///       Sys_PostdateConfigurationVO
		///    </Returns>
		///    <Authors>
		///       Code Generate 
		///    </Authors>
		///    <History>
		///       Monday, September 18, 2006
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
					+ Sys_PostdateConfigurationTable.POSTDATECONFIGURATIONID_FLD + ","
					+ Sys_PostdateConfigurationTable.DAYBEFORE_FLD + ","
					+ Sys_PostdateConfigurationTable.LASTUPDATED_FLD + ","
					+ Sys_PostdateConfigurationTable.USERNAME_FLD
					+ " FROM " + Sys_PostdateConfigurationTable.TABLE_NAME
					+ " WHERE " + Sys_PostdateConfigurationTable.POSTDATECONFIGURATIONID_FLD + "=" + pintID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();

				Sys_PostdateConfigurationVO objObject = new Sys_PostdateConfigurationVO();

				while (odrPCS.Read())
				{
					objObject.PostdateConfigurationID = int.Parse(odrPCS[Sys_PostdateConfigurationTable.POSTDATECONFIGURATIONID_FLD].ToString());
					objObject.DayBefore = int.Parse(odrPCS[Sys_PostdateConfigurationTable.DAYBEFORE_FLD].ToString());
					objObject.LastUpdated = DateTime.Parse(odrPCS[Sys_PostdateConfigurationTable.LASTUPDATED_FLD].ToString());
					objObject.Username = odrPCS[Sys_PostdateConfigurationTable.USERNAME_FLD].ToString();

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
		///       This method uses to update data to Sys_PostdateConfiguration
		///    </Description>
		///    <Inputs>
		///       Sys_PostdateConfigurationVO       
		///    </Inputs>
		///    <Outputs>
		///       
		///    </Outputs>
		///    <Returns>
		///       
		///    </Returns>
		///    <Authors>
		///       Code Generate 
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

			Sys_PostdateConfigurationVO objObject = (Sys_PostdateConfigurationVO) pobjObjecVO;


			//prepare value for parameters
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				strSql = "UPDATE Sys_PostdateConfiguration SET "
					+ Sys_PostdateConfigurationTable.DAYBEFORE_FLD + "=   ?" + ","
					+ Sys_PostdateConfigurationTable.LASTUPDATED_FLD + "=   ?" + ","
					+ Sys_PostdateConfigurationTable.USERNAME_FLD + "=  ?"
					+ " WHERE " + Sys_PostdateConfigurationTable.POSTDATECONFIGURATIONID_FLD + "= ?";

				ocmdPCS.Parameters.Add(new OleDbParameter(Sys_PostdateConfigurationTable.DAYBEFORE_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[Sys_PostdateConfigurationTable.DAYBEFORE_FLD].Value = objObject.DayBefore;

				ocmdPCS.Parameters.Add(new OleDbParameter(Sys_PostdateConfigurationTable.LASTUPDATED_FLD, OleDbType.Date));
				ocmdPCS.Parameters[Sys_PostdateConfigurationTable.LASTUPDATED_FLD].Value = objObject.LastUpdated;

				ocmdPCS.Parameters.Add(new OleDbParameter(Sys_PostdateConfigurationTable.USERNAME_FLD, OleDbType.WChar));
				ocmdPCS.Parameters[Sys_PostdateConfigurationTable.USERNAME_FLD].Value = objObject.Username;

				ocmdPCS.Parameters.Add(new OleDbParameter(Sys_PostdateConfigurationTable.POSTDATECONFIGURATIONID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[Sys_PostdateConfigurationTable.POSTDATECONFIGURATIONID_FLD].Value = objObject.PostdateConfigurationID;


				ocmdPCS.CommandText = strSql;
				ocmdPCS.Connection.Open();
				ocmdPCS.ExecuteNonQuery();
			}
			catch (OleDbException ex)
			{
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
			}

			catch (InvalidOperationException ex)
			{
				throw new PCSDBException(ErrorCode.DUPLICATE_KEY, METHOD_NAME, ex);
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
		///       This method uses to get all data from Sys_PostdateConfiguration
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
		///       Code Generate 
		///    </Authors>
		///    <History>
		///       Monday, September 18, 2006
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
					+ Sys_PostdateConfigurationTable.POSTDATECONFIGURATIONID_FLD + ","
					+ Sys_PostdateConfigurationTable.DAYBEFORE_FLD + ","
					+ Sys_PostdateConfigurationTable.LASTUPDATED_FLD + ","
					+ Sys_PostdateConfigurationTable.USERNAME_FLD
					+ " FROM " + Sys_PostdateConfigurationTable.TABLE_NAME;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();

				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS, Sys_PostdateConfigurationTable.TABLE_NAME);

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

		public DataSet List(PostDateConfigPurpose enmPurpose)
		{
			const string METHOD_NAME = THIS + ".List()";
			DataSet dstPCS = new DataSet();


			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;

				strSql = "SELECT "
					+ Sys_PostdateConfigurationTable.POSTDATECONFIGURATIONID_FLD + ","
					+ Sys_PostdateConfigurationTable.DAYBEFORE_FLD + ","
					+ Sys_PostdateConfigurationTable.LASTUPDATED_FLD + ","
					+ Sys_PostdateConfigurationTable.PURPOSE_FLD + ","
					+ Sys_PostdateConfigurationTable.USERNAME_FLD
					+ " FROM " + Sys_PostdateConfigurationTable.TABLE_NAME
					+ " WHERE " + Sys_PostdateConfigurationTable.PURPOSE_FLD + "=?";
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();

				ocmdPCS.Parameters.Add(new OleDbParameter(Sys_PostdateConfigurationTable.PURPOSE_FLD, OleDbType.VarWChar)).Value = enmPurpose.ToString();
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS, Sys_PostdateConfigurationTable.TABLE_NAME);

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
		///       Code Generate
		///    </Authors>
		///    <History>
		///       Monday, September 18, 2006
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
					+ Sys_PostdateConfigurationTable.POSTDATECONFIGURATIONID_FLD + ","
					+ Sys_PostdateConfigurationTable.DAYBEFORE_FLD + ","
					+ Sys_PostdateConfigurationTable.LASTUPDATED_FLD + ","
					+ Sys_PostdateConfigurationTable.USERNAME_FLD;


				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				odadPCS.SelectCommand = new OleDbCommand(strSql, oconPCS);
				odcbPCS = new OleDbCommandBuilder(odadPCS);
				pData.EnforceConstraints = false;
				odadPCS.Update(pData, Sys_PostdateConfigurationTable.TABLE_NAME);

			}
			catch (OleDbException ex)
			{
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
			}

			catch (InvalidOperationException ex)
			{
				throw new PCSDBException(ErrorCode.DUPLICATE_KEY, METHOD_NAME, ex);
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