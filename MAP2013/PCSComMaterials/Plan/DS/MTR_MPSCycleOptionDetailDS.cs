using System;
using System.Data;
using System.Data.OleDb;
using System.Collections;
using System.Configuration;
using PCSComUtils.DataAccess;
using PCSComUtils.PCSExc;
using PCSComUtils.Common;

namespace PCSComMaterials.Plan.DS
{
	public class MTR_MPSCycleOptionDetailDS 
	{
		public MTR_MPSCycleOptionDetailDS()
		{
		}
		private const string THIS = "PCSComMaterials.Plan.DS.MTR_MPSCycleOptionDetailDS";
		///    <summary>
		///       This method uses to add data to MTR_MPSCycleOptionDetail
		///    </summary>
		///    <Inputs>
		///        MTR_MPSCycleOptionDetailVO       
		///    </Inputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <History>
		///       Thursday, July 21, 2005
		///    </History>


		public void Add(object pobjObjectVO)
		{
			const string METHOD_NAME = THIS + ".Add()";
			 
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS =null;
			try
			{
				MTR_MPSCycleOptionDetailVO objObject = (MTR_MPSCycleOptionDetailVO) pobjObjectVO;
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand("", oconPCS);
				
				strSql=	"INSERT INTO MTR_MPSCycleOptionDetail("
					+ MTR_MPSCycleOptionDetailTable.ONHAND_FLD + ","
					+ MTR_MPSCycleOptionDetailTable.PURCHASEORDER_FLD + ","
					+ MTR_MPSCycleOptionDetailTable.SALEORDER_FLD + ","
					+ MTR_MPSCycleOptionDetailTable.DEMANDWO_FLD + ","
					+ MTR_MPSCycleOptionDetailTable.SUPPLYWO_FLD + ","
					+ MTR_MPSCycleOptionDetailTable.SAFETYSTOCK_FLD + ","
					+ MTR_MPSCycleOptionDetailTable.MASTERLOCATIONID_FLD + ","
					+ MTR_MPSCycleOptionDetailTable.MPSCYCLEOPTIONMASTERID_FLD + ")"
					+ "VALUES(?, ?,?,?,?,?,?,?)";

				ocmdPCS.Parameters.Add(new OleDbParameter(MTR_MPSCycleOptionDetailTable.ONHAND_FLD, OleDbType.Boolean));
				ocmdPCS.Parameters[MTR_MPSCycleOptionDetailTable.ONHAND_FLD].Value = objObject.OnHand;

				ocmdPCS.Parameters.Add(new OleDbParameter(MTR_MPSCycleOptionDetailTable.PURCHASEORDER_FLD, OleDbType.Boolean));
				ocmdPCS.Parameters[MTR_MPSCycleOptionDetailTable.PURCHASEORDER_FLD].Value = objObject.PurchaseOrder;

				ocmdPCS.Parameters.Add(new OleDbParameter(MTR_MPSCycleOptionDetailTable.SALEORDER_FLD, OleDbType.Boolean));
				ocmdPCS.Parameters[MTR_MPSCycleOptionDetailTable.SALEORDER_FLD].Value = objObject.SaleOrder;

				ocmdPCS.Parameters.Add(new OleDbParameter(MTR_MPSCycleOptionDetailTable.DEMANDWO_FLD, OleDbType.Boolean));
				ocmdPCS.Parameters[MTR_MPSCycleOptionDetailTable.DEMANDWO_FLD].Value = objObject.DemandWO;
				
				ocmdPCS.Parameters.Add(new OleDbParameter(MTR_MPSCycleOptionDetailTable.SUPPLYWO_FLD, OleDbType.Boolean));
				ocmdPCS.Parameters[MTR_MPSCycleOptionDetailTable.SUPPLYWO_FLD].Value = objObject.SupplyWO;

				ocmdPCS.Parameters.Add(new OleDbParameter(MTR_MPSCycleOptionDetailTable.SAFETYSTOCK_FLD, OleDbType.Boolean));
				ocmdPCS.Parameters[MTR_MPSCycleOptionDetailTable.SAFETYSTOCK_FLD].Value = objObject.SafetyStock;

				ocmdPCS.Parameters.Add(new OleDbParameter(MTR_MPSCycleOptionDetailTable.MASTERLOCATIONID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[MTR_MPSCycleOptionDetailTable.MASTERLOCATIONID_FLD].Value = objObject.MasterLocationID;

				ocmdPCS.Parameters.Add(new OleDbParameter(MTR_MPSCycleOptionDetailTable.MPSCYCLEOPTIONMASTERID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[MTR_MPSCycleOptionDetailTable.MPSCYCLEOPTIONMASTERID_FLD].Value = objObject.MPSCycleOptionMasterID;


				
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
		///       This method uses to add data to MTR_MPSCycleOptionDetail
		///    </summary>
		///    <Inputs>
		///        MTR_MPSCycleOptionDetailVO       
		///    </Inputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <History>
		///       Thursday, July 21, 2005
		///    </History>

		public void Delete(int pintID)
		{
			const string METHOD_NAME = THIS + ".Delete()";
			string strSql = String.Empty;
			strSql=	"DELETE " + MTR_MPSCycleOptionDetailTable.TABLE_NAME + " WHERE  " + "MPSCycleOptionDetailID" + "=" + pintID.ToString();
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
		///       This method uses to add data to MTR_MPSCycleOptionDetail
		///    </summary>
		///    <Inputs>
		///        MTR_MPSCycleOptionDetailVO       
		///    </Inputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <History>
		///       Thursday, July 21, 2005
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
					+ MTR_MPSCycleOptionDetailTable.MPSCYCLEOPTIONDETAILID_FLD + ","
					+ MTR_MPSCycleOptionDetailTable.ONHAND_FLD + ","
					+ MTR_MPSCycleOptionDetailTable.PURCHASEORDER_FLD + ","
					+ MTR_MPSCycleOptionDetailTable.SALEORDER_FLD + ","
					+ MTR_MPSCycleOptionDetailTable.DEMANDWO_FLD + ","
					+ MTR_MPSCycleOptionDetailTable.SUPPLYWO_FLD + ","
					+ MTR_MPSCycleOptionDetailTable.SAFETYSTOCK_FLD + ","
					+ MTR_MPSCycleOptionDetailTable.MASTERLOCATIONID_FLD + ","
					+ MTR_MPSCycleOptionDetailTable.MPSCYCLEOPTIONMASTERID_FLD
					+ " FROM " + MTR_MPSCycleOptionDetailTable.TABLE_NAME
					+" WHERE " + MTR_MPSCycleOptionDetailTable.MPSCYCLEOPTIONDETAILID_FLD + "=" + pintID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();

				MTR_MPSCycleOptionDetailVO objObject = new MTR_MPSCycleOptionDetailVO();

				while (odrPCS.Read())
				{ 
					objObject.MPSCycleOptionDetailID = int.Parse(odrPCS[MTR_MPSCycleOptionDetailTable.MPSCYCLEOPTIONDETAILID_FLD].ToString().Trim());
					objObject.OnHand = bool.Parse(odrPCS[MTR_MPSCycleOptionDetailTable.ONHAND_FLD].ToString().Trim());
					objObject.PurchaseOrder = bool.Parse(odrPCS[MTR_MPSCycleOptionDetailTable.PURCHASEORDER_FLD].ToString().Trim());
					objObject.SaleOrder = bool.Parse(odrPCS[MTR_MPSCycleOptionDetailTable.SALEORDER_FLD].ToString().Trim());
					objObject.DemandWO = bool.Parse(odrPCS[MTR_MPSCycleOptionDetailTable.DEMANDWO_FLD].ToString().Trim());
					objObject.SupplyWO = bool.Parse(odrPCS[MTR_MPSCycleOptionDetailTable.SUPPLYWO_FLD].ToString().Trim());					
					objObject.MasterLocationID = int.Parse(odrPCS[MTR_MPSCycleOptionDetailTable.MASTERLOCATIONID_FLD].ToString().Trim());
					objObject.MPSCycleOptionMasterID = int.Parse(odrPCS[MTR_MPSCycleOptionDetailTable.MPSCYCLEOPTIONMASTERID_FLD].ToString().Trim());
					//HACKED by Tuan TQ. Add safety stock
					objObject.SafetyStock = bool.Parse(odrPCS[MTR_MPSCycleOptionDetailTable.SAFETYSTOCK_FLD].ToString().Trim());
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
		///       This method uses to add data to MTR_MPSCycleOptionDetail
		///    </summary>
		///    <Inputs>
		///        MTR_MPSCycleOptionDetailVO       
		///    </Inputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <History>
		///       Thursday, July 21, 2005
		///    </History>
		
	
		public void Update(object pobjObjecVO)
		{
			const string METHOD_NAME = THIS + ".Update()";

			MTR_MPSCycleOptionDetailVO objObject = (MTR_MPSCycleOptionDetailVO) pobjObjecVO;
			

			//prepare value for parameters
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				strSql=	"UPDATE MTR_MPSCycleOptionDetail SET "
					+ MTR_MPSCycleOptionDetailTable.ONHAND_FLD + "=   ?" + ","
					+ MTR_MPSCycleOptionDetailTable.PURCHASEORDER_FLD + "=   ?" + ","
					+ MTR_MPSCycleOptionDetailTable.SALEORDER_FLD + "=   ?" + ","
					+ MTR_MPSCycleOptionDetailTable.DEMANDWO_FLD + "=   ?" + ","
					+ MTR_MPSCycleOptionDetailTable.SUPPLYWO_FLD + "=   ?" + ","
					+ MTR_MPSCycleOptionDetailTable.SAFETYSTOCK_FLD + "=   ?" + ","
					+ MTR_MPSCycleOptionDetailTable.MASTERLOCATIONID_FLD + "=   ?" + ","
					+ MTR_MPSCycleOptionDetailTable.MPSCYCLEOPTIONMASTERID_FLD + "=  ?"
					+" WHERE " + MTR_MPSCycleOptionDetailTable.MPSCYCLEOPTIONDETAILID_FLD + "= ?";

				ocmdPCS.Parameters.Add(new OleDbParameter(MTR_MPSCycleOptionDetailTable.ONHAND_FLD, OleDbType.Boolean));
				ocmdPCS.Parameters[MTR_MPSCycleOptionDetailTable.ONHAND_FLD].Value = objObject.OnHand;

				ocmdPCS.Parameters.Add(new OleDbParameter(MTR_MPSCycleOptionDetailTable.PURCHASEORDER_FLD, OleDbType.Boolean));
				ocmdPCS.Parameters[MTR_MPSCycleOptionDetailTable.PURCHASEORDER_FLD].Value = objObject.PurchaseOrder;

				ocmdPCS.Parameters.Add(new OleDbParameter(MTR_MPSCycleOptionDetailTable.SALEORDER_FLD, OleDbType.Boolean));
				ocmdPCS.Parameters[MTR_MPSCycleOptionDetailTable.SALEORDER_FLD].Value = objObject.SaleOrder;

				ocmdPCS.Parameters.Add(new OleDbParameter(MTR_MPSCycleOptionDetailTable.DEMANDWO_FLD, OleDbType.Boolean));
				ocmdPCS.Parameters[MTR_MPSCycleOptionDetailTable.DEMANDWO_FLD].Value = objObject.DemandWO;

				ocmdPCS.Parameters.Add(new OleDbParameter(MTR_MPSCycleOptionDetailTable.SUPPLYWO_FLD, OleDbType.Boolean));
				ocmdPCS.Parameters[MTR_MPSCycleOptionDetailTable.SUPPLYWO_FLD].Value = objObject.SupplyWO;

				ocmdPCS.Parameters.Add(new OleDbParameter(MTR_MPSCycleOptionDetailTable.SAFETYSTOCK_FLD, OleDbType.Boolean));
				ocmdPCS.Parameters[MTR_MPSCycleOptionDetailTable.SAFETYSTOCK_FLD].Value = objObject.SafetyStock;

				ocmdPCS.Parameters.Add(new OleDbParameter(MTR_MPSCycleOptionDetailTable.MASTERLOCATIONID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[MTR_MPSCycleOptionDetailTable.MASTERLOCATIONID_FLD].Value = objObject.MasterLocationID;

				ocmdPCS.Parameters.Add(new OleDbParameter(MTR_MPSCycleOptionDetailTable.MPSCYCLEOPTIONMASTERID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[MTR_MPSCycleOptionDetailTable.MPSCYCLEOPTIONMASTERID_FLD].Value = objObject.MPSCycleOptionMasterID;

				ocmdPCS.Parameters.Add(new OleDbParameter(MTR_MPSCycleOptionDetailTable.MPSCYCLEOPTIONDETAILID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[MTR_MPSCycleOptionDetailTable.MPSCYCLEOPTIONDETAILID_FLD].Value = objObject.MPSCycleOptionDetailID;


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
		///       This method uses to add data to MTR_MPSCycleOptionDetail
		///    </summary>
		///    <Inputs>
		///        MTR_MPSCycleOptionDetailVO       
		///    </Inputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <History>
		///       Thursday, July 21, 2005
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
					+ MTR_MPSCycleOptionDetailTable.MPSCYCLEOPTIONDETAILID_FLD + ","
					+ MTR_MPSCycleOptionDetailTable.ONHAND_FLD + ","
					+ MTR_MPSCycleOptionDetailTable.SAFETYSTOCK_FLD + ","
					+ MTR_MPSCycleOptionDetailTable.PURCHASEORDER_FLD + ","
					+ MTR_MPSCycleOptionDetailTable.SALEORDER_FLD + ","
					+ MTR_MPSCycleOptionDetailTable.DEMANDWO_FLD + ","
					+ MTR_MPSCycleOptionDetailTable.SUPPLYWO_FLD + ","					
					+ MTR_MPSCycleOptionDetailTable.MASTERLOCATIONID_FLD + ","
					+ MTR_MPSCycleOptionDetailTable.MPSCYCLEOPTIONMASTERID_FLD
					+ " FROM " + MTR_MPSCycleOptionDetailTable.TABLE_NAME;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS,MTR_MPSCycleOptionDetailTable.TABLE_NAME);

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
		///       This method uses to add data to MTR_MPSCycleOptionDetail
		///    </summary>
		///    <Inputs>
		///        MTR_MPSCycleOptionDetailVO       
		///    </Inputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <History>
		///       Thursday, July 21, 2005
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
					+ MTR_MPSCycleOptionDetailTable.ONHAND_FLD + ","
					+ MTR_MPSCycleOptionDetailTable.PURCHASEORDER_FLD + ","
					+ MTR_MPSCycleOptionDetailTable.MPSCYCLEOPTIONDETAILID_FLD + ","
					+ MTR_MPSCycleOptionDetailTable.SALEORDER_FLD + ","
					+ MTR_MPSCycleOptionDetailTable.DEMANDWO_FLD + ","
					+ MTR_MPSCycleOptionDetailTable.SUPPLYWO_FLD + ","
					+ MTR_MPSCycleOptionDetailTable.SAFETYSTOCK_FLD + ","
					+ MTR_MPSCycleOptionDetailTable.MASTERLOCATIONID_FLD + ","
					+ MTR_MPSCycleOptionDetailTable.MPSCYCLEOPTIONMASTERID_FLD 
					+ "  FROM " + MTR_MPSCycleOptionDetailTable.TABLE_NAME;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				odadPCS.SelectCommand = new OleDbCommand(strSql, oconPCS);
				odcbPCS = new OleDbCommandBuilder(odadPCS);
				pdstData.EnforceConstraints = false;
				odadPCS.Update(pdstData, MTR_MPSCycleOptionDetailTable.TABLE_NAME);

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
		///       This method uses to add data to MTR_MPSCycleOptionDetail by CycleOptionMasterID
		///    
		/// </summary>
		/// <author>Trada</author>
		/// <date>Wednesday, August 10 2005</date>
		public DataSet List(int pintCycleMasterID)
		{
			const string MASTERLOCATIONCODE = "MasterLocationCode";
			const string METHOD_NAME = THIS + ".List()";
			DataSet dstPCS = new DataSet();
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string 	strSql=	"SELECT "
					+ MTR_MPSCycleOptionDetailTable.MPSCYCLEOPTIONDETAILID_FLD + ","
					+ " (SELECT " + MST_MasterLocationTable.CODE_FLD  
					+ " FROM " + MST_MasterLocationTable.TABLE_NAME + " M "
					+ " WHERE M." + MST_MasterLocationTable.MASTERLOCATIONID_FLD + " = " 
					+ "MPS." + MTR_MPSCycleOptionDetailTable.MASTERLOCATIONID_FLD + ") " 
					+ MASTERLOCATIONCODE + ", "
					+ MTR_MPSCycleOptionDetailTable.ONHAND_FLD + ","
					+ MTR_MPSCycleOptionDetailTable.SAFETYSTOCK_FLD + ","
					+ MTR_MPSCycleOptionDetailTable.PURCHASEORDER_FLD + ","
					+ MTR_MPSCycleOptionDetailTable.DEMANDWO_FLD + ","
					+ MTR_MPSCycleOptionDetailTable.SUPPLYWO_FLD + ","					
					+ MTR_MPSCycleOptionDetailTable.SALEORDER_FLD + ","
					+ MTR_MPSCycleOptionDetailTable.MASTERLOCATIONID_FLD + ","
					+ MTR_MPSCycleOptionDetailTable.MPSCYCLEOPTIONMASTERID_FLD
					+ " FROM " + MTR_MPSCycleOptionDetailTable.TABLE_NAME + " MPS"
					+ " WHERE " + "MPS." + MTR_MPSCycleOptionDetailTable.MPSCYCLEOPTIONMASTERID_FLD + " = " + pintCycleMasterID;
				
				Utils utils = new Utils();

				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS,MTR_MPSCycleOptionDetailTable.TABLE_NAME);

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
		/// Get Cycle Detail By Master
		/// </summary>
		/// <param name="pintCycleMasterID">Cycle Master ID</param>
		/// <returns>Cycle Details</returns>
		public DataTable GetDetailByMaster(int pintCycleMasterID)
		{
			const string METHOD_NAME = THIS + ".GetDetailByMaster()";
			DataTable dtbData = new DataTable(MTR_MPSCycleOptionDetailTable.TABLE_NAME);
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string 	strSql=	"SELECT MPSCycleOptionDetailID, OnHand, PurchaseOrder, SaleOrder,"
					+ " DemandWO, SupplyWO, MasterLocationID, MPSCycleOptionMasterID, SafetyStock FROM MTR_MPSCycleOptionDetail"
					+ " WHERE MPSCycleOptionDetailID = " + pintCycleMasterID;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dtbData);

				return dtbData;
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
