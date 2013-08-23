using System;
using System.Data;
using System.Data.OleDb;
using System.Collections;
using System.Configuration;
using PCSComUtils.DataAccess;
using PCSComUtils.PCSExc;
using PCSComUtils.Common;


namespace PCSComProcurement.Purchase.DS
{
	
	public class PO_PRDeliveryScheduleDS 
	{
		public PO_PRDeliveryScheduleDS()
		{
		}
		private const string THIS = "PCSComProcurement.Purchase.DS.DS.PO_PRDeliveryScheduleDS";

	
		//**************************************************************************              
		///    <Description>
		///       This method uses to add data to PO_PRDeliverySchedule
		///    </Description>
		///    <Inputs>
		///        PO_PRDeliveryScheduleVO       
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
		///       Tuesday, March 01, 2005
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
				PO_PRDeliveryScheduleVO objObject = (PO_PRDeliveryScheduleVO) pobjObjectVO;
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand("", oconPCS);
				
				strSql=	"INSERT INTO PO_PRDeliverySchedule("
				+ PO_PRDeliveryScheduleTable.DELIVERYLINE_FLD + ","
				+ PO_PRDeliveryScheduleTable.SCHEDULEDATE_FLD + ","
				+ PO_PRDeliveryScheduleTable.DELIVERYQUANTITY_FLD + ","
				+ PO_PRDeliveryScheduleTable.RECEIVEDQUANTITY_FLD + ","
				+ PO_PRDeliveryScheduleTable.PURCHASEREQUISITIONLINESID_FLD + ")"
				+ "VALUES(?,?,?,?,?)";

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PRDeliveryScheduleTable.DELIVERYLINE_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_PRDeliveryScheduleTable.DELIVERYLINE_FLD].Value = objObject.DeliveryLine;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PRDeliveryScheduleTable.SCHEDULEDATE_FLD, OleDbType.Date));
				ocmdPCS.Parameters[PO_PRDeliveryScheduleTable.SCHEDULEDATE_FLD].Value = objObject.ScheduleDate;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PRDeliveryScheduleTable.DELIVERYQUANTITY_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_PRDeliveryScheduleTable.DELIVERYQUANTITY_FLD].Value = objObject.DeliveryQuantity;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PRDeliveryScheduleTable.RECEIVEDQUANTITY_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_PRDeliveryScheduleTable.RECEIVEDQUANTITY_FLD].Value = objObject.ReceivedQuantity;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PRDeliveryScheduleTable.PURCHASEREQUISITIONLINESID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_PRDeliveryScheduleTable.PURCHASEREQUISITIONLINESID_FLD].Value = objObject.PurchaseRequisitionLinesID;


				
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
		///       This method uses to delete data from PO_PRDeliverySchedule
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
			strSql=	"DELETE " + PO_PRDeliveryScheduleTable.TABLE_NAME + " WHERE  " + "DeliveryScheduleID" + "=" + pintID.ToString();
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
		///       This method uses to get data from PO_PRDeliverySchedule
		///    </Description>
		///    <Inputs>
		///        ID       
		///    </Inputs>
		///    <Outputs>
		///       PO_PRDeliveryScheduleVO
		///    </Outputs>
		///    <Returns>
		///       PO_PRDeliveryScheduleVO
		///    </Returns>
		///    <Authors>
		///       HungLa
		///    </Authors>
		///    <History>
		///       Tuesday, March 01, 2005
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
				+ PO_PRDeliveryScheduleTable.DELIVERYSCHEDULEID_FLD + ","
				+ PO_PRDeliveryScheduleTable.DELIVERYLINE_FLD + ","
				+ PO_PRDeliveryScheduleTable.SCHEDULEDATE_FLD + ","
				+ PO_PRDeliveryScheduleTable.DELIVERYQUANTITY_FLD + ","
				+ PO_PRDeliveryScheduleTable.RECEIVEDQUANTITY_FLD + ","
				+ PO_PRDeliveryScheduleTable.PURCHASEREQUISITIONLINESID_FLD
				+ " FROM " + PO_PRDeliveryScheduleTable.TABLE_NAME
				+" WHERE " + PO_PRDeliveryScheduleTable.DELIVERYSCHEDULEID_FLD + "=" + pintID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();

				PO_PRDeliveryScheduleVO objObject = new PO_PRDeliveryScheduleVO();

				while (odrPCS.Read())
				{ 
				objObject.DeliveryScheduleID = int.Parse(odrPCS[PO_PRDeliveryScheduleTable.DELIVERYSCHEDULEID_FLD].ToString());
				objObject.DeliveryLine = int.Parse(odrPCS[PO_PRDeliveryScheduleTable.DELIVERYLINE_FLD].ToString());
				objObject.ScheduleDate = DateTime.Parse(odrPCS[PO_PRDeliveryScheduleTable.SCHEDULEDATE_FLD].ToString());
				objObject.DeliveryQuantity = int.Parse(odrPCS[PO_PRDeliveryScheduleTable.DELIVERYQUANTITY_FLD].ToString());
				objObject.ReceivedQuantity = int.Parse(odrPCS[PO_PRDeliveryScheduleTable.RECEIVEDQUANTITY_FLD].ToString());
				objObject.PurchaseRequisitionLinesID = int.Parse(odrPCS[PO_PRDeliveryScheduleTable.PURCHASEREQUISITIONLINESID_FLD].ToString());

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
		///       This method uses to update data to PO_PRDeliverySchedule
		///    </Description>
		///    <Inputs>
		///       PO_PRDeliveryScheduleVO       
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

			PO_PRDeliveryScheduleVO objObject = (PO_PRDeliveryScheduleVO) pobjObjecVO;
			

			//prepare value for parameters
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				strSql=	"UPDATE PO_PRDeliverySchedule SET "
				+ PO_PRDeliveryScheduleTable.DELIVERYLINE_FLD + "=   ?" + ","
				+ PO_PRDeliveryScheduleTable.SCHEDULEDATE_FLD + "=   ?" + ","
				+ PO_PRDeliveryScheduleTable.DELIVERYQUANTITY_FLD + "=   ?" + ","
				+ PO_PRDeliveryScheduleTable.RECEIVEDQUANTITY_FLD + "=   ?" + ","
				+ PO_PRDeliveryScheduleTable.PURCHASEREQUISITIONLINESID_FLD + "=  ?"
				+" WHERE " + PO_PRDeliveryScheduleTable.DELIVERYSCHEDULEID_FLD + "= ?";

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PRDeliveryScheduleTable.DELIVERYLINE_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_PRDeliveryScheduleTable.DELIVERYLINE_FLD].Value = objObject.DeliveryLine;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PRDeliveryScheduleTable.SCHEDULEDATE_FLD, OleDbType.Date));
				ocmdPCS.Parameters[PO_PRDeliveryScheduleTable.SCHEDULEDATE_FLD].Value = objObject.ScheduleDate;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PRDeliveryScheduleTable.DELIVERYQUANTITY_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_PRDeliveryScheduleTable.DELIVERYQUANTITY_FLD].Value = objObject.DeliveryQuantity;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PRDeliveryScheduleTable.RECEIVEDQUANTITY_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_PRDeliveryScheduleTable.RECEIVEDQUANTITY_FLD].Value = objObject.ReceivedQuantity;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PRDeliveryScheduleTable.PURCHASEREQUISITIONLINESID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_PRDeliveryScheduleTable.PURCHASEREQUISITIONLINESID_FLD].Value = objObject.PurchaseRequisitionLinesID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PRDeliveryScheduleTable.DELIVERYSCHEDULEID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_PRDeliveryScheduleTable.DELIVERYSCHEDULEID_FLD].Value = objObject.DeliveryScheduleID;


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
		///       This method uses to get all data from PO_PRDeliverySchedule
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
		///       Tuesday, March 01, 2005
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
				+ PO_PRDeliveryScheduleTable.DELIVERYSCHEDULEID_FLD + ","
				+ PO_PRDeliveryScheduleTable.DELIVERYLINE_FLD + ","
				+ PO_PRDeliveryScheduleTable.SCHEDULEDATE_FLD + ","
				+ PO_PRDeliveryScheduleTable.DELIVERYQUANTITY_FLD + ","
				+ PO_PRDeliveryScheduleTable.RECEIVEDQUANTITY_FLD + ","
				+ PO_PRDeliveryScheduleTable.PURCHASEREQUISITIONLINESID_FLD
					+ " FROM " + PO_PRDeliveryScheduleTable.TABLE_NAME;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS,PO_PRDeliveryScheduleTable.TABLE_NAME);

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
		///       Tuesday, March 01, 2005
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
				+ PO_PRDeliveryScheduleTable.DELIVERYSCHEDULEID_FLD + ","
				+ PO_PRDeliveryScheduleTable.DELIVERYLINE_FLD + ","
				+ PO_PRDeliveryScheduleTable.SCHEDULEDATE_FLD + ","
				+ PO_PRDeliveryScheduleTable.DELIVERYQUANTITY_FLD + ","
				+ PO_PRDeliveryScheduleTable.RECEIVEDQUANTITY_FLD + ","
				+ PO_PRDeliveryScheduleTable.PURCHASEREQUISITIONLINESID_FLD;


				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				odadPCS.SelectCommand = new OleDbCommand(strSql, oconPCS);
				odcbPCS = new OleDbCommandBuilder(odadPCS);
				pData.EnforceConstraints = false;
				odadPCS.Update(pData,PO_PRDeliveryScheduleTable.TABLE_NAME);

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
