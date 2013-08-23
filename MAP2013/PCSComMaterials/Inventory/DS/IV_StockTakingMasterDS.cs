using System;
using System.Data;
using System.Data.OleDb;
using System.Collections;
using System.Configuration;
using PCSComUtils.Common;
using PCSComUtils.DataAccess;
using PCSComUtils.PCSExc;
namespace PCSComMaterials.Inventory.DS
{
	public class IV_StockTakingMasterDS 
	{
		public IV_StockTakingMasterDS()
		{
		}
		private const string THIS = "PCSComMaterials.Inventory.DS.DS.IV_StockTakingMasterDS";

	
		//**************************************************************************              
		///    <Description>
		///       This method uses to add data to IV_StockTakingMaster
		///    </Description>
		///    <Inputs>
		///        IV_StockTakingMasterVO       
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
		///       Monday, November 20, 2006
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
				IV_StockTakingMasterVO objObject = (IV_StockTakingMasterVO) pobjObjectVO;
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand("", oconPCS);
				
				strSql=	"INSERT INTO IV_StockTakingMaster("
				+ IV_StockTakingMasterTable.STOCKTAKINGPERIODID_FLD + ","
				+ "Code ,"
				+ IV_StockTakingMasterTable.DEPARTMENTID_FLD + ","
				+ IV_StockTakingMasterTable.PRODUCTIONLINEID_FLD + ","
				+ IV_StockTakingMasterTable.LOCATIONID_FLD + ","
				+ IV_StockTakingMasterTable.BINID_FLD + ")"
				+ "VALUES(?,?,?,?,?,?)";

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_StockTakingMasterTable.STOCKTAKINGPERIODID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_StockTakingMasterTable.STOCKTAKINGPERIODID_FLD].Value = objObject.StockTakingPeriodID;

				ocmdPCS.Parameters.Add(new OleDbParameter("Code", OleDbType.VarChar));
				ocmdPCS.Parameters["Code"].Value = objObject.Code;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_StockTakingMasterTable.DEPARTMENTID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_StockTakingMasterTable.DEPARTMENTID_FLD].Value = objObject.DepartmentID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_StockTakingMasterTable.PRODUCTIONLINEID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_StockTakingMasterTable.PRODUCTIONLINEID_FLD].Value = objObject.ProductionLineID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_StockTakingMasterTable.LOCATIONID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_StockTakingMasterTable.LOCATIONID_FLD].Value = objObject.LocationID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_StockTakingMasterTable.BINID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_StockTakingMasterTable.BINID_FLD].Value = objObject.BinID;


				
				ocmdPCS.CommandText = strSql;
				ocmdPCS.Connection.Open();
				ocmdPCS.ExecuteNonQuery();	

			}
			catch(OleDbException ex)
			{
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME,ex);
			}			

			catch(InvalidOperationException ex)
			{
				throw new PCSDBException(ErrorCode.DUPLICATE_KEY, METHOD_NAME, ex);
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
	
		public int AddAndReturnID(object pobjObjectVO)
		{
			const string METHOD_NAME = THIS + ".Add()";
			 
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS =null;
			try
			{
				IV_StockTakingMasterVO objObject = (IV_StockTakingMasterVO) pobjObjectVO;
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand("", oconPCS);
				
				strSql=	"INSERT INTO IV_StockTakingMaster("
					+ IV_StockTakingMasterTable.STOCKTAKINGPERIODID_FLD + ","
					+ "Code , StockTakingDate,"
					+ IV_StockTakingMasterTable.DEPARTMENTID_FLD + ","
					+ IV_StockTakingMasterTable.PRODUCTIONLINEID_FLD + ","
					+ IV_StockTakingMasterTable.LOCATIONID_FLD + ","
					+ IV_StockTakingMasterTable.BINID_FLD + ")"
					+ "VALUES(?,?,?,?,?,?,?);  SELECT @@Identity";

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_StockTakingMasterTable.STOCKTAKINGPERIODID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_StockTakingMasterTable.STOCKTAKINGPERIODID_FLD].Value = objObject.StockTakingPeriodID;

				ocmdPCS.Parameters.Add(new OleDbParameter("Code", OleDbType.VarChar));
				ocmdPCS.Parameters["Code"].Value = objObject.Code;
				ocmdPCS.Parameters.Add(new OleDbParameter("StockTakingDate", OleDbType.Date));
				ocmdPCS.Parameters["StockTakingDate"].Value = objObject.StockTakingDate;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_StockTakingMasterTable.DEPARTMENTID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_StockTakingMasterTable.DEPARTMENTID_FLD].Value = objObject.DepartmentID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_StockTakingMasterTable.PRODUCTIONLINEID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_StockTakingMasterTable.PRODUCTIONLINEID_FLD].Value = objObject.ProductionLineID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_StockTakingMasterTable.LOCATIONID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_StockTakingMasterTable.LOCATIONID_FLD].Value = objObject.LocationID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_StockTakingMasterTable.BINID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_StockTakingMasterTable.BINID_FLD].Value = objObject.BinID;


				
				ocmdPCS.CommandText = strSql;
				ocmdPCS.Connection.Open();
				//ocmdPCS.ExecuteNonQuery();	
				object objResult = ocmdPCS.ExecuteScalar();
				int intMasterID = 0;
				oconPCS.Close();
				if ((objResult != DBNull.Value) && (objResult != null))
				{
					intMasterID = int.Parse(objResult.ToString());
				}
				return intMasterID;
			}
			catch(OleDbException ex)
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
					throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
				
			}			
			catch(InvalidOperationException ex)
			{
				throw new PCSDBException(ErrorCode.DUPLICATE_KEY, METHOD_NAME, ex);
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
		///       This method uses to delete data from IV_StockTakingMaster
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
			strSql=	"DELETE " + IV_StockTakingMasterTable.TABLE_NAME + " WHERE  " + "StockTakingMasterID" + "=" + pintID.ToString();
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
		///       This method uses to get data from IV_StockTakingMaster
		///    </Description>
		///    <Inputs>
		///        ID       
		///    </Inputs>
		///    <Outputs>
		///       IV_StockTakingMasterVO
		///    </Outputs>
		///    <Returns>
		///       IV_StockTakingMasterVO
		///    </Returns>
		///    <Authors>
		///       Code Generate 
		///    </Authors>
		///    <History>
		///       Monday, November 20, 2006
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
				+ IV_StockTakingMasterTable.STOCKTAKINGMASTERID_FLD + ","
				+ IV_StockTakingMasterTable.STOCKTAKINGPERIODID_FLD + ","
				+ IV_StockTakingMasterTable.DEPARTMENTID_FLD + ","
				+ IV_StockTakingMasterTable.PRODUCTIONLINEID_FLD + ","
				+ IV_StockTakingMasterTable.LOCATIONID_FLD + ","
				+ IV_StockTakingMasterTable.BINID_FLD
				+ " FROM " + IV_StockTakingMasterTable.TABLE_NAME
				+" WHERE " + IV_StockTakingMasterTable.STOCKTAKINGMASTERID_FLD + "=" + pintID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();

				IV_StockTakingMasterVO objObject = new IV_StockTakingMasterVO();

				while (odrPCS.Read())
				{ 
				objObject.StockTakingMasterID = int.Parse(odrPCS[IV_StockTakingMasterTable.STOCKTAKINGMASTERID_FLD].ToString());
				objObject.StockTakingPeriodID = int.Parse(odrPCS[IV_StockTakingMasterTable.STOCKTAKINGPERIODID_FLD].ToString());
				objObject.DepartmentID = int.Parse(odrPCS[IV_StockTakingMasterTable.DEPARTMENTID_FLD].ToString());
				objObject.ProductionLineID = int.Parse(odrPCS[IV_StockTakingMasterTable.PRODUCTIONLINEID_FLD].ToString());
				objObject.LocationID = int.Parse(odrPCS[IV_StockTakingMasterTable.LOCATIONID_FLD].ToString());
				objObject.BinID = int.Parse(odrPCS[IV_StockTakingMasterTable.BINID_FLD].ToString());

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
		///       This method uses to update data to IV_StockTakingMaster
		///    </Description>
		///    <Inputs>
		///       IV_StockTakingMasterVO       
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

			IV_StockTakingMasterVO objObject = (IV_StockTakingMasterVO) pobjObjecVO;
			

			//prepare value for parameters
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				strSql=	"UPDATE IV_StockTakingMaster SET Code = ?,"
				+ " StockTakingDate = ?,"
				+ IV_StockTakingMasterTable.STOCKTAKINGPERIODID_FLD + "=   ?" + ","
				+ IV_StockTakingMasterTable.DEPARTMENTID_FLD + "=   ?" + ","
				+ IV_StockTakingMasterTable.PRODUCTIONLINEID_FLD + "=   ?" + ","
				+ IV_StockTakingMasterTable.LOCATIONID_FLD + "=   ?" + ","
				+ IV_StockTakingMasterTable.BINID_FLD + "=  ?"
				+" WHERE " + IV_StockTakingMasterTable.STOCKTAKINGMASTERID_FLD + "= ?";

				ocmdPCS.Parameters.Add(new OleDbParameter("Code", OleDbType.VarWChar));
				ocmdPCS.Parameters["Code"].Value = objObject.Code;

				ocmdPCS.Parameters.Add(new OleDbParameter("StockTakingDate", OleDbType.Date));
				ocmdPCS.Parameters["StockTakingDate"].Value = objObject.StockTakingDate;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_StockTakingMasterTable.STOCKTAKINGPERIODID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_StockTakingMasterTable.STOCKTAKINGPERIODID_FLD].Value = objObject.StockTakingPeriodID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_StockTakingMasterTable.DEPARTMENTID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_StockTakingMasterTable.DEPARTMENTID_FLD].Value = objObject.DepartmentID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_StockTakingMasterTable.PRODUCTIONLINEID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_StockTakingMasterTable.PRODUCTIONLINEID_FLD].Value = objObject.ProductionLineID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_StockTakingMasterTable.LOCATIONID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_StockTakingMasterTable.LOCATIONID_FLD].Value = objObject.LocationID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_StockTakingMasterTable.BINID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_StockTakingMasterTable.BINID_FLD].Value = objObject.BinID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_StockTakingMasterTable.STOCKTAKINGMASTERID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_StockTakingMasterTable.STOCKTAKINGMASTERID_FLD].Value = objObject.StockTakingMasterID;


				ocmdPCS.CommandText = strSql;
				ocmdPCS.Connection.Open();
				ocmdPCS.ExecuteNonQuery();	
			}
			catch(OleDbException ex)
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
		///       This method uses to get all data from IV_StockTakingMaster
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
		///       Monday, November 20, 2006
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
				+ IV_StockTakingMasterTable.STOCKTAKINGMASTERID_FLD + ","
				+ IV_StockTakingMasterTable.STOCKTAKINGPERIODID_FLD + ","
				+ IV_StockTakingMasterTable.DEPARTMENTID_FLD + ","
				+ IV_StockTakingMasterTable.PRODUCTIONLINEID_FLD + ","
				+ IV_StockTakingMasterTable.LOCATIONID_FLD + ","
				+ IV_StockTakingMasterTable.BINID_FLD
					+ " FROM " + IV_StockTakingMasterTable.TABLE_NAME;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS,IV_StockTakingMasterTable.TABLE_NAME);

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
		///       Monday, November 20, 2006
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
				+ IV_StockTakingMasterTable.STOCKTAKINGMASTERID_FLD + ","
				+ IV_StockTakingMasterTable.STOCKTAKINGPERIODID_FLD + ","
				+ IV_StockTakingMasterTable.DEPARTMENTID_FLD + ","
				+ IV_StockTakingMasterTable.PRODUCTIONLINEID_FLD + ","
				+ IV_StockTakingMasterTable.LOCATIONID_FLD + ","
				+ IV_StockTakingMasterTable.BINID_FLD;


				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				odadPCS.SelectCommand = new OleDbCommand(strSql, oconPCS);
				odcbPCS = new OleDbCommandBuilder(odadPCS);
				pData.EnforceConstraints = false;
				odadPCS.Update(pData,IV_StockTakingMasterTable.TABLE_NAME);

			}
			catch(OleDbException ex)
			{
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME,ex);
			}			

			catch(InvalidOperationException ex)
			{
				throw new PCSDBException(ErrorCode.DUPLICATE_KEY, METHOD_NAME, ex);
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
