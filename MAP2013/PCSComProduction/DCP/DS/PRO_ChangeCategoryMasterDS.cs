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
	public class PRO_ChangeCategoryMasterDS 
	{
		public PRO_ChangeCategoryMasterDS()
		{
		}
		private const string THIS = "PCSComProduction.DCP.DS.DS.PRO_ChangeCategoryMasterDS";

	
		//**************************************************************************              
		///    <Description>
		///       This method uses to add data to PRO_ChangeCategoryMaster
		///    </Description>
		///    <Inputs>
		///        PRO_ChangeCategoryMasterVO       
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
				PRO_ChangeCategoryMasterVO objObject = (PRO_ChangeCategoryMasterVO) pobjObjectVO;
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand("", oconPCS);
				
				strSql=	"INSERT INTO PRO_ChangeCategoryMaster("
				+ PRO_ChangeCategoryMasterTable.WORKCENTERID_FLD + ","
				+ PRO_ChangeCategoryMasterTable.CCNID_FLD + ")"
				+ "VALUES(?,?)";

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_ChangeCategoryMasterTable.WORKCENTERID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PRO_ChangeCategoryMasterTable.WORKCENTERID_FLD].Value = objObject.WorkCenterID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_ChangeCategoryMasterTable.CCNID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PRO_ChangeCategoryMasterTable.CCNID_FLD].Value = objObject.CCNID;


				
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
		///       This method uses to add data to PRO_ChangeCategoryMaster
		///    </Description>
		///    <Inputs>
		///        PRO_ChangeCategoryMasterVO       
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
		public int AddAndRetrurnID(object pobjObjectVO)
		{
			const string METHOD_NAME = THIS + ".AddAndRetrurnID()";
			 
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS =null;
			try
			{
				PRO_ChangeCategoryMasterVO objObject = (PRO_ChangeCategoryMasterVO) pobjObjectVO;
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand("", oconPCS);
				
				strSql=	"INSERT INTO PRO_ChangeCategoryMaster("
					+ PRO_ChangeCategoryMasterTable.WORKCENTERID_FLD + ","
					+ PRO_ChangeCategoryMasterTable.CCNID_FLD + ")"
					+ "VALUES(?,?) SELECT @@IDENTITY ";

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_ChangeCategoryMasterTable.WORKCENTERID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PRO_ChangeCategoryMasterTable.WORKCENTERID_FLD].Value = objObject.WorkCenterID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_ChangeCategoryMasterTable.CCNID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PRO_ChangeCategoryMasterTable.CCNID_FLD].Value = objObject.CCNID;
				ocmdPCS.CommandText = strSql;
				ocmdPCS.Connection.Open();
				object objReturn = ocmdPCS.ExecuteScalar();	
				if (objReturn != null)
				{
					return int.Parse(objReturn.ToString());
				}
				else
				{
					return 0;
				}
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
		///       This method uses to delete data from PRO_ChangeCategoryMaster
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
			strSql=	"DELETE " + PRO_ChangeCategoryMasterTable.TABLE_NAME + " WHERE  " + "ChangeCategoryMasterID" + "=" + pintID.ToString();
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
		///       This method uses to get data from PRO_ChangeCategoryMaster
		///    </Description>
		///    <Inputs>
		///        ID       
		///    </Inputs>
		///    <Outputs>
		///       PRO_ChangeCategoryMasterVO
		///    </Outputs>
		///    <Returns>
		///       PRO_ChangeCategoryMasterVO
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
				+ PRO_ChangeCategoryMasterTable.CHANGECATEGORYMASTERID_FLD + ","
				+ PRO_ChangeCategoryMasterTable.WORKCENTERID_FLD + ","
				+ PRO_ChangeCategoryMasterTable.CCNID_FLD
				+ " FROM " + PRO_ChangeCategoryMasterTable.TABLE_NAME
				+" WHERE " + PRO_ChangeCategoryMasterTable.CHANGECATEGORYMASTERID_FLD + "=" + pintID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();

				PRO_ChangeCategoryMasterVO objObject = new PRO_ChangeCategoryMasterVO();

				while (odrPCS.Read())
				{ 
				objObject.ChangeCategoryMasterID = int.Parse(odrPCS[PRO_ChangeCategoryMasterTable.CHANGECATEGORYMASTERID_FLD].ToString().Trim());
				objObject.WorkCenterID = int.Parse(odrPCS[PRO_ChangeCategoryMasterTable.WORKCENTERID_FLD].ToString().Trim());
				objObject.CCNID = int.Parse(odrPCS[PRO_ChangeCategoryMasterTable.CCNID_FLD].ToString().Trim());

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
		///       This method uses to get data from PRO_ChangeCategoryMaster
		///    </Description>
		///    <Inputs>
		///        ID       
		///    </Inputs>
		///    <Outputs>
		///       PRO_ChangeCategoryMasterVO
		///    </Outputs>
		///    <Returns>
		///       PRO_ChangeCategoryMasterVO
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

		public object GetObjectVO(int pintCCNID, int pintWorkCenterID)
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
					+ PRO_ChangeCategoryMasterTable.CHANGECATEGORYMASTERID_FLD + ","
					+ PRO_ChangeCategoryMasterTable.WORKCENTERID_FLD + ","
					+ PRO_ChangeCategoryMasterTable.CCNID_FLD
					+ " FROM " + PRO_ChangeCategoryMasterTable.TABLE_NAME
					+" WHERE " + PRO_ChangeCategoryMasterTable.CCNID_FLD + "=" + pintCCNID
					+" AND " + PRO_ChangeCategoryMasterTable.WORKCENTERID_FLD  + "=" + pintWorkCenterID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();

				PRO_ChangeCategoryMasterVO objObject = new PRO_ChangeCategoryMasterVO();

				while (odrPCS.Read())
				{ 
					objObject.ChangeCategoryMasterID = int.Parse(odrPCS[PRO_ChangeCategoryMasterTable.CHANGECATEGORYMASTERID_FLD].ToString().Trim());
					objObject.WorkCenterID = int.Parse(odrPCS[PRO_ChangeCategoryMasterTable.WORKCENTERID_FLD].ToString().Trim());
					objObject.CCNID = int.Parse(odrPCS[PRO_ChangeCategoryMasterTable.CCNID_FLD].ToString().Trim());

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
		///       This method uses to update data to PRO_ChangeCategoryMaster
		///    </Description>
		///    <Inputs>
		///       PRO_ChangeCategoryMasterVO       
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

			PRO_ChangeCategoryMasterVO objObject = (PRO_ChangeCategoryMasterVO) pobjObjecVO;
			

			//prepare value for parameters
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				strSql=	"UPDATE PRO_ChangeCategoryMaster SET "
				+ PRO_ChangeCategoryMasterTable.WORKCENTERID_FLD + "=   ?" + ","
				+ PRO_ChangeCategoryMasterTable.CCNID_FLD + "=  ?"
				+" WHERE " + PRO_ChangeCategoryMasterTable.CHANGECATEGORYMASTERID_FLD + "= ?";

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_ChangeCategoryMasterTable.WORKCENTERID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PRO_ChangeCategoryMasterTable.WORKCENTERID_FLD].Value = objObject.WorkCenterID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_ChangeCategoryMasterTable.CCNID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PRO_ChangeCategoryMasterTable.CCNID_FLD].Value = objObject.CCNID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_ChangeCategoryMasterTable.CHANGECATEGORYMASTERID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PRO_ChangeCategoryMasterTable.CHANGECATEGORYMASTERID_FLD].Value = objObject.ChangeCategoryMasterID;


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
		///       This method uses to get all data from PRO_ChangeCategoryMaster
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
				+ PRO_ChangeCategoryMasterTable.CHANGECATEGORYMASTERID_FLD + ","
				+ PRO_ChangeCategoryMasterTable.WORKCENTERID_FLD + ","
				+ PRO_ChangeCategoryMasterTable.CCNID_FLD
					+ " FROM " + PRO_ChangeCategoryMasterTable.TABLE_NAME;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS,PRO_ChangeCategoryMasterTable.TABLE_NAME);

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
				+ PRO_ChangeCategoryMasterTable.CHANGECATEGORYMASTERID_FLD + ","
				+ PRO_ChangeCategoryMasterTable.WORKCENTERID_FLD + ","
				+ PRO_ChangeCategoryMasterTable.CCNID_FLD 
		+ "  FROM " + PRO_ChangeCategoryMasterTable.TABLE_NAME;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				odadPCS.SelectCommand = new OleDbCommand(strSql, oconPCS);
				odcbPCS = new OleDbCommandBuilder(odadPCS);
				pData.EnforceConstraints = false;
				odadPCS.Update(pData,PRO_ChangeCategoryMasterTable.TABLE_NAME);

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
