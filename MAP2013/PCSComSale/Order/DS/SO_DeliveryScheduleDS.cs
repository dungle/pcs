using System;
using System.Data;
using System.Data.OleDb;
using System.Collections;
using System.Configuration;
using PCSComUtils.DataAccess;
using PCSComUtils.PCSExc;
using PCSComUtils.Common;


namespace PCSComSale.Order.DS
{
	
	public class SO_DeliveryScheduleDS 
	{
		public SO_DeliveryScheduleDS()
		{
		}
		private const string THIS = "PCSComSale.Order.DS.SO_DeliveryScheduleDS";

	
		//**************************************************************************              
		///    <Description>
		///       This method uses to add data to SO_DeliverySchedule
		///    </Description>
		///    <Inputs>
		///        SO_DeliveryScheduleVO       
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
		///       Tuesday, February 01, 2005
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
				SO_DeliveryScheduleVO objObject = (SO_DeliveryScheduleVO) pobjObjectVO;
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand("", oconPCS);
				
				strSql=	"INSERT INTO SO_DeliverySchedule("
				+ SO_DeliveryScheduleTable.REQUIREDDATE_FLD + ","
				+ SO_DeliveryScheduleTable.PROMISEDATE_FLD + ","
				+ SO_DeliveryScheduleTable.SCHEDULEDATE_FLD + ","
				+ SO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD + ","
				+ SO_DeliveryScheduleTable.DELIVERYNO_FLD + ","
				+ SO_DeliveryScheduleTable.LINE_FLD + ","
				+ SO_DeliveryScheduleTable.SALEORDERDETAILID_FLD + ")"
				+ "VALUES(?,?,?,?,?,?,?)";

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_DeliveryScheduleTable.REQUIREDDATE_FLD, OleDbType.Date));
				ocmdPCS.Parameters[SO_DeliveryScheduleTable.REQUIREDDATE_FLD].Value = objObject.RequiredDate;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_DeliveryScheduleTable.PROMISEDATE_FLD, OleDbType.Date));
				ocmdPCS.Parameters[SO_DeliveryScheduleTable.PROMISEDATE_FLD].Value = objObject.PromiseDate;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_DeliveryScheduleTable.SCHEDULEDATE_FLD, OleDbType.Date));
				ocmdPCS.Parameters[SO_DeliveryScheduleTable.SCHEDULEDATE_FLD].Value = objObject.ScheduleDate;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[SO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD].Value = objObject.DeliveryQuantity;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_DeliveryScheduleTable.DELIVERYNO_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[SO_DeliveryScheduleTable.DELIVERYNO_FLD].Value = objObject.DeliveryNo;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_DeliveryScheduleTable.LINE_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[SO_DeliveryScheduleTable.LINE_FLD].Value = objObject.Line;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_DeliveryScheduleTable.SALEORDERDETAILID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[SO_DeliveryScheduleTable.SALEORDERDETAILID_FLD].Value = objObject.SaleOrderDetailID;


				
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
		///       This method uses to delete data from SO_DeliverySchedule
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

		public void DeleteDeliveryDetail (int pintSaleOrderDetailID)
		{
			const string METHOD_NAME = THIS + ".Delete()";
			string strSql = String.Empty;
			strSql=	"DELETE " + SO_DeliveryScheduleTable.TABLE_NAME + " WHERE  " + SO_DeliveryScheduleTable.SALEORDERDETAILID_FLD + "=" + pintSaleOrderDetailID.ToString();
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
		///       This method uses to delete data from SO_DeliverySchedule
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
			strSql=	"DELETE " + SO_DeliveryScheduleTable.TABLE_NAME + " WHERE  " + "DeliveryScheduleID" + "=" + pintID.ToString();
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
		/// DeleteDupleSODelivery
		/// </summary>
		/// <param name="pstrDeliveryID"></param>
		/// <author>Trada</author>
		/// <date>Friday, Feb 17 2006</date>
		public void DeleteDupleSODelivery(string pstrDeliveryID)
		{
			const string METHOD_NAME = THIS + ".DeleteDupleSODelivery()";
			string strSql = String.Empty;
			strSql=	"DELETE " + SO_DeliveryScheduleTable.TABLE_NAME 
				+ " WHERE  " + SO_DeliveryScheduleTable.DELIVERYSCHEDULEID_FLD  + " IN " + pstrDeliveryID;
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
		///       This method uses to get max Line number in the list of the DeliverySchedule
		///    </Description>
		///    <Inputs>
		///               
		///    </Inputs>
		///    <Outputs>
		///       
		///    </Outputs>
		///    <Returns>
		///       
		///    </Returns>
		///    <Authors>
		///       
		///    </Authors>
		///    <History>
		///       Tuesday, February 01, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************

		public int GetMaxDeliveryScheduleLine(int pintSaleOrderDetailID)
		{
			const string METHOD_NAME = THIS + ".GetMaxDeliveryScheduleLine()";
			
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				strSql=	"SELECT max ("
					+ SO_DeliveryScheduleTable.LINE_FLD + ")"
					+ " FROM " + SO_DeliveryScheduleTable.TABLE_NAME
					+ " WHERE " + SO_DeliveryScheduleTable.SALEORDERDETAILID_FLD + "=" + pintSaleOrderDetailID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				string strResult = ocmdPCS.ExecuteScalar().ToString();
				int intResult;
				try 
				{
					intResult = int.Parse(strResult);
				}
				catch
				{
					intResult = 0;
				}
				return intResult;
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
		///       This method uses to get data from SO_DeliverySchedule
		///    </Description>
		///    <Inputs>
		///        ID       
		///    </Inputs>
		///    <Outputs>
		///       SO_DeliveryScheduleVO
		///    </Outputs>
		///    <Returns>
		///       SO_DeliveryScheduleVO
		///    </Returns>
		///    <Authors>
		///       HungLa
		///    </Authors>
		///    <History>
		///       Tuesday, February 01, 2005
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
				+ SO_DeliveryScheduleTable.DELIVERYSCHEDULEID_FLD + ","
				+ SO_DeliveryScheduleTable.REQUIREDDATE_FLD + ","
				+ SO_DeliveryScheduleTable.PROMISEDATE_FLD + ","
				+ SO_DeliveryScheduleTable.SCHEDULEDATE_FLD + ","
				+ SO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD + ","
				+ SO_DeliveryScheduleTable.DELIVERYNO_FLD + ","
				+ SO_DeliveryScheduleTable.LINE_FLD + ","
				+ SO_DeliveryScheduleTable.SALEORDERDETAILID_FLD
				+ " FROM " + SO_DeliveryScheduleTable.TABLE_NAME
				+" WHERE " + SO_DeliveryScheduleTable.DELIVERYSCHEDULEID_FLD + "=" + pintID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();

				SO_DeliveryScheduleVO objObject = new SO_DeliveryScheduleVO();

				while (odrPCS.Read())
				{ 
					objObject.DeliveryScheduleID = int.Parse(odrPCS[SO_DeliveryScheduleTable.DELIVERYSCHEDULEID_FLD].ToString().Trim());
					if (odrPCS[SO_DeliveryScheduleTable.REQUIREDDATE_FLD] != DBNull.Value)
					{
						objObject.RequiredDate = DateTime.Parse(odrPCS[SO_DeliveryScheduleTable.REQUIREDDATE_FLD].ToString().Trim());
					}
					if (odrPCS[SO_DeliveryScheduleTable.PROMISEDATE_FLD] != DBNull.Value)
					{
						objObject.PromiseDate = DateTime.Parse(odrPCS[SO_DeliveryScheduleTable.PROMISEDATE_FLD].ToString().Trim());
					}
					if (odrPCS[SO_DeliveryScheduleTable.SCHEDULEDATE_FLD] != DBNull.Value) 
					{
						objObject.ScheduleDate = DateTime.Parse(odrPCS[SO_DeliveryScheduleTable.SCHEDULEDATE_FLD].ToString().Trim());
					}
					if (odrPCS[SO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD] != DBNull.Value)
					{
						objObject.DeliveryQuantity = Decimal.Parse(odrPCS[SO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD].ToString().Trim());
					}

					if (odrPCS[SO_DeliveryScheduleTable.DELIVERYNO_FLD] != DBNull.Value)
					{
						objObject.DeliveryNo = Decimal.Parse(odrPCS[SO_DeliveryScheduleTable.DELIVERYNO_FLD].ToString().Trim());
					}
					if (odrPCS[SO_DeliveryScheduleTable.LINE_FLD] != DBNull.Value)
					{
						objObject.Line = int.Parse(odrPCS[SO_DeliveryScheduleTable.LINE_FLD].ToString().Trim());
					}
					objObject.SaleOrderDetailID = int.Parse(odrPCS[SO_DeliveryScheduleTable.SALEORDERDETAILID_FLD].ToString().Trim());
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
		///       This method uses to update data to SO_DeliverySchedule
		///    </Description>
		///    <Inputs>
		///       SO_DeliveryScheduleVO       
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

			SO_DeliveryScheduleVO objObject = (SO_DeliveryScheduleVO) pobjObjecVO;
			

			//prepare value for parameters
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				strSql=	"UPDATE SO_DeliverySchedule SET "
				+ SO_DeliveryScheduleTable.REQUIREDDATE_FLD + "=   ?" + ","
				+ SO_DeliveryScheduleTable.PROMISEDATE_FLD + "=   ?" + ","
				+ SO_DeliveryScheduleTable.SCHEDULEDATE_FLD + "=   ?" + ","
				+ SO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD + "=   ?" + ","
				+ SO_DeliveryScheduleTable.DELIVERYNO_FLD + "=   ?" + ","
				+ SO_DeliveryScheduleTable.LINE_FLD + "=   ?" + ","
				+ SO_DeliveryScheduleTable.SALEORDERDETAILID_FLD + "=  ?"
				+" WHERE " + SO_DeliveryScheduleTable.DELIVERYSCHEDULEID_FLD + "= ?";

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_DeliveryScheduleTable.REQUIREDDATE_FLD, OleDbType.Date));
				ocmdPCS.Parameters[SO_DeliveryScheduleTable.REQUIREDDATE_FLD].Value = objObject.RequiredDate;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_DeliveryScheduleTable.PROMISEDATE_FLD, OleDbType.Date));
				ocmdPCS.Parameters[SO_DeliveryScheduleTable.PROMISEDATE_FLD].Value = objObject.PromiseDate;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_DeliveryScheduleTable.SCHEDULEDATE_FLD, OleDbType.Date));
				ocmdPCS.Parameters[SO_DeliveryScheduleTable.SCHEDULEDATE_FLD].Value = objObject.ScheduleDate;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[SO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD].Value = objObject.DeliveryQuantity;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_DeliveryScheduleTable.DELIVERYNO_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[SO_DeliveryScheduleTable.DELIVERYNO_FLD].Value = objObject.DeliveryNo;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_DeliveryScheduleTable.LINE_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[SO_DeliveryScheduleTable.LINE_FLD].Value = objObject.Line;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_DeliveryScheduleTable.SALEORDERDETAILID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[SO_DeliveryScheduleTable.SALEORDERDETAILID_FLD].Value = objObject.SaleOrderDetailID;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_DeliveryScheduleTable.DELIVERYSCHEDULEID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[SO_DeliveryScheduleTable.DELIVERYSCHEDULEID_FLD].Value = objObject.DeliveryScheduleID;

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
		///       This method uses to get all data from SO_DeliverySchedule
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
		///       Tuesday, February 01, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************

		public DataSet GetDeliverySchedule(int pintSaleOrderDetailID)
		{
			const string METHOD_NAME = THIS + ".List()";
			DataSet dstPCS = new DataSet();
			
		

			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				
				strSql=	"SELECT "
					+ SO_DeliveryScheduleTable.TABLE_NAME + "." + SO_DeliveryScheduleTable.DELIVERYSCHEDULEID_FLD + ","
					+ SO_DeliveryScheduleTable.TABLE_NAME + "." + SO_DeliveryScheduleTable.LINE_FLD + ","
					+ SO_DeliveryScheduleTable.REQUIREDDATE_FLD + ","
					+ SO_DeliveryScheduleTable.PROMISEDATE_FLD + ","
					+ SO_DeliveryScheduleTable.SCHEDULEDATE_FLD + ","
					+ SO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD + ","
					+ "(Select SUM(CommitQuantity) From dbo.SO_CommitInventoryDetail Where DeliveryScheduleID = SO_DeliverySchedule.DeliveryScheduleID) as SUMCommitQuantity,"
					+ SO_DeliveryScheduleTable.SALEORDERDETAILID_FLD + ","
					+ SO_CommitInventoryDetailTable.COMMITINVENTORYDETAILID_FLD + ","
					+ SO_DeliveryScheduleTable.TABLE_NAME + "." + SO_DeliveryScheduleTable.GATEID_FLD + ","
					+ "G." + SO_GateTable.CODE_FLD
					+ " FROM " + SO_DeliveryScheduleTable.TABLE_NAME  + " left join " +  SO_CommitInventoryDetailTable.TABLE_NAME + 
					" ON " + SO_DeliveryScheduleTable.TABLE_NAME + "." + SO_DeliveryScheduleTable.DELIVERYSCHEDULEID_FLD +
					" = "  + SO_CommitInventoryDetailTable.TABLE_NAME + "." + SO_CommitInventoryDetailTable.DELIVERYSCHEDULEID_FLD
					+ " LEFT JOIN " +  SO_GateTable.TABLE_NAME + " G ON " + " G." + SO_GateTable.GATEID_FLD + " = " 
					+ SO_DeliveryScheduleTable.TABLE_NAME + "." + SO_DeliveryScheduleTable.GATEID_FLD 
					+ " WHERE " + SO_DeliveryScheduleTable.SALEORDERDETAILID_FLD + "=" + pintSaleOrderDetailID
					+ " ORDER BY " + SO_DeliveryScheduleTable.TABLE_NAME + "." + SO_DeliveryScheduleTable.LINE_FLD;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS,SO_DeliveryScheduleTable.TABLE_NAME);

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
		///       This method uses to get all data from SO_DeliverySchedule
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
		///       Tuesday, February 01, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public DataSet GetDeliveryScheduleBySOMaster(int pintSaleOrderMasterID)
		{
			const string METHOD_NAME = THIS + ".GetDeliveryScheduleBySOMaster()";
			DataSet dstPCS = new DataSet();					

			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				
				strSql=	"SELECT "

					+ SO_DeliveryScheduleTable.TABLE_NAME + "." + SO_DeliveryScheduleTable.DELIVERYNO_FLD + ","
					+ SO_DeliveryScheduleTable.TABLE_NAME + "." + SO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD + ","
					+ SO_DeliveryScheduleTable.TABLE_NAME + "." + SO_DeliveryScheduleTable.DELIVERYSCHEDULEID_FLD + ","
					+ SO_DeliveryScheduleTable.TABLE_NAME + "." + SO_DeliveryScheduleTable.GATEID_FLD + ","
					+ SO_DeliveryScheduleTable.TABLE_NAME + "." + SO_DeliveryScheduleTable.LINE_FLD + ","
					+ SO_DeliveryScheduleTable.TABLE_NAME + "." + SO_DeliveryScheduleTable.PROMISEDATE_FLD + ","
					+ SO_DeliveryScheduleTable.TABLE_NAME + "." + SO_DeliveryScheduleTable.REQUIREDDATE_FLD + ","
					+ SO_DeliveryScheduleTable.TABLE_NAME + "." + SO_DeliveryScheduleTable.SALEORDERDETAILID_FLD + ","
					+ SO_DeliveryScheduleTable.TABLE_NAME + "." + SO_DeliveryScheduleTable.SCHEDULEDATE_FLD + ","

					+ "(SELECT IsNull(SUM(IsNull(CommitQuantity,0)),0) FROM dbo.SO_CommitInventoryDetail WHERE DeliveryScheduleID = SO_DeliverySchedule.DeliveryScheduleID) AS SUMCommitQuantity"

					+ " FROM " + SO_DeliveryScheduleTable.TABLE_NAME  
					+ " INNER JOIN " +  SO_SaleOrderDetailTable.TABLE_NAME
					+ " ON " + SO_DeliveryScheduleTable.TABLE_NAME + "." + SO_DeliveryScheduleTable.SALEORDERDETAILID_FLD
					+ " = "  + SO_SaleOrderDetailTable.TABLE_NAME + "." + SO_SaleOrderDetailTable.SALEORDERDETAILID_FLD
					+ " WHERE " + SO_SaleOrderDetailTable.SALEORDERMASTERID_FLD + "=" + pintSaleOrderMasterID;
				
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS,SO_DeliveryScheduleTable.TABLE_NAME);

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
		///       This method uses to get all data from SO_DeliverySchedule
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
		///       Tuesday, February 01, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public void UpdateDeletedRowInDataSet(DataSet pData, int pintSaleOrderMasterID)
		{
			const string METHOD_NAME = THIS + ".UpdateDeletedRowInDataSet()";
			string strSql;
			OleDbConnection oconPCS =null;
			OleDbCommandBuilder odcbPCS ;
			OleDbDataAdapter odadPCS = new OleDbDataAdapter();

			try
			{
				strSql=	"SELECT "

					+ SO_DeliveryScheduleTable.TABLE_NAME + "." + SO_DeliveryScheduleTable.DELIVERYNO_FLD + ","
					+ SO_DeliveryScheduleTable.TABLE_NAME + "." + SO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD + ","
					+ SO_DeliveryScheduleTable.TABLE_NAME + "." + SO_DeliveryScheduleTable.DELIVERYSCHEDULEID_FLD + ","
					+ SO_DeliveryScheduleTable.TABLE_NAME + "." + SO_DeliveryScheduleTable.GATEID_FLD + ","
					+ SO_DeliveryScheduleTable.TABLE_NAME + "." + SO_DeliveryScheduleTable.LINE_FLD + ","
					+ SO_DeliveryScheduleTable.TABLE_NAME + "." + SO_DeliveryScheduleTable.PROMISEDATE_FLD + ","
					+ SO_DeliveryScheduleTable.TABLE_NAME + "." + SO_DeliveryScheduleTable.REQUIREDDATE_FLD + ","
					+ SO_DeliveryScheduleTable.TABLE_NAME + "." + SO_DeliveryScheduleTable.SALEORDERDETAILID_FLD + ","
					+ SO_DeliveryScheduleTable.TABLE_NAME + "." + SO_DeliveryScheduleTable.SCHEDULEDATE_FLD

					+ " FROM " + SO_DeliveryScheduleTable.TABLE_NAME;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				odadPCS.SelectCommand = new OleDbCommand(strSql, oconPCS);
				odcbPCS = new OleDbCommandBuilder(odadPCS);
				pData.EnforceConstraints = false;
				odadPCS.Update(pData.Tables[0].Select(string.Empty,string.Empty,DataViewRowState.Deleted));
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
		///       This method uses to get all data from SO_DeliverySchedule
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
		///       Tuesday, February 01, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public void UpdateInsertedRowInDataSet(DataSet pData, int pintSaleOrderMasterID)
		{
			const string METHOD_NAME = THIS + ".UpdateDeletedRowInDataSet()";
			string strSql;
			OleDbConnection oconPCS =null;
			OleDbCommandBuilder odcbPCS ;
			OleDbDataAdapter odadPCS = new OleDbDataAdapter();

			try
			{
				strSql=	"SELECT "

					+ SO_DeliveryScheduleTable.TABLE_NAME + "." + SO_DeliveryScheduleTable.DELIVERYNO_FLD + ","
					+ SO_DeliveryScheduleTable.TABLE_NAME + "." + SO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD + ","
					+ SO_DeliveryScheduleTable.TABLE_NAME + "." + SO_DeliveryScheduleTable.DELIVERYSCHEDULEID_FLD + ","
					+ SO_DeliveryScheduleTable.TABLE_NAME + "." + SO_DeliveryScheduleTable.GATEID_FLD + ","
					+ SO_DeliveryScheduleTable.TABLE_NAME + "." + SO_DeliveryScheduleTable.LINE_FLD + ","
					+ SO_DeliveryScheduleTable.TABLE_NAME + "." + SO_DeliveryScheduleTable.PROMISEDATE_FLD + ","
					+ SO_DeliveryScheduleTable.TABLE_NAME + "." + SO_DeliveryScheduleTable.REQUIREDDATE_FLD + ","
					+ SO_DeliveryScheduleTable.TABLE_NAME + "." + SO_DeliveryScheduleTable.SALEORDERDETAILID_FLD + ","
					+ SO_DeliveryScheduleTable.TABLE_NAME + "." + SO_DeliveryScheduleTable.SCHEDULEDATE_FLD

					+ " FROM " + SO_DeliveryScheduleTable.TABLE_NAME; 

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				odadPCS.SelectCommand = new OleDbCommand(strSql, oconPCS);
				odcbPCS = new OleDbCommandBuilder(odadPCS);
				pData.EnforceConstraints = false;
				odadPCS.Update(pData.Tables[0]);
			}
			catch(OleDbException ex)
			{
				if (ex.Errors.Count > 1)
				{
					if (ex.Errors[1].NativeError == ErrorCode.SQLDUPLICATE_KEYCODE)
						throw new PCSDBException(ErrorCode.DUPLICATE_KEY, METHOD_NAME, ex);
					else if (ex.Errors[1].NativeError == ErrorCode.SQLCASCADE_PREVENT_KEYCODE)
						throw new PCSDBException(ErrorCode.CASCADE_DELETE_PREVENT, METHOD_NAME, ex);
					else
						throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME,ex);
				}
				else
					throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME,ex);
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
		///       This method uses to get all data from SO_DeliverySchedule
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
		///       Tuesday, February 01, 2005
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
				+ SO_DeliveryScheduleTable.DELIVERYSCHEDULEID_FLD + ","
				+ SO_DeliveryScheduleTable.REQUIREDDATE_FLD + ","
				+ SO_DeliveryScheduleTable.PROMISEDATE_FLD + ","
				+ SO_DeliveryScheduleTable.SCHEDULEDATE_FLD + ","
				+ SO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD + ","
				+ SO_DeliveryScheduleTable.DELIVERYNO_FLD + ","
				+ SO_DeliveryScheduleTable.LINE_FLD + ","
				+ SO_DeliveryScheduleTable.SALEORDERDETAILID_FLD
					+ " FROM " + SO_DeliveryScheduleTable.TABLE_NAME;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS,SO_DeliveryScheduleTable.TABLE_NAME);

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
		///       Tuesday, February 01, 2005
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
				+ SO_DeliveryScheduleTable.DELIVERYSCHEDULEID_FLD + ","
				+ SO_DeliveryScheduleTable.REQUIREDDATE_FLD + ","
				+ SO_DeliveryScheduleTable.PROMISEDATE_FLD + ","
				+ SO_DeliveryScheduleTable.SCHEDULEDATE_FLD + ","
				+ SO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD + ","
				+ SO_DeliveryScheduleTable.DELIVERYNO_FLD + ","
				+ SO_DeliveryScheduleTable.LINE_FLD + ","
				+ SO_DeliveryScheduleTable.GATEID_FLD + ","
				+ SO_DeliveryScheduleTable.SALEORDERDETAILID_FLD 
				+ "  FROM " + SO_DeliveryScheduleTable.TABLE_NAME;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				odadPCS.SelectCommand = new OleDbCommand(strSql, oconPCS);
				odcbPCS = new OleDbCommandBuilder(odadPCS);
				pData.EnforceConstraints = false;
				odadPCS.Update(pData,SO_DeliveryScheduleTable.TABLE_NAME);

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
		///       Tuesday, February 01, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		
		public void UpdateDeliveryDataSet(DataSet pData, int pintSaleOrderDetailID)
		{
			const string METHOD_NAME = THIS + ".UpdateDataSet()";
			string strSql;
			OleDbConnection oconPCS =null;
			OleDbCommandBuilder odcbPCS ;
			OleDbDataAdapter odadPCS = new OleDbDataAdapter();

			try
			{
				strSql=	"SELECT "
					+ SO_DeliveryScheduleTable.DELIVERYSCHEDULEID_FLD + ","
					+ SO_DeliveryScheduleTable.REQUIREDDATE_FLD + ","
					+ SO_DeliveryScheduleTable.PROMISEDATE_FLD + ","
					+ SO_DeliveryScheduleTable.SCHEDULEDATE_FLD + ","
					+ SO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD + ","
					//+ SO_DeliveryScheduleTable.DELIVERYNO_FLD + ","
					+ SO_DeliveryScheduleTable.LINE_FLD + ","
					+ SO_DeliveryScheduleTable.GATEID_FLD + ","
					+ SO_DeliveryScheduleTable.SALEORDERDETAILID_FLD 
					+ "  FROM " + SO_DeliveryScheduleTable.TABLE_NAME;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				odadPCS.SelectCommand = new OleDbCommand(strSql, oconPCS);
				odcbPCS = new OleDbCommandBuilder(odadPCS);
				pData.EnforceConstraints = false;
				odadPCS.Update(pData,SO_DeliveryScheduleTable.TABLE_NAME);
				
				//Reload data from database 
				strSql += " WHERE " + SO_DeliveryScheduleTable.SALEORDERDETAILID_FLD + "=" + pintSaleOrderDetailID;
				pData.Clear();
				odadPCS.SelectCommand.CommandText = strSql;
				odadPCS.SelectCommand.CommandType = CommandType.Text;
				odadPCS.Fill(pData,pData.Tables[0].TableName);
				pData.AcceptChanges();

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
		///       This method uses to get all data from SO_DeliverySchedule
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
		///       DuongNA
		///    </Authors>
		///    <History>
		///       Monday, October 24, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************

		public DataSet ListForImport(int pintMasterID)
		{
			const string METHOD_NAME = THIS + ".ListForImport()";
			const string MASTER_ID_PRM = "SaleOrderMasterID";
			DataSet dstPCS = new DataSet();
			
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				
				strSql=	"SELECT "
					+ "A." + SO_DeliveryScheduleTable.DELIVERYSCHEDULEID_FLD + ","
					+ "A." + SO_DeliveryScheduleTable.REQUIREDDATE_FLD + ","
					+ "A." + SO_DeliveryScheduleTable.PROMISEDATE_FLD + ","
					+ "A." + SO_DeliveryScheduleTable.SCHEDULEDATE_FLD + ","
					+ "A." + SO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD + ","
					+ "A." + SO_DeliveryScheduleTable.DELIVERYNO_FLD + ","
					+ "A." + SO_DeliveryScheduleTable.LINE_FLD + ","
					+ "A." + SO_DeliveryScheduleTable.GATEID_FLD + ","
					+ "A." + SO_DeliveryScheduleTable.SALEORDERDETAILID_FLD
					+ " FROM " + SO_DeliveryScheduleTable.TABLE_NAME + " A "
					+ " INNER JOIN " + SO_SaleOrderDetailTable.TABLE_NAME + " B "
					+ " ON " + "A." + SO_DeliveryScheduleTable.SALEORDERDETAILID_FLD + "=" + "B." + SO_SaleOrderDetailTable.SALEORDERDETAILID_FLD
					+ " INNER JOIN " + SO_SaleOrderMasterTable.TABLE_NAME + " C "
					+ " ON " + "B." + SO_SaleOrderDetailTable.SALEORDERMASTERID_FLD + "=" + "C." + SO_SaleOrderMasterTable.SALEORDERMASTERID_FLD
					+ " WHERE C." + SO_SaleOrderMasterTable.SALEORDERMASTERID_FLD + "=?";

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				ocmdPCS.Parameters.AddWithValue(MASTER_ID_PRM,pintMasterID);
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS,SO_DeliveryScheduleTable.TABLE_NAME);

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
