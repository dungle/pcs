using System;
using System.Data;
using System.Data.OleDb;
using System.Collections;
using System.Configuration;
using PCSComUtils.DataAccess;
using PCSComUtils.PCSExc;
using PCSComUtils.Common;

namespace PCSComUtils.Admin.DS
{
	public class Sys_VisibilityItemDS 
	{
		public Sys_VisibilityItemDS()
		{
		}
		private const string THIS = "PCSComUtils.Admin.DS.Sys_VisibilityItemDS";

	
		//**************************************************************************              
		///    <Description>
		///       This method uses to add data to Sys_VisibilityItem
		///    </Description>
		///    <Inputs>
		///        Sys_VisibilityItemVO       
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
				Sys_VisibilityItemVO objObject = (Sys_VisibilityItemVO) pobjObjectVO;
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand("", oconPCS);
				
				strSql=	"INSERT INTO Sys_VisibilityItem("
				+ Sys_VisibilityItemTable.NAME_FLD + ","
				+ Sys_VisibilityItemTable.GROUPID_FLD + ","
				+ Sys_VisibilityItemTable.TYPE_FLD + ")"
				+ "VALUES(?,?,?)";

				ocmdPCS.Parameters.Add(new OleDbParameter(Sys_VisibilityItemTable.NAME_FLD, OleDbType.WChar));
				ocmdPCS.Parameters[Sys_VisibilityItemTable.NAME_FLD].Value = objObject.Name;

				ocmdPCS.Parameters.Add(new OleDbParameter(Sys_VisibilityItemTable.GROUPID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[Sys_VisibilityItemTable.GROUPID_FLD].Value = objObject.GroupID;

				ocmdPCS.Parameters.Add(new OleDbParameter(Sys_VisibilityItemTable.TYPE_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[Sys_VisibilityItemTable.TYPE_FLD].Value = objObject.Type;


				
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
		///       This method uses to delete data from Sys_VisibilityItem
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
			strSql=	"DELETE " + Sys_VisibilityItemTable.TABLE_NAME + " WHERE  " + "VisibilityItemID" + "=" + pintID.ToString();
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
		///       This method uses to get data from Sys_VisibilityItem
		///    </Description>
		///    <Inputs>
		///        ID       
		///    </Inputs>
		///    <Outputs>
		///       Sys_VisibilityItemVO
		///    </Outputs>
		///    <Returns>
		///       Sys_VisibilityItemVO
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
				+ Sys_VisibilityItemTable.VISIBILITYITEMID_FLD + ","
				+ Sys_VisibilityItemTable.NAME_FLD + ","
				+ Sys_VisibilityItemTable.GROUPID_FLD + ","
				+ Sys_VisibilityItemTable.TYPE_FLD
				+ " FROM " + Sys_VisibilityItemTable.TABLE_NAME
				+" WHERE " + Sys_VisibilityItemTable.VISIBILITYITEMID_FLD + "=" + pintID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();

				Sys_VisibilityItemVO objObject = new Sys_VisibilityItemVO();

				while (odrPCS.Read())
				{ 
				objObject.VisibilityItemID = int.Parse(odrPCS[Sys_VisibilityItemTable.VISIBILITYITEMID_FLD].ToString().Trim());
				objObject.Name = odrPCS[Sys_VisibilityItemTable.NAME_FLD].ToString().Trim();
				objObject.GroupID = int.Parse(odrPCS[Sys_VisibilityItemTable.GROUPID_FLD].ToString().Trim());
				objObject.Type = int.Parse(odrPCS[Sys_VisibilityItemTable.TYPE_FLD].ToString().Trim());

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
		///       This method uses to update data to Sys_VisibilityItem
		///    </Description>
		///    <Inputs>
		///       Sys_VisibilityItemVO       
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

			Sys_VisibilityItemVO objObject = (Sys_VisibilityItemVO) pobjObjecVO;
			

			//prepare value for parameters
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				strSql=	"UPDATE Sys_VisibilityItem SET "
				+ Sys_VisibilityItemTable.NAME_FLD + "=   ?" + ","
				+ Sys_VisibilityItemTable.GROUPID_FLD + "=   ?" + ","
				+ Sys_VisibilityItemTable.TYPE_FLD + "=  ?"
				+" WHERE " + Sys_VisibilityItemTable.VISIBILITYITEMID_FLD + "= ?";

				ocmdPCS.Parameters.Add(new OleDbParameter(Sys_VisibilityItemTable.NAME_FLD, OleDbType.WChar));
				ocmdPCS.Parameters[Sys_VisibilityItemTable.NAME_FLD].Value = objObject.Name;

				ocmdPCS.Parameters.Add(new OleDbParameter(Sys_VisibilityItemTable.GROUPID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[Sys_VisibilityItemTable.GROUPID_FLD].Value = objObject.GroupID;

				ocmdPCS.Parameters.Add(new OleDbParameter(Sys_VisibilityItemTable.TYPE_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[Sys_VisibilityItemTable.TYPE_FLD].Value = objObject.Type;

				ocmdPCS.Parameters.Add(new OleDbParameter(Sys_VisibilityItemTable.VISIBILITYITEMID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[Sys_VisibilityItemTable.VISIBILITYITEMID_FLD].Value = objObject.VisibilityItemID;


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
		///       This method uses to get all data from Sys_VisibilityItem
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
				+ Sys_VisibilityItemTable.VISIBILITYITEMID_FLD + ","
				+ Sys_VisibilityItemTable.NAME_FLD + ","
				+ Sys_VisibilityItemTable.GROUPID_FLD + ","
				+ Sys_VisibilityItemTable.TYPE_FLD
					+ " FROM " + Sys_VisibilityItemTable.TABLE_NAME;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS,Sys_VisibilityItemTable.TABLE_NAME);

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
				+ Sys_VisibilityItemTable.VISIBILITYITEMID_FLD + ","
				+ Sys_VisibilityItemTable.NAME_FLD + ","
				+ Sys_VisibilityItemTable.GROUPID_FLD + ","
				+ Sys_VisibilityItemTable.TYPE_FLD 
		+ "  FROM " + Sys_VisibilityItemTable.TABLE_NAME;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				odadPCS.SelectCommand = new OleDbCommand(strSql, oconPCS);
				odcbPCS = new OleDbCommandBuilder(odadPCS);
				pData.EnforceConstraints = false;
				odadPCS.Update(pData,Sys_VisibilityItemTable.TABLE_NAME);

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
