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
	public class PRO_ChangeCategoryMatrixDS 
	{
		public PRO_ChangeCategoryMatrixDS()
		{
		}
		private const string THIS = "PCSComProduction.DCP.DS.DS.PRO_ChangeCategoryMatrixDS";

	
		//**************************************************************************              
		///    <Description>
		///       This method uses to add data to PRO_ChangeCategoryMatrix
		///    </Description>
		///    <Inputs>
		///        PRO_ChangeCategoryMatrixVO       
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
				PRO_ChangeCategoryMatrixVO objObject = (PRO_ChangeCategoryMatrixVO) pobjObjectVO;
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand("", oconPCS);
				
				strSql=	"INSERT INTO PRO_ChangeCategoryMatrix("
				+ PRO_ChangeCategoryMatrixTable.SOURCEPRODUCTID_FLD + ","
				+ PRO_ChangeCategoryMatrixTable.DESTPRODUCTID_FLD + ","
				+ PRO_ChangeCategoryMatrixTable.CHANGETIME_FLD + ","
				+ PRO_ChangeCategoryMatrixTable.CHANGECATEGORYMASTERID_FLD + ")"
				+ "VALUES(?,?,?,?)";

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_ChangeCategoryMatrixTable.SOURCEPRODUCTID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PRO_ChangeCategoryMatrixTable.SOURCEPRODUCTID_FLD].Value = objObject.SourceProductID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_ChangeCategoryMatrixTable.DESTPRODUCTID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PRO_ChangeCategoryMatrixTable.DESTPRODUCTID_FLD].Value = objObject.DestProductID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_ChangeCategoryMatrixTable.CHANGETIME_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PRO_ChangeCategoryMatrixTable.CHANGETIME_FLD].Value = objObject.ChangeTime;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_ChangeCategoryMatrixTable.CHANGECATEGORYMASTERID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PRO_ChangeCategoryMatrixTable.CHANGECATEGORYMASTERID_FLD].Value = objObject.ChangeCategoryMasterID;


				
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
		///       This method uses to delete data from PRO_ChangeCategoryMatrix
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
			strSql=	"DELETE " + PRO_ChangeCategoryMatrixTable.TABLE_NAME + 
			" WHERE  " + PRO_ChangeCategoryMatrixTable.CHANGECATEGORYMASTERID_FLD + "=" + pintID.ToString();
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
		///       This method uses to delete data from PRO_ChangeCategoryMatrix
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
		public void DeleteByItems(string  pstrDeletedItem)
		{
			const string METHOD_NAME = THIS + ".Delete()";
			string strSql = String.Empty;
			strSql=	"DELETE " + PRO_ChangeCategoryMatrixTable.TABLE_NAME 
				+ " WHERE  " + PRO_ChangeCategoryMatrixTable.SOURCEPRODUCTID_FLD + " in " + pstrDeletedItem
				+ " OR " + PRO_ChangeCategoryMatrixTable.DESTPRODUCTID_FLD + " in " + pstrDeletedItem;
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
		///       This method uses to get data from PRO_ChangeCategoryMatrix
		///    </Description>
		///    <Inputs>
		///        ID       
		///    </Inputs>
		///    <Outputs>
		///       PRO_ChangeCategoryMatrixVO
		///    </Outputs>
		///    <Returns>
		///       PRO_ChangeCategoryMatrixVO
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
				+ PRO_ChangeCategoryMatrixTable.CHANGECATEGORYMATRIXID_FLD + ","
				+ PRO_ChangeCategoryMatrixTable.SOURCEPRODUCTID_FLD + ","
				+ PRO_ChangeCategoryMatrixTable.DESTPRODUCTID_FLD + ","
				+ PRO_ChangeCategoryMatrixTable.CHANGETIME_FLD + ","
				+ PRO_ChangeCategoryMatrixTable.CHANGECATEGORYMASTERID_FLD
				+ " FROM " + PRO_ChangeCategoryMatrixTable.TABLE_NAME
				+" WHERE " + PRO_ChangeCategoryMatrixTable.CHANGECATEGORYMATRIXID_FLD + "=" + pintID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();

				PRO_ChangeCategoryMatrixVO objObject = new PRO_ChangeCategoryMatrixVO();

				while (odrPCS.Read())
				{ 
				objObject.ChangeCategoryMatrixID = int.Parse(odrPCS[PRO_ChangeCategoryMatrixTable.CHANGECATEGORYMATRIXID_FLD].ToString().Trim());
				objObject.SourceProductID = int.Parse(odrPCS[PRO_ChangeCategoryMatrixTable.SOURCEPRODUCTID_FLD].ToString().Trim());
				objObject.DestProductID = int.Parse(odrPCS[PRO_ChangeCategoryMatrixTable.DESTPRODUCTID_FLD].ToString().Trim());
				objObject.ChangeTime = int.Parse(odrPCS[PRO_ChangeCategoryMatrixTable.CHANGETIME_FLD].ToString().Trim());
				objObject.ChangeCategoryMasterID = int.Parse(odrPCS[PRO_ChangeCategoryMatrixTable.CHANGECATEGORYMASTERID_FLD].ToString().Trim());

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
		///       This method uses to update data to PRO_ChangeCategoryMatrix
		///    </Description>
		///    <Inputs>
		///       PRO_ChangeCategoryMatrixVO       
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

			PRO_ChangeCategoryMatrixVO objObject = (PRO_ChangeCategoryMatrixVO) pobjObjecVO;
			

			//prepare value for parameters
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				strSql=	"UPDATE PRO_ChangeCategoryMatrix SET "
				+ PRO_ChangeCategoryMatrixTable.SOURCEPRODUCTID_FLD + "=   ?" + ","
				+ PRO_ChangeCategoryMatrixTable.DESTPRODUCTID_FLD + "=   ?" + ","
				+ PRO_ChangeCategoryMatrixTable.CHANGETIME_FLD + "=   ?" + ","
				+ PRO_ChangeCategoryMatrixTable.CHANGECATEGORYMASTERID_FLD + "=  ?"
				+" WHERE " + PRO_ChangeCategoryMatrixTable.CHANGECATEGORYMATRIXID_FLD + "= ?";

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_ChangeCategoryMatrixTable.SOURCEPRODUCTID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PRO_ChangeCategoryMatrixTable.SOURCEPRODUCTID_FLD].Value = objObject.SourceProductID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_ChangeCategoryMatrixTable.DESTPRODUCTID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PRO_ChangeCategoryMatrixTable.DESTPRODUCTID_FLD].Value = objObject.DestProductID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_ChangeCategoryMatrixTable.CHANGETIME_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PRO_ChangeCategoryMatrixTable.CHANGETIME_FLD].Value = objObject.ChangeTime;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_ChangeCategoryMatrixTable.CHANGECATEGORYMASTERID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PRO_ChangeCategoryMatrixTable.CHANGECATEGORYMASTERID_FLD].Value = objObject.ChangeCategoryMasterID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_ChangeCategoryMatrixTable.CHANGECATEGORYMATRIXID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PRO_ChangeCategoryMatrixTable.CHANGECATEGORYMATRIXID_FLD].Value = objObject.ChangeCategoryMatrixID;


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
		///       This method uses to get all data from PRO_ChangeCategoryMatrix
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
				+ PRO_ChangeCategoryMatrixTable.CHANGECATEGORYMATRIXID_FLD + ","
				+ PRO_ChangeCategoryMatrixTable.SOURCEPRODUCTID_FLD + ","
				+ PRO_ChangeCategoryMatrixTable.DESTPRODUCTID_FLD + ","
				+ PRO_ChangeCategoryMatrixTable.CHANGETIME_FLD + ","
				+ PRO_ChangeCategoryMatrixTable.CHANGECATEGORYMASTERID_FLD
					+ " FROM " + PRO_ChangeCategoryMatrixTable.TABLE_NAME;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS,PRO_ChangeCategoryMatrixTable.TABLE_NAME);

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
		///       This method uses to get all data from PRO_ChangeCategoryMatrix
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
		public DataSet ListMatrixByChangeCategoryMasterID(int pintID)
		{
			const string METHOD_NAME = THIS + ".List()";
			DataSet dstPCS = new DataSet();

			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				
				strSql=	"SELECT "
					+ PRO_ChangeCategoryMatrixTable.CHANGECATEGORYMATRIXID_FLD + ","
					+ PRO_ChangeCategoryMatrixTable.SOURCEPRODUCTID_FLD + ","
					+ PRO_ChangeCategoryMatrixTable.DESTPRODUCTID_FLD + ","
					+ PRO_ChangeCategoryMatrixTable.CHANGETIME_FLD + ","
					+ PRO_ChangeCategoryMatrixTable.CHANGECATEGORYMASTERID_FLD
					+ " FROM " + PRO_ChangeCategoryMatrixTable.TABLE_NAME
					+ " WHERE "+ PRO_ChangeCategoryMatrixTable.CHANGECATEGORYMASTERID_FLD + "=" + pintID;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS,PRO_ChangeCategoryMatrixTable.TABLE_NAME);

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
				+ PRO_ChangeCategoryMatrixTable.CHANGECATEGORYMATRIXID_FLD + ","
				+ PRO_ChangeCategoryMatrixTable.SOURCEPRODUCTID_FLD + ","
				+ PRO_ChangeCategoryMatrixTable.DESTPRODUCTID_FLD + ","
				+ PRO_ChangeCategoryMatrixTable.CHANGETIME_FLD + ","
				+ PRO_ChangeCategoryMatrixTable.CHANGECATEGORYMASTERID_FLD 
		+ "  FROM " + PRO_ChangeCategoryMatrixTable.TABLE_NAME;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				odadPCS.SelectCommand = new OleDbCommand(strSql, oconPCS);
				odcbPCS = new OleDbCommandBuilder(odadPCS);
				pData.EnforceConstraints = false;
				odadPCS.Update(pData,PRO_ChangeCategoryMatrixTable.TABLE_NAME);

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
