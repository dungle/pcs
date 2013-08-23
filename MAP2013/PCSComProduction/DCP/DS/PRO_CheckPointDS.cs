using System;
using System.Data;
using System.Data.OleDb;
using System.Collections;
using System.Configuration;
using PCSComUtils.DataAccess;
using PCSComUtils.PCSExc;
using PCSComUtils.Common;

namespace PCSComProduction.DCP.DS
{
	public class PRO_CheckPointDS 
	{
		public PRO_CheckPointDS()
		{
		}
		private const string THIS = "PCSComProduction.DCP.DS.PRO_CheckPointDS";

	
		///    <summary>
		///       This method uses to add data to PRO_CheckPoint
		///    </summary>
		///    <Inputs>
		///        PRO_CheckPointVO       
		///    </Inputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <History>
		///       Friday, August 12, 2005
		///    </History>
		public void Add(object pobjObjectVO)
		{
			const string METHOD_NAME = THIS + ".Add()";
			 
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS =null;
			try
			{
				PRO_CheckPointVO objObject = (PRO_CheckPointVO) pobjObjectVO;
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand("", oconPCS);
				
				strSql=	"INSERT INTO PRO_CheckPoint("
				+ PRO_CheckPointTable.PRODUCTID_FLD + ","
				+ PRO_CheckPointTable.CCNID_FLD + ","
				+ PRO_CheckPointTable.WORKCENTERID_FLD + ","
				+ PRO_CheckPointTable.SAMPLEPATTERN_FLD + ","
				+ PRO_CheckPointTable.SAMPLERATE_FLD + ","
				+ PRO_CheckPointTable.DELAYTIME_FLD + ")"
				+ "VALUES(?,?,?,?,?)";

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_CheckPointTable.PRODUCTID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PRO_CheckPointTable.PRODUCTID_FLD].Value = objObject.ProductID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_CheckPointTable.PRODUCTID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PRO_CheckPointTable.CCNID_FLD].Value = objObject.CCNID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_CheckPointTable.WORKCENTERID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PRO_CheckPointTable.WORKCENTERID_FLD].Value = objObject.WorkCenterID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_CheckPointTable.SAMPLEPATTERN_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PRO_CheckPointTable.SAMPLEPATTERN_FLD].Value = objObject.SamplePattern;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_CheckPointTable.SAMPLERATE_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PRO_CheckPointTable.SAMPLERATE_FLD].Value = objObject.SampleRate;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_CheckPointTable.DELAYTIME_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PRO_CheckPointTable.DELAYTIME_FLD].Value = objObject.DelayTime;


				
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
	

	

		///    <summary>
		///       This method uses to add data to PRO_CheckPoint
		///    </summary>
		///    <Inputs>
		///        PRO_CheckPointVO       
		///    </Inputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <History>
		///       Friday, August 12, 2005
		///    </History>

		public void Delete(int pintID)
		{
			const string METHOD_NAME = THIS + ".Delete()";
			string strSql = String.Empty;
			strSql=	"DELETE " + PRO_CheckPointTable.TABLE_NAME + " WHERE  " + "CheckPointID" + "=" + pintID.ToString();
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
	

	

		///    <summary>
		///       This method uses to add data to PRO_CheckPoint
		///    </summary>
		///    <Inputs>
		///        PRO_CheckPointVO       
		///    </Inputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <History>
		///       Friday, August 12, 2005
		///    </History>

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
				+ PRO_CheckPointTable.CHECKPOINTID_FLD + ","
				+ PRO_CheckPointTable.PRODUCTID_FLD + ","
				+ PRO_CheckPointTable.WORKCENTERID_FLD + ","
				+ PRO_CheckPointTable.SAMPLEPATTERN_FLD + ","
				+ PRO_CheckPointTable.SAMPLERATE_FLD + ","
				+ PRO_CheckPointTable.DELAYTIME_FLD
				+ " FROM " + PRO_CheckPointTable.TABLE_NAME
				+" WHERE " + PRO_CheckPointTable.CHECKPOINTID_FLD + "=" + pintID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();

				PRO_CheckPointVO objObject = new PRO_CheckPointVO();

				while (odrPCS.Read())
				{ 
				objObject.CheckPointID = int.Parse(odrPCS[PRO_CheckPointTable.CHECKPOINTID_FLD].ToString().Trim());
				objObject.ProductID = int.Parse(odrPCS[PRO_CheckPointTable.PRODUCTID_FLD].ToString().Trim());
				objObject.WorkCenterID = int.Parse(odrPCS[PRO_CheckPointTable.WORKCENTERID_FLD].ToString().Trim());
				objObject.SamplePattern = int.Parse(odrPCS[PRO_CheckPointTable.SAMPLEPATTERN_FLD].ToString().Trim());
				objObject.SampleRate = Decimal.Parse(odrPCS[PRO_CheckPointTable.SAMPLERATE_FLD].ToString().Trim());
				objObject.DelayTime = Decimal.Parse(odrPCS[PRO_CheckPointTable.DELAYTIME_FLD].ToString().Trim());

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


		///    <summary>
		///       This method uses to add data to PRO_CheckPoint
		///    </summary>
		///    <Inputs>
		///        PRO_CheckPointVO       
		///    </Inputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <History>
		///       Friday, August 12, 2005
		///    </History>
		
	
		public void Update(object pobjObjecVO)
		{
			const string METHOD_NAME = THIS + ".Update()";

			PRO_CheckPointVO objObject = (PRO_CheckPointVO) pobjObjecVO;
			

			//prepare value for parameters
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				strSql=	"UPDATE PRO_CheckPoint SET "
				+ PRO_CheckPointTable.PRODUCTID_FLD + "=   ?" + ","
				+ PRO_CheckPointTable.WORKCENTERID_FLD + "=   ?" + ","
				+ PRO_CheckPointTable.SAMPLEPATTERN_FLD + "=   ?" + ","
				+ PRO_CheckPointTable.SAMPLERATE_FLD + "=   ?" + ","
				+ PRO_CheckPointTable.DELAYTIME_FLD + "=  ?"
				+" WHERE " + PRO_CheckPointTable.CHECKPOINTID_FLD + "= ?";

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_CheckPointTable.PRODUCTID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PRO_CheckPointTable.PRODUCTID_FLD].Value = objObject.ProductID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_CheckPointTable.WORKCENTERID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PRO_CheckPointTable.WORKCENTERID_FLD].Value = objObject.WorkCenterID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_CheckPointTable.SAMPLEPATTERN_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PRO_CheckPointTable.SAMPLEPATTERN_FLD].Value = objObject.SamplePattern;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_CheckPointTable.SAMPLERATE_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PRO_CheckPointTable.SAMPLERATE_FLD].Value = objObject.SampleRate;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_CheckPointTable.DELAYTIME_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PRO_CheckPointTable.DELAYTIME_FLD].Value = objObject.DelayTime;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_CheckPointTable.CHECKPOINTID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PRO_CheckPointTable.CHECKPOINTID_FLD].Value = objObject.CheckPointID;


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


		///    <summary>
		///       This method uses to add data to PRO_CheckPoint
		///    </summary>
		///    <Inputs>
		///        PRO_CheckPointVO       
		///    </Inputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <History>
		///       Friday, August 12, 2005
		///    </History>

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
				+ PRO_CheckPointTable.CHECKPOINTID_FLD + ","
				+ PRO_CheckPointTable.PRODUCTID_FLD + ","
				+ PRO_CheckPointTable.WORKCENTERID_FLD + ","
				+ PRO_CheckPointTable.SAMPLEPATTERN_FLD + ","
				+ PRO_CheckPointTable.SAMPLERATE_FLD + ","
				+ PRO_CheckPointTable.DELAYTIME_FLD
					+ " FROM " + PRO_CheckPointTable.TABLE_NAME;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS,PRO_CheckPointTable.TABLE_NAME);

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


		///    <summary>
		///       This method uses to add data to PRO_CheckPoint
		///    </summary>
		///    <Inputs>
		///        PRO_CheckPointVO       
		///    </Inputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <History>
		///       Friday, August 12, 2005
		///    </History>
		public DataSet ListByCCNID(int pintCCNID)
		{
			const string METHOD_NAME = THIS + ".ListByCCNID()";
			DataSet dstPCS = new DataSet();

			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = "SELECT "
					+ "ITR."+ MST_WorkCenterTable.CODE_FLD + " As " + MST_WorkCenterTable.TABLE_NAME + MST_WorkCenterTable.CODE_FLD + ","
					+ "CK."+ PRO_CheckPointTable.CCNID_FLD + ","
					+ PRO_CheckPointTable.CHECKPOINTID_FLD + ","
					+ "CK."+ PRO_CheckPointTable.PRODUCTID_FLD + ","
					+ "P." + ITM_ProductTable.CODE_FLD + ","
					+ "P." + ITM_ProductTable.DESCRIPTION_FLD + ","
					+ "P." + ITM_ProductTable.REVISION_FLD + ","
					+ "UM." + MST_UnitOfMeasureTable.CODE_FLD + " as " + MST_UnitOfMeasureTable.TABLE_NAME + MST_UnitOfMeasureTable.CODE_FLD + ","
					+ "ITR."+ PRO_CheckPointTable.WORKCENTERID_FLD + ","
					+ PRO_CheckPointTable.SAMPLEPATTERN_FLD + ","
					+ PRO_CheckPointTable.SAMPLERATE_FLD + ","
					+ PRO_CheckPointTable.DELAYTIME_FLD
					+ " FROM " + PRO_CheckPointTable.TABLE_NAME + " CK " 
					+ " INNER JOIN " + ITM_ProductTable.TABLE_NAME + " P on CK." + PRO_CheckPointTable.PRODUCTID_FLD + "=" + "P." + ITM_ProductTable.PRODUCTID_FLD
					+ " INNER JOIN " + MST_UnitOfMeasureTable.TABLE_NAME + " UM on UM." + MST_UnitOfMeasureTable.UNITOFMEASUREID_FLD + "=" + "P." + ITM_ProductTable.STOCKUMID_FLD
					+ " INNER JOIN " + MST_WorkCenterTable.TABLE_NAME + " ITR on ITR." + MST_WorkCenterTable.WORKCENTERID_FLD + "=" + "CK." + PRO_CheckPointTable.WORKCENTERID_FLD
					+ " WHERE CK." + PRO_CheckPointTable.CCNID_FLD + "=" + pintCCNID;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS,PRO_CheckPointTable.TABLE_NAME);

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


		///    <summary>
		///       This method uses to add data to PRO_CheckPoint by CCNID, WorkCenterID
		///    </summary>
		///    <Inputs>
		///        PRO_CheckPointVO       
		///    </Inputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <History>
		///       Friday, August 12, 2005
		///    </History>
		public DataSet ListByWorkCenterID(int pintCCNID, int pintWorkCenterID)
		{
			const string METHOD_NAME = THIS + ".ListByWorkCenterID()";
			DataSet dstPCS = new DataSet();

			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = "SELECT "
					+ "ITR."+ MST_WorkCenterTable.CODE_FLD + " As " + MST_WorkCenterTable.TABLE_NAME + MST_WorkCenterTable.CODE_FLD + ","
					+ "CK."+ PRO_CheckPointTable.CCNID_FLD + ","
					+ PRO_CheckPointTable.CHECKPOINTID_FLD + ","
					+ "CK."+ PRO_CheckPointTable.PRODUCTID_FLD + ","
					+ "P." + ITM_ProductTable.CODE_FLD + ","
					+ "P." + ITM_ProductTable.DESCRIPTION_FLD + ","
					+ "P." + ITM_ProductTable.REVISION_FLD + ","
					+ "CA." + ITM_CategoryTable.CODE_FLD + " ITM_CategoryCode,"
					+ "UM." + MST_UnitOfMeasureTable.CODE_FLD + " as " + MST_UnitOfMeasureTable.TABLE_NAME + MST_UnitOfMeasureTable.CODE_FLD + ","
					+ "ITR."+ PRO_CheckPointTable.WORKCENTERID_FLD + ","
					+ PRO_CheckPointTable.SAMPLEPATTERN_FLD + ","
					+ PRO_CheckPointTable.SAMPLERATE_FLD + ","
					+ PRO_CheckPointTable.DELAYTIME_FLD
					+ " FROM " + PRO_CheckPointTable.TABLE_NAME + " CK " 
					+ " INNER JOIN " + ITM_ProductTable.TABLE_NAME + " P on CK." + PRO_CheckPointTable.PRODUCTID_FLD + "=" + "P." + ITM_ProductTable.PRODUCTID_FLD
					+ " INNER JOIN " + MST_UnitOfMeasureTable.TABLE_NAME + " UM on UM." + MST_UnitOfMeasureTable.UNITOFMEASUREID_FLD + "=" + "P." + ITM_ProductTable.STOCKUMID_FLD
					+ " INNER JOIN " + MST_WorkCenterTable.TABLE_NAME + " ITR on ITR." + MST_WorkCenterTable.WORKCENTERID_FLD + "=" + "CK." + PRO_CheckPointTable.WORKCENTERID_FLD
					+ " LEFT JOIN " + ITM_CategoryTable.TABLE_NAME + " CA ON CA." + ITM_CategoryTable.CATEGORYID_FLD + " = P." + ITM_ProductTable.CATEGORYID_FLD
					+ " WHERE CK." + PRO_CheckPointTable.CCNID_FLD + "=" + pintCCNID
					+ " AND CK." + PRO_CheckPointTable.WORKCENTERID_FLD + "=" + pintWorkCenterID;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS,PRO_CheckPointTable.TABLE_NAME);

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


		///    <summary>
		///       This method uses to add data to PRO_CheckPoint
		///    </summary>
		///    <Inputs>
		///        PRO_CheckPointVO       
		///    </Inputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <History>
		///       Friday, August 12, 2005
		///    </History>
		
		public void UpdateDataSet(DataSet pdstData)
		{
			const string METHOD_NAME = THIS + ".UpdateDataSet()";
			string strSql;
			OleDbConnection oconPCS =null;
			OleDbCommandBuilder odcbPCS ;
			OleDbDataAdapter odadPCS = new OleDbDataAdapter();

			try
			{
				strSql=	"SELECT "
				+ PRO_CheckPointTable.CHECKPOINTID_FLD + ","
				+ PRO_CheckPointTable.PRODUCTID_FLD + ","
				+ PRO_CheckPointTable.CCNID_FLD + ","
				+ PRO_CheckPointTable.WORKCENTERID_FLD + ","
				+ PRO_CheckPointTable.SAMPLEPATTERN_FLD + ","
				+ PRO_CheckPointTable.SAMPLERATE_FLD + ","
				+ PRO_CheckPointTable.DELAYTIME_FLD 
				+ "  FROM " + PRO_CheckPointTable.TABLE_NAME;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				odadPCS.SelectCommand = new OleDbCommand(strSql, oconPCS);
				odcbPCS = new OleDbCommandBuilder(odadPCS);
				pdstData.EnforceConstraints = false;
				odadPCS.Update(pdstData,PRO_CheckPointTable.TABLE_NAME);

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
	}
}
