using System;
using System.Data;
using System.Data.OleDb;
using PCSComUtils.DataAccess;
using PCSComUtils.PCSExc;
using PCSComUtils.Common;

namespace PCSComUtils.MasterSetup.DS
{
	public class MST_LocationDS 
	{
		public MST_LocationDS()
		{
		}
		private const string THIS = "PCSComUtils.MasterSetup.DS.MST_LocationDS";
		//**************************************************************************              
		///    <Description>
		///       This method uses to add data to MST_Location
		///    </Description>
		///    <Inputs>
		///        MST_LocationVO       
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
			 
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS =null;
			try
			{
				MST_LocationVO objObject = (MST_LocationVO) pobjObjectVO;
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand("", oconPCS);
				
				strSql=	"INSERT INTO MST_Location("
				+ MST_LocationTable.CODE_FLD + ","
				+ MST_LocationTable.NAME_FLD + ","
				+ MST_LocationTable.TYPE_FLD + ","
				+ MST_LocationTable.MANUFACTURINGACCESS_FLD + ","
				+ MST_LocationTable.SALEACCESS_FLD + ","
				+ MST_LocationTable.BIN_FLD + ","
				+ MST_LocationTable.MASTERLOCATIONID_FLD + ")"
				+ "VALUES(?,?,?,?,?,?,?)";

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_LocationTable.CODE_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[MST_LocationTable.CODE_FLD].Value = objObject.Code;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_LocationTable.NAME_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[MST_LocationTable.NAME_FLD].Value = objObject.Name;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_LocationTable.TYPE_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[MST_LocationTable.TYPE_FLD].Value = objObject.Type;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_LocationTable.MANUFACTURINGACCESS_FLD, OleDbType.Boolean));
				ocmdPCS.Parameters[MST_LocationTable.MANUFACTURINGACCESS_FLD].Value = objObject.ManufacturingAccess;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_LocationTable.SALEACCESS_FLD, OleDbType.Boolean));
				ocmdPCS.Parameters[MST_LocationTable.SALEACCESS_FLD].Value = objObject.SaleAccess;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_LocationTable.BIN_FLD, OleDbType.Boolean));
				ocmdPCS.Parameters[MST_LocationTable.BIN_FLD].Value = objObject.Bin;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_LocationTable.MASTERLOCATIONID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[MST_LocationTable.MASTERLOCATIONID_FLD].Value = objObject.MasterLocationID;


				
				ocmdPCS.CommandText = strSql;
				ocmdPCS.Connection.Open();
				ocmdPCS.ExecuteNonQuery();	

			}
			catch(OleDbException ex)
			{
				if (ex.Errors[1].NativeError == ErrorCode.SQLDUPLICATE_KEYCODE)
				{
																   
					throw new PCSDBException(ErrorCode.DUPLICATE_KEY, METHOD_NAME, ex);				
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
	
		//**************************************************************************              
		///    <Description>
		///       This method uses to delete data from MST_Location
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
			strSql=	"DELETE " + MST_LocationTable.TABLE_NAME + " WHERE  " + "LocationID" + "=" + pintID.ToString();
			OleDbConnection oconPCS=null;
			OleDbCommand ocmdPCS =null;
			try
			{
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				ocmdPCS.ExecuteNonQuery();	
				ocmdPCS = null;

			}
			catch(OleDbException ex)
			{
				if (ex.Errors[1].NativeError == ErrorCode.SQLCASCADE_PREVENT_KEYCODE)
				{
																   
					throw new PCSDBException(ErrorCode.CASCADE_DELETE_PREVENT, METHOD_NAME, ex);				
				}
				else
				{
					throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME,ex);
				}
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
	
		//**************************************************************************              
		///    <Description>
		///       This method uses to get data from MST_Location
		///    </Description>
		///    <Inputs>
		///        ID       
		///    </Inputs>
		///    <Outputs>
		///       MST_LocationVO
		///    </Outputs>
		///    <Returns>
		///       MST_LocationVO
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
				strSql=	"SELECT "
				+ MST_LocationTable.LOCATIONID_FLD + ","
				+ MST_LocationTable.CODE_FLD + ","
				+ MST_LocationTable.NAME_FLD + ","
				+ MST_LocationTable.TYPE_FLD + ","
				+ MST_LocationTable.MANUFACTURINGACCESS_FLD + ","
				+ MST_LocationTable.SALEACCESS_FLD + ","
				+ MST_LocationTable.BIN_FLD + ","
				+ MST_LocationTable.MASTERLOCATIONID_FLD
				+ " FROM " + MST_LocationTable.TABLE_NAME
				+" WHERE " + MST_LocationTable.LOCATIONID_FLD + "=" + pintID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();

				MST_LocationVO objObject = new MST_LocationVO();

				while (odrPCS.Read())
				{
					if (odrPCS[MST_LocationTable.LOCATIONID_FLD] != DBNull.Value)
					{
						objObject.LocationID = int.Parse(odrPCS[MST_LocationTable.LOCATIONID_FLD].ToString().Trim());
					}
					objObject.Code = odrPCS[MST_LocationTable.CODE_FLD].ToString().Trim();
					objObject.Name = odrPCS[MST_LocationTable.NAME_FLD].ToString().Trim();
					objObject.Type = odrPCS[MST_LocationTable.TYPE_FLD].ToString().Trim();
					if (odrPCS[MST_LocationTable.MANUFACTURINGACCESS_FLD] != DBNull.Value)
					{
						objObject.ManufacturingAccess = bool.Parse(odrPCS[MST_LocationTable.MANUFACTURINGACCESS_FLD].ToString().Trim());
					}
					if (odrPCS[MST_LocationTable.SALEACCESS_FLD] != DBNull.Value)
					{
						objObject.SaleAccess = bool.Parse(odrPCS[MST_LocationTable.SALEACCESS_FLD].ToString().Trim());
					}
					if (odrPCS[MST_LocationTable.BIN_FLD] != DBNull.Value)
					{
						objObject.Bin = bool.Parse(odrPCS[MST_LocationTable.BIN_FLD].ToString().Trim());
					}
					if (odrPCS[MST_LocationTable.MASTERLOCATIONID_FLD] != DBNull.Value)
					{
						objObject.MasterLocationID = int.Parse(odrPCS[MST_LocationTable.MASTERLOCATIONID_FLD].ToString().Trim());
					}
				}		
				return objObject;					
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

		//**************************************************************************              
		///    <Description>
		///       This method uses to update data to MST_Location
		///    </Description>
		///    <Inputs>
		///       MST_LocationVO       
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

			MST_LocationVO objObject = (MST_LocationVO) pobjObjecVO;
			

			//prepare value for parameters
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				strSql=	"UPDATE MST_Location SET "
				+ MST_LocationTable.CODE_FLD + "=   ?" + ","
				+ MST_LocationTable.NAME_FLD + "=   ?" + ","
				+ MST_LocationTable.TYPE_FLD + "=   ?" + ","
				+ MST_LocationTable.MANUFACTURINGACCESS_FLD + "=   ?" + ","
				+ MST_LocationTable.SALEACCESS_FLD + "=   ?" + ","
				+ MST_LocationTable.BIN_FLD + "=   ?" + ","
				+ MST_LocationTable.MASTERLOCATIONID_FLD + "=  ?"
				+" WHERE " + MST_LocationTable.LOCATIONID_FLD + "= ?";

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_LocationTable.CODE_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[MST_LocationTable.CODE_FLD].Value = objObject.Code;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_LocationTable.NAME_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[MST_LocationTable.NAME_FLD].Value = objObject.Name;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_LocationTable.TYPE_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[MST_LocationTable.TYPE_FLD].Value = objObject.Type;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_LocationTable.MANUFACTURINGACCESS_FLD, OleDbType.Boolean));
				ocmdPCS.Parameters[MST_LocationTable.MANUFACTURINGACCESS_FLD].Value = objObject.ManufacturingAccess;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_LocationTable.SALEACCESS_FLD, OleDbType.Boolean));
				ocmdPCS.Parameters[MST_LocationTable.SALEACCESS_FLD].Value = objObject.SaleAccess;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_LocationTable.BIN_FLD, OleDbType.Boolean));
				ocmdPCS.Parameters[MST_LocationTable.BIN_FLD].Value = objObject.Bin;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_LocationTable.MASTERLOCATIONID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[MST_LocationTable.MASTERLOCATIONID_FLD].Value = objObject.MasterLocationID;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_LocationTable.LOCATIONID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[MST_LocationTable.LOCATIONID_FLD].Value = objObject.LocationID;


				ocmdPCS.CommandText = strSql;
				ocmdPCS.Connection.Open();
				ocmdPCS.ExecuteNonQuery();	
			}
			catch(OleDbException ex)
			{
				if (ex.Errors[1].NativeError == ErrorCode.SQLDUPLICATE_KEYCODE)
				{
																   
					throw new PCSDBException(ErrorCode.DUPLICATE_KEY, METHOD_NAME, ex);				
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

		//**************************************************************************              
		///    <Description>
		///       This method uses to get all data from MST_Location
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
			
		

			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				
				strSql=	"SELECT "
				+ MST_LocationTable.LOCATIONID_FLD + ","
				+ MST_LocationTable.CODE_FLD + ","
				+ MST_LocationTable.NAME_FLD + ","
				+ MST_LocationTable.TYPE_FLD + ","
				+ MST_LocationTable.MANUFACTURINGACCESS_FLD + ","
				+ MST_LocationTable.SALEACCESS_FLD + ","
				+ MST_LocationTable.BIN_FLD + ","
				+ MST_LocationTable.MASTERLOCATIONID_FLD
					+ " FROM " + MST_LocationTable.TABLE_NAME;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS,MST_LocationTable.TABLE_NAME);

				return dstPCS;
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


		public int GetMasterLocationIDByLocationID(int pintLocationID)
		{
			const string METHOD_NAME = THIS + ".List()";
			DataSet dstPCS = new DataSet();
			
		

			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				
				strSql=	"SELECT "
					+ MST_LocationTable.LOCATIONID_FLD + ","
					+ MST_LocationTable.MASTERLOCATIONID_FLD
					+ " FROM " + MST_LocationTable.TABLE_NAME;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS,MST_LocationTable.TABLE_NAME);
				if (dstPCS.Tables[0].Rows.Count > 0)
				{
					return int.Parse(dstPCS.Tables[0].Rows[0][MST_LocationTable.MASTERLOCATIONID_FLD].ToString());	
				}
				else
					return 0;
				
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
			OleDbConnection oconPCS =null;
			OleDbCommandBuilder odcbPCS ;
			OleDbDataAdapter odadPCS = new OleDbDataAdapter();

			try
			{
				strSql=	"SELECT "
				+ MST_LocationTable.LOCATIONID_FLD + ","
				+ MST_LocationTable.CODE_FLD + ","
				+ MST_LocationTable.NAME_FLD + ","
				+ MST_LocationTable.TYPE_FLD + ","
				+ MST_LocationTable.MANUFACTURINGACCESS_FLD + ","
				+ MST_LocationTable.SALEACCESS_FLD + ","
				+ MST_LocationTable.BIN_FLD + ","
				+ MST_LocationTable.MASTERLOCATIONID_FLD 
		+ "  FROM " + MST_LocationTable.TABLE_NAME;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				odadPCS.SelectCommand = new OleDbCommand(strSql, oconPCS);
				odcbPCS = new OleDbCommandBuilder(odadPCS);
				pData.EnforceConstraints = false;
				odadPCS.Update(pData,MST_LocationTable.TABLE_NAME);

			}
			catch(OleDbException ex)
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
		//**************************************************************************              
		///    <Description>
		///       This method uses to get location by MasterLocationID
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
		public DataSet ListByMasterLocationID(int pintMasterLocationID)
		{
			const string METHOD_NAME = THIS + ".List()";
			DataSet dstPCS = new DataSet();
			
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				
				strSql=	"SELECT "
					+ MST_LocationTable.LOCATIONID_FLD + ","
					+ MST_LocationTable.CODE_FLD + ","
					+ MST_LocationTable.NAME_FLD + ","
					+ MST_LocationTable.TYPE_FLD + ","
					+ MST_LocationTable.MANUFACTURINGACCESS_FLD + ","
					+ MST_LocationTable.SALEACCESS_FLD + ","
					+ MST_LocationTable.BIN_FLD + ","
					+ MST_LocationTable.MASTERLOCATIONID_FLD
					+ " FROM " + MST_LocationTable.TABLE_NAME
					+ " WHERE " + MST_LocationTable.MASTERLOCATIONID_FLD + "=" + pintMasterLocationID;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS,MST_LocationTable.TABLE_NAME);

				return dstPCS;
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

		/// <summary>
		/// Get Location Info for selecting (in In-Out Stock Report)
		/// </summary>
		/// <param name="pintMasterLocationID"></param>
		/// <returns></returns>
		/// <author> Tuan TQ. 16 Jan, 2006</author>
		public DataTable GetByLocation4Selecting(int pintMasterLocationID, string pstrOtherCondition)
		{
			const string METHOD_NAME = THIS + ".List()";
			DataTable dtbResult = new DataTable(MST_LocationTable.TABLE_NAME);

			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				
				strSql = "SELECT Convert(bit, 0) as Select_Fld,";
				strSql += " [LocationID],";
				strSql += " [Code],";
				strSql += " [Name],";
				strSql += " [Bin]";
				strSql += " FROM MST_Location";
				strSql += " WHERE " + MST_LocationTable.MASTERLOCATIONID_FLD + "=" + pintMasterLocationID;
				strSql += pstrOtherCondition;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dtbResult);

				return dtbResult;
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

		//**************************************************************************              
		///    <Description>
		///       This method uses to get location by CCNID
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
		///       TuanDm
		///    </Authors>
		///    <History>
		///       Apr - 04 - 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public DataSet ListByCCNID(int pintCCNID)
		{
			const string METHOD_NAME = THIS + ".List()";
			DataSet dstPCS = new DataSet();
			
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				
				strSql=	"SELECT "
					+ MST_LocationTable.LOCATIONID_FLD + ","
					+ "A." + MST_LocationTable.CODE_FLD + ","
					+ "A." + MST_LocationTable.NAME_FLD + ","
					+ MST_LocationTable.TYPE_FLD + ","
					+ MST_LocationTable.MANUFACTURINGACCESS_FLD + ","
					+ MST_LocationTable.SALEACCESS_FLD + ","
					+ MST_LocationTable.BIN_FLD + ","
					+ "A." + MST_LocationTable.MASTERLOCATIONID_FLD
					+ " FROM " + MST_LocationTable.TABLE_NAME + " A inner join " + MST_MasterLocationTable.TABLE_NAME + " B on A." + MST_LocationTable.MASTERLOCATIONID_FLD + " = B." + MST_MasterLocationTable.MASTERLOCATIONID_FLD
					+ " WHERE B." + MST_MasterLocationTable.CCNID_FLD + "=" + pintCCNID;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS,MST_LocationTable.TABLE_NAME);

				return dstPCS;
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
		//**************************************************************************              
		///    <Description>
		///       This method uses to get data for Location dropdown
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
		public DataTable GetDataForTrueDBDropDown(int pintMasterLocationID)
		{
			const string METHOD_NAME = THIS + ".GetDataForLocationDropDown()";
			DataSet dstPCS = new DataSet();
			
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				
				strSql=	"SELECT "
					+ MST_LocationTable.LOCATIONID_FLD + ","
					+ MST_LocationTable.CODE_FLD 
					+ " FROM " + MST_LocationTable.TABLE_NAME;
				if (pintMasterLocationID != 0)
					strSql += " WHERE " + MST_LocationTable.MASTERLOCATIONID_FLD + "=" + pintMasterLocationID;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS, MST_LocationTable.TABLE_NAME);

				return dstPCS.Tables[MST_LocationTable.TABLE_NAME];
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
		//**************************************************************************              
		///    <Description>
		///       This method uses to get location code from id
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
					+ MST_LocationTable.CODE_FLD
					+ " FROM " + MST_LocationTable.TABLE_NAME
					+" WHERE " + MST_LocationTable.LOCATIONID_FLD + "=" + pintID;

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

		//**************************************************************************              
		///    <Description>
		///       This method uses to get location name from code
		///    </Description>
		///    <Inputs>
		///        id
		///    </Inputs>
		///    <Outputs>
		///       string Code
		///    </Outputs>
		///    <Returns>
		///       string Name
		///    </Returns>
		///    <Authors>
		///       Thachnn
		///    </Authors>
		///    <History>
		///       28-10-2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public string GetNameFromCode(string pstrCode)
		{
			//TODO:L Kill Injection here
			const string METHOD_NAME = THIS + ".GetNameFromCode()";
			
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				strSql=	"SELECT "
					+ MST_LocationTable.NAME_FLD
					+ " FROM " + MST_LocationTable.TABLE_NAME
					+" WHERE " + MST_LocationTable.CODE_FLD + "='" + pstrCode+"'";

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
