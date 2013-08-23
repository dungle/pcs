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
	public class PRO_DCPResultDS 
	{
		public PRO_DCPResultDS()
		{
		}
		private const string THIS = "PCSComProduction.DCP.DS.PRO_DCPResultDS";

	
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


		public void Add(object pobjObjectVO)
		{
			const string METHOD_NAME = THIS + ".Add()";
			 
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS =null;
			try
			{
				PRO_DCPResultVO objObject = (PRO_DCPResultVO) pobjObjectVO;
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand("", oconPCS);
				
				strSql=	"INSERT INTO PRO_DCPResult("
				+ PRO_DCPResultTable.WOROUTINGID_FLD + ","
				+ PRO_DCPResultTable.STARTDATETIME_FLD + ","
				+ PRO_DCPResultTable.DUEDATETIME_FLD + ","
				+ PRO_DCPResultTable.QUANTITY_FLD + ","
				+ PRO_DCPResultTable.DCOPTIONMASTERID_FLD + ","
				+ PRO_DCPResultTable.CHECKPOINTID_FLD + ","
				+ PRO_DCPResultTable.PRODUCTID_FLD + ","
				+ PRO_DCPResultTable.CPOID_FLD + ","
				+ PRO_DCPResultTable.WORKORDERDETAILID_FLD + ","
				+ PRO_DCPResultTable.WORKCENTERID_FLD + ")"
				+ "VALUES(?,?,?,?,?,?,?,?,?,?)";

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_DCPResultTable.WOROUTINGID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PRO_DCPResultTable.WOROUTINGID_FLD].Value = objObject.WORoutingID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_DCPResultTable.STARTDATETIME_FLD, OleDbType.Date));
				ocmdPCS.Parameters[PRO_DCPResultTable.STARTDATETIME_FLD].Value = objObject.StartDateTime;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_DCPResultTable.DUEDATETIME_FLD, OleDbType.Date));
				ocmdPCS.Parameters[PRO_DCPResultTable.DUEDATETIME_FLD].Value = objObject.DueDateTime;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_DCPResultTable.QUANTITY_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PRO_DCPResultTable.QUANTITY_FLD].Value = objObject.Quantity;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_DCPResultTable.DCOPTIONMASTERID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PRO_DCPResultTable.DCOPTIONMASTERID_FLD].Value = objObject.DCOptionMasterID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_DCPResultTable.CHECKPOINTID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PRO_DCPResultTable.CHECKPOINTID_FLD].Value = objObject.CheckPointID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_DCPResultTable.PRODUCTID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PRO_DCPResultTable.PRODUCTID_FLD].Value = objObject.ProductID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_DCPResultTable.CPOID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PRO_DCPResultTable.CPOID_FLD].Value = objObject.CPOID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_DCPResultTable.WORKORDERDETAILID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PRO_DCPResultTable.WORKORDERDETAILID_FLD].Value = objObject.WorkOrderDetailID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_DCPResultTable.WORKCENTERID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PRO_DCPResultTable.WORKCENTERID_FLD].Value = objObject.WorkCenterID;


				
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

		public void Delete(int pintID)
		{
			const string METHOD_NAME = THIS + ".Delete()";
			string strSql = String.Empty;
			strSql=	"DELETE " + PRO_DCPResultTable.TABLE_NAME + " WHERE  " + "DCPResultID" + "=" + pintID.ToString();
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
				+ PRO_DCPResultTable.DCPRESULTID_FLD + ","
				+ PRO_DCPResultTable.WOROUTINGID_FLD + ","
				+ PRO_DCPResultTable.STARTDATETIME_FLD + ","
				+ PRO_DCPResultTable.DUEDATETIME_FLD + ","
				+ PRO_DCPResultTable.QUANTITY_FLD + ","
				+ PRO_DCPResultTable.DCOPTIONMASTERID_FLD + ","
				+ PRO_DCPResultTable.CHECKPOINTID_FLD + ","
				+ PRO_DCPResultTable.PRODUCTID_FLD + ","
				+ PRO_DCPResultTable.CPOID_FLD + ","
				+ PRO_DCPResultTable.WORKORDERDETAILID_FLD + ","
				+ PRO_DCPResultTable.WORKCENTERID_FLD
				+ " FROM " + PRO_DCPResultTable.TABLE_NAME
				+" WHERE " + PRO_DCPResultTable.DCPRESULTID_FLD + "=" + pintID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();

				PRO_DCPResultVO objObject = new PRO_DCPResultVO();

				while (odrPCS.Read())
				{ 
				objObject.DCPResultID = int.Parse(odrPCS[PRO_DCPResultTable.DCPRESULTID_FLD].ToString().Trim());
				objObject.WORoutingID = int.Parse(odrPCS[PRO_DCPResultTable.WOROUTINGID_FLD].ToString().Trim());
				objObject.StartDateTime = DateTime.Parse(odrPCS[PRO_DCPResultTable.STARTDATETIME_FLD].ToString().Trim());
				objObject.DueDateTime = DateTime.Parse(odrPCS[PRO_DCPResultTable.DUEDATETIME_FLD].ToString().Trim());
				objObject.Quantity = Decimal.Parse(odrPCS[PRO_DCPResultTable.QUANTITY_FLD].ToString().Trim());
				objObject.DCOptionMasterID = int.Parse(odrPCS[PRO_DCPResultTable.DCOPTIONMASTERID_FLD].ToString().Trim());
				objObject.CheckPointID = int.Parse(odrPCS[PRO_DCPResultTable.CHECKPOINTID_FLD].ToString().Trim());
				objObject.ProductID = int.Parse(odrPCS[PRO_DCPResultTable.PRODUCTID_FLD].ToString().Trim());
				objObject.CPOID = int.Parse(odrPCS[PRO_DCPResultTable.CPOID_FLD].ToString().Trim());
				objObject.WorkOrderDetailID = int.Parse(odrPCS[PRO_DCPResultTable.WORKORDERDETAILID_FLD].ToString().Trim());
				objObject.WorkCenterID = int.Parse(odrPCS[PRO_DCPResultTable.WORKCENTERID_FLD].ToString().Trim());

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
		
	
		public void Update(object pobjObjecVO)
		{
			const string METHOD_NAME = THIS + ".Update()";

			PRO_DCPResultVO objObject = (PRO_DCPResultVO) pobjObjecVO;
			

			//prepare value for parameters
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				strSql=	"UPDATE PRO_DCPResult SET "
				+ PRO_DCPResultTable.WOROUTINGID_FLD + "=   ?" + ","
				+ PRO_DCPResultTable.STARTDATETIME_FLD + "=   ?" + ","
				+ PRO_DCPResultTable.DUEDATETIME_FLD + "=   ?" + ","
				+ PRO_DCPResultTable.QUANTITY_FLD + "=   ?" + ","
				+ PRO_DCPResultTable.DCOPTIONMASTERID_FLD + "=   ?" + ","
				+ PRO_DCPResultTable.CHECKPOINTID_FLD + "=   ?" + ","
				+ PRO_DCPResultTable.PRODUCTID_FLD + "=   ?" + ","
				+ PRO_DCPResultTable.CPOID_FLD + "=   ?" + ","
				+ PRO_DCPResultTable.WORKORDERDETAILID_FLD + "=   ?" + ","
				+ PRO_DCPResultTable.WORKCENTERID_FLD + "=  ?"
				+" WHERE " + PRO_DCPResultTable.DCPRESULTID_FLD + "= ?";

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_DCPResultTable.WOROUTINGID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PRO_DCPResultTable.WOROUTINGID_FLD].Value = objObject.WORoutingID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_DCPResultTable.STARTDATETIME_FLD, OleDbType.Date));
				ocmdPCS.Parameters[PRO_DCPResultTable.STARTDATETIME_FLD].Value = objObject.StartDateTime;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_DCPResultTable.DUEDATETIME_FLD, OleDbType.Date));
				ocmdPCS.Parameters[PRO_DCPResultTable.DUEDATETIME_FLD].Value = objObject.DueDateTime;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_DCPResultTable.QUANTITY_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PRO_DCPResultTable.QUANTITY_FLD].Value = objObject.Quantity;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_DCPResultTable.DCOPTIONMASTERID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PRO_DCPResultTable.DCOPTIONMASTERID_FLD].Value = objObject.DCOptionMasterID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_DCPResultTable.CHECKPOINTID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PRO_DCPResultTable.CHECKPOINTID_FLD].Value = objObject.CheckPointID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_DCPResultTable.PRODUCTID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PRO_DCPResultTable.PRODUCTID_FLD].Value = objObject.ProductID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_DCPResultTable.CPOID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PRO_DCPResultTable.CPOID_FLD].Value = objObject.CPOID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_DCPResultTable.WORKORDERDETAILID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PRO_DCPResultTable.WORKORDERDETAILID_FLD].Value = objObject.WorkOrderDetailID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_DCPResultTable.WORKCENTERID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PRO_DCPResultTable.WORKCENTERID_FLD].Value = objObject.WorkCenterID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_DCPResultTable.DCPRESULTID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PRO_DCPResultTable.DCPRESULTID_FLD].Value = objObject.DCPResultID;


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
				+ PRO_DCPResultTable.DCPRESULTID_FLD + ","
				+ PRO_DCPResultTable.WOROUTINGID_FLD + ","
				+ PRO_DCPResultTable.STARTDATETIME_FLD + ","
				+ PRO_DCPResultTable.DUEDATETIME_FLD + ","
				+ PRO_DCPResultTable.QUANTITY_FLD + ","
				+ PRO_DCPResultTable.DCOPTIONMASTERID_FLD + ","
				+ PRO_DCPResultTable.CHECKPOINTID_FLD + ","
				+ PRO_DCPResultTable.PRODUCTID_FLD + ","
				+ PRO_DCPResultTable.CPOID_FLD + ","
				+ PRO_DCPResultTable.WORKORDERDETAILID_FLD + ","
				+ PRO_DCPResultTable.WORKCENTERID_FLD
					+ " FROM " + PRO_DCPResultTable.TABLE_NAME;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS,PRO_DCPResultTable.TABLE_NAME);

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
				+ PRO_DCPResultTable.DCPRESULTID_FLD + ","
				+ PRO_DCPResultTable.WOROUTINGID_FLD + ","
				+ PRO_DCPResultTable.STARTDATETIME_FLD + ","
				+ PRO_DCPResultTable.DUEDATETIME_FLD + ","
				+ PRO_DCPResultTable.QUANTITY_FLD + ","
				+ PRO_DCPResultTable.DCOPTIONMASTERID_FLD + ","
				+ PRO_DCPResultTable.CHECKPOINTID_FLD + ","
				+ PRO_DCPResultTable.PRODUCTID_FLD + ","
				+ PRO_DCPResultTable.CPOID_FLD + ","
				+ PRO_DCPResultTable.WORKORDERDETAILID_FLD + ","
				+ PRO_DCPResultTable.WORKCENTERID_FLD 
		+ "  FROM " + PRO_DCPResultTable.TABLE_NAME;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				odadPCS.SelectCommand = new OleDbCommand(strSql, oconPCS);
				odcbPCS = new OleDbCommandBuilder(odadPCS);
				pdstData.EnforceConstraints = false;
				odadPCS.Update(pdstData,PRO_DCPResultTable.TABLE_NAME);

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
