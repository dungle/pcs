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
	public class PRO_DCPResultMasterDS 
	{
		public PRO_DCPResultMasterDS()
		{
		}
		private const string THIS = "PCSComProduction.DCP.DS.PRO_DCPResultMasterDS";
		///    <summary>
		///       This method uses to add data to PRO_DCPResultMaster
		///    </summary>
		///    <Inputs>
		///        PRO_DCPResultMasterVO       
		///    </Inputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <History>
		///       Monday, September 05, 2005
		///    </History>


		public void Add(object pobjObjectVO)
		{
			const string METHOD_NAME = THIS + ".Add()";
			 
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS =null;
			try
			{
				PRO_DCPResultMasterVO objObject = (PRO_DCPResultMasterVO) pobjObjectVO;
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand("", oconPCS);
				
				strSql=	"INSERT INTO PRO_DCPResultMaster("
				+ PRO_DCPResultMasterTable.WOROUTINGID_FLD + ","
				+ PRO_DCPResultMasterTable.STARTDATETIME_FLD + ","
				+ PRO_DCPResultMasterTable.DUEDATETIME_FLD + ","
				+ PRO_DCPResultMasterTable.QUANTITY_FLD + ","
				+ PRO_DCPResultMasterTable.DCOPTIONMASTERID_FLD + ","
				+ PRO_DCPResultMasterTable.CHECKPOINTID_FLD + ","
				+ PRO_DCPResultMasterTable.PRODUCTID_FLD + ","
				+ PRO_DCPResultMasterTable.CPOID_FLD + ","
				+ PRO_DCPResultMasterTable.WORKORDERDETAILID_FLD + ","
				+ PRO_DCPResultMasterTable.WORKCENTERID_FLD + ")"
				+ "VALUES(?,?,?,?,?,?,?,?,?,?)";

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_DCPResultMasterTable.WOROUTINGID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PRO_DCPResultMasterTable.WOROUTINGID_FLD].Value = objObject.WORoutingID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_DCPResultMasterTable.STARTDATETIME_FLD, OleDbType.Date));
				ocmdPCS.Parameters[PRO_DCPResultMasterTable.STARTDATETIME_FLD].Value = objObject.StartDateTime;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_DCPResultMasterTable.DUEDATETIME_FLD, OleDbType.Date));
				ocmdPCS.Parameters[PRO_DCPResultMasterTable.DUEDATETIME_FLD].Value = objObject.DueDateTime;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_DCPResultMasterTable.QUANTITY_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PRO_DCPResultMasterTable.QUANTITY_FLD].Value = objObject.Quantity;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_DCPResultMasterTable.DCOPTIONMASTERID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PRO_DCPResultMasterTable.DCOPTIONMASTERID_FLD].Value = objObject.DCOptionMasterID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_DCPResultMasterTable.CHECKPOINTID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PRO_DCPResultMasterTable.CHECKPOINTID_FLD].Value = objObject.CheckPointID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_DCPResultMasterTable.PRODUCTID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PRO_DCPResultMasterTable.PRODUCTID_FLD].Value = objObject.ProductID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_DCPResultMasterTable.CPOID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PRO_DCPResultMasterTable.CPOID_FLD].Value = objObject.CPOID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_DCPResultMasterTable.WORKORDERDETAILID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PRO_DCPResultMasterTable.WORKORDERDETAILID_FLD].Value = objObject.WorkOrderDetailID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_DCPResultMasterTable.WORKCENTERID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PRO_DCPResultMasterTable.WORKCENTERID_FLD].Value = objObject.WorkCenterID;


				
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
		///       This method uses to add data to PRO_DCPResultMaster
		///    </summary>
		///    <Inputs>
		///        PRO_DCPResultMasterVO       
		///    </Inputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <History>
		///       Monday, September 05, 2005
		///    </History>


		public int AddAndReturnID(object pobjObjectVO)
		{
			const string METHOD_NAME = THIS + ".AddAndReturnID()";
			 
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS =null;
			try
			{
				PRO_DCPResultMasterVO objObject = (PRO_DCPResultMasterVO) pobjObjectVO;
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand("", oconPCS);
				
				strSql=	"INSERT INTO PRO_DCPResultMaster("
					+ PRO_DCPResultMasterTable.WOROUTINGID_FLD + ","
					+ PRO_DCPResultMasterTable.STARTDATETIME_FLD + ","
					+ PRO_DCPResultMasterTable.DUEDATETIME_FLD + ","
					+ PRO_DCPResultMasterTable.QUANTITY_FLD + ","
					+ PRO_DCPResultMasterTable.DELIVERYQUANTITY_FLD + ","
					+ PRO_DCPResultMasterTable.DCOPTIONMASTERID_FLD + ","
//					+ PRO_DCPResultMasterTable.CHECKPOINTID_FLD + ","
					+ PRO_DCPResultMasterTable.PRODUCTID_FLD + ","
					+ PRO_DCPResultMasterTable.CPOID_FLD + ","
					+ PRO_DCPResultMasterTable.WORKORDERDETAILID_FLD + ","
					+ PRO_DCPResultMasterTable.ROUTINGID_FLD + ","
					+ PRO_DCPResultMasterTable.WORKCENTERID_FLD + ")"
					+ "VALUES(?,?,?,?,?,?,?,?,?,?,?)";
				strSql += "; SELECT @@IDENTITY as LatestID";
				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_DCPResultMasterTable.WOROUTINGID_FLD, OleDbType.Integer));
				if(objObject.WORoutingID > 0)
				ocmdPCS.Parameters[PRO_DCPResultMasterTable.WOROUTINGID_FLD].Value = objObject.WORoutingID;
				else ocmdPCS.Parameters[PRO_DCPResultMasterTable.WOROUTINGID_FLD].Value = DBNull.Value;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_DCPResultMasterTable.STARTDATETIME_FLD, OleDbType.Date));
				ocmdPCS.Parameters[PRO_DCPResultMasterTable.STARTDATETIME_FLD].Value = objObject.StartDateTime;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_DCPResultMasterTable.DUEDATETIME_FLD, OleDbType.Date));
				ocmdPCS.Parameters[PRO_DCPResultMasterTable.DUEDATETIME_FLD].Value = objObject.DueDateTime;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_DCPResultMasterTable.QUANTITY_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PRO_DCPResultMasterTable.QUANTITY_FLD].Value = objObject.Quantity;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_DCPResultMasterTable.DELIVERYQUANTITY_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PRO_DCPResultMasterTable.DELIVERYQUANTITY_FLD].Value = objObject.DeliveryQuantity;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_DCPResultMasterTable.DCOPTIONMASTERID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PRO_DCPResultMasterTable.DCOPTIONMASTERID_FLD].Value = objObject.DCOptionMasterID;

//				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_DCPResultMasterTable.CHECKPOINTID_FLD, OleDbType.Integer));
//				ocmdPCS.Parameters[PRO_DCPResultMasterTable.CHECKPOINTID_FLD].Value = objObject.CheckPointID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_DCPResultMasterTable.PRODUCTID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PRO_DCPResultMasterTable.PRODUCTID_FLD].Value = objObject.ProductID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_DCPResultMasterTable.CPOID_FLD, OleDbType.Integer));
				if(objObject.CPOID > 0)
				ocmdPCS.Parameters[PRO_DCPResultMasterTable.CPOID_FLD].Value = objObject.CPOID;
				else ocmdPCS.Parameters[PRO_DCPResultMasterTable.CPOID_FLD].Value = DBNull.Value;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_DCPResultMasterTable.WORKORDERDETAILID_FLD, OleDbType.Integer));
				if(objObject.WorkOrderDetailID > 0)
				ocmdPCS.Parameters[PRO_DCPResultMasterTable.WORKORDERDETAILID_FLD].Value = objObject.WorkOrderDetailID;
				else ocmdPCS.Parameters[PRO_DCPResultMasterTable.WORKORDERDETAILID_FLD].Value = DBNull.Value;
				
				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_DCPResultMasterTable.ROUTINGID_FLD, OleDbType.Integer));
				if(objObject.RoutingID > 0)
				ocmdPCS.Parameters[PRO_DCPResultMasterTable.ROUTINGID_FLD].Value = objObject.RoutingID;
				else ocmdPCS.Parameters[PRO_DCPResultMasterTable.ROUTINGID_FLD].Value = DBNull.Value;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_DCPResultMasterTable.WORKCENTERID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PRO_DCPResultMasterTable.WORKCENTERID_FLD].Value = objObject.WorkCenterID;
				
				ocmdPCS.CommandText = strSql;
				ocmdPCS.Connection.Open();
				//Add and return latest Id
				return int.Parse(ocmdPCS.ExecuteScalar().ToString());

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
		///       This method uses to add data to PRO_DCPResultMaster
		///    </summary>
		///    <Inputs>
		///        PRO_DCPResultMasterVO       
		///    </Inputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <History>
		///       Monday, September 05, 2005
		///    </History>

		public void Delete(int pintID)
		{
			const string METHOD_NAME = THIS + ".Delete()";
			string strSql = String.Empty;
			strSql=	"DELETE " + PRO_DCPResultMasterTable.TABLE_NAME + " WHERE  " + "DCPResultMasterID" + "=" + pintID.ToString();
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
		/// DeleteMultiRows
		/// </summary>
		/// <param name="pstrMasterID"></param>
		/// <author>Trada</author>
		/// <date>Tuesday, April 25 2006</date>
		public void DeleteMultiRows(string pstrMasterID)
		{
			const string METHOD_NAME = THIS + ".DeleteMultiRows()";
			string strSql = String.Empty;
			strSql=	"DELETE " + PRO_DCPResultMasterTable.TABLE_NAME 
				+ " WHERE  " + PRO_DCPResultMasterTable.DCPRESULTMASTERID_FLD + " IN (" + pstrMasterID + ")";

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
		///       This method uses to add data to PRO_DCPResultMaster
		///    </summary>
		///    <Inputs>
		///        PRO_DCPResultMasterVO       
		///    </Inputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <History>
		///       Monday, September 05, 2005
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
				+ PRO_DCPResultMasterTable.DCPRESULTMASTERID_FLD + ","
				+ PRO_DCPResultMasterTable.WOROUTINGID_FLD + ","
				+ PRO_DCPResultMasterTable.STARTDATETIME_FLD + ","
				+ PRO_DCPResultMasterTable.DUEDATETIME_FLD + ","
				+ PRO_DCPResultMasterTable.QUANTITY_FLD + ","
				+ PRO_DCPResultMasterTable.DCOPTIONMASTERID_FLD + ","
				+ PRO_DCPResultMasterTable.CHECKPOINTID_FLD + ","
				+ PRO_DCPResultMasterTable.PRODUCTID_FLD + ","
				+ PRO_DCPResultMasterTable.CPOID_FLD + ","
				+ PRO_DCPResultMasterTable.WORKORDERDETAILID_FLD + ","
				+ PRO_DCPResultMasterTable.WORKCENTERID_FLD
				+ " FROM " + PRO_DCPResultMasterTable.TABLE_NAME
				+" WHERE " + PRO_DCPResultMasterTable.DCPRESULTMASTERID_FLD + "=" + pintID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();

				PRO_DCPResultMasterVO objObject = new PRO_DCPResultMasterVO();

				while (odrPCS.Read())
				{ 
				objObject.DCPResultMasterID = int.Parse(odrPCS[PRO_DCPResultMasterTable.DCPRESULTMASTERID_FLD].ToString().Trim());
				objObject.WORoutingID = int.Parse(odrPCS[PRO_DCPResultMasterTable.WOROUTINGID_FLD].ToString().Trim());
				objObject.StartDateTime = DateTime.Parse(odrPCS[PRO_DCPResultMasterTable.STARTDATETIME_FLD].ToString().Trim());
				objObject.DueDateTime = DateTime.Parse(odrPCS[PRO_DCPResultMasterTable.DUEDATETIME_FLD].ToString().Trim());
				objObject.Quantity = Decimal.Parse(odrPCS[PRO_DCPResultMasterTable.QUANTITY_FLD].ToString().Trim());
				objObject.DCOptionMasterID = int.Parse(odrPCS[PRO_DCPResultMasterTable.DCOPTIONMASTERID_FLD].ToString().Trim());
				objObject.CheckPointID = int.Parse(odrPCS[PRO_DCPResultMasterTable.CHECKPOINTID_FLD].ToString().Trim());
				objObject.ProductID = int.Parse(odrPCS[PRO_DCPResultMasterTable.PRODUCTID_FLD].ToString().Trim());
				objObject.CPOID = int.Parse(odrPCS[PRO_DCPResultMasterTable.CPOID_FLD].ToString().Trim());
				objObject.WorkOrderDetailID = int.Parse(odrPCS[PRO_DCPResultMasterTable.WORKORDERDETAILID_FLD].ToString().Trim());
				objObject.WorkCenterID = int.Parse(odrPCS[PRO_DCPResultMasterTable.WORKCENTERID_FLD].ToString().Trim());

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
		///       This method uses to add data to PRO_DCPResultMaster
		///    </summary>
		///    <Inputs>
		///        PRO_DCPResultMasterVO       
		///    </Inputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <History>
		///       Monday, September 05, 2005
		///    </History>
		
	
		public void Update(object pobjObjecVO)
		{
			const string METHOD_NAME = THIS + ".Update()";

			PRO_DCPResultMasterVO objObject = (PRO_DCPResultMasterVO) pobjObjecVO;
			

			//prepare value for parameters
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				strSql=	"UPDATE PRO_DCPResultMaster SET "
				+ PRO_DCPResultMasterTable.STARTDATETIME_FLD + "=   ?" + ","
				+ PRO_DCPResultMasterTable.DUEDATETIME_FLD + "=   ?" + ","
				+ PRO_DCPResultMasterTable.QUANTITY_FLD + "=   ?" 
				+" WHERE " + PRO_DCPResultMasterTable.DCPRESULTMASTERID_FLD + "= ?";
				
				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_DCPResultMasterTable.STARTDATETIME_FLD, OleDbType.Date));
				ocmdPCS.Parameters[PRO_DCPResultMasterTable.STARTDATETIME_FLD].Value = objObject.StartDateTime;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_DCPResultMasterTable.DUEDATETIME_FLD, OleDbType.Date));
				ocmdPCS.Parameters[PRO_DCPResultMasterTable.DUEDATETIME_FLD].Value = objObject.DueDateTime;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_DCPResultMasterTable.QUANTITY_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PRO_DCPResultMasterTable.QUANTITY_FLD].Value = objObject.Quantity;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_DCPResultMasterTable.DCPRESULTMASTERID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PRO_DCPResultMasterTable.DCPRESULTMASTERID_FLD].Value = objObject.DCPResultMasterID;


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
		///       This method uses to add data to PRO_DCPResultMaster
		///    </summary>
		///    <Inputs>
		///        PRO_DCPResultMasterVO       
		///    </Inputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <History>
		///       Monday, September 05, 2005
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
				+ PRO_DCPResultMasterTable.DCPRESULTMASTERID_FLD + ","
				+ PRO_DCPResultMasterTable.WOROUTINGID_FLD + ","
				+ PRO_DCPResultMasterTable.STARTDATETIME_FLD + ","
				+ PRO_DCPResultMasterTable.DUEDATETIME_FLD + ","
				+ PRO_DCPResultMasterTable.QUANTITY_FLD + ","
				+ PRO_DCPResultMasterTable.DCOPTIONMASTERID_FLD + ","
				+ PRO_DCPResultMasterTable.CHECKPOINTID_FLD + ","
				+ PRO_DCPResultMasterTable.PRODUCTID_FLD + ","
				+ PRO_DCPResultMasterTable.CPOID_FLD + ","
				+ PRO_DCPResultMasterTable.WORKORDERDETAILID_FLD + ","
				+ PRO_DCPResultMasterTable.WORKCENTERID_FLD
					+ " FROM " + PRO_DCPResultMasterTable.TABLE_NAME;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS,PRO_DCPResultMasterTable.TABLE_NAME);

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

		public DataTable GetTableStruct()
		{
			const string METHOD_NAME = THIS + ".List()";
			DataTable dstPCS = new DataTable();
			
		

			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				
				strSql=	"SELECT TOP 0 "
					+ PRO_DCPResultMasterTable.DCPRESULTMASTERID_FLD + ","
					+ PRO_DCPResultMasterTable.WOROUTINGID_FLD + ","
					+ PRO_DCPResultMasterTable.STARTDATETIME_FLD + ","
					+ PRO_DCPResultMasterTable.DUEDATETIME_FLD + ","
					+ PRO_DCPResultMasterTable.QUANTITY_FLD + ","
					+ PRO_DCPResultMasterTable.DELIVERYQUANTITY_FLD + ","
					+ PRO_DCPResultMasterTable.DCOPTIONMASTERID_FLD + ","
					+ PRO_DCPResultMasterTable.PRODUCTID_FLD + ","
					+ PRO_DCPResultMasterTable.CPOID_FLD + ","
					+ PRO_DCPResultMasterTable.WORKORDERDETAILID_FLD + ","
					+ PRO_DCPResultMasterTable.ROUTINGID_FLD + ","
					+ "MasterShiftID, MasterTotalSecond,"
					+ PRO_DCPResultMasterTable.WORKCENTERID_FLD
					+ " FROM " + PRO_DCPResultMasterTable.TABLE_NAME
					+ " WHERE 0=1";
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS);

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
		/// GetDCPResultMasterByArrayMasterID
		/// </summary>
		/// <param name="pstrMasterID"></param>
		/// <returns></returns>
		/// <author>Trada</author>
		/// <date>Tuesday, April 25 2006</date>
		public DataSet GetDCPResultMasterByArrayMasterID(string pstrMasterID)
		{
			const string METHOD_NAME = THIS + ".GetDCPResultMasterByArrayMasterID()";
			DataSet dstPCS = new DataSet();
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				strSql=	"SELECT "
					+ PRO_DCPResultMasterTable.DCPRESULTMASTERID_FLD + ","
					+ PRO_DCPResultMasterTable.WOROUTINGID_FLD + ","
					+ PRO_DCPResultMasterTable.STARTDATETIME_FLD + ","
					+ PRO_DCPResultMasterTable.DUEDATETIME_FLD + ","
					+ PRO_DCPResultMasterTable.QUANTITY_FLD + ","
					+ PRO_DCPResultMasterTable.DCOPTIONMASTERID_FLD + ","
					+ PRO_DCPResultMasterTable.ROUTINGID_FLD + ","
					+ PRO_DCPResultMasterTable.PRODUCTID_FLD + ","
					+ PRO_DCPResultMasterTable.CPOID_FLD + ","
					+ PRO_DCPResultMasterTable.WORKORDERDETAILID_FLD + ","
					+ PRO_DCPResultMasterTable.WORKCENTERID_FLD
					+ " FROM " + PRO_DCPResultMasterTable.TABLE_NAME
					+ " WHERE " + PRO_DCPResultMasterTable.DCPRESULTMASTERID_FLD + " IN (" + pstrMasterID + ")";
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS,PRO_DCPResultMasterTable.TABLE_NAME);

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
		///       This method uses to add data to PRO_DCPResultMaster
		///    </summary>
		///    <Inputs>
		///        PRO_DCPResultMasterVO       
		///    </Inputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <History>
		///       Monday, September 05, 2005
		///    </History>
		
		public void UpdateDataTable(DataTable pdtbData)
		{
			const string METHOD_NAME = THIS + ".UpdateDataSet()";
			string strSql;
			OleDbConnection oconPCS =null;
			OleDbCommandBuilder odcbPCS ;
			OleDbDataAdapter odadPCS = new OleDbDataAdapter();

			try
			{
				strSql=	"SELECT "
				+ PRO_DCPResultMasterTable.DCPRESULTMASTERID_FLD + ","
				+ PRO_DCPResultMasterTable.WOROUTINGID_FLD + ","
				+ "RoutingID , MasterTotalSecond, MasterShiftID, "
				+ PRO_DCPResultMasterTable.STARTDATETIME_FLD + ","
				+ PRO_DCPResultMasterTable.DUEDATETIME_FLD + ","
				+ PRO_DCPResultMasterTable.QUANTITY_FLD + ","
				+ PRO_DCPResultMasterTable.DELIVERYQUANTITY_FLD + ","
				+ PRO_DCPResultMasterTable.DCOPTIONMASTERID_FLD + ","
				+ PRO_DCPResultMasterTable.PRODUCTID_FLD + ","
				+ PRO_DCPResultMasterTable.CPOID_FLD + ","
				+ PRO_DCPResultMasterTable.WORKORDERDETAILID_FLD + ","
				+ PRO_DCPResultMasterTable.WORKCENTERID_FLD 
				+ "  FROM " + PRO_DCPResultMasterTable.TABLE_NAME;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				odadPCS.SelectCommand = new OleDbCommand(strSql, oconPCS);
				odcbPCS = new OleDbCommandBuilder(odadPCS);
				//pdtbData.EnforceConstraints = false;
				odadPCS.Update(pdtbData);
			
				
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

		public void InsertDCPResultDetail(int pintDCOptionMasterID,string pstrChildWorkCenterIDs)
		{
			const string METHOD_NAME = THIS + ".Update()";

			//PRO_DCPResultMasterVO objObject = (PRO_DCPResultMasterVO) pobjObjecVO;
			

			//prepare value for parameters
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				strSql=	"insert into pro_dcpresultdetail(StartTime,EndTime," +
							" TotalSecond,Quantity,Percentage, " +
							" DCPResultMasterID, " +
							" Type, " +
							" WorkingDate, " +
							" ShiftID, " +
							" IsManual " +
							" ) " +
							" select StartDateTime StartTime, " +
							" DueDateTime EndTime, " +
							" Isnull(MasterTotalSecond,0) TotalSecond ,Quantity,100 Percentage, " +
							" DCPResultMasterID, " +
							" 0 Type, " +
							" cast(cast(dateadd(hh,-datepart(hh,StartDateTime),StartDateTime) as int) as datetime) WorkingDate, " +
							" MasterShiftID, " +
							" 0 IsManual from pro_dcpresultmaster  " +
							" where DCOptionMasterID= " + pintDCOptionMasterID +
							" AND WorkCenterID IN " + pstrChildWorkCenterIDs +
							" and Isnull(Quantity,0) > 0 " +
							" and DCPResultMasterID NOT IN (Select DCPResultMasterID from pro_dcpresultdetail)";

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

		public void InsertDCPOrderProduce(DataTable pdtbData, int pintDCOptionMasterID, string strChildWCIDs)
		{
			const string METHOD_NAME = THIS + ".Update()";

			//PRO_DCPResultMasterVO objObject = (PRO_DCPResultMasterVO) pobjObjecVO;
			
			OleDbConnection oconPCS =null;
			OleDbCommandBuilder odcbPCS ;
			OleDbDataAdapter odadPCS = new OleDbDataAdapter();

			//prepare value for parameters
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				strSql=	"Delete from DCP_OrderProduce where DCOptionMasterID =" + pintDCOptionMasterID
					+ " AND WorkCenterID IN " + strChildWCIDs;
				ocmdPCS.CommandText = strSql;
				ocmdPCS.Connection.Open();
				ocmdPCS.ExecuteNonQuery();	

				strSql=	"SELECT WorkCenterID,ColumnName,OrderNo,OrderPlan,ShiftID,DCOptionMasterID From DCP_OrderProduce";
				
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				odadPCS.SelectCommand = new OleDbCommand(strSql, oconPCS);
				odcbPCS = new OleDbCommandBuilder(odadPCS);
				//pdtbData.EnforceConstraints = false;
				odadPCS.Update(pdtbData);
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

		/// <summary>
		/// UpdateDataSetDeleteMultiRows
		/// </summary>
		/// <param name="pdstData"></param>
		/// <author>Trada</author>
		/// <date>Tuesday, April 25 2006</date>
		public void UpdateDataSetDeleteMultiRows(DataSet pdstData)
		{
			const string METHOD_NAME = THIS + ".UpdateDataSetDeleteMultiRows()";
			string strSql;
			OleDbConnection oconPCS =null;
			OleDbCommandBuilder odcbPCS ;
			OleDbDataAdapter odadPCS = new OleDbDataAdapter();

			try
			{
				strSql=	"SELECT "
					+ PRO_DCPResultMasterTable.DCPRESULTMASTERID_FLD + ","
					+ PRO_DCPResultMasterTable.WOROUTINGID_FLD + ","
					+ "RoutingID ,"
					+ PRO_DCPResultMasterTable.STARTDATETIME_FLD + ","
					+ PRO_DCPResultMasterTable.DUEDATETIME_FLD + ","
					+ PRO_DCPResultMasterTable.QUANTITY_FLD + ","
					+ PRO_DCPResultMasterTable.DCOPTIONMASTERID_FLD + ","
					+ PRO_DCPResultMasterTable.PRODUCTID_FLD + ","
					+ PRO_DCPResultMasterTable.CPOID_FLD + ","
					+ PRO_DCPResultMasterTable.WORKORDERDETAILID_FLD + ","
					+ PRO_DCPResultMasterTable.WORKCENTERID_FLD 
					+ "  FROM " + PRO_DCPResultMasterTable.TABLE_NAME;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				odadPCS.SelectCommand = new OleDbCommand(strSql, oconPCS);
				odcbPCS = new OleDbCommandBuilder(odadPCS);
				pdstData.EnforceConstraints = false;
				odadPCS.Update(pdstData,PRO_DCPResultMasterTable.TABLE_NAME);
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

		///    <summary>
		///       This method uses to add data to PRO_DCPResult
		///    </summary>
		///    <Inputs>
		///        PRO_DCPResultVO       
		///    </Inputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <History>
		///       Tuesday, August 30, 2005
		///    </History>

		public DataSet ListDetailStructTable()
		{
			const string METHOD_NAME = THIS + ".ListDetailStructTable()";
			DataSet dstPCS = new DataSet();
			
		

			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				
				strSql=	"SELECT TOP 0 DCPResultDetailID, 0 WorkCenterID, StartTime DATE, StartTime, EndTime, TotalSecond, Quantity, Percentage, DCPResultMasterID, Type, WorkingDate, Converted, ShiftID, '' WorkCenterCode"
					+ " FROM PRO_DCPResultDetail"; //+ PRO_DCPResultTable.TABLE_NAME;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS,PRO_DCPResultDetailTable.TABLE_NAME);

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

		public DataSet ListMasterStructTable()
		{
			const string METHOD_NAME = THIS + ".ListMasterStructTable()";
			DataSet dstPCS = new DataSet();
			
		

			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				
				strSql=	"SELECT TOP 0 DCPResultMasterID, 0 DetailID, StartDateTime, DueDateTime, Quantity, DCOptionMasterID, CPOID, WORoutingID, WorkOrderDetailID, ProductID, RoutingID, WorkCenterID"
					//					+ PRO_DCPResultTable.DCPRESULTID_FLD + ","
					//					+ PRO_DCPResultTable.WOROUTINGID_FLD + ","
					//					+ PRO_DCPResultTable.STARTDATETIME_FLD + ","
					//					+ PRO_DCPResultTable.DUEDATETIME_FLD + ","
					//					+ PRO_DCPResultTable.QUANTITY_FLD + ","
					//					+ PRO_DCPResultTable.DCOPTIONMASTERID_FLD + ","
					//					+ PRO_DCPResultTable.CHECKPOINTID_FLD + ","
					//					+ PRO_DCPResultTable.PRODUCTID_FLD + ","
					//					+ PRO_DCPResultTable.CPOID_FLD + ","
					//					+ PRO_DCPResultTable.WORKORDERDETAILID_FLD + ","
					//					+ PRO_DCPResultTable.WORKCENTERID_FLD
					+ " FROM " + PRO_DCPResultMasterTable.TABLE_NAME;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS,PRO_DCPResultMasterTable.TABLE_NAME);

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
		/// Delete all record in Detail and Master result table
		/// </summary>
		/// <param name="pintDCOptionMasterID"></param>
		public void DeleteOldResult(int pintDCOptionMasterID)
		{
			const string METHOD_NAME = THIS + ".DeleteOldResult()";
			string strSql = "DELETE FROM PRO_DCPResultDetail WHERE "
								+ " DCPResultMasterID IN (SELECT DCPResultMasterID FROM PRO_DCPResultMaster WHERE DCOptionMasterID = " + pintDCOptionMasterID + ")";
					strSql += " DELETE " + PRO_DCPResultMasterTable.TABLE_NAME + " WHERE  " + PRO_DCPResultMasterTable.DCOPTIONMASTERID_FLD + "=" + pintDCOptionMasterID;
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


		public void UpdateDCPResultMaster(DataSet pdstData)
		{
			const string METHOD_NAME = THIS + ".UpdateDataSet()";
			string strSql;
			OleDbConnection oconPCS =null;
			OleDbCommandBuilder odcbPCS ;
			OleDbDataAdapter odadPCS = new OleDbDataAdapter();

			try
			{
				strSql=	"SELECT "
					+ PRO_DCPResultMasterTable.STARTDATETIME_FLD + ","
					+ PRO_DCPResultMasterTable.DUEDATETIME_FLD + ","
					+ PRO_DCPResultMasterTable.DCOPTIONMASTERID_FLD + ","
					+ PRO_DCPResultMasterTable.QUANTITY_FLD + ","
					+ PRO_DCPResultMasterTable.PRODUCTID_FLD + ","
					+ PRO_DCPResultMasterTable.WORKCENTERID_FLD + ","
					+ PRO_DCPResultMasterTable.ROUTINGID_FLD
					+ "  FROM " + PRO_DCPResultMasterTable.TABLE_NAME;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				odadPCS.SelectCommand = new OleDbCommand(strSql, oconPCS);
				odcbPCS = new OleDbCommandBuilder(odadPCS);
				pdstData.EnforceConstraints = false;
				odadPCS.Update(pdstData,PRO_DCPResultMasterTable.TABLE_NAME);

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


		public int GetRoutingID(int pintProductID, int pintWorkCenterID)
		{
			const string METHOD_NAME = THIS + ".AddAndReturnID()";
			 
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS =null;
			try
			{
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand("", oconPCS);
				
				strSql=	"select RoutingID from Itm_routing where ProductID = " + pintProductID 
					+ " and WorkCenterID = " + pintWorkCenterID;
				ocmdPCS.CommandText = strSql;
				ocmdPCS.Connection.Open();
				//Add and return latest Id
				return int.Parse(ocmdPCS.ExecuteScalar().ToString());

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
					+ PRO_DCPResultMasterTable.DCPRESULTMASTERID_FLD + ","
					+ PRO_DCPResultMasterTable.WOROUTINGID_FLD + ","
					+ "RoutingID ,"
					+ PRO_DCPResultMasterTable.STARTDATETIME_FLD + ","
					+ PRO_DCPResultMasterTable.DUEDATETIME_FLD + ","
					+ PRO_DCPResultMasterTable.QUANTITY_FLD + ","
					+ PRO_DCPResultMasterTable.DCOPTIONMASTERID_FLD + ","
					+ PRO_DCPResultMasterTable.PRODUCTID_FLD + ","
					+ PRO_DCPResultMasterTable.CPOID_FLD + ","
					+ PRO_DCPResultMasterTable.WORKORDERDETAILID_FLD + ","
					+ PRO_DCPResultMasterTable.WORKCENTERID_FLD 
					+ "  FROM " + PRO_DCPResultMasterTable.TABLE_NAME;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				odadPCS.SelectCommand = new OleDbCommand(strSql, oconPCS);
				//odcbPCS = new OleDbCommandBuilder(odadPCS);
				pdstData.EnforceConstraints = false;
			
				OleDbCommand ocmdPCS = new OleDbCommand("", oconPCS);
				string strInsSql=	"INSERT INTO PRO_DCPResultMaster("
					+ PRO_DCPResultMasterTable.WOROUTINGID_FLD + ","
					+ "RoutingID ,"
					+ PRO_DCPResultMasterTable.STARTDATETIME_FLD + ","
					+ PRO_DCPResultMasterTable.DUEDATETIME_FLD + ","
					+ PRO_DCPResultMasterTable.QUANTITY_FLD + ","
					+ PRO_DCPResultMasterTable.DCOPTIONMASTERID_FLD + ","
					+ PRO_DCPResultMasterTable.PRODUCTID_FLD + ","
					+ PRO_DCPResultMasterTable.CPOID_FLD + ","
					+ PRO_DCPResultMasterTable.WORKORDERDETAILID_FLD + ","
					+ PRO_DCPResultMasterTable.WORKCENTERID_FLD + ")"
					+ " VALUES (?,?,?,?,?,?,?,?,?,?);"
					+ " SELECT SCOPE_IDENTITY() AS DCPResultMasterID";

				ocmdPCS.CommandText = strInsSql;
				ocmdPCS.CommandType = CommandType.Text;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_DCPResultMasterTable.WOROUTINGID_FLD, OleDbType.Integer,0,"WORouting"));
				ocmdPCS.Parameters.Add(new OleDbParameter("RoutingID", OleDbType.Integer,0,"Routing"));
				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_DCPResultMasterTable.STARTDATETIME_FLD, OleDbType.Date,0,"StartDateTime"));
				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_DCPResultMasterTable.DUEDATETIME_FLD, OleDbType.Date,0,"DueDateTime"));
				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_DCPResultMasterTable.QUANTITY_FLD, OleDbType.Decimal,0,"Quantity"));
				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_DCPResultMasterTable.DCOPTIONMASTERID_FLD, OleDbType.Integer,0,"DCOptionMasterID"));
				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_DCPResultMasterTable.PRODUCTID_FLD, OleDbType.Integer,0,"ProductID"));
				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_DCPResultMasterTable.CPOID_FLD, OleDbType.Integer,0,"CPOID"));
				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_DCPResultMasterTable.WORKORDERDETAILID_FLD, OleDbType.Integer,0,"WorkOrderDetailID"));
				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_DCPResultMasterTable.WORKCENTERID_FLD, OleDbType.Integer,0,"WorkCenterID"));
				ocmdPCS.UpdatedRowSource = UpdateRowSource.FirstReturnedRecord;
				odadPCS.InsertCommand = ocmdPCS;
				//odadPCS.

				odadPCS.Update(pdstData,PRO_DCPResultMasterTable.TABLE_NAME);
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
