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
	public class MTR_MRPCycleOptionDetailDS 
	{
		public MTR_MRPCycleOptionDetailDS()
		{
		}
		private const string THIS = "PCSComMaterials.Plan.DS.MTR_MRPCycleOptionDetailDS";
		///    <summary>
		///       This method uses to add data to MTR_MRPCycleOptionDetail
		///    </summary>
		///    <Inputs>
		///        MTR_MRPCycleOptionDetailVO       
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
				MTR_MRPCycleOptionDetailVO objObject = (MTR_MRPCycleOptionDetailVO) pobjObjectVO;
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand("", oconPCS);
				
				strSql=	"INSERT INTO MTR_MRPCycleOptionDetail("
					+ MTR_MRPCycleOptionDetailTable.ONHAND_FLD + ","
					+ MTR_MRPCycleOptionDetailTable.PURCHASEORDER_FLD + ","
					+ MTR_MRPCycleOptionDetailTable.SALEORDER_FLD + ","
					+ MTR_MRPCycleOptionDetailTable.DEMANDWO_FLD + ","
					+ MTR_MRPCycleOptionDetailTable.MRPCYCLEOPTIONMASTERID_FLD + ","
					+ MTR_MRPCycleOptionDetailTable.MASTERLOCATIONID_FLD + ")"
					+ "VALUES(?,?,?,?,?,?)";

				ocmdPCS.Parameters.Add(new OleDbParameter(MTR_MRPCycleOptionDetailTable.ONHAND_FLD, OleDbType.Boolean));
				ocmdPCS.Parameters[MTR_MRPCycleOptionDetailTable.ONHAND_FLD].Value = objObject.OnHand;

				ocmdPCS.Parameters.Add(new OleDbParameter(MTR_MRPCycleOptionDetailTable.PURCHASEORDER_FLD, OleDbType.Boolean));
				ocmdPCS.Parameters[MTR_MRPCycleOptionDetailTable.PURCHASEORDER_FLD].Value = objObject.PurchaseOrder;

				ocmdPCS.Parameters.Add(new OleDbParameter(MTR_MRPCycleOptionDetailTable.SALEORDER_FLD, OleDbType.Boolean));
				ocmdPCS.Parameters[MTR_MRPCycleOptionDetailTable.SALEORDER_FLD].Value = objObject.SaleOrder;

				ocmdPCS.Parameters.Add(new OleDbParameter(MTR_MRPCycleOptionDetailTable.DEMANDWO_FLD, OleDbType.Boolean));
				ocmdPCS.Parameters[MTR_MRPCycleOptionDetailTable.DEMANDWO_FLD].Value = objObject.DemandWO;

				ocmdPCS.Parameters.Add(new OleDbParameter(MTR_MRPCycleOptionDetailTable.MRPCYCLEOPTIONMASTERID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[MTR_MRPCycleOptionDetailTable.MRPCYCLEOPTIONMASTERID_FLD].Value = objObject.MRPCycleOptionMasterID;

				ocmdPCS.Parameters.Add(new OleDbParameter(MTR_MRPCycleOptionDetailTable.MASTERLOCATIONID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[MTR_MRPCycleOptionDetailTable.MASTERLOCATIONID_FLD].Value = objObject.MasterLocationID;


				
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
		///       This method uses to add data to MTR_MRPCycleOptionDetail
		///    </summary>
		///    <Inputs>
		///        MTR_MRPCycleOptionDetailVO       
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
			strSql=	"DELETE " + MTR_MRPCycleOptionDetailTable.TABLE_NAME + " WHERE  " + "MRPCycleOptionDetailID" + "=" + pintID.ToString();
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
		///       This method uses to add data to MTR_MRPCycleOptionDetail
		///    </summary>
		///    <Inputs>
		///        MTR_MRPCycleOptionDetailVO       
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
					+ MTR_MRPCycleOptionDetailTable.MRPCYCLEOPTIONDETAILID_FLD + ","
					+ MTR_MRPCycleOptionDetailTable.ONHAND_FLD + ","
					+ MTR_MRPCycleOptionDetailTable.PURCHASEORDER_FLD + ","
					+ MTR_MRPCycleOptionDetailTable.SALEORDER_FLD + ","
					+ MTR_MRPCycleOptionDetailTable.DEMANDWO_FLD + ","
					+ MTR_MRPCycleOptionDetailTable.MRPCYCLEOPTIONMASTERID_FLD + ","
					+ MTR_MRPCycleOptionDetailTable.MASTERLOCATIONID_FLD
					+ " FROM " + MTR_MRPCycleOptionDetailTable.TABLE_NAME
					+" WHERE " + MTR_MRPCycleOptionDetailTable.MRPCYCLEOPTIONDETAILID_FLD + "=" + pintID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();

				MTR_MRPCycleOptionDetailVO objObject = new MTR_MRPCycleOptionDetailVO();

				while (odrPCS.Read())
				{ 
					objObject.MRPCycleOptionDetailID = int.Parse(odrPCS[MTR_MRPCycleOptionDetailTable.MRPCYCLEOPTIONDETAILID_FLD].ToString().Trim());
					objObject.OnHand = bool.Parse(odrPCS[MTR_MRPCycleOptionDetailTable.ONHAND_FLD].ToString().Trim());
					objObject.PurchaseOrder = bool.Parse(odrPCS[MTR_MRPCycleOptionDetailTable.PURCHASEORDER_FLD].ToString().Trim());
					objObject.SaleOrder = bool.Parse(odrPCS[MTR_MRPCycleOptionDetailTable.SALEORDER_FLD].ToString().Trim());
					objObject.DemandWO = bool.Parse(odrPCS[MTR_MRPCycleOptionDetailTable.DEMANDWO_FLD].ToString().Trim());
					objObject.MRPCycleOptionMasterID = int.Parse(odrPCS[MTR_MRPCycleOptionDetailTable.MRPCYCLEOPTIONMASTERID_FLD].ToString().Trim());
					objObject.MasterLocationID = int.Parse(odrPCS[MTR_MRPCycleOptionDetailTable.MASTERLOCATIONID_FLD].ToString().Trim());

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
		///       This method uses to add data to MTR_MRPCycleOptionDetail
		///    </summary>
		///    <Inputs>
		///        MTR_MRPCycleOptionDetailVO       
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

			MTR_MRPCycleOptionDetailVO objObject = (MTR_MRPCycleOptionDetailVO) pobjObjecVO;
			

			//prepare value for parameters
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				strSql=	"UPDATE MTR_MRPCycleOptionDetail SET "
					+ MTR_MRPCycleOptionDetailTable.ONHAND_FLD + "=   ?" + ","
					+ MTR_MRPCycleOptionDetailTable.PURCHASEORDER_FLD + "=   ?" + ","
					+ MTR_MRPCycleOptionDetailTable.SALEORDER_FLD + "=   ?" + ","
					+ MTR_MRPCycleOptionDetailTable.DEMANDWO_FLD + "=   ?" + ","
					+ MTR_MRPCycleOptionDetailTable.MRPCYCLEOPTIONMASTERID_FLD + "=   ?" + ","
					+ MTR_MRPCycleOptionDetailTable.MASTERLOCATIONID_FLD + "=  ?"
					+" WHERE " + MTR_MRPCycleOptionDetailTable.MRPCYCLEOPTIONDETAILID_FLD + "= ?";

				ocmdPCS.Parameters.Add(new OleDbParameter(MTR_MRPCycleOptionDetailTable.ONHAND_FLD, OleDbType.Boolean));
				ocmdPCS.Parameters[MTR_MRPCycleOptionDetailTable.ONHAND_FLD].Value = objObject.OnHand;

				ocmdPCS.Parameters.Add(new OleDbParameter(MTR_MRPCycleOptionDetailTable.PURCHASEORDER_FLD, OleDbType.Boolean));
				ocmdPCS.Parameters[MTR_MRPCycleOptionDetailTable.PURCHASEORDER_FLD].Value = objObject.PurchaseOrder;

				ocmdPCS.Parameters.Add(new OleDbParameter(MTR_MRPCycleOptionDetailTable.SALEORDER_FLD, OleDbType.Boolean));
				ocmdPCS.Parameters[MTR_MRPCycleOptionDetailTable.SALEORDER_FLD].Value = objObject.SaleOrder;

				ocmdPCS.Parameters.Add(new OleDbParameter(MTR_MRPCycleOptionDetailTable.DEMANDWO_FLD, OleDbType.Boolean));
				ocmdPCS.Parameters[MTR_MRPCycleOptionDetailTable.DEMANDWO_FLD].Value = objObject.DemandWO;

				ocmdPCS.Parameters.Add(new OleDbParameter(MTR_MRPCycleOptionDetailTable.MRPCYCLEOPTIONMASTERID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[MTR_MRPCycleOptionDetailTable.MRPCYCLEOPTIONMASTERID_FLD].Value = objObject.MRPCycleOptionMasterID;

				ocmdPCS.Parameters.Add(new OleDbParameter(MTR_MRPCycleOptionDetailTable.MASTERLOCATIONID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[MTR_MRPCycleOptionDetailTable.MASTERLOCATIONID_FLD].Value = objObject.MasterLocationID;

				ocmdPCS.Parameters.Add(new OleDbParameter(MTR_MRPCycleOptionDetailTable.MRPCYCLEOPTIONDETAILID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[MTR_MRPCycleOptionDetailTable.MRPCYCLEOPTIONDETAILID_FLD].Value = objObject.MRPCycleOptionDetailID;


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
		///       This method uses to add data to MTR_MRPCycleOptionDetail
		///    </summary>
		///    <Inputs>
		///        MTR_MRPCycleOptionDetailVO       
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
					+ MTR_MRPCycleOptionDetailTable.MRPCYCLEOPTIONDETAILID_FLD + ","
					+ MTR_MRPCycleOptionDetailTable.ONHAND_FLD + ","
					+ MTR_MRPCycleOptionDetailTable.PURCHASEORDER_FLD + ","
					+ MTR_MRPCycleOptionDetailTable.DEMANDWO_FLD + ","
					+ MTR_MRPCycleOptionDetailTable.SALEORDER_FLD + ","
					+ MTR_MRPCycleOptionDetailTable.MRPCYCLEOPTIONMASTERID_FLD + ","
					+ MTR_MRPCycleOptionDetailTable.MASTERLOCATIONID_FLD 
					+ "  FROM " + MTR_MRPCycleOptionDetailTable.TABLE_NAME;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS,MTR_MRPCycleOptionDetailTable.TABLE_NAME);

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
		///       This method uses to add data to MTR_MRPCycleOptionDetail
		///    </summary>
		///    <Inputs>
		///        MTR_MRPCycleOptionDetailVO       
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
					+ MTR_MRPCycleOptionDetailTable.MRPCYCLEOPTIONDETAILID_FLD + ","
					+ MTR_MRPCycleOptionDetailTable.ONHAND_FLD + ","
					+ MTR_MRPCycleOptionDetailTable.PURCHASEORDER_FLD + ","
					+ MTR_MRPCycleOptionDetailTable.SALEORDER_FLD + ","
					+ MTR_MRPCycleOptionDetailTable.DEMANDWO_FLD + ","
					+ MTR_MRPCycleOptionDetailTable.MRPCYCLEOPTIONMASTERID_FLD + ","
					+ MTR_MRPCycleOptionDetailTable.MASTERLOCATIONID_FLD
					+ " FROM " + MTR_MRPCycleOptionDetailTable.TABLE_NAME; 
				
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				odadPCS.SelectCommand = new OleDbCommand(strSql, oconPCS);
				odcbPCS = new OleDbCommandBuilder(odadPCS);
				pdstData.EnforceConstraints = false;
				odadPCS.Update(pdstData,MTR_MRPCycleOptionDetailTable.TABLE_NAME);

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
		/// List MRPCycleOptionDetail by  MRPCycleOptionMasterID
		/// </summary>
		/// <param name="pintMRPCycleOptionMasterID"></param>
		/// <returns></returns>
		/// <author>Trada</author>
		/// <date>Thursday, August 11 2005</date>
		public DataSet List(int pintMRPCycleOptionMasterID)
		{
			const string MASTERLOCATIONCODE = "MasterLocationCode";
			const string METHOD_NAME = THIS + ".List()";
			DataSet dstPCS = new DataSet();
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string 	strSql=	"SELECT "
					+ MTR_MRPCycleOptionDetailTable.MRPCYCLEOPTIONDETAILID_FLD + ","
					+ " (SELECT " + MST_MasterLocationTable.CODE_FLD  
					+ " FROM " + MST_MasterLocationTable.TABLE_NAME + " M "
					+ " WHERE M." + MST_MasterLocationTable.MASTERLOCATIONID_FLD + " = " 
					+ "MRP." + MTR_MPSCycleOptionDetailTable.MASTERLOCATIONID_FLD + ") " 
					+ MASTERLOCATIONCODE + ", "
					+ MTR_MRPCycleOptionDetailTable.ONHAND_FLD + ","
					+ MTR_MRPCycleOptionDetailTable.PURCHASEORDER_FLD + ","
					+ MTR_MRPCycleOptionDetailTable.DEMANDWO_FLD + ","
					+ MTR_MRPCycleOptionDetailTable.SALEORDER_FLD + ","
					+ MTR_MRPCycleOptionDetailTable.MRPCYCLEOPTIONMASTERID_FLD + ","
					+ MTR_MRPCycleOptionDetailTable.MASTERLOCATIONID_FLD
					+ " FROM " + MTR_MRPCycleOptionDetailTable.TABLE_NAME + " MRP"
					+ " WHERE " + " MRP."+ MTR_MRPCycleOptionDetailTable.MRPCYCLEOPTIONMASTERID_FLD + " = " + pintMRPCycleOptionMasterID.ToString();
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS,MTR_MRPCycleOptionDetailTable.TABLE_NAME);

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
	}
}
