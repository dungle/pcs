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
	public class SO_CustomerItemRefDetailDS 
	{
		public SO_CustomerItemRefDetailDS()
		{
		}
		private const string THIS = "PCSComUtils.Framework.TableFrame.DS.SO_CustomerItemRefDetailDS";

	
		//**************************************************************************              
		///    <Description>
		///       This method uses to add data to SO_CustomerItemRefDetail
		///    </Description>
		///    <Inputs>
		///        SO_CustomerItemRefDetailVO       
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
		///       Tuesday, October 18, 2005
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
				SO_CustomerItemRefDetailVO objObject = (SO_CustomerItemRefDetailVO) pobjObjectVO;
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand("", oconPCS);
				
				strSql=	"INSERT INTO SO_CustomerItemRefDetail("
				+ SO_CustomerItemRefDetailTable.CUSTOMERITEMCODE_FLD + ","
				+ SO_CustomerItemRefDetailTable.CUSTOMERITEMMODEL_FLD + ","
				+ SO_CustomerItemRefDetailTable.PRODUCTID_FLD + ","
				+ SO_CustomerItemRefDetailTable.CUSTOMERITEMREFMASTERID_FLD + ","
				+ SO_CustomerItemRefDetailTable.UNITPRICE_FLD + ","
				+ SO_CustomerItemRefDetailTable.UNITOFMEASUREID_FLD + ")"
				+ "VALUES(?,?,?,?,?,?)";

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_CustomerItemRefDetailTable.CUSTOMERITEMCODE_FLD, OleDbType.VarChar));
				ocmdPCS.Parameters[SO_CustomerItemRefDetailTable.CUSTOMERITEMCODE_FLD].Value = objObject.CustomerItemCode;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_CustomerItemRefDetailTable.CUSTOMERITEMMODEL_FLD, OleDbType.VarChar));
				ocmdPCS.Parameters[SO_CustomerItemRefDetailTable.CUSTOMERITEMMODEL_FLD].Value = objObject.CustomerItemModel;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_CustomerItemRefDetailTable.PRODUCTID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[SO_CustomerItemRefDetailTable.PRODUCTID_FLD].Value = objObject.ProductID;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_CustomerItemRefDetailTable.CUSTOMERITEMREFMASTERID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[SO_CustomerItemRefDetailTable.CUSTOMERITEMREFMASTERID_FLD].Value = objObject.CustomerItemRefMasterID;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_CustomerItemRefDetailTable.UNITPRICE_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[SO_CustomerItemRefDetailTable.UNITPRICE_FLD].Value = objObject.UnitPrice;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_CustomerItemRefDetailTable.UNITOFMEASUREID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[SO_CustomerItemRefDetailTable.UNITOFMEASUREID_FLD].Value = objObject.UnitOfMeasureID;


				
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
		///       This method uses to delete data from SO_CustomerItemRefDetail
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
			strSql=	"DELETE " + SO_CustomerItemRefDetailTable.TABLE_NAME + " WHERE  " + "CustomerItemRefDetailID" + "=" + pintID.ToString();
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
		///       This method uses to get data from SO_CustomerItemRefDetail
		///    </Description>
		///    <Inputs>
		///        ID       
		///    </Inputs>
		///    <Outputs>
		///       SO_CustomerItemRefDetailVO
		///    </Outputs>
		///    <Returns>
		///       SO_CustomerItemRefDetailVO
		///    </Returns>
		///    <Authors>
		///       HungLa
		///    </Authors>
		///    <History>
		///       Tuesday, October 18, 2005
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
				+ SO_CustomerItemRefDetailTable.CUSTOMERITEMREFDETAILID_FLD + ","
				+ SO_CustomerItemRefDetailTable.CUSTOMERITEMCODE_FLD + ","
				+ SO_CustomerItemRefDetailTable.CUSTOMERITEMMODEL_FLD + ","
				+ SO_CustomerItemRefDetailTable.PRODUCTID_FLD + ","
				+ SO_CustomerItemRefDetailTable.CUSTOMERITEMREFMASTERID_FLD + ","
				+ SO_CustomerItemRefDetailTable.UNITPRICE_FLD + ","
				+ SO_CustomerItemRefDetailTable.UNITOFMEASUREID_FLD
				+ " FROM " + SO_CustomerItemRefDetailTable.TABLE_NAME
				+" WHERE " + SO_CustomerItemRefDetailTable.CUSTOMERITEMREFDETAILID_FLD + "=" + pintID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();

				SO_CustomerItemRefDetailVO objObject = null;

				if (odrPCS.Read())
				{ 
					objObject = new SO_CustomerItemRefDetailVO();
					objObject.CustomerItemRefDetailID = int.Parse(odrPCS[SO_CustomerItemRefDetailTable.CUSTOMERITEMREFDETAILID_FLD].ToString());
					objObject.CustomerItemCode = odrPCS[SO_CustomerItemRefDetailTable.CUSTOMERITEMCODE_FLD].ToString();
					objObject.CustomerItemModel = odrPCS[SO_CustomerItemRefDetailTable.CUSTOMERITEMMODEL_FLD].ToString();
					objObject.ProductID = int.Parse(odrPCS[SO_CustomerItemRefDetailTable.PRODUCTID_FLD].ToString());
					objObject.CustomerItemRefMasterID = int.Parse(odrPCS[SO_CustomerItemRefDetailTable.CUSTOMERITEMREFMASTERID_FLD].ToString());
					objObject.UnitPrice = Decimal.Parse(odrPCS[SO_CustomerItemRefDetailTable.UNITPRICE_FLD].ToString());
					objObject.UnitOfMeasureID = int.Parse(odrPCS[SO_CustomerItemRefDetailTable.UNITOFMEASUREID_FLD].ToString());
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
		///       This method uses to get data from SO_CustomerItemRefDetail
		///    </Description>
		///    <Inputs>
		///        ID       
		///    </Inputs>
		///    <Outputs>
		///       SO_CustomerItemRefDetailVO
		///    </Outputs>
		///    <Returns>
		///       SO_CustomerItemRefDetailVO
		///    </Returns>
		///    <Authors>
		///       HungLa
		///    </Authors>
		///    <History>
		///       Tuesday, October 18, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public object GetObjectVO(int pintProductID, int pintCustomerID)
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
					+ SO_CustomerItemRefDetailTable.CUSTOMERITEMREFDETAILID_FLD + ","
					+ SO_CustomerItemRefDetailTable.CUSTOMERITEMCODE_FLD + ","
					+ SO_CustomerItemRefDetailTable.CUSTOMERITEMMODEL_FLD + ","
					+ SO_CustomerItemRefDetailTable.PRODUCTID_FLD + ","
					+ SO_CustomerItemRefDetailTable.TABLE_NAME + "." + SO_CustomerItemRefDetailTable.CUSTOMERITEMREFMASTERID_FLD + ","
					+ SO_CustomerItemRefDetailTable.UNITPRICE_FLD + ","
					+ SO_CustomerItemRefDetailTable.UNITOFMEASUREID_FLD
					+ " FROM " + SO_CustomerItemRefDetailTable.TABLE_NAME + " INNER JOIN " + SO_CustomerItemRefMasterTable.TABLE_NAME
					+ " ON "  + SO_CustomerItemRefDetailTable.TABLE_NAME + "." + SO_CustomerItemRefDetailTable.CUSTOMERITEMREFMASTERID_FLD 
					+ " = "   + SO_CustomerItemRefMasterTable.TABLE_NAME + "." + SO_CustomerItemRefMasterTable.CUSTOMERITEMREFMASTERID_FLD
					+ " WHERE " + SO_CustomerItemRefDetailTable.PRODUCTID_FLD + "=" + pintProductID 
					+ " AND " + SO_CustomerItemRefMasterTable.PARTYID_FLD + "=" + pintCustomerID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();

				SO_CustomerItemRefDetailVO objObject = null;

				if (odrPCS.Read())
				{ 
					objObject = new SO_CustomerItemRefDetailVO();
					objObject.CustomerItemRefDetailID = int.Parse(odrPCS[SO_CustomerItemRefDetailTable.CUSTOMERITEMREFDETAILID_FLD].ToString());
					objObject.CustomerItemCode = odrPCS[SO_CustomerItemRefDetailTable.CUSTOMERITEMCODE_FLD].ToString();
					objObject.CustomerItemModel = odrPCS[SO_CustomerItemRefDetailTable.CUSTOMERITEMMODEL_FLD].ToString();
					objObject.ProductID = int.Parse(odrPCS[SO_CustomerItemRefDetailTable.PRODUCTID_FLD].ToString());
					objObject.CustomerItemRefMasterID = int.Parse(odrPCS[SO_CustomerItemRefDetailTable.CUSTOMERITEMREFMASTERID_FLD].ToString());
					objObject.UnitPrice = Decimal.Parse(odrPCS[SO_CustomerItemRefDetailTable.UNITPRICE_FLD].ToString());
					objObject.UnitOfMeasureID = int.Parse(odrPCS[SO_CustomerItemRefDetailTable.UNITOFMEASUREID_FLD].ToString());
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
		///       This method uses to update data to SO_CustomerItemRefDetail
		///    </Description>
		///    <Inputs>
		///       SO_CustomerItemRefDetailVO       
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

			SO_CustomerItemRefDetailVO objObject = (SO_CustomerItemRefDetailVO) pobjObjecVO;
			

			//prepare value for parameters
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				strSql=	"UPDATE SO_CustomerItemRefDetail SET "
				+ SO_CustomerItemRefDetailTable.CUSTOMERITEMCODE_FLD + "=   ?" + ","
				+ SO_CustomerItemRefDetailTable.CUSTOMERITEMMODEL_FLD + "=   ?" + ","
				+ SO_CustomerItemRefDetailTable.PRODUCTID_FLD + "=   ?" + ","
				+ SO_CustomerItemRefDetailTable.CUSTOMERITEMREFMASTERID_FLD + "=   ?" + ","
				+ SO_CustomerItemRefDetailTable.UNITPRICE_FLD + "=   ?" + ","
				+ SO_CustomerItemRefDetailTable.UNITOFMEASUREID_FLD + "=  ?"
				+" WHERE " + SO_CustomerItemRefDetailTable.CUSTOMERITEMREFDETAILID_FLD + "= ?";

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_CustomerItemRefDetailTable.CUSTOMERITEMCODE_FLD, OleDbType.VarChar));
				ocmdPCS.Parameters[SO_CustomerItemRefDetailTable.CUSTOMERITEMCODE_FLD].Value = objObject.CustomerItemCode;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_CustomerItemRefDetailTable.CUSTOMERITEMMODEL_FLD, OleDbType.VarChar));
				ocmdPCS.Parameters[SO_CustomerItemRefDetailTable.CUSTOMERITEMMODEL_FLD].Value = objObject.CustomerItemModel;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_CustomerItemRefDetailTable.PRODUCTID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[SO_CustomerItemRefDetailTable.PRODUCTID_FLD].Value = objObject.ProductID;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_CustomerItemRefDetailTable.CUSTOMERITEMREFMASTERID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[SO_CustomerItemRefDetailTable.CUSTOMERITEMREFMASTERID_FLD].Value = objObject.CustomerItemRefMasterID;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_CustomerItemRefDetailTable.UNITPRICE_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[SO_CustomerItemRefDetailTable.UNITPRICE_FLD].Value = objObject.UnitPrice;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_CustomerItemRefDetailTable.UNITOFMEASUREID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[SO_CustomerItemRefDetailTable.UNITOFMEASUREID_FLD].Value = objObject.UnitOfMeasureID;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_CustomerItemRefDetailTable.CUSTOMERITEMREFDETAILID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[SO_CustomerItemRefDetailTable.CUSTOMERITEMREFDETAILID_FLD].Value = objObject.CustomerItemRefDetailID;


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
		///       This method uses to get all data from SO_CustomerItemRefDetail
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
		///       Tuesday, October 18, 2005
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
				+ SO_CustomerItemRefDetailTable.CUSTOMERITEMREFDETAILID_FLD + ","
				+ SO_CustomerItemRefDetailTable.CUSTOMERITEMCODE_FLD + ","
				+ SO_CustomerItemRefDetailTable.CUSTOMERITEMMODEL_FLD + ","
				+ SO_CustomerItemRefDetailTable.PRODUCTID_FLD + ","
				+ SO_CustomerItemRefDetailTable.CUSTOMERITEMREFMASTERID_FLD + ","
				+ SO_CustomerItemRefDetailTable.UNITPRICE_FLD + ","
				+ SO_CustomerItemRefDetailTable.UNITOFMEASUREID_FLD
					+ " FROM " + SO_CustomerItemRefDetailTable.TABLE_NAME;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS,SO_CustomerItemRefDetailTable.TABLE_NAME);
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
		///       Tuesday, October 18, 2005
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
				+ SO_CustomerItemRefDetailTable.CUSTOMERITEMREFDETAILID_FLD + ","
				+ SO_CustomerItemRefDetailTable.CUSTOMERITEMCODE_FLD + ","
				+ SO_CustomerItemRefDetailTable.CUSTOMERITEMMODEL_FLD + ","
				+ SO_CustomerItemRefDetailTable.PRODUCTID_FLD + ","
				+ SO_CustomerItemRefDetailTable.CUSTOMERITEMREFMASTERID_FLD + ","
				+ SO_CustomerItemRefDetailTable.UNITPRICE_FLD + ","
				+ SO_CustomerItemRefDetailTable.UNITOFMEASUREID_FLD
				+ " FROM " + SO_CustomerItemRefDetailTable.TABLE_NAME;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				odadPCS.SelectCommand = new OleDbCommand(strSql, oconPCS);
				odcbPCS = new OleDbCommandBuilder(odadPCS);
				pData.EnforceConstraints = false;
				odadPCS.Update(pData, "SO_CustomerItemRefDetail");

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
		/// Get detail by MasterID and CCNID
		/// </summary>
		/// <param name="pintMasterID"></param>
		/// <param name="pintCCNID"></param>
		/// <returns></returns>
		public DataSet List(int pintMasterID, int pintCCNID)
		{
			const string METHOD_NAME = THIS + ".List()";
			DataSet dstPCS = new DataSet();

			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				strSql=	"SELECT "
					+ ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.CODE_FLD + ","
					+ ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.DESCRIPTION_FLD + ","
					+ ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.REVISION_FLD + ","
					+ ITM_CategoryTable.TABLE_NAME + "." + ITM_CategoryTable.CODE_FLD + " as " + ITM_CategoryTable.TABLE_NAME + ITM_CategoryTable.CODE_FLD + ","
					+ MST_UnitOfMeasureTable.TABLE_NAME + "." + MST_UnitOfMeasureTable.CODE_FLD + " as " + MST_UnitOfMeasureTable.TABLE_NAME + MST_UnitOfMeasureTable.CODE_FLD + ","
					+ SO_CustomerItemRefDetailTable.UNITPRICE_FLD + ","
					+ SO_CustomerItemRefDetailTable.CUSTOMERITEMREFDETAILID_FLD + ","
					+ SO_CustomerItemRefDetailTable.CUSTOMERITEMCODE_FLD + ","
					+ SO_CustomerItemRefDetailTable.CUSTOMERITEMMODEL_FLD + ","
					+ SO_CustomerItemRefDetailTable.TABLE_NAME + "." + SO_CustomerItemRefDetailTable.PRODUCTID_FLD + ","
					+ SO_CustomerItemRefDetailTable.TABLE_NAME + "." + SO_CustomerItemRefDetailTable.CUSTOMERITEMREFMASTERID_FLD + ","
					+ SO_CustomerItemRefDetailTable.TABLE_NAME + "." + SO_CustomerItemRefDetailTable.UNITOFMEASUREID_FLD
					+ " FROM " + SO_CustomerItemRefDetailTable.TABLE_NAME + " INNER JOIN "
					+ SO_CustomerItemRefMasterTable.TABLE_NAME + " ON " + SO_CustomerItemRefMasterTable.TABLE_NAME + "." + SO_CustomerItemRefMasterTable.CUSTOMERITEMREFMASTERID_FLD
					+ "=" + SO_CustomerItemRefDetailTable.TABLE_NAME + "." + SO_CustomerItemRefDetailTable.CUSTOMERITEMREFMASTERID_FLD
					+ " INNER JOIN " + ITM_ProductTable.TABLE_NAME + " ON " + ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.PRODUCTID_FLD + "="
					+ SO_CustomerItemRefDetailTable.TABLE_NAME + "." + SO_CustomerItemRefDetailTable.PRODUCTID_FLD
					+ " LEFT JOIN MST_UnitOfMeasure on MST_UnitOfMeasure.UnitOfMeasureID = " + SO_CustomerItemRefDetailTable.TABLE_NAME + "." + SO_CustomerItemRefDetailTable.UNITOFMEASUREID_FLD
					+ " LEFT JOIN ITM_Category on ITM_Category.CategoryID = " + ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.CATEGORYID_FLD
					+ " WHERE "+ SO_CustomerItemRefMasterTable.TABLE_NAME + "." + SO_CustomerItemRefMasterTable.PARTYID_FLD + "=" + pintMasterID
					+ " AND "  + SO_CustomerItemRefMasterTable.TABLE_NAME + "." + SO_CustomerItemRefMasterTable.CCNID_FLD + "=" + pintCCNID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS,SO_CustomerItemRefDetailTable.TABLE_NAME);

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

		#region // HACK: DuongNA 2005-10-20

		//**************************************************************************              
		///    <Description>
		///       This method uses to get data from SO_CustomerItemRefDetail
		///    </Description>
		///    <Inputs>
		///        ID       
		///    </Inputs>
		///    <Outputs>
		///       SO_CustomerItemRefDetailVO
		///    </Outputs>
		///    <Returns>
		///       SO_CustomerItemRefDetailVO
		///    </Returns>
		///    <Authors>
		///       DuongNA
		///    </Authors>
		///    <History>
		///       Thursday, October 20, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************

		public object GetObjectVO(int pintMasterID, string pstrCustomerItemCode)
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
					+ SO_CustomerItemRefDetailTable.CUSTOMERITEMREFDETAILID_FLD + ","
					+ SO_CustomerItemRefDetailTable.CUSTOMERITEMCODE_FLD + ","
					+ SO_CustomerItemRefDetailTable.CUSTOMERITEMMODEL_FLD + ","
					+ SO_CustomerItemRefDetailTable.PRODUCTID_FLD + ","
					+ SO_CustomerItemRefDetailTable.CUSTOMERITEMREFMASTERID_FLD + ","
					+ SO_CustomerItemRefDetailTable.UNITPRICE_FLD + ","
					+ SO_CustomerItemRefDetailTable.UNITOFMEASUREID_FLD
					+ " FROM " + SO_CustomerItemRefDetailTable.TABLE_NAME
					+ " WHERE " + SO_CustomerItemRefDetailTable.CUSTOMERITEMREFMASTERID_FLD + "=" + pintMasterID
					+ " AND RTRIM(LTRIM(" + SO_CustomerItemRefDetailTable.CUSTOMERITEMCODE_FLD + "))= ?";

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_CustomerItemRefDetailTable.CUSTOMERITEMCODE_FLD, OleDbType.VarChar));
				ocmdPCS.Parameters[SO_CustomerItemRefDetailTable.CUSTOMERITEMCODE_FLD].Value = pstrCustomerItemCode;

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();

				SO_CustomerItemRefDetailVO objObject = new SO_CustomerItemRefDetailVO();

				while (odrPCS.Read())
				{ 
					objObject.CustomerItemRefDetailID = int.Parse(odrPCS[SO_CustomerItemRefDetailTable.CUSTOMERITEMREFDETAILID_FLD].ToString());
					objObject.CustomerItemCode = odrPCS[SO_CustomerItemRefDetailTable.CUSTOMERITEMCODE_FLD].ToString();
					objObject.CustomerItemModel = odrPCS[SO_CustomerItemRefDetailTable.CUSTOMERITEMMODEL_FLD].ToString();
					objObject.ProductID = int.Parse(odrPCS[SO_CustomerItemRefDetailTable.PRODUCTID_FLD].ToString());
					objObject.CustomerItemRefMasterID = int.Parse(odrPCS[SO_CustomerItemRefDetailTable.CUSTOMERITEMREFMASTERID_FLD].ToString());
					objObject.UnitPrice = Decimal.Parse(odrPCS[SO_CustomerItemRefDetailTable.UNITPRICE_FLD].ToString());
					objObject.UnitOfMeasureID = int.Parse(odrPCS[SO_CustomerItemRefDetailTable.UNITOFMEASUREID_FLD].ToString());

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

		#endregion // END: DuongNA 2005-10-20

		//**************************************************************************              
		///    <Description>
		///       This method uses to get data from SO_CustomerItemRefDetail
		///    </Description>
		///    <Inputs>
		///        ID       
		///    </Inputs>
		///    <Outputs>
		///       SO_CustomerItemRefDetailVO
		///    </Outputs>
		///    <Returns>
		///       SO_CustomerItemRefDetailVO
		///    </Returns>
		///    <Authors>
		///       TuanDM
		///    </Authors>
		///    <History>
		///       Thursday, October 20, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************

		public object GetObjectVO(int pintMasterID, string pstrCustomerItemCode, string pstrCustomerItemModel)
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
					+ SO_CustomerItemRefDetailTable.CUSTOMERITEMREFDETAILID_FLD + ","
					+ SO_CustomerItemRefDetailTable.CUSTOMERITEMCODE_FLD + ","
					+ SO_CustomerItemRefDetailTable.CUSTOMERITEMMODEL_FLD + ","
					+ SO_CustomerItemRefDetailTable.PRODUCTID_FLD + ","
					+ SO_CustomerItemRefDetailTable.CUSTOMERITEMREFMASTERID_FLD + ","
					+ SO_CustomerItemRefDetailTable.UNITPRICE_FLD + ","
					+ SO_CustomerItemRefDetailTable.UNITOFMEASUREID_FLD
					+ " FROM " + SO_CustomerItemRefDetailTable.TABLE_NAME
					+ " WHERE " + SO_CustomerItemRefDetailTable.CUSTOMERITEMREFMASTERID_FLD + "=" + pintMasterID
					+ " AND RTRIM(LTRIM(" + SO_CustomerItemRefDetailTable.CUSTOMERITEMCODE_FLD + "))= ?"
					+ " AND RTRIM(LTRIM(" + SO_CustomerItemRefDetailTable.CUSTOMERITEMMODEL_FLD + "))= ?";

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_CustomerItemRefDetailTable.CUSTOMERITEMCODE_FLD, OleDbType.VarChar));
				ocmdPCS.Parameters[SO_CustomerItemRefDetailTable.CUSTOMERITEMCODE_FLD].Value = pstrCustomerItemCode;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_CustomerItemRefDetailTable.CUSTOMERITEMMODEL_FLD, OleDbType.VarChar));
				ocmdPCS.Parameters[SO_CustomerItemRefDetailTable.CUSTOMERITEMMODEL_FLD].Value = pstrCustomerItemModel;

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();

				SO_CustomerItemRefDetailVO objObject = new SO_CustomerItemRefDetailVO();

				while (odrPCS.Read())
				{ 
					objObject.CustomerItemRefDetailID = int.Parse(odrPCS[SO_CustomerItemRefDetailTable.CUSTOMERITEMREFDETAILID_FLD].ToString());
					objObject.CustomerItemCode = odrPCS[SO_CustomerItemRefDetailTable.CUSTOMERITEMCODE_FLD].ToString();
					objObject.CustomerItemModel = odrPCS[SO_CustomerItemRefDetailTable.CUSTOMERITEMMODEL_FLD].ToString();
					objObject.ProductID = int.Parse(odrPCS[SO_CustomerItemRefDetailTable.PRODUCTID_FLD].ToString());
					objObject.CustomerItemRefMasterID = int.Parse(odrPCS[SO_CustomerItemRefDetailTable.CUSTOMERITEMREFMASTERID_FLD].ToString());
					objObject.UnitPrice = Decimal.Parse(odrPCS[SO_CustomerItemRefDetailTable.UNITPRICE_FLD].ToString());
					objObject.UnitOfMeasureID = int.Parse(odrPCS[SO_CustomerItemRefDetailTable.UNITOFMEASUREID_FLD].ToString());

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

	}
}
