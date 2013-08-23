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
	public class PRO_ChangeCategoryDetailDS 
	{
		public PRO_ChangeCategoryDetailDS()
		{
		}
		private const string THIS = "PCSComProduction.DCP.DS.DS.PRO_ChangeCategoryDetailDS";
	
		//**************************************************************************              
		///    <Description>
		///       This method uses to add data to PRO_ChangeCategoryDetail
		///    </Description>
		///    <Inputs>
		///        PRO_ChangeCategoryDetailVO       
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
		///       Wednesday, September 07, 2005
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
				PRO_ChangeCategoryDetailVO objObject = (PRO_ChangeCategoryDetailVO) pobjObjectVO;
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand("", oconPCS);
				
				strSql=	"INSERT INTO PRO_ChangeCategoryDetail("
				+ PRO_ChangeCategoryDetailTable.PRODUCTID_FLD + ","
				+ PRO_ChangeCategoryDetailTable.CHANGECATEGORYMASTERID_FLD + ")"
				+ "VALUES(?,?)";

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_ChangeCategoryDetailTable.PRODUCTID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PRO_ChangeCategoryDetailTable.PRODUCTID_FLD].Value = objObject.ProductID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_ChangeCategoryDetailTable.CHANGECATEGORYMASTERID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PRO_ChangeCategoryDetailTable.CHANGECATEGORYMASTERID_FLD].Value = objObject.ChangeCategoryMasterID;


				
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
		///       This method uses to delete data from PRO_ChangeCategoryDetail
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
			strSql=	"DELETE " + PRO_ChangeCategoryDetailTable.TABLE_NAME + " WHERE  " + "ChangeCategoryDetailID" + "=" + pintID.ToString();
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
		///       This method uses to get data from PRO_ChangeCategoryDetail
		///    </Description>
		///    <Inputs>
		///        ID       
		///    </Inputs>
		///    <Outputs>
		///       PRO_ChangeCategoryDetailVO
		///    </Outputs>
		///    <Returns>
		///       PRO_ChangeCategoryDetailVO
		///    </Returns>
		///    <Authors>
		///       HungLa
		///    </Authors>
		///    <History>
		///       Wednesday, September 07, 2005
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
				+ PRO_ChangeCategoryDetailTable.CHANGECATEGORYDETAILID_FLD + ","
				+ PRO_ChangeCategoryDetailTable.PRODUCTID_FLD + ","
				+ PRO_ChangeCategoryDetailTable.CHANGECATEGORYMASTERID_FLD
				+ " FROM " + PRO_ChangeCategoryDetailTable.TABLE_NAME
				+" WHERE " + PRO_ChangeCategoryDetailTable.CHANGECATEGORYDETAILID_FLD + "=" + pintID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();

				PRO_ChangeCategoryDetailVO objObject = new PRO_ChangeCategoryDetailVO();

				while (odrPCS.Read())
				{ 
				objObject.ChangeCategoryDetailID = int.Parse(odrPCS[PRO_ChangeCategoryDetailTable.CHANGECATEGORYDETAILID_FLD].ToString().Trim());
				objObject.ProductID = int.Parse(odrPCS[PRO_ChangeCategoryDetailTable.PRODUCTID_FLD].ToString().Trim());
				objObject.ChangeCategoryMasterID = int.Parse(odrPCS[PRO_ChangeCategoryDetailTable.CHANGECATEGORYMASTERID_FLD].ToString().Trim());

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
		///       This method uses to update data to PRO_ChangeCategoryDetail
		///    </Description>
		///    <Inputs>
		///       PRO_ChangeCategoryDetailVO       
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

			PRO_ChangeCategoryDetailVO objObject = (PRO_ChangeCategoryDetailVO) pobjObjecVO;
			

			//prepare value for parameters
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				strSql=	"UPDATE PRO_ChangeCategoryDetail SET "
				+ PRO_ChangeCategoryDetailTable.PRODUCTID_FLD + "=   ?" + ","
				+ PRO_ChangeCategoryDetailTable.CHANGECATEGORYMASTERID_FLD + "=  ?"
				+" WHERE " + PRO_ChangeCategoryDetailTable.CHANGECATEGORYDETAILID_FLD + "= ?";

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_ChangeCategoryDetailTable.PRODUCTID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PRO_ChangeCategoryDetailTable.PRODUCTID_FLD].Value = objObject.ProductID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_ChangeCategoryDetailTable.CHANGECATEGORYMASTERID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PRO_ChangeCategoryDetailTable.CHANGECATEGORYMASTERID_FLD].Value = objObject.ChangeCategoryMasterID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_ChangeCategoryDetailTable.CHANGECATEGORYDETAILID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PRO_ChangeCategoryDetailTable.CHANGECATEGORYDETAILID_FLD].Value = objObject.ChangeCategoryDetailID;


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
		///       This method uses to get all data from PRO_ChangeCategoryDetail
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
		///       Wednesday, September 07, 2005
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
				+ PRO_ChangeCategoryDetailTable.CHANGECATEGORYDETAILID_FLD + ","
				+ PRO_ChangeCategoryDetailTable.PRODUCTID_FLD + ","
				+ PRO_ChangeCategoryDetailTable.CHANGECATEGORYMASTERID_FLD + ","
				+ "(SELECT " + ITM_ProductTable.CODE_FLD + " FROM " + ITM_ProductTable.TABLE_NAME + " WHERE " + ITM_ProductTable.PRODUCTID_FLD + "=" + PRO_ChangeCategoryDetailTable.TABLE_NAME + "." + PRO_ChangeCategoryDetailTable.PRODUCTID_FLD + ") as " + ITM_ProductTable.CODE_FLD + ","
				+ "(SELECT " + ITM_ProductTable.DESCRIPTION_FLD + " FROM " + ITM_ProductTable.TABLE_NAME + " WHERE " + ITM_ProductTable.PRODUCTID_FLD + "=" + PRO_ChangeCategoryDetailTable.TABLE_NAME + "." + PRO_ChangeCategoryDetailTable.PRODUCTID_FLD + ") as " + ITM_ProductTable.DESCRIPTION_FLD + ","
				+ "(SELECT " + ITM_ProductTable.REVISION_FLD + " FROM " + ITM_ProductTable.TABLE_NAME + " WHERE " + ITM_ProductTable.PRODUCTID_FLD + "=" + PRO_ChangeCategoryDetailTable.TABLE_NAME + "." + PRO_ChangeCategoryDetailTable.PRODUCTID_FLD + ") as " + ITM_ProductTable.REVISION_FLD
				+ " FROM " + PRO_ChangeCategoryDetailTable.TABLE_NAME;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS,PRO_ChangeCategoryDetailTable.TABLE_NAME);

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
		///       This method uses to get all data from PRO_ChangeCategoryDetail
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
		///       Wednesday, September 07, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************

		public DataSet List(int pintChangeCategoryMasterID)
		{
			const string METHOD_NAME = THIS + ".List()";
			DataSet dstPCS = new DataSet();

			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				
				strSql=	"SELECT	ChangeCategoryDetailID, PRO_ChangeCategoryDetail.ProductID, ChangeCategoryMasterID,"
					+ " ITM_Category.Code AS ITM_CategoryCode, ITM_Product.Code, ITM_Product.Description, ITM_Product.Revision"
					+ " FROM PRO_ChangeCategoryDetail"
					+ " JOIN ITM_Product"
					+ " ON PRO_ChangeCategoryDetail.ProductID = ITM_Product.ProductID"
					+ " LEFT JOIN ITM_Category"
					+ " ON ITM_Product.CategoryID = ITM_Category.CategoryID"
					+ " WHERE ChangeCategoryMasterID = " + pintChangeCategoryMasterID;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS,PRO_ChangeCategoryDetailTable.TABLE_NAME);

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
		///       Wednesday, September 07, 2005
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
				+ PRO_ChangeCategoryDetailTable.CHANGECATEGORYDETAILID_FLD + ","
				+ PRO_ChangeCategoryDetailTable.PRODUCTID_FLD + ","
				+ PRO_ChangeCategoryDetailTable.CHANGECATEGORYMASTERID_FLD 
		+ "  FROM " + PRO_ChangeCategoryDetailTable.TABLE_NAME;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				odadPCS.SelectCommand = new OleDbCommand(strSql, oconPCS);
				odcbPCS = new OleDbCommandBuilder(odadPCS);
				pData.EnforceConstraints = false;
				odadPCS.Update(pData,PRO_ChangeCategoryDetailTable.TABLE_NAME);

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
