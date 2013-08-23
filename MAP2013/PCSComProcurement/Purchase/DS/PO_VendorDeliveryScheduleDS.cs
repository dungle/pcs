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
	public class PO_VendorDeliveryScheduleDS 
	{
		public PO_VendorDeliveryScheduleDS()
		{
		}
		private const string THIS = "PCSComProcurement.Purchase.DS.PO_VendorDeliveryScheduleDS";
		//**************************************************************************              
		///    <Description>
		///       This method uses to add data to PO_VendorDeliverySchedule
		///    </Description>
		///    <Inputs>
		///        PO_VendorDeliveryScheduleVO       
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
		///       Tuesday, February 07, 2006
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
				PO_VendorDeliveryScheduleVO objObject = (PO_VendorDeliveryScheduleVO) pobjObjectVO;
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand("", oconPCS);
				
				strSql=	"INSERT INTO PO_VendorDeliverySchedule("
				+ PO_VendorDeliveryScheduleTable.PARTYID_FLD + ","
				+ PO_VendorDeliveryScheduleTable.CCNID_FLD + ","
				+ PO_VendorDeliveryScheduleTable.DELIVERYTYPE_FLD + ","
				+ PO_VendorDeliveryScheduleTable.DELHOUR_FLD + ","
				+ PO_VendorDeliveryScheduleTable.WEEKLYDAY_FLD + ","
				+ PO_VendorDeliveryScheduleTable.MONTHLYDATE_FLD + ","
				+ PO_VendorDeliveryScheduleTable.COMMENT_FLD + ","
				+ PO_VendorDeliveryScheduleTable.DELMIN_FLD + ","
				+ PO_VendorDeliveryScheduleTable.PRODUCTID_FLD + ")"
				+ "VALUES(?,?,?,?,?,?,?,?,?)";

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_VendorDeliveryScheduleTable.PARTYID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_VendorDeliveryScheduleTable.PARTYID_FLD].Value = objObject.PartyID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_VendorDeliveryScheduleTable.CCNID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_VendorDeliveryScheduleTable.CCNID_FLD].Value = objObject.CCNID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_VendorDeliveryScheduleTable.DELIVERYTYPE_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_VendorDeliveryScheduleTable.DELIVERYTYPE_FLD].Value = objObject.DeliveryType;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_VendorDeliveryScheduleTable.DELHOUR_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_VendorDeliveryScheduleTable.DELHOUR_FLD].Value = objObject.DelHour;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_VendorDeliveryScheduleTable.WEEKLYDAY_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_VendorDeliveryScheduleTable.WEEKLYDAY_FLD].Value = objObject.WeeklyDay;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_VendorDeliveryScheduleTable.MONTHLYDATE_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_VendorDeliveryScheduleTable.MONTHLYDATE_FLD].Value = objObject.MonthlyDate;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_VendorDeliveryScheduleTable.COMMENT_FLD, OleDbType.WChar));
				ocmdPCS.Parameters[PO_VendorDeliveryScheduleTable.COMMENT_FLD].Value = objObject.Comment;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_VendorDeliveryScheduleTable.DELMIN_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_VendorDeliveryScheduleTable.DELMIN_FLD].Value = objObject.DelMin;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_VendorDeliveryScheduleTable.PRODUCTID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_VendorDeliveryScheduleTable.PRODUCTID_FLD].Value = objObject.ProductID;


				
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
		///       This method uses to delete data from PO_VendorDeliverySchedule
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
			strSql=	"DELETE " + PO_VendorDeliveryScheduleTable.TABLE_NAME + " WHERE  " + "VendorDeliverySchedule" + "=" + pintID.ToString();
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
				if (ex.Errors.Count > 1)
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
	
		//**************************************************************************              
		///    <Description>
		///       This method uses to get data from PO_VendorDeliverySchedule
		///    </Description>
		///    <Inputs>
		///        ID       
		///    </Inputs>
		///    <Outputs>
		///       PO_VendorDeliveryScheduleVO
		///    </Outputs>
		///    <Returns>
		///       PO_VendorDeliveryScheduleVO
		///    </Returns>
		///    <Authors>
		///       HungLa
		///    </Authors>
		///    <History>
		///       Tuesday, February 07, 2006
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
				+ PO_VendorDeliveryScheduleTable.VENDORDELIVERYSCHEDULE_FLD + ","
				+ PO_VendorDeliveryScheduleTable.PARTYID_FLD + ","
				+ PO_VendorDeliveryScheduleTable.CCNID_FLD + ","
				+ PO_VendorDeliveryScheduleTable.DELIVERYTYPE_FLD + ","
				+ PO_VendorDeliveryScheduleTable.DELHOUR_FLD + ","
				+ PO_VendorDeliveryScheduleTable.WEEKLYDAY_FLD + ","
				+ PO_VendorDeliveryScheduleTable.MONTHLYDATE_FLD + ","
				+ PO_VendorDeliveryScheduleTable.COMMENT_FLD + ","
				+ PO_VendorDeliveryScheduleTable.DELMIN_FLD + ","
				+ PO_VendorDeliveryScheduleTable.PRODUCTID_FLD
				+ " FROM " + PO_VendorDeliveryScheduleTable.TABLE_NAME
				+" WHERE " + PO_VendorDeliveryScheduleTable.VENDORDELIVERYSCHEDULE_FLD + "=" + pintID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();

				PO_VendorDeliveryScheduleVO objObject = new PO_VendorDeliveryScheduleVO();

				while (odrPCS.Read())
				{ 
				objObject.VendorDeliverySchedule = int.Parse(odrPCS[PO_VendorDeliveryScheduleTable.VENDORDELIVERYSCHEDULE_FLD].ToString().Trim());
				objObject.PartyID = int.Parse(odrPCS[PO_VendorDeliveryScheduleTable.PARTYID_FLD].ToString().Trim());
				objObject.CCNID = int.Parse(odrPCS[PO_VendorDeliveryScheduleTable.CCNID_FLD].ToString().Trim());
				objObject.DeliveryType = int.Parse(odrPCS[PO_VendorDeliveryScheduleTable.DELIVERYTYPE_FLD].ToString().Trim());
				objObject.DelHour = int.Parse(odrPCS[PO_VendorDeliveryScheduleTable.DELHOUR_FLD].ToString().Trim());
				objObject.WeeklyDay = int.Parse(odrPCS[PO_VendorDeliveryScheduleTable.WEEKLYDAY_FLD].ToString().Trim());
				objObject.MonthlyDate = int.Parse(odrPCS[PO_VendorDeliveryScheduleTable.MONTHLYDATE_FLD].ToString().Trim());
				objObject.Comment = odrPCS[PO_VendorDeliveryScheduleTable.COMMENT_FLD].ToString().Trim();
				objObject.DelMin = int.Parse(odrPCS[PO_VendorDeliveryScheduleTable.DELMIN_FLD].ToString().Trim());
				objObject.ProductID = int.Parse(odrPCS[PO_VendorDeliveryScheduleTable.PRODUCTID_FLD].ToString().Trim());

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
		///       This method uses to update data to PO_VendorDeliverySchedule
		///    </Description>
		///    <Inputs>
		///       PO_VendorDeliveryScheduleVO       
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

			PO_VendorDeliveryScheduleVO objObject = (PO_VendorDeliveryScheduleVO) pobjObjecVO;
			

			//prepare value for parameters
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				strSql=	"UPDATE PO_VendorDeliverySchedule SET "
				+ PO_VendorDeliveryScheduleTable.PARTYID_FLD + "=   ?" + ","
				+ PO_VendorDeliveryScheduleTable.CCNID_FLD + "=   ?" + ","
				+ PO_VendorDeliveryScheduleTable.DELIVERYTYPE_FLD + "=   ?" + ","
				+ PO_VendorDeliveryScheduleTable.DELHOUR_FLD + "=   ?" + ","
				+ PO_VendorDeliveryScheduleTable.WEEKLYDAY_FLD + "=   ?" + ","
				+ PO_VendorDeliveryScheduleTable.MONTHLYDATE_FLD + "=   ?" + ","
				+ PO_VendorDeliveryScheduleTable.COMMENT_FLD + "=   ?" + ","
				+ PO_VendorDeliveryScheduleTable.DELMIN_FLD + "=   ?" + ","
				+ PO_VendorDeliveryScheduleTable.PRODUCTID_FLD + "=  ?"
				+" WHERE " + PO_VendorDeliveryScheduleTable.VENDORDELIVERYSCHEDULE_FLD + "= ?";

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_VendorDeliveryScheduleTable.PARTYID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_VendorDeliveryScheduleTable.PARTYID_FLD].Value = objObject.PartyID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_VendorDeliveryScheduleTable.CCNID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_VendorDeliveryScheduleTable.CCNID_FLD].Value = objObject.CCNID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_VendorDeliveryScheduleTable.DELIVERYTYPE_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_VendorDeliveryScheduleTable.DELIVERYTYPE_FLD].Value = objObject.DeliveryType;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_VendorDeliveryScheduleTable.DELHOUR_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_VendorDeliveryScheduleTable.DELHOUR_FLD].Value = objObject.DelHour;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_VendorDeliveryScheduleTable.WEEKLYDAY_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_VendorDeliveryScheduleTable.WEEKLYDAY_FLD].Value = objObject.WeeklyDay;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_VendorDeliveryScheduleTable.MONTHLYDATE_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_VendorDeliveryScheduleTable.MONTHLYDATE_FLD].Value = objObject.MonthlyDate;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_VendorDeliveryScheduleTable.COMMENT_FLD, OleDbType.WChar));
				ocmdPCS.Parameters[PO_VendorDeliveryScheduleTable.COMMENT_FLD].Value = objObject.Comment;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_VendorDeliveryScheduleTable.DELMIN_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_VendorDeliveryScheduleTable.DELMIN_FLD].Value = objObject.DelMin;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_VendorDeliveryScheduleTable.PRODUCTID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_VendorDeliveryScheduleTable.PRODUCTID_FLD].Value = objObject.ProductID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_VendorDeliveryScheduleTable.VENDORDELIVERYSCHEDULE_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_VendorDeliveryScheduleTable.VENDORDELIVERYSCHEDULE_FLD].Value = objObject.VendorDeliverySchedule;


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
		///       This method uses to get all data from PO_VendorDeliverySchedule
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
		///       Tuesday, February 07, 2006
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
				+ PO_VendorDeliveryScheduleTable.VENDORDELIVERYSCHEDULE_FLD + ","
				+ PO_VendorDeliveryScheduleTable.PARTYID_FLD + ","
				+ PO_VendorDeliveryScheduleTable.CCNID_FLD + ","
				+ PO_VendorDeliveryScheduleTable.DELIVERYTYPE_FLD + ","
				+ PO_VendorDeliveryScheduleTable.DELHOUR_FLD + ","
				+ PO_VendorDeliveryScheduleTable.WEEKLYDAY_FLD + ","
				+ PO_VendorDeliveryScheduleTable.MONTHLYDATE_FLD + ","
				+ PO_VendorDeliveryScheduleTable.COMMENT_FLD + ","
				+ PO_VendorDeliveryScheduleTable.DELMIN_FLD + ","
				+ PO_VendorDeliveryScheduleTable.PRODUCTID_FLD
					+ " FROM " + PO_VendorDeliveryScheduleTable.TABLE_NAME;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS,PO_VendorDeliveryScheduleTable.TABLE_NAME);

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
		/// GetVendorDeliverySchedule
		/// </summary>
		/// <returns></returns>
		/// <author>Trada</author>
		/// <date>Tuesday, Feb 7 2007</date>
		public DataSet GetVendorDeliverySchedule()
		{
			const string METHOD_NAME = THIS + ".GetVendorDeliverySchedule()";
			DataSet dstPCS = new DataSet();
			
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				
				strSql=	"SELECT GetDate() Time,"
					+ " VDS." + PO_VendorDeliveryScheduleTable.VENDORDELIVERYSCHEDULE_FLD + ","
					+ " VDS." + PO_VendorDeliveryScheduleTable.PARTYID_FLD + ","
					+ " VDS." + PO_VendorDeliveryScheduleTable.CCNID_FLD + ","
					+ " VDS." + PO_VendorDeliveryScheduleTable.DELIVERYTYPE_FLD + ","
					+ " VDS." + PO_VendorDeliveryScheduleTable.DELHOUR_FLD + ","
					+ " VDS." + PO_VendorDeliveryScheduleTable.WEEKLYDAY_FLD + ","
					+ " VDS." + PO_VendorDeliveryScheduleTable.MONTHLYDATE_FLD + ","
					+ " VDS." + PO_VendorDeliveryScheduleTable.DELMIN_FLD + ","
					+ " CA." + ITM_CategoryTable.CODE_FLD + Constants.WHITE_SPACE + ITM_CategoryTable.TABLE_NAME + ITM_CategoryTable.CODE_FLD + ", "
					+ " P." + ITM_ProductTable.CODE_FLD + Constants.WHITE_SPACE + ITM_ProductTable.TABLE_NAME + ITM_ProductTable.CODE_FLD + ","
					+ " P." + ITM_ProductTable.DESCRIPTION_FLD + Constants.WHITE_SPACE + ITM_ProductTable.TABLE_NAME + ITM_ProductTable.DESCRIPTION_FLD + ","
					+ " P." + ITM_ProductTable.REVISION_FLD + Constants.WHITE_SPACE + ITM_ProductTable.TABLE_NAME + ITM_ProductTable.REVISION_FLD + ","
					+ " VDS." + PO_VendorDeliveryScheduleTable.COMMENT_FLD + ","
					+ " VDS." + PO_VendorDeliveryScheduleTable.PRODUCTID_FLD
					+ " FROM " + PO_VendorDeliveryScheduleTable.TABLE_NAME + " VDS "
					+ " INNER JOIN " + ITM_ProductTable.TABLE_NAME + " P " 
					+ " ON P." + ITM_ProductTable.PRODUCTID_FLD + " = VDS." + PO_VendorDeliveryScheduleTable.PRODUCTID_FLD
					+ " LEFT JOIN " + ITM_CategoryTable.TABLE_NAME + " CA ON CA." + ITM_CategoryTable.CATEGORYID_FLD 
					+ " = P." + ITM_ProductTable.CATEGORYID_FLD;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS,PO_VendorDeliveryScheduleTable.TABLE_NAME);

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
		///       Tuesday, February 07, 2006
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
				+ PO_VendorDeliveryScheduleTable.VENDORDELIVERYSCHEDULE_FLD + ","
				+ PO_VendorDeliveryScheduleTable.PARTYID_FLD + ","
				+ PO_VendorDeliveryScheduleTable.CCNID_FLD + ","
				+ PO_VendorDeliveryScheduleTable.DELIVERYTYPE_FLD + ","
				+ PO_VendorDeliveryScheduleTable.DELHOUR_FLD + ","
				+ PO_VendorDeliveryScheduleTable.WEEKLYDAY_FLD + ","
				+ PO_VendorDeliveryScheduleTable.MONTHLYDATE_FLD + ","
				+ PO_VendorDeliveryScheduleTable.COMMENT_FLD + ","
				+ PO_VendorDeliveryScheduleTable.DELMIN_FLD + ","
				+ PO_VendorDeliveryScheduleTable.PRODUCTID_FLD 
		+ "  FROM " + PO_VendorDeliveryScheduleTable.TABLE_NAME;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				odadPCS.SelectCommand = new OleDbCommand(strSql, oconPCS);
				odcbPCS = new OleDbCommandBuilder(odadPCS);
				pData.EnforceConstraints = false;
				odadPCS.Update(pData,PO_VendorDeliveryScheduleTable.TABLE_NAME);

			}
			catch(OleDbException ex)
			{
				if(ex.Errors.Count > 1)
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
		///       Tuesday, February 07, 2006
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		
		public void SaveDeliveryHour(DataTable pdtbData)
		{
			const string METHOD_NAME = THIS + ".SaveDeliveryHour()";
			string strSql;
			OleDbConnection oconPCS =null;
			OleDbCommandBuilder odcbPCS ;
			OleDbDataAdapter odadPCS = new OleDbDataAdapter();

			try
			{
				strSql=	"SELECT "
					+ PO_VendorDeliveryScheduleTable.VENDORDELIVERYSCHEDULE_FLD + ","
					+ PO_VendorDeliveryScheduleTable.PARTYID_FLD + ","
					+ PO_VendorDeliveryScheduleTable.CCNID_FLD + ","
					+ PO_VendorDeliveryScheduleTable.DELIVERYTYPE_FLD + ","
					+ PO_VendorDeliveryScheduleTable.DELHOUR_FLD + ","
					+ PO_VendorDeliveryScheduleTable.WEEKLYDAY_FLD + ","
					+ PO_VendorDeliveryScheduleTable.MONTHLYDATE_FLD + ","
					+ PO_VendorDeliveryScheduleTable.COMMENT_FLD + ","
					+ PO_VendorDeliveryScheduleTable.DELMIN_FLD + ","
					+ PO_VendorDeliveryScheduleTable.PRODUCTID_FLD 
					+ "  FROM " + PO_VendorDeliveryScheduleTable.TABLE_NAME;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				odadPCS.SelectCommand = new OleDbCommand(strSql, oconPCS);
				odcbPCS = new OleDbCommandBuilder(odadPCS);
				odadPCS.Update(pdtbData);
			}
			catch(OleDbException ex)
			{
				if(ex.Errors.Count > 1)
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


	}
}
