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
	public class SO_CustomerItemRefMasterDS 
	{
		public SO_CustomerItemRefMasterDS()
		{
		}
		private const string THIS = "PCSComUtils.Framework.TableFrame.DS.SO_CustomerItemRefMasterDS";
	
		//**************************************************************************              
		///    <Description>
		///       This method uses to add data to SO_CustomerItemRefMaster
		///    </Description>
		///    <Inputs>
		///        SO_CustomerItemRefMasterVO       
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
				SO_CustomerItemRefMasterVO objObject = (SO_CustomerItemRefMasterVO) pobjObjectVO;
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand("", oconPCS);
				
				strSql=	"INSERT INTO SO_CustomerItemRefMaster("
				+ SO_CustomerItemRefMasterTable.CCNID_FLD + ","
				+ SO_CustomerItemRefMasterTable.PARTYID_FLD + ")"
				+ "VALUES(?,?)";

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_CustomerItemRefMasterTable.CCNID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[SO_CustomerItemRefMasterTable.CCNID_FLD].Value = objObject.CCNID;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_CustomerItemRefMasterTable.PARTYID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[SO_CustomerItemRefMasterTable.PARTYID_FLD].Value = objObject.PartyID;


				
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
		///       This method uses to delete data from SO_CustomerItemRefMaster
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
			strSql=	"DELETE " + SO_CustomerItemRefMasterTable.TABLE_NAME + " WHERE  " + "CustomerItemRefMasterID" + "=" + pintID.ToString();
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
		///       This method uses to get data from SO_CustomerItemRefMaster
		///    </Description>
		///    <Inputs>
		///        ID       
		///    </Inputs>
		///    <Outputs>
		///       SO_CustomerItemRefMasterVO
		///    </Outputs>
		///    <Returns>
		///       SO_CustomerItemRefMasterVO
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
				+ SO_CustomerItemRefMasterTable.CUSTOMERITEMREFMASTERID_FLD + ","
				+ SO_CustomerItemRefMasterTable.CCNID_FLD + ","
				+ SO_CustomerItemRefMasterTable.PARTYID_FLD
				+ " FROM " + SO_CustomerItemRefMasterTable.TABLE_NAME
				+" WHERE " + SO_CustomerItemRefMasterTable.CUSTOMERITEMREFMASTERID_FLD + "=" + pintID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();

				SO_CustomerItemRefMasterVO objObject = new SO_CustomerItemRefMasterVO();

				while (odrPCS.Read())
				{ 
				objObject.CustomerItemRefMasterID = int.Parse(odrPCS[SO_CustomerItemRefMasterTable.CUSTOMERITEMREFMASTERID_FLD].ToString());
				objObject.CCNID = int.Parse(odrPCS[SO_CustomerItemRefMasterTable.CCNID_FLD].ToString());
				objObject.PartyID = int.Parse(odrPCS[SO_CustomerItemRefMasterTable.PARTYID_FLD].ToString());

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


		public object GetObjectVO(int pintPartyID, int pintCCNID)
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
					+ SO_CustomerItemRefMasterTable.CUSTOMERITEMREFMASTERID_FLD + ","
					+ SO_CustomerItemRefMasterTable.CCNID_FLD + ","
					+ SO_CustomerItemRefMasterTable.PARTYID_FLD
					+ " FROM " + SO_CustomerItemRefMasterTable.TABLE_NAME
					+ " WHERE " + SO_CustomerItemRefMasterTable.PARTYID_FLD + "=" + pintPartyID
					+ " AND " + SO_CustomerItemRefMasterTable.CCNID_FLD + "=" + pintCCNID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();

				SO_CustomerItemRefMasterVO objObject = new SO_CustomerItemRefMasterVO();

				while (odrPCS.Read())
				{ 
					objObject.CustomerItemRefMasterID = int.Parse(odrPCS[SO_CustomerItemRefMasterTable.CUSTOMERITEMREFMASTERID_FLD].ToString());
					objObject.CCNID = int.Parse(odrPCS[SO_CustomerItemRefMasterTable.CCNID_FLD].ToString());
					objObject.PartyID = int.Parse(odrPCS[SO_CustomerItemRefMasterTable.PARTYID_FLD].ToString());

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
		///       This method uses to update data to SO_CustomerItemRefMaster
		///    </Description>
		///    <Inputs>
		///       SO_CustomerItemRefMasterVO       
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

			SO_CustomerItemRefMasterVO objObject = (SO_CustomerItemRefMasterVO) pobjObjecVO;
			

			//prepare value for parameters
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				strSql=	"UPDATE SO_CustomerItemRefMaster SET "
				+ SO_CustomerItemRefMasterTable.CCNID_FLD + "=   ?" + ","
				+ SO_CustomerItemRefMasterTable.PARTYID_FLD + "=  ?"
				+" WHERE " + SO_CustomerItemRefMasterTable.CUSTOMERITEMREFMASTERID_FLD + "= ?";

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_CustomerItemRefMasterTable.CCNID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[SO_CustomerItemRefMasterTable.CCNID_FLD].Value = objObject.CCNID;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_CustomerItemRefMasterTable.PARTYID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[SO_CustomerItemRefMasterTable.PARTYID_FLD].Value = objObject.PartyID;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_CustomerItemRefMasterTable.CUSTOMERITEMREFMASTERID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[SO_CustomerItemRefMasterTable.CUSTOMERITEMREFMASTERID_FLD].Value = objObject.CustomerItemRefMasterID;


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
		///       This method uses to get all data from SO_CustomerItemRefMaster
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
				+ SO_CustomerItemRefMasterTable.CUSTOMERITEMREFMASTERID_FLD + ","
				+ SO_CustomerItemRefMasterTable.CCNID_FLD + ","
				+ SO_CustomerItemRefMasterTable.PARTYID_FLD
					+ " FROM " + SO_CustomerItemRefMasterTable.TABLE_NAME;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS,SO_CustomerItemRefMasterTable.TABLE_NAME);

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
				+ SO_CustomerItemRefMasterTable.CUSTOMERITEMREFMASTERID_FLD + ","
				+ SO_CustomerItemRefMasterTable.CCNID_FLD + ","
				+ SO_CustomerItemRefMasterTable.PARTYID_FLD;


				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				odadPCS.SelectCommand = new OleDbCommand(strSql, oconPCS);
				odcbPCS = new OleDbCommandBuilder(odadPCS);
				pData.EnforceConstraints = false;
				odadPCS.Update(pData,SO_CustomerItemRefMasterTable.TABLE_NAME);

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
		/// Add and return MasterID
		/// </summary>
		/// <param name="pobjObjectVO"></param>
		/// <returns></returns>
		/// <author>TuanDM 18 - 10 - 2005</author>
		public int AddAndReturnID(object pobjObjectVO)
		{
			const string METHOD_NAME = THIS + ".AddAndReturnID()";
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS =null;
			try
			{
				SO_CustomerItemRefMasterVO objObject = (SO_CustomerItemRefMasterVO) pobjObjectVO;
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand("", oconPCS);
				
				strSql=	"INSERT INTO SO_CustomerItemRefMaster("
					+ SO_CustomerItemRefMasterTable.CCNID_FLD + ","
					+ SO_CustomerItemRefMasterTable.PARTYID_FLD + ")"
					+ "VALUES(?,?) SELECT @@IDENTITY";

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_CustomerItemRefMasterTable.CCNID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[SO_CustomerItemRefMasterTable.CCNID_FLD].Value = objObject.CCNID;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_CustomerItemRefMasterTable.PARTYID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[SO_CustomerItemRefMasterTable.PARTYID_FLD].Value = objObject.PartyID;
				
				ocmdPCS.CommandText = strSql;
				ocmdPCS.Connection.Open();
				object objResult = ocmdPCS.ExecuteScalar();	
				if (objResult != null && objResult != DBNull.Value)
				{
					return int.Parse(objResult.ToString());
				}
				return 0;
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
	}
}
