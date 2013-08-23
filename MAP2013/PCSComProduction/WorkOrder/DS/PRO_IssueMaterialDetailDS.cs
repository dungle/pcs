using System;
using System.Data;
using System.Data.OleDb;
using System.Collections;
using System.Configuration;
using PCSComUtils.DataAccess;
using PCSComUtils.PCSExc;
using PCSComUtils.Common;

namespace PCSComProduction.WorkOrder.DS
{
	public class PRO_IssueMaterialDetailDS 
	{
		public PRO_IssueMaterialDetailDS()
		{
		}
		private const string THIS = "PCSComProduction.WorkOrder.DS.PRO_IssueMaterialDetailDS";
		///    <summary>
		///       This method uses to add data to PRO_IssueMaterialDetail
		///    </summary>
		///    <Inputs>
		///        PRO_IssueMaterialDetailVO       
		///    </Inputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <History>
		///       Tuesday, June 14, 2005
		///    </History>


		public void Add(object pobjObjectVO)
		{
			const string METHOD_NAME = THIS + ".Add()";
			 
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS =null;
			try
			{
				PRO_IssueMaterialDetailVO objObject = (PRO_IssueMaterialDetailVO) pobjObjectVO;
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand("", oconPCS);
				
				strSql=	"INSERT INTO PRO_IssueMaterialDetail("
				+ PRO_IssueMaterialDetailTable.LINE_FLD + ","
				+ PRO_IssueMaterialDetailTable.COMMITQUANTITY_FLD + ","
				+ PRO_IssueMaterialDetailTable.PRODUCTID_FLD + ","
				+ PRO_IssueMaterialDetailTable.ISSUEMATERIALMASTERID_FLD + ","
				+ PRO_IssueMaterialDetailTable.LOCATIONID_FLD + ","
				+ PRO_IssueMaterialDetailTable.BINID_FLD + ","
				+ PRO_IssueMaterialDetailTable.LOT_FLD + ","
				+ PRO_IssueMaterialDetailTable.SERIAL_FLD + ","
				+ PRO_IssueMaterialDetailTable.MASTERLOCATIONID_FLD + ","
				+ PRO_IssueMaterialDetailTable.STOCKUMID_FLD + ","
				+ PRO_IssueMaterialDetailTable.QASTATUS_FLD + ","
				+ PRO_IssueMaterialDetailTable.WORKORDERMASTERID_FLD + ","
				+ PRO_IssueMaterialDetailTable.WORKORDERDETAILID_FLD + ","
				+ PRO_IssueMaterialDetailTable.BOMQUANTITY_FLD + ")"
				+ "VALUES(?,?,?,?,?,?,?,?,?,?,?,?,?)";

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_IssueMaterialDetailTable.LINE_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PRO_IssueMaterialDetailTable.LINE_FLD].Value = objObject.Line;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_IssueMaterialDetailTable.COMMITQUANTITY_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PRO_IssueMaterialDetailTable.COMMITQUANTITY_FLD].Value = objObject.CommitQuantity;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_IssueMaterialDetailTable.PRODUCTID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PRO_IssueMaterialDetailTable.PRODUCTID_FLD].Value = objObject.ProductID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_IssueMaterialDetailTable.ISSUEMATERIALMASTERID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PRO_IssueMaterialDetailTable.ISSUEMATERIALMASTERID_FLD].Value = objObject.IssueMaterialMasterID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_IssueMaterialDetailTable.LOCATIONID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PRO_IssueMaterialDetailTable.LOCATIONID_FLD].Value = objObject.LocationID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_IssueMaterialDetailTable.BINID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PRO_IssueMaterialDetailTable.BINID_FLD].Value = objObject.BinID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_IssueMaterialDetailTable.LOT_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[PRO_IssueMaterialDetailTable.LOT_FLD].Value = objObject.Lot;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_IssueMaterialDetailTable.SERIAL_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[PRO_IssueMaterialDetailTable.SERIAL_FLD].Value = objObject.Serial;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_IssueMaterialDetailTable.MASTERLOCATIONID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PRO_IssueMaterialDetailTable.MASTERLOCATIONID_FLD].Value = objObject.MasterLocationID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_IssueMaterialDetailTable.STOCKUMID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PRO_IssueMaterialDetailTable.STOCKUMID_FLD].Value = objObject.StockUMID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_IssueMaterialDetailTable.QASTATUS_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PRO_IssueMaterialDetailTable.QASTATUS_FLD].Value = objObject.QAStatus;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_IssueMaterialDetailTable.WORKORDERMASTERID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PRO_IssueMaterialDetailTable.WORKORDERMASTERID_FLD].Value = objObject.WorkOrderMasterID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_IssueMaterialDetailTable.WORKORDERDETAILID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PRO_IssueMaterialDetailTable.WORKORDERDETAILID_FLD].Value = objObject.WorkOrderDetailID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_IssueMaterialDetailTable.BOMQUANTITY_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PRO_IssueMaterialDetailTable.BOMQUANTITY_FLD].Value = objObject.BomQuantity;

				
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
		///       This method uses to add data to PRO_IssueMaterialDetail
		///    </summary>
		///    <Inputs>
		///        PRO_IssueMaterialDetailVO       
		///    </Inputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <History>
		///       Tuesday, June 14, 2005
		///    </History>

		public void Delete(int pintID)
		{
			const string METHOD_NAME = THIS + ".Delete()";
			string strSql = String.Empty;
			strSql=	"DELETE " + PRO_IssueMaterialDetailTable.TABLE_NAME + " WHERE  " + "IssueMaterialDetailID" + "=" + pintID.ToString();
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
	
		
		/// <summary>
		/// Delete detail by master id
		/// </summary>
		/// <param name="pintID"></param>
		public void DeleteByMaster(int pintID)
		{
			const string METHOD_NAME = THIS + ".DeleteByMaster()";
			OleDbConnection oconPCS=null;
			OleDbCommand ocmdPCS =null;
			try
			{
				string strSql=	"DELETE " + PRO_IssueMaterialDetailTable.TABLE_NAME
					+ " WHERE  " + PRO_IssueMaterialDetailTable.ISSUEMATERIALMASTERID_FLD + "=" + pintID.ToString();
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				ocmdPCS.ExecuteNonQuery();	
				ocmdPCS = null;

			}
			catch(OleDbException ex)
			{
				if (ex.Errors.Count > 1)
				{
					if (ex.Errors[1].NativeError == ErrorCode.SQLCASCADE_PREVENT_KEYCODE)
						throw new PCSDBException(ErrorCode.CASCADE_DELETE_PREVENT, METHOD_NAME, ex);
					else
						throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME,ex);
				}
				else
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
		///       This method uses to add data to PRO_IssueMaterialDetail
		///    </summary>
		///    <Inputs>
		///        PRO_IssueMaterialDetailVO       
		///    </Inputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <History>
		///       Tuesday, June 14, 2005
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
				+ PRO_IssueMaterialDetailTable.ISSUEMATERIALDETAILID_FLD + ","
				+ PRO_IssueMaterialDetailTable.LINE_FLD + ","
				+ PRO_IssueMaterialDetailTable.COMMITQUANTITY_FLD + ","
				+ PRO_IssueMaterialDetailTable.PRODUCTID_FLD + ","
				+ PRO_IssueMaterialDetailTable.ISSUEMATERIALMASTERID_FLD + ","
				+ PRO_IssueMaterialDetailTable.LOCATIONID_FLD + ","
				+ PRO_IssueMaterialDetailTable.BINID_FLD + ","
				+ PRO_IssueMaterialDetailTable.LOT_FLD + ","
				+ PRO_IssueMaterialDetailTable.SERIAL_FLD + ","
				+ PRO_IssueMaterialDetailTable.MASTERLOCATIONID_FLD + ","
				+ PRO_IssueMaterialDetailTable.STOCKUMID_FLD + ","
				+ PRO_IssueMaterialDetailTable.QASTATUS_FLD + ","
				+ PRO_IssueMaterialDetailTable.WORKORDERMASTERID_FLD + ","
				+ PRO_IssueMaterialDetailTable.WORKORDERDETAILID_FLD + ","
				+ PRO_IssueMaterialDetailTable.BOMQUANTITY_FLD
				+ " FROM " + PRO_IssueMaterialDetailTable.TABLE_NAME
				+" WHERE " + PRO_IssueMaterialDetailTable.ISSUEMATERIALDETAILID_FLD + "=" + pintID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();

				PRO_IssueMaterialDetailVO objObject = new PRO_IssueMaterialDetailVO();

				while (odrPCS.Read())
				{ 
				objObject.IssueMaterialDetailID = int.Parse(odrPCS[PRO_IssueMaterialDetailTable.ISSUEMATERIALDETAILID_FLD].ToString().Trim());
				objObject.Line = int.Parse(odrPCS[PRO_IssueMaterialDetailTable.LINE_FLD].ToString().Trim());
				objObject.CommitQuantity = Decimal.Parse(odrPCS[PRO_IssueMaterialDetailTable.COMMITQUANTITY_FLD].ToString().Trim());
				objObject.ProductID = int.Parse(odrPCS[PRO_IssueMaterialDetailTable.PRODUCTID_FLD].ToString().Trim());
				objObject.IssueMaterialMasterID = int.Parse(odrPCS[PRO_IssueMaterialDetailTable.ISSUEMATERIALMASTERID_FLD].ToString().Trim());
				objObject.LocationID = int.Parse(odrPCS[PRO_IssueMaterialDetailTable.LOCATIONID_FLD].ToString().Trim());
				objObject.BinID = int.Parse(odrPCS[PRO_IssueMaterialDetailTable.BINID_FLD].ToString().Trim());
				objObject.Lot = odrPCS[PRO_IssueMaterialDetailTable.LOT_FLD].ToString().Trim();
				objObject.Serial = odrPCS[PRO_IssueMaterialDetailTable.SERIAL_FLD].ToString().Trim();
				objObject.MasterLocationID = int.Parse(odrPCS[PRO_IssueMaterialDetailTable.MASTERLOCATIONID_FLD].ToString().Trim());
				objObject.StockUMID = int.Parse(odrPCS[PRO_IssueMaterialDetailTable.STOCKUMID_FLD].ToString().Trim());
				objObject.QAStatus = int.Parse(odrPCS[PRO_IssueMaterialDetailTable.QASTATUS_FLD].ToString().Trim());
				objObject.WorkOrderMasterID = int.Parse(odrPCS[PRO_IssueMaterialDetailTable.WORKORDERMASTERID_FLD].ToString().Trim());
				objObject.WorkOrderDetailID = int.Parse(odrPCS[PRO_IssueMaterialDetailTable.WORKORDERDETAILID_FLD].ToString().Trim());
				objObject.BomQuantity = int.Parse(odrPCS[PRO_IssueMaterialDetailTable.BOMQUANTITY_FLD].ToString().Trim());

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

		/// <summary>
		/// Get Cached QA Status
		/// </summary>
		/// <param name="pintMasterLocationID"></param>
		/// <returns></returns>
		/// <author> Tuan TQ. 8 Mar, 2006</author>
		public DataTable GetCachedQAStatus(int pintCCNID, int pintMasterLocationID)
		{			
			DataTable dtbResult = new DataTable();
			
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			
			string strSql=	"SELECT ISNULL(" + IV_BinCacheTable.INSPSTATUS_FLD + ",0) as "  + IV_BinCacheTable.INSPSTATUS_FLD + ", "
				+ IV_BinCacheTable.LOCATIONID_FLD + ", "
				+ IV_BinCacheTable.BINID_FLD + ", "
				+ IV_BinCacheTable.PRODUCTID_FLD
				+ " FROM " + IV_BinCacheTable.TABLE_NAME 
				+ " WHERE " + IV_BinCacheTable.CCNID_FLD + "=" + pintCCNID				
				+ " AND " + IV_BinCacheTable.MASTERLOCATIONID_FLD + "=" + pintMasterLocationID;

			Utils utils = new Utils();
			oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
			ocmdPCS = new OleDbCommand(strSql, oconPCS);
			ocmdPCS.Connection.Open();
			
			OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
			odadPCS.Fill(dtbResult);

			return dtbResult;	
		}
		
		/// <summary>
		/// Get Available Quantity By Post Date
		/// </summary>
		/// <param name="pintMasterLocationID"></param>
		/// <returns></returns>
		/// <author> Tuan TQ. 8 Mar, 2006</author>
		public DataTable GetAvailableQuantityByPostDate(DateTime pdtmPostDate, int pintCCNID, int pintMasLocID, string pstrProductIDs)
		{
			const string SQL_DATETIME_FORMAT = "yyyy-MM-dd HH:mm";
			DataTable dtbResult = new DataTable();
			
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;

			string strSql =	"SELECT (";
			strSql += " ISNULL(SUM(ISNULL(bc.OHQuantity, 0) - ISNULL(bc.CommitQuantity, 0)), 0)";
			strSql += " -  ISNULL((SELECT ISNULL(SUM(ISNULL(TH.Quantity,0)),0)";
            strSql += " FROM MST_TransactionHistory TH";
            strSql += " INNER JOIN dbo.MST_TranType TT ON TT.TranTypeID = TH.TranTypeID";
            strSql += " WHERE TT.Type = " +  (int)TransactionHistoryType.In;
            strSql += " AND TH.PostDate > '" + pdtmPostDate.ToString(SQL_DATETIME_FORMAT) + "'";
			strSql += " AND TH.CCNID = bc.CCNID";
			strSql += " AND TH.MasterLocationID = bc.MasterLocationID";
	        strSql += " AND TH.LocationID = bc.LocationID";
	        strSql += " AND TH.BinID = bc.BinID";
	        strSql += " AND TH.ProductID = bc.ProductID";
			strSql += " )";
			strSql += " ,0)";

			strSql += " -  ISNULL((SELECT ISNULL(SUM(ISNULL(TH.Quantity,0)),0)";
            strSql += " FROM MST_TransactionHistory TH";
	        strSql += " INNER JOIN dbo.MST_TranType TT ON TT.TranTypeID = TH.TranTypeID";
			strSql += " WHERE TT.Type = 2";
			strSql += " AND TH.PostDate > '" + pdtmPostDate.ToString(SQL_DATETIME_FORMAT) + "'";
			strSql += " AND TH.CCNID = bc.CCNID";
			strSql += " AND TH.MasterLocationID = bc.MasterLocationID";
			strSql += " AND TH.LocationID = bc.LocationID";
			strSql += " AND TH.BinID = bc.BinID";
			strSql += " AND TH.ProductID = bc.ProductID";
			strSql += " )";
			strSql += " ,0)";

			strSql += " +  ISNULL((SELECT ISNULL(SUM(ISNULL(TH.Quantity,0)),0)";
            strSql += " FROM MST_TransactionHistory TH";
	        strSql += " INNER JOIN dbo.MST_TranType TT ON TT.TranTypeID = TH.TranTypeID";
			strSql += " WHERE TT.Type = 0";
		    strSql += " AND TH.PostDate > '" + pdtmPostDate.ToString(SQL_DATETIME_FORMAT) + "'";
			strSql += " AND TH.CCNID = bc.CCNID";
			strSql += " AND TH.MasterLocationID = bc.MasterLocationID";
			strSql += " AND TH.LocationID = bc.LocationID";
			strSql += " AND TH.BinID = bc.BinID";
			strSql += " AND TH.ProductID = bc.ProductID";
		    strSql += " )";
			strSql += " ,0)"; 
			strSql += " )";
			strSql += " as " + Constants.AVAILABLE_QTY_COL;
			strSql += " , bc.ProductID";
			strSql += " , bc.BinID";
			strSql += " , bc.LocationID";

			strSql += " FROM IV_BinCache bc";
			strSql += " WHERE bc.CCNID = " + pintCCNID;
			strSql += " AND bc.MasterLocationID = " + pintMasLocID;
			strSql += " AND bc.ProductID IN " + pstrProductIDs;

			strSql += " GROUP BY bc.ProductID, bc.BinID, bc.LocationID, bc.MasterLocationID, bc.CCNID";

			Utils utils = new Utils();
			oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
			ocmdPCS = new OleDbCommand(strSql, oconPCS);

			ocmdPCS.Connection.Open();
			
			OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);

			odadPCS.Fill(dtbResult);

			return dtbResult;	
		}

		/// <summary>
		/// Get Needed Quantity
		/// </summary>
		/// <param name="pintMasLocID"></param>
		/// <returns></returns>
		/// <author> Tuan TQ. 8 Mar, 2006</author>
		public DataTable GetNeededQuantity(int pintMasLocID)
		{			
			const string VIEW_NEEDED_QUANTITY = "v_RemainComponentForWOIssueWithParentInfo";

			DataTable dtbResult = new DataTable();
			
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;

			string strSql=	"SELECT RequiredQuantity as " + Constants.NEEDED_QTY_COL + ", ";
				strSql += PRO_WorkOrderDetailTable.WORKORDERDETAILID_FLD + ", ";
				strSql += PRO_WorkOrderBomDetailTable.COMPONENTID_FLD;
				strSql += " FROM " + VIEW_NEEDED_QUANTITY;
				strSql += " WHERE " + PRO_WorkOrderMasterTable.MASTERLOCATIONID_FLD + "=" + pintMasLocID;
				

			Utils utils = new Utils();
			oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
			ocmdPCS = new OleDbCommand(strSql, oconPCS);

			ocmdPCS.Connection.Open();
			
			OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);

			odadPCS.Fill(dtbResult);

			return dtbResult;	
		}

		/// <summary>
		/// Get Commited Quantity
		/// </summary>
		/// <param name="pintMasterLocationID"></param>
		/// <returns></returns>
		/// <author> Tuan TQ. 8 Mar, 2006</author>
		public DataTable GetCommitedQuantity(int pintMasterLocationID)
		{			
			DataTable dtbResult = new DataTable();
			
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;

			string strSql = "SELECT SUM("
					+ PRO_IssueMaterialDetailTable.COMMITQUANTITY_FLD + ") as " + PRO_IssueMaterialDetailTable.COMMITQUANTITY_FLD + ", "
					+ PRO_IssueMaterialDetailTable.WORKORDERDETAILID_FLD + ", "
					+ PRO_IssueMaterialDetailTable.PRODUCTID_FLD
					+ " FROM " + PRO_IssueMaterialDetailTable.TABLE_NAME
					+ " WHERE " + PRO_IssueMaterialDetailTable.MASTERLOCATIONID_FLD + "=" + pintMasterLocationID
					+ " GROUP BY "	+ PRO_IssueMaterialDetailTable.WORKORDERDETAILID_FLD + ", "
					+ PRO_IssueMaterialDetailTable.PRODUCTID_FLD;

			Utils utils = new Utils();
			oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
			ocmdPCS = new OleDbCommand(strSql, oconPCS);

			ocmdPCS.Connection.Open();
			
			OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);

			odadPCS.Fill(dtbResult);

			return dtbResult;	
		}

		/// <summary>
		/// This method is used to calculate the commited quantity for this work order 
		/// </summary>
		/// <param name="pintWorkOrderDetailID"></param>
		/// <param name="pintProductID"></param>
		/// <returns></returns>
		public decimal CalculateCommitedQty(int pintWorkOrderDetailID, int pintProductID)
		{
			const string METHOD_NAME = THIS + ".CalculateCommitedQty()";
			DataSet dstPCS = new DataSet();
			
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				strSql=	"SELECT sum("
					+ PRO_IssueMaterialDetailTable.COMMITQUANTITY_FLD + ")"
					+ " FROM " + PRO_IssueMaterialDetailTable.TABLE_NAME
					+ " WHERE " + PRO_IssueMaterialDetailTable.WORKORDERDETAILID_FLD + "=" + pintWorkOrderDetailID
					+ " AND " + PRO_IssueMaterialDetailTable.PRODUCTID_FLD + "=" + pintProductID;


				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();

				object objResult = ocmdPCS.ExecuteScalar();
				if (objResult == DBNull.Value)
				{
					return 0;
				}
				else
				{
					return decimal.Parse(objResult.ToString());
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

		///    <summary>
		///       This method uses to add data to PRO_IssueMaterialDetail
		///    </summary>
		///    <Inputs>
		///        PRO_IssueMaterialDetailVO       
		///    </Inputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <History>
		///       Tuesday, June 14, 2005
		///    </History>
		
	
		public void Update(object pobjObjecVO)
		{
			const string METHOD_NAME = THIS + ".Update()";

			PRO_IssueMaterialDetailVO objObject = (PRO_IssueMaterialDetailVO) pobjObjecVO;
			

			//prepare value for parameters
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				strSql=	"UPDATE PRO_IssueMaterialDetail SET "
				+ PRO_IssueMaterialDetailTable.LINE_FLD + "=   ?" + ","
				+ PRO_IssueMaterialDetailTable.COMMITQUANTITY_FLD + "=   ?" + ","
				+ PRO_IssueMaterialDetailTable.PRODUCTID_FLD + "=   ?" + ","
				+ PRO_IssueMaterialDetailTable.ISSUEMATERIALMASTERID_FLD + "=   ?" + ","
				+ PRO_IssueMaterialDetailTable.LOCATIONID_FLD + "=   ?" + ","
				+ PRO_IssueMaterialDetailTable.BINID_FLD + "=   ?" + ","
				+ PRO_IssueMaterialDetailTable.LOT_FLD + "=   ?" + ","
				+ PRO_IssueMaterialDetailTable.SERIAL_FLD + "=   ?" + ","
				+ PRO_IssueMaterialDetailTable.MASTERLOCATIONID_FLD + "=   ?" + ","
				+ PRO_IssueMaterialDetailTable.STOCKUMID_FLD + "=   ?" + ","
				+ PRO_IssueMaterialDetailTable.QASTATUS_FLD + "=   ?" + ","
				+ PRO_IssueMaterialDetailTable.WORKORDERMASTERID_FLD + "=   ?" + ","
				+ PRO_IssueMaterialDetailTable.WORKORDERDETAILID_FLD + "=  ?"
				+ PRO_IssueMaterialDetailTable.BOMQUANTITY_FLD + "=  ?"
				+" WHERE " + PRO_IssueMaterialDetailTable.ISSUEMATERIALDETAILID_FLD + "= ?";

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_IssueMaterialDetailTable.LINE_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PRO_IssueMaterialDetailTable.LINE_FLD].Value = objObject.Line;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_IssueMaterialDetailTable.COMMITQUANTITY_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PRO_IssueMaterialDetailTable.COMMITQUANTITY_FLD].Value = objObject.CommitQuantity;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_IssueMaterialDetailTable.PRODUCTID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PRO_IssueMaterialDetailTable.PRODUCTID_FLD].Value = objObject.ProductID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_IssueMaterialDetailTable.ISSUEMATERIALMASTERID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PRO_IssueMaterialDetailTable.ISSUEMATERIALMASTERID_FLD].Value = objObject.IssueMaterialMasterID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_IssueMaterialDetailTable.LOCATIONID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PRO_IssueMaterialDetailTable.LOCATIONID_FLD].Value = objObject.LocationID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_IssueMaterialDetailTable.BINID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PRO_IssueMaterialDetailTable.BINID_FLD].Value = objObject.BinID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_IssueMaterialDetailTable.LOT_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[PRO_IssueMaterialDetailTable.LOT_FLD].Value = objObject.Lot;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_IssueMaterialDetailTable.SERIAL_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[PRO_IssueMaterialDetailTable.SERIAL_FLD].Value = objObject.Serial;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_IssueMaterialDetailTable.MASTERLOCATIONID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PRO_IssueMaterialDetailTable.MASTERLOCATIONID_FLD].Value = objObject.MasterLocationID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_IssueMaterialDetailTable.STOCKUMID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PRO_IssueMaterialDetailTable.STOCKUMID_FLD].Value = objObject.StockUMID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_IssueMaterialDetailTable.QASTATUS_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PRO_IssueMaterialDetailTable.QASTATUS_FLD].Value = objObject.QAStatus;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_IssueMaterialDetailTable.WORKORDERMASTERID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PRO_IssueMaterialDetailTable.WORKORDERMASTERID_FLD].Value = objObject.WorkOrderMasterID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_IssueMaterialDetailTable.WORKORDERDETAILID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PRO_IssueMaterialDetailTable.WORKORDERDETAILID_FLD].Value = objObject.WorkOrderDetailID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_IssueMaterialDetailTable.ISSUEMATERIALDETAILID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PRO_IssueMaterialDetailTable.ISSUEMATERIALDETAILID_FLD].Value = objObject.IssueMaterialDetailID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_IssueMaterialDetailTable.BOMQUANTITY_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PRO_IssueMaterialDetailTable.BOMQUANTITY_FLD].Value = objObject.BomQuantity;


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
		///       This method uses to add data to PRO_IssueMaterialDetail
		///    </summary>
		///    <Inputs>
		///        PRO_IssueMaterialDetailVO       
		///    </Inputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <History>
		///       Tuesday, June 14, 2005
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
				+ PRO_IssueMaterialDetailTable.ISSUEMATERIALDETAILID_FLD + ","
				+ PRO_IssueMaterialDetailTable.LINE_FLD + ","
				+ PRO_IssueMaterialDetailTable.COMMITQUANTITY_FLD + ","
				+ PRO_IssueMaterialDetailTable.PRODUCTID_FLD + ","
				+ PRO_IssueMaterialDetailTable.ISSUEMATERIALMASTERID_FLD + ","
				+ PRO_IssueMaterialDetailTable.LOCATIONID_FLD + ","
				+ PRO_IssueMaterialDetailTable.BINID_FLD + ","
				+ PRO_IssueMaterialDetailTable.LOT_FLD + ","
				+ PRO_IssueMaterialDetailTable.SERIAL_FLD + ","
				+ PRO_IssueMaterialDetailTable.MASTERLOCATIONID_FLD + ","
				+ PRO_IssueMaterialDetailTable.STOCKUMID_FLD + ","
				+ PRO_IssueMaterialDetailTable.QASTATUS_FLD + ","
				+ PRO_IssueMaterialDetailTable.WORKORDERMASTERID_FLD + ","
				+ PRO_IssueMaterialDetailTable.WORKORDERDETAILID_FLD + ","
				+ PRO_IssueMaterialDetailTable.BOMQUANTITY_FLD
					+ " FROM " + PRO_IssueMaterialDetailTable.TABLE_NAME;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS,PRO_IssueMaterialDetailTable.TABLE_NAME);

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
		///       This method uses to add data to PRO_IssueMaterialDetail
		///    </summary>
		///    <Inputs>
		///        PRO_IssueMaterialDetailVO       
		///    </Inputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <History>
		///       Tuesday, June 14, 2005
		///    </History>

		public DataSet GetDetailData(int pintMasterID)
		{
			const string METHOD_NAME = THIS + ".GetDetailData()";
			const string VIEW_PRO_ISSUEMATERIAL_DETAIL = "v_PRO_IssueMaterialDetail";
			DataSet dstPCS = new DataSet();
			
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				
				strSql=	"SELECT * "
					+ " FROM " + VIEW_PRO_ISSUEMATERIAL_DETAIL
					+ " WHERE " + PRO_IssueMaterialDetailTable.ISSUEMATERIALMASTERID_FLD + "=" + pintMasterID;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS,PRO_IssueMaterialDetailTable.TABLE_NAME);

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
		///       This method uses to add data to PRO_IssueMaterialDetail
		///    </summary>
		///    <Inputs>
		///        PRO_IssueMaterialDetailVO       
		///    </Inputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <History>
		///       Tuesday, June 14, 2005
		///    </History>
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
				+ PRO_IssueMaterialDetailTable.ISSUEMATERIALDETAILID_FLD + ","
				+ PRO_IssueMaterialDetailTable.LINE_FLD + ","
				+ PRO_IssueMaterialDetailTable.COMMITQUANTITY_FLD + ","
				+ PRO_IssueMaterialDetailTable.PRODUCTID_FLD + ","
				+ PRO_IssueMaterialDetailTable.ISSUEMATERIALMASTERID_FLD + ","
				+ PRO_IssueMaterialDetailTable.LOCATIONID_FLD + ","
				+ PRO_IssueMaterialDetailTable.BINID_FLD + ","
				+ PRO_IssueMaterialDetailTable.LOT_FLD + ","
				+ PRO_IssueMaterialDetailTable.SERIAL_FLD + ","
				+ PRO_IssueMaterialDetailTable.MASTERLOCATIONID_FLD + ","
				+ PRO_IssueMaterialDetailTable.STOCKUMID_FLD + ","
				+ PRO_IssueMaterialDetailTable.QASTATUS_FLD + ","
				+ PRO_IssueMaterialDetailTable.WORKORDERMASTERID_FLD + ","
				+ PRO_IssueMaterialDetailTable.AVAILABLEQUANTITY_FLD + ","
				+ PRO_IssueMaterialDetailTable.WORKORDERDETAILID_FLD + ","
				+ PRO_IssueMaterialDetailTable.BOMQUANTITY_FLD
				+ "  FROM " + PRO_IssueMaterialDetailTable.TABLE_NAME;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				odadPCS.SelectCommand = new OleDbCommand(strSql, oconPCS);
				odcbPCS = new OleDbCommandBuilder(odadPCS);
				pData.EnforceConstraints = false;
				odadPCS.Update(pData,PRO_IssueMaterialDetailTable.TABLE_NAME);

			}
			catch(OleDbException ex)
			{
				if(ex.Errors.Count >= 2)
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
		/// GetAvailableQuantity
		/// </summary>
		/// <param name="pintProductID"></param>
		/// <param name="pintWODetailID"></param>
		/// <returns></returns>
		/// <author>Trada</author>
		/// <date>Wednesday, June 29 2005</date>
		public decimal GetAvailableQuantity(int pintProductID, int pintWODetailID)
		{
			const string METHOD_NAME = THIS + ".GetAvailableQuantity()";
			const string AVAILABLE_QUANTITY = "AvailableQuantity";
			const string V_COMPONENTSCRAP = "V_ComponentScrap";
			DataSet dstPCS = new DataSet();
			
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql =	" SELECT " + AVAILABLE_QUANTITY  
							+ " FROM " + V_COMPONENTSCRAP  
							+ " WHERE " + PRO_WorkOrderDetailTable.WORKORDERDETAILID_FLD + " = " + pintWODetailID.ToString()
							+ " AND " + PRO_ComponentScrapDetailTable.COMPONENTID_FLD + " = " + pintProductID.ToString();
				
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();

				object objResult = ocmdPCS.ExecuteScalar();
				if (objResult == DBNull.Value)
				{
					return 0;
				}
				else
				{
					return decimal.Parse(objResult.ToString());
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
		/// Get committed quantity of a product
		/// </summary>
		/// <param name="pintWorkOderMasterID">Work Order Master</param>
		/// <param name="pintWorkOrderDetailID">Work Order Detail</param>
		/// <param name="pintProductID">Product</param>
		/// <param name="pintMasterLocationID">Master Location</param>
		/// <param name="pintLocationID">Location</param>
		/// <param name="pintBinID">Bin</param>
		/// <param name="pstrLot">Lot</param>
		/// <param name="pstrSerial">Serial</param>
		/// <returns>Committed Quantity</returns>
		public decimal GetCommittedQuatity(int pintWorkOderMasterID, int pintWorkOrderDetailID, int pintProductID, int pintMasterLocationID, int pintLocationID, int pintBinID, string pstrLot, string pstrSerial)
		{
			const string METHOD_NAME = THIS + ".GetCommittedQuatity()";
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql =	"SELECT ISNULL(SUM(ISNULL(CommitQuantity,0)), 0) FROM PRO_IssueMaterialDetail"
					+ " WHERE WorkOrderMasterID = " + pintWorkOderMasterID
					+ " AND WorkOrderDetailID = " + pintWorkOrderDetailID
					+ " AND ProductID = " + pintProductID
					+ " AND MasterLocationID = " + pintMasterLocationID
					+ " AND LocationID = " + pintLocationID;
				if (pintBinID > 0)
					strSql += " AND BinID = " + pintBinID;
				if (pstrLot != null && pstrLot != string.Empty)
					strSql += " AND Lot = ?";
				if (pstrSerial != null && pstrSerial != string.Empty)
					strSql += " AND Serial = ?";
				
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				if (pstrLot != null && pstrLot != string.Empty)
				{
					ocmdPCS.Parameters.Add(new OleDbParameter(PRO_IssueMaterialDetailTable.LOT_FLD, OleDbType.VarWChar));
					ocmdPCS.Parameters[PRO_IssueMaterialDetailTable.LOT_FLD].Value = pstrLot;
				}
				if (pstrSerial != null && pstrSerial != string.Empty)
				{
					ocmdPCS.Parameters.Add(new OleDbParameter(PRO_IssueMaterialDetailTable.SERIAL_FLD, OleDbType.VarWChar));
					ocmdPCS.Parameters[PRO_IssueMaterialDetailTable.SERIAL_FLD].Value = pstrSerial;
				}

				ocmdPCS.CommandText = strSql;
				ocmdPCS.Connection.Open();

				try
				{
					return decimal.Parse(ocmdPCS.ExecuteScalar().ToString());
				}
				catch
				{
					return decimal.Zero;
				}
			}
			catch(OleDbException ex)
			{			
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
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
		/// Recalculate remainquantity after complete WOLine
		/// </summary>
		/// <param name="pintWOIssueID"></param>
		/// <param name="pdecSubtractQty"></param>
		/// <returns></returns>
		public void UpdateRemainQuantity(int pintWOIssueID, decimal pdecSubtractQty)
		{
			const string METHOD_NAME = THIS + ".UpdateRemainQuantity()";

			//prepare value for parameters
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				strSql=	"UPDATE PRO_IssueMaterialDetail SET "
					+ "CompletedQuantity" + "= isnull(CompletedQuantity,0) + ?" + ""
					+" WHERE " + PRO_IssueMaterialDetailTable.ISSUEMATERIALDETAILID_FLD + "= ?";

				ocmdPCS.Parameters.Add(new OleDbParameter("CompletedQuantity", OleDbType.Decimal));
				ocmdPCS.Parameters["CompletedQuantity"].Value = pdecSubtractQty;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_IssueMaterialDetailTable.ISSUEMATERIALDETAILID_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[PRO_IssueMaterialDetailTable.ISSUEMATERIALDETAILID_FLD].Value = pintWOIssueID;

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
	}
}
