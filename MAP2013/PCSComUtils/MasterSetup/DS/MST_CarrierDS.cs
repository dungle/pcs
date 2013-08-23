using System;
using System.Data;
using System.Data.OleDb;
using PCSComUtils.DataAccess;
using PCSComUtils.PCSExc;
using PCSComUtils.Common;

namespace PCSComUtils.MasterSetup.DS
{
	public class MST_CarrierDS 
	{
		public MST_CarrierDS()
		{
		}
		private const string THIS = "PCSComUtils.MasterSetup.DS.MST_CarrierDS";

	
		//**************************************************************************              
		///    <Description>
		///       This method uses to add data to MST_Carrier
		///    </Description>
		///    <Inputs>
		///        MST_CarrierVO       
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
		///       Tuesday, January 25, 2005
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
				MST_CarrierVO objObject = (MST_CarrierVO) pobjObjectVO;
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand("", oconPCS);
				
				strSql=	"INSERT INTO MST_Carrier("
				+ MST_CarrierTable.CODE_FLD + ","
				+ MST_CarrierTable.NAME_FLD + ","
				+ MST_CarrierTable.ADDRESS_FLD + ","
				+ MST_CarrierTable.PHONE_FLD + ","
				+ MST_CarrierTable.FAX_FLD + ","
				+ MST_CarrierTable.EMAIL_FLD + ","
				+ MST_CarrierTable.WEBSITE_FLD + ")"
				+ "VALUES(?,?,?,?,?,?,?)";

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_CarrierTable.CODE_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[MST_CarrierTable.CODE_FLD].Value = objObject.Code;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_CarrierTable.NAME_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[MST_CarrierTable.NAME_FLD].Value = objObject.Name;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_CarrierTable.ADDRESS_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[MST_CarrierTable.ADDRESS_FLD].Value = objObject.Address;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_CarrierTable.PHONE_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[MST_CarrierTable.PHONE_FLD].Value = objObject.Phone;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_CarrierTable.FAX_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[MST_CarrierTable.FAX_FLD].Value = objObject.Fax;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_CarrierTable.EMAIL_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[MST_CarrierTable.EMAIL_FLD].Value = objObject.Email;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_CarrierTable.WEBSITE_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[MST_CarrierTable.WEBSITE_FLD].Value = objObject.WebSite;


				
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
		///       This method uses to delete data from MST_Carrier
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
			strSql=	"DELETE " + MST_CarrierTable.TABLE_NAME + " WHERE  " + "CarrierID" + "=" + pintID.ToString();
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
		///       This method uses to get data from MST_Carrier
		///    </Description>
		///    <Inputs>
		///        ID       
		///    </Inputs>
		///    <Outputs>
		///       MST_CarrierVO
		///    </Outputs>
		///    <Returns>
		///       MST_CarrierVO
		///    </Returns>
		///    <Authors>
		///       HungLa
		///    </Authors>
		///    <History>
		///       Tuesday, January 25, 2005
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
				+ MST_CarrierTable.CARRIERID_FLD + ","
				+ MST_CarrierTable.CODE_FLD + ","
				+ MST_CarrierTable.NAME_FLD + ","
				+ MST_CarrierTable.ADDRESS_FLD + ","
				+ MST_CarrierTable.PHONE_FLD + ","
				+ MST_CarrierTable.FAX_FLD + ","
				+ MST_CarrierTable.EMAIL_FLD + ","
				+ MST_CarrierTable.WEBSITE_FLD
				+ " FROM " + MST_CarrierTable.TABLE_NAME
				+" WHERE " + MST_CarrierTable.CARRIERID_FLD + "=" + pintID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();

				MST_CarrierVO objObject = new MST_CarrierVO();

				while (odrPCS.Read())
				{ 
				objObject.CarrierID = int.Parse(odrPCS[MST_CarrierTable.CARRIERID_FLD].ToString().Trim());
				objObject.Code = odrPCS[MST_CarrierTable.CODE_FLD].ToString().Trim();
				objObject.Name = odrPCS[MST_CarrierTable.NAME_FLD].ToString().Trim();
				objObject.Address = odrPCS[MST_CarrierTable.ADDRESS_FLD].ToString().Trim();
				objObject.Phone = odrPCS[MST_CarrierTable.PHONE_FLD].ToString().Trim();
				objObject.Fax = odrPCS[MST_CarrierTable.FAX_FLD].ToString().Trim();
				objObject.Email = odrPCS[MST_CarrierTable.EMAIL_FLD].ToString().Trim();
				objObject.WebSite = odrPCS[MST_CarrierTable.WEBSITE_FLD].ToString().Trim();

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
		///       This method uses to update data to MST_Carrier
		///    </Description>
		///    <Inputs>
		///       MST_CarrierVO       
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

			MST_CarrierVO objObject = (MST_CarrierVO) pobjObjecVO;
			

			//prepare value for parameters
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				strSql=	"UPDATE MST_Carrier SET "
				+ MST_CarrierTable.CODE_FLD + "=   ?" + ","
				+ MST_CarrierTable.NAME_FLD + "=   ?" + ","
				+ MST_CarrierTable.ADDRESS_FLD + "=   ?" + ","
				+ MST_CarrierTable.PHONE_FLD + "=   ?" + ","
				+ MST_CarrierTable.FAX_FLD + "=   ?" + ","
				+ MST_CarrierTable.EMAIL_FLD + "=   ?" + ","
				+ MST_CarrierTable.WEBSITE_FLD + "=  ?"
				+" WHERE " + MST_CarrierTable.CARRIERID_FLD + "= ?";

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_CarrierTable.CODE_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[MST_CarrierTable.CODE_FLD].Value = objObject.Code;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_CarrierTable.NAME_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[MST_CarrierTable.NAME_FLD].Value = objObject.Name;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_CarrierTable.ADDRESS_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[MST_CarrierTable.ADDRESS_FLD].Value = objObject.Address;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_CarrierTable.PHONE_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[MST_CarrierTable.PHONE_FLD].Value = objObject.Phone;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_CarrierTable.FAX_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[MST_CarrierTable.FAX_FLD].Value = objObject.Fax;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_CarrierTable.EMAIL_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[MST_CarrierTable.EMAIL_FLD].Value = objObject.Email;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_CarrierTable.WEBSITE_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[MST_CarrierTable.WEBSITE_FLD].Value = objObject.WebSite;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_CarrierTable.CARRIERID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[MST_CarrierTable.CARRIERID_FLD].Value = objObject.CarrierID;


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
		///       This method uses to get all data from MST_Carrier
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
		///       Tuesday, January 25, 2005
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
				+ MST_CarrierTable.CARRIERID_FLD + ","
				+ MST_CarrierTable.CODE_FLD + ","
				+ MST_CarrierTable.NAME_FLD + ","
				+ MST_CarrierTable.ADDRESS_FLD + ","
				+ MST_CarrierTable.PHONE_FLD + ","
				+ MST_CarrierTable.FAX_FLD + ","
				+ MST_CarrierTable.EMAIL_FLD + ","
				+ MST_CarrierTable.WEBSITE_FLD
					+ " FROM " + MST_CarrierTable.TABLE_NAME;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS,MST_CarrierTable.TABLE_NAME);

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
		///       Tuesday, January 25, 2005
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
				+ MST_CarrierTable.CARRIERID_FLD + ","
				+ MST_CarrierTable.CODE_FLD + ","
				+ MST_CarrierTable.NAME_FLD + ","
				+ MST_CarrierTable.ADDRESS_FLD + ","
				+ MST_CarrierTable.PHONE_FLD + ","
				+ MST_CarrierTable.FAX_FLD + ","
				+ MST_CarrierTable.EMAIL_FLD + ","
				+ MST_CarrierTable.WEBSITE_FLD 
		+ "  FROM " + MST_CarrierTable.TABLE_NAME;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				odadPCS.SelectCommand = new OleDbCommand(strSql, oconPCS);
				odcbPCS = new OleDbCommandBuilder(odadPCS);
				pData.EnforceConstraints = false;
				odadPCS.Update(pData,MST_CarrierTable.TABLE_NAME);

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
