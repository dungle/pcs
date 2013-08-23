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
	public class SO_GateDS 
	{
		public SO_GateDS()
		{
		}
		private const string THIS = "PCSComSale.Order.DS.SO_GateDS";

	
		//**************************************************************************              
		///    <Description>
		///       This method uses to add data to SO_Gate
		///    </Description>
		///    <Inputs>
		///        SO_GateVO       
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
		///       Friday, June 02, 2006
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************


		public void Add(object pobjObjectVO)
		{
//			const string METHOD_NAME = THIS + ".Add()";
//			 
//			OleDbConnection oconPCS =null;
//			OleDbCommand ocmdPCS =null;
//			try
//			{
//				SO_GateVO objObject = (SO_GateVO) pobjObjectVO;
//				string strSql = String.Empty;
//				Utils utils = new Utils();
//				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
//				ocmdPCS = new OleDbCommand("", oconPCS);
//				
//				strSql=	"INSERT INTO SO_Gate("
//				+ SO_GateTable.CODE_FLD + ","
//				+ SO_GateTable.DESCRIPTION_FLD + ","
//				+ SO_GateTable.GATETYPEID_FLD + ")"
//				+ "VALUES(?,?,?)";
//
//				ocmdPCS.Parameters.Add(new OleDbParameter(SO_GateTable.CODE_FLD, OleDbType.WChar));
//				ocmdPCS.Parameters[SO_GateTable.CODE_FLD].Value = objObject.Code;
//
//				ocmdPCS.Parameters.Add(new OleDbParameter(SO_GateTable.DESCRIPTION_FLD, OleDbType.WChar));
//				ocmdPCS.Parameters[SO_GateTable.DESCRIPTION_FLD].Value = objObject.Description;
//
//				ocmdPCS.Parameters.Add(new OleDbParameter(SO_GateTable.GATETYPEID_FLD, OleDbType.Integer));
//				ocmdPCS.Parameters[SO_GateTable.GATETYPEID_FLD].Value = objObject.GateTypeID;
//
//
//				
//				ocmdPCS.CommandText = strSql;
//				ocmdPCS.Connection.Open();
//				ocmdPCS.ExecuteNonQuery();	
//
//			}
//			catch(OleDbException ex)
//			{
//				if (ex.Errors.Count > 1)
//				{
//					if (ex.Errors[1].NativeError == ErrorCode.SQLDUPLICATE_KEYCODE)
//					{
//																   
//						throw new PCSDBException(ErrorCode.DUPLICATE_KEY, METHOD_NAME, ex);				
//					}
//				}
//				else
//				{
//					throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME,ex);
//				}
//			}			
//
//			catch(InvalidOperationException ex)
//			{
//				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME,ex);
//			}
//			catch (Exception ex) 
//			{
//				throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
//			}
//
//			finally 
//			{
//				if (oconPCS!=null) 
//				{
//					if (oconPCS.State != ConnectionState.Closed) 
//					{
//						oconPCS.Close();
//					}
//				}
//			}
		}
	

	

		//**************************************************************************              
		///    <Description>
		///       This method uses to delete data from SO_Gate
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
//			const string METHOD_NAME = THIS + ".Delete()";
//			string strSql = String.Empty;
//			strSql=	"DELETE " + SO_GateTable.TABLE_NAME + " WHERE  " + "GateID" + "=" + pintID.ToString();
//			OleDbConnection oconPCS=null;
//			OleDbCommand ocmdPCS =null;
//			try
//			{
//				Utils utils = new Utils();
//				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
//				ocmdPCS = new OleDbCommand(strSql, oconPCS);
//
//				ocmdPCS.Connection.Open();
//				ocmdPCS.ExecuteNonQuery();	
//				ocmdPCS = null;
//
//			}
//			catch(OleDbException ex)
//			{
//				if (ex.Errors.Count > 1)
//				{
//					if (ex.Errors[1].NativeError == ErrorCode.SQLCASCADE_PREVENT_KEYCODE)
//					{
//						throw new PCSDBException(ErrorCode.CASCADE_DELETE_PREVENT, METHOD_NAME, ex);				
//					}
//				}
//				else
//				{
//					throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME,ex);
//				}
//			}
//			catch (Exception ex) 
//			{
//				throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
//			}
//			finally 
//			{
//				if (oconPCS!=null) 
//				{
//					if (oconPCS.State != ConnectionState.Closed) 
//					{
//						oconPCS.Close();
//					}
//				}
//			}
		}
	

	

		//**************************************************************************              
		///    <Description>
		///       This method uses to get data from SO_Gate
		///    </Description>
		///    <Inputs>
		///        ID       
		///    </Inputs>
		///    <Outputs>
		///       SO_GateVO
		///    </Outputs>
		///    <Returns>
		///       SO_GateVO
		///    </Returns>
		///    <Authors>
		///       HungLa
		///    </Authors>
		///    <History>
		///       Friday, June 02, 2006
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************

		public object GetObjectVO(int pintID)
		{
			return null;
//			const string METHOD_NAME = THIS + ".GetObjectVO()";
//			DataSet dstPCS = new DataSet();
//			
//			OleDbDataReader odrPCS = null;
//			OleDbConnection oconPCS = null;
//			OleDbCommand ocmdPCS = null;
//			try 
//			{
//				string strSql = String.Empty;
//				strSql=	"SELECT "
//				+ SO_GateTable.GATEID_FLD + ","
//				+ SO_GateTable.CODE_FLD + ","
//				+ SO_GateTable.DESCRIPTION_FLD + ","
//				+ SO_GateTable.GATETYPEID_FLD
//				+ " FROM " + SO_GateTable.TABLE_NAME
//				+" WHERE " + SO_GateTable.GATEID_FLD + "=" + pintID;
//
//				Utils utils = new Utils();
//				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
//				ocmdPCS = new OleDbCommand(strSql, oconPCS);
//
//				ocmdPCS.Connection.Open();
//				odrPCS = ocmdPCS.ExecuteReader();
//
//				SO_GateVO objObject = new SO_GateVO();
//
//				while (odrPCS.Read())
//				{ 
//				objObject.GateID = int.Parse(odrPCS[SO_GateTable.GATEID_FLD].ToString().Trim());
//				objObject.Code = odrPCS[SO_GateTable.CODE_FLD].ToString().Trim();
//				objObject.Description = odrPCS[SO_GateTable.DESCRIPTION_FLD].ToString().Trim();
//				objObject.GateTypeID = int.Parse(odrPCS[SO_GateTable.GATETYPEID_FLD].ToString().Trim());
//
//				}		
//				return objObject;					
//			}
//			catch(OleDbException ex)
//			{			
//				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME,ex);
//			}			
//
//			catch (Exception ex) 
//			{
//				throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
//			}
//			finally 
//			{
//				if (oconPCS!=null) 
//				{
//					if (oconPCS.State != ConnectionState.Closed) 
//					{
//						oconPCS.Close();
//					}
//				}
//			}
		}


		//**************************************************************************              
		///    <Description>
		///       This method uses to update data to SO_Gate
		///    </Description>
		///    <Inputs>
		///       SO_GateVO       
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
//			const string METHOD_NAME = THIS + ".Update()";
//
//			SO_GateVO objObject = (SO_GateVO) pobjObjecVO;
//			
//
//			//prepare value for parameters
//			OleDbConnection oconPCS =null;
//			OleDbCommand ocmdPCS = null;
//			try 
//			{
//				string strSql = String.Empty;
//				Utils utils = new Utils();
//				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
//				ocmdPCS = new OleDbCommand(strSql, oconPCS);
//				strSql=	"UPDATE SO_Gate SET "
//				+ SO_GateTable.CODE_FLD + "=   ?" + ","
//				+ SO_GateTable.DESCRIPTION_FLD + "=   ?" + ","
//				+ SO_GateTable.GATETYPEID_FLD + "=  ?"
//				+" WHERE " + SO_GateTable.GATEID_FLD + "= ?";
//
//				ocmdPCS.Parameters.Add(new OleDbParameter(SO_GateTable.CODE_FLD, OleDbType.WChar));
//				ocmdPCS.Parameters[SO_GateTable.CODE_FLD].Value = objObject.Code;
//
//				ocmdPCS.Parameters.Add(new OleDbParameter(SO_GateTable.DESCRIPTION_FLD, OleDbType.WChar));
//				ocmdPCS.Parameters[SO_GateTable.DESCRIPTION_FLD].Value = objObject.Description;
//
//				ocmdPCS.Parameters.Add(new OleDbParameter(SO_GateTable.GATETYPEID_FLD, OleDbType.Integer));
//				ocmdPCS.Parameters[SO_GateTable.GATETYPEID_FLD].Value = objObject.GateTypeID;
//
//				ocmdPCS.Parameters.Add(new OleDbParameter(SO_GateTable.GATEID_FLD, OleDbType.Integer));
//				ocmdPCS.Parameters[SO_GateTable.GATEID_FLD].Value = objObject.GateID;
//
//
//				ocmdPCS.CommandText = strSql;
//				ocmdPCS.Connection.Open();
//				ocmdPCS.ExecuteNonQuery();	
//			}
//			catch(OleDbException ex)
//			{
//				if (ex.Errors.Count > 1)
//				{
//					if (ex.Errors[1].NativeError == ErrorCode.SQLDUPLICATE_KEYCODE)
//					{
//																   
//						throw new PCSDBException(ErrorCode.DUPLICATE_KEY, METHOD_NAME, ex);				
//					}
//				}
//				else
//				{
//					throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME,ex);
//				}
//			}			
//
//			catch(InvalidOperationException ex)
//			{
//				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME,ex);
//			}
//			catch (Exception ex) 
//			{
//				throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
//			}
//
//			finally 
//			{
//				if (oconPCS!=null) 
//				{
//					if (oconPCS.State != ConnectionState.Closed) 
//					{
//						oconPCS.Close();
//					}
//				}
//			}

		}


		//**************************************************************************              
		///    <Description>
		///       This method uses to get all data from SO_Gate
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
		///       Friday, June 02, 2006
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
				+ SO_GateTable.GATEID_FLD + ","
				+ SO_GateTable.CODE_FLD + ","
				+ SO_GateTable.DESCRIPTION_FLD + ","
				+ SO_GateTable.GATETYPEID_FLD
					+ " FROM " + SO_GateTable.TABLE_NAME;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS,SO_GateTable.TABLE_NAME);

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
		///       Friday, June 02, 2006
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		
		public void UpdateDataSet(DataSet pData)
		{
//			const string METHOD_NAME = THIS + ".UpdateDataSet()";
//			string strSql;
//			OleDbConnection oconPCS =null;
//			OleDbCommandBuilder odcbPCS ;
//			OleDbDataAdapter odadPCS = new OleDbDataAdapter();
//
//			try
//			{
//				strSql=	"SELECT "
//				+ SO_GateTable.GATEID_FLD + ","
//				+ SO_GateTable.CODE_FLD + ","
//				+ SO_GateTable.DESCRIPTION_FLD + ","
//				+ SO_GateTable.GATETYPEID_FLD 
//		+ "  FROM " + SO_GateTable.TABLE_NAME;
//
//				Utils utils = new Utils();
//				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
//				odadPCS.SelectCommand = new OleDbCommand(strSql, oconPCS);
//				odcbPCS = new OleDbCommandBuilder(odadPCS);
//				pData.EnforceConstraints = false;
//				odadPCS.Update(pData,SO_GateTable.TABLE_NAME);
//
//			}
//			catch(OleDbException ex)
//			{
//				if(ex.Errors.Count > 1)
//				{
//					if (ex.Errors[1].NativeError == ErrorCode.SQLDUPLICATE_KEYCODE)
//					{
//						throw new PCSDBException(ErrorCode.DUPLICATE_KEY, METHOD_NAME, ex);				
//					}
//					else if (ex.Errors[1].NativeError == ErrorCode.SQLCASCADE_PREVENT_KEYCODE)
//					{
//						throw new PCSDBException(ErrorCode.CASCADE_DELETE_PREVENT, METHOD_NAME, ex);				
//					}
//				}
//				else
//				{
//					throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME,ex);
//				}
//			}			
//
//			catch(InvalidOperationException ex)
//			{
//				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME,ex);
//			}
//			catch (Exception ex) 
//			{
//				throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
//			}
//
//			finally 
//			{
//				if (oconPCS!=null) 
//				{
//					if (oconPCS.State != ConnectionState.Closed) 
//					{
//						oconPCS.Close();
//					}
//				}
//			}

		}
	}
}
