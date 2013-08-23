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
	
	public class PO_ItemVendorReferenceDetailDS 
	{
		public PO_ItemVendorReferenceDetailDS()
		{
		}
		private const string THIS = "PCSComProcurement.Purchase.DS.DS.PO_ItemVendorReferenceDetailDS";

	
		//**************************************************************************              
		///    <Description>
		///       This method uses to add data to PO_ItemVendorReferenceDetail
		///    </Description>
		///    <Inputs>
		///        PO_ItemVendorReferenceDetailVO       
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
				PO_ItemVendorReferenceDetailVO objObject = (PO_ItemVendorReferenceDetailVO) pobjObjectVO;
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand("", oconPCS);
				
				strSql=	"INSERT INTO PO_ItemVendorReferenceDetail("
				+ PO_ItemVendorReferenceDetailTable.ITEMVENDORREFERENCEID_FLD + ","
				+ PO_ItemVendorReferenceDetailTable.ENDDATE_FLD + ","
				+ PO_ItemVendorReferenceDetailTable.UNITPRICE_FLD + ","
				+ PO_ItemVendorReferenceDetailTable.FROMQUANTITY_FLD + ","
				+ PO_ItemVendorReferenceDetailTable.TOQUANTITY_FLD + ","
				+ PO_ItemVendorReferenceDetailTable.FROMPRICE_FLD + ","
				+ PO_ItemVendorReferenceDetailTable.TOPRICE_FLD + ")"
				+ "VALUES(?,?,?,?,?,?,?)";

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_ItemVendorReferenceDetailTable.ITEMVENDORREFERENCEID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_ItemVendorReferenceDetailTable.ITEMVENDORREFERENCEID_FLD].Value = objObject.ItemVendorReferenceID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_ItemVendorReferenceDetailTable.ENDDATE_FLD, OleDbType.Date));
				ocmdPCS.Parameters[PO_ItemVendorReferenceDetailTable.ENDDATE_FLD].Value = objObject.EndDate;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_ItemVendorReferenceDetailTable.UNITPRICE_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PO_ItemVendorReferenceDetailTable.UNITPRICE_FLD].Value = objObject.UnitPrice;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_ItemVendorReferenceDetailTable.FROMQUANTITY_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PO_ItemVendorReferenceDetailTable.FROMQUANTITY_FLD].Value = objObject.FromQuantity;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_ItemVendorReferenceDetailTable.TOQUANTITY_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PO_ItemVendorReferenceDetailTable.TOQUANTITY_FLD].Value = objObject.ToQuantity;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_ItemVendorReferenceDetailTable.FROMPRICE_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PO_ItemVendorReferenceDetailTable.FROMPRICE_FLD].Value = objObject.FromPrice;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_ItemVendorReferenceDetailTable.TOPRICE_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PO_ItemVendorReferenceDetailTable.TOPRICE_FLD].Value = objObject.ToPrice;


				
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
		///       This method uses to delete data from PO_ItemVendorReferenceDetail
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
			strSql=	"DELETE " + PO_ItemVendorReferenceDetailTable.TABLE_NAME + " WHERE  " + "ItemVendorReferenceDetailID" + "=" + pintID.ToString();
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
		///       This method uses to get data from PO_ItemVendorReferenceDetail
		///    </Description>
		///    <Inputs>
		///        ID       
		///    </Inputs>
		///    <Outputs>
		///       PO_ItemVendorReferenceDetailVO
		///    </Outputs>
		///    <Returns>
		///       PO_ItemVendorReferenceDetailVO
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
				+ PO_ItemVendorReferenceDetailTable.ITEMVENDORREFERENCEDETAILID_FLD + ","
				+ PO_ItemVendorReferenceDetailTable.ITEMVENDORREFERENCEID_FLD + ","
				+ PO_ItemVendorReferenceDetailTable.ENDDATE_FLD + ","
				+ PO_ItemVendorReferenceDetailTable.UNITPRICE_FLD + ","
				+ PO_ItemVendorReferenceDetailTable.FROMQUANTITY_FLD + ","
				+ PO_ItemVendorReferenceDetailTable.TOQUANTITY_FLD + ","
				+ PO_ItemVendorReferenceDetailTable.FROMPRICE_FLD + ","
				+ PO_ItemVendorReferenceDetailTable.TOPRICE_FLD
				+ " FROM " + PO_ItemVendorReferenceDetailTable.TABLE_NAME
				+" WHERE " + PO_ItemVendorReferenceDetailTable.ITEMVENDORREFERENCEDETAILID_FLD + "=" + pintID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();

				PO_ItemVendorReferenceDetailVO objObject = new PO_ItemVendorReferenceDetailVO();

				while (odrPCS.Read())
				{ 
				objObject.ItemVendorReferenceDetailID = int.Parse(odrPCS[PO_ItemVendorReferenceDetailTable.ITEMVENDORREFERENCEDETAILID_FLD].ToString());
				objObject.ItemVendorReferenceID = int.Parse(odrPCS[PO_ItemVendorReferenceDetailTable.ITEMVENDORREFERENCEID_FLD].ToString());
				objObject.EndDate = DateTime.Parse(odrPCS[PO_ItemVendorReferenceDetailTable.ENDDATE_FLD].ToString());
				objObject.UnitPrice = Decimal.Parse(odrPCS[PO_ItemVendorReferenceDetailTable.UNITPRICE_FLD].ToString());
				objObject.FromQuantity = Decimal.Parse(odrPCS[PO_ItemVendorReferenceDetailTable.FROMQUANTITY_FLD].ToString());
				objObject.ToQuantity = Decimal.Parse(odrPCS[PO_ItemVendorReferenceDetailTable.TOQUANTITY_FLD].ToString());
				objObject.FromPrice = Decimal.Parse(odrPCS[PO_ItemVendorReferenceDetailTable.FROMPRICE_FLD].ToString());
				objObject.ToPrice = Decimal.Parse(odrPCS[PO_ItemVendorReferenceDetailTable.TOPRICE_FLD].ToString());

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
		///       This method uses to update data to PO_ItemVendorReferenceDetail
		///    </Description>
		///    <Inputs>
		///       PO_ItemVendorReferenceDetailVO       
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

			PO_ItemVendorReferenceDetailVO objObject = (PO_ItemVendorReferenceDetailVO) pobjObjecVO;
			

			//prepare value for parameters
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				strSql=	"UPDATE PO_ItemVendorReferenceDetail SET "
				+ PO_ItemVendorReferenceDetailTable.ITEMVENDORREFERENCEID_FLD + "=   ?" + ","
				+ PO_ItemVendorReferenceDetailTable.ENDDATE_FLD + "=   ?" + ","
				+ PO_ItemVendorReferenceDetailTable.UNITPRICE_FLD + "=   ?" + ","
				+ PO_ItemVendorReferenceDetailTable.FROMQUANTITY_FLD + "=   ?" + ","
				+ PO_ItemVendorReferenceDetailTable.TOQUANTITY_FLD + "=   ?" + ","
				+ PO_ItemVendorReferenceDetailTable.FROMPRICE_FLD + "=   ?" + ","
				+ PO_ItemVendorReferenceDetailTable.TOPRICE_FLD + "=  ?"
				+" WHERE " + PO_ItemVendorReferenceDetailTable.ITEMVENDORREFERENCEDETAILID_FLD + "= ?";

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_ItemVendorReferenceDetailTable.ITEMVENDORREFERENCEID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_ItemVendorReferenceDetailTable.ITEMVENDORREFERENCEID_FLD].Value = objObject.ItemVendorReferenceID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_ItemVendorReferenceDetailTable.ENDDATE_FLD, OleDbType.Date));
				ocmdPCS.Parameters[PO_ItemVendorReferenceDetailTable.ENDDATE_FLD].Value = objObject.EndDate;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_ItemVendorReferenceDetailTable.UNITPRICE_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PO_ItemVendorReferenceDetailTable.UNITPRICE_FLD].Value = objObject.UnitPrice;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_ItemVendorReferenceDetailTable.FROMQUANTITY_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PO_ItemVendorReferenceDetailTable.FROMQUANTITY_FLD].Value = objObject.FromQuantity;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_ItemVendorReferenceDetailTable.TOQUANTITY_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PO_ItemVendorReferenceDetailTable.TOQUANTITY_FLD].Value = objObject.ToQuantity;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_ItemVendorReferenceDetailTable.FROMPRICE_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PO_ItemVendorReferenceDetailTable.FROMPRICE_FLD].Value = objObject.FromPrice;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_ItemVendorReferenceDetailTable.TOPRICE_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PO_ItemVendorReferenceDetailTable.TOPRICE_FLD].Value = objObject.ToPrice;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_ItemVendorReferenceDetailTable.ITEMVENDORREFERENCEDETAILID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_ItemVendorReferenceDetailTable.ITEMVENDORREFERENCEDETAILID_FLD].Value = objObject.ItemVendorReferenceDetailID;


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
		///       This method uses to get all data from PO_ItemVendorReferenceDetail
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
				+ PO_ItemVendorReferenceDetailTable.ITEMVENDORREFERENCEDETAILID_FLD + ","
				+ PO_ItemVendorReferenceDetailTable.ITEMVENDORREFERENCEID_FLD + ","
				+ PO_ItemVendorReferenceDetailTable.ENDDATE_FLD + ","
				+ PO_ItemVendorReferenceDetailTable.UNITPRICE_FLD + ","
				+ PO_ItemVendorReferenceDetailTable.FROMQUANTITY_FLD + ","
				+ PO_ItemVendorReferenceDetailTable.TOQUANTITY_FLD + ","
				+ PO_ItemVendorReferenceDetailTable.FROMPRICE_FLD + ","
				+ PO_ItemVendorReferenceDetailTable.TOPRICE_FLD
					+ " FROM " + PO_ItemVendorReferenceDetailTable.TABLE_NAME;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS,PO_ItemVendorReferenceDetailTable.TABLE_NAME);

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
				+ PO_ItemVendorReferenceDetailTable.ITEMVENDORREFERENCEDETAILID_FLD + ","
				+ PO_ItemVendorReferenceDetailTable.ITEMVENDORREFERENCEID_FLD + ","
				+ PO_ItemVendorReferenceDetailTable.ENDDATE_FLD + ","
				+ PO_ItemVendorReferenceDetailTable.UNITPRICE_FLD + ","
				+ PO_ItemVendorReferenceDetailTable.FROMQUANTITY_FLD + ","
				+ PO_ItemVendorReferenceDetailTable.TOQUANTITY_FLD + ","
				+ PO_ItemVendorReferenceDetailTable.FROMPRICE_FLD + ","
				+ PO_ItemVendorReferenceDetailTable.TOPRICE_FLD;


				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				odadPCS.SelectCommand = new OleDbCommand(strSql, oconPCS);
				odcbPCS = new OleDbCommandBuilder(odadPCS);
				pData.EnforceConstraints = false;
				odadPCS.Update(pData,PO_ItemVendorReferenceDetailTable.TABLE_NAME);

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
