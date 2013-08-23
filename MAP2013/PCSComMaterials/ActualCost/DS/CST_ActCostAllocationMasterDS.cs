using System;
using System.Data;
using System.Data.OleDb;
using System.Collections;
using System.Configuration;


//Using PCS's namespaces
using PCSComUtils.DataAccess;
using PCSComUtils.PCSExc;
using PCSComUtils.Common;

namespace PCSComMaterials.ActualCost.DS
{
	public class CST_ActCostAllocationMasterDS 
	{
		private const string THIS = ".CST_ActCostAllocationMasterDS";

		public CST_ActCostAllocationMasterDS()
		{
		}
	
		//**************************************************************************              
		///    <Description>
		///       This method uses to add data to CST_ActCostAllocationMaster
		///    </Description>
		///    <Inputs>
		///        CST_ActCostAllocationMasterVO       
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
		///       Tuesday, February 21, 2006
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
				CST_ActCostAllocationMasterVO objObject = (CST_ActCostAllocationMasterVO) pobjObjectVO;

				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand("", oconPCS);
				
				strSql=	"INSERT INTO CST_ActCostAllocationMaster("
					+ CST_ActCostAllocationMasterTable.PERIODNAME_FLD + ","
					+ CST_ActCostAllocationMasterTable.FROMDATE_FLD + ","
					+ CST_ActCostAllocationMasterTable.TODATE_FLD + ","
					+ CST_ActCostAllocationMasterTable.CCNID_FLD + ","
					+ CST_ActCostAllocationMasterTable.CURRENCYID_FLD + ","
					+ CST_ActCostAllocationMasterTable.ROLLUPDATE_FLD + ")"
					+ "VALUES(?,?,?,?,?,?)";

				ocmdPCS.Parameters.Add(new OleDbParameter(CST_ActCostAllocationMasterTable.PERIODNAME_FLD, OleDbType.WChar));
				ocmdPCS.Parameters[CST_ActCostAllocationMasterTable.PERIODNAME_FLD].Value = objObject.PeriodName;

				ocmdPCS.Parameters.Add(new OleDbParameter(CST_ActCostAllocationMasterTable.FROMDATE_FLD, OleDbType.Date));
				ocmdPCS.Parameters[CST_ActCostAllocationMasterTable.FROMDATE_FLD].Value = objObject.FromDate;

				ocmdPCS.Parameters.Add(new OleDbParameter(CST_ActCostAllocationMasterTable.TODATE_FLD, OleDbType.Date));
				ocmdPCS.Parameters[CST_ActCostAllocationMasterTable.TODATE_FLD].Value = objObject.ToDate;

				ocmdPCS.Parameters.Add(new OleDbParameter(CST_ActCostAllocationMasterTable.CCNID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[CST_ActCostAllocationMasterTable.CCNID_FLD].Value = objObject.CCNID;

				ocmdPCS.Parameters.Add(new OleDbParameter(CST_ActCostAllocationMasterTable.CURRENCYID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[CST_ActCostAllocationMasterTable.CURRENCYID_FLD].Value = objObject.CurrencyID;

				ocmdPCS.Parameters.Add(new OleDbParameter(CST_ActCostAllocationMasterTable.ROLLUPDATE_FLD, OleDbType.Date));
				ocmdPCS.Parameters[CST_ActCostAllocationMasterTable.ROLLUPDATE_FLD].Value = objObject.RollupDate;


				
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
	
		public int AddAndReturnID(object pobjObjectVO)
		{
			const string METHOD_NAME = THIS + ".AddAndReturnID()";
			 
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;

			try
			{
				CST_ActCostAllocationMasterVO objObject = (CST_ActCostAllocationMasterVO) pobjObjectVO;

				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand("", oconPCS);
				
				strSql=	"INSERT INTO CST_ActCostAllocationMaster("
					+ CST_ActCostAllocationMasterTable.PERIODNAME_FLD + ","
					+ CST_ActCostAllocationMasterTable.FROMDATE_FLD + ","
					+ CST_ActCostAllocationMasterTable.TODATE_FLD + ","
					+ CST_ActCostAllocationMasterTable.CCNID_FLD + ","
					+ CST_ActCostAllocationMasterTable.CURRENCYID_FLD + ","
					+ CST_ActCostAllocationMasterTable.ROLLUPDATE_FLD + ")"
					+ "VALUES(?,?,?,?,?,?)"
					+ "; SELECT @@IDENTITY as LatestID";
					

				ocmdPCS.Parameters.Add(new OleDbParameter(CST_ActCostAllocationMasterTable.PERIODNAME_FLD, OleDbType.WChar));
				ocmdPCS.Parameters[CST_ActCostAllocationMasterTable.PERIODNAME_FLD].Value = objObject.PeriodName;

				ocmdPCS.Parameters.Add(new OleDbParameter(CST_ActCostAllocationMasterTable.FROMDATE_FLD, OleDbType.Date));
				ocmdPCS.Parameters[CST_ActCostAllocationMasterTable.FROMDATE_FLD].Value = objObject.FromDate;

				ocmdPCS.Parameters.Add(new OleDbParameter(CST_ActCostAllocationMasterTable.TODATE_FLD, OleDbType.Date));
				ocmdPCS.Parameters[CST_ActCostAllocationMasterTable.TODATE_FLD].Value = objObject.ToDate;

				ocmdPCS.Parameters.Add(new OleDbParameter(CST_ActCostAllocationMasterTable.CCNID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[CST_ActCostAllocationMasterTable.CCNID_FLD].Value = objObject.CCNID;

				ocmdPCS.Parameters.Add(new OleDbParameter(CST_ActCostAllocationMasterTable.CURRENCYID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[CST_ActCostAllocationMasterTable.CURRENCYID_FLD].Value = objObject.CurrencyID;

				ocmdPCS.Parameters.Add(new OleDbParameter(CST_ActCostAllocationMasterTable.ROLLUPDATE_FLD, OleDbType.Date));

				if(objObject.RollupDate != DateTime.MinValue)
				{
					ocmdPCS.Parameters[CST_ActCostAllocationMasterTable.ROLLUPDATE_FLD].Value = objObject.RollupDate;
				}
				else
				{
					ocmdPCS.Parameters[CST_ActCostAllocationMasterTable.ROLLUPDATE_FLD].Value = DBNull.Value;
				}
				
				ocmdPCS.CommandText = strSql;
				ocmdPCS.Connection.Open();

				object objReturnValue = ocmdPCS.ExecuteScalar();
				if(objReturnValue != null)
				{
					return int.Parse(objReturnValue.ToString());
				}
				else
				{
					return -1;
				}


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
		///       This method uses to delete data from CST_ActCostAllocationMaster
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
			strSql=	"DELETE " + CST_ActCostAllocationMasterTable.TABLE_NAME + " WHERE  " + "ActCostAllocationMasterID" + "=" + pintID.ToString();

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
		///       This method uses to get data from CST_ActCostAllocationMaster
		///    </Description>
		///    <Inputs>
		///        ID       
		///    </Inputs>
		///    <Outputs>
		///       CST_ActCostAllocationMasterVO
		///    </Outputs>
		///    <Returns>
		///       CST_ActCostAllocationMasterVO
		///    </Returns>
		///    <Authors>
		///       HungLa
		///    </Authors>
		///    <History>
		///       Tuesday, February 21, 2006
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
					+ CST_ActCostAllocationMasterTable.ACTCOSTALLOCATIONMASTERID_FLD + ","
					+ CST_ActCostAllocationMasterTable.PERIODNAME_FLD + ","
					+ CST_ActCostAllocationMasterTable.FROMDATE_FLD + ","
					+ CST_ActCostAllocationMasterTable.TODATE_FLD + ","
					+ CST_ActCostAllocationMasterTable.CCNID_FLD + ","
					+ CST_ActCostAllocationMasterTable.CURRENCYID_FLD + ","
					+ CST_ActCostAllocationMasterTable.ROLLUPDATE_FLD
					+ " FROM " + CST_ActCostAllocationMasterTable.TABLE_NAME
					+ " WHERE " + CST_ActCostAllocationMasterTable.ACTCOSTALLOCATIONMASTERID_FLD + "=" + pintID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();

				CST_ActCostAllocationMasterVO objObject = new CST_ActCostAllocationMasterVO();

				if (odrPCS.Read())
				{ 
					objObject.ActCostAllocationMasterID = int.Parse(odrPCS[CST_ActCostAllocationMasterTable.ACTCOSTALLOCATIONMASTERID_FLD].ToString().Trim());
					objObject.PeriodName = odrPCS[CST_ActCostAllocationMasterTable.PERIODNAME_FLD].ToString().Trim();
					objObject.FromDate = DateTime.Parse(odrPCS[CST_ActCostAllocationMasterTable.FROMDATE_FLD].ToString().Trim());
					objObject.ToDate = DateTime.Parse(odrPCS[CST_ActCostAllocationMasterTable.TODATE_FLD].ToString().Trim());
					objObject.CCNID = int.Parse(odrPCS[CST_ActCostAllocationMasterTable.CCNID_FLD].ToString().Trim());
					objObject.CurrencyID = int.Parse(odrPCS[CST_ActCostAllocationMasterTable.CURRENCYID_FLD].ToString().Trim());
					if(!odrPCS[CST_ActCostAllocationMasterTable.ROLLUPDATE_FLD].Equals(DBNull.Value))
					{
						objObject.RollupDate = DateTime.Parse(odrPCS[CST_ActCostAllocationMasterTable.ROLLUPDATE_FLD].ToString().Trim());
					}
					else
					{
						objObject.RollupDate = DateTime.MinValue;
					}

					return objObject;
				}

				return null;
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

		public object GetObjectVO(string pstrCode)
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
					+ CST_ActCostAllocationMasterTable.ACTCOSTALLOCATIONMASTERID_FLD + ","
					+ CST_ActCostAllocationMasterTable.PERIODNAME_FLD + ","
					+ CST_ActCostAllocationMasterTable.FROMDATE_FLD + ","
					+ CST_ActCostAllocationMasterTable.TODATE_FLD + ","
					+ CST_ActCostAllocationMasterTable.CCNID_FLD + ","
					+ CST_ActCostAllocationMasterTable.CURRENCYID_FLD + ","
					+ CST_ActCostAllocationMasterTable.ROLLUPDATE_FLD
					+ " FROM " + CST_ActCostAllocationMasterTable.TABLE_NAME
					+ " WHERE " + CST_ActCostAllocationMasterTable.PERIODNAME_FLD + "='" + pstrCode.Replace("'", "''") + "'";

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();

				CST_ActCostAllocationMasterVO objObject = new CST_ActCostAllocationMasterVO();

				if(odrPCS.Read())
				{ 
					objObject.ActCostAllocationMasterID = int.Parse(odrPCS[CST_ActCostAllocationMasterTable.ACTCOSTALLOCATIONMASTERID_FLD].ToString().Trim());
					objObject.PeriodName = odrPCS[CST_ActCostAllocationMasterTable.PERIODNAME_FLD].ToString().Trim();
					objObject.FromDate = DateTime.Parse(odrPCS[CST_ActCostAllocationMasterTable.FROMDATE_FLD].ToString().Trim());
					objObject.ToDate = DateTime.Parse(odrPCS[CST_ActCostAllocationMasterTable.TODATE_FLD].ToString().Trim());
					objObject.CCNID = int.Parse(odrPCS[CST_ActCostAllocationMasterTable.CCNID_FLD].ToString().Trim());
					objObject.CurrencyID = int.Parse(odrPCS[CST_ActCostAllocationMasterTable.CURRENCYID_FLD].ToString().Trim());
					if(!odrPCS[CST_ActCostAllocationMasterTable.ROLLUPDATE_FLD].Equals(DBNull.Value))
					{
						objObject.RollupDate = DateTime.Parse(odrPCS[CST_ActCostAllocationMasterTable.ROLLUPDATE_FLD].ToString().Trim());
					}
					else
					{
						objObject.RollupDate = DateTime.MinValue;
					}

					return objObject;
				}		

				return null;
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
		///       This method uses to update data to CST_ActCostAllocationMaster
		///    </Description>
		///    <Inputs>
		///       CST_ActCostAllocationMasterVO       
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

			CST_ActCostAllocationMasterVO objObject = (CST_ActCostAllocationMasterVO) pobjObjecVO;
			

			//prepare value for parameters
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				strSql=	"UPDATE CST_ActCostAllocationMaster SET "
					+ CST_ActCostAllocationMasterTable.PERIODNAME_FLD + "=   ?" + ","
					+ CST_ActCostAllocationMasterTable.FROMDATE_FLD + "=   ?" + ","
					+ CST_ActCostAllocationMasterTable.TODATE_FLD + "=   ?" + ","
					+ CST_ActCostAllocationMasterTable.CCNID_FLD + "=   ?" + ","
					+ CST_ActCostAllocationMasterTable.CURRENCYID_FLD + "=   ?" + ","
					+ CST_ActCostAllocationMasterTable.ROLLUPDATE_FLD + "=  ?"
					+" WHERE " + CST_ActCostAllocationMasterTable.ACTCOSTALLOCATIONMASTERID_FLD + "= ?";

				ocmdPCS.Parameters.Add(new OleDbParameter(CST_ActCostAllocationMasterTable.PERIODNAME_FLD, OleDbType.WChar));
				ocmdPCS.Parameters[CST_ActCostAllocationMasterTable.PERIODNAME_FLD].Value = objObject.PeriodName;

				ocmdPCS.Parameters.Add(new OleDbParameter(CST_ActCostAllocationMasterTable.FROMDATE_FLD, OleDbType.Date));
				ocmdPCS.Parameters[CST_ActCostAllocationMasterTable.FROMDATE_FLD].Value = objObject.FromDate;

				ocmdPCS.Parameters.Add(new OleDbParameter(CST_ActCostAllocationMasterTable.TODATE_FLD, OleDbType.Date));
				ocmdPCS.Parameters[CST_ActCostAllocationMasterTable.TODATE_FLD].Value = objObject.ToDate;

				ocmdPCS.Parameters.Add(new OleDbParameter(CST_ActCostAllocationMasterTable.CCNID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[CST_ActCostAllocationMasterTable.CCNID_FLD].Value = objObject.CCNID;

				ocmdPCS.Parameters.Add(new OleDbParameter(CST_ActCostAllocationMasterTable.CURRENCYID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[CST_ActCostAllocationMasterTable.CURRENCYID_FLD].Value = objObject.CurrencyID;
				
				ocmdPCS.Parameters.Add(new OleDbParameter(CST_ActCostAllocationMasterTable.ROLLUPDATE_FLD, OleDbType.Date));
				if(objObject.RollupDate != DateTime.MinValue)
				{
					ocmdPCS.Parameters[CST_ActCostAllocationMasterTable.ROLLUPDATE_FLD].Value = objObject.RollupDate;
				}
				else
				{
					ocmdPCS.Parameters[CST_ActCostAllocationMasterTable.ROLLUPDATE_FLD].Value = DBNull.Value;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(CST_ActCostAllocationMasterTable.ACTCOSTALLOCATIONMASTERID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[CST_ActCostAllocationMasterTable.ACTCOSTALLOCATIONMASTERID_FLD].Value = objObject.ActCostAllocationMasterID;


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
		///       This method uses to get all data from CST_ActCostAllocationMaster
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
		///       Tuesday, February 21, 2006
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
					+ CST_ActCostAllocationMasterTable.ACTCOSTALLOCATIONMASTERID_FLD + ","
					+ CST_ActCostAllocationMasterTable.PERIODNAME_FLD + ","
					+ CST_ActCostAllocationMasterTable.FROMDATE_FLD + ","
					+ CST_ActCostAllocationMasterTable.TODATE_FLD + ","
					+ CST_ActCostAllocationMasterTable.CCNID_FLD + ","
					+ CST_ActCostAllocationMasterTable.CURRENCYID_FLD + ","
					+ CST_ActCostAllocationMasterTable.ROLLUPDATE_FLD
					+ " FROM " + CST_ActCostAllocationMasterTable.TABLE_NAME;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS,CST_ActCostAllocationMasterTable.TABLE_NAME);

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
		///       HungLa
		///    </Authors>
		///    <History>
		///       Tuesday, February 21, 2006
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
					+ CST_ActCostAllocationMasterTable.ACTCOSTALLOCATIONMASTERID_FLD + ","
					+ CST_ActCostAllocationMasterTable.PERIODNAME_FLD + ","
					+ CST_ActCostAllocationMasterTable.FROMDATE_FLD + ","
					+ CST_ActCostAllocationMasterTable.TODATE_FLD + ","
					+ CST_ActCostAllocationMasterTable.CCNID_FLD + ","
					+ CST_ActCostAllocationMasterTable.CURRENCYID_FLD + ","
					+ CST_ActCostAllocationMasterTable.ROLLUPDATE_FLD 
					+ "  FROM " + CST_ActCostAllocationMasterTable.TABLE_NAME;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				odadPCS.SelectCommand = new OleDbCommand(strSql, oconPCS);
				odcbPCS = new OleDbCommandBuilder(odadPCS);
				pData.EnforceConstraints = false;
				odadPCS.Update(pData,CST_ActCostAllocationMasterTable.TABLE_NAME);

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

		/// <summary>
		/// If 
		/// SELECT FROM MTR_ACDSOptionDetailDS WHERE (pobjMasterVO.FromDate between FromDate, ToDate) OR (pobjMasterVO.ToDate between FromDate, ToDate)
		/// return >= 1 row then return False
		/// else return True
		/// </summary>
		public bool IsPeriodOverlap(object pobjMasterVO)
		{
			const string METHOD_NAME = THIS + ".IsPeriodOverlap()";
						
			//Cast to CST_ActCostAllocationMasterVO object
			CST_ActCostAllocationMasterVO voMaster = (CST_ActCostAllocationMasterVO)pobjMasterVO;
			
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				
				strSql =  "SELECT Count(*) "					
					+ " FROM " + CST_ActCostAllocationMasterTable.TABLE_NAME
					+ " WHERE (" + CST_ActCostAllocationMasterTable.ACTCOSTALLOCATIONMASTERID_FLD + " <> " + voMaster.ActCostAllocationMasterID + ") " 
					+ " AND ((" + CST_ActCostAllocationMasterTable.FROMDATE_FLD + " BETWEEN '" + voMaster.FromDate.ToString("yyyy-MM-dd") + "' AND '" + voMaster.ToDate.ToString("yyyy-MM-dd") + "')"
					+ " OR (" + CST_ActCostAllocationMasterTable.TODATE_FLD + " BETWEEN '" + voMaster.FromDate.ToString("yyyy-MM-dd") + "' AND '" + voMaster.ToDate.ToString("yyyy-MM-dd") + "')"
					+ " OR ('" + voMaster.FromDate.ToString("yyyy-MM-dd") + "' BETWEEN "  + CST_ActCostAllocationMasterTable.FROMDATE_FLD + " AND " + CST_ActCostAllocationMasterTable.TODATE_FLD + ")"
					+ " OR ('" + voMaster.ToDate.ToString("yyyy-MM-dd") + "' BETWEEN "  + CST_ActCostAllocationMasterTable.FROMDATE_FLD + " AND " + CST_ActCostAllocationMasterTable.TODATE_FLD + "))";

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				object objReturn = ocmdPCS.ExecuteScalar();
				if (objReturn == null) 
				{
					return false;
				}
				else
				{
					if (int.Parse(objReturn.ToString()) > 0 )
					{
						return true;
					}
					else
					{
						return false;
					}
				}
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
		/// Gets period by to date
		/// </summary>
		/// <param name="pdtmToDate">To Date of Period</param>
		/// <returns>CST_ActCostAllocationMasterVO object</returns>
		public object GetObjectVO(DateTime pdtmToDate)
		{
			const string METHOD_NAME = THIS + ".GetObjectVO()";
			
			OleDbDataReader odrPCS = null;
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				strSql=	"SELECT TOP 1 "
					+ CST_ActCostAllocationMasterTable.ACTCOSTALLOCATIONMASTERID_FLD + ","
					+ CST_ActCostAllocationMasterTable.PERIODNAME_FLD + ","
					+ CST_ActCostAllocationMasterTable.FROMDATE_FLD + ","
					+ CST_ActCostAllocationMasterTable.TODATE_FLD + ","
					+ CST_ActCostAllocationMasterTable.CCNID_FLD + ","
					+ CST_ActCostAllocationMasterTable.CURRENCYID_FLD + ","
					+ CST_ActCostAllocationMasterTable.ROLLUPDATE_FLD
					+ " FROM " + CST_ActCostAllocationMasterTable.TABLE_NAME
					+ " WHERE " + CST_ActCostAllocationMasterTable.TODATE_FLD + "< ?"
					+ " ORDER BY " + CST_ActCostAllocationMasterTable.TODATE_FLD + " DESC";

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Parameters.Add(new OleDbParameter(CST_ActCostAllocationMasterTable.TODATE_FLD, OleDbType.Date)).Value = pdtmToDate;

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();

				CST_ActCostAllocationMasterVO objObject = new CST_ActCostAllocationMasterVO();

				if (odrPCS.Read())
				{ 
					objObject.ActCostAllocationMasterID = int.Parse(odrPCS[CST_ActCostAllocationMasterTable.ACTCOSTALLOCATIONMASTERID_FLD].ToString().Trim());
					objObject.PeriodName = odrPCS[CST_ActCostAllocationMasterTable.PERIODNAME_FLD].ToString().Trim();
					objObject.FromDate = DateTime.Parse(odrPCS[CST_ActCostAllocationMasterTable.FROMDATE_FLD].ToString().Trim());
					objObject.ToDate = DateTime.Parse(odrPCS[CST_ActCostAllocationMasterTable.TODATE_FLD].ToString().Trim());
					objObject.CCNID = int.Parse(odrPCS[CST_ActCostAllocationMasterTable.CCNID_FLD].ToString().Trim());
					objObject.CurrencyID = int.Parse(odrPCS[CST_ActCostAllocationMasterTable.CURRENCYID_FLD].ToString().Trim());
					if(!odrPCS[CST_ActCostAllocationMasterTable.ROLLUPDATE_FLD].Equals(DBNull.Value))
					{
						objObject.RollupDate = DateTime.Parse(odrPCS[CST_ActCostAllocationMasterTable.ROLLUPDATE_FLD].ToString().Trim());
					}
					else
					{
						objObject.RollupDate = DateTime.MinValue;
					}

					return objObject;
				}

				return null;
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

		public void DelChargeAllocation(int actCostAllocationMasterId)
		{
			const string METHOD_NAME = THIS + ".DelChargeAllocation()";
			string strSql = String.Empty;
			strSql=	"DELETE " + CST_DSAndRecycleAllocationTable.TABLE_NAME + " WHERE  " + "ActCostAllocationMasterID" + "=" + actCostAllocationMasterId.ToString();

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
				if (ex.Errors.Count >= 1)
				{
					if (ex.Errors[1].NativeError == ErrorCode.SQLCASCADE_PREVENT_KEYCODE)
					{										   
						throw new PCSDBException(ErrorCode.CASCADE_DELETE_PREVENT, METHOD_NAME, ex);				
					}
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
	}
}
